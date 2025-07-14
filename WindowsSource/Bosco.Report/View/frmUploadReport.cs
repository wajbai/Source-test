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
using Bosco.Report.Base;
using System.Net;

namespace Bosco.Report.View
{
    public partial class frmUploadReport : Bosco.Utility.Base.frmBase
    {
        #region Decelaration
        private ReportBase ActiveReport = null;
        private SettingProperty settinguserpropertyproperty = new SettingProperty();
        private string ReportName = string.Empty;
        private string ReportDateFrom = string.Empty;
        private string ReportDateTo = string.Empty;

        public string UploadReportFileName
        {
            get
            {
                string uploadUrl = CommonMethod.RemoveSpecialCharacter(settinguserpropertyproperty.InstituteName);
                string[] branchlocations = settinguserpropertyproperty.BranchLocations.Split(',');
                if (branchlocations.GetUpperBound(0) > 0) //(this.AppSetting.MultiLocation == 1)
                {
                    uploadUrl = CommonMethod.RemoveSpecialCharacter(settinguserpropertyproperty.InstituteName) + " (" + CommonMethod.RemoveSpecialCharacter(settinguserpropertyproperty.Location) + ")";
                }

                uploadUrl += " - " + CommonMethod.RemoveSpecialCharacter(ReportName) + (rgFileType.SelectedIndex == 1 ? ".pdf" : ".xlsx");
                

                return uploadUrl;
            }
        }


        private string UploadFileURI
        {
            get
            {
                string uploadUrl = Path.Combine(ConfigurationManager.AppSettings["ftpURL"].ToString(), "");
                uploadUrl += "/Acmeerp_Branch_Reports/" + settinguserpropertyproperty.HeadofficeCode;
                uploadUrl += "/" + UtilityMember.DateSet.ToDate(ReportProperty.Current.DateFrom, false).Year.ToString();
                uploadUrl += "/" + UploadReportFileName;
                return uploadUrl;
            }
        }
        #endregion

        #region Constructor
        public frmUploadReport(ReportBase activeReport, string reportname, string reportdatefrom, string reportdateto)
        {
            InitializeComponent();
            ActiveReport = activeReport;
            ReportName = reportname;
            ReportDateFrom = reportdatefrom;
            ReportDateTo = reportdateto;
            pgUploadDatabase.Visible = false;
        }
        #endregion

        #region Events

        private void frmUploadBranchOfficeDBase_Load(object sender, EventArgs e)
        {
            this.Text = "Upload Report to Acme.erp Portal";
            rgFileType.SelectedIndex = 0;
            lblUploadStatus.Text = " ";
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
            string responsemsg = string.Empty;
            try
            {
                if (this.ShowConfirmationMessage("Are you sure to upload Report to Acmeerp portal ?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    lblUploadStatus.Text = " ";
                    string reportfyyear = UtilityMember.DateSet.ToDate(ReportDateFrom, false).Year.ToString();


                    this.Cursor = Cursors.WaitCursor;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        if (rgFileType.SelectedIndex == 0)
                            this.ActiveReport.ExportToXlsx(ms);
                        else
                            this.ActiveReport.ExportToPdf(ms);
                        using (AcMEERPFTP ftpFileTransfer = new AcMEERPFTP())
                        {
                            ResultArgs result = ftpFileTransfer.uploadDatabyWebClient(BranchUploadAction.BranchReport, this.AppSetting.HeadofficeCode, this.AppSetting.PartBranchOfficeCode,
                                    this.AppSetting.Location, UploadReportFileName, ReportName, reportfyyear, ms);
                            if (!result.Success)
                            {
                                responsemsg = result.Message;
                            }
                        }
                    }

                    /*WebClient client = new WebClient();
                    string uploadWebUrl = "http://staging.acmeerp.org/module/Software/BranchSync.aspx";
                    client.Headers.Add("HeadOfficeCode", this.AppSetting.HeadofficeCode);
                    client.Headers.Add("BranchOfficeCode", this.AppSetting.PartBranchOfficeCode);
                    client.Headers.Add("BranchLocation", this.AppSetting.Location);
                    client.Headers.Add("BranchAction", BranchUploadAction.BranchReport.ToString());
                    client.Headers.Add("FileName", UploadReportFileName);
                    client.Headers.Add("FileDescription", ReportName);
                    client.Headers.Add("CurrentFY", UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Year.ToString());

                    //byte[] response= client.UploadFile(uploadWebUrl, fullUploadFilePath);
                    byte[] uploadfilebyte = ms.ToArray();
                    byte[] response = client.UploadData(uploadWebUrl, uploadfilebyte);             
                    responsemsg = System.Text.Encoding.ASCII.GetString(response);*/
                    /*using (AcMEERPFTP ftpFileTransfer = new AcMEERPFTP())
                    {
                        ftpFileTransfer.OnProgress += new EventHandler<ProgressStatusEventArgs>(ftpFileTransfer_OnProgress);

                        ResultArgs resultargs = ftpFileTransfer.uploadfilebyFTP(UploadFileURI, ms);
                        if (resultargs.Success)
                        {
                            MessageRender.ShowMessage("Your Report is sucessfully uploaded to Head Office");
                        }
                        else
                        {
                            MessageRender.ShowMessage(resultargs.Message, true);
                        }
                    }*/

                    if (string.IsNullOrEmpty(responsemsg))
                    {
                        MessageRender.ShowMessage("Your Report is sucessfully uploaded to Head Office");
                    }
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally {
                this.Cursor = Cursors.Default; 
                if (!string.IsNullOrEmpty(responsemsg))
                {
                    this.ShowMessageBox(responsemsg);
                }
            }
        }

        private void ftpFileTransfer_OnProgress(object sender, ProgressStatusEventArgs e)
        {
            lblUploadStatus.AllowHtmlStringInCaption = true;
            lblUploadStatus.Text = e.Status;
            pgUploadDatabase.Properties.Maximum = this.UtilityMember.NumberSet.ToInteger(e.FileLength.ToString());
            pgUploadDatabase.Properties.Step = 2048;//this.UtilityMember.NumberSet.ToInteger(e.ByteSent)
            pgUploadDatabase.PerformStep();
            if (this.UtilityMember.NumberSet.ToInteger(e.ByteSent + 2048) >= this.UtilityMember.NumberSet.ToInteger(e.FileLength.ToString()))
            {
                lblUploadStatus.Text = "Uploaded Sucessfully";
            }
        }
                
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rgFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblFileName.Text = UploadReportFileName;
            lblFileName.ToolTip = lblFileName.Text;
            lblUploadStatus.Text = " ";
        }
        #endregion

      

        #region Common Methods
       
        #endregion
               

        

       
    }
}