using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;

using System.Windows.Forms;
using AcMEDSync;
using System.Threading;
using System.Reflection;
using System.IO;
using Bosco.Utility;

namespace AcMEDS
{
    public partial class AcMEDS : ServiceBase
    {
        private System.Timers.Timer tmrDataTransfer = new System.Timers.Timer();
        private bool isDataTransferProgress = false;

        public AcMEDS()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            CallSynchronization();
        }

        protected override void OnStop()
        {
            tmrDataTransfer.Stop();
            tmrDataTransfer.Enabled = false;
        }

        public void CallSynchronization()
        {
            tmrDataTransfer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            int interval = Common.AcMEDataSyncInterval;
            tmrDataTransfer.Interval = interval * 60000; // convert to seconds
            tmrDataTransfer.Enabled = true;
            tmrDataTransfer.Start();
            ProcessStart();
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            ProcessStart();
        }

        private void ProcessStart()
        {
            try
            {
                if (!isDataTransferProgress)
                {
                    isDataTransferProgress = true;
                    AcMEDataSynLog.WriteLog("Synchronization started to watch Data Sync folder: " + Common.DataSyncLocation);
                    DataTransfer();//Data Transfer
                    isDataTransferProgress = false;
                }
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog("Error in Process Start. " + ex.ToString());
            }
        }

        private void DataTransfer()
        {
            try
            {
                string datasyncLocation = Common.DataSyncLocation;
                if (!string.IsNullOrEmpty(datasyncLocation))
                {
                    if (Directory.Exists(datasyncLocation))
                    {
                        string[] fileVoucherXML = Directory.GetFiles(datasyncLocation, "*.xml", SearchOption.AllDirectories);

                        if (fileVoucherXML.Count() > 0)
                        {
                            foreach (string file in fileVoucherXML)
                            {
                                AcMEDataSynLog.WriteLog("--------------------------------XML FILE INFO-----------------------------------");
                                if (File.Exists(file))
                                {
                                    if (!Common.FileInUse(file))
                                    {
                                        AcMEDataSynLog.WriteLog("File Name : " + file);
                                        AcMEDataSynLog.WriteLog("-------------------------------------------------------------------------------------------------------------------------------");
                                        IAcMEDataSyn dataSynVoucher = new AcMEDataSyn();
                                        ResultArgs resulArg = dataSynVoucher.SynchronizeVouchers(file);
                                        //if (!resulArg.Success)
                                        //{
                                        //    if (!Common.FileInUse(file))
                                        //    {
                                        //        if (!string.IsNullOrEmpty(Common.DataSyncFailedVouchersPath))
                                        //        {
                                        //            if (!Directory.Exists(Common.DataSyncFailedVouchersPath))
                                        //            {
                                        //                Directory.CreateDirectory(Common.DataSyncFailedVouchersPath);
                                        //            }

                                        //            string destFile = System.IO.Path.Combine(Common.DataSyncFailedVouchersPath, file);

                                        //            File.Copy(file, destFile, true);
                                        //        }
                                        //    }
                                        //}
                                        File.Delete(file);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Directory.CreateDirectory(datasyncLocation);
                    }
                }
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog(ex.Message);
            }
        }
    }
}
