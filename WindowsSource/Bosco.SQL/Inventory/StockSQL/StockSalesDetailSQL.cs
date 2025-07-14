using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class StockSalesDetailSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.StockSalesDetail).FullName)
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
            SQLCommand.StockSalesDetail SqlcommandId = (SQLCommand.StockSalesDetail)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.StockSalesDetail.Add:
                    {
                        query = "INSERT INTO STOCK_SOLD_UTILIZED_DETAILS(\n" +
                                "   SALES_ID, ITEM_ID, LOCATION_ID, QUANTITY, UNIT_PRICE,AMOUNT,ACCOUNT_LEDGER_ID,DISPOSAL_LEDGER_ID)\n" +
                                "   VALUES(?SALES_ID,?ITEM_ID,?LOCATION_ID,?QUANTITY,?UNIT_PRICE,?AMOUNT,?ACCOUNT_LEDGER_ID,?DISPOSAL_LEDGER_ID)";
                        break;
                    }

                case SQLCommand.StockSalesDetail.Update:
                    {
                        query = "UPDATE STOCK_SOLD_UTILIZED_DETAILS SET\n" +
                                "   ITEM_ID=?ITEM_ID,\n"+
                                "   LOCATION_ID=?LOCATION_ID,\n"+
                                "   QUANTITY=?QUANTITY,\n"+
                                "   UNIT_PRICE=?UNIT_PRICE\n" +
                                "   WHERE SALES_ID =?SALES_ID";
                        break;
                    }

                case SQLCommand.StockSalesDetail.FetchAll:
                    {
                        query = "SELECT SMS.SALES_ID,\n" +
                                "       SIT.NAME AS ITEM_NAME,\n" +
                                "       ASL.LOCATION,\n" +
                                "       SUD.QUANTITY,\n" +
                                "       SUD.UNIT_PRICE,\n" +
                                "       SUD.QUANTITY * SUD.UNIT_PRICE AS AMOUNT\n" +
                                "  FROM STOCK_MASTER_SOLD_UTILIZED SMS\n" +
                                " INNER JOIN STOCK_SOLD_UTILIZED_DETAILS SUD\n" +
                                "    ON SMS.SALES_ID = SUD.SALES_ID\n" +
                                " INNER JOIN STOCK_ITEM SIT\n" +
                                "    ON SUD.ITEM_ID = SIT.ITEM_ID\n" +
                                " INNER JOIN ASSET_LOCATION ASL\n" +
                                "    ON SUD.LOCATION_ID = ASL.LOCATION_ID\n" +
                                " WHERE SMS.SALES_ID IN (?SALES_ID)";
                        break;
                    }
                case SQLCommand.StockSalesDetail.Delete:
                    {
                        query = "DELETE FROM STOCK_SOLD_UTILIZED_DETAILS WHERE SALES_ID =?SALES_ID";
                        break;
                    }
                case SQLCommand.StockSalesDetail.Fetch:
                    {
                        //query = "SELECT SALES_ID, ITEM_ID, LOCATION_ID, QUANTITY, UNIT_PRICE, AMOUNT,QUANTITY AS 'AVAIL_QUANTITY'\n" +
                        //        "  FROM STOCK_SOLD_UTILIZED_DETAILS \n" +
                        //        "  WHERE SALES_ID =?SALES_ID";
                        query = "SELECT T.SALES_ID,T.PROJECT_ID,\n" +
                                "       ISV.BALANCE_DATE AS BALANCE_DATE,\n" +
                                "       T.ITEM_ID,\n" +
                                "       T.LOCATION_ID,\n" +
                                "       T.QUANTITY, T.QUANTITY as TEMP_QUANTITY,\n" +
                                "       T.UNIT_PRICE,T.ACCOUNT_LEDGER_ID,T.DISPOSAL_LEDGER_ID,T.AMOUNT,\n" +
                                "       ISV.QUANTITY     AS AVAIL_QUANTITY,ASU.SYMBOL\n" +
                                "  FROM INVENTORY_STOCK ISV\n" +
                                "\n" +
                                "  JOIN (SELECT SMS.PROJECT_ID,\n" +
                                "               SMS.SALES_DATE AS BALANCE_DATE,\n" +
                                "               SMS.SALES_ID,\n" +
                                "               SSU.ITEM_ID,\n" +
                                "               SSU.LOCATION_ID,\n" +
                                "               SSU.QUANTITY,SSU.ACCOUNT_LEDGER_ID,SSU.DISPOSAL_LEDGER_ID,\n" +
                                "               UNIT_PRICE,AMOUNT\n" +
                                "          FROM STOCK_MASTER_SOLD_UTILIZED SMS\n" +
                                "         INNER JOIN STOCK_SOLD_UTILIZED_DETAILS SSU\n" +
                                "            ON SMS.SALES_ID = SSU.SALES_ID\n" +
                                "         WHERE SMS.SALES_ID =?SALES_ID) AS T\n" +
                                "    ON T.PROJECT_ID = ISV.PROJECT_ID\n" +
                                "   AND T.ITEM_ID = ISV.ITEM_ID\n" +
                                "   AND T.LOCATION_ID = ISV.LOCATION_ID\n" +
                                "   AND T.BALANCE_DATE = ISV.BALANCE_DATE\n" +
                                "  LEFT JOIN STOCK_ITEM SI \n" +
                                "     ON T.ITEM_ID=SI.ITEM_ID \n" +
                                "  LEFT JOIN UOM ASU \n" +
                                "      ON SI.UNIT_ID=ASU.UOM_ID \n" +
                                " ORDER BY BALANCE_DATE DESC";

                        break;
                    }
                case SQLCommand.StockSalesDetail.FetchDetailsbeforeDelete:
                    {
                        query = "SELECT SMS.PROJECT_ID,SMS.SALES_DATE AS BALANCE_DATE,SMS.SALES_ID, ITEM_ID, LOCATION_ID, QUANTITY,UNIT_PRICE\n" +
                               "  FROM STOCK_MASTER_SOLD_UTILIZED SMS\n" +
                                " INNER JOIN STOCK_SOLD_UTILIZED_DETAILS SSU\n" +
                                "    ON SMS.SALES_ID = SSU.SALES_ID\n" +
                                "  WHERE SMS.SALES_ID =?SALES_ID";
                        break;
                    }
            }
            return query;
        }
        #endregion

    }
}
