using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.DAO;

namespace Bosco.SQL
{
    public class AssetGroupSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.AssetGroup).FullName)
            {
                query = GetgroupSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        #region SQL Script
        public string GetgroupSQL()
        {
            string query = "";
            SQLCommand.AssetGroup SqlcommandId = (SQLCommand.AssetGroup)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.AssetGroup.Add:
                    {
                        query = "INSERT INTO ASSET_GROUP (" +
                                "NAME," +
                                "PARENT_GROUP_ID," +
                                "DEP_ID," +
                                "DEP_PERCENTAGE," +
                                "IMAGE_ID)VALUES(" +
                                "?NAME, " +
                                "?PARENT_GROUP_ID," +
                                "?DEP_ID," +
                                "?DEP_PERCENTAGE," +
                                "?IMAGE_ID)";
                        break;
                    }
                case SQLCommand.AssetGroup.Update:
                    {
                        query = "UPDATE ASSET_GROUP SET " +
                                "NAME=?NAME," +
                                "PARENT_GROUP_ID=?PARENT_GROUP_ID," +
                                "DEP_ID=?DEP_ID," +
                                "DEP_PERCENTAGE=?DEP_PERCENTAGE, " +
                                "IMAGE_ID=?IMAGE_ID " +
                                "WHERE GROUP_ID =?GROUP_ID";
                        break;
                    }
                case SQLCommand.AssetGroup.FetchAll:
                    {
                        query = "SELECT GROUP_ID,PARENT_GROUP_ID, NAME, IMAGE_ID FROM ASSET_GROUP ";
                        break;
                    }
                case SQLCommand.AssetGroup.FetchSelectedGroups:
                    {
                        query = "SELECT AI.ASSET_GROUP_ID,AI.NAME AS 'NAME', AG.NAME AS 'GROUP_NAME'\n" +
                                    "  FROM ASSET_ITEM AI INNER JOIN ASSET_GROUP AG ON AI.ASSET_GROUP_ID=AG.GROUP_ID\n" +
                                    "  JOIN (SELECT AG.GROUP_ID, '' AS L\n" +
                                    "          FROM ASSET_GROUP AG\n" +
                                    "         INNER JOIN ASSET_ITEM AI\n" +
                                    "            ON AI.ASSET_GROUP_ID = AG.GROUP_ID\n" +
                                    "         WHERE AI.ASSET_GROUP_ID IN (?GROUP_ID)\n" +
                                    "        UNION\n" +
                                    "       SELECT GROUP_ID, '' AS L\n" +
                                    "          FROM ASSET_GROUP\n" +
                                    "         WHERE GROUP_ID IN\n" +
                                    "               (SELECT GROUP_ID\n" +
                                    "                  FROM ASSET_GROUP AG\n" +
                                    "                 WHERE AG.GROUP_ID IN (?GROUP_ID))\n" +
                                    "        UNION\n" +
                                    "        SELECT GROUP_ID, '' AS L\n" +
                                    "          FROM ASSET_GROUP AG2\n" +
                                    "         WHERE AG2.GROUP_ID IN (?GROUP_ID)) AS T\n" +
                                    "    ON T.GROUP_ID = AI.ASSET_GROUP_ID AND AG.GROUP_ID = AI.ASSET_GROUP_ID ORDER BY AI.NAME;";
                        break;
                    }
                case SQLCommand.AssetGroup.Delete:
                    {
                        query = "DELETE FROM ASSET_GROUP WHERE GROUP_ID=?GROUP_ID OR PARENT_GROUP_ID =?GROUP_ID";
                        break;
                    }
                case SQLCommand.AssetGroup.FetchbyID:
                    {
                        query = "SELECT GROUP_ID, NAME, PARENT_GROUP_ID, DEP_ID, DEP_PERCENTAGE, IMAGER_ID\n" +
                                "  FROM ASSET_GROUP\n" +
                                " WHERE GROUP_ID = ?GROUP_ID";
                        break;
                    }
            }
            return query;

        }



        #endregion
    }
}
