using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model.UIModel.Master;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.Utility.Common;
using System.Linq;
using DevExpress.XtraGrid;
using Bosco.Model.Transaction;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using Payroll.Model.UIModel;
using Bosco.Model.UIModel.Master;

namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmMapProjectPayroll : frmPayrollBase
    {

        #region Varaible Decalaration
        ResultArgs resultArgs = null;
        DataTable dtproject = new DataTable();
        ProjectSystem dataManager = new ProjectSystem();
        CommonMember commem = new CommonMember();
        #endregion

        #region Constructor
        public frmMapProjectPayroll()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        private int PayrollProjectid { get; set; }
        private DataTable dtMappedprojects { get; set; }
        #endregion

        #region Methods
        private void LoadAllProjects()
        {
            try
            {
                using (ProjectSystem projectSystem = new ProjectSystem())
                {
                    resultArgs = projectSystem.FetchProjects();
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {

                        dtproject = resultArgs.DataSource.Table;
                        if (!dtproject.Columns.Contains("SELECT"))
                        {
                            dtproject.Columns.Add("SELECT", typeof(Int32));
                        }
                        gcProjects.DataSource = dtproject;
                        gcProjects.RefreshDataSource();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }
        //private void CheckedListBoxBindDataSource(DataTable BindDataSource, CheckedListBoxControl chkCtrl, string ValueMember, string DisplayMember)
        //{
        //    if (BindDataSource != null)
        //    {
        //        chkCtrl.DataSource = BindDataSource;
        //        chkCtrl.ValueMember = ValueMember;
        //        chkCtrl.DisplayMember = DisplayMember;
        //    }
        //}
        private bool ValidateProject()
        {
            bool isProject = true;
            if (gcProjects.DataSource == null)
            {
                //XtraMessageBox.Show("Project is empty.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.MapProjectPayroll.MAP_PROJECT_PAYROLL_PROJECT_EMPTY));
                gvProjects.Focus();
                isProject = false;
            }
            return isProject;
        }
        private void SetProjectMapping()
        {
            try
            {
                DataView dvMapProjects = (gcProjects.DataSource as DataTable).AsDataView();
                string projectId = string.Empty;
                int PayrollId = Convert.ToInt32(clsGeneral.PAYROLL_ID);
                using (PayrollSystem payrollsys = new PayrollSystem())
                {
                    payrollsys.DeleteProjectPayroll();
                    dvMapProjects.RowFilter = "SELECT=1";
                    foreach (DataRow drproj in dvMapProjects.ToTable().Rows)
                    {
                        projectId = drproj["PROJECT_ID"].ToString();
                        PayrollProjectid = commem.NumberSet.ToInteger(projectId);
                        if (PayrollProjectid != 0)
                        {
                            resultArgs = payrollsys.SaveProjectPayroll(PayrollProjectid, PayrollId);
                        }
                    }

                    dvMapProjects.RowFilter = "";
                }

                if (gvProjects.SelectedRowsCount != 0)
                {
                    //XtraMessageBox.Show("Projects are mapped succesfully.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                    //this.ShowSuccessMessage("Projects are mapped succesfully. ");
                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.MapProjectPayroll.MAP_PROJECT_PAYROLL_MAP_PROJECT_INFO));
                }
                else
                {
                    //XtraMessageBox.Show("Projects are unmapped succesfully.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                    //this.ShowSuccessMessage("Projects are unmapped succesfully.");
                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.MapProjectPayroll.MAP_PROJECT_PAYROLL_UNMAP_PROJECT_INFO));
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }

        }
        private DataTable CreateColumns()
        {
            DataTable dtPayrollProject = new DataTable();
            dtPayrollProject.Columns.Add(new DataColumn("PROJECT_ID", typeof(int)));
            dtPayrollProject.Columns.Add(new DataColumn("PAYROLLID", typeof(int)));
            return dtPayrollProject;
        }
        private void LoadMappedProjects()
        {
            string itemValues = string.Empty;
            DataTable dtProjects = gcProjects.DataSource as DataTable;
            using (PayrollSystem payrollsys = new PayrollSystem())
            {

                dtMappedprojects = payrollsys.FetchMappedPayrollProjects().DataSource.Table;
                if (dtMappedprojects != null && dtMappedprojects.Rows.Count > 0)
                {
                    foreach (DataRow drMappedproject in dtMappedprojects.Rows)
                    {
                        foreach (DataRow dr in dtProjects.Rows)
                        {
                            if (commem.NumberSet.ToInteger(dr["PROJECT_ID"].ToString()) == commem.NumberSet.ToInteger(drMappedproject["PROJECT_ID"].ToString()))
                            {
                                dr["SELECT"] = 1;
                            }
                        }
                    }
                }
            }
        }
        private void LoadPayrollProjects()
        {
            try
            {
                DataTable dtProjects = gcProjects.DataSource as DataTable;
                using (PayrollSystem payrollsys = new PayrollSystem())
                {
                    DataTable dtPayrollProjects = dtMappedprojects = payrollsys.FetchPayrollProjects().DataSource.Table;
                    if (dtPayrollProjects != null && dtPayrollProjects.Rows.Count > 0)
                    {
                        foreach (DataRow drpayproject in dtPayrollProjects.Rows)
                        {
                            foreach (DataRow dr in dtProjects.Rows)
                            {
                                if (commem.NumberSet.ToInteger(dr["PROJECT_ID"].ToString()) == commem.NumberSet.ToInteger(drpayproject["PROJECT_ID"].ToString()))
                                {
                                    dr["SELECT"] = 1;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
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
        private void frmMapProjectPayroll_Load(object sender, EventArgs e)
        {
            LoadAllProjects();
            //LoadMappedProjects();
            LoadPayrollProjects();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateProject())
            {
                if (clsGeneral.checkPayrollexists())
                {
                    SetProjectMapping();
                }
                else
                {
                    LoadAllProjects();
                }
            }
        }
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvProjects.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                if (gvProjects.RowCount > 0)
                {
                    this.SetFocusRowFilter(gvProjects, colProject);
                }
            }
        }
        private void gvProjects_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvProjects.RowCount.ToString();
        }
        private void gvProjects_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {

        }
        #endregion

        private void frmMapProjectPayroll_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void rchkEdit_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit chked = (CheckEdit)sender;
            if (!chked.Checked)
            {
                using (PayrollSystem paysystem = new PayrollSystem())
                {
                    if (ValidateProject())
                    {
                        int PayrollProjectid = commem.NumberSet.ToInteger(gvProjects.GetRowCellValue(gvProjects.FocusedRowHandle, colProjectId.FieldName).ToString());

                        if (PayrollProjectid != 0)
                        {
                            int ProcessedProject = paysystem.CheckComponentsProcessedforProject(PayrollProjectid);
                            if (ProcessedProject > 0)
                            {
                                chked.Checked = true;
                                //XtraMessageBox.Show("Cannot unmap, It has association", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.MapProjectPayroll.MAP_PROJECT_PAYROLL_UNMAP_INFO));
                            }
                            else
                            {
                                chked.Checked = false;
                            }
                        }
                    }
                }
            }
        }

        private void chkLedgerSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtProjects = gcProjects.DataSource as DataTable;
                if (dtProjects != null && dtProjects.Rows.Count > 0)
                {
                    foreach (DataRow drproject in dtProjects.Rows)
                    {
                        if (chkLedgerSelectAll.Checked)
                        {
                            drproject["SELECT"] = 1;
                        }
                        else
                        {
                            if (dtMappedprojects != null && dtMappedprojects.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dtMappedprojects.Rows)
                                {
                                    foreach (DataRow drProjects in dtProjects.Rows)
                                    {
                                        if (commem.NumberSet.ToInteger(drProjects["PROJECT_ID"].ToString()) == commem.NumberSet.ToInteger(dr["PROJECT_ID"].ToString()))
                                        {
                                            drProjects["SELECT"] = 1;
                                        }
                                        else
                                        {
                                            drproject["SELECT"] = 0;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                drproject["SELECT"] = 0;
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message, true);
            }
            finally { }

        }
    }
}