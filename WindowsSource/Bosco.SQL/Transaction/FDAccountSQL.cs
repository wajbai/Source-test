using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;
using Bosco.Utility;

namespace Bosco.SQL
{
    public class FDAccountSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.FDAccount).FullName)
            {
                query = GetFDAccount();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the In Kind Receid.
        /// </summary>
        /// <returns></returns>
        private string GetFDAccount()
        {
            string query = "";
            SQLCommand.FDAccount sqlCommandId = (SQLCommand.FDAccount)(dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.FDAccount.FetchLedgers:
                    {
                        query = "SELECT ML.LEDGER_ID, LEDGER_CODE, LEDGER_NAME, MP.PROJECT, MP.PROJECT_ID\n" +
                        "  FROM MASTER_LEDGER AS ML\n" +
                        " INNER JOIN PROJECT_LEDGER AS PL\n" +
                        "    ON ML.LEDGER_ID = PL.LEDGER_ID\n" +
                        " INNER JOIN MASTER_PROJECT AS MP\n" +
                        "    ON PL.PROJECT_ID = MP.PROJECT_ID\n" +
                        " WHERE ML.LEDGER_SUB_TYPE = 'FD' GROUP BY PL.LEDGER_ID";

                        break;
                    }
                case SQLCommand.FDAccount.FetchProjectByLedger:
                    {

                        query = "SELECT ML.LEDGER_ID, LEDGER_CODE, LEDGER_NAME, MP.PROJECT, MP.PROJECT_ID\n" +
                        "  FROM MASTER_LEDGER AS ML\n" +
                        " INNER JOIN PROJECT_LEDGER AS PL\n" +
                        "    ON ML.LEDGER_ID = PL.LEDGER_ID\n" +
                        " INNER JOIN MASTER_PROJECT AS MP\n" +
                        "    ON PL.PROJECT_ID = MP.PROJECT_ID\n" +
                        " WHERE ML.LEDGER_SUB_TYPE = 'FD'";

                        break;
                    }
                case SQLCommand.FDAccount.FetchAllProjectId:
                    {
                        query = "SELECT PROJECT_ID FROM MASTER_PROJECT";
                        break;
                    }
                case SQLCommand.FDAccount.FetchFDRegistersView:
                    {
                        query = "SELECT FDA.FD_ACCOUNT_ID, FDA.INVESTMENT_DATE, FDA.PROJECT_ID, FDA.CURRENCY_COUNTRY_ID,\n" +
                                "       FDA.PROJECT_ID,\n" +
                                "       IF(MLG.FD_INVESTMENT_TYPE_ID = " + (int)FDInvestmentType.MutualFund + ", NULL, IFNULL(FDRR.MATURITY_DATE, FDA.MATURED_ON)) AS MATURITY_DATE,\n" +
                                "       IF(FDA.FD_SCHEME = " + (int)FDScheme.Flexi + ",'" + FDScheme.Flexi.ToString() + "','" + FDScheme.Normal.ToString() + "') AS FD_SCHEME, \n" +    
                             // "       FDA.FD_ACCOUNT_NUMBER,\n" +
                                "       CONCAT(FDA.FD_ACCOUNT_NUMBER, IF((FR_RECEIPT_NO.RECEIPT_NO='' || FR_RECEIPT_NO.RECEIPT_NO IS NULL)  ,'',CONCAT(' (R: ',FR_RECEIPT_NO.RECEIPT_NO,')'))) AS FD_ACCOUNT_NUMBER, FDA.TRANS_TYPE,\n" +
                                "       CONCAT(MBK.BANK, ' (', MBK.BRANCH, ')') AS BANK,\n" +
                                "       MLG.LEDGER_NAME, IFNULL(MC.CURRENCY_NAME, '') AS CURRENCY_NAME,\n" +
                                "       MPR.PROJECT, FD.CLOSED_DATE, FD.VOUCHER_NO,\n" +
                                "       IFNULL(FDR.INTEREST_RATE, FDA.INTEREST_RATE) AS INTEREST_RATE,\n" +
                                "       FDA.AMOUNT + IFNULL(FDRO.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                                "       IFNULL(FDRO.WITHDRAWAL_AMOUNT, 0) AS PRINCIPLE_AMOUNT,\n" +
                                "       IFNULL(FDRO.REINVESTED_AMOUNT,0) + IFNULL(FDR.REINVESTED_AMOUNT,0) AS REINVESTED_AMOUNT,\n" + //On 24/11/2018 to include RE-invested amount
                                "       ROUND((IFNULL(FDRPOI.INTEREST_AMOUNT, 0) +\n" +
                                "             IFNULL(FDR.INTEREST_AMOUNT, 0)),\n" +
                                "             2) AS INTEREST_AMOUNT,\n" +
                                "       ROUND((IFNULL(FDRPOI.ACCUMULATED_INTEREST_AMOUNT, 0) +\n" +
                                "             IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0)),\n" +
                                "             2) AS ACCUMULATED_INTEREST_AMOUNT,\n" +
                                "       ROUND((IFNULL(FDRPOI.TDS_AMOUNT, 0) + IFNULL(FDR.TDS_AMOUNT, 0)),2) AS TDS_AMOUNT,\n" + // chinna (07.08.2017)
                                "       ROUND((IFNULL(FDRPOI.CHARGE_AMOUNT, 0) + IFNULL(FDR.CHARGE_AMOUNT, 0)),2) AS CHARGE_AMOUNT,\n" + //on 25/05/202, to show charge amount
                                "       FDA.AMOUNT + IFNULL(FDRO.REINVESTED_AMOUNT,0) + IFNULL(FDR.REINVESTED_AMOUNT,0) + \n" +
                                "       (IFNULL(FDRO.ACCUMULATED_INTEREST_AMOUNT, 0) +\n" +
                                "       IFNULL(FDRPOI.ACCUMULATED_INTEREST_AMOUNT, 0)) -\n" +
                                "       (IFNULL(FDRO.WITHDRAWAL_AMOUNT, 0) +\n" +
                                "       IFNULL(FDRPOI.WITHDRAWAL_AMOUNT, 0) +\n" +
                                "       IFNULL(FDRPO.WITHDRAWAL_AMOUNT, 0)) +\n" +
                                "       IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0) AS TOTAL_AMOUNT,\n" + //On 24/11/2018 to include RE-invested amount
                                "       IFNULL(FDR.WITHDRAWAL_AMOUNT, 0) AS WITHDRAWAL_AMOUNT,\n" +
                                "       FDA.AMOUNT + (IFNULL(FDRO.ACCUMULATED_INTEREST_AMOUNT, 0) +\n" +
                                "       IFNULL(FDRO.REINVESTED_AMOUNT,0) + IFNULL(FDR.REINVESTED_AMOUNT,0) +\n" + //On 24/11/2018 to include RE-invested amount
                                "       IFNULL(FDRPOI.ACCUMULATED_INTEREST_AMOUNT, 0)) -\n" +
                                "       (IFNULL(FDRO.WITHDRAWAL_AMOUNT, 0) +\n" +
                                "       IFNULL(FDRPOI.WITHDRAWAL_AMOUNT, 0) +\n" +
                                "       IFNULL(FDRPO.WITHDRAWAL_AMOUNT, 0)) +\n" +
                                "       IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                                "       IFNULL(FDR.WITHDRAWAL_AMOUNT, 0) AS BALANCE_AMOUNT,\n" +
                                "       IF(FDA.AMOUNT + (IFNULL(FDRO.ACCUMULATED_INTEREST_AMOUNT, 0) +\n" +
                                "       IFNULL(FDRO.REINVESTED_AMOUNT,0) + IFNULL(FDR.REINVESTED_AMOUNT,0) +\n" + //On 24/11/2018 to include RE-invested amount
                                "       IFNULL(FDRPOI.ACCUMULATED_INTEREST_AMOUNT, 0)) -\n" +
                                "       (IFNULL(FDRO.WITHDRAWAL_AMOUNT, 0) +\n" +
                                "       IFNULL(FDRPOI.WITHDRAWAL_AMOUNT, 0) +\n" +
                                "       IFNULL(FDRPO.WITHDRAWAL_AMOUNT, 0)) +\n" +
                                "       IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                                "       IFNULL(FDR.WITHDRAWAL_AMOUNT, 0)= 0,\n" +
                                "          'CLOSED',\n" +
                                "          'ACTIVE') AS CLOSING_STATUS\n" +
                                "  FROM FD_ACCOUNT AS FDA\n" +
                                "  LEFT JOIN (SELECT FD_ACCOUNT_ID,\n" +
                                "              MAX(MATURITY_DATE) AS MATURITY_DATE,\n" +
                                "              (RECEIPT_NO) AS RECEIPT_NO,\n" +
                                "              MAX(RENEWAL_DATE) AS RENEWAL_DATE,\n" +
                                "              SUM(IF(RENEWAL_TYPE = 'ACI', 0, INTEREST_AMOUNT)) AS INTEREST_AMOUNT,\n" +
                                "              SUM(IF(RENEWAL_TYPE = 'ACI', INTEREST_AMOUNT, 0) * IF(RENEWAL_TYPE ='ACI' AND FD_TRANS_MODE='CR', -1, 1)) AS ACCUMULATED_INTEREST_AMOUNT,\n" +
                                "              IFNULL(SUM(TDS_AMOUNT),0) AS TDS_AMOUNT,\n" + // chinna(07.08.2017)
                                "              SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT,\n" +
                                "              SUM(IFNULL(REINVESTED_AMOUNT,0)) AS REINVESTED_AMOUNT\n" + //On 24/11/2018 to include RE-invested amount
                                "              FROM FD_RENEWAL\n" +
                                "              WHERE STATUS = 1\n" +
                                "                AND FD_TYPE <> 'POI'\n" +
                                "                AND IS_DELETED = 1\n" +
                                "                AND RENEWAL_DATE < ?DATE_FROM\n" +
                                "              GROUP BY FD_ACCOUNT_ID HAVING MAX(FD_RENEWAL_ID)) AS FDRO\n" +
                                "       ON FDA.FD_ACCOUNT_ID = FDRO.FD_ACCOUNT_ID\n" +
                                "    LEFT JOIN (SELECT FD_ACCOUNT_ID,\n" +
                                "              MAX(MATURITY_DATE) AS MATURITY_DATE,\n" +
                                "              (RECEIPT_NO) AS RECEIPT_NO,\n" +
                                "              MAX(RENEWAL_DATE) AS RENEWAL_DATE,\n" +
                                "              SUM(IF(RENEWAL_TYPE = 'ACI', 0, INTEREST_AMOUNT)) AS INTEREST_AMOUNT,\n" +
                                "              SUM(IF(RENEWAL_TYPE = 'ACI', INTEREST_AMOUNT, 0) * IF(RENEWAL_TYPE ='ACI' AND FD_TRANS_MODE='CR', -1, 1) ) AS ACCUMULATED_INTEREST_AMOUNT,\n" +
                                "              IFNULL(SUM(TDS_AMOUNT),0) AS TDS_AMOUNT,\n" + // chinna(07.08.2017)
                                "              IFNULL(SUM(CHARGE_AMOUNT),0) AS CHARGE_AMOUNT,\n" + //25/05/2022
                                "              SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT\n" +
                                "              FROM FD_RENEWAL\n" +
                                "              WHERE STATUS = 1 AND FD_TYPE = 'POI' AND IS_DELETED = 1 AND RENEWAL_DATE <= ?DATE_TO\n" + //On 18/04/2024, MATURITY_DATE to have proper fd poi amount
                                "              GROUP BY FD_ACCOUNT_ID HAVING MAX(FD_RENEWAL_ID)) AS FDRPOI\n" +
                                "       ON FDA.FD_ACCOUNT_ID = FDRPOI.FD_ACCOUNT_ID\n" +
                                "    LEFT JOIN (SELECT FD_ACCOUNT_ID,\n" +
                                "              MAX(MATURITY_DATE) AS MATURITY_DATE,\n" +
                                "              (RECEIPT_NO) AS RECEIPT_NO,\n" +
                                "              MAX(RENEWAL_DATE) AS RENEWAL_DATE,\n" +
                                "              INTEREST_RATE,\n" +
                                "              SUM(IF(RENEWAL_TYPE = 'ACI', 0, INTEREST_AMOUNT)) AS INTEREST_AMOUNT,\n" +
                                "              SUM(IF(RENEWAL_TYPE = 'ACI', INTEREST_AMOUNT, 0) * IF(RENEWAL_TYPE ='ACI' AND FD_TRANS_MODE='CR', -1, 1)) AS ACCUMULATED_INTEREST_AMOUNT,\n" +
                                "              IFNULL(SUM(TDS_AMOUNT),0) AS TDS_AMOUNT,\n" + // chinna(07.08.2017)
                                "              SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT\n" +
                                "              FROM FD_RENEWAL\n" +
                                "              WHERE STATUS = 1 AND FD_TYPE = 'POI' AND IS_DELETED = 1 AND RENEWAL_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                "              GROUP BY FD_ACCOUNT_ID HAVING MAX(FD_RENEWAL_ID)\n" +
                                "              ORDER BY FD_ACCOUNT_ID DESC) AS FDRPO\n" +
                                "       ON FDA.FD_ACCOUNT_ID = FDRPO.FD_ACCOUNT_ID\n" +
                                "    LEFT JOIN (SELECT FD_ACCOUNT_ID,\n" +
                                "              MAX(MATURITY_DATE) AS MATURITY_DATE,\n" +
                                "              (RECEIPT_NO) AS RECEIPT_NO,\n" +
                                "              MAX(RENEWAL_DATE) AS RENEWAL_DATE,\n" +
                                "              INTEREST_RATE,\n" +
                                "              SUM(IF(RENEWAL_TYPE = 'ACI', 0, INTEREST_AMOUNT)) AS INTEREST_AMOUNT,\n" +
                                "              SUM(IF(RENEWAL_TYPE = 'ACI', INTEREST_AMOUNT, 0) * IF(RENEWAL_TYPE ='ACI' AND FD_TRANS_MODE='CR', -1, 1)) AS ACCUMULATED_INTEREST_AMOUNT,\n" +
                                "              IFNULL(SUM(TDS_AMOUNT),0) AS TDS_AMOUNT,\n" + // chinna(07.08.2017)
                                "              IFNULL(SUM(CHARGE_AMOUNT),0) AS CHARGE_AMOUNT,\n" + //25/05/2022
                                "              SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT,\n" +
                                "              SUM(IFNULL(REINVESTED_AMOUNT,0)) AS REINVESTED_AMOUNT\n" + //On 24/11/2018 to include RE-invested amount
                                "              FROM FD_RENEWAL\n" +
                                "              WHERE STATUS = 1 AND FD_TYPE <> 'POI' AND IS_DELETED = 1 AND RENEWAL_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                "              GROUP BY FD_ACCOUNT_ID HAVING MAX(FD_RENEWAL_ID)\n" +
                                "              ORDER BY FD_ACCOUNT_ID DESC) AS FDR\n" +
                                "       ON FDA.FD_ACCOUNT_ID = FDR.FD_ACCOUNT_ID\n" +
                                "    LEFT JOIN (SELECT FD_ACCOUNT_ID,\n" +
                                "              MAX(MATURITY_DATE) AS MATURITY_DATE,\n" +
                                "              (RECEIPT_NO) AS RECEIPT_NO,\n" +
                                "              MAX(RENEWAL_DATE) AS RENEWAL_DATE,\n" +
                                "              INTEREST_RATE,\n" +
                                "              SUM(IF(RENEWAL_TYPE = 'ACI', 0, INTEREST_AMOUNT)) AS INTEREST_AMOUNT,\n" +
                                "              SUM(IF(RENEWAL_TYPE = 'ACI', INTEREST_AMOUNT, 0) * IF(RENEWAL_TYPE ='ACI' AND FD_TRANS_MODE='CR', -1, 1)) AS ACCUMULATED_INTEREST_AMOUNT,\n" +
                                "              IFNULL(SUM(TDS_AMOUNT),0) AS TDS_AMOUNT,\n" + // chinna(07.08.2017)
                                "              SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT\n" +
                                "              FROM FD_RENEWAL\n" +
                                "              WHERE STATUS = 1 AND FD_TYPE <> 'POI' AND IS_DELETED = 1\n" +
                                "              AND (RENEWAL_DATE <=?DATE_FROM OR RENEWAL_DATE <=?DATE_TO)\n" +// "AND MATURITY_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                "              GROUP BY FD_ACCOUNT_ID HAVING MAX(FD_RENEWAL_ID)\n" +
                                "              ORDER BY FD_ACCOUNT_ID DESC) AS FDRR\n" +
                                "       ON FDA.FD_ACCOUNT_ID = FDRR.FD_ACCOUNT_ID\n" +
                                "    LEFT JOIN (SELECT FD_ACCOUNT_ID, CLOSED_DATE, VM.VOUCHER_NO\n" +
                                "              FROM FD_RENEWAL\n" +
                                "              LEFT JOIN VOUCHER_MASTER_TRANS VM ON VM.VOUCHER_ID = FD_VOUCHER_ID\n" +
                                "              WHERE FD_TYPE <> 'POI'\n" +
                                "              AND IS_DELETED = 1\n" +
                                "              AND FD_TYPE = 'WD' AND RENEWAL_TYPE='WDI'\n" +
                                "              AND CLOSED_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                "              GROUP BY FD_ACCOUNT_ID\n" +
                                "              ORDER BY FD_ACCOUNT_ID DESC) AS FD ON FDA.FD_ACCOUNT_ID = FD.FD_ACCOUNT_ID\n" +
                                "    LEFT JOIN (SELECT FR.FD_ACCOUNT_ID, IFNULL(FR.RECEIPT_NO,'') AS RECEIPT_NO\n" +
                                "              FROM FD_RENEWAL FR\n" +
                                "              INNER JOIN (SELECT FD_ACCOUNT_ID, MAX(RENEWAL_DATE) AS RENEWAL_DATE FROM FD_RENEWAL\n" +
                                "              WHERE STATUS =1 AND FD_TYPE='RN' AND (RENEWAL_DATE <=?DATE_FROM OR RENEWAL_DATE <= ?DATE_TO) GROUP BY FD_ACCOUNT_ID) FR1 ON FR1.FD_ACCOUNT_ID = FR.FD_ACCOUNT_ID\n" +
                                "              AND FR.RENEWAL_DATE = FR1.RENEWAL_DATE\n" +
                                "              WHERE FR.STATUS = 1 AND FD_TYPE = 'RN') AS FR_RECEIPT_NO ON FR_RECEIPT_NO.FD_ACCOUNT_ID = FDA.FD_ACCOUNT_ID\n" +
                                "     LEFT JOIN MASTER_BANK AS MBK ON FDA.BANK_ID = MBK.BANK_ID\n" +
                                "     LEFT JOIN MASTER_PROJECT MPR ON FDA.PROJECT_ID = MPR.PROJECT_ID\n" +
                                "     LEFT JOIN MASTER_LEDGER MLG ON FDA.LEDGER_ID = MLG.LEDGER_ID\n" +
                                "     LEFT JOIN MASTER_COUNTRY MC ON MC.COUNTRY_ID = FDA.CURRENCY_COUNTRY_ID\n" +
                                "     LEFT JOIN (SELECT PROJECT_ID FROM USER_PROJECT WHERE 1=1 { AND ROLE_ID=?USERROLE_ID} GROUP BY PROJECT_ID) UP ON UP.PROJECT_ID = MPR.PROJECT_ID\n" +
                                "     WHERE FDA.STATUS = 1\n" + // { AND UP.ROLE_ID=?USERROLE_ID }
                                "     AND FDA.INVESTMENT_DATE <= ?DATE_TO\n" +
                                "     {AND MPR.PROJECT_ID IN (?PROJECT_ID)} {AND MLG.FD_INVESTMENT_TYPE_ID=?FD_INVESTMENT_TYPE_ID}\n" +
                                "     AND FDA.AMOUNT + IFNULL(FDRO.REINVESTED_AMOUNT,0) + IFNULL(FDR.REINVESTED_AMOUNT,0) + IFNULL(FDRO.ACCUMULATED_INTEREST_AMOUNT, 0) +\n" +
                                "     IFNULL(FDRPOI.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                                "     (IFNULL(FDRO.WITHDRAWAL_AMOUNT, 0) +\n" +
                                "     IFNULL(FDRPOI.WITHDRAWAL_AMOUNT, 0)) <> 0\n" +
                                "     ORDER BY MATURITY_DATE ASC;";
                        break;
                    }
                case SQLCommand.FDAccount.FetchFDFullHistory:
                    {
                        query = @"SELECT FDH.*, @CL_BAL:= ((IFNULL(@CL_BAL, AMOUNT) + IF(RENEWAL_TYPE IN ('ACI'), ACCUMULATED_INTEREST_AMOUNT, 0) +
                                    IF(RENEWAL_TYPE ='RIN', REINVESTED_AMOUNT, 0)) -  IF(FD_TYPE = 'WD', WITHDRAWAL_AMOUNT,0)) AS CLOSING_BALANCE
                                    FROM (SELECT FD.FD_ACCOUNT_ID, FD.PROJECT_ID, FD.INVESTMENT_DATE, FR.FD_RENEWAL_ID, FD.FD_ACCOUNT_NUMBER, FD.TRANS_TYPE, FD.FD_SCHEME, FR.INTEREST_TYPE,
                                    IFNULL(MP.PROJECT, '') AS PROJECT, FML.LEDGER_NAME AS FD_LEDGER, BML.LEDGER_NAME AS INVESTED_BANK_LEDGER, IFNULL(FR.RECEIPT_NO, '') AS RECEIPT_NO,
                                    FR.RENEWAL_DATE, RIML.LEDGER_NAME AS INTEREST_LEDGER_NAME, RBML.LEDGER_NAME AS RENEWAL_BANK_LEDGER_NAME, MC.CURRENCY_NAME,
                                    FD.AMOUNT AS AMOUNT, IF((FR.RENEWAL_TYPE IN ('IRI') OR FR.FD_TYPE IN ('WD')), FR.INTEREST_AMOUNT, 0) AS RECEIVED_INTEREST_AMOUNT,
                                    (IF(FR.RENEWAL_TYPE IN ('ACI'), FR.INTEREST_AMOUNT, 0) * IF(FR.RENEWAL_TYPE ='ACI' AND FR.FD_TYPE IN ('RN', 'POI') AND FR.FD_TRANS_MODE='CR', -1, 1)) AS ACCUMULATED_INTEREST_AMOUNT,
                                    IF(FR.RENEWAL_TYPE ='RIN', REINVESTED_AMOUNT, 0) AS REINVESTED_AMOUNT, IF(FR.FD_TYPE = 'WD', FR.WITHDRAWAL_AMOUNT,0) AS WITHDRAWAL_AMOUNT,
                                    IFNULL(FR.CHARGE_AMOUNT, 0) AS CHARGE_AMOUNT,
                                    CASE WHEN FR.RENEWAL_TYPE ='IRI' AND FR.FD_TYPE = 'RN' THEN 'Renewed - Interest Received'
                                            WHEN FR.RENEWAL_TYPE ='ACI' AND FR.FD_TYPE IN ('RN') AND FR.FD_TRANS_MODE = 'CR' THEN 'Renewed - Fixed Deposit Adjustment'
                                            WHEN FR.RENEWAL_TYPE ='ACI' AND FR.FD_TYPE IN ('POI') AND FR.FD_TRANS_MODE = 'CR' THEN 'Post Interest - Fixed Deposit Adjustment'
                                            WHEN FR.RENEWAL_TYPE ='ACI' AND FR.FD_TYPE = 'RN' THEN 'Renewed - Interest Accumulated'
                                            WHEN FR.RENEWAL_TYPE ='IRI' AND FR.FD_TYPE = 'POI' THEN 'Post Interest - Interest Received'
                                            WHEN FR.RENEWAL_TYPE ='ACI' AND FR.FD_TYPE = 'POI' THEN 'Post Interest - Interest Accumulated'
                                            WHEN FR.RENEWAL_TYPE ='RIN' THEN 'Re-Invested Amount'
                                            WHEN FR.RENEWAL_TYPE ='PWD' THEN 'Partial Withdrawal'
                                            WHEN FR.RENEWAL_TYPE ='WDI' THEN 'Fully Withdrawal' ELSE ''
                                            END AS RENEWAL_TYPE_NAME, FR.TDS_AMOUNT, FR.RENEWAL_TYPE, FR.FD_TYPE, 
                                    @TEMP_MAT_DATE:= CAST( (CASE WHEN @TEMP_MAT_DATE IS NULL AND FR.FD_TYPE NOT IN ('RN') THEN FD.MATURED_ON
                                                    WHEN FR.FD_TYPE ='RN' THEN FR.MATURITY_DATE ELSE @TEMP_MAT_DATE END) AS DATETIME) AS TEMP_MAT_DATE, 
                                    IF(FML.FD_INVESTMENT_TYPE_ID=" + (Int32)FDInvestmentType.MutualFund + @", NULL, CAST(@TEMP_MAT_DATE AS DATETIME)) AS MATURITY_DATE,
                                    IFNULL(MIT.INVESTMENT_TYPE, '') AS INVESTMENT_TYPE
                                    FROM FD_ACCOUNT FD
                                    LEFT JOIN FD_RENEWAL FR ON FR.FD_ACCOUNT_ID = FD.FD_ACCOUNT_ID AND FR.STATUS =1
                                    LEFT JOIN MASTER_LEDGER FML ON FML.LEDGER_ID = FD.LEDGER_ID
                                    LEFT JOIN MASTER_LEDGER BML ON BML.LEDGER_ID = FD.BANK_LEDGER_ID
                                    LEFT JOIN MASTER_LEDGER RIML ON RIML.LEDGER_ID = FR.INTEREST_LEDGER_ID
                                    LEFT JOIN MASTER_LEDGER RBML ON RBML.LEDGER_ID = FR.BANK_LEDGER_ID
                                    LEFT JOIN MASTER_INVESTMENT_TYPE MIT ON MIT.INVESTMENT_TYPE_ID = FML.FD_INVESTMENT_TYPE_ID
                                    LEFT JOIN MASTER_COUNTRY MC ON MC.COUNTRY_ID = FD.CURRENCY_COUNTRY_ID 
                                    LEFT JOIN MASTER_PROJECT MP ON MP.PROJECT_ID = FD.PROJECT_ID, (SELECT @TEMP_MAT_DATE := NULL) AS M, (SELECT @CL_BAL:=NULL) AS CL_BAL
                                    WHERE FD.STATUS =1 AND FR.STATUS =1 {AND FD.FD_ACCOUNT_ID=?FD_ACCOUNT_ID AND  FR.FD_ACCOUNT_ID=?FD_ACCOUNT_ID} 
                                    ORDER BY FR.RENEWAL_DATE) AS FDH, (SELECT @CL_BAL:=NULL) AS CL_BAL";
                        break;
                    }
                case SQLCommand.FDAccount.FetchRecentFDRenewal:
                    {
                        query = @"SELECT * FROM (SELECT FR.FD_ACCOUNT_ID, FR.FD_RENEWAL_ID, FR.FD_VOUCHER_ID, FR.FD_INTEREST_VOUCHER_ID, FR.RENEWAL_DATE AS VOUCHER_DATE
                                FROM FD_RENEWAL FR
                                WHERE FR.STATUS=1 AND FR.FD_ACCOUNT_ID = ?FD_ACCOUNT_ID ORDER BY FR.RENEWAL_DATE DESC LIMIT 1) AS FR
                                UNION ALL
                                SELECT FD.FD_ACCOUNT_ID, 0 AS FD_RENEWAL_ID, 0 AS FD_VOUCHER_ID, 0 AS FD_INTEREST_VOUCHER_ID, FD.INVESTMENT_DATE AS VOUCHER_DATE
                                FROM FD_ACCOUNT FD WHERE FD.STATUS = 1 AND FD.FD_ACCOUNT_ID = ?FD_ACCOUNT_ID";
                        break;
                    }
                case SQLCommand.FDAccount.FetchActualFDAccountByVoucherId:
                    {
                        query = @"SELECT FR.FD_ACCOUNT_ID, FR.FD_RENEWAL_ID FROM FD_RENEWAL FR
                                WHERE FR.STATUS = 1 AND (FR.FD_VOUCHER_ID = ?FD_VOUCHER_ID || FR.FD_INTEREST_VOUCHER_ID = ?FD_VOUCHER_ID)
                                UNION
                                SELECT FD.FD_ACCOUNT_ID, 0 AS FD_RENEWAL_ID
                                FROM FD_ACCOUNT FD WHERE FD.STATUS = 1 AND FD.FD_VOUCHER_ID = ?FD_VOUCHER_ID;";
                        break;
                    }
                case SQLCommand.FDAccount.DeleteProjectLedger:
                    {
                        query = "DELETE FROM PROJECT_LEDGER\n" +
                                " WHERE LEDGER_ID = ?LEDGER_ID";
                        break;
                    }
                case SQLCommand.FDAccount.Add:
                    {
                        query = "INSERT INTO FD_ACCOUNT\n" +
                        "  (FD_ACCOUNT_NUMBER,\n" +
                        "   FD_SCHEME,\n" +
                        "   PROJECT_ID,\n" +
                        "   LEDGER_ID,\n" +
                        "   BANK_ID,\n" +
                        "   BANK_LEDGER_ID,\n" +
                        "   FD_VOUCHER_ID,\n" +
                        "   AMOUNT,\n" +
                        "   TRANS_TYPE,\n" +
                        "   RECEIPT_NO,\n" +
                        "   ACCOUNT_HOLDER,\n" +
                        "   INVESTMENT_DATE,\n" +
                        "   TRANS_MODE,\n" +
                        "   INTEREST_TYPE,\n" +
                        "   MATURED_ON,\n" +
                        "   MF_FOLIO_NO, MF_SCHEME_NAME, MF_NAV_PER_UNIT, MF_NO_OF_UNITS, MF_MODE_OF_HOLDING,\n" +
                        "   INTEREST_RATE,\n" +
                        "   INTEREST_AMOUNT,FD_SUB_TYPES,\n" +
                        "   CONTRIBUTION_AMOUNT, CURRENCY_COUNTRY_ID, EXCHANGE_RATE, ACTUAL_AMOUNT, IS_MULTI_CURRENCY,\n"+
                        "   STATUS,NOTES, EXPECTED_MATURITY_VALUE, EXPECTED_INTEREST_VALUE)\n" +
                        "VALUES\n" +
                        "  (?FD_ACCOUNT_NUMBER,\n" +
                        "   ?FD_SCHEME,\n" +
                        "   ?PROJECT_ID,\n" +
                        "   ?LEDGER_ID,\n" +
                        "   ?BANK_ID,\n" +
                        "   ?BANK_LEDGER_ID,\n" +
                        "   ?FD_VOUCHER_ID,\n" +
                        "   ?AMOUNT,\n" +
                        "   ?TRANS_TYPE,\n" +
                        "   ?RECEIPT_NO,\n" +
                        "   ?ACCOUNT_HOLDER,\n" +
                        "   ?INVESTMENT_DATE,\n" +
                        "   ?TRANS_MODE,\n" +
                        "   ?INTEREST_TYPE,\n" +
                        "   ?MATURED_ON,\n" +
                        "   ?MF_FOLIO_NO, ?MF_SCHEME_NAME, ?MF_NAV_PER_UNIT, ?MF_NO_OF_UNITS, ?MF_MODE_OF_HOLDING,\n" +
                        "   ?INTEREST_RATE,\n" +
                        "   ?INTEREST_AMOUNT,?FD_SUB_TYPES,\n" +
                        "   ?CONTRIBUTION_AMOUNT, ?CURRENCY_COUNTRY_ID, ?EXCHANGE_RATE, ?ACTUAL_AMOUNT, ?IS_MULTI_CURRENCY,\n" +
                        "   ?STATUS,?NOTES, ?EXPECTED_MATURITY_VALUE, ?EXPECTED_INTEREST_VALUE)";
                        break;
                    }
                case SQLCommand.FDAccount.Update:
                    {
                        query = "UPDATE FD_ACCOUNT\n" +
                        "   SET FD_ACCOUNT_NUMBER = ?FD_ACCOUNT_NUMBER,\n" +
                        "                           FD_SCHEME = ?FD_SCHEME,\n" +
                        "                           PROJECT_ID = ?PROJECT_ID,\n" +
                        "                           LEDGER_ID = ?LEDGER_ID,\n" +
                        "                           BANK_ID = ?BANK_ID,\n" +
                        "                           BANK_LEDGER_ID=?BANK_LEDGER_ID,\n" +
                        "                           FD_VOUCHER_ID=?FD_VOUCHER_ID,\n" +
                        "                           AMOUNT = ?AMOUNT,\n" +
                        "                           TRANS_TYPE = ?TRANS_TYPE,\n" +
                        "                           RECEIPT_NO = ?RECEIPT_NO,\n" +
                        "                           ACCOUNT_HOLDER = ?ACCOUNT_HOLDER,\n" +
                        "                           INVESTMENT_DATE = ?INVESTMENT_DATE,\n" +
                        "                           TRANS_MODE = ?TRANS_MODE,\n" +
                        "                           INTEREST_TYPE=?INTEREST_TYPE,\n" +
                        "                           MATURED_ON = ?MATURED_ON,\n" +
                        "                           INTEREST_RATE = ?INTEREST_RATE,\n" +
                        "                           INTEREST_AMOUNT = ?INTEREST_AMOUNT,\n" +
                        "                           FD_SUB_TYPES=?FD_SUB_TYPES,\n" +
                        "                           CONTRIBUTION_AMOUNT = ?CONTRIBUTION_AMOUNT, CURRENCY_COUNTRY_ID=?CURRENCY_COUNTRY_ID, EXCHANGE_RATE=?EXCHANGE_RATE,\n"+
                        "                           ACTUAL_AMOUNT=?ACTUAL_AMOUNT, IS_MULTI_CURRENCY=?IS_MULTI_CURRENCY,\n" +
                        "                           STATUS = ?STATUS,\n" +
                        "                           NOTES = ?NOTES,\n" +
                        "                           MF_FOLIO_NO=?MF_FOLIO_NO, MF_SCHEME_NAME=?MF_SCHEME_NAME, MF_NAV_PER_UNIT=?MF_NAV_PER_UNIT,\n" +
                        "                           MF_NO_OF_UNITS=?MF_NO_OF_UNITS, MF_MODE_OF_HOLDING=?MF_MODE_OF_HOLDING,\n" +
                        "                           EXPECTED_MATURITY_VALUE = ?EXPECTED_MATURITY_VALUE,\n" +
                        "                           EXPECTED_INTEREST_VALUE = ?EXPECTED_INTEREST_VALUE\n" +
                        "                           WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID";
                        break;
                    }
                case SQLCommand.FDAccount.Delete:
                    {
                        query = "DELETE FROM FD_ACCOUNT WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID";
                        break;
                    }
                case SQLCommand.FDAccount.FetchLedgerCurBalance:
                    {
                        query = "SELECT SUM(AMOUNT) AS 'AMOUNT', TRANS_MODE, TRANS_FLAG\n" +
                        "  FROM LEDGER_BALANCE\n" +
                        " WHERE LEDGER_ID =?LEDGER_ID  AND PROJECT_ID=?PROJECT_ID AND TRANS_FLAG='OP'";
                        break;
                    }
                case SQLCommand.FDAccount.Fetch:
                    {
                        query = "SELECT FDA.FD_ACCOUNT_ID, IFNULL(VM.VOUCHER_NO,'') AS VOUCHER_NO,\n" +
                               "       FDA.PROJECT_ID,\n" +
                               "       FDA.LEDGER_ID,\n" +
                               "       FDA.FD_VOUCHER_ID,\n" +
                               "       FDA.BANK_ID,FDA.BANK_LEDGER_ID,\n" +
                               "       FDA.TRANS_TYPE,FDA.INTEREST_RATE,\n" +
                               "       IF(FDA.FD_SCHEME = " + (int)FDScheme.Flexi + ",'" + FDScheme.Flexi.ToString() + "','" + FDScheme.Normal.ToString() + "') AS FD_SCHEME, FDA.FD_SCHEME AS FD_SCHEME_ID,\n" +
                               "       FDA.AMOUNT AS 'PRINCIPAL_AMOUNT', (FDA.AMOUNT *IF(FDA.IS_MULTI_CURRENCY=1, FDA.EXCHANGE_RATE, 1)) AS 'CURRENCY_PRINCIPAL_AMOUNT', \n" +
                            // 09.10.2017 .. CHINNA
                               "       IFNULL(FDR.EXPECTED_MATURITY_VALUE, FDA.EXPECTED_MATURITY_VALUE) AS EXPECTED_MATURITY_VALUE,\n" +
                               "       IFNULL(FDR.EXPECTED_INTEREST_VALUE, FDA.EXPECTED_INTEREST_VALUE) AS EXPECTED_INTEREST_VALUE,\n" +
                               "       FDA.INVESTMENT_DATE,FDA.INTEREST_TYPE, IFNULL(MIT.INVESTMENT_TYPE, '') AS INVESTMENT_TYPE,\n" +
                             //"       CASE \n"+
                             //"       WHEN FDR.FD_TYPE='RN'\n"+
                             //"       THEN\n"+
                             //"       IFNULL(FDR.MATURITY_DATE, FDA.MATURED_ON)\n"+
                             //"      ELSE\n"+
                             //"      IFNULL(FDR.MATURITY_DATE, FDA.MATURED_ON)\n"+
                             //"       END AS  MATURITY_DATE,\n"+
                               "       IF(MLG.FD_INVESTMENT_TYPE_ID = "+ (int)FDInvestmentType.MutualFund +", NULL,  IFNULL(FDR.MATURITY_DATE, FDA.MATURED_ON)) AS MATURITY_DATE,\n" +
                               //"       FDA.FD_ACCOUNT_NUMBER,\n" +
                               "       CONCAT(FDA.FD_ACCOUNT_NUMBER, IF((FR_RECEIPT_NO.RECEIPT_NO='' || FR_RECEIPT_NO.RECEIPT_NO IS NULL)  ,'',CONCAT(' (R: ',FR_RECEIPT_NO.RECEIPT_NO,')'))) AS FD_ACCOUNT_NUMBER,\n" +
                               "       CONCAT(MBK.BANK, ' (', MBK.BRANCH, ')') AS BANK,\n" +
                               "       MLG.LEDGER_NAME,\n" +
                               "       MPR.PROJECT, IFNULL(FDA.MF_SCHEME_NAME, '') AS MF_SCHEME_NAME,\n" +
                            //"       ROUND(FDA.AMOUNT + IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0),2) -ROUND( IFNULL(FDR.WITHDRAWAL_AMOUNT, 0),2) AS AMOUNT, " +
                               "       FDA.AMOUNT + IFNULL(FDR.REINVESTED_AMOUNT,0) + IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0) + IFNULL(FDRPO.ACCUMULATED_INTEREST_AMOUNT, 0) - IFNULL(FDR.WITHDRAWAL_AMOUNT, 0) AS AMOUNT, " +
                            //  "        IFNULL(FDR.WITHDRAWAL_AMOUNT, 0) AS AMOUNT, " +
                               "       FDA.INTEREST_AMOUNT,\n" +
                               "       IFNULL(FDR.REINVESTED_AMOUNT,0) AS REINVESTED_AMOUNT,\n" +
                               "       IF(ROUND(FDA.AMOUNT + IFNULL(FDR.REINVESTED_AMOUNT,0)+ IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0) + IFNULL(FDRPO.ACCUMULATED_INTEREST_AMOUNT, 0),2) -\n" +
                               "         ROUND(IFNULL(FDR.WITHDRAWAL_AMOUNT, 0) + IFNULL(FDRPO.WITHDRAWAL_AMOUNT, 0),2) = 0,\n" +
                               "          'Closed',\n" +
                               "          'Active') AS CLOSING_STATUS,\n" +
                               "       FDA.NOTES,\n" +
                            //"     CASE WHEN FDR.RENEWAL_TYPE='ACI' THEN\n" +
                            //"     'Accumulated Interest'\n" +
                            //"  WHEN FDR.RENEWAL_TYPE='IRI' THEN\n" +
                            //" 'Interest Received' END as 'RENEWAL_TYPE',\n" +
                               "       FDR.RENEWAL_TYPE, MLG.FD_INVESTMENT_TYPE_ID, MC.CURRENCY_NAME, \n" +
                               "       FDA.STATUS\n" +
                               "  FROM FD_ACCOUNT AS FDA\n" +
                               "  LEFT JOIN VOUCHER_MASTER_TRANS VM ON VM.VOUCHER_ID = FDA.FD_VOUCHER_ID" +
                               "  LEFT JOIN MASTER_COUNTRY MC ON MC.COUNTRY_ID = FDA.CURRENCY_COUNTRY_ID" +
                               "  LEFT JOIN (SELECT FD_RENEWAL.FD_ACCOUNT_ID, FD_TYPE," +
                               "                   RENEWAL_TYPE, \n" +
                               "                     (SELECT EXPECTED_MATURITY_VALUE FROM FD_RENEWAL FDR WHERE STATUS =1 AND FDR.FD_RENEWAL_ID\n" +
                               "                            = (SELECT MAX(FD_RENEWAL_ID) FROM FD_RENEWAL FDR1 WHERE STATUS =1 AND FDR1.FD_ACCOUNT_ID = FD_RENEWAL.FD_ACCOUNT_ID)\n" +
                               "                     ) AS EXPECTED_MATURITY_VALUE,\n" +
                               "                     (SELECT EXPECTED_INTEREST_VALUE FROM FD_RENEWAL FDR WHERE STATUS =1 AND FDR.FD_RENEWAL_ID\n" +
                               "                            = (SELECT MAX(FD_RENEWAL_ID) FROM FD_RENEWAL FDR1 WHERE STATUS =1 AND FDR1.FD_ACCOUNT_ID = FD_RENEWAL.FD_ACCOUNT_ID)\n" +
                               "                     ) AS EXPECTED_INTEREST_VALUE,\n" +
                               "                     SUM(REINVESTED_AMOUNT) AS REINVESTED_AMOUNT,\n" +
                               "                     MAX(MATURITY_DATE) AS MATURITY_DATE,\n" +
                               "                     MAX(RENEWAL_DATE) AS RENEWAL_DATE,\n" +
                               "                     INTEREST_RATE,BANK_LEDGER_ID,\n" +
                               "                     SUM(IF(RENEWAL_TYPE = 'ACI', 0, INTEREST_AMOUNT)) AS INTEREST_AMOUNT,\n" +
                               "                     SUM(IF(RENEWAL_TYPE = 'ACI', INTEREST_AMOUNT, 0) * IF(RENEWAL_TYPE ='ACI' AND FD_TRANS_MODE='CR', -1, 1) )  AS ACCUMULATED_INTEREST_AMOUNT,\n" +
                               "                     SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT\n" +
                               "                FROM FD_RENEWAL AS FD_RENEWAL \n" +
                               "               WHERE STATUS = 1 AND FD_TYPE<>'POI'\n" +
                               "               GROUP BY FD_ACCOUNT_ID HAVING MAX(FD_RENEWAL_ID)) AS FDR\n" +
                               "    ON FDA.FD_ACCOUNT_ID = FDR.FD_ACCOUNT_ID\n" +

                                "  LEFT JOIN (SELECT FD_ACCOUNT_ID, FD_TYPE,\n" +
                               "                    RENEWAL_TYPE,\n" +
                               "                    MAX(MATURITY_DATE) AS MATURITY_DATE,\n" +
                               "                    MAX(RENEWAL_DATE) AS RENEWAL_DATE,\n" +
                               "                    INTEREST_RATE,BANK_LEDGER_ID,\n" +
                               "                    SUM(IF(RENEWAL_TYPE = 'ACI', 0, INTEREST_AMOUNT)) AS INTEREST_AMOUNT,\n" +
                               "                    SUM(IF(RENEWAL_TYPE = 'ACI', INTEREST_AMOUNT, 0) * IF(RENEWAL_TYPE ='ACI' AND FD_TRANS_MODE='CR', -1, 1)) AS ACCUMULATED_INTEREST_AMOUNT,\n" +
                               "                    SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT\n" +
                               "               FROM FD_RENEWAL\n" +
                               "              WHERE STATUS = 1 AND FD_TYPE='POI' \n" +
                               "              GROUP BY FD_ACCOUNT_ID) AS FDRPO\n" +
                               "    ON FDA.FD_ACCOUNT_ID = FDRPO.FD_ACCOUNT_ID\n" +
                               "   LEFT JOIN (SELECT FR.FD_ACCOUNT_ID, IFNULL(FR.RECEIPT_NO,'') AS RECEIPT_NO\n" +
                               "               FROM FD_RENEWAL FR\n" +
                               "               INNER JOIN (SELECT FD_ACCOUNT_ID, MAX(RENEWAL_DATE) AS RENEWAL_DATE FROM FD_RENEWAL\n" +
                               "               WHERE STATUS =1 AND FD_TYPE='RN' GROUP BY FD_ACCOUNT_ID) FR1 ON FR1.FD_ACCOUNT_ID = FR.FD_ACCOUNT_ID\n" +
                               "               AND FR.RENEWAL_DATE = FR1.RENEWAL_DATE\n" +
                               "               WHERE FR.STATUS = 1 AND FD_TYPE = 'RN') AS FR_RECEIPT_NO ON FR_RECEIPT_NO.FD_ACCOUNT_ID = FDA.FD_ACCOUNT_ID\n" +
                               "  LEFT JOIN MASTER_BANK AS MBK ON FDA.BANK_ID = MBK.BANK_ID\n" +
                               "  LEFT JOIN MASTER_PROJECT MPR ON FDA.PROJECT_ID = MPR.PROJECT_ID\n" +
                               "  LEFT JOIN MASTER_LEDGER MLG ON FDA.LEDGER_ID = MLG.LEDGER_ID\n" +
                               "  LEFT JOIN MASTER_INVESTMENT_TYPE MIT ON MIT.INVESTMENT_TYPE_ID = MLG.FD_INVESTMENT_TYPE_ID\n" +
                            //" WHERE FDA.STATUS = 1 \n" +
                            //"   AND FDA.FD_STATUS = 1\n" +
                            //"   AND FDA.TRANS_TYPE IN (?TRANS_TYPE) {AND MPR.PROJECT_ID IN(?PROJECT_ID)}";
                               " WHERE  FDA.STATUS = 1 AND " +
                               " FDA.TRANS_TYPE IN (?TRANS_TYPE) {AND FDA.FD_SCHEME =?FD_SCHEME} {AND MPR.PROJECT_ID IN(?PROJECT_ID)}";

                        //query = "SELECT FDA.FD_ACCOUNT_ID,\n" +
                        //       "       FDA.PROJECT_ID,\n" +
                        //       "       FDA.LEDGER_ID,\n" +
                        //       "       FDA.FD_VOUCHER_ID,\n" +
                        //       "       FDA.BANK_ID,FDA.BANK_LEDGER_ID,\n" +
                        //       "       FDA.TRANS_TYPE,FDA.INTEREST_RATE,\n" +
                        //       "       FDA.AMOUNT AS 'PRINCIPAL_AMOUNT',\n" +
                        //    // 09.10.2017 .. CHINNA
                        //       "       IFNULL(FDR.EXPECTED_MATURITY_VALUE, FDA.EXPECTED_MATURITY_VALUE) AS EXPECTED_MATURITY_VALUE,\n" +
                        //       "       FDA.INVESTMENT_DATE,FDA.INTEREST_TYPE,\n" +
                        //    //"       CASE \n"+
                        //    //"       WHEN FDR.FD_TYPE='RN'\n"+
                        //    //"       THEN\n"+
                        //    //"       IFNULL(FDR.MATURITY_DATE, FDA.MATURED_ON)\n"+
                        //    //"      ELSE\n"+
                        //    //"      IFNULL(FDR.MATURITY_DATE, FDA.MATURED_ON)\n"+
                        //    //"       END AS  MATURITY_DATE,\n"+
                        //       "       IFNULL(FDR.MATURITY_DATE, FDA.MATURED_ON) AS MATURITY_DATE,\n" +
                        //       "       FDA.FD_ACCOUNT_NUMBER,\n" +
                        //       "       CONCAT(MBK.BANK, ' (', MBK.BRANCH, ')') AS BANK,\n" +
                        //       "       MLG.LEDGER_NAME,\n" +
                        //       "       MPR.PROJECT,\n" +
                        //    //"       ROUND(FDA.AMOUNT + IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0),2) -ROUND( IFNULL(FDR.WITHDRAWAL_AMOUNT, 0),2) AS AMOUNT, " +
                        //       "       FDA.AMOUNT + IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0) + IFNULL(FDRPO.ACCUMULATED_INTEREST_AMOUNT, 0) - IFNULL(FDR.WITHDRAWAL_AMOUNT, 0) AS AMOUNT, " +
                        //    //  "        IFNULL(FDR.WITHDRAWAL_AMOUNT, 0) AS AMOUNT, " +
                        //       "       FDA.INTEREST_AMOUNT,\n" +
                        //       "       IF(ROUND(FDA.AMOUNT + IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0) + IFNULL(FDRPO.ACCUMULATED_INTEREST_AMOUNT, 0),2) -\n" +
                        //       "         ROUND(IFNULL(FDR.WITHDRAWAL_AMOUNT, 0) + IFNULL(FDRPO.WITHDRAWAL_AMOUNT, 0),2) = 0,\n" +
                        //       "          'Closed',\n" +
                        //       "          'Active') AS CLOSING_STATUS,\n" +
                        //       "       FDA.NOTES,\n" +
                        //    //"     CASE WHEN FDR.RENEWAL_TYPE='ACI' THEN\n" +
                        //    //"     'Accumulated Interest'\n" +
                        //    //"  WHEN FDR.RENEWAL_TYPE='IRI' THEN\n" +
                        //    //" 'Interest Received' END as 'RENEWAL_TYPE',\n" +
                        //       "       FDR.RENEWAL_TYPE,\n" +
                        //       "       FDA.STATUS\n" +
                        //       "  FROM FD_ACCOUNT AS FDA\n" +
                        //       "\n" +
                        //       "  LEFT JOIN (SELECT FD_ACCOUNT_ID, FD_TYPE,\n" +
                        //       "                    RENEWAL_TYPE,\n" +
                        //       "                    MAX(MATURITY_DATE) AS MATURITY_DATE,\n" +
                        //       "                    EXPECTED_MATURITY_VALUE AS EXPECTED_MATURITY_VALUE,\n" +
                        //       "                    MAX(RENEWAL_DATE) AS RENEWAL_DATE,\n" +
                        //       "                    INTEREST_RATE,BANK_LEDGER_ID,\n" +
                        //       "                    SUM(IF(RENEWAL_TYPE = 'ACI', 0, INTEREST_AMOUNT)) AS INTEREST_AMOUNT,\n" +
                        //       "                    SUM(IF(RENEWAL_TYPE = 'ACI', INTEREST_AMOUNT, 0)) AS ACCUMULATED_INTEREST_AMOUNT,\n" +
                        //       "                    SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT\n" +
                        //       "               FROM FD_RENEWAL\n" +
                        //    //"              WHERE STATUS = 1 \n" +
                        //       "              WHERE STATUS = 1 AND FD_TYPE<>'POI' \n" +
                        //       "              GROUP BY FD_ACCOUNT_ID) AS FDR\n" +
                        //       "    ON FDA.FD_ACCOUNT_ID = FDR.FD_ACCOUNT_ID\n" +

                        //        "  LEFT JOIN (SELECT FD_ACCOUNT_ID, FD_TYPE,\n" +
                        //       "                    RENEWAL_TYPE,\n" +
                        //       "                    MAX(MATURITY_DATE) AS MATURITY_DATE,\n" +
                        //       "                    MAX(RENEWAL_DATE) AS RENEWAL_DATE,\n" +
                        //       "                    INTEREST_RATE,BANK_LEDGER_ID,\n" +
                        //       "                    SUM(IF(RENEWAL_TYPE = 'ACI', 0, INTEREST_AMOUNT)) AS INTEREST_AMOUNT,\n" +
                        //       "                    SUM(IF(RENEWAL_TYPE = 'ACI', INTEREST_AMOUNT, 0)) AS ACCUMULATED_INTEREST_AMOUNT,\n" +
                        //       "                    SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT\n" +
                        //       "               FROM FD_RENEWAL\n" +
                        //       "              WHERE STATUS = 1 AND FD_TYPE='POI' \n" +
                        //       "              GROUP BY FD_ACCOUNT_ID) AS FDRPO\n" +
                        //       "    ON FDA.FD_ACCOUNT_ID = FDRPO.FD_ACCOUNT_ID\n" +

                        //       "  LEFT JOIN MASTER_BANK AS MBK\n" +
                        //       "    ON FDA.BANK_ID = MBK.BANK_ID\n" +
                        //       "  LEFT JOIN MASTER_PROJECT MPR\n" +
                        //       "    ON FDA.PROJECT_ID = MPR.PROJECT_ID\n" +
                        //       "  LEFT JOIN MASTER_LEDGER MLG\n" +
                        //       "    ON FDA.LEDGER_ID = MLG.LEDGER_ID\n" +
                        //    //" WHERE FDA.STATUS = 1 \n" +
                        //    //"   AND FDA.FD_STATUS = 1\n" +
                        //    //"   AND FDA.TRANS_TYPE IN (?TRANS_TYPE) {AND MPR.PROJECT_ID IN(?PROJECT_ID)}";
                        //       " WHERE  FDA.STATUS = 1 AND " +
                        //       " FDA.TRANS_TYPE IN (?TRANS_TYPE) {AND MPR.PROJECT_ID IN(?PROJECT_ID)}";
                        break;
                    }
                case SQLCommand.FDAccount.FetchByLedgerId:
                    {
                        query = @"SELECT FD_ACCOUNT_ID, IFNULL(ML.FD_INVESTMENT_TYPE_ID,0) AS FD_INVESTMENT_TYPE_ID  
                                    FROM FD_ACCOUNT AS FD 
                                    INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = FD.LEDGER_ID
                                    WHERE FD.LEDGER_ID = ?LEDGER_ID AND FD.STATUS = 1";
                        break;
                    }
                case SQLCommand.FDAccount.FetchFDById:
                    {
                        query = "SELECT FD.LEDGER_ID,FD.FD_ACCOUNT_ID,\n" +
                                "       FD.PROJECT_ID,\n" +
                                "       FD.BANK_ID,\n" +
                                "       FD.BANK_LEDGER_ID,\n" +
                                "       FD.FD_VOUCHER_ID,\n" +
                                "       VMS.VOUCHER_NO,\n" +
                                "       MP.PROJECT,\n" +
                                "       FD.INVESTMENT_DATE,\n" +
                                "       MATURED_ON,\n" +
                                "       FD.FD_ACCOUNT_NUMBER, \n" +
                                "       FD.FD_SCHEME,\n" +
                                "       ML.LEDGER_CODE,\n" +
                                "       ML.LEDGER_NAME AS 'FD_LEDGER_NAME',\n" +
                                "       FD.AMOUNT,\n" +
                                "       FD.INTEREST_RATE,\n" +
                                "       FD.INTEREST_AMOUNT,\n" +
                                "       FD.TRANS_MODE,FD.INTEREST_TYPE,\n" +
                                "       FD.TRANS_TYPE,\n" +
                                "       FD.RECEIPT_NO,\n" +
                                "       IFNULL(FD.MF_FOLIO_NO,'') AS MF_FOLIO_NO, IFNULL(FD.MF_SCHEME_NAME, '') AS MF_SCHEME_NAME, \n" +
                                "       IFNULL(FD.MF_NAV_PER_UNIT, 0) AS MF_NAV_PER_UNIT, IFNULL(FD.MF_NO_OF_UNITS,0) AS MF_NO_OF_UNITS, IFNULL(FD.MF_MODE_OF_HOLDING,0) AS MF_MODE_OF_HOLDING, \n" +
                                "       FD.NOTES, ML.FD_INVESTMENT_TYPE_ID, FD.IS_MULTI_CURRENCY, FD.CURRENCY_COUNTRY_ID, FD.CONTRIBUTION_AMOUNT, FD.EXCHANGE_RATE, FD.ACTUAL_AMOUNT, \n" +
                                "       FD.EXPECTED_MATURITY_VALUE, FD.EXPECTED_INTEREST_VALUE,\n" +
                                "       FD.ACCOUNT_HOLDER,\n" +
                                "       CONCAT(MB.BRANCH, ' / ', MB.BANK) AS 'BANK'\n" +
                                " FROM FD_ACCOUNT AS FD\n" +
                                " INNER JOIN MASTER_LEDGER AS ML ON FD.LEDGER_ID = ML.LEDGER_ID\n" +
                                " INNER JOIN MASTER_PROJECT AS MP ON FD.PROJECT_ID = MP.PROJECT_ID\n" +
                                " LEFT JOIN VOUCHER_MASTER_TRANS AS VMS ON FD.FD_VOUCHER_ID=VMS.VOUCHER_ID\n" +
                                " INNER JOIN MASTER_BANK AS MB ON FD.BANK_ID = MB.BANK_ID\n" +
                                " WHERE FD.FD_ACCOUNT_ID = ?FD_ACCOUNT_ID ";
                        break;
                    }

                case SQLCommand.FDAccount.FetchLedgerByProject:
                    {
                        query = "SELECT ML.LEDGER_ID, IFNULL(ML.FD_INVESTMENT_TYPE_ID, 0) AS FD_INVESTMENT_TYPE_ID,\n" +
                                "  ML.LEDGER_NAME, IFNULL(MIT.INVESTMENT_TYPE, '') AS INVESTMENT_TYPE, IFNULL(ML.CUR_COUNTRY_ID,0) AS CUR_COUNTRY_ID, ML.OP_EXCHANGE_RATE\n" +
                                "  FROM MASTER_LEDGER AS ML\n" +
                                "  INNER JOIN PROJECT_LEDGER AS PL ON ML.LEDGER_ID = PL.LEDGER_ID\n" +
                                "  LEFT JOIN MASTER_INVESTMENT_TYPE MIT ON MIT.INVESTMENT_TYPE_ID = ML.FD_INVESTMENT_TYPE_ID\n" +
                                "  WHERE PROJECT_ID = ?PROJECT_ID AND ML.GROUP_ID=14 AND ML.LEDGER_SUB_TYPE='FD'\n"+
                                "  {AND (ML.DATE_CLOSED IS NULL OR ML.DATE_CLOSED >= ?DATE_CLOSED) } ";
                        break;
                    }
                case SQLCommand.FDAccount.FetchProjectId:
                    {
                        query = "SELECT TRANS_TYPE FROM FD_ACCOUNT WHERE PROJECT_ID=?PROJECT_ID AND LEDGER_ID=?LEDGER_ID AND STATUS NOT IN(0) ORDER BY FD_ACCOUNT_ID ASC";
                        break;
                    }
                case SQLCommand.FDAccount.FetchLedgerBalance:
                    {
                        query = "SELECT IFNULL(SUM(AMOUNT),0) AS 'AMOUNT',TRANS_MODE FROM FD_ACCOUNT WHERE PROJECT_ID=?PROJECT_ID AND LEDGER_ID=?LEDGER_ID AND TRANS_TYPE IN('OP') AND STATUS=1 ";
                        break;
                    }
                case SQLCommand.FDAccount.DeleteFDAcountDetails:
                    {
                        query = "UPDATE FD_ACCOUNT SET STATUS=0 WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID";
                        break;
                    }

                case SQLCommand.FDAccount.FetchFDRenewalById:
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
                               //"       CASE WHEN RENEWAL_TYPE='ACI' THEN\n" +
                               //"       'Accumulated Interest'\n" +
                               //"        WHEN RENEWAL_TYPE='IRI' THEN\n" +
                               //"       'Interest Received' END as 'RENEWAL_TYPE',\n" + 
                               //         Expected Maturity Value
                                "    RENEWAL_TYPE,VMT.NARRATION,\n" +
                                "    EXPECTED_MATURITY_VALUE AS 'EXPECTED_MATURITY_VALUE', EXPECTED_INTEREST_VALUE, FR.FD_TRANS_MODE\n" +
                               //" (SELECT FR1.EXPECTED_MATURITY_VALUE FROM FD_RENEWAL FR1 WHERE FR1.FD_RENEWAL_ID < FR.FD_RENEWAL_ID" +
                               //" AND FR1.STATUS =1 AND FR1.IS_DELETED =1 AND FR1.RENEWAL_TYPE = FR.RENEWAL_TYPE AND FR1.FD_TYPE =FR.FD_TYPE" +
                               //" AND FR1.FD_ACCOUNT_ID = FR.FD_ACCOUNT_ID ORDER BY FR1.FD_RENEWAL_ID DESC LIMIT 1) AS EXPECTED_MATURITY_VALUE" +
                                " FROM FD_RENEWAL AS FR\n" +
                                " LEFT JOIN VOUCHER_MASTER_TRANS VMT ON FR.FD_VOUCHER_ID=VMT.VOUCHER_ID\n" +
                                " WHERE FR.STATUS =1 AND FR.IS_DELETED=1 AND FR.RENEWAL_TYPE NOT IN('WDI') AND FD_TYPE IN ('RN') AND FD_ACCOUNT_ID IN(?FD_ACCOUNT_ID)\n"+
                                " ORDER BY MATURITY_DATE DESC";

                        break;
                    }
                case SQLCommand.FDAccount.FetchFDHistoryByFDId: 
                    {
                        query = "SELECT FR.FD_ACCOUNT_ID, FR.FD_RENEWAL_ID, VMT.VOUCHER_NO, FR.FD_VOUCHER_ID, FR.FD_INTEREST_VOUCHER_ID,\n" +
                                " FR.INTEREST_LEDGER_ID, FR.BANK_LEDGER_ID, FR.RENEWAL_DATE, RECEIPT_NO,\n" +
                                " INTEREST_RATE, FR.INTEREST_TYPE, FR.INTEREST_AMOUNT, FR.TDS_AMOUNT, MATURITY_DATE,\n" +
                                " CASE WHEN FR.RENEWAL_TYPE = 'PWD' THEN FR.WITHDRAWAL_AMOUNT\n" +
                                "   WHEN RENEWAL_TYPE='WDI' THEN FR.WITHDRAWAL_AMOUNT\n" +
                                "   WHEN FR.RENEWAL_TYPE='RIN' THEN FR.REINVESTED_AMOUNT END as AMOUNT,\n" +
                                " CASE WHEN FR.RENEWAL_TYPE='ACI' THEN CONCAT('Accumulated Interest', IF(FR.FD_TYPE='POI', ' (Post Interest)','')) \n" +
                                "   WHEN FR.RENEWAL_TYPE='IRI' THEN CONCAT('Interest Received', IF(FR.FD_TYPE='POI', ' (Post Interest)','')) \n" +
                                "   WHEN FR.RENEWAL_TYPE='RIN' THEN 'Re-Invested'\n" +
                                "   WHEN FR.RENEWAL_TYPE = 'PWD' THEN 'Re-Invested'\n" +
                                "   WHEN RENEWAL_TYPE='WDI' THEN 'Withdrawal' END as RENEWAL_TYPE,\n" +
                                " VMT.NARRATION, FR.EXPECTED_MATURITY_VALUE\n" +
                                " FROM FD_RENEWAL AS FR\n" +
                                " LEFT JOIN VOUCHER_MASTER_TRANS  VMT ON FR.FD_VOUCHER_ID=VMT.VOUCHER_ID\n" +
                                " WHERE FR.STATUS =1 AND FR.IS_DELETED=1 AND FD_ACCOUNT_ID IN(?FD_ACCOUNT_ID) ORDER BY FR.RENEWAL_DATE DESC";
                        break;
                    }
                case SQLCommand.FDAccount.FetchFDPostInterestDetailsById:
                    {
                        query = "SELECT FD_RENEWAL_ID,VMT.VOUCHER_NO,FR.FD_VOUCHER_ID,FR.FD_INTEREST_VOUCHER_ID,\n" +
                                "   FD_ACCOUNT_ID,\n" +
                                "   RENEWAL_DATE,\n" +
                                "   RECEIPT_NO,\n" +
                                "   INTEREST_RATE,FR.INTEREST_TYPE,\n" +
                                "   FR.INTEREST_AMOUNT,\n" +
                                "   FR.TDS_AMOUNT,\n" +
                                "   FR.EXPECTED_MATURITY_VALUE, FR.EXPECTED_INTEREST_VALUE, \n" +
                                "   MATURITY_DATE,\n" +
                                "   INTEREST_LEDGER_ID,\n" +
                                "   BANK_LEDGER_ID,\n" +
                              //"  CASE WHEN RENEWAL_TYPE='ACI' THEN\n" +
                              //"       'Accumulated Interest'\n" +
                              //"        WHEN RENEWAL_TYPE='IRI' THEN\n" +
                              //"       'Interest Received' END AS 'RENEWAL_TYPE',\n" +
                                "    RENEWAL_TYPE, VMT.NARRATION, FR.FD_TRANS_MODE\n" +
                                " FROM FD_RENEWAL AS FR\n" +
                                " LEFT JOIN VOUCHER_MASTER_TRANS  VMT ON\n" +
                                " FR.FD_VOUCHER_ID=VMT.VOUCHER_ID\n" +
                                " WHERE FR.STATUS =1 AND FR.IS_DELETED=1 AND FR.RENEWAL_TYPE NOT IN('WDI') AND FD_TYPE IN ('POI') AND FD_ACCOUNT_ID IN(?FD_ACCOUNT_ID) ORDER BY MATURITY_DATE DESC";

                        break;
                    }
                case SQLCommand.FDAccount.GetLastFDRenewalDate:
                    {
                        query = @" SELECT RENEWAL_DATE,MATURITY_DATE,RECEIPT_NO,FD_ACCOUNT_ID,INTEREST_AMOUNT,INTEREST_RATE,RENEWAL_TYPE,INTEREST_TYPE,EXPECTED_MATURITY_VALUE FROM
                                    FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID AND FD_TYPE IN ('RN') AND RENEWAL_TYPE NOT IN('WDI') AND IS_DELETED=1 AND STATUS=1 ORDER BY RENEWAL_DATE ASC";
                        break;
                    }
                case SQLCommand.FDAccount.GetMaturityValue:
                    {
                        query = @"SELECT FDA.FD_ACCOUNT_ID, IF(LastFDR.FD_RENEWAL_ID IS NULL,
                                    FDA.EXPECTED_MATURITY_VALUE, FDR1.EXPECTED_MATURITY_VALUE) AS EXPECTED_MATURITY_VALUE
                                    FROM FD_ACCOUNT FDA
                                      LEFT JOIN (SELECT FDR.FD_ACCOUNT_ID, MAX(FDR.FD_RENEWAL_ID) AS FD_RENEWAL_ID FROM FD_RENEWAL FDR
                                                WHERE FDR.FD_RENEWAL_ID <?FD_RENEWAL_ID AND FDR.STATUS=1 AND IS_DELETED =1 GROUP BY FDR.FD_ACCOUNT_ID) AS LastFDR
                                      ON LastFDR.FD_ACCOUNT_ID = FDA.FD_ACCOUNT_ID
                                      LEFT JOIN FD_RENEWAL FDR1
                                      ON FDR1.FD_RENEWAL_ID = LastFDR.FD_RENEWAL_ID
                                    WHERE FDA.FD_ACCOUNT_ID =?FD_ACCOUNT_ID;";

                        // ----------     This is done by Chinna     --------
                        //                        query = @" SELECT IFNULL(FDR.EXPECTED_MATURITY_VALUE, FDA.EXPECTED_MATURITY_VALUE) AS EXPECTED_MATURITY_VALUE,
                        //                                               FDA.INVESTMENT_DATE,
                        //                                               FDA.STATUS
                        //                                          FROM FD_ACCOUNT AS FDA
                        //                                          LEFT JOIN (SELECT T.FD_ACCOUNT_ID,
                        //                                                            T.FD_TYPE,
                        //                                                            T.RENEWAL_TYPE,
                        //                                                            T.EXPECTED_MATURITY_VALUE
                        //                                                       FROM (SELECT FD_ACCOUNT_ID,
                        //                                                                    FD_TYPE,
                        //                                                                    RENEWAL_TYPE,
                        //                                                                    (SELECT FR1.EXPECTED_MATURITY_VALUE
                        //                                                                       FROM FD_RENEWAL FR1
                        //                                                                      WHERE FR1.FD_RENEWAL_ID < FR.FD_RENEWAL_ID
                        //                                                                        AND FR1.STATUS = 1
                        //                                                                        AND FR1.IS_DELETED = 1
                        //                                                                        AND FR1.RENEWAL_TYPE = FR.RENEWAL_TYPE
                        //                                                                        AND FR1.FD_TYPE = FR.FD_TYPE
                        //                                                                        AND FR1.FD_ACCOUNT_ID = FR.FD_ACCOUNT_ID
                        //                                                                        AND FR1.FD_ACCOUNT_ID = ?FD_ACCOUNT_ID
                        //                                                                        AND FR.FD_RENEWAL_ID =?FD_RENEWAL_ID
                        //                                                                      ORDER BY FR1.FD_RENEWAL_ID DESC LIMIT 1) AS EXPECTED_MATURITY_VALUE
                        //                                                               FROM FD_RENEWAL AS FR
                        //                                                               LEFT JOIN VOUCHER_MASTER_TRANS VMT
                        //                                                                 ON FR.FD_VOUCHER_ID = VMT.VOUCHER_ID
                        //                                                              WHERE FR.STATUS = 1
                        //                                                                AND FR.IS_DELETED = 1
                        //                                                                AND FR.RENEWAL_TYPE NOT IN ('WDI')
                        //                                                                AND FD_TYPE IN ('RN')
                        //                                                                AND FD_ACCOUNT_ID IN (?FD_ACCOUNT_ID)
                        //                                                                AND FR.FD_RENEWAL_ID IN(?FD_RENEWAL_ID)
                        //                                                              ORDER BY MATURITY_DATE DESC) AS T
                        //                                                      WHERE T.EXPECTED_MATURITY_VALUE IS NOT NULL) AS FDR
                        //                                            ON FDA.FD_ACCOUNT_ID = FDR.FD_ACCOUNT_ID
                        //                                          LEFT JOIN MASTER_BANK AS MBK
                        //                                            ON FDA.BANK_ID = MBK.BANK_ID
                        //                                          LEFT JOIN MASTER_PROJECT MPR
                        //                                            ON FDA.PROJECT_ID = MPR.PROJECT_ID
                        //                                          LEFT JOIN MASTER_LEDGER MLG
                        //                                            ON FDA.LEDGER_ID = MLG.LEDGER_ID
                        //                                         WHERE FDA.FD_ACCOUNT_ID =?FD_ACCOUNT_ID
                        //                                           AND FDA.STATUS = 1";

                        break;
                    }
                case SQLCommand.FDAccount.FetchAccumulatedAmount:
                    {
                        query = @"SELECT IFNULL(SUM(INTEREST_AMOUNT),0) AS 'AMOUNT' FROM FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID 
                                     AND RENEWAL_TYPE='ACI' AND FD_TRANS_MODE = 'DR' AND STATUS=1 AND IS_DELETED=1 ";
                        break;
                    }
                case SQLCommand.FDAccount.FetchRenewalByRenewalId:
                    {

                        query = "SELECT FDR.FD_RENEWAL_ID,FDR.FD_INTEREST_VOUCHER_ID,\n" +
                        "       FDR.FD_ACCOUNT_ID,\n" +
                        "       FDA.PROJECT_ID,\n" +
                        "       MP.PROJECT,\n" +
                        "       FDA.FD_ACCOUNT_NUMBER,\n" +
                        "       FDA.LEDGER_ID,\n" +
                        "       ML.LEDGER_NAME,\n" +
                        "       FDA.BANK_ID,\n" +
                        "       FDA.RECEIPT_NO,\n" +
                            //  "       FDA.INTEREST_AMOUNT    AS 'STARTING_INT_AMOUNT',\n" +
                            // "       FDA.INTEREST_RATE      AS 'STARTING_INT_RATE',\n" +
                        "       FDR.INTEREST_LEDGER_ID,\n" +
                        "       FDR.BANK_LEDGER_ID,\n" +
                        "       FDR.INTEREST_AMOUNT ,\n" +
                        "       FDR.PRINICIPAL_AMOUNT,\n" +
                        "       FDR.INTEREST_RATE ,\n" +
                        "       FDR.RENEWAL_DATE,\n" +
                        "       FDR.MATURITY_DATE,FDR.RENEWAL_TYPE,FDA.NOTES\n" +
                        "  FROM FD_ACCOUNT AS FDA\n" +
                        " INNER JOIN FD_RENEWAL AS FDR\n" +
                        "    ON FDR.FD_ACCOUNT_ID = FDA.FD_ACCOUNT_ID\n" +
                        " INNER JOIN MASTER_PROJECT AS MP\n" +
                        "    ON MP.PROJECT_ID = FDA.PROJECT_ID\n" +
                        " INNER JOIN MASTER_LEDGER AS ML\n" +
                        "    ON ML.LEDGER_ID = FDA.LEDGER_ID\n" +
                        " WHERE FDR.FD_RENEWAL_ID = ?FD_RENEWAL_ID AND FDA.IS_DELETED=1 AND FDA.STATUS=1";

                        break;
                    }
                case SQLCommand.FDAccount.UpdateFDStatus:
                    {
                        query = "UPDATE FD_ACCOUNT SET FD_STATUS=0 WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID";
                        break;
                    }
                case SQLCommand.FDAccount.UpdateFDScheme:
                    {
                        query = "UPDATE FD_ACCOUNT SET FD_SCHEME = ?FD_SCHEME WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID";
                        break;
                    }
                case SQLCommand.FDAccount.FetchVoucherId:
                    {
                        query = "SELECT FD_VOUCHER_ID,FD_INTEREST_VOUCHER_ID,RENEWAL_TYPE FROM FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID AND RENEWAL_TYPE NOT IN('WDI') AND STATUS                                  NOT IN (0) AND IS_DELETED=1";
                        break;
                    }
                case SQLCommand.FDAccount.FetchAccountIdByVoucherId:
                    {
                        //                        query = @"SELECT FR.FD_ACCOUNT_ID,PROJECT_ID,FD_STATUS,FR.RENEWAL_TYPE  FROM  FD_ACCOUNT FA
                        //                                                            LEFT JOIN FD_RENEWAL FR
                        //                                                            ON FA.FD_ACCOUNT_ID=FR.FD_ACCOUNT_ID  WHERE FR.FD_INTEREST_VOUCHER_ID=?FD_INTEREST_VOUCHER_ID || FR.FD_VOUCHER_ID=?FD_INTEREST_VOUCHER_ID";
                        //                        query = @"SELECT FA.FD_ACCOUNT_ID,PROJECT_ID,
                        //                                IF(FA.AMOUNT-T.WITHDRAWAL_AMOUNT<0,0,1)AS FD_STATUS,
                        //                                IFNULL(FR.RENEWAL_TYPE,'IN') AS RENEWAL_TYPE
                        //                                FROM  FD_ACCOUNT FA
                        //                                LEFT JOIN FD_RENEWAL FR
                        //                                ON FA.FD_ACCOUNT_ID=FR.FD_ACCOUNT_ID
                        //                                LEFT JOIN (SELECT FD_ACCOUNT_ID,SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT FROM FD_RENEWAL WHERE IS_DELETED=1 AND STATUS=1 GROUP BY FD_ACCOUNT_ID) AS T
                        //                                ON T.FD_ACCOUNT_ID=FA.FD_ACCOUNT_ID
                        //                                WHERE FR.FD_INTEREST_VOUCHER_ID=?FD_INTEREST_VOUCHER_ID || FR.FD_VOUCHER_ID=?FD_INTEREST_VOUCHER_ID || FA.FD_VOUCHER_ID=?FD_INTEREST_VOUCHER_ID
                        //                                AND FR.IS_DELETED=1 AND FR.STATUS=1";
                        //*******changed by sugan--to route the post interest record*********************************************************************************************
                        query = @"SELECT FA.FD_ACCOUNT_ID, IFNULL(FR.FD_RENEWAL_ID, 0 ) AS FD_RENEWAL_ID, PROJECT_ID,
                                IF(FA.AMOUNT-T.WITHDRAWAL_AMOUNT<0,0,1)AS FD_STATUS,
                                IFNULL(FR.RENEWAL_TYPE,'IN') AS RENEWAL_TYPE,FR.FD_TYPE,IF(FR1.FD_VOUCHER_ID IS NOT NULL,'RIN', 'IN') AS 'TRANS_TYPE', FR.FD_TRANS_MODE
                                FROM  FD_ACCOUNT FA
                                LEFT JOIN FD_RENEWAL FR ON FA.FD_ACCOUNT_ID=FR.FD_ACCOUNT_ID
                                LEFT JOIN (SELECT FD_ACCOUNT_ID,SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT FROM FD_RENEWAL WHERE IS_DELETED=1 AND STATUS=1 GROUP BY FD_ACCOUNT_ID) AS T
                                            ON T.FD_ACCOUNT_ID=FA.FD_ACCOUNT_ID
                                LEFT JOIN FD_RENEWAL FR1 ON FR1.FD_ACCOUNT_ID = FA.FD_ACCOUNT_ID AND FR1.FD_VOUCHER_ID =?FD_INTEREST_VOUCHER_ID
                                WHERE FR.FD_INTEREST_VOUCHER_ID=?FD_INTEREST_VOUCHER_ID || FR.FD_VOUCHER_ID=?FD_INTEREST_VOUCHER_ID || FA.FD_VOUCHER_ID=?FD_INTEREST_VOUCHER_ID
                                AND FR.IS_DELETED=1 AND FR.STATUS=1";

                        break;
                    }
                case SQLCommand.FDAccount.FetchPhysicalAccountIdbyVoucherId:
                    {
                        query = @"SELECT FA.FD_ACCOUNT_ID,PROJECT_ID,
                                IF(FA.AMOUNT-T.WITHDRAWAL_AMOUNT<0,0,1)AS FD_STATUS,
                                IFNULL(FR.RENEWAL_TYPE,'IN') AS RENEWAL_TYPE
                                FROM  FD_ACCOUNT FA
                                LEFT JOIN FD_RENEWAL FR
                                ON FA.FD_ACCOUNT_ID=FR.FD_ACCOUNT_ID
                                LEFT JOIN (SELECT FD_ACCOUNT_ID,SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT FROM FD_RENEWAL WHERE IS_DELETED=0 AND STATUS=0 GROUP BY FD_ACCOUNT_ID) AS T
                                ON T.FD_ACCOUNT_ID=FA.FD_ACCOUNT_ID
                                WHERE FR.FD_INTEREST_VOUCHER_ID=?FD_INTEREST_VOUCHER_ID || FR.FD_VOUCHER_ID=?FD_INTEREST_VOUCHER_ID || FA.FD_VOUCHER_ID=?FD_INTEREST_VOUCHER_ID
                                AND FR.IS_DELETED=0 AND FR.STATUS=0";

                        break;
                    }
                case SQLCommand.FDAccount.FetchRenewalDetailsById:
                    {
                        query = "SELECT FDA.FD_ACCOUNT_ID,VMT.VOUCHER_DATE,\n" +
                        "       VMT.VOUCHER_NO,\n" +
                        "       FDR.FD_RENEWAL_ID,\n" +
                        "       FDA.PROJECT_ID,\n" +
                        "       FDR.FD_VOUCHER_ID,\n" +
                        "       FDR.FD_INTEREST_VOUCHER_ID,\n" +
                        "       FDR.FD_VOUCHER_ID,\n" +
                        "       FDR.INTEREST_LEDGER_ID,\n" +
                        "       VT.AMOUNT AS 'VOUCHER_AMOUNT',\n" +
                        "       FDR.BANK_LEDGER_ID,\n" +
                        "       FDA.LEDGER_ID,\n" +
                        "       FDA.BANK_ID,\n" +
                        "       FDA.TRANS_TYPE,\n" +
                        "       FDR.RECEIPT_NO,\n" +
                        "       FDR.INTEREST_RATE, FDR.FD_TRANS_MODE,\n" +
                        "       FDR.TDS_AMOUNT, FDR.CHARGE_AMOUNT, FDR.CHARGE_MODE, IFNULL(FDR.CHARGE_LEDGER_ID, 0) AS CHARGE_LEDGER_ID,\n" +
                        "       FDR.RENEWAL_DATE,\n" +
                        "       FDR.RENEWAL_TYPE,\n" +
                        "       FDA.INVESTMENT_DATE,\n" +
                        "       IFNULL(FDR.MATURITY_DATE, FDA.MATURED_ON) AS MATURITY_DATE,\n" +
                        "       FDA.FD_ACCOUNT_NUMBER,\n" +
                        "       CONCAT(MBK.BANK, ' (', MBK.BRANCH, ')') AS BANK,\n" +
                        "       MLG.LEDGER_NAME,\n" +
                        "       MPR.PROJECT,\n" +
                        "       FDA.AMOUNT, FDR.INTEREST_TYPE,\n" +
                        "       FDA.INTEREST_AMOUNT, FDR.INTEREST_AMOUNT AS 'RENEWAL_INS_AMOUNT', \n" +
                        "       FDA.NOTES, VMT.NARRATION, IFNULL(FDR.EXPECTED_MATURITY_VALUE, 0) AS EXPECTED_MATURITY_VALUE, IFNULL(FDR.EXPECTED_INTEREST_VALUE, 0) AS EXPECTED_INTEREST_VALUE,\n" +
                        "       FDA.STATUS\n" +
                        "  FROM FD_ACCOUNT AS FDA\n" +
                        "  LEFT JOIN FD_RENEWAL AS FDR\n" +
                        "    ON FDA.FD_ACCOUNT_ID = FDR.FD_ACCOUNT_ID\n" +
                        "  LEFT JOIN VOUCHER_MASTER_TRANS AS VMT\n" +
                        "    ON FDR.FD_VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        "  LEFT JOIN VOUCHER_TRANS AS VT\n" +
                        "    ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        "  LEFT JOIN MASTER_BANK AS MBK\n" +
                        "    ON FDA.BANK_ID = MBK.BANK_ID\n" +
                        "  LEFT JOIN MASTER_PROJECT MPR\n" +
                        "    ON FDA.PROJECT_ID = MPR.PROJECT_ID\n" +
                        "  LEFT JOIN MASTER_LEDGER MLG\n" +
                        "    ON FDA.LEDGER_ID = MLG.LEDGER_ID\n" +
                        //"LEFT JOIN (SELECT FR.FD_RENEWAL_ID, VT.LEDGER_ID\n" +
                        //"     FROM VOUCHER_TRANS VT\n" +
                        //"     INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                        //"     INNER JOIN FD_RENEWAL FR ON VT.VOUCHER_ID = FR.FD_INTEREST_VOUCHER_ID OR VT.VOUCHER_ID = FR.FD_VOUCHER_ID\n" +
                        //"     WHERE FR.FD_ACCOUNT_ID = ?FD_ACCOUNT_ID AND (FR.FD_INTEREST_VOUCHER_ID = ?FD_INTEREST_VOUCHER_ID OR FR.FD_VOUCHER_ID = ?FD_INTEREST_VOUCHER_ID)\n" +
                        //"     AND ML.GROUP_ID NOT IN (12, 13, 14) AND ML.IS_BANK_INTEREST_LEDGER = 0 {AND ML.LEDGER_ID NOT IN (?TDS_ON_FDINTEREST_LEDGER_ID)} ) AS PTL\n" +
                        //"     ON PTL.FD_RENEWAL_ID = FDR.FD_RENEWAL_ID\n" +
                        " WHERE FDA.STATUS = 1 AND FDR.IS_DELETED=1 \n" +
                            // "   AND FDA.FD_STATUS = 1\n" +
                        "   AND FDA.FD_ACCOUNT_ID = ?FD_ACCOUNT_ID\n" +
                        "   AND FDR.FD_INTEREST_VOUCHER_ID = ?FD_INTEREST_VOUCHER_ID OR FDR.FD_VOUCHER_ID=?FD_INTEREST_VOUCHER_ID\n" +
                        " GROUP BY VMT.VOUCHER_ID";

                        break;
                    }
                case SQLCommand.FDAccount.FetchFDStatus:
                    {
                        query = "SELECT FD_STATUS FROM FD_ACCOUNT WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID AND STATUS=1";
                        break;
                    }
                case SQLCommand.FDAccount.CountFDRenewalDetails:
                    {
                        query = "SELECT  COUNT(*) AS 'FD_ACCOUNT_ID'  FROM FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID AND STATUS=1 AND IS_DELETED=1";
                        break;
                    }
                case SQLCommand.FDAccount.FetchACIAmount:
                    {
                        query = "SELECT SUM(INTEREST_AMOUNT) AS 'AMOUNT' FROM FD_RENEWAL WHERE RENEWAL_DATE <=?RENEWAL_DATE AND RENEWAL_TYPE ='ACI' AND FD_ACCOUNT_ID=?FD_ACCOUNT_ID AND STATUS=1 AND IS_DELETED=1";
                        break;
                    }
                case SQLCommand.FDAccount.FetchRINAmount:
                    {
                        query = "SELECT SUM(REINVESTED_AMOUNT) AS 'AMOUNT' FROM FD_RENEWAL WHERE RENEWAL_DATE <=?RENEWAL_DATE AND RENEWAL_TYPE ='RIN' AND FD_ACCOUNT_ID=?FD_ACCOUNT_ID AND STATUS=1 AND IS_DELETED=1";
                        break;
                    }
                case SQLCommand.FDAccount.FetchWithdrawAmount:
                    {
                        //query = "SELECT SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT  FROM FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID {AND FD_RENEWAL_ID=?FD_RENEWAL_ID} AND RENEWAL_TYPE='WDI' AND STATUS=1 AND IS_DELETED=1 ";
                        query = "SELECT SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT  FROM FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID {AND FD_RENEWAL_ID=?FD_RENEWAL_ID} {AND FD_VOUCHER_ID=?FD_VOUCHER_ID} {OR FD_INTEREST_VOUCHER_ID=?FD_INTEREST_VOUCHER_ID} AND RENEWAL_TYPE='WDI' AND STATUS=1 AND IS_DELETED=1 ";
                        break;
                    }
                case SQLCommand.FDAccount.FetchWithdrawAmountUptoCurrent:
                    {
                        query = "SELECT SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT  FROM FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID AND RENEWAL_TYPE IN ('WDI','PWD') AND RENEWAL_DATE <?RENEWAL_DATE AND STATUS=1 AND IS_DELETED=1 ";
                        break;
                    }
                case SQLCommand.FDAccount.HasFDAccount:
                    {
                        query = "SELECT COUNT(*) FROM FD_ACCOUNT WHERE FD_ACCOUNT_NUMBER =?FD_ACCOUNT_NUMBER AND STATUS=1";
                        break;
                    }
                case SQLCommand.FDAccount.HasFlxiFD:
                    {
                        query = "SELECT COUNT(*) FROM FD_ACCOUNT WHERE STATUS=1 AND FD_SCHEME=1";
                        break;
                    }
                case SQLCommand.FDAccount.HasFDAdjustmentEntry:
                    {
                        query = "SELECT COUNT(*) FROM FD_ACCOUNT FA\n" +
                                    "INNER JOIN FD_RENEWAL FR ON FA.FD_ACCOUNT_ID = FR.FD_ACCOUNT_ID AND FR.STATUS=1 AND FR.RENEWAL_TYPE = 'ACI' AND FR.FD_TRANS_MODE = 'CR'\n" +
                                    "WHERE FA.STATUS=1 AND FR.STATUS=1 AND FR.FD_TRANS_MODE = 'CR'";
                        break;
                    }
                case SQLCommand.FDAccount.DeleteFDAccountByVoucherId:
                    {
                        query = "UPDATE FD_ACCOUNT SET STATUS=0 WHERE FD_VOUCHER_ID=?FD_VOUCHER_ID";
                        break;
                    }
                //chinna
                case SQLCommand.FDAccount.DeletePhysicalFDAccountByVoucherId:
                    {
                        query = "DELETE FROM FD_ACCOUNT WHERE FD_VOUCHER_ID =?FD_VOUCHER_ID";
                        break;
                    }
                case SQLCommand.FDAccount.FetchVoucherByAccount:
                    {
                        query = "SELECT FD_INTEREST_VOUCHER_ID, FD_VOUCHER_ID\n" +
                        "  FROM FD_RENEWAL\n" +
                        " WHERE FD_ACCOUNT_ID = ?FD_ACCOUNT_ID\n" +
                        "   AND STATUS = 1\n" +
                        "   AND RENEWAL_TYPE = 'WDI'\n" +
                        "   AND IS_DELETED = 1 ORDER BY FD_RENEWAL_ID DESC LIMIT 1";
                        break;
                    }
                case SQLCommand.FDAccount.GetNoOfPostInterest:
                    {
                        query = @" SELECT RENEWAL_DATE,MATURITY_DATE,RECEIPT_NO,FD_ACCOUNT_ID,INTEREST_AMOUNT,INTEREST_RATE,RENEWAL_TYPE,INTEREST_TYPE FROM
                                    FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID AND FD_TYPE IN ('POI') AND RENEWAL_TYPE NOT IN('WDI') AND IS_DELETED=1 AND STATUS=1 ORDER BY RENEWAL_DATE ASC";
                        break;
                    }
                case SQLCommand.FDAccount.GetNoOfPostInterestCount:
                    {
                        query = @"SELECT COUNT(RENEWAL_DATE) AS COUNT FROM
                                 FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID AND FD_TYPE IN ('POI') AND RENEWAL_TYPE NOT IN('WDI') AND IS_DELETED=1 AND STATUS=1 ORDER BY RENEWAL_DATE ASC";
                        break;
                    }
                case SQLCommand.FDAccount.GetNoofReInvestment:
                    {
                        query = @" SELECT RENEWAL_DATE,MATURITY_DATE,RECEIPT_NO,FD_ACCOUNT_ID,INTEREST_AMOUNT,INTEREST_RATE,RENEWAL_TYPE,INTEREST_TYPE,REINVESTED_AMOUNT FROM
                                    FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID AND FD_TYPE IN ('RIN') AND RENEWAL_TYPE NOT IN('WDI') AND IS_DELETED=1 AND STATUS=1 ORDER BY RENEWAL_DATE ASC";
                        break;
                    }
                case SQLCommand.FDAccount.GetNoOfPostInterestByDateRange:
                    {
                        query = @"SELECT RENEWAL_DATE,MATURITY_DATE,RECEIPT_NO,FD_ACCOUNT_ID,INTEREST_AMOUNT,INTEREST_RATE,RENEWAL_TYPE,INTEREST_TYPE,FD_TYPE FROM
                                  FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID AND FD_TYPE IN ('POI','RN') AND RENEWAL_TYPE NOT IN('WDI') AND MATURITY_DATE < ?RENEWAL_DATE AND IS_DELETED=1 AND STATUS=1 ORDER BY RENEWAL_DATE ASC";
                        break;
                    }
                case SQLCommand.FDAccount.FetchFDWithdrawalsByFDAccountId:
                    {
                        query = "SELECT FD_RENEWAL_ID, VMT.VOUCHER_NO, VMT1.VOUCHER_NO AS INTEREST_VOUCHER_NO, FR.FD_VOUCHER_ID, FR.FD_INTEREST_VOUCHER_ID,\n" +
                                "       FD_ACCOUNT_ID,\n" +
                                "       RENEWAL_DATE,\n" +
                                "       RECEIPT_NO,\n" +
                                "       INTEREST_RATE,FR.INTEREST_TYPE,\n" +
                                "       INTEREST_AMOUNT,\n" +
                                "       MATURITY_DATE,\n" +
                                "       INTEREST_LEDGER_ID,\n" +
                                "       BANK_LEDGER_ID,WITHDRAWAL_AMOUNT,\n" +
                            //"       CASE WHEN RENEWAL_TYPE='ACI' THEN\n" +
                            //"       'Accumulated Interest'\n" +
                            //"        WHEN RENEWAL_TYPE='IRI' THEN\n" +
                            //"       'Interest Received' END AS 'RENEWAL_TYPE',\n" +
                                "       RENEWAL_TYPE, VMT.NARRATION, IFNULL(FR.EXPECTED_MATURITY_VALUE, 0) AS EXPECTED_MATURITY_VALUE, IFNULL(FR.EXPECTED_INTEREST_VALUE, 0) AS EXPECTED_INTEREST_VALUE,\n" +
                                "       FR.CHARGE_LEDGER_ID, FR.CHARGE_MODE, CASE WHEN FR.CHARGE_MODE=1 THEN 'Interest' WHEN FR.CHARGE_MODE=2 THEN 'Principal' ELSE '' END AS CHARGE_MODE_NAME, \n" +
                                "       IFNULL(FR.TDS_AMOUNT, 0) AS TDS_AMOUNT, IFNULL(FR.CHARGE_AMOUNT, 0) AS CHARGE_AMOUNT, IFNULL(FR.FD_TRANS_MODE, '') AS FD_TRANS_MODE\n" +
                                " FROM FD_RENEWAL AS FR\n" +
                                " LEFT JOIN VOUCHER_MASTER_TRANS  VMT ON VMT.VOUCHER_ID = FR.FD_VOUCHER_ID\n" +
                                " LEFT JOIN VOUCHER_MASTER_TRANS  VMT1 ON VMT1.VOUCHER_ID = FR.FD_INTEREST_VOUCHER_ID\n" +
                                " WHERE FR.STATUS =1 AND FR.IS_DELETED=1 AND FR.RENEWAL_TYPE IN('WDI','PWD') AND FD_TYPE IN ('WD') AND FD_ACCOUNT_ID IN(?FD_ACCOUNT_ID) ORDER BY RENEWAL_DATE DESC";
                        break;
                    }
                case SQLCommand.FDAccount.FetchFDReInvestmentByFDAccountId:
                    {
                        query = "SELECT FD_RENEWAL_ID,VMT.VOUCHER_NO,FR.FD_VOUCHER_ID,FR.FD_INTEREST_VOUCHER_ID,\n" +
                                "       FD_ACCOUNT_ID,\n" +
                                "       RENEWAL_DATE,\n" +
                                "       RECEIPT_NO,\n" +
                                "       INTEREST_RATE,FR.INTEREST_TYPE,\n" +
                                "       REINVESTED_AMOUNT AS INTEREST_AMOUNT ,\n" +
                                "       MATURITY_DATE,\n" +
                                "       INTEREST_LEDGER_ID,\n" +
                                "       BANK_LEDGER_ID,WITHDRAWAL_AMOUNT,\n" +
                                "       RENEWAL_TYPE, IFNULL(FR.FD_TRANS_MODE, '') AS FD_TRANS_MODE,\n" +
                                "       VMT.NARRATION\n" +
                                " FROM FD_RENEWAL AS FR\n" +
                                " LEFT JOIN VOUCHER_MASTER_TRANS  VMT ON\n" +
                                " FR.FD_VOUCHER_ID=VMT.VOUCHER_ID\n" +
                                " WHERE FR.STATUS =1 AND FR.IS_DELETED=1 AND FR.RENEWAL_TYPE IN('RIN') AND FD_TYPE IN ('RIN') AND FD_ACCOUNT_ID IN(?FD_ACCOUNT_ID) ORDER BY RENEWAL_DATE DESC";
                        break;
                    }
                case SQLCommand.FDAccount.FetchPrinicpalAmountBydate:
                    {
                        // commanded by chinna Reg the ReInveste Amount and sum Calculation
                        query = "SELECT IFNULL(IFNULL(FDA.AMOUNT, 0) - SUM(IFNULL(FDR.WITHDRAWAL_AMOUNT, 0)),0) AS 'CURRENT_PRINCIPAL_AMOUNT',\n" +
                        "       (SELECT IFNULL(IFNULL(FDA.AMOUNT, 0) - SUM(IFNULL(FDR.WITHDRAWAL_AMOUNT, 0)),0)\n" +
                        "          FROM FD_ACCOUNT FDA\n" +
                        "         INNER JOIN FD_RENEWAL FDR\n" +
                        "            ON FDR.FD_ACCOUNT_ID = FDA.FD_ACCOUNT_ID\n" +
                        "         WHERE FDA.FD_ACCOUNT_ID = ?FD_ACCOUNT_ID AND FDA.STATUS=1\n" +
                        "           AND RENEWAL_DATE < ?RENEWAL_DATE) AS 'AMOUNT',IFNULL(FDA.AMOUNT,0) AS 'INVESTED AMOUNT',\n" +
                        "         SUM(FDR.REINVESTED_AMOUNT) AS 'REINVESTED_AMOUNT',\n" +
                        "         SUM(IF(RENEWAL_TYPE = 'ACI',FDR.INTEREST_AMOUNT , 0 )) AS 'INTEREST_AMOUNT',\n" +
                        "        (IFNULL(FDA.AMOUNT,0) + IFNULL(SUM(REINVESTED_AMOUNT),0) + IFNULL(SUM(IF(RENEWAL_TYPE = 'ACI',FDR.INTEREST_AMOUNT , 0 )),0)) AS 'PRINCIPAL',\n" +
                        "         IFNULL(SUM(FDR.WITHDRAWAL_AMOUNT),0)  AS 'WITHDRAWALS'\n" +
                        "  FROM FD_ACCOUNT FDA\n" +
                        " INNER JOIN FD_RENEWAL FDR\n" +
                        "    ON FDR.FD_ACCOUNT_ID = FDA.FD_ACCOUNT_ID\n" +
                        " WHERE FDA.FD_ACCOUNT_ID = ?FD_ACCOUNT_ID AND FDA.STATUS=1 AND FDR.STATUS=1 AND FDR.IS_DELETED=1;";

                        // query = "SELECT IFNULL(IFNULL(FDA.AMOUNT, 0) - SUM(IFNULL(FDR.WITHDRAWAL_AMOUNT, 0)),0) AS 'CURRENT_PRINCIPAL_AMOUNT',\n" +
                        //"       (SELECT IFNULL(IFNULL(FDA.AMOUNT, 0) - SUM(IFNULL(FDR.WITHDRAWAL_AMOUNT, 0)),0)\n" +
                        //"          FROM FD_ACCOUNT FDA\n" +
                        //"         INNER JOIN FD_RENEWAL FDR\n" +
                        //"            ON FDR.FD_ACCOUNT_ID = FDA.FD_ACCOUNT_ID\n" +
                        //"         WHERE FDA.FD_ACCOUNT_ID = ?FD_ACCOUNT_ID AND FDA.STATUS=1\n" +
                        //"           AND RENEWAL_DATE < ?RENEWAL_DATE) AS 'AMOUNT',IFNULL(FDA.AMOUNT,0) AS 'INVESTED AMOUNT',\n" +
                        //"         IFNULL(SUM(FDR.WITHDRAWAL_AMOUNT),0)  AS 'WITHDRAWALS'\n" +
                        //"  FROM FD_ACCOUNT FDA\n" +
                        //" INNER JOIN FD_RENEWAL FDR\n" +
                        //"    ON FDR.FD_ACCOUNT_ID = FDA.FD_ACCOUNT_ID\n" +
                        //" WHERE FDA.FD_ACCOUNT_ID = ?FD_ACCOUNT_ID AND FDA.STATUS=1 AND FDR.STATUS=1 AND FDR.IS_DELETED=1;";

                        break;
                    }

                case SQLCommand.FDAccount.DeleteIntrestVouchersinVoucherMasterTrans:
                    {
                        query = "DELETE FROM VOUCHER_MASTER_TRANS WHERE VOUCHER_ID=?VOUCHER_ID AND VOUCHER_SUB_TYPE=?VOUCHER_SUB_TYPE;";
                        break;
                    }

                case SQLCommand.FDAccount.DeleteIntrestVouchersinVoucherTrans:
                    {
                        query = "DELETE FROM VOUCHER_TRANS WHERE VOUCHER_ID=?VOUCHER_ID;";
                        break;
                    }
                case SQLCommand.FDAccount.UpdateFDRenewalVoucherIdByZero:
                    {
                        query = "UPDATE FD_RENEWAL SET FD_INTEREST_VOUCHER_ID =0, FD_VOUCHER_ID=0,INTEREST_AMOUNT=?INTEREST_AMOUNT WHERE FD_VOUCHER_ID=?VOUCHER_ID";
                        break;
                    }
            }
            return query;
        }
        #endregion Bank SQL
    }
}
