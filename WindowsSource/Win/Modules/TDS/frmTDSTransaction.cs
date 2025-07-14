using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.Configuration;

using Bosco.Model.UIModel;
using Bosco.Model.Transaction;
using Bosco.Model.TDS;
using Bosco.Utility;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Columns;

namespace ACPP.Modules.TDS
{
    public partial class frmTDSTransaction : frmFinanceBaseAdd
    {
        #region Decelaration
        ResultArgs resultArgs = null;
        private const string TAX_LEDGER_ID = "TAX_LEDGER_ID";
        private const string NATURE_PAY_ID = "NATURE_PAY_ID";
        private const string NATURE_PAYMENTS = "NATURE_PAYMENTS";
        private const string ASSESS_AMOUNT = "ASSESS_AMOUNT";
        private string TaxRate = string.Empty;
        private string TaxAmount = string.Empty;
        DataTable dtTransSummary = new DataTable();
        private TaxPolicyId TaxRatePolicyId;
        private int DeductNowType { get; set; }

        #endregion

        #region Properties
        private int ledgerId = 0;
        private int LedgerId
        {
            get { return gvTDSJournal.GetFocusedRowCellValue(colLedgerId) != null ? this.UtilityMember.NumberSet.ToInteger(gvTDSJournal.GetFocusedRowCellValue(colLedgerId).ToString()) : 0; }
            set { ledgerId = value; }
        }

        private double amount = 0;
        private double Amount
        {
            get { return gvTDSJournal.GetFocusedRowCellValue(colAssesValuess) != null ? this.UtilityMember.NumberSet.ToDouble(gvTDSJournal.GetFocusedRowCellValue(colAssesValuess).ToString()) : 0; }
            set { amount = value; }
        }

        private int taxLedgerId = 0;
        private int TaxLedgerId
        {
            get { return gvTDSJournal.GetFocusedRowCellValue(colTDSLedgerId) != null ? this.UtilityMember.NumberSet.ToInteger(gvTDSJournal.GetFocusedRowCellValue(colTDSLedgerId).ToString()) : 0; }
            set { taxLedgerId = value; }
        }

        private string DeductType;
        private string deductType
        {
            get { return gvTDSJournal.GetFocusedRowCellValue(colDeductTypes) != null ? gvTDSJournal.GetFocusedRowCellValue(colDeductTypes).ToString() : string.Empty; }
            set { DeductType = value; }
        }

        private int NatureofPaymentId = 0;
        private int NaturePayId
        {
            get { return gvTDSJournal.GetFocusedRowCellValue(colNatureofPayment) != null ? this.UtilityMember.NumberSet.ToInteger(gvTDSJournal.GetFocusedRowCellValue(colNatureofPayment).ToString()) : 0; }
            set { NatureofPaymentId = value; }
        }

        private int tdsLedgerId = 0;
        private int TDSLedgerID
        {
            get { return gvTDSJournal.GetFocusedRowCellValue(colTDSLedger) != null ? this.UtilityMember.NumberSet.ToInteger(gvTDSJournal.GetFocusedRowCellValue(colTDSLedger).ToString()) : 0; }
            set { tdsLedgerId = value; }
        }


        public int PartyLedgerId { get; set; }
        public int DeducteeId { get; set; }
        private double ExpenseLedgerAmount { get; set; }
        public int ExpenseLedgerId { get; set; }
        public double ExpAmount { get; set; }

        private string TDSLedgerName = string.Empty;
        private string TDSLedgerValue
        {
            get { return gvTDSJournal.GetFocusedRowCellDisplayText(colTDSLedger) != null ? gvTDSJournal.GetFocusedRowCellDisplayText(colTDSLedger).ToString() : string.Empty; }
        }

        public decimal tdsNetPayableAmount = 0;
        public decimal TDSNetPayableAmount
        {
            get { return !string.IsNullOrEmpty(txtNetPayableAmount.Text) ? this.UtilityMember.NumberSet.ToDecimal(txtNetPayableAmount.Text) : 0; }
            set { tdsNetPayableAmount = value; }
        }

        public decimal tdsDeductedAmount = 0;
        public decimal TDSDeductedAmount
        {
            get { return !string.IsNullOrEmpty(txtNetTDSAmount.Text) ? this.UtilityMember.NumberSet.ToDecimal(txtNetTDSAmount.Text) : 0; }
            set { tdsDeductedAmount = value; }
        }

        public DataTable dtBookingDetail;
        public DataTable BookingDetail
        {
            get
            {
                dtBookingDetail = gcTDSJournal.DataSource as DataTable;
                DataView dvBookingDetail = dtBookingDetail.DefaultView;
                dvBookingDetail.RowFilter = NATURE_PAY_ID + ">" + (int)YesNo.No;
                dtBookingDetail = dvBookingDetail.ToTable();
                dvBookingDetail.RowFilter = "";
                return dtBookingDetail;
            }
            set { dtBookingDetail = value; }
        }

        private int PartyLedgerDeductTypeId { get; set; }
        private int ProjectId { get; set; }
        private string LedgerName { get; set; }
        public int DefaultNatureofPaymentId { get; set; }
        public DataTable dtTDSTransSummary { get; set; }
        public DataTable dtTDSTransInfo { get; set; }
        public DataTable dtTDSMasterDetails { get; set; }
        public DataTable dtTDSBookingDetail { get; set; }

        private DataTable dtAssignMappedNatureOfPayments { get; set; }

        private double SummaryAmount = 0;
        private double AssessAmountSummaryValue
        {
            get { return colAssesValuess.SummaryItem.SummaryValue != null ? this.UtilityMember.NumberSet.ToDouble(colAssesValuess.SummaryItem.SummaryValue.ToString()) : 0; }
            set { SummaryAmount = value; }
        }

        private bool TransacationGridNewItem
        {
            set
            {
                if (value)
                {
                    DataTable dtTDSBooking = gcTDSJournal.DataSource as DataTable;
                    dtTDSBooking.Rows.Add(dtTDSBooking.NewRow());
                    gcTDSJournal.DataSource = dtTDSBooking;
                    gvTDSJournal.ShowEditor();
                }
            }
        }

        public int BookingId { get; set; }
        public int VoucherId { get; set; }
        private double ExpensesAmount { get; set; }
        private string ProjectName { get; set; }
        private string DeductTypeVal { get; set; }
        private int NaturePaymentId { get; set; }
        public double AssessAmount { get; set; }
        private double SumofAssesAmount { get; set; }
        private int PreviousExpLedgerId { get; set; }
        private int PreviousPartyLedgerId { get; set; }
        public int DeducteeTypeId { get; set; }
        public int IsVoucherTransType { get; set; }
        private DateTime VoucherDate { get; set; }
        public bool isTaxDeductable { get; set; }
        public int isTaxDeductedAlready { get; set; }
        public double ExemptionLimit { get; set; }

        public DataTable dtPartyTrans { get; set; }
        public string TDSPartyNarration { get; set; }
        public DataView dvTransUcSummary { get; set; }
        public Dictionary<int, double> TDSAmountValidation = new Dictionary<int, double>();

        public double TaxLedgerAmount { get; set; }
        public double PartyLedgerAmount { get; set; }
        public bool isAmountMismatch = false;
        #endregion

        #region Constructor
        public frmTDSTransaction()
        {
            InitializeComponent();
        }

        public frmTDSTransaction(int BookingId, int VoucherID)
            : this()
        {
            this.BookingId = BookingId;
            this.VoucherId = VoucherID;
        }

