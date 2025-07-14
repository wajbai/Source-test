using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.DAO;

namespace Bosco.SQL
{
    class ServiceSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.AssetService).FullName)
            {
                query = GetServiceSQL();
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
        private string GetServiceSQL()
        {
            string query = "";
            SQLCommand.AssetService sqlCommandId = (SQLCommand.AssetService)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.AssetService.Add:
                    {
                        query = "INSERT INTO ASSET_SERVICE (" +
                            "SERVICE_CODE," +
                            "NAME," +
                            "DESCRIPTION) VALUES(" +
                            "?SERVICE_CODE," +
                            "?NAME," +
                            "?DESCRIPTION)";
                        break;
                    }

                case SQLCommand.AssetService.Update:
                    {
                        query = "UPDATE ASSET_SERVICE SET SERVICE_ID=?SERVICE_ID, SERVICE_CODE=?SERVICE_CODE,NAME=?NAME,DESCRIPTION=?DESCRIPTION WHERE SERVICE_ID=?SERVICE_ID";
                        break;
                    }
                case SQLCommand.AssetService.FetchAll:
                    {
                        query = "SELECT SERVICE_ID, SERVICE_CODE, NAME, DESCRIPTION FROM ASSET_SERVICE";
                        break;
                    }

                case SQLCommand.AssetService.Delete:
                    {
                        query="DELETE FROM ASSET_SERVICE WHERE SERVICE_ID = ?SERVICE_ID";
                        break;
                    }
                case SQLCommand.AssetService.Fetch:
                    {
                        query = "SELECT SERVICE_ID, SERVICE_CODE, NAME, DESCRIPTION\n" +
                                "  FROM ASSET_SERVICE\n" + 
                                " WHERE SERVICE_ID = ?SERVICE_ID";
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}