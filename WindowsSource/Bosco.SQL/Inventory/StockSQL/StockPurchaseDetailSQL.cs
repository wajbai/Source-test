using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.DAO;
using Bosco.DAO.Schema;
using Bosco.DAO.Data;

namespace Bosco.SQL
{
    class StockPurchaseDetailSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.StockPurchaseDetail).FullName)
            {
                query = GetStockPurchaseDetail();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        #region Stock Master Purchase
        public string GetStockPurchaseDetail()
        {
            string query = "";
            SQLCommand.StockPurchaseDetail SqlcommandId = (SQLCommand.StockPurchaseDetail)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.StockPurchaseDetail.Add:
                    {
                        //chinna on 09.01.2021
                        query = "INSERT INTO STOCK_PURCHASE_DETAILS (" +
                                "PURCHASE_ID," +
                                "ITEM_ID," +
                                "LOCATION_ID," +
                                "UNIT_PRICE," +
                                "QUANTITY ," +
                                "AMOUNT," +
                                "ACCOUNT_LEDGER_ID)VALUES(" +
                                "?PURCHASE_ID," +
                                "?ITEM_ID, " +
                                "?LOCATION_ID," +
                                "?UNIT_PRICE, " +
                                "?QUANTITY," +
                                "?AMOUNT," +
                                "?ACCOUNT_LEDGER_ID)";


                        break;
                    }

                case SQLCommand.StockPurchaseDetail.Update:
                    {
                        query = "UPDATE STOCK_PURCHASE_DETAILS SET " +
                                "PURCHASE_ID=?PURCHASE_ID," +
                                "ITEM_ID=?ITEM_ID," +
                                "LOCATION_ID=?LOCATION_ID," +
                                "UNIT_PRICE=?UNIT_PRICE," +
                                "QUANTITY=?QUANTITY" +
                                "ACCOUNT_LEDGER_ID=?ACCOUNT_LEDGER_ID" +
                                "WHERE PURCHASE_ID =?PURCHASE_ID";
                        break;
                    }

                case SQLCommand.StockPurchaseDetail.FetchAll:
                    {
                        query = "SELECT SMP.PURCHASE_ID,\n" +
                        "       SPD.ITEM_ID,\n" +
                        "       SI.NAME,\n" +
                        "       ASL.LOCATION_ID,\n" +
                        "       ASL.LOCATION,\n" +
                        "       UNIT_PRICE,\n" +
                        "       SMP.DISCOUNT,\n" +
                        "       SMP.OTHER_CHARGES,\n" +
                        "       SMP.TAX,\n" +
                        "       SMP.TAX_AMOUNT,\n" +
                        "       SPD.QUANTITY\n" +
                        "  FROM STOCK_PURCHASE_DETAILS AS SPD\n" +
                        "  LEFT JOIN STOCK_MASTER_PURCHASE AS SMP\n" +
                        "    ON SPD.PURCHASE_ID=SMP.PURCHASE_ID\n" +
                        "  LEFT JOIN STOCK_ITEM AS SI\n" +
                        "    ON SPD.ITEM_ID = SI.ITEM_ID\n" +
                        "  LEFT JOIN ASSET_LOCATION AS ASL\n" +
                        "    ON SPD.LOCATION_ID = ASL.LOCATION_ID\n" +
                        " WHERE SPD.PURCHASE_ID IN(?PURCHASE_ID)";

                        break;
                    }
                case SQLCommand.StockPurchaseDetail.Delete:
                    {
                        query = "DELETE FROM STOCK_PURCHASE_DETAILS WHERE PURCHASE_ID =?PURCHASE_ID";
                        break;
                    }
                case SQLCommand.StockPurchaseDetail.Fetch:
                    {
                        query = "SELECT T.PROJECT_ID,\n" +
                        "       ISV.BALANCE_DATE AS BALANCE_DATE,\n" +
                        "       T.ITEM_ID,\n" +
                        "       T.LOCATION_ID,\n" +
                        "       T.QUANTITY,\n" +
                        "       T.UNIT_PRICE,\n" +
                        "       T.AMOUNT,\n" +
                        "       ISV.QUANTITY     AS AVAIL_QUANTITY,\n" +
                        "       T.QUANTITY AS TEMP_QUANTITY,\n" +
                        "       T.ACCOUNT_LEDGER_ID,\n" +
                        "       T.REORDER,T.SYMBOL\n" +
                        "  FROM INVENTORY_STOCK ISV\n" +
                        "\n" +
                        "  JOIN (SELECT SMP.PROJECT_ID,\n" +
                        "               SMP.PURCHASE_DATE AS BALANCE_DATE,\n" +
                        "               SPD.PURCHASE_ID,\n" +
                        "               SPD.ITEM_ID,\n" +
                        "               SPD.LOCATION_ID,\n" +
                        "               SPD.QUANTITY,\n" +
                        "               SPD.ACCOUNT_LEDGER_ID,\n" +
                        "               UNIT_PRICE,\n" +
                        "               AMOUNT,\n" +
                        "               SI.REORDER,ASU.SYMBOL\n" +
                        "          FROM STOCK_MASTER_PURCHASE AS SMP\n" +
                        "         INNER JOIN STOCK_PURCHASE_DETAILS AS SPD\n" +
                        "            ON SMP.PURCHASE_ID = SPD.PURCHASE_ID\n" +
                        "          LEFT JOIN STOCK_ITEM AS SI\n" +
                        "             ON SPD.ITEM_ID=SI.ITEM_ID\n" +
                        "          LEFT JOIN UOM ASU \n" +
                        "             ON SI.UNIT_ID=ASU.UOM_ID \n" +
                        "         WHERE SMP.PURCHASE_ID = ?PURCHASE_ID) AS T\n" +
                        "    ON T.PROJECT_ID = ISV.PROJECT_ID\n" +
                        "   AND T.ITEM_ID = ISV.ITEM_ID\n" +
                        "   AND T.LOCATION_ID = ISV.LOCATION_ID\n" +
                        "   AND T.BALANCE_DATE = ISV.BALANCE_DATE\n" +
                        " ORDER BY BALANCE_DATE DESC";
                        break;
                    }
                case SQLCommand.StockPurchaseDetail.FetchPurchaseById:
                    {
                        query = "SELECT SMP.PROJECT_ID ,\n" +
                        "       SMP.PURCHASE_DATE AS BALANCE_DATE,\n" +
                        "       SPD.LOCATION_ID,\n" +
                        "       SPD.ITEM_ID,\n" +
                        "       SPD.UNIT_PRICE,\n" +
                        "       SPD.QUANTITY\n" +
                        "  FROM STOCK_MASTER_PURCHASE AS SMP\n" +
                        " INNER JOIN STOCK_PURCHASE_DETAILS AS SPD\n" +
                        "    ON SMP.PURCHASE_ID = SPD.PURCHASE_ID\n" +
                        " WHERE SMP.PURCHASE_ID = ?PURCHASE_ID";
                        break;
                    }
                case SQLCommand.StockPurchaseDetail.FetchPurchaseIdByVoucherId:
                    {
                        query = "SELECT PURCHASE_ID FROM STOCK_MASTER_PURCHASE WHERE VOUCHER_ID=?VOUCHER_ID;";
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}
