/*
 * Date: 20/10/2017
 * Purpose : This form is used to get Jouranl Reference numbers for Payment Voucher ledger amount. This form will be shown only for Sundry Creditors/Sundry Creditors 
 * Ledger Amount could be splitted in many ref numbers still ledger amount comes to zero
 * 
 * Pending payments (biils) for party (Sundry Creditors/Sundry Creditors) are booked as Jounral voucher with unique reference numbers, When we pay to party, payment amount
 * should be refered to any one or more Journal booked voucher ref numbers
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

namespace ACPP.Modules.Transaction
{
    public partial class frmTransactionReference : frmFinanceBaseAdd
    {
        #region Variable Declaration
        ResultArgs resultArgs = null;
        #endregion

        #region Properites
        private int ProjectId = 0;
        private int RecPayVoucherid;
        private int LedgerId = 0;
        private double LedgerAmount = 0;
        private DateTime VoucherRecPayDate;

        //current row  ref voucher id
        private int referencevoucherid;
        private int ReferenceVoucherId
        {
            set
            {
                referencevoucherid = value;
            }
            get
            {
                referencevoucherid = gvReferenceNo.GetFocusedRowCellValue(colRefNumber) != null ? this.UtilityMember.NumberSet.ToInteger(gvReferenceNo.GetFocusedRowCellValue(colRefNumber).ToString()) : 0;
                return referencevoucherid;
            }
        }

        //current row  ref voucher id
        private double amount;
        private double Amount
        {
            set
            {
                amount = value;
            }
            get
            {
                amount = gvReferenceNo.GetFocusedRowCellValue(colAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvReferenceNo.GetFocusedRowCellValue(colAmount).ToString()) : 0;
                return amount;
            }
        }

        //total refered amount in grid
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

        private double PendingAmount
        {
            get
            {
                double rtn = 0;
                rtn = (LedgerAmount - TotalAmount);
                if (rtn < 0)
                {
                    rtn = 0;
                }
                return rtn;
            }

        }

        private DataTable dtVoucherLedgerReferenceDetails = new DataTable();
        public DataTable VoucherLedgerReferenceDetails
        {
            set { dtVoucherLedgerReferenceDetails = value; }
            get { return dtVoucherLedgerReferenceDetails; }

        }

        private DataTable dtValidate = new DataTable();

        //Focus to new row
        private bool GridNewItem
        {
            set
            {
                if (value)
                {
                    DataTable dtLedgerReference = gcReferenceNo.DataSource as DataTable;
                    AddNewRow(dtLedgerReference);
                    gcReferenceNo.DataSource = dtLedgerReference;
                    gvReferenceNo.MoveNext();
                    gvReferenceNo.FocusedColumn = colRefNumber;
                    gvReferenceNo.ShowEditor();
                }
            }
        }
        #endregion

        #region Constructor
        public frmTransactionReference()
        {
            InitializeComponent();
            this.PageTitle = "Payment Reference";

            //Attach amount chage event
            RealColumnEditTransAmount();
        }

        public frmTransactionReference(int projectid, int recpayvoucherid, int ledgerId, DateTime voucherRecPayDate,
                    double ledgeramount, string LedgerName, DataTable dtvoucerreferencedetails)
            : this()
        {
            ProjectId = projectid;
            RecPayVoucherid = recpayvoucherid;
            LedgerId = ledgerId;
            LedgerAmount = ledgeramount;
            VoucherRecPayDate = voucherRecPayDate;
            VoucherLedgerReferenceDetails = dtvoucerreferencedetails;
            lblLedgerAmount.Text = this.UtilityMember.NumberSet.ToCurrency(ledgeramount) + "Dr";
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
            gvReferenceNo.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            BindVoucherLedgerReferenceDetails();
            BindGridLKPReferenceBalances();
            //AssignValues();
            colValidationRefAmount.Visible = false;
            //rtxtAmount.NullText = "0.0";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmTransactionCostCenter_Shown(object sender, EventArgs e)
        {
            gcReferenceNo.Select();
            gvReferenceNo.FocusedRowHandle = DevExpress.XtraGrid.GridControl.NewItemRowHandle;
            gvReferenceNo.FocusedColumn = gvReferenceNo.VisibleColumns[0];
            gvReferenceNo.ShowEditor();

        }

        /// <summary>
        /// Attach Event for amount changed
        /// </summary>
        private void RealColumnEditTransAmount()
        {
            colAmount.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditTransAmount_EditValueChanged);
            this.gvReferenceNo.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvReferenceNo.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colAmount)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvReferenceNo.ShowEditorByMouse();
                    }));
                }
            };
        }

        /// <summary>--
        /// 
        /// </summary>
        private void AssignValues()
        {
            if (VoucherLedgerReferenceDetails != null && VoucherLedgerReferenceDetails.Rows.Count > 0)
            {
                gcReferenceNo.DataSource = VoucherLedgerReferenceDetails;
            }
        }

        /// <summary>
        /// Event for amount changed
        /// </summary>
        private void RealColumnEditTransAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvReferenceNo.PostEditor();
            gvReferenceNo.UpdateCurrentRow();
            if (gvReferenceNo.ActiveEditor == null)
            {
                gvReferenceNo.ShowEditor();
            }

            TextEdit txtTransAmount = edit as TextEdit;
            int grpCounts = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.DecimalPlaces) + 1);
            if (txtTransAmount.Text.Length > grpCounts && txtTransAmount.SelectionLength == txtTransAmount.Text.Length)
                txtTransAmount.Select(txtTransAmount.Text.Length - grpCounts, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvReferenceNo_ShownEditor(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view;
            view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view.FocusedColumn == colRefNumber && view.ActiveEditor is DevExpress.XtraEditors.GridLookUpEdit)
            {
                string selectedrefs = GetSelectedReferenceNosInGrid(ReferenceVoucherId);
                DevExpress.XtraEditors.GridLookUpEdit edit;
                edit = (DevExpress.XtraEditors.GridLookUpEdit)view.ActiveEditor;

                DataTable table = (DataTable)edit.Properties.DataSource; ;
                DataView clone = new DataView(table);
                /*ArrayList values = new ArrayList();
                for (int i = 0; i < view.DataRowCount; i++)
                {
                    DataRow row = view.GetDataRow(i);
                    string val = row["FactId"].ToString();
                    if (!values.Contains(val))
                        values.Add(val);
                }
                string ids = string.Empty;
                foreach (string str in values)
                    ids += string.Format("{0},", str);
                ids = ids.TrimEnd(',');*/
                using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                {
                    string selected = GetSelectedReferenceNosInGrid(ReferenceVoucherId);
                    clone.RowFilter = string.Empty;
                    if (!String.IsNullOrEmpty(selected))
                    {
                        clone.RowFilter = vouchersystem.AppSchema.VoucherTransaction.REF_VOUCHER_IDColumn.ColumnName + " NOT IN (" + selected + ")";
                    }

                    DataTable dtbind = clone.ToTable();
                    edit.Properties.DataSource = dtbind;
                }
            }
            else if (view.FocusedColumn == colAmount)
            {
                //var editor = (TextEdit)gvReferenceNo.ActiveEditor;
                //editor.SelectionLength = 0;
                //editor.SelectionStart = editor.Text.Length;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rtxtAmount_Enter(object sender, EventArgs e)
        {
            SetBalanceforCurrentRefNumber();
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
                ValidateReferenceEntries();
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

        /// <summary>
        /// Key press
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="KeyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.Alt | Keys.D))
            {
                DeleteReference();
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rglkpRefNumber_EditValueChanged(object sender, EventArgs e)
        {
            //Set selected referecne nubmer's balance amount in last column in the grid, it will use to validate when user gives amount
            SetBalanceforCurrentRefNumber();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcReferenceNo_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && gvReferenceNo.FocusedColumn == colAmount && !e.Shift && !e.Alt && !e.Control)
            {
                if (ReferenceVoucherId == 0 && Amount == 0 && gvReferenceNo.IsFirstRow) { this.Close(); }

                double dAmt = this.UtilityMember.NumberSet.ToDouble(LedgerAmount.ToString());

                if (dAmt == TotalAmount)
                {
                    if (gvReferenceNo.IsLastRow)
                    {
                        UpdateReferenceDetails();
                        btnOk.Focus();
                    }
                    else
                    {
                        MoveNextRow();
                    }
                }

                if (dAmt != TotalAmount && ReferenceVoucherId > 0 && TotalAmount > 0 && gvReferenceNo.IsLastRow)
                {
                    GridNewItem = true;
                    if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                    {
                        e.SuppressKeyPress = true;
                    }
                }
                else
                {
                    MoveNextRow();
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
            if (ValidateReferenceEntries())
            {
                UpdateReferenceDetails();
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
            DeleteReference();
        }

        /// <summary>
        /// When user gives amount, it should be less than or equal to balance reference amount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvReferenceNo_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            double amount = Convert.ToInt32(gvReferenceNo.GetRowCellValue(e.RowHandle, colAmount));
            double refamount = Convert.ToInt32(gvReferenceNo.GetRowCellValue(e.RowHandle, colValidationRefAmount));
            string referencenubmer = gvReferenceNo.GetRowCellDisplayText(e.RowHandle, colRefNumber).ToString();

            //Amount, it should be less than or equal to balance reference amount
            if (amount > refamount)
            {
                string errormsg = string.Format(UtilityMember.NumberSet.ToNumber(amount) + " is not less than Reference Amount {1} for {2}.\n",
                                    amount, UtilityMember.NumberSet.ToNumber(refamount), referencenubmer);

                this.ShowMessageBox(errormsg);
                //e.Valid = false;
                //e.ErrorText = errormsg;
                SetBalanceforCurrentRefNumber();
                gcReferenceNo.Select();
                gcReferenceNo.Focus();
                gvReferenceNo.SelectRow(e.RowHandle);
                gvReferenceNo.FocusedColumn = colAmount;
                gvReferenceNo.ShowEditor();
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// This method is uued to bind emptry datasource
        /// </summary>
        private void BindEmptySource()
        {
            using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
            {
                DataTable dtBind = new DataTable();
                dtBind.Columns.Add(vouchersystem.AppSchema.VoucherTransaction.REF_VOUCHER_IDColumn.ColumnName, typeof(Int32));
                dtBind.Columns.Add(vouchersystem.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName, typeof(decimal));
                dtBind.Columns.Add(vouchersystem.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName, typeof(Int32));
                dtBind.Columns.Add(vouchersystem.AppSchema.VoucherTransaction.REC_PAY_VOUCHER_IDColumn.ColumnName, typeof(Int32));
                dtBind.Columns.Add(vouchersystem.AppSchema.VoucherTransaction.REF_VALIDATION_AMOUNTColumn.ColumnName, typeof(decimal));
                AddNewRow(dtBind);
                gcReferenceNo.DataSource = dtBind;
            }
        }

        /// <summary>
        /// This method is used to get balances refernce nubmers which are avilable in journal booking
        /// for particular ledger
        /// </summary>
        private void BindGridLKPReferenceBalances()
        {
            try
            {
                using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                {
                    resultArgs = vouchersystem.FetchReferenceBalances(ProjectId, RecPayVoucherid, LedgerId, VoucherRecPayDate);
                    rglkpRefNumber.DataSource = null;
                    if (resultArgs.Success)
                    {
                        rglkpRefNumber.DataSource = resultArgs.DataSource.Table;
                        rglkpRefNumber.DisplayMember = vouchersystem.AppSchema.VoucherTransaction.REFERENCE_NUMBERColumn.ToString();
                        rglkpRefNumber.ValueMember = vouchersystem.AppSchema.VoucherTransaction.REF_VOUCHER_IDColumn.ToString();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        /// <summary>
        /// This method is used to bind leger reference details
        /// </summary>
        private void BindVoucherLedgerReferenceDetails()
        {
            try
            {
                /*using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                {
                    resultArgs = vouchersystem.FetchVoucherLedgerReferenceDetails(ProjectId, VoucherId, LedgerId);
                    if (resultArgs.Success)
                    {
                        DataTable dtBind = resultArgs.DataSource.Table;
                        AddNewRow(dtBind); 
                        gcReferenceNo.DataSource = dtBind;
                        gvReferenceNo.FocusedColumn = colRefNumber;
                        gvReferenceNo.ShowEditor();
                    } 
                }*/
                using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                {
                    dtVoucherLedgerReferenceDetails.DefaultView.RowFilter = vouchersystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName + " = " + LedgerId;
                    DataTable dtBind = dtVoucherLedgerReferenceDetails.DefaultView.ToTable();

                    dtBind = AddNewRow(dtBind);
                    gcReferenceNo.DataSource = dtBind;
                    gvReferenceNo.FocusedColumn = colRefNumber;
                    gvReferenceNo.ShowEditor();
                    dtVoucherLedgerReferenceDetails.DefaultView.RowFilter = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// To Focus grid with first row and reference column
        /// </summary>
        private void SetGridFocus()
        {
            gcReferenceNo.Select();
            gvReferenceNo.MoveFirst();
            gvReferenceNo.FocusedColumn = colRefNumber;
            gvReferenceNo.ShowEditor();
        }

        /// <summary>
        /// This method is ued to check all rows are valid
        /// 1. Check all rows should have reference voucher id and amount
        /// 2. Sum of refered amount should be equal to Ledger AMount
        /// </summary>
        /// <returns></returns>
        private bool ValidateReferenceEntries()
        {
            bool isValid = true;
            if (TotalAmount > 0)
            {
                if (!IsValidRows())
                {
                    isValid = false;
                }
                else if (LedgerAmount < TotalAmount)
                {
                    isValid = false;
                    this.ShowMessageBox("Referred Amount is greater than the Ledger Amount, Delete all rows or Refer Ledger amount fully.");
                    SetGridFocus();
                }
                else if (LedgerAmount > TotalAmount)
                {
                    isValid = false;
                    this.ShowMessageBox("Referred Amount is less than the Ledger Amount, Delete all rows or Refer Ledger amount fully.");
                    SetGridFocus();
                }
            }
            return isValid;
        }

        /// <summary>
        /// This method is used to check all rows should have reference voucher id and amount
        /// and foucs to concern rows
        /// </summary>
        /// <returns></returns>
        private bool IsValidRows()
        {
            DataTable dt = gcReferenceNo.DataSource as DataTable;

            int Id = 0;
            decimal Amt = 0;
            int RowPosition = 0;
            bool isValid = false;
            string validateMessage = this.GetMessage(MessageCatalog.Master.Transaction.TRANS_COSTCENTER_VALIDATION_MESSAGE);
            if (dt != null)
            {
                using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                {
                    DataView dv = new DataView(dt);
                    dv.RowFilter = "(" + vouchersystem.AppSchema.VoucherTransaction.REF_VOUCHER_IDColumn.ColumnName + " > 0 OR "
                                                + vouchersystem.AppSchema.VoucherTransaction.AMOUNTColumn + " > 0)";
                    gvReferenceNo.FocusedColumn = colRefNumber;
                    if (dv.Count > 0)
                    {
                        isValid = true;
                        foreach (DataRowView drTrans in dv)
                        {
                            Id = this.UtilityMember.NumberSet.ToInteger(drTrans["REF_VOUCHER_ID"].ToString());
                            Amt = this.UtilityMember.NumberSet.ToDecimal(drTrans["AMOUNT"].ToString());

                            if ((Id == 0 || Amt == 0))
                            {
                                if (Id == 0)
                                {
                                    //validateMessage = "Required Information not filled, Cost Centre Name is empty";
                                    validateMessage = this.GetMessage(MessageCatalog.Master.Transaction.TRANS_COSTCENTER_NAME_EMPTY);
                                    gvReferenceNo.FocusedColumn = colRefNumber;
                                }
                                if (Amt == 0)
                                {
                                    //validateMessage = "Required Information not filled, Amount is empty";
                                    validateMessage = this.GetMessage(MessageCatalog.Master.Transaction.TRANS_COSTCENTER_AMOUNT_EMPTY);
                                    gvReferenceNo.FocusedColumn = colAmount;
                                }
                                isValid = false;
                                break;
                            }
                            RowPosition = RowPosition + 1;
                        }
                    }
                    else
                    {
                        isValid = true;
                    }
                }
                if (!isValid)
                {
                    this.ShowMessageBox(validateMessage);
                    gvReferenceNo.CloseEditor();
                    gvReferenceNo.FocusedRowHandle = gvReferenceNo.GetRowHandle(RowPosition);
                    gvReferenceNo.ShowEditor();
                }
            }

            return isValid;
        }

        /// <summary>
        /// This method is used to get refered amount and return it back to the transaction screen
        /// 
        /// 1. Clear current ledger details in the source of the datatable
        /// 2. Add new reference details for current ledger in the source of the datatable
        /// </summary>
        private void UpdateReferenceDetails()
        {
            try
            {
                DataTable dtLedgerReferecne = gcReferenceNo.DataSource as DataTable;
                if (dtLedgerReferecne != null && dtLedgerReferecne.Rows.Count > 0)
                {
                    this.DialogResult = DialogResult.OK;
                    using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                    {
                        //1. Remove existing ledger referce details
                        DataRow[] ledgerdetails = dtVoucherLedgerReferenceDetails.Select(vouchersystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName + " = " + LedgerId);
                        if (ledgerdetails.Length > 0)
                        {
                            foreach (DataRow dr in ledgerdetails)
                            {
                                dtVoucherLedgerReferenceDetails.Rows.Remove(dr);
                            }
                        }

                        //2. Add ledger details

                        foreach (DataRow dr in dtLedgerReferecne.Rows)
                        {
                            Int32 refvouhcerid = UtilityMember.NumberSet.ToInteger(dr[vouchersystem.AppSchema.VoucherTransaction.REF_VOUCHER_IDColumn.ColumnName].ToString());
                            double amt = UtilityMember.NumberSet.ToDouble(dr[vouchersystem.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString());
                            double validationamt = UtilityMember.NumberSet.ToDouble(dr[vouchersystem.AppSchema.VoucherTransaction.REF_VALIDATION_AMOUNTColumn.ColumnName].ToString());
                            DateTime RefVoucherDate = dr[vouchersystem.AppSchema.VoucherReference.REF_VOUCHER_DATEColumn.ColumnName] != DBNull.Value ? UtilityMember.DateSet.ToDate(dr[vouchersystem.AppSchema.VoucherReference.REF_VOUCHER_DATEColumn.ColumnName].ToString(), false) : DateTime.MinValue;
                            if (refvouhcerid > 0 && amt > 0)
                            {
                                DataRow drledgerreference = dtVoucherLedgerReferenceDetails.NewRow();
                                drledgerreference[vouchersystem.AppSchema.VoucherTransaction.REF_VOUCHER_IDColumn.ColumnName] = refvouhcerid;
                                drledgerreference[vouchersystem.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName] = amt;
                                drledgerreference[vouchersystem.AppSchema.VoucherTransaction.REF_VALIDATION_AMOUNTColumn.ColumnName] = validationamt;
                                drledgerreference[vouchersystem.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName] = LedgerId;
                                drledgerreference[vouchersystem.AppSchema.VoucherTransaction.REC_PAY_VOUCHER_IDColumn.ColumnName] = RecPayVoucherid;
                                drledgerreference[vouchersystem.AppSchema.VoucherReference.REF_VOUCHER_DATEColumn.ColumnName] = RefVoucherDate;

                                dtVoucherLedgerReferenceDetails.Rows.Add(drledgerreference);
                            }
                        }
                    }
                    this.ReturnValue = dtVoucherLedgerReferenceDetails;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// Add Empty row 
        /// </summary>
        /// <param name="dtSource"></param>
        /// <returns></returns>
        private DataTable AddNewRow(DataTable dtSource)
        {
            using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
            {
                DataRow dr = dtSource.NewRow();
                dr[vouchersystem.AppSchema.VoucherTransaction.REF_VOUCHER_IDColumn.ColumnName] = 0;
                dr[vouchersystem.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName] = 0;
                dr[vouchersystem.AppSchema.VoucherTransaction.REF_VALIDATION_AMOUNTColumn.ColumnName] = 0;
                dr[vouchersystem.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName] = LedgerId;
                dr[vouchersystem.AppSchema.VoucherTransaction.REC_PAY_VOUCHER_IDColumn.ColumnName] = RecPayVoucherid;
                dtSource.Rows.Add(dr);
            }
            return dtSource;
        }

        /// <summary>
        /// Delete row in the grid
        /// </summary>
        public void DeleteReference()
        {
            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                gvReferenceNo.DeleteRow(gvReferenceNo.FocusedRowHandle);
                if (gvReferenceNo.RowCount == 0)
                {
                    BindEmptySource();
                }
                gvReferenceNo.MoveFirst();
                gvReferenceNo.FocusedColumn = colRefNumber;
                gvReferenceNo.ShowEditor();

            }
        }

        /// <summary>
        /// This method is used to get reference voucher Ids, which are already availbale in grid
        /// </summary>
        /// <param name="activerefid"></param>
        /// <returns></returns>
        private string GetSelectedReferenceNosInGrid(Int32 activerefid)
        {
            string rtn = string.Empty;
            try
            {
                DataTable dtRef = gcReferenceNo.DataSource as DataTable;
                foreach (DataRow dr in dtRef.Rows)
                {
                    Int32 selectedrefid = UtilityMember.NumberSet.ToInteger(dr["REF_VOUCHER_ID"].ToString());
                    //activerefid > 0 && 
                    if (activerefid != selectedrefid)
                    {
                        rtn += selectedrefid.ToString() + ",";
                    }
                }
                rtn = rtn.TrimEnd(',');
            }
            catch (Exception err)
            {
                rtn = string.Empty;
            }
            return rtn;
        }

        /// <summary>
        /// This method is used to set selected ref number's balance in the grid column for validation
        /// </summary>
        private void SetBalanceforCurrentRefNumber()
        {
            //Set selected referecne nubmer's balance amount in last column in the grid, it will use to validate when user gives amount
            gvReferenceNo.PostEditor();
            object value1 = gvReferenceNo.GetFocusedRowCellValue(colValidationRefAmount);
            //gvReferenceNo.UpdateCurrentRow();

            object value = rglkpRefNumber.GetRowByKeyValue(ReferenceVoucherId);
            DataRowView dr = value as DataRowView;

            if (dr != null)
            {
                double SelectedRefBalance = this.UtilityMember.NumberSet.ToDouble(dr[colcbBalance.FieldName].ToString());
                Int32 SelectedRefVoucherId = this.UtilityMember.NumberSet.ToInteger(dr[colRefNumber.FieldName].ToString());
                string SelectedRefVoucherDate = dr[colVoucherDate.FieldName].ToString();
                double ProposedAmount = Amount;

                if (SelectedRefBalance > 0)
                {
                    if (Amount <= 0)
                    {
                        ProposedAmount = Math.Min(PendingAmount, Math.Min(SelectedRefBalance, LedgerAmount));
                    }
                    else
                    {
                        //ProposedAmount = Amount;
                        ProposedAmount = Math.Min(SelectedRefBalance, Amount);
                    }
                    gvReferenceNo.SetRowCellValue(gvReferenceNo.FocusedRowHandle, colAmount, ProposedAmount);
                    gvReferenceNo.SetRowCellValue(gvReferenceNo.FocusedRowHandle, colValidationRefAmount, SelectedRefBalance);
                    gvReferenceNo.SetRowCellValue(gvReferenceNo.FocusedRowHandle, colVoucherDate, SelectedRefVoucherDate);
                }
                else
                {
                    gvReferenceNo.SetRowCellValue(gvReferenceNo.FocusedRowHandle, colValidationRefAmount, 0);
                    gvReferenceNo.SetRowCellValue(gvReferenceNo.FocusedRowHandle, colAmount, 0);
                    gvReferenceNo.SetRowCellValue(gvReferenceNo.FocusedRowHandle, colVoucherDate, string.Empty);
                }
                gvReferenceNo.PostEditor();
                gvReferenceNo.UpdateCurrentRow();
                gvReferenceNo.ShowEditor();
            }
        }

        /// <summary>
        /// This method is used to move next row in the grid
        /// </summary>
        private void MoveNextRow()
        {
            if (ReferenceVoucherId == 0)
            {
                ValidateReferenceEntries();
            }

            gvReferenceNo.MoveNext();
            gvReferenceNo.FocusedColumn = colRecPayVoucherId;
            gvReferenceNo.ShowEditor();
        }
        #endregion

        private void rtxtAmount_Validating(object sender, CancelEventArgs e)
        {

        }

        private void gvReferenceNo_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {

        }

    }
}



