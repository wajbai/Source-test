using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Utility;
using Bosco.Model.UIModel.Master;
using Bosco.Model.Transaction;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace ACPP.Modules.Master
{
    public partial class frmBudgetStatisticsDetail : frmFinanceBaseAdd
    {
        private Int32 BudgetId = 0;

        private DataTable dtBudgetStatisUpdateDetails = new DataTable();
        public DataTable BudgetStatisticsDetails
        {
            set { dtBudgetStatisUpdateDetails = value; }
            get { return dtBudgetStatisUpdateDetails; }

        }
        //current row statitiscs id
        private Int32 statisticstypeid = 0;
        private Int32 StatisticsTypeId
        {
            set
            {
                statisticstypeid = value;
            }
            get
            {
                statisticstypeid = gvBudgetStatisticsDetail.GetFocusedRowCellValue(colStatisticsType) != null ? this.UtilityMember.NumberSet.ToInteger(gvBudgetStatisticsDetail.GetFocusedRowCellValue(colStatisticsType).ToString()) : 0;
                return statisticstypeid;
            }
        }

        //current row amount
        private Int32 amount;
        private Int32 Amount
        {
            set
            {
                amount = value;
            }
            get
            {
                amount = gvBudgetStatisticsDetail.GetFocusedRowCellValue(colCount) != null ? this.UtilityMember.NumberSet.ToInteger(gvBudgetStatisticsDetail.GetFocusedRowCellValue(colCount).ToString()) : 0;
                return amount;
            }
        }

        //Focus to new row
        private bool GridNewItem
        {
            set
            {
                if (value)
                {
                    DataTable dtLedgerReference = gcBudgetStatisticsDetail.DataSource as DataTable;
                    AddNewRow(dtLedgerReference);
                    gcBudgetStatisticsDetail.DataSource = dtLedgerReference;
                    gvBudgetStatisticsDetail.MoveNext();
                    gvBudgetStatisticsDetail.FocusedColumn = colStatisticsType;
                    gvBudgetStatisticsDetail.ShowEditor();
                }
            }
        }

        public frmBudgetStatisticsDetail()
        {
            InitializeComponent();
            this.PageTitle = "Budget Statistics Detail";

            BindEmptySource();
            //Attach amount chage event
            RealColumnEditTotalCount();
        }

        public frmBudgetStatisticsDetail(int budgetid, DataTable dtbudgetstatisticsdetails)
            : this()
        {
            BudgetId = budgetid;
            BudgetStatisticsDetails = dtbudgetstatisticsdetails;
            gvBudgetStatisticsDetail.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }

        private void frmBudgetStatisticsDetail_Load(object sender, EventArgs e)
        {
            gvBudgetStatisticsDetail.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            BindGridLKPStatisticsType();
            BindBudgetStatisticsDetail();    
        }

        private void frmBudgetStatisticsDetail_Shown(object sender, EventArgs e)
        {
            gcBudgetStatisticsDetail.Select();
            gvBudgetStatisticsDetail.FocusedRowHandle = DevExpress.XtraGrid.GridControl.NewItemRowHandle;
            gvBudgetStatisticsDetail.FocusedColumn = gvBudgetStatisticsDetail.VisibleColumns[0];
            gvBudgetStatisticsDetail.ShowEditor();
        }

        /// <summary>
        /// Skip already selected statistic type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvBudgetStatisticsDetail_ShownEditor(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view;
            view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view.FocusedColumn == colStatisticsType && view.ActiveEditor is DevExpress.XtraEditors.GridLookUpEdit)
            {
                string selectedrefs = GetSelectedReferenceNosInGrid(StatisticsTypeId);
                DevExpress.XtraEditors.GridLookUpEdit edit;
                edit = (DevExpress.XtraEditors.GridLookUpEdit)view.ActiveEditor;

                DataTable table = (DataTable)edit.Properties.DataSource; ;
                DataView clone = new DataView(table);
                using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                {
                    string selected = GetSelectedReferenceNosInGrid(StatisticsTypeId);
                    clone.RowFilter = string.Empty;
                    if (!String.IsNullOrEmpty(selected))
                    {
                        clone.RowFilter = vouchersystem.AppSchema.BudgetStatistics.STATISTICS_TYPE_IDColumn.ColumnName + " NOT IN (" + selected + ")";
                    }

                    DataTable dtbind = clone.ToTable();
                    edit.Properties.DataSource = dtbind;
                    gvBudgetStatisticsDetail.ShowEditor();
                    return;
                }
            }
            else if (view.FocusedColumn == colCount)
            {
                //var editor = (TextEdit)gvBudgetStatisticsDetail.ActiveEditor;
                //editor.SelectionLength = 0;
                //editor.SelectionStart = editor.Text.Length;
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
            gvBudgetStatisticsDetail.PostEditor();
            gvBudgetStatisticsDetail.UpdateCurrentRow();
            if (gvBudgetStatisticsDetail.ActiveEditor == null)
            {
                gvBudgetStatisticsDetail.ShowEditor();
            }

            TextEdit txtTotalCount = edit as TextEdit;
            int grpCounts = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.DecimalPlaces) + 1);
            if (txtTotalCount.Text.Length > grpCounts && txtTotalCount.SelectionLength == txtTotalCount.Text.Length)
                txtTotalCount.Select(txtTotalCount.Text.Length - grpCounts, 0);
        }

        private void gcBudgetStatisticsDetail_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && gvBudgetStatisticsDetail.FocusedColumn == colCount && !e.Shift && !e.Alt && !e.Control)
            {
                //if (StatisticsTypeId == 0 && Amount == 0 && gvBudgetStatisticsDetail.IsFirstRow)
                //{
                //    UpdateBudgetDetails();
                //    btnOk.Focus();
                //}
                //else if (StatisticsTypeId == 0 && Amount == 0 && gvBudgetStatisticsDetail.IsLastRow)
                //{
                //    UpdateBudgetDetails();
                //    btnOk.Focus();
                //}
                if (StatisticsTypeId == 0 && Amount == 0 && gvBudgetStatisticsDetail.IsLastRow)
                {
                    btnOk.Select();
                    btnOk.Focus();
                }
                else if (StatisticsTypeId > 0 && Amount > 0 && gvBudgetStatisticsDetail.IsLastRow)
                {
                    if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                    {
                        GridNewItem = true;
                        e.SuppressKeyPress = true;
                    }
                }
                else
                {
                    if (IsValidRows())
                    {
                        MoveNextRow();
                        e.SuppressKeyPress = true;
                        //gvBudgetStatisticsDetail.FocusedColumn = colStatisticsType;
                        //gvBudgetStatisticsDetail.ShowEditor();
                    }
                    else
                    {
                        e.SuppressKeyPress = true;
                    }
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (IsValidRows())
            {
                UpdateBudgetDetails();
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbeBudgetStatistcsDetails_Click(object sender, EventArgs e)
        {
            DeleteBudgetStatisticsDetails();
        }

        private void gvBudgetStatisticsDetail_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            gvBudgetStatisticsDetail.PostEditor();
            Int32 satisticstypeid = Convert.ToInt32(gvBudgetStatisticsDetail.GetRowCellValue(e.RowHandle, colStatisticsType));
            Int32 amount = Convert.ToInt32(gvBudgetStatisticsDetail.GetRowCellValue(e.RowHandle, colCount));

            //Amount, it should be less than or equal to balance reference amount
            if (satisticstypeid > 0 && amount == 0)
            {

                this.ShowMessageBox("Total is empty");
                //e.Valid = false;
                //e.ErrorText = errormsg;
                gcBudgetStatisticsDetail.Select();
                gcBudgetStatisticsDetail.Focus();
                gvBudgetStatisticsDetail.SelectRow(e.RowHandle);
                gvBudgetStatisticsDetail.FocusedColumn = colCount;
                gvBudgetStatisticsDetail.ShowEditor();
            }
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
                DeleteBudgetStatisticsDetails();
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        /// <summary>
        /// This method is uued to bind emptry datasource
        /// </summary>
        private void BindEmptySource()
        {
            using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
            {
                DataTable dtBind = new DataTable();
                dtBind.Columns.Add(vouchersystem.AppSchema.BudgetStatistics.BUDGET_IDColumn.ColumnName, typeof(Int32));
                dtBind.Columns.Add(vouchersystem.AppSchema.BudgetStatistics.STATISTICS_TYPE_IDColumn.ColumnName, typeof(Int32));
                dtBind.Columns.Add(vouchersystem.AppSchema.BudgetStatistics.TOTAL_COUNTColumn.ColumnName, typeof(Int32));
                AddNewRow(dtBind);
                gcBudgetStatisticsDetail.DataSource = dtBind;
                BudgetStatisticsDetails = dtBind;
            }
        }

        /// <summary>
        /// This method is used to bind leger reference details
        /// </summary>
        private void BindBudgetStatisticsDetail()
        {
            try
            {
                using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                {
                    DataTable dtBind = BudgetStatisticsDetails;
                    dtBind = AddNewRow(dtBind);
                    gcBudgetStatisticsDetail.DataSource = dtBind;
                    gvBudgetStatisticsDetail.MoveFirst();
                    gvBudgetStatisticsDetail.FocusedColumn = colStatisticsType;
                    gvBudgetStatisticsDetail.ShowEditor();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// This method is used to get and bind list of Statistics Types
        /// </summary>
        private void BindGridLKPStatisticsType()
        {
            try
            {
                using (StatisticsTypeSystem vouchersystem = new StatisticsTypeSystem())
                {
                    ResultArgs resultArgs = vouchersystem.FetchStatisticsTypeAll();
                    rglkpStatisticsType.DataSource = null;
                    if (resultArgs.Success)
                    {
                        rglkpStatisticsType.DataSource = resultArgs.DataSource.Table;
                        rglkpStatisticsType.DisplayMember = vouchersystem.AppSchema.StatisticsType.STATISTICS_TYPEColumn.ToString();
                        rglkpStatisticsType.ValueMember = vouchersystem.AppSchema.StatisticsType.STATISTICS_TYPE_IDColumn.ToString();
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
        /// To Focus grid with first row and reference column
        /// </summary>
        private void SetGridFocus()
        {
            gcBudgetStatisticsDetail.Select();
            gvBudgetStatisticsDetail.MoveFirst();
            gvBudgetStatisticsDetail.FocusedColumn = colStatisticsType;
            gvBudgetStatisticsDetail.ShowEditor();
        }

        /// <summary>
        /// This method is used to check all rows should have stati and amount
        /// and foucs to concern rows
        /// </summary>
        /// <returns></returns>
        private bool IsValidRows()
        {
            DataTable dt = gcBudgetStatisticsDetail.DataSource as DataTable;

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
                    dv.RowFilter = "(" + vouchersystem.AppSchema.BudgetStatistics.STATISTICS_TYPE_IDColumn.ColumnName + " > 0 OR "
                                                + vouchersystem.AppSchema.BudgetStatistics.TOTAL_COUNTColumn.ColumnName + " > 0)";
                    gvBudgetStatisticsDetail.FocusedColumn = colStatisticsType;
                    if (dv.Count > 0)
                    {
                        isValid = true;
                        foreach (DataRowView drTrans in dv)
                        {
                            Id = this.UtilityMember.NumberSet.ToInteger(drTrans["STATISTICS_TYPE_ID"].ToString());
                            Amt = this.UtilityMember.NumberSet.ToDecimal(drTrans["TOTAL_COUNT"].ToString());

                            if ((Id == 0 || Amt == 0))
                            {
                                if (Id == 0)
                                {
                                    //validateMessage = "Required Information not filled, Cost Centre Name is empty";
                                    validateMessage = this.GetMessage(MessageCatalog.Master.StatisticsType.STATISTICSTYPE_EMPTY);
                                    gvBudgetStatisticsDetail.FocusedColumn = colStatisticsType;
                                }
                                if (Amt == 0)
                                {
                                    //validateMessage = "Required Information not filled, Amount is empty";
                                    validateMessage = "Total is empty";
                                    gvBudgetStatisticsDetail.FocusedColumn = colCount;
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
                    gvBudgetStatisticsDetail.CloseEditor();
                    gvBudgetStatisticsDetail.FocusedRowHandle = gvBudgetStatisticsDetail.GetRowHandle(RowPosition);
                    gvBudgetStatisticsDetail.ShowEditor();
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
        private void UpdateBudgetDetails()
        {
            try
            {
                DataTable dtgridbudgetstatistics = gcBudgetStatisticsDetail.DataSource as DataTable;
                if (dtgridbudgetstatistics != null && dtgridbudgetstatistics.Rows.Count > 0)
                {
                    this.DialogResult = DialogResult.OK;
                    using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                    {
                        BudgetStatisticsDetails.DefaultView.RowFilter = vouchersystem.AppSchema.BudgetStatistics.STATISTICS_TYPE_IDColumn.ColumnName + " > 0 AND " +
                                                                          vouchersystem.AppSchema.BudgetStatistics.TOTAL_COUNTColumn.ColumnName + " > 0";
                        BudgetStatisticsDetails = BudgetStatisticsDetails.DefaultView.ToTable();
                    }
                    this.ReturnValue = BudgetStatisticsDetails;
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
                dr[vouchersystem.AppSchema.BudgetStatistics.BUDGET_IDColumn.ColumnName] = 0;
                dr[vouchersystem.AppSchema.BudgetStatistics.STATISTICS_TYPE_IDColumn.ColumnName] = 0;
                dr[vouchersystem.AppSchema.BudgetStatistics.TOTAL_COUNTColumn.ColumnName] = 0;
                dtSource.Rows.Add(dr);
            }
            return dtSource;
        }

        /// <summary>
        /// Attach Event for amount changed
        /// </summary>
        private void RealColumnEditTotalCount()
        {
            colCount.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditTransAmount_EditValueChanged);
            this.gvBudgetStatisticsDetail.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvBudgetStatisticsDetail.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colCount)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvBudgetStatisticsDetail.ShowEditorByMouse();
                    }));
                }
            };
        }

        /// <summary>
        /// Delete row in the grid
        /// </summary>
        public void DeleteBudgetStatisticsDetails()
        {
            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                gvBudgetStatisticsDetail.DeleteRow(gvBudgetStatisticsDetail.FocusedRowHandle);
                if (gvBudgetStatisticsDetail.RowCount == 0)
                {
                    BindEmptySource();
                }
                gvBudgetStatisticsDetail.MoveFirst();
                gvBudgetStatisticsDetail.FocusedColumn = colStatisticsType;
                gvBudgetStatisticsDetail.ShowEditor();

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
                DataTable dtRef = gcBudgetStatisticsDetail.DataSource as DataTable;
                foreach (DataRow dr in dtRef.Rows)
                {
                    Int32 selectedrefid = UtilityMember.NumberSet.ToInteger(dr["STATISTICS_TYPE_ID"].ToString());
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
        /// This method is used to move next row in the grid
        /// </summary>
        private void MoveNextRow()
        {
            if (IsValidRows())
            {
                gvBudgetStatisticsDetail.MoveNext();
                gvBudgetStatisticsDetail.FocusedColumn = colStatisticsType;
                gvBudgetStatisticsDetail.ShowEditor();
            }
        }

        private void frmBudgetStatisticsDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
            {
                UpdateBudgetDetails();
                if (BudgetStatisticsDetails.Rows.Count == 0 && BudgetId>0)
                {
                    string validateMessage = this.GetMessage(MessageCatalog.Master.Transaction.TRANS_COSTCENTER_VALIDATION_MESSAGE);
                    MessageRender.ShowMessage(validateMessage);
                    if (gvBudgetStatisticsDetail.RowCount == 0)
                    {
                        BindEmptySource();
                    }
                    gvBudgetStatisticsDetail.MoveFirst();
                    gvBudgetStatisticsDetail.FocusedColumn = colStatisticsType;
                    gvBudgetStatisticsDetail.ShowEditor();
                    e.Cancel = true;
                }
            }
        }
    }
}