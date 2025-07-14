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
using Bosco.Model;
using Bosco.DAO.Schema;
using Bosco.Model.Inventory;
using Bosco.Model.UIModel;
using Bosco.Model.Inventory.Asset;
using Bosco.Model.UIModel.Master;
using ACPP;
using ACPP.Modules.Master;

namespace ACPP.Modules.Inventory
{
    public partial class frmLocationsAdd : frmFinanceBaseAdd
    {
        #region Event Decelaration
        public event EventHandler UpdateHeld;
        AppSchemaSet appSchema = new AppSchemaSet();
        #endregion

        #region Variable Decelaration
        private ResultArgs resultArgs = null;
        private object mode = null;
        int GroupItemId = 0;
        private int ProjectID = 0;
        FormMode FrmMode;
        string AssignProjectIds = string.Empty;
        //AssetStockProduct.ILocation Ilocation = null;
        #endregion

        #region
        public int LocationID { get; set; }
        //public FinanceModule Module { get; set; }
        private int LocationType { get; set; }


        public string GridselectedProjectIds
        {
            // i made this as a set property for selecting Date
            get
            {
                string selectedprojectids = string.Empty;
                foreach (int index in gvProject.GetSelectedRows())
                {
                    DataRow dr = gvProject.GetDataRow(index) as DataRow;
                    selectedprojectids += dr["PROJECT_ID"].ToString().Trim() + ",";
                }
                return selectedprojectids.TrimEnd(',');
            }
        }

        #endregion

        #region Constructor
        public frmLocationsAdd()
        {
            InitializeComponent();
        }
        public frmLocationsAdd(int locationId, FormMode Mode, FinanceModule Module, string ProjectIds)
            : this()
        {
            this.LocationID = 0;
            LocationID = locationId;
            FrmMode = Mode;
            AssignProjectIds = ProjectIds;

            //Ilocation = AssetStockFactory.GetLocationInstance(Module);
        }

        //public frmLocationsAdd(int locationId, FormMode Mode, FinanceModule Module, int ProjectId)
        //    : this()
        //{
        //    this.LocationID = 0;
        //    LocationID = locationId;
        //    FrmMode = Mode;
        //    ProjectID = ProjectId;
        //    //Ilocation = AssetStockFactory.GetLocationInstance(Module);
        //}
        #endregion

