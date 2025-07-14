using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Utility;

namespace Bosco.SQL
{
    public class TDSPaymentSQL : IDatabaseQuery
    {
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        public string GetQuery(DAO.Data.DataCommandArguments dataCommandArgs, ref DAO.Data.SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.TDSPayment).FullName)
            {
                query = GetTDSPayment();
            }

            sqlType = this.sqlType;
            return query;
        }

        private string GetTDSPayment()
        {
            string query = "";
            SQLCommand.TDSPayment sqlCommandId = (SQLCommand.TDSPayment)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.TDSPayment.Add:
                    {
                        query = "INSERT INTO TDS_PAYMENT\n" +
                        "  (PAYMENT_DATE, PROJECT_ID, PAYMENT_LEDGER_ID, VOUCHER_ID)\n" +
                        "VALUES\n" +
                        "  (\n" +
                        "   ?PAYMENT_DATE,\n" +
                        "   ?PROJECT_ID,\n" +
                        "   ?PAYMENT_LEDGER_ID,\n" +
                        "   ?VOUCHER_ID)";

                        break;
                    }
                case SQLCommand.TDSPayment.Update:
                    {
                        query = "UPDATE TDS_PAYMENT\n" +
                        "   SET PAYMENT_DATE      = ?PAYMENT_DATE,\n" +
                        "       PROJECT_ID        = ?PROJECT_ID,\n" +
                        "       PAYMENT_LEDGER_ID = ?PAYMENT_LEDGER_ID,\n" +
                        "       VOUCHER_ID        = ?VOUCHER_ID\n" +
                        " WHERE TDS_PAYMENT_ID = ?TDS_PAYMENT_ID";

                        break;
                    }
                case SQLCommand.TDSPayment.Delete:
                    {
                        query = "UPDATE TDS_PAYMENT SET IS_DELETED=0 WHERE TDS_PAYMENT_ID=?TDS_PAYMENT_ID";
                        break;
                    }

                case SQLCommand.TDSPayment.DeleteTDSPayment:
                    {
                        query = "DELETE FROM TDS_PAYMENT WHERE TDS_PAYMENT_ID=?TDS_PAYMENT_ID";
                        break;
                    }

                case SQLCommand.TDSPayment.FetchAll:
                    {
                        query = "SELECT TDS_PAYMENT_ID,\n" +
                        "       PAYMENT_DATE,\n" +
                        "       PROJECT_ID,\n" +
                        "       PAYMENT_LEDGER_ID,\n" +
                        "       VOUCHER_ID\n" +
                        "  FROM TDS_PAYMENT";
                        break;
                    }
                case SQLCommand.TDSPayment.Fetch:
                    {
                        query = "SELECT T.LEDGER_ID,\n" +
                         "       T.PROJECT_ID,\n" +
                         "       T.BOOKING_ID,\n" +
                         "       T.VOUCHER_ID,\n" +
                         "       T.BOOKING_DETAIL_ID,\n" +
                         "       T.DEDUCTION_DETAIL_ID,\n" +
                         "       T.TDS_PAYMENT_ID,\n" +
                         "       T.TDS_PAYMENT_DETAIL_ID,\n" +
                         "       T.PAYMENT_LEDGER_ID,\n" +
                         "       T.FLAG ,\n" +
                         "       T.PROJECT,\n" +
                         "       ROUND(T.AMOUNT, 2) AS 'NET AMOUNT',\n" +
                         "       T.ASSESS_AMOUNT,\n" +
                         "       T.VOUCHER_NO,\n" +
                         "       T.BOOKING_DATE,\n" +
                         "       T.LEDGER_NAME AS 'TDS_LEDGER',\n" +
                         "       T.NAME AS 'NATURE_OF_PAYMENT',\n" +
                         "       TPD.LEDGER_NAME AS 'PARTY_LEDGER',\n" +
                         "       T.PAID_AMOUNT AS 'AMOUNT',\n" +
                         "       T.TAX_AMOUNT AS 'PAID_AMOUNT',\n" +
                         "       0 AS TEMP_AMOUNT,\n" +
                         "       1 AS SOURCE,\n" +
                         "       T.TRANS_MODE,\n" +
                         "       T.IS_TDS_DEDUCTED,\n" +
                         "       T.CHEQUE_NO,\n" +
                         "       T.MATERIALIZED_ON,\n" +
                         "       T.NARRATION\n" +
                         "  FROM (SELECT ML.LEDGER_ID,\n" +
                         "               MLG.GROUP_ID,\n" +
                         "               TP.PROJECT_ID,\n" +
                         "               B.BOOKING_ID,\n" +
                         "               TBD.BOOKING_DETAIL_ID,\n" +
                         "               TDD.DEDUCTION_DETAIL_ID,\n" +
                         "               TPD.TDS_PAYMENT_ID,\n" +
                         "               TPD.TDS_PAYMENT_DETAIL_ID,\n" +
                         "               TPD.LEDGER_ID AS 'PAYMENT_LEDGER_ID',\n" +
                         "               TPD.FLAG ,\n" +
                         "               TP.IS_DELETED,\n" +
                         "               VMT.VOUCHER_NO,\n" +
                         "               BOOKING_DATE,\n" +
                         "               ML.LEDGER_NAME,\n" +
                         "               TNP.NAME,\n" +
                         "               TDD.TAX_AMOUNT,\n" +
                         "               B.AMOUNT,\n" +
                         "               TPD.PAID_AMOUNT,\n" +
                         "               TBD.ASSESS_AMOUNT,\n" +
                         "               TP.VOUCHER_ID,\n" +
                         "               PROJECT,\n" +
                         "               'DR' AS 'TRANS_MODE',\n" +
                         "               TBD.IS_TDS_DEDUCTED,\n" +
                         "               '' AS 'CHEQUE_NO',\n" +
                         "               '' AS 'MATERIALIZED_ON',\n" +
                         "               VMT.NARRATION \n" +
                         "          FROM TDS_BOOKING B\n" +
                         "          LEFT JOIN VOUCHER_MASTER_TRANS VMT\n" +
                         "            ON B.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                         "           AND VOUCHER_TYPE = 'JN'\n" +
                         "          LEFT JOIN VOUCHER_TRANS VT\n" +
                         "            ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                         "          LEFT JOIN TDS_BOOKING_DETAIL AS TBD\n" +
                         "            ON B.BOOKING_ID = TBD.BOOKING_ID\n" +
                         "          LEFT JOIN TDS_DEDUCTION_DETAIL AS TDD\n" +
                         "            ON TBD.BOOKING_DETAIL_ID = TDD.BOOKING_DETAIL_ID\n" +
                         "          LEFT JOIN TDS_PAYMENT_DETAIL AS TPD\n" +
                         "            ON TDD.DEDUCTION_DETAIL_ID = TPD.DEDUCTION_DETAIL_ID\n" +
                         "          LEFT JOIN TDS_PAYMENT AS TP\n" +
                         "            ON TPD.TDS_PAYMENT_ID = TP.TDS_PAYMENT_ID\n" +
                         "          LEFT JOIN TDS_NATURE_PAYMENT AS TNP\n" +
                         "            ON TBD.NATURE_OF_PAYMENT_ID = TNP.NATURE_PAY_ID\n" +
                         "          LEFT JOIN MASTER_LEDGER ML\n" +
                         "            ON ML.LEDGER_ID = TDD.TAX_LEDGER_ID\n" +
                         "          LEFT JOIN MASTER_LEDGER_GROUP MLG\n" +
                         "            ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                         "          LEFT JOIN MASTER_PROJECT MP\n" +
                         "            ON MP.PROJECT_ID = B.PROJECT_ID\n" +
                         "         WHERE B.IS_DELETED = 1\n" +
                         "          { AND ML.IS_TDS_LEDGER = ?STATUS } ) AS T\n" +
                         "  JOIN (SELECT ML.LEDGER_ID,\n" +
                         "               MLG.GROUP_ID,\n" +
                         "               B.PROJECT_ID,\n" +
                         "               B.BOOKING_ID,\n" +
                         "               TBD.BOOKING_DETAIL_ID,\n" +
                         "               TDD.DEDUCTION_DETAIL_ID,\n" +
                         "               TPD.TDS_PAYMENT_ID,\n" +
                         "               TPD.LEDGER_ID AS 'PAYMENT_LEDGER_ID',\n" +
                         "               TPD.FLAG ,\n" +
                         "               TP.IS_DELETED,\n" +
                         "               VMT.VOUCHER_NO,\n" +
                         "               BOOKING_DATE,\n" +
                         "               ML.LEDGER_NAME,\n" +
                         "               TNP.NAME,\n" +
                         "               TDD.TAX_AMOUNT,\n" +
                         "               B.AMOUNT,\n" +
                         "               TBD.ASSESS_AMOUNT,\n" +
                         "               VMT.VOUCHER_ID,\n" +
                         "               PROJECT,\n" +
                         "               'DR' AS 'TRANS_MODE',\n" +
                         "               TBD.IS_TDS_DEDUCTED,\n" +
                         "               '' AS 'CHEQUE_NO',\n" +
                         "               '' AS 'MATERIALIZED_ON'\n" +
                         "          FROM TDS_BOOKING B\n" +
                         "          LEFT JOIN VOUCHER_MASTER_TRANS VMT\n" +
                         "            ON B.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                         "           AND VOUCHER_TYPE = 'JN'\n" +
                         "          LEFT JOIN VOUCHER_TRANS VT\n" +
                         "            ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                         "          LEFT JOIN TDS_BOOKING_DETAIL AS TBD\n" +
                         "            ON B.BOOKING_ID = TBD.BOOKING_ID\n" +
                         "          LEFT JOIN TDS_DEDUCTION_DETAIL AS TDD\n" +
                         "            ON TBD.BOOKING_DETAIL_ID = TDD.BOOKING_DETAIL_ID\n" +
                         "          LEFT JOIN TDS_PAYMENT_DETAIL AS TPD\n" +
                         "            ON TDD.DEDUCTION_DETAIL_ID = TPD.DEDUCTION_DETAIL_ID\n" +
                         "          LEFT JOIN TDS_PAYMENT AS TP\n" +
                         "            ON TPD.TDS_PAYMENT_ID = TP.TDS_PAYMENT_ID\n" +
                         "          LEFT JOIN TDS_NATURE_PAYMENT AS TNP\n" +
                         "            ON TBD.NATURE_OF_PAYMENT_ID = TNP.NATURE_PAY_ID\n" +
                         "          LEFT JOIN MASTER_LEDGER ML\n" +
                         "            ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                         "          LEFT JOIN MASTER_LEDGER_GROUP MLG\n" +
                         "            ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                         "          LEFT JOIN MASTER_PROJECT MP\n" +
                         "            ON MP.PROJECT_ID = B.PROJECT_ID\n" +
                         "         WHERE B.IS_DELETED = 1\n" +
                         "           AND MLG.GROUP_ID = 26\n" +
                         "           { AND ML.IS_TDS_LEDGER = ?STATUS } \n" +
                         "         ORDER BY VMT.VOUCHER_NO) AS TPD\n" +
                         "    ON T.BOOKING_ID = TPD.BOOKING_ID\n" +
                         " WHERE T.PROJECT_ID = ?PROJECT_ID\n" +
                         "   AND T.TDS_PAYMENT_ID =?TDS_PAYMENT_ID\n" +
                         "   AND T.IS_DELETED = 1\n" +
                         " GROUP BY T.BOOKING_DETAIL_ID, T.NAME\n" +
                         " ORDER BY T.VOUCHER_NO, T.BOOKING_DATE";
                        break;
                    }
                case SQLCommand.TDSPayment.FetchPendingTDSPayment:
                    {
                        query = "SELECT T.LEDGER_ID,\n" +
                         "       T.PROJECT_ID,\n" +
                         "       T.BOOKING_ID,\n" +
                         "       T.VOUCHER_ID,\n" +
                         "       T.BOOKING_DETAIL_ID,\n" +
                         "       T.DEDUCTION_DETAIL_ID,\n" +
                         "       T.PROJECT,\n" +
                         "       ROUND(T.AMOUNT, 2) AS 'NET AMOUNT',\n" +
                         "       T.ASSESS_AMOUNT,\n" +
                         "       T.VOUCHER_NO,\n" +
                         "       T.BOOKING_DATE,\n" +
                         "       T.LEDGER_NAME AS 'TDS_LEDGER',\n" +
                         "       T.NAME AS 'NATURE_OF_PAYMENT',\n" +
                         "       TPD.LEDGER_NAME AS 'PARTY_LEDGER',\n" +
                         "       T.TAX_AMOUNT AS 'TAX_AMOUNT',\n" +
                         "       IFNULL(TPDD.PAID_AMOUNT, T.TAX_AMOUNT) AS 'AMOUNT',\n" +
                         "       IFNULL(TPDD.PAID_AMOUNT, T.TAX_AMOUNT) AS 'PAID_AMOUNT',\n" +
                         "        0 AS TEMP_AMOUNT,\n" +
                         "       1 AS SOURCE,\n" +
                         "       T.TRANS_MODE,\n" +
                         "       T.IS_TDS_DEDUCTED,\n" +
                         "       T.CHEQUE_NO,\n" +
                         "       T.MATERIALIZED_ON\n" +
                         "  FROM (SELECT ML.LEDGER_ID,\n" +
                         "               MLG.GROUP_ID,\n" +
                         "               B.PROJECT_ID,\n" +
                         "               B.BOOKING_ID,\n" +
                         "               TBD.BOOKING_DETAIL_ID,\n" +
                         "               TDD.DEDUCTION_DETAIL_ID,\n" +
                         "               VMT.VOUCHER_NO,\n" +
                         "               BOOKING_DATE,\n" +
                         "               ML.LEDGER_NAME,\n" +
                         "               VMT.STATUS,\n" +
                         "               TNP.NAME,\n" +
                         "               TDD.TAX_AMOUNT,\n" +
                         "               B.AMOUNT,\n" +
                         "               TBD.ASSESS_AMOUNT,\n" +
                         "               VMT.VOUCHER_ID,\n" +
                         "               PROJECT,\n" +
                         "               'DR' AS 'TRANS_MODE',\n" +
                         "               TBD.IS_TDS_DEDUCTED,\n" +
                         "               '' AS 'CHEQUE_NO',\n" +
                         "               '' AS 'MATERIALIZED_ON'\n" +
                         "          FROM TDS_BOOKING B\n" +
                         "          LEFT JOIN VOUCHER_MASTER_TRANS VMT\n" +
                         "            ON B.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                         "           AND VOUCHER_TYPE = 'JN'\n" +
                         "          LEFT JOIN VOUCHER_TRANS VT\n" +
                         "            ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                         "          LEFT JOIN TDS_BOOKING_DETAIL AS TBD\n" +
                         "            ON B.BOOKING_ID = TBD.BOOKING_ID\n" +
                         "          LEFT JOIN TDS_DEDUCTION_DETAIL AS TDD\n" +
                         "            ON TBD.BOOKING_DETAIL_ID = TDD.BOOKING_DETAIL_ID\n" +
                         "          LEFT JOIN TDS_NATURE_PAYMENT AS TNP\n" +
                         "            ON TBD.NATURE_OF_PAYMENT_ID = TNP.NATURE_PAY_ID\n" +
                         "          LEFT JOIN MASTER_LEDGER ML\n" +
                         "            ON ML.LEDGER_ID = TDD.TAX_LEDGER_ID\n" +
                         "           AND IS_TDS_LEDGER = 1\n" +
                         "          LEFT JOIN MASTER_LEDGER_GROUP MLG\n" +
                         "            ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                         "          LEFT JOIN MASTER_PROJECT MP\n" +
                         "            ON MP.PROJECT_ID = B.PROJECT_ID\n" +
                         "         WHERE B.IS_DELETED = 1) AS T\n" +
                         "  LEFT JOIN (SELECT ML.LEDGER_ID,\n" +
                         "                    MLG.GROUP_ID,\n" +
                         "                    B.PROJECT_ID,\n" +
                         "                    B.BOOKING_ID,\n" +
                         "                    TBD.BOOKING_DETAIL_ID,\n" +
                         "                    TDD.DEDUCTION_DETAIL_ID,\n" +
                         "                    TDD.TAX_LEDGER_ID,\n" +
                         "                    VMT.VOUCHER_NO,\n" +
                         "                    BOOKING_DATE,\n" +
                         "                    ML.LEDGER_NAME,\n" +
                         "                    TNP.NAME,\n" +
                         "                    TDD.TAX_AMOUNT,\n" +
                         "                    B.AMOUNT,\n" +
                         "                    TBD.ASSESS_AMOUNT,\n" +
                         "                    VMT.VOUCHER_ID,\n" +
                         "                    PROJECT,\n" +
                         "                    'DR' AS 'TRANS_MODE',\n" +
                         "                    TBD.IS_TDS_DEDUCTED,\n" +
                         "                    '' AS 'CHEQUE_NO',\n" +
                         "                    '' AS 'MATERIALIZED_ON'\n" +
                         "               FROM TDS_BOOKING B\n" +
                         "               LEFT JOIN VOUCHER_MASTER_TRANS VMT\n" +
                         "                 ON B.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                         "                AND VOUCHER_TYPE = 'JN'\n" +
                         "               LEFT JOIN VOUCHER_TRANS VT\n" +
                         "                 ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                         "               LEFT JOIN TDS_BOOKING_DETAIL AS TBD\n" +
                         "                 ON B.BOOKING_ID = TBD.BOOKING_ID\n" +
                         "               LEFT JOIN TDS_DEDUCTION_DETAIL AS TDD\n" +
                         "                 ON TBD.BOOKING_DETAIL_ID = TDD.BOOKING_DETAIL_ID\n" +
                         "               LEFT JOIN TDS_NATURE_PAYMENT AS TNP\n" +
                         "                 ON TBD.NATURE_OF_PAYMENT_ID = TNP.NATURE_PAY_ID\n" +
                         "               LEFT JOIN MASTER_LEDGER ML\n" +
                         "                 ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                         "                AND IS_TDS_LEDGER = 1\n" +
                         "               LEFT JOIN MASTER_LEDGER_GROUP MLG\n" +
                         "                 ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                         "               LEFT JOIN MASTER_PROJECT MP\n" +
                         "                 ON MP.PROJECT_ID = B.PROJECT_ID\n" +
                         "              WHERE B.IS_DELETED = 1\n" +
                         "                AND MLG.GROUP_ID = 26\n" +
                         "              ORDER BY VMT.VOUCHER_NO) AS TPD\n" +
                         "    ON T.BOOKING_ID = TPD.BOOKING_ID\n" +
                         "  LEFT JOIN (SELECT TDD.DEDUCTION_DETAIL_ID,\n" +
                         "                    TP.PROJECT_ID,\n" +
                         "                    CASE\n" +
                         "                      WHEN TDD.TAX_AMOUNT = SUM(PAID_AMOUNT) THEN\n" +
                         "                       TDD.TAX_AMOUNT - SUM(PAID_AMOUNT)\n" +
                         "                      ELSE\n" +
                         "                       CASE\n" +
                         "                         WHEN TDD.TAX_AMOUNT <> SUM(PAID_AMOUNT) THEN\n" +
                         "                          TDD.TAX_AMOUNT - SUM(PAID_AMOUNT)\n" +
                         "                         ELSE\n" +
                         "                          TDD.TAX_AMOUNT\n" +
                         "                       END\n" +
                         "                    END AS 'PAID_AMOUNT'\n" +
                         "               FROM TDS_PAYMENT AS TP\n" +
                         "               LEFT JOIN TDS_PAYMENT_DETAIL AS TPD\n" +
                         "                 ON TP.TDS_PAYMENT_ID = TPD.TDS_PAYMENT_ID\n" +
                         "               LEFT JOIN TDS_DEDUCTION_DETAIL AS TDD\n" +
                         "                 ON TPD.DEDUCTION_DETAIL_ID = TDD.DEDUCTION_DETAIL_ID\n" +
                         "              WHERE TP.IS_DELETED=1 \n" +
                         "              GROUP BY TDD.DEDUCTION_DETAIL_ID) AS TPDD\n" +
                         "    ON T.DEDUCTION_DETAIL_ID = TPDD.DEDUCTION_DETAIL_ID\n" +
                         " WHERE T.PROJECT_ID = ?PROJECT_ID\n" +
                         "   AND T.BOOKING_DATE <= ?BOOKING_DATE\n" +
                         "   AND T.STATUS=1\n" +
                         "   AND T.LEDGER_ID=?LEDGER_ID \n" +
                         "   AND T.IS_TDS_DEDUCTED = 1\n" +
                         " GROUP BY T.BOOKING_DETAIL_ID, T.NAME\n" +
                         " ORDER BY  T.BOOKING_DATE,T.VOUCHER_NO";
                        break;
                    }
                case SQLCommand.TDSPayment.FetchTDSPayment:
                    {
                        query = "SELECT TP.TDS_PAYMENT_ID,\n" +
                        "       TP.PAYMENT_LEDGER_ID,\n" +
                        "       TPD.TDS_PAYMENT_DETAIL_ID,\n" +
                        "       TP.PROJECT_ID,\n" +
                        "       VMT.VOUCHER_ID,\n" +
                        "       TB.BOOKING_ID,\n" +
                        "       TBD.BOOKING_DETAIL_ID,\n" +
                        "       VMT.VOUCHER_DATE,\n" +
                        "       TDD.DEDUCTION_DETAIL_ID,\n" +
                        "       TNP.NATURE_PAY_ID,\n" +
                        "       VMT.VOUCHER_DATE,\n" +
                        "       VMT.VOUCHER_NO,\n" +
                        "       ML.LEDGER_NAME,\n" +
                        "       CT.CASHBANK,\n" +
                        "       'Payment' as VOUCHER_TYPE,\n" +
                        "       CONCAT(MP.PROJECT, CONCAT(' - ', MD.DIVISION)) AS 'PROJECT',\n" +
                        "       TNP.NAME AS 'NATURE_OF_PAYMENT',\n" +
                        "       CT.AMOUNT,\n" +
                        "       VMT.NARRATION\n" +
                        "  FROM VOUCHER_MASTER_TRANS AS VMT\n" +
                        "  LEFT JOIN VOUCHER_TRANS AS VT\n" +
                        "    ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        "  LEFT JOIN TDS_PAYMENT AS TP\n" +
                        "    ON VMT.VOUCHER_ID = TP.VOUCHER_ID\n" +
                        "  LEFT JOIN TDS_PAYMENT_DETAIL AS TPD\n" +
                        "    ON TP.TDS_PAYMENT_ID = TPD.TDS_PAYMENT_ID\n" +
                        "  LEFT JOIN TDS_DEDUCTION_DETAIL AS TDD\n" +
                        "    ON TPD.DEDUCTION_DETAIL_ID = TDD.DEDUCTION_DETAIL_ID\n" +
                        "  LEFT JOIN TDS_BOOKING_DETAIL AS TBD\n" +
                        "    ON TDD.BOOKING_DETAIL_ID = TBD.BOOKING_DETAIL_ID\n" +
                        "  LEFT JOIN TDS_BOOKING AS TB\n" +
                        "    ON TBD.BOOKING_ID = TB.BOOKING_ID\n" +
                        "  LEFT JOIN TDS_NATURE_PAYMENT AS TNP\n" +
                        "    ON TBD.NATURE_OF_PAYMENT_ID = TNP.NATURE_PAY_ID\n" +
                        "  LEFT JOIN MASTER_LEDGER AS ML\n" +
                        "    ON TB.PARTY_LEDGER_ID = ML.LEDGER_ID\n" +
                        "  LEFT JOIN MASTER_PROJECT AS MP\n" +
                        "    ON TB.PROJECT_ID = MP.PROJECT_ID\n" +
                        "  LEFT JOIN MASTER_DIVISION AS MD\n" +
                        "    ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                        "  LEFT JOIN (SELECT VMT.VOUCHER_ID,\n" +
                        "                    CASE\n" +
                        "                      WHEN ML.LEDGER_SUB_TYPE = 'BK' THEN\n" +
                        "                       CONCAT(CONCAT(ML.LEDGER_NAME, ' - '),\n" +
                        "                              CONCAT(MB.BANK, ' - '),\n" +
                        "                              MB.BRANCH)\n" +
                        "                      ELSE\n" +
                        "                       ML.LEDGER_NAME\n" +
                        "                    END AS CASHBANK,\n" +
                        "                    VT.AMOUNT\n" +
                        "               FROM VOUCHER_MASTER_TRANS VMT\n" +
                        "              INNER JOIN VOUCHER_TRANS VT\n" +
                        "                 ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        "               LEFT JOIN MASTER_LEDGER ML\n" +
                        "                 ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                        "               LEFT JOIN MASTER_LEDGER_GROUP MLG\n" +
                        "                 ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                        "               LEFT JOIN MASTER_BANK_ACCOUNT MBA\n" +
                        "                 ON MBA.LEDGER_ID = ML.LEDGER_ID\n" +
                        "               LEFT JOIN MASTER_BANK MB\n" +
                        "                 ON MB.BANK_ID = MBA.BANK_ID\n" +
                        "              WHERE MLG.GROUP_ID IN (12, 13)\n" +
                        "              GROUP BY VMT.VOUCHER_ID) AS CT\n" +
                        "    ON VMT.VOUCHER_ID = CT.VOUCHER_ID\n" +
                        " WHERE ML.IS_TDS_LEDGER = 1\n" +
                        "   AND VMT.STATUS = 1\n" +
                            //  "   AND VMT.VOUCHER_TYPE = 'JN'\n" +
                        "   AND VT.TRANS_MODE = 'DR'\n" +
                        "   AND VOUCHER_TYPE = 'PY'\n" +
                        "   AND TP.IS_DELETED=1 \n" +
                        "   AND TP.PROJECT_ID=?PROJECT_ID AND TP.PAYMENT_DATE BETWEEN ?BOOKING_DATE AND ?DATE_CLOSED \n" +
                        " GROUP BY VMT.VOUCHER_ID  ORDER BY TP.PAYMENT_DATE,VMT.VOUCHER_NO";
                        break;
                    }

                case SQLCommand.TDSPayment.FetchTDSPaymentDetail:
                    {
                        query = "SELECT VMT.VOUCHER_ID,\n" +
                        "       ML.LEDGER_NAME,\n" +
                        "       CASE TRANS_MODE\n" +
                        "         WHEN 'CR' THEN\n" +
                        "          'Credit'\n" +
                        "         ELSE\n" +
                        "          'Debit'\n" +
                        "       END AS TRANSMODE,\n" +
                        "       CASE TRANS_MODE\n" +
                        "         WHEN 'CR' THEN\n" +
                        "          FORMAT(VT.AMOUNT, 2)\n" +
                        "         ELSE\n" +
                        "          ''\n" +
                        "       END AS 'CREDIT',\n" +
                        "       CASE TRANS_MODE\n" +
                        "         WHEN 'DR' THEN\n" +
                        "          FORMAT(VT.AMOUNT, 2)\n" +
                        "         ELSE\n" +
                        "          ''\n" +
                        "       END AS 'DEBIT'\n" +
                        "  FROM VOUCHER_MASTER_TRANS AS VMT\n" +
                        "  LEFT JOIN VOUCHER_TRANS AS VT\n" +
                        "    ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        "  LEFT JOIN MASTER_LEDGER AS ML\n" +
                        "    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                        "  LEFT JOIN MASTER_PROJECT AS MP\n" +
                        "    ON VMT.PROJECT_ID = MP.PROJECT_ID\n" +
                        "  LEFT JOIN MASTER_BANK_ACCOUNT AS MBA\n" +
                        "    ON ML.LEDGER_ID = MBA.LEDGER_ID\n" +
                        " WHERE VMT.STATUS = 1\n" +
                            // "   AND VOUCHER_TYPE = 'JN'\n" +
                        "   AND VOUCHER_TYPE = 'PY'\n" +
                        "   AND VMT.VOUCHER_ID IN (?VOUCHER_ID)";
                        break;
                    }
                case SQLCommand.TDSPayment.FetchPaymentId:
                    {
                        query = "SELECT TDS_PAYMENT_ID,PAYMENT_LEDGER_ID,VOUCHER_ID FROM TDS_PAYMENT WHERE VOUCHER_ID= ?VOUCHER_ID";

                        //query = "SELECT TP.TDS_PAYMENT_ID,PAYMENT_LEDGER_ID, TP.VOUCHER_ID \n" +
                        //        "  FROM TDS_PAYMENT TP\n" +
                        //        " INNER JOIN TDS_PAYMENT_DETAIL TPD\n" +
                        //        "    ON TP.TDS_PAYMENT_ID = TPD.TDS_PAYMENT_ID\n" +
                        //        " INNER JOIN TDS_DEDUCTION_DETAIL TDD\n" +
                        //        "    ON TPD.DEDUCTION_DETAIL_ID = TDD.DEDUCTION_DETAIL_ID\n" +
                        //        "INNER JOIN TDS_DEDUCTION TD \n" +
                        //        "    ON TD.DEDUCTION_ID = TDD.DEDUCTION_ID \n" +
                        //        " INNER JOIN TDS_BOOKING_DETAIL TBD\n" +
                        //        "    ON TDD.BOOKING_DETAIL_ID = TBD.BOOKING_DETAIL_ID\n" +
                        //        " INNER JOIN TDS_BOOKING TB\n" +
                        //        "    ON TBD.BOOKING_ID = TB.BOOKING_ID\n" +
                        //        " WHERE TD.VOUCHER_ID = ?VOUCHER_ID";

                        break;
                    }

                case SQLCommand.TDSPayment.FetchBookingMappedPaymentId:
                    {
                        query = "SELECT TP.TDS_PAYMENT_ID,PAYMENT_LEDGER_ID, TP.VOUCHER_ID \n" +
                                "  FROM TDS_PAYMENT TP\n" +
                                " INNER JOIN TDS_PAYMENT_DETAIL TPD\n" +
                                "    ON TP.TDS_PAYMENT_ID = TPD.TDS_PAYMENT_ID\n" +
                                " INNER JOIN TDS_DEDUCTION_DETAIL TDD\n" +
                                "    ON TPD.DEDUCTION_DETAIL_ID = TDD.DEDUCTION_DETAIL_ID\n" +
                                "INNER JOIN TDS_DEDUCTION TD \n" +
                                "    ON TD.DEDUCTION_ID = TDD.DEDUCTION_ID \n" +
                                " INNER JOIN TDS_BOOKING_DETAIL TBD\n" +
                                "    ON TDD.BOOKING_DETAIL_ID = TBD.BOOKING_DETAIL_ID\n" +
                                " INNER JOIN TDS_BOOKING TB\n" +
                                "    ON TBD.BOOKING_ID = TB.BOOKING_ID\n" +
                                " WHERE TD.VOUCHER_ID = ?VOUCHER_ID";

                        break;
                    }


                case SQLCommand.TDSPayment.TDSChallanReport:
                    {
                        query = "SELECT VMT.VOUCHER_ID,\n" +
                        "       VMT.VOUCHER_DATE,\n" +
                        "       VMT.VOUCHER_NO,\n" +
                        "       VT.CHEQUE_NO,\n" +
                        "       VT.MATERIALIZED_ON,\n" +
                        "       VT.AMOUNT,\n" +
                        "       ML.LEDGER_NAME,\n" +
                        "       MB.BANK\n" +
                        "  FROM VOUCHER_MASTER_TRANS AS VMT\n" +
                        " INNER JOIN VOUCHER_TRANS AS VT\n" +
                        "    ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        " INNER JOIN TDS_PAYMENT AS TP\n" +
                        "    ON VT.LEDGER_ID = TP.PAYMENT_LEDGER_ID\n" +
                        "     AND VMT.VOUCHER_ID=TP.VOUCHER_ID\n" +
                        " INNER JOIN MASTER_LEDGER AS ML\n" +
                        "    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                        "  LEFT JOIN MASTER_BANK_ACCOUNT AS MBA\n" +
                        "    ON ML.LEDGER_ID = MBA.LEDGER_ID\n" +
                        "  LEFT JOIN MASTER_BANK AS MB\n" +
                        "    ON MBA.BANK_ID = MB.BANK_ID\n" +
                        " WHERE VMT.PROJECT_ID IN (?PROJECT_ID)\n" +
                        "   AND VMT.STATUS = 1\n" +
                        "   AND ML.STATUS = 0\n" +
                        "   AND TP.IS_DELETED = 1\n" +
                        "   AND VMT.VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                        " GROUP BY VMT.VOUCHER_ID";

                        break;
                    }
                case SQLCommand.TDSPayment.FetchTDSInterest:
                    {
                        query = "SELECT TP.TDS_PAYMENT_ID, LEDGER_ID, PAID_AMOUNT, FLAG\n" +
                        "  FROM TDS_PAYMENT AS TP\n" +
                        " INNER JOIN TDS_PAYMENT_DETAIL AS TPD\n" +
                        "    ON TP.TDS_PAYMENT_ID = TPD.TDS_PAYMENT_ID\n" +
                        " WHERE TP.TDS_PAYMENT_ID = ?TDS_PAYMENT_ID\n" +
                        "   AND TP.IS_DELETED = 1 AND TPD.FLAG>0";
                        break;
                    }
            }

            return query;
        }
    }
}
