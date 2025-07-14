using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class FixedDepositSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.FixedDeposit).FullName)
            {
                query = GetFixedDepositSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the Fixed Deposit details.
        /// </summary>
        /// <returns></returns>
        private string GetFixedDepositSQL()
        {
            string query = "";
            SQLCommand.FixedDeposit sqlCommandId = (SQLCommand.FixedDeposit)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {

                case SQLCommand.FixedDeposit.FixedDepositFetchAll:
                    {

                        #region Fixed Deposit

                        query = "SELECT " +
                                      "MA.BANK_ACCOUNT_ID, " +
                                      "MA.ACCOUNT_CODE, " +
                                      "MA.ACCOUNT_NUMBER, " +
                                      "MB.BANK, " +
                                      "MB.BRANCH, " +
                                      "MA.DATE_OPENED, " +
                                      "MA.DATE_CLOSED " +
                                    "FROM " +
                                      "MASTER_BANK_ACCOUNT MA LEFT JOIN  MASTER_BANK MB ON MA.BANK_ID=MB.BANK_ID ";

                        break;
                    }
                case SQLCommand.FixedDeposit.FixedDepositFetch:
                    {
                        query = "SELECT " +
                                      "LEDGER_ID " +
                                    "FROM " +
                                      "MASTER_LEDGER WHERE BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID";
                        break;
                    }
                        #endregion

                #region BreakUp
                case SQLCommand.FixedDeposit.BreakUpAdd:
                    {
                        query = @"INSERT INTO FD_REGISTERS 
                                (
                                    ACCOUNT_NO,
                                    FD_NO,
                                    INVESTED_ON,
                                    MATURITY_DATE,       
                                    AMOUNT,
                                    INTEREST_RATE,
                                    INTEREST_AMOUNT,
                                    BANK_ACCOUNT_ID,
                                    STATUS,
                                    TRANS_MODE,
                                    IS_INTEREST_RECEIVED_PERIODICALLY,
                                    INTEREST_TERM,
                                    INTEREST_PERIOD
                                 )
                                VALUES
                               (
                                    ?ACCOUNT_NO,
                                    ?FD_NO,
                                    ?INVESTED_ON,
                                    ?MATURITY_DATE,       
                                    ?AMOUNT,
                                    ?INTEREST_RATE,
                                    ?INTEREST_AMOUNT,
                                    ?BANK_ACCOUNT_ID,
                                    ?STATUS,
                                    ?TRANS_MODE,
                                    ?IS_INTEREST_RECEIVED_PERIODICALLY,
                                    ?INTEREST_TERM,
                                    ?INTEREST_PERIOD
                                );";
                        break;
                    }
                case SQLCommand.FixedDeposit.BreakUpDelete:
                    {
                        query = "DELETE FROM FD_REGISTERS WHERE ACCOUNT_NO=?ACCOUNT_NUMBER AND TRANS_MODE='OP' AND STATUS =0;";
                        break;
                    }
                case SQLCommand.FixedDeposit.BreakUpFetchByAccountNo:
                    {
                        query = "SELECT FD_NO,INVESTED_ON,MATURITY_DATE,AMOUNT,INTEREST_RATE,INTEREST_AMOUNT " +
                                "FROM FD_REGISTERS WHERE ACCOUNT_NO=?ACCOUNT_NO AND TRANS_MODE='OP' AND STATUS=0;;";
                        break;
                    }
                case SQLCommand.FixedDeposit.FetchFDByID:
                    {
                        query = @"SELECT FD_REGISTER_ID,
                                        DATE_FORMAT(DATE_OPENED, '%d-%m-%Y') AS DATE_OPENED,
                                        INTEREST_AMOUNT,
                                        BANK,
                                        BRANCH,
                                        MBA.BANK_ACCOUNT_ID,
                                        ACCOUNT_NUMBER,
                                        DATE_OPENED,
                                        MBA.PERIOD_YEAR,
                                        MBA.PERIOD_MTH,
                                        MBA.PERIOD_DAY,
                                        MBA.INTEREST_RATE,
                                        MBA.MATURITY_DATE,
                                        FD.IS_INTEREST_RECEIVED_PERIODICALLY,
                                        FD.INTEREST_TERM,
                                        FD.INTEREST_PERIOD
                                    FROM MASTER_BANK_ACCOUNT MBA
                                    LEFT JOIN FD_REGISTERS FD
                                    ON MBA.BANK_ACCOUNT_ID = FD.BANK_ACCOUNT_ID
                                    LEFT JOIN MASTER_BANK MB
                                    ON MBA.BANK_ID = MB.BANK_ID
                                    WHERE MBA.BANK_ACCOUNT_ID = ?BANK_ACCOUNT_ID;";

                        break;
                    }
                case SQLCommand.FixedDeposit.UpdateFD:
                    {
                        query = "UPDATE MASTER_BANK_ACCOUNT " +
                                 "SET " +
                                      "PERIOD_YEAR=?PERIOD_YEAR," +
                                      "PERIOD_MTH=?PERIOD_MTH," +
                                      "PERIOD_DAY=?PERIOD_DAY," +
                                      "INTEREST_RATE=?INTEREST_RATE," +
                                      "MATURITY_DATE=?MATURITY_DATE," +
                                      "AMOUNT=?AMOUNT " +
                                "WHERE BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID;";
                        break;
                    }
                case SQLCommand.FixedDeposit.FDRegisterAdd:
                    {
                        query = @"INSERT INTO FD_REGISTERS
                                  (
                                        ACCOUNT_NO, 
                                        INVESTED_ON, 
                                        MATURITY_DATE, 
                                        AMOUNT, 
                                        INTEREST_RATE, 
                                        INTEREST_AMOUNT, 
                                        BANK_ACCOUNT_ID, 
                                        STATUS, 
                                        TRANS_MODE,
                                        PERIOD_YEAR,
                                        PERIOD_MTH,
                                        PERIOD_DAY,
                                        IS_INTEREST_RECEIVED_PERIODICALLY,
                                        INTEREST_TERM,
                                        INTEREST_PERIOD
                                    )
                             VALUES(
                                        ?ACCOUNT_NO, 
                                        ?INVESTED_ON, 
                                        ?MATURITY_DATE, 
                                        ?AMOUNT, 
                                        ?INTEREST_RATE, 
                                        ?INTEREST_AMOUNT, 
                                        ?BANK_ACCOUNT_ID, 
                                        ?STATUS, 
                                        ?TRANS_MODE,
                                        ?PERIOD_YEAR,
                                        ?PERIOD_MTH,
                                        ?PERIOD_DAY,
                                        ?IS_INTEREST_RECEIVED_PERIODICALLY,
                                        ?INTEREST_TERM,
                                        ?INTEREST_PERIOD
                                )";
                        break;
                    }
                case SQLCommand.FixedDeposit.FDRegisterUpdate:
                    {
                        query = @"UPDATE FD_REGISTERS 
                                  SET
                                        ACCOUNT_NO=?ACCOUNT_NO, 
                                        FD_NO=?FD_NO, 
                                        INVESTED_ON=?INVESTED_ON, 
                                        MATURITY_DATE=?MATURITY_DATE, 
                                        AMOUNT=?AMOUNT, 
                                        INTEREST_RATE=?INTEREST_RATE, 
                                        INTEREST_AMOUNT=?INTEREST_AMOUNT, 
                                        PERIOD_YEAR=?PERIOD_YEAR,
                                        PERIOD_MTH=?PERIOD_MTH,
                                        PERIOD_DAY=?PERIOD_DAY, 
                                        BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID, 
                                        STATUS=?STATUS, 
                                        TRANS_MODE=?TRANS_MODE,
                                        IS_INTEREST_RECEIVED_PERIODICALLY=?IS_INTEREST_RECEIVED_PERIODICALLY,
                                        INTEREST_TERM=?INTEREST_TERM,
                                        INTEREST_PERIOD=?INTEREST_PERIOD
                                    WHERE FD_REGISTER_ID=?FD_REGISTER_ID ";
                        break;
                    }

                case SQLCommand.FixedDeposit.FetchFDNumber:
                    {
                        query = "SELECT FD_REGISTER_ID FROM FD_REGISTERS  WHERE BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID AND TRANS_MODE=?TRANS_MODE AND STATUS =1";
                        break;
                    }
                case SQLCommand.FixedDeposit.ChangeProjectForOPFD:
                    {
                        //1. Map FD ledgers, 2. Update FD invested/interest voucher/withdrwa voucher. 3. update fd opening balance
                        query = @"INSERT INTO PROJECT_LEDGER (PROJECT_ID, LEDGER_ID)
                                    SELECT PROJECT_ID, LEDGER_ID FROM (SELECT ?NEW_PROJECT_ID AS PROJECT_ID,  LEDGER_ID FROM FD_ACCOUNT WHERE FD_ACCOUNT_ID = ?FD_ACCOUNT_ID UNION
                                    SELECT ?NEW_PROJECT_ID AS PROJECT_ID, BANK_LEDGER_ID FROM FD_ACCOUNT WHERE FD_ACCOUNT_ID = ?FD_ACCOUNT_ID UNION
                                    SELECT ?NEW_PROJECT_ID AS PROJECT_ID, BANK_LEDGER_ID FROM FD_RENEWAL WHERE FD_ACCOUNT_ID = ?FD_ACCOUNT_ID GROUP BY BANK_LEDGER_ID UNION
                                    SELECT ?NEW_PROJECT_ID AS PROJECT_ID, INTEREST_LEDGER_ID FROM FD_RENEWAL WHERE FD_ACCOUNT_ID = ?FD_ACCOUNT_ID GROUP BY INTEREST_LEDGER_ID) AS PROJECT_LEDGER
                                    WHERE LEDGER_Id >0 ON DUPLICATE KEY UPDATE PROJECT_ID = VALUES(PROJECT_ID), LEDGER_ID= VALUES(LEDGER_ID);
                                    
                                    UPDATE VOUCHER_MASTER_TRANS SET PROJECT_ID = ?NEW_PROJECT_ID, MODIFIED_ON = NOW(), MODIFIED_BY = ?MODIFIED_BY, MODIFIED_BY_NAME = ?USER_NAME 
                                           WHERE VOUCHER_ID IN (SELECT FD_VOUCHER_ID FROM FD_ACCOUNT WHERE FD_ACCOUNT_ID = ?FD_ACCOUNT_ID);
                                    UPDATE VOUCHER_MASTER_TRANS SET PROJECT_ID = ?NEW_PROJECT_ID, MODIFIED_ON = NOW(), MODIFIED_BY = ?MODIFIED_BY, MODIFIED_BY_NAME = ?USER_NAME 
                                           WHERE VOUCHER_ID IN (SELECT FD_VOUCHER_ID FROM FD_RENEWAL WHERE FD_ACCOUNT_ID = ?FD_ACCOUNT_ID);
                                    UPDATE VOUCHER_MASTER_TRANS SET PROJECT_ID = ?NEW_PROJECT_ID, MODIFIED_ON = NOW(), MODIFIED_BY = ?MODIFIED_BY, MODIFIED_BY_NAME = ?USER_NAME
                                           WHERE VOUCHER_ID IN (SELECT FD_INTEREST_VOUCHER_ID FROM FD_RENEWAL WHERE FD_ACCOUNT_ID = ?FD_ACCOUNT_ID);
                                    
                                    UPDATE FD_ACCOUNT SET PROJECT_ID = ?NEW_PROJECT_ID WHERE  FD_ACCOUNT_ID = ?FD_ACCOUNT_ID;

                                    DELETE FROM LEDGER_BALANCE WHERE BALANCE_DATE = ?BALANCE_DATE AND PROJECT_ID IN (?PROJECT_ID, ?NEW_PROJECT_ID) AND LEDGER_ID = ?LEDGER_ID;

                                    INSERT INTO LEDGER_BALANCE (BALANCE_DATE, PROJECT_ID, LEDGER_ID, AMOUNT, TRANS_MODE, TRANS_FC_MODE, TRANS_FLAG, BRANCH_ID)
                                    SELECT ?BALANCE_DATE AS BALANCE_DATE, PROJECT_ID, LEDGER_ID, SUM(AMOUNT) AMOUNT, 'DR' TRANS_MODE, TRANS_MODE, 'OP' TRANS_FLAG, BRANCH_ID
                                    FROM FD_ACCOUNT WHERE TRANS_TYPE = 'OP' AND PROJECT_ID IN (?PROJECT_ID, ?NEW_PROJECT_ID) GROUP BY PROJECT_ID, LEDGER_ID
                                    ON DUPLICATE KEY UPDATE BALANCE_DATE = VALUES(BALANCE_DATE), PROJECT_ID = VALUES(PROJECT_ID),
                                    LEDGER_ID = VALUES(LEDGER_ID), BRANCH_ID = VALUES(BRANCH_ID);";
                        break;
                    }
                case SQLCommand.FixedDeposit.ChangeProjectForInvestmentFD:
                    {
                        //1. Map FD ledgers, 2. Update FD invested/interest voucher/withdrwa voucher. 
                        query = @"INSERT INTO PROJECT_LEDGER (PROJECT_ID, LEDGER_ID)
                                    SELECT PROJECT_ID, LEDGER_ID FROM (SELECT ?NEW_PROJECT_ID AS PROJECT_ID,  LEDGER_ID FROM FD_ACCOUNT WHERE FD_ACCOUNT_ID = ?FD_ACCOUNT_ID UNION
                                    SELECT ?NEW_PROJECT_ID AS PROJECT_ID, BANK_LEDGER_ID FROM FD_ACCOUNT WHERE FD_ACCOUNT_ID = ?FD_ACCOUNT_ID UNION
                                    SELECT ?NEW_PROJECT_ID AS PROJECT_ID, BANK_LEDGER_ID FROM FD_RENEWAL WHERE FD_ACCOUNT_ID = ?FD_ACCOUNT_ID GROUP BY BANK_LEDGER_ID UNION
                                    SELECT ?NEW_PROJECT_ID AS PROJECT_ID, INTEREST_LEDGER_ID FROM FD_RENEWAL WHERE FD_ACCOUNT_ID = ?FD_ACCOUNT_ID GROUP BY INTEREST_LEDGER_ID) AS PROJECT_LEDGER
                                    WHERE LEDGER_Id >0 ON DUPLICATE KEY UPDATE PROJECT_ID = VALUES(PROJECT_ID), LEDGER_ID= VALUES(LEDGER_ID);
                                    
                                    UPDATE VOUCHER_MASTER_TRANS SET PROJECT_ID = ?NEW_PROJECT_ID, MODIFIED_ON = NOW(), MODIFIED_BY = ?MODIFIED_BY, MODIFIED_BY_NAME = ?USER_NAME 
                                           WHERE VOUCHER_ID IN (SELECT FD_VOUCHER_ID FROM FD_ACCOUNT WHERE FD_ACCOUNT_ID = ?FD_ACCOUNT_ID);
                                    UPDATE VOUCHER_MASTER_TRANS SET PROJECT_ID = ?NEW_PROJECT_ID, MODIFIED_ON = NOW(), MODIFIED_BY = ?MODIFIED_BY, MODIFIED_BY_NAME = ?USER_NAME 
                                           WHERE VOUCHER_ID IN (SELECT FD_VOUCHER_ID FROM FD_RENEWAL WHERE FD_ACCOUNT_ID = ?FD_ACCOUNT_ID);
                                    UPDATE VOUCHER_MASTER_TRANS SET PROJECT_ID = ?NEW_PROJECT_ID, MODIFIED_ON = NOW(), MODIFIED_BY = ?MODIFIED_BY, MODIFIED_BY_NAME = ?USER_NAME 
                                           WHERE VOUCHER_ID IN (SELECT FD_INTEREST_VOUCHER_ID FROM FD_RENEWAL WHERE FD_ACCOUNT_ID = ?FD_ACCOUNT_ID);

                                    UPDATE FD_ACCOUNT SET PROJECT_ID = ?NEW_PROJECT_ID WHERE  FD_ACCOUNT_ID = ?FD_ACCOUNT_ID";
                        break;
                    }
                case SQLCommand.FixedDeposit.IsFDVouchersExists:
                    {
                        //1. FD Vouchers in Vouchers , 2. REnewals, Investment and Openings
                        query = @"SELECT * FROM ((SELECT VM.VOUCHER_ID FROM VOUCHER_MASTER_TRANS VM
                                    INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VM.VOUCHER_ID AND VT.LEDGER_ID = ?LEDGER_ID
                                    WHERE VM.PROJECT_ID = ?PROJECT_ID AND VM.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO AND VM.VOUCHER_SUB_TYPE = 'FD' AND VM.STATUS = 1 LIMIT 1)
                                    UNION ALL
                                    (SELECT FD_ACCOUNT_ID FROM FD_RENEWAL
                                    WHERE BANK_LEDGER_ID = ?LEDGER_ID AND RENEWAL_DATE BETWEEN ?DATE_FROM AND ?DATE_TO AND STATUS = 1 AND
                                          FD_ACCOUNT_ID IN ( SELECT FD_ACCOUNT_ID FROM FD_ACCOUNT WHERE PROJECT_ID = ?PROJECT_ID AND STATUS = 1 ) LIMIT 1)
                                    UNION ALL
                                    (SELECT FD_ACCOUNT_ID FROM FD_ACCOUNT WHERE PROJECT_ID = ?PROJECT_ID AND BANK_LEDGER_ID = ?LEDGER_ID
                                      AND INVESTMENT_DATE BETWEEN ?DATE_FROM AND ?DATE_TO AND STATUS = 1 LIMIT 1)) AS T";
                        break;
                    }
                case SQLCommand.FixedDeposit.FetchFDAccountsExistsByInvestmentType:
                    {
                        query = @"SELECT FD.FD_ACCOUNT_ID, ML.FD_INVESTMENT_TYPE_ID FROM FD_ACCOUNT FD 
                                    INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = FD.LEDGER_ID AND ML.FD_INVESTMENT_TYPE_ID  = ?FD_INVESTMENT_TYPE_ID";
                        break;
                    }
                case SQLCommand.FixedDeposit.FetchFDRenewalsWithdrwals:
                    {
                        query = @"SELECT FR.FD_ACCOUNT_ID, FR.RENEWAL_DATE, FR.MATURITY_DATE, FD_TYPE AS TRANS_TYPE
                                FROM FD_RENEWAL FR
                                WHERE FR.STATUS = 1 AND FR.FD_ACCOUNT_ID = ?FD_ACCOUNT_ID AND FD_TYPE IN ('WD', 'RN')
                                UNION ALL
                                SELECT FD.FD_ACCOUNT_ID, FD.INVESTMENT_DATE, FD.MATURED_ON, TRANS_TYPE
                                FROM FD_ACCOUNT FD WHERE FD.STATUS = 1 AND FD.FD_ACCOUNT_ID = ?FD_ACCOUNT_ID";

                        break;
                    }
                case SQLCommand.FixedDeposit.FetchFDAccountByMaturityDate:
                    {
                        //query = "SELECT\n" +
                        //"       IFNULL(FDR.MATURITY_DATE, FDA.MATURED_ON) AS MATURITY_DATE,\n" +
                        //"       FDA.FD_ACCOUNT_NUMBER,\n" +
                        //"       FDA.AMOUNT + IFNULL(FDRO.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                        //"       IFNULL(FDRO.WITHDRAWAL_AMOUNT, 0) AS PRINCIPLE_AMOUNT,\n" +
                        //"       FDA.AMOUNT + IFNULL(FDRO.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                        //"       IFNULL(FDRO.WITHDRAWAL_AMOUNT, 0) +\n" +
                        //"       IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0) AS TOTAL_AMOUNT,\n" +
                        //"       IFNULL(FDR.WITHDRAWAL_AMOUNT, 0) AS WITHDRAWAL_AMOUNT,\n" +
                        //"       FDA.AMOUNT + IFNULL(FDRO.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                        //"       IFNULL(FDRO.WITHDRAWAL_AMOUNT, 0) +\n" +
                        //"       IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                        //"       IFNULL(FDR.WITHDRAWAL_AMOUNT, 0) AS BALANCE_AMOUNT,\n" +
                        //"       IF(FDA.AMOUNT + IFNULL(FDRO.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                        //"          IFNULL(FDRO.WITHDRAWAL_AMOUNT, 0) +\n" +
                        //"          IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                        //"          IFNULL(FDR.WITHDRAWAL_AMOUNT, 0) = 0,\n" +
                        //"          'Closed',\n" +
                        //"          'Active') AS CLOSING_STATUS\n" +
                        //"  FROM FD_ACCOUNT AS FDA\n" +
                        //"  LEFT JOIN (SELECT FD_ACCOUNT_ID,\n" +
                        //"                    MAX(MATURITY_DATE) AS MATURITY_DATE,\n" +
                        //"                    MAX(RENEWAL_DATE) AS RENEWAL_DATE,\n" +
                        //"                    SUM(IF(RENEWAL_TYPE = 'ACI', 0, INTEREST_AMOUNT)) AS INTEREST_AMOUNT,\n" +
                        //"                    SUM(IF(RENEWAL_TYPE = 'ACI', INTEREST_AMOUNT, 0)) AS ACCUMULATED_INTEREST_AMOUNT,\n" +
                        //"                    SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT\n" +
                        //"               FROM FD_RENEWAL\n" +
                        //"              WHERE STATUS = 1 AND IS_DELETED=1\n" +
                        //"                AND RENEWAL_DATE <?DATE_STARTED\n" +
                        //"              GROUP BY FD_ACCOUNT_ID) AS FDRO\n" +
                        //"    ON FDA.FD_ACCOUNT_ID = FDRO.FD_ACCOUNT_ID\n" +
                        //"  LEFT JOIN (SELECT FD_ACCOUNT_ID,\n" +
                        //"                    MAX(MATURITY_DATE) AS MATURITY_DATE,\n" +
                        //"                    MAX(RENEWAL_DATE) AS RENEWAL_DATE,\n" +
                        //"                    INTEREST_RATE,\n" +
                        //"                    IF(INTEREST_TYPE=0,'Simple','Compound') AS INTEREST_MODE,\n" +
                        //"                    SUM(IF(RENEWAL_TYPE = 'ACI', 0, INTEREST_AMOUNT)) AS INTEREST_AMOUNT,\n" +
                        //"                    SUM(IF(RENEWAL_TYPE = 'ACI', INTEREST_AMOUNT, 0)) AS ACCUMULATED_INTEREST_AMOUNT,\n" +
                        //"                    SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT\n" +
                        //"               FROM FD_RENEWAL\n" +
                        //"              WHERE STATUS = 1  AND IS_DELETED=1\n" +
                        //"                AND RENEWAL_DATE BETWEEN ?DATE_STARTED  AND ?DATE_CLOSED\n" +
                        //"              GROUP BY FD_ACCOUNT_ID) AS FDR\n" +
                        //"    ON FDA.FD_ACCOUNT_ID = FDR.FD_ACCOUNT_ID\n" +
                        //"  LEFT JOIN MASTER_BANK AS MBK\n" +
                        //"    ON FDA.BANK_ID = MBK.BANK_ID\n" +
                        //"  LEFT JOIN MASTER_PROJECT MPR\n" +
                        //"    ON FDA.PROJECT_ID = MPR.PROJECT_ID\n" +
                        //"  LEFT JOIN MASTER_LEDGER MLG\n" +
                        //"    ON FDA.LEDGER_ID = MLG.LEDGER_ID\n" +
                        //" WHERE FDA.STATUS = 1\n" +
                        //"   AND FDA.INVESTMENT_DATE <=?DATE_CLOSED\n" +
                        
                        //"AND MPR.PROJECT_ID IN (?PROJECT_ID)\n" +
                        //"  AND  FDA.AMOUNT + IFNULL(FDRO.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                        //"   IFNULL(FDRO.WITHDRAWAL_AMOUNT, 0) +\n" +
                        //"   IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                        //" IFNULL(FDR.WITHDRAWAL_AMOUNT, 0) <> 0";

                         query = "SELECT\n" +
                        "       IFNULL(FDR.MATURITY_DATE, FDA.MATURED_ON) AS MATURITY_DATE,\n" +
                        "       FDA.FD_ACCOUNT_NUMBER,\n" +
                        "       FDA.AMOUNT + IFNULL(FDRO.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                        "       IFNULL(FDRO.WITHDRAWAL_AMOUNT, 0) AS PRINCIPLE_AMOUNT,\n" +
                        "       FDA.AMOUNT + IFNULL(FDRO.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                        "       IFNULL(FDRO.WITHDRAWAL_AMOUNT, 0) +\n" +
                        "       IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0) AS TOTAL_AMOUNT,\n" +
                        "       IFNULL(FDR.WITHDRAWAL_AMOUNT, 0) AS WITHDRAWAL_AMOUNT,\n" +
                        "       FDA.AMOUNT + IFNULL(FDRO.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                        "       IFNULL(FDRO.WITHDRAWAL_AMOUNT, 0) +\n" +
                        "       IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                        "       IFNULL(FDR.WITHDRAWAL_AMOUNT, 0) AS BALANCE_AMOUNT,\n" +
                        "       IF(FDA.AMOUNT + IFNULL(FDRO.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                        "          IFNULL(FDRO.WITHDRAWAL_AMOUNT, 0) +\n" +
                        "          IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                        "          IFNULL(FDR.WITHDRAWAL_AMOUNT, 0) = 0,\n" +
                        "          'Closed',\n" +
                        "          'Active') AS CLOSING_STATUS\n" +
                        "  FROM FD_ACCOUNT AS FDA\n" +
                        "  LEFT JOIN (SELECT FD_ACCOUNT_ID,\n" +
                        "                    MAX(MATURITY_DATE) AS MATURITY_DATE,\n" +
                        "                    MAX(RENEWAL_DATE) AS RENEWAL_DATE,\n" +
                        "                    SUM(IF(RENEWAL_TYPE = 'ACI', 0, INTEREST_AMOUNT)) AS INTEREST_AMOUNT,\n" +
                        "                    SUM(IF(RENEWAL_TYPE = 'ACI', INTEREST_AMOUNT, 0)) AS ACCUMULATED_INTEREST_AMOUNT,\n" +
                        "                    SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT\n" +
                        "               FROM FD_RENEWAL\n" +
                        "              WHERE STATUS = 1 AND IS_DELETED=1\n" +
                        "                AND MATURITY_DATE BETWEEN ?DATE_STARTED  AND ?DATE_CLOSED\n" +
                        "              GROUP BY FD_ACCOUNT_ID) AS FDRO\n" +
                        "    ON FDA.FD_ACCOUNT_ID = FDRO.FD_ACCOUNT_ID\n" +
                        "  LEFT JOIN (SELECT FD_ACCOUNT_ID,\n" +
                        "                    MAX(MATURITY_DATE) AS MATURITY_DATE,\n" +
                        "                    MAX(RENEWAL_DATE) AS RENEWAL_DATE,\n" +
                        "                    INTEREST_RATE,\n" +
                        "                    IF(INTEREST_TYPE=0,'Simple','Compound') AS INTEREST_MODE,\n" +
                        "                    SUM(IF(RENEWAL_TYPE = 'ACI', 0, INTEREST_AMOUNT)) AS INTEREST_AMOUNT,\n" +
                        "                    SUM(IF(RENEWAL_TYPE = 'ACI', INTEREST_AMOUNT, 0)) AS ACCUMULATED_INTEREST_AMOUNT,\n" +
                        "                    SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT\n" +
                        "               FROM FD_RENEWAL\n" +
                        "              WHERE STATUS = 1  AND IS_DELETED=1\n" +
                        "                AND MATURITY_DATE BETWEEN ?DATE_STARTED  AND ?DATE_CLOSED\n" +
                        "              GROUP BY FD_ACCOUNT_ID) AS FDR\n" +
                        "    ON FDA.FD_ACCOUNT_ID = FDR.FD_ACCOUNT_ID\n" +
                        "  LEFT JOIN MASTER_BANK AS MBK\n" +
                        "    ON FDA.BANK_ID = MBK.BANK_ID\n" +
                        "  LEFT JOIN MASTER_PROJECT MPR\n" +
                        "    ON FDA.PROJECT_ID = MPR.PROJECT_ID\n" +
                        "  LEFT JOIN MASTER_LEDGER MLG\n" +
                        "    ON FDA.LEDGER_ID = MLG.LEDGER_ID\n" +
                        " WHERE FDA.STATUS = 1\n" +
                        "  AND FDA.MATURED_ON BETWEEN ?DATE_STARTED  AND ?DATE_CLOSED\n" +
                        " AND\n" +
                        "MPR.PROJECT_ID IN (?PROJECT_ID)\n" +
                        "   AND FDA.AMOUNT + IFNULL(FDRO.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                        "       IFNULL(FDRO.WITHDRAWAL_AMOUNT, 0) <> 0;";

                        break;

                    }
                case SQLCommand.FixedDeposit.FetchFDMasterByFDAccountId:
                      {
                          query = "SELECT FDA.FD_ACCOUNT_ID,\n" +
                                       "       FDA.PROJECT_ID,\n" +
                                       "       FDA.LEDGER_ID,\n" +
                                       "       FDA.FD_VOUCHER_ID,\n" +
                                       "       FDA.BANK_ID,FDA.BANK_LEDGER_ID,\n" +
                                       "       FDA.TRANS_TYPE,FDA.INTEREST_RATE,\n" +
                                       "       FDA.AMOUNT AS 'PRINCIPAL_AMOUNT',\n" +
                                       "       IFNULL(FDR.EXPECTED_MATURITY_VALUE, FDA.EXPECTED_MATURITY_VALUE) AS EXPECTED_MATURITY_VALUE,\n" +
                                       "       IFNULL(FDR.EXPECTED_INTEREST_VALUE, FDA.EXPECTED_INTEREST_VALUE) AS EXPECTED_INTEREST_VALUE,\n" +
                                       "       FDA.INVESTMENT_DATE,FDA.INTEREST_TYPE,\n" +
                                       "       IFNULL(FDR.MATURITY_DATE, FDA.MATURED_ON) AS MATURITY_DATE,\n" +
                                       "       FDA.FD_ACCOUNT_NUMBER, CONCAT(MBK.BANK, ' (', MBK.BRANCH, ')') AS BANK, MLG.LEDGER_NAME,\n" +
                                       "       MPR.PROJECT,\n" +
                                       "       FDA.AMOUNT + IFNULL(FDR.REINVESTED_AMOUNT,0) + IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0) + IFNULL(FDRPO.ACCUMULATED_INTEREST_AMOUNT, 0) - IFNULL(FDR.WITHDRAWAL_AMOUNT, 0) AS AMOUNT, " +
                                       "       FDA.INTEREST_AMOUNT,\n" +
                                       "       IFNULL(FDR.REINVESTED_AMOUNT,0) AS REINVESTED_AMOUNT,\n" +
                                       "       IF(ROUND(FDA.AMOUNT + IFNULL(FDR.REINVESTED_AMOUNT,0)+ IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0) + IFNULL(FDRPO.ACCUMULATED_INTEREST_AMOUNT, 0),2) -\n" +
                                       "        ROUND(IFNULL(FDR.WITHDRAWAL_AMOUNT, 0) + IFNULL(FDRPO.WITHDRAWAL_AMOUNT, 0),2) = 0,\n" +
                                       "          'Closed', 'Active') AS CLOSING_STATUS, FDA.NOTES, FDR.RENEWAL_TYPE, IFNULL(MLG.FD_INVESTMENT_TYPE_ID,0) AS FD_INVESTMENT_TYPE_ID, FDA.STATUS\n" +
                                       "  FROM FD_ACCOUNT AS FDA\n" +
                                       "  LEFT JOIN (SELECT FD_RENEWAL.FD_ACCOUNT_ID, FD_TYPE," +
                                       "                   RENEWAL_TYPE,\n" +
                                       "                     (SELECT EXPECTED_MATURITY_VALUE FROM FD_RENEWAL FDR WHERE FDR.FD_RENEWAL_ID\n" +
                                       "                            = (SELECT MAX(FD_RENEWAL_ID) FROM FD_RENEWAL FDR1 WHERE FDR1.FD_ACCOUNT_ID = FD_RENEWAL.FD_ACCOUNT_ID)\n" +
                                       "                     ) AS EXPECTED_MATURITY_VALUE,\n" +
                                       "                     (SELECT EXPECTED_INTEREST_VALUE FROM FD_RENEWAL FDR WHERE FDR.FD_RENEWAL_ID\n" +
                                       "                            = (SELECT MAX(FD_RENEWAL_ID) FROM FD_RENEWAL FDR1 WHERE FDR1.FD_ACCOUNT_ID = FD_RENEWAL.FD_ACCOUNT_ID)\n" +
                                       "                     ) AS EXPECTED_INTEREST_VALUE,\n" +
                                       "                     SUM(REINVESTED_AMOUNT) AS REINVESTED_AMOUNT,\n" +
                                       "                     MAX(MATURITY_DATE) AS MATURITY_DATE,\n" +
                                       "                     MAX(RENEWAL_DATE) AS RENEWAL_DATE,\n" +
                                       "                     INTEREST_RATE,BANK_LEDGER_ID,\n" +
                                       "                     SUM(IF(RENEWAL_TYPE = 'ACI', 0, INTEREST_AMOUNT)) AS INTEREST_AMOUNT,\n" +
                                       "                     SUM(IF(RENEWAL_TYPE = 'ACI', INTEREST_AMOUNT, 0)) AS ACCUMULATED_INTEREST_AMOUNT,\n" +
                                       "                     SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT\n" +
                                       "                FROM FD_RENEWAL AS FD_RENEWAL \n" +
                                       "               WHERE STATUS = 1 AND FD_TYPE<>'POI' GROUP BY FD_ACCOUNT_ID HAVING MAX(FD_RENEWAL_ID)) AS FDR\n" +
                                       "    ON FDA.FD_ACCOUNT_ID = FDR.FD_ACCOUNT_ID\n" +
                                       "  LEFT JOIN (SELECT FD_ACCOUNT_ID, FD_TYPE,\n" +
                                       "                    RENEWAL_TYPE,\n" +
                                       "                    MAX(MATURITY_DATE) AS MATURITY_DATE,\n" +
                                       "                    MAX(RENEWAL_DATE) AS RENEWAL_DATE,\n" +
                                       "                    INTEREST_RATE,BANK_LEDGER_ID,\n" +
                                       "                    SUM(IF(RENEWAL_TYPE = 'ACI', 0, INTEREST_AMOUNT)) AS INTEREST_AMOUNT,\n" +
                                       "                    SUM(IF(RENEWAL_TYPE = 'ACI', INTEREST_AMOUNT, 0)) AS ACCUMULATED_INTEREST_AMOUNT,\n" +
                                       "                    SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT\n" +
                                       "               FROM FD_RENEWAL WHERE STATUS = 1 AND FD_TYPE='POI' GROUP BY FD_ACCOUNT_ID) AS FDRPO\n" +
                                       "    ON FDA.FD_ACCOUNT_ID = FDRPO.FD_ACCOUNT_ID\n" +
                                       "  LEFT JOIN MASTER_BANK AS MBK ON FDA.BANK_ID = MBK.BANK_ID\n" +
                                       "  LEFT JOIN MASTER_PROJECT MPR ON FDA.PROJECT_ID = MPR.PROJECT_ID\n" +
                                       "  LEFT JOIN MASTER_LEDGER MLG ON FDA.LEDGER_ID = MLG.LEDGER_ID\n" +
                                       " WHERE  FDA.STATUS = 1 AND FDA.FD_ACCOUNT_ID IN (?FD_ACCOUNT_ID)" +
                                       " { AND FDA.TRANS_TYPE IN (?TRANS_TYPE)} {AND FDA.FD_SCHEME =?FD_SCHEME} {AND MPR.PROJECT_ID IN(?PROJECT_ID)}";
                          break;
                      }
                case SQLCommand.FixedDeposit.FetchFDRenewalsByFDAccountId:
                    {
                         query = "SELECT FD_RENEWAL_ID,VMT.VOUCHER_NO,FR.FD_VOUCHER_ID,FR.FD_INTEREST_VOUCHER_ID,\n" +
                                "       FD_ACCOUNT_ID,\n" +
                                "       RENEWAL_DATE,\n" +
                                "       RECEIPT_NO,\n" +
                                "       INTEREST_RATE,FR.INTEREST_TYPE,\n" +
                                "       INTEREST_AMOUNT,\n" +
                                "       FR.TDS_AMOUNT,\n" +
                                "       MATURITY_DATE,\n" +
                                "       INTEREST_LEDGER_ID,\n" +
                                "       BANK_LEDGER_ID,\n" +
                                "       RENEWAL_TYPE, FR.FD_TRANS_MODE,\n" +
                                "    VMT.NARRATION,\n" +
                                "    EXPECTED_MATURITY_VALUE AS 'EXPECTED_MATURITY_VALUE',\n" +
                                "    EXPECTED_INTEREST_VALUE AS 'EXPECTED_INTEREST_VALUE'\n" +
                                " FROM FD_RENEWAL AS FR\n" +
                                " LEFT JOIN VOUCHER_MASTER_TRANS  VMT ON\n" +
                                " FR.FD_VOUCHER_ID=VMT.VOUCHER_ID\n" +
                                " WHERE FD_ACCOUNT_ID IN(?FD_ACCOUNT_ID)";
                        break;
                    }

                case SQLCommand.FixedDeposit.CorrectACKPMAFDMutualFund_Temp:
                    {
                        query = @"UPDATE MASTER_LEDGER SET FD_INVESTMENT_TYPE_ID = 4 WHERE LEDGER_NAME = 'Mutual Funds';

                                    UPDATE FD_ACCOUNT AS FD
                                    INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = FD.LEDGER_ID
                                    SET FD.MF_SCHEME_NAME = ML.LEDGER_NAME, FD.LEDGER_ID = ?LEDGER_ID
                                    WHERE ML.LEDGER_NAME NOT IN ('Fixed Deposit', 'Mutual Funds') AND GROUP_ID = 14;

                                    UPDATE VOUCHER_TRANS AS VT
                                    INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID
                                    SET VT.LEDGER_ID = ?LEDGER_ID
                                        WHERE ML.LEDGER_NAME NOT IN ('Fixed Deposit', 'Mutual Funds') AND GROUP_ID = 14;

                                    UPDATE FD_ACCOUNT SET MATURED_ON = '0001-01-01 00:00:00' WHERE LEDGER_ID = ?LEDGER_ID;
                                    
                                    UPDATE FD_RENEWAL SET MATURITY_DATE = '0001-01-01 00:00:00' 
                                        WHERE FD_ACCOUNT_ID IN (SELECT FD_ACCOUNT_ID FROM FD_ACCOUNT WHERE LEDGER_ID = ?LEDGER_ID);

                                    DELETE PL.* FROM PROJECT_LEDGER PL INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = PL.LEDGER_ID
                                        WHERE ML.LEDGER_NAME NOT IN ('Fixed Deposit', 'Mutual Funds') AND GROUP_ID = 14;

                                    DELETE LB.* FROM LEDGER_BALANCE LB INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = LB.LEDGER_ID
                                        WHERE ML.LEDGER_NAME NOT IN ('Fixed Deposit', 'Mutual Funds') AND GROUP_ID = 14;

                                    DELETE FROM MASTER_LEDGER
                                        WHERE LEDGER_NAME NOT IN ('Fixed Deposit', 'Mutual Funds') AND GROUP_ID = 14;

                                    UPDATE LEDGER_BALANCE LB LEFT JOIN
                                        (SELECT FD.PROJECT_ID, FD.LEDGER_ID, SUM(IFNULL(FD.AMOUNT,0)) OP FROM FD_ACCOUNT FD
                                            WHERE FD.TRANS_TYPE = 'OP' AND FD.STATUS = 1 GROUP BY FD.PROJECT_ID, FD.LEDGER_ID) AS FDOP
                                            ON FDOP.PROJECT_ID = LB.PROJECT_ID AND FDOP.LEDGER_ID = LB.LEDGER_ID
                                        SET LB.AMOUNT = IFNULL(FDOP.OP,0)
                                        WHERE LB.TRANS_FLAG = 'OP' AND LB.LEDGER_ID IN (SELECT LEDGER_ID FROM MASTER_LEDGER ML WHERE ML.GROUP_ID = 14);";

                        break;
                    }


                #endregion

            }
            return query;
        }
        #endregion
    }
}
