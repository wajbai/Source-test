//Done on 09/05/2017, Modified by Alwar for getting Cash/Bank/FD Opening/Closing Balance(Query ID :TransOPBalance)
// 1. Show zero value balances (OP), Changed Query from INNER JOIN with LEDGER_BALANCE TABLE to LEFT JOIN
// 2. Above LEFT JOIN show all banks which are not mapped with project, so Added INNER JOIN with PROJECT_LEDGER

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class VoucherTransSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.VoucherTransDetails).FullName)
            {
                query = GetVoucherTransactionSQL();
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
        private string GetVoucherTransactionSQL()
        {
            string query = "";
            SQLCommand.VoucherTransDetails sqlCommandId = (SQLCommand.VoucherTransDetails)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.VoucherTransDetails.Add:
                    {
                        query = "INSERT INTO VOUCHER_TRANS ( " +
                               "VOUCHER_ID, " +
                               "SEQUENCE_NO, " +
                               "LEDGER_ID, " +
                               "AMOUNT," +
                               "TRANS_MODE," +
                               "LEDGER_FLAG," +
                               "CHEQUE_NO," +
                               "MATERIALIZED_ON," +
                               "STATUS,NARRATION,GST,CGST,SGST,IGST,REFERENCE_NUMBER," +
                               "CHEQUE_REF_DATE, CHEQUE_REF_BANKNAME, CHEQUE_REF_BRANCH, LEDGER_GST_CLASS_ID, FUND_TRANSFER_TYPE_NAME," +
                               "EXCHANGE_RATE, LIVE_EXCHANGE_RATE, ACTUAL_AMOUNT)" +
                               "VALUES( " +
                               "?VOUCHER_ID, " +
                               "?SEQUENCE_NO, " +
                               "?LEDGER_ID, " +
                               "?AMOUNT," +
                               "?TRANS_MODE," +
                               "?LEDGER_FLAG," +
                               "?CHEQUE_NO," +
                               "?MATERIALIZED_ON," +
                               "?STATUS,?NARRATION, ?GST, ?CGST, ?SGST, ?IGST, ?REFERENCE_NUMBER," +
                               "?CHEQUE_REF_DATE, ?CHEQUE_REF_BANKNAME, ?CHEQUE_REF_BRANCH, ?LEDGER_GST_CLASS_ID,?FUND_TRANSFER_TYPE_NAME," +
                               "?EXCHANGE_RATE, ?LIVE_EXCHANGE_RATE, ?ACTUAL_AMOUNT)";
                        break;
                    }
                case SQLCommand.VoucherTransDetails.Delete:
                    {
                        query = "DELETE FROM VOUCHER_TRANS WHERE VOUCHER_ID=?VOUCHER_ID ";
                        break;
                    }

                case SQLCommand.VoucherTransDetails.FetchAll:
                    {
                        query = "SELECT VOUCHER_ID, ML.LEDGER_NAME ,VT.AMOUNT ,CASE TRANS_MODE WHEN 'CR' THEN 'Credit' ELSE 'Debit' END AS TRANSMODE," +
                                "LEDGER_FLAG, MB.ACCOUNT_NUMBER, CHEQUE_NO ,MATERIALIZED_ON " +
                                "FROM VOUCHER_TRANS AS VT " +
                                "INNER JOIN MASTER_LEDGER AS ML ON VT.LEDGER_ID =ML.LEDGER_ID " +
                                "LEFT JOIN MASTER_BANK_ACCOUNT AS MB ON  ML.BANK_ACCOUNT_ID=MB.BANK_ACCOUNT_ID;";
                        break;
                    }

                case SQLCommand.VoucherTransDetails.FetchTransactionDetails:
                    {
                        query = " SELECT VT.VOUCHER_ID, ML.LEDGER_NAME,VT.LEDGER_ID ,CASE TRANS_MODE WHEN 'CR' THEN 'Credit' ELSE 'Debit' END AS TRANSMODE," +
                              " CASE TRANS_MODE WHEN 'CR' THEN VT.AMOUNT ELSE '' END AS 'CREDIT'," +
                              " CASE TRANS_MODE WHEN 'DR' THEN VT.AMOUNT ELSE '' END AS 'DEBIT'," +
                              " LEDGER_FLAG, MB.ACCOUNT_NUMBER, " +
                              " CHEQUE_NO ,MATERIALIZED_ON FROM VOUCHER_TRANS AS VT " +
                              " INNER JOIN MASTER_LEDGER AS ML ON VT.LEDGER_ID =ML.LEDGER_ID " +
                              " LEFT JOIN MASTER_BANK_ACCOUNT AS MB ON " +
                              " ML.LEDGER_ID=MB.LEDGER_ID WHERE FIND_IN_SET(VT.VOUCHER_ID,?VOUCHER_ID)>0 ORDER BY VOUCHER_ID,SORT_ID DESC";
                        break;
                    }
                case SQLCommand.VoucherTransDetails.FetchVoucherTrans:
                    {
                        //Added by Salamon Raj M on 27.10.2015
                        query = "SELECT VT.VOUCHER_ID,\n" +
                                "       ML.LEDGER_NAME,\n" +
                                "       VT.LEDGER_ID,\n" +
                                "       VT.SEQUENCE_NO,\n" +
                                "       CASE TRANS_MODE\n" +
                                "         WHEN 'CR' THEN\n" +
                                "          'Credit'\n" +
                                "         ELSE\n" +
                                "          'Debit'\n" +
                                "       END AS TRANSMODE,\n" +
                                "       CASE TRANS_MODE\n" +
                                "         WHEN 'CR' THEN\n" +
                                "          VT.AMOUNT\n" +
                                "         ELSE\n" +
                                "          0.00\n" +
                                "       END AS 'CREDIT',\n" +
                                "       CASE TRANS_MODE\n" +
                                "         WHEN 'DR' THEN\n" +
                                "          VT.AMOUNT\n" +
                                "         ELSE\n" +
                                "          0.00\n" +
                                "       END AS 'DEBIT', VT.EXCHANGE_RATE, VT.LIVE_EXCHANGE_RATE,\n" +
                                "       LEDGER_FLAG,\n" +
                                "       MB.ACCOUNT_NUMBER,\n" +
                            //"       CHEQUE_NO,\n" + On 17/10/2017, Add Cheque number details (Cheque Date, Cheque BankName and brancn)
                                "CONCAT(CHEQUE_NO, CONCAT(CONCAT(IF(CHEQUE_REF_DATE IS NULL OR CHEQUE_NO='','', CONCAT(' - ',DATE_FORMAT(CHEQUE_REF_DATE,'%d/%m/%Y'))),\n" +
                                "       IF(CHEQUE_REF_DATE IS NULL,'', CONCAT(', ', CHEQUE_REF_BANKNAME))),\n" +
                                "       IF(CHEQUE_REF_BANKNAME IS NULL OR CHEQUE_REF_BANKNAME='', '', CONCAT(', ', CHEQUE_REF_BRANCH)))) AS CHEQUE_NO,FUND_TRANSFER_TYPE_NAME,\n" +
                                "       MATERIALIZED_ON\n" +
                                "  FROM VOUCHER_TRANS AS VT\n" +
                                " INNER JOIN VOUCHER_MASTER_TRANS VMT\n" +
                                "    ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                                " INNER JOIN MASTER_LEDGER AS ML\n" +
                                "    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                                "  LEFT JOIN MASTER_BANK_ACCOUNT AS MB\n" +
                                "    ON ML.LEDGER_ID = MB.LEDGER_ID\n" +
                                " WHERE FIND_IN_SET(VOUCHER_TYPE, ?VOUCHER_TYPE) > 0\n" +
                                "   AND VMT.PROJECT_ID = ?PROJECT_ID\n" +
                                "   AND VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                                "   AND VMT.STATUS = 1\n" +
                                " ORDER BY VOUCHER_ID,SEQUENCE_NO,ML.GROUP_ID NOT IN(12,13) DESC;"; //  to set the sequence of Voucher Trans 25.09.2017 (Chinna)
                        break;
                    }
                case SQLCommand.VoucherTransDetails.FetchJournalDetailById: // 07/02/2025,chinna
                    {
                        query = "SELECT VMT.VOUCHER_ID, TB.BOOKING_ID, TB.EXPENSE_LEDGER_ID, TB.PARTY_LEDGER_ID, TB.DEDUCTEE_TYPE_ID, TB.AMOUNT AS 'TDS_AMOUNT',\n" +
                                "       CASE\n" +
                                "         WHEN TB.BOOKING_ID > 0 AND VT.TRANS_MODE = 'DR' AND\n" +
                                "              TB.EXPENSE_LEDGER_ID > 0 THEN\n" +
                                "          0\n" +
                                "         ELSE\n" +
                                "          CASE\n" +
                                "            WHEN TB.BOOKING_ID > 0  AND ML.IS_TDS_LEDGER=1 AND ML.GROUP_ID=26 THEN\n" +
                                "             2\n" +
                                "            ELSE\n" +
                                "               CASE \n" +
                                "                   WHEN TB.BOOKING_ID>0 AND ML.IS_TDS_LEDGER=1 AND ML.GROUP_ID=24 THEN \n" +
                                "                     1\n" +
                                "                   ELSE  \n" +
                                "                      0\n" +
                                "                   END\n" +
                                "          END\n" +
                                "       END AS 'VALUE',\n" +
                                "       VMT.VOUCHER_NO, VMT.VOUCHER_DATE, ML.LEDGER_NAME, ML.LEDGER_ID, VT.REFERENCE_NUMBER,\n" +
                                "      IF(GST>0, CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(GST),'  (', CGST),'+',SGST),'/',IGST),')'),'')  AS GST_AMOUNT,\n" +
                                "      GST AS GST, CGST AS CGST, SGST AS SGST, IGST AS IGST, VMT.NARRATION AS JN_NARRATION, VT.NARRATION AS NARRATION, '' AS LEDGER_BALANCE,\n" +
                                "       CASE\n" +
                                "         WHEN VT.TRANS_MODE = 'DR' THEN\n" +
                                "          IFNULL(VT.AMOUNT, 0)\n" +
                                "       END AS DEBIT,\n" +
                                "       CASE\n" +
                                "         WHEN VT.TRANS_MODE = 'DR' THEN\n" +
                                "          IFNULL(VT.AMOUNT, 0)\n" +
                                "       END AS TEMP_DEBIT,\n" +
                                "       CASE\n" +
                                "         WHEN VT.TRANS_MODE = 'CR' THEN\n" +
                                "          IFNULL(VT.AMOUNT, 0)\n" +
                                "       END AS CREDIT,\n" +
                                "       CASE\n" +
                                "         WHEN VT.TRANS_MODE = 'CR' THEN\n" +
                                "          IFNULL(VT.AMOUNT, 0)\n" +
                                "       END AS TEMP_CREDIT, VT.LEDGER_ID AS TEMP_LEDGER_ID, VOUCHER_DEFINITION_ID,CHEQUE_NO,MATERIALIZED_ON, \n" +
                                "       IFNULL(LEDGER_GST_CLASS_ID, ?LEDGER_GST_CLASS_ID) AS LEDGER_GST_CLASS_ID, IFNULL(GIM.GST_INVOICE_ID, 0) AS GST_INVOICE_ID, ML.IS_GST_LEDGERS, \n" +
                                "       CURRENCY_COUNTRY_ID, CONTRIBUTION_AMOUNT, VMT.EXCHANGE_RATE, CALCULATED_AMOUNT, VMT.ACTUAL_AMOUNT, EXCHANGE_COUNTRY_ID,\n" +
                                "       VT.EXCHANGE_RATE AS LEDGER_EXCHANGE_RATE, VT.LIVE_EXCHANGE_RATE, VMT.IS_CASH_BANK_STATUS\n" +
                                " FROM VOUCHER_MASTER_TRANS VMT\n" +
                                " INNER JOIN VOUCHER_TRANS VT ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                " INNER JOIN MASTER_LEDGER ML ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                                " LEFT JOIN TDS_BOOKING AS TB ON VMT.VOUCHER_ID = TB.VOUCHER_ID\n" +
                                " LEFT JOIN GST_INVOICE_MASTER GIM ON GIM.BOOKING_VOUCHER_ID = VMT.VOUCHER_ID\n" +
                                " WHERE VMT.VOUCHER_ID = ?VOUCHER_ID AND VMT.VOUCHER_TYPE = 'JN' { AND ((?SUB_LEDGER_ID = 1 AND ML.GROUP_ID NOT IN (14)) or (?SUB_LEDGER_ID = 0 AND ML.GROUP_ID NOT IN (12, 13, 14))) } AND VMT.STATUS = 1\n";

                        //query = "SELECT VMT.VOUCHER_ID, TB.BOOKING_ID, TB.EXPENSE_LEDGER_ID, TB.PARTY_LEDGER_ID, TB.DEDUCTEE_TYPE_ID, TB.AMOUNT AS 'TDS_AMOUNT',\n" +
                        //        "       CASE\n" +
                        //        "         WHEN TB.BOOKING_ID > 0 AND VT.TRANS_MODE = 'DR' AND\n" +
                        //        "              TB.EXPENSE_LEDGER_ID > 0 THEN\n" +
                        //        "          0\n" +
                        //        "         ELSE\n" +
                        //        "          CASE\n" +
                        //        "            WHEN TB.BOOKING_ID > 0  AND ML.IS_TDS_LEDGER=1 AND ML.GROUP_ID=26 THEN\n" +
                        //        "             2\n" +
                        //        "            ELSE\n" +
                        //        "               CASE \n" +
                        //        "                   WHEN TB.BOOKING_ID>0 AND ML.IS_TDS_LEDGER=1 AND ML.GROUP_ID=24 THEN \n" +
                        //        "                     1\n" +
                        //        "                   ELSE  \n" +
                        //        "                      0\n" +
                        //        "                   END\n" +
                        //        "          END\n" +
                        //        "       END AS 'VALUE',\n" +
                        //        "       VMT.VOUCHER_NO, VMT.VOUCHER_DATE, ML.LEDGER_NAME, ML.LEDGER_ID, VT.REFERENCE_NUMBER,\n" +
                        //        "      IF(GST>0, CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(GST),'  (', CGST),'+',SGST),'/',IGST),')'),'')  AS GST_AMOUNT,\n" +
                        //        "      GST AS GST, CGST AS CGST, SGST AS SGST, IGST AS IGST, VMT.NARRATION AS JN_NARRATION, VT.NARRATION AS NARRATION, '' AS LEDGER_BALANCE,\n" +
                        //        "       CASE\n" +
                        //        "         WHEN VT.TRANS_MODE = 'DR' THEN\n" +
                        //        "          IFNULL(VT.AMOUNT, 0)\n" +
                        //        "       END AS DEBIT,\n" +
                        //        "       CASE\n" +
                        //        "         WHEN VT.TRANS_MODE = 'DR' THEN\n" +
                        //        "          IFNULL(VT.AMOUNT, 0)\n" +
                        //        "       END AS TEMP_DEBIT,\n" +
                        //        "       CASE\n" +
                        //        "         WHEN VT.TRANS_MODE = 'CR' THEN\n" +
                        //        "          IFNULL(VT.AMOUNT, 0)\n" +
                        //        "       END AS CREDIT,\n" +
                        //        "       CASE\n" +
                        //        "         WHEN VT.TRANS_MODE = 'CR' THEN\n" +
                        //        "          IFNULL(VT.AMOUNT, 0)\n" +
                        //        "       END AS TEMP_CREDIT, VT.LEDGER_ID AS TEMP_LEDGER_ID, VOUCHER_DEFINITION_ID,CHEQUE_NO,MATERIALIZED_ON, \n" +
                        //        "       IFNULL(LEDGER_GST_CLASS_ID, ?LEDGER_GST_CLASS_ID) AS LEDGER_GST_CLASS_ID, IFNULL(GIM.GST_INVOICE_ID, 0) AS GST_INVOICE_ID, ML.IS_GST_LEDGERS, \n" +
                        //        "       CURRENCY_COUNTRY_ID, CONTRIBUTION_AMOUNT, VMT.EXCHANGE_RATE, CALCULATED_AMOUNT, VMT.ACTUAL_AMOUNT, EXCHANGE_COUNTRY_ID,\n" +
                        //        "       VT.EXCHANGE_RATE AS LEDGER_EXCHANGE_RATE, VT.LIVE_EXCHANGE_RATE\n" +
                        //        " FROM VOUCHER_MASTER_TRANS VMT\n" +
                        //        " INNER JOIN VOUCHER_TRANS VT ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        //        " INNER JOIN MASTER_LEDGER ML ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                        //        " LEFT JOIN TDS_BOOKING AS TB ON VMT.VOUCHER_ID = TB.VOUCHER_ID\n" +
                        //        " LEFT JOIN GST_INVOICE_MASTER GIM ON GIM.BOOKING_VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        //        " WHERE VMT.VOUCHER_ID = ?VOUCHER_ID AND VMT.VOUCHER_TYPE = 'JN'  AND VMT.STATUS = 1\n";

                        break;
                    }
                case SQLCommand.VoucherTransDetails.FetchTransDetails:
                    {
                        query = "SELECT VT.VOUCHER_ID,\n" +
                               "       SEQUENCE_NO,\n" +
                               "       IF(TRANS_MODE = 'CR', 1, 2) AS SOURCE,\n" +
                               "       VT.LEDGER_ID,\n" +
                               "       ML.GROUP_ID,\n" +
                               "       MLG.NATURE_ID,\n" +
                               "       VT.AMOUNT,\n" +
                               "       VT.TRANS_MODE,\n" +
                               "       LEDGER_FLAG,\n" +
                               "       VM.VOUCHER_SUB_TYPE,\n" +
                               "       CHEQUE_NO,\n" +
                               "       MATERIALIZED_ON,\n" +
                               "       VT.STATUS,\n" +
                               "       '' AS LEDGER_BALANCE,'' AS BUDGET_AMOUNT,\n" +
                               "       IF(GST>0, CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(GST),'  (', CGST),'+',SGST),'/',IGST),')'),'')  AS GST_AMOUNT,\n" +
                               "      GST AS GST,\n" +
                               "      CGST AS CGST,\n" +
                               "      SGST AS SGST,\n" +
                               "      IGST AS IGST,\n" +
                               "       VT.AMOUNT AS TEMP_AMOUNT,\n" +
                               "       VT.LEDGER_ID AS TEMP_LEDGER_ID,\n" +
                               "       VT.AMOUNT AS BASE_AMOUNT,\n" +
                               "       VT.CHEQUE_REF_DATE, VT.CHEQUE_REF_BANKNAME, VT.CHEQUE_REF_BRANCH, VT.FUND_TRANSFER_TYPE_NAME,\n" +
                               "       T.LEDGER_ID AS REF_LEDGER_ID,T.AMOUNT AS REF_AMOUNT, VT.NARRATION, VM.VOUCHER_DEFINITION_ID,\n" +
                               "       IFNULL(LEDGER_GST_CLASS_ID, ?LEDGER_GST_CLASS_ID) AS LEDGER_GST_CLASS_ID, ML.IS_GST_LEDGERS,\n" +
                               "       VT.EXCHANGE_RATE, VT.LIVE_EXCHANGE_RATE, VT.ACTUAL_AMOUNT\n" +
                               " FROM VOUCHER_TRANS VT\n" +
                               " INNER JOIN VOUCHER_MASTER_TRANS VM ON VT.VOUCHER_ID = VM.VOUCHER_ID\n" +
                               " LEFT JOIN (SELECT REC_PAY_VOUCHER_ID,LEDGER_ID,SUM(AMOUNT) AS AMOUNT FROM VOUCHER_REFERENCE WHERE REC_PAY_VOUCHER_ID IN (?VOUCHER_ID) GROUP BY REC_PAY_VOUCHER_ID, LEDGER_ID) AS T\n" +
                               "     ON T.REC_PAY_VOUCHER_ID = VM.VOUCHER_ID AND T.LEDGER_ID = VT.LEDGER_ID\n" +
                               " INNER JOIN MASTER_LEDGER ML ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                               " INNER JOIN MASTER_LEDGER_GROUP MLG ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                               " WHERE VT.VOUCHER_ID = ?VOUCHER_ID\n" +
                               "   AND IF(VM.VOUCHER_TYPE = 'CN',\n" +
                               "          VT.TRANS_MODE = 'CR',\n" +
                               "          ML.GROUP_ID NOT IN (12, 13))";
                        break;
                    }
                case SQLCommand.VoucherTransDetails.FetchMoveTransDetails:
                    {
                        query = "SELECT VT.VOUCHER_ID,\n" +
                              "       SEQUENCE_NO,\n" +
                              "       IF(TRANS_MODE = 'CR', 1, 2) AS SOURCE,\n" +
                              "       VT.LEDGER_ID,\n" +
                              "       AMOUNT,\n" +
                              "       VT.TRANS_MODE,\n" +
                              "       LEDGER_FLAG,\n" +
                              "       CHEQUE_NO,\n" +
                              "       MATERIALIZED_ON,\n" +
                              "       VT.STATUS,\n" +
                              "       '' AS LEDGER_BALANCE,'' AS BUDGET_AMOUNT,\n" +
                              "       AMOUNT AS TEMP_AMOUNT,\n" +
                              "       VT.LEDGER_ID AS TEMP_LEDGER_ID,\n" +
                              "       AMOUNT AS BASE_AMOUNT, VM.VOUCHER_DEFINITION_ID\n" +
                              "  FROM VOUCHER_TRANS VT\n" +
                              " INNER JOIN VOUCHER_MASTER_TRANS VM\n" +
                              "    ON VT.VOUCHER_ID = VM.VOUCHER_ID\n" +
                              " INNER JOIN MASTER_LEDGER ML\n" +
                              "    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                              " WHERE VT.VOUCHER_ID = ?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherTransDetails.FetchCashBankDetails:
                    {
                        //query = "SELECT VOUCHER_ID, SEQUENCE_NO, LEDGER_ID, AMOUNT, TRANS_MODE, LEDGER_FLAG, CHEQUE_NO, " +
                        //         "MATERIALIZED_ON, STATUS,'' AS LEDGER_BALANCE,AMOUNT AS TEMP_AMOUNT,LEDGER_ID AS TEMP_LEDGER_ID,AMOUNT AS BASE_AMOUNT " +
                        //         "FROM voucher_trans WHERE VOUCHER_ID=?VOUCHER_ID AND IDENTITY_FLAG=2";

                        query = "SELECT VT.VOUCHER_ID,\n" +
                                "       SEQUENCE_NO,\n" +
                                "       GST AS GST_TOTAL,\n" +
                                "       IF(TRANS_MODE = 'CR', 1, 2) AS SOURCE,\n" +
                                "       VT.LEDGER_ID,\n" +
                                "       ML.GROUP_ID,\n" +
                                "       MLG.NATURE_ID,\n" +
                                "       AMOUNT,\n" +
                                "       TRANS_MODE,\n" +
                                "       LEDGER_FLAG,\n" +
                                "       CHEQUE_NO,\n" +
                                "       MATERIALIZED_ON,\n" +
                                "       VT.STATUS,\n" +
                                "       '' AS LEDGER_BALANCE,'' AS BUDGET_AMOUNT,\n" +
                                "       AMOUNT AS TEMP_AMOUNT,\n" +
                                "       VT.LEDGER_ID AS TEMP_LEDGER_ID,\n" +
                                "       AMOUNT AS BASE_AMOUNT,\n" +
                                "       VT.CHEQUE_REF_DATE, VT.CHEQUE_REF_BANKNAME, VT.CHEQUE_REF_BRANCH, VT.FUND_TRANSFER_TYPE_NAME, VM.VOUCHER_DEFINITION_ID,\n" +
                                "       VT.EXCHANGE_RATE, VT.LIVE_EXCHANGE_RATE, VT.ACTUAL_AMOUNT\n" +
                                "  FROM VOUCHER_TRANS VT\n" +
                                " INNER JOIN VOUCHER_MASTER_TRANS VM\n" +
                                "    ON VT.VOUCHER_ID = VM.VOUCHER_ID\n" +
                                " INNER JOIN MASTER_LEDGER ML\n" +
                                "    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                                 " INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                                "    ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                                " WHERE VT.VOUCHER_ID = ?VOUCHER_ID\n" +
                                "   AND IF(VM.VOUCHER_TYPE = 'CN',\n" +
                                "          VT.TRANS_MODE = 'DR',\n" +
                                "          ML.GROUP_ID IN (12, 13));";

                        break;
                    }
                //case SQLCommand.VoucherTransDetails.FetchReferedVoucherLedgerId:
                //    {
                //        query = "SELECT COUNT(LEDGER_ID) FROM VOUCHER_REFERENCE WHERE LEDGER_ID =?LEDGER_ID AND REC_PAY_VOUCHER_ID=?VOUCHER_ID";
                //        break;
                //    }
                case SQLCommand.VoucherTransDetails.TransOPBalance:
                    {
                        //query = " SELECT ML.LEDGER_ID AS ID ," +
                        //        " LEDGER_NAME AS 'LEDGER_NAME',LB.AMOUNT AS AMOUNT,TRANS_MODE AS 'TRANSMODE' FROM MASTER_LEDGER ML" +
                        //        " INNER JOIN LEDGER_BALANCE AS LB ON ML.LEDGER_ID=LB.LEDGER_ID " +
                        //        " WHERE  TRANS_FLAG='OP' AND LB.PROJECT_ID=?PROJECT_ID AND ML.GROUP_ID=?GROUP_ID";
                        //break;
                        //query = "SELECT ML.LEDGER_ID AS 'ID', LG.GROUP_ID, LG.GROUP_CODE, LG.LEDGER_GROUP,ML.LEDGER_NAME AS 'LEDGER_NAME', " +
                        //      "ABS(SUM(CASE WHEN LB2.TRANS_MODE = 'DR' THEN LB2.AMOUNT ELSE - LB2.AMOUNT END)) AS AMOUNT, " +
                        //      "CASE WHEN (SUM(CASE WHEN LB2.TRANS_MODE = 'DR' " +
                        //      "               THEN LB2.AMOUNT ELSE - LB2.AMOUNT END) >= 0 ) " +
                        //      "     THEN 'DR' ELSE 'CR' END AS 'TRANSMODE' " +
                        //      "FROM MASTER_LEDGER AS ML " +
                        //      "INNER JOIN MASTER_LEDGER_GROUP AS LG " +
                        //      "ON ML.GROUP_ID = LG.GROUP_ID " +
                        //      "INNER JOIN " +
                        //      "     (SELECT LB.BALANCE_DATE, LB.PROJECT_ID, LB.LEDGER_ID, LB.AMOUNT, LB.TRANS_MODE " +
                        //      "      FROM LEDGER_BALANCE AS LB " +
                        //      "      LEFT JOIN (SELECT LBA.PROJECT_ID, LBA.LEDGER_ID, MAX(LBA.BALANCE_DATE) AS BAL_DATE " +
                        //      "                 FROM LEDGER_BALANCE LBA " +
                        //      "                 WHERE 1 = 1 {AND LBA.BALANCE_DATE <= ?BALANCE_DATE} " +
                        //      "                 GROUP BY LBA.PROJECT_ID, LBA.LEDGER_ID) AS LB1 " +
                        //      "      ON LB.PROJECT_ID = LB1.PROJECT_ID " +
                        //      "      AND LB.LEDGER_ID = LB1.LEDGER_ID " +
                        //      "      WHERE LB.PROJECT_ID IN (?PROJECT_ID) " +
                        //      "      AND LB.BALANCE_DATE = LB1.BAL_DATE) LB2 " +
                        //      "ON ML.LEDGER_ID = LB2.LEDGER_ID " +
                        //      "WHERE LG.GROUP_ID IN (?GROUP_ID) " +
                        //      "GROUP BY LG.GROUP_ID, LG.GROUP_CODE, LG.LEDGER_GROUP,ML.LEDGER_NAME";

                        query = "SELECT ML.LEDGER_ID AS 'ID', LG.GROUP_ID, LG.GROUP_CODE, LG.LEDGER_GROUP, " +
                             "CASE " +
                             " WHEN ML.LEDGER_SUB_TYPE='BK' THEN " +
                             " CONCAT(CONCAT(ML.LEDGER_NAME,' - '),CONCAT(MB.BANK,' - '),MB.BRANCH) " +
                             " ELSE " +
                             " ML.LEDGER_NAME " +
                             "END AS 'LEDGER_NAME', IFNULL(MC.CURRENCY_SYMBOL, '') AS CURRENCY_SYMBOL," +
                             "ABS(SUM(CASE WHEN LB2.TRANS_MODE = 'DR' THEN IFNULL(LB2.AMOUNT,0) ELSE - IFNULL(LB2.AMOUNT,0)  END)) AS AMOUNT, " +
                             "CASE WHEN (SUM(CASE WHEN LB2.TRANS_MODE = 'DR' " +
                             "               THEN IFNULL(LB2.AMOUNT,0)  ELSE - IFNULL(LB2.AMOUNT,0) END) >= 0 ) " +
                             "     THEN 'DR' ELSE 'CR' END AS 'TRANSMODE' " +
                             "FROM MASTER_LEDGER AS ML " +
                             "INNER JOIN MASTER_LEDGER_GROUP AS LG " +
                             "ON ML.GROUP_ID = LG.GROUP_ID " +
                             "INNER JOIN PROJECT_LEDGER AS PL ON PL.PROJECT_ID IN (?PROJECT_ID) AND PL.LEDGER_ID = ML.LEDGER_ID " + //On 09/05/2017, To show zero value cash/bank/Fd Opening/Closing Balance Skip unmapped ledgers: Changed INNER JOIN with PROJECT_LEDGERS
                             "LEFT JOIN " + //On 09/05/2017, To show zero value cash/bank/Fd Opening/Closing Balance: Changed INNER JOIN to LEFT
                             "     (SELECT LB.BALANCE_DATE, LB.PROJECT_ID, LB.LEDGER_ID, " +
                             "       IF(ML.GROUP_ID IN (12, 13) AND ML.CUR_COUNTRY_ID > 0, LB.AMOUNT_FC, LB.AMOUNT) AS AMOUNT, " +
                             "       IF(ML.GROUP_ID IN (12, 13) AND ML.CUR_COUNTRY_ID > 0, LB.TRANS_FC_MODE, LB.TRANS_MODE) AS TRANS_MODE " +
                             "      FROM LEDGER_BALANCE AS LB INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = LB.LEDGER_ID" +
                             "      LEFT JOIN (SELECT LBA.PROJECT_ID, LBA.LEDGER_ID, MAX(LBA.BALANCE_DATE) AS BAL_DATE " +
                             "                 FROM LEDGER_BALANCE LBA " +
                             "                 WHERE 1 = 1 {AND LBA.BALANCE_DATE <= ?BALANCE_DATE} " +
                             "                 GROUP BY LBA.PROJECT_ID, LBA.LEDGER_ID) AS LB1 " +
                             "      ON LB.PROJECT_ID = LB1.PROJECT_ID " +
                             "      AND LB.LEDGER_ID = LB1.LEDGER_ID " +
                             "      WHERE LB.PROJECT_ID IN (?PROJECT_ID) " +
                             "      AND LB.BALANCE_DATE = LB1.BAL_DATE) LB2 " +
                             "ON ML.LEDGER_ID = LB2.LEDGER_ID AND PL.PROJECT_ID = LB2.PROJECT_ID " +
                             "LEFT JOIN MASTER_BANK_ACCOUNT MBA ON MBA.LEDGER_ID=ML.LEDGER_ID " +
                             "LEFT JOIN MASTER_BANK MB ON MB.BANK_ID=MBA.BANK_ID " +
                             "LEFT JOIN MASTER_COUNTRY MC ON MC.COUNTRY_ID = ML.CUR_COUNTRY_ID " +
                             "WHERE LG.GROUP_ID IN (?GROUP_ID) and ML.STATUS=0 " +
                             "{AND (MBA.DATE_CLOSED IS NULL OR MBA.DATE_CLOSED >= ?DATE_CLOSED) } " + //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                             "GROUP BY LG.GROUP_ID, LG.GROUP_CODE, LG.LEDGER_GROUP,ML.LEDGER_NAME";
                        break;
                    }
                case SQLCommand.VoucherTransDetails.FetchFDOPBalance:
                    {
                        query = "SELECT FD.FD_ACCOUNT_NUMBER AS 'LEDGER_NAME', FD.AMOUNT AS 'AMOUNT' ,FD.TRANS_MODE AS 'TRANSMODE' \n" +
                                "  FROM FD_ACCOUNT AS FD\n" +
                                " INNER JOIN MASTER_LEDGER AS ML\n" +
                                "    ON FD.LEDGER_ID = ML.LEDGER_ID\n" +
                                " INNER JOIN LEDGER_BALANCE AS LB\n" +
                                "    ON FD.PROJECT_ID = LB.PROJECT_ID\n" +
                                " WHERE FD.TRANS_TYPE = 'OP'\n" +
                                "   AND FD.PROJECT_ID = ?PROJECT_ID\n" +
                                "   AND FD.STATUS = 1\n" +
                                "   AND GROUP_ID = ?GROUP_ID\n" +
                                " GROUP BY FD_ACCOUNT_NUMBER";
                        break;
                    }
                case SQLCommand.VoucherTransDetails.TransCBBalance:
                    {
                        query = "SELECT ML.LEDGER_ID AS 'ID', LG.GROUP_ID, LG.GROUP_CODE, LG.LEDGER_GROUP, " +
                               "CASE " +
                               " WHEN ML.LEDGER_SUB_TYPE='BK' THEN " +
                               " CONCAT(CONCAT(ML.LEDGER_NAME,' - '),CONCAT(MB.BANK,' - '),MB.BRANCH) " +
                               " ELSE " +
                               " ML.LEDGER_NAME " +
                               "END AS 'LEDGER_NAME', " +
                               "ABS(SUM(CASE WHEN LB2.TRANS_MODE = 'DR' THEN IFNULL(LB2.AMOUNT,0) ELSE - IFNULL(LB2.AMOUNT,0) END)) AS AMOUNT, " +
                               "CASE WHEN (SUM(CASE WHEN LB2.TRANS_MODE = 'DR' " +
                               "               THEN IFNULL(LB2.AMOUNT,0) ELSE - IFNULL(LB2.AMOUNT,0) END) >= 0 ) " +
                               "     THEN 'DR' ELSE 'CR' END AS 'TRANSMODE', IFNULL(MC.CURRENCY_SYMBOL, '') AS CURRENCY_SYMBOL " +
                               "FROM MASTER_LEDGER AS ML " +
                               "INNER JOIN MASTER_LEDGER_GROUP AS LG ON ML.GROUP_ID = LG.GROUP_ID " +
                               "INNER JOIN PROJECT_LEDGER AS PL ON {PL.PROJECT_ID IN (?PROJECT_ID) AND} PL.LEDGER_ID = ML.LEDGER_ID " + //On 09/05/2017, To show zero value cash/bank/Fd Opening/Closing Balance Skip unmapped ledgers: Changed INNER JOIN with PROJECT_LEDGERS
                                "LEFT JOIN " + //On 09/05/2017, To show zero value cash/bank/Fd Opening/Closing Balance: Changed INNER JOIN to LEFT
                               "     (SELECT LB.BALANCE_DATE, LB.PROJECT_ID, LB.LEDGER_ID, " +
                               "        IF(ML.GROUP_ID IN (12, 13) AND ML.CUR_COUNTRY_ID > 0, LB.AMOUNT_FC, LB.AMOUNT) AS AMOUNT, " +
                               "        IF(ML.GROUP_ID IN (12, 13) AND ML.CUR_COUNTRY_ID > 0, LB.TRANS_FC_MODE, LB.TRANS_MODE) AS TRANS_MODE " +
                               "      FROM LEDGER_BALANCE AS LB INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = LB.LEDGER_ID" +
                               "      LEFT JOIN (SELECT LBA.PROJECT_ID, LBA.LEDGER_ID, MAX(LBA.BALANCE_DATE) AS BAL_DATE " +
                               "                 FROM LEDGER_BALANCE LBA " +
                               "                 WHERE 1 = 1 {AND LBA.BALANCE_DATE <= ?BALANCE_DATE} " +
                               "                 GROUP BY LBA.PROJECT_ID, LBA.LEDGER_ID) AS LB1 " +
                               "      ON LB.PROJECT_ID = LB1.PROJECT_ID " +
                               "      AND LB.LEDGER_ID = LB1.LEDGER_ID " +
                               "      WHERE 1=1 {AND LB.PROJECT_ID IN (?PROJECT_ID)} " +
                               "      AND LB.BALANCE_DATE = LB1.BAL_DATE) LB2 " +
                               "ON ML.LEDGER_ID = LB2.LEDGER_ID AND PL.PROJECT_ID = LB2.PROJECT_ID " +
                               "LEFT JOIN MASTER_BANK_ACCOUNT MBA ON MBA.LEDGER_ID=ML.LEDGER_ID " +
                               "LEFT JOIN MASTER_BANK MB ON MB.BANK_ID=MBA.BANK_ID " +
                               "LEFT JOIN MASTER_COUNTRY MC ON MC.COUNTRY_ID = ML.CUR_COUNTRY_ID " +
                               "WHERE LG.GROUP_ID IN (?GROUP_ID) AND ML.STATUS=0 " +
                               " {AND (MBA.DATE_CLOSED IS NULL OR MBA.DATE_CLOSED >= ?DATE_CLOSED) } " + //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                               "GROUP BY LG.GROUP_ID, LG.GROUP_CODE, LG.LEDGER_GROUP,ML.LEDGER_NAME";
                        break;
                    }
                case SQLCommand.VoucherTransDetails.TransCBNegativeBalance:
                    {
                        query = "SELECT T.ID,T.GROUP_ID,T.GROUP_CODE,T.LEDGER_GROUP,T.LEDGER_NAME,T.AMOUNT,T.TRANSMODE FROM (SELECT ML.LEDGER_ID AS 'ID', LG.GROUP_ID, LG.GROUP_CODE, LG.LEDGER_GROUP, " +
                               "CASE " +
                               " WHEN ML.LEDGER_SUB_TYPE='BK' THEN " +
                               " CONCAT(CONCAT(ML.LEDGER_NAME,' - '),CONCAT(MB.BANK,' - '),MB.BRANCH) " +
                               " ELSE " +
                               " ML.LEDGER_NAME " +
                               "END AS 'LEDGER_NAME', " +
                               "ABS(SUM(CASE WHEN LB2.TRANS_MODE = 'DR' THEN LB2.AMOUNT ELSE - LB2.AMOUNT END)) AS AMOUNT, " +
                               "CASE WHEN (SUM(CASE WHEN LB2.TRANS_MODE = 'DR' " +
                               "               THEN LB2.AMOUNT ELSE - LB2.AMOUNT END) >= 0 ) " +
                               "     THEN 'DR' ELSE 'CR' END AS 'TRANSMODE' " +
                               "FROM MASTER_LEDGER AS ML " +
                               "INNER JOIN MASTER_LEDGER_GROUP AS LG " +
                               "ON ML.GROUP_ID = LG.GROUP_ID " +
                               "INNER JOIN " +
                               "     (SELECT LB.BALANCE_DATE, LB.PROJECT_ID, LB.LEDGER_ID, LB.AMOUNT, LB.TRANS_MODE " +
                               "      FROM LEDGER_BALANCE AS LB " +
                               "      LEFT JOIN (SELECT LBA.PROJECT_ID, LBA.LEDGER_ID, MAX(LBA.BALANCE_DATE) AS BAL_DATE " +
                               "                 FROM LEDGER_BALANCE LBA " +
                               "                 WHERE 1 = 1 {AND LBA.BALANCE_DATE <= ?BALANCE_DATE} " +
                               "                 GROUP BY LBA.PROJECT_ID, LBA.LEDGER_ID) AS LB1 " +
                               "      ON LB.PROJECT_ID = LB1.PROJECT_ID " +
                               "      AND LB.LEDGER_ID = LB1.LEDGER_ID " +
                               "      WHERE LB.PROJECT_ID IN (?PROJECT_ID) " +
                               "      AND LB.BALANCE_DATE = LB1.BAL_DATE) LB2 " +
                               "ON ML.LEDGER_ID = LB2.LEDGER_ID " +
                               "LEFT JOIN MASTER_BANK_ACCOUNT MBA " +
                               "ON MBA.LEDGER_ID=ML.LEDGER_ID " +
                               "LEFT JOIN MASTER_BANK MB " +
                               "ON MB.BANK_ID=MBA.BANK_ID " +
                               "WHERE LG.GROUP_ID IN (?GROUP_ID) AND ML.STATUS=0 " +
                               "GROUP BY LG.GROUP_ID, LG.GROUP_CODE, LG.LEDGER_GROUP,ML.LEDGER_NAME ) T WHERE T.AMOUNT>0 AND T.TRANSMODE != 'DR'";
                        break;
                    }

                case SQLCommand.VoucherTransDetails.TransFDCBalance:
                    {

                        //changed by sugan-- to load the negative FDs
                        query = "SELECT ML.LEDGER_NAME AS BASE_LEDGER_NAME, FDA.FD_ACCOUNT_NUMBER AS 'LEDGER_NAME',\n" +
                            "       --    CONCAT(MBK.BANK, ' (', MBK.BRANCH, ')') AS BANK,\n" +
                            "       --  MPR.PROJECT,MPR.PROJECT_ID,\n" +
                            "       (FDA.AMOUNT + IFNULL(FDRO.REINVESTED_AMOUNT, 0) + IFNULL(FDR.REINVESTED_AMOUNT, 0)+\n" +
                            "        IFNULL(FDRO.ACCUMULATED_INTEREST_AMOUNT, 0) + IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0)) -\n" +
                            "        (IFNULL(FDRO.WITHDRAWAL_AMOUNT, 0) + IFNULL(FDR.WITHDRAWAL_AMOUNT, 0)) AS AMOUNT,\n" +
                            "       CASE\n" +
                            "         WHEN (FDA.AMOUNT + IFNULL(FDRO.REINVESTED_AMOUNT, 0) + IFNULL(FDR.REINVESTED_AMOUNT, 0)+\n" +
                            "         IFNULL(FDRO.ACCUMULATED_INTEREST_AMOUNT, 0) + IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0)) -\n" +
                            "          (IFNULL(FDRO.WITHDRAWAL_AMOUNT, 0) + IFNULL(FDR.WITHDRAWAL_AMOUNT, 0)) >= 0 THEN\n" +
                            "          'DR'\n" +
                            "         ELSE\n" +
                            "          'CR'\n" +
                            "       END AS 'TRANSMODE', IFNULL(MC.CURRENCY_SYMBOL, '') AS CURRENCY_SYMBOL\n" +
                           "\n" +
                           "  FROM FD_ACCOUNT AS FDA\n" +
                           "  LEFT JOIN (SELECT FD_ACCOUNT_ID,\n" +
                           "                 MAX(MATURITY_DATE) AS MATURITY_DATE,\n" +
                           "                 MAX(RENEWAL_DATE) AS RENEWAL_DATE,\n" +
                           "                 SUM(IF(RENEWAL_TYPE = 'ACI', 0, INTEREST_AMOUNT)) AS INTEREST_AMOUNT,\n" +
                           "                 SUM(IF(RENEWAL_TYPE = 'ACI', INTEREST_AMOUNT, 0)) AS ACCUMULATED_INTEREST_AMOUNT,\n" +
                           "                 SUM(REINVESTED_AMOUNT) AS REINVESTED_AMOUNT,\n" +
                           "                 SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT\n" +
                           "              FROM FD_RENEWAL\n" +
                           "              WHERE STATUS = 1 AND RENEWAL_DATE < ?BALANCE_DATE GROUP BY FD_ACCOUNT_ID) AS FDRO\n" +
                           "    ON FDA.FD_ACCOUNT_ID = FDRO.FD_ACCOUNT_ID\n" +
                           "\n" +
                           "  LEFT JOIN (SELECT FD_ACCOUNT_ID,\n" +
                           "                 MAX(MATURITY_DATE) AS MATURITY_DATE,\n" +
                           "                 MAX(RENEWAL_DATE) AS RENEWAL_DATE,\n" +
                           "                 INTEREST_RATE,\n" +
                           "                 SUM(IF(RENEWAL_TYPE = 'ACI', 0, INTEREST_AMOUNT)) AS INTEREST_AMOUNT,\n" +
                           "                 SUM(IF(RENEWAL_TYPE = 'ACI', INTEREST_AMOUNT, 0)) AS ACCUMULATED_INTEREST_AMOUNT,\n" +
                           "                 SUM(REINVESTED_AMOUNT) AS REINVESTED_AMOUNT,\n" +
                           "                 SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT\n" +
                           "               FROM FD_RENEWAL\n" +
                           "               WHERE STATUS = 1 AND RENEWAL_DATE BETWEEN ?BALANCE_DATE AND ?BALANCE_DATE\n" +
                           "               GROUP BY FD_ACCOUNT_ID) AS FDR ON FDA.FD_ACCOUNT_ID = FDR.FD_ACCOUNT_ID\n" +
                           "  LEFT JOIN MASTER_BANK AS MBK ON FDA.BANK_ID = MBK.BANK_ID\n" +
                           "  LEFT JOIN MASTER_PROJECT MPR ON FDA.PROJECT_ID = MPR.PROJECT_ID\n" +
                           "  LEFT JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = FDA.LEDGER_ID\n" +
                           "  LEFT JOIN MASTER_COUNTRY MC ON MC.COUNTRY_ID = ML.CUR_COUNTRY_ID\n" +
                           " WHERE FDA.STATUS = 1 { AND FDA.INVESTMENT_DATE <= ?BALANCE_DATE\n} {AND FDA.PROJECT_ID = ?PROJECT_ID}\n" +
                           "   AND (FDA.AMOUNT + IFNULL(FDRO.REINVESTED_AMOUNT, 0) + IFNULL(FDR.REINVESTED_AMOUNT, 0)+\n" +
                           "   IFNULL(FDRO.ACCUMULATED_INTEREST_AMOUNT, 0) + IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0)) -\n" +
                           "   (IFNULL(FDRO.WITHDRAWAL_AMOUNT, 0) + IFNULL(FDR.WITHDRAWAL_AMOUNT, 0)) != 0";// changed by sugan --to load the negative FDs
                        break;
                    }

                #region Voucher Router Analyzer
                case SQLCommand.VoucherTransDetails.VoucherRouterAnalyzerStock:
                    {
                        query = "SELECT @SET := (SELECT PURCHASE_ID\n" +
                                "          FROM STOCK_MASTER_PURCHASE\n" +
                                "         WHERE VOUCHER_ID = ?VOUCHER_ID)AS PURCHASE_ID,\n" +
                                "       @PURCHASE_RETURN := (SELECT RETURN_ID\n" +
                                "          FROM STOCK_MASTER_PURCHASE_RETURNS\n" +
                                "         WHERE VOUCHER_ID = ?VOUCHER_ID) AS RETURN_ID,\n" +
                                "       @SALES_ID:= (SELECT SALES_ID\n" +
                                "          FROM STOCK_MASTER_SOLD_UTILIZED\n" +
                                "         WHERE VOUCHER_ID = ?VOUCHER_ID) AS SALES_ID;";
                        break;
                    }

                case SQLCommand.VoucherTransDetails.VoucherRouterAnalyzerAsset:
                    {
                        query = "SELECT @SET:= (SELECT PURCHASE_ID\n" +
                                "          FROM ASSET_PURCHASE_MASTER\n" +
                                "         WHERE VOUCHER_ID = ?VOUCHER_ID) AS PURCHASE_ID,\n" +
                                "       @PURCHASE_RETURN :=(SELECT SALES_ID\n" +
                                "        FROM ASSET_SALES_MASTER\n" +
                                "        WHERE VOUCHER_ID = ?VOUCHER_ID) AS SALES_ID";
                        break;
                    }
                case SQLCommand.VoucherTransDetails.VoucherRouterAnalyzerPayRole:
                    {
                        //query = "SELECT (SELECT PRLOANGETID FROM PRLOANGET WHERE PRLOANGETID =?VOUCHER_ID) AS ISSUE_LOAN,\n" +
                        //        "       (SELECT POST_ID FROM PAYROLL_FINANCE WHERE POST_ID = ?VOUCHER_ID) AS POST_VOUCHER";

                        query = "SELECT VOUCHER_ID FROM PAYROLL_VOUCHER WHERE VOUCHER_ID = ?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherTransDetails.FetchAssetCashBankDetails:
                    {
                        //query = "SELECT VT.VOUCHER_ID,\n" +
                        //        "       SEQUENCE_NO,\n" +
                        //        "       IF(TRANS_MODE = 'CR', '1', '2') AS SOURCE,\n" +
                        //        "       VT.LEDGER_ID,\n" +
                        //        "       AMOUNT,\n" +
                        //        "       TRANS_MODE,\n" +
                        //        "       ML.GROUP_ID,\n" +
                        //        "       LEDGER_FLAG,\n" +
                        //        "       CHEQUE_NO,\n" +
                        //        "       MATERIALIZED_ON,\n" +
                        //        "       VT.STATUS,\n" +
                        //        "       TF.ACT_AMT,\n" +
                        //        "       '' AS LEDGER_BALANCE,\n" +
                        //        "       AMOUNT AS TEMP_AMOUNT,\n" +
                        //        "       VT.LEDGER_ID AS TEMP_LEDGER_ID,\n" +
                        //        "       AMOUNT AS BASE_AMOUNT\n" +
                        //        "\n" +
                        //        "  FROM VOUCHER_TRANS VT\n" +
                        //        " INNER JOIN VOUCHER_MASTER_TRANS VM\n" +
                        //        "    ON VT.VOUCHER_ID = VM.VOUCHER_ID\n" +
                        //        " INNER JOIN MASTER_LEDGER ML\n" +
                        //        "    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                        //        "  JOIN (SELECT AST.AMOUNT AS TAMOUNT,\n" +
                        //        "               SUM(IF((GAIN_AMOUNT + LOSS_AMOUNT) > 0, 1, 0)) AS ACT_AMT\n" +
                        //        "          FROM ASSET_IN_OUT_MASTER AIM\n" +
                        //        "         INNER JOIN ASSET_IN_OUT_DETAIL AID\n" +
                        //        "            ON AIM.IN_OUT_ID = AID.IN_OUT_ID\n" +
                        //        "         INNER JOIN ASSET_TRANS AST\n" +
                        //        "            ON AST.IN_OUT_DETAIL_ID = AID.IN_OUT_DETAIL_ID\n" +
                        //        "         WHERE AIM.VOUCHER_ID = ?VOUCHER_ID) AS TF\n" +
                        //        " WHERE VT.VOUCHER_ID = ?VOUCHER_ID  GROUP BY VOUCHER_ID,SEQUENCE_NO\n" +
                        //        " ORDER BY SEQUENCE_NO DESC";

                        query = "SELECT VT.VOUCHER_ID,\n" +
                                "       SEQUENCE_NO,\n" +
                                "       IF(TRANS_MODE = 'CR', '1', '2') AS SOURCE,\n" +
                                "       VT.LEDGER_ID,\n" +
                                "       AMOUNT,\n" +
                                "       TRANS_MODE,\n" +
                                "       ML.GROUP_ID,\n" +
                                "       LEDGER_FLAG,\n" +
                                "       CHEQUE_NO,\n" +
                                "       MATERIALIZED_ON,\n" +
                                "       VT.STATUS,\n" +
                                "       '' AS LEDGER_BALANCE,\n" +
                                "       AMOUNT AS TEMP_AMOUNT,\n" +
                                "       VT.LEDGER_ID AS TEMP_LEDGER_ID,\n" +
                                "       AMOUNT AS BASE_AMOUNT\n" +
                                "\n" +
                                "  FROM VOUCHER_TRANS VT\n" +
                                " INNER JOIN VOUCHER_MASTER_TRANS VM\n" +
                                "    ON VT.VOUCHER_ID = VM.VOUCHER_ID\n" +
                                " INNER JOIN MASTER_LEDGER ML\n" +
                                "    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                                " WHERE VT.VOUCHER_ID = ?VOUCHER_ID\n" +
                                "   AND (GROUP_ID IN (12, 13) OR\n" +
                                "       (IS_ASSET_GAIN_LEDGER = 1 OR IS_ASSET_LOSS_LEDGER = 1))\n" +
                                " GROUP BY VOUCHER_ID, SEQUENCE_NO\n" +
                                " ORDER BY SEQUENCE_NO DESC;";

                        break;
                    }

                case SQLCommand.VoucherTransDetails.FetchAssetInsuranceAMCDetails:
                    {
                        query = "SELECT VT.VOUCHER_ID,\n" +
                                "       SEQUENCE_NO,\n" +
                                "       IF(TRANS_MODE = 'CR', '1', '2') AS SOURCE,\n" +
                                "       VT.LEDGER_ID,\n" +
                                "       AMOUNT,\n" +
                                "       TRANS_MODE,\n" +
                                "       ML.GROUP_ID,\n" +
                                "       LEDGER_FLAG,\n" +
                                "       CHEQUE_NO,\n" +
                                "       MATERIALIZED_ON,\n" +
                                "       VT.STATUS,\n" +
                                "       '' AS LEDGER_BALANCE,\n" +
                                "       AMOUNT AS TEMP_AMOUNT,\n" +
                                "       VT.LEDGER_ID AS TEMP_LEDGER_ID,\n" +
                                "       AMOUNT AS BASE_AMOUNT\n" +
                                "\n" +
                                "  FROM VOUCHER_TRANS VT\n" +
                                " INNER JOIN VOUCHER_MASTER_TRANS VM\n" +
                                "    ON VT.VOUCHER_ID = VM.VOUCHER_ID\n" +
                                " INNER JOIN MASTER_LEDGER ML\n" +
                                "    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                                " WHERE VT.VOUCHER_ID = ?VOUCHER_ID\n" +
                                " GROUP BY VOUCHER_ID, SEQUENCE_NO\n" +
                                " ORDER BY SEQUENCE_NO DESC;";
                        break;
                    }


                case SQLCommand.VoucherTransDetails.FetchAssetCashBankDetailsForPurchase:
                    {
                        query = "SELECT VT.VOUCHER_ID,\n" +
                                "       SEQUENCE_NO,\n" +
                                "       IF(TRANS_MODE = 'CR', '1', '2') AS SOURCE,\n" +
                                "       VT.LEDGER_ID,\n" +
                                "       AMOUNT,\n" +
                                "       TRANS_MODE,ML.GROUP_ID,\n" +
                                "       LEDGER_FLAG,\n" +
                                "       CHEQUE_NO,\n" +
                                "       MATERIALIZED_ON,\n" +
                                "       VT.STATUS,\n" +
                                "       '' AS LEDGER_BALANCE,\n" +
                                "       AMOUNT AS TEMP_AMOUNT,\n" +
                                "       VT.LEDGER_ID AS TEMP_LEDGER_ID,\n" +
                                "       AMOUNT AS BASE_AMOUNT\n" +
                                "  FROM VOUCHER_TRANS VT\n" +
                                " INNER JOIN VOUCHER_MASTER_TRANS VM\n" +
                                "    ON VT.VOUCHER_ID = VM.VOUCHER_ID\n" +
                                " INNER JOIN MASTER_LEDGER ML\n" +
                                "    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                                " WHERE VT.VOUCHER_ID = ?VOUCHER_ID\n" +
                        "   AND IF(VM.VOUCHER_TYPE = ?VOUCHER_TYPE,\n" +
                        "          VT.TRANS_MODE = ?TRANS_MODE,\n" +
                        "          ML.GROUP_ID IN (12, 13));";
                        break;
                    }
                case SQLCommand.VoucherTransDetails.UpdateSubLedgerVouchers:
                    {
                        query = "INSERT INTO VOUCHER_SUB_LEDGER_TRANS (VOUCHER_ID, SEQUENCE_NO, LEDGER_ID, SUB_LEDGER_ID, AMOUNT, TRANS_MODE)\n" +
                                        "VALUES(?VOUCHER_ID, ?SEQUENCE_NO, ?LEDGER_ID, ?SUB_LEDGER_ID, ?AMOUNT, ?TRANS_MODE);";
                        break;
                    }
                case SQLCommand.VoucherTransDetails.DeleteSubLedgerVouchers:
                    {
                        query = "DELETE FROM VOUCHER_SUB_LEDGER_TRANS WHERE VOUCHER_ID = ?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherTransDetails.ChequeNumberExists:
                    {
                        query = "SELECT COUNT(*) AS CHEQUE_NO FROM VOUCHER_MASTER_TRANS VM\n" +
                                "INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VM.VOUCHER_ID AND VT.CHEQUE_NO=?CHEQUE_NO\n" +
                                "WHERE VM.STATUS=1 AND VM.VOUCHER_ID<>?VOUCHER_ID AND VT.CHEQUE_NO=?CHEQUE_NO";
                        break;
                    }
                case SQLCommand.VoucherTransDetails.FetchFundTransferList:
                    {
                        query = @"SELECT FUND_TRANSFER_TYPE_ID, FUND_TRANSFER_TYPE_NAME FROM MASTER_FUND_TRANSFER_TYPE";
                        break;
                    }
                #endregion
            }

            return query;
        }
        #endregion Bank SQL
    }
}
