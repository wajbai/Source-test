using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Payroll.Model.UIModel;
using Bosco.Utility.Common;
using Bosco.Utility;

namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmLoanAdd : frmPayrollBase
    {
        #region VariableDeclaration
        public event EventHandler UpdateHeld;
        public int LoanId = 0;
        private int ValidateLoan = 0;
        //private string LoanAdd = "Loan (Add)";
        //private string LoanEdit = "Loan (Edit)";
        private string get_LoanName = "";
        private clsPayrollStaff objClsStaff = new clsPayrollStaff();
        ResultArgs resultArgs = null;
        CommonMember commonmem = new CommonMember();
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public frmLoanAdd()
        {
            InitializeComponent();
        }
        public frmLoanAdd(int Id)
            : this()
        {
            objClsStaff.StaffId = LoanId = Id;
            LoanId = Id;
            AssignLoanDetails();
        }
        #endregion

        #region Events
        /// <summary>
        /// Load Loan Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLoanAdd_Load(object sender, EventArgs e)
        {
            AssignLoanDetails();//check
            SetTitle();
        }

        /// <summary>
        /// To Assign Value to Controls
        /// </summary>
        private void AssignLoanDetails()
        {
            try
            {
                if (LoanId != 0)
                {
                    using (clsPayrollLoan PayrollLoan = new clsPayrollLoan())
                    {
                        PayrollLoan.CommonId = LoanId;
                        PayrollLoan.getPayrollLoanDetails();
                        txtLoanName.Text = PayrollLoan.LoanName;
                        txtAbbreviation.Text = PayrollLoan.LoanAbbrivation;
                        get_LoanName = txtLoanName.Text;
                        txtAbbreviation.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
            finally { }
        }
        
        private void txtLoanName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtLoanName);
            txtLoanName.Text = this.commonmem.StringSet.ToSentenceCase(txtLoanName.Text);
        }

        private void txtAbbreviation_Leave(object sender, EventArgs e)
        {
            //this.SetBorderColor(txtAbbreviation);
            txtAbbreviation.Text = this.commonmem.StringSet.ToSentenceCase(txtAbbreviation.Text);
        }

        /// <summary>
        /// Save Loan Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateLoanDetails())
                {
                    using (clsPayrollLoan PayrollLoan = new clsPayrollLoan())
                    {
                        objClsStaff.StaffId = PayrollLoan.CommonId = (LoanId == 0) ? (int)AddNewRow.NewRow : LoanId;
                        objClsStaff.LoanName = txtLoanName.Text;
                        PayrollLoan.LoanName = txtLoanName.Text;
                        PayrollLoan.LoanAbbrivation = txtAbbreviation.Text;
                        ValidateLoan = PayrollLoan.savePayrollLoanData(LoanId);
                        if (ValidateLoan == 1)
                        {
                            //this.ShowSuccessMessage(MessageCatalog.Payroll.Loan.LOAN_DETAILS_SAVED);
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.Loan.LOAN_DETAILS_SAVED));
                            //txtAbbreviation.Focus();
                            txtAbbreviation.Focus();
                            //XtraMessageBox.Show("Record Saved", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            //XtraMessageBox.Show("Record Not Saved", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        if (LoanId == 0)
                        {
                            ClearControl();
                        }
                        if (UpdateHeld != null)
                        {
                            UpdateHeld(this, e);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(resultArgs.Message, ex.ToString());
            }
        }

        /// <summary>
        /// Close the Loan Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        /// <summary>
        /// Set Form Title for Loan
        /// </summary>
        /// <param name="BankId"></param>
        private void SetTitle()
        {
            this.Text = LoanId == 0 ? this.GetMessage(MessageCatalog.Payroll.Loan.LOAN_ADD_CAPTION) : this.GetMessage(MessageCatalog.Payroll.Loan.LOAN_EDIT_CAPTION);
            txtLoanName.Focus();
        }

        #region Method
        /// <summary>
        /// Purpose:To validate the mandatory fields.
        /// </summary>
        /// <returns></returns>
        public bool ValidateLoanDetails()
        {
            bool isLoan = true;
            //if (string.IsNullOrEmpty(txtAbbreviation.Text))
            //{
            //    //ShowMessageBox("Abbreviation is empty");
            //    XtraMessageBox.Show("Code is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    SetBorderColor(txtAbbreviation);
            //    isLoan = false;
            //    txtAbbreviation.Focus();
            //}
            if (string.IsNullOrEmpty(txtLoanName.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Loan.LOAN_NAME_EMPTY));
                this.SetBorderColor(txtLoanName);
                isLoan = false;
                txtLoanName.Focus();
            }
            else
            {
                txtAbbreviation.Focus();
            }
            return isLoan;
        }

        /// <summary>
        /// Clear the Controls
        /// </summary>
        private void ClearControl()
        {
            txtLoanName.Text = "";
            txtAbbreviation.Text = "";
            txtAbbreviation.Focus();
        }

        /// <summary>
        /// Set Border for textBox
        /// </summary>
        /// <param name="txtEdit"></param>
        protected void SetBorderColor(TextEdit txtEdit)
        {
            txtEdit.Properties.Appearance.BorderColor = string.IsNullOrEmpty(txtEdit.Text) ? Color.Red : Color.Empty;
        }
        #endregion
    }
}