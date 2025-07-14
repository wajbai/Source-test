using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net;
using System.Reflection;

namespace Acme.erpSupport
{
    public static class AcmeerpMySQLServiceDetails
    {
        public static string MYSERVICE_NAME = "MYSQLACPERP";
        public static bool MYSERVICE_INSTALLED = false;
        public static bool MYSERVICE_IS_RUNNING = false;
        public static string MYSERVICE_INI_PATH = string.Empty;
        public static Int32 MYSERVICE_PORT = -1;
        public static string MYSQL_INSTALLED_PATH = string.Empty;
        public static string MY_DATA_PATH = string.Empty;
        public static string MYSERVICE_INI_FILE = "My.ini";

        public static string MYAPPLICATION = "Acme.erp";                            //Customize:: Name of the application 
        public static string MY_DBNAME = "acperp";                                  //Customize:: Name of the application databasename
        public static string MY_BASE_TABLENAME = "voucher_master_trans";            //Customize:: Name of the application table, to
        public static string MYAPPLICATION_EXE_FILE = "ACPP.exe";                   //Customize:: Name of the application exe
        public static string MYAPPLICATION_EXE_CONFIG_FILE = "ACPP.exe.config";     //Customize:: Name of the application config file
        
        public static ServiceControllerStatus MYSERVICE_STATUS = new ServiceControllerStatus();
        public static Assembly abyAssembly = System.Reflection.Assembly.GetExecutingAssembly();

        /// <summary>
        ///  This method is used to check and retrn MySQLacperp service installed and status
        /// 
        ///  1. check MySQLAcperp service in services list
        ///  2. if exits, then get its status and port on running
        ///  3. Assign all these details into global variables to show in form
        /// </summary>
        /// <returns></returns>
        public static ResultArgs AssignMyServiceProperties()
        {
            ResultArgs result = new ResultArgs();
            
            MYSERVICE_INSTALLED = false;
            MYSERVICE_IS_RUNNING = false;
            MYSERVICE_INI_PATH = string.Empty;
            MYSERVICE_PORT = -1;
            MYSQL_INSTALLED_PATH = string.Empty;
            MYSERVICE_STATUS = ServiceControllerStatus.Stopped;

            try
            {
                ServiceController[] scServices = ServiceController.GetServices();
                foreach (ServiceController scTemp in scServices)
                {
                    if (scTemp.ServiceName.ToUpper() == MYSERVICE_NAME.ToUpper())
                    {
                        MYSERVICE_INSTALLED = true;
                        MYSERVICE_STATUS = scTemp.Status;

                        //GET ini path ("C:\Program Files\MySQL\MySQL Server 5.6\bin\mysqld" --defaults-file=C:/AcMEERP/myacperp.ini MySQLACPERP)
                        string servicepath = string.Empty;
                        using (RegistryKey regKey1 = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\services\\" + scTemp.ServiceName))
                        {
                            if (regKey1.GetValue("ImagePath") != null)
                            {
                                servicepath = regKey1.GetValue("ImagePath").ToString();
                                servicepath = servicepath.Replace("\"", "");
                            }
                        }
                        if (servicepath.IndexOf("mysqld") > 0)
                        {
                            MYSQL_INSTALLED_PATH = servicepath.Substring(0, servicepath.IndexOf("mysqld") - 1);
                            
                            string inipath = servicepath.Substring(servicepath.IndexOf("=") +1);
                            inipath = inipath.Substring(0, inipath.LastIndexOf(" ") + 1).ToString().Trim();
                            //inipath = inipath.ToUpper().Replace(MYSERVICE_NAME, string.Empty).ToLower();
                            if (File.Exists(inipath))
                            {
                                MYSERVICE_INI_PATH = inipath;
                            }
                            else
                            {
                                MYSERVICE_INI_PATH = string.Empty;
                            }
                        }

                        if (scTemp.Status == ServiceControllerStatus.Running)
                        {
                            MYSERVICE_IS_RUNNING = true;
                        }

                        break;
                    }
                }

                int port = -1;
                if (MYSERVICE_INSTALLED)
                {
                    string portnumber = string.Empty;
                    using (StreamReader sr = new StreamReader(MYSERVICE_INI_PATH))
                    {
                        string acperpini = sr.ReadToEnd();
                        int portindex = acperpini.IndexOf("|");
                        if (portindex >= 0)
                        {
                            portnumber = acperpini.Substring(portindex + 1, 4);
                        }
                    }

                    if (int.TryParse(portnumber, out port))
                    {
                        MYSERVICE_PORT = port;
                        result.Success = true;
                    }
                }
            }
            catch (Exception err)
            {
                MYSERVICE_INSTALLED = false;
                MYSERVICE_IS_RUNNING = false;
                MYSERVICE_INI_PATH = string.Empty;
                MYSERVICE_PORT = -1;
                MYSQL_INSTALLED_PATH = string.Empty;
                MYSERVICE_STATUS = ServiceControllerStatus.Stopped;
                result.Message = "Error in AssignMyServiceProperties()" + err.Message;
            }
            return result;
        }


        public static ResultArgs OpenDBConfigure()
        {
            ResultArgs result = new ResultArgs();
            try
            {
                using (Process p = new Process())
                {
                    //p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.RedirectStandardInput = true;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.UseShellExecute = false; //required to redirect
                    
                    string mysqlconfigureexe = Path.Combine(General.ACMEERP_INSTALLED_PATH, "MySQLConfigure.exe");
                    string mysqloldconfigureexe = Path.Combine(General.ACMEERP_INSTALLED_PATH, "AcMEERPInstaller.exe");

                    //AcMEERPInstaller.exe
                    mysqlconfigureexe = Path.Combine(mysqlconfigureexe);
                    
                    if (File.Exists(mysqlconfigureexe))
                    {
                        p.StartInfo.FileName = mysqlconfigureexe;
                        MessageRender.ShowMessage("\"" + General.ACMEERP_INSTALLED_PATH.TrimEnd(Path.DirectorySeparatorChar) + "\"");
                        p.StartInfo.Arguments = "\"" + General.ACMEERP_INSTALLED_PATH.TrimEnd(Path.DirectorySeparatorChar) + "\"";
                        p.Start();
                        p.WaitForExit();
                        result.Success = true;
                    }
                    else if (File.Exists(mysqloldconfigureexe))
                    {
                        p.StartInfo.FileName = mysqloldconfigureexe;
                        //p.StartInfo.Arguments = "\"" + General.ACMEERP_INSTALLED_PATH.TrimEnd(Path.DirectorySeparatorChar) + "\"";
                        p.Start();
                        p.WaitForExit();
                        result.Success = true;
                    }
                    else
                    {
                        result.Message = "MySQL Configure Application is not found";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Message = "Error in OpenDBConfigure " + ex.Message;
            }
            return result;
        }

        /// <summary>
        /// To Get My service status
        /// </summary>
        /// <returns></returns>
        public static ServiceControllerStatus GetMyServiceStatus()
        {
            ServiceControllerStatus Rtn = ServiceControllerStatus.Stopped;
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
                MessageRender.ShowMessage("Error in getting Acmeerp service status");
                Rtn = ServiceControllerStatus.Stopped;
            }
            return Rtn;
        }

        public static ResultArgs ChangeServiceStatus(bool start)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                ServiceController[] scServices = ServiceController.GetServices();
                foreach (ServiceController scTemp in scServices)
                {
                    if (scTemp.ServiceName.ToUpper() == MYSERVICE_NAME.ToUpper())
                    {
                        TimeSpan timeout = TimeSpan.FromMilliseconds(10000 *5);
                        if (start) //Start Service
                        {
                            if (scTemp.Status != ServiceControllerStatus.Running)
                            {
                                scTemp.Start();
                                scTemp.WaitForStatus(ServiceControllerStatus.Running, timeout);
                            }
                        }
                        else
                        {
                            if (MYSERVICE_STATUS == ServiceControllerStatus.Running)
                            {
                                scTemp.Stop();
                                scTemp.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                            }
                        }
                                                
                        result.Success = true;
                        break;
                    }
                }
            }
            catch (Exception err)
            {
                result.Message = err.Message;
            }
            return result;
        }

