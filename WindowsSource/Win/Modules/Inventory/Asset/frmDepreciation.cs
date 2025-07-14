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
using Bosco.DAO.Schema;
using Bosco.Utility.ConfigSetting;
using ACPP.Modules.Inventory.Asset;
using Bosco.Model.UIModel;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using AcMEDSync.Model;
using System.Globalization;
using Bosco.Utility.CommonMemberSet;
using Bosco.Model.Transaction;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmDepreciation : frmFinanceBaseAdd
    {
        #region Declaration
        ResultArgs resultArgs = new ResultArgs();
        DateTime dtLastDate = new DateTime();
        SettingProperty setting = new SettingProperty();
        public event EventHandler UpdateHeld;
        bool statusapplied = false;

        public const string METHOD_ID = "METHOD_ID";
        public const string LIFE_YRS = "LIFE_YRS";
        public const string SALVAGE_VALUE = "SALVAGE_VALUE";
        public const string PREV_COST = "PREV_COST";
        public const string CUR_COST = "CUR_COST";
        public const string TOTAL_VALUE = "TOTAL_VALUE";
        public const string DEPRECIATION_PERCENTAGE = "DEPRECIATION_PERCENTAGE";
        public const string DEPRECIATION_VALUE = "DEPRECIATION_VALUE";
        public const string BALANCE_AMOUNT = "BALANCE_AMOUNT";
        public const string DATE_OF_APPLY_FROM = "DATE_OF_APPLY";
        public const string DATE_OF_APPLY_DOD = "DATE_OF_DOD";
        public const string DATE_OF_APPLY_TO = "DATE_OF_APPLY_TO";
        public const string NO_MONTHS = "NO_MONTHS";
        #endregion

        #region Properties
        public int ProjectId { get; set; }
        public int VoucherId { get; set; }
        public string ProjectName { get; set; }
        public int DepreciationId { get; set; }
        public string DepPeriodFrom { get; set; }
        public string DepPeriodTo { get; set; }
        #endregion

        #region Constructor
        public frmDepreciation()
        {
            InitializeComponent();
        }
        public frmDepreciation(int DepId)
            : this()
        {
            DepreciationId = DepId;
        }
        public frmDepreciation(int DeprID, int ProjID, string PrName)
            : this()
        {
            DepreciationId = DeprID;
            ProjectId = ProjID;
            ProjectName = PrName;
            ucProject.Caption = ProjectName;
            RealColumnEditPercentage();
            RealColumnEditMethod();
        }
        #endregion

        #region Events

        /// <summary>
        /// Load Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDepreciation_Load(object sender, EventArgs e)
        {
            LoadDepreciationLedger();
            LoadDepreciationMethods();
            LoadVoucherDate();
            SetDefaultsDate();
            if (DepreciationId == 0)
                LoadVoucherNo();
            LoadNarrationAutoComplete();
            AssignValues();
            SetDatePeriod();
            LoadVoucherDepreciationDetails();
            SetDeprMethodDetails();

            //this.Text = (DepreciationId == 0) ? "Depreciation (Add)" : "Depreciation (Edit)";
            this.Text = (DepreciationId == 0) ? this.GetMessage(MessageCatalog.Asset.Depreciation.DEPRECIATION_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.Depreciation.DEPRECIATION_EDIT_CAPTION);

            if (dtPeriodFrom.DateTime == dtPeriodTo.DateTime)
            {
                //this.ShowMessageBox("Depreciation Applied for the Current Transaction Period.");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Depreciation.DEP_APPLY_CURRENT_TRANS_PEROID));
                this.Close();
            }
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

        private void AssignValues()
        {
            if (DepreciationId > 0)
            {
                using (AssetDepreciation depreciationSystem = new AssetDepreciation())
                {
                    depreciationSystem.DepId = DepreciationId;
                    depreciationSystem.ProjectId = ProjectId;
                    resultArgs = depreciationSystem.FetchDeprVoucherProperties();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        // Master Details
                        dtDepreciationApplied.DateTime = depreciationSystem.DepApplied;
                        dtPeriodFrom.DateTime = depreciationSystem.DepPeriodFrom;
                        dtPeriodTo.DateTime = depreciationSystem.DepPeriodTo;
                        VoucherId = depreciationSystem.VoucherId;
                        txtNarration.Text = depreciationSystem.Narration;
                        // Finance Ledger Details

                        resultArgs = depreciationSystem.FetchFinanceVoucherDetails();
                        if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            dtDepreciationApplied.DateTime =
                                UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][depreciationSystem.AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString(), false);
                            txtVNo.Text = resultArgs.DataSource.Table.Rows[0][depreciationSystem.AppSchema.VoucherMaster.VOUCHER_NOColumn.ColumnName].ToString();
                            glkpDepLedger.EditValue = resultArgs.DataSource.Table.Rows[0][depreciationSystem.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString();
                        }

                        // Child Details
                        resultArgs = depreciationSystem.FetchApplyDepreciationDetails();
                        if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            gcDepreciation.DataSource = resultArgs.DataSource.Table;
                        }
                        else
                        {
                            gcDepreciation.DataSource = null;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// close the form while pressing  the Escape 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="KeyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.Escape))
            {
                this.Close();
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        /// <summary>
        /// Load  the Depreciation Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoad_Click(object sender, EventArgs e)
        {

        }

        private void dtDepreciationApplied_EditValueChanged(object sender, EventArgs e)
        {
            LoadVoucherNo();
        }

        #endregion

        #region Methods

        /// <summary>
        /// This is to edit the Depreciation method
        /// </summary>
        public void LoadDepreciationMethods()
        {
            try
            {
                using (AssetDepreciationSystem depreciationSystem = new AssetDepreciationSystem())
                {
                    resultArgs = depreciationSystem.FetchDepreciationMethods();
                    if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs != null)
                    {
                        DataTable dtRecords = resultArgs.DataSource.Table;
                        int MethodId = setting.ShowDepr == "0" ? (int)DepreciationMethods.WDV : (int)DepreciationMethods.SLV;
                        dtRecords.DefaultView.RowFilter = "METHOD_ID =" + MethodId;
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpMethods, dtRecords.DefaultView.ToTable(), depreciationSystem.AppSchema.ASSETDepreciationDetails.DEP_METHODColumn.ColumnName, depreciationSystem.AppSchema.ASSETDepreciationDetails.METHOD_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        /// <summary>
        /// load the Fence Period
        /// </summary>

        private void SetDatePeriod()
        {
            bgcAssetCostDuringPeriod.Caption = bgcDeprCurrentPeriod.Caption = dtPeriodFrom.Text.Trim() + " To " + dtPeriodTo.Text.Trim();
            bgcAccumulatedDepreValue.Caption = bgccolOpeningCostTotal.Caption = UtilityMember.DateSet.ToDate(dtPeriodFrom.Text, false).AddDays(-1).ToShortDateString();
            bgcTotalAssetValues.Caption = dtPeriodFrom.Text.Trim();
            bgcCurrentAssetValueAsOn.Caption = dtPeriodTo.Text.Trim();
        }

        /// <summary>
        /// This is to load the Depreciation Ledger
        /// </summary>
        private void LoadDepreciationLedger()
        {
            try
            {
                using (AssetDepreciation Depreciation = new AssetDepreciation())
                {
                    Depreciation.ProjectId = ProjectId;
                    resultArgs = Depreciation.FetchDepreciationMappedLedger();
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpDepLedger, resultArgs.DataSource.Table, Depreciation.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, Depreciation.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                        glkpDepLedger.EditValue = glkpDepLedger.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception Ex)
            {
                this.ShowMessageBox(Ex.Message + Environment.NewLine + Ex.Source);
            }
            finally { }
        }

        private void LoadVoucherNo()
        {
            if (DepreciationId == 0)
            {
                using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                {
                    voucherTransaction.VoucherType = VoucherSubTypes.JN.ToString();
                    voucherTransaction.ProjectId = ProjectId;
                    voucherTransaction.VoucherDate = this.UtilityMember.DateSet.ToDate(dtDepreciationApplied.Text, false);
                    txtVNo.Text = voucherTransaction.TempVoucherNo();
                }
            }
        }

        private void LoadVoucherDepreciationDetails()
        {
            try
            {
                using (AssetDepreciation Depreciation = new AssetDepreciation())
                {
                    Depreciation.DepId = DepreciationId;
                    Depreciation.ProjectId = ProjectId;
                    Depreciation.DepPeriodFrom = dtPeriodFrom.DateTime;
                    Depreciation.DepPeriodTo = dtPeriodTo.DateTime;
                    resultArgs = Depreciation.FetchApplyDepreciationDetails();
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        gcDepreciation.DataSource = resultArgs.DataSource.Table;
                        SetDeprMethodDetails();
                    }
                    else
                    {
                        gcDepreciation.DataSource = null;
                    }
                }
            }
            catch (Exception Ex)
            {
                this.ShowMessageBox(Ex.Message + Environment.NewLine + Ex.Source);
            }
            finally { }
        }

        #endregion

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.AppSetting.ShowOpApplyFrom))
            {
                gcDepreciation.DataSource = gcLedgerDetails.DataSource = null;
                LoadVoucherDepreciationDetails();
                SetDatePeriod();
                SetDeprMethodDetails();
            }
            else
            {
                this.ShowMessageBox("Opening Balance Depreciation date can be set in Asset Setting");
            }
        }

        private void SetDeprMethodDetails()
        {
            //gvDepreciation.UpdateCurrentRow();
            DataTable dtWDV = gcDepreciation.DataSource as DataTable;
            if (dtWDV != null && dtWDV.Rows.Count > 0)
            {
                int MethodID = 0;
                double LifeYrs = 0;
                double SalvageValue = 0;
                double PrvAssetValue = 0;
                double CurAssetValue = 0;
                double DepPercent = 0;
                double DepValue = 0;
                double NoofMonths = 0;

                int ItemStatus = 1;

                double Salvalue = 0;
                double LifeValue = 0;
                double CurTotalValue = 0;
                double CurDepValue = 0;

                int CurPeriodValue = 0;
                int CurPeriodBalance = 0;

                DateTime dtePeriodFrom = dtPeriodFrom.DateTime;
                DateTime dtDBPeriodFrom = dtPeriodFrom.DateTime;
                DateTime dtePeriodTo = dtPeriodTo.DateTime;

                string opBalDate = this.AppSetting.BookBeginFrom;
                foreach (DataRow dtRow in dtWDV.Rows)
                {
                    string OpFrom = dtRow["FLAG"].ToString();
                    string PrevDOP = dtRow["PREDOP"].ToString();
                    if (OpFrom == "OP" && string.IsNullOrEmpty(PrevDOP))
                    {
                        dtRow[DATE_OF_APPLY_DOD] = dtRow[DATE_OF_APPLY_FROM] = this.UtilityMember.DateSet.ToDate(this.AppSetting.ShowOpApplyFrom, false);

                        //if (string.IsNullOrEmpty(this.AppSetting.ShowOpApplyFrom))
                        //{
                        //    dtRow[DATE_OF_APPLY_DOD] = DateTime.Parse(opBalDate); //DateTime.Parse(opBalDate).AddDays(-1); // = dtRow[DATE_OF_APPLY_DOD] dtRow[DATE_OF_APPLY_FROM]
                        //}
                        //else
                        //{
                        //    dtRow[DATE_OF_APPLY_DOD] = this.UtilityMember.DateSet.ToDate(this.AppSetting.ShowOpApplyFrom, false); // = dtRow[DATE_OF_APPLY_DOD] dtRow[DATE_OF_APPLY_FROM]
                        //}
                    }
                }
                dtWDV.AcceptChanges();


                foreach (DataRow dr in dtWDV.Rows)
                {
                    MethodID = 0;
                    LifeYrs = SalvageValue =
                         PrvAssetValue = CurAssetValue = DepPercent = DepValue = NoofMonths = Salvalue = LifeValue = CurTotalValue = CurDepValue = 0;
                    ItemStatus = UtilityMember.NumberSet.ToInteger(dr["STATUS"].ToString());
                    // Depreciation Apply To
                    if (!dtWDV.Columns.Contains(DATE_OF_APPLY_TO))
                        dtWDV.Columns.Add(DATE_OF_APPLY_TO, typeof(DateTime));
                    dr[DATE_OF_APPLY_TO] = dtePeriodTo.ToShortDateString();

                    // Depreciation Apply To
                    //if (!dtWDV.Columns.Contains(DATE_OF_APPLY_FROM))
                    //    dtWDV.Columns.Add(DATE_OF_APPLY_FROM, typeof(DateTime));
                    //dr[DATE_OF_APPLY_FROM] = dtePeriodFrom.ToShortDateString();
                    dtDBPeriodFrom = UtilityMember.DateSet.ToDate(dr[DATE_OF_APPLY_FROM].ToString(), false);
                    // Depreciation No Of Months
                    if (!dtWDV.Columns.Contains(NO_MONTHS))
                        dtWDV.Columns.Add(NO_MONTHS, typeof(double));
                    var diffMonths = (dtePeriodTo.Month + dtePeriodTo.Year * 12) - (dtDBPeriodFrom.Month + dtDBPeriodFrom.Year * 12) + 1;
                    NoofMonths = UtilityMember.NumberSet.ToDouble(diffMonths.ToString());
                    dr[NO_MONTHS] = NoofMonths.ToString();
                    MethodID = UtilityMember.NumberSet.ToInteger(dr[METHOD_ID].ToString());

                    dr[METHOD_ID] = setting.ShowDepr == "0" ? (int)DepreciationMethods.WDV : (int)DepreciationMethods.SLV;


                    LifeYrs = UtilityMember.NumberSet.ToInteger(dr[LIFE_YRS].ToString());
                    SalvageValue = UtilityMember.NumberSet.ToDouble(dr[SALVAGE_VALUE].ToString());
                    SalvageValue = SalvageValue == 0 ? 1 : SalvageValue;
                    PrvAssetValue = UtilityMember.NumberSet.ToDouble(dr[PREV_COST].ToString());
                    CurAssetValue = UtilityMember.NumberSet.ToDouble(dr[CUR_COST].ToString());

                    DepPercent = UtilityMember.NumberSet.ToDouble(dr[DEPRECIATION_PERCENTAGE].ToString());

                    string value = dr[DEPRECIATION_PERCENTAGE].ToString();
                    CurTotalValue = UtilityMember.NumberSet.ToDouble(dr[TOTAL_VALUE].ToString());

                    if (MethodID == (int)DepreciationMethods.WDV)// && DepreciationId.Equals(0))  // && DepPercent.Equals(0)
                    {
                        MethodID = (int)DepreciationMethods.WDV;

                        DepValue = Math.Abs((((CurTotalValue * DepPercent) / 100)));
                        CurDepValue = UtilityMember.NumberSet.ToDouble(diffMonths.ToString()) / 12.00;

                        CurPeriodValue = UtilityMember.NumberSet.ToInteger(Math.Abs(Math.Round(CurDepValue * DepValue)).ToString());
                        CurPeriodBalance = UtilityMember.NumberSet.ToInteger(Math.Abs(Math.Round(CurTotalValue - CurPeriodValue)).ToString());
                        dr[DEPRECIATION_VALUE] = String.Format("{0:0.00}", CurPeriodValue);
                        dr[BALANCE_AMOUNT] = String.Format("{0:0.00}", CurPeriodBalance);
                        dr[METHOD_ID] = MethodID;

                        //if (DepPercent == 0)
                        //{
                        //    Salvalue = ((SalvageValue) / (PrvAssetValue + CurAssetValue));
                        //    LifeValue = (1 / LifeYrs);
                        //    DepPercent = ((1 - (Math.Pow(Salvalue, LifeValue))) * 100);
                        //    dr[DEPRECIATION_PERCENTAGE] = String.Format("{0:0.00}", Math.Abs(DepPercent));
                        //}
                        //DepValue = Math.Abs((((CurTotalValue * DepPercent) / 100)));
                        //CurDepValue = UtilityMember.NumberSet.ToDouble(diffMonths.ToString()) / 12.00;

                        //CurPeriodValue = UtilityMember.NumberSet.ToInteger(Math.Abs(Math.Round(CurDepValue * DepValue)).ToString());
                        //CurPeriodBalance = UtilityMember.NumberSet.ToInteger(Math.Abs(Math.Round(CurTotalValue - CurPeriodValue)).ToString());
                        //dr[DEPRECIATION_VALUE] = String.Format("{0:0.00}", CurPeriodValue);
                        //dr[BALANCE_AMOUNT] = String.Format("{0:0.00}", CurPeriodBalance);
                    }
                    else if (MethodID == (int)DepreciationMethods.SLV)
                    {
                        DepValue = Math.Abs((((CurTotalValue * DepPercent) / 100)));
                        CurDepValue = UtilityMember.NumberSet.ToDouble(diffMonths.ToString()) / 12.00;

                        CurPeriodValue = UtilityMember.NumberSet.ToInteger(Math.Abs(Math.Round(CurDepValue * DepValue)).ToString());
                        CurPeriodBalance = UtilityMember.NumberSet.ToInteger(Math.Abs(Math.Round(CurTotalValue - CurPeriodValue)).ToString());
                        dr[DEPRECIATION_VALUE] = String.Format("{0:0.00}", CurPeriodValue);
                        dr[BALANCE_AMOUNT] = String.Format("{0:0.00}", CurPeriodBalance);

                        //if (DepPercent == 0)
                        //{
                        //    //Salvalue = ((PrvAssetValue + CurAssetValue) - SalvageValue) / LifeYrs;
                        //    //DepValue = ((Salvalue) / 12) * NoofMonths;

                        //    //CurPeriodValue = UtilityMember.NumberSet.ToInteger(Math.Abs(Math.Round(DepValue)).ToString());
                        //    //CurPeriodBalance = UtilityMember.NumberSet.ToInteger(Math.Abs(Math.Round(CurTotalValue - CurPeriodValue)).ToString());
                        //    //dr[DEPRECIATION_VALUE] = String.Format("{0:0.00}", CurPeriodValue);
                        //    //dr[BALANCE_AMOUNT] = String.Format("{0:0.00}", CurPeriodBalance);

                        //    Salvalue = ((PrvAssetValue + CurAssetValue) - SalvageValue) / LifeYrs;
                        //    DepValue = Salvalue * NoofMonths / 12; // Formula for Without Percentage

                        //    CurPeriodValue = UtilityMember.NumberSet.ToInteger(Math.Abs(Math.Round(DepValue)).ToString());
                        //    CurPeriodBalance = UtilityMember.NumberSet.ToInteger(Math.Abs(Math.Round(CurTotalValue - CurPeriodValue)).ToString());
                        //    dr[DEPRECIATION_VALUE] = String.Format("{0:0.00}", CurPeriodValue);
                        //    dr[BALANCE_AMOUNT] = String.Format("{0:0.00}", CurPeriodBalance);

                        //}
                        //else
                        //{
                        //    // Chinna for Calculating % Values

                        //    //Salvalue = ((PrvAssetValue + CurAssetValue) - SalvageValue) * DepPercent / 100 * NoofMonths / 12; // Formula for With pecentage

                        //    //CurPeriodValue = UtilityMember.NumberSet.ToInteger(Math.Abs(Math.Round(Salvalue)).ToString());
                        //    //CurPeriodBalance = UtilityMember.NumberSet.ToInteger(Math.Abs(Math.Round(CurTotalValue - CurPeriodValue)).ToString());

                        //    Salvalue = ((PrvAssetValue + CurAssetValue)) * DepPercent / 100 * NoofMonths / 12; // Formula for With pecentage

                        //    CurPeriodValue = UtilityMember.NumberSet.ToInteger(Math.Abs(Math.Round(Salvalue)).ToString());
                        //    CurPeriodBalance = UtilityMember.NumberSet.ToInteger(Math.Abs(Math.Round(CurTotalValue - CurPeriodValue)).ToString());

                        //    dr[DEPRECIATION_VALUE] = String.Format("{0:0.00}", CurPeriodValue);
                        //    dr[BALANCE_AMOUNT] = String.Format("{0:0.00}", CurPeriodBalance);
                        //}
                    }


                }
                dtWDV.AcceptChanges();
                gcDepreciation.DataSource = dtWDV;
                SetFinanceDetails(dtWDV);
            }
        }

        private void SetFinanceDetails(DataTable dtWDV)
        {
            DataTable dtLedgerSource = ConstructLedgerSource();
            DataTable dtLedger = dtWDV;
            int CurLedID = 0;
            int PrevLedID = 0;
            string PLedgername = string.Empty;
            double LedBalance = 0.0;
            double SumBalance = 0.0;
            DataView dvTemp = dtLedger.AsDataView();
            dvTemp.Sort = "LEDGER_ID ASC";
            dtLedger = dvTemp.ToTable();
            if (dtLedger != null && dtLedger.Rows.Count > 0)
            {
                foreach (DataRow drLed in dtLedger.Rows)
                {
                    CurLedID = UtilityMember.NumberSet.ToInteger(drLed["LEDGER_ID"].ToString());
                    PLedgername = drLed["LEDGER_NAME"].ToString();
                    LedBalance = UtilityMember.NumberSet.ToDouble(dtLedger.Compute("SUM(DEPRECIATION_VALUE)", "LEDGER_ID=" + CurLedID.ToString()).ToString());
                    if (!PrevLedID.Equals(CurLedID))
                    {
                        SumBalance += LedBalance;
                        DataRow dr = dtLedgerSource.NewRow();
                        dr["LEDGER_ID"] = CurLedID;
                        dr["LEDGER_NAME"] = PLedgername;
                        dr["CREDIT"] = LedBalance;
                        dr["AMOUNT"] = LedBalance;
                        dr["DEBIT"] = 0;
                        dr["SOURCE"] = (int)TransSource.Cr;
                        dtLedgerSource.Rows.Add(dr);
                        PrevLedID = CurLedID;
                    }
                }
            }
            lblDeprValue.Text = SumBalance > 0 ? UtilityMember.NumberSet.ToNumber(Math.Abs(SumBalance)) : "0.00";
            gcLedgerDetails.DataSource = null;
            gcLedgerDetails.DataSource = dtLedgerSource;
        }

        private DataTable ConstructLedgerSource()
        {
            DataTable dtLedger = new DataTable("Ledger");
            dtLedger.Columns.Add("LEDGER_ID", typeof(int));
            dtLedger.Columns.Add("LEDGER_NAME", typeof(string));
            dtLedger.Columns.Add("DEBIT", typeof(double));
            dtLedger.Columns.Add("CREDIT", typeof(double));
            dtLedger.Columns.Add("AMOUNT", typeof(double));
            dtLedger.Columns.Add("SOURCE", typeof(string));
            dtLedger.Columns.Add("LEDGER_FLAG", typeof(string));
            dtLedger.Columns.Add("CHEQUE_NO", typeof(string));
            dtLedger.Columns.Add("MATERIALIZED_ON", typeof(DateTime));
            dtLedger.Columns.Add("LEDGER_BALANCE", typeof(string));
            dtLedger.Columns.Add("BUDGET_AMOUNT", typeof(string));
            dtLedger.Columns.Add("TEMP_AMOUNT", typeof(decimal));
            dtLedger.Columns.Add("GROUP_ID", typeof(int));
            dtLedger.Columns.Add("NARRATION", typeof(string));
            return dtLedger;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidSource())
                {
                    using (AssetDepreciation depreciationSystem = new AssetDepreciation())
                    {
                        depreciationSystem.ProjectId = ProjectId;
                        depreciationSystem.VoucherId = VoucherId;
                        depreciationSystem.DepId = DepreciationId;
                        depreciationSystem.DepApplied = dtDepreciationApplied.DateTime;
                        depreciationSystem.DepPeriodFrom = dtPeriodFrom.DateTime;
                        depreciationSystem.DepPeriodTo = dtPeriodTo.DateTime;
                        depreciationSystem.Narration = txtNarration.Text;
                        depreciationSystem.VoucherNo = txtVNo.Text;
                        // Depreciation Details
                        depreciationSystem.dtDepreciation = gcDepreciation.DataSource as DataTable;
                        // Depreciation Finance Account Ledger Details 
                        depreciationSystem.dtFinanceLedgerDetails = gcLedgerDetails.DataSource as DataTable;
                        // Depreciation Finance Depreciation Ledger Details 

                        depreciationSystem.dtFinanceLedgerDetails = ConstructDeprData(depreciationSystem.dtFinanceLedgerDetails);

                        depreciationSystem.dtDepreciation = depreciationSystem.dtDepreciation.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull)).CopyToDataTable();
                        depreciationSystem.dtFinanceLedgerDetails = depreciationSystem.dtFinanceLedgerDetails.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull)).CopyToDataTable();

                        //depreciationSystem.dtDepreciationLedgerDetails = depreciationSystem.dtDepreciationLedgerDetails.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull)).CopyToDataTable();


                        resultArgs = depreciationSystem.SaveDepreciation();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            //this.ShowMessageBox("Depreciation Applied Successfully.");
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Depreciation.DEPRECIATION_SUCCESS_INFO));
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                            this.Close();
                        }
                        else
                        {
                            this.ShowMessageBoxError(resultArgs.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBoxError(ex.Message);
            }
        }

        private DataTable ConstructDeprData(DataTable dtLedger)
        {
            DataTable dtDepre = ConstructLedgerSource();
            DataRow dr = dtDepre.NewRow();
            dr["LEDGER_ID"] = glkpDepLedger.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkpDepLedger.EditValue.ToString()) : 0;
            dr["DEBIT"] = UtilityMember.NumberSet.ToDouble(lblDeprValue.Text) > 0 ? UtilityMember.NumberSet.ToDouble(lblDeprValue.Text) : 0;
            dr["CREDIT"] = 0;
            dr["SOURCE"] = (int)TransSource.Dr;
            dtDepre.Rows.Add(dr);
            if (dtLedger != null && dtLedger.Rows.Count > 0)
                dtLedger.Merge(dtDepre);

            dtLedger.DefaultView.RowFilter = "DEBIT <> 0 OR CREDIT <> 0";
            dtLedger = dtLedger.DefaultView.ToTable();

            return dtLedger;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void rglkpMethods_EditValueChanged(object sender, EventArgs e)
        {
            // SetDeprMethodDetails();
        }

        private void rtxtdepreciationPercalculation_EditValueChanged(object sender, EventArgs e)
        {
            //SetDeprMethodDetails();
        }

        private void RealColumnEditPercentage()
        {
            bgcdepPercentage.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditPercentage_EditValueChanged);
            this.bgvDepreciation.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = bgvDepreciation.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == bgcdepPercentage)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        statusapplied = false;
                        bgvDepreciation.ShowEditorByMouse();
                        statusapplied = true;
                    }));
                }
            };
        }

        void RealColumnEditPercentage_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            bgvDepreciation.PostEditor();
            bgvDepreciation.UpdateCurrentRow();
            if (bgvDepreciation.ActiveEditor == null)
            {
                bgvDepreciation.ShowEditor();
            }

            SetDeprMethodDetails();

            //TextEdit txtTransAmount = edit as TextEdit;
            //int grpCounts = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.DecimalPlaces) + 1);
            ////   if (txtTransAmount.Text.Length > grpCounts && txtTransAmount.SelectionLength == txtTransAmount.Text.Length)
            //if (txtTransAmount.Text.Length > grpCounts)
            //    txtTransAmount.Select(txtTransAmount.Text.Length - grpCounts, 0);
        }

        private void RealColumnEditMethod()
        {
            bgcDepMethod.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditMethod_EditValueChanged);
            this.bgvDepreciation.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = bgvDepreciation.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == bgcDepMethod)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        statusapplied = false;
                        bgvDepreciation.ShowEditorByMouse();
                        statusapplied = true;
                    }));
                }
            };
        }

        void RealColumnEditMethod_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            bgvDepreciation.PostEditor();
            bgvDepreciation.UpdateCurrentRow();
            if (bgvDepreciation.ActiveEditor == null)
            {
                bgvDepreciation.ShowEditor();
            }

            // Chinna commanded on while editing the values while changing  ( 23.03.2021)
            // SetDeprMethodDetails();
        }

        private void LoadVoucherDate()
        {
            dtDepreciationApplied.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtDepreciationApplied.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            dtDepreciationApplied.EditValue = DBNull.Value;

            //chinna on 23.03.2021
            //dtDepreciationApplied.DateTime = (!string.IsNullOrEmpty(this.AppSetting.RecentVoucherDate)) ? UtilityMember.DateSet.ToDate(this.AppSetting.RecentVoucherDate, false)
            //    : UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
        }


        private void SetDefaultsDate()
        {
            rdeAppliedOn.MinValue = dtPeriodFrom.Properties.MinValue = dtPeriodFrom.DateTime =
                dtPeriodTo.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);

            rdeAppliedOn.MaxValue = dtPeriodFrom.Properties.MaxValue = dtPeriodTo.DateTime =
                dtPeriodTo.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);


            if (DepreciationId > 0)
            {
                resultArgs = FetchPreviousPeriod();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    if (resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        dtPeriodFrom.Properties.MinValue = dtPeriodTo.Properties.MinValue = this.UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["DEPRECIATION_PERIOD_TO"].ToString(), false).AddDays(1);
                        if (dtPeriodFrom.Properties.MinValue < UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false))
                        {
                            dtPeriodFrom.Properties.MinValue = dtPeriodTo.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
                        }
                    }
                }
                resultArgs = FetchNextPeriod();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    if (resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        dtPeriodTo.Properties.MaxValue = dtPeriodFrom.Properties.MaxValue = this.UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["DEPRECIATION_PERIOD_FROM"].ToString(), false).AddDays(-1);
                        if (dtPeriodFrom.Properties.MaxValue > UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false))
                        {
                            dtPeriodFrom.Properties.MaxValue = dtPeriodTo.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
                        }
                    }
                }

                dtPeriodFrom.DateTime = dtPeriodFrom.Properties.MinValue.AddDays(1);
                dtPeriodTo.DateTime = dtPeriodFrom.DateTime.AddMonths(6).AddDays(-1);
            }
            else
            {
                resultArgs = FetchMaxPeriod();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    if (resultArgs.DataSource.Table.Rows[0]["PERIOD_TO"].ToString() != DBNull.Value.ToString())
                    {
                        DateTime dtTemp = dtPeriodFrom.Properties.MinValue;
                        dtTemp = this.UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["PERIOD_TO"].ToString(), false).AddDays(1);
                        if (dtTemp < dtPeriodFrom.Properties.MaxValue)
                        {
                            dtPeriodFrom.DateTime = dtTemp;
                        }
                        else
                        {
                            dtPeriodFrom.DateTime = dtPeriodFrom.Properties.MaxValue;
                        }
                        if (dtTemp.AddMonths(6).AddDays(-1) > dtPeriodFrom.Properties.MaxValue)
                        {
                            dtPeriodTo.DateTime = dtPeriodFrom.Properties.MaxValue;
                        }
                        else
                        {
                            dtPeriodFrom.DateTime = this.UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["PERIOD_TO"].ToString(), false).AddDays(1);
                        }
                    }
                    else
                    {
                        string MonthName = !string.IsNullOrEmpty(this.AppSetting.Months) ? this.AppSetting.Months : string.Empty;
                        if (!string.IsNullOrEmpty(MonthName))
                        {
                            int imonthNumber = DateTime.ParseExact(MonthName, "MMMM", CultureInfo.CurrentCulture).Month;
                            int year = imonthNumber >= 4 && imonthNumber <= 12 ? this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Year :
                                                                 this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false).Year;
                            dtLastDate = new DateTime(year, imonthNumber, DateTime.DaysInMonth(year, imonthNumber));
                            dtPeriodTo.DateTime = this.UtilityMember.DateSet.ToDate(dtLastDate.ToString(), false);
                        }
                        else // If Month name is Empty 
                        {
                            dtPeriodFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
                            dtPeriodTo.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
                        }
                    }
                }
            }
        }

        private ResultArgs FetchPreviousPeriod()
        {
            try
            {
                using (AssetDepreciation assetdepreciation = new AssetDepreciation())
                {
                    assetdepreciation.DepId = DepreciationId;
                    assetdepreciation.DepPeriodFrom = UtilityMember.DateSet.ToDate(DepPeriodFrom, false);
                    resultArgs = assetdepreciation.FetchPreviousRenewal();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            return resultArgs;
        }

        private ResultArgs FetchNextPeriod()
        {
            try
            {
                using (AssetDepreciation assetdepreciation = new AssetDepreciation())
                {
                    assetdepreciation.DepId = DepreciationId;
                    assetdepreciation.DepPeriodFrom = UtilityMember.DateSet.ToDate(DepPeriodFrom, false);
                    resultArgs = assetdepreciation.FetchNextRenewal();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            return resultArgs;
        }

        private ResultArgs FetchMaxPeriod()
        {
            try
            {
                using (AssetDepreciation assetdepreciation = new AssetDepreciation())
                {
                    assetdepreciation.ProjectId = ProjectId;
                    resultArgs = assetdepreciation.FetchMaxRenewal();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            return resultArgs;
        }

        public bool IsValidSource()
        {
            bool isSucess = true;
            if (string.IsNullOrEmpty(dtDepreciationApplied.Text))
            {
                //this.ShowMessageBox("Voucher Date is Empty");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Depreciation.DEP_VOUCHER_DATE_EMPTY));
                this.SetBorderColorForDateTimeEdit(dtDepreciationApplied);
                dtDepreciationApplied.Focus();
                isSucess = false;
            }
            else if (string.IsNullOrEmpty(dtPeriodFrom.Text))
            {
                //this.ShowMessageBox("Period From is Empty");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Depreciation.DEP_VOUCHER_PEROID_FROM_EMPTY));
                this.SetBorderColorForDateTimeEdit(dtPeriodFrom);
                dtPeriodFrom.Focus();
                isSucess = false;
            }
            else if (string.IsNullOrEmpty(dtPeriodTo.Text))
            {
                //this.ShowMessageBox("Period To is Empty");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Depreciation.DEP_VOUCHER_PEROID_TO_EMPTY));
                this.SetBorderColorForDateTimeEdit(dtPeriodTo);
                dtPeriodTo.Focus();
                isSucess = false;
            }
            else if (dtPeriodFrom.DateTime > dtPeriodTo.DateTime)
            {
                //this.ShowMessageBox("Period From is greater than Period To");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Depreciation.DEP_VOUCHER_PEROID_FROM_TO_DATE));
                dtPeriodFrom.Focus();
                isSucess = false;
            }
            else if (CheckDepreciationExists())
            {
                //this.ShowMessageBox("Depreciation details are Empty");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Depreciation.DEP_DETAILS_EMPTY));
                glkpDepLedger.Focus();
                isSucess = false;
            }
            else if (CheckFinanceLedgerExists())
            {
                //this.ShowMessageBox("Finance Ledger details are Empty");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Depreciation.DEP_FINANCE_LEDGER_EMPTY));
                glkpDepLedger.Focus();
                isSucess = false;
            }
            else if (glkpDepLedger.EditValue == null)
            {
                //this.ShowMessageBox("Depreciation Ledger is Empty");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Depreciation.DEP_LEDGER_DETAILS_EMPTY));
                glkpDepLedger.Focus();
                isSucess = false;
            }
            else if (UtilityMember.NumberSet.ToInteger(glkpDepLedger.EditValue.ToString()) == 0)
            {
                //this.ShowMessageBox("Depreciation Ledger is Empty");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Depreciation.DEP_LEDGER_DETAILS_EMPTY));
                glkpDepLedger.Focus();
                isSucess = false;
            }
            else if (UtilityMember.NumberSet.ToDouble(lblDeprValue.Text) == 0)
            {
                //this.ShowMessageBox("Depreciation Amount is Empty");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Depreciation.DEP_AMOUNT_EMPTY));
                glkpDepLedger.Focus();
                isSucess = false;
            }
            return isSucess;
        }

        private bool CheckFinanceLedgerExists()
        {
            bool Invalid = false;
            DataTable dtTemp = gcLedgerDetails.DataSource as DataTable;
            if (dtTemp == null)
            {
                Invalid = true;
            }
            else if (dtTemp.Rows.Count == 0)
            {
                Invalid = true;
            }
            return Invalid;
        }

        private bool CheckDepreciationExists()
        {
            bool Invalid = false;
            DataTable dtTemp = gcDepreciation.DataSource as DataTable;
            if (dtTemp == null)
            {
                Invalid = true;
            }
            else if (dtTemp.Rows.Count == 0)
            {
                Invalid = true;
            }
            return Invalid;
        }

        private void gvDepreciation_ShowingEditor(object sender, CancelEventArgs e)
        {
            //try
            //{
            //    if (DepreciationId > 0)
            //    {
            //        if (gvDepreciation.GetRowCellValue(gvDepreciation.FocusedRowHandle, bgcStatus).ToString() != string.Empty)
            //        {
            //            string Status = (string)gvDepreciation.GetRowCellValue(gvDepreciation.FocusedRowHandle, bgcStatus).ToString();
            //            //Status 0 = Sold or Donoted or Diposed
            //            //Status 1 = Purchase
            //            if (Status == "0")
            //            {
            //                e.Cancel = true; //Disabling the editing of the cell 
            //                this.ShowMessageBox("This Item Id cannot be edited.");
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ShowMessageBox(ex.Message);
            //}
        }

        private void bgvDepreciation_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                if (bgvDepreciation.GetRowCellValue(bgvDepreciation.FocusedRowHandle, bgcStatus).ToString() != string.Empty)
                {
                    string Status = (string)bgvDepreciation.GetRowCellValue(bgvDepreciation.FocusedRowHandle, bgcStatus).ToString();
                    //Status 0 = Sold or Donoted or Diposed
                    //Status 1 = Purchase
                    if (Status == "0")
                    {
                        e.Cancel = true; //Disabling the editing of the cell 
                        //this.ShowMessageBox("Asset Item Id has been Sold / Disposed / Donated.");

                    }

                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        private void bgvDepreciation_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (bgvDepreciation.GetRowCellValue(e.RowHandle, bgcStatus) != null)
                {
                    if (bgvDepreciation.GetRowCellValue(e.RowHandle, bgcStatus).ToString() != string.Empty)
                    {
                        string Status = bgvDepreciation.GetRowCellValue(e.RowHandle, bgcStatus).ToString();
                        //Status 0 = sold or Donoted or Diposed
                        //Status 1 = Purchase
                        if (Status == "0")
                        {
                            e.Appearance.BackColor = Color.Khaki;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

    }
}
