using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using System.Diagnostics;
using System.Configuration;
using Bosco.Utility;
using Microsoft.Win32;
using System.ServiceProcess;
using System.Reflection;
using System.Security.AccessControl;
using System.Data.OleDb;
using System.Security.Principal;
using System.IO.Compression;
using Bosco.Utility.ConfigSetting;

namespace Bosco.DAO
{
    public class BackupAndRestore : IDisposable
    {
        #region VariableDeclaration
        string GetPathName = string.Empty;
        ResultArgs resultArgs = new ResultArgs();
        Bosco.Utility.ConfigSetting.UserProperty userProperty = new Utility.ConfigSetting.UserProperty();
        StreamWriter OutputStream;
        int RecordIncrease = 0;
        string cmd = string.Empty;
        string DataBaseName = string.Empty;
        #endregion

        #region Property
        private string ServicePath { get; set; }
        public string MyInstallPath { get; set; }
        public static string MySQLACPERP_SETUP_NAME = @"BoscoSoft\AcMEERP";
        public static string SAFETY_FOLDER_NAME = "/AcMeSafetyBackUp";
        public static string SAFETY_BACKUP_NAME = "/AcpERPSafetyBackUp";
        public static string BACKUP_CORRUPTION_MESSAGE = "As few Table(s) are not in correct format in existing Database, could not take Acmeerp Database backup. Run Acmeerp Updater/Contact Acme.erp Support Team.";
        public static string RESTORE_CORRUPTION_MESSAGE = "As few Table(s) are not in correct format in existing Database, could not take Acmeerp Database restore. Run Acmeerp Updater/Contact Acme.erp Support Team.";
        public static string BACKUP_WARNING_CORRUPTION_MESSAGE = "WARNING : As few Table(s) are not in correct format in existing Database, could not take Acmeerp Database Backup fully. Run Acmeerp Updater/Contact Acme.erp Support Team.";
        public Int32 No_of_Active_Vouchers = 0; // 26/11/2024 - number of vouchers exists in current database
        SettingProperty AppSetting = new SettingProperty();

        public event EventHandler<ProgressStatusEventArgs> OnProgress;
        #endregion

        #region BackUp and Restore
        /// <summary>
        /// On 04/09/2019, To check databse corrected
        /// # Check all schema and tables status, total number of tables count
        /// if both are equal db is not corrupted
        /// </summary>
        /// <returns></returns>
        public bool IsDBCorrupted()
        {
            bool Rtn = false;
            Int32 SchemaTablesCount = 0;
            Int32 ShowTableTablesCount = 0;
            try
            {
                if (ConfigurationManager.ConnectionStrings["AppConnectionString"] != null)
                {
                    string Connectionstring = ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString;

                    //1. Get schema Tables count
                    DataTable dtTableSchema = new DataTable();
                    dtTableSchema.Columns.Add("TABLE_NAME", typeof(string));

                    DataTable dtCorrupted = new DataTable();
                    dtCorrupted.Columns.Add("TABLE_NAME", typeof(string));

                    string sql = "SELECT TABLE_NAME FROM information_schema.tables WHERE table_schema = DATABASE()";
                    using (DataAccess db = new DataAccess())
                    {
                        dtTableSchema = db.ExecuteQuery(sql);

                        if (dtTableSchema != null)
                        {
                            SchemaTablesCount = dtTableSchema.Rows.Count;
                        }
                    }

                    //2. Get Show tables count
                    DataTable dtTableStatus = new DataTable();
                    dtTableSchema.Columns.Add("NAME", typeof(string));
                    sql = "show table status";
                    using (DataAccess db = new DataAccess())
                    {
                        dtTableStatus = db.ExecuteQuery(sql);
                        if (dtTableStatus != null)
                        {
                            ShowTableTablesCount = dtTableStatus.Rows.Count;
                        }
                    }

                    Rtn = (SchemaTablesCount != ShowTableTablesCount);

                    //To find which table(s) are corrupted
                    if (Rtn)
                    {
                        if (SchemaTablesCount > ShowTableTablesCount)
                        {
                            var idsNotInB = dtTableSchema.AsEnumerable().Select(r => r.Field<string>("TABLE_NAME")).Except(dtTableStatus.AsEnumerable().Select(r => r.Field<string>("NAME")));
                            dtCorrupted = (from row in dtTableSchema.AsEnumerable()
                                                join id in idsNotInB
                                                on row.Field<string>("TABLE_NAME") equals id
                                                select row).CopyToDataTable();

                            if (dtCorrupted.Rows.Count > 0)
                            {
                                AcMELog.WriteLog("---------------------------------");
                                AcMELog.WriteLog("Few Table(s) are corrupted");
                                foreach (DataRow dr in dtCorrupted.Rows)
                                {
                                    string tblname = dr["TABLE_NAME"].ToString();
                                    AcMELog.WriteLog(tblname);
                                }
                                AcMELog.WriteLog("---------------------------------");
                            }
                        }
                    }


                }
            }
            catch (Exception err)
            {
                Rtn = false;
                MessageRender.ShowMessage("Problem in checking Database " + err.Message);
            }
            return Rtn;
        }


