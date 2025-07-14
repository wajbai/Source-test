using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.Model.TDS;
using Bosco.Model.Transaction;
using ACPP.Modules.Transaction;

namespace ACPP.Modules.TDS
{
    public partial class frmTDSPartyTrans : frmFinanceBaseAdd
    {
        #region Decelaration
        ResultArgs resultArgs = null;
        DataTable dtDebitSummary = new DataTable();
        DataTable dtTransSummary = new DataTable();


        #endregion

        #region Properties
        private DataTable dtTDSPendingSelected { get; set; }
        private int ProjectId { get; set; }
        private string ProjectName { get; set; }
        private int TDSLedgerId { get; set; }
        private string TDSLedgerName { get; set; }
        public int VoucherId { get; set; }
        public int BookingId { get; set; }
        public DateTime dtVoucherDate { get; set; }
        private double TDSAmount { get; set; }
        public decimal TDSNetAmount { get; set; }
        public int DeducteeTypeId { get; set; }
        private string DeductBookingId { get; set; }
        private int NatureofPaymentId { get; set; }
        private DataTable GetPendingTDS
        {
            get { return gcPartyTrans.DataSource as DataTable; }
        }
        public int PartyLedgerId { get; set; }
        private int PrevPartyLedgerId { get; set; }
        private int partyLedgerId = 0;
        public int PartyId
        {
            get { return glkpPartyLedgers.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpPartyLedgers.EditValue.ToString()) : 0; }
            set { partyLedgerId = value; }
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
        private string partyLedgerValue = string.Empty;
        private string PartyLedgerName
        {
            get { return glkpPartyLedgers.Text; }
        }

        public decimal TDSNetPayableAmount
        {
            get { return !string.IsNullOrEmpty(txtNetAmount.Text) ? this.UtilityMember.NumberSet.ToDecimal(txtNetAmount.Text) : 0; }
        }

        public DataTable dtTDSTransSummary { get; set; }
        public DataTable dtTDSTransInfo { get; set; }
        public DataTable dtTDSMasterDetails { get; set; }
        public DataTable dtTDSBookingDetail { get; set; }
        private int TaxLedgerId { get; set; }
        public DataTable dtPartyTransAtJournal { get; set; }
        public DataView dtUcTransSummary { get; set; }
        public string Narration { get; set; }

        private bool DeductedTaxAtJournal = false;
        #endregion

        #region Constructor
        public frmTDSPartyTrans()
        {
            InitializeComponent();
        }

        public frmTDSPartyTrans(DateTime dtVoucherDate, int VoucherId, int ProjectId, int LedgerId, string LedgerName, double TDSAmount)
            : this()
        {
            this.dtVoucherDate = dtVoucherDate;
            this.VoucherId = VoucherId;
            this.ProjectId = ProjectId;
            this.TDSLedgerId = LedgerId;
            this.TDSLedgerName = LedgerName;
            this.TDSAmount = TDSAmount;
        }

        public frmTDSPartyTrans(DateTime dtVoucherDate, int ProjectId, int PartyLedgerId, int NatureofPaymentId, int TDSLedgerId, bool DeductTaxAtJournal)
            : this()
        {
            this.dtVoucherDate = dtVoucherDate;
            this.ProjectId = ProjectId;
            this.PartyLedgerId = PartyLedgerId;
            this.NatureofPaymentId = NatureofPaymentId;
            this.TDSLedgerId = TDSLedgerId;
            this.DeductedTaxAtJournal = DeductTaxAtJournal;
            EnableControls(false);

        }
        #endregion

        #region Events
        private void frmTDSPartyTrans_Load(object sender, EventArgs e)
        {
            SetDefaults();
            FetchDefaults();
            if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic)
            {
                LoadVoucherNo();
            }
        }

