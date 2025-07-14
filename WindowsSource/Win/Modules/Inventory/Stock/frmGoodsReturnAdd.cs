using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Model.Inventory;
using Bosco.Model;
using Bosco.Utility;
using Bosco.Model.UIModel;
using Bosco.Model.Transaction;
using Bosco.Model.Inventory.Stock;
using ACPP.Modules.Transaction;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid;

namespace ACPP.Modules.Inventory.Stock
{
    public partial class frmGoodsReturnAdd : frmFinanceBaseAdd
    {
        #region Declarion

        ResultArgs resultArgs;
        private const string ITEM_NAME = "ITEM_NAME";
        private const string LOCATION_NAME = "LOCATION_NAME";
        private const string ITEM_ID = "ITEM_ID";
        private const string LOCATION_ID = "LOCATION_ID";
        private const string VENDOR_ID = "VENDOR_ID";
        private const string UNIT_PRICE = "UNIT_PRICE";
        private const string QUANTITY = "QUANTITY";
        private const string TOTAL_AMOUNT = "TOTAL_AMOUNT";
        private const string AVAIL_QUANTITY = "AVAIL_QUANTITY";
        private decimal TotalAmount = 0;
        int TotalQuantity = 0;
        DataTable dtItemLocation;
        DataView dvItems;
        DataTable dtLocation; // All the location will be in this datatable.
        DataView dvLocation;
        DataTable dtLocationByItem; // Filter location from dtlocation by selected ItemId
        DataTable dtReturn = new DataTable();

        #endregion

        #region Event Declaration
        private int SelectedItemId { get; set; }
        private int SelectedLocationId { get; set; }
        public bool isMouseClicked { get; set; }
        public int AccountledgerId { get; set; }
        public event EventHandler UpdateHeld;
        #endregion

        #region Properties

        public int VoucherId { get; set; }
        int returnId = 0;
        int ReturnId
        {
            set { returnId = value; }
            get { return returnId; }
        }
        private int projectId = 0;
        private int ProjectId
        {
            set { projectId = value; }
            get { return projectId; }
        }
        private string projectName = string.Empty;
        private string ProjectName
        {
            set { projectName = value; }
            get { return projectName; }
        }
        private DateTime returnDate;
        private DateTime ReturnDate
        {
            set { returnDate = value; }
            get { return returnDate; }
        }
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
        private decimal UnitPrice { get; set; }
        private double ReturnseSummaryVal
        {
            get { return colAmount.SummaryItem.SummaryValue != null ? this.UtilityMember.NumberSet.ToDouble(colAmount.SummaryItem.SummaryValue.ToString()) : 0; }
        }
        public int LocationId { get { return gvItems.GetFocusedRowCellValue(colLocation) != null ? this.UtilityMember.NumberSet.ToInteger(gvItems.GetFocusedRowCellValue(colLocation).ToString()) : 0; } }
        public int ItemId { get { return gvItems.GetFocusedRowCellValue(colItem) != null ? this.UtilityMember.NumberSet.ToInteger(gvItems.GetFocusedRowCellValue(colItem).ToString()) : 0; } }

        #endregion

        #region Constractor

        public frmGoodsReturnAdd()
        {
            InitializeComponent();
        }

        public frmGoodsReturnAdd(int returnid, int projectid, string projectname, DateTime returndate)
            : this()
        {
            ReturnId = returnid;
            ProjectId = projectid;
            ProjectName = projectname;
            ReturnDate = returndate;
            RealColumnEditTransAmount();
            RealColumnEditLocation();
            RealColumnEditQuantity();
        }


        #endregion

        #region Events

        private void frmGoodsReturnAdd_Load(object sender, EventArgs e)
        {
            LoadDefaults();
            ConstructTable();
            LoadItems();
            LoadLocation();
            LoadVendor();
            LoadVoucherNo();
            LoadCashBankLedger();
            AssignValues();
            if (dtLocation != null && dtLocation.Rows.Count > 0)
            {
                //dvLocation = dtLocation.DefaultView;
                //dvLocation.RowFilter = "ITEM_ID=" + ItemId;
                //dtLocationByItem = dvLocation.ToTable();
                this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpLocation, dtLocation, LOCATION_NAME, LOCATION_ID);
            }
            dtGoodReturnDate.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtGoodReturnDate.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGoodReturns_Click(object sender, EventArgs e)
        {
            SavePurchaseReturns(e);
        }

