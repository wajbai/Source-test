using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Bosco.Utility;
using Bosco.Model.UIModel;
using DevExpress.XtraEditors;
using Bosco.Model;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using ACPP.Modules.Inventory;
using Bosco.Model.Transaction;
using ACPP.Modules.Transaction;
using Bosco.Model.Inventory;

namespace ACPP.Modules.Asset
{
    public partial class frmPurchaseVoucherAdd : frmBaseAdd
    {

        #region Declaration
        public event EventHandler UpdateHeld;
        ResultArgs resultArgs = new ResultArgs();
        int PurchaseId = 0;
        private const string ITEM_ID = "ITEM_ID";
        private const string QUANTITY = "QUANTITY";
        private const string RATE = "RATE";
        private const string LOCATION_ID = "LOCATION_ID";
        private const string GROUP_ID = "GROUP_ID";
        private const string CUSTODIANS_ID = "CUSTODIANS_ID";
        #endregion

        #region Properties
        private int vendorId = 0;
        public int VendorId
        {
            get
            {
                return vendorId;
            }
            set
            {
                vendorId = value;
            }
        }

        private int manuFactureId = 0;
        public int ManufactureId
        {
            get
            {
                return manuFactureId;
            }
            set
            {
                manuFactureId = value;
            }
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

        public int VoucherId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        private string RecentVoucherDate { get; set; }
        private DataTable dtAssetGroup { get; set; }
        public decimal NetAmount { get; set; }
        private double AmountSummaryVal
        {
            get { return this.UtilityMember.NumberSet.ToDouble(colAmount.SummaryItem.SummaryValue.ToString()); }
        }

        #endregion

        #region Constructors
        public frmPurchaseVoucherAdd()
        {
            InitializeComponent();
        }
        public frmPurchaseVoucherAdd(string recentVoucherDate, int projectId, string projectName, int purchaseId)
            : this()
        {
            this.RecentVoucherDate = recentVoucherDate;
            this.ProjectId = projectId;
            this.ProjectName = projectName;
            this.PurchaseId = purchaseId;
            //RealColumnEditQuantityAmount();
            RealColumnEditRateAmount();
            RealColumnEditTransAmount();
        }
        #endregion

        #region Events
        private void frmPurchaseVoucherAdd_Load(object sender, EventArgs e)
        {
            SetTitle();
            ConstructPurchaseVoucherDetail();
            constractBankDatasource();
            LoadDefaults();
            BindPurchaseVoucherGrid();
            dePurchaseDate.DateTime = UtilityMember.DateSet.ToDate(RecentVoucherDate, false);
            ShowEditPurchase();
            LoadNarrationAutoComplete();
            LoadNameAddressAutoComplete();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidatePurchaseDetials())
            {
                try
                {
                    using (AssetPurchaseVoucherSystem purchaseSystem = new AssetPurchaseVoucherSystem())
                    {
                        purchaseSystem.PurchaseId = PurchaseId;
                        purchaseSystem.ProjectId = ProjectId;
                        purchaseSystem.PurchaseDate = this.UtilityMember.DateSet.ToDate(dePurchaseDate.Text, false);
                        purchaseSystem.VendorId = this.UtilityMember.NumberSet.ToInteger(glkVendor.EditValue.ToString());
                        purchaseSystem.BillNo = txtBillNo.Text.Trim();
                        //purchaseSystem.InvoiceNo = txtInvoiceNumber.Text.Trim();
                        purchaseSystem.TotalAmount = this.UtilityMember.NumberSet.ToDecimal(AmountSummaryVal.ToString());
                        purchaseSystem.NetAmount = NetAmount; //this.UtilityMember.NumberSet.ToDecimal(AmountSummaryVal.ToString()); // NetAmount;
                        //purchaseSystem.ManufactureId = this.UtilityMember.NumberSet.ToInteger(glkManufacture.EditValue.ToString());
                        //purchaseSystem.DiscountPercent = this.UtilityMember.NumberSet.ToDecimal(txtDiscountPer.Text.Trim());
                        //purchaseSystem.Discount = this.UtilityMember.NumberSet.ToDecimal(txtDiscountAmount.Text.Trim());
                        //purchaseSystem.TaxAmount = this.UtilityMember.NumberSet.ToDecimal(txtTaxCalAmount.Text.Trim());
                        //purchaseSystem.TaxPercent = this.UtilityMember.NumberSet.ToDecimal(txtTaxPercent.Text.Trim());
                        purchaseSystem.OtherCharges = this.UtilityMember.NumberSet.ToDecimal(txtOtherCharges.Text.Trim());
                        purchaseSystem.SourceFlag = (int)AssetSourceFlag.Purchase;
                        //purchaseSystem.NameAddress = txtNameAddress.Text;
                        purchaseSystem.Narration = txtNarration.Text;
                        purchaseSystem.VoucherId = VoucherId;
                        //purchaseSystem.CashLedgerId = this.UtilityMember.NumberSet.ToInteger(glkpLedger.EditValue.ToString());
                        purchaseSystem.BranchId = 0;

                        DataTable dtSource = gcPurchase.DataSource as DataTable;
                        dtSource.AcceptChanges();
                        DataTable dtFilteredRows = dtSource.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull)).CopyToDataTable();

                        purchaseSystem.dtPurchaseDetail = dtFilteredRows;
                        resultArgs = purchaseSystem.SaveAssetPurchase();
                        if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            ClearControls();
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                        }

                    }
                }
                catch (Exception Ex)
                {
                    MessageRender.ShowMessage(Ex.Message + System.Environment.NewLine + Ex.Source);
                }
                finally
                {
                }
            }

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            PurchaseId = 0;
            ClearControls();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void glkVendor_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                VendorId = glkVendor.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkVendor.EditValue.ToString()) : 0;
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void glkManufacture_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //ManufactureId = glkManufacture.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkManufacture.EditValue.ToString()) : 0;
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void gcPurchase_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control)
                {
                    gvPurchase.PostEditor();
                    gvPurchase.UpdateCurrentRow();
                    LoadCustodain();
                    if (gvPurchase.FocusedColumn == colAssetName)
                    {
                        if (string.IsNullOrEmpty(gvPurchase.GetFocusedRowCellValue(colAssetName).ToString()))
                        {
                            gvPurchase.CloseEditor();
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                            //txtDiscountPer.Focus();
                            //txtDiscountPer.Select();
                        }
                    }
                    //if (gvPurchase.IsLastRow && (gvPurchase.FocusedColumn == colSalvageLife) && gvPurchase.GetFocusedRowCellValue(colSalvageLife) != null)
                    //{
                    //    string AssetGroup = gvPurchase.GetFocusedRowCellValue(colGroup) != null ? gvPurchase.GetFocusedRowCellValue(colGroup).ToString() : string.Empty;
                    //    string AssetName = gvPurchase.GetFocusedRowCellValue(colAssetName) != null ? gvPurchase.GetFocusedRowCellValue(colAssetName).ToString() : string.Empty;
                    //    string Location = gvPurchase.GetFocusedRowCellValue(colLocation) != null ? gvPurchase.GetFocusedRowCellValue(colLocation).ToString() : string.Empty;
                    //    string Quantity = gvPurchase.GetFocusedRowCellValue(colQuantity) != null ? gvPurchase.GetFocusedRowCellValue(colQuantity).ToString() : string.Empty;
                    //    string Rate = gvPurchase.GetFocusedRowCellValue(colRate) != null ? gvPurchase.GetFocusedRowCellValue(colRate).ToString() : string.Empty;
                    //    string Amount = gvPurchase.GetFocusedRowCellValue(colAmount) != null ? gvPurchase.GetFocusedRowCellValue(colAmount).ToString() : string.Empty;
                    //    string UsefulLife = gvPurchase.GetFocusedRowCellValue(colUsefulLife) != null ? gvPurchase.GetFocusedRowCellValue(colUsefulLife).ToString() : string.Empty;
                    //    string Salvage = gvPurchase.GetFocusedRowCellValue(colSalvageLife) != null ? gvPurchase.GetFocusedRowCellValue(colSalvageLife).ToString() : string.Empty;
                    //    if (!string.IsNullOrEmpty(AssetGroup) && !string.IsNullOrEmpty(AssetName) && !string.IsNullOrEmpty(Location) && !string.IsNullOrEmpty(Quantity) && !string.IsNullOrEmpty(Rate) && !string.IsNullOrEmpty(Amount))
                    //    {
                    //        gvPurchase.AddNewRow();
                    //        //lblTotalAmount.Text = lblNetAmount.Text = this.UtilityMember.NumberSet.ToCurrency(AmountSummaryVal);
                    //        NetAmount = this.UtilityMember.NumberSet.ToDecimal(AmountSummaryVal.ToString());
                    //        gvPurchase.FocusedColumn = gvPurchase.Columns[colAssetName.Name];
                    //        gvPurchase.ShowEditor();
                    //    }
                    //    else
                    //    {
                    //        gvPurchase.CloseEditor();
                    //        e.Handled = true;
                    //        e.SuppressKeyPress = true;
                    //        //txtDiscountPer.Focus();
                    //        //txtDiscountPer.Select();
                    //    }
                    //}
                }
                else if (gvPurchase.IsFirstRow && gvPurchase.FocusedColumn == colAssetName && e.Shift && e.KeyCode == Keys.Tab)
                {
                    //txtInvoiceNumber.Select();
                    //txtInvoiceNumber.Focus();
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

        private void dePurchaseDate_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(dePurchaseDate);
        }

        private void glkpLedger_Leave(object sender, EventArgs e)
        {
            //this.SetBorderColorForGridLookUpEdit(glkpLedger);
        }

        private void rglkpAssName_Validating(object sender, CancelEventArgs e)
        {
            int groupId = 0;
            int itemId = 0;
            GridLookUpEdit gridLKPEdit = sender as GridLookUpEdit;
            if (gridLKPEdit.EditValue != null)
            {
                DataRowView drv = gridLKPEdit.GetSelectedDataRow() as DataRowView;

                if (drv != null)
                {
                    groupId = this.UtilityMember.NumberSet.ToInteger(drv[GROUP_ID].ToString());
                    itemId = this.UtilityMember.NumberSet.ToInteger(drv[ITEM_ID].ToString());
                    gvPurchase.SetFocusedRowCellValue(colAssetName, itemId);
                    //gvPurchase.SetFocusedRowCellValue(colGroup, groupId);

                    gvPurchase.PostEditor();
                    gvPurchase.UpdateCurrentRow();
                }
            }
        }

        private void gcPurchasePrticulars_Enter(object sender, EventArgs e)
        {
            gvPurchase.OptionsSelection.EnableAppearanceFocusedCell = true;
        }

        private void gcPurchasePrticulars_Leave(object sender, EventArgs e)
        {
            gvPurchase.OptionsSelection.EnableAppearanceFocusedCell = false;
        }

        private void glkVendor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                frmVendorInfoAdd vendor = new frmVendorInfoAdd(0, VendorManufacture.Vendor);
                vendor.ShowDialog();
                LoadVendors();
            }
        }

        private void glkManufacture_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                frmVendorInfoAdd vendor = new frmVendorInfoAdd(0, VendorManufacture.Manufacture);
                vendor.ShowDialog();
                LoadManufacture();
            }

        }

        private void txtDiscountPer_Leave(object sender, EventArgs e)
        {
            //lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(AmountSummaryVal);
            //txtDiscountAmount.Text = this.UtilityMember.NumberSet.ToNumber(CommonMethod.CalculateAmount(AmountSummaryVal, this.UtilityMember.NumberSet.ToDouble(txtDiscountPer.Text))).ToString();

            //lblNetAmount.Text = this.UtilityMember.NumberSet.ToCurrency(CommonMethod.calculateAssetNetPayment(AmountSummaryVal,
                //this.UtilityMember.NumberSet.ToDouble(txtDiscountAmount.Text),
               //this.UtilityMember.NumberSet.ToDouble(txtTaxCalAmount.Text), this.UtilityMember.NumberSet.ToDouble(txtOtherCharges.Text)));

            NetAmount = this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) > 0 ?
                this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) : NetAmount;

        }

        private void txtDiscountAmount_Leave(object sender, EventArgs e)
        {
            //txtDiscountPer.Text = this.UtilityMember.NumberSet.ToNumber(CommonMethod.CalculatePerCent(AmountSummaryVal, this.UtilityMember.NumberSet.ToDouble(txtDiscountAmount.Text))).ToString();

            //lblTotalDiscount.Text = this.UtilityMember.NumberSet.ToCurrency(-this.UtilityMember.NumberSet.ToDouble(txtDiscountAmount.Text));

            //lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(AmountSummaryVal);
            //lblNetAmount.Text = this.UtilityMember.NumberSet.ToCurrency(CommonMethod.calculateAssetNetPayment(AmountSummaryVal,
                //this.UtilityMember.NumberSet.ToDouble(txtDiscountAmount.Text),
               //this.UtilityMember.NumberSet.ToDouble(txtTaxCalAmount.Text), this.UtilityMember.NumberSet.ToDouble(txtOtherCharges.Text)));

            NetAmount = this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) > 0 ?
                this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) : NetAmount;

        }

        private void txtOtherCharges_Leave(object sender, EventArgs e)
        {
            //lblTotalOtherCharges.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(txtOtherCharges.Text));

            //lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(AmountSummaryVal);
            //lblNetAmount.Text = this.UtilityMember.NumberSet.ToCurrency(CommonMethod.calculateAssetNetPayment(AmountSummaryVal,
               //this.UtilityMember.NumberSet.ToDouble(txtDiscountAmount.Text),
              //this.UtilityMember.NumberSet.ToDouble(txtTaxCalAmount.Text), this.UtilityMember.NumberSet.ToDouble(txtOtherCharges.Text)));

            NetAmount = this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) > 0 ?
                this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) : NetAmount;
        }

        private void txtTaxPercent_Leave(object sender, EventArgs e)
        {
            //lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(AmountSummaryVal);
            //txtTaxCalAmount.Text = this.UtilityMember.NumberSet.ToNumber(CommonMethod.CalculateAmount(AmountSummaryVal,
                //this.UtilityMember.NumberSet.ToDouble(txtTaxPercent.Text))).ToString();

            //this.UtilityMember.NumberSet.ToCurrency(CommonMethod.calculateAssetNetPayment(AmountSummaryVal,
               //this.UtilityMember.NumberSet.ToDouble(txtDiscountAmount.Text),
              //this.UtilityMember.NumberSet.ToDouble(txtTaxCalAmount.Text), this.UtilityMember.NumberSet.ToDouble(txtOtherCharges.Text)));

            NetAmount = this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) > 0 ?
                this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) : NetAmount;

        }

        private void txtTaxCalAmount_Leave(object sender, EventArgs e)
        {
            //lblTotalTaxAmount.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(txtTaxCalAmount.Text));
            //txtTaxPercent.Text = this.UtilityMember.NumberSet.ToNumber(CommonMethod.CalculatePerCent(AmountSummaryVal, this.UtilityMember.NumberSet.ToDouble(txtTaxCalAmount.Text))).ToString();

            //lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(AmountSummaryVal);
            //lblNetAmount.Text = this.UtilityMember.NumberSet.ToCurrency(CommonMethod.calculateAssetNetPayment(AmountSummaryVal,
               //this.UtilityMember.NumberSet.ToDouble(txtDiscountAmount.Text),
              //this.UtilityMember.NumberSet.ToDouble(txtTaxCalAmount.Text), this.UtilityMember.NumberSet.ToDouble(txtOtherCharges.Text)));

            NetAmount = this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) > 0 ? this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) :