        private void glkpPartyLedgers_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpPartyLedgers);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (gcPartyTrans.DataSource != null && (gcPartyTrans.DataSource as DataTable).Rows.Count > 0)
            {
                if (ValidateTDSDeduction(true))
                {
                    if (!DeductedTaxAtJournal)
                    {
                        using (VoucherTransactionSystem VoucherSystem = new VoucherTransactionSystem())
                        {
                            VoucherSystem.VoucherDate = dteDate.DateTime;
                            VoucherSystem.ProjectId = glkpProject.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
                            if (VoucherId == 0)
                            {
                                if (TransVoucherMethod == (int)TransactionVoucherMethod.Manual) { VoucherSystem.VoucherNo = txtVoucherNo.Text; }
                                else { VoucherSystem.TransVoucherMethod = TransVoucherMethod; }
                            }
                            else { VoucherSystem.VoucherNo = txtVoucherNo.Text; }

                            VoucherSystem.PartyLedgerId = glkpPartyLedgers.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkpPartyLedgers.EditValue.ToString()) : 0;
                            VoucherSystem.VoucherType = VoucherSubTypes.JN.ToString();
                            VoucherSystem.VoucherSubType = ledgerSubType.GN.ToString();
                            VoucherSystem.Narration = metxtNarration.Text;
                            VoucherSystem.Status = (int)YesNo.Yes;
                            VoucherSystem.CreatedBy = this.UtilityMember.NumberSet.ToInteger(this.LoginUser.LoginUserId);
                            VoucherSystem.ModifiedBy = this.UtilityMember.NumberSet.ToInteger(this.LoginUser.LoginUserId);
                            VoucherSystem.CreatedOn = dteDate.DateTime;
                            VoucherSystem.ModifiedOn = dteDate.DateTime;
                            VoucherSystem.dtTDSDeductionLater = GetPendingTDS;
                            this.UIAppSetting.TransInfo = UcTDSSummary.UpdateTDSSummary.DefaultView;
                            resultArgs = VoucherSystem.SaveTransactions();
                            if (resultArgs != null && resultArgs.Success)
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_SAVE));
                                ClearValues();
                            }
                        }
                    }
                    else
                    {
                        dtPartyTransAtJournal = GetPendingTDS;
                        dtUcTransSummary = UcTDSSummary.UpdateTDSSummary.DefaultView;
                        Narration = metxtNarration.Text;
                        this.Close();
                    }
                }
            }
            else
            {
                ShowMessageBox(GetMessage(MessageCatalog.TDS.TDSTrasaction.TDS_PENDING_TRANS_CONFIRMATION));
            }
        }

        private void rbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvPartyTrans.RowCount > 0)
                {
                    gvPartyTrans.DeleteRow(gvPartyTrans.FocusedRowHandle);
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void glkpProject_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpProject);
        }

        private void glkpNOP_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpNOP);
        }

        private void dteDate_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForDateTimeEdit(dteDate);
        }

        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            FetchMappedLedgers(glkpPartyLedgers, TDSLedgerGroup.SundryCreditors);
            FetchVoucherMethod();
        }

        private void txtVoucherNo_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtVoucherNo);
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (ValidateTDSDeduction())
            {
                GetPendingDetails();
            }
        }
        #endregion

        #region Methods
        private void LoadTaxLedger()
        {
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                ledgerSystem.ProjectId = glkpProject.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
                resultArgs = ledgerSystem.FetchDutiesTaxLedgers();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpTDSLedgers, resultArgs.DataSource.Table, ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                }
            }
        }

        private void FetchDefaults()
        {
            //LoadLedgers(rglkpTDSLedgers, TDSLedgerGroup.DutiesAndTax);
            FetchNatureofPayments();
            FetchProjects();
        }

        private void LoadVoucherNo()
        {
            string vType = string.Empty;
            string pId = string.Empty;
            using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
            {
                voucherTransaction.VoucherType = "JN";
                voucherTransaction.ProjectId = glkpProject.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
                voucherTransaction.VoucherDate = this.UtilityMember.DateSet.ToDate(dteDate.Text, false);
                txtVoucherNo.Text = voucherTransaction.TempVoucherNo();
            }
        }

        private void SetDefaults()
        {
            dtTransSummary = UcTDSSummary.ConstructTable();
            dteDate.DateTime = this.UtilityMember.DateSet.ToDate(this.AppSetting.RecentVoucherDate, false);
            if (DeductedTaxAtJournal)
            {
                //btnSave.Text = "&Ok";
                btnSave.Text = this.GetMessage(MessageCatalog.TDS.TDSPayment.TDS_OK_BUTTON_CAPTION);
                //btnClose.Text = "&Cancel";
                btnClose.Text = this.GetMessage(MessageCatalog.TDS.TDSPayment.TDS_CANCEL_BUTTON_CAPTION);
            }
        }

        /// <summary>
        /// Fetch Nature of Payments and TDS Ledgers
        /// </summary>
        /// <param name="rglkpEdit">Nature of Payments and TDS Ledgers</param>
        /// <param name="tdsLedgerGroup">Ledger Group based on Expense Ledgers ,Party Ledgers </param>
        private void LoadLedgers(DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkpEdit, TDSLedgerGroup tdsLedgerGroup)
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    ledgerSystem.ProjectId = glkpProject.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
                    resultArgs = ledgerSystem.FetchDutiesTaxLedgers();
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        //DataView dvLedgers = resultArgs.DataSource.Table.DefaultView;
                        //dvLedgers.RowFilter = "GROUP_ID IN(" + (int)tdsLedgerGroup + ") AND IS_TDS_LEDGER=1";
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpEdit, resultArgs.DataSource.Table, ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                        //dvLedgers.RowFilter = "";
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
        /// Fetch All the default ledgers 
        /// Filter (Expenses Ledger ,Duties and Tax Ledgers based on the (TDSLEdgerGroup)).
        /// </summary>
        /// <param name="glkpLookUpEdit">Expense Ledgers ,Party Ledgers   </param>
        /// <param name="tdsLedgerGroup">Ledger Group based on Expense Ledgers ,Party Ledgers  </param>
        /// <returns></returns>
        private void FetchMappedLedgers(GridLookUpEdit glkpLookUpEdit, TDSLedgerGroup tdsLedgerGroup)
        {
            using (MappingSystem mappingSystem = new MappingSystem())
            {
                mappingSystem.ProjectId = glkpProject.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
                resultArgs = mappingSystem.FetchMappedLedgers();
                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > (int)YesNo.No)
                {
                    DataView dvPartyLedgers = resultArgs.DataSource.Table.DefaultView;
                    dvPartyLedgers.RowFilter = mappingSystem.AppSchema.LedgerGroup.GROUP_IDColumn.ColumnName + " IN(" + (int)tdsLedgerGroup + ") AND " + mappingSystem.AppSchema.Ledger.IS_TDS_LEDGERColumn.ColumnName + "=" + (int)YesNo.Yes + "";
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpLookUpEdit, dvPartyLedgers.ToTable(), mappingSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, mappingSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                    dvPartyLedgers.RowFilter = "";
                    if (glkpLookUpEdit.EditValue != null)
                        PrevPartyLedgerId = glkpLookUpEdit.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpLookUpEdit.EditValue.ToString()) : 0;
                    if (this.PartyLedgerId > 0)
                    {
                        glkpPartyLedgers.EditValue = this.PartyLedgerId;
                        GetPendingDetails();
                    }
                }
            }
        }

        private void UpdateTransSummary()
        {
            try
            {
                DataTable dtSummary = gcPartyTrans.DataSource as DataTable;
                using (TDSBookingSystem BookingSystem = new TDSBookingSystem())
                {
                    if (dtSummary != null && dtSummary.Rows.Count > 0)
                    {
                        double PartyLedgerAmount = this.UtilityMember.NumberSet.ToDouble(dtSummary.Compute("SUM(BALANCE)", "").ToString());
                        if (UcTDSSummary.UpdateTDSSummary == null || UcTDSSummary.UpdateTDSSummary.Rows.Count == 0)
                        {
                            dtTransSummary.Rows.Add(PartyId, PartyLedgerName, PartyId, PartyLedgerAmount, 0, PartyLedgerAmount, TransactionMode.DR.ToString());
                            TransSummary(dtSummary, BookingSystem);
                        }
                        else
                        {
                            // dtTransSummary.Rows.Add(PartyId, PartyLedgerName, PartyLedgerAmount, TransactionMode.DR.ToString());
                            if (dtTDSTransSummary != null && dtTDSTransSummary.Rows.Count > 0)
                            {
                                dtTransSummary = dtTDSTransSummary;
                            }
                            FilterTransSummary();
                            TransSummary(dtSummary, BookingSystem);
                        }
                        UcTDSSummary.UpdateTDSSummary = dtTransSummary;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void TransSummary(DataTable dtSummary, TDSBookingSystem BookingSystem)
        {
            foreach (DataRow drTransSummary in dtSummary.Rows)
            {
                int LedgerId = drTransSummary[BookingSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(drTransSummary[BookingSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()) : 0;
                double TAXAmount = drTransSummary["BALANCE"] != DBNull.Value ? this.UtilityMember.NumberSet.ToDouble(drTransSummary["BALANCE"].ToString()) : 0;
                if (LedgerId > 0)
                {
                    BookingSystem.ExpenseLedgerId = LedgerId;
                    string ExpLedgerName = BookingSystem.FetchLedgerName();
                    if (LedgerId > 0 && TAXAmount > 0 && !string.IsNullOrEmpty(ExpLedgerName))
                        dtTransSummary.Rows.Add(LedgerId, ExpLedgerName, LedgerId, 0, TAXAmount, TAXAmount, TransactionMode.CR.ToString());
                }
            }
        }

        private void FilterTransSummary()
        {
            try
            {
                if (dtTransSummary != null && dtTransSummary.Rows.Count > 0)
                {
                    DataView dvTransSummary = dtTransSummary.DefaultView;
                    dvTransSummary.RowFilter = "TRANS_MODE='DR'";
                    dtTransSummary = dvTransSummary.ToTable();
                    dvTransSummary.RowFilter = "";
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void ClearValues()
        {
            txtPayableAmount.Text = txtNetAmount.Text = string.Empty;
            UcTDSSummary.UpdateTDSSummary.Clear();
            gcPartyTrans.DataSource = null;
            glkpNOP.EditValue = glkpPartyLedgers.EditValue = glkpProject.EditValue = null;
            txtVoucherNo.Text = string.Empty;
            lblDeduteeValue.Text = " ";
            dteDate.Text = string.Empty;
            SetDefaults();
            FetchDefaults();
        }

        private void AssignDeducteeType()
        {
            using (DeducteeTypeSystem deducteeTypeSystem = new DeducteeTypeSystem())
            {
                deducteeTypeSystem.PartyLedgerId = glkpPartyLedgers.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpPartyLedgers.EditValue.ToString()) : 0;
                resultArgs = deducteeTypeSystem.FetchDeductTypesByPartyLedger();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    DeducteeTypeId = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["ID"].ToString());
                    lblDeduteeValue.Text = resultArgs.DataSource.Table.Rows[0][deducteeTypeSystem.AppSchema.NatureofPayment.NAMEColumn.ColumnName].ToString();
                }
            }
        }

        /// <summary>
        ///  Assign TDS Booking Values based  on the Selected Booking Id,Voucher Id
        /// </summary>
        private void AssignTDSDeductionDetails()
        {
            try
            {
                using (TDSDeductionSystem DeductionSystem = new TDSDeductionSystem())
                {
                    DeductionSystem.DeducteeTypeId = DeducteeTypeId;
                    DeductionSystem.ProjectId = ProjectId;
                    DeductionSystem.BookingId = BookingId;
                    DeductionSystem.ApplicableFrom = dtVoucherDate;

                    DataTable dtTDSDeduction = DeductionSystem.FetchByBooking();
                    gcPartyTrans.DataSource = DefineDefaultTDSLedger(dtTDSDeduction);

                    glkpPartyLedgers.EditValue = dtTDSDeduction.Rows[0][DeductionSystem.AppSchema.TDSBooking.PARTY_LEDGER_IDColumn.ColumnName] != DBNull.Value ?
                     this.UtilityMember.NumberSet.ToInteger(dtTDSDeduction.Rows[0][DeductionSystem.AppSchema.TDSBooking.PARTY_LEDGER_IDColumn.ColumnName].ToString()) : 0;
                    gcPartyTrans.RefreshDataSource();
                    UpdateTransSummary();
                    txtNetAmount.Text = txtPayableAmount.Text = dtTDSDeduction.Compute("SUM(BALANCE)", "") != DBNull.Value ?
                                       this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(dtTDSDeduction.Compute("SUM(BALANCE)", "").ToString())) : string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void EnableControls(bool isTrue)
        {
            dteDate.Enabled = glkpNOP.Enabled = glkpPartyLedgers.Enabled = glkpProject.Enabled = isTrue;
            gvPartyTrans.Focus();
            gvPartyTrans.FocusedColumn = colTDSLedger;
        }
        #endregion

        #region Transaction Events

        private void glkpPartyLedgers_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                AssignDeducteeType();
                //if (dtTDSTransSummary == null && dtTDSBookingDetail == null)
                //{
                //}
                //else
                if (PrevPartyLedgerId != PartyId)
                {
                    PrevPartyLedgerId = PartyId;
                }
                PartyLedgerId = glkpPartyLedgers.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpPartyLedgers.EditValue.ToString()) : 0;
                if (!PartyLedgerId.Equals(PrevPartyLedgerId))
                {
                    DataTable dtTable = UcTDSSummary.UpdateTDSSummary;
                    DataTable dtTranSummary = gcPartyTrans.DataSource as DataTable;
                    if (dtTranSummary != null && dtTranSummary.Rows.Count > 0 && dtTable != null && PrevPartyLedgerId > 0)
                    {
                        double TDSAmount = this.UtilityMember.NumberSet.ToDouble(dtTranSummary.Compute("SUM(BALANCE)", "").ToString());
                        for (int i = 0; i < dtTable.Rows.Count; i++)
                        {
                            if (dtTable.Rows[i]["TRANS_MODE"].ToString().Equals(TransactionMode.DR.ToString()))
                            {
                                dtTable.Rows.RemoveAt(i);
                                dtTable.AcceptChanges();
                                break;
                            }
                        }
                        dtTable.Rows.Add(PartyLedgerId, PartyLedgerName, TDSAmount, TransactionMode.DR.ToString());
                        PrevPartyLedgerId = PartyLedgerId;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void gcPartyTrans_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode.Equals(Keys.Tab) || e.KeyCode.Equals(Keys.Enter)) && gvPartyTrans.FocusedColumn == colTDSLedger)
                {
                    int TDSLedgerID = gvPartyTrans.GetFocusedRowCellValue(colTDSLedger) != null ? this.UtilityMember.NumberSet.ToInteger(gvPartyTrans.GetFocusedRowCellValue(colTDSLedger).ToString()) : 0;
                    if (gvPartyTrans.IsLastRow)
                    {
                        e.SuppressKeyPress = true;
                        e.Handled = true;
                        metxtNarration.Focus();
                        metxtNarration.Select();
                    }
                    UpdateTransSummary();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void uctdsPayments_GetPendingDetails(object sender, EventArgs e)
        {
            GetPendingDetails();
        }

        private void GetPendingDetails()
        {
            using (DeducteeTaxSystem deduteeTaxSystem = new DeducteeTaxSystem())
            {
                LoadTaxLedger();
                deduteeTaxSystem.PartyPaymentId = PartyId;
                deduteeTaxSystem.DeducteeTypeId = this.DeducteeTypeId;
                deduteeTaxSystem.ProjectId = glkpProject.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
                deduteeTaxSystem.ApplicableFrom = dteDate.DateTime;
                deduteeTaxSystem.NaturePaymentId = glkpNOP.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpNOP.EditValue.ToString()) : 0;
                resultArgs = deduteeTaxSystem.FetchPendingTransaction();
                if (resultArgs != null && resultArgs.Success)
                {
                    DataTable dtPendingTransaction = resultArgs.DataSource.Table;
                    if (dtPendingTransaction != null && dtPendingTransaction.Rows.Count > 0)
                    {
                        dtTDSPendingSelected = DefineDefaultTDSLedger(dtPendingTransaction);
                        if (VoucherId > 0 && BookingId > 0)
                        {

                        }
                        else
                        {
                            gcPartyTrans.DataSource = dtTDSPendingSelected;
                        }
                        if (gcPartyTrans.DataSource as DataTable != null && (gcPartyTrans.DataSource as DataTable).Rows.Count > 0)
                            txtNetAmount.Text = txtPayableAmount.Text = (gcPartyTrans.DataSource as DataTable).Compute("SUM(BALANCE)", "") != DBNull.Value ?
                                this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble((gcPartyTrans.DataSource as DataTable).Compute("SUM(BALANCE)", "").ToString())) : string.Empty;

                        gvPartyTrans.Focus();
                        gvPartyTrans.PostEditor();
                        gvPartyTrans.FocusedColumn = colTDSLedger;
                        UcTDSSummary.UpdateTDSSummary.Clear();
                        UpdateTransSummary();
                    }
                    else
                    {
                        ShowMessageBox(GetMessage(MessageCatalog.TDS.TDSTrasaction.TDS_PENDING_TRANS_CONFIRMATION));
                        if (VoucherId == 0 && BookingId == 0)
                        {
                            gcPartyTrans.DataSource = null;
                            UcTDSSummary.UpdateTDSSummary.Clear();
                        }
                    }
                }
            }
        }

        private DataTable DefineDefaultTDSLedger(DataTable dtPendingTDSLedger)
        {
            try
            {
                if (dtPendingTDSLedger != null && dtPendingTDSLedger.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtPendingTDSLedger.Rows)
                    {
                        dr.SetField("LEDGER_ID", this.TDSLedgerId);
                        dr.SetField("VALUE", (int)YesNo.Yes);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
            return dtPendingTDSLedger;
        }

        private void FetchProjects()
        {
            using (ProjectSystem projectSystem = new ProjectSystem())
            {
                projectSystem.ProjectClosedDate = dteDate.Text;
                resultArgs = projectSystem.FetchProjects();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, projectSystem.AppSchema.Project.PROJECTColumn.ColumnName, projectSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                    glkpProject.EditValue = (glkpProject.Properties.GetDisplayValueByKeyValue(this.AppSetting.UserProjectId) != null ? this.AppSetting.UserProjectId :glkpProject.Properties.GetKeyValue(0));

                    glkpProject.EditValue = this.UtilityMember.NumberSet.ToInteger(this.AppSetting.UserProjectId);
                    if (this.ProjectId > 0)
                    {
                        glkpProject.EditValue = this.ProjectId;
                    }
                }
            }
        }

        private void FetchNatureofPayments()
        {
            using (Bosco.Model.TDS.NatureofPaymentsSystem NaturePaySystem = new NatureofPaymentsSystem())
            {
                resultArgs = NaturePaySystem.FetchNatureofPayments();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpNOP, resultArgs.DataSource.Table, NaturePaySystem.AppSchema.NatureofPayment.NAMEColumn.ColumnName, NaturePaySystem.AppSchema.NatureofPayment.NATURE_PAY_IDColumn.ColumnName);
                    if (this.NatureofPaymentId > 0)
                    {
                        glkpNOP.EditValue = this.NatureofPaymentId;
                    }
                }
            }
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
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
            return dtVoucherTrans;
        }

        private void FetchVoucherMethod()
        {
            using (ProjectSystem projectSystem = new ProjectSystem())
            {
                resultArgs = projectSystem.FetchVoucherByProjectId(glkpProject.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0, ((int)DefaultVoucherTypes.Journal).ToString(), (int)DefaultVoucherTypes.Journal);
                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    TransVoucherMethod = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][projectSystem.AppSchema.Voucher.VOUCHER_METHODColumn.ColumnName].ToString());
                    if (TransVoucherMethod == 1) { txtVoucherNo.Enabled = false; } else { txtVoucherNo.Enabled = true; }
                }
                else
                {
                    TransVoucherMethod = 2;
                }
            }
        }

        private bool ValidateTDSDeduction(bool isValidateTaxLedger = false)
        {
            bool isTDSDeduction = true;
            if (dteDate.DateTime == DateTime.MinValue || string.IsNullOrEmpty(dteDate.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_TRANSACTION_DATE));
                this.SetBorderColorForDateTimeEdit(dteDate);
                dteDate.Focus();
                isTDSDeduction = false;
            }
            else if (glkpProject.EditValue == null || this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) == 0)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Project.PROJECT_NAME_EMPTY));
                this.SetBorderColorForGridLookUpEdit(glkpProject);
                glkpProject.Focus();
                isTDSDeduction = false;
            }
            else if (TransVoucherMethod == (int)TransactionVoucherMethod.Manual && string.IsNullOrEmpty(txtVoucherNo.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_NUMBER_EMPTY));
                this.SetBorderColor(txtVoucherNo);
                txtVoucherNo.Focus();
                isTDSDeduction = false;
            }
            else if (glkpPartyLedgers.EditValue == null || this.UtilityMember.NumberSet.ToInteger(glkpPartyLedgers.EditValue.ToString()) == 0)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.TDS.NatureofPayments.PARTY_LEDGER_EMPTY));
                this.SetBorderColorForGridLookUpEdit(glkpPartyLedgers);
                glkpPartyLedgers.Focus();
                isTDSDeduction = false;
            }
            else if (glkpNOP.EditValue == null || this.UtilityMember.NumberSet.ToInteger(glkpNOP.EditValue.ToString()) == 0)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.TDS.NatureofPayments.NATURE_OF_PAYMENT_EMPTY));
                this.SetBorderColorForGridLookUpEdit(glkpNOP);
                glkpNOP.Focus();
                isTDSDeduction = false;
            }
            else if (gcPartyTrans.DataSource != null && ((DataTable)gcPartyTrans.DataSource).Rows.Count > 0)
            {
                if (isValidateTaxLedger)
                {
                    DataRow[] HasRows = ((DataTable)gcPartyTrans.DataSource).Select("LEDGER_ID=0");
                    if (HasRows.Count() > 0)
                    {
                        //this.ShowMessageBox("TDS Ledger is empty");
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.TDS.TDSPayment.TDS_LEDGER_EMTPY));
                        gvPartyTrans.FocusedColumn = colTDSLedger;
                        gvPartyTrans.Focus();
                        gvPartyTrans.ShowEditor();
                        isTDSDeduction = false;
                    }
                    UpdateTransSummary();
                }
            }
            return isTDSDeduction;
        }

        private void rglkpTDSLedgers_Leave(object sender, EventArgs e)
        {
            UpdateTransSummary();
        }
        #endregion

        private void metxtNarration_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //if (e.KeyData.Equals(Keys.ShiftKey) | e.KeyData.Equals(Keys.Shift))
            //if (e.Shift)
            //{
            //    gvPartyTrans.MoveLast();
            //    gvPartyTrans.FocusedColumn = colTDSLedger;
            //    gvPartyTrans.Focus();
            //    gvPartyTrans.ShowEditor();
            //}
        }

        private void metxtNarration_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.Shift && e.KeyCode == Keys.Tab)
            //{
            //    gvPartyTrans.MoveLast();
            //    gvPartyTrans.FocusedColumn = colTDSLedger;
            //    gvPartyTrans.Focus();
            //    gvPartyTrans.ShowEditor();
            //    e.SuppressKeyPress = true;
            //}
        }

        private void gvPartyTrans_KeyUp(object sender, KeyEventArgs e)
        {
            //if (gvPartyTrans.IsFirstRow && e.Shift && e.KeyCode.Equals(Keys.Tab))
            //{
            //    glkpNOP.Focus();
            //    e.SuppressKeyPress = true;
            //}
        }

        private void gvPartyTrans_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (gvPartyTrans.IsFirstRow && e.KeyChar.Equals(Keys.Tab))
            //{
            //    glkpNOP.Focus();
            //}
        }

        private void metxtNarration_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar.Equals(Keys.Shift) || e.KeyChar.Equals(Keys.ShiftKey) && e.KeyChar.Equals(Keys.Tab))
            //{
            //    gvPartyTrans.MoveLast();
            //    gvPartyTrans.FocusedColumn = colTDSLedger;
            //    gvPartyTrans.Focus();
            //    gvPartyTrans.ShowEditor();
            //}
        }

        private void frmTDSPartyTrans_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F3)
                {
                    frmDatePicker DatePicker = new frmDatePicker(dteDate.DateTime, DatePickerType.VoucherDate);
                    DatePicker.ShowDialog();
                    dteDate.DateTime = AppSetting.VoucherDate;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex);
            }
        }

        private void dteDate_EditValueChanged(object sender, EventArgs e)
        {
            //On 12/07/2018, For closed Projects----
            FetchProjects();
            //-------------------------------------
        }
    }
}