        public frmTDSTransaction(int ProjectId, int LedgerId, double ExpenseAmount, string ExpenseLedgerName, DateTime voucherDate)
            : this()
        {
            this.ProjectId = ProjectId;
            this.ExpenseLedgerId = LedgerId;
            this.ExpenseLedgerAmount = ExpenseAmount;
            this.LedgerName = ExpenseLedgerName;
            this.VoucherDate = voucherDate;
        }

        #endregion

        #region Events

        /// <summary>
        /// Load Default values 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmTDSTransaction_Load(object sender, EventArgs e)
        {
            gvTDSJournal.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            SetDefaults();
            FetchDefaults();
        }

        /// <summary>
        /// Save TDS Booking Information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateTrans())
                {
                    if (TDSLedgerID > 0)
                    {
                        FillDictionary(TDSLedgerID, TaxLedgerAmount, PartyLedgerId, PartyLedgerAmount);
                    }
                    this.DialogResult = DialogResult.OK;
                    resultArgs = SaveTDSBooking();
                    DeductNowType = UtilityMember.NumberSet.ToInteger(deductType);
                    if (isTaxDeductedAlready.Equals(0) && isTaxDeductable)
                    {
                        string PartyLedgerName = GetLedgerName(PartyLedgerId);
                        //if (this.ShowConfirmationMessage("TDS is not deducted for the previous bills for '" + PartyLedgerName + "' " + Environment.NewLine + Environment.NewLine + "                          Do you want to deduct now?          " + Environment.NewLine + Environment.NewLine + "(This message will appear once when the exemption limit is crossed)", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.TDS.TDSSection.TDS_DEDUCT_PREVIOUS_BILLS_INFO) + PartyLedgerName + "' " + Environment.NewLine + Environment.NewLine + "                          Do you want to deduct now?          " + Environment.NewLine + Environment.NewLine + "(This message will appear once when the exemption limit is crossed)", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            ACPP.Modules.TDS.frmTDSPartyTrans frmTDSPayment = new TDS.frmTDSPartyTrans(VoucherDate, ProjectId, PartyLedgerId, NaturePayId, TaxLedgerId, true);
                            frmTDSPayment.ShowDialog();
                            dtPartyTrans = frmTDSPayment.dtPartyTransAtJournal;
                            dvTransUcSummary = frmTDSPayment.dtUcTransSummary;
                            TDSPartyNarration = frmTDSPayment.Narration;
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

        /// <summary>
        /// Close the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucTransactionSummary_PreviewKeyDownEvent(object sender, EventArgs e)
        {
            if (ucTransactionSummary.UcTransGrid.IsLastRow && ucTransactionSummary.UcTransGrid.FocusedColumn == ucTransactionSummary.UcTransColumn)
            {
                btnSave.Select();
                btnSave.Focus();
            }
        }

        #endregion

        #region Transaction Events
        private void gvTDSJournal_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (gvTDSJournal.FocusedColumn != null)
                {
                    if (VoucherId > 0 && BookingId > 0)
                    {
                        if (gvTDSJournal.FocusedColumn.Equals(colDeductTypes) && gvTDSJournal.GetRowCellValue(e.RowHandle, colDeductTypes) != null)
                        {
                            colDeductTypes.OptionsColumn.AllowEdit = false;
                            string DeducteeType = deductType;
                            if (!string.IsNullOrEmpty(DeducteeType))
                            {
                                if (!string.IsNullOrEmpty(DeducteeType) && DeducteeType.Equals("0"))
                                {
                                    colTDSLedger.OptionsColumn.AllowEdit = true;
                                }
                                else
                                {
                                    colTDSLedger.OptionsColumn.AllowEdit = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (gvTDSJournal.FocusedColumn.Equals(colDeductTypes) && gvTDSJournal.GetRowCellValue(e.RowHandle, colDeductTypes) != null)
                        {
                            string DeducteeType = deductType;
                            if (!string.IsNullOrEmpty(DeducteeType))
                            {
                                if (!string.IsNullOrEmpty(DeducteeType) && DeducteeType.Equals("0"))
                                {
                                    colTDSLedger.OptionsColumn.AllowEdit = true;
                                }
                                else
                                {
                                    colTDSLedger.OptionsColumn.AllowEdit = false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void gvTDSJournal_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            this.gvTDSJournal.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvTDSJournal_CellValueChanged);
            if (gvTDSJournal.FocusedColumn == colNatureofPayment)
            {
                if (NaturePayId > (int)YesNo.No)
                {
                    DataTable dtBookingDetail = gcTDSJournal.DataSource as DataTable;
                    double AssessAmount = this.UtilityMember.NumberSet.ToDouble(gvTDSJournal.GetFocusedRowCellValue(colAssesValuess).ToString());
                    double Amt = dtBookingDetail != null && dtBookingDetail.Rows.Count > 0 ? this.UtilityMember.NumberSet.ToDouble(dtBookingDetail.Compute("SUM(ASSESS_AMOUNT)", "").ToString()) : 0;
                    if (AssessAmount <= 0)
                    {
                        if (ExpenseLedgerAmount > 0 && Amt < ExpenseLedgerAmount)
                        {
                            gvTDSJournal.SetRowCellValue(e.RowHandle, colAssesValuess, ExpenseLedgerAmount - Amt);
                        }
                        else if (ExpenseLedgerAmount == 0)
                        {
                            gvTDSJournal.SetRowCellValue(e.RowHandle, colAssesValuess, ExpenseLedgerAmount - Amt);
                        }
                    }
                    DeductTypeVal = !string.IsNullOrEmpty(deductType) ? deductType.Equals("0") ? YesNo.Yes.ToString() : YesNo.No.ToString() : string.Empty;
                    if (!string.IsNullOrEmpty(DeductTypeVal))
                    {
                        TAXCalcuation(DeductTypeVal);
                    }
                }
            }
            else if (gvTDSJournal.FocusedColumn == colAssesValuess)
            {
                CalculateTDSNow();
            }
            else if (gvTDSJournal.FocusedColumn == colDeductTypes)
            {
                CalculateTDSNow();
            }
            else if (gvTDSJournal.FocusedColumn == colTDSLedger)
            {
                CalculateTDSNow();
            }
            this.gvTDSJournal.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvTDSJournal_CellValueChanged);
        }

        private void glkpPartyLedger_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                ACPP.Modules.Master.frmLedgerBankAccountAdd frmLedgerAdd = new Master.frmLedgerBankAccountAdd((int)AddNewRow.NewRow, ledgerSubType.TDS);
                frmLedgerAdd.ShowDialog();
            }
        }

        private void gcTDSJournal_ProcessGridKey(object sender, KeyEventArgs e)
        {
            bool canFocusNameAddress = false;
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab)
               && !e.Shift && !e.Alt && !e.Control
               && (gvTDSJournal.FocusedColumn == colTDSLedger))
            {
                DataTable dtBookingDetail = gcTDSJournal.DataSource as DataTable;
                if (NaturePayId == 0 && Amount == 0 && string.IsNullOrEmpty(deductType) && TDSLedgerID == 0) { canFocusNameAddress = true; }
                double Amt = dtBookingDetail != null && dtBookingDetail.Rows.Count > 0 ? this.UtilityMember.NumberSet.ToDouble(dtBookingDetail.Compute("SUM(" + ASSESS_AMOUNT + ")", "").ToString()) : 0;
                if (Amt.Equals(ExpenseLedgerAmount) || Amt > ExpenseLedgerAmount)
                {
                    if ((gvTDSJournal.FocusedColumn == colTDSLedger)
                           || (gvTDSJournal.FocusedColumn == colDeductTypes))
                    {
                        if (gvTDSJournal.IsLastRow && NaturePayId > 0 && Amount > 0 && !string.IsNullOrEmpty(deductType) && TDSLedgerID > 0) { canFocusNameAddress = true; }
                        else if (gvTDSJournal.IsLastRow)
                        {
                            canFocusNameAddress = true;
                        }
                    }
                }
                else
                {
                    if (gvTDSJournal.IsLastRow)
                    {
                        if (BookingId.Equals(0))
                        {
                            if (NaturePayId > 0 && Amount > 0 && !string.IsNullOrEmpty(deductType))
                                TransacationGridNewItem = true;
                        }
                    }
                }
                if (canFocusNameAddress)
                {
                    gvTDSJournal.CloseEditor();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    btnSave.Select();
                }
                CalNetTDSAmount();
            }
        }

        private void gvTDSJournal_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            if (ExpenseLedgerAmount > (int)YesNo.No)
            {
                if (gvTDSJournal.FocusedColumn == colNatureofPayment)
                {
                    if (NaturePayId > (int)YesNo.No)
                    {
                        DataTable dtBookingDetail = gcTDSJournal.DataSource as DataTable;
                        double AssessAmount = this.UtilityMember.NumberSet.ToDouble(gvTDSJournal.GetFocusedRowCellValue(colAssesValuess).ToString());
                        double Amt = dtBookingDetail != null && dtBookingDetail.Rows.Count > 0 ? this.UtilityMember.NumberSet.ToDouble(dtBookingDetail.Compute("SUM(" + ASSESS_AMOUNT + ")", "").ToString()) : 0;
                        DeductTypeVal = !string.IsNullOrEmpty(deductType) ? deductType.Equals("0") ? YesNo.Yes.ToString() : YesNo.No.ToString() : string.Empty;
                        if (!string.IsNullOrEmpty(DeductTypeVal))
                        {
                            TAXCalcuation(DeductTypeVal);
                        }
                    }
                }
                else if (gvTDSJournal.FocusedColumn == colAssesValuess)
                {
                    if (NaturePayId > 0)
                    {
                        CalculateTDSNow();
                    }
                    else
                    {
                        gvTDSJournal.PostEditor();
                        gvTDSJournal.UpdateCurrentRow();
                        gvTDSJournal.FocusedColumn = e.PrevFocusedColumn;
                    }
                }
                else if (gvTDSJournal.FocusedColumn == colDeductTypes)
                {
                    if (NaturePayId > 0)
                    {
                        if (Amount > 0)
                        {
                            CalculateTDSNow();
                        }
                        else
                        {
                            gvTDSJournal.PostEditor();
                            gvTDSJournal.UpdateCurrentRow();
                            gvTDSJournal.FocusedColumn = e.PrevFocusedColumn;
                        }
                    }
                    else
                    {
                        gvTDSJournal.PostEditor();
                        gvTDSJournal.UpdateCurrentRow();
                        gvTDSJournal.FocusedColumn = colNatureofPayment;
                    }
                }

                else if (gvTDSJournal.FocusedColumn == colTDSLedger)
                {
                    if (NaturePayId > 0)
                    {
                        if (Amount > 0)
                        {
                            if (!string.IsNullOrEmpty(deductType))
                            {
                                if (TDSLedgerID == (int)YesNo.No)
                                {
                                    gvTDSJournal.PostEditor();
                                    gvTDSJournal.FocusedColumn = e.FocusedColumn;
                                }
                                else
                                {
                                    CalculateTDSNow();
                                }
                            }
                            else
                            {
                                gvTDSJournal.PostEditor();
                                gvTDSJournal.UpdateCurrentRow();
                                gvTDSJournal.FocusedColumn = e.PrevFocusedColumn;
                            }
                        }
                        else
                        {
                            gvTDSJournal.PostEditor();
                            gvTDSJournal.UpdateCurrentRow();
                            gvTDSJournal.FocusedColumn = colAssesValuess;
                        }
                    }
                    else
                    {
                        gvTDSJournal.PostEditor();
                        gvTDSJournal.UpdateCurrentRow();
                        gvTDSJournal.FocusedColumn = colNatureofPayment;
                    }
                }
            }
        }

        private void glkpPartyLedger_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData.Equals(Keys.Tab) || e.KeyData.Equals(Keys.Enter))
            {
                SetFocusToGridControl();
            }
        }
        #endregion

