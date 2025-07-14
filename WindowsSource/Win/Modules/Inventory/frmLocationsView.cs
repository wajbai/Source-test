using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;

using Bosco.Model.Inventory;
using Bosco.Model;
using Bosco.Utility;
using ACPP.Modules.Data_Utility;
using Bosco.Model.Inventory.Asset;

namespace ACPP.Modules.Inventory
{
    public partial class frmLocationsView : frmFinanceBase
    {
        #region Varialbe Decelaration
        int RowIndex = 0;
        int locationId = 0;
        TreeListNode Selectednode = null;
        FinanceModule FrmModule;
        private int TempLocationId { get; set; }
        public FinanceModule module { get; set; }
        //AssetStockProduct.ILocation Ilocation;
        #endregion

        #region Event Decelaration
        public event EventHandler UpdateHeld;
        #endregion

        #region Properties
        public int LocationID
        {
            get
            {
                RowIndex = gvLocationView.FocusedRowHandle;
                locationId = gvLocationView.GetFocusedRowCellValue(colId) != null ? this.UtilityMember.NumberSet.ToInteger(gvLocationView.GetFocusedRowCellValue(colId).ToString()) : 0;
                return locationId;
            }
            set
            {
                locationId = value;
            }
        }
        private int ProjectId = 0;
        public int ProjectID
        {
            get
            {
                RowIndex = gvLocationView.FocusedRowHandle;
                ProjectId = gvLocationView.GetFocusedRowCellValue(colProjectID) != null ? this.UtilityMember.NumberSet.ToInteger(gvLocationView.GetFocusedRowCellValue(colProjectID).ToString()) : 0;
                return ProjectId;
            }
            set
            {
                ProjectId = value;
            }
        }

        private string selectedProjectIds;
        public string SelectedProjectIds
        {
            get
            {
                selectedProjectIds = gvLocationView.GetFocusedRowCellValue(colProjectID).ToString() != null ? gvLocationView.GetFocusedRowCellValue(colProjectID).ToString() : string.Empty;
                return selectedProjectIds;
            }
            set
            {
                selectedProjectIds = value;
            }
        }
        #endregion

        #region Constructor

        public frmLocationsView()
        {
            InitializeComponent();
        }

        public frmLocationsView(FinanceModule module)
            : this()
        {
            //Ilocation = AssetStockFactory.GetLocationInstance(module);
            FrmModule = module;
        }

        #endregion

        #region Events

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            RefreshTreeView();

        }

        private void frmLocationsView_Load(object sender, EventArgs e)
        {
            SetVisibileShortCuts(true, true);
            LoadAssetLocationDetails();
            //LoadStockLocationDetails();
            SetTitle();
            if (FrmModule.ToString().Equals("Asset"))
                ApplyUserRights();
            else
                ApplyUserRightsStock();

        }

        private void uctbLocationView_AddClicked(object sender, EventArgs e)
        {
            ShowLocationDetails((int)AddNewRow.NewRow, string.Empty);
        }

        private void uctbLocationView_DeleteClicked(object sender, EventArgs e)
        {
            DeleteLocationDetails();
        }

        private void uctbLocationView_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void uctbLocationView_EditClicked(object sender, EventArgs e)
        {
            ShowLocationEditForm();
        }

