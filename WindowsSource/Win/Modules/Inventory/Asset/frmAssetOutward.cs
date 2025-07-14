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
using ACPP.Modules.Inventory.Asset;
using Bosco.DAO.Schema;
using Bosco.Utility.ConfigSetting;
using System.Drawing;
using ACPP.Modules.Inventory;
using ACPP.Modules.Master;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using ACPP.Modules.UIControls;
using DevExpress.XtraBars;

namespace ACPP.Modules.Asset.Transactions
{
    public partial class frmAssetOutward : frmFinanceBaseAdd
    {
        #region VariableDeclaration

        ResultArgs resultArgs = new ResultArgs();
        public event EventHandler UpdateHeld;
        private int InOutId { get; set; }
        private DateTime RecentDate { get; set; }
        AppSchemaSet.ApplicationSchemaSet appSchema = new AppSchemaSet.ApplicationSchemaSet();
        private int PrvQty { get; set; }
        private int AvailQty { get; set; }
        public bool isvalidQty = true;
        DataTable dtFilteredRows = null;
        #endregion

        #region Properties

        private double AmountSummaryVal
        {
            get { return colAmount.SummaryItem.SummaryValue != null ? this.UtilityMember.NumberSet.ToDouble(colAmount.SummaryItem.SummaryValue.ToString()) : 0; }
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

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int VoucherId { get; set; }
        private DataSet dsCostCentre = new DataSet();

        public int ItemId
        {
            get { return gvInOutAdd.GetFocusedRowCellValue(colAsset) != null ? this.UtilityMember.NumberSet.ToInteger(gvInOutAdd.GetFocusedRowCellValue(colAsset).ToString()) : 0; }
        }

        public int InoutDetailId
        {
            get { return gvInOutAdd.GetFocusedRowCellValue(colInoutDetailId) != null ? this.UtilityMember.NumberSet.ToInteger(gvInOutAdd.GetFocusedRowCellValue(colInoutDetailId).ToString()) : 0; }
        }

        int locationId = 0;
        public int LocationId
        {
            get
            {
                return gvInOutAdd.GetFocusedRowCellValue(colLocation) != null ?
                    this.UtilityMember.NumberSet.ToInteger(gvInOutAdd.GetFocusedRowCellValue(colLocation).ToString()) : 0;
            }
        }

        public int Quantity
        {
            get
            {
                return this.UtilityMember.NumberSet.ToInteger(gvInOutAdd.GetFocusedRowCellValue(colQuantity) != null ? gvInOutAdd.GetFocusedRowCellValue(colQuantity).ToString() : string.Empty);
            }
        }
        public int tmpQuantity { get; set; }

        public int AssetTempQuantity { get; set; }

        public double Amount
        {
            get
            {
                return gvInOutAdd.GetFocusedRowCellValue(colAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvInOutAdd.GetFocusedRowCellValue(colAmount).ToString()) : 0;
            }
        }

        private double LedgerAmount
        {
            get
            {
                double ledgerAmount;
                ledgerAmount = gvInOutAdd.GetFocusedRowCellValue(colAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvInOutAdd.GetFocusedRowCellValue(colAmount).ToString()) : 0;
                return ledgerAmount;
            }
        }

        double ledgerccamount;
        private double LedgerCCAmount
        {
            get
            {
                return ledgerccamount;
            }
            set
            {
                ledgerccamount = value;
            }
        }

        int Aid = 0;
        public int AccLedgerId  // For Costcentre usage
        {
            get
            {
                return Aid;
            }
            set { Aid = value; }
        }

        private int AccountLedgerId
        {
            get
            {
                int ledgerId = 0;
                if (ItemId > 0)
                {
                    DataRowView dv = rglkpAssetName.GetRowByKeyValue(ItemId) as DataRowView;
                    if (dv != null)
                        ledgerId = this.UtilityMember.NumberSet.ToInteger(dv.Row["ACCOUNT_LEDGER_ID"].ToString());
                }
                return ledgerId;
            }
        }

        private AssetInOut Flag { get; set; }
        #endregion

        #region Constructor
        public frmAssetOutward()
        {
            InitializeComponent();
        }

        public frmAssetOutward(int projectId, string projectName, int inOutId, string recentDate)
            : this()
        {
            this.ProjectId = projectId;
            ProjectName = projectName;
            InOutId = inOutId;
            RecentDate = UtilityMember.DateSet.ToDate(recentDate, false);
            RealColumnEditTransAmount();
            ucAssetJournal1.ProjectId = this.ProjectId;
            ucAssetJournal1.MinDate = recentDate;
            ucAssetJournal1.NextFocusControl = txtNarration;
            ucAssetJournal1.BeforeFocusControl = gcInOut;
            //  ucAssetJournal1.Flag = Flag;
            ucAssetJournal1.PurchaseTransSummary = this.AmountSummaryVal;
        }

        /// <summary>
        /// Onm 04/09/2024, To check currency based voucher details
        /// </summary>
        private bool IsCurrencyEnabledVoucher
        {
            get
            {
                Int32 currencycountry = glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString());
                double currencyamt = UtilityMember.NumberSet.ToDouble(txtCurrencyAmount.Text);
                double exchagnerate = UtilityMember.NumberSet.ToDouble(txtExchangeRate.Text);
                double liveexchangerate = UtilityMember.NumberSet.ToDouble(lblAvgRate.Text);
                double actalamount = UtilityMember.NumberSet.ToDouble(txtActualAmt.Text);

                return (currencycountry > 0 && currencyamt > 0 && exchagnerate > 0 && actalamount > 0 && liveexchangerate > 0);
            }
        }
        #endregion

        #region Events

        #region Form Events

        private void frmSalesVoucherAdd_Load(object sender, EventArgs e)
        {
            LoadDefaults();
        }

        private void LoadDefaults()
        {
            LoadReceiptType();
            SetTitle();
            ConstructSalesDetail();
            LoadLocation();
            ucAssetJournal1.Flag = AssetInOut.SL;
            this.Flag = ucAssetJournal1.Flag == AssetInOut.SL ? AssetInOut.SL : Flag == AssetInOut.DS ? AssetInOut.DS : AssetInOut.DN;

            //13/12/2024
            //ucAssetJournal1.LoadLedger();

            LoadAccountingDate();
            LoadAssetNameByLocation(this.ProjectId);
            dtSalesDate.DateTime = RecentDate;
            AssignValuesToControls();
            LoadSoldToAutoComplete();
            LoadNarrationAutoComplete();
            dtSalesDate.Select();

            LoadCountry();

            if (VoucherId != 0)
            {
                ucAssetVoucherShortcuts.DisableProject = BarItemVisibility.Never;
            }

            HideShowCurrency();
        }

