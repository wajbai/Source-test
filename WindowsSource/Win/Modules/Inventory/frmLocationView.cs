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

namespace ACPP.Modules.Inventory
{
    public partial class frmLocationAdd : frmBaseAdd
    {
        #region Variables
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Constructor
        public frmLocationAdd()
        {
            InitializeComponent();
        }
        #endregion

        #region Area
        #region Area Properties
        private int areaId = 0;
        public int AreaId
        {
            get
            {
                areaId = gvArea.GetFocusedRowCellValue(colAreaId) != null ? this.UtilityMember.NumberSet.ToInteger(gvArea.GetFocusedRowCellValue(colAreaId).ToString()) : 0;
                return areaId;
            }
            set
            {
                areaId = value;
            }
        }
        #endregion

        #region Area Events
        private void ucArea_AddClicked(object sender, EventArgs e)
        {
            ShowAreaForm((int)AddNewRow.NewRow);
        }

        private void ucArea_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucArea_DeleteClicked(object sender, EventArgs e)
        {
            DeleteArea();
        }

        private void gvArea_DoubleClick(object sender, EventArgs e)
        {
            ShowAreaEditForm();
        }

        private void ucArea_EditClicked(object sender, EventArgs e)
        {
            ShowAreaEditForm();
        }

        private void ucArea_RefreshClicked(object sender, EventArgs e)
        {
            LoadAreaDetails();
        }

