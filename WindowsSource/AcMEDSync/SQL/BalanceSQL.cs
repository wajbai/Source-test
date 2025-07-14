﻿using System;
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
                case SQLCommand.TransBalance.UpdateBalance:
                    {
                        query = "INSERT INTO LEDGER_BALANCE (BALANCE_DATE, BRANCH_ID, PROJECT_ID, LEDGER_ID, " +
                                "AMOUNT, TRANS_MODE, TRANS_FLAG) " +
                                "(SELECT LB.BALANCE_DATE, LB.BRANCH_ID, LB.PROJECT_ID, LEDGER_ID, " +
                                " IFNULL(LB1.AMOUNT, 0) AS AMOUNT, " +
                                " IFNULL(LB1.TRANS_MODE, ?TRANS_MODE) AS TRANS_MODE, LB.TRANS_FLAG " +
                                " FROM " +
                                " (SELECT ?BALANCE_DATE AS BALANCE_DATE, ?BRANCH_ID AS BRANCH_ID, ?PROJECT_ID AS PROJECT_ID, " +
                                "  ?LEDGER_ID AS LEDGER_ID, ?TRANS_FLAG AS TRANS_FLAG) AS LB " +
                                " LEFT JOIN " +
                                " (SELECT AMOUNT, TRANS_MODE FROM LEDGER_BALANCE " +
                                "  WHERE BALANCE_DATE < ?BALANCE_DATE " +
                                "  AND BRANCH_ID = ?BRANCH_ID AND PROJECT_ID = ?PROJECT_ID AND LEDGER_ID = ?LEDGER_ID " +
                                "  ORDER BY BALANCE_DATE DESC LIMIT 1) AS LB1 ON 1 = 1) " +
                                "ON DUPLICATE KEY UPDATE LEDGER_BALANCE.AMOUNT = LEDGER_BALANCE.AMOUNT; " +
                                "SET @tmpAmt := 0.0; " +
                                "UPDATE LEDGER_BALANCE SET " +
                                "       AMOUNT = CASE WHEN TRANS_MODE = ?TRANS_MODE THEN ABS((@tmpAmt := AMOUNT) + ?AMOUNT) " +
                                "                     WHEN TRANS_MODE <> ?TRANS_MODE THEN ABS((@tmpAmt := AMOUNT) - ?AMOUNT) " +
                                "                     ELSE @tmpAmt := AMOUNT END, " +
                                "       TRANS_MODE = CASE WHEN ((TRANS_MODE = ?TRANS_MODE) AND ((@tmpAmt + ?AMOUNT) < 0.0)) " +
                                "                              THEN IF(TRANS_MODE = 'CR', 'DR', 'CR') " +
                                "                         WHEN ((TRANS_MODE <> ?TRANS_MODE) AND ((@tmpAmt - ?AMOUNT) < 0.0)) " +
                                "                              THEN IF(TRANS_MODE = 'CR', 'DR', 'CR') ELSE TRANS_MODE END " +
                                "WHERE BALANCE_DATE >= ?BALANCE_DATE AND BRANCH_ID = ?BRANCH_ID " +
                                "AND PROJECT_ID = ?PROJECT_ID AND LEDGER_ID = ?LEDGER_ID;";
                        break;
                    }
                case SQLCommand.TransBalance.FetchTransaction:
                    {
                        query = "SELECT VM.VOUCHER_ID, VM.VOUCHER_DATE, VM.PROJECT_ID, " +
                                "VT.LEDGER_ID, VT.AMOUNT, VT.TRANS_MODE, VM.BRANCH_ID " +
                                "FROM VOUCHER_MASTER_TRANS AS VM  " +
                                "INNER JOIN VOUCHER_TRANS AS VT  " +
                                "ON VM.VOUCHER_ID = VT.VOUCHER_ID " +
                                "AND VM.BRANCH_ID = VT.BRANCH_ID " +
                                "WHERE VM.BRANCH_ID = ?BRANCH_ID AND VM.VOUCHER_ID = ?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.TransBalance.FetchOpBalance:
                    {
                        query = "SELECT BALANCE_DATE, AMOUNT, TRANS_MODE " +
                                "FROM LEDGER_BALANCE " +
                                "WHERE BRANCH_ID = ?BRANCH_ID " +
                                "AND PROJECT_ID = ?PROJECT_ID " +
                                "AND LEDGER_ID = ?LEDGER_ID " +
                                "AND TRANS_FLAG = 'OP'";
                        break;
                    }
                case SQLCommand.TransBalance.FetchOpBalanceList:
                    {
                        query = "SELECT PL.PROJECT_ID, PL.LEDGER_ID, " +
                                "BANK_ACCOUNT_ID, ML.GROUP_ID, " +
                                "ML.LEDGER_NAME, LG.LEDGER_GROUP, " +
                                "IFNULL(LB.AMOUNT,0) AS AMOUNT, " +
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
                                "WHERE LB.BRANCH_ID = ?BRANCH_ID AND PL.PROJECT_ID = ?PROJECT_ID " +
                                "{AND PL.LEDGER_ID = ?LEDGER_ID} {AND ML.GROUP_ID = ?GROUP_ID} ";
                        break;
                    }
                case SQLCommand.TransBalance.HasBalance:
                    {
                        query = "SELECT AMOUNT FROM LEDGER_BALANCE " +
                                "WHERE BRANCH_ID = ?BRANCH_ID AND PROJECT_ID = ?PROJECT_ID " +
                                "AND LEDGER_ID = ?LEDGER_ID AND AMOUNT > 0";
                        break;
                    }
                case SQLCommand.TransBalance.DeleteBalance:
                    {
                        query = "DELETE FROM LEDGER_BALANCE " +
                                "WHERE BRANCH_ID = ?BRANCH_ID AND PROJECT_ID = ?PROJECT_ID " +
                                "AND LEDGER_ID = ?LEDGER_ID AND AMOUNT = 0";
                        break;
                    }
                case SQLCommand.TransBalance.FetchGroupSumBalance:
                    {
                        query = "SELECT LG.GROUP_ID, LG.GROUP_CODE, LG.LEDGER_GROUP, " +
                                "ABS(SUM(CASE WHEN LB2.TRANS_MODE = 'DR' THEN LB2.AMOUNT ELSE - LB2.AMOUNT END)) AS AMOUNT, " +
                                "SUM(CASE WHEN LB2.TRANS_MODE = 'DR' THEN LB2.AMOUNT ELSE 0 END) AS AMOUNT_DR, " +
                                "SUM(CASE WHEN LB2.TRANS_MODE = 'CR' THEN LB2.AMOUNT ELSE 0 END) AS AMOUNT_CR, " +
                                "CASE WHEN (SUM(CASE WHEN LB2.TRANS_MODE = 'DR' " +
                                "               THEN LB2.AMOUNT ELSE - LB2.AMOUNT END) >= 0 ) " +
                                "     THEN 'DR' ELSE 'CR' END AS TRANS_MODE " +
                                "FROM MASTER_LEDGER AS ML " +
                                "INNER JOIN MASTER_LEDGER_GROUP AS LG " +
                                "ON ML.GROUP_ID = LG.GROUP_ID " +
                                "INNER JOIN " +
                                "     (SELECT LB.BALANCE_DATE, LB.BRANCH_ID, LB.PROJECT_ID, LB.LEDGER_ID, LB.AMOUNT, LB.TRANS_MODE " +
                                "      FROM LEDGER_BALANCE AS LB " +
                                "      LEFT JOIN (SELECT LBA.BRANCH_ID, LBA.PROJECT_ID, LBA.LEDGER_ID, MAX(LBA.BALANCE_DATE) AS BAL_DATE " +
                                "                 FROM LEDGER_BALANCE LBA " +
                                "                 WHERE 1 = 1 {AND LBA.BALANCE_DATE <= ?BALANCE_DATE} " +
                                "                 GROUP BY LBA.BRANCH_ID, LBA.PROJECT_ID, LBA.LEDGER_ID) AS LB1 " +
                                "      ON LB.BRANCH_ID = LB1.BRANCH_ID " + 
                                "      AND LB.PROJECT_ID = LB1.PROJECT_ID " +
                                "      AND LB.LEDGER_ID = LB1.LEDGER_ID " +
                                "      WHERE LB.BRANCH_ID IN (?BRANCH_ID) AND LB.PROJECT_ID IN (?PROJECT_ID) " +
                                "      AND LB.BALANCE_DATE = LB1.BAL_DATE) LB2 " +
                                "ON ML.LEDGER_ID = LB2.LEDGER_ID " +
                                "WHERE LG.GROUP_ID IN (?GROUP_ID) " +
                                "{AND ML.LEDGER_ID in(?LEDGER_ID)} " +
                                "GROUP BY LG.GROUP_ID, LG.GROUP_CODE, LG.LEDGER_GROUP";
                        break;
                    }
                case SQLCommand.TransBalance.FetchBalance:
                    {
                        //Make use of this query for all reports Opening and Closing Balance for Cash/Bank/FD
                        query = "SELECT LG.GROUP_ID, LG.GROUP_CODE, LG.LEDGER_GROUP, " +
                                "ML.LEDGER_ID, ML.LEDGER_CODE, ML.LEDGER_NAME,ML.SORT_ID ,  " +
                                "ABS(SUM(CASE WHEN LB2.TRANS_MODE = 'DR' THEN LB2.AMOUNT ELSE - LB2.AMOUNT END)) AS AMOUNT, " +
                                "SUM(CASE WHEN LB2.TRANS_MODE = 'DR' THEN LB2.AMOUNT ELSE 0 END) AS AMOUNT_DR, " +
                                "SUM(CASE WHEN LB2.TRANS_MODE = 'CR' THEN LB2.AMOUNT ELSE 0 END) AS AMOUNT_CR, " +
                                "CASE WHEN (SUM(CASE WHEN LB2.TRANS_MODE = 'DR' " +
                                "               THEN LB2.AMOUNT ELSE - LB2.AMOUNT END) >= 0 ) " +
                                "     THEN 'DR' ELSE 'CR' END AS TRANS_MODE " +
                                "FROM MASTER_LEDGER AS ML " +
                                "INNER JOIN MASTER_LEDGER_GROUP AS LG " +
                                "ON ML.GROUP_ID = LG.GROUP_ID " +
                                "INNER JOIN " +
                                "     (SELECT LB.BALANCE_DATE, LB.BRANCH_ID, LB.PROJECT_ID, LB.LEDGER_ID, LB.AMOUNT, LB.TRANS_MODE " +
                                "      FROM LEDGER_BALANCE AS LB " +
                                "      LEFT JOIN (SELECT LBA.BRANCH_ID, LBA.PROJECT_ID, LBA.LEDGER_ID, MAX(LBA.BALANCE_DATE) AS BAL_DATE " +
                                "                 FROM LEDGER_BALANCE LBA " +
                                "                 WHERE 1 = 1 {AND LBA.BALANCE_DATE <= ?BALANCE_DATE} " +
                                "                 GROUP BY LBA.BRANCH_ID, LBA.PROJECT_ID, LBA.LEDGER_ID) AS LB1 " +
                                "      ON LB.BRANCH_ID = LB1.BRANCH_ID " +
                                "      AND LB.PROJECT_ID = LB1.PROJECT_ID " +
                                "      AND LB.LEDGER_ID = LB1.LEDGER_ID " +
                                "      WHERE LB.BRANCH_ID IN (?BRANCH_ID) AND LB.PROJECT_ID IN (?PROJECT_ID) " +
                                "      AND LB.BALANCE_DATE = LB1.BAL_DATE) LB2 " +
                                "ON ML.LEDGER_ID = LB2.LEDGER_ID " +
                                "WHERE 1 = 1 {AND LG.GROUP_ID IN (?GROUP_ID)} " +
                                "{AND ML.LEDGER_ID IN (?LEDGER_ID)} " +
                                "GROUP BY LG.GROUP_ID, LG.GROUP_CODE, LG.LEDGER_GROUP, " +
                                "ML.LEDGER_ID, ML.LEDGER_CODE, ML.LEDGER_NAME ORDER BY SORT_ID ASC";
                        break;
                    }

                /*case SQLCommand.TransBalance.FetchLedgerName:
                    {
                        query = "SELECT LEDGER_NAME FROM MASTER_LEDGER WHERE LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.TransBalance.FetchGroupName:
                    {
                        query = "SELECT LEDGER_GROUP FROM MASTER_LEDGER_GROUP WHERE GROUP_ID=?GROUP_ID";
                        break;
                    }*/
                case SQLCommand.TransBalance.FetchCCOPBalance: //??
                    {
                        query = "SELECT (T.DEBIT - T.DEBIT) AS AMOUNT FROM " +
                                "(SELECT " +
                                " COST_CENTRE_ID, " +
                                " CASE WHEN TRANS_MODE ='CR' THEN IFNULL(SUM(AMOUNT),0)  END AS CREDIT, " +
                                " CASE WHEN TRANS_MODE ='DR' THEN IFNULL(SUM(AMOUNT),0) END AS DEBIT, " +
                                " TRANS_MODE " +
                                " FROM PROJECT_COSTCENTRE " +
                                " WHERE PROJECT_ID IN (?PROJECT_ID) AND COST_CENTRE_ID IN (?COST_CENTRE_ID) ) AS T ";
                        break;
                    }
                case SQLCommand.TransBalance.DeleteTransBalance:
                    {
                        query = "DELETE FROM LEDGER_BALANCE " +
                                "WHERE BRANCH_ID = ?BRANCH_ID " +
                                "AND BALANCE_DATE >= ?VOUCHER_DATE AND TRANS_FLAG='TR';";
                        break;
                    }

                case SQLCommand.TransBalance.BulkBalanceRefresh:
                    {
                        query = "SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS " +
                                "WHERE BRANCH_ID = ?BRANCH_ID " +
                                "AND STATUS = 1 AND VOUCHER_DATE >= ?VOUCHER_DATE " +
                                "{AND PROJECT_ID = ?PROJECT_ID} ORDER BY VOUCHER_DATE";
                        break;
                    }
                case SQLCommand.TransBalance.AllProjectBalanceRefresh:
                    {
                        query = "SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS " +
                                "WHERE BRANCH_ID = ?BRANCH_ID AND STATUS = 1 ORDER BY VOUCHER_DATE;";
                        break;
                    }
            }

            return query;
        }

        #endregion
    }
}