NetAmount;

        }

        private void dePurchaseDate_EditValueChanged(object sender, EventArgs e)
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
        #endregion

        #region Methods

        private void LoadVoucherNo()
        {
            using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
            {
                voucherTransaction.VoucherType = VoucherSubTypes.PY.ToString();
                voucherTransaction.ProjectId = ProjectId;
                voucherTransaction.VoucherDate = this.UtilityMember.DateSet.ToDate(dePurchaseDate.Text, false);
                txtVoucherNo.Text = voucherTransaction.TempVoucherNo();
            }
        }

        private void ShowEditPurchase()
        {
            try
            {
                if (PurchaseId > 0)
                {
                    using (AssetPurchaseVoucherSystem purchaseSystem = new AssetPurchaseVoucherSystem(PurchaseId, (int)AssetSourceFlag.Purchase))
                    {
                        purchaseSystem.PurchaseId = PurchaseId;
                        dePurchaseDate.DateTime = purchaseSystem.PurchaseDate;
                        //glkManufacture.EditValue = purchaseSystem.ManufactureId;
                        glkVendor.EditValue = purchaseSystem.VendorId;
                        //glkpLedger.EditValue = purchaseSystem.CashLedgerId;
                        txtBillNo.Text = purchaseSystem.BillNo;
                        //txtInvoiceNumber.Text = purchaseSystem.InvoiceNo.ToString();
                        VoucherId = purchaseSystem.VoucherId;
                        txtVoucherNo.Text = purchaseSystem.VoucherNo;
                        NetAmount = this.UtilityMember.NumberSet.ToDecimal(purchaseSystem.NetAmount.ToString());
                        resultArgs = purchaseSystem.FetchPurchaseDetails(PurchaseId.ToString());
                        if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            gcPurchase.DataSource = resultArgs.DataSource.Table;
                            gcPurchase.RefreshDataSource();
                        }
                        //txtDiscountPer.Text = purchaseSystem.DiscountPercent.ToString();
                        //txtDiscountAmount.Text = purchaseSystem.Discount.ToString();
                        //txtTaxPercent.Text = purchaseSystem.TaxPercent.ToString();
                        //txtTaxCalAmount.Text = purchaseSystem.TaxAmount.ToString();
                        txtOtherCharges.Text = purchaseSystem.OtherCharges.ToString();
                        //txtNameAddress.Text = purchaseSystem.NameAddress;
                        txtNarration.Text = purchaseSystem.Narration;
                        //lblTotalOtherCharges.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(purchaseSystem.OtherCharges.ToString()));
                        //lblNetAmount.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(purchaseSystem.NetAmount.ToString()));
                        //lblTotalTaxAmount.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(purchaseSystem.TaxAmount.ToString()));
                        //lblTotalDiscount.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(purchaseSystem.Discount.ToString()));
                        //lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(purchaseSystem.TotalAmount.ToString()));

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

        private void SetTitle()
        {
            this.Text = PurchaseId == 0 ? this.GetMessage(MessageCatalog.Asset.PurchaseVoucher.PURCHASE_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.PurchaseVoucher.PURCHASE_EDIT_CAPTION);
            ucCaptionProject.Caption = ProjectName;
        }

        private bool ValidatePurchaseDetials()
        {
            bool isPurchaseTrue = true;
            if (string.IsNullOrEmpty(dePurchaseDate.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.PurchaseVoucher.PURCHASE_DATE_EMPTY));
                this.SetBorderColor(dePurchaseDate);
                isPurchaseTrue = false;
                this.dePurchaseDate.Focus();
            }
            else if (string.IsNullOrEmpty(glkVendor.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.VendorInfo.VENDOR_NAME_EMPTY));
                this.SetBorderColor(glkVendor);
                isPurchaseTrue = false;
                this.glkVendor.Focus();
            }
            else if (string.IsNullOrEmpty(txtBillNo.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.PurchaseVoucher.PURCHASE_BILL_NO_EMPTY));
                this.SetBorderColor(txtBillNo);
                isPurchaseTrue = false;
                this.txtBillNo.Focus();
            }
            else if (!IsValidTransGrid())
            {
                isPurchaseTrue = false;
            }
            ////else if (string.IsNullOrEmpty(glkpLedger.Text) || glkpLedger.EditValue.Equals("0") || glkpLedger.EditValue == null)
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.PurchaseVoucher.PURCHASE_ASSET_CASH_BANK_LEDGER_EMPTY));
            //    this.SetBorderColorForGridLookUpEdit(glkpLedger);
            //    isPurchaseTrue = false;
            //    glkpLedger.Focus();
            //}
            return isPurchaseTrue;
        }

        private void LoadVendors()
        {
            try
            {
                using (VendorInfoSystem vendorSystem = new VendorInfoSystem())
                {
                    resultArgs = vendorSystem.FetchDetails();
                    glkVendor.Properties.DataSource = null;
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkVendor, resultArgs.DataSource.Table, vendorSystem.AppSchema.Vendors.NAMEColumn.ColumnName, vendorSystem.AppSchema.Vendors.IDColumn.ColumnName);
                        this.glkVendor.EditValueChanged -= new System.EventHandler(this.glkVendor_EditValueChanged);
                        //glkVendor.EditValue = (VendorId != 0) ? VendorId : glkVendor.Properties.GetKeyValue(0);

                        this.glkVendor.EditValueChanged += new System.EventHandler(this.glkVendor_EditValueChanged);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void LoadManufacture()
        {
            try
            {
                using (ManufactureInfoSystem manufactureSystem = new ManufactureInfoSystem())
                {
                    resultArgs = manufactureSystem.FetchDetails();
                    //glkManufacture.Properties.DataSource = null;
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        //this.UtilityMember.ComboSet.BindGridLookUpCombo(glkManufacture, resultArgs.DataSource.Table, manufactureSystem.AppSchema.Manufactures.NAMEColumn.ColumnName, manufactureSystem.AppSchema.Vendors.IDColumn.ColumnName);
                        //this.glkManufacture.EditValueChanged -= new System.EventHandler(this.glkManufacture_EditValueChanged);
                        //glkManufacture.EditValue = (ManufactureId != 0) ? ManufactureId : glkManufacture.Properties.GetKeyValue(0);

                        //this.glkManufacture.EditValueChanged += new System.EventHandler(this.glkManufacture_EditValueChanged);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void LoadAssetGroup()
        {
            try
            {
                using (AssetGroupSystem assetGroupSystem = new AssetGroupSystem())
                {
                    resultArgs = assetGroupSystem.FetchGroupDetails();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpAssetGroup, resultArgs.DataSource.Table, assetGroupSystem.AppSchema.ASSETGroupDetails.GROUP_NAMEColumn.ColumnName, assetGroupSystem.AppSchema.ASSETGroupDetails.GROUP_IDColumn.ColumnName);
                        dtAssetGroup = resultArgs.DataSource.Table;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void LoadCustodain()
        {
            try
            {
                using (CustodiansSystem custodainSystem = new CustodiansSystem())
                {
                    // int LocationID = gvPurchase.GetFocusedRowCellValue(colLocation) != null ? this.UtilityMember.NumberSet.ToInteger(gvPurchase.GetFocusedRowCellValue(colLocation).ToString()) : 0;
                    //  custodainSystem.LocationID = LocationID;
                    resultArgs = custodainSystem.FetchAllCustodiansDetails();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkCustodian, resultArgs.DataSource.Table, custodainSystem.AppSchema.AssetCustodians.NAMEColumn.ColumnName, custodainSystem.AppSchema.AssetCustodians.CUSTODIANS_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void LoadCustodainDetails()
        {
            try
            {
                using (CustodiansSystem custodainSystem = new CustodiansSystem())
                {
                    resultArgs = custodainSystem.FetchAllCustodiansDetails();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkCustodian, resultArgs.DataSource.Table, custodainSystem.AppSchema.AssetCustodians.NAMEColumn.ColumnName, custodainSystem.AppSchema.AssetCustodians.CUSTODIANS_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void LoadAssetLocation()
        {
            try
            {
                using (LocationSystem locationSystem = new LocationSystem())
                {
                    resultArgs = locationSystem.FetchLocationDetails();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpAssetLocation, resultArgs.DataSource.Table, locationSystem.AppSchema.ASSETLocationDetails.LOCATION_NAMEColumn.ColumnName, locationSystem.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void LoadAssetName()
        {
            try
            {
                using (AssetItemSystem assetItemSystem = new AssetItemSystem())
                {
                    resultArgs = assetItemSystem.FetchAssetItemDetails();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpAssName, resultArgs.DataSource.Table, assetItemSystem.AppSchema.ASSETItem.ASSET_NAMEColumn.ColumnName, assetItemSystem.AppSchema.ASSETItem.ITEM_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void LoadCashBankLedgers()
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    ledgerSystem.ProjectId = this.ProjectId;
                    resultArgs = ledgerSystem.FetchCashBankLedger();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        //this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpLedger, resultArgs.DataSource.Table, ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData.Equals(Keys.F5))
            {
                ShowProjectSelectionWindow();
            }
            else if (KeyData == (Keys.Alt | Keys.D))
            {
                DeleteTransaction();
            }
            else if (KeyData.Equals(Keys.F3))
            {
                dePurchaseDate.Focus();
            }
            return base.ProcessCmdKey(ref msg, KeyData);
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
                    ucCaptionProject.Caption = ProjectName = projectSelection.ProjectName;
                }
            }
        }

        private void ClearControls()
        {
            if (PurchaseId == 0)
            {
                PurchaseId = 0;
                NetAmount = 0;
                dePurchaseDate.DateTime = UtilityMember.DateSet.ToDate(DateTime.Now.ToString(), false);
                //txtDiscountAmount.Text = txtDiscountPer.Text = txtOtherCharges.Text =
                //txtTaxCalAmount.Text = txtTaxPercent.Text = glkpLedger.Text = string.Empty;
                //lblTotalAmount.Text = lblTotalDiscount.Text = lblTotalOtherCharges.Text = lblTotalTaxAmount.Text =
                    //lblNetAmount.Text = this.UtilityMember.NumberSet.ToCurrency(0.00);
                txtBillNo.Text = txtNarration.Text = string.Empty;
                gcPurchase.DataSource = null;
                LoadDefaults();
                ConstructPurchaseVoucherDetail();
                BindPurchaseVoucherGrid();
                LoadVoucherNo();
                LoadCashBankLedgers();
                dePurchaseDate.Focus();
            }
            else
            {
                if (this.UIAppSetting.UITransClose == "1")
                {
                    this.Close();
                }
            }
        }

        private void LoadDefaults()
        {
            LoadAccountingDate();
            LoadVendors();
            LoadManufacture();
            LoadCashBankLedgers();
        }

        private void LoadAccountingDate()
        {
            dePurchaseDate.DateTime = UtilityMember.DateSet.ToDate(RecentVoucherDate, false);
            dePurchaseDate.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dePurchaseDate.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            dePurchaseDate.DateTime = (!string.IsNullOrEmpty(RecentVoucherDate)) ? UtilityMember.DateSet.ToDate(RecentVoucherDate, false) : UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dePurchaseDate.DateTime = dePurchaseDate.DateTime.AddMonths(1).AddDays(-1);
        }

        private void BindPurchaseVoucherGrid()
        {
            LoadAssetGroup();
            LoadAssetName();
            LoadAssetLocation();
            LoadCustodainDetails();
        }

        private void ConstructPurchaseVoucherDetail()
        {
            DataTable dtPurchaseVouhcerDetail = new DataTable();
            dtPurchaseVouhcerDetail.Columns.Add("GROUP_ID", typeof(string));
            dtPurchaseVouhcerDetail.Columns.Add("ITEM_ID", typeof(string));
            dtPurchaseVouhcerDetail.Columns.Add("LOCATION_ID", typeof(string));
            dtPurchaseVouhcerDetail.Columns.Add("CUSTODIANS_ID", typeof(string));
            dtPurchaseVouhcerDetail.Columns.Add("QUANTITY", typeof(UInt32));
            dtPurchaseVouhcerDetail.Columns.Add("RATE", typeof(decimal));
            dtPurchaseVouhcerDetail.Columns.Add("DISCOUNT", typeof(decimal));
            dtPurchaseVouhcerDetail.Columns.Add("AMOUNT", typeof(decimal));
            dtPurchaseVouhcerDetail.Columns.Add("USEFUL_LIFE", typeof(decimal));
            dtPurchaseVouhcerDetail.Columns.Add("SALVAGE_LIFE", typeof(decimal));
            gcPurchase.DataSource = dtPurchaseVouhcerDetail;
            gvPurchase.AddNewRow();
        }

        private bool IsValidTransGrid()
        {
            bool isValid = true;
            try
            {
                DataTable dtPurchase = gcPurchase.DataSource as DataTable;
                int ItemId = 0;
                decimal Rate = 0;
                int RowPosition = 0;
                int location = 0;
                string AssetId = string.Empty;
                decimal Quantity = 0;
                DataView dv = new DataView(dtPurchase);
                dv.RowFilter = "(ITEM_ID>0 OR QUANTITY>0)";
                gvPurchase.FocusedColumn = colAssetName;
                if (dv.Count > 0)
                {
                    foreach (DataRowView drPurchase in dv)
                    {
                        ItemId = this.UtilityMember.NumberSet.ToInteger(drPurchase[ITEM_ID].ToString());
                        Quantity = this.UtilityMember.NumberSet.ToDecimal(drPurchase[QUANTITY].ToString());
                        location = this.UtilityMember.NumberSet.ToInteger(drPurchase[LOCATION_ID].ToString());
                        Rate = this.UtilityMember.NumberSet.ToDecimal(drPurchase[RATE].ToString());
                        if (ItemId == 0)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.SalesVoucher.SALES_ASSET_NAME_EMPTY));
                            gvPurchase.FocusedColumn = colAssetName;
                            isValid = false;
                        }
                        else if (location == 0)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.PurchaseVoucher.PURCHASE_ASSETLOCATION_VALIDATION));
                            //gvPurchase.FocusedColumn = colLocation;
                            isValid = false;
                        }
                        else if (Quantity == 0)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.SalesVoucher.SALES_QUANTITY_EMPTY));
                            //gvPurchase.FocusedColumn = colQuantity;
                            isValid = false;
                        }
                        else if (Rate == 0)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.SalesVoucher.SALES_RATE_EMPTY));
                            //gvPurchase.FocusedColumn = colQuantity;
                            isValid = false;
                        }
                        if (!isValid) break;
                        RowPosition = RowPosition + 1;

                    }
                }
                else
                {
                    isValid = false;
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.SalesVoucher.SALES_ASSET_NAME_EMPTY));
                    gvPurchase.FocusedColumn = colAssetName;
                }

                if (!isValid)
                {
                    gvPurchase.CloseEditor();
                    gvPurchase.FocusedRowHandle = gvPurchase.GetRowHandle(RowPosition);
                    gvPurchase.ShowEditor();
                }

            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
                isValid = false;
            }


            return isValid;
        }

        private void DeleteTransaction()
        {
            try
            {
                if (!string.IsNullOrEmpty(gvPurchase.GetFocusedRowCellValue(colAssetName).ToString()))
                {
                    if (gvPurchase.RowCount > 1)
                    {

                        if (gvPurchase.FocusedRowHandle != GridControl.NewItemRowHandle)
                        {

                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                gvPurchase.DeleteRow(gvPurchase.FocusedRowHandle);
                                gvPurchase.UpdateCurrentRow();
                                gcPurchase.RefreshDataSource();
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                gvPurchase.FocusedColumn = colAssetName;
                            }

                        }
                    }
                    else if (gvPurchase.RowCount == 1)
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            ConstructPurchaseVoucherDetail();
                            gcPurchase.RefreshDataSource();
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                            gvPurchase.FocusedColumn = colAssetName;
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Bank.BANK_DELETE));
                    gvPurchase.FocusedColumn = colAssetName;
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        private void RealColumnEditTransAmount()
        {
            colAmount.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditTransAmount_EditValueChanged);
            this.gvPurchase.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvPurchase.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colAmount)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvPurchase.ShowEditorByMouse();
                    }));
                }
            };
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
                        //txtNameAddress.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        //txtNameAddress.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        //txtNameAddress.MaskBox.AutoCompleteCustomSource = collection;
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

        void RealColumnEditTransAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvPurchase.PostEditor();
            gvPurchase.UpdateCurrentRow();
            if (gvPurchase.ActiveEditor == null)
            {
                gvPurchase.ShowEditor();
            }
            CalculatePurchaseNetAmount();
        }

        private void RealColumnEditRateAmount()
        {
            //colRate.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditRateAmount_EditValueChanged);
            this.gvPurchase.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvPurchase.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colAmount)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvPurchase.ShowEditorByMouse();
                    }));
                }
            };
        }

        //void RealColumnEditRateAmount_EditValueChanged(object sender, System.EventArgs e)
        //{
        //    int quantity = 0;
        //    decimal rate = 0;
        //    BaseEdit edit = sender as BaseEdit;
        //    if (edit.EditValue == null)
        //        return;
        //    gvPurchase.PostEditor();
        //    gvPurchase.UpdateCurrentRow();
        //    if (gvPurchase.ActiveEditor == null)
        //    {
        //        gvPurchase.ShowEditor();
        //    }
        //    quantity = this.UtilityMember.NumberSet.ToInteger(gvPurchase.GetFocusedRowCellValue(colQuantity).ToString());
        //    rate = this.UtilityMember.NumberSet.ToDecimal(gvPurchase.GetFocusedRowCellValue(colRate).ToString());
        //    gvPurchase.SetFocusedRowCellValue(colAmount, quantity * rate);

        //    CalculatePurchaseNetAmount();
        //}

        //private void RealColumnEditQuantityAmount()
        //{
        //    colQuantity.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditQuantityAmount_EditValueChanged);
        //    this.gvPurchase.MouseDown += (object sender, MouseEventArgs e) =>
        //    {
        //        GridHitInfo hitInfo = gvPurchase.CalcHitInfo(e.Location);
        //        if (hitInfo.Column != null && hitInfo.Column == colAmount)
        //        {
        //            this.BeginInvoke(new MethodInvoker(delegate
        //            {
        //                gvPurchase.ShowEditorByMouse();
        //            }));
        //        }
        //    };
        //}

        //void RealColumnEditQuantityAmount_EditValueChanged(object sender, System.EventArgs e)
        //{
        //    int quantity = 0;
        //    decimal rate = 0;
        //    BaseEdit edit = sender as BaseEdit;
        //    if (edit.EditValue == null)
        //        return;
        //    gvPurchase.PostEditor();
        //    gvPurchase.UpdateCurrentRow();
        //    if (gvPurchase.ActiveEditor == null)
        //    {
        //        gvPurchase.ShowEditor();
        //    }
        //    quantity = this.UtilityMember.NumberSet.ToInteger(gvPurchase.GetFocusedRowCellValue(colQuantity).ToString());
        //    rate = this.UtilityMember.NumberSet.ToDecimal(gvPurchase.GetFocusedRowCellValue(colRate).ToString());
        //    gvPurchase.SetFocusedRowCellValue(colAmount, quantity * rate);
        //    CalculatePurchaseNetAmount();
        //}

        private void CalculatePurchaseNetAmount()
        {
            //lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(AmountSummaryVal);
            //lblNetAmount.Text = this.UtilityMember.NumberSet.ToCurrency(CommonMethod.calculateAssetNetPayment(AmountSummaryVal,
              //this.UtilityMember.NumberSet.ToDouble(txtDiscountAmount.Text),
             //this.UtilityMember.NumberSet.ToDouble(txtTaxCalAmount.Text), this.UtilityMember.NumberSet.ToDouble(txtOtherCharges.Text)));

            NetAmount = this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) > 0 ? this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) :