        #region Common Methods
        /// <summary>
        /// Load default Values
        /// </summary>
        private void FetchDefaults()
        {
            FetchNatureofPayments();
            LoadLedgers(rglkpTDSLedger);
            AssignDeducteeType();
            LoadDeductType();
            ConstructTransEmptySource();
            FetchBookingId();
            if (dtTDSBookingDetail != null)
            {
                gcTDSJournal.DataSource = dtTDSBookingDetail;
                AssignTDSMasterDetails();
            }
            if (VoucherId > 0 && BookingId > 0 && dtTDSBookingDetail == null)
            {
                AssignTDSBookingDetails();
            }
            TDSAmountValidation.Clear();
        }

        /// <summary>
        /// Set Default values
        /// </summary>
        private void SetDefaults()
        {
            dtTransSummary = ucTransactionSummary.ConstructTable();
            this.Text = VoucherId.Equals((int)YesNo.No) ? "TDS Journal (Add)" : "TDS Journal (Edit)";
            lblExpenseLedgerName.Text = this.LedgerName;
            if (dtTDSBookingDetail != null && dtTDSBookingDetail.Rows.Count > 0)
            {
                double PartyAmount = this.UtilityMember.NumberSet.ToDouble(dtTDSBookingDetail.Compute("SUM(PARTY_AMOUNT)", "").ToString());
                double TDSAmount = this.UtilityMember.NumberSet.ToDouble(dtTDSBookingDetail.Compute("SUM(TDS_AMOUNT)", "").ToString());
                ExpenseLedgerAmount = PartyAmount + TDSAmount;
                if (ExpAmount < ExpenseLedgerAmount)
                {
                    ExpAmount = ExpenseLedgerAmount;
                    lblTDSAmount.Text = this.UtilityMember.NumberSet.ToNumber(ExpAmount) + " " + TransactionMode.CR.ToString();
                }
                else if (ExpAmount > ExpenseLedgerAmount)
                {
                    ExpenseLedgerAmount = ExpAmount;
                    lblTDSAmount.Text = this.UtilityMember.NumberSet.ToNumber(ExpAmount) + " " + TransactionMode.CR.ToString();
                }
                else if (ExpAmount.Equals(ExpenseLedgerAmount))
                {
                    lblTDSAmount.Text = this.UtilityMember.NumberSet.ToNumber(PartyAmount + TDSAmount) + " " + TransactionMode.CR.ToString();
                }
            }
            else
            {
                lblTDSAmount.Text = this.UtilityMember.NumberSet.ToNumber(this.ExpenseLedgerAmount) + " " + TransactionMode.CR.ToString();
            }
        }

