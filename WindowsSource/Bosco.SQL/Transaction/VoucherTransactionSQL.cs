using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class VoucherTransactionSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.VoucherMasterDetails).FullName)
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
            SQLCommand.VoucherMasterDetails sqlCommandId = (SQLCommand.VoucherMasterDetails)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.VoucherMasterDetails.Add:
                    {
                        query = "INSERT INTO VOUCHER_TRANSACTION ( " +
                               "VOUCHER_ID, " +
                               "SEQUENCE_NO, " +
                               "LEDGER_ID, " +
                               "AMOUNT," +
                               "TRANS_MODE," +
                               "LEDGER_FLAG," +
                               "CHEQUE_NO," +
                               "MATERIALIZED_ON," +
                               "STATUS ) VALUES( " +
                               "?VOUCHER_ID, " +
                               "?SEQUENCE_NO, " +
                               "?LEDGER_ID, " +
                               "?AMOUNT," +
                               "?TRANS_MODE," +
                               "?LEDGER_FLAG," +
                               "?CHEQUE_NO," +
                               "?MATERIALIZED_ON," +
                               "?STATUS)";
                        break;
                    }
                case SQLCommand.VoucherMasterDetails.Delete:
                    {
                        query = "DELETE FROM VOUCHER_TRANSACTION WHERE VOUCHER_ID=?VOUCHER_ID ";
                        break;
                    }
                case SQLCommand.VoucherMasterDetails.Fetch:
                    {
                        query = "";
                        break;
                    }
                case SQLCommand.VoucherMasterDetails.FetchAll:
                    {
                        query = " SELECT VOUCHER_ID, ML.LEDGER_NAME ,AMOUNT ,CASE TRANS_MODE WHEN 'CR' THEN 'Credit' ELSE 'Debit' END AS TRANSMODE," +
                               "LEDGER_FLAG, MB.ACCOUNT_NUMBER, " +
                               " CHEQUE_NO ,MATERIALIZED_ON FROM VOUCHER_TRANSACTION AS VT " +
                               " INNER JOIN MASTER_LEDGER AS ML ON VT.LEDGER_ID =ML.LEDGER_ID " +
                               " LEFT JOIN MASTER_BANK_ACCOUNT AS MB ON " +
                               " ML.BANK_ACCOUNT_ID=MB.BANK_ACCOUNT_ID;";
                        break;
                    }
            }

            return query;
        }
        #endregion Bank SQL
    }
}
