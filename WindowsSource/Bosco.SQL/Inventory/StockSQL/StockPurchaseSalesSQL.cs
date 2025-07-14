using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class StockPurchaseSalesSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.StockPurchaseSales).FullName)
            {
                query = GetstockSalesSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion


        #region SQL Script
        public string GetstockSalesSQL()
        {
            string query = "";
            SQLCommand.StockPurchaseSales SqlcommandId = (SQLCommand.StockPurchaseSales)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.StockPurchaseSales.FetchItem:
                    {
                        query = "SELECT SNI.ITEM_ID, CONCAT(NAME, ' - ', SGP.GROUP_NAME) AS ITEM_NAME, RATE\n" +
                                "FROM STOCK_ITEM SNI\n" +
                                "LEFT JOIN STOCK_GROUP SGP\n" +
                                  "ON SNI.GROUP_ID = SGP.GROUP_ID";
                        break;
                    }

                case SQLCommand.StockPurchaseSales.FetchLocationbyItem:
                    {
                        //query = "SELECT ITEM_ID, ASL.LOCATION_ID, LOCATION_NAME, QUANTITY AS AVAIL_QUANTITY\n" +
                        //        "  FROM ASSET_STOCK_LOCATION ASL  \n" +
                        //        " LEFT JOIN STOCK_ITEM_DETAILS SID\n" +
                        //        "    ON SID.LOCATION_ID = ASL.LOCATION_ID\n" +
                        //        "WHERE LOCATION_NAME NOT IN('Primary')";
                        query = "SELECT ITEM_ID,\n" +
                                "       SID.LOCATION_ID,\n" +
                                "       ASL.LOCATION_NAME,\n" +
                                "       QUANTITY AS AVAIL_QUANTITY\n" +
                                "  FROM ASSET_STOCK_LOCATION ASL\n" +
                                " INNER JOIN INVENTORY_STOCK SID\n" +
                                "    ON SID.LOCATION_ID = ASL.LOCATION_ID\n" +
                                " WHERE LOCATION_NAME NOT IN ('Primary') AND TRANS_FLAG <>'OP'\n";
                               // " GROUP BY ITEM_ID, LOCATION_ID;";
                        break;
                    }

                case SQLCommand.StockPurchaseSales.FetchLocations:
                    {
                        query = "SELECT ITEM_ID,\n" +
                                "       SID.LOCATION_ID,\n" +
                                "       ASL.LOCATION,\n" +
                                "       QUANTITY AS AVAIL_QUANTITY\n" +
                                "  FROM ASSET_LOCATION ASL\n" +
                                " INNER JOIN INVENTORY_STOCK SID\n" +
                                "    ON SID.LOCATION_ID = ASL.LOCATION_ID\n" +
                                " GROUP BY ITEM_ID,LOCATION_ID";
                        break;
                    }
                case SQLCommand.StockPurchaseSales.FetchDashboardDetails:
                    {
                        query = "SELECT CONCAT(LEFT(MONTHNAME(TT.BALANCE_DATE), 3), '-', YEAR(BALANCE_DATE)) AS MONTH_NAME,\n" +
                                "       SUM(TT.QUANTITY) AS QUANTITY\n" +
                                "  FROM (SELECT T.BALANCE_DATE,\n" +
                                "               T.LOCATION,\n" +
                                "               T.ITEM_ID,\n" +
                                "               T.LOCATION_ID,\n" +
                                "               T.NAME,\n" +
                                "               T.RATE,\n" +
                                "               T.QUANTITY,\n" +
                                "               T.RATE * T.QUANTITY AS VALUE_AMOUNT\n" +
                                "          FROM (SELECT CASE\n" +
                                "                         WHEN ISV.TRANS_FLAG = 'OP' THEN\n" +
                                "                          DATE_ADD(ISV.BALANCE_DATE, INTERVAL 1 DAY)\n" +
                                "                         ELSE\n" +
                                "                          ISV.BALANCE_DATE\n" +
                                "                       END AS BALANCE_DATE,\n" +
                                "                       SL.LOCATION,\n" +
                                "                       ISV.ITEM_ID,\n" +
                                "                       SL.LOCATION_ID,\n" +
                                "                       SI.NAME,\n" +
                                "                       ISV.RATE,\n" +
                                "                       ISV.QUANTITY\n" +
                                "                  FROM INVENTORY_STOCK ISV\n" +
                                "                 INNER JOIN STOCK_ITEM SI\n" +
                                "                    ON ISV.ITEM_ID = SI.ITEM_ID\n" +
                                "                 INNER JOIN ASSET_LOCATION SL\n" +
                                "                    ON ISV.LOCATION_ID = SL.LOCATION_ID\n" +
                                "                 WHERE ISV.PROJECT_ID IN (?PROJECT_ID)\n" +
                                "                   AND BALANCE_DATE <= ?YEAR_TO\n" +
                                "                  { AND ISV.ITEM_ID = ?ITEM_ID }\n" +
                                "                  { AND ISV.LOCATION_ID = ?LOCATION_ID }\n" +
                                "                  { AND ISV.GROUP_ID = ?GROUP_ID }\n" +
                                "                 ORDER BY ISV.BALANCE_DATE DESC) AS T\n" +
                                "         WHERE T.QUANTITY\n" +
                                "         GROUP BY T.ITEM_ID, T.LOCATION_ID) AS TT\n" +
                                " GROUP BY YEAR(BALANCE_DATE), MONTH(BALANCE_DATE)\n" +
                                " ORDER BY YEAR(BALANCE_DATE), MONTH(BALANCE_DATE);";
                        break;
                    }
                case SQLCommand.StockPurchaseSales.FetchReorderLevel:
                    {
                        //query = "SELECT INV.ITEM_ID,\n" +
                        //        "       SI.NAME AS ITEM_NAME,\n" +
                        //        "       SI.REORDER,\n" +
                        //        "       INV.QUANTITY AS AVAIL_QTY\n" +
                        //        "  FROM INVENTORY_STOCK INV\n" +
                        //        " INNER JOIN STOCK_ITEM SI\n" +
                        //        "    ON INV.ITEM_ID = SI.ITEM_ID\n" +
                        //        " LEFT JOIN (SELECT BALANCE_DATE AS STOCK_DATE\n" +
                        //        "          FROM INVENTORY_STOCK\n" +
                        //        "         WHERE PROJECT_ID = ?PROJECT_ID ORDER BY BALANCE_DATE DESC) AS T1\n" +
                        //        "    ON T1.STOCK_DATE <= INV.BALANCE_DATE\n" +
                        //        " WHERE INV.QUANTITY < SI.REORDER\n" +
                        //        "   AND SI.REORDER > 0\n" +
                        //        "   AND INV.PROJECT_ID = ?PROJECT_ID\n" +
                        //        " GROUP BY PROJECT_ID, ITEM_ID";
                        query = "SELECT T1.BALANCE_DATE,\n" +
                                "       INV.ITEM_ID,\n" +
                                "       INV.LOCATION_ID,\n" +
                                "      SI.REORDER, INV.QUANTITY AS AVAIL_QTY,\n" +
                                "       SI.NAME AS ITEM_NAME,\n" +
                                "       ASL.LOCATION\n" +
                                "  FROM INVENTORY_STOCK INV\n" +
                                " INNER JOIN (SELECT MAX(BALANCE_DATE) AS BALANCE_DATE, LOCATION_ID, ITEM_ID\n" +
                                "               FROM INVENTORY_STOCK ISS\n" +
                                "              WHERE PROJECT_ID = ?PROJECT_ID\n" +
                                "              GROUP BY LOCATION_ID, ITEM_ID\n" +
                                "              ORDER BY ISS.LOCATION_ID, ISS.ITEM_ID DESC) AS T1\n" +
                                "    ON INV.BALANCE_DATE = T1.BALANCE_DATE\n" +
                                "   AND INV.LOCATION_ID = T1.LOCATION_ID\n" +
                                "   AND INV.ITEM_ID = T1.ITEM_ID\n" +
                                "  LEFT JOIN STOCK_ITEM SI\n" +
                                "    ON INV.ITEM_ID = SI.ITEM_ID\n" +
                                "  LEFT JOIN ASSET_LOCATION ASL\n" +
                                "    ON INV.LOCATION_ID = ASL.LOCATION_ID\n" +
                                " WHERE INV.PROJECT_ID = ?PROJECT_ID AND INV.BALANCE_DATE BETWEEN ?YEAR_FROM AND ?YEAR_TO  \n" +
                                "   AND INV.QUANTITY < SI.REORDER\n" +
                                "   AND SI.REORDER > 0\n" +
                                " GROUP BY LOCATION_ID, ITEM_ID\n" +
                                " ORDER BY BALANCE_DATE ASC;";

                        break;
                    }
                case SQLCommand.StockPurchaseSales.FetchUnitofMeasurebyItem:
                    {
                        query = "SELECT ASU.SYMBOL\n" +
                                "  FROM STOCK_ITEM SI\n" +
                                " INNER JOIN UOM ASU\n" +
                                "    ON SI.UNIT_ID = ASU.UOM_ID\n" +
                                " WHERE SI.ITEM_ID = ?ITEM_ID;";
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}