        private void uctbLocationView_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcLocationView, this.GetMessage(MessageCatalog.Asset.Location.LOCATION_PRINT_CAPTION), PrintType.DT, gvLocationView, true);
        }

        private void uctbLocationView_RefreshClicked(object sender, EventArgs e)
        {
            LoadAssetLocationDetails();
        }

        private void trlLocation_DoubleClick(object sender, EventArgs e)
        {
            ShowLocationEditForm();
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(LocationForm.CreateLocation);
            this.enumUserRights.Add(LocationForm.EditLocation);
            this.enumUserRights.Add(LocationForm.DeleteLocation);
            this.enumUserRights.Add(LocationForm.ViewLocation);
            this.ApplyUserRights(uctbLocationView, enumUserRights, (int)Menus.Location);
        }

        private void ApplyUserRightsStock()
        {
            this.enumUserRights.Add(StockLocation.CreateStockLocation);
            this.enumUserRights.Add(StockLocation.EditStockLocation);
            this.enumUserRights.Add(StockLocation.DeleteStockLocation);
            this.enumUserRights.Add(StockLocation.ViewStockLocation);
            this.ApplyUserRights(uctbLocationView, enumUserRights, (int)Menus.StockLocation);
        }


        private string FetchBlackName()
        {
            ResultArgs resultArgs = new ResultArgs();
            string BlackName = string.Empty;
            try
            {
                using (LocationSystem LocationSystem = new LocationSystem())
                {
                    resultArgs = LocationSystem.FetchLocationById(LocationID);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        BlackName = resultArgs.DataSource.Table.Rows[0]["LOCATION"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
            return BlackName;
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvLocationView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvLocationView, colBlock);
            }
        }

        private void frmLocationsView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void frmLocationsView_EnterClicked(object sender, EventArgs e)
        {
            ShowLocationEditForm();
        }

        private void gvAssetItems_RowCountChanged(object sender, EventArgs e)
        {
            lblCount.Text = gvLocationView.RowCount.ToString();
        }

        #endregion

        #region Methods

        private void DeleteLocationDetails()
        {
            try
            {
                ResultArgs resultArgs = null;
                if (gvLocationView.RowCount > 0)
                {
                    if (this.LocationID > 0)
                    {
                        using (LocationSystem locationsystem = new LocationSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                using (AssetMappingSystem assetMappingSystem = new AssetMappingSystem())
                                {
                                    assetMappingSystem.LocationId = LocationID;
                                    resultArgs = assetMappingSystem.DeleteMapLocation();
                                    if (resultArgs.Success)
                                    {
                                        resultArgs = locationsystem.DeleteLocationDetails(LocationID);
                                    }
                                    if (resultArgs.Success)
                                    {

                                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                        LoadAssetLocationDetails();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NOSELECTION_FOR_DELETE));
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally
            { }
        }

        private void ShowLocationEditForm()
        {
            if (this.isEditable)
            {
                if (gvLocationView.RowCount > 0)
                {
                    if (this.LocationID != 0)
                    {
                        ShowLocationDetails(FormMode.Edit, SelectedProjectIds);
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NOSELECTION_FOR_EDIT));
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_USER_RIGHTS));
            }

        }

        private void ShowLocationDetails(FormMode FrmMode, string ProjectIds)
        {
            try
            {
                frmLocationsAdd Location = new frmLocationsAdd(this.LocationID, FrmMode, FrmModule, ProjectIds);
                Location.UpdateHeld += new EventHandler(OnUpdateHeld);
                TempLocationId = Location.LocationID;
                Location.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            { }
        }

        private void RefreshTreeView()
        {
            LoadAssetLocationDetails();
        }

        private void SetTitle()
        {
            this.Text = this.GetMessage(MessageCatalog.Asset.Location.LOCATION_VIEW_CAPTION);
        }

        private void LoadAssetLocationDetails()
        {
            try
            {
                using (LocationSystem LocationSystem = new LocationSystem())
                {
                    ResultArgs resultArgs = null;
                    gcLocationView.DataSource = null;
                    LocationSystem.Module = FrmModule;
                    resultArgs = LocationSystem.FetchLocationDetails();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtLocation = resultArgs.DataSource.Table;
                        gcLocationView.DataSource = dtLocation;
                    }
                    else
                    {
                        gcLocationView.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally
            { }
        }
        #endregion

        private void uctbLocationView_DownloadExcel(object sender, EventArgs e)
        {
            using (frmExcelSupport excelSupport = new frmExcelSupport("Location", MasterImport.Location, module))
            {
                excelSupport.UpdateHeld += new EventHandler(OnUpdateHeld);
                excelSupport.ShowDialog();
            }

        }

        private void frmLocationsView_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(true, true);
            LoadAssetLocationDetails();
            SetTitle();
        }

        private void gvLocationView_RowCountChanged(object sender, EventArgs e)
        {
            lblCount.Text = gvLocationView.DataRowCount.ToString();
        }

        private void gcLocationView_DoubleClick(object sender, EventArgs e)
        {
            ShowLocationEditForm();
        }
    }
}
