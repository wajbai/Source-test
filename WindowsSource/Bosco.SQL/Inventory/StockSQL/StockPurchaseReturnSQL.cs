using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    class StockPurchaseReturnSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.StockPurchaseReturns).FullName)
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
            SQLCommand.StockPurchaseReturns SqlcommandId = (SQLCommand.StockPurchaseReturns)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.StockPurchaseReturns.AddMasterPurchaseReturns:
                    {
                        query = " INSERT INTO STOCK_MASTER_PURCHASE_RETURNS\n" +
                                " (RETURN_TYPE, REASON, RETURN_DATE, LEDGER_ID,PROJECT_ID, NET_PAY,VOUCHER_ID)\n" +
                                " VALUES\n" +
                                " (?RETURN_TYPE, ?REASON, ?RETURN_DATE, ?LEDGER_ID, ?PROJECT_ID, ?NET_PAY,?VOUCHER_ID)";
                        break;
                    }


                case SQLCommand.StockPurchaseReturns.AddPruchaseReturnDetails:
                    {
                        query = " INSERT INTO STOCK_PURCHASE_RETURNS_DETAILS\n" +
                                "  (RETURN_ID,\n" +
                                "   ITEM_ID,\n" +
                                "   UNIT_PRICE,\n" +
                                "   QUANTITY,\n" +
                                "   LOCATION_ID,\n" +
                                "   VENDOR_ID,\n" +
                                "   TOTAL_AMOUNT,\n" +
                                "   ACCOUNT_LEDGER_ID)\n" +
                                " VALUES\n" +
                                "  (?RETURN_ID,\n" +
                                "   ?ITEM_ID,\n" +
                                "   ?UNIT_PRICE,\n" +
                                "   ?QUANTITY,\n" +
                                "   ?LOCATION_ID,\n" +
                                "   ?VENDOR_ID,\n" +
                                "   ?TOTAL_AMOUNT,\n" +
                                "   ?ACCOUNT_LEDGER_ID)";
                        break;
                    }

                case SQLCommand.StockPurchaseReturns.DeletePruchaseReturnDetails:
                    {
                        query = "DELETE FROM STOCK_PURCHASE_RETURNS_DETAILS WHERE RETURN_ID=?RETURN_ID";
                        break;
                    }

                case SQLCommand.StockPurchaseReturns.FetchPurchaseReturnIdByVoucherId:
                    {
                        query = "SELECT RETURN_ID FROM STOCK_MASTER_PURCHASE_RETURNS WHERE VOUCHER_ID =?VOUCHER_ID;";
                        break;
                    }

                case SQLCommand.StockPurchaseReturns.FetchPurchaseReturnsMasterDetails:
                    {
                        query = " SELECT MPR.RETURN_ID,\n" +
                                "       MPR.RETURN_DATE,\n" +
                                "       MPR.LEDGER_ID,\n" +
                                "       ML.LEDGER_NAME,\n" +
                                "       IF(MPR.RETURN_TYPE = 0, 'InWards', 'OutWards') AS RETURN_TYPE,\n" +
                                "       MPR.NET_PAY,\n" +
                                "       MPR.REASON,VOUCHER_ID\n" +
                                "  FROM STOCK_MASTER_PURCHASE_RETURNS AS MPR\n" +
                                " INNER JOIN MASTER_LEDGER AS ML\n" +
                                "    ON MPR.LEDGER_ID = ML.LEDGER_ID\n" +
                                " WHERE PROJECT_ID=?PROJECT_ID { AND MPR.RETURN_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED } { AND RETURN_ID=?RETURN_ID }";
                        break;
                    }
                case SQLCommand.StockPurchaseReturns.FetchPurchaseReturnDetails:
                    {
                        query = "SELECT PRD.RETURN_ID,\n" +
                                "       PRD.ITEM_ID,\n" +
                                "       SI.NAME AS ITEM_NAME,\n" +
                                "       PRD.UNIT_PRICE,\n" +
                                "       PRD.QUANTITY,\n" +
                                "       PRD.LOCATION_ID,\n" +
                                "       ASL.LOCATION,\n" +
                                "       PRD.VENDOR_ID,\n" +
                                "       ASV.VENDOR,\n" +
                                "       PRD.TOTAL_AMOUNT\n" +
                                "  FROM STOCK_PURCHASE_RETURNS_DETAILS AS PRD\n" +
                                " INNER JOIN STOCK_ITEM AS SI\n" +
                                "    ON PRD.ITEM_ID = SI.ITEM_ID\n" +
                                "  LEFT JOIN ASSET_LOCATION AS ASL\n" +
                                "    ON ASL.LOCATION_ID = PRD.LOCATION_ID\n" +
                                "  LEFT JOIN ASSET_STOCK_VENDOR AS ASV\n" +
                                "    ON ASV.VENDOR_ID = PRD.VENDOR_ID\n" +
                                "   WHERE PRD.RETURN_ID IN (?RETURN_ID)";
                        break;
                    }
                case SQLCommand.StockPurchaseReturns.DeletePurchaseReturnMaster:
                    {
                        query = "DELETE FROM STOCK_MASTER_PURCHASE_RETURNS WHERE RETURN_ID=?RETURN_ID";
                        break;
                    }
                case SQLCommand.StockPurchaseReturns.DeletePurchaseReturnDetails:
                    {
                        query = "DELETE FROM STOCK_PURCHASE_RETURNS_DETAILS WHERE RETURN_ID=?RETURN_ID";
                        break;
                    }
                case SQLCommand.StockPurchaseReturns.FetchPurchaseDetailsById:
                    {
                        query = "SELECT PRD.ITEM_ID,\n" +
                                "       PRD.LOCATION_ID,\n" +
                                "       PRD.VENDOR_ID,\n" +
                                "       PRD.QUANTITY,\n" +
                                "       PRD.UNIT_PRICE,\n" +
                                "       PRD.TOTAL_AMOUNT,\n" +
                                "       ACCOUNT_LEDGER_ID,\n" +
                                "       TT.QUANTITY AS AVAIL_QUANTITY, PRD.QUANTITY AS TEMP_QUANTITY\n" +
                                "  FROM STOCK_PURCHASE_RETURNS_DETAILS AS PRD\n" +
                                "  LEFT JOIN (SELECT ITEM_ID, QUANTITY\n" +
                                "               FROM (SELECT ITEM_ID, BALANCE_DATE, QUANTITY, RATE\n" +
                                "                       FROM INVENTORY_STOCK\n" +
                                "                      WHERE PROJECT_ID = ?PROJECT_ID\n" +
                                "                        AND BALANCE_DATE <= (select max(BALANCE_DATE) from inventory_stock)\n" +
                                "                        AND ITEM_ID IN (?ITEM_ID)\n" +
                                "                        AND LOCATION_ID IN (?LOCATION_ID)\n" +
                                "                      ORDER BY BALANCE_DATE DESC) AS T\n" +
                                "              GROUP BY ITEM_ID) AS TT\n" +
                                "    ON PRD.ITEM_ID = TT.ITEM_ID\n" +
                                " WHERE PRD.RETURN_ID IN (?RETURN_ID)";
                        break;
                    }
                case SQLCommand.StockPurchaseReturns.FetchQuantity:
                    {
                        query = "SELECT QUANTITY FROM STOCK_ITEM_DETAILS WHERE ITEM_ID=?ITEM_ID AND LOCATION_ID=?LOCATION_ID";
                        break;
                    }
                case SQLCommand.StockPurchaseReturns.UpdateMasterPurchaseReturns:
                    {
                        query = " UPDATE STOCK_MASTER_PURCHASE_RETURNS\n" +
                                "   SET RETURN_TYPE = ?RETURN_TYPE,\n" +
                                "       REASON      = ?REASON,\n" +
                                "       RETURN_DATE = ?RETURN_DATE,\n" +
                                "       LEDGER_ID   = ?LEDGER_ID,\n" +
                                "       NET_PAY     = ?NET_PAY,\n" +
                                "       VOUCHER_ID     = ?VOUCHER_ID\n" +
                                " WHERE RETURN_ID = ?RETURN_ID";
                        break;
                    }
                case SQLCommand.StockPurchaseReturns.FetchPurchaseById:
                    {
                        query = " SELECT MPR.PROJECT_ID,\n" +
                                "       MPR.RETURN_DATE AS BALANCE_DATE,\n" +
                                "       PRD.ITEM_ID,\n" +
                                "       PRD.LOCATION_ID,\n" +
                                "       PRD.QUANTITY,\n" +
                                "       PRD.UNIT_PRICE,VOUCHER_ID \n" +
                                "  FROM STOCK_MASTER_PURCHASE_RETURNS AS MPR\n" +
                                " INNER JOIN STOCK_PURCHASE_RETURNS_DETAILS AS PRD\n" +
                                "    ON MPR.RETURN_ID = PRD.RETURN_ID\n" +
                                " WHERE MPR.RETURN_ID=?RETURN_ID";
                        break;
                    }
                case SQLCommand.StockPurchaseReturns.FetchItemLocationById:
                    {
                        query = "SELECT ITEM_ID,LOCATION_ID FROM STOCK_PURCHASE_RETURNS_DETAILS WHERE RETURN_ID=?RETURN_ID";
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}
