using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model.UIModel;
using ACPP.Modules.Master;
using ACPP.Modules;
using System.Text.RegularExpressions;
using Bosco.Model.ASSET;
using Bosco.Model.Asset;

namespace ACPP.Modules.Asset.Masters
{
    public partial class frmAssetItemAdd : frmBaseAdd
    {
        #region Properties

        public event EventHandler UpdateHeld;
        private ResultArgs resultArgs = null;
        int AsstItemId = 0;

        #endregion
       
        #region Constructor
        public frmAssetItemAdd()
        {
            InitializeComponent();
        }
        public frmAssetItemAdd(int ItemId)
            : this()
        {
            AsstItemId = ItemId;
        }
        #endregion

        #region Events
      
        private void glpAssetGroup_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                frmAssetGroupAdd objAssetGroup = new frmAssetGroupAdd(0,FinanceModule.Asset);
                objAssetGroup.ShowDialog();
                LoadAssetGrouptoCombo();
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAssetItemCreation_Load(object sender, EventArgs e)
        {
            SetTittle();
            //  LoadDepreciationLedgerById();
            LoadDefaultLedgers();
            LoadAssetGrouptoCombo();
            LoadAssetCategory();
            LoadAssetUnitOfMeasure();
            LoadMethods();
            LoadItemKinds();
            AssignValuesToControls();
        }
       
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateIteamDetails())
                {
                    ResultArgs resultArgs = null;
                    using (AssetItemSystem assetItemSystem = new AssetItemSystem())
                    {
                        assetItemSystem.ItemId = AsstItemId == 0 ? this.UtilityMember.NumberSet.ToInteger(AddNewRow.NewRow.ToString()) : AsstItemId;
                        assetItemSystem.AssetGroupId = this.UtilityMember.NumberSet.ToInteger(glpAssetGroup.EditValue.ToString());
                        assetItemSystem.DepreciationLedger = this.UtilityMember.NumberSet.ToInteger(glkpDepreciationLedger.EditValue.ToString());
                        assetItemSystem.AccountLeger = this.UtilityMember.NumberSet.ToInteger(glkpAssetAccountLedger.EditValue.ToString());
                        assetItemSystem.DisposalLedger = this.UtilityMember.NumberSet.ToInteger(glkpDisposalLedger.EditValue.ToString());
                        assetItemSystem.Catogery = this.UtilityMember.NumberSet.ToInteger(glkpCategories.EditValue.ToString());
                        assetItemSystem.Name = txtName.Text.Trim();
                        assetItemSystem.ItemKind = glkpItemKind.Text.Trim();
                        assetItemSystem.Unit = this.UtilityMember.NumberSet.ToInteger(glkpUnit.EditValue.ToString());
                        assetItemSystem.Method = lkpMethod.Text.Trim();
                        assetItemSystem.Prefix = txtPrefix.Text.Trim();
                        assetItemSystem.Suffix = txtSuffix.Text.Trim();
                        assetItemSystem.StartingNo = this.UtilityMember.NumberSet.ToInteger(txtStartingNo.Text.Trim());
                        assetItemSystem.Quantity = this.UtilityMember.NumberSet.ToInteger(txtQuantity.Text.Trim());
                        assetItemSystem.RatePerItem = this.UtilityMember.NumberSet.ToDecimal(txtQuantity.Text.Trim());
                        assetItemSystem.Total = this.UtilityMember.NumberSet.ToDecimal(txtTotal.Text.Trim());
                        resultArgs = assetItemSystem.SaveItemDetails();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            ClearControls();
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                        }
                        else
                        {
                            glpAssetGroup.Focus();
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

        private void glpAssetGroup_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glpAssetGroup);
        }

