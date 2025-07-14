using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class StockSalesSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.StockMasterSales).FullName)
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
            SQLCommand.StockMasterSales SqlcommandId = (SQLCommand.StockMasterSales)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.StockMasterSales.Add:
                    {
                        query = "INSERT INTO STOCK_MASTER_SOLD_UTILIZED(\n" +
                                "   SALES_REF_NO,\n" +
                                "   CUSTOMER_NAME,\n" +
                                "   SALES_DATE,\n" +
                                "   DISCOUNT,\n" +
                                "   OTHER_CHARGES,\n" +
                                "   TAX,TAX_AMOUNT,\n" +
                                "   NET_PAY,\n" +
                                "   LEDGER_ID,\n" +
                                "   NAME_ADDRESS,\n" +
                                "   NARRATION,\n" +
                                "   TRANS_TYPE,VOUCHER_NO,PROJECT_ID,DISCOUNT_PER,VOUCHER_ID)\n" +
                                "VALUES(\n" +
                                "?SALES_REF_NO, " +
                                "?CUSTOMER_NAME," +
                                "?SALES_DATE, " +
                                "?DISCOUNT," +
                                "?OTHER_CHARGES," +
                                "?TAX,?TAMOUNT," +
                                "?NET_PAY," +
                                "?LEDGER_ID," +
                                "?NAME_ADDRESS," +
                                "?NARRATION,?TRANS_TYPE,?VOUCHER_NO,?PROJECT_ID,?DISCOUNT_PER,?VOUCHER_ID)";
                        break;
                    }

                case SQLCommand.StockMasterSales.Update:
                    {
                        query = "UPDATE STOCK_MASTER_SOLD_UTILIZED SET " +
                                "SALES_REF_NO=?SALES_REF_NO, " +
                                "CUSTOMER_NAME=?CUSTOMER_NAME," +
                                "SALES_DATE=?SALES_DATE, " +
                                "DISCOUNT=?DISCOUNT," +
                                "OTHER_CHARGES=?OTHER_CHARGES," +
                                "TAX=?TAX ," +
                                "TAX_AMOUNT=?TAMOUNT ," +
                                "NET_PAY=?NET_PAY," +
                                "LEDGER_ID=?LEDGER_ID," +
                                "NAME_ADDRESS=?NAME_ADDRESS," +
                                "NARRATION=?NARRATION," +
                                "TRANS_TYPE=?TRANS_TYPE," +
                                "VOUCHER_NO=?VOUCHER_NO," +
                                "PROJECT_ID=?PROJECT_ID," +
                                "DISCOUNT_PER=?DISCOUNT_PER," +
                                "VOUCHER_ID=?VOUCHER_ID" +
                                " WHERE SALES_ID =?SALES_ID";
                        break;
                    }

                case SQLCommand.StockMasterSales.FetchAll:
                    {
                        query = "SELECT SALES_ID,SALES_REF_NO AS REF_NO,\n" +
                                "       CUSTOMER_NAME,\n" +
                                "       SALES_DATE,\n" +
                                "       DISCOUNT,DISCOUNT_PER,\n" +
                                "       OTHER_CHARGES,\n" +
                                "       TAX,TAX_AMOUNT,\n" +
                                "       NET_PAY,\n" +
                                "       CASE\n" +
                                "         WHEN TRANS_TYPE = 0 THEN\n" +
                                "          'SOLD'\n" +
                                "         WHEN TRANS_TYPE = 1 THEN\n" +
                                "          'UTILIZED'\n" +
                                "         WHEN TRANS_TYPE = 2 THEN\n" +
                                "          'DISPOSAL'\n" +
                                "       END AS SALES_TYPE,VOUCHER_NO,PROJECT_ID,VOUCHER_ID\n" +
                                "  FROM STOCK_MASTER_SOLD_UTILIZED WHERE PROJECT_ID=?PROJECT_ID AND SALES_DATE BETWEEN ?DATE_FROM AND ?DATE_TO AND TRANS_TYPE IN (?TRANS_TYPE)";

                        break;
                    }
                case SQLCommand.StockMasterSales.Delete:
                    {
                        query = "DELETE FROM STOCK_MASTER_SOLD_UTILIZED WHERE SALES_ID =?SALES_ID";
                        break;
                    }
                case SQLCommand.StockMasterSales.Fetch:
                    {
                        query = "SELECT \n" +
                                "  SMU.SALES_ID,\n" +
                                "   SALES_REF_NO,\n" +
                                "   CUSTOMER_NAME,\n" +
                                "   SALES_DATE,\n" +
                                "   DISCOUNT,DISCOUNT_PER,\n" +
                                "   OTHER_CHARGES,\n" +
                                "   TAX,TAX_AMOUNT,\n" +
                                "   NET_PAY,\n" +
                                "   LEDGER_ID,\n" +
                                "   NAME_ADDRESS,\n" +
                                "   NARRATION,\n" +
                                "   TRANS_TYPE,VOUCHER_NO,PROJECT_ID,ACCOUNT_LEDGER_ID,DISPOSAL_LEDGER_ID,\n" +
                                "VOUCHER_ID FROM  \n" +
                                " STOCK_MASTER_SOLD_UTILIZED SMU \n" +
                                "INNER JOIN STOCK_SOLD_UTILIZED_DETAILS SD\n" +
                                "ON SMU.SALES_ID=SD.SALES_ID\n" +
                                "WHERE SMU.SALES_ID =?SALES_ID";
                        break;
                    }
                case SQLCommand.StockMasterSales.AutoFetchCustomer:
                    {
                        query = "SELECT CUSTOMER_NAME,VOUCHER_ID FROM STOCK_MASTER_SOLD_UTILIZED";
                        break;
                    }

                case SQLCommand.StockMasterSales.FetchSalesIdByVoucherId:
                    {
                        query = "SELECT SALES_ID FROM STOCK_MASTER_SOLD_UTILIZED WHERE VOUCHER_ID=?VOUCHER_ID;";
                        break;
                    }

                case SQLCommand.StockMasterSales.AutoFetchNameAddress:
                    {
                        query = "SELECT NAME_ADDRESS,VOUCHER_ID FROM STOCK_MASTER_SOLD_UTILIZED";
                        break;
                    }

                case SQLCommand.StockMasterSales.AutoFetchNarration:
                    {
                        query = "SELECT NARRATION,VOUCHER_ID FROM STOCK_MASTER_SOLD_UTILIZED";
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}
