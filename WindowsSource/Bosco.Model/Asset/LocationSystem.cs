using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.Model.Asset
{
    public class LocationSystem: SystemBase
    {
        #region Variable Decelaration
        ResultArgs resultArgs = null;

        #endregion

        #region constructor
        public LocationSystem()
        {
        }
        #endregion

        #region Location Properties
        public int AreaId { get; set; }
        public int BuildingId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public int RoomId { get; set; }
        public string Name { get; set; }

        #endregion

        #region Area Methods
        public ResultArgs FetchAreaAll()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetLocation.AreaFetchAll))
            {
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveAreaDetails()
        {
            using (DataManager datamanager = new DataManager((AreaId == 0) ? SQLCommand.AssetLocation.AreaAdd : SQLCommand.AssetLocation.AreaUpdate))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.AREA_IDColumn, AreaId);
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.NAMEColumn, Name);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        //public void FillDepreciationProperties(int DepreciationId)
        //{
        //    resultArgs = FetchById(DepreciationId);
        //    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
        //    {
        //        Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETDepreciationDetails.NAMEColumn.ColumnName].ToString();
        //        Description = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETDepreciationDetails.DESCRIPTIONColumn.ColumnName].ToString();
        //    }
        //}

        public ResultArgs DeleteArea()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetLocation.AreaDelete))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.AREA_IDColumn, AreaId);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchAreaById()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetLocation.AreaFetch))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.AREA_IDColumn, AreaId);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion

        #region Building Methods
        public ResultArgs FetchBuildingAll()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetLocation.BuildingFetchAll))
            {
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveBuildingDetails()
        {
            using (DataManager datamanager = new DataManager((BuildingId == 0) ? SQLCommand.AssetLocation.BuildingAdd: SQLCommand.AssetLocation.BuildingUpdate))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.BUILDING_IDColumn, BuildingId);
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.AREA_IDColumn, AreaId);
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.NAMEColumn, Name);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        //public void FillDepreciationProperties(int DepreciationId)
        //{
        //    resultArgs = FetchById(DepreciationId);
        //    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
        //    {
        //        Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETDepreciationDetails.NAMEColumn.ColumnName].ToString();
        //        Description = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETDepreciationDetails.DESCRIPTIONColumn.ColumnName].ToString();
        //    }
        //}

        public ResultArgs DeleteBuilding()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetLocation.BuildingDelete))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.BUILDING_IDColumn, BuildingId);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchBuildingById()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetLocation.BuildingFetch))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.BUILDING_IDColumn, BuildingId);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion

        #region Block Methods
        public ResultArgs FetchBlockAll()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetLocation.BlockFetchAll))
            {
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveBlockDetails()
        {
            using (DataManager datamanager = new DataManager((BlockId== 0) ? SQLCommand.AssetLocation.BlockAdd: SQLCommand.AssetLocation.BlockUpdate))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.BLOCK_IDColumn, BlockId);
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.BUILDING_IDColumn, BuildingId);
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.NAMEColumn, Name);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        //public void FillDepreciationProperties(int DepreciationId)
        //{
        //    resultArgs = FetchById(DepreciationId);
        //    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
        //    {
        //        Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETDepreciationDetails.NAMEColumn.ColumnName].ToString();
        //        Description = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETDepreciationDetails.DESCRIPTIONColumn.ColumnName].ToString();
        //    }
        //}

        public ResultArgs DeleteBlock()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetLocation.BlockDelete))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.BLOCK_IDColumn, BlockId);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchBlockById()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetLocation.BlockFetch))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.BLOCK_IDColumn, BlockId);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchFloorByBlock()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetLocation.FloorByBlock))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.BLOCK_IDColumn, BlockId);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion

        #region Floor Methods
        public ResultArgs FetchFloorAll()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetLocation.FloorFetchAll))
            {
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveFloorDetails()
        {
            using (DataManager datamanager = new DataManager((FloorId== 0) ? SQLCommand.AssetLocation.FloorAdd: SQLCommand.AssetLocation.FloorUpdate))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.FLOOR_IDColumn, FloorId);
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.BLOCK_IDColumn, BlockId);
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.NAMEColumn, Name);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        //public void FillDepreciationProperties(int DepreciationId)
        //{
        //    resultArgs = FetchById(DepreciationId);
        //    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
        //    {
        //        Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETDepreciationDetails.NAMEColumn.ColumnName].ToString();
        //        Description = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETDepreciationDetails.DESCRIPTIONColumn.ColumnName].ToString();
        //    }
        //}

        public ResultArgs DeleteFloor()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetLocation.FloorDelete))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.FLOOR_IDColumn, FloorId);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchFloorById()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetLocation.FloorFetch))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.FLOOR_IDColumn, FloorId);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion

        #region Room Methods
        public ResultArgs FetchRoomAll()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetLocation.RoomFetchAll))
            {
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveRoomDetails()
        {
            using (DataManager datamanager = new DataManager((RoomId == 0) ? SQLCommand.AssetLocation.RoomAdd: SQLCommand.AssetLocation.RoomUpdate))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.ROOM_IDColumn, RoomId);
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.BLOCK_IDColumn, BlockId);
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.FLOOR_IDColumn, FloorId);
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.NAMEColumn, Name);

                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        //public void FillDepreciationProperties(int DepreciationId)
        //{
        //    resultArgs = FetchById(DepreciationId);
        //    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
        //    {
        //        Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETDepreciationDetails.NAMEColumn.ColumnName].ToString();
        //        Description = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETDepreciationDetails.DESCRIPTIONColumn.ColumnName].ToString();
        //    }
        //}

        public ResultArgs DeleteRoom()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetLocation.RoomDelete))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.ROOM_IDColumn, RoomId);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchRoomById()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetLocation.RoomFetch))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.ROOM_IDColumn, RoomId);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion
    }
}
