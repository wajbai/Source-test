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
using System.IO;
using System.Reflection;
using Bosco.Model.Transaction;
using DevExpress.XtraRichEdit.Commands;
using Bosco.Model;
using Bosco.Model.Donor;
using AcMEDSync;
using Bosco.DAO.Schema;

namespace ACPP.Modules.ProspectsDonor
{
    public partial class frmDonorSMSMerge : frmFinanceBase
    {
        #region Declaration
        public static string ACPERP_INSTALLED_PATH = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SMS Templates");
        ResultArgs resultArgs = new ResultArgs();
        AppSchemaSet appSchema = new AppSchemaSet();
        public PreviewType DonorPreviewType { get; set; }
        public int LetterTypeId { get; set; }
        public event EventHandler UpdateHeld;
        #endregion

        #region Constructor
        public frmDonorSMSMerge()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public DonorMailTemplate TemplateType { get; set; }
        public string FeastDayTemplate { get; set; }
        public int FeastTemplateId { get; set; }
        public AnniversaryType AnniversaryTemplate { get; set; }
        public DataTable DataSource { get; set; }
        #endregion

        #region Methods
        private void AutomateMailMerge()
        {
            string sTemplatePath = string.Empty;
            if (DataSource != null && DataSource.Rows.Count > 0)
            {
                DevExpress.XtraRichEdit.API.Native.MailMergeOptions myMergeOptions = reSMSMerge.Document.CreateMailMergeOptions();
                myMergeOptions.MergeMode = DevExpress.XtraRichEdit.API.Native.MergeMode.NewSection;
                myMergeOptions.DataSource = DataSource;
                this.richEditBarController1.Control = reSMSMerge;

                using (DevExpress.XtraRichEdit.IRichEditDocumentServer myTempRichEditServer = reSMSMerge.CreateDocumentServer())
                {
                    myTempRichEditServer.Options.MailMerge.DataSource = reSMSMerge;
                    GetTemaplate(myTempRichEditServer);
                    myTempRichEditServer.MailMerge(myMergeOptions, reSMSMerge.Document);

                }
            }
        }
        private void GetTemaplate(DevExpress.XtraRichEdit.IRichEditDocumentServer myTempRichEditServer)
        {
            using (DonorFrontOfficeSystem donorfrontofficesystem = new DonorFrontOfficeSystem())
            {

                donorfrontofficesystem.Communicationmode = CommunicationMode.ContactDesk;
                DataTable dtContent = new DataTable();
                LetterTypeId = (int)TemplateType;
                donorfrontofficesystem.LetterTypeId = LetterTypeId;
                if (LetterTypeId != (int)DonorMailTemplate.Tasks && LetterTypeId != (int)DonorMailTemplate.Anniversary)
                {
                    // donorfrontofficesystem.Name = GetTemplateName();
                    dtContent = donorfrontofficesystem.FetchTemplateContent().DataSource.Table;
                }
                else if (LetterTypeId == (int)DonorMailTemplate.Tasks)
                {
                    donorfrontofficesystem.TemplateId = FeastTemplateId;
                    dtContent = donorfrontofficesystem.FetchTemplateContentById().DataSource.Table;
                }
                else
                {
                    donorfrontofficesystem.Name = GetTemplateName();
                    dtContent = donorfrontofficesystem.FetchContentByname().DataSource.Table;
                }

                if (dtContent != null && dtContent.Rows.Count > 0)
                {
                    byte[] content = (byte[])dtContent.Rows[dtContent.Rows.Count - 1]["CONTENT"];
                    if (content != null && content.Count() > 0)
                    {
                        MemoryStream ms = new MemoryStream(content);
                        ms.Seek(0, SeekOrigin.Begin);
                        myTempRichEditServer.LoadDocument(ms, DevExpress.XtraRichEdit.DocumentFormat.OpenXml);
                        ms.Close();
                    }
                }
            }
        }
        private string GetTemplateName()
        {
            string TemplateName = "";

            if (TemplateType == DonorMailTemplate.Anniversary)
            {
                if (AnniversaryTemplate == AnniversaryType.Birthday)
                {
                    TemplateName = AnniversaryType.Birthday.ToString();
                }
                else
                {
                    TemplateName = AnniversaryType.Wedding.ToString();
                }

            }
            else if (TemplateType == DonorMailTemplate.Tasks)
            {
                TemplateName = FeastDayTemplate;
            }
            else
            {
                TemplateName = TemplateType.ToString();
            }
            return TemplateName + ".rtf";
        }

