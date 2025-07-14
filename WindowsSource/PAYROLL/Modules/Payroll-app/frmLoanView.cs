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
using Payroll.Model.UIModel;
using Bosco.Utility.Common;
using DevExpress.XtraPrinting;

namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmLoanView : frmPayrollBase
    {
        #region VariableDeclartion
        public int LoanID = 0;
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        clsPayrollBase payrollbase = new clsPayrollBase();
        #endregion

        #region Propertise
        private int lID = 0;
        private int LoanId
        {
            get
            {
                RowIndex = gvLoanView.FocusedRowHandle;
                lID = gvLoanView.GetFocusedRowCellValue(colLoanId) != null ? Convert.ToInt32(gvLoanView.GetFocusedRowCellValue(colLoanId).ToString()) : 0;
                return lID;
            }
            set
            {
                lID = value;
            }
        }
        #endregion

        #region Constructor
        public frmLoanView()
        {
            InitializeComponent();
        }

        #endregion

        #region Events
        /// <summary>
        /// Load Loan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLoanView_Load(object sender, EventArgs e)
        {
            FetchLoanRecord();
            ApplyUserRights();
        }

        /// <summary>
        /// Add Loan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar1_AddClicked(object sender, EventArgs e)
        {
            ShowLoanForm((int)AddNewRow.NewRow);
        }



        /// <summary>
        /// Print the Loan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar1_PrintClicked(object sender, EventArgs e)
        {

            //payrollbase.PrintGridView(gcLoanView, this.Text, PrintType.DT, gvLoanView, false);
            payrollbase.PrintGridView(gcLoanView, this.GetMessage(MessageCatalog.Payroll.Loan.LOAN_PRINT_CAPTION), PrintType.DT, gvLoanView, false);

        }
        /// <summary>
        /// delete the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar1_DeleteClicked(object sender, EventArgs e)
        {
            DeleteLoan();
            FetchLoanRecord();
        }


        /// <summary>
        /// Edit the Loan Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar1_EditClicked(object sender, EventArgs e)
        {
            showEditLoan();
        }

        /// <summary>
        /// implement Loan Edit Details 
        /// </summary>
        private void showEditLoan()
        {
            if (gvLoanView.RowCount != 0)
            {
                if (LoanId != 0)
                {
                    ShowLoanForm(LoanId);
                }
                else
                {
                    if (!chkShowFilter.Checked)
                    {
                        //XtraMessageBox.Show(" No record selected", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_GRID_EMTPY_INFO));
                    }
                }
            }
            else
            {
                //XtraMessageBox.Show("No record is available in the grid to edit", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_EDIT_GRID_EMTPY));
            }
        }

        /// <summary>
        /// Refresh Loan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar1_RefreshClicked(object sender, EventArgs e)
        {
            FetchLoanRecord();
        }

        /// <summary>
        /// Close loan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar1_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Double Click Loan Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvLoanView_DoubleClick(object sender, EventArgs e)
        {
            showEditLoan();
        }

        /// <summary>
        /// Refresh Loan 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchLoanRecord();
            gvLoanView.FocusedRowHandle = RowIndex;
        }

        /// <summary>
        /// Count Loan Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvLoanView_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvLoanView.RowCount.ToString();
        }

        /// <summary>
        /// filter Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvLoanView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvLoanView, colLoanAbbreviation);
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

        #region Methods

        /// <summary>
        /// Show Module popup Loan form based on the id.
        /// </summary>
        private void ShowLoanForm(int LoanId)
        {
            try
            {
                frmLoanAdd frmloan = new frmLoanAdd(LoanId);
                frmloan.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmloan.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
            finally { }
        }

        private void SetTitle()
        {
            this.Text = this.GetMessage(MessageCatalog.Payroll.Loan.LOAN_VIEW_CAPTION);
        }

        /// <summary>
        /// Load Loan
        /// </summary>
        public void FetchLoanRecord()
        {
            using (clsPayrollLoan Payroll = new clsPayrollLoan())
            {
                DataTable dtGridAssign = Payroll.getPayrollLoanList();
                gcLoanView.DataSource = dtGridAssign;
                gcLoanView.RefreshDataSource();
            }
        }

        /// <summary>
        /// Delete the Loan
        /// </summary>
        private void DeleteLoan()
        {
            try
            {
                if (gvLoanView.RowCount != 0)
                {
                    if (LoanId != 0)
                    {
                        using (clsPayrollLoan loanSystem = new clsPayrollLoan())
                        {
                            //if (XtraMessageBox.Show("Are you sure to Delete? ", "PAYROLL", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_DELETED_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)//Are you sure to Delete?
                            {
                                int ValidateId = loanSystem.savePayrollLoanData(clsPayrollConstants.PAYROLL_LOAN_DELETE, LoanId);
                                if (ValidateId == 1)
                                {
                                    //this.ShowSuccessMessage(MessageCatalog.Payroll.Loan.LOAN_DELETE_SUCCESS);
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.Loan.LOAN_DELETE_SUCCESS));
                                    //XtraMessageBox.Show("Record Deleted", "PAYROLL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                    else
                    {
                        //this.ShowSuccessMessage(MessageCatalog.Payroll.Loan.LOAN_DETAILS_EMPTY);
                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.Loan.LOAN_DETAILS_EMPTY));
                        //XtraMessageBox.Show("No record is Available to Delete", "PAYROLL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }




        #endregion



        private void gvLoanView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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

        private void frmLoanView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void frmLoanView_EnterClicked(object sender, EventArgs e)
        {
            showEditLoan();
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(Loan.CreateLoan);
            this.enumUserRights.Add(Loan.EditLoan);
            this.enumUserRights.Add(Loan.DeleteLoan);
            this.enumUserRights.Add(Loan.ViewLoan);
            this.ApplyUserRights(ucToolBar1, enumUserRights, (int)Menus.Loan);
        }

    }
}