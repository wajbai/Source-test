using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model.UIModel;
using DevExpress.XtraEditors.Repository;
using Bosco.Utility;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Bosco.Utility.ConfigSetting;
using AcMEDSync.Model;

namespace ACPP.Modules.UIControls
{
    public partial class UcCashBankGrid : DevExpress.XtraEditors.XtraUserControl
    {
        private ResultArgs resultArgs = null;
        CommonMember UtilityMember = new CommonMember();
        SettingProperty Settingproperty = new SettingProperty();
        MessageCatalog messageCatolough = new MessageCatalog();
        Dictionary<int, double> ExpAmount = new Dictionary<int, double>();
        bool isMouseClicked = false;

        int cashLedgerId = 0;
        public UcCashBankGrid()
        {
            InitializeComponent();
            RealColumnEditDebitAmount();
            RealColumnCreditAmount();
        }

        #region Properties
        private int LedgerId { get; set; }

        private double SummaryCredit
        {
            get { return this.UtilityMember.NumberSet.ToDouble(colCheqNo.SummaryItem.SummaryValue.ToString()); }
        }

        private double SummaryDebit
        {
            get { return colAmount.SummaryItem.SummaryValue != null ? this.UtilityMember.NumberSet.ToDouble(colAmount.SummaryItem.SummaryValue.ToString()) : 0; }
        }

        public int voucherId = 0;
        public int VoucherId
        {
            get { return voucherId; }
            set { voucherId = value; }
        }

        private double SummaryAmount;
        private double summaryAmount
        {
            set
            {
                SummaryAmount = value;
            }
            get
            {
                SummaryAmount = gvBank.GetFocusedRowCellValue(colAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvBank.GetFocusedRowCellValue(colAmount).ToString()) : 0;
                return SummaryAmount;
            }
        }

        private int projectId = 0;
        private int ProjectId
        {
            set { projectId = value; }
            get { return projectId; }
        }

        int groupId = 0;
        private int GroupId
        {
            get { return groupId; }
            set { groupId = value; }
        }
        private string projectName = string.Empty;
        private string ProjectName
        {
            set { projectName = value; }
            get { return projectName; }
        }

        private double ExpenseLedgerAmount { get; set; }

        private int CashBankGroupId
        {
            get
            {
                int GroupId = 0;
                if (LedgerId > 0)
                {
                    DataRowView dv = rglkpCashLedger.GetRowByKeyValue(LedgerId) as DataRowView;
                    if (dv != null)
                        GroupId = this.UtilityMember.NumberSet.ToInteger(dv.Row["Group_ID"].ToString());
                }
                return GroupId;
            }
        }

        private double CashTransSummaryVal
        {
            get { return this.UtilityMember.NumberSet.ToDouble(colAmount.SummaryItem.SummaryValue.ToString()); }
        }
        #endregion

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

        private void LoadReceiptType()
        {
            Source transSource = new Source();
            DataView dvtransSource = this.UtilityMember.EnumSet.GetEnumDataSource(transSource, Sorting.None);
            rglkpSource.DataSource = dvtransSource.ToTable();
            rglkpSource.DisplayMember = "Name";
            rglkpSource.ValueMember = "Id";
        }


