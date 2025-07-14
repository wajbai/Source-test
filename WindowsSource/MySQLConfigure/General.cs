/* 
 * Purpose: All glboal variables, common methods and customzed our application properties
 * 
 * Initialize_MySQLConfigure_Settings is the main method, define or set our own application propertis
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Win32;
using System.IO;
using System.ServiceProcess;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using Bosco.Utility;
using System.Windows.Forms;
using System.Configuration;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Net;
using System.ComponentModel;

namespace MySQLConfigure
{
    public static class General
    {
        public enum InstallationMode
        {
            [Description("New Acme.erp Service")]
            SERVER_NEW_INSTANCE,
            [Description("System Existing Service")]
            SERVER,
            [Description("Connect to Server")]
            CLIENT
        }

        #region My Application Properties

        public static string MYAPPLICATION = "Acme.erp";                            //Customize:: Name of the application 
        public static string MYSERVICE_NAME = "MYSQLACPERP";                        //Customize:: Name of the our own MySQL Service
        public static string MY_DBNAME = "acperp";                                  //Customize:: Name of the application databasename
        public static string MY_BASE_TABLENAME = "voucher_master_trans";            //Customize:: Name of the application table, to check db exists or not
        public static string MYAPPLICATION_EXE_FILE = "ACPP.exe";                   //Customize:: Name of the application exe
        public static string MYAPPLICATION_EXE_CONFIG_FILE = "ACPP.exe.config";     //Customize:: Name of the application config file
        public static bool CAN_ASK_CONFIRMATION_TO_USE_EXISTING_MYSQL = false;      //Customize:: option to ask confirmation to use existing mysql
        public static string name_log_file = "installer.txt";                       //Customize : Name of the log file
        public static string name_license_file = "AcMEERPLicense.xml";                                //Customize : Name of the licensefile

        public static bool ACMEERP_FOUND_OLD_INSTALLATION = false;              //If Acme.erp/AOD is arleady installed by using previous (old logic for service creation)
        public static string ACMEERP_MYSERVICE_NAME = "MYSQLACPERP";            // To manage old acme.erp installed systems

        public static bool MYSERVICE_IS_INSTALLED = false;
        public static bool MYSERVICE_IS_RUNNING = false;
        public static string MY_DATA_PATH = string.Empty;
        public static string MYSERVICE_INI_FILE = "My.ini";
        public static bool LIC_UPDATED = false;
        public static bool DB_CONFIGURED = false;
        public static string LIC_PATH = string.Empty;
        #endregion

        public static string MYSQL_DEFAULT_CONNECTION = string.Empty;   //Connection for default mysql db
        public static string MYSQL_MY_CONNECTION = string.Empty;       //Connection for our application db
        public static bool MYSQL_IS_INSTALLED = false;
        public static string MYSQL_INSTALL_PATH = string.Empty;

        public static string MySQL_HOSTNAME = "localhost";
        public static string MySQL_PORT = "3320";
        public static string MySQL_ROOT_USERNAME = "root";
        public static string MySQL_ROOT_PASSWORD = "acperproot";                                //Customize : Set root password which is fixed in mysql base folder
        public static string Title = MYAPPLICATION + " Configuration";

        public static InstallationMode enInstallationMode = InstallationMode.CLIENT;
        public static Assembly abyAssembly = System.Reflection.Assembly.GetExecutingAssembly();
        public static string TARGET_PATH = string.Empty;
        public static string INSTALLER_LOG = Path.Combine(TARGET_PATH, name_log_file);
        public static string RETURN_MSG = string.Empty;
        public static bool MYSQL_BUILD_IN_SERVICE_EXISTS = false; //If mysql service other than my service is already rununing..(other port)
        private static SimpleEncrypt.SimpleEncDec objdec = new SimpleEncrypt.SimpleEncDec();

        /// <summary>
        /// Write exection and error log
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteLog(string msg)
        {
            try
            {
                string logpath = INSTALLER_LOG;
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
                throw new Exception("Error in writing installer log " + ex.Message);
            }
        }

        /// <summary>
        /// Clear log file
        /// </summary>
        public static void ClearLog()
        {
            try
            {

                string logpath = INSTALLER_LOG;
                if (System.IO.File.Exists(logpath))
                {
                    using (var fs = new FileStream(logpath, FileMode.Truncate))
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in writing installer log " + ex.Message);
            }
        }

        /// <summary>
        /// This method is used to configure My service and Restore and update my database by using script.sql
        /// </summary>
        /// <returns></returns>
        public static bool ConfigureMyService()
        {
            bool Rtn = false;
            bool mysqlconfigured = false;
            try
            {
                WriteLog("Start:ConfigureMyService");
                if (!MYSERVICE_IS_INSTALLED)
                {
                    if (CreateMyService())
                    {
                        mysqlconfigured = true;
                    }
                }
                else
                {
                    if (!MYSERVICE_IS_RUNNING)
                    {
                        mysqlconfigured = ChangeMyServiceStatus(true);
                    }
                    else
                    {
                        mysqlconfigured = true;
                    }
                }

                //On 30/09/2024, Try Final one --------------------------------------------------------------------
                if (!mysqlconfigured)
                {
                    mysqlconfigured = ChangeMyServiceStatus(true);
                }
                //-------------------------------------------------------------------------------------------------
                
                if (mysqlconfigured)
                {
                    Rtn = ConfigureMyDataBase();
                }
                else
                {
                    WriteLog("ConfigureMyService: My New service is not configured, DB would not be created");
                }
            }
            catch (Exception er)
            {
                MessageRender.ShowMessage(er.Message);
                Rtn = false;
                WriteLog("Error in ConfigureMyService, " + er.Message);
            }
            WriteLog("Ended:ConfigureMyService");
            return Rtn;
        }

        /// <summary>
        /// This method is used to configure aod database
        /// 1. check mysql connection with user name and password
        /// 2. Restore aod database
        /// 3. Update My Application Config file
        /// 4. Update My DB changes
        /// </summary>
        /// <returns></returns>
        public static bool ConfigureMyDataBase()
        {
            bool Rtn = false;
            WriteLog("Start:ConfigureMyDB");

            try
            {
                using (DBHandler dbh = new DBHandler())
                {
                    if (dbh.TestConnection(true))
                    {
                        if (dbh.RestoreMyDatabase())
                        {
                            if (CreateMyExeConfig()) //Create My Application exe Config file with connection string
                            {
                                dbh.UpdateMyDatabaseChanges();
                                dbh.CreateSP_DROPFD();
                                Rtn = true;
                            }
                            else
                            {
                                WriteLog("Error in creating my exe config file");
                            }
                        }
                        else
                        {
                            Rtn = false;
                            WriteLog("Error in Restore DB ConfigureMyDB ");
                        }
                    }
                }
            }
            catch (Exception errr)
            {
                Rtn = false;
                WriteLog("Error in ConfigureMyDB " + errr.Message);
            }
            WriteLog("End:ConfigureMyDB");
            return Rtn;
        }

        /// <summary>
        /// this method is used to update connection string in aod config file if aod connection is sucessed
        /// this mehtod will be used when instalation mode is client
        /// </summary>
        /// <returns></returns>
        public static bool ConnectMyDataBase()
        {
            bool Rtn = false;
            WriteLog("Start:ConnectMyDB");

            try
            {
                using (DBHandler dbh = new DBHandler())
                {
                    if (dbh.TestConnection(false))
                    {
                        if (CreateMyExeConfig()) //Create My Applcation exe Config file with connection string
                        {
                            dbh.UpdateMyDatabaseChanges();
                            Rtn = true;
                        }
                    }
                }
            }
            catch (Exception errr)
            {
                Rtn = false;
                WriteLog("Error in ConnectMyDB " + errr.Message);
            }
            WriteLog("End:ConfigureMyDB");
            return Rtn;
        }

        /// <summary>
        /// this method is used to get avialbe ports for mysql service between 1025 and 5000
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static int GetMySQLPortAvilable(string port)
        {
            //1025–5000
            int MySQLPort = int.Parse(port);
            int avilablePort = 0;
            WriteLog("Start:IsMySQLPortAvilable");
            try
            {
                if (MySQLPort >= 1025 && MySQLPort <= 5000)
                {
                    for (int checkport = MySQLPort; checkport <= 5000; checkport++)
                    {
                        if (!isPortUsing(checkport))
                        {
                            avilablePort = checkport;
                            MySQL_PORT = avilablePort.ToString();
                            WriteLog("Found free port");
                            break;
                        }
                    }
                }
                else
                {
                    avilablePort = 0;
                    MessageBox.Show("MySQL Port should be between 1025 and 5000", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception err)
            {
                WriteLog("Error in checking valid port " + err.Message);
            }
            WriteLog("Ended:IsMySQLPortAvilable");
            return avilablePort;
        }

        /// <summary>
        /// This method is used to create and update config file
        /// Replace ths following tags in app.config file with values
        /// 1. Replace("<localhost>", General.MySQL_HOSTNAME);
        /// 2. Replace("<mysqlport>", General.MySQL_PORT)
        /// 3. Replace("<username>", General.MySQL_ROOT_USERNAME);
        /// 4. Replace("<userpassword>", General.MySQL_ROOT_PASSWORD);
        /// 5. Encrypt connection string in app.cong file (optional)
        /// </summary>
        /// <returns></returns>
        public static bool CreateMyExeConfig()
        {
            bool Rtn = false;
            string acperpconfig = string.Empty;
            string acperpConnectionstring = string.Empty;
            string mysqlConnectionString = string.Empty;
            WriteLog("Start:CreateMyExeConfig");
            try
            {
                using (DBHandler dbh = new DBHandler())
                {
                    if (dbh.TestConnection(false))
                    {

                        using (Stream strmScript = Assembly.GetExecutingAssembly().GetManifestResourceStream(abyAssembly.GetName().Name + ".My.exe.config"))
                        {
                            using (StreamWriter sw = new StreamWriter(TARGET_PATH + "\\" + MYAPPLICATION_EXE_CONFIG_FILE, false))
                            {
                                using (StreamReader sr = new StreamReader(strmScript))
                                {
                                    acperpconfig = sr.ReadToEnd();

                                    acperpConnectionstring = "server=" + MySQL_HOSTNAME + "; port=" + MySQL_PORT + "; database=acperp; uid=" + MySQL_ROOT_USERNAME + ";pwd=" + MySQL_ROOT_PASSWORD + "; charset=utf8; pooling=false";
                                    mysqlConnectionString = "server=" + MySQL_HOSTNAME + "; port=" + MySQL_PORT + "; database=mysql; uid=" + MySQL_ROOT_USERNAME + ";pwd=" + MySQL_ROOT_PASSWORD + "; charset=utf8; pooling=false";

                                    acperpconfig = acperpconfig.Replace("<myconnection>", acperpConnectionstring); // General.Encrypt(Connectionstring));
                                    acperpconfig = acperpconfig.Replace("<mysqlconnection>", mysqlConnectionString); // General.Encrypt(mysqlConnectionString));

                                    // acperpconfig = acperpconfig.Replace("<localhost>", General.MySQL_HOSTNAME);
                                    // acperpconfig = acperpconfig.Replace("<mysqlport>", General.MySQL_PORT);
                                    // acperpconfig = acperpconfig.Replace("<username>", General.MySQL_ROOT_USERNAME);
                                    // acperpconfig = acperpconfig.Replace("<userpassword>", General.MySQL_ROOT_PASSWORD);

                                    sw.Write(acperpconfig);
                                    Rtn = true;
                                    WriteLog("End:CreateMyExeConfig");
                                }
                            }
                        }

                        //Encrypting config file
                        //bool bresult = General.EncryptConnectionSettings(Path.Combine(General.TARGET_PATH, General.MYAPPLICATION_EXE_CONFIG_FILE), "connectionStrings");
                        //if (bresult)
                        //{
                        //    General.WriteLog("Application config encrypted");
                        //}
                        //else
                        //{
                        //    General.WriteLog("Application config not encrypted ");
                        //}
                    }
                    else
                    {
                        WriteLog("CreateMyExeConfig: Connection failed.");
                    }
                }
            }
            catch (Exception err)
            {
                WriteLog("Error in CreateMyExeConfig " + err.Message);
            }
            WriteLog("Ended:CreateMyExeConfig");
            return Rtn;
        }

        /// <summary>
        /// This method is used to detele mysqlacperp service
        /// </summary>
        /// <returns></returns>
        public static bool RemoveMyService()
        {
            bool bRtn = false;
            string ServiceStatus = string.Empty;
            WriteLog("Start:RemoveMyService");
            try
            {
                if (ChangeMyServiceStatus(false))
                {
                    using (Process p = new Process())
                    {
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        p.StartInfo.CreateNoWindow = true;
                        p.StartInfo.RedirectStandardInput = true;
                        p.StartInfo.RedirectStandardOutput = true;
                        p.StartInfo.UseShellExecute = false; //required to redirect
                        string MyService = "SC DELETE " + MYSERVICE_NAME;
                        p.StartInfo.FileName = "cmd.exe ";
                        p.Start();
                        using (System.IO.StreamReader SR = p.StandardOutput)
                        {
                            using (System.IO.StreamWriter SW = p.StandardInput)
                            {
                                //mysqld –console
                                SW.WriteLine(MyService);
                                SW.WriteLine("exit");
                                ServiceStatus = SR.ReadToEnd();
                            }
                            //DeleteService SUCCESS
                            if (ServiceStatus.ToUpper().IndexOf("DELETESERVICE SUCCESS") >= 0)
                            {
                                bRtn = true;
                                WriteLog("Sucessfully my service removed :: " + ServiceStatus);
                            }
                            else
                            {
                                WriteLog("Sucessfully my service is not removed :: " + ServiceStatus);
                            }
                        }
                    }

                    //Remove acperpini file
                    //if (File.Exists(Path.Combine(General.MYSQL_INSTALL_PATH, "acperp.ini")))
                    //{
                    //    General.WriteLog("Before removing ininin removing mysqlacperp service " + err.Message);
                    //    File.Delete(Path.Combine(General.MYSQL_INSTALL_PATH, "acperp.ini"));
                    //    General.WriteLog("Error in removing mysqlacperp service " + err.Message);
                    //}
                }
            }
            catch (Exception err)
            {
                WriteLog("Error in removing my service " + err.Message);
            }
            WriteLog("Ended:RemoveMyService");
            return bRtn;
        }

        /// <summary>
        /// Set defual and acperp conneciton string wwith username and password
        /// </summary>
        public static void SetMySQLConnectionString()
        {

            MYSQL_DEFAULT_CONNECTION = "server=" + MySQL_HOSTNAME + ";port=" +
                                             MySQL_PORT + ";user id=" + MySQL_ROOT_USERNAME + "; password=" +
                                             MySQL_ROOT_PASSWORD + "; database=mysql; charset=utf8; pooling=true;Connect Timeout=600;";
            MYSQL_MY_CONNECTION = "server=" + MySQL_HOSTNAME + ";port=" +
                                            MySQL_PORT + ";user id=" + MySQL_ROOT_USERNAME + "; password=" +
                                            MySQL_ROOT_PASSWORD + "; database=" + MY_DBNAME + "; charset=utf8; pooling=true;Connect Timeout=600;";
        }

        /// <summary>
        /// this method is used to check whether mysql service is already running or not 
        /// This method will be called if installation mode is server
        /// </summary>
        /// <returns></returns>
        public static bool IsMySQL5_6_Service_Exists()
        {
            string installedPath = string.Empty;
            bool isalreadyserviceexists = false;
            string servicepath = string.Empty;
            WriteLog("Start:IsMySQL5_6_Service_Exists");
            try
            {
                ServiceController sc = new ServiceController("SareeManagerNotifications");
                ServiceController[] scServices;
                scServices = ServiceController.GetServices();
                foreach (ServiceController scTemp in scServices)
                {
                    if (scTemp.ServiceName.ToUpper().Contains("MYSQL") && scTemp.ServiceName.ToUpper() != MYSERVICE_NAME.ToUpper())
                    {
                        using (RegistryKey regKey1 = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\services\\" + scTemp.ServiceName))
                        {
                            if (regKey1.GetValue("ImagePath") != null)
                            {
                                servicepath = regKey1.GetValue("ImagePath").ToString();
                                servicepath = servicepath.Replace("\"", "");
                            }
                        }

                        if (servicepath.IndexOf("mysqld") > 0)
                            servicepath = servicepath.Substring(0, servicepath.IndexOf("mysqld") - 1);

                        if (System.IO.Directory.Exists(servicepath))
                        {
                            string serviceexe = System.IO.Path.Combine(servicepath, "mysqld.exe");
                            if (System.IO.File.Exists(serviceexe))
                            {
                                var versionInfo = FileVersionInfo.GetVersionInfo(serviceexe);
                                if (versionInfo.ProductVersion != null && versionInfo.ProductVersion.StartsWith("5.6"))
                                {
                                    isalreadyserviceexists = true;
                                    MYSQL_BUILD_IN_SERVICE_EXISTS = true;
                                    WriteLog("Found alreay mysql 5.6 service is running" + scTemp.Status.ToString());
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                WriteLog("Error in already service is running " + err.Message);
            }
            WriteLog("Ended:IsMySQL5_6_Service_Exists");
            return isalreadyserviceexists;
        }

        /// <summary>
        ///  This method is used to check and retrn MySQLacperp service installed and status
        /// 
        ///  1. Check acperp.ini
        ///  2. if exits, then check MySQLAcperp service in services list
        ///  3. if exits, then get its status and port on running
        ///  4. Assign all these details into global variables to show in form
        /// </summary>
        /// <returns></returns>
        public static bool IsMyServiceInstalled()
        {
            string installedPath = string.Empty;
            bool isMyServiceInstlled = false;
            string acperpini = string.Empty;
            WriteLog("Start:IsMyServiceInstalled");

            string mysqlmypath = General.GetMyDataPath();

            if (mysqlmypath != string.Empty && !General.ACMEERP_FOUND_OLD_INSTALLATION)
            {
                string myinipath = Path.Combine(mysqlmypath, MYSERVICE_INI_FILE);

                bool ismysqlacperpFound = false;
                string mysqlacperpstatus = string.Empty;

                MYSQL_IS_INSTALLED = true;
                MYSQL_INSTALL_PATH = mysqlmypath;
                MY_DATA_PATH = Path.Combine(mysqlmypath, "DATA");

                ServiceController[] scServices = ServiceController.GetServices();
                foreach (ServiceController scTemp in scServices)
                {
                    if (scTemp.ServiceName.ToUpper() == MYSERVICE_NAME.ToUpper())
                    {
                        ismysqlacperpFound = true;
                        if (scTemp.Status == ServiceControllerStatus.Running)
                        {
                            MYSERVICE_IS_RUNNING = true;
                        }
                        MYSERVICE_IS_INSTALLED = true;
                        isMyServiceInstlled = true;
                        break;
                    }
                }

                MySQL_PORT = "-1";
                if (ismysqlacperpFound && File.Exists(myinipath))
                {
                    string portnumber = string.Empty;
                    using (StreamReader sr = new StreamReader(myinipath))
                    {
                        acperpini = sr.ReadToEnd();
                        int portindex = acperpini.IndexOf("|");
                        if (portindex >= 0)
                        {
                            portnumber = acperpini.Substring(portindex + 1, 4);
                        }
                    }

                    int port = 0;
                    if (int.TryParse(portnumber, out port))
                    {
                        MySQL_PORT = port.ToString();
                        WriteLog("my service is installed :: " + mysqlacperpstatus);
                    }
                }
            }

            if (mysqlmypath != string.Empty && General.ACMEERP_FOUND_OLD_INSTALLATION) //for already old installed (old logic)
            {
                MY_DATA_PATH = mysqlmypath;
                isMyServiceInstlled = IsOLDMySQLACPERPServiceInstalled();
            }

            //If datapath is not found in registry, but service is avilabled in services list
            //so stop service and remove it
            if (String.IsNullOrEmpty(mysqlmypath) && !isMyServiceInstlled)
            {
                RemoveMyService();
            }

            WriteLog("Ended:IsMyServiceInstalled");
            return isMyServiceInstlled;
        }


        /// <summary>
        /// This method is used to get my service details if and if only acperp/aod service is already installed by old setup logic
        /// </summary>
        /// <returns></returns>
        public static bool IsOLDMySQLACPERPServiceInstalled()
        {
            string mysqlpath = string.Empty;
            string servicepath = string.Empty;
            bool isMySQLACPERPInstlled = false;
            string acperpini = string.Empty;
            WriteLog("Start:IsMySQLACPERPInstalled");
            if (MY_DATA_PATH != string.Empty)
            {
                string acperpinipath = Path.Combine(Path.GetDirectoryName(MY_DATA_PATH), "myacperp.ini");

                bool ismysqlacperpFound = false;
                string mysqlacperpstatus = string.Empty;
                ServiceController[] scServices = ServiceController.GetServices();
                foreach (ServiceController scTemp in scServices)
                {
                    if (scTemp.ServiceName.ToUpper() == ACMEERP_MYSERVICE_NAME.ToUpper())
                    {
                        ismysqlacperpFound = true;
                        if (scTemp.Status == ServiceControllerStatus.Running)
                        {
                            MYSERVICE_IS_RUNNING = true;
                        }

                        using (RegistryKey regKey1 = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\services\\" + scTemp.ServiceName))
                        {
                            if (regKey1.GetValue("ImagePath") != null)
                            {
                                servicepath = regKey1.GetValue("ImagePath").ToString();
                                servicepath = servicepath.Replace("\"", "");
                            }
                        }

                        if (servicepath.IndexOf("mysqld") > 0)
                            servicepath = servicepath.Substring(0, servicepath.IndexOf("mysqld") - 1);

                        if (servicepath.IndexOf("bin") > 0)
                        {
                            string serviceexe = System.IO.Path.Combine(servicepath, "mysqld.exe");
                            if (System.IO.File.Exists(serviceexe))
                            {
                                mysqlpath = servicepath.Substring(0, servicepath.LastIndexOf("bin"));
                            }
                        }

                        isMySQLACPERPInstlled = true;
                        MYSERVICE_IS_INSTALLED = true;

                        MYSQL_INSTALL_PATH = mysqlpath;
                        MYSQL_IS_INSTALLED = true;
                        break;
                    }
                }

                MySQL_PORT = "-1";
                if (ismysqlacperpFound && File.Exists(acperpinipath))
                {
                    string portnumber = string.Empty;
                    using (StreamReader sr = new StreamReader(acperpinipath))
                    {
                        acperpini = sr.ReadToEnd();
                        Int32 portindex = acperpini.IndexOf("|");
                        if (portindex >= 0)
                        {
                            portnumber = acperpini.Substring(portindex + 1, 4);
                        }
                    }

                    int port = 0;
                    if (int.TryParse(portnumber, out port))
                    {
                        MySQL_PORT = port.ToString();
                        WriteLog("mysqlacperp is installed :: " + mysqlacperpstatus);
                    }
                }
                else //Acmeerp old installation, path is avilable in registery but service is not there, so treat as new installation with new logic
                {
                    ACMEERP_FOUND_OLD_INSTALLATION = false;
                }

            }
            WriteLog("Ended:IsMySQLacperpInstalled");
            return isMySQLACPERPInstlled;
        }

        /// <summary>
        /// This method is used to change status of My service
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public static bool ChangeMyServiceStatus(bool start)
        {
            bool Rtn = false;
            WriteLog("Start::ChangeMySQLMyServiceStatus");
            try
            {
                ServiceController[] scServices = ServiceController.GetServices();
                foreach (ServiceController scTemp in scServices)
                {
                    if (scTemp.ServiceName.ToUpper() == MYSERVICE_NAME.ToUpper())
                    {
                        Rtn = true;
                        WriteLog("MySQLAcperp found");
                        if (start) //Start Service
                        {
                            if (scTemp.Status != ServiceControllerStatus.Running)
                            {
                                scTemp.Start();
                                TimeSpan timeout = TimeSpan.FromMilliseconds(5000);
                                scTemp.WaitForStatus(ServiceControllerStatus.Running, timeout);
                            }
                            WriteLog("My Service is Started..");
                        }
                        else //Stop service
                        {
                            if (scTemp.Status == ServiceControllerStatus.Running) scTemp.Stop();
                            WriteLog("My Service is Stopped..");
                        }
                        break;
                    }
                }
            }
            catch (Exception err)
            {
                WriteLog("Error in chaninging My service status " + err.Message);
            }
            WriteLog("Ended::ChangeMySQLMyServiceStatus");
            return Rtn;
        }

        /// <summary>
        /// To Get My service status
        /// </summary>
        /// <returns></returns>
        public static ServiceControllerStatus GetMyServiceStatus()
        {
            ServiceControllerStatus Rtn = ServiceControllerStatus.Stopped;
            WriteLog("Start::GetMySQLMyServiceStatus");
            try
            {
                ServiceController[] scServices = ServiceController.GetServices();

                foreach (ServiceController scTemp in scServices)
                {
                    if (scTemp.ServiceName.ToUpper() == MYSERVICE_NAME.ToUpper())
                    {
                        if (scTemp.Status != ServiceControllerStatus.Running)
                        {
                            scTemp.Start();
                            TimeSpan timeout = TimeSpan.FromMilliseconds(6000);
                            scTemp.WaitForStatus(ServiceControllerStatus.Running);
                        }
                        Rtn = scTemp.Status;
                        break;
                    }
                }
            }
            catch (Exception err)
            {
                WriteLog("Error in getting My service status " + err.Message);
            }
            WriteLog("Ended::GetMySQLMyServiceStatus");
            return Rtn;
        }

        /// <summary>
        /// This mehtod is used to get My Service path, it will be stored at first time when service installed,
        /// next time...installer will point to actual path (to retain databbase)
        /// 
        /// OLD logic was to store datapath alone in "datapath" registry and mysql path would be default path (c:\mysql 5.6) or already other 5.6 path
        /// so if already acmerp or aod service is installed by old logic it will take old existing acme.epr/aod service 
        /// </summary>
        /// <returns></returns>
        public static string GetMyDataPath()
        {
            string mysqlpath = string.Empty;
            mysqlpath = GetRegistry(Registry.LocalMachine, @"Software\" + MYSERVICE_NAME, "path");

            //This is only for acme.erp for already installed places---------------------------------------------------------------------------
            if (!string.IsNullOrEmpty(mysqlpath))
            {
                DirectoryInfo dirMysql = new DirectoryInfo(Path.Combine(mysqlpath, @"DATA\MySQL"));
                if (!dirMysql.Exists && !File.Exists(Path.Combine(mysqlpath, @"BIN\MySQLD.exe")))
                {
                    mysqlpath = string.Empty;
                }
            }
            //On 30/09/2024 To Handle, OLD Acmeerp installation with myql installation / new installation without mysql installation
            /*else if (MYSERVICE_NAME.ToUpper() == ACMEERP_MYSERVICE_NAME.ToUpper()) //Only for acmerp
            {
                mysqlpath = GetRegistry(Registry.LocalMachine, @"Software\" + MYSERVICE_NAME, "datapath");
                DirectoryInfo dirMysql = new DirectoryInfo(Path.Combine(mysqlpath, @"MySQL"));
                ACMEERP_FOUND_OLD_INSTALLATION = dirMysql.Exists;
            }*/

            if (string.IsNullOrEmpty(mysqlpath) && MYSERVICE_NAME.ToUpper() == ACMEERP_MYSERVICE_NAME.ToUpper()) //Only for acmerp
            {
                mysqlpath = GetRegistry(Registry.LocalMachine, @"Software\" + MYSERVICE_NAME, "datapath");
                DirectoryInfo dirMysql = new DirectoryInfo(Path.Combine(mysqlpath, @"MySQL"));
                ACMEERP_FOUND_OLD_INSTALLATION = dirMysql.Exists;
            }

            //-----------------------------------------------------------------------------------------------------------------------------------

            return mysqlpath;
        }

        /// <summary>
        /// Get Branch code from licence key
        /// </summary>
        /// <param name="licencekay"></param>
        /// <returns></returns>
        public static string GetLicBranchCode(string licencekay)
        {
            DataTable dtLicenseInfo = new DataTable();
            string BranchOfficeCode = string.Empty;
            try
            {
                if (File.Exists(licencekay))
                {
                    DataSet dsLicenseInfo = XMLConverter.ConvertXMLToDataSet(licencekay);
                    if (dsLicenseInfo != null && dsLicenseInfo.Tables.Count > 0)
                    {
                        dtLicenseInfo = dsLicenseInfo.Tables["LicenseKey"];
                        if (dtLicenseInfo != null && dtLicenseInfo.Rows.Count > 0)
                        {
                            DataRow drLicense = dtLicenseInfo.Rows[0];
                            BranchOfficeCode = objdec.DecryptString(drLicense["BRANCH_OFFICE_CODE"].ToString());
                        }
                    }
                    dsLicenseInfo.Clear();
                    dsLicenseInfo = null;
                }
            }
            catch (Exception ex)
            {
                General.WriteLog("Could not read License Info");
            }
            finally { }
            return BranchOfficeCode;
        }

        /// <summary>
        /// Get Institute Name from licence key
        /// </summary>
        /// <param name="licencekay"></param>
        /// <returns></returns>
        public static string GetLicInstituteName(string licencekay)
        {
            DataTable dtLicenseInfo = new DataTable();
            string HeadOfficeCode = string.Empty;
            try
            {
                if (File.Exists(licencekay))
                {
                    DataSet dsLicenseInfo = XMLConverter.ConvertXMLToDataSet(licencekay);
                    if (dsLicenseInfo != null && dsLicenseInfo.Tables.Count > 0)
                    {
                        dtLicenseInfo = dsLicenseInfo.Tables["LicenseKey"];
                        if (dtLicenseInfo != null && dtLicenseInfo.Rows.Count > 0)
                        {
                            DataRow drLicense = dtLicenseInfo.Rows[0];
                            HeadOfficeCode = objdec.DecryptString(drLicense["InstituteName"].ToString());
                        }
                    }
                    dsLicenseInfo.Clear();
                    dsLicenseInfo = null;
                }
            }
            catch (Exception ex)
            {
                General.WriteLog("Could not read License Info");
            }
            finally { }
            return HeadOfficeCode;
        }

        /// <summary>
        /// This method is initialize method.
        /// This Method is used to Initialize MySQL configuration, 1. create new instance 2. use existing mysql connection
        ///Set our own customzied application properties
        /// 
        /// Based on the user choice installation mode will change SERVER to SERVER_NEW_INSTANCE
        /// 
        /// 1.If installtion mode is Server/Server_new_instnace, 
        /// 2. it will check whether myservice is installed, it means, server mode is already installed
        ///     and mysql path and its properties will be assigned.
        /// 3. If myservice is not installed, check mysql 5.6 is already running
        /// 4. If Mysql 5.6 is already running, will ask confirmation whether going to use existing mysql connection or wnat to create new instance
        /// 5. Based on the user choice installation mode will change SERVER to SERVER_NEW_INSTANCE
        /// 6. Assign MySQL installed path and its properties
        /// </summary>
        public static void Initialize_MySQLConfigure_Settings()
        {
            //Installation mode is server, check mysql instance is already running, 
            //If mysql service is already running, Get confirmation from user to use existing mysql authendication or create new mysql service instance
            if (General.enInstallationMode == General.InstallationMode.SERVER ||
                General.enInstallationMode == General.InstallationMode.SERVER_NEW_INSTANCE)
            {
                if (!IsMyServiceInstalled())
                {
                    if (IsMySQL5_6_Service_Exists())
                    {
                        if (CAN_ASK_CONFIRMATION_TO_USE_EXISTING_MYSQL)
                        {
                            DialogResult dialogresule = MessageBox.Show("MySQL 5.6 is already running in this system, Do you want to create New MySQL Instance for "
                                + General.MYAPPLICATION + " ?", General.Title, MessageBoxButtons.YesNo);

                            if (dialogresule == System.Windows.Forms.DialogResult.Yes)
                            {
                                MYSQL_IS_INSTALLED = true;
                                MYSQL_INSTALL_PATH = Path.Combine(Path.GetPathRoot(General.TARGET_PATH), General.MYSERVICE_NAME);

                                enInstallationMode = InstallationMode.SERVER_NEW_INSTANCE;
                            }
                        }
                        else
                        {
                            enInstallationMode = InstallationMode.SERVER_NEW_INSTANCE;
                        }
                    }
                    else
                    {
                        General.enInstallationMode = InstallationMode.SERVER_NEW_INSTANCE;
                    }
                }
                else
                {
                    General.enInstallationMode = InstallationMode.SERVER_NEW_INSTANCE;
                }
            }
        }

        /// <summary>
        /// This method is called from unistall method from instlaler class,
        /// unistall aod setup
        /// </summary>
        /// <param name="productcode"></param>
        /// <returns></returns>
        public static bool UninstallMyApplication(string productcode)
        {
            bool rtn = false;
            try
            {
                if (productcode != string.Empty)
                {
                    using (Process p = new Process())
                    {
                        p.StartInfo.RedirectStandardInput = true;
                        p.StartInfo.RedirectStandardOutput = true;
                        p.StartInfo.UseShellExecute = false;
                        p.StartInfo.CreateNoWindow = false;
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        p.StartInfo.FileName = "msiexec";
                        p.StartInfo.Arguments = "/x " + productcode;

                        p.Start();
                        p.WaitForExit();
                    }


                    rtn = true;
                }
            }
            catch (Exception err)
            {
                rtn = false;
                WriteLog("Not found in UninstallMyApplication " + err.Message);
            }

            return rtn;
        }

        #region Local Methods

        /// <summary>
        /// This method is used to Create My Service 
        /// 1. Copy MySQL based folder to MySQL path
        /// 2. Add execptions in firewall (Acpp.exe, mysql port, mysqld.exe)
        /// 3. Create acmperp.ini file
        /// 4. By using dos command with help of base mysqld exe (command: MySQLpath\bin\mysqld.exe --inistlal servicename --default-file
        /// 5. Start service
        /// </summary>
        /// <returns></returns>
        private static bool CreateMyService()
        {
            bool Rtn = false;
            string ServiceStatus = string.Empty;
            WriteLog("Start:ConfigureMySQLMyService");
            try
            {
                //Copy MySQL base folder from setup to mysql path
                if (CreateMySQLBaseFiles())
                {
                    string mysqldexepath = MYSQL_INSTALL_PATH + @"\bin\mysqld.exe";
                    if (File.Exists(mysqldexepath))
                    {
                        //Add port into firewall exception ---------------------------------------------------------------
                        //port and application
                        using (FirewallHelper1 firewall = new FirewallHelper1())
                        {
                            string myexe = Path.Combine(TARGET_PATH, MYAPPLICATION_EXE_FILE);
                            firewall.AddExecptionApplication(MYAPPLICATION, myexe); //my application exe
                            firewall.AddExecptionApplication(MYSERVICE_NAME + "EXE", mysqldexepath); //service mysqld.exe
                            firewall.AddExecptionPort(MYSERVICE_NAME, Convert.ToInt32(MySQL_PORT)); //service port
                        }
                        //--------------------------------------------------------------------------------------------------------

                        //Copy my.ini to mysql installed path (datapath)
                        if (CreateMySQLMYINI())
                        {
                            string inipath = Path.Combine(Path.GetDirectoryName(MY_DATA_PATH), MYSERVICE_INI_FILE);
                            using (Process p = new Process())
                            {
                                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                p.StartInfo.CreateNoWindow = true;
                                p.StartInfo.RedirectStandardInput = true;
                                p.StartInfo.RedirectStandardOutput = true;
                                p.StartInfo.UseShellExecute = false; //required to redirect
                                string MyService = "\"" + MYSQL_INSTALL_PATH + @"\bin\mysqld" + "\""
                                    + " --install " + MYSERVICE_NAME + " --defaults-file=" + "\"" + inipath + "\"";

                                MyService = MyService.Replace(@"\", @"/");
                                p.StartInfo.FileName = "cmd.exe ";
                                p.Start();
                                //p.WaitForExit();
                                using (System.IO.StreamReader SR = p.StandardOutput)
                                {
                                    using (System.IO.StreamWriter SW = p.StandardInput)
                                    {
                                        //mysqld –console
                                        SW.WriteLine(MyService);
                                        SW.WriteLine("sc start " + MYSERVICE_NAME);
                                        SW.WriteLine("exit");
                                        ServiceStatus = SR.ReadToEnd();
                                    }
                                }
                            }
                        }
                        else
                        {
                            WriteLog("Problem in creating ini file");
                        }
                    }
                    else
                    {
                        WriteLog("My Service could not find MySQL Installed Path");
                    }
                }
                else
                {
                    WriteLog("Problem in creating mysql setup base files");
                }
            }
            catch (Exception err)
            {
                WriteLog("Error in Configuring My Service " + err.Message);
            }

            if (ServiceStatus.ToUpper().IndexOf("SERVICE SUCCESSFULLY INSTALLED") >= 0)
            {
                WriteLog("My Service successfully installed");
                ChangeMyServiceStatus(false);
                ChangeMyServiceStatus(true);
                Rtn = true;
            }
            else if (ServiceStatus.ToUpper().IndexOf("THE SERVICE ALREADY EXISTS") >= 0)
            {
                WriteLog("The service already exists");
                //Restart service
                ChangeMyServiceStatus(false);
                ChangeMyServiceStatus(true);
                Rtn = true;
            }
            else
            {
                WriteLog("Error in configuring service " + ServiceStatus);
                Rtn = false;
            }

            if (Rtn)
            {
                System.Threading.Thread.Sleep(6000); //60000
                ServiceControllerStatus servicestatus = GetMyServiceStatus();

                //On 30/09/2024, Try Final one
                if (servicestatus != ServiceControllerStatus.Running)
                {
                    //MessageRender.ShowMessage("Trying to Restart the Service once....");
                    ChangeMyServiceStatus(true);
                    servicestatus = GetMyServiceStatus();
                }
                
                if (servicestatus == ServiceControllerStatus.Running)
                {
                    Rtn = true;
                    WriteLog("My service is configured, it is running");
                }
                else if (servicestatus != ServiceControllerStatus.Running)
                {
                    Rtn = false;
                    WriteLog("My service is configured, but not yet running");

                }
            }

            MYSERVICE_IS_RUNNING = Rtn;
            WriteLog("Ended:ConfigureMySQLMyService");
            return Rtn;
        }

        /// <summary>
        /// This method is used to checkport
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        private static bool isPortUsing(int port)
        {
            bool bUse = false;
            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();

            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    bUse = true;
                    break;
                }
            }
            return bUse;
        }

        /// <summary>
        /// This method is used to copy MySQL base folder, this mehtod will be called when installation mode server with new instance
        /// this folder contains the following mysql folders and files
        /// PreCondition : if mysql(DB) folder should not be there othereise it will overwritte base files 
        /// Not need to copy base mysql setupfiles for every time if service is not there
        /// 
        /// 1. Default mysql system folders mysql db and files
        /// 2. Root password is fixed (our own password) and it has remote access rights
        /// 3. copy mysql base folder from setup to MySQL path which is user defined
        /// </summary>
        /// <returns></returns>
        private static bool CreateMySQLBaseFiles()
        {
            bool Rtn = false;
            WriteLog("Start:CreateMySQLBaseFiles");
            try
            {
                //Copy all mysql setup files to my datapth
                DirectoryInfo mysqlsetpsource = new DirectoryInfo(Path.Combine(TARGET_PATH, "MySQLSetup"));
                DirectoryInfo mysqlDefaultDBpath = new DirectoryInfo(Path.Combine(MYSQL_INSTALL_PATH, @"data\mysql"));
                DirectoryInfo mysqlpath = new DirectoryInfo(Path.Combine(MYSQL_INSTALL_PATH));

                if (!mysqlDefaultDBpath.Exists)
                {
                    if (!mysqlpath.Exists)
                    {
                        mysqlpath.Create();
                        mysqlpath.Refresh();
                    }

                    //Copy MySQL default folders
                    if (mysqlsetpsource.Exists)
                    {
                        if (mysqlpath.Exists)
                        {
                            CopyAll(mysqlsetpsource, mysqlpath);
                        }
                        Rtn = true;
                        WriteLog("End:copying my.ini to mysql path");
                    }
                    else
                    {
                        WriteLog("Could not find mysql folder in mysql defualt datapath");
                    }
                }
                else
                {
                    Rtn = true;
                }

                //Store My Service datapath into registry for future reference
                if (Rtn)
                {
                    if (SaveRegistryKey(Registry.LocalMachine, @"Software", MYSERVICE_NAME))
                    {
                        if (SaveRegistryValue(Registry.LocalMachine, @"Software\" + MYSERVICE_NAME, "path", MYSQL_INSTALL_PATH))
                        {
                            //If Acmeerp, if we install new logic, remove old datapath from registry (it will make confusion)
                            if (MYSERVICE_NAME == ACMEERP_MYSERVICE_NAME)
                            {
                                if (RemoveRegistryKeyValue(Registry.LocalMachine, @"Software\" + MYSERVICE_NAME, "datapath"))
                                {
                                    WriteLog("removed old datapath from registry");
                                }
                            }
                            WriteLog("Created my datapath in registry");
                        }
                    }

                }
            }
            catch (Exception err)
            {
                WriteLog("Error in copying mysql base files : CreateMySQLBaseFiles " + err.Message);
            }
            WriteLog("End:CreateMySQLBaseFiles");
            return Rtn;
        }

        /// <summary>
        /// This method is used to create ini file for mysql acperp service
        /// 
        /// 1. Create my.ini file from resource stream and replace (basedir, datapath and port)
        /// 2. Copty fixed Mysqlbase dirs from setup(mysql, performance_schema and ibdata1) (These files are fixed in setup with database password and remote rights for root user)
        /// 3. Store service datapath into registry, it will be used for next time..installer will point to this path when we install next time
        /// will take acutal database
        /// </summary>
        /// <returns></returns>
        private static bool CreateMySQLMYINI()
        {
            bool Rtn = false;
            string mysqlini = string.Empty;
            WriteLog("Start:CreateMySQLMYINI");

            try
            {
                using (Stream strmScript = Assembly.GetExecutingAssembly().GetManifestResourceStream(abyAssembly.GetName().Name + "." + MYSERVICE_INI_FILE))
                {
                    DirectoryInfo dir = new DirectoryInfo(MY_DATA_PATH);
                    string inipath = Path.Combine(Path.GetDirectoryName(MY_DATA_PATH), MYSERVICE_INI_FILE);

                    using (StreamWriter sw = new StreamWriter(inipath, false))
                    {
                        using (StreamReader sr = new StreamReader(strmScript))
                        {
                            mysqlini = sr.ReadToEnd();
                            //Change MySQL path and port in the ini file
                            mysqlini = mysqlini.Replace("<datapath>", MY_DATA_PATH);
                            mysqlini = mysqlini.Replace("<mysqlport>", MySQL_PORT);
                            mysqlini = mysqlini.Replace("<basedir>", MYSQL_INSTALL_PATH);
                            sw.Write(mysqlini);
                            Rtn = true;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Rtn = false;
                WriteLog("Error in creating my ini file " + err.Message);
            }
            WriteLog("Ended:CreateMySQLMYINI");
            return Rtn;
        }

        /// <summary>
        /// copy all files form one foelder to another folder
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        private static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            // Check if the target directory exists; if not, create it.
            if (Directory.Exists(target.FullName) == false)
            {
                Directory.CreateDirectory(target.FullName);
            }

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        /// <summary>
        /// This method is used to save value into registry
        /// </summary>
        /// <param name="regkey"></param>
        /// <param name="path"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool SaveRegistryValue(RegistryKey regkey, string path, string property, string value)
        {
            bool Rtn = false;
            RegistryKey root = regkey.OpenSubKey(path, true);
            try
            {
                if (root != null)
                {
                    root.SetValue(property, value);
                    Rtn = true;
                }
            }
            catch (Exception err)
            {
                Rtn = false;
                WriteLog("Error in creating My Service datapath " + err.Message);
            }
            return Rtn;
        }

        /// <summary>
        /// This method is used to save value into registry
        /// </summary>
        /// <param name="regkey"></param>
        /// <param name="path"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool SaveRegistryKey(RegistryKey regkey, string path, string key)
        {
            bool Rtn = false;
            RegistryKey root = regkey.OpenSubKey(path, true);
            try
            {
                if (root != null)
                {
                    root.CreateSubKey(key);
                    Rtn = true;
                }
            }
            catch (Exception err)
            {
                Rtn = false;
                General.WriteLog("Error in creating in MySQLACPERP Key" + err.Message);
            }
            return Rtn;
        }

        /// <summary>
        /// This mehtod is used to get registry value
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
                General.WriteLog("Not found in MySQLACPERP datapath " + err.Message);
            }

            return registryvalue;
        }

        /// <summary>
        /// This method is used to remove key value from registry
        /// </summary>
        /// <param name="regkey"></param>
        /// <param name="path"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static bool RemoveRegistryKeyValue(RegistryKey regkey, string path, string property)
        {
            bool Rtn = false;
            RegistryKey root = regkey.OpenSubKey(path, true);
            try
            {
                if (root != null)
                {
                    root.DeleteValue(property, false);
                    Rtn = true;
                }
            }
            catch (Exception err)
            {
                Rtn = false;
                WriteLog("Error in creating in RemoveRegistryKey" + err.Message);
            }
            return Rtn;
        }




        /// <summary>
        /// This method is used to check given path's drive is harddisk or not
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsHardDisk(string path)
        {
            bool rtn = false;
            try
            {
                string drive = Path.GetDirectoryName(path);
                DriveInfo driveinfo = new DriveInfo(drive);
                rtn = (driveinfo.DriveType == DriveType.Fixed);
            }
            catch (Exception err)
            {
                WriteLog("IsHardDisk " + path + " " + err.Message);
            }
            return rtn;
        }

        #endregion

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

        public static string Encrypt(string EncryptString)
        {
            try
            {
                SimpleEncrypt.SimpleEncDec objDec = new SimpleEncrypt.SimpleEncDec();
                if (!string.IsNullOrEmpty(EncryptString))
                {
                    EncryptString = objDec.EncryptString(EncryptString);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
            return EncryptString;
        }

        #endregion
    }
}