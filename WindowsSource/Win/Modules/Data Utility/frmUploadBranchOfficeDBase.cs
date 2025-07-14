//1. On 25/05/2017 modified by alwar, fixed few issues like 
// "AcpERPUpload" is creating multi-times , problem in taking latest backup db to upload portal
//Added logic to add location based...if current license is multi location upload branch db name is based on location

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Utility;
using Bosco.DAO;
using System.IO;
using System.IO.Compression;
using System.Configuration;
using Bosco.Utility.ConfigSetting;
using Bosco.Model.Setting;


namespace ACPP.Modules.Data_Utility
{
    public partial class frmUploadBranchOfficeDBase : frmFinanceBaseAdd
    {
        #region Decelaration
        
        string localUploadBackupName = string.Empty;
        string MyDBUploadPath = Path.Combine(SettingProperty.ApplicationStartUpPath, "AcpERPUpload");
        
        #endregion

        #region Constructor
        public frmUploadBranchOfficeDBase()
        {
            InitializeComponent();
            pgUploadDatabase.Visible = false;
            if (!Directory.Exists(MyDBUploadPath))
            {
                Directory.CreateDirectory(MyDBUploadPath);
            }
        }
        #endregion

        #region Events

        private void frmUploadBranchOfficeDBase_Load(object sender, EventArgs e)
        {
            this.Text = "Upload Database Backup to Acme.erp Portal";
            lblUploadStatus.Text = " ";
            lblInfoMessage.Text = "'" + this.AppSetting.BranchUploadDBName + "' database backup will be uploaded to Acmeerp portal" ;
            pgUploadDatabase.Visible = false;
        }

