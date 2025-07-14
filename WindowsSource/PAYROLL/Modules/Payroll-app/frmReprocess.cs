using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using Payroll.Model.UIModel;
using Bosco.Utility.Common;
using Bosco.Utility;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Bosco.Model.UIModel;
using Bosco.Utility.ConfigSetting;
using Bosco.Model.Transaction;

namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmReprocess : frmPayrollBase
    {
        #region VariableDeclartion
        private long PayRollId = 0;
        CommonMember commem = new CommonMember();
        ResultArgs resultArgs = null;
        TransProperty Transaction = new TransProperty();
        UserProperty LoginUser = new UserProperty();
        #endregion

        #region Constructor
        public frmReprocess()
        {
            InitializeComponent();
        }
        public frmReprocess(long PRollId)
            : this()
        {
            PayRollId = PRollId;
        }
        #endregion

        #region Properties
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
        //private int deductionledgerid = 0;
        //public int DeductionLedgerId
        //{
        //    get
        //    {
        //        return deductionledgerid;
        //    }
        //    set
        //    {
        //        deductionledgerid = value;
        //    }
        //}
        //private DateTime processdate = DateTime.Now;
        //public DateTime ProcessDate
        //{
        //    get
        //    {
        //        return processdate;
        //    }
        //    set
        //    {
        //        processdate = value;
        //    }
        //}
        #endregion

        #region Events

        private void frmReprocess_Load(object sender, EventArgs e)
        {
            GetSource();
            LoadProjects();
            AssignProcessDate();
        }

        private void gvProcess_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (!chkShowfilter.Checked)
            {
                if (gvProcess.GetRowCellValue(e.RowHandle, colIdentification).ToString() == "G")
                {

                    // e.Appearance.ForeColor = System.Drawing.Color.Green;
                    e.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
                    e.Appearance.BackColor = Color.Gainsboro;
                    // e.Appearance.BackColor = System.Drawing.Color.MidnightBlue;

                }
            }
        }

        private void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit chkSelect = (CheckEdit)sender;
            if (chkSelect.Checked)
            {
                gvProcess.SetFocusedRowCellValue(colCheck, 1);
                if (gvProcess.GetFocusedRowCellValue(colIdentification) != null)
                {
                    if (gvProcess.GetFocusedRowCellValue(colIdentification).ToString() == "G")
                    {
                        SelectGroup(chkSelect.Checked);
                    }
                }
            }
            else
            {
                gvProcess.SetFocusedRowCellValue(colCheck, 0);
                if (gvProcess.GetFocusedRowCellValue(colIdentification) != null)
                {
                    if (gvProcess.GetFocusedRowCellValue(colIdentification).ToString() == "G")
                    {
                        SelectGroup(chkSelect.Checked);
                    }
                }
            }
            DataTable dt = gcProcess.DataSource as DataTable;

        }

        #endregion

        #region Methods
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

        public void AssignProcessDate()
        {
            try
            {
                using (clsprCompBuild objCompBuild = new clsprCompBuild())
                {
                    resultArgs = objCompBuild.GetPayrollPeriodMonth();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        deProcessDate.Properties.MinValue = commem.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["FROMDATE"].ToString(), false);
                        deProcessDate.Properties.MaxValue = commem.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["TODATE"].ToString(), false);
                        resultArgs = objCompBuild.AssignProcessDate();
                        if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            DateTime dttemp = new DateTime();
                            dttemp = commem.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["PROCESS_DATE"].ToString(), false);
                            if (dttemp >= deProcessDate.Properties.MinValue && dttemp <= deProcessDate.Properties.MaxValue)
                            {
                                deProcessDate.DateTime = commem.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["PROCESS_DATE"].ToString(), false);
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


        private void LoadProjects()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    resultArgs = mappingSystem.FetchProjectsLookup();
                    glkpProject.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        commem.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                        glkpProject.EditValue = glkpProject.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }
        private void GetSource()
        {
            using (clsPrComponent component = new clsPrComponent())
            {
                DataTable dtData = component.GetData(clsGeneral.PAYROLL_ID);
                if (dtData != null && dtData.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtData.Rows)
                    {
                        dr["Check"] = false;
                    }
                    gcProcess.DataSource = dtData;
                    gcProcess.RefreshDataSource();
                }
            }
        }
        private void SelectGroup(bool Checked)
        {
            try
            {
                DataTable dt = (DataTable)gcProcess.DataSource;
                int iRow = gvProcess.FocusedRowHandle;
                if (Checked)
                {
                    if (dt.Rows[iRow]["Identification"].ToString() == "G") //when group is selected , select all its component
                    {
                        dt.Rows[iRow]["Check"] = 1;
                        while (dt.Rows[++iRow]["Identification"].ToString() == "C")
                        {
                            dt.Rows[iRow]["Check"] = 1;
                        }
                    }
                }
                else
                {
                    if (dt.Rows[iRow]["Identification"].ToString() == "G") //when group is selected , select all its component
                    {
                        dt.Rows[iRow]["Check"] = false;
                        while (dt.Rows[++iRow]["Identification"].ToString() == "C")
                        {
                            dt.Rows[iRow]["Check"] = false;
                        }
                    }
                }

            }
            catch
            {
            }
        }
        protected virtual void SetFocusRowFilter(DevExpress.XtraGrid.Views.Grid.GridView gridview, DevExpress.XtraGrid.Columns.GridColumn colGridColumn)
        {
            gridview.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
            gridview.FocusedColumn = colGridColumn;
            gridview.OptionsFind.AllowFindPanel = false;
            gridview.ShowEditor();
        }

        #endregion

        private void btnProcess_Click(object sender, EventArgs e)
        {
            long nGrpId = 0;
            string sGroup = "";
            string sComp = "";
            try
            {
                InsertProcessDate();
                //LoadProcessLedger();
                //if (IsProcessLedgerExists())
                //{
                if (IsVoucherLedgersMapped())
                {
                    resultArgs = DeleteProcessPayVouher();
                    if (resultArgs.Success)
                    {
                        DataTable dtDataConstruct = (DataTable)gcProcess.DataSource;

                        this.Cursor = Cursors.WaitCursor;
                        //Fill Selected Group Ids and Component Ids, each Id concatinate with ',' separator
                        bool isRecordSelected = false;
                        if (dtDataConstruct == null || dtDataConstruct.Rows.Count == 0)
                        {
                            XtraMessageBox.Show(this, "No record is available in the grid to process", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (gvProcess.RowCount != 0)
                        {
                            for (int i = 0; i <= dtDataConstruct.Rows.Count - 1; i++)
                            {
                                if (dtDataConstruct.Rows[i]["Identification"].ToString() == "G") //It is Group
                                    nGrpId = long.Parse(dtDataConstruct.Rows[i][2].ToString());
                                else
                                {
                                    isRecordSelected = (dtDataConstruct.Rows[i]["Check"].ToString() == "False") ? false : true;
                                    if (isRecordSelected) //if row is checked for selection
                                    {
                                        if (nGrpId > 0)
                                        {
                                            sGroup = sGroup + nGrpId + ","; //Group Id
                                            nGrpId = 0;
                                        }
                                        sComp = sComp + dtDataConstruct.Rows[i][2].ToString() + ","; //Component Id
                                    }
                                }
                            }
                            if ((!string.IsNullOrEmpty(sGroup)) && (!string.IsNullOrEmpty(sComp)))
                            {
                                sGroup = sGroup.Substring(0, sGroup.Length - 1);
                                sComp = sComp.Substring(0, sComp.Length - 1);
                            }
                            //string MappedStaffId = string.Empty;
                            //using (clsPrGroupStaff GroupStaff = new clsPrGroupStaff())
                            //{

                            //    DataTable dtMappedStaffs = GroupStaff.GetMappedStaffs(glkpProject.EditValue.ToString());
                            //    foreach (DataRow dr in dtMappedStaffs.Rows)
                            //    {
                            //        MappedStaffId += dr["STAFFID"].ToString() + ',';
                            //    }
                            //    MappedStaffId = MappedStaffId.Trim(',');
                            //}
                            if (sComp != "") //If component is selected, reprocess the Component
                            {
                                clsprCompBuild objCompBuild = new clsprCompBuild();
                                objCompBuild.ProcessComponent(PayRollId, sGroup, sComp, MappedStaffId, false, pgbProgress, false, true, true);
                                resultArgs = ProcessJournalEntry();
                                if (resultArgs.Success)
                                {
                                    pgbProgress.Value = 0;
                                    XtraMessageBox.Show(this, "Processed...", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageRender.ShowMessage(resultArgs.Message.ToString());
                                }
                            }
                            else
                            {
                                DialogResult res = XtraMessageBox.Show(this, "Group is not selected, select group(s) to process.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show(this, "No record is available in the grid to process", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        this.Cursor = Cursors.Default;
                    }
                    else
                    {
                        XtraMessageBox.Show(resultArgs.Message, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    XtraMessageBox.Show("Ledgers are not mapped with either Project or Components.Map the Ledgers and try again.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //}
                //else
                //{
                //    XtraMessageBox.Show("Map Income and Deduction Ledgers to Process the Payroll.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch
            {
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkShowfilter_CheckedChanged(object sender, EventArgs e)
        {
            gvProcess.OptionsView.ShowAutoFilterRow = chkShowfilter.Checked;
            if (chkShowfilter.Checked)
            {
                this.SetFocusRowFilter(gvProcess, colGroupName);
            }
        }

        private void gvProcess_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordcount.Text = gvProcess.RowCount.ToString();
        }

        private void frmReprocess_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowfilter.Checked = (chkShowfilter.Checked) ? false : true;
        }

        #region Integration with Finance Methods
        /// <summary>
        /// Show Map Ledger Form
        /// </summary>
        public void LoadProcessLedger()
        {
            frmMapProcessLedgers mapLedger = new frmMapProcessLedgers();
            mapLedger.ShowDialog();
        }


        /// <summary>
        /// Check all the Map Ledgers exists
        /// </summary>
        /// <returns></returns>
        //public bool IsProcessLedgerExists()
        //{
        //    bool IsExists = false;
        //    try
        //    {
        //        clsPayrollProcess mapledger = new clsPayrollProcess();
        //        resultArgs = mapledger.FetchMappedLedger();
        //        if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count == 2)
        //        {
        //            IncomeLedgerId = commem.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["LEDGER_ID"].ToString());
        //            DeductionLedgerId = commem.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[1]["LEDGER_ID"].ToString());
        //            ProcessDate = commem.DateSet.ToDate(resultArgs.DataSource.Table.Rows[1]["PROCESS_DATE"].ToString(), false);
        //            IsExists = true;
        //        }
        //        else
        //        {
        //            IsExists = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.ToString(), true);
        //    }
        //    return IsExists;
        //}

        /// <summary>
        /// Check whether Ledgers are mapped with the Components
        /// </summary>
        /// <returns></returns>
        public bool IsVoucherLedgersMapped()
        {
            bool IsMapped = false;
            try
            {
                DataView dvLedTempCheck = new DataView();
                using (clsprCompBuild objCompBuild = new clsprCompBuild())
                {
                    resultArgs = objCompBuild.FetchComponentMappedLedgers();
                    if (resultArgs.Success && resultArgs.DataSource.TableView.Table.Rows.Count > 0)
                    {
                        dvLedTempCheck = resultArgs.DataSource.TableView;
                        dvLedTempCheck.RowFilter = "LEDGER_ID <> 0 AND PROCESS_LEDGER_ID <> 0";
                        if (dvLedTempCheck.ToTable().Rows.Count == resultArgs.DataSource.TableView.Table.Rows.Count)
                        {
                            DataTable dtTempVal = dvLedTempCheck.ToTable();
                            string LedgerIdtemp = string.Empty;
                            foreach (DataRow dr in dtTempVal.Rows)
                            {
                                LedgerIdtemp += dr["LEDGER_ID"].ToString() + ',';
                            }
                            LedgerIdtemp = LedgerIdtemp.TrimEnd(',');
                            IsMapped = IsProjectLedgersMapped(LedgerIdtemp);
                        }
                        else
                        {
                            IsMapped = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return IsMapped;
        }

        /// <summary>
        /// Delete all the vouchers of the current payroll (While ReProcessing the payroll)
        /// </summary>
        /// <returns></returns>
        public ResultArgs DeleteProcessPayVouher()
        {
            //try
            //{
            //    clsPayrollProcess mapledger = new clsPayrollProcess();
            //    string ProId = glkpProject.EditValue != null ? glkpProject.EditValue.ToString() : "0";
            //    resultArgs = mapledger.DeleteVouchersbyPayId(ProId);
            //}
            //catch (Exception ex)
            //{
            //    MessageRender.ShowMessage(ex.ToString(), true);
            //}
            return resultArgs;
        }

        /// <summary>
        /// Integrating Payroll with Finance by passing Journal Entry
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
                        voucherTransaction.ProjectId = commem.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                        voucherTransaction.VoucherDate = GetProcessDate(); // this.commem.DateSet.ToDate(clsGeneral.PAYROLLDATE, false);
                        voucherTransaction.VoucherType = "JN";
                        voucherTransaction.Status = (int)YesNo.Yes;
                        voucherTransaction.TransVoucherMethod = (int)TransactionVoucherMethod.Automatic;
                        voucherTransaction.CreatedOn = this.commem.DateSet.ToDate(this.commem.DateSet.GetDateToday(false), false);
                        voucherTransaction.ModifiedOn = this.commem.DateSet.ToDate(this.commem.DateSet.GetDateToday(false), false);
                        voucherTransaction.CreatedBy = this.commem.NumberSet.ToInteger(this.LoginUser.LoginUserId.ToString());
                        voucherTransaction.ModifiedBy = this.commem.NumberSet.ToInteger(this.LoginUser.LoginUserId.ToString());
                        //voucherTransaction.Narration = "Payroll Processed for the " + dtTempProcess.Rows[Ptype]["Name"].ToString() + " Process type for the Month of " + clsGeneral.PAYROLL_MONTH;
                        voucherTransaction.Narration = "Payroll Processed for the Month of " + clsGeneral.PAYROLL_MONTH;
                        voucherTransaction.VoucherSubType = ledgerSubType.PAY.ToString();
                        voucherTransaction.ClientReferenceId = this.commem.NumberSet.ToInteger(clsGeneral.PAYROLL_ID.ToString()).ToString();
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

        /// <summary>
        /// Empty Structure of the Journal Entry
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
        /// Costructs the Raw data for the Journal Entry
        /// </summary>
        /// <returns></returns>
        private DataView ConstructData()
        {
            DataView dvtemp = new DataView();
            DataView dvLedgerDetails = new DataView();
            DataTable dtLedgerTemp = new DataTable();
            Processtype processtype = new Processtype();

            DataView dvPType = commem.EnumSet.GetEnumDataSource(processtype, Sorting.None);
            DataTable dtTempProcess = dvPType.ToTable();

            dtLedgerTemp = ConstructEmptySource();
            using (clsprCompBuild objCompBuild = new clsprCompBuild())
            {
                resultArgs = objCompBuild.FetchComponentValueToProcessLedger(MappedStaffId);
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
                                ProcessLedgerId = commem.NumberSet.ToInteger(dr["PROCESS_LEDGER_ID"].ToString());
                                dtLedgerTemp.Rows.Add(dr["LEDGER_ID"], string.Empty, commem.NumberSet.ToDecimal(dr["AMOUNT"].ToString()), 0.00, 0.00, 0.00, string.Empty, 0);
                            }
                            dtLedgerTemp.Rows.Add(ProcessLedgerId, string.Empty, 0.00, this.commem.NumberSet.ToDecimal(dvtemp.ToTable().Compute("SUM(AMOUNT)", "PROCESS_TYPE_ID=" + Ptype + "").ToString()), 0.00, 0.00, string.Empty, 0);
                        }
                    }
                    dvLedgerDetails = dtLedgerTemp.AsDataView();
                }
            }
            return dvLedgerDetails;
        }

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
                        dtTemp = commem.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["PROCESS_DATE"].ToString(), false);
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

        public string GetmappedStaffs()
        {
            string MapStaff = string.Empty;
            try
            {
                using (clsPrGroupStaff GroupStaff = new clsPrGroupStaff())
                {
                    DataTable dtMappedStaffs = GroupStaff.GetMappedStaffs(glkpProject.EditValue.ToString());
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

        public bool IsProjectLedgersMapped(string LedgId)
        {
            bool IsMapped = false;
            try
            {
                string[] LedgIds = LedgId.Split(',');
                DataView dvLedTempCheck = new DataView();
                using (clsprCompBuild objCompBuild = new clsprCompBuild())
                {
                    string ProjectId = glkpProject.EditValue != null ? glkpProject.EditValue.ToString() : "0";
                    //resultArgs = objCompBuild.IsCompLedgerMappedwithProject(LedgId, ProjectId);
                    //if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    //{
                    //    if (LedgIds.Length == resultArgs.DataSource.Table.Rows.Count)
                    //    {
                    //        IsMapped = true;
                    //    }
                    //    else
                    //    {
                    //        IsMapped = false;
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return IsMapped;
        }

        #endregion


        //  public DateTime dtprocessdate { get; set; }
    }
}