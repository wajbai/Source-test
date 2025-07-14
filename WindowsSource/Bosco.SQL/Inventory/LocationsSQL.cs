using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL.Inventory
{
    class LocationsSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.AssetLocation).FullName)
            {
                query = GetLocationSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the Location details.
        /// </summary>
        /// <returns></returns>
        private string GetLocationSQL()
        {
            string query = "";
            SQLCommand.AssetLocation sqlCommandId = (SQLCommand.AssetLocation)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.AssetLocation.Add:
                    {
                        query = "INSERT INTO ASSET_LOCATION\n" +
                                "  (LOCATION_ID, NAME, PARENT_ID)\n" +
                                "VALUES\n" +
                                "  (?LOCATION_ID, ?NAME, ?PARENT_ID);";
                        break;
                    }

                case SQLCommand.AssetLocation.Update:
                    {
                        query = "UPDATE ASSET_LOCATION\n" +
                                "   SET LOCATION_ID   = ?LOCATION_ID,\n" +
                                "       NAME          = ?NAME,\n" +
                                "       PARENT_ID       = ?PARENT_ID,\n" +
                            //"       LOCATION_TYPE = ?LOCATION_TYPE\n" + 
                                " WHERE LOCATION_ID = ?LOCATION_ID";
                        break;
                    }

                case SQLCommand.AssetLocation.Delete:
                    {
                        query = "DELETE FROM ASSET_LOCATION WHERE LOCATION_ID = ?LOCATION_ID";
                        break;
                    }

                case SQLCommand.AssetLocation.Fetch:
                    {
                        query = "SELECT LOCATION_ID, NAME, PARENT_ID\n" +
                              "  FROM ASSET_LOCATION\n" +
                              " WHERE LOCATION_ID = ?LOCATION_ID";
                        break;
                    }

                case SQLCommand.AssetLocation.FetchAll:
                    {
                        query = "SELECT LOCATION_ID, NAME, PARENT_ID FROM ASSET_LOCATION";
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}