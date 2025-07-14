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
using AcMEDSync.Model;
using Payroll.DAO.Schema;


namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmPostPaymentVoucher : frmPayrollBase
    {
        #region Variable Decalration
        ResultArgs resultArgs = null;
        ApplicationSchema.PAYROLL_FINANCEDataTable dtpayrollFinance = new ApplicationSchema.PAYROLL_FINANCEDataTable();
        
        public event EventHandler UpdateHeld;
        DataTable dtPayrollComponentDetails = new DataTable();
        #endregion

        #region Properties

        decimal totalPostingamount = 0;
        public decimal TotalPostingamount
        {
            get
            {
                return totalPostingamount;
            }
            set { totalPostingamount = value; }
        }


        double totalNetAmount = 0;
        public double TotalNetAmount
        {
            get
            {
                return totalNetAmount;
            }
            set { totalNetAmount = value; }
        }

        long payrollid = 0;
        public long PayrollId
        {
            get
            {
                return payrollid;
            }
            set { payrollid = value; }
        }

        private string PayrollGroupIds
        {
            get
            {
                string selectedpaygroupid = string.Empty;
                chkCBPayGroup.RefreshEditValue();
                List<object> selecteditems = chkCBPayGroup.Properties.Items.GetCheckedValues();

                foreach (object item in selecteditems)
                {
                    selectedpaygroupid += item.ToString() + ",";
                }
                selectedpaygroupid = selectedpaygroupid.TrimEnd(',');
                return selectedpaygroupid;
            }
            set
            {
                //object[] selectedPayGroups = value.Split(',');
                chkCBPayGroup.SetEditValue(value);
                //chkCBPayGroup.EditValue = value;
            }
        }

        private int ProjectId
        {
            get
            {
                return (glkpProject.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()));
            }
            set
            {
                glkpProject.EditValue = value;
            }
        }

        Int32 voucherid = 0;
        public Int32 VoucherId
        {
            get
            {
                //Int32 voucherid = this.UtilityMember.NumberSet.ToInteger(gvCompLedgerAmount.GetFocusedRowCellValue(colVoucherId) != null ? gvCompLedgerAmount.GetFocusedRowCellValue(colVoucherId).ToString() : "0");
                return voucherid;
            }
            set { voucherid = value; }
        }

        public string VoucherNo
        {
            get
            {
                string voucherNo = gvCompLedgerAmount.GetFocusedRowCellValue(colVoucherNo) != null ? gvCompLedgerAmount.GetFocusedRowCellValue(colVoucherNo).ToString() : "";
                return voucherNo;
            }
        }

        private int CashBankLedgerId
        {
            get
            {
                return (glkpCashBank.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCashBank.EditValue.ToString()));
            }
            set
            {
                glkpCashBank.EditValue = value;
            }
        }

        private decimal amount { get; set; }
        private double sumAmount { get; set; }
        private DataTable dtConsturct { get; set; }

        private double tmpEditValue { get; set; }

        private int TransLedgerId
        {
            get
            {
                int ledgerId = 0;
                ledgerId = gvCompLedgerAmount.GetFocusedRowCellValue(colLedger) != null ? this.UtilityMember.NumberSet.ToInteger(gvCompLedgerAmount.GetRowCellValue(gvCompLedgerAmount.FocusedRowHandle, colLedger).ToString()) : 0;
                return ledgerId;
            }
        }
        private double TransLedgerAmount
        {
            get
            {
                double ledgerAmount;
                ledgerAmount = gvCompLedgerAmount.GetFocusedRowCellValue(colAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvCompLedgerAmount.GetFocusedRowCellValue(colAmount).ToString()) : 0;
                return ledgerAmount;
            }
        }

        private double CurrentCashBankLedgerBalance = 0;
        private string CashBankLedgerTransMode = string.Empty;
        #endregion

        #region Constructor
        public frmPostPaymentVoucher()
        {
            InitializeComponent();

            dtPayrollComponentDetails.Columns.Add(dtpayrollFinance.GROUP_IDColumn.ColumnName, typeof(Int32));
            dtPayrollComponentDetails.Columns.Add(dtpayrollFinance.GROUPNAMEColumn.ColumnName, typeof(string));
            dtPayrollComponentDetails.Columns.Add(dtpayrollFinance.COMPONENT_IDColumn.ColumnName, typeof(Int32));
            dtPayrollComponentDetails.Columns.Add(dtpayrollFinance.COMPONENTColumn.ColumnName, typeof(string));
            dtPayrollComponentDetails.Columns.Add(dtpayrollFinance.LEDGER_IDColumn.ColumnName, typeof(Double));
            dtPayrollComponentDetails.Columns.Add(dtpayrollFinance.AMOUNTColumn.ColumnName, typeof(Double));
            dtPayrollComponentDetails.Columns.Add(dtpayrollFinance.EARNINGColumn.ColumnName, typeof(Double));
            dtPayrollComponentDetails.Columns.Add(dtpayrollFinance.DEDUCTIONColumn.ColumnName, typeof(Double));
        }

        public frmPostPaymentVoucher(Int32 PostedVoucherId)
            : this()
        {
            VoucherId = PostedVoucherId;
            PayrollId = clsGeneral.PAYROLL_ID;
            if (VoucherId > 0)
            {
                AssignParyrollidByEditDrillfromReport();
            }
            LoadPayrolGroupList();
            LoadProjectDetails();

            //# for posting mode, will take current payrollid, for edit or drilling from repot
            //payrollid will be assigned selected posted voucher's id
            if (VoucherId > 0)
            {
                FillPayrollPostedPaymentDetails();
            }
        }


        #endregion

        #region Methods

        /// <summary>
        /// Load Payroll Group List
        /// </summary>
        private void LoadPayrolGroupList()
        {
            try
            {

                using (clsPayrollGrade Grade = new clsPayrollGrade())
                {

                    ResultArgs result = Grade.getPayrollGroupByPosting(PayrollId);
                    if (result.Success && result.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtGradeList = result.DataSource.Table;
                        using (CommonMethod SelectAll = new CommonMethod())
                        {
                            //Load not poseted payroll groups, for posting || for modify load posted groups 
                            dtGradeList.DefaultView.RowFilter = Grade.AppSchema.PRSTAFFGROUP.IS_PAYROLL_POSTEDColumn.ColumnName + " = " + (VoucherId > 0 ? 1 : 0);
                            DataTable dtPayrollGroupsofProject = dtGradeList.DefaultView.ToTable(false, new string[] { Grade.AppSchema.PRSTAFFGROUP.GROUPIDColumn.ColumnName, Grade.AppSchema.PRSTAFFGROUP.GROUPNAMEColumn.ColumnName });

                            dtPayrollGroupsofProject.Columns[Grade.AppSchema.PRSTAFFGROUP.GROUPIDColumn.ColumnName].ColumnMapping = MappingType.Hidden;
                            //lkpPayrollGroup.Properties.DataSource = dtPayrollGroupsofProject;
                            //lkpPayrollGroup.Properties.ValueMember = Grade.AppSchema.PRSTAFFGROUP.GROUPIDColumn.ColumnName;
                            //lkpPayrollGroup.Properties.DisplayMember = Grade.AppSchema.PRSTAFFGROUP.GROUPNAMEColumn.ColumnName;

                            chkCBPayGroup.Properties.DataSource = dtPayrollGroupsofProject;
                            chkCBPayGroup.Properties.ValueMember = Grade.AppSchema.PRSTAFFGROUP.GROUPIDColumn.ColumnName;
                            chkCBPayGroup.Properties.DisplayMember = Grade.AppSchema.PRSTAFFGROUP.GROUPNAMEColumn.ColumnName;

                            // On 11/01/2017, to remove all option in list of group and select first group item 
                            //lkpGroupData.EditValue = lkpGroupData.EditValue = lkpGroupData.Properties.GetKeyValueByDisplayText("<--All-->");
                            if (dtGradeList.Rows.Count > 0)
                            {
                                //lkpPayrollGroup.EditValue = lkpPayrollGroup.EditValue = lkpPayrollGroup.Properties.GetKeyValueByDisplayText(dtGradeList.Rows[0][Grade.AppSchema.PRSTAFFGROUP.GROUPNAMEColumn.ColumnName].ToString());

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// To fetch projects to process the payroll
        /// </summary>
        /// <returns>Returns the datatable which contains project,projectid as columns</returns>
        private ResultArgs LoadProjectDetails()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    mappingSystem.ProjectClosedDate = dteDate.Text;
                    Int32 Prevprojectid = glkpProject.EditValue != null ? ProjectId : this.UtilityMember.NumberSet.ToInteger(this.AppSetting.UserProjectId);

                    resultArgs = mappingSystem.FetchProjectsLookup();
                    if (resultArgs.DataSource != null)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, 
                                dtpayrollFinance.PROJECTColumn.ColumnName, dtpayrollFinance.PROJECT_IDColumn.ColumnName);
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

        private void LoadLedger()
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    ledgerSystem.ProjectId = ProjectId;
                    string dateTrans = dteDate.Text;
                    if (dateTrans != this.UtilityMember.DateSet.ToDate(DateTime.MinValue.ToShortDateString()))
                    {
                        ledgerSystem.LedgerClosedDateForFilter = dateTrans;
                    }
                    resultArgs = ledgerSystem.FetchLedgerByGroup();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtLedgers = resultArgs.DataSource.Table;
                        //For SDBINM Auditors suggested to skip below mentioned Ledgers for Voucher Entries
                        if (AppSetting.IS_SDB_INM)
                        {
                            dtLedgers = this.ForSDBINMSkipLedgers(this.dteDate.Text, dtLedgers);
                        }

                        rglkpLedger.DisplayMember = dtpayrollFinance.LEDGER_NAMEColumn.ColumnName;
                        rglkpLedger.ValueMember = dtpayrollFinance.LEDGER_IDColumn.ColumnName;

                        rglkpLedger.DataSource = dtLedgers;
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
        private void LoadCashBankLedger()
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    ledgerSystem.ProjectId = ProjectId;
                    string dateTrans = dteDate.Text;

                    if (dateTrans != this.UtilityMember.DateSet.ToDate(DateTime.MinValue.ToShortDateString()))
                    {
                        ledgerSystem.LedgerClosedDateForFilter = dateTrans;
                    }
                    ResultArgs resultArgsCashBank = ledgerSystem.FetchCashBankLedger();
                    if (resultArgsCashBank.Success && resultArgsCashBank.DataSource.Table != null)
                    {
                        Int32 PrevCashBankLedgerId = glkpCashBank.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpCashBank.EditValue.ToString()) : 0;

                        DataTable dtCashBank = resultArgsCashBank.DataSource.Table;
                        if (dateTrans != this.UtilityMember.DateSet.ToDate(DateTime.MinValue.ToShortDateString()) && !string.IsNullOrEmpty(dateTrans))
                        {
                            dtCashBank.DefaultView.RowFilter = "((DATE_CLOSED >='" + dateTrans + "' OR DATE_CLOSED IS NULL) AND " +
                                                                "(DATE_OPENED <='" + dateTrans + "' OR DATE_OPENED IS NULL))";
                            
                            //On 21/10/2024, If multi currency enabled, to fix currency local currency ----------------------------------------------------
                            if (this.AppSetting.AllowMultiCurrency == 1)
                            {
                                dtCashBank.DefaultView.RowFilter += " AND " + ledgerSystem.AppSchema.Ledger.CUR_COUNTRY_IDColumn.ColumnName + 
                                            " = " + (string.IsNullOrEmpty(AppSetting.Country) ? "0" : AppSetting.Country);
                            }
                            //-----------------------------------------------------------------------------------------------------------------------------
                            dtCashBank = dtCashBank.DefaultView.ToTable();
                        }
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpCashBank, dtCashBank, ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                        if (glkpCashBank.Properties.GetDisplayValueByKeyValue(PrevCashBankLedgerId) != null)
                        {
                            glkpCashBank.EditValue = PrevCashBankLedgerId;
                        }
                        else
                        {
                            glkpCashBank.EditValue = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        /// <summary>
        /// To load payroll pending amount
        /// </summary>
        private void LoadPayrollPostPending(bool forceapply=false)
        {
            try
            {
                TotalNetAmount = 0;
                TotalPostingamount = 0;
                using (clsPrGateWay payrollgateway = new clsPrGateWay())
                {
                    //if ((!string.IsNullOrEmpty(PayrollGroupIds)) && ProjectId > 0)
                    if ((!string.IsNullOrEmpty(PayrollGroupIds)))
                    {
                        //# get pending payroll voucher detials for selected paygroup
                        ResultArgs result = payrollgateway.FetchPayrollPostPending(PayrollId, PayrollGroupIds, ProjectId);
                        if (result.Success && result.DataSource.Table!=null)
                        {
                            DataTable dtPostPayrollPending = result.DataSource.Table;

                            //On 08/04/2021, for Modify already posted voucher. If NET amount is posted
                            // show only NET amount Component alone
                            if (VoucherId > 0 && !forceapply)
                            {
                                //GET Posted NET AMOUNT, If amount is greater than zero, it means, Posted by Salary NET amount otherwise Gross wages with all Payable componetns
                                double alreadyPostedNET =  UtilityMember.NumberSet.ToDouble(dtPostPayrollPending.Compute("SUM(" + dtpayrollFinance.AMOUNTColumn.ColumnName + ")",
                                        dtpayrollFinance.PROCESS_COMPONENT_TYPEColumn.ColumnName + " = 1 AND " + dtpayrollFinance.VOUCHER_IDColumn.ColumnName + "> 0").ToString() );

                                if (alreadyPostedNET > 0)
                                {
                                    cbDrPostComponent.SelectedIndex = 1;   
                                }
                            }

                            //Calculate Net Amount 
                            TotalNetAmount = UtilityMember.NumberSet.ToDouble(dtPostPayrollPending.Compute("SUM(" + dtpayrollFinance.AMOUNTColumn.ColumnName + ")", 
                                                dtpayrollFinance.PROCESS_COMPONENT_TYPEColumn.ColumnName +  " = 1").ToString());

                            //dtPostPayrollPending.DefaultView.RowFilter = "BALANCE > 0 AND TOTAL_AMOUNT>0";    
                            if (cbDrPostComponent.SelectedIndex==1)
                            {  //For Posting only Salary NET amount
                                dtPostPayrollPending.DefaultView.RowFilter = dtpayrollFinance.AMOUNTColumn.ColumnName + " > 0 AND " +
                                            dtpayrollFinance.PROCESS_COMPONENT_TYPEColumn.ColumnName + " = 1";
                            }
                            else
                            {  //For Posting Gross with Pay Payable components
                                dtPostPayrollPending.DefaultView.RowFilter = dtpayrollFinance.AMOUNTColumn.ColumnName + " > 0 AND " +
                                            dtpayrollFinance.PROCESS_COMPONENT_TYPEColumn.ColumnName + "<> 1";
                            }

                            dtPostPayrollPending.DefaultView.Sort = "TYPE, GROUPNAME, COMPONENT";
                            dtPostPayrollPending = dtPostPayrollPending.DefaultView.ToTable();

                            //1. Prepare Vouchers Grid to set Finance Ledger for Corresponding Component
                            DataTable dtPendingPosting = dtPostPayrollPending.Clone();
                            dtPendingPosting.Columns.Remove(dtpayrollFinance.GROUPIDColumn.ColumnName);
                            dtPendingPosting.Columns.Remove(dtpayrollFinance.GROUPNAMEColumn.ColumnName);
                            var groups = from drpendingrow in dtPostPayrollPending.AsEnumerable()
                                         group drpendingrow by drpendingrow["COMPONENTID"] into g
                                        //where g.Count() > 1
                                         select dtPendingPosting.LoadDataRow(new object[]
                                         {
                                             g.First()[dtpayrollFinance.PAYROLLIDColumn.ColumnName],
                                             g.Key,
                                             g.First()[dtpayrollFinance.SOURCEColumn.ColumnName],
                                             g.First()[dtpayrollFinance.TYPEColumn.ColumnName],
                                             g.First()[dtpayrollFinance.TRANS_MODEColumn.ColumnName],
                                             g.First()[dtpayrollFinance.COMPONENTColumn.ColumnName],
                                             g.First()[dtpayrollFinance.LEDGER_IDColumn.ColumnName],                                             
                                             g.Sum(x => x.Field<decimal?>(dtpayrollFinance.TOTAL_AMOUNTColumn.ColumnName)),
                                             g.Sum(x => x.Field<decimal?>(dtpayrollFinance.AMOUNTColumn.ColumnName)),
                                             g.Sum(x => x.Field<decimal?>(dtpayrollFinance.BALANCEColumn.ColumnName)),
                                             g.First()[dtpayrollFinance.VOUCHER_IDColumn.ColumnName],
                                             g.First()[dtpayrollFinance.VOUCHER_NOColumn.ColumnName]
                                        }, true);

                            if (groups != null && groups.Count() > 0)
                            {
                                dtPendingPosting = groups.CopyToDataTable();
                            }
                            gcCompLedgerAmount.DataSource = null;
                            gcCompLedgerAmount.DataSource = dtPendingPosting;

                            //2. Prepare and show Payroll Details with Pay group and Component values
                            AssignPayrollDetails(dtPostPayrollPending);
                            gcPayrollCompDetails.DataSource = null;
                            gcPayrollCompDetails.DataSource = dtPayrollComponentDetails;
                            
                            dtPostPayrollPending.DefaultView.RowFilter = string.Empty;
                            dtPostPayrollPending.DefaultView.Sort = string.Empty;
                        }
                        else
                        {
                            MessageRender.ShowMessage(result);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally {
                lblTotalNetAmount.Text = this.UtilityMember.NumberSet.ToCurrency(TotalNetAmount) + " "+ TransactionMode.DR.ToString().ToUpper();
                
            }
        }

        private void AssignPayrollDetails(DataTable dtPayrollpost)
        {
            if (dtPayrollpost != null)
            {
                dtPayrollComponentDetails.Clear();
                dtPayrollpost.DefaultView.Sort = dtpayrollFinance.TYPEColumn.ColumnName + "," 
                            + dtpayrollFinance.GROUPNAMEColumn.ColumnName + "," + dtpayrollFinance.COMPONENTColumn.ColumnName;

                foreach (DataRowView drv in dtPayrollpost.DefaultView)
                {
                    DataRow dr = dtPayrollComponentDetails.NewRow();
                    Int32 type = UtilityMember.NumberSet.ToInteger(drv[dtpayrollFinance.TYPEColumn.ColumnName].ToString());
                    double amt = UtilityMember.NumberSet.ToDouble(drv[dtpayrollFinance.AMOUNTColumn.ColumnName].ToString());

                    dr[dtpayrollFinance.GROUP_IDColumn.ColumnName] = drv[dtpayrollFinance.GROUPIDColumn.ColumnName].ToString();
                    dr[dtpayrollFinance.GROUPNAMEColumn.ColumnName] = drv[dtpayrollFinance.GROUPNAMEColumn.ColumnName].ToString();
                    dr[dtpayrollFinance.COMPONENT_IDColumn.ColumnName] = drv[dtpayrollFinance.COMPONENTIDColumn.ColumnName].ToString();
                    dr[dtpayrollFinance.COMPONENTColumn.ColumnName] = drv[dtpayrollFinance.COMPONENTColumn.ColumnName].ToString();
                    dr[dtpayrollFinance.LEDGER_IDColumn.ColumnName] = drv[dtpayrollFinance.LEDGER_IDColumn.ColumnName].ToString();
                    
                    if (type == 0) //Earnings
                    {
                        dr[dtpayrollFinance.EARNINGColumn.ColumnName] = amt;
                    }
                    else //Deduction
                    {
                        dr[dtpayrollFinance.DEDUCTIONColumn.ColumnName] = amt;
                    }
                    dtPayrollComponentDetails.Rows.Add(dr);
                }
            }
        }

        /// <summary>
        /// This method is used to load already poseted details
        /// </summary>
        private void FillPayrollPostedPaymentDetails()
        {
            //# check already payroll payment voucher posted to finance for selected group
            using (clsPrGateWay payrollgateway = new clsPrGateWay())
            {
                ResultArgs result = payrollgateway.FetchPayrollPostPaymentByVoucherId(VoucherId);
                if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                {
                    DataTable dtAlreadyPosted = result.DataSource.Table;
                    PayrollId = UtilityMember.NumberSet.ToInteger(dtAlreadyPosted.Rows[0][dtpayrollFinance.PAYROLL_IDColumn.ColumnName].ToString());
                    PayrollGroupIds = dtAlreadyPosted.Rows[0][dtpayrollFinance.SALARY_GROUP_IDColumn.ColumnName].ToString();
                    ProjectId = UtilityMember.NumberSet.ToInteger(dtAlreadyPosted.Rows[0][dtpayrollFinance.PROJECT_IDColumn.ColumnName].ToString());
                    CashBankLedgerId = UtilityMember.NumberSet.ToInteger(dtAlreadyPosted.Rows[0][dtpayrollFinance.LEDGER_IDColumn.ColumnName].ToString());
                    txtChequeRefNumber.Text = dtAlreadyPosted.Rows[0][dtpayrollFinance.CHEQUE_NOColumn.ColumnName].ToString().Trim();
                    txtNarration.Text = dtAlreadyPosted.Rows[0][dtpayrollFinance.NARRATIONColumn.ColumnName].ToString().Trim();
                    dteDate.DateTime = UtilityMember.DateSet.ToDate(dtAlreadyPosted.Rows[0][dtpayrollFinance.VOUCHER_DATEColumn.ColumnName].ToString(), false);
                    //lkpPayrollGroup.Enabled = false;
                    chkCBPayGroup.Enabled = false;

                }
            }
        }

        /// <summary>
        /// for drill from report
        /// </summary>
        private void AssignParyrollidByEditDrillfromReport()
        {
            //# check already payroll payment voucher posted to finance for selected group
            using (clsPrGateWay payrollgateway = new clsPrGateWay())
            {
                ResultArgs result = payrollgateway.FetchPayrollPostPaymentByVoucherId(VoucherId);
                if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                {
                    DataTable dtAlreadyPosted = result.DataSource.Table;
                    PayrollId = UtilityMember.NumberSet.ToInteger(dtAlreadyPosted.Rows[0][dtpayrollFinance.PAYROLL_IDColumn.ColumnName].ToString());
                    PayrollGroupIds = dtAlreadyPosted.Rows[0][dtpayrollFinance.SALARY_GROUP_IDColumn.ColumnName].ToString();
                }
            }
        }

        /// <summary>
        /// To Validate the Post payment detials 
        /// </summary>
        /// <returns>Returns false when there is any empty or correct field value </returns>
        private bool ValidatePostPaymentDetails()
        {
            bool isValid = true;
            if (!IsValidSource())
            {
                this.ShowMessageBox("Select Finance Ledger and Amount");
                FocusTransactionGrid();
                isValid = false;
            }
            else if (chkCBPayGroup.Properties.Items.GetCheckedValues().Count == 0)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PostPayment.POST_GROUP_EMPTY));
                this.SetBorderColor(chkCBPayGroup);
                isValid = false;
                chkCBPayGroup.Focus();
            }
            else if (string.IsNullOrEmpty(glkpProject.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PostPayment.POST_PROJECT_EMPTY));
                this.SetBorderColor(glkpProject);
                isValid = false;
                glkpProject.Focus();
            }
            else if (string.IsNullOrEmpty(dteDate.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PostPayment.POST_DATE_EMPTY));
                this.SetBorderColor(dteDate);
                isValid = false;
                dteDate.Focus();
            }
            else if (string.IsNullOrEmpty(glkpCashBank.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PostPayment.POST_CASHBANK_EMPTY));
                this.SetBorderColor(glkpCashBank);
                isValid = false;
                glkpCashBank.Focus();
            }
            else if (CashBankLedgerTransMode == TransactionMode.CR.ToString())
            {
                DialogResult dialogResult = XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.FDLedger.FD_BANK_CASH_GOES_CREDIT), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult != DialogResult.Yes)
                {
                    this.SetBorderColor(glkpCashBank);
                    isValid = false;
                    glkpCashBank.Focus();
                }
            }

            //Check NET Amount and Posting Amount
            if (isValid && TotalPostingamount != UtilityMember.NumberSet.ToDecimal(TotalNetAmount.ToString()))
            {
                isValid = false;
                this.ShowMessageBox("Posting Amount is mismatching with Payroll Salary NET Amount. Check all Deduction Component(s) are set as Payable.");
                FocusTransactionGrid();
            }

            return isValid;
        }

        /// <summary>
        /// To save the post payment details and payment voucher details by assigning the input values to properties in clsprGateway
        /// </summary>
        private bool SavePostDetails()
        {
            bool Rtn = false;
            try
            {
                this.ShowwaitDialog("Posting Payroll Voucher");
                DataTable dtPayrollVoucherLedgers = gcCompLedgerAmount.DataSource as DataTable;
                if (dtPayrollVoucherLedgers != null && dtPayrollVoucherLedgers.Rows.Count > 0 && IsValidSource())
                {
                    //Assign Finance Ledger in Payroll group and component details
                    foreach (DataRow dr in dtPayrollVoucherLedgers.Rows)
                    {
                        Int32 componentid = UtilityMember.NumberSet.ToInteger(dr[dtpayrollFinance.COMPONENTIDColumn.ColumnName].ToString());
                        Int32 ledgerid = UtilityMember.NumberSet.ToInteger(dr[dtpayrollFinance.LEDGER_IDColumn.ColumnName].ToString());

                        dtPayrollComponentDetails.DefaultView.RowFilter = dtpayrollFinance.COMPONENT_IDColumn.ColumnName + "=" + componentid;
                        foreach (DataRowView drv in dtPayrollComponentDetails.DefaultView)
                        {
                            drv.BeginEdit();
                            drv[dtpayrollFinance.LEDGER_IDColumn.ColumnName] = ledgerid;
                            drv.EndEdit();
                            dtPayrollComponentDetails.AcceptChanges();
                        }
                    }
                    dtPayrollComponentDetails.DefaultView.RowFilter = string.Empty;

                    using (clsPrGateWay objprgateway = new clsPrGateWay())
                    {
                        objprgateway.postDate = UtilityMember.DateSet.ToDate(dteDate.EditValue.ToString(), false);
                        objprgateway.PostedPayrollId = PayrollId;
                        objprgateway.ProjectId = ProjectId;
                        objprgateway.PayrollGroupIds = PayrollGroupIds;
                        objprgateway.VoucherId = VoucherId;
                        objprgateway.VoucherNo = VoucherNo;
                        objprgateway.dtPayrollVoucherDetails = dtPayrollVoucherLedgers;
                        objprgateway.dtPayrollComponentDetails = dtPayrollComponentDetails;
                        objprgateway.CashBankLedgerId = this.UtilityMember.NumberSet.ToInteger(glkpCashBank.EditValue.ToString());
                        objprgateway.PostAmount = GetSummaryAmount("AMOUNT");
                        objprgateway.Narration = txtNarration.Text;
                        objprgateway.RefChequeDDNumber = txtChequeRefNumber.Text;
                        resultArgs = objprgateway.PostPaymentVoucher();
                        if (resultArgs.Success)
                        {
                            LoadPayrollPostPending();
                            ClearControls();
                            Rtn = true;
                            MessageRender.ShowMessage("Sucessfully Payroll Payment Voucher is Posted.");
                        }
                        else
                        {
                            MessageRender.ShowMessage("Could not Post Payroll Payment to Finance, " + resultArgs.Message);
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox("Select Ledger and Amount");
                    FocusTransactionGrid();
                }
                this.CloseWaitDialog();
            }
            catch (Exception Ex)
            {
                this.CloseWaitDialog();
                MessageRender.ShowMessage(Ex.Message, true);
            }
            finally { this.CloseWaitDialog(); }

            return Rtn;
        }

        /// <summary>
        /// To clear the vlaues after the post the voucher
        /// </summary>
        private void ClearControls()
        {
            //dteDate.EditValue = this.UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false);
            glkpCashBank.EditValue = 0;
            txtNarration.Text = txtChequeRefNumber.Text = string.Empty;
        }

        /// <summary>
        /// To load the default date value 
        /// </summary>
        private void SetDate()
        {
            this.Text = "Post Payroll Payment Voucher to Finance";
            lcGrpPayrollComponentDetails.Text = this.GetMessage(MessageCatalog.Payroll.PostPayment.POST_PAYROLL_MONTH_FOR_INFO) + "  " + clsGeneral.PAYROLL_MONTH;

            if (VoucherId == 0)
            {
                string payrolldate = clsGeneral.PAYROLLDATE;
                if (!String.IsNullOrEmpty(payrolldate))
                {
                    DateTime dtPayrolldate = this.UtilityMember.DateSet.ToDate(payrolldate, false);
                    dteDate.EditValue = dtPayrolldate.AddMonths(1).AddDays(-1);
                }
            }

            //On 20/05/2024, To Post any date
            //dteDate.Properties.MinValue = this.UtilityMember.DateSet.ToDate(AppSetting.BookBeginFrom, false);
            //dteDate.Properties.MaxValue = this.UtilityMember.DateSet.ToDate(AppSetting.YearTo, false);
        }

        private void FocusTransactionGrid()
        {
            gcCompLedgerAmount.Focus();
            gvCompLedgerAmount.MoveFirst(); //DevExpress.XtraGrid.GridControl.NewItemRowHandle;
            gvCompLedgerAmount.FocusedColumn = gvCompLedgerAmount.Columns.ColumnByName(colLedger.Name);
            gvCompLedgerAmount.ShowEditor();
        }

        private bool IsValidSource()
        {
            bool isValid = false;
            DateTime dtClosedDate = DateTime.MinValue;
            string validateMessage = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.REQUIRED_INFORMATION_NOT_FILLED);

            DataTable dtPayrollVoucherLedgers = gcCompLedgerAmount.DataSource as DataTable;

            if (dtPayrollVoucherLedgers != null && dtPayrollVoucherLedgers.Rows.Count > 0)
            {
                DataView dv = new DataView(dtPayrollVoucherLedgers);
                dv.RowFilter = "(LEDGER_ID = 0 OR AMOUNT = 0)";
                isValid = (dv.Count == 0);

                //Check First Ledger DR must be available
                if (isValid && dtPayrollVoucherLedgers.Rows[0][dtpayrollFinance.TRANS_MODEColumn.ColumnName].ToString().ToUpper() == TransSource.Cr.ToString().ToUpper())
                {
                    this.ShowMessageBox("For Posting Payroll to Finance (Dr Ledger is missing), Dr Ledger is compulsory");
                    isValid = false;
                }

                if (isValid)
                {
                    foreach (DataRowView drTrans in dtPayrollVoucherLedgers.DefaultView)
                    {
                        Int32 Id = this.UtilityMember.NumberSet.ToInteger(drTrans["LEDGER_ID"].ToString());
                        double amount = this.UtilityMember.NumberSet.ToDouble(drTrans["AMOUNT"].ToString());
                        
                        //Check Ledger Closed Date
                        using (LedgerSystem ledgersystem = new LedgerSystem())
                        {
                            dtClosedDate = ledgersystem.GetLedgerClosedDate(Id);
                        }
                        
                        if ((Id == 0 || amount == 0) || (!(dtClosedDate >= dteDate.DateTime)) && (!(dtClosedDate == DateTime.MinValue)))
                        {
                            if (Id == 0)
                            {
                                validateMessage = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.REQUIRED_INFORMATION_LEDGER_EMPTY);
                                gvCompLedgerAmount.FocusedColumn = colLedger;
                            }
                            if (amount==0)
                            {
                                validateMessage = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.REQUIRED_INFORMATION_AMOUNT_EMPTY);
                                gvCompLedgerAmount.FocusedColumn = colAmount;
                            }
                            isValid = false;
                            break;
                        }
                    }
                }

                if (!isValid)
                {
                    FocusTransactionGrid();
                }
            }
            return isValid;
        }

        private decimal GetSummaryAmount(string fldname)
        {
            decimal dAmount = 0;
            DataTable dtTrans = gcCompLedgerAmount.DataSource as DataTable;
            if (dtTrans != null && dtTrans.Columns.Contains(fldname) && dtTrans.Rows.Count > 0)
            {
                decimal CrAmt = 0;
                decimal DrAmt = 0;

                DrAmt = this.UtilityMember.NumberSet.ToDecimal(dtTrans.Compute("SUM(" + fldname + ")", "TRANS_MODE='" + TransactionMode.DR.ToString() + "'").ToString());
                CrAmt = this.UtilityMember.NumberSet.ToDecimal(dtTrans.Compute("SUM(" + fldname + ")", "TRANS_MODE= '" + TransactionMode.CR.ToString() + "'").ToString());

                dAmount = DrAmt - CrAmt;
            }
            return dAmount;
        }

        /// <summary>
        /// This is to load the current balance of Ledger selected.
        /// </summary>
        /// <params>Ledger Id</params>
        private void FetchCashBankLedgerAmt()
        {
            using (BalanceSystem balancesystem = new BalanceSystem())
            {
                BalanceProperty balProperty = balancesystem.GetBalance(ProjectId, glkpCashBank.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpCashBank.EditValue.ToString()) : 0, "", BalanceSystem.BalanceType.CurrentBalance);
                CurrentCashBankLedgerBalance = this.UtilityMember.NumberSet.ToDouble(balProperty.Amount.ToString());
                lcLblCashBankBalance.Text = this.UtilityMember.NumberSet.ToCurrency(balProperty.Amount) + " " + balProperty.TransMode;
                CashBankLedgerTransMode = balProperty.TransMode;
            }
        }

        #endregion

        #region Events
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ShowConfirmationMessage("Are you sure to Post Payroll Payment Voucher to Finance ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                if (ValidatePostPaymentDetails())
                {
                    if (SavePostDetails())
                    {
                        if (UpdateHeld != null)
                        {
                            UpdateHeld(this, e);
                        }
                        this.Close();
                    }
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
                cbDrPostComponent.SelectedIndex = 0;
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message, true);
            }
            finally { }
        }

        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //ProjectId = (glkpProject.EditValue==null? 0 : this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()));
                LoadLedger();
                LoadCashBankLedger();
                LoadPayrollPostPending();
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message, true);
            }
            finally { }
        }


        private void dteDate_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(dteDate);
        }

        private void glkpProject_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpProject);
        }

        private void dteDate_EditValueChanged(object sender, EventArgs e)
        {
            //On 12/07/2018, For closed Projects and ledgers----
            LoadProjectDetails();
            LoadCashBankLedger();
            LoadLedger();
            //---------------------------------------
        }

        private void gcCompLedgerAmount_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control &&
               (gvCompLedgerAmount.FocusedColumn == colAmount || gvCompLedgerAmount.FocusedColumn == colLedger))
            {
                gvCompLedgerAmount.PostEditor();
                gvCompLedgerAmount.UpdateCurrentRow();
                
                if (gvCompLedgerAmount.IsLastRow)
                {
                    if ((TransLedgerId > 0 && TransLedgerAmount > 0) || (TransLedgerId == 0 || TransLedgerAmount == 0))
                    {
                        gvCompLedgerAmount.CloseEditor();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        glkpCashBank.Focus();
                    }
                }
                else if (TransLedgerId > 0 && TransLedgerAmount > 0)
                {
                    gvCompLedgerAmount.MoveNext();
                    gvCompLedgerAmount.FocusedColumn = gvCompLedgerAmount.Columns.ColumnByName(colLedger.Name);
                    gvCompLedgerAmount.ShowEditor();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
                else
                {
                    FocusTransactionGrid();
                }
            }
        }

        private void gvCompLedgerAmount_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            string summaryFldName = ((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName;
            totalPostingamount = GetSummaryAmount(summaryFldName);
            e.TotalValue = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(totalPostingamount.ToString()));
        }

        private void glkpCashBank_EditValueChanged(object sender, EventArgs e)
        {
            GridLookUpEdit grplkp = sender as GridLookUpEdit;
            if (grplkp != null && grplkp.GetSelectedDataRow() !=null)
            {
                DataRowView datarow = (DataRowView)grplkp.GetSelectedDataRow();//grplkp.Properties.View.GetDataRow(grplkp.Properties.View.FocusedRowHandle);
                if (datarow != null)
                {
                    Int32 grpid = UtilityMember.NumberSet.ToInteger(datarow[dtpayrollFinance.GROUP_IDColumn.ColumnName].ToString());
                    txtChequeRefNumber.Enabled = (grpid == (int)FixedLedgerGroup.BankAccounts);
                    if (!txtChequeRefNumber.Enabled)
                    {
                        txtChequeRefNumber.Text = string.Empty;
                    }
                }

                FetchCashBankLedgerAmt();
            }
        }

        private void chkCBPayGroup_EditValueChanged(object sender, EventArgs e)
        {
            LoadPayrollPostPending();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            LoadPayrollPostPending(true);
        }

               
        #endregion

    }
}

