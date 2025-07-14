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


namespace AcMEERPUpdater
{
    public static class General
    {
        private static Assembly currentAssembly = Assembly.GetExecutingAssembly();
        public static string ACMEERP_UPDATER_TITLE = "Acme.erp Updater";
        public static string AcMEERP_PRODUCT_NAME = "Acme.erp";

        public static bool IS_SILENT_INSTALLATION = true;
        public static bool ACMEERP_IS_INSTALLED = false;
        public static bool ACMEERP_IS_SQLSCRIPTAVILABLE = false;
        public static string ACMEERP_INSTALLED_PATH = string.Empty;
        public static string ACMEERP_VERSION = string.Empty;
        public static string ACMEERP_UPDATE_VERSION = string.Empty;
        public static string ACMEERP_DB_CONNECTION = string.Empty;
        public static string ACMEERP_MULTIDB_CONNECTION = string.Empty;
        public static string ACMEERP_MULTIDB_NAME = string.Empty;
        public static DataTable ACMEERP_UPDATE_FILES = null;
        public static DataTable ACMEERP_MULTI_DATABASES = null;
        public static DataTable ACMEERP_SELECTED_DATABASES = null;
        public static DataTable ACMEERP_PREV_REPORT_SETTING = null;
        private static Dictionary<string, Type> reportproperties = new Dictionary<string, Type>();
        public static string RestoreMultipleDBPath = @"C:\AcME_ERP\MultipleDB.xml";

        public static bool UpdateDBScriptSelectedBranchAlone = false;
        public static string ActiveBranchDB = string.Empty;
        public static string ActiveBranchName = string.Empty;


        private static string LOG_FILE = "acmeerpupdaterlog.log";
        private static string RegistryPath = @"Software\BoscoSoft\Acme.erp";//Acme.erp

        //for temp purpose
        private static string RegistryPathOLD = @"Software\BoscoSoft\AcMEERP";

        //To show progress while updating files
        public static event EventHandler<ProgressStatusEventArgs> OnProgress;

