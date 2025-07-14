using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class StockBalanceSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.StockUpdation).FullName)
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
            SQLCommand.StockUpdation SqlcommandId = (SQLCommand.StockUpdation)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.StockUpdation.UpdatestockDetails:
                    {
                        query = "INSERT INTO inventory_stock\n" +
                                "  (BALANCE_DATE, BRANCH_ID, PROJECT_ID, LOCATION_ID, ITEM_ID, QUANTITY,STOCK_TYPE,RATE,TRANS_FLAG)\n" +
                                "  (SELECT LB.BALANCE_DATE,\n" +
                                "          LB.BRANCH_ID,\n" +
                                "          LB.PROJECT_ID,\n" +
                                "          LB.LOACTION_ID,\n" +
                                "          LB.ITEM_ID,\n" +
                                "          IFNULL(LB1.QUANTITY, 0) AS QUANTITY,LB.STOCK_TYPE,LB.RATE,LB.TRANS_FLAG\n" +
                                "     FROM (SELECT ?BALANCE_DATE AS BALANCE_DATE,\n" +
                                "                  0 AS BRANCH_ID,\n" +
                                "                  ?PROJECT_ID AS PROJECT_ID,\n" +
                                "                  ?LOCATION_ID AS LOACTION_ID,\n" +
                                "                  ?ITEM_ID AS ITEM_ID,?STOCK_TYPE AS STOCK_TYPE,?RATE AS RATE,?TRANS_FLAG AS TRANS_FLAG) AS LB\n" +
                                "     LEFT JOIN (SELECT IFNULL(QUANTITY, 0) AS QUANTITY\n" +
                                "                 FROM inventory_stock\n" +
                                "                WHERE BALANCE_DATE < ?BALANCE_DATE\n" +
                                "                  AND BRANCH_ID = 0\n" +
                                "                  AND PROJECT_ID = ?PROJECT_ID\n" +
                                "                  AND LOCATION_ID = ?LOCATION_ID\n" +
                                "                  AND ITEM_ID = ?ITEM_ID\n" +
                                "                ORDER BY BALANCE_DATE DESC LIMIT 1) AS LB1\n" +
                                "       ON 1 = 1) ON DUPLICATE KEY UPDATE inventory_stock.QUANTITY = inventory_stock.QUANTITY;\n" +
                                "SET @Qty := 0;\n" +
                                "UPDATE INVENTORY_STOCK SET RATE=?RATE,STOCK_TYPE=?STOCK_TYPE,\n" +
                                "   QUANTITY = CASE WHEN ?STOCK_TYPE =0 THEN (@Qty := QUANTITY) + ?QUANTITY WHEN ?STOCK_TYPE <> 0 THEN (@Qty :=\n" +
                                "                                                                                                            QUANTITY) - ?QUANTITY ELSE @Qty := QUANTITY END\n" +
                                "\n" +
                                " WHERE BALANCE_DATE >= ?BALANCE_DATE\n" +
                                "   AND BRANCH_ID = 0\n" +
                                "   AND PROJECT_ID = ?PROJECT_ID\n" +
                                "   AND LOCATION_ID = ?LOCATION_ID\n" +
                                "   AND ITEM_ID = ?ITEM_ID;";
                        break;
                    }
                case SQLCommand.StockUpdation.FetchStockBalance:
                    {
                        query = "SELECT  QUANTITY,RATE FROM INVENTORY_STOCK \n" +
                                " WHERE \n" +
                                "   PROJECT_ID=?PROJECT_ID  \n" +
                                " { AND BALANCE_DATE <= ?BALANCE_DATE }\n" +
                                " { AND ITEM_ID=?ITEM_ID }\n" +
                                " { AND LOCATION_ID=?LOCATION_ID } \n" +
                                " { AND TRANS_FLAG=?TRANS_FLAG } \n" +
                                " ORDER BY BALANCE_DATE DESC LIMIT 1";
                        break;
                    }
                case SQLCommand.StockUpdation.FetchStockAvailabilityDetails:
                    {
                        //query = "SELECT\n" +
                        //" CASE\n" +
                        //" WHEN ISV.TRANS_FLAG = 'OP' THEN\n" +
                        //" DATE_ADD(ISV.BALANCE_DATE, INTERVAL 1 DAY)\n" +
                        //" ELSE\n" +
                        //" ISV.BALANCE_DATE\n" +
                        //" END AS STOCK_DATE,\n" +
                        //" MP.PROJECT,\n" +
                        //" SG.GROUP_NAME,\n" +
                        //" ASL.LOCATION_NAME,\n" +
                        //" SI.NAME AS ITEM_NAME,\n" +
                        //" SI.RATE,\n" +
                        //" ISV.QUANTITY\n" +
                        //" FROM INVENTORY_STOCK ISV\n" +
                        //" INNER JOIN ASSET_STOCK_LOCATION ASL\n" +
                        //" ON ISV.LOCATION_ID = ASL.LOCATION_ID\n" +
                        //" INNER JOIN MASTER_PROJECT MP\n" +
                        //" ON ISV.PROJECT_ID = MP.PROJECT_ID\n" +
                        //" INNER JOIN STOCK_ITEM SI\n" +
                        //" ON ISV.ITEM_ID=SI.ITEM_ID\n" +
                        //" INNER JOIN STOCK_GROUP SG\n" +
                        //" ON SI.GROUP_ID = SG.GROUP_ID\n" +
                        //" JOIN (SELECT MAX(BALANCE_DATE) AS BAL_DATE,\n" +
                        //" LOCATION_ID AS LOC_ID,\n" +
                        //" ITEM_ID AS ITM_ID\n" +
                        //" FROM INVENTORY_STOCK ISV\n" +
                        //" WHERE PROJECT_ID = ?PROJECT_ID\n" +
                        //" { AND LOCATION_ID = ?LOCATION_ID  }\n" +
                        //" {      AND ITEM_ID = ?ITEM_ID }\n" +
                        //" AND QUANTITY NOT IN (0)\n" +
                        //" AND BALANCE_DATE <= ?BALANCE_DATE\n" +
                        //" GROUP BY LOCATION_ID, ITEM_ID) AS T\n" +
                        //" ON ISV.LOCATION_ID = T.LOC_ID\n" +
                        //" AND ISV.ITEM_ID = T.ITM_ID\n" +
                        //" AND ISV.BALANCE_DATE = T.BAL_DATE\n" +
                        //" WHERE ISV.PROJECT_ID IN (?PROJECT_ID) { AND SG.GROUP_ID = ?GROUP_ID}\n" +
                        //" GROUP BY ISV.LOCATION_ID, ISV.ITEM_ID";

                        query = "SELECT CASE\n" +
                                   "         WHEN ISV.TRANS_FLAG = 'OP' THEN\n" +
                                   "          DATE_ADD(ISV.BALANCE_DATE, INTERVAL 1 DAY)\n" +
                                   "         ELSE\n" +
                                   "          ISV.BALANCE_DATE\n" +
                                   "       END AS STOCK_DATE,\n" +
                                   "       MP.PROJECT,\n" +
                                   "       SG.GROUP_NAME AS ASSET_CLASS,\n" +
                                   "       ASL.LOCATION,\n" +
                                   "       CONCAT(SI.NAME, ' - ',ASL.LOCATION) AS ITEM_NAME,\n" +
                                   "       SI.RATE,\n" +
                                   "       ISV.QUANTITY\n" +
                                   "  FROM INVENTORY_STOCK ISV\n" +
                                   " INNER JOIN ASSET_LOCATION ASL\n" +
                                   "    ON ISV.LOCATION_ID = ASL.LOCATION_ID\n" +
                                   " INNER JOIN MASTER_PROJECT MP\n" +
                                   "    ON ISV.PROJECT_ID = MP.PROJECT_ID\n" +
                                   " INNER JOIN STOCK_ITEM SI\n" +
                                   "    ON ISV.ITEM_ID = SI.ITEM_ID\n" +
                                   " INNER JOIN STOCK_GROUP SG\n" +
                                   "    ON SI.GROUP_ID = SG.GROUP_ID\n" +
                                   "  JOIN (SELECT MAX(BALANCE_DATE) AS BAL_DATE,\n" +
                                   "               LOCATION_ID AS LOC_ID,\n" +
                                   "               ITEM_ID AS ITM_ID\n" +
                                   "          FROM INVENTORY_STOCK ISV\n" +
                                   "         WHERE PROJECT_ID = ?PROJECT_ID\n" +
                                   "          { AND LOCATION_ID = ?LOCATION_ID }\n" +
                                   "          { AND ITEM_ID = ?ITEM_ID }\n" +
                                   "           AND QUANTITY NOT IN (0)\n" +
                                   "           AND BALANCE_DATE <= ?BALANCE_DATE\n" +
                                   "         GROUP BY LOCATION_ID, ITEM_ID) AS T\n" +
                                   "    ON ISV.LOCATION_ID = T.LOC_ID\n" +
                                   "   AND ISV.ITEM_ID = T.ITM_ID\n" +
                                   "   AND ISV.BALANCE_DATE = T.BAL_DATE\n" +
                                   " WHERE ISV.PROJECT_ID IN (?PROJECT_ID)\n" +
                                   "  { AND SG.GROUP_ID = ?GROUP_ID }\n" +
                                   " GROUP BY ISV.LOCATION_ID, ISV.ITEM_ID";   


                        break;
                    }
                case SQLCommand.StockUpdation.FetchStockOPBalance:
                    {
                        query = "SELECT PROJECT_ID,BALANCE_DATE,\n" +
                        "       LOCATION_ID,\n" +
                        "       ITEM_ID,\n" +
                        "       QUANTITY,\n" +
                        "       RATE,\n" +
                        "       STOCK_TYPE,\n" +
                        "       TRANS_FLAG\n" +
                        "  FROM INVENTORY_STOCK\n" +
                        " WHERE PROJECT_ID = ?PROJECT_ID\n" +
                        "   AND LOCATION_ID = ?LOCATION_ID\n" +
                        "   AND TRANS_FLAG = 'OP'";
                        break;
                    }
                case SQLCommand.StockUpdation.DeleteOPBalance:
                    {
                        query = "DELETE FROM INVENTORY_STOCK WHERE PROJECT_ID=?PROJECT_ID AND LOCATION_ID=?LOCATION_ID AND TRANS_FLAG='OP'";
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}
