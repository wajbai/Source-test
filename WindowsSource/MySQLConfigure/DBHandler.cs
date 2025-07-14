//Purpose: All database handled common methods

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

namespace MySQLConfigure
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
        /// This method is used to test db connection based on the input whether mysql default db or aod db
        /// </summary>
        /// <param name="IsDefaultMySQL"></param>
        /// <returns></returns>
        public bool TestConnection(bool IsDefaultMySQL)
        {
            bool Rtn = false;
            string dbconnection = General.MYSQL_DEFAULT_CONNECTION;
            string checksql = "SELECT USER FROM mysql.user";
            if (!IsDefaultMySQL)
            {
                dbconnection = General.MYSQL_MY_CONNECTION;
                checksql = "SELECT * FROM " + General.MY_BASE_TABLENAME + " LIMIT 1";
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
                        General.WriteLog("Invalid username/password (connection default :" + IsDefaultMySQL.ToString() + ") " + ex.Message);
                        break;
                    case 1045:
                        General.WriteLog("Invalid username/password (connection default :" + IsDefaultMySQL.ToString() + ") " + ex.Message);
                        break;
                    case 1042:
                        General.WriteLog("MySQL host is not there (connection default :" + IsDefaultMySQL.ToString() + ") " + ex.Message);
                        break;
                    default:
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
        /// 1. Check aod.db exists(should contain main tables), if main tables are not there, consider that aod is not exists
        /// 2. If already (condition with main tables) exists and improper database, remove database
        /// 3. Get database script from resource stream and execute it
        /// </summary>
        /// <returns></returns>
        public bool RestoreMyDatabase()
        {
            bool Rtn = false;
            General.WriteLog("Start:RestoreMydatabase");
            try
            {
                if (!IsMyDatabaseExists())
                {
                    string sql = string.Empty;
                    General.WriteLog("Remove My database");
                    RemoveMyDatabase();

                    //Read My empty db script file
                    General.WriteLog("Start:Reading My db scripts");
                    using (Stream strmScript = this.GetType().Assembly.GetManifestResourceStream(General.abyAssembly.GetName().Name + ".DB_Create_Script.sql"))
                    {
                        using (StreamReader sr = new StreamReader(strmScript))
                        {
                            sql = sr.ReadToEnd();
                            General.WriteLog("End:Reading My DB scripts");
                        }
                    }
                    //execute My db script file
                    General.WriteLog("Start:Executing My db script");

                    using (MySqlConnection sqlCnn = new MySqlConnection(General.MYSQL_DEFAULT_CONNECTION))
                    {
                        using (MySqlCommand sqlCommand = new MySqlCommand(sql, sqlCnn))
                        {
                            sqlCommand.CommandType = CommandType.Text;
                            sqlCommand.Connection.Open();
                            sqlCommand.ExecuteNonQuery();
                            General.WriteLog("End:Executing My db script");
                        }
                    }
                }
                Rtn = IsMyDatabaseExists();
            }
            catch (Exception err)
            {
                General.WriteLog("Error in restoring My database : " + err.Message);
            }

            if (Rtn)
            {
                General.WriteLog("My Database created");
            }
            else
            {
                General.WriteLog("Failure in creating My database");
            }

            General.WriteLog("Ended:RestoreMydatabase");
            return Rtn;
        }

        /// <summary>
        /// This method is used to get update db script from resourcre stream and update databgase
        /// </summary>
        /// <returns></returns>
        public bool UpdateMyDatabaseChanges()
        {
            bool rtn = false;
            string sql = string.Empty;
            string[] query;

            //Read My db update script file
            General.WriteLog("Start:UpdateMyDBchanges");
            try
            {
                using (Stream strmScript = this.GetType().Assembly.GetManifestResourceStream(General.abyAssembly.GetName().Name + ".DB_Changes_Script.sql"))
                {
                    using (StreamReader sr = new StreamReader(strmScript))
                    {
                        //sql = sr.ReadToEnd();
                        query = sr.ReadToEnd().Split(';');
                        General.WriteLog("End:Reading My db update scripts");
                    }
                }

                for (int i = 0; i < query.Length; i++)
                {
                    try
                    {
                        sql = query[i];
                        //MessageRender.ShowMessage(sql);
                        if (!string.IsNullOrEmpty(sql))
                        {
                            if (ExecuteCommand(sql, false))
                            {
                                rtn = true;
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        General.WriteLog("Errro in UpdateMyDBchanges " + err.Message);
                    }

                }
            }
            catch (Exception err)
            {
                General.WriteLog("Errro in UpdateMyDBchanges " + err.Message);
            }
            General.WriteLog("End:UpdateMyDBchanges");

            return rtn;
        }

        /// <summary>
        /// This method is used to create SP (DROP FD)
        /// 
        /// 1. Execute DROPPROJECT.sql
        /// </summary>
        /// <returns></returns>
        public ResultArgs CreateSP_DROPFD()
        {
            ResultArgs result = new ResultArgs();
            string script = string.Empty;
            General.WriteLog("Start:CreateSP_DROPFD");
            try
            {
                using (Stream strmScript = this.GetType().Assembly.GetManifestResourceStream(General.abyAssembly.GetName().Name + ".DB_DROPFD.sql"))
                {
                    using (StreamReader sr = new StreamReader(strmScript))
                    {
                        script = sr.ReadToEnd().ToString();
                    }
                }

                if (!string.IsNullOrEmpty(script))
                {
                    if (ExecuteCommand(script, false))
                    {
                        result.Success = true;
                    }
                }
            }
            catch (Exception err)
            {
                result.Message = "Errro in DROPFD.sql " + err.Message;
            }

            General.WriteLog("End:CreateSP_DROPFD");
            return result;
        }

        /// <summary>
        ///  To fill the Data Table
        /// <param name="sSQL">SQL Statement</param>		
        /// </summary>
        public DataTable ExecuteTable(string sSQL, bool IsDefaultMySQL)
        {
            DataTable dt = new DataTable();
            string dbconnection = General.MYSQL_DEFAULT_CONNECTION;
            if (!IsDefaultMySQL)
                dbconnection = General.MYSQL_MY_CONNECTION;

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
        public bool ExecuteCommand(string sSql, bool IsDefaultMySQL)
        {
            bool Rtn = false;
            string dbconnection = General.MYSQL_DEFAULT_CONNECTION;

            if (!IsDefaultMySQL)
                dbconnection = General.MYSQL_MY_CONNECTION;

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
        /// This method is used to check whether My database exists in server or not.
        /// </summary>
        /// <returns></returns>
        private bool IsMyDatabaseExists()
        {
            bool Rtn = false;
            int TblTotal = 0;
            General.WriteLog("Start:IsMydatabaseExists");

            DataTable dt = ExecuteTable("SELECT COUNT(*) as TblCount FROM information_schema.tables WHERE table_schema = '" + General.MY_DBNAME + "'", true);
            if (dt != null && dt.Rows.Count > 0)
            {

                if (int.TryParse(dt.Rows[0]["TblCount"].ToString(), out TblTotal))
                {
                    Rtn = (TblTotal > 0);
                }
                General.WriteLog("My Database exists status:: " + dt.Rows.Count);
            }
            General.WriteLog("Ended:IsMydatabaseExists");
            return Rtn;
        }

        /// <summary>
        /// This method used to remove aod db
        /// </summary>
        /// <returns></returns>
        private bool RemoveMyDatabase()
        {
            bool Rtn = false;
            General.WriteLog("Start:RemoveMydatabase");
            if (IsMyDatabaseExists())
            {
                ExecuteCommand("Drop schema " + General.MY_DBNAME + " FROM mysql", true);
                General.WriteLog("Ended:RemoveMydatabase");
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
