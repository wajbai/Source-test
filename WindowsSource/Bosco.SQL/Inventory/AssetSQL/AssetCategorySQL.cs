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
        #region ISQLServerQuery Members
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
#endregion

        #region SQL Script
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
                case SQLCommand.AssetCategory.FetchSelectedCategoryDetails:
                    {
                        query ="SELECT AI.ITEM_ID,\n" +
                                "       AI.ASSET_NAME AS NAME,\n" + 
                                "       AG.GROUP_NAME AS GROUP_NAME,\n" + 
                                "       AC.NAME       AS CATEGORY,\n" + 
                                "       UM.SYMBOL     AS UNIT,\n" + 
                                "       AP.QUANTITY,\n" +
                                "       AC.CATEGORY_ID\n" + 
                                "   FROM ASSET_CATEGORY AC\n" + 
                                " INNER JOIN ASSET_ITEM AI\n" + 
                                "    ON AC.CATEGORY_ID = AI.CATEGORY_ID\n" + 
                                " INNER JOIN ASSET_STOCK_UNITOFMEASURE UM\n" + 
                                "    ON AI.UNIT_ID = UM.UNIT_ID\n" +
                                " INNER JOIN ASSET_PURCHASE_DETAIL AP\n" +
                                "    ON AI.ITEM_ID=AP.ITEM_ID\n" +
                                " INNER JOIN ASSET_GROUP AG\n" + 
                                "    ON AI.ASSET_GROUP_ID = AG.GROUP_ID\n" + 
                                " WHERE AC.CATEGORY_ID IN (?CATEGORY_ID)";
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}
