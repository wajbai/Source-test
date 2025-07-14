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
using Bosco.Utility.Validations;
using System.Text.RegularExpressions;
using Payroll.Model;
using Payroll.Model.UIModel;
using Payroll.DAO.Schema;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;


namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        #region Varaible Declaration
        private clsPrGateWay objPRGW = new clsPrGateWay();
        public DataTable dtStatus = new DataTable();
        public clsPayrollGrade objClsGrade = new clsPayrollGrade();
        private clsModPay objModPay = new clsModPay();

        #endregion

        public frmMain()
        {
            InitializeComponent();
        }

        public void SetValues()
        {
            dtStatus = objClsGrade.getPayrollMonth();
            if (dtStatus != null && dtStatus.Rows.Count > 0)
            {
                SettingProperty.PayrollMonth = lblPayrollMonth.Text = dtStatus.Rows[0][2].ToString();
                clsGeneral.PAYROLL_MONTH = dtStatus.Rows[0][2].ToString();
                clsGeneral.PAYROLL_ID = Convert.ToInt32(dtStatus.Rows[0][0]);
            }
            else
            {
                clsGeneral.PAYROLL_ID = 0;
                clsGeneral.PAYROLL_MONTH = "";
                SettingProperty.PayrollMonth = lblPayrollMonth.Text = "";
            }
        }
        private void SetRecentPayRoll()
        {
            objPRGW.PayRollId = objPRGW.GetCurrentPayroll();
            clsGeneral.PAYROLL_ID = objPRGW.PayRollId;
            clsGeneral.PAYROLLDATE = objPRGW.PRDate;

        }
        private void SetPayRollCaption()
        {
            objPRGW.PRName = "";
            clsGeneral.PAYROLLDATE = "";
            if (objPRGW.PayRollId != 0)
            {
                objPRGW.PRName = objModPay.GetValue("PRCREATE", "PRNAME", "PAYROLLID = " + clsGeneral.PAYROLL_ID);
                clsGeneral.PAYROLLDATE = objModPay.GetValue("PRCREATE", "DATE_FORMAT(PRDATE, '%d/%m/%Y') as PRDATE", "PAYROLLID = " + clsGeneral.PAYROLL_ID);
            }
            if (objPRGW.PRName != "")
                SettingProperty.PayrollMonth = lblPayrollMonth.Text = objPRGW.PRName.ToString();
            clsGeneral.PAYROLL_MONTH = objPRGW.PRName;

            if (clsGeneral.PAYROLL_MONTH == "" & clsGeneral.PAYROLL_ID == 0)
            {
                lblPayrollMonth.Text = "No Payroll exist!...";
                nbgDefinition.Visible = nbgAllotement.Visible = nbgStaff.Visible = nbgReports.Visible = false;
                nbiDelete.Visible = nbiLock.Visible = nbiOpen.Visible = nbiReprocess.Visible = false;
                lblcurrPayroll.Visible = false;
                CloseAllFormInMDI();
            }
            else
            {
                nbgDefinition.Visible = nbgAllotement.Visible = nbgStaff.Visible = nbgReports.Visible = true;
                nbiDelete.Visible = nbiLock.Visible = nbiOpen.Visible = nbiReprocess.Visible = true;
                lblcurrPayroll.Visible = true;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            EnableMenu();
            if (!IsPayrollExists())
            {
                DialogResult dr = MessageBox.Show("Payroll Does not Exists.Do you want to create?", "Payroll", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    frmCreatePayroll createpayroll = new frmCreatePayroll();
                    createpayroll.ShowDialog();
                }
            }
            SettingProperty.PayrollMonth = lblPayrollMonth.Text = clsGeneral.PAYROLL_MONTH;
            //frmOpenPayroll openpayroll = new frmOpenPayroll();
            //openpayroll.UpdateHeld += new EventHandler(OnUpdateHeld);
            //openpayroll.ShowDialog();



            frmPayrollview Payrollview = new frmPayrollview();
            Payrollview.MdiParent = this;
            Payrollview.Show();

            SetRecentPayRoll();
            SetPayRollCaption();
            SetValues();
        }
        public virtual void OnUpdateHeld(object sender, EventArgs e)
        {
            frmPayrollview payrollview = new frmPayrollview();
            payrollview.LoadData();
            //gvComponentDetails.FocusedRowHandle = RowIndex;
        }
        private void nbiCreate_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(frmPayrollview).Name);
            CloseFormInMDI(typeof(frmPayrollview).Name);
            frmPayrollview PayView = new frmPayrollview();
            PayView.MdiParent = this;
            lblCaption.Text = PayView.Text;
            PayView.Show();
            frmCreatePayroll createpayroll = new frmCreatePayroll();
            createpayroll.ShowDialog();
            lblcurrPayroll.Text = "Payroll for ";

            SetRecentPayRoll();
            SetPayRollCaption();

        }

        private void mbiPayrollComponent_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(frmComponentView).Name);
            if (!hasHome)
            {
                frmComponentView componentview = new frmComponentView();
                componentview.MdiParent = this;
                lblCaption.Text = componentview.Text;
                componentview.Show();

            }
        }

        private void nbiPayrollGroup_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(frmPayrollGroupView).Name);
            if (!hasHome)
            {
                frmPayrollGroupView paygrp = new frmPayrollGroupView();
                paygrp.MdiParent = this;
                lblCaption.Text = paygrp.Text;
                paygrp.Show();
            }
        }

        private void nbiGroupAllocation_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            frmGroupAllocation Groupallocation = new frmGroupAllocation();
            Groupallocation.ShowDialog();
        }

        private void nbiComponentAllocation_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            frmComponentAllocation componentallocation = new frmComponentAllocation();
            componentallocation.ShowDialog();
        }

        private void mbiPayroll_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //bool hasHome = HasFormInMDI(typeof(frmPayrollBrowseView).Name);
            //if (!hasHome)
            //{
            //    frmPayrollBrowseView PayrollView = new frmPayrollBrowseView();
            //    PayrollView.MdiParent = this;
            //    PayrollView.Show();
            //    lblCaption.Text = PayrollView.Text;
            //}
            CloseForm(typeof(frmPayrollBrowseView).Name);
            frmPayrollBrowseView PayrollView = new frmPayrollBrowseView();
            PayrollView.MdiParent = this;
            PayrollView.Show();
            lblCaption.Text = PayrollView.Text;
        }
        private void CloseForm(string FormName)
        {
            foreach (Form frmActive in this.MdiChildren)
            {
                if (frmActive.Name.ToLower() == FormName.ToLower())
                    frmActive.Close();
            }

        }
        private void nbiLoans_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(frmLoanView).Name);
            if (!hasHome)
            {
                frmLoanView LoanView = new frmLoanView();
                LoanView.MdiParent = this;
                LoanView.Show();
                lblCaption.Text = LoanView.Text;
            }
        }

        private void nbiLoanManagement_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(frmLoanManagementView).Name);
            if (!hasHome)
            {
                frmLoanManagementView LoanManagement = new frmLoanManagementView();
                LoanManagement.MdiParent = this;
                LoanManagement.Show();
                lblCaption.Text = LoanManagement.Text;
            }
        }

        private void nbiStaffDetails_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(frmStaffView).Name);
            if (!hasHome)
            {
                frmStaffView Staff = new frmStaffView();
                Staff.MdiParent = this;
                Staff.Show();
                lblCaption.Text = Staff.Text;
            }
        }

        private void nbiStaffOrder_ItemChanged(object sender, EventArgs e)
        {
            frmStaffOrder stafforder = new frmStaffOrder();
            stafforder.ShowDialog();
        }

        private void nbiOpen_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(frmPayrollview).Name);
            CloseFormInMDI(typeof(frmPayrollview).Name);
            frmOpenPayroll OpenPayroll = new frmOpenPayroll(this);
            OpenPayroll.UpdateHeld += new EventHandler(OnUpdateHeld);
            OpenPayroll.ShowDialog();
            frmPayrollview PayView = new frmPayrollview();
            PayView.MdiParent = this;
            lblCaption.Text = PayView.Text;
            PayView.Show();
            lblPayrollMonth.Text = SettingProperty.PayrollMonth;
            lblcurrPayroll.Text = "Payroll for ";
            EnableMenu();
        }

        private void CloseFormInMDI(string formName)
        {
            bool hasForm = false;
            foreach (Form frmActive in this.MdiChildren)
            {
                hasForm = (frmActive.Name.ToLower() == formName.ToLower());

                if (hasForm)
                {
                    frmActive.Close();
                    break;
                }

                //  frmActive.Select();
            }

            //return hasForm;
        }

        private void CloseAllFormInMDI()
        {
            foreach (Form frmActive in this.MdiChildren)
            {
                if (frmActive.Name.ToLower() != typeof(frmPayrollview).Name.ToLower())
                {
                    frmActive.Close();
                }
            }
        }
        private void nbiLock_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(frmPayrollview).Name);
            CloseFormInMDI(typeof(frmPayrollview).Name);
            frmPayrollview PayView = new frmPayrollview();
            PayView.MdiParent = this;
            lblCaption.Text = PayView.Text;
            PayView.Show();
            frmLockPayroll LockPayroll = new frmLockPayroll();
            LockPayroll.ShowDialog();
            EnableMenu();
        }
        private void EnableMenu()
        {
            if (objModPay.GetValue("PRSTATUS", "lockedstatus", "PayRollId = " + clsGeneral.PAYROLL_ID) == "Y")
            {
                nbiLock.Caption = "Unlock";
                nbgDefinition.Visible = nbgAllotement.Visible = nbgStaff.Visible = false;
                nbiDelete.Visible = nbiReprocess.Visible = false;
                lblPayrollMonth.Text += " is Locked";
                CloseAllFormInMDI();

            }
            else
            {
                nbiLock.Caption = "Lock";
                nbgDefinition.Visible = nbgAllotement.Visible = nbgStaff.Visible = true;
                nbiDelete.Visible = nbiReprocess.Visible = true;
                lblPayrollMonth.Text = SettingProperty.PayrollMonth;
            }

        }

        private void nbiDelete_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(frmPayrollview).Name);
            CloseFormInMDI(typeof(frmPayrollview).Name);
            frmPayrollview PayView = new frmPayrollview();
            PayView.MdiParent = this;
            lblCaption.Text = PayView.Text;
            PayView.Show();
            if (SettingProperty.dtOpen != null)
            {
                if (MessageBox.Show("Are you sure to delete the current payroll.", "Payroll", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (new clsModPay().GetValue("PRSTATUS", "lockedstatus", "PayRollId = " + clsGeneral.PAYROLL_ID) == "N")
                    {
                        if (objPRGW.DeletePayRoll(clsGeneral.PAYROLL_ID))
                        {
                            MessageBox.Show("Current payroll is deleted", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SetValues();
                            //SetTreeView();
                            SetRecentPayRoll();
                            SetPayRollCaption();
                            //chkListGrade_SelectedIndexChanged(new object(), new EventArgs());
                            //FillPayrollInfo();
                            //dgPayrollProcess.DataSource = null;
                            //   fillGridValues();	
                            lblcurrPayroll.Text = "Payroll for ";
                        }
                        else
                            MessageBox.Show("Current payroll can not be deleted.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Current payroll is locked unable to delete.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Payroll is not Available", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private bool HasFormInMDI(string formName)
        {
            bool hasForm = false;

            foreach (Form frmActive in this.MdiChildren)
            {
                hasForm = (frmActive.Name.ToLower() == formName.ToLower());

                if (hasForm)
                {
                    frmActive.Select();
                    break;
                }
            }
            return hasForm;
        }
        public bool IsPayrollExists()
        {
            bool IsExists = true;
            dtStatus = objClsGrade.getPayrollMonth();
            if (dtStatus == null)
            {
                IsExists = false;
            }
            else
            {
                clsGeneral.PAYROLL_ID = Convert.ToInt32(dtStatus.Rows[0][0]);
            }
            return IsExists;
        }

        private void nbiReprocess_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(frmPayrollview).Name);
            CloseFormInMDI(typeof(frmPayrollview).Name);
            frmPayrollview PayView = new frmPayrollview();
            PayView.MdiParent = this;
            lblCaption.Text = PayView.Text;
            PayView.Show();
            frmReprocess process = new frmReprocess(clsGeneral.PAYROLL_ID);
            process.ShowDialog();
        }

        private void xtraTabbedMdiManager1_PageRemoved(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
        {

        }

        private void xtraTabbedMdiManager1_PageRemoved_1(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
        {
            if (e.Page.MdiChild.GetType().Name == typeof(frmMain).Name)
            {
                if (xtraTabbedMdiManager1.Pages.Count == 0)
                {
                    Application.Exit();
                }
            }
        }
        /// <summary>
        /// Report Menu 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiPayRegister_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //Payroll.frmPRRptPayReg objRptPayReg = new Payroll.frmPRRptPayReg(0, clsGeneral.PAYROLL_ID, clsGeneral.PAYROLL_MONTH);
            //objRptPayReg.ShowDialog();
        }

        private void nbiLoanLedger_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //Payroll.frmPRRptPayReg objRptPayReg = new Payroll.frmPRRptPayReg(3, clsGeneral.PAYROLL_ID, clsGeneral.PAYROLL_MONTH);
            //objRptPayReg.ShowDialog();
        }

        /// <summary>
        /// Payslip Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiPaySlip_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            frmPaySlipViewer frmpayslip = new frmPaySlipViewer();
            frmpayslip.ShowDialog();
        }

        private void nbiCustomizeReport_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //Payroll.frmCustomizeReport paySlipViewer = new Payroll.frmCustomizeReport(); //change form name
            //try
            //{
            //    paySlipViewer.ShowDialog();
            //}
            //catch
            //{

            //}
        }

        /// <summary>
        /// Abstract Payroll 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiAbstractPayroll_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //frmPRRptPayReg objRptPayReg = new frmPRRptPayReg(1, clsGeneral.PAYROLL_ID, clsGeneral.PAYROLL_MONTH);
            //objRptPayReg.ShowDialog();
        }

        /// <summary>
        /// Daily Report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbipfDailyReport_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //frmReportViewer objDailyReport = new frmReportViewer(clsGeneral.PAYROLL_ID, clsGeneral.PAYROLL_MONTH, 1);
            //objDailyReport.ShowDialog();
        }

        /// <summary>
        /// Monthly Report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbipfmonthlyreport_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //frmReportViewer objMonthlyReport = new frmReportViewer(clsGeneral.PAYROLL_ID, clsGeneral.PAYROLL_MONTH, 3);
            //objMonthlyReport.ShowDialog();
        }

        /// <summary>
        /// Yearly Report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbipfYearlyReport_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //frmReportViewer objYearlyReport = new frmReportViewer(clsGeneral.PAYROLL_ID, clsGeneral.PAYROLL_MONTH, 4);
            //objYearlyReport.ShowDialog();
        }

        /// <summary>
        /// Employee Report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbipfEmployeeReport_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //frmReportViewer objEmployeeReport = new frmReportViewer(clsGeneral.PAYROLL_ID, clsGeneral.PAYROLL_MONTH, 2);
            //objEmployeeReport.ShowDialog();
        }

        /// <summary>
        /// Abstract Component Report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiAbstractComponent_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //frmPRRptPayReg objRptPayReg = new frmPRRptPayReg(2, clsGeneral.PAYROLL_ID, clsGeneral.PAYROLL_MONTH);
            //objRptPayReg.ShowDialog();
        }

        /// <summary>
        /// Wages Report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiWagesReport_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //frmPRRptPayReg objRptPayReg = new frmPRRptPayReg(6, clsGeneral.PAYROLL_ID, clsGeneral.PAYROLL_MONTH);
            //objRptPayReg.ShowDialog();
        }

        
    }
}