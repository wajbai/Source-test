using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Sql;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Data;

using System.Configuration;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Configuration.Install;
using System.Threading;
using System.ComponentModel;

using Bosco.Utility;

namespace AcMEERPInstaller
{
    public class DBHandler : IDisposable
    {
        #region Decelaration
        private MySqlConnection mysqlCon = null;
        private DataTable dtTable = new DataTable();
        
        #endregion

        public DBHandler()
        {

        }

        /// <summary>
        /// This method is used to test db connection based on the input whether mysql default db or acp erp db
        /// </summary>
        /// <param name="IsDefaultMySQL"></param>
        /// <returns></returns>
        public bool TestConnection(bool IsDefaultMySQL)
        {
            bool Rtn = false;
            string dbconnection = General.MYSQL_DEFAULT_CONNECTION;
            string checksql = "SELECT USER FROM MYSQL.USER";
            if (!IsDefaultMySQL)
            {
                dbconnection = General.MYSQL_ACPERP_CONNECTION;
                checksql = "SELECT * FROM VOUCHER_MASTER_TRANS LIMIT 1";
            }

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
                        //MessageBox.Show("Cannot connect to server. contact support team", General.ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        General.WriteLog("Invalid username/password (connection default :" + IsDefaultMySQL.ToString() + ") " + ex.Message);
                        break;
                    case 1045:
                        //if (!IsDefaultMySQL)
                        //    MessageBox.Show("Invalid username/password, please try again, " + dbconnection, General.ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        General.WriteLog("Invalid username/password (connection default :" + IsDefaultMySQL.ToString() + ") " + ex.Message);
                        break;
                    case 1042:
                        //MessageBox.Show(ex.Message, General.ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        General.WriteLog("MySQL host is not there (connection default :" + IsDefaultMySQL.ToString() + ") " + ex.Message);
                        break;
                    default:
                        //MessageBox.Show(ex.Message, General.ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        General.WriteLog("Error in checking connection (connection default :" + IsDefaultMySQL.ToString() + ") " + ex.Message);
                        break;
                }
            }
            catch (Exception err)
            {
                General.WriteLog("Error in checking connection (connection default :" + IsDefaultMySQL.ToString() + ") " + err.Message);
            }

