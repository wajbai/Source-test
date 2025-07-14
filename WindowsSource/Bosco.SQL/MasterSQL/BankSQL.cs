using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class BankSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.Bank).FullName)
            {
                query = GetBankSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the bank details.
        /// </summary>
        /// <returns></returns>
        private string GetBankSQL()
        {
            string query = "";
            SQLCommand.Bank sqlCommandId = (SQLCommand.Bank)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.Bank.Add:
                    {
                        query = "INSERT INTO MASTER_BANK ( " +
                               "BANK_CODE, " +
                               "BANK, " +
                               "BRANCH, " +
                               "ADDRESS," +
                               "BSRCODE," +
                               "IFSCCODE," +
                               "MICRCODE," +
                               "CONTACTNUMBER," +
                               "SWIFTCODE," +
                               "NOTES ) VALUES( " +
                               "?BANK_CODE, " +
                               "?BANK, " +
                               "?BRANCH, " +
                               "?ADDRESS," +
                               "?BSRCODE," +
                               "?IFSCCODE," +
                               "?MICRCODE," +
                               "?CONTACTNUMBER," +
                               "?SWIFTCODE," +
                               "?NOTES)";
                        break;
                    }
                case SQLCommand.Bank.Update:
                    {
                        query = "UPDATE MASTER_BANK SET " +
                                    "BANK_CODE = ?BANK_CODE, " +
                                    "BANK =?BANK, " +
                                    "BRANCH =?BRANCH, " +
                                    "ADDRESS=?ADDRESS, " +
                                    "BSRCODE=?BSRCODE," +
                                    "IFSCCODE=?IFSCCODE," +
                                    "MICRCODE=?MICRCODE," +
                                    "CONTACTNUMBER=?CONTACTNUMBER ," +
                                    "SWIFTCODE=?SWIFTCODE," +
                                    "NOTES=?NOTES " +
                                    " WHERE BANK_ID=?BANK_ID ";
                        break;
                    }
                case SQLCommand.Bank.Delete:
                    {
                        query = "DELETE FROM  MASTER_BANK WHERE BANK_ID=?BANK_ID";
                        break;
                    }
                case SQLCommand.Bank.Fetch:
                    {
                        query = "SELECT " +
                                "BANK_ID, " +
                                "BANK_CODE, " +
                                "BANK, " +
                                "BRANCH, " +
                                "ADDRESS, " +
                                "BSRCODE, " +
                                "IFSCCODE," +
                                "MICRCODE," +
                                "CONTACTNUMBER, " +
                                "SWIFTCODE," +
                                "NOTES " +
                            " FROM " +
                                "MASTER_BANK " +
                                " WHERE BANK_ID=?BANK_ID  ";
                        break;
                    }
                case SQLCommand.Bank.FetchAll:
                    {
                        query = "SELECT " +
                                "BANK_ID, " +
                                "BANK_CODE, " +
                                "BANK, " +
                                "BRANCH, " +
                                "ADDRESS, " +
                                "BSRCODE, " +
                                "IFSCCODE," +
                                "MICRCODE," +
                                "CONTACTNUMBER ," +
                                "SWIFTCODE " +
                            " FROM" +
                                " MASTER_BANK ORDER BY BANK ASC";
                        break;
                    }
                case SQLCommand.Bank.FetchforLookup:
                    {
                        query = "SELECT " +
                                "BANK_ID, " +
                                "CONCAT(CONCAT(BRANCH,' - '), BANK) AS BANK " +
                            "FROM" +
                                " MASTER_BANK ORDER BY BANK ASC"; //Previous given by BANK_ID DESC
                        break;
                    }
                case SQLCommand.Bank.SetBankAccountSource:
                    {
                        query = "SELECT MB.BANK_ID, " +
                               " CONCAT(MB.BANK,CONCAT(' - ',MBA.ACCOUNT_NUMBER),CONCAT(' - ',MB.BRANCH)) AS 'BANK'," +
                               " ML.GROUP_ID " +
                               " FROM MASTER_BANK_ACCOUNT AS MBA " +
                               " INNER JOIN MASTER_BANK AS MB ON MBA.BANK_ID=MB.BANK_ID " +
                               " INNER JOIN MASTER_LEDGER AS ML ON MBA.BANK_ACCOUNT_ID=ML.BANK_ACCOUNT_ID " +
                               " INNER JOIN PROJECT_LEDGER AS PL ON PL.LEDGER_ID=ML.LEDGER_ID " +
                               " WHERE PL.PROJECT_ID IN(?PROJECT_ID) " +
                               " GROUP BY MBA.ACCOUNT_NUMBER " +
                               " ORDER BY BANK_ID ASC";
                        break;
                    }
                case SQLCommand.Bank.FetchSettingBankAccount:
                    {
                        query = "SELECT MB.BANK_ID,\n" +
                                "MBA.LEDGER_ID,\n" +
                                "CONCAT(MB.BANK,\n" +
                                "CONCAT(' - ', MBA.ACCOUNT_NUMBER),\n" +
                                "CONCAT(' - ', MB.BRANCH)) AS 'BANK'\n" +
                                "  FROM MASTER_BANK_ACCOUNT AS MBA\n" +
                                " INNER JOIN MASTER_BANK AS MB\n" +
                                "    ON MBA.BANK_ID = MB.BANK_ID\n" +
                                " INNER JOIN MASTER_LEDGER AS ML\n" +
                                "    ON MBA.LEDGER_ID = ML.LEDGER_ID\n" +
                                " INNER JOIN PROJECT_LEDGER AS PL\n" +
                                "    ON PL.LEDGER_ID = ML.LEDGER_ID\n" +
                                " WHERE ML.GROUP_ID=12 " +
                                " GROUP BY MBA.LEDGER_ID\n" +
                                " ORDER BY BANK_ID ASC;";

                        break;
                    }
                case SQLCommand.Bank.FetchAllCashLedgerByProject:
                    {
                        query = "SELECT ML.LEDGER_ID, ML.LEDGER_NAME,\n" +
                                    " ML.GROUP_ID,\n" +
                                    " PL.PROJECT_ID\n" +
                                    " FROM  PROJECT_LEDGER PL\n" +
                                    " INNER JOIN MASTER_LEDGER ML ON PL.LEDGER_ID = ML.LEDGER_ID\n" +
                                    " WHERE {PL.PROJECT_ID IN (?PROJECT_ID) AND } ML.GROUP_ID = 13\n" +
                                    " GROUP BY ML.LEDGER_ID;";
                        break;
                    }
                case SQLCommand.Bank.SelectAllBank:
                    {

                        query = "SELECT MB.BANK_ID,\n" +
                                "       CONCAT(ML.LEDGER_NAME,\n" +
                                "              CONCAT(' - ',MB.BANK ),\n" +
                                "              CONCAT(' - ', MB.BRANCH)) AS 'BANK',\n" +
                                " ML.GROUP_ID,\n" +
                                " PL.PROJECT_ID,\n" +
                                " ML.BANK_ACCOUNT_ID,ML.LEDGER_ID,ML.LEDGER_NAME AS BANKNAME, IFNULL(ML.CUR_COUNTRY_ID, 0) AS CUR_COUNTRY_ID\n" +
                                " FROM  PROJECT_LEDGER PL\n" +
                                " INNER JOIN MASTER_LEDGER ML ON PL.LEDGER_ID = ML.LEDGER_ID\n" +
                                " INNER JOIN MASTER_BANK_ACCOUNT MBA ON MBA.LEDGER_ID = ML.LEDGER_ID\n" +
                                " INNER JOIN MASTER_BANK MB ON MBA.BANK_ID=MB.BANK_ID\n" +
                                " LEFT JOIN (SELECT PLA.PROJECT_ID, PLA.LEDGER_ID, PLA.APPLICABLE_FROM, PLA.APPLICABLE_TO FROM PROJECT_LEDGER_APPLICABLE PLA " +
                                "  WHERE PLA.PROJECT_ID IN (?PROJECT_ID) ) AS PLA ON PLA.PROJECT_ID = PL.PROJECT_ID AND PLA.LEDGER_ID = ML.LEDGER_ID " +
                                " WHERE 1 = 1 {AND PL.PROJECT_ID IN (?PROJECT_ID)} {AND (MBA.DATE_CLOSED IS NULL OR MBA.DATE_CLOSED >= ?DATE_CLOSED) } " +
                                "{AND (((PLA.APPLICABLE_FROM BETWEEN ?APPLICABLE_FROM AND ?APPLICABLE_TO) OR PLA.APPLICABLE_FROM  IS NULL) " +
                                " OR ( IF(PLA.APPLICABLE_TO IS NULL, PLA.APPLICABLE_TO IS NOT NULL, (PLA.APPLICABLE_TO BETWEEN ?APPLICABLE_FROM AND ?APPLICABLE_TO)) ))} " +   //On 28/09/2023, This property is used to skip bank ledger project based
                                " GROUP BY MBA.LEDGER_ID \n" +
                                " ORDER BY BANK ASC";
                        break;
                    }
                case SQLCommand.Bank.SelectAllFD:
                    {
                        query = "SELECT MB.BANK_ID,\n" +
                                  "       CONCAT(FD.FD_ACCOUNT_NUMBER,\n" +
                                  "              CONCAT(' - ', MB.BANK),\n" +
                                  "              CONCAT(' - ', MB.BRANCH)) AS 'BANK',\n" +
                                  "       ML.GROUP_ID,\n" +
                                  "       PL.PROJECT_ID,\n" +
                                  "       ML.BANK_ACCOUNT_ID,\n" +
                                  "       ML.LEDGER_ID,FD.FD_ACCOUNT_ID\n" +
                                  "  FROM PROJECT_LEDGER PL\n" +
                                  " INNER JOIN MASTER_LEDGER ML\n" +
                                  "    ON PL.LEDGER_ID = ML.LEDGER_ID\n" +
                                  " INNER JOIN FD_ACCOUNT FD\n" +
                                  "    ON FD.LEDGER_ID = ML.LEDGER_ID\n" +
                                  " INNER JOIN MASTER_BANK MB\n" +
                                  "    ON MB.BANK_ID = FD.BANK_ID\n" +
                                  " GROUP BY FD.FD_ACCOUNT_ID\n" +
                                  " ORDER BY BANK ASC;";

                        break;
                    }
                case SQLCommand.Bank.FetchFDByProject:
                    {
                        query = "SELECT MB.BANK_ID,\n" +
                                  "  CONCAT(FD.FD_ACCOUNT_NUMBER, IF(IFNULL(FR_RECEIPT_NO.RECEIPT_NO,'')='' ,'',CONCAT(' (R: ',FR_RECEIPT_NO.RECEIPT_NO,')')), CONCAT(' - ', MB.BANK), CONCAT(' - ', MB.BRANCH)) AS 'BANK',\n" +
                                  "  ML.GROUP_ID,\n" +
                                  "  PL.PROJECT_ID,\n" +
                                  "  ML.BANK_ACCOUNT_ID,\n" +
                                  "  ML.LEDGER_ID,FD.FD_ACCOUNT_ID\n" +
                                  " FROM PROJECT_LEDGER PL\n" +
                                  " INNER JOIN MASTER_LEDGER ML ON PL.LEDGER_ID = ML.LEDGER_ID\n" +
                                  " INNER JOIN FD_ACCOUNT FD ON FD.LEDGER_ID = ML.LEDGER_ID\n" +
                                  " INNER JOIN MASTER_BANK MB ON MB.BANK_ID = FD.BANK_ID\n" +
                                  " LEFT JOIN (SELECT FR.FD_ACCOUNT_ID, IFNULL(FR.RECEIPT_NO,'') AS RECEIPT_NO\n" +
                                  "      FROM FD_RENEWAL FR\n" +
                                  "      INNER JOIN (SELECT FD_ACCOUNT_ID, MAX(RENEWAL_DATE) AS RENEWAL_DATE FROM FD_RENEWAL\n" +
                                  "      WHERE STATUS =1 AND FD_TYPE='RN' AND (RENEWAL_DATE <=?DATE_FROM OR RENEWAL_DATE <= ?DATE_TO) GROUP BY FD_ACCOUNT_ID) FR1 ON FR1.FD_ACCOUNT_ID = FR.FD_ACCOUNT_ID\n" +
                                  "      AND FR1.RENEWAL_DATE = FR.RENEWAL_DATE\n" +
                                  "      WHERE FR.STATUS = 1 AND FD_TYPE = 'RN') AS FR_RECEIPT_NO ON FR_RECEIPT_NO.FD_ACCOUNT_ID = FD.FD_ACCOUNT_ID\n" +
                                  " WHERE FD.STATUS =1 AND FD.PROJECT_ID  IN (?PROJECT_ID) \n" +
                                  " GROUP BY FD.FD_ACCOUNT_ID\n" +
                                  " ORDER BY BANK ASC;";
                        break;
                    }
                case SQLCommand.Bank.FetchBankByProject:
                    {
                        query = "SELECT MB.BANK_ID,\n" +
                                "       CONCAT(ML.LEDGER_NAME,\n" +
                                "              CONCAT(' - ',MB.BANK ),\n" +
                                "              CONCAT(' - ', MB.BRANCH)) AS 'BANK',\n" +
                                "       ML.GROUP_ID,\n" +
                                "       PL.PROJECT_ID,\n" +
                                "       ML.BANK_ACCOUNT_ID, ML.LEDGER_ID, IFNULL(ML.CUR_COUNTRY_ID, 0) AS CUR_COUNTRY_ID\n" +
                                " FROM  PROJECT_LEDGER PL\n" +
                                " INNER JOIN MASTER_LEDGER ML ON PL.LEDGER_ID = ML.LEDGER_ID\n" +
                                " INNER JOIN MASTER_BANK_ACCOUNT MBA ON MBA.LEDGER_ID = ML.LEDGER_ID\n" +
                                " INNER JOIN MASTER_BANK MB ON   MBA.BANK_ID=MB.BANK_ID\n" +
                                " LEFT JOIN (SELECT PLA.PROJECT_ID, PLA.LEDGER_ID, PLA.APPLICABLE_FROM, PLA.APPLICABLE_TO FROM PROJECT_LEDGER_APPLICABLE PLA " +
                                "  WHERE PLA.PROJECT_ID IN (?PROJECT_ID)) AS PLA ON PLA.PROJECT_ID = PL.PROJECT_ID AND PLA.LEDGER_ID = ML.LEDGER_ID " +
                                " WHERE PL.PROJECT_ID IN (?PROJECT_ID)\n" +
                                " {AND (MBA.DATE_CLOSED IS NULL OR MBA.DATE_CLOSED >= ?DATE_CLOSED)}\n" + //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                                "{AND (((PLA.APPLICABLE_FROM BETWEEN ?APPLICABLE_FROM AND ?APPLICABLE_TO) OR PLA.APPLICABLE_FROM  IS NULL) " +
                                " OR ( IF(PLA.APPLICABLE_TO IS NULL, PLA.APPLICABLE_TO IS NOT NULL, (PLA.APPLICABLE_TO BETWEEN ?APPLICABLE_FROM AND ?APPLICABLE_TO)) ))} " +   //On 28/09/2023, This property is used to skip bank ledger project based
                                " GROUP BY MBA.LEDGER_ID \n" +
                                " ORDER BY BANK ASC";

                        break;
                    }
                case SQLCommand.Bank.FetchAllBankAccounts:
                    {
                        query = "SELECT PL.PROJECT_ID, ML.BANK_ACCOUNT_ID,ML.LEDGER_ID, MB.BANK_ID, ML.GROUP_ID, \n" +
                                "ML.LEDGER_NAME AS ACCOUNT_NUMBER, MB.BANK, MB.BRANCH, MB.ADDRESS,\n" +
                                "CONCAT(ML.LEDGER_NAME, CONCAT(' - ',MB.BANK ), CONCAT(' - ', MB.BRANCH)) AS LEDGER_NAME\n" +
                                " FROM  PROJECT_LEDGER PL\n" +
                                " INNER JOIN MASTER_LEDGER ML ON PL.LEDGER_ID = ML.LEDGER_ID\n" +
                                " INNER JOIN MASTER_BANK_ACCOUNT MBA ON MBA.LEDGER_ID = ML.LEDGER_ID\n" +
                                " INNER JOIN MASTER_BANK MB ON MBA.BANK_ID=MB.BANK_ID\n" +
                                " LEFT JOIN (SELECT PLA.PROJECT_ID, PLA.LEDGER_ID, PLA.APPLICABLE_FROM, PLA.APPLICABLE_TO FROM PROJECT_LEDGER_APPLICABLE PLA " +
                                "  WHERE PLA.PROJECT_ID IN (?PROJECT_ID)) AS PLA ON PLA.PROJECT_ID = PL.PROJECT_ID AND PLA.LEDGER_ID = ML.LEDGER_ID " +
                                " WHERE 1=1 {AND PL.PROJECT_ID IN (?PROJECT_ID)}\n" +
                                " {AND (MBA.DATE_CLOSED IS NULL OR MBA.DATE_CLOSED >= ?DATE_CLOSED)}\n" + //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                                "{AND (((PLA.APPLICABLE_FROM BETWEEN ?APPLICABLE_FROM AND ?APPLICABLE_TO) OR PLA.APPLICABLE_FROM  IS NULL) " +
                                " OR ( IF(PLA.APPLICABLE_TO IS NULL, PLA.APPLICABLE_TO IS NOT NULL, (PLA.APPLICABLE_TO BETWEEN ?APPLICABLE_FROM AND ?APPLICABLE_TO)) ))} " +   //On 28/09/2023, This property is used to skip bank ledger project based
                                " ORDER BY ML.LEDGER_NAME ASC";
                        break;
                    }
                case SQLCommand.Bank.FetchBankCodes:
                    {
                        query = "SELECT BANK_CODE AS 'USED_CODE',CONCAT(CONCAT(BANK,' - '),BRANCH) AS 'NAME' FROM MASTER_BANK ORDER BY BANK_ID DESC";
                        break;
                    }

                case SQLCommand.Bank.FetchBankCount:
                    {
                        query = "SELECT COUNT(*) FROM MASTER_BANK";
                        break;
                    }
                case SQLCommand.Bank.FetchBankByBankCode:
                    {
                        query = "SELECT BANK_CODE AS 'EXIST_CODE' FROM MASTER_BANK WHERE BANK_CODE=?BANK_CODE";
                        break;
                    }
                case SQLCommand.Bank.FetchBankDetailsByProjectIds:
                    {
                        query = "SELECT MB.BANK_ID,\n" +
                                "       CONCAT(ML.LEDGER_NAME,\n" +
                                "              CONCAT(' - ',MB.BANK ),\n" +
                                "              CONCAT(' - ', MB.BRANCH)) AS 'BANK',\n" +
                                "       ML.GROUP_ID,\n" +
                                "       PL.PROJECT_ID,\n" +
                                "       ML.BANK_ACCOUNT_ID,ML.LEDGER_ID,ML.LEDGER_NAME AS BANKNAME\n" +
                                "  FROM  PROJECT_LEDGER PL\n" +
                                " INNER JOIN MASTER_LEDGER ML\n" +
                                " ON PL.LEDGER_ID = ML.LEDGER_ID\n" +
                                " INNER JOIN MASTER_BANK_ACCOUNT MBA\n" +
                                " ON MBA.LEDGER_ID = ML.LEDGER_ID\n" +
                                " INNER JOIN MASTER_BANK MB\n" +
                                " ON   MBA.BANK_ID=MB.BANK_ID\n" +
                                " WHERE PL.PROJECT_ID IN (?PROJECT_ID)\n" +
                                " {AND (MBA.DATE_CLOSED IS NULL OR MBA.DATE_CLOSED >= ?DATE_CLOSED)}\n" + //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                                " GROUP BY MBA.LEDGER_ID \n" +
                                " ORDER BY BANK ASC";
                        break;
                    }
                case SQLCommand.Bank.FetchBankandBaranchByProjectId:
                    {
                        query = "SELECT MB.BANK_ID,\n" +
                            // "       CONCAT(ML.LEDGER_NAME,\n" +
                                "CONCAT(CONCAT(MB.BRANCH,' - '),MB.BANK) AS 'BANK',\n" +
                            //"              CONCAT(' - ',MB.BANK ),\n" +
                            //"              CONCAT(' - ', MB.BRANCH)) AS 'BANK',\n" +
                                "       ML.GROUP_ID,\n" +
                                "       PL.PROJECT_ID,\n" +
                                "       ML.BANK_ACCOUNT_ID,ML.LEDGER_ID\n" +
                                "  FROM  PROJECT_LEDGER PL\n" +
                                " INNER JOIN MASTER_LEDGER ML\n" +
                                " ON PL.LEDGER_ID = ML.LEDGER_ID\n" +
                                " INNER JOIN MASTER_BANK_ACCOUNT MBA\n" +
                                " ON MBA.LEDGER_ID = ML.LEDGER_ID\n" +
                                " INNER JOIN MASTER_BANK MB\n" +
                                " ON   MBA.BANK_ID=MB.BANK_ID\n" +
                                " WHERE PL.PROJECT_ID IN (?PROJECT_ID)\n" +
                                " GROUP BY MBA.LEDGER_ID \n" +
                                " ORDER BY BANK ASC";
                        break;
                    }
                case SQLCommand.Bank.FetchBankIdByLedgerId:
                    {
                        query = "SELECT BANK_ID FROM MASTER_BANK_ACCOUNT WHERE LEDGER_ID = ?LEDGER_ID";
                        break;
                    }
                case SQLCommand.Bank.FetchBankFDAccountDetailsByProjectId:
                    {
                        query = "SELECT T.BANK,\n" +
                                "       T.GROUP_ID,\n" +
                                "       T.PROJECT_ID,\n" +
                                "       T.BANK_ACCOUNT_ID,\n" +
                                "       T.LEDGER_ID,\n" +
                                "       T.FD_ACCOUNT_ID,\n" +
                                "       T.TYPE_NAME\n" +
                                "  FROM (SELECT MB.BANK_ID,\n" +
                                "               CONCAT(ML.LEDGER_NAME,\n" +
                                "                      CONCAT(' - ', MB.BANK),\n" +
                                "                      CONCAT(' - ', MB.BRANCH)) AS 'BANK',\n" +
                                "               ML.GROUP_ID,\n" +
                                "               PL.PROJECT_ID,\n" +
                                "               ML.BANK_ACCOUNT_ID,\n" +
                                "               ML.LEDGER_ID,\n" +
                                "               0 AS FD_ACCOUNT_ID,\n" +
                                "               'Bank Accounts' as TYPE_NAME\n" +
                                "     FROM PROJECT_LEDGER PL\n" +
                                "     INNER JOIN MASTER_LEDGER ML ON PL.LEDGER_ID = ML.LEDGER_ID\n" +
                                "     INNER JOIN MASTER_BANK_ACCOUNT MBA ON MBA.LEDGER_ID = ML.LEDGER_ID\n" +
                                "     INNER JOIN MASTER_BANK MB ON MBA.BANK_ID = MB.BANK_ID\n" +
                                "     LEFT JOIN (SELECT PLA.PROJECT_ID, PLA.LEDGER_ID, PLA.APPLICABLE_FROM, PLA.APPLICABLE_TO FROM PROJECT_LEDGER_APPLICABLE PLA " +
                                "         WHERE 1=1 {AND PLA.PROJECT_ID IN (?PROJECT_ID)} ) AS PLA ON PLA.PROJECT_ID = PL.PROJECT_ID AND PLA.LEDGER_ID = ML.LEDGER_ID " +
                                "     WHERE 1=1 {AND (MBA.DATE_CLOSED IS NULL OR MBA.DATE_CLOSED >= ?DATE_CLOSED) }\n" + //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                                "       {AND (((PLA.APPLICABLE_FROM BETWEEN ?APPLICABLE_FROM AND ?APPLICABLE_TO) OR PLA.APPLICABLE_FROM  IS NULL) " +
                                "        OR ( IF(PLA.APPLICABLE_TO IS NULL, PLA.APPLICABLE_TO IS NOT NULL, (PLA.APPLICABLE_TO BETWEEN ?APPLICABLE_FROM AND ?APPLICABLE_TO)) ))} " +   //On 29/09/2023, This property is used to skip bank ledger project based
                                "     UNION ALL\n" +
                                "     SELECT MB.BANK_ID,\n" +
                                "               CONCAT(FD.FD_ACCOUNT_NUMBER,\n" +
                                "                      CONCAT(' - ', MB.BANK),\n" +
                                "                      CONCAT(' - ', MB.BRANCH)) AS 'BANK',\n" +
                                "               ML.GROUP_ID,\n" +
                                "               FD.PROJECT_ID,\n" +
                                "               ML.BANK_ACCOUNT_ID,\n" +
                                "               ML.LEDGER_ID,\n" +
                                "               FD.FD_ACCOUNT_ID,\n" +
                                "               'Fixed Deposits' as TYPE_NAME\n" +
                                "     FROM PROJECT_LEDGER PL\n" +
                                "     INNER JOIN MASTER_LEDGER ML ON PL.LEDGER_ID = ML.LEDGER_ID\n" +
                                "     INNER JOIN FD_ACCOUNT FD ON FD.LEDGER_ID = ML.LEDGER_ID\n" +
                                "     INNER JOIN MASTER_BANK MB ON MB.BANK_ID = FD.BANK_ID\n" +
                                "     ORDER BY BANK ASC) AS T\n" +
                                " { WHERE PROJECT_ID IN (?PROJECT_ID) }\n" +
                                " GROUP BY BANK\n" +
                                " ORDER BY TYPE_NAME ASC";
                        break;
                    }
                case SQLCommand.Bank.FetchBankVoucher:
                    {
                        query = "SELECT VT.VOUCHER_ID, VT.LEDGER_ID, VMT.VOUCHER_NO, VMT.VOUCHER_DATE, BVP.LEDGER_NAME, \n" +
                                "CONCAT(ML.LEDGER_NAME, CONCAT(' - ' , CONCAT(MB.BANK, CONCAT(' - ', MB.BRANCH)))) AS BANK_LEDGER,VMT.NAME_ADDRESS,\n" +
                                "VT.AMOUNT, MB.BANK_ID\n" +
                                "FROM VOUCHER_TRANS VT\n" +
                                "INNER JOIN MASTER_LEDGER ML ON  ML.LEDGER_ID = VT.LEDGER_ID INNER JOIN master_bank_ACCOUNT BA ON ML.LEDGER_ID = BA.LEDGER_ID\n" +
                                "INNER JOIN MASTER_BANK MB ON BA.BANK_ID = MB.BANK_ID\n" +
                                "INNER JOIN VOUCHER_MASTER_TRANS VMT ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                "INNER JOIN (SELECT VT.VOUCHER_ID, ML.LEDGER_NAME FROM VOUCHER_TRANS VT\n" +
                                "INNER JOIN VOUCHER_MASTER_TRANS VMT ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                "INNER JOIN MASTER_LEDGER ML ON  ML.LEDGER_ID = VT.LEDGER_ID WHERE VT.TRANS_MODE ='DR'\n" +
                                "GROUP BY VT.VOUCHER_ID ORDER BY VT.SEQUENCE_NO ASC) AS BVP\n" +
                                "ON BVP.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                "WHERE VT.LEDGER_ID = ?LEDGER_ID AND VMT.PROJECT_ID = ?PROJECT_ID AND VT.TRANS_MODE ='CR'\n" +
                                "AND ML.GROUP_ID = 12 AND VMT.VOUCHER_TYPE IN ('PY','CN') AND VMT.STATUS= 1 AND\n" +
                                "VMT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                "ORDER BY VMT.VOUCHER_DATE, VMT.VOUCHER_NO ASC";
                        break;
                    }
            }
            return query;
        }
        #endregion Bank SQL
    }
}