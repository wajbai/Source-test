using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.Serialization;
using System.Linq;
using Bosco.Model.Business;
using Bosco.Model.Transaction;
using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using Bosco.Utility.Common;
using Bosco.Utility.CommonMemberSet;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Base;
using Payroll.Model.UIModel;
using Payroll.DAO;
using DevExpress.XtraEditors.Mask;


namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmPostPayment : frmPayrollBase
    {
        #region Variable Decalration
        ResultArgs resultArgs = null;
        SettingProperty AppSetting = new SettingProperty();
        public event EventHandler UpdateHeld;
        #endregion

        #region Constructor
        public frmPostPayment()
        {
            InitializeComponent();
        }
        public frmPostPayment(int PostPayemntid)
            : this()
        {
            PostId = PostPayemntid;
            //AssignPostVoucherDetails();
        }
        #endregion

        #region Properties
        private int postid = 0;
        private int PostId
        {
            get { return postid; }
            set { postid = value; }
        }
        private int ProjectId { get; set; }
        private int GroupId { get; set; }
        private decimal amount { get; set; }
        private double sumAmount { get; set; }
        private DataTable dtConsturct { get; set; }

        private double tmpEditValue { get; set; }

        #endregion

        #region Methods
        /// <summary>
        /// To fetch projects to process the payroll
        /// </summary>
        /// <returns>Returns the datatable which contains project,projectid as columns</returns>
        private ResultArgs FetchProjectDetails()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    mappingSystem.ProjectClosedDate = dteDate.Text;
                    Int32 Prevprojectid = glkpProject.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : this.UtilityMember.NumberSet.ToInteger(this.AppSetting.UserProjectId);

                    resultArgs = mappingSystem.FetchProjectsLookup();
                    if (resultArgs.DataSource != null)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, "PROJECT", "PROJECT_ID");
                        //if (PostId == 0) { 
                        //    //glkpProject.EditValue = glkpProject.Properties.GetKeyValue(0); 
                        //    glkpProject.EditValue = (glkpProject.Properties.GetDisplayValueByKeyValue(ProjectId) != null ? ProjectId : glkpProject.Properties.GetKeyValue(0));   
                        //} 
                        //else 
                        //{ 
                        //    glkpProject.EditValue = ProjectId; 
                        //}
                        glkpProject.EditValue = (glkpProject.Properties.GetDisplayValueByKeyValue(Prevprojectid) != null ? Prevprojectid : 0);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
            return resultArgs;
        }
        /// <summary>
        /// To Validate the Post payment detials 
        /// </summary>
        /// <returns>Returns false when there is any empty or correct field value </returns>
        private bool ValidatePostPaymentDetails()
        {
            bool isValid = true;
            //DataTable dtPostAmount = (DataTable)gcTransaction.DataSource;
            if (string.IsNullOrEmpty(glkpGroup.Text.Trim()))
            {
                //XtraMessageBox.Show(MessageCatalog.Payroll.PostPayment.POST_GROUP_EMPTY, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PostPayment.POST_GROUP_EMPTY));
                this.SetBorderColor(glkpGroup);
                isValid = false;
                glkpGroup.Focus();
            }
            else if (string.IsNullOrEmpty(glkpProject.Text.Trim()))
            {
                //XtraMessageBox.Show(MessageCatalog.Payroll.PostPayment.POST_PROJECT_EMPTY, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PostPayment.POST_PROJECT_EMPTY));
                this.SetBorderColor(glkpProject);
                isValid = false;
                glkpProject.Focus();
            }
            else if (string.IsNullOrEmpty(dteDate.Text.Trim()))
            {
                //XtraMessageBox.Show(MessageCatalog.Payroll.PostPayment.POST_DATE_EMPTY, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PostPayment.POST_DATE_EMPTY));
                this.SetBorderColor(dteDate);
                isValid = false;
                dteDate.Focus();
            }
            else if (this.UtilityMember.NumberSet.ToDouble(txtCompAmount.Text.Trim()) == 0.00)
            {
                //XtraMessageBox.Show(MessageCatalog.Payroll.PostPayment.POST_AMOUNT_EMPTY, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PostPayment.POST_AMOUNT_EMPTY));
                this.SetBorderColor(txtCompAmount);
                isValid = false;
                txtCompAmount.Focus();
            }

            //else if (gcTransaction.DataSource == null)
            //{
            //    XtraMessageBox.Show(MessageCatalog.Payroll.PostPayment.GRID_EMPTY, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    isValid = false;
            //}
            //else if (dtPostAmount != null && dtPostAmount.Rows.Count > 0)
            //{
            //    double SumAmount = UtilityMember.NumberSet.ToDouble(dtPostAmount.AsEnumerable().Sum(r => r.Field<decimal>("AMOUNT")).ToString());
            //    if (UtilityMember.NumberSet.ToDouble(txtCompAmount.Text.Trim()) != SumAmount)
            //    {
            //        XtraMessageBox.Show(MessageCatalog.Payroll.PostPayment.AMOUNT_MISMATCH, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        this.SetBorderColor(txtPayAmount);
            //        isValid = false;
            //        txtPayAmount.Focus();
            //    }
            //}
            else if (string.IsNullOrEmpty(glkpLedgers.Text.Trim()))
            {
                //XtraMessageBox.Show(MessageCatalog.Payroll.PostPayment.LEDGER_EMPTY, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PostPayment.LEDGER_EMPTY));
                this.SetBorderColor(glkpLedgers);
                isValid = false;
                glkpLedgers.Focus();
            }
            else if (string.IsNullOrEmpty(glkpCashBank.Text.Trim()))
            {
                //XtraMessageBox.Show(MessageCatalog.Payroll.PostPayment.POST_CASHBANK_EMPTY, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PostPayment.POST_CASHBANK_EMPTY));
                this.SetBorderColor(glkpCashBank);
                isValid = false;
                glkpCashBank.Focus();
            }

            //******************************Added by sugan--to validate post amount if it exceeds actual amount*****************************************************************
            using (clsPrGateWay objprgateway = new clsPrGateWay())
            {
                if (PostId == 0) // add
                {
                    sumAmount = objprgateway.FetchSumofPostvoucheramountBypayrollid();
                    if ((sumAmount + this.UtilityMember.NumberSet.ToDouble(txtCompAmount.Text)) > this.UtilityMember.NumberSet.ToDouble(amount.ToString()))
                    {
                        //DialogResult diares = XtraMessageBox.Show("Post amount exceeds actual amount. Do you want to processd?", "Payroll", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        DialogResult diares = XtraMessageBox.Show(this.GetMessage(MessageCatalog.Payroll.PostPayment.POST_AMOUNT_EXCEEDS_INFO), this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (diares == DialogResult.Yes)
                        {
                            isValid = true;
                        }
                        else
                        {
                            isValid = false;
                            txtCompAmount.Focus();
                        }
                    }
                }
                else if (PostId != 0) //edit 
                {
                    sumAmount = objprgateway.ValidateFetchSumofPostvoucheramountBypayrollid();
                    sumAmount = sumAmount - tmpEditValue;
                    sumAmount += this.UtilityMember.NumberSet.ToDouble(txtCompAmount.Text);

                    if (sumAmount > this.UtilityMember.NumberSet.ToDouble(lblTotalAmount.Text))
                    {
                        //DialogResult diares = XtraMessageBox.Show("Post amount exceeds actual amount. Do you want to proceed?", "Payroll", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        DialogResult diares = XtraMessageBox.Show(this.GetMessage(MessageCatalog.Payroll.PostPayment.POST_AMOUNT_EXCEEDS_INFO), this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (diares == DialogResult.Yes)
                        {
                            isValid = true;

                        }
                        else
                        {
                            isValid = false;
                            txtCompAmount.Focus();
                        }
                    }
                }

            }
            return isValid;
        }
        //*************************************************************************************************************************************************************************
        /// <summary>
        /// To load the components which does not conatain equations (i.e, Unrelated components)
        /// </summary>
        private void LoadComponents()
        {
            try
            {
                using (clsPrGateWay objgateway = new clsPrGateWay())
                {
                    DataTable dtComponents = objgateway.FetchComponentsUnrelatedComponents(GroupId).DataSource.Table;
                    if (dtComponents != null && dtComponents.Rows.Count > 0)
                    {
                        if (!dtComponents.Columns.Contains("SELECT"))
                        {
                            dtComponents.Columns.Add("SELECT", typeof(Int32));
                        }
                        if (dtComponents.Columns.Contains("SELECT"))
                        {
                            foreach (DataRow dr in dtComponents.Rows)
                            {
                                dr["SELECT"] = 0;
                            }
                        }
                        //  gcMapComponent.DataSource = dtComponents;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }

        }
        /// <summary>
        /// to load expense ledgers
        /// </summary>
        private void Loadledgers()
        {
            try
            {
                using (clsPrGateWay objgateway = new clsPrGateWay())
                {
                    resultArgs = GroupId == 0 ? objgateway.FetchExpenseLedgersByProjectId(ProjectId) : objgateway.FetchLiabilityLedgersByProjectId(ProjectId);
                    if (resultArgs.DataSource != null)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpLedgers, resultArgs.DataSource.Table, "LEDGER_NAME", "LEDGER_ID");
                        //if (PostId == 0)
                        //{
                        //glkpLedgers.EditValue = glkpLedgers.Properties.GetKeyValue(0);
                        //}
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }
        /// <summary>
        /// To load cash/bank ledgers
        /// </summary>
        private void LoadCashBankLedgers()
        {
            try
            {
                using (clsLoanManagement objLoanManagement = new clsLoanManagement())
                {
                    objLoanManagement.BankClosedDate = dteDate.Text;
                    resultArgs = objLoanManagement.FetchCashBankLedgersofpayrollProjects(ProjectId);
                    if (resultArgs.DataSource != null)
                    {
                        Int32 PrevCashBankLedgerId = glkpCashBank.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpCashBank.EditValue.ToString()) : 0;

                        DataTable dtCashBank = resultArgs.DataSource.Table;
                        dtCashBank.DefaultView.RowFilter = "(DATE_CLOSED >='" + dteDate.Text + "' OR DATE_CLOSED IS NULL) AND " +
                                                               "(DATE_OPENED <='" + dteDate.Text + "' OR DATE_OPENED IS NULL)";
                        dtCashBank = dtCashBank.DefaultView.ToTable();

                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpCashBank, dtCashBank, "LEDGER_NAME", "LEDGER_ID");
                        //if (PostId == 0) { glkpCashBank.EditValue = glkpCashBank.Properties.GetKeyValue(0); }
                        glkpCashBank.EditValue = glkpCashBank.Properties.GetDisplayValueByKeyValue(PrevCashBankLedgerId) != null ? PrevCashBankLedgerId : 0;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        /// <summary>
        /// To load the component groups such as Income or Deduction type
        /// </summary>
        private void LoadComponentTypes()
        {
            using (clsPrGateWay objgateway = new clsPrGateWay())
            {
                DataTable dtTypes = new DataTable();
                dtTypes.Columns.Add("COMPONENTID", typeof(UInt32));
                dtTypes.Columns.Add("COMPONENT", typeof(string));
                dtTypes.Rows.Add(0, "Salary");
                DataTable dtDeduction = objgateway.FetchDeductionComponents(1).DataSource.Table;
                if (dtDeduction != null && dtDeduction.Rows.Count > 0)
                {
                    dtTypes.Merge(dtDeduction);
                }
                this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpGroup, dtTypes, "COMPONENT", "COMPONENTID");
                if (PostId == 0) { glkpGroup.EditValue = glkpGroup.Properties.GetKeyValue(0); }
            }

        }

        /// <summary>
        /// To load the post payment details which helps to load the temporary data in grid 
        /// </summary>
        /// <returns></returns>
        private DataTable ConstructFields()
        {
            try
            {
                dtConsturct = new DataTable();
                DataColumn dc = dtConsturct.Columns.Add("POST_ID", typeof(int));
                dc.AutoIncrement = true;
                dc.AutoIncrementSeed = 1;
                dc.AutoIncrementStep = 1;
                dtConsturct.Columns.Add("PAYROLL_ID", typeof(int));
                dtConsturct.Columns.Add("PRNAME", typeof(string));
                dtConsturct.Columns.Add("LEDGER_ID", typeof(int));
                dtConsturct.Columns.Add("LEDGER", typeof(string));
                dtConsturct.Columns.Add("AMOUNT", typeof(decimal));
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message, true);
            }
            finally { }
            return dtConsturct;
        }
        /// <summary>
        /// To save the post payment details and payment voucher details by assigning the input values to properties in clsprGateway
        /// </summary>
        private void SavePostDetails()
        {
            try
            {
                using (clsPrGateWay objprgateway = new clsPrGateWay())
                {
                //    objprgateway.postid = PostId == 0 ? (int)AddNewRow.NewRow : PostId;
                //    objprgateway.TypeId = GroupId = this.UtilityMember.NumberSet.ToInteger(glkpGroup.EditValue.ToString());
                //    objprgateway.ProjectId = ProjectId;
                //    objprgateway.CashBankLedgerId = this.UtilityMember.NumberSet.ToInteger(glkpCashBank.EditValue.ToString());
                //    objprgateway.NetAmount = UtilityMember.NumberSet.ToDecimal(txtCompAmount.Text);
                //    objprgateway.CashBankLedgerName = glkpCashBank.Properties.GetDisplayValueByKeyValue(UtilityMember.NumberSet.ToInteger(glkpCashBank.EditValue.ToString())).ToString();

                //    objprgateway.postDate = UtilityMember.DateSet.ToDate(dteDate.EditValue.ToString(), false);
                //    objprgateway.postLedgerId = this.UtilityMember.NumberSet.ToInteger(glkpLedgers.EditValue.ToString());
                //    objprgateway.postLedgerName = glkpLedgers.Properties.GetDisplayValueByKeyValue(UtilityMember.NumberSet.ToInteger(glkpLedgers.EditValue.ToString())).ToString();
                //    objprgateway.postAmount = UtilityMember.NumberSet.ToDecimal(txtCompAmount.Text);
                //    objprgateway.Narration = txtNarration.Text;
                //    resultArgs = objprgateway.SavePostPaymentDetails();

                //    if (resultArgs.Success)
                //    {
                //        if (PostId == 0)
                //        {
                //            resultArgs = objprgateway.SaveVoucherDetails();
                //        }
                //        else
                //        {
                //            int VoucherId = objprgateway.FetchVoucherIdByPostId();
                //            resultArgs = objprgateway.DeleteVoucherDetails(VoucherId);
                //            if (resultArgs.Success == true)
                //            {
                //                resultArgs = objprgateway.SaveVoucherDetails();
                //            }

                //        }
                //        if (resultArgs.Success)
                //        {
                //            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.Staff.STAFF_DETAILS_SAVED));
                //        }
                //        else
                //        {
                //            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.PayrollGroup.GROUP_DETAILS_NOT_SAVED));
                //        }
                //    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message, true);
            }
            finally { }
        }

        /// <summary>
        /// To clear the vlaues after the post the voucher
        /// </summary>
        private void ClearControls()
        {
            if (PostId == 0)
            {
                dteDate.EditValue = this.UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false);
                txtCompAmount.Text = txtNarration.Text = string.Empty;
                glkpLedgers.EditValue = glkpCashBank.EditValue = string.Empty;
            }
            //LoadComponents();
            //  gcTransaction.DataSource = null;
        }
        /// <summary>
        /// To load the default date value 
        /// </summary>
        private void SetDate()
        {
            //this.Text = PostId == 0 ? "Post Voucher(Add)" : "Post Voucher(Edit)";
            this.Text = PostId == 0 ? this.GetMessage(MessageCatalog.Payroll.PostPayment.POST_ADD_CAPTION) : this.GetMessage(MessageCatalog.Payroll.PostPayment.POST_EDIT_CAPTION);
            //lblPayMonth.Text = "Payroll for " + clsGeneral.PAYROLL_MONTH;
            lblPayMonth.Text = this.GetMessage(MessageCatalog.Payroll.PostPayment.POST_PAYROLL_MONTH_FOR_INFO) + clsGeneral.PAYROLL_MONTH;
            dteDate.Properties.MinValue = this.UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false);
            dteDate.Properties.MaxValue = this.UtilityMember.DateSet.ToDate(AppSetting.YearTo, false);
        }
        /// <summary>
        /// To load the assign values for controls when edit the post amount
        /// </summary>
        private void AssignPostVoucherDetails()
        {
            using (clsPrGateWay objprgateway = new clsPrGateWay(PostId))
            {
                //glkpGroup.EditValue = objprgateway.TypeId;
                //dteDate.EditValue = objprgateway.postDate;
                //glkpProject.EditValue = objprgateway.ProjectId;
                //glkpLedgers.EditValue = objprgateway.postLedgerId;
                //glkpCashBank.EditValue = objprgateway.CashBankLedgerId;
                //txtCompAmount.Text = this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(objprgateway.postAmount.ToString())).ToString();
                //tmpEditValue = this.UtilityMember.NumberSet.ToDouble(objprgateway.postAmount.ToString());
                //txtNarration.Text = objprgateway.Narration;
            }
        }

        /// <summary>
        /// To load the balance amount of select component type after post the voucher
        /// </summary>
        private void LoadPostamount()
        {
            try
            {

                using (clsPrGateWay objgateway = new clsPrGateWay())
                {
                    ////Check wheather post payment exists for selected type and Current payroll id
                    //objgateway.TypeId = GroupId;
                    //resultArgs = objgateway.CheckPostPaymentExists();
                    //if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    //{
                    //    decimal Assignamount = amount - this.UtilityMember.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0]["AMOUNT"].ToString());
                    //    //  lblCurrentbalance.Text += lblCurrentbalance.Text;
                    //    string tmpamt = this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(Assignamount.ToString()));
                    //    if (PostId == 0)
                    //    {
                    //        txtCompAmount.Text = lblCurrentbalance.Text = tmpamt;
                    //    }
                    //    else
                    //    {
                    //        lblCurrentbalance.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(tmpamt.ToString()));
                    //    }
                    //}
                    //else
                    //{
                    //    //lblCurrentbalance.Text += lblCurrentbalance.Text;
                    //    if (PostId == 0)
                    //    {
                    //        txtCompAmount.Text = lblCurrentbalance.Text = this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(amount.ToString()));
                    //    }
                    //    else
                    //    {
                    //        lblCurrentbalance.Text = this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(amount.ToString()));
                    //    }
                    //}
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message, true);
            }
            finally { }
        }
        #endregion

        #region Events
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidatePostPaymentDetails())
            {
                SavePostDetails();
                ClearControls();
                if (UpdateHeld != null)
                {
                    UpdateHeld(this, e);
                }
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmPostPayment_Load(object sender, EventArgs e)
        {
            try
            {
                SetDate();
                LoadComponentTypes();
                FetchProjectDetails();
                //  Loadledgers();
                LoadCashBankLedgers();
                if (PostId > 0)
                {
                    AssignPostVoucherDetails();
                }
                Loadledgers();
                LoadCashBankLedgers();
                LoadPostamount();
                //LoadComponents();

            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message, true);
            }
            finally { }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (ValidatePostAmountDetails())
            //    {
            //        dtConsturct = (DataTable)gcTransaction.DataSource;
            //        dtConsturct = dtConsturct == null || dtConsturct.Rows.Count == 0 ? ConstructFields() : dtConsturct;
            //        int Ledgerid = UtilityMember.NumberSet.ToInteger(glkpLedgers.EditValue.ToString());
            //        string LedgerName = glkpLedgers.Properties.GetDisplayValueByKeyValue(UtilityMember.NumberSet.ToInteger(glkpLedgers.EditValue.ToString())).ToString();
            //        dtConsturct.Rows.Add(null,clsGeneral.PAYROLL_ID,clsGeneral.PAYROLL_MONTH,Ledgerid, LedgerName, txtPayAmount.Text);
            //        gcTransaction.DataSource = dtConsturct;
            //        txtPayAmount.Text = string.Empty;
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    MessageRender.ShowMessage(Ex.Message, true);
            //}
            //finally { }
        }

        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                ProjectId = (glkpProject.EditValue==null? 0 : this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()));
                Loadledgers();
                LoadCashBankLedgers();
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message, true);
            }
            finally { }
        }

        private void glkpGroup_EditValueChanged(object sender, EventArgs e)
        {
            try
            {

                GroupId = this.UtilityMember.NumberSet.ToInteger(glkpGroup.EditValue.ToString());
                Loadledgers();
                using (clsPrGateWay objprgateway = new clsPrGateWay())
                {
                    amount = GroupId == 0 ? objprgateway.FetchProcessValuesByComponentId(GroupId) : objprgateway.FetchProcessValuesofDeductionComponents(GroupId);
                    lblTotalAmount.Text = this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(amount.ToString()));
                    txtCompAmount.Text = this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(amount.ToString()));
                }
                LoadPostamount();
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message, true);
            }
            finally { }
        }

        private void rchkEdit_CheckedChanged(object sender, EventArgs e)
        {
            ////try
            ////{
            ////    string SelectedComponentId = string.Empty;
            ////    using (clsPrGateWay objprgateway = new clsPrGateWay())
            ////    {
            ////int rowid = gvMapComponent.FocusedRowHandle;
            ////        DataTable dtSource = (DataTable)gcMapComponent.DataSource;
            ////        if (dtSource != null && dtSource.Rows.Count > 0)
            ////        {
            ////            if (dtSource.Rows[rowid]["SELECT"].ToString() == "1")
            ////            {
            ////                dtSource.Rows[rowid]["SELECT"] = 0;
            ////                UpdateBalance(SelectedComponentId, objprgateway, dtSource);
            ////            }
            ////            else
            ////            {
            ////                dtSource.Rows[rowid]["SELECT"] = 1;
            ////                UpdateBalance(SelectedComponentId, objprgateway, dtSource);
            ////            }
            ////        }
            ////    }
            //}
            //catch (Exception Ex)
            //{
            //    MessageRender.ShowMessage(Ex.Message, true);
            //}
            //finally { }
        }

        private void UpdateBalance(string SelectedComponentId, clsPrGateWay objprgateway, DataTable dtSource)
        {
            try
            {
                var SelectedItems = (from MapComponents in dtSource.AsEnumerable()
                                     where (MapComponents.Field<Int32?>("SELECT").Equals(1))
                                     select MapComponents);
                if (SelectedItems.Count() > 0)
                {
                    DataTable dtSelectedComponent = SelectedItems.CopyToDataTable();
                    if (dtSelectedComponent != null && dtSelectedComponent.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtSelectedComponent.Rows)
                        {
                            SelectedComponentId += dr["COMPONENTID"].ToString() + ",";
                        }
                    }
                    SelectedComponentId = SelectedComponentId.TrimEnd(',');
                    //int amount = objprgateway.FetchProcessValuesByComponentId(SelectedComponentId, GroupId);
                    //txtCompAmount.Text = amount.ToString();
                }
                else
                {
                    txtCompAmount.Text = string.Empty;
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message, true);
            }
            finally { }
        }

        //private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        using (clsPrGateWay objPrgateway = new clsPrGateWay())
        //        {
        //            string SelectAllComponets = string.Empty;
        //            DataTable dtComponents = gcMapComponent.DataSource as DataTable;
        //            foreach (DataRow drComp in dtComponents.Rows)
        //            {
        //                if (chkSelectAll.Checked)
        //                {
        //                    drComp["SELECT"] = 1;
        //                }
        //                else
        //                {
        //                    drComp["SELECT"] = 0;

        //                }
        //            }
        //            UpdateBalance(SelectAllComponets, objPrgateway, dtComponents);
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        MessageRender.ShowMessage(Ex.Message, true);
        //    }
        //    finally { }
        //}

        private void dteDate_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(dteDate);
        }

        private void glkpProject_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpProject);
        }

        private void txtCompAmount_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtCompAmount);

        }

        private void glkpLedgers_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpLedgers);
        }



        private void gvTransaction_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {

        }

        private void txtCompAmount_TextChanged(object sender, EventArgs e)
        {

        }
        private void glkpGroup_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpGroup);
        }
        #endregion

        private void dteDate_EditValueChanged(object sender, EventArgs e)
        {
            //On 12/07/2018, For closed Projects----
            FetchProjectDetails();
            //---------------------------------------

            LoadCashBankLedgers();
        }

    }
}