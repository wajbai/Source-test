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
    public partial class frmPayrollWorkLocation : frmPayrollBase
    {
        #region Variable Declaration
        private int PayrollWorkLocationid = 0;
        clsPayrollGrade obj = new clsPayrollGrade();
        CommonMember commem = new CommonMember();
        #endregion

        #region Event Declaration
        public event EventHandler UpdateHeld;
        #endregion

        #region Constructors
        public frmPayrollWorkLocation()
        {
            InitializeComponent();
        }

        public frmPayrollWorkLocation(int payrollworklocationid)
            : this()
        {
            PayrollWorkLocationid = obj.PayrollId = payrollworklocationid;
            this.PayrollWorkLocationid = payrollworklocationid;
            AssignPayrollWorkLocationDetails();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Assign the Payroll Group Values to the Form Control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AssignPayrollWorkLocationDetails()
        {
            try
            {
                if (PayrollWorkLocationid != 0)
                {
                    using (PayrollWorkLocationSystem payrollWorkLocaitonSystem = new PayrollWorkLocationSystem(PayrollWorkLocationid))
                    {
                        payrollWorkLocaitonSystem.FetchPayrollWorkLocationById();
                        txtWorkLocation.Text = payrollWorkLocaitonSystem.PayrollWorkLocation;
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

        public bool ValidatePayrollWorkLocationDetails()
        {
            bool isPayrollDepartment = true;
            if (string.IsNullOrEmpty(txtWorkLocation.Text))
            {
                //ShowMessageBox("Group Name is empty");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PayrollWorkLocation.PAYROLL_WORKLOCATION_NAME_EMPTY));
                this.SetBorderColor(txtWorkLocation);
                isPayrollDepartment= false;
                txtWorkLocation.Focus();
            }
            else
            {
                txtWorkLocation.Focus();
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
            txtWorkLocation.Text = string.Empty;
            txtWorkLocation.Focus();
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
            this.Text = PayrollWorkLocationid == 0 ? this.GetMessage(MessageCatalog.Payroll.PayrollWorkLocation.PAYROLL_WORKLOCATION_ADD_CAPTION) :
                this.GetMessage(MessageCatalog.Payroll.PayrollWorkLocation.PAYROLL_WORKLOCATION_EDIT_CAPTION);
            txtWorkLocation.Focus();
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
            this.SetBorderColor(txtWorkLocation);
            txtWorkLocation.Text = this.commem.StringSet.ToSentenceCase(txtWorkLocation.Text);
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
                if (ValidatePayrollWorkLocationDetails())
                {
                    using (PayrollWorkLocationSystem payrollWorklocationSystem = new PayrollWorkLocationSystem())
                    {
                        payrollWorklocationSystem.PayrollWorkLocationId = PayrollWorkLocationid;
                        payrollWorklocationSystem.PayrollWorkLocation = txtWorkLocation.Text;
                        ResultArgs result = payrollWorklocationSystem.SavePayrollWorkLocation();
                        if (result.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.PayrollGroup.PAYROLL_GROUP_DETAILS_SAVED));
                            if (PayrollWorkLocationid==0) ClearControl();
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                        }
                        //else
                        //{
                        //    this.ShowMessageBox(result.Message);
                        //}
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
