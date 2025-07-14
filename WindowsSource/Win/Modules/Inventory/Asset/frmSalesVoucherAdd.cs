using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model;
using Bosco.Model.UIModel;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Bosco.Model.Transaction;
using ACPP.Modules.Transaction;

namespace ACPP.Modules.Asset.Transactions
{
    public partial class frmSalesVoucherAdd : frmBaseAdd
    {
        #region VariableDeclaration
        ResultArgs resultArgs = new ResultArgs();
        public event EventHandler UpdateHeld;

        private int SalesId = 0;
        private const string AMOUNT = "AMOUNT";
        private const string ITEM_ID = "ITEM_ID";
        private const string ASSET_ID = "ASSET_ID";
        private const string QUANTITY = "QUANTITY";
        private const string LOCATION_ID = "LOCATION_ID";
        #endregion

        #region Properties
        private int Quantity { get; set; }
        private decimal ItemTotalRate { get; set; }
        private DateTime RecentDate { get; set; }
        public decimal NetAmount { get; set; }
        //Contains all the Active Asset Items
        private DataTable dtActiveAsset { get; set; }

        public DataTable dtAssetIdDetails { get; set; }
        public int VoucherId { get; set; }
        private double AmountSummaryVal
        {
            get { return this.UtilityMember.NumberSet.ToDouble(colAmount.SummaryItem.SummaryValue.ToString()); }
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
        #endregion

        #region Constructor
        public frmSalesVoucherAdd()
        {
            InitializeComponent();

        }

        public frmSalesVoucherAdd(int projectId, string projectName, int Salesid, string recentDate)
            : this()
        {
            this.ProjectId = projectId;
            ProjectName = projectName;
            SalesId = Salesid;
            RecentDate = UtilityMember.DateSet.ToDate(recentDate, false);
            // RealColumnEditMultiAssetIdSelect();
            //  RealColumnEditTransAmount();
        }
        #endregion

        #region Property
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

        int itemId = 0;
        private int ItemId
        {
            get
            {
                itemId = gvSalesAdd.GetFocusedRowCellValue(colAssetName) != null ?
                this.UtilityMember.NumberSet.ToInteger(gvSalesAdd.GetRowCellValue(gvSalesAdd.FocusedRowHandle, colAssetName).ToString()) : 0;
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
                AID = gvSalesAdd.GetFocusedRowCellValue(colAssName) != null ?
                gvSalesAdd.GetFocusedRowCellDisplayText(colAssName) : string.Empty;
                return AID;
            }
        }

        #endregion

        #region Events
        private void frmSalesVoucherAdd_Load(object sender, EventArgs e)
        {
            SetTitle();
            BindGrid();
            LoadPartyName();
            LoadProjectDate();
            LoadCashBankLedgers();
            dtSalesDate.DateTime = RecentDate;
            AssignValuesToControls();
            LoadNarrationAutoComplete();
            LoadNameAddressAutoComplete();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidSalesDetails())
                {
                    using (AssetSalesSystem salesSystem = new AssetSalesSystem())
                    {
                        salesSystem.SalesId = SalesId == 0 ? this.UtilityMember.NumberSet.ToInteger(AddNewRow.NewRow.ToString()) : SalesId;
                        salesSystem.SalesDate = this.UtilityMember.DateSet.ToDate(dtSalesDate.Text, false);
                        salesSystem.PartyName = txtName.Text.Trim();
                        salesSystem.VoucherId = VoucherId;
                        salesSystem.ProjectId = ProjectId;
                        //    salesSystem.TaxAmount = this.UtilityMember.NumberSet.ToDecimal(txtTaxCalAmt.Text.Trim());
                        salesSystem.NetAmount = NetAmount;
                        salesSystem.TotalAmount = this.UtilityMember.NumberSet.ToDecimal(AmountSummaryVal.ToString());
                        //     salesSystem.NameAddress = txtNameAddress.Text.Trim();
                        salesSystem.Narration = txtNarration.Text.Trim();
                        //     salesSystem.Discount = this.UtilityMember.NumberSet.ToDecimal(txtDiscountAmount.Text.Trim());
                        salesSystem.OtherCharges = this.UtilityMember.NumberSet.ToDecimal(txtOtherCharges.Text.Trim());
                        //     salesSystem.DiscountPercent = this.UtilityMember.NumberSet.ToDecimal(txtDiscountPer.Text.Trim());
                        //     salesSystem.TaxPercent = this.UtilityMember.NumberSet.ToDecimal(txtTaxPercent.Text.Trim());
                        //     salesSystem.CashLedgerId = this.UtilityMember.NumberSet.ToInteger(glkpCashBankLedger.EditValue.ToString());
                        salesSystem.SourceFlag = (int)AssetSourceFlag.Sales;
                        DataTable dtSource = gcSalesAdd.DataSource as DataTable;
                        DataTable dtFilteredRows = dtSource.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull)).CopyToDataTable();
                        salesSystem.dtSalesDetail = dtFilteredRows;
                        resultArgs = salesSystem.SaveAssetSales();
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
                            dtSalesDate.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.ToString());
            }
            finally { }
        }

        private void rglkpLocation_EditValueChanged(object sender, EventArgs e)
        {
            LoadActiveAssets();
        }

        //private void gvSalesAdd_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        //{
        //    LoadLocationByItem();

        //}

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvSalesAdd_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            string assetIds = string.Empty;
            string ItemName = string.Empty;
            //if (gvSalesAdd.FocusedColumn == colMultAssetId && !string.IsNullOrEmpty(gvSalesAdd.GetFocusedRowCellValue(colMultAssetId).ToString()))
            //{
            //    ItemName = gvSalesAdd.GetFocusedRowCellValue(colAssName).ToString();
            //    assetIds = gvSalesAdd.GetFocusedRowCellValue(colMultAssetId).ToString();
            //    if (!string.IsNullOrEmpty(assetIds) && !string.IsNullOrEmpty(ItemName))
            //    {
            //        LoadAssetByItemId();
            //        LoadActiveAssets();
            //        gvSalesAdd.SetRowCellValue(gvSalesAdd.FocusedRowHandle, colMultAssetId, assetIds);
            //        gvSalesAdd.UpdateCurrentRow();
            //    }
            //    else
            //    {
            //        LoadActiveAssets();
            //    }
            //}
        }

        private void rbiDelete_Click(object sender, EventArgs e)
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

        private void txtTaxCalAmt_EditValueChanged(object sender, EventArgs e)
        {
            txtTaxCalAmt_Leave(sender, e);

        }

        private void txtOtherCharges_EditValueChanged(object sender, EventArgs e)
        {
            txtOtherCharges_Leave(sender, e);
        }

        private void txtDiscountPer_Leave(object sender, EventArgs e)
        {
            // lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(AmountSummaryVal);
            // txtDiscountAmount.Text = this.UtilityMember.NumberSet.ToNumber(CommonMethod.CalculateAmount(AmountSummaryVal, this.UtilityMember.NumberSet.ToDouble(txtDiscountPer.Text))).ToString();

            //  lblNetAmt.Text = this.UtilityMember.NumberSet.ToCurrency(CommonMethod.calculateAssetNetPayment(AmountSummaryVal,
            //      this.UtilityMember.NumberSet.ToDouble(txtDiscountAmount.Text),
            //     this.UtilityMember.NumberSet.ToDouble(txtTaxCalAmt.Text), this.UtilityMember.NumberSet.ToDouble(txtOtherCharges.Text)));

            NetAmount = this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) > 0 ?
                this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) : NetAmount;

        }

        private void txtDiscountAmount_Leave(object sender, EventArgs e)
        {
            //     txtDiscountPer.Text = this.UtilityMember.NumberSet.ToNumber(CommonMethod.CalculatePerCent(AmountSummaryVal, this.UtilityMember.NumberSet.ToDouble(txtDiscountAmount.Text))).ToString();

            //   lblTotalDiscount.Text = this.UtilityMember.NumberSet.ToCurrency(-this.UtilityMember.NumberSet.ToDouble(txtDiscountAmount.Text));

            //   lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(AmountSummaryVal);
            //   lblNetAmt.Text = this.UtilityMember.NumberSet.ToCurrency(CommonMethod.calculateAssetNetPayment(AmountSummaryVal,
            //       this.UtilityMember.NumberSet.ToDouble(txtDiscountAmount.Text),
            //      this.UtilityMember.NumberSet.ToDouble(txtTaxCalAmt.Text), this.UtilityMember.NumberSet.ToDouble(txtOtherCharges.Text)));

            NetAmount = this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) > 0 ?
                this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) : NetAmount;

        }

        private void txtOtherCharges_Leave(object sender, EventArgs e)
        {
            //     lblTotalOtherCharges.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(txtOtherCharges.Text));

            //      lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(AmountSummaryVal);
            //      lblNetAmt.Text = this.UtilityMember.NumberSet.ToCurrency(CommonMethod.calculateAssetNetPayment(AmountSummaryVal,
            //         this.UtilityMember.NumberSet.ToDouble(txtDiscountAmount.Text),
            //        this.UtilityMember.NumberSet.ToDouble(txtTaxCalAmt.Text), this.UtilityMember.NumberSet.ToDouble(txtOtherCharges.Text)));

            NetAmount = this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) > 0 ?
                this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) : NetAmount;
        }

        private void txtTaxPercent_Leave(object sender, EventArgs e)
        {
            //     lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(AmountSummaryVal);
            //     txtTaxCalAmt.Text = this.UtilityMember.NumberSet.ToNumber(CommonMethod.CalculateAmount(AmountSummaryVal,
            //         this.UtilityMember.NumberSet.ToDouble(txtTaxPercent.Text))).ToString();

            //    this.UtilityMember.NumberSet.ToCurrency(CommonMethod.calculateAssetNetPayment(AmountSummaryVal,
            //        this.UtilityMember.NumberSet.ToDouble(txtDiscountAmount.Text),
            //       this.UtilityMember.NumberSet.ToDouble(txtTaxCalAmt.Text), this.UtilityMember.NumberSet.ToDouble(txtOtherCharges.Text)));

            NetAmount = this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) > 0 ?
                this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) : NetAmount;

        }


        private void txtTaxCalAmt_Leave(object sender, EventArgs e)
        {
            //    lblTotalTaxAmount.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(txtTaxCalAmt.Text));
            //     txtTaxPercent.Text = this.UtilityMember.NumberSet.ToNumber(CommonMethod.CalculatePerCent(AmountSummaryVal, this.UtilityMember.NumberSet.ToDouble(txtTaxCalAmt.Text))).ToString();

            //    lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(AmountSummaryVal);
            //    lblNetAmt.Text = this.UtilityMember.NumberSet.ToCurrency(CommonMethod.calculateAssetNetPayment(AmountSummaryVal,
            //       this.UtilityMember.NumberSet.ToDouble(txtDiscountAmount.Text),
            //      this.UtilityMember.NumberSet.ToDouble(txtTaxCalAmt.Text), this.UtilityMember.NumberSet.ToDouble(txtOtherCharges.Text)));

            NetAmount = this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) > 0 ? this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) :
            NetAmount;

        }

        private void glkpCashBankLedger_Leave(object sender, EventArgs e)
        {
            //     this.SetBorderColorForGridLookUpEdit(glkpCashBankLedger);
        }

        private void dtSalesDate_EditValueChanged(object sender, EventArgs e)
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

        #region RealEditMulitselectAsset

        /// <summary>
        /// this allow user to select the Id's and based on that amount is calculated
        /// </summary>
        private void RealColumnEditMultiAssetIdSelect()
        {
            //  colMultAssetId.RealColumnEdit.EditValueChanged += new EventHandler(RealColumnEdit_EditValueChanged);
            this.gvSalesAdd.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvSalesAdd.CalcHitInfo(e.Location);
                //if (hitInfo.Column != null && hitInfo.Column == colMultAssetId)
                //{
                //    this.BeginInvoke(new MethodInvoker(delegate
                //    {
                //        gvSalesAdd.ShowEditorByMouse();
                //    }));
                //}
            };
        }

        /// <summary>
        /// this is Real Edit Coloumn to be derived from the source
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RealColumnEdit_EditValueChanged(object sender, EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvSalesAdd.PostEditor();
            gvSalesAdd.UpdateCurrentRow();
            if (gvSalesAdd.ActiveEditor == null)
            {
                gvSalesAdd.ShowEditor();
            }
            LoadActiveAssets();
        }

        /// <summary>
        /// Add cash bank ledger form for add ledger
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkpCashBankLedger_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                ACPP.Modules.Master.frmLedgerDetailAdd frmBank = new ACPP.Modules.Master.frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.BK, ProjectId);
                frmBank.ShowDialog();
                LoadCashBankLedgers();
            }
        }

        #endregion

        private void RealColumnEditTransAmount()
        {
            colAmount.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditTransAmount_EditValueChanged);
            this.gvSalesAdd.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvSalesAdd.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colAmount)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvSalesAdd.ShowEditorByMouse();
                    }));
                }
            };
        }

        void RealColumnEditTransAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvSalesAdd.PostEditor();
            gvSalesAdd.UpdateCurrentRow();
            if (gvSalesAdd.ActiveEditor == null)
            {
                gvSalesAdd.ShowEditor();
            }
            CalculateSalesNetAmount();
        }

        #endregion

        #region Methods
        private bool ValidSalesDetails()
        {
            bool isSalesTrue = true;
            if (string.IsNullOrEmpty(dtSalesDate.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_TRANSACTION_DATE));
                isSalesTrue = false;
                this.SetBorderColor(dtSalesDate);
                dtSalesDate.Focus();
            }
            else if (!IsValidTransGrid())
            {
                isSalesTrue = false;
            }
            // else if (string.IsNullOrEmpty(glkpCashBankLedger.Text) || glkpCashBankLedger.EditValue.Equals("0") || glkpCashBankLedger.EditValue == null)
            // {
            //     this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.PurchaseVoucher.PURCHASE_ASSET_CASH_BANK_LEDGER_EMPTY));
            //  //   this.SetBorderColorForGridLookUpEdit(glkpCashBankLedger);
            //     isSalesTrue = false;
            ////     glkpCashBankLedger.Focus();
            // }
            return isSalesTrue;
        }

        private void LoadVoucherNo()
        {
            using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
            {
                voucherTransaction.VoucherType = VoucherSubTypes.PY.ToString();
                voucherTransaction.ProjectId = ProjectId;
                voucherTransaction.VoucherDate = this.UtilityMember.DateSet.ToDate(dtSalesDate.Text, false);
                txtVoucherNo.Text = voucherTransaction.TempVoucherNo();
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
                        //     txtNameAddress.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        //     txtNameAddress.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        //    txtNameAddress.MaskBox.AutoCompleteCustomSource = collection;
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

        private void LoadPartyName()
        {
            try
            {
                using (AssetSalesSystem assetSalesSystem = new AssetSalesSystem())
                {
                    resultArgs = assetSalesSystem.FetchSalesMastersByPartyName();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataView dvNarration = resultArgs.DataSource.Table.AsDataView();
                        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            collection.Add(dr[assetSalesSystem.AppSchema.SalesAsset.NAMEColumn.ColumnName].ToString());
                        }
                        txtName.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        txtName.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        txtName.MaskBox.AutoCompleteCustomSource = collection;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void LoadGroups()
        {
            try
            {
                using (AssetClassSystem AssetGroupSystem = new AssetClassSystem())
                {
                    resultArgs = AssetGroupSystem.FetchClassDetails();
                    if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs != null)
                    {
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpGroup, resultArgs.DataSource.Table, AssetGroupSystem.AppSchema.ASSETClassDetails.CLASS_NAMEColumn.ColumnName, AssetGroupSystem.AppSchema.ASSETClassDetails.ASSET_CLASS_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        private void LoadLocation()
        {
            try
            {
                using (LocationSystem LocationSystem = new LocationSystem())
                {
                    resultArgs = LocationSystem.FetchLocationDetails();
                    if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs != null)
                    {
                   //     this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpLocation, resultArgs.DataSource.Table, LocationSystem.AppSchema.ASSETLocationDetails.LOCATION_NAMEColumn.ColumnName, LocationSystem.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        private void LoadLocationByItem()
        {
            try
            {
                using (LocationSystem LocationSystem = new LocationSystem())
                {
                    int ItemId = gvSalesAdd.GetFocusedRowCellValue(colAssName) != null ? this.UtilityMember.NumberSet.ToInteger(gvSalesAdd.GetFocusedRowCellValue(colAssName).ToString()) : 0;
                    resultArgs = LocationSystem.FetchLocaitonByAssetId();
                    if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs != null)
                    {
                        DataView dvSelect = new DataView(resultArgs.DataSource.Table);
                        dvSelect.RowFilter = "ITEM_ID=" + ItemId + "";
                        DataTable dt = dvSelect.ToTable();
                   //     this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpLocation, dvSelect.ToTable(), LocationSystem.AppSchema.ASSETLocationDetails.LOCATION_NAMEColumn.ColumnName, LocationSystem.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
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
                        //      this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpCashBankLedger, resultArgs.DataSource.Table, ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void BindGrid()
        {
            ConstructSalesDetail();
            LoadGroups();
            LoadLocation();
            LoadAssetName();
            LoadAssetIdDetails();
            LoadActiveAssets();
        }

        /// <summary>
        /// Load asset Item by group 
        /// </summary>
        private void LoadAssetByItemId()
        {
            try
            {
                using (AssetItemSystem assetItemSystem = new AssetItemSystem())
                {
                    int ItemId = gvSalesAdd.GetFocusedRowCellValue(colAssName) != null ? this.UtilityMember.NumberSet.ToInteger(gvSalesAdd.GetFocusedRowCellValue(colAssName).ToString()) : 0;
                    int LocationId = gvSalesAdd.GetFocusedRowCellValue(colLocation) != null ? this.UtilityMember.NumberSet.ToInteger(gvSalesAdd.GetFocusedRowCellValue(colLocation).ToString()) : 0;
                    if (dtActiveAsset != null && dtActiveAsset.Rows.Count > 0 && ItemId > 0 && LocationId > 0)
                    {
                        DataView dvAssetItem = new DataView(dtActiveAsset);
                        dvAssetItem.RowFilter = "ITEM_ID=" + ItemId + "AND LOCATION_ID=" + LocationId + "";
                        this.UtilityMember.ComboSet.BindRepositoryItemCheckBoxGridLookUpEdit(rccmbAssetId, dvAssetItem.ToTable(), assetItemSystem.AppSchema.ASSETItem.ASSET_IDColumn.ColumnName,
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

        private void LoadActiveAssets()
        {
            DataTable dtSelectValue = new DataTable();
            try
            {
                using (AssetItemSystem assetItemSystem = new AssetItemSystem())
                {
                    int ItemId = gvSalesAdd.GetFocusedRowCellValue(colAssName) != null ? this.UtilityMember.NumberSet.ToInteger(gvSalesAdd.GetFocusedRowCellValue(colAssName).ToString()) : 0;
                    int LocationId = gvSalesAdd.GetFocusedRowCellValue(colLocation) != null ? this.UtilityMember.NumberSet.ToInteger(gvSalesAdd.GetFocusedRowCellValue(colLocation).ToString()) : 0;
                    string AssetIds = string.Empty;//gvSalesAdd.GetFocusedRowCellValue(colMultAssetId) != null ? (gvSalesAdd.GetFocusedRowCellValue(colMultAssetId).ToString()) : string.Empty;
                    DataTable dtSource = gcSalesAdd.DataSource as DataTable;
                    if (dtSource != null && dtSource.Rows.Count == 0)
                    {
                        if (this.SalesId > 0)
                        {
                            assetItemSystem.SalesId = this.SalesId;
                            resultArgs = assetItemSystem.FetchActiveAssetItemAtEdit();
                        }
                        else
                        {
                            resultArgs = assetItemSystem.FetchActiveAssetItem();
                        }
                        if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            dtActiveAsset = resultArgs.DataSource.Table;
                            DataView dvSelect = new DataView(resultArgs.DataSource.Table);
                            if (this.SalesId == 0)
                            {
                                //dvSelect.RowFilter = "ITEM_ID=" + ItemId + " OR SELECT=0";
                                dvSelect.RowFilter = "ITEM_ID=" + ItemId + "";
                            }
                            this.UtilityMember.ComboSet.BindRepositoryItemCheckBoxGridLookUpEdit(rccmbAssetId, dvSelect.ToTable(), assetItemSystem.AppSchema.ASSETItem.ASSET_IDColumn.ColumnName,
                             assetItemSystem.AppSchema.ASSETItem.ASSET_IDColumn.ColumnName);
                        }
                    }
                    else
                    {
                        if (this.SalesId > 0)
                        {
                            assetItemSystem.SalesId = this.SalesId;
                            resultArgs = assetItemSystem.FetchActiveAssetItemAtEdit();
                            if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
                            {
                                dtActiveAsset = resultArgs.DataSource.Table;
                            }
                        }
                        else
                        {
                            SetSelectedList();
                        }
                        if (dtActiveAsset != null && dtActiveAsset.Rows.Count > 0)
                        {
                            DataView dvSelect = new DataView(dtActiveAsset);
                            //if (this.SalesId == 0)
                            //{
                            if (!string.IsNullOrEmpty(AssetIds))
                            {
                                StringBuilder assetId = new StringBuilder();
                                string[] ids = AssetIds.Split(',');
                                for (int i = 0; i < ids.Count(); i++)
                                {
                                    assetId.Append("'" + ids[i].ToString().Trim() + "',");
                                }
                                AssetIds = assetId.ToString();
                            }
                            dvSelect.RowFilter = !string.IsNullOrEmpty(AssetIds) ? "ITEM_ID=" + ItemId + "AND LOCATION_ID=" + LocationId + " AND SELECT=0 OR ASSET_ID IN (" + AssetIds.TrimEnd(',') + ")"
                                : "ITEM_ID=" + ItemId + "AND LOCATION_ID=" + LocationId + " AND SELECT=0";
                            AssetIds = string.Empty;
                            DataTable dt = dvSelect.ToTable();
                            //}
                            this.UtilityMember.ComboSet.BindRepositoryItemCheckBoxGridLookUpEdit(rccmbAssetId, dvSelect.ToTable(), assetItemSystem.AppSchema.ASSETItem.ASSET_IDColumn.ColumnName,
                             assetItemSystem.AppSchema.ASSETItem.ASSET_IDColumn.ColumnName);
                        }
                    }
                }
                //    using (AssetItemSystem assetItemSystem = new AssetItemSystem())
                //    {
                //        if (this.SalesId > 0)
                //        {
                //            assetItemSystem.SalesId = this.SalesId;
                //            resultArgs = assetItemSystem.FetchActiveAssetItemAtEdit();
                //        }
                //        else
                //        {
                //            resultArgs = assetItemSystem.FetchActiveAssetItem();
                //        }
                //        if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                //        {
                //            dtActiveAsset = resultArgs.DataSource.Table;
                //            this.UtilityMember.ComboSet.BindRepositoryItemCheckBoxGridLookUpEdit(rccmbAssetId, resultArgs.DataSource.Table, assetItemSystem.AppSchema.ASSETItem.ASSET_IDColumn.ColumnName,
                //             assetItemSystem.AppSchema.ASSETItem.ASSET_IDColumn.ColumnName);
                //        }
                //    }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private bool IsValidTransGrid()
        {
            bool isValid = true;
            int RowPosition = 0;
            int ItemId = 0;
            try
            {
                DataTable dtSales = gcSalesAdd.DataSource as DataTable;
                string AssetId = string.Empty;
                decimal Quantity = 0;
                int LocationId = 0;
                DataView dv = new DataView(dtSales);
                dv.RowFilter = "(ITEM_ID>0 OR QUANTITY>0)";
                gvSalesAdd.FocusedColumn = colAssetName;
                if (dv.Count > 0)
                {
                    foreach (DataRowView drSales in dv)
                    {
                        ItemId = this.UtilityMember.NumberSet.ToInteger(drSales[ITEM_ID].ToString());
                        AssetId = drSales[ASSET_ID].ToString();
                        Quantity = this.UtilityMember.NumberSet.ToDecimal(drSales[QUANTITY].ToString());
                        LocationId = this.UtilityMember.NumberSet.ToInteger(drSales[LOCATION_ID].ToString()); ;
                        if (ItemId == 0)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.SalesVoucher.SALES_ASSET_NAME_EMPTY));
                            gvSalesAdd.FocusedColumn = colAssName;
                            isValid = false;
                        }
                        else if (LocationId == 0)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.PurchaseVoucher.PURCHASE_ASSETLOCATION_VALIDATION));
                            gvSalesAdd.FocusedColumn = colAssetName;
                            isValid = false;
                        }
                        else if (string.IsNullOrEmpty(AssetId))
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.SalesVoucher.SALES_ASSET_ID_EMPTY));
                            //  gvSalesAdd.FocusedColumn = colMultAssetId;
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
                    gvSalesAdd.FocusedColumn = colAssName;
                }

                if (!isValid)
                {
                    gvSalesAdd.CloseEditor();
                    gvSalesAdd.FocusedRowHandle = gvSalesAdd.GetRowHandle(RowPosition);
                    gvSalesAdd.ShowEditor();
                }


            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
                isValid = false;
            }
            finally { }
            return isValid;
        }

        private void ConstructSalesDetail()
        {
            try
            {
                DataTable dtSalesDetail = new DataTable();
                dtSalesDetail.Columns.Add("ITEM_ID", typeof(Int32));
                dtSalesDetail.Columns.Add("GROUP_ID", typeof(String));
                dtSalesDetail.Columns.Add("LOCATION_ID", typeof(String));
                dtSalesDetail.Columns.Add("ASSET_ID", typeof(String));
                dtSalesDetail.Columns.Add("QUANTITY", typeof(Int32));
                dtSalesDetail.Columns.Add("PERCENTAGE", typeof(decimal));
                dtSalesDetail.Columns.Add("AMOUNT", typeof(decimal));
                gcSalesAdd.DataSource = dtSalesDetail;
                gvSalesAdd.AddNewRow();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void LoadProjectDate()
        {
            dtSalesDate.DateTime = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtSalesDate.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtSalesDate.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
        }

        private void LoadAssetName()
        {
            try
            {
                using (AssetItemSystem AssetItemSystem = new AssetItemSystem())
                {
                    resultArgs = AssetItemSystem.FetchAssetItemDetails();
                    if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs != null)
                    {
                    //    this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpAssetName, resultArgs.DataSource.Table, AssetItemSystem.AppSchema.ASSETItem.ASSET_NAMEColumn.ColumnName, AssetItemSystem.AppSchema.ASSETItem.ITEM_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {

                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void LoadAssetIdDetails()
        {
            using (AssetItemSystem assetSystem = new AssetItemSystem())
            {
                resultArgs = assetSystem.FetchAssetItemDetails();
                if (resultArgs != null && resultArgs.Success)
                {
                    dtAssetIdDetails = resultArgs.DataSource.Table;
                }
            }
        }

        private void SetTitle()
        {
            ucCaptionTitle.Caption = ProjectName;
            this.Text = SalesId == 0 ? this.GetMessage(MessageCatalog.Asset.SalesVoucher.SALES_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.SalesVoucher.SALES_EDIT_CAPTION);
            if (rgTransactionType.SelectedIndex == 0)
            {
                lblDisplayType.Text = "SALES";
            }
            else if (rgTransactionType.SelectedIndex == 1)
            {
                lblDisplayType.Text = "DISPOSE";
            }
            else if (rgTransactionType.SelectedIndex == 2)
            {
                lblDisplayType.Text = "DONATE";
            }
            else
            {
              //  this.Text = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_TRANSACTION_CONTRA);
            }
        }

        private void ClearControls()
        {
            if (SalesId == 0)
            {
                //     glkpCashBankLedger.Text = string.Empty;
                BindGrid();
                ConstructSalesDetail();
                LoadCashBankLedgers();
                NetAmount = 0;
                //     txtName.Text = txtNameAddress.Text = txtNarration.Text = string.Empty;
                //    txtOtherCharges.Text = txtVoucherNo.Text = string.Empty;
                //  lblTotalAmount.Text = lblTotalDiscount.Text = lblTotalOtherCharges.Text = lblTotalTaxAmount.Text = lblNetAmt.Text = this.UtilityMember.NumberSet.ToCurrency(0.00);
                dtSalesDate.DateTime = this.UtilityMember.DateSet.ToDate(DateTime.Now.ToString(), false);
                LoadVoucherNo();
                this.dtSalesDate.Focus();
            }
            else
            {
                if (this.UIAppSetting.UITransClose == "1")
                {
                    this.Close();
                }
            }
        }

        private void AssignValuesToControls()
        {
            if (SalesId > 0)
            {
                using (AssetSalesSystem assetSalesSystem = new AssetSalesSystem(SalesId))
                {
                    assetSalesSystem.SalesId = SalesId;
                    dtSalesDate.DateTime = assetSalesSystem.SalesDate;
                    txtName.Text = assetSalesSystem.PartyName;
                    VoucherId = assetSalesSystem.VoucherId;
                    NetAmount = this.UtilityMember.NumberSet.ToDecimal(assetSalesSystem.NetAmount.ToString());
                    resultArgs = assetSalesSystem.FetchSalesDetailsBySalesId(SalesId.ToString());
                    if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        gcSalesAdd.DataSource = resultArgs.DataSource.Table;
                        gcSalesAdd.RefreshDataSource();
                    }
                    //     txtDiscountPer.Text = assetSalesSystem.DiscountPercent.ToString();
                    //     txtDiscountAmount.Text = assetSalesSystem.Discount.ToString();
                    //     txtTaxPercent.Text = assetSalesSystem.TaxPercent.ToString();
                    //     txtTaxCalAmt.Text = assetSalesSystem.TaxAmount.ToString();
                    txtOtherCharges.Text = assetSalesSystem.OtherCharges.ToString();
                    //      txtNameAddress.Text = assetSalesSystem.NameAddress;
                    txtNarration.Text = assetSalesSystem.Narration;
                    //      glkpCashBankLedger.EditValue = assetSalesSystem.CashLedgerId;
                    //      lblTotalOtherCharges.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(assetSalesSystem.OtherCharges.ToString()));
                    //      lblNetAmt.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(assetSalesSystem.NetAmount.ToString()));
                    //      lblTotalTaxAmount.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(assetSalesSystem.TaxAmount.ToString()));
                    //      lblTotalDiscount.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(assetSalesSystem.Discount.ToString()));
                    //      lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(assetSalesSystem.TotalAmount.ToString()));
                }
            }
        }

        private void DeleteTransaction()
        {
            try
            {
                if (!string.IsNullOrEmpty(gvSalesAdd.GetFocusedRowCellValue(colAssName).ToString()))
                {
                    if (gvSalesAdd.RowCount > 1)
                    {
                        if (gvSalesAdd.FocusedRowHandle != GridControl.NewItemRowHandle)
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                gvSalesAdd.DeleteRow(gvSalesAdd.FocusedRowHandle);
                                gvSalesAdd.UpdateCurrentRow();
                                gcSalesAdd.RefreshDataSource();
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                gvSalesAdd.FocusedColumn = colAssName;
                            }
                        }
                    }
                    else if (gvSalesAdd.RowCount == 1)
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            ConstructSalesDetail();
                            gcSalesAdd.RefreshDataSource();
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                            gvSalesAdd.FocusedColumn = colAssName;
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Bank.BANK_DELETE));
                    gvSalesAdd.FocusedColumn = colAssName;
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        private void rglkpAssetName_Validating(object sender, CancelEventArgs e)
        {
            int groupId = 0;
            int itemId = 0;
            int locationId = 0;
            GridLookUpEdit gridLKPEdit = sender as GridLookUpEdit;
            if (gridLKPEdit.EditValue != null)
            {
                DataRowView drv = gridLKPEdit.GetSelectedDataRow() as DataRowView;

                if (drv != null)
                {
                    groupId = this.UtilityMember.NumberSet.ToInteger(drv["GROUP_ID"].ToString());
                    itemId = this.UtilityMember.NumberSet.ToInteger(drv["ITEM_ID"].ToString());
                    locationId = this.UtilityMember.NumberSet.ToInteger(drv["LOCATION_ID"].ToString());
                    gvSalesAdd.SetFocusedRowCellValue(colAssName, itemId);
                    gvSalesAdd.SetFocusedRowCellValue(colGroup, groupId);
                    gvSalesAdd.SetFocusedRowCellValue(colLocation, locationId);

                    gvSalesAdd.PostEditor();
                    gvSalesAdd.UpdateCurrentRow();
                }
                LoadAssetByItemId();
                LoadActiveAssets();
            }
        }

        private void gcSalesAdd_Leave(object sender, EventArgs e)
        {
            gvSalesAdd.OptionsSelection.EnableAppearanceFocusedCell = false;
        }

        private void gcSalesAdd_Enter(object sender, EventArgs e)
        {
            gvSalesAdd.OptionsSelection.EnableAppearanceFocusedCell = true;
        }

        private void ribDelete_Click(object sender, EventArgs e)
        {
            DeleteTransaction();
        }

        private void gcSalesAdd_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control)
                {
                    gvSalesAdd.PostEditor();
                    gvSalesAdd.UpdateCurrentRow();
                    LoadActiveAssets();
                    LoadLocationByItem();
                    if (gvSalesAdd.FocusedColumn == colAssName)
                    {
                        if (string.IsNullOrEmpty(gvSalesAdd.GetFocusedRowCellValue(colAssName).ToString()))
                        {
                            gvSalesAdd.CloseEditor();
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                            //    txtDiscountPer.Focus();
                            //    txtDiscountPer.Select();
                        }
                    }

                    //if (gvSalesAdd.FocusedColumn == colMultAssetId && !string.IsNullOrEmpty(gvSalesAdd.GetFocusedRowCellValue(colMultAssetId).ToString()))
                    //{
                    //    GetSelectedAssetValue();
                    //    //Once the Asset Item is Selected Calculate the Total and Net Amount
                    //    CalculateSalesNetAmount();
                    //}

                    //if (gvSalesAdd.IsLastRow && (gvSalesAdd.FocusedColumn == colMultAssetId) &&
                    //    !string.IsNullOrEmpty(gvSalesAdd.GetFocusedRowCellValue(colMultAssetId).ToString()))
                    //{
                    string AssetGroup = gvSalesAdd.GetFocusedRowCellValue(colGroup) != null ? gvSalesAdd.GetFocusedRowCellValue(colGroup).ToString() : string.Empty;
                    string AssetName = gvSalesAdd.GetFocusedRowCellValue(colAssName) != null ? gvSalesAdd.GetFocusedRowCellValue(colAssName).ToString() : string.Empty;
                    string Location = gvSalesAdd.GetFocusedRowCellValue(colLocation) != null ? gvSalesAdd.GetFocusedRowCellValue(colLocation).ToString() : string.Empty;
                    string Quantity = gvSalesAdd.GetFocusedRowCellValue(colQuantity) != null ? gvSalesAdd.GetFocusedRowCellValue(colQuantity).ToString() : string.Empty;
                    string Amount = gvSalesAdd.GetFocusedRowCellValue(colAmount) != null ? gvSalesAdd.GetFocusedRowCellValue(colAmount).ToString() : string.Empty;
                    if (!string.IsNullOrEmpty(AssetGroup) && !string.IsNullOrEmpty(AssetName) && !string.IsNullOrEmpty(Location) && !string.IsNullOrEmpty(Quantity))
                    {
                        gvSalesAdd.AddNewRow();
                        gvSalesAdd.FocusedColumn = gvSalesAdd.Columns[colAssName.Name];
                        LoadActiveAssets();
                        gvSalesAdd.ShowEditor();
                    }
                    else
                    {
                        gvSalesAdd.CloseEditor();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        //    txtDiscountPer.Focus();
                        //    txtDiscountPer.Select();
                    }
                    // }
                }
                else if (gvSalesAdd.IsFirstRow && gvSalesAdd.FocusedColumn == colAssName && e.Shift && e.KeyCode == Keys.Tab)
                {
                    dtSalesDate.Select();
                    dtSalesDate.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void GetSelectedAssetValue()
        {
            using (AssetItemSystem itemSystem = new AssetItemSystem())
            {
                string assetIds = string.Empty;// = gvSalesAdd.GetFocusedRowCellValue(colMultAssetId).ToString();
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
                    gvSalesAdd.SetRowCellValue(gvSalesAdd.FocusedRowHandle, colQuantity, Quantity);
                    gvSalesAdd.SetRowCellValue(gvSalesAdd.FocusedRowHandle, colAmount, ItemTotalRate);
                    gvSalesAdd.ShowEditor();
                }
                else
                {
                    gvSalesAdd.SetRowCellValue(gvSalesAdd.FocusedRowHandle, colQuantity, null);
                    gvSalesAdd.SetRowCellValue(gvSalesAdd.FocusedRowHandle, colAmount, null);
                    gvSalesAdd.ShowEditor();
                }
            }
        }

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
                dtSalesDate.Focus();
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
                    ucCaptionTitle.Caption = ProjectName = projectSelection.ProjectName;
                }
            }
        }
        private void CalculateSalesNetAmount()
        {
            //   lblTotalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(AmountSummaryVal);
            //   lblNetAmt.Text = this.UtilityMember.NumberSet.ToCurrency(CommonMethod.calculateAssetNetPayment(AmountSummaryVal,
            //     this.UtilityMember.NumberSet.ToDouble(txtDiscountAmount.Text),
            //    this.UtilityMember.NumberSet.ToDouble(txtTaxCalAmt.Text), this.UtilityMember.NumberSet.ToDouble(txtOtherCharges.Text)));

            NetAmount = this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) > 0 ? this.UtilityMember.NumberSet.ToDecimal(CommonMethod.NetAmout.ToString()) :
NetAmount;
        }

        //set SELECT FIELD as 1 if the asset id is in checkedState else set SELECT Field to 0 in ActiveAssets
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
        #endregion

        private void gcSalesAdd_Click(object sender, EventArgs e)
        {

        }

        private void rgTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTitle();
        }


    }
}