        private void ucArea_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcArea, this.GetMessage(MessageCatalog.Asset.Location.LOCATION_PRINT_CAPTION), PrintType.DT, gvArea, true);
        }
        #endregion

        #region Area Methods
        private void ShowAreaEditForm()
        {
            if (gvArea.RowCount != 0)
            {
                if (AreaId != 0)
                {
                    ShowAreaForm(AreaId);
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }

        }

        private void ShowAreaForm(int areaId)
        {
            try
            {
                frmLocationArea frmLocation = new frmLocationArea(areaId);
                frmLocation.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmLocation.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void DeleteArea()
        {
            try
            {
                ResultArgs resultArgs = null;
                if (gvArea.RowCount != 0)
                {
                    if (AreaId != 0)
                    {
                        using (LocationSystem location = new LocationSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                location.AreaId = AreaId;
                                resultArgs = location.DeleteArea();
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    LoadAreaDetails();
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

        private void LoadAreaDetails()
        {
            using (LocationSystem location = new LocationSystem())
            {
                resultArgs = location.FetchAreaAll();
                if (resultArgs != null && resultArgs.Success)
                {
                    gcArea.DataSource = resultArgs.DataSource.Table;
                    gcArea.RefreshDataSource();
                    // this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpBuildingArea, resultArgs.DataSource.Table, location.AppSchema.ASSETLocationDetails.AREA_NAMEColumn.ColumnName,
                    //     location.AppSchema.ASSETLocationDetails.AREA_IDColumn.ColumnName);
                }
            }
        }
        #endregion
        #endregion

        #region Building
        #region Building Properties
        private int buildingId = 0;
        public int BuildingId
        {
            get
            {
                buildingId = gvBuilding.GetFocusedRowCellValue(colBuildingId) != null ? this.UtilityMember.NumberSet.ToInteger(gvBuilding.GetFocusedRowCellValue(colBuildingId).ToString()) : 0;
                return buildingId;
            }
            set
            {
                buildingId = value;
            }
        }
        #endregion

        #region Building Events
        private void ucBuilding_AddClicked(object sender, EventArgs e)
        {
            ShowBuildingForm((int)AddNewRow.NewRow);
        }

        private void ucBuilding_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucBuilding_DeleteClicked(object sender, EventArgs e)
        {
            DeleteBuilding();
        }
        
        private void gvBuilding_DoubleClick(object sender, EventArgs e)
        {
            ShowBuildingEditForm();
        }

        private void ucBuilding_EditClicked(object sender, EventArgs e)
        {
            ShowBuildingEditForm();
        }

        private void ucBuilding_PrintClicked(object sender, EventArgs e)
        {

        }

        private void ucBuilding_RefreshClicked(object sender, EventArgs e)
        {
            LoadBuildingDetails();
        }
        #endregion

        #region Building Methods
        private void ShowBuildingEditForm()
        {
            if (gvBuilding.RowCount != 0)
            {
                if (BuildingId != 0)
                {
                    ShowBuildingForm(BuildingId);
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }

        }

        private void ShowBuildingForm(int buildingId)
        {
            try
            {
                frmLocationBuilding frmBuilding = new frmLocationBuilding(buildingId);
                frmBuilding.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmBuilding.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void DeleteBuilding()
        {
            try
            {
                ResultArgs resultArgs = null;
                if (gvBuilding.RowCount != 0)
                {
                    if (BuildingId != 0)
                    {
                        using (LocationSystem location = new LocationSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                location.BuildingId = BuildingId;
                                resultArgs = location.DeleteBuilding();
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    LoadBuildingDetails();
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

        private void LoadBuildingDetails()
        {
            using (LocationSystem location = new LocationSystem())
            {
                resultArgs = location.FetchBuildingAll();
                if (resultArgs != null && resultArgs.Success)
                {
                    gcBuilding.DataSource = resultArgs.DataSource.Table;
                    gcBuilding.RefreshDataSource();
                    //this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpBlockBuilding, resultArgs.DataSource.Table, location.AppSchema.ASSETLocationDetails.BUILDING_NAMEColumn.ColumnName,
                    //    location.AppSchema.ASSETLocationDetails.BUILDING_IDColumn.ColumnName);
                }
            }
        }
        #endregion
        #endregion

        #region Block
        #region Block Properties
        private int blockId = 0;
        public int BlockId
        {
            get
            {
                blockId = gvBlock.GetFocusedRowCellValue(colBlockId) != null ? this.UtilityMember.NumberSet.ToInteger(gvBlock.GetFocusedRowCellValue(colBlockId).ToString()) : 0;
                return blockId;
            }
            set
            {
                blockId = value;
            }
        }
        #endregion

        #region Block Events
        private void ucBlock_AddClicked(object sender, EventArgs e)
        {
            ShowBlockForm((int)AddNewRow.NewRow);
        }

        private void ucBlock_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucBlock_DeleteClicked(object sender, EventArgs e)
        {
            DeleteBlock();
        }

        private void gvBlock_DoubleClick(object sender, EventArgs e)
        {
            ShowBlockEditForm();
        }

        private void ucBlock_EditClicked(object sender, EventArgs e)
        {
            ShowBlockEditForm();
        }

        private void ucBlock_PrintClicked(object sender, EventArgs e)
        {

        }

        private void ucBlock_RefreshClicked(object sender, EventArgs e)
        {
            LoadBlockDetails();
        }
        #endregion

        #region Block Methods
        private void ShowBlockEditForm()
        {
            if (gvBlock.RowCount != 0)
            {
                if (BlockId != 0)
                {
                    ShowBlockForm(BlockId);
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }

        }

        private void ShowBlockForm(int blockId)
        {
            try
            {
                frmLocationBlock frmBlock = new frmLocationBlock(blockId);
                frmBlock.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmBlock.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void DeleteBlock()
        {
            try
            {
                ResultArgs resultArgs = null;
                if (gvBlock.RowCount != 0)
                {
                    if (BlockId != 0)
                    {
                        using (LocationSystem location = new LocationSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                location.BlockId = BlockId;
                                resultArgs = location.DeleteBlock();
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    LoadBlockDetails();
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

        private void LoadBlockDetails()
        {
            using (LocationSystem location = new LocationSystem())
            {
                resultArgs = location.FetchBlockAll();
                if (resultArgs != null && resultArgs.Success)
                {
                    gcBlock.DataSource = resultArgs.DataSource.Table;
                    gcBlock.RefreshDataSource();
                    //this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpFloorBlock, resultArgs.DataSource.Table, location.AppSchema.ASSETLocationDetails.BLOCK_NAMEColumn.ColumnName,
                    //    location.AppSchema.ASSETLocationDetails.BLOCK_IDColumn.ColumnName);
                }
            }
        }
        #endregion
        #endregion

        #region Floor
        #region Floor Properties
        private int floorId = 0;
        public int FloorId
        {
            get
            {
                floorId = gvFloor.GetFocusedRowCellValue(colFloorId) != null ? this.UtilityMember.NumberSet.ToInteger(gvFloor.GetFocusedRowCellValue(colFloorId).ToString()) : 0;
                return floorId;
            }
            set
            {
                floorId = value;
            }
        }
        #endregion

        #region Floor Events
        private void ucFloor_AddClicked(object sender, EventArgs e)
        {
            ShowFloorForm((int)AddNewRow.NewRow);
        }

        private void ucFloor_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucFloor_DeleteClicked(object sender, EventArgs e)
        {
            DeleteFloor();
        }

        private void gvFloor_DoubleClick(object sender, EventArgs e)
        {
            ShowFloorEditForm();
        }

        private void ucFloor_EditClicked(object sender, EventArgs e)
        {
            ShowFloorEditForm();
        }

        private void ucFloor_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcFloor, this.GetMessage(MessageCatalog.Asset.Location.LOCATION_PRINT_CAPTION), PrintType.DT, gvFloor, true);
        }

        private void ucFloor_RefreshClicked(object sender, EventArgs e)
        {
            LoadFloorDetails();
        }
        #endregion

        #region Floor Methods
        private void ShowFloorEditForm()
        {
            if (gvFloor.RowCount != 0)
            {
                if (FloorId != 0)
                {
                    ShowFloorForm(FloorId);
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }

        }

        private void ShowFloorForm(int floorId)
        {
            try
            {
                frmLocationFloor frmFloor = new frmLocationFloor(floorId);
                frmFloor.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmFloor.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void DeleteFloor()
        {
            try
            {
                ResultArgs resultArgs = null;
                if (gvFloor.RowCount != 0)
                {
                    if (FloorId != 0)
                    {
                        using (LocationSystem location = new LocationSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                location.FloorId = FloorId;
                                resultArgs = location.DeleteFloor();
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    LoadFloorDetails();
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

        private void LoadFloorDetails()
        {
            using (LocationSystem location = new LocationSystem())
            {
                resultArgs = location.FetchFloorAll();
                if (resultArgs != null && resultArgs.Success)
                {
                    gcFloor.DataSource = resultArgs.DataSource.Table;
                    gcFloor.RefreshDataSource();
                    //this.UtilityMember.ComboSet.BindGridLookUpCombo(glk, resultArgs.DataSource.Table, location.AppSchema.ASSETLocationDetails.NAMEColumn.ColumnName,
                    //    location.AppSchema.ASSETLocationDetails.BLOCK_IDColumn.ColumnName);
                }
            }
        }
        #endregion
        #endregion

        #region Room
        #region Room Properties
        private int roomId = 0;
        public int RoomId
        {
            get
            {
                roomId = gvRoom.GetFocusedRowCellValue(colRoomId) != null ? this.UtilityMember.NumberSet.ToInteger(gvRoom.GetFocusedRowCellValue(colRoomId).ToString()) : 0;
                return roomId;
            }
            set
            {
                roomId = value;
            }
        }
        #endregion

        #region Room Events
        private void ucRoom_AddClicked(object sender, EventArgs e)
        {
            ShowRoomForm((int)AddNewRow.NewRow);
        }

        private void ucRoom_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucRoom_DeleteClicked(object sender, EventArgs e)
        {
            DeleteRoom();
        }

        private void gvRoom_DoubleClick(object sender, EventArgs e)
        {
            ShowRoomEditForm();
        }

        private void ucRoom_EditClicked(object sender, EventArgs e)
        {
            ShowRoomEditForm();
        }

        private void ucRoom_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcRoom, this.GetMessage(MessageCatalog.Asset.Location.LOCATION_PRINT_CAPTION), PrintType.DT, gvRoom, true);
        }

        private void ucRoom_RefreshClicked(object sender, EventArgs e)
        {
            LoadRoomDetails();
        }
        #endregion

        #region Room Methods
        private void ShowRoomEditForm()
        {
            if (gvRoom.RowCount != 0)
            {
                if (RoomId != 0)
                {
                    ShowRoomForm(RoomId);
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }

        }

        private void ShowRoomForm(int roomId)
        {
            try
            {
                frmLocationRoomNo frmRoom = new frmLocationRoomNo(roomId);
                frmRoom.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmRoom.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void DeleteRoom()
        {
            try
            {
                ResultArgs resultArgs = null;
                if (gvRoom.RowCount != 0)
                {
                    if (RoomId != 0)
                    {
                        using (LocationSystem location = new LocationSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                location.RoomId = RoomId;
                                resultArgs = location.DeleteRoom();
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    LoadRoomDetails();
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

        private void LoadRoomDetails()
        {
            using (LocationSystem location = new LocationSystem())
            {
                resultArgs = location.FetchRoomAll();
                if (resultArgs != null && resultArgs.Success)
                {
                    gcRoom.DataSource = resultArgs.DataSource.Table;
                    gcRoom.RefreshDataSource();
                    //this.UtilityMember.ComboSet.BindGridLookUpCombo(glk, resultArgs.DataSource.Table, location.AppSchema.ASSETLocationDetails.NAMEColumn.ColumnName,
                    //    location.AppSchema.ASSETLocationDetails.BLOCK_IDColumn.ColumnName);
                }
            }
        }
        #endregion
        #endregion

        #region Common
        private void frmLocationAdd_Load(object sender, EventArgs e)
        {
            LoadAreaDetails();
        }

        private void xrtLocation_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xrtLocation.SelectedTabPage.Text.Equals(xtpArea.Text))
            {
                LoadAreaDetails();
            }
            else if (xrtLocation.SelectedTabPage.Text.Equals(xrtBuilding.Text))
            {
                LoadBuildingDetails();
            }
            else if (xrtLocation.SelectedTabPage.Text.Equals(xrtBlock.Text))
            {
                LoadBlockDetails();
            }
            else if (xrtLocation.SelectedTabPage.Text.Equals(xrtFloor.Text))
            {
                LoadFloorDetails();
            }
            else if (xrtLocation.SelectedTabPage.Text.Equals(xrtRoomNo.Text))
            {
                LoadRoomDetails();
            }
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            if (xrtLocation.SelectedTabPage.Text.Equals(xtpArea.Text))
            {
                LoadAreaDetails();
            }
            else if (xrtLocation.SelectedTabPage.Text.Equals(xrtBuilding.Text))
            {
                LoadBuildingDetails();
            }
            else if (xrtLocation.SelectedTabPage.Text.Equals(xrtBlock.Text))
            {
                LoadBlockDetails();
            }
            else if (xrtLocation.SelectedTabPage.Text.Equals(xrtFloor.Text))
            {
                LoadFloorDetails();
            }
            else if (xrtLocation.SelectedTabPage.Text.Equals(xrtRoomNo.Text))
            {
                LoadRoomDetails();
            }
        }
        #endregion
    }
}