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
    public class TDSPaymentDetailSQL : IDatabaseQuery
    {
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        public string GetQuery(DAO.Data.DataCommandArguments dataCommandArgs, ref DAO.Data.SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.TDSPayemtDetail).FullName)
            {
                query = GetTDSPayment();
            }

            sqlType = this.sqlType;
            return query;
        }

        private string GetTDSPayment()
        {
            string query = "";
            SQLCommand.TDSPayemtDetail sqlCommandId = (SQLCommand.TDSPayemtDetail)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.TDSPayemtDetail.Add:
                    {
                        query = "INSERT INTO TDS_PAYMENT_DETAIL\n" +
                        "   (TDS_PAYMENT_ID,\n" +
                        "   DEDUCTION_DETAIL_ID,\n" +
                        "   PAID_AMOUNT,\n" +
                        "   LEDGER_ID,\n" +
                        "   FLAG,\n" +
                        "   IS_ADVANCE_PAID,\n" +
                        "   IS_ADVANCE_ADJUSTED)\n" +
                        "VALUES\n" +
                        "  (?TDS_PAYMENT_ID,\n" +
                        "   ?DEDUCTION_DETAIL_ID,\n" +
                        "   ?PAID_AMOUNT,\n" +
                         "  ?LEDGER_ID,\n" +
                        "   ?FLAG,\n" +
                        "   ?IS_ADVANCE_PAID,\n" +
                        "   ?IS_ADVANCE_ADJUSTED)";
                        break;
                    }
                case SQLCommand.TDSPayemtDetail.Update:
                    {
                        query = "UPDATE TDS_PAYMENT_DETAIL\n" +
                         "   SET TDS_PAYMENT_ID      = ?TDS_PAYMENT_ID,\n" +
                         "       DEDUCTION_DETAIL_ID = ?DEDUCTION_DETAIL_ID,\n" +
                         "       PAID_AMOUNT         = ?PAID_AMOUNT,\n" +
                         "       IS_ADVANCE_PAID     = ?IS_ADVANCE_PAID,\n" +
                         "       IS_ADVANCE_ADJUSTED = ?IS_ADVANCE_ADJUSTED\n" +
                         " WHERE TDS_PAYMENT_DETAIL_ID = ?TDS_PAYMENT_DETAIL_ID";
                        break;
                    }
                case SQLCommand.TDSPayemtDetail.Delete:
                    {
                        query = "DELETE FROM TDS_PAYMENT_DETAIL WHERE TDS_PAYMENT_ID=?TDS_PAYMENT_ID";
                        break;
                    }

                case SQLCommand.TDSPayemtDetail.FetchAll:
                    {
                        query = "SELECT TDS_PAYMENT_DETAIL_ID,\n" +
                        "       TDS_PAYMENT_ID,\n" +
                        "       DEDUCTION_DETAIL_ID,\n" +
                        "       PAID_AMOUNT,\n" +
                        "       IS_ADVANCE_PAID,\n" +
                        "       IS_ADVANCE_ADJUSTED\n" +
                        "  FROM TDS_PAYMENT_DETAIL";

                        break;
                    }
                case SQLCommand.TDSPayemtDetail.Fetch:
                    {
                        query = "SELECT TDS_PAYMENT_DETAIL_ID,\n" +
                        "       TDS_PAYMENT_ID,\n" +
                        "       DEDUCTION_DETAIL_ID,\n" +
                        "       PAID_AMOUNT,\n" +
                        "       IS_ADVANCE_PAID,\n" +
                        "       IS_ADVANCE_ADJUSTED\n" +
                        "  FROM TDS_PAYMENT_DETAIL\n" +
                        " WHERE TDS_PAYMENT_DETAIL_ID = ?TDS_PAYMENT_DETAIL_ID";

                        break;
                    }
                case SQLCommand.TDSPayemtDetail.CheckTDSPayment:
                    {
                        query = "SELECT COUNT(*) AS TDS_COUNT\n" +
                                "  FROM TDS_PAYMENT\n" +
                                " WHERE IS_DELETED = 1\n" +
                                "   AND TDS_PAYMENT_ID IN\n" +
                                "       (SELECT TDS_PAYMENT_ID\n" +
                                "          FROM TDS_PAYMENT_DETAIL AS TPD\n" +
                                "         WHERE TPD.DEDUCTION_DETAIL_ID IN\n" +
                                "               -- (SELECT TPPD.DEDUCTION_DETAIL_ID\n" +
                                "                  -- FROM TDS_PARTY_PAYMENT_DETAIL AS TPPD\n" +
                                "                --  WHERE TPPD.DEDUCTION_DETAIL_ID IN\n" +
                                "                       (SELECT TDD.DEDUCTION_DETAIL_ID\n" +
                                "                          FROM TDS_DEDUCTION_DETAIL AS TDD\n" +
                                "                         WHERE TDD.BOOKING_DETAIL_ID IN\n" +
                                "                               (SELECT TBD.BOOKING_DETAIL_ID\n" +
                                "                                  FROM TDS_BOOKING_DETAIL AS TBD\n" +
                                "                                 WHERE TBD.BOOKING_ID IN\n" +
                                "                                       (SELECT TB.BOOKING_ID\n" +
                                "                                          FROM TDS_BOOKING AS TB\n" +
                                "                                         WHERE VOUCHER_ID =?VOUCHER_ID))));";
                        break;
                    }
                case SQLCommand.TDSPayemtDetail.HasTDSVoucher:
                    {
                        query = "SELECT COUNT(*) FROM TDS_PAYMENT WHERE VOUCHER_ID IN(?VOUCHER_ID) AND IS_DELETED=1";
                        break;
                    }
                case SQLCommand.TDSPayemtDetail.HasTDSLedger:
                    {
                        query = "SELECT COUNT(*)\n" +
                         "  FROM TDS_PAYMENT AS TP\n" +
                         " INNER JOIN TDS_PAYMENT_DETAIL AS TPD\n" +
                         "    ON TP.TDS_PAYMENT_ID = TPD.TDS_PAYMENT_ID\n" +
                         " WHERE TP.VOUCHER_ID = ?VOUCHER_ID\n" +
                         "   AND TPD.LEDGER_ID = ?LEDGER_ID\n" +
                         "   AND TP.IS_DELETED = 1";

                        break;
                    }
            }

            return query;
        }
    }
}
