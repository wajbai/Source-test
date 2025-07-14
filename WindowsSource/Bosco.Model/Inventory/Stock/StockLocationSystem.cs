using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.Model.Inventory.Stock
{
    public class StockLocationSystem : SystemBase
    {
        #region Variable Decelaration
        ResultArgs resultArgs = null;

        #endregion

        #region constructor
        public StockLocationSystem()
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
        public int LocationType { get; set; }
        public FinanceModule Module { get; set; }

        #endregion

        #region Location Methods

        public ResultArgs FetchLocationDetails()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.StockLocation.FetchAll))
            {
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchLocationDetailsByItem()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.StockLocation.FetchLocationByItem))
            {
                resultArgs = datamanager.FetchData(DataSource.DataTable);
                Name = resultArgs.DataSource.Table.Rows[0][AppSchema.ASSETLocationDetails.LOCATIONColumn.ColumnName].ToString();
                BlockId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName].ToString());
            }
            return resultArgs;
        }

        public ResultArgs SaveLocationDetails()
        {
            using (DataManager datamanager = new DataManager((this.LocationId == 0) ? SQLCommand.StockLocation.Add : SQLCommand.StockLocation.Update))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATION_IDColumn, LocationId);
                //datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.PARENT_LOCATION_IDColumn, BlockId);
                datamanager.Parameters.Add(this.AppSchema.LedgerGroup.IMAGE_IDColumn, ImageId);
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATIONColumn, Name);
                datamanager.Parameters.Add(this.AppSchema.AssetCustodians.CUSTODIAN_IDColumn, CustodianId);
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATION_TYPEColumn, LocationType);
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.RESPONSIBLE_FROMColumn, ResponsibleDate);
                resultArgs = datamanager.UpdateData();
                datamanager.Dispose();
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
                LocationType = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETLocationDetails.LOCATION_TYPEColumn.ColumnName].ToString());
            }
        }

        public ResultArgs DeleteLocationDetails(int locationId)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.StockLocation.Delete))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATION_IDColumn, locationId);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchLocationById(int LocationId)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.StockLocation.Fetch))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATION_IDColumn, LocationId);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchLocaitonByAssetId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockLocation.FetchLocationByItemId))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        public ResultArgs FetchSelectedLocationDetails()
        {
            if (Module.Equals(FinanceModule.Asset))
            {
                using (DataManager datamanager = new DataManager(SQLCommand.StockLocation.FetchSelectedLocationDetails))
                {
                    datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATION_IDColumn, this.LocationIds);
                    datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = datamanager.FetchData(DataSource.DataTable);
                }
            }
            else
            {
                using (DataManager datamanager = new DataManager(SQLCommand.StockLocation.FetchStockLocation))
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
            using (DataManager dataManager = new DataManager(SQLCommand.StockLocation.FetchAssetLocationNameByID))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATIONColumn, Name);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs DeleteLocationDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockLocation.DeleteLocationDetails))
            {
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs ValidateLocationId(int LocationId)
        {
            ResultArgs resultArgs = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.StockLocation.FetchValidateLocation))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, LocationId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        public ResultArgs FetchBlockDetails()
        {
            ResultArgs resultArgs = new ResultArgs();
            //using (DataManager dataManager = new DataManager(SQLCommand.StockLocation.FetchValidateLocation))
            //{
            //    dataManager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATION_IDColumn, LocationId);
            //    resultArgs = dataManager.FetchData(DataSource.DataTable);
            //}

            return resultArgs;
        }

        #endregion
    }
}
