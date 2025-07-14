using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model;
using Bosco.DAO.Schema;
using ACPP.Modules.Inventory.Asset;

namespace ACPP.Modules.Inventory
{
    public partial class frmGroupAdd : frmBaseAdd
    {
        #region Event Decelaration
        public event EventHandler UpdateHeld;
        AppSchemaSet appSchema = new AppSchemaSet();
        #endregion

        #region Variable Decelaration
        public int ClassId { get; set; }
        private ResultArgs resultArgs = null;
        private object mode = null;
        AssetStockProduct.IGroup Igroup = null;
        int ClassItemId = 0;
        FormMode FrmMode;
        #endregion

        #region Construction
        public frmGroupAdd()
        {
            InitializeComponent();
        }
        public frmGroupAdd(int AssetClassId, FinanceModule mod, FormMode Mode)
            : this()
        {
            mode = mod;
            Igroup = AssetStockFactory.GetGroupInstance(mod);
            FrmMode = Mode;
            ClassId = AssetClassId;
        }
        #endregion

        #region Events

        private void frmAssetGroup_Load(object sender, EventArgs e)
        {
            LoadAssetGroups();
            SetTittle();
            LoadDepreciationMethods();
            AssignAToProperties();
            if (mode.Equals(FinanceModule.Stock))
            {
                lblDepreciationPercent.Visibility = emptySpaceItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lblMethod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.Height = 164 - 38;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateItemDetails())
                {
                    Igroup.AssetClassId = FrmMode == 0 ? 0 : this.ClassId;
                    Igroup.Name = txtAssGroupName.Text.Trim();
                    Igroup.ParentClassId = this.UtilityMember.NumberSet.ToInteger(glpUnderGroup.EditValue.ToString());
                    //Igroup.ImageId = 1;
                    if (mode.Equals(FinanceModule.Asset))
                    {
                        Igroup.Method = this.UtilityMember.NumberSet.ToInteger(glkDepreciationMethod.EditValue.ToString());
                        Igroup.Depreciation = this.UtilityMember.NumberSet.ToDouble(txtDepreciationPercent.Text.Trim());
                    }

                    resultArgs = Igroup.SaveClassDetails();
                    if (this.ClassId == 0)
                    {
                        if (resultArgs != null && resultArgs.Success && glpUnderGroup.Text.Equals(AssetCategory.Primary.ToString()))
                        {
                            Igroup.AssetClassId = Igroup.ParentClassId = UtilityMember.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                            resultArgs = Igroup.SaveClassDetails();
                        }
                    }

                    if (resultArgs != null && resultArgs.Success)
                    {
                        this.ClassId = (FrmMode == 0) ? UtilityMember.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : this.ClassId;
                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                        ClearControls();
                        if (UpdateHeld != null)
                        {
                            UpdateHeld(this, e);
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
            txtAssGroupName.Text = this.UtilityMember.StringSet.ToSentenceCase(txtAssGroupName.Text.Trim());
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
            bool isClassItemTrue = true;
            if (string.IsNullOrEmpty(glpUnderGroup.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetClass.ASSETCLASS_UNDERGROUP_EMPTY));
                this.SetBorderColor(glpUnderGroup);
                isClassItemTrue = false;
                this.glpUnderGroup.Focus();
            }
            else if (string.IsNullOrEmpty(txtAssGroupName.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetClass.ASSETCLASS_NAME_EMPTY));
                this.SetBorderColor(txtAssGroupName);
                isClassItemTrue = false;
                this.txtAssGroupName.Focus();
            }

            else if (lblDepreciationPercent.Visibility.Equals(DevExpress.XtraLayout.Utils.LayoutVisibility.Always))
            {
                if (string.IsNullOrEmpty(glkDepreciationMethod.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetClass.ASSETCLASS_METHOD_EMPTY));
                    this.SetBorderColor(glkDepreciationMethod);
                    isClassItemTrue = false;
                    this.glkDepreciationMethod.Focus();
                }
                else if (string.IsNullOrEmpty(txtDepreciationPercent.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetClass.ASSETCLASS_DEPRECIATION_EMPTY));
                    this.SetBorderColor(txtDepreciationPercent);
                    isClassItemTrue = false;
                    this.txtDepreciationPercent.Focus();
                }
            }
            return isClassItemTrue;
        }
        private bool ValidateGroupLevel()
        {
            bool IsGroupLevel = true;
            //using (AssetGroupSystem groupSystem = new AssetGroupSystem())
            //{
            //    resultArgs = groupSystem.ValidateGroupId(this.UtilityMember.NumberSet.ToInteger(glpUnderGroup.EditValue.ToString()));
            //    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            //    {
            //        if (resultArgs.DataSource.Table.Rows[0][groupSystem.AppSchema.ASSETGroupDetails.PARENT_GROUP_IDColumn.ColumnName].ToString() != resultArgs.DataSource.Table.Rows[0][groupSystem.AppSchema.LedgerGroup.NATURE_IDColumn.ColumnName].ToString())
            //        {
            //            IsGroupLevel = false;
            //        }
            //    }
            //}
            return IsGroupLevel;
        }
        private void ClearControls()
        {
            if (FrmMode.Equals(FormMode.Add))
            {
                glkDepreciationMethod.Text = txtAssGroupName.Text = txtDepreciationPercent.Text = string.Empty;
            }
            else
            {
                //this.Close();
            }
            LoadAssetGroups();
            LoadDepreciationMethods();
            txtAssGroupName.Focus();
        }