        private void ApplyUserRights()
        {
            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                if (TemplateType == DonorMailTemplate.Thanksgiving)
                {
                    ucSMSMergeOptions.VisibleSendSMS = CommonMethod.ApplyUserRights((int)ThanksgivingSMS.SendThanksgivingSMS) != 0 ?
                        DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                    ucSMSMergeOptions.VisibleModifyTemplate = CommonMethod.ApplyUserRights((int)SMSTemplate.CreateThanksgivingSMS) != 0 ?
    DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                }
                else if (TemplateType == DonorMailTemplate.Anniversary)
                {
                    ucSMSMergeOptions.VisibleSendSMS = CommonMethod.ApplyUserRights((int)AnniversarySMS.SendAnniversarySMS) != 0 ?
                        DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                    ucSMSMergeOptions.VisibleModifyTemplate = CommonMethod.ApplyUserRights((int)SMSTemplate.CreateAnniversarySMS) != 0 ?
    DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                }
                else if (TemplateType == DonorMailTemplate.Tasks)
                {
                    ucSMSMergeOptions.VisibleSendSMS = CommonMethod.ApplyUserRights((int)FeastSMS.SendFeastSMS) != 0 ?
                        DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                    ucSMSMergeOptions.VisibleModifyTemplate = CommonMethod.ApplyUserRights((int)SMSTemplate.CreateFeastDaySMS) != 0 ?
    DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                }
                else if (TemplateType == DonorMailTemplate.Appeal)
                {
                    ucSMSMergeOptions.VisibleSendSMS = CommonMethod.ApplyUserRights((int)AppealSMS.SendAppealSMS) != 0 ?
                        DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                    ucSMSMergeOptions.VisibleModifyTemplate = CommonMethod.ApplyUserRights((int)SMSTemplate.CreateAppealSMS) != 0 ?
    DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                }
            }
        }
        #endregion

        private void ucSMSMergeOptions_SendEmailClicked(object sender, EventArgs e)
        {

        }

        private void ucSMSMergeOptions_ModifyTemplateClicked(object sender, EventArgs e)
        {
            frmDonorSMSTemplate frmTemplateSMS = new frmDonorSMSTemplate();
            frmTemplateSMS.TemplateType = this.TemplateType;
            frmTemplateSMS.FeastTemplateId = this.FeastTemplateId;
            frmTemplateSMS.AnniversaryTemplatetype = AnniversaryTemplate;
            frmTemplateSMS.ShowDialog();
            if (frmTemplateSMS.dialogResult == DialogResult.OK)
            {
                AutomateMailMerge();
            }
        }

        private void frmDonorSMSMerge_Load(object sender, EventArgs e)
        {
            if (DonorPreviewType == PreviewType.Email)
            {
                ucSMSMergeOptions.VisibleSendSMS = DevExpress.XtraBars.BarItemVisibility.Always;
                ucSMSMergeOptions.VisiblePrintLabel = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            else
            {
                ucSMSMergeOptions.VisibleSendSMS = DevExpress.XtraBars.BarItemVisibility.Never;
                ucSMSMergeOptions.VisiblePrintLabel = DevExpress.XtraBars.BarItemVisibility.Always;
            }

            AutomateMailMerge();
            ApplyUserRights();
        }

        private void ucSMSMergeOptions_SendSMSClicked(object sender, EventArgs e)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                //this.ShowWaitDialog("Connecting to Internet..");
                this.ShowWaitDialog(this.GetMessage(MessageCatalog.Networking.DonorMailThanksgiving.DONOR_SMS_MERGE_INTERNET_CONNECTION_INFO));
                if (this.CheckForInternetConnectionhttp())
                {
                    this.CloseWaitDialog();
                    using (DonorFrontOfficeSystem FrontOffice = new DonorFrontOfficeSystem())
                    {
                        FrontOffice.TemplateType = TemplateType;
                        FrontOffice.dtSelectedDataSource = DataSource;
                        FrontOffice.document = reSMSMerge.Document;
                        FrontOffice.Communicationmode = CommunicationMode.ContactDesk;

                        DataTable dtSMSCredit = FrontOffice.ConsturctSMSCredits();
                        if (dtSMSCredit != null && dtSMSCredit.Rows.Count > 0)
                        {
                            frmDonorSMSCredits frmsmscredits = new frmDonorSMSCredits(dtSMSCredit);
                            frmsmscredits.ShowDialog();

                            if (frmsmscredits.DialogResult == System.Windows.Forms.DialogResult.OK)
                            {
                                //this.ShowWaitDialog("Sending SMS...,");
                                this.ShowWaitDialog(this.GetMessage(MessageCatalog.Networking.DonorMailThanksgiving.DONOR_SMS_MERGE_SENDING_SMS_INFO));
                                result = FrontOffice.SendDonorSMS();
                            }
                        }
                    }
                }
                else
                {
                    CloseWaitDialog();
                    //result.Message = "Internet is not connected. Please check your internet or FTP rights";
                    result.Message = this.GetMessage(MessageCatalog.Networking.DonorMailThanksgiving.DONOR_SMS_MERGE_INTERNET_CONNECTION_CHECK_INFO);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
            finally
            {
                this.CloseWaitDialog();
                if (result.Success)
                {
                    //this.ShowMessageBox("SMS has been sent successfully");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Networking.DonorMailThanksgiving.DONOR_SMS_MERGE_SMS_SEND_INFORMATION));
                    if (UpdateHeld != null)
                    {
                        UpdateHeld(this, e);
                    }
                }
                else
                {
                    this.ShowMessageBoxError(result.Message);
                }
            }
        }

        private void ucSMSMergeOptions_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucSMSMergeOptions_PrintLabelClicked(object sender, EventArgs e)
        {
            //frmLabelPrint labelPrint = new frmLabelPrint(DataSource);
            //labelPrint.ShowDialog();
            Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry();
            report.ShowPrintView(DataSource, "RPT-127");
        }
    }
}