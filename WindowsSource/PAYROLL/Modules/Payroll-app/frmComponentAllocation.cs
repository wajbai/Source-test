/*****************************************************************************************
*					Interface       : frmComponentAllocation
*					Object Involved : 
*					Purpose         : To Process a component,create a new component,and to allocate components to available groups
*					Date from       : 02-Dec-2014
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
    public partial class frmComponentAllocation : frmPayrollBase
    {
        #region Declaration
        public static string PayrollCaption = "Payroll Group Component ";
        CommonMember UtilityMember = new CommonMember();
        CommonMember commem = new CommonMember();
        ResultArgs resultArgs = new ResultArgs();
        private bool[] borderChanged;
        private int SelectedGroupIndex = 0;
        private bool[] FirstTimeOrder;
        private DataTable dtConst;
        private int RowIndex = 0;
        private clsPayrollComponent objPayrollComp = new clsPayrollComponent();
        private clsPayrollStaff staff = new clsPayrollStaff();
        private clsPrComponent objPrComponent = new clsPrComponent();
        private int iSetFocus = 0;
        private int iSelectedGroupIndex = 0;
        private DataSet ds1;
        private string selectedgroup = string.Empty;
        GridHitInfo downHitInfo = null;
        TransProperty Transaction = new TransProperty();
        UserProperty LoginUser = new UserProperty();
        #endregion

        #region Properties
        private int payrollcomponentid = 0;
        private int PayrollComponentId
        {
            get
            {
                return (glkpPayrollGroups.EditValue != null) ? UtilityMember.NumberSet.ToInteger(glkpPayrollGroups.EditValue.ToString()) : 0;
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

        int dragRowHandle = -1;
        int DragRowHandle
        {
            get { return dragRowHandle; }
            set
            {
                dragRowHandle = value;
                gcComponent.Invalidate();
            }
        }

        #endregion

        #region Constructor

        #endregion

        #region Events
        public frmComponentAllocation()
        {
            InitializeComponent();
        }

        private void btnAllocateComponent_Click(object sender, EventArgs e)
        {
            frmComponentWise allocatecomponent = new frmComponentWise();
            allocatecomponent.ShowDialog();
        }

        private void frmComponentAllocation_Load(object sender, EventArgs e)
        {
            LoadPayrollGroups();
            LoadProjects();
            LoadComponentDetails();
            AssignProcessDate();

            //SelectedGroupIndex = glkpPayrollGroups.Properties.GetIndexByKeyValue(glkpPayrollGroups.EditValue);
            //try
            //{
            //    if (gvComponent.RowCount == 0)
            //    {
            //        btnProcess.Enabled = btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = false;
            //    }
            //    else
            //    {
            //        btnProcess.Enabled = btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = true;

            //    }
            //    SetArrowsState();
            //}
            //catch (Exception ex)
            //{
            //    btnProcess.Enabled = btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = false;
            //}

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            //frmComponents objAdd = new frmComponents("Add");
            frmComponentAdd objAdd = new frmComponentAdd("Add");
            objAdd.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvComponent.RowCount == 0)
                {
                    XtraMessageBox.Show("No component is here to save the order, add components and proceed..", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                // if (borderChanged[SelectedGroupIndex])
                //  {
                //   this.dtConst = gcComponent.DataSource as DataTable;
                //   if (objPrComponent.SaveAssignedComponent(clsGeneral.PAYROLL_ID, long.Parse(glkpPayrollGroups.EditValue.ToString()), dtConst))
                //       XtraMessageBox.Show("Record order is saved.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    borderChanged[SelectedGroupIndex] = false;
                //  }
                //  if (!FirstTimeOrder[SelectedGroupIndex])
                //  {
                this.dtConst = gcComponent.DataSource as DataTable;
                if (objPrComponent.SaveAssignedComponent(clsGeneral.PAYROLL_ID, long.Parse(glkpPayrollGroups.EditValue.ToString()), dtConst))
                    //  XtraMessageBox.Show("Record order is saved.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FirstTimeOrder[SelectedGroupIndex] = true;
                // }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
        }

        #endregion

        #region Methods
        private void LoadPayrollGroups()
        {
            try
            {
                resultArgs = staff.FetchGroups("Group");
                glkpPayrollGroups.Properties.DataSource = null;
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    borderChanged = new bool[resultArgs.DataSource.Table.Rows.Count];
                    FirstTimeOrder = new bool[resultArgs.DataSource.Table.Rows.Count];
                    //this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpPayrollGroups, resultArgs.DataSource.Table, "Group Name", "GROUP ID");
                    //glkpPayrollGroups.EditValue = glkpPayrollGroups.Properties.GetKeyValue(0);

                    using (CommonMethod SelectAll = new CommonMethod())
                    {
                        DataTable dtProject = SelectAll.AddHeaderColumn(resultArgs.DataSource.Table, "GROUP ID", "Group Name");
                        UtilityMember.ComboSet.BindGridLookUpCombo(glkpPayrollGroups, resultArgs.DataSource.Table, "Group Name", "GROUP ID");
                        glkpPayrollGroups.EditValue = glkpPayrollGroups.Properties.GetKeyValue(0);
                    }

                    DataTable dtComponents = objPayrollComp.getComponentDetails(PayrollComponentId);
                    try
                    {
                        if (gvComponent.RowCount > 0)
                        {
                            btnProcess.Enabled = btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = true;
                            fraProcess.Visible = false;
                            SetArrowsState();
                        }
                    }
                    catch (Exception ex)
                    {
                        btnProcess.Enabled = btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = fraProcess.Visible = false;
                        SetArrowsState();
                    }
                }
                else
                {
                    this.btnProcess.Enabled = btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = fraProcess.Visible = false;
                }

            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void LoadProjects()
        {
            try
            {
                using (PayrollSystem Paysystem = new PayrollSystem())
                {
                    resultArgs = Paysystem.FetchPayrollProjects();
                    glkpProject.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        UtilityMember.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, Paysystem.AppSchema.PRPayrollProject.PROJECTColumn.ColumnName, Paysystem.AppSchema.PRPayrollProject.PROJECT_IDColumn.ColumnName);
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

        public void LoadComponentDetails()
        {
            DataTable dTDetails = objPayrollComp.getComponentDetails(PayrollComponentId);
            gcComponent.DataSource = dTDetails;
        }

        public void LoadProcessLedger()
        {
            frmMapProcessLedgers mapLedger = new frmMapProcessLedgers();
            mapLedger.ShowDialog();
        }

        //To set Arrow's State as(Disabled/Enabled)
        private void SetArrowsState()
        {
            if (gvComponent.RowCount >= 2)
                btnIncrease.Visible = true;
            else
                btnDecrease.Visible = false;
            if (gvComponent.RowCount >= 2)
            {
                btnDecrease.Enabled = btnIncrease.Enabled = true;
            }
            else
            {
                btnDecrease.Enabled = btnIncrease.Enabled = false;
            }
            if (gvComponent.RowCount == 1)
                btnDecrease.Enabled = false;
        }

        protected virtual void SetFocusRowFilter(DevExpress.XtraGrid.Views.Grid.GridView gridview, DevExpress.XtraGrid.Columns.GridColumn colGridColumn)
        {
            gridview.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
            gridview.FocusedColumn = colGridColumn;
            gridview.OptionsFind.AllowFindPanel = false;
            gridview.ShowEditor();
        }
        protected virtual void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadComponentDetails();
            gvComponent.FocusedRowHandle = RowIndex;
        }
        #endregion

        private void glkpPayrollGroups_EditValueChanged(object sender, EventArgs e)
        {
            SelectedGroupIndex = glkpPayrollGroups.Properties.GetIndexByKeyValue(glkpPayrollGroups.EditValue);
            payrollcomponentid = UtilityMember.NumberSet.ToInteger(glkpPayrollGroups.EditValue.ToString());
            LoadComponentDetails();
            try
            {
                if (payrollcomponentid != 0)
                {
                    if (gvComponent.RowCount == 0)
                    {
                        btnProcess.Enabled = btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = false;
                    }
                    else
                    {
                        btnProcess.Enabled = btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = true;
                    }
                }
                else
                {
                    btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = false;
                    btnProcess.Enabled = true;
                }
                SetArrowsState();
            }
            catch (Exception ex)
            {
                btnProcess.Enabled = btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = false;
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                Validate();
                InsertProcessDate();
                if (IsVoucherLedgersMapped())
                {
                    resultArgs = DeleteProcessPayVouher();
                    if (resultArgs.Success)
                    {
                        if (glkpProject.EditValue != null)
                        {
                            //layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            if (payrollcomponentid != 0)
                            {
                                if (gvComponent.RowCount == 0)
                                {
                                    XtraMessageBox.Show("No component is here to process,add components and proceed.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                                btnSave_Click(sender, e);
                                //  if (!FirstTimeOrder[SelectedGroupIndex] | borderChanged[SelectedGroupIndex])
                                //  {
                                //if (XtraMessageBox.Show("The Component's Order is not Set.Would you like to Save the Order?", "Payroll", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                //{
                                //    btnSave_Click(sender, e);
                                //}
                                //else if (XtraMessageBox.Show("Sure to Process the Records?", "Payroll", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                //{
                                //    return;
                                //}
                                //  }

                            }
                            if (clsGeneral.PAYROLL_ID == 0)
                                return;
                            fraProcess.Visible = true;
                            fraProcess.Refresh();

                            string PayGroup = string.Empty;
                            if (glkpPayrollGroups.EditValue.ToString() == glkpPayrollGroups.Properties.GetKeyValue(0).ToString())
                            {
                                DataTable dtGroups = (DataTable)glkpPayrollGroups.Properties.DataSource;
                                foreach (DataRow dr in dtGroups.Rows)
                                {
                                    PayGroup += dr["GROUP ID"].ToString() + ',';
                                }
                                PayGroup = PayGroup.TrimEnd(',');
                            }
                            else
                            {
                                PayGroup = glkpPayrollGroups.EditValue.ToString();
                            }
                            if (MappedStaffId != string.Empty)
                            {
                                if (PayrollComponentId == 0)
                                {
                                    if (XtraMessageBox.Show("Are you sure to process payroll for all the groups?", "Payroll", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                        using (clsprCompBuild objCompBuild = new clsprCompBuild())
                                        {
                                            objCompBuild.ProcessComponent(clsGeneral.PAYROLL_ID, PayGroup, "", MappedStaffId, false, fraProcess, false, false, true);
                                            resultArgs = ProcessJournalEntry();
                                            if (resultArgs.Success)
                                            {
                                                DialogResult drs = XtraMessageBox.Show("Processed....", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                if (drs == DialogResult.OK)
                                                {
                                                    layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                                }
                                            }
                                            else
                                            {
                                                MessageRender.ShowMessage(resultArgs.Message.ToString());
                                            }
                                            fraProcess.Refresh();
                                            fraProcess.Visible = false;
                                        }
                                    }
                                }
                                else
                                {
                                    layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                    using (clsprCompBuild objCompBuild = new clsprCompBuild())
                                    {
                                        objCompBuild.ProcessComponent(clsGeneral.PAYROLL_ID, PayGroup, "", MappedStaffId, false, fraProcess, false, false, true);
                                        resultArgs = ProcessJournalEntry();
                                        if (resultArgs.Success)
                                        {
                                            DialogResult drs = XtraMessageBox.Show("Processed....", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            if (drs == DialogResult.OK)
                                            {
                                                layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                            }
                                        }
                                        else
                                        {
                                            MessageRender.ShowMessage(resultArgs.Message.ToString());
                                        }
                                        fraProcess.Refresh();
                                        fraProcess.Visible = false;
                                    }
                                }

                            }
                            else
                            {
                                XtraMessageBox.Show("Staffs are not mapped with the group.Map the staff and try again.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show("Projects are not available to process payroll.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
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
                //  }
                //  else
                //  {
                //      XtraMessageBox.Show("Map Income and Deduction Ledgers to Process the Payroll.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //  }
            }
            catch
            {
                //  MessageRender.ShowMessage(ed.ToString(), true);
            }
        }
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
                        voucherTransaction.ProjectId = UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
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
        private DataView ConstructData()
        {
            DataView dvtemp = new DataView();
            DataView dvLedgerDetails = new DataView();
            DataTable dtLedgerTemp = new DataTable();
            Processtype processtype = new Processtype();

            DataView dvPType = UtilityMember.EnumSet.GetEnumDataSource(processtype, Sorting.None);
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
                                ProcessLedgerId = UtilityMember.NumberSet.ToInteger(dr["PROCESS_LEDGER_ID"].ToString());
                                dtLedgerTemp.Rows.Add(dr["LEDGER_ID"], string.Empty, UtilityMember.NumberSet.ToDecimal(dr["AMOUNT"].ToString()), 0.00, 0.00, 0.00, string.Empty, 0);
                            }
                            dtLedgerTemp.Rows.Add(ProcessLedgerId, string.Empty, 0.00, this.UtilityMember.NumberSet.ToDecimal(dvtemp.ToTable().Compute("SUM(AMOUNT)", "PROCESS_TYPE_ID=" + Ptype + "").ToString()), 0.00, 0.00, string.Empty, 0);
                        }
                    }
                    dvLedgerDetails = dtLedgerTemp.AsDataView();
                }
            }
            return dvLedgerDetails;
        }

        private void btnAllocateComponent_Click_1(object sender, EventArgs e)
        {
            DataTable ds = gcComponent.DataSource as DataTable;
            //frmComponentWise objAllocateComp = new frmComponentWise(this.btnProcess, this.btnSave, gcComponent, this.btnIncrease, this.btnDecrease, ds);
            //objAllocateComp.UpdateHeld += new EventHandler(OnUpdateHeld);
            //frmComponenttoGroupWise objAllocateComp = new frmComponenttoGroupWise(this.btnProcess, this.btnSave, gcComponent, this.btnIncrease, this.btnDecrease, ds);
            frmMapComponenttoGroups objAllocateComp = new frmMapComponenttoGroups();
            objAllocateComp.ShowDialog();

            LoadComponentDetails();
            SelectedGroupIndex = glkpPayrollGroups.Properties.GetIndexByKeyValue(glkpPayrollGroups.EditValue);
            try
            {
                if (payrollcomponentid != 0)
                {
                    if (gvComponent.RowCount == 0)
                    {
                        btnProcess.Enabled = btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = false;
                    }
                    else
                    {
                        btnProcess.Enabled = btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = true;
                    }
                }
                else
                {
                    btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = false;
                    btnProcess.Enabled = true;
                }
                SetArrowsState();
            }
            catch (Exception ex)
            {
                btnProcess.Enabled = btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = false;
            }
        }

        private void btnIncrease_Click(object sender, EventArgs e)
        {
            //DevExpress.XtraGrid.Views.Grid.GridView view = gvComponent;
            //view.GridControl.Focus();
            //int index = view.FocusedRowHandle;
            //if (index <= 0) return;

            //DataRow row1 = view.GetDataRow(index);
            //DataRow row2 = view.GetDataRow(index - 1);

            //object val1 = row1["Component"];
            //object val2 = row2["Component"];

            //row1["Component"] = val2;
            //row2["Component"] = val1;
            //view.FocusedRowHandle = index - 1;

            //ds1 = new DataSet("StaffData");
            DevExpress.XtraGrid.Views.Grid.GridView view = gvComponent;
            view.GridControl.Focus();
            int index = view.FocusedRowHandle;
            dtConst = new DataTable("Staff");
            //ds1.Tables.Add(dtConst);
            int columnCount = gvComponent.Columns.Count;
            if (index <= 0) return;
            DataRow row1 = view.GetDataRow(index);
            DataRow row2 = view.GetDataRow(index - 1);

            object[] getRowData = row1.ItemArray;
            object[] getPrevRowData = row2.ItemArray;
            DataColumn dc;
            for (int j = 0; j < columnCount; j++)
            {

                dc = new DataColumn(gvComponent.Columns[j].Caption);
                //dc.DataType =gvComponent.Columns[j].ColumnType;
                dtConst.Columns.Add(dc);
            }
            for (int i = 0; i < gvComponent.RowCount; i++)
            {
                DataTable dtchangetable = gcComponent.DataSource as DataTable;
                if (dtchangetable.Rows[i][1] != getPrevRowData[1])
                {
                    DataRow dr = dtConst.NewRow();
                    dtConst.Rows.Add(dr);
                    for (int j = 0; j < columnCount; j++)
                        dtConst.Rows[i][j] = dtchangetable.Rows[i][j];
                }
                else
                {
                    DataRow dr = dtConst.NewRow();
                    dtConst.Rows.Add(dr);
                    for (int j = 0; j < columnCount; j++)
                        dtConst.Rows[i][j] = getRowData[j];
                    dr = dtConst.NewRow();
                    dtConst.Rows.Add(dr);
                    i++;
                    iSetFocus = i - 1;
                    for (int j = 0; j < columnCount; j++)
                        dtConst.Rows[i][j] = getPrevRowData[j];
                }
            }
            //DataTable dtStaff = dtConst;
            gcComponent.DataSource = dtConst;
            gvComponent.FocusedRowHandle = iSetFocus;
            //bOrderChanged[iSelectedGroupIndex] = true;
        }

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


        #endregion


        private void btnDecrease_Click(object sender, EventArgs e)
        {
            //DevExpress.XtraGrid.Views.Grid.GridView view = gvComponent;
            //view.GridControl.Focus();
            //int index = view.FocusedRowHandle;
            //if (index >= view.DataRowCount - 1) return;

            //DataRow row1 = view.GetDataRow(index);
            //DataRow row2 = view.GetDataRow(index + 1);

            //object val1 = row1["Component"];
            //object val2 = row2["Component"];

            //row1["Component"] = val2;
            //row2["Component"] = val1;
            //view.FocusedRowHandle = index + 1;

            DevExpress.XtraGrid.Views.Grid.GridView view = gvComponent;
            view.GridControl.Focus();
            int index = view.FocusedRowHandle;
            if (index >= view.DataRowCount - 1) return;

            DataRow row1 = view.GetDataRow(index);
            DataRow row2 = view.GetDataRow(index + 1);

            ds1 = new DataSet("StaffData");
            dtConst = new DataTable("Staff");
            //ds1.Tables.Add(dtConst);
            int columnCount = gvComponent.Columns.Count;
            object[] getRowData = row1.ItemArray;
            object[] getNextRowData = row2.ItemArray;
            DataColumn dc;
            for (int j = 0; j < columnCount; j++)
            {
                dc = new DataColumn(gvComponent.Columns[j].Caption);
                // dc.DataType = gvComponent.Columns[j].ColumnType;
                dtConst.Columns.Add(dc);
            }
            for (int i = 0; i < gvComponent.RowCount; i++)
            {
                DataTable dtchangetable = gcComponent.DataSource as DataTable;
                if (dtchangetable.Rows[i][1] != getRowData[1])
                {
                    DataRow dr = dtConst.NewRow();
                    dtConst.Rows.Add(dr);
                    for (int j = 0; j < columnCount; j++)
                        dtConst.Rows[i][j] = dtchangetable.Rows[i][j];
                }
                else
                {
                    DataRow dr = dtConst.NewRow();
                    dtConst.Rows.Add(dr);
                    for (int j = 0; j < columnCount; j++)
                        dtConst.Rows[i][j] = getNextRowData[j];
                    dr = dtConst.NewRow();
                    dtConst.Rows.Add(dr);
                    i++;
                    iSetFocus = i;
                    for (int j = 0; j < columnCount; j++)
                        dtConst.Rows[i][j] = getRowData[j];
                }
            }

            //DataTable dtStaff = dtConst;
            gcComponent.DataSource = dtConst;
            gvComponent.FocusedRowHandle = iSetFocus;
            //   bOrderChanged[iSelectedGroupIndex] = true;

        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvComponent.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvComponent, colComponent);
            }
        }

        private void gvComponent_RowCountChanged_1(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvComponent.RowCount.ToString();

        }

        private void frmComponentAllocation_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void ucComponentAllocation_CreateComponentClicked(object sender, EventArgs e)
        {
            frmComponentAdd objAdd = new frmComponentAdd("Add");
            objAdd.ShowDialog();
        }

        private void ucComponentAllocation_MapComponentClicked(object sender, EventArgs e)
        {
            DataTable ds = gcComponent.DataSource as DataTable;
            if (!string.IsNullOrEmpty(glkpPayrollGroups.Text.Trim()))
            {
                selectedgroup = glkpPayrollGroups.EditValue.ToString();
            }
            frmMapComponenttoGroups objAllocateComp = new frmMapComponenttoGroups(selectedgroup);
            objAllocateComp.ShowDialog();

            LoadComponentDetails();
            SelectedGroupIndex = glkpPayrollGroups.Properties.GetIndexByKeyValue(glkpPayrollGroups.EditValue);
            try
            {
                if (payrollcomponentid != 0)
                {
                    if (gvComponent.RowCount == 0)
                    {
                        btnProcess.Enabled = btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = false;
                    }
                    else
                    {
                        btnProcess.Enabled = btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = true;
                    }
                }
                else
                {
                    btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = false;
                    btnProcess.Enabled = true;
                }
                SetArrowsState();
            }
            catch (Exception ex)
            {
                btnProcess.Enabled = btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = false;
            }
        }

        private void ProcessCmdKeys(KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Alt | Keys.T))
            {
                ShowCreateComponentForm();
            }
            if (e.KeyData == (Keys.Alt | Keys.M))
            {
                ShowMapComponentForm();
            }
        }
        private void ShowCreateComponentForm()
        {
            frmComponentAdd objAdd = new frmComponentAdd("Add");
            objAdd.ShowDialog();
        }
        private void ShowMapComponentForm()
        {
            DataTable ds = gcComponent.DataSource as DataTable;
            if (!string.IsNullOrEmpty(glkpPayrollGroups.Text.Trim()))
            {
                selectedgroup = glkpPayrollGroups.EditValue.ToString();
            }
            frmMapComponenttoGroups objAllocateComp = new frmMapComponenttoGroups(selectedgroup);
            objAllocateComp.ShowDialog();

            LoadComponentDetails();
            SelectedGroupIndex = glkpPayrollGroups.Properties.GetIndexByKeyValue(glkpPayrollGroups.EditValue);
            try
            {
                if (payrollcomponentid != 0)
                {
                    if (gvComponent.RowCount == 0)
                    {
                        btnProcess.Enabled = btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = false;
                    }
                    else
                    {
                        btnProcess.Enabled = btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = true;
                    }
                }
                else
                {
                    btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = false;
                    btnProcess.Enabled = true;
                }
                SetArrowsState();
            }
            catch (Exception ex)
            {
                btnProcess.Enabled = btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = false;
            }
        }

        private void frmComponentAllocation_KeyDown(object sender, KeyEventArgs e)
        {
            ProcessCmdKeys(e);
        }

        private void gvComponent_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            downHitInfo = null;

            GridHitInfo hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));
            if (Control.ModifierKeys != Keys.None)
                return;
            if (e.Button == MouseButtons.Left && hitInfo.InRow && hitInfo.RowHandle != GridControl.NewItemRowHandle)
                downHitInfo = hitInfo;
        }

        private void gvComponent_MouseMove(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Button == MouseButtons.Left && downHitInfo != null)
            {
                Size dragSize = SystemInformation.DragSize;
                Rectangle dragRect = new Rectangle(new Point(downHitInfo.HitPoint.X - dragSize.Width / 2,
                    downHitInfo.HitPoint.Y - dragSize.Height / 2), dragSize);

                if (!dragRect.Contains(new Point(e.X, e.Y)))
                {
                    view.GridControl.DoDragDrop(downHitInfo, DragDropEffects.All);
                    downHitInfo = null;
                }
            }
        }

        private void gcComponent_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(GridHitInfo)))
            {
                GridHitInfo downHitInfo = e.Data.GetData(typeof(GridHitInfo)) as GridHitInfo;
                if (downHitInfo == null)
                    return;

                GridControl grid = sender as GridControl;
                GridView view = grid.MainView as GridView;
                GridHitInfo hitInfo = view.CalcHitInfo(grid.PointToClient(new Point(e.X, e.Y)));
                if (hitInfo.InRow && hitInfo.RowHandle != downHitInfo.RowHandle && hitInfo.RowHandle != GridControl.NewItemRowHandle)
                    e.Effect = DragDropEffects.Move;
                else
                    e.Effect = DragDropEffects.None;
            }
        }

        private void gcComponent_DragDrop(object sender, DragEventArgs e)
        {
            GridControl grid = sender as GridControl;
            GridView view = grid.MainView as GridView;
            GridHitInfo srcHitInfo = e.Data.GetData(typeof(GridHitInfo)) as GridHitInfo;
            GridHitInfo hitInfo = view.CalcHitInfo(grid.PointToClient(new Point(e.X, e.Y)));
            int sourceRow = srcHitInfo.RowHandle;
            int targetRow = hitInfo.RowHandle;
            MoveRow(sourceRow, targetRow);
        }
        private void MoveRow(int sourceRow, int targetRow)
        {
            if (sourceRow == targetRow)
                return;

            GridView view = gvComponent;
            DataTable dtComponents = gcComponent.DataSource as DataTable;
            DataRow row1 = view.GetDataRow(sourceRow);
            DataRow row2 = view.GetDataRow(targetRow);
            DataRow temp = dtComponents.NewRow();
            if (dtComponents != null && dtComponents.Rows.Count > 1)
            {
                foreach (DataColumn dc in dtComponents.Columns)
                {
                    temp[dc] = row1[dc];
                    row1[dc] = row2[dc];
                    row2[dc] = temp[dc];
                }
            }
        }
        //public bool IsProcessLedgerExists()
        //{
        //    bool IsExists = false;
        //    try
        //    {
        //        clsPayrollProcess mapledger = new clsPayrollProcess();
        //        resultArgs = mapledger.FetchMappedLedger();
        //        if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count == 2)
        //        {
        //            IncomeLedgerId = UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["LEDGER_ID"].ToString());
        //            DeductionLedgerId = UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[1]["LEDGER_ID"].ToString());
        //            ProcessDate = UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[1]["PROCESS_DATE"].ToString(), false);
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

        public ResultArgs DeleteProcessPayVouher()
        {
            //try
            //{
            //    clsPayrollProcess mapledger = new clsPayrollProcess();
            //    string ProjectId = glkpProject.EditValue != null ? glkpProject.EditValue.ToString() : "0";
            //    resultArgs = mapledger.DeleteVouchersbyPayId(ProjectId);
            //}
            //catch (Exception ex)
            //{
            //    MessageRender.ShowMessage(ex.ToString(), true);
            //}
            return resultArgs;
        }

        public string GetmappedStaffs()
        {
            string MapStaff = string.Empty;
            try
            {
                string PayGroup = string.Empty;
                if (glkpPayrollGroups.EditValue.ToString() == glkpPayrollGroups.Properties.GetKeyValue(0).ToString())
                {
                    DataTable dtGroups = (DataTable)glkpPayrollGroups.Properties.DataSource;
                    foreach (DataRow dr in dtGroups.Rows)
                    {
                        PayGroup += dr["GROUP ID"].ToString() + ',';
                    }
                    PayGroup = PayGroup.TrimEnd(',');
                }
                else
                {
                    PayGroup = glkpPayrollGroups.EditValue.ToString();
                }
                using (clsPrGroupStaff GroupStaff = new clsPrGroupStaff())
                {
                    string Projectid = (glkpProject.EditValue != null) ? glkpProject.EditValue.ToString() : "0";
                    DataTable dtMappedStaffs = GroupStaff.GetProjectGroupMappedStaffs(Projectid, PayGroup);
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


        //private void ucComponentAllocation_SaveOrderClicked(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (gvComponent.RowCount == 0)
        //        {
        //            XtraMessageBox.Show("No component is here to save the order, add components and proceed..", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }
        //        if (borderChanged[SelectedGroupIndex])
        //        {
        //            this.dtConst = gcComponent.DataSource as DataTable;
        //            if (objPrComponent.SaveAssignedComponent(clsGeneral.PAYROLL_ID, long.Parse(glkpPayrollGroups.EditValue.ToString()), dtConst))
        //                XtraMessageBox.Show("Record order is saved.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            borderChanged[SelectedGroupIndex] = false;
        //        }
        //        if (!FirstTimeOrder[SelectedGroupIndex])
        //        {
        //            this.dtConst = gcComponent.DataSource as DataTable;
        //            if (objPrComponent.SaveAssignedComponent(clsGeneral.PAYROLL_ID, long.Parse(glkpPayrollGroups.EditValue.ToString()), dtConst))
        //                XtraMessageBox.Show("Record order is saved.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            FirstTimeOrder[SelectedGroupIndex] = true;
        //        }
        //    }
        //    catch { }
        //}
    }
}