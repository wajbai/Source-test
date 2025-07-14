using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using System.Data;

namespace Bosco.Model
{
    public class LocationSystem : SystemBase
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
        public int CustodianId { get; set; }
        public int LocationId { get; set; }
        public int ImageId { get; set; }
        public string LocationIds { get; set; }
        public int AssetId { get; set; }
        public DateTime ResponsibleDate { get; set; }
        public DateTime ResponsibleToDate { get; set; }
        public int LocationType { get; set; }
        public FinanceModule Module { get; set; }
        public int ProjectId { get; set; }
        public FormMode FrMode { get; set; }
        public string MultipleProjectIds { get; set; }

        #endregion

        #region Location Methods

        public ResultArgs FetchLocationDetails()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetLocation.FetchAll))
            {
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchLocationDetailsByItem()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetLocation.FetchLocationByItem))
            {
                resultArgs = datamanager.FetchData(DataSource.DataTable);
                Name = resultArgs.DataSource.Table.Rows[0][AppSchema.ASSETLocationDetails.LOCATIONColumn.ColumnName].ToString();
                BlockId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName].ToString());
            }
            return resultArgs;
        }

        public ResultArgs SaveLocationDetails(int LocationId)
        {
            using (DataManager datamanager = new DataManager((this.LocationId == 0) ? SQLCommand.AssetLocation.Add : SQLCommand.AssetLocation.Update))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATION_IDColumn, LocationId, true);
                datamanager.Parameters.Add(this.AppSchema.Block.BLOCK_IDColumn, BlockId);
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.RESPONSIBLE_FROMColumn, ResponsibleDate);
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATIONColumn, Name);
                datamanager.Parameters.Add(this.AppSchema.AssetCustodians.CUSTODIAN_IDColumn, CustodianId);
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATION_TYPEColumn, LocationType);
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.RESPONSIBLE_TOColumn, ResponsibleToDate);
                resultArgs = datamanager.UpdateData();
                datamanager.Dispose();
            }
            return resultArgs;
        }
        public ResultArgs SaveAssetMapLocation()
        {
            string[] projects = MultipleProjectIds.Split(',');
            foreach (string project in projects)
            {
                using (DataManager datamanager = new DataManager(SQLCommand.AssetMapping.MapLocation))
                {
                    datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATION_IDColumn, LocationId, true);
                    datamanager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, project);
                    resultArgs = datamanager.UpdateData();
                    datamanager.Dispose();
                }
                if (!resultArgs.Success) { break; }
            }
            return resultArgs;
        }
        public void FillLocationProperties(int LocationId)
        {
            resultArgs = FetchLocationById(LocationId);
            if (resultArgs.DataSource.Table != null && resultArgs.Success)
            {
                Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETLocationDetails.LOCATIONColumn.ColumnName].ToString();
                BlockId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Block.BLOCK_IDColumn.ColumnName].ToString());
                CustodianId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetCustodians.CUSTODIAN_IDColumn.ColumnName].ToString());
                ResponsibleDate = this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETLocationDetails.RESPONSIBLE_FROMColumn.ColumnName].ToString(), false);
                ResponsibleToDate = this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETLocationDetails.RESPONSIBLE_TOColumn.ColumnName].ToString(), false);
                LocationType = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETLocationDetails.LOCATION_TYPEColumn.ColumnName].ToString());
                this.LocationId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName].ToString());
                ProjectId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString());
                MultipleProjectIds = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.PROJECT_IDColumn.ColumnName].ToString();
            }
        }

        public ResultArgs DeleteLocationDetails(int locationId)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetLocation.Delete))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATION_IDColumn, locationId);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchLocationById(int LocationId)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetLocation.Fetch))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATION_IDColumn, LocationId);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchLocaitonByAssetId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetLocation.FetchLocationByItemId))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        public ResultArgs FetchSelectedLocationDetails()
        {
            if (Module.Equals(FinanceModule.Asset))
            {
                using (DataManager datamanager = new DataManager(SQLCommand.AssetLocation.FetchSelectedLocationDetails))
                {
                    datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATION_IDColumn, this.LocationIds);
                    datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = datamanager.FetchData(DataSource.DataTable);
                }
            }
            else
            {
                using (DataManager datamanager = new DataManager(SQLCommand.AssetLocation.FetchStockLocation))
                {
                    datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATION_IDColumn, this.LocationIds);
                    datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = datamanager.FetchData(DataSource.DataTable);
                }
            }
            return resultArgs;
        }

        public int FetchLocationNameByID()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetLocation.FetchAssetLocationNameByID))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATIONColumn, Name);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs DeleteLocationDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetLocation.DeleteLocationDetails))
            {
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchBlockDetails()
        {
            ResultArgs resultArgs = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.AssetLocation.FetchBlockDetails))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }
        /// <summary>
        /// aldrin
        /// Validate the block. Wether block has the location already.
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchValidateLocationDetails()
        {
            ResultArgs resultArgs = new ResultArgs();
            // Validate the locations while adding.
            using (DataManager dataManager = new DataManager(this.FrMode == (int)FormMode.Add ? SQLCommand.AssetLocation.FetchValidateLocation : SQLCommand.AssetLocation.FetchEditValidateLocation))
            {
                dataManager.Parameters.Add(this.AppSchema.Block.BLOCK_IDColumn, BlockId);
                dataManager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATIONColumn, Name);
                dataManager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATION_IDColumn, this.LocationId);
                dataManager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATION_TYPEColumn, this.LocationType);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success && resultArgs != null)
                {
                    foreach (DataRow name in resultArgs.DataSource.Table.Rows)
                    {
                        if (name[this.AppSchema.ASSETLocationDetails.LOCATIONColumn.ColumnName].ToString().Equals(this.Name))
                        {
                            resultArgs.Success = false;
                        }
                    }
                }

            }
            return resultArgs;
        }
        public ResultArgs FetchLocationByProject()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetLocation.FetchProjectLocationProjectId))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn.ColumnName, ProjectId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchProjectLocationByProjectId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetLocation.FetchProjectLocationProjectId))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion
    }
}
