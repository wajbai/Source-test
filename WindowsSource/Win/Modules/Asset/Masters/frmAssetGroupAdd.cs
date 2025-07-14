using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model.ASSET;
using Bosco.Model.Asset;
using Bosco.Model;
using Bosco.DAO.Schema;

namespace ACPP.Modules.Asset.Masters
{
    public partial class frmAssetGroupAdd : frmBaseAdd
    {
        #region Event Decelaration
        public event EventHandler UpdateHeld;
        AppSchemaSet appSchema = new AppSchemaSet();
        #endregion

        #region Variable Decelaration
        int GroupId = 0;
        private ResultArgs resultArgs = null;
        private object mode = null;
        Bosco.Model.AssetStockProduct.IGroup Igroup = null;
        int GroupItemId = 0;
        #endregion

        #region Construction
        public frmAssetGroupAdd()
        {
            InitializeComponent();
        }
        public frmAssetGroupAdd(int AssetGroupId, FinanceModule mod)
            : this()
        {
            mode = mod;
            Igroup = AssetStockFactory.GetGroupInstance(mod);
            GroupId = AssetGroupId;
        }
        #endregion

        #region Events

        private void frmAssetGroup_Load(object sender, EventArgs e)
        {
            SetTittle();
            LoadDepreciationMethods();
            LoadAssetGroups();
            AssignAssetGroupItemDetails();
            if (mode.Equals(FinanceModule.Stock))
            {
                lblDepreciationPercent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lblMethod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.Height = 164 - 25;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateItemDetails())
                {
                    Igroup.GroupId = this.GroupId;
                    Igroup.Name = txtAssGroupName.Text.Trim();
                    Igroup.ParentGroupId = this.UtilityMember.NumberSet.ToInteger(glpUnderGroup.EditValue.ToString());
                    Igroup.ImageId = 1;
                    if (mode.Equals(FinanceModule.Asset))
                    {
                        Igroup.Method = this.UtilityMember.NumberSet.ToInteger(glkDepreciationMethod.EditValue.ToString());
                        Igroup.Depreciation = this.UtilityMember.NumberSet.ToDouble(txtDepreciationPercent.Text.Trim());
                    }

                    resultArgs = Igroup.SaveGroupDetails();
                    if (resultArgs != null && resultArgs.Success && glpUnderGroup.Text.Equals(AssetCategory.Primary.ToString()))
                    {
                        Igroup.GroupId = Igroup.ParentGroupId = UtilityMember.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                        resultArgs = Igroup.SaveGroupDetails();
                    }

                    if (resultArgs.Success)
                    {
                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                        ClearControls();
                        LoadAssetGroups();
                        if (UpdateHeld != null)
                        {
                            UpdateHeld(this, e);
                        }
                        else
                        {
                            txtAssGroupName.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            { }
        }

        private void glkDepreciationMethod_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                LoadDepreciationMethodfrm();
                LoadDepreciationMethods();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void glpUnderGroup_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glpUnderGroup);
        }

        private void txtAssGroupName_Leave_1(object sender, EventArgs e)
        {
            this.SetBorderColor(txtAssGroupName);
        }

        private void txtAssGroupName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtAssGroupName);
        }

        private void glkDepreciationMethod_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkDepreciationMethod);
        }

        private void txtDepreciationPercent_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtDepreciationPercent);
        }

        #endregion

        #region Methods

        private void LoadDepreciationMethodfrm()
        {
            frmDepreciationMethodsAdd depreciationmethod = new frmDepreciationMethodsAdd();
            depreciationmethod.ShowDialog();
        }

        private bool ValidateItemDetails()
        {
            bool isGroupItemTrue = true;
            if (string.IsNullOrEmpty(glpUnderGroup.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetGroup.ASSETGROUP_UNDERGROUP_EMPTY));
                this.SetBorderColor(glpUnderGroup);
                isGroupItemTrue = false;
                this.glpUnderGroup.Focus();
            }
            else if (string.IsNullOrEmpty(txtAssGroupName.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetGroup.ASSETGROUP_NAME_EMPTY));
                this.SetBorderColor(txtAssGroupName);
                isGroupItemTrue = false;
                this.txtAssGroupName.Focus();
            }
            if (lblDepreciationPercent.Visibility.Equals(DevExpress.XtraLayout.Utils.LayoutVisibility.Always))
            {
                if (string.IsNullOrEmpty(glkDepreciationMethod.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetGroup.ASSETGROUP_METHOD_EMPTY));
                    this.SetBorderColor(glkDepreciationMethod);
                    isGroupItemTrue = false;
                    this.glkDepreciationMethod.Focus();
                }
                else if (string.IsNullOrEmpty(txtDepreciationPercent.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetGroup.ASSETGROUP_DEPRECIATION_EMPTY));
                    this.SetBorderColor(txtDepreciationPercent);
                    isGroupItemTrue = false;
                    this.txtDepreciationPercent.Focus();
                }
            }
            return isGroupItemTrue;
        }

        private void ClearControls()
        {
            if (GroupId == 0)
            {
                txtAssGroupName.Text = txtDepreciationPercent.Text = string.Empty;
                glkDepreciationMethod.EditValue = glpUnderGroup.EditValue = null;
            }
            else
            {
                this.Close();
            }
        }

        public void SetTittle()
        {
            this.Text = GroupId == 0 ? this.GetMessage(MessageCatalog.Asset.AssetGroup.ASSETGROUP_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.AssetGroup.ASSETGROUP_EDIT_CAPTION);
        }

        public void LoadDepreciationMethods()
        {
            try
            {
                using (AssetDepreciationSystem depreciationSystem = new AssetDepreciationSystem())
                {
                    resultArgs = depreciationSystem.FetchAll();
                    if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs != null)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkDepreciationMethod, resultArgs.DataSource.Table, depreciationSystem.AppSchema.ASSETDepreciationDetails.NAMEColumn.ColumnName, depreciationSystem.AppSchema.ASSETDepreciationDetails.DEP_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        public void LoadAssetGroups()
        {
            try
            {
                resultArgs = Igroup.FetchGroupDetails();
                if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs != null)
                {
                    using (CommonMethod GetMethod = new CommonMethod())
                    {
                        DataTable dtGroups = GetMethod.AddHeaderColumn(resultArgs.DataSource.Table, appSchema.AppSchema.ASSETGroupDetails.GROUP_IDColumn.ColumnName, appSchema.AppSchema.ASSETGroupDetails.NAMEColumn.ColumnName, "Primary");
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glpUnderGroup, dtGroups, appSchema.AppSchema.ASSETGroupDetails.NAMEColumn.ColumnName, appSchema.AppSchema.ASSETGroupDetails.GROUP_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void AssignAssetGroupItemDetails()
        {
            try
            {
                if (GroupId != 0)
                {
                    Igroup.GroupId = GroupId;
                    Igroup.AssignGroupProperties();
                    txtAssGroupName.Text = Igroup.Name;
                    glpUnderGroup.EditValue = Igroup.ParentGroupId;
                    glkDepreciationMethod.EditValue = Igroup.Method;
                    txtDepreciationPercent.Text = Igroup.Depreciation.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            { }
        }

        #endregion
    }
}
