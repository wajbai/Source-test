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
using Bosco.Model.Donor;
using Bosco.Model;
using DevExpress.XtraGrid.Views.Grid;

namespace ACPP.Modules.ProspectsDonor
{
    public partial class frmDonorAnniversaries : frmFinanceBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = new ResultArgs();
        DataTable dtAnniversary = new DataTable();
        const string SELECT = "SELECT";
        #endregion

        #region MyRegion

        #endregion

        #region Properties
        public CommunicationMode communcationMode { get; set; }
        #endregion

        #region Constructor
        public frmDonorAnniversaries()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// Load the Anniversaries Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAnniversaries_Load(object sender, EventArgs e)
        {
            SetTitle();
            LoadDate();
            LoadAniversaryDetails();
            ApplyRights();
        }

        /// <summary>
        /// Apply the rights
        /// </summary>
        private void ApplyRights()
        {
            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                lciGenerate.Visibility = (communcationMode == CommunicationMode.MailDesk) ? (CommonMethod.ApplyUserRights((int)AnniversaryMail.PreviewAnniversaryMail) != 0 ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never) :
                (CommonMethod.ApplyUserRights((int)AnniversarySMS.PreviewAnniversarySMS) != 0 ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
            }
        }

        /// <summary>
        /// Set the title for Mode
        /// </summary>
        private void SetTitle()
        {
            if (communcationMode == CommunicationMode.MailDesk)
            {
                //this.Text = "Anniversary - Mail";
                this.Text = this.GetMessage(MessageCatalog.Networking.Donoranniversaries.DONOR_ANNIVERSARIES_MODE_MAIL_CAPTION);
                rgPreviewType.Properties.Items[1].Description = "Email";
                //btnGenerate.Text = "Preview";
                btnGenerate.Text = this.GetMessage(MessageCatalog.Networking.Donoranniversaries.DONOR_ANNIVERSARIES_GENERATE_PREVIEW_CAPTION);
            }
            else
            {
                //this.Text = "Anniversary - SMS";
                this.Text = this.GetMessage(MessageCatalog.Networking.Donoranniversaries.DONOR_ANNIVERSARIES_MODE_SMS_CAPTION);
                rgPreviewType.Properties.Items[1].Description = "SMS";
                //btnGenerate.Text = "Preview";
                btnGenerate.Text = this.GetMessage(MessageCatalog.Networking.Donoranniversaries.DONOR_ANNIVERSARIES_GENERATE_PREVIEW_CAPTION);
            }
        }

