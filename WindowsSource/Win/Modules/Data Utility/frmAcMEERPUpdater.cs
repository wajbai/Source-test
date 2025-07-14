using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using System.IO;
using System.Diagnostics;
using System.IO.Compression;
using Bosco.Utility;
using System.Configuration;
using Bosco.Utility.ConfigSetting;
using System.Xml;
using DevExpress.XtraLayout.HitInfo;
using DevExpress.XtraLayout;

namespace ACPP.Modules.Data_Utility
{
    public partial class frmAcMEERPUpdater : frmFinanceBaseAdd
    {
        #region Declaration
        private string localversion = string.Empty;
        private string serverversion = string.Empty;
        private bool candownload = false;
        string acmerpinstalledpath = string.Empty;
        string updaterlocalpath = string.Empty;
        bool AutomcaticDownload = false;
        private string LatestVersionFeatures = string.Empty;
        #endregion

        #region Constructor
        public frmAcMEERPUpdater()
        {
            InitializeComponent();

            string inspath = Installedpath();
            updaterlocalpath = Path.Combine(inspath, "latestversion.EXE");
            // localversion = GetLocalAcMEERPVersion();
            lblDownloadStatus.Text = " ";
            // lblLocalVersion.Text = localversion;
            AutomcaticDownload = false;

            //On 15/04/2021, To have latest version features local and show in notepad
            LatestVersionFeatures = string.Empty;
        }

        public frmAcMEERPUpdater(bool automaticdownlaod = false, string latestversionfeatures= ""): this()
        {
            string inspath = Installedpath();
            updaterlocalpath = Path.Combine(inspath, "latestversion.EXE");
            // localversion = GetLocalAcMEERPVersion();
            lblDownloadStatus.Text = " ";
            // lblLocalVersion.Text = localversion;
            AutomcaticDownload = automaticdownlaod;
            
            //On 15/04/2021, To have latest version features local and show in notepad
            LatestVersionFeatures = latestversionfeatures;
        }
        #endregion