        /// <summary>
        /// On 02/05/2024, To load list of databases from db server
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchDBListFromServer()
        {
            ResultArgs result = new ResultArgs();
            try
            {
                if (ConfigurationManager.ConnectionStrings["AppConnectionString"] != null)
                {
                    string Connectionstring = ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString;

                    string sql = "SHOW SCHEMAS where `Database` NOT IN ('MYSQL', 'PERFORMANCE_SCHEMA' , 'INFORMATION_SCHEMA');";
                    using (DataAccess db = new DataAccess())
                    {
                        DataTable dtSchemas = db.ExecuteQuery(sql);

                        if (dtSchemas != null)
                        {
                            result.DataSource.Data = dtSchemas;
                            result.Success = true; ;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                result.Message = err.Message;
                MessageRender.ShowMessage(result.Message);
            }
            return result;
        }

        /// <summary>
        /// Pn 02/05/2024, To generate multidb xml for all databases from db server
        /// </summary>
        /// <returns></returns>
        public ResultArgs GenerateMultiDBXMLFromServer()
        {
            ResultArgs result = new ResultArgs();
            try
            {
                if (this.AppSetting.AccesstoMultiDB == 1)
                {
                    string filename = "autogeneratedmutidb.xml";
                    
                    result = FetchDBListFromServer();
                    if (result.Success && result.DataSource.Table != null)
                    {
                        DataTable dtSchemas = result.DataSource.Table;
                        dtSchemas.TableName = "MultiBranch";
                        dtSchemas.DataSet.DataSetName = "MultiBranch";
                        DataColumn dcRestoredb = dtSchemas.Columns.Add("Restore_Db", typeof(System.String));
                        dcRestoredb.Expression = "Database";
                        DataColumn dcLicenseKey = new DataColumn(); ;

                        dtSchemas.Columns.Add(new DataColumn() { ColumnName = "MultipleLicenseKey", DataType = typeof(string), DefaultValue = "AcMEERPLicense.xml" });

                        dcLicenseKey.DefaultValue = "AcMEERPLicense.xml";
                        DataColumn dcRestoredbname = dtSchemas.Columns.Add("RestoreDBName", typeof(System.String));
                        dcRestoredbname.Expression = "Database";
                        result.DataSource.Data = dtSchemas;
                        DataView dv = new DataView();

                        dtSchemas.WriteXml(filename);
                        result.Success = true;
                        MessageRender.ShowMessage("Acme.erp Multi Database XML is generated in Acme.erp application path ('" + filename + "')");
                    }
                }
            }
            catch (Exception err)
            {
                result.Message = err.Message;
                MessageRender.ShowMessage(result.Message);
            }
            return result;
        }

        /// <summary>
        /// On 22/10/2021, to get db connection db server name
        /// </summary>
        /// <returns></returns>
        public string GetAppConfigDBServerName()
        {
            string dbservername = string.Empty;
            try
            {
                string mysqlDefaultConnection = ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString;
                MySqlConnectionStringBuilder mysqlconnectionstring = new MySqlConnectionStringBuilder(mysqlDefaultConnection);
                dbservername = mysqlconnectionstring.Server;
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
            return dbservername;
        }

        /// <summary>
        /// On 25/10/2021, to get db connection db name
        /// </summary>
        /// <returns></returns>
        public string GetAppConfigDBName()
        {
            string dbname = string.Empty;
            try
            {
                string mysqlDefaultConnection = ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString;
                MySqlConnectionStringBuilder mysqlconnectionstring = new MySqlConnectionStringBuilder(mysqlDefaultConnection);
                dbname = mysqlconnectionstring.Database;
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
            return dbname;
        }

        /// <summary>
        /// On 26/10/2021, to get db connection db port name
        /// </summary>
        /// <returns></returns>
        public string GetAppConfigDBServerPort()
        {
            string dbserverport = string.Empty;
            try
            {
                string mysqlDefaultConnection = ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString;
                MySqlConnectionStringBuilder mysqlconnectionstring = new MySqlConnectionStringBuilder(mysqlDefaultConnection);
                if (mysqlconnectionstring.Port >0 )
                {
                 dbserverport= mysqlconnectionstring.Port.ToString();
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
            return dbserverport;
        }

        /// <summary>
        /// On 09/09/2019, to get mysql windows or linqux
        /// </summary>
        /// <returns></returns>
        public bool IsLinuxMySQL()
        {
            bool Rtn = false;
            try
            {
                if (ConfigurationManager.ConnectionStrings["AppConnectionString"] != null)
                {
                    string Connectionstring = ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString;
                    string sql = "show variables where variable_name like 'version%' and value Like '%Linux%';";
                    using (DataAccess db = new DataAccess())
                    {
                        DataTable dt = db.ExecuteQuery(sql);
                        if (dt != null)
                        {
                            Rtn = (dt.Rows.Count == 1);
                        }
                    }
                }   
            }
            catch (Exception err)
            {
                Rtn = false;
                MessageRender.ShowMessage("Problem in checking Database OS Version" + err.Message);
            }
            return Rtn;
        }

        private bool isACPERPDatabaseExists()
        {
            bool Rtn = false;
            string mysqlDefaultConnection = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ConnectionString;
            string sql = "SELECT COUNT(*) as TblCount FROM information_schema.tables WHERE table_schema = 'acperp'";
            int Val = 0;
            using (MySqlConnection sqlCnn = new MySqlConnection(mysqlDefaultConnection))
            {
                using (MySqlCommand sqlCommand = new MySqlCommand(sql, sqlCnn))
                {
                    sqlCommand.Connection.Open();
                    sqlCommand.CommandType = CommandType.Text;
                    object ObjVal = sqlCommand.ExecuteScalar();
                    if (ObjVal != null)
                    {
                        Val = int.Parse(ObjVal.ToString());
                    }
                    Rtn = Val > 40 ? false : true;
                }
            }

            return Rtn;
        }

        public string MysqlVersion()
        {
            string Values = string.Empty;
            try
            {

                string mysqlDefaultConnection = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ConnectionString;
                // string sql = "show Values where variable_name = 'version'";
                string sql = "select version() as 'Version'";
                using (MySqlConnection sqlCnn = new MySqlConnection(mysqlDefaultConnection))
                {
                    using (MySqlCommand sqlCommand = new MySqlCommand(sql, sqlCnn))
                    {
                        sqlCommand.Connection.Open();
                        sqlCommand.CommandType = CommandType.Text;
                        object ObjVal = sqlCommand.ExecuteScalar();
                        if (ObjVal != null)
                        {
                            Values = ObjVal.ToString();
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Values = "5.6.10";
            }
            return Values;
        }

        public string[] MysqlInstalledDetails()
        {
            string[] Values = {"",""};
            DataTable dt = new DataTable();
            try
            {
                string mysqlDefaultConnection = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ConnectionString;
                string sql = "show variables where variable_name IN ('basedir', 'port');";
                using (MySqlDataAdapter dAdapter = new MySqlDataAdapter())
                {
                    using (MySqlConnection sqlCnn = new MySqlConnection(mysqlDefaultConnection))
                    {
                        using (MySqlCommand sqlCommand = new MySqlCommand(sql, sqlCnn))
                        {
                            sqlCommand.CommandType = CommandType.Text;
                            dAdapter.SelectCommand = sqlCommand;
                            dAdapter.Fill(dt);
                            if (dt != null && dt.Rows.Count == 2)
                            {
                                Values[0] = Path.Combine(dt.Rows[0]["basedir"].ToString(),@"\bin\mysql.exe"); //MySQL Base Installed Path
                                Values[1] = dt.Rows[0]["port"].ToString(); //MySQL Port
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                //Values = {"",""};
            }
            return Values;
        }

        public void RemoveACPERPdatabase()
        {
            if (isACPERPDatabaseExists())
            {
                string mysqlDefaultConnection = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ConnectionString;
                string sql = "Drop schema acperp FROM mysql";
                using (MySqlConnection sqlCnn = new MySqlConnection(mysqlDefaultConnection))
                {
                    using (MySqlCommand sqlCommand = new MySqlCommand(sql, sqlCnn))
                    {
                        sqlCommand.Connection.Open();
                        sqlCommand.CommandType = CommandType.Text;
                        object ObjVal = sqlCommand.ExecuteScalar();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myconn"></param>
        /// <returns></returns>
        private bool UpdateAcMEERPDBchanges(MySqlConnection myconn)
        {
            Assembly abyAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            bool rtn = false;
            string sql = string.Empty;
            string[] query;

            //Read acperp empty db update script file
            try
            {
                using (Stream strmScript = this.GetType().Assembly.GetManifestResourceStream(abyAssembly.GetName().Name + ".ACPERP_DBChange_Update.sql"))
                {
                    using (StreamReader sr = new StreamReader(strmScript))
                    {
                        //sql = sr.ReadToEnd();
                        query = sr.ReadToEnd().Split(';');
                    }
                }

                for (int i = 0; i < query.Length; i++)
                {
                    try
                    {
                        sql = query[i];
                        if (!String.IsNullOrWhiteSpace(sql))
                        {
                            using (MySqlCommand sqlCommand = new MySqlCommand(sql, myconn))
                            {
                                sqlCommand.CommandType = CommandType.Text;
                                sqlCommand.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (Exception err)
                    {
                    }

                }
            }
            catch (Exception err)
            {
            }
            return rtn;
        }
        
        /// <summary>
        /// Restore AcmeErp to check the DB Exists or not
        /// </summary>
        /// <returns></returns>
        public bool isACPERPDatabase(string DBname)
        {
            bool Rtn = false;
            string mysqlDefaultConnection = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ConnectionString;
            string sql = "SELECT COUNT(SCHEMA_NAME) AS COUNT FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = '" + DBname + "'";
            int Val = 0;
            using (MySqlConnection sqlCnn = new MySqlConnection(mysqlDefaultConnection))
            {
                using (MySqlCommand sqlCommand = new MySqlCommand(sql, sqlCnn))
                {
                    sqlCommand.Connection.Open();
                    sqlCommand.CommandType = CommandType.Text;
                    object ObjVal = sqlCommand.ExecuteScalar();
                    if (ObjVal != null)
                    {
                        Val = int.Parse(ObjVal.ToString());
                    }
                    Rtn = Val > 0 ? false : true;
                }
            }
            return Rtn;
        }

        /// <summary>
        /// To have Multiple check to be commented by chinna
        /// </summary>
        /// <param name="selectedPath"></param>
        public ResultArgs RestoreDB(string selectedPath, string dbname, string PathDB)
        {
            try
            {
                string sql = string.Empty;
                string mysqlDefaultConnection = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ConnectionString;
                //if (resultArgs.Success)
                //{
                //Assembly abyAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                if (DropDatabase(dbname))
                {
                    using (StreamReader sr = new StreamReader(selectedPath))
                    {
                        sql = sr.ReadToEnd();
                        sql = sql.Replace(PathDB, dbname.ToUpper());
                    }
                    using (MySqlConnection sqlCnn = new MySqlConnection(mysqlDefaultConnection))
                    {
                        using (MySqlCommand sqlCommand = new MySqlCommand(sql, sqlCnn))
                        {
                            sqlCommand.CommandType = CommandType.Text;
                            sqlCommand.Connection.Open();
                            sqlCommand.ExecuteNonQuery();
                            UpdateAcMEERPDBchanges(sqlCnn);
                            resultArgs.Success = true;
                        }
                    }
                }
                else
                {
                    resultArgs.Message = "Could not drop the database before restore";
                }
                //}
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// To have Multiple check to be commented by chinna
        /// 
        /// On 23/05/2020, to call new restore method
        /// </summary>
        /// <param name="selectedPath"></param>
        public ResultArgs RestoreDBNew(string selectedPath, string dbname, string PathDB)
        {
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                resultArgs = RestoreExistDBNew(selectedPath, dbname, PathDB);
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// DeCompress the gz file into Sql file.
        /// </summary>
        /// <param name="fileToDecompress"></param>
        public string DeCompress(DirectoryInfo directorySelected)
        {
            string ExtractFilePath = string.Empty;
            try
            {
                string currentFileName = directorySelected.FullName;
                string newFileName = directorySelected.Name;
                string newFilePath = currentFileName.Remove(currentFileName.Length - directorySelected.Name.Length);

                DirectoryInfo directorySelectedForDecompress = new DirectoryInfo(newFilePath);
                foreach (FileInfo fileToDecompress in directorySelectedForDecompress.GetFiles("*.gz"))
                {
                    if (newFileName.Equals(fileToDecompress.Name))
                    {
                        using (FileStream orignalFileStream = fileToDecompress.OpenRead())
                        {
                            string FileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);
                            string fileName = newFileName.Remove(newFileName.Length - fileToDecompress.Extension.Length);
                            ExtractFilePath = newFilePath + "" + fileName;
                            using (FileStream deCompressedFileStream = File.Create(FileName))
                            {
                                using (GZipStream compressionStream = new GZipStream(orignalFileStream, CompressionMode.Decompress))
                                {
                                    compressionStream.CopyTo(deCompressedFileStream);
                                    Console.WriteLine("Decompressed: {0}", fileToDecompress.Name);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally { }
            return ExtractFilePath;
        }

        /// <summary>
        /// Update DB changes 
        /// </summary>
        /// <returns></returns>
        public ResultArgs UpdateDBChanges(string ConnString)
        {
            string appConnection = string.Empty;
            if (string.IsNullOrEmpty(ConnString))
            {
                appConnection = ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString;
            }
            else
            {
                appConnection = ConnString;
            }
            string sql = string.Empty;
            try
            {
                using (MySqlConnection sqlCnn = new MySqlConnection(appConnection))
                {
                    sqlCnn.Open();
                    UpdateAcMEERPDBchanges(sqlCnn);
                    resultArgs.Success = true;
                    if (sqlCnn.State == ConnectionState.Open)
                    { sqlCnn.Close(); }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally
            {
            }
            return resultArgs;
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
                string mysqlDefaultConnection = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ConnectionString;
                string sql = "DROP DATABASE IF EXISTS `" + DBname + "`;";
                // int Val = 0;
                using (MySqlConnection sqlCnn = new MySqlConnection(mysqlDefaultConnection))
                {
                    using (MySqlCommand sqlCommand = new MySqlCommand(sql, sqlCnn))
                    {
                        sqlCommand.Connection.Open();
                        sqlCommand.CommandType = CommandType.Text;
                        object ObjVal = sqlCommand.ExecuteNonQuery();
                        if (ObjVal != null)
                        {
                            Rtn = true;
                            // Val = int.Parse(ObjVal.ToString());
                        }
                        //Rtn = Val > 0 ? true : false;
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

        /// <summary>
        /// To Exists DB to be Overriden
        /// </summary>
        /// <param name="selectedPath"></param>
        public ResultArgs RestoreExistDB(string selectedPath, string dbname, string PathDB)
        {
            try
            {
                string sql = string.Empty;
                string mysqlDefaultConnection = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ConnectionString;
                resultArgs = MySqlBackup("", dbname);
                if (resultArgs.Success)
                {
                    if (DropDatabase(dbname))
                    {
                        //Assembly abyAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                        using (StreamReader sr = new StreamReader(selectedPath))
                        {
                            sql = sr.ReadToEnd();
                            string Header = sql.Substring(0, sql.IndexOf("Table"));
                            Header = Header.Replace(PathDB, dbname.ToUpper());
                            sql = sql.Replace(sql.Substring(0, sql.IndexOf("Table")), Header);
                        }
                        using (MySqlConnection sqlCnn = new MySqlConnection(mysqlDefaultConnection))
                        {
                            using (MySqlCommand sqlCommand = new MySqlCommand(sql, sqlCnn))
                            {
                                sqlCommand.CommandType = CommandType.Text;
                                sqlCommand.Connection.Open();
                                sqlCommand.ExecuteNonQuery();
                                UpdateAcMEERPDBchanges(sqlCnn);
                                resultArgs.Success = true;
                            }
                        }
                    }
                    else
                    {
                        resultArgs.Message = "Could not drop the database before restore";
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// To Exists DB to be Overriden
        /// 
        /// On 23/05/2020, to call new restore method
        /// </summary>
        /// <param name="selectedPath"></param>
        public ResultArgs RestoreExistDBNew(string selectedPath, string dbname, string PathDB)
        {
            ResultArgs resultArgs = new ResultArgs();
            DateTime dtStartTime = DateTime.Now;
            DateTime dtEndTime = DateTime.Now;
            try
            {
                string sql = string.Empty;
                string mysqlDefaultConnection = ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString;
                resultArgs = MySqlBackup("", dbname);
                if (resultArgs.Success)
                {
                    resultArgs = RestoreDBWithMySQLCommand(dbname, PathDB, selectedPath);
                    if (resultArgs.Success)
                    {
                        using (MySqlConnection sqlCnn = new MySqlConnection(mysqlDefaultConnection))
                        {
                            UpdateAcMEERPDBchanges(sqlCnn);
                            resultArgs.Success = true;
                            dtEndTime = DateTime.Now;
                        }
                    }
                }
                
                //Temp, show timing 
                if (resultArgs.Success)
                {
                    double diffMinutes = dtEndTime.Subtract(dtStartTime).TotalSeconds / 60;
                    //MessageRender.ShowMessage("Taken time duration : " + Math.Round(diffMinutes, 2).ToString() + " Minutes");
                }
                else
                {
                    if (resultArgs.Message.Contains("Could not drop the database before restore"))
                    {
                        resultArgs.Message = "Try again to Restore Database";
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            return resultArgs;
        }


        /// <summary>
        /// On 22/05/2020, 
        /// This method is used to restore Current Acmeerp Database
        /// OLD LOGIC  : Execute entire script one by one in connection object. Sometimes it takes long time/give time out problem etc
        /// NEW LOGIC  : using mysql command to restore backup file.
        /// </summary>
        /// <returns></returns>
        public static ResultArgs RestoreDBWithMySQLCommand(string RestoreDBName, string FileBackDBName, string DBBackupFilePath)
        {
            ResultArgs result = new ResultArgs();
            string RestoreDBCommands = string.Empty;
            string RestoreResult = string.Empty;
            string AcmeerpInstalledPath = AppDomain.CurrentDomain.BaseDirectory;
            string appConnection = ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString;
            MySql.Data.MySqlClient.MySqlConnectionStringBuilder mysqlconnectionstring = new MySql.Data.MySqlClient.MySqlConnectionStringBuilder(appConnection);
            string mysqlinstalledpath = Path.Combine(AcmeerpInstalledPath + "mysql.exe"); //AcmeerpMySQLServiceDetails.MYSQL_INSTALLED_PATH;
            string AcmeerpTmpPath = Path.Combine(AcmeerpInstalledPath + @"temp\"); //Path.Combine(General.ACMEERP_INSTALLED_PATH, @"temp\");
            uint Mysqlport = mysqlconnectionstring.Port;
            if (!Directory.Exists(AcmeerpTmpPath))
            {
                Directory.CreateDirectory(AcmeerpTmpPath);
            }

            string AcmeerpTmpRestorePath = Path.Combine(AcmeerpInstalledPath + @"temp\restore.sql");// Path.Combine(General.ACMEERP_INSTALLED_PATH, @"temp\restore.sql");
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

                            //int createDBStartPosition = Header.IndexOf("CREATE DATABASE");
                            //int DBStartPosition = Header.IndexOf("`", createDBStartPosition);
                            //int DBEndPosition = Header.IndexOf("`", DBStartPosition + 1);
                            //string backupDBName = Header.Substring(DBStartPosition, (DBEndPosition - DBStartPosition) + 1);
                            //Header = Header.Replace(FileBackDBName, "`" + RestoreDBName.ToUpper() + "`");
                            Header = Header.Replace(FileBackDBName, RestoreDBName.ToUpper());

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
                            //# Drop datatbase before restoring
                            if (DropDatabase(RestoreDBName))
                            {
                                //Prepare DOS command to restore current db
                                RestoreDBCommands = "\"" + mysqlinstalledpath + "\"" + " -uroot -pacperproot -P" + Mysqlport + " < " + "\"" + AcmeerpTmpRestorePath + "\""; ;
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
                                                        //result.Message = "Unhandle exception, " + RestoreResult + " (" + RestoreDBCommands + ")";
                                                        result.Message = "Unhandle exception, " + RestoreResult;
                                                        AcMELog.WriteLog("Unhandle exception, " + RestoreResult + " (" + RestoreDBCommands + ")");
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
                            result.Message = "Acmeerp Database backup is not found in temporary path";
                        }
                    }
                    else
                    {
                        result.Message = "Acmeerp Database backup file is not found";
                    }
                }
                catch (Exception ex)
                {
                    result.Message = "Unable to restore, " + ex.Message;
                    AcMELog.WriteLog("Unable to restore, " + ex.Message);
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
                result.Message = "Mysql.exe is not found in Acmeerp Application path";
            }


            return result;
        }


        public ResultArgs RestoreEmptyDatabase(bool defaultdb =false)
        {
            ResultArgs resultArgs = new ResultArgs();
            Assembly abyAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            string sql = string.Empty;
            string CurrentDBName = "acperp";

            if (!defaultdb)
            {
                CurrentDBName = GetAppConfigDBName();
            }
            try
            {
                string filenewemptyfile = Path.Combine(SettingProperty.ApplicationStartUpPath, "Acmeerp_empty.sql");

                if (!string.IsNullOrEmpty(CurrentDBName))
                {
                    if (File.Exists(filenewemptyfile))
                    {
                        File.Delete(filenewemptyfile);
                    }

                    using (Stream strmScript = abyAssembly.GetManifestResourceStream(abyAssembly.GetName().Name + ".Acmeerp_empty.sql"))
                    {
                        using (StreamReader sr = new StreamReader(strmScript))
                        {
                            sql = sr.ReadToEnd();
                        }

                        using (StreamWriter sw = new StreamWriter(filenewemptyfile))
                        {
                            sw.Write(sql);
                        }
                    }

                    if (File.Exists(filenewemptyfile))
                    {
                        resultArgs = RestoreDBWithMySQLCommand(CurrentDBName, "acperpempty", filenewemptyfile);
                    }
                    else
                    {
                        resultArgs.Message = "Acmeerp empty Database file is not found in the installed path";
                    }
                }
                else
                {
                    resultArgs.Message = "There is no Active Database";
                }

            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            
            return resultArgs;
        }

        /// <summary>
        /// Execute query
        /// </summary>
        /// <param name="selectedPath"></param>
        public void ExecuteQuery(string selectedPath)
        {
            //try
            //{
            string[] query;
            string mysqlDefaultConnection = ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString;
            string sql = string.Empty;
            Assembly abyAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            using (StreamReader sr = new StreamReader(selectedPath))
            {
                query = sr.ReadToEnd().Split(';');
                //sql = sr.ReadToEnd();
            }
            for (int i = 0; i < query.Length; i++)
            {
                try
                {
                    sql = query[i];
                    using (MySqlConnection sqlCnn = new MySqlConnection(mysqlDefaultConnection))
                    {
                        using (MySqlCommand sqlCommand = new MySqlCommand(sql, sqlCnn))
                        {
                            sqlCommand.CommandType = CommandType.Text;
                            sqlCommand.Connection.Open();
                            sqlCommand.ExecuteNonQuery();


                        }
                    }
                }
                catch (Exception ex)
                {
                    // MessageRender.ShowMessage(ex.ToString());
                }

            }
        }

        /// <summary>
        /// This method used to create Default acperp Database 
        /// This Script will be inside the DAO ( ACPEPR_DB.sql)
        /// </summary>
        public void RestoreDefaultDatabase()
        {
            bool Rtn = false;
            try
            {
                string mysqlDefaultConnection = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ConnectionString;
                if (isACPERPDatabaseExists())
                {
                    //On 29/10/2024, to restore empty to have common method
                    /*string sql = string.Empty;
                    Assembly abyAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                    using (Stream strmScript = this.GetType().Assembly.GetManifestResourceStream(abyAssembly.GetName().Name + ".ACPERP_DB.sql"))
                    {
                        using (StreamReader sr = new StreamReader(strmScript))
                        {
                            sql = sr.ReadToEnd();
                        }
                    }
                    using (MySqlConnection sqlCnn = new MySqlConnection(mysqlDefaultConnection))
                    {
                        using (MySqlCommand sqlCommand = new MySqlCommand(sql, sqlCnn))
                        {
                            sqlCommand.CommandType = CommandType.Text;
                            sqlCommand.Connection.Open();
                            sqlCommand.ExecuteNonQuery();
                        }
                    }*/
                    ResultArgs result = RestoreEmptyDatabase(true);
                    if (result.Success)
                    {
                        Rtn = result.Success;
                    }

                }
            }
            catch (Exception err)
            {
                //MessageRender.ShowMessage("Error in restore ACPERP database");
            }
        }

        /// <summary>
        /// To take Mysql Silent Backup in the MysqlInstallation Path
        /// </summary>
        /// <param name="DrivePath"></param>
        public ResultArgs MySqlBackup(string DrivePath, string DataBaseBackupName, int flag = 0)
        {
            string[] DB;
            string[] UName;
            string[] PWord;
            string[] CheckDataBase;
            resultArgs.Success = true;
            DateTime Time = DateTime.Now;
            int Year = Time.Year;
            int Month = Time.Month;
            int Day = Time.Day;
            int Hour = Time.Hour;
            int Minites = Time.Minute;
            int Seconds = Time.Second;
            int MilliSecond = Time.Millisecond;
            GetPathName = DrivePath;
            string version = MysqlVersion();
            bool isLinuxMysql = IsLinuxMySQL();
            string linuxTabDisable_GIT_PURED_TAG = (isLinuxMysql ? " --set-gtid-purged=OFF" : string.Empty);
            string mysqlDefaultConnection = ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString;

            try
            {
                if (mysqlDefaultConnection.Contains("port") == true)
                {
                    string[] DefaultAppString = mysqlDefaultConnection.Split(';');
                    string[] SName = DefaultAppString[0].ToString().Split('=');
                    string[] Port = DefaultAppString[1].ToString().Split('=');
                    CheckDataBase = DefaultAppString[2].ToString().Split('=');
                    if (CheckDataBase[0].Contains("Connection Timeout"))
                    {
                        DB = DefaultAppString[3].ToString().Split('=');
                        UName = DefaultAppString[4].ToString().Split('=');
                        PWord = DefaultAppString[5].ToString().Split('=');
                    }
                    else
                    {
                        DB = DefaultAppString[2].ToString().Split('=');
                        UName = DefaultAppString[3].ToString().Split('=');
                        PWord = DefaultAppString[4].ToString().Split('=');
                    }
                    string serverName = SName[1];
                    string userName = UName[1];
                    string password = PWord[1];
                    string databaseName = DataBaseBackupName;   //"acperp";// DB[1];
                    string dbPort = Port[1];
                    string host = serverName;
                    string user = userName;
                    string pswd = password;
                    string dbnm = databaseName;
                    string dbport = dbPort;

                    if (version.Contains("5.6"))
                    {
                        if (!string.IsNullOrEmpty(password))
                            cmd = String.Format("-h{0} -P{1} -u{2} -p{3} --databases  --hex-blob \"{4}\" -R", host, dbport, user, pswd, dbnm);
                        else
                            cmd = String.Format("-h{0} -P{1} -u{2}  --databases --hex-blob \"{3}\" -R", host, dbport, user, dbnm);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(password))
                            cmd = String.Format("-h{0} -P{1} -u{2} -p{3} --databases  --hex-blob --set-gtid-purged=OFF \"{4}\" -R", host, dbport, user, pswd, dbnm);
                        else
                            cmd = String.Format("-h{0} -P{1} -u{2}  --databases --hex-blob --set-gtid-purged=OFF \"{3}\" -R", host, dbport, user, dbnm);
                    }
                }
                else
                {
                    string[] DefaultAppString = mysqlDefaultConnection.Split(';');
                    string[] SName = DefaultAppString[0].ToString().Split('=');
                    CheckDataBase = DefaultAppString[1].ToString().Split('=');
                    if (CheckDataBase[0].Contains("connection timeout"))
                    {
                        DB = DefaultAppString[2].ToString().Split('=');
                        UName = DefaultAppString[3].ToString().Split('=');
                        PWord = DefaultAppString[4].ToString().Split('=');
                    }
                    else
                    {
                        DB = DefaultAppString[1].ToString().Split('=');
                        UName = DefaultAppString[2].ToString().Split('=');
                        PWord = DefaultAppString[3].ToString().Split('=');
                    }

                    string serverName = SName[1];
                    string userName = UName[1];
                    string password = PWord[1];
                    string databaseName = DataBaseBackupName;
                    string host = serverName;
                    string user = userName;
                    string pswd = password;
                    string dbnm = databaseName;
                    if (version.Contains("5.6"))
                    {
                        if (!string.IsNullOrEmpty(password.TrimEnd()))
                            cmd = String.Format("-h{0} -u{1} -p{2}  --opt --databases --hex-blob {3} -R", host, user, pswd, dbnm);
                        else
                            cmd = String.Format("-h{0} -u{1} --opt --databases --hex-blob {2} -R", host, user, dbnm);

                    }
                    else // This is to take Backup in the Linux Os
                    {
                        if (!string.IsNullOrEmpty(password.TrimEnd()))
                            cmd = String.Format("-h{0} -u{1} -p{2}  --opt --databases --hex-blob --set-gtid-purged=OFF {3} -R", host, user, pswd, dbnm);
                        else
                            cmd = String.Format("-h{0} -u{1} --opt --databases --hex-blob --set-gtid-purged=OFF {2} -R", host, user, dbnm);
                    }
                }

                // application path Instead of registry Path... chinna
                string InstallationPath = GetAcMEERPDataPath();
                InstallationPath = InstallationPath + "\\mysqldump.exe";
                if (File.Exists(InstallationPath))
                {
                    string mysqldumpPath = InstallationPath;
                    string filePath = GetPathName;
                    if (filePath != string.Empty)
                    {
                        OutputStream = new StreamWriter(filePath);
                    }
                    else if (flag == 0)
                    {
                        string NameDeclared = string.Empty;
                        RecordIncrease = RecordIncrease + 1;
                        NameDeclared = SAFETY_BACKUP_NAME + RecordIncrease + "-" + Year + Month + Day + Hour + Minites + Seconds + MilliSecond + ".sql";
                        ServicePath = GetAcMEERPDataPath();
                        ServicePath = ServicePath + SAFETY_FOLDER_NAME;
                        if (!Directory.Exists(ServicePath))
                        {
                            Directory.CreateDirectory(ServicePath);
                            Access(ServicePath);
                        }
                        string test = ServicePath + NameDeclared;
                        OutputStream = new StreamWriter(test);
                    }
                    else
                    {
                        string NameDeclared = string.Empty;
                        if (flag == 1)
                        {
                            //NameDeclared = "/AcpERPBackup" + "-" + Year + Month + Day + Hour + Minites + Seconds + MilliSecond + ".sql";

                            // Added by Praven to have different Name for Multi DB starts
                            if (AppSetting.AccesstoMultiDB == (int)YesNo.Yes)
                            {
                                NameDeclared = "/" + SettingProperty.ActiveDatabaseAliasName + "_" + Year + "_" + Month + "_" + Day + "_" + Hour + "_" + Minites + "_" + Seconds + "_" + MilliSecond + ".sql";
                            }
                            else
                            {
                                NameDeclared = "/Default" + "_" + Year + "_" + Month + "_" + Day + "_" + Hour + "_" + Minites + "_" + Seconds + "_" + MilliSecond + ".sql";
                            }
                            // Different Name for Multi DB ends
                        }
                        else if (flag == 2)
                        {
                            //On 25/05/2017, get location based branch backup name
                            //NameDeclared = CommonMethod.RemoveSpecialCharacter(userProperty.InstituteName) + "-" + userProperty.PartBranchOfficeCode + ".sql";
                            NameDeclared = userProperty.BranchUploadDBName + ".sql"; ;
                        }
                        // ServicePath = InstallationPath;
                        //  if (ServicePath.IndexOf("mysqldump.exe") > 0)
                        //    ServicePath = ServicePath.Substring(0, ServicePath.IndexOf("mysqldump") - 4);
                        if (flag == 1)
                        {
                            // ServicePath = ServicePath + "AcpBackUp";
                            ServicePath = GetAcMEERPDataPath();
                            ServicePath = ServicePath + "\\AcpBackUp";
                        }
                        else if (flag == 2)
                        {
                            ServicePath = ServicePath + "AcpERPUpload/";
                            DeleteExceptRecentFiles(ServicePath, flag);
                        }
                        if (!Directory.Exists(ServicePath))
                        {
                            Directory.CreateDirectory(ServicePath);
                            Access(ServicePath);
                        }
                        string test = ServicePath + NameDeclared;
                        OutputStream = new StreamWriter(test);
                    }
                    MyInstallPath = ServicePath;
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.FileName = mysqldumpPath;
                    startInfo.Arguments = cmd + linuxTabDisable_GIT_PURED_TAG;
                    startInfo.RedirectStandardError = true;
                    startInfo.RedirectStandardInput = false;
                    startInfo.RedirectStandardOutput = true;
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = true;
                    startInfo.ErrorDialog = false;
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.StartInfo = startInfo;
                    proc.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(OnDataReceived);
                    proc.Start();
                    string readDataline = proc.StandardOutput.ReadToEnd();
                    //On 24/08/2020, To insert Current License details into backup file to validate it when it is restored ------------------
                    OutputStream.WriteLine("-- Acmeerp" + Delimiter.ECap + AppSetting.AcmeerpBranchDetailsReference + Delimiter.ECap  + No_of_Active_Vouchers.ToString());
                    //-----------------------------------------------------------------------------------------------------------------------
                    OutputStream.WriteLine(readDataline);
                    
                    proc.WaitForExit();
                    OutputStream.Flush();
                    OutputStream.Close();
                    proc.Close();
                    if (flag == 1)
                    {
                        //  DeleteExceptRecentFiles(ServicePath, flag);
                        DeleteExceptRecentFilesByDBName(ServicePath, flag);
                    }
                }
                else
                {
                    resultArgs.Message = "mysqldump is not available in the Installation Path" + " " + InstallationPath;
                }
            }
            catch (Exception ex)
            {
                resultArgs.Success = false;
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        /// <summary>
        /// this is to get recent file
        /// </summary>
        /// <param name="SourceFilePath"></param>
        private void DeleteExceptRecentFiles(string SourceFilePath, int flag)
        {
            try
            {
                if (Directory.Exists(SourceFilePath))
                {
                    string[] Files = Directory.GetFiles(SourceFilePath);
                    // Five Backup files has to be kept in the Application Path
                    if (Files.Count() > 5)
                    {
                        FileInfo RecentFileAccess = (new DirectoryInfo(SourceFilePath).GetFiles().OrderByDescending(s => s.LastWriteTime).LastOrDefault());
                        string RecentFileName = RecentFileAccess.Name;
                        foreach (string FilePath in Files)
                        {
                            if (flag == 1)
                            {
                                string FileName = Path.GetFileName(FilePath);
                                if (FileName.Equals(RecentFileName))
                                {
                                    File.Delete(FilePath);
                                    break;
                                }
                            }
                            else if (flag == 2)
                            {
                                File.Delete(FilePath);
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally { }

            //try
            //{
            //    if (Directory.Exists(SourceFilePath))
            //    {
            //        string[] Files = Directory.GetFiles(SourceFilePath);
            //        if (Files.Count() > 0)
            //        {
            //            var RecentFileAccess = (new DirectoryInfo(SourceFilePath).GetFiles().OrderByDescending(s => s.LastAccessTime).First());
            //            string RecentFileName = RecentFileAccess.Name;
            //            foreach (string FilePath in Files)
            //            {
            //                if (flag == 1)
            //                {
            //                    string FileName = Path.GetFileName(FilePath);
            //                    if (!FileName.Equals(RecentFileName))
            //                    {
            //                        File.Delete(FilePath);
            //                    }
            //                }
            //                else if (flag == 2)
            //                {
            //                    File.Delete(FilePath);
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //}
            //finally { }
        }


        /// <summary>
        /// this is to get recent file and delete based on DB if it exists more than 5
        /// </summary>
        /// <param name="SourceFilePath"></param>
        private void DeleteExceptRecentFilesByDBName(string SourceFilePath, int flag)
        {
            try
            {
                if (Directory.Exists(SourceFilePath))
                {
                    string DeleteThis = "";
                    string[] Files = Directory.GetFiles(SourceFilePath);
                    string FileName = string.Empty;
                    String[] FileNameCollections;
                    int FileCount = 0;
                    foreach (string flmstr in Files)
                    {
                        FileName = Path.GetFileName(flmstr);
                        FileNameCollections = FileName.Split('_');
                        if (FileNameCollections.Count() > 0)
                        {
                            DeleteThis = FileNameCollections[0];
                            var orderedFiles = Directory.GetFiles(SourceFilePath, DeleteThis + "_*").Select(f => new FileInfo(f)).OrderBy(f => f.LastAccessTime);
                            if (orderedFiles.Count() > 5)
                            {
                                FileCount = orderedFiles.Count();
                                foreach (FileInfo delfile in orderedFiles)
                                {
                                    if (FileCount > 5)
                                    {
                                        delfile.Delete();
                                        FileCount--;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally { }
        }

        /// <summary>
        /// To Set Rights for Particular Folder
        /// </summary>
        /// <param name="AccessPath"></param>
        private void Access(string AccessPath)
        {
            DirectorySecurity sec = Directory.GetAccessControl(AccessPath);
            SecurityIdentifier everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            sec.AddAccessRule(new FileSystemAccessRule(everyone, FileSystemRights.Modify | FileSystemRights.Synchronize, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
            Directory.SetAccessControl(AccessPath, sec);
        }

        /// <summary>
        /// Events to read the Mysql Data
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void OnDataReceived(object Sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                OutputStream.WriteLine(e.Data);
            }
        }

        /// <summary>
        /// This method is used to get MySQL Installed Path;
        /// </summary>
        /// <param name="c_name"></param>
        /// <returns></returns>
        public string MysqlInstallPath()
        {
            string servicepath = string.Empty;
            string serviceexe = string.Empty;
            FileVersionInfo VersionInfo = null;
            try
            {
                //ServiceController sc = new ServiceController("SareeManagerNotifications");
                ServiceController[] scServices;
                scServices = ServiceController.GetServices();
                foreach (ServiceController scTemp in scServices)
                {
                    if (scTemp.ServiceName.ToUpper().Contains("MYSQLACPERP"))
                    {
                        using (RegistryKey regKey1 = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\services\\" + scTemp.ServiceName))
                        {
                            if (regKey1.GetValue("ImagePath") != null)
                                servicepath = regKey1.GetValue("ImagePath").ToString();
                        }
                        if (servicepath.IndexOf("mysqld") > 0)
                        {
                            servicepath = servicepath.Substring(1, servicepath.IndexOf("mysqld") - 1);
                        }
                        if (System.IO.Directory.Exists(servicepath))
                        {
                            serviceexe = System.IO.Path.Combine(servicepath, "mysqldump.exe");
                            VersionInfo = FileVersionInfo.GetVersionInfo(serviceexe);
                        }
                        if (VersionInfo.ProductVersion != null && VersionInfo.ProductVersion.StartsWith("5.6"))
                        {
                            if (!System.IO.File.Exists(serviceexe))
                            {
                                serviceexe = "mysqldump.exe is not exists in the Mysql Installation Path";
                            }
                        }
                        else
                        {
                            serviceexe = "MYSQL 5.6 not Installation";
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.ToString());
            }
            return serviceexe;
        }

        /// <summary>
        /// To get the Acmeerp Installation Path
        /// </summary>
        /// <returns></returns>
        public static string GetAcMEERPDataPath()
        {
            //string AcMEERPPath = string.Empty;
            //AcMEERPPath = GetRegistry(Registry.LocalMachine, @"Software\" + MySQLACPERP_SETUP_NAME, "AcMEERPPath");

            //DirectoryInfo dirDirector = new DirectoryInfo(AcMEERPPath);

            //if (!dirDirector.Exists)
            //{
            //    AcMEERPPath = string.Empty;
            //}
            // //return AcMEERPPath;
            return SettingProperty.ApplicationStartUpPath;
        }

        /// <summary>
        /// This is to get the Registry of Acmeerp Path Key
        /// </summary>
        /// <param name="regkey"></param>
        /// <param name="path"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string GetRegistry(RegistryKey regkey, string path, string property)
        {
            string registryvalue = string.Empty;
            RegistryKey root = regkey.OpenSubKey(path, false);
            try
            {
                if (root != null)
                {
                    if (root.GetValue(property) != null)
                    {
                        registryvalue = root.GetValue(property).ToString();
                    }
                }
            }
            catch (Exception err)
            {
                registryvalue = string.Empty;
            }
            return registryvalue;
        }

        /// <summary>
        /// This method is used to get current database name from connection string or 
        /// if multi db, it will return current database name
        /// Get Existing Database name from AppConfig File
        /// </summary>
        /// <returns></returns>
        public string GetCurrentDatabaseName()
        {
            string databaseName = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(CommonMethod.MultiDataBaseName))
                {
                    string mysqlDefaultConnection = ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString;
                    MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder(mysqlDefaultConnection);
                    databaseName = conn_string.Database;
                }
                else
                {
                    databaseName = CommonMethod.MultiDataBaseName;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
            return databaseName;
        }

        /// <summary>
        /// This method is used to compress given datatabse backup and upload it to portal
        /// 1. Compress given backfile and store into \AcpERPUpload folder
        /// 
        /// 2. upload compressed database backup to concern head office folder in portal
        /// </summary>
        /// <param name="dblocalBackupFile"></param>
        /// <returns></returns>
        public ResultArgs UploadDatabaseToPortal(string dblocalBackupFile)
        {
            ResultArgs resultargs = new ResultArgs();
            string SEVERBACKUP_FOLDER = "AcMEERPBackup";
            //string MyDBUploadPath = Path.Combine(SettingProperty.ApplicationStartUpPath, "AcpERPUpload");
            string localBackupGZName = Path.Combine(SettingProperty.ApplicationStartUpPath, "AcpERPUpload");
            localBackupGZName = Path.Combine(localBackupGZName, this.AppSetting.BranchUploadDBName + ".sql.gz");

            if (!Directory.Exists(Path.Combine(SettingProperty.ApplicationStartUpPath, "AcpERPUpload")))
            {
                Directory.CreateDirectory(Path.Combine(SettingProperty.ApplicationStartUpPath, "AcpERPUpload"));
            }

            try
            {
                if (!String.IsNullOrEmpty(dblocalBackupFile) && File.Exists(dblocalBackupFile))
                {
                    //1. Compress given backfile into \AcpERPUpload 
                    resultargs = Utility.CommonMethod.CompressFile(dblocalBackupFile, localBackupGZName);
                    if (resultargs.Success)
                    {
                        //2. Upload compressed backupfile
                        using (AcMEERPFTP ftpFileTransfer = new AcMEERPFTP())
                        {
                            if (OnProgress != null)
                            {
                                ftpFileTransfer.OnProgress += new EventHandler<ProgressStatusEventArgs>(ftpFileTransfer_OnProgress); ;
                            }
                            string uploadUrl = Path.Combine(ConfigurationManager.AppSettings["ftpURL"].ToString(), SEVERBACKUP_FOLDER);
                            uploadUrl += "/" + this.AppSetting.HeadofficeCode + "/" + Path.GetFileName(localBackupGZName);
                            resultargs = ftpFileTransfer.uploadAcpERPDataBase(uploadUrl, localBackupGZName);
                        }
                    }
                }
                else
                {
                    resultargs.Message = "Could not Upload Database to Portal, Backup file is not exists";
                }
            }
            catch (Exception err)
            {
                resultargs.Message = "Could not Upload Database to Portal, Backup file is not exists " + err.Message;
            }

            return resultargs;
        }

        private void ftpFileTransfer_OnProgress(object sender, ProgressStatusEventArgs e)
        {
            ProgressStatusEventArgs args = new ProgressStatusEventArgs();
            args.Status = e.Status;
            OnProgress(this, args);
        }

        #endregion

        public virtual void Dispose()
        {
            GC.Collect();
        }
    }

    #region Data Migration from AcMEPlus
    public class DataAccess : IDisposable
    {
        #region Variable and Properties
        int Count = 0;
        MySqlConnection conn;
        MySqlDataAdapter adap;
        MySqlTransaction trans;
        bool ConnectionSuccess = true;
        bool hasTransCompleted = true;
        public int LastInsertedId = 0;

        OleDbConnection OleDbConn;
        OleDbDataAdapter OleDbAdap;
        bool OleDbConnectionSuccess = true;

        private bool isTallyMigration = false;
        public bool IsTallyMigration
        {
            set { isTallyMigration = value; }
            get { return isTallyMigration; }
        }
        #endregion

        #region MySQL Methods
        #region Test Connection
        public bool TestConnection()
        {
            OpenConnction();
            return ConnectionSuccess;
        }
        #endregion

        #region Open Connection
        private MySqlConnection OpenConnction()
        {
            conn = new MySqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString;
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
                conn.Open();
            }
            catch (Exception e)
            {
                Logger("MySQL Open Connection error", e.Message);
                ConnectionSuccess = false;
            }
            return conn;
        }
        #endregion

        #region Close Connection
        private void CloseConnction()
        {
            if (conn.State.Equals(ConnectionState.Open))
            {
                conn.Close();
                conn.Dispose();
            }
        }
        #endregion

        #region Query Execution
        public DataTable ExecuteQuery(string Sqlquery)
        {
            adap = new MySqlDataAdapter(Sqlquery, OpenConnction());
            DataSet dset = new DataSet();
            adap.Fill(dset);
            DataTable dt = dset.Tables[0];
            CloseConnction();
            return (dt);
        }

        public int ExecuteCommand(string Query, bool GetRowUnique = false)
        {
            MySqlCommand Command = new MySqlCommand(Query, OpenConnction());
            int val = 0;
            try
            {
                Command.CommandType = CommandType.Text;
                val = Command.ExecuteNonQuery();
                if (GetRowUnique)
                {
                    string sQuery = "SELECT LAST_INSERT_ID()";
                    Command.CommandText = sQuery;
                    Command.CommandType = CommandType.Text;
                    LastInsertedId = Convert.ToInt32(Command.ExecuteScalar().ToString());
                }
            }
            catch (MySqlException ex)
            {
                Logger(Query, ex.Message);
                //  MessageRender.ShowMessage(ex.Message);
            }
            finally
            {
                Command.Dispose();
                Command = null;
                CloseConnction();
            }
            return val;
        }

        public object ExecuteScalarValue(string sQuery)
        {
            MySqlCommand Command = new MySqlCommand(sQuery, OpenConnction());
            object val = -1;
            try
            {
                Command.CommandType = CommandType.Text;
                val = Command.ExecuteScalar();
                if (val == null)
                    val = -1;
            }
            catch (MySqlException ex)
            {
                Logger(sQuery, ex.Message);
                // MessageRender.ShowMessage(ex.Message);
            }
            finally
            {
                Command.Dispose();
                Command = null;
                CloseConnction();
            }
            return val;
        }
        #endregion
        #endregion

        #region Ole DB Methods
        public OleDbConnection OpenOleDbConnection(bool IsTans = false)
        {
            try
            {
                if (IsTans) //For Log purpose
                    Count++;
                OleDbConn = new OleDbConnection(ConfigurationManager.AppSettings.Get("OleDBConStr"));
                if (OleDbConn != null)
                {
                    if (OleDbConn.State == ConnectionState.Open)
                    {
                        OleDbConn.Close();
                        OleDbConn.Dispose();
                    }
                    OleDbConn.Open();
                }
                else
                {
                    MessageRender.ShowMessage("OleDBConStr tag is missing in the App.Configuration file");
                }
            }
            catch (Exception e)
            {
                Logger("OleDB Connection Error", e.Message);
                OleDbConnectionSuccess = false;
            }
            return OleDbConn;
        }

        public bool OleDbTestConnection()
        {
            OpenOleDbConnection();
            return OleDbConnectionSuccess;
        }

        private void CloseOleDbConnection()
        {
            if (OleDbConn.State == ConnectionState.Open)
            {
                OleDbConn.Close();
                OleDbConn.Dispose();
            }
        }
        /// <summary>
        /// If return value is null. It will return -1
        /// </summary>
        /// <param name="sQuery"></param>
        /// <param name="OleDbConn"></param>
        /// <returns></returns>
        public object ExecuteOleDbScalarValue(string sQuery, OleDbConnection OleDbConn)
        {
            //OleDbCommand Command = new OleDbCommand(sQuery, OpenOleDbConnection(IsTans));
            OleDbCommand Command = new OleDbCommand(sQuery, OleDbConn);
            object val = -1;
            try
            {
                Command.CommandType = CommandType.Text;
                val = Command.ExecuteScalar();
            }
            catch (MySqlException ex)
            {
                Logger(sQuery, ex.Message);
                // MessageRender.ShowMessage(ex.Message);
            }
            finally
            {
                Command.Dispose();
                Command = null;
                // CloseConnction();
            }
            return (val == null) ? -1 : val;
        }

        public DataTable ExecuteOleDbQuery(string Sqlquery, OleDbConnection OleDbConn)
        {
            DataTable dt = null;
            try
            {
                // OleDbAdap = new OleDbDataAdapter(Sqlquery, OpenOleDbConnection());
                OleDbAdap = new OleDbDataAdapter(Sqlquery, OleDbConn);
                DataSet dset = new DataSet();
                OleDbAdap.Fill(dset);
                dt = dset.Tables[0];
                // CloseOleDbConnection();
            }
            catch (Exception e)
            {
                Logger(Sqlquery, e.Message);
                // MessageRender.ShowMessage(e.Message);
            }
            return (dt);
        }

        #endregion

        #region Transaction
        public void BeginTransaction()
        {
            OpenConnction();
            trans = conn.BeginTransaction();
        }

        public void EndTransaction()
        {
            if (hasTransCompleted)
            {
                trans.Rollback();
            }
        }
        #endregion

        #region Dispose
        void IDisposable.Dispose()
        {
            GC.Collect();
        }
        #endregion

        #region Migration Log
        public void Logger(String Query, String Error, bool IsTitle = false, string HeaderType = "", bool ClearLog = true, bool MigrationCompleted = false)
        {
            try
            {
                string LogName = IsTallyMigration ? "\\Tally_MigrationLog.txt" : "\\APP_Migrationlog.txt";
                if (IsTitle)
                {
                    System.IO.StreamWriter file = new System.IO.StreamWriter(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)) + LogName, ClearLog);
                    file.WriteLine(String.Format("{0}-----------------------------------------{1}: {2}-----------------------------------", Environment.NewLine, HeaderType, DateTime.Now.ToString("HH:mm:ss tt")));
                    file.Close();
                }
                else if (MigrationCompleted)
                {
                    System.IO.StreamWriter file = new System.IO.StreamWriter(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)) + LogName, true);
                    file.WriteLine(String.Format("{0}Migration Completed", Environment.NewLine));
                    file.Close();
                }
                else
                {
                    System.IO.StreamWriter file = new System.IO.StreamWriter(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)) + LogName, true);
                    file.WriteLine(String.Format("{1} Error Query : {0}", Query, Environment.NewLine));
                    file.WriteLine(String.Format("{1} Error       : {0}", Error, Environment.NewLine));
                    file.Close();
                }
            }
            catch (Exception ee)
            {
                MessageRender.ShowMessage(ee.Message);
            }
        }

        public void WriteMigrationSummary(int MasCount, int TransCount, int MappingCount, string Time, bool ClearSummary, string TimeStarted, int ProjectCount)
        {
            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)) + "\\MigrationSummary.txt", ClearSummary);
                file.WriteLine(String.Format("{0}-----------------------------------APP Migration Summary Started On {1} at {2} -------------------------------------{3}",
                     Environment.NewLine, DateTime.Now.ToShortDateString(), TimeStarted, Environment.NewLine));
                file.WriteLine(String.Format("{0} Total Migrated Record ---------------------> :{1}", Environment.NewLine, MasCount + TransCount + MappingCount));
                file.WriteLine(String.Format("{0} Project Count -----------------------------> :{1}", Environment.NewLine, ProjectCount));
                file.WriteLine(String.Format("{0} Masters Count -----------------------------> :{1}", Environment.NewLine, MasCount));
                file.WriteLine(String.Format("{0} Transaction Count -------------------------> :{1}", Environment.NewLine, TransCount));
                file.WriteLine(String.Format("{0} Mapping Count -----------------------------> :{1}", Environment.NewLine, MappingCount));
                file.WriteLine(String.Format("{0} Time Taken for Migration ------------------> :{1}", Environment.NewLine, Time));
                file.WriteLine(String.Format("{0}-------------------------------------Migration ended on {1} at {2}----------------------------------------", Environment.NewLine, DateTime.Now.ToShortDateString(), DateTime.Now.ToString("HH:mm:ss tt")));
                file.Close();
            }
            catch (Exception e)
            {
                MessageRender.ShowMessage(e.Message);
            }
        }
        #endregion


    }
    #endregion
}
