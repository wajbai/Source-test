using System;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model.UIModel.Master;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraEditors.Controls;
using Bosco.Model.UIModel;
using Bosco.DAO.Schema;
using System.Net.Mail;
using ACPP.Modules.Master;
using ACPP.Modules.Data_Utility;
using ACPP.Modules.UIControls;
using Bosco.Model.Donor;
using Bosco.Model;
using DevExpress.XtraGrid.Columns;
using System.Data;

namespace ACPP.Modules.ProspectsDonor
{
    public partial class frmProspectsView : frmFinanceBase
    {
        #region Variable Decelartion
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        #endregion

        #region Property
        private int prospectId = 0;
        private int ProspectId
        {
            get
            {
                RowIndex = gvProspects.FocusedRowHandle;
                prospectId = gvProspects.GetFocusedRowCellValue(colProspectId) != null ? this.UtilityMember.NumberSet.ToInteger(gvProspects.GetFocusedRowCellValue(colProspectId).ToString()) : 0;
                return prospectId;
            }
            set
            {
                prospectId = value;
            }
        }
        #endregion

        #region Constructor
        public frmProspectsView()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void frmProspectsView_Load(object sender, EventArgs e)
        {
            ApplyUserRights();
            colName.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;
            ucProspectsToolbar.ChnageRenewCaption = "Convert as Donor";
            ucProspectsToolbar.ChnageRenewCaption = this.GetMessage(MessageCatalog.Networking.DonorProspects.DONOR_PROSPECTS_CONVERT_DONOR_CAPTION);
            SetTitle();
            
            if (this.UtilityMember.NumberSet.ToInteger(LoginUser.LoginUserId) == (int)UserRights.Admin)
            {
                ucProspectsToolbar.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Always;
                ucProspectsToolbar.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Always;
                ucProspectsToolbar.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(Prospect.ConvertProspect);
            this.enumUserRights.Add(Prospect.CreateProspect);
            this.enumUserRights.Add(Prospect.DeleteProspect);
            this.enumUserRights.Add(Prospect.EditProspect);
            this.enumUserRights.Add(Prospect.ImportProspect);
            this.enumUserRights.Add(Prospect.PrintProspect);
            this.enumUserRights.Add(Prospect.ViewProspect);

            this.ApplyUserRights(ucProspectsToolbar, enumUserRights, (int)Menus.Prospect);
        }

        private void frmProspectsView_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(false, true);
            LoadRegistrationTypeDetails();
            FetchProspectDetails();
        }


        private void ucProspectsToolbar_RefreshClicked(object sender, EventArgs e)
        {
            FetchProspectDetails();
            LoadRegistrationTypeDetails();
        }

        private void ucProspectsToolbar_DownloadExcel(object sender, EventArgs e)
        {
            using (frmExcelSupport excelSupport = new frmExcelSupport("Prospects", MasterImport.Prospects))
            {
                excelSupport.UpdateHeld += new EventHandler(OnUpdateHeld);
                excelSupport.ShowDialog();
            }

        }

        private void ucProspectsToolbar_EditClicked(object sender, EventArgs e)
        {
            ShowProspectEditForm();
        }

        private void ucProspectsToolbar_DeleteClicked(object sender, EventArgs e)
        {
            try
            {
                if (gvProspects.RowCount != 0)
                {
                    if (ProspectId > 0)
                    {
                        using (ProspectManagementSystem prospectSystem = new ProspectManagementSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                prospectSystem.ProspectId = ProspectId;
                                resultArgs = prospectSystem.DeleteDonorProspectDetails();
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    FetchProspectDetails();
                                    LoadRegistrationTypeDetails();
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

        private void ucProspectsToolbar_AddClicked(object sender, EventArgs e)
        {
            RowIndex = 0;
            ShowProspectForm((int)AddNewRow.NewRow);

        }

        private void ucProspectsToolbar_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcProspects, this.GetMessage(MessageCatalog.Master.Donor.DONOR_PRINT_CAPTION), PrintType.DT, gvProspects);

        }
        private void frmProspectsView_EnterClicked(object sender, EventArgs e)
        {
            ShowProspectEditForm();
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchProspectDetails();
            LoadRegistrationTypeDetails();
            gvProspects.FocusedRowHandle = RowIndex;
        }

        private void gvProspects_DoubleClick(object sender, EventArgs e)
        {
            ShowProspectEditForm();
        }

        private void ucProspectsToolbar_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvProspects.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvProspects, colName);
            }
        }

        private void gvProspects_RowCountChanged(object sender, EventArgs e)
        {
            lblRowCount.Text = gvProspects.RowCount.ToString();
        }

        private void frmProspectsView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }
        #endregion

        #region Methods

        private void SetTitle()
        {
            this.Text = this.GetMessage(MessageCatalog.Networking.DonorProspects.DONOR_PROSPECTS_VIEW_CAPTION);
        }

        private void FetchProspectDetails()
        {
            try
            {
                using (ProspectManagementSystem prospectSystem = new ProspectManagementSystem())
                {
                    resultArgs = prospectSystem.FetchProspectsDetails();
                    gcProspects.DataSource = resultArgs.DataSource.Table;
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


        private void ShowProspectForm(int prospectId)
        {
            try
            {
                frmProspectAdd frmProspectAdd = new frmProspectAdd(prospectId);
                frmProspectAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmProspectAdd.ShowDialog();
                this.ProspectId = 0;
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            {
            }
        }

        private void ShowProspectEditForm()
        {
            if (gvProspects.RowCount != 0)
            {
                if (ProspectId > 0)
                {
                    ShowProspectForm(ProspectId);
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

        #endregion

        private void ucProspectsToolbar_RenewClicked(object sender, EventArgs e)
        {
            try
            {
                if (gvProspects.RowCount != 0)
                {
                    if (ProspectId > 0)
                    {
                        frmDonorAdd frmDonorAdd = new frmDonorAdd();
                        frmDonorAdd.ProspectId = ProspectId;
                        frmDonorAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
                        frmDonorAdd.ShowDialog();
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

        private void btnApply_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    using (ProspectManagementSystem prospectSystem = new ProspectManagementSystem())
            //    {
            //        prospectSystem.RegType = UtilityMember.NumberSet.ToInteger(glkpRegistrationType.EditValue.ToString());
            //        resultArgs = prospectSystem.FetchDonorByRegistrationType();
            //        gcProspects.DataSource = resultArgs.DataSource.Table;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageRender.ShowMessage(ex.ToString(), true);
            //}
            //finally
            //{
            //}
        }

        private void glkpRegistrationType_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                using (ProspectManagementSystem prospectSystem = new ProspectManagementSystem())
                {
                    prospectSystem.RegType = UtilityMember.NumberSet.ToInteger(glkpRegistrationType.EditValue.ToString());
                    resultArgs = prospectSystem.FetchDonorByRegistrationType();
                    gcProspects.DataSource = resultArgs.DataSource.Table;
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
    }
}