        #region Events
        private void rglkpCashLedger_Leave(object sender, EventArgs e)
        {
            int Group = 0;
            if (LedgerId > 0)
            {
                gvBank.PostEditor();
                gvBank.UpdateCurrentRow();
                DataTable dtTrans = gcBank.DataSource as DataTable;
                string Balance = GetLedgerBalanceValues(dtTrans, LedgerId);
                if (Balance != string.Empty)
                {
                    int GroupId = FetchLedgerDetails(LedgerId);
                    //if (PartyLedgerAmount > 0 && (!GroupId.Equals((int)TDSDefaultLedgers.DutiesandTaxes)) && (!GroupId.Equals((int)TDSDefaultLedgers.SunderyCreditors)))
                    //{
                    //    if (VoucherId == 0)
                    //    {
                    //        gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colDebit, PartyLedgerAmount);
                    //    }
                    //}
                    gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colLedgerBalance, Balance);
                }

                // This is to automatically update debit amount to credit amount starts here
                //if (LedgerId > 0 && (SummaryDebit - SummaryCredit) > 0)
                //{
                //    if (summaryAmount == 0)
                //    {
                //        gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCheqNo, SummaryDebit - SummaryCredit);
                //    }
                //}
                //else if (LedgerId > 0 && (SummaryCredit - SummaryDebit) > 0)
                //{
                //    if (summaryAmount == 0)
                //    {
                //        gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colAmount, SummaryCredit - SummaryDebit);
                //    }
                //}
                // Ends Here

                //DataRowView drvLedger = rglkpCashLedger.GetRowByKeyValue(LedgerId) as DataRowView;
                //if (drvLedger != null)
                //{
                //    Group = this.UtilityMember.NumberSet.ToInteger(drvLedger["GROUP_ID"].ToString());  //CashBankGroupId;//ledgerSystem.FetchLedgerGroupById();
                //}


                ////if (TransSummaryVal > 0 && CashTransSummaryVal <= TransSummaryVal && CashLedgerAmount < 1)
                ////{
                ////    double Amt = TransSummaryVal - CashTransSummaryVal;
                ////gvBank.SetFocusedRowCellValue(colDebit, Amt.ToString());
                gvBank.SetFocusedRowCellValue(colLedgers, LedgerId);
                gvBank.PostEditor();
                gvBank.UpdateCurrentRow();
                ////}

                EnableCashBankFields();
            }
            else
            {
                gvBank.UpdateCurrentRow();
            }
        }

        private void UcCashBankGrid_Load(object sender, EventArgs e)
        {
            Construct();
            LoadReceiptType();
            LoadLedger(rglkpCashLedger);
            FocusCashTransactionGrid();
        }

        private void gvBank_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (gvBank.GetRowCellValue(e.RowHandle, colLedgers) != null)
                {
                    int GroupId = 0;
                    DataTable dt = gcBank.DataSource as DataTable;
                    int LedgerId = gvBank.GetRowCellValue(e.RowHandle, colLedgers) != null ? this.UtilityMember.NumberSet.ToInteger(gvBank.GetRowCellValue(e.RowHandle, colLedgers).ToString()) : 0;
                    if (this.LedgerId > 0)
                    {
                        DataRowView drvLedger = rglkpCashLedger.GetRowByKeyValue(LedgerId) as DataRowView;
                        if (drvLedger != null)
                        {
                            GroupId = this.UtilityMember.NumberSet.ToInteger(drvLedger["GROUP_ID"].ToString());  //CashBankGroupId;//ledgerSystem.FetchLedgerGroupById();
                        }

                        if ((e.Column == colCheqNo || e.Column == colMaterializedOn) &&
                        (GroupId != 12))
                        {
                            e.Appearance.BackColor = Color.LightGray;
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

        private void gvBank_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (LedgerId > 0)
            {
                if ((CashBankGroupId != 12)
                    && (gvBank.FocusedColumn == colCheqNo || gvBank.FocusedColumn == colMaterializedOn))
                {
                    e.Cancel = true;
                }
            }
            //if (gvBank.GetRowCellValue(gvBank.FocusedRowHandle, colLedgers) != null)
            //{
            //    if (gvBank.FocusedColumn == colAmount && summaryAmount > 0)
            //    {
            //        e.Cancel = true;
            //    }
            //    else if (gvBank.FocusedColumn == colCheqNo && summaryAmount > 0)
            //    {
            //        e.Cancel = true;
            //    }
            //    else if (gvBank.FocusedColumn == colLedgers && LedgerId.Equals(0))
            //    {
            //        e.Cancel = false;
            //    }
            //}
        }

        private void gcBank_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                bool canFocusOtherCharges = false;
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab)
                    && !e.Shift && !e.Alt && !e.Control
                    && (gvBank.FocusedColumn == colAmount || gvBank.FocusedColumn == colMaterializedOn))//&& (gvBank.IsLastRow))
                {
                    //if (LedgerId == 0 && CashLedgerAmount == 0) { canFocusOtherCharges = true; }
                    if ((gvBank.FocusedColumn == colMaterializedOn && CashBankGroupId == (int)FixedLedgerGroup.BankAccounts)
                            || (gvBank.FocusedColumn == colAmount)) //&& CashBankGroupId == (int)FixedLedgerGroup.Cash))
                    {
                        gvBank.MoveNext();
                        gvBank.FocusedColumn = colLedgers;
                        if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                        {
                            e.SuppressKeyPress = true;
                        }
                    }
                    if (canFocusOtherCharges)
                    {
                        gvBank.CloseEditor();
                        e.Handled = true;
                        e.SuppressKeyPress = true;

                        //txtOtherCharges.Select();
                        //txtOtherCharges.Focus();
                    }
                }
                else if (gvBank.IsFirstRow && gvBank.FocusedColumn == colLedgers && e.Shift && e.KeyCode == Keys.Tab)
                {
                    //gcPurchase.Select();
                    //gcPurchase.Focus();
                    //gvBank.FocusedColumn = colQuantity;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void gvBank_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            if (LedgerId == 0)
            {
                e.Valid = false;
                gvBank.FocusedColumn = colLedgers;
                gvBank.ShowEditor();
            }
        }

