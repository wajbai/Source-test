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
using Bosco.DAO.Data;
using Bosco.Utility;
using DevExpress.Web.ASPxEditors;
namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmGroupAllocation : frmPayrollBase
    {
        #region Declaration
        string StaffAlloted = "STAFFALLOTED";
        string GroupIds = "0";
        private int RowIndex = 0;
        #endregion

        #region Properties

        #endregion

        #region Constractors

        public frmGroupAllocation()
        {
            InitializeComponent();
        }

        #endregion

        #region Events
        private void frmGroupAllocation_Load(object sender, EventArgs e)
        {
            LoadGradeList();
            LoadAllotedStaffGroup();
        }

        private void btnAllocate_Click(object sender, EventArgs e)
        {
            frmSelectGroup SelectGroup = new frmSelectGroup();
            SelectGroup.UpdateHeld += new EventHandler(OnUpdateHeld);
            SelectGroup.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (clsPrGroupStaff GroupStaff = new clsPrGroupStaff())
                {
                    if (gcAlloted.DataSource != null)
                    {
                        DataTable dtAlloted = ((DataTable)gcAlloted.DataSource);
                        string Order = string.Empty;

                        DataTable dtGroup = (DataTable)gcSelectGroup.DataSource;

                        string[] sGroups = new string[dtGroup.Rows.Count];
                        int[] iOrder = new int[dtGroup.Rows.Count];

                        for (int i = 0; i < sGroups.Length; i++)
                        {
                            sGroups[i] = dtGroup.Rows[i]["Group Name"].ToString();
                            iOrder[i] = 0;
                        }

                        for (int i = 0; i < dtAlloted.Rows.Count; i++)
                        {
                            Order += dtAlloted.Rows[i]["Staff ID"].ToString();
                            Order += "|";
                            Order += dtAlloted.Rows[i]["Group ID"].ToString();
                            Order += "|";
                            for (int j = 0; j < sGroups.Length; j++)
                            {
                                if (dtAlloted.Rows[i]["Group"].ToString() == sGroups[j].ToString())
                                {
                                    iOrder[j] += 1;
                                    Order += iOrder[j].ToString();
                                }
                            }
                            Order += "@";
                        }
                        Order = Order.Trim();
                        Order = Order.TrimEnd('@');
                        if (GroupStaff.SaveStaffGroupOrder(Order))
                            XtraMessageBox.Show(MessageCatalog.Payroll.GroupAllocation.STAFF_ORDER_SUCCESS, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION,MessageBoxButtons.OK,MessageBoxIcon.Information);
                        else
                            XtraMessageBox.Show(MessageCatalog.Payroll.GroupAllocation.STAFF_ORDER_FAILURE, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION,MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION);
            }
        }

        //private void chklstGroups_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        //{
        //    try
        //    {
        //        string GroudIds = string.Empty;
        //        CheckedListBoxControl chklGroup = (CheckedListBoxControl)sender;
        //        GroupIds = GetGroupId(chklGroup);

        //        using (clsPrGroupStaff GroupStaff = new clsPrGroupStaff())
        //        {
        //            object strQuery = GroupStaff.GetGroupStaffSQL(GroupIds);
        //            DataTable dtAllotedStaff = GroupStaff.FetchRecord(strQuery, StaffAlloted, GroupIds);
        //            LoadGrid(dtAllotedStaff);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.ToString(), MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION);
        //    }
        //}

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                using (clsPrGroupStaff GroupStaff = new clsPrGroupStaff())
                {
                    if (gcAlloted.DataSource != null)
                    {
                        string StaffId = gvAlloted.GetFocusedRowCellValue(colId).ToString();
                        string GroupId = gvAlloted.GetFocusedRowCellValue(colGroupId).ToString();
                     //   ResultArgs resultArgs = GroupStaff.DeleteStaffGroup(StaffId, GroupId);
                        //if (!resultArgs.Success)
                        //{
                        //    XtraMessageBox.Show("Staff is not Un Mapped!", MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION,MessageBoxButtons.OK,MessageBoxIcon.Information);
                        //}

                    }
                    LoadAllotedStaffGroup();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION);
            }
        }

        private void gvAlloted_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvAlloted.RowCount.ToString();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvAlloted.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvAlloted, colStaffCode);
            }
        }


        /// <summary>
        /// To refresh the grid after adding auditor information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected virtual void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadAllotedStaffGroup();
            gvAlloted.FocusedRowHandle = RowIndex;
        }

        private void frmGroupAllocation_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void ucGroupAllocation_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucGroupAllocation_AddClicked(object sender, EventArgs e)
        {
            frmSelectGroup SelectGroup = new frmSelectGroup();
            SelectGroup.UpdateHeld += new EventHandler(OnUpdateHeld);
            SelectGroup.ShowDialog();
        }

        private void rbtnDeleteStaff_Click(object sender, EventArgs e)
        {
            DeleteStaff();
        }

        private void gvSelectGroup_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            try
            {
                int[] GroupIds = gvSelectGroup.GetSelectedRows();
                DataTable dtGroups = gcSelectGroup.DataSource as DataTable;
                if (GroupIds.Count() > 0)
                {
                    DataRow drSelectedGroup;
                    string SelectedGroupid = string.Empty;
                    foreach (int item in GroupIds)
                    {
                        drSelectedGroup = gvSelectGroup.GetDataRow(item);
                        if (drSelectedGroup != null)
                        {
                            SelectedGroupid += drSelectedGroup["GROUP ID"].ToString() + ',';
                        }

                    }
                    string PayrollGroupIds = SelectedGroupid.Trim(',');
                    using (clsPrGroupStaff GroupStaff = new clsPrGroupStaff())
                    {
                        object strquery = GroupStaff.GetGroupStaffSQL(PayrollGroupIds);
                        DataTable dtAllotedStaff = GroupStaff.FetchRecord(strquery, StaffAlloted, PayrollGroupIds);
                        LoadGrid(dtAllotedStaff);
                    }
                }
                else
                {
                    gcAlloted.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION);
            }
        }
        #endregion

        #region Methods

        private void LoadGradeList()
        {
            try
            {
                DataTable dtGradeList;
                using (clsPayrollGrade Grade = new clsPayrollGrade())
                {
                    dtGradeList = Grade.getPayrollGradeList();
                    if (dtGradeList != null && dtGradeList.Rows.Count > 0)
                        gcSelectGroup.DataSource = dtGradeList;
                    else
                        gcSelectGroup.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION);
            }

        }

        private void LoadAllotedStaffGroup()
        {
            try
            {
                using (clsPrGroupStaff GroupStaff = new clsPrGroupStaff())
                {
                    DataTable dtAllotedStaff;
                    object strQuery = GroupStaff.GetGroupStaffSQL(GroupIds);
                    dtAllotedStaff = GroupStaff.FetchRecord(strQuery, StaffAlloted, GroupIds);
                    LoadGrid(dtAllotedStaff);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION);
            }
        }

        private string GetGroupId(CheckedListBoxControl ChklGroup)
        {
            string GroupIds = string.Empty;
            foreach (DataRowView drv in ChklGroup.CheckedItems)
            {
                GroupIds += drv["GROUP ID"].ToString() + ",";
            }
            GroupIds = GroupIds.TrimEnd(',');
            return GroupIds;
        }

        private void LoadGrid(DataTable Source)
        {
            if (Source != null && Source.Rows.Count > 0)
                gcAlloted.DataSource = Source;
            else
                gcAlloted.DataSource = null;
        }

        protected virtual void SetFocusRowFilter(DevExpress.XtraGrid.Views.Grid.GridView gridview, DevExpress.XtraGrid.Columns.GridColumn colGridColumn)
        {
            gridview.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
            gridview.FocusedColumn = colGridColumn;
            gridview.OptionsFind.AllowFindPanel = false;
            gridview.ShowEditor();
        }
        private void DeleteStaff()
        {
            try
            {
                using (clsPrGroupStaff GroupStaff = new clsPrGroupStaff())
                {
                    if (gcAlloted.DataSource != null)
                    {
                        string StaffId = gvAlloted.GetFocusedRowCellValue(colId).ToString();
                        string GroupId = GroupIds = GetGroupID(gvSelectGroup.GetSelectedRows());
                        //string GroupId =GroupIds= gvAlloted.GetFocusedRowCellValue(colGroupId).ToString();
                        if (XtraMessageBox.Show("Are you sure to delete this record?","Payroll", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            ResultArgs resultArgs = GroupStaff.DeleteStaffGroup(StaffId);   // , GroupId  -> remove staff from group by staffid
                            if (!resultArgs.Success)
                            {
                                XtraMessageBox.Show("Staff is not Un Mapped!", MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    LoadAllotedStaffGroup();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION);
            }
        }

        private string GetGroupID(int[] groupId)
        {
            string tGroupId = string.Empty;
            string GroupId = string.Empty;
            foreach (int id in groupId)
            {
                tGroupId = gvSelectGroup.GetRowCellValue(id, colGrpId).ToString();
                GroupId += tGroupId + ",";
            }
            GroupId = GroupId.TrimEnd(',');
            return GroupId;
        }
        #endregion

        private void ucGroupAllocation_RefreshClicked(object sender, EventArgs e)
        {
            LoadGradeList();
            LoadAllotedStaffGroup();
        }
      
    }
}