        private void HideShowCurrency()
        {
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                lciCurrency.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                lciCurrency.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }
        private void ShowCurrencyDetails()
        {
            int CountryId = ucAssetJournal1.CurrencyCountryId = (glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString()));
            lblDonorCurrency.Text = string.Empty;
            txtExchangeRate.Text = "1";
            lblAvgRate.Text = "1.00";
            try
            {
                if (CountryId != 0)
                {
                    using (CountrySystem countrySystem = new CountrySystem())
                    {
                        ResultArgs result = countrySystem.FetchCountryCurrencyExchangeRateByCountryDate(CountryId, UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false));
                        if (result.Success)
                        {
                            lblDonorCurrency.Text = countrySystem.CurrencySymbol;
                            txtExchangeRate.Text = UtilityMember.NumberSet.ToNumber(countrySystem.ExchangeRate);
                            lblAvgRate.Text = UtilityMember.NumberSet.ToNumber(countrySystem.ExchangeRate);

                            // On 12/12/2024 - get Live exchange rate, if we received live exchange rate, let us have live exchange rate
                            AssignLiveExchangeRate();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void CalculateExchangeRate()
        {
            try
            {
                Double ActualAmt = this.UtilityMember.NumberSet.ToDouble(txtCurrencyAmount.Text) * this.UtilityMember.NumberSet.ToDouble(txtExchangeRate.Text);
                lblCalAmt.Text = ActualAmt.ToString();
                txtActualAmt.Text = ActualAmt.ToString();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void LoadCountry()
        {
            try
            {
                using (CountrySystem countrySystem = new CountrySystem())
                {
                    //On 26/08/2024, If multi currency enabled, let us load only currencies with have exchange rate for voucher date
                    if (this.AppSetting.AllowMultiCurrency == 1)
                        resultArgs = countrySystem.FetchCountryCurrencyDetails(this.UtilityMember.DateSet.ToDate(dtSalesDate.Text, false));
                    else
                        resultArgs = countrySystem.FetchCountryDetails();

                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        DataTable dtCurencyCountry = resultArgs.DataSource.Table;
                        dtCurencyCountry.DefaultView.RowFilter = "";

                        //26/08/2024, Load Currecny which have exchange rate
                        if (this.AppSetting.AllowMultiCurrency == 1)
                        {
                            dtCurencyCountry.DefaultView.RowFilter = countrySystem.AppSchema.Country.EXCHANGE_RATEColumn.ColumnName + " >0"; ;
                            dtCurencyCountry = dtCurencyCountry.DefaultView.ToTable();
                        }
                        //this.UtilityMember.ComboSet.BindLookUpEditCombo(glkpCurrencyCountry, resultArgs.DataSource.Table, countrySystem.AppSchema.Country.CURRENCYColumn.ToString(), countrySystem.AppSchema.Country.COUNTRY_IDColumn.ToString());

                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpCurrencyCountry, resultArgs.DataSource.Table,
                            countrySystem.AppSchema.Country.CURRENCYColumn.ToString(), countrySystem.AppSchema.Country.COUNTRY_IDColumn.ToString());

                        //26/08/2024, For new voucher, set default currecny (global setting)
                        if (VoucherId == 0 && this.AppSetting.AllowMultiCurrency == 1)
                        {
                            glkpCurrencyCountry.EditValue = string.IsNullOrEmpty(this.AppSetting.Country) ? 0 : UtilityMember.NumberSet.ToInteger(this.AppSetting.Country);

                            object findcountry = glkpCurrencyCountry.Properties.GetDisplayValueByKeyValue(this.AppSetting.Country);
                            if (findcountry == null) glkpCurrencyCountry.EditValue = null;
                        }

                        ucAssetJournal1.CurrencyCountryId = glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidSalesDetails())
                {
                    using (AssetInwardOutwardSystem assetInwardOutwardSystem = new AssetInwardOutwardSystem())
                    {
                        DataTable dtSource = gcInOut.DataSource as DataTable;
                        assetInwardOutwardSystem.InoutId = InOutId;
                        assetInwardOutwardSystem.InOutDate = this.UtilityMember.DateSet.ToDate(dtSalesDate.Text, false);
                        assetInwardOutwardSystem.BillInvoiceNo = txtBillInvoiceNo.Text.Trim();
                        assetInwardOutwardSystem.VoucherNo = txtVoucherNo.Text.Trim();
                        assetInwardOutwardSystem.SoldTo = txtSoldTo.Text.Trim();
                        assetInwardOutwardSystem.Narration = txtNarration.Text.Trim();
                        assetInwardOutwardSystem.Flag = rgTransactionType.SelectedIndex == 0 ? AssetInOut.SL.ToString() : rgTransactionType.SelectedIndex == 1 ? AssetInOut.DS.ToString() : AssetInOut.DN.ToString();
                        assetInwardOutwardSystem.ProjectId = this.ProjectId;

                        assetInwardOutwardSystem.AssetCurrencyCountryId = ucAssetJournal1.CurrencyCountryId = glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString());
                        assetInwardOutwardSystem.AssetExchangeRate = this.UtilityMember.NumberSet.ToDecimal(txtExchangeRate.Text);
                        // inwardvouchersystem.AssetCurrencyAmount = this.UtilityMember.NumberSet.ToDecimal(txtCurrencyAmount.Text);
                        assetInwardOutwardSystem.AssetExchageCountryId = this.glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString());
                        assetInwardOutwardSystem.AssetLiveExchangeRate = this.UtilityMember.NumberSet.ToDecimal(lblAvgRate.Text);

                        assetInwardOutwardSystem.VoucherId = VoucherId;
                        if (dtSource != null && dtSource.Rows.Count > 0)
                            assetInwardOutwardSystem.TotalAmount = UtilityMember.NumberSet.ToDouble(dtSource.Compute("SUM(AMOUNT)", string.Empty).ToString());

                        DataTable dtBankTrans = ConstructCashBankLedgerSource(ucAssetJournal1.DtCashBank);
                        dtSource.AsEnumerable().ToList<DataRow>().ForEach(dr =>
                    {
                        if (dr.RowState == DataRowState.Deleted)
                            dr.Delete();
                    });
                        dtSource.AcceptChanges();
                        dtBankTrans.AsEnumerable().ToList<DataRow>().ForEach(dr =>
                        {
                            if (dr.RowState == DataRowState.Deleted)
                                dr.Delete();
                        });
                        dtBankTrans.AcceptChanges();
                        // Asset Masters
                        dtFilteredRows = dtSource.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull)).CopyToDataTable();
                        DataTable dtTemp = dtFilteredRows.Copy();
                        assetInwardOutwardSystem.dtinoutword = dtTemp;
                        this.Transaction.CostCenterInfo = dsCostCentre;
                        // Asset Vouchers
                        assetInwardOutwardSystem.dtAssetVoucher = ConstructGeneralLedgerSource(dtFilteredRows, ucAssetJournal1.DtCashBank);

                        // assetInwardOutwardSystem.dtinoutword = dtSource;

                        if (dtBankTrans.Rows.Count > 0)
                            dtFilteredRows = dtBankTrans.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull)).CopyToDataTable();
                        assetInwardOutwardSystem.dtCashBank = dtBankTrans;
                        resultArgs = assetInwardOutwardSystem.SaveAssetInwardOutward();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            ClearAssetCommonProperties();
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            ClearControls();
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                            dtSalesDate.Focus();
                        }
                        else
                        {
                            this.ShowMessageBox(resultArgs.Message.ToString());
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

        /// <summary>
        // On 12/12/2024 - get Live exchange rate, if we received live exchange rate, let us have live exchange rate
        /// </summary>
        private void AssignLiveExchangeRate()
        {
            lblAvgRate.ForeColor = Color.Black;
            lblliveAvgRate.AppearanceItemCaption.ForeColor = Color.Black;
            lblAvgRate.Font = new System.Drawing.Font(lblAvgRate.Font.FontFamily, lblAvgRate.Font.Size, FontStyle.Regular);
            lblliveAvgRate.AppearanceItemCaption.Font = new System.Drawing.Font(lblliveAvgRate.AppearanceItemCaption.Font.FontFamily,
                    lblliveAvgRate.AppearanceItemCaption.Font.Size, FontStyle.Regular);

            int CountryId = (glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString()));
            using (CountrySystem countrySystem = new CountrySystem())
            {
                ResultArgs result = countrySystem.FetchCountryCurrencyExchangeRateByCountryDate(CountryId, UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false));
                if (result.Success)
                {
                    lblAvgRate.Text = UtilityMember.NumberSet.ToNumber(countrySystem.ExchangeRate);
                    this.ShowWaitDialog("Fetching Live Exchange Rate");
                    double liveExchangeAmount = this.AppSetting.CurrencyLiveExchangeRate(dtSalesDate.DateTime.Date, countrySystem.CurrencyCode, AppSetting.CurrencyCode);
                    this.CloseWaitDialog();
                    if (liveExchangeAmount > 0)
                    {
                        lblAvgRate.Text = UtilityMember.NumberSet.ToNumber(liveExchangeAmount);

                        lblAvgRate.ForeColor = Color.Green;
                        lblliveAvgRate.AppearanceItemCaption.ForeColor = Color.Green;
                        lblAvgRate.Font = new System.Drawing.Font(lblAvgRate.Font.FontFamily, lblAvgRate.Font.Size, (FontStyle.Bold | FontStyle.Underline));
                        lblliveAvgRate.AppearanceItemCaption.Font = new System.Drawing.Font(lblliveAvgRate.AppearanceItemCaption.Font.FontFamily,
                                lblliveAvgRate.AppearanceItemCaption.Font.Size, (FontStyle.Bold | FontStyle.Underline));
                    }
                }
            }
        }

        private DataTable ConstructCashBankLedgerSource(DataTable dtCashbank)
        {
            DataTable dtBankTrans = dtCashbank;
            DataView dvcashbank = dtBankTrans.AsDataView();
            if (rgTransactionType.SelectedIndex == 0)
            {
                dvcashbank.RowFilter = "GROUP_ID IN (12,13)";
                dtBankTrans = dvcashbank.ToTable();
            }
            dtBankTrans.AcceptChanges();
            return dtBankTrans;
        }

        private DataTable ConstructGeneralLedgerSource(DataTable dtFilteredRows, DataTable dtCashBank)
        {
            DataTable dtLedgers = dtFilteredRows;
            DataTable dtBankTrans = dtCashBank;
            DataTable dtTempBank = dtBankTrans.Clone();
            if (rgTransactionType.SelectedIndex == 0)
            {
                DataView dv = dtBankTrans.AsDataView();
                dv.RowFilter = "GROUP_ID NOT IN (12,13)";
                dtTempBank = dv.ToTable();

                foreach (DataRow dr in dtLedgers.Rows)
                {
                    dr["AMOUNT"] = this.UtilityMember.NumberSet.ToDecimal(dr["TEMP_AMOUNT"].ToString());
                }

                dtLedgers.Merge(dtTempBank);
            }
            else
            {
                if (!dtLedgers.Columns.Contains("DEBIT"))
                    dtLedgers.Columns.Add("DEBIT", typeof(decimal));
                if (!dtLedgers.Columns.Contains("CREDIT"))
                    dtLedgers.Columns.Add("CREDIT", typeof(decimal));
                if (!dtLedgers.Columns.Contains("NARRATION"))
                    dtLedgers.Columns.Add("NARRATION", typeof(string));

                if (!dtBankTrans.Columns.Contains("DEBIT"))
                    dtBankTrans.Columns.Add("DEBIT", typeof(decimal));
                if (!dtBankTrans.Columns.Contains("CREDIT"))
                    dtBankTrans.Columns.Add("CREDIT", typeof(decimal));
                if (!dtBankTrans.Columns.Contains("NARRATION"))
                    dtBankTrans.Columns.Add("NARRATION", typeof(string));

                if (!(ucAssetJournal1.Flag == AssetInOut.DS || ucAssetJournal1.Flag == AssetInOut.DN)) // Done by chinna in order to pass dr cr 23.03.2021 at 11 AM
                {
                    foreach (DataRow dr in dtLedgers.Rows)
                    {
                        dr["DEBIT"] = this.UtilityMember.NumberSet.ToDecimal(dr["AMOUNT"].ToString());
                        dr["CREDIT"] = 0;
                    }
                    foreach (DataRow dr in dtBankTrans.Rows)
                    {
                        dr["CREDIT"] = this.UtilityMember.NumberSet.ToDecimal(dr["AMOUNT"].ToString());
                        dr["DEBIT"] = 0;
                    }
                }
                else
                {
                    foreach (DataRow dr in dtLedgers.Rows)
                    {
                        dr["CREDIT"] = this.UtilityMember.NumberSet.ToDecimal(dr["AMOUNT"].ToString());
                        dr["DEBIT"] = 0;
                    }
                    foreach (DataRow dr in dtBankTrans.Rows)
                    {
                        dr["DEBIT"] = this.UtilityMember.NumberSet.ToDecimal(dr["AMOUNT"].ToString());
                        dr["CREDIT"] = 0;
                    }
                }
                dtBankTrans.AcceptChanges();
                dtLedgers.Merge(dtBankTrans);
            }
            return dtLedgers;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ConstructSalesDetail();
            LoadCashBankLedgers();
            txtBillInvoiceNo.Text = txtSoldTo.Text = txtOtherCharges.Text = txtNarration.Text = string.Empty;
            LoadVoucherNo();
            ucAssetJournal1.ConstructCashTransEmptySournce();
            this.dtSalesDate.Focus();

            ClearAssetCommonProperties();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBillInvoiceNo_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtBillInvoiceNo);
        }

