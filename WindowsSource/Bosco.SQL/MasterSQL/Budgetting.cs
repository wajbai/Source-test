using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class Budgetting : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.Budget).FullName)
            {
                query = GetBudgetSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #region SQL Script
        private string GetBudgetSQL()
        {
            string query = "";
            SQLCommand.Budget sqlCommandId = (SQLCommand.Budget)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.Budget.FetchById:
                    {
                        query = "SELECT BUDGET_NAME, BUDGET_TYPE_ID, BM.PROJECT_ID, MP.PROJECT,DATE_FROM,DATE_TO,REMARKS,BM.IS_ACTIVE\n" +
                               "  FROM BUDGET_MASTER BM\n" +
                               " INNER JOIN MASTER_PROJECT MP\n" +
                               "    ON BM.PROJECT_ID = MP.PROJECT_ID\n" +
                               " WHERE BUDGET_ID = ?BUDGET_ID";
                        break;
                    }
                case SQLCommand.Budget.FetchAll:
                    {
                        query = "SELECT BUDGET_ID,\n" +
                                "       BUDGET_NAME,\n" +
                                "       MP.PROJECT,\n" +
                                "       BUDGET_TYPE_ID,\n" +
                                "       BM.PROJECT_ID,\n" +
                                "       DATE_FROM,\n" +
                                "       DATE_TO,\n" +
                                "       REMARKS,\n" +
                                "       IS_ACTIVE,\n" +
                                "       CASE\n" +
                                "         WHEN IS_ACTIVE = 0 THEN\n" +
                                "          \"Inactive\"\n" +
                                "         ELSE\n" +
                                "          \"Active\"\n" +
                                "       END AS STATUS\n" +
                                "  FROM BUDGET_MASTER BM\n" +
                                " INNER JOIN MASTER_PROJECT MP\n" +
                                "    ON BM.PROJECT_ID = MP.PROJECT_ID\n" +
                                "   ORDER BY STATUS,BUDGET_NAME ASC";

                        break;
                    }
                case SQLCommand.Budget.AddNewBudgetFetchLedger:
                    {
                        query = @"SELECT ML.LEDGER_CODE,MONTH(MONTH_YEAR) AS MONTH,
                                           DATE_FORMAT(MONTH_YEAR, '%b - %Y') AS DURATION,
                                           MLG.LEDGER_GROUP,
                                           ML.LEDGER_ID,
                                           ML.LEDGER_NAME,
                                           ML.LEDGER_CODE,
                                           if(MONTH(MONTH_YEAR) = 1,
                                              0.00,
                                              if(MONTH(MONTH_YEAR) = 2,
                                                 0.00,
                                                 if(MONTH(MONTH_YEAR) = 3,
                                                    0.00,
                                                    if(MONTH(MONTH_YEAR) = 4,
                                                       0.00,
                                                       if(MONTH(MONTH_YEAR) = 5,
                                                          0.00,
                                                          if(MONTH(MONTH_YEAR) = 6,
                                                             0.00,
                                                             if(MONTH(MONTH_YEAR) = 7,
                                                                0.00,
                                                                if(MONTH(MONTH_YEAR) = 8,
                                                                   0.00,
                                                                   if(MONTH(MONTH_YEAR) = 9,
                                                                      0.00,
                                                                      if(MONTH(MONTH_YEAR) = 10,
                                                                         0.00,
                                                                         if(MONTH(MONTH_YEAR) = 11,
                                                                            0.00,
                                                                            if(MONTH(MONTH_YEAR) = 12, 0.00, 0.00)))))))))))) as AMOUNT,
                                           IF(MLG.NATURE_ID IN (1, 4), 'CR', 'DR') AS TRANS_MODE
                                      FROM (SELECT (?DATE_FROM - INTERVAL
                                                    DAYOFMONTH(?DATE_FROM) - 1 DAY) + INTERVAL NO_OF_MONTH MONTH AS MONTH_YEAR,
                                                   NO_OF_MONTH
                                              FROM (SELECT @rownum := @rownum + 1 AS NO_OF_MONTH
                                                      FROM (SELECT 1 UNION
                                                                    SELECT 2 UNION
                                                                            SELECT 3 UNION
                                                                                    SELECT 4

                                                            ) AS T1,
                                                           (SELECT 1 UNION
                                                                    SELECT 2 UNION
                                                                            SELECT 3 UNION
                                                                                    SELECT 4

                                                            ) AS T2,

                                                           (SELECT @rownum := -1) AS T0) D1) D2
                                      LEFT JOIN PROJECT_LEDGER PL
                                      INNER JOIN MASTER_LEDGER ML
                                      INNER JOIN MASTER_LEDGER_GROUP MLG
                                          ON ML.GROUP_ID = MLG.GROUP_ID ON ML.LEDGER_ID = PL.LEDGER_ID
                                          AND PL.PROJECT_ID=?PROJECT_ID
                                         -- AND YEAR(D2.MONTH_YEAR)  = YEAR(?DATE_TO) 
                                         -- AND MONTH(D2.MONTH_YEAR) = MONTH(?DATE_TO)
                                     AND D2.MONTH_YEAR <= ?DATE_TO;";
                        break;
                    }
                case SQLCommand.Budget.FetchRecentBudgetList:
                    {
                        query = "SELECT BUDGET_ID  FROM BUDGET_MASTER BM WHERE PROJECT_ID = ?PROJECT_ID ORDER BY BM.BUDGET_ID DESC LIMIT 1";
                        break;
                    }

                case SQLCommand.Budget.FetchBudgetNames:
                    {
                        query = "SELECT BM.BUDGET_ID AS BANK_ID, BM.BUDGET_NAME AS BANK, BM.PROJECT_ID\n" +
                                    "  FROM BUDGET_MASTER BM\n" +
                                    " INNER JOIN MASTER_PROJECT MP\n" +
                                    "    ON MP.PROJECT_ID = BM.PROJECT_ID\n" +
                                    " GROUP BY BANK_ID\n" +
                                    " ORDER BY BANK";
                        break;
                    }

                case SQLCommand.Budget.Add:
                    {
                        query = "INSERT INTO BUDGET_MASTER\n" +
                                 "  (BUDGET_NAME, BUDGET_TYPE_ID, PROJECT_ID,DATE_FROM,DATE_TO, REMARKS, IS_ACTIVE)\n" +
                                 "VALUES\n" +
                                 "  (?BUDGET_NAME, ?BUDGET_TYPE_ID, ?PROJECT_ID, ?DATE_FROM, ?DATE_TO, ?REMARKS, ?IS_ACTIVE)";
                        break;
                    }
                case SQLCommand.Budget.Update:
                    {
                        query = " UPDATE BUDGET_MASTER\n" +
                                "   SET BUDGET_NAME    = ?BUDGET_NAME,\n" +
                                "       BUDGET_TYPE_ID = ?BUDGET_TYPE_ID,\n" +
                                "       PROJECT_ID     = ?PROJECT_ID,\n" +
                                "       DATE_FROM      = ?DATE_FROM,\n" +
                                "       DATE_TO        = ?DATE_TO,\n" +
                                "       REMARKS        = ?REMARKS,\n" +
                                "       IS_ACTIVE      = ?IS_ACTIVE\n" +
                                " WHERE BUDGET_ID = ?BUDGET_ID";
                        break;
                    }
                case SQLCommand.Budget.Delete:
                    {
                        query = "DELETE FROM BUDGET_MASTER WHERE BUDGET_ID = ?BUDGET_ID";
                        break;
                    }
                case SQLCommand.Budget.DeleteBudgetLedgerById:
                    {
                        query = "DELETE FROM BUDGET_LEDGER WHERE BUDGET_ID=?BUDGET_ID";
                        break;
                    }
                case SQLCommand.Budget.DeleteAllotFund:
                    {
                        query = "DELETE FROM ALLOT_FUND WHERE BUDGET_ID=?BUDGET_ID";
                        break;
                    }
                case SQLCommand.Budget.BudgetLedgerAdd:
                    {
                        query = "INSERT INTO BUDGET_LEDGER\n" +
                                "   (BUDGET_ID,LEDGER_ID,AMOUNT,TRANS_MODE)\n" +
                                "VALUES\n" +
                                "   (?BUDGET_ID,?LEDGER_ID,?AMOUNT,?TRANS_MODE)";
                        break;
                    }
                case SQLCommand.Budget.FetchMappedLedgers:
                    {
                        query = "SELECT ML.LEDGER_CODE,\n" +
                                "       PL.PROJECT_ID,\n" +
                                "       MP.PROJECT,\n" +
                                "       PL.LEDGER_ID,\n" +
                                "       ML.LEDGER_NAME,\n" +
                                "       MLG.LEDGER_GROUP,\n" +
                                "       IF(MLG.NATURE_ID IN (1, 4), 'CR', 'DR') AS TRANS_MODE,\n" +
                                "       0 AS AMOUNT\n" +
                                "  FROM project_ledger PL\n" +
                                " INNER JOIN MASTER_LEDGER ML\n" +
                                "    ON ML.LEDGER_ID = PL.LEDGER_ID\n" +
                                " INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                                "    ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                            // "   AND MLG.NATURE_ID IN (1, 2)\n" +
                                " INNER JOIN MASTER_PROJECT MP\n" +
                                "    ON MP.PROJECT_ID = PL.PROJECT_ID\n" +
                                "   AND PL.PROJECT_ID = ?PROJECT_ID;";

                        break;
                    }
                case SQLCommand.Budget.ChangeStatusToInActive:
                    {
                        query = "UPDATE BUDGET_MASTER SET IS_ACTIVE=0 WHERE PROJECT_ID=?PROJECT_ID";
                        break;
                    }
                case SQLCommand.Budget.FetchBudgetBalance:
                    {
                        query = "SELECT BM.BUDGET_ID, BL.LEDGER_ID, BL.AMOUNT, BL.TRANS_MODE\n" +
                              "  FROM BUDGET_MASTER BM\n" +
                              " INNER JOIN BUDGET_LEDGER BL\n" +
                              "    ON BL.BUDGET_ID = BM.BUDGET_ID\n" +
                              "   AND BL.LEDGER_ID = ?LEDGER_ID\n" +
                              "   AND BM.PROJECT_ID = ?PROJECT_ID\n" +
                              "   AND BM.IS_ACTIVE = 1;";
                        break;
                    }
                case SQLCommand.Budget.CheckBudgetByDate:
                    {
                        query = " SELECT *\n" +
                                "  FROM BUDGET_MASTER BM\n" +
                                " WHERE BM.PROJECT_ID = ?PROJECT_ID\n" +
                                "   AND BM.IS_ACTIVE = 1\n" +
                                "   AND ?VOUCHER_DATE BETWEEN BM.DATE_FROM AND BM.DATE_TO;";
                        break;
                    }
                case SQLCommand.Budget.AddAllotFund:
                    {
                        query = "INSERT INTO ALLOT_FUND " +
                              "(BUDGET_ID, " +
                               "LEDGER_ID, " +
                               "MONTH1, " +
                               "MONTH2, " +
                               "MONTH3, " +
                               "MONTH4, " +
                               "MONTH5, " +
                               "MONTH6, " +
                               "MONTH7, " +
                               "MONTH8, " +
                               "MONTH9, " +
                               "MONTH10, " +
                               "MONTH11, " +
                               "MONTH12) " +
                            "VALUES " +
                              "(?BUDGET_ID, " +
                               "?LEDGER_ID," +
                               " ?MONTH1, " +
                               "?MONTH2, " +
                               "?MONTH3, " +
                               "?MONTH4, " +
                               "?MONTH5, " +
                               "?MONTH6, " +
                               "?MONTH7, " +
                               "?MONTH8, " +
                               "?MONTH9, " +
                               "?MONTH10, " +
                               "?MONTH11, " +
                               "?MONTH12)";

                        break;
                    }
                case SQLCommand.Budget.UpdateAllotFund:
                    {
                        query = "UPDATE ALLOT_FUND SET " +
                                        "MONTH1 =?MONTH1, " +
                                        "MONTH2=?MONTH2, " +
                                        "MONTH3=?MONTH3, " +
                                        "MONTH4=?MONTH4, " +
                                        "MONTH5=?MONTH5, " +
                                        "MONTH6=?MONTH6, " +
                                        "MONTH7=?MONTH7, " +
                                        "MONTH8=?MONTH8, " +
                                        "MONTH9=?MONTH9, " +
                                        "MONTH10=?MONTH10, " +
                                        "MONTH11=?MONTH11, " +
                                        "MONTH12=?MONTH12 " +
                                        "WHERE LEDGER_ID=?LEDGER_ID ";
                        break;
                    }
                case SQLCommand.Budget.FetchAllotFund:
                    {
                        query = "SELECT " +
                                    "BUDGET_ID," +
                                    "LEDGER_ID," +
                                    "MONTH1, " +
                                    "MONTH2, " +
                                    "MONTH3, " +
                                    "MONTH4, " +
                                    "MONTH5, " +
                                    "MONTH6, " +
                                    "MONTH7, " +
                                    "MONTH8, " +
                                    "MONTH9, " +
                                    "MONTH10, " +
                                    "MONTH11, " +
                                    "MONTH12 " +
                                    "FROM ALLOT_FUND WHERE LEDGER_ID=?LEDGER_ID AND BUDGET_ID=?BUDGET_ID;";
                        break;
                    }
                case SQLCommand.Budget.GetLedgerExist:
                    {
                        query = "SELECT COUNT(*) AS COUNT FROM ALLOT_FUND WHERE LEDGER_ID=?LEDGER_ID AND BUDGET_ID=?BUDGET_ID;";
                        break;
                    }
                case SQLCommand.Budget.GetRandomMonth:
                    {
                        query = @"SELECT MONTH_YEAR,DATE_FORMAT(MONTH_YEAR,'%b - %Y') AS DURATION,
                                           IF(MONTH(MONTH_YEAR) = 1 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                              MONTH1,
                                              IF(MONTH(MONTH_YEAR) = 2 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                 MONTH2,
                                                 IF(MONTH(MONTH_YEAR) = 3 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                    MONTH3,
                                                    IF(MONTH(MONTH_YEAR) = 4 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                       MONTH4,
                                                       IF(MONTH(MONTH_YEAR) = 5 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                          MONTH5,
                                                          IF(MONTH(MONTH_YEAR) = 6 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                             MONTH6,
                                                             IF(MONTH(MONTH_YEAR) = 7 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                                MONTH7,
                                                                IF(MONTH(MONTH_YEAR) = 8 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                                   MONTH8,
                                                                   IF(MONTH(MONTH_YEAR) = 9 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                                      MONTH9,
                                                                      IF(MONTH(MONTH_YEAR) = 10 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                                         MONTH10,
                                                                         IF(MONTH(MONTH_YEAR) = 11 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                                            MONTH11,
                                                                            IF(MONTH(MONTH_YEAR) = 12 AND AL.LEDGER_ID=BL.LEDGER_ID, MONTH12, 0.00)))))))))))) AS AMOUNT,
                                           IF(MLG.NATURE_ID IN (1, 4), 'CR', 'DR') AS TRANS_MODE,
                                           YEAR(MONTH_YEAR) AS 'YEAR',
                                           MONTH(MONTH_YEAR) AS 'MONTH',
                                           ML.LEDGER_CODE,
                                           MLG.LEDGER_GROUP,
                                           BL.BUDGET_ID,
                                           BL.LEDGER_ID,
                                           ML.LEDGER_NAME
                                      FROM (SELECT (?DATE_FROM - INTERVAL
                                                    DAYOFMONTH(?DATE_FROM) - 1 DAY) + INTERVAL NO_OF_MONTH MONTH AS MONTH_YEAR,
                                                   NO_OF_MONTH
                                              FROM (SELECT @ROWNUM := @ROWNUM + 1 AS NO_OF_MONTH
                                                      FROM (SELECT 1 UNION
                                                                    SELECT 2 UNION
                                                                            SELECT 3 UNION
                                                                                    SELECT 4
                                                
                                                            ) AS T1,
                                                           (SELECT 1 UNION
                                                                    SELECT 2 UNION
                                                                            SELECT 3 UNION
                                                                                    SELECT 4
                                                
                                                            ) AS T2,
                                                           (SELECT 1 UNION
                                                                    SELECT 2 UNION
                                                                            SELECT 3 UNION
                                                                                    SELECT 4
                                                
                        
                                                            ) AS T3,
                                                           (SELECT @ROWNUM := -1) AS T0) D1) D2
                                      LEFT JOIN BUDGET_LEDGER BL
                                     INNER JOIN MASTER_LEDGER ML
                                     INNER JOIN MASTER_LEDGER_GROUP MLG
                                        ON ML.GROUP_ID = MLG.GROUP_ID ON ML.LEDGER_ID = BL.LEDGER_ID
                                      LEFT JOIN ALLOT_FUND AL
                                        ON AL.BUDGET_ID = BL.BUDGET_ID AND AL.LEDGER_ID=BL.LEDGER_ID
                                      LEFT JOIN BUDGET_MASTER BM
                                        ON BM.BUDGET_ID = BL.BUDGET_ID AND AL.LEDGER_ID=BL.LEDGER_ID
                                       AND BM.DATE_FROM <= ?DATE_FROM
                                       AND BM.DATE_TO >= ?DATE_TO
                                       AND YEAR(D2.MONTH_YEAR) = YEAR(BM.DATE_FROM)
                                       AND MONTH(D2.MONTH_YEAR) = MONTH(BM.DATE_FROM)
                                     WHERE BL.BUDGET_ID = ?BUDGET_ID
                                       AND D2.MONTH_YEAR <= ?DATE_TO;";
                        break;
                    }

            }
            return query;
        }
        #endregion Bank SQL
        #endregion
    }
}