        public static void AssignAcMEERPProperties()
        {
            try
            {
                //IsApplictionInstalled(AcMEERP_PRODUCT_NAME);
                //ACMEERP_INSTALLED_PATH = @"C:\Program Files\BoscoSoft\AcMEERP";

                ACMEERP_INSTALLED_PATH = GetAcMeERPInstallPath();
                if (Directory.Exists(ACMEERP_INSTALLED_PATH))
                {
                    //ACMEERP_VERSION = "1.0.0";
                    ACMEERP_VERSION = CurrentVersion();
                    ACMEERP_UPDATE_VERSION = GetUpdaterVersion();
                    ACMEERP_UPDATE_FILES = GetUpdateFiles();
                    ACMEERP_DB_CONNECTION = GetDBconnectionstring();

                    if (!string.IsNullOrEmpty(ACMEERP_UPDATE_VERSION) && !string.IsNullOrEmpty(ACMEERP_DB_CONNECTION))
                    {
                        ACMEERP_IS_INSTALLED = true;

                        //On 05/11/2024, To current report setting
                        string reportsettingxml = Path.Combine(ACMEERP_INSTALLED_PATH, "ReportSetting.xml");
                        if (File.Exists(reportsettingxml))
                        {
                            DataSet ds = new DataSet();
                            ds.ReadXml(reportsettingxml);
                            if (ds.Tables.Count > 0 && ds.Tables.Contains("ReportSetting"))
                            {
                                ACMEERP_PREV_REPORT_SETTING = ds.Tables["ReportSetting"].DefaultView.ToTable();
                            }
                        }
                    }
                }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, ACMEERP_UPDATER_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string FormatResourceName(Assembly assembly, string resourceName)
        {
            return assembly.GetName().Name + "." + resourceName.Replace(" ", "_")
                                                               .Replace("\\", ".")
                                                               .Replace("/", ".");
        }

        public static bool UpdateFiles()
        {
            bool Rtn = false;
            bool isallfilesupdated = false;
            try
            {
                DataView dvTargetPath = new DataView();
                using (var stream = currentAssembly.GetManifestResourceStream("AcMEERPUpdater.TargetPath.xml"))
                {
                    DataSet dsTargetPath = new DataSet();
                    dsTargetPath.ReadXml(stream);
                    if (dsTargetPath.Tables.Count > 0)
                    {
                        dvTargetPath = dsTargetPath.Tables["updatefile"].DefaultView;
                    }
                }

                var arrResources = currentAssembly.GetManifestResourceNames().Where(n => n.Contains(".updatefiles."));

                foreach (string resourcename in arrResources)
                {
                    using (Stream S = currentAssembly.GetManifestResourceStream(resourcename))
                    {
                        RaiseProgressEvent("Updating Acme.erp files...", General.ACMEERP_UPDATE_FILES.Rows.Count);

                        string filename = resourcename.Replace(currentAssembly.GetName().Name + ".updatefiles.", "");
                        //If target path fixed in xml file, filename taken from from that
                        dvTargetPath.RowFilter = string.Empty;
                        dvTargetPath.RowFilter = "file='" + filename + "'";
                        if (dvTargetPath.Count >= 1)
                        {
                            string path = dvTargetPath[0]["path"].ToString().Trim();
                            string actualapth = dvTargetPath[0]["actualfile"].ToString().Trim();
                            if (path != string.Empty)
                            {
                                //filename = Path.Combine(path, filename);
                                filename = Path.Combine(path, actualapth);
                                //  MessageBox.Show(filename);
                            }
                        }

                        string filepath = Path.Combine(ACMEERP_INSTALLED_PATH, filename);


                        if (TakeBackupAcMERPFiles(filepath))
                        {

                            byte[] rawFile = new byte[S.Length];

                            //Read the data from the assembly
                            S.Read(rawFile, 0, (int)S.Length);

                            //Save the data to the hard drive
                            if (File.Exists(filepath))
                            {
                                File.SetAttributes(filepath, FileAttributes.Normal);
                            }
                            using (FileStream fs = new FileStream(filepath, FileMode.Create))
                            {
                                fs.Write(rawFile, 0, (int)S.Length);
                            }
                            isallfilesupdated = true;
                        }
                        else
                        {
                            MessageBox.Show("Could not take backup. " + filename, ACMEERP_UPDATER_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            isallfilesupdated = false;
                            break;
                        }
                    }
                }

                //If there are no files in update files, return true by default
                if (arrResources.Count() == 0)
                {
                    isallfilesupdated = true;
                }
            }
            catch (Exception err)
            {
                if ((err.Message.Contains("denied")))
                    MessageBox.Show("You have no rights. Run as Administrator", ACMEERP_UPDATER_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                WriteLog("Error in UpdateFiles " + err.Message);
                isallfilesupdated = false;
            }


            Rtn = isallfilesupdated;
            return Rtn;
        }
        public static void WriteLog(string msg)
        {
            try
            {
                string logpath = LOG_FILE;
                System.IO.StreamWriter sw = new System.IO.StreamWriter(logpath, true);

                if (msg.Replace("-", "").Length > 0)
                {
                    msg = (DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt")) + " || " + msg;
                }
                sw.WriteLine(msg);
                sw.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in writing acperp installer log " + ex.Message);
            }
        }
        public static void ClearLog()
        {
            try
            {

                string logpath = LOG_FILE;
                if (System.IO.File.Exists(logpath))
                {
                    using (var fs = new FileStream(logpath, FileMode.Truncate))
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in writing acperp installer log " + ex.Message);
            }
        }

        /// <summary>
        /// This method is used to verify invlaid tables and delete invalid entreis
        /// 
        /// 1. Execute VerifyInvalidTables.sql
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool VerifyInvalidTables()
        {
            bool rtn = false;
            string script = string.Empty;

            //Read acperp empty db update script file
            try
            {
                using (Stream strmScript = currentAssembly.GetManifestResourceStream(currentAssembly.GetName().Name + ".dbscript.VerifyInvalidTables.sql"))
                {
                    using (StreamReader sr = new StreamReader(strmScript))
                    {
                        script = sr.ReadToEnd().ToString();
                        General.WriteLog("End:Reading acperp db VerifyInvalidTables scripts");
                    }
                }

                if (!string.IsNullOrEmpty(script))
                {
                    if (General.ExecuteCommand(script))
                    {
                        rtn = true;
                    }
                }
            }
            catch (Exception err)
            {
                General.WriteLog("Errro in VerifyInvalidTables.sql " + err.Message);
            }

            return rtn;
        }

        /// <summary>
        /// This method is used to verify invlaid FD vouchers in FD tables and Voucher Transactions
        /// 
        /// 1. Execute VerifyInvalidFDVouchers.sql
        /// </summary>
        /// <returns></returns>
        public static bool VerifyInvalidFDVouchers()
        {
            bool rtn = false;
            string script = string.Empty;

            //Read acperp empty db update script file
            try
            {
                using (Stream strmScript = currentAssembly.GetManifestResourceStream(currentAssembly.GetName().Name + ".dbscript.VerifyInvalidFDVouchers.sql"))
                {
                    using (StreamReader sr = new StreamReader(strmScript))
                    {
                        script = sr.ReadToEnd().ToString();
                        General.WriteLog("End:Reading acperp db VerifyInvalidFDVouchers scripts");
                    }
                }

                if (!string.IsNullOrEmpty(script))
                {
                    if (General.ExecuteCommand(script))
                    {
                        rtn = true;
                    }
                }
            }
            catch (Exception err)
            {
                General.WriteLog("Errro in VerifyInvalidFDVouchers.sql " + err.Message);
            }

            return rtn;
        }


        /// <summary>
        /// This method is used to create DROP FD PROCEDURE (this SP will drop given FD and its Contra, Interest, renewal)
        /// 
        /// 1. Execute DROPFD.sql
        /// </summary>
        /// <returns></returns>
        public static bool CreateDROPFDSP()
        {
            bool rtn = false;
            string script = string.Empty;

            //Read acperp empty db update script file
            try
            {
                using (Stream strmScript = currentAssembly.GetManifestResourceStream(currentAssembly.GetName().Name + ".dbscript.DROPFD.sql"))
                {
                    using (StreamReader sr = new StreamReader(strmScript))
                    {
                        script = sr.ReadToEnd().ToString();
                        General.WriteLog("End:Reading acperp db DROPFD scripts");
                    }
                }

                if (!string.IsNullOrEmpty(script))
                {
                    if (General.ExecuteCommand(script))
                    {
                        rtn = true;
                    }
                }
            }
            catch (Exception err)
            {
                General.WriteLog("Errro in DROPFD.sql " + err.Message);
            }

            return rtn;
        }
        private static void RaiseProgressEvent(string Message, int NoOfUpdates)
        {
            if (OnProgress != null)
            {
                ProgressStatusEventArgs args = new ProgressStatusEventArgs();
                args.Status = Message;
                args.NoofUpdates = NoOfUpdates;
                Application.DoEvents();
                OnProgress(null, args);
            }
        }

        public static int GetDbChangesLength()
        {
            Int32 scriptCount = 0;
            try
            {
                General.WriteLog("Start:Getting DB Changes Length");
                using (Stream strmScript = currentAssembly.GetManifestResourceStream(currentAssembly.GetName().Name + ".dbscript.dbscript.sql"))
                {
                    using (StreamReader sr = new StreamReader(strmScript))
                    {
                        scriptCount = sr.ReadToEnd().Split(';').Length;
                        General.WriteLog("End:Getting DB Changes Length");
                    }
                }
            }
            catch (Exception err)
            {
                General.WriteLog("Errro in UpdateAcMEERPDBchanges " + err.Message);
            }
            return scriptCount;
        }

        public static bool UpdateAcMEERPDBchanges()
        {
            bool rtn = false;
            string sql = string.Empty;
            string[] query;

            //Read acperp empty db update script file
            try
            {
                //verify invalid tables and vouchers
                VerifyInvalidTables();

                using (Stream strmScript = currentAssembly.GetManifestResourceStream(currentAssembly.GetName().Name + ".dbscript.dbscript.sql"))
                {
                    using (StreamReader sr = new StreamReader(strmScript))
                    {
                        query = sr.ReadToEnd().Split(';');
                        General.WriteLog("End:Reading acperp db update scripts");
                    }
                }

                if (query.Length > 0)
                {
                    //MessageBox.Show(ACMEERP_MULTIDB_NAME + " - " + General.ACMEERP_MULTIDB_CONNECTION);

                    for (int i = 0; i < query.Length; i++)
                    {
                        RaiseProgressEvent(string.IsNullOrEmpty(General.ACMEERP_MULTIDB_CONNECTION) ? "Updating Database Changes..." :
                            "Updating Branch (" + ACMEERP_MULTIDB_NAME + ") Database Changes...", query.Length);
                        try
                        {
                            sql = query[i];
                            if (query[i].ToString() == "442")
                            {
                                string a = query[i].ToString();
                            }
                            if (General.ExecuteCommand(sql))
                            {
                                rtn = true;
                            }
                        }
                        catch (Exception err)
                        {
                            //rtn = false;
                            General.WriteLog("Errro in UpdateAcMEERPDBchanges " + err.Message);
                        }

                    }
                }
            }
            catch (Exception err)
            {
                General.WriteLog("Errro in UpdateAcMEERPDBchanges " + err.Message);
            }


            //verify invalid FD vouchers
            VerifyInvalidFDVouchers();

            //On 04/10/2017, To create MySQL procudure DROPFD in Acme.erp db
            CreateDROPFDSP();

            //On 13/06/2017, Update Product version into Acmeerp.db
            UpdateVersion();


            UpdateCashBankStatusTransactions();

            //On 29/08/2018, update tds entry in FD interest voucher
            UpdateFDInterestVoucherForTDSAmount();

            //On 03/09/2020,update mismathced CRs and DRs Vouchers
            UpdateMismatchingCRsDRsVouchers();

            //On 05/01/2023, To update GST Invoice details separate tables GST Invoice Yable Master and GST Invocie Ledger Details 
            UpdateGSTInvoiceMaster();

            return rtn;
        }

        /// <summary>
        /// To update DBChanges in Multiple Database
        /// </summary>
        /// <returns></returns>
        //public static bool UpdateAcMEERPDBchanges(string DBconnection)
        //{
        //    bool rtn = false;
        //    string sql = string.Empty;
        //    string[] query;

        //    //Read acperp empty db update script file
        //    try
        //    {
        //        VerifyInvalidTables();
        //        using (Stream strmScript = currentAssembly.GetManifestResourceStream(currentAssembly.GetName().Name + ".dbscript.dbscript.sql"))
        //        {
        //            using (StreamReader sr = new StreamReader(strmScript))
        //            {
        //                query = sr.ReadToEnd().Split(';');
        //                General.WriteLog("End:Reading acperp db update scripts");
        //            }
        //        }

        //        for (int i = 0; i < query.Length; i++)
        //        {
        //            try
        //            {
        //                sql = query[i];
        //                if (General.ExecuteCommand(sql, DBconnection))
        //                {
        //                    rtn = true;
        //                }
        //            }
        //            catch (Exception err)
        //            {
        //                //rtn = false;
        //                General.WriteLog("Errro in UpdateAcMEERPDBchanges " + err.Message);
        //            }

        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        General.WriteLog("Errro in UpdateAcMEERPDBchanges " + err.Message);
        //    }

        //    return rtn;
        //}

        public static bool IsAcMEERPRunning()
        {
            bool Rtn = true;
            try
            {

                Process[] processByName = Process.GetProcessesByName("acpp");
                Rtn = (processByName.Length > 0);
            }
            catch (Exception err)
            {
                //MessageBox.Show(err.Message, ACMEERP_UPDATER_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                WriteLog("Error in getting updater GetSQLscript " + err.Message);
            }
            return Rtn;
        }

        private static string GetSQLscript()
        {
            string Rtn = string.Empty;
            try
            {
                using (Stream strmScript = currentAssembly.GetManifestResourceStream(currentAssembly.GetName().Name + ".dbscript.dbscript.sql"))
                {
                    using (StreamReader sr = new StreamReader(strmScript))
                    {
                        Rtn = sr.ReadToEnd();
                    }
                }

            }
            catch (Exception err)
            {
                //MessageBox.Show(err.Message, ACMEERP_UPDATER_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                WriteLog("Error in getting updater GetSQLscript " + err.Message);
            }

            return Rtn;
        }

        /// <summary>
        /// This method is used to test db connection based on the input whether mysql default db or acp erp db
        /// </summary>
        /// <param name="IsDefaultMySQL"></param>
        /// <returns></returns>
        public static bool TestConnection()
        {
            bool Rtn = false;
            string dbconnection = General.ACMEERP_DB_CONNECTION;
            string checksql = "SELECT COUNTRY FROM MASTER_COUNTRY";

            string errormsg = string.Empty;
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
                Rtn = true;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server. contact support team", General.ACMEERP_UPDATER_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 1045:
                        //if (!IsDefaultMySQL)
                        //    MessageBox.Show("Invalid username/password, please try again, " + dbconnection, General.ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 1042:
                        MessageBox.Show(ex.Message, General.ACMEERP_UPDATER_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        General.WriteLog("MySQL host is not there " + ex.Message);
                        break;
                    default:
                        //MessageBox.Show(ex.Message, General.ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        General.WriteLog("Error in checking connection " + ex.Message);
                        break;
                }
            }
            return Rtn;
        }

        private static string GetUpdaterVersion()
        {
            string Rtn = string.Empty;
            try
            {
                using (Stream strmScript = currentAssembly.GetManifestResourceStream(currentAssembly.GetName().Name + ".dbscript.version.txt"))
                {
                    using (StreamReader sr = new StreamReader(strmScript))
                    {
                        Rtn = sr.ReadToEnd();
                    }
                }

            }
            catch (Exception err)
            {
                //MessageBox.Show(err.Message, ACMEERP_UPDATER_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                WriteLog("Error in getting updater version" + err.Message);
            }

            return Rtn;
        }

        private static string CurrentVersion()
        {
            string Rtn = string.Empty;
            try
            {
                string acpexepath = Path.Combine(General.ACMEERP_INSTALLED_PATH, "ACPP.exe");
                Configuration appconfig = ConfigurationManager.OpenExeConfiguration(acpexepath);

                if (appconfig.AppSettings.Settings["version"] != null)
                {
                    Rtn = appconfig.AppSettings.Settings["version"].Value;
                }
                else
                {
                    Rtn = "1.0.0";
                }
            }
            catch (Exception err)
            {
                //MessageBox.Show(err.Message, ACMEERP_UPDATER_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                WriteLog("Error in getting updater version" + err.Message);
            }

            return Rtn;
        }

        private static string GetDBconnectionstring()
        {
            string Rtn = string.Empty;
            try
            {
                if (Directory.Exists(ACMEERP_INSTALLED_PATH))
                {
                    string acppexepath = Path.Combine(ACMEERP_INSTALLED_PATH, "ACPP.exe");
                    Configuration acperpconfig = ConfigurationManager.OpenExeConfiguration(acppexepath);
                    if (acperpconfig != null)
                    {
                        Rtn = acperpconfig.ConnectionStrings.ConnectionStrings["AppConnectionString"].ToString();
                    }
                }

            }
            catch (Exception err)
            {
                //MessageBox.Show(err.Message, ACMEERP_UPDATER_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                WriteLog("Error in getting acperp db connectionstring " + err.Message);
            }

            return Rtn;
        }


        private static DataTable GetUpdateFiles()
        {
            DataTable dtUpdates = new DataTable();
            dtUpdates.Columns.Add("SNo", typeof(System.Int32));
            dtUpdates.Columns.Add("File", typeof(System.String));
            dtUpdates.Columns.Add("Version", typeof(System.String));
            dtUpdates.Columns.Add("NewVersion", typeof(System.String));

            try
            {
                var arrResources = currentAssembly.GetManifestResourceNames().Where(n => n.Contains(".updatefiles."));
                foreach (string resourceName in arrResources)
                {
                    string filename = resourceName.Replace(currentAssembly.GetName().Name + ".updatefiles.", "");
                    DataRow dr = dtUpdates.NewRow();
                    dr["SNo"] = dtUpdates.Rows.Count + 1;
                    dr["File"] = filename;
                    dr["Version"] = "1.6.0";
                    dr["NewVersion"] = "1.7.0";
                    dtUpdates.Rows.Add(dr);
                }

                //Check db script avilable or not
                string dbscript = GetSQLscript();
                ACMEERP_IS_SQLSCRIPTAVILABLE = false;
                if (!string.IsNullOrEmpty(dbscript))
                {
                    DataRow dr = dtUpdates.NewRow();
                    dr["SNo"] = dtUpdates.Rows.Count + 1;
                    dr["File"] = "SQL Script";
                    dr["Version"] = "1.6.0";
                    dr["NewVersion"] = "1.7.0";
                    ACMEERP_IS_SQLSCRIPTAVILABLE = true;
                    dtUpdates.Rows.Add(dr);
                }

            }
            catch (Exception err)
            {
                //MessageBox.Show(err.Message, ACMEERP_UPDATER_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                WriteLog("Error in getting update files list " + err.Message);
            }
            return dtUpdates;
        }


        /// <summary>
        ///  To execute the Sql Statement
        ///  Executing Insert/Update/Delete statements
        /// <param name="sSql">SQL Statement</param>
        /// </summary>
        private static bool ExecuteCommand(string sSql)
        {
            bool Rtn = false;
            string dbconnection = (General.ACMEERP_MULTIDB_CONNECTION == string.Empty) ? General.ACMEERP_DB_CONNECTION : General.ACMEERP_MULTIDB_CONNECTION;

            try
            {
                using (MySqlConnection sqlCnn = new MySqlConnection(dbconnection))
                {
                    using (MySqlCommand sqlCommand = new MySqlCommand(sSql, sqlCnn))
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.Connection.Open();
                        sqlCommand.ExecuteNonQuery();
                        Rtn = true;
                    }
                }
            }
            catch (Exception e)
            {
                General.WriteLog("Error in executeing command " + e.Message);
                Rtn = false;
            }
            return Rtn;
        }

        /// <summary>
        ///  To execute the Sql Statement
        ///  Executing Insert/Update/Delete statements For multiple db
        /// <param name="sSql">SQL Statement</param>
        /// </summary>
        private static bool ExecuteCommand(string sSql, string DatabaseConnection)
        {
            bool Rtn = false;
            string dbconnection = DatabaseConnection;

            try
            {
                using (MySqlConnection sqlCnn = new MySqlConnection(dbconnection))
                {
                    using (MySqlCommand sqlCommand = new MySqlCommand(sSql, sqlCnn))
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.Connection.Open();
                        sqlCommand.ExecuteNonQuery();
                        Rtn = true;
                    }
                }
            }
            catch (Exception e)
            {
                General.WriteLog("Error in executeing command " + e.Message);
                Rtn = false;
            }
            return Rtn;
        }

        /// <summary>
        /// This method is used to get data from database for given db connection string
        /// </summary>
        /// <param name="dbconnection"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        private static DataTable FetchData(string sql)
        {
            DataTable dtData = new DataTable();
            string dbconnection = (General.ACMEERP_MULTIDB_CONNECTION == string.Empty) ? General.ACMEERP_DB_CONNECTION : General.ACMEERP_MULTIDB_CONNECTION;

            try
            {
                using (MySqlDataAdapter dAdapter = new MySqlDataAdapter())
                {
                    using (MySqlConnection sqlCnn = new MySqlConnection(dbconnection))
                    {
                        using (MySqlCommand sqlCommand = new MySqlCommand(sql, sqlCnn))
                        {
                            sqlCommand.CommandType = CommandType.Text;
                            dAdapter.SelectCommand = sqlCommand;
                            dAdapter.Fill(dtData);
                        }
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                General.WriteLog("Error in fetching data from Database" + ex.Message);
            }
            return dtData;
        }

        //public object GetDynamicInstance(string instanceType, object[] args)
        //{
        //    Type type = Type.GetType(instanceType, false, true);
        //    object instance = null;

        //    if (type != null)
        //    {
        //        try
        //        {
        //            if (args != null)
        //            {
        //                instance = System.Activator.CreateInstance(type, args);
        //            }
        //            else
        //            {
        //                instance = System.Activator.CreateInstance(type);
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            General.WriteLog("Error in GetDynamicInstance" + e.Message);
        //        }
        //    }

        //    return instance;
        //}

        /// <summary>
        //If old Acme.erp setup already installed in client system, the registry folder of Acme.erp is "AcMEERP"
        //so when we run updater first time, it will look "AcMEERP" under BoscoSoft
        //If "AcMEERP" exists under "BoscoSoft", Rename it as "Acme.erp"
        /// </summary>
        private static void RenameProductNameInRegistry(RegistryKey regkey)
        {
            try
            {
                if (CheckRegistryValueExists(regkey, RegistryPathOLD)) //Check AcMEERP key exists in registry
                {
                    string acmeerpinstallpath = GetRegistryValue(regkey, RegistryPathOLD);
                    if (!CheckRegistryValueExists(regkey, RegistryPath)) //Check Acme.erp key exists in registry
                    {
                        //If Acme.erp is not there, create it with old path value

                        RegistryKey key = regkey.CreateSubKey(RegistryPath);
                        key.SetValue("AcMEERPPath", acmeerpinstallpath);
                        key.Close();
                    }
                    regkey.DeleteSubKey(RegistryPathOLD);   //Delete key "AcMEERP"
                }
            }
            catch (Exception err)
            {
                WriteLog("Error in RenameProductNameInRegistry " + err.Message);
            }
        }

        private static string GetAcMeERPInstallPath()
        {
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
                WriteLog("Error in GetAcMeERPInstallPath " + err.Message);
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
            return acmeerpInstallPath;
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
                WriteLog("Error in GetRegistryValue " + err.Message);
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
                WriteLog("Error in CheckRegistryValueExists " + err.Message);
            }

            return Rtn;
        }

        public static bool TakeBackupAcMERPFiles(string filename)
        {
            bool Rtn = false;
            string bkpfilepath = string.Empty;
            try
            {
                if (File.Exists(filename))
                {
                    string bkpfolder = General.ACMEERP_INSTALLED_PATH;// Directory.GetParent(General.ACMEERP_INSTALLED_PATH).Parent.FullName;
                    bkpfolder = Path.Combine(bkpfolder, "Acme.erpBackupDlls");

                    //Backup folder
                    DirectoryInfo dirbackup = new DirectoryInfo(bkpfolder);
                    if (!dirbackup.Exists)
                    {
                        dirbackup.Create();
                    }

                    //check with file is with in folder of acmeerp for eg resource files with in en-us folder
                    if (Directory.GetParent(filename).Name != Directory.GetParent(General.ACMEERP_INSTALLED_PATH).Name)
                    {
                        string folder = Directory.GetParent(filename).Name;
                        dirbackup = new DirectoryInfo(Path.Combine(bkpfolder, folder));
                        if (!dirbackup.Exists)
                        {
                            dirbackup.Create();
                        }
                        bkpfilepath = Path.Combine(dirbackup.FullName, Path.GetFileName(filename));
                    }
                    else
                    {
                        bkpfilepath = Path.Combine(bkpfolder, Path.GetFileName(filename));
                    }

                    //Take bakup
                    if (File.Exists(bkpfilepath))
                    {
                        File.SetAttributes(bkpfilepath, FileAttributes.Normal);
                    }
                    File.Copy(filename, bkpfilepath, true);
                }
                Rtn = true;
            }
            catch (Exception err)
            {
                WriteLog("Error in taking backup " + err.Message);
            }

            return Rtn;
        }

        /// <summary>
        /// This method is used to update current product version into acmeerp database
        /// master_setting : SETTING_NAME : ProductVersion, USER_ID = 1
        /// </summary>
        /// <returns></returns>
        public static bool UpdateVersion()
        {
            bool Rtn = false;
            try
            {
                Int32 userid = 1; //Admin User
                string productversion = Assembly.GetEntryAssembly().GetName().Version.ToString();
                string SQL = "INSERT INTO MASTER_SETTING (SETTING_NAME, VALUE, USER_ID ) VALUES " +
                             " ('ProductVersion','" + productversion + "', " + userid + ") ON DUPLICATE KEY UPDATE VALUE ='" + productversion + "'";

                Rtn = ExecuteCommand(SQL);
            }
            catch (Exception err)
            {
                WriteLog("Error in updating product version " + err.Message);
            }

            return Rtn;
        }

        /// <summary>
        /// On 29/08/2018, Update FD interest voucher for TDS for FD Interest amount (for old clients those who have already have TDS amount)
        /// 
        ///old: We just keep TDS amont for FD interest amount in FD module alone, it will not affect vouchers
        /// New we have provided featuer to make vouchers in FD interest voucher for TDS too
        /// 
        /// This method will correct existing clients to create TDS entry into FD interest voucher
        /// 
        /// </summary>
        public static void UpdateFDInterestVoucherForTDSAmount()
        {
            Int32 TDSonFDInterestLedger = 0;
            Int32 FD_INTEREST_VOUCHER_ID = 0;
            double INTEREST_AMOUNT = 0;
            double TDS_AMOUNT = 0;
            bool check = false;


            //1. Check TDS on FD Interest is avialbe
            DataTable dtTDSLedger = FetchData("SELECT LEDGER_ID FROM MASTER_LEDGER WHERE LEDGER_NAME = 'TDS on FD Interest'");
            if (dtTDSLedger.Rows.Count == 1)
            {
                check = Int32.TryParse(dtTDSLedger.Rows[0]["LEDGER_ID"].ToString(), out TDSonFDInterestLedger);

                if (TDSonFDInterestLedger > 0)
                {
                    //2.Get list of Renewals which are having TDS Amount
                    DataTable dtRenewalsWithTDS = FetchData("SELECT FD_INTEREST_VOUCHER_ID, INTEREST_AMOUNT, TDS_AMOUNT FROM FD_RENEWAL WHERE TDS_AMOUNT > 0");
                    foreach (DataRow drFDRenewal in dtRenewalsWithTDS.Rows)
                    {
                        check = Int32.TryParse(drFDRenewal["FD_INTEREST_VOUCHER_ID"].ToString(), out FD_INTEREST_VOUCHER_ID);
                        check = double.TryParse(drFDRenewal["INTEREST_AMOUNT"].ToString(), out INTEREST_AMOUNT);
                        check = double.TryParse(drFDRenewal["TDS_AMOUNT"].ToString(), out TDS_AMOUNT);

                        //3. Get FD interest voucher 
                        string sql = "SELECT VT.*, LG.GROUP_ID, ML.IS_BANK_INTEREST_LEDGER FROM VOUCHER_TRANS VT " +
                                        "INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID " +
                                        "INNER JOIN MASTER_LEDGER_GROUP LG ON LG.GROUP_ID = ML.GROUP_ID " +
                                        "WHERE VT.VOUCHER_ID = " + FD_INTEREST_VOUCHER_ID;
                        DataTable dtInterestVoucher = FetchData(sql);
                        if (dtInterestVoucher.Rows.Count > 0)
                        {
                            //4. Check already FD voucher linked with TDS on FD interert leder, if so sskip voucher
                            dtInterestVoucher.DefaultView.RowFilter = dtInterestVoucher.DefaultView.Sort = string.Empty;
                            dtInterestVoucher.DefaultView.RowFilter = "LEDGER_ID = " + TDSonFDInterestLedger.ToString();
                            dtInterestVoucher.DefaultView.Sort = "SEQUENCE_NO";
                            if (dtInterestVoucher.DefaultView.Count == 0 && INTEREST_AMOUNT > 0 && TDS_AMOUNT > 0)
                            {
                                //4. Add TDS amount to FD Bank Interest
                                sql = "UPDATE VOUCHER_TRANS SET AMOUNT = " + (INTEREST_AMOUNT + TDS_AMOUNT) +
                                        " WHERE VOUCHER_ID = " + FD_INTEREST_VOUCHER_ID + " AND SEQUENCE_NO = 1";
                                ExecuteCommand(sql);

                                //5. Update DR ledger (Bank or FD ledger) as SEQUENCE NUMBER is 3 and real Interest amount
                                sql = "UPDATE VOUCHER_TRANS SET SEQUENCE_NO = 3, AMOUNT = " + INTEREST_AMOUNT +
                                        " WHERE VOUCHER_ID = " + FD_INTEREST_VOUCHER_ID + " AND SEQUENCE_NO = 2";
                                ExecuteCommand(sql);

                                //7. add one more CR ledger (TDS on FD interest Ledger) as SEQUENCE NUMBER is 2 and TDS Amount
                                sql = "INSERT INTO VOUCHER_TRANS (VOUCHER_ID, SEQUENCE_NO, LEDGER_ID, AMOUNT, TRANS_MODE)" +
                                        " VALUES (" + FD_INTEREST_VOUCHER_ID + ",2," + TDSonFDInterestLedger + "," + TDS_AMOUNT + ",'DR')";
                                ExecuteCommand(sql);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This is to update the Cash and Bank Transactions, by using Journal Cash\Bank Transactions
        /// </summary>
        /// 

        public static void UpdateCashBankStatusTransactions()
        {
            try
            {
                string columnCheckQuery = string.Format(@"
                                            SELECT COUNT(*) AS COLUMNEXISTS 
                                            FROM INFORMATION_SCHEMA.COLUMNS 
                                            WHERE TABLE_SCHEMA = '{0}' 
                                            AND TABLE_NAME = 'VOUCHER_MASTER_TRANS' 
                                            AND COLUMN_NAME = 'IS_CASH_BANK_STATUS';", ACMEERP_MULTIDB_NAME);

                DataTable dtSource = FetchData(columnCheckQuery);

                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    int statusId = Convert.ToInt32(dtSource.Rows[0]["COLUMNEXISTS"]);

                    if (statusId == 0)
                    {
                        string alterTableQuery = @"
                    ALTER TABLE VOUCHER_MASTER_TRANS 
                    ADD COLUMN IS_CASH_BANK_STATUS INTEGER UNSIGNED NOT NULL DEFAULT 1 
                    COMMENT 'Default 1, Journal 0 if No Cash/Bank Involved' 
                    AFTER IS_MULTI_CURRENCY;";

                        ExecuteCommand(alterTableQuery);

                        string updateQuery = "UPDATE VOUCHER_MASTER_TRANS SET IS_CASH_BANK_STATUS = 0 WHERE VOUCHER_TYPE = 'JN';";
                        ExecuteCommand(updateQuery);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating Cash Bank Status Transactions: " + ex.Message);
            }
        }


        //    public static void UpdateCashBankStatusTransactions()
        //    {
        //            DataTable dtSource = FetchData("SELECT COUNT(*) AS COLUMNEXISTS FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '"
        //+ ActiveBranchDB + "' AND TABLE_NAME = 'VOUCHER_MASTER_TRANS' AND COLUMN_NAME = 'IS_CASH_BANK_STATUS';");
        //        // columns is exists 1 not exist 0

        //        if (dtSource.Rows.Count == 1)
        //        {
        //            int StatusId = Convert.ToInt32(dtSource.Rows[0]["COLUMNEXISTS"].ToString());

        //            if (StatusId == 0)
        //            {
        //                string altersql = "ALTER TABLE `voucher_master_trans` ADD COLUMN `IS_CASH_BANK_STATUS` INTEGER UNSIGNED NOT NULL DEFAULT 1\n" +
        //                                   "COMMENT 'Default 1, Journal 0 if No Cash\Bank Involved' AFTER `IS_MULTI_CURRENCY`";
        //                ExecuteCommand(altersql);

        //                string updatesql = "UPDATE VOUCHER_MASTER_TRANS SET IS_CASH_BANK_STATUS=0 WHERE VOUCHER_TYPE ='JN'";
        //            ExecuteCommand(updatesql);
        //            }
        //        }
        //    }


        /// <summary>
        /// 
        /// On 03/09/2020, In Few Branch DBs Sum of DRs and Sum of CRs are mismatching, So we find and correct those Vouchers in the following logic
        /// It should not affect/modify Bank Ledgers as it was already affected and updated balance.
        /// 
        /// 1. For Receipts/Payments
        /// -  Calculate difference amount and update Cash/Bank Ledger's against General Ledger's top first Ledger amount
        /// 
        /// 2. For Contra Deposit
        /// -  Calculate difference amount and update Bank Ledger's against Cash Ledger's top first Ledger amount
        /// 
        /// 4. For Contra Withdrawal 
        /// - Calculate difference amount and update Bank Ledger's against Cash Ledger's top first Ledger amount
        /// 
        /// 5. Jounral
        /// - Nill
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool UpdateMismatchingCRsDRsVouchers()
        {
            bool rtn = false;

            try
            {
                string sSQL = "SELECT VT.VOUCHER_ID, SUM(IF(VT.TRANS_MODE='CR', VT.AMOUNT,0)) CR, SUM(IF(VT.TRANS_MODE='DR', VT.AMOUNT,0)) DR,\n" +
                            "SUM(IF(TRANS_MODE='CR', AMOUNT,0)) - SUM(IF(TRANS_MODE='DR', AMOUNT,0)) As Diff\n" +
                            "FROM VOUCHER_TRANS VT\n" +
                            "INNER JOIN VOUCHER_MASTER_TRANS VM ON VM.VOUCHER_ID = VT.VOUCHER_ID AND VM.STATUS =1\n" +
                            "WHERE VM.STATUS = 1 AND IF(VM.IS_MULTI_CURRENCY=1, VM.VOUCHER_TYPE NOT IN ('CN'), 1=1 ) GROUP BY VT.VOUCHER_ID\n" +
                            "HAVING (SUM(IF(VT.TRANS_MODE='CR', VT.AMOUNT,0)) - SUM(IF(VT.TRANS_MODE='DR', VT.AMOUNT,0))) <> 0";
                DataTable dtMisMatchingVouchers = FetchData(sSQL);
                if (dtMisMatchingVouchers != null && dtMisMatchingVouchers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtMisMatchingVouchers.Rows)
                    {
                        Int32 voucherid = 0;
                        double diffAmount = 0;
                        bool check = double.TryParse(dr["Diff"].ToString(), out diffAmount);
                        check = Int32.TryParse(dr["VOUCHER_ID"].ToString(), out voucherid);

                        if (voucherid > 0 && diffAmount != 0)
                        {
                            sSQL = "SELECT ML.GROUP_ID, VM.VOUCHER_TYPE, VT.SEQUENCE_NO, TRANS_MODE\n" +
                                    "FROM VOUCHER_MASTER_TRANS VM\n" +
                                    "INNER JOIN VOUCHER_TRANS VT ON VM.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                    "INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                                    "WHERE VM.STATUS=1 AND VM.VOUCHER_ID = " + voucherid;
                            DataTable dtVoucherDetails = FetchData(sSQL);
                            if (dtVoucherDetails != null && dtVoucherDetails.Rows.Count > 0)
                            {
                                string vouchertype = dtVoucherDetails.Rows[0]["VOUCHER_TYPE"].ToString();

                                //For Receipts and Payments Vouchers
                                if (vouchertype == "RC")
                                {
                                    string updatesql = "UPDATE VOUCHER_TRANS SET AMOUNT = AMOUNT + (" + diffAmount * -1 + ")" +
                                           " WHERE VOUCHER_ID =" + voucherid + " AND TRANS_MODE='CR' AND SEQUENCE_NO = 1";
                                    ExecuteCommand(updatesql);
                                }
                                else if (vouchertype == "PY")
                                {
                                    string updatesql = "UPDATE VOUCHER_TRANS SET AMOUNT = AMOUNT + (" + diffAmount + ")" +
                                           " WHERE VOUCHER_ID =" + voucherid + " AND TRANS_MODE='DR' AND SEQUENCE_NO = 1";
                                    ExecuteCommand(updatesql);
                                }
                                else if (vouchertype == "CN")
                                {
                                    //For Withdrwal
                                    dtVoucherDetails.DefaultView.RowFilter = string.Empty;
                                    dtVoucherDetails.DefaultView.RowFilter = "TRANS_MODE='CR' AND GROUP_ID = 12";
                                    bool WithDrwalContra = (dtVoucherDetails.DefaultView.Count > 0);
                                    if (WithDrwalContra)
                                    {
                                        string updatesql = "UPDATE VOUCHER_TRANS SET AMOUNT = AMOUNT + (" + diffAmount + ")" +
                                                            " WHERE VOUCHER_ID =" + voucherid + " AND TRANS_MODE='DR' AND SEQUENCE_NO = 2";
                                        ExecuteCommand(updatesql);
                                    }
                                    else
                                    {
                                        dtVoucherDetails.DefaultView.RowFilter = string.Empty;
                                        dtVoucherDetails.DefaultView.RowFilter = "TRANS_MODE='DR' AND GROUP_ID = 12";
                                        bool DepositContra = (dtVoucherDetails.DefaultView.Count > 0);
                                        if (DepositContra)
                                        {
                                            string updatesql = "UPDATE VOUCHER_TRANS SET AMOUNT = AMOUNT + " + (diffAmount * -1) +
                                                               " WHERE VOUCHER_ID =" + voucherid + " AND TRANS_MODE='CR' AND SEQUENCE_NO = 1";
                                            ExecuteCommand(updatesql);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                WriteLog("Error in UpdateMismatchingCRsDRsVouchers " + err.Message);
            }

            return rtn;
        }

        public static DataTable GetDatabasesfromXml()
        {
            DataTable dtMultipleDatabase = null;
            try
            {
                if (!string.IsNullOrEmpty(RestoreMultipleDBPath) && System.IO.File.Exists(RestoreMultipleDBPath))
                {
                    dtMultipleDatabase = ConvertXMLToDataSetWithResultArgs(RestoreMultipleDBPath);
                }

            }
            catch (Exception Ex)
            {
                //  MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
            return dtMultipleDatabase;
        }

        /// <summary>
        /// Convert XML file into DataSet 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static DataTable ConvertXMLToDataSetWithResultArgs(string fileName)
        {
            DataSet dsReadXML = new DataSet();
            dsReadXML.ReadXml(fileName);
            return dsReadXML.Tables[0];
        }

        /// <summary>
        /// To Check the License attributes,Multiple Database is enabled or not 
        /// </summary>
        /// <returns></returns>
        public static bool IsAccesstoMultipleDatabase()
        {
            bool isMulti = false;
            string Value = "";
            string LicensePath = Path.Combine(General.ACMEERP_INSTALLED_PATH, "AcMEERPLicense.xml");
            if (System.IO.File.Exists(LicensePath))
            {
                DataSet ds = new DataSet();
                ds.ReadXml(LicensePath);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Columns.Contains("AccessToMultiDB"))
                        Value = ds.Tables[0].Rows[0]["AccessToMultiDB"].ToString();
                }
                isMulti = (Value == "1") ? true : false;
            }
            else
            {
                General.WriteLog("License File is not Found ");
            }
            return isMulti;
        }

        /// <summary>
        /// To Check the License attributes,Multiple Database is enabled or not 
        /// </summary>
        /// <returns></returns>
        public static string GetHeadOfficeCode()
        {
            string HeadOfficeCode = "";
            string LicensePath = Path.Combine(General.ACMEERP_INSTALLED_PATH, "AcMEERPLicense.xml");
            if (System.IO.File.Exists(LicensePath))
            {
                DataSet ds = new DataSet();
                ds.ReadXml(LicensePath);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Columns.Contains("HEAD_OFFICE_CODE"))
                        HeadOfficeCode = ds.Tables[0].Rows[0]["HEAD_OFFICE_CODE"].ToString();

                }
            }
            else
            {
                General.WriteLog("License File is not Found ");
            }
            return HeadOfficeCode;
        }

        /// <summary>
        /// On 01/08/2022, to get active db name from connection string
        /// </summary>
        /// <returns></returns>
        public static string GetActiveAcmeerpBranchDB()
        {
            string Rtn = string.Empty;
            try
            {
                if (Directory.Exists(ACMEERP_INSTALLED_PATH))
                {
                    string acppexepath = Path.Combine(ACMEERP_INSTALLED_PATH, "ACPP.exe");
                    Configuration acperpconfig = ConfigurationManager.OpenExeConfiguration(acppexepath);
                    if (acperpconfig != null)
                    {
                        Rtn = acperpconfig.ConnectionStrings.ConnectionStrings["AppConnectionString"].ToString();
                        MySqlConnectionStringBuilder mysqlconnectionstring = new MySqlConnectionStringBuilder(Rtn);
                        Rtn = mysqlconnectionstring.Database;
                    }
                }

            }
            catch (Exception err)
            {
                WriteLog("Error in GetActiveBranchDB " + err.Message);
            }
            return Rtn;
        }

        /// <summary>
        /// On 01/08/2022, to get active branch name
        /// </summary>
        /// <returns></returns>
        public static string GetBranchDBByBranchName(string selectedBranch)
        {
            string Rtn = string.Empty;
            try
            {
                if (Directory.Exists(ACMEERP_INSTALLED_PATH))
                {
                    if (!string.IsNullOrEmpty(selectedBranch))
                    {
                        DataTable dtMultiBranchList = GetDatabasesfromXml();
                        if (dtMultiBranchList != null && dtMultiBranchList.Rows.Count > 0)
                        {
                            //dtMultiBranchList.DefaultView.RowFilter = "RESTORE_DB='" + Rtn + "'";
                            dtMultiBranchList.DefaultView.RowFilter = "RestoreDBName='" + selectedBranch + "'";
                            if (dtMultiBranchList.DefaultView.Count > 0)
                            {
                                Rtn = dtMultiBranchList.DefaultView[0]["RESTORE_DB"].ToString();
                            }
                        }
                    }
                }

            }
            catch (Exception err)
            {
                WriteLog("Error in GetBranchDBByBranchName " + err.Message);
            }
            return Rtn;
        }

        /// <summary>
        /// On 02/08/2022, Confirm to update DB changes to all the branches or concern active branch alone 
        /// </summary>
        /// <returns></returns>
        public static bool ConfirmUpdateDBChangesSelectedBranch(string selectedBranch)
        {
            bool rtn = false;
            UpdateDBScriptSelectedBranchAlone = false;
            ActiveBranchName = string.Empty;
            ActiveBranchDB = string.Empty;

            try
            {
                if (Directory.Exists(ACMEERP_INSTALLED_PATH) && IsAccesstoMultipleDatabase() && !string.IsNullOrEmpty(selectedBranch))
                {
                    ActiveBranchName = selectedBranch;
                    ActiveBranchDB = GetBranchDBByBranchName(selectedBranch);
                    if (!string.IsNullOrEmpty(ActiveBranchDB))
                    {
                        if (MessageBox.Show("Are you sure to update selected Branch '" + ActiveBranchName + "' alone ?", General.ACMEERP_UPDATER_TITLE, MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            General.ACMEERP_MULTIDB_CONNECTION = General.ACMEERP_DB_CONNECTION;
                            General.ACMEERP_MULTIDB_NAME = ActiveBranchDB;
                            //MessageBox.Show("Active Connection String " + General.ACMEERP_MULTIDB_CONNECTION);
                            //MessageBox.Show("Active Branch :: " + ActiveBranchName, General.ACMEERP_UPDATER_TITLE);
                            UpdateDBScriptSelectedBranchAlone = true;
                            rtn = true;
                        }
                    }
                    else
                    {
                        rtn = (MessageBox.Show("It will take some minute(s) to update all the Branches, " +
                                    "Are you sure to proceed ?", General.ACMEERP_UPDATER_TITLE, MessageBoxButtons.YesNo) == DialogResult.Yes);
                    }
                }
            }
            catch (Exception err)
            {
                WriteLog("Error in ConfirmUpdateDBChangesSelectedBranch " + err.Message);
            }
            return rtn;
        }

        #region Connectionstring Encryption/Decryption in Appconfig

        /// <summary>
        /// This method is used to encrypt connectionstring section in the application config
        /// </summary>
        /// <param name="configfile">config file path </param>
        /// <param name="section">Section to encrypt</param>
        public static bool EncryptConnectionSettings(string configfile, string section)
        {
            bool Rtn = false;
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
                    Rtn = true;
                }
            }
            catch (Exception err)
            {
                WriteLog("EncryptAppSettings " + err.Message);
            }
            return Rtn;
        }


        /// <summary>
        /// On 05/11/2024, to re assign already kept report setting (before updater)
        /// 
        /// Only for few properties will be re assigned
        /// DateFrom, DateTo, DateAsOn, ShowByLedger, ShowByLedgerGroup, ShowByCostCentre, BreakByCostCentre, HorizontalLine, VerticalLine,
        /// PaperKind, PaperLandscape, MarginLeft, MarginRight, MarginTop, MarginBottom, AverageEuroExchange, AverageEuroDollorExchange
        /// </summary>
        /// <returns></returns>
        public static bool ReAssignReportSettingProperties()
        {
            bool Rtn = false;
            reportproperties.Clear();
            reportproperties.Add("Project", typeof(System.String)); reportproperties.Add("DateFrom", typeof(System.String));
            reportproperties.Add("DateTo", typeof(System.String)); reportproperties.Add("DateAsOn", typeof(System.String));
            reportproperties.Add("ShowByLedger", typeof(System.Int32)); reportproperties.Add("ShowByLedgerGroup", typeof(System.Int32));
            reportproperties.Add("IncludeNarration", typeof(System.Int32)); reportproperties.Add("ShowDailyBalance", typeof(System.Int32));
            reportproperties.Add("ShowBankDetails", typeof(System.Int32));

            reportproperties.Add("PaperKind", typeof(System.String)); reportproperties.Add("PaperLandscape", typeof(System.Int32));
            reportproperties.Add("MarginLeft", typeof(System.Int32)); reportproperties.Add("MarginRight", typeof(System.Int32));
            reportproperties.Add("MarginTop", typeof(System.Int32)); reportproperties.Add("MarginBottom", typeof(System.Int32));
            reportproperties.Add("AverageEuroExchange", typeof(System.Double)); reportproperties.Add("AverageEuroDollorExchange", typeof(System.Double));
            reportproperties.Add("ShowHeaderInstituteSocietyName", typeof(System.Int32)); reportproperties.Add("ShowHeaderInstituteSocietyAddress", typeof(System.Int32));
            reportproperties.Add("ShowProjectsinFooter", typeof(System.Int32));
            reportproperties.Add("HorizontalLine", typeof(System.Int32)); reportproperties.Add("VerticalLine", typeof(System.Int32));

            try
            {
                if (ACMEERP_PREV_REPORT_SETTING != null && ACMEERP_PREV_REPORT_SETTING.Rows.Count > 0)
                {
                    string reportsettingxml = Path.Combine(ACMEERP_INSTALLED_PATH, "ReportSetting.xml");
                    if (File.Exists(reportsettingxml))
                    {
                        DataSet ds = new DataSet();
                        ds.ReadXml(reportsettingxml);
                        if (ds.Tables.Count > 0 && ds.Tables.Contains("ReportSetting"))
                        {
                            DataTable dtNewReportSettingProperties = ds.Tables["ReportSetting"];

                            if (dtNewReportSettingProperties != null && dtNewReportSettingProperties.Rows.Count > 0)
                            {
                                AttachConcernReportProperties(ACMEERP_PREV_REPORT_SETTING);
                                AttachConcernReportProperties(dtNewReportSettingProperties);

                                foreach (DataRow dr in ACMEERP_PREV_REPORT_SETTING.Rows)
                                {
                                    string reportid = dr["ReportId"].ToString();
                                    dtNewReportSettingProperties.DefaultView.RowFilter = string.Empty;
                                    dtNewReportSettingProperties.DefaultView.RowFilter = "ReportId='" + reportid + "'";
                                    if (dtNewReportSettingProperties.DefaultView.Count > 0)
                                    {
                                        foreach (KeyValuePair<string, Type> rptproperty in reportproperties)
                                        {
                                            string property = rptproperty.Key;
                                            Type propertydatatype = rptproperty.Value as Type;

                                            if (dr.Table.Columns.Contains(property))
                                            {

                                                dtNewReportSettingProperties.DefaultView[0].BeginEdit();
                                                string propertyvalue = "";
                                                if (dr[property] != null)
                                                {
                                                    propertyvalue = dr[property].ToString();
                                                }

                                                if (string.IsNullOrEmpty(propertyvalue))
                                                {
                                                    propertyvalue = (propertydatatype == typeof(System.String) ? "" : "0");
                                                }
                                                dtNewReportSettingProperties.DefaultView[0][property] = propertyvalue;
                                                dtNewReportSettingProperties.DefaultView[0].EndEdit();
                                                dtNewReportSettingProperties.AcceptChanges();
                                                Rtn = true;
                                            }
                                        }
                                    }
                                }

                                if (Rtn)
                                {
                                    dtNewReportSettingProperties.WriteXml(reportsettingxml, XmlWriteMode.IgnoreSchema);
                                    //MessageBox.Show("Reassigned");
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception err)
            {
                WriteLog("ReassignReportSettingProperties " + err.Message);
            }
            return Rtn;
        }

        private static DataTable AttachConcernReportProperties(DataTable dt)
        {

            foreach (KeyValuePair<string, Type> rptproperty in reportproperties)
            {
                string property = rptproperty.Key;
                Type propertydatatype = rptproperty.Value as Type;

                if (!dt.Columns.Contains(property))
                {
                    DataColumn dc = new DataColumn(property, propertydatatype);
                    dt.Columns.Add(dc);
                }
            }

            return dt;
        }

        /// <summary>
        /// This method is used to update the connectionstring value in config file
        /// </summary>
        /// <param name="keyname">name of the key</param>
        /// <param name="keyvalue">valye of the key</param>
        public static bool UpdateConnectionString(string configfile, string keyname, string keyvalue)
        {
            bool Rtn = false;
            try
            {
                ExeConfigurationFileMap configFile = new ExeConfigurationFileMap();
                configFile.ExeConfigFilename = configfile;
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);

                if (config != null)
                {
                    ConnectionStringsSection oSection = config.GetSection("connectionStrings") as ConnectionStringsSection;
                    if (oSection != null)
                    {
                        oSection.ConnectionStrings[keyname].ConnectionString = keyvalue;
                        config.Save(ConfigurationSaveMode.Modified);
                        Rtn = true;
                    }
                }
            }
            catch (Exception err)
            {
                WriteLog("UpdateConnectionString " + err.Message);
            }
            return Rtn;
        }

        #endregion


        /// <summary>
        /// On 05/01/2023, To update GST Invoice details separate tables GST Invoice Yable Master and GST Invocie Ledger Details 
        /// From Voucher Master Trans those who have GST Vendor details
        /// </summary>
        /// <returns></returns>
        public static bool UpdateGSTInvoiceMaster()
        {
            bool Rtn = false;
            try
            {
                //Check GST Vendor detials availbe in Mastser Trans
                DataTable dtGSTVendorDetails = FetchData("SELECT GST_VENDOR_ID FROM VOUCHER_MASTER_TRANS WHERE STATUS = 0 AND GST_VENDOR_ID>0 LIMIT 1");
                if (dtGSTVendorDetails.Rows.Count > 0)
                {
                    //Check GST Master details availbe, If this empty, let us dump all vendor detials from Mastser Trans
                    DataTable dtGSTMasterDetails = FetchData("SELECT GST_VENDOR_ID FROM GST_INVOICE_MASTER LIMIT 1");
                    if (dtGSTMasterDetails.Rows.Count == 0)
                    {
                        string SQL = "INSERT INTO GST_INVOICE_MASTER\n" +
                                     "(GST_VENDOR_INVOICE_NO, GST_VENDOR_INVOICE_DATE, GST_VENDOR_INVOICE_TYPE, GST_VENDOR_ID, IS_REVERSE_CHARGE, REVERSE_CHARGE_AMOUNT,\n" +
                                     "BILLING_ADDRESS, BILLING_STATE_ID, BILLING_COUNTRY_ID, SHIPPING_ADDRESS, SHIPPING_STATE_ID, SHIPPING_COUNTRY_ID,\n" +
                                     "TOTAL_AMOUNT, TOTAL_CGST_AMOUNT, TOTAL_SGST_AMOUNT, TOTAL_IGST_AMOUNT, STATUS)\n" +
                                        "SELECT GST_VENDOR_INVOICE_NO, GST_VENDOR_INVOICE_DATE, GST_VENDOR_INVOICE_TYPE, GST_VENDOR_ID, 0, 0,\n" +
                                        "ASV.ADDRESS, ASV.STATE_ID, ASV.COUNTRY_ID, ASV.ADDRESS, ASV.STATE_ID, ASV.COUNTRY_ID,\n" +
                                        "SUM( CASE WHEN VM.VOUCHER_TYPE = 'RC' AND VT.TRANS_MODE='DR' THEN VT.AMOUNT\n" +
                                        "          WHEN VM.VOUCHER_TYPE = 'PY' AND VT.TRANS_MODE='CR' THEN VT.AMOUNT END) AS AMOUNT,\n" +
                                        "SUM(VT.CGST) AS CGST, SUM(VT.SGST) AS SGST, SUM(VT.IGST) AS IGST, 2\n" +
                                        "FROM VOUCHER_MASTER_TRANS VM\n" +
                                        "INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VM.VOUCHER_ID\n" +
                                        "LEFT JOIN ASSET_STOCK_VENDOR ASV ON ASV.VENDOR_ID = VM.GST_VENDOR_ID\n" +
                                        "WHERE VM.GST_VENDOR_ID >0 AND VM.STATUS =1\n" +
                                        "GROUP BY VM.VOUCHER_ID";
                        Rtn = ExecuteCommand(SQL);
                        if (Rtn)
                        {
                            SQL = "INSERT INTO GST_INVOICE_MASTER_DETAILS (GST_INVOICE_ID, LEDGER_ID, LEDGER_GST_CLASS_ID, AMOUNT, TRANS_MODE,\n" +
                                        "GST_HSN_SAC_CODE, CGST, SGST, IGST,BRANCH_ID)\n" +
                                        "SELECT GIM.GST_INVOICE_ID, VT.LEDGER_ID, VT.LEDGER_GST_CLASS_ID, VT.AMOUNT AS AMOUNT, VT.TRANS_MODE, '' AS GST_HSN_SAC_CODE,\n" +
                                        "SUM(CGST) AS CGST, SUM(SGST) AS SGST, SUM(IGST) AS IGST, VM.BRANCH_ID\n" +
                                        "FROM VOUCHER_MASTER_TRANS VM\n" +
                                        "INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VM.VOUCHER_ID\n" +
                                        "LEFT JOIN gst_invoice_master GIM ON GIM.GST_VENDOR_INVOICE_NO = VM.GST_VENDOR_INVOICE_NO\n" +
                                        "AND GIM.GST_VENDOR_INVOICE_DATE = VM.GST_VENDOR_INVOICE_DATE\n" +
                                        "WHERE VM.GST_VENDOR_ID > 0 AND VM.STATUS =1 AND VT.LEDGER_GST_CLASS_ID >0\n" +
                                        "GROUP BY VM.VOUCHER_ID, VT.LEDGER_ID;";
                            Rtn = ExecuteCommand(SQL);

                            if (Rtn)
                            {
                                SQL = "INSERT INTO VOUCHER_GST_INVOICE (GST_INVOICE_ID, VOUCHER_ID, AMOUNT)\n" +
                                        "SELECT GIM.GST_INVOICE_ID, VM.VOUCHER_ID,\n" +
                                        "SUM( CASE WHEN VM.VOUCHER_TYPE = 'RC' AND VT.TRANS_MODE='DR' THEN VT.AMOUNT\n" +
                                        "      WHEN VM.VOUCHER_TYPE = 'PY' AND VT.TRANS_MODE='CR' THEN VT.AMOUNT END) AS AMOUNT\n" +
                                        "FROM VOUCHER_MASTER_TRANS VM\n" +
                                        "INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VM.VOUCHER_ID\n" +
                                        "LEFT JOIN gst_invoice_master GIM ON GIM.GST_VENDOR_INVOICE_NO = VM.GST_VENDOR_INVOICE_NO\n" +
                                        "    AND GIM.GST_VENDOR_INVOICE_DATE = VM.GST_VENDOR_INVOICE_DATE\n" +
                                        "WHERE VM.GST_VENDOR_ID > 0 AND VM.STATUS =1\n" +
                                        "GROUP BY VM.VOUCHER_ID";
                                Rtn = ExecuteCommand(SQL);
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                WriteLog("Error in updating UpdateGSTInvoiceMaster " + err.Message);
            }

            return Rtn;
        }
    }

    public class ProgressStatusEventArgs : EventArgs
    {
        public string Status { get; set; }
        public int NoofUpdates { get; set; }
        //public string ByteSent { get; set; }
    }
}
