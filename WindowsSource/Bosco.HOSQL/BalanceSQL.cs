//Done on 09/05/2017 , Modified by Alwar for getting Cash/Bank/FD Opening/Closing Balance (Query ID :FetchBalance)
// 1. Show zero value balances (OP), Changed Query from INNER JOIN with LEDGER_BALANCE TABLE to LEFT JOIN
// 2. Above LEFT JOIN show all banks which are not mapped with project, so Added INNER JOIN with PROJECT_LEDGER


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.HOSQL
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
                query = GetBalanceSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script

        private string GetBalanceSQL()
        {
            string query = "";
            SQLCommand.TransBalance sqlCommandId = (SQLCommand.TransBalance)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.TransBalance.UpdateBalance:
                    {
                        query = "INSERT INTO LEDGER_BALANCE (BALANCE_DATE, BRANCH_ID, PROJECT_ID, LEDGER_ID, " +
                                "AMOUNT, AMOUNT_FC, TRANS_MODE, TRANS_FC_MODE, TRANS_FLAG) " +  
                                "(SELECT LB.BALANCE_DATE, LB.BRANCH_ID, LB.PROJECT_ID, LEDGER_ID, " +
                                " IFNULL(LB1.AMOUNT, 0) AS AMOUNT, IFNULL(LB1.AMOUNT_FC, 0) AS AMOUNT_FC, " +
                                " IFNULL(LB1.TRANS_MODE, ?TRANS_MODE) AS TRANS_MODE, IFNULL(LB1.TRANS_FC_MODE, ?TRANS_MODE) AS TRANS_FC_MODE, LB.TRANS_FLAG " + 
                                " FROM " +
                                " (SELECT ?BALANCE_DATE AS BALANCE_DATE, ?BRANCH_ID AS BRANCH_ID, ?PROJECT_ID AS PROJECT_ID, " +
                                "  ?LEDGER_ID AS LEDGER_ID, ?TRANS_FLAG AS TRANS_FLAG) AS LB " +
                                " LEFT JOIN " +
                                " (SELECT AMOUNT, AMOUNT_FC, TRANS_MODE, TRANS_FC_MODE FROM LEDGER_BALANCE " + 
                                "  WHERE BALANCE_DATE < ?BALANCE_DATE " +
                                "  AND BRANCH_ID = ?BRANCH_ID AND PROJECT_ID = ?PROJECT_ID AND LEDGER_ID = ?LEDGER_ID " +
                                "  ORDER BY BALANCE_DATE DESC LIMIT 1) AS LB1 ON 1 = 1) " +
                                "ON DUPLICATE KEY UPDATE LEDGER_BALANCE.AMOUNT = LEDGER_BALANCE.AMOUNT; " +
                                "SET @tmpAmt := 0.0, @tmpAmtfc := 0.0; " +
                                "UPDATE LEDGER_BALANCE SET " +
                                "       AMOUNT = CASE WHEN TRANS_MODE = ?TRANS_MODE THEN ABS((@tmpAmt := AMOUNT) + ?AMOUNT) " +
                                "                     WHEN TRANS_MODE <> ?TRANS_MODE THEN ABS((@tmpAmt := AMOUNT) - ?AMOUNT) " +
                                "                     ELSE @tmpAmt := AMOUNT END, " +
                                "       AMOUNT_FC = CASE WHEN TRANS_FC_MODE = ?TRANS_MODE THEN ABS((@tmpAmtfc := AMOUNT_FC) + ?AMOUNT_FC) " +
                                "                     WHEN TRANS_FC_MODE <> ?TRANS_MODE THEN ABS((@tmpAmtfc := AMOUNT_FC) - ?AMOUNT_FC) " +
                                "                     ELSE @tmpAmtfc := AMOUNT_FC END, " +
                                "       TRANS_MODE = CASE WHEN ((TRANS_MODE = ?TRANS_MODE) AND ((@tmpAmt + ?AMOUNT) < 0.0)) " +
                                "                              THEN IF(TRANS_MODE = 'CR', 'DR', 'CR') " +
                                "                         WHEN ((TRANS_MODE <> ?TRANS_MODE) AND ((@tmpAmt - ?AMOUNT) < 0.0)) " +
                                "                              THEN IF(TRANS_MODE = 'CR', 'DR', 'CR') ELSE TRANS_MODE END, " +
                                "       TRANS_FC_MODE = CASE WHEN ((TRANS_FC_MODE = ?TRANS_MODE) AND ((@tmpAmtfc + ?AMOUNT_FC) < 0.0)) " +
                                "                              THEN IF(TRANS_FC_MODE = 'CR', 'DR', 'CR') " +
                                "                         WHEN ((TRANS_FC_MODE <> ?TRANS_MODE) AND ((@tmpAmtfc - ?AMOUNT_FC) < 0.0)) " +
                                "                              THEN IF(TRANS_FC_MODE = 'CR', 'DR', 'CR') ELSE TRANS_FC_MODE END " +
                                "WHERE BALANCE_DATE >= ?BALANCE_DATE AND BRANCH_ID = ?BRANCH_ID " +
                                "AND PROJECT_ID = ?PROJECT_ID AND LEDGER_ID = ?LEDGER_ID;";


                        //query = "INSERT INTO LEDGER_BALANCE (BALANCE_DATE, BRANCH_ID, PROJECT_ID, LEDGER_ID, " +
                        //        "AMOUNT, TRANS_MODE, TRANS_FLAG) " +
                        //        "(SELECT LB.BALANCE_DATE, LB.BRANCH_ID, LB.PROJECT_ID, LEDGER_ID, " +
                        //        " IFNULL(LB1.AMOUNT, 0) AS AMOUNT,  " +
                        //        " IFNULL(LB1.TRANS_MODE, ?TRANS_MODE) AS TRANS_MODE, LB.TRANS_FLAG " +
                        //        " FROM " +
                        //        " (SELECT ?BALANCE_DATE AS BALANCE_DATE, ?BRANCH_ID AS BRANCH_ID, ?PROJECT_ID AS PROJECT_ID, " +
                        //        "  ?LEDGER_ID AS LEDGER_ID, ?TRANS_FLAG AS TRANS_FLAG) AS LB " +
                        //        " LEFT JOIN " +
                        //        " (SELECT AMOUNT, TRANS_MODE FROM LEDGER_BALANCE " +
                        //        "  WHERE BALANCE_DATE < ?BALANCE_DATE " +
                        //        "  AND BRANCH_ID = ?BRANCH_ID AND PROJECT_ID = ?PROJECT_ID AND LEDGER_ID = ?LEDGER_ID " +
                        //        "  ORDER BY BALANCE_DATE DESC LIMIT 1) AS LB1 ON 1 = 1) " +
                        //        "ON DUPLICATE KEY UPDATE LEDGER_BALANCE.AMOUNT = LEDGER_BALANCE.AMOUNT; " +
                        //        "SET @tmpAmt := 0.0 " +
                        //        ";UPDATE LEDGER_BALANCE SET " +
                        //        "       AMOUNT = CASE WHEN TRANS_MODE = ?TRANS_MODE THEN ABS((@tmpAmt := AMOUNT) + ?AMOUNT) " +
                        //        "                     WHEN TRANS_MODE <> ?TRANS_MODE THEN ABS((@tmpAmt := AMOUNT) - ?AMOUNT) " +
                        //        "                     ELSE @tmpAmt := AMOUNT END, " +
                        //        /*"       AMOUNT_FC = CASE WHEN TRANS_MODE = ?TRANS_MODE THEN ABS((@tmpAmtfc := AMOUNT_FC) + ?AMOUNT_FC) " +
                        //        "                     WHEN TRANS_MODE <> ?TRANS_MODE THEN ABS((@tmpAmtfc := AMOUNT_FC) - ?AMOUNT_FC) " +
                        //        "                     ELSE @tmpAmtfc := AMOUNT_FC END, " +*/
                        //        "       TRANS_MODE = CASE WHEN ((TRANS_MODE = ?TRANS_MODE) AND ((@tmpAmt + ?AMOUNT) < 0.0)) " +
                        //        "                              THEN IF(TRANS_MODE = 'CR', 'DR', 'CR') " +
                        //        "                         WHEN ((TRANS_MODE <> ?TRANS_MODE) AND ((@tmpAmt - ?AMOUNT) < 0.0)) " +
                        //        "                              THEN IF(TRANS_MODE = 'CR', 'DR', 'CR') ELSE TRANS_MODE END " +
                        //        "WHERE BALANCE_DATE >= ?BALANCE_DATE AND BRANCH_ID = ?BRANCH_ID " +
                        //        "AND PROJECT_ID = ?PROJECT_ID AND LEDGER_ID = ?LEDGER_ID;";
                        break;
                    }
                case SQLCommand.TransBalance.FetchTransaction:
                    {
                        query = "SELECT VM.VOUCHER_ID, VM.PROJECT_ID, VM.VOUCHER_TYPE, VT.LEDGER_ID, ML.GROUP_ID, VM.VOUCHER_DATE, VT.AMOUNT, " +
                                "VT.TRANS_MODE, VM.BRANCH_ID, IF(VM.IS_MULTI_CURRENCY = 1, VT.EXCHANGE_RATE, 1) AS EXCHANGE_RATE, " +
                                "IF(VM.IS_MULTI_CURRENCY = 1, VM.ACTUAL_AMOUNT, 1) AS ACTUAL_AMOUNT " +
                                "FROM VOUCHER_MASTER_TRANS AS VM " +
                                "INNER JOIN VOUCHER_TRANS AS VT ON VM.VOUCHER_ID = VT.VOUCHER_ID AND VM.BRANCH_ID = VT.BRANCH_ID AND VM.LOCATION_ID=VT.LOCATION_ID " +
                                "INNER JOIN MASTER_LEDGER AS ML ON ML.LEDGER_ID = VT.LEDGER_ID " +
                                "WHERE VM.BRANCH_ID = ?BRANCH_ID AND VM.LOCATION_ID=?LOCATION_ID AND VM.VOUCHER_ID = ?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.TransBalance.FetchOpBalance:
                    {
                        query = "SELECT LB.PROJECT_ID, LB.LEDGER_ID, ML.GROUP_ID, LB.BALANCE_DATE, LB.AMOUNT, LB.AMOUNT_FC, LB.TRANS_MODE, LB.TRANS_FC_MODE " +
                                "FROM LEDGER_BALANCE LB " +
                                "LEFT JOIN MASTER_LEDGER AS ML ON ML.LEDGER_ID = LB.LEDGER_ID " +
                                "WHERE LB.BRANCH_ID = ?BRANCH_ID {AND LB.PROJECT_ID = ?PROJECT_ID} {AND LB.LEDGER_ID = ?LEDGER_ID} AND LB.TRANS_FLAG = 'OP'";
                        break;
                    }
                case SQLCommand.TransBalance.FetchTotalLedgerOpBalance:
                    {
                        query = "SELECT SUM( IF(TRANS_MODE ='DR', IFNULL(AMOUNT, 0), -IFNULL(AMOUNT, 0)) ) AS AMOUNT " +
                                "FROM LEDGER_BALANCE WHERE TRANS_FLAG = 'OP'";
                        break;
                    }
                case SQLCommand.TransBalance.FetchCashBankBaseCurrencyExchangeRate:
                    {
                        query = "SELECT ML.LEDGER_ID, ML.GROUP_ID, IF(ML.OP_EXCHANGE_RATE=0 OR ML.OP_EXCHANGE_RATE IS NULL, 1, OP_EXCHANGE_RATE) AS OP_EXCHANGE_RATE,"+
                                "IF(CUR.EXCHANGE_RATE=0 OR CUR.EXCHANGE_RATE IS NULL, 1, EXCHANGE_RATE) AS EXCHANGE_RATE\n" + 
                                "FROM MASTER_LEDGER ML\n" + 
                                "LEFT JOIN (SELECT COUNTRY_ID, EXCHANGE_RATE FROM MASTER_COUNTRY_CURRENCY_EXCHANGE_RATE\n" +
                                "WHERE ?BALANCE_DATE BETWEEN APPLICABLE_FROM AND APPLICABLE_TO\n" +
                                "GROUP BY COUNTRY_ID) CUR ON CUR.COUNTRY_ID = ML.CUR_COUNTRY_ID\n" +
                                "WHERE ML.LEDGER_ID = ?LEDGER_ID AND ML.GROUP_ID IN (12, 13, 14);";
                        break;
                    }
                case SQLCommand.TransBalance.FetchOpBalanceList:
                    {
                        query = "SELECT PL.PROJECT_ID, PL.LEDGER_ID, " +
                                "BANK_ACCOUNT_ID, ML.GROUP_ID, " +
                                "ML.LEDGER_NAME, LG.LEDGER_GROUP, " +
                                "IFNULL(IF(ML.GROUP_ID IN (12, 13, 14) AND ML.CUR_COUNTRY_ID >0, LB.AMOUNT_FC, LB.AMOUNT),0) AS AMOUNT, " +
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
                                "LEFT JOIN MASTER_LEDGER AS ML ON PL.LEDGER_ID = ML.LEDGER_ID " +
                                "LEFT JOIN MASTER_LEDGER_GROUP AS LG ON ML.GROUP_ID = LG.GROUP_ID " +
                                "LEFT JOIN LEDGER_BALANCE AS LB ON PL.PROJECT_ID = LB.PROJECT_ID AND PL.LEDGER_ID = LB.LEDGER_ID AND LB.TRANS_FLAG = 'OP' " +
                                "WHERE LB.BRANCH_ID = ?BRANCH_ID AND PL.PROJECT_ID = ?PROJECT_ID " +
                                "{AND PL.LEDGER_ID = ?LEDGER_ID} {AND ML.GROUP_ID = ?GROUP_ID} ";
                        break;
                    }
                case SQLCommand.TransBalance.HasBalance:
                    {
                        query = "SELECT AMOUNT FROM LEDGER_BALANCE " +
                                "WHERE BRANCH_ID = ?BRANCH_ID AND PROJECT_ID = ?PROJECT_ID " +
                                "AND LEDGER_ID = ?LEDGER_ID AND (AMOUNT > 0 OR AMOUNT_FC > 0)";
                        break;
                    }
                case SQLCommand.TransBalance.HasLedgerBalanceByDate:
                    {
                        query = "SELECT LB.LEDGER_ID,\n" +
                                "ABS(SUM(CASE WHEN LB.TRANS_MODE = 'DR' THEN IFNULL(LB.AMOUNT,0) ELSE - IFNULL(LB.AMOUNT,0) END)) AS AMOUNT, " +
                                "SUM(CASE WHEN LB.TRANS_MODE = 'DR' THEN LB.AMOUNT ELSE 0 END) AS AMOUNT_DR, " +
                                "SUM(CASE WHEN LB.TRANS_MODE = 'CR' THEN LB.AMOUNT ELSE 0 END) AS AMOUNT_CR, " +
                                "CASE WHEN (SUM(CASE WHEN LB.TRANS_MODE = 'DR' THEN LB.AMOUNT ELSE - LB.AMOUNT END) >= 0 ) " +
                                "     THEN 'DR' ELSE 'CR' END AS TRANS_MODE, " +
                                "ABS(SUM(CASE WHEN LB.TRANS_FC_MODE = 'DR' THEN IFNULL(LB.AMOUNT_FC,0) ELSE - IFNULL(LB.AMOUNT_FC,0) END)) AS AMOUNT_FC, " +
                                "SUM(CASE WHEN LB.TRANS_FC_MODE = 'DR' THEN LB.AMOUNT_FC ELSE 0 END) AS AMOUNT_FC_DR, " +
                                "SUM(CASE WHEN LB.TRANS_FC_MODE = 'CR' THEN LB.AMOUNT_FC ELSE 0 END) AS AMOUNT_FC_CR, " +
                                "CASE WHEN (SUM(CASE WHEN LB.TRANS_FC_MODE = 'DR' THEN LB.AMOUNT_FC ELSE - LB.AMOUNT_FC END) >= 0 ) " +
                                "     THEN 'DR' ELSE 'CR' END AS TRANS_FC_MODE " +
                                "FROM LEDGER_BALANCE AS LB\n" +
                                "LEFT JOIN (SELECT LBA.BRANCH_ID, LBA.PROJECT_ID, LBA.LEDGER_ID, MAX(LBA.BALANCE_DATE) AS BAL_DATE\n" +
                                "     FROM LEDGER_BALANCE LBA\n" +
                                "     WHERE LBA.BALANCE_DATE <= ?BALANCE_DATE {AND LBA.BRANCH_ID IN (?BRANCH_ID)}\n" +
                                "     {AND LBA.PROJECT_ID IN (?PROJECT_ID)} {AND LBA.LEDGER_ID IN (?LEDGER_ID)}\n" +
                                "     GROUP BY LBA.BRANCH_ID, LBA.PROJECT_ID, LBA.LEDGER_ID) AS LB1\n" +
                                "ON LB.BRANCH_ID = LB1.BRANCH_ID AND LB.PROJECT_ID = LB1.PROJECT_ID AND LB.LEDGER_ID = LB1.LEDGER_ID\n" +
                                "WHERE 1=1 {AND LB.BRANCH_ID IN (?BRANCH_ID)} {AND LB.PROJECT_ID IN (?PROJECT_ID)}\n" +
                                "{AND LB.LEDGER_ID IN (?LEDGER_ID)} AND LB.BALANCE_DATE = LB1.BAL_DATE";
                        break;
                    }
                case SQLCommand.TransBalance.DeleteBalance:
                    {
                        query = "DELETE FROM LEDGER_BALANCE " +
                                "WHERE BRANCH_ID = ?BRANCH_ID AND PROJECT_ID = ?PROJECT_ID " +
                                "AND LEDGER_ID = ?LEDGER_ID AND AMOUNT = 0 AND AMOUNT_FC = 0";
                        break;
                    }
                case SQLCommand.TransBalance.FetchGroupSumBalance:
                    {
                        query = "SELECT LG.GROUP_ID, LG.GROUP_CODE, LG.LEDGER_GROUP, " +
                                "ABS(SUM(CASE WHEN LB2.TRANS_MODE = 'DR' THEN LB2.AMOUNT ELSE - LB2.AMOUNT END)) AS AMOUNT, " +
                                "ABS(SUM(CASE WHEN LB2.TRANS_FC_MODE = 'DR' THEN LB2.AMOUNT_FC ELSE - LB2.AMOUNT_FC END)) AS AMOUNT_FC, " +
                                "SUM(CASE WHEN LB2.TRANS_MODE = 'DR' THEN LB2.AMOUNT ELSE 0 END) AS AMOUNT_DR, " +
                                "SUM(CASE WHEN LB2.TRANS_MODE = 'CR' THEN LB2.AMOUNT ELSE 0 END) AS AMOUNT_CR, " +
                                "SUM(CASE WHEN LB2.TRANS_FC_MODE = 'DR' THEN LB2.AMOUNT_FC ELSE 0 END) AS AMOUNT_FC_DR, " +
                                "SUM(CASE WHEN LB2.TRANS_FC_MODE = 'CR' THEN LB2.AMOUNT_FC ELSE 0 END) AS AMOUNT_FC_CR, " +
                                "CASE WHEN (SUM(CASE WHEN LB2.TRANS_MODE = 'DR' THEN LB2.AMOUNT ELSE - LB2.AMOUNT END) >= 0 ) " +
                                "     THEN 'DR' ELSE 'CR' END AS TRANS_MODE, " +
                                "CASE WHEN (SUM(CASE WHEN LB2.TRANS_FC_MODE = 'DR' THEN LB2.AMOUNT_FC ELSE - LB2.AMOUNT_FC END) >= 0 ) " +
                                "     THEN 'DR' ELSE 'CR' END AS TRANS_FC_MODE " +
                                "FROM MASTER_LEDGER AS ML " +
                                "INNER JOIN MASTER_LEDGER_GROUP AS LG " +
                                "ON ML.GROUP_ID = LG.GROUP_ID " +
                                "INNER JOIN PROJECT_LEDGER AS PL ON {PL.PROJECT_ID IN (?PROJECT_ID) AND} PL.LEDGER_ID = ML.LEDGER_ID " +
                                " LEFT JOIN MASTER_BANK_ACCOUNT AS MBA " +
                                " ON ML.LEDGER_ID=MBA.LEDGER_ID " +
                                "INNER JOIN " +
                                "     (SELECT LB.BALANCE_DATE, LB.BRANCH_ID, LB.PROJECT_ID, LB.LEDGER_ID, LB.AMOUNT_FC, LB.AMOUNT, LB.TRANS_MODE, LB.TRANS_FC_MODE " +
                                "      FROM LEDGER_BALANCE AS LB INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = LB.LEDGER_ID " +
                                "      LEFT JOIN (SELECT LBA.BRANCH_ID, LBA.PROJECT_ID, LBA.LEDGER_ID, MAX(LBA.BALANCE_DATE) AS BAL_DATE " +
                                "                 FROM LEDGER_BALANCE LBA " +
                                "                 WHERE 1 = 1 {AND LBA.BALANCE_DATE <= ?BALANCE_DATE} " +
                                "                 GROUP BY LBA.BRANCH_ID, LBA.PROJECT_ID, LBA.LEDGER_ID) AS LB1 " +
                                "      ON LB.BRANCH_ID = LB1.BRANCH_ID AND LB.PROJECT_ID = LB1.PROJECT_ID AND LB.LEDGER_ID = LB1.LEDGER_ID  " +
                                "      WHERE LB.BRANCH_ID IN (?BRANCH_ID) {AND LB.PROJECT_ID IN (?PROJECT_ID)} " +
                                "      AND LB.BALANCE_DATE = LB1.BAL_DATE) LB2 " +
                                "ON ML.LEDGER_ID = LB2.LEDGER_ID AND PL.PROJECT_ID = LB2.PROJECT_ID " +
                                "WHERE LG.GROUP_ID IN (?GROUP_ID) {AND ML.LEDGER_ID in(?LEDGER_ID)} AND ML.STATUS=0 " +
                                " {AND (MBA.DATE_CLOSED IS NULL OR MBA.DATE_CLOSED >= ?DATE_CLOSED) } " + //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                                " {AND IF(?CURRENCY_COUNTRY_ID > 0, ML.CUR_COUNTRY_ID = ?CURRENCY_COUNTRY_ID, 1=1)} " +
                                "GROUP BY LG.GROUP_ID, LG.GROUP_CODE, LG.LEDGER_GROUP";
                        break;
                    }
                case SQLCommand.TransBalance.FetchBalance:
                    {
                        //Make use of this query for all reports Opening and Closing Balance for Cash/Bank/FD
                        query = "SELECT LG.GROUP_ID, LG.GROUP_CODE, LG.LEDGER_GROUP, " +
                                "ML.LEDGER_ID, ML.LEDGER_CODE, ML.LEDGER_NAME,ML.SORT_ID ,  " +
                                "ABS(SUM(CASE WHEN LB2.TRANS_MODE = 'DR' THEN IFNULL(LB2.AMOUNT,0) ELSE - IFNULL(LB2.AMOUNT,0) END)) AS AMOUNT, " +
                                "ABS(SUM(CASE WHEN LB2.TRANS_MODE = 'DR' THEN IFNULL(LB2.AMOUNT_FC,0) ELSE - IFNULL(LB2.AMOUNT_FC,0) END)) AS AMOUNT_FC, " +
                                "SUM(CASE WHEN LB2.TRANS_MODE = 'DR' THEN LB2.AMOUNT ELSE 0 END) AS AMOUNT_DR, " +
                                "SUM(CASE WHEN LB2.TRANS_MODE = 'CR' THEN LB2.AMOUNT ELSE 0 END) AS AMOUNT_CR, " +
                                "SUM(CASE WHEN LB2.TRANS_FC_MODE = 'DR' THEN LB2.AMOUNT_FC ELSE 0 END) AS AMOUNT_FC_DR, " +
                                "SUM(CASE WHEN LB2.TRANS_FC_MODE = 'CR' THEN LB2.AMOUNT_FC ELSE 0 END) AS AMOUNT_FC_CR, " +
                                "CASE WHEN (SUM(CASE WHEN LB2.TRANS_MODE = 'DR' THEN LB2.AMOUNT ELSE - LB2.AMOUNT END) >= 0 ) " +
                                "     THEN 'DR' ELSE 'CR' END AS TRANS_MODE, " +
                                "CASE WHEN (SUM(CASE WHEN LB2.TRANS_FC_MODE = 'DR' THEN LB2.AMOUNT_FC ELSE - LB2.AMOUNT_FC END) >= 0 ) " +
                                "     THEN 'DR' ELSE 'CR' END AS TRANS_FC_MODE, " + 
                                "IFNULL(MC.CURRENCY_SYMBOL, '') AS CURRENCY_SYMBOL " +
                                "FROM MASTER_LEDGER AS ML " +
                                "INNER JOIN MASTER_LEDGER_GROUP AS LG ON ML.GROUP_ID = LG.GROUP_ID " +
                                "INNER JOIN PROJECT_LEDGER AS PL ON {PL.PROJECT_ID IN (?PROJECT_ID) AND} PL.LEDGER_ID = ML.LEDGER_ID " + //On 09/05/2017, To show zero value cash/bank/Fd Opening/Closing Balance Skip unmapped ledgers: Changed INNER JOIN with PROJECT_LEDGERS
                                "LEFT JOIN MASTER_BANK_ACCOUNT AS MBA ON ML.LEDGER_ID=MBA.LEDGER_ID " +
                                "LEFT JOIN " + //On 09/05/2017, To show zero value cash/bank/Fd Opening/Closing Balance: Changed INNER JOIN to LEFT
                                "     (SELECT LB.BALANCE_DATE, LB.BRANCH_ID, LB.PROJECT_ID, LB.LEDGER_ID, LB.AMOUNT, LB.AMOUNT_FC, LB.TRANS_MODE, LB.TRANS_FC_MODE" +
                                "      FROM LEDGER_BALANCE AS LB " +
                                "      LEFT JOIN (SELECT LBA.BRANCH_ID, LBA.PROJECT_ID, LBA.LEDGER_ID, MAX(LBA.BALANCE_DATE) AS BAL_DATE " +
                                "                 FROM LEDGER_BALANCE LBA " +
                                "                 WHERE 1 = 1 {AND LBA.BALANCE_DATE <= ?BALANCE_DATE} " +
                                "                 GROUP BY LBA.BRANCH_ID, LBA.PROJECT_ID, LBA.LEDGER_ID) AS LB1 " +
                                "      ON LB.BRANCH_ID = LB1.BRANCH_ID " +
                                "      AND LB.PROJECT_ID = LB1.PROJECT_ID " +
                                "      AND LB.LEDGER_ID = LB1.LEDGER_ID " +
                                "      WHERE 1=1 { AND LB.BRANCH_ID IN (?BRANCH_ID)} { AND LB.PROJECT_ID IN (?PROJECT_ID)} " +
                                "      AND LB.BALANCE_DATE = LB1.BAL_DATE) LB2 " +
                                "ON ML.LEDGER_ID = LB2.LEDGER_ID AND PL.PROJECT_ID = LB2.PROJECT_ID " +
                                "LEFT JOIN MASTER_COUNTRY MC ON MC.COUNTRY_ID = ML.CUR_COUNTRY_ID "+
                                "WHERE 1 = 1 {AND LG.GROUP_ID IN (?GROUP_ID)} " +
                                " {AND ML.LEDGER_ID IN (?LEDGER_ID)} " +
                                " {AND (MBA.DATE_CLOSED IS NULL OR MBA.DATE_CLOSED >= ?DATE_CLOSED) } " + //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                                " {AND IF(?CURRENCY_COUNTRY_ID > 0, ML.CUR_COUNTRY_ID  = ?CURRENCY_COUNTRY_ID, 1=1)} " +
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
                /*case SQLCommand.TransBalance.FetchCCOPBalance:
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
                    }*/
                case SQLCommand.TransBalance.DeleteTransBalance:
                    {
                        query = "DELETE FROM LEDGER_BALANCE " +
                                "WHERE BRANCH_ID = ?BRANCH_ID " +
                                "AND BALANCE_DATE >= ?VOUCHER_DATE {AND PROJECT_ID = ?PROJECT_ID} {AND LEDGER_ID = ?LEDGER_ID} AND TRANS_FLAG='TR';";
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
                        query = "SELECT VOUCHER_ID, LOCATION_ID FROM VOUCHER_MASTER_TRANS " +
                                "WHERE STATUS = 1 AND VOUCHER_DATE >= ?VOUCHER_DATE {AND BRANCH_ID = ?BRANCH_ID} {AND PROJECT_ID = ?PROJECT_ID} ORDER BY VOUCHER_DATE;";
                        break;
                    }
                case SQLCommand.TransBalance.ProjectLedgerBalanceRefreshByLedger:
                    { 
                        query = @"SELECT VM.VOUCHER_ID, VM.LOCATION_ID 
                                    FROM VOUCHER_MASTER_TRANS VM INNER JOIN VOUCHER_TRANS VT ON VT.BRANCH_ID = VM.BRANCH_ID AND 
                                        VT.VOUCHER_ID = VM.VOUCHER_ID AND VT.LOCATION_ID = VM.LOCATION_ID
                                    WHERE VM.STATUS = 1 {AND VM.PROJECT_ID = ?PROJECT_ID} AND VT.LEDGER_ID = ?LEDGER_ID AND VM.VOUCHER_DATE >= ?VOUCHER_DATE 
                                    {AND VM.BRANCH_ID = ?BRANCH_ID} GROUP BY VM.VOUCHER_ID, VM.LOCATION_ID ORDER BY VOUCHER_DATE;";
                        break;
                    }

                case SQLCommand.TransBalance.FetchBudgetLedgerBalance: // By Aldrin to fetch budget ledger balance from the budget period.
                    {
                        //This is to the old query to get the Logic (chinna on 18.04.201)
                        query = "SELECT\n" +
                                " VOUCHER_ID,\n" +
                                " VOUCHER_DATE,\n" +
                                " BRANCH_ID,\n" +
                                " PROJECT_ID,\n" +
                                " LEDGER_ID,TRANS_MODE,\n" +
                                " ABS(SUM(AMOUNT)) AS AMOUNT\n" +
                                "  FROM (SELECT VMT.VOUCHER_ID,\n" +
                                "               VMT.VOUCHER_DATE,\n" +
                                "               VMT.BRANCH_ID,\n" +
                                "               VMT.PROJECT_ID,\n" +
                                "               VT.LEDGER_ID,\n" +
                                "               SUM(CASE\n" +
                                "                     WHEN VT.TRANS_MODE = 'DR' THEN\n" +
                                "                      VT.AMOUNT\n" +
                                "                     ELSE\n" +
                                "                      -VT.AMOUNT\n" +
                                "                   END) AS AMOUNT,\n" +
                                "               SUM(CASE\n" +
                                "                     WHEN VT.TRANS_MODE = 'DR' THEN\n" +
                                "                      VT.AMOUNT\n" +
                                "                     ELSE\n" +
                                "                      0\n" +
                                "                   END) AS AMOUNT_DR,\n" +
                                "               SUM(CASE\n" +
                                "                     WHEN VT.TRANS_MODE = 'CR' THEN\n" +
                                "                      VT.AMOUNT\n" +
                                "                     ELSE\n" +
                                "                      0\n" +
                                "                   END) AS AMOUNT_CR,\n" +
                                "               VT.TRANS_MODE\n" +
                                "          FROM VOUCHER_MASTER_TRANS AS VMT\n" +
                                "         INNER JOIN VOUCHER_TRANS VT\n" +
                                "            ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                "         WHERE VMT.PROJECT_ID IN (?PROJECT_ID)\n" +
                                "           AND VMT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                "           AND VMT.STATUS = 1\n" +
                                "           AND LEDGER_ID = ?LEDGER_ID\n" +
                                "         GROUP BY VMT.VOUCHER_ID, VT.LEDGER_ID, TRANS_MODE) AS T";
                        break;
                    }
                case SQLCommand.TransBalance.FetchBudgetLedgerBalanceByTransMode:
                    {
                        query = " SELECT\n" +
                                "     PROJECT_ID,\n" +
                                "     LEDGER_ID,\n" +
                                "     TRANS_MODE,\n" +
                                "      SUM(AMOUNT_DR) AS AMOUNT_DR,\n" +
                                "      SUM(AMOUNT_CR) AS AMOUNT_CR FROM (SELECT VMT.VOUCHER_ID,\n" +
                                "               VMT.VOUCHER_DATE,\n" +
                                "               VMT.BRANCH_ID,\n" +
                                "               VMT.PROJECT_ID,\n" +
                                "               VT.LEDGER_ID,\n" +
                                "               SUM(CASE\n" +
                                "                     WHEN VT.TRANS_MODE = 'DR' THEN\n" +
                                "                      VT.AMOUNT\n" +
                                "                     ELSE\n" +
                                "                      -VT.AMOUNT\n" +
                                "                   END) AS AMOUNT,\n" +
                                "               SUM(CASE\n" +
                                "                     WHEN VT.TRANS_MODE = 'DR' THEN\n" +
                                "                      VT.AMOUNT\n" +
                                "                     ELSE\n" +
                                "                      0\n" +
                                "                   END) AS AMOUNT_DR,\n" +
                                "               SUM(CASE\n" +
                                "                     WHEN VT.TRANS_MODE = 'CR' THEN\n" +
                                "                      VT.AMOUNT\n" +
                                "                     ELSE\n" +
                                "                      0\n" +
                                "                   END) AS AMOUNT_CR,\n" +
                                "               VT.TRANS_MODE\n" +
                                "          FROM VOUCHER_MASTER_TRANS AS VMT\n" +
                                "         INNER JOIN VOUCHER_TRANS VT\n" +
                                "            ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                "         WHERE VMT.PROJECT_ID IN (?PROJECT_ID)\n" +
                                "           AND VMT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                "           AND VMT.STATUS = 1\n" +
                                "           AND LEDGER_ID = ?LEDGER_ID AND TRANS_MODE =?TRANS_MODE\n" +
                                "         GROUP BY VMT.VOUCHER_ID, VT.LEDGER_ID, TRANS_MODE) AS T GROUP BY T.TRANS_MODE";

                        //  "         GROUP BY VMT.VOUCHER_ID, VT.LEDGER_ID, TRANS_MODE) AS T GROUP BY T.PROJECT_ID,T.LEDGER_ID,T.TRANS_MODE";
                        break;
                    }
                case SQLCommand.TransBalance.FetchSubLedgerBalance:
                    {
                        query = "SELECT VT.LEDGER_ID, VST.SUB_LEDGER_ID,\n" +
                                    "ABS(SUM(CASE WHEN VST.TRANS_MODE = 'DR' THEN  VST.AMOUNT ELSE -VST.AMOUNT END)) AS AMOUNT,\n" +
                                    "IF((SUM(CASE WHEN VST.TRANS_MODE = 'DR' THEN  VST.AMOUNT ELSE -VST.AMOUNT END))>=0,'DR','CR') AS TRANS_MODE\n" +
                                    "FROM VOUCHER_MASTER_TRANS AS VMT\n" +
                                    "INNER JOIN VOUCHER_TRANS VT ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                    "INNER JOIN VOUCHER_SUB_LEDGER_TRANS VST ON VST.VOUCHER_ID = VMT.VOUCHER_ID AND VST.LEDGER_ID = VT.LEDGER_ID\n" +
                                    "WHERE VMT.PROJECT_ID IN (?PROJECT_ID) AND VMT.STATUS = 1 AND VST.SUB_LEDGER_ID=?SUB_LEDGER_ID {AND VT.LEDGER_ID = ?LEDGER_ID}\n" +
                                    "{AND VMT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO} \n" +
                                    "GROUP BY VT.LEDGER_ID, VST.SUB_LEDGER_ID ORDER BY VT.LEDGER_ID";
                        break;
                    }
                case SQLCommand.TransBalance.ResetLedgerOpeningBalance:
                    {
                        query = "UPDATE LEDGER_BALANCE AS LB\n" +
                                "INNER JOIN MASTER_LEDGER AS ML ON ML.LEDGER_ID = LB.LEDGER_ID\n" +
                                "INNER JOIN MASTER_LEDGER_GROUP AS LG ON LG.GROUP_ID = ML.GROUP_ID\n" +
                                "SET LB.AMOUNT =0\n" +
                                "WHERE LB.PROJECT_ID=?PROJECT_ID AND LB.TRANS_FLAG = 'OP' AND LG.NATURE_ID =?NATURE_ID\n" +
                                "{AND (LG.MAIN_GROUP_ID = ?MAIN_GROUP_ID OR LG.GROUP_ID = ?MAIN_GROUP_ID)}\n" +
                                "{AND ML.GROUP_ID NOT IN (?GROUP_ID)};";
                        break;
                    }
            }

            return query;
        }

        #endregion
    }
}
