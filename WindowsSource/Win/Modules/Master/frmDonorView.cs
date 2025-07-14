using System;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model.UIModel.Master;
using DevExpress.XtraGrid.Columns;
using ACPP.Modules.Data_Utility;
using Bosco.Model;
using System.Data;

namespace ACPP.Modules.Master
{
    public partial class frmDonorView : frmFinanceBase
    {
        #region Variable Decelartion
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        #endregion

        #region Property
        private int donorId = 0;
        private int DonorId
        {
            get
            {
                RowIndex = gvDonorDetails.FocusedRowHandle;
                donorId = gvDonorDetails.GetFocusedRowCellValue(colDonAudId) != null ? this.UtilityMember.NumberSet.ToInteger(gvDonorDetails.GetFocusedRowCellValue(colDonAudId).ToString()) : 0;
                return donorId;
            }
            set
            {
                donorId = value;
            }
        }
        #endregion

        #region Constructor
        public frmDonorView()
        {
            InitializeComponent();
        }
        #endregion

        #region Events

        /// <summary>
        /// Load the grid with donor information while loading the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void frmDonorView_Load(object sender, EventArgs e)
        {
            colDonorName.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;
            ApplyUserRights();
        }

        private void frmDonorView_Activated(object sender, EventArgs e)
        {
            //Added by Carmel Raj
            SetVisibileShortCuts(false, true);
            LoadRegistrationTypeDetails();
            FetchDonorDetails();
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(Donor.CreateDonor);
            this.enumUserRights.Add(Donor.EditDonor);
            this.enumUserRights.Add(Donor.DeleteDonor);
            this.enumUserRights.Add(Donor.PrintDonor);
            this.enumUserRights.Add(Donor.ViewDonor);
            this.ApplyUserRights(ucDonorView, enumUserRights, (int)Menus.Donor);
        }

        /// <summary>
        /// Invoke Auditor form to add the donor details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucDonorView_AddClicked(object sender, EventArgs e)
        {
            RowIndex = 0;
            ShowDonorForm((int)AddNewRow.NewRow);
        }

        /// <summary>
        /// Invoke Auditor form to edit the donor information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucDonorView_EditClicked(object sender, EventArgs e)
        {
            ShowDonorEditForm();
        }