        private void gcItems_ProcessGridKey(object sender, KeyEventArgs e)
        {
            int ItemId = 0;
            int LocationId = 0;
            int VendorId = 0;
            int Quantity = 0;
            decimal Amount = 0;
            decimal Price = 0;
            int AvaliQuantity = 0;
            try
            {
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control)
                {
                    gvItems.PostEditor();
                    gvItems.UpdateCurrentRow();

                    if (gvItems.FocusedColumn == colItem)
                    {
                        SelectedItemId = gvItems.GetFocusedRowCellValue(colItem) != null ? this.UtilityMember.NumberSet.ToInteger(gvItems.GetFocusedRowCellValue(colItem).ToString()) : 0;
                        if (SelectedItemId > 0)
                        {
                            if (dtLocation != null && dtLocation.Rows.Count > 0)
                            {
                                //dvLocation = dtLocation.DefaultView;
                                //dvLocation.RowFilter = "ITEM_ID=" + SelectedItemId + "AND AVAIL_QUANTITY>0";
                                //dtLocationByItem = dvLocation.ToTable();
                                this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpLocation, dtLocation, LOCATION_NAME, LOCATION_ID);
                            }
                        }
                        if (string.IsNullOrEmpty(gvItems.GetFocusedRowCellValue(colItem).ToString()))
                        {
                            gvItems.CloseEditor();
                            e.SuppressKeyPress = true;
                            e.Handled = true;
                            glkpLedger.Select();
                            glkpLedger.Focus();
                        }
                    }
                    else
                    {
                        if (gvItems.FocusedColumn == colItemLocation)
                        {
                            SelectedItemId = ItemId = gvItems.GetFocusedRowCellValue(colItem) != null ? this.UtilityMember.NumberSet.ToInteger(gvItems.GetFocusedRowCellValue(colItem).ToString()) : 0;
                            SelectedLocationId = LocationId = gvItems.GetFocusedRowCellValue(colLocation) != null ? this.UtilityMember.NumberSet.ToInteger(gvItems.GetFocusedRowCellValue(colLocation).ToString()) : 0;
                            if (ItemId > 0 && LocationId > 0)
                            {
                                LoadVendor();
                                //    dvVendor.RowFilter = "ITEM_ID=" + SelectedItemId + "";
                                //    DataTable dtVendor = dvVendor.Table;
                                //    this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpVendor, dtVendor,
                                //vendorSystem.AppSchema.Vendors.NAMEColumn.ColumnName,
                                //vendorSystem.AppSchema.Vendors.IDColumn.ColumnName);
                                //    gvItems.SetRowCellValue(gvItems.FocusedRowHandle, colVendor, 1);
                                //    gcItems.RefreshDataSource();
                                int TotalQuantity = GetAvailableQuantity(gcItems.DataSource as DataTable, ItemId, LocationId);
                                gvItems.SetRowCellValue(gvItems.FocusedRowHandle, colStockQuantity, TotalQuantity);
                                GetLedgerIdByName(ItemId);
                                gvItems.SetRowCellValue(gvItems.FocusedRowHandle, colAccountLedgerId, AccountledgerId);
                                double TempRate = gvItems.GetFocusedRowCellValue(colPrice) != null ? this.UtilityMember.NumberSet.ToInteger(gvItems.GetFocusedRowCellValue(colPrice).ToString()) : 0;
                                if (TempRate == 0)
                                {
                                    gvItems.SetRowCellValue(gvItems.FocusedRowHandle, colPrice, UnitPrice);
                                }
                                else
                                {
                                    gvItems.SetRowCellValue(gvItems.FocusedRowHandle, colPrice, TempRate);
                                }
                            }
                        }
                        if (gvItems.FocusedColumn == colQuantity)
                        {
                            bool isTrue = true;
                            ItemId = gvItems.GetFocusedRowCellValue(colItem) != null ? this.UtilityMember.NumberSet.ToInteger(gvItems.GetFocusedRowCellValue(colItem).ToString()) : 0;
                            LocationId = gvItems.GetFocusedRowCellValue(colLocation) != null ? this.UtilityMember.NumberSet.ToInteger(gvItems.GetFocusedRowCellValue(colLocation).ToString()) : 0;
                            Quantity = gvItems.GetRowCellValue(gvItems.FocusedRowHandle, colQuantity) != null ? this.UtilityMember.NumberSet.ToInteger(gvItems.GetRowCellValue(gvItems.FocusedRowHandle, colQuantity).ToString()) : 0;

                            if (!isTrue)
                            {
                                int AvailQuantity = GetAvailableQuantity(gcItems.DataSource as DataTable, ItemId, LocationId);
                                gvItems.SetRowCellValue(gvItems.FocusedRowHandle, colStockQuantity, AvailQuantity);
                            }
                        }
                        if (gvItems.FocusedColumn == colPrice && gvItems.IsLastRow)
                        {
                            ItemId = gvItems.GetFocusedRowCellValue(colItem) != null ? this.UtilityMember.NumberSet.ToInteger(gvItems.GetFocusedRowCellValue(colItem).ToString()) : 0;
                            LocationId = gvItems.GetFocusedRowCellValue(colLocation) != null ? this.UtilityMember.NumberSet.ToInteger(gvItems.GetFocusedRowCellValue(colLocation).ToString()) : 0;
                            VendorId = gvItems.GetFocusedRowCellValue(colVendor) != null ? this.UtilityMember.NumberSet.ToInteger(gvItems.GetFocusedRowCellValue(colVendor).ToString()) : 0;
                            Quantity = gvItems.GetFocusedRowCellValue(colQuantity) != null ? this.UtilityMember.NumberSet.ToInteger(gvItems.GetFocusedRowCellValue(colQuantity).ToString()) : 0;
                            Amount = gvItems.GetFocusedRowCellValue(colAmount) != null ? this.UtilityMember.NumberSet.ToDecimal(gvItems.GetFocusedRowCellValue(colAmount).ToString()) : 0;
                            decimal UnitPrice = gvItems.GetRowCellValue(gvItems.FocusedRowHandle, colPrice) != null ? this.UtilityMember.NumberSet.ToDecimal(gvItems.GetRowCellValue(gvItems.FocusedRowHandle, colPrice).ToString()) : 0;
                            Quantity = gvItems.GetRowCellValue(gvItems.FocusedRowHandle, colQuantity) != null ? this.UtilityMember.NumberSet.ToInteger(gvItems.GetRowCellValue(gvItems.FocusedRowHandle, colQuantity).ToString()) : 0;
                            Amount = (UnitPrice * Quantity);
                            if (ItemId > 0 && LocationId > 0 && VendorId > 0 && Quantity > 0 && UnitPrice > 0 && Amount > 0)
                            {
                                gvItems.SetFocusedRowCellValue(colAmount, Amount);
                                gvItems.AddNewRow();
                                gvItems.ShowEditor();
                                gvItems.FocusedColumn = colDelete;
                            }
                            else
                            {
                                gvItems.CloseEditor();
                                e.SuppressKeyPress = true;
                                e.Handled = true;
                                glkpLedger.Select();
                                glkpLedger.Focus();
                            }
                        }
                        //if ((gvItems.FocusedColumn == colAmount || gvItems.FocusedColumn == colDelete))
                        //{
                        //    ItemId = gvItems.GetFocusedRowCellValue(colItem) != null ? this.UtilityMember.NumberSet.ToInteger(gvItems.GetFocusedRowCellValue(colItem).ToString()) : 0;
                        //    LocationId = gvItems.GetFocusedRowCellValue(colLocation) != null ? this.UtilityMember.NumberSet.ToInteger(gvItems.GetFocusedRowCellValue(colLocation).ToString()) : 0;
                        //    VendorId = gvItems.GetFocusedRowCellValue(colVendor) != null ? this.UtilityMember.NumberSet.ToInteger(gvItems.GetFocusedRowCellValue(colVendor).ToString()) : 0;
                        //    Quantity = gvItems.GetFocusedRowCellValue(colQuantity) != null ? this.UtilityMember.NumberSet.ToInteger(gvItems.GetFocusedRowCellValue(colQuantity).ToString()) : 0;
                        //    Price = gvItems.GetFocusedRowCellValue(colPrice) != null ? this.UtilityMember.NumberSet.ToDecimal(gvItems.GetFocusedRowCellValue(colPrice).ToString()) : 0;
                        //    Amount = gvItems.GetFocusedRowCellValue(colAmount) != null ? this.UtilityMember.NumberSet.ToDecimal(gvItems.GetFocusedRowCellValue(colAmount).ToString()) : 0;
                        //    AvaliQuantity = gvItems.GetFocusedRowCellValue(colStockQuantity) != null ? this.UtilityMember.NumberSet.ToInteger(gvItems.GetFocusedRowCellValue(colStockQuantity).ToString()) : 0;
                        //    if (ItemId > 0 && LocationId > 0 && VendorId > 0 && Quantity > 0 && Price > 0 && Amount > 0)
                        //    {
                        //        gvItems.AddNewRow();
                        //        gvItems.ShowEditor();
                        //        gvItems.FocusedColumn = colDelete;
                        //    }
                        //    else
                        //    {
                        //        gvItems.CloseEditor();
                        //        e.SuppressKeyPress = true;
                        //        e.Handled = true;
                        //        glkpLedger.Select();
                        //        glkpLedger.Focus();
                        //    }
                        //}
                    }
                }
                else if (gvItems.IsFirstRow && gvItems.FocusedColumn == colItem && e.Shift && e.KeyCode == Keys.Tab)
                {
                    txtVoucherNo.Focus();
                    txtVoucherNo.Select();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBoxError(ex.Message);
            }
        }

