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
using Bosco.Model;
using Bosco.Model.Inventory;
using Bosco.Utility.CommonMemberSet;
using Bosco.Model.Setting;
using AcMEDSync.Model;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmAssetItemAdd : frmFinanceBaseAdd
    {
        #region Declaration
        CommonMember commem = new CommonMember();
        FormMode frmMode = FormMode.Add;
        #endregion

        #region Properties

        public event EventHandler UpdateHeld;
        private ResultArgs resultArgs = null;
        private int AssetItemId = 0;
        private int PrevAssetLedgerId = 0;

        #endregion

        #region Constructor
        public frmAssetItemAdd()
        {
            InitializeComponent();
        }
        public frmAssetItemAdd(int ItemId)
            : this()
        {
            AssetItemId = ItemId;
        }
        #endregion

        #region Events

        private void glpAssetGroup_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                // if (this.AppSetting.LockMasters == (int)YesNo.No)
                //{
                frmAssetClassAdd frmAssetClassAdd = new frmAssetClassAdd();
                frmAssetClassAdd.ShowDialog();
                if (frmAssetClassAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                {
                    LoadAssetClasstoCombo();
                    if (frmAssetClassAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmAssetClassAdd.ReturnValue.ToString()) > 0)
                    {
                        glpAssetGroup.EditValue = this.UtilityMember.NumberSet.ToInteger(frmAssetClassAdd.ReturnValue.ToString());
                    }
                }
                //}
                //else
                //{
                //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
                //}
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAssetItemCreation_Load(object sender, EventArgs e)
        {
            SetTittle();
            LoadAssetClasstoCombo();
            // LoadAssetCategory();
            LoadAssetLedgers();
            BindLookupEditControls();
            LoadExpenseLedgers();
            LoadAssetUnitOfMeasure();
            // LoadCustodian();
            AssignValuesToControls();
            IsLedgerCreationAllowed();
            HideLedgerDetails();

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
                        assetItemSystem.ItemId = AssetItemId == 0 ? this.UtilityMember.NumberSet.ToInteger(AddNewRow.NewRow.ToString()) : AssetItemId;
                        assetItemSystem.AssetClassId = this.UtilityMember.NumberSet.ToInteger(glpAssetGroup.EditValue.ToString());
                        assetItemSystem.DepreciationLedger = this.UtilityMember.NumberSet.ToInteger(glkpDepreciationLedger.EditValue.ToString());
                        assetItemSystem.AccountLeger = glkpAssetAccountLedger.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpAssetAccountLedger.EditValue.ToString()) : 0;
                        assetItemSystem.DisposalLedger = 1;// this.UtilityMember.NumberSet.ToInteger(glkpDisposalLedger.EditValue.ToString());
                        assetItemSystem.AssetItemMode = lkpAssetMethod.EditValue != null ? UtilityMember.NumberSet.ToInteger(lkpAssetMethod.EditValue.ToString()) : 1; //By default AutoMode
                        //    assetItemSystem.Catogery = 0;// this.UtilityMember.NumberSet.ToInteger(glkpCategories.EditValue.ToString());
                        assetItemSystem.Name = txtName.Text.Trim();
                        assetItemSystem.Unit = this.UtilityMember.NumberSet.ToInteger(glkpUnit.EditValue.ToString());
                        assetItemSystem.Custodian = 0;// glkpCustodian.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpCustodian.EditValue.ToString()) : 0;
                        assetItemSystem.Prefix = txtPrefix.Text.Trim();
                        assetItemSystem.Suffix = txtSuffix.Text.Trim();
                        assetItemSystem.StartingNo = this.UtilityMember.NumberSet.ToInteger(txtStartingNo.Text.Trim());
                        assetItemSystem.Width = this.UtilityMember.NumberSet.ToInteger(txtWidth.Text.Trim());
                        assetItemSystem.InsuranceApplicable = chkInsurance.Checked == true ? 1 : 0;
                        assetItemSystem.AMCApplicable = chkAnnualMaintanance.Checked == true ? 1 : 0;
                        assetItemSystem.DepreciatonApplicable = chkAssetDepreciation.Checked == true ? 1 : 0;
                        assetItemSystem.DepreciationYrs = this.UtilityMember.NumberSet.ToInteger(txtDepreciationYears.Text.ToString());
                        assetItemSystem.RetentionYrs = this.UtilityMember.NumberSet.ToInteger(txtRetentionYears.Text.ToString());
                        assetItemSystem.DepreciationNo = txtDepreciationNo.Text.Trim();

                        resultArgs = UpdateAccountLedgerOpBalance();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            resultArgs = assetItemSystem.SaveItemDetails();
                            if (resultArgs != null && resultArgs.Success)
                            {
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                                ClearControls();
                                if (UpdateHeld != null)
                                {
                                    UpdateHeld(this, e);
                                }
                                glpAssetGroup.Focus();
                            }
                            else
                            {
                                glpAssetGroup.Focus();
                            }
                        }
                        else
                        {
                            this.ShowMessageBox(resultArgs.Message);
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

        private ResultArgs UpdateAccountLedgerOpBalance()
        {
            int ProjectId = 0;
            double Amount = 0;
            double LedOPAmount = 0;
            double LedTempOPAmount = 0;

            if (PrevAssetLedgerId > 0 && PrevAssetLedgerId.ToString() != glkpAssetAccountLedger.EditValue.ToString())
            {
                using (AssetInwardOutwardSystem inwardoutward = new AssetInwardOutwardSystem())
                {
                    using (BalanceSystem balancesystem = new BalanceSystem())
                    {
                        resultArgs = inwardoutward.FetchTransactionDetailsByItemId(AssetItemId);
                        DataTable dtItems = resultArgs.DataSource.Table;
                        if (dtItems != null && dtItems.Rows.Count > 0)
                        {
                            foreach (DataRow drItems in dtItems.Rows)
                            {
                                ProjectId = this.UtilityMember.NumberSet.ToInteger(drItems["PROJECT_ID"].ToString());
                                Amount = this.UtilityMember.NumberSet.ToDouble(drItems["AMOUNT"].ToString());

                                LedOPAmount = GetOpeningBalance(this.AppSetting.BookBeginFrom, ProjectId, PrevAssetLedgerId);
                                LedTempOPAmount = LedOPAmount > 0 ? (LedOPAmount - Amount) > 0 ? LedOPAmount - Amount : 0 : 0;
                                resultArgs = UpdateOpBalance(ProjectId, PrevAssetLedgerId.ToString(), (LedOPAmount - Amount) > 0 ? LedOPAmount - Amount : 0);

                                if (!resultArgs.Success)
                                    break;
                            }
                        }

                    }
                }
            }
            return resultArgs;
        }

        public double GetOpeningBalance(string OpDate, int ProjectId, int LedgerId)
        {
            double Amount = 0;
            using (BalanceSystem balanceSystem = new BalanceSystem())
            {
                BalanceProperty balProperty = balanceSystem.GetBalance(ProjectId, LedgerId, OpDate, BalanceSystem.BalanceType.OpeningBalance);
                Amount = balProperty.Amount;
            }
            return Amount;
        }

        private ResultArgs UpdateOpBalance(int ProjectId, string LedId, double Amount)
        {
            if (LedId != "0")
            {
                using (MappingSystem mapsystem = new MappingSystem())
                {
                    mapsystem.ProjectId = ProjectId;
                    mapsystem.LedgerId = UtilityMember.NumberSet.ToInteger(LedId);
                    resultArgs = mapsystem.MapProjectLedger();

                    if (resultArgs != null && resultArgs.Success)
                    {
                        using (BalanceSystem balanceSystem = new BalanceSystem())
                        {
                            if (balanceSystem.HasBalance(ProjectId, UtilityMember.NumberSet.ToInteger(LedId)))
                            {
                                resultArgs = balanceSystem.UpdateOpBalance(this.AppSetting.BookBeginFrom, ProjectId,
                                       UtilityMember.NumberSet.ToInteger(LedId), Amount, TransactionMode.DR.ToString(), TransactionAction.EditBeforeSave);
                            }
                        }
                    }
                }

            }
            return resultArgs;
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

        //private void cmbCategories_Leave(object sender, EventArgs e)
        //{
        //    this.SetBorderColor(glkpCategories);
        //}

        private void txtName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtName);
            txtName.Text = this.commem.StringSet.ToSentenceCase(txtName.Text);
        }

        private void cmbUnit_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpUnit);
        }

        private void txtStartingNo_Leave(object sender, EventArgs e)
        {
            if (this.UtilityMember.NumberSet.ToInteger(lkpAssetMethod.EditValue.ToString()).Equals((int)AssetItemManualType.Automatic))
            {
                this.SetBorderColor(txtStartingNo);
            }
        }

        private void glkpDisposalLedger_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpDisposalLedger);
        }

        private void glkpAssetAccountLedger_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpAssetAccountLedger);
        }

        private void glkpUnit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                //if (this.AppSetting.LockMasters == (int)YesNo.No)
                //{
                frmUnitofMeasureAdd frmUnitofMeasureAdd = new frmUnitofMeasureAdd();
                frmUnitofMeasureAdd.ShowDialog();
                if (frmUnitofMeasureAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                {
                    LoadAssetUnitOfMeasure();
                    if (frmUnitofMeasureAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmUnitofMeasureAdd.ReturnValue.ToString()) > 0)
                    {
                        glkpUnit.EditValue = this.UtilityMember.NumberSet.ToInteger(frmUnitofMeasureAdd.ReturnValue.ToString());
                    }
                }
                //}
                //else
                //{
                //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
                //}
            }
        }
        #endregion Events

        #region Methods

        public void LoadUnitofMeasure()
        {
            // if (this.AppSetting.LockMasters == (int)YesNo.No)
            //{
            frmUnitofMeasureAdd frmUnitofMeasureAdd = new frmUnitofMeasureAdd();
            frmUnitofMeasureAdd.ShowDialog();
            if (frmUnitofMeasureAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
            {
                LoadAssetUnitOfMeasure();
                if (frmUnitofMeasureAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmUnitofMeasureAdd.ReturnValue.ToString()) > 0)
                {
                    glkpUnit.EditValue = this.UtilityMember.NumberSet.ToInteger(frmUnitofMeasureAdd.ReturnValue.ToString());
                }
            }
            //}
            //else
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
            //}
        }

        private void HideLedgerDetails()
        {
            Size = new System.Drawing.Size(490, 300);
            //this.btnOpenningStock.Enabled = false;
            //  this.btnOpenningStock.Visible = false;
            if (this.AppSetting.IS_SDB_INM)
            {
                lblDepreciationLedger.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                simpleLabelItem1.Visibility = lciDepreciation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                lblDepreciationLedger.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                simpleLabelItem1.Visibility = lciDepreciation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }

            lblDepreciationYears.Visibility = layoutControlItem9.Visibility = lblRetentionYear.Visibility = layoutControlItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

        }

        private void LoadAssetLedgers()
        {
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                // Assign only the Fixed Asset Ledger only not all the Fixed Ledger as Nature as 3

                ledgerSystem.GroupId = (int)TDSLedgerGroup.FixedAsset; //(int)Natures.Assert;
                resultArgs = ledgerSystem.FetchLedgerByLedgerGroup();
                DataView dvAccountLeger = new DataView(resultArgs.DataSource.Table);
                if (resultArgs != null && resultArgs.Success)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpAssetAccountLedger, resultArgs.DataSource.Table,
                        ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                }
            }
        }

        private void LoadExpenseLedgers()
        {
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                if (this.AppSetting.IS_SDB_INM)
                {
                    // ledgerSystem.GroupId = (int)Natures.Expenses;
                }
                else
                {
                    //int Asset = (int)Natures.Assert;
                    //int Liability = (int)Natures.Libilities;

                    //string GroupIds = string.Format("{0},{1}", Asset, Liability);

                    // ledgerSystem.GroupIds = GroupIds;
                }

                resultArgs = ledgerSystem.FetchLedgerByNatures();

                if (resultArgs != null && resultArgs.Success)
                {
                    DataView dvAll = resultArgs.DataSource.Table.DefaultView;
                    DataTable dtSource = new DataTable();

                    if (this.AppSetting.IS_SDB_INM)
                    {
                        dvAll.RowFilter = "NATURE_ID IN (2)";
                        dvAll.Sort = "NATURE_ID";
                        dtSource = dvAll.ToTable();
                        dvAll.RowFilter = string.Empty;
                    }
                    else
                    {
                        dvAll.RowFilter = "NATURE_ID IN (2,3)";
                        dvAll.Sort = "NATURE_ID";
                        dtSource = dvAll.ToTable();
                        dvAll.RowFilter = string.Empty;
                    }

                    //if (resultArgs != null && resultArgs.Success)
                    //{
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpDepreciationLedger, dtSource,
                        ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                    //  this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpDisposalLedger, resultArgs.DataSource.Table,
                    //  ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                    //}
                }
            }
        }

        /// <summary>
        /// Loading Asset Group in the drop down control.
        /// </summary>
        private void LoadAssetClasstoCombo()
        {
            try
            {
                using (AssetClassSystem assetClassSystem = new AssetClassSystem())
                {
                    resultArgs = assetClassSystem.FetchClassDetails();
                    if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs != null)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glpAssetGroup, resultArgs.DataSource.Table, assetClassSystem.AppSchema.ASSETClassDetails.ASSET_CLASSColumn.ColumnName, assetClassSystem.AppSchema.ASSETClassDetails.ASSET_CLASS_IDColumn.ColumnName);
                    }
                }

            }
            catch (Exception ex)
            {
                this.ShowMessageBoxError(ex.Message + Environment.NewLine + ex.Message);
            }
        }

        /// <summary>
        /// Loading unit symbols in the drop down control. 
        /// </summary>
        private void LoadAssetUnitOfMeasure()
        {
            try
            {
                using (AssetUnitOfMeassureSystem assetUnitOfMeasureSystem = new AssetUnitOfMeassureSystem())
                {
                    resultArgs = assetUnitOfMeasureSystem.FetchMeasureDetails();
                    if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs != null)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpUnit, resultArgs.DataSource.Table, assetUnitOfMeasureSystem.AppSchema.ASSETUnitOfMeassure.SYMBOLColumn.ColumnName, assetUnitOfMeasureSystem.AppSchema.ASSETUnitOfMeassure.UOM_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBoxError(ex.Message + Environment.NewLine + ex.Message);
            }
        }

        /// <summary>
        /// This will clear the controls after saving.
        /// </summary>
        private void ClearControls()
        {
            if (AssetItemId == 0)
            {
                glpAssetGroup.EditValue = 0;
                glkpUnit.EditValue = 0;
                txtName.Text = txtPrefix.Text = txtSuffix.Text = txtStartingNo.Text = txtWidth.Text = txtDepreciationNo.Text = string.Empty;
                AssignValuesToControls();
                txtDepreciationYears.Text = txtRetentionYears.Text = string.Empty;
                chkAnnualMaintanance.Checked = chkInsurance.Checked = false;
                chkAssetDepreciation.Checked = false;
            }
            else
            {
                //this.Close();
            }
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
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_ASSETCLASS_EMPTY));
                this.SetBorderColor(glpAssetGroup);
                isItemTrue = false;
                this.glpAssetGroup.Focus();
            }
            else if (string.IsNullOrEmpty(txtName.Text))
            {
                this.SetBorderColor(txtName);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_NAME_EMPTY));
                isItemTrue = false;
                this.txtName.Focus();
            }
            else if (string.IsNullOrEmpty(lkpAssetMethod.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Voucher.VOUCHER_METHOD_EMPTY));
                this.SetBorderColorForLookUpEdit(lkpAssetMethod);
                lkpAssetMethod.Focus();
                isItemTrue = false;
            }
            else if (string.IsNullOrEmpty(glkpUnit.Text))
            {
                this.SetBorderColor(glkpUnit);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_UNIT_EMPTY));
                isItemTrue = false;
                this.glkpUnit.Focus();
            }

            //commond by Sudhakar for validection
            //else if (string.IsNullOrEmpty(txtRetentionYears.Text))
            //{
            //    this.SetBorderColor(txtRetentionYears);
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.RETENTION_YRS_EMPTY));
            //    isItemTrue = false;
            //    this.txtRetentionYears.Focus();
            //}
            //else if (string.IsNullOrEmpty(txtDepreciationYears.Text))
            //{
            //    this.SetBorderColor(txtDepreciationYears);
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.DEPRECIATION_YRS_EMPTY));
            //    isItemTrue = false;
            //    this.txtDepreciationYears.Focus();
            //}

            else if (glpAssetGroup.SelectedText != "Land")
            {
                // chinna 29.01.2021 at 1 PM for manuel generation of NUmber
                if (string.IsNullOrEmpty(txtPrefix.Text) && this.UtilityMember.NumberSet.ToInteger(lkpAssetMethod.EditValue.ToString()).Equals((int)AssetItemManualType.Automatic))
                {
                    this.SetBorderColor(txtPrefix);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSET_PREFIX_EMPTY));
                    isItemTrue = false;
                    this.txtPrefix.Focus();
                }

                //Chinna
                //else if (string.IsNullOrEmpty(txtRetentionYears.Text))
                //{
                //    this.SetBorderColor(txtRetentionYears);
                //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.RETENTION_YRS_EMPTY));
                //    isItemTrue = false;
                //    this.txtRetentionYears.Focus();
                //}
                //else if (string.IsNullOrEmpty(txtDepreciationYears.Text))
                //{
                //    this.SetBorderColor(txtDepreciationYears);
                //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.DEPRECIATION_YRS_EMPTY));
                //    isItemTrue = false;
                //    this.txtDepreciationYears.Focus();
                //}

                else if (string.IsNullOrEmpty(glkpAssetAccountLedger.Text))
                {
                    this.SetBorderColor(glkpAssetAccountLedger);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_ACCOUNTLEDGER_EMPTY));
                    isItemTrue = false;
                    this.glkpAssetAccountLedger.Focus();
                }
                else if (!this.UtilityMember.NumberSet.ToInteger(lkpAssetMethod.EditValue.ToString()).Equals((int)AssetItemManualType.Manual))
                {
                    if (string.IsNullOrEmpty(txtStartingNo.Text) || txtStartingNo.Text == "0")
                    {
                        this.SetBorderColor(txtStartingNo);
                        this.ShowMessageBox("Starting Number is empty OR Starting Number should not be Zero");
                        isItemTrue = false;
                        this.txtStartingNo.Focus();
                    }
                }

            }
            //else if (!Valide()) // Chinna
            //{
            //    isItemTrue = false;
            //    this.txtDepreciationYears.Focus();
            //}
            else if (this.UtilityMember.NumberSet.ToInteger(lkpAssetMethod.EditValue.ToString()).Equals((int)AssetItemManualType.Automatic))
            {
                if (string.IsNullOrEmpty(txtStartingNo.Text) || txtStartingNo.Text == "0")
                {
                    this.SetBorderColor(txtStartingNo);
                    this.ShowMessageBox("Starting Number is empty OR Starting Number should not be Zero"); //MessageCatalog.Asset.AssetItem.ASSETITEM_STARTINNO_EMPTY
                    isItemTrue = false;
                    this.txtStartingNo.Focus();
                }
                else if (AssetItemId == 0 && !ValidatePrefixandSuffix())
                {
                    isItemTrue = false;
                    txtPrefix.Text = string.Empty;
                    txtSuffix.Text = string.Empty;
                }
                else if (string.IsNullOrEmpty(txtPrefix.Text))
                {
                    this.SetBorderColor(txtPrefix);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSET_PREFIX_EMPTY));
                    isItemTrue = false;
                    this.txtPrefix.Focus();
                }
                else if (string.IsNullOrEmpty(glkpAssetAccountLedger.Text))
                {
                    this.SetBorderColor(glkpAssetAccountLedger);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_ACCOUNTLEDGER_EMPTY));
                    isItemTrue = false;
                    this.glkpAssetAccountLedger.Focus();
                }
            }

            //else if (string.IsNullOrEmpty(glkpDepreciationLedger.Text))
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_DEPRECIATIONLEDGER_EMPTY));
            //    this.SetBorderColor(glkpDepreciationLedger);
            //    isItemTrue = false;
            //    this.glkpDepreciationLedger.Focus();
            //}
            //else if (string.IsNullOrEmpty(glkpDisposalLedger.Text))
            //{
            //    this.SetBorderColor(glkpDisposalLedger);
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_DISPOSALLEDGER_EMPTY));
            //    isItemTrue = false;
            //    this.glkpDisposalLedger.Focus();
            //}
            //else if (string.IsNullOrEmpty(txtSuffix.Text))
            //{
            //    this.SetBorderColor(txtSuffix);
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSET_SUFFIX_EMPTY));
            //    isItemTrue = false;
            //    this.txtSuffix.Focus();
            //}

            return isItemTrue;
        }

        private bool ValidatePrefixandSuffix()
        {
            bool isValid = true;
            using (AssetItemSystem assetItem = new AssetItemSystem())
            {
                resultArgs = assetItem.FetchAssetItemDetails();
                if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    DataView dvAsset = resultArgs.DataSource.Table.DefaultView;
                    if (dvAsset != null && dvAsset.Count > 0)
                    {
                        dvAsset.RowFilter = "PREFIXSUFFIX='" + txtPrefix.Text.Trim() + txtSuffix.Text.Trim() + "'";
                        if (dvAsset.Count > 0)
                        {
                            isValid = false;
                            //this.ShowMessageBoxWarning("Prefix and Suffix is set for the other item already.");
                            this.ShowMessageBoxWarning(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSET_PREFIX_SUFFIX_INFO));
                        }
                    }
                }
            }

            return isValid;
        }

        /// <summary>
        /// This is to set the title.
        /// </summary>
        private void SetTittle()
        {
            this.Text = AssetItemId == 0 ? this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_EDIT_CAPTION);
            if (AssetItemId == 0)
                txtDepreciationNo.Text = "10";
        }

        /// <summary>
        /// Assign values to controls while edit mode.
        /// </summary>
        public void AssignValuesToControls()
        {
            if (this.AssetItemId > 0)
            {
                using (AssetItemSystem assetItemSystem = new AssetItemSystem(AssetItemId))
                {
                    glpAssetGroup.EditValue = assetItemSystem.AssetClassId;
                    glkpAssetAccountLedger.EditValue = PrevAssetLedgerId = assetItemSystem.AccountLeger;
                    lkpAssetMethod.EditValue = assetItemSystem.AssetItemMode == 1 ? lkpAssetMethod.Properties.GetDataSourceValue(lkpAssetMethod.Properties.ValueMember, 0)
                       : lkpAssetMethod.Properties.GetDataSourceValue(lkpAssetMethod.Properties.ValueMember, 1);
                    // glkpCategories.EditValue = assetItemSystem.Catogery;
                    glkpDepreciationLedger.EditValue = assetItemSystem.DepreciationLedger;
                    glkpDisposalLedger.EditValue = assetItemSystem.DisposalLedger;
                    glkpUnit.EditValue = assetItemSystem.Unit;
                    // glkpCustodian.EditValue = assetItemSystem.Custodian;
                    txtName.Text = assetItemSystem.Name;
                    txtPrefix.Text = assetItemSystem.Prefix;
                    txtSuffix.Text = assetItemSystem.Suffix;
                    txtStartingNo.Text = assetItemSystem.StartingNo.ToString();
                    txtWidth.Text = assetItemSystem.Width.ToString();
                    txtRetentionYears.Text = assetItemSystem.RetentionYrs.ToString();
                    txtDepreciationYears.Text = assetItemSystem.DepreciationYrs.ToString();
                    chkAnnualMaintanance.Checked = assetItemSystem.AMCApplicable == 1 ? true : false;
                    chkInsurance.Checked = assetItemSystem.InsuranceApplicable == 1 ? true : false;
                    chkAssetDepreciation.Checked = assetItemSystem.DepreciatonApplicable == 1 ? true : false;
                    txtDepreciationNo.Text = assetItemSystem.DepreciationNo;
                }
            }
            else
            {
                using (AssetItemSystem assetItem = new AssetItemSystem())
                {
                    glkpAssetAccountLedger.EditValue = assetItem.AccountLedgerId;
                    glkpDepreciationLedger.EditValue = assetItem.DepreciationLedger;
                    glkpDisposalLedger.EditValue = assetItem.DisposalLedger;
                }
            }
        }

        private void BindLookupEditControls()
        {
            try
            {
                TransactionVoucherMethod AssetMethod = new TransactionVoucherMethod();
                EnumSetMember eumSetMembers = new EnumSetMember();
                //To convert the enum type to DataSouce and binds it to the LookUpEdit
                this.UtilityMember.ComboSet.BindLookUpEditCombo(lkpAssetMethod, eumSetMembers.GetEnumDataSource(AssetMethod, Sorting.None).ToTable(), EnumColumns.Name.ToString(), EnumColumns.Id.ToString());
                lkpAssetMethod.EditValue = lkpAssetMethod.Properties.GetDataSourceValue(lkpAssetMethod.Properties.ValueMember, 0);
                // BindEnumValue(lkpAssetMethod, AssetMethod, EnumColumns.Name.ToString(), EnumColumns.Id.ToString(), 0);
            }
            catch (Exception e)
            {
                MessageRender.ShowMessage(e.Message, true);
            }
            finally { }
        }

        #endregion

        private void glkpCustodian_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                frmCustodiansAdd custodianAdd = new frmCustodiansAdd();
                custodianAdd.ShowDialog();
                //LoadCustodian();
            }
        }

        private void IsLedgerCreationAllowed()
        {
            //if (this.AppSetting.LockMasters == (int)YesNo.Yes)
            //{
            glkpAssetAccountLedger.Properties.Buttons[1].Visible = false;
            glkpDepreciationLedger.Properties.Buttons[1].Visible = false;
            glkpDisposalLedger.Properties.Buttons[1].Visible = false;
            // }
        }

        private void txtPrefix_Leave(object sender, EventArgs e)
        {
            if (this.UtilityMember.NumberSet.ToInteger(lkpAssetMethod.EditValue.ToString()).Equals((int)AssetItemManualType.Automatic))
            {
                this.SetBorderColor(txtPrefix);
                txtPrefix.Text = this.UtilityMember.StringSet.ToSentenceCase(txtPrefix.Text.ToUpper());
            }
        }

        private void txtSuffix_Leave(object sender, EventArgs e)
        {
            if (this.UtilityMember.NumberSet.ToInteger(lkpAssetMethod.EditValue.ToString()).Equals((int)AssetItemManualType.Automatic))
            {
                this.SetBorderColor(txtSuffix);
                txtSuffix.Text = this.UtilityMember.StringSet.ToSentenceCase(txtSuffix.Text.ToUpper());
            }
        }

        private void glkpAssetAccountLedger_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                //if (this.AppSetting.LockMasters == (int)YesNo.No)
                //  {
                ACPP.Modules.Master.frmLedgerDetailAdd frmLedgerDetailAdd = new ACPP.Modules.Master.frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.GN);
                frmLedgerDetailAdd.ShowDialog();
                if (frmLedgerDetailAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                {
                    LoadAssetLedgers();
                    //LoadExpenseLedgers();
                    if (frmLedgerDetailAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmLedgerDetailAdd.ReturnValue.ToString()) > 0)
                    {
                        glkpAssetAccountLedger.EditValue = this.UtilityMember.NumberSet.ToInteger(frmLedgerDetailAdd.ReturnValue.ToString());
                    }
                }
                // }
                //else
                //{
                //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
                //}
            }
        }

        private void glkpDepreciationLedger_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                ACPP.Modules.Master.frmLedgerDetailAdd frmBank = new ACPP.Modules.Master.frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.GN);
                frmBank.ShowDialog();
                LoadExpenseLedgers();
            }
        }

        private void glkpDisposalLedger_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                ACPP.Modules.Master.frmLedgerDetailAdd frmBank = new ACPP.Modules.Master.frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.GN);
                frmBank.ShowDialog();
                LoadExpenseLedgers();
            }
        }

        private void glkpUnit_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpUnit);
        }

        private void txtRetentionYears_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtRetentionYears);
        }

        private void txtDepreciationYears_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtDepreciationYears);
        }

        private void btnOpenningStock_Click(object sender, EventArgs e)
        {

        }

        private bool Valide()
        {
            bool istrue = true;
            int a = 0;
            int b = 0;
            a = this.UtilityMember.NumberSet.ToInteger(txtRetentionYears.Text.ToString());
            b = this.UtilityMember.NumberSet.ToInteger(txtDepreciationYears.Text.ToString());
            if (b > a)
            {
                //ShowMessageBox("Depreciation year should less or equal to Retention year");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSET_DEP_YEAR_LESSTHAN_RETN_YEAR_INFO));
                istrue = false;
            }
            return istrue;
        }

        private void lkpAssetMethod_EditValueChanged(object sender, EventArgs e)
        {
            if (this.UtilityMember.NumberSet.ToInteger(lkpAssetMethod.EditValue.ToString()).Equals((int)AssetItemManualType.Automatic))
            {
                txtPrefix.Enabled = txtSuffix.Enabled = txtStartingNo.Enabled = true;
            }
            else
            {
                txtPrefix.Enabled = txtSuffix.Enabled = txtStartingNo.Enabled = true;

                txtPrefix.Text = txtSuffix.Text = txtStartingNo.Text = string.Empty;
                txtPrefix.Enabled = txtSuffix.Enabled = txtStartingNo.Enabled = false;
                txtPrefix.Properties.Appearance.BorderColor = txtSuffix.Properties.Appearance.BorderColor =
                txtStartingNo.Properties.Appearance.BorderColor = Color.Empty;
            }
        }
        //Add by Sudhakar to hide Retention Years and Depreciation Years when select the land  
        private void glpAssetGroup_EditValueChanged(object sender, EventArgs e)
        {
            if (glpAssetGroup.SelectedText == "Land")
            {
                lblDepreciationYears.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lblRetentionYear.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                chkAssetDepreciation.Checked = false;

            }
            else
            {
                // Commanded by chinna
                lblDepreciationYears.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lblRetentionYear.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                chkAssetDepreciation.Checked = true;
            }
        }
    }
}
