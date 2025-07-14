/*
 * Date: 06/02/2020
 * Purpose : This form is used to get selected voucher's sub ledger vooucher details. 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Model.Transaction;
using Bosco.Model.UIModel;
using System.Runtime.Serialization;
using DevExpress.XtraGrid;
using ACPP.Modules.Master;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using System.Collections;
using AcMEDSync.Model;

namespace ACPP.Modules.Transaction
{
    public partial class frmTransactionSubLedger : frmFinanceBaseAdd
    {
        #region Variable Declaration
        ResultArgs resultArgs = null;
        #endregion

        #region Properites
        DefaultVoucherTypes VType = DefaultVoucherTypes.Receipt;
        private int ProjectId = 0;
        private int Voucherid = 0;
        private int LedgerId = 0;
        private double LedgerAmount = 0;
        private DateTime VoucherDate;
        private TransactionMode LedgerTransMode = TransactionMode.DR;

        private Int32 BudgetId = 0;
        private Nullable<DateTime> BudgetdateFrom;
        private Nullable<DateTime> BudgetdateTo;

        //current row  sub ledger id
        private int subledgerid = 0;
        private int SubLedgerId
        {
            set
            {
                subledgerid = value;
            }
            get
            {
                subledgerid = gvSubLedgerVouchers.GetFocusedRowCellValue(colSubLedgerId) != null ? this.UtilityMember.NumberSet.ToInteger(gvSubLedgerVouchers.GetFocusedRowCellValue(colSubLedgerId).ToString()) : 0;
                return subledgerid;
            }
        }

        //current row sub ledger amount
        private double amount = 0;
        private double Amount
        {
            set
            {
                amount = value;
            }
            get
            {
                amount = gvSubLedgerVouchers.GetFocusedRowCellValue(colAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvSubLedgerVouchers.GetFocusedRowCellValue(colAmount).ToString()) : 0;
                return amount;
            }
        }

        //current row sub ledger amount
        private double tmpamount = 0;
        private double TmpAmount
        {
            get
            {
                tmpamount = gvSubLedgerVouchers.GetFocusedRowCellValue(colTmpAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvSubLedgerVouchers.GetFocusedRowCellValue(colTmpAmount).ToString()) : 0;
                return tmpamount;
            }
        }

        //current row sub ledger Budget amount
        private double budgetamount = 0;
        private double BudgetAmount
        {
            get
            {
                budgetamount = gvSubLedgerVouchers.GetFocusedRowCellValue(colBudgetAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvSubLedgerVouchers.GetFocusedRowCellValue(colBudgetAmount).ToString()) : 0;
                return budgetamount;
            }
        }

        private string budgettransmode= "";
        private string BudgetTransMode
        {
            set
            {
                budgettransmode = value;
            }
            get
            {
                return budgettransmode;
            }
        }
      
        //total all Sub Ledger amount in grid
        private double totalamount = 0;
        public double TotalAmount
        {
            get
            {
                double rtn = 0;

                if (colAmount.SummaryItem.SummaryValue != null)
                {
                    rtn = this.UtilityMember.NumberSet.ToDouble(colAmount.SummaryItem.SummaryValue.ToString());
                }
                return rtn;
            }

        }

        private DataTable dtSubLedgerVouchers = new DataTable();
        public DataTable SubLedgersVoucher
        {
            set { dtSubLedgerVouchers = value; }
            get { return dtSubLedgerVouchers; }

        }

        private DataTable dtValidate = new DataTable();

        ////Focus to new row
        //private bool GridNewItem
        //{
        //    set
        //    {
        //        if (value)
        //        {
        //            DataTable dtLedgerReference = gcSubLedgerVouchers.DataSource as DataTable;
        //            AddNewRow(dtLedgerReference);
        //            gcSubLedgerVouchers.DataSource = dtLedgerReference;
        //            gvSubLedgerVouchers.MoveNext();
        //            gvSubLedgerVouchers.FocusedColumn = colSubLedger;
        //            gvSubLedgerVouchers.ShowEditor();
        //        }
        //    }
        //}
        #endregion

        #region Constructor
        public frmTransactionSubLedger()
        {
            InitializeComponent();
            this.PageTitle = "Sub Ledger Vouchers";

            //Attach amount chage event
            RealColumnEditTransAmount();
        }

        public frmTransactionSubLedger(DefaultVoucherTypes vouchertype, int projectid, int voucherid, int ledgerId, TransactionMode ledgertransmode, DateTime voucherdate,
                    double ledgeramount, string LedgerName, DataTable dtSubLedgersVouchers): this()
        {
            VType = vouchertype;
            ProjectId = projectid;
            Voucherid = voucherid;
            LedgerId = ledgerId;
            LedgerTransMode = ledgertransmode;
            LedgerAmount = ledgeramount;
            VoucherDate = voucherdate;
            SubLedgersVoucher = dtSubLedgersVouchers;
            lblLedgerAmount.Text = this.UtilityMember.NumberSet.ToCurrency(ledgeramount) + " " + ledgertransmode.ToString();
            lblLedgerNameValue.Text = " ";
            lblLedgerNameValue.Text = LedgerName;
        }
        #endregion

        #region Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmTransactionReference_Load(object sender, EventArgs e)
        {
            BindVoucherLedgerSubLedgerVouchers();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmTransactionCostCenter_Shown(object sender, EventArgs e)
        {
            gcSubLedgerVouchers.Select();
            gvSubLedgerVouchers.FocusedRowHandle = DevExpress.XtraGrid.GridControl.NewItemRowHandle;
            gvSubLedgerVouchers.FocusedColumn = gvSubLedgerVouchers.VisibleColumns[0];
            gvSubLedgerVouchers.ShowEditor();
        }

        /// <summary>
        /// Attach Event for amount changed
        /// </summary>
        private void RealColumnEditTransAmount()
        {
            colAmount.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditTransAmount_EditValueChanged);
            this.gvSubLedgerVouchers.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvSubLedgerVouchers.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colAmount)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvSubLedgerVouchers.ShowEditorByMouse();
                    }));
                }
            };
        }

        ///// <summary>--
        ///// 
        ///// </summary>
        //private void AssignValues()
        //{
        //    if (SubLedgersVoucher != null && SubLedgersVoucher.Rows.Count > 0)
        //    {
        //        gcSubLedgerVouchers.DataSource = SubLedgersVoucher;
        //    }
        //}

        /// <summary>
        /// Event for amount changed
        /// </summary>
        private void RealColumnEditTransAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvSubLedgerVouchers.PostEditor();
            gvSubLedgerVouchers.UpdateCurrentRow();
            if (gvSubLedgerVouchers.ActiveEditor == null)
            {
                gvSubLedgerVouchers.ShowEditor();
            }

            TextEdit txtTransAmount = edit as TextEdit;
            int grpCounts = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.DecimalPlaces) + 1);
            if (txtTransAmount.Text.Length > grpCounts && txtTransAmount.SelectionLength == txtTransAmount.Text.Length)
                txtTransAmount.Select(txtTransAmount.Text.Length - grpCounts, 0);

            if (BudgetId > 0 && LedgerId > 0 && SubLedgerId > 0)
            {
                DataTable dtTrans = gcSubLedgerVouchers.DataSource as DataTable;
                string Balance = CalculateSubLedgerBudgetVariance(); 
                if (Balance != string.Empty) { gvSubLedgerVouchers.SetRowCellValue(gvSubLedgerVouchers.FocusedRowHandle, colBudgetVariance, Balance); }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvReferenceNo_ShownEditor(object sender, EventArgs e)
        {
            //DevExpress.XtraGrid.Views.Grid.GridView view;
            //view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            //if (view.FocusedColumn == colSubLedger && view.ActiveEditor is DevExpress.XtraEditors.GridLookUpEdit)
            //{
            //    string selectedrefs = GetSelectedReferenceNosInGrid(subledgerid);
            //    DevExpress.XtraEditors.GridLookUpEdit edit;
            //    edit = (DevExpress.XtraEditors.GridLookUpEdit)view.ActiveEditor;

            //    DataTable table = (DataTable)edit.Properties.DataSource; ;
            //    DataView clone = new DataView(table);
            //    /*ArrayList values = new ArrayList();
            //    for (int i = 0; i < view.DataRowCount; i++)
            //    {
            //        DataRow row = view.GetDataRow(i);
            //        string val = row["FactId"].ToString();
            //        if (!values.Contains(val))
            //            values.Add(val);
            //    }
            //    string ids = string.Empty;
            //    foreach (string str in values)
            //        ids += string.Format("{0},", str);
            //    ids = ids.TrimEnd(',');*/
            //    using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
            //    {
            //        string selected = GetSelectedReferenceNosInGrid(SubLedgerId);
            //        clone.RowFilter = string.Empty;
            //        if (!String.IsNullOrEmpty(selected))
            //        {
            //            clone.RowFilter = vouchersystem.AppSchema.VoucherTransaction.REF_VOUCHER_IDColumn.ColumnName + " NOT IN (" + selected + ")";
            //        }

            //        DataTable dtbind = clone.ToTable();
            //        edit.Properties.DataSource = dtbind;
            //    }
            //}
            //else if (view.FocusedColumn == colAmount)
            //{
            //    //var editor = (TextEdit)gvReferenceNo.ActiveEditor;
            //    //editor.SelectionLength = 0;
            //    //editor.SelectionStart = editor.Text.Length;
            //}
        }
            
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmTransactionReference_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (TotalAmount > 0)
            {
                ValidateSubLedgerVouchers();
            }
            //if (TotalAmount > 0)
            //{
            //    if (!ValidateReferenceEntries() && e.CloseReason != CloseReason.UserClosing)
            //    {
            //        e.Cancel = true;
            //    }
            //}
            //UpdateReferenceDetails();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        ///// <summary>
        ///// Key press
        ///// </summary>
        ///// <param name="msg"></param>
        ///// <param name="KeyData"></param>
        ///// <returns></returns>
        //protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        //{
        //    if (KeyData == (Keys.Alt | Keys.D))
        //    {
        //        DeleteReference();
        //    }
        //    return base.ProcessCmdKey(ref msg, KeyData);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcReferenceNo_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && gvSubLedgerVouchers.FocusedColumn == colAmount && !e.Shift && !e.Alt && !e.Control)
            {
                if (SubLedgerId == 0 && Amount == 0 && gvSubLedgerVouchers.IsFirstRow) { this.Close(); }

                double dAmt = this.UtilityMember.NumberSet.ToDouble(LedgerAmount.ToString());

                //if (dAmt == TotalAmount)
                //{
                    if (gvSubLedgerVouchers.IsLastRow)
                    {
                        //UpdateSubLedgerVouchers();
                        btnOk.Focus();
                    }
                    else
                    {
                        //MoveNextRow();
                    }
                //}

                //if (dAmt != TotalAmount && Voucherid > 0 && TotalAmount > 0 && gvSubLedgerVouchers.IsLastRow)
                if (gvSubLedgerVouchers.IsLastRow)
                {
                    //GridNewItem = true;
                    if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                    {
                        e.SuppressKeyPress = true;
                    }
                }
                else
                {
                    //MoveNextRow();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ValidateSubLedgerVouchers())
            {
                UpdateSubLedgerVouchers();
                this.Close();
            }

            //bool isValid = true;
            //DataTable dtTemp = gcReferenceNo.DataSource as DataTable;
            //if (dtTemp.Rows.Count == 1)
            //{
            //    DataView dvTemp = dtTemp.DefaultView;
            //    dvTemp.RowFilter = "REF_VOUCHER_ID > 0 AND AMOUNT > 0";
            //    if (dvTemp.Count == 0)
            //    {
            //        isValid = false;
            //    }
            //}

            //if (isValid)
            //{
            //    if (ValidateReferenceEntries())
            //    {
            //        UpdateReferenceDetailsAndClose();
            //    }
            //}
            //else
            //{
            //    UpdateReferenceDetailsAndClose();
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbeDeleteReference_Click(object sender, EventArgs e)
        {
            DeleteSubLegetEntry();
        }

        /// <summary>
        /// When user gives amount, it should be less than or equal to balance reference amount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvReferenceNo_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            double amount = Convert.ToInt32(gvSubLedgerVouchers.GetRowCellValue(e.RowHandle, colAmount));
            //string referencenubmer = gvSubLedgerVouchers.GetRowCellDisplayText(e.RowHandle, colSubLedger).ToString();

            ////Amount, it should be less than or equal to balance reference amount
            //if (amount > refamount)
            //{
            //    string errormsg = string.Format(UtilityMember.NumberSet.ToNumber(amount) + " is not less than Reference Amount {1} for {2}.\n",
            //                        amount, UtilityMember.NumberSet.ToNumber(refamount), referencenubmer);

            //    this.ShowMessageBox(errormsg);
            //    //e.Valid = false;
            //    //e.ErrorText = errormsg;
            //    SetBalanceforCurrentRefNumber();
            //    gcSubLedgerVouchers.Select();
            //    gcSubLedgerVouchers.Focus();
            //    gvSubLedgerVouchers.SelectRow(e.RowHandle);
            //    gvSubLedgerVouchers.FocusedColumn = colAmount;
            //    gvSubLedgerVouchers.ShowEditor();
            //}
        }
        #endregion

        #region Methods

        ///// <summary>
        ///// This method is uued to bind emptry datasource
        ///// </summary>
        //private void BindEmptySource()
        //{
        //    using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
        //    {
        //        DataTable dtBind = new DataTable();
        //        dtBind.Columns.Add(vouchersystem.AppSchema.VoucherTransaction.REF_VOUCHER_IDColumn.ColumnName, typeof(Int32));
        //        dtBind.Columns.Add(vouchersystem.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName, typeof(decimal));
        //        dtBind.Columns.Add(vouchersystem.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName, typeof(Int32));
        //        dtBind.Columns.Add(vouchersystem.AppSchema.VoucherTransaction.REC_PAY_VOUCHER_IDColumn.ColumnName, typeof(Int32));
        //        dtBind.Columns.Add(vouchersystem.AppSchema.VoucherTransaction.REF_VALIDATION_AMOUNTColumn.ColumnName, typeof(decimal));
        //        AddNewRow(dtBind);
        //        gcSubLedgerVouchers.DataSource = dtBind;
        //    }
        //}
        
        /// <summary>
        /// This method is used to bind leger sub ledger voucher details
        /// </summary>
        private void BindVoucherLedgerSubLedgerVouchers()
        {
            try
            {
                using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                {
                    dtSubLedgerVouchers.DefaultView.RowFilter = vouchersystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName + " = " + LedgerId;
                    DataTable dtBind = dtSubLedgerVouchers.DefaultView.ToTable();

                    //Bind Sub Ledger Current Balance 
                    BindSubLegerCurrentBalance(dtBind);

                    //dtBind = AddNewRow(dtBind);
                    gcSubLedgerVouchers.DataSource = dtBind;
                    gvSubLedgerVouchers.FocusedColumn = colAmount;
                    gvSubLedgerVouchers.ShowEditor();
                    dtSubLedgerVouchers.DefaultView.RowFilter = string.Empty;
                }

                SetGridFocus();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// To Focus grid with first row and amount column
        /// </summary>
        private void SetGridFocus()
        {
            gcSubLedgerVouchers.Select();
            gvSubLedgerVouchers.MoveFirst();
            gvSubLedgerVouchers.FocusedColumn = colAmount;
            gvSubLedgerVouchers.ShowEditor();
        }

        /// <summary>
        /// This method is used to check all rows are valid
        /// </summary>
        /// <returns></returns>
        private bool ValidateSubLedgerVouchers()
        {
            bool isValid = true;
            
            return isValid;
        }
        
        /// <summary>
        /// This method is used to get sub ledger's amount and return it back to the transaction screen
        /// 
        /// 1. Clear current ledger's sub ledger vouchers details in the source of the datatable
        /// 2. Add new sub ledger vouchers for selected ledger in the source of the datatable
        /// </summary>
        private void UpdateSubLedgerVouchers()
        {
            try
            {
                DataTable dtLedgerSubLedgerVouchers= gcSubLedgerVouchers.DataSource as DataTable;
                if (dtLedgerSubLedgerVouchers != null && dtLedgerSubLedgerVouchers.Rows.Count > 0)
                {
                    using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                    {
                        //1. Remove existing ledger sub ledger voucher details for selected ledger
                        DataRow[] ledgerdetails = dtSubLedgerVouchers.Select(vouchersystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName + " = " + LedgerId);
                        if (ledgerdetails.Length > 0)
                        {
                            foreach (DataRow dr in ledgerdetails)
                            {
                                dtSubLedgerVouchers.Rows.Remove(dr);
                            }
                            dtSubLedgerVouchers.AcceptChanges();
                        }

                        //2. Add ledger's sub ledger voucher details
                        Int32 sequenceno = 1;
                        foreach (DataRow dr in dtLedgerSubLedgerVouchers.Rows)
                        {
                            Int32 subledgerid = UtilityMember.NumberSet.ToInteger(dr[vouchersystem.AppSchema.VoucherTransaction.SUB_LEDGER_IDColumn.ColumnName].ToString());
                            Int32 budgetid = UtilityMember.NumberSet.ToInteger(dr[vouchersystem.AppSchema.Budget.BUDGET_IDColumn.ColumnName].ToString());
                            string subledgername = dr[vouchersystem.AppSchema.VoucherTransaction.SUB_LEDGER_NAMEColumn.ColumnName].ToString();
                            double subledgerbudgetamount = UtilityMember.NumberSet.ToDouble(dr[vouchersystem.AppSchema.Budget.BUDGET_AMOUNTColumn.ColumnName].ToString());
                            double amt = UtilityMember.NumberSet.ToDouble(dr[vouchersystem.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString());
                            Int32 isbudget = UtilityMember.NumberSet.ToInteger(dr[vouchersystem.AppSchema.Budget.IS_BUDGETColumn.ColumnName].ToString());
                            string budgettransmode = dr[vouchersystem.AppSchema.Budget.BUDGET_TRANS_MODEColumn.ColumnName].ToString();
                            
                            if (subledgerid > 0)
                            {
                                DataRow drledgerreference = dtSubLedgerVouchers.NewRow();
                                drledgerreference[vouchersystem.AppSchema.VoucherTransaction.VOUCHER_IDColumn.ColumnName] = Voucherid;
                                drledgerreference[vouchersystem.AppSchema.VoucherTransaction.SEQUENCE_NOColumn.ColumnName] = sequenceno;
                                drledgerreference[vouchersystem.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName] = LedgerId;
                                drledgerreference[vouchersystem.AppSchema.VoucherTransaction.SUB_LEDGER_IDColumn.ColumnName] = subledgerid;
                                drledgerreference[vouchersystem.AppSchema.VoucherTransaction.SUB_LEDGER_NAMEColumn.ColumnName] = subledgername;
                                drledgerreference[vouchersystem.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName] = amt;

                                drledgerreference[vouchersystem.AppSchema.Budget.IS_BUDGETColumn.ColumnName] = isbudget;
                                drledgerreference[vouchersystem.AppSchema.Budget.BUDGET_IDColumn.ColumnName] = budgetid;
                                drledgerreference[vouchersystem.AppSchema.Budget.BUDGET_AMOUNTColumn.ColumnName] = subledgerbudgetamount;
                                drledgerreference[vouchersystem.AppSchema.Budget.BUDGET_TRANS_MODEColumn.ColumnName] = budgettransmode;
                                
                                dtSubLedgerVouchers.Rows.Add(drledgerreference);
                                sequenceno++;
                            }
                        }
                    }

                    this.DialogResult = DialogResult.OK;
                    this.ReturnValue = dtSubLedgerVouchers;
                    
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        
        /// <summary>
        /// Delete row in the grid
        /// </summary>
        public void DeleteSubLegetEntry()
        {
            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                gvSubLedgerVouchers.DeleteRow(gvSubLedgerVouchers.FocusedRowHandle);
                //if (gvSubLedgerVouchers.RowCount == 0)
                //{
                //    BindEmptySource();
                //}
                gvSubLedgerVouchers.MoveFirst();
                gvSubLedgerVouchers.FocusedColumn = colAmount;
                gvSubLedgerVouchers.ShowEditor();
            }
        }

        ///// <summary>
        ///// This method is used to get reference voucher Ids, which are already availbale in grid
        ///// </summary>
        ///// <param name="activerefid"></param>
        ///// <returns></returns>
        //private string GetSelectedReferenceNosInGrid(Int32 subledgerid)
        //{
        //    string rtn = string.Empty;
        //    try
        //    {
        //        DataTable dtRef = gcSubLedgerVouchers.DataSource as DataTable;
        //        foreach (DataRow dr in dtRef.Rows)
        //        {
        //            Int32 selectedsubledgerid = UtilityMember.NumberSet.ToInteger(dr["SUB_LEDGER_ID"].ToString());
        //            //activerefid > 0 && 
        //            if (subledgerid != selectedsubledgerid)
        //            {
        //                rtn += selectedsubledgerid.ToString() + ",";
        //            }
        //        }
        //        rtn = rtn.TrimEnd(',');
        //    }
        //    catch (Exception err)
        //    {
        //        rtn = string.Empty;
        //    }
        //    return rtn;
        //}

        ///// <summary>
        ///// This method is used to set selected ref number's balance in the grid column for validation
        ///// </summary>
        //private void SetBalanceforCurrentRefNumber()
        //{
        //    //Set selected referecne nubmer's balance amount in last column in the grid, it will use to validate when user gives amount
        //    gvSubLedgerVouchers.PostEditor();
        //    object value1 = 0;//; gvSubLedgerVouchers.GetFocusedRowCellValue(colValidationRefAmount);
        //    //gvReferenceNo.UpdateCurrentRow();

        //    object value = rglkpSubLedger.GetRowByKeyValue(Voucherid);
        //    DataRowView dr = value as DataRowView;

        //    if (dr != null)
        //    {
        //        double SelectedRefBalance = this.UtilityMember.NumberSet.ToDouble(dr[colcbBalance.FieldName].ToString());
        //        Int32 SelectedRefVoucherId = this.UtilityMember.NumberSet.ToInteger(dr[colSubLedger.FieldName].ToString());
        //        string SelectedRefVoucherDate = ""; //dr[colVoucherDate.FieldName].ToString();
        //        double ProposedAmount = Amount;

        //        if (SelectedRefBalance > 0)
        //        {
        //            if (Amount <= 0)
        //            {
        //                ProposedAmount = Math.Min(PendingAmount, Math.Min(SelectedRefBalance, LedgerAmount));
        //            }
        //            else
        //            {
        //                //ProposedAmount = Amount;
        //                ProposedAmount = Math.Min(SelectedRefBalance, Amount);
        //            }
        //            gvSubLedgerVouchers.SetRowCellValue(gvSubLedgerVouchers.FocusedRowHandle, colAmount, ProposedAmount);
        //            //gvSubLedgerVouchers.SetRowCellValue(gvSubLedgerVouchers.FocusedRowHandle, colValidationRefAmount, SelectedRefBalance);
        //            //gvSubLedgerVouchers.SetRowCellValue(gvSubLedgerVouchers.FocusedRowHandle, colVoucherDate, SelectedRefVoucherDate);
        //        }
        //        else
        //        {
        //            //gvSubLedgerVouchers.SetRowCellValue(gvSubLedgerVouchers.FocusedRowHandle, colValidationRefAmount, 0);
        //            gvSubLedgerVouchers.SetRowCellValue(gvSubLedgerVouchers.FocusedRowHandle, colAmount, 0);
        //            //gvSubLedgerVouchers.SetRowCellValue(gvSubLedgerVouchers.FocusedRowHandle, colVoucherDate, string.Empty);
        //        }
        //        gvSubLedgerVouchers.PostEditor();
        //        gvSubLedgerVouchers.UpdateCurrentRow();
        //        gvSubLedgerVouchers.ShowEditor();
        //    }
        //}

        /// <summary>
        /// This method is used to move next row in the grid
        /// </summary>
        private void MoveNextRow()
        {
            //if (Voucherid == 0)
            //{
            //    ValidateSubLedgerVouchers();
            //}

            gvSubLedgerVouchers.MoveNext();
            gvSubLedgerVouchers.FocusedColumn = colAmount;
            gvSubLedgerVouchers.ShowEditor();
        }

        /// <summary>
        /// On 11/02/2020, to bind Current balance
        /// </summary>
        /// <param name="dtSubLedgerTrans"></param>
        /// <returns></returns>
        private DataTable BindSubLegerCurrentBalance(DataTable dtSubLedgerTrans)
        {
            int ledgerId = 0;
            int subledgerId = 0;

            foreach (DataRow dr in dtSubLedgerTrans.Rows)
            {
                ledgerId = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                subledgerId = this.UtilityMember.NumberSet.ToInteger(dr["SUB_LEDGER_ID"].ToString());
                double subledgerBudgetAmount = this.UtilityMember.NumberSet.ToDouble(dr["BUDGET_AMOUNT"].ToString());
                double subledgercurrentBalance = 0;
                string subledgercurrentBalancetransmode = string.Empty;
                bool IsBudget = (this.UtilityMember.NumberSet.ToDouble(dr["IS_BUDGET"].ToString()) == 1);
                string datefrom = dr["DATE_FROM"].ToString();
                string dateto = dr["DATE_TO"].ToString();
                BudgetId = this.UtilityMember.NumberSet.ToInteger(dr["BUDGET_ID"].ToString());
                BudgetdateFrom = (string.IsNullOrEmpty(datefrom) ? (DateTime?)null : this.UtilityMember.DateSet.ToDate(datefrom, false));
                BudgetdateTo = (string.IsNullOrEmpty(datefrom) ? (DateTime?)null : this.UtilityMember.DateSet.ToDate(dateto, false));
                BudgetTransMode = dr["BUDGET_TRANS_MODE"].ToString();

                //Updtae For Sub Ledger Current Balance
                BalanceProperty subledgercurrentbalance = FetchCurrentSubLedgerBalance(ledgerId, subledgerId, BudgetdateFrom, BudgetdateTo);
                if (subledgercurrentbalance.Result.Success)
                {
                    subledgercurrentBalance = subledgercurrentbalance.Amount;
                    subledgercurrentBalancetransmode = subledgercurrentbalance.TransMode;
                    dr["SUB_LEDGER_BALANCE"] = this.UtilityMember.NumberSet.ToCurrency(subledgercurrentBalance) + " " + subledgercurrentbalance.TransMode;
                }

                //Updtae For Sub Ledger Budget Amount
                dr["BUDGET_VARIANCE"] = "";
                if (IsBudget)
                {
                    dr["BUDGET_VARIANCE"] = this.UtilityMember.NumberSet.ToNumber(subledgercurrentBalance) + " / " + this.UtilityMember.NumberSet.ToNumber(subledgerBudgetAmount) + " " + BudgetTransMode;
                }
            }

            return dtSubLedgerTrans;
        }

        /// <summary>
        /// On 11/02/2020, to get Sub Ledger Current Balance
        /// </summary>
        /// <param name="LedgerId"></param>
        /// <param name="SubLegerId"></param>
        /// <returns></returns>
        private BalanceProperty FetchCurrentSubLedgerBalance(int LedgerId, int SubLegerId, Nullable<DateTime> budgetDateFrom, Nullable<DateTime> budgetDateTo)
        {
            BalanceProperty balProperty;
            using (BalanceSystem balancesystem = new BalanceSystem())
            {
                balProperty = balancesystem.GetSubLedgereBalance(ProjectId.ToString(), LedgerId, SubLegerId, budgetDateFrom, budgetDateTo);
            }
            return balProperty;
        }


        /// <summary>
        /// To Calculate Sub Ledger Variance
        /// </summary>
        /// <param name="ledgerId"></param>
        /// <returns></returns>
        private string CalculateSubLedgerBudgetVariance()
        {
            ResultArgs result = new ResultArgs();
            string tmpMode = string.Empty;
            string value = string.Empty;
            string BalanceCurrentTransaction = string.Empty;

            double OldValue = 0;
            double NewValue = 0;
            double CalOldNewValue = 0;

            BalanceProperty CurrentAmount = FetchCurrentSubLedgerBalance(LedgerId, SubLedgerId, BudgetdateFrom, BudgetdateTo);
            DataTable dtTemp = gcSubLedgerVouchers.DataSource as DataTable;
            string NewValueMode = string.Empty;

            if (dtTemp != null)
            {
                NewValue = Amount;
                OldValue = TmpAmount;
                CalOldNewValue = (OldValue - NewValue);
            }

            double Total = CurrentAmount.Amount;
            string CurrentLedgerTransMode = CurrentAmount.TransMode;
            string ActualAmount = string.Empty;

            if (VType == DefaultVoucherTypes.Receipt)
            {
                if (CurrentLedgerTransMode == "CR")
                {
                    Total = Total + CalOldNewValue;
                }
            }
            else
            {
                if (CurrentLedgerTransMode == "CR" && Total != Amount)
                {
                    Total = Total > LedgerAmount ? Total - CalOldNewValue : CalOldNewValue - Total;
                }
                else if (CurrentLedgerTransMode == "DR" && Total == Amount)
                {
                    Total = Total > LedgerAmount ? Total - CalOldNewValue : CalOldNewValue - Total;
                }
                else if (CurrentLedgerTransMode == "DR" && Total != Amount)
                {
                    if (Voucherid == 0)
                    {
                        Total = Total = Total + Amount;
                    }
                    else
                    {
                        Total = Total = Total + (OldValue + NewValue);
                    }
                }
                else
                {
                    Total = Total > LedgerAmount ? Total - CalOldNewValue : CalOldNewValue - Total;
                }
            }

            if (Total != 0)
            {
                ActualAmount = Math.Abs(this.UtilityMember.NumberSet.ToDouble(Total.ToString())).ToString();
            }
            else
            {
                ActualAmount = "0.00";
            }
            value = this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(ActualAmount.ToString())) + " / " + UtilityMember.NumberSet.ToNumber(BudgetAmount) + " " + BudgetTransMode;
            return value;
        }
        #endregion

        private void rtxtAmount_Leave(object sender, EventArgs e)
        {
            if (this.BudgetId > 0)
            {
                if (SubLedgerId>0 && Amount>0)
                {
                    //BalanceProperty dAmt = FetchBudgetAmount(LedgerId);
                    if (BudgetAmount > 0 && BudgetAmount != 0)
                    {
                        //BalanceProperty Balance = FetchBudgetLedgerBalance(LedgerId);
                        //double amt = Balance.Amount;

                        double OldValue = 0;
                        double NewValue = 0;
                        double finalCurrentBalance = 0;
                        double BothOldNewValue = 0;
                        DataTable dtTrans = gcSubLedgerVouchers.DataSource as DataTable;

                        if (dtTrans != null)
                        {
                            NewValue = Amount;
                            OldValue = TmpAmount;
                            BalanceProperty CurrentSubLedgerBalance= FetchCurrentSubLedgerBalance(LedgerId, SubLedgerId, BudgetdateFrom, BudgetdateTo);
                            finalCurrentBalance = CurrentSubLedgerBalance.Amount;
                            double CurrentBudgetBalance = Math.Abs(OldValue - NewValue);

                            BothOldNewValue = finalCurrentBalance;
                        }

                        if (BudgetAmount < BothOldNewValue)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.TRANSACTION_AMOUNT_EXCEEDS));
                            gvSubLedgerVouchers.FocusedColumn = colAmount;
                        }
                    }
                }
            }
        }
    }
}