        /// <summary>
        /// Upload branch office to database to head office based on the branch office code.
        /// 
        /// 1. Take DB back in upload path in local ()
        /// 2. Compress
        /// 3. Upload compressed db to Acmeerp.org portal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUploadDB_Click(object sender, EventArgs e)
        {
            BackupAndRestore BackRestore = new BackupAndRestore();
            ResultArgs resultArgs = new ResultArgs();
            string dbBackupServerFileName = string.Empty;
            //Get backup dbnames based on location
            localUploadBackupName = this.AppSetting.BranchUploadDBName + ".sql";
             
            try
            {
                if (!AppSetting.IsSplitPreviousYearAcmeerpDB)
                {
                    //On 04/09/2019, to check corrupted db or not
                    if (!BackRestore.IsDBCorrupted())
                    {
                        if (this.ShowConfirmationMessage("Are you sure to upload Database backup to Acmeerp portal",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            this.Cursor = Cursors.WaitCursor;
                            lblUploadStatus.Text = " ";
                            if (this.CheckForInternetConnection())
                            {
                                if (!this.IsLicenseKeyMismatchedByLicenseKeyDBLocation())
                                {
                                    if (!this.IsLicenseKeyMismatchedByHoProjects()) //On 30/05/2019
                                    {
                                        this.Cursor = Cursors.WaitCursor;
                                        resultArgs = TakeDatabaseBackupInUploadPath();
                                        if (resultArgs.Success)
                                        {
                                            //CAll backup uploader, this will compress and upload current dtatbase to portal
                                            using (BackupAndRestore dbBackupUpload = new BackupAndRestore())
                                            {
                                                dbBackupUpload.OnProgress += new EventHandler<ProgressStatusEventArgs>(dbBackupUpload_OnProgress);
                                                resultArgs = dbBackupUpload.UploadDatabaseToPortal(localUploadBackupName);
                                                this.Cursor = Cursors.Default;
                                                if (resultArgs != null && resultArgs.Success)
                                                {
                                                    string LastDBUploadedOn = this.AppSetting.DBUploadedOn;

                                                    //1. Get Server Date
                                                    using (AcMEERPFTP acmeerpftp = new AcMEERPFTP())
                                                    {
                                                        resultArgs = acmeerpftp.GetAcMEERPServerDateTime();
                                                    }
                                                    if (resultArgs.Success)
                                                    {
                                                        string serverdatetime = resultArgs.ReturnValue.ToString();

                                                        //2. Update current date and time
                                                        using (UISetting uisetting = new UISetting())
                                                        {
                                                            resultArgs = uisetting.SaveSettingDetails(Setting.DBUploadedOn.ToString(), serverdatetime, this.ADMIN_USER_DEFAULT_ID);
                                                            if (resultArgs.Success)
                                                            {
                                                                this.AppSetting.DBUploadedOn = serverdatetime;
                                                                foreach (Form frm in Application.OpenForms)
                                                                {
                                                                    if (frm.Name == typeof(frmLoginDashboard).Name)
                                                                    {
                                                                        ((frmLoginDashboard)frm).CheckAlerts();
                                                                        break;
                                                                    }
                                                                }
                                                                resultArgs.Success = true;
                                                                lblUploadStatus.Text = this.GetMessage(MessageCatalog.Master.DataUtilityForms.DATABASE_HAS_BEEN_UPLOADED_SUCCESS);
                                                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.DataUtilityForms.DATABASE_HAS_BEEN_UPLOADED_SUCCESS));
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        //If failied show message
                                        if (!resultArgs.Success)
                                        {
                                            lblUploadStatus.Text = this.GetMessage(MessageCatalog.Master.DataUtilityForms.ERROR_UPLOADING_DATABASE);
                                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.DataUtilityForms.ERROR_UPLOADING_DATABASE) + " " + resultArgs.Message);
                                            AcMELog.WriteLog("Manual Upload DB " + resultArgs.Message);
                                        }
                                    }
                                    else
                                    {
                                        this.ShowMessageBox("Acme.erp License key and Database are mismatching (Branch Office Projects and Head Office Projects are mismatching)");
                                    }
                                }
                                else
                                {
                                    this.ShowMessageBox("License key Location(s) and Database Location are mismatching, Check your License Key");
                                }
                            }
                            else
                            {
                                this.Cursor = Cursors.Default;
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.DataUtilityForms.CHECK_INTERNET_FTP_CONNECTION));
                            }
                        }
                    }
                    else
                    {
                        MessageRender.ShowMessage(BackupAndRestore.BACKUP_CORRUPTION_MESSAGE);
                    }
                }
                else
                {
                    MessageRender.ShowMessage("This is previous year data, you are not allowed to Upload Database to Acme.erp portal");
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { this.Cursor = Cursors.Default; }
        }

        private void dbBackupUpload_OnProgress(object sender, ProgressStatusEventArgs e)
        {
            lblUploadStatus.AllowHtmlStringInCaption = true;
            lblUploadStatus.Text = e.Status;
            pgUploadDatabase.Properties.Maximum = this.UtilityMember.NumberSet.ToInteger(e.FileLength.ToString());
            pgUploadDatabase.Properties.Step = 2048; //this.UtilityMember.NumberSet.ToInteger(e.ByteSent);
            pgUploadDatabase.PerformStep();
        }
        #endregion

        #region Common Methods
        /// <summary>
        /// Take Database backup into application upload path and compress it as .gz
        /// </summary>
        /// <returns></returns>
        private ResultArgs TakeDatabaseBackupInUploadPath()
        {
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                using (BackupAndRestore dbBackup = new BackupAndRestore())
                {
                    //1. Take Backup current databse into local upload path AcpERPUpload
                    Application.DoEvents();
                    lblUploadStatus.Text = "Taking Backup...";
                    Application.DoEvents();
                    string currentdbname = dbBackup.GetCurrentDatabaseName();
                    if (!string.IsNullOrEmpty(currentdbname))
                    {
                        resultArgs = dbBackup.MySqlBackup("", currentdbname, 2);
                        if (resultArgs != null && resultArgs.Success)
                        {
                            localUploadBackupName = Path.Combine(MyDBUploadPath, localUploadBackupName);
                            resultArgs.Success = true;
                        }
                        else
                        {
                            this.ShowMessageBox(resultArgs.Message);
                        }
                    }
                    else
                    {
                        this.ShowMessageBox("Database name is empty");
                    }
                }
            }
            catch (Exception ex)
            {
                AcMELog.WriteLog("Could not take Database Backup in local path " + ex.Message);
                this.ShowMessageBox("Could not take Database Backup in local path ");
            }
            finally { }
            return resultArgs;
        }
        #endregion
               

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        ///// <summary>
        ///// Compress the sql file into gz file.
        ///// </summary>
        ///// <param name="directorySelected"></param>
        //public bool Compress_OLD(DirectoryInfo directorySelected)
        //{
        //    bool isTrue = true;
        //    try
        //    {
        //        foreach (FileInfo filetoCompress in directorySelected.GetFiles("*.sql"))
        //        {
        //            using (FileStream orignalFileStream = filetoCompress.OpenRead())
        //            {
        //                if ((File.GetAttributes(filetoCompress.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden && filetoCompress.Extension != ".gz")
        //                {
        //                    using (FileStream CompressedFileStream = File.Create(filetoCompress.FullName + ".gz"))
        //                    {
        //                        using (GZipStream compressionStream = new GZipStream(CompressedFileStream, CompressionMode.Compress))
        //                        {
        //                            orignalFileStream.CopyTo(compressionStream);
        //                        }
        //                    }
        //                    FileInfo info = new FileInfo(MyInstallPath + "\\" + filetoCompress.Name + ".gz");
        //                    Console.WriteLine("Compressed {0} from {1} to {2} bytes.", filetoCompress.Name + ".gz", filetoCompress.Length.ToString(), info.Length.ToString());
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        isTrue = false;
        //    }
        //    finally { }
        //    return isTrue;
        //}

        ///// <summary>
        ///// This is to get recent file
        ///// </summary>
        ///// <param name="SourceFilePath"></param>
        //private bool GetRecentFile_OLD(string SourceFilePath)
        //{
        //    bool isSuccess = true;
        //    try
        //    {
        //        if (Directory.Exists(SourceFilePath))
        //        {
        //            DirectoryInfo directorySelected = new DirectoryInfo(MyInstallPath);
        //            isSuccess = Compress(directorySelected);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
        //        isSuccess = false;
        //    }
        //    finally { }
        //    return isSuccess;
        //}

        //private string GetLocalFilePath_OLD()
        //{
        //    try
        //    {
        //        if (Directory.Exists(MyInstallPath))
        //        {
        //            DirectoryInfo directorySelected = new DirectoryInfo(MyInstallPath);
        //            foreach (FileInfo filetoCompress in directorySelected.GetFiles())
        //            {
        //                if (Path.GetExtension(filetoCompress.FullName).Contains(".gz"))
        //                {
        //                    localFilePath = filetoCompress.FullName;
        //                    dbBackupServerFileName = Path.GetFileName(filetoCompress.FullName);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
        //    }
        //    finally { }
        //    return localFilePath;
        //}
    }
}