        /// <summary>
        /// Generate the Anniversary Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (ValidateAnniversaries())
            {
                DataTable dtSource = gcAnniversaries.DataSource as DataTable;
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    DataView dvDonorSelectedDetails = dtSource.Copy().AsDataView();
                    dvDonorSelectedDetails.RowFilter = "SELECT=1";
                    dtSource = dvDonorSelectedDetails.ToTable();
                    if (dtSource != null && dtSource.Rows.Count > 0)
                    {
                        if (communcationMode == CommunicationMode.MailDesk)
                        {
                            frmDonorMailMerge frmMailMerge = new frmDonorMailMerge();
                            frmMailMerge.TemplateType = DonorMailTemplate.Anniversary;
                            frmMailMerge.DonorPreviewType = rgPreviewType.SelectedIndex == 0 ? PreviewType.Print : PreviewType.Email;
                            frmMailMerge.LetterTypeId = (int)DonorMailTemplate.Anniversary;
                            frmMailMerge.AnniversaryTemplate = cboAnnType.SelectedIndex == 0 ? AnniversaryType.Birthday : AnniversaryType.Wedding;
                            frmMailMerge.DataSource = dtSource;
                            frmMailMerge.UpdateHeld += new EventHandler(OnUpdateHeld);
                            frmMailMerge.ShowDialog();
                        }
                        else if (communcationMode == CommunicationMode.ContactDesk)
                        {
                            frmDonorSMSMerge frmDonorSms = new frmDonorSMSMerge();
                            frmDonorSms.DonorPreviewType = rgPreviewType.SelectedIndex == 0 ? PreviewType.Print : PreviewType.Email;
                            frmDonorSms.TemplateType = DonorMailTemplate.Anniversary;
                            frmDonorSms.AnniversaryTemplate = cboAnnType.SelectedIndex == 0 ? AnniversaryType.Birthday : AnniversaryType.Wedding;
                            frmDonorSms.DataSource = dtSource;
                            frmDonorSms.UpdateHeld += new EventHandler(OnUpdateHeld);
                            frmDonorSms.ShowDialog();
                        }

                    }
                    else
                    {
                        //this.ShowMessageBoxError("No records is selected to generate");
                        this.ShowMessageBoxError(this.GetMessage(MessageCatalog.Networking.Donoranniversaries.DONOR_ANNIVERSARIES_RECORDS_GENERATE_EMPTY));
                    }
                }
            }
        }

        /// <summary>
        /// To set the Shortcut Visibility for Date(F3) and Project(F5) 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void frmDonorAnniversaries_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(true, true);
        }

        /// <summary>
        /// row Count the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvAnniversaries_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvAnniversaries.RowCount.ToString();
        }

        /// <summary>
        /// Check the Items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {

            if (gcAnniversaries.DataSource != null)
            {
                DataTable dtanniversaries = (DataTable)gcAnniversaries.DataSource;
                if (dtanniversaries != null && dtanniversaries.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtanniversaries.Rows)
                    {
                        int index = dtanniversaries.Rows.IndexOf(dr);
                        if (gvAnniversaries.IsDataRow(gvAnniversaries.GetRowHandle(index)))
                        {
                            dr[SELECT] = chkSelectAll.Checked;
                        }
                    }
                }
                gcAnniversaries.DataSource = dtanniversaries;
            }
        }

        /// <summary>
        /// check the filter 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvAnniversaries.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvAnniversaries, gccolDonor);
            }
        }

        /// <summary>
        /// rowstyle selection changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        /// <summary>
        /// Apply the Anniversary type details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            LoadAniversaryDetails();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load Date Controls
        /// </summary>
        private void LoadDate()
        {
            dtDateFrom.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtDateTo.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// Validate the anniversaries
        /// </summary>
        /// <returns></returns>
        private bool ValidateAnniversaries()
        {
            bool isValid = true;
            if (gvAnniversaries.SelectedRowsCount == 0)
            {
                isValid = false;
                //this.ShowMessageBoxWarning("No record is selected to generate.");
                this.ShowMessageBoxWarning(this.GetMessage(MessageCatalog.Networking.Donoranniversaries.DONOR_ANNIVERSARIES_RECORDS_GENERATE_EMPTY));
                gvAnniversaries.Focus();
            }
            return isValid;
        }

        /// <summary>
        /// Load Anniversary Day Details
        /// </summary>
        private void LoadAniversaryDetails()
        {
            try
            {
                using (DonorFrontOfficeSystem donorfrontOfficesys = new DonorFrontOfficeSystem())
                {
                    rgPreviewType.SelectedIndex = -1;
                    if (communcationMode == CommunicationMode.MailDesk)
                    {
                        donorfrontOfficesys.MaritalStatus = cboAnnType.SelectedIndex == 0 ? this.UtilityMember.NumberSet.ToInteger(cboAnnType.SelectedIndex.ToString()) : this.UtilityMember.NumberSet.ToInteger(cboAnnType.SelectedIndex.ToString());
                        donorfrontOfficesys.DateFrom = this.UtilityMember.DateSet.ToDate(dtDateFrom.Text.ToString(), false);
                        donorfrontOfficesys.DateTo = this.UtilityMember.DateSet.ToDate(dtDateTo.Text.ToString(), false);
                        donorfrontOfficesys.Communicationmode = cboAnnType.SelectedIndex == 0 ? CommunicationMode.MailDesk : CommunicationMode.ContactDesk; // this is to get 1 and 2 only
                        resultArgs = donorfrontOfficesys.FetchAniversaryTypeDetails();
                        if (resultArgs.Success && resultArgs.DataSource.Table != null)
                        {
                            gcAnniversaries.DataSource = resultArgs.DataSource.Table;
                            gcAnniversaries.DataSource = dtAnniversary = AddSelectColumn(resultArgs.DataSource.Table);
                            gcAnniversaries.RefreshDataSource();
                        }
                        else
                        {
                            gcAnniversaries.DataSource = null;
                        }
                    }
                    else if (communcationMode == CommunicationMode.ContactDesk)
                    {
                        donorfrontOfficesys.MaritalStatus = cboAnnType.SelectedIndex == 0 ? this.UtilityMember.NumberSet.ToInteger(cboAnnType.SelectedIndex.ToString()) : this.UtilityMember.NumberSet.ToInteger(cboAnnType.SelectedIndex.ToString());
                        donorfrontOfficesys.DateFrom = this.UtilityMember.DateSet.ToDate(dtDateFrom.Text.ToString(), false);
                        donorfrontOfficesys.DateTo = this.UtilityMember.DateSet.ToDate(dtDateTo.Text.ToString(), false);
                        donorfrontOfficesys.Communicationmode = cboAnnType.SelectedIndex == 0 ? CommunicationMode.MailDesk : CommunicationMode.ContactDesk; // this is to get 1 and 2 only
                        resultArgs = donorfrontOfficesys.FetchAnniversaryTypeSMSDetail();
                        if (resultArgs.Success && resultArgs.DataSource.Table != null)
                        {
                            gcAnniversaries.DataSource = resultArgs.DataSource.Table;
                            gcAnniversaries.DataSource = dtAnniversary = AddSelectColumn(resultArgs.DataSource.Table);
                            gcAnniversaries.RefreshDataSource();
                        }
                        else
                        {
                            gcAnniversaries.DataSource = null;
                        }
                    }
                    rgPreviewType.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBoxError(ex.Message);
            }
            finally { }
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
        #endregion

        private void rgPreviewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rgPreviewType.SelectedIndex == 0 || rgPreviewType.SelectedIndex == 1)
            {
                if (dtAnniversary != null && dtAnniversary.Rows.Count > 0)
                {
                    DataView dvAnniversary = new DataView(dtAnniversary);
                    if (rgPreviewType.SelectedIndex == 0)
                    {
                        dvAnniversary.RowFilter = "ADDRESS<>'Nil' AND ADDRESS<>''";
                    }
                    else if (rgPreviewType.SelectedIndex == 1)
                    {
                        dvAnniversary.RowFilter = (communcationMode == CommunicationMode.MailDesk) ? "EMAIL<>''" : "[MOBILE NO]<>''";
                    }

                    gcAnniversaries.DataSource = dvAnniversary.ToTable();
                    gcAnniversaries.RefreshDataSource();
                    dvAnniversary.RowFilter = "";
                }
            }
        }

        private void ucAnniversaries_PrintClicked(object sender, EventArgs e)
        {
            //PrintGridViewDetails(gcAnniversaries, "Anniversary Details", PrintType.DT, gvAnniversaries, true);
            PrintGridViewDetails(gcAnniversaries, this.GetMessage(MessageCatalog.Networking.Donoranniversaries.DONOR_ANNIVERSARIES_PRINT_CAPTION), PrintType.DT, gvAnniversaries, true);
        }

        private void ucAnniversaries_RefreshClicked(object sender, EventArgs e)
        {
            LoadAniversaryDetails();
        }

        private void ucAnniversaries_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gcAnniversaries_Click(object sender, EventArgs e)
        {

        }

    }
}