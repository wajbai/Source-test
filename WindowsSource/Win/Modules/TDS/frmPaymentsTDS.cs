using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.Model.Transaction;
using Bosco.Model.TDS;
using AcMEDSync.Model;

namespace ACPP.Modules.TDS
{
    public partial class frmPaymentsTDS : frmFinanceBaseAdd
    {
        #region Variables
        ResultArgs resultArgs = null;
        #endregion

        #region Properties
        public DataTable dtPaymentTDS { get; set; }
        private int IsVoucherTransType { get; set; }
        private DataTable dtTransSummary { get; set; }
        public DataTable dtTDSPaymentInfo { get; set; }
        private int TDSPaymentId { get; set; }
        private int TDSPaymentProjectId { get; set; }
        private int TDSPaymentVoucherId { get; set; }
        private DateTime VoucherDate { get; set; }
        public int CashBankVoucherId { get; set; }
        private int TaxLedgerId { get; set; }
        public string ProjectName { get; set; }
        private int PrevInterestLedgerId { get; set; }
        private int PrevPenLedgerId { get; set; }

        public bool IsTDSOptionEnabled { get; set; }

        public double PrevInsAmount { get; set; }
        public double PrevPenAmount { get; set; }
        private DataTable dtDataSource { get; set; }

        public int InsLedgerId { get; set; }
        public int PenLedgerId { get; set; }
        double RowAmount = 0;
        double FocusedRowAmount = 0;
        private int VoucherId
        {
            get
            {
                return (gcPaymentsTDS.DataSource as DataTable) != null && (gcPaymentsTDS.DataSource as DataTable).Rows.Count > 0 ?
                this.UtilityMember.NumberSet.ToInteger((gcPaymentsTDS.DataSource as DataTable).Rows[0]["VOUCHER_ID"].ToString()) : 0;
            }
        }

        private int CashBankId
        {
            get { return glkpCansBankLedger.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpCansBankLedger.EditValue.ToString()) : 0; }
        }

        private string CashBankName
        {
            get { return glkpCansBankLedger.Text; }
        }

        private DataTable TDSPaymentDetail
        {
            get
            {
                DataTable dtTDSPayment = new DataTable();
                dtTDSPayment = gcPaymentsTDS.DataSource as DataTable;
                if (dtTDSPayment != null && dtTDSPayment.Rows.Count > 0)
                {
                    DataView dvTDSPayment = dtTDSPayment.DefaultView;
                    dvTDSPayment.RowFilter = "AMOUNT >0";
                    dtTDSPayment = dvTDSPayment.ToTable();
                    dvTDSPayment.RowFilter = "";
                }
                return dtTDSPayment;
            }
        }

        private DataTable dtCashBankTransaction { get; set; }

        private double CashBankAmount
        {
            get
            {
                return gcPaymentsTDS.DataSource != null && (gcPaymentsTDS.DataSource as DataTable).Rows.Count > 0 ?
                this.UtilityMember.NumberSet.ToDouble((gcPaymentsTDS.DataSource as DataTable).Compute("SUM(AMOUNT)", "").ToString()) : 0;
            }
        }
        #endregion

        #region Constructor
        public frmPaymentsTDS()
        {
            InitializeComponent();
        }
        public frmPaymentsTDS(int TDSPaymentId, int ProjectId, int DutyTaxLedgerId, DateTime dtVoucherDat, string partyLedgerName, double ledgerAmount = 0)
            : this()
        {
            this.TDSPaymentId = TDSPaymentId;
            this.TDSPaymentProjectId = ProjectId;
            this.VoucherDate = dtVoucherDat;
            this.TaxLedgerId = DutyTaxLedgerId;
            lblPartyLedgerName.Text = partyLedgerName;
            lblLedgerAmount.Text = this.UtilityMember.NumberSet.ToNumber(ledgerAmount) + " " + TransactionMode.DR.ToString();
        }
        #endregion

        #region TDS Payment Events
        private void frmPaymentsTDS_Load(object sender, EventArgs e)
        {
            SetDefaults();
            FetchLedgers();
            AssignTaxLedgers();
            LoadTaxInfo();
            AssignPaymentTDS();
        }

