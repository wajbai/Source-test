using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.Utility;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using System.Data;

namespace Bosco.Model.Stock
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
        #endregion

        #region Methods
        public ResultArgs SaveStockItemDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager((ItemId == 0) ? SQLCommand.StockItem.Add : SQLCommand.StockItem.Update))
                {
                    dataManager.Parameters.Add(this.AppSchema.StockItem.ITEM_IDColumn, ItemId);
                    dataManager.Parameters.Add(this.AppSchema.StockItem.NAMEColumn, Name);
                    dataManager.Parameters.Add(this.AppSchema.StockItem.GROUP_IDColumn, GroupId);
                    dataManager.Parameters.Add(this.AppSchema.ASSETCategory.CATEGORY_IDColumn, CategoryId);
                    dataManager.Parameters.Add(this.AppSchema.StockItem.UNIT_IDColumn, UnitId);
                    dataManager.Parameters.Add(this.AppSchema.StockItem.QUANTITYColumn, Quantity);
                    dataManager.Parameters.Add(this.AppSchema.StockItem.RATEColumn, Rate);
                    dataManager.Parameters.Add(this.AppSchema.StockItem.VALUEColumn, Value);
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
            if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs.DataSource.Table != null)
            {
                Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockItem.NAMEColumn.ColumnName].ToString();
                GroupId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockItem.GROUP_IDColumn.ColumnName].ToString());
                CategoryId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETCategory.CATEGORY_IDColumn.ColumnName].ToString());
                UnitId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockItem.UNIT_IDColumn.ColumnName].ToString());
                Quantity = NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockItem.QUANTITYColumn.ColumnName].ToString());
                Rate = NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockItem.RATEColumn.ColumnName].ToString());
                Value = NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockItem.VALUEColumn.ColumnName].ToString());
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
        #endregion
        
    }
}
