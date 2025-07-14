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
    public class PartyPaymentSQL : IDatabaseQuery
    {
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        public string GetQuery(DAO.Data.DataCommandArguments dataCommandArgs, ref DAO.Data.SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.TDSPartyPayment).FullName)
            {
                query = GetBooking();
            }

            sqlType = this.sqlType;
            return query;
        }

        private string GetBooking()
        {
            string query = "";
            SQLCommand.TDSPartyPayment sqlCommandId = (SQLCommand.TDSPartyPayment)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.TDSPartyPayment.Add:
                    {
                        query = @"INSERT INTO TDS_PARTY_PAYMENT
                                  (PAYMENT_DATE,
                                   PROJECT_ID,
                                   PARTY_LEDGER_ID,
                                   PAYMENT_LEDGER_ID,
                                   VOUCHER_ID)
                                VALUES
                                  (?PAYMENT_DATE,
                                   ?PROJECT_ID,
                                   ?LEDGER_ID,
                                   ?PAYMENT_LEDGER_ID,
                                   ?VOUCHER_ID)";
                        break;
                    }
                case SQLCommand.TDSPartyPayment.Update:
                    {
                        //query = "SELECT *\n" +
                        //        "  FROM ((SELECT TDSB.PROJECT_ID,\n" +
                        //        "                TDSB.PARTY_LEDGER_ID AS LEDGER_ID,\n" +
                        //        "                1 AS SORT_ID,\n" +
                        //        "                VOUCHER_NO,\n" +
                        //        "                BOOKING_DATE AS VOUCHER_DATE,\n" +
                        //        "                ROUND(AMOUNT, 2) AS AMOUNT,\n" +
                        //        "                ROUND(AMOUNT, 2) AS TEMP_AMOUNT,\n" +
                        //        "                'DR' AS TRANS_MODE,\n" +
                        //        "                '' AS CHEQUE_NO,\n" +
                        //        "                '' AS MATERIALIZED_ON,\n" +
                        //        "                0 AS BOOKING_DETAIL_ID,\n" +
                        //        "                0 AS DEDUCTION_DETAIL_ID\n" +
                        //        "           FROM TDS_BOOKING TDSB\n" +
                        //        "           LEFT JOIN VOUCHER_MASTER_TRANS VMT\n" +
                        //        "             ON TDSB.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        //        "          WHERE VMT.VOUCHER_SUB_TYPE = 'TDS'\n" +
                        //        "            AND IS_DELETED = 1\n" +
                        //        "            AND TDSB.PROJECT_ID =?PROJECT_ID\n" +
                        //        "            AND PARTY_LEDGER_ID =?LEDGER_ID\n" +
                        //        "            AND BOOKING_DATE <=?BOOKING_DATE) UNION\n" +
                        //        "        (SELECT TDSPP.PROJECT_ID,\n" +
                        //        "                TDSPP.PARTY_LEDGER_ID AS LEDGER_ID,\n" +
                        //        "                2 AS SORT_ID,\n" +
                        //        "                VOUCHER_NO,\n" +
                        //        "                PAYMENT_DATE AS VOUCHER_DATE,\n" +
                        //        "                ROUND(AMOUNT - PAID_AMOUNT, 2) AS AMOUNT,\n" +
                        //        "                ROUND(PAID_AMOUNT, 2) AS TEMP_AMOUNT,\n" +
                        //        "                \"CR\" AS TRANS_MODE,\n" +
                        //        "                '' AS CHEQUE_NO,\n" +
                        //        "                '' AS MATERIALIZED_ON,\n" +
                        //        "                TDSBD.BOOKING_DETAIL_ID,\n" +
                        //        "                TDSDD.DEDUCTION_DETAIL_ID\n" +
                        //        "           FROM TDS_PARTY_PAYMENT TDSPP\n" +
                        //        "           LEFT JOIN TDS_PARTY_PAYMENT_DETAIL PPD\n" +
                        //        "             ON TDSPP.PARTY_PAYMENT_ID = PPD.PARTY_PAYMENT_ID\n" +
                        //        "           LEFT JOIN TDS_BOOKING_DETAIL TDSBD\n" +
                        //        "             ON PPD.BOOKING_DETAIL_ID = TDSBD.BOOKING_DETAIL_ID\n" +
                        //        "           LEFT JOIN TDS_DEDUCTION_DETAIL TDSDD\n" +
                        //        "             ON TDSDD.BOOKING_DETAIL_ID = TDSBD.BOOKING_ID\n" +
                        //        "           LEFT JOIN TDS_BOOKING TDSB\n" +
                        //        "             ON TDSB.BOOKING_ID = TDSBD.BOOKING_ID\n" +
                        //        "           LEFT JOIN VOUCHER_MASTER_TRANS VMT\n" +
                        //        "             ON TDSB.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        //        "          WHERE TDSPP.PROJECT_ID =?PROJECT_ID\n" +
                        //        "            AND TDSPP.PARTY_PAYMENT_ID =?PARTY_PAYMENT_ID\n" +
                        //        "          GROUP BY TDSPP.PAYMENT_DATE)) AS T\n" +
                        //        " GROUP BY SORT_ID";

                        query = "SELECT T.* FROM (SELECT TB.BOOKING_DATE AS VOUCHER_DATE,\n" +
                        "              VMT.VOUCHER_NO,\n" +
                        "              TB.PROJECT_ID,\n" +
                        "              TB.VOUCHER_ID,\n" +
                        "              TB.BOOKING_ID,\n" +
                        "              TBD.BOOKING_DETAIL_ID,\n" +
                        "              TPPD.PARTY_PAYMENT_ID,\n" +
                        "              TPPD.PARTY_PAYMENT_DETAIL_ID,\n" +
                        "              TPPD.DEDUCTION_DETAIL_ID,\n" +
                        "              TB.PARTY_LEDGER_ID AS LEDGER_ID,\n" +
                        "              TD.DEDUCTION_ID,\n" +
                        "              0 AS TEMP_AMOUNT,\n" +
                        "              1 AS SOURCE,\n" +
                        "              1 AS SORT_ID,\n" +
                        "              TB.AMOUNT AS ACTUAL_AMOUNT,\n" +
                        "              TD.AMOUNT AS 'PARTY_AMOUNT',\n" +
                        "              TPPD.PAID_AMOUNT AS 'AMOUNT',\n" +
                        "              TPPD.PAID_AMOUNT AS 'REMAINING_BALANCE',\n" +
                        "              'DR' AS TRANS_MODE,\n" +
                        "              '' AS 'CHEQUE_NO',\n" +
                        "              '' AS 'MATERIALIZED_ON'\n" +
                        "         FROM TDS_BOOKING AS TB\n" +
                        "         LEFT JOIN VOUCHER_MASTER_TRANS AS VMT\n" +
                        "           ON TB.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        "         LEFT JOIN TDS_BOOKING_DETAIL AS TBD\n" +
                        "           ON TB.BOOKING_ID = TBD.BOOKING_ID\n" +
                        "        LEFT JOIN TDS_DEDUCTION AS TD\n" +
                        "           ON TB.VOUCHER_ID=TD.VOUCHER_ID\n" +
                        "         LEFT JOIN TDS_DEDUCTION_DETAIL AS TDD\n" +
                        "           ON TD.DEDUCTION_ID=TDD.DEDUCTION_ID\n" +
                        "         LEFT JOIN TDS_PARTY_PAYMENT_DETAIL AS TPPD\n" +
                        "           ON TBD.BOOKING_DETAIL_ID = TPPD.BOOKING_DETAIL_ID\n" +
                        "           AND TDD.DEDUCTION_DETAIL_ID=TPPD.DEDUCTION_DETAIL_ID\n" +
                        "            LEFT JOIN TDS_PARTY_PAYMENT AS TPP\n" +
                        "           ON TPPD.PARTY_PAYMENT_ID=TPP.PARTY_PAYMENT_ID\n" +
                        "        WHERE TB.IS_DELETED = 1\n" +
                        "          AND TPP.VOUCHER_ID=?VOUCHER_ID\n" +
                        "         GROUP BY TB.BOOKING_ID) AS T WHERE T.AMOUNT>0 ";

                        break;
                    }
                case SQLCommand.TDSPartyPayment.LogicalDelete:
                    {
                        //                        query = @"UPDATE TDS_PARTY_PAYMENT SET IS_DELETED=0 WHERE PARTY_PAYMENT_ID=?PAYMENT_LEDGER_ID;
                        //                                  DELETE FROM TDS_PARTY_PAYMENT_DETAIL WHERE PARTY_PAYMENT_ID=?PAYMENT_LEDGER_ID;";
                        query = "UPDATE TDS_PARTY_PAYMENT SET IS_DELETED=0 WHERE VOUCHER_ID=?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.TDSPartyPayment.PhysicalDelete:
                    {
                        query = @"DELETE FROM TDS_PARTY_PAYMENT_DETAIL WHERE PARTY_PAYMENT_ID in (?PAYMENT_LEDGER_ID);
                                  DELETE FROM TDS_PARTY_PAYMENT WHERE PARTY_PAYMENT_ID in (?PAYMENT_LEDGER_ID);";
                        break;
                    }

                case SQLCommand.TDSPartyPayment.FetchAllPartyPayment:
                    {
                        query = @"SELECT PAYMENT_DATE AS VOUCHER_DATE,
                                           PP.PARTY_PAYMENT_ID AS TDS_PAYMENT_ID,
                                           VMT.VOUCHER_ID,
                                           PP.PARTY_LEDGER_ID,
                                           PAYMENT_LEDGER_ID,
                                           MP.PROJECT_ID,
                                           VOUCHER_NO,
                                           'Payment' AS VOUCHER_TYPE,
                                           PROJECT,
                                           LEDGER_NAME AS CASHBANK,
                                           AMOUNT,
                                           NARRATION
                                      FROM TDS_PARTY_PAYMENT PP
                                      LEFT JOIN VOUCHER_MASTER_TRANS VMT
                                        ON PP.VOUCHER_ID = VMT.VOUCHER_ID
                                      LEFT JOIN MASTER_PROJECT MP
                                        ON MP.PROJECT_ID = PP.PROJECT_ID
                                      LEFT JOIN MASTER_LEDGER ML
                                        ON PP.PAYMENT_LEDGER_ID = ML.LEDGER_ID
                                      LEFT JOIN TDS_PARTY_PAYMENT_DETAIL PPD
                                        ON PP.PARTY_PAYMENT_ID = PPD.PARTY_PAYMENT_ID
                                      LEFT JOIN TDS_BOOKING_DETAIL TDSBD
                                        ON TDSBD.BOOKING_DETAIL_ID = PPD.BOOKING_DETAIL_ID
                                      LEFT JOIN TDS_BOOKING TDSB
                                        ON TDSB.BOOKING_ID = TDSBD.BOOKING_ID
                                     WHERE PP.PROJECT_ID =?PROJECT_ID
                                       AND PP.IS_DELETED = 1
                                       AND TDSB.IS_DELETED=1
                                       AND PAYMENT_DATE BETWEEN ?BOOKING_DATE AND ?DATE_CLOSED
                                     GROUP BY PP.PARTY_LEDGER_ID;";
                        break;
                    }

                case SQLCommand.TDSPartyPayment.FetchPaymentByParyPaymentId:
                    {
                        query = @"SELECT LEDGER_ID,PARTY_PAYMENT_ID AS TDS_PAYMENT_ID,
                                       LEDGER_NAME,
                                       IF(TYPE = 'CR', AMOUNT, '') AS CREDIT,
                                       IF(TYPE = 'DR', AMOUNT, '') AS DEBIT
                                  FROM ((SELECT LEDGER_ID,TDSPP.PARTY_PAYMENT_ID,
                                                LEDGER_NAME,
                                                PAID_AMOUNT AS AMOUNT,
                                                'DR' TYPE,
                                                2 AS SORT_ID
                                           FROM TDS_PARTY_PAYMENT TDSPP
                                           LEFT JOIN TDS_PARTY_PAYMENT_DETAIL TDSPPD
                                             ON TDSPP.PARTY_PAYMENT_ID = TDSPPD.PARTY_PAYMENT_ID
                                           LEFT JOIN MASTER_LEDGER ML
                                             ON TDSPP.PARTY_LEDGER_ID = ML.LEDGER_ID
                                             WHERE IS_DELETED=1 AND FIND_IN_SET(TDSPPD.PARTY_PAYMENT_ID,?IDs))
                                       UNION
                                       (SELECT LEDGER_ID,TDSPP.PARTY_PAYMENT_ID,
                                                LEDGER_NAME,
                                                SUM(PAID_AMOUNT) AS AMOUNT,
                                                'CR' TYPE,
                                                1 AS SORT_ID
                                           FROM TDS_PARTY_PAYMENT TDSPP
                                           LEFT JOIN TDS_PARTY_PAYMENT_DETAIL TDSPPD
                                             ON TDSPP.PARTY_PAYMENT_ID = TDSPPD.PARTY_PAYMENT_ID
                                           LEFT JOIN MASTER_LEDGER ML
                                             ON TDSPP.PAYMENT_LEDGER_ID = ML.LEDGER_ID
                                          WHERE IS_DELETED=1 AND FIND_IN_SET(TDSPPD.PARTY_PAYMENT_ID,?IDs))) AS P
                                 ORDER BY SORT_ID;";
                        break;
                    }
                case SQLCommand.TDSPartyPayment.FetchPendingPartyPayment:
                    {
                        //query = "SELECT TT.*\n" +
                        //         "  FROM (SELECT TB.BOOKING_DATE AS VOUCHER_DATE,\n" +
                        //         "               VMT.VOUCHER_NO,\n" +
                        //         "               TB.PROJECT_ID,\n" +
                        //         "               TB.VOUCHER_ID,\n" +
                        //         "               TB.BOOKING_ID,\n" +
                        //         "               TBD.BOOKING_DETAIL_ID,\n" +
                        //         "               TPPD.PARTY_PAYMENT_ID,\n" +
                        //         "               TPPD.PARTY_PAYMENT_DETAIL_ID,\n" +
                        //         "               TPPD.DEDUCTION_DETAIL_ID,\n" +
                        //         "               PARTY_LEDGER_ID AS LEDGER_ID,\n" +
                        //         "               0 AS TEMP_AMOUNT,\n" +
                        //         "               1 AS SOURCE,\n" +
                        //         "               1 AS SORT_ID,\n" +
                        //         "               AMOUNT AS ACTUAL_AMOUNT,\n" +
                        //         "               TPPD.PAID_AMOUNT,\n" +
                        //         "               '' AS 'CHEQUE_NO',\n" +
                        //         "               '' AS 'MATERIALIZED_ON',\n" +
                        //         "               CASE\n" +
                        //         "                 WHEN TB.AMOUNT > 0 THEN\n" +
                        //         "                  CASE\n" +
                        //         "                    WHEN TB.AMOUNT > SUM(TPPD.PAID_AMOUNT) THEN\n" +
                        //         "                     TB.AMOUNT - SUM(TPPD.PAID_AMOUNT)\n" +
                        //         "                    ELSE\n" +
                        //         "                     CASE\n" +
                        //         "                       WHEN TB.AMOUNT < SUM(TPPD.PAID_AMOUNT) THEN\n" +
                        //         "                      -(SUM(TPPD.PAID_AMOUNT) - TB.AMOUNT)\n" +
                        //         "                       ELSE\n" +
                        //         "                        CASE\n" +
                        //         "                          WHEN TB.AMOUNT = SUM(TPPD.PAID_AMOUNT) THEN\n" +
                        //         "                           TB.AMOUNT - TPPD.PAID_AMOUNT\n" +
                        //         "                          ELSE\n" +
                        //         "                           TB.AMOUNT\n" +
                        //         "                        END\n" +
                        //         "                     END\n" +
                        //         "                  END\n" +
                        //         "               END AS 'AMOUNT',\n" +
                        //         "               CASE\n" +
                        //         "                 WHEN TB.AMOUNT > 0 THEN\n" +
                        //         "                  CASE\n" +
                        //         "                    WHEN TB.AMOUNT > SUM(TPPD.PAID_AMOUNT) THEN\n" +
                        //         "                     TB.AMOUNT - SUM(TPPD.PAID_AMOUNT)\n" +
                        //         "                    ELSE\n" +
                        //         "                     CASE\n" +
                        //         "                       WHEN TB.AMOUNT < SUM(TPPD.PAID_AMOUNT) THEN\n" +
                        //         "                        SUM(TPPD.PAID_AMOUNT) - TB.AMOUNT\n" +
                        //         "                       ELSE\n" +
                        //         "                        CASE\n" +
                        //         "                          WHEN TB.AMOUNT = SUM(TPPD.PAID_AMOUNT) THEN\n" +
                        //         "                           TB.AMOUNT - TPPD.PAID_AMOUNT\n" +
                        //         "                          ELSE\n" +
                        //         "                           TB.AMOUNT\n" +
                        //         "                        END\n" +
                        //         "                     END\n" +
                        //         "                  END\n" +
                        //         "               END AS 'ACTUAL_TEMP_AMOUNT',\n" +
                        //         "               'DR' AS TRANS_MODE\n" +
                        //         "          FROM TDS_BOOKING AS TB\n" +
                        //         "          LEFT JOIN VOUCHER_MASTER_TRANS AS VMT\n" +
                        //         "            ON TB.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        //         "          LEFT JOIN TDS_BOOKING_DETAIL AS TBD\n" +
                        //         "            ON TB.BOOKING_ID = TBD.BOOKING_ID\n" +
                        //         "          LEFT JOIN TDS_PARTY_PAYMENT_DETAIL AS TPPD\n" +
                        //         "            ON TBD.BOOKING_DETAIL_ID = TPPD.BOOKING_DETAIL_ID\n" +
                        //         "         WHERE TB.IS_DELETED = 1\n" +
                        //         "           AND TB.BOOKING_DATE <= ?BOOKING_DATE\n" +
                        //         "           AND TB.PROJECT_ID = ?PROJECT_ID\n" +
                        //         "           AND TB.PARTY_LEDGER_ID = ?LEDGER_ID\n" +
                        //         "         GROUP BY BOOKING_ID\n" +
                        //         "\n" +
                        //         "        UNION ALL\n" +
                        //         "\n" +
                        //         "        SELECT TB.BOOKING_DATE AS VOUCHER_DATE,\n" +
                        //         "               VMT.VOUCHER_NO,\n" +
                        //         "               TB.PROJECT_ID,\n" +
                        //         "               TB.VOUCHER_ID,\n" +
                        //         "               TB.BOOKING_ID,\n" +
                        //         "               TBD.BOOKING_DETAIL_ID,\n" +
                        //         "               TPPD.PARTY_PAYMENT_ID,\n" +
                        //         "               TPPD.PARTY_PAYMENT_DETAIL_ID,\n" +
                        //         "               TDD.DEDUCTION_DETAIL_ID,\n" +
                        //         "               TPP.PARTY_LEDGER_ID AS LEDGER_ID,\n" +
                        //         "               0 AS TEMP_AMOUNT,\n" +
                        //         "               1 AS SOURCE,\n" +
                        //         "               2 AS SORT_ID,\n" +
                        //         "               SUM(TAX_AMOUNT) AS ACTUAL_AMOUNT,\n" +
                        //         "               TPPD.PAID_AMOUNT,\n" +
                        //         "               '' AS 'CHEQUE_NO',\n" +
                        //         "               '' AS 'MATERIALIZED_ON',\n" +
                        //         "               CASE\n" +
                        //         "                 WHEN SUM(TAX_AMOUNT) > 0 THEN\n" +
                        //         "                  CASE\n" +
                        //         "                    WHEN SUM(TAX_AMOUNT) > SUM(TPPD.PAID_AMOUNT) THEN\n" +
                        //         "                     SUM(TAX_AMOUNT) - SUM(TPPD.PAID_AMOUNT)\n" +
                        //         "                    ELSE\n" +
                        //         "                     CASE\n" +
                        //         "                       WHEN SUM(TAX_AMOUNT) < SUM(TPPD.PAID_AMOUNT) THEN\n" +
                        //         "                      -(SUM(TPPD.PAID_AMOUNT) - SUM(TAX_AMOUNT))\n" +
                        //         "                       ELSE\n" +
                        //         "                        CASE\n" +
                        //         "                          WHEN SUM(TAX_AMOUNT) = SUM(TPPD.PAID_AMOUNT) THEN\n" +
                        //         "                           SUM(TAX_AMOUNT) - TPPD.PAID_AMOUNT\n" +
                        //         "                          ELSE\n" +
                        //         "                           SUM(TAX_AMOUNT)\n" +
                        //         "                        END\n" +
                        //         "                     END\n" +
                        //         "                  END\n" +
                        //         "               END AS 'AMOUNT',\n" +
                        //         "               CASE\n" +
                        //         "                 WHEN SUM(TAX_AMOUNT) > 0 THEN\n" +
                        //         "                  CASE\n" +
                        //         "                    WHEN SUM(TAX_AMOUNT) > SUM(TPPD.PAID_AMOUNT) THEN\n" +
                        //         "                     SUM(TAX_AMOUNT) - SUM(TPPD.PAID_AMOUNT)\n" +
                        //         "                    ELSE\n" +
                        //         "                     CASE\n" +
                        //         "                       WHEN SUM(TAX_AMOUNT) < SUM(TPPD.PAID_AMOUNT) THEN\n" +
                        //         "                        SUM(TPPD.PAID_AMOUNT) - SUM(TAX_AMOUNT)\n" +
                        //         "                       ELSE\n" +
                        //         "                        CASE\n" +
                        //         "                          WHEN SUM(TAX_AMOUNT) = SUM(TPPD.PAID_AMOUNT) THEN\n" +
                        //         "                           SUM(TAX_AMOUNT) - TPPD.PAID_AMOUNT\n" +
                        //         "                          ELSE\n" +
                        //         "                           SUM(TAX_AMOUNT)\n" +
                        //         "                        END\n" +
                        //         "                     END\n" +
                        //         "                  END\n" +
                        //         "               END AS 'ACTUAL_TEMP_AMOUNT',\n" +
                        //         "               'CR' AS TRANS_MODE\n" +
                        //         "          FROM TDS_BOOKING AS TB\n" +
                        //         "          LEFT JOIN VOUCHER_MASTER_TRANS AS VMT\n" +
                        //         "            ON TB.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        //         "          LEFT JOIN TDS_BOOKING_DETAIL AS TBD\n" +
                        //         "            ON TB.BOOKING_ID = TBD.BOOKING_ID\n" +
                        //         "          LEFT JOIN TDS_DEDUCTION_DETAIL AS TDD\n" +
                        //         "            ON TBD.BOOKING_DETAIL_ID = TDD.BOOKING_DETAIL_ID\n" +
                        //         "          LEFT JOIN TDS_PARTY_PAYMENT_DETAIL AS TPPD\n" +
                        //         "            ON TBD.BOOKING_DETAIL_ID = TPPD.BOOKING_DETAIL_ID\n" +
                        //         "          LEFT JOIN TDS_PARTY_PAYMENT AS TPP\n" +
                        //         "            ON TPPD.PARTY_PAYMENT_ID = TPP.PARTY_PAYMENT_ID\n" +
                        //         "         WHERE TB.IS_DELETED = 1\n" +
                        //         "           AND VMT.VOUCHER_TYPE IN ('JN')\n" +
                        //         "           AND TB.BOOKING_DATE <= ?BOOKING_DATE\n" +
                        //         "           AND TB.PROJECT_ID = ?PROJECT_ID\n" +
                        //         "           AND TB.PARTY_LEDGER_ID = ?LEDGER_ID\n" +
                        //         "         GROUP BY BOOKING_ID) AS TT\n" +
                        //         "           WHERE TT.AMOUNT>0\n" +
                        //         " ORDER BY TT.VOUCHER_ID, SORT_ID ASC";

                       query = "SELECT TT.*\n" +
                        "  FROM (\n" +
                        "\n" +
                        "        SELECT TB.BOOKING_DATE AS VOUCHER_DATE,\n" +
                        "                VMT.VOUCHER_NO,\n" +
                        "                TB.PROJECT_ID,\n" +
                        "                TB.VOUCHER_ID,\n" +
                        "                TB.BOOKING_ID,\n" +
                        "                TBD.BOOKING_DETAIL_ID,\n" +
                        "                TPPD.PARTY_PAYMENT_ID,\n" +
                        "                TPPD.PARTY_PAYMENT_DETAIL_ID,\n" +
                        "                TDD.DEDUCTION_DETAIL_ID,\n" +
                        "                TB.PARTY_LEDGER_ID AS LEDGER_ID,\n" +
                        "                TD.DEDUCTION_ID,\n" +
                        "                0 AS TEMP_AMOUNT,\n" +
                        "                1 AS SOURCE,\n" +
                        "                1 AS SORT_ID,\n" +
                        "                TB.AMOUNT AS ACTUAL_AMOUNT,\n" +
                        "                TD.AMOUNT AS 'PARTY_AMOUNT',\n" +
                        "                '' AS 'CHEQUE_NO',\n" +
                        "                '' AS 'MATERIALIZED_ON',\n" +
                        "                TT.AMOUNT AS 'PAID_AMOUNT',\n" +
                        "                'CR' AS TRANS_MODE,\n" +
                        "                CASE\n" +
                        "                  WHEN TP.PARTY_PAYMENT_ID > 0 OR TP.PARTY_PAYMENT_ID IS NULL THEN\n" +
                        "                   CASE\n" +
                        "                     WHEN TD.AMOUNT > TT.AMOUNT THEN\n" +
                        "                      TD.AMOUNT - TT.AMOUNT\n" +
                        "                     ELSE\n" +
                        "                      CASE\n" +
                        "                        WHEN TD.AMOUNT < TT.AMOUNT THEN\n" +
                        "                         - (TT.AMOUNT - TD.AMOUNT)\n" +
                        "                        ELSE\n" +
                        "                         CASE\n" +
                        "                           WHEN TD.AMOUNT = TT.AMOUNT THEN\n" +
                        "                            TD.AMOUNT - TT.AMOUNT\n" +
                        "                           ELSE\n" +
                        "                            TD.AMOUNT\n" +
                        "                         END\n" +
                        "                      END\n" +
                        "                   END\n" +
                        "                END AS 'AMOUNT',\n" +
                        "                CASE\n" +
                        "                  WHEN TP.PARTY_PAYMENT_ID > 0 OR TP.PARTY_PAYMENT_ID IS NULL THEN\n" +
                        "                   CASE\n" +
                        "                     WHEN TD.AMOUNT > TT.AMOUNT THEN\n" +
                        "                      TD.AMOUNT - TT.AMOUNT\n" +
                        "                     ELSE\n" +
                        "                      CASE\n" +
                        "                        WHEN TD.AMOUNT < TT.AMOUNT THEN\n" +
                        "                         - (TT.AMOUNT - TD.AMOUNT)\n" +
                        "                        ELSE\n" +
                        "                         CASE\n" +
                        "                           WHEN TD.AMOUNT = TT.AMOUNT THEN\n" +
                        "                            TD.AMOUNT - TT.AMOUNT\n" +
                        "                           ELSE\n" +
                        "                            TD.AMOUNT\n" +
                        "                         END\n" +
                        "                      END\n" +
                        "                   END\n" +
                        "                END AS 'REMAINING_BALANCE',\n" +
                        "                TB.IS_DELETED\n" +
                        "          FROM TDS_BOOKING AS TB\n" +
                        "          LEFT JOIN VOUCHER_MASTER_TRANS AS VMT\n" +
                        "            ON TB.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        "          LEFT JOIN TDS_BOOKING_DETAIL AS TBD\n" +
                        "            ON TB.BOOKING_ID = TBD.BOOKING_ID\n" +
                        "          LEFT JOIN TDS_DEDUCTION AS TD\n" +
                        "            ON TB.VOUCHER_ID = TD.VOUCHER_ID\n" +
                        "          LEFT JOIN TDS_DEDUCTION_DETAIL AS TDD\n" +
                        "            ON TD.DEDUCTION_ID = TDD.DEDUCTION_ID\n" +
                        "          LEFT JOIN TDS_PARTY_PAYMENT_DETAIL AS TPPD\n" +
                        "            ON TBD.BOOKING_DETAIL_ID = TPPD.BOOKING_DETAIL_ID\n" +
                        "           AND TDD.DEDUCTION_DETAIL_ID = TPPD.DEDUCTION_DETAIL_ID\n" +
                        "          LEFT JOIN TDS_PARTY_PAYMENT AS TP\n" +
                        "            ON TPPD.PARTY_PAYMENT_ID = TP.PARTY_PAYMENT_ID\n" +
                        "          LEFT JOIN (SELECT TPPD.DEDUCTION_DETAIL_ID,\n" +
                        "                            SUM(TPPD.PAID_AMOUNT) AS 'AMOUNT'\n" +
                        "                       FROM TDS_PARTY_PAYMENT AS TP\n" +
                        "                       LEFT JOIN TDS_PARTY_PAYMENT_DETAIL AS TPPD\n" +
                        "                         ON TP.PARTY_PAYMENT_ID = TPPD.PARTY_PAYMENT_ID\n" +
                        "                       LEFT JOIN TDS_DEDUCTION_DETAIL AS TDD\n" +
                        "                         ON TPPD.DEDUCTION_DETAIL_ID = TDD.DEDUCTION_DETAIL_ID\n" +
                        "                       LEFT JOIN TDS_DEDUCTION AS TD\n" +
                        "                         ON TDD.DEDUCTION_ID = TD.DEDUCTION_ID\n" +
                        "                       LEFT JOIN TDS_BOOKING_DETAIL AS TBD\n" +
                        "                         ON TDD.BOOKING_DETAIL_ID = TBD.BOOKING_DETAIL_ID\n" +
                        "                      WHERE TP.IS_DELETED = 1 GROUP BY TPPD.DEDUCTION_DETAIL_ID) AS TT\n" +
                        "            ON TDD.DEDUCTION_DETAIL_ID = TT.DEDUCTION_DETAIL_ID\n" +
                        "         WHERE TB.IS_DELETED = 1\n" +
                        "           AND TB.BOOKING_DATE <= ?BOOKING_DATE\n" +
                        "           AND TB.PROJECT_ID = ?PROJECT_ID\n" +
                        "           AND TB.PARTY_LEDGER_ID = ?LEDGER_ID\n" +
                        "         GROUP BY TB.BOOKING_ID) AS TT\n" +
                        " WHERE TT.AMOUNT > 0";

                        break;
                    }
                case SQLCommand.TDSPartyPayment.GetPartyPaymentId:
                    {
                        query = "SELECT GROUP_CONCAT(PARTY_PAYMENT_ID)\n" +
                                "  FROM TDS_PARTY_PAYMENT\n" +
                                " WHERE IS_DELETED = 1\n" +
                                "   AND PROJECT_ID =?PROJECT_ID\n" +
                                "   AND PARTY_LEDGER_ID =?PARTY_LEDGER_ID;";
                        break;
                    }
                case SQLCommand.TDSPartyPayment.FetchPartyPaymentId:
                    {
                        query = "SELECT PARTY_PAYMENT_ID ,PAYMENT_LEDGER_ID FROM TDS_PARTY_PAYMENT WHERE VOUCHER_ID=?VOUCHER_ID";
                        break;
                    }
            }

            return query;
        }
    }
}