        /// <summary>
        /// Construct Empty Data Source for Grid Control
        /// </summary>
        private void ConstructTransEmptySource()
        {
            DataTable dtTransaction = new DataTable();
            using (TDSDeductionSystem tdsBookingDetailSystem = new TDSDeductionSystem())
            {
                dtTransaction.Columns.Add("NATURE_PAY_ID", typeof(Int32));
                dtTransaction.Columns.Add("ASSESS_AMOUNT", typeof(decimal));
                dtTransaction.Columns.Add("Id", typeof(String));
                dtTransaction.Columns.Add("LEDGER_ID", typeof(Int32));
                dtTransaction.Columns.Add("TAX_AMOUNT", typeof(string));
                dtTransaction.Columns.Add("TDS_AMOUNT", typeof(decimal));
                dtTransaction.Columns.Add("PARTY_LEDGER_ID", typeof(Int32));
                dtTransaction.Columns.Add("PARTY_AMOUNT", typeof(decimal));
                dtTransaction.Columns.Add("VALUE", typeof(Int32));
            }

            dtAssignMappedNatureOfPayments = dtTransaction.Clone();
            dtTransaction.Rows.Add(dtTransaction.NewRow());

            dtTransaction.Rows[0]["NATURE_PAY_ID"] = DefaultNatureofPaymentId;
            dtTransaction.Rows[0]["ASSESS_AMOUNT"] = ExpenseLedgerAmount;
            if (isTaxDeductable)
            {
                dtTransaction.Rows[0]["Id"] = "0";
            }
            else
            {
                dtTransaction.Rows[0]["Id"] = "1";
            }
            // CalculateTDSNow("0");
            gcTDSJournal.DataSource = dtTransaction;
        }
        #endregion

