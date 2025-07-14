using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model;
using Bosco.Model.UIModel;
using Bosco.Model.UIModel.Master;
using Bosco.Utility;
using Bosco.Utility.CommonMemberSet;
using System.IO;
using System.Reflection;
using DevExpress.XtraGrid.Views.Grid;
using Bosco.Model.Transaction;
using Bosco.Report.Base;

namespace ACPP.Modules.ProspectsDonor
{
    public partial class frmDonorMailThanksgiving : frmFinanceBase
    {
        #region Declaration

        ResultArgs resultArgs = new ResultArgs();
        const string SELECT = "SELECT";
        #endregion

        #region Properties
        public CommunicationMode communicationmode { get; set; }
        public DataTable dtThanksgiving = new DataTable();
        private int LetterTypeId { get; set; }
        #endregion

        #region Constructor
        public frmDonorMailThanksgiving()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Load Date Controls
        /// </summary>
        private void LoadDate()
        {
            dtDateFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtDateFrom.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtDateFrom.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            dtDateTo.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtDateTo.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            dtDateTo.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
        }

        /// <summary>
        /// fetch the Records
        /// </summary>
        public void FetchDonationDetails()
        {
            try
            {
                using (DonorAuditorSystem MailingStatusSystem = new DonorAuditorSystem())
                {
                    MailingStatusSystem.MailStatus = (!string.IsNullOrEmpty(cboStatus.Text)) ? this.UtilityMember.NumberSet.ToInteger(cboStatus.SelectedIndex.ToString()) : 2;
                    MailingStatusSystem.DateFrom = dtDateFrom.DateTime;
                    MailingStatusSystem.DateTo = dtDateTo.DateTime;
                    MailingStatusSystem.communicationMode = communicationmode;

                    resultArgs = MailingStatusSystem.FetchMailingThanksDetails();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        rgPreviewType.SelectedIndex = -1;
                        gcMailThanks.DataSource = dtThanksgiving = AddSelectColumn(resultArgs.DataSource.Table);
                        gcMailThanks.RefreshDataSource();
                        rgPreviewType.SelectedIndex = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            {
            }
        }

        /// <summary>
        /// Add the Select Coloum 
        /// </summary>
        /// <param name="dtSource"></param>
        /// <returns></returns>
        private DataTable AddSelectColumn(DataTable dtSource)
        {
            if (!dtSource.Columns.Contains(SELECT))
            {
                dtSource.Columns.Add(SELECT, typeof(Int32));
                dtSource.Select().ToList<DataRow>().ForEach(r => { r[SELECT] = 0; });
            }
            return dtSource;
        }

        private int GetLetterTypeId()
        {
            using (DonorFrontOfficeSystem donorfrontoffice = new DonorFrontOfficeSystem())
            {
                //donorfrontoffice.Communicationmode = communicationmode;
                LetterTypeId = donorfrontoffice.FetchTemplateIdByname(DonorMailTemplate.Thanksgiving.ToString());
            }
            return LetterTypeId;
        }
        #endregion

        #region Events
        /// <summary>
        /// Load the Thanks Giving Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDonorMailThanksgiving_Load(object sender, EventArgs e)
        {
            SetVisibileShortCuts(true, true);
            SetTitle();
            GetLetterTypeId();
            LoadDate();
            FetchDonationDetails();
            ApplyRights();
        }

        private void ApplyRights()
        {
            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                lciGenerate.Visibility = (communicationmode == CommunicationMode.MailDesk) ? (CommonMethod.ApplyUserRights((int)ThanksgivingMail.PreviewThanksgivingMail) != 0 ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never) :
                    (CommonMethod.ApplyUserRights((int)ThanksgivingSMS.PreviewThanksgivingSMS) != 0 ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
            }
        }

        private void SetTitle()
        {
            if (communicationmode == CommunicationMode.MailDesk)
            {
                //this.Text = "Thanksgiving - Mail";
                this.Text = this.GetMessage(MessageCatalog.Networking.DonorMailThanksgiving.DONOR_MAIL_THANKSGIVING_MAIL_CAPTION);
                //btnGenerate.Text = "Preview";
                btnGenerate.Text = this.GetMessage(MessageCatalog.Networking.DonorMailThanksgiving.DONOR_MAIL_THANKSGIVING_MAIL_GENERATE_PREVIEW);
                rgPreviewType.Properties.Items[1].Description = "Email";
            }
            else
            {
                //this.Text = "Thanksgiving - SMS";
                this.Text = this.GetMessage(MessageCatalog.Networking.DonorMailThanksgiving.DONOR_MAIL_THANKSGIVING_SMS_CAPTION);
                //btnGenerate.Text = "Preview";
                btnGenerate.Text = this.GetMessage(MessageCatalog.Networking.DonorMailThanksgiving.DONOR_MAIL_THANKSGIVING_SMS_GENERATE_PREVIEW);
                rgPreviewType.Properties.Items[1].Description = "SMS";
            }
        }

        /// <summary>
        /// count the details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMailThanks_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvMailThanks.RowCount.ToString();
        }

        /// <summary>
        /// Generate the Records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                using (DonorAuditorSystem MailingStatusSystem = new DonorAuditorSystem())
                {
                    DataTable dtSource = gcMailThanks.DataSource as DataTable;
                    if (dtSource != null && dtSource.Rows.Count > 0)
                    {
                        DataView dvDonorSelectedDetails = dtSource.Copy().AsDataView();
                        dvDonorSelectedDetails.RowFilter = "SELECT=1";
                        dtSource = dvDonorSelectedDetails.ToTable();
                        if (dtSource != null && dtSource.Rows.Count > 0)
                        {
                            if (communicationmode == CommunicationMode.MailDesk)
                            {
                                frmDonorMailMerge frmMailMerge = new frmDonorMailMerge();
                                frmMailMerge.TemplateType = DonorMailTemplate.Thanksgiving;
                                frmMailMerge.DonorPreviewType = rgPreviewType.SelectedIndex == 0 ? PreviewType.Print : PreviewType.Email;
                                frmMailMerge.LetterTypeId = LetterTypeId;
                                frmMailMerge.DataSource = GetReceiptVoucher(dtSource);
                                frmMailMerge.UpdateHeld += new EventHandler(OnUpdateHeld);
                                frmMailMerge.ShowDialog();
                            }
                            else if (communicationmode == CommunicationMode.ContactDesk)
                            {
                                frmDonorSMSMerge frmMailMerge = new frmDonorSMSMerge();
                                frmMailMerge.DonorPreviewType = rgPreviewType.SelectedIndex == 0 ? PreviewType.Print : PreviewType.Email;
                                frmMailMerge.TemplateType = DonorMailTemplate.Thanksgiving;
                                frmMailMerge.DataSource = dtSource;
                                frmMailMerge.ShowDialog();
                            }
                        }
                        else
                        {
                            //this.ShowMessageBoxWarning("No record selected to preview");
                            this.ShowMessageBoxWarning(this.GetMessage(MessageCatalog.Networking.DonorMailThanksgiving.DONOR_MAIL_THANKSGIVING_NORECORD_SELECT_PREVIEW));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally
            {
            }
        }

        private DataTable GetReceiptVoucher(DataTable dtSource)
        {
            try
            {
                using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
                {
                    if (dtSource != null && dtSource.Rows.Count > 0)
                    {
                        dtSource.Columns.Add("RECEIPT", typeof(MemoryStream));
                        foreach (DataRow drVoucher in dtSource.Rows)
                        {
                            if (!string.IsNullOrEmpty(drVoucher["VOUCHER_ID"].ToString()))
                            {
                                resultArgs = voucherSystem.FetchReportSetting(UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKRECEIPTS));
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    ReportProperty.Current.VoucherPrintSettingInfo = resultArgs.DataSource.TableView;
                                    Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry(this.MdiParent);
                                    Bosco.Report.Base.ReportProperty.Current.ReportId = UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKDONORRECEIPTS);
                                    Bosco.Report.Base.ReportProperty.Current.PrintCashBankVoucherId = drVoucher["VOUCHER_ID"].ToString();
                                    Bosco.Report.Base.ReportProperty.Current.ProjectTitle = drVoucher["PROJECT"].ToString();

                                    DevExpress.XtraReports.UI.XtraReport rptReceipt = report.GetReport();
                                    if (rptReceipt != null)
                                    {
                                        //using (MemoryStream stream = new MemoryStream())
                                        //{
                                        MemoryStream stream = new MemoryStream();
                                        rptReceipt.ExportToPdf(stream);
                                        // rptReceipt.ExportToPdf("D:\\chinna.pdf");
                                        if (stream != null)
                                        {
                                            drVoucher["RECEIPT"] = stream;
                                        }
                                        //}
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally
            {

            }
            return dtSource;
        }

        /// <summary>
        /// Apply the Logic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            FetchDonationDetails();
        }

        /// <summary>
        /// Select all the Donor Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dtThanksDetails = (DataTable)gcMailThanks.DataSource;
            if (dtThanksDetails != null && dtThanksDetails.Rows.Count > 0)
            {
                foreach (DataRow dr in dtThanksDetails.Rows)
                {
                    int index = dtThanksDetails.Rows.IndexOf(dr);
                    if (gvMailThanks.IsDataRow(gvMailThanks.GetRowHandle(index)))
                    {
                        dr[SELECT] = chkSelectAll.Checked;
                    }
                }
            }
            gcMailThanks.DataSource = dtThanksDetails;
        }

        /// <summary>
        /// filter the records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvMailThanks.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvMailThanks, gcName);
            }
        }

        #endregion

        private void gvMailThanks_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            //int EmailStatus = 0;
            //int SMSStatus = 0;
            //GridView View = sender as GridView;
            //if (e.RowHandle >= 0)
            //{
            //    DataRow drRow = View.GetDataRow(e.RowHandle);  // .GetRowCellDisplayText(e.RowHandle, View.Columns["Category"]);
            //    if (drRow != null)
            //    {
            //        if (communicationmode == CommunicationMode.MailDesk)
            //        {
            //            EmailStatus = this.UtilityMember.NumberSet.ToInteger(drRow["MAIL_STATUS"].ToString());

            //            if (EmailStatus == (int)YesNo.Yes)
            //            {
            //                e.Appearance.BackColor = Color.LightSteelBlue;
            //            }
            //            else if (EmailStatus == (int)YesNo.No)
            //            {
            //                e.Appearance.BackColor = Color.IndianRed;
            //            }
            //        }
            //        else if (communicationmode == CommunicationMode.ContactDesk)
            //        {
            //            SMSStatus = this.UtilityMember.NumberSet.ToInteger(drRow["SMS_STATUS"].ToString());

            //            if (SMSStatus == (int)YesNo.Yes)
            //            {
            //                e.Appearance.BackColor = Color.LightSteelBlue;
            //            }
            //            else if (SMSStatus == (int)YesNo.No)
            //            {
            //                e.Appearance.BackColor = Color.IndianRed;
            //            }
            //        }
            //    }
            //}
        }

        private void rgPreviewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rgPreviewType.SelectedIndex == 0 || rgPreviewType.SelectedIndex == 1)
            {
                if (dtThanksgiving != null && dtThanksgiving.Rows.Count > 0)
                {
                    DataView dvThanksgiving = new DataView(dtThanksgiving);
                    if (rgPreviewType.SelectedIndex == 0)
                    {
                        dvThanksgiving.RowFilter = "ADDRESS<>'Nil' AND ADDRESS<>''";
                    }
                    else if (rgPreviewType.SelectedIndex == 1)
                    {
                        dvThanksgiving.RowFilter = (communicationmode == CommunicationMode.MailDesk) ? "EMAIL<>''" : "[MOBILE NO]<>''";
                    }

                    gcMailThanks.DataSource = dvThanksgiving.ToTable();
                    gcMailThanks.RefreshDataSource();
                    dvThanksgiving.RowFilter = "";
                }
            }
        }

        private void ucThanksGiving_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucThanksGiving_PrintClicked(object sender, EventArgs e)
        {
            //PrintGridViewDetails(gcMailThanks, "Thanksgiving Details", PrintType.DT, gvMailThanks, true);
            PrintGridViewDetails(gcMailThanks, this.GetMessage(MessageCatalog.Networking.DonorMailThanksgiving.DONOR_MAIL_THANKSGIVING_PRINT_CAPTION), PrintType.DT, gvMailThanks, true);
        }

        private void ucThanksGiving_RefreshClicked(object sender, EventArgs e)
        {
            FetchDonationDetails();
        }

    }
}