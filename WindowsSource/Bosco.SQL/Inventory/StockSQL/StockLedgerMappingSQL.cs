using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.DAO;

namespace Bosco.SQL
{
    public class StockLedgerMappingSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.StockLedgerMapping).FullName)
            {
                query = GetStockLedgerSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        #region SQL Script
        public string GetStockLedgerSQL()
        {
            string query = "";
            SQLCommand.StockLedgerMapping SqlcommandId = (SQLCommand.StockLedgerMapping)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.StockLedgerMapping.FetchStockLedgers:
                    {
                        query = "SELECT NAME, LEDGER_ID FROM STOCK_LEDGER";
                        break;
                    }
                case SQLCommand.StockLedgerMapping.SaveStockMappedLedgers:
                    {
                        query = "INSERT INTO STOCK_LEDGER ( " +
                               "NAME, " +
                               "LEDGER_ID ) VALUES( " +
                               "?Name, " +
                               "?LEDGER_ID) ON DUPLICATE KEY UPDATE LEDGER_ID=?LEDGER_ID";
                        break;
                    }

                case SQLCommand.StockLedgerMapping.MapLedgerToAllItems:
                    {
                        query = "UPDATE STOCK_ITEM\n" +
                                "   SET INCOME_LEDGER_ID = ?INCOME_LEDGER_ID,\n" +
                                "   EXPENSE_LEDGER_ID = ?EXPENSE_LEDGER_ID\n" +
                                " WHERE INCOME_LEDGER_ID = 0\n" +
                                "   OR EXPENSE_LEDGER_ID = 0";
                                //"or INCOME_LEDGER_ID = 0\n" +
                                //"or EXPENSE_LEDGER_ID=0";
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}
