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
    public partial class frmLockPayroll : frmPayrollBase
    {
        #region Varaible Declaration
        private clsPayrollActivities objActivities = new clsPayrollActivities();
        private frmOpenPayroll objOpenPR = new frmOpenPayroll();
        private int iLockStatus = 1;
        private string get_Status = "";
        private clsModPay objPRModPay = new clsModPay();
        private clsPrGateWay objPRGW = new clsPrGateWay();
        #endregion
        public frmLockPayroll()
        {
            InitializeComponent();
        }

        private void frmLockPayroll_Load(object sender, EventArgs e)
        {
            //lblMessageShow.Text = "Release Lock for " + clsGeneral.PAYROLL_MONTH;
            if (new clsModPay().GetValue("PRSTATUS", "lockedstatus", "PayRollId = " + clsGeneral.PAYROLL_ID) == "Y")
            {
                //this.Text = "Unlock Payroll";
                this.Text = this.GetMessage(MessageCatalog.Payroll.LockPayroll.UNLOCK_PAYROLL_CAPTION);
                //lblMessageShow.Text = "Release Lock for the Payroll '" + clsGeneral.PAYROLL_MONTH + "'";
                lblMessageShow.Text = this.GetMessage(MessageCatalog.Payroll.LockPayroll.LOCK_PAYROLL_MONTH) + clsGeneral.PAYROLL_MONTH + "'";
                //lblMessage.Text = "After Releasing the Lock, you are allowed to make changes in the payroll.";
                lblMessage.Text = this.GetMessage(MessageCatalog.Payroll.LockPayroll.LOCK_PAYROLL_CHANGE_INFO);
            }
            else
            {
                //this.Text = "Lock Payroll";
                this.Text = this.GetMessage(MessageCatalog.Payroll.LockPayroll.LOCK_PAYROLL_INFO);
                //lblMessageShow.Text = "Lock the Payroll '" + clsGeneral.PAYROLL_MONTH + "'";
                lblMessageShow.Text =this.GetMessage(MessageCatalog.Payroll.LockPayroll.LOCK_PAYROLL_MONTH_INFO)+ clsGeneral.PAYROLL_MONTH + "'";
                //lblMessage.Text = "After Locking you can't make any changes in the payroll.";
                lblMessage.Text = this.GetMessage(MessageCatalog.Payroll.LockPayroll.LOCK_PAYROLL_AFTERLOCK_INFO);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (objPRModPay.CheckDuplicate("PRStatus", "PayrollId = " + clsGeneral.PAYROLL_ID.ToString()))
            {
                objPRGW.PRLocked = objPRModPay.GetValue("PRStatus", "Lockedstatus", "PayrollId = " + clsGeneral.PAYROLL_ID.ToString());
                if (objPRGW.PRLocked == "N")
                    objPRGW.PRLocked = "Y";
                else
                    objPRGW.PRLocked = "N";
                //objPRGW.PRLocked = (objPRGW.PRLocked == "N")? "Y" : "N" ;
                objPRGW.PayRollId = clsGeneral.PAYROLL_ID;
                if (objPRGW.LockUnLockPayRoll())
                    this.Close();
                else
                    //XtraMessageBox.Show("Payroll is not available. Already Deleted!. ", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.LockPayroll.LOCK_PAYROLL_NOT_AVAILABLE_INFO));
            }
        }


    }
}