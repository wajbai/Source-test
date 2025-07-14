using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class AssetMappingSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.AssetMapping).FullName)
            {
                query = GetSettingSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        #region SQL Script
        private string GetSettingSQL()
        {
            string Query = string.Empty;
            SQLCommand.AssetMapping sqlCommandId = (SQLCommand.AssetMapping)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                case SQLCommand.AssetMapping.MapLocation:
                    {
                        Query = "INSERT INTO ASSET_PROJECT_LOCATION\n" +
                                "  (PROJECT_ID, LOCATION_ID)\n" +
                                "VALUES\n" +
                                "  (?PROJECT_ID, ?LOCATION_ID);";
                        break;
                    }
                case SQLCommand.AssetMapping.DeleteMapping:
                    {
                        Query = "DELETE FROM ASSET_PROJECT_LOCATION WHERE PROJECT_ID=?PROJECT_ID AND LOCATION_ID = ?LOCATION_ID;";
                        break;
                    }
                case SQLCommand.AssetMapping.DeleteMapLocation:
                    {
                        Query = "DELETE FROM ASSET_PROJECT_LOCATION WHERE LOCATION_ID=?LOCATION_ID ;";
                        break;
                    }
            }
            return Query;
        }
        #endregion
    }
}
