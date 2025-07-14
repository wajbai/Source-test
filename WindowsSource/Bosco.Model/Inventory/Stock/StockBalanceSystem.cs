using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using System.Data;

namespace Bosco.Model.Inventory.Stock
{
    public class StockBalanceSystem : SystemBase
    {
        #region Declartion
        ResultArgs resultArgs = new ResultArgs();
        public int projectId = 0;
        private int itemid = 0;
        public int locationid = 0;
        private double quantity = 0;
        private decimal rate = 0;
        private DateTime balanceDate = DateTime.Now;
        public DataTable dtStockOPBalance { get; set; }
        #endregion

        #region Properties
        //public int ItemId { get; set; }
        //public int ProjectId { get; set; }
        //public int BranchId { get; set; }
        //public int LocationId { get; set; }
        //public int Quantity { get; set; }
        //public int Type { get; set; }
        //public string Balancedate { get; set; }
        #endregion

        #region Enum
        public enum BalanceType
        {
            OpeningBalance,
            ClosingBalance,
            CurrentBalance
        }
        #endregion

        #region Methods

        public ResultArgs UpdatestockBalance(int stockId, StockType stockType, TransactionAction transAction)
        {
            ResultArgs result = new ResultArgs();
            if (stockType.Equals(StockType.Sales))
            {
                result = FetchSalesdetailsbyId(stockId);
            }
            else if (stockType.Equals(StockType.Purchase))
            {
                result = FetchPurchasedetailsbyId(stockId);
            }
            else if (stockType.Equals(StockType.PurchaseReturns))
            {
                result = FetchPurchaseReturndetailsbyId(stockId);
            }

            if (result.Success)
            {
                DateTime transDate = DateTime.Now;
                DataTable dtTrans = result.DataSource.Table;
                if (dtTrans != null)
                {
                    foreach (DataRow drTrans in dtTrans.Rows)
                    {
                        transDate = DateSet.ToDate(drTrans[this.AppSchema.StockBalance.BALANCE_DATEColumn.ColumnName].ToString(), false);
                        projectId = NumberSet.ToInteger(drTrans[this.AppSchema.StockBalance.PROJECT_IDColumn.ColumnName].ToString());
                        itemid = NumberSet.ToInteger(drTrans[this.AppSchema.StockBalance.ITEM_IDColumn.ColumnName].ToString());
                        locationid = NumberSet.ToInteger(drTrans[this.AppSchema.StockBalance.LOCATION_IDColumn.ColumnName].ToString());
                        quantity = NumberSet.ToDouble(drTrans[this.AppSchema.StockBalance.QUANTITYColumn.ColumnName].ToString());
                        rate = NumberSet.ToDecimal(drTrans[this.AppSchema.StockSalesDetails.UNIT_PRICEColumn.ColumnName].ToString());

                        if (transAction == TransactionAction.EditBeforeSave || transAction == TransactionAction.Cancel)
                        {
                            quantity = -quantity;
                        }
                        result = UpdateStockDetails(projectId, transDate, locationid, itemid, quantity, rate, (int)stockType);
                        if (!result.Success) { break; }
                    }
                }
            }

            return result;
        }

        public ResultArgs FetchSalesdetailsbyId(int salesid)
        {
            using (StockSalesSystem salessystem = new StockSalesSystem())
            {
                resultArgs = salessystem.FetchitemsalesdetailsbyId(salesid);
            }
            return resultArgs;
        }



        public ResultArgs FetchPurchasedetailsbyId(int purchaseId)
        {
            using (StockPurchaseDetail stockPurchase = new StockPurchaseDetail())
            {
                stockPurchase.PurchaseId = purchaseId;
                resultArgs = stockPurchase.FetchStockDetailsBeforeDelete();
            }
            return resultArgs;
        }

        public ResultArgs FetchPurchaseReturndetailsbyId(int ReturnId)
        {
            using (StockPurcahseReturnSystem stockPurchase = new StockPurcahseReturnSystem())
            {
                stockPurchase.ReturnId = ReturnId;
                resultArgs = stockPurchase.FetchPurchaseReturnDetailsBeforeDelete();
            }
            return resultArgs;
        }

        /// <summary>
        /// Update Transaction Balance
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="Balancedate"></param>
        /// <param name="LocationId"></param>
        /// <param name="ItemId"></param>
        /// <param name="Quantity"></param>
        /// <param name="Rate"></param>
        /// <param name="stockType"></param>
        /// <returns></returns>
        public ResultArgs UpdateStockDetails(int ProjectId, DateTime Balancedate, int LocationId, int ItemId, double Quantity, decimal Rate, int stockType)
        {
            string transFlag = "TR";
            resultArgs = UpdateStockBalanceDetails(ProjectId, Balancedate, LocationId, ItemId, Quantity, Rate, (int)stockType, transFlag);
            return resultArgs;
        }

