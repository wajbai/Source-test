using System;
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
using System.ServiceProcess;
using AcMEDSync;

using Bosco.Utility;


namespace AcMEDS
{
    public static class Common
    {
        public const string ACMEDS_TITLE = "AcME Data Synchronization Service";
        public const string ACMEDS_SERVICE_NAME = "AcMEDS";
        public const string ACMEDS_SERVICE_FILE = "AcMEDS.exe";
        public const string DATASYNC_FOLDER = "AcMEERP_Vouchers";
        public const int ACMEDS_SERVICE_TIMEOUT = 20000;

        private static CommonMember utilityMember = null;
        private static CommonMember UtilityMember
        {
            get
            {
                if (utilityMember == null) { utilityMember = new CommonMember(); }
                return utilityMember;
            }
        }

        public static int AcMEDataSyncInterval
        {
            get
            {
                int datasyncInterval = 0;
                if (ConfigurationManager.AppSettings["DataSyncInterval"] != null)
                {
                    datasyncInterval = UtilityMember.NumberSet.ToInteger(ConfigurationManager.AppSettings["DataSyncInterval"].ToString());
                }
                return datasyncInterval;
            }
        }

        public static string DataSyncLocation
        {
            get
            {
                string dataSyncLocation = string.Empty;
                try
                {
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["DataSyncLocation"]))
                    {
                        dataSyncLocation = Path.Combine(ConfigurationManager.AppSettings["DataSyncLocation"].ToString(), DATASYNC_FOLDER);
                    }

                    if (string.IsNullOrEmpty(dataSyncLocation))
                    {
                        dataSyncLocation = CommonMethod.GetAcMeERPInstallPath();
                        if (!string.IsNullOrEmpty(dataSyncLocation))
                        {
                            dataSyncLocation = Path.Combine(dataSyncLocation, DATASYNC_FOLDER);
                        }
                    }
                }
                catch (Exception ex)
                {
                    AcMEDataSynLog.WriteLog(ex.Message);
                }
                return dataSyncLocation;
            }
        }

        public static string DataSyncFailedVouchersPath
        {
            get
            {
                string dataSyncFailedVouchersPath = string.Empty;
                try
                {
                    if (ConfigurationManager.AppSettings["DataSyncFailedVoucherPath"] != null)
                    {
                        dataSyncFailedVouchersPath = ConfigurationManager.AppSettings["DataSyncFailedVoucherPath"];
                    }
                }
                catch (Exception ex)
                {
                    AcMEDataSynLog.WriteLog(ex.Message);
                }
                return dataSyncFailedVouchersPath;
            }
        }
        //------------------------------------------------------------------------

        public static bool FileInUse(string path)
        {
            bool CanRead = false;
            FileStream fs = null;
            try
            {
                if (fs == null)
                    fs = File.OpenRead(path);
            }
            catch (IOException ex)
            {
                CanRead = true;
            }
            finally
            {
                fs.Close();
                fs.Dispose();
                fs = null;
            }
            return CanRead;
        }

        public static bool CreateService()
        {
            bool Rtn = false;

            try
            {
                string sPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Common.ACMEDS_SERVICE_FILE);
                sPath = "\"" + sPath + "\"";
                string sCreateService = @"SC Create " + Common.ACMEDS_SERVICE_NAME + " binpath= " + sPath + " start= auto type= own type= interact error= ignore";
                ProcessStartInfo proStartInfo = new ProcessStartInfo("cmd.exe", "/c " + sCreateService);

                proStartInfo.RedirectStandardInput = true;
                proStartInfo.RedirectStandardOutput = true;
                proStartInfo.UseShellExecute = false;
                proStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proStartInfo.CreateNoWindow = true;

                Process proCreate = new Process();
                proCreate.StartInfo = proStartInfo;
                proCreate.Start();
                string result = proCreate.StandardOutput.ReadToEnd();
                if (result.IndexOf("SUCCESS") > 0)
                {
                    Rtn = true;
                }
                else
                {
                    AcMEDataSynLog.WriteLog("Service not removed " + result);
                }
            }
            catch (Exception err)
            {
                AcMEDataSynLog.WriteLog("Error in starting service " + err.Message);
                Rtn = false;
            }

            return Rtn;
        }

        public static bool StartService()
        {
            bool Rtn = false;
            try
            {
                ServiceController service = new ServiceController(Common.ACMEDS_SERVICE_NAME);
                TimeSpan timeout = TimeSpan.FromMilliseconds(Common.ACMEDS_SERVICE_TIMEOUT);

                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                Rtn = true;
            }
            catch (Exception err)
            {
                Rtn = false;
                AcMEDataSynLog.WriteLog("Error in starting Service " + err.Message);
            }
            return Rtn;
        }

        public static bool StopService()
        {
            bool Rtn = false;
            try
            {
                ServiceController service = new ServiceController(Common.ACMEDS_SERVICE_NAME);
                TimeSpan timeout = TimeSpan.FromMilliseconds(Common.ACMEDS_SERVICE_TIMEOUT);

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                Rtn = true;
            }
            catch (Exception err)
            {
                Rtn = false;
                AcMEDataSynLog.WriteLog("Error in stopping service " + err.Message);
            }
            return Rtn;
        }

        public static bool RemoveService()
        {
            bool Rtn = false;
            try
            {
                string sPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Common.ACMEDS_SERVICE_FILE);
                string sRemoveService = @"SC delete " + Common.ACMEDS_SERVICE_NAME;
                ProcessStartInfo proStartInfo = new ProcessStartInfo("cmd.exe", "/c " + sRemoveService);

                proStartInfo.RedirectStandardInput = true;
                proStartInfo.RedirectStandardOutput = true;
                proStartInfo.UseShellExecute = false;
                proStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proStartInfo.CreateNoWindow = true;

                Process proCreate = new Process();
                proCreate.StartInfo = proStartInfo;
                proCreate.Start();

                string result = proCreate.StandardOutput.ReadToEnd();
                if (result.IndexOf("SUCCESS") > 0)
                {
                    Rtn = true;
                }
                else
                {
                    AcMEDataSynLog.WriteLog("Service not removed " + result);
                }
            }
            catch (Exception err)
            {
                AcMEDataSynLog.WriteLog("Error in removing service " + err.Message);
                Rtn = false;
            }
            return Rtn;
        }
    }
}
