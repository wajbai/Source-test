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
using Payroll.Model.UIModel;
using Bosco.Utility;

namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmCreatePayroll : frmPayrollBase
    {
        #region Declaration
        string sDate = "";
        CommonMember UtilityMember = new CommonMember();
        //public string PayrollMessage = "Create Payroll For ";
        public string PayrollMessage = string.Empty;
        public DataTable dtStatus;
        public clsPrGateWay objPrGateWay = new clsPrGateWay();
        public clsModPay objModPay = new clsModPay();
        public clsPayrollGrade objClsGrade = new clsPayrollGrade();
        #endregion

        #region Properties

        #endregion

        #region Constructor

        public frmCreatePayroll()
        {
            InitializeComponent();
            PayrollMessage = this.GetMessage(MessageCatalog.Payroll.CreatePayroll.CREATE_PAYROLL_FOR_INFO);
        }
        #endregion

        #region Events
        /// <summary>
        /// Load Defaults
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCreatePayroll_Load(object sender, EventArgs e)
        {
            dePayrollFor.DateTime = UtilityMember.DateSet.ToDate(DateTime.Parse(System.DateTime.Now.ToString()).ToLongDateString(), false);
            SetDate();
            SetValues();
            GetRecentPayroll();
            lcProgressbar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }
        /// <summary>
        /// Load Concern month From,To date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dePayrollFor_EditValueChanged(object sender, EventArgs e)
        {  
            lblMessage.Text = PayrollMessage + " " + Enum.GetName(typeof(Month), dePayrollFor.DateTime.Month) + " " + dePayrollFor.DateTime.Year;
            SetDate();
            ShowMessage(false);
        }
        /// <summary>
        /// Create Payroll
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnCreate_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            lcProgressbar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            GetRecentPayroll();

            string sMonth = Enum.GetName(typeof(Month), dePayrollFor.DateTime.Month);
            objPrGateWay.PRDate = "01/" + dePayrollFor.DateTime.Month + "/" + dePayrollFor.DateTime.Year;
            //objPrGateWay.PRDate = dePeriodFrom.DateTime.Day + "/" + dePayrollFor.DateTime.Month + "/" + dePayrollFor.DateTime.Year;
            objPrGateWay.PRName = sMonth.Substring(0, 3) + " - " + dePayrollFor.DateTime.Year;
            objPrGateWay.CompImport = (clsGeneral.PAYROLL_ID != 0) ? "Y" : "N";
            objPrGateWay.PRImport = (clsGeneral.PAYROLL_ID != 0) ? "Y" : "N";
            objPrGateWay.FromDate = dePeriodFrom.DateTime.ToString();
            objPrGateWay.ToDate = dePeriodTo.DateTime.ToString();

            if (objPrGateWay.CreatePayRoll())
            {
                long nLastPayrollId = objPrGateWay.PRPrevId;
                objPrGateWay.PayRollId = objPrGateWay.GetCurrentPayroll();
                clsModPay.g_PayRollDate = objPrGateWay.PRDate;
                               
                if (nLastPayrollId != 0)
                {
                    objPrGateWay.CreateNewStaffGroup((int)nLastPayrollId, (int)objPrGateWay.PayRollId);
                }
                else
                {
                    objPrGateWay.UpdtaeNewStaffGroup((int)objPrGateWay.PayRollId);
                }
                                
                if (objPrGateWay.CompImport == "Y")
                {
                    ProcessPayroll();
                }
                //03/02/2023, to clear invalid payroll details
                objPrGateWay.ClearInvalidPaydetails();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            this.Cursor = Cursors.Default;
            //if (btnCreate.Text == "Impo<U>&r</U>t")
            if (btnCreate.Text ==this.GetMessage(MessageCatalog.Payroll.CreatePayroll.CREATE_PAYROLL_IMPORT_INFO))
            {
                //XtraMessageBox.Show("Payroll imported successfully.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.ShowSuccessMessage("Payroll imported successfully.");
                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.CreatePayroll.CREATE_PAYROLL_IMPORT_INFO));
            }
            else
            {
                //XtraMessageBox.Show("Payroll created successfully.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.ShowSuccessMessage("Payroll created successfully.");
                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.CreatePayroll.CREATE_PAYROLL_CREATE_INFO));
            }
            lcProgressbar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            clsGeneral.PAYROLL_ID = objPrGateWay.PayRollId;
            clsGeneral.PAYROLL_MONTH = clsModPay.g_PayRollDate;

        }
        /// <summary>
        /// This is to close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void dePeriodFrom_EditValueChanged(object sender, EventArgs e)
        {
            dePeriodTo.DateTime = dePeriodFrom.DateTime.AddMonths(1).AddDays(-1);
        }
        #endregion

        #region Methods

        private void  SetDate()
        {
            DateTime dtYearFrom = new DateTime(dePayrollFor.DateTime.Year, dePayrollFor.DateTime.Month, 1);
            DateTime dtYearTo = dtYearFrom.AddMonths(1).AddDays(-1);
            dePeriodFrom.DateTime = dtYearFrom;
            dePeriodTo.DateTime = dtYearTo;
        }

        private void GetRecentPayroll()
        {
            bool bEnable = true;
            if (objModPay.CheckDuplicate("PRCreate"))
            {
                string sRecentDate = objPrGateWay.GetPreviousValue();
                if (sRecentDate != "")
                {
                    DateTime dNewDate = new DateTime(int.Parse(sRecentDate.Substring(6, 4)), int.Parse(sRecentDate.Substring(3, 2)), int.Parse(sRecentDate.Substring(0, 2)));
                    dNewDate = dNewDate.AddMonths(1);
                    dePayrollFor.DateTime = dNewDate;
                    dePeriodFrom.DateTime = dePeriodFrom.Properties.MinValue = dePeriodTo.Properties.MinValue = dNewDate.AddDays(1);
                    bEnable = false;
                }

            }
            dePayrollFor.Enabled = bEnable;
            ShowMessage((clsGeneral.PAYROLL_ID > 0));
        }

        public void ProcessPayroll()
        {
            this.Cursor = Cursors.WaitCursor;

            long nPayRollId = objPrGateWay.PayRollId;
            if (nPayRollId == 0)
            {
                this.Cursor = Cursors.Default;
                return;
            }
            fraProgress.Visible = true;
            fraProgress.Refresh();
            
            
            //23/01/2017, Importa repreocesss for all componetns, so to make first group alone..--------------------------------------------------
            string firstgroup = string.Empty;
            using (clsPayrollGrade ClsGrade = new clsPayrollGrade())
            {
                DataTable dtGrpList = ClsGrade.getPayrollGradeList();
                if (dtGrpList != null && dtGrpList.Rows.Count > 0)
                {
                    if (dtGrpList.Rows.Count > 0)
                    {
                        firstgroup = dtGrpList.Rows[0]["Group Id"].ToString();
                    }
                }
            }
            //------------------------------------------------------------------------------------------------------------------------
            //Arguments- PayRollId,GroupIds,Optional sCompIds,Optional StaffIds,Optional bNewPayRoll As Boolean,objProgress
            clsprCompBuild objCompBuild = new clsprCompBuild();
            objCompBuild.ProcessComponent(nPayRollId, firstgroup, "", "", true, fraProgress, false,true, true);
            //------------------------------------------------------------------------------------------------------------------------------------

            fraProgress.Visible = false;
            this.Cursor = Cursors.Default;

        }

        private void ShowMessage(bool bImport)
        {
            lblMessage.Text = "";
            if (bImport)
            {
                //lblMessage.Text = "Import Payroll from " + clsGeneral.PAYROLL_MONTH;
                lblMessage.Text = this.GetMessage(MessageCatalog.Payroll.CreatePayroll.CREATE_PAYROLL_IMPORT_PAYROLLFROM_INFO) + clsGeneral.PAYROLL_MONTH;
                //btnCreate.Text = "Impo<U>&r</U>t";
                btnCreate.Text =this.GetMessage(MessageCatalog.Payroll.CreatePayroll.CREATE_PAYROLL_IMPORT_CAPTION);
                dePeriodFrom.Enabled = dePeriodTo.Enabled = false;
            }
            else
            {
                lblMessage.Text = PayrollMessage + " " + Enum.GetName(typeof(Month), dePayrollFor.DateTime.Month) + " " + dePayrollFor.DateTime.Year;
                //btnCreate.Text = "C<U>&r</U>eate";
                btnCreate.Text =this.GetMessage(MessageCatalog.Payroll.CreatePayroll.CREATE_PAYROLL_CREATE_CAPTION);
                //dePeriodFrom.Enabled = dePeriodTo.Enabled = true;
            }

            // RJ
            // Purpose : To Load the start date and End date for the payroll Period  Date.
            // 1. Get the selected month
            string sMonth = Enum.GetName(typeof(Month), dePayrollFor.DateTime.Month);
            string beginDate = string.Empty;
            string endDate = string.Empty;
            // Get the Start date as 1 in the selected month/
            DateTime startDate = DateTime.Parse("01/" + dePayrollFor.DateTime.Month + "/" + dePayrollFor.DateTime.Year, clsGeneral.DATE_FORMAT);
            beginDate = startDate.ToShortDateString();
            DateTime finishDate = startDate;
            // Add one month in the start date.
            finishDate = finishDate.AddMonths(1);
            // Reduce one day from the month to get the last date of the previous month.
            finishDate = finishDate.AddDays(-1);

            dePeriodFrom.DateTime = startDate; // Get 
            dePeriodTo.DateTime = finishDate;
        }

        public void SetValues()
        {
            dtStatus = objClsGrade.getPayrollMonth();
            if (dtStatus != null && dtStatus.Rows.Count > 0)
            {
                clsGeneral.PAYROLL_MONTH = dtStatus.Rows[0][2].ToString();
                clsGeneral.PAYROLL_ID = Convert.ToInt32(dtStatus.Rows[0][0]);
            }
            else
            {
                clsGeneral.PAYROLL_MONTH = "";
                clsGeneral.PAYROLL_ID = 0;
            }

            objPrGateWay.PRName = "";
            clsGeneral.PAYROLLDATE = "";
            if (clsGeneral.PAYROLL_ID != 0)
            {
                objPrGateWay.PRName = objModPay.GetValue("PRCREATE", "PRNAME", "PAYROLLID = " + clsGeneral.PAYROLL_ID);
                clsGeneral.PAYROLLDATE = objModPay.GetValue("PRCREATE", "DATE_FORMAT(PRDATE, '%d/%m/%Y') as PRDATE", "PAYROLLID = " + clsGeneral.PAYROLL_ID);
            }

        }
        #endregion
    }
}