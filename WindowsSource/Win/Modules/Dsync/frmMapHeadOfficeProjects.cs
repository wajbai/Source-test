using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Utility;
using Bosco.Utility.CommonMemberSet;
using Bosco.Model.Dsync;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace ACPP.Modules.Dsync
{
    public partial class frmMapHeadOfficeProjects : frmFinanceBaseAdd
    {
        #region Variables
        ResultArgs resultArgs = null;
        DataTable dtMisMatchedProject = null;
        DataTable dtModifiedProject = null;
        public DataTable dtMappedProjects = new DataTable();
        public int MismatchedRowCount = 0;
        #endregion

        #region Properties

        #endregion

        #region Constructor
        public frmMapHeadOfficeProjects()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void frmMapHeadOfficeProjects_Load(object sender, EventArgs e)
        {
            CloseWaitDialog();
            dtMisMatchedProject = this.AppSetting.MisMatchedProjects;
            dtModifiedProject = this.AppSetting.ModifiedProjects;
            MismatchedRowCount = dtMisMatchedProject.Rows.Count;
            LoadMisMatchedProjects();
            LoadHeadOfficeProjects();
            dtMappedProjects.Columns.Add("PROJECT_ID", typeof(int));
            dtMappedProjects.Columns.Add("PROJECT_NAME", typeof(string));
        }
        #endregion

        #region Methods
        private void LoadMisMatchedProjects()
        {
            if (dtMisMatchedProject != null && dtMisMatchedProject.Rows.Count > 0)
            {
                gcProjectMapping.DataSource = dtMisMatchedProject;
            }
        }
        private void LoadHeadOfficeProjects()
        {
            DataTable dtTemp = new DataTable();
            if (dtModifiedProject == null)
            {
                dtTemp.Columns.Add("PROJECT", typeof(string));
            }
            else
            {
                dtTemp = dtModifiedProject.Copy();
            }

            rglkpHeadOfficeProjects.DataSource = dtTemp;
            rglkpHeadOfficeProjects.DisplayMember = "PROJECT";
            rglkpHeadOfficeProjects.ValueMember = "PROJECT";
        }

        #endregion
        /// <summary>
        /// Selecting the Mapped Head Office Projects
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rglkpHeadOfficeProjects_Validating(object sender, CancelEventArgs e)
        {
            //string ProjectName = string.Empty;
            //int ProjectId = 0;
            //GridLookUpEdit gridLKPEdit = sender as GridLookUpEdit;
            //if (gridLKPEdit.EditValue != null)
            //{
            //    DataRowView drv = gridLKPEdit.GetSelectedDataRow() as DataRowView;
            //    if (drv != null)
            //    {
            //        ProjectName = drv["PROJECT"].ToString();
            //        ProjectId = this.AppSetting.NumberSet.ToInteger(gvProject.GetFocusedRowCellValue(gcolBranchProjectId).ToString());
            //        dtMappedProjects.Rows.Add(ProjectId, ProjectName);
            //    }
            //}
            dtMappedProjects = null;
            dtMappedProjects = (DataTable)gcProjectMapping.DataSource;
        }

        /// <summary>
        /// Mapping the Branch Office Projects
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMapProject_Click(object sender, EventArgs e)
        {
            // Checking whether  all the BO projects are mapped
            if (IsValid(dtMappedProjects))
            {
                string ProjectName = string.Empty;
                string MisMatchProjectName = string.Empty;
                // Rename the Branch Office Projects
                using (ImportMasterSystem importSystem = new ImportMasterSystem())
                {
                    if (dtMappedProjects != null && dtMappedProjects.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtMappedProjects.Rows)
                        {
                            int ProjectId = this.AppSetting.NumberSet.ToInteger(dr["PROJECT_ID"].ToString());
                            ProjectName = dr["PROJECT"].ToString();
                            MisMatchProjectName += ProjectName + ',';
                            if (ProjectId != 0 && !string.IsNullOrEmpty(ProjectName))
                            {
                                resultArgs = importSystem.UpdateImportMasterProjects(ProjectId, ProjectName);
                            }
                        }

                        // Get all the other Un Mapped Projects to save in the Branch Office
                        MisMatchProjectName = MisMatchProjectName.TrimEnd(',');
                        DataTable dtHOProjects = (DataTable)rglkpHeadOfficeProjects.DataSource;
                        DataTable dtunmappedHOProject = dtHOProjects.Clone();
                        DataView dvunmap = dtHOProjects.AsDataView();
                        if (dtHOProjects != null && dtHOProjects.Rows.Count > 0)
                        {
                            dvunmap.RowFilter = "PROJECT NOT IN ('" + CommonMethod.EscapeLikeValue(MisMatchProjectName) + "')";
                            dtunmappedHOProject.Merge(dvunmap.ToTable());
                            dvunmap.RowFilter = "";

                            if (dtunmappedHOProject != null && dtunmappedHOProject.Rows.Count > 0)
                            {
                                resultArgs = importSystem.SaveMasterHeadOfficeProjects(dtunmappedHOProject);

                            }
                            if (resultArgs.Success && resultArgs != null)
                            {
                                //XtraMessageBox.Show("Branch Office Projects Mapped to Head Office Projects Successfully", "ACPERP", MessageBoxButtons.OK);
                                //this.ShowWaitDialog("Updating Masters in Branch Office..");
                                this.ShowWaitDialog(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.MAP_HO_PROJECTS_UPDATING_MASTERS_BO_INFO));
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                            else
                            {
                                this.DialogResult = DialogResult.Cancel;
                            }
                        }
                    }
                    else
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            //else
            //{
            //    XtraMessageBox.Show("Map all the Branch office projects with the Head Office Projects", "ACPERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        public bool IsValid(DataTable dtMappedProject)
        {
            bool IsValid = false;

            DataTable dtMappedUniqueProjects = dtMappedProject.DefaultView.ToTable(true, "PROJECT_NAME");
            DataTable dtHOProjects = dtMappedProject.DefaultView.ToTable(true, "PROJECT");

            if (dtMappedUniqueProjects.Rows.Count != dtHOProjects.Rows.Count)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.MAP_HO_MORETHAN_HO_PROJECTS_MAP_BO_PROJECT_INFO));
                IsValid = false;
            }
            else
            {
                IsValid = true;
            }


            //foreach (DataRow dr in dtMappedProject.Rows)
            //{
            //    if (string.IsNullOrEmpty(dr["PROJECT"].ToString()))
            //    {
            //        IsValid = false;
            //    }
            //}
            //if (dtMappedProject == null || dtMappedProject.Rows.Count == 0)
            //{
            //    IsValid = false;
            //}
            // string proName = string.Empty;
            // bool IsFirst = true;
            //if (dtMappedProjects != null && dtMappedProjects.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dtMappedProject.Rows)
            //    {
            //        if (IsFirst)
            //        {
            //            proName = dr["PROJECT"].ToString().Trim() + ',';
            //            IsFirst = false;
            //        }
            //        else
            //        {
            //            if (!string.IsNullOrEmpty(dr["PROJECT"].ToString().Trim()))
            //            {
            //                //on 23/05/2017, existing code was checking mapped Ho project should not be mapped again with another Bo project
            //                //when they check project name with HO mapped project, error occured if both BO project start names equal
            //                //Modfied to check with "," (Katpadi St.Joseph's Home Society - FC, Katpadi St.Joseph's Home Society).

            //                //if (proName.Contains(dr["PROJECT"].ToString().Trim()))
            //                string mappedproject = dr["PROJECT"].ToString().Trim() + ',';
            //                if (proName.Contains(mappedproject))
            //                {
            //                    //this.ShowMessageBox("More than one Branch office projects are mapped with the one Head Office Project,clear and Try again");
            //                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.MAP_HO_MORETHAN_HO_PROJECTS_MAP_BO_PROJECT_INFO));
            //                    IsValid = false;
            //                    break;
            //                }
            //                proName += dr["PROJECT"].ToString().Trim() + ',';
            //            }

            //        }
            //    }
            //}
            return IsValid;
        }
    }
}