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
    class PartyPaymentDetailSQL : IDatabaseQuery
    {
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        public string GetQuery(DAO.Data.DataCommandArguments dataCommandArgs, ref DAO.Data.SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.TDSPartyPaymentDetail).FullName)
            {
                query = GetBooking();
            }

            sqlType = this.sqlType;
            return query;
        }

        private string GetBooking()
        {
            string query = "";
            SQLCommand.TDSPartyPaymentDetail sqlCommandId = (SQLCommand.TDSPartyPaymentDetail)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.TDSPartyPaymentDetail.Add:
                    {
                        query = @"INSERT INTO TDS_PARTY_PAYMENT_DETAIL
                                  (PARTY_PAYMENT_ID,
                                   BOOKING_DETAIL_ID,
                                   DEDUCTION_DETAIL_ID,
                                   PAID_AMOUNT,
                                   IS_ADVANCE_PAID,
                                   IS_ADVANCE_ADJUSTED)
                                VALUES
                                  (?PARTY_PAYMENT_ID,
                                   ?BOOKING_DETAIL_ID,
                                   ?DEDUCTION_DETAIL_ID,
                                   ?PAID_AMOUNT,
                                   ?IS_ADVANCE_PAID,
                                   ?IS_ADVANCE_ADJUSTED);";
                        break;
                    }
                case SQLCommand.TDSPartyPaymentDetail.Update:
                    {
                        break;
                    }
                case SQLCommand.TDSPartyPaymentDetail.Delete:
                    {
                        break;
                    }

                case SQLCommand.TDSPartyPaymentDetail.FetchAll:
                    {
                        break;
                    }
                case SQLCommand.TDSPartyPaymentDetail.Fetch:
                    {
                        break;
                    }
                case SQLCommand.TDSPartyPaymentDetail.CheckPartyPayment:
                    {
                        //query = "SELECT COUNT(*) AS TDS_COUNT\n" +
                        // "  FROM TDS_PARTY_PAYMENT_DETAIL AS TPPD\n" +
                        // " WHERE TPPD.DEDUCTION_DETAIL_ID IN\n" +
                        // "       (SELECT TDD.DEDUCTION_DETAIL_ID\n" +
                        // "          FROM TDS_DEDUCTION_DETAIL AS TDD\n" +
                        // "         WHERE TDD.BOOKING_DETAIL_ID IN\n" +
                        // "               (SELECT TBD.BOOKING_DETAIL_ID\n" +
                        // "                  FROM TDS_BOOKING_DETAIL AS TBD\n" +
                        // "                 WHERE TBD.BOOKING_ID IN\n" +
                        // "                       (SELECT TB.BOOKING_ID\n" +
                        // "                          FROM TDS_BOOKING AS TB\n" +
                        // "                         WHERE VOUCHER_ID = ?VOUCHER_ID)))";


                        query = "SELECT COUNT(*) AS TDS_COUNT\n" +
                                "  FROM TDS_PARTY_PAYMENT\n" +
                                " WHERE IS_DELETED = 1\n" +
                                "   AND PARTY_PAYMENT_ID IN\n" +
                                "       (SELECT PARTY_PAYMENT_ID\n" +
                                "          FROM TDS_PARTY_PAYMENT_DETAIL AS TPPD\n" +
                                "         WHERE TPPD.DEDUCTION_DETAIL_ID IN\n" +
                                "               (SELECT TDD.DEDUCTION_DETAIL_ID\n" +
                                "                  FROM TDS_DEDUCTION_DETAIL AS TDD\n" +
                                "                 WHERE TDD.BOOKING_DETAIL_ID IN\n" +
                                "                       (SELECT TBD.BOOKING_DETAIL_ID\n" +
                                "                          FROM TDS_BOOKING_DETAIL AS TBD\n" +
                                "                         WHERE TBD.BOOKING_ID IN\n" +
                                "                               (SELECT TB.BOOKING_ID\n" +
                                "                                  FROM TDS_BOOKING AS TB\n" +
                                "                                 WHERE VOUCHER_ID = ?VOUCHER_ID))));";

                        break;
                    }

                case SQLCommand.TDSPartyPaymentDetail.CheckIsPartyVoucher:
                    {
                        query = "SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS WHERE VOUCHER_SUB_TYPE = 'TDS' AND CLIENT_CODE = 'TDS' AND CLIENT_REFERENCE_ID > 0 AND STATUS = 1 AND VOUCHER_ID = ?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.TDSPartyPaymentDetail.CheckIsTDSPaymentVoucher:
                    {
                        query = "SELECT VMT.VOUCHER_ID\n" +
                                "  FROM VOUCHER_MASTER_TRANS VMT\n" +
                                " INNER JOIN TDS_PAYMENT TP\n" +
                                "    ON VMT.VOUCHER_ID = TP.VOUCHER_ID\n" +
                                "   AND VMT.VOUCHER_ID IN (?VOUCHER_ID)\n" +
                                "   AND IS_DELETED = 1\n" +
                                "   AND VMT.STATUS = 1;";

                        break;
                    }

                case SQLCommand.TDSPartyPaymentDetail.CheckIsTDSBookingVoucher:
                    {
                        query = "SELECT TB.VOUCHER_ID\n" +
                                "  FROM VOUCHER_MASTER_TRANS VMT\n" +
                                " INNER JOIN TDS_BOOKING TB\n" +
                                "    ON VMT.VOUCHER_ID = TB.VOUCHER_ID\n" +
                                "   AND TB.VOUCHER_ID IN (?VOUCHER_ID)\n" +
                                "   AND IS_DELETED = 1\n" +
                                "   AND VMT.STATUS = 1;";

                        break;
                    }
            }

            return query;
        }
    }
}
