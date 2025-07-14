using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    class TDSBookingDetailSQL : IDatabaseQuery
    {
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        public string GetQuery(DAO.Data.DataCommandArguments dataCommandArgs, ref DAO.Data.SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.TDSBookingDetail).FullName)
            {
                query = GetBookingDetail();
            }

            sqlType = this.sqlType;
            return query;
        }

        #region SQL Script

        private string GetBookingDetail()
        {
            string query = "";
            SQLCommand.TDSBookingDetail sqlCommandId = (SQLCommand.TDSBookingDetail)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.TDSBookingDetail.Add:
                    {
                        query = "INSERT INTO TDS_BOOKING_DETAIL\n" +
                                "  (BOOKING_ID, NATURE_OF_PAYMENT_ID, ASSESS_AMOUNT, IS_TDS_DEDUCTED)\n" +
                                "VALUES\n" +
                                "  (?BOOKING_ID,\n" +
                                "   ?NATURE_OF_PAYMENT_ID,\n" +
                                "   ?ASSESS_AMOUNT,\n" +
                                "   ?IS_TDS_DEDUCTED)";
                        break;
                    }

                case SQLCommand.TDSBookingDetail.Update:
                    {
                        query = "UPDATE TDS_BOOKING_DETAIL\n" +
                                "   SET BOOKING_ID           = ?BOOKING_ID,\n" +
                                "       NATURE_OF_PAYMENT_ID = ?NATURE_OF_PAYMENT_ID,\n" +
                                "       ASSESS_AMOUNT        = ?ASSESS_AMOUNT,\n" +
                                "       IS_TDS_DEDUCTED      = ?IS_TDS_DEDUCTED\n" +
                                " WHERE BOOKING_DETAIL_ID = ?BOOKING_DETAIL_ID";
                        break;
                    }
                case SQLCommand.TDSBookingDetail.Delete:
                    {
                        query = "DELETE FROM TDS_BOOKING_DETAIL WHERE BOOKING_ID=?BOOKING_ID";
                        break;
                    }
                case SQLCommand.TDSBookingDetail.FetchAll:
                    {
                        query = "SELECT BOOKING_DETAIL_ID,\n" +
                                "       BOOKING_ID,\n" +
                                "       NATURE_OF_PAYMENT_ID,\n" +
                                "       ASSESS_AMOUNT,\n" +
                                "       IS_TDS_DEDUCTED\n" +
                                "  FROM TDS_BOOKING_DETAIL\n" +
                                " ORDER BY BOOKING_ID ASC";
                        break;
                    }
                case SQLCommand.TDSBookingDetail.Fetch:
                    {
                        query = "SELECT BOOKING_DETAIL_ID,\n" +
                                "       BOOKING_ID,\n" +
                                "       NATURE_OF_PAYMENT_ID,\n" +
                                "       ASSESS_AMOUNT,\n" +
                                "       IS_TDS_DEDUCTED\n" +
                                "  FROM TDS_BOOKING_DETAIL\n" +
                                " WHERE BOOKING_DETAIL_ID = ?BOOKING_DETAIL_ID";
                        break;
                    }
                case SQLCommand.TDSBookingDetail.UpdateTaxDeductStatus:
                    {
                        query = "UPDATE TDS_BOOKING_DETAIL SET  IS_TDS_DEDUCTED=?IS_TDS_DEDUCTED WHERE BOOKING_DETAIL_ID=?BOOKING_DETAIL_ID";
                        break;
                    }
            }
            return query;
        }
        #endregion TDS SQL
    }
}