        private void txtSoldTo_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtSoldTo);
            gvInOutAdd.Focus();
            gvInOutAdd.MoveFirst();
            gvInOutAdd.FocusedColumn = colAsset;
        }

        private void frmAssetOutward_KeyDown(object sender, KeyEventArgs e)
        {
            ProcessShortcutKeys(e);
        }

        private void frmAssetOutward_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClearAssetCommonProperties();
        }

        private void rgTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ucAssetJournal1.Flag = rgTransactionType.SelectedIndex == 0 ? AssetInOut.SL : rgTransactionType.SelectedIndex == 1 ? AssetInOut.DS : AssetInOut.DN;
            this.Flag = ucAssetJournal1.Flag = rgTransactionType.SelectedIndex == 0 ? AssetInOut.SL : rgTransactionType.SelectedIndex == 1 ? AssetInOut.DS : AssetInOut.DN;
            ucAssetJournal1.ConstructCashTransEmptySournce();
            ucAssetJournal1.LoadLedger();
            SetTitle();
            LoadVoucherNo();
            colActualAmount.Visible = true;
            colDifference.Visible = true;
            colType.Visible = true;
            if (ucAssetJournal1.Flag == AssetInOut.DN || ucAssetJournal1.Flag == AssetInOut.DS)
            {
                colActualAmount.Visible = false;
                colDifference.Visible = false;
                colType.Visible = false;
                layoutControlItem13.Text = "Bill / Invoice No";
                lblName.Text = "Outward to";
                if (ucAssetJournal1.Flag != AssetInOut.DS)
                    lblName.Text = "Outward to <color=red>*";

            }
            else
            {
                layoutControlItem13.Text = "Bill / Invoice No <color=red>*";
                lblName.Text = "Outward to <color=red>*";
                colActualAmount.Visible = true;
                colDifference.Visible = true;
                colType.Visible = true;
                colActualAmount.VisibleIndex = 3;
                colAmount.VisibleIndex = 4;
                colDifference.VisibleIndex = 5;
                colType.VisibleIndex = 6;
            }
        }

        #endregion

        #region Grid Events

        private void gcSalesAdd_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control)
                {
                    gvInOutAdd.PostEditor();
                    gvInOutAdd.UpdateCurrentRow();
                    int item = this.UtilityMember.NumberSet.ToInteger(gvInOutAdd.GetFocusedRowCellValue(colAsset) != null ? gvInOutAdd.GetFocusedRowCellValue(colAsset).ToString() : string.Empty);
                    if (item > 0)
                    {
                        gvInOutAdd.SetFocusedRowCellValue(colLedgerId, this.AccountLedgerId);
                        AccLedgerId = this.AccountLedgerId;
                        //  LoadAssetNameByLocation(this.ProjectId, location);
                        if (gvInOutAdd.FocusedColumn == colAsset)
                        {
                            gvInOutAdd.FocusedColumn = gvInOutAdd.Columns.ColumnByName(colAsset.Name);
                            gvInOutAdd.ShowEditor();
                            if (InOutId == 0)
                            {
                                AvailQty = AvailableQuantity();
                                AvailQty = AvailQty - Quantity;
                                if (Quantity <= AvailableQuantity())
                                {
                                    gvInOutAdd.SetFocusedRowCellValue(colAvailableQuantity, AvailQty);
                                }
                                else
                                {
                                    gvInOutAdd.SetFocusedRowCellValue(colQuantity, 0);
                                    gvInOutAdd.SetFocusedRowCellValue(colAvailableQuantity, AvailQty);
                                }
                            }
                            else
                            {
                                AvailQty = AvailableQuantity();
                                int TotalQuantity = AvailQty + AssetTempQuantity;
                                AvailQty = (AvailQty + AssetTempQuantity) - Quantity;
                                if (Quantity <= TotalQuantity)
                                {
                                    gvInOutAdd.SetFocusedRowCellValue(colAvailableQuantity, AvailQty);
                                }
                                else
                                {
                                    gvInOutAdd.SetFocusedRowCellValue(colQuantity, 0);
                                    gvInOutAdd.SetFocusedRowCellValue(colAvailableQuantity, AvailableQuantity());
                                }
                            }
                            tmpQuantity = 0;
                        }
                        if (gvInOutAdd.FocusedColumn == colQuantity) // || gvInOutAdd.FocusedColumn == colAsset)
                        {
                            gvInOutAdd.FocusedColumn = gvInOutAdd.Columns.ColumnByName(colQuantity.Name);
                            gvInOutAdd.ShowEditor();
                            AssetGenerationList();
                            gvInOutAdd.FocusedColumn = colAsset;
                            gvInOutAdd.FocusedColumn = gvInOutAdd.Columns["0"];//sudhakar//
                        }
                        else if (gvInOutAdd.IsFirstRow && gvInOutAdd.FocusedColumn == colAssetName && e.Shift && e.KeyCode == Keys.Tab)
                        {
                            gvInOutAdd.CloseEditor();
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                            txtSoldTo.Select();
                            txtSoldTo.Focus();
                        }
                    }
                    else
                    {
                        gvInOutAdd.CloseEditor();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        //  txtNarration.Focus();
                        //  txtNarration.Select();
                        ucAssetJournal1.Focus();
                    }
                }
                else if (gvInOutAdd.IsFirstRow && gvInOutAdd.FocusedColumn == colAsset && e.Shift && e.KeyCode == Keys.Tab)
                {
                    gvInOutAdd.CloseEditor();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    txtSoldTo.Select();
                    txtSoldTo.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void AssetGenerationList()
        {
            if (!string.IsNullOrEmpty(gvInOutAdd.GetFocusedRowCellValue(colQuantity).ToString()))
            {
                int rowid = gvInOutAdd.GetFocusedDataSourceRowIndex();
                gvInOutAdd.CloseEditor();
                //e.Handled = true;
                //e.SuppressKeyPress = true;

                if (Quantity > 0)
                {
                    if (!CheckQuantityExceeds(Quantity))
                    {
                        frmAssetItemList AssetItemList = new frmAssetItemList(ItemId, Quantity, rowid, InoutDetailId, rgTransactionType.SelectedIndex == 0 ? AssetInOut.SL : rgTransactionType.SelectedIndex == 1 ? AssetInOut.DS : AssetInOut.DN, ProjectId, "", dtSalesDate.DateTime.ToShortDateString());
                        AssetItemList.AMCVisible = false;
                        AssetItemList.CostCentreVisibile = false;
                        AssetItemList.InsuranceVisibile = false;
                        AssetItemList.DeleteVisibile = false;
                        AssetItemList.CommonLookupVisibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        AssetItemList.ShowDialog();
                        if (AssetItemList.Dialogresult == DialogResult.OK)
                        {
                            if (AssetItemList.Quantity > 0)
                            {
                                gvInOutAdd.SetFocusedRowCellValue(colQuantity, AssetItemList.Quantity);
                                gvInOutAdd.SetFocusedRowCellValue(colTemp, AssetItemList.PurchaseAmount);
                                double tmpActAmt = this.UtilityMember.NumberSet.ToDouble(AssetItemList.PurchaseAmount.ToString());
                                gvInOutAdd.SetFocusedRowCellValue(colActualAmount, this.UtilityMember.NumberSet.ToNumber(tmpActAmt));
                                gvInOutAdd.UpdateTotalSummary();

                                double difference = this.UtilityMember.NumberSet.ToDouble(AssetItemList.Amount.ToString()) - tmpActAmt;
                                gvInOutAdd.SetFocusedRowCellValue(colDifference, Math.Abs(difference));

                                decimal amount = AssetItemList.Amount;
                                LedgerCCAmount = UtilityMember.NumberSet.ToDouble(amount.ToString());  // for costcentre purpose

                                if (difference > 0)
                                {
                                    gvInOutAdd.SetFocusedRowCellValue(colType, "Gain");
                                }
                                else if (difference < 0)
                                {
                                    gvInOutAdd.SetFocusedRowCellValue(colType, "Loss");
                                }
                                else
                                {
                                    gvInOutAdd.SetFocusedRowCellValue(colType, "");
                                }

                                tmpQuantity = AssetItemList.Quantity;
                                //    gvInOutAdd.SetFocusedRowCellValue(colAvailableQuantity, AvailQty - tmpQuantity);
                                //     gvInOutAdd.SetFocusedRowCellValue(colAvailableQuantity, ((AvailQty + tmpQuantity) - AssetItemList.Quantity));
                                if (InOutId == 0)
                                {
                                    AvailQty = AvailableQuantity();
                                    AvailQty = AvailQty - Quantity;
                                    if (Quantity <= AvailableQuantity())
                                    {
                                        gvInOutAdd.SetFocusedRowCellValue(colAvailableQuantity, AvailQty);
                                    }
                                    else
                                    {
                                        gvInOutAdd.SetFocusedRowCellValue(colQuantity, 0);
                                        gvInOutAdd.SetFocusedRowCellValue(colAvailableQuantity, AvailQty);
                                    }
                                }
                                else
                                {
                                    AvailQty = AvailableQuantity();
                                    int TotalQuantity = AvailQty + AssetTempQuantity;
                                    AvailQty = (AvailQty + AssetTempQuantity) - Quantity;
                                    if (Quantity <= TotalQuantity)
                                    {
                                        gvInOutAdd.SetFocusedRowCellValue(colAvailableQuantity, AvailQty);
                                    }
                                    else
                                    {
                                        gvInOutAdd.SetFocusedRowCellValue(colQuantity, 0);
                                        gvInOutAdd.SetFocusedRowCellValue(colAvailableQuantity, AvailableQuantity());
                                    }
                                }
                            }
                            else
                            {
                                gvInOutAdd.SetFocusedRowCellValue(colQuantity, 0);
                                gvInOutAdd.SetFocusedRowCellValue(colAmount, 0);
                                gvInOutAdd.SetFocusedRowCellValue(colAvailableQuantity, AvailableQuantity());
                            }
                            gvInOutAdd.UpdateCurrentRow();
                            gvInOutAdd.UpdateSummary();
                            if (AssetItemList.Amount > 0)
                                gvInOutAdd.SetFocusedRowCellValue(colAmount, AssetItemList.Amount);
                        }
                        else
                        {
                            gvInOutAdd.SetFocusedRowCellValue(colAvailableQuantity, AvailQty - tmpQuantity);
                            if (InOutId == 0)
                            {
                                if (Quantity <= AvailQty)
                                    gvInOutAdd.SetFocusedRowCellValue(colQuantity, tmpQuantity);
                                //  gvInOutAdd.SetFocusedRowCellValue(colQuantity, Quantity);
                                else
                                    gvInOutAdd.SetFocusedRowCellValue(colQuantity, Quantity);
                                //   gvInOutAdd.SetFocusedRowCellValue(colQuantity, tmpQuantity);
                            }
                            else
                            {
                                gvInOutAdd.SetFocusedRowCellValue(colQuantity, tmpQuantity);
                                gvInOutAdd.SetFocusedRowCellValue(colAvailableQuantity, AvailableQuantity());
                            }
                            if (AssetItemList.Amount == 0 && SettingProperty.AssetListCollection.ContainsKey(rowid))
                                SettingProperty.AssetListCollection.Remove(rowid);
                        }

                        if (Amount > 0 && Quantity > 0 && ItemId > 0)
                        {
                            if (rgTransactionType.SelectedIndex == 0)// || rgTransactionType.SelectedIndex == 1)
                            {
                                calculategainLossLedger();
                            }
                            else
                            {
                                //   if (rgTransactionType.SelectedIndex == 1 ?ucAssetJournal1.Flag=AssetInOut.DS:ucAssetJournal1.Flag=AssetInOut.DN;) 
                                //  -- { ucAssetJournal1.Flag = AssetInOut.DN; };
                                ucAssetJournal1.Flag = rgTransactionType.SelectedIndex == 1 ? AssetInOut.DS : AssetInOut.DN;
                                gvInOutAdd.UpdateTotalSummary();
                                ucAssetJournal1.SetCashLedger(AmountSummaryVal);
                            }

                            if (gvInOutAdd.IsLastRow)
                            {
                                gvInOutAdd.AddNewRow();
                                gvInOutAdd.SetRowCellValue(gvInOutAdd.FocusedRowHandle, colSource, (int)Source.To);
                                gvInOutAdd.FocusedColumn = gvInOutAdd.Columns["0"];//sudhakar
                            }
                            else
                                gvInOutAdd.MoveNext();

                            gvInOutAdd.UpdateCurrentRow();
                            gvInOutAdd.FocusedColumn = colAsset;
                            gvInOutAdd.FocusedColumn = gvInOutAdd.Columns["0"];//sudhakar
                            gvInOutAdd.ShowEditor();
                        }
                        else
                        {
                            gvInOutAdd.SetFocusedRowCellValue(colQuantity, 0);
                            //     gvInOutAdd.SetFocusedRowCellValue(colAmount, 0);
                            gvInOutAdd.CloseEditor();
                            //e.Handled = true;
                            //e.SuppressKeyPress = true;
                        }
                        ShowCostCentre(LedgerCCAmount, false);
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Stock.StockPurcahseReturns.STOCK_QUANTITY_EXCEEDS));
                        gvInOutAdd.FocusedColumn = colQuantity;
                    }
                }

                if (this.AppSetting.AllowMultiCurrency == 1)
                {
                    txtCurrencyAmount.Text = ucAssetJournal1.BankTransSummaryVal.ToString();
                    Double ActualValues = this.UtilityMember.NumberSet.ToDouble(ucAssetJournal1.BankTransSummaryVal.ToString()) * this.UtilityMember.NumberSet.ToDouble(txtExchangeRate.Text);
                    lblCalAmt.Text = this.UtilityMember.NumberSet.ToNumber(ActualValues).ToString();
                    txtActualAmt.Text = this.UtilityMember.NumberSet.ToNumber(ActualValues).ToString();
                }

            }
            else
            {
                gvInOutAdd.FocusedColumn = colAsset;
            }

        }

        private void calculategainLossLedger()
        {
            double tempAmount = 0;
            double ActAmount = 0;

            int ListCount = SettingProperty.AssetListCollection.Count;
            DataTable dt = new DataTable();
            for (int i = 0; i < ListCount; i++)
            {
                if (SettingProperty.AssetListCollection.ContainsKey(i))
                {
                    dt = SettingProperty.AssetListCollection[i];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        var SelectedRecords = from asset in dt.AsEnumerable()
                                              where asset.RowState != DataRowState.Deleted ? asset.Field<Int32?>("SELECT") == 1 : false
                                              select asset;
                        foreach (DataRow item in SelectedRecords)
                        {
                            tempAmount += this.UtilityMember.NumberSet.ToDouble(item["TEMP_AMOUNT"].ToString());
                            ActAmount += this.UtilityMember.NumberSet.ToDouble(item["AMOUNT"].ToString());
                        }
                    }
                }
            }
            if (SettingProperty.AssetListCollection.Count > 0)
            {
                if (ActAmount > tempAmount)
                {
                    // gain Ledger
                    ucAssetJournal1.Flag = AssetInOut.GAIN;
                    ucAssetJournal1.LoadLedger();
                    ucAssetJournal1.ConstructCashTransEmptySournce();
                    ucAssetJournal1.PurchaseTransSummary = tempAmount; // +(ActAmount - tempAmount);
                    ucAssetJournal1.SetCashLedger(ActAmount);
                    ucAssetJournal1.SetGainLossLedger(ActAmount - tempAmount);
                }
                else if (tempAmount > ActAmount)
                {
                    // Loss Ledger
                    ucAssetJournal1.Flag = AssetInOut.LOSS;
                    ucAssetJournal1.LoadLedger();
                    ucAssetJournal1.ConstructCashTransEmptySournce();
                    ucAssetJournal1.PurchaseTransSummary = tempAmount; // +(tempAmount - ActAmount);
                    ucAssetJournal1.SetCashLedger(ActAmount);
                    ucAssetJournal1.SetGainLossLedger(tempAmount - ActAmount);
                }
                else
                {
                    gvInOutAdd.UpdateCurrentRow();
                    ucAssetJournal1.ConstructCashTransEmptySournce();
                    ucAssetJournal1.SetCashLedger(AmountSummaryVal);
                }
            }
        }

        private void rglkpAssetName_Enter(object sender, EventArgs e)
        {
            AssetTempQuantity = this.UtilityMember.NumberSet.ToInteger(gvInOutAdd.GetFocusedRowCellValue(colQuantity) != null ? gvInOutAdd.GetFocusedRowCellValue(colQuantity).ToString() : string.Empty);
        }

        private void ribDelete_Click(object sender, EventArgs e)
        {
            DeleteTransaction();
        }

        private void rtxtQuantity_EditValueChanging(object sender, ChangingEventArgs e)
        {
            //if (tmpQuantity == 0)
            //    tmpQuantity = Quantity;
        }

        private void rtxtQuantity_Enter(object sender, EventArgs e)
        {
            //   if (tmpQuantity == 0)
            tmpQuantity = Quantity;
            AssetTempQuantity = this.UtilityMember.NumberSet.ToInteger(gvInOutAdd.GetFocusedRowCellValue(colQuantity) != null ? gvInOutAdd.GetFocusedRowCellValue(colQuantity).ToString() : string.Empty);
        }

        private void dtSalesDate_EditValueChanged(object sender, EventArgs e)
        {
            FetchVoucherMethod();
            if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic && VoucherId == 0)
            {
                LoadVoucherNo();
            }
            else if (TransVoucherMethod == (int)TransactionVoucherMethod.Manual && VoucherId == 0)
            {
                txtVoucherNo.Enabled = true;
                txtVoucherNo.Text = string.Empty;
            }

            // On 12/12/2024 - get Live exchange rate, if we received live exchange rate, let us have live exchange rate
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                AssignLiveExchangeRate();
            }
        }

        private void rbtView_Click(object sender, EventArgs e)
        {
            //int rowid = gvInOutAdd.GetVisibleIndex(gvInOutAdd.FocusedRowHandle);
            //if (Quantity > 0)
            //{
            //    frmAssetItemList AssetItemList = new frmAssetItemList(ItemId, Quantity, rowid, InoutDetailId, rgTransactionType.SelectedIndex == 0 ? AssetInOut.SL : rgTransactionType.SelectedIndex == 1 ? AssetInOut.DS : AssetInOut.DN, ProjectId);
            //    AssetItemList.AMCVisible = false;
            //    AssetItemList.CostCentreVisibile = false;
            //    AssetItemList.InsuranceVisibile = false;
            //    AssetItemList.DeleteVisibile = false;
            //    AssetItemList.CommonLookupVisibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    AssetItemList.ShowDialog();
            //    if (AssetItemList.Dialogresult == DialogResult.OK)
            //    {
            //        if (AssetItemList.Quantity > 0)
            //        {
            //            gvInOutAdd.SetFocusedRowCellValue(colQuantity, AssetItemList.Quantity);
            //            tmpQuantity = AssetItemList.Quantity;
            //        }
            //        if (AssetItemList.Amount > 0)
            //            gvInOutAdd.SetFocusedRowCellValue(colAmount, AssetItemList.Amount);
            //    }
            //    else
            //    {
            //        gvInOutAdd.SetFocusedRowCellValue(colQuantity, tmpQuantity);
            //    }
            //}
            //else
            //    gvInOutAdd.FocusedColumn = colAsset;

            AssetGenerationList();
        }

        void RealColumnQuantity_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvInOutAdd.PostEditor();
            gvInOutAdd.UpdateCurrentRow();
            if (gvInOutAdd.ActiveEditor == null)
            {
                gvInOutAdd.ShowEditor();
            }
            if (InOutId == 0)
            {
                AvailQty = AvailableQuantity();
                AvailQty = AvailQty - Quantity;
                if (Quantity <= AvailableQuantity())
                {
                    gvInOutAdd.SetFocusedRowCellValue(colAvailableQuantity, AvailQty);
                }
                else
                {
                    gvInOutAdd.SetFocusedRowCellValue(colQuantity, 0);
                    gvInOutAdd.SetFocusedRowCellValue(colAvailableQuantity, AvailableQuantity());
                }
            }
            else
            {
                AvailQty = AvailableQuantity();
                int TotalQuantity = AvailQty + tmpQuantity;
                AvailQty = (AvailQty + tmpQuantity) - Quantity;
                if (Quantity <= TotalQuantity)
                {
                    gvInOutAdd.SetFocusedRowCellValue(colAvailableQuantity, AvailQty);
                }
                else
                {
                    gvInOutAdd.SetFocusedRowCellValue(colQuantity, 0);
                    gvInOutAdd.SetFocusedRowCellValue(colAvailableQuantity, AvailableQuantity() + tmpQuantity);
                }
            }
        }

        private void gvInOutAdd_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //   ShowAssetListForm();
        }

        private void ShowAssetListForm()
        {
            int rowid = gvInOutAdd.GetFocusedDataSourceRowIndex();
            if (gvInOutAdd.FocusedColumn == colQuantity)
            {
                if (!CheckQuantityExceeds(Quantity))
                {
                    if (Quantity > 0)
                    {
                        frmAssetItemList AssetItemList = new frmAssetItemList(ItemId, Quantity, rowid, InoutDetailId, rgTransactionType.SelectedIndex == 0 ? AssetInOut.SL : rgTransactionType.SelectedIndex == 1 ? AssetInOut.DS : AssetInOut.DN, ProjectId);
                        AssetItemList.AMCVisible = false;
                        AssetItemList.CostCentreVisibile = false;
                        AssetItemList.InsuranceVisibile = false;
                        AssetItemList.DeleteVisibile = false;
                        AssetItemList.CommonLookupVisibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        AssetItemList.ShowDialog();
                    }
                }
            }
        }

        private void gvInOutAdd_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "ITEM_ID" && IsDuplicatedValue((sender as GridView), e.Column, e.Value))
            {
                (sender as GridView).CancelUpdateCurrentRow();
            }
        }

        #endregion

        #region User control Events

        private void ucAssetVoucherShortcuts_VendorClicked(object sender, EventArgs e)
        {
            frmVendorInfoAdd vendorAdd = new frmVendorInfoAdd(0, VendorManufacture.Vendor);
            vendorAdd.ShowDialog();
        }

        private void ucAssetVoucherShortcuts_DateClicked(object sender, EventArgs e)
        {
            dtSalesDate.Focus();
        }

        private void ucAssetVoucherShortcuts_ProjectClicked(object sender, EventArgs e)
        {
            ShowProjectSelectionWindow();
        }

        private void ucAssetVoucherShortcuts_ConfigureClicked(object sender, EventArgs e)
        {
            frmAssetSettings assetsetting = new frmAssetSettings();
            assetsetting.ShowDialog();
        }

        private void ucAssetVoucherShortcuts_NextDateClicked(object sender, EventArgs e)
        {
            DateTime dtYearTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            dtSalesDate.DateTime = (dtSalesDate.DateTime < dtYearTo) ? dtSalesDate.DateTime.AddDays(1) : dtYearTo;
        }

        private void ucAssetVoucherShortcuts_MappingClicked(object sender, EventArgs e)
        {
            frmMapLocation maplocation = new frmMapLocation();
            maplocation.ShowDialog();
        }

        private void ucAssetVoucherShortcuts_ManufacturerClicked(object sender, EventArgs e)
        {
            frmVendorInfoAdd Manufacturer = new frmVendorInfoAdd(0, VendorManufacture.Manufacture);
            Manufacturer.ShowDialog();
        }

        private void ucAssetVoucherShortcuts_LocationClicked(object sender, EventArgs e)
        {
            frmLocationsAdd locationAdd = new frmLocationsAdd();
            locationAdd.ShowDialog();
            LoadLocation();
        }

        private void ucAssetVoucherShortcuts_CustodianClicked(object sender, EventArgs e)
        {
            frmCustodiansAdd custodianAdd = new frmCustodiansAdd();
            custodianAdd.ShowDialog();
        }

        private void ucAssetVoucherShortcuts_BankAccountClicked(object sender, EventArgs e)
        {
            ACPP.Modules.Master.frmLedgerDetailAdd frmBank = new ACPP.Modules.Master.frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.BK, ProjectId);
            frmBank.ShowDialog();
            LoadCashBankLedgers();
        }

        private void ucAssetVoucherShortcuts_AssetItemClicked(object sender, EventArgs e)
        {
            frmAssetItemAdd itemAdd = new frmAssetItemAdd();
            itemAdd.ShowDialog();
        }

        private void ucAssetVoucherShortcuts_SalesClicked(object sender, EventArgs e)
        {
            rgTransactionType.SelectedIndex = 0;
        }

        private void ucAssetVoucherShortcuts_DonationClicked(object sender, EventArgs e)
        {
            rgTransactionType.SelectedIndex = 2;
        }

        private void ucAssetVoucherShortcuts_DisposeClicked(object sender, EventArgs e)
        {
            rgTransactionType.SelectedIndex = 1;
        }

        private void ucAssetVoucherShortcuts_LocationMappingClicked(object sender, EventArgs e)
        {
            frmMapLocation maplocation = new frmMapLocation();
            maplocation.ShowDialog();
        }

        private void ucAssetVoucherShortcuts_DonorClicked(object sender, EventArgs e)
        {
            frmDonorAdd donorAdd = new frmDonorAdd();
            donorAdd.ShowDialog();
        }

        private void ucAssetVoucherShortcuts_AccountMappingClicked(object sender, EventArgs e)
        {
            frmMapProjectLedger mapping = new frmMapProjectLedger(MapForm.Asset, (int)AddNewRow.NewRow, "");
            mapping.ShowDialog();
            ucAssetJournal1.LoadLedger();
        }

        private void ucAssetVoucherShortcuts_LedgerOptionClicked(object sender, EventArgs e)
        {
            frmLedgerOptions ledgerOption = new frmLedgerOptions();
            ledgerOption.ShowDialog();
        }

        private void ucAssetVoucherShortcuts_LedgerClicked(object sender, EventArgs e)
        {
            frmLedgerDetailAdd BankAdd = new frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.GN, ProjectId);
            BankAdd.ShowDialog();
            ucAssetJournal1.LoadLedger();
        }

        private void bbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtBillInvoiceNo.Focus();
        }

        private void bbiAssetGenerationList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AssetGenerationList();
        }

        private void bbiDeletePurchase_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteTransaction();
        }

        private void bbiCashBank_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteCashbanktransaction();
        }

        private void txtOtherCharges_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.Tab) { FocusCashTransactionGrid(); e.SuppressKeyPress = true; }
        }

        private void rtxtQuantity_Leave(object sender, EventArgs e)
        {
            isvalidQty = true;
            int rowid = gvInOutAdd.GetVisibleIndex(gvInOutAdd.FocusedRowHandle);
            if (SettingProperty.AssetListCollection.ContainsKey(rowid))
            {
                int ListCount = SettingProperty.AssetListCollection[rowid].AsEnumerable().Count(r => r.RowState != DataRowState.Deleted ?
                            UtilityMember.NumberSet.ToInteger(r["SELECT"].ToString()) == 1 : false);
                string DCount = ListCount.ToString();

                if (Quantity.ToString() != DCount)
                {
                    // this.ShowMessageBox("Quantity Mismatch with the Asset Item List.");
                    isvalidQty = false;
                }
            }
            gvInOutAdd.SetRowCellValue(gvInOutAdd.FocusedRowHandle, colQuantity, Quantity);
            // SendKeys.Send("{ENTER}");
        }

        #endregion

        #endregion

        #region Methods

        private void LoadReceiptType()
        {
            TransSource transSource = new TransSource();
            DataView dvtransSource = this.UtilityMember.EnumSet.GetEnumDataSource(transSource, Sorting.None);
            rglkpSource.DataSource = dvtransSource.ToTable();
            rglkpSource.DisplayMember = "Name";
            rglkpSource.ValueMember = "Id";
        }

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
            else if (rgTransactionType.SelectedIndex == 0)
            {

                if (string.IsNullOrEmpty(txtBillInvoiceNo.Text))
                {
                    //this.ShowMessageBox("Bill / Invoice No is Empty.");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetOutward.ASSET_OUTWARD_BILL_INVOCENO_EMPTY));
                    isSalesTrue = false;
                    this.SetBorderColor(txtBillInvoiceNo);
                    txtBillInvoiceNo.Focus();
                }

                else if (string.IsNullOrEmpty(txtSoldTo.Text))
                {
                    //this.ShowMessageBox("Outward to is Empty.");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetOutward.ASSET_OUTWARD_TO_EMPTY));
                    isSalesTrue = false;
                    this.SetBorderColor(txtSoldTo);
                    txtSoldTo.Focus();
                }
            }

            else if (rgTransactionType.SelectedIndex == 2)
            {
                if (string.IsNullOrEmpty(txtSoldTo.Text))
                {
                    //this.ShowMessageBox("Outward to is Empty.");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetOutward.ASSET_OUTWARD_TO_EMPTY));
                    isSalesTrue = false;
                    this.SetBorderColor(txtSoldTo);
                    txtSoldTo.Focus();
                }
            }
            else if (!IsValidTransGrid())
            {
                isSalesTrue = false;
            }
            else if (!IsValidCashBankGrid())
            {
                // this.ShowMessageBox("Ledger Details are empty.");
                isSalesTrue = false;
                ucAssetJournal1.FocusCashTransaction();
            }
            else if (!IsQuantitymatch())
            {
                //this.ShowMessageBox("Quantity Mismatch with the Asset Item List.");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetOutward.ASSET_QYT_MISMATCH_INFO));
                isSalesTrue = false;
            }
            else if (this.AppSetting.AllowMultiCurrency == 1 && !IsCurrencyEnabledVoucher)
            { //On 04/09/2024, If multi currency enabled, all the currency details must be filled
                MessageRender.ShowMessage("As Multi Currency option is enabled, All the Currecny details should be filled.");
                glkpCurrencyCountry.Select();
                glkpCurrencyCountry.Focus();
                isSalesTrue = false;
            }

            return isSalesTrue;
        }

        private void LoadVoucherNo()
        {
            using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
            {
                voucherTransaction.VoucherType = rgTransactionType.SelectedIndex == 0 || rgTransactionType.SelectedIndex == 1 ? VoucherSubTypes.RC.ToString() : VoucherSubTypes.JN.ToString();
                voucherTransaction.VoucherDefinitionId = rgTransactionType.SelectedIndex == 0 || rgTransactionType.SelectedIndex == 1 ? (Int32)DefaultVoucherTypes.Receipt : (Int32)DefaultVoucherTypes.Journal;
                voucherTransaction.ProjectId = ProjectId;
                voucherTransaction.VoucherDate = this.UtilityMember.DateSet.ToDate(dtSalesDate.Text, false);
                txtVoucherNo.Text = voucherTransaction.TempVoucherNo();
            }
        }

        private void LoadAccountingDate()
        {
            dtSalesDate.DateTime = RecentDate;
            dtSalesDate.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtSalesDate.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            dtSalesDate.DateTime = (!string.IsNullOrEmpty(RecentDate.ToString())) ? RecentDate : UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
        }

        private void RealColumnEditTransAmount()
        {
            colQuantity.RealColumnEdit.EditValueChanged += new EventHandler(RealColumnQuantity_EditValueChanged);
            this.gvInOutAdd.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvInOutAdd.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colQuantity)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvInOutAdd.ShowEditorByMouse();
                    }));
                }
            };
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

        private void LoadSoldToAutoComplete()
        {
            try
            {
                using (AssetInwardOutwardSystem inwardSystem = new AssetInwardOutwardSystem())
                {
                    resultArgs = inwardSystem.AutoFetchSoldTo();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataView dvSoldTo = resultArgs.DataSource.Table.AsDataView();
                        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            collection.Add(dr[inwardSystem.AppSchema.AssetInOut.SOLD_TOColumn.ColumnName].ToString());
                        }
                        txtSoldTo.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        txtSoldTo.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        txtSoldTo.MaskBox.AutoCompleteCustomSource = collection;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
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
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpLocation, resultArgs.DataSource.Table, LocationSystem.AppSchema.ASSETLocationDetails.LOCATIONColumn.ColumnName, LocationSystem.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName);
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

        private bool IsValidTransGrid()
        {
            bool isValid = true;
            int RowPosition = 0;
            // int ItemId = 0;
            try
            {
                DataTable dtSales = gcInOut.DataSource as DataTable;
                string AssetId = string.Empty;
                decimal Quantity = 0;
                decimal Amount = 0;
                int ItemId = 0;
                int LocationId = 0;
                DataView dv = new DataView(dtSales);
                dv.RowFilter = "(ITEM_ID>0 OR QUANTITY>0)";
                gvInOutAdd.FocusedColumn = colAssetName;
                if (dv.Count > 0)
                {
                    foreach (DataRowView drSales in dv)
                    {
                        LocationId = this.UtilityMember.NumberSet.ToInteger(drSales[this.appSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName].ToString());
                        ItemId = this.UtilityMember.NumberSet.ToInteger(drSales[this.appSchema.ASSETItem.ITEM_IDColumn.ColumnName].ToString());
                        Quantity = this.UtilityMember.NumberSet.ToInteger(drSales[this.appSchema.AssetInOut.QUANTITYColumn.ColumnName].ToString());
                        Amount = this.UtilityMember.NumberSet.ToDecimal(drSales[this.appSchema.AssetInOut.AMOUNTColumn.ColumnName].ToString());

                        //if (LocationId == 0)
                        //{
                        //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.PurchaseVoucher.PURCHASE_ASSETLOCATION_VALIDATION));
                        //    gvInOutAdd.FocusedColumn = colLocation;
                        //    isValid = false;
                        //}
                        if (ItemId == 0)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.SalesVoucher.SALES_ASSET_NAME_EMPTY));
                            gvInOutAdd.FocusedColumn = colAsset;
                            isValid = false;
                        }
                        else if (Quantity == 0)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.SalesVoucher.SALES_QUANTITY_EMPTY));
                            gvInOutAdd.FocusedColumn = colQuantity;
                            isValid = false;
                        }
                        else if (Amount == 0)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AMCVoucher.AMCVOUCHER_AMOUNT_EMPTY));
                            gvInOutAdd.FocusedColumn = colAmount;
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
                    gvInOutAdd.FocusedColumn = colAsset;
                }

                if (!isValid)
                {
                    gvInOutAdd.CloseEditor();
                    gvInOutAdd.FocusedRowHandle = gvInOutAdd.GetRowHandle(RowPosition);
                    gvInOutAdd.ShowEditor();
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

        private bool IsValidCashBankGrid()
        {
            bool isvalid = true;
            int RowPosition = 0;

            DataTable dtSales = ucAssetJournal1.DtCashBank;

            decimal Amount = 0;
            int LedgerId = 0;
            DataView dv = new DataView(dtSales);
            dv.RowFilter = "(LEDGER_ID > 0 OR AMOUNT > 0)";
            if (dv.Count > 0)
            {
                foreach (DataRowView drSales in dv)
                {
                    LedgerId = this.UtilityMember.NumberSet.ToInteger(drSales[this.appSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString());
                    Amount = this.UtilityMember.NumberSet.ToDecimal(drSales[this.appSchema.AssetInOut.AMOUNTColumn.ColumnName].ToString());

                    if (LedgerId == 0)
                    {
                        //this.ShowMessageBox("Ledger is empty.");
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetOutward.ASSET_LEDGER_EMPTY));
                        isvalid = false;
                    }
                    else if (Amount == 0)
                    {
                        //this.ShowMessageBox("Amount is empty.");
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetOutward.ASSET_AMOUNT_EMPTY));
                        isvalid = false;
                    }
                    if (!isvalid) break;
                    RowPosition = RowPosition + 1;
                }
            }
            else
            {
                isvalid = false;
                //this.ShowMessageBox("Cash / Bank Details are empty.");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetOutward.ASSET_CASH_BANK_DETAIL_EMTPY));
            }

            return isvalid;


            //if (dtSales == null)
            //{
            //    isvalid = false;
            //}
            //else if (dtSales != null && dtSales.Rows.Count >= 0)
            //{
            //    if (dtSales.Rows.Count == 1)
            //    {
            //        if (dtSales.Rows[0].IsNull("LEDGER_ID"))
            //        {
            //            isvalid = false;
            //        }
            //    }
            //    else if (dtSales.Rows.Count == 0)
            //    {
            //        isvalid = false;
            //    }
            //}
            //return isvalid;
        }

        private void ConstructSalesDetail()
        {
            try
            {
                DataTable dtSalesDetail = new DataTable();
                dtSalesDetail.Columns.Add("SOURCE", typeof(string));
                dtSalesDetail.Columns.Add("LOCATION_ID", typeof(Int32));
                dtSalesDetail.Columns.Add("IN_OUT_DETAIL_ID", typeof(Int32));
                dtSalesDetail.Columns.Add("ITEM_ID", typeof(Int32));
                dtSalesDetail.Columns.Add("QUANTITY", typeof(Int32));
                dtSalesDetail.Columns.Add("AVAILABLE_QUANTITY", typeof(Int32));
                dtSalesDetail.Columns.Add("AMOUNT", typeof(decimal));
                dtSalesDetail.Columns.Add("LEDGER_ID", typeof(UInt32));
                dtSalesDetail.Columns.Add("TEMP_AMOUNT", typeof(decimal));
                dtSalesDetail.Columns.Add("DIFFERENCE", typeof(decimal));
                dtSalesDetail.Columns.Add("TYPE", typeof(string));
                dtSalesDetail.Columns.Add("EXCHANGE_RATE", typeof(decimal));
                dtSalesDetail.Columns.Add("LIVE_EXCHANGE_RATE", typeof(decimal));
                gcInOut.DataSource = dtSalesDetail;
                gvInOutAdd.AddNewRow();

                gvInOutAdd.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
                //int sourceId = rgTransactionType.SelectedIndex == 0 ? (int)Source.To : rgTransactionType.SelectedIndex == 1 ? (int)Source.To : (int)Source.To;
                gvInOutAdd.MoveFirst();
                gvInOutAdd.SetRowCellValue(gvInOutAdd.FocusedRowHandle, colSource, (int)Source.To);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void SetTitle()
        {
            ucCaptionTitle.Caption = ProjectName;
            this.Text = InOutId == 0 ? this.GetMessage(MessageCatalog.Asset.SalesVoucher.SALES_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.SalesVoucher.SALES_EDIT_CAPTION);
            SetBackColorByAssetVoucherType();
        }

        private void ClearControls()
        {
            if (InOutId == 0)
            {
                ConstructSalesDetail();
                LoadCashBankLedgers();
                txtBillInvoiceNo.Text = txtSoldTo.Text = txtOtherCharges.Text = txtNarration.Text = string.Empty;
                LoadVoucherNo();
                ucAssetJournal1.ConstructCashTransEmptySournce();
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
            DataTable dtCostCentre = null;
            if (InOutId > 0)
            {
                using (AssetInwardOutwardSystem assetInwardOutwardSystem = new AssetInwardOutwardSystem())
                {
                    assetInwardOutwardSystem.InoutId = InOutId;
                    assetInwardOutwardSystem.AssigntoProperties();
                    dtSalesDate.DateTime = assetInwardOutwardSystem.InOutDate;
                    txtSoldTo.Text = assetInwardOutwardSystem.SoldTo;
                    txtBillInvoiceNo.Text = assetInwardOutwardSystem.BillInvoiceNo;
                    VoucherId = assetInwardOutwardSystem.VoucherId;
                    txtVoucherNo.Text = assetInwardOutwardSystem.VoucherNo;
                    txtNarration.Text = assetInwardOutwardSystem.Narration;

                    if (this.AppSetting.AllowMultiCurrency == 1)
                    {
                        glkpCurrencyCountry.EditValue = assetInwardOutwardSystem.AssetCurrencyCountryId;
                        txtCurrencyAmount.Text = this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(assetInwardOutwardSystem.AssetCurrencyAmount.ToString())).ToString();
                        txtExchangeRate.Text = assetInwardOutwardSystem.AssetExchangeRate.ToString();
                        lblAvgRate.Text = assetInwardOutwardSystem.AssetExchangeRate.ToString();
                        lblCalAmt.Text = assetInwardOutwardSystem.AssetCalcAmount.ToString();
                    }
                    resultArgs = assetInwardOutwardSystem.FetchAssetInOutDetailById();

                    ucAssetJournal1.Flag = assetInwardOutwardSystem.Flag == AssetInOut.SL.ToString() ? AssetInOut.SL : assetInwardOutwardSystem.Flag == AssetInOut.DS.ToString() ? AssetInOut.DS : AssetInOut.DN;
                    ucAssetJournal1.LoadLedger();
                    if (ucAssetJournal1.Flag == AssetInOut.DN || ucAssetJournal1.Flag == AssetInOut.DS)
                    {

                        colActualAmount.Visible = false;
                        colDifference.Visible = false;
                        colType.Visible = false;
                    }
                    else
                    {
                        colActualAmount.Visible = true;
                        colType.Visible = true;
                        colDifference.Visible = true;
                    }

                    rgTransactionType.SelectedIndex = assetInwardOutwardSystem.Flag == AssetInOut.SL.ToString() ? rgTransactionType.SelectedIndex = 0 : assetInwardOutwardSystem.Flag == AssetInOut.DS.ToString() ? rgTransactionType.SelectedIndex = 1 : rgTransactionType.SelectedIndex = 2;
                    if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        if (!resultArgs.DataSource.Table.Columns.Contains("AVAILABLE_QUANTITY"))
                            resultArgs.DataSource.Table.Columns.Add("AVAILABLE_QUANTITY", typeof(Int32));

                        if (!resultArgs.DataSource.Table.Columns.Contains("SOURCE"))
                            resultArgs.DataSource.Table.Columns.Add("SOURCE", typeof(string));

                        if (!resultArgs.DataSource.Table.Columns.Contains("TEMP_AMOUNT"))
                            resultArgs.DataSource.Table.Columns.Add("TEMP_AMOUNT", typeof(decimal));

                        gcInOut.DataSource = BindSource(resultArgs.DataSource.Table);
                        gcInOut.RefreshDataSource();
                        gvInOutAdd.SetFocusedRowCellValue(colAvailableQuantity, AvailableQuantity());

                        DataTable dtList = resultArgs.DataSource.Table;
                        int itmID = 0;
                        int projID = 0;
                        int Qunty = 0;
                        int InOutDetID = 0;
                        int RNo = 0;
                        foreach (DataRow dritem in dtList.Rows)
                        {
                            itmID = UtilityMember.NumberSet.ToInteger(dritem["ITEM_ID"].ToString());
                            Qunty = UtilityMember.NumberSet.ToInteger(dritem["QUANTITY"].ToString());
                            InOutDetID = UtilityMember.NumberSet.ToInteger(dritem["IN_OUT_DETAIL_ID"].ToString());

                            if (Qunty > 0)
                            {
                                frmAssetItemList frmlist = new frmAssetItemList(itmID, Qunty, RNo, InOutDetID,
                                    assetInwardOutwardSystem.Flag == AssetInOut.SL.ToString() ? AssetInOut.SL
                                    : assetInwardOutwardSystem.Flag == AssetInOut.DS.ToString() ? AssetInOut.DS : AssetInOut.DN, ProjectId, "", dtSalesDate.DateTime.ToShortDateString());
                                frmlist.AssignItemDetails();
                            }
                            RNo++;
                        }
                        //ucAssetJournal1.Flag = rgTransactionType.SelectedIndex == 0 ? AssetInOut.SL : rgTransactionType.SelectedIndex == 1 ? AssetInOut.DS : AssetInOut.DN;
                        ucAssetJournal1.AssignValues(VoucherId);
                    }

                    using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem(VoucherId))
                    {
                        resultArgs = voucherSystem.FetchTransDetails();
                        if (resultArgs.Success)
                        {
                            dtCostCentre = resultArgs.DataSource.Table;
                            dsCostCentre.Clear();
                            for (int i = 0; i < dtCostCentre.Rows.Count; i++)
                            {
                                int LedId = this.UtilityMember.NumberSet.ToInteger(dtCostCentre.Rows[i][voucherSystem.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                                voucherSystem.LedgerId = LedId;
                                voucherSystem.CostCenterTable = i + "LDR" + LedId;
                                resultArgs = voucherSystem.GetCostCentreDetails();
                                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                                {
                                    DataTable CostCentreInfo = resultArgs.DataSource.Table;
                                    CostCentreInfo.TableName = i + "LDR" + LedId;
                                    if (CostCentreInfo != null) { dsCostCentre.Tables.Add(CostCentreInfo); }
                                }
                            }
                        }
                    }
                }
            }
        }

        private DataTable BindSource(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                //rgTransactionType.SelectedIndex == 0 ? AssetInOut.SL : rgTransactionType.SelectedIndex == 1 ? AssetInOut.DS : AssetInOut.DN
                int sourceId = (rgTransactionType.SelectedIndex == 0 || rgTransactionType.SelectedIndex == 0) ? (int)Source.To : (int)Source.By;
                dr["SOURCE"] = 1;// sourceId; chinna on 23.03.2021 at 11 AM
            }
            return dt;
        }

        private void DeleteTransaction()
        {
            try
            {
                //if (!string.IsNullOrEmpty(gvInOutAdd.GetFocusedRowCellValue(colLocation).ToString()))
                //{
                if (gvInOutAdd.RowCount > 1)
                {
                    if (gvInOutAdd.FocusedRowHandle != GridControl.NewItemRowHandle)
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            ucAssetJournal1.ConstructCashTransEmptySournce();
                            //    AssetInwardOutwardSystem inoutwardSystem = new AssetInwardOutwardSystem();
                            //    int insurance = inoutwardSystem.CheckInsuranceByItemID(InOutId, InoutDetailId, ItemId);
                            //    if (insurance > 0)
                            //    {
                            //        MessageBox.Show("Do you want to delete this Entry?");
                            //    }
                            //    int soldItemcount = inoutwardSystem.CheckSoldAssetIdByItemID(InOutId, InoutDetailId, ItemId);

                            int rowNo = gvInOutAdd.FocusedRowHandle;
                            frmAssetItemList list = new frmAssetItemList(ItemId, Quantity, rowNo, InoutDetailId, rgTransactionType.SelectedIndex == 0 ? AssetInOut.SL : rgTransactionType.SelectedIndex == 1 ? AssetInOut.DS : AssetInOut.DN, ProjectId);
                            list.DeleteAssetList();

                            gvInOutAdd.DeleteRow(gvInOutAdd.FocusedRowHandle);
                            gvInOutAdd.UpdateCurrentRow();
                            gcInOut.RefreshDataSource();
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                            gvInOutAdd.SetRowCellValue(gvInOutAdd.FocusedRowHandle, colSource, (int)Source.To);
                            gcInOut.Focus();
                            gvInOutAdd.MoveFirst();
                            gvInOutAdd.FocusedColumn = colAsset;
                            gvInOutAdd.ShowEditor();
                        }
                    }
                }
                else if (gvInOutAdd.RowCount == 1)
                {
                    if (ItemId > 0 || Quantity > 0)
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            ucAssetJournal1.ConstructCashTransEmptySournce();
                            int rowNo = gvInOutAdd.FocusedRowHandle;
                            frmAssetItemList list = new frmAssetItemList(ItemId, Quantity, rowNo, InoutDetailId, rgTransactionType.SelectedIndex == 0 ? AssetInOut.SL : rgTransactionType.SelectedIndex == 1 ? AssetInOut.DS : AssetInOut.DN, ProjectId);
                            list.DeleteAssetList();
                            ConstructSalesDetail();
                            gcInOut.RefreshDataSource();
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                            gcInOut.Focus();
                            gvInOutAdd.MoveFirst();
                            gvInOutAdd.FocusedColumn = colAsset;
                            gvInOutAdd.ShowEditor();
                        }
                    }
                }
                //}
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Bank.BANK_DELETE));
                    gvInOutAdd.FocusedColumn = colLocation;
                }

            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        private void ProcessShortcutKeys(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode.Equals(Keys.F5))
            {
                ShowProjectSelectionWindow();
                LoadDefaults();
            }
            else if (e.KeyCode.Equals(Keys.F8))
            {
                rgTransactionType.SelectedIndex = 0;
            }
            else if (e.KeyCode.Equals(Keys.F9))
            {
                rgTransactionType.SelectedIndex = 1;
            }
            else if (e.KeyCode == Keys.F10)
            {
                SendKeys.Send("{F10}");
                rgTransactionType.SelectedIndex = 2;
            }
            else if (e.KeyCode.Equals(Keys.F12))
            {
                frmAssetSettings setting = new frmAssetSettings();
                setting.ShowDialog();
            }
            //else if (e.KeyCode == (Keys.Control | Keys.D))
            //{
            //    DeleteTransaction();
            //}
            else if (e.KeyCode.Equals(Keys.F3))
            {
                dtSalesDate.Focus();
            }
            else if (e.KeyCode.Equals(Keys.F4))
            {
                DateTime dtYearTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
                dtSalesDate.DateTime = (dtSalesDate.DateTime < dtYearTo) ? dtSalesDate.DateTime.AddDays(1) : dtYearTo;
            }
            else if (e.KeyData == (Keys.Alt | Keys.A))
            {
                frmAssetItemAdd itemadd = new frmAssetItemAdd();
                itemadd.ShowDialog();
                LoadAssetNameByLocation(ProjectId);
            }
            else if (e.KeyData == (Keys.Alt | Keys.B))
            {
                frmLedgerDetailAdd BankAdd = new frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.BK, ProjectId);
                BankAdd.ShowDialog();
            }
            else if (e.KeyData == (Keys.Alt | Keys.E))
            {
                frmCostCentreAdd costcentre = new frmCostCentreAdd((int)AddNewRow.NewRow, ProjectId);
                costcentre.ShowDialog();
            }
            //else if (e.KeyData == (Keys.Alt | Keys.T))
            //{
            //    frmMapLocation maplocation = new frmMapLocation();
            //    maplocation.ShowDialog();
            //}
            if (e.KeyData == (Keys.Alt | Keys.M))
            {
                frmMapProjectLedger mapping = new frmMapProjectLedger(MapForm.Asset, (int)AddNewRow.NewRow, ProjectName);
                mapping.ShowDialog();
                ucAssetJournal1.LoadLedger();
            }
            else if (e.KeyData == (Keys.Alt | Keys.R))
            {
                frmLedgerOptions ledgerOption = new frmLedgerOptions();
                ledgerOption.ShowDialog();
            }
            //else if (e.KeyData == (Keys.Alt | Keys.F))
            //{
            //    frmVendorInfoAdd Manufacturer = new frmVendorInfoAdd(0, VendorManufacture.Manufacture);
            //    Manufacturer.ShowDialog();
            //}
            //else if (e.KeyData == (Keys.Alt | Keys.U))
            //{
            //    frmCustodiansAdd custodianAdd = new frmCustodiansAdd();
            //    custodianAdd.ShowDialog();
            //}
            //else if (e.KeyData == (Keys.Alt | Keys.V))
            //{
            //    frmVendorInfoAdd vendorAdd = new frmVendorInfoAdd(0, VendorManufacture.Vendor);
            //    vendorAdd.ShowDialog();
            //}
            else if (e.KeyData == (Keys.Alt | Keys.G))
            {
                frmLedgerDetailAdd BankAdd = new frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.GN, ProjectId);
                BankAdd.ShowDialog();
                ucAssetJournal1.LoadLedger();
            }
            //else if (e.KeyData == (Keys.Alt | Keys.O))
            //{
            //    frmDonorAdd donorAdd = new frmDonorAdd();
            //    donorAdd.ShowDialog();
            //}
            //else if (e.KeyData == (Keys.Alt | Keys.L))
            //{
            //    frmLocationsAdd LocationAdd = new frmLocationsAdd();
            //    LocationAdd.ShowDialog();
            //}
            else if (e.KeyData == (Keys.Control | Keys.L))
            {
                AssetGenerationList();
            }
            if (e.KeyData == (Keys.Alt | Keys.D))
            {
                // DeleteCashbanktransaction();
            }
            if (e.KeyData == (Keys.Control | Keys.D))
            {
                DeleteTransaction();
            }
        }

        private void DeleteCashbanktransaction()
        {
            string Flag = rgTransactionType.SelectedIndex == 0 ? AssetInOut.SL.ToString() : rgTransactionType.SelectedIndex == 1 ? AssetInOut.DS.ToString() : AssetInOut.DN.ToString();
            if (Flag == AssetInOut.SL.ToString() || Flag == AssetInOut.DS.ToString())
                ucAssetJournal1.DeleteTransaction();
        }

        private void ShowCostCentre(double LedgerAmount, bool isCCClicked)
        {
            try
            {
                if (LedgerAmount > 0)
                {
                    int CostCentre = 0;
                    string LedgerName = string.Empty;
                    DataView dvCostCentre = null;
                    using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
                    {
                        voucherSystem.LedgerId = isCCClicked == true ? AccountLedgerId : AccLedgerId;
                        AccLedgerId = voucherSystem.LedgerId;
                        resultArgs = voucherSystem.FetchCostCentreLedger();
                        if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            LedgerName = resultArgs.DataSource.Table.Rows[0][voucherSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                            CostCentre = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][voucherSystem.AppSchema.Ledger.IS_COST_CENTERColumn.ColumnName].ToString());
                            if (CostCentre != 0 && !string.IsNullOrEmpty(LedgerName))
                            {
                                int RowIndex = this.UtilityMember.NumberSet.ToInteger(gvInOutAdd.GetDataSourceRowIndex(gvInOutAdd.FocusedRowHandle).ToString()) - 1;
                                RowIndex = RowIndex < 0 ? 0 : RowIndex;

                                if (dsCostCentre.Tables.Contains(RowIndex + "LDR" + AccLedgerId))
                                {
                                    dvCostCentre = dsCostCentre.Tables[RowIndex + "LDR" + AccLedgerId].DefaultView;
                                }

                                frmTransactionCostCenter frmCostCentre = new frmTransactionCostCenter(ProjectId, dvCostCentre, AccLedgerId, LedgerAmount, LedgerName);
                                frmCostCentre.ShowDialog();
                                if (frmCostCentre.DialogResult == DialogResult.OK)
                                {
                                    DataTable dtValues = frmCostCentre.dtRecord;
                                    if (dtValues != null)
                                    {
                                        dtValues.TableName = RowIndex + "LDR" + AccLedgerId;
                                        if (dsCostCentre.Tables.Contains(dtValues.TableName))
                                        {
                                            dsCostCentre.Tables.Remove(dtValues.TableName);
                                        }
                                        dsCostCentre.Tables.Add(dtValues);
                                    }
                                }
                            }
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
                    ClearAssetCommonProperties();
                }
            }
        }

        private bool CheckQuantityExceeds(int CurQty)
        {
            bool isExceed = false;
            using (AssetInwardOutwardSystem inoutSystem = new AssetInwardOutwardSystem())
            {
                inoutSystem.ProjectId = ProjectId;
                inoutSystem.ItemId = ItemId;
                inoutSystem.LocationId = LocationId;
                inoutSystem.InOutDate = dtSalesDate.DateTime;
                int availQty = AvailQty = inoutSystem.FetchAvailableQty();
                if (InoutDetailId > 0)
                {
                    PrvQty = inoutSystem.FetchCurrentQty(InoutDetailId);
                }
                isExceed = (availQty + PrvQty) >= CurQty ? false : true;
            }
            return isExceed;
        }

        private int AvailableQuantity()
        {
            int availQty = 0;
            using (AssetInwardOutwardSystem inoutSystem = new AssetInwardOutwardSystem())
            {
                inoutSystem.ProjectId = ProjectId;
                inoutSystem.ItemId = ItemId;
                inoutSystem.LocationId = LocationId;
                inoutSystem.InOutDate = dtSalesDate.DateTime;
                availQty = inoutSystem.FetchAvailableQty();

            }
            return availQty;
        }

        private void FetchVoucherMethod()
        {
            using (ProjectSystem projectSystem = new ProjectSystem())
            {
                string VoucherMethod = rgTransactionType.SelectedIndex == 0 || rgTransactionType.SelectedIndex == 1 ? "1" : "4";
                int voucherdefinitionid = this.Flag == AssetInOut.PU ? 1 : 4;

                resultArgs = projectSystem.FetchVoucherByProjectId(ProjectId, VoucherMethod, voucherdefinitionid);
                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    TransVoucherMethod = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][projectSystem.AppSchema.Voucher.VOUCHER_METHODColumn.ColumnName].ToString());
                    if (TransVoucherMethod == 1)
                    {
                        txtVoucherNo.Enabled = false;
                    }
                    else
                    {
                        txtVoucherNo.Enabled = true;
                        txtVoucherNo.Select();
                        txtVoucherNo.Focus();
                    }
                }
                else
                {
                    TransVoucherMethod = 2;
                }
            }
        }

        bool IsDuplicatedValue(GridView currentView, GridColumn currentColumn, object someValue)
        {
            for (int i = 0; i < currentView.DataRowCount; i++)
            {
                if (currentView.GetRowCellValue(currentView.GetRowHandle(i), currentColumn).ToString() == someValue.ToString())
                {
                    gvInOutAdd.FocusedRowHandle = i;
                    gvInOutAdd.FocusedColumn = colQuantity;
                    return true;
                }
            }
            return false;
        }

        private void LoadAssetNameByLocation(int ProjectId)
        {
            try
            {
                using (AssetItemSystem AssetItemSystem = new AssetItemSystem())
                {
                    resultArgs = AssetItemSystem.FetchAssetItemDetailByLocation(ProjectId);
                    if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs != null)
                    {
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpAssetName, resultArgs.DataSource.Table, AssetItemSystem.AppSchema.ASSETItem.ASSET_ITEMColumn.ColumnName, AssetItemSystem.AppSchema.ASSETItem.ITEM_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void SetBackColorByAssetVoucherType()
        {
            if (rgTransactionType.SelectedIndex == 0)
            {
                this.Text = InOutId == 0 ? this.GetMessage(MessageCatalog.Asset.SalesVoucher.SALES_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.SalesVoucher.SALES_EDIT_CAPTION);
                lcgSales.Text = this.GetMessage(MessageCatalog.Asset.SalesVoucher.SALES_DETAILS);
                lblDisplayType.Text = this.GetMessage(MessageCatalog.Asset.SalesVoucher.SALES);
                lblDisplayType.AppearanceItemCaption.BackColor = rgTransactionType.BackColor = gvInOutAdd.Appearance.Row.BackColor =
                    //       gvCashBank.Appearance.Row.BackColor = gvInOutAdd.Appearance.FocusedRow.BackColor =
                    //       gvInOutAdd.Appearance.FocusedRow.BackColor = gvCashBank.Appearance.FocusedRow.BackColor = Color.LightSteelBlue;
                gvInOutAdd.Appearance.FocusedCell.BackColor = Color.Wheat;
                gvInOutAdd.Columns[colAmount.FieldName].Caption = "Amount";
            }
            else if (rgTransactionType.SelectedIndex == 1)
            {
                this.Text = InOutId == 0 ? this.GetMessage(MessageCatalog.Asset.SalesVoucher.DISPOSAL_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.SalesVoucher.DISPOSAL_EDIT_CAPTION);
                lcgSales.Text = this.GetMessage(MessageCatalog.Asset.SalesVoucher.DISPOSAL_DETAILS);
                lblDisplayType.Text = this.GetMessage(MessageCatalog.Asset.SalesVoucher.DISPOSE);
                lblDisplayType.AppearanceItemCaption.BackColor = rgTransactionType.BackColor = gvInOutAdd.Appearance.Row.BackColor =
                    gvInOutAdd.Appearance.FocusedRow.BackColor = Color.Wheat;
                gvInOutAdd.Appearance.FocusedCell.BackColor = Color.LightSteelBlue;
                gvInOutAdd.Columns[colAmount.FieldName].Caption = "Amount";
            }
            else if (rgTransactionType.SelectedIndex == 2)
            {
                this.Text = InOutId == 0 ? this.GetMessage(MessageCatalog.Asset.SalesVoucher.DONATE_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.SalesVoucher.DONATE_EDIT_CAPTION);
                lcgSales.Text = this.GetMessage(MessageCatalog.Asset.SalesVoucher.DONATE_DETAILS);
                lblDisplayType.Text = this.GetMessage(MessageCatalog.Asset.SalesVoucher.DONATE);
                lblDisplayType.AppearanceItemCaption.BackColor = rgTransactionType.BackColor = gvInOutAdd.Appearance.Row.BackColor =
                    gvInOutAdd.Appearance.FocusedRow.BackColor = Color.LightYellow;
                gvInOutAdd.Appearance.FocusedCell.BackColor = Color.LightSteelBlue;
                gvInOutAdd.Columns[colAmount.FieldName].Caption = "Amount";
            }
        }

        private void FocusCashTransactionGrid()
        {
            gcInOut.Select();
            gvInOutAdd.MoveFirst();
            gvInOutAdd.FocusedColumn = colAsset;
            gvInOutAdd.ShowEditor();
        }

        private bool IsQuantitymatch()
        {
            isvalidQty = true;
            DataTable dtItem = (DataTable)gcInOut.DataSource;
            if (dtItem != null && dtItem.Rows.Count > 0)
            {
                int RId = 0;
                foreach (DataRow drItem in dtItem.Rows)
                {
                    if (drItem.RowState != DataRowState.Deleted)
                    {
                        if (SettingProperty.AssetListCollection.ContainsKey(RId))
                        {
                            int ListCount = SettingProperty.AssetListCollection[RId].AsEnumerable().Count(r => r.RowState != DataRowState.Deleted ?
                                UtilityMember.NumberSet.ToInteger(r["SELECT"].ToString()) == 1 : false);
                            string DCount = ListCount.ToString();
                            int id = UtilityMember.NumberSet.ToInteger(drItem["QUANTITY"].ToString());
                            if (id > 0)
                            {
                                if (id.ToString() != DCount)
                                {
                                    int ids = UtilityMember.NumberSet.ToInteger(drItem["QUANTITY"].ToString());
                                    gvInOutAdd.SetRowCellValue(RId, colQuantity, drItem["QUANTITY"].ToString());
                                    isvalidQty = false;
                                    break;
                                }
                            }
                        }
                        RId++;
                    }
                }
            }
            return isvalidQty;
        }

        public void ClearAssetCommonProperties()
        {
            SettingProperty.AssetListCollection.Clear();
            SettingProperty.AssetInsuranceCollection.Clear();
            SettingProperty.AssetMultiInsuranceCollection.Clear();
            SettingProperty.AssetDeletedInoutIds = string.Empty;
            SettingProperty.AssetDeletedItemDetailIds = string.Empty;
        }

        #endregion

        private void gvInOutAdd_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.Column.FieldName == "TEMP_AMOUNT")
            {
                e.Appearance.BackColor = Color.LightGray;
            }
            if (e.Column.FieldName == "TYPE")
            {
                string Text = View.GetRowCellDisplayText(e.RowHandle, View.Columns["TYPE"]);
                if (Text == "Gain")
                {
                    e.Appearance.ForeColor = Color.Green;
                }
                else if (Text == "Loss")
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else
                {
                    e.Appearance.BackColor = Color.Empty;
                }
            }
        }

        private void gcInOut_Click(object sender, EventArgs e)
        {

        }

        private void rbtnCostCentre_Click(object sender, EventArgs e)
        {
            ShowCostCentre(this.UtilityMember.NumberSet.ToDouble(LedgerAmount.ToString()), true);
        }

        private void gvInOutAdd_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (gvInOutAdd.FocusedColumn == colCosCentre) // Added By praveen to restrict the costcentre form to be shown
            {
                if (AccountLedgerId > 0)
                {
                    if (!CheckCostcentreEnabled(AccountLedgerId))
                        e.Cancel = true;
                }
            }
        }

        public bool CheckCostcentreEnabled(int LedgerId)
        {
            int CostCentre = 0;
            bool IsExists = false;
            using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
            {
                voucherSystem.LedgerId = LedgerId;
                resultArgs = voucherSystem.FetchCostCentreLedger();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    CostCentre = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][voucherSystem.AppSchema.Ledger.IS_COST_CENTERColumn.ColumnName].ToString());
                    IsExists = CostCentre > 0 ? true : false;
                }
            }
            return IsExists;
        }

        private void ucAssetVoucherShortcuts_AssetVoucherViewClicked(object sender, EventArgs e)
        {
            frmAssetVoucherView assetVoucherView = new frmAssetVoucherView(ProjectName, ProjectId, dtSalesDate.DateTime, Flag);
            assetVoucherView.VoucherEditHeld += new EventHandler(OnEditHeld);
            assetVoucherView.ShowDialog();
        }

        private void glkpCurrencyCountry_EditValueChanged(object sender, EventArgs e)
        {
            ShowCurrencyDetails();
            ucAssetJournal1.LoadLedger();
        }

        private void txtCurrencyAmount_EditValueChanged(object sender, EventArgs e)
        {
            if (this.UtilityMember.NumberSet.ToDouble(txtCurrencyAmount.Text) >= 0)
            {
                CalculateExchangeRate();
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_AMOUNT_LESS_THAN_ZERO));
                txtCurrencyAmount.Text = "0";
                CalculateExchangeRate();
            }
        }

        private void txtExchangeRate_EditValueChanged(object sender, EventArgs e)
        {
            if (this.UtilityMember.NumberSet.ToDouble(txtExchangeRate.Text) >= 0)
            {
                CalculateExchangeRate();
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_AMOUNT_LESS_THAN_ZERO));
                txtExchangeRate.Text = "1";
                CalculateExchangeRate();
            }
        }
    }
}