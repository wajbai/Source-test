using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;
using System.ServiceProcess;
using System.IO;

namespace AcMEERPPrerequisites
{   
    class AcMEERPPrerequisites
    {
       static int Main()
       {
            int rtn = 0; //MySQL 5.6 is not installed
            try
            {
                if (IsMySQL5_6Installed())
                {
                    rtn = 1;    //MySQL 5.6 is already installed
                }
            }
            catch (Exception errr)
            {
                rtn = 0; //MySQL 5.6 is not installed
            }
            return rtn;
        }
        
    /// <summary>
    /// This method is used to get MySQL Installed Path, if mysql is already installed or it will return STring.Empty
    /// </summary>
    /// <param name="c_name"></param>
    /// <returns></returns>
       static bool IsMySQL5_6Installed()
       {
           string installedPath = string.Empty;
           bool IsMySQLInstlled = false;
           string servicepath = string.Empty;
           try
           {
               //ServiceController sc = new ServiceController("SareeManagerNotifications");
               ServiceController[] scServices;
               scServices = ServiceController.GetServices();
               foreach (ServiceController scTemp in scServices)
               {
                   if (scTemp.ServiceName.ToUpper().Contains("MYSQL"))
                   {
                       using (RegistryKey regKey1 = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\services\\" + scTemp.ServiceName))
                       {
                           if (regKey1.GetValue("ImagePath") != null)
                               servicepath = regKey1.GetValue("ImagePath").ToString();
                       }

                       if (servicepath.IndexOf("mysqld") > 0)
                           servicepath = servicepath.Substring(1, servicepath.IndexOf("mysqld") - 1);

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
                                       IsMySQLInstlled = true;
                                   }
                                   break;
                               }
                           }
                       }
                   }

               }
           }
           catch (Exception err)
           {
               IsMySQLInstlled = false;
           }
           return IsMySQLInstlled;
       }
    }
}
