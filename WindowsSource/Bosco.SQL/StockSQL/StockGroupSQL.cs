using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class StockGroupSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.StockGroup).FullName)
            {
                query = GetStockGroupSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        #region SQL Script
        public string GetStockGroupSQL()
        {
            string query = "";
            SQLCommand.StockGroup SqlcommandId = (SQLCommand.StockGroup)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.StockGroup.Add:
                    {
                        query = "INSERT INTO STOCK_GROUP (" +
                                "NAME," +
                                "PARENT_GROUP_ID)VALUES(" +
                                "?NAME, " +
                                "?PARENT_GROUP_ID)";
                        break;
                    }
                case SQLCommand.StockGroup.Update:
                    {
                        query = "UPDATE STOCK_GROUP SET " +
                                "NAME=?NAME," +
                                "PARENT_GROUP_ID=?PARENT_GROUP_ID " +
                                "WHERE GROUP_ID =?GROUP_ID";
                        break;
                    }
                case SQLCommand.StockGroup.FetchAll:
                    {
                        query = "SELECT GROUP_ID,PARENT_GROUP_ID, NAME FROM STOCK_GROUP";
                        break;
                    }

                case SQLCommand.StockGroup.FetchbyID:
                    {
                        query = "SELECT GROUP_ID, NAME, PARENT_GROUP_ID\n" +
                                "  FROM STOCK_GROUP\n" +
                                " WHERE GROUP_ID = ?GROUP_ID";
                        break;
                    }
                case SQLCommand.StockGroup.Delete:
                    {
                        query = "DELETE FROM STOCK_GROUP WHERE GROUP_ID =?GROUP_ID";
                        break;
                    }

                case SQLCommand.StockGroup.FetchSelectedGroups:
                    {
                        query = "SELECT AI.ASSET_GROUP_ID,AI.NAME AS 'NAME', AG.NAME AS 'GROUP_NAME'\n" +
                                    "  FROM ASSET_ITEM AI INNER JOIN STOCK_GROUP AG ON AI.ASSET_GROUP_ID=AG.GROUP_ID\n" +
                                    "  JOIN (SELECT AG.GROUP_ID, '' AS L\n" +
                                    "          FROM STOCK_GROUP AG\n" +
                                    "         INNER JOIN ASSET_ITEM AI\n" +
                                    "            ON AI.ASSET_GROUP_ID = AG.GROUP_ID\n" +
                                    "         WHERE AI.ASSET_GROUP_ID IN (?GROUP_ID)\n" +
                                    "        UNION\n" +
                                    "       SELECT GROUP_ID, '' AS L\n" +
                                    "          FROM STOCK_GROUP\n" +
                                    "         WHERE GROUP_ID IN\n" +
                                    "               (SELECT GROUP_ID\n" +
                                    "                  FROM STOCK_GROUP AG\n" +
                                    "                 WHERE AG.GROUP_ID IN (?GROUP_ID))\n" +
                                    "        UNION\n" +
                                    "        SELECT GROUP_ID, '' AS L\n" +
                                    "          FROM STOCK_GROUP AG2\n" +
                                    "         WHERE AG2.GROUP_ID IN (?GROUP_ID)) AS T\n" +
                                    "    ON T.GROUP_ID = AI.ASSET_GROUP_ID AND AG.GROUP_ID = AI.ASSET_GROUP_ID ORDER BY AI.NAME;";
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}
