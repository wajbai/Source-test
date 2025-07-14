using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using System.Text.RegularExpressions;

using Bosco.Utility.Common;
using Bosco.Utility;
using Payroll.Model.UIModel;
using PAYROLL.Modules.Payroll_app;
using Payroll.DAO;



namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmPayrollDepartment : frmPayrollBase
    {
        #region Variable Declaration
        private int PayrollDepartmentid = 0;
        clsPayrollGrade obj = new clsPayrollGrade();
        CommonMember commem = new CommonMember();
        #endregion

        #region Event Declaration
        public event EventHandler UpdateHeld;
        #endregion

        #region Constructors
        public frmPayrollDepartment()
        {
            InitializeComponent();
        }

        public frmPayrollDepartment(int payrolldepartmentid) : this()
        {
            PayrollDepartmentid = obj.PayrollId = payrolldepartmentid;
            //groupId = obj.GradeId = GroupId;
            this.PayrollDepartmentid = payrolldepartmentid;
            AssignPayrollDepartmentDetails();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Assign the Payroll Group Values to the Form Control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AssignPayrollDepartmentDetails()
        {
            try
            {
                if (PayrollDepartmentid != 0)
                {
                    using (PayrollDepartmentSystem payrollDepSystem = new PayrollDepartmentSystem(PayrollDepartmentid))
                    {
                        payrollDepSystem.FetchPayrollDepartmentById();
                        txtDepartment.Text = payrollDepSystem.Department;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// To Validate the Field Values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public bool ValidatePayrollDepartmentDetails()
        {
            bool isPayrollDepartment = true;
            if (string.IsNullOrEmpty(txtDepartment.Text))
            {
                //ShowMessageBox("Group Name is empty");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PayrollDepartment.PAYROLL_DEPARTMENT_NAME_EMPTY));
                this.SetBorderColor(txtDepartment);
                isPayrollDepartment= false;
                txtDepartment.Focus();
            }
            else
            {
                txtDepartment.Focus();
            }
            return isPayrollDepartment;
        }

        /// <summary>
        /// To Clear the Fieds Values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ClearControl()
        {
            txtDepartment.Text = string.Empty;
            txtDepartment.Focus();
        }

        /// <summary>
        /// Change the Caption for Form Naming While Adding and Editing the Values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPayrollGroup_Load(object sender, EventArgs e)
        {
            SetTitle();
        }
        private void SetTitle()
        {
            this.Text = PayrollDepartmentid == 0 ? this.GetMessage(MessageCatalog.Payroll.PayrollDepartment.PAYROLL_DEPARTMENT_ADD_CAPTION) :
                this.GetMessage(MessageCatalog.Payroll.PayrollGroup.PAYROLL_GROUP_EDIT_CAPTION);
            txtDepartment.Focus();
        }

        /// <summary>
        /// To set the Border Color Red While the Field is Emplty.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SetBorderColor(TextEdit txtEdit)
        {
            txtEdit.Properties.Appearance.BorderColor = string.IsNullOrEmpty(txtEdit.Text) ? Color.Red : Color.Empty;
        }

        #endregion

        #region Events

        private void txtGroup_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtDepartment);
            txtDepartment.Text = this.commem.StringSet.ToSentenceCase(txtDepartment.Text);
        }

        /// <summary>
        /// To Save or Edit the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidatePayrollDepartmentDetails())
                {
                    using (PayrollDepartmentSystem payrollDepSystem = new PayrollDepartmentSystem())
                    {
                        payrollDepSystem.DepartmentId = PayrollDepartmentid;
                        payrollDepSystem.Department = txtDepartment.Text;
                        ResultArgs result = payrollDepSystem.SavePayrollDepartments();
                        if (result.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.PayrollGroup.PAYROLL_GROUP_DETAILS_SAVED));
                            if (PayrollDepartmentid ==0) ClearControl();
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
            finally
            {

            }
        #endregion
        }
    }
}
