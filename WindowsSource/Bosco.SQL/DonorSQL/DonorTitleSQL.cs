using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class DonorTitleSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.DonorTitle).FullName)
            {
                query = GetDonorTitleSQL();
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
        private string GetDonorTitleSQL()
        {
            string query = "";
            SQLCommand.DonorTitle sqlCommandId = (SQLCommand.DonorTitle)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.DonorTitle.Add:
                    {
                        query = "INSERT INTO MASTER_DONOR_TITLE(TITLE)VALUES(?TITLE)";
                        break;

                    }
                case SQLCommand.DonorTitle.Update:
                    {
                        query = "UPDATE MASTER_DONOR_TITLE SET TITLE=?TITLE WHERE TITLE_ID=?TITLE_ID";
                        break;

                    }

                case SQLCommand.DonorTitle.Delete:
                    {
                        query = "DELETE FROM MASTER_DONOR_TITLE WHERE TITLE_ID=?TITLE_ID";
                        break;

                    }

                case SQLCommand.DonorTitle.Fetch:
                    {
                        query = "SELECT TITLE FROM MASTER_DONOR_TITLE WHERE TITLE_ID=?TITLE_ID";
                        break;

                    }
                case SQLCommand.DonorTitle.FetchAll:
                    {
                        query = "SELECT TITLE_ID,TITLE FROM MASTER_DONOR_TITLE ORDER BY TITLE ASC";
                        break;

                    }
            }

            return query;
        }
        #endregion
    }
}