NetAmount;
        }

        #endregion

        private void glkVendor_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkVendor);
        }

        private void txtBillNo_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtBillNo);
        }

        private void bbiDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteTransaction();
        }

        private void txtDiscountPer_EditValueChanged(object sender, EventArgs e)
        {
            txtDiscountPer_Leave(sender, e);
        }

        private void txtDiscountAmount_EditValueChanged(object sender, EventArgs e)
        {
            txtDiscountAmount_Leave(sender, e);
        }

        private void txtTaxPercent_EditValueChanged(object sender, EventArgs e)
        {
            txtTaxPercent_Leave(sender, e);
        }

        private void txtTaxCalAmount_EditValueChanged(object sender, EventArgs e)
        {
            txtTaxCalAmount_Leave(sender, e);
        }

        private void txtOtherCharges_EditValueChanged(object sender, EventArgs e)
        {
            txtOtherCharges_Leave(sender, e);
        }

        private void glkpLedger_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                ACPP.Modules.Master.frmLedgerDetailAdd frmBank = new ACPP.Modules.Master.frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.BK, ProjectId);
                frmBank.ShowDialog();
                LoadCashBankLedgers();
            }
        }

        private void rglkpAssetLocation_Validating(object sender, CancelEventArgs e)
        {
            int CustodianId = 0;
            int LocationId = 0;
            GridLookUpEdit gridLKPEdit = sender as GridLookUpEdit;
            if (gridLKPEdit.EditValue != null)
            {
                DataRowView drv = gridLKPEdit.GetSelectedDataRow() as DataRowView;
                if (drv != null)
                {
                    CustodianId = this.UtilityMember.NumberSet.ToInteger(drv[CUSTODIANS_ID].ToString());
                    LocationId = this.UtilityMember.NumberSet.ToInteger(drv[LOCATION_ID].ToString());
                    //gvPurchase.SetFocusedRowCellValue(colLocation, LocationId);
                    //gvPurchase.SetFocusedRowCellValue(colCustodian, CustodianId);
                    gvPurchase.PostEditor();
                    gvPurchase.UpdateCurrentRow();
                }
            }
        }

        private DataTable constractBankDatasource()
        {
            DataTable dtBank = new DataTable();
            dtBank.Columns.Add("BANK_ID", typeof(int));
            dtBank.Columns.Add("AMOUNT", typeof(decimal));
            dtBank.Columns.Add("REF_NO", typeof(string));
            dtBank.Columns.Add("MATTERIALIZED_ON", typeof(DateTime));
            dtBank.Columns.Add("CURRENT_BALANCE", typeof(decimal));
            gcCashBank.DataSource = dtBank;
            gvCashBank.AddNewRow();
            return dtBank;
        }

        private void gcPurchase_Click(object sender, EventArgs e)
        {

        }
    }
}