            return Rtn;
        }

        /// <summary>
        /// This method is used to restore db script file
        /// 1. Check Acperp.db exists(should contain main tables), if main tables are not there, consider that acperp is not exists
        /// 2. If already (condition with main tables) exists and improper database, remove database
        /// 3. Get database script from resource stream and execute it
        /// </summary>
        /// <returns></returns>
        public bool RestoreACPERPdatabase()
        {
            bool Rtn = false;
            General.WriteLog("Start:RestoreACPERPdatabase");
            try
            {
                if (!IsACPERPdatabaseExists())
                {
                    string sql = string.Empty;
                    General.WriteLog("Remove ACPERP database");
                    RemoveACPERPdatabase();

                    //Read acperp empty db script file
                    General.WriteLog("Start:Reading ACPERP scripts");
                    using (Stream strmScript = this.GetType().Assembly.GetManifestResourceStream(General.abyAssembly.GetName().Name + ".ACPERP_DB.sql"))
                    {
                        using (StreamReader sr = new StreamReader(strmScript))
                        {
                            sql = sr.ReadToEnd();
                            General.WriteLog("End:Reading ACPERP scripts");
                        }
                    }
                    //execute ACPERP db script file
                    General.WriteLog("Start:Executing ACPERP db script");
                    
                    using (MySqlConnection sqlCnn = new MySqlConnection(General.MYSQL_DEFAULT_CONNECTION))
                    {
                        using (MySqlCommand sqlCommand = new MySqlCommand(sql, sqlCnn))
                        {
                            sqlCommand.CommandType = CommandType.Text;
                            sqlCommand.Connection.Open();
                            sqlCommand.ExecuteNonQuery();
                            General.WriteLog("End:Executing ACPERP db script");
                        }
                    }
                }
                Rtn = IsACPERPdatabaseExists();
            }
            catch (Exception err)
            {
                General.WriteLog("Error in restoring ACPERP database : " + err.Message);
            }

            if (Rtn)
            {
                General.WriteLog("ACPERP Database created");
            }
            else
            {
                General.WriteLog("Failure in creating ACPERP database");
            }

            General.WriteLog("Ended:RestoreACPERPdatabase");
            return Rtn;
        }

        /// <summary>
        /// This method is used to get update db script from resourcre stream and update databgase
        /// </summary>
        /// <returns></returns>
        public bool UpdateACPERPDBchanges()
        {
            bool rtn = false;
            string sql = string.Empty;
            string[] query;

            //Read acperp empty db update script file
            General.WriteLog("Start:UpdateACPERPDBchanges");
            try
            {
                using (Stream strmScript = this.GetType().Assembly.GetManifestResourceStream(General.abyAssembly.GetName().Name + ".ACPERP_DBChanges.sql"))
                {
                    using (StreamReader sr = new StreamReader(strmScript))
                    {
                        //sql = sr.ReadToEnd();
                        query = sr.ReadToEnd().Split(';');
                        General.WriteLog("End:Reading ACPERP db update scripts");
                    }
                }

                for (int i = 0; i < query.Length; i++)
                {
                    try
                    {
                        sql = query[i];
                        if (ExecuteCommand(sql, false))
                        {
                            rtn = true;
                        }
                    }
                    catch (Exception err)
                    {
                        General.WriteLog("Errro in UpdateACPERPDBchanges " + err.Message);
                    }

                }
            }
            catch (Exception err)
            {
                General.WriteLog("Errro in UpdateACPERPDBchanges " + err.Message);
            }
            General.WriteLog("End:UpdateACPERPDBchanges");

            return rtn;
        }
        
        /// <summary>
        ///  To fill the Data Table
        /// <param name="sSQL">SQL Statement</param>		
        /// </summary>
        public  DataTable ExecuteTable(string sSQL, bool IsDefaultMySQL)
        {
            DataTable dt = new DataTable();
            string dbconnection = General.MYSQL_DEFAULT_CONNECTION;
            if (!IsDefaultMySQL)
                dbconnection = General.MYSQL_ACPERP_CONNECTION;

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
                        }
                    }
                }
            }
            catch (Exception e)
            {
                General.WriteLog("Error in executeing ExecuteTable " + e.Message);
            }
            return dt;
        }

        /// <summary>
        ///  To execute the Sql Statement
        ///  Executing Insert/Update/Delete statements
        /// <param name="sSql">SQL Statement</param>
        /// </summary>
        public  bool ExecuteCommand(string sSql, bool IsDefaultMySQL)
        {
            bool Rtn = false;
            string dbconnection = General.MYSQL_DEFAULT_CONNECTION;

            if (!IsDefaultMySQL)
                dbconnection = General.MYSQL_ACPERP_CONNECTION;

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
                
        public Cursor Cursor { get; set; }

        /// <summary>
        /// This method is used to check whether acperp database exists in server or not.
        /// 
        /// 1. table count must be >40, 
        /// 2. if it is less than 40, acperp database is not in proper, retrn as not exists
        /// </summary>
        /// <returns></returns>
        private bool IsACPERPdatabaseExists()
        {
            bool Rtn = false;
            int TblTotal = 0;
            General.WriteLog("Start:IsACPERPdatabaseExists");

            DataTable dt = ExecuteTable("SELECT COUNT(*) as TblCount FROM information_schema.tables WHERE table_schema = '" + General.MySQL_ACPERP_DBNAME + "'", true);
            if (dt != null && dt.Rows.Count > 0)
            {

                if (int.TryParse(dt.Rows[0]["TblCount"].ToString(), out TblTotal))
                {
                    Rtn = (TblTotal >= 40); 
                }
                General.WriteLog("Acme.erp Database exists status:: " + dt.Rows.Count);
            }
            General.WriteLog("Ended:IsACPERPdatabaseExists");
            return Rtn;
        }

        /// <summary>
        /// This method used to remove acmerp db
        /// </summary>
        /// <returns></returns>
        private bool RemoveACPERPdatabase()
        {
            bool Rtn = false;
            General.WriteLog("Start:RemoveACPERPdatabase");
            if (IsACPERPdatabaseExists())
            {
                ExecuteCommand("Drop schema ACPERP FROM mysql", true);
                General.WriteLog("Ended:RemoveACPERPdatabase");
            }
            return Rtn;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
        }

    }
}
