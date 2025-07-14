//#1. Done on 09/05/2017, Modified by Alwar for getting Cash/Bank/FD Opening/Closing Balance (Query ID:FetchBalance)
//      1. Show zero value balances (OP), Changed Query from INNER JOIN with LEDGER_BALANCE TABLE to LEFT JOIN
//      2. Above LEFT JOIN show all banks which are not mapped with project, so Added INNER JOIN with PROJECT_LEDGER

// #2. Done on 18/05/2017, Modified by Alwar for fixing Cash/Bank/FD Order in all reports. In XtraReport there is no option to Grouping on one field and sorting on another field
// by default it takes accending order so it shows like Bank/Cash/Fixed Deposit. to resolve this issue, we have added spaces in query like ("  Cash", " Bank", "FD") and trim
// and assing in display

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class BalanceSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.TransBalance).FullName)
            {
                query = GetSettingSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script

        private string GetSettingSQL()
        {
            string query = "";
            SQLCommand.TransBalance sqlCommandId = (SQLCommand.TransBalance)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                
                case SQLCommand.TransBalance.FetchOpBalance:
                    {
                        query = "SELECT BALANCE_DATE, AMOUNT, TRANS_MODE " +
                                "FROM LEDGER_BALANCE " +
                                "WHERE PROJECT_ID = ?PROJECT_ID " +
                                "AND LEDGER_ID = ?LEDGER_ID " +
                                "AND TRANS_FLAG = 'OP'";
                        break;
                    }
                case SQLCommand.TransBalance.FetchOpBalanceList:
                    {
                        
                        //On 03/02/2021, to include Project Category
                        query = "SELECT PL.PROJECT_ID, PL.LEDGER_ID,IF(MIP.LEDGER_ID IS NULL,0,MIP.LEDGER_ID) AS LEGAL_ENTITY_LEDGER_ID,IF(MIP.CUSTOMERID IS NULL,0,MIP.CUSTOMERID) AS CUSTOMER_ID, " +
                                "ML.BANK_ACCOUNT_ID, ML.GROUP_ID,ML.ACCESS_FLAG, " +
                                "CASE " +
                                    "WHEN LEDGER_GROUP='Bank Accounts' THEN " +
                                     "CONCAT(CONCAT(ML.LEDGER_NAME,' - '),CONCAT(MB.BANK,' - '),MB.BRANCH) " +
                                    "ELSE " +
                                     "ML.LEDGER_NAME " +
                                "END AS 'LEDGER_NAME', " +
                                "LG.LEDGER_GROUP, " +
                                "IFNULL(IF(ML.GROUP_ID IN (12, 13, 14) AND ML.CUR_COUNTRY_ID>0, LB.AMOUNT_FC, LB.AMOUNT),0) AS AMOUNT, ML.IS_TDS_LEDGER, LG.NATURE_ID, " +
                                "LEDGER_CODE," +
                                "IF (IFNULL(LB.TRANS_MODE, '') = '', " +
                                "    IF(LG.NATURE_ID IN (1,4), 'CR', 'DR'), LB.TRANS_MODE) AS TRANS_MODE, " +
                                "CASE WHEN LEDGER_TYPE='GN' " +
                                          "THEN 'General'   ELSE CASE WHEN LEDGER_TYPE='IK' " +
                                          "THEN 'In kind' END END 'GROUP', " +
                                  "CASE " +
                                        "WHEN LEDGER_SUB_TYPE = 'BK' THEN " +
                                        "'Bank Accounts' " +
                                        "ELSE " +
                                        "CASE " +
                                        "WHEN LEDGER_SUB_TYPE = 'FD' THEN " +
                                            "'Fixed Deposit' " +
                                        "ELSE " +
                                            "LEDGER_GROUP " +
                                        "END " +
                                    "END AS 'TYPE', " +
                                "IFNULL(LB.LEDGER_ID, 0) AS UPDATE_MODE, ML.IS_COST_CENTER, PC.PROJECT_CATOGORY_NAME, " + 
                                " IF(MCE.EXCHANGE_RATE = 0 OR MCE.EXCHANGE_RATE IS NULL, 1, MCE.EXCHANGE_RATE) AS EXCHANGE_RATE " +
                                "FROM PROJECT_LEDGER AS PL " +
                                "LEFT JOIN MASTER_LEDGER AS ML  ON PL.LEDGER_ID = ML.LEDGER_ID " +
                                "LEFT JOIN MASTER_COUNTRY_CURRENCY_EXCHANGE_RATE MCE ON MCE.COUNTRY_ID = ML.CUR_COUNTRY_ID " +
                                "   AND MCE.APPLICABLE_FROM = ?APPLICABLE_FROM AND  MCE.APPLICABLE_TO =?APPLICABLE_TO " +
                                "LEFT JOIN MASTER_LEDGER_GROUP AS LG  ON ML.GROUP_ID = LG.GROUP_ID " +
                                "LEFT JOIN LEDGER_BALANCE AS LB ON PL.PROJECT_ID = LB.PROJECT_ID " +
                                "AND PL.LEDGER_ID = LB.LEDGER_ID AND LB.TRANS_FLAG = 'OP' " +
                                "LEFT JOIN MASTER_BANK_ACCOUNT MBA  ON MBA.LEDGER_ID=ML.LEDGER_ID " +
                                "LEFT JOIN MASTER_BANK MB  ON MB.BANK_ID=MBA.BANK_ID " +
                                "LEFT JOIN MASTER_INSTI_PERFERENCE MIP ON ML.LEDGER_ID=MIP.LEDGER_ID " +
                                "LEFT JOIN (SELECT PCL.LEDGER_ID, GROUP_CONCAT(MPC.PROJECT_CATOGORY_NAME SEPARATOR  ', ') AS PROJECT_CATOGORY_NAME " +
                                "            FROM PROJECT_CATEGORY_LEDGER PCL " +
                                "            INNER JOIN MASTER_PROJECT_CATOGORY MPC ON MPC.PROJECT_CATOGORY_ID = PCL.PROJECT_CATEGORY_ID " +
                                "            GROUP BY PCL.LEDGER_ID) AS PC ON PC.LEDGER_ID = PL.LEDGER_ID " +
                                "WHERE PL.PROJECT_ID = ?PROJECT_ID " +
                                "{AND PL.LEDGER_ID = ?LEDGER_ID} {AND ML.GROUP_ID = ?GROUP_ID} ORDER BY ML.SORT_ID";

                        break;
                    }

                case SQLCommand.TransBalance.FetchOpBalanceListGeneralate:
                    {
                        //On 03/02/2021, to include Project Category
                        query = "SELECT PL.PROJECT_ID, PL.LEDGER_ID,IF(MIP.LEDGER_ID IS NULL,0,MIP.LEDGER_ID) AS LEGAL_ENTITY_LEDGER_ID,IF(MIP.CUSTOMERID IS NULL,0,MIP.CUSTOMERID) AS CUSTOMER_ID, " +
                                "ML.BANK_ACCOUNT_ID, ML.GROUP_ID,ML.ACCESS_FLAG, " +
                                "CASE " +
                                    "WHEN LEDGER_GROUP='Bank Accounts' THEN " +
                                     "CONCAT(CONCAT(ML.LEDGER_NAME,' - '),CONCAT(MB.BANK,' - '),MB.BRANCH) " +
                                    "ELSE " +
                                     "ML.LEDGER_NAME " +
                                "END AS 'LEDGER_NAME', " +
                                "LG.LEDGER_GROUP, " +
                                "IFNULL(LB.AMOUNT,0) AS AMOUNT,ML.IS_TDS_LEDGER, LG.NATURE_ID, " +
                                "LEDGER_CODE," +
                                "IF (IFNULL(LB.TRANS_MODE, '') = '', " +
                                "    IF(LG.NATURE_ID IN (1,4), 'CR', 'DR'), LB.TRANS_MODE) AS TRANS_MODE, " +
                                "CASE WHEN LEDGER_TYPE='GN' " +
                                          "THEN 'General'   ELSE CASE WHEN LEDGER_TYPE='IK' " +
                                          "THEN 'In kind' END END 'GROUP', " +
                                  "CASE " +
                                        "WHEN LEDGER_SUB_TYPE = 'BK' THEN " +
                                        "'Bank Accounts' " +
                                        "ELSE " +
                                        "CASE " +
                                        "WHEN LEDGER_SUB_TYPE = 'FD' THEN " +
                                            "'Fixed Deposit' " +
                                        "ELSE " +
                                            "LEDGER_GROUP " +
                                        "END " +
                                    "END AS 'TYPE', " +
                                "IFNULL(LB.LEDGER_ID, 0) AS UPDATE_MODE, ML.IS_COST_CENTER, PC.PROJECT_CATOGORY_NAME " +
                                "FROM PROJECT_LEDGER AS PL " +
                                "LEFT JOIN MASTER_LEDGER AS ML  ON PL.LEDGER_ID = ML.LEDGER_ID " +
                                "LEFT JOIN MASTER_LEDGER_GROUP AS LG  ON ML.GROUP_ID = LG.GROUP_ID " +
                                "LEFT JOIN LEDGER_BALANCE AS LB ON PL.PROJECT_ID = LB.PROJECT_ID " +
                                "AND PL.LEDGER_ID = LB.LEDGER_ID AND LB.TRANS_FLAG = 'OP' " +
                                "LEFT JOIN MASTER_BANK_ACCOUNT MBA  ON MBA.LEDGER_ID=ML.LEDGER_ID " +
                                "LEFT JOIN MASTER_BANK MB  ON MB.BANK_ID=MBA.BANK_ID " +
                                "LEFT JOIN MASTER_INSTI_PERFERENCE MIP ON ML.LEDGER_ID=MIP.LEDGER_ID " +
                                "LEFT JOIN (SELECT PCL.LEDGER_ID, GROUP_CONCAT(MPC.PROJECT_CATOGORY_NAME SEPARATOR  ', ') AS PROJECT_CATOGORY_NAME " +
                                "            FROM PROJECT_CATEGORY_LEDGER PCL " +
                                "            INNER JOIN MASTER_PROJECT_CATOGORY MPC ON MPC.PROJECT_CATOGORY_ID = PCL.PROJECT_CATEGORY_ID " +
                                "            GROUP BY PCL.LEDGER_ID) AS PC ON PC.LEDGER_ID = PL.LEDGER_ID " +
                                " ORDER BY ML.SORT_ID";

                        break;
                    }
                case SQLCommand.TransBalance.FetchExpenseLedgerListBudget:
                    {
                        query = "SELECT PL.PROJECT_ID, PL.LEDGER_ID,IF(MIP.LEDGER_ID IS NULL,0,MIP.LEDGER_ID) AS LEGAL_ENTITY_LEDGER_ID,IF(MIP.CUSTOMERID IS NULL,0,MIP.CUSTOMERID) AS CUSTOMER_ID, " +
                                "ML.BANK_ACCOUNT_ID, ML.GROUP_ID,ML.ACCESS_FLAG, " +
                                "CASE " +
                                    "WHEN LEDGER_GROUP='Bank Accounts' THEN " +
                                     "CONCAT(CONCAT(ML.LEDGER_NAME,' - '),CONCAT(MB.BANK,' - '),MB.BRANCH) " +
                                    "ELSE " +
                                     "ML.LEDGER_NAME " +
                                "END AS 'LEDGER_NAME', " +
                                "LG.LEDGER_GROUP, " +
                                "IFNULL(LB.AMOUNT,0) AS AMOUNT,ML.IS_TDS_LEDGER, LG.NATURE_ID, " +
                                "LEDGER_CODE," +
                                "IF (IFNULL(LB.TRANS_MODE, '') = '', " +
                                "    IF(LG.NATURE_ID IN (1,4), 'CR', 'DR'), LB.TRANS_MODE) AS TRANS_MODE, " +
                                "CASE WHEN LEDGER_TYPE='GN' " +
                                          "THEN 'General'   ELSE CASE WHEN LEDGER_TYPE='IK' " +
                                          "THEN 'In kind' END END 'GROUP', " +
                                  "CASE " +
                                        "WHEN LEDGER_SUB_TYPE = 'BK' THEN " +
                                        "'Bank Accounts' " +
                                        "ELSE " +
                                        "CASE " +
                                        "WHEN LEDGER_SUB_TYPE = 'FD' THEN " +
                                            "'Fixed Deposit' " +
                                        "ELSE " +
                                            "LEDGER_GROUP " +
                                        "END " +
                                    "END AS 'TYPE', " +
                                "IFNULL(LB.LEDGER_ID, 0) AS UPDATE_MODE " +
                                "FROM PROJECT_LEDGER AS PL " +
                                "LEFT JOIN MASTER_LEDGER AS ML " +
                                "ON PL.LEDGER_ID = ML.LEDGER_ID " +
                                "LEFT JOIN MASTER_LEDGER_GROUP AS LG " +
                                "ON ML.GROUP_ID = LG.GROUP_ID " +
                                "LEFT JOIN LEDGER_BALANCE AS LB " +
                                "ON PL.PROJECT_ID = LB.PROJECT_ID " +
                                "AND PL.LEDGER_ID = LB.LEDGER_ID " +
                                "AND LB.TRANS_FLAG = 'OP' " +
                                "LEFT JOIN MASTER_BANK_ACCOUNT MBA " +
                                "ON MBA.LEDGER_ID=ML.LEDGER_ID " +
                                "LEFT JOIN MASTER_BANK MB " +
                                "ON MB.BANK_ID=MBA.BANK_ID " +
                                "LEFT JOIN MASTER_INSTI_PERFERENCE MIP " +
                                "ON ML.LEDGER_ID=MIP.LEDGER_ID " +
                                "WHERE PL.PROJECT_ID = ?PROJECT_ID AND ML.ACCESS_FLAG <>2 AND ML.GROUP_ID NOT IN(12,13,14) AND NATURE_ID IN (2, 3, 4) " +
                                "{AND PL.LEDGER_ID = ?LEDGER_ID} {AND ML.GROUP_ID = ?GROUP_ID} ORDER BY ML.SORT_ID";
                        break;
                    }
                // to fetch the Budget Mapped details for Transactions 04.01.2020
                case SQLCommand.TransBalance.FetchBudgetMappedLedgerList:
                    {
                        query = "SELECT PL.PROJECT_ID, PL.LEDGER_ID,IF(MIP.LEDGER_ID IS NULL,0,MIP.LEDGER_ID) AS LEGAL_ENTITY_LEDGER_ID,IF(MIP.CUSTOMERID IS NULL,0,MIP.CUSTOMERID) AS CUSTOMER_ID, " +
                               "ML.BANK_ACCOUNT_ID, ML.GROUP_ID,ML.ACCESS_FLAG, " +
                               "CASE " +
                                   "WHEN LEDGER_GROUP='Bank Accounts' THEN " +
                                    "CONCAT(CONCAT(ML.LEDGER_NAME,' - '),CONCAT(MB.BANK,' - '),MB.BRANCH) " +
                                   "ELSE " +
                                    "ML.LEDGER_NAME " +
                               "END AS 'LEDGER_NAME', " +
                               "LG.LEDGER_GROUP, " +
                               "IFNULL(LB.AMOUNT,0) AS AMOUNT,ML.IS_TDS_LEDGER, LG.NATURE_ID, " +
                               "LEDGER_CODE," +
                               "IF (IFNULL(LB.TRANS_MODE, '') = '', " +
                               "    IF(LG.NATURE_ID IN (1,4), 'CR', 'DR'), LB.TRANS_MODE) AS TRANS_MODE, " +
                               "CASE WHEN LEDGER_TYPE='GN' " +
                                         "THEN 'General'   ELSE CASE WHEN LEDGER_TYPE='IK' " +
                                         "THEN 'In kind' END END 'GROUP', " +
                                 "CASE " +
                                       "WHEN LEDGER_SUB_TYPE = 'BK' THEN " +
                                       "'Bank Accounts' " +
                                       "ELSE " +
                                       "CASE " +
                                       "WHEN LEDGER_SUB_TYPE = 'FD' THEN " +
                                           "'Fixed Deposit' " +
                                       "ELSE " +
                                           "LEDGER_GROUP " +
                                       "END " +
                                   "END AS 'TYPE', " +
                               "IFNULL(LB.LEDGER_ID, 0) AS UPDATE_MODE " +
                               "FROM PROJECT_BUDGET_LEDGER AS PL " +
                               "LEFT JOIN MASTER_LEDGER AS ML " +
                               "ON PL.LEDGER_ID = ML.LEDGER_ID " +
                               "LEFT JOIN MASTER_LEDGER_GROUP AS LG " +
                               "ON ML.GROUP_ID = LG.GROUP_ID " +
                               "LEFT JOIN LEDGER_BALANCE AS LB " +
                               "ON PL.PROJECT_ID = LB.PROJECT_ID " +
                               "AND PL.LEDGER_ID = LB.LEDGER_ID " +
                               "AND LB.TRANS_FLAG = 'OP' " +
                               "LEFT JOIN MASTER_BANK_ACCOUNT MBA " +
                               "ON MBA.LEDGER_ID=ML.LEDGER_ID " +
                               "LEFT JOIN MASTER_BANK MB " +
                               "ON MB.BANK_ID=MBA.BANK_ID " +
                               "LEFT JOIN MASTER_INSTI_PERFERENCE MIP " +
                               "ON ML.LEDGER_ID=MIP.LEDGER_ID " +
                               "WHERE PL.PROJECT_ID = ?PROJECT_ID " +
                               "{AND PL.LEDGER_ID = ?LEDGER_ID} {AND ML.GROUP_ID = ?GROUP_ID} ORDER BY ML.SORT_ID";
                        break;
                    }
                case SQLCommand.TransBalance.FetchGeneralateMappedLedgerList:
                    {
                        query = "SELECT ML.LEDGER_ID,\n" +
                        "       ML.LEDGER_CODE,ML.LEDGER_NAME,\n" +
                        "       CONCAT(IFNULL(GROUP_CODE, ''), ' - ', MLG.LEDGER_GROUP) AS TYPE, ML.ACCESS_FLAG, \n" +
                        "       MLG.NATURE_ID,\n" +
                        "       CASE\n" +
                        "         WHEN MLG.NATURE_ID = 1 THEN\n" +
                        "          'Incomes'\n" +
                        "         WHEN MLG.NATURE_ID = 2 THEN\n" +
                        "          'Expenses'\n" +
                        "         WHEN MLG.NATURE_ID = 3 THEN\n" +
                        "          'Assets'\n" +
                        "         WHEN MLG.NATURE_ID = 4 THEN\n" +
                        "          'Liabilities'\n" +
                        "       END NATURE\n" +
                        " -- IF(CLM.CON_LEDGER_ID = ?CON_LEDGER_ID, 1, 0) AS 'SELECT'\n" +
                        "  FROM MASTER_LEDGER AS ML\n" +
                        " INNER JOIN PORTAL_CONGREGATION_LEDGER_MAP AS CLM\n" +
                        "   ON ML.LEDGER_ID = CLM.LEDGER_ID\n" +
                        "   AND ML.ACCESS_FLAG = 0\n" +
                        "  LEFT JOIN MASTER_LEDGER_GROUP MLG\n" +
                        "   ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                        " WHERE ML.GROUP_ID NOT IN (12, 13)\n" +
                        "  AND ML.IS_BRANCH_LEDGER = 0\n" +
                        "  AND (CLM.CON_LEDGER_ID IS NULL OR CLM.CON_LEDGER_ID = ?CON_LEDGER_ID);";
                        break;
                    }
                case SQLCommand.TransBalance.FetchGroupSumBalance:
                    {
                        query = "SELECT LG.GROUP_ID, LG.GROUP_CODE, LG.LEDGER_GROUP," +
                                "ABS(SUM(CASE WHEN LB2.TRANS_MODE = 'DR' THEN LB2.AMOUNT ELSE - LB2.AMOUNT END)) AS AMOUNT, " +
                                "SUM(CASE WHEN LB2.TRANS_MODE = 'DR' THEN LB2.AMOUNT ELSE 0 END) AS AMOUNT_DR, " +
                                "SUM(CASE WHEN LB2.TRANS_MODE = 'CR' THEN LB2.AMOUNT ELSE 0 END) AS AMOUNT_CR, " +
                                "CASE WHEN (SUM(CASE WHEN LB2.TRANS_MODE = 'DR' " +
                                "               THEN LB2.AMOUNT ELSE - LB2.AMOUNT END) >= 0 ) " +
                                "     THEN 'DR' ELSE 'CR' END AS TRANS_MODE " +
                                "FROM MASTER_LEDGER AS ML " +
                                "INNER JOIN MASTER_LEDGER_GROUP AS LG  ON ML.GROUP_ID = LG.GROUP_ID " +
                                "INNER JOIN PROJECT_LEDGER AS PL ON PL.PROJECT_ID IN (?PROJECT_ID) AND PL.LEDGER_ID = ML.LEDGER_ID " +
                                "INNER JOIN " +
                                "     (SELECT LB.BALANCE_DATE, LB.PROJECT_ID, LB.LEDGER_ID, LB.AMOUNT, LB.TRANS_MODE " +
                                "      FROM LEDGER_BALANCE AS LB " +
                                "      LEFT JOIN (SELECT LBA.PROJECT_ID, LBA.LEDGER_ID, MAX(LBA.BALANCE_DATE) AS BAL_DATE " +
                                "                 FROM LEDGER_BALANCE LBA " +
                                "                 WHERE 1 = 1 {AND LBA.BALANCE_DATE <= ?BALANCE_DATE} " +
                                "                 GROUP BY LBA.PROJECT_ID, LBA.LEDGER_ID) AS LB1 " +
                                "      ON LB.PROJECT_ID = LB1.PROJECT_ID  AND LB.LEDGER_ID = LB1.LEDGER_ID " +
                                "      WHERE LB.PROJECT_ID IN (?PROJECT_ID) AND LB.BALANCE_DATE = LB1.BAL_DATE) LB2 " +
                                "ON ML.LEDGER_ID = LB2.LEDGER_ID AND PL.PROJECT_ID = LB2.PROJECT_ID " +
                                "WHERE LG.GROUP_ID IN (?GROUP_ID) {AND ML.LEDGER_ID IN (?LEDGER_ID)} " +
                                "GROUP BY LG.GROUP_ID, LG.GROUP_CODE, LG.LEDGER_GROUP";
                        break;
                    }
                case SQLCommand.TransBalance.FetchGroupSumBalancePreviousYears:
                    {
                        query = "SELECT LG.GROUP_ID, LG.GROUP_CODE, LG.LEDGER_GROUP," +
                                "SUM(CASE WHEN LB.TRANS_MODE = 'DR' THEN LB.AMOUNT ELSE - LB.AMOUNT END) AS AMOUNT," +
                                "SUM(CASE WHEN LBY1.TRANS_MODE = 'DR' THEN LBY1.AMOUNT ELSE - LBY1.AMOUNT END) AS Y1_AMOUNT, " +
                                "SUM(CASE WHEN LBY2.TRANS_MODE = 'DR' THEN LBY2.AMOUNT ELSE - LBY2.AMOUNT END) AS Y2_AMOUNT " +
                                "FROM MASTER_LEDGER AS ML " +
                                "INNER JOIN MASTER_LEDGER_GROUP AS LG  ON ML.GROUP_ID = LG.GROUP_ID " +
                                "INNER JOIN PROJECT_LEDGER AS PL ON PL.PROJECT_ID IN (?PROJECT_ID) AND PL.LEDGER_ID = ML.LEDGER_ID " +
                                "LEFT JOIN " +
                                "     (SELECT LB.BALANCE_DATE, LB.PROJECT_ID, LB.LEDGER_ID, LB.AMOUNT, LB.TRANS_MODE " +
                                "      FROM LEDGER_BALANCE AS LB " +
                                "      LEFT JOIN (SELECT LBA.PROJECT_ID, LBA.LEDGER_ID, MAX(LBA.BALANCE_DATE) AS BAL_DATE " +
                                "                 FROM LEDGER_BALANCE LBA " +
                                "                 WHERE 1 = 1 {AND LBA.BALANCE_DATE <= ?BALANCE_DATE} " +
                                "                 GROUP BY LBA.PROJECT_ID, LBA.LEDGER_ID) AS LB1 " +
                                "      ON LB.PROJECT_ID = LB1.PROJECT_ID  AND LB.LEDGER_ID = LB1.LEDGER_ID " +
                                "      WHERE LB.PROJECT_ID IN (?PROJECT_ID) AND LB.BALANCE_DATE = LB1.BAL_DATE) LB " +
                                "ON LB.LEDGER_ID = ML.LEDGER_ID AND LB.PROJECT_ID = PL.PROJECT_ID " +
                                "LEFT JOIN " +
                                "     (SELECT LB.BALANCE_DATE, LB.PROJECT_ID, LB.LEDGER_ID, LB.AMOUNT, LB.TRANS_MODE " +
                                "      FROM LEDGER_BALANCE AS LB " +
                                "      LEFT JOIN (SELECT LBA.PROJECT_ID, LBA.LEDGER_ID, MAX(LBA.BALANCE_DATE) AS BAL_DATE " +
                                "                 FROM LEDGER_BALANCE LBA " +
                                "                 WHERE 1 = 1 {AND LBA.BALANCE_DATE <= DATE_ADD(?BALANCE_DATE, INTERVAL - 1 YEAR)} " +
                                "                 GROUP BY LBA.PROJECT_ID, LBA.LEDGER_ID) AS LB1 " +
                                "      ON LB.PROJECT_ID = LB1.PROJECT_ID  AND LB.LEDGER_ID = LB1.LEDGER_ID " +
                                "      WHERE LB.PROJECT_ID IN (?PROJECT_ID) AND LB.BALANCE_DATE = LB1.BAL_DATE) LBY1 " +
                                "ON LBY1.LEDGER_ID = ML.LEDGER_ID AND LBY1.PROJECT_ID = PL.PROJECT_ID " +
                                "LEFT JOIN " +
                                "     (SELECT LB.BALANCE_DATE, LB.PROJECT_ID, LB.LEDGER_ID, LB.AMOUNT, LB.TRANS_MODE " +
                                "      FROM LEDGER_BALANCE AS LB " +
                                "      LEFT JOIN (SELECT LBA.PROJECT_ID, LBA.LEDGER_ID, MAX(LBA.BALANCE_DATE) AS BAL_DATE " +
                                "                 FROM LEDGER_BALANCE LBA " +
                                "                 WHERE 1 = 1 {AND LBA.BALANCE_DATE <= DATE_ADD(?BALANCE_DATE, INTERVAL - 2 YEAR)} " +
                                "                 GROUP BY LBA.PROJECT_ID, LBA.LEDGER_ID) AS LB1 " +
                                "      ON LB.PROJECT_ID = LB1.PROJECT_ID  AND LB.LEDGER_ID = LB1.LEDGER_ID " +
                                "      WHERE LB.PROJECT_ID IN (?PROJECT_ID) AND LB.BALANCE_DATE = LB1.BAL_DATE) LBY2 " +
                                "ON PL.PROJECT_ID = LBY2.PROJECT_ID AND ML.LEDGER_ID = LBY2.LEDGER_ID " +
                                "WHERE LG.GROUP_ID IN (?GROUP_ID) {AND ML.LEDGER_ID IN (?LEDGER_ID)} " +
                                "GROUP BY LG.GROUP_ID, LG.GROUP_CODE, LG.LEDGER_GROUP";
                        break;
                    }
                case SQLCommand.TransBalance.FetchBalance:
                    {
                        //Make use of this query for all reports Opening and Closing Balance for Cash/Bank/FD
                        //ML.LEDGER_NAME,
                        query = "SELECT LG.GROUP_ID, LG.GROUP_CODE, " +
                                 " CASE WHEN LG.GROUP_ID = " + (int)Utility.FixedLedgerGroup.Cash + " THEN CONCAT('  ', LG.LEDGER_GROUP) " + // Done on 18/05/2017, refer top history #2
                                 "WHEN LG.GROUP_ID = " + (int)Utility.FixedLedgerGroup.BankAccounts + " THEN CONCAT(' ', LG.LEDGER_GROUP) " +
                                 "WHEN LG.GROUP_ID = " + (int)Utility.FixedLedgerGroup.FixedDeposit + " THEN CONCAT('', LG.LEDGER_GROUP) " +
                                 "END AS LEDGER_GROUP, " +
                                 "ML.LEDGER_ID, ML.LEDGER_CODE, ML.SORT_ID, " +
                                 "CASE WHEN LG.GROUP_ID=12 THEN " +
                                     " CONCAT(ML.LEDGER_NAME,CONCAT(' -', CONCAT(MB.BANK, CONCAT(' -',MB.BRANCH)))) " +
                                  "ELSE  ML.LEDGER_NAME END AS 'LEDGER_NAME'," +
                                 "ABS(SUM(CASE WHEN LB2.TRANS_MODE = 'DR' THEN IFNULL( LB2.AMOUNT,0 ) ELSE - IFNULL( LB2.AMOUNT,0 ) END)) AS AMOUNT, " +
                                 "SUM(CASE WHEN LB2.TRANS_MODE = 'DR' THEN LB2.AMOUNT ELSE 0 END) AS AMOUNT_DR, " +
                                 "SUM(CASE WHEN LB2.TRANS_MODE = 'CR' THEN LB2.AMOUNT ELSE 0 END) AS AMOUNT_CR, " +
                                 "CASE WHEN (SUM(CASE WHEN LB2.TRANS_MODE = 'DR' " +
                                 "               THEN LB2.AMOUNT ELSE - LB2.AMOUNT END) >= 0 ) " +
                                 "     THEN 'DR' ELSE 'CR' END AS TRANS_MODE " +
                                 "FROM MASTER_LEDGER AS ML " +
                                 "INNER JOIN MASTER_LEDGER_GROUP AS LG " +
                                 " ON ML.GROUP_ID = LG.GROUP_ID " +
                                 "INNER JOIN PROJECT_LEDGER AS PL ON {PL.PROJECT_ID IN (?PROJECT_ID) AND} PL.LEDGER_ID = ML.LEDGER_ID " + //On 09/05/2017, To show zero value cash/bank/Fd Opening/Closing Balance Skip unmapped ledgers: Changed INNER JOIN with PROJECT_LEDGERS
                                 "LEFT JOIN MASTER_BANK_ACCOUNT AS MBA ON ML.LEDGER_ID=MBA.LEDGER_ID " +
                                 "LEFT JOIN MASTER_BANK AS MB  ON MBA.BANK_ID=MB.BANK_ID " +
                                 "LEFT JOIN (SELECT PLA.PROJECT_ID, PLA.LEDGER_ID, PLA.APPLICABLE_FROM, PLA.APPLICABLE_TO FROM PROJECT_LEDGER_APPLICABLE PLA " +
                                 "  WHERE PLA.PROJECT_ID IN (?PROJECT_ID)) AS PLA ON PLA.PROJECT_ID = PL.PROJECT_ID AND PLA.LEDGER_ID = ML.LEDGER_ID " +
                                 "LEFT JOIN " + //On 09/05/2017, To show zero value cash/bank/Fd Opening/Closing Balance: Changed INNER JOIN to LEFT
                                 "     (SELECT LB.BALANCE_DATE, LB.PROJECT_ID, LB.LEDGER_ID, " +
                                 "      IF(1=1 {AND ?CURRENCY_COUNTRY_ID = 0}, LB.AMOUNT, LB.AMOUNT_FC) AS AMOUNT, " +
                                 "      IF(1=1 {AND ?CURRENCY_COUNTRY_ID = 0}, LB.TRANS_MODE, LB.TRANS_FC_MODE) AS TRANS_MODE " +
                                 "      FROM LEDGER_BALANCE AS LB " +
                                 "      LEFT JOIN (SELECT LBA.PROJECT_ID, LBA.LEDGER_ID, MAX(LBA.BALANCE_DATE) AS BAL_DATE " +
                                 "                 FROM LEDGER_BALANCE LBA " +
                                 "                 WHERE 1 = 1 {AND LBA.BALANCE_DATE <= ?BALANCE_DATE} " +
                                 "                 GROUP BY LBA.PROJECT_ID, LBA.LEDGER_ID) AS LB1 " +
                                 "      ON LB.PROJECT_ID = LB1.PROJECT_ID " +
                                 "      AND LB.LEDGER_ID = LB1.LEDGER_ID " +
                                 "      WHERE 1=1 {AND LB.PROJECT_ID IN (?PROJECT_ID)}" +
                                 "      AND LB.BALANCE_DATE = LB1.BAL_DATE) LB2 " +
                                 "ON ML.LEDGER_ID = LB2.LEDGER_ID  AND PL.PROJECT_ID = LB2.PROJECT_ID " +
                                 "WHERE 1 = 1 {AND LG.GROUP_ID IN (?GROUP_ID)} " +
                                 "{AND ML.LEDGER_ID IN(?LEDGER_ID)} " +
                                 "{AND (MBA.DATE_CLOSED IS NULL OR MBA.DATE_CLOSED >= ?DATE_CLOSED) } " + //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                                 "{AND (((PLA.APPLICABLE_FROM BETWEEN ?APPLICABLE_FROM AND ?APPLICABLE_TO) OR PLA.APPLICABLE_FROM  IS NULL) " +
                                 " OR ( IF(PLA.APPLICABLE_TO IS NULL, PLA.APPLICABLE_TO IS NOT NULL, (PLA.APPLICABLE_TO BETWEEN ?APPLICABLE_FROM AND ?APPLICABLE_TO)) ))} " +   //On 26/09/2023, This property is used to skip bank ledger project based
                                 " {AND IF(?CURRENCY_COUNTRY_ID > 0, ML.CUR_COUNTRY_ID = ?CURRENCY_COUNTRY_ID, 1=1)}" +
                                 "GROUP BY LG.GROUP_ID, LG.GROUP_CODE, LG.LEDGER_GROUP, " +
                                 "ML.LEDGER_ID, ML.LEDGER_CODE, ML.LEDGER_NAME ORDER BY SORT_ID ASC";
                        break;
                    }
                case SQLCommand.TransBalance.FetchBalanceByProjectwise:
                    {
                        //Make use of this query for all reports Opening and Closing Balance for Cash/Bank/FD
                        //ML.LEDGER_NAME,
                        query = "SELECT LG.GROUP_ID, LG.GROUP_CODE, " +
                                " CASE WHEN LG.GROUP_ID = " + (int)Utility.FixedLedgerGroup.Cash + " THEN CONCAT('  ', LG.LEDGER_GROUP) " + // Done on 18/05/2017, refer top history #2
                                "WHEN LG.GROUP_ID = " + (int)Utility.FixedLedgerGroup.BankAccounts + " THEN CONCAT(' ', LG.LEDGER_GROUP) " +
                                "WHEN LG.GROUP_ID = " + (int)Utility.FixedLedgerGroup.FixedDeposit + " THEN CONCAT('', LG.LEDGER_GROUP) " +
                                "END AS LEDGER_GROUP, " +
                                "ML.LEDGER_ID, ML.LEDGER_CODE, ML.SORT_ID ,  " +
                                "CONCAT(CASE WHEN LG.GROUP_ID=12 THEN " +
                                    " CONCAT(ML.LEDGER_NAME,CONCAT(' -', CONCAT(MB.BANK, CONCAT(' -',MB.BRANCH)))) " +
                                 "ELSE  ML.LEDGER_NAME END, ' - ', MP.PROJECT) AS 'LEDGER_NAME'," +
                                "ABS(SUM(CASE WHEN LB2.TRANS_MODE = 'DR' THEN IFNULL(LB2.AMOUNT,0) ELSE - IFNULL(LB2.AMOUNT,0) END)) AS AMOUNT, " +
                                "SUM(CASE WHEN LB2.TRANS_MODE = 'DR' THEN LB2.AMOUNT ELSE 0 END) AS AMOUNT_DR, " +
                                "SUM(CASE WHEN LB2.TRANS_MODE = 'CR' THEN LB2.AMOUNT ELSE 0 END) AS AMOUNT_CR, " +
                                "CASE WHEN (SUM(CASE WHEN LB2.TRANS_MODE = 'DR' " +
                                "               THEN LB2.AMOUNT ELSE - LB2.AMOUNT END) >= 0 ) " +
                                "     THEN 'DR' ELSE 'CR' END AS TRANS_MODE, CONCAT(LG.LEDGER_GROUP, ' - ', MP.PROJECT) AS PROJECT_LEDGER_GROUP, MP.PROJECT, MP.PROJECT_ID " +
                                "FROM MASTER_LEDGER AS ML " +
                                "INNER JOIN MASTER_LEDGER_GROUP AS LG " +
                                " ON ML.GROUP_ID = LG.GROUP_ID " +
                                "INNER JOIN PROJECT_LEDGER AS PL ON {PL.PROJECT_ID IN (?PROJECT_ID) AND} PL.LEDGER_ID = ML.LEDGER_ID " + //On 09/05/2017, To show zero value cash/bank/Fd Opening/Closing Balance Skip unmapped ledgers: Changed INNER JOIN with PROJECT_LEDGERS
                                "INNER JOIN MASTER_PROJECT AS MP ON MP.PROJECT_ID = PL.PROJECT_ID" + //On 13/07/2020, to have project name
                                " LEFT JOIN MASTER_BANK_ACCOUNT AS MBA " +
                                " ON ML.LEDGER_ID=MBA.LEDGER_ID " +
                                " LEFT JOIN MASTER_BANK AS MB " +
                                " ON MBA.BANK_ID=MB.BANK_ID " +
                                "LEFT JOIN " + //On 09/05/2017, To show zero value cash/bank/Fd Opening/Closing Balance: Changed INNER JOIN to LEFT
                                "     (SELECT LB.BALANCE_DATE, LB.PROJECT_ID, LB.LEDGER_ID, " +
                                "      IF(1=1 {AND ?CURRENCY_COUNTRY_ID = 0}, LB.AMOUNT, LB.AMOUNT_FC) AS AMOUNT, " +
                               "       IF(1=1 {AND ?CURRENCY_COUNTRY_ID = 0}, LB.TRANS_MODE, LB.TRANS_FC_MODE) AS TRANS_MODE " +
                                "      FROM LEDGER_BALANCE AS LB " +
                                "      LEFT JOIN (SELECT LBA.PROJECT_ID, LBA.LEDGER_ID, MAX(LBA.BALANCE_DATE) AS BAL_DATE " +
                                "                 FROM LEDGER_BALANCE LBA " +
                                "                 WHERE 1 = 1 {AND LBA.BALANCE_DATE <= ?BALANCE_DATE} " +
                                "                 GROUP BY LBA.PROJECT_ID, LBA.LEDGER_ID) AS LB1 " +
                                "      ON LB.PROJECT_ID = LB1.PROJECT_ID " +
                                "      AND LB.LEDGER_ID = LB1.LEDGER_ID " +
                                "      WHERE 1=1 {AND LB.PROJECT_ID IN (?PROJECT_ID)}" +
                                "      AND LB.BALANCE_DATE = LB1.BAL_DATE) LB2 " +
                                "ON ML.LEDGER_ID = LB2.LEDGER_ID  AND PL.PROJECT_ID = LB2.PROJECT_ID " +
                                "WHERE 1 = 1 {AND LG.GROUP_ID IN (?GROUP_ID)} " +
                                "{AND ML.LEDGER_ID IN(?LEDGER_ID)} " +
                                "{AND (MBA.DATE_CLOSED IS NULL OR MBA.DATE_CLOSED >= ?DATE_CLOSED) } " + //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                                " {AND IF(?CURRENCY_COUNTRY_ID > 0, ML.CUR_COUNTRY_ID = ?CURRENCY_COUNTRY_ID, 1=1)}" +
                                "GROUP BY PL.PROJECT_ID, LG.GROUP_ID, LG.GROUP_CODE, LG.LEDGER_GROUP, " +
                                "ML.LEDGER_ID, ML.LEDGER_CODE, ML.LEDGER_NAME ORDER BY SORT_ID ASC";
                        break;
                    }
                //case SQLCommand.TransBalance.FetchLedgerName:
                //    {
                //        query = "SELECT LEDGER_NAME FROM MASTER_LEDGER WHERE LEDGER_ID=?LEDGER_ID";
                //        break;
                //    }
                //case SQLCommand.TransBalance.FetchGroupName:
                //    {
                //        query = "SELECT LEDGER_GROUP FROM MASTER_LEDGER_GROUP WHERE GROUP_ID=?GROUP_ID";
                //        break;
                //    }
                //case SQLCommand.TransBalance.FetchCCOPBalance:
                //    {
                //        query = " SELECT (T.DEBIT - T.DEBIT) AS AMOUNT FROM  (SELECT " +
                //                    "COST_CENTRE_ID, " +
                //                    "CASE WHEN TRANS_MODE ='CR' THEN IFNULL(SUM(AMOUNT),0)  END AS CREDIT, " +
                //                    "CASE WHEN TRANS_MODE ='DR' THEN IFNULL(SUM(AMOUNT),0) END AS DEBIT, " +
                //                    "TRANS_MODE " +
                //                    "FROM PROJECT_COSTCENTRE " +
                //                  "WHERE PROJECT_ID IN (?PROJECT_ID)  AND COST_CENTRE_ID IN (?COST_CENTRE_ID) ) AS T ";
                //        break;
                //    }
                //case SQLCommand.TransBalance.FetchTotalLedgerOpBalance:
                //    {
                //        query = "SELECT SUM( IF(TRANS_MODE ='DR', IFNULL(AMOUNT, 0), -IFNULL(AMOUNT, 0)) ) AS AMOUNT " +
                //                "FROM LEDGER_BALANCE WHERE TRANS_FLAG = 'OP'";
                //        break;
                //    }
                //case SQLCommand.TransBalance.HasBalance:
                //    {
                //        query = "SELECT AMOUNT FROM LEDGER_BALANCE " +
                //                "WHERE PROJECT_ID = ?PROJECT_ID AND LEDGER_ID = ?LEDGER_ID " +
                //                "AND AMOUNT > 0";
                //        break;
                //    }
                //case SQLCommand.TransBalance.DeleteBalance:
                //    {
                //        query = "DELETE FROM LEDGER_BALANCE WHERE PROJECT_ID = ?PROJECT_ID " +
                //                "AND LEDGER_ID = ?LEDGER_ID AND AMOUNT = 0";
                //        break;
                //    }
                //case SQLCommand.TransBalance.UpdateBalance:
                //    {
                //        query = "INSERT INTO LEDGER_BALANCE (BALANCE_DATE, PROJECT_ID, LEDGER_ID, AMOUNT, TRANS_MODE, TRANS_FLAG) " +
                //                "(SELECT LB.BALANCE_DATE, LB.PROJECT_ID, LEDGER_ID, IFNULL(LB1.AMOUNT, 0) AS AMOUNT, " +
                //                " IFNULL(LB1.TRANS_MODE, ?TRANS_MODE) AS TRANS_MODE, LB.TRANS_FLAG " +
                //                " FROM " +
                //                " (SELECT ?BALANCE_DATE AS BALANCE_DATE, ?PROJECT_ID AS PROJECT_ID, " +
                //                "  ?LEDGER_ID AS LEDGER_ID, ?TRANS_FLAG AS TRANS_FLAG) AS LB " +
                //                " LEFT JOIN " +
                //                " (SELECT AMOUNT, TRANS_MODE FROM LEDGER_BALANCE " +
                //                "  WHERE BALANCE_DATE < ?BALANCE_DATE " +
                //                "  AND PROJECT_ID = ?PROJECT_ID AND LEDGER_ID = ?LEDGER_ID " +
                //                "  ORDER BY BALANCE_DATE DESC LIMIT 1) AS LB1 ON 1 = 1) " +
                //                "ON DUPLICATE KEY UPDATE LEDGER_BALANCE.AMOUNT = LEDGER_BALANCE.AMOUNT; " +
                //                "SET @tmpAmt := 0.0; " +
                //                "UPDATE LEDGER_BALANCE SET " +
                //                "       AMOUNT = CASE WHEN TRANS_MODE = ?TRANS_MODE THEN ABS((@tmpAmt := AMOUNT) + ?AMOUNT) " +
                //                "                     WHEN TRANS_MODE <> ?TRANS_MODE THEN ABS((@tmpAmt := AMOUNT) - ?AMOUNT) " +
                //                "                     ELSE @tmpAmt := AMOUNT END, " +
                //                "       TRANS_MODE = CASE WHEN ((TRANS_MODE = ?TRANS_MODE) AND ((@tmpAmt + ?AMOUNT) < 0.0)) " +
                //                "                              THEN IF(TRANS_MODE = 'CR', 'DR', 'CR') " +
                //                "                         WHEN ((TRANS_MODE <> ?TRANS_MODE) AND ((@tmpAmt - ?AMOUNT) < 0.0)) " +
                //                "                              THEN IF(TRANS_MODE = 'CR', 'DR', 'CR') ELSE TRANS_MODE END " +
                //                "WHERE BALANCE_DATE >= ?BALANCE_DATE AND PROJECT_ID = ?PROJECT_ID AND LEDGER_ID = ?LEDGER_ID;";
                //        break;
                //    }
                //case SQLCommand.TransBalance.FetchTransaction:
                //    {
                //        query = "SELECT VM.VOUCHER_ID, VM.VOUCHER_DATE, VM.PROJECT_ID, " +
                //                "VT.LEDGER_ID, VT.AMOUNT, VT.TRANS_MODE  " +
                //                "FROM VOUCHER_MASTER_TRANS AS VM  " +
                //                "INNER JOIN VOUCHER_TRANS AS VT  " +
                //                "ON VM.VOUCHER_ID = VT.VOUCHER_ID  " +
                //                "WHERE VM.VOUCHER_ID = ?VOUCHER_ID";
                //        break;
                //    }
                //case SQLCommand.TransBalance.DeleteTransBalance:
                //    {
                //        query = "DELETE FROM LEDGER_BALANCE WHERE BALANCE_DATE >= ?VOUCHER_DATE AND TRANS_FLAG = 'TR';";
                //        break;
                //    }
                //case SQLCommand.TransBalance.BulkBalanceRefresh:
                //    {
                //        query = "SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS WHERE STATUS = 1 AND VOUCHER_DATE >= ?VOUCHER_DATE {AND PROJECT_ID=?PROJECT_ID} ORDER BY VOUCHER_DATE";
                //        break;
                //    }
                //case SQLCommand.TransBalance.AllProjectBalanceRefresh:
                //    {
                //        query = "SELECT VOUCHER_ID, LOCATION_ID FROM VOUCHER_MASTER_TRANS WHERE STATUS = 1 ORDER BY VOUCHER_DATE;";
                //        break;
                //    }
                /*case SQLCommand.TransBalance.FetchOpeningBalance:
                    {
                        //Make use of this query for all reports Opening and Closing Balance for Cash/Bank/FD and for multiple projects.
                        query = "SELECT LG.GROUP_ID, " +
                                "    CASE LG.GROUP_CODE " +
                                "    WHEN 12 THEN 'Bank' " +
                                "    WHEN 13 THEN 'Cash' " +
                                "    ELSE 'Fixed Depost' " +
                                "    END AS 'GROUP_CODE', " +
                                "    LG.LEDGER_GROUP, " +
                                "    ML.LEDGER_ID," +
                                "    ML.LEDGER_CODE, " +
                                "    ML.LEDGER_NAME, " +
                                "    IFNULL(ABS(SUM(CASE " +
                                "               WHEN LB2.TRANS_MODE = 'DR' THEN " +
                                "               LB2.AMOUNT " +
                                "               ELSE -LB2.AMOUNT " +
                                "               END)),0) AS AMOUNT, " +
                                "    CASE WHEN (SUM(CASE " +
                                "                   WHEN LB2.TRANS_MODE = 'DR' THEN LB2.AMOUNT " +
                                "                   ELSE -LB2.AMOUNT " +
                                "                   END) >= 0) THEN " +
                                "    'DR' ELSE 'CR' END AS TRANS_MODE " +
                                " FROM MASTER_LEDGER AS ML " +
                                " INNER JOIN MASTER_LEDGER_GROUP AS LG " +
                                " ON ML.GROUP_ID = LG.GROUP_ID " +
                                " LEFT JOIN (SELECT LB.BALANCE_DATE, " +
                                "            LB.PROJECT_ID, " +
                                "            LB.LEDGER_ID, " +
                                "            LB.AMOUNT, " +
                                "            LB.TRANS_MODE " +
                                "            FROM LEDGER_BALANCE AS LB " +
                                "            LEFT JOIN (SELECT LBA.PROJECT_ID, " +
                                "                       LBA.LEDGER_ID, " +
                                "                       MAX(LBA.BALANCE_DATE) AS BAL_DATE " +
                                "                       FROM LEDGER_BALANCE LBA " +
                                "                       WHERE 1 = 1 " +
                                "                       {AND LBA.BALANCE_DATE <= ?DATE_FROM}" +
                                "                       GROUP BY LBA.PROJECT_ID, LBA.LEDGER_ID) AS LB1 " +
                                "            ON LB.PROJECT_ID = LB1.PROJECT_ID " +
                                "            AND LB.LEDGER_ID = LB1.LEDGER_ID " +
                                "            WHERE FIND_IN_SET(LB.PROJECT_ID, ?PROJECT) > 0 " +
                                "            AND LB.BALANCE_DATE = LB1.BAL_DATE) LB2 " +
                                " ON ML.LEDGER_ID = LB2.LEDGER_ID " +
                                " WHERE 1 = 1 " +
                                " AND LG.GROUP_ID IN (12, 13, 14) " +
                                " GROUP BY LG.GROUP_ID";
                        break;
                    }*/
            }

            return query;
        }

        #endregion
    }
}
