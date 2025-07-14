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
using DevExpress.XtraGrid.Columns;

namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmStaffOrder : frmPayrollBase
    {
        #region Declaration
        CommonMember UtilityMember = new CommonMember();
        ResultArgs resultArgs = new ResultArgs();
        private DataTable dtConst;
        private int RowIndex = 0;
        
        #endregion

        #region Properties
        private int payrollgroupid = 0;
        private int PayrollGroupid
        {
            get
            {
                return (glkpGroup.EditValue != null) ? UtilityMember.NumberSet.ToInteger(glkpGroup.EditValue.ToString()) : 0;
            }
            set
            {
                payrollgroupid = value;
            }
        }
        
       
        #endregion

        #region Constructor
        public frmStaffOrder()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods

        private void LoadPayrollGroups()
        {
            try
            {
                using (clsPayrollStaff paystaff = new clsPayrollStaff())
                {
                    resultArgs = paystaff.FetchGroups("Group");
                    glkpGroup.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        UtilityMember.ComboSet.BindGridLookUpCombo(glkpGroup, resultArgs.DataSource.Table, "Group Name", "GROUP ID");
                        glkpGroup.EditValue = glkpGroup.Properties.GetKeyValue(0);
                        SetArrowsState();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        
        public void LoadStaffMembers()
        {
            try
            {
                using (clsPayrollStaff SysStaff= new clsPayrollStaff())
                {
                    DataTable dtStaff = SysStaff.getPayrollStaffList();
                    dtStaff = dtStaff.DefaultView.ToTable(false, new string[] { "STAFF ID", "GROUPID", "STAFF CODE", "STAFF NAME", "KNOWN AS",
                                                  "DATE OF JOIN", "AGE", "GROUP", "STAFFORDER" });
                    dtStaff.Columns["STAFF CODE"].ColumnName = "STAFF_CODE";
                    dtStaff.Columns["KNOWN AS"].ColumnName = "KNOWN_AS";
                    dtStaff.Columns["DATE OF JOIN"].ColumnName = "DATE_OF_JOIN";
                    dtStaff.DefaultView.RowFilter = "GROUPID =" + PayrollGroupid;
                    dtStaff.DefaultView.Sort = "STAFFORDER";
                    gcGroupMembers.DataSource = null;
                    gcGroupMembers.DataSource = dtStaff.DefaultView.ToTable();
                }
                SetArrowsState();
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.ToString());
            }
        }

        private void SetArrowsState()
        {
            if (gvGroupMembers.RowCount >= 2)
                btnMoveUp.Visible = true;
            else
                btnMoveDown.Visible = false;
            if (gvGroupMembers.RowCount >= 2)
            {
                btnMoveDown.Enabled = btnMoveUp.Enabled = true;
            }
            else
            {
                btnMoveDown.Enabled = btnMoveUp.Enabled = false;
            }
            if (gvGroupMembers.RowCount == 1)
                btnMoveDown.Enabled = false;
        }

       

        public bool ValidateOrderMapComponentDetails()
        {
            bool isMapComp = true;
            if (string.IsNullOrEmpty(glkpGroup.Text.Trim()))
            {
                XtraMessageBox.Show("Group is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                glkpGroup.Focus();
                return false;
            }
            else if (gvGroupMembers.RowCount<=0)
            {
                XtraMessageBox.Show("No record is available in the grid.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
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

       
        private void frmOrderComponents_Load(object sender, EventArgs e)
        {
            LoadPayrollGroups();
        }

        private void glkpGroup_EditValueChanged(object sender, EventArgs e)
        {
            LoadStaffMembers();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (gcGroupMembers.DataSource != null)
                {
                    using (clsPrGroupStaff SysStaff = new clsPrGroupStaff())
                    {
                        DataTable dtStaffOrder = gcGroupMembers.DataSource as DataTable;
                        if (dtStaffOrder.Rows.Count > 0)
                        {
                            ResultArgs result = SysStaff.BulkUpdateStaffOrder(dtStaffOrder, clsGeneral.PAYROLL_ID);
                            if (result.Success)
                            {
                                this.ShowSuccessMessage("Staff Order is updated");
                            }
                            else
                            {
                                this.ShowMessageBox(result.Message);
                            }
                        }
                    }
                }
            }
            catch(Exception err) {
                this.ShowMessageBox(err.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvGroupMembers.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvGroupMembers, colName);
            }
        }

        private void gvGroupMembers_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvGroupMembers.RowCount.ToString();
        }
        #endregion

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = gvGroupMembers;
            view.GridControl.Focus();
            int index = view.FocusedRowHandle;
            if (index > 0)
            {
                DataRow row1 = view.GetDataRow(index);
                DataRow row2 = view.GetDataRow(index - 1);
                object[] value = row1.ItemArray;
                row1.ItemArray = row2.ItemArray;
                row2.ItemArray = value;

                view.FocusedRowHandle = index - 1;
                gvGroupMembers.ClearSorting();
                GenerateSortOrder();
            }
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = gvGroupMembers;
            view.GridControl.Focus();
            int index = view.FocusedRowHandle;
            if (index < view.DataRowCount - 1)
            {
                DataRow row1 = view.GetDataRow(index);
                DataRow row2 = view.GetDataRow(index + 1);
                object[] value = row1.ItemArray;
                row1.ItemArray = row2.ItemArray;
                row2.ItemArray = value;

                view.FocusedRowHandle = index + 1;
                gvGroupMembers.ClearSorting();
                GenerateSortOrder();
            }
        }

        private void GenerateSortOrder()
        {
            if (gcGroupMembers.DataSource != null)
            {
                DataTable dtStaffOrder = gcGroupMembers.DataSource as DataTable;
                                
                string sortString = "";
                foreach (GridColumn item in gvGroupMembers.SortedColumns)
                {
                    string order = (item.SortOrder == DevExpress.Data.ColumnSortOrder.Descending ? "DESC" : "ASC");
                    if (sortString == "")
                        sortString = "[" + item.FieldName + "] " + order;
                    else
                        sortString = sortString + ", " + "[" + item.FieldName + "]" + order;
                }

                if (!string.IsNullOrEmpty(sortString))
                {
                    dtStaffOrder.DefaultView.Sort = sortString;
                    dtStaffOrder = dtStaffOrder.DefaultView.ToTable();
                }

                foreach (DataRow dr in dtStaffOrder.Rows)
                {
                    dr.BeginEdit();
                    dr["STAFFORDER"] = dtStaffOrder.Rows.IndexOf(dr) + 1;
                    dr.EndEdit();
                }
                gcGroupMembers.DataSource = dtStaffOrder;
            }
        }

        private void gvGroupMembers_EndSorting(object sender, EventArgs e)
        {
            
        }

        private void btnGenerateOrder_Click(object sender, EventArgs e)
        {
            if (this.ShowConfirmationMessage("Are you sure to generate Staff order as per showing the list ?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                GenerateSortOrder();
            }
        }

    }
}