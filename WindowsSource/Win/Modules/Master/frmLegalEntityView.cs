using System;
using System.Windows.Forms;
using Bosco.Model.UIModel.Master;
using Bosco.Utility;
using Bosco.Model.UIModel;
using DevExpress.XtraBars;
using Bosco.Utility.ConfigSetting;

namespace ACPP.Modules.Master
{
    public partial class frmLegalEntityView : frmFinanceBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        #endregion

        #region Property

        private int _CustomerId = 0;
        private int CustomerId
        {
            get
            {
                RowIndex = gvLegalEntity.FocusedRowHandle;
                _CustomerId = gvLegalEntity.GetFocusedRowCellValue(gvColCustomerId) != null ? this.UtilityMember.NumberSet.ToInteger(gvLegalEntity.GetFocusedRowCellValue(gvColCustomerId).ToString()) : 0;
                return _CustomerId;
            }
        }

        #endregion

        #region Constructor
        public frmLegalEntityView()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void frmLegalEntityView_Load(object sender, EventArgs e)
        {
            //if (!this.UtilityMember.NumberSet.ToInteger(LoginUser.LoginUserId).Equals((int)UserRights.Admin))
            //{
            ApplyUserRights();
            //}
            //  SetRights();
            //this.isEditable = true;
            // ucToolBarBankView.VisibleAddButton = BarItemVisibility.Never;
            //Set Visible false to Add/Edit/Delete based on the License Key file
            this.LockMasters(ucToolBarBankView);
        }

        private void frmLegalEntityView_Activated(object sender, EventArgs e)
        {
            //Added by Carmel Raj
            SetVisibileShortCuts(false, true);
            LoadLegalEntityData();
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(LegalEntity.CreateLegalEntity);
            this.enumUserRights.Add(LegalEntity.EditLegalEntity);
            this.enumUserRights.Add(LegalEntity.DeleteLegalEntity);
            this.enumUserRights.Add(LegalEntity.PrintLegalEntity);
            this.enumUserRights.Add(LegalEntity.ViewLedgalEntity);
            this.ApplyUserRights(ucToolBarBankView, this.enumUserRights, (int)Menus.LegalEntity);
        }

        private void UcLegalEntity_AddClicked(object sender, EventArgs e)
        {
            ShowLegalEntiryForm();
        }

        private void gvLegalEntity_DoubleClick(object sender, EventArgs e)
        {
            ShowEditForm();
        }

        private void UcLegalEntity_DeleteClicked(object sender, EventArgs e)
        {
            DeleteVoucher();
        }

        private void UcLegalEntity_EditClicked(object sender, EventArgs e)
        {
            ShowEditForm();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvLegalEntity.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvLegalEntity, gvColSocietyName);
            }
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadLegalEntityData();
            gvLegalEntity.FocusedRowHandle = RowIndex;
        }

        private void UcLegalEntity_RefreshClicked(object sender, EventArgs e)
        {
            LoadLegalEntityData();
        }

        private void UcLegalEntity_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcLegalEntity, this.GetMessage(MessageCatalog.Master.LegalEntity.LEGAL_ENTITY_PRINT_CAPTION), PrintType.DT, gvLegalEntity, true);
        }
        #endregion

        #region Methods
        private void ShowEditForm()
        {
            if (this.isEditable)
            {
                if (gvLegalEntity.RowCount != 0)
                {
                    if (CustomerId != 0)
                    {
                        ShowLegalEntiryForm(CustomerId);
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
                // this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_USER_RIGHTS));
            }
        }

        private void ShowLegalEntiryForm(int CustomerId = 0)
        {
            try
            {
                if (this.AppSetting.LockMasters == (int)YesNo.No)
                {
                    using (frmLegalEntity frmLegalEntity = new frmLegalEntity(CustomerId))
                    {
                        frmLegalEntity.UpdateHeld += new EventHandler(this.OnUpdateHeld);
                        frmLegalEntity.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }

        }

        private void LoadLegalEntityData()
        {
            using (LegalEntitySystem legalEntity = new LegalEntitySystem())
            {
                resultArgs = legalEntity.FetchLegalEntity();
                if (resultArgs != null && resultArgs.Success)
                {
                    gcLegalEntity.DataSource = resultArgs.DataSource.Table;
                    lblRowCount.Text = resultArgs.DataSource.Table.Rows.Count.ToString();
                }
            }
        }

        private void SetRights()
        {
            try
            {
                using (MasterRightsSystem masterRights = new MasterRightsSystem())
                {
                    masterRights.MasterName = this.Text;
                    if (masterRights.MasterRights() == (int)MasterRights.ReadOnly)
                    {
                        ucToolBarBankView.VisibleAddButton = ucToolBarBankView.VisibleDeleteButton = BarItemVisibility.Never;//= ucToolBarBankView.VisibleEditButton 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void DeleteVoucher()
        {
            try
            {

                if (gvLegalEntity.RowCount != 0)
                {
                    if (CustomerId != 0)
                    {
                        using (LegalEntitySystem legalEntity = new LegalEntitySystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                legalEntity.CustomerId = CustomerId;
                                resultArgs = legalEntity.DeleteLegalEntityData();
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    LoadLegalEntityData();
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
            catch (Exception ee)
            {
                MessageRender.ShowMessage(ee.Message, true);
            }
            finally { }
        }
        #endregion

        private void frmLegalEntityView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void ucToolBarBankView_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLegalEntityView_EnterClicked(object sender, EventArgs e)
        {
            ShowEditForm();
        }
    }
}
