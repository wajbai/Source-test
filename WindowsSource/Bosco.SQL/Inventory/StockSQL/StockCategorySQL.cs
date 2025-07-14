using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class StockCategorySQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.StockCategory).FullName)
            {
                query = GetstockCategory();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        #region SQL Script
        public string GetstockCategory()
        {
            string query = "";
            SQLCommand.StockCategory SqlcommandId = (SQLCommand.StockCategory)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.StockCategory.Add:
                    {
                        query = "INSERT INTO STOCK_CATEGORY (" +
                                "CATEGORY_NAME," +
                                "PARENT_CATEGORY_ID)VALUES(" +
                                "?CATEGORY_NAME, " +
                                "?PARENT_CATEGORY_ID)";
                        break;
                    }
                case SQLCommand.StockCategory.Update:
                    {
                        query = "UPDATE STOCK_CATEGORY SET " +
                                "CATEGORY_NAME=?CATEGORY_NAME," +
                                "PARENT_CATEGORY_ID=?PARENT_CATEGORY_ID " +
                                "WHERE CATEGORY_ID =?CATEGORY_ID";
                        break;
                    }
                case SQLCommand.StockCategory.Fetch:
                    {
                        query = "SELECT CATEGORY_ID, CATEGORY_NAME, PARENT_CATEGORY_ID\n" +
                                "FROM STOCK_CATEGORY\n" +
                                "WHERE CATEGORY_ID = ?CATEGORY_ID";
                        break;
                    }
                case SQLCommand.StockCategory.FetchAll:
                    {
                        query = "SELECT CATEGORY_ID,CATEGORY_NAME AS NAME,PARENT_CATEGORY_ID FROM STOCK_CATEGORY";
                        break;
                    }
                //case SQLCommand.StockCategory.FetchStockCategory:
                //    {
                //        query = "SELECT CATEGORY_ID, NAME FROM STOCK_CATEGORY";
                //        break;
                //    }
                //case SQLCommand.StockCategory.FetchGroup:
                //    {
                //       query = "SELECT SC.NAME  AS 'CATEGORY NAME'," +
                //                "SC.PARENT_GROUP_ID," +
                //                "SG.NAME AS 'GROUP NAME'" +
                //                "FROM STOCK_CATEGORY SC" +
                //                "INNER JOIN STOCK_GROUP SG" +
                //                "ON SG.GROUP_ID = SC.PARENT_GROUP_ID";

                //        break;
                //    }

                case SQLCommand.StockCategory.Delete:
                    {
                        query = "DELETE FROM STOCK_CATEGORY WHERE CATEGORY_ID=?CATEGORY_ID";
                        break;
                    }
                case SQLCommand.StockCategory.FetchSelectedCategoryDetails:
                    {
                        query = "SELECT SI.ITEM_ID,\n" +
                                "       SI.NAME,\n" +
                                "       SI.CATEGORY_ID,\n" +
                                "       SC.CATEGORY_NAME AS CATEGORY,\n" +
                                "       UM.SYMBOL        AS UNIT,\n" +
                                "       T.QUANTITY,\n" +
                                "       SG.GROUP_NAME\n" +
                                "  FROM STOCK_ITEM AS SI\n" +
                                "  LEFT JOIN STOCK_CATEGORY AS SC\n" +
                                "    ON SI.CATEGORY_ID = SC.CATEGORY_ID\n" +
                                "  LEFT JOIN ASSET_STOCK_UNITOFMEASURE UM\n" +
                                "    ON SI.UNIT_ID = UM.UNIT_ID\n" +
                                "  LEFT JOIN STOCK_GROUP AS SG\n" +
                                "    ON SI.GROUP_ID = SG.GROUP_ID\n" +
                                "  LEFT JOIN (SELECT BALANCE_DATE, ITEM_ID, QUANTITY\n" +
                                "               FROM INVENTORY_STOCK\n" +
                                "              GROUP BY ITEM_ID\n" +
                                "              ORDER BY BALANCE_DATE DESC) AS T\n" +
                                "    ON SI.ITEM_ID = T.ITEM_ID\n" +
                                " WHERE SI.CATEGORY_ID IN (?CATEGORY_ID)";
                        break;
                    }

            }
            return query;

        }



        #endregion
    }
}
