using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using Bosco.Utility;
using Bosco.Utility.CommonMemberSet;
using Payroll.Model.UIModel;

namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmLoanManagementView : frmPayrollBase
    {
        #region VariableDeclaration
        public int LoanMgtId = 0;
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        NumberSetMember numberset = new NumberSetMember();
        clsLoanManagement loanmanagement = new clsLoanManagement();
        clsPayrollBase payrollbase = new clsPayrollBase();
        #endregion

        #region Property
        private int lID = 0;
        private int LoanMgId
        {
            get
            {
                RowIndex = gvLoanMgt.FocusedRowHandle;
                lID = gvLoanMgt.GetFocusedRowCellValue(colPrLoanId) != null ? Convert.ToInt32(gvLoanMgt.GetFocusedRowCellValue(colPrLoanId).ToString()) : 0;
                return lID;
            }
            set
            {
                lID = value;
            }
        }
        #endregion

        #region Constructor
        public frmLoanManagementView()
        {
            InitializeComponent();
        }

        #endregion

        #region Events
        private void frmLoanManagementView_Load(object sender, EventArgs e)
        {
            FetchLoanRecord();
            SetTitle();
            ApplyUserRights();
        }
        private void ucToolBar1_AddClicked(object sender, EventArgs e)
        {
            ShowLoanForm((int)AddNewRow.NewRow);
        }
        private void ucToolBar1_EditClicked(object sender, EventArgs e)
        {
            showEditLoan();
        }
        private void ucToolBar1_DeleteClicked(object sender, EventArgs e)
        {
            DeleteLoan();
            FetchLoanRecord();
        }

        private void ucToolBar1_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucToolBar1_PrintClicked(object sender, EventArgs e)
        {
            //payrollbase.PrintGridView(gcLoanMgt, this.Text, PrintType.DT, gvLoanMgt, false);
            payrollbase.PrintGridView(gcLoanMgt, this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_PRINT_CAPTION), PrintType.DT, gvLoanMgt, false);
        }
        private void gvLoanMgt_DoubleClick(object sender, EventArgs e)
        {
            showEditLoan();
        }

        private void gvLoanMgt_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvLoanMgt.RowCount.ToString();
        }

        public void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchLoanRecord();
            gvLoanMgt.FocusedRowHandle = RowIndex;
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvLoanMgt.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvLoanMgt, colName);
            }
        }

        /// <summary>
        /// Set focus for Row Filter
        /// </summary>
        /// <param name="gridview"></param>
        /// <param name="colGridColumn"></param>
        protected virtual void SetFocusRowFilter(DevExpress.XtraGrid.Views.Grid.GridView gridview, DevExpress.XtraGrid.Columns.GridColumn colGridColumn)
        {
            gridview.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
            gridview.FocusedColumn = colGridColumn;
            gridview.OptionsFind.AllowFindPanel = false;
            gridview.ShowEditor();
        }
        #endregion

        #region Method

        private void SetTitle()
        {
            this.Text = this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_VIEW_CAPTION);
        }

        /// <summary>
        /// Show Module popup Loan form based on the id.
        /// </summary>
        private void ShowLoanForm(int LoanMId)
        {
            try
            {
                frmAddLoanManagement frmloanMgt = new frmAddLoanManagement(LoanMId);
                frmloanMgt.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmloanMgt.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
            finally { }
        }

        /// <summary>
        /// implement Loan Edit Details 
        /// </summary>
        private void showEditLoan()
        {
            if (gvLoanMgt.RowCount != 0)
            {
                if (LoanMgId != 0)
                {
                    DataTable dtLoanRecord = loanmanagement.FetchLoanMgtId(LoanMgId).DataSource.Table;
                    string InstallmentPaid = dtLoanRecord.Rows[0]["Installments"].ToString();
                    string[] strSplit = InstallmentPaid.Split('/');
                    if (strSplit.Length > 0)
                    {
                        //if (Int32.Parse(strSplit[0]) > 0)
                        //{

                        //    XtraMessageBox.Show("Loan cannot be edited because the installsment has been paid,", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}
                        //else
                        //{
                        ShowLoanForm(LoanMgId);
                        //}
                    }

                }
                else
                {
                    if (!chkShowFilter.Checked)
                    {
                        //XtraMessageBox.Show("No record is available in the grid", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_GRID_EMTPY_INFO));
                    }
                }
            }
            else
            {
                //XtraMessageBox.Show(" No record is available in the grid to edit", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_EDIT_GRID_EMTPY));
            }
        }
        /// <summary>
        /// Delete the Loan
        /// </summary>
        private void DeleteLoan()
        {
            try
            {
                if (gvLoanMgt.RowCount != 0)
                {
                    if (LoanMgId != 0)
                    {
                        using (clsPrLoan loanMgtSystem = new clsPrLoan())
                        {
                            //if (this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_DELETED_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_DELETED_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)//Are you sure to Delete?
                            {
                                bool ValidateId = loanMgtSystem.DeleteLoanObtain(LoanMgId);
                                if (ValidateId == true)
                                {
                                    resultArgs = DeleteVouchersbyPrloanId(LoanMgId);
                                    if (resultArgs.Success)
                                    {
                                        //XtraMessageBox.Show("Record deleted", "PAYROLL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //this.ShowSuccessMessage("Record deleted");
                                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_DELETE_SUCCESS));
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //XtraMessageBox.Show("Record not deleted", "PAYROLL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_DELETE_FAILURE));
                    }
                }
                else
                {
                    //XtraMessageBox.Show("No record is available in the grid to delete", "PAYROLL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_GRID_EMTPY_INFO));
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// Load Loan Management
        /// </summary>
        public void FetchLoanRecord()
        {
            using (clsPrLoan LoanMgt = new clsPrLoan())
            {
                DataTable dtGridAssign = LoanMgt.GetLoanSQL(true, "PRLOANMNT");
                if (dtGridAssign != null && dtGridAssign.Rows.Count > 0)
                {
                    gcLoanMgt.DataSource = dtGridAssign;
                    gcLoanMgt.RefreshDataSource();
                }
                else
                {
                    gcLoanMgt.DataSource = null;
                }
            }
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(IssuesLoan.CreateIssuesLoan);
            this.enumUserRights.Add(IssuesLoan.EditIssuesLoan);
            this.enumUserRights.Add(IssuesLoan.DeleteIssuesLoan);
            this.enumUserRights.Add(IssuesLoan.ViewIssuesLoan);
            this.ApplyUserRights(ucToolBar1, enumUserRights, (int)Menus.IssuesLoan);
        }
        /// <summary>
        /// To delete loan management vouchers
        /// </summary>
        /// <param name="Projectid"></param>
        /// <returns></returns>
        public ResultArgs DeleteVouchersbyPrloanId(int Prloangetid)
        {
            try
            {
                using (clsPrLoan prloan = new clsPrLoan())
                {
                    resultArgs = prloan.DeleteVoucherTransByLoangetId(Prloangetid);
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        resultArgs = prloan.DeleteVoucherMasterTransByLoangetid(Prloangetid);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return resultArgs;
        }
        #endregion

        private void ucToolBar1_RefreshClicked(object sender, EventArgs e)
        {
            FetchLoanRecord();
        }

        private void gvLoanMgt_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

            if (!chkShowFilter.Checked)
            {
                //if (gvProcess.GetRowCellValue(e.RowHandle, colIdentification).ToString() == "G")
                //{

                //    e.Appearance.ForeColor = System.Drawing.Color.Green;
                //    e.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
                //    e.Appearance.BackColor = Color.Gainsboro;
                //    // e.Appearance.BackColor = System.Drawing.Color.MidnightBlue;

                //}
            }

        }

        private void frmLoanManagementView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void frmLoanManagementView_EnterClicked(object sender, EventArgs e)
        {
            showEditLoan();
        }

    }
}