using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using System.Data;

namespace Bosco.Model.Inventory.Asset
{
    public class AssetMappingSystem : SystemBase
    {
        #region Properties
        ResultArgs resultArgs = null;
        public int LocationId { get; set; }
        public int ProjectId { get; set; }
        public DataTable dtLocations { get; set; }
        #endregion

        #region Public Methods
        public ResultArgs FetchBlockLocation()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetLedgerMapping.FetchLocation))
            {
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs GetMappedProjectLocaion()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetLedgerMapping.GetMappedProjectLocation))
            {
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs MapProjectLocation()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = DeleteMapping();
                if (resultArgs != null && resultArgs.Success)
                {
                    resultArgs = MapLocation();
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }
        #endregion

        #region Private Methods
        private ResultArgs MapLocation()
        {
            if (dtLocations != null)
            {
                foreach (DataRow drItem in dtLocations.Rows)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.AssetMapping.MapLocation))
                    {
                        dataManager.Parameters.Add(AppSchema.ASSETLocationDetails.LOCATION_IDColumn, NumberSet.ToInteger(drItem[AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName].ToString()));
                        dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                        resultArgs = dataManager.UpdateData();
                        if (!resultArgs.Success) break;
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs DeleteMapping()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetMapping.DeleteMapping))
            {
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(AppSchema.StockLocation.LOCATION_IDColumn, LocationId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        public ResultArgs DeleteMapLocation()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetMapping.DeleteMapLocation))
            {
                dataManager.Parameters.Add(AppSchema.ASSETLocationDetails.LOCATION_IDColumn, LocationId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        #endregion
    }
}
