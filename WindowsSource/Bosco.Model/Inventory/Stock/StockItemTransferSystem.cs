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
    public class StockItemTransferSystem : SystemBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        private const string FROM_LOCATION = "FROM_LOCATION";
        private const string TO_LOCATION = "TO_LOCATION";
        #endregion

        #region Construtor
        public StockItemTransferSystem()
        {

        }
        public StockItemTransferSystem(int EditId)
        {
            this.EditId = EditId;
        }

        #endregion

        #region Properties
        public DataTable dtTransferItem { get; set; }
        public DateTime TransferDate { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int TransferredItemId { get; set; }
        public int EditId { get; set; }
        public int ProjectId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public int FromLocationId { get; set; }
        public int ToLocationId { get; set; }
        #endregion

        #region Methods
        public ResultArgs SaveItemTransfer()
        {
            using (DataManager dataManger = new DataManager())
            {
                dataManger.BeginTransaction();
                resultArgs = TransferItem();
                dataManger.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs TransferItem()
        {
            if (dtTransferItem != null)
            {
                int EditNo = EditId == 0 ? GetEditIdAddMode() + 1 : EditId;
                foreach (DataRow drItem in dtTransferItem.Rows)
                {
                    using (DataManager dataManger = new DataManager(EditId == 0 ? SQLCommand.StockItemTransfer.Add : SQLCommand.StockItemTransfer.Update))
                    {
                        ItemId = NumberSet.ToInteger(drItem[AppSchema.StockPurchaseSalesDetails.ITEM_NAMEColumn.ColumnName].ToString());
                        int FromLocationId = NumberSet.ToInteger(drItem[FROM_LOCATION].ToString());
                        int ToLocationId = NumberSet.ToInteger(drItem[TO_LOCATION].ToString());
                        int Quantity = NumberSet.ToInteger(drItem[AppSchema.StockSalesDetails.QUANTITYColumn.ColumnName].ToString());
                        if (ItemId > 0)
                        {
                            if (EditId > 0) //Updating based on Transfer id  only in Edit Mode
                                dataManger.Parameters.Add(AppSchema.StockItemTransfer.TRANSFER_IDColumn, NumberSet.ToInteger(drItem[AppSchema.StockItemTransfer.TRANSFER_IDColumn.ColumnName].ToString()));
                            dataManger.Parameters.Add(AppSchema.StockItemTransfer.TRANSFER_DATEColumn, TransferDate);
                            dataManger.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                            dataManger.Parameters.Add(AppSchema.StockItemTransfer.ITEM_IDColumn, ItemId);
                            dataManger.Parameters.Add(AppSchema.StockItemTransfer.EDIT_IDColumn, EditNo);
                            dataManger.Parameters.Add(AppSchema.StockItemTransfer.QUANTITYColumn, Quantity);
                            dataManger.Parameters.Add(AppSchema.StockItemTransfer.FROM_LOCATION_IDColumn, FromLocationId);
                            dataManger.Parameters.Add(AppSchema.StockItemTransfer.TO_LOCATION_IDColumn, ToLocationId);
                            resultArgs = dataManger.UpdateData();
                            if (resultArgs != null && resultArgs.Success)
                            {
                                using (StockBalanceSystem stockBalanceSystem = new StockBalanceSystem())
                                {
                                    // resultArgs = stockBalanceSystem.UpdateStockDetails(ProjectId, TransferDate, FromLocationId, ItemId, Quantity, 0, 1);//Reduce the stock item Quantity by number of Quantity
                                    resultArgs = stockBalanceSystem.UpdateStockDetails(ProjectId, TransferDate, FromLocationId, ItemId, Quantity, 0, (int)StockReturnType.OutWards);//Reduce the stock item Quantity by number of Quantity
                                    if (resultArgs != null && resultArgs.Success)
                                    {
                                        // resultArgs = stockBalanceSystem.UpdateStockDetails(ProjectId, TransferDate, ToLocationId, ItemId, Quantity, 0, 0);//Increase the stock item Quantity by number of Quantity
                                        resultArgs = stockBalanceSystem.UpdateStockDetails(ProjectId, TransferDate, ToLocationId, ItemId, Quantity, 0, (int)StockReturnType.InWards);//Increase the stock item Quantity by number of Quantity
                                        if (resultArgs != null && !resultArgs.Success) break;
                                    }
                                    else break;
                                }
                            }
                            else break;
                        }
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs FetchTransferredItemDetails()
        {
            using (DataManager dataManger = new DataManager(SQLCommand.StockItemTransfer.FetchByProjectId))
            {
                dataManger.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManger.Parameters.Add(AppSchema.Project.DATE_STARTEDColumn, DateFrom);
                dataManger.Parameters.Add(AppSchema.Project.DATE_CLOSEDColumn, DateTo);
                resultArgs = dataManger.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DelelteTransferedItem()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockItemTransfer.Delete))
            {
                dataManager.Parameters.Add(AppSchema.StockItemTransfer.TRANSFER_IDColumn, TransferredItemId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchTransferredItemByEditId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockItemTransfer.FetchByEditId))
            {
                dataManager.Parameters.Add(AppSchema.StockItemTransfer.EDIT_IDColumn, EditId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private int GetEditIdAddMode()
        {
            using (DataManager dataMananer = new DataManager(SQLCommand.StockItemTransfer.GetNewEditId))
            {
                resultArgs = dataMananer.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        #endregion
    }
}
