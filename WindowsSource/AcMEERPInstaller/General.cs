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

namespace AcMEERPInstaller
{
    public static class General
    {
        public enum InstallationMode
        {
            Server,
            Client
        }

        public static Assembly abyAssembly = System.Reflection.Assembly.GetExecutingAssembly();
        
        private static SimpleEncrypt.SimpleEncDec objdec = new SimpleEncrypt.SimpleEncDec();
        public static bool FROM_INSTALLTION_KIT = false;
        public static string ACPERP_Title = "Acme.erp Database Configuration";
        public static string ACPERP_SETUP_PATH = string.Empty;
        public static string ACPERP_TARGET_PATH = string.Empty;
        public static string ACPERP_INSTALLER_LOG = Path.Combine(ACPERP_TARGET_PATH, "acperpinstaller.txt");
        public static string ACPERP_LIC_PATH = string.Empty;
        public static InstallationMode enInstallationMode = InstallationMode.Client;

        public static string MYSQL_DEFAULT_CONNECTION = string.Empty;
        public static string MYSQL_ACPERP_CONNECTION = string.Empty;

        public static bool MYSQL_IS_INSTALLED = false;
        public static string MYSQL_INSTALL_DEFAULT_PATH = @"C:/MySQL 5.6/"; //MySQL 5.6\";
        public static string MYSQL_INSTALL_PATH = MYSQL_INSTALL_DEFAULT_PATH;
        public static string MySQL_HOSTNAME = "localhost";
        public static string MySQL_PORT = "3320";
        public static string MySQL_ROOT_USERNAME = "root";
        public static string MySQL_ROOT_PASSWORD = "acperproot";

        public static bool MySQLACPERPSERVICE_IS_INSTALLED = false;
        public static bool MySQLACPERPSERVICE_IS_RUNNING = false;
        public static string MySQLACPERP_SERVICE_NAME = "MySQLACPERP";
        public static string MySQLACPERP_DATA_PATH = string.Empty;

        public static string MySQL_ACPERP_DBNAME = "acperp";
        public static string MySQL_ACPERP_USERNAME = "acperp";
        public static string MySQL_ACPERP_PASSWORD = "acperp";

        public static bool ACMEERP_DB_CONFIGURED = false;
        public static bool LIC_UPDATED = false;
        public static string LIC_HEAD_OFFICE_CODE = "sdbinm";
        public static string LIC_BRANCH_OFFICE_CODE = "sdbinmdbcylg";

        public static string RETURN_MSG = string.Empty;

        public static string MySQLACPERP_SERVICE_INI_FILE = "myacperp.ini";
        public static string ACPERP_CONFIG_FILE = "ACPP.exe.config";

        /// <summary>
        /// Write exection and error log
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteLog(string msg)
        {
            try
            {
                string logpath = ACPERP_INSTALLER_LOG;
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
                throw new Exception("Error in writing Acme.erp installer log " + ex.Message);
            }
        }
        