        private void gvBank_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
        {
            e.ErrorText = "Required information not filled";
            e.WindowCaption = "Acme.erp";
        }

        #endregion

        #region Methods
        private void EnableCashBankFields()
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

        private void VisibleCashBankAdditionalFields(bool Visible)
        {
            colAmount.VisibleIndex = 2;
            colCheqNo.VisibleIndex = 3;
            colMaterializedOn.VisibleIndex = 4;
            colLedgerBalance.VisibleIndex = 5;
            // colBudgetAmt.VisibleIndex = 6;
            //colDeleteCashBank.VisibleIndex = 6;

            if (Visible)
            {
                colCheqNo.Visible = true;
                colMaterializedOn.Visible = true;
            }
            else
            {
                colCheqNo.Visible = false;
                colMaterializedOn.Visible = false;
            }
        }

        //private void CalculateFirstRowValue()
        //{
        //    if (LedgerAmount >= 0 && CashTransSummaryVal != TransSummaryVal && VoucherId >= 0)
        //    {
        //        gvBank.MoveFirst();
        //        double Amount = gvBank.GetRowCellValue(gvBank.FocusedRowHandle, colDebit) != null ?
        //            this.UtilityMember.NumberSet.ToDouble(gvBank.GetRowCellValue(gvBank.FocusedRowHandle, colDebit).ToString()) : 0;
        //        if (Amount >= 0)
        //        {
        //            double dAmount = 0.0;
        //            if (CashTransSummaryVal <= TransSummaryVal)
        //            {
        //                dAmount = (TransSummaryVal - CashTransSummaryVal) + Amount;
        //            }
        //            else if (CashTransSummaryVal >= TransSummaryVal)
        //            {
        //                dAmount = Amount - (CashTransSummaryVal - TransSummaryVal);
        //            }

