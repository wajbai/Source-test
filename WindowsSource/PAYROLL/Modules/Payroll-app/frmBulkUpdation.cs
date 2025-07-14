using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Utility.Common;
using Bosco.Utility.CommonMemberSet;
using Bosco.Utility.Validations;
using System.Text.RegularExpressions;
using Payroll.Model;
using Payroll.Model.UIModel;
using Payroll.DAO.Schema;
using Bosco.DAO.Data;


namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmBulkUpdation : DevExpress.XtraEditors.XtraForm
    {
        #region Variable Declaration
        public event EventHandler UpdateHeld;
        private clsPayrollStaff objClsStaff = new clsPayrollStaff();
        //private clsPayrollLoan objClsLoan = new clsPayrollLoan();
        private clsPayrollComponent objClsComponent = new clsPayrollComponent();
        //private clsLoanManagement objClsLoanMnt = new clsLoanManagement();
        private clsPayrollGrade objClsGrade = new clsPayrollGrade();
        private clsPrComponent objPRComp = new clsPrComponent();
        private clsprCompBuild objPRBuild = new clsprCompBuild();
        //private clsPrLoan objPrLoan = new clsPrLoan();
        private DataView dvCopyOfPayroll = new DataView();
        private Panel pnlProcess1;
        //private Panel pnlProcess;
        private Panel pnlAmount1;
        //private Panel pnlAmount;
        private CheckedListBox chkListGrade;
        private CheckedListBox chklstNonEditable;
        private RadioButton rdoNonEditable;
        private RadioButton rdoRestricted;
        private RadioButton rdoAllComponents;
        private CheckedListBox chklstComponents;
        private ListBox lstPayrollGroup;
        //private Button btnClose;
        //private Button btnBulkUpdation;
        private Panel fraProcess;
        public System.Windows.Forms.ProgressBar prgPRProcess;
        private TextBox txtReplaceComp;
        private System.Windows.Forms.ComboBox cboComponent;
        //private Label label2;
        private Label label1;
        private DataGrid dgPayrollProcess;
        //private Panel panel7;
        //private Panel panel6;
        private Label lblMessage;
        private Label lblSelectedGroups;
        private Label lblSelectedGroup;
        private PictureBox SelectPayroll;
        private Button btnSelectGroups;
        private Panel pnlSelectGroup;
        #endregion

        #region Constructors
        public frmBulkUpdation()
        {
            InitializeComponent();
        }
        #endregion

        #region Property
        public DevExpress.XtraEditors.ComboBoxEdit ComboboxData
        {
            get { return cboComponentData; }
            set { cboComponentData = value; }
        }

        public string sStaffIds = string.Empty;
        public string sGroupIdData = string.Empty;
        #endregion

        #region Events

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            int failedUpdation = 0;
            // if (lblMessage.Visible == true)
            //  return;
            if (cboComponentData.Text == "")
            {
                XtraMessageBox.Show("Select one of the Component to Edit", "Payroll ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboComponent.Focus();
                return;
            }
            if (txtReplaceCom.Text == "")
            {
                XtraMessageBox.Show("Enter the value", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReplaceCom.Focus();
                return;
            }
            //on 23-06-2007 @ 11 AM
            if (sStaffIds != "" && txtReplaceCom.Text != "" && cboComponentData.Text != "")
            {
                string[] sIndividualStaffId = sStaffIds.Split(',');
                for (int i = 0; i < sIndividualStaffId.Length; i++)
                {
                    if (!objPRBuild.ModifyStaffComponent(clsGeneral.PAYROLL_ID,
                        sGroupIdData,
                        cboComponentData.Text.Trim(),
                        sIndividualStaffId[i],
                        txtReplaceCom.Text.Trim(),
                        prgPRProcess))
                    {
                        //MessageBox.Show("Invalid to edit this component for this staff.","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        failedUpdation += 1;
                    }
                }
                prgPRProcess.Value = prgPRProcess.Maximum;
            }

            txtReplaceCom.Text = "";

            if (failedUpdation != 0)
            {
                XtraMessageBox.Show("Component is updated only for the eligible staff(s).", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                XtraMessageBox.Show("Component Values are updated Successfully.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (UpdateHeld != null)
            {
                UpdateHeld(this, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}