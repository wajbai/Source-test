using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class LedgerSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.LedgerBank).FullName)
            {
                query = GetLedgerSQL();
            }
            else if (sqlCommandName == typeof(SQLCommand.CongregationLedgers).FullName)
            {
                query = GetCongregationLedgerSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the Ledger details.
        /// </summary>
        /// <returns></returns>
        private string GetLedgerSQL()
        {
            string query = "";
            SQLCommand.LedgerBank sqlCommandId = (SQLCommand.LedgerBank)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.LedgerBank.Add:
                    {
                        query = "INSERT INTO MASTER_LEDGER ( " +
                                    "LEDGER_CODE, " +
                                    "LEDGER_NAME, " +
                                    "GROUP_ID,  " +
                                    "LEDGER_TYPE, " +
                                    "LEDGER_SUB_TYPE, " +
                                    "BANK_ACCOUNT_ID, " +
                                    "IS_ASSET_GAIN_LEDGER, " +
                                    "IS_ASSET_LOSS_LEDGER, " +
                                    "IS_DISPOSAL_LEDGER,IS_SUBSIDY_LEDGER,IS_GST_LEDGERS, IS_COST_CENTER, NOTES, SORT_ID, " +
                                    "IS_BANK_INTEREST_LEDGER, IS_TDS_LEDGER, IS_INKIND_LEDGER, IS_DEPRECIATION_LEDGER, GST_SERVICE_TYPE, GST_HSN_SAC_CODE," +
                                    "BUDGET_GROUP_ID, BUDGET_SUB_GROUP_ID, IS_BANK_SB_INTEREST_LEDGER, IS_BANK_COMMISSION_LEDGER, IS_BANK_FD_PENALTY_LEDGER," +
                                    "FD_INVESTMENT_TYPE_ID, CUR_COUNTRY_ID, OP_EXCHANGE_RATE, DATE_CLOSED, CLOSED_BY) " +
                                    "VALUES( " +
                                    "?LEDGER_CODE, " +
                                    "?LEDGER_NAME, " +
                                    "?GROUP_ID,  " +
                                    "?LEDGER_TYPE, " +
                                    "?LEDGER_SUB_TYPE, " +
                                    "?BANK_ACCOUNT_ID, " +
                                    "?IS_ASSET_GAIN_LEDGER, " +
                                    "?IS_ASSET_LOSS_LEDGER, " +
                                    "?IS_DISPOSAL_LEDGER,?IS_SUBSIDY_LEDGER,?IS_GST_LEDGERS, ?IS_COST_CENTER, ?NOTES, ?SORT_ID, " +
                                    "?IS_BANK_INTEREST_LEDGER, ?IS_TDS_LEDGER, ?IS_INKIND_LEDGER, ?IS_DEPRECIATION_LEDGER, ?GST_SERVICE_TYPE, ?GST_HSN_SAC_CODE," +
                                    "?BUDGET_GROUP_ID, ?BUDGET_SUB_GROUP_ID, ?IS_BANK_SB_INTEREST_LEDGER, ?IS_BANK_COMMISSION_LEDGER, ?IS_BANK_FD_PENALTY_LEDGER," +
                                    "?FD_INVESTMENT_TYPE_ID, ?CUR_COUNTRY_ID, ?OP_EXCHANGE_RATE, ?DATE_CLOSED, ?CLOSED_BY) ";
                        break;
                    }
                case SQLCommand.LedgerBank.HeadOfficeAdd:
                    {
                        query = "INSERT INTO MASTER_HEADOFFICE_LEDGER ( " +
                                "LEDGER_CODE, " +
                                "LEDGER_NAME, " +
                                "GROUP_ID,  " +
                                "LEDGER_TYPE, " +
                                "LEDGER_SUB_TYPE, " +
                                "BANK_ACCOUNT_ID, " +
                                "IS_COST_CENTER, NOTES,SORT_ID, IS_BANK_INTEREST_LEDGER, IS_BANK_SB_INTEREST_LEDGER, IS_BANK_COMMISSION_LEDGER, IS_BANK_FD_PENALTY_LEDGER) " +
                                "VALUES( " +
                                "?LEDGER_CODE, " +
                                "?LEDGER_NAME, " +
                                "?GROUP_ID,  " +
                                "?LEDGER_TYPE, " +
                                "?LEDGER_SUB_TYPE, " +
                                "?BANK_ACCOUNT_ID, " +
                                "?IS_COST_CENTER,?NOTES,?SORT_ID,?IS_BANK_INTEREST_LEDGER, ?IS_BANK_SB_INTEREST_LEDGER, ?IS_BANK_COMMISSION_LEDGER, ?IS_BANK_FD_PENALTY_LEDGER) ";
                        break;
                    }
                case SQLCommand.LedgerBank.HeadOfficeLedgerUpdate:
                    {
                        query = "UPDATE MASTER_HEADOFFICE_LEDGER SET " +
                                    "LEDGER_CODE =?LEDGER_CODE, " +
                                    "LEDGER_NAME =?LEDGER_NAME, " +
                                    "GROUP_ID=?GROUP_ID, " +
                                    "LEDGER_TYPE=?LEDGER_TYPE," +
                                    "LEDGER_SUB_TYPE=?LEDGER_SUB_TYPE," +
                                    "BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID," +
                                    "IS_COST_CENTER=?IS_COST_CENTER," +
                                    "IS_BANK_INTEREST_LEDGER=?IS_BANK_INTEREST_LEDGER," +
                                    "IS_BANK_SB_INTEREST_LEDGER=?IS_BANK_SB_INTEREST_LEDGER," +
                                    "IS_BANK_COMMISSION_LEDGER=?IS_BANK_COMMISSION_LEDGER," +
                                    "IS_BANK_FD_PENALTY_LEDGER=?IS_BANK_FD_PENALTY_LEDGER," +
                                    "NOTES=?NOTES" +
                                    " WHERE HEADOFFICE_LEDGER_ID=?LEDGER_ID ";
                        break;
                    }
                case SQLCommand.LedgerBank.Update:
                    {
                        query = "UPDATE MASTER_LEDGER SET " +
                                    "LEDGER_CODE =?LEDGER_CODE, " +
                                    "LEDGER_NAME =?LEDGER_NAME, " +
                                    "GROUP_ID=?GROUP_ID, " +
                                    "LEDGER_TYPE=?LEDGER_TYPE," +
                                    "LEDGER_SUB_TYPE=?LEDGER_SUB_TYPE," +
                                    "BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID, " +
                                    "IS_SUBSIDY_LEDGER=?IS_SUBSIDY_LEDGER, " +
                                    "IS_GST_LEDGERS=?IS_GST_LEDGERS, " +
                                    "IS_COST_CENTER=?IS_COST_CENTER, " +
                                    "IS_ASSET_GAIN_LEDGER=?IS_ASSET_GAIN_LEDGER, " +
                                    "IS_ASSET_LOSS_LEDGER=?IS_ASSET_LOSS_LEDGER, " +
                                    "IS_DISPOSAL_LEDGER=?IS_DISPOSAL_LEDGER, " +
                                    "IS_BANK_INTEREST_LEDGER=?IS_BANK_INTEREST_LEDGER, " +
                                    "IS_TDS_LEDGER=?IS_TDS_LEDGER, " +
                                    "NOTES=?NOTES, " +
                                    "IS_INKIND_LEDGER=?IS_INKIND_LEDGER, " +
                                    "IS_DEPRECIATION_LEDGER=?IS_DEPRECIATION_LEDGER," +
                                    "GST_SERVICE_TYPE=?GST_SERVICE_TYPE, GST_HSN_SAC_CODE=?GST_HSN_SAC_CODE," +
                                    "IS_BANK_FD_PENALTY_LEDGER=?IS_BANK_FD_PENALTY_LEDGER," +
                                    "IS_BANK_SB_INTEREST_LEDGER = ?IS_BANK_SB_INTEREST_LEDGER," +
                                    "IS_BANK_COMMISSION_LEDGER = ?IS_BANK_COMMISSION_LEDGER," +
                                    "BUDGET_GROUP_ID=?BUDGET_GROUP_ID, " +
                                    "BUDGET_SUB_GROUP_ID=?BUDGET_SUB_GROUP_ID," +
                                    "FD_INVESTMENT_TYPE_ID=?FD_INVESTMENT_TYPE_ID, CUR_COUNTRY_ID=?CUR_COUNTRY_ID, OP_EXCHANGE_RATE = ?OP_EXCHANGE_RATE," +
                                    "DATE_CLOSED = ?DATE_CLOSED," +
                                    "CLOSED_BY=?CLOSED_BY" +
                                    " WHERE LEDGER_ID=?LEDGER_ID ";
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateClosedDate:
                    {
                        query = "UPDATE MASTER_LEDGER SET DATE_CLOSED=?LEDGER_DATE_CLOSED, CLOSED_BY=?CLOSED_BY WHERE LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateByHeadOffice: //On 04/07/2019, to skip updation of BO ledger option
                    {
                        query = "UPDATE MASTER_LEDGER SET " +
                                    "LEDGER_CODE =?LEDGER_CODE, " +
                                    "LEDGER_NAME =?LEDGER_NAME, " +
                                    "GROUP_ID=?GROUP_ID, " +
                                    "BUDGET_GROUP_ID=?BUDGET_GROUP_ID, " +
                                    "BUDGET_SUB_GROUP_ID=?BUDGET_SUB_GROUP_ID, " +
                                    "LEDGER_TYPE=?LEDGER_TYPE," +
                                    "LEDGER_SUB_TYPE=?LEDGER_SUB_TYPE," +
                                    "NOTES=?NOTES," +
                                    "IS_BANK_INTEREST_LEDGER = IF(IS_BANK_INTEREST_LEDGER = 0, ?IS_BANK_INTEREST_LEDGER, IS_BANK_INTEREST_LEDGER), " +
                                    "IS_COST_CENTER = IF(IS_COST_CENTER = 0, ?IS_COST_CENTER, IS_COST_CENTER), " +
                                    "IS_TDS_LEDGER = IF(IS_TDS_LEDGER = 0, ?IS_TDS_LEDGER, IS_TDS_LEDGER), " +
                                    "IS_DEPRECIATION_LEDGER = IF(IS_DEPRECIATION_LEDGER = 0, ?IS_DEPRECIATION_LEDGER, IS_DEPRECIATION_LEDGER), " +
                                    "IS_ASSET_GAIN_LEDGER = IF(IS_ASSET_GAIN_LEDGER = 0, ?IS_ASSET_GAIN_LEDGER, IS_ASSET_GAIN_LEDGER), " +
                                    "IS_ASSET_LOSS_LEDGER = IF(IS_ASSET_LOSS_LEDGER = 0, ?IS_ASSET_LOSS_LEDGER, IS_ASSET_LOSS_LEDGER), " +
                                    "IS_DISPOSAL_LEDGER = IF(IS_DISPOSAL_LEDGER = 0, ?IS_DISPOSAL_LEDGER, IS_DISPOSAL_LEDGER), " +
                                    "IS_INKIND_LEDGER = IF(IS_INKIND_LEDGER = 0, ?IS_INKIND_LEDGER, IS_INKIND_LEDGER), " +
                                    "IS_SUBSIDY_LEDGER = IF(IS_SUBSIDY_LEDGER = 0, ?IS_SUBSIDY_LEDGER, IS_SUBSIDY_LEDGER), " +
                                    "IS_BANK_SB_INTEREST_LEDGER = IF(IS_BANK_SB_INTEREST_LEDGER  = 0, ?IS_BANK_SB_INTEREST_LEDGER, IS_BANK_SB_INTEREST_LEDGER), " +
                                    "IS_BANK_COMMISSION_LEDGER= IF(IS_BANK_COMMISSION_LEDGER  = 0, ?IS_BANK_COMMISSION_LEDGER, IS_BANK_COMMISSION_LEDGER), " +
                                    "IS_BANK_FD_PENALTY_LEDGER= IF(IS_BANK_FD_PENALTY_LEDGER  = 0, ?IS_BANK_FD_PENALTY_LEDGER, IS_BANK_FD_PENALTY_LEDGER), " +
                                    "FD_INVESTMENT_TYPE_ID = ?FD_INVESTMENT_TYPE_ID," +
                                    "DATE_CLOSED = ?DATE_CLOSED, CLOSED_BY=?CLOSED_BY" +
                                    " WHERE LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.Delete:
                    {
                        //query = "UPDATE MASTER_LEDGER SET STATUS=1 WHERE LEDGER_ID=?LEDGER_ID;";
                        query = "DELETE FROM MASTER_LEDGER WHERE LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.BankAccountDelete:
                    {
                        query = "DELETE FROM MASTER_BANK_ACCOUNT WHERE BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.Fetch:
                    {
                        query = "SELECT ML.LEDGER_ID, ML.GROUP_ID, LG.NATURE_ID,\n" +
                        "       LEDGER_CODE,\n" +
                        "       TCP.NICK_NAME,\n" +
                        "       DEDUTEE_TYPE_ID,\n" +
                        "       NATURE_OF_PAYMENT_ID,\n" +
                        "       CREDITORS_PROFILE_ID,\n" +
                        "       NAME,\n" +
                        "       ADDRESS,\n" +
                        "       STATE_ID,\n" +
                        "       TYPE_ID,\n" +
                        "       COUNTRY_ID,\n" +
                        "       GST_Id,\n" +
                        "       GST_NO,\n" +
                        "       GST_SERVICE_TYPE, GST_HSN_SAC_CODE,\n" +
                        "       PIN_CODE,\n" +
                        "       CONTACT_PERSON,\n" +
                        "       CONTACT_NUMBER,\n" +
                        "       EMAIL,\n" +
                        "       LEDGER_NAME,\n" +
                        "       PAN_NUMBER,\n" +
                        "       LEDGER_TYPE,\n" +
                        "       LEDGER_SUB_TYPE,\n" +
                        "       MBA.BANK_ACCOUNT_ID,\n" +
                        "       IS_COST_CENTER,\n" +
                        "       IS_BANK_INTEREST_LEDGER,\n" +
                        "       IS_SUBSIDY_LEDGER,\n" +
                        "       IS_TDS_LEDGER,\n" +
                        "       IS_GST_LEDGERS,\n" +
                        "       IS_ASSET_GAIN_LEDGER,\n" +
                        "       IS_ASSET_LOSS_LEDGER,\n" +
                        "       IS_DISPOSAL_LEDGER, IS_BANK_SB_INTEREST_LEDGER, IS_BANK_COMMISSION_LEDGER, IS_BANK_FD_PENALTY_LEDGER, \n" +
                        "       ML.NOTES,\n" +
                        "       LEDGER_TYPE,\n" +
                        "       MBA.BANK_ID,\n" +
                        "       MBA.DATE_OPENED,\n" +
                        "       MBA.DATE_CLOSED,\n" +
                        "       MBA.OPERATED_BY,\n" +
                        "       MBA.ACCOUNT_HOLDER_NAME, IFNULL(ML.CUR_COUNTRY_ID, 0) AS CUR_COUNTRY_ID, ML.OP_EXCHANGE_RATE, \n" +
                        "       IS_INKIND_LEDGER,\n" +
                        "       IS_DEPRECIATION_LEDGER,\n" +
                        "       IFNULL(HML.HEADOFFICE_LEDGER_ID, 0) AS HEADOFFICE_LEDGER_ID, ML.FD_INVESTMENT_TYPE_ID,\n" +
                        "       BUDGET_GROUP_ID,\n" +
                        "       BUDGET_SUB_GROUP_ID, ML.DATE_CLOSED as LEDGER_DATE_CLOSED, ML.CLOSED_BY\n" +
                        "  FROM MASTER_LEDGER AS ML INNER JOIN MASTER_LEDGER_GROUP LG ON LG.GROUP_ID = ML.GROUP_ID\n" +
                        "  LEFT JOIN TDS_CREDTIORS_PROFILE AS TCP ON ML.LEDGER_ID = TCP.LEDGER_ID\n" +
                        "  LEFT JOIN MASTER_BANK_ACCOUNT AS MBA ON ML.LEDGER_ID = MBA.LEDGER_ID\n" +
                        "  LEFT JOIN HEADOFFICE_MAPPED_LEDGER HML ON HML.LEDGER_ID = ML.LEDGER_ID\n" +
                        " WHERE ML.LEDGER_ID = ?LEDGER_ID AND STATUS = 0";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchAll:
                    {
                        //On 02/02/2021, To include Project Category
                        query = @"SELECT ML.LEDGER_ID,
                                            MG.GROUP_ID,
                                            SORT_ID,
                                            ML.LEDGER_CODE,
                                            CASE
                                                WHEN ML.LEDGER_SUB_TYPE = 'BK' THEN
                                                CONCAT(CONCAT(ML.LEDGER_NAME, ' - '), CONCAT(BANK, ' - '), BRANCH)
                                                ELSE
                                                ML.LEDGER_NAME
                                            END AS 'LEDGER_NAME', IFNULL(MC.CURRENCY_NAME, '') AS CURRENCY_NAME, ML.OP_EXCHANGE_RATE,
                                            MG.NATURE_ID, MA.NATURE,
                                            CASE
                                                WHEN ML.LEDGER_SUB_TYPE = 'BK' THEN
                                                'Bank Accounts'
                                                ELSE
                                                CASE
                                                WHEN ML.LEDGER_SUB_TYPE = 'FD' THEN
                                                    'Fixed Deposit'
                                                ELSE
                                                    MG.LEDGER_GROUP
                                                END
                                            END AS 'GROUP',
                                            ML.LEDGER_TYPE,
                                            ML.LEDGER_SUB_TYPE, IFNULL(MIT.INVESTMENT_TYPE, '') AS FD_INVESTMENT_TYPE, 
                                            MBA.BANK_ACCOUNT_ID,
                                            ML.ACCESS_FLAG,
                                            ML.IS_TDS_LEDGER,
                                            IF(IS_COST_CENTER=1,'Yes', 'No') AS IS_COST_CENTER, IF(IS_BANK_INTEREST_LEDGER=1, 'Yes','No') AS IS_BANK_INTEREST_LEDGER, 
                                            IF(IS_GST_LEDGERS=1, 'Yes', 'No') AS IS_GST_LEDGERS,  IF(IS_GST_LEDGERS=1, IF(GST_SERVICE_TYPE=1,'Service', 'Goods'),'') AS GST_SERVICE_TYPE, MGS.SLAB,
                                            IFNULL(BG.BUDGET_GROUP, '') AS BUDGET_GROUP, IFNULL(BSG.BUDGET_SUB_GROUP, '') AS BUDGET_SUB_GROUP,
                                            PC.PROJECT_CATOGORY_NAME, IFNULL(HL.LEDGER_NAME,'') AS  HEADOFFICE_LEDGER_NAME, ML.DATE_CLOSED, ML.CLOSED_BY
                                        FROM MASTER_LEDGER ML
                                        INNER JOIN MASTER_LEDGER_GROUP MG ON ML.GROUP_ID = MG.GROUP_ID
                                        INNER JOIN MASTER_NATURE MA ON MA.NATURE_ID =  MG.NATURE_ID
                                        LEFT JOIN MASTER_BANK_ACCOUNT MBA ON ML.LEDGER_ID = MBA.LEDGER_ID
                                        LEFT JOIN MASTER_BANK MB ON MB.BANK_ID = MBA.BANK_ID
                                        LEFT JOIN TDS_CREDTIORS_PROFILE TCP ON TCP.LEDGER_ID = ML.LEDGER_ID
                                        LEFT JOIN MASTER_GST_CLASS MGS ON MGS.GST_ID = TCP.GST_ID
                                        LEFT JOIN BUDGET_GROUP BG ON BG.BUDGET_GROUP_ID = ML.BUDGET_GROUP_ID
                                        LEFT JOIN BUDGET_SUB_GROUP BSG ON BSG.BUDGET_SUB_GROUP_ID = ML.BUDGET_SUB_GROUP_ID
                                        LEFT JOIN MASTER_INVESTMENT_TYPE MIT ON MIT.INVESTMENT_TYPE_ID = ML.FD_INVESTMENT_TYPE_ID
                                        LEFT JOIN (SELECT PCL.LEDGER_ID, GROUP_CONCAT(MPC.PROJECT_CATOGORY_NAME SEPARATOR  ', ') AS PROJECT_CATOGORY_NAME
                                            FROM PROJECT_CATEGORY_LEDGER PCL
                                            INNER JOIN MASTER_PROJECT_CATOGORY MPC ON MPC.PROJECT_CATOGORY_ID = PCL.PROJECT_CATEGORY_ID
                                            GROUP BY PCL.LEDGER_ID) AS PC ON PC.LEDGER_ID = ML.LEDGER_ID

                                        LEFT JOIN (SELECT HML.LEDGER_ID, GROUP_CONCAT(MHL.LEDGER_NAME) AS LEDGER_NAME 
                                                FROM MASTER_HEADOFFICE_LEDGER MHL
                                                INNER JOIN HEADOFFICE_MAPPED_LEDGER HML ON HML.HEADOFFICE_LEDGER_ID = MHL.HEADOFFICE_LEDGER_ID
                                                GROUP BY MHL.HEADOFFICE_LEDGER_ID) AS HL ON HL.LEDGER_NAME = ML.LEDGER_NAME
                                        LEFT JOIN MASTER_COUNTRY MC ON MC.COUNTRY_ID = ML.CUR_COUNTRY_ID
                                        WHERE ML.STATUS = 0 ORDER BY SORT_ID ASC;";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchAllIntegration: // FILTERED closed date on 13.04.2022
                    {
                        query = @"SELECT  MG.GROUP_ID,
                                            CASE
                                                WHEN ML.LEDGER_SUB_TYPE = 'BK' THEN
                                                -- CONCAT(CONCAT(ML.LEDGER_NAME, ' - '), CONCAT(BANK, ' - '), BRANCH)
                                                ML.LEDGER_NAME
                                                ELSE
                                                ML.LEDGER_NAME
                                            END AS 'LEDGER_NAME',
                                            MG.NATURE_ID,
                                            CASE
                                                WHEN ML.LEDGER_SUB_TYPE = 'BK' THEN
                                                'Bank Accounts'
                                                ELSE
                                                CASE
                                                WHEN ML.LEDGER_SUB_TYPE = 'FD' THEN
                                                    'Fixed Deposit'
                                                ELSE
                                                    MG.LEDGER_GROUP
                                                END
                                            END AS 'GROUP',
                                            MP.PROJECT AS 'PROJECT'
                                        FROM MASTER_LEDGER ML
                                        INNER JOIN MASTER_LEDGER_GROUP MG ON ML.GROUP_ID = MG.GROUP_ID AND ML.DATE_CLOSED IS NULL
                                        LEFT JOIN MASTER_BANK_ACCOUNT MBA ON ML.LEDGER_ID = MBA.LEDGER_ID
                                        LEFT JOIN MASTER_BANK MB ON MB.BANK_ID = MBA.BANK_ID
                                        LEFT JOIN TDS_CREDTIORS_PROFILE TCP ON TCP.LEDGER_ID = ML.LEDGER_ID
                                        LEFT JOIN MASTER_GST_CLASS MGS ON MGS.GST_ID = TCP.GST_ID
                                        INNER JOIN PROJECT_LEDGER PL ON ML.LEDGER_ID= PL.LEDGER_ID
                                        LEFT JOIN MASTER_PROJECT MP ON MP.PROJECT_ID = PL.PROJECT_ID
                                        LEFT JOIN USER_PROJECT UP ON MP.PROJECT_ID=UP.PROJECT_ID
                                        WHERE ML.STATUS = 0 {AND UP.ROLE_ID=?USERROLE_ID} AND (ML.GROUP_ID IN (12,13) OR ML.ACCESS_FLAG <>2) ORDER BY SORT_ID ASC;";
                        break;
                    }

                case SQLCommand.LedgerBank.FetchMergeLedgers:
                    {
                        query = @"SELECT CASE
                                             WHEN MG.NATURE_ID = 1 OR MG.NATURE_ID = 4 THEN 'DR'
                                             ELSE 'CR'
                                           END AS TRANS_MODE,
                                           ML.LEDGER_ID,
                                           MG.GROUP_ID,
                                           SORT_ID, MG.NATURE_ID,
                                           ML.LEDGER_CODE,
                                           CONCAT(ML.LEDGER_NAME, ' (', LEDGER_GROUP, ')-', NATURE,'-',LEDGER_CODE) AS 'LEDGER_NAME',
                                            IF(HML.LEDGER_ID IS NULL,'No','Yes') AS YES_NO,
                                           CASE
                                             WHEN ML.LEDGER_SUB_TYPE = 'BK' THEN 'Bank Accounts'
                                             ELSE 
                                              CASE
                                                WHEN ML.LEDGER_SUB_TYPE = 'FD' THEN 'Fixed Deposit'
                                                ELSE MG.LEDGER_GROUP
                                              END
                                           END AS 'GROUP',
                                           ML.LEDGER_TYPE,
                                           ML.LEDGER_SUB_TYPE,
                                           ML.BANK_ACCOUNT_ID,
                                           ML.ACCESS_FLAG,
                                           ML.IS_COST_CENTER, IS_BANK_INTEREST_LEDGER,
                                           ML.IS_TDS_LEDGER , ML.IS_BRANCH_LEDGER ,0 MERGE_LEDGER_ID, 
                                           GROUP_CONCAT(IFNULL(PROJECT_CATOGORY_NAME,'') ORDER BY PROJECT_CATOGORY_NAME ASC) AS PROJECT_CATOGORY_NAME, 
                                           IFNULL(MP.PROJECT, '') AS PROJECT, IFNULL(MP.PROJECT_ID, '') AS PROJECT_ID
                                      FROM MASTER_LEDGER ML
                                        INNER JOIN MASTER_LEDGER_GROUP MG  ON ML.GROUP_ID = MG.GROUP_ID AND ML.STATUS = 0
                                        INNER JOIN MASTER_NATURE MN ON MG.NATURE_ID = MN.NATURE_ID
                                        LEFT JOIN HEADOFFICE_MAPPED_LEDGER HML ON ML.LEDGER_ID=HML.LEDGER_ID
                                        LEFT JOIN PROJECT_CATEGORY_LEDGER PCL ON PCL.LEDGER_ID = ML.LEDGER_ID
                                        LEFT JOIN MASTER_PROJECT_CATOGORY PC ON PC.PROJECT_CATOGORY_ID = PCL.PROJECT_CATEGORY_ID
                                        LEFT JOIN (SELECT PL.LEDGER_ID, GROUP_CONCAT(MP.PROJECT_ID, '') AS PROJECT_ID, GROUP_CONCAT(MP.PROJECT, '') AS PROJECT FROM PROJECT_LEDGER PL
                                        INNER JOIN MASTER_PROJECT MP ON MP.PROJECT_ID = PL.PROJECT_ID GROUP BY PL.LEDGER_ID) MP ON MP.LEDGER_ID = ML.LEDGER_ID
                                     GROUP BY LEDGER_ID
                                    ORDER BY SORT_ID, LEDGER_NAME,YES_NO ASC;";
                        break;
                    }

                case SQLCommand.LedgerBank.BankAccountFetchAll:
                    {
                        query = "SELECT MA.BANK_ACCOUNT_ID, MA.ACCOUNT_CODE, MA.ACCOUNT_NUMBER, MB.BANK, MB.BRANCH, MA.DATE_OPENED AS DATE_OPENED, " +
                                    "CONCAT(MA.ACCOUNT_NUMBER,' (',MB.BANK,'-',MB.BRANCH,' )') AS BANK_BRANCH, MA.DATE_CLOSED AS DATE_CLOSED, MA.LEDGER_ID," +
                                    "PL.PROJECT, PL.NO_PROJECTS, IFNULL(MC.CURRENCY_NAME, '') AS CURRENCY_NAME, ML.OP_EXCHANGE_RATE " +
                                    "FROM MASTER_BANK_ACCOUNT MA " +
                                    "INNER JOIN  MASTER_BANK MB ON MA.BANK_ID=MB.BANK_ID " +
                                    "INNER JOIN  MASTER_LEDGER AS ML ON MA.LEDGER_ID = ML.LEDGER_ID " +
                                    "LEFT JOIN MASTER_COUNTRY MC ON MC.COUNTRY_ID = ML.CUR_COUNTRY_ID " +
                                    "LEFT JOIN (SELECT GROUP_CONCAT(MP.PROJECT_ID) AS PROJECT_ID, GROUP_CONCAT(MP.PROJECT SEPARATOR  ', ')  AS PROJECT,  " +
                                    "     PL.LEDGER_ID, COUNT(MP.PROJECT_ID) as NO_PROJECTS  " +
                                    "     FROM PROJECT_LEDGER PL INNER JOIN MASTER_PROJECT MP ON MP.PROJECT_ID = PL.PROJECT_ID GROUP BY PL.LEDGER_ID) PL   " +
                                    "     ON PL.LEDGER_ID = ML.LEDGER_ID  " +
                                    "WHERE MA.ACCOUNT_TYPE_ID=1 ORDER BY MB.BANK"; //ACCOUNT_TYPE_ID=1 Saving Account  

                        break;
                    }
                case SQLCommand.LedgerBank.FetchUnmappedBankAccounts:
                    {
                        //query = "SELECT MA.BANK_ACCOUNT_ID,\n" +
                        //           "       MA.ACCOUNT_CODE,\n" +
                        //           "       MA.ACCOUNT_NUMBER,\n" +
                        //           "       MB.BANK,\n" +
                        //           "       MB.BRANCH,\n" +
                        //           "       MA.DATE_OPENED,\n" +
                        //           "       CONCAT(MA.ACCOUNT_NUMBER, ' (', MB.BANK, '-', MB.BRANCH, ' )') AS BANK_BRANCH,\n" +
                        //           "       MA.DATE_CLOSED,\n" +
                        //           "       MA.LEDGER_ID,\n" +
                        //           "       MP.PROJECT_ID,\n" +
                        //           "       MP.PROJECT\n" +
                        //           "  FROM MASTER_BANK_ACCOUNT MA\n" +
                        //           "  LEFT JOIN MASTER_BANK MB\n" +
                        //           "    ON MA.BANK_ID = MB.BANK_ID\n" +
                        //           "  INNER JOIN MASTER_LEDGER ML\n" +
                        //           "    ON MA.LEDGER_ID=ML.LEDGER_ID\n" +
                        //           "  LEFT JOIN PROJECT_LEDGER PL\n" +
                        //           "    ON PL.LEDGER_ID=ML.LEDGER_ID\n" +
                        //           "  LEFT JOIN MASTER_PROJECT MP\n" +
                        //           "    ON PL.PROJECT_ID=MP.PROJECT_ID\n" +
                        //           " WHERE MA.ACCOUNT_TYPE_ID = 1 AND MP.PROJECT_ID IS NULL\n" +
                        //           " ORDER BY MB.BANK";

                        query = "SELECT T.BANK_ACCOUNT_ID,\n" +
                                    "       T.ACCOUNT_CODE,\n" +
                                    "       T.ACCOUNT_NUMBER,\n" +
                                    "       T.BANK,\n" +
                                    "       T.BRANCH,\n" +
                                    "       T.DATE_OPENED,\n" +
                                    "       CONCAT(T.ACCOUNT_NUMBER, ' (', T.BANK, '-', T.BRANCH, ' )') AS BANK_BRANCH,\n" +
                                    "       T.DATE_CLOSED,\n" +
                                    "       T.LEDGER_ID,\n" +
                                    "       T.PROJECT_ID,\n" +
                                    "       T.PROJECT,\n" +
                                    "       T.DIVISION_ID\n" +
                                    "  FROM ((SELECT MA.BANK_ACCOUNT_ID,\n" +
                                    "                MA.ACCOUNT_CODE,\n" +
                                    "                MA.ACCOUNT_NUMBER,\n" +
                                    "                MB.BANK,\n" +
                                    "                MB.BRANCH,\n" +
                                    "                MA.DATE_OPENED,\n" +
                                    "                CONCAT(MA.ACCOUNT_NUMBER,\n" +
                                    "                       ' (',\n" +
                                    "                       MB.BANK,\n" +
                                    "                       '-',\n" +
                                    "                       MB.BRANCH,\n" +
                                    "                       ' )') AS BANK_BRANCH,\n" +
                                    "                MA.DATE_CLOSED,\n" +
                                    "                MA.LEDGER_ID,\n" +
                                    "                MP.PROJECT_ID,\n" +
                                    "                MP.PROJECT,\n" +
                                    "                MP.DIVISION_ID\n" +
                                    "           FROM MASTER_BANK_ACCOUNT MA\n" +
                                    "           LEFT JOIN MASTER_BANK MB\n" +
                                    "             ON MA.BANK_ID = MB.BANK_ID\n" +
                                    "          INNER JOIN MASTER_LEDGER ML\n" +
                                    "             ON MA.LEDGER_ID = ML.LEDGER_ID\n" +
                                    "           LEFT JOIN PROJECT_LEDGER PL\n" +
                                    "             ON PL.LEDGER_ID = ML.LEDGER_ID\n" +
                                    "           LEFT JOIN MASTER_PROJECT MP\n" +
                                    "             ON PL.PROJECT_ID = MP.PROJECT_ID\n" +
                                    "          WHERE MA.ACCOUNT_TYPE_ID = 1\n" +
                                    "            AND MP.PROJECT_ID IS NULL) UNION\n" +
                                    "        (SELECT MA.BANK_ACCOUNT_ID,\n" +
                                    "                MA.ACCOUNT_CODE,\n" +
                                    "                MA.ACCOUNT_NUMBER,\n" +
                                    "                MB.BANK,\n" +
                                    "                MB.BRANCH,\n" +
                                    "                MA.DATE_OPENED,\n" +
                                    "                CONCAT(MA.ACCOUNT_NUMBER, ' (', MB.BANK, '-', MB.BRANCH, ' )') AS BANK_BRANCH,\n" +
                                    "                MA.DATE_CLOSED,\n" +
                                    "                MA.LEDGER_ID,\n" +
                                    "                MP.PROJECT_ID,\n" +
                                    "                MP.PROJECT,\n" +
                                    "                MP.DIVISION_ID\n" +
                                    "           FROM MASTER_BANK_ACCOUNT MA\n" +
                                    "           LEFT JOIN MASTER_BANK MB\n" +
                                    "             ON MA.BANK_ID = MB.BANK_ID\n" +
                                    "          INNER JOIN MASTER_LEDGER ML\n" +
                                    "             ON MA.LEDGER_ID = ML.LEDGER_ID\n" +
                                    "           LEFT JOIN PROJECT_LEDGER PL\n" +
                                    "             ON PL.LEDGER_ID = ML.LEDGER_ID\n" +
                                    "           LEFT JOIN MASTER_PROJECT MP\n" +
                                    "             ON PL.PROJECT_ID = MP.PROJECT_ID\n" +
                                    "          WHERE MA.ACCOUNT_TYPE_ID = 1\n" +
                                    "            AND MP.DIVISION_ID = 2)) AS T\n" +
                                    " GROUP BY T.BANK_BRANCH ASC;";
                        break;
                    }
                case SQLCommand.LedgerBank.FixedDepositFetchAll:
                    {
                        //query = "SELECT " +
                        //             "MA.BANK_ACCOUNT_ID, " +
                        //             "MA.ACCOUNT_CODE, " +
                        //             "MA.ACCOUNT_NUMBER, " +
                        //             "MB.BANK, " +
                        //             "MB.BRANCH, " +
                        //             "MA.DATE_OPENED, " +
                        //             "MA.MATURITY_DATE " +
                        //           "FROM " +
                        //             "MASTER_BANK_ACCOUNT MA LEFT JOIN  MASTER_BANK MB ON MA.BANK_ID=MB.BANK_ID  WHERE MA.ACCOUNT_TYPE_ID=2 ORDER BY MB.BANK ASC";//ACCOUNT_TYPE_ID=2 Fixed Deposit

                        query = "SELECT MA.BANK_ACCOUNT_ID,\n" +
                                "       MA.ACCOUNT_CODE,\n" +
                                "       MA.ACCOUNT_NUMBER,\n" +
                                "       CONCAT(MP.PROJECT, ' - ', MD.DIVISION) AS 'PROJECT',\n" +
                                "       MB.BANK,\n" +
                                "       MB.BRANCH,\n" +
                                "       MA.DATE_OPENED,\n" +
                                "       MA.MATURITY_DATE\n" +
                                "  FROM MASTER_BANK_ACCOUNT MA\n" +
                                "  LEFT JOIN MASTER_BANK MB\n" +
                                "    ON MA.BANK_ID = MB.BANK_ID\n" +
                                "  LEFT JOIN MASTER_LEDGER ML\n" +
                                "    ON MA.BANK_ACCOUNT_ID = ML.BANK_ACCOUNT_ID\n" +
                                "  LEFT JOIN PROJECT_LEDGER PL\n" +
                                "    ON ML.LEDGER_ID = PL.LEDGER_ID\n" +
                                "  LEFT JOIN MASTER_PROJECT MP\n" +
                                "    ON PL.PROJECT_ID = MP.PROJECT_ID\n" +
                                "  LEFT JOIN MASTER_DIVISION MD\n" +
                                "    ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                                " WHERE MA.ACCOUNT_TYPE_ID = 2\n" +
                                " ORDER BY MB.BANK ASC";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchFixedDepositCodes:
                    {
                        query = "SELECT MA.ACCOUNT_CODE\n" +
                                "  FROM MASTER_BANK_ACCOUNT MA\n" +
                                "  LEFT JOIN MASTER_BANK MB\n" +
                                "    ON MA.BANK_ID = MB.BANK_ID\n" +
                                " WHERE MA.ACCOUNT_TYPE_ID = 2\n" +
                                " ORDER BY MA.BANK_ACCOUNT_ID DESc";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerForLookup:
                    {
                        query = "SELECT " +
                                  "ML.LEDGER_ID,ML.LEDGER_CODE, " +
                                   "CONCAT(ML.LEDGER_NAME,CONCAT(' - ',MP.LEDGER_GROUP)) AS LEDGER_NAME " +
                             "FROM " +
                                 "MASTER_LEDGER ML,MASTER_LEDGER_GROUP MP WHERE ML.GROUP_ID=MP.GROUP_ID AND ML.STATUS=0 ORDER BY LEDGER_CODE ASC ";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchBudgetGroupLookup:
                    {
                        query = "SELECT BUDGET_GROUP_ID, BUDGET_GROUP FROM BUDGET_GROUP ORDER BY BUDGET_GROUP_SORT_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchBudgetSubGroupLookup:
                    {
                        query = "SELECT BUDGET_SUB_GROUP_ID, BUDGET_SUB_GROUP FROM BUDGET_SUB_GROUP ORDER BY BUDGET_SUB_GROUP_SORT_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.LedgerIdFetch:
                    {
                        query = "SELECT " +
                                      "LEDGER_ID " +
                                    "FROM " +
                                      "MASTER_BANK_ACCOUNT WHERE BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.CheckTransactionExistsByDateClose:
                    {
                        query = "SELECT VMT.VOUCHER_ID, VT.LEDGER_ID\n" +
                                "  FROM VOUCHER_MASTER_TRANS VMT\n" +
                                " INNER JOIN VOUCHER_TRANS VT\n" +
                                "    ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                                " WHERE VMT.VOUCHER_DATE > ?DATE_CLOSED {AND VMT.PROJECT_ID IN (?PROJECT_ID)}\n" +
                                "   AND VT.LEDGER_ID = ?LEDGER_ID AND VMT.STATUS = 1 ";
                        break;
                    }
                case SQLCommand.LedgerBank.CheckTransactionExistsByDateFrom:
                    {
                        query = "SELECT VMT.VOUCHER_ID, VT.LEDGER_ID\n" +
                                "  FROM VOUCHER_MASTER_TRANS VMT\n" +
                                " INNER JOIN VOUCHER_TRANS VT\n" +
                                "    ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                                " WHERE VMT.VOUCHER_DATE < ?DATE_FROM {AND VMT.PROJECT_ID IN (?PROJECT_ID)}\n" +
                                "   AND VT.LEDGER_ID = ?LEDGER_ID AND VMT.STATUS = 1 ";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchClosedLedgersByDate:
                    {
                        query = "SELECT LEDGER_ID, LEDGER_NAME, GROUP_ID, ACCESS_FLAG FROM MASTER_LEDGER WHERE DATE_CLOSED IS NOT NULL AND DATE_CLOSED < ?DATE_CLOSED;";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchReferedVoucherByLedgerId:
                    {
                        query = "SELECT COUNT(LEDGER_ID) COUNT FROM VOUCHER_MASTER_TRANS  VMT LEFT JOIN " +
                                " VOUCHER_TRANS VT ON VT.VOUCHER_ID = VMT.VOUCHER_ID WHERE VMT.VOUCHER_TYPE ='JN' " +
                                " AND VT.LEDGER_ID =?LEDGER_ID AND VT.REFERENCE_NUMBER IS NOT NULL";
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateLedgerReferenceNo:
                    {
                        query = "UPDATE VOUCHER_TRANS SET REFERENCE_NUMBER = NULL WHERE LEDGER_ID =?LEDGER_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.DeleteLedgerReferenceNo:
                    {
                        query = "DELETE FROM VOUCHER_REFERENCE WHERE LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.BankAccountIdFetch:
                    {
                        query = "SELECT " +
                                      "BANK_ACCOUNT_ID " +
                                    "FROM " +
                                      "MASTER_LEDGER WHERE LEDGER_ID=?LEDGER_ID ";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchMaturityDate:
                    {
                        query = "SELECT " +
                                    "MATURITY_DATE " +
                                    "FROM " +
                                    "MASTER_BANK_ACCOUNT WHERE BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchCostCenterId:
                    {
                        query = "SELECT " +
                                     "IS_COST_CENTER " +
                                   "FROM " +
                                     "MASTER_LEDGER WHERE LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerClosedDateById:
                    {
                        query = "SELECT DATE_CLOSED FROM MASTER_LEDGER WHERE LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.DeleteMapImportedProjectLedgerNotExists:
                    {
                        query = "DELETE FROM PROJECT_IMPORT_MAPPED_LEDGER WHERE MAP_LEDGER_ID NOT IN (SELECT LEDGER_ID FROM MASTER_LEDGER);";
                        break;
                    }
                case SQLCommand.LedgerBank.InsertUpdateMapImportedProjectLedger:
                    {
                        query = "INSERT INTO PROJECT_IMPORT_MAPPED_LEDGER (IMPORT_LEDGER_NAME, MAP_LEDGER_ID)\n" +
                                "VALUES (?IMPORT_LEDGER_NAME, ?MAP_LEDGER_ID)\n" +
                                "ON DUPLICATE KEY UPDATE IMPORT_LEDGER_NAME = ?IMPORT_LEDGER_NAME, MAP_LEDGER_ID = ?MAP_LEDGER_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchMappedImportedProjectLedger:
                    {
                        query = "SELECT PIM.IMPORT_LEDGER_NAME, ML.LEDGER_NAME, ML.LEDGER_ID\n" +
                                "FROM PROJECT_IMPORT_MAPPED_LEDGER PIM\n" +
                                "LEFT JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = PIM.MAP_LEDGER_ID;";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchBankLedgerClosedDateById:
                    {
                        query = "SELECT DATE_CLOSED FROM MASTER_BANK_ACCOUNT WHERE LEDGER_ID =?LEDGER_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerByGroup: //To combine Bank and Branch include with Bank Accounts
                    {
                        query = " SELECT " +
                                     " ML.BANK_ACCOUNT_ID, " +
                                     " ML.GROUP_ID, " +
                                     " MP.NATURE_ID, " +
                                     " ML.LEDGER_ID,ML.LEDGER_CODE, " +
                                     " ML.IS_COST_CENTER, " +
                                     " CONCAT(ML.LEDGER_NAME,CONCAT(' - ', MP.LEDGER_GROUP), IF(ML.LEDGER_CODE = '', '', CONCAT(' (', ML.LEDGER_CODE, ')'))," +
                                     " CASE WHEN ML.GROUP_ID = 12 THEN CONCAT(' (', MB.BANK, ' - ', MB.BRANCH, ')') ELSE '' END) AS LEDGER_NAME," +
                            // " CONCAT(ML.LEDGER_NAME,CONCAT(' - ',MP.LEDGER_GROUP),IF(ML.LEDGER_CODE='','', CONCAT(' (',ML.LEDGER_CODE,')'))) AS LEDGER_NAME," +
                            // " CONCAT(MB.BANK,CONCAT(' - ',MBA.ACCOUNT_NUMBER),CONCAT(' - ',MB.BRANCH)) AS 'BANK'," +
                                     "ML.IS_BANK_INTEREST_LEDGER, IS_BANK_FD_PENALTY_LEDGER, IS_TDS_LEDGER, IS_GST_LEDGERS, " +
                                     "IFNULL(GST_HSN_SAC_CODE,'') AS GST_HSN_SAC_CODE, GST_SERVICE_TYPE, IFNULL(TCP.GST_ID,0) AS GST_ID, ML.DATE_CLOSED" +
                                     " FROM MASTER_LEDGER  ML " +
                                     "  LEFT JOIN MASTER_BANK_ACCOUNT MBA ON ML.LEDGER_ID = MBA.LEDGER_ID" +
                                     "  LEFT JOIN MASTER_BANK MB ON MB.BANK_ID = MBA.BANK_ID" +
                                     " LEFT JOIN TDS_CREDTIORS_PROFILE TCP ON TCP.LEDGER_ID = ML.LEDGER_ID " + //On 02/12/2019
                                     " LEFT JOIN PROJECT_LEDGER PL ON PL.LEDGER_ID=ML.LEDGER_ID, " +
                                     " MASTER_LEDGER_GROUP MP  WHERE ML.GROUP_ID IN (SELECT GROUP_ID FROM MASTER_LEDGER_GROUP WHERE ( (?DIVISION_ID = 1 AND ML.GROUP_ID NOT IN (14)) or (?DIVISION_ID = 0 AND ML.GROUP_ID NOT IN (12, 13, 14)) )) " +
                                     " AND  ML.GROUP_ID=MP.GROUP_ID AND ML.STATUS=0 AND ML.LEDGER_TYPE='GN' AND PROJECT_ID=?PROJECT_ID " +
                                     " {AND (ML.DATE_CLOSED IS NULL OR ML.DATE_CLOSED >= ?DATE_CLOSED) } " +
                                     " ORDER  BY LEDGER_NAME ASC ";

                        //query = " SELECT " +
                        //            " ML.BANK_ACCOUNT_ID, " +
                        //            " ML.GROUP_ID, " +
                        //            " MP.NATURE_ID, " +
                        //            " ML.LEDGER_ID,ML.LEDGER_CODE, " +
                        //            " ML.IS_COST_CENTER, " +
                        //            " CONCAT(ML.LEDGER_NAME,CONCAT(' - ',MP.LEDGER_GROUP),IF(ML.LEDGER_CODE='','', CONCAT(' (',ML.LEDGER_CODE,')'))) AS LEDGER_NAME," +
                        //    // " CONCAT(MB.BANK,CONCAT(' - ',MBA.ACCOUNT_NUMBER),CONCAT(' - ',MB.BRANCH)) AS 'BANK'," +
                        //            "ML.IS_BANK_INTEREST_LEDGER, IS_BANK_FD_PENALTY_LEDGER, IS_TDS_LEDGER, IS_GST_LEDGERS, " +
                        //            "IFNULL(GST_HSN_SAC_CODE,'') AS GST_HSN_SAC_CODE, GST_SERVICE_TYPE, IFNULL(TCP.GST_ID,0) AS GST_ID, ML.DATE_CLOSED" +
                        //            " FROM MASTER_LEDGER  ML " +
                        //            " LEFT JOIN TDS_CREDTIORS_PROFILE TCP ON TCP.LEDGER_ID = ML.LEDGER_ID " + //On 02/12/2019
                        //            " LEFT JOIN PROJECT_LEDGER PL ON PL.LEDGER_ID=ML.LEDGER_ID, " +
                        //            " MASTER_LEDGER_GROUP MP  WHERE ML.GROUP_ID IN (SELECT GROUP_ID FROM MASTER_LEDGER_GROUP WHERE ML.GROUP_ID NOT IN(12,13,14)) " +
                        //            " AND  ML.GROUP_ID=MP.GROUP_ID AND ML.STATUS=0 AND ML.LEDGER_TYPE='GN' AND PROJECT_ID=?PROJECT_ID " +
                        //            " {AND (ML.DATE_CLOSED IS NULL OR ML.DATE_CLOSED >= ?DATE_CLOSED) } " +
                        //            " ORDER  BY LEDGER_NAME ASC ";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchAllHSNSACCode:
                    {
                        query = "SELECT GST_HSN_SAC_CODE FROM MASTER_LEDGER GROUP BY GST_HSN_SAC_CODE\n" +
                                 "UNION SELECT GST_HSN_SAC_CODE FROM gst_invoice_master_details GROUP BY GST_HSN_SAC_CODE;";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchGSTId:
                    {
                        query = "SELECT TCP.LEDGER_ID,GST_ID FROM MASTER_LEDGER ML INNER JOIN \n" +
                                " TDS_CREDTIORS_PROFILE TCP ON TCP.LEDGER_ID = ML.LEDGER_ID WHERE TCP.LEDGER_ID =?LEDGER_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchGSTLedgerId:
                    {
                        query = "SELECT LEDGER_ID FROM MASTER_LEDGER WHERE LEDGER_NAME =?LEDGER_NAME";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchBankInterestLedger:
                    {
                        query = " SELECT " +
                                     " ML.BANK_ACCOUNT_ID, " +
                                     " ML.LEDGER_ID,ML.LEDGER_CODE, " +
                                     " ML.IS_COST_CENTER, " +
                                     " CONCAT(ML.LEDGER_NAME,CONCAT(' - ',MP.LEDGER_GROUP)) AS LEDGER_NAME " +
                                " FROM " +
                                     " MASTER_LEDGER  ML LEFT JOIN PROJECT_LEDGER PL ON PL.LEDGER_ID=ML.LEDGER_ID, " +
                                     " MASTER_LEDGER_GROUP MP  WHERE ML.GROUP_ID IN (SELECT GROUP_ID FROM MASTER_LEDGER_GROUP WHERE ML.GROUP_ID NOT IN(12,13,14)) " +
                                     " AND  ML.GROUP_ID=MP.GROUP_ID " +
                                     " AND ML.STATUS=0 " +
                                     " AND ML.LEDGER_TYPE='GN' " +
                                     " AND IS_BANK_INTEREST_LEDGER=1 " +
                                     " AND PROJECT_ID=?PROJECT_ID " +
                                     " {AND (ML.DATE_CLOSED IS NULL OR ML.DATE_CLOSED >= ?DATE_CLOSED) } " +
                                     " ORDER  BY LEDGER_NAME ASC ";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchCashBankLedger:
                    {
                        query = "SELECT ML.BANK_ACCOUNT_ID,MB.BANK_ID,\n" +
                                "       MP.NATURE_ID,\n" +
                                "       ML.GROUP_ID,\n" +
                                "       ML.LEDGER_ID,\n" +
                                "       ML.LEDGER_CODE,\n" +
                                "       ML.IS_COST_CENTER,\n" +
                                "       CASE\n" +
                                "         WHEN ML.GROUP_ID = 12 THEN\n" +
                                "          CONCAT(ML.LEDGER_NAME, ' ( ', MB.BANK, ' - ', MB.BRANCH, ')')\n" +
                                "         ELSE\n" +
                                "       CONCAT(ML.LEDGER_NAME,' - ',MP.LEDGER_GROUP, IF(ML.LEDGER_CODE='','', CONCAT(' ( ', ML.LEDGER_CODE, ')')))\n" +
                                "        END AS LEDGER_NAME, IFNULL(ML.CUR_COUNTRY_ID,0) AS CUR_COUNTRY_ID, \n" +
                                "       MBA.DATE_OPENED, MBA.DATE_CLOSED, ML.IS_GST_LEDGERS\n" +
                                " FROM MASTER_LEDGER ML\n" +
                                " LEFT JOIN MASTER_LEDGER_GROUP MP ON MP.GROUP_ID = ML.GROUP_ID\n" +
                                " LEFT JOIN MASTER_BANK_ACCOUNT MBA ON ML.LEDGER_ID = MBA.LEDGER_ID\n" +
                                " LEFT JOIN MASTER_BANK MB ON MB.BANK_ID = MBA.BANK_ID\n" +
                                " LEFT JOIN PROJECT_LEDGER PL ON PL.LEDGER_ID = ML.LEDGER_ID\n" +
                                " LEFT JOIN (SELECT PLA.PROJECT_ID, PLA.LEDGER_ID, PLA.APPLICABLE_FROM, PLA.APPLICABLE_TO FROM PROJECT_LEDGER_APPLICABLE PLA " +
                                "  WHERE PLA.PROJECT_ID IN (?PROJECT_ID)) AS PLA ON PLA.PROJECT_ID = PL.PROJECT_ID AND PLA.LEDGER_ID = ML.LEDGER_ID " +
                                " WHERE ML.GROUP_ID IN (12, 13)\n" +
                                " AND ML.GROUP_ID = MP.GROUP_ID AND ML.STATUS = 0 AND ML.LEDGER_TYPE = 'GN'\n" +
                                " AND PL.PROJECT_ID = ?PROJECT_ID\n" +
                                " {AND (MBA.DATE_CLOSED IS NULL OR MBA.DATE_CLOSED >= ?DATE_CLOSED) } " + //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                                " {AND ( (PLA.APPLICABLE_FROM<=?DATE_CLOSED OR PLA.APPLICABLE_FROM  IS NULL) AND " +
                                " (PLA.APPLICABLE_TO >=?DATE_CLOSED OR PLA.APPLICABLE_TO IS NULL) )}" +  //On 27/09/2023, This property is used to skip bank ledger project based
                                " ORDER BY LEDGER_NAME ASC";      // Previous given order for LEDGER_CODE

                        break;
                    }
                case SQLCommand.LedgerBank.FetchAllCashBankLedger:
                    {
                        query = "SELECT ML.BANK_ACCOUNT_ID, ML.GROUP_ID, ML.LEDGER_ID,\n" +
                                "CASE\n" +
                                "  WHEN ML.GROUP_ID = 12 THEN\n" +
                                "   CONCAT(ML.LEDGER_NAME, ' ( ', MB.BANK, ' - ', MB.BRANCH, ')')\n" +
                                "  ELSE\n" +
                                "   CONCAT(ML.LEDGER_NAME,' - ',MP.LEDGER_GROUP, IF(ML.LEDGER_CODE='','', CONCAT(' ( ', ML.LEDGER_CODE, ')')))\n" +
                                "END AS LEDGER_NAME, MBA.DATE_OPENED, MBA.DATE_CLOSED, ML.IS_GST_LEDGERS, IFNULL(ML.CUR_COUNTRY_ID,0) AS CUR_COUNTRY_ID \n" +
                                "FROM MASTER_LEDGER ML\n" +
                                "LEFT JOIN MASTER_BANK_ACCOUNT MBA ON ML.LEDGER_ID = MBA.LEDGER_ID\n" +
                                "LEFT JOIN MASTER_BANK MB ON MB.BANK_ID = MBA.BANK_ID\n" +
                                "LEFT JOIN PROJECT_LEDGER PL ON PL.LEDGER_ID = ML.LEDGER_ID, MASTER_LEDGER_GROUP MP\n" +
                                " WHERE ML.GROUP_ID IN (12, 13) AND ML.GROUP_ID = MP.GROUP_ID\n" +
                                " {AND PROJECT_ID = ?PROJECT_ID} \n" +
                                " {AND (MBA.DATE_CLOSED IS NULL OR MBA.DATE_CLOSED >= ?DATE_CLOSED) } " + //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                                " ORDER BY LEDGER_NAME ASC";

                        break;
                    }
                case SQLCommand.LedgerBank.FetchAllCashBankLedgerByProject:
                    {
                        query = "SELECT ML.GROUP_ID, ML.LEDGER_ID, ML.BANK_ACCOUNT_ID, MB.BANK_ID, ML.LEDGER_CODE,\n" +
                                "  CASE\n" +
                                "   WHEN ML.GROUP_ID = 12 THEN\n" +
                                "    CONCAT(ML.LEDGER_NAME, ' ( ', MB.BANK, ' - ', MB.BRANCH, ')')\n" +
                                "   ELSE ML.LEDGER_NAME END AS LEDGER_NAME, " +
                                "  IFNULL(ML.CUR_COUNTRY_ID,0) AS CUR_COUNTRY_ID, IFNULL(MC.CURRENCY_NAME, '') AS CURRENCY_NAME, MBA.DATE_OPENED, MBA.DATE_CLOSED\n" +
                                " FROM MASTER_LEDGER ML\n" +
                                " LEFT JOIN MASTER_BANK_ACCOUNT MBA ON ML.LEDGER_ID = MBA.LEDGER_ID\n" +
                                " LEFT JOIN MASTER_BANK MB ON MB.BANK_ID = MBA.BANK_ID\n" +
                                " LEFT JOIN PROJECT_LEDGER PL ON PL.LEDGER_ID = ML.LEDGER_ID\n" +
                                " LEFT JOIN MASTER_COUNTRY MC ON MC.COUNTRY_ID = ML.CUR_COUNTRY_ID\n" +
                                " LEFT JOIN (SELECT PLA.PROJECT_ID, PLA.LEDGER_ID, PLA.APPLICABLE_FROM, PLA.APPLICABLE_TO FROM PROJECT_LEDGER_APPLICABLE PLA " +
                                "  WHERE PLA.PROJECT_ID IN (?PROJECT_ID)) AS PLA ON PLA.PROJECT_ID = PL.PROJECT_ID AND PLA.LEDGER_ID = ML.LEDGER_ID " +
                                " WHERE ML.GROUP_ID IN (12, 13) AND PL.PROJECT_ID IN (?PROJECT_ID) AND ML.STATUS = 0\n" +
                                " {AND (MBA.DATE_CLOSED IS NULL OR MBA.DATE_CLOSED >= ?DATE_CLOSED) } " +
                                " {AND ( (PLA.APPLICABLE_FROM<=?DATE_CLOSED OR PLA.APPLICABLE_FROM  IS NULL) AND " +
                                " (PLA.APPLICABLE_TO >=?DATE_CLOSED OR PLA.APPLICABLE_TO IS NULL) )}" +
                                " GROUP BY ML.LEDGER_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.IsBankLedger:
                    {
                        query = "SELECT " +
                                    "BANK_ACCOUNT_ID " +
                                "FROM " +
                                    "MASTER_LEDGER  " +
                                "WHERE LEDGER_TYPE ='GN' AND LEDGER_SUB_TYPE IN('BK','FD') AND LEDGER_ID=?LEDGER_ID ";
                        break;
                    }
                case SQLCommand.LedgerBank.IsCashLedgerExists:
                    {
                        query = "SELECT LEDGER_ID FROM MASTER_LEDGER WHERE LEDGER_ID=1 AND GROUP_ID=13";
                        break;
                    }
                case SQLCommand.LedgerBank.IsBudgetedLedger:
                    {
                        query = "SELECT LEDGER_ID FROM BUDGET_LEDGER WHERE LEDGER_ID = ?LEDGER_ID;";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchCashBankFDLedger:
                    {
                        query = "SELECT " +
                                    " ML.BANK_ACCOUNT_ID,10 AS STATUS, " +
                                    "ML.LEDGER_ID, " +
                                    "ML.GROUP_ID, " +
                                    "ML.LEDGER_CODE, " +
                                    "ML.LEDGER_NAME " +
                                "FROM " +
                                    "MASTER_LEDGER ML LEFT JOIN PROJECT_LEDGER PL ON PL.LEDGER_ID=ML.LEDGER_ID  " +
                                "WHERE ML.GROUP_ID =13 AND ML.STATUS=0   AND PL.PROJECT_ID=?PROJECT_ID  " +
                                "UNION " +
                                "SELECT " +
                                    " ML.BANK_ACCOUNT_ID, " +
                                    " CASE WHEN GROUP_ID =12 THEN 10 ELSE FR.STATUS END AS STATUS, " +
                                    "ML.LEDGER_ID, " +
                                    "ML.GROUP_ID, " +
                                    "ML.LEDGER_CODE, " +
                                "CASE\n" +
                                "        WHEN GROUP_ID = 12 THEN\n" +
                                "         CONCAT(CONCAT(LEDGER_CODE),\n" +
                                "                CONCAT(' - ', LEDGER_NAME),\n" +
                                "                ' (BankAccount)')\n" +
                                "        ELSE\n" +
                                "         CASE\n" +
                                "           WHEN GROUP_ID = 14 THEN\n" +
                                "            CONCAT(CONCAT(LEDGER_CODE),\n" +
                                "                   CONCAT(' - ', LEDGER_NAME),\n" +
                                "                   ' (Fixed Deposit)')\n" +
                                "         END\n" +
                                "      END AS LEDGER_NAME\n" +
                                "FROM " +
                                    "MASTER_LEDGER ML LEFT JOIN PROJECT_LEDGER PL ON PL.LEDGER_ID=ML.LEDGER_ID , MASTER_BANK_ACCOUNT BA LEFT JOIN FD_REGISTERS FR " +
                                "ON BA.BANK_ACCOUNT_ID = FR.BANK_ACCOUNT_ID " +
                                "WHERE " +
                                  "GROUP_ID IN(12,14)  " +
                                  "AND ML.BANK_ACCOUNT_ID=BA.BANK_ACCOUNT_ID AND ML.STATUS=0 AND PL.PROJECT_ID=?PROJECT_ID ";

                        break;
                    }
                case SQLCommand.LedgerBank.FetchFDLedgers:
                    {
                        //query = "SELECT ML.BANK_ACCOUNT_ID,\n" +
                        //"       ML.LEDGER_ID,\n" +
                        //"       ML.GROUP_ID,\n" +
                        //"       ML.LEDGER_CODE,\n" +
                        //"       CASE\n" +
                        //"         WHEN GROUP_ID = 14 THEN\n" +
                        //"          CONCAT(CONCAT(LEDGER_CODE),\n" +
                        //"                 CONCAT(' - ', LEDGER_NAME),\n" +
                        //"                 ' (Fixed Deposit)')\n" +
                        //"       END AS LEDGER_NAME\n" +
                        //"  FROM MASTER_LEDGER ML\n" +
                        //"  LEFT JOIN PROJECT_LEDGER PL\n" +
                        //"    ON PL.LEDGER_ID = ML.LEDGER_ID, MASTER_BANK_ACCOUNT BA\n" +
                        //"  LEFT JOIN FD_REGISTERS FR\n" +
                        //"    ON BA.ACCOUNT_NUMBER = FR.ACCOUNT_NO\n" +
                        //" WHERE GROUP_ID IN (14)\n" +
                        //"   AND ML.BANK_ACCOUNT_ID = BA.BANK_ACCOUNT_ID\n" +
                        //"   AND ML.STATUS = 0 \n" +
                        //"   AND PL.PROJECT_ID = ?PROJECT_ID\n" +
                        //" GROUP BY BANK_ACCOUNT_ID";
                        query = "SELECT T.LEDGER_ID,\n" +
                                "       T.LEDGER_CODE,\n" +
                                "       T.LEDGER_NAME,\n" +
                                "       CASE\n" +
                                "         WHEN T.LEDGER_SUB_TYPE = 'FD' THEN\n" +
                                "          'Fixed Deposit'\n" +
                                "       END AS 'GROUP'\n" +
                                "  FROM MASTER_LEDGER_GROUP AS MLG\n" +
                                "  JOIN (SELECT ML.LEDGER_ID,\n" +
                                "               ML.LEDGER_CODE,\n" +
                                "               ML.LEDGER_NAME,\n" +
                                "               ML.LEDGER_SUB_TYPE\n" +
                                "          FROM MASTER_LEDGER AS ML\n" +
                                "         WHERE GROUP_ID = 14) AS T\n" +
                                "    ON MLG.GROUP_ID = 14";

                        break;
                    }
                case SQLCommand.LedgerBank.FixedDepositByLedger:
                    {
                        query = "SELECT ML.BANK_ACCOUNT_ID,\n" +
                        "       ML.LEDGER_ID,\n" +
                        "       ML.GROUP_ID,\n" +
                        "       ML.LEDGER_CODE,\n" +
                        "       CASE\n" +
                        "         WHEN GROUP_ID = 14 THEN\n" +
                        "          CONCAT(CONCAT(LEDGER_CODE),\n" +
                        "                 CONCAT(' - ', LEDGER_NAME),\n" +
                        "                 ' (Fixed Deposit)')\n" +
                        "       END AS LEDGER_NAME\n" +
                        "  FROM MASTER_LEDGER ML\n" +
                        "  LEFT JOIN PROJECT_LEDGER PL\n" +
                        "    ON PL.LEDGER_ID = ML.LEDGER_ID, MASTER_BANK_ACCOUNT BA\n" +
                        "  LEFT JOIN FD_REGISTERS FR\n" +
                        "    ON BA.ACCOUNT_NUMBER = FR.ACCOUNT_NO\n" +
                        " WHERE GROUP_ID IN (14)\n" +
                        "   AND ML.BANK_ACCOUNT_ID = BA.BANK_ACCOUNT_ID\n" +
                        "   AND ML.STATUS = 0 \n" +
                        "   AND PL.PROJECT_ID = ?PROJECT_ID AND ML.BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID\n" +
                        " GROUP BY BANK_ACCOUNT_ID";

                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerOpeningBalanceNotMappedWithProject:
                    {
                        query = @"SELECT LB.PROJECT_ID, LB.LEDGER_ID, LB.AMOUNT FROM LEDGER_BALANCE LB
                                    LEFT JOIN PROJECT_LEDGER PL ON PL.PROJECT_ID = LB.PROJECT_ID AND PL.LEDGER_ID = LB.LEDGER_ID
                                    WHERE LB.TRANS_FLAG = 'OP' AND PL.LEDGER_ID IS NULL;";
                        break;
                    }
                case SQLCommand.LedgerBank.ClearInvalidLedgerBalanceData:
                    {
                        query = @"DELETE FROM LEDGER_BALANCE WHERE PROJECT_ID NOT IN (SELECT PROJECT_ID FROM MASTER_PROJECT)
                                            OR LEDGER_ID NOT IN (SELECT LEDGER_ID FROM MASTER_LEDGER);
                                  DELETE FROM LEDGER_BALANCE WHERE PROJECT_ID = 0 OR LEDGER_ID = 0;";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchVoucherLedgersNotMappedWithProject:
                    {
                        query = @"SELECT VMT.PROJECT_ID, VMT.LEDGER_ID  FROM
                                    (SELECT VM.PROJECT_ID, VT.LEDGER_ID FROM VOUCHER_TRANS VT
                                     INNER JOIN VOUCHER_MASTER_TRANS VM ON VM.VOUCHER_ID = VT.VOUCHER_ID WHERE VM.STATUS = 1 GROUP BY VM.PROJECT_ID, VT.LEDGER_ID) AS VMT
                                  LEFT JOIN PROJECT_LEDGER PL ON PL.PROJECT_ID = VMT.PROJECT_ID AND PL.LEDGER_ID = VMT.LEDGER_ID
                                  WHERE PL.LEDGER_ID IS NULL;";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchProjectBudgetLedgersNotMappedWithProject:
                    {
                        query = @"SELECT PBL.PROJECT_ID, PBL.LEDGER_ID
                                    FROM PROJECT_BUDGET_LEDGER PBL
                                    LEFT JOIN PROJECT_LEDGER PL ON PL.PROJECT_ID = PBL.PROJECT_ID AND PL.LEDGER_ID = PBL.LEDGER_ID
                                    WHERE PL.LEDGER_ID IS NULL;";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchBudgetLedgersNotMappedWithProjectBudgetLedger:
                    {
                        query = @"SELECT BM.BUDGET_ID, BL.LEDGER_ID, PBL.*
                                    FROM BUDGET_LEDGER BL
                                    INNER JOIN BUDGET_MASTER BM ON BM.BUDGET_ID = BL.BUDGET_ID
                                    INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID
                                    LEFT JOIN PROJECT_BUDGET_LEDGER PBL ON PBL.PROJECT_ID = BP.PROJECT_ID AND PBL.LEDGER_ID = BL.LEDGER_ID
                                    WHERE PBL.LEDGER_ID IS NULL GROUP BY BM.BUDGET_ID, BL.LEDGER_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchFDLedgersMismatchingOpeningBalance:
                    {
                        query = @"SELECT FD.PROJECT_ID, FD.LEDGER_ID, FD.OP_AMOUNT FROM
                                 (SELECT PROJECT_ID, LEDGER_ID, SUM(AMOUNT) AS OP_AMOUNT
                                  FROM FD_ACCOUNT FA WHERE TRANS_TYPE = 'OP' AND STATUS = 1 GROUP BY PROJECT_ID, LEDGER_ID) AS FD
                                LEFT JOIN LEDGER_BALANCE LB ON LB.PROJECT_ID = FD.PROJECT_ID AND LB.LEDGER_ID = FD.LEDGER_ID AND LB.AMOUNT <> FD.OP_AMOUNT
                                WHERE LB.TRANS_FLAG = 'OP';";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchFDLedgersMismatchingOpeningBalanceFC:
                    {
                        query = @"SELECT FD.PROJECT_ID, FD.LEDGER_ID, OP_AMOUNT_FC, OP_AMOUNT_LC FROM
                                 (SELECT PROJECT_ID, LEDGER_ID, SUM(AMOUNT) AS OP_AMOUNT_FC, SUM(AMOUNT * EXCHANGE_RATE) AS OP_AMOUNT_LC
                                    FROM FD_ACCOUNT FA WHERE TRANS_TYPE = 'OP' AND STATUS = 1 GROUP BY PROJECT_ID, LEDGER_ID) AS FD
                                LEFT JOIN LEDGER_BALANCE LB ON LB.PROJECT_ID = FD.PROJECT_ID AND LB.LEDGER_ID = FD.LEDGER_ID AND 
                                    (LB.AMOUNT_FC <> FD.OP_AMOUNT_FC OR LB.AMOUNT <> FD.OP_AMOUNT_LC) 
                                WHERE LB.TRANS_FLAG = 'OP';";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchMorethanOneLedgerBalanceDate:
                    {
                        query = @"SELECT LB.BALANCE_DATE, COUNT(*)
                                    FROM LEDGER_BALANCE LB WHERE LB.TRANS_FLAG = 'OP' GROUP BY LB.BALANCE_DATE;";
                        break;
                    }
                case SQLCommand.LedgerBank.BankAccountAdd:
                    {

                        query = "INSERT INTO MASTER_BANK_ACCOUNT ( " +
                                    "ACCOUNT_CODE, " +
                                    "ACCOUNT_NUMBER, " +
                                    "ACCOUNT_HOLDER_NAME, " +
                                    "ACCOUNT_TYPE_ID, " +
                                     "TYPE_ID, " +
                                    "BANK_ID, " +
                                    "DATE_OPENED,  " +
                                    "DATE_CLOSED, " +
                                    "OPERATED_BY, " +
                                    "PERIOD_YEAR,  " +
                                    "PERIOD_MTH,   " +
                                    "PERIOD_DAY,   " +
                                    "INTEREST_RATE, " +
                                    "AMOUNT, " +
                                    "MATURITY_DATE,  " +
                                    "NOTES,LEDGER_ID,IS_FCRA_ACCOUNT) " +
                                    "VALUES( " +
                                    "?ACCOUNT_CODE, " +
                                    "?ACCOUNT_NUMBER, " +
                                    "?ACCOUNT_HOLDER_NAME, " +
                                    "?ACCOUNT_TYPE_ID, " +
                                    "?TYPE_ID, " +
                                    "?BANK_ID, " +
                                    "?DATE_OPENED,  " +
                                    "?DATE_CLOSED, " +
                                    "?OPERATED_BY, " +
                                    "?PERIOD_YEAR,  " +
                                    "?PERIOD_MTH,   " +
                                    "?PERIOD_DAY,   " +
                                    "?INTEREST_RATE, " +
                                    "?AMOUNT, " +
                                    "?MATURITY_DATE,  " +
                                    "?NOTES,?LEDGER_ID,?IS_FCRA_ACCOUNT) ";
                        break;
                    }
                case SQLCommand.LedgerBank.BankAccountUpdate:
                    {
                        query = "UPDATE MASTER_BANK_ACCOUNT SET " +
                                    "ACCOUNT_CODE = ?ACCOUNT_CODE, " +
                                    "ACCOUNT_NUMBER = ?ACCOUNT_NUMBER, " +
                                    "ACCOUNT_HOLDER_NAME = ?ACCOUNT_HOLDER_NAME, " +
                                    "ACCOUNT_TYPE_ID = ?ACCOUNT_TYPE_ID, " +
                                    "TYPE_ID = ?TYPE_ID, " +
                                    "BANK_ID = ?BANK_ID," +
                                    "DATE_OPENED = ?DATE_OPENED, " +
                                    "DATE_CLOSED = ?DATE_CLOSED, " +
                                    "OPERATED_BY=?OPERATED_BY, " +
                            //  "DATE_CLOSED = NULL, " +
                                    "PERIOD_YEAR = ?PERIOD_YEAR,  " +
                                    "PERIOD_MTH = ?PERIOD_MTH,   " +
                                    "PERIOD_DAY = ?PERIOD_DAY, " +
                                    "INTEREST_RATE = ?INTEREST_RATE, " +
                                    "AMOUNT = ?AMOUNT, " +
                                    "MATURITY_DATE = ?MATURITY_DATE,  " +
                            //"MATURITY_DATE = NULL, " +
                                    "NOTES = ?NOTES, " +
                            //"LEDGER_ID = ?LEDGER_ID " +
                                    "IS_FCRA_ACCOUNT = ?IS_FCRA_ACCOUNT" +
                                    " WHERE BANK_ACCOUNT_ID = ?BANK_ACCOUNT_ID";
                        break;
                    }

                case SQLCommand.LedgerBank.BankAccountFetch:
                    {
                        query = "SELECT " +
                                 "BANK_ACCOUNT_ID, " +
                                    "ACCOUNT_CODE, " +
                                    "ACCOUNT_NUMBER, " +
                                    "ACCOUNT_HOLDER_NAME, " +
                                    "ACCOUNT_TYPE_ID, " +
                                    "TYPE_ID, " +
                                    "BANK_ID, " +
                                    "DATE_OPENED,  " +
                                    "OPERATED_BY, " +
                                    "PERIOD_YEAR,  " +
                                    "PERIOD_MTH,   " +
                                    "PERIOD_DAY, " +
                                    "INTEREST_RATE, " +
                                    "MATURITY_DATE,  " +
                                    "DATE_CLOSED,NOTES,AMOUNT,IS_FCRA_ACCOUNT " +
                            "FROM " +
                                "MASTER_BANK_ACCOUNT " +
                                " WHERE BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID ";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerNature:
                    {
                        query = "SELECT MG.NATURE_ID " +
                                    "FROM MASTER_LEDGER_GROUP MG " +
                                    "LEFT JOIN MASTER_LEDGER ML " +
                                   "ON MG.GROUP_ID=ML.GROUP_ID " +
                                   "WHERE ML.LEDGER_ID=?LEDGER_ID ";
                        break;
                    }
                case SQLCommand.LedgerBank.SetLedgerSource:
                    {
                        //LEDGER_GROUP

                        query = "SELECT MLG.GROUP_ID, CONCAT(MLG.LEDGER_GROUP,CONCAT(' - ',MLG.GROUP_CODE)) AS 'GROUP'  " +
                                "FROM MASTER_LEDGER_GROUP AS MLG " +
                                "LEFT JOIN MASTER_LEDGER AS ML ON MLG.GROUP_ID=ML.GROUP_ID " +
                                "GROUP BY MLG.GROUP_ID ORDER BY MLG.LEDGER_GROUP ASC";
                        break;
                    }
                case SQLCommand.LedgerBank.SetLedgerDetailSource:
                    {
                        // LEDGER_NAME

                        query = "SELECT ML.LEDGER_ID, " +
                                "IF(ML.LEDGER_CODE IS NULL OR ML.LEDGER_CODE ='',ML.LEDGER_NAME,CONCAT(ML.LEDGER_NAME,' - ',ML.LEDGER_CODE)) AS LEDGER ,MLG.GROUP_ID " +
                            // " CONCAT(ML.LEDGER_NAME,CONCAT(' - ',ML.LEDGER_CODE)) AS 'LEDGER',MLG.GROUP_ID FROM " +
                                "FROM MASTER_LEDGER_GROUP AS MLG INNER JOIN MASTER_LEDGER AS ML ON MLG.GROUP_ID = ML.GROUP_ID " +
                                "WHERE 1=1 {AND (ML.DATE_CLOSED IS NULL OR ML.DATE_CLOSED >= ?DATE_CLOSED) } " + //On 21/10/2021, This property is used to skip Ledgers which is closed on or equal to this date
                                "ORDER BY ML.LEDGER_NAME ASC";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerGroupbyLedgerId:
                    {
                        query = "SELECT LG.GROUP_ID\n" +
                          "  FROM MASTER_LEDGER ML\n" +
                          " INNER JOIN MASTER_LEDGER_GROUP LG\n" +
                          "    ON ML.GROUP_ID = LG.GROUP_ID\n" +
                          " WHERE ML.LEDGER_ID = ?LEDGER_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchBankAccountById:
                    {
                        query = "SELECT BANK_ACCOUNT_ID FROM MASTER_LEDGER WHERE LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateFDBankAccount:
                    {
                        query = "UPDATE MASTER_BANK_ACCOUNT SET " +
                                "INTEREST_RATE=?INTEREST_RATE, " +
                                "MATURITY_DATE=?MATURITY_DATE, " +
                                "AMOUNT=?AMOUNT " +
                                "WHERE BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerByLedgerGroup:
                    {
                        query = "SELECT" +
                               " ML.LEDGER_ID,ML.LEDGER_CODE, " +
                                "ML.LEDGER_NAME " +
                                "FROM " +
                                "MASTER_LEDGER ML,PROJECT_LEDGER PL " +
                                "WHERE ML.LEDGER_ID=PL.LEDGER_ID " +
                                "AND PROJECT_ID=?PROJECT_ID AND LEDGER_SUB_TYPE='IK'";
                        break;
                    }
                case SQLCommand.LedgerBank.CheckProjectExist:
                    {
                        query = "SELECT COUNT(*) FROM PROJECT_LEDGER WHERE PROJECT_ID=?PROJECT_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerCodes:
                    {
                        query = "SELECT LEDGER_CODE AS 'USED_CODE',LEDGER_NAME AS 'NAME' FROM MASTER_LEDGER {WHERE LEDGER_ID!=?LEDGER_ID} ORDER BY LEDGER_ID DESC;";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgersByLedgercode:
                    {
                        query = "SELECT LEDGER_CODE AS 'EXIST_CODE'\n" +
                                "  FROM MASTER_LEDGER WHERE LEDGER_CODE=?LEDGER_CODE;";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchMaxLedgerID:
                    {
                        query = "SELECT MAX(LEDGER_ID) AS LEDGER_ID FROM MASTER_LEDGER ";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchBankAccountCodes:
                    {
                        query = "SELECT MA.ACCOUNT_CODE AS 'USED_CODE',CONCAT(CONCAT(MA.ACCOUNT_NUMBER,' - '),CONCAT(MB.BANK,' - '),MB.BRANCH) AS 'NAME'\n" +
                            "  FROM MASTER_BANK_ACCOUNT MA\n" +
                                "  LEFT JOIN MASTER_BANK MB\n" +
                                "    ON MA.BANK_ID = MB.BANK_ID\n" +
                                " WHERE MA.ACCOUNT_TYPE_ID = 1\n" +
                                " ORDER BY MA.BANK_ACCOUNT_ID DESc";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchBankAccountsByCode:
                    {
                        query = " SELECT\n" +
                                " ACCOUNT_CODE AS 'EXIST_CODE'\n" +
                                "  FROM MASTER_BANK_ACCOUNT\n" +
                                " WHERE ACCOUNT_TYPE_ID = 1\n" +
                                "   AND ACCOUNT_CODE =?ACCOUNT_CODE ;";

                        break;
                    }
                case SQLCommand.LedgerBank.FetchFDLedgerById:
                    {
                        query = "SELECT ML.LEDGER_ID, ML.LEDGER_CODE, ML.LEDGER_NAME, PL.PROJECT_ID\n" +
                                "  FROM MASTER_LEDGER AS ML\n" +
                                " INNER JOIN PROJECT_LEDGER AS PL\n" +
                                "    ON ML.LEDGER_ID = PL.LEDGER_ID\n" +
                                " WHERE ML.LEDGER_ID =?LEDGER_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.FDLedgerUpdate:
                    {
                        query = "UPDATE MASTER_LEDGER AS ML\n" +
                                "   SET ML.LEDGER_CODE = ?LEDGER_CODE, LEDGER_NAME = ?LEDGER_NAME\n" +
                                " WHERE ML.LEDGER_ID =?LEDGERID";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchAccessFlag:
                    {
                        query = "SELECT ACCESS_FLAG FROM MASTER_LEDGER WHERE LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.IsLedgerNameExists:
                    {
                        query = "SELECT COUNT(*) FROM MASTER_LEDGER WHERE LEDGER_NAME=?LEDGER_NAME";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchDefaultLedgers:
                    {
                        query = "SELECT LEDGER_ID ,LEDGER_NAME FROM MASTER_LEDGER WHERE LEDGER_ID IN(1,2,3) ";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerId:
                    {
                        query = "SELECT LEDGER_ID FROM MASTER_LEDGER WHERE LEDGER_NAME=?LEDGER_NAME";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchGroupIdByLedgerName:
                    {
                        query = "SELECT GROUP_ID FROM MASTER_LEDGER WHERE LEDGER_NAME=?LEDGER_NAME";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchHOLedgerId:
                    {
                        query = "SELECT HEADOFFICE_LEDGER_ID FROM MASTER_HEADOFFICE_LEDGER WHERE LEDGER_NAME=?LEDGER_NAME";
                        break;
                    }
                case SQLCommand.LedgerBank.IsLedgerCodeExists:
                    {
                        query = "SELECT COUNT(*) FROM MASTER_LEDGER WHERE LEDGER_CODE=?LEDGER_CODE";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerByIncludeCostCentre:
                    {
                        query = "SELECT LEDGER_ID,\n" +
                            "       MLG.LEDGER_GROUP,\n" +
                                "       LEDGER_CODE,\n" +
                                "       LEDGER_NAME,\n" +
                                "       IS_COST_CENTER AS SELECT_TMP\n" +
                                "  FROM MASTER_LEDGER ML\n" +
                                " INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                                "    ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                                "  WHERE MLG.GROUP_ID  NOT IN (12,13,14) AND STATUS=0 \n" +
                                " ORDER BY LEDGER_NAME;";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchHighValuePaymentbyLedgers:
                    {
                        query = "SELECT LEDGER_ID,\n" +
                               "       MLG.LEDGER_GROUP,\n" +
                               "       LEDGER_CODE,\n" +
                               "       LEDGER_NAME,\n" +
                               "       IS_HIGHVALUEPAYMENT_LEDGER AS SELECT_TMP\n" +
                               "  FROM MASTER_LEDGER ML\n" +
                               " INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                               "    ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                               "  WHERE MLG.GROUP_ID  NOT IN (12,13,14) AND STATUS=0 \n" +
                               " ORDER BY LEDGER_NAME;";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLocalDonationLedgers:
                    {
                        query = "SELECT LEDGER_ID,\n" +
                               "       MLG.LEDGER_GROUP,\n" +
                               "       LEDGER_CODE,\n" +
                               "       LEDGER_NAME,\n" +
                               "       IS_LOCAL_DONATION AS SELECT_TMP\n" +
                               "  FROM MASTER_LEDGER ML\n" +
                               " INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                               "    ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                               "  WHERE MLG.GROUP_ID  NOT IN (12,13,14) AND STATUS=0 \n" +
                               " ORDER BY LEDGER_NAME;";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerByIncludeGST:
                    {
                        query = "SELECT ML.LEDGER_ID,\n" +
                                "       MLG.LEDGER_GROUP,\n" +
                                "       LEDGER_CODE,\n" +
                                "       LEDGER_NAME,\n" +
                                "       IS_GST_LEDGERS AS SELECT_TMP,\n" +
                                "       GST_SERVICE_TYPE, IFNULL(TCP.GST_ID,0) AS GST_ID\n" +
                                "  FROM MASTER_LEDGER ML\n" +
                                " INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                                "    ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                                " LEFT JOIN TDS_CREDTIORS_PROFILE TCP ON TCP.LEDGER_ID = ML.LEDGER_ID\n" +
                                "  WHERE MLG.GROUP_ID  NOT IN (12,13,14) AND STATUS=0 \n" +
                                " ORDER BY LEDGER_NAME;";
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateLedgerOptions:
                    {
                        query = "UPDATE MASTER_LEDGER SET IS_COST_CENTER=1 WHERE LEDGER_ID IN (?LEDGER_ID)";
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateLedgerOptionsHighValuePaymentByLedger:
                    {
                        query = "UPDATE MASTER_LEDGER SET IS_HIGHVALUEPAYMENT_LEDGER=1 WHERE LEDGER_ID IN (?LEDGER_ID)";
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateLedgerOptionsLocalDonationByLedger:
                    {
                        query = "UPDATE MASTER_LEDGER SET IS_LOCAL_DONATION=1 WHERE LEDGER_ID IN (?LEDGER_ID)";
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateLedgerOptionsCostcentre:
                    {
                        query = @"UPDATE MASTER_LEDGER SET IS_COST_CENTER=0
                                WHERE LEDGER_ID NOT IN (SELECT VT.LEDGER_ID FROM VOUCHER_TRANS VT
                                    INNER JOIN VOUCHER_CC_TRANS VCT ON VCT.VOUCHER_ID = VT.VOUCHER_ID AND VCT.LEDGER_ID = VT.LEDGER_ID 
                                    INNER JOIN VOUCHER_MASTER_TRANS VM ON VM.VOUCHER_ID = VT.VOUCHER_ID AND VM.STATUS=1)";
                        break;
                    }
                case SQLCommand.LedgerBank.UpdaterLedgerOptionHighValuePayments:
                    {
                        query = "UPDATE MASTER_LEDGER SET IS_HIGHVALUEPAYMENT_LEDGER=0";
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateLedgerOptionLocalDonations:
                    {
                        query = "UPDATE MASTER_LEDGER SET IS_LOCAL_DONATION=0";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerByIncludeBankInterest:
                    {
                        query = "SELECT LEDGER_ID,\n" +
                                "       MLG.LEDGER_GROUP,\n" +
                                "       LEDGER_CODE,\n" +
                                "       LEDGER_NAME,\n" +
                                "       IS_BANK_INTEREST_LEDGER AS SELECT_TMP\n" +
                                "  FROM MASTER_LEDGER ML\n" +
                                " INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                                "    ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                                "  WHERE MLG.GROUP_ID  NOT IN (12,13,14) AND STATUS=0 \n" +
                                " ORDER BY LEDGER_NAME;";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerByIncludeBankFDPenaltyLedger:
                    {
                        query = "SELECT LEDGER_ID, MLG.LEDGER_GROUP, LEDGER_CODE, LEDGER_NAME,\n" +
                                " IS_BANK_FD_PENALTY_LEDGER AS SELECT_TMP\n" +
                                " FROM MASTER_LEDGER ML\n" +
                                " INNER JOIN MASTER_LEDGER_GROUP MLG ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                                " WHERE MLG.GROUP_ID  NOT IN (12,13,14) AND STATUS=0 \n" +
                                " ORDER BY LEDGER_NAME;";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerByIncludeBankSBInterest:
                    {
                        query = "SELECT LEDGER_ID,\n" +
                                "       MLG.LEDGER_GROUP,\n" +
                                "       LEDGER_CODE,\n" +
                                "       LEDGER_NAME,\n" +
                                "       IS_BANK_SB_INTEREST_LEDGER AS SELECT_TMP\n" +
                                "  FROM MASTER_LEDGER ML\n" +
                                " INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                                "    ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                                "  WHERE MLG.GROUP_ID  NOT IN (12,13,14) AND STATUS=0 \n" +
                                " ORDER BY LEDGER_NAME;";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerByIncludeBankCommission:
                    {
                        query = "SELECT LEDGER_ID,\n" +
                                "       MLG.LEDGER_GROUP,\n" +
                                "       LEDGER_CODE,\n" +
                                "       LEDGER_NAME,\n" +
                                "       IS_BANK_COMMISSION_LEDGER AS SELECT_TMP\n" +
                                "  FROM MASTER_LEDGER ML\n" +
                                " INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                                "    ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                                "  WHERE MLG.GROUP_ID  NOT IN (12,13,14) AND STATUS=0 \n" +
                                " ORDER BY LEDGER_NAME;";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerByIncludeInkindLedger:
                    {
                        query = "SELECT LEDGER_ID,\n" +
                                "       MLG.LEDGER_GROUP,\n" +
                                "       LEDGER_CODE,\n" +
                                "       LEDGER_NAME,\n" +
                                "       IS_INKIND_LEDGER AS SELECT_TMP\n" +
                                "  FROM MASTER_LEDGER ML\n" +
                                " INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                                "    ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                                "  WHERE MLG.GROUP_ID  NOT IN (12,13,14) AND STATUS=0 \n" +
                                " ORDER BY LEDGER_NAME;";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgersByEnableAssetGainLedger:
                    {
                        query = "SELECT LEDGER_ID,\n" +
                                "       MLG.LEDGER_GROUP,\n" +
                                "       LEDGER_CODE,\n" +
                                "       LEDGER_NAME,\n" +
                                "       IS_ASSET_GAIN_LEDGER AS SELECT_TMP\n" +
                                "  FROM MASTER_LEDGER ML\n" +
                                " INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                                "    ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                                " WHERE MLG.GROUP_ID NOT IN (12, 13, 14) AND MLG.NATURE_ID = 1 \n" +
                                "   AND STATUS = 0\n" +
                                " ORDER BY LEDGER_NAME;";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgersByEnableAssetLossLedger:
                    {
                        query = "SELECT LEDGER_ID,\n" +
                                      "       MLG.LEDGER_GROUP,\n" +
                                      "       LEDGER_CODE,\n" +
                                      "       LEDGER_NAME,\n" +
                                      "       IS_ASSET_LOSS_LEDGER AS SELECT_TMP\n" +
                                      "  FROM MASTER_LEDGER ML\n" +
                                      " INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                                      "    ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                                      " WHERE MLG.GROUP_ID NOT IN (12, 13, 14) AND MLG.NATURE_ID = 2 \n" +
                                      "   AND STATUS = 0\n" +
                                      " ORDER BY LEDGER_NAME;";
                        break;
                    }

                case SQLCommand.LedgerBank.FetchLedgersByEnableAssetDisposalLedger:
                    {
                        query = "SELECT LEDGER_ID,\n" +
                                "       MLG.LEDGER_GROUP,\n" +
                                "       LEDGER_CODE,\n" +
                                "       LEDGER_NAME,\n" +
                                "       IS_DISPOSAL_LEDGER AS SELECT_TMP\n" +
                                "  FROM MASTER_LEDGER ML\n" +
                                " INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                                "    ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                                " WHERE MLG.GROUP_ID NOT IN (12, 13, 14)\n" +
                                "   AND MLG.NATURE_ID = 2\n" +
                                "   AND STATUS = 0\n" +
                                " ORDER BY LEDGER_NAME;";
                        break;
                    }

                case SQLCommand.LedgerBank.FetchLedgersByEnableSubsidyLedger:
                    {
                        query = "SELECT LEDGER_ID,\n" +
                                "       MLG.LEDGER_GROUP,\n" +
                                "       LEDGER_CODE,\n" +
                                "       LEDGER_NAME,\n" +
                                "       IS_SUBSIDY_LEDGER AS SELECT_TMP\n" +
                                "  FROM MASTER_LEDGER ML\n" +
                                " INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                                "    ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                                " WHERE MLG.GROUP_ID NOT IN (12, 13, 14)\n" +
                                "   AND MLG.NATURE_ID = 2\n" +
                                "   AND STATUS = 0\n" +
                                " ORDER BY LEDGER_NAME;";
                        break;
                    }

                case SQLCommand.LedgerBank.UpdateLedgerOptionsInkindLedgersetone:
                    {
                        query = "UPDATE MASTER_LEDGER SET IS_INKIND_LEDGER=1 WHERE LEDGER_ID IN (?LEDGER_ID)";
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateLedgerOptionsGainLedgerssetone:
                    {
                        query = "UPDATE MASTER_LEDGER SET IS_ASSET_GAIN_LEDGER = 1 WHERE LEDGER_ID IN (?LEDGER_ID)";
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateLedgerOptionsLossLedgerssetone:
                    {
                        query = "UPDATE MASTER_LEDGER SET IS_ASSET_LOSS_LEDGER = 1 WHERE LEDGER_ID IN (?LEDGER_ID)";
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateLedgerOptionsInkindLedgersetzero:
                    {
                        query = "UPDATE MASTER_LEDGER SET IS_INKIND_LEDGER=0;";
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateLedgerOptionsGainLedgerssetzero:
                    {
                        query = "UPDATE MASTER_LEDGER SET IS_ASSET_GAIN_LEDGER = 0;";
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateLedgerOptionsLossLedgerssetzero:
                    {
                        query = "UPDATE MASTER_LEDGER SET IS_ASSET_LOSS_LEDGER = 0;";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerByIncludeDepreciationLedger:
                    {
                        query = "SELECT LEDGER_ID,\n" +
                             "       MLG.LEDGER_GROUP,\n" +
                                "       LEDGER_CODE,\n" +
                                "       LEDGER_NAME,\n" +
                                "       IS_DEPRECIATION_LEDGER AS SELECT_TMP\n" +
                                "  FROM MASTER_LEDGER ML\n" +
                                " INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                                "    ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                                "  WHERE MLG.GROUP_ID  NOT IN (12,13,14) AND STATUS=0 \n" +
                                " ORDER BY LEDGER_NAME;";
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateLedgerOptionsDepreciationsetone:
                    {
                        query = "UPDATE MASTER_LEDGER SET IS_DEPRECIATION_LEDGER=1 WHERE LEDGER_ID IN (?LEDGER_ID)";
                        break;
                    }

                case SQLCommand.LedgerBank.UpdateLedgerOptionsDisposalsetone:
                    {
                        query = "UPDATE MASTER_LEDGER SET IS_DISPOSAL_LEDGER = 1 WHERE LEDGER_ID IN (?LEDGER_ID)";
                        break;
                    }

                case SQLCommand.LedgerBank.UpdateLedgerOptionsDepreciationsetzero:
                    {
                        query = "UPDATE MASTER_LEDGER SET IS_DEPRECIATION_LEDGER=0;";
                        break;
                    }

                case SQLCommand.LedgerBank.UpdateLedgerOptionsDisposalsetzero:
                    {
                        query = "UPDATE MASTER_LEDGER SET IS_DISPOSAL_LEDGER = 0;";
                        break;
                    }

                case SQLCommand.LedgerBank.UpdateLedgerOptionsBankInterestsetone:
                    {
                        query = "UPDATE MASTER_LEDGER SET IS_BANK_INTEREST_LEDGER=1 WHERE LEDGER_ID IN (?LEDGER_ID)";
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateLedgerOptionsBankInterestsetzero:
                    {
                        query = "UPDATE MASTER_LEDGER SET IS_BANK_INTEREST_LEDGER=0\n" +
                                "WHERE LEDGER_ID NOT IN (SELECT INTEREST_LEDGER_ID FROM FD_RENEWAL FDR\n" +
                                "INNER JOIN FD_ACCOUNT FA ON FA.FD_ACCOUNT_ID = FDR.FD_ACCOUNT_ID AND FA.STATUS=1 AND FDR.STATUS=1)";
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateLedgerOptionsBankSBInterestsetDisableAll:
                    {
                        query = "UPDATE MASTER_LEDGER SET IS_BANK_SB_INTEREST_LEDGER = 0";
                        /*"WHERE LEDGER_ID NOT IN (SELECT INTEREST_LEDGER_ID FROM FD_RENEWAL FDR\n" +
                        "INNER JOIN FD_ACCOUNT FA ON FA.FD_ACCOUNT_ID = FDR.FD_ACCOUNT_ID AND FA.STATUS=1)";*/
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateLedgerOptionsBankSBInterestByLedger:
                    {
                        query = "UPDATE MASTER_LEDGER SET IS_BANK_SB_INTEREST_LEDGER = 1 WHERE LEDGER_ID IN (?LEDGER_ID)";
                        break;
                    }

                case SQLCommand.LedgerBank.UpdateLedgerOptionsBankFDPenaltyLedgers:
                    {
                        query = "UPDATE MASTER_LEDGER SET IS_BANK_FD_PENALTY_LEDGER = 1 WHERE LEDGER_ID IN (?LEDGER_ID)";
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateLedgerOptionsBankFDPenaltyLedgersSetDisableAll:
                    {
                        query = "UPDATE MASTER_LEDGER SET IS_BANK_FD_PENALTY_LEDGER = 0\n" +
                                "WHERE LEDGER_ID NOT IN (SELECT CHARGE_LEDGER_ID FROM FD_RENEWAL FDR\n" +
                                "INNER JOIN FD_ACCOUNT FA ON FA.FD_ACCOUNT_ID = FDR.FD_ACCOUNT_ID AND FA.STATUS=1 AND FDR.STATUS=1)";
                        break;
                    }
                case SQLCommand.LedgerBank.CheckFDInterestLedgerVoucherExists:
                    {
                        query = "SELECT FA.FD_ACCOUNT_ID, FDR.FD_RENEWAL_ID FROM FD_RENEWAL FDR\n" +
                                "INNER JOIN FD_ACCOUNT FA ON FA.FD_ACCOUNT_ID = FDR.FD_ACCOUNT_ID AND FA.STATUS=1 AND FDR.STATUS=1\n" +
                                "WHERE FDR.INTEREST_LEDGER_ID = ?LEDGER_ID LIMIT 1";
                        break;
                    }
                case SQLCommand.LedgerBank.CheckFDPenaltyLedgerVoucherExists:
                    {
                        query = "SELECT FA.FD_ACCOUNT_ID, FDR.FD_RENEWAL_ID FROM FD_RENEWAL FDR\n" +
                                "INNER JOIN FD_ACCOUNT FA ON FA.FD_ACCOUNT_ID = FDR.FD_ACCOUNT_ID AND FA.STATUS=1 AND FDR.STATUS=1\n" +
                                "WHERE FDR.CHARGE_LEDGER_ID = ?LEDGER_ID LIMIT 1";
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateLedgerOptionsBankCommissionDisableAll:
                    {
                        query = "UPDATE MASTER_LEDGER SET IS_BANK_COMMISSION_LEDGER = 0";
                        /*"WHERE LEDGER_ID NOT IN (SELECT INTEREST_LEDGER_ID FROM FD_RENEWAL FDR\n" +
                        "INNER JOIN FD_ACCOUNT FA ON FA.FD_ACCOUNT_ID = FDR.FD_ACCOUNT_ID AND FA.STATUS=1)";*/
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateLedgerOptionsBankCommissionByLedger:
                    {
                        query = "UPDATE MASTER_LEDGER SET IS_BANK_COMMISSION_LEDGER = 1 WHERE LEDGER_ID IN (?LEDGER_ID)";
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateLedgerOptionsSubsidyzero:
                    {
                        query = "UPDATE MASTER_LEDGER SET IS_SUBSIDY_LEDGER=0;";
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateLedgerOptionsGSTZero:
                    {
                        query = "UPDATE MASTER_LEDGER ML INNER JOIN TDS_CREDTIORS_PROFILE TCP ON TCP.LEDGER_ID = ML.LEDGER_ID\n" +
                                "SET ML.IS_GST_LEDGERS=0, ML.GST_SERVICE_TYPE=0, TCP.GST_ID=0\n" +
                                "WHERE GST_SERVICE_TYPE=?GST_SERVICE_TYPE AND TCP.GST_ID=?GST_Id;";
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateLedgerOptionsSubsidyone:
                    {
                        query = "UPDATE MASTER_LEDGER SET IS_SUBSIDY_LEDGER=1 WHERE LEDGER_ID IN (?LEDGER_ID)";
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateLedgerOptionsGSTone:
                    {
                        query = "UPDATE MASTER_LEDGER SET IS_GST_LEDGERS=1,GST_SERVICE_TYPE =?GST_SERVICE_TYPE WHERE LEDGER_ID IN (?LEDGER_ID)";
                        break;
                    }
                case SQLCommand.LedgerBank.InsertCreditorsOptionsGST:
                    {
                        query = "INSERT INTO TDS_CREDTIORS_PROFILE (LEDGER_ID,GST_Id)\n" +
                                "VALUES (?LEDGER_ID,?GST_Id)";
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateCreditorsOptionsGST:
                    {
                        query = "UPDATE TDS_CREDTIORS_PROFILE SET LEDGER_ID=?LEDGER_ID, GST_Id=?GST_Id\n" +
                                "WHERE LEDGER_ID =?LEDGER_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.CheckTransactionMadeByLedger:
                    {
                        query = "SELECT VT.LEDGER_ID\n" +
                                "  FROM VOUCHER_MASTER_TRANS VMT\n" +
                                " INNER JOIN VOUCHER_TRANS VT\n" +
                                "    ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                " INNER JOIN VOUCHER_CC_TRANS VCC\n" +
                                "    ON VCC.LEDGER_ID = VT.LEDGER_ID\n" +
                                " AND VCC.VOUCHER_ID=VMT.VOUCHER_ID \n" +
                                " WHERE VT.LEDGER_ID IN (?LEDGER_ID)\n" +
                                "   AND VMT.STATUS = 1\n" +
                                " GROUP BY LEDGER_ID;";
                        break;
                    }
                case SQLCommand.LedgerBank.CheckTransactionMadeByAssetGainLedger:
                    {
                        query = "SELECT VT.LEDGER_ID\n" +
                                "  FROM VOUCHER_MASTER_TRANS VMT\n" +
                                " INNER JOIN VOUCHER_TRANS VT\n" +
                                "    ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                " INNER JOIN MASTER_LEDGER ML\n" +
                                "    ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                                "--  AND VCC.VOUCHER_ID=VMT.VOUCHER_ID\n" +
                                " WHERE VT.LEDGER_ID IN (?LEDGER_ID)\n" +
                                "   AND ML.IS_ASSET_GAIN_LEDGER = 1\n" +
                                "   AND VMT.STATUS = 1\n" +
                                " GROUP BY LEDGER_ID;";
                        break;
                    }

                case SQLCommand.LedgerBank.CheckTransactionMadeByAssetDisposalLedger:
                    {
                        query = "SELECT VT.LEDGER_ID\n" +
                                "  FROM VOUCHER_MASTER_TRANS VMT\n" +
                                " INNER JOIN VOUCHER_TRANS VT\n" +
                                "    ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                " INNER JOIN MASTER_LEDGER ML\n" +
                                "    ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                                "--  AND VCC.VOUCHER_ID=VMT.VOUCHER_ID\n" +
                                " WHERE VT.LEDGER_ID IN (?LEDGER_ID)\n" +
                                "   AND ML.IS_DISPOSAL_LEDGER = 1\n" +
                                "   AND VMT.STATUS = 1\n" +
                                " GROUP BY LEDGER_ID;";
                        break;
                    }

                case SQLCommand.LedgerBank.CheckTransactionMadeByAssetLossLedger:
                    {
                        query = "SELECT VT.LEDGER_ID\n" +
                                "  FROM VOUCHER_MASTER_TRANS VMT\n" +
                                " INNER JOIN VOUCHER_TRANS VT\n" +
                                "    ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                " INNER JOIN MASTER_LEDGER ML\n" +
                                "    ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                                "--  AND VCC.VOUCHER_ID=VMT.VOUCHER_ID\n" +
                                " WHERE VT.LEDGER_ID IN (?LEDGER_ID)\n" +
                                "   AND ML.IS_ASSET_LOSS_LEDGER = 1\n" +
                                "   AND VMT.STATUS = 1\n" +
                                " GROUP BY LEDGER_ID;";
                        break;
                    }
                case SQLCommand.LedgerBank.CheckFDTransactionMadeByLedger:
                    {
                        query = "SELECT VT.LEDGER_ID\n" +
                                "  FROM VOUCHER_MASTER_TRANS VMT\n" +
                                " INNER JOIN VOUCHER_TRANS VT\n" +
                                "    ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                " WHERE VT.LEDGER_ID IN (?LEDGER_ID)\n" +
                                "   AND VMT.STATUS = 1 AND VOUCHER_SUB_TYPE='FD'\n" +
                                " GROUP BY LEDGER_ID;";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerName:
                    {
                        query = "SELECT LEDGER_NAME FROM MASTER_LEDGER WHERE LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchGroupName:
                    {
                        query = "SELECT LEDGER_GROUP FROM MASTER_LEDGER_GROUP WHERE GROUP_ID=?GROUP_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.CheckFDTransactionMadeByInterestLedger:
                    {
                        query = "SELECT VT.LEDGER_ID\n" +
                                "  FROM VOUCHER_MASTER_TRANS VMT\n" +
                                " INNER JOIN VOUCHER_TRANS VT\n" +
                                "    ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                " INNER JOIN FD_RENEWAL FDR ON FDR.INTEREST_LEDGER_ID = VT.LEDGER_ID\n" +
                                " WHERE VT.LEDGER_ID IN (?LEDGER_ID)\n" +
                                "   AND VMT.STATUS = 1 AND VOUCHER_SUB_TYPE='FD'\n" +
                                " GROUP BY LEDGER_ID;";
                        break;
                    }
                case SQLCommand.LedgerBank.IsTDSLedger:
                    {
                        query = "SELECT COUNT(*) FROM MASTER_LEDGER WHERE LEDGER_ID=?LEDGER_ID AND IS_TDS_LEDGER=1 AND STATUS=0";
                        break;
                    }
                case SQLCommand.LedgerBank.CheckBankAccountMappedToProject:
                    {
                        query = "SELECT COUNT(*) FROM PROJECT_LEDGER WHERE LEDGER_ID=?LEDGER_ID ";
                        break;
                    }
                case SQLCommand.LedgerBank.CheckBankAccountMappedToLegalEntity:
                    {
                        query = "SELECT COUNT(*) FROM MASTER_INSTI_PERFERENCE WHERE LEDGER_ID=?LEDGER_ID AND CUSTOMERID<>?CUSTOMERID";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchTDSLedger:
                    {
                        //query = "SELECT LEDGER_ID,LEDGER_NAME,IS_TDS_LEDGER FROM MASTER_LEDGER WHERE LEDGER_ID=?LEDGER_ID AND IS_TDS_LEDGER=1";
                        query = "SELECT IS_TDS_LEDGER, NATURE_ID, ML.LEDGER_ID, ML.LEDGER_NAME,MLG.GROUP_ID \n" +
                                        "  FROM MASTER_LEDGER AS ML\n" +
                                        " INNER JOIN MASTER_LEDGER_GROUP AS MLG\n" +
                                        "    ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                                        " WHERE LEDGER_ID = ?LEDGER_ID\n" +
                                        "   AND STATUS = 0\n" +
                                        "   AND ML.IS_TDS_LEDGER = 1";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchTDSExpNOP:
                    {
                        query = "SELECT NATURE_OF_PAYMENT_ID\n" +
                        "  FROM MASTER_LEDGER AS ML\n" +
                        "  LEFT JOIN tds_credtiors_profile as TCP\n" +
                        "    ON ML.LEDGER_ID = TCP.LEDGER_ID\n" +
                        " WHERE TCP.LEDGER_ID = ?LEDGER_ID\n" +
                        "   AND ML.STATUS = 0\n" +
                        "   AND ML.IS_TDS_LEDGER = 1;";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerIdByLedger:
                    {
                        //query = "SELECT LEDGER_ID FROM MASTER_LEDGER WHERE LEDGER_NAME=?LEDGER_NAME";
                        query = "SELECT LEDGER_ID\n" +
                                "  FROM PROJECT_LEDGER\n" +
                                " WHERE PROJECT_ID = ?PROJECT_ID\n" +
                                "   and LEDGER_ID = (SELECT LEDGER_ID\n" +
                                "                      FROM MASTER_LEDGER\n" +
                                "                     WHERE LEDGER_NAME = ?LEDGER_NAME {AND GROUP_ID IN (?GROUP_ID)});";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchDutiesTaxLedger:
                    {
                        query = "SELECT ML.LEDGER_ID AS LEDGER_ID, LEDGER_NAME\n" +
                                "  FROM MASTER_LEDGER AS ML\n" +
                                " INNER JOIN PROJECT_LEDGER AS PL\n" +
                                "    ON ML.LEDGER_ID = PL.LEDGER_ID\n" +
                                " INNER JOIN tds_credtiors_profile AS TCP\n" +
                                "     ON ML.LEDGER_ID=TCP.LEDGER_ID\n" +
                                " WHERE ML.GROUP_ID IN (24)\n" +
                                "   AND ML.STATUS = 0 AND ML.IS_TDS_LEDGER=1\n" +
                                "   AND PL.PROJECT_ID IN (?PROJECT_ID)";

                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerByFixedGroup:
                    {
                        query = "SELECT ML.BANK_ACCOUNT_ID,\n" +
                                "       ML.GROUP_ID,\n" +
                                "       MP.NATURE_ID,\n" +
                                "       ML.LEDGER_ID,\n" +
                                "       ML.LEDGER_CODE,\n" +
                                "       ML.IS_COST_CENTER,\n" +
                                "       CONCAT(ML.LEDGER_NAME,\n" +
                                "              CONCAT(' - ', MP.LEDGER_GROUP),\n" +
                                "              IF(ML.LEDGER_CODE = '', '', CONCAT(' (', ML.LEDGER_CODE, ')'))) AS LEDGER_NAME\n" +
                                "  FROM MASTER_LEDGER ML\n" +
                                "  LEFT JOIN MASTER_LEDGER_GROUP MP\n" +
                                "    ON ML.GROUP_ID = MP.GROUP_ID\n" +
                                " WHERE MP.GROUP_ID IN (?GROUP_ID)\n" +
                                "   AND ML.STATUS = 0\n" +
                                "    AND MP.GROUP_ID NOT IN (12,13,14)\n" +
                                "   AND ML.LEDGER_TYPE = 'GN'\n" +
                                " ORDER BY LEDGER_NAME ASC;"; //Previous Given ORDER BY LEDGER_CODE ASC
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerByNature:
                    {
                        query = "SELECT ML.BANK_ACCOUNT_ID,\n" +
                                "       ML.GROUP_ID,\n" +
                                "       MP.NATURE_ID,\n" +
                                "       ML.LEDGER_ID,\n" +
                                "       ML.LEDGER_CODE,\n" +
                                "       ML.IS_COST_CENTER,\n" +
                                "       CONCAT(ML.LEDGER_NAME,\n" +
                                "              CONCAT(' - ', MP.LEDGER_GROUP),\n" +
                                "              IF(ML.LEDGER_CODE = '', '', CONCAT(' (', ML.LEDGER_CODE, ')'))) AS LEDGER_NAME\n" +
                                "  FROM MASTER_LEDGER ML\n" +
                                "  LEFT JOIN MASTER_LEDGER_GROUP MP\n" +
                                "    ON ML.GROUP_ID = MP.GROUP_ID\n" +
                                " WHERE MP.NATURE_ID IN (?GROUP_ID)\n" +
                                "   AND ML.STATUS = 0\n" +
                                "    AND MP.GROUP_ID NOT IN (12,13,14)\n" +
                                "   AND ML.LEDGER_TYPE = 'GN'\n" +
                                " ORDER BY LEDGER_NAME ASC;"; //Previous Given ORDER BY LEDGER_CODE ASC
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerByNatureAll:
                    {
                        query = "SELECT ML.BANK_ACCOUNT_ID,\n" +
                                "       ML.GROUP_ID,\n" +
                                "       MP.NATURE_ID,\n" +
                                "       ML.LEDGER_ID,\n" +
                                "       ML.LEDGER_CODE,\n" +
                                "       ML.IS_COST_CENTER,\n" +
                                "       CONCAT(ML.LEDGER_NAME,\n" +
                                "              CONCAT(' - ', MP.LEDGER_GROUP),\n" +
                                "              IF(ML.LEDGER_CODE = '', '', CONCAT(' (', ML.LEDGER_CODE, ')'))) AS LEDGER_NAME\n" +
                                "  FROM MASTER_LEDGER ML\n" +
                                "  LEFT JOIN MASTER_LEDGER_GROUP MP\n" +
                                "    ON ML.GROUP_ID = MP.GROUP_ID\n" +
                                " WHERE ML.STATUS = 0\n" +
                                "   AND MP.GROUP_ID NOT IN (12,13,14)\n" +
                                "   AND ML.LEDGER_TYPE = 'GN'\n" +
                                " ORDER BY LEDGER_NAME ASC;"; //Previous Given ORDER BY LEDGER_CODE ASC
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerIdByName:
                    {
                        query = "SELECT LEDGER_ID FROM MASTER_LEDGER WHERE LEDGER_NAME=?LEDGER_NAME ORDER BY LEDGER_NAME ASC"; // ORDER BY LEDGER_NAME
                        break;
                    }

                /// aldrin for asset user control
                /// To fetch all the inkind ledgers
                case SQLCommand.LedgerBank.FetchAllInkindLedgers:
                    {
                        //query = "SELECT  LEDGER_ID,LEDGER_CODE,GROUP_ID,BANK_ACCOUNT_ID FROM MASTER_LEDGER WHERE IS_INKIND_LEDGER";
                        query = "SELECT ML.BANK_ACCOUNT_ID,\n" +
                                "       ML.GROUP_ID,\n" +
                                "       MP.NATURE_ID,\n" +
                                "       ML.LEDGER_ID,\n" +
                                "       ML.LEDGER_CODE,\n" +
                                "       ML.IS_COST_CENTER,\n" +
                                "       CONCAT(ML.LEDGER_NAME,\n" +
                                "              CONCAT(' - ', MP.LEDGER_GROUP),\n" +
                                "              IF(ML.LEDGER_CODE = '', '', CONCAT(' (', ML.LEDGER_CODE, ')'))) AS LEDGER_NAME\n" +
                                "  FROM MASTER_LEDGER ML\n" +
                                "  LEFT JOIN PROJECT_LEDGER PL\n" +
                                "    ON PL.LEDGER_ID = ML.LEDGER_ID, MASTER_LEDGER_GROUP MP\n" +
                                " WHERE ML.GROUP_ID IN (SELECT GROUP_ID\n" +
                                "                         FROM MASTER_LEDGER_GROUP\n" +
                                "                        WHERE ML.GROUP_ID NOT IN (14))\n" +
                                "   AND ML.GROUP_ID = MP.GROUP_ID\n" +
                                "   AND ML.STATUS = 0\n" +
                                "   AND ML.IS_INKIND_LEDGER = 1\n" +
                                "   AND PROJECT_ID = ?PROJECT_ID\n" +
                                " ORDER BY LEDGER_NAME ASC"; //Previous Given ORDER BY LEDGER_CODE ASC
                        break;
                    }

                case SQLCommand.LedgerBank.FetchGainLedgers:
                    {
                        query = "SELECT ML.BANK_ACCOUNT_ID,\n" +
                                "       ML.GROUP_ID,\n" +
                                "       MP.NATURE_ID,\n" +
                                "       ML.LEDGER_ID,\n" +
                                "       ML.LEDGER_CODE,\n" +
                                "       ML.IS_COST_CENTER,\n" +
                                "       CONCAT(ML.LEDGER_NAME,\n" +
                                "              CONCAT(' - ', MP.LEDGER_GROUP),\n" +
                                "              IF(ML.LEDGER_CODE = '', '', CONCAT(' (', ML.LEDGER_CODE, ')'))) AS LEDGER_NAME\n" +
                                "  FROM MASTER_LEDGER ML\n" +
                                "  LEFT JOIN PROJECT_LEDGER PL\n" +
                                "    ON PL.LEDGER_ID = ML.LEDGER_ID, MASTER_LEDGER_GROUP MP\n" +
                                " WHERE ML.GROUP_ID IN (SELECT GROUP_ID\n" +
                                "                         FROM MASTER_LEDGER_GROUP\n" +
                                "                        WHERE ML.GROUP_ID NOT IN (14))\n" +
                                "   AND ML.GROUP_ID = MP.GROUP_ID\n" +
                                "   AND ML.STATUS = 0\n" +
                                "   AND ML.IS_ASSET_GAIN_LEDGER = 1\n" +
                                "   AND PROJECT_ID = ?PROJECT_ID\n" +
                                " ORDER BY LEDGER_NAME ASC"; //Previous Given ORDER BY LEDGER_CODE ASC
                        break;
                    }

                case SQLCommand.LedgerBank.FetchLossLedgers:
                    {
                        query = "SELECT ML.BANK_ACCOUNT_ID,\n" +
                                "       ML.GROUP_ID,\n" +
                                "       MP.NATURE_ID,\n" +
                                "       ML.LEDGER_ID,\n" +
                                "       ML.LEDGER_CODE,\n" +
                                "       ML.IS_COST_CENTER,\n" +
                                "       CONCAT(ML.LEDGER_NAME,\n" +
                                "              CONCAT(' - ', MP.LEDGER_GROUP),\n" +
                                "              IF(ML.LEDGER_CODE = '', '', CONCAT(' (', ML.LEDGER_CODE, ')'))) AS LEDGER_NAME\n" +
                                "  FROM MASTER_LEDGER ML\n" +
                                "  LEFT JOIN PROJECT_LEDGER PL\n" +
                                "    ON PL.LEDGER_ID = ML.LEDGER_ID, MASTER_LEDGER_GROUP MP\n" +
                                " WHERE ML.GROUP_ID IN (SELECT GROUP_ID\n" +
                                "                         FROM MASTER_LEDGER_GROUP\n" +
                                "                        WHERE ML.GROUP_ID NOT IN (14))\n" +
                                "   AND ML.GROUP_ID = MP.GROUP_ID\n" +
                                "   AND ML.STATUS = 0\n" +
                                "   AND ML.IS_ASSET_LOSS_LEDGER = 1\n" +
                                "   AND PROJECT_ID = ?PROJECT_ID\n" +
                                " ORDER BY LEDGER_NAME ASC"; //Previous Given ORDER BY LEDGER_CODE ASC
                        break;
                    }

                case SQLCommand.LedgerBank.FetchAllExpenceLedgers:
                    {
                        query = "SELECT ML.BANK_ACCOUNT_ID,\n" +
                                 "       ML.GROUP_ID,\n" +
                                 "       MP.NATURE_ID,\n" +
                                 "       ML.LEDGER_ID,\n" +
                                 "       ML.LEDGER_CODE,\n" +
                                 "       ML.IS_COST_CENTER,\n" +
                                 "       CONCAT(ML.LEDGER_NAME,\n" +
                                 "              CONCAT(' - ', MP.LEDGER_GROUP),\n" +
                                 "              IF(ML.LEDGER_CODE = '', '', CONCAT(' (', ML.LEDGER_CODE, ')'))) AS LEDGER_NAME\n" +
                                 "  FROM MASTER_LEDGER ML\n" +
                                 "  LEFT JOIN MASTER_LEDGER_GROUP MP\n" +
                                 "    ON ML.GROUP_ID = MP.GROUP_ID\n" +
                                 " INNER JOIN PROJECT_LEDGER PL\n" +
                                 "    ON PL.PROJECT_ID IN (?PROJECT_ID)\n" +
                                 "   AND ML.LEDGER_ID = PL.LEDGER_ID\n" +
                                 " WHERE MP.NATURE_ID IN (?GROUP_ID)\n" +
                                 "   AND ML.STATUS = 0\n" +
                                 "   AND MP.GROUP_ID NOT IN (12, 13, 14)\n" +
                                  " {AND (ML.DATE_CLOSED IS NULL OR ML.DATE_CLOSED >= ?DATE_CLOSED) } " +
                                 "   AND ML.LEDGER_TYPE = 'GN'\n" +
                                 " ORDER BY LEDGER_NAME ASC"; //Previous Given ORDER BY LEDGER_CODE ASC
                        break;
                    }

                case SQLCommand.LedgerBank.FetchAllDisposalLedgers:
                    {
                        query = "SELECT ML.BANK_ACCOUNT_ID,\n" +
                               "       ML.GROUP_ID,\n" +
                               "       MP.NATURE_ID,\n" +
                               "       ML.LEDGER_ID,\n" +
                               "       ML.LEDGER_CODE,\n" +
                               "       ML.IS_COST_CENTER,\n" +
                               "       CONCAT(ML.LEDGER_NAME,\n" +
                               "              CONCAT(' - ', MP.LEDGER_GROUP),\n" +
                               "              IF(ML.LEDGER_CODE = '', '', CONCAT(' (', ML.LEDGER_CODE, ')'))) AS LEDGER_NAME\n" +
                               "  FROM MASTER_LEDGER ML\n" +
                               "  LEFT JOIN PROJECT_LEDGER PL\n" +
                               "    ON PL.LEDGER_ID = ML.LEDGER_ID, MASTER_LEDGER_GROUP MP\n" +
                               " WHERE ML.GROUP_ID IN (SELECT GROUP_ID\n" +
                               "                         FROM MASTER_LEDGER_GROUP\n" +
                               "                        WHERE ML.GROUP_ID NOT IN (14))\n" +
                               "   AND ML.GROUP_ID = MP.GROUP_ID\n" +
                               "   AND ML.STATUS = 0\n" +
                               "   AND ML.IS_DISPOSAL_LEDGER = 1\n" +
                               "   AND PROJECT_ID = ?PROJECT_ID\n" +
                               " ORDER BY LEDGER_NAME ASC"; //Previous Given ORDER BY LEDGER_CODE ASC
                        break;
                    }
                case SQLCommand.LedgerBank.FetchCashBankLedgerByID:
                    {
                        query = "SELECT CASE\n" +
                                "         WHEN ML.GROUP_ID = 12 THEN\n" +
                                "          CONCAT(ML.LEDGER_NAME, ' ( ', MB.BANK, ' - ', MB.BRANCH, ')')\n" +
                                "       END AS LEDGER_NAME\n" +
                                "  FROM MASTER_LEDGER ML\n" +
                                "  LEFT JOIN MASTER_BANK_ACCOUNT MBA\n" +
                                "    ON ML.LEDGER_ID = MBA.LEDGER_ID\n" +
                                "  LEFT JOIN MASTER_BANK MB\n" +
                                "    ON MB.BANK_ID = MBA.BANK_ID\n" +
                                "  LEFT JOIN PROJECT_LEDGER PL\n" +
                                "    ON PL.LEDGER_ID = ML.LEDGER_ID, MASTER_LEDGER_GROUP MP\n" +
                                " WHERE ML.GROUP_ID IN\n" +
                                "       (SELECT GROUP_ID FROM MASTER_LEDGER_GROUP WHERE ML.GROUP_ID IN (12))\n" +
                                "   AND ML.GROUP_ID = MP.GROUP_ID\n" +
                                "   AND ML.STATUS = 0\n" +
                                "   AND ML.LEDGER_TYPE = 'GN'\n" +
                                "   AND ml.Ledger_id = ?LEDGER_ID\n" +
                                "   AND PL.PROJECT_ID = ?PROJECT_ID\n" +
                                " GROUP BY ML.LEDGER_NAME;";

                        break;
                    }

                case SQLCommand.LedgerBank.FetchAllUnusedLedgers: // done the Accessflag removed from the source
                    {
                        //query= "SELECT ML.LEDGER_ID, LEDGER_GROUP, LEDGER_CODE, LEDGER_NAME\n" +
                        //        "  FROM MASTER_LEDGER ML\n" + 
                        //        " INNER JOIN MASTER_LEDGER_GROUP MLG\n" + 
                        //        "    ON MLG.GROUP_ID = ML.GROUP_ID\n" + 
                        //        "\n" + 
                        //        " WHERE ML.LEDGER_ID NOT IN (1, 2, 3)\n" + 
                        //        "\n" + 
                        //        "   AND ML.LEDGER_ID NOT IN (SELECT LEDGER_ID\n" + 
                        //        "                              FROM LEDGER_BALANCE\n" + 
                        //        "                             GROUP BY LEDGER_ID\n" + 
                        //        "                            UNION\n" + 
                        //        "                            SELECT LEDGER_ID\n" + 
                        //        "                              FROM BUDGET_LEDGER\n" + 
                        //        "                             GROUP BY LEDGER_ID\n" + 
                        //        "                             ORDER BY LEDGER_ID)\n" + 
                        //        "\n" + 
                        //        "    OR ML.LEDGER_ID IN\n" + 
                        //        "       (SELECT LEDGER_ID\n" + 
                        //        "          FROM (SELECT LEDGER_ID, TRANS_FLAG\n" + 
                        //        "                  FROM LEDGER_BALANCE\n" + 
                        //        "                 GROUP BY LEDGER_ID\n" + 
                        //        "                HAVING SUM(AMOUNT) = 0 AND TRANS_FLAG = 'OP') AS T)\n" + 
                        //        " ORDER BY LEDGER_NAME;";

                        //query = "SELECT ML.LEDGER_ID, LEDGER_GROUP, LEDGER_CODE, LEDGER_NAME\n" +
                        //        "  FROM MASTER_LEDGER ML\n" +
                        //        " INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                        //        "    ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                        //        "\n" +
                        //        " WHERE ML.LEDGER_ID NOT IN (1, 2, 3)\n" +
                        //        "\n" +
                        //        "   AND ML.LEDGER_ID NOT IN (SELECT LEDGER_ID\n" +
                        //        "                              FROM LEDGER_BALANCE\n" +
                        //        "                             GROUP BY LEDGER_ID\n" +
                        //        "                            UNION\n" +
                        //        "                            SELECT LEDGER_ID\n" +
                        //        "                              FROM BUDGET_LEDGER\n" +
                        //        "                             GROUP BY LEDGER_ID\n" +
                        //        "                             ORDER BY LEDGER_ID)\n" +
                        //        "\n" +
                        //        "    OR ML.LEDGER_ID IN\n" +
                        //        "       (SELECT LEDGER_ID\n" +
                        //        "          FROM (SELECT LEDGER_ID, TRANS_FLAG\n" +
                        //        "                  FROM LEDGER_BALANCE\n" +
                        //        "                 GROUP BY LEDGER_ID\n" +
                        //        "                HAVING SUM(AMOUNT) = 0 AND TRANS_FLAG = 'OP' AND LEDGER_ID NOT IN (SELECT LEDGER_ID\n" +
                        //        "               FROM LEDGER_BALANCE  GROUP BY LEDGER_ID\n" +
                        //        "               HAVING SUM(AMOUNT) = 0 AND TRANS_FLAG = 'TR')) AS T)\n" +
                        //        " ORDER BY LEDGER_ID;";

                        // Changed by Praveen on 02-09-2016

                        query = "SELECT ML.LEDGER_ID, LEDGER_GROUP, LEDGER_CODE, LEDGER_NAME\n" +
                                "  FROM MASTER_LEDGER ML\n" +
                                " INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                                "    ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                                "\n" +
                                " WHERE ML.LEDGER_ID NOT IN (1, 2, 3) AND ML.ACCESS_FLAG <>2 \n" +
                                "\n" +
                                "   AND ML.LEDGER_ID NOT IN (SELECT LEDGER_ID\n" +
                                "                              FROM LEDGER_BALANCE\n" +
                                "                             GROUP BY LEDGER_ID\n" +
                                "                            UNION\n" +
                                "                            SELECT LEDGER_ID\n" +
                                "                              FROM BUDGET_LEDGER\n" +
                                "                             GROUP BY LEDGER_ID\n" +
                                "                             )\n" + //ORDER BY LEDGER_ID
                                "      OR ML.LEDGER_ID IN\n" +
                                "       (SELECT LEDGER_ID\n" +
                                "          FROM (SELECT LEDGER_ID, TRANS_FLAG\n" +
                                "                  FROM LEDGER_BALANCE\n" +
                                "                 WHERE LEDGER_ID NOT IN\n" +
                                "                       (SELECT VT.LEDGER_ID AS VLEDID\n" +
                                "                          FROM VOUCHER_MASTER_TRANS VMT\n" +
                                "                         INNER JOIN VOUCHER_TRANS VT\n" +
                                "                            ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                "                         WHERE VMT.STATUS = 1\n" +
                                "                         GROUP BY LEDGER_ID)\n" +
                                "                 GROUP BY LEDGER_ID\n" +
                                "                HAVING SUM(AMOUNT) = 0 AND TRANS_FLAG = 'OP' AND LEDGER_ID NOT IN\n" +
                                "                (SELECT LEDGER_ID FROM LEDGER_BALANCE GROUP BY LEDGER_ID HAVING SUM(AMOUNT) = 0 AND TRANS_FLAG = 'TR'\n" +
                                "                  UNION SELECT LEDGER_ID FROM BUDGET_LEDGER GROUP BY LEDGER_ID)) AS T)";
                        //"    ORDER BY LEDGER_ID;";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerById:
                    {
                        query = "SELECT LEDGER_NAME AS LEDGER, IFNULL(GST_HSN_SAC_CODE,'') AS GST_HSN_SAC_CODE FROM MASTER_LEDGER WHERE LEDGER_ID IN (?LEDGER_ID);";
                        break;
                    }
                case SQLCommand.LedgerBank.DeleteAllUnusedLedgers:
                    {
                        query = "DELETE FROM PROJECT_LEDGER WHERE LEDGER_ID IN(?ID_COLLECTIONS);\n" +
                                "DELETE FROM HEADOFFICE_MAPPED_LEDGER WHERE LEDGER_ID IN(?ID_COLLECTIONS);\n" +
                                "DELETE FROM PROJECT_CATEGORY_LEDGER WHERE LEDGER_ID IN(?ID_COLLECTIONS);\n" +
                                "DELETE FROM MASTER_LEDGER WHERE LEDGER_ID IN (?ID_COLLECTIONS)";

                        break;
                    }

                case SQLCommand.LedgerBank.FetchAllUnusedGroups:
                    {
                        query = "SELECT GROUP_ID,LEDGER_GROUP FROM MASTER_LEDGER_GROUP WHERE GROUP_ID NOT IN(SELECT GROUP_ID FROM MASTER_LEDGER GROUP BY GROUP_ID)";
                        break;
                    }
                case SQLCommand.LedgerBank.DeleteAllUnusedGroups:
                    {
                        query = "DELETE FROM MASTER_LEDGER_GROUP WHERE GROUP_ID IN(?IDs)";
                        break;
                    }
                case SQLCommand.LedgerBank.ChangeLedgerNameInMaster:
                    {
                        query = "UPDATE MASTER_LEDGER SET LEDGER_NAME = ?LEDGER_NAME WHERE LEDGER_NAME = ?LEDGER_CODE;"; // ?LEDGER_CODE :: Old Ledger name
                        break;
                    }
                case SQLCommand.LedgerBank.ChangeLedgerNameInHOMaster:
                    {
                        query = "UPDATE MASTER_HEADOFFICE_LEDGER SET LEDGER_NAME = ?LEDGER_NAME WHERE LEDGER_NAME = ?LEDGER_CODE;"; // ?LEDGER_CODE :: Old Ledger name
                        break;
                    }
                case SQLCommand.LedgerBank.FetchHOLedgerForMerge:
                    {
                        query = "SELECT HEADOFFICE_LEDGER_ID AS MERGE_LEDGER_ID, MHL.GROUP_ID AS MERGE_GROUP_ID, MHL.IS_BANK_INTEREST_LEDGER," +
                                 " CONCAT(LEDGER_NAME,' ( ',LEDGER_GROUP,' )') AS MERGE_LEDGER_NAME" +
                                 " FROM MASTER_HEADOFFICE_LEDGER MHL" +
                                 " INNER JOIN MASTER_LEDGER_GROUP MLG ON MHL.GROUP_ID = MLG.GROUP_ID" +
                                 " ORDER BY LEDGER_NAME ASC";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchBOLedgerForMerge:
                    {
                        query = "SELECT ML.LEDGER_ID AS MERGE_LEDGER_ID, ML.GROUP_ID AS MERGE_GROUP_ID," +
                                 "  CONCAT(CONCAT(ML.LEDGER_NAME,' ( ',MLG.LEDGER_GROUP,' ) - '), IF(HML.HEADOFFICE_LEDGER_ID IS NULL,ML.LEDGER_CODE, HML.LEDGER_CODE)) AS MERGE_LEDGER_NAME, ML.IS_COST_CENTER," +
                                 " ML.IS_BANK_INTEREST_LEDGER, ML.IS_TDS_LEDGER, ML.ACCESS_FLAG, IF(HML.HEADOFFICE_LEDGER_ID IS NULL,0, 1) AS IS_BRANCH_LEDGER" +
                                 " FROM MASTER_LEDGER ML" +
                                 " INNER JOIN MASTER_LEDGER_GROUP MLG ON ML.GROUP_ID = MLG.GROUP_ID" +
                                 " LEFT JOIN MASTER_HEADOFFICE_LEDGER HML " +
                                 " ON HML.LEDGER_NAME = ML.LEDGER_NAME" +
                                 " ORDER BY MLG.NATURE_ID, ML.LEDGER_NAME ASC";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchBOLedgers:
                    {
                        query = "SELECT ML.LEDGER_ID AS MERGE_LEDGER_ID, ML.GROUP_ID AS MERGE_GROUP_ID, ML.LEDGER_NAME AS MERGE_LEDGER_NAME,\n" +
                                 "ML.IS_COST_CENTER, ML.IS_BANK_INTEREST_LEDGER, ML.IS_TDS_LEDGER, ML.IS_GST_LEDGERS, ML.IS_DEPRECIATION_LEDGER,\n" +
                                 "ML.IS_ASSET_GAIN_LEDGER, ML.IS_ASSET_GAIN_LEDGER, ML.IS_ASSET_LOSS_LEDGER, ML.IS_DISPOSAL_LEDGER, ML.IS_SUBSIDY_LEDGER, ML.ACCESS_FLAG\n" +
                                 "FROM MASTER_LEDGER ML INNER JOIN MASTER_LEDGER_GROUP MLG ON MLG.GROUP_ID = ML.GROUP_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.ClearAllDeletedDataByLedger:
                    {
                        //On 13/09/2018, This method is used to clear all deleted Vouchers for given ledger
                        query = @"DELETE FROM FD_RENEWAL 
                                        WHERE FD_INTEREST_VOUCHER_ID IN (SELECT VT.VOUCHER_ID FROM VOUCHER_TRANS VT
                                            INNER JOIN VOUCHER_MASTER_TRANS VM ON VM.VOUCHER_ID = VT.VOUCHER_ID
                                            WHERE VT.LEDGER_ID = ?LEDGER_ID AND VM.STATUS = 0) OR
                                        FD_VOUCHER_ID IN (SELECT VT.VOUCHER_ID FROM VOUCHER_TRANS VT
                                            INNER JOIN VOUCHER_MASTER_TRANS VM ON VM.VOUCHER_ID = VT.VOUCHER_ID
                                            WHERE VT.LEDGER_ID = ?LEDGER_ID AND VM.STATUS = 0);

                                  DELETE FROM FD_ACCOUNT WHERE FD_VOUCHER_ID IN (SELECT VT.VOUCHER_ID FROM VOUCHER_TRANS VT
                                        INNER JOIN VOUCHER_MASTER_TRANS VM ON VM.VOUCHER_ID = VT.VOUCHER_ID
                                            WHERE VT.LEDGER_ID = ?LEDGER_ID AND VM.STATUS = 0);

                                  DELETE TDSPD.*, TDSP.* FROM TDS_PAYMENT TDSP INNER JOIN TDS_PAYMENT_DETAIL TDSPD ON TDSP.TDS_PAYMENT_ID = TDSPD.TDS_PAYMENT_ID
                                        WHERE TDSP.VOUCHER_ID IN (SELECT VT.VOUCHER_ID FROM VOUCHER_TRANS VT
                                            INNER JOIN VOUCHER_MASTER_TRANS VM ON VM.VOUCHER_ID = VT.VOUCHER_ID
                                            WHERE VT.LEDGER_ID = ?LEDGER_ID AND VM.STATUS = 0);

                                  DELETE  TPPD.*, TPP.* FROM TDS_PARTY_PAYMENT TPP INNER JOIN TDS_PARTY_PAYMENT_DETAIL TPPD ON TPP.PARTY_PAYMENT_ID = TPPD.PARTY_PAYMENT_ID
                                        WHERE TPP.VOUCHER_ID IN (SELECT VT.VOUCHER_ID FROM VOUCHER_TRANS VT
                                            INNER JOIN VOUCHER_MASTER_TRANS VM ON VM.VOUCHER_ID = VT.VOUCHER_ID
                                            WHERE VT.LEDGER_ID = ?LEDGER_ID AND VM.STATUS = 0);

                                  DELETE  TDD.*, TD.* FROM TDS_DEDUCTION TD INNER JOIN TDS_DEDUCTION_DETAIL TDD ON TD.DEDUCTION_ID = TDD.DEDUCTION_ID
                                        WHERE TD.VOUCHER_ID IN (SELECT VT.VOUCHER_ID FROM VOUCHER_TRANS VT
                                        INNER JOIN VOUCHER_MASTER_TRANS VM ON VM.VOUCHER_ID = VT.VOUCHER_ID
                                            WHERE VT.LEDGER_ID = ?LEDGER_ID AND VM.STATUS = 0);

                                  DELETE  TB.*, TBB.* FROM TDS_BOOKING TB INNER JOIN TDS_BOOKING_DETAIL TBB ON TB.BOOKING_ID = TBB.BOOKING_ID
                                        WHERE TB.VOUCHER_ID IN (SELECT VT.VOUCHER_ID FROM VOUCHER_TRANS VT
                                        INNER JOIN VOUCHER_MASTER_TRANS VM ON VM.VOUCHER_ID = VT.VOUCHER_ID
                                        WHERE VT.LEDGER_ID = ?LEDGER_ID AND VM.STATUS = 0);

                                  DELETE FROM VOUCHER_REFERENCE
                                        WHERE REF_VOUCHER_ID IN (SELECT VT.VOUCHER_ID FROM VOUCHER_TRANS VT
                                            INNER JOIN VOUCHER_MASTER_TRANS VM ON VM.VOUCHER_ID = VT.VOUCHER_ID
                                            WHERE VT.LEDGER_ID = ?LEDGER_ID AND VM.STATUS = 0) OR
                                        REC_PAY_VOUCHER_ID IN (SELECT VT.VOUCHER_ID FROM VOUCHER_TRANS VT
                                           INNER JOIN VOUCHER_MASTER_TRANS VM ON VM.VOUCHER_ID = VT.VOUCHER_ID
                                           WHERE VT.LEDGER_ID = ?LEDGER_ID AND VM.STATUS = 0);                                   
 
                                   DELETE FROM VOUCHER_CC_TRANS
                                        WHERE VOUCHER_ID IN (SELECT VT.VOUCHER_ID FROM VOUCHER_TRANS VT
                                            INNER JOIN VOUCHER_MASTER_TRANS VM ON VM.VOUCHER_ID = VT.VOUCHER_ID
                                            WHERE VT.LEDGER_ID = ?LEDGER_ID AND VM.STATUS = 0);

                                   DELETE VT.*,VM.* FROM VOUCHER_TRANS VT INNER JOIN VOUCHER_MASTER_TRANS VM ON VM.VOUCHER_ID = VT.VOUCHER_ID
                                            WHERE VT.LEDGER_ID = ?LEDGER_ID AND VM.STATUS = 0;

                                   DELETE FROM MASTER_BANK_ACCOUNT WHERE LEDGER_ID = ?LEDGER_ID;

                                   DELETE FROM TDS_CREDTIORS_PROFILE WHERE LEDGER_ID = ?LEDGER_ID;

                                   DELETE FROM BUDGET_LEDGER WHERE LEDGER_ID = ?LEDGER_ID;

                                   DELETE FROM PROJECT_CATEGORY_LEDGER WHERE LEDGER_ID = ?LEDGER_ID;

                                   DELETE FROM HEADOFFICE_MAPPED_LEDGER WHERE LEDGER_ID = ?LEDGER_ID;

                                   DELETE FROM PROJECT_LEDGER WHERE LEDGER_ID = ?LEDGER_ID;

                                   DELETE FROM LEDGER_BALANCE WHERE LEDGER_ID = ?LEDGER_ID;

                                   DELETE FROM PROJECT_COSTCENTRE WHERE LEDGER_ID = ?LEDGER_ID;
                                   DELETE FROM PROJECT_LEDGER_APPLICABLE WHERE LEDGER_ID = ?LEDGER_ID;
                                   DELETE FROM PORTAL_CONGREGATION_LEDGER_MAP WHERE LEDGER_ID = ?LEDGER_ID;
                                   DELETE FROM PROJECT_CATEGORY_LEDGER WHERE LEDGER_ID = ?LEDGER_ID;
                                   DELETE FROM BRANCH_CONGREGATION_LEDGER_MAP WHERE LEDGER_ID = ?LEDGER_ID;
                                   DELETE FROM BRANCH_CONGREGATION_FIXEDASSET_DETAILS WHERE LEDGER_ID = ?LEDGER_ID;";
                        //PayRoll,Asset and Stock should not be executed unless those modules used by the client
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateBudgetGroupRecurringByLedgerName:
                    {
                        query = "UPDATE MASTER_LEDGER SET BUDGET_GROUP_ID=2;";
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateMysoreBudgetSubGroupRecurringByLedgerName:
                    {
                        query = "UPDATE MASTER_LEDGER SET BUDGET_GROUP_ID=1,  BUDGET_SUB_GROUP_ID=?BUDGET_SUB_GROUP_ID, SORT_ID=?SORT_ID WHERE LEDGER_NAME=?LEDGER_NAME;";
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateBudgetGroupDetails:
                    {
                        query = "UPDATE MASTER_LEDGER SET BUDGET_GROUP_ID=?BUDGET_GROUP_ID, BUDGET_SUB_GROUP_ID=?BUDGET_SUB_GROUP_ID, SORT_ID=?SORT_ID\n" +
                                 "WHERE LEDGER_ID IN (?LEDGER_ID)";
                        break;
                    }
                case SQLCommand.LedgerBank.GetSDBINMAuditorSkippedLedgerIds:
                    {
                        query = "SELECT GROUP_CONCAT(LEDGER_ID) AS LEDGER_ID_COLLECTION\n" +
                                    "FROM MASTER_LEDGER\n" +
                                    "WHERE LEDGER_NAME IN ('EPF Recovered / Remitted',\n" +
                                    "'ESI Recovered / Remitted',\n" +
                                    "'Domestic Staff Salary',\n" +
                                    "'Fund from NABFINS',\n" +
                                    "'Gifts and Mementos',\n" +
                                    "'University Recognition Expenses',\n" +
                                    "'Hair Cut &Toilet Articles',\n" +
                                    "'University Registration Expenses');";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchBankBranch:
                    {
                        query = @"SELECT MB.BANK, MB.BRANCH, CONCAT(CONCAT(MA.ACCOUNT_NUMBER,' - '),CONCAT(MB.BANK,' - '),MB.BRANCH) AS 'NAME'
                                    FROM MASTER_BANK_ACCOUNT MA
                                    LEFT JOIN MASTER_BANK MB
                                    ON MA.BANK_ID = MB.BANK_ID
                                    WHERE MA.LEDGER_ID =?LEDGER_ID
                                    ORDER BY MA.BANK_ACCOUNT_ID DESC";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchCashBankCurrencySymbolByLedger:
                    {
                        query = @"SELECT ML.CUR_COUNTRY_ID, IFNULL(MC.CURRENCY_SYMBOL, '') AS CURRENCY_SYMBOL
                                FROM MASTER_LEDGER ML INNER JOIN MASTER_COUNTRY MC ON MC.COUNTRY_ID = CUR_COUNTRY_ID
                                WHERE ML.LEDGER_ID IN (?LEDGER_ID) GROUP BY CUR_COUNTRY_ID;";
                        break;
                    }
            }
            return query;
        }


        private string GetCongregationLedgerSQL()
        {
            string query = "";
            SQLCommand.CongregationLedgers sqlCommandId = (SQLCommand.CongregationLedgers)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.CongregationLedgers.FetchCongregationLedgers:
                    {
                        query = "SELECT BCL.CON_NATURE_ID, BCL.CON_LEDGER_ID, BCL.CON_LEDGER_CODE,\n" +
                                    "CONCAT(BCL.CON_LEDGER_CODE, ' - ', BCL.CON_LEDGER_NAME, ' (', MA.NATURE, ')') AS CON_LEDGER_NAME\n" +
                                    "FROM BRANCH_CONGREGATION_LEDGER BCL\n" +
                                    "INNER JOIN MASTER_NATURE MA ON MA.NATURE_ID =  BCL.CON_NATURE_ID\n" +
                                    "ORDER BY BCL.CON_LEDGER_CODE";
                        break;
                    }
                case SQLCommand.CongregationLedgers.FetchLedgersMappedWithCongregationLedgers:
                    {
                        query = @"SELECT IF(IFNULL(BCLM.CON_LEDGER_ID, 0)>0, 1, 0) AS 'SELECT', MG.NATURE_ID, MG.GROUP_ID, MN.NATURE,
                                IFNULL(BCLM.CON_LEDGER_ID, 0) AS CON_LEDGER_ID,  
                                ML.LEDGER_ID, ML.LEDGER_CODE,
                                CASE
                                    WHEN ML.LEDGER_SUB_TYPE = 'BK' THEN
                                    CONCAT(CONCAT(ML.LEDGER_NAME, ' - '), CONCAT(BANK, ' - '), BRANCH)
                                    ELSE
                                    ML.LEDGER_NAME
                                END AS 'LEDGER_NAME',
                                CASE
                                    WHEN ML.LEDGER_SUB_TYPE = 'BK' THEN
                                    'Bank Accounts'
                                    ELSE
                                    CASE
                                    WHEN ML.LEDGER_SUB_TYPE = 'FD' THEN
                                        'Fixed Deposit'
                                    ELSE
                                        MG.LEDGER_GROUP
                                    END
                                END AS LEDGER_GROUP, ML.LEDGER_TYPE, ML.LEDGER_SUB_TYPE, MBA.BANK_ACCOUNT_ID,
                                ML.IS_TDS_LEDGER, ML.IS_COST_CENTER, ML.IS_BANK_INTEREST_LEDGER, ML.ACCESS_FLAG, ML.DATE_CLOSED,
                                IFNULL(BCLM.CON_OP_AMOUNT, 0) AS CON_OP_AMOUNT, 
                                IF(BCLM.CON_OP_TRANS_MODE IS NULL OR BCLM.CON_OP_TRANS_MODE='', IF((?CON_NATURE_ID = 2 OR ?CON_NATURE_ID=3), 'DR', 'CR'), BCLM.CON_OP_TRANS_MODE) AS CON_OP_TRANS_MODE
                                FROM MASTER_LEDGER ML
                                INNER JOIN MASTER_LEDGER_GROUP MG ON ML.GROUP_ID = MG.GROUP_ID
                                INNER JOIN MASTER_NATURE MN ON MN.NATURE_ID = MG.NATURE_ID
                                LEFT JOIN MASTER_BANK_ACCOUNT MBA ON ML.LEDGER_ID = MBA.LEDGER_ID
                                LEFT JOIN MASTER_BANK MB ON MB.BANK_ID = MBA.BANK_ID
                                LEFT JOIN BRANCH_CONGREGATION_LEDGER_MAP BCLM ON BCLM.LEDGER_ID = ML.LEDGER_ID
                                LEFT JOIN BRANCH_CONGREGATION_LEDGER BCL ON BCL.CON_LEDGER_ID = BCLM.CON_LEDGER_ID
                                WHERE ML.STATUS = 0";
                        break;
                    }
                case SQLCommand.CongregationLedgers.FetchCongregationFixedAssetDetails:
                    {
                        query = @"SELECT BCLM.CON_LEDGER_ID, BCLM.LEDGER_ID, BCL.CON_LEDGER_CODE, ML.LEDGER_CODE, ML.LEDGER_NAME,
                                    ((IFNULL(CON_OP_AMOUNT, 0) * IF(BCLM.CON_OP_TRANS_MODE = 'DR', 1, -1)) + IFNULL(BCFD.DEBIT_PREVIOUS, 0)) - IFNULL(BCFD.CREDIT_PREVIOUS, 0) AS CON_OP_AMOUNT,
                                    IFNULL(BCFD.DEBIT, 0) AS DEBIT, IFNULL(BCFD.CREDIT, 0) AS CREDIT,
                                    IFNULL(BCFD.DEBIT_PREVIOUS, 0) AS DEBIT_PREVIOUS, IFNULL(BCFD.CREDIT_PREVIOUS, 0) AS CREDIT_PREVIOUS,
                                    (((IFNULL(CON_OP_AMOUNT, 0) * IF(BCLM.CON_OP_TRANS_MODE = 'DR', 1, -1)) + IFNULL(BCFD.DEBIT_PREVIOUS, 0)) - IFNULL(BCFD.CREDIT_PREVIOUS, 0)) + (IFNULL(BCFD.DEBIT, 0) - IFNULL(BCFD.CREDIT, 0)) CON_CL_AMOUNT,
                                    IFNULL(VM.DEBIT, 0) AS FN_DEBIT, IFNULL(VM.CREDIT, 0) AS FN_CREDIT
                                    FROM BRANCH_CONGREGATION_LEDGER_MAP BCLM
                                    INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = BCLM.LEDGER_ID
                                    INNER JOIN BRANCH_CONGREGATION_LEDGER BCL ON BCL.CON_LEDGER_ID = BCLM.CON_LEDGER_ID

                                    LEFT JOIN (SELECT CON_LEDGER_ID, LEDGER_ID, 
                                         SUM( IF( ( (?DATE_FROM BETWEEN VOUCHER_DATE_FROM AND VOUCHER_DATE_TO) OR (?DATE_TO BETWEEN VOUCHER_DATE_FROM AND VOUCHER_DATE_TO) ),  IFNULL(DEBIT, 0), 0) ) AS DEBIT,  
                                         SUM( IF( ( (?DATE_FROM BETWEEN VOUCHER_DATE_FROM AND VOUCHER_DATE_TO) OR (?DATE_TO BETWEEN VOUCHER_DATE_FROM AND VOUCHER_DATE_TO) ),  IFNULL(CREDIT, 0), 0) ) AS CREDIT,
                                         SUM( IF( (VOUCHER_DATE_TO <= DATE_ADD(?YEAR_TO, INTERVAL -1 YEAR)), IFNULL(DEBIT, 0), 0) ) AS DEBIT_PREVIOUS, 
                                         SUM( IF( (VOUCHER_DATE_TO <= DATE_ADD(?YEAR_TO, INTERVAL -1 YEAR)), IFNULL(CREDIT, 0), 0) ) AS CREDIT_PREVIOUS 
                                         FROM BRANCH_CONGREGATION_FIXEDASSET_DETAILS WHERE VOUCHER_DATE_TO <= ?YEAR_TO GROUP BY CON_LEDGER_ID, LEDGER_ID) AS BCFD
                                            ON BCFD.CON_LEDGER_ID = BCLM.CON_LEDGER_ID AND BCFD.LEDGER_ID = BCLM.LEDGER_ID
                                    
                                    LEFT JOIN (SELECT VT.LEDGER_ID, SUM(IF(VT.TRANS_MODE = 'DR', VT.AMOUNT, 0)) AS DEBIT, SUM(IF(VT.TRANS_MODE = 'CR', VT.AMOUNT, 0)) AS CREDIT
                                               FROM VOUCHER_TRANS VT INNER JOIN VOUCHER_MASTER_TRANS VM ON VM.VOUCHER_ID = VT.VOUCHER_ID
                                               WHERE VM.STATUS =1 AND VM.VOUCHER_TYPE NOT IN ('CN') AND VM.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO GROUP BY VT.LEDGER_ID) AS VM
                                            ON VM.LEDGER_ID = BCLM.LEDGER_ID
                                    WHERE BCL.CON_LEDGER_CODE IN ('B', 'G') {AND ML.LEDGER_ID IN (?LEDGER_ID)}";

                        /*LEFT JOIN (SELECT CON_LEDGER_ID, LEDGER_ID, SUM(IFNULL(DEBIT, 0)) AS DEBIT, SUM(IFNULL(CREDIT, 0)) AS CREDIT
                                         FROM BRANCH_CONGREGATION_FIXEDASSET_DETAILS WHERE  VOUCHER_DATE_TO<= ?PREVIOUS_YEAR_TO  GROUP BY CON_LEDGER_ID, LEDGER_ID) AS PY_BCFD
                                            ON PY_BCFD.CON_LEDGER_ID = BCLM.CON_LEDGER_ID AND PY_BCFD.LEDGER_ID = BCLM.LEDGER_ID*/
                        break;
                    }
                case SQLCommand.CongregationLedgers.DeleteLedgersMappedWithCongregationByConLedgerId:
                    {
                        query = "DELETE FROM BRANCH_CONGREGATION_LEDGER_MAP WHERE CON_LEDGER_ID IN (?CON_LEDGER_ID)";
                        break;
                    }
                case SQLCommand.CongregationLedgers.InsertLedgersMappedWithCongregationLedger:
                    {
                        query = "INSERT BRANCH_CONGREGATION_LEDGER_MAP (CON_LEDGER_ID, LEDGER_ID, PROJECT_CATOGORY_GROUP_ID, CON_OP_AMOUNT, CON_OP_TRANS_MODE)\n" +
                                    "VALUES(?CON_LEDGER_ID, ?LEDGER_ID, ?PROJECT_CATOGORY_GROUP_ID, ?CON_OP_AMOUNT, ?CON_OP_TRANS_MODE)";
                        break;
                    }
                case SQLCommand.CongregationLedgers.InsertUpdateDefaultCongregationLedgers:
                    {
                        query = "INSERT INTO BRANCH_CONGREGATION_LEDGER (CON_LEDGER_ID, CON_LEDGER_CODE, CON_LEDGER_NAME,\n" +
                                "CON_PARENT_LEDGER_ID, CON_MAIN_PARENT_ID, CON_NATURE_ID) VALUES\n" +
                                "(1,'A','FINANCIAL STATUS',1,1,3),\n" +
                                "(2,'B','FIXED ASSETS',2,2,3),\n" +
                                "(3,'C','CREDIT- LOAN',3,3,4),\n" +
                                "(4,'D01','DEBTS - ASSET',4,4,3),\n" +
                                "(5,'D02','DEBTS - LIABILITIES',5,5,4),\n" +
                                "(6,'E','EXPENSES AND PAYMENTS',6,6,2),\n" +
                                "(7,'F','REVENUE',7,7,1),\n" +
                                "(8,'G','TOTAL DEPRECIATION',8,8,2);";
                        //query = "INSERT INTO BRANCH_CONGREGATION_LEDGER\n" +
                        //            "VALUES (1,'A','FINANCIAL STATUS',1,1), (2,'B','FIXED ASSETS',2,2),\n" +
                        //            "(3,'C','CREDIT- LOAN',3,3), (4,'D01','DEBTS - ASSET',4,4), (5,'D02','DEBTS - LIABILITIES',5,5),\n" +
                        //            "(6,'E','EXPENSES AND PAYMENTS',6,6), (7,'F','REVENUE',7,7), (8,'G','TOTAL DEPRECIATION',8,8);";
                        break;
                    }
                case SQLCommand.CongregationLedgers.InsertUpdateDefaultMappingWithCongregationLedgers:
                    {
                        query = "INSERT BRANCH_CONGREGATION_LEDGER_MAP SELECT 1, LEDGER_ID, 0, 0, 'DR'\n" +
                                "FROM MASTER_LEDGER ML\n" +
                                "INNER JOIN MASTER_LEDGER_GROUP LG ON LG.GROUP_ID = ML.GROUP_ID\n" +
                                "WHERE LG.GROUP_ID IN (12, 13, 14) ON DUPLICATE KEY UPDATE CON_LEDGER_ID = 1, LEDGER_ID = ML.LEDGER_ID, PROJECT_CATOGORY_GROUP_ID = 0;\n" +
                                "\n" + //For A
                                "INSERT BRANCH_CONGREGATION_LEDGER_MAP SELECT 2, LEDGER_ID, 0, 0, 'DR'\n" +
                                "FROM MASTER_LEDGER ML\n" +
                                "INNER JOIN MASTER_LEDGER_GROUP LG ON LG.GROUP_ID = ML.GROUP_ID\n" +
                                "WHERE LG.GROUP_ID IN (18) ON DUPLICATE KEY UPDATE CON_LEDGER_ID = 2, LEDGER_ID = ML.LEDGER_ID, PROJECT_CATOGORY_GROUP_ID = 0;\n" +
                                "\n" + //For B
                                "INSERT BRANCH_CONGREGATION_LEDGER_MAP SELECT 3, LEDGER_ID, 0, 0, 'CR'\n" +
                                "FROM MASTER_LEDGER ML INNER JOIN MASTER_LEDGER_GROUP LG ON LG.GROUP_ID = ML.GROUP_ID\n" +
                                "WHERE ML.LEDGER_NAME IN ('THIRD PARTY') ON DUPLICATE KEY UPDATE CON_LEDGER_ID = 3, LEDGER_ID = ML.LEDGER_ID, PROJECT_CATOGORY_GROUP_ID = 0;\n" +
                                "\n" + //For C
                                "INSERT BRANCH_CONGREGATION_LEDGER_MAP SELECT 4, LEDGER_ID, 0, 0, 'DR'\n" +
                                "FROM MASTER_LEDGER ML INNER JOIN MASTER_LEDGER_GROUP LG ON LG.GROUP_ID = ML.GROUP_ID\n" +
                                "WHERE ML.LEDGER_NAME IN ('AMOUNTS IN FD KEPT WITH THE PROVINCE') ON DUPLICATE KEY UPDATE CON_LEDGER_ID = 4, LEDGER_ID = ML.LEDGER_ID, PROJECT_CATOGORY_GROUP_ID = 0;\n" +
                                "\n" + //For D 01;
                                "INSERT BRANCH_CONGREGATION_LEDGER_MAP SELECT 5, LEDGER_ID, 0, 0, 'CR'\n" +
                                "FROM MASTER_LEDGER ML INNER JOIN MASTER_LEDGER_GROUP LG ON LG.GROUP_ID = ML.GROUP_ID\n" +
                                "WHERE ML.LEDGER_NAME IN ('PAYMENT TO BE MADE TO SUPPLIERS', 'PAYMENT TO BE MADE TO THIRD PARTIES') ON DUPLICATE KEY UPDATE CON_LEDGER_ID = 5, LEDGER_ID = ML.LEDGER_ID, PROJECT_CATOGORY_GROUP_ID = 0;\n" +
                                "\n" + //For D 02;
                                "INSERT BRANCH_CONGREGATION_LEDGER_MAP SELECT 6, LEDGER_ID, 0, 0 , 'DR'\n" +
                                "FROM MASTER_LEDGER ML INNER JOIN MASTER_LEDGER_GROUP LG ON LG.GROUP_ID = ML.GROUP_ID AND ML.IS_DEPRECIATION_LEDGER = 0\n" +
                                "WHERE LG.NATURE_ID IN (2) ON DUPLICATE KEY UPDATE CON_LEDGER_ID = 6, LEDGER_ID = ML.LEDGER_ID, PROJECT_CATOGORY_GROUP_ID = 0;\n" +
                                "\n" + //For E;
                                "INSERT BRANCH_CONGREGATION_LEDGER_MAP SELECT 7, LEDGER_ID, 0, 0, 'CR'\n" +
                                "FROM MASTER_LEDGER ML INNER JOIN MASTER_LEDGER_GROUP LG ON LG.GROUP_ID = ML.GROUP_ID\n" +
                                "WHERE LG.NATURE_ID IN (1) ON DUPLICATE KEY UPDATE CON_LEDGER_ID = 7, LEDGER_ID = ML.LEDGER_ID, PROJECT_CATOGORY_GROUP_ID = 0;\n" +
                                "\n" + //For F;
                                "INSERT BRANCH_CONGREGATION_LEDGER_MAP SELECT 8, LEDGER_ID, 0, 0, 'DR'\n" +
                                "FROM MASTER_LEDGER ML INNER JOIN MASTER_LEDGER_GROUP LG ON LG.GROUP_ID = ML.GROUP_ID\n" +
                                "WHERE ML.IS_DEPRECIATION_LEDGER=1 ON DUPLICATE KEY UPDATE CON_LEDGER_ID = 8, LEDGER_ID = ML.LEDGER_ID, PROJECT_CATOGORY_GROUP_ID = 0;";
                        //For F;
                        break;
                    }
                case SQLCommand.CongregationLedgers.DeleteCongregationFACurrentYearDetails:
                    {
                        query = "DELETE FROM BRANCH_CONGREGATION_FIXEDASSET_DETAILS WHERE VOUCHER_DATE_FROM =?YEAR_FROM AND VOUCHER_DATE_TO=?YEAR_TO";
                        break;
                    }
                case SQLCommand.CongregationLedgers.InsertCongregationFACurrentYearDetails:
                    {
                        query = "INSERT INTO BRANCH_CONGREGATION_FIXEDASSET_DETAILS (VOUCHER_DATE_FROM, VOUCHER_DATE_TO, CON_LEDGER_ID, LEDGER_ID, DEBIT, CREDIT)\n" +
                                "VALUES (?YEAR_FROM, ?YEAR_TO, ?CON_LEDGER_ID, ?LEDGER_ID, ?DEBIT, ?CREDIT)";
                        break;
                    }
            }
            return query;
        }
        #endregion LEDGER SQL
    }
}
