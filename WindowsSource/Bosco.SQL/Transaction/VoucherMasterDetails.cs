using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class VoucherMasterDetails : IDatabaseQuery
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
                query = GetBankSQL();
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
        private string GetBankSQL()
        {
            string query = "";
            SQLCommand.VoucherMasterDetails sqlCommandId = (SQLCommand.VoucherMasterDetails)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.VoucherMasterDetails.Add:
                    {
                        query = "INSERT INTO MASTER_BANK ( " +
                               "BANK_CODE, " +
                               "BANK, " +
                               "BRANCH, " +
                               "ADDRESS," +
                               "IFSCCODE," +
                               "MICRCODE," +
                               "CONTACTNUMBER," +
                               "SWIFTCODE," +
                               "NOTES ) VALUES( " +
                               "?BANK_CODE, " +
                               "?BANK, " +
                               "?BRANCH, " +
                               "?ADDRESS," +
                               "?IFSCCODE," +
                               "?MICRCODE," +
                               "?CONTACTNUMBER," +
                               "?SWIFTCODE," +
                               "?NOTES)";
                        break;
                    }
                case SQLCommand.VoucherMasterDetails.Update:
                    {
                        query = "UPDATE MASTER_BANK SET " +
                                    "BANK_CODE = ?BANK_CODE, " +
                                    "BANK =?BANK, " +
                                    "BRANCH =?BRANCH, " +
                                    "ADDRESS=?ADDRESS, " +
                                    "IFSCCODE=?IFSCCODE," +
                                    "MICRCODE=?MICRCODE," +
                                    "CONTACTNUMBER=?CONTACTNUMBER ," +
                                    "SWIFTCODE=?SWIFTCODE," +
                                    "NOTES=?NOTES " +
                                    " WHERE BANK_ID=?BANK_ID ";
                        break;
                    }
                case SQLCommand.VoucherMasterDetails.Delete:
                    {
                        query = "DELETE FROM  MASTER_BANK WHERE BANK_ID=?BANK_ID";
                        break;
                    }
                case SQLCommand.VoucherMasterDetails.Fetch:
                    {
                        query = "SELECT " +
                                "BANK_ID, " +
                                "BANK_CODE, " +
                                "BANK, " +
                                "BRANCH, " +
                                "ADDRESS, " +
                                "IFSCCODE," +
                                "MICRCODE," +
                                "CONTACTNUMBER, " +
                                "SWIFTCODE," +
                                "NOTES " +
                            " FROM " +
                                "MASTER_BANK " +
                                " WHERE BANK_ID=?BANK_ID  ";
                        break;
                    }
                case SQLCommand.VoucherMasterDetails.FetchAll:
                    {
                        query = "SELECT " +
                                "BANK_ID, " +
                                "BANK_CODE, " +
                                "BANK, " +
                                "BRANCH, " +
                                "ADDRESS, " +
                                "IFSCCODE," +
                                "MICRCODE," +
                                "CONTACTNUMBER ," +
                                "SWIFTCODE " +
                            " FROM" +
                                " MASTER_BANK ORDER BY BANK_CODE ASC";
                        break;
                    }
            }

            return query;
        }
        #endregion Bank SQL
    }
}
