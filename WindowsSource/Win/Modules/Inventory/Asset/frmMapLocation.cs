/*************************************************************************************************************************
 *                                              Purpose     : Map Asset locations to projects
 *                                              Author      : Carmel Raj M
 *                                              Created On  : 21-October-2015
 *                                              Modified On : 
 *                                              Modified By :
 *************************************************************************************************************************/

using System;
using System.Data;
using System.Linq;
using Bosco.Utility;
using Bosco.Model.UIModel;
using Bosco.Model.Inventory.Asset;
using System.Collections.Generic;
using ACPP.Modules.Master;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmMapLocation : frmFinanceBaseAdd
    {
        #region Variables
        ResultArgs resultArgs = null;
        const string SELECT = "SELECT";
        int RecentProjectId = 0;
        #endregion

        #region Properties
        public int ProjectId { get { return glkProject.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkProject.EditValue.ToString()) : 0; } }
        #endregion

        #region Constructors
        public frmMapLocation()
        {
            InitializeComponent();
        }

        public frmMapLocation(int RecentProjectId)
            : this()
        {
            this.RecentProjectId = RecentProjectId;
        }
        #endregion

        #region Events
        private void frmMapLocation_Load(object sender, EventArgs e)
        {
            LoadProject();
            SetTitle();
            
            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                ApplyRights();
            }
            else
            {
                glkProject.Properties.Buttons[1].Visible = true;
            }
        }

        private void chkAvailableFilterLocation_CheckedChanged(object sender, EventArgs e)
        {
            gvAvailableLocation.OptionsView.ShowAutoFilterRow = chkAvailableFilterLocation.Checked;
            if (chkSelectedLocationFilter.Checked)
            {
                this.SetFocusRowFilter(gvAvailableLocation, colAvailableLocationBlock);
            }
        }

        private void chkSelectedLocationFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvSelectedLocation.OptionsView.ShowAutoFilterRow = chkSelectedLocationFilter.Checked;
            if (chkSelectedLocationFilter.Checked)
            {
                this.SetFocusRowFilter(gvSelectedLocation, colSelectedLocationBlock);
            }
        }

        private void gvSelectedLocation_RowCountChanged(object sender, EventArgs e)
        {
            lblSelectedLocationCount.Text = gvSelectedLocation.RowCount.ToString();
            chkSelectAllSelectedLocation.Checked = gvSelectedLocation.RowCount.ToString() == "0" ? false : true;
        }

        private void gvAvailableLocation_RowCountChanged(object sender, EventArgs e)
        {
            lblAvailableLocationCount.Text = gvAvailableLocation.RowCount.ToString();
            chkSelectAllAvailableLocation.Checked = gvAvailableLocation.RowCount.ToString() == "0" ? false : true;
        }

        private void chkSelectAllAvailableLocation_CheckedChanged(object sender, EventArgs e)
        {
            if ((gcAvailableLocation.DataSource as DataTable) != null && (gcAvailableLocation.DataSource as DataTable).Rows.Count > 0)
                (gcAvailableLocation.DataSource as DataTable).Select().ToList<DataRow>().ForEach(r => { r[SELECT] = chkSelectAllAvailableLocation.Checked ? 1 : 0; });
        }

        private void chkSelectAllSelectedLocation_CheckedChanged(object sender, EventArgs e)
        {
            if ((gcSelectedLocation.DataSource as DataTable) != null && (gcSelectedLocation.DataSource as DataTable).Rows.Count > 0)
                (gcSelectedLocation.DataSource as DataTable).Select().ToList<DataRow>().ForEach(r => { r[SELECT] = chkSelectAllSelectedLocation.Checked ? 1 : 0; });
        }

        private void glkProject_EditValueChanged(object sender, EventArgs e)
        {
            LoadBlockLocation();
        }

        private void btnMoveIn_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtAllLocation = (gcAvailableLocation.DataSource as DataTable);
                if (dtAllLocation != null)
                {
                    EnumerableRowCollection<DataRow> NewlySelectedLocations = dtAllLocation.AsEnumerable().Where(dr => dr.Field<Int32>(SELECT) == 1);
                    EnumerableRowCollection<DataRow> RemoveSelectedLocations = dtAllLocation.AsEnumerable().Where(dr => dr.Field<Int32>(SELECT) == 0);
                    if (NewlySelectedLocations.Count() > 0)
                    {
                        DataTable dtMappedRecords = NewlySelectedLocations.CopyToDataTable();
                        gcAvailableLocation.DataSource = RemoveSelectedLocations.Count() > 0 ? RemoveSelectedLocations.CopyToDataTable() : null;
                        if (gvSelectedLocation.RowCount == 0)
                        {
                            dtMappedRecords.Select().ToList<DataRow>().ForEach(dr => dr[SELECT] = 0);
                            gcSelectedLocation.DataSource = dtMappedRecords;
                        }
                        else
                        {
                            // Merge records with already mapped location or already moved records
                            dtMappedRecords = (gcSelectedLocation.DataSource as DataTable);
                            dtMappedRecords.Merge(NewlySelectedLocations.CopyToDataTable());
                            dtMappedRecords.Select().ToList<DataRow>().ForEach(dr => dr[SELECT] = 0);
                            gcSelectedLocation.DataSource = dtMappedRecords;
                        }
                        gvSelectedLocation.ExpandAllGroups();
                        gvAvailableLocation.ExpandAllGroups();
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Location.LOCATION_NORECORD_MOVE_INFO));
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        private void btnMoveOut_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtSelectedLocation = (gcSelectedLocation.DataSource as DataTable);
                if (dtSelectedLocation != null)
                {
                    EnumerableRowCollection<DataRow> SelectedLocations = dtSelectedLocation.AsEnumerable().Where(dr => dr.Field<Int32>(SELECT) == 1);
                    EnumerableRowCollection<DataRow> UnSelectedLocations = dtSelectedLocation.AsEnumerable().Where(dr => dr.Field<Int32>(SELECT) == 0);
                    if (SelectedLocations.Count() > 0)
                    {
                        DataTable dtUnMappedRecords = SelectedLocations.CopyToDataTable();
                        gcSelectedLocation.DataSource = UnSelectedLocations.Count() > 0 ? UnSelectedLocations.CopyToDataTable() : null;
                        if (gvAvailableLocation.RowCount == 0)
                        {
                            dtUnMappedRecords.Select().ToList<DataRow>().ForEach(dr => dr[SELECT] = 0);
                            gcAvailableLocation.DataSource = dtUnMappedRecords;
                        }
                        else
                        {
                            // Merge records with already mapped location or already moved records
                            dtUnMappedRecords = (gcAvailableLocation.DataSource as DataTable);
                            dtUnMappedRecords.Merge(SelectedLocations.CopyToDataTable());
                            dtUnMappedRecords.Select().ToList<DataRow>().ForEach(dr => dr[SELECT] = 0);
                            gcAvailableLocation.DataSource = dtUnMappedRecords;
                        }
                        gvSelectedLocation.ExpandAllGroups();
                        gvAvailableLocation.ExpandAllGroups();
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Location.LOCATION_NORECORD_MOVE_INFO));
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        private void glkProject_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                LoadProjectCombo();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (AssetMappingSystem assetMappingSystem = new AssetMappingSystem())
            {
              //  if (gcSelectedLocation.DataSource as DataTable != null)
               // {
                    assetMappingSystem.dtLocations = gcSelectedLocation.DataSource as DataTable;
                    assetMappingSystem.ProjectId = glkProject.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkProject.EditValue.ToString()) : 0;
                    resultArgs = assetMappingSystem.MapProjectLocation();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Location.LOCATION_MAP_SUCCESS_INFO));
                    }
                    else
                    {
                        ShowMessageBox(resultArgs.Message);
                    }
              //  }
               // else
                  //  ShowMessageBox(GetMessage(MessageCatalog.Common.COMMON_NO_RECORDS_TO_SAVE));
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region User Rights

        private void ApplyRights()
        {
            bool createprojectrights = (CommonMethod.ApplyUserRights((int)Forms.CreateProject) != 0);
            glkProject.Properties.Buttons[1].Visible = createprojectrights;
        }

        #endregion

        #region Methods
        private void LoadProject()
        {
            using (MappingSystem mappingSystem = new MappingSystem())
            {
                resultArgs = mappingSystem.FetchProjectsLookup();
                glkProject.Properties.DataSource = null;
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkProject, resultArgs.DataSource.Table, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                    glkProject.EditValue = RecentProjectId > 0 ? RecentProjectId.ToString() : this.AppSetting.UserProjectId;
                }
            }
        }

        private void LoadBlockLocation()
        {
            using (AssetMappingSystem mappingDetails = new AssetMappingSystem())
            {
                mappingDetails.ProjectId = ProjectId;
                resultArgs = mappingDetails.FetchBlockLocation();
                if (resultArgs != null && resultArgs.Success)
                {
                    gcAvailableLocation.DataSource = AddSelectColumn(resultArgs.DataSource.Table);
                    gvAvailableLocation.ExpandAllGroups();
                    resultArgs = mappingDetails.GetMappedProjectLocaion();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        gcSelectedLocation.DataSource = AddSelectColumn(resultArgs.DataSource.Table);
                        gvSelectedLocation.ExpandAllGroups();
                    }
                    else ShowMessageBox(resultArgs.Message);
                }
                else
                {
                    ShowMessageBox(resultArgs.Message);
                }
            }
        }

        public void LoadProjectCombo()
        {
            if (this.AppSetting.LockMasters == (int)YesNo.No)
            {
                frmProjectAdd frmprojectAdd = new frmProjectAdd();
                frmprojectAdd.ShowDialog();
                if (frmprojectAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                {
                    LoadProject();
                    if (frmprojectAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmprojectAdd.ReturnValue.ToString()) > 0)
                    {
                        glkProject.EditValue = this.UtilityMember.NumberSet.ToInteger(frmprojectAdd.ReturnValue.ToString());
                    }
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
            }
        }

        private DataTable AddSelectColumn(DataTable dtSource)
        {
            if (!dtSource.Columns.Contains(SELECT))
            {
                dtSource.Columns.Add(SELECT, typeof(Int32));
                dtSource.Select().ToList<DataRow>().ForEach(r => { r[SELECT] = 0; });
            }
            return dtSource;
        }

        public void SetTitle()
        {
            this.Text = this.GetMessage(MessageCatalog.Asset.Location.LOCATION_MAP_TITLE);
        }
        #endregion
    }
}