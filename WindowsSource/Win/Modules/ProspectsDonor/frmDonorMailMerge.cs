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
using System.Net.Mail;

namespace ACPP.Modules.ProspectsDonor
{
    public partial class frmDonorMailMerge : frmFinanceBase
    {
        public static string ACPERP_INSTALLED_PATH = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "MailTemplate");
        public int LetterTypeId { get; set; }
        public DonorMailTemplate TemplateType { get; set; }
        public PreviewType DonorPreviewType { get; set; }
        public string FeastDayTemplate { get; set; }
        public int FeastTemplateId { get; set; }
        public AnniversaryType AnniversaryTemplate { get; set; }
        public DataTable DataSource { get; set; }
        ResultArgs resultArgs = new ResultArgs();
        AppSchemaSet appSchema = new AppSchemaSet();
        public event EventHandler UpdateHeld;
        public byte[] NewsLetter { get; set; }

        public frmDonorMailMerge()
        {
            InitializeComponent();
        }

        private void frmDonorMailMerge_Load(object sender, EventArgs e)
        {
            if (DonorPreviewType == PreviewType.Email)
            {
                ucMailMergeOptions1.VisibleSendMail = DevExpress.XtraBars.BarItemVisibility.Always;
                ucMailMergeOptions1.VisiblePrintLabel = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            else
            {
                ucMailMergeOptions1.VisibleSendMail = DevExpress.XtraBars.BarItemVisibility.Never;
                ucMailMergeOptions1.VisiblePrintLabel = DevExpress.XtraBars.BarItemVisibility.Always;
            }

            AutomateMailMerge();
            ApplyUserRights();
        }

        private void ApplyUserRights()
        {
            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                if (TemplateType == DonorMailTemplate.Thanksgiving)
                {
                    ucMailMergeOptions1.VisibleSendMail = CommonMethod.ApplyUserRights((int)ThanksgivingMail.SendThanksgivingMail) != 0 ?
                        DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                    ucMailMergeOptions1.VisibleModifyTemplate = CommonMethod.ApplyUserRights((int)MailTemplate.CreateThanksgivingMail) != 0 ?
    DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                }
                else if (TemplateType == DonorMailTemplate.Anniversary)
                {
                    ucMailMergeOptions1.VisibleSendMail = CommonMethod.ApplyUserRights((int)AnniversaryMail.SendAnniversaryMail) != 0 ?
                        DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                    ucMailMergeOptions1.VisibleModifyTemplate = CommonMethod.ApplyUserRights((int)MailTemplate.CreateAnniversaryMail) != 0 ?
    DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                }
                else if (TemplateType == DonorMailTemplate.Tasks)
                {
                    ucMailMergeOptions1.VisibleSendMail = CommonMethod.ApplyUserRights((int)FeastMail.SendFeastMail) != 0 ?
                        DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                    ucMailMergeOptions1.VisibleModifyTemplate = CommonMethod.ApplyUserRights((int)MailTemplate.CreateFeastDayMail) != 0 ?
    DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                }
                else if (TemplateType == DonorMailTemplate.NewsLetter)
                {
                    ucMailMergeOptions1.VisibleSendMail = CommonMethod.ApplyUserRights((int)NewsletterMail.SendNewsletterMail) != 0 ?
                        DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                    ucMailMergeOptions1.VisibleModifyTemplate = CommonMethod.ApplyUserRights((int)MailTemplate.CreateNewsletterMail) != 0 ?
    DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                }
                else if (TemplateType == DonorMailTemplate.Appeal)
                {
                    ucMailMergeOptions1.VisibleSendMail = CommonMethod.ApplyUserRights((int)AppealMail.SendAppealMail) != 0 ?
                        DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                    ucMailMergeOptions1.VisibleModifyTemplate = CommonMethod.ApplyUserRights((int)MailTemplate.CreateAppealMail) != 0 ?
    DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                }
            }
        }