        public void SetTittle()
        {
            this.Text = FrmMode == 0 ? this.GetMessage(MessageCatalog.Asset.AssetClass.ASSETCLASS_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.AssetClass.ASSETCLASS_EDIT_CAPTION);
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
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkDepreciationMethod, resultArgs.DataSource.Table, depreciationSystem.AppSchema.ASSETDepreciationDetails.NAMEColumn.ColumnName, depreciationSystem.AppSchema.ASSETDepreciationDetails.METHOD_IDColumn.ColumnName);
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
                using (AssetClassSystem assetGroupSystem = new AssetClassSystem())
                {
                    resultArgs = assetGroupSystem.FetchParentClassName();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        using (CommonMethod GetMethod = new CommonMethod())
                        {
                            // DataTable dtGroups = GetMethod.AddHeaderColumn(resultArgs.DataSource.Table, appSchema.AppSchema.ASSETGroupDetails.GROUP_IDColumn.ColumnName, appSchema.AppSchema.ASSETGroupDetails.GROUP_NAMEColumn.ColumnName,"Primary");
                            DataTable dtGroups = GetMethod.AddHeaderColumn(resultArgs.DataSource.Table, appSchema.AppSchema.ASSETClassDetails.ASSET_CLASS_IDColumn.ColumnName, appSchema.AppSchema.ASSETClassDetails.CLASS_NAMEColumn.ColumnName, "Primary");
                            this.UtilityMember.ComboSet.BindGridLookUpCombo(glpUnderGroup, dtGroups, appSchema.AppSchema.ASSETClassDetails.CLASS_NAMEColumn.ColumnName, appSchema.AppSchema.ASSETClassDetails.ASSET_CLASS_IDColumn.ColumnName);
                          
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void AssignAToProperties()
        {
            try
            {
                if (FrmMode == FormMode.Edit)
                {
                    if (ClassId != 0)
                    {
                        Igroup.AssetClassId = ClassId;
                        Igroup.AssignClassProperties();
                        txtAssGroupName.Text = Igroup.Name;
                        glpUnderGroup.EditValue = Igroup.ParentClassId == 1 ? 0 : Igroup.ParentClassId;
                        glkDepreciationMethod.EditValue = Igroup.Method;
                        txtDepreciationPercent.Text = Igroup.Depreciation.ToString();
                        if (ClassId == UtilityMember.NumberSet.ToInteger(glpUnderGroup.EditValue.ToString()))
                        {
                            glpUnderGroup.EditValue = 0;
                        }
                    }
                }
                else
                {
                    object editval = glpUnderGroup.Properties.GetKeyValue(ClassId);
                    if (editval != null)
                        glpUnderGroup.EditValue = this.ClassId;
                    else
                        glpUnderGroup.EditValue = glpUnderGroup.Properties.GetKeyValue(0);
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
