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
using Bosco.Utility.ConfigSetting;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Bosco.Model.UIModel;
using Bosco.Model.Transaction;

namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmProcessPayroll : frmPayrollBase
    {
        #region Variables
        CommonMember UtilityMember = new CommonMember();
        ResultArgs resultArgs = new ResultArgs();
        private clsPayrollComponent objPayrollComp = new clsPayrollComponent();
        private clsPayrollProcess objPayrollProcess = new clsPayrollProcess();
        private clsPayrollStaff staff = new clsPayrollStaff();
        private clsPrComponent objPrComponent = new clsPrComponent();
        TransProperty Transaction = new TransProperty();
        UserProperty LoginUser = new UserProperty();
        public static DataTable dtComponents = new DataTable();
        #endregion

        #region Constructor
        public frmProcessPayroll()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        private int payrollcomponentid = 0;
        private int PayrollComponentId
        {
            get
            {
                return 0;  // 0 Indicates Process Payroll for all the groups.
            }
            set
            {
                payrollcomponentid = value;
            }
        }
        private int processledgerid = 0;
        public int ProcessLedgerId
        {
            get
            {
                return processledgerid;
            }
            set
            {
                processledgerid = value;
            }
        }
        private string mappedstaffid = string.Empty;
        public string MappedStaffId
        {
            get
            {
                return GetmappedStaffs();
            }
        }

        private string paygroupid = string.Empty;
        public string PayGroupId
        {
            get
            {
                return GetPayrollGroupsIds();
            }
        }
        public DialogResult drs { get; set; }
        #endregion

        #region Events
        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (ValidateFields())
                {
                    InsertProcessDate();
                    resultArgs = IsComponentLedgersMapped();
                    if (resultArgs.Success)
                    {
                        resultArgs = IsProcessTypesMapped();
                        if (resultArgs.Success)
                        {
                            resultArgs = DeleteProcessPayVouher();
                            if (resultArgs.Success)
                            {
                                if (glkProject.EditValue != null)
                                {

                                    int projectId = UtilityMember.NumberSet.ToInteger(glkProject.EditValue.ToString());
                                    resultArgs = objPayrollProcess.FetchmappedComponentsByProjectId(projectId);
                                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                                    {
                                        if (clsGeneral.PAYROLL_ID == 0)
                                            return;
                                        fraProcess.Visible = true;
                                        fraProcess.Refresh();
                                        if (MappedStaffId != string.Empty && PayGroupId != string.Empty)
                                        {
                                            layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                            using (clsprCompBuild objCompBuild = new clsprCompBuild())
                                            {
                                                AcMELog.WriteLog("Process payroll started");
                                                objCompBuild.ProcessComponent(clsGeneral.PAYROLL_ID, PayGroupId, "", MappedStaffId, false, fraProcess, false, false, true);
                                                AcMELog.WriteLog("Process payroll ended");
                                                AcMELog.WriteLog("Order Components started.");
                                                string[] grpcollection = PayGroupId.Split(',');
                                                foreach (string Grpid in grpcollection)
                                                {
                                                    objPrComponent.SaveAssignedComponent(clsGeneral.PAYROLL_ID, long.Parse(Grpid), dtComponents);
                                                }
                                                AcMELog.WriteLog("Order Components ended.");
                                                AcMELog.WriteLog("Process Journal entry started.");
                                                resultArgs = IsLoanLedgersaremappedwithProject();
                                                if (resultArgs.Success)
                                                {
                                                    resultArgs = ProcessJournalEntry();
                                                    // if (resultArgs.Success)
                                                    //{
                                                    // resultArgs = ProcessJournalEntryforLoan();
                                                    AcMELog.WriteLog("Process Journal entry ended.");
                                                    if (resultArgs.Success)
                                                    {
                                                        AcMELog.WriteLog("Processed payroll successfully..");
                                                        //DialogResult drs = XtraMessageBox.Show("Processed....", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        drs = DialogResult.OK;
                                                        //this.ShowSuccessMessage("Processed....");
                                                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.ProcessPayroll.PROCESS_PAYROLL_PROCESS_INFO));
                                                        if (drs == DialogResult.OK)
                                                        {
                                                            this.Close();
                                                            //PAYROLL.Modules.Payroll_app.frmPayrollBrowseView PayrollView = new PAYROLL.Modules.Payroll_app.frmPayrollBrowseView();
                                                            //PayrollView.MdiParent = this;
                                                            //PayrollView.Show();

                                                            //frmPayrollBrowseView browseview = new frmPayrollBrowseView();
                                                            //browseview.ShowDialog();
                                                            layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                                        }
                                                        else
                                                        {
                                                            //frmProcessPayroll objpayrollprocess = new frmProcessPayroll();
                                                            //objpayrollprocess.ShowDialog();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        drs = DialogResult.Cancel;
                                                        MessageRender.ShowMessage(resultArgs.Message.ToString());
                                                    }
                                                    fraProcess.Refresh();
                                                    fraProcess.Visible = false;
                                                    // }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //XtraMessageBox.Show("Staff are not mapped with the group.Map the staff and try again.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            XtraMessageBox.Show(this.GetMessage(MessageCatalog.Payroll.PayrollBrowseView.BROWSE_MAP_STAFF_MAPWITH_GROUP_INFO), this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                    }
                                    else
                                    {
                                        //XtraMessageBox.Show("Components are not mapped to the groups.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        XtraMessageBox.Show(this.GetMessage(MessageCatalog.Payroll.PayrollBrowseView.BROWSE_MAP_COMPONENT_MAPWITH_GROUP_INFO), this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    //XtraMessageBox.Show("Projects are not available to process payroll.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    XtraMessageBox.Show(this.GetMessage(MessageCatalog.Payroll.ProcessPayroll.PROCESS_PAYROLL_MAP_PROJECT_INFO), this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                //XtraMessageBox.Show(resultArgs.Message, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                XtraMessageBox.Show(resultArgs.Message, this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            // XtraMessageBox.Show("Ledgers are not mapped with either Project or Components.Map the Ledgers and try again.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //XtraMessageBox.Show(resultArgs.Message, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            XtraMessageBox.Show(resultArgs.Message, this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        // XtraMessageBox.Show("Ledgers are not mapped with either Project or Components.Map the Ledgers and try again.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //XtraMessageBox.Show(resultArgs.Message, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        XtraMessageBox.Show(resultArgs.Message, this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmProcessPayroll_Load(object sender, EventArgs e)
        {
            layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            LoadProjects();
            AssignProcessDate();
            LoadComponentDetails();
        }

        #endregion

        #region Methods
        /// <summary>
        /// Load Component details to order components
        /// </summary>
        public void LoadComponentDetails()
        {
            dtComponents = objPayrollComp.getComponentDetails(PayrollComponentId);
        }

        /// <summary>
        /// Update Payroll Process date
        /// </summary>
        private void InsertProcessDate()
        {
            try
            {
                using (clsprCompBuild objCompBuild = new clsprCompBuild())
                {
                    resultArgs = objCompBuild.UpdateProcessDate(deProcessDate.DateTime);
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        /// <summary>
        /// Assign Process date while edit mode
        /// </summary>
        public void AssignProcessDate()
        {
            try
            {
                using (clsprCompBuild objCompBuild = new clsprCompBuild())
                {
                    resultArgs = objCompBuild.GetPayrollPeriodMonth();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        deProcessDate.Properties.MinValue = UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["FROMDATE"].ToString(), false);
                        deProcessDate.Properties.MaxValue = UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["TODATE"].ToString(), false);
                        resultArgs = objCompBuild.AssignProcessDate();
                        if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            DateTime dttemp = new DateTime();
                            dttemp = UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["PROCESS_DATE"].ToString(), false);
                            if (dttemp >= deProcessDate.Properties.MinValue && dttemp <= deProcessDate.Properties.MaxValue)
                            {
                                deProcessDate.DateTime = UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["PROCESS_DATE"].ToString(), false);
                            }
                            else
                            {
                                deProcessDate.DateTime = deProcessDate.Properties.MaxValue;
                            }
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

        /// <summary>
        /// Check whether component ledger mapped with the component ledgers
        /// </summary>
        /// <returns></returns>
        public ResultArgs IsComponentLedgersMapped()
        {
            try
            {
                DataView dvLedTempCheck = new DataView();
                using (clsprCompBuild objCompBuild = new clsprCompBuild())
                {
                    AcMELog.WriteLog("Payroll.Fetching Ledgers mapped with the Components started.");
                    resultArgs = objCompBuild.FetchComponentMappedLedgers();
                    AcMELog.WriteLog("Payroll.Fetching Ledgers  mapped with the Components ended.");
                    if (resultArgs.Success && resultArgs.DataSource.TableView.Table.Rows.Count > 0)
                    {
                        AcMELog.WriteLog("Payroll.Checking all the ledgers are mapped with the Components started.");
                        dvLedTempCheck = resultArgs.DataSource.TableView;
                        dvLedTempCheck.RowFilter = "LEDGER_ID <> 0";
                        if (dvLedTempCheck.ToTable().Rows.Count == resultArgs.DataSource.TableView.Table.Rows.Count)
                        {
                            AcMELog.WriteLog("Payroll.Checking all the ledgers are mapped with the Components ended.");
                            DataTable dtTempVal = dvLedTempCheck.ToTable();
                            string LedgerIdtemp = string.Empty;
                            foreach (DataRow dr in dtTempVal.Rows)
                            {
                                LedgerIdtemp += dr["LEDGER_ID"].ToString() + ',';
                            }
                            LedgerIdtemp = LedgerIdtemp.TrimEnd(',');
                            AcMELog.WriteLog("Payroll.Checking all the ledgers are mapped with the Projects started.");
                            resultArgs = IsProjectLedgersMapped(LedgerIdtemp);
                            AcMELog.WriteLog("Payroll.Checking all the ledgers are mapped with the Projects ended.");
                            if (resultArgs.Success)
                            {
                                AcMELog.WriteLog("Payroll.Ledger mapped with project successfully.");
                            }
                            else
                            {
                                resultArgs.Message = "Problem while mapping Ledger with the Project.";
                            }
                        }
                        else
                        {
                            resultArgs.Message = "Ledgers are not mapped with the Components,try after mapping the ledgers.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return resultArgs;
        }

        private ResultArgs IsLoanLedgersaremappedwithProject()
        {
            string ProjectId = glkProject.EditValue != null ? glkProject.EditValue.ToString() : "0";
            using (clsprCompBuild objCompBuild = new clsprCompBuild())
            {
                int ConfLedger = objCompBuild.IsLoanCompLedgerMappedwithProject(ProjectId);
                if (ConfLedger == 0)
                {
                    using (clsPrComponent objComp = new clsPrComponent())
                    {
                        resultArgs = objComp.MapProjectLedger("1001", ProjectId);
                        resultArgs = objComp.MapProjectLedger("1002", ProjectId);
                        AcMELog.WriteLog("Payroll.Mapping Ledger with the Project ended.");
                    }
                }
            }
            return resultArgs;
        }
        /// <summary>
        /// Check whether Process ledger mapped with the Process type
        /// </summary>
        /// <returns></returns>
        public ResultArgs IsProcessTypesMapped()
        {
            Processtype processtype = new Processtype();
            DataView dvPType = UtilityMember.EnumSet.GetEnumDataSource(processtype, Sorting.None);
            try
            {
                DataView dvLedTempCheck = new DataView();
                using (clsPayrollProcess objProcess = new clsPayrollProcess())
                {
                    resultArgs = objProcess.FetchMappedLedger();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        if (resultArgs.DataSource.Table.Rows.Count == dvPType.ToTable().Rows.Count)
                        {
                            DataTable dtTempVal = resultArgs.DataSource.Table;
                            string LedgerIdtemp = string.Empty;
                            foreach (DataRow dr in dtTempVal.Rows)
                            {
                                LedgerIdtemp += dr["LEDGER_ID"].ToString() + ',';
                            }
                            LedgerIdtemp = LedgerIdtemp.TrimEnd(',');
                            if (!string.IsNullOrEmpty(LedgerIdtemp))
                            {
                                resultArgs = IsProjectLedgersMapped(LedgerIdtemp);
                                if (resultArgs.Success)
                                {
                                    AcMELog.WriteLog("Payroll.Process Ledger mapped with project successfully.");
                                }
                                else
                                {
                                    resultArgs.Message = "Problem while mapping Ledger with the Project.";
                                }
                            }
                        }
                        else
                        {
                            resultArgs.Message = "Process Type Ledgers are not mapped with the Process Type.";
                        }
                    }
                    else
                    {
                        resultArgs.Message = "Process Type Ledgers are not mapped with the Process Type.";
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
        /// Delete Journal entry before process payroll by reference id
        /// </summary>
        /// <returns></returns>
        public ResultArgs DeleteProcessPayVouher()
        {
            try
            {
                clsPayrollProcess mapledger = new clsPayrollProcess();
                string ProjectId = glkProject.EditValue != null ? glkProject.EditValue.ToString() : "0";
                int VoucherrId = mapledger.FetchVoucherMasterPayVoucherbyCId(ProjectId);
                resultArgs = DeleteVoucherTrans(VoucherrId);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return resultArgs;
        }
        public ResultArgs IsComponentMappedForGrups(int projectId)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return resultArgs;
        }
        private ResultArgs DeleteVoucherTrans(int VoucherId)
        {
            try
            {
                using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                {
                    voucherTransaction.VoucherId = VoucherId;
                    voucherTransaction.tdsTransType = TDSTransType.TDSPartyPayment;
                    resultArgs = voucherTransaction.DeleteVoucherTrans();
                }
            }
            catch (Exception ex)
            {
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// Fetch staff ids mapped with the project
        /// </summary>
        /// <returns></returns>
        public string GetmappedStaffs()
        {
            string MapStaff = string.Empty;
            try
            {
                using (clsPrGroupStaff GroupStaff = new clsPrGroupStaff())
                {
                    string Projectid = (glkProject.EditValue != null) ? glkProject.EditValue.ToString() : "0";
                    string GroupId = !string.IsNullOrEmpty(PayGroupId) ? PayGroupId : "0";
                    DataTable dtMappedStaffs = GroupStaff.GetProjectGroupMappedStaffs(Projectid, GroupId);
                    foreach (DataRow dr in dtMappedStaffs.Rows)
                    {
                        MapStaff += dr["STAFFID"].ToString() + ',';
                    }
                    MapStaff = MapStaff.Trim(',');
                }
            }
            catch (Exception ed)
            {
                MessageRender.ShowMessage(ed.ToString(), false);
            }
            finally { }
            return MapStaff;
        }

        /// <summary>
        /// Fetch Process date to process journal entry for the date
        /// </summary>
        /// <returns></returns>
        public DateTime GetProcessDate()
        {
            DateTime dtTemp = new DateTime();
            try
            {
                using (clsprCompBuild objCompBuild = new clsprCompBuild())
                {
                    resultArgs = objCompBuild.AssignProcessDate();
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        dtTemp = UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["PROCESS_DATE"].ToString(), false);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
            return dtTemp;
        }

        /// <summary>
        ///  Check whether all the ledger mapped with project
        /// </summary>
        /// <param name="LedgId"></param>
        /// <returns></returns>
        public ResultArgs IsProjectLedgersMapped(string LedgId)
        {
            resultArgs.Success = true;
            try
            {
                AcMELog.WriteLog("Payroll.Mapped Ledger Ids : " + LedgId);
                string[] LedgIds = LedgId.Split(',');
                foreach (string LedgerrIds in LedgIds)
                {
                    DataView dvLedTempCheck = new DataView();
                    using (clsprCompBuild objCompBuild = new clsprCompBuild())
                    {
                        AcMELog.WriteLog("Payroll.Checking Ledger mapped with the Projects started.");
                        string ProjectId = glkProject.EditValue != null ? glkProject.EditValue.ToString() : "0";
                        int ConfLedger = objCompBuild.IsCompLedgerMappedwithProject(LedgerrIds, ProjectId);
                        AcMELog.WriteLog("Payroll.Checking Ledger mapped with the Projects ended.");
                        if (ConfLedger == 0)
                        {
                            using (clsPrComponent objComp = new clsPrComponent())
                            {
                                AcMELog.WriteLog("Payroll.Mapping LedgerID: " + LedgerrIds + " with the Project started.");
                                resultArgs = objComp.MapProjectLedger(LedgerrIds, ProjectId);
                                AcMELog.WriteLog("Payroll.Mapping Ledger with the Project ended.");
                            }
                        }
                        else
                        {
                            AcMELog.WriteLog("Payroll.Provided Ledger mapped with the already.");
                        }
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
        /// Fetch Group ids mapped with staff and payroll
        /// </summary>
        /// <returns></returns>
        private string GetPayrollGroupsIds()
        {
            string PayGroup = string.Empty;
            try
            {
                resultArgs = staff.FetchGroups("Group");
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    DataTable dtGroups = resultArgs.DataSource.Table;
                    if (dtGroups.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtGroups.Rows)
                        {
                            PayGroup += dr["GROUP ID"].ToString() + ',';
                        }
                    }
                    PayGroup = PayGroup.TrimEnd(',');
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
            return PayGroup;
        }

        /// <summary>
        /// Process payroll by passing journal entry
        /// </summary>
        /// <returns></returns>
        public ResultArgs ProcessJournalEntry()
        {
            try
            {
                using (clsprCompBuild objCompBuild = new clsprCompBuild())
                {

                    using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                    {
                        //Voucher Master Details
                        voucherTransaction.VoucherId = 0;
                        voucherTransaction.ProjectId = UtilityMember.NumberSet.ToInteger(glkProject.EditValue.ToString());
                        voucherTransaction.VoucherDate = GetProcessDate(); // this.UtilityMember.DateSet.ToDate(clsGeneral.PAYROLLDATE, false);
                        voucherTransaction.VoucherType = "JN";
                        voucherTransaction.Status = (int)YesNo.Yes;
                        voucherTransaction.TransVoucherMethod = (int)TransactionVoucherMethod.Automatic;
                        voucherTransaction.CreatedOn = this.UtilityMember.DateSet.ToDate(this.UtilityMember.DateSet.GetDateToday(false), false);
                        voucherTransaction.ModifiedOn = this.UtilityMember.DateSet.ToDate(this.UtilityMember.DateSet.GetDateToday(false), false);
                        voucherTransaction.CreatedBy = this.UtilityMember.NumberSet.ToInteger(this.LoginUser.LoginUserId.ToString());
                        voucherTransaction.ModifiedBy = this.UtilityMember.NumberSet.ToInteger(this.LoginUser.LoginUserId.ToString());
                        //voucherTransaction.Narration = "Payroll Processed for the " + dtTempProcess.Rows[Ptype]["Name"].ToString() + " Process type for the Month of " + clsGeneral.PAYROLL_MONTH;
                        voucherTransaction.Narration = "Payroll Processed for the Month of " + clsGeneral.PAYROLL_MONTH;
                        voucherTransaction.VoucherSubType = ledgerSubType.PAY.ToString();
                        voucherTransaction.ClientReferenceId = this.UtilityMember.NumberSet.ToInteger(clsGeneral.PAYROLL_ID.ToString()).ToString();
                        //Voucher Trans Details
                        DataView dvLedgerDetails = ConstructData();
                        if (dvLedgerDetails != null && dvLedgerDetails.Table.Rows.Count > 0)
                        {
                            Transaction.TransInfo = dvLedgerDetails;
                            resultArgs = voucherTransaction.SaveTransactions();
                        }
                    }

                }
            }
            catch (Exception ed)
            {
                resultArgs.Message = ed.Message;
            }
            return resultArgs;
        }
        private ResultArgs ProcessJournalEntryforLoan()
        {
            try
            {
                using (clsprCompBuild objCompBuild = new clsprCompBuild())
                {

                    using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                    {
                        //Voucher Master Details
                        voucherTransaction.VoucherId = 0;
                        voucherTransaction.ProjectId = UtilityMember.NumberSet.ToInteger(glkProject.EditValue.ToString());
                        voucherTransaction.VoucherDate = GetProcessDate(); // this.UtilityMember.DateSet.ToDate(clsGeneral.PAYROLLDATE, false);
                        voucherTransaction.VoucherType = "JN";
                        voucherTransaction.Status = (int)YesNo.Yes;
                        voucherTransaction.TransVoucherMethod = (int)TransactionVoucherMethod.Automatic;
                        voucherTransaction.CreatedOn = this.UtilityMember.DateSet.ToDate(this.UtilityMember.DateSet.GetDateToday(false), false);
                        voucherTransaction.ModifiedOn = this.UtilityMember.DateSet.ToDate(this.UtilityMember.DateSet.GetDateToday(false), false);
                        voucherTransaction.CreatedBy = this.UtilityMember.NumberSet.ToInteger(this.LoginUser.LoginUserId.ToString());
                        voucherTransaction.ModifiedBy = this.UtilityMember.NumberSet.ToInteger(this.LoginUser.LoginUserId.ToString());
                        //voucherTransaction.Narration = "Payroll Processed for the " + dtTempProcess.Rows[Ptype]["Name"].ToString() + " Process type for the Month of " + clsGeneral.PAYROLL_MONTH;
                        voucherTransaction.Narration = "Payroll Processed for the Month of " + clsGeneral.PAYROLL_MONTH;
                        voucherTransaction.VoucherSubType = ledgerSubType.PAY.ToString();
                        voucherTransaction.ClientReferenceId = this.UtilityMember.NumberSet.ToInteger(clsGeneral.PAYROLL_ID.ToString()).ToString();
                        //Voucher Trans Details
                        DataView dvLedgerDetails = ConstructDataforLoan();
                        if (dvLedgerDetails != null && dvLedgerDetails.Table.Rows.Count > 0)
                        {
                            Transaction.TransInfo = dvLedgerDetails;
                            resultArgs = voucherTransaction.SaveTransactions();
                        }
                    }

                }
            }
            catch (Exception ed)
            {
                resultArgs.Message = ed.Message;
            }
            return resultArgs;
        }

        private DataView ConstructDataforLoan()
        {
            DataView dvtemp = new DataView();
            DataView dvLedgerDetails = new DataView();
            DataTable dtLedgerTemp = new DataTable();
            Processtype processtype = new Processtype();

            DataView dvPType = UtilityMember.EnumSet.GetEnumDataSource(processtype, Sorting.None);
            DataTable dtTempProcess = dvPType.ToTable();
            AcMELog.WriteLog("Constructs empty data structure started..");
            dtLedgerTemp = ConstructEmptySource();
            AcMELog.WriteLog("Constructs empty data structure ended..");
            using (clsprCompBuild objCompBuild = new clsprCompBuild())
            {
                AcMELog.WriteLog("Fetching Processed value by staff id to process journal entry started");
                resultArgs = objCompBuild.FetchLoanComponentValueToProcess(MappedStaffId);
                AcMELog.WriteLog("Fetching Processed value by staff id to process journal entry ended");
                dvtemp = resultArgs.DataSource.TableView;
                if (dtTempProcess != null && dtTempProcess.Rows.Count > 0)
                {
                    for (int Ptype = 0; Ptype < dtTempProcess.Rows.Count; Ptype++)
                    {
                        dvtemp.RowFilter = string.Empty;
                        dvtemp.RowFilter = "PROCESS_TYPE_ID=" + Ptype + "";
                        if (dvtemp.ToTable().Rows.Count > 0)
                        {
                            foreach (DataRow dr in dvtemp.ToTable().Rows)
                            {
                                ProcessLedgerId = UtilityMember.NumberSet.ToInteger(dr["PROCESS_LEDGER_ID"].ToString());
                                dtLedgerTemp.Rows.Add(1001, string.Empty, 0.00, UtilityMember.NumberSet.ToDecimal(dr["ACTUAL_AMOUNT"].ToString()), 0.00, 0.00, string.Empty, 0);
                                dtLedgerTemp.Rows.Add(1002, string.Empty, 0.00, UtilityMember.NumberSet.ToDecimal(dr["INRESTAMOUNT"].ToString()), 0.00, 0.00, string.Empty, 0);
                            }
                            dtLedgerTemp.Rows.Add(ProcessLedgerId, string.Empty, this.UtilityMember.NumberSet.ToDecimal(dvtemp.ToTable().Compute("SUM(AMOUNT)", "PROCESS_TYPE_ID=" + Ptype + "").ToString()), 0.00, 0.00, 0.00, string.Empty, 0);
                        }

                    }
                    dvLedgerDetails = dtLedgerTemp.AsDataView();
                }
            }
            return dvLedgerDetails;
        }
        /// <summary>
        /// Construct empty source
        /// </summary>
        /// <returns></returns>
        private DataTable ConstructEmptySource()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("LEDGER_ID", typeof(string));
            dt.Columns.Add("NARRATION", typeof(string));
            dt.Columns.Add("DEBIT", typeof(decimal));
            dt.Columns.Add("CREDIT", typeof(decimal));
            dt.Columns.Add("TEMP_CREDIT", typeof(decimal));
            dt.Columns.Add("TEMP_DEBIT", typeof(decimal));
            dt.Columns.Add("LEDGER_BALANCE", typeof(string));
            dt.Columns.Add("VALUE", typeof(int));
            return dt;
        }

        /// <summary>
        /// Construct data structure for voucher
        /// </summary>
        /// <returns></returns>
        private DataView ConstructData()
        {
            DataView dvtemp = new DataView();
            DataView dvLedgerDetails = new DataView();
            DataTable dtLedgerTemp = new DataTable();
            Processtype processtype = new Processtype();

            DataView dvPType = UtilityMember.EnumSet.GetEnumDataSource(processtype, Sorting.None);
            DataTable dtTempProcess = dvPType.ToTable();
            AcMELog.WriteLog("Constructs empty data structure started..");
            dtLedgerTemp = ConstructEmptySource();
            AcMELog.WriteLog("Constructs empty data structure ended..");
            using (clsprCompBuild objCompBuild = new clsprCompBuild())
            {
                AcMELog.WriteLog("Fetching Processed value by staff id to process journal entry started");
                //resultArgs = objCompBuild.FetchComponentValueToProcessLedger(MappedStaffId);
                resultArgs = objCompBuild.FetchLoanComponentValueToProcess(MappedStaffId);
                AcMELog.WriteLog("Fetching Processed value by staff id to process journal entry ended");
                dvtemp = resultArgs.DataSource.TableView;
                if (dtTempProcess != null && dtTempProcess.Rows.Count > 0)
                {
                    for (int Ptype = 0; Ptype < dtTempProcess.Rows.Count; Ptype++)
                    {
                        dvtemp.RowFilter = string.Empty;
                        dvtemp.RowFilter = "PROCESS_TYPE_ID=" + Ptype + "";
                        if (Ptype != 3)
                        {
                            if (dvtemp.ToTable().Rows.Count > 0)
                            {
                                foreach (DataRow dr in dvtemp.ToTable().Rows)
                                {
                                    ProcessLedgerId = UtilityMember.NumberSet.ToInteger(dr["PROCESS_LEDGER_ID"].ToString());
                                    dtLedgerTemp.Rows.Add(dr["LEDGER_ID"], string.Empty, UtilityMember.NumberSet.ToDecimal(dr["AMOUNT"].ToString()), 0.00, 0.00, 0.00, string.Empty, 0);
                                }
                                dtLedgerTemp.Rows.Add(ProcessLedgerId, string.Empty, 0.00, this.UtilityMember.NumberSet.ToDecimal(dvtemp.ToTable().Compute("SUM(AMOUNT)", "PROCESS_TYPE_ID=" + Ptype + "").ToString()), 0.00, 0.00, string.Empty, 0);
                            }
                        }
                        else
                        {
                            if (dvtemp.ToTable().Rows.Count > 0)
                            {
                                foreach (DataRow dr in dvtemp.ToTable().Rows)
                                {
                                    ProcessLedgerId = UtilityMember.NumberSet.ToInteger(dr["PROCESS_LEDGER_ID"].ToString());
                                    dtLedgerTemp.Rows.Add(1001, string.Empty, 0.00, UtilityMember.NumberSet.ToDecimal(dr["ACTUAL_AMOUNT"].ToString()), 0.00, 0.00, string.Empty, 0);
                                    dtLedgerTemp.Rows.Add(1002, string.Empty, 0.00, UtilityMember.NumberSet.ToDecimal(dr["INRESTAMOUNT"].ToString()), 0.00, 0.00, string.Empty, 0);
                                }
                                dtLedgerTemp.Rows.Add(ProcessLedgerId, string.Empty, this.UtilityMember.NumberSet.ToDecimal(dvtemp.ToTable().Compute("SUM(AMOUNT)", "PROCESS_TYPE_ID=" + Ptype + "").ToString()), 0.00, 0.00, 0.00, string.Empty, 0);
                            }

                        }
                    }
                    dvLedgerDetails = dtLedgerTemp.AsDataView();
                }
            }
            return dvLedgerDetails;
        }

        /// <summary>
        /// Fetch Projects
        /// </summary>
        private void LoadProjects()
        {
            try
            {
                using (PayrollSystem Paysystem = new PayrollSystem())
                {
                    resultArgs = Paysystem.FetchPayrollProjects();
                    glkProject.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        UtilityMember.ComboSet.BindGridLookUpCombo(glkProject, resultArgs.DataSource.Table, Paysystem.AppSchema.PRPayrollProject.PROJECTColumn.ColumnName, Paysystem.AppSchema.PRPayrollProject.PROJECT_IDColumn.ColumnName);
                        glkProject.EditValue = glkProject.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }
        private bool ValidateFields()
        {
            bool isvalid = true;
            //if (glkProject.EditValue == null || string.IsNullOrEmpty(glkProject.Text) || glkProject.EditValue.ToString() == "0")
            if (glkProject.EditValue == null || string.IsNullOrEmpty(glkProject.Text))
            {
                XtraMessageBox.Show("Project is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isvalid = false;
            }
            return isvalid;
        }
        #endregion

    }
}