using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using System.Data;
using Bosco.Utility;

namespace Bosco.Model
{
    public class StockPurchaseSalesSystem : SystemBase
    {
        #region Varaible Declaration
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Properties
        public int ItemId { get; set; }
        #endregion

        #region Methods
        public ResultArgs FetchStockItemDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockPurchaseSales.FetchItem))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchStockItemLocationDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockPurchaseSales.FetchLocations))
            {
                if (ItemId > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.StockItem.ITEM_IDColumn, ItemId);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchUnitOfMeasureByItemId(int ItemID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockPurchaseSales.FetchUnitofMeasurebyItem))
            {
                dataManager.Parameters.Add(this.AppSchema.StockItem.ITEM_IDColumn, ItemID);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }


        public ResultArgs FetchstockDashboardDetails(int ProjectId, int LocationID, int ItemID,string debaldate)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockPurchaseSales.FetchDashboardDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.StockBalance.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_FROMColumn, DateSet.ToDate(this.YearFrom, false));
                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_TOColumn, DateSet.ToDate(debaldate, false));
                if (LocationID > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.StockBalance.LOCATION_IDColumn, LocationID);
                }
                if (ItemID > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.StockBalance.ITEM_IDColumn, ItemID);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchReorderLevel(int ProjectId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockPurchaseSales.FetchReorderLevel))
            {
                dataManager.Parameters.Add(this.AppSchema.StockBalance.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_FROMColumn, DateSet.ToDate(this.YearFrom, false));
                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_TOColumn, DateSet.ToDate(this.YearTo, false));
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs AutoNameAddress()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockMasterPurchase.FetchNameAddress))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs AutoNarration()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockMasterPurchase.AutoFetchNarration))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        #endregion
    }
}