        #region Events
        private void frmAcMEERPUpdater_Load(object sender, EventArgs e)
        {
            // this.Height = 120;
            //ucProductCaption.Caption = "Acme.erp";
            ucProductCaption.Caption = this.GetMessage(MessageCatalog.Master.DataUtilityForms.COMMON_ACMEERP_CAPTION);
            lciProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //lblVersion.Text = "Version " + this.ProductVersion.ToString();
            lblVersion.Text = this.GetMessage(MessageCatalog.Master.DataUtilityForms.UPDATER_VERSION) + this.AppSetting.AcmeerpVersionFromDB; //this.ProductVersion.ToString();  

            if (AutomcaticDownload)
            {
                btnDownload.Visible = false;
                lcUpdate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void ftpClient_OnDownload(object sender, ProgressStatusEventArgs e)
        {
            //if (this.Height != 225)
            //{
            //    this.Height = 225;
            //   // lcdownload.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //}
            lblDownloadStatus.Text = e.Status;
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            StartDownload();
        }
        #endregion

        #region Methods

        private void StartDownload()
        {
            lblDownloadStatus.AppearanceItemCaption.ForeColor = Color.Blue;
            //lblDownloadStatus.Text = "Connecting to Portal...";
            lblDownloadStatus.Text = this.GetMessage(MessageCatalog.Master.DataUtilityForms.CONNECTING_TO_PORTAL_INFO);
            lciProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            Application.DoEvents();

            //On 24/07/2017, based on the setting download updater by (through)
            bool downloadbyFTP = (AppSetting.UpdaterDownloadBy == "0" || string.IsNullOrEmpty(AppSetting.UpdaterDownloadBy));
            bool internetexists = false;
            if (downloadbyFTP)
            {
                internetexists = this.CheckForInternetConnection();
            }
            else
            {
                internetexists = this.CheckForInternetConnectionhttp();
            }

            if (internetexists)
            {
                ResultArgs resultarg = new ResultArgs();
                this.Cursor = Cursors.WaitCursor;
                //string serverversion = GetAcMEERPServerVersion();
                DataSyncService.DataSynchronizerClient objservice = new DataSyncService.DataSynchronizerClient();
                serverversion = objservice.GetAcmeERPProductVersion();
                if (serverversion == this.AppSetting.AcmeerpVersionFromDB)
                {
                    if (AutomcaticDownload)
                    {
                        resultarg = InvokeDownloder(downloadbyFTP);
                    }
                    else
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Master.DataUtilityForms.ACMEERP_UP_TO_DATE_INFO), MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == System.Windows.Forms.DialogResult.Yes)
                        {
                            resultarg = InvokeDownloder(downloadbyFTP);
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                }
                else
                {
                    resultarg = InvokeDownloder(downloadbyFTP);
                }

                if (resultarg.Success)
                {
                    Application.DoEvents();
                    StartAcMEERPUpdater();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    lblDownloadStatus.AppearanceItemCaption.ForeColor = Color.Red;
                    if (downloadbyFTP)
                        lblDownloadStatus.Text = this.GetMessage(MessageCatalog.Master.DataUtilityForms.CHECK_INTERNET_FTP_CONNECTION);
                    else
                        lblDownloadStatus.Text = this.GetMessage(MessageCatalog.Common.COMMON_CHECK_INTERNET_CONNECTIVITY);

                    lciProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    AcMELog.WriteLog("Checking for the Internet Connection.. " + resultarg.Message);
                }
            }
            else
            {
                lblDownloadStatus.AppearanceItemCaption.ForeColor = Color.Red;
                //lblDownloadStatus.Text = "Unable to reach Portal. Please check your internet or FTP rights";
                if (downloadbyFTP)
                    lblDownloadStatus.Text = this.GetMessage(MessageCatalog.Master.DataUtilityForms.CHECK_INTERNET_FTP_CONNECTION);
                else
                    lblDownloadStatus.Text = this.GetMessage(MessageCatalog.Common.COMMON_CHECK_INTERNET_CONNECTIVITY);
                lciProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            this.Cursor = Cursors.Default;
        }

        private void StartAcMEERPUpdater()
        {
            try
            {
                using (Process myProcess = new Process())
                {
                    myProcess.StartInfo.UseShellExecute = false;
                    myProcess.StartInfo.FileName = updaterlocalpath;
                    myProcess.StartInfo.CreateNoWindow = false;
                    myProcess.Start();
                }
            }
            catch (Exception e)
            {
                //this.ShowMessageBox("Could not update latest version  " + e.Message);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.DataUtilityForms.COULD_NOT_UPDATE_LATEST_VERSION) + " " + e.Message);
            }
        }

        private string GetLocalAcMEERPVersion()
        {
            string Rtn = string.Empty;
            string acpexepath = Path.Combine(acmerpinstalledpath, "ACPP.exe");

            Configuration appconfig = ConfigurationManager.OpenExeConfiguration(acpexepath);

            if (appconfig.AppSettings.Settings["version"] != null)
            {
                Rtn = appconfig.AppSettings.Settings["version"].Value;
            }
            else
            {
                Rtn = "1.0.0";
            }

            return Rtn;
        }

        private void SetLocalAcMEERPVersion(string serversion)
        {
            string acpexepath = Path.Combine(acmerpinstalledpath, "ACPP.exe");
            Configuration config = ConfigurationManager.OpenExeConfiguration(acpexepath);
            config.AppSettings.Settings["version"].Value = serversion;
            config.Save();
        }

        public string Installedpath()
        {
            //acmerpinstalledpath = @"C:\Program Files (x86)\BoscoSoft\AcMEERP\";
            //if (!File.Exists(Path.Combine(acmerpinstalledpath, "ACPP.exe")))
            //    acmerpinstalledpath = @"C:\Program Files\BoscoSoft\AcMEERP\";
            acmerpinstalledpath = Application.StartupPath.ToString();
            return acmerpinstalledpath;
        }
        #endregion

        private void frmAcMEERPUpdater_Activated(object sender, EventArgs e)
        {
            if (AutomcaticDownload)
            {
                btnDownload.Visible = false;
                lcUpdate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void frmAcMEERPUpdater_Shown(object sender, EventArgs e)
        {
            //if automatic downalod, start immediately when form is shown
            if (AutomcaticDownload)
            {
                Application.DoEvents();
                StartDownload();
            }
        }

        private ResultArgs InvokeDownloder(bool ByFtp)
        {
            ResultArgs resultargs = new ResultArgs();
            lblDownloadStatus.Text = this.GetMessage(MessageCatalog.Master.DataUtilityForms.DOWNLOADING_LATEST_VERSION);
            using (AcMEERPFTP ftpClient = new AcMEERPFTP())
            {
                ftpClient.OnProgress += new EventHandler<ProgressStatusEventArgs>(ftpClient_OnDownload);
                if (ByFtp)
                    resultargs = ftpClient.download("httpdocs/Module/Software/Uploads/latestversion.EXE", updaterlocalpath);
                else
                    resultargs = ftpClient.downloadhttp("http://acmeerp.org/module/Software/Uploads/latestversion.EXE", updaterlocalpath);
                    
            }
            return resultargs;
        }

    }
}