        #region Events
        private void frmLocationsAdd_Load(object sender, EventArgs e)
        {
            LoadBlockDetails();
            LoadCustodian();
            LoadProjectDetails();
            if (LocationID > 0)
            {
                AssignToControls();
            }

            lblLocationType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            glkpCustodian.Focus();
            //if (LocationID > 0)
            //{
            //    if (dtResponsibleDate.DateTime == DateTime.MinValue)
            //    {
            //        dtResponsibleDate.Text = string.Empty;
            //    }
            //}
            //if (LocationID > 0)
            //{
            //    if (dtResponsibleDate.DateTime == DateTime.MinValue && dtResponsibleToDate.DateTime == DateTime.MinValue)
            //    {
            //        dtResponsibleDate.Text = dtResponsibleToDate.Text = string.Empty;
            //    }
            //}

            SetTitle();

            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                ApplyRights();
            }
            else
            {
                // glkpProject.Properties.Buttons[1].Visible = true;
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateControls())
                {
                    using (LocationSystem locationsystem = new LocationSystem())
                    {
                        locationsystem.MultipleProjectIds = GridselectedProjectIds;
                        locationsystem.LocationId = FrmMode == 0 ? 0 : this.LocationID;
                        locationsystem.Name = txtName.Text;
                        locationsystem.BlockId = this.UtilityMember.NumberSet.ToInteger(gklpBlock.EditValue.ToString());
                        locationsystem.CustodianId = glkpCustodian.EditValue.ToString() != null ? this.UtilityMember.NumberSet.ToInteger(glkpCustodian.EditValue.ToString()) : 0;
                        locationsystem.ResponsibleDate = dtResponsibleDate.DateTime;
                        locationsystem.ResponsibleToDate = dtResponsibleToDate.DateTime;
                        locationsystem.LocationType = this.UtilityMember.NumberSet.ToInteger(rgLocationType.SelectedIndex.ToString());

                        using (AssetMappingSystem assetMappingSystem = new AssetMappingSystem())
                        {

                            assetMappingSystem.LocationId = (FrmMode != 0) ? this.LocationID : 0;

                            // Commanded by Chinna (20/09/2024)
                            // assetMappingSystem.ProjectId = (FrmMode != 0) ? ProjectID : 0;
                            //if (FrmMode != Bosco.Utility.FormMode.Edit)
                            // {
                            //resultArgs = assetMappingSystem.DeleteMapping();
                            // }

                            resultArgs = assetMappingSystem.DeleteMapLocation();
                            if (resultArgs.Success)
                            {
                                // resultArgs = locationsystem.SaveLocationDetails(); //sdhakar
                                resultArgs = locationsystem.SaveLocationDetails(locationsystem.LocationId);
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    locationsystem.LocationId = (FrmMode == 0) ? UtilityMember.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : this.LocationID;
                                    locationsystem.MultipleProjectIds = (FrmMode.ToString() != "0") ? GridselectedProjectIds : "0";

                                    //  locationsystem.ProjectId = glkpProject.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
                                    //  locationsystem.ProjectId = glkpProject.EditValue != null ? get : 0;

                                    resultArgs = locationsystem.SaveAssetMapLocation();
                                    if (resultArgs != null && resultArgs.Success)
                                    {
                                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                                        ClearControls();
                                        LoadBlockDetails();
                                        if (UpdateHeld != null)
                                        {
                                            UpdateHeld(this, e);
                                            txtName.Focus();
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
            }
            finally
            { }
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtName);
            txtName.Text = this.UtilityMember.StringSet.ToTitleCase(txtName.Text.Trim());
        }
        #endregion


        #region User Rights

        private void ApplyRights()
        {
            // bool createprojectrights = (CommonMethod.ApplyUserRights((int)Forms.CreateProject) != 0);
            //glkpProject.Properties.Buttons[1].Visible = createprojectrights;
        }

        #endregion

        #region Methods

        public void LoadCustodianDetails()
        {
            //if (this.AppSetting.LockMasters == (int)YesNo.No)
            //{
            frmCustodiansAdd frmCustodiansAdd = new frmCustodiansAdd();
            frmCustodiansAdd.ShowDialog();
            if (frmCustodiansAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
            {
                LoadCustodian();
                if (frmCustodiansAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmCustodiansAdd.ReturnValue.ToString()) > 0)
                {
                    glkpCustodian.EditValue = this.UtilityMember.NumberSet.ToInteger(frmCustodiansAdd.ReturnValue.ToString());
                }
            }
            //}
            //else
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
            //}
        }

        public void LoadBlock()
        {
            //if (this.AppSetting.LockMasters == (int)YesNo.No)
            //{
            frmBlockAdd frmBlockAdd = new frmBlockAdd();
            frmBlockAdd.ShowDialog();
            if (frmBlockAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
            {
                LoadBlockDetails();
                if (frmBlockAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmBlockAdd.ReturnValue.ToString()) > 0)
                {
                    gklpBlock.EditValue = this.UtilityMember.NumberSet.ToInteger(frmBlockAdd.ReturnValue.ToString());
                }
            }
            //}
            //else
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
            //}
        }

        public void LoadProject()
        {
            //if (this.AppSetting.LockMasters == (int)YesNo.No)
            //{
            frmProjectAdd frmprojectAdd = new frmProjectAdd();
            frmprojectAdd.ShowDialog();
            if (frmprojectAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
            {
                LoadProjectDetails();
                //if (frmprojectAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmprojectAdd.ReturnValue.ToString()) > 0)
                //{
                //    glkpProject.EditValue = this.UtilityMember.NumberSet.ToInteger(frmprojectAdd.ReturnValue.ToString());
                //}
            }
            //}
            //else
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
            //}
        }

        private void LoadCustodian()
        {
            try
            {
                using (CustodiansSystem custodianSystem = new CustodiansSystem())
                {
                    resultArgs = custodianSystem.FetchAllCustodiansDetails();
                    if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs != null)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpCustodian, resultArgs.DataSource.Table, custodianSystem.AppSchema.AssetCustodians.CUSTODIANColumn.ColumnName, custodianSystem.AppSchema.AssetCustodians.CUSTODIAN_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        public void LoadBlockDetails()
        {
            try
            {
                using (LocationSystem locationsystem = new LocationSystem())
                {
                    resultArgs = locationsystem.FetchBlockDetails();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        using (CommonMethod GetMethod = new CommonMethod())
                        {
                            DataTable dtBlock = resultArgs.DataSource.Table;//GetMethod.AddHeaderColumn(resultArgs.DataSource.Table, appSchema.AppSchema.Block.BLOCK_IDColumn.ColumnName, appSchema.AppSchema.Block.BLOCKColumn.ColumnName, "Primary");
                            this.UtilityMember.ComboSet.BindGridLookUpCombo(gklpBlock, dtBlock, appSchema.AppSchema.Block.BLOCKColumn.ColumnName, appSchema.AppSchema.Block.BLOCK_IDColumn.ColumnName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
            }
        }

        public void LoadProjectDetails()
        {
            try
            {
                using (ProjectSystem projectSystem = new ProjectSystem())
                {
                    resultArgs = projectSystem.FetchProjectsDetails();
                    glkpProject.Properties.DataSource = null;
                    if (resultArgs != null && resultArgs.Success)
                    {
                        // DataTable dtBlock = resultArgs.DataSource.Table;//GetMethod.AddHeaderColumn(resultArgs.DataSource.Table, appSchema.AppSchema.Block.BLOCK_IDColumn.ColumnName, appSchema.AppSchema.Block.BLOCKColumn.ColumnName, "Primary");
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, appSchema.AppSchema.Project.PROJECTColumn.ColumnName, appSchema.AppSchema.Project.PROJECT_IDColumn.ColumnName);

                        this.BeginInvoke(new MethodInvoker(glkpProject.ShowPopup));
                        this.BeginInvoke(new MethodInvoker(glkpProject.ClosePopup));
                        //if (FrmMode == FormMode.Add)
                        //{
                        //    glkpProject.EditValue = 1;
                        //    glkpProject.Enabled = true;
                        //}
                        //else
                        //{
                        //    // 19/09/2024
                        //    // Editable false when edit the locations Details
                        //    glkpProject.Enabled = false;
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
            }
        }
        public bool ValidateControls()
        {
            bool IsLocationTrue = true;
            if (string.IsNullOrEmpty(glkpProject.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Project.PROJECT_NAME_EMPTY));
                //this.SetBorderColor(gklpBlock);
                IsLocationTrue = false;
                glkpProject.Focus();
            }
            else if (string.IsNullOrEmpty(GridselectedProjectIds))
            {
                this.ShowMessageBox(this.GetMessage("Location Project is empty"));
                this.SetBorderColorForGridLookUpEdit(glkpProject);
                IsLocationTrue = false;
                glkpProject.Focus();
            }
            else if (string.IsNullOrEmpty(gklpBlock.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Block.ASSETBLOCK_NAME_EMPTY));
                //this.SetBorderColor(gklpBlock);
                IsLocationTrue = false;
                gklpBlock.Focus();
            }
            else if (string.IsNullOrEmpty(txtName.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Location.LOCATION_NAME_EMPTY));
                this.SetBorderColor(txtName);
                IsLocationTrue = false;
                txtName.Focus();
            }
            else if (dtResponsibleToDate.Text.Trim() != string.Empty)
            {
                if (!this.UtilityMember.DateSet.ValidateDate(dtResponsibleDate.DateTime, dtResponsibleToDate.DateTime))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Location.TODATE_VALIDATION));
                    //this.ShowMessageBox("ResponsibleToDate cannot be less than ResponsibleDate.");
                    dtResponsibleToDate.Focus();
                    IsLocationTrue = false;
                }
            }
            else if (string.IsNullOrEmpty(glkpCustodian.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetCustodians.ASSETCUSTODIANS_NAME_EMPTY));
                //this.SetBorderColor(glkpCustodian);
                IsLocationTrue = false;
                glkpCustodian.Focus();
            }
            else if (!validateLocations())
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Location.LOCATION_AVAILABLE));
                IsLocationTrue = false;
                //this.SetBorderColor(txtName);
                txtName.Focus();
            }
            return IsLocationTrue;
        }

