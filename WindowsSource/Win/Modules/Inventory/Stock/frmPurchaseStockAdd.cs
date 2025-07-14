using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model;
using Bosco.Utility;
using Bosco.Model.UIModel;
using Bosco.Model.Inventory.Stock;
using DevExpress.XtraGrid;
using ACPP.Modules.Transaction;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Bosco.Model.Transaction;

namespace ACPP.Modules.Inventory.Stock
{
    public partial class frmPurchaseStockAdd : frmFinanceBaseAdd
    {
        #region Event Handler
        public event EventHandler UpdateHeld;
        #endregion

        #region Variables
        ResultArgs resultArgs = new ResultArgs();
        int AccountledgerId = 0;
        //StockPurchaseTransType Type=0;
        #endregion

        #region Properties
        public int PurchaseTransType { get; set; }
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
        public int PurchaseId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal NetAmount { get; set; }
        private int AvailQuantity { get; set; }
        private double Rate { get; set; }
        public int VoucherId { get; set; }
        public int VoucherType { get; set; }
        public bool isMouseClicked { get; set; }

        private double PurchaseSummaryVal
        {
            get { return colAmount.SummaryItem.SummaryValue != null ? this.UtilityMember.NumberSet.ToDouble(colAmount.SummaryItem.SummaryValue.ToString()) : 0; }
        }

        private int ItemId { get { return gvPurchaseItems.GetFocusedRowCellValue(colItemId) != null ? this.UtilityMember.NumberSet.ToInteger(gvPurchaseItems.GetFocusedRowCellValue(colItemId).ToString()) : 0; } }

        private int LocatoinId { get { return gvPurchaseItems.GetFocusedRowCellValue(colLocationId) != null ? this.UtilityMember.NumberSet.ToInteger(gvPurchaseItems.GetFocusedRowCellValue(colLocationId).ToString()) : 0; } }

        private DataTable dtGridControlSource;
        private DataTable GridSource
        {
            get
            {
                DataTable dtGridSource = gcPurchaseItems.DataSource as DataTable;
                DataView dvGridSource = dtGridSource.DefaultView;
                dvGridSource.RowFilter = "ITEM_ID>0";
                dtGridControlSource = dvGridSource.ToTable();
                dvGridSource.RowFilter = "";
                return dtGridControlSource;
            }
        }

        private const string ITEM_ID = "ITEM_ID";
        private const string LOCATION_ID = "LOCATION_ID";
        private const string VENDOR_ID = "VENDOR_ID";
        private const string UNIT_PRICE = "UNIT_PRICE";
        private const string QUANTITY = "QUANTITY";
        private const string TOTAL_AMOUNT = "TOTAL_AMOUNT";
        private const string AVAIL_QUANTITY = "AVAIL_QUANTITY";
        private const string AMOUNT = "AMOUNT";
        #endregion

        #region Constructor
        public frmPurchaseStockAdd()
        {
            InitializeComponent();
        }

        public frmPurchaseStockAdd(int purchaseId, int projectId, string projectName, DateTime purchaseDate, int purchasetype)
            : this()
        {
            this.PurchaseId = purchaseId;
            this.ProjectId = projectId;
            this.ProjectName = projectName;
            this.PurchaseDate = purchaseDate;
            this.PurchaseTransType = purchasetype;
            RealColumnEditTransAmount();
            RealColumnEditQuantity();
            RealColumnEditRateAmount();
            RealColumnEditTransAmount();
            RealColumnEditLocation();
            //RealColumnEditItem();
        }
        #endregion

        #region Events
        private void frmPurchaseStockAdd_Load(object sender, EventArgs e)
        {
            setDefaults();
            setTitle();
            ConstructStockPurchase();
            loadLocation();
            loadItemDetails();
            loadCashBankLedgers();
            loadVendors();
            FillStockPurchaseDetails();
            LoadNarrationAutoComplete();
            LoadNameAddressAutoComplete();
            //lcgNameNarration.Width = Width + 10;
            if ((int)StockPurchaseTransType.Receive == PurchaseTransType)
            {
                lcgNameNarration.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcgNetAmount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.Height = 558 - 58;
                lyVoucherNo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //txtDiscountPer.Enabled = txtDiscount.Enabled = txtDiscountPer.Enabled = txtTax.Enabled = txtTaxAmount.Enabled = glkpLedger.Enabled = txtOterChargers.Enabled = false;
            }
            dtePurchaseDate.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtePurchaseDate.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
        }

