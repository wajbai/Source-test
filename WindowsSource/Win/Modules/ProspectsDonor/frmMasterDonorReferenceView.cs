using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model.Donor;
using Bosco.Utility;

namespace ACPP.Modules.ProspectsDonor
{
    public partial class frmMasterDonorReferenceView : frmFinanceBase
    {
        #region Constructor

        public frmMasterDonorReferenceView()
        {
            InitializeComponent();
        }

        #endregion

        #region Property

        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        private int referredStaffId = 0;
        public int ReferredStaffId
        {
            get
            {
                RowIndex = gvDonorReference.FocusedRowHandle;
                referredStaffId = gvDonorReference.GetFocusedRowCellValue(colRefferedStaffID) != null ? this.UtilityMember.NumberSet.ToInteger(gvDonorReference.GetFocusedRowCellValue(colRefferedStaffID).ToString()) : 0;
                return referredStaffId;
            }
            set
            {
                referredStaffId = value;
            }
        }

        #endregion

        #region Events

        private void frmMasterDonorReferenceView_Load(object sender, EventArgs e)
        {
            LoadReferredStaffDetails();
            SetTitle();
        }

        private void ucToolBar1_RefreshClicked(object sender, EventArgs e)
        {
            LoadReferredStaffDetails();
        }

        private void ucToolBar1_EditClicked(object sender, EventArgs e)
        {
            EditReferreStaff();
        }

        private void ucToolBar1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void ucToolBar1_AddClicked(object sender, EventArgs e)
        {
            ShowForm((int)AddNewRow.NewRow);
        }

        private void ucToolBar1_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcDonorReference, this.GetMessage(MessageCatalog.Master.Donor.DONOR_REFERRED_STAFF_DETAILS), PrintType.DT, gvDonorReference);
        }

        private void frmMasterDonorReferenceView_DoubleClick(object sender, EventArgs e)
        {

        }

        private void gcDonorReference_DoubleClick(object sender, EventArgs e)
        {
            EditReferreStaff();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvDonorReference.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvDonorReference, colRefferedStaffName);
            }
        }

        private void ucToolBar1_DeleteClicked(object sender, EventArgs e)
        {
            try
            {
                if (gvDonorReference.RowCount != 0)
                {
                    if (ReferredStaffId > 0)
                    {
                        using (MasterDonorReferenceSystem donorReferenceSystem = new MasterDonorReferenceSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                donorReferenceSystem.RefferedStaffId = ReferredStaffId;
                                resultArgs = donorReferenceSystem.DeleteReferredStaffDetails();
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    LoadReferredStaffDetails();
                                }
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NOSELECTION_FOR_DELETE));
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void gvDonorReference_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvDonorReference.RowCount.ToString();
        }

        private void frmMasterDonorReferenceView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;

        }

        #endregion

        #region Methods

        private void LoadReferredStaffDetails()
        {
            using (MasterDonorReferenceSystem donorReferenceSystem = new MasterDonorReferenceSystem())
            {
                resultArgs = donorReferenceSystem.FetchReferedStaffDetails();
                if (resultArgs.Success)
                {
                    gcDonorReference.DataSource = resultArgs.DataSource.Table;
                    gcDonorReference.RefreshDataSource();
                }
            }
        }

        private void SetTitle()
        {
            this.Text = this.GetMessage(MessageCatalog.Networking.NetworkingMasterDonorReference.NETWORKING_MASTER_DONOR_REF_VIEW_CAPTION);
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadReferredStaffDetails();
        }

        public void EditReferreStaff()
        {
            try
            {
                if (gvDonorReference.RowCount != 0)
                {
                    if (ReferredStaffId != 0)
                    {
                        ShowForm(ReferredStaffId);
                    }
                    else
                    {
                        if (!chkShowFilter.Checked)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NOSELECTION_FOR_EDIT));
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                }

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        public void ShowForm(int referredStaffId)
        {
            try
            {
                frmMasterDonorReferenceAdd donorReferenceAdd = new frmMasterDonorReferenceAdd(referredStaffId);
                donorReferenceAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
                donorReferenceAdd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        #endregion

        private void ucToolBar1_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMasterDonorReferenceView_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(false, true);
        }
    }
}