        private void gcPaymentsTDS_ProcessGridKey(object sender, KeyEventArgs e)
        {
            bool isTransSuccess = false;
            if (gvPaymentsTDS.FocusedColumn == colPaymentAmount)
            {
                if (gvPaymentsTDS.IsLastRow && gvPaymentsTDS.FocusedColumn == colPaymentAmount && (e.KeyCode.Equals(Keys.Tab) || e.KeyCode.Equals(Keys.Enter)))
                {
                    isTransSuccess = true;
                }
                if (isTransSuccess)
                {
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    glkpInterestLedger.Select();
                    glkpInterestLedger.Focus();
                }
                UpdateTransSummary();
            }
            else if (gvPaymentsTDS.IsFirstRow && gvPaymentsTDS.FocusedColumn == colPaymentAmount && e.Shift && e.KeyCode == Keys.Tab)
            {
                glkpCansBankLedger.Select();
                glkpCansBankLedger.Focus();
            }
        }

        private void glkpCansBankLedger_EditValueChanged(object sender, EventArgs e)
        {
            UpdateTransSummary();
            FetchCashBankBalance();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateTDSPayment())
                {
                    CashBankVoucherId = this.UtilityMember.NumberSet.ToInteger(glkpCansBankLedger.EditValue.ToString());
                    InsLedgerId = glkpInterestLedger.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpInterestLedger.EditValue.ToString()) : 0;
                    PenLedgerId = glkpPenalty.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpPenalty.EditValue.ToString()) : 0;
                    PrevInsAmount = !string.IsNullOrEmpty(txtInsAmount.Text) ? this.UtilityMember.NumberSet.ToDouble(txtInsAmount.Text) : 0;
                    PrevPenAmount = !string.IsNullOrEmpty(txtPenalty.Text) ? this.UtilityMember.NumberSet.ToDouble(txtPenalty.Text) : 0;
                    DataTable dtTDSPaymentInfo = TDSPaymentDetail;
                    if (dtTDSPaymentInfo != null && dtTDSPaymentInfo.Rows.Count > 0)
                    {
                        if (!dtTDSPaymentInfo.Columns.Contains("FLAG"))
                        {
                            dtTDSPaymentInfo.Columns.Add("FLAG", typeof(int));
                        }

                        int DeductionId = this.UtilityMember.NumberSet.ToInteger((dtTDSPaymentInfo.AsEnumerable().Reverse().Take(1).CopyToDataTable()).Rows[0]["DEDUCTION_DETAIL_ID"].ToString());

                        if (this.UtilityMember.NumberSet.ToDouble(txtInsAmount.Text) > 0 && glkpInterestLedger.EditValue != null && this.UtilityMember.NumberSet.ToInteger(glkpInterestLedger.EditValue.ToString()) > 0)
                        {
                            DataRow dr = dtTDSPaymentInfo.NewRow();
                            dr["LEDGER_ID"] = this.UtilityMember.NumberSet.ToInteger(glkpInterestLedger.EditValue.ToString());
                            dr["DEDUCTION_DETAIL_ID"] = DeductionId;
                            dr["AMOUNT"] = this.UtilityMember.NumberSet.ToDecimal(txtInsAmount.Text);
                            dr["PROJECT_ID"] = TDSPaymentProjectId;
                            dr["SOURCE"] = (int)Source.By;
                            dr["FLAG"] = (int)TDSLedgerType.Interest;
                            dtTDSPaymentInfo.Rows.Add(dr);
                        }
                        if (this.UtilityMember.NumberSet.ToDouble(txtPenalty.Text) > 0 && glkpPenalty.EditValue != null && this.UtilityMember.NumberSet.ToInteger(glkpPenalty.EditValue.ToString()) > 0)
                        {
                            DataRow dr = dtTDSPaymentInfo.NewRow();
                            dr["LEDGER_ID"] = this.UtilityMember.NumberSet.ToInteger(glkpPenalty.EditValue.ToString());
                            dr["DEDUCTION_DETAIL_ID"] = DeductionId;
                            dr["AMOUNT"] = this.UtilityMember.NumberSet.ToDecimal(txtPenalty.Text);
                            dr["PROJECT_ID"] = TDSPaymentProjectId;
                            dr["SOURCE"] = (int)Source.By;
                            dr["FLAG"] = (int)TDSLedgerType.Penalty;
                            dtTDSPaymentInfo.Rows.Add(dr);
                        }
                        foreach (DataRow dr in dtTDSPaymentInfo.Rows)
                        {
                            dr["SOURCE"] = (int)Source.By;
                        }
                        this.Transaction.TDSPayment = dtPaymentTDS = dtTDSPaymentInfo;

                        DataTable dtTDSBank = AssignCashBank();
                        if (dtTDSBank != null && dtTDSBank.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtTDSBank.Rows)
                            {
                                dr["SOURCE"] = (int)Source.To;
                            }
                            this.Transaction.TDSPaymentBank = dtTDSBank;
                        }

                        this.DialogResult = DialogResult.OK;
                    }
                    this.Close();
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

        private void glkpCansBankLedger_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpCansBankLedger);
            if (glkpCansBankLedger.EditValue != null)
            {
                gcPaymentsTDS.Focus();
                gvPaymentsTDS.FocusedColumn = colPaymentAmount;
                gvPaymentsTDS.MoveFirst();
            }
        }

        private void gvPaymentsTDS_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                RowAmount = gvPaymentsTDS.GetFocusedRowCellValue(colActualAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvPaymentsTDS.GetFocusedRowCellValue(colActualAmount).ToString()) : 0;
                FocusedRowAmount = gvPaymentsTDS.GetFocusedRowCellValue(colPaymentAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvPaymentsTDS.GetFocusedRowCellValue(colPaymentAmount).ToString()) : 0;
                if (FocusedRowAmount > RowAmount)
                {
                    // e.Valid = false;
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void gvPaymentsTDS_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void uctdsPayments_GetPendingDetails(object sender, EventArgs e)
        {
            ShowPendingPayments();
        }

        private void ucTDSSummary_PreviewKeyDownEvent(object sender, EventArgs e)
        {
            if (ucTDSSummary.UcTransGrid.IsLastRow && ucTDSSummary.UcTransGrid.FocusedColumn == ucTDSSummary.UcTransColumn)
            {
                btnSave.Select();
                btnSave.Focus();
            }
        }

        private void glkpCansBankLedger_KeyDown(object sender, KeyEventArgs e)
        {
            ProcessShortcutKeys(e);
        }

        private void frmPaymentsTDS_KeyDown(object sender, KeyEventArgs e)
        {
            ProcessShortcutKeys(e);
        }

        private void meNarration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.Tab) { FocusTDSTransactionGrid(); e.SuppressKeyPress = true; }
            else if (e.KeyCode == Keys.Tab) { btnSave.Select(); btnSave.Focus(); e.SuppressKeyPress = true; }
        }

        private void txtInsAmount_Leave(object sender, EventArgs e)
        {
            if (this.UtilityMember.NumberSet.ToDouble(txtInsAmount.Text) > 0)
            {
                UpdateTransSummary();
            }
            else
            {
                if (PrevInsAmount > 0 || PrevInterestLedgerId > 0)
                {
                    if (glkpInterestLedger.EditValue != null && this.UtilityMember.NumberSet.ToInteger(glkpInterestLedger.EditValue.ToString()) > 0)
                    {
                        UpdateTransSummary();
                    }
                }
            }
        }

        private void txtPenalty_Leave(object sender, EventArgs e)
        {
            if (this.UtilityMember.NumberSet.ToDouble(txtPenalty.Text) > 0)
            {
                UpdateTransSummary();
            }
            else
            {
                if (PrevPenAmount > 0 || PrevPenLedgerId > 0)
                {
                    UpdateTransSummary();
                }
            }
        }

        private void glkpPenalty_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Close)
            {
                glkpPenalty.EditValue = 0;
                UpdateTransSummary();
            }
        }

        private void glkpInterestLedger_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Close)
            {
                glkpInterestLedger.EditValue = 0;
                UpdateTransSummary();
            }
        }

        private void glkpInterestLedger_EditValueChanged(object sender, EventArgs e)
        {
            if (this.UtilityMember.NumberSet.ToInteger(txtPenalty.Text) > 0)
            {
                UpdateTransSummary();
            }
        }

        private void glkpPenalty_EditValueChanged(object sender, EventArgs e)
        {
            if (this.UtilityMember.NumberSet.ToInteger(txtPenalty.Text) > 0)
            {
                UpdateTransSummary();
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Loading Project based Cash bank Ledgers
        /// </summary>
        private void LoadCashBankLedger()
        {
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                ledgerSystem.ProjectId = TDSPaymentProjectId;
                resultArgs = ledgerSystem.FetchCashBankLedger();
                if (resultArgs != null && resultArgs.Success)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpCansBankLedger, resultArgs.DataSource.Table, ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                    if (CashBankVoucherId > 0)
                    {
                        glkpCansBankLedger.EditValue = CashBankVoucherId;
                    }
                }
            }
        }

        /// <summary>
        /// Set Default Values
        /// </summary>
        private void SetDefaults()
        {
            LoadCashBankLedger();
            dtTransSummary = ucTDSSummary.ConstructTable();
            ConstructCashBank();
        }

        private void AssignPaymentTDS()
        {
            if (TDSPaymentId > 0 && TDSPaymentProjectId > 0)
            {
                if (dtTDSPaymentInfo == null)
                {
                    glkpCansBankLedger.EditValue = CashBankVoucherId;
                    using (TDSPaymentSystem TDSPaySystem = new TDSPaymentSystem())
                    {
                        TDSPaySystem.TDSPaymentId = TDSPaymentId;
                        TDSPaySystem.ProjectId = TDSPaymentProjectId;
                        DataTable dtTDSPayment = TDSPaySystem.FetchTDSPaymentById(IsTDSOptionEnabled);
                        gcPaymentsTDS.DataSource = dtTDSPayment;
                        gcPaymentsTDS.RefreshDataSource();
                        FetchTDSInterestPenaltyDetails();
                        UpdateTransSummary();
                    }
                }
                else
                {
                    DataView dvTDSPayment = dtTDSPaymentInfo.DefaultView;
                    dvTDSPayment.RowFilter = "VOUCHER_ID>0";
                    gcPaymentsTDS.DataSource = dvTDSPayment.ToTable();
                    glkpInterestLedger.EditValue = InsLedgerId > 0 ? InsLedgerId : 0;
                    glkpPenalty.EditValue = PenLedgerId > 0 ? PenLedgerId : 0;
                    txtInsAmount.Text = this.UtilityMember.NumberSet.ToNumber(PrevInsAmount);
                    txtPenalty.Text = this.UtilityMember.NumberSet.ToNumber(PrevPenAmount);
                    UpdateTransSummary();
                    dvTDSPayment.RowFilter = "";
                }
                dtDataSource = gcPaymentsTDS.DataSource as DataTable;
            }
        }

        private void FetchTDSInterestPenaltyDetails()
        {
            try
            {
                using (TDSPaymentSystem tdsPayment = new TDSPaymentSystem())
                {
                    tdsPayment.TDSPaymentId = TDSPaymentId;
                    DataTable dtTDSIns = tdsPayment.FetchTDSInterestPenaltyDetails();
                    if (dtTDSIns != null && dtTDSIns.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtTDSIns.Rows)
                        {
                            int Flag = dr[tdsPayment.AppSchema.TDSPaymentDetail.FLAGColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dr[tdsPayment.AppSchema.TDSPaymentDetail.FLAGColumn.ColumnName].ToString()) : 0;
                            if (Flag.Equals((int)TDSLedgerType.Interest))
                            {
                                glkpInterestLedger.EditValue = this.UtilityMember.NumberSet.ToInteger(dr[tdsPayment.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString());
                                txtInsAmount.Text = this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(dr[tdsPayment.AppSchema.TDSPaymentDetail.PAID_AMOUNTColumn.ColumnName].ToString()));
                            }
                            else if (Flag.Equals((int)TDSLedgerType.Penalty))
                            {
                                glkpPenalty.EditValue = this.UtilityMember.NumberSet.ToInteger(dr[tdsPayment.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString());
                                txtPenalty.Text = this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(dr[tdsPayment.AppSchema.TDSPaymentDetail.PAID_AMOUNTColumn.ColumnName].ToString()));
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
        #endregion

        #region Transaction Methods
        private void AssignTDSPending(ACPP.Modules.Master.frmPendingTransaction frmPenTrans)
        {
            if (TDSPaymentId > 0)
            {
                DataTable dtTDSPayment = frmPenTrans.dtTDSPending;
                if (gcPaymentsTDS.DataSource != null && dtTDSPayment != null && dtTDSPayment.Rows.Count > 0)
                {
                    DataView dvTDSPayment = dtDataSource.DefaultView;
                    dvTDSPayment.RowFilter = "TDS_PAYMENT_ID>0";

                    //dtDataSource.Merge(dtTDSPayment);
                    DataTable dtNewDataSource = dvTDSPayment.ToTable();

                    dtNewDataSource.Merge(dtTDSPayment);
                    gcPaymentsTDS.DataSource = dtNewDataSource;
                    dvTDSPayment.RowFilter = "";
                }
            }
            else
            {
                gcPaymentsTDS.DataSource = frmPenTrans.dtTDSPending;
            }
            lblTDSAmount.Text = gcPaymentsTDS.DataSource != null && (gcPaymentsTDS.DataSource as DataTable).Rows.Count > 0 ? this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble((gcPaymentsTDS.DataSource as DataTable).Compute("SUM(AMOUNT)", "").ToString())) : "0";
            UpdateTransSummary();
            gvPaymentsTDS.Focus();
            gvPaymentsTDS.PostEditor();
            gvPaymentsTDS.FocusedColumn = colPaymentAmount;
        }

        /// <summary>
        /// Update transaction summary for each and every transaction
        /// </summary>
        private void UpdateTransSummary()
        {
            double CashBankAmount = 0;
            try
            {
                DataTable dtUpdateTransSummary = gcPaymentsTDS.DataSource as DataTable;
                using (Bosco.Model.TDS.TDSPaymentSystem TDSPayment = new Bosco.Model.TDS.TDSPaymentSystem())
                {
                    if (dtUpdateTransSummary != null && dtUpdateTransSummary.Rows.Count > 0)
                    {
                        ucTDSSummary.UpdateTDSSummary.Clear();
                        FilterCashBankLedger();
                        foreach (DataRow dr in dtUpdateTransSummary.Rows)
                        {
                            int LedgerId = dr[TDSPayment.AppSchema.Ledger.LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dr[TDSPayment.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()) : 0;
                            string LedgerName = dr[TDSPayment.AppSchema.TDSPayment.TDS_LEDGERColumn.ColumnName] != DBNull.Value ? dr[TDSPayment.AppSchema.TDSPayment.TDS_LEDGERColumn.ColumnName].ToString() : string.Empty;
                            double Tax_Amount = dr[TDSPayment.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToDouble(dr[TDSPayment.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString()) : 0;
                            if (LedgerId > 0 && !string.IsNullOrEmpty(LedgerName) && Tax_Amount > 0)
                            {
                                dtTransSummary.Rows.Add(LedgerId, LedgerName, LedgerId, Tax_Amount, 0, Tax_Amount, TransactionMode.DR.ToString());
                            }
                        }

                        if (this.UtilityMember.NumberSet.ToDouble(txtInsAmount.Text) > 0 && this.UtilityMember.NumberSet.ToInteger(glkpInterestLedger.EditValue.ToString()) > 0)
                        {
                            PrevInsAmount = this.UtilityMember.NumberSet.ToDouble(txtInsAmount.Text);
                            dtTransSummary.Rows.Add(this.UtilityMember.NumberSet.ToInteger(glkpInterestLedger.EditValue.ToString()), glkpInterestLedger.Text, this.UtilityMember.NumberSet.ToInteger(glkpInterestLedger.EditValue.ToString()), this.UtilityMember.NumberSet.ToDecimal(txtInsAmount.Text), 0, this.UtilityMember.NumberSet.ToDecimal(txtInsAmount.Text), TransactionMode.DR.ToString());
                        }
                        if (this.UtilityMember.NumberSet.ToDouble(txtPenalty.Text) > 0 && this.UtilityMember.NumberSet.ToInteger(glkpPenalty.EditValue.ToString()) > 0)
                        {
                            PrevPenAmount = this.UtilityMember.NumberSet.ToDouble(txtPenalty.Text);
                            dtTransSummary.Rows.Add(this.UtilityMember.NumberSet.ToInteger(glkpPenalty.EditValue.ToString()), glkpPenalty.Text, this.UtilityMember.NumberSet.ToInteger(glkpPenalty.EditValue.ToString()), this.UtilityMember.NumberSet.ToDecimal(txtPenalty.Text), 0, this.UtilityMember.NumberSet.ToDecimal(txtPenalty.Text), TransactionMode.DR.ToString());
                        }

                        if (dtTransSummary != null && dtTransSummary.Rows.Count > 0)
                        {
                            CashBankAmount = this.UtilityMember.NumberSet.ToDouble(dtTransSummary.Compute("SUM(DEBIT)", "").ToString());
                        }
                        double TDSTaxAmount = this.UtilityMember.NumberSet.ToDouble(dtUpdateTransSummary.Compute("SUM(AMOUNT)", "").ToString());
                        dtTransSummary.Rows.Add(CashBankId, CashBankName, CashBankId, 0, TDSTaxAmount > CashBankAmount ? TDSTaxAmount : CashBankAmount, TDSTaxAmount > CashBankAmount ? TDSTaxAmount : CashBankAmount, TransactionMode.CR.ToString());

                        DataView dvTransSummary = dtTransSummary.DefaultView;
                        dvTransSummary.Sort = "TRANS_MODE DESC";
                        dtTransSummary = dvTransSummary.ToTable();
                        ucTDSSummary.UpdateTDSSummary = dtTransSummary;
                    }
                    SetNetAmount();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        /// <summary>
        /// Filter Cash Bank Ledger based on Trans Mode
        /// </summary>
        private void FilterCashBankLedger()
        {
            try
            {
                DataView dvTransSummary = ucTDSSummary.UpdateTDSSummary.DefaultView;
                dvTransSummary.RowFilter = "TRANS_MODE='CR'";
                dtTransSummary = dvTransSummary.ToTable();
                dvTransSummary.RowFilter = "";
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void SetNetAmount()
        {
            if (ucTDSSummary.UpdateTDSSummary != null && ucTDSSummary.UpdateTDSSummary.Rows.Count > 0)
            {
                DataView dvFilter = ucTDSSummary.UpdateTDSSummary.DefaultView;
                dvFilter.RowFilter = "TRANS_MODE='" + TransactionMode.DR.ToString() + "'";
                if (dvFilter != null && dvFilter.Count > 0)
                {
                    lblTDSAmount.Text = this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(dvFilter.ToTable().Compute("SUM(DEBIT)", "").ToString()));
                }
                dvFilter.RowFilter = "";
            }
        }

        private DataTable FetchTDSPendingTrans()
        {
            DataTable dtTDSPendingTrans = new DataTable();
            using (Bosco.Model.TDS.TDSPaymentSystem TDSPayment = new Bosco.Model.TDS.TDSPaymentSystem())
            {
                TDSPayment.ProjectId = TDSPaymentProjectId;
                TDSPayment.BookingDate = VoucherDate;
                TDSPayment.PartyLedgerId = this.TaxLedgerId;
                dtTDSPendingTrans = TDSPayment.FetchTDSPendingPayment();
            }
            return dtTDSPendingTrans;
        }

        private void FetchCashBankBalance()
        {
            using (BalanceSystem balancesystem = new BalanceSystem())
            {
                BalanceProperty balanceProperty = balancesystem.GetBalance(TDSPaymentProjectId, CashBankId, "", BalanceSystem.BalanceType.CurrentBalance);
                lblCashBankAmount.Text = this.UtilityMember.NumberSet.ToCurrency(balanceProperty.Amount) + " " + balanceProperty.TransMode;
            }
        }

        private void ProcessShortcutKeys(KeyEventArgs e)
        {
            try
            {
                if (e.KeyData.Equals(Keys.Alt | Keys.P))
                {
                    ShowPendingPayments();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void ShowPendingPayments()
        {
            try
            {
                DataTable dtPendingTrans = FetchTDSPendingTrans();
                DataView dvTDSPayment = dtPendingTrans.DefaultView;
                dvTDSPayment.RowFilter = "AMOUNT >0";
                dtPendingTrans = dvTDSPayment.ToTable();
                dvTDSPayment.RowFilter = "";
                if (dtPendingTrans != null && dtPendingTrans.Rows.Count > 0)
                {
                    ACPP.Modules.Master.frmPendingTransaction frmPenTrans = new Master.frmPendingTransaction(TDSTransTypes.TDSPayments);
                    frmPenTrans.dtTDSSelectedLedgers = dtPendingTrans;
                    frmPenTrans.ShowDialog();
                    if (gcPaymentsTDS.DataSource != null && frmPenTrans.dtTDSPending != null)
                    {
                        AssignTDSPending(frmPenTrans);
                    }
                    else if (frmPenTrans.dtTDSPending != null)
                    {
                        AssignTDSPending(frmPenTrans);
                    }
                    else if (frmPenTrans.dtTDSPending == null && TDSPaymentId == (int)YesNo.No)
                    {
                        AssignTDSPending(frmPenTrans);
                        ucTDSSummary.UpdateTDSSummary.Clear();
                    }
                }
                else
                {
                    ShowMessageBox(GetMessage(MessageCatalog.TDS.TDSTrasaction.TDS_PENDING_TRANS_CONFIRMATION) + " - " + ProjectName);
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void FocusTDSTransactionGrid()
        {
            gcPaymentsTDS.Select();
            gvPaymentsTDS.MoveLast();
            gvPaymentsTDS.FocusedColumn = colPaymentAmount;
            gvPaymentsTDS.ShowEditor();
        }

        private void AssignTaxLedgers()
        {
            if (InsLedgerId > 0 || PenLedgerId > 0)
            {
                glkpInterestLedger.EditValue = InsLedgerId;
                glkpPenalty.EditValue = PenLedgerId;
            }
        }

        private void LoadTaxInfo()
        {
            if (TDSPaymentId == 0)
            {
                if (this.TaxLedgerId > 0)
                {
                    if (this.AppSetting.TDSPayment != null && this.AppSetting.TDSPayment.Rows.Count > 0)
                    {
                        int Ledger = this.UtilityMember.NumberSet.ToInteger(this.AppSetting.TDSPayment.Rows[0]["LEDGER_ID"].ToString());
                        if (Ledger == this.TaxLedgerId)
                        {
                            DataView dvTDSPayment = this.AppSetting.TDSPayment.DefaultView;
                            dvTDSPayment.RowFilter = "FLAG IS NULL";

                            if (dvTDSPayment != null && dvTDSPayment.Count > 0)
                            {
                                gcPaymentsTDS.DataSource = dvTDSPayment.ToTable();
                                gcPaymentsTDS.RefreshDataSource();
                                gvPaymentsTDS.Focus();
                                gvPaymentsTDS.PostEditor();
                                gvPaymentsTDS.FocusedColumn = colPaymentAmount;
                            }

                            DataView dvTDSInterest = this.AppSetting.TDSPayment.DefaultView;
                            dvTDSInterest.RowFilter = "FLAG IN(1,2)";


                            if (dvTDSInterest != null && dvTDSInterest.Count > 0)
                            {
                                foreach (DataRow dr in dvTDSInterest.ToTable().Rows)
                                {
                                    int Flag = this.UtilityMember.NumberSet.ToInteger(dr["FLAG"].ToString());
                                    if (Flag.Equals((int)TDSLedgerType.Interest))
                                    {
                                        glkpInterestLedger.EditValue = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                                        txtInsAmount.Text = this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(dr["AMOUNT"].ToString()));
                                    }
                                    else
                                    {
                                        glkpPenalty.EditValue = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                                        txtPenalty.Text = this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(dr["AMOUNT"].ToString()));
                                    }
                                }
                            }
                            UpdateTransSummary();
                            dvTDSPayment.RowFilter = "";
                            dvTDSInterest.RowFilter = "";
                        }
                    }
                }
            }
        }
        #endregion

        #region Save TDS Payments

        private bool ValidateTDSPayment()
        {
            bool isTrue = true;
            if (CashBankId.Equals((int)YesNo.No))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDLedger.FD_CASH_BANK_EMPTY));
                glkpCansBankLedger.Focus();
                isTrue = false;
            }
            else if (gcPaymentsTDS.DataSource == null)
            {
                this.ShowMessageBox("Record is not available");
                isTrue = false;
            }
            return isTrue;
        }

        private void ConstructCashBank()
        {
            DataTable dtCashTrans = new DataTable();
            dtCashTrans.Columns.Add("SOURCE", typeof(string));
            dtCashTrans.Columns.Add("LEDGER_FLAG", typeof(string));
            dtCashTrans.Columns.Add("LEDGER_ID", typeof(Int32));
            dtCashTrans.Columns.Add("AMOUNT", typeof(decimal));
            dtCashTrans.Columns.Add("CHEQUE_NO", typeof(string));
            dtCashTrans.Columns.Add("MATERIALIZED_ON", typeof(DateTime));
            dtCashTrans.Columns.Add("LEDGER_BALANCE", typeof(string));
            dtCashTrans.Columns.Add("BUDGET_AMOUNT", typeof(string));
            dtCashTrans.Columns.Add("TEMP_AMOUNT", typeof(decimal));
            dtCashTrans.Columns.Add("CHEQUE_REF_DATE", typeof(DateTime));
            dtCashTrans.Columns.Add("CHEQUE_REF_BANKNAME", typeof(string));
            dtCashTrans.Columns.Add("CHEQUE_REF_BRANCH", typeof(string));
            dtCashBankTransaction = dtCashTrans;
        }

        private DataTable AssignCashBank()
        {
            double Amount = ucTDSSummary.UpdateTDSSummary != null && ucTDSSummary.UpdateTDSSummary.Rows.Count > 0 ? this.UtilityMember.NumberSet.ToDouble(ucTDSSummary.UpdateTDSSummary.Compute("SUM(DEBIT)", "").ToString()) : 0;
            dtCashBankTransaction.Rows.Add(string.Empty, string.Empty, CashBankId, Amount, string.Empty, null, string.Empty, string.Empty, 0);
            return dtCashBankTransaction;
        }

        private void ClearControl()
        {
            gcPaymentsTDS.DataSource = null;
            ucTDSSummary.UpdateTDSSummary.Clear();
            lblTDSAmount.Text = this.UtilityMember.NumberSet.ToNumber(0);
            SetDefaults();
        }

        private void FetchLedgers()
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    ledgerSystem.ProjectId = this.TDSPaymentProjectId;
                    resultArgs = ledgerSystem.FetchLedgerByGroup();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataView dvLedgers = resultArgs.DataSource.Table.DefaultView;
                        dvLedgers.RowFilter = "NATURE_ID IN(" + (int)Natures.Expenses + "," + (int)Natures.Income + ")";
                        if (dvLedgers != null && dvLedgers.Count > 0)
                        {
                            this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpInterestLedger, dvLedgers.ToTable(), "LEDGER_NAME", "LEDGER_ID");
                            this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpPenalty, dvLedgers.ToTable(), "LEDGER_NAME", "LEDGER_ID");
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
        #endregion

    }
}