        #region Common Methods for Fetching Data
        /// <summary>
        /// Fetch Default Nature of Payments.
        /// </summary>
        private void FetchNatureofPayments()
        {
            try
            {
                using (NatureofPaymentsSystem natureOfPayments = new NatureofPaymentsSystem())
                {
                    resultArgs = natureOfPayments.FetchNatureofPaymentWithCode();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpNatureofPayments, resultArgs.DataSource.Table, "NAME", "NATURE_PAY_ID");
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
        /// Fetch TDS Ledger Based on the Selected Nature of Payments
        /// </summary>
        private void FetchTDSLedger()
        {
            try
            {
                using (NatureofPaymentsSystem NatureSystem = new NatureofPaymentsSystem())
                {
                    resultArgs = NatureSystem.FetchTDSLedgerByNOP();
                    this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpTDSLedger, resultArgs.DataSource.Table, NatureSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, "LEDGER_ID");
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        /// <summary>
        /// Fetch Nature of Payments and TDS Ledgers
        /// </summary>
        /// <param name="rglkpEdit">Nature of Payments and TDS Ledgers</param>
        /// <param name="tdsLedgerGroup">Ledger Group based on Expense Ledgers ,Party Ledgers </param>
        private void LoadLedgers(DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkpEdit)
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    ledgerSystem.ProjectId = ProjectId;
                    resultArgs = ledgerSystem.FetchDutiesTaxLedgers();
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpEdit, resultArgs.DataSource.Table, ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        /// <summary>
        /// Load deductee Types from Enum
        /// </summary>
        private void LoadDeductType()
        {
            try
            {
                using (DeducteeTypeSystem DeductSystem = new DeducteeTypeSystem())
                {
                    IsPeriodically Periodically = new IsPeriodically();
                    DataView dvDeductType = this.UtilityMember.EnumSet.GetEnumDataSource(Periodically, Sorting.Ascending);
                    this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpDeducteeTypes, dvDeductType.ToTable(), "Name", "Id");
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }
        #endregion

        #region Save TDS Booking Methods

        /// <summary>
        /// Validate Mandatory Fields for TDS Trans
        /// </summary>
        /// <returns>If all sucess then Return True or False</returns>
        private bool ValidateTrans()
        {
            bool isTrue = true;
            try
            {
                if (AssessAmountSummaryValue > ExpenseLedgerAmount)
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.TDS.TDSBooking.TDS_AMOUNT_VALIDATION));
                    isTrue = false;
                    gvTDSJournal.Focus();
                    gvTDSJournal.FocusedColumn = colAssesValuess;
                    gvTDSJournal.MoveLast();
                }
                else if (AssessAmountSummaryValue < ExpenseLedgerAmount)
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.TDS.TDSBooking.TDS_AMOUNT_LESS_VALIDATION));
                    isTrue = false;
                    gvTDSJournal.Focus();
                    gvTDSJournal.FocusedColumn = colAssesValuess;
                    gvTDSJournal.MoveLast();
                }
                else if (BookingDetail == null || BookingDetail.Rows.Count <= 0)
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.TDS.TDSBooking.TDS_GRID_EMPTY));
                    isTrue = false;
                    gvTDSJournal.Focus();
                    gvTDSJournal.FocusedColumn = colNatureofPayment;
                    gvTDSJournal.MoveFirst();
                }
                if (BookingDetail == null || BookingDetail.Rows.Count <= 0)
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.TDS.TDSBooking.TDS_GRID_EMPTY));
                    isTrue = false;
                    gvTDSJournal.Focus();
                    gvTDSJournal.FocusedColumn = colNatureofPayment;
                    gvTDSJournal.MoveFirst();
                }
                if (BookingId.Equals(0))
                {
                    if (gcTDSJournal.DataSource != null && ((DataTable)gcTDSJournal.DataSource).Rows.Count > 0)
                    {
                        DataRow[] HasRows = ((DataTable)gcTDSJournal.DataSource).Select("Id=0");
                        if (HasRows.Count() > 0)
                        {
                            for (int i = 0; i < HasRows.Count(); i++)
                            {
                                int LedgerId = HasRows[i].ItemArray.GetValue(3) != DBNull.Value ? Convert.ToInt32(HasRows[i].ItemArray.GetValue(3)) : 0;

                                if (LedgerId.Equals(0))
                                {
                                    this.ShowMessageBox("TDS Ledger is empty.");
                                    isTrue = false;
                                    gvTDSJournal.FocusedColumn = colTDSLedger;
                                    gvTDSJournal.Focus();
                                    gvTDSJournal.ShowEditor();
                                    return isTrue;

                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
            return isTrue;
        }

        /// <summary>
        /// Define voucher Transactions
        /// </summary>
        /// <returns></returns>
        public DataTable SetVoucherTrans()
        {
            DataTable dtVoucherTrans = new DataTable();
            try
            {
                using (VoucherTransactionSystem voucherTransSystem = new VoucherTransactionSystem())
                {
                    dtVoucherTrans.Columns.Add(voucherTransSystem.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName, typeof(int));
                    dtVoucherTrans.Columns.Add(voucherTransSystem.AppSchema.LedgerProfileData.DEBITColumn.ColumnName, typeof(double));
                    dtVoucherTrans.Columns.Add(voucherTransSystem.AppSchema.LedgerProfileData.CREDITColumn.ColumnName, typeof(decimal));
                    dtVoucherTrans.Rows.Add(ExpenseLedgerId, ExpenseLedgerAmount, (int)YesNo.No);
                    dtVoucherTrans.Rows.Add(PartyLedgerId, (int)YesNo.No, TDSNetPayableAmount);
                    dtVoucherTrans.Rows.Add(PartyLedgerId, (int)YesNo.No, TDSDeductedAmount);
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
            return dtVoucherTrans;
        }

        public DataTable AssignMasterDetails()
        {
            DataTable dtMasterDetails = new DataTable();
            try
            {
                dtMasterDetails.Columns.Add("ID", typeof(int));
                dtMasterDetails.Columns.Add("AMOUNT", typeof(decimal));
                dtMasterDetails.Columns.Add("DETAIL_ID", typeof(int));
                dtMasterDetails.Rows.Add(1, TDSDeductedAmount, 0);
                dtMasterDetails.Rows.Add(2, TDSNetPayableAmount, 0);
                dtMasterDetails.Rows.Add(3, 0, PartyLedgerId);
                dtMasterDetails.Rows.Add(4, 0, DeducteeTypeId);
                dtMasterDetails.Rows.Add(5, 0, ExpenseLedgerId);
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
            return dtMasterDetails;
        }

        /// <summary>
        /// Save TDS Booking.
        /// </summary>
        /// <returns></returns>
        private ResultArgs SaveTDSBooking()
        {
            try
            {
                using (TDSBookingSystem BookingSystem = new TDSBookingSystem())
                {
                    BookingSystem.PartyLedgerId = PartyLedgerId;
                    BookingSystem.DeducteeId = DeducteeTypeId;
                    BookingSystem.dtBookingDetail = BookingDetail;
                    BookingSystem.TDSNetPayableAmount = TDSNetPayableAmount;
                    BookingSystem.TDSDeductedAmount = TDSDeductedAmount;
                    dtTDSBookingDetail = BookingDetail;
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        ///  Assign TDS Booking Values based  on the Selected Booking Id,Voucher Id
        /// </summary>
        private void AssignTDSBooking()
        {
            try
            {
                using (TDSBookingSystem tdsBookingSystem = new TDSBookingSystem(BookingId, VoucherId, DeducteeTypeId, this.VoucherDate))
                {
                    gcTDSJournal.DataSource = tdsBookingSystem.dtEditBookingTrans;
                    CalNetTDSAmount();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
        }


        #endregion

        #region TDS Functional Methods

        /// <summary>
        /// Calculate tax based on TDS Ledger Types 
        /// </summary>
        /// <param name="amount">The amount is Shown here as (Exepnese Ledger Amount)</param>
        /// <returns> Returns the tax amount</returns>
        private void EnableDisableColumns(bool isTrue)
        {
            if (!isTrue)
            {
                gvTDSJournal.SetRowCellValue(gvTDSJournal.FocusedRowHandle, colTDSLedger, null);
            }
        }

        /// <summary>
        /// Values must be cleared once the new record is added.
        /// </summary>
        private void ClearTDSBooking()
        {
            txtNetPayableAmount.Text = txtNetTDSAmount.Text = string.Empty;
            FetchDefaults();
            ucTransactionSummary.UpdateTDSSummary.Clear();
        }

        /// <summary>
        /// Calculate TDS Amount based on the Dedutee type and Nature of Payment selected
        /// </summary>
        /// <param name="glkpDeductType"></param>
        private void CalculateTDSTaxRate(string DeductType)
        
        {
            try
            {
                if (Amount > 0)
                {
                    using (NatureofPaymentsSystem natureOfPayment = new NatureofPaymentsSystem())
                    {
                        if (DeductType.Equals(YesNo.Yes.ToString()))
                        {
                            natureOfPayment.NatureofPaymentId = NaturePayId;
                            natureOfPayment.DeduteeTypeId = DeducteeTypeId;
                            natureOfPayment.taxPolicyId = TaxRatePolicyId;
                            natureOfPayment.ApplicableFrom = this.VoucherDate;
                            resultArgs = natureOfPayment.FetchTaxRateByNatureOfPayment();
                            TaxRate = CommonMethod.CalculateTaxRate(resultArgs.DataSource.Table, Amount, TaxPolicyId.TDSWithPAN);
                            gvTDSJournal.SetRowCellValue(gvTDSJournal.FocusedRowHandle, colPayableAmt, TaxRate);
                            gvTDSJournal.SetRowCellValue(gvTDSJournal.FocusedRowHandle, colTaxAmount, CommonMethod.SumofTaxAmount);
                            gvTDSJournal.SetRowCellValue(gvTDSJournal.FocusedRowHandle, colPartyLedgerId, PartyLedgerId);
                            gvTDSJournal.SetRowCellValue(gvTDSJournal.FocusedRowHandle, colPartyAmount, CommonMethod.NetTdsAmount);
                            gvTDSJournal.SetRowCellValue(gvTDSJournal.FocusedRowHandle, colValue, (int)YesNo.Yes);

                            //Add Values to Dictionary 
                            TaxLedgerAmount = CommonMethod.SumofTaxAmount;
                            PartyLedgerAmount = CommonMethod.NetTdsAmount;
                        }
                        else
                        {
                            natureOfPayment.taxPolicyId = TaxPolicyId.TDSWithoutPAN;
                            TaxAmount = string.Empty;
                            TaxAmount += String.Format("Payable to TDS = {0} {1}", 0, Environment.NewLine);
                            TaxAmount += String.Format("Payable to Party = {0} {1}", this.UtilityMember.NumberSet.ToNumber(Amount), Environment.NewLine);
                            gvTDSJournal.SetRowCellValue(gvTDSJournal.FocusedRowHandle, colPayableAmt, TaxAmount);
                            gvTDSJournal.SetRowCellValue(gvTDSJournal.FocusedRowHandle, colTaxAmount, 0);
                            gvTDSJournal.SetRowCellValue(gvTDSJournal.FocusedRowHandle, colPartyLedgerId, PartyLedgerId);
                            gvTDSJournal.SetRowCellValue(gvTDSJournal.FocusedRowHandle, colPartyAmount, Amount);
                            gvTDSJournal.SetRowCellValue(gvTDSJournal.FocusedRowHandle, colValue, (int)YesNo.Yes);

                            //Add values to Dictionary
                            TaxLedgerAmount = 0;
                            PartyLedgerAmount = Amount;
                        }
                        CalNetTDSAmount();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void FillDictionary(int TaxLedgerId, double TaxAmount, int PartyLedgerId, double PartyAmount)
        {
            if (TDSAmountValidation.Count > 0)
            {
                if (TDSAmountValidation.Keys.Contains(1) && TDSAmountValidation.Keys.Contains(2))
                {
                    TDSAmountValidation.Clear();
                }
            }
            TDSAmountValidation.Add(TaxLedgerId, TaxAmount);
            TDSAmountValidation.Add(PartyLedgerId, PartyAmount);
        }

        /// <summary>
        /// Calcuate Net TDS Amount based on the each transactions
        /// </summary>
        private void CalNetTDSAmount()
        {
            try
            {
                using (TDSBookingSystem BookingSystem = new TDSBookingSystem())
                {
                    DataTable dtBookingDetail = gcTDSJournal.DataSource as DataTable;
                    if (dtBookingDetail != null && dtBookingDetail.Rows.Count > 0)
                    {
                        double SumofAssessAmount = this.UtilityMember.NumberSet.ToDouble(dtBookingDetail.Compute("SUM(" + BookingSystem.AppSchema.TDSBookingDetails.ASSESS_AMOUNTColumn.ColumnName + ")", "").ToString());
                        double SumofTAXAmount = this.UtilityMember.NumberSet.ToDouble(dtBookingDetail.Compute("SUM(" + BookingSystem.AppSchema.TDSDeductionDetails.TDS_AMOUNTColumn.ColumnName + ")", "").ToString());
                        txtNetPayableAmount.Text = this.UtilityMember.NumberSet.ToNumber(SumofAssessAmount - SumofTAXAmount);
                        txtNetTDSAmount.Text = this.UtilityMember.NumberSet.ToNumber(SumofTAXAmount);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        /// <summary>
        /// Check whether the party Ledger has PAN Number or not
        /// </summary>
        /// <returns></returns>
        private int HasPANNumber()
        {
            int HasPanNumber = 0;
            try
            {
                using (LedgerProfileSystem ledgerProfileSystem = new LedgerProfileSystem())
                {
                    ledgerProfileSystem.LedgerID = PartyLedgerId;
                    HasPanNumber = ledgerProfileSystem.HasPanNumber();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
            return HasPanNumber;
        }

        /// <summary>
        /// Calcuate TDS Amount When the party Ledger does not have PAN Number
        /// TDS Amount will be calculate for 20% when the party Ledger does not have PAN Number
        /// </summary>
        /// <param name="DeductType">Deducte Type (Yes,No)</param>
        /// <returns></returns>
        private string CalTDSWithoutPANNumber(string DeductType)
        {
            string TAXDescription = string.Empty;
            try
            {
                if (Amount > 0)
                {
                    using (NatureofPaymentsSystem natureOfPayment = new NatureofPaymentsSystem())
                    {
                        if (!string.IsNullOrEmpty(DeductType) && DeductType.Equals(YesNo.Yes.ToString()))
                        {
                            natureOfPayment.NatureofPaymentId = NaturePayId;
                            natureOfPayment.DeduteeTypeId = DeducteeTypeId;
                            natureOfPayment.taxPolicyId = TaxPolicyId.TDSWithPAN;
                            natureOfPayment.ApplicableFrom = this.VoucherDate;
                            resultArgs = natureOfPayment.FetchTDSWithoutPAN();
                            TaxRate = CommonMethod.CalculateTaxRate(resultArgs.DataSource.Table, Amount, TaxPolicyId.TDSWithoutPAN);
                            gvTDSJournal.SetRowCellValue(gvTDSJournal.FocusedRowHandle, colPayableAmt, TaxRate);
                            gvTDSJournal.SetRowCellValue(gvTDSJournal.FocusedRowHandle, colTaxAmount, CommonMethod.SumofTaxAmount);
                            gvTDSJournal.SetRowCellValue(gvTDSJournal.FocusedRowHandle, colPartyLedgerId, PartyLedgerId);
                            gvTDSJournal.SetRowCellValue(gvTDSJournal.FocusedRowHandle, colPartyAmount, CommonMethod.NetTdsAmount);
                            gvTDSJournal.SetRowCellValue(gvTDSJournal.FocusedRowHandle, colValue, (int)YesNo.Yes);

                            //Add Values to Dictionary
                            TaxLedgerAmount = CommonMethod.SumofTaxAmount;
                            PartyLedgerAmount = CommonMethod.NetTdsAmount;
                        }
                        else
                        {
                            natureOfPayment.taxPolicyId = TaxPolicyId.TDSWithoutPAN;
                            TaxAmount = string.Empty;
                            TaxAmount += String.Format("Payable to TDS = {0} {1}", 0, Environment.NewLine);
                            TaxAmount += String.Format("Payable to Party= {0} {1}", this.UtilityMember.NumberSet.ToNumber(Amount), Environment.NewLine);
                            gvTDSJournal.SetRowCellValue(gvTDSJournal.FocusedRowHandle, colPayableAmt, TaxAmount);
                            gvTDSJournal.SetRowCellValue(gvTDSJournal.FocusedRowHandle, colTaxAmount, 0);
                            gvTDSJournal.SetRowCellValue(gvTDSJournal.FocusedRowHandle, colPartyLedgerId, PartyLedgerId);
                            gvTDSJournal.SetRowCellValue(gvTDSJournal.FocusedRowHandle, colPartyAmount, Amount);
                            gvTDSJournal.SetRowCellValue(gvTDSJournal.FocusedRowHandle, colValue, (int)YesNo.Yes);

                            //Add Values to Dictionary
                            TaxLedgerAmount = 0;
                            PartyLedgerAmount = Amount;
                        }
                        CalNetTDSAmount();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
            return TAXDescription;
        }

        private double FetchTDSWithoutPAN()
        {
            double TDSWithoutPAN = 0;
            using (NatureofPaymentsSystem NaturePayment = new NatureofPaymentsSystem())
            {
                NaturePayment.NatureofPaymentId = NaturePayId;
                NaturePayment.DeduteeTypeId = DeducteeTypeId;
                NaturePayment.taxPolicyId = TaxPolicyId.TDSWithoutPAN;
                NaturePayment.ApplicableFrom = this.VoucherDate;
                resultArgs = NaturePayment.FetchTaxRateByNatureOfPayment();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    TDSWithoutPAN = resultArgs.DataSource.Table.Rows[0]["TDS_RATE"] != DBNull.Value ? this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0]["TDS_RATE"].ToString()) : 0;
                }
            }
            return TDSWithoutPAN;
        }

        /// <summary>
        /// Update Trans Summary for each and every Nature of payments
        /// </summary>
        private void UpdateTransSummary()
        {
            try
            {
                using (TDSBookingSystem BookingSystem = new TDSBookingSystem())
                {
                    if (dtTransSummary != null && dtTransSummary.Rows.Count > 0)
                    {
                        DataTable dtTransactionSummary = gcTDSJournal.DataSource as DataTable;
                        if (dtTransactionSummary != null && dtTransactionSummary.Rows.Count > (int)YesNo.Yes)
                        {
                            dtTransSummary = FilterTransSummary();
                            dtTransSummary.NewRow();
                            double TdsAmount = this.UtilityMember.NumberSet.ToDouble(dtTransactionSummary.Compute("SUM(" + BookingSystem.AppSchema.TDSDeductionDetails.TDS_AMOUNTColumn.ColumnName + ")", "").ToString());
                            if (PartyLedgerId > 0)
                                dtTransSummary.Rows.Add(PartyLedgerId, lblExpenseLedgerName.Text, ExpenseLedgerAmount - TdsAmount, TransactionMode.CR.ToString(), (int)SetDefaultValue.DefaultValue);
                            foreach (DataRow dr in dtTransactionSummary.Rows)
                            {
                                int LedgerId = dr[BookingSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dr[BookingSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()) : 0;
                                double TAXAmount = dr[BookingSystem.AppSchema.TDSDeductionDetails.TDS_AMOUNTColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToDouble(dr[BookingSystem.AppSchema.TDSDeductionDetails.TDS_AMOUNTColumn.ColumnName].ToString()) : 0;
                                if (LedgerId > 0)
                                {
                                    string ExpLedgerName = FetchLedgerName(LedgerId);
                                    if (LedgerId > 0 && TAXAmount > 0 && !string.IsNullOrEmpty(ExpLedgerName))
                                        dtTransSummary.Rows.Add(LedgerId, ExpLedgerName, TAXAmount, TransactionMode.CR.ToString(), (int)SetDefaultValue.DefaultValue);
                                }
                            }
                        }
                        else if (dtTransactionSummary != null && dtTransactionSummary.Rows.Count == (int)YesNo.Yes)
                        {
                            dtTransSummary = FilterTransSummary();
                            dtTransSummary.NewRow();
                            double TdsAmount = this.UtilityMember.NumberSet.ToDouble(dtTransactionSummary.Compute("SUM(" + BookingSystem.AppSchema.TDSDeductionDetails.TDS_AMOUNTColumn.ColumnName + ")", "").ToString());
                            if (PartyLedgerId > 0)
                                dtTransSummary.Rows.Add(PartyLedgerId, lblExpenseLedgerName.Text, ExpenseLedgerAmount - TdsAmount, TransactionMode.CR.ToString(), (int)SetDefaultValue.DefaultValue);
                            foreach (DataRow dr in dtTransactionSummary.Rows)
                            {
                                int LedgerId = dr[BookingSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dr[BookingSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()) : 0;
                                double TAXAmount = dr[BookingSystem.AppSchema.TDSDeductionDetails.TDS_AMOUNTColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToDouble(dr[BookingSystem.AppSchema.TDSDeductionDetails.TDS_AMOUNTColumn.ColumnName].ToString()) : 0;
                                if (LedgerId > 0)
                                {
                                    string ExpLedgerName = FetchLedgerName(LedgerId);
                                    if (LedgerId > 0 && TAXAmount > 0 && !string.IsNullOrEmpty(ExpLedgerName))
                                        dtTransSummary.Rows.Add(LedgerId, ExpLedgerName, TAXAmount, TransactionMode.CR.ToString(), (int)SetDefaultValue.DefaultValue);
                                }
                            }
                        }
                        else
                        {
                            if (ucTransactionSummary.UpdateTDSSummary != null && ucTransactionSummary.UpdateTDSSummary.Rows.Count > 0)
                            {
                                foreach (DataRow dr in ucTransactionSummary.UpdateTDSSummary.Rows)
                                {
                                    if (dr[BookingSystem.AppSchema.LedgerBalance.TRANS_MODEColumn.ColumnName].ToString().Equals(TransactionMode.DR.ToString()))
                                    {
                                        dr.SetField(BookingSystem.AppSchema.TDSBooking.AMOUNTColumn.ColumnName, ExpenseLedgerAmount);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(LedgerName) && ExpenseLedgerId > 0 && ExpenseLedgerAmount > 0)
                            dtTransSummary.Rows.Add(ExpenseLedgerId, LedgerName, ExpenseLedgerAmount, TransactionMode.DR.ToString(), (int)SetDefaultValue.DefaultValue);
                    }
                }
                ucTransactionSummary.UpdateTDSSummary = dtTransSummary;
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void UpdateTransSummaryAtEdit()
        {
            DataTable dtTransactionSummary = gcTDSJournal.DataSource as DataTable;
            using (TDSBookingSystem BookingSystem = new TDSBookingSystem())
            {
                if (dtTransactionSummary != null && dtTransactionSummary.Rows.Count > 0)
                {
                    dtTransSummary.Rows.Add(ExpenseLedgerId, LedgerName, ExpenseLedgerAmount, TransactionMode.DR.ToString());
                    double TdsAmount = this.UtilityMember.NumberSet.ToDouble(dtTransactionSummary.Compute("SUM(" + BookingSystem.AppSchema.TDSDeductionDetails.TDS_AMOUNTColumn.ColumnName + ")", "").ToString());
                    dtTransSummary.Rows.Add(PartyLedgerId, lblExpenseLedgerName.Text, ExpenseLedgerAmount - TdsAmount, TransactionMode.CR.ToString(), (int)SetDefaultValue.DefaultValue);
                    foreach (DataRow dr in dtTransactionSummary.Rows)
                    {
                        int LedgerId = dr[BookingSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dr[BookingSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()) : 0;
                        double TAXAmount = dr[BookingSystem.AppSchema.TDSDeductionDetails.TDS_AMOUNTColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToDouble(dr[BookingSystem.AppSchema.TDSDeductionDetails.TDS_AMOUNTColumn.ColumnName].ToString()) : 0;
                        if (LedgerId > 0)
                        {
                            string ExpLedgerName = FetchLedgerName(LedgerId);
                            if (LedgerId > 0 && TAXAmount > 0 && !string.IsNullOrEmpty(ExpLedgerName))
                                dtTransSummary.Rows.Add(LedgerId, ExpLedgerName, TAXAmount, TransactionMode.CR.ToString(), (int)SetDefaultValue.DefaultValue);
                        }
                    }
                }
                ucTransactionSummary.UpdateTDSSummary = dtTransSummary;
            }
        }

        /// <summary>
        /// Calcuate TAX Amount Based on the Deductee Types
        /// </summary>
        /// <param name="DeductTypeVal"></param>
        private void TAXCalcuation(string DeductTypeVal)
        {
            if (BookingId.Equals(0) && VoucherId.Equals(0) && !isAmountMismatch)
            {
                if (HasPANNumber() > 0)
                {
                    CalculateTDSTaxRate(DeductTypeVal);
                }
                else
                {
                    CalTDSWithoutPANNumber(DeductTypeVal);
                }
            }
            else if (isAmountMismatch)
            {
                if (HasPANNumber() > 0)
                {
                    CalculateTDSTaxRate(DeductTypeVal);
                }
                else
                {
                    CalTDSWithoutPANNumber(DeductTypeVal);
                }
            }
        }

        private void CalculateTDSNow()
        {
            if (!string.IsNullOrEmpty(deductType))
            {
                DataTable dtBookingDetails = gcTDSJournal.DataSource as DataTable;
                DeductTypeVal = deductType.Equals("0") ? YesNo.Yes.ToString() : YesNo.No.ToString();

                double Amt = dtBookingDetails != null && dtBookingDetails.Rows.Count > 0 ? this.UtilityMember.NumberSet.ToDouble(dtBookingDetails.Compute("SUM(" + ASSESS_AMOUNT + ")", "").ToString()) : 0;
                if (DeductTypeVal.Equals(YesNo.No.ToString()))
                {
                    if (NaturePayId > 0 && Amt <= ExpenseLedgerAmount || Amt >= ExpenseLedgerAmount)
                    {
                        EnableDisableColumns(false);
                        TAXCalcuation(DeductTypeVal);
                    }
                }
                else
                {
                    if (NaturePayId > 0 && Amt <= ExpenseLedgerAmount || Amt >= ExpenseLedgerAmount)
                    {
                        EnableDisableColumns(true);
                        TAXCalcuation(DeductTypeVal);
                    }
                }
            }
        }

        private void CalculateTDSNow(string DeducteeType)
        {
            if (!string.IsNullOrEmpty(DeducteeType))
            {
                DeductTypeVal = DeducteeType.Equals("0") ? YesNo.Yes.ToString() : YesNo.No.ToString();
                if (DeductTypeVal.Equals(YesNo.Yes.ToString()))
                {
                    if (DefaultNatureofPaymentId > 0 && ExpenseLedgerAmount > 0)
                    {
                        EnableDisableColumns(true);
                        TAXCalcuation(DeductTypeVal);
                    }
                }
            }
        }

        private string FetchLedgerName(int ExpLedgerId)
        {
            string ExpLedgerName = string.Empty;
            using (TDSBookingSystem BookingSystem = new TDSBookingSystem())
            {
                BookingSystem.ExpenseLedgerId = ExpLedgerId;
                ExpLedgerName = BookingSystem.FetchLedgerName();
            }
            return ExpLedgerName;
        }

        private DataTable FilterTransSummary()
        {
            DataTable dtTranSummaryTable = new DataTable();
            try
            {
                using (TDSBookingSystem BookingSystem = new TDSBookingSystem())
                {
                    dtTranSummaryTable = ucTransactionSummary.UpdateTDSSummary;
                    if (dtTranSummaryTable != null)
                    {
                        DataView dvTranSummary = new DataView(dtTranSummaryTable);
                        dvTranSummary.RowFilter = BookingSystem.AppSchema.LedgerBalance.TRANS_MODEColumn.ColumnName + " = " + "'DR'";
                        dtTranSummaryTable = dvTranSummary.ToTable();
                    }

                    if (dtTranSummaryTable != null && dtTranSummaryTable.Rows.Count > 0)
                    {
                        if (this.UtilityMember.NumberSet.ToDouble(dtTranSummaryTable.Rows[0][BookingSystem.AppSchema.TDSBooking.AMOUNTColumn.ColumnName].ToString()).Equals(0))
                        {
                            foreach (DataRow dr in dtTranSummaryTable.Rows)
                            {
                                if (dr[BookingSystem.AppSchema.LedgerBalance.TRANS_MODEColumn.ColumnName].ToString().Equals(TransactionMode.DR.ToString()))
                                {
                                    dr.SetField(BookingSystem.AppSchema.TDSBooking.AMOUNTColumn.ColumnName, ExpenseLedgerAmount);
                                }
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
            return dtTranSummaryTable;
        }

        private void FocusTDSTransactionGrid()
        {
            gcTDSJournal.Select();
            gvTDSJournal.MoveLast();
            gvTDSJournal.FocusedColumn = colNatureofPayment;
            gvTDSJournal.ShowEditor();
        }

        private void SetFocusToGridControl()
        {
            gcTDSJournal.Select();
            gvTDSJournal.MoveFirst();
            gvTDSJournal.FocusedColumn = colNatureofPayment;
            gvTDSJournal.ShowEditor();
            gvTDSJournal.Focus();
        }

        private void AssignDeducteeType()
        {
            using (DeducteeTypeSystem deducteeTypeSystem = new DeducteeTypeSystem())
            {
                deducteeTypeSystem.PartyLedgerId = PartyLedgerId;
                resultArgs = deducteeTypeSystem.FetchDeductTypesByPartyLedger();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    DeducteeTypeId = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["ID"].ToString());
                    lblDeducteeTypeValue.Text = resultArgs.DataSource.Table.Rows[0][deducteeTypeSystem.AppSchema.NatureofPayment.NAMEColumn.ColumnName].ToString();
                }
            }
        }

        private void AssignTDSMasterDetails()
        {
            try
            {
                if (dtTDSBookingDetail != null && dtTDSBookingDetail.Rows.Count > 0)
                {
                    txtNetTDSAmount.Text = this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(dtTDSBookingDetail.Compute("SUM(TDS_AMOUNT)", "").ToString()));
                    txtNetPayableAmount.Text = this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(dtTDSBookingDetail.Compute("SUM(PARTY_AMOUNT)", "").ToString()));
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void AssignDefaultNatureofPayments()
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    ledgerSystem.LedgerId = this.ExpenseLedgerId;
                    resultArgs = ledgerSystem.FetchNOPforTDSExpenseLedger();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        dtAssignMappedNatureOfPayments.Rows.Add(dtAssignMappedNatureOfPayments.NewRow());
                        int NatureofPaymentId = resultArgs.DataSource.Table.Rows[0]["NATURE_OF_PAYMENT_ID"] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["NATURE_OF_PAYMENT_ID"].ToString()) : 0;
                        foreach (DataRow dr in dtAssignMappedNatureOfPayments.Rows)
                        {
                            dr.SetField("NATURE_PAY_ID", NatureofPaymentId);
                            dr.SetField("ASSESS_AMOUNT", ExpenseLedgerAmount);
                        }
                    }
                    else
                    {
                        dtAssignMappedNatureOfPayments.Rows.Add(dtAssignMappedNatureOfPayments.NewRow());
                    }
                    gcTDSJournal.DataSource = dtAssignMappedNatureOfPayments;
                    gcTDSJournal.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        /// <summary>
        ///  Assign TDS Booking Values based  on the Selected Booking Id,Voucher Id
        /// </summary>
        private void AssignTDSBookingDetails()
        {
            try
            {
                using (TDSBookingSystem tdsBookingSystem = new TDSBookingSystem(BookingId, VoucherId, DeducteeTypeId, VoucherDate))
                {
                    gcTDSJournal.DataSource = tdsBookingSystem.dtEditBookingTrans;
                    CalNetTDSAmount();
                    if ((gcTDSJournal.DataSource as DataTable) != null && (gcTDSJournal.DataSource as DataTable).Rows.Count > 0)
                    {
                        double ExpAmount = this.UtilityMember.NumberSet.ToDouble((gcTDSJournal.DataSource as DataTable).Compute("SUM(ASSESS_AMOUNT)", "").ToString());
                        if (ExpenseLedgerAmount > 0)
                        {
                            if (ExpenseLedgerAmount > ExpAmount || ExpenseLedgerAmount < ExpAmount)
                            {
                                isAmountMismatch = true;
                                lblTDSAmount.Text = this.UtilityMember.NumberSet.ToNumber(ExpenseLedgerAmount) + " " + TransactionMode.CR.ToString();
                            }
                            else
                            {
                                ExpenseLedgerAmount = this.UtilityMember.NumberSet.ToDouble((gcTDSJournal.DataSource as DataTable).Compute("SUM(ASSESS_AMOUNT)", "").ToString());
                                lblTDSAmount.Text = this.UtilityMember.NumberSet.ToNumber(ExpenseLedgerAmount) + " " + TransactionMode.CR.ToString();
                            }
                        }
                        else
                        {
                            ExpenseLedgerAmount = this.UtilityMember.NumberSet.ToDouble((gcTDSJournal.DataSource as DataTable).Compute("SUM(ASSESS_AMOUNT)", "").ToString());
                            lblTDSAmount.Text = this.UtilityMember.NumberSet.ToNumber(ExpenseLedgerAmount) + " " + TransactionMode.CR.ToString();
                        }
                        ExpenseLedgerId = tdsBookingSystem.ExpenseLedgerId;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        public int FetchBookingId()
        {
            if (VoucherId > 0)
            {
                using (TDSBookingSystem BookingSystem = new TDSBookingSystem())
                {
                    BookingSystem.VoucherId = VoucherId;
                    BookingId = BookingSystem.FetchBookingId();
                }
            }
            return BookingId;
        }

        private string GetLedgerName(int PartyLedgerId)
        {
            string LedgerName = string.Empty;
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                LedgerName = ledgerSystem.GetLegerName(PartyLedgerId);
            }
            return LedgerName;
        }
        #endregion

        #region TDS Events
        private void rbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvTDSJournal.FocusedRowHandle >= 0)
                {
                    gvTDSJournal.DeleteRow(gvTDSJournal.FocusedRowHandle);
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void gvTDSJournal_ColumnChanged(object sender, EventArgs e)
        {
            if (gvTDSJournal.FocusedColumn == colDeductTypes)
            {
                int DeductNow = gvTDSJournal.GetFocusedRowCellValue(colDeductTypes) != null ? this.UtilityMember.NumberSet.ToInteger(gvTDSJournal.GetFocusedRowCellValue(colDeductTypes).ToString()) : 0;
                if (DeductNow.Equals(0) && !isTaxDeductable)
                {
                    if (VoucherId.Equals(0))
                    {
                        if (this.ShowConfirmationMessage("You cannot deduct Tax for this,since the amount is less than the exemption limit." + Environment.NewLine + Environment.NewLine + "                                Do you want to deduct Tax now?        ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
                        {
                            gvTDSJournal.SetFocusedRowCellValue(colDeductTypes, 1);
                            colTDSLedger.OptionsColumn.AllowEdit = false;
                            CalculateTDSNow();
                        }
                        else
                        {
                            gvTDSJournal.SetFocusedRowCellValue(colDeductTypes, 0);
                            colTDSLedger.OptionsColumn.AllowEdit = true;
                            CalculateTDSNow();
                        }
                    }
                }
                else
                {
                    CalculateTDSNow();
                }
            }
        }
        #endregion
    }
}