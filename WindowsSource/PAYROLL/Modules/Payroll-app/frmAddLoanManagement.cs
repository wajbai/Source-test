using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using Payroll.Model.UIModel;
using Bosco.Model.Transaction;
using System.Text.RegularExpressions;
using Bosco.Utility.Common;
using Microsoft.VisualBasic;

namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmAddLoanManagement : frmPayrollBase
    {
        #region VariableDeclaration
        public event EventHandler UpdateHeld;
        ResultArgs resultArgs = null;
        public int LoanMgtId = 0;
        //private string LoanManagementAdd = "LoanManagement (Add)";
        //private string LoanManagementEdit = "LoanManagement (Edit)";
        private clsPrLoan PayrollLoan = new clsPrLoan();
        private string strOperation = "";
        private double Payment;
        private Regex objR = new Regex("[0-9]|\b");
        private string sLoanStr = "";
        private long nPRLoanId;

        private clsLoanManagement objLoanMnt = new clsLoanManagement();
        private clsPayrollStaff objpaystaff = new clsPayrollStaff();
        private double dPayableIntrest = 0;
        private bool bLoanEdit;
        TransProperty Transaction = new TransProperty();
        CommonMember comboset = new CommonMember();
        UserProperty LoginUser = new UserProperty();
        int loanEditid = 0;
        int LoanStaffId = 0;
        public bool VisibleFlyPane = false;
        #endregion

        #region Constructor
        public frmAddLoanManagement()
        {
            InitializeComponent();

        }
        public frmAddLoanManagement(int Id)
            : this()
        {
            LoanMgtId = Id;
            AssignLoanDetails();
            //btnView.Enabled = (Id == 0) ? false : true;

        }
        #endregion

        #region Property
        private int staffId = 0;
        private int StaffId
        {
            get { return staffId; }
            set { staffId = value; }
        }
        private int Projectid { get; set; }
        private int LoangetId { get; set; }
        private int CurrentInst { get; set; }
        #endregion

        #region Events
        private void frmAddLoanManagement_Load(object sender, EventArgs e)
        {
            //this.Text = LoanMgtId == 0 ? LoanManagementAdd : LoanManagementEdit;
            this.Text = LoanMgtId == 0 ? this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_ADD_CAPTION) : this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_EDIT_CAPTION);
            ItemHide();
            EnableControls();
            LoadData();
            GetStaffDetails();
            GetLoanDetails();
            SetTitle();
            //ReallyCenterToScreen();
            //this.StartPosition = FormStartPosition.CenterScreen;

            //---changed  by sugan 
            //int Width = this.Width = 450;
            //int Height = this.Height = 500;

            //int boundWidth = Screen.PrimaryScreen.Bounds.Width;
            //int boundHeight = Screen.PrimaryScreen.Bounds.Height;
            //int x = boundWidth - this.Width;
            //int y = boundHeight - this.Height;
            //this.Location = new Point(x / 2, y / 2);//----------
        }



        public void ItemHide()
        {
            //this.Width = 450;
            //this.StartPosition = FormStartPosition.CenterScreen;

            //---changed by sugan //layoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lblNote.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (LoanMgtId != 0 && CurrentInst != 0)
            {
                btnView.Enabled = true;
                btnSave.Enabled = false;
                lblNote.Text = "Locked";
                lblNote.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                btnView.Enabled = true;
                btnSave.Enabled = true;
                this.CenterToScreen();
            }
        }

        private void EnableControls()
        {
            if (SettingProperty.PayrollFinanceEnabled == true)
            {
                lciCashBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

            }
            else
            {
                lciCashBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                // this.Height = this.Height - 24;
            }
        }
        /// <summary>
        /// Loading Data
        /// </summary>
        private void LoadData()
        {
            if (LoanMgtId == (int)AddNewRow.NewRow)
            {
                dtPayfrom.DateTime = this.comboset.DateSet.ToDate(clsGeneral.PAYROLLDATE, false);
                //dtPayfrom.Text = DateTime.Parse(clsGeneral.PAYROLLDATE, clsGeneral.DATE_FORMAT).ToString();
                dtPayto.DateTime = this.comboset.DateSet.ToDate(clsGeneral.PAYROLLDATE, false);
            }
            // pnlInstallment.Visible = false;
            // lblUserName.Text = clsGeneral.USER_NAME;
            dtPayfrom.Enabled = true;
            dtPayto.Enabled = false;
        }

        /// <summary>
        /// Get Staff Details
        /// </summary>
        public void GetStaffDetails()
        {
            try
            {
                using (clsLoanManagement loanmgt = new clsLoanManagement())
                {
                    resultArgs = loanmgt.GetStaff();
                    if (resultArgs.Success)
                    {
                        comboset.ComboSet.BindGridLookUpCombo(glkpStaff, resultArgs.DataSource.Table, "FIRST_NAME", "STAFF_ID");
                        glkpStaff.EditValue = glkpStaff.Properties.GetKeyValue(0);
                        if (LoanMgtId > 0)
                        {
                            glkpStaff.EditValue = LoanStaffId;
                        }
                    }
                    else
                    {
                        MessageRender.ShowMessage(resultArgs.Message, true);
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
        /// Select Change Event of Radio Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rgModeOfInterest_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rgModeOfInterest.SelectedIndex == 0)
            {
                if (txtRateOfInterest.Text == "0" || txtRateOfInterest.Text == "0.00")
                {
                    //ShowMessageBox("Enter the interest rate");
                    //XtraMessageBox.Show("Rate of interest is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_RATE_INTEREST_EMTPY));
                    SetBorderColor(txtRateOfInterest);
                    txtRateOfInterest.Focus();
                }
                txtRateOfInterest.Enabled = true;
            }
            else if (rgModeOfInterest.SelectedIndex == 1)
            {
                if (txtRateOfInterest.Text == "0" || txtRateOfInterest.Text == "0.00")
                {
                    //ShowMessageBox("Enter the interest rate");
                    //XtraMessageBox.Show("Rate of interest is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_RATE_INTEREST_EMTPY));
                    SetBorderColor(txtRateOfInterest);
                    txtRateOfInterest.Focus();
                }
                txtRateOfInterest.Enabled = true;

            }
            else if (rgModeOfInterest.SelectedIndex == 2)
            {
                if (txtRateOfInterest.Text == "0" || txtRateOfInterest.Text == "0.00")
                {
                    //ShowMessageBox("Enter the interest rate");
                    //XtraMessageBox.Show("Rate of interest is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_RATE_INTEREST_EMTPY));
                    SetBorderColor(txtRateOfInterest);
                    txtRateOfInterest.Focus();
                }
                txtRateOfInterest.Enabled = true;
            }
            else if (rgModeOfInterest.SelectedIndex == 3)
            {
                txtRateOfInterest.Text = Convert.ToInt32(0).ToString();
                txtRateOfInterest.Properties.Appearance.BorderColor = Color.Empty;
                txtRateOfInterest.Enabled = false;
            }
        }

        /// <summary>
        /// Form Title Add and Edit for the Loan Management
        /// </summary>
        public void SetTitle()
        {
            this.Text = LoanMgtId == 0 ? this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_ADD_CAPTION) : this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_EDIT_CAPTION);
        }

        /// <summary>
        /// Get Loan Details
        /// </summary>
        public void GetLoanDetails()
        {
            try
            {
                using (clsLoanManagement loanget = new clsLoanManagement())
                {
                    resultArgs = loanget.GetLoan();
                    if (resultArgs.Success)
                    {

                        comboset.ComboSet.BindGridLookUpCombo(glkpLoan, resultArgs.DataSource.Table, "LOAN_NAME", "LOAN_ID");

                        glkpLoan.EditValue = glkpLoan.Properties.GetKeyValue(0);
                        if (LoanMgtId > 0)
                        {
                            glkpLoan.EditValue = loanEditid;
                        }
                    }
                    else
                    {
                        MessageRender.ShowMessage(resultArgs.Message, true);
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
        /// Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtPayfrom_Click(object sender, EventArgs e)
        {
            dtPayfrom_Leave(sender, new EventArgs());
        }

        /// <summary>
        /// Save the Loan Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            int i;
            long nStaffId;
            long nLoanTypeId;
            try
            {
                if (ValidateLoanMgtDetails())
                {
                    txtRateOfInterest.Text = string.IsNullOrEmpty(txtRateOfInterest.Text) ? Convert.ToInt32(0).ToString() : txtRateOfInterest.Text;
                    int iMonth = Int32.Parse(txtNoOfInstallments.Text.Trim());
                    dtPayto.Text = dtPayfrom.DateTime.AddMonths(iMonth).ToShortDateString();
                    dtPayto.Enabled = false;
                    //dtPayto.Properties.Buttons[0].Visible = false;
                    //dtPayto.Properties.Buttons[2].Visible = true;
                    //dtPayto.Properties.Buttons[1].Image = PAYROLL.Properties.Resources.CheckBoxChecked;
                    dtPayto.Tag = true;

                    this.AssignValues();
                    nStaffId = long.Parse(glkpStaff.EditValue.ToString());
                    nLoanTypeId = long.Parse(glkpLoan.EditValue.ToString());
                    DataTable dtloanExists = PayrollLoan.GetLoanSQL(true, "PRLOANMNT");
                    if (LoanMgtId == (int)AddNewRow.NewRow)
                    {
                        for (i = 0; i < dtloanExists.Rows.Count; i++)
                        {
                            if (dtloanExists != null && dtloanExists.Rows.Count > 0)
                            {
                                if (dtloanExists.Rows[i]["STAFFID"].ToString() == glkpStaff.EditValue.ToString() && dtloanExists.Rows[i]["LOANID"].ToString() == glkpLoan.EditValue.ToString())
                                {
                                    //XtraMessageBox.Show("Loan exists already", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_EXITS_ALREADY_INFO));
                                    return;
                                }
                            }
                        }
                        bool result = PayrollLoan.SaveLoanObtain(this.sLoanStr, nPRLoanId, nLoanTypeId, nStaffId);
                        LoangetId = PayrollLoan.PRLoanGetId;
                        if (result == true)
                        {
                            if (SettingProperty.PayrollFinanceEnabled == true)
                            {
                                resultArgs = IsLoanLedgersaremappedwithProject();
                                if (resultArgs.Success)
                                {
                                    resultArgs = ProcessPaymententry();
                                    if (resultArgs.Success)
                                    {
                                        //this.ShowSuccessMessage("Record saved");
                                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_DETAILS_SAVED));
                                        DialogResult res = new DialogResult();
                                        if (res == DialogResult.OK)
                                        {
                                            ClearControls();
                                            // btnView.Enabled = false;

                                        }
                                    }
                                }
                            }
                            else
                            {
                                //this.ShowSuccessMessage("Record saved");
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_DETAILS_SAVED));
                                DialogResult res = new DialogResult();
                                if (res == DialogResult.OK)
                                {
                                    ClearControls();
                                    // btnView.Enabled = false;

                                }
                            }
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                        }
                        else
                        {
                            //DialogResult res = XtraMessageBox.Show("Record not saved", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DialogResult res = XtraMessageBox.Show(this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_DETAILS_SAVED));
                            if (res == DialogResult.OK)
                            {
                                ClearControls();
                                //  btnView.Enabled = false;

                            }

                        }
                    }
                    else
                    {
                        //   int nLoanId = int.Parse(ParentForm.ucGrdPayroll.ColValue(0));
                        int nLoanId = 1;//Convert.ToInt32(glkpLoan.EditValue.ToString());
                        string sWhere = "staffid= " + nStaffId + " and loanid=" + nLoanTypeId + " " +
                            "and completed=0  and prloangetid <> " + nLoanId;
                        //if (PayrollLoan.SaveLoanObtain(this.sLoanStr,LoanMgtId)   // if (objPRLoan.SaveLoanObtain(this.sLoanStr, long.Parse(ParentForm.ucGrdPayroll.ColValue(0)))) 
                        //{
                        bool result = PayrollLoan.SaveLoanObtain(this.sLoanStr, LoanMgtId);
                        LoangetId = PayrollLoan.PRLoanGetId;
                        if (result == true)
                        {
                            if (SettingProperty.PayrollFinanceEnabled == true)
                            {
                                resultArgs = DeleteVouchersbyPrloanId(LoanMgtId);
                                if (resultArgs.Success)
                                {
                                    resultArgs = IsLoanLedgersaremappedwithProject();
                                    if (resultArgs.Success)
                                    {
                                        resultArgs = ProcessPaymententry();
                                        if (resultArgs.Success)
                                        {
                                            //XtraMessageBox.Show("Record saved", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            //this.ShowSuccessMessage("Record saved");
                                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_DETAILS_SAVED));
                                            this.Close();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (resultArgs.Success)
                                {
                                    //XtraMessageBox.Show("Record saved", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //this.ShowSuccessMessage("Record saved");
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_DETAILS_SAVED));
                                    this.Close();
                                }
                            }
                        }
                        else
                        {
                            //XtraMessageBox.Show("Record not saved", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_DETAILS_NOT_SAVED));
                            this.Close();
                        }
                        //}
                    }
                }
                if (UpdateHeld != null)
                {
                    UpdateHeld(this, e);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Leave NO of Installments
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNoOfInstallments_Leave(object sender, EventArgs e)
        {
            SetBorderColor(txtNoOfInstallments);
            bool bAllowFromDate = true;
            if (dtPayfrom.Text == "")
                dtPayfrom.Text = clsGeneral.PAYROLLDATE;
            if (txtNoOfInstallments.Text != "")
            {
                if (txtNoOfInstallments.Text == "0")
                {
                    //XtraMessageBox.Show("No.of.installments should not be less than zero", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_NO_INSTALLMENT_GREATERTHAN_ZERO_INFO));
                    this.txtNoOfInstallments.Focus();
                }
                //SetBorderColor(txtNoOfInstallments);
                int iMonth = Int32.Parse(txtNoOfInstallments.Text.Trim());
                string sRetirementDate = objLoanMnt.getStaffRetirementDate(glkpStaff.EditValue.ToString());
                if (sRetirementDate != "")
                {
                    DateTime dtRetiremntDate = new DateTime(Convert.ToDateTime(sRetirementDate).Year, Convert.ToDateTime(sRetirementDate).Month, Convert.ToDateTime(sRetirementDate).Day);
                    DateTime dtPrDate = new DateTime(Convert.ToDateTime(clsGeneral.PAYROLLDATE).Year, Convert.ToDateTime(clsGeneral.PAYROLLDATE).Month, Convert.ToDateTime(clsGeneral.PAYROLLDATE).Day);

                    if (dtRetiremntDate <= dtPrDate)
                    {
                        //XtraMessageBox.Show("Staff is retired, can't provide the loan", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_RETIRED_STAFF_LOAN_NOTPROVIDE_INFO));
                        return;
                    }
                    if (dtRetiremntDate > dtPrDate)
                    {
                        dtPrDate = dtPrDate.Date.AddMonths(iMonth);
                        if (dtPrDate >= dtRetiremntDate)
                        {
                            TimeSpan dt = dtPrDate.Subtract(dtRetiremntDate.Date);
                            int interval = (dt.Days / 30);
                            interval = Convert.ToInt32(txtNoOfInstallments.Text) - (interval + 1);
                            //dtPayfrom.Enabled = false;
                            //bAllowFromDate = false;
                            dtPayfrom.Enabled = true;
                            bAllowFromDate = true;
                            //XtraMessageBox.Show("The maximum number of installement for this staff should be " + interval.ToString() + "\nBecause she/he will retire on " + dtRetiremntDate.ToShortDateString(), "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_MAXNO_INSTALLMENTS_SELECT_STAFF_INFO + interval.ToString() + this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_MAXNO_RETIREDON_INFO) + dtRetiremntDate.ToShortDateString()));
                            return;
                        }
                    }

                }
                if (bAllowFromDate)
                {
                    dtPayfrom.Enabled = true;
                }
                dtPayto.Text = dtPayfrom.DateTime.AddMonths(iMonth).ToShortDateString();
                dtPayto.Enabled = false;
                //dtPayto.Properties.Buttons[0].Visible = false;
                //dtPayto.Properties.Buttons[2].Visible = true;
                //dtPayto.Properties.Buttons[1].Image = PAYROLL.Properties.Resources.CheckBoxChecked;
                dtPayto.Tag = true;
                //dtPayto.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                //dtPayto.Properties.Appearance.BackColor = Color.White;
                if (LoanMgtId != 0)
                {
                    ShowInterest();
                }
            }
            else
                dtPayfrom.Enabled = true;
        }

        /// <summary>
        /// leave of DataFrom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtPayfrom_Leave(object sender, EventArgs e)
        {
            if (txtNoOfInstallments.Text != "")
            {
                if (txtNoOfInstallments.Text != "0")
                {
                    int iMonth = Int32.Parse(txtNoOfInstallments.Text.Trim());
                    DateTime Datefrom = this.comboset.DateSet.ToDate(dtPayfrom.Text, false);
                    dtPayto.DateTime = Datefrom.AddMonths(iMonth);

                }
                else
                {
                    //XtraMessageBox.Show("Installment should not be zero or negative value", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_MAXNO_INSTALL_GREATERTHAN_NEGATIVE_VALUE_INFO));
                    dtPayfrom.Focus();
                    // return;
                }
            }
            //else
            //{
            //    MessageBox.Show("Installment should not be empty.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtNoOfInstallments.Focus();
            //   // return;
            //}
        }

        /// <summary>
        /// View all the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            //if (LoanMgtId != 0)
            //{
            ///----Changed by sugan
            //layoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //this.Width = 619;
            //flyoutPanel1.ShowPopup();
            //if (ValidateLoanMgtDetails())
            //{
            //    lblloanamt.Text = txtAmount.Text;
            //    lblNoOfInstallmentsAmt.Text = txtNoOfInstallments.Text;
            //    lblRateOfIntrestAmt.Text = txtRateOfInterest.Text;
            //    ShowInterest();
            //}
            //if (VisibleFlyPane)
            //{
            //    layoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    flyoutPanel1.HidePopup();
            //    this.Width = 443;
            //    VisibleFlyPane = false;
            //}
            //else if (!VisibleFlyPane)
            //{
            //    layoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //    flyoutPanel1.ShowPopup();
            //    VisibleFlyPane = true;
            //}------

            //}
        }

        private void dtPayfrom_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void dtPayfrom_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //try
            //{
            //    if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)
            //    {
            //        if (Convert.ToBoolean(dtPayfrom.Tag))
            //        {
            //            dtPayfrom.Properties.Buttons[2].Visible = false;
            //            dtPayfrom.Properties.Buttons[0].Visible = true;
            //            dtPayfrom.Properties.Buttons[1].Image = PAYROLL.Properties.Resources.CheckBox;
            //            dtPayfrom.Tag = false;
            //            dtPayfrom.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            //        }
            //        else
            //        {
            //            dtPayfrom.Properties.Buttons[0].Visible = false;
            //            dtPayfrom.Properties.Buttons[2].Visible = true;
            //            dtPayfrom.Properties.Buttons[1].Image = PAYROLL.Properties.Resources.CheckBoxChecked;
            //            dtPayfrom.Tag = true;
            //            int iMonth = Int32.Parse(txtNoOfInstallments.Text.Trim());
            //            dtPayto.Text = string.Empty;
            //            dtPayto.Text = dtPayfrom.DateTime.AddMonths(iMonth).ToShortDateString();
            //            dtPayfrom.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            //            dtPayfrom.Properties.Appearance.BackColor = Color.White;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            //}
            //finally { }
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !objR.IsMatch(e.KeyChar.ToString());
        }

        private void txtNoOfInstallments_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !objR.IsMatch(e.KeyChar.ToString());
        }

        private void txtRateOfInterest_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !objR.IsMatch(e.KeyChar.ToString());
        }

        private void dtPayfrom_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //try
            //{
            //    if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)
            //    {
            //        if (Convert.ToBoolean(dtPayfrom.Tag))
            //        {
            //            dtPayfrom.Properties.Buttons[1].Image = PAYROLL.Properties.Resources.CheckBox;
            //            dtPayfrom.Tag = false;
            //            dtPayfrom.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            //        }
            //        else
            //        {
            //            dtPayfrom.Properties.Buttons[1].Image = PAYROLL.Properties.Resources.CheckBoxChecked;
            //            dtPayfrom.Tag = true;
            //            dtPayfrom.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            //            dtPayfrom.Properties.Appearance.BackColor = Color.White;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            //}
            //finally { }
        }
        private void dtPayto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)
                {
                    if (Convert.ToBoolean(dtPayto.Tag))
                    {
                        dtPayto.Properties.Buttons[2].Visible = false;
                        dtPayto.Properties.Buttons[0].Visible = true;
                        dtPayto.Properties.Buttons[1].Image = PAYROLL.Properties.Resources.CheckBox;
                        dtPayto.Tag = false;
                        dtPayto.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                    }
                    else
                    {
                        dtPayto.Properties.Buttons[0].Visible = false;
                        dtPayto.Properties.Buttons[2].Visible = true;
                        dtPayto.Properties.Buttons[1].Image = PAYROLL.Properties.Resources.CheckBoxChecked;
                        dtPayto.Tag = true;
                        dtPayto.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                        dtPayto.Properties.Appearance.BackColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void glkpLoan_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                LoadLoanCategory();
            }
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            SetBorderColor(txtAmount);
            if (LoanMgtId != 0)
            {
                ShowInterest();
            }
            //if (txtAmount.Text != "")
            //{
            //    if (txtAmount.Text == "0.00")
            //    {
            //        XtraMessageBox.Show("Loan amount should not be less than zero", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        SetBorderColor(txtAmount);
            //        this.txtAmount.Focus();
            //    }
            //}
        }
        private void txtRateOfInterest_Leave(object sender, EventArgs e)
        {
            SetBorderColor(txtRateOfInterest);
            if (LoanMgtId != 0)
            {
                ShowInterest();
            }
        }

        private void glkpStaff_EditValueChanged(object sender, EventArgs e)
        {
            LoadCashBankLedgers();
            LoadStaffDetails();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Clear Controls
        /// </summary>
        private void ClearControls()
        {
            txtAmount.Text = string.Empty;
            txtNoOfInstallments.Text = string.Empty;
            txtRateOfInterest.Text = string.Empty;
            lblloanamt.Text = string.Empty;
            lblRateOfIntrestAmt.Text = string.Empty;
            lblPayableAmt.Text = string.Empty;
            lblNoOfInstallmentsAmt.Text = string.Empty;
            gvInstallmentAmt.Columns.Clear();
        }

        private void LoadCashBankLedgers()
        {
            try
            {
                if (!string.IsNullOrEmpty(glkpStaff.EditValue.ToString()))
                {
                    StaffId = comboset.NumberSet.ToInteger(glkpStaff.EditValue.ToString());
                    Projectid = objpaystaff.GetProjectidByStaffId(StaffId.ToString());
                    resultArgs = objLoanMnt.FetchCashBankLedgersofpayrollProjects(Projectid);
                    if (resultArgs.Success)
                    {
                        comboset.ComboSet.BindGridLookUpCombo(glkpCashBank, resultArgs.DataSource.Table, "LEDGER_NAME", "LEDGER_ID");
                        if (LoanMgtId == (int)AddNewRow.NewRow)
                        {
                            glkpCashBank.EditValue = glkpCashBank.Properties.GetKeyValue(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        private void LoadLoanCategory()
        {
            frmLoanAdd Loanadd = new frmLoanAdd();
            Loanadd.ShowDialog();
            GetLoanDetails();
        }

        /// <summary>
        /// Assign Values
        /// </summary>
        private void AssignValues()
        {
            //lblLoanAmt.Text       = txtAmount.Text;
            //lblNoInstallment.Text = txtNoInstallment.Text;
            //lblRateofIntrest.Text = txtRateOfIntrest.Text;
            ShowInterest();
            //pnlInstallment.Visible = false;
            sLoanStr = "STAFFID|" + Convert.ToInt32(glkpStaff.EditValue.ToString()) + "@" +
                "LOANID|" + Convert.ToInt32(glkpLoan.EditValue.ToString()) + "@" +
                "AMOUNT|" + Convert.ToInt32(txtAmount.Text) + "@" +
                "INSTALLMENT|" + Convert.ToInt32(txtNoOfInstallments.Text) + "@" +
                "FROMDATE|" + DateTime.Parse(dtPayfrom.Text).ToShortDateString() + "@";
            //c
            //if ( dpPayTo.Checked == true)
            if (dtPayto.Enabled == false)
                sLoanStr = sLoanStr + "TODATE|" + DateTime.Parse(dtPayto.DateTime.ToShortDateString()).ToShortDateString() + "@";
            else
                sLoanStr = sLoanStr + "TODATE|" + "" + "@";
            sLoanStr = sLoanStr + "INTREST|" + Convert.ToDouble(txtRateOfInterest.Text.Trim()) + "@";
            if (rgModeOfInterest.SelectedIndex == 3)
                sLoanStr = sLoanStr + "INTRESTMODE|" + "3@";
            if (rgModeOfInterest.SelectedIndex == 1)
                sLoanStr = sLoanStr + "INTRESTMODE|" + "1@";
            if (rgModeOfInterest.SelectedIndex == 2)
                sLoanStr = sLoanStr + "INTRESTMODE|" + "2@";
            if (rgModeOfInterest.SelectedIndex == 0)
                sLoanStr = sLoanStr + "INTRESTMODE|" + "0@";
            sLoanStr = sLoanStr + "INTRESTAMOUNT|" + dPayableIntrest; //Modified by PE
            sLoanStr = sLoanStr + "@CURRENTINSTALLMENT|0@COMPLETED|0@";
        }

        private void ShowInterest()
        {
            int Installment;
            double Interest;
            double Amount;
            double PayableAmt = 0.0;

            //-----if ((txtRateOfInterest.Text == "") && (!(rgModeOfInterest.SelectedIndex == 3)))
            //{
            //    XtraMessageBox.Show("Rate of interest is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtRateOfInterest.Focus();
            //    return;
            //}  --------changed by sugan

            //pnlInstallment.Visible = true;
            Interest = Convert.ToDouble(txtRateOfInterest.Text) / 100;  //to be corrected 10/02
            Amount = Convert.ToDouble(txtAmount.Text);
            Installment = Convert.ToInt32(txtNoOfInstallments.Text);
            DataTable dt = new DataTable();
            dt.Columns.Add("Installment");
            dt.Columns.Add("Amount");
            DataRow dr;
            int j = 1;
            for (int i = 1; i <= Installment; i++)
            {
                if (rgModeOfInterest.SelectedIndex == 0)
                    Payment = Math.Round(Microsoft.VisualBasic.Financial.Pmt(Interest / 12, Installment, -Amount, 0, DueDate.EndOfPeriod), 2);
                if (rgModeOfInterest.SelectedIndex == 1)
                    Payment = Math.Round((Amount / Installment) + Microsoft.VisualBasic.Financial.IPmt(Interest / 12, i, Installment, -Amount, 0, DueDate.EndOfPeriod), 2);
                if (rgModeOfInterest.SelectedIndex == 2)
                    Payment = Math.Round(Microsoft.VisualBasic.Financial.PPmt(Interest / 12, i, Installment, -Amount, 0, DueDate.EndOfPeriod), 2);
                if (rgModeOfInterest.SelectedIndex == 3)
                    Payment = Math.Round(Amount / Installment, 2);

                dr = dt.NewRow();
                dr[0] = i.ToString();
                dr[1] = Payment.ToString();
                dt.Rows.Add(dr);
                j++;
                PayableAmt = PayableAmt + Payment;
            }
            lblloanamt.Text = Amount.ToString();
            lblPayableAmt.Text = PayableAmt.ToString();
            lblRateOfIntrestAmt.Text = Interest.ToString();
            lblNoOfInstallmentsAmt.Text = Installment.ToString();
            gcInstallmentAmount.DataSource = dt.DefaultView;
            dPayableIntrest = (Convert.ToDouble(lblPayableAmt.Text)) - Amount;
        }

        /// <summary>
        /// Purpose:To validate the mandatory fields.
        /// </summary>
        /// <returns></returns>
        public bool ValidateLoanMgtDetails()
        {
            bool isLoan = true;
            if (string.IsNullOrEmpty(glkpStaff.Text.Trim()))
            {
                //ShowMessageBox("Staff Name is empty");
                //XtraMessageBox.Show("Staff name is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_STAFF_NAME_EMPTY));
                this.SetBorderColor(glkpStaff);
                glkpStaff.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(glkpLoan.Text.Trim()))
            {
                //ShowMessageBox("Loan is empty");
                //XtraMessageBox.Show("Loan name is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_LOAN_NAME_EMPTY));
                SetBorderColor(glkpLoan);
                glkpLoan.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtAmount.Text) || txtAmount.Text == "0.00" || txtAmount.Text == "0")
            {
                //ShowMessageBox("Enter the amount");
                //XtraMessageBox.Show("Loan amount is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_LOAN_AMOUNT_EMPTY));
                SetBorderColor(txtAmount);
                txtAmount.Focus();
                return false;
            }
            //else if (string.IsNullOrEmpty(txtNoOfInstallments.Text))
            //{
            //    //ShowMessageBox("Instalment should not be empty and zero or negative value.");
            //    XtraMessageBox.Show("Instalment should not be empty and zero or negative value.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    SetBorderColor(txtNoOfInstallments);
            //    isLoan = false;
            //    txtNoOfInstallments.Focus();
            //}
            else if (string.IsNullOrEmpty(txtNoOfInstallments.Text))
            {
                //ShowMessageBox("Enter the interest rate");
                //XtraMessageBox.Show("Loan installments is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_LOAN_INSTALLMENT_EMPTY));
                SetBorderColor(txtNoOfInstallments);
                txtNoOfInstallments.Focus();
                return false;
            }
            else if (rgModeOfInterest.SelectedIndex != 3)
            {
                if (string.IsNullOrEmpty(txtRateOfInterest.Text) || (txtRateOfInterest.Text == "0.00" || txtRateOfInterest.Text == "0"))
                {
                    //ShowMessageBox("Enter the interest rate");
                    //XtraMessageBox.Show("Rate of interest is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_LOAN_RATE_INTEREST_EMPTY));
                    SetBorderColor(txtRateOfInterest);
                    txtRateOfInterest.Focus();
                    return false;
                }
            }
            else if (string.IsNullOrEmpty(dtPayfrom.Text))
            {
                //ShowMessageBox("Enter the pay from date");
                //XtraMessageBox.Show("Pay From date is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_PAYFROM_DATE_EMPTY));
                SetBorderColor(glkpLoan);
                dtPayto.Focus();
                return false;

            }
            else if (dtPayfrom.DateTime > dtPayto.DateTime)
            {
                //ShowMessageBox("Pay to date should be greater than the Pay From Date");
                //XtraMessageBox.Show("Pay to date should be greater than the Pay From Date", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_PAYTODATE_GREATERTHAN_PAYFROMDATE_INFO));
                dtPayto.Focus();
                return false;
            }
            if (dtPayfrom.DateTime < comboset.DateSet.ToDate(clsGeneral.PAYROLLDATE, false))
            {
                //XtraMessageBox.Show("Loan cannot be provided for the Past days! Check the Pay From date", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_LOAN_CANNOT_PROVIDE_PAST_DAYS));
                dtPayfrom.Focus();
                return false;
            }
            if (dtPayto.DateTime < comboset.DateSet.ToDate(clsGeneral.PAYROLLDATE, false))
            {
                //XtraMessageBox.Show("Loan cannot be provided for the Past days! Check the Pay To date", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_LOAN_CANNOT_PROVIDE_PAST_DAYS_CHECK_PAYTO_DATE_INFO));
                dtPayto.Focus();
                return false;
            }
            if (SettingProperty.PayrollFinanceEnabled == true)
            {
                if (string.IsNullOrEmpty(glkpCashBank.Text.Trim()))
                {
                    //XtraMessageBox.Show("Cash/Bank Ledger is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_LOAN_CASHBANK_LEDGER_EMPTY));
                    this.SetBorderColor(glkpCashBank);
                    glkpCashBank.Focus();
                    return false;
                }
            }
            else
            {
                glkpStaff.Focus();
            }
            return isLoan;
        }

        /// <summary>
        /// To Assign Value to Controls
        /// </summary>
        private void AssignLoanDetails()
        {
            try
            {
                if (LoanMgtId != 0)
                {
                    using (clsLoanManagement PayrollLoanMgt = new clsLoanManagement(LoanMgtId))
                    {
                        glkpCashBank.EditValue = SettingProperty.PayrollFinanceEnabled == true ? PayrollLoanMgt.CashBank : 0;
                        glkpStaff.EditValue = LoanStaffId = PayrollLoanMgt.StaffId;
                        glkpLoan.EditValue = loanEditid = PayrollLoanMgt.LoanId;
                        txtAmount.Text = Convert.ToString(PayrollLoanMgt.Amount);
                        txtNoOfInstallments.Text = Convert.ToString(PayrollLoanMgt.NoInstallment);
                        txtRateOfInterest.Text = Convert.ToString(PayrollLoanMgt.IntrestRate);
                        dtPayfrom.EditValue = PayrollLoanMgt.PayFrom;
                        rgModeOfInterest.SelectedIndex = PayrollLoanMgt.IntrestMode;
                        dtPayto.EditValue = PayrollLoanMgt.PayTo;
                        CurrentInst = PayrollLoanMgt.CurrentInstallment;
                        ShowInterest();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
            finally { }
        }

        /// <summary>
        /// Clear the Controls
        /// </summary>
        private void ClearControl()
        {
            txtAmount.Text = "";
        }

        /// <summary>
        /// Set Border for textBox
        /// </summary>
        /// <param name="txtEdit"></param>
        protected void SetBorderColor(TextEdit txtEdit)
        {
            txtEdit.Properties.Appearance.BorderColor = string.IsNullOrEmpty(txtEdit.Text) ? Color.Red : Color.Empty;
        }

        /// <summary>
        /// To get Message and Display
        /// </summary>
        /// <param name="Msg"></param>
        //protected void ShowMessageBox(string Msg)
        //{
        //    XtraMessageBox.Show(Msg, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //}

        /// <summary>
        /// To delete loan management vouchers
        /// </summary>
        /// <param name="Projectid"></param>
        /// <returns></returns>
        public ResultArgs DeleteVouchersbyPrloanId(int Prloangetid)
        {
            try
            {
                using (clsPrLoan prloan = new clsPrLoan())
                {
                    resultArgs = prloan.DeleteVoucherTransByLoangetId(Prloangetid);
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        resultArgs = prloan.DeleteVoucherMasterTransByLoangetid(Prloangetid);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return resultArgs;
        }
        /// <summary>
        /// To add the payemnt entry for staff loan
        /// </summary>
        /// <returns></returns>
        private ResultArgs ProcessPaymententry()
        {
            try
            {
                using (clsprCompBuild objCompBuild = new clsprCompBuild())
                {

                    using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                    {
                        //Voucher Master Details
                        voucherTransaction.VoucherId = 0;
                        voucherTransaction.ProjectId = Projectid;
                        voucherTransaction.VoucherDate = comboset.DateSet.ToDate(dtPayfrom.Text, false); ; ; // this.UtilityMember.DateSet.ToDate(clsGeneral.PAYROLLDATE, false);
                        voucherTransaction.VoucherType = "PY";
                        voucherTransaction.Status = (int)YesNo.Yes;
                        voucherTransaction.TransVoucherMethod = (int)TransactionVoucherMethod.Automatic;
                        voucherTransaction.CreatedOn = comboset.DateSet.ToDate(comboset.DateSet.GetDateToday(false), false);
                        voucherTransaction.ModifiedOn = comboset.DateSet.ToDate(comboset.DateSet.GetDateToday(false), false);
                        voucherTransaction.CreatedBy = comboset.NumberSet.ToInteger(this.LoginUser.LoginUserId.ToString());
                        voucherTransaction.ModifiedBy = comboset.NumberSet.ToInteger(this.LoginUser.LoginUserId.ToString());
                        //voucherTransaction.Narration = "Payroll Processed for the " + dtTempProcess.Rows[Ptype]["Name"].ToString() + " Process type for the Month of " + clsGeneral.PAYROLL_MONTH;
                        //voucherTransaction.Narration = "Payroll loan for the Month of " + clsGeneral.PAYROLL_MONTH;
                        voucherTransaction.Narration = this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_LOAN_MONTH_INFO) + clsGeneral.PAYROLL_MONTH;
                        voucherTransaction.VoucherSubType = ledgerSubType.PAY.ToString();
                        voucherTransaction.ClientReferenceId = LoangetId.ToString();
                        //Voucher Trans Details
                        Transaction.TransInfo = ConstructTransData();
                        Transaction.CashTransInfo = ConstructCashBankData();
                        resultArgs = voucherTransaction.SaveTransactions();
                    }
                }

            }
            catch (Exception ed)
            {
                resultArgs.Message = ed.Message;
            }
            return resultArgs;
        }
        /// <summary>
        /// Empty data source for payroll ledger
        /// </summary>
        /// <returns></returns>
        private DataTable ConstructTransEmptySource()
        {
            DataTable dtTransaction = new DataTable();
            dtTransaction.Columns.Add("SOURCE", typeof(string));
            dtTransaction.Columns.Add("LEDGER_ID", typeof(Int32));
            dtTransaction.Columns.Add("AMOUNT", typeof(decimal));
            dtTransaction.Columns.Add("CHEQUE_NO", typeof(string));
            dtTransaction.Columns.Add("MATERIALIZED_ON", typeof(DateTime));
            dtTransaction.Columns.Add("LEDGER_BALANCE", typeof(string));
            dtTransaction.Columns.Add("BUDGET_AMOUNT", typeof(string));
            dtTransaction.Columns.Add("TEMP_AMOUNT", typeof(decimal));
            return dtTransaction;
        }
        /// <summary>
        /// Empty data source for Cash/Bank ledger
        /// </summary>
        /// <returns></returns>
        private DataTable ConstructCashTransEmptySource()
        {
            DataTable dtCashTransaction = new DataTable();
            dtCashTransaction.Columns.Add("SOURCE", typeof(string));
            dtCashTransaction.Columns.Add("LEDGER_FLAG", typeof(string));
            dtCashTransaction.Columns.Add("LEDGER_ID", typeof(Int32));
            dtCashTransaction.Columns.Add("AMOUNT", typeof(decimal));
            dtCashTransaction.Columns.Add("CHEQUE_NO", typeof(string));
            dtCashTransaction.Columns.Add("MATERIALIZED_ON", typeof(DateTime));
            dtCashTransaction.Columns.Add("LEDGER_BALANCE", typeof(string));
            dtCashTransaction.Columns.Add("BUDGET_AMOUNT", typeof(string));
            dtCashTransaction.Columns.Add("TEMP_AMOUNT", typeof(decimal));
            return dtCashTransaction;
        }
        /// <summary>
        /// To update the voucher transaction data(General ledgers)
        /// </summary>
        /// <returns></returns>
        private DataView ConstructTransData()
        {
            DataView dvTransdata = new DataView();
            DataTable dtTransdata = ConstructTransEmptySource();
            DataTable dtPayrollLedger = new DataTable();
            using (LedgerSystem ledgersystm = new LedgerSystem())
            {
                dtPayrollLedger = ledgersystm.FetchLedgerDetailsById(1001).DataSource.Table;
                if (ValidateLoanMgtDetails())
                {
                    if (dtPayrollLedger != null && dtPayrollLedger.Rows.Count > 0)
                    {
                        dtTransdata.Rows.Add("", comboset.NumberSet.ToInteger(dtPayrollLedger.Rows[0]["LEDGER_ID"].ToString()), comboset.NumberSet.ToDecimal(txtAmount.Text), " ", DBNull.Value, "", "", 0.00);
                    }
                }
            }
            dvTransdata = dtTransdata.DefaultView;
            return dvTransdata;
        }
        /// <summary>
        /// To update the voucher transaction data(Cash/Bank)
        /// </summary>
        /// <returns></returns>
        private DataView ConstructCashBankData()
        {
            DataView dvCashBankdata = new DataView();
            DataTable dtCashBankdata = ConstructCashTransEmptySource();
            if (ValidateLoanMgtDetails())
            {
                dtCashBankdata.Rows.Add("", "", comboset.NumberSet.ToInteger(glkpCashBank.EditValue.ToString()), comboset.NumberSet.ToDecimal(txtAmount.Text), " ", DBNull.Value, "", "", 0.00);
            }
            dvCashBankdata = dtCashBankdata.DefaultView;
            return dvCashBankdata;
        }
        private ResultArgs IsLoanLedgersaremappedwithProject()
        {

            using (clsprCompBuild objCompBuild = new clsprCompBuild())
            {
                int ConfLedger = objCompBuild.IsLoanCompLedgerMappedwithProject(Projectid.ToString());
                if (ConfLedger == 0)
                {
                    using (clsPrComponent objComp = new clsPrComponent())
                    {
                        resultArgs = objComp.MapProjectLedger("1001", Projectid.ToString());
                        resultArgs = objComp.MapProjectLedger("1002", Projectid.ToString());
                        //AcMELog.WriteLog("Payroll.Mapping Ledger with the Project ended.");
                        AcMELog.WriteLog(this.GetMessage(MessageCatalog.Payroll.LoanManagement.LOAN_MGT_LEDGER_MAPPINGWITH_PROJECT_END_INFO));
                    }
                }
            }
            return resultArgs;
        }
        private void LoadLoanInterestValues()
        {
            if (ValidateLoanMgtDetails())
            {
                lblloanamt.Text = txtAmount.Text;
                lblNoOfInstallmentsAmt.Text = txtNoOfInstallments.Text;
                lblRateOfIntrestAmt.Text = txtRateOfInterest.Text;
                ShowInterest();
            }

        }

        private void LoadStaffDetails()
        {
            DataTable dtStaff = PayrollLoan.FetchLoantaffDetailsById(staffId).DataSource.Table;
            string staffdetails = string.Empty;
            foreach (DataRow drstaff in dtStaff.Rows)
            {
                lblGender.Text = drstaff["GENDER"].ToString();
                lblAge.Text = drstaff["AGE"].ToString();
                lblGroup.Text = drstaff["STAFF_GROUP"].ToString();
                lblDesignation.Text = drstaff["DESIGNATION"].ToString();
            }
        }
        #endregion

        private void glkpCashBank_Leave(object sender, EventArgs e)
        {
        }
    }
}