        //            if (dAmount >= 0)
        //            {
        //                gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colDebit, dAmount);
        //                if (CashLedgerId == 0 && isCashLedgerExists())
        //                {
        //                    gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colLedger, 1);
        //                }
        //                gvBank.PostEditor();
        //                gvBank.UpdateCurrentRow();
        //                DataTable dtTemp = gcBank.DataSource as DataTable;
        //                if (CashLedgerId > 0)
        //                {
        //                    string Balance = GetLedgerBalanceValues(dtTemp, CashLedgerId); //ShowLedgerBalance(CashLedgerId, dtTemp, false);
        //                    if (Balance != string.Empty) { gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colLedgerBalance, Balance); }
        //                }
        //            }
        //        }
        //    }
        //}

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
            //string balanceamount = string.Empty;
            //double LedgerBalance = 0.00;
            //try
            //{
            //    if (dtTrans != null)
            //    {
            //        BalanceProperty balance = FetchCurrentBalance(LedgerId);
            //        LedgerBalance = (balance.Amount - summaryAmount);
            //        balanceamount = UtilityMember.NumberSet.ToCurrency(LedgerBalance) + " " + balance.TransMode;
            //        balanceamount = balanceamount.Remove(0, 1);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message);
            //}

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

        private double GetCalculatedAmount(int LedId, DataTable dtVoucher)
        {
            double Amt = 0;
            if (dtVoucher.Rows.Count > 0)
            {

                //double DrAmt = 0;

                Amt = this.UtilityMember.NumberSet.ToDouble(dtVoucher.Compute("SUM(AMOUNT)", "LEDGER_ID='" + LedId + "'").ToString());

                if (Amt > 0)
                {
                    Amt = -(Amt);
                }
                else
                {
                    Amt = Math.Abs(Amt);
                }
            }
            return Amt;
        }

        private void FocusTransactionGrid()
        {
            gcBank.Focus();
            gvBank.MoveFirst();
            gvBank.FocusedRowHandle = DevExpress.XtraGrid.GridControl.NewItemRowHandle;
            gvBank.FocusedColumn = gvBank.VisibleColumns[0];
            gvBank.ShowEditor();
        }

