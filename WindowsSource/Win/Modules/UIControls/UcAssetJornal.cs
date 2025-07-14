using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using Bosco.Model.UIModel;
using AcMEDSync.Model;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid;
using Bosco.Model;
using Bosco.Model.Transaction;
using DevExpress.XtraGrid.Views.Grid;
using Bosco.DAO.Schema;

namespace ACPP.Modules.UIControls
{
    public partial class UcAssetJournal : UserControl
    {

        #region Constructor

        public UcAssetJournal()
        {
            InitializeComponent();
            RealColumnEditCashTransAmount();
        }

        #endregion

        #region Declaration

        private ResultArgs resultArgs = null;
        CommonMember UtilityMember = new CommonMember();
        SettingProperty Settingproperty = new SettingProperty();
        MessageCatalog messageCatolough = new MessageCatalog();
        AppSchemaSet.ApplicationSchemaSet AppSchema = new AppSchemaSet.ApplicationSchemaSet();

        #endregion

        #region Properties

        bool isMouseClicked = false;
        private bool EnableMultiRow = false;
        string dr = "DR";
        string cr = "CR";
        DataTable dtLedgers = null;

        public int CashLedgerId
        {
            get
            {
                int cashLedgerId = 0;
                cashLedgerId = gvBank.GetRowCellValue(gvBank.FocusedRowHandle, colCashLedgerId) != null ? this.UtilityMember.NumberSet.ToInteger(gvBank.GetRowCellValue(gvBank.FocusedRowHandle, colCashLedgerId).ToString()) : 0;
                return cashLedgerId;
            }
            //set { CashLedgerId = value; }
        }

        public int GainLossLedgerId
        {
            get
            {
                int gainlossLedgerId = 0;
                gainlossLedgerId = gvBank.GetRowCellValue(gvBank.FocusedRowHandle + 1, colCashLedgerId) != null ? this.UtilityMember.NumberSet.ToInteger(gvBank.GetRowCellValue(gvBank.FocusedRowHandle + 1, colCashLedgerId).ToString()) : 0;
                return gainlossLedgerId;
            }
            //set { CashLedgerId = value; }
        }
        double amount = 0.0;
        public double PurchaseTransSummary
        {
            get { return amount; }
            set { amount = value; }
        }

        public bool ShowDeleteColumn
        {
            set { colDeleteCashBank.Visible = value; }
            get { return colDeleteCashBank.Visible; }
        }

        public bool EnableCashBankGrid
        {
            get { return gcBank.Enabled; }
            set { gcBank.Enabled = value; }
        }

        public AssetInOut Flag { get; set; }

        private int voucherId = 0;
        private int VoucherId
        {
            get { return voucherId; }
            set { voucherId = value; }
        }


        public DataTable dt = null;
        public DataTable DtCashBank
        {
            get
            {
                dt = gcBank.DataSource as DataTable;
                return dt;
            }
        }

        public int projectId = 0;
        public int ProjectId
        {
            set { projectId = value; }
            get { return projectId; }
        }

        public int currencycountryid = 0;
        public int CurrencyCountryId
        {
            set { currencycountryid = value; }
            get { return currencycountryid; }
        }

        private int CashBankGroupId
        {
            get
            {
                int GroupId = 0;
                if (CashLedgerId > 0)
                {
                    DataRowView dv = rglkpCashLedger.GetRowByKeyValue(CashLedgerId) as DataRowView;
                    if (dv != null)
                        GroupId = this.UtilityMember.NumberSet.ToInteger(dv.Row["Group_ID"].ToString());
                }
                return GroupId;
            }
        }

        private double ledgerAmount = 0;
        private double LedgerAmount
        {
            get
            {
                ledgerAmount = gvBank.GetFocusedRowCellValue(colCashAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvBank.GetFocusedRowCellValue(colCashAmount).ToString()) : 0;
                return ledgerAmount;
            }
            set
            {
                LedgerAmount = value;
            }
        }

        public string MinDate
        {
            get;
            set;
        }

        public double CrTotal
        {
            get;
            set;
        }
        public double DrTotal
        {
            get;
            set;
        }

        public double BankTransSummaryVal
        {
            get { return colCashAmount.SummaryItem.SummaryValue != null ? this.UtilityMember.NumberSet.ToDouble(colCashAmount.SummaryItem.SummaryValue.ToString()) : 0; }
        }

        public Control nextFocusControl;
        public Control NextFocusControl
        {
            get { return nextFocusControl; }
            set { nextFocusControl = value; }
        }

        public Control beforeFocusControl;
        public Control BeforeFocusControl
        {
            get { return beforeFocusControl; }
            set { beforeFocusControl = value; }
        }

        public double bankAmount;
        public double BankAmount
        {
            get
            {
                bankAmount = 0;
                bankAmount = gvBank.GetRowCellValue(gvBank.FocusedRowHandle, colCashAmount) != null ?
                              this.UtilityMember.NumberSet.ToDouble(gvBank.GetRowCellValue(gvBank.FocusedRowHandle, colCashAmount).ToString()) : 0;
                return bankAmount;
            }
            set { bankAmount = value; }
        }

        #endregion

        #region Events

        private void UcAssetJournal_Load(object sender, EventArgs e)
        {
            ConstructCashTransEmptySournce();
            LoadLedger();
            LoadReceiptType();
            // colCashAmount.Caption = SetCurrencyFormat(colCashAmount.Caption);
            colLedgerBalance.Caption = colLedgerBalance.Caption; // this.SetCurrencyFormat(colLedgerBalance.Caption);
            //  gvBank.ExpandAllGroups();

            if (Flag == AssetInOut.PU || Flag == AssetInOut.IK)
                colDeleteCashBank.OptionsColumn.AllowEdit = colDeleteCashBank.OptionsColumn.AllowFocus = true;
            else
                colDeleteCashBank.OptionsColumn.AllowEdit = colDeleteCashBank.OptionsColumn.AllowFocus = false;

            if (Flag == AssetInOut.SL)
                colLedgerType.OptionsColumn.AllowEdit = colLedgerType.OptionsColumn.AllowFocus = true;
            else
                colLedgerType.OptionsColumn.AllowEdit = colLedgerType.OptionsColumn.AllowFocus = false;

            // Added by Praveen  to show or hide the delete column
            colDeleteCashBank.Visible = ShowDeleteColumn == true ? true : false;
        }

