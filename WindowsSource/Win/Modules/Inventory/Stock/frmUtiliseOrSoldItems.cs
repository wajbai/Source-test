using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Utility;
using Bosco.Model;
using Bosco.Model.Inventory.Stock;
using Bosco.Model.UIModel;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using ACPP.Modules.Transaction;
using Bosco.Model.Transaction;


namespace ACPP.Modules.Inventory.Stock
{
    public partial class frmUtiliseOrSoldItems : frmFinanceBaseAdd
    {
        #region Declaration
        public event EventHandler UpdateHeld;
        ResultArgs resultArgs = new ResultArgs();
        int AccountLedgerId = 0;
        int DisposalLedgerId = 0;
        DataView dv = new DataView();
        DataTable dt = new DataTable();
        DataTable dtSales = new DataTable();
        #endregion

        #region Properties

        public decimal UnitPrice { get; set; }
        public decimal NetAmount { get; set; }
        public int VoucherId { get; set; }

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
        private string salesdate = string.Empty;
        private string SalesDate
        {
            set
            {
                salesdate = value;
            }
            get
            {
                return salesdate;
            }
        }

        private int salesid = 0;
        private int SalesId
        {
            set
            {
                salesid = value;
            }
            get
            {
                return salesid;
            }
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

        private int stocktype = 0;
        private int StockType
        {
            set { stocktype = value; }
            get { return stocktype; }
        }

        private int itemid = 0;
        private int ItemId
        {
            get
            {
                int itemid = 0;
                itemid = gvSoldItems.GetFocusedRowCellValue(colItemId) != null ?
                    this.UtilityMember.NumberSet.ToInteger(gvSoldItems.GetRowCellValue(gvSoldItems.FocusedRowHandle, colItemId).ToString()) : 0;
                return itemid;
            }
            set { itemid = value; }
        }

        public bool isMouseClicked { get; set; }

        private int locationid = 0;
        private int LocationId
        {
            get
            {
                int locationid = 0;
                locationid = gvSoldItems.GetFocusedRowCellValue(colLocationId) != null ?
                    this.UtilityMember.NumberSet.ToInteger(gvSoldItems.GetRowCellValue(gvSoldItems.FocusedRowHandle, colLocationId).ToString()) : 0;
                return locationid;
            }
        }

        private double SalesSummaryVal
        {
            get { return colAmount.SummaryItem.SummaryValue != null ? this.UtilityMember.NumberSet.ToDouble(colAmount.SummaryItem.SummaryValue.ToString()) : 0; }
        }



        private DataTable dtLocation { get; set; }

        #endregion

        #region Constructor
        public frmUtiliseOrSoldItems()
        {
            InitializeComponent();
        }

        public frmUtiliseOrSoldItems(int slsId, string solddisposalDate, int ProjId, string PrjName, int stktype)
            : this()
        {
            SalesId = slsId;
            SalesDate = solddisposalDate;
            ProjectId = ProjId;
            ProjectName = PrjName;
            StockType = stktype;
            //RealColumnEditTransAmount();
            RealColumnEditQuantityAmount();
            RealColumnEditRateAmount();
            RealColumnEditTransAmount();
            RealColumnEditValidateAvailableQuantity();
            RealColumnEditLocation();
        }
        #endregion

        #region Events

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveDetails(e);
        }