        /// <summary>
        /// Clear log file
        /// </summary>
        public static void ClearLog()
        {
            try
            {

                string logpath = ACPERP_INSTALLER_LOG;
                if (System.IO.File.Exists(logpath))
                {
                    using (var fs = new FileStream(logpath, FileMode.Truncate))
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in writing Acme.erp installer log " + ex.Message);
            }
        }
        
        /// <summary>
        /// This method is used to configure MySQLACPER service and Restore Acperp database
        /// </summary>
        /// <returns></returns>
        public static bool ConfigureACPERPMySQL()
        {
            bool Rtn = false;
            bool mysqlconfigured = false;
            try
            {
                General.WriteLog("Start:ConfigureACPERPMySQL");
                if (!General.MySQLACPERPSERVICE_IS_INSTALLED)
                {
                    if (General.MYSQL_IS_INSTALLED)
                    {
                        if (ConfigureMySQLACPERPService())
                        {
                            //-----------------------------------------------------------------------------------------
                            //WE fixed mysql authendication and remote rights in mysql folder itself in setup project, so we have not updated password and rights
                            //General.SetRootPassword();
                            //if (UpdateMySQLAuthentication())
                            //{
                            //    mysqlconfigured = true;
                            //}
                            //----------------------------------------------------------------------------------------
                            mysqlconfigured = true;
                        }
                    }
                }
                else
                {
                    if (!General.MySQLACPERPSERVICE_IS_RUNNING)
                    {
                        mysqlconfigured = General.ChangeMySQLACPERPServiceStatus(true);
                    }
                    else
                    {
                        mysqlconfigured = true;
                    }

                }

                if (mysqlconfigured)
                {
                    using (DBHandler dbh = new DBHandler())
                    {
                        if (dbh.TestConnection(true))
                        {
                            if (dbh.RestoreACPERPdatabase())
                            {
                                if (CreateACPERPconfig()) //Create ACPERP Config file with connection string
                                {
                                    dbh.UpdateACPERPDBchanges();
                                    Rtn = true;
                                }
                            }
                            else
                                Rtn = false;
                        }
                    }
                }
                else
                {
                    General.WriteLog("ConfigureACPERPMySQL: MySQLACPERP service is not configured, DB would not be created");
                }
            }
            catch (Exception er)
            {
                Rtn = false;
                General.WriteLog("Error in ConfigureACPERPMySQL, " + er.Message);
            }
            General.WriteLog("Ended:ConfigureACPERPMySQL");
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
            General.WriteLog("Start:IsMySQLPortAvilable");
            try
            {
                if (MySQLPort >= 1025 && MySQLPort <= 5000)
                {
                    for (int checkport = MySQLPort; checkport <= 5000; checkport++)
                    {
                        if (!isPortUsing(checkport))
                        {
                            avilablePort = checkport;
                            General.MySQL_PORT = avilablePort.ToString();
                            General.WriteLog("Found free port");
                            break;
                        }
                    }
                }
                else
                {
                    avilablePort = 0;
                    MessageBox.Show("MySQL Port should be between 1025 and 5000", General.ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception err)
            {
                //MessageBox.Show("Error in checking valid port", General.ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                General.WriteLog("Error in checking valid port " + err.Message);
            }
            General.WriteLog("Ended:IsMySQLPortAvilable");
            return avilablePort;
        }

        /// <summary>
        /// This method is used to create Acme.erp, config file
        /// Replace ths following tags in app.config file with values
        /// 1. Replace("<localhost>", General.MySQL_HOSTNAME);
        /// 2. Replace("<mysqlport>", General.MySQL_PORT)
        /// 3. Replace("<username>", General.MySQL_ROOT_USERNAME);
        /// 4. Replace("<userpassword>", General.MySQL_ROOT_PASSWORD);
        /// 5. Encrypt connection string in app.cong file (optional)
        /// </summary>
        /// <returns></returns>
        public static bool CreateACPERPconfig()
        {
            bool Rtn = false;
            string acperpconfig = string.Empty;
            string acperpConnectionstring = string.Empty;
            string mysqlConnectionString = string.Empty;
            General.WriteLog("Start:CreateACPERPconfig");
            try
            {
                using (DBHandler dbh = new DBHandler())
                {
                    if (dbh.TestConnection(false))
                    {
                        using (Stream strmScript = Assembly.GetExecutingAssembly().GetManifestResourceStream(abyAssembly.GetName().Name + "." + General.ACPERP_CONFIG_FILE))
                        {
                            using (StreamWriter sw = new StreamWriter(General.ACPERP_TARGET_PATH + "\\" + General.ACPERP_CONFIG_FILE, false))
                            {
                                using (StreamReader sr = new StreamReader(strmScript))
                                {
                                    acperpconfig = sr.ReadToEnd();

                                    acperpConnectionstring = "server=" + General.MySQL_HOSTNAME + "; port=" + General.MySQL_PORT + "; database=acperp; uid=" + General.MySQL_ROOT_USERNAME + ";pwd=" + General.MySQL_ROOT_PASSWORD + "; charset=utf8; pooling=false";
                                    mysqlConnectionString = "server=" + General.MySQL_HOSTNAME + "; port=" + General.MySQL_PORT + "; database=mysql; uid=" + General.MySQL_ROOT_USERNAME + ";pwd=" + General.MySQL_ROOT_PASSWORD + "; charset=utf8; pooling=false";

                                    acperpconfig = acperpconfig.Replace("<acperpconnection>", acperpConnectionstring); // General.Encrypt(Connectionstring));
                                    acperpconfig = acperpconfig.Replace("<mysqlconnection>", mysqlConnectionString); // General.Encrypt(mysqlConnectionString));

                                    // acperpconfig = acperpconfig.Replace("<localhost>", General.MySQL_HOSTNAME);
                                    // acperpconfig = acperpconfig.Replace("<mysqlport>", General.MySQL_PORT);
                                    // acperpconfig = acperpconfig.Replace("<username>", General.MySQL_ROOT_USERNAME);
                                    // acperpconfig = acperpconfig.Replace("<userpassword>", General.MySQL_ROOT_PASSWORD);

                                    sw.Write(acperpconfig);
                                    Rtn = true;
                                    General.WriteLog("End:Create acperp config file");
                                }
                            }
                        }

                        //Encrypting config file
                        //bool bresult = General.EncryptConnectionSettings(Path.Combine(General.ACPERP_TARGET_PATH, General.ACPERP_CONFIG_FILE), "connectionStrings");
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
                        //MessageBox.Show("ACPERP server is not installed properly.", General.ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        General.WriteLog("ACPERP server is not installed properly. Connection failed.");
                    }
                }
            }
            catch (Exception err)
            {
                General.WriteLog("Error in creating ACPERP config " + err.Message);
            }
            General.WriteLog("Ended:CreateACPERPconfig");
            return Rtn;
        }

        /// <summary>
        /// This method is used to detele mysqlacperp service
        /// </summary>
        /// <returns></returns>
        public static bool RemoveMySQLACPERPService()
        {
            bool bRtn = false;
            string ServiceStatus = string.Empty;
            General.WriteLog("Start:RemoveMySQLACPERPService");
            try
            {
                if (General.IsMySQLACPERPServiceInstalled())
                {
                    using (Process p = new Process())
                    {
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        p.StartInfo.CreateNoWindow = true;
                        p.StartInfo.RedirectStandardInput = true;
                        p.StartInfo.RedirectStandardOutput = true;
                        p.StartInfo.UseShellExecute = false; //required to redirect
                        string MySQLACPERPService = "SC DELETE " + General.MySQLACPERP_SERVICE_NAME;
                        p.StartInfo.FileName = "cmd.exe ";
                        p.Start();
                        using (System.IO.StreamReader SR = p.StandardOutput)
                        {
                            using (System.IO.StreamWriter SW = p.StandardInput)
                            {
                                //mysqld –console
                                SW.WriteLine(MySQLACPERPService);
                                SW.WriteLine("exit");
                                ServiceStatus = SR.ReadToEnd();
                            }
                            //DeleteService SUCCESS
                            if (ServiceStatus.ToUpper().IndexOf("DeleteService SUCCESS") >= 0)
                            {
                                bRtn = true;
                                General.WriteLog("Sucessfully mysqlacp removed :: " + ServiceStatus);
                            }
                            else
                            {
                                General.WriteLog("Sucessfully mysqlacperp is not removed :: " + ServiceStatus);
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
                General.WriteLog("Error in removing mysqlacperp service " + err.Message);
            }
            General.WriteLog("Ended:RemoveMySQLACPERPService");
            return bRtn;
        }

        /// <summary>
        /// This method is used to create ini file for mysql acperp service
        /// 
        /// 1. Create acperp.ini file from resource stream and replace (basedir, datapath and port)
        /// 2. Copty fixed Mysqlbase dirs from setup(mysql, performance_schema and ibdata1) (These files are fixed in setup with database password and remote rights for root user)
        /// 3. Store service datapath into registry, it will be used for next time..installer will point to this path when we install next time
        /// will take acutal database
        /// </summary>
        /// <returns></returns>
        private static bool CreateMySQLACPERPINI()
        {
            bool Rtn = false;
            string mysqlini = string.Empty;
            General.WriteLog("Start:CreateMySQLACPERPINI");
            string installedpathdrive = Path.GetPathRoot(General.ACPERP_TARGET_PATH);

            //Copy mysql db from default datapath to acmeerp datapath
            DirectoryInfo mysqlsource = new DirectoryInfo(Path.Combine(General.ACPERP_TARGET_PATH, "mysql56 data//mysql"));
            DirectoryInfo mysqlacperpdatapth = new DirectoryInfo(Path.Combine(General.MySQLACPERP_DATA_PATH, "mysql"));
            //Copy performance_schema db from default datapath to acmeerp datapath
            DirectoryInfo performance_schemasource = new DirectoryInfo(Path.Combine(General.ACPERP_TARGET_PATH, "mysql56 data//performance_schema"));
            DirectoryInfo performance_schemaacmeerpdatapth = new DirectoryInfo(Path.Combine(General.MySQLACPERP_DATA_PATH, "performance_schema"));

            if (!mysqlacperpdatapth.Exists)
            {
                mysqlacperpdatapth.Create();
            }

            try
            {
                using (Stream strmScript = Assembly.GetExecutingAssembly().GetManifestResourceStream(abyAssembly.GetName().Name + "." + General.MySQLACPERP_SERVICE_INI_FILE))
                {
                    //using (StreamWriter sw = new StreamWriter(General.MYSQL_INSTALL_PATH + "\\myacperp.ini", false))
                    DirectoryInfo dir = new DirectoryInfo(General.MySQLACPERP_DATA_PATH);
                    string inipath = Path.Combine(Path.GetDirectoryName(General.MySQLACPERP_DATA_PATH), General.MySQLACPERP_SERVICE_INI_FILE);

                    using (StreamWriter sw = new StreamWriter(inipath, false))
                    {
                        using (StreamReader sr = new StreamReader(strmScript))
                        {
                            mysqlini = sr.ReadToEnd();
                            //Change MySQL path and port in the ini file
                            mysqlini = mysqlini.Replace("<datapath>", General.MySQLACPERP_DATA_PATH);
                            mysqlini = mysqlini.Replace("<mysqlport>", General.MySQL_PORT);
                            mysqlini = mysqlini.Replace("<basedir>", General.MYSQL_INSTALL_PATH);

                            sw.Write(mysqlini);

                            //Copy MySQL default folders
                            if (mysqlsource.Exists)
                            {
                                if (!mysqlacperpdatapth.Exists) //if not there mysql folder in acmerp data path
                                {
                                    CopyAll(mysqlsource, mysqlacperpdatapth);//mydql db
                                    CopyAll(performance_schemasource, performance_schemaacmeerpdatapth); //performance_schema db 
                                    //copy ibdata1
                                    if (File.Exists(Path.Combine(General.ACPERP_TARGET_PATH, "mysql56 data//ibdata1")))
                                    {
                                        File.Copy(Path.Combine(General.ACPERP_TARGET_PATH, "mysql56 data//ibdata1"), Path.Combine(General.MySQLACPERP_DATA_PATH, "ibdata1"), true);
                                    }
                                }
                                Rtn = true;
                                General.WriteLog("End:copying my.ini to mysql path");

                                //Store MySQLACPERP datapath into registry for future reference
                                if (General.SaveRegistryKey(Registry.LocalMachine, @"Software", General.MySQLACPERP_SERVICE_NAME))
                                {
                                    if (General.SaveRegistryValue(Registry.LocalMachine, @"Software\" + General.MySQLACPERP_SERVICE_NAME, "datapath", General.MySQLACPERP_DATA_PATH))
                                    {
                                        General.WriteLog("Created MySQLACPERP datapath in registry");
                                    }
                                }
                            }
                            else
                            {
                                General.WriteLog("Could not find mysql folder in mysql defualt datapath");
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                General.WriteLog("Error in creating ACPERPini " + err.Message);
            }
            General.WriteLog("Ended:CreateMySQLACPERPINI");
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
        /// Set defual and acperp conneciton string wwith username and password
        /// </summary>
        public static void SetMySQLConnectionString()
        {

            General.MYSQL_DEFAULT_CONNECTION = "server=" + General.MySQL_HOSTNAME + ";port=" +
                                             General.MySQL_PORT + ";user id=" + General.MySQL_ROOT_USERNAME + "; password=" +
                                             General.MySQL_ROOT_PASSWORD + "; database=mysql; charset=utf8; pooling=true;Connect Timeout=600;";
            General.MYSQL_ACPERP_CONNECTION = "server=" + General.MySQL_HOSTNAME + ";port=" +
                                            General.MySQL_PORT + ";user id=" + General.MySQL_ROOT_USERNAME + "; password=" +
                                            General.MySQL_ROOT_PASSWORD + "; database=" + General.MySQL_ACPERP_DBNAME + "; charset=utf8; pooling=true;Connect Timeout=600;";
        }

        /// <summary>
        /// This method is used to get MySQL Installed Path, if mysql is already installed or it will return String.Empty
        /// 
        /// CASE 1:
        /// 1. Get Services list and check its name with MySQL.
        /// 2. If Name of the service contains "MySQL" and check its foder version and FileVersionInfo is 5.6
        /// 3. If equals to 5.6, it means mysql 5.6 is exists in the system
        /// 4. Assign mysql installed path, this will be used treated as base path..using this path, we have to create mysqlacperp service
        /// 5. case 1 is used to get already mysql 5.6 installed path
        /// 
        /// CASE 2:
        /// if mysql service is not there, Prereqiest might have installed mysql5.6.msi to default path (C:\MySQL 5.6) before we run this setup
        /// 1. so we fix mysql installed path as  (C:\MySQL 5.6) 
        /// 2. check mysql.exe is exists in default path (C:\MySQL 5.6) 
        /// 3. If not there, Prereqiest might not have installed property
        /// 4. so alert message that "Prereqiest might not have installed " and exit
        /// </summary>
        /// <param name="c_name"></param>
        /// <returns></returns>
        public static bool IsMySQL5_6Installed()
        {
            string installedPath = string.Empty;
            bool IsMySQLInstlled = false;
            string servicepath = string.Empty;
            General.WriteLog("Start:IsMySQL5_6Installed");
            try
            {
                //CASE 1:
                ServiceController sc = new ServiceController("SareeManagerNotifications");
                ServiceController[] scServices;
                scServices = ServiceController.GetServices();
                foreach (ServiceController scTemp in scServices)
                {
                    if (scTemp.ServiceName.ToUpper().Contains("MYSQL"))
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
                                    if (servicepath.IndexOf("bin") > 0)
                                    {
                                        General.MYSQL_INSTALL_PATH = servicepath.Substring(0, servicepath.LastIndexOf("bin"));
                                        IsMySQLInstlled = true;
                                        General.MYSQL_IS_INSTALLED = true;
                                        General.WriteLog("MySQL 5.6 is found: " + scTemp.Status.ToString());
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }

                //CASE 2:
                //If not found in service, AcMEERP prerequist would have handled and installed mysql 5.6
                //installed path is "C:\MySQL5.6\"
                if (General.FROM_INSTALLTION_KIT)
                {
                    if (!IsMySQLInstlled)
                    {
                        
                        if (File.Exists(General.MYSQL_INSTALL_PATH + @"\bin\mysqld.exe"))
                        {
                            General.MYSQL_INSTALL_PATH = General.MYSQL_INSTALL_DEFAULT_PATH;
                            IsMySQLInstlled = true;
                            General.MYSQL_IS_INSTALLED = true;
                            General.WriteLog("MySQL 5.6 is found");
                        }
                        else
                        {
                            General.MYSQL_INSTALL_PATH = string.Empty;
                            IsMySQLInstlled = false;
                            General.MYSQL_IS_INSTALLED = false;
                            General.WriteLog("MySQL 5.6 is not found");
                        }
                    }
                }

                

            }
            catch (Exception err)
            {
                General.WriteLog("Error in checking mysql " + err.Message);
            }
            if (!IsMySQLInstlled)
            {
                General.WriteLog("MySQL 5.6 is not found");
            }
            General.WriteLog("Ended:IsMySQL5_6Installed");
            return IsMySQLInstlled;
        }
        
        /// <summary>
        ///  This method is used to check and retrn MySQLacperp service installed and status
        ///  1. Check acperp.ini
        ///  2. if exits, then check MySQLAcperp service in services list
        ///  3. if exits, then get its status and port on running
        ///  4. Assign all these details in to global variables to show in form
        /// </summary>
        /// <returns></returns>
        public static bool IsMySQLACPERPServiceInstalled()
        {
            string installedPath = string.Empty;
            bool isMySQLACPERPInstlled = false;
            string acperpini = string.Empty;
            General.WriteLog("Start:IsMySQLACPERPInstalled");

            if (General.MySQLACPERP_DATA_PATH != string.Empty)
            {
                string acperpinipath = Path.Combine(Path.GetDirectoryName(General.MySQLACPERP_DATA_PATH), General.MySQLACPERP_SERVICE_INI_FILE);
                if (File.Exists(acperpinipath))
                {
                    bool ismysqlacperpFound = false;
                    string mysqlacperpstatus = string.Empty;
                    ServiceController[] scServices = ServiceController.GetServices();
                    foreach (ServiceController scTemp in scServices)
                    {
                        if (scTemp.ServiceName == General.MySQLACPERP_SERVICE_NAME)
                        {
                            ismysqlacperpFound = true;
                            if (scTemp.Status == ServiceControllerStatus.Running)
                            {
                                MySQLACPERPSERVICE_IS_RUNNING = true;
                            }
                            break;
                        }
                    }

                    if (ismysqlacperpFound)
                    {
                        string portnumber = string.Empty;
                        using (StreamReader sr = new StreamReader(acperpinipath))
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
                            General.MySQL_PORT = port.ToString();
                            isMySQLACPERPInstlled = true;
                            MySQLACPERPSERVICE_IS_INSTALLED = true;
                            General.WriteLog("mysqlacperp is installed :: " + mysqlacperpstatus);
                        }
                    }
                }
            }
            General.WriteLog("Ended:IsMySQLacperpInstalled");
            return isMySQLACPERPInstlled;
        }

        /// <summary>
        /// This method is used to change status of acme.erp service
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public static bool ChangeMySQLACPERPServiceStatus(bool start)
        {
            bool Rtn = false;
            General.WriteLog("Start::ChangeMySQLacperpServiceStatus");
            try
            {
                ServiceController[] scServices = ServiceController.GetServices();
                foreach (ServiceController scTemp in scServices)
                {
                    if (scTemp.ServiceName == General.MySQLACPERP_SERVICE_NAME)
                    {
                        Rtn = true;
                        General.WriteLog("MySQLAcperp found");
                        if (start) //Start Service
                        {
                            if (scTemp.Status != ServiceControllerStatus.Running)
                            {
                                scTemp.Start();
                                TimeSpan timeout = TimeSpan.FromMilliseconds(5000);
                                scTemp.WaitForStatus(ServiceControllerStatus.Running, timeout);
                            }
                            General.WriteLog("MySQLAcperp is Started..");
                        }
                        else //Stop service
                        {
                            if (scTemp.Status == ServiceControllerStatus.Running) scTemp.Stop();
                            General.WriteLog("MySQLAcperp is Stopped..");
                        }
                        break;
                    }
                }
            }
            catch (Exception err)
            {
                General.WriteLog("Error in chaninging MySQLAcperp service status " + err.Message);
            }
            General.WriteLog("Ended::ChangeMySQLacperpServiceStatus");
            return Rtn;
        }

        /// <summary>
        /// To Get acme.erp service status
        /// </summary>
        /// <returns></returns>
        public static ServiceControllerStatus GetMySQLACPERPServiceStatus()
        {
            ServiceControllerStatus Rtn = ServiceControllerStatus.Stopped;
            General.WriteLog("Start::GetMySQLACPERPServiceStatus");
            try
            {
                ServiceController[] scServices = ServiceController.GetServices();

                foreach (ServiceController scTemp in scServices)
                {
                    if (scTemp.ServiceName == General.MySQLACPERP_SERVICE_NAME)
                    {
                        if (scTemp.Status != ServiceControllerStatus.Running)
                        {
                            TimeSpan timeout = TimeSpan.FromMilliseconds(6000);
                            scTemp.WaitForStatus(ServiceControllerStatus.Running, timeout);
                        }
                        Rtn = scTemp.Status;
                        break;
                    }
                }
            }
            catch (Exception err)
            {
                General.WriteLog("Error in getting MySQLACPERP service status " + err.Message);
            }
            General.WriteLog("Ended::GetMySQLACPERPServiceStatus");
            return Rtn;
        }

        /// <summary>
        /// This method is used to save value into registry
        /// </summary>
        /// <param name="regkey"></param>
        /// <param name="path"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SaveRegistryValue(RegistryKey regkey, string path, string property, string value)
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
                General.WriteLog("Error in creating MySQLACPERP datapath " + err.Message);
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
        /// This mehtod is used to get MySQLACPERP datapath, it will be stored at first time when service installed,
        /// next time...installer will point to actual datapath
        /// </summary>
        /// <returns></returns>
        public static string GetAcMEERPDataPath()
        {
            string datapath = string.Empty;
            datapath = General.GetRegistry(Registry.LocalMachine, @"Software\" + General.MySQLACPERP_SERVICE_NAME, "datapath");
            
            DirectoryInfo dirDirector = new DirectoryInfo(Path.Combine(datapath, "MySQL"));

            if (!dirDirector.Exists)
            {
                datapath = string.Empty;
            }
            return datapath;
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
        /// This method is called from unistall method from instlaler class,
        /// unistall acmerp setup
        /// </summary>
        /// <param name="productcode"></param>
        /// <returns></returns>
        public static bool UninstallAcMEERP(string productcode)
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
                General.WriteLog("Not found in MySQLACPERP datapath " + err.Message);
            }

            return rtn;
        }

        /// <summary>
        /// This method is used to Create MySQLACPERP Service 
        /// 1. Add execptions in firewall (Acpp.exe, mysql port, mysqld.exe)
        /// 2. Create acmperp.ini file
        /// 3. By using dos command with help of base mysqld exe (command: MySQLpath\bin\mysqld.exe --inistlal servicename --default-file
        /// 4. Start service
        /// </summary>
        /// <returns></returns>
        private static bool ConfigureMySQLACPERPService()
        {
            bool Rtn = false;
            string ServiceStatus = string.Empty;
            General.WriteLog("Start:ConfigureMySQLACPERPService");
            try
            {
                string mysqldexepath = General.MYSQL_INSTALL_PATH + @"\bin\mysqld.exe";
                if (File.Exists(mysqldexepath))
                {
                    //Add port into firewall exception
                    using (FirewallHelper firewall = new FirewallHelper())
                    {
                        string acmerpexe = Path.Combine(General.ACPERP_TARGET_PATH, "ACPP.exe");
                        firewall.AddExecptionPort(General.MySQLACPERP_SERVICE_NAME, Convert.ToInt32(General.MySQL_PORT));
                        firewall.AddExecptionApplication("Acme.erp", acmerpexe);
                        firewall.AddExecptionApplication(General.MySQLACPERP_SERVICE_NAME + "EXE", mysqldexepath);
                    }

                    //Copy my.ini to mysql installed path (datapath)
                    CreateMySQLACPERPINI();
                    string inipath = Path.Combine(Path.GetDirectoryName(General.MySQLACPERP_DATA_PATH), General.MySQLACPERP_SERVICE_INI_FILE);
                    using (Process p = new Process())
                    {
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        p.StartInfo.CreateNoWindow = true;
                        p.StartInfo.RedirectStandardInput = true;
                        p.StartInfo.RedirectStandardOutput = true;
                        p.StartInfo.UseShellExecute = false; //required to redirect
                        string MySQLACPERPService = "\"" + General.MYSQL_INSTALL_PATH + @"\bin\mysqld" + "\""
                            + " --install " + General.MySQLACPERP_SERVICE_NAME + " --defaults-file=" + "\"" + inipath + "\"";

                        MySQLACPERPService = MySQLACPERPService.Replace(@"\", @"/");
                        p.StartInfo.FileName = "cmd.exe ";
                        p.Start();
                        //p.WaitForExit();
                        using (System.IO.StreamReader SR = p.StandardOutput)
                        {
                            using (System.IO.StreamWriter SW = p.StandardInput)
                            {
                                //mysqld –console
                                SW.WriteLine(MySQLACPERPService);
                                SW.WriteLine("sc start " + General.MySQLACPERP_SERVICE_NAME);
                                SW.WriteLine("exit");
                                ServiceStatus = SR.ReadToEnd();
                            }
                        }
                    }
                }
                else
                {
                    General.WriteLog("ACPERP Service could not find MySQL Installed Path");
                }
            }
            catch (Exception err)
            {
                General.WriteLog("Error in Configuring MySQLACPERP Service " + err.Message);
            }

            if (ServiceStatus.ToUpper().IndexOf("SERVICE SUCCESSFULLY INSTALLED") >= 0)
            {
                General.WriteLog("MySQLACPERP Service successfully installed");
                Rtn = true;
            }
            else if (ServiceStatus.ToUpper().IndexOf("THE SERVICE ALREADY EXISTS") >= 0)
            {
                General.WriteLog("The service already exists");
                //Restart service
                General.ChangeMySQLACPERPServiceStatus(false);
                General.ChangeMySQLACPERPServiceStatus(true);
                Rtn = true;
            }
            else
            {
                General.WriteLog("Error in configuring service " + ServiceStatus);
                Rtn = false;
            }

            if (Rtn)
            {
                System.Threading.Thread.Sleep(6000);

                ServiceControllerStatus servicestatus = General.GetMySQLACPERPServiceStatus();
                if (servicestatus == ServiceControllerStatus.Running)
                {
                    Rtn = true;
                    General.WriteLog("MySQLACPERP service is configured, it is running");
                }
                else if (servicestatus != ServiceControllerStatus.Running)
                {
                    Rtn = false;
                    General.WriteLog("MySQLACPERP service is configured, but not yet running");
                }
            }

            General.MySQLACPERPSERVICE_IS_RUNNING = Rtn;
            General.WriteLog("Ended:ConfigureMySQLACPERPService");
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