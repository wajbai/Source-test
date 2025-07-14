using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.DAO;

namespace Bosco.SQL
{
    class AssetCategorySQL : IDatabaseQuery
    {
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.AssetCategory).FullName)
            {
                query = GetgroupSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        public string GetgroupSQL()
        {
            string query = "";
            SQLCommand.AssetCategory SqlcommandId = (SQLCommand.AssetCategory)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.AssetCategory.Add:
                    {
                        query = "INSERT INTO ASSET_CATEGORY\n" +
                                "  (NAME,PARENT_CATEGORY_ID , IMAGE_ID)\n" +
                                "VALUES\n" +
                                "  (?NAME, ?PARENT_CATEGORY_ID, ?IMAGE_ID)";

                        break;
                    }
                case SQLCommand.AssetCategory.Update:
                    {
                        query = "UPDATE ASSET_CATEGORY\n" +
                                "   SET NAME = ?NAME, PARENT_CATEGORY_ID=?PARENT_CATEGORY_ID\n" +
                                " WHERE CATEGORY_ID =?CATEGORY_ID";
                        break;
                    }
                case SQLCommand.AssetCategory.Delete:
                    {
                        query = "DELETE FROM ASSET_CATEGORY WHERE CATEGORY_ID=?CATEGORY_ID OR PARENT_CATEGORY_ID=?CATEGORY_ID";
                        break;
                    }
                case SQLCommand.AssetCategory.Fetch:
                    {
                        query = "SELECT CATEGORY_ID, NAME, PARENT_CATEGORY_ID\n" +
                                "  FROM ASSET_CATEGORY\n" +
                                " WHERE CATEGORY_ID = ?CATEGORY_ID";
                        break;
                    }
                case SQLCommand.AssetCategory.FetchAll:
                    {
                        query = "SELECT NAME,PARENT_CATEGORY_ID,CATEGORY_ID,IMAGE_ID FROM ASSET_CATEGORY";
                        break;
                    }
                case SQLCommand.AssetCategory.FetchSelectedGroups:
                    {
                        query = "SELECT AI.ITEM_ID, AI.NAME,AC.NAME AS 'GROUP_NAME' FROM ASSET_CATEGORY AC " +
                                   "INNER JOIN ASSET_ITEM AI ON AC.CATEGORY_ID = AI.CATEGORY_ID " +
                                   "WHERE AC.CATEGORY_ID IN (?CATEGORY_ID)";
                        break;
                    }
            }
            return query;
        }
    }
}
