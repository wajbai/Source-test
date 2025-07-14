using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AcMEDSync;
using System.IO;
using Bosco.Utility;
using AcMEDSync.Model;

namespace AcMEDS
{
    public partial class frmTestVoucher : Form
    {
        private string FileName { get; set; }
        public frmTestVoucher()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DoDataSync();
        }

        private void DoDataSync()
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
                                        if (!resulArg.Success)
                                        {
                                            if (!string.IsNullOrEmpty(Common.DataSyncFailedVouchersPath))
                                            {
                                                if (!Directory.Exists(Common.DataSyncFailedVouchersPath))
                                                {
                                                    Directory.CreateDirectory(Common.DataSyncFailedVouchersPath);
                                                }

                                                string destFile = System.IO.Path.Combine(Common.DataSyncFailedVouchersPath, Path.GetFileName(file));

                                                File.Copy(file, destFile, true);
                                            }
                                        }
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