        private bool validateLocations()
        {
            bool istrue = true;
            using (LocationSystem locationSystem = new LocationSystem())
            {
                locationSystem.BlockId = this.UtilityMember.NumberSet.ToInteger(gklpBlock.EditValue.ToString());
                locationSystem.LocationId = this.LocationID;
                locationSystem.FrMode = this.FrmMode;
                locationSystem.Name = txtName.Text != null ? txtName.Text : string.Empty;
                locationSystem.LocationType = this.LocationType;
                resultArgs = locationSystem.FetchValidateLocationDetails();
                if (!resultArgs.Success)
                {
                    istrue = false;
                }
            }
            return istrue;
        }

        private void ClearControls()
        {
            if (FrmMode.Equals(FormMode.Add))
            {
                txtName.Text = string.Empty;
                glkpCustodian.EditValue = null;
                dtResponsibleDate.Text = string.Empty;
                dtResponsibleToDate.Text = string.Empty;
                gklpBlock.EditValue = null;
                rgLocationType.SelectedIndex = 0;
                gklpBlock.Focus();
            }
            else
            {
                //this.Close();
            }
        }

        private void AssignToControls()
        {
            try
            {
                if (FrmMode == FormMode.Edit)
                {
                    if (LocationID > 0)
                    {
                        using (LocationSystem locationsystem = new LocationSystem())
                        {
                            locationsystem.FillLocationProperties(LocationID);
                            if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                            {
                                this.LocationID = locationsystem.LocationId;
                                //locationsystem.MultipleProjectIds = GridselectedProjectIds;
                                AssignProjectIds = locationsystem.MultipleProjectIds = GridselectedProjectIds.Equals(string.Empty) ? locationsystem.MultipleProjectIds : GridselectedProjectIds;
                                //glkpProject.EditValue = locationsystem.ProjectId;
                                txtName.Text = locationsystem.Name;
                                gklpBlock.EditValue = locationsystem.BlockId;
                                glkpCustodian.EditValue = locationsystem.CustodianId;
                                if (locationsystem.ResponsibleDate == DateTime.MinValue)
                                {
                                    dtResponsibleDate.Text = string.Empty;
                                }
                                else
                                    dtResponsibleDate.DateTime = locationsystem.ResponsibleDate;
                                if (locationsystem.ResponsibleToDate == DateTime.MinValue)
                                {
                                    dtResponsibleToDate.Text = string.Empty;
                                }
                                else
                                    dtResponsibleToDate.DateTime = locationsystem.ResponsibleToDate;


                                //if (!locationsystem.ResponsibleDate.Equals(dtResponsibleDate.Properties.MinValue))
                                //{
                                //    dtResponsibleDate.DateTime = this.UtilityMember.DateSet.ToDate(locationsystem.ResponsibleDate.ToString(), false);
                                //}

                                //if (!locationsystem.ResponsibleToDate.Equals(dtResponsibleToDate.Properties.MinValue))
                                //{
                                //    dtResponsibleToDate.DateTime = this.UtilityMember.DateSet.ToDate(locationsystem.ResponsibleToDate.ToString(), false);
                                //}
                                //dtResponsibleToDate.DateTime = locationsystem.ResponsibleToDate;
                                rgLocationType.SelectedIndex = locationsystem.LocationType;
                                this.LocationType = locationsystem.LocationType;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
            }
            finally
            { }
        }

        private void SetTitle()
        {
            this.Text = FrmMode == FormMode.Add ? this.GetMessage(MessageCatalog.Asset.Location.LOCATION_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.Location.LOCATION_EDIT_CAPTION);
        }
        #endregion

        private void glkpCustodian_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                LoadCustodianDetails();
            }
        }

        private void gklpBlock_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                LoadBlock();
            }
        }

