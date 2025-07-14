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
using System.IO;

namespace ACPP.Modules.ProspectsDonor
{
    public partial class frmDonorMailNewsLetter : frmFinanceBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = new ResultArgs();
        DataTable dtNewsLetter = new DataTable();
        const string SELECT = "SELECT";
        #endregion

        #region Constructor
        public frmDonorMailNewsLetter()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public CommunicationMode communicationmode { get; set; }
        public byte[] NewsLetter { get; set; }
        #endregion

        #region Events

        /// <summary>
        /// Load the details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDonorMailNewsLetter_Load(object sender, EventArgs e)
        {
            SetTitle();
            loadTaskDetails();
            ApplyRights();
        }

        private void ApplyRights()
        {
            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                lciGenerate.Visibility = CommonMethod.ApplyUserRights((int)NewsletterMail.PreviewNewsletterMail) != 0 ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        /// <summary>
        /// Load the Details of Task Mapped Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkpTasklist_EditValueChanged(object sender, EventArgs e)
        {
            LoadNewsLetterDonorsByTask();
        }

        /// <summary>
        /// This method is used to generated merged data for selected donors with newsletter template and will be show in mail preivew form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (ValidateNewsLetter())
            {
                DataTable dtSource = gcNewsletter.DataSource as DataTable;

                // DataView dvNewsletter = gvNewsLetter.DataSource as DataView;
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    DataView dvDonorSelectedDetails = dtSource.Copy().AsDataView();
                    dvDonorSelectedDetails.RowFilter = "SELECT=1";
                    dtSource = dvDonorSelectedDetails.ToTable();

                    // dvNewsletter.RowFilter = "SELECT=1";
                    if (dtSource != null && dtSource.Rows.Count > 0)
                    {
                        frmDonorMailMerge frmMailMerge = new frmDonorMailMerge();
                        frmMailMerge.TemplateType = DonorMailTemplate.NewsLetter;
                        frmMailMerge.DonorPreviewType = rgPreviewType.SelectedIndex == 0 ? PreviewType.Print : PreviewType.Email;
                        frmMailMerge.LetterTypeId = (int)DonorMailTemplate.NewsLetter;
                        frmMailMerge.DataSource = dtSource;
                        frmMailMerge.NewsLetter = NewsLetter;
                        frmMailMerge.UpdateHeld += new EventHandler(OnUpdateHeld);
                        frmMailMerge.ShowDialog();
                    }
                    else
                    {
                        //this.ShowMessageBoxError("No records is selected to generate");
                        this.ShowMessageBoxError(this.GetMessage(MessageCatalog.Networking.DonorMailNewsLetter.DONOR_NEWSLETTER_NORECORD_EDIT_INFO));
                    }
                }
            }
        }

        /// <summary>
        /// refresh the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadNewsLetterDonorsByTask();
            loadTaskDetails();
        }

        /// <summary>
        /// edit the Button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkpTasklist_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                frmDonorMailTask MailTask = new frmDonorMailTask();
                MailTask.TagId = (int)AddNewRow.NewRow;
                MailTask.ComMode = communicationmode;
                MailTask.UpdataHeld += new EventHandler(OnUpdateHeld);
                MailTask.ShowDialog();
            }
            else if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.OK)
            {
                if (glkpTasklist.EditValue != null)
                {
                    frmDonorMailTask MailTask = new frmDonorMailTask();
                    MailTask.TagId = this.UtilityMember.NumberSet.ToInteger(glkpTasklist.EditValue.ToString());
                    MailTask.ComMode = communicationmode;
                    MailTask.UpdataHeld += new EventHandler(OnUpdateHeld);
                    MailTask.ShowDialog();
                }
                else
                {
                    //this.ShowMessageBoxWarning("Task is not selected to edit");
                    this.ShowMessageBoxWarning(this.GetMessage(MessageCatalog.Networking.DonorMailNewsLetter.DONOR_NEWSLETTER_NORECORD_INFO));
                }
            }
        }

        /// <summary>
        /// Select all the Donors
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (gcNewsletter.DataSource != null)
            {
                DataTable dtNewsLetter = (DataTable)gcNewsletter.DataSource;
                if (dtNewsLetter != null && dtNewsLetter.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtNewsLetter.Rows)
                    {
                        int index = dtNewsLetter.Rows.IndexOf(dr);
                        if (gvNewsLetter.IsDataRow(gvNewsLetter.GetRowHandle(index)))
                        {
                            dr[SELECT] = chkSelectAll.Checked;
                        }
                    }
                }
                gcNewsletter.DataSource = dtNewsLetter;
            }
        }

        /// <summary>
        /// Count the Logic of the Screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvNewsLetter_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvNewsLetter.RowCount.ToString();
        }

        /// <summary>
        /// Status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadNewsLetterDonorsByTask();
        }

        /// <summary>
        /// check the filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvNewsLetter.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvNewsLetter, gccolName);
            }
        }

        private void frmDonorMailNewsLetter_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(false, true);
        }

        #endregion

        #region methods

        /// <summary>
        /// Load the task
        /// </summary>
        private void loadTaskDetails()
        {
            try
            {
                using (DonorFrontOfficeSystem frontOffice = new DonorFrontOfficeSystem())
                {
                    frontOffice.TemplateType = DonorMailTemplate.NewsLetter;
                    resultArgs = frontOffice.FetchTaskDetails();
                    if (resultArgs.Success)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpTasklist, resultArgs.DataSource.Table, frontOffice.AppSchema.DonorTags.TAG_NAMEColumn.ColumnName, frontOffice.AppSchema.DonorTags.TAG_IDColumn.ColumnName);
                        glkpTasklist.EditValue = glkpTasklist.Properties.GetKeyValue(0);
                    }
                    else
                    {
                        MessageRender.ShowMessage(resultArgs.Message, true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// Validate the News Letter
        /// </summary>
        /// <returns></returns>
        private bool ValidateNewsLetter()
        {
            bool isValid = true;
            if (gvNewsLetter.SelectedRowsCount == 0)
            {
                isValid = false;
                //this.ShowMessageBoxWarning("No record is selected to generate.");
                this.ShowMessageBoxWarning(this.GetMessage(MessageCatalog.Networking.DonorMailNewsLetter.DONOR_NEWSLETTER_RECORD_EMPTY));
                gvNewsLetter.Focus();
            }
            return isValid;
        }

        /// <summary>
        /// NewsLetter
        /// </summary>
        private void LoadNewsLetterDonorsByTask()
        {
            if (glkpTasklist.EditValue != null)
            {
                using (DonorFrontOfficeSystem donorSystem = new DonorFrontOfficeSystem())
                {
                    donorSystem.TagId = this.UtilityMember.NumberSet.ToInteger(glkpTasklist.EditValue.ToString());
                    donorSystem.Status = !string.IsNullOrEmpty(cboStatus.Text) ? cboStatus.SelectedIndex : 2;

                    resultArgs = donorSystem.FetchDonorsByTaskId();

                    if (resultArgs != null && resultArgs.Success)
                    {
                        rgPreviewType.SelectedIndex = -1;
                        gcNewsletter.DataSource = dtNewsLetter = AddSelectColumn(resultArgs.DataSource.Table);
                        gcNewsletter.RefreshDataSource();
                        rgPreviewType.SelectedIndex = 1;
                        resultArgs = donorSystem.FetchNewsLetterByTaskId();
                        if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            NewsLetter = (byte[])resultArgs.DataSource.Table.Rows[0][donorSystem.AppSchema.DonorTags.NEWS_LETTERColumn.ColumnName];
                        }
                    }
                }
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

        /// <summary>
        /// Title of the source
        /// </summary>
        private void SetTitle()
        {
            if (communicationmode == CommunicationMode.MailDesk)
            {
                //this.Text = "News Letter - Mail";
                this.Text = this.GetMessage(MessageCatalog.Networking.DonorMailNewsLetter.DONOR_NEWSLETTER_MAIL_INFO);
                //btnGenerate.Text = "Preview";
                btnGenerate.Text = this.GetMessage(MessageCatalog.Networking.DonorMailNewsLetter.DONOR_NEWSLETTER_MAIL_PREVIEW);
            }
            else
            {
                //this.Text = "News Letter - SMS";
                this.Text = this.GetMessage(MessageCatalog.Networking.DonorMailNewsLetter.DONOR_NEWSLETTER_SMS_CAPTION);
                //btnGenerate.Text = "Preview";
                btnGenerate.Text = this.GetMessage(MessageCatalog.Networking.DonorMailNewsLetter.DONOR_NEWSLETTER_SMS_PREVIEW);
            }
        }
        #endregion

        private void rgPreviewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rgPreviewType.SelectedIndex == 0 || rgPreviewType.SelectedIndex == 1)
            {
                if (dtNewsLetter != null && dtNewsLetter.Rows.Count > 0)
                {
                    DataView dvNewsLetter = new DataView(dtNewsLetter);
                    if (rgPreviewType.SelectedIndex == 0)
                    {
                        dvNewsLetter.RowFilter = "ADDRESS<>'Nil' AND ADDRESS<>''";
                    }
                    else if (rgPreviewType.SelectedIndex == 1)
                    {
                        dvNewsLetter.RowFilter = (communicationmode == CommunicationMode.MailDesk) ? "EMAIL<>''" : "[MOBILE NO]<>''";
                    }

                    gcNewsletter.DataSource = dvNewsLetter.ToTable();
                    gcNewsletter.RefreshDataSource();
                    dvNewsLetter.RowFilter = "";
                }
            }
        }

        private void ucNewsLetter_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucNewsLetter_PrintClicked(object sender, EventArgs e)
        {
            //PrintGridViewDetails(gcNewsletter, "News Letter", PrintType.DT, gvNewsLetter, true);
            PrintGridViewDetails(gcNewsletter, this.GetMessage(MessageCatalog.Networking.DonorMailNewsLetter.DONOR_NEWSLETTER_PRINT_CAPTION), PrintType.DT, gvNewsLetter, true);
        }

        private void ucNewsLetter_RefreshClicked(object sender, EventArgs e)
        {
            LoadNewsLetterDonorsByTask();
            loadTaskDetails();
        }
    }
}
