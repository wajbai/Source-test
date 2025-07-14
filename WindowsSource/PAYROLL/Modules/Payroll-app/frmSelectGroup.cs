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
using Bosco.Utility.Common;
using Bosco.Model.UIModel;
using ACPP.Modules;
using DevExpress.XtraGrid.Views.Grid;

namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmSelectGroup : frmPayrollBase
    {
        #region Declaration
        CommonMember commem = new CommonMember();
        string StaffUnAlloted = "UNALLOTED";
        string GroupIds = "0";
        public event EventHandler UpdateHeld;
        ResultArgs resultArgs = null;
        #endregion

        #region Properties



        #endregion

        #region Constractor

        public frmSelectGroup()
        {
            InitializeComponent();
        }
        #endregion
              
        #region Events
        private void frmSelectGroup_Load(object sender, EventArgs e)
        {
            LoadProject(glkpProject);
            LoadGradeList();
            LoadMappedStaffsforSelectedProject();
            //LoadUnAllotedStaffGroup();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvSelectGroup.RowCount > 0)
                {
                    long GroupId = Convert.ToInt32(gvSelectGroup.GetFocusedRowCellValue(colGroupID).ToString()) == 0 ? 0 : Convert.ToInt32(gvSelectGroup.GetFocusedRowCellValue(colGroupID).ToString());
                    string StaffId = GetStaffID(gvGroupMembers.GetSelectedRows());
                    using (clsPrGroupStaff GroupStaff = new clsPrGroupStaff())
                    {
                        string strResult = GroupStaff.SaveNewStaffInGroup(GroupId, StaffId);
                        if (gvGroupMembers.RowCount != 0)
                        {
                            if (string.IsNullOrEmpty(strResult))
                            {
                                XtraMessageBox.Show(MessageCatalog.Payroll.GroupAllocation.GROUP_ALLOCATION_SUCCESS, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadMappedStaffsforSelectedProject();
                                if (UpdateHeld != null)
                                    UpdateHeld(this, e);
                            }
                            else
                            {
                                XtraMessageBox.Show(MessageCatalog.Payroll.GroupAllocation.GROUP_ALLOCATION_FAILURE, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            //XtraMessageBox.Show("No record is avaliable in the grid", MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            XtraMessageBox.Show(this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_GRID_EMTPY_INFO), MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    //XtraMessageBox.Show("No record is avaliable in the grid", MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    XtraMessageBox.Show(this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_GRID_EMTPY_INFO), MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
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
            gvSelectGroup.FocusedRowHandle = 0;

        }

        private void gvGroupMembers_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvGroupMembers.RowCount.ToString();
        }

        #endregion

        #region Methods
        private void LoadProject(GridLookUpEdit glkProject)
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    resultArgs = LoadProjects(mappingSystem);
                    glkProject.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {

                        commem.ComboSet.BindGridLookUpCombo(glkProject, resultArgs.DataSource.Table, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                        glkProject.EditValue = glkProject.Properties.GetKeyValue(0);
                        //  grdlProject.EditValue = grdlProject.Properties.GetKeyValue(0);
                    }

                    //else
                    //{
                    //    if (this.ShowConfirmationMessage("Project is not yet created. Do you want to create Project now?", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    //    {
                    //        frmProjectAdd frmProject = new frmProjectAdd((int)AddNewRow.NewRow);
                    //        frmProject.ShowDialog();
                    //        if (frmProject.DialogResult == DialogResult.OK)
                    //        {
                    //            LoadProject(grdlProjectName);
                    //        }
                    //    }
                    //}
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }
        private ResultArgs LoadProjects(MappingSystem mappingSystem)
        {
            return resultArgs = mappingSystem.FetchProjectsLookup();
        }
        private void LoadMappedStaffsforSelectedProject()
        {
            try
            {
                clsModPay.g_PayRollId = clsGeneral.PAYROLL_ID;
                using (clsPrGroupStaff GroupStaff = new clsPrGroupStaff())
                {
                    if (ValidateMappingDetails())
                    {
                        string ProjectId = glkpProject.EditValue.ToString();
                        DataTable dtMappedStaffs = GroupStaff.GetUnmappedStaffs(ProjectId);
                        if (dtMappedStaffs != null && dtMappedStaffs.Rows.Count > 0)
                        {
                            gcGroupMembers.DataSource = dtMappedStaffs;
                        }
                        else
                        {
                            gcGroupMembers.DataSource = null;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION);
            }
        }
        private bool ValidateMappingDetails()
        {
            bool isVaild = true;
            if (string.IsNullOrEmpty(glkpProject.Text) || glkpProject.EditValue.ToString() == "0")
            {
                //XtraMessageBox.Show("Project is empty.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Staff.STAFF_PROJECT_EMPTY));
                glkpProject.Focus();
                isVaild = false;
            }
            return isVaild;
        }
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

        //private void LoadUnAllotedStaffGroup()
        //{
        //    try
        //    {
        //        clsModPay.g_PayRollId = clsGeneral.PAYROLL_ID;
        //        DataTable dtStaffUnAlloted;
        //        using (clsPrGroupStaff GroupStaff = new clsPrGroupStaff())
        //        {
        //            object strUndefined = GroupStaff.GetUnDefinedStaffGroupSQL();
        //            dtStaffUnAlloted = GroupStaff.FetchRecord(strUndefined, StaffUnAlloted);
        //            if (dtStaffUnAlloted != null && dtStaffUnAlloted.Rows.Count > 0)
        //                gcGroupMembers.DataSource = dtStaffUnAlloted;
        //            else
        //                gcGroupMembers.DataSource = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.ToString(), MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION);
        //    }
        //}

        private string GetStaffID(int[] staffId)
        {
            string tStaffId = string.Empty;
            string StaffId = string.Empty;
            foreach (int id in staffId)
            {
                tStaffId = gvGroupMembers.GetRowCellValue(id, colStaffId).ToString();
                StaffId += tStaffId + ",";
            }
            StaffId = StaffId.TrimEnd(',');
            return StaffId;
        }

        protected virtual void SetFocusRowFilter(DevExpress.XtraGrid.Views.Grid.GridView gridview, DevExpress.XtraGrid.Columns.GridColumn colGridColumn)
        {
            gridview.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
            gridview.FocusedColumn = colGridColumn;
            gridview.OptionsFind.AllowFindPanel = false;
            gridview.ShowEditor();
        }
        #endregion

        private void gvSelectGroup_RowCountChanged(object sender, EventArgs e)
        {
            lblGroupRecordCount.Text = gvSelectGroup.RowCount.ToString();
        }

        private void chkGroupFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvSelectGroup.OptionsView.ShowAutoFilterRow = chkGroupFilter.Checked;
            if (chkGroupFilter.Checked)
            {
                gvSelectGroup.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                gvSelectGroup.FocusedColumn = colGroupName;
                gvSelectGroup.ShowEditor();
            }
            gvSelectGroup.FocusedRowHandle = 0;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            //if (KeyData == (Keys.Control | Keys.W) && xtcProject.SelectedTabPageIndex == 0)
            //{
            //    chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
            //}
            if (KeyData == (Keys.Control | Keys.H))
            {
                chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
            }
            else if (KeyData == (Keys.Control | Keys.O))
            {
                chkGroupFilter.Checked=(chkGroupFilter.Checked) ? false : true;
            }
            //if (KeyData == (Keys.Control | Keys.W) && xtcProject.SelectedTabPageIndex == 1)
            //{
            //    chkShowFilterLedger.Checked = (chkShowFilterLedger.Checked) ? false : true;
            //}
            //if (KeyData == (Keys.Control | Keys.W) && xtcProject.SelectedTabPageIndex == 2)
            //{
            //    chkCostCentreFilter.Checked = (chkCostCentreFilter.Checked) ? false : true;
            //}
            //if (KeyData == (Keys.Control | Keys.W) && xtcProject.SelectedTabPageIndex == 3)
            //{
            //    chkShowFilterDonor.Checked = (chkShowFilterDonor.Checked) ? false : true;
            //}
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        private void frmSelectGroup_ShowFilterClicked(object sender, EventArgs e)
        {
            chkGroupFilter.Checked = (chkGroupFilter.Checked) ? false : true;
        }

        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            LoadMappedStaffsforSelectedProject();
        }
    }
}