        private void AutomateMailMerge()
        {
            if (DataSource != null && DataSource.Rows.Count > 0)
            {
                DevExpress.XtraRichEdit.API.Native.MailMergeOptions myMergeOptions = reMailMerge.Document.CreateMailMergeOptions();
                myMergeOptions.MergeMode = DevExpress.XtraRichEdit.API.Native.MergeMode.NewSection;
                myMergeOptions.DataSource = DataSource;
                this.richEditBarController1.Control = reMailMerge;

                using (DevExpress.XtraRichEdit.IRichEditDocumentServer myTempRichEditServer = reMailMerge.CreateDocumentServer())
                {
                    myTempRichEditServer.Options.MailMerge.DataSource = reMailMerge;
                    GetTemaplate(myTempRichEditServer);
                    myTempRichEditServer.MailMerge(myMergeOptions, reMailMerge.Document);

                }

            }
        }
        private void GetTemaplate(DevExpress.XtraRichEdit.IRichEditDocumentServer myTempRichEditServer)
        {
            using (DonorFrontOfficeSystem donorfrontofficesystem = new DonorFrontOfficeSystem())
            {
                donorfrontofficesystem.Communicationmode = CommunicationMode.MailDesk;
                DataTable dtContent = new DataTable();
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

        /// <summary>
        /// Modify the Content of the Template
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucMailMergeOptions1_ModifyTemplateClicked(object sender, EventArgs e)
        {
            string TemplateName = string.Empty;
            string MailTemplatePath = string.Empty;
            frmDonorMailTemplate frmTemplateMail = new frmDonorMailTemplate();
            frmTemplateMail.TemplateType = this.TemplateType;
            frmTemplateMail.FeastTemplateId = this.FeastTemplateId;
            frmTemplateMail.AnniversaryTemplatetype = AnniversaryTemplate;
            frmTemplateMail.ShowDialog();
            if (frmTemplateMail.dialogResult == DialogResult.OK)
            {
                AutomateMailMerge();
            }
        }

        private AlternateViewCollection GetAlternateViewCollection()
        {
            AlternateViewCollection viewCollection = null;
            using (RichEditMailMessageHTML richeditMailMessage = new RichEditMailMessageHTML(reMailMerge))
            {
                viewCollection = richeditMailMessage.ConvertRTFtoHTMLVIEW();
            }
            return viewCollection;
        }

        /// <summary>
        /// Send a Mail to the Donor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucMailMergeOptions1_SendEmailClicked(object sender, EventArgs e)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                this.ShowWaitDialog("Connecting to Internet..");
                if (this.CheckForInternetConnectionhttp())
                {
                    using (DonorFrontOfficeSystem FrontOffice = new DonorFrontOfficeSystem())
                    {
                        FrontOffice.TemplateType = TemplateType;
                        FrontOffice.dtSelectedDataSource = DataSource;
                        FrontOffice.MaritalStatus = (int)AnniversaryTemplate;
                        FrontOffice.document = reMailMerge.Document;
                        FrontOffice.Content = NewsLetter;
                        FrontOffice.DocumentAlternateViewCollection = GetAlternateViewCollection();
                        FrontOffice.Communicationmode = CommunicationMode.MailDesk;
                        //this.ShowWaitDialog("Sending Mail...");
                        this.ShowWaitDialog(this.GetMessage(MessageCatalog.Networking.DonorMailFeast.DONOR_MAIL_MERGE_SENDING_MAIL_INFORMATION));
                        result = FrontOffice.SendDonorMail();
                    }
                }
                else
                {
                    CloseWaitDialog();
                    //result.Message = "Internet is not connected. Please check your internet or FTP rights";
                    result.Message = this.GetMessage(MessageCatalog.Networking.DonorMailFeast.DONOR_MAIL_MERGE_INTERNET_CONNECTION_INFORMATION);
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
                    //this.ShowMessageBox("Mail has been sent successfully");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Networking.DonorMailFeast.DONOR_MAIL_MERGE_SENDING_MAIL_INFO));
                    if (UpdateHeld != null)
                    {
                        UpdateHeld(this, e);
                    }
                    this.Close();
                }
                else
                {
                    this.ShowMessageBoxError(result.Message);
                }
            }
        }

        private void ucMailMergeOptions1_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucMailMergeOptions1_PrintLabelClicked(object sender, EventArgs e)
        {
            //frmLabelPrint labelPrint = new frmLabelPrint(DataSource);
            //labelPrint.ShowDialog();

            Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry();

            // take unique donor lable
            DataView dvUniqueFields = DataSource.DefaultView;
            string[] distinct = { "NAME", "ADDRESS", "CITYPLACE", "STATE", "PINCODE" };
            DataSource = dvUniqueFields.ToTable(true, distinct);

            report.ShowPrintView(DataSource, "RPT-127");
        }
    }
}