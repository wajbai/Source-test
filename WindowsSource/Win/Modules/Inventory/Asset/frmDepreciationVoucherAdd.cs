/*  Class Name      : frmDepreciationVoucherAdd.cs
 *  Purpose         : To calculate the Asset Depreciation
 *  Author          : CD
 *  Created on      : 20-May-2015
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Bosco.Model;
using Bosco.Utility;
using Bosco.Model.UIModel;
using DevExpress.XtraEditors;
using Bosco.Model;
using Bosco.DAO.Schema;
using Bosco.Model.Inventory;
using DevExpress.XtraGrid;
using ACPP.Modules.Transaction;
using Bosco.Model.Transaction;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmDepreciationVoucherAdd : frmFinanceBaseAdd
    {
        #region VariableDeclaration
        ResultArgs resultArgs = new ResultArgs();
        public event EventHandler UpdateHeld;
        string AssetName = string.Empty;
        string GROUP_NAME = "GROUP_NAME";
        string ITEM_ID = "ITEM_ID";
        string ASSET_ID = "ASSET_ID";
        string AMOUNT = "AMOUNT";
        string DEP_PERCENTAGE = "DEP_PERCENTAGE";
        string PURCHASED_ON = "PURCHASED_ON";
        #endregion

        #region Property
        public int DepriciationId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        private DataTable dtActiveAsset { get; set; }

        private VoucherEntryMethod DepreciationGridNewRow
        {
            set
            {
                DataTable dtTransaction = gcDepreciation.DataSource as DataTable;
                dtTransaction.Rows.Add(dtTransaction.NewRow());
                gcDepreciation.DataSource = dtTransaction;
                gvDepreciation.FocusedColumn = colAssetName;
                gvDepreciation.ShowEditor();
            }
        }

        int itemId = 0;
        private int ItemId
        {
            get
            {
                itemId = gvDepreciation.GetFocusedRowCellValue(colAssetName) != null ?
                this.UtilityMember.NumberSet.ToInteger(gvDepreciation.GetRowCellValue(gvDepreciation.FocusedRowHandle, colAssetName).ToString()) : 0;
                return itemId;
            }
            set
            {
                itemId = value;
            }
        }
        string AID = string.Empty;
        private string AssetId
        {
            get
            {
                AID = gvDepreciation.GetFocusedRowCellValue(colAssetId) != null ?
                gvDepreciation.GetFocusedRowCellDisplayText(colAssetId) : string.Empty;
                return AID;
            }
        }
        #endregion

        #region Constructor
        public frmDepreciationVoucherAdd()
        {
            InitializeComponent();
        }

        public frmDepreciationVoucherAdd(int projectId, string Project, int depreciationid)
            : this()
        {
            ProjectId = projectId;
            ProjectName = Project;
            DepriciationId = depreciationid;
        }
        #endregion

        #region Events

        /// <summary>
        /// This is to load the records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDepriciation_Load(object sender, EventArgs e)
        {
            SetTitle();
            ConstructEmptyDataSource();
            LoadCaption();
            LoadAssetGroup();
            LoadAssetLocation();
            LoadPurpose();
            LoadCashBankLedger();
            LoadAssetIdDetails();
            LoadNarrationAutoComplete();
            LoadNameAddressAutoComplete();
            //if (DepriciationId > 0)
            //{
            //    LoadAssetIdDetails();
            //}
            ShowEditDepreciation();
        }

        /// <summary>
        /// This is to Save the Records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateDepreciationDetails())
            {
                try
                {
                    using (DepriciationVoucherSystem VoucherSystem = new DepriciationVoucherSystem())
                    {
                        VoucherSystem.DepriciationId = DepriciationId;
                        VoucherSystem.ProjectId = ProjectId;
                        VoucherSystem.DepreciationDate = this.UtilityMember.DateSet.ToDate(dtVoucherDate.Text, false);
                        VoucherSystem.LocationId = this.UtilityMember.NumberSet.ToInteger(glkLocation.EditValue.ToString());
                        VoucherSystem.AssetGroupId = this.UtilityMember.NumberSet.ToInteger(glkAssetGroup.EditValue.ToString());
                        VoucherSystem.purpose = glkpPurpose.EditValue != null ? ((this.UtilityMember.NumberSet.ToInteger(glkpPurpose.EditValue.ToString())) == (int)PurposeAct.CompaniesAct ? (int)PurposeAct.CompaniesAct : (int)PurposeAct.IncomeTaxAct) : 0;
                        VoucherSystem.ToDate = this.UtilityMember.DateSet.ToDate(dtToDate.Text, false);
                        VoucherSystem.VoucherId = this.UtilityMember.NumberSet.ToInteger(txtVoucherNo.Text.Trim());
                        VoucherSystem.BranchId = 0;
                        DataTable dtSource = gcDepreciation.DataSource as DataTable;
                        DataTable dtFilteredRows = dtSource.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull)).CopyToDataTable();
                        VoucherSystem.dtDepreciationDetails = dtFilteredRows;
                        resultArgs = VoucherSystem.SaveDepreciation();
                        if (resultArgs.Success)
                        {
                            ClearControls();
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowMessageBox(ex.ToString());
                }
                finally { }
            }
        }

        /// <summary>
        /// This is to asset group functionality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkAssetGroup_EditValueChanged(object sender, EventArgs e)
        {
            string DepName = string.Empty;
            GridLookUpEdit gridLKPEdit = sender as GridLookUpEdit;
            if (gridLKPEdit.EditValue != null)
            {
                LoadAssetItemByGroup();
                DataRowView drv = gridLKPEdit.GetSelectedDataRow() as DataRowView;
                if (drv != null)
                {
                    lblDepMethod.Text = drv["DEP_NAME"].ToString();
                }
            }
        }

        /// <summary>
        /// To Validate the Controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkAssetId_Validating(object sender, CancelEventArgs e)
        {
            DateTime dtPurchasedDate = DateTime.Now;
            double Value = 0;
            double DepPercentage = 0;
            int ItemID = 0;
            string AssetId = string.Empty;

            GridLookUpEdit gridLKPEdit = sender as GridLookUpEdit;
            if (gridLKPEdit.EditValue != null)
            {
                DataRowView drv = gridLKPEdit.GetSelectedDataRow() as DataRowView;
                if (drv != null)
                {
                    dtPurchasedDate = this.UtilityMember.DateSet.ToDate(drv[PURCHASED_ON].ToString(), false);
                    DepPercentage = this.UtilityMember.NumberSet.ToDouble(drv[DEP_PERCENTAGE].ToString());
                    Value = this.UtilityMember.NumberSet.ToDouble(drv[AMOUNT].ToString());
                    ItemID = this.UtilityMember.NumberSet.ToInteger(drv[ITEM_ID].ToString());
                    AssetId = drv[ASSET_ID].ToString();

                    gvDepreciation.SetFocusedRowCellValue(colPurchaseOn, dtPurchasedDate);
                    gvDepreciation.SetFocusedRowCellValue(colDepPercentage, DepPercentage);
                    gvDepreciation.SetFocusedRowCellValue(colValue, Value);
                    gvDepreciation.SetFocusedRowCellValue(gcColAssetId, AssetId);

                    gvDepreciation.PostEditor();
                    gvDepreciation.UpdateCurrentRow();
                }
            }
        }

        /// <summary>
        /// This is to validate the functionality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rglkpAssetName_Validating(object sender, CancelEventArgs e)
        {
            DateTime dtPurchasedDate = DateTime.Now;
            string GroupName = "";
            int ItemID = 0;

            GridLookUpEdit gridLKPEdit = sender as GridLookUpEdit;
            if (gridLKPEdit.EditValue != null)
            {
                DataRowView drv = gridLKPEdit.GetSelectedDataRow() as DataRowView;
                if (drv != null)
                {
                    GroupName = drv[GROUP_NAME].ToString();
                    ItemID = this.UtilityMember.NumberSet.ToInteger(drv[ITEM_ID].ToString());

                    gvDepreciation.SetFocusedRowCellValue(colGroup, GroupName);
                    gvDepreciation.SetFocusedRowCellValue(colAssetName, ItemID);

                    gvDepreciation.PostEditor();
                    gvDepreciation.UpdateCurrentRow();
                }
                // LoadAssetByItemIdDetails(ItemID);
            }
        }

        /// <summary>
        /// Focus the Item Date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dtVoucherDate.Focus();
        }

        /// <summary>
        /// Press the key down event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDepreciationVoucherAdd_KeyDown(object sender, KeyEventArgs e)
        {
            ProcessShortcutKeys(e);
        }

        /// <summary>
        /// Show Project
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiProject_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DepriciationId == 0)
            {
                ShowProjectSelectionWindow();
            }
        }


        /// <summary>
        /// Validating Records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcDepreciation_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control)
                {
                    gvDepreciation.PostEditor();
                    gvDepreciation.UpdateCurrentRow();

                    if (gvDepreciation.FocusedColumn == colAssetName)
                    {
                        if (string.IsNullOrEmpty(gvDepreciation.GetFocusedRowCellValue(colAssetName).ToString()))
                        {
                            gvDepreciation.CloseEditor();
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                            glkpCashBankLedger.Focus();
                            glkpCashBankLedger.Select();
                        }
                    }
                    if (gvDepreciation.IsLastRow && (gvDepreciation.FocusedColumn == colValue) &&
                        !string.IsNullOrEmpty(gvDepreciation.GetFocusedRowCellValue(colValue).ToString()))
                    {
                        string AssetGroup = gvDepreciation.GetFocusedRowCellValue(colGroup) != null ? gvDepreciation.GetFocusedRowCellValue(colGroup).ToString() : string.Empty;
                        string AssetName = gvDepreciation.GetFocusedRowCellValue(colAssetName) != null ? gvDepreciation.GetFocusedRowCellValue(colAssetName).ToString() : string.Empty;
                        string Value = gvDepreciation.GetFocusedRowCellValue(colValue) != null ? gvDepreciation.GetFocusedRowCellValue(colValue).ToString() : string.Empty;
                        string PurchaseOn = gvDepreciation.GetFocusedRowCellValue(colPurchaseOn) != null ? gvDepreciation.GetFocusedRowCellValue(colPurchaseOn).ToString() : string.Empty;
                        string depAmt = gvDepreciation.GetFocusedRowCellValue(colDepAmt) != null ? gvDepreciation.GetFocusedRowCellValue(colDepAmt).ToString() : string.Empty;
                        string AssetId = gvDepreciation.GetFocusedRowCellValue(gcColAssetId) != null ? gvDepreciation.GetFocusedRowCellValue(gcColAssetId).ToString() : string.Empty;
                        if (!string.IsNullOrEmpty(AssetGroup) && !string.IsNullOrEmpty(AssetName) && !string.IsNullOrEmpty(AssetId))
                        {
                            gvDepreciation.AddNewRow();
                            gvDepreciation.FocusedColumn = gvDepreciation.Columns[colAssetName.Name];
                            gvDepreciation.ShowEditor();
                        }
                        else
                        {
                            gvDepreciation.CloseEditor();
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                            glkpCashBankLedger.Focus();
                            glkpCashBankLedger.Select();
                        }
                    }
                }
                else if (gvDepreciation.IsFirstRow && gvDepreciation.FocusedColumn == colAssetName && e.Shift && e.KeyCode == Keys.Tab)
                {
                    dtVoucherDate.Select();
                    dtVoucherDate.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        /// <summary>
        /// this is to enter the records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcDepreciation_Enter(object sender, EventArgs e)
        {
            gvDepreciation.OptionsSelection.EnableAppearanceFocusedCell = true;
        }

        /// <summary>
        /// This is to Validate the records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcDepreciation_Leave(object sender, EventArgs e)
        {
            gvDepreciation.OptionsSelection.EnableAppearanceFocusedCell = false;
        }

        /// <summary>
        /// This is to close the records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Constructing the records
        /// </summary>
        private void ConstructEmptyDataSource()
        {
            DataTable dtDeprication = new DataTable();
            dtDeprication.Columns.Add("ITEM_ID", typeof(string));
            dtDeprication.Columns.Add("ASSET_NAME", typeof(string));
            dtDeprication.Columns.Add("GROUP_NAME", typeof(string));
            dtDeprication.Columns.Add("ASSET_ID", typeof(string));
            dtDeprication.Columns.Add("PURCHASED_ON", typeof(DateTime));
            dtDeprication.Columns.Add("VALUE", typeof(decimal));
            dtDeprication.Columns.Add("DEP_PERCENTAGE", typeof(int));
            gcDepreciation.DataSource = dtDeprication;
            gvDepreciation.AddNewRow();
        }

        /// <summary>
        /// This is to clear the controls
        /// </summary>
        private void ClearControls()
        {
            if (DepriciationId == 0)
            {
                dtVoucherDate.DateTime = UtilityMember.DateSet.ToDate(DateTime.Now.ToString(), false);
                dtToDate.DateTime = UtilityMember.DateSet.ToDate(DateTime.Now.ToString(), false);
                txtVoucherNo.Text = string.Empty;
                LoadAssetGroup();
                LoadAssetLocation();
                gcDepreciation.DataSource = null;
                ConstructEmptyDataSource();
            }
            else
            {
                if (this.UIAppSetting.UITransClose == "1")
                {
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Load the assetGroup
        /// </summary>
        private void LoadAssetGroup()
        {
            try
            {
                using (AssetClassSystem assetGroupSystem = new AssetClassSystem())
                {
                    resultArgs = assetGroupSystem.FetchClassDetails();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkAssetGroup, resultArgs.DataSource.Table, assetGroupSystem.AppSchema.ASSETClassDetails.ASSET_CLASSColumn.ColumnName, assetGroupSystem.AppSchema.ASSETClassDetails.ASSET_CLASS_IDColumn.ColumnName);
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpAssetGroup, resultArgs.DataSource.Table, assetGroupSystem.AppSchema.ASSETClassDetails.ASSET_CLASSColumn.ColumnName, assetGroupSystem.AppSchema.ASSETClassDetails.ASSET_CLASS_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        /// <summary>
        /// Load the Cash Bank Ledger
        /// </summary>
        /// <param name="glkpLedger"></param>
        private void LoadCashBankLedger()
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    ledgerSystem.ProjectId = ProjectId;
                    resultArgs = ledgerSystem.FetchCashBankLedger();
                    glkpCashBankLedger.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpCashBankLedger, resultArgs.DataSource.Table, ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }
        /// <summary>
        /// Delete Click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnActionDelete_Click(object sender, EventArgs e)
        {
            DeleteTransaction();
        }

        /// <summary>
        /// Delete the Transaction
        /// </summary>
        private void DeleteTransaction()
        {
            try
            {
                if (gvDepreciation.RowCount > 1)
                {
                    if (gvDepreciation.FocusedRowHandle != GridControl.NewItemRowHandle)
                    {

                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            gvDepreciation.DeleteRow(gvDepreciation.FocusedRowHandle);
                            gvDepreciation.UpdateCurrentRow();
                            gcDepreciation.RefreshDataSource();
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                        }

                    }
                }
                else if (gvDepreciation.RowCount == 1)
                {
                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ConstructEmptyDataSource();
                        gcDepreciation.RefreshDataSource();
                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        /// <summary>
        /// Load asset Item by group 
        /// </summary>
        private void LoadAssetItemByGroup()
        {
            try
            {
                using (AssetItemSystem assetItemSystem = new AssetItemSystem())
                {
                    assetItemSystem.AssetClassId = this.UtilityMember.NumberSet.ToInteger(glkAssetGroup.EditValue.ToString());
                    resultArgs = assetItemSystem.FetchAssetItemByGroup();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        //this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpAssetName, resultArgs.DataSource.Table, assetItemSystem.AppSchema.ASSETItem.ASSET_NAMEColumn.ColumnName,
                            //assetItemSystem.AppSchema.ASSETItem.ITEM_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        /// <summary>
        /// Load the Asset Id Details
        /// </summary>
        private void LoadAssetIdDetails()
        {
            using (AssetItemSystem assetSystem = new Bosco.Model.AssetItemSystem())
            {
                resultArgs = assetSystem.FetchAssetItem();
                dtActiveAsset = resultArgs.DataSource.Table;
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(glkAssetId, resultArgs.DataSource.Table, assetSystem.AppSchema.ASSETItem.ASSET_IDColumn.ColumnName,
                         assetSystem.AppSchema.ASSETItem.ASSET_IDColumn.ColumnName);
                }
            }
        }

        /// <summary>
        /// Load asset Item by group 
        /// </summary>
        private void LoadAssetByItemIdDetails(int id)
        {
            try
            {
                using (AssetItemSystem assetItemSystem = new AssetItemSystem())
                {
                    assetItemSystem.ItemId = id;
                    resultArgs = assetItemSystem.FetchAssetItemByItem();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(glkAssetId, resultArgs.DataSource.Table, assetItemSystem.AppSchema.ASSETItem.ASSET_IDColumn.ColumnName,
                         assetItemSystem.AppSchema.ASSETItem.ASSET_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        /// <summary>
        /// Load the Asset Location
        /// </summary>
        private void LoadAssetLocation()
        {
            try
            {
                using (LocationSystem locationSystem = new LocationSystem())
                {
                    resultArgs = locationSystem.FetchLocationDetails();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkLocation, resultArgs.DataSource.Table, locationSystem.AppSchema.ASSETLocationDetails.LOCATIONColumn.ColumnName, locationSystem.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        /// <summary>
        /// Load the Purpose
        /// </summary>
        private void LoadPurpose()
        {
            PurposeAct PurposeValue = new PurposeAct();
            DataView PurposeView = this.UtilityMember.EnumSet.GetEnumDataSource(PurposeValue, Sorting.Ascending);
            this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpPurpose, PurposeView.ToTable(), "Name", "Id");
        }

        /// <summary>
        /// Set the Title
        /// </summary>
        private void SetTitle()
        {
            this.Text = DepriciationId == 0 ? this.GetMessage(MessageCatalog.Asset.DepreciationVoucher.DEPRECIATION_VOUCHER_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.DepreciationVoucher.DEPRECIATION_VOUCHER_EDIT_CAPTION);
            ucCaptionPanel1.Caption = ProjectName;
        }

        /// <summary>
        /// This is to edit the records
        /// </summary>
        private void ShowEditDepreciation()
        {
            try
            {
                if (DepriciationId > 0)
                {
                    using (DepriciationVoucherSystem Depreciation = new DepriciationVoucherSystem(DepriciationId))
                    {
                        Depreciation.DepriciationId = DepriciationId;
                        dtVoucherDate.DateTime = Depreciation.DepreciationDate;
                        txtVoucherNo.Text = Depreciation.VoucherId.ToString();
                        glkLocation.EditValue = Depreciation.LocationId;
                        glkAssetGroup.EditValue = Depreciation.AssetGroupId;
                        glkpPurpose.EditValue = ((Depreciation.purpose == (int)PurposeAct.CompaniesAct) ? (int)PurposeAct.CompaniesAct : (int)PurposeAct.IncomeTaxAct);
                        dtToDate.DateTime = Depreciation.ToDate;
                        glkpCashBankLedger.EditValue = 1;
                        resultArgs = Depreciation.FetchDepreciationDetails(DepriciationId.ToString());
                        if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            gcDepreciation.DataSource = resultArgs.DataSource.Table;
                            gcDepreciation.RefreshDataSource();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally
            {
            }
        }

        private void LoadNameAddressAutoComplete()
        {
            try
            {
                using (VoucherTransactionSystem vouchermastersystem = new VoucherTransactionSystem())
                {
                    resultArgs = vouchermastersystem.FetchAutoFetchNames();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            collection.Add(dr[vouchermastersystem.AppSchema.VoucherMaster.NAME_ADDRESSColumn.ColumnName].ToString());
                        }
                        txtNameAddress.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        txtNameAddress.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        txtNameAddress.MaskBox.AutoCompleteCustomSource = collection;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        private void LoadNarrationAutoComplete()
        {
            try
            {
                using (VoucherTransactionSystem vouchermastersystem = new VoucherTransactionSystem())
                {
                    resultArgs = vouchermastersystem.AutoFetchNarration();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataView dvNarration = resultArgs.DataSource.Table.AsDataView();
                        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            collection.Add(dr[vouchermastersystem.AppSchema.VoucherMaster.NARRATIONColumn.ColumnName].ToString());
                        }
                        txtNarration.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        txtNarration.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        txtNarration.MaskBox.AutoCompleteCustomSource = collection;
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
        /// Validating the Controls
        /// </summary>
        /// <returns></returns>
        public bool ValidateDepreciationDetails()
        {
            bool isDepreciation = true;
            if (string.IsNullOrEmpty(glkLocation.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.DepreciationVoucher.DEPRECIATON_LOCATION_EMPTY));
                this.SetBorderColor(glkLocation);
                isDepreciation = false;
                this.glkLocation.Focus();
            }
            else if (string.IsNullOrEmpty(glkAssetGroup.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.DepreciationVoucher.DEPRECIATION_GROUP_EMPTY));
                this.SetBorderColor(glkAssetGroup);
                isDepreciation = false;
                this.glkAssetGroup.Focus();
            }
            else if (string.IsNullOrEmpty(glkpCashBankLedger.Text))
            {
                this.ShowMessageBox("Cash / Bank Ledger is empty");
                this.SetBorderColor(glkpCashBankLedger);
                isDepreciation = false;
                this.glkpCashBankLedger.Focus();
            }
            else if (string.IsNullOrEmpty(dtToDate.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.DepreciationVoucher.DEPRECIATION_TODATE_EMPTY));
                this.SetBorderColor(glkAssetGroup);
                isDepreciation = false;
                this.glkAssetGroup.Focus();
            }
            else if (!IsValidDepreciationGrid())
            {
                isDepreciation = false;
            }
            return isDepreciation;
        }

        /// <summary>
        /// This is to set the Caption
        /// </summary>
        private void LoadCaption()
        {
            ucCaptionPanel1.Caption = ProjectName;
        }

        /// <summary>
        /// To validate the Details
        /// </summary>
        /// <returns></returns>
        private bool IsValidDepreciationGrid()
        {
            DataTable dtTrans = gcDepreciation.DataSource as DataTable;

            int Id = 0;
            int RowPosition = 0;
            bool isValid = false;

            string validateMessage = this.GetMessage(MessageCatalog.Asset.DepreciationVoucher.DEPRECIATION_EMPTY);

            DataView dv = new DataView(dtTrans);
            dv.RowFilter = "(ITEM_ID>0)";
            gvDepreciation.FocusedColumn = colItemId;
            gvDepreciation.FocusedColumn = colAssetName;
            if (dv.Count > 0)
            {
                isValid = true;
                foreach (DataRowView drTrans in dv)
                {
                    Id = this.UtilityMember.NumberSet.ToInteger(drTrans[ITEM_ID].ToString());

                    if ((Id == 0))
                    {
                        if (Id == 0)
                        {
                            validateMessage = this.GetMessage(MessageCatalog.Asset.DepreciationVoucher.DEPRECIATION_EMPTY);
                            gvDepreciation.FocusedColumn = colItemId;
                        }
                        isValid = false;
                        break;
                    }
                    RowPosition = RowPosition + 1;
                }
            }
            if (!isValid)
            {
                this.ShowMessageBox(validateMessage);
                gvDepreciation.CloseEditor();
                gvDepreciation.FocusedRowHandle = gvDepreciation.GetRowHandle(RowPosition);
                gvDepreciation.ShowEditor();
            }

            return isValid;
        }

        /// <summary>
        /// Project showing form
        /// </summary>
        private void ShowProjectSelectionWindow()
        {
            frmProjectSelection projectSelection = new frmProjectSelection(Bosco.Utility.ProjectSelection.DisableVoucherSelectionMethod);
            projectSelection.ShowDialog();
            if (projectSelection.DialogResult == DialogResult.OK)
            {
                if (projectSelection.ProjectName != string.Empty)
                {
                    ProjectId = projectSelection.ProjectId;
                    ProjectName = projectSelection.ProjectName;
                    dtVoucherDate.DateTime = this.UtilityMember.DateSet.ToDate(projectSelection.RecentVoucherDate, false);
                    SetTitle();
                }
            }
        }

        /// <summary>
        /// Show Short cut keys
        /// </summary>
        /// <param name="e"></param>
        private void ProcessShortcutKeys(KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F5)
                {
                    if (DepriciationId == 0)
                        ShowProjectSelectionWindow();
                }
                else if (e.KeyCode == Keys.F3)
                {
                    dtVoucherDate.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }

        }

        #endregion
    }
}
