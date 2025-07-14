using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class DashBoardSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.DashBoard).FullName)
            {
                query = GetDashBoardSQL();
            }
            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        private string GetDashBoardSQL()
        {
            string Query = "";
            SQLCommand.DashBoard sqlCommandId = (SQLCommand.DashBoard)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                case SQLCommand.DashBoard.FetchChartInfo:
                    {
                        Query = "SELECT T.YEAR,\n" +
                              "       T.MONTH,\n" +
                              "       T.MONTH_NAME,\n" +
                              "       SUM(RECEIPT) AS RECEIPT,\n" +
                              "       SUM(PAYMENT) AS PAYMENT\n" +
                              "  FROM (SELECT YEAR(MONTH_YEAR) AS 'YEAR',\n" +
                              "               MONTH(MONTH_YEAR) AS 'MONTH',\n" +
                              "               CONCAT(LEFT(MONTHNAME(MONTH_YEAR), 3), '-', YEAR(MONTH_YEAR)) AS MONTH_NAME,\n" +
                              "               0 AS RECEIPT,\n" +
                              "  IFNULL(SUM( IF(VT.TRANS_MODE = 'DR', VT.AMOUNT, -VT.AMOUNT)*IF(VMT.IS_MULTI_CURRENCY=1, VMT.EXCHANGE_RATE, 1)  ),0) AS PAYMENT\n" +
                              "\n" +
                              "          FROM (SELECT (?DATE_FROM - INTERVAL\n" +
                              "                        DAYOFMONTH(?DATE_FROM) - 1 DAY) + INTERVAL NO_OF_MONTH MONTH AS MONTH_YEAR,\n" +
                              "                       NO_OF_MONTH\n" +
                              "                  FROM (SELECT @rownum1 := @rownum1 + 1 AS NO_OF_MONTH\n" +
                              "                          FROM (SELECT 1 UNION\n" +
                              "                                        SELECT 2 UNION\n" +
                              "                                                SELECT 3 UNION\n" +
                              "                                                        SELECT 4\n" +
                              "\n" +
                              "\n" +
                              "\n" +
                              "\n" +
                              "                                ) AS T1,\n" +
                              "                               (SELECT 1 UNION\n" +
                              "                                        SELECT 2 UNION\n" +
                              "                                                SELECT 3 UNION\n" +
                              "                                                        SELECT 4\n" +
                              "\n" +
                              "\n" +
                              "\n" +
                              "\n" +
                              "                                ) AS T2,\n" +
                              "                               (SELECT 1 UNION\n" +
                              "                                        SELECT 2 UNION\n" +
                              "                                                SELECT 3 UNION\n" +
                              "                                                        SELECT 4\n" +
                              "\n" +
                              "\n" +
                              "\n" +
                              "\n" +
                              "                                ) AS T3,\n" +
                              "                               (SELECT @rownum1 := -1) AS T0) D1) D2\n" +
                              "          LEFT JOIN PROJECT_LEDGER AS PL\n" +
                              "         INNER JOIN MASTER_LEDGER AS ML\n" +
                              "         INNER JOIN MASTER_LEDGER_GROUP AS MLG\n" +
                              "            ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                              "         ON  PL.LEDGER_ID = ML.LEDGER_ID\n" +
                              "          LEFT JOIN VOUCHER_MASTER_TRANS AS VMT\n" +
                              "         INNER JOIN VOUCHER_TRANS AS VT\n" +
                              "            ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                              "            ON PL.PROJECT_ID = VMT.PROJECT_ID\n" +
                              "           AND PL.LEDGER_ID = VT.LEDGER_ID\n" +
                              "         WHERE D2.MONTH_YEAR <= ?DATE_TO\n" +
                              "           AND MLG.GROUP_ID NOT IN (12, 13) \n" +
                              "           AND PL.PROJECT_ID IN (?PROJECT_ID)\n" +
                              "           AND VMT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO \n" +
                              "           AND VMT.STATUS = 1\n" +
                              "           AND (VMT.VOUCHER_TYPE IN ('PY') OR (VMT.VOUCHER_TYPE ='CN' AND VMT.VOUCHER_SUB_TYPE= 'FD' AND VT.TRANS_MODE = 'DR'))\n" +
                              "           AND YEAR(D2.MONTH_YEAR) = YEAR(VMT.VOUCHER_DATE)\n" +
                              "           AND MONTH(D2.MONTH_YEAR) = MONTH(VMT.VOUCHER_DATE)\n" +
                              "         GROUP BY MONTH_YEAR\n" +
                              "        union\n" +
                              "        SELECT YEAR(MONTH_YEAR) AS 'YEAR',\n" +
                              "               MONTH(MONTH_YEAR) AS 'MONTH',\n" +
                              "               CONCAT(LEFT(MONTHNAME(MONTH_YEAR), 3), '-', YEAR(MONTH_YEAR)) AS MONTH_NAME,\n" +
                              "        IFNULL(SUM( IF(VT.TRANS_MODE = 'CR', VT.AMOUNT, -VT.AMOUNT)*IF(VMT.IS_MULTI_CURRENCY=1, VMT.EXCHANGE_RATE, 1) ),0) AS RECEIPT,\n" +
                              "               0 AS PAYMENT\n" +
                              "\n" +
                              "          FROM (SELECT (?DATE_FROM - INTERVAL\n" +
                              "                        DAYOFMONTH(?DATE_FROM) - 1 DAY) + INTERVAL NO_OF_MONTH MONTH AS MONTH_YEAR,\n" +
                              "                       NO_OF_MONTH\n" +
                              "                  FROM (SELECT @rownum := @rownum + 1 AS NO_OF_MONTH\n" +
                              "                          FROM (SELECT 1 UNION\n" +
                              "                                        SELECT 2 UNION\n" +
                              "                                                SELECT 3 UNION\n" +
                              "                                                        SELECT 4\n" +
                              "\n" +
                              "\n" +
                              "\n" +
                              "\n" +
                              "                                ) AS T1,\n" +
                              "                               (SELECT 1 UNION\n" +
                              "                                        SELECT 2 UNION\n" +
                              "                                                SELECT 3 UNION\n" +
                              "                                                        SELECT 4\n" +
                              "\n" +
                              "\n" +
                              "\n" +
                              "\n" +
                              "                                ) AS T2,\n" +
                              "                               (SELECT 1 UNION\n" +
                              "                                        SELECT 2 UNION\n" +
                              "                                                SELECT 3 UNION\n" +
                              "                                                        SELECT 4\n" +
                              "\n" +
                              "\n" +
                              "\n" +
                              "\n" +
                              "                                ) AS T3,\n" +
                              "                               (SELECT @rownum := -1) AS T0) D1) D2\n" +
                              "          LEFT JOIN PROJECT_LEDGER AS PL\n" +
                              "         INNER JOIN MASTER_LEDGER AS ML\n" +
                              "         INNER JOIN MASTER_LEDGER_GROUP AS MLG\n" +
                              "            ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                              "      ON   PL.LEDGER_ID = ML.LEDGER_ID\n" +
                              "          LEFT JOIN VOUCHER_MASTER_TRANS AS VMT\n" +
                              "         INNER JOIN VOUCHER_TRANS AS VT\n" +
                              "            ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                              "     ON    PL.PROJECT_ID = VMT.PROJECT_ID\n" +
                              "           AND PL.LEDGER_ID = VT.LEDGER_ID\n" +
                              "         WHERE D2.MONTH_YEAR <= ?DATE_TO\n" +
                              "           AND MLG.GROUP_ID NOT IN (12, 13) \n" +
                              "           AND PL.PROJECT_ID IN (?PROJECT_ID)\n" +
                              "           AND VMT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO  \n" +
                              "           AND VMT.STATUS = 1\n" +
                              "           AND (VMT.VOUCHER_TYPE IN ('RC') OR (VMT.VOUCHER_TYPE ='CN' AND VMT.VOUCHER_SUB_TYPE= 'FD' AND VT.TRANS_MODE = 'CR'))\n" +
                              "           AND YEAR(D2.MONTH_YEAR) = YEAR(VMT.VOUCHER_DATE)\n" +
                              "           AND MONTH(D2.MONTH_YEAR) = MONTH(VMT.VOUCHER_DATE)\n" +
                              "         GROUP BY MONTH_YEAR) as t\n" +
                              " WHERE T.RECEIPT>0 OR T.PAYMENT>0 GROUP BY T.YEAR, T.MONTH";

                        break;
                    }
                case SQLCommand.DashBoard.FetchMaturedFD:
                    {
                        //Query = "SELECT FD.FD_ACCOUNT_NUMBER,\n" +
                        //        "       CASE\n" +
                        //        "         WHEN T.RENEWAL_COUNT = 0 THEN\n" +
                        //        "          DATE(FD.MATURED_ON)\n" +
                        //        "         ELSE\n" +
                        //        "          DATE(T.MATURED_ON)\n" +
                        //        "       END AS MATURED_ON\n" +
                        //        "  FROM FD_ACCOUNT AS FD\n" +
                        //        " INNER JOIN (SELECT FDA.FD_ACCOUNT_NUMBER AS FD_ACCOUNT_NO,\n" +
                        //        "                    IF(FDA.AMOUNT - FDR.WITHDRAWAL_AMOUNT = 0, 0, 1) AS STATUS,\n" +
                        //        "                    MAX(FDR.MATURITY_DATE) AS MATURED_ON,\n" +
                        //        "                    COUNT(FDR.FD_ACCOUNT_ID) AS RENEWAL_COUNT\n" +
                        //        "               FROM FD_ACCOUNT AS FDA\n" +
                        //        "               LEFT JOIN FD_RENEWAL AS FDR\n" +
                        //        "                 ON FDA.FD_ACCOUNT_ID = FDR.FD_ACCOUNT_ID\n" +
                        //        "                AND FDA.PROJECT_ID = ?PROJECT_ID\n" +
                        //        "              GROUP BY FDA.FD_ACCOUNT_ID) AS T\n" +
                        //        "    ON FD.FD_ACCOUNT_NUMBER = T.FD_ACCOUNT_NO\n" +
                        //        " WHERE FD.STATUS = 1\n" +
                        //        "   AND FD.PROJECT_ID = ?PROJECT_ID AND T.STATUS=1\n" +
                        //        " AND FD.MATURED_ON<=?RECENTVOUCHER";

                        Query = "SELECT FD.FD_ACCOUNT_NUMBER,\n" +
                                "       CASE\n" +
                                "         WHEN T.RENEWAL_COUNT = 0 THEN\n" +
                                "          DATE(FD.MATURED_ON)\n" +
                                "         ELSE\n" +
                                "          DATE(T.MATURED_ON)\n" +
                                "       END AS MATURED_ON\n" +
                                "  FROM FD_ACCOUNT AS FD\n" +
                                " INNER JOIN (SELECT FDA.FD_ACCOUNT_NUMBER AS FD_ACCOUNT_NO,\n" +
                                "                    IF(FDA.AMOUNT - FDR.WITHDRAWAL_AMOUNT = 0, 0, 1) AS STATUS,\n" +
                                "                    MAX(FDR.MATURITY_DATE) AS MATURED_ON,\n" +
                                "                    COUNT(FDR.FD_ACCOUNT_ID) AS RENEWAL_COUNT\n" +
                                "               FROM FD_ACCOUNT AS FDA\n" +
                                "               LEFT JOIN FD_RENEWAL AS FDR\n" +
                                "                 ON FDA.FD_ACCOUNT_ID = FDR.FD_ACCOUNT_ID\n" +
                                "                AND FDA.PROJECT_ID = ?PROJECT_ID\n" +
                                "              GROUP BY FDA.FD_ACCOUNT_ID) AS T\n" +
                                "    ON FD.FD_ACCOUNT_NUMBER = T.FD_ACCOUNT_NO\n" +
                                " WHERE FD.STATUS = 1\n" +
                                "   AND FD.PROJECT_ID = ?PROJECT_ID\n" +
                                "   AND T.STATUS = 1 AND\n" +
                                " (CASE WHEN T.RENEWAL_COUNT = 0 THEN\n"+ //27/03/2018, FD.MATURED_ON 
                                "  DATE(FD.MATURED_ON)\n" +
                                " ELSE\n" +
                                "  DATE(T.MATURED_ON) END)\n" +
                                "BETWEEN\n" +
                                "       (SELECT DATE_FORMAT(NOW() - INTERVAL 1 MONTH, '%Y-%m-01')) AND\n" +
                                "       (SELECT LAST_DAY(DATE_FORMAT(NOW() + INTERVAL 1 MONTH, '%Y-%m-%d')));";

                        break;
                    }
                case SQLCommand.DashBoard.FetchDatabases:
                    {
                        Query = "SELECT db_Name AS Restore_Db FROM RESTORE_DB";
                        break;
                    }
                case SQLCommand.DashBoard.CheckDatabaseExists:
                    {
                        Query = "SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME=?DATABASE";
                        break;
                    }
                case SQLCommand.DashBoard.InsertRestoredDatabase:
                    {
                        Query = "INSERT INTO RESTORE_DB (db_Name) VALUES (?DATABASE)";
                        break;
                    }

                case SQLCommand.DashBoard.FetchProjectsbySociety:
                    {
                        Query= "SELECT MP.CUSTOMERID, MP.PROJECT_ID\n" +
                                "  FROM MASTER_PROJECT MP\n" +
                                "  JOIN (SELECT CUSTOMERID FROM MASTER_PROJECT WHERE PROJECT_ID IN (?PROJECT_ID)) AS T\n" + 
                                "    ON MP.CUSTOMERID = T.CUSTOMERID;";
                        break;
                    }

                case SQLCommand.DashBoard.DropDatabase:
                    {
                        Query = "DROP SCHEMA `?DRPDATABASE`";
                        break;
                    }
                case SQLCommand.DashBoard.UpdatePayrollSymbols:
                    {
                        Query = @"UPDATE PRCOMPONENT SET relatedcomponents =replace(relatedcomponents,'Ãª','ê');
                        UPDATE PRCOMPONENT SET EQUATION =replace(EQUATION,'Â',''),EQUATIONID =replace(EQUATIONID,'Â','');
                        UPDATE prcompmonth SET EQUATION =replace(EQUATION,'Â',''),EQUATIONID =replace(EQUATIONID,'Â','');";
                        break;
                    }
            }
            return Query;
        }
        #endregion
    }
}