        private void SaveDetails(EventArgs e)
        {
            if (IsValidEntry())
            {
                using (StockSalesSystem salessystem = new StockSalesSystem())
                {
                    salessystem.SalesId = SalesId;
                    salessystem.SalesRefNo = txtSalesRefNo.Text.Trim();
                    salessystem.CustomerName = txtReceipientName.Text.Trim();
                    salessystem.SalesDate = this.UtilityMember.DateSet.ToDate(dtDate.Text, false);
                    salessystem.Discount = this.UtilityMember.NumberSet.ToDouble(txtDiscountAmount.Text.Trim());
                    salessystem.DiscountPer = this.UtilityMember.NumberSet.ToDouble(txtDiscountPer.Text.Trim());
                    salessystem.OtherCharges = this.UtilityMember.NumberSet.ToDouble(txtOtherCharges.Text.Trim());
                    salessystem.Tax = this.UtilityMember.NumberSet.ToDouble(txtTaxPercentage.Text.Trim());
                    salessystem.TaxAmount = this.UtilityMember.NumberSet.ToDouble(txtTaxAmount.Text.Trim());
                    salessystem.NetPay = this.UtilityMember.NumberSet.ToDouble(lblNetPay.Text.Remove(0, 2).Trim());
                    salessystem.CashBankLedgerId = glkpLedger.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpLedger.EditValue.ToString()) : 0;
                    salessystem.NameAddress = txtNameAddress.Text.Trim();
                    salessystem.Narration = txtNarration.Text.Trim();
                    salessystem.SalesFlag = StockType;
                    salessystem.VoucherNo = txtVoucherNo.Text.Trim();
                    salessystem.ProjectId = ProjectId;
                    salessystem.VoucherId = VoucherId;
                    salessystem.BranchId = 0;
                    salessystem.dtStockDetails = gcSoldItems.DataSource as DataTable;

                    resultArgs = salessystem.SaveStockSales();
                    if (resultArgs.Success)
                    {
                        clearControls();
                        if (UpdateHeld != null)
                        {
                            UpdateHeld(this, e);
                            ConstructEmptySalesDetails();
                            LoadNameAddressAutoComplete();
                            LoadNarrationAutoComplete();
                        }
                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_SAVE));
                    }
                }
            }
        }



        private void frmUtiliseOrSoldItems_Load(object sender, EventArgs e)
        {
            LoadDefaults();
            setTitle();
            ConstructEmptySalesDetails();
            LoadItems();
            LoadLocation();
            LoadCashbankLedger();
            AssignValues();
            LoadCustomerAutoComplete();
            LoadNameAddressAutoComplete();
            LoadNarrationAutoComplete();
            CommonMethod.NetAmout = CommonMethod.CalCuTaxAmount = 0;

            //glkpLedger.Enabled = (StockType != (int)StockSalesTransType.Utilized) ? true : false;
            ////else
            ////{
            ////    lcgDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            ////}
            //txtDiscountPer.Enabled = txtDiscountAmount.Enabled = txtOtherCharges.Enabled = txtTaxPercentage.Enabled = txtTaxAmount.Enabled = glkpLedger.Enabled;

            //if (StockType.Equals((int)StockSalesTransType.Disposal))
            //{
            //    txtDiscountPer.Enabled = txtDiscountAmount.Enabled = txtOtherCharges.Enabled = txtTaxPercentage.Enabled = txtTaxAmount.Enabled = glkpLedger.Enabled = false;
            //}
            if (StockType == (int)StockSalesTransType.Utilized || StockType == (int)StockSalesTransType.Disposal)
            {
                lcgDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlGroup2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.Height = 561 - 55;
                lyVoucherNo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            if (dtLocation != null && dtLocation.Rows.Count > 0)
            {
                dv = dtLocation.DefaultView;
                dv.RowFilter = "ITEM_ID=" + ItemId;
                dt = dv.ToTable();
                this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpLocatoin, dt, "LOCATION", "LOCATION_ID");
            }
            dtDate.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtDate.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
        }

        private void setTitle()
        {
            if ((int)StockSalesTransType.Disposal == StockType)
            {
                this.Text = SalesId == 0 ? "Dispose (Add)" : "Dispose (Edit)";
            }
            else if ((int)StockSalesTransType.Sold == StockType)
            {
                this.Text = SalesId == 0 ? "Sales (Add)" : "Sales (Edit)";
            }
            else if ((int)StockSalesTransType.Utilized == StockType)
            {
                this.Text = SalesId == 0 ? "Utilise (Add)" : "Utilise (Edit)";
            }
            //this.Text = (StockType == 0) ? "Sales" : (StockType == 1) ? "Utilise" : "Dispose";
        }

        private void gcSoldItems_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control)
                {
                    gvSoldItems.PostEditor();
                    gvSoldItems.UpdateCurrentRow();

                    if (gvSoldItems.FocusedColumn == colItem)
                    {
                        int Itemid = gvSoldItems.GetFocusedRowCellValue(colItem) != null ? this.UtilityMember.NumberSet.ToInteger(gvSoldItems.GetFocusedRowCellValue(colItem).ToString()) : 0;
                        if (string.IsNullOrEmpty(gvSoldItems.GetFocusedRowCellValue(colItem).ToString()))
                        {
                            gvSoldItems.CloseEditor();
                            e.SuppressKeyPress = true;
                            e.Handled = true;
                            if (StockType != (int)StockSalesTransType.Utilized && StockType != (int)StockSalesTransType.Disposal)
                            {
                                txtDiscountPer.Select();
                            }
                            else
                            {
                                txtNameAddress.Select();
                            }
                        }
                        if (ItemId > 0)
                        {
                            string UnitMeasure = GetUnitofMeasure();
                            gvSoldItems.SetRowCellValue(gvSoldItems.FocusedRowHandle, colUnit, UnitMeasure);
                            GetLedgerIdByName(Itemid);
                            gvSoldItems.SetRowCellValue(gvSoldItems.FocusedRowHandle, colAccountLedger, AccountLedgerId);
                            gvSoldItems.SetRowCellValue(gvSoldItems.FocusedRowHandle, colDisposalLedgerId, DisposalLedgerId);
                            if (dtLocation != null && dtLocation.Rows.Count > 0)
                            {
                                dv = dtLocation.DefaultView;
                                dv.RowFilter = "(ITEM_ID=" + ItemId + " AND AVAIL_QUANTITY <> 0)";
                                dt = dv.ToTable();
                                this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpLocatoin, dt, "LOCATION", "LOCATION_ID");
                            }
                        }
                    }
                    //else
                    //{
                    if (gvSoldItems.FocusedColumn == colLocation)
                    {
                        if (ItemId > 0 && LocationId > 0)
                        {
                            //int AvailQuantity = FetchAvailableStock(LocationId, ItemId);
                            int AvailQuantity = GetAvailableQuantity(gcSoldItems.DataSource as DataTable, ItemId, LocationId);
                            gvSoldItems.SetRowCellValue(gvSoldItems.FocusedRowHandle, colAvailQuantity, AvailQuantity);
                            //gvSoldItems.SetRowCellValue(gvSoldItems.FocusedRowHandle, colUnitPrice, UnitPrice);
                            double TempRate = gvSoldItems.GetFocusedRowCellValue(colUnitPrice) != null ? this.UtilityMember.NumberSet.ToInteger(gvSoldItems.GetFocusedRowCellValue(colUnitPrice).ToString()) : 0;
                            if (TempRate == 0)
                            {
                                gvSoldItems.SetRowCellValue(gvSoldItems.FocusedRowHandle, colUnitPrice, UnitPrice);
                            }
                            else
                            {
                                gvSoldItems.SetRowCellValue(gvSoldItems.FocusedRowHandle, colUnitPrice, TempRate);
                            }
                        }
                    }
                    if (gvSoldItems.FocusedColumn == colUnitPrice && gvSoldItems.IsLastRow)
                    {
                        int LocationId = gvSoldItems.GetFocusedRowCellValue(colLocationId) != null ? this.UtilityMember.NumberSet.ToInteger(gvSoldItems.GetFocusedRowCellValue(colLocationId).ToString()) : 0;
                        int ItemId = gvSoldItems.GetFocusedRowCellValue(colItem) != null ? this.UtilityMember.NumberSet.ToInteger(gvSoldItems.GetFocusedRowCellValue(colItem).ToString()) : 0;
                        int Rate = gvSoldItems.GetFocusedRowCellValue(colQuantity) != null ? this.UtilityMember.NumberSet.ToInteger(gvSoldItems.GetFocusedRowCellValue(colQuantity).ToString()) : 0;
                        decimal Quantity = gvSoldItems.GetFocusedRowCellValue(colUnitPrice) != null ? this.UtilityMember.NumberSet.ToDecimal(gvSoldItems.GetFocusedRowCellValue(colUnitPrice).ToString()) : 0;
                        if (ItemId > 0 && Rate > 0 && Quantity > 0 && LocationId > 0)
                        {
                            gvSoldItems.SetRowCellValue(gvSoldItems.FocusedRowHandle, colAmount, Rate * Quantity);
                            setFocusToGridControl();
                            gvSoldItems.AddNewRow();
                            NetAmount = this.UtilityMember.NumberSet.ToDecimal(SalesSummaryVal.ToString());
                            lblNetPay.Text = lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(SalesSummaryVal);
                        }
                        else
                        {
                            gvSoldItems.CloseEditor();
                            e.SuppressKeyPress = true;
                            e.Handled = true;

                            if (StockType != (int)StockSalesTransType.Utilized)
                            {
                                txtDiscountPer.Select();
                            }
                            else
                            {
                                txtNameAddress.Select();
                            }
                        }
                        //}
                        //if (gvSoldItems.FocusedColumn == colAmount && gvSoldItems.IsLastRow)
                        //{
                        //    int ItemId = gvSoldItems.GetFocusedRowCellValue(colItem) != null ? this.UtilityMember.NumberSet.ToInteger(gvSoldItems.GetFocusedRowCellValue(colItem).ToString()) : 0;
                        //    int Rate = gvSoldItems.GetFocusedRowCellValue(colQuantity) != null ? this.UtilityMember.NumberSet.ToInteger(gvSoldItems.GetFocusedRowCellValue(colQuantity).ToString()) : 0;
                        //    decimal Quantity = gvSoldItems.GetFocusedRowCellValue(colUnitPrice) != null ? this.UtilityMember.NumberSet.ToDecimal(gvSoldItems.GetFocusedRowCellValue(colUnitPrice).ToString()) : 0;
                        //    int LocationId = gvSoldItems.GetFocusedRowCellValue(colLocationId) != null ? this.UtilityMember.NumberSet.ToInteger(gvSoldItems.GetFocusedRowCellValue(colLocationId).ToString()) : 0;
                        //    if (ItemId > 0 && Rate > 0 && Quantity > 0 && LocationId > 0)
                        //    {
                        //        setFocusToGridControl();
                        //        gvSoldItems.AddNewRow();
                        //        lblNetPay.Text = lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(SalesSummaryVal);
                        //    }
                        //    else
                        //    {
                        //        gvSoldItems.CloseEditor();
                        //        e.SuppressKeyPress = true;
                        //        e.Handled = true;

                        //        if (StockType != (int)StockSalesTransType.Utilized)
                        //        {
                        //            txtDiscountPer.Select();
                        //        }
                        //        else
                        //        {
                        //            txtNameAddress.Select();
                        //        }
                        //    }
                        //}
                    }
                }
                else if (gvSoldItems.IsFirstRow && gvSoldItems.FocusedColumn == colItem && e.Shift && e.KeyCode == Keys.Tab)
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

        private void rbtnDelete_Click(object sender, EventArgs e)
        {
            DeleteTransaction();
        }

        private void dtDate_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForDateTimeEdit(dtDate);
        }

        private void txtSalesRefNo_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtSalesRefNo);
        }

        private void txtReceipientName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtReceipientName);
        }

        private void txtVoucherNo_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtVoucherNo);
        }

        private void glkpLedger_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpLedger);
        }

        private void gcSoldItems_Enter(object sender, EventArgs e)
        {
            gvSoldItems.OptionsSelection.EnableAppearanceFocusedCell = true;
        }

        private void gcSoldItems_Leave(object sender, EventArgs e)
        {
            gvSoldItems.OptionsSelection.EnableAppearanceFocusedCell = false;
        }

        private void frmUtiliseOrSoldItems_KeyDown(object sender, KeyEventArgs e)
        {
            ProcessShortcutKeys(e);
        }

        private void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveDetails(e);
        }

        private void bbiProject_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.SalesId == 0)
            {
                ShowProjectSelectionWindow();
            }
        }

        private void bbiDate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dtDate.Focus();
        }

        private void bbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void txtDiscountAmount_Leave(object sender, EventArgs e)
        {
            //txtDiscountPer.Text = this.UtilityMember.NumberSet.ToNumber(CommonMethod.CalculatePerCent(SalesSummaryVal, this.UtilityMember.NumberSet.ToDouble(txtDiscountAmount.Text))).ToString();

            //lblDiscount.Text = this.UtilityMember.NumberSet.ToCurrency(-this.UtilityMember.NumberSet.ToDouble(txtDiscountAmount.Text));

            //lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(SalesSummaryVal);
            //lblNetPay.Text = this.UtilityMember.NumberSet.ToCurrency(CommonMethod.calculateAssetNetPayment(SalesSummaryVal,
            //    this.UtilityMember.NumberSet.ToDouble(txtDiscountAmount.Text),
            //   this.UtilityMember.NumberSet.ToDouble(txtTaxAmount.Text), this.UtilityMember.NumberSet.ToDouble(txtOtherCharges.Text)));

            //NetAmount = this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) > 0 ?
            //    this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) : NetAmount;
        }

        private void txtDiscountPer_Leave(object sender, EventArgs e)
        {
            lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(SalesSummaryVal);
            txtDiscountAmount.Text = this.UtilityMember.NumberSet.ToNumber(CommonMethod.CalculateAmount(SalesSummaryVal, this.UtilityMember.NumberSet.ToDouble(txtDiscountPer.Text))).ToString();

            lblNetPay.Text = this.UtilityMember.NumberSet.ToCurrency(CommonMethod.calculateAssetNetPayment(SalesSummaryVal,
                this.UtilityMember.NumberSet.ToDouble(txtDiscountAmount.Text),
               this.UtilityMember.NumberSet.ToDouble(txtTaxAmount.Text), this.UtilityMember.NumberSet.ToDouble(txtOtherCharges.Text)));

            NetAmount = this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) > 0 ?
                this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) : NetAmount;
        }

        private void txtTaxPercentage_Leave(object sender, EventArgs e)
        {
            lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(SalesSummaryVal);
            txtTaxAmount.Text = this.UtilityMember.NumberSet.ToNumber(CommonMethod.CalculateAmount(SalesSummaryVal,
                this.UtilityMember.NumberSet.ToDouble(txtTaxPercentage.Text))).ToString();

            this.UtilityMember.NumberSet.ToCurrency(CommonMethod.calculateAssetNetPayment(SalesSummaryVal,
               this.UtilityMember.NumberSet.ToDouble(txtDiscountAmount.Text),
              this.UtilityMember.NumberSet.ToDouble(txtTaxAmount.Text), this.UtilityMember.NumberSet.ToDouble(txtOtherCharges.Text)));

            NetAmount = this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) > 0 ?
                this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) : NetAmount;
        }

        private void txtTaxAmount_Leave(object sender, EventArgs e)
        {
            //            lblTaxAmount.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(txtTaxAmount.Text));
            //            txtTaxPercentage.Text = this.UtilityMember.NumberSet.ToNumber(CommonMethod.CalculatePerCent(SalesSummaryVal, this.UtilityMember.NumberSet.ToDouble(txtTaxAmount.Text))).ToString();

            //            lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(SalesSummaryVal);
            //            lblNetPay.Text = this.UtilityMember.NumberSet.ToCurrency(CommonMethod.calculateAssetNetPayment(SalesSummaryVal,
            //               this.UtilityMember.NumberSet.ToDouble(txtDiscountAmount.Text),
            //              this.UtilityMember.NumberSet.ToDouble(txtTaxAmount.Text), this.UtilityMember.NumberSet.ToDouble(txtOtherCharges.Text)));

            //            NetAmount = this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) > 0 ? this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) :
            //NetAmount;
        }

        private void txtOtherCharges_Leave(object sender, EventArgs e)
        {
            lblOtherCharges.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(txtOtherCharges.Text));

            lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(SalesSummaryVal);
            lblNetPay.Text = this.UtilityMember.NumberSet.ToCurrency(CommonMethod.calculateAssetNetPayment(SalesSummaryVal,
               this.UtilityMember.NumberSet.ToDouble(txtDiscountAmount.Text),
              this.UtilityMember.NumberSet.ToDouble(txtTaxAmount.Text), this.UtilityMember.NumberSet.ToDouble(txtOtherCharges.Text)));

            NetAmount = this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) > 0 ?
                this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) : NetAmount;
        }

        #endregion

        #region Methods

        private void RealColumnEditLocation()
        {
            colLocation.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditLocation_EditValueChanged);
            this.gvSoldItems.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvSoldItems.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colLocation)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvSoldItems.ShowEditorByMouse();
                    }));
                }
            };
        }

        void RealColumnEditLocation_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvSoldItems.PostEditor();
            gvSoldItems.UpdateCurrentRow();
            if (gvSoldItems.ActiveEditor == null)
            {
                gvSoldItems.ShowEditor();
            }
            TextEdit txtTransAmount = edit as TextEdit;
            int grpCounts = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.DecimalPlaces) + 1);
            if (txtTransAmount.Text.Length > grpCounts && txtTransAmount.SelectionLength == txtTransAmount.Text.Length)
                txtTransAmount.Select(txtTransAmount.Text.Length - grpCounts, 0);
            if (LocationId > 0 && ItemId > 0)
            {
                DataTable dtTrans = gcSoldItems.DataSource as DataTable;
                int AvailQuantity = GetAvailableQuantity(dtTrans, ItemId, LocationId);
                gvSoldItems.SetRowCellValue(gvSoldItems.FocusedRowHandle, colAvailQuantity, AvailQuantity);
                double TempRate = gvSoldItems.GetFocusedRowCellValue(colUnitPrice) != null ? this.UtilityMember.NumberSet.ToInteger(gvSoldItems.GetFocusedRowCellValue(colUnitPrice).ToString()) : 0;
                if (TempRate == 0)
                {
                    gvSoldItems.SetRowCellValue(gvSoldItems.FocusedRowHandle, colUnitPrice, UnitPrice);
                }
                else
                {
                    gvSoldItems.SetRowCellValue(gvSoldItems.FocusedRowHandle, colUnitPrice, TempRate);
                }
            }
        }

        private void RealColumnEditTransAmount()
        {
            colQuantity.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditTransAmount_EditValueChanged);
            this.gvSoldItems.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvSoldItems.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colAmount)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvSoldItems.ShowEditorByMouse();
                    }));
                }
            };
        }

        private void RealColumnEditValidateAvailableQuantity()
        {
            colQuantity.RealColumnEdit.EditValueChanged += new EventHandler(RealColumnEdit_EditValueChanged);
            this.gvSoldItems.MouseDown += (object sender, MouseEventArgs e) =>
            {
                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hitInfo = gvSoldItems.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colQuantity)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvSoldItems.ShowEditorByMouse();
                    }));
                }
            };
        }

        void RealColumnEditTransAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvSoldItems.PostEditor();
            gvSoldItems.UpdateCurrentRow();
            if (gvSoldItems.ActiveEditor == null)
            {
                gvSoldItems.ShowEditor();
            }
            TextEdit txtTransAmount = edit as TextEdit;
            int grpCounts = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.DecimalPlaces) + 1);
            if (txtTransAmount.Text.Length > grpCounts && txtTransAmount.SelectionLength == txtTransAmount.Text.Length)
                txtTransAmount.Select(txtTransAmount.Text.Length - grpCounts, 0);
            if (LocationId > 0 && ItemId > 0)
            {
                DataTable dtTrans = gcSoldItems.DataSource as DataTable;
                int AvailQuantity = GetAvailableQuantity(dtTrans, ItemId, LocationId);
                gvSoldItems.SetRowCellValue(gvSoldItems.FocusedRowHandle, colAvailQuantity, AvailQuantity);
            }
        }

        void RealColumnEdit_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvSoldItems.PostEditor();
            gvSoldItems.UpdateCurrentRow();
            if (gvSoldItems.ActiveEditor == null)
            {
                gvSoldItems.ShowEditor();
            }
            if (gvSoldItems.GetFocusedRowCellValue(colAvailQuantity) != null && gvSoldItems.GetFocusedRowCellValue(colQuantity) != null)
            {
                int EnteredQuantity = GetCalculatedQuantity(LocationId, ItemId, gcSoldItems.DataSource as DataTable);
                int AvailableQuantity = FetchAvailableStock(LocationId, ItemId);

                if (EnteredQuantity > AvailableQuantity)
                {
                    ShowMessageBox(GetMessage(MessageCatalog.Stock.StokItemTransfer.STOCK_QUANTITY_EXCEEDS));
                    gvSoldItems.SetFocusedRowCellValue(colQuantity, 0);
                    gvSoldItems.FocusedColumn = colQuantity;
                }
                int Quantity = GetAvailableQuantity(gcSoldItems.DataSource as DataTable, ItemId, LocationId);
                gvSoldItems.SetFocusedRowCellValue(colAvailQuantity, Quantity);
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
                int CurrentAvail = FetchAvailableStock(LocationId, ItemId);

                if (this.SalesId.Equals(0))
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
                    //AvailQuantity = NewQuantity;
                    //if ((gcSoldItems.DataSource as DataTable) != null && (gcSoldItems.DataSource as DataTable).Rows.Count > 0)
                    //{
                    //    DataTable dtUpdateRows = gcSoldItems.DataSource as DataTable;
                    //    IEnumerable<DataRow> rows = dtUpdateRows.Rows.Cast<DataRow>().Where(r => r["ITEM_ID"].ToString() == ItemId.ToString() && r["LOCATION_ID"].ToString() == LocationId.ToString());
                    //    if (rows.Count() > 0)
                    //    {
                    //        rows.ToList().ForEach(r => r.SetField("AVAIL_QUANTITY", AvailQuantity));
                    //    }
                    //}

                    AvailQuantity = CurrentAvail;
                    int TempQuantity = gvSoldItems.GetFocusedRowCellValue(colTempQuantity) != null ? this.UtilityMember.NumberSet.ToInteger(gvSoldItems.GetFocusedRowCellValue(colTempQuantity).ToString()) : 0;

                    AvailQuantity = AvailQuantity + TempQuantity;
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

        public void LoadDefaults()
        {
            dtDate.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtDate.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            dtDate.DateTime = this.UtilityMember.DateSet.ToDate(SalesDate, false);
            uccpProject.Caption = projectName;
        }

        private void ConstructEmptySalesDetails()
        {
            DataTable dtsalesDetail = new DataTable();
            dtsalesDetail.Columns.Add("ITEM_ID", typeof(Int32));
            dtsalesDetail.Columns.Add("LOCATION_ID", typeof(Int32));
            dtsalesDetail.Columns.Add("QUANTITY", typeof(decimal));
            dtsalesDetail.Columns.Add("UNIT_PRICE", typeof(decimal));
            dtsalesDetail.Columns.Add("AMOUNT", typeof(decimal));
            dtsalesDetail.Columns.Add("AVAIL_QUANTITY", typeof(decimal));
            dtsalesDetail.Columns.Add("TEMP_QUANTITY", typeof(decimal));
            dtsalesDetail.Columns.Add("SYMBOL", typeof(string));
            dtsalesDetail.Columns.Add("DISPOSAL_LEDGER_ID", typeof(Int32));
            dtsalesDetail.Columns.Add("ACCOUNT_LEDGER_ID", typeof(Int32));
            dtsalesDetail.Rows.Add(dtsalesDetail.NewRow());
            gcSoldItems.DataSource = dtsalesDetail;
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
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpLocatoin, resultArgs.DataSource.Table, locationSystem.AppSchema.ASSETLocationDetails.LOCATIONColumn.ColumnName, locationSystem.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName);
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
        //        using (StockPurchaseSalesSystem salesSystem = new StockPurchaseSalesSystem())
        //        {
        //            resultArgs = salesSystem.FetchStockItemLocationDetails();
        //            if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
        //            {
        //                dtLocation = resultArgs.DataSource.Table;
        //                //  this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpLocatoin, resultArgs.DataSource.Table,
        //                //  salesSystem.AppSchema.ASSETLocationDetails.LOCATIONColumn.ColumnName, salesSystem.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName);

        //                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
        //                    this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpLocatoin, resultArgs.DataSource.Table, salesSystem.AppSchema.ASSETLocationDetails.LOCATIONColumn.ColumnName, salesSystem.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName);
        //                //  this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpLocatoin, resultArgs.DataSource.Table, "LOCATION", "LOCATION_ID");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
        //    }
        //    finally { }
        //}

        private void LoadItems()
        {
            try
            {
                using (StockPurchaseSalesSystem salesSystem = new StockPurchaseSalesSystem())
                {
                    resultArgs = salesSystem.FetchStockItemDetails();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpItems, resultArgs.DataSource.Table,
                          salesSystem.AppSchema.StockPurchaseSalesDetails.ITEM_NAMEColumn.ColumnName, salesSystem.AppSchema.StockPurchaseSalesDetails.ITEM_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void LoadCashbankLedger()
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    ledgerSystem.ProjectId = ProjectId;
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

        private bool IsValidEntry()
        {
            bool isValid = true;
            DataTable dttransSource = gcSoldItems.DataSource as DataTable;
            if (!IsValidTransactionDate())
            {
                dtDate.DateTime = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom.ToString(), false);
                isValid = false;
            }
            else if (string.IsNullOrEmpty(dtDate.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_TRANSACTION_DATE));
                isValid = false;
                this.SetBorderColorForDateTimeEdit(dtDate);
                dtDate.Focus();
            }
            else if (UtilityMember.NumberSet.ToDouble(txtDiscountAmount.Text) > SalesSummaryVal)
            {
                this.ShowMessageBoxError(this.GetMessage(MessageCatalog.Stock.StockMasterPurchase.DISCOUNT_AMOUNT_EXCEEDS));
                isValid = false;
                this.SetBorderColor(txtDiscountAmount);
                txtDiscountAmount.Focus();
                txtDiscountAmount.Text = string.Empty;
            }
            else if (UtilityMember.NumberSet.ToDouble(txtTaxAmount.Text) > SalesSummaryVal)
            {
                this.ShowMessageBoxError(this.GetMessage(MessageCatalog.Stock.StockMasterPurchase.TAX_AMOUNT_EXCEEDS));
                isValid = false;
                this.SetBorderColor(txtDiscountAmount);
                txtDiscountAmount.Focus();
                txtDiscountAmount.Text = string.Empty;
            }
            else if (string.IsNullOrEmpty(txtSalesRefNo.Text))
            {
                this.ShowMessageBox("Reference No is empty.");
                isValid = false;
                this.SetBorderColor(txtSalesRefNo);
                txtSalesRefNo.Focus();
            }
            else if (string.IsNullOrEmpty(txtReceipientName.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Stock.StockSales.RECIPIENT_NAME_EMPTY));
                isValid = false;
                this.SetBorderColor(txtReceipientName);
                txtReceipientName.Focus();
            }
            //else if (string.IsNullOrEmpty(txtVoucherNo.Text))
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_NUMBER_EMPTY));
            //    isValid = false;
            //    this.SetBorderColor(txtVoucherNo);
            //    txtVoucherNo.Focus();
            //}
            else if (gvSoldItems.RowCount == 0)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Stock.StockSales.SALES_GRID_EMPTY));
                isValid = false;
                gcSoldItems.Focus();
            }
            else if (!string.IsNullOrEmpty(txtTaxPercentage.Text) && string.IsNullOrEmpty(txtTaxAmount.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Stock.StockSales.SALES_GRID_EMPTY));
                isValid = false;
                gcSoldItems.Focus();
            }
            else if (glkpLedger.EditValue == null || this.UtilityMember.NumberSet.ToInteger(glkpLedger.EditValue.ToString()) < 0)
            {
                if (!StockType.Equals(1) && !StockType.Equals(2))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Stock.StockPurcahseReturns.STOCK_LEDGER_EMPTY));
                    isValid = false;
                    this.SetBorderColorForGridLookUpEdit(glkpLedger);
                    glkpLedger.Focus();
                }
            }
            else if (!IsValidItemDetailsGrid())
            {
                isValid = false;
            }
            return isValid;
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
            dtRecentVoucherDate = UtilityMember.DateSet.ToDate(SalesDate.ToString(), false);

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

        private void AssignValues()
        {
            if (SalesId > 0)
            {
                using (StockSalesSystem salessystem = new StockSalesSystem(SalesId))
                {
                    ItemId = salessystem.ItemId;
                    dtDate.DateTime = salessystem.SalesDate;
                    txtSalesRefNo.Text = salessystem.SalesRefNo;
                    txtReceipientName.Text = salessystem.CustomerName;
                    txtDiscountAmount.Text = salessystem.Discount.ToString();
                    txtDiscountPer.Text = salessystem.DiscountPer.ToString();
                    txtOtherCharges.Text = salessystem.OtherCharges.ToString();
                    txtTaxPercentage.Text = salessystem.Tax.ToString();
                    txtTaxAmount.Text = salessystem.TaxAmount.ToString();
                    txtNameAddress.Text = salessystem.NameAddress;
                    txtNarration.Text = salessystem.Narration;
                    glkpLedger.EditValue = salessystem.CashBankLedgerId;
                    lblNetPay.Text = this.UtilityMember.NumberSet.ToNumber(salessystem.NetPay).ToString();
                    txtVoucherNo.Text = salessystem.VoucherNo;
                    VoucherId = salessystem.VoucherId;

                    gcSoldItems.DataSource = salessystem.dtStockDetails;

                    lblNetPay.Text = this.UtilityMember.NumberSet.ToCurrency(salessystem.NetPay);
                    lblDiscount.Text = this.UtilityMember.NumberSet.ToCurrency(-salessystem.Discount).ToString();
                    lblTaxAmount.Text = this.UtilityMember.NumberSet.ToCurrency(salessystem.TaxAmount).ToString();
                    lblOtherCharges.Text = this.UtilityMember.NumberSet.ToCurrency(salessystem.OtherCharges).ToString();
                    lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(SalesSummaryVal);

                    int AvailQuantity = GetAvailableQuantity(gcSoldItems.DataSource as DataTable, ItemId, LocationId);
                    gvSoldItems.SetRowCellValue(gvSoldItems.FocusedRowHandle, colAvailQuantity, AvailQuantity);

                }
            }
        }


        private bool IsValidItemDetailsGrid()
        {
            DataTable dtTrans = gcSoldItems.DataSource as DataTable;

            int ItmId = 0;
            int LocId = 0;
            decimal Amt = 0;
            decimal qty = 0;
            decimal rate = 0;
            bool isValid = false;
            int RowPosition = 0;

            string validateMessage = "Required Information not filled"; //, Stock Details are not filled fully";

            DataView dv = new DataView(dtTrans);
            dv.RowFilter = "(ITEM_ID>0 OR LOCATION_ID>0 OR QUANTITY>0 OR UNIT_PRICE>0 OR AMOUNT>0)";
            gvSoldItems.FocusedColumn = colItem;
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
                            gvSoldItems.FocusedColumn = colItem;
                        }
                        else if (LocId == 0)
                        {
                            validateMessage = "Required Information not filled, Location is empty";
                            gvSoldItems.FocusedColumn = colLocation;
                        }
                        else if (qty == 0)
                        {
                            validateMessage = "Required Information not filled, Quantity is empty";
                            gvSoldItems.FocusedColumn = colQuantity;
                        }
                        else if (rate == 0)
                        {
                            validateMessage = "Required Information not filled, Unit Price is empty";
                            gvSoldItems.FocusedColumn = colUnitPrice;
                        }
                        else if (Amt == 0)
                        {
                            validateMessage = "Required Information not filled, Amount is empty";
                            gvSoldItems.FocusedColumn = colAmount;
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
                gvSoldItems.CloseEditor();
                gvSoldItems.FocusedRowHandle = gvSoldItems.GetRowHandle(RowPosition);
                gvSoldItems.ShowEditor();
            }

            return isValid;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.Alt | Keys.D))
            {
                DeleteTransaction();
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        private void DeleteTransaction()
        {
            try
            {
                dtSales = gcSoldItems.DataSource as DataTable;
                int itemId = gvSoldItems.GetFocusedRowCellValue(colItemId) != null ? this.UtilityMember.NumberSet.ToInteger(gvSoldItems.GetFocusedRowCellValue(colItemId).ToString()) : 0;
                Decimal Amount = gvSoldItems.GetFocusedRowCellValue(colAmount) != null ? this.UtilityMember.NumberSet.ToDecimal(gvSoldItems.GetFocusedRowCellValue(colAmount).ToString()) : 0;
                if (gvSoldItems.RowCount > 1)
                {
                    if (VoucherId > 0)
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            gvSoldItems.DeleteRow(gvSoldItems.FocusedRowHandle);
                            gvSoldItems.FocusedColumn = gvSoldItems.Columns.ColumnByName(colItemName.Name);
                        }
                    }
                    else
                    {
                        if (dtSales != null)
                        {
                            if (ItemId > 0 || Amount > 0)
                            {
                                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    gvSoldItems.DeleteRow(gvSoldItems.FocusedRowHandle);
                                    gvSoldItems.FocusedColumn = gvSoldItems.Columns.ColumnByName(colItemName.Name);
                                }
                            }
                            else
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Bank.BANK_DELETE));
                            }
                        }
                    }
                }
                else if (gvSoldItems.RowCount == 1)
                {
                    if (ItemId > 0 || Amount > 0)
                    {
                        if (VoucherId > 0)
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                ConstructEmptySalesDetails();
                                gvSoldItems.FocusedColumn = gvSoldItems.Columns.ColumnByName(colItemName.Name);
                                //int sourceId = (rgTransactionType.SelectedIndex == 0 || rgTransactionType.SelectedIndex == 2) ? (int)Source.To : (int)Source.By;
                                //gvPurchaseItems.SetRowCellValue(gvTransaction.FocusedRowHandle, colSource, sourceId);
                            }
                        }
                        else
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                gvSoldItems.DeleteRow(gvSoldItems.FocusedRowHandle);
                                ConstructEmptySalesDetails();
                                gvSoldItems.FocusedColumn = gvSoldItems.Columns.ColumnByName(colItemName.Name);
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



        public void LoadOtherAmount()
        {
            lblNetPay.Text = this.UtilityMember.NumberSet.ToCurrency(CommonMethod.calculateStockNetPayment(SalesSummaryVal, this.UtilityMember.NumberSet.ToDouble(txtDiscountPer.Text), this.UtilityMember.NumberSet.ToDouble(txtOtherCharges.Text), this.UtilityMember.NumberSet.ToDouble(txtTaxPercentage.Text)));
            txtTaxAmount.Text = this.UtilityMember.NumberSet.ToNumber(CommonMethod.CalCuTaxAmount).ToString();

            lblDiscount.Text = this.UtilityMember.NumberSet.ToCurrency(-this.UtilityMember.NumberSet.ToDouble(txtDiscountPer.Text)).ToString();

            lblTaxAmount.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(txtTaxAmount.Text)).ToString();
            lblOtherCharges.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(txtOtherCharges.Text)).ToString();
            lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(SalesSummaryVal);

        }

        private void setFocusToGridControl()
        {
            gvSoldItems.Focus();
            gvSoldItems.SelectAll();
            gvSoldItems.FocusedColumn = colDelete;
        }

        private void clearControls()
        {
            if (SalesId == 0)
            {
                txtReceipientName.Text = txtSalesRefNo.Text = txtVoucherNo.Text = txtDiscountPer.Text = txtTaxAmount.Text =
                txtTaxAmount.Text = txtTaxPercentage.Text = txtOtherCharges.Text = txtNameAddress.Text = txtNarration.Text = string.Empty;
                glkpLedger.EditValue = null;
                gcSoldItems.DataSource = null;
                lblNetPay.Text = this.UtilityMember.NumberSet.ToCurrency(0.00);
                lblDiscount.Text = lblTotalAmount.Text = lblTaxAmount.Text = lblOtherCharges.Text = this.UtilityMember.NumberSet.ToCurrency(0.00);
                ConstructEmptySalesDetails();
                LoadVoucherNo();
                dtDate.Focus();
            }
            else
            {
                this.Close();
            }
        }

        private int FetchAvailableStock(int LocationId, int ItemId)
        {
            int AvaliableStock = 0;
            try
            {
                using (StockBalanceSystem BalanceSystem = new StockBalanceSystem())
                {
                    resultArgs = BalanceSystem.GetCurrentBalance(this.ProjectId, ItemId, LocationId);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        //  AvaliableStock = resultArgs.DataSource.Sclar.ToInteger;
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

        private void ProcessShortcutKeys(KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == (Keys.F3))
                {
                    dtDate.Focus();
                }

                if (e.KeyCode == (Keys.F5))
                {
                    if (SalesId == 0)
                    {
                        ShowProjectSelectionWindow();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
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
                    SalesDate = projectSelection.RecentVoucherDate;
                    LoadDefaults();
                    LoadCashbankLedger();
                }
            }
        }

        #endregion

        private void glkpLedger_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                ACPP.Modules.Master.frmLedgerDetailAdd frmBank = new ACPP.Modules.Master.frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.BK, ProjectId);
                frmBank.ShowDialog();
                LoadCashbankLedger();
            }
        }

        private void LoadCustomerAutoComplete()
        {
            try
            {
                using (StockSalesSystem SalesSystem = new StockSalesSystem())
                {
                    resultArgs = SalesSystem.AutoCustomerName();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataView dvNarration = resultArgs.DataSource.Table.AsDataView();
                        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            collection.Add(dr[SalesSystem.AppSchema.StockMasterSales.CUSTOMER_NAMEColumn.ColumnName].ToString());
                        }
                        txtReceipientName.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        txtReceipientName.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        txtReceipientName.MaskBox.AutoCompleteCustomSource = collection;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void LoadNameAddressAutoComplete()
        {
            try
            {
                using (StockSalesSystem SalesSystem = new StockSalesSystem())
                {
                    resultArgs = SalesSystem.AutoNameAddress();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataView dvNarration = resultArgs.DataSource.Table.AsDataView();
                        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            collection.Add(dr[SalesSystem.AppSchema.StockMasterSales.NAME_ADDRESSColumn.ColumnName].ToString());
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
                using (StockSalesSystem SalesSystem = new StockSalesSystem())
                {
                    resultArgs = SalesSystem.AutoNarration();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataView dvNarration = resultArgs.DataSource.Table.AsDataView();
                        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            collection.Add(dr[SalesSystem.AppSchema.StockMasterSales.NARRATIONColumn.ColumnName].ToString());
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

        private void RealColumnEditQuantityAmount()
        {
            colQuantity.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditQuantityAmount_EditValueChanged);
            this.gvSoldItems.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvSoldItems.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colAmount)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvSoldItems.ShowEditorByMouse();
                    }));
                }
            };
        }

        private void RealColumnEditRateAmount()
        {
            colUnitPrice.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditRateAmount_EditValueChanged);
            this.gvSoldItems.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvSoldItems.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colAmount)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvSoldItems.ShowEditorByMouse();
                    }));
                }
            };
        }

        void RealColumnEditQuantityAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            int quantity = 0;
            double rate = 0.00;
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvSoldItems.PostEditor();
            gvSoldItems.UpdateCurrentRow();
            if (gvSoldItems.ActiveEditor == null)
            {
                gvSoldItems.ShowEditor();
            }
            quantity = this.UtilityMember.NumberSet.ToInteger(gvSoldItems.GetFocusedRowCellValue(colQuantity).ToString());
            rate = this.UtilityMember.NumberSet.ToDouble(gvSoldItems.GetFocusedRowCellValue(colUnitPrice).ToString());
            gvSoldItems.SetFocusedRowCellValue(colAmount, quantity * rate);
            lblNetPay.Text = UtilityMember.NumberSet.ToCurrency((quantity * rate)).ToString();
            lblTotalAmount.Text = UtilityMember.NumberSet.ToCurrency((quantity * rate)).ToString();
        }

        void RealColumnEditRateAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            int quantity = 0;
            double rate = 0.00;
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvSoldItems.PostEditor();
            gvSoldItems.UpdateCurrentRow();
            if (gvSoldItems.ActiveEditor == null)
            {
                gvSoldItems.ShowEditor();
            }
            quantity = this.UtilityMember.NumberSet.ToInteger(gvSoldItems.GetFocusedRowCellValue(colQuantity).ToString());
            rate = this.UtilityMember.NumberSet.ToDouble(gvSoldItems.GetFocusedRowCellValue(colUnitPrice).ToString());
            gvSoldItems.SetFocusedRowCellValue(colAmount, quantity * rate);
            lblNetPay.Text = this.UtilityMember.NumberSet.ToCurrency((quantity * rate)).ToString();
            lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency((quantity * rate)).ToString();
        }

        private void GetLedgerIdByName(int itemId)
        {
            using (StockPurchaseDetail purchaseSystem = new StockPurchaseDetail())
            {
                resultArgs = purchaseSystem.FetchLedgerIdByItem(itemId);
                if (resultArgs.Success && resultArgs != null)
                {
                    DisposalLedgerId = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][purchaseSystem.AppSchema.StockItem.EXPENSE_LEDGER_IDColumn.ColumnName].ToString());
                    AccountLedgerId = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][purchaseSystem.AppSchema.StockItem.INCOME_LEDGER_IDColumn.ColumnName].ToString());
                }
            }
        }

        private void LoadVoucherNo()
        {
            using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
            {
                voucherTransaction.VoucherType = VoucherSubTypes.RC.ToString();
                voucherTransaction.ProjectId = ProjectId;
                voucherTransaction.VoucherDate = this.UtilityMember.DateSet.ToDate(dtDate.Text, false);
                txtVoucherNo.Text = voucherTransaction.TempVoucherNo();
            }
        }

        private void dtDate_EditValueChanged(object sender, EventArgs e)
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

        private void txtDiscountAmount_EditValueChanged(object sender, EventArgs e)
        {
            txtDiscountPer.Text = this.UtilityMember.NumberSet.ToNumber(CommonMethod.CalculatePerCent(SalesSummaryVal, this.UtilityMember.NumberSet.ToDouble(txtDiscountAmount.Text))).ToString();

            lblDiscount.Text = this.UtilityMember.NumberSet.ToCurrency(-this.UtilityMember.NumberSet.ToDouble(txtDiscountAmount.Text));

            lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(SalesSummaryVal);
            lblNetPay.Text = this.UtilityMember.NumberSet.ToCurrency(CommonMethod.calculateAssetNetPayment(SalesSummaryVal,
                this.UtilityMember.NumberSet.ToDouble(txtDiscountAmount.Text),
               this.UtilityMember.NumberSet.ToDouble(txtTaxAmount.Text), this.UtilityMember.NumberSet.ToDouble(txtOtherCharges.Text)));

            NetAmount = this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) > 0 ?
                this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) : NetAmount;
        }

        private void txtTaxAmount_EditValueChanged(object sender, EventArgs e)
        {
            lblTaxAmount.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(txtTaxAmount.Text));
            txtTaxPercentage.Text = this.UtilityMember.NumberSet.ToNumber(CommonMethod.CalculatePerCent(SalesSummaryVal, this.UtilityMember.NumberSet.ToDouble(txtTaxAmount.Text))).ToString();

            lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(SalesSummaryVal);
            lblNetPay.Text = this.UtilityMember.NumberSet.ToCurrency(CommonMethod.calculateAssetNetPayment(SalesSummaryVal,
               this.UtilityMember.NumberSet.ToDouble(txtDiscountAmount.Text),
              this.UtilityMember.NumberSet.ToDouble(txtTaxAmount.Text), this.UtilityMember.NumberSet.ToDouble(txtOtherCharges.Text)));

            NetAmount = this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) > 0 ? this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) :
NetAmount;
        }

        private void txtDiscountAmount_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtOtherCharges_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void bbiItemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteTransaction();
        }

        private void txtOtherCharges_TextChanged(object sender, EventArgs e)
        {
            lblOtherCharges.Text = txtOtherCharges.Text;
            Decimal amount = UtilityMember.NumberSet.ToDecimal(lblNetPay.Text) + UtilityMember.NumberSet.ToDecimal(txtOtherCharges.Text);
            lblNetPay.Text = amount.ToString();
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

        private void gcSoldItems_Click(object sender, EventArgs e)
        {

        }
    }
}