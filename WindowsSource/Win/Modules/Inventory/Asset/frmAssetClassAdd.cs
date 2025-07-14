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
    public partial class frmAssetClassAdd : frmFinanceBaseAdd
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
        FormMode FrmMode;
        #endregion

        #region Construction
        public frmAssetClassAdd()
        {
            InitializeComponent();
        }
        public frmAssetClassAdd(int AssetClassId, FormMode Mode)
            : this()
        {
            // mode = mod;
            //  Igroup = AssetStockFactory.GetGroupInstance(mod);
            FrmMode = Mode;
            ClassId = AssetClassId;
        }
        #endregion

        #region Events
        private void frmAssetClassAdd_Load(object sender, EventArgs e)
        {
            LoadAssetClass();
            SetTittle();
            LoadDepreciationMethods();
            AssignAToProperties();
            //if (mode.Equals(FinanceModule.Stock))
            //{
            //    lblDepreciationPercent.Visibility = emptySpaceItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    lblMethod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    this.Height = 164 - 38;
            //}
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateItemDetails())
                {
                    assetClassSystem.AssetClassId = FrmMode == 0 ? 0 : this.ClassId;
                    assetClassSystem.AssetClass = txtAssetClassName.Text.Trim();
                    assetClassSystem.ParentClassId = this.UtilityMember.NumberSet.ToInteger(glkpParentClassName.EditValue.ToString());
                    // Igroup.ImageId = 1;
                    // if (mode.Equals(FinanceModule.Asset))
                    // {
                    assetClassSystem.Method = this.UtilityMember.NumberSet.ToInteger(glkDepreciationMethod.EditValue.ToString());
                    assetClassSystem.Depreciation = this.UtilityMember.NumberSet.ToDouble(txtDepreciationPercent.Text.Trim());
                    // }

                    resultArgs = assetClassSystem.SaveClassDetails();
                    this.ReturnValue = resultArgs.RowUniqueId;
                    this.ReturnDialog = System.Windows.Forms.DialogResult.OK;

                    if (this.ClassId == 0)
                    {
                        if (resultArgs != null && resultArgs.Success && glkpParentClassName.Text.Equals(AssetCategory.Primary.ToString()))
                        {
                            this.ReturnValue = resultArgs.RowUniqueId;
                            this.ReturnDialog = System.Windows.Forms.DialogResult.OK;
                            assetClassSystem.AssetClassId = assetClassSystem.ParentClassId = UtilityMember.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                            resultArgs = assetClassSystem.SaveClassDetails();
                        }
                    }

                    if (resultArgs != null && resultArgs.Success)
                    {
                        this.ReturnValue = resultArgs.RowUniqueId;
                        this.ReturnDialog = System.Windows.Forms.DialogResult.OK;
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

        private void glkDepreciationMethod_Leave(object sender, EventArgs e)
        {
          //  this.SetBorderColor(glkDepreciationMethod);
        }

        private void txtDepreciationPercent_Leave(object sender, EventArgs e)
        {
            //this.SetBorderColor(txtDepreciationPercent);
        }

        private void glkpParentClassName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpParentClassName);
        }

        private void txtAssetClass_Leave_1(object sender, EventArgs e)
        {
            this.SetBorderColor(txtAssetClassName);
            txtAssetClassName.Text = this.UtilityMember.StringSet.ToSentenceCase(txtAssetClassName.Text.Trim());
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
            if (string.IsNullOrEmpty(glkpParentClassName.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetClass.ASSETCLASS_PARENTCLASS_EMPTY));
                this.SetBorderColor(glkpParentClassName);
                isClassItemTrue = false;
                this.glkpParentClassName.Focus();
            }
            else if (string.IsNullOrEmpty(txtAssetClassName.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetClass.ASSETCLASS_NAME_EMPTY));
                this.SetBorderColor(txtAssetClassName);
                isClassItemTrue = false;
                this.txtAssetClassName.Focus();
            }

            //else if (lblDepreciationPercent.Visibility.Equals(DevExpress.XtraLayout.Utils.LayoutVisibility.Always))
            //{
            //    if (string.IsNullOrEmpty(glkDepreciationMethod.Text))
            //    {
            //        this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetClass.ASSETCLASS_METHOD_EMPTY));
            //        this.SetBorderColor(glkDepreciationMethod);
            //        isClassItemTrue = false;
            //        this.glkDepreciationMethod.Focus();
            //    }
            //    else if (string.IsNullOrEmpty(txtDepreciationPercent.Text))
            //    {
            //        this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetClass.ASSETCLASS_DEPRECIATION_EMPTY));
            //        this.SetBorderColor(txtDepreciationPercent);
            //        isClassItemTrue = false;
            //        this.txtDepreciationPercent.Focus();
            //    }
            //}
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
            if (FrmMode == FormMode.Add)
            {
                glkDepreciationMethod.Text = txtAssetClassName.Text = txtDepreciationPercent.Text = string.Empty;
                txtAssetClassName.Focus();
            }
            else
            {
           //     this.Close();
            }
            LoadAssetClass();
            LoadDepreciationMethods();
            // txtAssetClassName.Focus();
        }

        public void SetTittle()
        {
            this.Text = FrmMode == FormMode.Add ? this.GetMessage(MessageCatalog.Asset.AssetClass.ASSETCLASS_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.AssetClass.ASSETCLASS_EDIT_CAPTION);
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
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkDepreciationMethod, resultArgs.DataSource.Table, depreciationSystem.AppSchema.ASSETDepreciationDetails.DEP_METHODColumn.ColumnName, depreciationSystem.AppSchema.ASSETDepreciationDetails.METHOD_IDColumn.ColumnName);
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
                        // DataTable dtAssetClass = GetMethod.AddHeaderColumn(resultArgs.DataSource.Table, appSchema.AppSchema.ASSETClassDetails.ASSET_CLASS_IDColumn.ColumnName, appSchema.AppSchema.ASSETClassDetails.ASSET_CLASSColumn.ColumnName, "Primary");
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpParentClassName, resultArgs.DataSource.Table, assetClassSystem.AppSchema.ASSETClassDetails.ASSET_CLASSColumn.ColumnName, assetClassSystem.AppSchema.ASSETClassDetails.ASSET_CLASS_IDColumn.ColumnName);
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
                        using (AssetClassSystem assetClassSystem = new AssetClassSystem())
                        {
                            assetClassSystem.AssetClassId = ClassId;
                            assetClassSystem.AssignClassProperties();
                            txtAssetClassName.Text = assetClassSystem.AssetClass;
                            glkpParentClassName.EditValue = assetClassSystem.ParentClassId == 1 ? 0 : assetClassSystem.ParentClassId;
                            glkDepreciationMethod.EditValue = assetClassSystem.Method;
                            txtDepreciationPercent.Text = assetClassSystem.Depreciation.ToString();
                            //if (ClassId == UtilityMember.NumberSet.ToInteger(glkpParentClassName.EditValue.ToString()))
                            //{
                            //    glkpParentClassName.EditValue = 0;
                            //}
                            if (UtilityMember.NumberSet.ToInteger(glkpParentClassName.EditValue.ToString())==0)
                            {
                                glkpParentClassName.EditValue = 1;
                            }
                        }
                    }
                }
                else
                {
                    //object editval = glkpParentClassName.Properties.GetKeyValue(ClassId);
                    //if (editval != null)
                    //    glkpParentClassName.EditValue = ClassId.ToString();
                    //else
                    //    glkpParentClassName.EditValue = glkpParentClassName.Properties.GetKeyValue(0);
                    glkpParentClassName.EditValue = ClassId.ToString();
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

        private void glkpParentClassName_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