        private void gcPurchaseItems_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control)
                {
                    gvPurchaseItems.PostEditor();
                    gvPurchaseItems.UpdateCurrentRow();

                    if (gvPurchaseItems.FocusedColumn == colItems)
                    {
                        int ItemId = gvPurchaseItems.GetFocusedRowCellValue(colItems).ToString() != null ? this.UtilityMember.NumberSet.ToInteger(gvPurchaseItems.GetFocusedRowCellValue(colItems).ToString()) : 0;

                        if (string.IsNullOrEmpty(gvPurchaseItems.GetFocusedRowCellValue(colItems).ToString()))
                        {
                            NetAmount = this.UtilityMember.NumberSet.ToDecimal(PurchaseSummaryVal.ToString());
                            gvPurchaseItems.CloseEditor();
                            e.SuppressKeyPress = true;
                            e.Handled = true;
                            txtDiscountPer.Select();
                            txtDiscountPer.Focus();
                            if (PurchaseTransType != (int)StockPurchaseTransType.Receive)
                            {
                                txtDiscountPer.Select();
                            }
                            else
                            {
                                txtNameandAddress.Select();
                            }
                        }
                        else
                        {
                            gvPurchaseItems.SetFocusedRowCellValue(colReorder, FetchReorderLevel());
                        }
                        if (ItemId > 0)
                        {
                            string UnitMeasure = GetUnitofMeasure();
                            gvPurchaseItems.SetRowCellValue(gvPurchaseItems.FocusedRowHandle, colUnitofMeasure, UnitMeasure);
                            GetLedgerIdByName(ItemId);
                            gvPurchaseItems.SetRowCellValue(gvPurchaseItems.FocusedRowHandle, colAccountLedgerId, AccountledgerId);
                        }
                    }

                    if (gvPurchaseItems.FocusedColumn == colLocation)
                    {
                        int ItemId = gvPurchaseItems.GetFocusedRowCellValue(colItems) != null ? this.UtilityMember.NumberSet.ToInteger(gvPurchaseItems.GetFocusedRowCellValue(colItems).ToString()) : 0;
                        int LocationId = gvPurchaseItems.GetFocusedRowCellValue(colLocation) != null ? this.UtilityMember.NumberSet.ToInteger(gvPurchaseItems.GetFocusedRowCellValue(colLocation).ToString()) : 0;
                        if (ItemId > 0 && LocationId > 0)
                        {
                            AvailQuantity = GetAvailableQuantity(gcPurchaseItems.DataSource as DataTable, ItemId, LocationId);
                            gvPurchaseItems.SetRowCellValue(gvPurchaseItems.FocusedRowHandle, colStockQuantity, AvailQuantity);
                            double TempRate = gvPurchaseItems.GetFocusedRowCellValue(colUnitPrice) != null ? this.UtilityMember.NumberSet.ToInteger(gvPurchaseItems.GetFocusedRowCellValue(colUnitPrice).ToString()) : 0;
                            if (TempRate == 0)
                            {
                                gvPurchaseItems.SetRowCellValue(gvPurchaseItems.FocusedRowHandle, colUnitPrice, Rate);
                            }
                            else
                            {
                                gvPurchaseItems.SetRowCellValue(gvPurchaseItems.FocusedRowHandle, colUnitPrice, TempRate);
                            }
                        }
                    }

                    if (gvPurchaseItems.FocusedColumn == colUnitPrice && gvPurchaseItems.IsLastRow)
                    {
                        int Rate = gvPurchaseItems.GetFocusedRowCellValue(colPurchaseQuantity) != null ? this.UtilityMember.NumberSet.ToInteger(gvPurchaseItems.GetFocusedRowCellValue(colPurchaseQuantity).ToString()) : 0;
                        int LocationId = gvPurchaseItems.GetFocusedRowCellValue(colLocationId) != null ? this.UtilityMember.NumberSet.ToInteger(gvPurchaseItems.GetFocusedRowCellValue(colLocationId).ToString()) : 0;
                        decimal Quantity = gvPurchaseItems.GetFocusedRowCellValue(colUnitPrice) != null ? this.UtilityMember.NumberSet.ToDecimal(gvPurchaseItems.GetFocusedRowCellValue(colUnitPrice).ToString()) : 0;
                        if (ItemId > 0 && Rate > 0 && Quantity > 0 && LocationId > 0)
                        {
                            gvPurchaseItems.SetRowCellValue(gvPurchaseItems.FocusedRowHandle, colAmount, Rate * Quantity);
                            gvPurchaseItems.AddNewRow();
                            lblTotalAmount.Text = lblNetPaymentAmount.Text = this.UtilityMember.NumberSet.ToCurrency(PurchaseSummaryVal);
                            NetAmount = this.UtilityMember.NumberSet.ToDecimal(PurchaseSummaryVal.ToString());
                            gvPurchaseItems.FocusedColumn = colDelete;
                            gvPurchaseItems.SelectAll();
                        }
                        else
                        {
                            gvPurchaseItems.CloseEditor();
                            e.SuppressKeyPress = true;
                            e.Handled = true;
                            txtDiscountPer.Select();
                            txtDiscountPer.Focus();
                        }
                    }

                    //if (gvPurchaseItems.FocusedColumn == colAmount && gvPurchaseItems.IsLastRow)
                    //{
                    //    int ItemId = gvPurchaseItems.GetFocusedRowCellValue(colItems) != null ? this.UtilityMember.NumberSet.ToInteger(gvPurchaseItems.GetFocusedRowCellValue(colItems).ToString()) : 0;
                    //    int Rate = gvPurchaseItems.GetFocusedRowCellValue(colPurchaseQuantity) != null ? this.UtilityMember.NumberSet.ToInteger(gvPurchaseItems.GetFocusedRowCellValue(colPurchaseQuantity).ToString()) : 0;
                    //    decimal Quantity = gvPurchaseItems.GetFocusedRowCellValue(colUnitPrice) != null ? this.UtilityMember.NumberSet.ToDecimal(gvPurchaseItems.GetFocusedRowCellValue(colUnitPrice).ToString()) : 0;
                    //    int LocationId = gvPurchaseItems.GetFocusedRowCellValue(colLocationId) != null ? this.UtilityMember.NumberSet.ToInteger(gvPurchaseItems.GetFocusedRowCellValue(colLocationId).ToString()) : 0;
                    //    if (ItemId > 0 && Rate > 0 && Quantity > 0 && LocationId > 0)
                    //    {
                    //        gvPurchaseItems.AddNewRow();
                    //        lblTotalAmount.Text = lblNetPaymentAmount.Text = this.UtilityMember.NumberSet.ToCurrency(PurchaseSummaryVal);
                    //        NetAmount = this.UtilityMember.NumberSet.ToDecimal(PurchaseSummaryVal.ToString());
                    //        gvPurchaseItems.FocusedColumn = colDelete;
                    //        gvPurchaseItems.SelectAll();
                    //    }
                    //    else
                    //    {
                    //        gvPurchaseItems.CloseEditor();
                    //        e.SuppressKeyPress = true;
                    //        e.Handled = true;
                    //        txtDiscountPer.Select();
                    //        txtDiscountPer.Focus();
                    //    }
                    //}
                }
                else if (gvPurchaseItems.IsFirstRow && gvPurchaseItems.FocusedColumn == colItems && e.Shift && e.KeyCode == Keys.Tab)
                {
                    txtVoucherNo.Select();
                    txtVoucherNo.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidatePurchaseStock())
                {
                    using (StockPurchaseDetail stockPurchase = new StockPurchaseDetail())
                    {
                        stockPurchase.PurchaseId = PurchaseId;
                        stockPurchase.ProjectId = ProjectId;
                        stockPurchase.PurchaseDate = dtePurchaseDate.DateTime;
                        stockPurchase.PurchaseOrderNo = txtBillNo.Text;
                        stockPurchase.VendorId = glkpVendor.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpVendor.EditValue.ToString()) : 0;
                        stockPurchase.VoucherNo = txtVoucherNo.Text;
                        stockPurchase.DiscountPer = this.UtilityMember.NumberSet.ToDecimal(txtDiscountPer.Text);
                        stockPurchase.Discount = this.UtilityMember.NumberSet.ToDecimal(txtDiscount.Text);
                        stockPurchase.OtherCharges = this.UtilityMember.NumberSet.ToDecimal(txtOterChargers.Text);
                        stockPurchase.Tax = this.UtilityMember.NumberSet.ToDecimal(txtTax.Text);
                        stockPurchase.TaxAmount = this.UtilityMember.NumberSet.ToDecimal(txtTaxAmount.Text);
                        stockPurchase.Narration = txtNarration.Text;
                        stockPurchase.NameandAddress = txtNameandAddress.Text;
                        stockPurchase.NetAmount = NetAmount;
                        stockPurchase.PurchaseFlag = PurchaseTransType;
                        stockPurchase.LedgerId = glkpLedger.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpLedger.EditValue.ToString()) : 0;
                        stockPurchase.BranchId = 0;
                        stockPurchase.VoucherId = VoucherId;
                        DataTable dtPurchase = gcPurchaseItems.DataSource as DataTable;
                        DataTable dtFilteredRows = dtPurchase.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull)).CopyToDataTable();

                        stockPurchase.dtStockPurchaseDetail = dtFilteredRows;
                        resultArgs = stockPurchase.SavePurchase();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            if (PurchaseId.Equals(0))
                                clearControls();
                            else
                                this.Close();
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                                LoadNarrationAutoComplete();
                                LoadNameAddressAutoComplete();

                                dtePurchaseDate.Select();
                                dtePurchaseDate.Focus();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDiscountPer_Leave(object sender, EventArgs e)
        {
            lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(PurchaseSummaryVal);
            txtDiscount.Text = this.UtilityMember.NumberSet.ToNumber(CommonMethod.CalculateAmount(PurchaseSummaryVal, this.UtilityMember.NumberSet.ToDouble(txtDiscountPer.Text))).ToString();

            lblNetPaymentAmount.Text = this.UtilityMember.NumberSet.ToCurrency(CommonMethod.calculateAssetNetPayment(PurchaseSummaryVal,
                this.UtilityMember.NumberSet.ToDouble(txtDiscount.Text),
               this.UtilityMember.NumberSet.ToDouble(txtTaxAmount.Text), this.UtilityMember.NumberSet.ToDouble(txtOterChargers.Text)));

            NetAmount = this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) > 0 ?
                this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) : NetAmount;
        }

        private void txtDiscount_Leave(object sender, EventArgs e)
        {
            //txtDiscountPer.Text = this.UtilityMember.NumberSet.ToNumber(CommonMethod.CalculatePerCent(PurchaseSummaryVal, this.UtilityMember.NumberSet.ToDouble(txtDiscount.Text))).ToString();

            //lblDiscountAmount.Text = this.UtilityMember.NumberSet.ToCurrency(-this.UtilityMember.NumberSet.ToDouble(txtDiscount.Text));

            //lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(PurchaseSummaryVal);
            //lblNetPaymentAmount.Text = this.UtilityMember.NumberSet.ToCurrency(CommonMethod.calculateAssetNetPayment(PurchaseSummaryVal,
            //    this.UtilityMember.NumberSet.ToDouble(txtDiscount.Text),
            //   this.UtilityMember.NumberSet.ToDouble(txtTaxAmount.Text), this.UtilityMember.NumberSet.ToDouble(txtOterChargers.Text)));

            //NetAmount = this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) > 0 ?
            //    this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) : NetAmount;
        }

        private void txtOterChargers_Leave(object sender, EventArgs e)
        {

            lblOtherChargesAmount.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(txtOterChargers.Text));

            lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(PurchaseSummaryVal);
            lblNetPaymentAmount.Text = this.UtilityMember.NumberSet.ToCurrency(CommonMethod.calculateAssetNetPayment(PurchaseSummaryVal,
               this.UtilityMember.NumberSet.ToDouble(txtDiscount.Text),
              this.UtilityMember.NumberSet.ToDouble(txtTaxAmount.Text), this.UtilityMember.NumberSet.ToDouble(txtOterChargers.Text)));

            NetAmount = this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) > 0 ?
                this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) : NetAmount;
        }

        private void txtTax_Leave(object sender, EventArgs e)
        {

            lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(PurchaseSummaryVal);
            txtTaxAmount.Text = this.UtilityMember.NumberSet.ToNumber(CommonMethod.CalculateAmount(PurchaseSummaryVal,
                this.UtilityMember.NumberSet.ToDouble(txtTax.Text))).ToString();

            this.UtilityMember.NumberSet.ToCurrency(CommonMethod.calculateAssetNetPayment(PurchaseSummaryVal,
               this.UtilityMember.NumberSet.ToDouble(txtDiscount.Text),
              this.UtilityMember.NumberSet.ToDouble(txtTaxAmount.Text), this.UtilityMember.NumberSet.ToDouble(txtOterChargers.Text)));

            NetAmount = this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) > 0 ?
                this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) : NetAmount;
        }

        private void txtTaxAmount_Leave(object sender, EventArgs e)
        {

            //            lblTaxAmount.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(txtTaxAmount.Text));
            //            txtTax.Text = this.UtilityMember.NumberSet.ToNumber(CommonMethod.CalculatePerCent(PurchaseSummaryVal, this.UtilityMember.NumberSet.ToDouble(txtTaxAmount.Text))).ToString();

            //            lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(PurchaseSummaryVal);
            //            lblNetPaymentAmount.Text = this.UtilityMember.NumberSet.ToCurrency(CommonMethod.calculateAssetNetPayment(PurchaseSummaryVal,
            //               this.UtilityMember.NumberSet.ToDouble(txtDiscount.Text),
            //              this.UtilityMember.NumberSet.ToDouble(txtTaxAmount.Text), this.UtilityMember.NumberSet.ToDouble(txtOterChargers.Text)));

            //            NetAmount = this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) > 0 ? this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) :
            //NetAmount;
        }

        private void dtePurchaseDate_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForDateTimeEdit(dtePurchaseDate);
        }

        private void txtVoucherNo_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtVoucherNo);
            setFocusToGridControl();
        }

        private void glkpVendor_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpVendor);
        }

        private void txtBillNo_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtBillNo);
        }

        private void glkpLedger_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpLedger);
        }

        private void rbtnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                DeleteTransaction();
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void gcPurchaseItems_Enter(object sender, EventArgs e)
        {
            gvPurchaseItems.OptionsSelection.EnableAppearanceFocusedCell = true;
        }

        private void gcPurchaseItems_Leave(object sender, EventArgs e)
        {
            gvPurchaseItems.OptionsSelection.EnableAppearanceFocusedCell = false;
        }

        private void frmPurchaseStockAdd_KeyDown(object sender, KeyEventArgs e)
        {
            ProcessShortcutKeys(e);
        }

        private void glkpLedger_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                ACPP.Modules.Master.frmLedgerDetailAdd frmBank = new ACPP.Modules.Master.frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.BK, ProjectId);
                frmBank.ShowDialog();
                loadCashBankLedgers();
            }
        }

        private void glkpVendor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                frmVendorInfoAdd frmVendor = new frmVendorInfoAdd(0, VendorManufacture.Vendor);
                frmVendor.ShowDialog();
                loadVendors();
            }
        }

        private void bbiDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteTransaction();
        }

        #endregion

        #region Fetch Default Methods

        private void loadLocation()
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

        private void loadItemDetails()
        {
            try
            {
                using (StockPurchaseSalesSystem salesSystem = new StockPurchaseSalesSystem())
                {
                    resultArgs = salesSystem.FetchStockItemDetails();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpItems, resultArgs.DataSource.Table, "ITEM_NAME", salesSystem.AppSchema.StockPurchaseSalesDetails.ITEM_IDColumn.ColumnName);
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void DeleteTransaction()
        {
            try
            {
                dtGridControlSource = gcPurchaseItems.DataSource as DataTable;
                int itemId = gvPurchaseItems.GetFocusedRowCellValue(colItemId) != null ? this.UtilityMember.NumberSet.ToInteger(gvPurchaseItems.GetFocusedRowCellValue(colItemId).ToString()) : 0;
                Decimal Amount = gvPurchaseItems.GetFocusedRowCellValue(colAmount) != null ? this.UtilityMember.NumberSet.ToDecimal(gvPurchaseItems.GetFocusedRowCellValue(colAmount).ToString()) : 0;
                if (gvPurchaseItems.RowCount > 1)
                {
                    if (VoucherId > 0)
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            gvPurchaseItems.DeleteRow(gvPurchaseItems.FocusedRowHandle);
                            gvPurchaseItems.UpdateCurrentRow();
                            gcPurchaseItems.RefreshDataSource();
                            gvPurchaseItems.FocusedColumn = colItemName;
                        }
                    }
                    else
                    {
                        if (dtGridControlSource != null)
                        {
                            if (ItemId > 0 || Amount > 0)
                            {
                                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    gvPurchaseItems.DeleteRow(gvPurchaseItems.FocusedRowHandle);
                                    gvPurchaseItems.UpdateCurrentRow();
                                    gcPurchaseItems.RefreshDataSource();
                                    gvPurchaseItems.FocusedColumn = colItemName;
                                }
                            }
                        }
                        else
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Bank.BANK_DELETE));
                        }
                    }
                }
                else if (gvPurchaseItems.RowCount == 1)
                {
                    if (ItemId > 0 || Amount > 0)
                    {
                        if (VoucherId > 0)
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                ConstructStockPurchase();
                                gvPurchaseItems.UpdateCurrentRow();
                                gcPurchaseItems.RefreshDataSource();
                                gvPurchaseItems.FocusedColumn = colItemName;
                            }
                        }
                        else
                        {

                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                gvPurchaseItems.DeleteRow(gvPurchaseItems.FocusedRowHandle);
                                ConstructStockPurchase();
                                gvPurchaseItems.UpdateCurrentRow();
                                gcPurchaseItems.RefreshDataSource();
                                gvPurchaseItems.FocusedColumn = colItemName;
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

        private void loadVendors()
        {
            try
            {
                using (VendorInfoSystem vendorSystem = new VendorInfoSystem())
                {
                    resultArgs = vendorSystem.FetchDetails();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpVendor, resultArgs.DataSource.Table, "NAME", "ID");
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void loadCashBankLedgers()
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    ledgerSystem.ProjectId = this.ProjectId;
                    resultArgs = ledgerSystem.FetchCashBankLedger();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpLedger, resultArgs.DataSource.Table, ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void ConstructStockPurchase()
        {
            try
            {
                DataTable dtStockPurchase = new DataTable();
                dtStockPurchase.Columns.Add("ITEM_ID", typeof(Int32));
                dtStockPurchase.Columns.Add("LOCATION_ID", typeof(Int32));
                dtStockPurchase.Columns.Add("QUANTITY", typeof(Int32));
                dtStockPurchase.Columns.Add("UNIT_PRICE", typeof(decimal));
                dtStockPurchase.Columns.Add("AMOUNT", typeof(decimal));
                dtStockPurchase.Columns.Add("AVAIL_QUANTITY", typeof(Int32));
                dtStockPurchase.Columns.Add("TEMP_QUANTITY", typeof(Int32));
                dtStockPurchase.Columns.Add("REORDER", typeof(Int32));
                dtStockPurchase.Columns.Add("SYMBOL", typeof(string));
                dtStockPurchase.Columns.Add("ACCOUNT_LEDGER_ID", typeof(Int32));
                gcPurchaseItems.DataSource = dtStockPurchase;
                gvPurchaseItems.AddNewRow();
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void setDefaults()
        {
            uccProject.Caption = this.ProjectName;
            dtePurchaseDate.DateTime = this.PurchaseDate;
            CommonMethod.NetAmout = CommonMethod.CalCuTaxAmount = 0;
            //this.Text = this.PurchaseId.Equals((int)AddNewRow.NewRow) ? "Voucher - Purchase / Receive" : "Purchase / Receive (Edit)";
            txtDiscountPer.Enabled = true;
        }
        #endregion

        #region Grid Methods

        private bool ValidatePurchaseStock()
        {
            bool isTrue = true;
            if (!IsValidTransactionDate())
            {
                dtePurchaseDate.DateTime = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom.ToString(), false);
                isTrue = false;
            }
            //  else if (string.IsNullOrEmpty(txtVoucherNo.Text))
            else if (TransVoucherMethod == (int)TransactionVoucherMethod.Manual && string.IsNullOrEmpty(txtVoucherNo.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_NUMBER_EMPTY));
                this.SetBorderColor(txtVoucherNo);
                isTrue = false;
                txtVoucherNo.Focus();
            }
            else if (string.IsNullOrEmpty(dtePurchaseDate.Text) || dtePurchaseDate.DateTime.Equals(DateTime.MinValue))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Stock.StockMasterPurchase.PURCHASE_DATE_EMTPTY));
                this.SetBorderColorForDateTimeEdit(dtePurchaseDate);
                isTrue = false;
                dtePurchaseDate.Focus();
            }
            else if (string.IsNullOrEmpty(txtBillNo.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Stock.StockMasterPurchase.BILL_NO_EMPTY));
                this.SetBorderColor(txtBillNo);
                isTrue = false;
                txtBillNo.Focus();
            }
            else if (string.IsNullOrEmpty(glkpVendor.Text) || glkpVendor.EditValue.Equals("0") || glkpVendor.EditValue == null)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.VendorInfo.VENDOR_NAME_EMPTY));
                this.SetBorderColorForGridLookUpEdit(glkpVendor);
                isTrue = false;
                glkpVendor.Focus();
            }
            //Sudhakar
            //else if (string.IsNullOrEmpty(txtVoucherNo.Text))
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_NUMBER_EMPTY));
            //    this.SetBorderColor(txtVoucherNo);
            //    isTrue = false;
            //    txtVoucherNo.Focus();
            //}
            else if (UtilityMember.NumberSet.ToDouble(txtDiscount.Text) > PurchaseSummaryVal)
            {
                this.SetBorderColor(txtDiscount);
                isTrue = false;
                txtDiscount.Focus();
                this.ShowMessageBoxError(this.GetMessage(MessageCatalog.Stock.StockMasterPurchase.DISCOUNT_AMOUNT_EXCEEDS));
                txtDiscount.Text = string.Empty;
            }
            else if (UtilityMember.NumberSet.ToDouble(txtTaxAmount.Text) > PurchaseSummaryVal)
            {
                this.SetBorderColor(txtTaxAmount);
                isTrue = false;
                txtTaxAmount.Focus();
                this.ShowMessageBoxError(this.GetMessage(MessageCatalog.Stock.StockMasterPurchase.TAX_AMOUNT_EXCEEDS));
                txtTaxAmount.Text = string.Empty;
            }

            else if (!IsValidGrid())
            {
                isTrue = false;
            }

            else if ((int)StockPurchaseTransType.Purchase == PurchaseTransType)
            {
                if (string.IsNullOrEmpty(glkpLedger.Text) || glkpLedger.EditValue.Equals("0") || glkpLedger.EditValue == null)
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Stock.StockPurcahseReturns.STOCK_LEDGER_EMPTY));
                    this.SetBorderColorForGridLookUpEdit(glkpLedger);
                    isTrue = false;
                    glkpLedger.Focus();
                }
            }
            return isTrue;
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
            dtRecentVoucherDate = UtilityMember.DateSet.ToDate(PurchaseDate.ToString(), false);

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
                    this.ShowMessageBoxWarning("Project start date and closed date does not fall between transaction period." + Environment.NewLine + "Kindly change the Project start date and closed date and try again.");
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

        private bool IsValidGrid()
        {
            DataTable dtTrans = gcPurchaseItems.DataSource as DataTable;

            int ItmId = 0;
            int LocId = 0;
            decimal Amt = 0;
            decimal qty = 0;
            decimal rate = 0;
            bool isValid = false;
            int RowPosition = 0;

            string validateMessage = "Stock Details are not filled.";

            DataView dv = new DataView(dtTrans);
            dv.RowFilter = "(ITEM_ID>0 OR LOCATION_ID>0 OR QUANTITY>0 OR UNIT_PRICE>0 OR AMOUNT>0)";
            gvPurchaseItems.FocusedColumn = colItems;
            if (dv.Count > 0)
            {
                isValid = true;
                foreach (DataRowView drTrans in dv)
                {
                    ItmId = this.UtilityMember.NumberSet.ToInteger(drTrans["ITEM_ID"].ToString());
                    LocId = this.UtilityMember.NumberSet.ToInteger(drTrans["LOCATION_ID"].ToString());
                    Amt = this.UtilityMember.NumberSet.ToDecimal(drTrans["AMOUNT"].ToString());
                    qty = this.UtilityMember.NumberSet.ToDecimal(drTrans["QUANTITY"].ToString());
                    rate = this.UtilityMember.NumberSet.ToDecimal(drTrans["UNIT_PRICE"].ToString());

                    if ((ItmId == 0 || LocId == 0 || Amt == 0 || qty == 0 || rate == 0))
                    {
                        if (ItmId == 0)
                        {
                            validateMessage = "Required Information not filled, Item is empty";
                            gvPurchaseItems.FocusedColumn = colItems;
                        }
                        else if (LocId == 0)
                        {
                            validateMessage = "Required Information not filled, Location is empty";
                            gvPurchaseItems.FocusedColumn = colLocation;
                        }
                        else if (qty == 0)
                        {
                            validateMessage = "Required Information not filled, Quantity is empty";
                            gvPurchaseItems.FocusedColumn = colPurchaseQuantity;
                        }
                        else if (rate == 0)
                        {
                            validateMessage = "Required Information not filled, Unit Price is empty";
                            gvPurchaseItems.FocusedColumn = colUnitPrice;
                        }
                        else if (Amt == 0)
                        {
                            validateMessage = "Required Information not filled, Amount is empty";
                            gvPurchaseItems.FocusedColumn = colAmount;
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
                gvPurchaseItems.CloseEditor();
                gvPurchaseItems.FocusedRowHandle = gvPurchaseItems.GetRowHandle(RowPosition);
                gvPurchaseItems.ShowEditor();
            }

            return isValid;
        }

        public bool ValidateRows(DataTable Items)
        {
            bool Success = true;
            try
            {
                Items = RemoveEmptyRows(Items.Copy());
                if (Items.Rows.Count > 0)
                {
                    foreach (DataRow drItem in Items.Rows)
                    {

                        if (this.UtilityMember.NumberSet.ToInteger(drItem[ITEM_ID].ToString()) != 0)
                        {
                            if (this.UtilityMember.NumberSet.ToInteger(drItem[ITEM_ID].ToString()) == 0)
                            {
                                this.ShowMessageBox(GetMessage(MessageCatalog.Stock.StockPurcahseReturns.STOCK_ITEM_EMPTY));
                                Success = false;
                                gvPurchaseItems.Focus();
                                gcPurchaseItems.Select();
                                gvPurchaseItems.FocusedColumn = colItems;
                            }
                            else if (UtilityMember.NumberSet.ToInteger(drItem[LOCATION_ID].ToString()) == 0)
                            {
                                this.ShowMessageBox(GetMessage(MessageCatalog.Stock.StockPurcahseReturns.STOCK_LOCATION_EMPTY));
                                Success = false;
                                gvPurchaseItems.Focus();
                                gcPurchaseItems.Select();
                                gvPurchaseItems.FocusedColumn = colLocation;
                            }

                            else if (UtilityMember.NumberSet.ToDecimal(drItem[UNIT_PRICE].ToString()) == 0)
                            {
                                this.ShowMessageBox(GetMessage(MessageCatalog.Stock.StockPurcahseReturns.STOCK_UNIT_PRICE_EMPTY));
                                Success = false;
                                gvPurchaseItems.Focus();
                                gcPurchaseItems.Select();
                                gvPurchaseItems.FocusedColumn = colUnitPrice;
                            }
                            else if (UtilityMember.NumberSet.ToInteger(drItem[QUANTITY].ToString()) == 0)
                            {
                                this.ShowMessageBox(GetMessage(MessageCatalog.Stock.StockPurcahseReturns.STOCK_QUANTITY_EMPTY));
                                Success = false;
                                gvPurchaseItems.Focus();
                                gcPurchaseItems.Select();
                                gvPurchaseItems.FocusedColumn = colAmount;
                            }
                            else if (UtilityMember.NumberSet.ToDecimal(drItem[AMOUNT].ToString()) == 0)
                            {
                                this.ShowMessageBox(GetMessage(MessageCatalog.Stock.StockPurcahseReturns.STOCK_AMOUNT_EMPTY));
                                Success = false;
                            }
                        }
                        else
                        {
                            this.ShowMessageBox(GetMessage(MessageCatalog.Stock.StockPurcahseReturns.STOCK_NO_ITEM_ROWS));
                            Success = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
                Success = false;
            }
            return Success;
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

        private void setFocusToGridControl()
        {
            gcPurchaseItems.Focus();
            gcPurchaseItems.Select();
            gvPurchaseItems.FocusedColumn = colItems;
        }

        private void clearControls()
        {
            if (PurchaseId.Equals(0))
            {
                txtBillNo.Text = txtVoucherNo.Text = txtDiscount.Text = txtTaxAmount.Text = txtTax.Text = txtOterChargers.Text = txtNameandAddress.Text = txtNarration.Text = string.Empty;
                glkpLedger.EditValue = glkpVendor.EditValue = null;
                gcPurchaseItems.DataSource = null;
                lblTaxAmount.Text = lblOtherChargesAmount.Text = lblNetPaymentAmount.Text = lblDiscountAmount.Text = lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble("0.00"));
                ConstructStockPurchase();
                LoadVoucherNo();
            }
            else
            {
                this.Close();
            }
        }

        private void FillStockPurchaseDetails()
        {
            try
            {
                if (this.PurchaseId > 0)
                {
                    using (StockPurchaseDetail stockPurchase = new StockPurchaseDetail(this.PurchaseId))
                    {
                        dtePurchaseDate.DateTime = stockPurchase.PurchaseDate;
                        txtBillNo.Text = stockPurchase.PurchaseOrderNo;
                        txtVoucherNo.Text = stockPurchase.VoucherId.ToString();
                        glkpVendor.EditValue = stockPurchase.VendorId;
                        glkpLedger.EditValue = stockPurchase.LedgerId;
                        txtDiscountPer.Text = stockPurchase.DiscountPer.ToString();
                        txtDiscount.Text = this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(stockPurchase.Discount.ToString()));
                        txtTaxAmount.Text = this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(stockPurchase.TaxAmount.ToString()));
                        txtTax.Text = stockPurchase.Tax.ToString();
                        txtOterChargers.Text = this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(stockPurchase.OtherCharges.ToString()));
                        txtNameandAddress.Text = stockPurchase.NameandAddress;
                        txtNarration.Text = stockPurchase.Narration;
                        gcPurchaseItems.DataSource = stockPurchase.dtStockPurchaseDetail;
                        lblDiscountAmount.Text = this.UtilityMember.NumberSet.ToCurrency(-this.UtilityMember.NumberSet.ToDouble(stockPurchase.Discount.ToString()));
                        lblTaxAmount.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(stockPurchase.TaxAmount.ToString()));
                        lblOtherChargesAmount.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(stockPurchase.OtherCharges.ToString()));
                        //lblNetPaymentAmount.Text = this.UtilityMember.NumberSet.ToCurrency(CommonMethod.calculateStockNetPayment(PurchaseSummaryVal, this.UtilityMember.NumberSet.ToDouble(stockPurchase.Discount.ToString()), this.UtilityMember.NumberSet.ToDouble(stockPurchase.OtherCharges.ToString()), this.UtilityMember.NumberSet.ToDouble(stockPurchase.Tax.ToString()), this.UtilityMember.NumberSet.ToDouble(stockPurchase.TaxAmount.ToString())));
                        lblNetPaymentAmount.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(stockPurchase.NetAmount.ToString()));
                        lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(PurchaseSummaryVal);
                        NetAmount = stockPurchase.NetAmount;
                        VoucherId = stockPurchase.VoucherId;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private int FetchAvailableStock(int LocationId, int ItemId)
        {
            int AvaliableStock = 0;
            Rate = 0;
            try
            {
                using (StockBalanceSystem BalanceSystem = new StockBalanceSystem())
                {
                    resultArgs = BalanceSystem.GetCurrentBalance(this.ProjectId, ItemId, LocationId);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        AvaliableStock = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][BalanceSystem.AppSchema.StockPurchaseDetails.QUANTITYColumn.ColumnName].ToString());
                        Rate = this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0][BalanceSystem.AppSchema.StockItem.RATEColumn.ColumnName].ToString());
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

        private int FetchReorderLevel()
        {
            int ReorderLevel = 0;
            try
            {
                using (StockItemSystem stockItemSystem = new StockItemSystem())
                {
                    stockItemSystem.ItemId = ItemId;
                    ReorderLevel = stockItemSystem.FetchStockReorderLevel();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
            return ReorderLevel;
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
                    PurchaseDate = this.UtilityMember.DateSet.ToDate(projectSelection.RecentVoucherDate, false);
                    setDefaults();
                }
            }
        }

        private void ProcessShortcutKeys(KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F5)
                {
                    if (PurchaseId == 0)
                        ShowProjectSelectionWindow();
                }
                else if (e.KeyCode == Keys.F3)
                {
                    dtePurchaseDate.Focus();
                }
                else if (e.KeyData == (Keys.Alt | Keys.D))
                {
                    DeleteTransaction();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void RealColumnEditTransAmount()
        {
            colAmount.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditTransAmount_EditValueChanged);
            this.gvPurchaseItems.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvPurchaseItems.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colAmount)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvPurchaseItems.ShowEditorByMouse();
                    }));
                }
            };
        }

        void RealColumnEditTransAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            int quantity = 0;
            double rate = 0;
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvPurchaseItems.PostEditor();
            gvPurchaseItems.UpdateCurrentRow();
            if (gvPurchaseItems.ActiveEditor == null)
            {
                gvPurchaseItems.ShowEditor();
            }
            TextEdit txtTransAmount = edit as TextEdit;
            int grpCounts = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.DecimalPlaces) + 1);
            if (txtTransAmount.Text.Length > grpCounts && txtTransAmount.SelectionLength == txtTransAmount.Text.Length)
                txtTransAmount.Select(txtTransAmount.Text.Length - grpCounts, 0);
            if (LocatoinId > 0 && ItemId > 0)
            {
                quantity = this.UtilityMember.NumberSet.ToInteger(gvPurchaseItems.GetFocusedRowCellValue(colPurchaseQuantity).ToString());
                rate = this.UtilityMember.NumberSet.ToDouble(gvPurchaseItems.GetFocusedRowCellValue(colUnitPrice).ToString());
                DataTable dtTrans = gcPurchaseItems.DataSource as DataTable;
                int AvailQuantity = GetAvailableQuantity(dtTrans, ItemId, LocatoinId); //ShowLedgerBalance(LedgerId, dtTrans, true);
                if (AvailQuantity > 0) { gvPurchaseItems.SetRowCellValue(gvPurchaseItems.FocusedRowHandle, colStockQuantity, AvailQuantity); }
                lblTotalAmount.Text = PurchaseSummaryVal.ToString();
                lblNetPaymentAmount.Text = PurchaseSummaryVal.ToString();
                NetAmount = UtilityMember.NumberSet.ToDecimal(lblNetPaymentAmount.Text.ToString());
            }
        }

        private int GetAvailableQuantity(DataTable dtTrans, int ItemId, int LocationId)
        {
            int AvailQuantity = 0;
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
            int AvailQuantity = 0;
            try
            {
                int CurrentAvail = FetchAvailableStock(LocatoinId, ItemId);

                if (this.PurchaseId.Equals(0))
                {
                    if (CurrentAvail > 0 && NewQuantity > 0)
                    {
                        AvailQuantity = CurrentAvail + NewQuantity;
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
                        AvailQuantity = CurrentAvail + NewQuantity;
                    }
                }
                else
                {
                    AvailQuantity = CurrentAvail;
                    int TempQuantity = gvPurchaseItems.GetFocusedRowCellValue(colTempQuantity) != null ? this.UtilityMember.NumberSet.ToInteger(gvPurchaseItems.GetFocusedRowCellValue(colTempQuantity).ToString()) : 0;

                    AvailQuantity = AvailQuantity - TempQuantity;
                    AvailQuantity += NewQuantity;

                    //if (TempQuantity > NewQuantity)
                    //{
                    //    AvailQuantity = NewQuantity;
                    //}
                    //else if (NewQuantity > TempQuantity)
                    //{
                    //    AvailQuantity = NewQuantity;
                    //}
                    //else
                    //{
                    //    AvailQuantity = NewQuantity;
                    //}

                    //if ((gcPurchaseItems.DataSource as DataTable) != null && (gcPurchaseItems.DataSource as DataTable).Rows.Count > 0)
                    //{
                    //    DataTable dtUpdateRows = gcPurchaseItems.DataSource as DataTable;
                    //    IEnumerable<DataRow> rows = dtUpdateRows.Rows.Cast<DataRow>().Where(r => r["ITEM_ID"].ToString() == ItemId.ToString() && r["LOCATION_ID"].ToString() == LocatoinId.ToString());
                    //    if (rows.Count() > 0)
                    //    {
                    //        rows.ToList().ForEach(r => r.SetField("AVAIL_QUANTITY", AvailQuantity));
                    //    }
                    //}
                }
                if ((gcPurchaseItems.DataSource as DataTable) != null && (gcPurchaseItems.DataSource as DataTable).Rows.Count > 0)
                {
                    DataTable dtUpdateRows = gcPurchaseItems.DataSource as DataTable;
                    IEnumerable<DataRow> rows = dtUpdateRows.Rows.Cast<DataRow>().Where(r => r["ITEM_ID"].ToString() == ItemId.ToString() && r["LOCATION_ID"].ToString() == LocatoinId.ToString());
                    if (rows.Count() > 0)
                    {
                        rows.ToList().ForEach(r => r.SetField("AVAIL_QUANTITY", AvailQuantity));
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
            return AvailQuantity;
        }

        private string GetUnitofMeasure()
        {
            string untMeasure = string.Empty;
            try
            {
                using (StockPurchaseSalesSystem purchasesalessystem = new StockPurchaseSalesSystem())
                {
                    resultArgs = purchasesalessystem.FetchUnitOfMeasureByItemId(ItemId);
                    if (resultArgs.Success)
                    {
                        untMeasure = resultArgs.DataSource.Sclar.ToString;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
            return untMeasure;
        }

        private void setTitle()
        {
            if ((int)StockPurchaseTransType.Purchase == PurchaseTransType)
            {
                this.Text = (PurchaseId == 0) ? this.GetMessage(MessageCatalog.Stock.StockMasterPurchase.PURCHASE_CAPTION) : this.GetMessage(MessageCatalog.Stock.StockMasterPurchase.PURCHASE_EDIT);
            }
            else
                if ((int)StockPurchaseTransType.Receive == PurchaseTransType)
                {
                    this.Text = (PurchaseId == 0) ? this.GetMessage(MessageCatalog.Stock.StockMasterPurchase.RECEIVES_ADD_CAPTION) : this.GetMessage(MessageCatalog.Stock.StockMasterPurchase.RECEIVES_EDIT_CAPTION);
                }
        }

        private void LoadNameAddressAutoComplete()
        {
            try
            {
                using (StockPurchaseSalesSystem PurchaseSystem = new StockPurchaseSalesSystem())
                {
                    resultArgs = PurchaseSystem.AutoNameAddress();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataView dvNarration = resultArgs.DataSource.Table.AsDataView();
                        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            collection.Add(dr[PurchaseSystem.AppSchema.StockMasterSales.NAME_ADDRESSColumn.ColumnName].ToString());
                        }
                        txtNameandAddress.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        txtNameandAddress.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        txtNameandAddress.MaskBox.AutoCompleteCustomSource = collection;
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
                using (StockPurchaseSalesSystem PurchaseSystem = new StockPurchaseSalesSystem())
                {
                    resultArgs = PurchaseSystem.AutoNarration();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataView dvNarration = resultArgs.DataSource.Table.AsDataView();
                        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            collection.Add(dr[PurchaseSystem.AppSchema.StockMasterSales.NARRATIONColumn.ColumnName].ToString());
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

        private void RealColumnEditQuantity()
        {
            colPurchaseQuantity.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditQuantity_EditValueChanged);
            this.gvPurchaseItems.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvPurchaseItems.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colPurchaseQuantity)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvPurchaseItems.ShowEditorByMouse();
                    }));
                }
            };
        }

        private void RealColumnEditRateAmount()
        {
            colUnitPrice.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditRateAmount_EditValueChanged);
            this.gvPurchaseItems.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvPurchaseItems.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colAmount)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvPurchaseItems.ShowEditorByMouse();
                    }));
                }
            };
        }

        void RealColumnEditQuantity_EditValueChanged(object sender, System.EventArgs e)
        {
            int quantity = 0;
            double rate = 0.00;
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvPurchaseItems.PostEditor();
            gvPurchaseItems.UpdateCurrentRow();
            if (gvPurchaseItems.ActiveEditor == null)
            {
                gvPurchaseItems.ShowEditor();
            }
            int LocationId = gvPurchaseItems.GetFocusedRowCellValue(colLocation) != null ? this.UtilityMember.NumberSet.ToInteger(gvPurchaseItems.GetFocusedRowCellValue(colLocation).ToString()) : 0;
            AvailQuantity = GetAvailableQuantity(gcPurchaseItems.DataSource as DataTable, ItemId, LocationId);
            gvPurchaseItems.SetRowCellValue(gvPurchaseItems.FocusedRowHandle, colStockQuantity, AvailQuantity);
            quantity = this.UtilityMember.NumberSet.ToInteger(gvPurchaseItems.GetFocusedRowCellValue(colPurchaseQuantity).ToString());
            rate = this.UtilityMember.NumberSet.ToDouble(gvPurchaseItems.GetFocusedRowCellValue(colUnitPrice).ToString());
            gvPurchaseItems.SetFocusedRowCellValue(colAmount, quantity * rate);
            lblNetPaymentAmount.Text = UtilityMember.NumberSet.ToCurrency((rate * quantity)).ToString();
            lblTotalAmount.Text = UtilityMember.NumberSet.ToCurrency((rate * quantity)).ToString();
            NetAmount = UtilityMember.NumberSet.ToDecimal((rate * quantity).ToString());
        }

        void RealColumnEditRateAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            int quantity = 0;
            double rate = 0.00;
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvPurchaseItems.PostEditor();
            gvPurchaseItems.UpdateCurrentRow();
            if (gvPurchaseItems.ActiveEditor == null)
            {
                gvPurchaseItems.ShowEditor();
            }
            quantity = this.UtilityMember.NumberSet.ToInteger(gvPurchaseItems.GetFocusedRowCellValue(colPurchaseQuantity).ToString());
            rate = this.UtilityMember.NumberSet.ToDouble(gvPurchaseItems.GetFocusedRowCellValue(colUnitPrice).ToString());
            gvPurchaseItems.SetFocusedRowCellValue(colAmount, quantity * rate);
            lblNetPaymentAmount.Text = UtilityMember.NumberSet.ToCurrency(PurchaseSummaryVal).ToString();
            lblTotalAmount.Text = UtilityMember.NumberSet.ToCurrency(PurchaseSummaryVal).ToString();
            NetAmount = UtilityMember.NumberSet.ToDecimal((rate * quantity).ToString());
        }

        private void GetLedgerIdByName(int itemId)
        {
            using (StockPurchaseDetail purchaseSystem = new StockPurchaseDetail())
            {
                resultArgs = purchaseSystem.FetchLedgerIdByItem(itemId);
                if (resultArgs.Success && resultArgs != null)
                {
                    AccountledgerId = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][purchaseSystem.AppSchema.StockItem.EXPENSE_LEDGER_IDColumn.ColumnName].ToString());
                }
            }
        }

        #endregion

        private void dtePurchaseDate_EditValueChanged(object sender, EventArgs e)
        {
            DateTime date = default(DateTime);
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
            date = dtePurchaseDate.DateTime;
        }

        private void LoadVoucherNo()
        {
            using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
            {
                voucherTransaction.VoucherType = VoucherSubTypes.PY.ToString();
                voucherTransaction.ProjectId = ProjectId;
                voucherTransaction.VoucherDate = this.UtilityMember.DateSet.ToDate(dtePurchaseDate.Text, false);
                txtVoucherNo.Text = voucherTransaction.TempVoucherNo();
            }
        }

        private void txtDiscount_EditValueChanged(object sender, EventArgs e)
        {
            if (UtilityMember.NumberSet.ToDouble(txtDiscount.Text) < PurchaseSummaryVal)
            {
                txtDiscountPer.Text = this.UtilityMember.NumberSet.ToNumber(CommonMethod.CalculatePerCent(PurchaseSummaryVal, this.UtilityMember.NumberSet.ToDouble(txtDiscount.Text))).ToString();

                lblDiscountAmount.Text = this.UtilityMember.NumberSet.ToCurrency(-this.UtilityMember.NumberSet.ToDouble(txtDiscount.Text));

                lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(PurchaseSummaryVal);
                lblNetPaymentAmount.Text = this.UtilityMember.NumberSet.ToCurrency(CommonMethod.calculateAssetNetPayment(PurchaseSummaryVal,
                    this.UtilityMember.NumberSet.ToDouble(txtDiscount.Text),
                   this.UtilityMember.NumberSet.ToDouble(txtTaxAmount.Text), this.UtilityMember.NumberSet.ToDouble(txtOterChargers.Text)));

                NetAmount = this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) > 0 ?
                    this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) : NetAmount;
            }

        }

        private void txtTaxAmount_EditValueChanged(object sender, EventArgs e)
        {
            lblTaxAmount.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(txtTaxAmount.Text));
            txtTax.Text = this.UtilityMember.NumberSet.ToNumber(CommonMethod.CalculatePerCent(PurchaseSummaryVal, this.UtilityMember.NumberSet.ToDouble(txtTaxAmount.Text))).ToString();
            lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(PurchaseSummaryVal);
            lblNetPaymentAmount.Text = this.UtilityMember.NumberSet.ToCurrency(CommonMethod.calculateAssetNetPayment(PurchaseSummaryVal,
            this.UtilityMember.NumberSet.ToDouble(txtDiscount.Text),
            this.UtilityMember.NumberSet.ToDouble(txtTaxAmount.Text), this.UtilityMember.NumberSet.ToDouble(txtOterChargers.Text)));
            NetAmount = this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) > 0 ? this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) : NetAmount;
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtTaxAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtOterChargers_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void RealColumnEditLocation()
        {
            colLocation.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditLocation_EditValueChanged);
            this.gvPurchaseItems.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvPurchaseItems.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colLocation)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvPurchaseItems.ShowEditorByMouse();
                    }));
                }
            };
        }

        void RealColumnEditLocation_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvPurchaseItems.PostEditor();
            gvPurchaseItems.UpdateCurrentRow();
            if (gvPurchaseItems.ActiveEditor == null)
            {
                gvPurchaseItems.ShowEditor();
            }
            if (LocatoinId > 0 && ItemId > 0)
            {
                AvailQuantity = GetAvailableQuantity(gcPurchaseItems.DataSource as DataTable, ItemId, LocatoinId);
                gvPurchaseItems.SetRowCellValue(gvPurchaseItems.FocusedRowHandle, colStockQuantity, AvailQuantity);
                double TempRate = gvPurchaseItems.GetFocusedRowCellValue(colUnitPrice) != null ? this.UtilityMember.NumberSet.ToInteger(gvPurchaseItems.GetFocusedRowCellValue(colUnitPrice).ToString()) : 0;
                if (TempRate == 0)
                {
                    gvPurchaseItems.SetRowCellValue(gvPurchaseItems.FocusedRowHandle, colUnitPrice, Rate);
                }
                else
                {
                    gvPurchaseItems.SetRowCellValue(gvPurchaseItems.FocusedRowHandle, colUnitPrice, TempRate);
                }
                //rtxtPurchaseQuantity.NullText = 0.ToString();
                gvPurchaseItems.SetFocusedRowCellValue(colReorder, FetchReorderLevel());
                string UnitMeasure = GetUnitofMeasure();
                gvPurchaseItems.SetRowCellValue(gvPurchaseItems.FocusedRowHandle, colUnitofMeasure, UnitMeasure);
                GetLedgerIdByName(ItemId);
                gvPurchaseItems.SetRowCellValue(gvPurchaseItems.FocusedRowHandle, colAccountLedgerId, AccountledgerId);
            }
        }

        private void rglkpItems_MouseDown(object sender, MouseEventArgs e)
        {
            //To Identify the Mouse Click Event
            if (e.Button == MouseButtons.Left)
                isMouseClicked = true;
        }

        private void rglkpItems_EditValueChanged(object sender, EventArgs e)
        {
            //To select the Ledger Using Mouse Click
            if (isMouseClicked)
            {
                SendKeys.Send("{tab}"); isMouseClicked = false;
            }
        }

        private void RealColumnEditItem()
        {
            colItems.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditItem_EditValueChanged);
            this.gvPurchaseItems.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvPurchaseItems.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colItemName)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvPurchaseItems.ShowEditorByMouse();
                    }));
                }
            };
        }

        void RealColumnEditItem_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            //  gvPurchaseItems.PostEditor();
            gvPurchaseItems.UpdateCurrentRow();
            if (gvPurchaseItems.ActiveEditor == null)
            {
                gvPurchaseItems.ShowEditor();
            }
            if (ItemId > 0)
            {
                gvPurchaseItems.SetFocusedRowCellValue(colReorder, FetchReorderLevel());
                string UnitMeasure = GetUnitofMeasure();
                gvPurchaseItems.SetRowCellValue(gvPurchaseItems.FocusedRowHandle, colUnitofMeasure, UnitMeasure);
                GetLedgerIdByName(ItemId);
                gvPurchaseItems.SetRowCellValue(gvPurchaseItems.FocusedRowHandle, colAccountLedgerId, AccountledgerId);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtBillNo.Text = txtVoucherNo.Text = txtDiscount.Text = txtTaxAmount.Text = txtTax.Text = txtOterChargers.Text = txtNameandAddress.Text = txtNarration.Text = string.Empty;
            glkpLedger.EditValue = glkpVendor.EditValue = null;
            gcPurchaseItems.DataSource = null;
            lblTaxAmount.Text = lblOtherChargesAmount.Text = lblNetPaymentAmount.Text = lblDiscountAmount.Text = lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble("0.00"));
            ConstructStockPurchase();
            LoadVoucherNo();
        }

        private void txtOterChargers_TextChanged(object sender, EventArgs e)
        {

            lblNetPaymentAmount.Text = this.UtilityMember.NumberSet.ToCurrency(CommonMethod.calculateAssetNetPayment(PurchaseSummaryVal,
               this.UtilityMember.NumberSet.ToDouble(txtDiscount.Text),
              this.UtilityMember.NumberSet.ToDouble(txtTaxAmount.Text), this.UtilityMember.NumberSet.ToDouble(txtOterChargers.Text)));
            lblOtherChargesAmount.Text = txtOterChargers.Text;
        }

        private void dtePurchaseDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                if (txtVoucherNo.Enabled)
                {
                    txtVoucherNo.Focus();
                }
                else
                {
                    FocusTransactionGrid();
                }
                e.SuppressKeyPress = true;
            }
        }

        private void FocusTransactionGrid()
        {
            gcPurchaseItems.Focus();
            gvPurchaseItems.MoveFirst(); //DevExpress.XtraGrid.GridControl.NewItemRowHandle;
            gvPurchaseItems.FocusedColumn = gvPurchaseItems.Columns.ColumnByName(colItemName.Name);
            gvPurchaseItems.ShowEditor();
        }

        private void dtePurchaseDate_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }
    }
}