        private void gcBank_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                gvBank.SetFocusedRowCellValue(colLedgerGroupId, CashBankGroupId);
                bool canFocusNarration = false;
                string LedgerName = gvBank.GetFocusedRowCellValue(colCashLedger) != null ? gvBank.GetFocusedRowCellValue(colCashLedger).ToString() : string.Empty;
                double Amount = gvBank.GetFocusedRowCellValue(colCashAmount) != null ? UtilityMember.NumberSet.ToDouble(gvBank.GetFocusedRowCellValue(colCashAmount).ToString()) : 0;
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab)
                    && !e.Shift && !e.Alt
                    && (gvBank.FocusedColumn == colCashAmount || gvBank.FocusedColumn == colMaterializedOn))//&& (gvBank.IsLastRow))
                {
                    if (CashLedgerId == 0 && LedgerAmount == 0) { canFocusNarration = true; }
                    if (BankTransSummaryVal == PurchaseTransSummary || BankTransSummaryVal != PurchaseTransSummary)// == TransSummaryVal)
                    {
                        if ((gvBank.FocusedColumn == colMaterializedOn && CashBankGroupId == (int)FixedLedgerGroup.BankAccounts)
                            || (gvBank.FocusedColumn == colCashAmount && CashBankGroupId == (int)FixedLedgerGroup.Cash))
                        {
                            if (gvBank.IsLastRow) { canFocusNarration = true; }
                            else
                            {
                                //gvBank.MoveNext(); gvBank.FocusedColumn = colCashLedgerId;
                            }
                        }
                        else
                        {
                            if (CashBankGroupId == (int)FixedLedgerGroup.BankAccounts || CashBankGroupId == (int)FixedLedgerGroup.Cash)
                            {
                                //gvBank.MoveNext();
                                //gvBank.FocusedColumn = colMaterializedOn;
                                //gvBank.MoveNext();
                            }
                            else

                                //if (Flag == AssetInOut.IK || Flag == AssetInOut.PU)
                                canFocusNarration = true;
                        }
                    }
                    else if ((gvBank.FocusedColumn == colMaterializedOn && CashBankGroupId == (int)FixedLedgerGroup.BankAccounts)
                            || (gvBank.FocusedColumn == colCashAmount || CashBankGroupId == (int)FixedLedgerGroup.Cash))
                    {
                        if (gvBank.IsLastRow) { canFocusNarration = true; } //CashBankTransGridNewItem(); }
                        else { gvBank.MoveNext(); gvBank.FocusedColumn = colCashLedger; }
                        if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                        {
                            e.SuppressKeyPress = true;
                        }
                    }

                }
                if (canFocusNarration)
                {
                    gvBank.CloseEditor();
                    e.Handled = true;
                    e.SuppressKeyPress = true;

                    if (NextFocusControl != null)
                    {
                        NextFocusControl.Select();
                        NextFocusControl.Focus();
                    }
                }
                else if (gvBank.IsFirstRow && gvBank.FocusedColumn == colCashLedger && e.Shift && e.KeyCode == Keys.Tab)
                {
                    if (BeforeFocusControl != null)
                        BeforeFocusControl.Focus();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        private void rglkpCashLedger_Validating(object sender, CancelEventArgs e)
        {
            GridLookUpEdit gridLKPEdit = sender as GridLookUpEdit;
            int Group = 0;
            if (gridLKPEdit.EditValue != null)
            {
                int LedgerID = this.UtilityMember.NumberSet.ToInteger(gridLKPEdit.EditValue.ToString());

                DataRowView drvLedger = rglkpCashLedger.GetRowByKeyValue(LedgerID) as DataRowView;
                if (drvLedger != null)
                {
                    Group = this.UtilityMember.NumberSet.ToInteger(drvLedger["GROUP_ID"].ToString());  //CashBankGroupId;//ledgerSystem.FetchLedgerGroupById();
                }
                gvBank.SetFocusedRowCellValue(colCashLedger, LedgerID);
                gvBank.SetFocusedRowCellValue(colLedgerGroupId, CashBankGroupId);
                //if (BankTransSummaryVal <= 0 && LedgerAmount < 1)
                //{
                //    //double Amt = TransSummaryVal - CashTransSummaryVal;
                //    //gvBank.SetFocusedRowCellValue(colCashAmount, Amt.ToString());
                //    gvBank.SetFocusedRowCellValue(colCashLedger, LedgerID);
                //    gvBank.PostEditor();
                //    gvBank.UpdateCurrentRow();
                //}
                gvBank.PostEditor();
                gvBank.UpdateCurrentRow();
                EnableCashBankFields();
            }
        }

        private void rglkpCashLedger_Leave(object sender, EventArgs e)
        {
            if (gvBank.IsFirstRow)
            {
                CalculateFirstRowValue();
            }

            if (CashLedgerId > 0)
            {
                if (VoucherId == 0)
                {
                    gvBank.PostEditor();
                    gvBank.UpdateCurrentRow();
                    DataTable dtCashTrans = gcBank.DataSource as DataTable;
                    string Balance = GetLedgerBalanceValues(dtCashTrans, CashLedgerId); //ShowLedgerBalance(CashLedgerId, dtCashTrans, false);
                    if (Balance != string.Empty)
                    {
                        gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colLedgerBalance, Balance);
                    }
                }

            }
            else { gvBank.UpdateCurrentRow(); }
        }

        private void rglkpCashLedger_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                isMouseClicked = true;
        }

        private void rglkpCashLedger_EditValueChanged(object sender, EventArgs e)
        {
            if (isMouseClicked)
            {
                SendKeys.Send("{tab}"); isMouseClicked = false;
            }
        }

        private void gvBank_GotFocus(object sender, EventArgs e)
        {
            BaseView view = sender as BaseView;
            if (view == null)
                return;

            if (MouseButtons == System.Windows.Forms.MouseButtons.Left)
                return;
            view.ShowEditor();
            TextEdit editor = view.ActiveEditor as TextEdit;
            if (editor != null)
            {
                editor.SelectAll();
                editor.Focus();
            }
        }

        private void gvBank_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                // 21/04/2025, *Chinna, / They set the Maxvalue for current date, it is wrong
                // rdtMaterializedOn.MaxValue = UtilityMember.DateSet.ToDate(MinDate, false);
                if (CashLedgerId > 0)
                {
                    if ((CashBankGroupId != 12) // == (int)FixedLedgerGroup.Cash)
                        && (gvBank.FocusedColumn == colCashCheque || gvBank.FocusedColumn == colMaterializedOn))
                    {
                        e.Cancel = true;
                    }
                }
                if (gvBank.FocusedColumn == colCashSource)
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally { }
        }

        private void gvBank_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (gvBank.GetRowCellValue(e.RowHandle, colCashLedger) != null)
                {
                    int GroupId = 0;
                    int LedgerId = this.UtilityMember.NumberSet.ToInteger(gvBank.GetRowCellValue(e.RowHandle, colCashLedgerId).ToString());
                    if (CashLedgerId > 0)
                    {
                        DataRowView drvLedger = rglkpCashLedger.GetRowByKeyValue(LedgerId) as DataRowView;
                        if (drvLedger != null)
                        {
                            GroupId = this.UtilityMember.NumberSet.ToInteger(drvLedger["GROUP_ID"].ToString());  //CashBankGroupId;//ledgerSystem.FetchLedgerGroupById();
                        }

                        if ((e.Column == colCashCheque || e.Column == colMaterializedOn) &&
                        (GroupId != 12)) //== (int)FixedLedgerGroup.Cash))
                        {
                            e.Appearance.BackColor = Color.LightGray;
                        }
                    }
                }
                rtxtCashAmount.ReadOnly = true;
                colCashAmount.OptionsColumn.AllowEdit = false;
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally { }
        }

        void RealColumnEditCashTransAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            try
            {
                BaseEdit edit = sender as BaseEdit;
                if (edit.EditValue == null) return;
                gvBank.PostEditor();
                gvBank.UpdateCurrentRow();
                if (gvBank.ActiveEditor == null)
                    gvBank.ShowEditor();

                TextEdit txtTransAmount = edit as TextEdit;
                int grpCounts = (this.UtilityMember.NumberSet.ToInteger(Settingproperty.DecimalPlaces) + 1);
                if (txtTransAmount.Text.Length > grpCounts && txtTransAmount.SelectionLength == txtTransAmount.Text.Length)
                    txtTransAmount.Select(txtTransAmount.Text.Length - grpCounts, 0);

                if (CashLedgerId > 0)
                {
                    DataTable dtCashTrans = gcBank.DataSource as DataTable;
                    string Balance = GetLedgerBalanceValues(dtCashTrans, CashLedgerId); //ShowLedgerBalance(CashLedgerId, dtCashTrans, false);
                    if (Balance != string.Empty)
                    {
                        gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colLedgerBalance, Balance);
                        DrTotal = UtilityMember.NumberSet.ToDouble(dtCashTrans.Compute("SUM(AMOUNT)", "SOURCE=" + 2).ToString());
                        CrTotal = UtilityMember.NumberSet.ToDouble(dtCashTrans.Compute("SUM(AMOUNT)", "SOURCE=" + 1).ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void rdtMaterializedOn_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void rdtMaterializedOn_MouseDown(object sender, MouseEventArgs e)
        {
            rdtMaterializedOn.MinValue = UtilityMember.DateSet.ToDate(MinDate, false);
        }

        private void rdtMaterializedOn_KeyDown(object sender, KeyEventArgs e)
        {
            rdtMaterializedOn.MinValue = UtilityMember.DateSet.ToDate(MinDate, false);
        }

        #endregion

        #region Methods

        private void EnableCashBankFields()
        {
            try
            {
                int iLedgerId = 0;
                int Group = 0;
                DataTable dtTrans = gcBank.DataSource as DataTable;

                foreach (DataRow dr in dtTrans.Rows)
                {
                    if (dr.RowState != DataRowState.Deleted)
                    {
                        iLedgerId = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                        using (LedgerSystem ledger = new LedgerSystem())
                        {
                            ledger.LedgerId = iLedgerId;
                            Group = ledger.FetchLedgerGroupById();
                            if (Group == (int)FixedLedgerGroup.BankAccounts)
                            {
                                VisibleCashBankAdditionalFields(true);
                                break;
                            }
                            else
                            {
                                VisibleCashBankAdditionalFields(false);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        private void VisibleCashBankAdditionalFields(bool Visible)
        {
            colCashSource.VisibleIndex = 0;
            colCashLedger.VisibleIndex = 1;
            colCashAmount.VisibleIndex = 2;
            colCashCheque.VisibleIndex = 3;
            colMaterializedOn.VisibleIndex = 4;
            //colLedgerBalance.VisibleIndex = 5;
            // colDeleteCashBank.VisibleIndex = 6;

            if (Visible)
            {
                colCashCheque.Visible = true;
                colMaterializedOn.Visible = true;
            }
            else
            {
                colCashCheque.Visible = false;
                colMaterializedOn.Visible = false;
            }
        }

        public void CalculateFirstRowValue()
        {
            if (LedgerAmount >= 0 && BankTransSummaryVal != PurchaseTransSummary && VoucherId >= 0)
            {
                gvBank.MoveFirst();
                if (CashLedgerId == 0 && isCashLedgerExists())
                {
                    gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCashLedger, 1);
                }
                gvBank.PostEditor();
                gvBank.UpdateCurrentRow();
                DataTable dtTemp = gcBank.DataSource as DataTable;
                if (CashLedgerId > 0)
                {
                    string Balance = GetLedgerBalanceValues(dtTemp, CashLedgerId); //ShowLedgerBalance(CashLedgerId, dtTemp, false);
                    if (Balance != string.Empty) { gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colLedgerBalance, Balance); }
                }
                BankAmount = amount;
            }
        }

        private bool isCashLedgerExists()
        {
            bool isValid = true;
            int LedId = 0;
            using (LedgerSystem ledger = new LedgerSystem())
            {
                LedId = ledger.IsCashLedgerExists();
                if (LedId != 1)
                {
                    isValid = false;
                }
            }
            return isValid;
        }

        private string GetLedgerBalanceValues(DataTable dtTrans, int LedgerId)
        {
            string LedgerBalance = string.Empty;
            double OldValue = 0;
            double NewValue = 0;
            string NewValueMode = string.Empty;

            if (dtTrans != null)
            {
                NewValue = GetCalculatedAmount(LedgerId, dtTrans);
                OldValue = GetCalculatedTempAmount(LedgerId, dtTrans);

                LedgerBalance = GetCurBalance(LedgerId, OldValue, NewValue);
            }
            return LedgerBalance;
        }

        private string GetCurBalance(int LedId, double OldValue, double NewValue)
        {
            string Mode = string.Empty;
            double CurBal = 0.00;
            double dCalculateCurBal = 0.00;

            BalanceProperty Balance = FetchCurrentBalance(LedId);

            if (Balance.TransMode == TransactionMode.CR.ToString())
            {
                CurBal = -(Balance.Amount);
            }
            else
            {
                CurBal = Balance.Amount;
            }

            dCalculateCurBal = CurBal - (OldValue) + NewValue;
            if (dCalculateCurBal < 0)
            {
                Mode = TransactionMode.CR.ToString();
            }
            else
            {
                Mode = TransactionMode.DR.ToString();
            }

            return this.UtilityMember.NumberSet.ToCurrency(Math.Abs(dCalculateCurBal)) + " " + Mode;
        }

        private BalanceProperty FetchCurrentBalance(int LedgerId)
        {
            BalanceProperty balProperty;
            using (BalanceSystem balancesystem = new BalanceSystem())
            {
                balProperty = balancesystem.GetBalance(ProjectId, LedgerId, "", BalanceSystem.BalanceType.CurrentBalance);
            }
            return balProperty;
        }

        public void ConstructCashTransEmptySournce()
        {
            DataTable dtCashTransaction = new DataTable();
            dtCashTransaction.Columns.Add("SOURCE", typeof(string));
            dtCashTransaction.Columns.Add("LEDGER_FLAG", typeof(string));
            dtCashTransaction.Columns.Add("LEDGER_ID", typeof(UInt32));
            dtCashTransaction.Columns.Add("AMOUNT", typeof(decimal));
            dtCashTransaction.Columns.Add("CHEQUE_NO", typeof(string));
            dtCashTransaction.Columns.Add("MATERIALIZED_ON", typeof(DateTime));
            dtCashTransaction.Columns.Add("LEDGER_BALANCE", typeof(string));
            dtCashTransaction.Columns.Add("BUDGET_AMOUNT", typeof(string));
            dtCashTransaction.Columns.Add("TEMP_AMOUNT", typeof(decimal));
            dtCashTransaction.Columns.Add("GROUP_ID", typeof(int));
            dtCashTransaction.Columns.Add("LIVE_EXCHANGE_RATE", typeof(decimal));
            dtCashTransaction.Columns.Add("EXCHANGE_RATE", typeof(decimal));
            dtCashTransaction.Rows.Add(dtCashTransaction.NewRow());

            gcBank.DataSource = dtCashTransaction;
            gvBank.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            int sourceId = (Flag == AssetInOut.PU || Flag == AssetInOut.IK) ? (int)Source.To : (int)Source.By;
            gvBank.MoveFirst();
            gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCashSource, sourceId);

        }

        private void LoadReceiptType()
        {
            TransSource transSource = new TransSource();
            DataView dvtransSource = this.UtilityMember.EnumSet.GetEnumDataSource(transSource, Sorting.None);
            rglkpCashSource.DataSource = dvtransSource.ToTable();
            rglkpCashSource.DisplayMember = "Name";
            rglkpCashSource.ValueMember = "Id";
        }

        private void CashBankTransGridNewItem()
        {
            try
            {
                DataTable dtCashTransaction = gcBank.DataSource as DataTable;
                dtCashTransaction.Rows.Add(dtCashTransaction.NewRow());
                gcBank.DataSource = dtCashTransaction;
                gvBank.MoveNext();
                int sourceId = (Flag == AssetInOut.PU || Flag == AssetInOut.IK) ? (int)Source.To : (int)Source.By;
                gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCashSource, sourceId);
                gvBank.FocusedColumn = colCashLedger;
                gvBank.ShowEditor();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void gcBank_Leave(object sender, EventArgs e)
        {

        }

        private void gcBank_Enter(object sender, EventArgs e)
        {

        }


        public void LoadLedger()
        {
            try
            {
                if (this.Flag != AssetInOut.OP)
                {
                    using (LedgerSystem ledgerSystem = new LedgerSystem())
                    {
                        ledgerSystem.ProjectId = this.ProjectId;
                        //if (this.Flag == AssetInOut.PU)
                        //{
                        resultArgs = ledgerSystem.FetchCashBankLedger(); //Fetch only the Cash and Bank Ledgers.
                        dtLedgers = (resultArgs != null && resultArgs.Success) ? resultArgs.DataSource.Table : null;
                        //}
                        //else 

                        if (this.Settingproperty.AllowMultiCurrency == 1)
                        {
                            //dtLedgers.DefaultView.RowFilter = ledgerSystem.AppSchema.Ledger.CUR_COUNTRY_IDColumn.ColumnName +
                            //            " = " + (string.IsNullOrEmpty(Settingproperty.Country) ? "0" : Settingproperty.Country);

                            Int32 currencycountry = CurrencyCountryId; //glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString());
                            dtLedgers.DefaultView.RowFilter = ledgerSystem.AppSchema.Ledger.CUR_COUNTRY_IDColumn.ColumnName + " = " + currencycountry;
                        }
                        dtLedgers = dtLedgers.DefaultView.ToTable();

                        if (this.Flag == AssetInOut.IK)
                        {
                            colCashLedger.Caption = "Ledger";
                            resultArgs = ledgerSystem.FetchAllInkindLedgers(); //Fetch All inkind Ledgers.
                            dtLedgers = (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0) ? resultArgs.DataSource.Table : null;
                        }
                        else if (this.Flag == AssetInOut.GAIN)
                        {
                            dtLedgers = LoadGainLedgers(); //Fetch All Gain Ledgers.
                        }
                        else if (this.Flag == AssetInOut.LOSS)
                        {
                            dtLedgers = LoadLossLedgers(); //Fetch All Loss Ledgers.
                        }
                        else if (this.Flag == AssetInOut.DN)
                        {
                            colCashLedger.Caption = "Ledger";
                            resultArgs = ledgerSystem.FetchAllInkindLedgers();
                            dtLedgers = (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0) ? resultArgs.DataSource.Table : null;  //Fetch All Expence Ledgers.
                        }
                        else if (this.Flag == AssetInOut.DS)
                        {
                            colCashLedger.Caption = "Ledger";
                            dtLedgers = LoadDisposalLedgers();

                            // it is commanded in order to post invalid Cash and Bank Transaction , Need to find. it is added by Mr. Sudhakar in view History. (Chinna)
                            //  dtLedgers = LoadCashExpenseLedgers();
                        }
                        else if (this.Flag == AssetInOut.INS || this.Flag == AssetInOut.AMC)
                        {
                            dtLedgers = LoadCashExpenseLedgers();
                        }
                        else if (this.Flag == AssetInOut.SL)
                        {
                            DataView dv = null;
                            colCashLedger.Caption = "Cash / Bank";
                            resultArgs = ledgerSystem.FetchCashBankLedger(); //Fetch only the Cash and Bank Ledgers.   

                            if (resultArgs != null && resultArgs.Success)
                            {
                                //dtLedgers = resultArgs.DataSource.Table;
                                dtLedgers = resultArgs.DataSource.Table; //LoadLossLedgers();

                                if (this.Settingproperty.AllowMultiCurrency == 1)
                                {
                                    Int32 currencycountry = CurrencyCountryId; //glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString());
                                    dtLedgers.DefaultView.RowFilter = ledgerSystem.AppSchema.Ledger.CUR_COUNTRY_IDColumn.ColumnName + " = " + currencycountry;
                                }
                                dtLedgers = dtLedgers.DefaultView.ToTable();
                                dv = new DataView(dtLedgers);
                                //dv.RowFilter = "LEDGER_ID=1";
                                dtLedgers = dv.ToTable();


                                dtLedgers = LoadLossLedgers();

                                // Temp
                                resultArgs = ledgerSystem.FetchGainLedgers();
                                if (resultArgs != null && resultArgs.Success)
                                    dtLedgers.Merge(resultArgs.DataSource.Table);

                                //if (GainLossType != null)
                                //{
                                //    if (GainLossType.Equals("Loss"))
                                //        dtSource = LoadLossLedgers();
                                //    else
                                //        dtSource = LoadGainLedgers();

                                //    dtLedgers = dtSource;
                                //}
                            }
                            else
                            {
                                dtLedgers = null;
                            }
                        }
                        rglkpCashLedger.DataSource = null;
                        if (dtLedgers != null && dtLedgers.Rows.Count > 0)
                        {
                            rglkpCashLedger.DisplayMember = AppSchema.Ledger.LEDGER_NAMEColumn.ToString();
                            rglkpCashLedger.ValueMember = AppSchema.Ledger.LEDGER_IDColumn.ToString();
                            rglkpCashLedger.DataSource = dtLedgers;
                        }

                        //else
                        //{
                        //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_LEDGER_MAPPING_TO_PROJECT) + " ' " + ProjectName + " '");
                        //}

                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private DataTable LoadGainLedgers()
        {
            DataTable dt = new DataTable();
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                ledgerSystem.ProjectId = this.ProjectId;
                resultArgs = ledgerSystem.FetchCashBankLedger(); //Fetch All the Ledgers.
                dtLedgers = dtLedgers.DefaultView.ToTable();
                dt = (resultArgs != null && resultArgs.Success) ? resultArgs.DataSource.Table : null;
                if (this.Settingproperty.AllowMultiCurrency == 1)
                {

                    Int32 currencycountry = CurrencyCountryId; //glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString());
                    dt.DefaultView.RowFilter = ledgerSystem.AppSchema.Ledger.CUR_COUNTRY_IDColumn.ColumnName + " = " + currencycountry;
                    dt = dt.DefaultView.ToTable();
                }
                resultArgs = ledgerSystem.FetchGainLedgers();
                if (resultArgs != null && resultArgs.Success)
                    dt.Merge(resultArgs.DataSource.Table);
            }
            return dt;
        }

        private DataTable LoadLossLedgers()
        {
            DataTable dt = new DataTable();
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                ledgerSystem.ProjectId = this.ProjectId;
                resultArgs = ledgerSystem.FetchCashBankLedger(); //Fetch All the Ledgers.
                dt = (resultArgs != null && resultArgs.Success) ? resultArgs.DataSource.Table : null;
                if (this.Settingproperty.AllowMultiCurrency == 1)
                {

                    Int32 currencycountry = CurrencyCountryId; //glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString());
                    dt.DefaultView.RowFilter = ledgerSystem.AppSchema.Ledger.CUR_COUNTRY_IDColumn.ColumnName + " = " + currencycountry;
                    dt = dt.DefaultView.ToTable();
                }
                resultArgs = ledgerSystem.FetchLossLedgers();
                if (resultArgs != null && resultArgs.Success)
                    dt.Merge(resultArgs.DataSource.Table);
            }
            return dt;
        }

        private DataTable LoadDisposalLedgers()
        {
            DataTable dt = new DataTable();
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                ledgerSystem.ProjectId = this.ProjectId;
                resultArgs = ledgerSystem.FetchDisposalLedgers(); //Fetch All the Ledgers.
                dt = (resultArgs != null && resultArgs.Success) ? resultArgs.DataSource.Table : null;
            }
            return dt;
        }

        private DataTable LoadCashExpenseLedgers()
        {
            DataTable dt = new DataTable();
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                ledgerSystem.ProjectId = this.ProjectId;
                ledgerSystem.GroupId = (int)Natures.Expenses;

                resultArgs = ledgerSystem.FetchCashBankLedger(); //Fetch All the Ledgers.
                dt = (resultArgs != null && resultArgs.Success) ? resultArgs.DataSource.Table : null;
                resultArgs = ledgerSystem.FetchAllExpenceLedgers(); // Fetch All Expense  Ledger

                if (resultArgs != null && resultArgs.Success)
                    dt.Merge(resultArgs.DataSource.Table);
            }
            return dt;
        }

        private void RealColumnEditCashTransAmount()
        {
            colCashAmount.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditCashTransAmount_EditValueChanged);
            this.gvBank.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvBank.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colCashAmount)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvBank.ShowEditorByMouse();
                    }));
                }
            };
        }

        private double GetCalculatedAmount(int LedId, DataTable dtVoucher)
        {
            double Amount = 0;
            if (dtVoucher.Rows.Count > 0)
            {
                //double DrAmt = 0;

                Amount = this.UtilityMember.NumberSet.ToDouble(dtVoucher.Compute("SUM(AMOUNT)", " LEDGER_ID=" + LedId).ToString());//+ (int)Source.To
                //DrAmt = this.UtilityMember.NumberSet.ToDouble(dtVoucher.Compute("SUM(AMOUNT)", "SOURCE=" + (int)Source.By + " AND LEDGER_ID=" + LedId).ToString());

                //if ((rgTransactionType.SelectedIndex == 0) || (rgTransactionType.SelectedIndex == 2))
                //{
                //dAmount = CrAmt - DrAmt;
                if (Amount > 0)
                {
                    Amount = -(Amount);
                }
                else
                {
                    Amount = Math.Abs(Amount);
                }
                //}
                //else if (rgTransactionType.SelectedIndex == 1)
                //{
                //    dAmount = DrAmt - CrAmt;
                //    if (dAmount > 0)
                //    {
                //        dAmount = +(dAmount);
                //    }
                //    else
                //    {
                //        dAmount = -(Math.Abs(dAmount));
                //    }
                //}
            }
            return Amount;
        }

        private double GetCalculatedTempAmount(int LedId, DataTable dtVoucher)
        {
            double Amount = 0;
            if (dtVoucher.Rows.Count > 0)
            {
                double CrAmt = 0;
                double DrAmt = 0;

                Amount = this.UtilityMember.NumberSet.ToDouble(dtVoucher.Compute("SUM(TEMP_AMOUNT)", "SOURCE=" + (int)Source.To + " AND LEDGER_ID=" + LedId).ToString());
                //DrAmt = this.UtilityMember.NumberSet.ToDouble(dtVoucher.Compute("SUM(TEMP_AMOUNT)", "SOURCE=" + (int)Source.By + " AND LEDGER_ID=" + LedId).ToString());

                //if ((rgTransactionType.SelectedIndex == 0) || (rgTransactionType.SelectedIndex == 2))
                //{
                //dAmount = CrAmt - DrAmt;
                Amount = Math.Abs(Amount);
                //if (rgTransactionType.SelectedIndex == 0 || rgTransactionType.SelectedIndex == 2)
                //{
                //dAmount = -dAmount;
                //}
                //else
                //{
                //dAmount = +(dAmount);
                //}
                //}
                //else if (rgTransactionType.SelectedIndex == 1)
                //{
                //dAmount = DrAmt - CrAmt;
                //if (rgTransactionType.SelectedIndex == 0 || rgTransactionType.SelectedIndex == 2)
                //{
                //    dAmount = +(dAmount);
                //}
                //else
                //{
                //    dAmount = -(dAmount);
                //}
                //}
            }
            return Amount;
        }

        //private bool IsValidSource()
        //{
        //    bool isValid = true;

        //    DataTable dtCashTrans = gcBank.DataSource as DataTable;
        //    DataView dv = new DataView(dtCashTrans);
        //    dv.RowFilter = "(LEDGER_ID>0 OR AMOUNT>0)";

        //    if (dv.Count > 0)
        //    {
        //        double dAmt = GetTransSummaryAmount();
        //        if (dAmt <= 0)
        //        {
        //            if (rgTransactionType.SelectedIndex == 0)
        //            {
        //                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.CASHBANK_MUST_DEBITED));
        //            }
        //            else
        //            {
        //                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.CASHBANK_MUST_CREDITED));
        //            }
        //            isValid = false;
        //        }
        //    }

        //    if (!isValid) { FocusTransactionGrid(); }

        //    return isValid;
        //}

        public string SetCurrencyFormat(string Caption)
        {
            string CurrencyFormat = string.Empty;
            if (!Caption.Contains(Settingproperty.Currency))
                CurrencyFormat = String.Format("{0} ({1})", Caption, Settingproperty.Currency);
            else { CurrencyFormat = Caption; }
            return CurrencyFormat;
        }

        public bool IsValidBankGrid()
        {
            bool isValid = true;
            try
            {
                gcBank.RefreshDataSource();
                gvBank.UpdateCurrentRow();
                DataTable dtBank = gcBank.DataSource as DataTable;
                int RowPosition = 0;
                int LedgerId = 0;
                decimal Amount = 0;
                //int Source = 0;
                DataView dv = new DataView(dtBank);
                dv.RowFilter = "(LEDGER_ID>0 OR AMOUNT>0)"; // OR SOURCE>0)";
                dtBank = dv.ToTable();
                //gvPurchase.FocusedColumn = colAssetName;
                if (dtBank.Rows.Count > 0)
                {
                    foreach (DataRow drPurchase in dtBank.Rows)
                    {
                        LedgerId = this.UtilityMember.NumberSet.ToInteger(drPurchase["LEDGER_ID"].ToString());
                        Amount = this.UtilityMember.NumberSet.ToDecimal(drPurchase["AMOUNT"].ToString());
                        //Source = this.UtilityMember.NumberSet.ToInteger(drPurchase["SOURCE"].ToString());

                        if (LedgerId == 0)
                        {
                            XtraMessageBox.Show("Required Information not filled, Cash/Bank Ledger is empty");
                            gvBank.FocusedColumn = colCashLedger;
                            isValid = false;
                        }
                        else if (Amount == 0)
                        {
                            XtraMessageBox.Show("Required Information not filled, Cash/Bank Amount is empty");
                            gvBank.FocusedColumn = colCashLedger;
                            isValid = false;
                        }
                        //else if (Source == 0)
                        //{
                        //    XtraMessageBox.Show("Required Information not filled, Source is empty");
                        //    gvBank.FocusedColumn = colCashSource;
                        //    isValid = false;
                        //}
                        if (!isValid) break;
                        RowPosition = RowPosition + 1;
                    }
                }
                else
                {
                    isValid = false;
                    XtraMessageBox.Show("Select Cash/Bank.");
                    gvBank.FocusedColumn = colCashLedger;
                }
                if (!isValid)
                {
                    gvBank.CloseEditor();
                    gvBank.FocusedRowHandle = gvBank.GetRowHandle(RowPosition);
                    gvBank.ShowEditor();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message + Environment.NewLine + ex.Source);
                isValid = false;
            }
            return isValid;
        }

        public void FocusCashTransaction()
        {
            gcBank.Select();
            gvBank.MoveFirst();
            gvBank.FocusedColumn = colCashLedger;
            gvBank.ShowEditor();
        }
        public void SetCashLedger(double amount)
        {
            gcBank.RefreshDataSource();
            if (dtLedgers != null && dtLedgers.Rows.Count > 0)
            {
                gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCashAmount, amount);
            }
            if (CashLedgerId == 0 && isCashLedgerExists())
            {
                if (Flag == AssetInOut.IK)
                {
                    if (dtLedgers != null && dtLedgers.Rows.Count > 0)
                    {
                        int ledgerId = UtilityMember.NumberSet.ToInteger(dtLedgers.Rows[0]["LEDGER_ID"].ToString());
                        gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCashLedger, ledgerId);
                        gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colLedgerGroupId, CashBankGroupId);
                    }
                }
                else if (Flag == AssetInOut.DN)
                {
                    if (dtLedgers != null && dtLedgers.Rows.Count > 0)
                    {
                        int ledgerId = UtilityMember.NumberSet.ToInteger(dtLedgers.Rows[0]["LEDGER_ID"].ToString());
                        gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCashLedger, ledgerId);
                    }
                }
                else if (Flag == AssetInOut.DS)
                {
                    if (dtLedgers != null && dtLedgers.Rows.Count > 0)
                    {
                        int ledgerId = UtilityMember.NumberSet.ToInteger(dtLedgers.Rows[0]["LEDGER_ID"].ToString());
                        gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCashLedger, ledgerId);
                    }
                }

                else if (Flag == AssetInOut.GAIN)
                {
                    gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCashLedger, 1);
                    gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colLedgerGroupId, CashBankGroupId);

                }
                else if (Flag == AssetInOut.LOSS)
                {
                    gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCashLedger, 1);
                    gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colLedgerGroupId, CashBankGroupId);

                }
                else if (Flag == AssetInOut.PU || Flag == AssetInOut.AMC || Flag == AssetInOut.INS)
                {
                    gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCashLedger, 1);
                    gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCashSource, (int)Source.To);
                    gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colLedgerGroupId, CashBankGroupId);
                }
                else if (Flag != AssetInOut.PU || Flag != AssetInOut.LOSS || Flag != AssetInOut.GAIN || Flag != AssetInOut.DN || Flag != AssetInOut.LOSS || Flag != AssetInOut.LOSS)
                {
                    gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCashLedger, 1);
                    gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colLedgerGroupId, CashBankGroupId);
                }

            }
            gvBank.PostEditor();
            gvBank.UpdateCurrentRow();
            DataTable dtTemp = gcBank.DataSource as DataTable;
            if (CashLedgerId > 0)
            {
                string Balance = GetLedgerBalanceValues(dtTemp, CashLedgerId); //ShowLedgerBalance(CashLedgerId, dtTemp, false);
                if (Balance != string.Empty) { gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colLedgerBalance, Balance); }
            }
            BankAmount = amount;
        }

        public void SetGainLossLedger(double amount)
        {
            DataView dv = null;
            gcBank.RefreshDataSource();
            DataTable dtTemp = gcBank.DataSource as DataTable;
            gvBank.SetRowCellValue(gvBank.FocusedRowHandle + 1, colCashAmount, amount);
            if (GainLossLedgerId == 0 && isCashLedgerExists())
            {
                if (Flag == AssetInOut.GAIN)
                {
                    dtTemp.Rows.Add();
                    int ledgerId = 0;
                    if (dtLedgers.Rows.Count > 1)
                    {
                        dv = new DataView(dtLedgers);
                        dv.RowFilter = "GROUP_ID NOT IN (12,13)";
                        dtLedgers = dv.ToTable();
                        if (dtLedgers != null && dtLedgers.Rows.Count > 0)
                        {
                            ledgerId = UtilityMember.NumberSet.ToInteger(dtLedgers.Rows[0]["LEDGER_ID"].ToString());
                        }
                        //else
                        //    UtilityMember.NumberSet.ToInteger(dtLedgers.Rows[0]["LEDGER_ID"].ToString());
                    }
                    //else
                    //    UtilityMember.NumberSet.ToInteger(dtLedgers.Rows[0]["LEDGER_ID"].ToString());

                    if (ledgerId != 1)
                    {
                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            if (!dtTemp.Columns.Contains("LEDGER_TYPE"))
                                dtTemp.Columns.Add("LEDGER_TYPE", typeof(string));

                            dtTemp.Rows[dtTemp.Rows.Count - 1]["LEDGER_ID"] = ledgerId;
                            dtTemp.Rows[dtTemp.Rows.Count - 1]["AMOUNT"] = amount;
                            dtTemp.Rows[dtTemp.Rows.Count - 1]["SOURCE"] = 1; // 13/12/2024
                            string Balance = GetLedgerBalanceValues(dtTemp, ledgerId);
                            gvBank.SetRowCellValue(gvBank.FocusedRowHandle + 1, colLedgerGroupId, FetchledgerGroupId(ledgerId));
                            dtTemp.Rows[dtTemp.Rows.Count - 1]["LEDGER_BALANCE"] = Balance;

                            foreach (DataRow dr in dtTemp.Rows)
                            {
                                string Group = dr["GROUP_ID"].ToString();
                                if (Group == "12" || Group == "13")
                                {
                                    dr["LEDGER_TYPE"] = "Cash / Bank Ledger";
                                }
                                else
                                {
                                    dr["LEDGER_TYPE"] = "Gain / Loss Ledger";
                                }
                            }
                            dtTemp.AcceptChanges();
                        }
                    }
                }
                else if (Flag == AssetInOut.LOSS)
                {
                    int ledgerID = 0;
                    dtTemp.Rows.Add();
                    if (dtLedgers.Rows.Count > 1)
                    {
                        dv = new DataView(dtLedgers);
                        dv.RowFilter = "GROUP_ID NOT IN (12,13)";
                        dtLedgers = dv.ToTable();
                        if (dtLedgers != null && dtLedgers.Rows.Count > 0)
                        {
                            ledgerID = UtilityMember.NumberSet.ToInteger(dtLedgers.Rows[0]["LEDGER_ID"].ToString());
                        }
                    }
                    //else
                    //    UtilityMember.NumberSet.ToInteger(dtLedgers.Rows[0]["LEDGER_ID"].ToString());
                    if (ledgerID != 1)
                    {
                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {

                            if (!dtTemp.Columns.Contains("LEDGER_TYPE"))
                                dtTemp.Columns.Add("LEDGER_TYPE", typeof(string));

                            dtTemp.Rows[dtTemp.Rows.Count - 1]["LEDGER_ID"] = ledgerID;
                            dtTemp.Rows[dtTemp.Rows.Count - 1]["AMOUNT"] = amount;
                            dtTemp.Rows[dtTemp.Rows.Count - 1]["SOURCE"] = 2;
                            string Balance = GetLedgerBalanceValues(dtTemp, ledgerID);
                            gvBank.SetRowCellValue(gvBank.FocusedRowHandle + 1, colLedgerGroupId, FetchledgerGroupId(ledgerID));
                            dtTemp.Rows[dtTemp.Rows.Count - 1]["LEDGER_BALANCE"] = Balance;

                            foreach (DataRow dr in dtTemp.Rows)
                            {
                                string Group = dr["GROUP_ID"].ToString();
                                if (Group == "12" || Group == "13")
                                {
                                    dr["LEDGER_TYPE"] = "Cash / Bank Ledger";
                                }
                                else
                                {
                                    dr["LEDGER_TYPE"] = "Gain / Loss Ledger";
                                }
                            }
                            dtTemp.AcceptChanges();
                        }
                    }
                }
                gcBank.DataSource = null;
                gcBank.DataSource = dtTemp;
                if (dtTemp != null && dtTemp.Rows.Count > 1)
                {
                    colLedgerType.Visible = true;
                    colLedgerType.Group();
                }
                gcBank.RefreshDataSource();
                gcBank.ForceInitialize();
                // gvBank.FocusedColumn = colGroupId;
                colGroupId.Tag = false;
                gvBank.ExpandAllGroups();
                gvBank.PostEditor();
                gvBank.UpdateCurrentRow();
            }
        }

        public void SetExpenseLedger(double amount)
        {
            DataView dv = null;
            gcBank.RefreshDataSource();
            DataTable dtTemp = gcBank.DataSource as DataTable;
            gvBank.SetRowCellValue(gvBank.FocusedRowHandle + 1, colCashAmount, amount);
            dtTemp.Rows.Add();

            int ledgerId = 0;
            if (dtLedgers.Rows.Count > 1)
            {
                dv = new DataView(dtLedgers);
                dv.RowFilter = "GROUP_ID NOT IN (12,13)";
                dtLedgers = dv.ToTable();
                if (dtLedgers != null && dtLedgers.Rows.Count > 0)
                {
                    ledgerId = UtilityMember.NumberSet.ToInteger(dtLedgers.Rows[0]["LEDGER_ID"].ToString());
                }
            }

            if (ledgerId != 1)
            {
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    if (!dtTemp.Columns.Contains("LEDGER_TYPE"))
                        dtTemp.Columns.Add("LEDGER_TYPE", typeof(string));

                    dtTemp.Rows[dtTemp.Rows.Count - 1]["LEDGER_ID"] = ledgerId;
                    dtTemp.Rows[dtTemp.Rows.Count - 1]["AMOUNT"] = amount;
                    dtTemp.Rows[dtTemp.Rows.Count - 1]["SOURCE"] = (int)Source.By;
                    string Balance = GetLedgerBalanceValues(dtTemp, ledgerId);
                    gvBank.SetRowCellValue(gvBank.FocusedRowHandle + 1, colLedgerGroupId, FetchledgerGroupId(ledgerId));
                    dtTemp.Rows[dtTemp.Rows.Count - 1]["LEDGER_BALANCE"] = Balance;

                    foreach (DataRow dr in dtTemp.Rows)
                    {
                        string Group = dr["GROUP_ID"].ToString();
                        if (Group == "12" || Group == "13")
                        {
                            dr["LEDGER_TYPE"] = "Cash / Bank Ledger";
                        }
                        else
                        {
                            dr["LEDGER_TYPE"] = "Expense Ledger";
                        }
                    }
                    dtTemp.AcceptChanges();
                }
            }

            gcBank.DataSource = null;
            gcBank.DataSource = dtTemp;
            if (dtTemp != null && dtTemp.Rows.Count > 1)
            {
                colLedgerType.Visible = true;
                colLedgerType.Group();
            }
            gcBank.RefreshDataSource();
            gcBank.ForceInitialize();
            colGroupId.Tag = false;
            gvBank.ExpandAllGroups();
            gvBank.PostEditor();
            gvBank.UpdateCurrentRow();
        }

        private int FetchledgerGroupId(int ledgerId)
        {
            int groupId = 0;
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                ledgerSystem.LedgerId = ledgerId;
                groupId = ledgerSystem.FetchLedgerGroupById();
            }
            return groupId;
        }

        public void DeleteTransaction()
        {
            try
            {
                //if (!string.IsNullOrEmpty(gvBank.GetFocusedRowCellValue(colCashLedger).ToString()))
                //{
                if (gvBank.RowCount > 1)
                {
                    if (gvBank.FocusedRowHandle != GridControl.NewItemRowHandle)
                    {
                        if (XtraMessageBox.Show("Delete this entry?", "Acme.erp", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            gvBank.DeleteRow(gvBank.FocusedRowHandle);
                            //XtraMessageBox.Show("Record deleted");
                            gvBank.FocusedColumn = colCashSource;
                        }
                    }
                }
                else if (gvBank.RowCount == 1)
                {
                    if (LedgerAmount > 0 || CashLedgerId > 0)
                    {
                        if (XtraMessageBox.Show("Delete this entry?", "Acme.erp", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            ConstructCashTransEmptySournce();
                            int sourceId = (Flag == AssetInOut.PU || Flag == AssetInOut.IK) ? (int)Source.To : (int)Source.By;
                            gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCashSource, sourceId);
                            gvBank.FocusedColumn = colCashLedger;
                        }
                    }
                }
                //}
                else
                {
                    //XtraMessageBox.Show("There is no record to delete.");
                    //gvBank.FocusedColumn = colCashLedger;
                }
                CalculateFirstRowValue();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        private void rbtnDeleteBankTransaction_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DeleteTransaction();
        }

        public void AssignValues(int VoucherId)
        {
            try
            {
                DataTable dt = null;
                DataView dv = null;
                double amount = 0;
                using (AssetInwardOutwardSystem InwardOutwardSystem = new AssetInwardOutwardSystem())
                {
                    using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
                    {
                        voucherSystem.VoucherId = VoucherId;
                        InwardOutwardSystem.Flag = Flag.ToString();
                        resultArgs = (Flag == AssetInOut.SL) ? InwardOutwardSystem.FetchCashBankByVoucherId(VoucherId) :
                            Flag == AssetInOut.PU ? InwardOutwardSystem.FetchCashBankByVoucherIdForPurchase(VoucherId) :
                            Flag == AssetInOut.INS || Flag == AssetInOut.AMC ? InwardOutwardSystem.FetchAMCInsuranceByVoucherId(VoucherId) :
                        voucherSystem.FetchJournalDetails();

                        if (resultArgs != null && resultArgs.Success)
                        {
                            if (!resultArgs.DataSource.Table.Columns.Contains("GROUP_ID"))
                                resultArgs.DataSource.Table.Columns.Add("GROUP_ID", typeof(int));

                            dv = new DataView(resultArgs.DataSource.Table);

                            if (Flag == AssetInOut.IK)
                            {
                                dv.RowFilter = "CREDIT>0";
                                dt = dv.ToTable();
                                dt.Columns.Add("AMOUNT", typeof(decimal));
                                dt.Columns.Add("SOURCE", typeof(string));
                                dt.Columns.Add("TEMP_AMOUNT", typeof(decimal));
                                gcBank.DataSource = dt;
                                foreach (DataRow dr in dt.Rows)
                                {
                                    amount = UtilityMember.NumberSet.ToDouble(dr["CREDIT"].ToString());
                                    dr["AMOUNT"] = amount;
                                    dr["SOURCE"] = 1;
                                }
                            }
                            else if (Flag == AssetInOut.DS || Flag == AssetInOut.DN) // Chinna 23.03.2020 at 11 AM
                            {
                                dv.RowFilter = "DEBIT>0";
                                dt = dv.ToTable();
                                dt.Columns.Add("AMOUNT", typeof(decimal));
                                dt.Columns.Add("SOURCE", typeof(string));
                                dt.Columns.Add("TEMP_AMOUNT", typeof(decimal));
                                gcBank.DataSource = dt;
                                foreach (DataRow dr in dt.Rows)
                                {
                                    amount = UtilityMember.NumberSet.ToDouble(dr["DEBIT"].ToString());
                                    dr["AMOUNT"] = amount;
                                    dr["SOURCE"] = 2;
                                }
                            }
                            else
                            {
                                if (Flag == AssetInOut.SL || Flag == AssetInOut.INS || Flag == AssetInOut.AMC)
                                {
                                    DataTable dttemp = resultArgs.DataSource.Table;
                                    if (dttemp != null && dttemp.Rows.Count > 0)
                                    {
                                        if (!dttemp.Columns.Contains("LEDGER_TYPE"))
                                            dttemp.Columns.Add("LEDGER_TYPE", typeof(string));
                                        foreach (DataRow dr in dttemp.Rows)
                                        {
                                            string Group = dr["GROUP_ID"].ToString();
                                            if (Group == "12" || Group == "13")
                                            {
                                                dr["LEDGER_TYPE"] = "Cash / Bank Ledger";
                                            }
                                            else
                                            {
                                                dr["LEDGER_TYPE"] = Flag == AssetInOut.INS || Flag == AssetInOut.AMC ? "Expense Ledger" : "Gain / Loss Ledger";
                                            }
                                        }
                                    }
                                    gcBank.DataSource = dttemp;
                                    if (dttemp != null && dttemp.Rows.Count > 1)
                                    {
                                        colLedgerType.Visible = true;
                                        gvBank.ExpandAllGroups();
                                    }
                                    else
                                    {
                                        colLedgerType.UnGroup();
                                        colLedgerType.Visible = false;
                                    }
                                }
                                else
                                {
                                    gcBank.DataSource = resultArgs.DataSource.Table;
                                    gvBank.ExpandAllGroups();
                                }
                            }
                            BindCurrentBalance(gcBank.DataSource as DataTable);
                            EnableCashBankFields();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        public void AssignCashBankDetails(DataTable dtCashBankDetails)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                DataView dv = new DataView();
                ConstructCashTransEmptySournce();
                gcBank.DataSource = dtCashBankDetails;
                gvBank.ExpandAllGroups();
                BindCurrentBalance(gcBank.DataSource as DataTable);
                EnableCashBankFields();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        private DataTable BindCurrentBalance(DataTable dtTrans)
        {
            double currentbal = 0;
            int LedgerId = 0;
            string amount = string.Empty;
            foreach (DataRow dr in dtTrans.Rows)
            {
                currentbal = 0;
                LedgerId = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());

                BalanceProperty balance = FetchCurrentBalance(LedgerId);
                currentbal = UtilityMember.NumberSet.ToDouble(balance.Amount.ToString());
                amount = UtilityMember.NumberSet.ToCurrency(currentbal);
                amount = amount.Remove(0, 1);
                dr["LEDGER_BALANCE"] = amount + " " + balance.TransMode;
            }
            return dtTrans;
        }

        public void setBgColor()
        {
            if (Flag == AssetInOut.PU)
            {
                gvBank.Appearance.Row.BackColor = gvBank.Appearance.FocusedRow.BackColor =
                            gvBank.Appearance.FocusedRow.BackColor = gvBank.Appearance.FocusedRow.BackColor = Color.Wheat;
            }
            else if (Flag == AssetInOut.IK)
            {
                gvBank.Appearance.Row.BackColor = gvBank.Appearance.FocusedRow.BackColor =
                            gvBank.Appearance.FocusedRow.BackColor = gvBank.Appearance.FocusedRow.BackColor = Color.LightSteelBlue;
            }
        }

        public void CalculateAmount()
        {
            dt = gcBank.DataSource as DataTable;
            DrTotal = UtilityMember.NumberSet.ToDouble(dt.Compute("SUM(AMOUNT)", "SOURCE=" + 2).ToString());
            CrTotal = UtilityMember.NumberSet.ToDouble(dt.Compute("SUM(AMOUNT)", "SOURCE=" + 1).ToString());
        }

        #endregion

        private void gvBank_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (Flag == AssetInOut.GAIN || Flag == AssetInOut.LOSS)
            {
                GridView view = sender as GridView;
                if (e.RowHandle >= 0)
                {
                    if (e.RowHandle == 2)
                        e.Appearance.BackColor = Color.LightGreen;
                }
            }
        }

        private void gvBank_GroupRowCollapsing(object sender, RowAllowEventArgs e)
        {
            //  if (e.RowHandle == null || ((Invoice)e.Row).Status == InvoiceStatus.Invalidated && colGroupId.GroupIndex != -1)
            e.Allow = false;
        }

        private void gvBank_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
        {
            (e.Info as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo).ButtonBounds = Rectangle.Empty;
        }

        private void gvBank_ShownEditor(object sender, EventArgs e)
        {
            try
            {
                DataRow drLedger = null;
                int LedgerGroupId = 0;
                string filter = string.Empty;
                ColumnView cview = (ColumnView)sender;
                //ColumnView cview = (ColumnView)sender;               
                string text = cview.FocusedColumn.Name;
                if (cview.FocusedColumn.FieldName == colLedgerGroupId.FieldName && cview.FocusedValue != null)
                {
                    if (LedgerAmount != 0)
                    {
                        GridLookUpEdit grdlkp = (GridLookUpEdit)cview.ActiveEditor;
                        drLedger = gvBank.GetDataRow(gvBank.FocusedRowHandle);
                        if (drLedger != null)
                        {
                            LedgerGroupId = UtilityMember.NumberSet.ToInteger(drLedger["GROUP_ID"].ToString());

                            if (LedgerGroupId >= 0 && LedgerGroupId == (int)FixedLedgerGroup.BankAccounts || LedgerGroupId == (int)FixedLedgerGroup.Cash)
                            {
                                filter = "GROUP_ID IN (" + (int)FixedLedgerGroup.BankAccounts + "," + (int)FixedLedgerGroup.Cash + ")";
                            }
                            else if (LedgerGroupId >= 0 && !gvBank.IsFirstRow) //(int)FixedLedgerGroup.FixedDeposit)
                            {
                                filter = "GROUP_ID NOT IN (" + (int)FixedLedgerGroup.BankAccounts + "," + (int)FixedLedgerGroup.Cash + ")";
                            }

                            DataTable dtLedgers = grdlkp.Properties.DataSource as DataTable;
                            if (dtLedgers != null && dtLedgers.Rows.Count > 0)
                            {
                                DataView dvLedgers = new DataView(dtLedgers);
                                dvLedgers.RowFilter = filter;
                                if (dvLedgers != null)
                                {
                                    grdlkp.Properties.DataSource = dvLedgers.ToTable();
                                    this.gvBank.PostEditor();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message + Environment.NewLine + ex.Source);
            }
        }
    }
}