        private double GetCalculatedTempAmount(int LedId, DataTable dtVoucher)
        {
            double Amt = 0;
            if (dtVoucher.Rows.Count > 0)
            {
                //double DrAmt = 0;
                Amt = this.UtilityMember.NumberSet.ToDouble(dtVoucher.Compute("SUM(TEMP_AMOUNT)", "LEDGER_ID='" + LedId + "'").ToString());

                if (Amt > 0)
                {
                    Amt = -(Amt);
                }
                else
                {
                    Amt = Math.Abs(Amt);
                }
            }
            return Amt;
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
                balProperty = balancesystem.GetBalance(7, LedgerId, "", BalanceSystem.BalanceType.CurrentBalance);
            }
            return balProperty;
        }

        #endregion

        private int FetchLedgerDetails(int LedgerID)
        {
            int GroupId = 0;
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    ledgerSystem.LedgerId = LedgerID;
                    GroupId = ledgerSystem.FetchLedgerGroupById();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
            return GroupId;
        }

        private void LoadLedger(RepositoryItemGridLookUpEdit glkpLedger)
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    ledgerSystem.ProjectId = 7;//ProjectId;
                    //resultArgs = ledgerSystem.FetchLedgersForAssetUserContorls();
                    glkpLedger.DataSource = null;
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        //CostCentreInfo = resultArgs.DataSource.Table.DefaultView;
                        glkpLedger.DataSource = resultArgs.DataSource.Table;
                        glkpLedger.DisplayMember = "LEDGER_NAME";
                        glkpLedger.ValueMember = "LEDGER_ID";
                        //this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpCashLedger, resultArgs.DataSource.Table, ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                    }
                    else
                    {
                        XtraMessageBox.Show("Ledger(s) are not mapped to this project." + ProjectName + "");
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message + Environment.NewLine + Ex.Source);
            }
            finally { }
        }

        private static void FocusCashTransactionGrid()
        {
            //gcBank.Focus();
            //gvBank.MoveFirst();
            //gvBank.FocusedRowHandle = DevExpress.XtraGrid.GridControl.NewItemRowHandle;
            //gvBank.FocusedColumn = gvBank.VisibleColumns[0];
            //gvBank.ShowEditor();
        }

        private void Construct()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SOURCE", typeof(string));
            dt.Columns.Add("LEDGER_ID", typeof(string));
            dt.Columns.Add("AMOUNT", typeof(decimal));
            dt.Columns.Add("CHEQUE_NO", typeof(string));
            dt.Columns.Add("MATERIALIZED_ON", typeof(DateTime));
            dt.Columns.Add("TEMP_AMOUNT", typeof(decimal));
            dt.Columns.Add("LEDGER_BALANCE", typeof(string));
            dt.Columns.Add("VALUE", typeof(int));
            gcBank.DataSource = dt;
            gvBank.AddNewRow();
        }

        private void RealColumnCreditAmount()
        {
            colCheqNo.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditTransAmount_EditValueChanged);
            this.gvBank.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvBank.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colCheqNo)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvBank.ShowEditorByMouse();
                    }));
                }
            };
        }

        private void RealColumnEditDebitAmount()
        {
            colAmount.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditTransAmount_EditValueChanged);
            this.gvBank.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvBank.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colAmount)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvBank.ShowEditorByMouse();
                    }));
                }
            };
        }

        void RealColumnEditTransAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvBank.PostEditor();
            gvBank.UpdateCurrentRow();
            if (gvBank.ActiveEditor == null)
            {
                gvBank.ShowEditor();
            }

            TextEdit txtTransAmount = edit as TextEdit;
            int grpCounts = (this.UtilityMember.NumberSet.ToInteger(Settingproperty.DecimalPlaces) + 1);
            if (txtTransAmount.Text.Length > grpCounts && txtTransAmount.SelectionLength == txtTransAmount.Text.Length)
                txtTransAmount.Select(txtTransAmount.Text.Length - grpCounts, 0);

            if (LedgerId > 0)
            {
                DataTable dtTrans = gcBank.DataSource as DataTable;
                string Balance = GetLedgerBalanceValues(dtTrans, LedgerId); //ShowLedgerBalance(LedgerId, dtTrans, true);
                if (Balance != string.Empty) { gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colLedgerBalance, Balance); }
            }
        }

        private void rglkpCashLedger_EditValueChanged(object sender, EventArgs e)
        {
            GridLookUpEdit gridLKPEdit = sender as GridLookUpEdit;
            int Group = 0;
            if (gridLKPEdit.EditValue != null)
            {
                this.LedgerId = this.UtilityMember.NumberSet.ToInteger(gridLKPEdit.EditValue.ToString());

                DataRowView drvLedger = rglkpCashLedger.GetRowByKeyValue(this.LedgerId) as DataRowView;
                if (drvLedger != null)
                {
                    Group = this.UtilityMember.NumberSet.ToInteger(drvLedger["GROUP_ID"].ToString());  //CashBankGroupId;//ledgerSystem.FetchLedgerGroupById();
                }
                gvBank.SetFocusedRowCellValue(colLedgers, LedgerId);
            }
            //LedgerId = this.UtilityMember.NumberSet.ToInteger(gvBank.GetFocusedRowCellValue(colLedgers).ToString());
            //if (isMouseClicked)
            //{
            //    SendKeys.Send("{tab}"); isMouseClicked = false;
            //}
        }

        private void rglkpCashLedger_MouseDown(object sender, MouseEventArgs e)
        {
            //To Identify the Mouse Click Event
            if (e.Button == MouseButtons.Left)
                isMouseClicked = true;
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


                //if (TransSummaryVal > 0 && CashTransSummaryVal <= TransSummaryVal && CashLedgerAmount < 1)
                //{
                //    double Amt = TransSummaryVal - CashTransSummaryVal;
                //gvBank.SetFocusedRowCellValue(colDebit, Amt.ToString());
                gvBank.SetFocusedRowCellValue(colLedgers, LedgerID);
                gvBank.PostEditor();
                gvBank.UpdateCurrentRow();
                //}

                EnableCashBankFields();
            }
        }
    }
}
