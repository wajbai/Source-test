using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.SQL;
using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class TDSDeductionSQL : IDatabaseQuery
    {
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        public string GetQuery(DAO.Data.DataCommandArguments dataCommandArgs, ref DAO.Data.SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.TDSDeduction).FullName)
            {
                query = GetDeduction();
            }

            sqlType = this.sqlType;
            return query;
        }

        #region SQL Script

        private string GetDeduction()
        {
            string query = "";
            SQLCommand.TDSDeduction sqlCommandId = (SQLCommand.TDSDeduction)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.TDSDeduction.Add:
                    {
                        query = "INSERT INTO TDS_DEDUCTION\n" +
                                "  (DEDUCTION_DATE, PROJECT_ID, PARTY_LEDGER_ID, AMOUNT, VOUCHER_ID)\n" +
                                "VALUES\n" +
                                "  (?DEDUCTION_DATE,\n" +
                                "   ?PROJECT_ID,\n" +
                                "   ?PARTY_LEDGER_ID,\n" +
                                "   ?AMOUNT,\n" +
                                "   ?VOUCHER_ID)";
                        break;
                    }

                case SQLCommand.TDSDeduction.Update:
                    {
                        query = "UPDATE TDS_DEDUCTION\n" +
                                "   SET DEDUCTION_DATE  = ?DEDUCTION_DATE,\n" +
                                "       PROJECT_ID      = ?PROJECT_ID,\n" +
                                "       PARTY_LEDGER_ID = ?PARTY_LEDGER_ID,\n" +
                                "       AMOUNT          = ?AMOUNT,\n" +
                                "       VOUCHER_ID      = ?VOUCHER_ID\n" +
                                " WHERE DEDUCTION_ID = ?DEDUCTION_ID";
                        break;
                    }
                case SQLCommand.TDSDeduction.Delete:
                    {
                        query = "DELETE FROM TDS_DEDUCTION WHERE VOUCHER_ID=?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.TDSDeduction.FetchByBooking:
                    {
                        query = "SELECT T.BOOKING_ID,\n" +
                        "       T.LEDGER_ID,\n" +
                        "       T.NATURE_PAY_ID,\n" +
                        "       T.BOOKING_DETAIL_ID,\n" +
                        "       T.BOOKING_DATE AS VOUCHER_DATE,\n" +
                        "       T.PROJECT_ID,\n" +
                        "       T.EXPENSE_LEDGER_ID,\n" +
                        "       T.PARTY_LEDGER_ID,\n" +
                        "       T.VOUCHER_ID,\n" +
                        "       T.DEDUCTEE_TYPE_ID,\n" +
                        "       T.AMOUNT,\n" +
                        "       T.VOUCHER_NO,\n" +
                        "       T.NARRATION,\n" +
                        "       T.NAME AS NATURE_PAYMENTS,\n" +
                        "       T.PAN_NUMBER,\n" +
                        "       T.ASSESS_AMOUNT,\n" +
                        "       T.VALUE,\n" +
                        "       T.RATE,\n" +
                        "       T.TDS_TAX_TYPE,\n" +
                        "       0 AS TDS_LEDGER_ID,\n" +
                        "       T.TAX,\n" +
                        "       T.IS_TDS_DEDUCTED,\n" +
                        "       CASE\n" +
                        "         WHEN T.IS_TDS_DEDUCTED = 1 THEN\n" +
                        "          CASE\n" +
                        "            WHEN T.PAN_NUMBER <> '' THEN\n" +
                        "             IF(T.ASSESS_AMOUNT >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                    '@',\n" +
                        "                                                                    1),\n" +
                        "                                                    '@',\n" +
                        "                                                    -1)),\n" +
                        "                CONCAT(CONCAT('1.TDS Tax - ',\n" +
                        "                              CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.ASSESS_AMOUNT,\n" +
                        "                                                                 2),\n" +
                        "                                                          'x '),\n" +
                        "                                                   CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(1 = 1,\n" +
                        "                                                                                              T.RATE,\n" +
                        "                                                                                              0.00),\n" +
                        "                                                                                           '@',\n" +
                        "                                                                                           1),\n" +
                        "                                                                           '@',\n" +
                        "                                                                           -1)),\n" +
                        "                                                          ' % ')),\n" +
                        "                                            ' = '),\n" +
                        "                                     ROUND(IF(1 = 1,\n" +
                        "                                              T.ASSESS_AMOUNT *\n" +
                        "                                              (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                               '@',\n" +
                        "                                                                               1),\n" +
                        "                                                               '@',\n" +
                        "                                                               -1)) / 100,\n" +
                        "                                              0),\n" +
                        "                                           2))),\n" +
                        "                       IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                    '@',\n" +
                        "                                                                    3),\n" +
                        "                                                    '@',\n" +
                        "                                                    -1)),\n" +
                        "                          '\\n',\n" +
                        "                          ''),\n" +
                        "                       IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                    '@',\n" +
                        "                                                                    3),\n" +
                        "                                                    '@',\n" +
                        "                                                    -1)),\n" +
                        "                          CONCAT('2.Surcharge - ',\n" +
                        "                                 CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.TAX, 2), 'x '),\n" +
                        "                                                      CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(3 = 3,\n" +
                        "                                                                                                 T.RATE,\n" +
                        "                                                                                                 0.00),\n" +
                        "                                                                                              '@',\n" +
                        "                                                                                              3),\n" +
                        "                                                                              '@',\n" +
                        "                                                                              -1)),\n" +
                        "                                                             ' % ')),\n" +
                        "                                               ' = '),\n" +
                        "                                        ROUND(IF(3 = 3,\n" +
                        "                                                 T.TAX *\n" +
                        "                                                 (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  3),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)) / 100,\n" +
                        "                                                 0),\n" +
                        "                                              2))),\n" +
                        "                          ''),\n" +
                        "                       IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                    '@',\n" +
                        "                                                                    4),\n" +
                        "                                                    '@',\n" +
                        "                                                    -1)),\n" +
                        "                          '\\n',\n" +
                        "                          ''),\n" +
                        "                       IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                    '@',\n" +
                        "                                                                    4),\n" +
                        "                                                    '@',\n" +
                        "                                                    -1)),\n" +
                        "                          CONCAT('3.Ed Cess - ',\n" +
                        "                                 CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.TAX, 2), 'x '),\n" +
                        "                                                      CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(4 = 4,\n" +
                        "                                                                                                 T.RATE,\n" +
                        "                                                                                                 0.00),\n" +
                        "                                                                                              '@',\n" +
                        "                                                                                              4),\n" +
                        "                                                                              '@',\n" +
                        "                                                                              -1)),\n" +
                        "                                                             ' % ')),\n" +
                        "                                               ' = '),\n" +
                        "                                        ROUND(IF(3 = 3,\n" +
                        "                                                 T.TAX *\n" +
                        "                                                 (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  4),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)) / 100,\n" +
                        "                                                 0),\n" +
                        "                                              2))),\n" +
                        "                          ''),\n" +
                        "                       IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                    '@',\n" +
                        "                                                                    5),\n" +
                        "                                                    '@',\n" +
                        "                                                    -1)),\n" +
                        "                          '\\n',\n" +
                        "                          ''),\n" +
                        "                       IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                    '@',\n" +
                        "                                                                    5),\n" +
                        "                                                    '@',\n" +
                        "                                                    -1)),\n" +
                        "                          CONCAT('4.Sec Ed Cess - ',\n" +
                        "                                 CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.TAX, 2), 'x '),\n" +
                        "                                                      CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(5 = 5,\n" +
                        "                                                                                                 T.RATE,\n" +
                        "                                                                                                 0.00),\n" +
                        "                                                                                              '@',\n" +
                        "                                                                                              5),\n" +
                        "                                                                              '@',\n" +
                        "                                                                              -1)),\n" +
                        "                                                             ' % ')),\n" +
                        "                                               ' = '),\n" +
                        "                                        ROUND(IF(5 = 5,\n" +
                        "                                                 T.TAX *\n" +
                        "                                                 (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  5),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)) / 100,\n" +
                        "                                                 0),\n" +
                        "                                              2))),\n" +
                        "                          ''),\n" +
                        "                       '\\n',\n" +
                        "                       CONCAT(' '),\n" +
                        "                       '\\n',\n" +
                        "                       CONCAT('Payable to TDS =',\n" +
                        "                              ROUND(IF(1 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                 '@',\n" +
                        "                                                                                 1),\n" +
                        "                                                                 '@',\n" +
                        "                                                                 -1)),\n" +
                        "                                       T.ASSESS_AMOUNT *\n" +
                        "                                       (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                        '@',\n" +
                        "                                                                        1),\n" +
                        "                                                        '@',\n" +
                        "                                                        -1)) / 100,\n" +
                        "                                       0),\n" +
                        "                                    2) +\n" +
                        "                              ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                 '@',\n" +
                        "                                                                                 3),\n" +
                        "                                                                 '@',\n" +
                        "                                                                 -1)),\n" +
                        "                                       T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                '@',\n" +
                        "                                                                                3),\n" +
                        "                                                                '@',\n" +
                        "                                                                -1)) / 100,\n" +
                        "                                       0),\n" +
                        "                                    2) +\n" +
                        "                              ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                 '@',\n" +
                        "                                                                                 4),\n" +
                        "                                                                 '@',\n" +
                        "                                                                 -1)),\n" +
                        "                                       T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                '@',\n" +
                        "                                                                                4),\n" +
                        "                                                                '@',\n" +
                        "                                                                -1)) / 100,\n" +
                        "                                       0),\n" +
                        "                                    2) +\n" +
                        "                              ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                 '@',\n" +
                        "                                                                                 4),\n" +
                        "                                                                 '@',\n" +
                        "                                                                 -1)),\n" +
                        "                                       T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                '@',\n" +
                        "                                                                                5),\n" +
                        "                                                                '@',\n" +
                        "                                                                -1)) / 100,\n" +
                        "                                       0),\n" +
                        "                                    2)),\n" +
                        "                       '\\n',\n" +
                        "                       CONCAT('Payable to Party = ',\n" +
                        "                              ROUND(T.ASSESS_AMOUNT -\n" +
                        "                                    (ROUND(IF(1 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                        '@',\n" +
                        "                                                                                        1),\n" +
                        "                                                                        '@',\n" +
                        "                                                                        -1)),\n" +
                        "                                              T.ASSESS_AMOUNT *\n" +
                        "                                              (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                               '@',\n" +
                        "                                                                               1),\n" +
                        "                                                               '@',\n" +
                        "                                                               -1)) / 100,\n" +
                        "                                              0),\n" +
                        "                                           2) +\n" +
                        "                                    ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                        '@',\n" +
                        "                                                                                        3),\n" +
                        "                                                                        '@',\n" +
                        "                                                                        -1)),\n" +
                        "                                              T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                       '@',\n" +
                        "                                                                                       3),\n" +
                        "                                                                       '@',\n" +
                        "                                                                       -1)) / 100,\n" +
                        "                                              0),\n" +
                        "                                           2) +\n" +
                        "                                    ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                        '@',\n" +
                        "                                                                                        4),\n" +
                        "                                                                        '@',\n" +
                        "                                                                        -1)),\n" +
                        "                                              T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                       '@',\n" +
                        "                                                                                       4),\n" +
                        "                                                                       '@',\n" +
                        "                                                                       -1)) / 100,\n" +
                        "                                              0),\n" +
                        "                                           2) +\n" +
                        "                                    ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                        '@',\n" +
                        "                                                                                        5),\n" +
                        "                                                                        '@',\n" +
                        "                                                                        -1)),\n" +
                        "                                              T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                       '@',\n" +
                        "                                                                                       5),\n" +
                        "                                                                       '@',\n" +
                        "                                                                       -1)) / 100,\n" +
                        "                                              0),\n" +
                        "                                           2)),\n" +
                        "                                    2))),\n" +
                        "                '')\n" +
                        "            ELSE\n" +
                        "\n" +
                        "             IF(T.ASSESS_AMOUNT >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                    '@',\n" +
                        "                                                                    2),\n" +
                        "                                                    '@',\n" +
                        "                                                    -1)),\n" +
                        "                CONCAT(CONCAT('1.TDS Tax - ',\n" +
                        "                              CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.ASSESS_AMOUNT,\n" +
                        "                                                                 2),\n" +
                        "                                                          ' x '),\n" +
                        "                                                   CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(2 =\n" +
                        "                                                                                              ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                                                                    '@',\n" +
                        "                                                                                                                                    2),\n" +
                        "                                                                                                                    '@',\n" +
                        "                                                                                                                    -1)),\n" +
                        "                                                                                              T.RATE,\n" +
                        "                                                                                              0.00),\n" +
                        "                                                                                           '@',\n" +
                        "                                                                                           2),\n" +
                        "                                                                           '@',\n" +
                        "                                                                           -1)),\n" +
                        "                                                          '% ')),\n" +
                        "                                            ' = '),\n" +
                        "                                     ROUND(IF(2 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                        '@',\n" +
                        "                                                                                        2),\n" +
                        "                                                                        '@',\n" +
                        "                                                                        -1)),\n" +
                        "                                              T.ASSESS_AMOUNT *\n" +
                        "                                              (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                               '@',\n" +
                        "                                                                               2),\n" +
                        "                                                               '@',\n" +
                        "                                                               -1)) / 100,\n" +
                        "                                              2)))),\n" +
                        "                       IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                    '@',\n" +
                        "                                                                    3),\n" +
                        "                                                    '@',\n" +
                        "                                                    -1)),\n" +
                        "                          '\\n',\n" +
                        "                          ''),\n" +
                        "                       IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                    '@',\n" +
                        "                                                                    3),\n" +
                        "                                                    '@',\n" +
                        "                                                    -1)),\n" +
                        "                          CONCAT('2.Surcharge - ',\n" +
                        "                                 CONCAT(CONCAT(CONCAT(CONCAT(T.TAX, ' x '),\n" +
                        "                                                      CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(3 = 3,\n" +
                        "                                                                                                 T.RATE,\n" +
                        "                                                                                                 0.00),\n" +
                        "                                                                                              '@',\n" +
                        "                                                                                              3),\n" +
                        "                                                                              '@',\n" +
                        "                                                                              -1)),\n" +
                        "                                                             '% ')),\n" +
                        "                                               ' = '),\n" +
                        "                                        ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                           '@',\n" +
                        "                                                                                           3),\n" +
                        "                                                                           '@',\n" +
                        "                                                                           -1)),\n" +
                        "                                                 T.TAX *\n" +
                        "                                                 (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  3),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)) / 100,\n" +
                        "                                                 2)))),\n" +
                        "                          ''),\n" +
                        "                       IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                    '@',\n" +
                        "                                                                    4),\n" +
                        "                                                    '@',\n" +
                        "                                                    -1)),\n" +
                        "                          '\\n',\n" +
                        "                          ''),\n" +
                        "                       IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                    '@',\n" +
                        "                                                                    4),\n" +
                        "                                                    '@',\n" +
                        "                                                    -1)),\n" +
                        "                          CONCAT('3.Ed Cess - ',\n" +
                        "                                 CONCAT(CONCAT(CONCAT(CONCAT(T.TAX, ' x '),\n" +
                        "                                                      CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(4 = 4,\n" +
                        "                                                                                                 T.RATE,\n" +
                        "                                                                                                 0.00),\n" +
                        "                                                                                              '@',\n" +
                        "                                                                                              4),\n" +
                        "                                                                              '@',\n" +
                        "                                                                              -1)),\n" +
                        "                                                             '% ')),\n" +
                        "                                               ' = '),\n" +
                        "                                        ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                           '@',\n" +
                        "                                                                                           4),\n" +
                        "                                                                           '@',\n" +
                        "                                                                           -1)),\n" +
                        "                                                 T.TAX *\n" +
                        "                                                 (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  4),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)) / 100,\n" +
                        "                                                 2)))),\n" +
                        "                          ''),\n" +
                        "                       IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                    '@',\n" +
                        "                                                                    5),\n" +
                        "                                                    '@',\n" +
                        "                                                    -1)),\n" +
                        "                          '\\n',\n" +
                        "                          ''),\n" +
                        "                       IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                    '@',\n" +
                        "                                                                    5),\n" +
                        "                                                    '@',\n" +
                        "                                                    -1)),\n" +
                        "                          CONCAT('4.Sec Ed Cess - ',\n" +
                        "                                 CONCAT(CONCAT(CONCAT(CONCAT(T.TAX, ' x '),\n" +
                        "                                                      CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(5 =\n" +
                        "                                                                                                 ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                                                                       '@',\n" +
                        "                                                                                                                                       5),\n" +
                        "                                                                                                                       '@',\n" +
                        "                                                                                                                       -1)),\n" +
                        "                                                                                                 T.RATE,\n" +
                        "                                                                                                 0.00),\n" +
                        "                                                                                              '@',\n" +
                        "                                                                                              5),\n" +
                        "                                                                              '@',\n" +
                        "                                                                              -1)),\n" +
                        "                                                             '% ')),\n" +
                        "                                               ' = '),\n" +
                        "                                        ROUND(IF(5 = 5,\n" +
                        "                                                 T.TAX *\n" +
                        "                                                 (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  5),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)) / 100,\n" +
                        "                                                 2)))),\n" +
                        "                          ''),\n" +
                        "                       '\\n',\n" +
                        "                       CONCAT(''),\n" +
                        "                       '\\n',\n" +
                        "\n" +
                        "                       CONCAT('Payable to TDS =',\n" +
                        "                              ROUND(IF(2 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                 '@',\n" +
                        "                                                                                 2),\n" +
                        "                                                                 '@',\n" +
                        "                                                                 -1)),\n" +
                        "                                       T.ASSESS_AMOUNT *\n" +
                        "                                       (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                        '@',\n" +
                        "                                                                        2),\n" +
                        "                                                        '@',\n" +
                        "                                                        -1)) / 100,\n" +
                        "                                       0),\n" +
                        "                                    2) +\n" +
                        "                              ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                 '@',\n" +
                        "                                                                                 3),\n" +
                        "                                                                 '@',\n" +
                        "                                                                 -1)),\n" +
                        "                                       T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                '@',\n" +
                        "                                                                                3),\n" +
                        "                                                                '@',\n" +
                        "                                                                -1)) / 100,\n" +
                        "                                       0),\n" +
                        "                                    2) +\n" +
                        "                              ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                 '@',\n" +
                        "                                                                                 4),\n" +
                        "                                                                 '@',\n" +
                        "                                                                 -1)),\n" +
                        "                                       T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                '@',\n" +
                        "                                                                                4),\n" +
                        "                                                                '@',\n" +
                        "                                                                -1)) / 100,\n" +
                        "                                       0),\n" +
                        "                                    2) +\n" +
                        "                              ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                 '@',\n" +
                        "                                                                                 5),\n" +
                        "                                                                 '@',\n" +
                        "                                                                 -1)),\n" +
                        "                                       T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                '@',\n" +
                        "                                                                                5),\n" +
                        "                                                                '@',\n" +
                        "                                                                -1)) / 100,\n" +
                        "                                       0),\n" +
                        "                                    2)),\n" +
                        "                       '\\n',\n" +
                        "                       CONCAT('Payable to Party = ',\n" +
                        "                              ROUND(T.ASSESS_AMOUNT -\n" +
                        "                                    (ROUND(IF(2 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                        '@',\n" +
                        "                                                                                        2),\n" +
                        "                                                                        '@',\n" +
                        "                                                                        -1)),\n" +
                        "                                              T.ASSESS_AMOUNT *\n" +
                        "                                              (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                               '@',\n" +
                        "                                                                               2),\n" +
                        "                                                               '@',\n" +
                        "                                                               -1)) / 100,\n" +
                        "                                              0),\n" +
                        "                                           2) +\n" +
                        "                                    ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                        '@',\n" +
                        "                                                                                        3),\n" +
                        "                                                                        '@',\n" +
                        "                                                                        -1)),\n" +
                        "                                              T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                       '@',\n" +
                        "                                                                                       3),\n" +
                        "                                                                       '@',\n" +
                        "                                                                       -1)) / 100,\n" +
                        "                                              0),\n" +
                        "                                           2) +\n" +
                        "                                    ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                        '@',\n" +
                        "                                                                                        4),\n" +
                        "                                                                        '@',\n" +
                        "                                                                        -1)),\n" +
                        "                                              T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                       '@',\n" +
                        "                                                                                       4),\n" +
                        "                                                                       '@',\n" +
                        "                                                                       -1)) / 100,\n" +
                        "                                              0),\n" +
                        "                                           2) +\n" +
                        "                                    ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                        '@',\n" +
                        "                                                                                        5),\n" +
                        "                                                                        '@',\n" +
                        "                                                                        -1)),\n" +
                        "                                              T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                       '@',\n" +
                        "                                                                                       5),\n" +
                        "                                                                       '@',\n" +
                        "                                                                       -1)) / 100,\n" +
                        "                                              0),\n" +
                        "                                           2)),\n" +
                        "                                    2))),\n" +
                        "                '')\n" +
                        "          END\n" +
                        "       END AS 'TAX_AMOUNT',\n" +
                        "       CASE\n" +
                        "         WHEN T.IS_TDS_DEDUCTED = 1 THEN\n" +
                        "          CASE\n" +
                        "            WHEN T.PAN_NUMBER <> '' THEN\n" +
                        "             IF(T.ASSESS_AMOUNT >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                    '@',\n" +
                        "                                                                    1),\n" +
                        "                                                    '@',\n" +
                        "                                                    -1)),\n" +
                        "                ROUND(IF(1 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                   '@',\n" +
                        "                                                                   1),\n" +
                        "                                                   '@',\n" +
                        "                                                   -1)),\n" +
                        "                         T.ASSESS_AMOUNT *\n" +
                        "                         (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE, '@', 1),\n" +
                        "                                          '@',\n" +
                        "                                          -1)) / 100,\n" +
                        "                         0),\n" +
                        "                      2) +\n" +
                        "                ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                   '@',\n" +
                        "                                                                   3),\n" +
                        "                                                   '@',\n" +
                        "                                                   -1)),\n" +
                        "                         T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE, '@', 3),\n" +
                        "                                                  '@',\n" +
                        "                                                  -1)) / 100,\n" +
                        "                         0),\n" +
                        "                      2) +\n" +
                        "                ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                   '@',\n" +
                        "                                                                   4),\n" +
                        "                                                   '@',\n" +
                        "                                                   -1)),\n" +
                        "                         T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE, '@', 4),\n" +
                        "                                                  '@',\n" +
                        "                                                  -1)) / 100,\n" +
                        "                         0),\n" +
                        "                      2) +\n" +
                        "                ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                   '@',\n" +
                        "                                                                   4),\n" +
                        "                                                   '@',\n" +
                        "                                                   -1)),\n" +
                        "                         T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE, '@', 5),\n" +
                        "                                                  '@',\n" +
                        "                                                  -1)) / 100,\n" +
                        "                         0),\n" +
                        "                      2),\n" +
                        "                0)\n" +
                        "            ELSE\n" +
                        "             IF(T.ASSESS_AMOUNT >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                    '@',\n" +
                        "                                                                    1),\n" +
                        "                                                    '@',\n" +
                        "                                                    -1)),\n" +
                        "                ROUND(IF(2 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                   '@',\n" +
                        "                                                                   2),\n" +
                        "                                                   '@',\n" +
                        "                                                   -1)),\n" +
                        "                         T.ASSESS_AMOUNT *\n" +
                        "                         (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE, '@', 2),\n" +
                        "                                          '@',\n" +
                        "                                          -1)) / 100,\n" +
                        "                         0),\n" +
                        "                      2) +\n" +
                        "                ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                   '@',\n" +
                        "                                                                   3),\n" +
                        "                                                   '@',\n" +
                        "                                                   -1)),\n" +
                        "                         T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE, '@', 3),\n" +
                        "                                                  '@',\n" +
                        "                                                  -1)) / 100,\n" +
                        "                         0),\n" +
                        "                      2) +\n" +
                        "                ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                   '@',\n" +
                        "                                                                   4),\n" +
                        "                                                   '@',\n" +
                        "                                                   -1)),\n" +
                        "                         T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE, '@', 4),\n" +
                        "                                                  '@',\n" +
                        "                                                  -1)) / 100,\n" +
                        "                         0),\n" +
                        "                      2) +\n" +
                        "                ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                   '@',\n" +
                        "                                                                   4),\n" +
                        "                                                   '@',\n" +
                        "                                                   -1)),\n" +
                        "                         T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE, '@', 5),\n" +
                        "                                                  '@',\n" +
                        "                                                  -1)) / 100,\n" +
                        "                         0),\n" +
                        "                      2),\n" +
                        "                0)\n" +
                        "          END\n" +
                        "       END AS BALANCE,\n" +
                        "       T.DEDUCT_NOW AS 'Id',\n" +
                        "       T.LEDGER_NAME,\n" +
                        "       T.TDS_TAX_TYPE_ID,\n" +
                        "       T.TDS_RATE,\n" +
                        "       T.TDS_EXEMPTION_LIMIT,\n" +
                        "       T.TDS_LIMITATION_AMOUNT,\n" +
                        "       T.APPLICABLE_FROM\n" +
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
                        "               TNP.NAME,\n" +
                        "               TTR.TDS_TAX_TYPE_ID,\n" +
                        "               TBD.IS_TDS_DEDUCTED,\n" +
                        "               TCP.PAN_NUMBER,\n" +
                        "               TTR.TDS_RATE,\n" +
                        "               TB.AMOUNT,\n" +
                        "               0 AS VALUE,\n" +
                        "               TTR.TDS_EXEMPTION_LIMIT,\n" +
                        "               TP.APPLICABLE_FROM,\n" +
                        "               ROUND(TBD.ASSESS_AMOUNT, 2) AS ASSESS_AMOUNT,\n" +
                        "               GROUP_CONCAT(TTR.TDS_RATE ORDER BY TTR.TDS_TAX_TYPE_ID\n" +
                        "                            SEPARATOR '@') AS Rate,\n" +
                        "               GROUP_CONCAT(TTR.TDS_TAX_TYPE_ID ORDER BY TTR.TDS_TAX_TYPE_ID\n" +
                        "                            SEPARATOR '@') AS TDS_TAX_TYPE,\n" +
                        "               GROUP_CONCAT(TTR.TDS_EXEMPTION_LIMIT ORDER BY\n" +
                        "                            TTR.TDS_TAX_TYPE_ID SEPARATOR '@') AS TDS_LIMITATION_AMOUNT,\n" +
                        "               ROUND((ASSESS_AMOUNT *\n" +
                        "                     SUBSTRING_INDEX(GROUP_CONCAT(TTR.TDS_RATE ORDER BY\n" +
                        "                                                   TTR.TDS_TAX_TYPE_ID\n" +
                        "                                                   SEPARATOR '@'),\n" +
                        "                                      '@',\n" +
                        "                                      1)) / 100,\n" +
                        "                     2) as Tax,\n" +
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
                        "               ML.LEDGER_NAME\n" +
                        "          FROM TDS_BOOKING AS TB\n" +
                        "          LEFT JOIN TDS_BOOKING_DETAIL AS TBD\n" +
                        "            ON TB.BOOKING_ID = TBD.BOOKING_ID\n" +
                        "          LEFT JOIN VOUCHER_MASTER_TRANS AS VMT\n" +
                        "            ON TB.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        "          LEFT JOIN MASTER_LEDGER AS ML\n" +
                        "            ON TB.PARTY_LEDGER_ID = ML.LEDGER_ID\n" +
                        "          LEFT JOIN TDS_CREDTIORS_PROFILE AS TCP\n" +
                        "            ON ML.LEDGER_ID = TCP.LEDGER_ID\n" +
                        "          LEFT JOIN TDS_NATURE_PAYMENT AS TNP\n" +
                        "            ON TBD.NATURE_OF_PAYMENT_ID = TNP.NATURE_PAY_ID\n" +
                        "          LEFT JOIN TDS_POLICY AS TP\n" +
                        "            ON TBD.NATURE_OF_PAYMENT_ID = TP.TDS_NATURE_PAYMENT_ID\n" +
                        "          LEFT JOIN TDS_TAX_RATE AS TTR\n" +
                        "            ON TP.TDS_POLICY_ID = TTR.TDS_POLICY_ID\n" +
                        "          LEFT JOIN TDS_DUTY_TAXTYPE AS TDT\n" +
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
                        "           AND TB.PROJECT_ID IN (?PROJECT_ID)\n" +
                        "         GROUP BY TBD.BOOKING_DETAIL_ID) AS T";
                        break;
                    }
                case SQLCommand.TDSDeduction.FetchAll:
                    {
                        query = "SELECT DEDUCTION_ID,\n" +
                                "       DEDUCTION_DATE,\n" +
                                "       PROJECT_ID,\n" +
                                "       PARTY_LEDGER_ID,\n" +
                                "       AMOUNT,\n" +
                                "       VOUCHER_ID\n" +
                                "  FROM TDS_DEDUCTION\n" +
                                " ORDER BY DEDUCTION_DATE ASC";
                        break;
                    }
                case SQLCommand.TDSDeduction.Fetch:
                    {
                        query = "SELECT DEDUCTION_ID,\n" +
                                "       DEDUCTION_DATE,\n" +
                                "       PROJECT_ID,\n" +
                                "       PARTY_LEDGER_ID,\n" +
                                "       AMOUNT,\n" +
                                "       VOUCHER_ID\n" +
                                "  FROM TDS_DEDUCTION\n" +
                                " WHERE DEDUCTION_ID = ?DEDUCTION_ID";
                        break;
                    }
                case SQLCommand.TDSDeduction.FetchPendingTransaction:
                    {
                        //query = "SELECT T.BOOKING_ID,\n" +
                        //"       T.LEDGER_ID,\n" +
                        //"       T.NATURE_PAY_ID,\n" +
                        //"       T.BOOKING_DETAIL_ID,\n" +
                        //"       T.BOOKING_DATE AS VOUCHER_DATE,\n" +
                        //"       T.PROJECT_ID,\n" +
                        //"       T.EXPENSE_LEDGER_ID,\n" +
                        //"       T.PARTY_LEDGER_ID,\n" +
                        //"       T.VOUCHER_ID,\n" +
                        //"       T.DEDUCTEE_TYPE_ID,\n" +
                        //"       T.AMOUNT,\n" +
                        //"       T.VOUCHER_NO,\n" +
                        //"       T.NARRATION,\n" +
                        //"       T.NAME AS NATURE_PAYMENTS,\n" +
                        //"       T.PAN_NUMBER,\n" +
                        //"       T.ASSESS_AMOUNT,\n" +
                        //"       1 AS VALUE,\n" +
                        //"       T.RATE,\n" +
                        //"       T.TDS_TAX_TYPE,\n" +
                        //"       0 AS TDS_LEDGER_ID,\n" +
                        //"       T.TAX,\n" +
                        //"       T.IS_TDS_DEDUCTED,\n" +
                        //"       CASE\n" +
                        //"         WHEN T.IS_TDS_DEDUCTED = 0 THEN\n" +
                        //"          CASE\n" +
                        //"            WHEN T.PAN_NUMBER <> '' THEN\n" +
                        //"\n" +
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
                        //"                       '\n',\n" +
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
                        //"                       '\n',\n" +
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
                        //"                       '\n',\n" +
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
                        //"                    '\n',\n" +
                        //"                    CONCAT(' '),\n" +
                        //"                    '\n',\n" +
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
                        //"                                                                              4),\n" +
                        //"                                                              '@',\n" +
                        //"                                                              -1)),\n" +
                        //"                                    T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                             '@',\n" +
                        //"                                                                             5),\n" +
                        //"                                                             '@',\n" +
                        //"                                                             -1)) / 100,\n" +
                        //"                                    0),\n" +
                        //"                                 2)),\n" +
                        //"                    '\n',\n" +
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
                        //"\n" +
                        //"            ELSE\n" +
                        //"\n" +
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
                        //"                       '\n',\n" +
                        //"                       ''),\n" +
                        //"                    IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                 '@',\n" +
                        //"                                                                 3),\n" +
                        //"                                                 '@',\n" +
                        //"                                                 -1)),\n" +
                        //"                       CONCAT('2.Surcharge - ',\n" +
                        //"                              CONCAT(CONCAT(CONCAT(CONCAT(T.TAX, ' x '),\n" +
                        //"                                                   CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(3 = 3,\n" +
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
                        //"                       '\n',\n" +
                        //"                       ''),\n" +
                        //"                    IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                 '@',\n" +
                        //"                                                                 4),\n" +
                        //"                                                 '@',\n" +
                        //"                                                 -1)),\n" +
                        //"                       CONCAT('3.Ed Cess - ',\n" +
                        //"                              CONCAT(CONCAT(CONCAT(CONCAT(T.TAX, ' x '),\n" +
                        //"                                                   CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(4 = 4,\n" +
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
                        //"                       '\n',\n" +
                        //"                       ''),\n" +
                        //"                    IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                 '@',\n" +
                        //"                                                                 5),\n" +
                        //"                                                 '@',\n" +
                        //"                                                 -1)),\n" +
                        //"                       CONCAT('4.Sec Ed Cess - ',\n" +
                        //"                              CONCAT(CONCAT(CONCAT(CONCAT(T.TAX, ' x '),\n" +
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
                        //"                                     ROUND(IF(5 = 5,\n" +
                        //"                                              T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        //"                                                                                       '@',\n" +
                        //"                                                                                       5),\n" +
                        //"                                                                       '@',\n" +
                        //"                                                                       -1)) / 100,\n" +
                        //"                                              2)))),\n" +
                        //"                       ''),\n" +
                        //"                    '\n',\n" +
                        //"                    CONCAT(''),\n" +
                        //"                    '\n',\n" +
                        //"\n" +
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
                        //"                    '\n',\n" +
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
                        //"\n" +
                        //"          END\n" +
                        //"       END AS 'TAX_AMOUNT',\n" +
                        //"       CASE\n" +
                        //"         WHEN T.IS_TDS_DEDUCTED = 0 THEN\n" +
                        //"          CASE\n" +
                        //"            WHEN T.PAN_NUMBER <> '' THEN\n" +
                        //"                ROUND(IF(1 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                   '@',\n" +
                        //"                                                                   1),\n" +
                        //"                                                   '@',\n" +
                        //"                                                   -1)),\n" +
                        //"                         T.ASSESS_AMOUNT *\n" +
                        //"                         (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE, '@', 1),\n" +
                        //"                                          '@',\n" +
                        //"                                          -1)) / 100,\n" +
                        //"                         0),\n" +
                        //"                      2) +\n" +
                        //"                ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                   '@',\n" +
                        //"                                                                   3),\n" +
                        //"                                                   '@',\n" +
                        //"                                                   -1)),\n" +
                        //"                         T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE, '@', 3),\n" +
                        //"                                                  '@',\n" +
                        //"                                                  -1)) / 100,\n" +
                        //"                         0),\n" +
                        //"                      2) +\n" +
                        //"                ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                   '@',\n" +
                        //"                                                                   4),\n" +
                        //"                                                   '@',\n" +
                        //"                                                   -1)),\n" +
                        //"                         T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE, '@', 4),\n" +
                        //"                                                  '@',\n" +
                        //"                                                  -1)) / 100,\n" +
                        //"                         0),\n" +
                        //"                      2) +\n" +
                        //"                ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                   '@',\n" +
                        //"                                                                   4),\n" +
                        //"                                                   '@',\n" +
                        //"                                                   -1)),\n" +
                        //"                         T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE, '@', 5),\n" +
                        //"                                                  '@',\n" +
                        //"                                                  -1)) / 100,\n" +
                        //"                         0),\n" +
                        //"                      2)\n" +
                        //"            ELSE\n" +
                        //"\n" +
                        //"             ROUND(IF(2 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                '@',\n" +
                        //"                                                                2),\n" +
                        //"                                                '@',\n" +
                        //"                                                -1)),\n" +
                        //"                      T.ASSESS_AMOUNT *\n" +
                        //"                      (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE, '@', 2), '@', -1)) / 100,\n" +
                        //"                      0),\n" +
                        //"                   2) +\n" +
                        //"             ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                '@',\n" +
                        //"                                                                3),\n" +
                        //"                                                '@',\n" +
                        //"                                                -1)),\n" +
                        //"                      T.TAX *\n" +
                        //"                      (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE, '@', 3), '@', -1)) / 100,\n" +
                        //"                      0),\n" +
                        //"                   2) +\n" +
                        //"             ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                '@',\n" +
                        //"                                                                4),\n" +
                        //"                                                '@',\n" +
                        //"                                                -1)),\n" +
                        //"                      T.TAX *\n" +
                        //"                      (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE, '@', 4), '@', -1)) / 100,\n" +
                        //"                      0),\n" +
                        //"                   2) +\n" +
                        //"             ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        //"                                                                '@',\n" +
                        //"                                                                4),\n" +
                        //"                                                '@',\n" +
                        //"                                                -1)),\n" +
                        //"                      T.TAX *\n" +
                        //"                      (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE, '@', 5), '@', -1)) / 100,\n" +
                        //"                      0),\n" +
                        //"                   2)\n" +
                        //"          END\n" +
                        //"       END AS BALANCE,\n" +
                        //"       T.DEDUCT_NOW AS 'Id',\n" +
                        //"       T.LEDGER_NAME,\n" +
                        //"       T.TDS_TAX_TYPE_ID,\n" +
                        //"       T.TDS_RATE,\n" +
                        //"       T.TDS_EXEMPTION_LIMIT,\n" +
                        //"       T.TDS_LIMITATION_AMOUNT,\n" +
                        //"       T.APPLICABLE_FROM\n" +
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
                        //"               TNP.NAME,\n" +
                        //"               TTR.TDS_TAX_TYPE_ID,\n" +
                        //"               TBD.IS_TDS_DEDUCTED,\n" +
                        //"               TCP.PAN_NUMBER,\n" +
                        //"               TTR.TDS_RATE,\n" +
                        //"               TB.AMOUNT,\n" +
                        //"               TTR.TDS_EXEMPTION_LIMIT,\n" +
                        //"               TP.APPLICABLE_FROM,\n" +
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
                        //"               ML.LEDGER_NAME\n" +
                        //"          FROM TDS_BOOKING AS TB\n" +
                        //"          LEFT JOIN TDS_BOOKING_DETAIL AS TBD\n" +
                        //"            ON TB.BOOKING_ID = TBD.BOOKING_ID\n" +
                        //"          LEFT JOIN VOUCHER_MASTER_TRANS AS VMT\n" +
                        //"            ON TB.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        //"          LEFT JOIN MASTER_LEDGER AS ML\n" +
                        //"            ON TB.PARTY_LEDGER_ID = ML.LEDGER_ID\n" +
                        //"          LEFT JOIN TDS_CREDTIORS_PROFILE AS TCP\n" +
                        //"            ON ML.LEDGER_ID = TCP.LEDGER_ID\n" +
                        //"          LEFT JOIN TDS_NATURE_PAYMENT AS TNP\n" +
                        //"            ON TBD.NATURE_OF_PAYMENT_ID = TNP.NATURE_PAY_ID\n" +
                        //"          LEFT JOIN TDS_POLICY AS TP\n" +
                        //"            ON TBD.NATURE_OF_PAYMENT_ID = TP.TDS_NATURE_PAYMENT_ID\n" +
                        //"          LEFT JOIN TDS_TAX_RATE AS TTR\n" +
                        //"            ON TP.TDS_POLICY_ID = TTR.TDS_POLICY_ID\n" +
                        //"          LEFT JOIN TDS_DUTY_TAXTYPE AS TDT\n" +
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
                        //"                       AND TBD1.IS_TDS_DEDUCTED = 0\n" +
                        //"                     GROUP BY TP.TDS_NATURE_PAYMENT_ID) AS TTD\n" +
                        //"            ON TBD.NATURE_OF_PAYMENT_ID = TTD.TDS_NATURE_PAYMENT_ID\n" +
                        //"         WHERE TB.PARTY_LEDGER_ID = ?LEDGER_ID\n" +
                        //"           AND TP.TDS_DEDUCTEE_TYPE_ID = ?DEDUCTEE_TYPE_ID\n" +
                        //"           AND TP.APPLICABLE_FROM = TTD.APPLICABLE_FROM\n" +
                        //"           AND TB.IS_DELETED = 1\n" +
                        //"           AND TDT.STATUS = 1\n" +
                        //"           AND TB.PROJECT_ID IN (?PROJECT_ID)\n" +
                        //"           AND TBD.NATURE_OF_PAYMENT_ID = ?NATURE_OF_PAYMENT_ID\n" +
                        //"           AND TBD.IS_TDS_DEDUCTED = 0\n" +
                        //"         GROUP BY TBD.BOOKING_DETAIL_ID) AS T";

                        query = "SELECT T.BOOKING_ID,\n" +
                        "       T.LEDGER_ID,\n" +
                        "       T.NATURE_PAY_ID,\n" +
                        "       T.BOOKING_DETAIL_ID,\n" +
                        "       T.BOOKING_DATE AS VOUCHER_DATE,\n" +
                        "       T.PROJECT_ID,\n" +
                        "       T.EXPENSE_LEDGER_ID,\n" +
                        "       T.PARTY_LEDGER_ID,\n" +
                        "       T.VOUCHER_ID,\n" +
                        "       T.DEDUCTEE_TYPE_ID,\n" +
                        "       T.AMOUNT,\n" +
                        "       T.VOUCHER_NO,\n" +
                        "       T.NARRATION,\n" +
                        "       T.NAME AS NATURE_PAYMENTS,\n" +
                        "       T.PAN_NUMBER,\n" +
                        "       T.ASSESS_AMOUNT,\n" +
                        "       1 AS VALUE,\n" +
                        "       T.RATE,\n" +
                        "       T.TDS_TAX_TYPE,\n" +
                        "       0 AS TDS_LEDGER_ID,\n" +
                        "       T.TAX,\n" +
                        "       T.IS_TDS_DEDUCTED,\n" +
                        "       CASE\n" +
                        "          WHEN T.IS_TDS_DEDUCTED = 0 THEN\n" +
                        "           CASE\n" +
                        "             WHEN T.PAN_NUMBER <> '' THEN\n" +
                        "\n" +
                        "              CONCAT(CONCAT('1.TDS Tax - ',\n" +
                        "                            CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.ASSESS_AMOUNT, 2),\n" +
                        "                                                        'x '),\n" +
                        "                                                 CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(1 = 1,\n" +
                        "                                                                                            T.RATE,\n" +
                        "                                                                                            0.00),\n" +
                        "                                                                                         '@',\n" +
                        "                                                                                         1),\n" +
                        "                                                                         '@',\n" +
                        "                                                                         -1)),\n" +
                        "                                                        ' % ')),\n" +
                        "                                          ' = '),\n" +
                        "                                   ROUND(IF(1 = 1,\n" +
                        "                                            T.ASSESS_AMOUNT *\n" +
                        "                                            (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                             '@',\n" +
                        "                                                                             1),\n" +
                        "                                                             '@',\n" +
                        "                                                             -1)) / 100,\n" +
                        "                                            0),\n" +
                        "                                         2))),\n" +
                        "                     IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                   '@',\n" +
                        "                                                                   3),\n" +
                        "                                                   '@',\n" +
                        "                                                   -1)),\n" +
                        "                         '\n" +
                        "',\n" +
                        "                         ''),\n" +
                        "                     IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                  '@',\n" +
                        "                                                                  3),\n" +
                        "                                                  '@',\n" +
                        "                                                  -1)),\n" +
                        "                        CONCAT('2.Surcharge - ',\n" +
                        "                               CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.TAX, 2), 'x '),\n" +
                        "                                                    CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(3 = 3,\n" +
                        "                                                                                               T.RATE,\n" +
                        "                                                                                               0.00),\n" +
                        "                                                                                            '@',\n" +
                        "                                                                                            3),\n" +
                        "                                                                            '@',\n" +
                        "                                                                            -1)),\n" +
                        "                                                           ' % ')),\n" +
                        "                                             ' = '),\n" +
                        "                                      ROUND(IF(3 = 3,\n" +
                        "\n" +
                        "                                               IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                   '@',\n" +
                        "                                                                                   3),\n" +
                        "                                                                   '@',\n" +
                        "                                                                   -1)) > 0 AND\n" +
                        "                                                  T.TAX >=\n" +
                        "                                                  (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                   '@',\n" +
                        "                                                                                   3),\n" +
                        "                                                                   '@',\n" +
                        "                                                                   -1)),\n" +
                        "                                                  T.TAX *\n" +
                        "                                                  (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                   '@',\n" +
                        "                                                                                   3),\n" +
                        "                                                                   '@',\n" +
                        "                                                                   -1)) / 100,\n" +
                        "                                                  0),\n" +
                        "                                               2),\n" +
                        "                                            0.00))),\n" +
                        "                        ''),\n" +
                        "                     IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                   '@',\n" +
                        "                                                                   4),\n" +
                        "                                                   '@',\n" +
                        "                                                   -1)),\n" +
                        "                         '\n" +
                        "',\n" +
                        "                         ''),\n" +
                        "                     IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                  '@',\n" +
                        "                                                                  4),\n" +
                        "                                                  '@',\n" +
                        "                                                  -1)),\n" +
                        "                        CONCAT('3.Ed Cess - ',\n" +
                        "                               CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.TAX, 2), 'x '),\n" +
                        "                                                    CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(4 = 4,\n" +
                        "                                                                                               T.RATE,\n" +
                        "                                                                                               0.00),\n" +
                        "                                                                                            '@',\n" +
                        "                                                                                            4),\n" +
                        "                                                                            '@',\n" +
                        "                                                                            -1)),\n" +
                        "                                                           ' % ')),\n" +
                        "                                             ' = '),\n" +
                        "                                      ROUND(IF(3 = 3,\n" +
                        "                                               IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                   '@',\n" +
                        "                                                                                   4),\n" +
                        "                                                                   '@',\n" +
                        "                                                                   -1)) > 0 AND\n" +
                        "                                                  T.TAX >=\n" +
                        "                                                  (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                   '@',\n" +
                        "                                                                                   4),\n" +
                        "                                                                   '@',\n" +
                        "                                                                   -1)),\n" +
                        "                                                  T.TAX *\n" +
                        "                                                  (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                   '@',\n" +
                        "                                                                                   4),\n" +
                        "                                                                   '@',\n" +
                        "                                                                   -1)) / 100,\n" +
                        "                                                  0),\n" +
                        "                                               2),\n" +
                        "                                            0))),\n" +
                        "                        ''),\n" +
                        "                     IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                   '@',\n" +
                        "                                                                   5),\n" +
                        "                                                   '@',\n" +
                        "                                                   -1)),\n" +
                        "                         '\n" +
                        "',\n" +
                        "                         ''),\n" +
                        "                     IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                  '@',\n" +
                        "                                                                  5),\n" +
                        "                                                  '@',\n" +
                        "                                                  -1)),\n" +
                        "                        CONCAT('4.Sec Ed Cess - ',\n" +
                        "                               CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.TAX, 2), 'x '),\n" +
                        "                                                    CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(5 = 5,\n" +
                        "                                                                                               T.RATE,\n" +
                        "                                                                                               0.00),\n" +
                        "                                                                                            '@',\n" +
                        "                                                                                            5),\n" +
                        "                                                                            '@',\n" +
                        "                                                                            -1)),\n" +
                        "                                                           ' % ')),\n" +
                        "                                             ' = '),\n" +
                        "                                      ROUND(IF(5 = 5,\n" +
                        "                                               IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                   '@',\n" +
                        "                                                                                   5),\n" +
                        "                                                                   '@',\n" +
                        "                                                                   -1)) > 0 AND\n" +
                        "                                                  T.TAX >=\n" +
                        "                                                  (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                   '@',\n" +
                        "                                                                                   5),\n" +
                        "                                                                   '@',\n" +
                        "                                                                   -1)),\n" +
                        "                                                  T.TAX *\n" +
                        "                                                  (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                   '@',\n" +
                        "                                                                                   5),\n" +
                        "                                                                   '@',\n" +
                        "                                                                   -1)) / 100,\n" +
                        "                                                  0),\n" +
                        "                                               2),\n" +
                        "                                            0))),\n" +
                        "                        ''),\n" +
                        "                     '\n" +
                        "',\n" +
                        "                     CONCAT(' '),\n" +
                        "                     '\n" +
                        "',\n" +
                        "                     CONCAT('Payable to TDS =',\n" +
                        "                            ROUND(IF(1 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                               '@',\n" +
                        "                                                                               1),\n" +
                        "                                                               '@',\n" +
                        "                                                               -1)),\n" +
                        "                                     T.ASSESS_AMOUNT *\n" +
                        "                                     (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                      '@',\n" +
                        "                                                                      1),\n" +
                        "                                                      '@',\n" +
                        "                                                      -1)) / 100,\n" +
                        "                                     0),\n" +
                        "                                  2) +\n" +
                        "                            ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                               '@',\n" +
                        "                                                                               3),\n" +
                        "                                                               '@',\n" +
                        "                                                               -1)),\n" +
                        "                                     IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                         '@',\n" +
                        "                                                                         3),\n" +
                        "                                                         '@',\n" +
                        "                                                         -1)) > 0 AND\n" +
                        "                                        T.TAX >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  3),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)),\n" +
                        "                                        T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                 '@',\n" +
                        "                                                                                 3),\n" +
                        "                                                                 '@',\n" +
                        "                                                                 -1)) / 100,\n" +
                        "                                        0),\n" +
                        "                                     2),\n" +
                        "                                  0) +\n" +
                        "                            ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                               '@',\n" +
                        "                                                                               4),\n" +
                        "                                                               '@',\n" +
                        "                                                               -1)),\n" +
                        "                                     IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                         '@',\n" +
                        "                                                                         4),\n" +
                        "                                                         '@',\n" +
                        "                                                         -1)) > 0 AND\n" +
                        "                                        T.TAX >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  4),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)),\n" +
                        "                                        T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                 '@',\n" +
                        "                                                                                 4),\n" +
                        "                                                                 '@',\n" +
                        "                                                                 -1)) / 100,\n" +
                        "                                        0),\n" +
                        "                                     2),\n" +
                        "                                  0) +\n" +
                        "                            ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                               '@',\n" +
                        "                                                                               4),\n" +
                        "                                                               '@',\n" +
                        "                                                               -1)),\n" +
                        "                                     IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                         '@',\n" +
                        "                                                                         5),\n" +
                        "                                                         '@',\n" +
                        "                                                         -1)) > 0 AND\n" +
                        "                                        T.TAX >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  5),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)),\n" +
                        "                                        T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                 '@',\n" +
                        "                                                                                 5),\n" +
                        "                                                                 '@',\n" +
                        "                                                                 -1)) / 100,\n" +
                        "                                        0),\n" +
                        "                                     0),\n" +
                        "                                  0)),\n" +
                        "                     '\n" +
                        "',\n" +
                        "                     CONCAT('Payable to Party = ',\n" +
                        "                            ROUND(T.ASSESS_AMOUNT -\n" +
                        "                                  (ROUND(IF(1 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                      '@',\n" +
                        "                                                                                      1),\n" +
                        "                                                                      '@',\n" +
                        "                                                                      -1)),\n" +
                        "                                            T.ASSESS_AMOUNT *\n" +
                        "                                            (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                             '@',\n" +
                        "                                                                             1),\n" +
                        "                                                             '@',\n" +
                        "                                                             -1)) / 100,\n" +
                        "                                            0),\n" +
                        "                                         2) +\n" +
                        "                                  ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                      '@',\n" +
                        "                                                                                      3),\n" +
                        "                                                                      '@',\n" +
                        "                                                                      -1)),\n" +
                        "                                            IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                '@',\n" +
                        "                                                                                3),\n" +
                        "                                                                '@',\n" +
                        "                                                                -1)) > 0 AND\n" +
                        "                                               T.TAX >=\n" +
                        "                                               (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                '@',\n" +
                        "                                                                                3),\n" +
                        "                                                                '@',\n" +
                        "                                                                -1)),\n" +
                        "                                               T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                        '@',\n" +
                        "                                                                                        3),\n" +
                        "                                                                        '@',\n" +
                        "                                                                        -1)) / 100,\n" +
                        "                                               0),\n" +
                        "                                            2),\n" +
                        "                                         0) +\n" +
                        "                                  ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                      '@',\n" +
                        "                                                                                      4),\n" +
                        "                                                                      '@',\n" +
                        "                                                                      -1)),\n" +
                        "                                            IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                '@',\n" +
                        "                                                                                4),\n" +
                        "                                                                '@',\n" +
                        "                                                                -1)) > 0 AND\n" +
                        "                                               T.TAX >=\n" +
                        "                                               (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                '@',\n" +
                        "                                                                                4),\n" +
                        "                                                                '@',\n" +
                        "                                                                -1)),\n" +
                        "                                               T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                        '@',\n" +
                        "                                                                                        4),\n" +
                        "                                                                        '@',\n" +
                        "                                                                        -1)) / 100,\n" +
                        "                                               0),\n" +
                        "                                            2),\n" +
                        "                                         0) +\n" +
                        "                                  ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                      '@',\n" +
                        "                                                                                      5),\n" +
                        "                                                                      '@',\n" +
                        "\n" +
                        "                                                                      -1)),\n" +
                        "                                            IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                '@',\n" +
                        "                                                                                5),\n" +
                        "                                                                '@',\n" +
                        "                                                                -1)) > 0 AND\n" +
                        "                                               T.TAX >=\n" +
                        "                                               (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                '@',\n" +
                        "                                                                                5),\n" +
                        "                                                                '@',\n" +
                        "                                                                -1)),\n" +
                        "\n" +
                        "                                               T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                        '@',\n" +
                        "                                                                                        5),\n" +
                        "                                                                        '@',\n" +
                        "                                                                        -1)) / 100,\n" +
                        "                                               0),\n" +
                        "                                            2),\n" +
                        "                                         0)),\n" +
                        "                                  0)))\n" +
                        "\n" +
                        "             ELSE\n" +
                        "\n" +
                        "              CONCAT(CONCAT('1.TDS Tax - ',\n" +
                        "                            CONCAT(CONCAT(CONCAT(CONCAT(FORMAT(T.ASSESS_AMOUNT, 2),\n" +
                        "                                                        ' x '),\n" +
                        "                                                 CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(2 =\n" +
                        "                                                                                            ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                                                                  '@',\n" +
                        "                                                                                                                                  2),\n" +
                        "                                                                                                                  '@',\n" +
                        "                                                                                                                  -1)),\n" +
                        "                                                                                            T.RATE,\n" +
                        "                                                                                            0.00),\n" +
                        "                                                                                         '@',\n" +
                        "                                                                                         2),\n" +
                        "                                                                         '@',\n" +
                        "                                                                         -1)),\n" +
                        "                                                        '% ')),\n" +
                        "                                          ' = '),\n" +
                        "                                   ROUND(IF(2 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                      '@',\n" +
                        "                                                                                      2),\n" +
                        "                                                                      '@',\n" +
                        "                                                                      -1)),\n" +
                        "                                            T.ASSESS_AMOUNT *\n" +
                        "                                            (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                             '@',\n" +
                        "                                                                             2),\n" +
                        "                                                             '@',\n" +
                        "                                                             -1)) / 100,\n" +
                        "                                            2)))),\n" +
                        "                     IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                   '@',\n" +
                        "                                                                   3),\n" +
                        "                                                   '@',\n" +
                        "                                                   -1)),\n" +
                        "                         '\n" +
                        "',\n" +
                        "                         ''),\n" +
                        "                     IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                  '@',\n" +
                        "                                                                  3),\n" +
                        "                                                  '@',\n" +
                        "                                                  -1)),\n" +
                        "                        CONCAT('2.Surcharge - ',\n" +
                        "                               CONCAT(CONCAT(CONCAT(CONCAT(T.TAX, ' x '),\n" +
                        "                                                    CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(3 = 3,\n" +
                        "                                                                                               T.RATE,\n" +
                        "                                                                                               0.00),\n" +
                        "                                                                                            '@',\n" +
                        "                                                                                            3),\n" +
                        "                                                                            '@',\n" +
                        "                                                                            -1)),\n" +
                        "                                                           '% ')),\n" +
                        "                                             ' = '),\n" +
                        "                                      ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                         '@',\n" +
                        "                                                                                         3),\n" +
                        "                                                                         '@',\n" +
                        "                                                                         -1)),\n" +
                        "                                               IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                   '@',\n" +
                        "                                                                                   3),\n" +
                        "                                                                   '@',\n" +
                        "                                                                   -1)) > 0 AND\n" +
                        "                                                  T.TAX >=\n" +
                        "                                                  (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                   '@',\n" +
                        "                                                                                   3),\n" +
                        "                                                                   '@',\n" +
                        "                                                                   -1)),\n" +
                        "                                                  T.TAX *\n" +
                        "                                                  (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                   '@',\n" +
                        "                                                                                   3),\n" +
                        "                                                                   '@',\n" +
                        "                                                                   -1)) / 100,\n" +
                        "                                                  2),\n" +
                        "                                               0)))),\n" +
                        "                        ''),\n" +
                        "                     IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                   '@',\n" +
                        "                                                                   4),\n" +
                        "                                                   '@',\n" +
                        "                                                   -1)),\n" +
                        "                         '\n" +
                        "',\n" +
                        "                         ''),\n" +
                        "                     IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                  '@',\n" +
                        "                                                                  4),\n" +
                        "                                                  '@',\n" +
                        "                                                  -1)),\n" +
                        "                        CONCAT('3.Ed Cess - ',\n" +
                        "                               CONCAT(CONCAT(CONCAT(CONCAT(T.TAX, ' x '),\n" +
                        "                                                    CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(4 = 4,\n" +
                        "                                                                                               T.RATE,\n" +
                        "                                                                                               0.00),\n" +
                        "                                                                                            '@',\n" +
                        "                                                                                            4),\n" +
                        "                                                                            '@',\n" +
                        "                                                                            -1)),\n" +
                        "                                                           '% ')),\n" +
                        "                                             ' = '),\n" +
                        "                                      ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                         '@',\n" +
                        "                                                                                         4),\n" +
                        "                                                                         '@',\n" +
                        "                                                                         -1)),\n" +
                        "\n" +
                        "                                               IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                   '@',\n" +
                        "                                                                                   4),\n" +
                        "                                                                   '@',\n" +
                        "                                                                   -1)) > 0 AND\n" +
                        "                                                  T.TAX >=\n" +
                        "                                                  (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                   '@',\n" +
                        "                                                                                   4),\n" +
                        "                                                                   '@',\n" +
                        "                                                                   -1)),\n" +
                        "                                                  T.TAX *\n" +
                        "                                                  (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                   '@',\n" +
                        "                                                                                   4),\n" +
                        "                                                                   '@',\n" +
                        "                                                                   -1)) / 100,\n" +
                        "                                                  2),\n" +
                        "                                               0)))),\n" +
                        "                        ''),\n" +
                        "                     IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                   '@',\n" +
                        "                                                                   5),\n" +
                        "                                                   '@',\n" +
                        "                                                   -1)),\n" +
                        "                         '\n" +
                        "',\n" +
                        "                         ''),\n" +
                        "                     IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                  '@',\n" +
                        "                                                                  5),\n" +
                        "                                                  '@',\n" +
                        "                                                  -1)),\n" +
                        "                        CONCAT('4.Sec Ed Cess - ',\n" +
                        "                               CONCAT(CONCAT(CONCAT(CONCAT(T.TAX, ' x '),\n" +
                        "                                                    CONCAT((SUBSTRING_INDEX(SUBSTRING_INDEX(IF(5 =\n" +
                        "                                                                                               ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                                                                     '@',\n" +
                        "                                                                                                                                     5),\n" +
                        "                                                                                                                     '@',\n" +
                        "                                                                                                                     -1)),\n" +
                        "                                                                                               T.RATE,\n" +
                        "                                                                                               0.00),\n" +
                        "                                                                                            '@',\n" +
                        "                                                                                            5),\n" +
                        "                                                                            '@',\n" +
                        "                                                                            -1)),\n" +
                        "                                                           '% ')),\n" +
                        "                                             ' = '),\n" +
                        "                                      ROUND(IF(5 = 5,\n" +
                        "                                               IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                   '@',\n" +
                        "                                                                                   5),\n" +
                        "                                                                   '@',\n" +
                        "                                                                   -1)) > 0 AND\n" +
                        "                                                  T.TAX >=\n" +
                        "                                                  (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                   '@',\n" +
                        "                                                                                   5),\n" +
                        "                                                                   '@',\n" +
                        "                                                                   -1)),\n" +
                        "                                                  T.TAX *\n" +
                        "                                                  (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                   '@',\n" +
                        "                                                                                   5),\n" +
                        "                                                                   '@',\n" +
                        "                                                                   -1)) / 100,\n" +
                        "                                                  2),\n" +
                        "                                               0)))),\n" +
                        "                        ''),\n" +
                        "                     '\n" +
                        "',\n" +
                        "                     CONCAT(''),\n" +
                        "                     '\n" +
                        "',\n" +
                        "\n" +
                        "                     CONCAT('Payable to TDS =',\n" +
                        "                            ROUND(IF(2 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                               '@',\n" +
                        "                                                                               2),\n" +
                        "                                                               '@',\n" +
                        "                                                               -1)),\n" +
                        "                                     T.ASSESS_AMOUNT *\n" +
                        "                                     (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                      '@',\n" +
                        "                                                                      2),\n" +
                        "                                                      '@',\n" +
                        "                                                      -1)) / 100,\n" +
                        "                                     0),\n" +
                        "                                  2) +\n" +
                        "                            ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                               '@',\n" +
                        "                                                                               3),\n" +
                        "                                                               '@',\n" +
                        "                                                               -1)),\n" +
                        "                                     IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                         '@',\n" +
                        "                                                                         3),\n" +
                        "                                                         '@',\n" +
                        "                                                         -1)) > 0 AND\n" +
                        "                                        T.TAX >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  3),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)),\n" +
                        "                                        T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                 '@',\n" +
                        "                                                                                 3),\n" +
                        "                                                                 '@',\n" +
                        "                                                                 -1)) / 100,\n" +
                        "                                        0),\n" +
                        "                                     2),\n" +
                        "                                  0) +\n" +
                        "                            ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                               '@',\n" +
                        "                                                                               4),\n" +
                        "                                                               '@',\n" +
                        "                                                               -1)),\n" +
                        "                                     IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                         '@',\n" +
                        "                                                                         4),\n" +
                        "                                                         '@',\n" +
                        "                                                         -1)) > 0 AND\n" +
                        "                                        T.TAX >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  4),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)),\n" +
                        "                                        T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                 '@',\n" +
                        "                                                                                 4),\n" +
                        "                                                                 '@',\n" +
                        "                                                                 -1)) / 100,\n" +
                        "                                        0),\n" +
                        "                                     2),\n" +
                        "                                  0) +\n" +
                        "                            ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                               '@',\n" +
                        "                                                                               5),\n" +
                        "                                                               '@',\n" +
                        "                                                               -1)),\n" +
                        "                                     IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                         '@',\n" +
                        "                                                                         5),\n" +
                        "                                                         '@',\n" +
                        "                                                         -1)) > 0 AND\n" +
                        "                                        T.TAX >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                  '@',\n" +
                        "                                                                                  5),\n" +
                        "                                                                  '@',\n" +
                        "                                                                  -1)),\n" +
                        "                                        T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                 '@',\n" +
                        "                                                                                 5),\n" +
                        "                                                                 '@',\n" +
                        "                                                                 -1)) / 100,\n" +
                        "                                        0),\n" +
                        "                                     2),\n" +
                        "                                  0)),\n" +
                        "                     '\n" +
                        "',\n" +
                        "                     CONCAT('Payable to Party = ',\n" +
                        "                            ROUND(T.ASSESS_AMOUNT -\n" +
                        "                                  (ROUND(IF(2 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                      '@',\n" +
                        "                                                                                      2),\n" +
                        "                                                                      '@',\n" +
                        "                                                                      -1)),\n" +
                        "                                            T.ASSESS_AMOUNT *\n" +
                        "                                            (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                             '@',\n" +
                        "                                                                             2),\n" +
                        "                                                             '@',\n" +
                        "                                                             -1)) / 100,\n" +
                        "                                            0),\n" +
                        "                                         2) +\n" +
                        "                                  ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                      '@',\n" +
                        "                                                                                      3),\n" +
                        "                                                                      '@',\n" +
                        "                                                                      -1)),\n" +
                        "                                            IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                '@',\n" +
                        "                                                                                3),\n" +
                        "                                                                '@',\n" +
                        "                                                                -1)) > 0 AND\n" +
                        "                                               T.TAX >=\n" +
                        "                                               (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                '@',\n" +
                        "                                                                                3),\n" +
                        "                                                                '@',\n" +
                        "                                                                -1)),\n" +
                        "                                               T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                        '@',\n" +
                        "                                                                                        3),\n" +
                        "                                                                        '@',\n" +
                        "                                                                        -1)) / 100,\n" +
                        "                                               0),\n" +
                        "                                            2),\n" +
                        "                                         0) +\n" +
                        "                                  ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                      '@',\n" +
                        "                                                                                      4),\n" +
                        "                                                                      '@',\n" +
                        "                                                                      -1)),\n" +
                        "                                            IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                '@',\n" +
                        "                                                                                4),\n" +
                        "                                                                '@',\n" +
                        "                                                                -1)) > 0 AND\n" +
                        "                                               T.TAX >=\n" +
                        "                                               (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                '@',\n" +
                        "                                                                                4),\n" +
                        "                                                                '@',\n" +
                        "                                                                -1)),\n" +
                        "                                               T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                        '@',\n" +
                        "                                                                                        4),\n" +
                        "                                                                        '@',\n" +
                        "                                                                        -1)) / 100,\n" +
                        "                                               0),\n" +
                        "                                            2),\n" +
                        "                                         0) +\n" +
                        "                                  ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                                      '@',\n" +
                        "                                                                                      5),\n" +
                        "                                                                      '@',\n" +
                        "                                                                      -1)),\n" +
                        "                                            IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                '@',\n" +
                        "                                                                                5),\n" +
                        "                                                                '@',\n" +
                        "                                                                -1)) > 0 AND\n" +
                        "                                               T.TAX >=\n" +
                        "                                               (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                                '@',\n" +
                        "                                                                                5),\n" +
                        "                                                                '@',\n" +
                        "                                                                -1)),\n" +
                        "\n" +
                        "                                               T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE,\n" +
                        "                                                                                        '@',\n" +
                        "                                                                                        5),\n" +
                        "                                                                        '@',\n" +
                        "                                                                        -1)) / 100,\n" +
                        "                                               0),\n" +
                        "                                            2),\n" +
                        "                                         0)),\n" +
                        "                                  0)))\n" +
                        "\n" +
                        "           END\n" +
                        "        END AS 'TAX_AMOUNT',\n" +
                        "       CASE\n" +
                        "         WHEN T.IS_TDS_DEDUCTED = 0 THEN\n" +
                        "          CASE\n" +
                        "            WHEN T.PAN_NUMBER <> '' THEN\n" +
                        "             ROUND(IF(1 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                '@',\n" +
                        "                                                                1),\n" +
                        "                                                '@',\n" +
                        "                                                -1)),\n" +
                        "                      T.ASSESS_AMOUNT *\n" +
                        "                      (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE, '@', 1), '@', -1)) / 100,\n" +
                        "                      0),\n" +
                        "                   0) +\n" +
                        "             ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                '@',\n" +
                        "                                                                3),\n" +
                        "                                                '@',\n" +
                        "                                                -1)),\n" +
                        "                      IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                          '@',\n" +
                        "                                                          3),\n" +
                        "                                          '@',\n" +
                        "                                          -1)) > 0 AND\n" +
                        "                         T.TAX >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                   '@',\n" +
                        "                                                                   3),\n" +
                        "                                                   '@',\n" +
                        "                                                   -1)),\n" +
                        "                         T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE, '@', 3),\n" +
                        "                                                  '@',\n" +
                        "                                                  -1)) / 100,\n" +
                        "                         0),\n" +
                        "                      0),\n" +
                        "                   0) +\n" +
                        "             ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                '@',\n" +
                        "                                                                4),\n" +
                        "                                                '@',\n" +
                        "                                                -1)),\n" +
                        "                      IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                          '@',\n" +
                        "                                                          4),\n" +
                        "                                          '@',\n" +
                        "                                          -1)) > 0 AND\n" +
                        "                         T.TAX >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                   '@',\n" +
                        "                                                                   4),\n" +
                        "                                                   '@',\n" +
                        "                                                   -1)),\n" +
                        "                         T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE, '@', 4),\n" +
                        "                                                  '@',\n" +
                        "                                                  -1)) / 100,\n" +
                        "                         0),\n" +
                        "                      0),\n" +
                        "                   0) +\n" +
                        "             ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                '@',\n" +
                        "                                                                4),\n" +
                        "                                                '@',\n" +
                        "                                                -1)),\n" +
                        "                      IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                          '@',\n" +
                        "                                                          5),\n" +
                        "                                          '@',\n" +
                        "                                          -1)) > 0 AND\n" +
                        "                         T.TAX >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                   '@',\n" +
                        "                                                                   5),\n" +
                        "                                                   '@',\n" +
                        "                                                   -1)),\n" +
                        "                         T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE, '@', 5),\n" +
                        "                                                  '@',\n" +
                        "                                                  -1)) / 100,\n" +
                        "                         0),\n" +
                        "                      0),\n" +
                        "                   0)\n" +
                        "            ELSE\n" +
                        "\n" +
                        "             ROUND(IF(2 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                '@',\n" +
                        "                                                                2),\n" +
                        "                                                '@',\n" +
                        "                                                -1)),\n" +
                        "                      T.ASSESS_AMOUNT *\n" +
                        "                      (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE, '@', 2), '@', -1)) / 100,\n" +
                        "                      0),\n" +
                        "                   0) +\n" +
                        "             ROUND(IF(3 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                '@',\n" +
                        "                                                                3),\n" +
                        "                                                '@',\n" +
                        "                                                -1)),\n" +
                        "\n" +
                        "                      IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                          '@',\n" +
                        "                                                          3),\n" +
                        "                                          '@',\n" +
                        "                                          -1)) > 0 AND\n" +
                        "                         T.TAX >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                   '@',\n" +
                        "                                                                   3),\n" +
                        "                                                   '@',\n" +
                        "                                                   -1)),\n" +
                        "                         T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE, '@', 3),\n" +
                        "                                                  '@',\n" +
                        "                                                  -1)) / 100,\n" +
                        "                         0),\n" +
                        "                      0),\n" +
                        "                   0) +\n" +
                        "             ROUND(IF(4 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                '@',\n" +
                        "                                                                4),\n" +
                        "                                                '@',\n" +
                        "                                                -1)),\n" +
                        "                      IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                          '@',\n" +
                        "                                                          4),\n" +
                        "                                          '@',\n" +
                        "                                          -1)) > 0 AND\n" +
                        "                         T.TAX >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                   '@',\n" +
                        "                                                                   4),\n" +
                        "                                                   '@',\n" +
                        "                                                   -1)),\n" +
                        "                         T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE, '@', 4),\n" +
                        "                                                  '@',\n" +
                        "                                                  -1)) / 100,\n" +
                        "                         0),\n" +
                        "                      0),\n" +
                        "                   0) +\n" +
                        "             ROUND(IF(5 = ROUND(SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_TAX_TYPE,\n" +
                        "                                                                '@',\n" +
                        "                                                                4),\n" +
                        "                                                '@',\n" +
                        "                                                -1)),\n" +
                        "                      IF((SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                          '@',\n" +
                        "                                                          5),\n" +
                        "                                          '@',\n" +
                        "                                          -1)) > 0 AND\n" +
                        "                         T.TAX >= (SUBSTRING_INDEX(SUBSTRING_INDEX(T.TDS_LIMITATION_AMOUNT,\n" +
                        "                                                                   '@',\n" +
                        "                                                                   5),\n" +
                        "                                                   '@',\n" +
                        "                                                   -1)),\n" +
                        "                         T.TAX * (SUBSTRING_INDEX(SUBSTRING_INDEX(T.RATE, '@', 5),\n" +
                        "                                                  '@',\n" +
                        "                                                  -1)) / 100,\n" +
                        "                         0),\n" +
                        "                      0),\n" +
                        "                   0)\n" +
                        "          END\n" +
                        "       END AS BALANCE,\n" +
                        "       T.DEDUCT_NOW AS 'Id',\n" +
                        "       T.LEDGER_NAME,\n" +
                        "       T.TDS_TAX_TYPE_ID,\n" +
                        "       T.TDS_RATE,\n" +
                        "       T.TDS_EXEMPTION_LIMIT,\n" +
                        "       T.TDS_LIMITATION_AMOUNT,\n" +
                        "       T.APPLICABLE_FROM\n" +
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
                        "               TNP.NAME,\n" +
                        "               TTR.TDS_TAX_TYPE_ID,\n" +
                        "               TBD.IS_TDS_DEDUCTED,\n" +
                        "               TCP.PAN_NUMBER,\n" +
                        "               TTR.TDS_RATE,\n" +
                        "               TB.AMOUNT,\n" +
                        "               TTR.TDS_EXEMPTION_LIMIT,\n" +
                        "               TP.APPLICABLE_FROM,\n" +
                        "               ROUND(TBD.ASSESS_AMOUNT, 2) AS ASSESS_AMOUNT,\n" +
                        "               GROUP_CONCAT(TTR.TDS_RATE ORDER BY TTR.TDS_TAX_TYPE_ID\n" +
                        "                            SEPARATOR '@') AS Rate,\n" +
                        "               GROUP_CONCAT(TTR.TDS_TAX_TYPE_ID ORDER BY TTR.TDS_TAX_TYPE_ID\n" +
                        "                            SEPARATOR '@') AS TDS_TAX_TYPE,\n" +
                        "               GROUP_CONCAT(TTR.TDS_EXEMPTION_LIMIT ORDER BY\n" +
                        "                            TTR.TDS_TAX_TYPE_ID SEPARATOR '@') AS TDS_LIMITATION_AMOUNT,\n" +
                        "               CASE\n" +
                        "                 WHEN TCP.PAN_NUMBER <> '' THEN\n" +
                        "                  ROUND((ASSESS_AMOUNT *\n" +
                        "                        SUBSTRING_INDEX(GROUP_CONCAT(TTR.TDS_RATE ORDER BY\n" +
                        "                                                      TTR.TDS_TAX_TYPE_ID\n" +
                        "                                                      SEPARATOR '@'),\n" +
                        "                                         '@',\n" +
                        "                                         1)) / 100,\n" +
                        "                        2)\n" +
                        "                 ELSE\n" +
                        "                  ROUND(ASSESS_AMOUNT * 20 / 100, 2)\n" +
                        "               END AS Tax,\n" +
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
                        "               ML.LEDGER_NAME\n" +
                        "          FROM TDS_BOOKING AS TB\n" +
                        "          LEFT JOIN TDS_BOOKING_DETAIL AS TBD\n" +
                        "            ON TB.BOOKING_ID = TBD.BOOKING_ID\n" +
                        "          LEFT JOIN VOUCHER_MASTER_TRANS AS VMT\n" +
                        "            ON TB.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        "          LEFT JOIN MASTER_LEDGER AS ML\n" +
                        "            ON TB.PARTY_LEDGER_ID = ML.LEDGER_ID\n" +
                        "          LEFT JOIN TDS_CREDTIORS_PROFILE AS TCP\n" +
                        "            ON ML.LEDGER_ID = TCP.LEDGER_ID\n" +
                        "          LEFT JOIN TDS_NATURE_PAYMENT AS TNP\n" +
                        "            ON TBD.NATURE_OF_PAYMENT_ID = TNP.NATURE_PAY_ID\n" +
                        "          LEFT JOIN TDS_POLICY AS TP\n" +
                        "            ON TBD.NATURE_OF_PAYMENT_ID = TP.TDS_NATURE_PAYMENT_ID\n" +
                        "          LEFT JOIN TDS_TAX_RATE AS TTR\n" +
                        "            ON TP.TDS_POLICY_ID = TTR.TDS_POLICY_ID\n" +
                        "          LEFT JOIN TDS_DUTY_TAXTYPE AS TDT\n" +
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
                        "                       AND TBD1.IS_TDS_DEDUCTED = 0\n" +
                        "                     GROUP BY TP.TDS_NATURE_PAYMENT_ID) AS TTD\n" +
                        "            ON TBD.NATURE_OF_PAYMENT_ID = TTD.TDS_NATURE_PAYMENT_ID\n" +
                        "         WHERE TB.PARTY_LEDGER_ID = ?LEDGER_ID\n" +
                        "           AND TP.TDS_DEDUCTEE_TYPE_ID = ?DEDUCTEE_TYPE_ID\n" +
                        "           AND TP.APPLICABLE_FROM = TTD.APPLICABLE_FROM\n" +
                        "           AND TB.IS_DELETED = 1\n" +
                        "           AND TDT.STATUS = 1\n" +
                        "           AND TB.PROJECT_ID IN (?PROJECT_ID)\n" +
                        "           AND TBD.NATURE_OF_PAYMENT_ID = ?NATURE_OF_PAYMENT_ID\n" +
                        "           AND TBD.IS_TDS_DEDUCTED = 0\n" +
                        "         GROUP BY TBD.BOOKING_DETAIL_ID) AS T";

                        break;
                    }
                case SQLCommand.TDSDeduction.FetchDeductionId:
                    {
                        query = "SELECT DEDUCTION_ID FROM TDS_DEDUCTION WHERE VOUCHER_ID=?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.TDSDeduction.UpdateIsTDSDeductable:
                    {
                        query = "UPDATE TDS_DEDUCTION SET IS_DELETED=0 WHERE VOUCHER_ID =?VOUCHER_ID";
                        break;
                    }
            }
            return query;
        }
        #endregion TDS SQL
    }
}
