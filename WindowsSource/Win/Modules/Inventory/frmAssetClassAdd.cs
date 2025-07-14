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
    public partial class frmAssetClassAdd : frmBaseAdd
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
        AssetClassSystem assetClassSystem = new AssetClassSystem();
        int ClassItemId = 0;
      //  FormMode FrmMode;
        #endregion

        #region Construction
        public frmAssetClassAdd()
        {
            InitializeComponent();
        }
        public frmAssetClassAdd(int AssetClassId)
            : this()
        {
           // mode = mod;
          //  Igroup = AssetStockFactory.GetGroupInstance(mod);
           // FrmMode = Mode;
            ClassId = AssetClassId;
        }
        #endregion

        #region Events

        private void frmAssetGroup_Load(object sender, EventArgs e)
        {
            LoadAssetClass();
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
                    assetClassSystem.AssetClassId = ClassId == 0 ? 0 : this.ClassId;
                    assetClassSystem.Name = txtAssGroupName.Text.Trim();
                    assetClassSystem.ParentClassId = this.UtilityMember.NumberSet.ToInteger(glpUnderGroup.EditValue.ToString());
                    //Igroup.ImageId = 1;
                   // if (mode.Equals(FinanceModule.Asset))
                   // {
                        assetClassSystem.Method = this.UtilityMember.NumberSet.ToInteger(glkDepreciationMethod.EditValue.ToString());
                        assetClassSystem.Depreciation = this.UtilityMember.NumberSet.ToDouble(txtDepreciationPercent.Text.Trim());
                   // }

                    resultArgs = assetClassSystem.SaveClassDetails();
                    if (this.ClassId == 0)
                    {
                        if (resultArgs != null && resultArgs.Success && glpUnderGroup.Text.Equals(AssetCategory.Primary.ToString()))
                        {
                            assetClassSystem.AssetClassId = assetClassSystem.ParentClassId = UtilityMember.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                            resultArgs = assetClassSystem.SaveClassDetails();
                        }
                    }

                    if (resultArgs != null && resultArgs.Success)
                    {
                        this.ClassId = (ClassId == 0) ? UtilityMember.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : this.ClassId;
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
            if (ClassId==0)
            {
                glkDepreciationMethod.Text = txtAssGroupName.Text = txtDepreciationPercent.Text = string.Empty;
            }
            else
            {
                //this.Close();
            }
            LoadAssetClass();
            LoadDepreciationMethods();
            txtAssGroupName.Focus();
        }

        public void SetTittle()
        {
            this.Text = ClassId == 0 ? this.GetMessage(MessageCatalog.Asset.AssetClass.ASSETCLASS_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.AssetClass.ASSETCLASS_EDIT_CAPTION);
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

        public void LoadAssetClass()
        {
            try
            {
                using (AssetClassSystem assetClassSystem = new AssetClassSystem())
                {
                    resultArgs = assetClassSystem.FetchParentClassName();
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
                if (ClassId>0)
                {
                    if (ClassId != 0)
                    {
                        using (AssetClassSystem assetClassSystem = new AssetClassSystem())
                        {
                            assetClassSystem.AssetClassId = ClassId;
                            assetClassSystem.AssignClassProperties();
                            txtAssGroupName.Text = assetClassSystem.Name;
                            glpUnderGroup.EditValue = assetClassSystem.ParentClassId == 1 ? 0 : Igroup.ParentClassId;
                            glkDepreciationMethod.EditValue = assetClassSystem.Method;
                            txtDepreciationPercent.Text = assetClassSystem.Depreciation.ToString();
                            if (ClassId == UtilityMember.NumberSet.ToInteger(glpUnderGroup.EditValue.ToString()))
                            {
                                glpUnderGroup.EditValue = 0;
                            }
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
