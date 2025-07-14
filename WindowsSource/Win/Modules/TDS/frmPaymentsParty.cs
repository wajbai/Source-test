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
using Bosco.Model.TDS;
using Bosco.Model.Transaction;
using AcMEDSync.Model;

namespace ACPP.Modules.TDS
{
    public partial class frmPaymentsParty : frmFinanceBaseAdd
    {
        #region Variable
        ResultArgs resultArgs = null;
        public event EventHandler UpdateHeld;
        #endregion

        #region Properties
        private DataTable dtTDSPartyLedgers { get; set; }
        private DataTable dtTDSPendingSelected { get; set; }
        private DataTable dtPaymentTDS { get; set; }
        public int TDSPaymentId { get; set; }
        public int ProjectId { get; set; }
        public int PartyProjectId { get; set; }
        private int IsVoucherTransType { get; set; }

        public string VoucherNo { get; set; }
        public string PartyDate { get; set; }
        public string Narration { get; set; }
        public int VoucherId { get; set; }
        public int PartyPaymentId { get; set; }
        public int CashBankLedgerId { get; set; }
        public int PartyId { get; set; }
        public int PartyLedgerId { get; set; }
        public DateTime dteVoucherDate { get; set; }
        public string PartyLedgerName { get; set; }
        public string ProjectName { get; set; }
        public int TDSPartyBankId { get; set; }
        public double Amount { get; set; }

        private double RowAmount { get; set; }
        private double FocusedRowAmount { get; set; }

        private DataTable dtCashBankTransaction { get; set; }

        private double CashBankAmount
        {
            get
            {
                double NetAmount = 0;
                if (gcPaymentsParty.DataSource != null)
                {
                    double DebitAmount = this.UtilityMember.NumberSet.ToDouble((gcPaymentsParty.DataSource as DataTable).Compute("SUM(AMOUNT)", "TRANS_MODE='DR'").ToString());
                    double CreditAmount = this.UtilityMember.NumberSet.ToDouble((gcPaymentsParty.DataSource as DataTable).Compute("SUM(AMOUNT)", "TRANS_MODE='CR'").ToString());
                    NetAmount = DebitAmount - CreditAmount;
                }
                return NetAmount;
            }
        }

        private int CashBankId
        {
            get { return glkpCashOrBank.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpCashOrBank.EditValue.ToString()) : 0; }
        }

