using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.DAO;

namespace Bosco.SQL
{
    public class StatisticsTypeSQL : IDatabaseQuery
    {
        #region ISQLServerQueryMembers
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;
        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.StatisticsType).FullName)
            {
                query = GetStatisticsTypeSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        public string GetStatisticsTypeSQL()
        {
            string query = "";
            SQLCommand.StatisticsType sqlCommandId = (SQLCommand.StatisticsType)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                case SQLCommand.StatisticsType.Add:
                    {
                        query = "INSERT INTO MASTER_STATISTICS_TYPE " +
                               "(STATISTICS_TYPE) VALUES(?STATISTICS_TYPE) ";
                        break;
                    }
                case SQLCommand.StatisticsType.Update:
                    {
                        query = "UPDATE MASTER_STATISTICS_TYPE SET STATISTICS_TYPE=?STATISTICS_TYPE " +
                                    "WHERE STATISTICS_TYPE_ID=?STATISTICS_TYPE_ID";
                        break;
                    }

                case SQLCommand.StatisticsType.Delete:
                    {
                        query = "DELETE FROM MASTER_STATISTICS_TYPE WHERE STATISTICS_TYPE_ID=?STATISTICS_TYPE_ID";
                        break;
                    }

                case SQLCommand.StatisticsType.Fetch:
                    {
                        query = "SELECT STATISTICS_TYPE_ID, STATISTICS_TYPE " +
                               "FROM MASTER_STATISTICS_TYPE " +
                               "WHERE STATISTICS_TYPE_ID=?STATISTICS_TYPE_ID";
                        break;
                    }

                case SQLCommand.StatisticsType.FetchAll:
                    {
                        query = "SELECT STATISTICS_TYPE_ID, STATISTICS_TYPE "+
                               "FROM MASTER_STATISTICS_TYPE " +
                               "ORDER BY STATISTICS_TYPE ASC";
                               //"ORDER BY STATISTICS_TYPE_ID ASC";
                        break;
                    }
            }
            return query;
        }
    }
}