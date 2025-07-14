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
    public partial class frmOrderComponents : frmPayrollBase
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
                return (glkpGroup.EditValue != null) ? UtilityMember.NumberSet.ToInteger(glkpGroup.EditValue.ToString()) : 0;
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

        //private string mappedstaffid = string.Empty;
        //public string MappedStaffId
        //{
        //    get
        //    {
        //        return GetmappedStaffs();
        //    }
        //}

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
        public frmOrderComponents()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        public void LoadComponentDetails()
        {
            DataTable dTDetails = objPayrollComp.getComponentDetails(PayrollComponentId);
            gcComponent.DataSource = dTDetails;
        }

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
        private void LoadPayrollGroups()
        {
            try
            {
                resultArgs = staff.FetchGroups("Group");
                glkpGroup.Properties.DataSource = null;
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    borderChanged = new bool[resultArgs.DataSource.Table.Rows.Count];
                    FirstTimeOrder = new bool[resultArgs.DataSource.Table.Rows.Count];
                    //this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpPayrollGroups, resultArgs.DataSource.Table, "Group Name", "GROUP ID");
                    //glkpPayrollGroups.EditValue = glkpPayrollGroups.Properties.GetKeyValue(0);

                    using (CommonMethod SelectAll = new CommonMethod())
                    {
                        //DataTable dtProject = SelectAll.AddHeaderColumn(resultArgs.DataSource.Table, "GROUP ID", "Group Name");
                        UtilityMember.ComboSet.BindGridLookUpCombo(glkpGroup, resultArgs.DataSource.Table, "Group Name", "GROUP ID");
                        glkpGroup.EditValue = glkpGroup.Properties.GetKeyValue(0);
                    }

                    DataTable dtComponents = objPayrollComp.getComponentDetails(PayrollComponentId);
                    try
                    {
                        if (gvComponent.RowCount > 0)
                        {
                           btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = true;
                            SetArrowsState();
                        }
                    }
                    catch (Exception ex)
                    {
                        btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled;
                        SetArrowsState();
                    }
                }
                else
                {
                    btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled;
                }

            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        public bool ValidateOrderMapComponentDetails()
        {
            bool isMapComp = true;
            if (string.IsNullOrEmpty(glkpGroup.Text.Trim()))
            {
                //ShowMessageBox("Staff Name is empty");
                XtraMessageBox.Show("Group is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.SetBorderColor(glkpGroup);
                glkpGroup.Focus();
                return false;
                //XtraMessageBox.Show("Group is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (gvComponent.RowCount<=0)
            {
                XtraMessageBox.Show("No record is available in the grid.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
                //return;
            }
            else
            {
                glkpGroup.Focus();
            }
            return isMapComp;
        }


        protected virtual void SetFocusRowFilter(DevExpress.XtraGrid.Views.Grid.GridView gridview, DevExpress.XtraGrid.Columns.GridColumn colGridColumn)
        {
            gridview.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
            gridview.FocusedColumn = colGridColumn;
            gridview.OptionsFind.AllowFindPanel = false;
            gridview.ShowEditor();
        }

        #endregion

        #region Events

        private void gvComponent_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvComponent.RowCount.ToString();
        }

        private void frmOrderComponents_Load(object sender, EventArgs e)
        {
            LoadPayrollGroups();
        }

        private void glkpGroup_EditValueChanged(object sender, EventArgs e)
        {
            SelectedGroupIndex = glkpGroup.Properties.GetIndexByKeyValue(glkpGroup.EditValue);
            payrollcomponentid = UtilityMember.NumberSet.ToInteger(glkpGroup.EditValue.ToString());
            LoadComponentDetails();
            try
            {
                if (payrollcomponentid != 0)
                {
                    if (gvComponent.RowCount == 0)
                    {
                         //btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = false;
                    }
                    else
                    {
                       // btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = true;
                    }
                }
                else
                {
                    //btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = false;
                }
                SetArrowsState();
            }
            catch (Exception ex)
            {
               //btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateOrderMapComponentDetails())
                {
                    //if (string.IsNullOrEmpty(glkpGroup.Text) == null)
                    //{
                    //    XtraMessageBox.Show("Group is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                    //else if (gvComponent.RowCount == 0 & gvComponent.RowCount != null)
                    //{
                    //    XtraMessageBox.Show("No component is here to save the order, add components and proceed..", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    //return;
                    //}
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
                    if (objPrComponent.SaveAssignedComponent(clsGeneral.PAYROLL_ID, long.Parse(glkpGroup.EditValue.ToString()), dtConst))
                        //XtraMessageBox.Show("Record order is saved.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ShowSuccessMessage("Components order is saved");
                    FirstTimeOrder[SelectedGroupIndex] = true;
                    // }
                }
            }
            catch { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIncrease_Click(object sender, EventArgs e)
        {
            if (ValidateOrderMapComponentDetails())
            {
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
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (ValidateOrderMapComponentDetails())
            {
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
        }
        #endregion

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvComponent.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvComponent, colComponent);
            }
        }

    }
}