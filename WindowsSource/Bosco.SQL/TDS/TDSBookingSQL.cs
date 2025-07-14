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
    public class TDSBookingSQL : IDatabaseQuery
    {
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        public string GetQuery(DAO.Data.DataCommandArguments dataCommandArgs, ref DAO.Data.SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.TDSBooking).FullName)
            {
                query = GetBooking();
            }

            sqlType = this.sqlType;
            return query;
        }

        #region SQL Script

        private string GetBooking()
        {
            string query = "";
            SQLCommand.TDSBooking sqlCommandId = (SQLCommand.TDSBooking)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.TDSBooking.Add:
                    {
                        query = "INSERT INTO TDS_BOOKING\n" +
                                "  (BOOKING_DATE,\n" +
                                "   PROJECT_ID,\n" +
                                "   EXPENSE_LEDGER_ID,\n" +
                                "   PARTY_LEDGER_ID,\n" +
                                "   DEDUCTEE_TYPE_ID,\n" +
                                "   AMOUNT,\n" +
                                "   VOUCHER_ID)\n" +
                                "VALUES\n" +
                                "  (?BOOKING_DATE,\n" +
                                "   ?PROJECT_ID,\n" +
                                "   ?EXPENSE_LEDGER_ID,\n" +
                                "   ?PARTY_LEDGER_ID,\n" +
                                "   ?DEDUCTEE_TYPE_ID,\n" +
                                "   ?AMOUNT,\n" +
                                "   ?VOUCHER_ID)";
                        break;
                    }

                case SQLCommand.TDSBooking.Update:
                    {
                        query = "UPDATE TDS_BOOKING\n" +
                                "   SET BOOKING_DATE      = ?BOOKING_DATE,\n" +
                                "       PROJECT_ID        = ?PROJECT_ID,\n" +
                                "       EXPENSE_LEDGER_ID = ?EXPENSE_LEDGER_ID,\n" +
                                "       PARTY_LEDGER_ID   = ?PARTY_LEDGER_ID,\n" +
                                "       AMOUNT            = ?AMOUNT,\n" +
                                "       VOUCHER_ID        = ?VOUCHER_ID\n" +
                                " WHERE BOOKING_ID = ?BOOKING_ID";
                        break;
                    }
                case SQLCommand.TDSBooking.Delete:
                    {
                        query = "UPDATE TDS_BOOKING SET IS_DELETED=0 WHERE BOOKING_ID=?BOOKING_ID";
                        break;
                    }
                case SQLCommand.TDSBooking.DeleteBooking:
                    {
                        query = "DELETE FROM TDS_BOOKING WHERE BOOKING_ID=?BOOKING_ID";
                        break;
                    }

                case SQLCommand.TDSBooking.FetchAll:
                    {
                        query = "SELECT BOOKING_ID,\n" +
                                "       BOOKING_DATE,\n" +
                                "       PROJECT_ID,\n" +
                                "       EXPENSE_LEDGER_ID,\n" +
                                "       PARTY_LEDGER_ID,\n" +
                                "       AMOUNT,\n" +
                                "       VOUCHER_ID\n" +
                                "  FROM TDS_BOOKING\n" +
                                " ORDER BY BOOKING_DATE ASC";
                        break;
                    }
                case SQLCommand.TDSBooking.Fetch:
                    {
                        query = "SELECT TB.BOOKING_ID,VMT.VOUCHER_ID,\n" +
                        "       TB.BOOKING_DATE,\n" +
                        "       TB.PROJECT_ID,\n" +
                        "       TB.EXPENSE_LEDGER_ID,\n" +
                        "       TB.PARTY_LEDGER_ID,\n" +
                        "       TB.AMOUNT,\n" +
                        "       VMT.VOUCHER_NO,VMT.NARRATION\n" +
                        "  FROM TDS_BOOKING AS TB\n" +
                        "  LEFT JOIN VOUCHER_MASTER_TRANS AS VMT\n" +
                        "    ON TB.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        " WHERE TB.BOOKING_ID = ?BOOKING_ID\n" +
                        "   AND TB.VOUCHER_ID = ?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.TDSBooking.FetchTDSMaster:
                    {
                        query = "SELECT T.LEDGER_ID,\n" +
                        "       T.PROJECT_ID,\n" +
                        "       T.GROUP_ID,\n" +
                        "       T.BOOKING_DATE,\n" +
                        "       T.BOOKING_ID,\n" +
                        "       T.DEDUCTEE_TYPE_ID,\n" +
                        "       T.VOUCHER_ID,\n" +
                        "       T.VOUCHER_NO,\n" +
                        "       T.LEDGER_NAME      AS 'EXPENSE_LEDGER',\n" +
                        "       TPD.LEDGER_NAME    AS 'PARTY_LEDGER',\n" +
                        "       T.PROJECT,\n" +
                        "       T.AMOUNT\n" +
                        "  FROM (SELECT ML.LEDGER_ID,\n" +
                        "               MLG.GROUP_ID,\n" +
                        "               B.PROJECT_ID,\n" +
                        "               BOOKING_DATE,\n" +
                        "               B.BOOKING_ID,\n" +
                        "               VMT.VOUCHER_ID,\n" +
                        "               VOUCHER_NO,\n" +
                        "               ML.LEDGER_NAME,\n" +
                        "               B.DEDUCTEE_TYPE_ID,\n" +
                        "               PROJECT,\n" +
                        "               B.AMOUNT\n" +
                        "          FROM TDS_BOOKING B\n" +
                        "          LEFT JOIN VOUCHER_MASTER_TRANS VMT\n" +
                        "            ON B.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        "           AND VOUCHER_SUB_TYPE = 'TDS'\n" +
                        "          LEFT JOIN VOUCHER_TRANS VT\n" +
                        "            ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        "          LEFT JOIN MASTER_LEDGER ML\n" +
                        "            ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                        "           AND IS_TDS_LEDGER = 1\n" +
                        "          LEFT JOIN MASTER_LEDGER_GROUP MLG\n" +
                        "            ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                        "          LEFT JOIN MASTER_PROJECT MP\n" +
                        "            ON MP.PROJECT_ID = B.PROJECT_ID\n" +
                        "         WHERE B.IS_DELETED = 1\n" +
                        "           AND MLG.NATURE_ID = 2\n" +
                        "         GROUP BY B.BOOKING_ID ORDER BY B.BOOKING_DATE, VMT.VOUCHER_NO ASC) AS T\n" +
                        "  JOIN (SELECT ML.LEDGER_ID,\n" +
                        "               MLG.GROUP_ID,\n" +
                        "               B.PROJECT_ID,\n" +
                        "               BOOKING_DATE,\n" +
                        "               B.BOOKING_ID,\n" +
                        "               VMT.VOUCHER_ID,\n" +
                        "               VOUCHER_NO,\n" +
                        "               ML.LEDGER_NAME,\n" +
                        "               PROJECT,\n" +
                        "               B.AMOUNT\n" +
                        "          FROM TDS_BOOKING B\n" +
                        "          LEFT JOIN VOUCHER_MASTER_TRANS VMT\n" +
                        "            ON B.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        "           AND VOUCHER_SUB_TYPE = 'TDS'\n" +
                        "          LEFT JOIN VOUCHER_TRANS VT\n" +
                        "            ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        "          LEFT JOIN MASTER_LEDGER ML\n" +
                        "            ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                        "           AND IS_TDS_LEDGER = 1\n" +
                        "          LEFT JOIN MASTER_LEDGER_GROUP MLG\n" +
                        "            ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                        "          LEFT JOIN MASTER_PROJECT MP\n" +
                        "            ON MP.PROJECT_ID = B.PROJECT_ID\n" +
                        "         WHERE B.IS_DELETED = 1\n" +
                        "           AND ML.GROUP_ID = 26\n" +
                        "         GROUP BY B.BOOKING_ID ORDER BY B.BOOKING_DATE,  VMT.VOUCHER_NO ASC) AS TPD\n" +
                        "    ON T.BOOKING_ID = TPD.BOOKING_ID  \n" +
                        "    WHERE T.PROJECT_ID=?PROJECT_ID AND T.BOOKING_DATE BETWEEN ?BOOKING_DATE AND ?DATE_CLOSED";

                        break;
                    }
                case SQLCommand.TDSBooking.FetchTDSVoucher:
                    {
                        query = "SELECT TBD.BOOKING_ID,\n" +
                        "       NAME,\n" +
                        "       TBD.BOOKING_ID,\n" +
                        "       TB.VOUCHER_ID,\n" +
                        "       TB.DEDUCTEE_TYPE_ID,\n" +
                        "       ASSESS_AMOUNT,\n" +
                        "       CASE\n" +
                        "         WHEN IS_TDS_DEDUCTED = 1 THEN\n" +
                        "          'Yes'\n" +
                        "         ELSE\n" +
                        "          'No'\n" +
                        "       END AS 'DEDUCTEE_TYPE',\n" +
                        "       ML.LEDGER_NAME,\n" +
                        "       TDD.TAX_AMOUNT\n" +
                        "  FROM TDS_BOOKING AS TB\n" +
                        "  LEFT JOIN TDS_BOOKING_DETAIL AS TBD\n" +
                        "    ON TB.BOOKING_ID = TBD.BOOKING_ID\n" +
                        "  LEFT JOIN TDS_NATURE_PAYMENT AS TNP\n" +
                        "    ON TNP.NATURE_PAY_ID = TBD.NATURE_OF_PAYMENT_ID\n" +
                        "  LEFT JOIN TDS_DEDUCTION_DETAIL AS TDD\n" +
                        "    ON TBD.BOOKING_DETAIL_ID = TDD.BOOKING_DETAIL_ID\n" +
                        "  LEFT JOIN TDS_DEDUCTION AS TD\n" +
                        "    ON TDD.DEDUCTION_ID = TD.DEDUCTION_ID\n" +
                        "  LEFT JOIN MASTER_LEDGER AS ML\n" +
                        "    ON TDD.TAX_LEDGER_ID = ML.LEDGER_ID\n" +
                        " WHERE TBD.BOOKING_ID IN (?BOOKING_ID)";
                        break;
                    }
                case SQLCommand.TDSBooking.FetchLedgerName:
                    {
                        query = "SELECT LEDGER_NAME FROM MASTER_LEDGER WHERE LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.TDSBooking.FetchTDSBooking:
                    {
                        //query = "SELECT T.BOOKING_ID,\n" +
                        //"       T.LEDGER_ID,\n" +
                        //"       T.NATURE_PAY_ID,\n" +
                        //"       T.BOOKING_DETAIL_ID,\n" +
                        //"       T.BOOKING_DATE,\n" +
                        //"       T.PROJECT_ID,\n" +
                        //"       T.EXPENSE_LEDGER_ID,\n" +
                        //"       T.PARTY_LEDGER_ID,\n" +
                        //"       T.VOUCHER_ID,\n" +
                        //"       T.DEDUCTEE_TYPE_ID,\n" +
                        //"       T.AMOUNT,\n" +
                        //"       T.PARTY_AMOUNT,\n" +
                        //"       T.VOUCHER_NO,\n" +
                        //"       T.NARRATION,\n" +
                        //"       T.NAME,\n" +
                        //"       T.PAN_NUMBER,\n" +
                        //"       T.ASSESS_AMOUNT,\n" +
                        //"       T.RATE,\n" +
                        //"       T.TDS_TAX_TYPE,\n" +
                        //"       T.TAX,\n" +
                        //"       T.IS_TDS_DEDUCTED,\n" +
                        //"       CASE\n" +
                        //"         WHEN T.IS_TDS_DEDUCTED = 1 THEN\n" +
                        //"\n" +
                        //"          CASE\n" +
                        //"            WHEN T.PAN_NUMBER <> '' THEN\n" +
                        //"            /* IF(T.ASSESS_AMOUNT >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        //"                            '@',\n" +
                        //"                            1),\n" +
                        //"            '@',\n" +
                        //"            -1)),*/\n" +
                        //"             CONCAT(CONCAT('1.TDS Tax - ',\n" +
                        //"                           CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.ASSESS_AMOUNT, 2),\n" +
                        //"                                                       'x '),\n" +
                        //"                                                CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(1 = 1,\n" +
                        //"                                                                                           T.RATE,\n" +
                        //"                                                                                           0.00),\n" +
                        //"                                                                                        '@',\n" +
                        //"                                                                                        1),\n" +
                        //"                                                                        '@',\n" +
                        //"                                                                        -1)),\n" +
                        //"                                                       ' % ')),\n" +
                        //"                                         ' = '),\n" +
                        //"                                  ROUND(IF(1 = 1,\n" +
                        //"                                           T.ASSESS_AMOUNT *\n" +
                        //"                                           (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                            '@',\n" +
                        //"                                                                            1),\n" +
                        //"                                                            '@',\n" +
                        //"                                                            -1)) / 100,\n" +
                        //"                                           0),\n" +
                        //"                                        2))),\n" +
                        //"                    IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                 '@',\n" +
                        //"                                                                 3),\n" +
                        //"                                                 '@',\n" +
                        //"                                                 -1)),\n" +
                        //"                       '\\n',\n" +
                        //"                       ''),\n" +
                        //"                    IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                 '@',\n" +
                        //"                                                                 3),\n" +
                        //"                                                 '@',\n" +
                        //"                                                 -1)),\n" +
                        //"                       CONCAT('2.Surcharge - ',\n" +
                        //"                              CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.TAX, 2), 'x '),\n" +
                        //"                                                   CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(3 = 3,\n" +
                        //"                                                                                              T.RATE,\n" +
                        //"                                                                                              0.00),\n" +
                        //"                                                                                           '@',\n" +
                        //"                                                                                           3),\n" +
                        //"                                                                           '@',\n" +
                        //"                                                                           -1)),\n" +
                        //"                                                          ' % ')),\n" +
                        //"                                            ' = '),\n" +
                        //"                                     ROUND(IF(3 = 3,\n" +
                        //"                                              T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                                       '@',\n" +
                        //"                                                                                       3),\n" +
                        //"                                                                       '@',\n" +
                        //"                                                                       -1)) / 100,\n" +
                        //"                                              0),\n" +
                        //"                                           2))),\n" +
                        //"                       ''),\n" +
                        //"                    IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                 '@',\n" +
                        //"                                                                 4),\n" +
                        //"                                                 '@',\n" +
                        //"                                                 -1)),\n" +
                        //"                       '\\n',\n" +
                        //"                       ''),\n" +
                        //"                    IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                 '@',\n" +
                        //"                                                                 4),\n" +
                        //"                                                 '@',\n" +
                        //"                                                 -1)),\n" +
                        //"                       CONCAT('3.Ed Cess - ',\n" +
                        //"                              CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.TAX, 2), 'x '),\n" +
                        //"                                                   CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(4 = 4,\n" +
                        //"                                                                                              T.RATE,\n" +
                        //"                                                                                              0.00),\n" +
                        //"                                                                                           '@',\n" +
                        //"                                                                                           4),\n" +
                        //"                                                                           '@',\n" +
                        //"                                                                           -1)),\n" +
                        //"                                                          ' % ')),\n" +
                        //"                                            ' = '),\n" +
                        //"                                     ROUND(IF(3 = 3,\n" +
                        //"                                              T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                                       '@',\n" +
                        //"                                                                                       4),\n" +
                        //"                                                                       '@',\n" +
                        //"                                                                       -1)) / 100,\n" +
                        //"                                              0),\n" +
                        //"                                           2))),\n" +
                        //"                       ''),\n" +
                        //"                    IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                 '@',\n" +
                        //"                                                                 5),\n" +
                        //"                                                 '@',\n" +
                        //"                                                 -1)),\n" +
                        //"                       '\\n',\n" +
                        //"                       ''),\n" +
                        //"                    IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                 '@',\n" +
                        //"                                                                 5),\n" +
                        //"                                                 '@',\n" +
                        //"                                                 -1)),\n" +
                        //"                       CONCAT('4.Sec Ed Cess - ',\n" +
                        //"                              CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.TAX, 2), 'x '),\n" +
                        //"                                                   CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(5 = 5,\n" +
                        //"                                                                                              T.RATE,\n" +
                        //"                                                                                              0.00),\n" +
                        //"                                                                                           '@',\n" +
                        //"                                                                                           5),\n" +
                        //"                                                                           '@',\n" +
                        //"                                                                           -1)),\n" +
                        //"                                                          ' % ')),\n" +
                        //"                                            ' = '),\n" +
                        //"                                     ROUND(IF(5 = 5,\n" +
                        //"                                              T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                                       '@',\n" +
                        //"                                                                                       5),\n" +
                        //"                                                                       '@',\n" +
                        //"                                                                       -1)) / 100,\n" +
                        //"                                              0),\n" +
                        //"                                           2))),\n" +
                        //"                       ''),\n" +
                        //"                    '\\n',\n" +
                        //"                    CONCAT(' '),\n" +
                        //"                    '\\n',\n" +
                        //"                    CONCAT('Payable to TDS =',\n" +
                        //"                           ROUND(IF(1 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                              '@',\n" +
                        //"                                                                              1),\n" +
                        //"                                                              '@',\n" +
                        //"                                                              -1)),\n" +
                        //"                                    T.ASSESS_AMOUNT *\n" +
                        //"                                    (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                     '@',\n" +
                        //"                                                                     1),\n" +
                        //"                                                     '@',\n" +
                        //"                                                     -1)) / 100,\n" +
                        //"                                    0),\n" +
                        //"                                 2) +\n" +
                        //"                           ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                              '@',\n" +
                        //"                                                                              3),\n" +
                        //"                                                              '@',\n" +
                        //"                                                              -1)),\n" +
                        //"                                    T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                             '@',\n" +
                        //"                                                                             3),\n" +
                        //"                                                             '@',\n" +
                        //"                                                             -1)) / 100,\n" +
                        //"                                    0),\n" +
                        //"                                 2) +\n" +
                        //"                           ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                              '@',\n" +
                        //"                                                                              4),\n" +
                        //"                                                              '@',\n" +
                        //"                                                              -1)),\n" +
                        //"                                    T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                             '@',\n" +
                        //"                                                                             4),\n" +
                        //"                                                             '@',\n" +
                        //"                                                             -1)) / 100,\n" +
                        //"                                    0),\n" +
                        //"                                 2) +\n" +
                        //"                           ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                              '@',\n" +
                        //"                                                                              5),\n" +
                        //"                                                              '@',\n" +
                        //"                                                              -1)),\n" +
                        //"                                    T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                             '@',\n" +
                        //"                                                                             5),\n" +
                        //"                                                             '@',\n" +
                        //"                                                             -1)) / 100,\n" +
                        //"                                    0),\n" +
                        //"                                 2)),\n" +
                        //"                    '\\n',\n" +
                        //"                    CONCAT('Payable to Party = ',\n" +
                        //"                           ROUND(T.ASSESS_AMOUNT -\n" +
                        //"                                 (ROUND(IF(1 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                     '@',\n" +
                        //"                                                                                     1),\n" +
                        //"                                                                     '@',\n" +
                        //"                                                                     -1)),\n" +
                        //"                                           T.ASSESS_AMOUNT *\n" +
                        //"                                           (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                            '@',\n" +
                        //"                                                                            1),\n" +
                        //"                                                            '@',\n" +
                        //"                                                            -1)) / 100,\n" +
                        //"                                           0),\n" +
                        //"                                        2) +\n" +
                        //"                                 ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                     '@',\n" +
                        //"                                                                                     3),\n" +
                        //"                                                                     '@',\n" +
                        //"                                                                     -1)),\n" +
                        //"                                           T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                                    '@',\n" +
                        //"                                                                                    3),\n" +
                        //"                                                                    '@',\n" +
                        //"                                                                    -1)) / 100,\n" +
                        //"                                           0),\n" +
                        //"                                        2) +\n" +
                        //"                                 ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                     '@',\n" +
                        //"                                                                                     4),\n" +
                        //"                                                                     '@',\n" +
                        //"                                                                     -1)),\n" +
                        //"                                           T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                                    '@',\n" +
                        //"                                                                                    4),\n" +
                        //"                                                                    '@',\n" +
                        //"                                                                    -1)) / 100,\n" +
                        //"                                           0),\n" +
                        //"                                        2) +\n" +
                        //"                                 ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                     '@',\n" +
                        //"                                                                                     5),\n" +
                        //"                                                                     '@',\n" +
                        //"                                                                     -1)),\n" +
                        //"                                           T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                                    '@',\n" +
                        //"                                                                                    5),\n" +
                        //"                                                                    '@',\n" +
                        //"                                                                    -1)) / 100,\n" +
                        //"                                           0),\n" +
                        //"                                        2)),\n" +
                        //"                                 2)))\n" +
                        //"          --                '')\n" +
                        //"            ELSE\n" +
                        //"             IF(T.ASSESS_AMOUNT >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        //"                                                                    '@',\n" +
                        //"                                                                    2),\n" +
                        //"                                                    '@',\n" +
                        //"                                                    -1)),\n" +
                        //"                CONCAT(CONCAT('1.TDS Tax - ',\n" +
                        //"                              CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.ASSESS_AMOUNT,\n" +
                        //"                                                                 2),\n" +
                        //"                                                          ' x '),\n" +
                        //"                                                   CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(2 =\n" +
                        //"                                                                                              ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                                                                    '@',\n" +
                        //"                                                                                                                                    2),\n" +
                        //"                                                                                                                    '@',\n" +
                        //"                                                                                                                    -1)),\n" +
                        //"                                                                                              T.RATE,\n" +
                        //"                                                                                              0.00),\n" +
                        //"                                                                                           '@',\n" +
                        //"                                                                                           2),\n" +
                        //"                                                                           '@',\n" +
                        //"                                                                           -1)),\n" +
                        //"                                                          '% ')),\n" +
                        //"                                            ' = '),\n" +
                        //"                                     ROUND(IF(2 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                        '@',\n" +
                        //"                                                                                        2),\n" +
                        //"                                                                        '@',\n" +
                        //"                                                                        -1)),\n" +
                        //"                                              T.ASSESS_AMOUNT *\n" +
                        //"                                              (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                               '@',\n" +
                        //"                                                                               2),\n" +
                        //"                                                               '@',\n" +
                        //"                                                               -1)) / 100,\n" +
                        //"                                              2)))),\n" +
                        //"                       IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                    '@',\n" +
                        //"                                                                    3),\n" +
                        //"                                                    '@',\n" +
                        //"                                                    -1)),\n" +
                        //"                          '\\n',\n" +
                        //"                          ''),\n" +
                        //"                       IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                    '@',\n" +
                        //"                                                                    3),\n" +
                        //"                                                    '@',\n" +
                        //"                                                    -1)),\n" +
                        //"                          CONCAT('2.Surcharge - ',\n" +
                        //"                                 CONCAT(CONCAT(CONCAT(CONCAT(0.00, ' x '),\n" +
                        //"                                                      CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(3 =\n" +
                        //"                                                                                                 ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                                                                       '@',\n" +
                        //"                                                                                                                                       3),\n" +
                        //"                                                                                                                       '@',\n" +
                        //"                                                                                                                       -1)),\n" +
                        //"                                                                                                 T.RATE,\n" +
                        //"                                                                                                 0.00),\n" +
                        //"                                                                                              '@',\n" +
                        //"                                                                                              3),\n" +
                        //"                                                                              '@',\n" +
                        //"                                                                              -1)),\n" +
                        //"                                                             '% ')),\n" +
                        //"                                               ' = '),\n" +
                        //"                                        ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                           '@',\n" +
                        //"                                                                                           3),\n" +
                        //"                                                                           '@',\n" +
                        //"                                                                           -1)),\n" +
                        //"                                                 T.TAX *\n" +
                        //"                                                 (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                                  '@',\n" +
                        //"                                                                                  3),\n" +
                        //"                                                                  '@',\n" +
                        //"                                                                  -1)) / 100,\n" +
                        //"                                                 2)))),\n" +
                        //"                          ''),\n" +
                        //"                       IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                    '@',\n" +
                        //"                                                                    4),\n" +
                        //"                                                    '@',\n" +
                        //"                                                    -1)),\n" +
                        //"                          '\\n',\n" +
                        //"                          ''),\n" +
                        //"                       IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                    '@',\n" +
                        //"                                                                    4),\n" +
                        //"                                                    '@',\n" +
                        //"                                                    -1)),\n" +
                        //"                          CONCAT('3.Ed Cess - ',\n" +
                        //"                                 CONCAT(CONCAT(CONCAT(CONCAT(0.00, ' x '),\n" +
                        //"                                                      CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(4 =\n" +
                        //"                                                                                                 ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                                                                       '@',\n" +
                        //"                                                                                                                                       4),\n" +
                        //"                                                                                                                       '@',\n" +
                        //"                                                                                                                       -1)),\n" +
                        //"                                                                                                 T.RATE,\n" +
                        //"                                                                                                 0.00),\n" +
                        //"                                                                                              '@',\n" +
                        //"                                                                                              4),\n" +
                        //"                                                                              '@',\n" +
                        //"                                                                              -1)),\n" +
                        //"                                                             '% ')),\n" +
                        //"                                               ' = '),\n" +
                        //"                                        ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                           '@',\n" +
                        //"                                                                                           4),\n" +
                        //"                                                                           '@',\n" +
                        //"                                                                           -1)),\n" +
                        //"                                                 T.TAX *\n" +
                        //"                                                 (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                                  '@',\n" +
                        //"                                                                                  4),\n" +
                        //"                                                                  '@',\n" +
                        //"                                                                  -1)) / 100,\n" +
                        //"                                                 2)))),\n" +
                        //"                          ''),\n" +
                        //"                       IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                    '@',\n" +
                        //"                                                                    5),\n" +
                        //"                                                    '@',\n" +
                        //"                                                    -1)),\n" +
                        //"                          '\\n',\n" +
                        //"                          ''),\n" +
                        //"                       IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                    '@',\n" +
                        //"                                                                    5),\n" +
                        //"                                                    '@',\n" +
                        //"                                                    -1)),\n" +
                        //"                          CONCAT('4.Sec Ed Cess - ',\n" +
                        //"                                 CONCAT(CONCAT(CONCAT(CONCAT(0.00, ' x '),\n" +
                        //"                                                      CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(5 =\n" +
                        //"                                                                                                 ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                                                                       '@',\n" +
                        //"                                                                                                                                       5),\n" +
                        //"                                                                                                                       '@',\n" +
                        //"                                                                                                                       -1)),\n" +
                        //"                                                                                                 T.RATE,\n" +
                        //"                                                                                                 0.00),\n" +
                        //"                                                                                              '@',\n" +
                        //"                                                                                              5),\n" +
                        //"                                                                              '@',\n" +
                        //"                                                                              -1)),\n" +
                        //"                                                             '% ')),\n" +
                        //"                                               ' = '),\n" +
                        //"                                        ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                           '@',\n" +
                        //"                                                                                           5),\n" +
                        //"                                                                           '@',\n" +
                        //"                                                                           -1)),\n" +
                        //"                                                 T.TAX *\n" +
                        //"                                                 (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                                  '@',\n" +
                        //"                                                                                  5),\n" +
                        //"                                                                  '@',\n" +
                        //"                                                                  -1)) / 100,\n" +
                        //"                                                 2)))),\n" +
                        //"                          ''),\n" +
                        //"                       '\\n',\n" +
                        //"                       CONCAT(''),\n" +
                        //"                       '\\n',\n" +
                        //"\n" +
                        //"                       CONCAT('Payable to TDS =',\n" +
                        //"                              ROUND(IF(2 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                 '@',\n" +
                        //"                                                                                 2),\n" +
                        //"                                                                 '@',\n" +
                        //"                                                                 -1)),\n" +
                        //"                                       T.ASSESS_AMOUNT *\n" +
                        //"                                       (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                        '@',\n" +
                        //"                                                                        2),\n" +
                        //"                                                        '@',\n" +
                        //"                                                        -1)) / 100,\n" +
                        //"                                       0),\n" +
                        //"                                    2) +\n" +
                        //"                              ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                 '@',\n" +
                        //"                                                                                 3),\n" +
                        //"                                                                 '@',\n" +
                        //"                                                                 -1)),\n" +
                        //"                                       T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                                '@',\n" +
                        //"                                                                                3),\n" +
                        //"                                                                '@',\n" +
                        //"                                                                -1)) / 100,\n" +
                        //"                                       0),\n" +
                        //"                                    2) +\n" +
                        //"                              ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                 '@',\n" +
                        //"                                                                                 4),\n" +
                        //"                                                                 '@',\n" +
                        //"                                                                 -1)),\n" +
                        //"                                       T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                                '@',\n" +
                        //"                                                                                4),\n" +
                        //"                                                                '@',\n" +
                        //"                                                                -1)) / 100,\n" +
                        //"                                       0),\n" +
                        //"                                    2) +\n" +
                        //"                              ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                 '@',\n" +
                        //"                                                                                 5),\n" +
                        //"                                                                 '@',\n" +
                        //"                                                                 -1)),\n" +
                        //"                                       T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                                '@',\n" +
                        //"                                                                                5),\n" +
                        //"                                                                '@',\n" +
                        //"                                                                -1)) / 100,\n" +
                        //"                                       0),\n" +
                        //"                                    2)),\n" +
                        //"                       '\\n',\n" +
                        //"                       CONCAT('Payable to Party = ',\n" +
                        //"                              ROUND(T.ASSESS_AMOUNT -\n" +
                        //"                                    (ROUND(IF(2 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                        '@',\n" +
                        //"                                                                                        2),\n" +
                        //"                                                                        '@',\n" +
                        //"                                                                        -1)),\n" +
                        //"                                              T.ASSESS_AMOUNT *\n" +
                        //"                                              (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                               '@',\n" +
                        //"                                                                               2),\n" +
                        //"                                                               '@',\n" +
                        //"                                                               -1)) / 100,\n" +
                        //"                                              0),\n" +
                        //"                                           2) +\n" +
                        //"                                    ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                        '@',\n" +
                        //"                                                                                        3),\n" +
                        //"                                                                        '@',\n" +
                        //"                                                                        -1)),\n" +
                        //"                                              T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                                       '@',\n" +
                        //"                                                                                       3),\n" +
                        //"                                                                       '@',\n" +
                        //"                                                                       -1)) / 100,\n" +
                        //"                                              0),\n" +
                        //"                                           2) +\n" +
                        //"                                    ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                        '@',\n" +
                        //"                                                                                        4),\n" +
                        //"                                                                        '@',\n" +
                        //"                                                                        -1)),\n" +
                        //"                                              T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                                       '@',\n" +
                        //"                                                                                       4),\n" +
                        //"                                                                       '@',\n" +
                        //"                                                                       -1)) / 100,\n" +
                        //"                                              0),\n" +
                        //"                                           2) +\n" +
                        //"                                    ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                        '@',\n" +
                        //"                                                                                        5),\n" +
                        //"                                                                        '@',\n" +
                        //"                                                                        -1)),\n" +
                        //"                                              T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                                       '@',\n" +
                        //"                                                                                       5),\n" +
                        //"                                                                       '@',\n" +
                        //"                                                                       -1)) / 100,\n" +
                        //"                                              0),\n" +
                        //"                                           2)),\n" +
                        //"                                    2))),\n" +
                        //"                '')\n" +
                        //"          END\n" +
                        //"         ELSE\n" +
                        //"          CASE\n" +
                        //"            WHEN T.IS_TDS_DEDUCTED = 0 OR T.PAN_NUMBER <> '' THEN\n" +
                        //"             CONCAT(CONCAT('Payable to TDS = ', 0.00),\n" +
                        //"                    '\\n',\n" +
                        //"                    CONCAT('Payable to Party = ', ROUND(T.ASSESS_AMOUNT, 2)))\n" +
                        //"            ELSE\n" +
                        //"            /* IF(T.ASSESS_AMOUNT >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        //"                            '@',\n" +
                        //"                            2),\n" +
                        //"            '@',\n" +
                        //"            -1)),*/\n" +
                        //"             CONCAT(CONCAT('1.TDS Tax - ',\n" +
                        //"                           CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.ASSESS_AMOUNT, 2),\n" +
                        //"                                                       ' x '),\n" +
                        //"                                                CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(2 =\n" +
                        //"                                                                                           ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                                                                 '@',\n" +
                        //"                                                                                                                                 2),\n" +
                        //"                                                                                                                 '@',\n" +
                        //"                                                                                                                 -1)),\n" +
                        //"                                                                                           T.RATE,\n" +
                        //"                                                                                           0.00),\n" +
                        //"                                                                                        '@',\n" +
                        //"                                                                                        2),\n" +
                        //"                                                                        '@',\n" +
                        //"                                                                        -1)),\n" +
                        //"                                                       '% ')),\n" +
                        //"                                         ' = '),\n" +
                        //"                                  ROUND(IF(2 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                     '@',\n" +
                        //"                                                                                     2),\n" +
                        //"                                                                     '@',\n" +
                        //"                                                                     -1)),\n" +
                        //"                                           T.ASSESS_AMOUNT *\n" +
                        //"                                           (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                            '@',\n" +
                        //"                                                                            2),\n" +
                        //"                                                            '@',\n" +
                        //"                                                            -1)) / 100,\n" +
                        //"                                           2)))),\n" +
                        //"                    IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                 '@',\n" +
                        //"                                                                 3),\n" +
                        //"                                                 '@',\n" +
                        //"                                                 -1)),\n" +
                        //"                       '\\n',\n" +
                        //"                       ''),\n" +
                        //"                    IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                 '@',\n" +
                        //"                                                                 3),\n" +
                        //"                                                 '@',\n" +
                        //"                                                 -1)),\n" +
                        //"                       CONCAT('2.Surcharge - ',\n" +
                        //"                              CONCAT(CONCAT(CONCAT(CONCAT(0.00, ' x '),\n" +
                        //"                                                   CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(3 =\n" +
                        //"                                                                                              ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                                                                    '@',\n" +
                        //"                                                                                                                                    3),\n" +
                        //"                                                                                                                    '@',\n" +
                        //"                                                                                                                    -1)),\n" +
                        //"                                                                                              T.RATE,\n" +
                        //"                                                                                              0.00),\n" +
                        //"                                                                                           '@',\n" +
                        //"                                                                                           3),\n" +
                        //"                                                                           '@',\n" +
                        //"                                                                           -1)),\n" +
                        //"                                                          '% ')),\n" +
                        //"                                            ' = '),\n" +
                        //"                                     ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                        '@',\n" +
                        //"                                                                                        3),\n" +
                        //"                                                                        '@',\n" +
                        //"                                                                        -1)),\n" +
                        //"                                              T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                                       '@',\n" +
                        //"                                                                                       3),\n" +
                        //"                                                                       '@',\n" +
                        //"                                                                       -1)) / 100,\n" +
                        //"                                              2)))),\n" +
                        //"                       ''),\n" +
                        //"                    IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                 '@',\n" +
                        //"                                                                 4),\n" +
                        //"                                                 '@',\n" +
                        //"                                                 -1)),\n" +
                        //"                       '\\n',\n" +
                        //"                       ''),\n" +
                        //"                    IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                 '@',\n" +
                        //"                                                                 4),\n" +
                        //"                                                 '@',\n" +
                        //"                                                 -1)),\n" +
                        //"                       CONCAT('3.Ed Cess - ',\n" +
                        //"                              CONCAT(CONCAT(CONCAT(CONCAT(0.00, ' x '),\n" +
                        //"                                                   CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(4 =\n" +
                        //"                                                                                              ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                                                                    '@',\n" +
                        //"                                                                                                                                    4),\n" +
                        //"                                                                                                                    '@',\n" +
                        //"                                                                                                                    -1)),\n" +
                        //"                                                                                              T.RATE,\n" +
                        //"                                                                                              0.00),\n" +
                        //"                                                                                           '@',\n" +
                        //"                                                                                           4),\n" +
                        //"                                                                           '@',\n" +
                        //"                                                                           -1)),\n" +
                        //"                                                          '% ')),\n" +
                        //"                                            ' = '),\n" +
                        //"                                     ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                        '@',\n" +
                        //"                                                                                        4),\n" +
                        //"                                                                        '@',\n" +
                        //"                                                                        -1)),\n" +
                        //"                                              T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                                       '@',\n" +
                        //"                                                                                       4),\n" +
                        //"                                                                       '@',\n" +
                        //"                                                                       -1)) / 100,\n" +
                        //"                                              2)))),\n" +
                        //"                       ''),\n" +
                        //"                    IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                 '@',\n" +
                        //"                                                                 5),\n" +
                        //"                                                 '@',\n" +
                        //"                                                 -1)),\n" +
                        //"                       '\\n',\n" +
                        //"                       ''),\n" +
                        //"                    IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                 '@',\n" +
                        //"                                                                 5),\n" +
                        //"                                                 '@',\n" +
                        //"                                                 -1)),\n" +
                        //"                       CONCAT('4.Sec Ed Cess - ',\n" +
                        //"                              CONCAT(CONCAT(CONCAT(CONCAT(0.00, ' x '),\n" +
                        //"                                                   CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(5 =\n" +
                        //"                                                                                              ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                                                                    '@',\n" +
                        //"                                                                                                                                    5),\n" +
                        //"                                                                                                                    '@',\n" +
                        //"                                                                                                                    -1)),\n" +
                        //"                                                                                              T.RATE,\n" +
                        //"                                                                                              0.00),\n" +
                        //"                                                                                           '@',\n" +
                        //"                                                                                           5),\n" +
                        //"                                                                           '@',\n" +
                        //"                                                                           -1)),\n" +
                        //"                                                          '% ')),\n" +
                        //"                                            ' = '),\n" +
                        //"                                     ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                        '@',\n" +
                        //"                                                                                        5),\n" +
                        //"                                                                        '@',\n" +
                        //"                                                                        -1)),\n" +
                        //"                                              T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                                       '@',\n" +
                        //"                                                                                       5),\n" +
                        //"                                                                       '@',\n" +
                        //"                                                                       -1)) / 100,\n" +
                        //"                                              2)))),\n" +
                        //"                       ''),\n" +
                        //"                    '\\n',\n" +
                        //"                    CONCAT(''),\n" +
                        //"                    '\\n',\n" +
                        //"                    CONCAT('Payable to TDS =',\n" +
                        //"                           ROUND(IF(2 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                              '@',\n" +
                        //"                                                                              2),\n" +
                        //"                                                              '@',\n" +
                        //"                                                              -1)),\n" +
                        //"                                    T.ASSESS_AMOUNT *\n" +
                        //"                                    (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                     '@',\n" +
                        //"                                                                     2),\n" +
                        //"                                                     '@',\n" +
                        //"                                                     -1)) / 100,\n" +
                        //"                                    0),\n" +
                        //"                                 2) +\n" +
                        //"                           ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                              '@',\n" +
                        //"                                                                              3),\n" +
                        //"                                                              '@',\n" +
                        //"                                                              -1)),\n" +
                        //"                                    T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                             '@',\n" +
                        //"                                                                             3),\n" +
                        //"                                                             '@',\n" +
                        //"                                                             -1)) / 100,\n" +
                        //"                                    0),\n" +
                        //"                                 2) +\n" +
                        //"                           ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                              '@',\n" +
                        //"                                                                              4),\n" +
                        //"                                                              '@',\n" +
                        //"                                                              -1)),\n" +
                        //"                                    T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                             '@',\n" +
                        //"                                                                             4),\n" +
                        //"                                                             '@',\n" +
                        //"                                                             -1)) / 100,\n" +
                        //"                                    0),\n" +
                        //"                                 2) +\n" +
                        //"                           ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                              '@',\n" +
                        //"                                                                              5),\n" +
                        //"                                                              '@',\n" +
                        //"                                                              -1)),\n" +
                        //"                                    T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                             '@',\n" +
                        //"                                                                             5),\n" +
                        //"                                                             '@',\n" +
                        //"                                                             -1)) / 100,\n" +
                        //"                                    0),\n" +
                        //"                                 2)),\n" +
                        //"                    '\\n',\n" +
                        //"                    CONCAT('Payable to Party = ',\n" +
                        //"                           ROUND(T.ASSESS_AMOUNT -\n" +
                        //"                                 (ROUND(IF(2 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                     '@',\n" +
                        //"                                                                                     2),\n" +
                        //"                                                                     '@',\n" +
                        //"                                                                     -1)),\n" +
                        //"                                           T.ASSESS_AMOUNT *\n" +
                        //"                                           (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                            '@',\n" +
                        //"                                                                            2),\n" +
                        //"                                                            '@',\n" +
                        //"                                                            -1)) / 100,\n" +
                        //"                                           0),\n" +
                        //"                                        2) +\n" +
                        //"                                 ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                     '@',\n" +
                        //"                                                                                     3),\n" +
                        //"                                                                     '@',\n" +
                        //"                                                                     -1)),\n" +
                        //"                                           T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                                    '@',\n" +
                        //"                                                                                    3),\n" +
                        //"                                                                    '@',\n" +
                        //"                                                                    -1)) / 100,\n" +
                        //"                                           0),\n" +
                        //"                                        2) +\n" +
                        //"                                 ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                     '@',\n" +
                        //"                                                                                     4),\n" +
                        //"                                                                     '@',\n" +
                        //"                                                                     -1)),\n" +
                        //"                                           T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                                    '@',\n" +
                        //"                                                                                    4),\n" +
                        //"                                                                    '@',\n" +
                        //"                                                                    -1)) / 100,\n" +
                        //"                                           0),\n" +
                        //"                                        2) +\n" +
                        //"                                 ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                                     '@',\n" +
                        //"                                                                                     5),\n" +
                        //"                                                                     '@',\n" +
                        //"                                                                     -1)),\n" +
                        //"                                           T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                                    '@',\n" +
                        //"                                                                                    5),\n" +
                        //"                                                                    '@',\n" +
                        //"                                                                    -1)) / 100,\n" +
                        //"                                           0),\n" +
                        //"                                        2)),\n" +
                        //"                                 2)))\n" +
                        //"          --                '')\n" +
                        //"          END\n" +
                        //"       END AS 'TAX_AMOUNT',\n" +
                        //"       T.DEDUCT_NOW AS 'Id',\n" +
                        //"       T.LEDGER_NAME,\n" +
                        //"       T.TAX_AMOUNT AS 'TDS_AMOUNT',\n" +
                        //"       T.TDS_TAX_TYPE_ID,\n" +
                        //"       T.TDS_RATE,\n" +
                        //"       T.DEDUCTION_ID,\n" +
                        //"       T.TDS_LIMITATION_AMOUNT,\n" +
                        //"       T.TAX_LEDGER_ID\n" +
                        //"  FROM (SELECT TB.BOOKING_ID,\n" +
                        //"               TBD.BOOKING_DETAIL_ID,\n" +
                        //"               TB.BOOKING_DATE,\n" +
                        //"               TB.PROJECT_ID,\n" +
                        //"               TB.EXPENSE_LEDGER_ID,\n" +
                        //"               TB.PARTY_LEDGER_ID,\n" +
                        //"               TB.DEDUCTEE_TYPE_ID,\n" +
                        //"               TB.VOUCHER_ID,\n" +
                        //"               VMT.VOUCHER_NO,\n" +
                        //"               VMT.NARRATION,\n" +
                        //"               ML.LEDGER_ID,\n" +
                        //"               TNP.NATURE_PAY_ID,\n" +
                        //"               TD.AMOUNT AS PARTY_AMOUNT,\n" +
                        //"               TNP.NAME,\n" +
                        //"               TTR.TDS_TAX_TYPE_ID,\n" +
                        //"               TBD.IS_TDS_DEDUCTED,\n" +
                        //"               TCP.PAN_NUMBER,\n" +
                        //"               TTR.TDS_RATE,\n" +
                        //"               TB.AMOUNT,\n" +
                        //"               ROUND(TBD.ASSESS_AMOUNT, 2) AS ASSESS_AMOUNT,\n" +
                        //"               GROUP_CONCAT(TTR.TDS_RATE ORDER BY TTR.TDS_TAX_TYPE_ID\n" +
                        //"                            SEPARATOR '@') AS Rate,\n" +
                        //"               GROUP_CONCAT(TTR.TDS_TAX_TYPE_ID ORDER BY TTR.TDS_TAX_TYPE_ID\n" +
                        //"                            SEPARATOR '@') AS TDS_TAX_TYPE,\n" +
                        //"               GROUP_CONCAT(TTR.TDS_EXEMPTION_LIMIT ORDER BY\n" +
                        //"                            TTR.TDS_TAX_TYPE_ID SEPARATOR '@') AS TDS_LIMITATION_AMOUNT,\n" +
                        //"               ROUND((ASSESS_AMOUNT *\n" +
                        //"                     SUBSTRING_INDEX(GROUP_CONCAT(TTR.TDS_RATE ORDER BY\n" +
                        //"                                                   TTR.TDS_TAX_TYPE_ID\n" +
                        //"                                                   SEPARATOR '@'),\n" +
                        //"                                      '@',\n" +
                        //"                                      1)) / 100,\n" +
                        //"                     2) as Tax,\n" +
                        //"               SUBSTRING_INDEX(GROUP_CONCAT(TTR.TDS_RATE ORDER BY\n" +
                        //"                                            TTR.TDS_TAX_TYPE_ID SEPARATOR '@'),\n" +
                        //"                               '@',\n" +
                        //"                               1) AS T,\n" +
                        //"               CASE\n" +
                        //"                 WHEN TBD.IS_TDS_DEDUCTED = 1 THEN\n" +
                        //"                  '0'\n" +
                        //"                 ELSE\n" +
                        //"                  '1'\n" +
                        //"               END AS 'Deduct_Now',\n" +
                        //"               ML.LEDGER_NAME,\n" +
                        //"               TDD.TAX_AMOUNT,\n" +
                        //"               TDD.DEDUCTION_ID,\n" +
                        //"               TDD.TAX_LEDGER_ID\n" +
                        //"          FROM TDS_BOOKING AS TB\n" +
                        //"          LEFT JOIN TDS_BOOKING_DETAIL AS TBD\n" +
                        //"            ON TB.BOOKING_ID = TBD.BOOKING_ID\n" +
                        //"          LEFT JOIN TDS_DEDUCTION_DETAIL AS TDD\n" +
                        //"            ON TBD.BOOKING_DETAIL_ID = TDD.BOOKING_DETAIL_ID\n" +
                        //"          LEFT JOIN TDS_DEDUCTION AS TD\n" +
                        //"            ON TDD.DEDUCTION_ID = TD.DEDUCTION_ID\n" +
                        //"          LEFT JOIN VOUCHER_MASTER_TRANS AS VMT\n" +
                        //"            ON TB.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        //"          LEFT JOIN MASTER_LEDGER AS ML\n" +
                        //"            ON TDD.TAX_LEDGER_ID = ML.LEDGER_ID\n" +
                        //"          LEFT JOIN TDS_CREDTIORS_PROFILE AS TCP\n" +
                        //"            ON TB.PARTY_LEDGER_ID = TCP.LEDGER_ID\n" +
                        //"          LEFT JOIN TDS_NATURE_PAYMENT AS TNP\n" +
                        //"            ON TBD.NATURE_OF_PAYMENT_ID = TNP.NATURE_PAY_ID\n" +
                        //"          LEFT JOIN TDS_POLICY AS TP\n" +
                        //"            ON TBD.NATURE_OF_PAYMENT_ID = TP.TDS_NATURE_PAYMENT_ID\n" +
                        //"          LEFT JOIN TDS_TAX_RATE AS TTR\n" +
                        //"            ON TP.TDS_POLICY_ID = TTR.TDS_POLICY_ID\n" +
                        //"          LEFT JOIN tds_duty_taxtype AS TDT\n" +
                        //"            ON TTR.TDS_TAX_TYPE_ID = TDT.TDS_DUTY_TAXTYPE_ID\n" +
                        //"          LEFT JOIN TDS_DEDUCTEE_TYPE AS TDTT\n" +
                        //"            ON TP.TDS_DEDUCTEE_TYPE_ID = TDTT.DEDUCTEE_TYPE_ID\n" +
                        //"          LEFT JOIN (SELECT TDS_NATURE_PAYMENT_ID,\n" +
                        //"                           MAX(APPLICABLE_FROM) AS APPLICABLE_FROM\n" +
                        //"                      FROM TDS_POLICY AS TP\n" +
                        //"                      LEFT JOIN TDS_TAX_RATE AS TTR\n" +
                        //"                        ON TP.TDS_POLICY_ID = TTR.TDS_POLICY_ID\n" +
                        //"                      LEFT JOIN TDS_BOOKING_DETAIL AS TBD1\n" +
                        //"                        ON TP.TDS_NATURE_PAYMENT_ID =\n" +
                        //"                           TBD1.NATURE_OF_PAYMENT_ID\n" +
                        //"                     WHERE TP.APPLICABLE_FROM <= ?APPLICABLE_FROM\n" +
                        //"                       AND TP.TDS_DEDUCTEE_TYPE_ID = ?DEDUCTEE_TYPE_ID\n" +
                        //"                     GROUP BY TP.TDS_NATURE_PAYMENT_ID) AS TTD\n" +
                        //"            ON TBD.NATURE_OF_PAYMENT_ID = TTD.TDS_NATURE_PAYMENT_ID\n" +
                        //"         WHERE TB.BOOKING_ID = ?BOOKING_ID\n" +
                        //"           AND TP.TDS_DEDUCTEE_TYPE_ID = ?DEDUCTEE_TYPE_ID\n" +
                        //"           AND TP.APPLICABLE_FROM = TTD.APPLICABLE_FROM\n" +
                        //"           AND TB.IS_DELETED = 1\n" +
                        //"           AND TDT.STATUS = 1\n" +
                        //"         GROUP BY TBD.BOOKING_DETAIL_ID, NAME) AS T";

                        query = "SELECT T.BOOKING_ID,\n" +
                        "       T.LEDGER_ID,\n" +
                        "       T.NATURE_PAY_ID,\n" +
                        "       T.BOOKING_DETAIL_ID,\n" +
                        "       T.BOOKING_DATE,\n" +
                        "       T.PROJECT_ID,\n" +
                        "       T.EXPENSE_LEDGER_ID,\n" +
                        "       T.PARTY_LEDGER_ID,\n" +
                        "       T.VOUCHER_ID,\n" +
                        "       T.DEDUCTEE_TYPE_ID,\n" +
                        "       T.AMOUNT,\n" +
                        "       T.PARTY_AMOUNT,\n" +
                        "       T.VOUCHER_NO,\n" +
                        "       T.NARRATION,\n" +
                        "       T.NAME,\n" +
                        "       T.PAN_NUMBER,\n" +
                        "       T.ASSESS_AMOUNT,\n" +
                        "       T.RATE,\n" +
                        "       T.TDS_TAX_TYPE,\n" +
                        "       T.TAX,\n" +
                        "       T.IS_TDS_DEDUCTED,\n" +
                        "       CASE\n" +
                        "         WHEN T.IS_TDS_DEDUCTED = 1 THEN\n" +
                        "\n" +
                        "          CASE\n" +
                        "            WHEN T.PAN_NUMBER <> '' THEN\n" +
                        "\n" +
                        "             CONCAT(CONCAT('1.TDS Tax - ',\n" +
                        "                           CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.ASSESS_AMOUNT, 2),\n" +
                        "                                                       'x '),\n" +
                        "                                                CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(1 = 1,\n" +
                        "                                                                                           T.RATE,\n" +
                        "                                                                                           0.00),\n" +
                        "                                                                                        '@',\n" +
                        "                                                                                        1),\n" +
                        "                                                                        '@',\n" +
                        "                                                                        -1)),\n" +
                        "                                                       ' % ')),\n" +
                        "                                         ' = '),\n" +
                        "                                  ROUND(IF(1 = 1,\n" +
                        "                                           T.ASSESS_AMOUNT *\n" +
                        "                                           (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                            '@',\n" +
                        "                                                                            1),\n" +
                        "                                                            '@',\n" +
                        "                                                            -1)) / 100,\n" +
                        "                                           0),\n" +
                        "                                        2))),\n" +
                        "                    IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                 '@',\n" +
                        "                                                                 3),\n" +
                        "                                                 '@',\n" +
                        "                                                 -1)),\n" +
                        "                       '\\n',\n" +
                        "                       ''),\n" +
                        "                    IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                 '@',\n" +
                        "                                                                 3),\n" +
                        "                                                 '@',\n" +
                        "                                                 -1)),\n" +
                        "                       CONCAT('2.Surcharge - ',\n" +
                        "                              CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.TAX, 2), 'x '),\n" +
                        "                                                   CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(3 = 3,\n" +
                        "                                                                                              T.RATE,\n" +
                        "                                                                                              0.0),\n" +
                        "                                                                                           '@',\n" +
                        "                                                                                           3),\n" +
                        "                                                                           '@',\n" +
                        "                                                                           -1)),\n" +
                        "                                                          ' % ')),\n" +
                        "                                            ' = '),\n" +
                        "                                     ROUND(IF(3 = 3,\n" +
                        "                                              IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  4),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)) > 0 AND\n" +
                        "                                                 T.TAX >=\n" +
                        "                                                 (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  3),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)),\n" +
                        "                                                 T.TAX *\n" +
                        "                                                 (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  3),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)) / 100,\n" +
                        "                                                 0),\n" +
                        "                                              2))),\n" +
                        "                              '.00'),\n" +
                        "                       ''),\n" +
                        "                    IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                 '@',\n" +
                        "                                                                 4),\n" +
                        "                                                 '@',\n" +
                        "                                                 -1)),\n" +
                        "                       '\\n',\n" +
                        "                       ''),\n" +
                        "                    IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                 '@',\n" +
                        "                                                                 4),\n" +
                        "                                                 '@',\n" +
                        "                                                 -1)),\n" +
                        "                       CONCAT('3.Ed Cess - ',\n" +
                        "                              CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.TAX, 2), 'x '),\n" +
                        "                                                   CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(4 = 4,\n" +
                        "                                                                                              T.RATE,\n" +
                        "                                                                                              0.00),\n" +
                        "                                                                                           '@',\n" +
                        "                                                                                           4),\n" +
                        "                                                                           '@',\n" +
                        "                                                                           -1)),\n" +
                        "                                                          ' % ')),\n" +
                        "                                            ' = '),\n" +
                        "                                     ROUND(IF(4 = 4,\n" +
                        "                                              IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  4),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)) > 0 AND\n" +
                        "                                                 T.TAX >=\n" +
                        "                                                 (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  4),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)),\n" +
                        "                                                 T.TAX *\n" +
                        "                                                 (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  4),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)) / 100,\n" +
                        "                                                 0),\n" +
                        "                                              2))),\n" +
                        "                              '.00'),\n" +
                        "                       ''),\n" +
                        "                    IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                 '@',\n" +
                        "                                                                 5),\n" +
                        "                                                 '@',\n" +
                        "                                                 -1)),\n" +
                        "                       '\\n',\n" +
                        "                       ''),\n" +
                        "                    IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                 '@',\n" +
                        "                                                                 5),\n" +
                        "                                                 '@',\n" +
                        "                                                 -1)),\n" +
                        "                       CONCAT('4.Sec Ed Cess - ',\n" +
                        "                              CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.TAX, 2), 'x '),\n" +
                        "                                                   CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(5 = 5,\n" +
                        "                                                                                              T.RATE,\n" +
                        "                                                                                              0.00),\n" +
                        "                                                                                           '@',\n" +
                        "                                                                                           5),\n" +
                        "                                                                           '@',\n" +
                        "                                                                           -1)),\n" +
                        "                                                          ' % ')),\n" +
                        "                                            ' = '),\n" +
                        "                                     ROUND(IF(5 = 5,\n" +
                        "                                              IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  5),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)) > 0 AND\n" +
                        "                                                 T.TAX >=\n" +
                        "                                                 (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  5),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)),\n" +
                        "                                                 T.TAX *\n" +
                        "                                                 (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  5),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)) / 100,\n" +
                        "                                                 0),\n" +
                        "                                              2))),\n" +
                        "                              '.00'),\n" +
                        "                       ''),\n" +
                        "                    '\\n',\n" +
                        "                    CONCAT(' '),\n" +
                        "                    '\\n',\n" +
                        "                    CONCAT('Payable to TDS= ',\n" +
                        "                           IF(TAX_AMOUNT > 0, FORMAT(TAX_AMOUNT, 2), 0)),\n" +
                        "                    '\\n',\n" +
                        "                    CONCAT('Payable to Party = ',\n" +
                        "                           IF(PARTY_AMOUNT > 0,\n" +
                        "                              TRIM(LEADING '0' FROM FORMAT(PARTY_AMOUNT, 2)),\n" +
                        "                              0)))\n" +
                        "            ELSE\n" +
                        "             CONCAT(CONCAT('1.TDS Tax - ',\n" +
                        "                           CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.ASSESS_AMOUNT, 2),\n" +
                        "                                                       ' x '),\n" +
                        "                                                CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(2 =\n" +
                        "                                                                                           ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                                                                 '@',\n" +
                        "                                                                                                                                 2),\n" +
                        "                                                                                                                 '@',\n" +
                        "                                                                                                                 -1)),\n" +
                        "                                                                                           T.RATE,\n" +
                        "                                                                                           0.00),\n" +
                        "                                                                                        '@',\n" +
                        "                                                                                        2),\n" +
                        "                                                                        '@',\n" +
                        "                                                                        -1)),\n" +
                        "                                                       '% ')),\n" +
                        "                                         ' = '),\n" +
                        "                                  (IF(2 = SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                          '@',\n" +
                        "                                                                          2),\n" +
                        "                                                          '@',\n" +
                        "                                                          -1),\n" +
                        "                                      T.ASSESS_AMOUNT *\n" +
                        "                                      (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                       '@',\n" +
                        "                                                                       2),\n" +
                        "                                                       '@',\n" +
                        "                                                       -1)) / 100,\n" +
                        "                                      0.00)))),\n" +
                        "                    IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                 '@',\n" +
                        "                                                                 3),\n" +
                        "                                                 '@',\n" +
                        "                                                 -1)),\n" +
                        "                       '\\n',\n" +
                        "                       ''),\n" +
                        "                    IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                 '@',\n" +
                        "                                                                 3),\n" +
                        "                                                 '@',\n" +
                        "                                                 -1)),\n" +
                        "                       CONCAT('2.Surcharge - ',\n" +
                        "                              CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.TAX, 2), ' x '),\n" +
                        "                                                   CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(3 =\n" +
                        "                                                                                              ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                                                                    '@',\n" +
                        "                                                                                                                                    3),\n" +
                        "                                                                                                                    '@',\n" +
                        "                                                                                                                    -1)),\n" +
                        "                                                                                              T.RATE,\n" +
                        "                                                                                              0.00),\n" +
                        "                                                                                           '@',\n" +
                        "                                                                                           3),\n" +
                        "                                                                           '@',\n" +
                        "                                                                           -1)),\n" +
                        "                                                          '% ')),\n" +
                        "                                            ' = '),\n" +
                        "                                     ROUND(IF(3 = (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                   '@',\n" +
                        "                                                                                   3),\n" +
                        "                                                                   '@',\n" +
                        "                                                                   -1)),\n" +
                        "                                              IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  3),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)) > 0 AND\n" +
                        "                                                 T.TAX >=\n" +
                        "                                                 (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  3),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)),\n" +
                        "\n" +
                        "                                                 T.TAX *\n" +
                        "                                                 (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  3),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)) / 100,\n" +
                        "                                                 0.00),\n" +
                        "                                              '')))),\n" +
                        "                       ''),\n" +
                        "                    IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                 '@',\n" +
                        "                                                                 4),\n" +
                        "                                                 '@',\n" +
                        "                                                 -1)),\n" +
                        "                       '\\n',\n" +
                        "                       ''),\n" +
                        "                    IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                 '@',\n" +
                        "                                                                 4),\n" +
                        "                                                 '@',\n" +
                        "                                                 -1)),\n" +
                        "                       CONCAT('3.Ed Cess - ',\n" +
                        "                              CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.TAX, 2), ' x '),\n" +
                        "                                                   CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(4 =\n" +
                        "                                                                                              ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                                                                    '@',\n" +
                        "                                                                                                                                    4),\n" +
                        "                                                                                                                    '@',\n" +
                        "                                                                                                                    -1)),\n" +
                        "                                                                                              T.RATE,\n" +
                        "                                                                                              0.00),\n" +
                        "                                                                                           '@',\n" +
                        "                                                                                           4),\n" +
                        "                                                                           '@',\n" +
                        "                                                                           -1)),\n" +
                        "                                                          '% ')),\n" +
                        "                                            ' = '),\n" +
                        "                                     (IF(4 = SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                             '@',\n" +
                        "                                                                             4),\n" +
                        "                                                             '@',\n" +
                        "                                                             -1),\n" +
                        "                                         IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                             '@',\n" +
                        "                                                                             4),\n" +
                        "                                                             '@',\n" +
                        "                                                             -1)) > 0 AND\n" +
                        "                                            T.TAX >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                      '@',\n" +
                        "                                                                                      4),\n" +
                        "                                                                      '@',\n" +
                        "                                                                      -1)),\n" +
                        "                                            T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                     '@',\n" +
                        "                                                                                     4),\n" +
                        "                                                                     '@',\n" +
                        "                                                                     -1)) / 100,\n" +
                        "                                            0.00),\n" +
                        "                                         '')))),\n" +
                        "                       ''),\n" +
                        "                    IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                 '@',\n" +
                        "                                                                 5),\n" +
                        "                                                 '@',\n" +
                        "                                                 -1)),\n" +
                        "                       '\\n',\n" +
                        "                       ''),\n" +
                        "                    IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                 '@',\n" +
                        "                                                                 5),\n" +
                        "                                                 '@',\n" +
                        "                                                 -1)),\n" +
                        "                       CONCAT('4.Sec Ed Cess - ',\n" +
                        "                              CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.TAX, 2), ' x '),\n" +
                        "                                                   CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(5 =\n" +
                        "                                                                                              ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                                                                    '@',\n" +
                        "                                                                                                                                    5),\n" +
                        "                                                                                                                    '@',\n" +
                        "                                                                                                                    -1)),\n" +
                        "                                                                                              T.RATE,\n" +
                        "                                                                                              0.00),\n" +
                        "                                                                                           '@',\n" +
                        "                                                                                           5),\n" +
                        "                                                                           '@',\n" +
                        "                                                                           -1)),\n" +
                        "                                                          '% ')),\n" +
                        "                                            ' = '),\n" +
                        "                                     (IF(5 = SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                             '@',\n" +
                        "                                                                             5),\n" +
                        "                                                             '@',\n" +
                        "                                                             -1),\n" +
                        "                                         IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                             '@',\n" +
                        "                                                                             5),\n" +
                        "                                                             '@',\n" +
                        "                                                             -1)) > 0 AND\n" +
                        "                                            T.TAX >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                      '@',\n" +
                        "                                                                                      5),\n" +
                        "                                                                      '@',\n" +
                        "                                                                      -1)),\n" +
                        "                                            T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                     '@',\n" +
                        "                                                                                     5),\n" +
                        "                                                                     '@',\n" +
                        "                                                                     -1)) / 100,\n" +
                        "                                            0.00),\n" +
                        "                                         '')))),\n" +
                        "                       ''),\n" +
                        "                    '\\n',\n" +
                        "                    CONCAT(''),\n" +
                        "                    '\\n',\n" +
                        "                    CONCAT('Payable to TDS= ',\n" +
                        "                           IF(TAX_AMOUNT > 0, FORMAT(TAX_AMOUNT, 2), 0)),\n" +
                        "                    '\\n',\n" +
                        "                    CONCAT('Payable to Party = ',\n" +
                        "                           IF(PARTY_AMOUNT > 0,\n" +
                        "                              TRIM(LEADING '0' FROM FORMAT(PARTY_AMOUNT, 2)),\n" +
                        "                              0)))\n" +
                        "          END\n" +
                        "         ELSE\n" +
                        "          CASE\n" +
                        "            WHEN T.IS_TDS_DEDUCTED = 0 OR T.PAN_NUMBER <> '' THEN\n" +
                        "             CONCAT(CONCAT('Payable to TDS = ', 0.00),\n" +
                        "                    '\\n',\n" +
                        "                    CONCAT('Payable to Party = ', FORMAT(T.ASSESS_AMOUNT, 2)))\n" +
                        "            ELSE\n" +
                        "             CONCAT(CONCAT('1.TDS Tax - ',\n" +
                        "                           CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.ASSESS_AMOUNT, 2),\n" +
                        "                                                       ' x '),\n" +
                        "                                                CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(2 =\n" +
                        "                                                                                           ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                                                                 '@',\n" +
                        "                                                                                                                                 2),\n" +
                        "                                                                                                                 '@',\n" +
                        "                                                                                                                 -1)),\n" +
                        "                                                                                           T.RATE,\n" +
                        "                                                                                           0.00),\n" +
                        "                                                                                        '@',\n" +
                        "                                                                                        2),\n" +
                        "                                                                        '@',\n" +
                        "                                                                        -1)),\n" +
                        "                                                       '% ')),\n" +
                        "                                         ' = '),\n" +
                        "                                  ROUND(IF(2 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                     '@',\n" +
                        "                                                                                     2),\n" +
                        "                                                                     '@',\n" +
                        "                                                                     -1)),\n" +
                        "                                           T.ASSESS_AMOUNT *\n" +
                        "                                           (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                            '@',\n" +
                        "                                                                            2),\n" +
                        "                                                            '@',\n" +
                        "                                                            -1)) / 100,\n" +
                        "                                           0.00)))),\n" +
                        "                    IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                 '@',\n" +
                        "                                                                 3),\n" +
                        "                                                 '@',\n" +
                        "                                                 -1)),\n" +
                        "                       '\\n',\n" +
                        "                       ''),\n" +
                        "                    IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                 '@',\n" +
                        "                                                                 3),\n" +
                        "                                                 '@',\n" +
                        "                                                 -1)),\n" +
                        "                       CONCAT('2.Surcharge - ',\n" +
                        "                              CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.TAX, 2), ' x '),\n" +
                        "                                                   CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(3 =\n" +
                        "                                                                                              ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                                                                    '@',\n" +
                        "                                                                                                                                    3),\n" +
                        "                                                                                                                    '@',\n" +
                        "                                                                                                                    -1)),\n" +
                        "                                                                                              T.RATE,\n" +
                        "                                                                                              0.00),\n" +
                        "                                                                                           '@',\n" +
                        "                                                                                           3),\n" +
                        "                                                                           '@',\n" +
                        "                                                                           -1)),\n" +
                        "                                                          '% ')),\n" +
                        "                                            ' = '),\n" +
                        "                                     ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                        '@',\n" +
                        "                                                                                        3),\n" +
                        "                                                                        '@',\n" +
                        "                                                                        -1)),\n" +
                        "\n" +
                        "                                              IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  3),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)) > 0 AND\n" +
                        "                                                 T.TAX >=\n" +
                        "                                                 (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  3),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)),\n" +
                        "                                                 T.TAX *\n" +
                        "                                                 (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  3),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)) / 100,\n" +
                        "                                                 0.00),\n" +
                        "                                              '')))),\n" +
                        "                       ''),\n" +
                        "                    IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                 '@',\n" +
                        "                                                                 4),\n" +
                        "                                                 '@',\n" +
                        "                                                 -1)),\n" +
                        "                       '\\n',\n" +
                        "                       ''),\n" +
                        "                    IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                 '@',\n" +
                        "                                                                 4),\n" +
                        "                                                 '@',\n" +
                        "                                                 -1)),\n" +
                        "                       CONCAT('3.Ed Cess - ',\n" +
                        "                              CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.TAX, 2), ' x '),\n" +
                        "                                                   CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(4 =\n" +
                        "                                                                                              ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                                                                    '@',\n" +
                        "                                                                                                                                    4),\n" +
                        "                                                                                                                    '@',\n" +
                        "                                                                                                                    -1)),\n" +
                        "                                                                                              T.RATE,\n" +
                        "                                                                                              0.00),\n" +
                        "                                                                                           '@',\n" +
                        "                                                                                           4),\n" +
                        "                                                                           '@',\n" +
                        "                                                                           -1)),\n" +
                        "                                                          '% ')),\n" +
                        "                                            ' = '),\n" +
                        "                                     ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                        '@',\n" +
                        "                                                                                        4),\n" +
                        "                                                                        '@',\n" +
                        "                                                                        -1)),\n" +
                        "                                              IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  4),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)) > 0 AND\n" +
                        "                                                 T.TAX >=\n" +
                        "                                                 (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  4),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)),\n" +
                        "                                                 T.TAX *\n" +
                        "                                                 (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  4),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)) / 100,\n" +
                        "                                                 0.00),\n" +
                        "                                              '')))),\n" +
                        "                       ''),\n" +
                        "                    IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                 '@',\n" +
                        "                                                                 5),\n" +
                        "                                                 '@',\n" +
                        "                                                 -1)),\n" +
                        "                       '\\n',\n" +
                        "                       ''),\n" +
                        "                    IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                 '@',\n" +
                        "                                                                 5),\n" +
                        "                                                 '@',\n" +
                        "                                                 -1)),\n" +
                        "                       CONCAT('4.Sec Ed Cess - ',\n" +
                        "                              CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.TAX, 2), ' x '),\n" +
                        "                                                   CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(5 =\n" +
                        "                                                                                              ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                                                                    '@',\n" +
                        "                                                                                                                                    5),\n" +
                        "                                                                                                                    '@',\n" +
                        "                                                                                                                    -1)),\n" +
                        "                                                                                              T.RATE,\n" +
                        "                                                                                              0.00),\n" +
                        "                                                                                           '@',\n" +
                        "                                                                                           5),\n" +
                        "                                                                           '@',\n" +
                        "                                                                           -1)),\n" +
                        "                                                          '% ')),\n" +
                        "                                            ' = '),\n" +
                        "                                     ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                        '@',\n" +
                        "                                                                                        5),\n" +
                        "                                                                        '@',\n" +
                        "                                                                        -1)),\n" +
                        "                                              IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  5),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)) > 0 AND\n" +
                        "                                                 T.TAX >=\n" +
                        "                                                 (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  5),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)),\n" +
                        "                                                 T.TAX *\n" +
                        "                                                 (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  5),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)) / 100,\n" +
                        "                                                 0.00),\n" +
                        "                                              '')))),\n" +
                        "                       ''),\n" +
                        "                    '\\n',\n" +
                        "                    CONCAT(''),\n" +
                        "                    '\\n',\n" +
                        "                    CONCAT('Payable to TDS= ',\n" +
                        "                           IF(TAX_AMOUNT > 0, FORMAT(TAX_AMOUNT, 2), 0)),\n" +
                        "                    '\\n',\n" +
                        "                    CONCAT('Payable to Party = ',\n" +
                        "                           IF(PARTY_AMOUNT > 0,\n" +
                        "                              TRIM(LEADING '0' FROM FORMAT(PARTY_AMOUNT, 2)),\n" +
                        "                              0)))\n" +
                        "          END\n" +
                        "       END AS 'TAX_AMOUNT',\n" +
                        "       T.DEDUCT_NOW AS 'Id',\n" +
                        "       T.LEDGER_NAME,\n" +
                        "       T.TAX_AMOUNT AS 'TDS_AMOUNT',\n" +
                        "       T.TDS_TAX_TYPE_ID,\n" +
                        "       T.TDS_RATE,\n" +
                        "       T.DEDUCTION_ID,\n" +
                        "       T.TDS_LIMITATION_AMOUNT,\n" +
                        "       T.TAX_LEDGER_ID\n" +
                        "  FROM (SELECT TB.BOOKING_ID,\n" +
                        "               TBD.BOOKING_DETAIL_ID,\n" +
                        "               TB.BOOKING_DATE,\n" +
                        "               TB.PROJECT_ID,\n" +
                        "               TB.EXPENSE_LEDGER_ID,\n" +
                        "               TB.PARTY_LEDGER_ID,\n" +
                        "               TB.DEDUCTEE_TYPE_ID,\n" +
                        "               TB.VOUCHER_ID,\n" +
                        "               VMT.VOUCHER_NO,\n" +
                        "               VMT.NARRATION,\n" +
                        "               ML.LEDGER_ID,\n" +
                        "               TNP.NATURE_PAY_ID,\n" +
                        "               CASE WHEN TD.AMOUNT>0 THEN  TD.AMOUNT ELSE TBD.ASSESS_AMOUNT END AS PARTY_AMOUNT,\n" +
                        "               TNP.NAME,\n" +
                        "               TTR.TDS_TAX_TYPE_ID,\n" +
                        "               TBD.IS_TDS_DEDUCTED,\n" +
                        "               TCP.PAN_NUMBER,\n" +
                        "               TTR.TDS_RATE,\n" +
                        "               TB.AMOUNT,\n" +
                        "               ROUND(TBD.ASSESS_AMOUNT, 2) AS ASSESS_AMOUNT,\n" +
                        "               GROUP_CONCAT(TTR.TDS_RATE ORDER BY TTR.TDS_TAX_TYPE_ID\n" +
                        "                            SEPARATOR '@') AS Rate,\n" +
                        "               GROUP_CONCAT(TTR.TDS_TAX_TYPE_ID ORDER BY TTR.TDS_TAX_TYPE_ID\n" +
                        "                            SEPARATOR '@') AS TDS_TAX_TYPE,\n" +
                        "               GROUP_CONCAT(TTR.TDS_EXEMPTION_LIMIT ORDER BY\n" +
                        "                            TTR.TDS_TAX_TYPE_ID SEPARATOR '@') AS TDS_LIMITATION_AMOUNT,\n" +
                        "               CASE\n" +
                        "                 WHEN TCP.PAN_NUMBER <> '' THEN\n" +
                        "                  (ASSESS_AMOUNT *\n" +
                        "                  SUBSTRING_INDEX(GROUP_CONCAT(TTR.TDS_RATE ORDER BY\n" +
                        "                                                TTR.TDS_TAX_TYPE_ID SEPARATOR '@'),\n" +
                        "                                   '@',\n" +
                        "                                   1) / 100)\n" +
                        "                 ELSE\n" +
                        "\n" +
                        "                  (ASSESS_AMOUNT * 20 / 100)\n" +
                        "               END as Tax,\n" +
                        "               SUBSTRING_INDEX(GROUP_CONCAT(TTR.TDS_RATE ORDER BY\n" +
                        "                                            TTR.TDS_TAX_TYPE_ID SEPARATOR '@'),\n" +
                        "                               '@',\n" +
                        "                               1) AS T,\n" +
                        "               CASE\n" +
                        "                 WHEN TBD.IS_TDS_DEDUCTED = 1 THEN\n" +
                        "                  '0'\n" +
                        "                 ELSE\n" +
                        "                  '1'\n" +
                        "               END AS 'Deduct_Now',\n" +
                        "               ML.LEDGER_NAME,\n" +
                        "               TDD.TAX_AMOUNT,\n" +
                        "               TDD.DEDUCTION_ID,\n" +
                        "               TDD.TAX_LEDGER_ID\n" +
                        "          FROM TDS_BOOKING AS TB\n" +
                        "          LEFT JOIN TDS_BOOKING_DETAIL AS TBD\n" +
                        "            ON TB.BOOKING_ID = TBD.BOOKING_ID\n" +
                        "          LEFT JOIN TDS_DEDUCTION_DETAIL AS TDD\n" +
                        "            ON TBD.BOOKING_DETAIL_ID = TDD.BOOKING_DETAIL_ID\n" +
                        "          LEFT JOIN TDS_DEDUCTION AS TD\n" +
                        "            ON TDD.DEDUCTION_ID = TD.DEDUCTION_ID\n" +
                        "          LEFT JOIN VOUCHER_MASTER_TRANS AS VMT\n" +
                        "            ON TB.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        "          LEFT JOIN MASTER_LEDGER AS ML\n" +
                        "            ON TDD.TAX_LEDGER_ID = ML.LEDGER_ID\n" +
                        "          LEFT JOIN TDS_CREDTIORS_PROFILE AS TCP\n" +
                        "            ON TB.PARTY_LEDGER_ID = TCP.LEDGER_ID\n" +
                        "          LEFT JOIN TDS_NATURE_PAYMENT AS TNP\n" +
                        "            ON TBD.NATURE_OF_PAYMENT_ID = TNP.NATURE_PAY_ID\n" +
                        "          LEFT JOIN TDS_POLICY AS TP\n" +
                        "            ON TBD.NATURE_OF_PAYMENT_ID = TP.TDS_NATURE_PAYMENT_ID\n" +
                        "          LEFT JOIN TDS_TAX_RATE AS TTR\n" +
                        "            ON TP.TDS_POLICY_ID = TTR.TDS_POLICY_ID\n" +
                        "          LEFT JOIN tds_duty_taxtype AS TDT\n" +
                        "            ON TTR.TDS_TAX_TYPE_ID = TDT.TDS_DUTY_TAXTYPE_ID\n" +
                        "          LEFT JOIN TDS_DEDUCTEE_TYPE AS TDTT\n" +
                        "            ON TP.TDS_DEDUCTEE_TYPE_ID = TDTT.DEDUCTEE_TYPE_ID\n" +
                        "          LEFT JOIN (SELECT TDS_NATURE_PAYMENT_ID,\n" +
                        "                           MAX(APPLICABLE_FROM) AS APPLICABLE_FROM\n" +
                        "                      FROM TDS_POLICY AS TP\n" +
                        "                      LEFT JOIN TDS_TAX_RATE AS TTR\n" +
                        "                        ON TP.TDS_POLICY_ID = TTR.TDS_POLICY_ID\n" +
                        "                      LEFT JOIN TDS_BOOKING_DETAIL AS TBD1\n" +
                        "                        ON TP.TDS_NATURE_PAYMENT_ID =\n" +
                        "                           TBD1.NATURE_OF_PAYMENT_ID\n" +
                        "                     WHERE TP.APPLICABLE_FROM <= ?APPLICABLE_FROM\n" +
                        "                       AND TP.TDS_DEDUCTEE_TYPE_ID = ?DEDUCTEE_TYPE_ID\n" +
                        "                     GROUP BY TP.TDS_NATURE_PAYMENT_ID) AS TTD\n" +
                        "            ON TBD.NATURE_OF_PAYMENT_ID = TTD.TDS_NATURE_PAYMENT_ID\n" +
                        "         WHERE TB.BOOKING_ID = ?BOOKING_ID\n" +
                        "           AND TP.TDS_DEDUCTEE_TYPE_ID = ?DEDUCTEE_TYPE_ID\n" +
                        "           AND TP.APPLICABLE_FROM = TTD.APPLICABLE_FROM\n" +
                        "           AND TB.IS_DELETED = 1\n" +
                        "           AND TDT.STATUS = 1\n" +
                        "         GROUP BY TBD.BOOKING_DETAIL_ID, NAME) AS T";





                        break;
                    }

                case SQLCommand.TDSBooking.CheckTDSBookingByID:
                    {
                        query = "SELECT COUNT(*) FROM TDS_BOOKING WHERE VOUCHER_ID=?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.TDSBooking.DeleteBookingByVoucher:
                    {
                        query = @"UPDATE TDS_BOOKING SET IS_DELETED=0 WHERE VOUCHER_ID=?VOUCHER_ID;
                                  UPDATE TDS_DEDUCTION SET IS_DELETED=0 WHERE VOUCHER_ID=?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.TDSBooking.CheckBookingByVoucher:
                    {
                        query = "SELECT COUNT(*) FROM TDS_BOOKING WHERE VOUCHER_ID=?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.TDSBooking.FetchBookingDetailByVoucherId:
                    {
                        query = "SELECT TDD.BOOKING_DETAIL_ID, TD.DEDUCTION_ID\n" +
                        "  FROM TDS_DEDUCTION AS TD\n" +
                        "  LEFT JOIN TDS_DEDUCTION_DETAIL AS TDD\n" +
                        "    ON TD.DEDUCTION_ID = TDD.DEDUCTION_ID\n" +
                        " WHERE TD.VOUCHER_ID = ?VOUCHER_ID";

                        break;
                    }

                case SQLCommand.TDSBooking.UpdateIsTDSDeductedByBookingDetailId:
                    {
                        query = "UPDATE TDS_BOOKING_DETAIL SET IS_TDS_DEDUCTED=0 WHERE BOOKING_DETAIL_ID=?BOOKING_DETAIL_ID";
                        break;
                    }
                case SQLCommand.TDSBooking.DeleteDeduction:
                    {
                        query = "UPDATE TDS_DEDUCTION SET IS_DELETED=0 WHERE DEDUCTION_ID=?DEDUCTION_ID";
                        break;
                    }
                case SQLCommand.TDSBooking.FetchBookingIdByVoucher:
                    {
                        query = "SELECT BOOKING_ID, DEDUCTEE_TYPE_ID\n" +
                        "  FROM TDS_BOOKING AS TB\n" +
                        " WHERE TB.BOOKING_ID IN\n" +
                        "       (SELECT BOOKING_ID\n" +
                        "          FROM TDS_BOOKING_DETAIL AS TBD\n" +
                        "         WHERE TBD.BOOKING_DETAIL_ID IN\n" +
                        "               (SELECT TDD.BOOKING_DETAIL_ID\n" +
                        "                  FROM TDS_DEDUCTION_DETAIL AS TDD\n" +
                        "                 WHERE TDD.DEDUCTION_ID IN\n" +
                        "                       (SELECT DEDUCTION_ID\n" +
                        "                          FROM TDS_DEDUCTION AS TD\n" +
                        "                         WHERE TD.VOUCHER_ID IN (?VOUCHER_ID)))) LIMIT 1";
                        break;
                    }

                case SQLCommand.TDSBooking.FetchLedgerDetailsById:
                    {
                        query = "SELECT ML.LEDGER_ID,ML.GROUP_ID,\n" +
                         "       ML.LEDGER_NAME,MLG.NATURE_ID,\n" +
                         "       TCP.NATURE_OF_PAYMENT_ID,\n" +
                         "       TCP.DEDUTEE_TYPE_ID,\n" +
                         "       ML.IS_TDS_LEDGER\n" +
                         "  FROM MASTER_LEDGER AS ML\n" +
                         " INNER JOIN TDS_CREDTIORS_PROFILE AS TCP\n" +
                         "    ON ML.LEDGER_ID = TCP.LEDGER_ID\n" +
                         " LEFT JOIN MASTER_LEDGER_GROUP AS MLG\n" +
                         "    ON ML.GROUP_ID=MLG.GROUP_ID\n" +
                         " WHERE ML.LEDGER_ID = ?LEDGER_ID\n" +
                         "   AND ML.STATUS = 0";
                        break;
                    }

                case SQLCommand.TDSBooking.TempTDSBooking:
                    {
                        query = @"SELECT T.BOOKING_ID,
                                T.LEDGER_ID,
                                T.NATURE_PAY_ID,
                                T.BOOKING_DETAIL_ID,
                                T.BOOKING_DATE,
                                T.PROJECT_ID,
                                T.EXPENSE_LEDGER_ID,
                                T.PARTY_LEDGER_ID,
                                T.VOUCHER_ID,
                                T.DEDUCTEE_TYPE_ID,
                                T.AMOUNT,
                                T.PARTY_AMOUNT,
                                T.VOUCHER_NO,
                                T.NARRATION,
                                T.NAME,
                                T.PAN_NUMBER,
                                T.ASSESS_AMOUNT,
                                T.RATE,
                                T.TDS_TAX_TYPE,
                                T.TAX,
                                T.IS_TDS_DEDUCTED,
                                CASE
                                  WHEN T.IS_TDS_DEDUCTED = 1 THEN

                                   CASE
                                     WHEN T.PAN_NUMBER <> '' THEN
                                      IF(T.ASSESS_AMOUNT >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,
                                                                                             '@',
                                                                                             1),
                                                                             '@',
                                                                             -1)),
                                         CONCAT(CONCAT('1.TDS Tax -',
                                                       CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.ASSESS_AMOUNT,
                                                                                          2),
                                                                                   'x '),
                                                                            CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(1 = 1,
                                                                                                                       T.RATE,
                                                                                                                       0.00),
                                                                                                                    '@',
                                                                                                                    1),
                                                                                                    '@',
                                                                                                    -1)),
                                                                                   ' % ')),
                                                                     ' = '),
                                                              ROUND(IF(1 = 1,
                                                                       T.ASSESS_AMOUNT *
                                                                       (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                        '@',
                                                                                                        1),
                                                                                        '@',
                                                                                        -1)) / 100,
                                                                       0),
                                                                    2))),
                                                IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                             '@',
                                                                                             3),
                                                                             '@',
                                                                             -1)),
                                                   '\\n',
                                                   ''),
                                                IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                             '@',
                                                                                             3),
                                                                             '@',
                                                                             -1)),
                                                  IF(T.TAX >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,
                                                                                             '@',
                                                                                             3),
                                                                             '@',
                                                                             -1)),
                                                   CONCAT('2.Surcharge -',
                                                          CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.TAX, 2), 'x '),
                                                                               CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(3 = 3,
                                                                                                                          T.RATE,
                                                                                                                          0.00),
                                                                                                                       '@',
                                                                                                                       3),
                                                                                                       '@',
                                                                                                       -1)),
                                                                                      ' % ')),
                                                                        ' = '),
                                                                 ROUND(IF(3 = 3,
                                                                          T.TAX *
                                                                          (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                           '@',
                                                                                                           3),
                                                                                           '@',
                                                                                           -1)) / 100,
                                                                          0),
                                                                       2))),
                                                   ''),''),
                                                IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                             '@',
                                                                                             4),
                                                                             '@',
                                                                             -1)),
                                                   '\\n',
                                                   ''),
                                                IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                             '@',
                                                                                             4),
                                                                             '@',
                                                                             -1)),
                                               IF(T.TAX >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,
                                                                                             '@',
                                                                                             4),
                                                                             '@',
                                                                             -1)),
                                                   CONCAT('3.Ed Cess -',
                                                          CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.TAX, 2), 'x '),
                                                                               CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(4 = 4,
                                                                                                                          T.RATE,
                                                                                                                          0.00),
                                                                                                                       '@',
                                                                                                                       4),
                                                                                                       '@',
                                                                                                       -1)),
                                                                                      ' % ')),
                                                                        ' = '),
                                                                 ROUND(IF(3 = 3,
                                                                          T.TAX *
                                                                          (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                           '@',
                                                                                                           4),
                                                                                           '@',
                                                                                           -1)) / 100,
                                                                          0),
                                                                       2))),
                                                   ''),''),
                                                IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                             '@',
                                                                                             5),
                                                                             '@',
                                                                             -1)),
                                                   '\\n',
                                                   ''),
                                                IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                             '@',
                                                                                             5),
                                                                             '@',
                                                                             -1)),
                                               IF(T.TAX >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,
                                                                                             '@',
                                                                                             5),
                                                                             '@',
                                                                             -1)),
                                                   CONCAT('4.Sec Ed Cess -',
                                                          CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.TAX, 2), 'x '),
                                                                               CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(5 = 5,
                                                                                                                          T.RATE,
                                                                                                                          0.00),
                                                                                                                       '@',
                                                                                                                       5),
                                                                                                       '@',
                                                                                                       -1)),
                                                                                      ' % ')),
                                                                        ' = '),
                                                                 ROUND(IF(5 = 5,
                                                                          T.TAX *
                                                                          (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                           '@',
                                                                                                           5),
                                                                                           '@',
                                                                                           -1)) / 100,
                                                                          0),
                                                                       2))),
                                                   ''),''),
                                                '\\n',
                                                CONCAT(' '),
                                                '\\n',
                                                CONCAT('Payable to TDS =',
                                                       ROUND(IF(1 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                          '@',
                                                                                                          1),
                                                                                          '@',
                                                                                          -1)),
                                                                T.ASSESS_AMOUNT *
                                                                (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                 '@',
                                                                                                 1),
                                                                                 '@',
                                                                                 -1)) / 100,
                                                                0),
                                                             2) +
                                                       IF(T.TAX >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,
                                                                                             '@',
                                                                                             3),
                                                                             '@',
                                                                             -1)),
                                                       ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                          '@',
                                                                                                          3),
                                                                                          '@',
                                                                                          -1)),
                                                                T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                         '@',
                                                                                                         3),
                                                                                         '@',
                                                                                         -1)) / 100,
                                                                0),0),
                                                             2) +
                                                       IF(T.TAX >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,
                                                                                             '@',
                                                                                             4),
                                                                             '@',
                                                                             -1)),
                                                       ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                          '@',
                                                                                                          4),
                                                                                          '@',
                                                                                          -1)),
                                                                T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                         '@',
                                                                                                         4),
                                                                                         '@',
                                                                                         -1)) / 100,
                                                                0),0),
                                                             2) +
                                                      IF(T.TAX >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,
                                                                                             '@',
                                                                                             5),
                                                                             '@',
                                                                             -1)),
                                                       ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                          '@',
                                                                                                          5),
                                                                                          '@',
                                                                                          -1)),
                                                                T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                         '@',
                                                                                                         5),
                                                                                         '@',
                                                                                         -1)) / 100,
                                                                0),0),
                                                             2)),
                                                '\\n',
                                                CONCAT('Payable to Party = ',
                                                       ROUND(T.ASSESS_AMOUNT -
                                                             (ROUND(IF(1 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                                 '@',
                                                                                                                 1),
                                                                                                 '@',
                                                                                                 -1)),
                                                                       T.ASSESS_AMOUNT *
                                                                       (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                        '@',
                                                                                                        1),
                                                                                        '@',
                                                                                        -1)) / 100,
                                                                       0),
                                                                    2) +
                                                               IF(T.TAX >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,
                                                                                             '@',
                                                                                            3),
                                                                             '@',
                                                                             -1)),
                                                             ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                                 '@',
                                                                                                                 3),
                                                                                                 '@',
                                                                                                 -1)),
                                                                       T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                                '@',
                                                                                                                3),
                                                                                                '@',
                                                                                                -1)) / 100,
                                                                       0),0),
                                                                    2) +
                                                            IF(T.TAX >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,
                                                                                             '@',
                                                                                            4),
                                                                             '@',
                                                                             -1)),
                                                             ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                                 '@',
                                                                                                                 4),
                                                                                                 '@',
                                                                                                 -1)),
                                                                       T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                                '@',
                                                                                                                4),
                                                                                                '@',
                                                                                                -1)) / 100,
                                                                       0),0),
                                                                    2) +
                                                            IF(T.TAX >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,
                                                                                             '@',
                                                                                            5),
                                                                             '@',
                                                                             -1)),
                                                             ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                                 '@',
                                                                                                                 5),
                                                                                                 '@',
                                                                                                 -1)),
                                                                       T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                                '@',
                                                                                                                5),
                                                                                                '@',
                                                                                                -1)) / 100,
                                                                       0),0),
                                                                    2)),
                                                             2))),
                                         '')
                                     ELSE
                                         CONCAT(CONCAT('1.TDS Tax -',
                                                       CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.ASSESS_AMOUNT,
                                                                                          2),
                                                                                   ' x '),
                                                                            CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(2 =
                                                                                                                       ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                                                                             '@',
                                                                                                                                                             2),
                                                                                                                                             '@',
                                                                                                                                             -1)),
                                                                                                                       T.RATE,
                                                                                                                       0.00),
                                                                                                                    '@',
                                                                                                                    2),
                                                                                                    '@',
                                                                                                    -1)),
                                                                                   '% ')),
                                                                     ' = '),
                                                              ROUND(IF(2 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                                 '@',
                                                                                                                 2),
                                                                                                 '@',
                                                                                                 -1)),
                                                                       T.ASSESS_AMOUNT *
                                                                       (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                        '@',
                                                                                                        2),
                                                                                        '@',
                                                                                        -1)) / 100,
                                                                       2)))),
                                                IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                             '@',
                                                                                             3),
                                                                             '@',
                                                                             -1)),
                                                   '\\n',
                                                   ''),
                                                IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                             '@',
                                                                                             3),
                                                                             '@',
                                                                             -1)),
                                                   CONCAT('2.Surcharge -',
                                                          CONCAT(CONCAT(CONCAT(CONCAT(0.00, ' x '),
                                                                               CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(3 =
                                                                                                                          ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                                                                                '@',
                                                                                                                                                                3),
                                                                                                                                                '@',
                                                                                                                                                -1)),
                                                                                                                          T.RATE,
                                                                                                                          0.00),
                                                                                                                       '@',
                                                                                                                       3),
                                                                                                       '@',
                                                                                                       -1)),
                                                                                      '% ')),
                                                                        ' = '),
                                                                 ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                                    '@',
                                                                                                                    3),
                                                                                                    '@',
                                                                                                    -1)),
                                                                          T.TAX *
                                                                          (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                           '@',
                                                                                                           3),
                                                                                           '@',
                                                                                           -1)) / 100,
                                                                          2)))),
                                                   ''),
                                                IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                             '@',
                                                                                             4),
                                                                             '@',
                                                                             -1)),
                                                   '\\n',
                                                   ''),
                                                IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                             '@',
                                                                                             4),
                                                                             '@',
                                                                             -1)),
                                                   CONCAT('3.Ed Cess -',
                                                          CONCAT(CONCAT(CONCAT(CONCAT(0.00, ' x '),
                                                                               CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(4 =
                                                                                                                          ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                                                                                '@',
                                                                                                                                                                4),
                                                                                                                                                '@',
                                                                                                                                                -1)),
                                                                                                                          T.RATE,
                                                                                                                          0.00),
                                                                                                                       '@',
                                                                                                                       4),
                                                                                                       '@',
                                                                                                       -1)),
                                                                                      '% ')),
                                                                        ' = '),
                                                                 ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                                    '@',
                                                                                                                    4),
                                                                                                    '@',
                                                                                                    -1)),
                                                                          T.TAX *
                                                                          (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                           '@',
                                                                                                           4),
                                                                                           '@',
                                                                                           -1)) / 100,
                                                                          2)))),
                                                   ''),
                                                IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                             '@',
                                                                                             5),
                                                                             '@',
                                                                             -1)),
                                                   '\\n',
                                                   ''),
                                                IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                             '@',
                                                                                             5),
                                                                             '@',
                                                                             -1)),
                                                   CONCAT('4.Sec Ed Cess -',
                                                          CONCAT(CONCAT(CONCAT(CONCAT(0.00, ' x '),
                                                                               CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(5 =
                                                                                                                          ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                                                                                '@',
                                                                                                                                                                5),
                                                                                                                                                '@',
                                                                                                                                                -1)),
                                                                                                                          T.RATE,
                                                                                                                          0.00),
                                                                                                                       '@',
                                                                                                                       5),
                                                                                                       '@',
                                                                                                       -1)),
                                                                                      '% ')),
                                                                        ' = '),
                                                                 ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                                    '@',
                                                                                                                    5),
                                                                                                    '@',
                                                                                                    -1)),
                                                                          T.TAX *
                                                                          (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                           '@',
                                                                                                           5),
                                                                                           '@',
                                                                                           -1)) / 100,
                                                                          2)))),
                                                   ''),
                                                '\\n',
                                                CONCAT(''),
                                                '\\n',

                                                CONCAT('Payable to TDS =',
                                                       ROUND(IF(2 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                          '@',
                                                                                                          2),
                                                                                          '@',
                                                                                          -1)),
                                                                T.ASSESS_AMOUNT *
                                                                (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                 '@',
                                                                                                 2),
                                                                                 '@',
                                                                                 -1)) / 100,
                                                                0),
                                                             2) +
                                                       ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                          '@',
                                                                                                          3),
                                                                                          '@',
                                                                                          -1)),
                                                                T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                         '@',
                                                                                                         3),
                                                                                         '@',
                                                                                         -1)) / 100,
                                                                0),
                                                             2) +
                                                       ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                          '@',
                                                                                                          4),
                                                                                          '@',
                                                                                          -1)),
                                                                T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                         '@',
                                                                                                         4),
                                                                                         '@',
                                                                                         -1)) / 100,
                                                                0),
                                                             2) +
                                                       ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                          '@',
                                                                                                          5),
                                                                                          '@',
                                                                                          -1)),
                                                                T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                         '@',
                                                                                                         5),
                                                                                         '@',
                                                                                         -1)) / 100,
                                                                0),
                                                             2)),
                                                '\\n',
                                                CONCAT('Payable to Party = ',
                                                       ROUND(T.ASSESS_AMOUNT -
                                                             (ROUND(IF(2 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                                 '@',
                                                                                                                 2),
                                                                                                 '@',
                                                                                                 -1)),
                                                                       T.ASSESS_AMOUNT *
                                                                       (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                        '@',
                                                                                                        2),
                                                                                        '@',
                                                                                        -1)) / 100,
                                                                       0),
                                                                    2) +
                                                             ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                                 '@',
                                                                                                                 3),
                                                                                                 '@',
                                                                                                 -1)),
                                                                       T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                                '@',
                                                                                                                3),
                                                                                                '@',
                                                                                                -1)) / 100,
                                                                       0),
                                                                    2) +
                                                             ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                                 '@',
                                                                                                                 4),
                                                                                                 '@',
                                                                                                 -1)),
                                                                       T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                                '@',
                                                                                                                4),
                                                                                                '@',
                                                                                                -1)) / 100,
                                                                       0),
                                                                    2) +
                                                             ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                                 '@',
                                                                                                                 5),
                                                                                                 '@',
                                                                                                 -1)),
                                                                       T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                                '@',
                                                                                                                5),
                                                                                                '@',
                                                                                                -1)) / 100,
                                                                       0),
                                                                    2)),
                                                             2)))
                                   END
                                  ELSE
                                   CASE
                                     WHEN T.IS_TDS_DEDUCTED = 0 OR T.PAN_NUMBER <> '' THEN
                                      CONCAT(CONCAT('Payable to TDS = ', 0.00),
                                             '\\n',
                                             CONCAT('Payable to Party = ', ROUND(T.ASSESS_AMOUNT, 2)))
                                     ELSE
                                      IF(T.ASSESS_AMOUNT >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,
                                                                                             '@',
                                                                                             2),
                                                                             '@',
                                                                             -1)),
                                         CONCAT(CONCAT('1.TDS Tax -',
                                                       CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.ASSESS_AMOUNT,
                                                                                          2),
                                                                                   ' x '),
                                                                            CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(2 =
                                                                                                                       ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                                                                             '@',
                                                                                                                                                             2),
                                                                                                                                             '@',
                                                                                                                                             -1)),
                                                                                                                       T.RATE,
                                                                                                                       0.00),
                                                                                                                    '@',
                                                                                                                    2),
                                                                                                    '@',
                                                                                                    -1)),
                                                                                   '% ')),
                                                                     ' = '),
                                                              ROUND(IF(2 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                                 '@',
                                                                                                                 2),
                                                                                                 '@',
                                                                                                 -1)),
                                                                       T.ASSESS_AMOUNT *
                                                                       (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                        '@',
                                                                                                        2),
                                                                                        '@',
                                                                                        -1)) / 100,
                                                                       2)))),
                                                IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                             '@',
                                                                                             3),
                                                                             '@',
                                                                             -1)),
                                                   '\\n',
                                                   ''),
                                                IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                             '@',
                                                                                             3),
                                                                             '@',
                                                                             -1)),
                                                   CONCAT('2.Surcharge -',
                                                          CONCAT(CONCAT(CONCAT(CONCAT(0.00, ' x '),
                                                                               CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(3 =
                                                                                                                          ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                                                                                '@',
                                                                                                                                                                3),
                                                                                                                                                '@',
                                                                                                                                                -1)),
                                                                                                                          T.RATE,
                                                                                                                          0.00),
                                                                                                                       '@',
                                                                                                                       3),
                                                                                                       '@',
                                                                                                       -1)),
                                                                                      '% ')),
                                                                        ' = '),
                                                                 ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                                    '@',
                                                                                                                    3),
                                                                                                    '@',
                                                                                                    -1)),
                                                                          T.TAX *
                                                                          (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                           '@',
                                                                                                           3),
                                                                                           '@',
                                                                                           -1)) / 100,
                                                                          2)))),
                                                   ''),
                                                IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                             '@',
                                                                                             4),
                                                                             '@',
                                                                             -1)),
                                                   '\\n',
                                                   ''),
                                                IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                             '@',
                                                                                             4),
                                                                             '@',
                                                                             -1)),
                                                   CONCAT('3.Ed Cess -',
                                                          CONCAT(CONCAT(CONCAT(CONCAT(0.00, ' x '),
                                                                               CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(4 =
                                                                                                                          ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                                                                                '@',
                                                                                                                                                                4),
                                                                                                                                                '@',
                                                                                                                                                -1)),
                                                                                                                          T.RATE,
                                                                                                                          0.00),
                                                                                                                       '@',
                                                                                                                       4),
                                                                                                       '@',
                                                                                                       -1)),
                                                                                      '% ')),
                                                                        ' = '),
                                                                 ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                                    '@',
                                                                                                                    4),
                                                                                                    '@',
                                                                                                    -1)),
                                                                          T.TAX *
                                                                          (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                           '@',
                                                                                                           4),
                                                                                           '@',
                                                                                           -1)) / 100,
                                                                          2)))),
                                                   ''),
                                                IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                             '@',
                                                                                             5),
                                                                             '@',
                                                                             -1)),
                                                   '\\n',
                                                   ''),
                                                IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                             '@',
                                                                                             5),
                                                                             '@',
                                                                             -1)),
                                                   CONCAT('4.Sec Ed Cess -',
                                                          CONCAT(CONCAT(CONCAT(CONCAT(0.00, ' x '),
                                                                               CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(5 =
                                                                                                                          ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                                                                                '@',
                                                                                                                                                                5),
                                                                                                                                                '@',
                                                                                                                                                -1)),
                                                                                                                          T.RATE,
                                                                                                                          0.00),
                                                                                                                       '@',
                                                                                                                       5),
                                                                                                       '@',
                                                                                                       -1)),
                                                                                      '% ')),
                                                                        ' = '),
                                                                 ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                                    '@',
                                                                                                                    5),
                                                                                                    '@',
                                                                                                    -1)),
                                                                          T.TAX *
                                                                          (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                           '@',
                                                                                                           5),
                                                                                           '@',
                                                                                           -1)) / 100,
                                                                          2)))),
                                                   ''),
                                                '\\n',
                                                CONCAT(''),
                                                '\\n',
                                                CONCAT('Payable to TDS =',
                                                       ROUND(IF(2 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                          '@',
                                                                                                          2),
                                                                                          '@',
                                                                                          -1)),
                                                                T.ASSESS_AMOUNT *
                                                                (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                 '@',
                                                                                                 2),
                                                                                 '@',
                                                                                 -1)) / 100,
                                                                0),
                                                             2) +
                                                       ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                          '@',
                                                                                                          3),
                                                                                          '@',
                                                                                          -1)),
                                                                T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                         '@',
                                                                                                         3),
                                                                                         '@',
                                                                                         -1)) / 100,
                                                                0),
                                                             2) +
                                                       ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                          '@',
                                                                                                          4),
                                                                                          '@',
                                                                                          -1)),
                                                                T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                         '@',
                                                                                                         4),
                                                                                         '@',
                                                                                         -1)) / 100,
                                                                0),
                                                             2) +
                                                       ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                          '@',
                                                                                                          5),
                                                                                          '@',
                                                                                          -1)),
                                                                T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                         '@',
                                                                                                         5),
                                                                                         '@',
                                                                                         -1)) / 100,
                                                                0),
                                                             2)),
                                                '\\n',
                                                CONCAT('Payable to Party = ',
                                                       ROUND(T.ASSESS_AMOUNT -
                                                             (ROUND(IF(2 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                                 '@',
                                                                                                                 2),
                                                                                                 '@',
                                                                                                 -1)),
                                                                       T.ASSESS_AMOUNT *
                                                                       (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                        '@',
                                                                                                        2),
                                                                                        '@',
                                                                                        -1)) / 100,
                                                                       0),
                                                                    2) +
                                                             ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                                 '@',
                                                                                                                 3),
                                                                                                 '@',
                                                                                                 -1)),
                                                                       T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                                '@',
                                                                                                                3),
                                                                                                '@',
                                                                                                -1)) / 100,
                                                                       0),
                                                                    2) +
                                                             ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                                 '@',
                                                                                                                 4),
                                                                                                 '@',
                                                                                                 -1)),
                                                                       T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                                '@',
                                                                                                                4),
                                                                                                '@',
                                                                                                -1)) / 100,
                                                                       0),
                                                                    2) +
                                                             ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,
                                                                                                                 '@',
                                                                                                                 5),
                                                                                                 '@',
                                                                                                 -1)),
                                                                       T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,
                                                                                                                '@',
                                                                                                                5),
                                                                                                '@',
                                                                                                -1)) / 100,
                                                                       0),
                                                                    2)),
                                                             2))),
                                         '')
                                   END
                                END AS 'TAX_AMOUNT',
                                T.DEDUCT_NOW AS 'Id',
                                T.LEDGER_NAME,
                                T.TAX_AMOUNT AS 'TDS_AMOUNT',
                                T.TDS_TAX_TYPE_ID,
                                T.TDS_RATE,
                                T.DEDUCTION_ID,
                                T.TDS_LIMITATION_AMOUNT,
                                T.TAX_LEDGER_ID
                           FROM (SELECT TB.BOOKING_ID,
                                        TBD.BOOKING_DETAIL_ID,
                                        TB.BOOKING_DATE,
                                        TB.PROJECT_ID,
                                        TB.EXPENSE_LEDGER_ID,
                                        TB.PARTY_LEDGER_ID,
                                        TB.DEDUCTEE_TYPE_ID,
                                        TB.VOUCHER_ID,
                                        VMT.VOUCHER_NO,
                                        VMT.NARRATION,
                                        ML.LEDGER_ID,
                                        TNP.NATURE_PAY_ID,
                                        TD.AMOUNT AS PARTY_AMOUNT,
                                        TNP.NAME,
                                        TTR.TDS_TAX_TYPE_ID,
                                        TBD.IS_TDS_DEDUCTED,
                                        TCP.PAN_NUMBER,
                                        TTR.TDS_RATE,
                                        TB.AMOUNT,
                                        ROUND(TBD.ASSESS_AMOUNT, 2) AS ASSESS_AMOUNT,
                                        GROUP_CONCAT(TTR.TDS_RATE ORDER BY TTR.TDS_TAX_TYPE_ID
                                                     SEPARATOR '@') AS Rate,
                                        GROUP_CONCAT(TTR.TDS_TAX_TYPE_ID ORDER BY TTR.TDS_TAX_TYPE_ID
                                                     SEPARATOR '@') AS TDS_TAX_TYPE,
                                        GROUP_CONCAT(TTR.TDS_EXEMPTION_LIMIT ORDER BY
                                                     TTR.TDS_TAX_TYPE_ID SEPARATOR '@') AS TDS_LIMITATION_AMOUNT,
                                        ROUND((ASSESS_AMOUNT *
                                              SUBSTRING_INDEX(GROUP_CONCAT(TTR.TDS_RATE ORDER BY
                                                                            TTR.TDS_TAX_TYPE_ID
                                                                            SEPARATOR '@'),
                                                               '@',
                                                               1)) / 100,
                                              2) as Tax,
                                        SUBSTRING_INDEX(GROUP_CONCAT(TTR.TDS_RATE ORDER BY
                                                                     TTR.TDS_TAX_TYPE_ID SEPARATOR '@'),
                                                        '@',
                                                        1) AS T,
                                        CASE
                                          WHEN TBD.IS_TDS_DEDUCTED = 1 THEN
                                           '0'
                                          ELSE
                                           '1'
                                        END AS 'Deduct_Now',
                                        ML.LEDGER_NAME,
                                        TDD.TAX_AMOUNT,
                                        TDD.DEDUCTION_ID,
                                        TDD.TAX_LEDGER_ID
                                   FROM TDS_BOOKING AS TB
                                   LEFT JOIN TDS_BOOKING_DETAIL AS TBD
                                     ON TB.BOOKING_ID = TBD.BOOKING_ID
                                   LEFT JOIN TDS_DEDUCTION_DETAIL AS TDD
                                     ON TBD.BOOKING_DETAIL_ID = TDD.BOOKING_DETAIL_ID
                                   LEFT JOIN TDS_DEDUCTION AS TD
                                     ON TDD.DEDUCTION_ID=TD.DEDUCTION_ID
                                   LEFT JOIN VOUCHER_MASTER_TRANS AS VMT
                                     ON TB.VOUCHER_ID = VMT.VOUCHER_ID
                                   LEFT JOIN MASTER_LEDGER AS ML
                                     ON TDD.TAX_LEDGER_ID = ML.LEDGER_ID
                                   LEFT JOIN TDS_CREDTIORS_PROFILE AS TCP
                                     ON TB.PARTY_LEDGER_ID = TCP.LEDGER_ID
                                   LEFT JOIN TDS_NATURE_PAYMENT AS TNP
                                     ON TBD.NATURE_OF_PAYMENT_ID = TNP.NATURE_PAY_ID
                                   LEFT JOIN TDS_POLICY AS TP
                                     ON TBD.NATURE_OF_PAYMENT_ID = TP.TDS_NATURE_PAYMENT_ID
                                   LEFT JOIN TDS_TAX_RATE AS TTR
                                     ON TP.TDS_POLICY_ID = TTR.TDS_POLICY_ID
                                   LEFT JOIN tds_duty_taxtype AS TDT
                                     ON TTR.TDS_TAX_TYPE_ID = TDT.TDS_DUTY_TAXTYPE_ID
                                   LEFT JOIN TDS_DEDUCTEE_TYPE AS TDTT
                                     ON TP.TDS_DEDUCTEE_TYPE_ID = TDTT.DEDUCTEE_TYPE_ID
                                   LEFT JOIN (SELECT TDS_NATURE_PAYMENT_ID,
                                                    MAX(APPLICABLE_FROM) AS APPLICABLE_FROM
                                               FROM TDS_POLICY AS TP
                                               LEFT JOIN TDS_TAX_RATE AS TTR
                                                 ON TP.TDS_POLICY_ID = TTR.TDS_POLICY_ID
                                               LEFT JOIN TDS_BOOKING_DETAIL AS TBD1
                                                 ON TP.TDS_NATURE_PAYMENT_ID =
                                                    TBD1.NATURE_OF_PAYMENT_ID
                                              WHERE TP.APPLICABLE_FROM <= ?APPLICABLE_FROM
                                                AND TP.TDS_DEDUCTEE_TYPE_ID =?DEDUCTEE_TYPE_ID
                                              GROUP BY TP.TDS_NATURE_PAYMENT_ID) AS TTD
                                     ON TBD.NATURE_OF_PAYMENT_ID = TTD.TDS_NATURE_PAYMENT_ID
                                  WHERE TB.BOOKING_ID = ?BOOKING_ID
                                    AND TP.TDS_DEDUCTEE_TYPE_ID = ?DEDUCTEE_TYPE_ID
                                    AND TP.APPLICABLE_FROM = TTD.APPLICABLE_FROM
                                    AND TB.IS_DELETED = 1
                                    AND TDT.STATUS = 1
                                  GROUP BY TBD.BOOKING_DETAIL_ID, NAME) AS T";
                        break;
                    }
                case SQLCommand.TDSBooking.FetchBookingIdByVoucherId:
                    {
                        query = "SELECT BOOKING_ID FROM TDS_BOOKING WHERE VOUCHER_ID=?VOUCHER_ID AND IS_DELETED=1";
                        break;
                    }
                case SQLCommand.TDSBooking.CheckIsTDSBookingVoucher:
                    {
                        query = "SELECT COUNT(*) FROM TDS_BOOKING WHERE VOUCHER_ID=?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.TDSBooking.CheckIsTDSPaymentVoucher:
                    {
                        query = "SELECT COUNT(*) FROM TDS_PAYMENT WHERE VOUCHER_ID=?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.TDSBooking.CheckIsTDSDeductionVoucher:
                    {
                        query = "SELECT COUNT(*) FROM TDS_DEDUCTION WHERE VOUCHER_ID=?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.TDSBooking.ExpenseLedgerAmount:
                    {
                        query = "SELECT EXPENSE_LEDGER_ID, PROJECT_ID, SUM(AMOUNT) AS AMOUNT,TBD.IS_TDS_DEDUCTED\n" +
                        "  FROM TDS_BOOKING AS TB\n" +
                        " INNER JOIN TDS_BOOKING_DETAIL AS TBD\n" +
                        "    ON TB.BOOKING_ID = TBD.BOOKING_ID\n" +
                        " WHERE PROJECT_ID = ?PROJECT_ID\n" +
                        "   AND EXPENSE_LEDGER_ID = ?EXPENSE_LEDGER_ID\n" +
                        "   AND IS_DELETED = 1\n" +
                        " GROUP BY TBD.IS_TDS_DEDUCTED ORDER BY TBD.IS_TDS_DEDUCTED";
                        break;
                    }
                case SQLCommand.TDSBooking.CheckVoucherInBooking:
                    {
                        query = "SELECT COUNT(*) FROM TDS_BOOKING WHERE VOUCHER_ID=?VOUCHER_ID";
                        break;
                    }

                case SQLCommand.TDSBooking.CheckHasBooking:
                    {
                        query = "SELECT COUNT(*) FROM TDS_BOOKING WHERE EXPENSE_LEDGER_ID =?EXPENSE_LEDGER_ID OR PARTY_LEDGER_ID=?PARTY_LEDGER_ID AND IS_DELETED=1";
                        break;
                    }
                case SQLCommand.TDSBooking.CheckHasDeduction:
                    {
                        query = "SELECT COUNT(*)\n" +
                        "  FROM TDS_DEDUCTION_DETAIL AS TDD\n" +
                        " INNER JOIN TDS_DEDUCTION AS TD\n" +
                        "    ON TDD.DEDUCTION_ID = TD.DEDUCTION_ID\n" +
                        " WHERE TAX_LEDGER_ID = ?TAX_LEDGER_ID\n" +
                        "   AND IS_DELETED = 1";
                        break;
                    }
                case SQLCommand.TDSBooking.FetchBookingVIDbyPartyVID:
                    { 
                        /* Input Voucher Id is Journal VID 
                       and to Fetch If this voucher is done while Party Payment Voucher.
                          Output : Party Payment Voucher If exists */
                        query = "SELECT VOUCHER_ID\n" +
                                "  FROM VOUCHER_MASTER_TRANS\n" +
                                " WHERE VOUCHER_SUB_TYPE = 'TDS'\n" +
                                "   AND CLIENT_CODE = 'TDS'\n" +
                                "   AND CLIENT_REFERENCE_ID = ?VOUCHER_ID;";
                        break;
                    }
            }
            return query;
        }
        #endregion TDS SQL
    }
}
