using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class StockItemSQL : IDatabaseQuery
    {
        #region ISQLServerQuery Members

        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.StockItem).FullName)
            {
                query = GetstockItemSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion


        #region SQL Script
        public string GetstockItemSQL()
        {
            string query = "";
            SQLCommand.StockItem SqlcommandId = (SQLCommand.StockItem)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.StockItem.Add:
                    {
                        query = "INSERT INTO STOCK_ITEM (" +
                                "NAME," +
                                "GROUP_ID," +
                                "CATEGORY_ID," +
                                "UNIT_ID," +
                                "INCOME_LEDGER_ID," +
                                "EXPENSE_LEDGER_ID," +
                                "QUANTITY," +
                                "RATE," +
                                "REORDER," +
                                "VALUE)VALUES(" +
                                "?NAME," +
                                "?GROUP_ID, " +
                                "?CATEGORY_ID," +
                                "?UNIT_ID, " +
                                "?INCOME_LEDGER_ID, " +
                                "?EXPENSE_LEDGER_ID, " +
                                "?QUANTITY," +
                                "?RATE," +
                                "?REORDER," +
                                "?VALUE)";
                        break;
                    }

                case SQLCommand.StockItem.Update:
                    {
                        query = "UPDATE STOCK_ITEM SET " +
                                "NAME=?NAME," +
                                "GROUP_ID=?GROUP_ID," +
                                "CATEGORY_ID=?CATEGORY_ID," +
                                "UNIT_ID=?UNIT_ID," +
                                "INCOME_LEDGER_ID=?INCOME_LEDGER_ID," +
                                "EXPENSE_LEDGER_ID=?EXPENSE_LEDGER_ID," +
                                "QUANTITY=?QUANTITY," +
                                "RATE=?RATE," +
                                "REORDER=?REORDER," +
                                "VALUE=?VALUE " +
                                "WHERE ITEM_ID =?ITEM_ID";
                        break;
                    }

                case SQLCommand.StockItem.FetchAll:
                    {
                        query = "SELECT ITEM_ID, SI.NAME, SI.GROUP_ID,SG.GROUP_NAME AS ASSET_CLASS, SU.SYMBOL, QUANTITY, RATE,REORDER, VALUE FROM STOCK_ITEM SI " +
                                "LEFT JOIN STOCK_GROUP SG ON SI.GROUP_ID = SG.GROUP_ID LEFT JOIN " +
                                "UOM SU ON SU.UOM_ID =SI.UNIT_ID ";
                        break;
                    }
                case SQLCommand.StockItem.Delete:
                    {
                        query = "DELETE FROM STOCK_ITEM WHERE ITEM_ID =?ITEM_ID";
                        break;
                    }
                case SQLCommand.StockItem.Fetch:
                    {
                        query = "SELECT ITEM_ID, NAME, GROUP_ID, CATEGORY_ID, UNIT_ID,INCOME_LEDGER_ID,EXPENSE_LEDGER_ID, QUANTITY, RATE,REORDER, VALUE FROM STOCK_ITEM WHERE ITEM_ID =?ITEM_ID";
                        break;
                    }
                case SQLCommand.StockItem.FetchReorderLevelByItem:
                    {
                        query = "SELECT REORDER FROM STOCK_ITEM WHERE ITEM_ID =?ITEM_ID";
                        break;
                    }
                case SQLCommand.StockItem.FetchStockBalance:
                    {
                        query = "SELECT SI.NAME,\n" +
                        "       SI.ITEM_ID,\n" +
                        "       IFNULL(IVS.QUANTITY, 0) AS QUANTITY,\n" +
                        "      CASE WHEN IVS.RATE>0 THEN IVS.RATE ELSE SI.RATE END AS RATE\n" +
                        "  FROM STOCK_ITEM AS SI\n" +
                        "  LEFT JOIN INVENTORY_STOCK AS IVS\n" +
                        "    ON SI.ITEM_ID = IVS.ITEM_ID\n" +
                        "   AND IVS.PROJECT_ID = ?PROJECT_ID\n" +
                        "   AND LOCATION_ID = ?LOCATION_ID\n" +
                        "   AND TRANS_FLAG='OP' ORDER BY IVS.QUANTITY DESC";
                        break;
                    }

                case SQLCommand.StockItem.DeleteStockItemDetails:
                    {
                        query = "DELETE FROM STOCK_ITEM";
                        break;
                    }

                case SQLCommand.StockItem.FetchStockItemNameByID:
                    {
                        query = "SELECT ITEM_ID FROM STOCK_ITEM WHERE NAME=?NAME";
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}
