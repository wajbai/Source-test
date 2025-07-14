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
using Bosco.Utility;
using Bosco.Utility.Common;
using Bosco.Model.UIModel;
using ACPP.Modules;
using DevExpress.XtraGrid.Views.Grid;

namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmMapProjectStaff :frmPayrollBase
    {
        #region Variable Declaration
        CommonMember commem = new CommonMember();
        ResultArgs resultArgs = null;
        string StaffUnAlloted = "UNALLOTED";
        string GroupIds = "0";
        public event EventHandler UpdateHeld;
        #endregion

        #region Constructor
        public frmMapProjectStaff()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        #endregion

        #region Methods
        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            //if (KeyData == (Keys.Control | Keys.W) && xtcProject.SelectedTabPageIndex == 0)
            //{
            //    chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
            //}
            if (KeyData == (Keys.Control | Keys.H))
            {
                chkAvailableStaff.Checked = (chkAvailableStaff.Checked) ? false : true;
            }
            if (KeyData == (Keys.Control | Keys.F))
            {
                chkSelectedStaff.Checked = (chkSelectedStaff.Checked) ? false : true;
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
        private void FetchAllStaffs()
        {
            try
            {
                clsModPay.g_PayRollId = clsGeneral.PAYROLL_ID;
                using (clsPrGroupStaff GroupStaff = new clsPrGroupStaff())
                {
                    string ProjectId = glkpProject.EditValue.ToString();
                    DataTable dtUnMappedStaffs = new DataTable();
                    if (!string.IsNullOrEmpty(ProjectId))
                    {
                        dtUnMappedStaffs = GroupStaff.GetAllUnmappedStaffs();
                    }
                    if (dtUnMappedStaffs != null && dtUnMappedStaffs.Rows.Count > 0)
                    {
                        gcAvailableStaff.DataSource = dtUnMappedStaffs;
                    }
                    else
                    {
                        gcAvailableStaff.DataSource = null;
                    }


                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION);
            }
        }
        private void LoadProject(GridLookUpEdit glkProject)
        {
            try
            {
                using (PayrollSystem Paysystem = new PayrollSystem())
                {
                    resultArgs = LoadProjects(Paysystem);
                    glkProject.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {

                        commem.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, Paysystem.AppSchema.PRPayrollProject.PROJECTColumn.ColumnName, Paysystem.AppSchema.PRPayrollProject.PROJECT_IDColumn.ColumnName);
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
        private ResultArgs LoadProjects(PayrollSystem Paysystem)
        {
            return resultArgs = Paysystem.FetchPayrollProjects();
        }
        protected virtual void SetFocusRowFilter(DevExpress.XtraGrid.Views.Grid.GridView gridview, DevExpress.XtraGrid.Columns.GridColumn colGridColumn)
        {
            gridview.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
            gridview.FocusedColumn = colGridColumn;
            gridview.OptionsFind.AllowFindPanel = false;
            gridview.ShowEditor();
        }
        private void LoadUnAllotedStaffGroup()
        {
            try
            {
                clsModPay.g_PayRollId = clsGeneral.PAYROLL_ID;
                DataTable dtStaffUnAlloted;
                using (clsPrGroupStaff GroupStaff = new clsPrGroupStaff())
                {
                    object strUndefined = GroupStaff.GetUnAssignedStaff();
                    dtStaffUnAlloted = GroupStaff.FetchRecord(strUndefined, StaffUnAlloted);
                    if (dtStaffUnAlloted != null && dtStaffUnAlloted.Rows.Count > 0)
                        gcAvailableStaff.DataSource = dtStaffUnAlloted;
                    else
                        gcAvailableStaff.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION);
            }
        }
        private void LoadMappedStaffsforSelectedProject()
        {
            try
            {
                clsModPay.g_PayRollId = clsGeneral.PAYROLL_ID;
                using (clsPrGroupStaff GroupStaff = new clsPrGroupStaff())
                {
                    string ProjectId = glkpProject.EditValue.ToString();
                    DataTable dtMappedStaffs = new DataTable();
                    if (!string.IsNullOrEmpty(ProjectId))
                    {
                        dtMappedStaffs = GroupStaff.GetMappedStaffs(ProjectId);
                    }
                    if (dtMappedStaffs != null && dtMappedStaffs.Rows.Count > 0)
                    {
                        gcSelectedStaff.DataSource = dtMappedStaffs;
                    }
                    else
                    {
                        gcSelectedStaff.DataSource = null;
                    }


                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION);
            }
        }
        private void MoveInStaffs()
        {
            DataTable dtStaffs = gcAvailableStaff.DataSource as DataTable;
            DataTable dtSelectedStaffs = gcSelectedStaff.DataSource as DataTable;
            DataTable dtMappaedStaffs = new DataTable();
            string CheckedStaffIds = string.Empty;
            int[] SelectedStaffs = gvAvailableStaff.GetSelectedRows();
            if (dtStaffs.Rows.Count > 0 && dtStaffs != null)
            {
                dtMappaedStaffs = dtStaffs.Clone();
                if (ValidateMappingDetails())
                {
                    if (SelectedStaffs.Count() > 0)
                    {
                        DataRow drMapRow;
                        foreach (int item in SelectedStaffs)
                        {
                            drMapRow = gvAvailableStaff.GetDataRow(item);
                            CheckedStaffIds += drMapRow["STAFFID"].ToString() + ",";
                            if (drMapRow != null)
                            {
                                dtMappaedStaffs.ImportRow(drMapRow);
                            }
                        }
                        if (dtSelectedStaffs != null && dtSelectedStaffs.Rows.Count > 0)
                        {
                            dtMappaedStaffs.Merge(dtSelectedStaffs);
                        }
                        gcSelectedStaff.DataSource = dtMappaedStaffs;
                        //dtStaffs.DefaultView.RowFilter = String.Format("STAFFID NOT IN ({0})", CheckedStaffIds.TrimEnd(','));
                        CheckedStaffIds = CheckedStaffIds.TrimEnd(',');
                        DataView dvStaffDetails = dtStaffs.DefaultView;
                        dvStaffDetails.RowFilter = "STAFFID NOT IN (" + CheckedStaffIds + ")";
                        gcAvailableStaff.DataSource = dvStaffDetails.ToTable();
                        dvStaffDetails.RowFilter = "";
                    }
                    //else
                    //{
                    //    gcSelectedStaff.DataSource = null;
                    //}
                }
            }
        }
        private void MoveOutStaffs()
        {
            DataTable dtStaffs = gcSelectedStaff.DataSource as DataTable;
            DataTable dtSelectedStaffs = gcAvailableStaff.DataSource as DataTable;
            DataTable dtUnMappaedStaffs = new DataTable();
            string CheckedStaffIds = string.Empty;
            int[] UnSelectedStaffs = gvSelectedStaff.GetSelectedRows();
            DataRow drUnMapRow;
            if (dtStaffs != null && dtStaffs.Rows.Count > 0)
            {
                dtUnMappaedStaffs = dtStaffs.Clone();
                if (ValidateMappingDetails())
                {
                    if (UnSelectedStaffs.Count() > 0)
                    {

                        foreach (int item in UnSelectedStaffs)
                        {
                            drUnMapRow = gvSelectedStaff.GetDataRow(item);
                            CheckedStaffIds += drUnMapRow["STAFFID"].ToString() + ",";
                        }
                        CheckedStaffIds = CheckedStaffIds.TrimEnd(',');
                        if (CheckLoanExistsforStaff(CheckedStaffIds))
                        {
                            XtraMessageBox.Show("Cannot unmap, It has association ", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                            return;
                        }
                        else
                        {
                            foreach (int item in UnSelectedStaffs)
                            {
                                drUnMapRow = gvSelectedStaff.GetDataRow(item);
                                dtUnMappaedStaffs.ImportRow(drUnMapRow);
                            }
                        }
                        if (dtSelectedStaffs != null && dtSelectedStaffs.Rows.Count > 0)
                        {
                            dtUnMappaedStaffs.Merge(dtSelectedStaffs);
                        }
                        gcAvailableStaff.DataSource = dtUnMappaedStaffs;
                        //dtStaffs.DefaultView.RowFilter = String.Format("STAFFID NOT IN ({0})", CheckedStaffIds.TrimEnd(','));

                        DataView dvStaffDetails = dtStaffs.DefaultView;
                        dvStaffDetails.RowFilter = "STAFFID NOT IN (" + CheckedStaffIds + ")";
                        gcSelectedStaff.DataSource = dvStaffDetails.ToTable();
                        dvStaffDetails.RowFilter = "";
                    }
                    //else
                    //{
                    //    gcAvailableStaff.DataSource = null;
                    //}
                }
            }
        }
        private bool ValidateMappingDetails()
        {
            bool isVaild = true;
            if (string.IsNullOrEmpty(glkpProject.Text) || glkpProject.EditValue.ToString() == "0")
            {
                XtraMessageBox.Show("Project is empty.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                glkpProject.Focus();
                isVaild = false;
            }
            else if (gcAvailableStaff.DataSource == null && gcSelectedStaff.DataSource == null)
            {
                XtraMessageBox.Show("No record is available in the grid", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                isVaild = false;
            }
            return isVaild;
        }
        private bool CheckLoanExistsforStaff(string staffId)
        {
            bool isvalid = false;
            using (clsPrGroupStaff ObjGroupStaff = new clsPrGroupStaff())
            {
                int IsLoanExist = ObjGroupStaff.CheckLoanExists(staffId);
                if (IsLoanExist > 0)
                {
                    isvalid = true;
                }
            }
            return isvalid;
        }
        #endregion

        #region Events
        private void frmMapProjectStaff_Load(object sender, EventArgs e)
        {
            LoadProject(glkpProject);
            LoadUnAllotedStaffGroup();
            //LoadMappedStaffsforProjects();
        }
        private void chkSelectedStaff_CheckedChanged(object sender, EventArgs e)
        {
            gvSelectedStaff.OptionsView.ShowAutoFilterRow = chkSelectedStaff.Checked;
            if (chkSelectedStaff.Checked)
            {
                this.SetFocusRowFilter(gvSelectedStaff, colName);
                //gvLedgerDetail.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                //gvLedgerDetail.FocusedColumn = colLedger;
                //gvLedgerDetail.ShowEditor();
            }
        }

        private void chkAvailableStaff_CheckedChanged(object sender, EventArgs e)
        {
            gvAvailableStaff.OptionsView.ShowAutoFilterRow = chkAvailableStaff.Checked;
            if (chkAvailableStaff.Checked)
            {
                this.SetFocusRowFilter(gvAvailableStaff, colAvailName);
                //gvLedgerDetail.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                //gvLedgerDetail.FocusedColumn = colLedger;
                //gvLedgerDetail.ShowEditor();
            }
        }

        private void gvAvailableStaff_RowCountChanged(object sender, EventArgs e)
        {
            lblAvailRecordCount.Text = gvAvailableStaff.RowCount.ToString();
        }

        private void gvSelectedStaff_RowCountChanged(object sender, EventArgs e)
        {
            lblSlectedStaffCount.Text = gvSelectedStaff.RowCount.ToString();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateMappingDetails())
                {
                    int ProjectId = commem.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                    DataTable dtStaffIds = gcSelectedStaff.DataSource as DataTable;
                    DataTable dtUnmappedStaffIds = gcAvailableStaff.DataSource as DataTable;
                    string StaffIds = string.Empty;
                    if (dtUnmappedStaffIds != null && dtUnmappedStaffIds.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtUnmappedStaffIds.Rows)
                        {
                            StaffIds += dr["STAFFID"].ToString() + ",";
                        }
                    }
                    StaffIds = StaffIds.TrimEnd(',');
                    using (clsPrGroupStaff GroupStaff = new clsPrGroupStaff())
                    {
                        if (!string.IsNullOrEmpty(StaffIds))
                        {
                            resultArgs = GroupStaff.DeleteProjectIdStaff(ProjectId);

                            if (resultArgs.Success)
                            {
                                resultArgs = GroupStaff.SaveProjectStaff(ProjectId, dtStaffIds);

                                if (resultArgs.Success)
                                {
                                    if (gvSelectedStaff.RowCount > 0)
                                    {
                                        //XtraMessageBox.Show(MessageCatalog.Payroll.GroupAllocation.PROJECT_STAFF_ALLOCATION_SUCCESS, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.ShowSuccessMessage("Staff are mapped successfully.");
                                    }
                                    else
                                    {
                                        //XtraMessageBox.Show(MessageCatalog.Payroll.GroupAllocation.PROJECT_STAFF_UNMAP, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.ShowSuccessMessage("Staff are unmapped successfully.");
                                    }
                                }
                                else
                                {
                                    XtraMessageBox.Show(MessageCatalog.Payroll.GroupAllocation.PROJECT_STAFF_ALLOCATION_FAILURE, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        else
                        {
                            resultArgs = GroupStaff.DeleteProjectIdStaff(ProjectId);
                            if (resultArgs.Success)
                            {
                                resultArgs = GroupStaff.SaveProjectStaff(ProjectId, dtStaffIds);
                                if (resultArgs.Success)
                                {
                                    //XtraMessageBox.Show(MessageCatalog.Payroll.GroupAllocation.PROJECT_STAFF_ALLOCATION_SUCCESS, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //XtraMessageBox.Show(MessageCatalog.Payroll.GroupAllocation.PROJECT_STAFF_ALLOCATION_SUCCESS, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.ShowSuccessMessage("Staff are mapped successfully.");
                                }
                                else
                                {
                                    XtraMessageBox.Show(MessageCatalog.Payroll.GroupAllocation.PROJECT_STAFF_UNMAP, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.ShowSuccessMessage("Staff are unmapped successfully.");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMoveIn_Click(object sender, EventArgs e)
        {
            if (gcAvailableStaff.DataSource != null)
            {
                MoveInStaffs();
            }
        }

        private void btnMoveOut_Click(object sender, EventArgs e)
        {
            if (gcSelectedStaff.DataSource != null)
            {
                MoveOutStaffs();
            }
        }
        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            FetchAllStaffs();
            LoadMappedStaffsforSelectedProject();
        }
        #endregion

    }
}