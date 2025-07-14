/*****************************************************************************************
*					Interface       : frmOpenPayroll
*					Object Involved : 
*					Purpose         : To Open existing Payroll
*					Date from       : 01-Dec-2014
*					Author          : P.Adaikala Praveen
*					Modified by     : 
*****************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Utility.Common;
using Bosco.Utility.CommonMemberSet;
using Payroll.Model.UIModel;
using Payroll.DAO.Schema;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;

namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmOpenPayroll : frmPayrollBase
    {

        #region Declaration
        CommonMember UtilityMember = new CommonMember();
        frmMain BrowseForm = new frmMain();
        private clsPayrollStaff objClsStaff = new clsPayrollStaff();
        private clsPayrollActivities objActivities = new clsPayrollActivities();
        private clsPrGateWay objPRGW = new clsPrGateWay();
        private clsModPay objModPay = new clsModPay();
        private frmPayrollview viewData = new frmPayrollview();
        private frmMain clsParent = new frmMain();
        public EventHandler UpdateHeld;
        #endregion

        #region Properties

        #endregion

        #region Constructor

        #endregion

        #region Events
        public frmOpenPayroll()
        {
            InitializeComponent();
        }
        public frmOpenPayroll(frmMain getParent)
        {
            InitializeComponent();
            this.clsParent = getParent;
        }
        private void frmOpenPayroll_Load(object sender, EventArgs e)
        {
            FillPayroll();
            this.Text = "Open Payroll Month";
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                long nPRId;
                if (lstExistingPayroll.SelectedIndex < 0)
                    return;
                nPRId = long.Parse(lstExistingPayroll.SelectedValue.ToString());
                objPRGW.PRLocked = objModPay.GetValue("PRStatus", "Lockedstatus", "PayrollId = " + nPRId);
                if (objPRGW.PRLocked == "Y")
                {
                    //XtraMessageBox.Show("The selected payroll is locked!", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.OpenPayroll.OPENPAYROLL_SELECT_PAYROLL_LOCKED_INFO));
                    //return;
                }
                //For Client/Server - while opening payroll check the selected payroll exists or not

                if (objModPay.CheckDuplicate("PRCREATE", "PayrollId = " + nPRId))
                {
                    clsGeneral.PAYROLL_ID = nPRId;
                    objPRGW.PayRollId = clsGeneral.PAYROLL_ID;
                    objPRGW.PRName = lstExistingPayroll.Text.ToString();

                    BrowseForm.lblPayrollMonth.Text = SettingProperty.PayrollMonth = lstExistingPayroll.Text.ToString();
                    clsGeneral.PAYROLLDATE = objModPay.GetValue("PRCREATE", "DATE_FORMAT(PRDATE, '%d/%m/%Y') as PRDATE", "PAYROLLID = " + nPRId);
                    clsGeneral.PAYROLL_MONTH = lstExistingPayroll.Text.ToString();

                    //clsGeneral.PAYROLL_ID=objPRGW.PayRollId;
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                else
                {
                    //XtraMessageBox.Show("The payroll has been deleted!", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //this.ShowSuccessMessage(" The current payroll month deleted");
                     this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.OpenPayroll.LOCK_DELETE_SUCCESS));
                    FillPayroll();
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                if (UpdateHeld != null)
                    UpdateHeld(this, e);
            }

            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Methods
        private void FillPayroll()
        {
            //long nPRId;
            //object BindTable = objPRGW.FetchRecord(SQLCommand.Payroll.PayrollExistOpen, "payrol");

            //UtilityMember.ListSet.BindDataList(this.lstExistingPayroll, BindTable, "PRNAME", "PAYROLLID", false);
            //nPRId = objPRGW.PayRollId;
            //lstExistingPayroll.SelectedValue = nPRId;
            //if (lstExistingPayroll.Items.Count > 0 && lstExistingPayroll.SelectedIndex < 0)
            //{
            //    lstExistingPayroll.SelectedIndex = 0;
            //    clsGeneral.PAYROLL_ID = long.Parse(lstExistingPayroll.SelectedValue.ToString());
            //}


            int i;
            long nPRId;

            object BindTable = objPRGW.FetchRecord(SQLCommand.Payroll.PayrollExistOpen, "payrol");
            SettingProperty.dtOpen = BindTable as DataTable;
            //  UtilityMember.ListSet.BindDataList(this.lstExistingPayroll, BindTable, "PRNAME", "PAYROLLID", false);
            if (SettingProperty.dtOpen != null && SettingProperty.dtOpen.Rows.Count > 0)
            {
                lstExistingPayroll.DataSource = SettingProperty.dtOpen;
                lstExistingPayroll.ValueMember = "PAYROLLID";
                lstExistingPayroll.DisplayMember = "PRNAME";
            }
            nPRId = objPRGW.PayRollId;
            lstExistingPayroll.SelectedValue = clsGeneral.PAYROLL_ID;
            if (lstExistingPayroll.Items.Count > 0 && lstExistingPayroll.SelectedIndex < 0)
            {
                lstExistingPayroll.SelectedIndex = 0;
            }
        }
        #endregion

        private void lstExistingPayroll_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            if (lstExistingPayroll.SelectedIndex == e.Index)
            {
                e.Appearance.Font = new Font(lstExistingPayroll.Font, FontStyle.Bold);
            }
        }
    }
}