        private DataTable TDSPaymentDetail
        {
            get
            {
                DataTable dtTDSPayment = new DataTable();
                dtTDSPayment = gcPaymentsParty.DataSource as DataTable;
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

        #endregion

        #region Constructor
        public frmPaymentsParty()
        {
            InitializeComponent();
        }
        public frmPaymentsParty(int TDSPaymentId, int ProjectId)
            : this()
        {
            this.TDSPaymentId = TDSPaymentId;
            this.PartyProjectId = ProjectId;
        }
        public frmPaymentsParty(int TDSPartyPaymentId, int ProjectId, int PartyLedgerId, DateTime dtePartyDate, string partyLedgerName, double ledgerAmount = 0)
            : this()
        {
            this.PartyPaymentId = TDSPartyPaymentId;
            this.ProjectId = ProjectId;
            this.PartyLedgerId = PartyLedgerId;
            this.dteVoucherDate = dtePartyDate;
            this.PartyLedgerName = partyLedgerName;
            this.Amount = ledgerAmount;
        }
        #endregion

        #region Events
        private void frmPaymentsParty_Load(object sender, EventArgs e)
        {
            SetDefults();
            AssignPaymentTDS();
        }

        private void SetDefults()
        {
            LoadCashBankLedger();
            ConstructCashBank();
            lblPartyLedgerName.Text = this.PartyLedgerName;
        }

        private void AssignPaymentTDS()
        {
            if (PartyPaymentId > 0 && ProjectId > 0 && PartyLedgerId > 0)
            {
                glkpCashOrBank.EditValue = CashBankLedgerId;
                using (PartyPaymentSystem PartyPayment = new PartyPaymentSystem())
                {
                    PartyPayment.PartyId = PartyPaymentId;
                    PartyPayment.PartyPaymentId = PartyPaymentId;
                    PartyPayment.ProjectId = ProjectId;
                    PartyPayment.VoucherId = VoucherId;
                    PartyPayment.VocherDate = this.UtilityMember.DateSet.ToDate(PartyDate, false);
                    resultArgs = PartyPayment.FetchPendingTransactionForPartyment();
                    if (resultArgs != null && resultArgs.DataSource.Table != null)
                    {
                        gcPaymentsParty.DataSource = resultArgs.DataSource.Table;
                        CalculateSummary();
                    }
                }
            }
        }

        private void GetPendingPartyPayment()
        {
            using (DeducteeTaxSystem deduteeTaxSystem = new DeducteeTaxSystem())
            {
                deduteeTaxSystem.PartyPaymentId = PartyLedgerId;
                deduteeTaxSystem.ApplicableFrom = dteVoucherDate;
                deduteeTaxSystem.ProjectId = ProjectId;
                resultArgs = deduteeTaxSystem.FetchPendingTransactionForPartyment();
                if (resultArgs != null && resultArgs.Success)
                {
                    DataTable dtPendingTransaction = resultArgs.DataSource.Table;//Compute("SUM(AMOUNT)", "TRANS_MODE='CR'").ToString()
                    double Amount = UtilityMember.NumberSet.ToDouble(dtPendingTransaction.Compute("SUM(AMOUNT)", "TRANS_MODE='CR'").ToString());
                    if (dtPendingTransaction != null && dtPendingTransaction.Rows.Count > 0 && Amount > 0)
                    {
                        using (frmPendingTDSTransaction pendingTransaction = new frmPendingTDSTransaction(TDSPendingType.PartyPaymentPending))
                        {
                            pendingTransaction.dtPendingTDSTransaction = dtPendingTransaction;
                            pendingTransaction.ShowDialog();
                            dtTDSPendingSelected = pendingTransaction.dePendingTDSSelected;
                            if (dtTDSPendingSelected != null && dtTDSPendingSelected.Rows.Count > 0)
                            {
                                gcPaymentsParty.DataSource = ChangeTransMode();
                                CalculateSummary();
                                gvPaymentsParty.FocusedColumn = ColPaymentAmount;
                                gvPaymentsParty.Focus();
                            }
                        }
                    }
                    else
                    {
                        ShowMessageBox(GetMessage(MessageCatalog.TDS.TDSTrasaction.TDS_PENDING_TRANS_CONFIRMATION) + " - " + ProjectName);
                        ucPartyPaymentSummary.UpdateTDSSummary = null;
                    }
                }
            }
        }

        private void CalculateSummary()
        {
            DataTable dtSummary = gcPaymentsParty.DataSource as DataTable;
            if (dtSummary != null && dtSummary.Rows.Count > 0)
            {
                double DebitAmount = UtilityMember.NumberSet.ToDouble(dtSummary.Compute("SUM(AMOUNT)", "TRANS_MODE='DR'").ToString());
                double CreditAmount = UtilityMember.NumberSet.ToDouble(dtSummary.Compute("SUM(AMOUNT)", "TRANS_MODE='CR'").ToString());
                //decimal Amount = (DebitAmount - PayableAmount);

                double PaymentAmount = DebitAmount - CreditAmount;
                lblNetAmount.Text = UtilityMember.NumberSet.ToCurrency(PaymentAmount);
                DataTable dtSummaryRecords = dtSummary.Clone();
                if (!dtSummaryRecords.Columns.Contains("NATURE_PAYMENTS"))
                    dtSummaryRecords.Columns.Add("NATURE_PAYMENTS", typeof(string));
                DataRow dr = dtSummaryRecords.NewRow();
                dr["NATURE_PAYMENTS"] = PartyLedgerName;
                //dr["AMOUNT"] = DebitAmount;
                dr["AMOUNT"] = DebitAmount;
                dr["TRANS_MODE"] = "DR";
                dtSummaryRecords.Rows.Add(dr);
                DataRow dr1 = dtSummaryRecords.NewRow();
                dr1["NATURE_PAYMENTS"] = glkpCashOrBank.EditValue != null ? glkpCashOrBank.Text : string.Empty;
                //dr1["AMOUNT"] = CreditAmount;
                dr1["AMOUNT"] = DebitAmount;
                dr1["TRANS_MODE"] = "CR";
                dtSummaryRecords.Rows.Add(dr1);
                ucPartyPaymentSummary.gcTDSSummary.DataSource = dtSummaryRecords;
            }
        }

        private void gcPaymentsParty_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode.Equals(Keys.Tab) || e.KeyCode.Equals(Keys.Enter)) || (e.KeyCode.Equals(Keys.Down)) || (e.KeyCode.Equals(Keys.Up)) && gvPaymentsParty.FocusedColumn == ColPaymentAmount)
                {
                    if (gvPaymentsParty.IsLastRow)
                    {
                        // ChangeTransactionMode();
                        e.SuppressKeyPress = true;
                        e.Handled = true;
                        btnSave.Select();
                        btnSave.Focus();
                    }
                    else
                    {
                        //  ChangeTransactionMode();
                        gvPaymentsParty.MoveNext();
                        gvPaymentsParty.FocusedColumn = ColPaymentAmount;
                    }
                    CalculateSummary();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void ChangeTransactionMode()
        {
            double ActualAmount = gvPaymentsParty.GetFocusedRowCellValue(colActualAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvPaymentsParty.GetFocusedRowCellValue(colActualAmount).ToString()) : 0;
            double PaymentAmount = gvPaymentsParty.GetFocusedRowCellValue(ColPaymentAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvPaymentsParty.GetFocusedRowCellValue(ColPaymentAmount).ToString()) : 0;
            if (ActualAmount < PaymentAmount)
            {
                gvPaymentsParty.SetFocusedRowCellValue(ColVoucherMode, TransactionMode.CR.ToString());
            }
            else
            {
                gvPaymentsParty.SetFocusedRowCellValue(ColVoucherMode, TransactionMode.DR.ToString());
            }
        }

        private void gcPaymentsParty_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData.Equals(Keys.Tab))
            {
                //if (gvPaymentsParty.IsLastRow && gvPaymentsParty.FocusedColumn.Equals(ColPaymentAmount))
                if (gvPaymentsParty.IsLastRow)
                {
                    btnSave.Focus();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateTDSPayment())
                {
                    DataTable dtTDSPaymentInfo = TDSPaymentDetail;
                    if (dtTDSPaymentInfo != null && dtTDSPaymentInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtTDSPaymentInfo.Rows)
                        {
                            dr["SOURCE"] = (int)Source.By;
                        }
                        this.Transaction.TDSPartyPayment = dtPaymentTDS = dtTDSPaymentInfo;

                        DataTable dtTDSBank = AssignCashBank();
                        if (dtTDSBank != null && dtTDSBank.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtTDSBank.Rows)
                            {
                                dr["SOURCE"] = (int)Source.To;
                            }
                            this.Transaction.TDSPartyPaymentBank = dtTDSBank;
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

        private void gvPaymentsParty_ShowingEditor(object sender, CancelEventArgs e)
        {
            //if (gvPaymentsParty.GetRowCellValue(gvPaymentsParty.FocusedRowHandle, ColPaymentAmount) != null &&
            //    gvPaymentsParty.GetRowCellValue(gvPaymentsParty.FocusedRowHandle, ColPaymentAmount.FieldName).ToString() != string.Empty)
            //{
            //    string VoucherType = gvPaymentsParty.GetRowCellValue(gvPaymentsParty.FocusedRowHandle, ColVoucherMode.FieldName).ToString();
            //    //if (VoucherType.Equals("DR"))
            //    //{
            //    //    gvPaymentsParty.FocusedRowHandle += 1;
            //    //    e.Cancel = true;
            //    //}
            //}
        }

        private void UcPendingDetails_GetPendingDetails(object sender, EventArgs e)
        {
            GetPendingPartyPayment();
        }

        private void glkpCashOrBank_EditValueChanged(object sender, EventArgs e)
        {
            //  CalculateSummary();
            FetchCashBankBalance();
        }

        private void glkpCashOrBank_KeyDown(object sender, KeyEventArgs e)
        {
            ProcessShortcutKeys(e);
        }

        private void glkpCashOrBank_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpCashOrBank);
            if (glkpCashOrBank.EditValue != null)
            {
                gvPaymentsParty.Focus();
                gvPaymentsParty.FocusedColumn = ColPaymentAmount;
                gvPaymentsParty.MoveFirst();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gcPaymentsParty_KeyDown(object sender, KeyEventArgs e)
        {
            ProcessShortcutKeys(e);
        }

        private void frmPaymentsParty_KeyDown(object sender, KeyEventArgs e)
        {
            ProcessShortcutKeys(e);
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
                ledgerSystem.ProjectId = ProjectId;
                resultArgs = ledgerSystem.FetchCashBankLedger();
                if (resultArgs != null && resultArgs.Success)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpCashOrBank, resultArgs.DataSource.Table, ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                }
            }
        }

