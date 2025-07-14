using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Utility.ConfigSetting;
using Bosco.Model;
using Bosco.Model.Donor;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Commands;

using Bosco.Utility;
using System.Reflection;
using Bosco.Model.UIModel.Master;

namespace ACPP.Modules.ProspectsDonor
{
    public partial class frmDonorMailAppealList : frmFinanceBase
    {
        #region Declaration
        ResultArgs resultArgs = new ResultArgs();
        const string SELECT = "SELECT";
        public CommunicationMode communicationmode { get; set; }
        #endregion

        #region Constructor
        public frmDonorMailAppealList()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        private DataTable dtAppeal = new DataTable();
        private string Donationdate { get; set; }
        private int LetterTypeId { get; set; }
        #endregion

        #region Methods
        private void SetTitle()
        {
            deDate.DateTime = DateTime.Now;
            
            if (communicationmode == CommunicationMode.MailDesk)
            {
                this.Text = "Appeal - Mail";
                btnGenerate.Text = "Preview";
                rgPreviewType.Properties.Items[1].Description = "Email";
            }
            else
            {
                this.Text = "Appeal - SMS";
                btnGenerate.Text = "Preview";
                rgPreviewType.Properties.Items[1].Description = "SMS";
            }
        }
        private void LoadDonorList()
        {
            try
            {
                using (DonorFrontOfficeSystem donorfrontOfficesys = new DonorFrontOfficeSystem())
                {
                    rgPreviewType.SelectedIndex = -1;
                    donorfrontOfficesys.Communicationmode = communicationmode;
                    resultArgs = donorfrontOfficesys.FetchNonPerformingDonors(Donationdate);
                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        gcDonorList.DataSource = resultArgs.DataSource.Table;
                        gcDonorList.DataSource = dtAppeal = AddSelectColumn(resultArgs.DataSource.Table);
                        gcDonorList.RefreshDataSource();
                    }
                    else
                    {
                        gcDonorList.DataSource = null;
                    }
                    rgPreviewType.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBoxError(ex.Message);
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
                //donorfrontoffice.Communicationmode =communicationmode;
                LetterTypeId = donorfrontoffice.FetchTemplateIdByname(DonorMailTemplate.Appeal.ToString());
            }
            return LetterTypeId;
        }
        #endregion

        #region Events
        private void btnApply_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    LoadDonorList();

            //}
            //catch (Exception ex)
            //{
            //    this.ShowMessageBoxError(ex.Message);
            //}
            //finally { }
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                gvDonorList.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
                if (chkShowFilter.Checked)
                {
                    this.SetFocusRowFilter(gvDonorList, colDonor);
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBoxError(ex.Message);
            }
            finally { }
        }

        private void frmDonorList_Load(object sender, EventArgs e)
        {
            SetTitle();
            GetLetterTypeId();
            ApplyRights();
        }

        private void ApplyRights()
        {
            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                lciGenerate.Visibility = (communicationmode == CommunicationMode.MailDesk) ? (CommonMethod.ApplyUserRights((int)AppealMail.PreviewAppealMail) != 0 ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never) :
                    (CommonMethod.ApplyUserRights((int)AppealSMS.PreviewAppealSMS) != 0 ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
            }
        }

        private void deDate_EditValueChanged(object sender, EventArgs e)
        {
            Donationdate = this.UtilityMember.DateSet.ToDate(deDate.Text, false).ToString();
            try
            {
                LoadDonorList();
            }
            catch (Exception ex)
            {
                this.ShowMessageBoxError(ex.Message);
            }
            finally { }
        }
        #endregion

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (gcDonorList.DataSource != null)
            {
                DataTable dtAppealList = (DataTable)gcDonorList.DataSource;
                if (dtAppealList != null && dtAppealList.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtAppealList.Rows)
                    {
                        int index = dtAppealList.Rows.IndexOf(dr);
                        if (gvDonorList.IsDataRow(gvDonorList.GetRowHandle(index)))
                        {
                            dr[SELECT] = chkSelectAll.Checked;
                        }
                    }
                }
                gcDonorList.DataSource = dtAppealList;
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                using (DonorAuditorSystem MailingStatusSystem = new DonorAuditorSystem())
                {
                    DataTable dtSource = gcDonorList.DataSource as DataTable;
                    if (dtSource != null && dtSource.Rows.Count > 0)
                    {
                        DataView dvDonorSelectedDetails = dtSource.AsDataView();
                        dvDonorSelectedDetails.RowFilter = "SELECT=1";
                        dtSource = dvDonorSelectedDetails.ToTable();
                        dvDonorSelectedDetails.RowFilter = "";
                        if (dtSource != null && dtSource.Rows.Count > 0)
                        {
                            if (communicationmode == CommunicationMode.MailDesk)
                            {
                                frmDonorMailMerge frmMailMerge = new frmDonorMailMerge();
                                frmMailMerge.DonorPreviewType = rgPreviewType.SelectedIndex == 0 ? PreviewType.Print : PreviewType.Email;
                                frmMailMerge.TemplateType = DonorMailTemplate.Appeal;
                                frmMailMerge.LetterTypeId = LetterTypeId;
                                frmMailMerge.DataSource = dtSource;
                                frmMailMerge.UpdateHeld += new EventHandler(OnUpdateHeld);
                                frmMailMerge.ShowDialog();
                            }
                            else if (communicationmode == CommunicationMode.ContactDesk)
                            {
                                frmDonorSMSMerge frmMailMerge = new frmDonorSMSMerge();
                                frmMailMerge.DonorPreviewType = rgPreviewType.SelectedIndex == 0 ? PreviewType.Print : PreviewType.Email;
                                frmMailMerge.TemplateType = DonorMailTemplate.Appeal;
                                frmMailMerge.DataSource = dtSource;
                                frmMailMerge.ShowDialog();
                            }
                        }
                        else
                        {
                            this.ShowMessageBoxError("No record is selected to preview");
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
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadDonorList();
        }

        private void gvDonorList_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvDonorList.RowCount.ToString();
        }

        private void deDate_DateTimeChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    LoadDonorList();

            //}
            //catch (Exception ex)
            //{
            //    this.ShowMessageBoxError(ex.Message);
            //}
            //finally { }
        }

        private void frmDonorMailAppealList_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(true, true);
        }

        private void rgPreviewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rgPreviewType.SelectedIndex == 0 || rgPreviewType.SelectedIndex == 1)
            {
                if (dtAppeal != null && dtAppeal.Rows.Count > 0)
                {
                    DataView dvAppeal = new DataView(dtAppeal);
                    if (rgPreviewType.SelectedIndex == 0)
                    {
                        dvAppeal.RowFilter = "ADDRESS<>'Nil' AND ADDRESS<>''";
                    }
                    else if (rgPreviewType.SelectedIndex == 1)
                    {
                        dvAppeal.RowFilter = (communicationmode == CommunicationMode.MailDesk) ? "EMAIL<>''" : "[MOBILE NO]<>''";
                    }

                    gcDonorList.DataSource = dvAppeal.ToTable();
                    gcDonorList.RefreshDataSource();
                    dvAppeal.RowFilter = "";
                }
            }
        }

        private void ucAppeal_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucAppeal_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcDonorList, "Appeal List", PrintType.DT, gvDonorList, true);
        }

        private void ucAppeal_RefreshClicked(object sender, EventArgs e)
        {
            LoadDonorList();
        }
    }
}