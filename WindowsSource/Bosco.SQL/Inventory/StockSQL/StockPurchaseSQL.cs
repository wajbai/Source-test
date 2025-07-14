using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class StockPurchaseSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.StockMasterPurchase).FullName)
            {
                query = GetStockMasterPurchase();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        #region Stock Master Purchase
        public string GetStockMasterPurchase()
        {
            string query = "";
            SQLCommand.StockMasterPurchase SqlcommandId = (SQLCommand.StockMasterPurchase)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.StockMasterPurchase.Add:
                    {
                        query = "INSERT INTO STOCK_MASTER_PURCHASE (" +
                                "PROJECT_ID," +
                                "PURCHASE_DATE," +
                                "VOUCHER_NO," +
                                "PURCHASE_ORDER_NO," +
                                "VENDOR_ID," +
                                "DISCOUNT_PER," +
                                "DISCOUNT," +
                                "OTHER_CHARGES," +
                                "TAX," +
                                "TAX_AMOUNT," +
                                "NET_PAY," +
                                "LEDGER_ID," +
                                "NAME_ADDRESS," +
                                "NARRATION,TRANS_TYPE,VOUCHER_ID )VALUES(" +
                                "?PROJECT_ID," +
                                "?PURCHASE_DATE," +
                                "?VOUCHER_NO, " +
                                "?PURCHASE_ORDER_NO," +
                                "?VENDOR_ID, " +
                                "?DISCOUNT_PER," +
                                "?DISCOUNT," +
                                "?OTHER_CHARGES," +
                                "?TAX," +
                                "?TAX_AMOUNT," +
                                "?NET_PAY," +
                                "?LEDGER_ID," +
                                "?NAME_ADDRESS," +
                                "?NARRATION,?TRANS_TYPE,?VOUCHER_ID)";
                        break;
                    }

                case SQLCommand.StockMasterPurchase.Update:
                    {
                        query = "UPDATE STOCK_MASTER_PURCHASE SET " +
                                "PROJECT_ID=?PROJECT_ID," +
                                "PURCHASE_DATE=?PURCHASE_DATE," +
                                "VOUCHER_NO=?VOUCHER_NO," +
                                "PURCHASE_ORDER_NO=?PURCHASE_ORDER_NO," +
                                "VENDOR_ID=?VENDOR_ID," +
                                "DISCOUNT_PER=?DISCOUNT_PER," +
                                "DISCOUNT=?DISCOUNT," +
                                "OTHER_CHARGES=?OTHER_CHARGES," +
                                "TAX=?TAX ," +
                                "TAX_AMOUNT=?TAX_AMOUNT," +
                                "NET_PAY=?NET_PAY," +
                                "LEDGER_ID=?LEDGER_ID," +
                                "NAME_ADDRESS=?NAME_ADDRESS," +
                                "NARRATION=?NARRATION, " +
                                "TRANS_TYPE=?TRANS_TYPE, " +
                                "VOUCHER_ID=?VOUCHER_ID " +
                                " WHERE PURCHASE_ID =?PURCHASE_ID";
                        break;
                    }

                case SQLCommand.StockMasterPurchase.FetchAll:
                    {
                        query = "SELECT SMP.PURCHASE_ID,\n" +
                        "       SMP.PURCHASE_DATE,\n" +
                        "       ML.GROUP_ID,\n" +
                        "       CASE\n" +
                        "         WHEN ML.GROUP_ID = 12 THEN\n" +
                        "          CONCAT(ML.LEDGER_NAME, ' - ', MB.BANK, ' - ', MB.BRANCH)\n" +
                        "         ELSE\n" +
                        "          'Cash'\n" +
                        "       END AS 'LEDGER_NAME',\n" +
                        "       ASV.VENDOR,\n" +
                        "       DISCOUNT,\n" +
                        "       VOUCHER_NO,\n" +
                        "       NET_PAY,\n" +
                        "       NARRATION,TRANS_TYPE,VOUCHER_ID\n" +
                        "  FROM STOCK_MASTER_PURCHASE AS SMP\n" +
                        "  LEFT JOIN ASSET_STOCK_VENDOR AS ASV\n" +
                        "    ON SMP.VENDOR_ID = ASV.VENDOR_ID\n" +
                        "  LEFT JOIN MASTER_LEDGER AS ML\n" +
                        "    ON SMP.LEDGER_ID = ML.LEDGER_ID\n" +
                        "  LEFT JOIN MASTER_LEDGER_GROUP AS MLG\n" +
                        "    ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                        "  LEFT JOIN MASTER_BANK_ACCOUNT AS MBA\n" +
                        "    ON ML.LEDGER_ID = MBA.LEDGER_ID\n" +
                        "  LEFT JOIN MASTER_BANK AS MB\n" +
                        "    ON MBA.BANK_ID = MB.BANK_ID\n" +
                        " WHERE PROJECT_ID = ?PROJECT_ID\n" +
                        "   AND PURCHASE_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED AND TRANS_TYPE=?TRANS_TYPE";
                        break;
                    }
                case SQLCommand.StockMasterPurchase.Delete:
                    {
                        query = "DELETE FROM STOCK_MASTER_PURCHASE WHERE PURCHASE_ID =?PURCHASE_ID";
                        break;
                    }
                case SQLCommand.StockMasterPurchase.Fetch:
                    {
                        query = "SELECT  PURCHASE_ID,\n" +
                        "            PURCHASE_DATE,\n" +
                        "            VOUCHER_NO,\n" +
                        "            PURCHASE_ORDER_NO,\n" +
                        "            VENDOR_ID,\n" +
                        "            DISCOUNT_PER,\n" +
                        "            DISCOUNT,\n" +
                        "            OTHER_CHARGES,\n" +
                        "            TAX,\n" +
                        "            TAX_AMOUNT,\n" +
                        "            NET_PAY,\n" +
                        "            LEDGER_ID,\n" +
                        "            NAME_ADDRESS,\n" +
                        "            NARRATION,TRANS_TYPE,VOUCHER_ID FROM STOCK_MASTER_PURCHASE WHERE PURCHASE_ID =?PURCHASE_ID";
                        break;
                    }

                case SQLCommand.StockMasterPurchase.FetchNameAddress:
                    {
                        query = "SELECT NAME_ADDRESS,VOUCHER_ID FROM STOCK_MASTER_PURCHASE";
                        break;
                    }

                case SQLCommand.StockMasterPurchase.AutoFetchNarration:
                    {
                        query = "SELECT NARRATION,VOUCHER_ID FROM STOCK_MASTER_PURCHASE";
                        break;
                    }

                case SQLCommand.StockMasterPurchase.FetchLedgerId:
                    {
                        query = "SELECT INCOME_LEDGER_ID,EXPENSE_LEDGER_ID FROM STOCK_ITEM WHERE ITEM_ID=?ITEM_ID";
                        break;
                    } 
            }
            return query;
        }
        #endregion
    }
}
