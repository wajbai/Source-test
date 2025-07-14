using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class FDRenewalSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.FDRenewal).FullName)
            {
                //  query = GetFDRenewalSQL();
                query = GetFixedRenewalSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the FD Renewal.
        /// </summary>
        /// <returns></returns>
        private string GetFDRenewalSQL()
        {
            string query = "";
            SQLCommand.FDRenewal sqlCommandId = (SQLCommand.FDRenewal)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.FDRenewal.Add:
                    {
                        query = "INSERT INTO FD_REGISTERS " +
                               " (ACCOUNT_NO,FD_NO ," +
                               " INVESTED_ON,MATURITY_DATE, " +
                               " AMOUNT,INTEREST_RATE, " +
                               " INTEREST_AMOUNT,CLOSED_DATE, " +
                               " BANK_ACCOUNT_ID, " +
                               " STATUS,TRANS_MODE,PERIOD_YEAR,PERIOD_MTH,PERIOD_DAY) " +
                               " VALUES(?ACCOUNT_NO,?FD_NO, " +
                               " ?INVESTED_ON,?MATURITY_DATE, " +
                               " ?AMOUNT,?INTEREST_RATE,?INTEREST_AMOUNT,?CLOSED_DATE, " +
                               " ?BANK_ACCOUNT_ID,?STATUS,?TRANS_MODE,?PERIOD_YEAR,?PERIOD_MTH,?PERIOD_DAY) ";
                        break;
                    }

                case SQLCommand.FDRenewal.Update:
                    {
                        query = "UPDATE FD_REGISTERS SET " +
                               " ACCOUNT_NO=?ACCOUNT_NO, " +
                               " FD_NO=?FD_NO, " +
                               " INVESTED_ON=?INVESTED_ON, " +
                               " MATURITY_DATE=?MATURITY_DATE, " +
                               " AMOUNT=?AMOUNT, " +
                               " INTEREST_RATE=?INTEREST_RATE, " +
                               " INTEREST_AMOUNT=?INTEREST_AMOUNT, " +
                               " STATUS=?STATUS,CLOSED_DATE=?CLOSED_DATE, " +
                               " PERIOD_YEAR=?PERIOD_YEAR," +
                               " PERIOD_MTH=?PERIOD_MTH," +
                               " PERIOD_DAY=?PERIOD_DAY" +
                               " WHERE FD_REGISTER_ID=?FD_REGISTER_ID";
                        break;
                    }
                case SQLCommand.FDRenewal.Fetch:
                    {
                        query = "SELECT MA.BANK_ACCOUNT_ID,\n" +
                        "       MA.ACCOUNT_NUMBER,\n" +
                        "       PL.PROJECT_ID,\n" +
                        "       MB.BANK,\n" +
                        "       MB.BRANCH,\n" +
                        "       MA.DATE_OPENED,\n" +
                        "       ML.LEDGER_ID,\n" +
                        "       CONCAT(MB.BANK, CONCAT(' - ', MA.ACCOUNT_NUMBER)) AS 'BANKACCOUNT',\n" +
                        "       FD.FD_NO,\n" +
                        "       ML.LEDGER_NAME,\n" +
                        "       IFNULL(FD.AMOUNT, 0) AS AMOUNT,\n" +
                        "       FD.TRANS_MODE,\n" +
                        "       FD.INVESTED_ON,\n" +
                        "       FD.MATURITY_DATE,\n" +
                        "       FD.STATUS,\n" +
                        "       CASE\n" +
                        "         WHEN FD.STATUS IN (4) THEN\n" +
                        "          'Inactive'\n" +
                        "         WHEN  FD.STATUS IN(3) THEN 'Closed' ELSE \n" +
                        "          'Active'\n" +
                        "       END AS 'FD_STATUS',\n" +
                        "       CONCAT(MP.PROJECT, CONCAT(' - ', MDI.DIVISION)) AS 'PROJECT',\n" +
                        "       DATEDIFF(FD.MATURITY_DATE, CURDATE()) AS DAYS\n" +
                        "\n" +
                        "  FROM FD_REGISTERS FD\n" +
                        " INNER JOIN MASTER_BANK_ACCOUNT MA\n" +
                        "    ON FD.BANK_ACCOUNT_ID = MA.BANK_ACCOUNT_ID\n" +
                        " INNER JOIN MASTER_BANK MB\n" +
                        "    ON MB.BANK_ID = MA.BANK_ID\n" +
                        " INNER JOIN MASTER_LEDGER ML\n" +
                        "    ON FD.BANK_ACCOUNT_ID = ML.BANK_ACCOUNT_ID\n" +
                        " INNER JOIN PROJECT_LEDGER PL\n" +
                        "    ON ML.LEDGER_ID = PL.LEDGER_ID\n" +
                        " INNER JOIN LEDGER_BALANCE LB\n" +
                        "    ON PL.LEDGER_ID = LB.LEDGER_ID\n" +
                        " INNER JOIN MASTER_PROJECT MP\n" +
                        "    ON MP.PROJECT_ID = LB.PROJECT_ID\n" +
                        " INNER JOIN MASTER_DIVISION MDI\n" +
                        "    ON MP.DIVISION_ID = MDI.DIVISION_ID\n" +
                        " WHERE GROUP_ID = 14\n" +
                        "   AND FD.STATUS IN (?STATUS)\n" +
                        "   AND FIND_IN_SET(FD.TRANS_MODE, 'OP,TR')\n" +
                        " GROUP BY PROJECT_ID, BANK, LEDGER_ID, FD_NO\n" +
                        " ORDER BY STATUS DESC";


                        break;
                    }
                case SQLCommand.FDRenewal.FetchAll:
                    {
                        query = "SELECT MA.BANK_ACCOUNT_ID, FD.FD_REGISTER_ID,\n" +
                        "       MA.ACCOUNT_NUMBER,\n" +
                        "       PL.PROJECT_ID,\n" +
                        "       MB.BANK,\n" +
                        "       MB.BRANCH,\n" +
                        "       MA.DATE_OPENED,\n" +
                        "       ML.LEDGER_ID,\n" +
                        "       CONCAT(MB.BANK, CONCAT(' - ', MA.ACCOUNT_NUMBER)) AS 'BANKACCOUNT',\n" +
                        "       FD.FD_NO,\n" +
                        "       ML.LEDGER_NAME,\n" +
                        "       IFNULL(FD.AMOUNT, 0) AS AMOUNT,\n" +
                        "       FD.TRANS_MODE,\n" +
                        "       FD.INVESTED_ON,\n" +
                        "       FD.MATURITY_DATE,\n" +
                        "       FD.STATUS,\n" +
                        "       CASE\n" +
                        "         WHEN FD.STATUS IN (4) THEN\n" +
                        "          'Inactive'\n" +
                        "         WHEN FD.STATUS IN(2) THEN 'Closed' ELSE\n" +
                        "          'Active'\n" +
                        "       END AS 'FD_STATUS',\n" +
                        "       CONCAT(MP.PROJECT, CONCAT(' - ', MDI.DIVISION)) AS 'PROJECT',\n" +
                        "       DATEDIFF(FD.MATURITY_DATE,  FD.INVESTED_ON) AS DAYS,\n" +
                        "       CASE \n" +
                        "       WHEN FD.IS_INTEREST_RECEIVED_PERIODICALLY = 0 THEN \n" +
                        "       'No'\n" +
                        "       WHEN FD.IS_INTEREST_RECEIVED_PERIODICALLY = 1 THEN \n" +
                        "       'Yes' \n" +
                        "       ELSE \n" +
                        "       '' \n" +
                        "       END AS 'PERIODICALLY'\n" +
                        "\n" +
                        "  FROM FD_REGISTERS FD\n" +
                        " INNER JOIN MASTER_BANK_ACCOUNT MA\n" +
                        "    ON FD.BANK_ACCOUNT_ID = MA.BANK_ACCOUNT_ID\n" +
                        " INNER JOIN MASTER_BANK MB\n" +
                        "    ON MB.BANK_ID = MA.BANK_ID\n" +
                        " INNER JOIN MASTER_LEDGER ML\n" +
                        "    ON FD.BANK_ACCOUNT_ID = ML.BANK_ACCOUNT_ID\n" +
                        " INNER JOIN PROJECT_LEDGER PL\n" +
                        "    ON ML.LEDGER_ID = PL.LEDGER_ID\n" +
                        " INNER JOIN LEDGER_BALANCE LB\n" +
                        "    ON PL.LEDGER_ID = LB.LEDGER_ID\n" +
                        " INNER JOIN MASTER_PROJECT MP\n" +
                        "    ON MP.PROJECT_ID = LB.PROJECT_ID\n" +
                        " INNER JOIN MASTER_DIVISION MDI\n" +
                        "    ON MP.DIVISION_ID = MDI.DIVISION_ID\n" +
                        " WHERE GROUP_ID = 14\n" +
                        "   AND FD.STATUS IN (?STATUS)\n" +
                        "   AND FIND_IN_SET(FD.TRANS_MODE, 'OP,TR')\n" +
                        " GROUP BY FD_REGISTER_ID\n" +
                        " ORDER BY DAYS ASC";

                        break;
                    }
                case SQLCommand.FDRenewal.FetchById:
                    {
                        query = "SELECT FD.BANK_ACCOUNT_ID, FD_NO, FD_REGISTER_ID,\n" +
                        "       INVESTED_ON, ACCOUNT_NO,\n" +
                        "       FD.MATURITY_DATE,\n" +
                        "       FD.INTEREST_RATE,\n" +
                        "       FD.AMOUNT,\n" +
                        "       FD.INTEREST_AMOUNT,\n" +
                        "       STATUS,\n" +
                        "       CASE\n" +
                        "         WHEN STATUS IN(2,4) THEN\n" +
                        "          'Inactive'\n" +
                        "         ELSE\n" +
                        "          'Active'\n" +
                        "       END AS 'FD_STATUS',\n" +
                        "       MB.DATE_OPENED\n" +
                        "  FROM FD_REGISTERS AS FD\n" +
                        " INNER JOIN MASTER_BANK_ACCOUNT AS MB\n" +
                        "    ON FD.BANK_ACCOUNT_ID = MB.BANK_ACCOUNT_ID\n" +
                        " WHERE FD.BANK_ACCOUNT_ID = ?BANK_ACCOUNT_ID\n" +
                        "   AND STATUS IN (4,1,3)\n" +
                        " ORDER BY STATUS";
                        break;
                    }
                case SQLCommand.FDRenewal.UpdateById:
                    {
                        query = "UPDATE  FD_REGISTERS SET STATUS=4 WHERE BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID";
                        break;
                    }
                case SQLCommand.FDRenewal.UpdateFDStatus:
                    {
                        query = "UPDATE FD_REGISTERS SET STATUS=2 WHERE BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID AND STATUS=1";
                        break;
                    }
                case SQLCommand.FDRenewal.FetchFixedDepositStatus:
                    {
                        query = "SELECT STATUS FROM FD_REGISTERS WHERE BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID AND STATUS!=0";
                        break;
                    }
                case SQLCommand.FDRenewal.UpdateStatusByID:
                    {
                        query = "UPDATE FD_REGISTERS SET STATUS=?STATUS WHERE BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID AND STATUS!=0";
                        break;
                    }
                case SQLCommand.FDRenewal.DeleteFDByID:
                    {
                        query = "DELETE FROM FD_REGISTERS WHERE BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID";
                        break;
                    }
                case SQLCommand.FDRenewal.DeleteFDRegisters:
                    {
                        query = "DELETE FROM FD_REGISTERS\n" +
                                " WHERE FD_REGISTER_ID =?FD_REGISTER_ID";
                        break;
                    }
                case SQLCommand.FDRenewal.UpdateLastFDRow:
                    {
                        query = "UPDATE FD_REGISTERS\n" +
                        "   SET STATUS = 1\n" +
                        " WHERE BANK_ACCOUNT_ID = ?BANK_ACCOUNT_ID\n" +
                        "   AND FD_REGISTER_ID = ?FD_REGISTER_ID";

                        break;
                    }
                case SQLCommand.FDRenewal.FetchFDRegisters:
                    {
                        query = "SELECT MA.BANK_ACCOUNT_ID,\n" +
                        "       FD_REGISTER_ID,\n" +
                        "       MA.ACCOUNT_NUMBER,\n" +
                        "       PL.PROJECT_ID,\n" +
                        "       MB.BANK,\n" +
                        "       MB.BRANCH,\n" +
                        "       MA.DATE_OPENED AS 'CREATED_ON',\n" +
                        "       CASE\n" +
                        "         WHEN FD.STATUS IS NULL THEN\n" +
                        "          ''\n" +
                        "         WHEN FD.STATUS IN(1,2,3) THEN\n" +
                        "          DATE_FORMAT(FD.INVESTED_ON, '%d-%m-%Y')\n" +
                        "         ELSE\n" +
                        "          ''\n" +
                        "       END AS 'DATE_OPENED',\n" +
                        "       ML.LEDGER_ID,\n" +
                        "       FD.INTEREST_AMOUNT,\n" +
                        "       FD.AMOUNT,\n" +
                        "       CONCAT(MB.BANK, CONCAT(' - ', MA.ACCOUNT_NUMBER)) AS 'BANKACCOUNT',\n" +
                        "       FD.FD_NO,\n" +
                        "       ML.LEDGER_NAME,\n" +
                        "       IFNULL(FD.AMOUNT, 0) AS AMOUNT,\n" +
                        "       FD.TRANS_MODE,\n" +
                        "       CASE\n" +
                        "         WHEN FD.INVESTED_ON IS NOT NULL AND FD.STATUS IN(2,3) THEN\n" +
                        "         DATE_FORMAT(FD.INVESTED_ON,'%d-%m-%Y')\n" +
                        "         ELSE\n" +
                        "          ''\n" +
                        "       END AS 'INVESTED_ON',\n" +
                        "       FD.MATURITY_DATE,\n" +
                        "       FD.STATUS,\n" +
                        "       CASE\n" +
                        "         WHEN FD.STATUS = 1 OR (FD.STATUS = 1 AND FD.TRANS_MODE = 'OP') THEN\n" +
                        "          'Invested'\n" +
                        "         WHEN FD.STATUS = 3 THEN\n" +
                        "          'ReInvested'\n" +
                        "         WHEN FD.STATUS = 2 THEN\n" +
                        "          'Realized'\n" +
                        "         ELSE\n" +
                        "          ''\n" +
                        "       END AS 'FD_STATUS',\n" +
                        "       CONCAT(MP.PROJECT, CONCAT(' - ', MDI.DIVISION)) AS 'PROJECT',\n" +
                        "       CASE\n" +
                        "       WHEN FD.MATURITY_DATE <=CURDATE() THEN\n" +
                        "            DATEDIFF(FD.MATURITY_DATE,CURDATE())\n" +
                        "         WHEN FD.INVESTED_ON IS NULL THEN\n" +
                        "          DATEDIFF(FD.MATURITY_DATE, MA.DATE_OPENED)\n" +
                        "         ELSE\n" +
                        "          DATEDIFF(FD.MATURITY_DATE, FD.INVESTED_ON)\n" +
                        "       END AS 'DAYS',\n" +
                        "       CASE\n" +
                        "         WHEN FD.IS_INTEREST_RECEIVED_PERIODICALLY = 0 THEN\n" +
                        "          'No'\n" +
                        "         WHEN FD.IS_INTEREST_RECEIVED_PERIODICALLY = 1 THEN\n" +
                        "          'Yes'\n" +
                        "         ELSE\n" +
                        "          ''\n" +
                        "       END AS 'PERIODICALLY'\n" +
                        "  FROM MASTER_BANK_ACCOUNT MA\n" +
                        "  LEFT JOIN FD_REGISTERS FD\n" +
                        "    ON MA.BANK_ACCOUNT_ID = FD.BANK_ACCOUNT_ID AND FD.STATUS NOT IN(4)\n" +
                        " INNER JOIN MASTER_LEDGER ML\n" +
                        "    ON MA.BANK_ACCOUNT_ID = ML.BANK_ACCOUNT_ID\n" +
                        " INNER JOIN PROJECT_LEDGER PL\n" +
                        "    ON ML.LEDGER_ID = PL.LEDGER_ID\n" +
                        " INNER JOIN MASTER_PROJECT MP\n" +
                        "    ON PL.PROJECT_ID = MP.PROJECT_ID\n" +
                        " INNER JOIN MASTER_DIVISION MDI\n" +
                        "    ON MP.DIVISION_ID = MDI.DIVISION_ID, MASTER_BANK MB\n" +
                        " WHERE MA.ACCOUNT_TYPE_ID = 2\n" +
                        "   AND MA.BANK_ID = MB.BANK_ID\n" +
                        " ORDER BY DAYS";

                        break;
                    }
                
            }
            return query;
        }

        public string GetFixedRenewalSQL()
        {
            string query = "";
            SQLCommand.FDRenewal sqlCommandId = (SQLCommand.FDRenewal)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                case SQLCommand.FDRenewal.Add:
                    {
                        //PENALTY_AMOUNT, PENALTY_MODE,
                        query = @"INSERT INTO FD_RENEWAL
                                  (FD_ACCOUNT_ID,
                                   INTEREST_LEDGER_ID,
                                   BANK_LEDGER_ID,
                                   FD_INTEREST_VOUCHER_ID,
                                   FD_VOUCHER_ID,
                                   INTEREST_AMOUNT,
                                   WITHDRAWAL_AMOUNT,
                                   REINVESTED_AMOUNT,
                                   TDS_AMOUNT, CHARGE_MODE, CHARGE_AMOUNT, CHARGE_LEDGER_ID,
                                   EXPECTED_MATURITY_VALUE, EXPECTED_INTEREST_VALUE,
                                   CLOSED_DATE,
                                   RENEWAL_DATE,
                                   MATURITY_DATE,
                                   INTEREST_RATE,
                                   INTEREST_TYPE,
                                   RECEIPT_NO, 
                                   RENEWAL_TYPE,
                                   STATUS,
                                   FD_TYPE, FD_TRANS_MODE)
                                VALUES
                                  (?FD_ACCOUNT_ID,
                                   ?INTEREST_LEDGER_ID,
                                   ?BANK_LEDGER_ID,
                                   ?FD_INTEREST_VOUCHER_ID,
                                   ?FD_VOUCHER_ID,
                                   ?INTEREST_AMOUNT,
                                   ?WITHDRAWAL_AMOUNT,
                                   ?REINVESTED_AMOUNT,
                                   ?TDS_AMOUNT, ?CHARGE_MODE, ?CHARGE_AMOUNT, ?CHARGE_LEDGER_ID,
                                   ?EXPECTED_MATURITY_VALUE, ?EXPECTED_INTEREST_VALUE,
                                   ?CLOSED_DATE,
                                   ?RENEWAL_DATE,
                                   ?MATURITY_DATE,
                                   ?INTEREST_RATE,
                                   ?INTEREST_TYPE,
                                   ?RECEIPT_NO,
                                   ?RENEWAL_TYPE,
                                   ?STATUS,?FD_TYPE, ?FD_TRANS_MODE)";
                        break;
                    }
                case SQLCommand.FDRenewal.Update:
                    {
                        //PENALTY_AMOUNT = ?PENALTY_AMOUNT, 
                        //PENALTY_MODE = ?PENALTY_MODE,
                        query = @"UPDATE  FD_RENEWAL  SET
                                   INTEREST_LEDGER_ID=?INTEREST_LEDGER_ID,
                                   BANK_LEDGER_ID=?BANK_LEDGER_ID,
                                   FD_INTEREST_VOUCHER_ID=?FD_INTEREST_VOUCHER_ID,
                                   FD_VOUCHER_ID=?FD_VOUCHER_ID,
                                   INTEREST_AMOUNT=?INTEREST_AMOUNT,
                                   WITHDRAWAL_AMOUNT=?WITHDRAWAL_AMOUNT,
                                   REINVESTED_AMOUNT=?REINVESTED_AMOUNT,
                                   TDS_AMOUNT=?TDS_AMOUNT,
                                   CHARGE_MODE=?CHARGE_MODE, CHARGE_AMOUNT=?CHARGE_AMOUNT, CHARGE_LEDGER_ID = ?CHARGE_LEDGER_ID,
                                   EXPECTED_MATURITY_VALUE=?EXPECTED_MATURITY_VALUE,
                                   EXPECTED_INTEREST_VALUE = ?EXPECTED_INTEREST_VALUE,
                                   RENEWAL_DATE=?RENEWAL_DATE,
                                   CLOSED_DATE=?CLOSED_DATE,
                                   MATURITY_DATE=?MATURITY_DATE,
                                   INTEREST_RATE=?INTEREST_RATE,
                                   INTEREST_TYPE=?INTEREST_TYPE,
                                   RECEIPT_NO=?RECEIPT_NO,
                                   RENEWAL_TYPE=?RENEWAL_TYPE,
                                   STATUS=?STATUS,FD_TYPE=?FD_TYPE, FD_TRANS_MODE=?FD_TRANS_MODE WHERE FD_RENEWAL_ID=?FD_RENEWAL_ID";
                        break;
                    }
                case SQLCommand.FDRenewal.FetchVoucherID:
                    {
                        query = "SELECT FD_VOUCHER_ID FROM FD_RENEWAL WHERE FD_RENEWAL_ID=?FD_RENEWAL_ID AND STATUS=1 AND IS_DELETED=1";
                        break;
                    }
                case SQLCommand.FDRenewal.FetchFDAccountIdByRenewalId:
                    {
                        query = "SELECT FD_ACCOUNT_ID FROM FD_RENEWAL WHERE FD_RENEWAL_ID=?FD_RENEWAL_ID AND STATUS=1 AND IS_DELETED=1";
                        break;
                    }
                case SQLCommand.FDRenewal.Delete:
                    {
                        query = "UPDATE FD_RENEWAL SET IS_DELETED=0 ,STATUS=0 WHERE FD_RENEWAL_ID=?FD_RENEWAL_ID";
                        break;
                    }
                case SQLCommand.FDRenewal.DeleteFDRenewal:
                    {
                        query = "UPDATE  FD_RENEWAL SET STATUS=0, IS_DELETED=0  WHERE FD_INTEREST_VOUCHER_ID=?FD_VOUCHER_ID OR FD_VOUCHER_ID=?FD_VOUCHER_ID";
                        break;
                    }
                case SQLCommand.FDRenewal.DeleteFDPhysicalReneval:
                    {
                        query = "DELETE FROM FD_RENEWAL WHERE FD_INTEREST_VOUCHER_ID=?FD_VOUCHER_ID OR FD_VOUCHER_ID=?FD_VOUCHER_ID";
                        break;
                    }
                case SQLCommand.FDRenewal.DeleteFDAccount:
                    {
                        query = "UPDATE FD_ACCOUNT SET STATUS=0 WHERE FD_VOUCHER_ID=?FD_VOUCHER_ID";
                        break;
                    }
                case SQLCommand.FDRenewal.DeleteFDPhysicalAccount:
                    {
                        query = "DELETE FROM FD_ACCOUNT WHERE FD_VOUCHER_ID=?FD_VOUCHER_ID";
                        break;
                    }
                case SQLCommand.FDRenewal.DeleteFDPhysicalOpeningAccount:
                    {
                        query = "DELETE FROM FD_ACCOUNT WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID AND TRANS_TYPE ='OP' AND STATUS=0";
                        break;
                    }
                case SQLCommand.FDRenewal.DeleteFDPhysicalRenewalAccount:
                    {
                        query = "DELETE FROM FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID AND STATUS=0 AND IS_DELETED=0";
                        break;
                    }
                case SQLCommand.FDRenewal.CheckFDAccountExists:
                    {
                        query = "SELECT COUNT(*) FROM FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID AND STATUS=1 AND IS_DELETED=1";
                        break;
                    }
                case SQLCommand.FDRenewal.CheckPhysicalAccountExists:
                    {
                        query = "SELECT COUNT(*) FROM FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID AND STATUS=0 AND IS_DELETED=0";
                        break;
                    }
                case SQLCommand.FDRenewal.FetchFDAccountId:
                    {
                        query = "SELECT FD_ACCOUNT_ID FROM FD_ACCOUNT WHERE FD_VOUCHER_ID=?FD_VOUCHER_ID";
                        break;
                    }
                case SQLCommand.FDRenewal.FetchRenewalType:
                    {
                        query = "SELECT FD_ACCOUNT_ID,FD_RENEWAL_ID,FD_INTEREST_VOUCHER_ID,FD_VOUCHER_ID, RENEWAL_TYPE FROM FD_RENEWAL WHERE FD_INTEREST_VOUCHER_ID=?FD_VOUCHER_ID OR FD_VOUCHER_ID=?FD_VOUCHER_ID AND STATUS=1 AND IS_DELETED=1 ";
                        break;
                    }
                case SQLCommand.FDRenewal.FetchDeletedRenewalType:
                    {
                        query = "SELECT FD_ACCOUNT_ID,FD_RENEWAL_ID,FD_INTEREST_VOUCHER_ID,FD_VOUCHER_ID, RENEWAL_TYPE FROM FD_RENEWAL WHERE FD_INTEREST_VOUCHER_ID=?FD_VOUCHER_ID OR FD_VOUCHER_ID=?FD_VOUCHER_ID AND STATUS=0 AND IS_DELETED=0";
                        break;
                    }
                case SQLCommand.FDRenewal.UpdateFDAccountStatus:
                    {
                        query = "UPDATE FD_ACCOUNT SET STATUS=1 WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID";
                        break;
                    }
                case SQLCommand.FDRenewal.CheckFDClosed:
                    {
                        query = "SELECT COUNT(*)  AS 'COUNT' FROM FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID AND RENEWAL_TYPE='WDI' AND IS_DELETED=1 AND STATUS=1";
                        break;
                    }
                case SQLCommand.FDRenewal.CheckFDPhysicalClosd:
                    {
                        query = "SELECT COUNT(*)  AS 'COUNT' FROM FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID AND IS_DELETED=0 AND STATUS=0";
                        break;
                    }
                case SQLCommand.FDRenewal.CheckByVoucherId:
                    {
                        query = "SELECT RENEWAL_TYPE FROM FD_RENEWAL WHERE FD_INTEREST_VOUCHER_ID=?FD_VOUCHER_ID OR FD_VOUCHER_ID=?FD_VOUCHER_ID AND IS_DELETED=1 AND STATUS=1";
                        break;
                    }

                case SQLCommand.FDRenewal.FetchRenewalByVoucherId:
                    {
                        query = "SELECT FD.FD_ACCOUNT_ID, PROJECT_ID, FD.STATUS, RENEWAL_TYPE\n" +
                                "  FROM FD_ACCOUNT AS FD\n" +
                                "  LEFT JOIN FD_RENEWAL AS FR\n" +
                                "    ON FD.FD_ACCOUNT_ID = FR.FD_ACCOUNT_ID\n" +
                                " WHERE FR.FD_INTEREST_VOUCHER_ID = ?FD_VOUCHER_ID\n" +
                                "    OR FR.FD_VOUCHER_ID = ?FD_VOUCHER_ID AND FR.STATUS=1 AND FR.IS_DELETED=1";
                        break;
                    }
                case SQLCommand.FDRenewal.FetchFDAccountByVoucherId:
                    {
                        query = "SELECT FD_ACCOUNT_ID,PROJECT_ID,STATUS,TRANS_TYPE FROM FD_ACCOUNT WHERE {FD_ACCOUNT_ID=?FD_ACCOUNT_ID} {FD_VOUCHER_ID=?FD_VOUCHER_ID}";
                        break;
                    }

                case SQLCommand.FDRenewal.CheckDuplicateRenewal:
                    {
                        //query = "SELECT COUNT(*) FROM FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID AND RENEWAL_DATE=?RENEWAL_DATE AND STATUS=1 AND IS_DELETED=1";

                        //On 31/08/2022, to pass post interest on maturity date and when modify not allow existing renewal date
                        //query = "SELECT COUNT(*) FROM FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID AND RENEWAL_DATE=?RENEWAL_DATE AND STATUS=1 AND ";
                        query = "SELECT COUNT(*) FROM FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID AND FD_TYPE <> 'POI'\n"+
                                        "{AND FD_RENEWAL_ID <> ?FD_RENEWAL_ID} AND RENEWAL_DATE=?RENEWAL_DATE AND STATUS=1";

                        break;
                    }
                case SQLCommand.FDRenewal.CheckFDRenewalWithdraw:
                    {
                        query = "SELECT COUNT(*) FROM FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID AND RENEWAL_TYPE='WDI' AND STATUS=1 AND IS_DELETED=1 ";
                        break;
                    }
                case SQLCommand.FDRenewal.GetVoucherId:
                    {
                        query = "SELECT FD_VOUCHER_ID FROM FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID AND RENEWAL_TYPE='WDI' AND STATUS=1 AND IS_DELETED=1 ";
                        break;
                    }
                case SQLCommand.FDRenewal.HasFDRenewal:
                    {
                        query = "SELECT COUNT(*) FROM  FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID  AND STATUS=1 AND IS_DELETED=1  AND FD_TYPE IN('RN') AND RENEWAL_TYPE NOT IN ('WDI')";
                        break;
                    }
                case SQLCommand.FDRenewal.HasFDReInvestmentByFDAccountId:
                    {
                        query = "SELECT COUNT(*) FROM  FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID  AND STATUS=1 AND IS_DELETED=1  AND FD_TYPE IN ('RIN') AND RENEWAL_TYPE IN ('RIN')";
                        break;
                    }
                case SQLCommand.FDRenewal.HasFDPostInterests:
                    {
                        query = "SELECT COUNT(*) FROM  FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID  AND STATUS=1 AND IS_DELETED=1  AND FD_TYPE IN ('POI') AND RENEWAL_TYPE NOT IN ('WDI')";
                        break;
                    }
                case SQLCommand.FDRenewal.HasFDPartialwithdrawal:
                    {
                        query = "SELECT COUNT(*) FROM  FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID AND STATUS = 1 AND IS_DELETED=1 AND FD_TYPE IN ('WD') AND RENEWAL_TYPE IN ('PWD')";
                        break;
                    }
                case SQLCommand.FDRenewal.HasFDWithdrawal:
                    {
                        query = "SELECT COUNT(*) FROM  FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID  AND STATUS=1 AND IS_DELETED=1  AND FD_TYPE IN('WD') AND RENEWAL_TYPE IN ('WDI')";
                        break;
                    }
                case SQLCommand.FDRenewal.GetLastRenewalIdByFDAccountId:
                    {

                        query = "SELECT MAX(FD_RENEWAL_ID) AS MAX_FD_RENEWAL_ID\n" +
                                "  FROM FD_RENEWAL\n" +
                                " WHERE FD_ACCOUNT_ID = ?FD_ACCOUNT_ID\n" +
                                "   AND STATUS = 1\n" +
                                "   AND IS_DELETED = 1 AND FD_TYPE IN ('RN')\n" +
                                "   AND RENEWAL_TYPE NOT IN ('WDI');";
                        break;
                    }
                case SQLCommand.FDRenewal.GetLastRenwalDetailsByRenewalId:
                    {

                        query = "SELECT RENEWAL_DATE,\n" +
                                "       MATURITY_DATE,\n" +
                                "       INTEREST_RATE,\n" +
                                "       INTEREST_TYPE,\n" +
                                "       RENEWAL_TYPE,\n" +
                                "       BANK_LEDGER_ID\n" +
                                "  FROM FD_RENEWAL\n" +
                                " WHERE FD_RENEWAL_ID = ?FD_RENEWAL_ID\n" +
                                "   AND STATUS = 1\n" +
                                "   AND IS_DELETED = 1 AND FD_TYPE IN ('RN')\n" +
                                "   AND RENEWAL_TYPE NOT IN ('WDI');";

                        break;
                    }
                case SQLCommand.FDRenewal.GetLastPostInterestIdByFDAccountId:
                    {
                        //query = "SELECT MAX(FD_RENEWAL_ID) AS MAX_FD_RENEWAL_ID\n" +
                        //        "  FROM FD_RENEWAL\n" +
                        //        " WHERE FD_ACCOUNT_ID = ?FD_ACCOUNT_ID\n" +
                        //        "   AND STATUS = 1\n" +
                        //        "   AND IS_DELETED = 1 AND FD_TYPE IN ('POI')\n" +
                        //        "   AND RENEWAL_TYPE NOT IN ('WDI');";

                        query = "SELECT FD_RENEWAL_ID AS MAX_FD_RENEWAL_ID\n" +
                                "FROM FD_RENEWAL\n" +
                                " WHERE FD_ACCOUNT_ID = ?FD_ACCOUNT_ID\n AND STATUS = 1\n" +
                                " AND IS_DELETED = 1 AND FD_TYPE IN ('POI') AND RENEWAL_TYPE NOT IN ('WDI') ORDER BY RENEWAL_DATE DESC LIMIT 1";
                        break;
                    }
                case SQLCommand.FDRenewal.GetLastPostInterestDetailsByRenewalId:
                    {

                        query = "SELECT RENEWAL_DATE,\n" +
                                "       MATURITY_DATE,\n" +
                                "       INTEREST_RATE,\n" +
                                "       INTEREST_TYPE,\n" +
                                "       RENEWAL_TYPE\n" +
                                "  FROM FD_RENEWAL\n" +
                                " WHERE FD_RENEWAL_ID = ?FD_RENEWAL_ID\n" +
                                "   AND STATUS = 1\n" +
                                "   AND IS_DELETED = 1 AND FD_TYPE IN ('POI')\n" +
                                "   AND RENEWAL_TYPE NOT IN ('WDI');";

                        break;
                    }
                case SQLCommand.FDRenewal.FetchFDAccountDetailsByFDAccountID:
                    {

                        query = "SELECT FD.FD_ACCOUNT_ID, IFNULL(MP.PROJECT, '') AS PROJECT, IFNULL(MIT.INVESTMENT_TYPE, '') AS INVESTMENT_TYPE,\n" +
                        "       FD.FD_ACCOUNT_NUMBER,\n" +
                        "       FD.PROJECT_ID,\n" +
                        "       FD.LEDGER_ID,\n" +
                        "       FD.BANK_LEDGER_ID,\n" +
                        "       FD.BANK_ID,\n" +
                        "       FD.FD_VOUCHER_ID,\n" +
                        "       FD.AMOUNT,\n" +
                        "       FD.TRANS_MODE,\n" +
                        "       FD.TRANS_TYPE,\n" +
                        "       FD.RECEIPT_NO,\n" +
                        "       FD.ACCOUNT_HOLDER,\n" +
                        "       FD.INVESTMENT_DATE,\n" +  //   CLOSED_DATE (Commanded by chinna)"
                        "       FD.MATURED_ON,\n" +
                        "       FD.INTEREST_RATE,\n" +
                        "       FD.INTEREST_AMOUNT,\n" +
                        "       FD.INTEREST_TYPE,\n" +
                        "       FD.STATUS,\n" +
                        "       FD.FD_STATUS,\n" +
                        "       FD.FD_SUB_TYPES,\n" +
                        "       FD.NOTES,\n" +
                        "       FD.BRANCH_ID,\n" +
                        "       FD.LOCATION_ID, MC.CURRENCY_NAME\n" +
                        "  FROM FD_ACCOUNT FD\n" +
                        "  LEFT JOIN MASTER_PROJECT MP ON MP.PROJECT_ID = Fd.PROJECT_ID\n" +
                        "  LEFT JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = FD.LEDGER_ID\n" +
                        "  LEFT JOIN MASTER_INVESTMENT_TYPE MIT ON MIT.INVESTMENT_TYPE_ID = ML.FD_INVESTMENT_TYPE_ID\n" +
                        "  LEFT JOIN MASTER_COUNTRY MC ON MC.COUNTRY_ID = FD.CURRENCY_COUNTRY_ID\n" +
                        "  WHERE FD.FD_ACCOUNT_ID = ?FD_ACCOUNT_ID;";

                        break;
                    }
                case SQLCommand.FDRenewal.GetMaxFDRenewal:
                    {
                        query = "SELECT MAX(RENEWAL_DATE) AS 'RENEWAL_DATE'\n" +
                        "  FROM FD_RENEWAL\n" +
                        " WHERE FD_ACCOUNT_ID = ?FD_ACCOUNT_ID\n" +
                        "   AND RENEWAL_DATE >= ?RENEWAL_DATE\n" +
                        "   AND IS_DELETED = 1\n" +
                        "   AND STATUS = 1\n" +
                        "   AND FD_TYPE = 'RN';";

                        break;

                    }
                case SQLCommand.FDRenewal.DisableFDInterestDeprecaitionLedger:
                    {
                        query = @"UPDATE MASTER_LEDGER SET IS_BANK_INTEREST_LEDGER = 0
                                   WHERE LEDGER_NAME = 'depreciation' AND LEDGER_ID NOT IN (SELECT INTEREST_LEDGER_ID FROM FD_RENEWAL WHERE STATUS = 1);";
                        break;
                    }

            }
            return query;
        }
        #endregion Bank SQL
    }
}
