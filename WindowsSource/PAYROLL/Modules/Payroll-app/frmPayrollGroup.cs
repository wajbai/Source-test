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
    public partial class frmPayrollGroup : frmPayrollBase
    {
        #region Variable Declaration
        private int groupId = 0;
        //private string PayrollGroupAdd = "Payroll Group (Add)";
        //private string PayrollGroupEdit = "Payroll Group (Edit)";
        private bool ValidateStatus = false;
        clsPayrollGrade obj = new clsPayrollGrade();
        ResultArgs resultArgs = new ResultArgs();
        CommonMember commem = new CommonMember();
        #endregion

        #region Event Declaration
        public event EventHandler UpdateHeld;
        #endregion

        #region Constructors
        public frmPayrollGroup()
        {
            InitializeComponent();
        }

        public frmPayrollGroup(int GroupId)
            : this()
        {
            groupId = obj.PayrollId = GroupId;
            //groupId = obj.GradeId = GroupId;
            this.groupId = GroupId;
            AssignPayrollDetails();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Assign the Payroll Group Values to the Form Control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AssignPayrollDetails()
        {
            try
            {
                if (groupId != 0)
                {

                    using (clsPayrollGrade groupSystem = new clsPayrollGrade(groupId))
                    {

                        txtGroup.Text = groupSystem.PayrollGrade;
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

        public bool ValidateGroupDetails()
        {
            bool isGroup = true;
            if (string.IsNullOrEmpty(txtGroup.Text))
            {
                //ShowMessageBox("Group Name is empty");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PayrollGroup.PAYROLL_GROUP_NAME_EMPTY));
                this.SetBorderColor(txtGroup);
                isGroup = false;
                txtGroup.Focus();
            }
            else
            {
                txtGroup.Focus();
            }
            return isGroup;
        }

        /// <summary>
        /// To Clear the Fieds Values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ClearControl()
        {
            txtGroup.Text = string.Empty;
            txtGroup.Focus();
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
            //this.Text = groupId == 0 ? PayrollGroupAdd : PayrollGroupEdit;
            this.Text = groupId == 0 ? this.GetMessage(MessageCatalog.Payroll.PayrollGroup.PAYROLL_GROUP_ADD_CAPTION) : this.GetMessage(MessageCatalog.Payroll.PayrollGroup.PAYROLL_GROUP_EDIT_CAPTION);
            txtGroup.Focus();
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

        //private void txtGroup_Leave(object sender, EventArgs e)
        //{
        //    this.SetBorderColor(txtGroup);
        //    txtGroup.Text = this.commem.StringSet.ToSentenceCase(txtGroup.Text);
        //}

        //protected void ShowMessageBox(string Msg)
        //{
        //    XtraMessageBox.Show(Msg, this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
        //}
        #endregion

        #region Events

        private void txtGroup_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtGroup);
            txtGroup.Text = this.commem.StringSet.ToSentenceCase(txtGroup.Text);
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
                if (ValidateGroupDetails())
                {
                    using (clsPayrollGrade PayrollgGrade = new clsPayrollGrade())
                    {
                        PayrollgGrade.GradeId = groupId == 0 ? (int)AddNewRow.NewRow : groupId;
                        if (groupId == 0)
                        {
                            PayrollgGrade.PayrollGrade = txtGroup.Text;
                            ValidateStatus = PayrollgGrade.addPayrollStaffGrade();
                            if (ValidateStatus == true)
                            {
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.PayrollGroup.PAYROLL_GROUP_DETAILS_SAVED));
                                Refresh();
                                ClearControl();
                                if (UpdateHeld != null)
                                {
                                    UpdateHeld(this, e);
                                }
                            }
                            else
                            {
                                //XtraMessageBox.Show("Group exists already", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PayrollGroup.PAYROLL_GROUP_EXISTS_ALREADY));
                            }
                        }
                        else
                        {
                            PayrollgGrade.PayrollGrade = txtGroup.Text;
                            PayrollgGrade.PayrollId = groupId;
                            bool isEdited = PayrollgGrade.updatePayrollStaffGrade();
                            if (isEdited == true)
                            {
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.PayrollGroup.PAYROLL_GROUP_DETAILS_SAVED));
                                //XtraMessageBox.Show("Record Saved", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //ClearControl();
                                if (UpdateHeld != null)
                                {
                                    UpdateHeld(this, e);
                                }
                            }
                            else
                            {
                                //XtraMessageBox.Show("Group exists already", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PayrollGroup.PAYROLL_GROUP_EXISTS));
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