        /// <summary>
        /// This method is used to detele mysqlacperp service
        /// </summary>
        /// <returns></returns>
        public static ResultArgs RemoveMyService()
        {
            ResultArgs result = new ResultArgs();
            string ServiceStatus = string.Empty;

            try
            {
                result  = ChangeServiceStatus(false);
                if (result.Success)
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
                                //1. If Service is removed, remove value from registry tooo
                                result = RemoveRegistryKey(Registry.LocalMachine, @"Software", MYSERVICE_NAME);
                                if (result.Success)
                                {
                                    result.Success = true;
                                }
                            }
                            else
                            {
                                result.Message = "Service is not Removed, " + ServiceStatus;
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
                else
                {
                    result.Message = "Error in changing service status ";
                }
            }
            catch (Exception err)
            {
                result.Message = "Error in removing my service " + err.Message;
            }
            return result;
        }
        
        /// <summary>
        /// This method is used to save value into registry
        /// </summary>
        /// <param name="regkey"></param>
        /// <param name="path"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static ResultArgs SaveRegistryValue(RegistryKey regkey, string path, string property, string value)
        {
            ResultArgs result = new ResultArgs();
            RegistryKey root = regkey.OpenSubKey(path, true);
            try
            {
                if (root != null)
                {
                    root.SetValue(property, value);
                    result.Success = true;
                }
            }
            catch (Exception err)
            {
                result.Message = "Error in creating registry key " + err.Message;
            }
            return result;
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
        public static ResultArgs SaveRegistryKey(RegistryKey regkey, string path, string key)
        {
            ResultArgs result = new ResultArgs();
            RegistryKey root = regkey.OpenSubKey(path, true);
            try
            {
                if (root != null)
                {
                    root.CreateSubKey(key);
                    result.Success = true;
                }
            }
            catch (Exception err)
            {
                result.Message = "Error in creating in MySQLACPERP Key" + err.Message;
            }
            return result;
        }

        /// <summary>
        /// This method is used to remove from registry
        /// </summary>
        /// <param name="regkey"></param>
        /// <param name="path"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static ResultArgs RemoveRegistryKey(RegistryKey regkey, string path, string key)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                RegistryKey root = regkey.OpenSubKey(path, true);
                if (root != null)
                {
                    root.DeleteSubKey(key, false);
                    result.Success = true;
                }
            }
            catch (Exception err)
            {
                result.Message = "Error in creating in MySQLACPERP Key" + err.Message;
            }
            return result;
        }

        /// <summary>
        /// This mehtod is used to get registry value
        /// </summary>
        /// <param name="regkey"></param>
        /// <param name="path"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static ResultArgs GetRegistry(RegistryKey regkey, string path, string property)
        {
            ResultArgs result = new ResultArgs();
            string registryvalue = string.Empty;
            RegistryKey root = regkey.OpenSubKey(path, false);
            try
            {
                if (root != null)
                {
                    if (root.GetValue(property) != null)
                    {
                        registryvalue = root.GetValue(property).ToString();
                        result.Success = true;
                        result.ReturnValue = registryvalue;
                    }
                }
            }
            catch (Exception err)
            {
                result.ReturnValue = string.Empty;
                result.Message = "Not found in MySQLACPERP datapath " + err.Message;
            }
            return result;
        }


        
    }
}