        private void glkpDepreciationLedger_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpDepreciationLedger);
        }

        private void cmbDisposalLedger_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpDisposalLedger);
        }

        private void cmbAssetAccountLedger_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpAssetAccountLedger);
        }

        private void cmbCategories_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpCategories);
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtName);
        }

        private void cmbItemKind_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpItemKind);
        }

        private void cmbUnit_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpUnit);
        }

        private void lkpMethod_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(lkpMethod);            
        }

        private void txtStartingNo_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtStartingNo);
        }

        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtQuantity);
        }

        private void txtRatePerItem_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtRatePerItem);
        }

        private void glkpDisposalLedger_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpDisposalLedger);
        }

        private void glkpAssetAccountLedger_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpAssetAccountLedger);
        }

        private void glkpCategories_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpCategories);
        }

        private void glkpCategories_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                frmAssetCategoriesAdd objAssetCategory = new frmAssetCategoriesAdd(0,FinanceModule.Asset);
                objAssetCategory.ShowDialog();
                LoadAssetCategory();
            }
        }

        private void glkpUnit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                frmUnitofMeasureAdd objUnitOfMeasuerAdd = new frmUnitofMeasureAdd(0,FinanceModule.Asset);
                objUnitOfMeasuerAdd.ShowDialog();
                LoadAssetUnitOfMeasure();
            }

        }

        private void lkpMethod_EditValueChanged(object sender, EventArgs e)
        {
            string method = lkpMethod.EditValue.ToString();
            int type = (int)TransactionVoucherMethod.Automatic;
            if (method.Equals(type.ToString()))
            {
                txtPrefix.Enabled = txtSuffix.Enabled = txtStartingNo.Enabled = false;
                txtPrefix.Text = txtSuffix.Text = string.Empty;
                txtStartingNo.Text = "0";
            }
            else
            {
                txtPrefix.Enabled = txtSuffix.Enabled = txtStartingNo.Enabled = true;
            }
        }

        private void txtQuantity_EditValueChanged(object sender, EventArgs e)
        {
            txtTotal.Text = (this.UtilityMember.NumberSet.ToInteger(txtQuantity.Text) * this.UtilityMember.NumberSet.ToInteger(txtRatePerItem.Text)).ToString();
        }

        private void txtRatePerItem_EditValueChanged(object sender, EventArgs e)
        {
            txtTotal.Text = (this.UtilityMember.NumberSet.ToInteger(txtQuantity.Text) * this.UtilityMember.NumberSet.ToInteger(txtRatePerItem.Text)).ToString();
        }

        private void txtQuantity_EditValueChanged_1(object sender, EventArgs e)
        {
            Double TotalAmt = (this.UtilityMember.NumberSet.ToDouble(txtQuantity.Text) * this.UtilityMember.NumberSet.ToDouble(txtRatePerItem.Text));
            txtTotal.Text = TotalAmt.ToString();
        }

        private void txtRatePerItem_EditValueChanged_1(object sender, EventArgs e)
        {
            Double TotalAmt = (this.UtilityMember.NumberSet.ToDouble(txtQuantity.Text) * this.UtilityMember.NumberSet.ToDouble(txtRatePerItem.Text));
            txtTotal.Text = TotalAmt.ToString();
        }
        
        #endregion Events

        #region Methods

        //private void LoadDepreciationLedgerById()
        //{
        //    try
        //    {
        //        using (AssetItemSystem assItemSystem = new AssetItemSystem())
        //        {
        //            //resultArgs = assItemSystem.FetchDepreciationLedgerById();
        //            if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs != null)
        //            {
        //                this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpDepreciationLedger, resultArgs.DataSource.Table, assItemSystem.AppSchema.ASSETDepreciationDetails.NAMEColumn.ColumnName, assItemSystem.AppSchema.ASSETDepreciationDetails.DEP_IDColumn.ColumnName);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    { }

        //}
        /// <summary>
        /// Loding jenral ledgers in the AccountLedger,DepreciationLedger,DisposalLedger controls.
        /// </summary>
        private void LoadDefaultLedgers()
        {
            try
            {
                using (AssetItemSystem assetItemSystem = new AssetItemSystem())
                {
                    resultArgs = assetItemSystem.FetchDefaultLedgers();
                    if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs != null)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpDepreciationLedger, resultArgs.DataSource.Table, assetItemSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, assetItemSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpAssetAccountLedger, resultArgs.DataSource.Table, assetItemSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, assetItemSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpDisposalLedger, resultArgs.DataSource.Table, assetItemSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, assetItemSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
            }

        }
        /// <summary>
        /// Loading Asset Group in the drop down control.
        /// </summary>
        private void LoadAssetGrouptoCombo()
        {
            try
            {
                using (AssetGroupSystem assetGroupSystem = new AssetGroupSystem())
                {
                    resultArgs = assetGroupSystem.FetchGroupDetails();
                    if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs != null)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glpAssetGroup, resultArgs.DataSource.Table, assetGroupSystem.AppSchema.ASSETGroupDetails.NAMEColumn.ColumnName, assetGroupSystem.AppSchema.ASSETGroupDetails.GROUP_IDColumn.ColumnName);
                    }
                }

            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Loading asset category in the drop down control.
        /// </summary>
        private void LoadAssetCategory()
        {
            try
            {
                using (AssetCategorySystem assetCategorySystem = new AssetCategorySystem())
                {
                    resultArgs = assetCategorySystem.FetchCategoryDetails();
                    if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs != null)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpCategories, resultArgs.DataSource.Table, assetCategorySystem.AppSchema.ASSETCategory.NAMEColumn.ColumnName, assetCategorySystem.AppSchema.ASSETCategory.CATEGORY_IDColumn.ColumnName);
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Loading unit symbols in the drop down control. 
        /// </summary>
        private void LoadAssetUnitOfMeasure()
        {
            try
            {
                using (AssetUnitOfMeassureSystem assetUnitOfMeasureSystem=new AssetUnitOfMeassureSystem())
                {
                    resultArgs = assetUnitOfMeasureSystem.FetchMeasureDetails();
                    if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs != null)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpUnit, resultArgs.DataSource.Table, assetUnitOfMeasureSystem.AppSchema.ASSETUnitOfMeassure.SYMBOLColumn.ColumnName, assetUnitOfMeasureSystem.AppSchema.ASSETUnitOfMeassure.UNIT_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Loading default methods from enum to drop down control.
        /// </summary>
        private void LoadMethods()
        {
            TransactionVoucherMethod transactionVoucherMethod = new TransactionVoucherMethod();
            DataView AssetMthodType = this.UtilityMember.EnumSet.GetEnumDataSource(transactionVoucherMethod, Sorting.Ascending);
            this.UtilityMember.ComboSet.BindGridLookUpCombo(lkpMethod, AssetMthodType.ToTable(), "Name", "Id");
            lkpMethod.EditValue = lkpMethod.Properties.GetKeyValue(1);
        }
        /// <summary>
        /// Loading default Item Kind from enum to drop down control.
        /// </summary>
        private void LoadItemKinds()
        {
            ItemKind itemKind = new ItemKind();
            DataView AssetItemKind = this.UtilityMember.EnumSet.GetEnumDataSource(itemKind, Sorting.Ascending);
            this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpItemKind, AssetItemKind.ToTable(), "Name", "Id");
            glkpItemKind.EditValue = glkpItemKind.Properties.GetKeyValue(1);
        }
        /// <summary>
        /// This will clear the controls after saving.
        /// </summary>
        private void ClearControls()
        {
            LoadAssetCategory();
            LoadAssetGrouptoCombo();
            LoadAssetUnitOfMeasure();
            LoadDefaultLedgers();
            LoadMethods();
            LoadItemKinds();
            txtName.Text = txtPrefix.Text = txtQuantity.Text = txtSuffix.Text = txtTotal.Text = string.Empty;
        }
       /// <summary>
       /// Doing validation before saving.
       /// </summary>
       /// <returns></returns>
        private bool ValidateIteamDetails()
        {
            bool isItemTrue = true;
            if (string.IsNullOrEmpty(glpAssetGroup.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_ASSETGROUP_EMPTY));
                this.SetBorderColor(glpAssetGroup);
                isItemTrue = false;
                this.glpAssetGroup.Focus();
            }
            else if (string.IsNullOrEmpty(glkpDepreciationLedger.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_DEPRECIATIONLEDGER_EMPTY));
                this.SetBorderColor(glkpDepreciationLedger);
                isItemTrue = false;
                this.glkpDepreciationLedger.Focus();

            }
            else if (string.IsNullOrEmpty(glkpDisposalLedger.Text))
            {
                this.SetBorderColor(glkpDisposalLedger);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_DISPOSALLEDGER_EMPTY));
                isItemTrue = false;
                this.glkpDisposalLedger.Focus();

            }
            else if (string.IsNullOrEmpty(glkpAssetAccountLedger.Text))
            {
                this.SetBorderColor(glkpAssetAccountLedger);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_ACCOUNTLEDGER_EMPTY));
                isItemTrue = false;
                this.glkpAssetAccountLedger.Focus();
            }
            else if (string.IsNullOrEmpty(glkpCategories.Text))
            {
                this.SetBorderColor(glkpCategories);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_CATOGERY_EMPTY));
                isItemTrue = false;
                this.glkpCategories.Focus();
            }
            else if (string.IsNullOrEmpty(txtName.Text))
            {
                this.SetBorderColor(txtName);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_NAME_EMPTY));
                isItemTrue = false;
                this.txtName.Focus();
            }            
            else if (string.IsNullOrEmpty(glkpUnit.Text))
            {
                this.SetBorderColor(glkpUnit);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_UNIT_EMPTY));
                isItemTrue = false;
                this.glkpUnit.Focus();
            }
            else if (string.IsNullOrEmpty(lkpMethod.Text))
            {
                this.SetBorderColor(lkpMethod);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_METHOD_EMPTY));
                isItemTrue = false;
                this.lkpMethod.Focus();
            }
            else if (string.IsNullOrEmpty(txtStartingNo.Text))
            {
                this.SetBorderColor(txtStartingNo);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_STARTINNO_EMPTY));
                isItemTrue = false;
                this.txtStartingNo.Focus();
            }
            else if (string.IsNullOrEmpty(txtQuantity.Text))
            {
                this.SetBorderColor(txtQuantity);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_QUANTITY_EMPTY));
                isItemTrue = false;
                this.txtQuantity.Focus();
            }
            else if (string.IsNullOrEmpty(txtRatePerItem.Text))
            {
                this.SetBorderColor(txtRatePerItem);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_RATEPERITEM_EMPTY));
                isItemTrue = false;
                this.txtRatePerItem.Focus();
            }
            return isItemTrue;
        }
        /// <summary>
        /// This is to set the title.
        /// </summary>
        private void SetTittle()
        {
            this.Text = AsstItemId == 0 ? this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_EDIT_CAPTION);
        }
        /// <summary>
        /// Assign values to controls while edit mode.
        /// </summary>
        public void AssignValuesToControls()
        {
            if (this.AsstItemId > 0)
            {
                using (AssetItemSystem  assetItemSystem=new AssetItemSystem(AsstItemId))
                {
                    glpAssetGroup.EditValue = assetItemSystem.AssetGroupId;
                    glkpAssetAccountLedger.EditValue = assetItemSystem.AccountLeger;
                    glkpCategories.EditValue = assetItemSystem.Catogery;
                    glkpDepreciationLedger.EditValue = assetItemSystem.DepreciationLedger;
                    glkpDisposalLedger.EditValue = assetItemSystem.DisposalLedger;
                    glkpItemKind.EditValue = assetItemSystem.ItemKind.Equals(ItemKind.New.ToString()) ? (int)ItemKind.New : (int)ItemKind.Used;
                    glkpUnit.EditValue = assetItemSystem.Unit;
                    lkpMethod.EditValue = assetItemSystem.Method.Equals(TransactionVoucherMethod.Manual.ToString()) ? (int)TransactionVoucherMethod.Manual : (int)TransactionVoucherMethod.Automatic;
                    txtName.Text = assetItemSystem.Name;
                    txtPrefix.Text = assetItemSystem.Prefix;
                    txtSuffix.Text = assetItemSystem.Suffix;
                    txtStartingNo.Text = assetItemSystem.StartingNo.ToString();
                    txtTotal.Text = assetItemSystem.Total.ToString();
                    txtRatePerItem.Text = assetItemSystem.RatePerItem.ToString();
                    txtQuantity.Text = assetItemSystem.Quantity.ToString();
                    
                }
            }
        }

        #endregion
        
    }
}
