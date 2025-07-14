using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class DenominationSql : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.Denomination).FullName)
            {
                query = GetDenomination();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script

        private string GetDenomination()
        {
            string query = "";
            SQLCommand.Denomination sqlCommandId = (SQLCommand.Denomination)(dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.Denomination.Add:
                    {

                        query = "INSERT INTO VOUCHER_DENOMINATION_TRANS (\n"+
                               "VOUCHER_ID,"+
                               "SEQUENCE_NO,"+
                               "LEDGER_ID,"+
                               "DENOMINATION_ID,"+
                               "COUNT,"+
                               "AMOUNT) VALUES("+
                               "?VOUCHER_ID,"+
                               "?SEQUENCE_ID,"+
                               "?LEDGER_ID, " +
                               "?DENOMINATION_ID," +
                               "?COUNT," +
                               "?AMOUNT)";
                        break;
                    }
                case SQLCommand.Denomination.FetchDenomination:
                    {
                        query = "SELECT DENOMINATION_ID,DENOMINATION,'X'AS MULTIPLE FROM DENOMINATION ORDER BY DENOMINATION_ID DESC";
                        break;
                    }
                case SQLCommand.Denomination.FetchDenominationByID:
                    {
                        query = "SELECT DN.DENOMINATION_ID,\n" +
                                "       DN.DENOMINATION,\n" +
                                "       VOUCHER_ID,\n" +
                                "       'X' AS MULTIPLE,\n" +
                                "       T.COUNT,\n" +
                                "       T.AMOUNT\n" +
                                "  FROM DENOMINATION DN\n" +
                                "  LEFT JOIN (SELECT DN.DENOMINATION_ID,\n" +
                                "                    DN.DENOMINATION,\n" +
                                "                    VOUCHER_ID,\n" +
                                "                    'X' AS MULTIPLE,\n" +
                                "                    COUNT,\n" +
                                "                    VDT.AMOUNT\n" +
                                "               FROM DENOMINATION DN\n" +
                                "               LEFT JOIN VOUCHER_DENOMINATION_TRANS VDT\n" +
                                "                 ON DN.DENOMINATION_ID = VDT.DENOMINATION_ID\n" +
                                "              WHERE VOUCHER_ID =?VOUCHER_ID AND VDT.LEDGER_ID=?LEDGER_ID\n" +
                                "              ORDER BY DENOMINATION_ID DESC) AS T\n" +
                                "    ON DN.DENOMINATION_ID = T.DENOMINATION_ID\n" +
                                " GROUP BY DENOMINATION_ID\n" +
                                " ORDER BY DENOMINATION_ID DESC";
                        break;
                    }
                case SQLCommand.Denomination.DeleteDenomination:
                    {
                        query = "DELETE FROM VOUCHER_DENOMINATION_TRANS WHERE VOUCHER_ID =?VOUCHER_ID";
                        break;
                    }
            }
            return query;
        }

        #endregion
    }
}
