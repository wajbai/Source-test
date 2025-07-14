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
    public partial class frmStaffNameTitle : frmPayrollBase
    {
        #region Variable Declaration
        private int StaffNameTitleId = 0;
        clsPayrollGrade obj = new clsPayrollGrade();
        CommonMember commem = new CommonMember();
        #endregion

        #region Event Declaration
        public event EventHandler UpdateHeld;
        #endregion

        #region Constructors
        public frmStaffNameTitle()
        {
            InitializeComponent();
        }

        public frmStaffNameTitle(int staffnametitleid) : this()
        {
            StaffNameTitleId = obj.PayrollId = staffnametitleid;
            //groupId = obj.GradeId = GroupId;
            this.StaffNameTitleId = staffnametitleid;
            AssignStaffNameTitleDetails();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Assign the Payroll Group Values to the Form Control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AssignStaffNameTitleDetails()
        {
            try
            {
                if (StaffNameTitleId != 0)
                {
                    using (NameTitleSystem nametitleSystem = new NameTitleSystem(StaffNameTitleId))
                    {
                        nametitleSystem.FetchNameTitleById();
                        txtStaffNameTitle.Text = nametitleSystem.NameTilte;
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

        public bool ValidateStaffNameDetails()
        {
            bool isStaffNameTitle = true;
            if (string.IsNullOrEmpty(txtStaffNameTitle.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.StaffNameTitle.PAYROLL_STAFF_NAME_TITLE_EMPTY));
                this.SetBorderColor(txtStaffNameTitle);
                isStaffNameTitle = false;
                txtStaffNameTitle.Focus();
            }
            else
            {
                txtStaffNameTitle.Focus();
            }
            return isStaffNameTitle;
        }

        /// <summary>
        /// To Clear the Fieds Values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ClearControl()
        {
            txtStaffNameTitle.Text = string.Empty;
            txtStaffNameTitle.Focus();
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
            this.Text = StaffNameTitleId == 0 ? this.GetMessage(MessageCatalog.Payroll.StaffNameTitle.PAYROLL_STAFF_NAME_TITLE_ADD_CAPTION) :
                this.GetMessage(MessageCatalog.Payroll.StaffNameTitle.PAYROLL_STAFF_NAME_TITLE_EDIT_CAPTION);
            txtStaffNameTitle.Focus();
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
            this.SetBorderColor(txtStaffNameTitle);
            txtStaffNameTitle.Text = this.commem.StringSet.ToSentenceCase(txtStaffNameTitle.Text);
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
                if (ValidateStaffNameDetails())
                {
                    using (NameTitleSystem nametitleSystem = new NameTitleSystem())
                    {
                        nametitleSystem.NameTilteid = StaffNameTitleId;
                        nametitleSystem.NameTilte = txtStaffNameTitle.Text;
                        ResultArgs result = nametitleSystem.SaveNameTitle();
                        if (result.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.PayrollGroup.PAYROLL_GROUP_DETAILS_SAVED));
                            ClearControl();
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