        private void rglkpItem_MouseDown(object sender, MouseEventArgs e)
        {
            //To Identify the Mouse Click Event
            if (e.Button == MouseButtons.Left)
                isMouseClicked = true;
        }

        private void rglkpItem_EditValueChanged(object sender, EventArgs e)
        {
            //To select the Ledger Using Mouse Click
            if (isMouseClicked)
            {
                SendKeys.Send("{tab}"); isMouseClicked = false;
                //GetLedgerIdByName(ItemId);
            }
        }

        private void rbtnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)
                {
                    //if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Information).Equals(DialogResult.Yes))
                    //{
                    //    gvItems.DeleteRow(gvItems.FocusedRowHandle);
                    //    if (gvItems.RowCount == 0)
                    //        ConstructTable();
                    //}
                    DeleteTransaction();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }

        }

        private void dtGoodReturnDate_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForDateTimeEdit(dtGoodReturnDate);
        }

        private void txtVoucherNo_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtVoucherNo);
        }

        private void glkpLedger_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpLedger);
        }

        private void gcItems_Enter(object sender, EventArgs e)
        {
            gvItems.OptionsSelection.EnableAppearanceFocusedCell = true;
        }

        private void bbiDeleteRow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteTransaction();
        }

        private void gcItems_Leave(object sender, EventArgs e)
        {
            gvItems.OptionsSelection.EnableAppearanceFocusedCell = false;
            txtAmount.Text = ReturnseSummaryVal.ToString();
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtAmount);
        }

        private void dtGoodReturnDate_EditValueChanged(object sender, EventArgs e)
        {
            TransVoucherMethod = (int)TransactionVoucherMethod.Automatic;
            if (TransVoucherMethod == 1) { txtVoucherNo.Enabled = false; } else { txtVoucherNo.Enabled = true; }
            if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic && VoucherId == 0)
            {
                LoadVoucherNo();
            }
            else if (TransVoucherMethod == (int)TransactionVoucherMethod.Manual && VoucherId == 0)
            {
                txtVoucherNo.Text = string.Empty;
            }
        }

        private void frmGoodsReturnAdd_KeyDown(object sender, KeyEventArgs e)
        {
            ProcessShortcutKeys(e);
        }

        private void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SavePurchaseReturns(e);
        }

        private void bbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void glkpLedger_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                ACPP.Modules.Master.frmLedgerDetailAdd frmBank = new ACPP.Modules.Master.frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.BK, ProjectId);
                frmBank.ShowDialog();
                LoadCashBankLedger();
            }
        }
        #endregion

        #region Methods

        private void LoadDefaults()
        {
            uccpProject.Caption = ProjectName;
            dtGoodReturnDate.DateTime = ReturnDate;
            dtItemLocation = ConstructItemLocationTable();
            if (ReturnId == 0)
                this.Text = this.GetMessage(MessageCatalog.Stock.StockPurcahseReturns.STOCK_PURCHASE_RETURN_ADD);
            else
                this.Text = this.GetMessage(MessageCatalog.Stock.StockPurcahseReturns.STOCK_PURCHASE_RETURN_EDIT);
        }

        private void ConstructTable()
        {
            try
            {
                DataTable dtItemDetails = new DataTable();
                dtItemDetails.Columns.Add("ITEM_ID", typeof(int));
                dtItemDetails.Columns.Add("LOCATION_ID", typeof(int));
                dtItemDetails.Columns.Add("VENDOR_ID", typeof(int));
                dtItemDetails.Columns.Add("QUANTITY", typeof(int));
                dtItemDetails.Columns.Add("UNIT_PRICE", typeof(decimal));
                dtItemDetails.Columns.Add("TOTAL_AMOUNT", typeof(decimal));
                dtItemDetails.Columns.Add("AVAIL_QUANTITY", typeof(int));
                dtItemDetails.Columns.Add("TEMP_QUANTITY", typeof(int));
                dtItemDetails.Columns.Add("ACCOUNT_LEDGER_ID", typeof(Int32));
                gcItems.DataSource = dtItemDetails;
                gvItems.AddNewRow();
            }
            catch (Exception ex)
            {
                this.ShowMessageBoxError(ex.Message);
            }
        }
        private void LoadItems()
        {
            try
            {
                using (StockPurchaseSalesSystem stockPurchaseSalseSystem = new StockPurchaseSalesSystem())
                {
                    resultArgs = stockPurchaseSalseSystem.FetchStockItemDetails();
                    dvItems = resultArgs.DataSource.Table.DefaultView;
                    rglkpItem.DataSource = null;
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpItem, resultArgs.DataSource.Table, ITEM_NAME,
                            stockPurchaseSalseSystem.AppSchema.StockItem.ITEM_IDColumn.ColumnName);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
        }

        /// <summary>
        /// Need to filter the Location if not available for concern Projects (Chinna)
        /// </summary>
        private void LoadLocation()
        {
            try
            {
                using (LocationSystem locationSystem = new LocationSystem())
                {
                    resultArgs = locationSystem.FetchLocationDetails();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpLocation, resultArgs.DataSource.Table, locationSystem.AppSchema.ASSETLocationDetails.LOCATIONColumn.ColumnName, locationSystem.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName);
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }


        //private void LoadLocation()
        //{
        //    try
        //    {
        //        using (StockPurchaseSalesSystem stockPurchaseSalseSystem = new StockPurchaseSalesSystem())
        //        {
        //            stockPurchaseSalseSystem.ItemId = SelectedItemId;
        //            resultArgs = stockPurchaseSalseSystem.FetchStockItemLocationDetails();
        //            rglkpLocation.DataSource = null;
        //            if (resultArgs.Success && resultArgs.RowsAffected > 0)
        //            {
        //                dtLocation = resultArgs.DataSource.Table;
        //                this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpLocation, resultArgs.DataSource.Table, "LOCATION",
        //                "LOCATION_ID");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source);
        //    }
        //}

        private void LoadCashBankLedger()
        {

            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    ledgerSystem.ProjectId = ProjectId;
                    resultArgs = ledgerSystem.FetchCashBankLedger();
                    glkpLedger.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpLedger, resultArgs.DataSource.Table,
                            ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName,
                            ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                    }

                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void LoadVoucherNo()
        {
            string vType = string.Empty;
            string pId = string.Empty;
            using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
            {
                voucherTransaction.VoucherType = VoucherSubTypes.RC.ToString();
                voucherTransaction.ProjectId = ProjectId;
                voucherTransaction.VoucherDate = this.UtilityMember.DateSet.ToDate(dtGoodReturnDate.Text, false);
                txtVoucherNo.Text = voucherTransaction.TempVoucherNo();
            }
        }

        private void LoadVendor()
        {
            try
            {
                using (VendorInfoSystem vendorSystem = new VendorInfoSystem())
                {
                    vendorSystem.ItemId = SelectedItemId;
                    vendorSystem.LocationId = SelectedLocationId;
                    resultArgs = vendorSystem.FetchDetailsByItemId();
                    rglkpVendor.DataSource = null;
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpVendor, resultArgs.DataSource.Table,
                            vendorSystem.AppSchema.Vendors.VENDORColumn.ColumnName,
                            vendorSystem.AppSchema.Vendors.IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void AssignValues()
        {
            try
            {
                if (ReturnId > 0)
                {
                    using (StockPurcahseReturnSystem PurchaseReturnSystem = new StockPurcahseReturnSystem())
                    {
                        PurchaseReturnSystem.ProjectId = ProjectId;
                        PurchaseReturnSystem.ReturnId = ReturnId;
                        DataSet dsPurchaseReturn = PurchaseReturnSystem.FetchPurchaseReturnById();
                        if (dsPurchaseReturn.Tables.Count > 0)
                        {
                            if (dsPurchaseReturn.Tables[0].Rows.Count > 0)
                            {
                                dtGoodReturnDate.DateTime = this.UtilityMember.DateSet.ToDate(dsPurchaseReturn.Tables[0].Rows[0][PurchaseReturnSystem.AppSchema.StockMasterPurchaseReturns.RETURN_DATEColumn.ColumnName].ToString(), false);
                                txtAmount.Text = dsPurchaseReturn.Tables[0].Rows[0][PurchaseReturnSystem.AppSchema.StockMasterPurchaseReturns.NET_PAYColumn.ColumnName].ToString();
                                glkpLedger.EditValue = this.UtilityMember.NumberSet.ToInteger(dsPurchaseReturn.Tables[0].Rows[0][PurchaseReturnSystem.AppSchema.StockMasterPurchaseReturns.LEDGER_IDColumn.ColumnName].ToString());
                                moeReasons.Text = dsPurchaseReturn.Tables[0].Rows[0][PurchaseReturnSystem.AppSchema.StockMasterPurchaseReturns.REASONColumn.ColumnName].ToString();
                                VoucherId = this.UtilityMember.NumberSet.ToInteger(dsPurchaseReturn.Tables[0].Rows[0][PurchaseReturnSystem.AppSchema.VoucherTransaction.VOUCHER_IDColumn.ColumnName].ToString());
                                txtVoucherNo.Text = VoucherId.ToString();
                                gcItems.DataSource = null;
                                if (dsPurchaseReturn.Tables[1].Rows.Count > 0)
                                {
                                    gcItems.DataSource = dsPurchaseReturn.Tables[1];
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
        }

        private void SavePurchaseReturns(EventArgs e)
        {
            try
            {
                using (StockPurcahseReturnSystem PurchaseReturnSystem = new StockPurcahseReturnSystem())
                {
                    DataTable items = gcItems.DataSource as DataTable;
                    if (ValidateRows(items))
                    {
                        PurchaseReturnSystem.ProjectId = ProjectId;
                        PurchaseReturnSystem.ReturnId = ReturnId;
                        PurchaseReturnSystem.ReturnDate = dtGoodReturnDate.DateTime;
                        PurchaseReturnSystem.ReturnType = 1;
                        PurchaseReturnSystem.LedgerId = this.UtilityMember.NumberSet.ToInteger(glkpLedger.EditValue.ToString());
                        PurchaseReturnSystem.Reason = moeReasons.Text;
                        PurchaseReturnSystem.NetPay = this.UtilityMember.NumberSet.ToDecimal(txtAmount.Text);
                        PurchaseReturnSystem.VoucherId = VoucherId;
                        PurchaseReturnSystem.dtItems = ((DataTable)gcItems.DataSource).Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull)).CopyToDataTable();
                        resultArgs = PurchaseReturnSystem.SavePurchaseReturns();
                        if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                            if (this.ReturnId.Equals(0))
                            {
                                ClearControls();
                            }
                            else
                            {
                                this.Close();
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        public void ClearControls()
        {
            txtAmount.Text = string.Empty;
            glkpLedger.EditValue = 0;
            moeReasons.Text = string.Empty;
            gcItems.DataSource = null;
            ConstructTable();
            dtGoodReturnDate.Focus();
            gvItems.FocusedColumn = colItem;
            LoadVoucherNo();
        }

        public bool ValidateRows(DataTable Items)
        {
            bool Success = true;
            try
            {
                Items = RemoveEmptyRows(Items.Copy());
                if (!IsValidTransactionDate())
                {
                    dtGoodReturnDate.DateTime = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom.ToString(), false);
                    Success = false;
                }
                else if (Items.Rows.Count > 0)
                {
                    foreach (DataRow drItem in Items.Rows)
                    {

                        if (this.UtilityMember.NumberSet.ToInteger(drItem[ITEM_ID].ToString()) != 0)
                        {
                            if (this.UtilityMember.NumberSet.ToInteger(drItem[ITEM_ID].ToString()) == 0)
                            {
                                this.ShowMessageBox(GetMessage(MessageCatalog.Stock.StockPurcahseReturns.STOCK_ITEM_EMPTY));
                                Success = false;
                                gvItems.Focus();
                                gcItems.Select();
                                gvItems.FocusedColumn = colItem;
                            }
                            else if (UtilityMember.NumberSet.ToInteger(drItem[LOCATION_ID].ToString()) == 0)
                            {
                                this.ShowMessageBox(GetMessage(MessageCatalog.Stock.StockPurcahseReturns.STOCK_LOCATION_EMPTY));
                                Success = false;
                                gvItems.Focus();
                                gcItems.Select();
                                gvItems.FocusedColumn = colLocation;
                            }
                            else if (UtilityMember.NumberSet.ToInteger(drItem[VENDOR_ID].ToString()) == 0)
                            {
                                this.ShowMessageBox(GetMessage(MessageCatalog.Stock.StockPurcahseReturns.STOCK_VENDOR_EMPTY));
                                Success = false;
                                gvItems.Focus();
                                gcItems.Select();
                                gvItems.FocusedColumn = colVendor;
                            }
                            else if (UtilityMember.NumberSet.ToDecimal(drItem[UNIT_PRICE].ToString()) == 0)
                            {
                                this.ShowMessageBox(GetMessage(MessageCatalog.Stock.StockPurcahseReturns.STOCK_UNIT_PRICE_EMPTY));
                                Success = false;
                                gvItems.Focus();
                                gcItems.Select();
                                gvItems.FocusedColumn = colPrice;
                            }
                            else if (UtilityMember.NumberSet.ToInteger(drItem[QUANTITY].ToString()) == 0)
                            {
                                this.ShowMessageBox(GetMessage(MessageCatalog.Stock.StockPurcahseReturns.STOCK_QUANTITY_EMPTY));
                                Success = false;
                                gvItems.Focus();
                                gcItems.Select();
                                gvItems.FocusedColumn = colQuantity;
                            }
                            else if (UtilityMember.NumberSet.ToDecimal(drItem[TOTAL_AMOUNT].ToString()) == 0)
                            {
                                this.ShowMessageBox(GetMessage(MessageCatalog.Stock.StockPurcahseReturns.STOCK_AMOUNT_EMPTY));
                                Success = false;
                            }
                            //chinna 
                            //else if (string.IsNullOrEmpty(txtVoucherNo.Text))
                            //{
                            //    this.ShowMessageBox(GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_NUMBER_EMPTY));
                            //    Success = false;
                            //    txtVoucherNo.Focus();
                            //}
                            else if (UtilityMember.NumberSet.ToInteger(glkpLedger.EditValue.ToString()) == 0)
                            {
                                this.ShowMessageBox(GetMessage(MessageCatalog.Stock.StockPurcahseReturns.STOCK_LEDGER_EMPTY));
                                Success = false;
                                glkpLedger.Focus();
                            }
                            else if (string.IsNullOrEmpty(txtAmount.Text))
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Stock.StockPurcahseReturns.STOCK_TOTAL_AMOUTN_EMPTY));
                                Success = false;
                                txtAmount.Focus();
                            }
                            else if (UtilityMember.NumberSet.ToInteger(drItem[AVAIL_QUANTITY].ToString()) < 0 || UtilityMember.NumberSet.ToInteger(drItem[AVAIL_QUANTITY].ToString()) < 0)
                            {
                                if (ReturnId == 0)
                                {
                                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Stock.StockPurcahseReturns.STOCK_QUANTITY_EXCEEDS));
                                    Success = false;
                                }
                            }
                        }
                        else
                        {
                            this.ShowMessageBox(GetMessage(MessageCatalog.Stock.StockPurcahseReturns.STOCK_NO_ITEM_ROWS));
                            Success = false;
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(GetMessage(MessageCatalog.Stock.StockPurcahseReturns.STOCK_NO_ITEM_ROWS));
                    dtGoodReturnDate.Focus();
                    Success = false;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
                Success = false;
            }
            return Success;
        }

        private bool IsValidTransactionDate()
        {
            bool isValid = true;
            DateTime dtProjectFrom;
            DateTime dtProjectTo;
            DateTime dtyearfrom;
            DateTime dtbookbeginfrom;
            DateTime dtYearTo;
            DateTime dtRecentVoucherDate;
            dtyearfrom = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtbookbeginfrom = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false);
            dtYearTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            dtRecentVoucherDate = UtilityMember.DateSet.ToDate(ReturnDate.ToString(), false);

            ResultArgs result = FetchProjectDetails();

            DataView dvResult = result.DataSource.Table.DefaultView;
            dvResult.RowFilter = "PROJECT_ID=" + ProjectId;
            DataTable dtResult = dvResult.ToTable();
            dvResult.RowFilter = "";
            if (dtResult.Rows.Count > 0)
            {
                DataRow drProject = dtResult.Rows[0];

                string sProjectFrom = drProject["DATE_STARTED"].ToString();
                string sProjectTo = drProject["DATE_CLOSED"].ToString();

                dtProjectFrom = (!string.IsNullOrEmpty(sProjectFrom)) ? this.UtilityMember.DateSet.ToDate(sProjectFrom, false) : dtyearfrom;

                if (!string.IsNullOrEmpty(sProjectTo))
                {
                    dtProjectTo = this.UtilityMember.DateSet.ToDate(sProjectTo, false);
                }
                else
                {
                    dtProjectTo = dtProjectFrom > dtYearTo ? dtProjectFrom : dtYearTo;
                }

                if ((dtProjectFrom < dtyearfrom && dtProjectTo < dtyearfrom) || (dtProjectFrom > dtYearTo && dtProjectTo > dtYearTo))
                {
                    //this.ShowMessageBox(this.GetMessage(MessageCatalog.Stock.StockMasterPurchase.VALIDATE_ACCOUNTING_YEAR));
                    //this.ShowMessageBoxWarning("Project start date and closed date does not fall between transaction period." + Environment.NewLine + "Kindly change the Project start date and closed date and try again.");
                    this.ShowMessageBoxWarning(this.GetMessage(MessageCatalog.Stock.StockMasterPurchase.PROJECT_START_DATE_CLOSED_DATE_FALL_BETWEEN_TRANS_PEROID) + Environment.NewLine + this.GetMessage(MessageCatalog.Stock.StockMasterPurchase.CHANGE_PROJECT_START_DATE_CLOSED_DATE));
                    isValid = false;
                    //this.Close();
                }
            }
            return isValid;
        }

        private ResultArgs FetchProjectDetails()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    resultArgs = mappingSystem.FetchProjectsLookup();
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
            return resultArgs;
        }

        private int FetchAvailableStock(int LocationId, int ItemId)
        {
            int AvaliableStock = 0;
            try
            {
                using (StockBalanceSystem BalanceSystem = new StockBalanceSystem())
                {
                    resultArgs = BalanceSystem.GetCurrentBalance(ProjectId, ItemId, LocationId);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        AvaliableStock = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][BalanceSystem.AppSchema.StockPurchaseDetails.QUANTITYColumn.ColumnName].ToString());
                        UnitPrice = this.UtilityMember.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][BalanceSystem.AppSchema.StockItem.RATEColumn.ColumnName].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
            return AvaliableStock;
        }

        private DataTable RemoveEmptyRows(DataTable table)
        {
            try
            {
                for (int i = 0; i <= table.Rows.Count - 1; i++)
                {
                    string valuesarr = string.Empty;
                    List<object> lst = new List<object>(table.Rows[i].ItemArray);
                    foreach (object s in lst)
                    {
                        valuesarr += s.ToString();
                    }
                    if (string.IsNullOrEmpty(valuesarr))
                    {
                        //Remove row here, this row do not have any value 
                        table.Rows[i].Delete();
                        valuesarr = string.Empty;
                    }
                }
                table.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source);
            }
            return table;
        }

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
                    ReturnDate = this.UtilityMember.DateSet.ToDate(projectSelection.RecentVoucherDate, false);
                    LoadDefaults();
                }
            }
        }

        private void ProcessShortcutKeys(KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F3)
                {
                    dtGoodReturnDate.Focus();
                }

                if (e.KeyCode == Keys.F5)
                {
                    if (ReturnId == 0)
                    {
                        ShowProjectSelectionWindow();
                    }
                }
                if (e.KeyData == (Keys.Alt | Keys.D))
                {
                    DeleteTransaction();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        private DataTable ConstructItemLocationTable()
        {
            DataTable dtItemLocation = new DataTable();
            try
            {
                dtItemLocation.Columns.Add(ITEM_ID, typeof(int));
                dtItemLocation.Columns.Add(LOCATION_ID, typeof(int));
                dtItemLocation.Columns.Add(AVAIL_QUANTITY, typeof(int));
                dtItemLocation.Columns.Add(UNIT_PRICE, typeof(decimal));
                dtItemLocation.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
            return dtItemLocation;
        }

        private void RealColumnEditTransAmount()
        {
            colPrice.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditTransAmount_EditValueChanged);
            this.gvItems.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvItems.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colPrice)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvItems.ShowEditorByMouse();
                    }));
                }
            };
        }

        void RealColumnEditTransAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            int quantity = 0;
            double rate = 0.00;
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvItems.PostEditor();
            gvItems.UpdateCurrentRow();
            if (gvItems.ActiveEditor == null)
            {
                gvItems.ShowEditor();
            }

            if (gvItems.GetFocusedRowCellValue(colStockQuantity) != null && gvItems.GetFocusedRowCellValue(colQuantity) != null)
            {
                quantity = GetCalculatedQuantity(LocationId, ItemId, gcItems.DataSource as DataTable);
                int AvailableQuantity = FetchAvailableStock(LocationId, ItemId);
                if (quantity > AvailableQuantity)
                {
                    ShowMessageBox(GetMessage(MessageCatalog.Stock.StokItemTransfer.STOCK_QUANTITY_EXCEEDS));
                    gvItems.SetFocusedRowCellValue(colQuantity, 0);
                    gvItems.FocusedColumn = colQuantity;
                }
                int Quantity = GetAvailableQuantity(gcItems.DataSource as DataTable, ItemId, LocationId);
                gvItems.SetFocusedRowCellValue(colStockQuantity, Quantity);

            }

            TextEdit txtTransAmount = edit as TextEdit;
            int grpCounts = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.DecimalPlaces) + 1);
            if (txtTransAmount.Text.Length > grpCounts && txtTransAmount.SelectionLength == txtTransAmount.Text.Length)
                txtTransAmount.Select(txtTransAmount.Text.Length - grpCounts, 0);
            if (LocationId > 0 && ItemId > 0)
            {
                rate = this.UtilityMember.NumberSet.ToDouble(gvItems.GetFocusedRowCellValue(colPrice).ToString());
                DataTable dtTrans = gcItems.DataSource as DataTable;
                int AvailQuantity = GetAvailableQuantity(dtTrans, ItemId, LocationId); //ShowLedgerBalance(LedgerId, dtTrans, true);
                if (AvailQuantity >= 0) { gvItems.SetRowCellValue(gvItems.FocusedRowHandle, colStockQuantity, AvailQuantity); }
                gvItems.SetFocusedRowCellValue(colAmount, (quantity * rate).ToString());
                txtAmount.Text = ReturnseSummaryVal.ToString();
            }
        }

        private void RealColumnEditLocation()
        {
            colItemLocation.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditLocation_EditValueChanged);
            this.gvItems.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvItems.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colItemLocation)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvItems.ShowEditorByMouse();
                    }));
                }
            };
        }

        void RealColumnEditLocation_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvItems.PostEditor();
            gvItems.UpdateCurrentRow();
            if (gvItems.ActiveEditor == null)
            {
                gvItems.ShowEditor();
            }

            if (gvItems.GetFocusedRowCellValue(colItemLocation) != null)
            {
                LoadVendor();
                double TempRate = gvItems.GetFocusedRowCellValue(colPrice) != null ? this.UtilityMember.NumberSet.ToInteger(gvItems.GetFocusedRowCellValue(colPrice).ToString()) : 0;
                if (TempRate == 0)
                {
                    gvItems.SetRowCellValue(gvItems.FocusedRowHandle, colPrice, UnitPrice);
                }
                else
                {
                    gvItems.SetRowCellValue(gvItems.FocusedRowHandle, colPrice, TempRate);
                }
                int TotalQuantity = GetAvailableQuantity(gcItems.DataSource as DataTable, ItemId, LocationId);
                gvItems.SetRowCellValue(gvItems.FocusedRowHandle, colStockQuantity, TotalQuantity);
                GetLedgerIdByName(ItemId);
                gvItems.SetFocusedRowCellValue(colAccountLedgerId, AccountledgerId);
                int EnteredQuantity = GetCalculatedQuantity(LocationId, ItemId, gcItems.DataSource as DataTable);
                int AvailableQuantity = FetchAvailableStock(LocationId, ItemId);
                if (EnteredQuantity > AvailableQuantity)
                {
                    ShowMessageBox(GetMessage(MessageCatalog.Stock.StokItemTransfer.STOCK_QUANTITY_EXCEEDS));
                    gvItems.SetFocusedRowCellValue(colQuantity, 0);
                    gvItems.FocusedColumn = colQuantity;
                }
                int Quantity = GetAvailableQuantity(gcItems.DataSource as DataTable, ItemId, LocationId);
                gvItems.SetFocusedRowCellValue(colStockQuantity, Quantity);
            }

            TextEdit txtTransAmount = edit as TextEdit;
            int grpCounts = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.DecimalPlaces) + 1);
            if (txtTransAmount.Text.Length > grpCounts && txtTransAmount.SelectionLength == txtTransAmount.Text.Length)
                txtTransAmount.Select(txtTransAmount.Text.Length - grpCounts, 0);
            if (LocationId > 0 && ItemId > 0)
            {
                DataTable dtTrans = gcItems.DataSource as DataTable;
                int AvailQuantity = GetAvailableQuantity(dtTrans, ItemId, LocationId); //ShowLedgerBalance(LedgerId, dtTrans, true);
                if (AvailQuantity >= 0) { gvItems.SetRowCellValue(gvItems.FocusedRowHandle, colStockQuantity, AvailQuantity); }
                txtAmount.Text = ReturnseSummaryVal.ToString();
            }
        }

        private void RealColumnEditQuantity()
        {
            colQuantity.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditQuantity_EditValueChanged);
            this.gvItems.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvItems.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colQuantity)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvItems.ShowEditorByMouse();
                    }));
                }
            };
        }

        void RealColumnEditQuantity_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvItems.PostEditor();
            gvItems.UpdateCurrentRow();
            if (gvItems.ActiveEditor == null)
            {
                gvItems.ShowEditor();
            }

            if (gvItems.GetFocusedRowCellValue(colItemLocation) != null)
            {
                int quantity = this.UtilityMember.NumberSet.ToInteger(gvItems.GetFocusedRowCellValue(colQuantity).ToString());
                double rate = this.UtilityMember.NumberSet.ToDouble(gvItems.GetFocusedRowCellValue(colPrice).ToString());
                int TotalQuantity = GetAvailableQuantity(gcItems.DataSource as DataTable, ItemId, LocationId);
                gvItems.SetRowCellValue(gvItems.FocusedRowHandle, colStockQuantity, TotalQuantity);
                GetLedgerIdByName(ItemId);
                int EnteredQuantity = GetCalculatedQuantity(LocationId, ItemId, gcItems.DataSource as DataTable);
                int AvailableQuantity = FetchAvailableStock(LocationId, ItemId);
                if (EnteredQuantity > AvailableQuantity)
                {
                    ShowMessageBox(GetMessage(MessageCatalog.Stock.StokItemTransfer.STOCK_QUANTITY_EXCEEDS));
                    gvItems.SetFocusedRowCellValue(colQuantity, 0);
                    gvItems.FocusedColumn = colQuantity;
                }
                gvItems.SetFocusedRowCellValue(colAmount, (quantity * rate));
                int Quantity = GetAvailableQuantity(gcItems.DataSource as DataTable, ItemId, LocationId);
                gvItems.SetFocusedRowCellValue(colStockQuantity, Quantity);
                txtAmount.Text = (quantity * rate).ToString();
            }

            TextEdit txtTransAmount = edit as TextEdit;
            int grpCounts = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.DecimalPlaces) + 1);
            if (txtTransAmount.Text.Length > grpCounts && txtTransAmount.SelectionLength == txtTransAmount.Text.Length)
                txtTransAmount.Select(txtTransAmount.Text.Length - grpCounts, 0);
            if (LocationId > 0 && ItemId > 0)
            {
                DataTable dtTrans = gcItems.DataSource as DataTable;
                int AvailQuantity = GetAvailableQuantity(dtTrans, ItemId, LocationId); //ShowLedgerBalance(LedgerId, dtTrans, true);
                if (AvailQuantity >= 0) { gvItems.SetRowCellValue(gvItems.FocusedRowHandle, colStockQuantity, AvailQuantity); }
                //txtAmount.Text = ReturnseSummaryVal.ToString();
            }
        }

        private int GetAvailableQuantity(DataTable dtTrans, int ItemId, int LocationId)
        {
            int AvailQuantity = 0;
            int OldValue = 0;
            int NewValue = 0;

            if (dtTrans != null)
            {
                NewValue = GetCalculatedQuantity(LocationId, ItemId, dtTrans);
                AvailQuantity = GetCurBalance(NewValue);
            }
            return AvailQuantity;
        }

        private int GetCalculatedQuantity(int LocationId, int ItemId, DataTable dtTrans)
        {
            int Quantity = 0;
            try
            {
                Quantity = this.UtilityMember.NumberSet.ToInteger(dtTrans.Compute("SUM(QUANTITY)", "ITEM_ID=" + ItemId + " AND LOCATION_ID=" + LocationId).ToString());
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
            return Quantity;
        }

        private int GetCurBalance(int NewQuantity)
        {
            //int AvailQuantity = 0;
            //try
            //{
            //    int CurrentAvail = TotalQuantity = FetchAvailableStock(LocationId, ItemId);

            //    if (CurrentAvail > 0 && OldQuantity > 0)
            //    {
            //        AvailQuantity = CurrentAvail - OldQuantity;
            //    }
            //    else if (CurrentAvail > 0 && OldQuantity < 0)
            //    {
            //        AvailQuantity = CurrentAvail - OldQuantity;
            //    }
            //    else if (CurrentAvail < 0 && OldQuantity > 0)
            //    {
            //        AvailQuantity = OldQuantity - CurrentAvail;
            //    }
            //    else if (CurrentAvail.Equals(0) && OldQuantity > 0)
            //    {
            //        AvailQuantity = OldQuantity;
            //    }
            //    else if (CurrentAvail > 0 && OldQuantity.Equals(0))
            //    {
            //        AvailQuantity = CurrentAvail;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            //}
            //finally { }
            //return AvailQuantity;

            int AvailQuantity = 0;
            try
            {
                int CurrentAvail = TotalQuantity = FetchAvailableStock(LocationId, ItemId);

                if (this.ReturnId.Equals(0))
                {
                    if (CurrentAvail > 0 && NewQuantity > 0)
                    {
                        AvailQuantity = CurrentAvail - NewQuantity;
                    }
                    else if (CurrentAvail > 0 && NewQuantity <= 0)
                    {
                        AvailQuantity = CurrentAvail - NewQuantity;
                    }
                    else if (CurrentAvail < 0 && NewQuantity > 0)
                    {
                        AvailQuantity = NewQuantity - CurrentAvail;
                    }
                    else if (CurrentAvail.Equals(0) && NewQuantity > 0)
                    {
                        AvailQuantity = CurrentAvail - NewQuantity;
                    }
                }
                else
                {
                    AvailQuantity = CurrentAvail;
                    int TempQuantity = gvItems.GetFocusedRowCellValue(colTempQuantity) != null ? this.UtilityMember.NumberSet.ToInteger(gvItems.GetFocusedRowCellValue(colTempQuantity).ToString()) : 0;

                    AvailQuantity = AvailQuantity + TempQuantity;
                    TotalQuantity = AvailQuantity;
                    AvailQuantity -= NewQuantity;

                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
            return AvailQuantity;

        }

        private void GetLedgerIdByName(int itemId)
        {
            using (StockPurchaseDetail purchaseSystem = new StockPurchaseDetail())
            {
                resultArgs = purchaseSystem.FetchLedgerIdByItem(itemId);
                if (resultArgs.Success && resultArgs != null)
                {
                    AccountledgerId = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][purchaseSystem.AppSchema.StockItem.INCOME_LEDGER_IDColumn.ColumnName].ToString());
                }
            }
        }

        //private void DeleteTransaction()
        //{
        //    try
        //    {
        //        if (gvItems.RowCount == 1)
        //        {
        //            int itemId = gvItems.GetFocusedRowCellValue(colItem) != null ? this.UtilityMember.NumberSet.ToInteger(gvItems.GetFocusedRowCellValue(colItem).ToString()) : 0;
        //            Decimal Amount = gvItems.GetFocusedRowCellValue(colAmount) != null ? this.UtilityMember.NumberSet.ToDecimal(gvItems.GetFocusedRowCellValue(colAmount).ToString()) : 0;
        //            if (itemId > 0 || Amount > 0)
        //            {
        //                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //                {
        //                    gvItems.DeleteRow(gvItems.FocusedRowHandle);
        //                    gvItems.UpdateCurrentRow();
        //                    gvItems.AddNewRow();
        //                    gvItems.FocusedColumn = colItem;
        //                }
        //            }
        //            else
        //            {
        //                gvItems.UpdateCurrentRow();
        //                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Bank.BANK_DELETE));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.ToString(), true);
        //    }
        //    finally { }
        //}

        private void DeleteTransaction()
        {
            try
            {
                int itemId = gvItems.GetFocusedRowCellValue(colItem) != null ? this.UtilityMember.NumberSet.ToInteger(gvItems.GetFocusedRowCellValue(colItem).ToString()) : 0;
                Decimal Amount = gvItems.GetFocusedRowCellValue(colAmount) != null ? this.UtilityMember.NumberSet.ToDecimal(gvItems.GetFocusedRowCellValue(colAmount).ToString()) : 0;
                if (gvItems.RowCount > 1)
                {
                    if (VoucherId > 0)
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            gvItems.DeleteRow(gvItems.FocusedRowHandle);
                            gvItems.FocusedColumn = gvItems.Columns.ColumnByName(colItem.Name);
                        }
                    }
                    else
                    {
                        if (dtReturn != null)
                        {
                            if (ItemId > 0 || Amount > 0)
                            {
                                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    gvItems.DeleteRow(gvItems.FocusedRowHandle);
                                    gvItems.FocusedColumn = gvItems.Columns.ColumnByName(colItem.Name);
                                }
                            }
                        }
                        else
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Bank.BANK_DELETE));
                        }
                    }
                }
                else if (gvItems.RowCount == 1)
                {
                    if (ItemId > 0 || Amount > 0)
                    {
                        if (VoucherId > 0)
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                ConstructTable();
                                gvItems.FocusedColumn = gvItems.Columns.ColumnByName(colItem.Name);
                                //int sourceId = (rgTransactionType.SelectedIndex == 0 || rgTransactionType.SelectedIndex == 2) ? (int)Source.To : (int)Source.By;
                                //gvPurchaseItems.SetRowCellValue(gvTransaction.FocusedRowHandle, colSource, sourceId);
                            }
                        }
                        else
                        {

                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                gvItems.DeleteRow(gvItems.FocusedRowHandle);
                                ConstructTable();
                                gvItems.FocusedColumn = gvItems.Columns.ColumnByName(colItem.Name);
                            }
                            else
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Bank.BANK_DELETE));
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Bank.BANK_DELETE));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        #endregion
    }
}