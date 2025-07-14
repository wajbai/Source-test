using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

using System.Configuration;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Threading;
using System.ComponentModel;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace Acme.erpSupport
{
    public enum SupportRequest
    {
        GET_CONNECTION_STRING,
        UPDATE_CONNECTION_STRING,
        UPDATE_ACPERP_ROOT_PASSWORD,
        ACMEERP_SERVICE_INFO,
        TEST_CONNECTION,
        GET_USER_PASSWORD,
        DELETE_PROJECT,
        DELETE_FD_BY_PROJECTS,
        DELETE_FD_ALL,
        DELETE_ALL_VOUCHERS_WHICH_ARE_OUT_OF_BOOKS_BEGIN,
        UPDATE_LOCATION_IN_SETTING_FROM_LICENSE_KEY,
        CLEAR_AND_RESET_VOUCHERS,
        RESTORE_DB_BACKUP
    }

    public enum DefaultLocation
    {
        Primary
    }

    public static class General
    {
        private static Assembly currentAssembly = Assembly.GetExecutingAssembly();
        private static SimpleEncrypt.SimpleEncDec simpleencrypt = new SimpleEncrypt.SimpleEncDec();

        public static string ACMEERP_TITLE = "Acme.erp Supporting Tool";
        public static string AcMEERP_PRODUCT_NAME = "Acme.erp";

        public static string ACMEERP_DB_NAME  = "acperp";
        public static bool ACMEERP_IS_INSTALLED = false;
        public static string ACMEERP_INSTALLED_PATH = string.Empty;
        public static string ACMEERP_VERSION = string.Empty;
        public static string ACMEERP_DB_CONNECTION = string.Empty;
        public static string ACMEERP_MULTIDB_CONNECTION = string.Empty;

        private static string LOG_FILE = "acmeerpsupporting.log";
        private static string RegistryPath = @"Software\BoscoSoft\Acme.erp";//Acme.erp
        
        //for temp purpose
        private static string RegistryPathOLD = @"Software\BoscoSoft\AcMEERP";
        private static ResultArgs result = new ResultArgs();

        public static bool IS_ADMIN_User = false;
        public static bool IS_GET_USER_PWD_User = false;
        public static bool IS_UPDATE_LOCATION_User = false;
        public static bool IS_DELETE_PROJECT_User = false;

        public static ResultArgs AssignAcmeerpProperties()
        {
            ResultArgs result = new ResultArgs();
            try
            {
                result = GetAcmeerpInstalledPath();
                if (result.Success)
                {
                    ACMEERP_INSTALLED_PATH = result.ReturnValue.ToString();
                    if (Directory.Exists(ACMEERP_INSTALLED_PATH))
                    {
                        result = CurrentVersion();
                        if (result.Success)
                        {
                            ACMEERP_VERSION = result.ReturnValue.ToString();
                            result = GetDBconnectionstring();
                            if (result.Success)
                            {
                                ACMEERP_DB_CONNECTION = result.ReturnValue.ToString();
                                if (!string.IsNullOrEmpty(ACMEERP_DB_CONNECTION))
                                {
                                    ACMEERP_IS_INSTALLED = true;
                                    result.Success = true;
                                }
                                else
                                {
                                    result.Message = "DB connection tag is not found in app.confg file";
                                }
                            }
                        }
                    }
                    else
                    {
                        result.Message = "Acmeerp Installtion path is not found in the registry";
                    }
                    
                }
            }
            catch (Exception err)
            {
                result.Message = "Error in AssignAcmeerpProperties " + err.Message;
            }
            return result;
        }
        
        public static bool IsAcmeerpRunning()
        {
            bool rtn = false;
            try
            {

                Process[] processByName = Process.GetProcessesByName("acpp");
                rtn =  (processByName.Length >=1 );
            }
            catch (Exception err)
            {
                rtn = false;
                MessageRender.ShowMessage("Problem in checking Acmeerp is running " + err.Message);
            }
            return rtn;
        }
                
        public static ResultArgs GetDBconnectionstring()
        {
            ResultArgs result = new ResultArgs();
            string Rtn = string.Empty;
            try
            {
                if (Directory.Exists(ACMEERP_INSTALLED_PATH))
                {
                    string acppexepath = Path.Combine(ACMEERP_INSTALLED_PATH, "ACPP.exe");
                    Configuration acperpconfig = ConfigurationManager.OpenExeConfiguration(acppexepath);
                    
                    if (acperpconfig != null)
                    {
                        Rtn = acperpconfig.ConnectionStrings.ConnectionStrings["AppConnectionString"].ToString(); ;
                        MySqlConnectionStringBuilder mysqlconnectionstring = new MySqlConnectionStringBuilder(Rtn);
                        General.ACMEERP_DB_NAME =  mysqlconnectionstring.Database;
                    }
                }

                if (!string.IsNullOrEmpty(Rtn))
                {
                    result.Success = true;
                    result.ReturnValue = Rtn;
                }
                else
                {
                    result.Message = "ConnectionString is not found ";
                }
            }
            catch (Exception err)
            {
                result.Message = "Error in getting acperp db connectionstring " + err.Message;
            }

            return result;
        }

        public static ResultArgs UpdateAcmeerpRootPassword()
        {
            ResultArgs result = new ResultArgs();
            string Rtn = string.Empty;
            try
            {
                string rootauthendication = "use mysql; GRANT ALL PRIVILEGES ON *.* TO 'root'@'%' WITH GRANT OPTION;" +
                                             "FLUSH PRIVILEGES;" +
                                             "update user set password = PASSWORD('acperproot') where User='root';" +
                                             "FLUSH PRIVILEGES;";
                result = ExecuteCommand(rootauthendication);
                if (result.Success)
                {
                    result.ReturnValue = "Acmeerp root password is updated in Acmeerp MySQL Server";
                    result.Success = true;
                }
                else
                {
                    result.Message = "Error in processing request:Update Acme.erp root password";
                }
            }
            catch (Exception err)
            {
                result.Message = "Error in processing request:Update Acme.erp root password " + err.Message;
            }
            return result;
        }

        public static ResultArgs GetUserInfo()
        {
            ResultArgs result = new ResultArgs();
            string Rtn = string.Empty;
            try
            {
                result = ExecuteTable("SELECT user_id, user_name, password FROM user_info");
                if (result.Success)
                {
                    DataTable dtUserinfo = result.DataSource.Data as DataTable;

                    if (dtUserinfo != null)
                    {
                        if (dtUserinfo.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtUserinfo.Rows)
                            {
                                Rtn += dr["user_id"].ToString() + "::" + string.Empty + "::" + dr["user_name"].ToString()
                                        + "::" + DecryptString(dr["password"].ToString()) + Environment.NewLine;
                            }

                            result.ReturnValue = Rtn;
                            result.Success = true;
                        }
                        else
                        {
                            result.Message = "Error in processing request:could not get User Info ";
                        }
                    }
                }
            }
            catch (Exception err)
            {
                result.Message = "Error in processing request:Get User Info " + err.Message;
            }
            return result;
        }

        public static ResultArgs DeleteProject(Int32 projectid)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                result = ExecuteTable("CALL DROPPROJECT(" + projectid +")");
                if (result.Success)
                {
                    DataTable dt = result.DataSource.Data as DataTable;
                    if (dt.Rows.Count > 0)
                    {
                        result.Message = "Removed";
                        result.Success = true;
                    }
                }
            }
            catch (Exception err)
            {
                result.Message = "Error in processing request: DeleteProject " + err.Message;
            }
            return result;
        }

        public static ResultArgs DeleteFD(Int32 fdaccountid)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                result = ExecuteTable("CALL DROPFD(" + fdaccountid + ")");
                if (result.Success)
                {
                    DataTable dt = result.DataSource.Data as DataTable;
                    if (dt.Rows.Count > 0)
                    {
                        result.Message = "Removed";
                        result.Success = true;
                    }
                }
            }
            catch (Exception err)
            {
                result.Message = "Error in processing request: DeleteProject " + err.Message;
            }
            return result;
        }

        public static ResultArgs DeleteAllFD()
        {
            ResultArgs result = new ResultArgs();
            try
            {
                string queryDeleteAllFDs = "DELETE FROM FD_RENEWAL;\n" +
                                           "DELETE FROM FD_ACCOUNT;\n" +
                                           "DELETE FROM VOUCHER_TRANS WHERE VOUCHER_ID IN (SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS WHERE VOUCHER_SUB_TYPE = 'FD');\n" +
                                           "DELETE FROM LEDGER_BALANCE WHERE LEDGER_ID IN (SELECT LEDGER_Id FROM MASTER_LEDGER WHERE GROUP_ID=14);\n" +
                                           "DELETE FROM LEDGER_BALANCE WHERE TRANS_FLAG = 'OP' and AMOUNT=0;";

                result = ExecuteCommand(queryDeleteAllFDs);
                if (result.Success)
                {
                    result.Message = "All FDs are cleared";
                    result.Success = true;
                }
            }
            catch (Exception err)
            {
                result.Message = "Error in processing request: DeleteAllFD " + err.Message;
            }
            return result;
        }

        public static ResultArgs GetCurrentLocation()
        {
            ResultArgs result = new ResultArgs();
            string Rtn = string.Empty;
            try
            {
                string query = "SELECT SETTING_NAME, VALUE FROM MASTER_SETTING " +
                                "WHERE SETTING_NAME = 'Location'";

                result = ExecuteTable(query);
                if (result.Success)
                {
                    DataTable dtCurrentLocation = result.DataSource.Data as DataTable;

                    if (dtCurrentLocation != null && dtCurrentLocation.Rows.Count==1)
                    {
                        result.ReturnValue = dtCurrentLocation.Rows[0]["VALUE"].ToString().Trim();
                        result.Success = true;
                    }
                }
            }
            catch (Exception err)
            {
                result.Message = "Error in processing request: GetCurrentLocation" + err.Message;
            }
            return result;
        }

        public static ResultArgs UpdateCurrentLocation(string newlocation)
        {
            ResultArgs result = new ResultArgs();
            string Rtn = string.Empty;
            try
            {
                string query = "UPDATE MASTER_SETTING SET VALUE='" + newlocation + "' " +
                                "WHERE SETTING_NAME = 'Location'";

                result = ExecuteTable(query);
                if (result.Success)
                {
                    DataTable dtCurrentLocation = result.DataSource.Data as DataTable;

                    if (dtCurrentLocation != null && dtCurrentLocation.Rows.Count == 1)
                    {
                        result.ReturnValue = dtCurrentLocation.Rows[0]["VALUE"].ToString().Trim();
                        result.Success = true;
                    }
                }
            }
            catch (Exception err)
            {
                result.Message = "Error in processing request: UpdateCurrentLocation" + err.Message;
            }
            return result;
        }

        public static ResultArgs ClearResetVouchers(DateTime dtFrom, DateTime dtTo, DateTime dtBooksBegin, bool ResetOpBalance)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                
                //date = DateTime.ParseExact(dtFrom.ToShortDateString(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                string frm = dtFrom.ToString("yyyy-MM-dd");
                string to = dtTo.ToString("yyyy-MM-dd");
                string bb = dtBooksBegin.ToString("yyyy-MM-dd");

                result = ExecuteTable("CALL ResetVouchersTables('" + frm + "','" + to + "','" + bb + "'," + ResetOpBalance + ")");
                if (result.Success)
                {
                    DataTable dt = result.DataSource.Data as DataTable;
                    if (dt.Rows.Count > 0)
                    {
                        result.Message = "All Vouchers and FY are cleared";
                        result.Success = true;
                    }
                }
            }
            catch (Exception err)
            {
                result.Message = "Error in processing request: ClearResetVouchers " + err.Message;
            }
            return result;
        }

        public static ResultArgs GetProjects()
        {
            ResultArgs result = new ResultArgs();
            string Rtn = string.Empty;
            try
            {
                string query = "SELECT " +
                                "PROJECT_ID, CONCAT(CONCAT(PROJECT, CONCAT(' #',MD.DIVISION)), IF(DELETE_FLAG=0,' #Active',' #Cancelled')) as PROJECT_NAME " +
                                "FROM " +
                                "MASTER_PROJECT MP " +
                                "INNER JOIN MASTER_DIVISION MD ON " +
                                "MP.DIVISION_ID=MD.DIVISION_ID ORDER BY PROJECT ";

                result = ExecuteTable(query);
                if (result.Success)
                {
                    DataTable dtprojects = result.DataSource.Data as DataTable;
                    result.DataSource.Data  = dtprojects.DefaultView;

                }
            }
            catch (Exception err)
            {
                result.Message = "Error in processing request: GetProjects " + err.Message;
            }
            return result;
        }

        public static ResultArgs GetLocations()
        {
            ResultArgs result = new ResultArgs();
            string branchLocation = LicenseDetails.BranchLocations;
            DataTable dtBranches = new DataTable();
            dtBranches.Columns.Add("LOCATION_NAME", typeof(string));
            try
            {
                if (!string.IsNullOrEmpty(branchLocation))
                {
                    string[] branches = branchLocation.Split(',');
                    if (branches.Count() > 0)
                    {
                        foreach (string name in branches)
                        {
                            if (!string.IsNullOrEmpty(name))
                            {
                                dtBranches.Rows.Add(name);
                            }
                        }
                    }
                    else
                    {
                        dtBranches.Rows.Add(DefaultLocation.Primary.ToString());
                    }
                }
                result.Success = true;
                result.DataSource.Data = dtBranches;
            }
            catch (Exception err)
            {
                result.Message = "Error in processing request: GetLocations " + err.Message;
            }
            return result;
        }

        public static ResultArgs GetRecentFY()
        {
            ResultArgs result = new ResultArgs();
            DataTable dtRecentFY = new DataTable();
            string Rtn = string.Empty;
            try
            {
                //# get recent AY
                string query = "SELECT YEAR_FROM, YEAR_TO, BOOKS_BEGINNING_FROM " +
                                "FROM ACCOUNTING_YEAR AY ORDER BY YEAR_FROM DESC LIMIT 1";

                result = ExecuteTable(query);
                if (result.Success && result.DataSource.Data!=null)
                {
                    dtRecentFY = result.DataSource.Data as DataTable;

                    //# Books Begin
                    query = "SELECT YEAR_FROM, YEAR_TO, BOOKS_BEGINNING_FROM " +
                                "FROM ACCOUNTING_YEAR AY WHERE IS_FIRST_ACCOUNTING_YEAR=1";
                    result = ExecuteTable(query);
                    if (result.Success && result.DataSource.Table != null)
                    {
                        DataTable dtBooksBegin = result.DataSource.Table;
                        dtRecentFY.Rows[0]["BOOKS_BEGINNING_FROM"] = dtBooksBegin.Rows[0]["BOOKS_BEGINNING_FROM"].ToString();
                        dtRecentFY.AcceptChanges();

                        result.DataSource.Data = dtRecentFY;
                    }
                }
            }
            catch (Exception err)
            {
                result.Message = "Error in processing request: GetRecentFY " + err.Message;
            }
            return result;
        }

        public static ResultArgs GetFDAccounts(int projectid)
        {
            ResultArgs result = new ResultArgs();
            DataView dvFDs= null;
            string Rtn = string.Empty;
            try
            {
                string query = "SELECT fd_account_id, CONCAT(CONCAT(trans_type, CONCAT(' ' , fd_account_number)), IF(STATUS=1, ' #Active', ' #Deleted')) fd_account_number " +
                                "FROM FD_ACCOUNT " +
                                "WHERE project_id = "+ projectid +
                                " ORDER BY trans_type DESC, fd_account_number";

                result = ExecuteTable(query);
                if (result.Success)
                {
                    DataTable dtFDs = result.DataSource.Data as DataTable;
                    result.DataSource.Data = dtFDs.DefaultView;
                }
            }
            catch (Exception err)
            {
                result.Message = "Error in processing request: GetProjects " + err.Message;
            }
            return result;
        }

        public static ResultArgs GetBranches()
        {
            ResultArgs result = new ResultArgs();
            DataTable dtBranch = new DataTable();
            try
            {
                string multibranchpath = @"C:\AcME_ERP\MultipleDB.xml";
                if (File.Exists(multibranchpath))
                {
                    DataSet dsMultiBranch = new DataSet();
                    dsMultiBranch.ReadXml(multibranchpath);

                    if (dsMultiBranch.Tables[0].Rows.Count > 0)
                    {
                        dtBranch = dsMultiBranch.Tables[0];
                        dtBranch.Columns.Add("Default", typeof(System.Int32), "RESTORE_DB <> 'acperp'");
                        dtBranch.DefaultView.Sort = "Default ASC";
                        dtBranch = dtBranch.DefaultView.ToTable();
                        result.DataSource.Data = dtBranch;
                        result.Success = true;
                    }
                }
            }
            catch (Exception Ex)
            {
                result.Message = "Error in getting multi branch " + Ex.Message;
            }
            finally { }

            return result;
        }

        public static string GetBranchLicensefile(string branch)
        {
            ResultArgs result = new ResultArgs();
            string Rtn = string.Empty;
            result = GetBranches();
            if (result.Success)
            {
                DataTable dtBranches = result.DataSource.Data as DataTable;
                if (dtBranches.Rows.Count > 0)
                {
                    dtBranches.DefaultView.RowFilter = "RestoreDBName = '" + branch + "'";
                    if (dtBranches.DefaultView.Count > 0)
                    {
                        Rtn = dtBranches.DefaultView[0]["MultipleLicenseKey"].ToString();
                    }
                }
            }
            return Rtn ;
        }
        
        public static ResultArgs GetEnumItemType(string enumItem)
        {
            ResultArgs result = new ResultArgs();
            SupportRequest request = new SupportRequest();
            Type enumType = request.GetType();
            Enum enumTypeItem = null;

            if (enumType != null)
            {
                try
                {
                    enumTypeItem = (Enum)Enum.Parse(enumType, enumItem, true);
                    result.ReturnValue = enumTypeItem;
                    result.Success = true;
                }
                catch (Exception err)
                {
                    result.Message = "Error in Converting Enum " + err.Message;
                }
            }

            return result;
        }

        public static ResultArgs GetEnumDataSource()
        {
            ResultArgs result = new ResultArgs();
            SupportRequest request = new SupportRequest();
            Type genrequest = request.GetType();
            DataView dvEnumSource = new DataView();
            DataRow drEnumSource = null;
            DataTable dtEnumSource = new DataTable();
            dtEnumSource.Columns.Add("Id", typeof(System.Int32));
            dtEnumSource.Columns.Add("Name", typeof(System.String));

            if (genrequest != null)
            {
                try
                {
                    string[] enumNames = Enum.GetNames(genrequest);
                    int enumValue = 0;

                    foreach (string enumName in enumNames)
                    {
                        enumValue = (int)Enum.Parse(genrequest, enumName, true);

                        drEnumSource = dtEnumSource.NewRow();
                        drEnumSource["Id"] = enumValue;
                        drEnumSource["Name"] = enumName;
                        dtEnumSource.Rows.Add(drEnumSource);
                    }

                    dtEnumSource.AcceptChanges();
                    dvEnumSource = dtEnumSource.DefaultView;
                    dvEnumSource.Sort = "Id";
                    result.DataSource.Data = dvEnumSource;
                    result.Success = true;
                }
                catch (Exception err)
                {
                    result.Message = "Error in Converting Enum as dataview" + err.Message;
                }
            }

            return result;
        }
        
        private static ResultArgs CurrentVersion()
        {
            ResultArgs result = new ResultArgs();
            try
            {
                string Rtn = string.Empty;
                string acpexepath = Path.Combine(General.ACMEERP_INSTALLED_PATH, "ACPP.exe");
                if (File.Exists(acpexepath))
                {
                    Configuration appconfig = ConfigurationManager.OpenExeConfiguration(acpexepath);

                    if (appconfig.AppSettings.Settings["version"] != null)
                    {
                        Rtn = appconfig.AppSettings.Settings["version"].Value;
                    }
                    else
                    {
                        Rtn = "1.0.0";
                    }
                    result.Success = true;
                    result.ReturnValue = Rtn;
                }
                else
                {
                    result.Message = "Acmeerp config file is not exists";
                }
            }
            catch (Exception err)
            {
                result.Message = "Error in getting updater version " + err.Message;
            }
            return result;
        }

        private static ResultArgs GetAcmeerpInstalledPath()
        {
            ResultArgs result = new ResultArgs();
            string acmeerpInstallPath = string.Empty;

            //REnmae Old "AcMEERP" product install path key to "Acme.erp"----------------------
            //RenameProductNameInRegistry(Registry.LocalMachine);
            //RenameProductNameInRegistry(Registry.CurrentUser);
            //----------------------------------------------------------------------------------
            try
            {
                //Find installed path in registry as productname as Acme.erp
                acmeerpInstallPath = GetRegistryValue(Registry.LocalMachine, RegistryPath);
                if (string.IsNullOrEmpty(acmeerpInstallPath))
                {
                    acmeerpInstallPath = GetRegistryValue(Registry.CurrentUser, RegistryPath);
                }

                //If path does not exists in new product name (Acme.erp ), find in old product namde (AcMEERP)
                if (string.IsNullOrEmpty(acmeerpInstallPath))
                {
                    acmeerpInstallPath = GetRegistryValue(Registry.LocalMachine, RegistryPathOLD);
                    if (string.IsNullOrEmpty(acmeerpInstallPath))
                    {
                        acmeerpInstallPath = GetRegistryValue(Registry.CurrentUser, RegistryPathOLD);
                    }
                }
            }
            catch (Exception err)
            {
                result.Message = "Error in GetAcMeERPInstallPath " + err.Message;
            }
            finally
            {
                //If not avilable, fix default path
                if (string.IsNullOrEmpty(acmeerpInstallPath))
                {
                    acmeerpInstallPath = @"C:\Program Files (x86)\BoscoSoft\AcMEERP\";
                    if (!File.Exists(Path.Combine(acmeerpInstallPath, "ACPP.exe")))
                        acmeerpInstallPath = @"C:\Program Files\BoscoSoft\AcMEERP\";
                    else
                    {
                        acmeerpInstallPath = @"C:\Program Files (x86)\BoscoSoft\Acme.erp\";
                    }
                }
            }

            if (!string.IsNullOrEmpty(acmeerpInstallPath))
            {
                if (File.Exists(Path.Combine(acmeerpInstallPath, "ACPP.exe")))
                {
                    result.Success = true;
                    result.ReturnValue = acmeerpInstallPath;
                }
                else
                {
                    result.Message = "Acmeerp Installation folder is not exists";
                }
            }
            else
            {
                result.Message = "Acmeerp Installation Path in not found in the registry";
            }

            return result;
        }

        private static string GetRegistryValue(RegistryKey regkey, string registrypath)
        {
            string Rtn = string.Empty;
            RegistryKey root = regkey.OpenSubKey(registrypath, false);
            try
            {
                if (root != null)
                {
                    if (root.GetValue("AcMEERPPath") != null)
                    {
                        Rtn = root.GetValue("AcMEERPPath").ToString();
                    }
                }
            }
            catch (Exception err)
            {
                Rtn = string.Empty;
            }

            return Rtn;
        }

        private static bool CheckRegistryValueExists(RegistryKey regkey, string registrypath)
        {
            bool Rtn = false;
            RegistryKey root = regkey.OpenSubKey(registrypath, false);
            try
            {
                if (root != null)
                {
                    Rtn = true;
                }
            }
            catch (Exception err)
            {
                Rtn = false;
            }

            return Rtn;
        }

        /// <summary>
        ///  To execute the Sql Statement
        ///  Executing Insert/Update/Delete statements
        /// <param name="sSql">SQL Statement</param>
        /// </summary>
        private static ResultArgs ExecuteCommand(string sSql)
        {
            ResultArgs result = new ResultArgs();
            string dbconnection = ACMEERP_DB_CONNECTION;

            try
            {
                using (MySqlConnection sqlCnn = new MySqlConnection(dbconnection))
                {
                    using (MySqlCommand sqlCommand = new MySqlCommand(sSql, sqlCnn))
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.Connection.Open();
                        sqlCommand.ExecuteNonQuery();
                        result.Success = true;
                    }
                }
            }
            catch (Exception e)
            {
                string errormessage = e.Message;
                if (errormessage.Contains("Unable to connect to any of the specified MySQL hosts"))
                {
                    result.Message = "Could not connect Database server, Check MySQLAcperp Service is running or Check DataBase Server is reachable";
                }
                else
                {
                    result.Message = "Error in ExecuteCommand " + e.Message;
                }
            }
            return result;
        }

        /// <summary>
        ///  To fill the Data Table
        /// <param name="sSQL">SQL Statement</param>		
        /// </summary>
        private static ResultArgs ExecuteTable(string sSQL)
        {
            ResultArgs result = new ResultArgs();
            DataTable dt = new DataTable();
            string dbconnection = ACMEERP_DB_CONNECTION;


            try
            {
                using (MySqlDataAdapter dAdapter = new MySqlDataAdapter())
                {
                    using (MySqlConnection sqlCnn = new MySqlConnection(dbconnection))
                    {
                        using (MySqlCommand sqlCommand = new MySqlCommand(sSQL, sqlCnn))
                        {
                            sqlCommand.CommandType = CommandType.Text;
                            dAdapter.SelectCommand = sqlCommand;
                            dAdapter.Fill(dt);
                            result.Success = true;
                            result.DataSource.Data = dt;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string errormessage = e.Message;
                if (errormessage.Contains("Unable to connect to any of the specified MySQL hosts"))
                {
                    result.Message = "Could not connect Database server, Check MySQLAcperp Service is running or Check DataBase Server is reachable";
                }
                else
                {
                    result.Message = "Error in ExecuteCommand " + e.Message;
                }
            }
            return result;
        }

        /// <summary>
        /// This method is used to test db connection based on the input whether mysql default db or aod db
        /// </summary>
        /// <param name="IsDefaultMySQL"></param>
        /// <returns></returns>
        public static ResultArgs TestConnection()
        {
            return TestConnection(General.ACMEERP_DB_CONNECTION);
        }

        /// <summary>
        /// This method is used to test db connection based on the input whether mysql default db or aod db
        /// </summary>
        /// <param name="IsDefaultMySQL"></param>
        /// <returns></returns>
        public static ResultArgs TestConnection(string dbconnection)
        {
            ResultArgs result = new ResultArgs();
            string Rtn = string.Empty;
            string checksql = "SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS LIMIT 1";

            try
            {
                using (MySqlDataAdapter dAdapter = new MySqlDataAdapter())
                {
                    using (MySqlConnection sqlCnn = new MySqlConnection(dbconnection))
                    {
                        using (MySqlCommand sqlCommand = new MySqlCommand(checksql, sqlCnn))
                        {
                            DataTable dt = new DataTable();
                            sqlCommand.CommandType = CommandType.Text;
                            dAdapter.SelectCommand = sqlCommand;
                            dAdapter.Fill(dt);
                        }
                    }
                }
                result.ReturnValue = "Done, Connection is succeed.";
                result.Success = true;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Rtn = "Invalid username/password " + ex.Message;
                        break;
                    case 1045:
                        Rtn = "Invalid username/password " + ex.Message;
                        break;
                    case 1042:
                        //Rtn = "MySQL host is not there " + ex.Message;
                        Rtn = "Could not connect Database server, Check MySQLAcperp Service is running or Check DataBase Server is reachable";
                        break;
                    default:
                        Rtn = "Error in checking connection " + ex.Message;
                        break;
                }
                result.Message = Rtn;
            }
            catch (Exception err)
            {
                result.Message = "Error in checking connection " + err.Message;
            }
            return result;
        }


        /// <summary>
        /// This method is used to create SP (DROP FD)
        /// 
        /// 1. Execute DROPPROJECT.sql
        /// </summary>
        /// <returns></returns>
        private static ResultArgs CreateSP_DROPFD()
        {
            ResultArgs result = new ResultArgs();
            string script = string.Empty;
            try
            {
                using (Stream strmScript = currentAssembly.GetManifestResourceStream(currentAssembly.GetName().Name + ".DROPFD.sql"))
                {
                    using (StreamReader sr = new StreamReader(strmScript))
                    {
                        script = sr.ReadToEnd().ToString();
                    }
                }

                if (!string.IsNullOrEmpty(script))
                {
                    result = General.ExecuteCommand(script);
                    if (result.Success)
                    {
                        result.Success = true;
                    }
                }
            }
            catch (Exception err)
            {
                result.Message = "Errro in DROPFD.sql " + err.Message;
            }

            return result;
        }

        /// <summary>
        /// This method is used to create SP (DROP PROJECT)
        /// 
        /// 1. Execute DROPPROJECT.sql
        /// </summary>
        /// <returns></returns>
        private static ResultArgs CreateSP_DROPPROJECT()
        {
            ResultArgs result = new ResultArgs();
            string script = string.Empty;
            try
            {
                using (Stream strmScript = currentAssembly.GetManifestResourceStream(currentAssembly.GetName().Name + ".DROPPROJECT.sql"))
                {
                    using (StreamReader sr = new StreamReader(strmScript))
                    {
                        script = sr.ReadToEnd().ToString();
                    }
                }

                if (!string.IsNullOrEmpty(script))
                {
                    result = General.ExecuteCommand(script);
                    if (result.Success)
                    {
                        result.Success = true;
                    }
                }
            }
            catch (Exception err)
            {
                result.Message = "Errro in DROPPROJECT.sql " + err.Message;
            }

            return result;
        }

        /// <summary>
        /// This method is used to create SP (ResetVouchersTables)
        /// 
        /// 1. Execute ClearReset.sql
        /// </summary>
        /// <returns></returns>
        private static ResultArgs CreateSP_ClearResetVoucher()
        {
            ResultArgs result = new ResultArgs();
            string script = string.Empty;
            try
            {
                using (Stream strmScript = currentAssembly.GetManifestResourceStream(currentAssembly.GetName().Name + ".ClearReset.sql"))
                {
                    using (StreamReader sr = new StreamReader(strmScript))
                    {
                        script = sr.ReadToEnd().ToString();
                    }
                }

                if (!string.IsNullOrEmpty(script))
                {
                    result = General.ExecuteCommand(script);
                    if (result.Success)
                    {
                        result.Success = true;
                    }
                }
            }
            catch (Exception err)
            {
                result.Message = "Errro in ClearReset.sql " + err.Message;
            }

            return result;
        }

        public static ResultArgs EncryptString(string EncreptString)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                if (!string.IsNullOrEmpty(EncreptString))
                {
                    EncreptString = simpleencrypt.EncryptString(EncreptString);
                    result.ReturnValue = EncreptString;
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message + System.Environment.NewLine + ex.Source;
            }
            finally { }
            return result;
        }

        public static string DecryptString(string EncreptValue)
        {
            string decreptValue = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(EncreptValue))
                {
                    decreptValue = simpleencrypt.DecryptString(EncreptValue);
                }
            }
            catch (Exception ex)
            {
                decreptValue = string.Empty;
            }
            finally
            { }
            return decreptValue;
        }

        public static ResultArgs CreateSupportSPs()
        {
            ResultArgs result = new ResultArgs();
            result = CreateSP_DROPPROJECT();
            if (result.Success)
            {
                result = CreateSP_DROPFD();
                if (result.Success)
                {
                    result = CreateSP_ClearResetVoucher();
                }
            }
            return result;
        }

        #region Connectionstring Encryption/Decryption in Appconfig

        /// <summary>
        /// This method is used to encrypt connectionstring section in the application config
        /// </summary>
        /// <param name="configfile">config file path </param>
        /// <param name="section">Section to encrypt</param>
        public static ResultArgs EncryptConnectionSettings(string configfile, string section)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                ExeConfigurationFileMap configFile = new ExeConfigurationFileMap();
                configFile.ExeConfigFilename = configfile;
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);
                ConnectionStringsSection objconnectionstring = (ConnectionStringsSection)config.GetSection(section);
                if (!objconnectionstring.SectionInformation.IsProtected)
                {
                    objconnectionstring.SectionInformation.ProtectSection("RsaProtectedConfigurationProvider");
                    objconnectionstring.SectionInformation.ForceSave = true;
                    config.Save(ConfigurationSaveMode.Modified);
                    result.Success = true;
                }
            }
            catch (Exception err)
            {
                result.Message = "EncryptAppSettings " + err.Message;
            }
            return result;
        }

        /// <summary>
        /// This method is used to update the connectionstring value in config file
        /// </summary>
        /// <param name="keyname">name of the key</param>
        /// <param name="keyvalue">valye of the key</param>
        public static ResultArgs UpdateConnectionString(string connectionstring)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                result = TestConnection(connectionstring);
                if (result.Success)
                {
                    string configfilename = Path.Combine(ACMEERP_INSTALLED_PATH, "ACPP.exe");
                    Configuration acperpconfig = ConfigurationManager.OpenExeConfiguration(configfilename);

                    if (acperpconfig != null)
                    {
                        MySqlConnectionStringBuilder acmeerp_conn_string = new MySqlConnectionStringBuilder(connectionstring);
                        MySqlConnectionStringBuilder mysql_conn_string = new MySqlConnectionStringBuilder(connectionstring);
                        mysql_conn_string.Database = "mysql";

                        acperpconfig.ConnectionStrings.ConnectionStrings["AppConnectionString"].ConnectionString = acmeerp_conn_string.ToString();
                        acperpconfig.ConnectionStrings.ConnectionStrings["MySQLConnectionString"].ConnectionString = mysql_conn_string.ToString();

                        acperpconfig.Save(ConfigurationSaveMode.Modified);
                        ConfigurationManager.RefreshSection("AppConnectionString");
                        ConfigurationManager.RefreshSection("MySQLConnectionString");
                        result.ReturnValue = "Updated :: " + acmeerp_conn_string;
                        result.Success = true;
                    }
                }
            }
            catch (Exception err)
            {
                result.Message  = "UpdateConnectionString " + err.Message;
            }
            return result;
        }

        /// <summary>
        /// This methoid is used to clear voucher which are out of books begin
        /// </summary>
        /// <returns></returns>
        public static ResultArgs DeleteVouchersOutofBooksBegin()
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                string queryDeleteVouchersOutOfBooksBegin = "DELETE FROM FD_RENEWAL WHERE FD_INTEREST_VOUCHER_ID IN (SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS VM\n" +
                                 "WHERE VM.VOUCHER_DATE < (SELECT YEAR_FROM FROM Accounting_Year ac WHERE ac.IS_FIRST_ACCOUNTING_YEAR = 1)) OR\n" +
                                 "FD_VOUCHER_ID IN (SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS VM\n" +
                                 "WHERE VM.VOUCHER_DATE < (SELECT YEAR_FROM FROM Accounting_Year ac WHERE ac.IS_FIRST_ACCOUNTING_YEAR = 1));\n" +

                                 "DELETE FROM FD_ACCOUNT WHERE FD_VOUCHER_ID IN (SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS VM\n" +
                                  "WHERE VM.VOUCHER_DATE < (SELECT YEAR_FROM FROM Accounting_Year ac WHERE ac.IS_FIRST_ACCOUNTING_YEAR = 1));\n" +

                                 "DELETE TDSPD.*, TDSP.* FROM TDS_PAYMENT TDSP INNER JOIN TDS_PAYMENT_DETAIL TDSPD ON TDSP.TDS_PAYMENT_ID = TDSPD.TDS_PAYMENT_ID\n" +
                                    "WHERE TDSP.VOUCHER_ID IN (SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS VM\n" +
                                    "WHERE VM.VOUCHER_DATE < (SELECT YEAR_FROM FROM Accounting_Year ac WHERE ac.IS_FIRST_ACCOUNTING_YEAR = 1));\n" +

                                 "DELETE TPPD.*, TPP.* FROM TDS_PARTY_PAYMENT TPP INNER JOIN TDS_PARTY_PAYMENT_DETAIL TPPD ON TPP.PARTY_PAYMENT_ID = TPPD.PARTY_PAYMENT_ID\n" +
                                    "WHERE TPP.VOUCHER_ID IN (SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS VM\n" +
                                    "WHERE VM.VOUCHER_DATE < (SELECT YEAR_FROM FROM Accounting_Year ac WHERE ac.IS_FIRST_ACCOUNTING_YEAR = 1));\n" +

                                "DELETE TDD.*, TD.* FROM TDS_DEDUCTION TD INNER JOIN TDS_DEDUCTION_DETAIL TDD ON TD.DEDUCTION_ID = TDD.DEDUCTION_ID\n" +
                                "WHERE TD.VOUCHER_ID IN (SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS VM\n" +
                                "WHERE VM.VOUCHER_DATE < (SELECT YEAR_FROM FROM Accounting_Year ac WHERE ac.IS_FIRST_ACCOUNTING_YEAR = 1));\n" +

                                "DELETE TB.*, TBB.* FROM TDS_BOOKING TB INNER JOIN TDS_BOOKING_DETAIL TBB ON TB.BOOKING_ID = TBB.BOOKING_ID\n" +
                                "WHERE TB.VOUCHER_ID IN (SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS VM\n" +
                                "WHERE VM.VOUCHER_DATE < (SELECT YEAR_FROM FROM Accounting_Year ac WHERE ac.IS_FIRST_ACCOUNTING_YEAR = 1));\n" +

                                "DELETE FROM VOUCHER_REFERENCE\n" +
                                   "WHERE REF_VOUCHER_ID IN (SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS VM\n" +
                                   "WHERE VM.VOUCHER_DATE < (SELECT YEAR_FROM FROM Accounting_Year ac WHERE ac.IS_FIRST_ACCOUNTING_YEAR = 1)) OR\n" +
                                   "REC_PAY_VOUCHER_ID IN (SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS VM\n" +
                                   "WHERE VM.VOUCHER_DATE < (SELECT YEAR_FROM FROM Accounting_Year ac WHERE ac.IS_FIRST_ACCOUNTING_YEAR = 1));\n" +

                                "DELETE FROM PAYROLL_FINANCE WHERE DATE < (SELECT YEAR_FROM FROM Accounting_Year ac WHERE ac.IS_FIRST_ACCOUNTING_YEAR = 1);\n" +

                                "DELETE FROM VOUCHER_CC_TRANS WHERE VOUCHER_ID IN (SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS VM\n" +
                                "WHERE VOUCHER_DATE < (SELECT YEAR_FROM FROM Accounting_Year ac WHERE ac.IS_FIRST_ACCOUNTING_YEAR = 1));\n" +

                                "DELETE FROM VOUCHER_TRANS WHERE VOUCHER_ID IN (SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS VM\n" +
                                "WHERE VOUCHER_DATE < (SELECT YEAR_FROM FROM Accounting_Year ac WHERE ac.IS_FIRST_ACCOUNTING_YEAR = 1));\n" +

                                "DELETE FROM VOUCHER_MASTER_TRANS \n" +
                                "WHERE VOUCHER_DATE < (SELECT YEAR_FROM FROM Accounting_Year ac WHERE ac.IS_FIRST_ACCOUNTING_YEAR = 1);\n" +

                                "DELETE FROM LEDGER_BALANCE WHERE TRANS_FLAG <> 'OP'\n" +
                                "AND BALANCE_DATE < (SELECT YEAR_FROM FROM Accounting_Year ac WHERE ac.IS_FIRST_ACCOUNTING_YEAR = 1);";

                resultarg = ExecuteCommand(queryDeleteVouchersOutOfBooksBegin);
                if (resultarg.Success)
                {
                    resultarg.Message = "Vouchers are deleted which are out of books begin (No of Records : " + resultarg.RowsAffected.ToString() + ")";
                    resultarg.Success = true;
                }
            }
            catch (Exception err)
            {
                resultarg.Message = "Error in processing request: DeleteVouchersOutofBooksBegin " + err.Message;
            }
            return resultarg;
        }
        
        /// <summary>
        /// On 22/05/2020, 
        /// This method is used to restore Current Acmeerp Database
        /// OLD LOGIC  : Execute entire script one by one in connection object. Sometimes it takes long time/give time out problem etc
        /// NEW LOGIC  : using mysql command to restore backup file.
        /// </summary>
        /// <returns></returns>
        public static ResultArgs RestoreDBWithMySQLCommand(string RestoreDBName, string DBBackupFilePath)
        {
            ResultArgs result = new ResultArgs();
            string RestoreDBCommands = string.Empty;
            string RestoreResult = string.Empty;
            //string mysqlinstalledpath = AcmeerpMySQLServiceDetails.MYSQL_INSTALLED_PATH;
            //string mysqlinstalledpath = Path.Combine(mysqlinstalledpath, "mysql.exe");
            string mysqlinstalledpath = Path.Combine(General.ACMEERP_INSTALLED_PATH, "mysql.exe");
            string AcmeerpTmpPath = Path.Combine(General.ACMEERP_INSTALLED_PATH, @"temp\");

            if (!Directory.Exists(AcmeerpTmpPath))
            {
                Directory.CreateDirectory(AcmeerpTmpPath);
            }

            string AcmeerpTmpRestorePath = Path.Combine(General.ACMEERP_INSTALLED_PATH, @"temp\restore.sql");
            if (!string.IsNullOrEmpty(mysqlinstalledpath) && File.Exists(mysqlinstalledpath))
            {
                try
                {
                    if (File.Exists(DBBackupFilePath))
                    {
                        //Delete temp restore db if already exists
                        if (File.Exists(AcmeerpTmpRestorePath))
                        {
                            File.Delete(AcmeerpTmpRestorePath);
                        }

                        //Update Current DB NAME in backup file
                        string sql = string.Empty;
                        using (StreamReader sr = new StreamReader(DBBackupFilePath))
                        {
                            sql = sr.ReadToEnd();
                            string Header = sql.Substring(0, sql.IndexOf("Table"));
                            //Header = Header.Replace(PathDB, dbname.ToUpper());

                            int createDBStartPosition = Header.IndexOf("CREATE DATABASE");
                            int DBStartPosition = Header.IndexOf("`", createDBStartPosition);
                            int DBEndPosition = Header.IndexOf("`", DBStartPosition + 1);
                            string backupDBName = Header.Substring(DBStartPosition, (DBEndPosition - DBStartPosition) + 1);
                            Header = Header.Replace(backupDBName, "`" + RestoreDBName.ToUpper() + "`");

                            sql = sql.Replace(sql.Substring(0, sql.IndexOf("Table")), Header);
                        }

                        if (!string.IsNullOrEmpty(sql))
                        {
                            using (StreamWriter sw = new StreamWriter(AcmeerpTmpRestorePath))
                            {
                                sw.Write(sql);
                            }
                        }

                        if (File.Exists(AcmeerpTmpRestorePath))
                        {
                            if (DropDatabase(RestoreDBName))
                            {
                                //Prepare DOS command to restore current db
                                RestoreDBCommands = "\"" + mysqlinstalledpath + "\"" + " -uroot -pacperproot -P" + AcmeerpMySQLServiceDetails.MYSERVICE_PORT + " < " + "\"" + AcmeerpTmpRestorePath + "\""; ;
                                using (Process p = new Process())
                                {
                                    ProcessStartInfo psI = new ProcessStartInfo("cmd");
                                    psI.UseShellExecute = false;
                                    psI.RedirectStandardInput = true;
                                    psI.RedirectStandardOutput = true;
                                    psI.RedirectStandardError = true;
                                    psI.CreateNoWindow = true;
                                    p.StartInfo = psI;
                                    p.Start();
                                    using (StreamWriter sw = p.StandardInput)
                                    {
                                        using (StreamReader sr = p.StandardOutput)
                                        {
                                            using (StreamReader err = p.StandardError)
                                            {
                                                sw.AutoFlush = true;
                                                if (!string.IsNullOrEmpty(RestoreDBCommands))
                                                {
                                                    sw.WriteLine(RestoreDBCommands);
                                                    sw.Close();
                                                    //RestoreResult = sr.ReadToEnd();
                                                    RestoreResult = err.ReadToEnd();

                                                    //Check return error messate, sucess or not
                                                    if (RestoreResult.ToUpper().Contains("ERROR"))
                                                    {
                                                        result.Message = RestoreResult;
                                                    }
                                                    else if (RestoreResult.Trim().ToUpper() == "WARNING: USING A PASSWORD ON THE COMMAND LINE INTERFACE CAN BE INSECURE.")
                                                    {
                                                        result.Success = true; //Sucess
                                                    }
                                                    else
                                                    {
                                                        result.Message = "Unhandle exception, " + RestoreResult + " (" + RestoreDBCommands + ")";
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                result.Message = "Could not drop the database before restore";
                            }
                        }
                        else
                        {
                            result.Message = "Acmeerp Database backup is not found";
                        }
                    }
                    else
                    {
                        result.Message = "Acmeerp Database backup is not found";
                    }
                }
                catch (Exception ex)
                {
                    result.Message = "Unable to restore, " + ex.Message;
                }
                finally
                {
                    //Delete temp restore db if already exists
                    if (File.Exists(AcmeerpTmpRestorePath))
                    {
                        File.Delete(AcmeerpTmpRestorePath);
                    }
                }
            }
            else
            {
                result.Message = "Mysql Installtion Path is not found";
            }

            
            return result;
        }


        /// <summary>
        /// Drop the Database
        /// </summary>
        /// <returns></returns>
        public static bool DropDatabase(string DBname)
        {
            bool Rtn = false;
            try
            {
                string sql = "DROP DATABASE IF EXISTS `" + DBname + "`;";
                using (MySqlConnection sqlCnn = new MySqlConnection(ACMEERP_DB_CONNECTION))
                {
                    using (MySqlCommand sqlCommand = new MySqlCommand(sql, sqlCnn))
                    {
                        sqlCommand.Connection.Open();
                        sqlCommand.CommandType = CommandType.Text;
                        object ObjVal = sqlCommand.ExecuteNonQuery();
                        if (ObjVal != null)
                        {
                            Rtn = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
                Rtn = false;
            }
            return Rtn;
        }
        #endregion
    }
}
