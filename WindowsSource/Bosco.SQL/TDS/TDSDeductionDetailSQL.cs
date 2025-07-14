using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    class TDSDeductionDetailSQL : IDatabaseQuery
    {
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        public string GetQuery(DAO.Data.DataCommandArguments dataCommandArgs, ref DAO.Data.SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.TDSDeductionDetail).FullName)
            {
                query = GetDeductionDetail();
            }

            sqlType = this.sqlType;
            return query;
        }

        #region SQL Script

        private string GetDeductionDetail()
        {
            string query = "";
            SQLCommand.TDSDeductionDetail sqlCommandId = (SQLCommand.TDSDeductionDetail)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.TDSDeductionDetail.Add:
                    {
                        query = "INSERT INTO TDS_DEDUCTION_DETAIL\n" +
                                "  (DEDUCTION_ID, BOOKING_DETAIL_ID, TAX_LEDGER_ID, TAX_AMOUNT)\n" +
                                "VALUES\n" +
                                "  (?DEDUCTION_ID, ?BOOKING_DETAIL_ID, ?TAX_LEDGER_ID, ?TAX_AMOUNT)";
                        break;
                    }
                case SQLCommand.TDSDeductionDetail.Update:
                    {
                        query = "UPDATE TDS_DEDUCTION_DETAIL\n" +
                                "   SET DEDUCTION_ID      = ?DEDUCTION_ID,\n" +
                                "       BOOKING_DETAIL_ID = ?BOOKING_DETAIL_ID,\n" +
                                "       TAX_LEDGER_ID     = ?TAX_LEDGER_ID,\n" +
                                "       TAX_AMOUNT        = ?TAX_AMOUNT\n" +
                                " WHERE DEDUCTION_DETAIL_ID = ?DEDUCTION_DETAIL_ID";
                        break;
                    }
                case SQLCommand.TDSDeductionDetail.Delete:
                    {
                        query = "DELETE FROM TDS_DEDUCTION_DETAIL WHERE DEDUCTION_ID=?DEDUCTION_ID";
                        break;
                    }
                case SQLCommand.TDSDeductionDetail.FetchAll:
                    {
                        query = "SELECT DEDUCTION_DETAIL_ID,\n" +
                                "       DEDUCTION_ID,\n" +
                                "       BOOKING_DETAIL_ID,\n" +
                                "       TAX_LEDGER_ID,\n" +
                                "       TAX_AMOUNT\n" +
                                "  FROM TDS_DEDUCTION_DETAIL";
                        break;
                    }
                case SQLCommand.TDSDeductionDetail.Fetch:
                    {
                        query = "SELECT DEDUCTION_DETAIL_ID,\n" +
                                "       DEDUCTION_ID,\n" +
                                "       BOOKING_DETAIL_ID,\n" +
                                "       TAX_LEDGER_ID,\n" +
                                "       TAX_AMOUNT\n" +
                                "  FROM TDS_DEDUCTION_DETAIL\n" +
                                " WHERE DEDUCTION_DETAIL_ID = ?DEDUCTION_DETAIL_ID";
                        break;
                    }
            }
            return query;
        }
        #endregion TDS SQL
    }
}