        /// <summary>
        /// To delete the selected donor information from the grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucDonorView_DeleteClicked(object sender, EventArgs e)
        {
            try
            {
                if (gvDonorDetails.RowCount != 0)
                {
                    if (DonorId != 0)
                    {
                        using (DonorAuditorSystem donorSystem = new DonorAuditorSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                resultArgs = donorSystem.DeleteDonorAuditorDetails(DonorId);
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    FetchDonorDetails();
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

        /// <summary>
        ///  To print the list of donor details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucDonorView_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcDonorDetails, this.GetMessage(MessageCatalog.Master.Donor.DONOR_PRINT_CAPTION), PrintType.DT, gvDonorDetails);
        }

        /// <summary>
        /// Close the currently opened form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucDonorView_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Invoke donor form to edit the donor details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void gvDonorDetails_DoubleClick(object sender, EventArgs e)
        {
            ShowDonorEditForm();
        }

        /// <summary>
        /// To show the number of records available in the grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void gvDonorDetails_RowCountChanged(object sender, EventArgs e)
        {
            lblCount.Text = gvDonorDetails.RowCount.ToString();
        }

        /// <summary>
        /// To enable auto filter row in the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        /// <summary>
        /// Refresh the grid after adding and editing the values. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadRegistrationTypeDetails();
            FetchDonorDetails();
            gvDonorDetails.FocusedRowHandle = RowIndex;
        }
        /// <summary>
        /// To refresh the Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucDonorView_RefreshClicked(object sender, EventArgs e)
        {
            LoadRegistrationTypeDetails();
            FetchDonorDetails();
        }
        #endregion

        #region Methods

        private void LoadRegistrationTypeDetails()
        {
            try
            {
                using (ProspectManagementSystem prospectSystem = new ProspectManagementSystem())
                {
                    resultArgs = prospectSystem.FetchDonorRegistrationType();
                    if (resultArgs.Success)
                    {
                        using (CommonMethod SelectAll = new CommonMethod())
                        {
                            DataTable dtRegistrationType = SelectAll.AddHeaderColumn(resultArgs.DataSource.Table, prospectSystem.AppSchema.DonorRegistrationType.REGISTRATION_TYPE_IDColumn.ColumnName, prospectSystem.AppSchema.DonorRegistrationType.REGISTRATION_TYPEColumn.ColumnName);
                            this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpRegistrationType, dtRegistrationType, prospectSystem.AppSchema.DonorRegistrationType.REGISTRATION_TYPEColumn.ColumnName, prospectSystem.AppSchema.DonorRegistrationType.REGISTRATION_TYPE_IDColumn.ColumnName);
                            glkpRegistrationType.EditValue = glkpRegistrationType.Properties.GetKeyValue(0);
                        }
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
        /// To load donor details in the grid while loading the form
        /// </summary>

        private void FetchDonorDetails()
        {
            try
            {
                using (DonorAuditorSystem donaudSystem = new DonorAuditorSystem())
                {
                    string Status = GetStatusFilter();
                    donaudSystem.StatusFilter = Status;
                    donaudSystem.RegType = (glkpRegistrationType.EditValue != null) ? UtilityMember.NumberSet.ToInteger(glkpRegistrationType.EditValue.ToString()) : 0;
                    resultArgs = donaudSystem.FetchDonorByStatus();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        gcDonorDetails.DataSource = resultArgs.DataSource.Table;
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

        private string GetStatusFilter()
        {
            string StatusFilter = string.Empty;
            if (chkActive.Checked)
            {
                StatusFilter = (int)Status.Active + ",";
            }
            else
            {
                StatusFilter = "" + "";
            }

            if (chkInactive.Checked)
            {
                StatusFilter += (int)Status.Inactive + ",";
            }
            else
            {
                StatusFilter += "" + "";
            }
            return StatusFilter.TrimEnd(',');
        }

        /// <summary>
        /// To show donor form to edit when edit/double clicked.
        /// </summary>

        private void ShowDonorEditForm()
        {
            if (this.isEditable)
            {
                if (gvDonorDetails.RowCount != 0)
                {
                    if (DonorId != 0)
                    {
                        ShowDonorForm(DonorId);
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
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_USER_RIGHTS));
            }
        }

        /// <summary>
        /// To Show the Auditor for to add and edit donor details
        /// </summary>

        private void ShowDonorForm(int donaudId)
        {
            try
            {
                frmDonorAdd frmDonorAdd = new frmDonorAdd(ViewDetails.Donor, donaudId);
                frmDonorAdd.frmParent = this.MdiParent;
                frmDonorAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmDonorAdd.ShowDialog();
                this.donorId = 0;
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            {
            }
        }
        #endregion

        private void frmDonorView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void frmDonorView_EnterClicked(object sender, EventArgs e)
        {
            ShowDonorEditForm();
        }

        private void ucDonorView_DownloadExcel(object sender, EventArgs e)
        {
            using (frmExcelSupport excelSupport = new frmExcelSupport("Donor", MasterImport.Donor))
            {
                excelSupport.UpdateHeld += new EventHandler(OnUpdateHeld);
                excelSupport.ShowDialog();
            }
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvDonorDetails.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvDonorDetails, colDonorName);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            FetchDonorDetails();
        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkActive.Checked && !chkInactive.Checked)
            {
                chkActive.Checked = true;
            }
        }

        private void chkInactive_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkActive.Checked && !chkInactive.Checked)
            {
                chkInactive.Checked = true;
            }
        }

        private void gcDonorDetails_Click(object sender, EventArgs e)
        {

        }
    }
}