using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.Utility;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using System.Data;

namespace Bosco.Model
{
    public class StockItemSystem : SystemBase
    {
        #region VaraibleDeclaration
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Constructor
        public StockItemSystem()
        {
        }
        public StockItemSystem(int item_Id)
        {
            this.ItemId = item_Id;
            AssignToStockItemPoroperties();
        }
        #endregion

        #region Properties
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
        public int CategoryId { get; set; }
        public int UnitId { get; set; }
        public Decimal Quantity { get; set; }
        public Decimal Rate { get; set; }
        public Decimal Value { get; set; }
        public int LocationId { get; set; }
        public int ReOrder { get; set; }
        public int ProjectId { get; set; }
        public int IncomeLedgerId { get; set; }
        public int ExpenseLedgerId { get; set; }
        #endregion

        #region Methods
        public ResultArgs SaveStockItems()
        {
            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.BeginTransaction();
                    resultArgs = SaveStockItemMasterDetails();
                    //if (resultArgs != null && resultArgs.Success)
                    //{
                    //resultArgs = SaveStockItemDetails();
                    //}
                    dataManager.EndTransaction();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString() + Environment.NewLine + ex.Source, true);
            }
            finally { }
            return resultArgs;
        }

        private ResultArgs SaveStockItemDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.AppSchema.StockItem.ITEM_IDColumn, ItemId);
                    dataManager.Parameters.Add(this.AppSchema.StockPurchaseDetails.LOCATION_IDColumn, LocationId);
                    dataManager.Parameters.Add(this.AppSchema.StockItem.QUANTITYColumn, Quantity);
                    dataManager.Parameters.Add(this.AppSchema.StockPurchaseDetails.UNIT_PRICEColumn, Rate);
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
            return resultArgs;
        }

        public ResultArgs SaveStockItemMasterDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager((ItemId == 0) ? SQLCommand.StockItem.Add : SQLCommand.StockItem.Update))
                {
                    dataManager.Parameters.Add(this.AppSchema.StockItem.ITEM_IDColumn, ItemId);
                    dataManager.Parameters.Add(this.AppSchema.StockItem.NAMEColumn, Name);
                    dataManager.Parameters.Add(this.AppSchema.StockItem.GROUP_IDColumn, GroupId);
                    dataManager.Parameters.Add(this.AppSchema.ASSETCategory.CATEGORY_IDColumn, 1); // chinna 
                    dataManager.Parameters.Add(this.AppSchema.StockItem.UNIT_IDColumn, UnitId);
                    dataManager.Parameters.Add(this.AppSchema.StockItem.QUANTITYColumn, Quantity);
                    dataManager.Parameters.Add(this.AppSchema.StockItem.INCOME_LEDGER_IDColumn, IncomeLedgerId);
                    dataManager.Parameters.Add(this.AppSchema.StockItem.EXPENSE_LEDGER_IDColumn, ExpenseLedgerId);
                    dataManager.Parameters.Add(this.AppSchema.StockItem.RATEColumn, Rate);
                    dataManager.Parameters.Add(this.AppSchema.StockItem.VALUEColumn, Quantity * Rate);
                    dataManager.Parameters.Add(this.AppSchema.StockItem.REORDERColumn, ReOrder);
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            { }
            return resultArgs;
        }

        public ResultArgs FetchStockItemDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockItem.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchStockItemDetailsById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockItem.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.StockItem.ITEM_IDColumn, this.ItemId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public void AssignToStockItemPoroperties()
        {
            resultArgs = FetchStockItemDetailsById();
            if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs.DataSource.Table != null)
            {
                Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockItem.NAMEColumn.ColumnName].ToString();
                GroupId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockItem.GROUP_IDColumn.ColumnName].ToString());
                CategoryId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETCategory.CATEGORY_IDColumn.ColumnName].ToString());
                UnitId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockItem.UNIT_IDColumn.ColumnName].ToString());
                IncomeLedgerId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockItem.INCOME_LEDGER_IDColumn.ColumnName].ToString());
                ExpenseLedgerId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockItem.EXPENSE_LEDGER_IDColumn.ColumnName].ToString());
                Quantity = NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockItem.QUANTITYColumn.ColumnName].ToString());
                Rate = NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockItem.RATEColumn.ColumnName].ToString());
                Value = NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockItem.VALUEColumn.ColumnName].ToString());
                ReOrder = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockItem.REORDERColumn.ColumnName].ToString());
            }
        }

        public ResultArgs DeleteStockItem(int ItemId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockItem.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.StockItem.ITEM_IDColumn, ItemId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public int FetchStockReorderLevel()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockItem.FetchReorderLevelByItem))
            {
                dataManager.Parameters.Add(this.AppSchema.StockItem.ITEM_IDColumn, ItemId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs FetchStockOPBalance()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockItem.FetchStockBalance))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.StockBalance.LOCATION_IDColumn, LocationId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteStockItemDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockItem.DeleteStockItemDetails))
            {
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        #endregion

    }
}
