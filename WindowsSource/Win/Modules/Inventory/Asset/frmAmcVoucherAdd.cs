using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model.UIModel;
using Bosco.Model;
using Bosco.DAO.Schema;
using Bosco.Model.Inventory.Asset;
using Bosco.Utility.CommonMemberSet;
using Bosco.Utility.ConfigSetting;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using ACPP.Modules.Transaction;
using Bosco.Model.Transaction;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraGrid.Views.Base;
using AcMEDSync.Model;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using ACPP.Modules.Master;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmAmcVoucherAdd : frmFinanceBaseAdd
    {
        #region Variable Decelaration
        private decimal ItemTotalRate { get; set; }
        private const string AMOUNT = "AMOUNT";
        private const string LEDGER_ID = "LEDGER_ID";
        private int Quantity { get; set; }
        public ResultArgs resultArgs = null;
        public event EventHandler UpdateHeld;
        private int AmcId = 0;
        AppSchemaSet appSchema = new AppSchemaSet();
        private DataTable dtActiveAsset { get; set; }
        private string RecentVoucherDate { get; set; }
        private int RowIndex = 0;
        bool isMouseClicked = false;
        #endregion

        #region Constructor

        public frmAmcVoucherAdd()
        {
            InitializeComponent();
        }

        public frmAmcVoucherAdd(int projectId, string projectName, int Amcid)
            : this()
        {
            ProjectId = projectId;
            Project = projectName;
            AmcId = Amcid;
            RealColumnEditTransAmount();
            RealColumnEditCashTransAmount();
            //RealColumnEditMultiAssetIdSelect();
        }
        #endregion

        #region Property
        public int ProjectId { get; set; }
        public string Project { get; set; }
        public int VoucherId { get; set; }

        //private int Itemid = 0;
        private int ItemID
        {
            get;
            set;
            //get
            //{
            //    Itemid = gvAMcVoucher.GetFocusedRowCellValue(colItemId) != null ? this.UtilityMember.NumberSet.ToInteger(gvAMcVoucher.GetFocusedRowCellValue(colItemId).ToString()) : 0;
            //    return Itemid;
            //}
            //set
            //{
            //    Itemid = value;
            //}
        }
        private int LocationID
        {
            get;
            set;

        }
        // private int Quantityvalue;
        private int QuantityValue
        {
            get;
            set;
            //get
            //{
            //    Quantityvalue= gvAMcVoucher.GetFocusedRowCellValue(colQuantity) != null ? this.UtilityMember.NumberSet.ToInteger(gvAMcVoucher.GetFocusedRowCellValue(colQuantity).ToString()) : 0;
            //    return Quantityvalue;
            //}
            //set
            //{
            //    Quantityvalue = value;
            //}
        }
        private int Inoutid { get; set; }
        private DataTable dtAssetItem { get; set; }
        public int tmpQuantity { get; set; }
        private int transVoucherMethod = 0;
        private int TransVoucherMethod
        {
            set
            {
                transVoucherMethod = value;
            }
            get
            {
                return transVoucherMethod;
            }
        }
        private int CashLedgerId
        {
            get
            {
                int cashLedgerId = 0;
                cashLedgerId = gvBank.GetRowCellValue(gvBank.FocusedRowHandle, colCashLedgerId) != null ? this.UtilityMember.NumberSet.ToInteger(gvBank.GetRowCellValue(gvBank.FocusedRowHandle, colCashLedgerId).ToString()) : 0;
                return cashLedgerId;
            }
        }
        private double LedgerAmount
        {
            get
            {
                double ledgerAmount;
                ledgerAmount = gvAMcVoucher.GetFocusedRowCellValue(colAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvAMcVoucher.GetFocusedRowCellValue(colAmount).ToString()) : 0;
                return ledgerAmount;
            }
        }
        private double AmountSummaryVal
        {
            get { return this.UtilityMember.NumberSet.ToDouble(colAmount.SummaryItem.SummaryValue.ToString()); }
        }
        private int CashBankGroupId
        {
            get
            {
                int GroupId = 0;
                if (CashLedgerId > 0)
                {
                    DataRowView dv = rglkpCashBankLedgers.GetRowByKeyValue(CashLedgerId) as DataRowView;
                    if (dv != null)
                        GroupId = this.UtilityMember.NumberSet.ToInteger(dv.Row["Group_ID"].ToString());
                }
                return GroupId;
            }
        }
        private int AccountLedgerId
        {
            get
            {
                int ledgerId = 0;
                if (ItemID > 0)
                {
                    DataRowView dv = rglkpAssetName.GetRowByKeyValue(ItemID) as DataRowView;
                    if (dv != null)
                        ledgerId = this.UtilityMember.NumberSet.ToInteger(dv.Row["ACCOUNT_LEDGER_ID"].ToString());
                }
                return ledgerId;
            }
        }
        private DataTable dtSelectedAssetItems { get; set; }

        private double CashLedgerAmount
        {
            get
            {
                double cashLedgerAmount;
                cashLedgerAmount = gvBank.GetFocusedRowCellValue(colAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvBank.GetFocusedRowCellValue(colAmount).ToString()) : 0;
                return cashLedgerAmount;
            }
        }

        private double CashBankAmount
        {
            get { return this.UtilityMember.NumberSet.ToDouble(colCashBankAmount.SummaryItem.SummaryValue.ToString()); }
        }

        private double TransSummaryVal
        {
            get { return this.UtilityMember.NumberSet.ToDouble(colAmount.SummaryItem.SummaryValue.ToString()); }
        }
        #endregion

        #region Events
        /// <summary>
        /// Load the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void frmAmcVoucherAdd_Load(object sender, EventArgs e)
        {
            Caption();
            BindGrid();
            LoadProjectDate();
            LoadCashBankLedger();
            AssignAMCVoucherDetails();
            LoadNarrationAutoComplete();
            LoadProviderAutoComplete();
            dtAMCVoucherDate.Focus();
        }
        /// <summary>
        /// Call shortcut keys
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAmcVoucherAdd_KeyDown(object sender, KeyEventArgs e)
        {
            ProcessShortcutKeys(e);
        }

        /// <summary>
        /// Save the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValidAMCDetails())
            {
                using (AMCVoucherSystem amcVoucherSystem = new AMCVoucherSystem())
                {
                    amcVoucherSystem.AmcId = AmcId;
                    amcVoucherSystem.VoucherId = VoucherId;
                    amcVoucherSystem.ProjectId = ProjectId;
                    amcVoucherSystem.VoucherDate = this.UtilityMember.DateSet.ToDate(dtAMCVoucherDate.Text, false);
                    amcVoucherSystem.BillNo = txtBillNo.Text.Trim();
                    amcVoucherSystem.Provider = txtProvider.Text;
                    amcVoucherSystem.Narration = txtNarration.Text;
                    amcVoucherSystem.Amount = this.UtilityMember.NumberSet.ToDecimal(AmountSummaryVal.ToString());
                    amcVoucherSystem.CashLedgerId = CashLedgerId;

                    DataTable dtSource = gcAMCVoucher.DataSource as DataTable;
                    DataTable dtFilteredRows = dtSource.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull)).CopyToDataTable();
                    amcVoucherSystem.dtVoucherEdit = dtFilteredRows;//To save asset amc master 
                    amcVoucherSystem.dtVoucherDetails = dtSelectedAssetItems;//To save asset amc details

                    DataTable dtBankTrans = gcBank.DataSource as DataTable;
                    DataTable dtFilteredcashbank = dtBankTrans.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull)).CopyToDataTable();
                    amcVoucherSystem.dtcashbankDetails = dtFilteredcashbank;

                    resultArgs = amcVoucherSystem.SaveAMCVoucher();
                    if (AmcId > 0)
                    {
                        for (int i = 0; i < dtFilteredRows.Rows.Count; i++)
                        {
                            if (SettingProperty.AssetListCollection.ContainsKey(i))
                            {
                                DataView dvDetails = SettingProperty.AssetListCollection[i].AsDataView();
                                dvDetails.RowFilter = "SELECT=1";
                                DataTable dtAmcDetailEdit = dvDetails.ToTable();
                                foreach (DataRow dr in dtAmcDetailEdit.Rows)
                                {
                                    int itemdetailid = this.UtilityMember.NumberSet.ToInteger(dr["ITEM_DETAIL_ID"].ToString());
                                    resultArgs = amcVoucherSystem.DeleteAssetItemDetailsByItemDeatilId(AmcId, itemdetailid);
                                }
                            }
                        }
                    }

                    if (resultArgs != null && resultArgs.Success)
                    {
                        amcVoucherSystem.AmcId = AmcId == 0 ? this.UtilityMember.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : AmcId;
                        for (int i = 0; i < dtFilteredRows.Rows.Count; i++)
                        {
                            if (SettingProperty.AssetListCollection.ContainsKey(i))
                            {
                                DataView dvDetails = SettingProperty.AssetListCollection[i].AsDataView();
                                dvDetails.RowFilter = "SELECT=1";
                                amcVoucherSystem.sequenceno = i + 1;
                                amcVoucherSystem.dtVoucherDetails = dvDetails.ToTable();
                                amcVoucherSystem.ItemAmount = this.UtilityMember.NumberSet.ToDecimal(gvAMcVoucher.GetRowCellValue(i, colAmount).ToString());
                                amcVoucherSystem.StartDate = this.UtilityMember.DateSet.ToDate(dtFilteredRows.Rows[i]["START_DATE"].ToString(), false);
                                amcVoucherSystem.DueDate = this.UtilityMember.DateSet.ToDate(dtFilteredRows.Rows[i]["DUE_DATE"].ToString(), false);
                                resultArgs = amcVoucherSystem.SaveAMCVoucherDetail();
                            }
                        }
                    }

                    if (resultArgs != null && resultArgs.Success)
                    {
                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                        ClearControls();
                        dtAMCVoucherDate.Focus();
                        if (UpdateHeld != null)
                        {
                            UpdateHeld(this, e);
                        }
                    }
                    else
                    {
                        dtAMCVoucherDate.Focus();
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbtDelete_Click(object sender, EventArgs e)
        {
            DeleteTransaction();
        }

        /// <summary>
        /// Delete Transaction 
        /// </summary>
        private void DeleteTransaction()
        {
            try
            {
                if (!string.IsNullOrEmpty(gvAMcVoucher.GetFocusedRowCellValue(colItemId).ToString()))
                {
                    if (gvAMcVoucher.RowCount > 1)
                    {
                        if (gvAMcVoucher.FocusedRowHandle != GridControl.NewItemRowHandle)
                        {

                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                gvAMcVoucher.DeleteRow(gvAMcVoucher.FocusedRowHandle);
                                gvAMcVoucher.UpdateCurrentRow();
                                gcAMCVoucher.RefreshDataSource();
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                            }
                        }

                    }
                    else if (gvAMcVoucher.RowCount == 1)
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            ConstructAMCVoucherDetail();
                            gcAMCVoucher.RefreshDataSource();
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Bank.BANK_DELETE));
                    gvAMcVoucher.FocusedColumn = colItemId;
                }
                CalculateFirstRowValue();
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        /// <summary>
        /// Load Cas bank ledger
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
                    // rglkpCashBankLedger.Properties.datasource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpCashBankLedgers, resultArgs.DataSource.Table, ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
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
        /// Grid Entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcAMCVoucher_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control)
                {
                    if (gvAMcVoucher.FocusedColumn == colItemId)
                    {
                        gvAMcVoucher.FocusedColumn = colQuantity;
                        //gvAMcVoucher.PostEditor();
                        gvAMcVoucher.SetFocusedRowCellValue(colAccountLedgerID, this.AccountLedgerId);
                        gvAMcVoucher.UpdateCurrentRow();
                        e.SuppressKeyPress = true;
                    }
                    else if (gvAMcVoucher.FocusedColumn == colQuantity)
                    {
                        gvAMcVoucher.FocusedColumn = colAmount;
                        gvAMcVoucher.UpdateCurrentRow();
                        e.SuppressKeyPress = true;
                        DataTable dtFilterdAsset = new DataTable();
                        DataView dvAssetItem = new DataView(dtAssetItem);
                        string AssetName = gvAMcVoucher.GetFocusedRowCellValue(colItemId) != null ? gvAMcVoucher.GetFocusedRowCellValue(colItemId).ToString() : string.Empty;
                        QuantityValue = this.UtilityMember.NumberSet.ToInteger(gvAMcVoucher.GetFocusedRowCellValue(colQuantity).ToString());
                        ItemID = this.UtilityMember.NumberSet.ToInteger(gvAMcVoucher.GetFocusedRowCellValue(colItemId).ToString());


                        if (!string.IsNullOrEmpty(gvAMcVoucher.GetFocusedRowCellValue(colItemId).ToString()) && QuantityValue != 0)
                        {
                            int quantity = this.UtilityMember.NumberSet.ToInteger(gvAMcVoucher.GetFocusedRowCellValue(colQuantity).ToString());
                            gvAMcVoucher.CloseEditor();
                            e.Handled = true;

                            frmAssetItemList AssetItemList = new frmAssetItemList(ItemID, quantity, gvAMcVoucher.FocusedRowHandle, Inoutid, AssetInOut.AMC, ProjectId);
                            AssetItemList.CommonLookupVisibility = LayoutVisibility.Never;
                            AssetItemList.AMCVisible = AssetItemList.CostCentreVisibile = AssetItemList.InsuranceVisibile = AssetItemList.DeleteVisibile = false;
                            AssetItemList.ShowDialog();
                            if (SettingProperty.AssetListCollection.Count > 0)
                            {
                                dtSelectedAssetItems = SettingProperty.AssetListCollection[gvAMcVoucher.FocusedRowHandle];
                                foreach (DataRow dr in dtSelectedAssetItems.Rows)
                                {
                                    if (dr["ITEM_DETAIL_ID"].ToString() != "" && dr["ITEM_DETAIL_ID"] != DBNull.Value)
                                    {
                                        if (dtSelectedAssetItems != null && dtSelectedAssetItems.Rows.Count > 0)
                                        {
                                            DataView dvSelectedItems = dtSelectedAssetItems.AsDataView();
                                            dvSelectedItems.RowFilter = "SELECT=1";
                                            DataTable dtUpdatedSelectedItems = dvSelectedItems.ToTable();
                                            if (dtUpdatedSelectedItems != null && dtUpdatedSelectedItems.Rows.Count > 0)
                                            {
                                                gvAMcVoucher.SetFocusedRowCellValue(colQuantity, dtUpdatedSelectedItems.Rows.Count);
                                            }
                                            dvSelectedItems.RowFilter = "";
                                        }
                                        gvAMcVoucher.UpdateTotalSummary();
                                        CalculateFirstRowValue();
                                    }
                                    else
                                    {
                                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AMCVoucher.ASSET_NO_ITEM_DETAIL));
                                        gvAMcVoucher.SetFocusedRowCellValue(colQuantity, 0);
                                        gvAMcVoucher.FocusedColumn = colItemId;
                                    }
                                }
                                if (AssetItemList.Dialogresult == DialogResult.OK)
                                {
                                    if (AssetItemList.Quantity > 0)
                                    {
                                        gvAMcVoucher.SetFocusedRowCellValue(colQuantity, AssetItemList.Quantity);
                                        tmpQuantity = AssetItemList.Quantity;
                                    }
                                    //decimal amount = AssetItemList.Amount;
                                    //if (amount > 0) { gvAMcVoucher.SetFocusedRowCellValue(colAmount, amount.ToString()); }
                                    gvAMcVoucher.UpdateTotalSummary();
                                    CalculateFirstRowValue();
                                    gvAMcVoucher.FocusedColumn = colAmount;
                                }
                            }
                            else
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AMCVoucher.ASSET_NO_ITEM_DETAIL));
                                gvAMcVoucher.SetFocusedRowCellValue(colQuantity, 0);
                                gvAMcVoucher.FocusedColumn = colItemId;
                            }

                        }
                    }
                    else if (gvAMcVoucher.FocusedColumn == colAmount)
                    {
                        gvAMcVoucher.FocusedColumn = colStartDate;
                        // gvAMcVoucher.PostEditor();
                        gvAMcVoucher.UpdateCurrentRow();
                        e.SuppressKeyPress = true;
                    }
                    else if (gvAMcVoucher.FocusedColumn == colStartDate)
                    {
                        gvAMcVoucher.FocusedColumn = ColDueDate;
                        // gvAMcVoucher.PostEditor();
                        gvAMcVoucher.UpdateCurrentRow();
                        e.SuppressKeyPress = true;
                    }

                    else if (gvAMcVoucher.FocusedColumn == ColDueDate)
                    {
                        gvAMcVoucher.FocusedColumn = colDelete;
                        // gvAMcVoucher.PostEditor();
                        gvAMcVoucher.UpdateCurrentRow();
                        e.SuppressKeyPress = true;


                        gvAMcVoucher.FocusedColumn = colDelete;
                        // gvAMcVoucher.PostEditor();
                        gvAMcVoucher.UpdateCurrentRow();
                        e.SuppressKeyPress = true;
                        string AssetName = gvAMcVoucher.GetFocusedRowCellValue(colItemId) != null ? gvAMcVoucher.GetFocusedRowCellValue(colItemId).ToString() : string.Empty;
                        string Quantity = gvAMcVoucher.GetFocusedRowCellValue(colQuantity) != null ? gvAMcVoucher.GetFocusedRowCellValue(colQuantity).ToString() : string.Empty;
                        string Amount = gvAMcVoucher.GetFocusedRowCellValue(colAmount) != null ? gvAMcVoucher.GetFocusedRowCellValue(colAmount).ToString() : string.Empty;
                        string startdate = gvAMcVoucher.GetFocusedRowCellValue(colStartDate) != null ? gvAMcVoucher.GetFocusedRowCellValue(colStartDate).ToString() : string.Empty;
                        string dueDate = gvAMcVoucher.GetFocusedRowCellValue(ColDueDate) != null ? gvAMcVoucher.GetFocusedRowCellValue(ColDueDate).ToString() : string.Empty;
                        // if (!string.IsNullOrEmpty(Location) && !string.IsNullOrEmpty(AssetName) && !string.IsNullOrEmpty(Quantity) && !string.IsNullOrEmpty(Amount))
                        if (!string.IsNullOrEmpty(AssetName) && !string.IsNullOrEmpty(Quantity) && !string.IsNullOrEmpty(Amount) && !string.IsNullOrEmpty(startdate) && !string.IsNullOrEmpty(dueDate) && gvAMcVoucher.IsLastRow)
                        {
                            gvAMcVoucher.AddNewRow();
                            gvAMcVoucher.FocusedColumn = colItemId;
                            //  LoadActiveAssets();
                            gvAMcVoucher.ShowEditor();
                        }
                        else
                        {
                            gvAMcVoucher.CloseEditor();
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                            FocusCashTransactionGrid();
                        }
                    }
                }
                else if (gvAMcVoucher.IsFirstRow && gvAMcVoucher.FocusedColumn == ColDueDate && e.Shift && e.KeyCode == Keys.Tab)
                {
                    gvAMcVoucher.CloseEditor();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    FocusCashTransactionGrid();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        /// <summary>
        /// Focus the Name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcAMCVoucher_Leave(object sender, EventArgs e)
        {
            gvAMcVoucher.OptionsSelection.EnableAppearanceFocusedCell = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void SetSelectedList()
        {
            if (rccmbAssetId.DataSource != null && rccmbAssetId.Items.Count > 0)
            {
                foreach (CheckedListBoxItem item in rccmbAssetId.Items)
                {
                    if (dtActiveAsset != null && dtActiveAsset.Rows.Count > 0)
                    {
                        dtActiveAsset.Select().Where(x => x.Field<string>("ASSET_ID") == item.Value.ToString()).ToList<DataRow>().ForEach(r => { r["SELECT"] = item.CheckState == CheckState.Checked ? 1 : 0; });
                        dtActiveAsset.AcceptChanges();
                    }
                }
            }
        }

        /// <summary>
        /// enable the enter Screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcAMCVoucher_Enter(object sender, EventArgs e)
        {
            gvAMcVoucher.OptionsSelection.EnableAppearanceFocusedCell = true;
        }

        private void glkpCashBankLedger_Leave(object sender, EventArgs e)
        {
            if (gvBank.IsFirstRow)
            {
                CalculateFirstRowValue();
            }
            try
            {
                if (this.CashLedgerId > 0)
                {
                    if (VoucherId == 0)
                    {
                        gvBank.PostEditor();
                        gvBank.UpdateCurrentRow();
                        DataTable dtCashTrans = gcBank.DataSource as DataTable;
                        string Balance = GetLedgerBalanceValues(dtCashTrans, CashLedgerId); //ShowLedgerBalance(CashLedgerId, dtCashTrans, false);
                        if (Balance != string.Empty)
                        {
                            gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colLedgerBalance, Balance);
                        }
                    }
                }
                else { gvBank.UpdateCurrentRow(); }

            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        private void rglkpCashBankLedgers_EditValueChanged(object sender, EventArgs e)
        {
            //To retain the ledger in  the Cash/Bank Grid
            if (isMouseClicked)
            {
                SendKeys.Send("{tab}"); isMouseClicked = false;
            }
        }

        private void rglkpCashBankLedgers_MouseDown(object sender, MouseEventArgs e)
        {
            //To Identify the Mouse Click Event
            if (e.Button == MouseButtons.Left)
                isMouseClicked = true;
        }

        private void ProcessShortcutKeys(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode.Equals(Keys.F5))
            {
                if (VoucherId == 0)
                {
                    ShowProjectSelectionWindow();
                    if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic)
                    {
                        LoadVoucherNo();
                    }
                    else if (TransVoucherMethod == (int)TransactionVoucherMethod.Manual)
                    {
                        txtVoucherNo.Text = string.Empty;
                    }
                }
            }
            //else if (e.KeyData == (Keys.Alt | Keys.A))
            //{
            //    frmVendorInfoAdd ManufacturerAdd = new frmVendorInfoAdd(0, VendorManufacture.Manufacture);
            //    ManufacturerAdd.ShowDialog();
            //}
            else if (e.KeyCode.Equals(Keys.F12))
            {
                frmAssetSettings setting = new frmAssetSettings();
                setting.ShowDialog();
            }
            else if (e.KeyCode == (Keys.Alt | Keys.D))
            {
                DeleteTransaction();
            }
            else if (e.KeyCode.Equals(Keys.F3))
            {
                dtAMCVoucherDate.Focus();
            }
            else if (e.KeyCode.Equals(Keys.F4))
            {
                DateTime dtYearTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
                dtAMCVoucherDate.DateTime = (dtAMCVoucherDate.DateTime < dtYearTo) ? dtAMCVoucherDate.DateTime.AddDays(1) : dtYearTo;
            }

            else if (e.KeyData == (Keys.Alt | Keys.M))
            {
                frmMapLocation maplocation = new frmMapLocation();
                maplocation.ShowDialog();
            }
            //else if (e.KeyData == (Keys.Alt | Keys.U))
            //{
            //    frmCustodiansAdd custodianAdd = new frmCustodiansAdd();
            //    custodianAdd.ShowDialog();
            //}

            else if (e.KeyData == (Keys.Alt | Keys.B))
            {
                //if (rgVoucherType.SelectedIndex == 0)
                // {
                frmLedgerDetailAdd BankAdd = new frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.BK, ProjectId);
                BankAdd.ShowDialog();
                LoadCashBankLedger();
                //}
            }
        }

        private void ucAssetVoucherShortcuts_BankAccountClicked(object sender, EventArgs e)
        {
            ACPP.Modules.Master.frmLedgerDetailAdd frmBank = new ACPP.Modules.Master.frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.BK, ProjectId);
            frmBank.ShowDialog();
            LoadCashBankLedger();
        }

        private void ucAssetVoucherShortcuts_ConfigureClicked(object sender, EventArgs e)
        {
            frmAssetSettings assetsetting = new frmAssetSettings();
            assetsetting.ShowDialog();
        }

        private void ucAssetVoucherShortcuts_DateClicked(object sender, EventArgs e)
        {
            DateTime dtYearTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            dtAMCVoucherDate.DateTime = (dtAMCVoucherDate.DateTime < dtYearTo) ? dtAMCVoucherDate.DateTime.AddDays(1) : dtYearTo;
        }

        private void ucAssetVoucherShortcuts_MappingClicked(object sender, EventArgs e)
        {
            frmMapLocation maplocation = new frmMapLocation();
            maplocation.ShowDialog();
        }

        private void ucAssetVoucherShortcuts_NextDateClicked(object sender, EventArgs e)
        {
            DateTime dtYearTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            dtAMCVoucherDate.DateTime = (dtAMCVoucherDate.DateTime < dtYearTo) ? dtAMCVoucherDate.DateTime.AddDays(1) : dtYearTo;
        }

        private void ucAssetVoucherShortcuts_ProjectClicked(object sender, EventArgs e)
        {
            frmProjectSelection projectSelection = new frmProjectSelection();
            projectSelection.ShowDialog();
        }

        private void RealColumnEditTransAmount()
        {
            colQuantity.RealColumnEdit.Leave += new System.EventHandler(RealColumnEditAMCAmount_EditValueChanged);
            this.gvAMcVoucher.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvAMcVoucher.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colQuantity)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvAMcVoucher.ShowEditorByMouse();
                    }));
                }
            };
        }

        private void RealColumnEditCashTransAmount()
        {
            colCashBankAmount.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditCashTransAmount_EditValueChanged);
            this.gvBank.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvBank.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colCashBankAmount)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvBank.ShowEditorByMouse();
                    }));
                }
            };
        }

        void RealColumnEditAMCAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvAMcVoucher.PostEditor();
            gvAMcVoucher.UpdateCurrentRow();
            if (gvAMcVoucher.ActiveEditor == null)
            {
                gvAMcVoucher.ShowEditor();
            }

            TextEdit txtTransAmount = edit as TextEdit;
            int grpCounts = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.DecimalPlaces) + 1);
            if (txtTransAmount.Text.Length > grpCounts && txtTransAmount.SelectionLength == txtTransAmount.Text.Length)
                txtTransAmount.Select(txtTransAmount.Text.Length - grpCounts, 0);
            //if (LedgerId > 0)
            //{
            //    DataTable dtTrans = gcPurchase.DataSource as DataTable;
            //    string Balance = GetLedgerBalanceValues(dtTrans, LedgerId); //ShowLedgerBalance(LedgerId, dtTrans, true);
            //    if (Balance != string.Empty) { gvPurchase.SetRowCellValue(gvPurchase.FocusedRowHandle, colBankCurrentBalance, Balance); }
            //}

            CalculateFirstRowValue();
        }

        void RealColumnEditCashTransAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            try
            {
                BaseEdit edit = sender as BaseEdit;
                if (edit.EditValue == null) return;
                gvBank.PostEditor();
                gvBank.UpdateCurrentRow();
                if (gvBank.ActiveEditor == null)
                    gvBank.ShowEditor();

                TextEdit txtTransAmount = edit as TextEdit;
                int grpCounts = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.DecimalPlaces) + 1);
                if (txtTransAmount.Text.Length > grpCounts && txtTransAmount.SelectionLength == txtTransAmount.Text.Length)
                    txtTransAmount.Select(txtTransAmount.Text.Length - grpCounts, 0);

                if (CashLedgerId > 0)
                {
                    DataTable dtCashTrans = gcBank.DataSource as DataTable;
                    string Balance = GetLedgerBalanceValues(dtCashTrans, CashLedgerId); //ShowLedgerBalance(CashLedgerId, dtCashTrans, false);
                    if (Balance != string.Empty) { gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colLedgerBalance, Balance.ToString()); }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void bbiDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteTransaction();
        }

        private void glkpCashBankLedger_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                ACPP.Modules.Master.frmLedgerDetailAdd frmBank = new ACPP.Modules.Master.frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.BK, ProjectId);
                frmBank.ShowDialog();
                LoadCashBankLedger();
            }
        }

        private void glkpExpenseLedger_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                ACPP.Modules.Master.frmLedgerDetailAdd frmBank = new ACPP.Modules.Master.frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.GN, ProjectId);
                frmBank.ShowDialog();
                LoadCashBankLedger();
            }
        }

        private void rglkpAssetName_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                GridLookUpEdit gridLKPEdit = sender as GridLookUpEdit;
                if (gridLKPEdit.EditValue != null)
                {
                    DataRowView drv = gridLKPEdit.GetSelectedDataRow() as DataRowView;
                    if (drv != null)
                    {
                        ItemID = this.UtilityMember.NumberSet.ToInteger(drv["ITEM_ID"].ToString());
                        if (ItemID > 0)
                        {
                            using (AssetItemSystem assetitemsystem = new AssetItemSystem())
                            {
                                Inoutid = assetitemsystem.FetchInoutItemByItemId(ItemID);
                            }
                        }
                    }
                    if (!e.Cancel)
                    {
                        gvAMcVoucher.SetFocusedRowCellValue(colItemId, ItemID);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally { }
        }

        private void rdtDueDate_Validating(object sender, CancelEventArgs e)
        {
            string StartDate = string.Empty;
            string DueDate = string.Empty;
            bool isValid = true;
            DataTable dtSales = gcAMCVoucher.DataSource as DataTable;
            DataView dv = new DataView(dtSales);
            foreach (DataRowView drAMC in dv)
            {
                StartDate = drAMC[appSchema.AppSchema.AMCDetails.START_DATEColumn.ColumnName].ToString();
                DueDate = drAMC[appSchema.AppSchema.AMCDetails.DUE_DATEColumn.ColumnName].ToString();
                if (!this.UtilityMember.DateSet.ValidateDate(this.UtilityMember.DateSet.ToDate(StartDate, false), this.UtilityMember.DateSet.ToDate(DueDate, false)))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AMCVoucher.DUE_DATE_GREATER_THAN_START_DATE));
                    gvAMcVoucher.FocusedColumn = ColDueDate;
                    isValid = false;
                }
            }
        }

        private void frmAmcVoucherAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            SettingProperty.AssetListCollection.Clear();
        }

        private void gvBank_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                if (gvBank.GetRowCellValue(e.RowHandle, colCashLedgerId) != null)
                {
                    int GroupId = 0;
                    int LedgerId = gvBank.GetRowCellValue(e.RowHandle, colCashLedgerId) != null ? this.UtilityMember.NumberSet.ToInteger(gvBank.GetRowCellValue(e.RowHandle, colCashLedgerId).ToString()) : 0;
                    if (LedgerId > 0)
                    {
                        DataRowView drvLedger = rglkpCashBankLedgers.GetRowByKeyValue(LedgerId) as DataRowView;
                        if (drvLedger != null)
                        {
                            GroupId = this.UtilityMember.NumberSet.ToInteger(drvLedger["GROUP_ID"].ToString());  //CashBankGroupId;//ledgerSystem.FetchLedgerGroupById();
                        }

                        if ((e.Column == colRefNo || e.Column == colMaterializedOn) &&
                        (GroupId == (int)FixedLedgerGroup.Cash))
                        {
                            e.Appearance.BackColor = Color.LightGray;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally { }
        }

        private void gvBank_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                if (CashLedgerId > 0)
                {
                    if ((CashBankGroupId == (int)FixedLedgerGroup.Cash)
                        && (gvBank.FocusedColumn == colRefNo || gvBank.FocusedColumn == colMaterializedOn))
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally { }
        }

        private void rglkpCashBankLedgers_Leave(object sender, EventArgs e)
        {
            try
            {
                if (CashLedgerId > 0)
                {
                    if (VoucherId == 0)
                    {
                        gvBank.PostEditor();
                        gvBank.UpdateCurrentRow();
                        DataTable dtCashTrans = gcBank.DataSource as DataTable;
                        string Balance = GetLedgerBalanceValues(dtCashTrans, CashLedgerId); //ShowLedgerBalance(CashLedgerId, dtCashTrans, false);
                        if (Balance != string.Empty)
                        {
                            gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colLedgerBalance, Balance);
                        }
                    }
                }
                else { gvBank.UpdateCurrentRow(); }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        private void gvBank_GotFocus(object sender, EventArgs e)
        {
            BaseView view = sender as BaseView;
            if (view == null)
                return;

            if (MouseButtons == System.Windows.Forms.MouseButtons.Left)
                return;
            view.ShowEditor();
            TextEdit editor = view.ActiveEditor as TextEdit;
            if (editor != null)
            {
                editor.SelectAll();
                editor.Focus();
            }
        }

        private void ucCaptionProject_Click(object sender, EventArgs e)
        {

        }

        private void gcBank_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                bool canFocusOtherCharges = false;
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab)
                    && !e.Shift && !e.Alt && !e.Control
                    && (gvBank.FocusedColumn == colCashBankAmount || gvBank.FocusedColumn == colMaterializedOn))//&& (gvBank.IsLastRow))
                {
                    gvBank.SetFocusedRowCellValue(colCashLedgerId, this.CashLedgerId);

                    if ((gvBank.FocusedColumn == colMaterializedOn && CashBankGroupId == (int)FixedLedgerGroup.BankAccounts)
                        || (gvBank.FocusedColumn == colCashBankAmount && CashBankGroupId == (int)FixedLedgerGroup.Cash || CashBankGroupId == 0) && gvBank.IsLastRow)
                    {
                        canFocusOtherCharges = true;
                    }
                    if (canFocusOtherCharges)
                    {
                        gvBank.CloseEditor();
                        e.Handled = true;
                        e.SuppressKeyPress = true;

                        txtNarration.Select();
                        txtNarration.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void rglkpCashBankLedgers_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                GridLookUpEdit gridLKPEdit = sender as GridLookUpEdit;
                int Group = 0;
                if (gridLKPEdit.EditValue != null)
                {
                    int LedgerID = this.UtilityMember.NumberSet.ToInteger(gridLKPEdit.EditValue.ToString());

                    DataRowView drvLedger = rglkpCashBankLedgers.GetRowByKeyValue(LedgerID) as DataRowView;
                    if (drvLedger != null)
                    {
                        Group = this.UtilityMember.NumberSet.ToInteger(drvLedger["GROUP_ID"].ToString()); //CashBankGroupId;//ledgerSystem.FetchLedgerGroupById();
                    }

                    gvBank.SetFocusedRowCellValue(colCashLedgerId, LedgerID);

                    if (AmountSummaryVal > 0 && CashBankAmount <= AmountSummaryVal && CashLedgerAmount < 1)
                    {
                        double Amt = AmountSummaryVal - CashBankAmount;
                        gvBank.SetFocusedRowCellValue(colCashBankAmount, Amt.ToString());
                        gvBank.SetFocusedRowCellValue(colCashLedgerId, LedgerID);
                        gvBank.PostEditor();
                        gvBank.UpdateCurrentRow();
                    }
                    EnableCashBankFields();
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Bind the Grid 
        /// </summary>
        private void BindGrid()
        {
            ConstructAMCVoucherDetail();
            ConstructCashBankDetail();
            LoadGroupName();
            LoadAssetName();
            LoadAMCType();
            rdtMaterializedOn.MinValue = dtAMCVoucherDate.DateTime;
        }
        /// <summary>
        /// Asset ID is Trim
        /// </summary>
        /// <param name="assetIds"></param>
        /// <returns></returns>
        private string TrimAssetIds(string assetIds)
        {
            string assetId = string.Empty;
            string[] assetIdSplit = assetIds.Split(',');
            for (int i = 0; i < assetIdSplit.Count(); i++)
            {
                assetId += assetIdSplit[i].TrimStart().ToString() + ',';

            }
            return assetId.TrimEnd(',');
        }

        /// <summary>
        /// Get Selected Asset Value
        /// </summary>
        private void GetSelectedAssetValue()
        {
            using (AssetItemSystem itemSystem = new AssetItemSystem())
            {
                string assetIds = gvAMcVoucher.GetFocusedRowCellValue(colItemId).ToString();
                if (!string.IsNullOrEmpty(assetIds))
                {
                    if (assetIds.Contains(Delimiter.Comma))
                    {
                        string[] assetIdSplit = assetIds.Split(',');
                        Quantity = assetIdSplit.Count();
                        itemSystem.AssetID = TrimAssetIds(assetIds);
                    }
                    else
                    {
                        Quantity = 1;
                        itemSystem.AssetID = assetIds;
                    }
                    resultArgs = itemSystem.FetchAssetItemDetailByAssetId();
                    if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        ItemTotalRate = this.UtilityMember.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][AMOUNT].ToString());
                    }
                    gvAMcVoucher.SetRowCellValue(gvAMcVoucher.FocusedRowHandle, colQuantity, Quantity);
                    gvAMcVoucher.SetRowCellValue(gvAMcVoucher.FocusedRowHandle, colAmount, ItemTotalRate);
                    gvAMcVoucher.ShowEditor();
                }
                else
                {
                    gvAMcVoucher.SetRowCellValue(gvAMcVoucher.FocusedRowHandle, colQuantity, null);
                    gvAMcVoucher.SetRowCellValue(gvAMcVoucher.FocusedRowHandle, colAmount, null);
                    gvAMcVoucher.ShowEditor();
                }
            }
        }
        /// <summary>
        /// Load the Asset Name
        /// </summary>
        private void LoadAssetName()
        {
            try
            {
                using (AssetItemSystem AssetItemSystem = new AssetItemSystem())
                {
                    resultArgs = AssetItemSystem.FetchAMCAssetItems();
                    dtAssetItem = resultArgs.DataSource.Table;
                    if (dtAssetItem != null && dtAssetItem.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpAssetName, dtAssetItem, AssetItemSystem.AppSchema.ASSETItem.ASSET_ITEMColumn.ColumnName, AssetItemSystem.AppSchema.ASSETItem.ITEM_IDColumn.ColumnName);
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
        /// Load the Project Date
        /// </summary>
        private void LoadProjectDate()
        {
            dtAMCVoucherDate.DateTime = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtAMCVoucherDate.DateTime = UtilityMember.DateSet.ToDate(RecentVoucherDate, false);
            dtAMCVoucherDate.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtAMCVoucherDate.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            dtAMCVoucherDate.DateTime = (!string.IsNullOrEmpty(RecentVoucherDate)) ? UtilityMember.DateSet.ToDate(RecentVoucherDate, false) : UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtAMCVoucherDate.DateTime = dtAMCVoucherDate.DateTime.AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// Load the Caption
        /// </summary>
        private void Caption()
        {
            ucCaptionProject.Caption = Project;
            this.Text = AmcId == 0 ? this.GetMessage(MessageCatalog.Asset.AMCVoucher.AMCVOUCHER_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.AMCVoucher.AMCVOUCHER_EDIT_CAPTION);
        }

        /// <summary>
        /// Construct the DataTable
        /// </summary>
        private void ConstructAMCVoucherDetail()
        {
            DataTable dtAMCVoucherDetails = new DataTable();
            dtAMCVoucherDetails.Columns.Add("LOCATION_ID", typeof(int));
            dtAMCVoucherDetails.Columns.Add("ITEM_ID", typeof(int));
            dtAMCVoucherDetails.Columns.Add("GROUP_ID", typeof(int));
            dtAMCVoucherDetails.Columns.Add("ASSET_ID", typeof(string));
            dtAMCVoucherDetails.Columns.Add("QUANTITY", typeof(int));
            dtAMCVoucherDetails.Columns.Add("AMC_TYPE", typeof(int));
            dtAMCVoucherDetails.Columns.Add("START_DATE", typeof(DateTime));
            dtAMCVoucherDetails.Columns.Add("AMOUNT", typeof(decimal));
            dtAMCVoucherDetails.Columns.Add("DUE_DATE", typeof(DateTime));
            dtAMCVoucherDetails.Columns.Add("LEDGER_ID", typeof(int));
            dtAMCVoucherDetails.Columns.Add("CHEQUE_NO", typeof(string));
            dtAMCVoucherDetails.Columns.Add("MATERIALIZED_ON", typeof(DateTime));
            gcAMCVoucher.DataSource = dtAMCVoucherDetails;
            gvAMcVoucher.AddNewRow();
        }

        private void ConstructCashBankDetail()
        {
            DataTable dtBank = new DataTable();
            dtBank.Columns.Add("LEDGER_ID", typeof(int));
            dtBank.Columns.Add("AMOUNT", typeof(Decimal));
            dtBank.Columns.Add("CHEQUE_NO", typeof(string));
            dtBank.Columns.Add("MATERIALIZED_ON", typeof(DateTime));
            dtBank.Columns.Add("LEDGER_BALANCE", typeof(decimal));
            gcBank.DataSource = dtBank;
            gvBank.AddNewRow();
        }
        /// <summary>
        /// Load Group Name
        /// </summary>
        private void LoadGroupName()
        {
            try
            {
                using (AssetClassSystem AssetGroupSystem = new AssetClassSystem())
                {
                    resultArgs = AssetGroupSystem.FetchClassDetails();
                    if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs != null)
                    {
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpAssetGroup, resultArgs.DataSource.Table, AssetGroupSystem.AppSchema.ASSETClassDetails.ASSET_CLASSColumn.ColumnName, AssetGroupSystem.AppSchema.ASSETClassDetails.ASSET_CLASS_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        /// <summary>
        /// Load AMC type
        /// </summary>
        private void LoadAMCType()
        {
            try
            {
                EnumSetMember eumSetMembers = new EnumSetMember();
                AssetAMCVoucher amcvoucher = new AssetAMCVoucher();
                this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpAMCType, eumSetMembers.GetEnumDataSource(amcvoucher, Sorting.None).ToTable(),
                    EnumColumns.Name.ToString(),
                    EnumColumns.Id.ToString());
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        /// <summary>
        /// Load Location Name
        /// </summary>
        private void LoadLocationName()
        {
            try
            {
                using (LocationSystem LocationSystem = new LocationSystem())
                {
                    LocationSystem.ProjectId = ProjectId;
                    resultArgs = LocationSystem.FetchProjectLocationByProjectId();
                    if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs != null)
                    {
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpLocationName, resultArgs.DataSource.Table, LocationSystem.AppSchema.ASSETLocationDetails.LOCATIONColumn.ColumnName, LocationSystem.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        /// <summary>
        /// Clear the Controls
        /// </summary>
        private void ClearControls()
        {
            if (AmcId == 0)
            {
                BindGrid();
                // glkpCashBankLedger.Text = glkpExpenseLedger.Text = string.Empty;
                LoadCashBankLedger();
                // LoadExpenseLedgers();
                dtAMCVoucherDate.DateTime = UtilityMember.DateSet.ToDate(DateTime.Now.ToString(), false);
                txtVoucherNo.Text = txtBillNo.Text = txtNarration.Text = txtProvider.Text = string.Empty;
                LoadVoucherNo();
                LoadProviderAutoComplete();
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
        /// Edit the Voucher
        /// </summary>
        private void AssignAMCVoucherDetails()
        {
            try
            {
                if (AmcId > 0)
                {
                    using (AMCVoucherSystem amcVoucher = new AMCVoucherSystem(AmcId))
                    {
                        amcVoucher.AmcId = AmcId;
                        dtAMCVoucherDate.DateTime = amcVoucher.AmcDate;
                        txtVoucherNo.Text = amcVoucher.VoucherIDs.ToString();
                        txtBillNo.Text = amcVoucher.BillNo;
                        txtProvider.Text = amcVoucher.Provider;
                        VoucherId = amcVoucher.VoucherIDs;
                        txtNarration.Text = amcVoucher.Narration;
                        resultArgs = amcVoucher.FetchAMCDetailsById();
                        if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            if (!resultArgs.DataSource.Table.Columns.Contains("CHEQUE_NO"))
                                resultArgs.DataSource.Table.Columns.Add("CHEQUE_NO", typeof(string));
                            if (!resultArgs.DataSource.Table.Columns.Contains("MATERIALIZED_ON"))
                                resultArgs.DataSource.Table.Columns.Add("MATERIALIZED_ON", typeof(DateTime));

                            gcAMCVoucher.DataSource = resultArgs.DataSource.Table;
                            gcAMCVoucher.RefreshDataSource();
                        }
                        using (AssetInwardOutwardSystem InwardOutwardSystem = new AssetInwardOutwardSystem())
                        {
                            resultArgs = InwardOutwardSystem.FetchCashBankByVoucherId(VoucherId);
                            if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                            {
                                gcBank.DataSource = resultArgs.DataSource.Table;
                                gcBank.RefreshDataSource();
                                EnableCashBankFields();
                                CalculateFirstRowValue();
                            }
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

        /// <summary>
        /// Load name and address auto complete method
        /// </summary>
        private void LoadNameAddressAutoComplete()
        {
            //try
            //{
            //    using (VoucherTransactionSystem vouchermastersystem = new VoucherTransactionSystem())
            //    {
            //        resultArgs = vouchermastersystem.FetchAutoFetchNames();
            //        if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
            //        {
            //            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            //            foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
            //            {
            //                collection.Add(dr[vouchermastersystem.AppSchema.VoucherMaster.NAME_ADDRESSColumn.ColumnName].ToString());
            //            }
            //            txtNameAddress.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //            txtNameAddress.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //            txtNameAddress.MaskBox.AutoCompleteCustomSource = collection;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageRender.ShowMessage(ex.ToString(), true);
            //}
            //finally { }
        }

        /// <summary>
        /// Load narration auto complete
        /// </summary>
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

        private void LoadProviderAutoComplete()
        {
            try
            {
                using (AMCVoucherSystem amcVoucherSystem = new AMCVoucherSystem())
                {
                    resultArgs = amcVoucherSystem.AutoFetchProviderName();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataView dvNarration = resultArgs.DataSource.Table.AsDataView();
                        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            collection.Add(dr[amcVoucherSystem.AppSchema.AMCDetails.PROVIDERColumn.ColumnName].ToString());
                        }
                        txtProvider.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        txtProvider.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        txtProvider.MaskBox.AutoCompleteCustomSource = collection;
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
        /// Load Voucher no from master trans table
        /// </summary>
        private void LoadVoucherNo()
        {
            using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
            {
                voucherTransaction.VoucherType = VoucherSubTypes.PY.ToString();
                voucherTransaction.ProjectId = ProjectId;
                voucherTransaction.VoucherDate = this.UtilityMember.DateSet.ToDate(dtAMCVoucherDate.Text, false);
                txtVoucherNo.Text = voucherTransaction.TempVoucherNo();
            }
        }

        /// <summary>
        /// Validating amc grid, expense ledger and cash bank ledger
        /// </summary>
        /// <returns></returns>
        private bool IsValidAMCDetails()
        {
            bool isAMCTrue = true;
            try
            {
                if (!IsValidAMCGrid())
                {
                    isAMCTrue = false;
                }
                else if (AmountSummaryVal != CashBankAmount)
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_AMOUNT_MISMATCH));
                    isAMCTrue = false;
                    gvBank.FocusedColumn = colCashBankAmount;
                }
                //else if (string.IsNullOrEmpty(glkpExpenseLedger.Text) || glkpExpenseLedger.EditValue.Equals("0") || glkpExpenseLedger.EditValue == null)
                //{
                //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AMCVoucher.AMC_EXPENSE_LEDGER_EMPTY));
                //    this.SetBorderColorForGridLookUpEdit(glkpExpenseLedger);
                //    isAMCTrue = false;
                //    glkpExpenseLedger.Focus();
                //}
                //else if (string.IsNullOrEmpty(/glkpCashBankLedger.Text) || glkpCashBankLedger.EditValue.Equals("0") || glkpCashBankLedger.EditValue == null)
                //{
                //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.PurchaseVoucher.PURCHASE_ASSET_CASH_BANK_LEDGER_EMPTY));
                //    this.SetBorderColorForGridLookUpEdit(glkpCashBankLedger);
                //    isAMCTrue = false;
                //    glkpCashBankLedger.Focus();
                //}
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally
            {
            }
            return isAMCTrue;
        }
        /// <summary>
        /// Validate the grid Controls
        /// </summary>
        /// <returns></returns>
        private bool IsValidAMCGrid()
        {
            DataTable dtSales = gcAMCVoucher.DataSource as DataTable;
            int ItemId = 0;
            string AssetId = string.Empty;
            string StartDate = string.Empty;
            string DueDate = string.Empty;
            // int LocationId = 0;
            double Amount = 0;
            int RowPosition = 0;
            bool isValid = false;
            // string validateMessage = "Required Information not filled, AMC Voucher is not filled fully";
            DataView dv = new DataView(dtSales);
            dv.RowFilter = "(ITEM_ID>0)";
            gvAMcVoucher.FocusedColumn = ColItemName;
            if (dv.Count > 0)
            {
                isValid = true;
                foreach (DataRowView drAMC in dv)
                {
                    // LocationId = this.UtilityMember.NumberSet.ToInteger(drAMC[appSchema.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName].ToString());
                    ItemId = this.UtilityMember.NumberSet.ToInteger(drAMC[appSchema.AppSchema.ASSETItem.ITEM_IDColumn.ColumnName].ToString());
                    StartDate = drAMC[appSchema.AppSchema.AMCDetails.START_DATEColumn.ColumnName].ToString();
                    DueDate = drAMC[appSchema.AppSchema.AMCDetails.DUE_DATEColumn.ColumnName].ToString();
                    Amount = this.UtilityMember.NumberSet.ToDouble(drAMC["AMOUNT"].ToString());

                    if ((ItemId == 0 || string.IsNullOrEmpty(StartDate) || string.IsNullOrEmpty(DueDate) || Amount == 0))
                    {
                        //if (LocationId == 0)
                        //{
                        //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Location.LOCATION_NAME_EMPTY));
                        //    gvAMcVoucher.FocusedColumn = colLocation;
                        //}
                        if (ItemId == 0)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.SalesVoucher.SALES_ASSET_NAME_EMPTY));
                            gvAMcVoucher.FocusedColumn = colItemId;
                        }

                        else if (string.IsNullOrEmpty(StartDate))
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AMCVoucher.AMCVOUCHER_START_DATE_EMPTY));
                            gvAMcVoucher.FocusedColumn = colStartDate;
                        }
                        else if (string.IsNullOrEmpty(DueDate))
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AMCVoucher.AMCVOUCHER_DUE_DATE_EMPTY));
                            gvAMcVoucher.FocusedColumn = ColDueDate;
                        }
                        isValid = false;
                        break;
                    }
                    if (!this.UtilityMember.DateSet.ValidateDate(this.UtilityMember.DateSet.ToDate(StartDate, false), this.UtilityMember.DateSet.ToDate(DueDate, false)))
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AMCVoucher.DUE_DATE_GREATER_THAN_START_DATE));
                        gvAMcVoucher.FocusedColumn = ColDueDate;
                        isValid = false;
                    }
                    RowPosition = RowPosition + 1;
                }
            }
            else
            {
                isValid = false;
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_NAME_EMPTY));
                gvAMcVoucher.FocusedColumn = colItemId;
            }
            if (!isValid)
            {
                gvAMcVoucher.CloseEditor();
                gvAMcVoucher.FocusedRowHandle = gvAMcVoucher.GetRowHandle(RowPosition);
                gvAMcVoucher.ShowEditor();
            }
            return isValid;
        }

        /// <summary>
        /// After delete grid row set focus on date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dtAMCVoucherDate.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtAMCVoucherDate_EditValueChanged(object sender, EventArgs e)
        {
            TransVoucherMethod = (int)TransactionVoucherMethod.Automatic;
            if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic && VoucherId == 0)
            {
                LoadVoucherNo();
            }
            else if (TransVoucherMethod == (int)TransactionVoucherMethod.Manual && VoucherId == 0)
            {
                txtVoucherNo.Text = string.Empty;
            }
        }

        /// <summary>
        /// set border color 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkpExpenseLedger_Leave(object sender, EventArgs e)
        {
            // this.SetBorderColorForGridLookUpEdit(glkpExpenseLedger);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiProject_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (AmcId == 0)
            {
                ShowProjectSelectionWindow();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="KeyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            try
            {
                if (KeyData.Equals(Keys.F5))
                {
                    ShowProjectSelectionWindow();
                }
                else if (KeyData.Equals(Keys.F3))
                {
                    dtAMCVoucherDate.Focus();
                }
                else if (KeyData == (Keys.Alt | Keys.D))
                {
                    DeleteTransaction();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        /// <summary>
        /// 
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
                    Project = projectSelection.ProjectName;
                    dtAMCVoucherDate.DateTime = this.UtilityMember.DateSet.ToDate(projectSelection.RecentVoucherDate, false);
                    Caption();
                }
            }
        }

        private string GetLedgerBalanceValues(DataTable dtTrans, int LedgerId)
        {
            string LedgerBalance = string.Empty;
            if (dtTrans != null)
            {
                BalanceProperty balance = FetchCurrentBalance(LedgerId);
                LedgerBalance = (balance.Amount - AmountSummaryVal).ToString();
            }
            return LedgerBalance;
        }

        private BalanceProperty FetchCurrentBalance(int LedgerId)
        {
            BalanceProperty balProperty;
            using (BalanceSystem balancesystem = new BalanceSystem())
            {
                balProperty = balancesystem.GetBalance(ProjectId, LedgerId, "", BalanceSystem.BalanceType.CurrentBalance);
            }
            return balProperty;
        }

        private void EnableCashBankFields()
        {
            int iLedgerId = 0;
            int Group = 0;
            gvBank.UpdateCurrentRow();
            DataTable dtTrans = gcBank.DataSource as DataTable;

            foreach (DataRow dr in dtTrans.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    iLedgerId = dr["LEDGER_ID"] != null ? this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString()) : 0;
                    using (LedgerSystem ledger = new LedgerSystem())
                    {
                        ledger.LedgerId = iLedgerId;
                        Group = ledger.FetchLedgerGroupById();
                        if (Group == (int)FixedLedgerGroup.BankAccounts)
                        {
                            VisibleCashBankAdditionalFields(true);
                            break;
                        }
                        else
                        {
                            VisibleCashBankAdditionalFields(false);
                        }
                    }
                }
            }
        }

        private void LoadCashBankLedger(RepositoryItemGridLookUpEdit glkpLedger)
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    ledgerSystem.ProjectId = ProjectId;
                    resultArgs = ledgerSystem.FetchCashBankLedger();
                    //glkpLedger.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        glkpLedger.ValueMember = "LEDGER_ID";
                        glkpLedger.DisplayMember = "LEDGER_NAME";
                        DataTable dtCashBankLedger = resultArgs.DataSource.Table; // FetchLedgerByDateClosed(resultArgs.DataSource.Table);
                        glkpLedger.DataSource = dtCashBankLedger; //resultArgs.DataSource.Table;
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_CASHBANK_MAPPING_TO_PROJECT));
                        // this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_CASHBANK_MAPPING_TO_PROJECT) + ProjectName + "");
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void CalculateFirstRowValue()
        {
            if (LedgerAmount >= 0 && VoucherId >= 0) //&& CashBankAmount != TransSummaryVal && VoucherId >= 0)
            {
                gvBank.MoveFirst();
                double Amount = gvBank.GetRowCellValue(gvBank.FocusedRowHandle, colCashBankAmount) != null ?
                    this.UtilityMember.NumberSet.ToDouble(gvBank.GetRowCellValue(gvBank.FocusedRowHandle, colCashBankAmount).ToString()) : 0;
                if (Amount >= 0)
                {
                    double dAmount = 0.0;
                    if (CashBankAmount <= TransSummaryVal)
                    {
                        dAmount = (TransSummaryVal - CashBankAmount) + Amount;
                    }
                    else if (CashBankAmount >= TransSummaryVal)
                    {
                        dAmount = Amount - (CashBankAmount - TransSummaryVal);
                    }

                    if (dAmount >= 0)
                    {
                        gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCashBankAmount, dAmount);
                        if (CashLedgerId == 0 && isCashLedgerExists())
                        {
                            gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCashLedgerId, 1);
                        }
                        gvBank.PostEditor();
                        gvBank.UpdateCurrentRow();
                        DataTable dtTemp = gcBank.DataSource as DataTable;
                        if (CashLedgerId > 0)
                        {
                            string Balance = GetLedgerBalanceValues(dtTemp, CashLedgerId); //ShowLedgerBalance(CashLedgerId, dtTemp, false);
                            if (Balance != string.Empty) { gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colLedgerBalance, Balance.ToString()); }
                        }
                    }
                }
            }
        }

        private bool isCashLedgerExists()
        {
            bool isValid = true;
            int LedId = 0;
            using (LedgerSystem ledger = new LedgerSystem())
            {
                LedId = ledger.IsCashLedgerExists();
                if (LedId != 1)
                {
                    isValid = false;
                }
            }
            return isValid;
        }

        private void VisibleCashBankAdditionalFields(bool Visible)
        {
            // colCashFlag.VisibleIndex = 0;
            colCashLedgerId.VisibleIndex = 1;
            colCashBankAmount.VisibleIndex = 2;
            colRefNo.VisibleIndex = 3;
            colMaterializedOn.VisibleIndex = 4;
            colLedgerBalance.VisibleIndex = 5;
            // colBudgetAmt.VisibleIndex = 6;

            if (Visible)
            {
                colRefNo.Visible = true;
                colMaterializedOn.Visible = true;
            }
            else
            {
                colRefNo.Visible = false;
                colMaterializedOn.Visible = false;
                rdtMaterializedOn.NullText = "";
                rtxtRefno.NullText = "";
            }
        }

        private void FocusCashTransactionGrid()
        {
            gcBank.Select();

            gvBank.MoveFirst();
            gvBank.FocusedColumn = colCashLedgerId;
            gvBank.ShowEditor();
        }
        #endregion

        private void rbtViewAssetItem_Click(object sender, EventArgs e)
        {
            int quantity = this.UtilityMember.NumberSet.ToInteger(gvAMcVoucher.GetFocusedRowCellValue(colQuantity).ToString());
            int ItemID = this.UtilityMember.NumberSet.ToInteger(gvAMcVoucher.GetFocusedRowCellValue(colItemId).ToString());
            if (quantity > 0)
            {
                frmAssetItemList AssetItemList = new frmAssetItemList(ItemID, quantity, gvAMcVoucher.FocusedRowHandle, Inoutid, AssetInOut.AMC, ProjectId);
                AssetItemList.ShowDialog();
                if (AssetItemList.Dialogresult == DialogResult.OK)
                {
                    gvAMcVoucher.SetFocusedRowCellValue(colQuantity, AssetItemList.Quantity);

                    decimal amount = AssetItemList.Amount;
                    gvAMcVoucher.SetFocusedRowCellValue(colAmount, amount.ToString());
                    gvAMcVoucher.FocusedColumn = colAmount;
                    if (ItemID > 0 && Quantity > 0)
                    {
                        if (gvAMcVoucher.IsLastRow)
                        {
                            gvAMcVoucher.AddNewRow();
                        }
                        else
                            gvAMcVoucher.MoveNext();

                        //gvBank.SetFocusedRowCellValue(colBankAmount, AmountSummaryVal);
                        //gvBank.SetFocusedRowCellValue(colCashBank, 1);
                        //if (CashLedgerId > 0)
                        //{
                        //    DataTable dtCashTrans = gcBank.DataSource as DataTable;
                        //    string Balance = GetLedgerBalanceValues(dtCashTrans, CashLedgerId); //ShowLedgerBalance(CashLedgerId, dtCashTrans, false);
                        //    if (Balance != string.Empty)
                        //    {
                        //        gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colLedgerBalance, Balance);
                        //    }
                        //}
                        gvAMcVoucher.UpdateCurrentRow();
                        gvAMcVoucher.FocusedColumn = colItemId;
                        gvAMcVoucher.ShowEditor();
                    }
                    //gvCashBank.SetFocusedRowCellValue(colBankAmount, AmountSummaryVal.ToString());
                    //rglkpCashBankLedgers.(0);
                }
            }
            else
            {
                gvAMcVoucher.FocusedColumn = colItemId;
            }
        }

        private void rtxtAmount_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                decimal amount = this.UtilityMember.NumberSet.ToDecimal(gvAMcVoucher.GetFocusedRowCellValue(colAmount).ToString());
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }
    }
}