        private void AssignDefaultPartyLedgers()
        {
            gcPaymentsParty.DataSource = dtTDSPartyLedgers;
        }

        private void FetchCashBankBalance()
        {
            using (BalanceSystem balancesystem = new BalanceSystem())
            {
                BalanceProperty balanceProperty = balancesystem.GetBalance(ProjectId, CashBankId, "", BalanceSystem.BalanceType.CurrentBalance);
                lblCashBankAmount.Text = this.UtilityMember.NumberSet.ToCurrency(balanceProperty.Amount) + " " + balanceProperty.TransMode;
            }
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
            dtCashBankTransaction = dtCashTrans;
        }

        private DataTable AssignCashBank()
        {
            dtCashBankTransaction.Rows.Add(string.Empty, string.Empty, CashBankId, CashBankAmount, string.Empty, null, string.Empty, string.Empty, 0);
            return dtCashBankTransaction;
        }

        private bool ValidateTDSPayment()
        {
            bool isTrue = true;
            if (CashBankId.Equals((int)YesNo.No))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDLedger.FD_CASH_BANK_EMPTY));
                glkpCashOrBank.Focus();
                isTrue = false;
            }
            return isTrue;
        }

        private void ProcessShortcutKeys(KeyEventArgs e)
        {
            try
            {
                if (e.KeyData.Equals(Keys.Alt | Keys.P))
                {
                    GetPendingPartyPayment();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        public DataTable ChangeTransMode()
        {
            DataTable dtTransMode = new DataTable();
            try
            {
                foreach (DataRow dr in dtTDSPendingSelected.Rows)
                {
                    if (dr["TRANS_MODE"].ToString().Equals(TransactionMode.CR.ToString()))
                    {
                        dr.SetField("TRANS_MODE", "DR");
                    }
                    else
                    {
                        dr.SetField("TRANS_MODE", "CR");
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
            return dtTDSPendingSelected;
        }
        #endregion

        private void gvPaymentsParty_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                //double ActualAmount = gvPaymentsParty.GetFocusedRowCellValue(colActualAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvPaymentsParty.GetFocusedRowCellValue(colActualAmount).ToString()) : 0;
                //double PaymentAmount = gvPaymentsParty.GetFocusedRowCellValue(ColPaymentAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvPaymentsParty.GetFocusedRowCellValue(ColPaymentAmount).ToString()) : 0;
                //if (ActualAmount < PaymentAmount)
                //{
                //    gvPaymentsParty.SetFocusedRowCellValue(ColVoucherMode, "CR");
                //}
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void gvPaymentsParty_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void gcPaymentsParty_Enter(object sender, EventArgs e)
        {
            //  gvPaymentsParty.OptionsSelection.EnableAppearanceFocusedCell = true;
        }

        private void gcPaymentsParty_Leave(object sender, EventArgs e)
        {
            //  gvPaymentsParty.OptionsSelection.EnableAppearanceFocusedCell = false;
        }

        private void glkpCashOrBank_Validating(object sender, CancelEventArgs e)
        {
            if (glkpCashOrBank.EditValue != null)
            {
                gvPaymentsParty.Focus();
                gvPaymentsParty.FocusedColumn = ColPaymentAmount;
                gvPaymentsParty.MoveFirst();
            }
        }
    }
}