        private void gklpBlock_Leave(object sender, EventArgs e)
        {
            //this.SetBorderColor(gklpBlock);
        }

        private void glkpCustodian_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpCustodian);
        }


        //private void glkpProject_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
        //    {
        //        LoadProject();
        //    }
        //}

        private void glkpProject_Popup(object sender, EventArgs e)
        {
            GridLookUpEdit edit = sender as GridLookUpEdit;

            string[] proIds = AssignProjectIds.Split(',');
            foreach (string project in proIds)
            {
                for (int i = 0; i < gvProject.DataRowCount; i++)
                {
                    string getvalue = gvProject.GetRowCellValue(i, colProjectID).ToString();
                    if (getvalue != null && getvalue.Equals(project))
                    {
                        int rowHandle = gvProject.GetRowHandle(i);

                        if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                        {
                            edit.Properties.View.SelectRow(rowHandle);
                        }
                    }
                }
            }
        }

        private void glkpProject_Properties_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            int[] selectedrows = gvProject.GetSelectedRows();
            e.DisplayText = "(" + selectedrows.Length.ToString() + ") project(s) are selected";
        }

        private void glkpProject_QueryPopUp(object sender, CancelEventArgs e)
        {
            //19/07/2021, To set Popup widow size
            if (sender != null)
            {
                GridLookUpEdit editor = (GridLookUpEdit)sender;
                SetGridLookPopupWindowSize(editor);
            }
        }

        private void frmLocationsAdd_Shown(object sender, EventArgs e)
        {
            txtName.Focus();
        }

        //private void glkpProject_Properties_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        //{
        //    int[] selectedrows = gvProject.GetSelectedRows();
        //    e.DisplayText = "(" + selectedrows.Length.ToString() + ") project(s) are selected";
        //}

        //private void glkpProject_Popup(object sender, EventArgs e)
        //{
        //    GridLookUpEdit edit = sender as GridLookUpEdit;

        //    string[] proIds = AssignProjectIds.Split(',');
        //    foreach (string project in proIds)
        //    {
        //        for (int i = 0; i < gvProject.DataRowCount; i++)
        //        {
        //            string getvalue = gvProject.GetRowCellValue(i, colProjectName).ToString();
        //            if (getvalue != null && getvalue.Equals(project))
        //            {
        //                int rowHandle = gvProject.GetRowHandle(i);

        //                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
        //                {
        //                    edit.Properties.View.SelectRow(rowHandle);
        //                }
        //            }
        //        }
        //    }
        //}
    }
}