        public ResultArgs UpdateStockOpBalance()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                double tmpqty = 0;
                DateTime date = this.DateSet.ToDate(this.BookBeginFrom, false);
                DateTime OPDate = date.AddDays(-1);
                if (dtStockOPBalance != null && dtStockOPBalance.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtStockOPBalance.Rows)
                    {
                        itemid = this.NumberSet.ToInteger(dr[this.AppSchema.StockItem.ITEM_IDColumn.ColumnName].ToString());
                        quantity = this.NumberSet.ToDouble(dr[this.AppSchema.StockItem.QUANTITYColumn.ColumnName].ToString());
                        rate = this.NumberSet.ToDecimal(dr[this.AppSchema.StockItem.RATEColumn.ColumnName].ToString());

                        resultArgs = FetchStockOPDetails(projectId, locationid, itemid);
                        if (resultArgs.Success && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            tmpqty = NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0]["QUANTITY"].ToString());
                            resultArgs = UpdateStockOPDetails(projectId, OPDate, locationid, itemid, tmpqty, rate, (int)StockReturnType.OutWards);
                        }
                        resultArgs = UpdateStockOPDetails(projectId, OPDate, locationid, itemid, quantity, rate, (int)StockReturnType.InWards);
                    }
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        /// <summary>
        /// Update Opening Balance
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="Balancedate"></param>
        /// <param name="LocationId"></param>
        /// <param name="ItemId"></param>
        /// <param name="Quantity"></param>
        /// <param name="Rate"></param>
        /// <param name="stockType"></param>
        /// <returns></returns>
        public ResultArgs UpdateStockOPDetails(int ProjectId, DateTime Balancedate, int LocationId, int ItemId, double Quantity, decimal Rate, int stockType)
        {
            string transFlag = "OP";
            resultArgs = UpdateStockBalanceDetails(ProjectId, Balancedate, LocationId, ItemId, Quantity, Rate, stockType, transFlag);
            return resultArgs;
        }

        //public ResultArgs UpdateStockOpeningBalance(int ProjectId, int LocationId, TransactionAction transAction)
        //{
        //    using (DataManager dataManager = new DataManager(SQLCommand.StockUpdation.FetchStockOPBalance))
        //    {
        //        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
        //        dataManager.Parameters.Add(this.AppSchema.StockItem.QUANTITYColumn, LocationId);
        //        resultArgs = dataManager.FetchData(DataSource.DataTable);
        //        if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
        //            {
        //                projectId = this.NumberSet.ToInteger(dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString());
        //                balanceDate = this.DateSet.ToDate(dr[this.AppSchema.StockBalance.BALANCE_DATEColumn.ColumnName].ToString(), false);
        //                LocationId = this.NumberSet.ToInteger(dr[this.AppSchema.StockLocation.LOCATION_IDColumn.ColumnName].ToString());
        //                itemid = this.NumberSet.ToInteger(dr[this.AppSchema.StockItem.ITEM_IDColumn.ColumnName].ToString());
        //                quantity = this.NumberSet.ToDouble(dr[this.AppSchema.StockBalance.QUANTITYColumn.ColumnName].ToString());
        //                rate = this.NumberSet.ToDecimal(dr[this.AppSchema.StockItem.RATEColumn.ColumnName].ToString());
        //                if (transAction.Equals(TransactionAction.EditBeforeSave))
        //                {
        //                    quantity = -quantity;
        //                }
        //                resultArgs = UpdateStockOPDetails(projectId, balanceDate, locationid, itemid, quantity, rate, (int)StockReturnType.OutWards);
        //                if (!resultArgs.Success) { break; }
        //            }
        //        }
        //    }
        //    return resultArgs;
        //}

        public ResultArgs DeleteStockOPBalance()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockUpdation.DeleteOPBalance))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, projectId);
                dataManager.Parameters.Add(this.AppSchema.StockLocation.LOCATION_IDColumn, locationid);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs UpdateStockBalanceDetails(int ProjectId, DateTime Balancedate, int LocationId, int ItemId, double Quantity, decimal Rate, int stockType, string TransFlag)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.StockUpdation.UpdatestockDetails))
                {
                    dataManager.Parameters.Add(this.AppSchema.StockBalance.ITEM_IDColumn, ItemId);
                    dataManager.Parameters.Add(this.AppSchema.StockBalance.LOCATION_IDColumn, LocationId);
                    dataManager.Parameters.Add(this.AppSchema.StockBalance.QUANTITYColumn, Quantity);
                    dataManager.Parameters.Add(this.AppSchema.StockBalance.BALANCE_DATEColumn, Balancedate);
                    dataManager.Parameters.Add(this.AppSchema.StockBalance.STOCK_TYPEColumn, stockType);
                    dataManager.Parameters.Add(this.AppSchema.StockBalance.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.StockItem.RATEColumn, Rate);
                    dataManager.Parameters.Add(this.AppSchema.LedgerBalance.TRANS_FLAGColumn, TransFlag);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs GetOPBalance(int ProjectID, int itemID, int locationID, string Balancedate)
        {
            try
            {
                resultArgs = GetStockBalance(ProjectID, itemID, locationID, Balancedate, BalanceType.OpeningBalance);
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs GetCurrentBalance(int ProjectID, int itemID, int locationID)
        {
            try
            {
                resultArgs = GetStockBalance(ProjectID, itemID, locationID, "", BalanceType.CurrentBalance);
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs GetClosingBalance(int ProjectID, int itemID, int locationID, string Balancedate)
        {
            try
            {
                resultArgs = GetStockBalance(ProjectID, itemID, locationID, Balancedate, BalanceType.ClosingBalance);
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        private ResultArgs GetStockBalance(int ProjectID, int itemID, int locationID, string Balancedate, BalanceType stocktype)
        {
            try
            {
                if (stocktype == BalanceType.OpeningBalance)
                {
                    DateTime dateBal = DateSet.ToDate(Balancedate, false).AddDays(-1);
                    Balancedate = dateBal.ToShortDateString();
                }
                else if (stocktype == BalanceType.ClosingBalance)
                {
                    DateTime dateBal = DateSet.ToDate(Balancedate, false);
                    Balancedate = dateBal.ToShortDateString();
                }
                else if (stocktype == BalanceType.CurrentBalance)
                {
                    Balancedate = "";
                }
                using (DataManager dataManager = new DataManager(SQLCommand.StockUpdation.FetchStockBalance))
                {
                    if (!string.IsNullOrEmpty(Balancedate))
                    {
                        dataManager.Parameters.Add(this.AppSchema.StockBalance.BALANCE_DATEColumn, Balancedate);
                    }
                    dataManager.Parameters.Add(this.AppSchema.StockBalance.PROJECT_IDColumn, ProjectID);
                    if (itemID > 0)
                    {
                        dataManager.Parameters.Add(this.AppSchema.StockBalance.ITEM_IDColumn, itemID);
                    }
                    if (locationID > 0)
                    {
                        dataManager.Parameters.Add(this.AppSchema.StockBalance.LOCATION_IDColumn, locationID);
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs FetchStockDetails(int ProjectID, string Balancedate, int locationID, int itemID)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.StockUpdation.FetchStockBalance))
                {
                    dataManager.Parameters.Add(this.AppSchema.StockBalance.BALANCE_DATEColumn, Balancedate);
                    dataManager.Parameters.Add(this.AppSchema.StockBalance.PROJECT_IDColumn, ProjectID);
                    if (itemID > 0)
                    {
                        dataManager.Parameters.Add(this.AppSchema.StockBalance.ITEM_IDColumn, itemID);
                    }
                    if (locationID > 0)
                    {
                        dataManager.Parameters.Add(this.AppSchema.StockBalance.LOCATION_IDColumn, locationID);
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs FetchStockDetails(int ProjectID, int locationID, int itemID)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.StockUpdation.FetchStockBalance))
                {
                    dataManager.Parameters.Add(this.AppSchema.StockBalance.PROJECT_IDColumn, ProjectID);
                    if (itemID > 0)
                    {
                        dataManager.Parameters.Add(this.AppSchema.StockBalance.ITEM_IDColumn, itemID);
                    }
                    if (locationID > 0)
                    {
                        dataManager.Parameters.Add(this.AppSchema.StockBalance.LOCATION_IDColumn, locationID);
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs FetchStockOPDetails(int ProjectID, int locationID, int itemID)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.StockUpdation.FetchStockBalance))
                {
                    dataManager.Parameters.Add(this.AppSchema.StockBalance.PROJECT_IDColumn, ProjectID);
                    dataManager.Parameters.Add(this.AppSchema.StockBalance.TRANS_FLAGColumn, "OP");
                    if (itemID > 0)
                    {
                        dataManager.Parameters.Add(this.AppSchema.StockBalance.ITEM_IDColumn, itemID);
                    }
                    if (locationID > 0)
                    {
                        dataManager.Parameters.Add(this.AppSchema.StockBalance.LOCATION_IDColumn, locationID);
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }


        public ResultArgs FetchStockDetails(int ProjectID, string Balancedate, int locationID, int itemID, int GroupID, int CategoryID)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.StockUpdation.FetchStockAvailabilityDetails))
                {
                    dataManager.Parameters.Add(this.AppSchema.StockBalance.BALANCE_DATEColumn, Balancedate);
                    dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_FROMColumn, DateSet.ToDate(this.YearFrom, false));
                    dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_TOColumn, DateSet.ToDate(Balancedate, false));
                    dataManager.Parameters.Add(this.AppSchema.StockBalance.PROJECT_IDColumn, ProjectID);
                    if (itemID > 0)
                    {
                        dataManager.Parameters.Add(this.AppSchema.StockBalance.ITEM_IDColumn, itemID);
                    }
                    if (locationID > 0)
                    {
                        dataManager.Parameters.Add(this.AppSchema.StockBalance.LOCATION_IDColumn, locationID);
                    }
                    if (GroupID > 0)
                    {
                        dataManager.Parameters.Add(this.AppSchema.StockGroup.GROUP_IDColumn, GroupID);
                    }
                    if (CategoryID > 0)
                    {
                        dataManager.Parameters.Add(this.AppSchema.StockCategory.CATEGORY_IDColumn, CategoryID);
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        #endregion
    }
}
