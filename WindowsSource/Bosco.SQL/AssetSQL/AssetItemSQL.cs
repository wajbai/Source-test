using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.DAO;


namespace Bosco.SQL
{
    public class AssetItemSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.AssetItem).FullName)
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
            SQLCommand.AssetItem SqlcommandId = (SQLCommand.AssetItem)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.AssetItem.Add:
                    {
                        query = "INSERT INTO ASSET_ITEM (" +
                                "ASSET_GROUP_ID," +
                                "DEPRECIATION_LEDGER_ID," +
                                "DISPOSAL_LEDGER_ID," +
                                "ACCOUNT_LEDGER_ID," +
                                "CATEGORY_ID," +
                                "NAME," +
                                "ITEM_KIND," +
                                "UNIT_ID," +
                                "METHOD," +
                                "PREFIX," +
                                "SUFFIX," +
                                "STARTING_NO ," +
                                "QUANTITY," +
                                "RATE_PER_ITEM," +
                                "TOTAL)VALUES(" +
                                "?ASSET_GROUP_ID," +
                                "?DEPRECIATION_LEDGER_ID," +
                                "?DISPOSAL_LEDGER_ID," +
                                "?ACCOUNT_LEDGER_ID," +
                                "?CATEGORY_ID," +
                                "?NAME," +
                                "?ITEM_KIND," +
                                "?UNIT_ID," +
                                "?METHOD," +
                                "?PREFIX," +
                                "?SUFFIX," +
                                "?STARTING_NO ," +
                                "?QUANTITY," +
                                "?RATE_PER_ITEM," +
                                "?TOTAL)";
                        break;
                    }
                case SQLCommand.AssetItem.Delete:
                    {
                        query = "DELETE FROM ASSET_ITEM WHERE ITEM_ID=?ITEM_ID";
                        break;
                    }
                case SQLCommand.AssetItem.FetchAll:
                    {
                        query = "SELECT ITEM_ID,\n" +
                                 "       AG.NAME        AS ASSET_GROUP,\n" +
                                 "       AC.NAME        AS CATEGORY,\n" +
                                 "       AU.SYMBOL      AS UNIT,\n" +
                                 "       ITEM_KIND,\n" +
                                 "       AI.NAME        AS NAME,\n" +
                                 "       METHOD,\n" +
                                 "       PREFIX,\n" +
                                 "       SUFFIX,\n" +
                                 "       STARTING_NO,\n" +
                                 "       QUANTITY,\n" +
                                 "       RATE_PER_ITEM,\n" +
                                 "       TOTAL,\n" +
                                 "       ML.LEDGER_NAME AS DEPRECIATION_LEDGER,\n" +
                                 "       DP.LEDGER_NAME AS DISPOSAL_LEDGER,\n" +
                                 "       AL.LEDGER_NAME AS ACCOUNT_LEDGER\n" +
                                 "  FROM ASSET_ITEM AI\n" +
                                 " INNER JOIN ASSET_GROUP AG\n" +
                                 "    ON AG.GROUP_ID = AI.ASSET_GROUP_ID\n" +
                                 " INNER JOIN ASSET_CATEGORY AC\n" +
                                 "    ON AC.CATEGORY_ID = AI.CATEGORY_ID\n" +
                                 " INNER JOIN ASSET_UNITOFMEASURE AU\n" +
                                 "    ON AU.UNIT_ID = AI.UNIT_ID\n" +
                                 " INNER JOIN MASTER_LEDGER ML\n" +
                                 "    ON ML.LEDGER_ID = AI.DEPRECIATION_LEDGER_ID\n" +
                                 " INNER JOIN MASTER_LEDGER DP\n" +
                                 "    ON DP.LEDGER_ID = AI.DISPOSAL_LEDGER_ID\n" +
                                 " INNER JOIN MASTER_LEDGER AL\n" +
                                 "    ON AL.LEDGER_ID = AI.ACCOUNT_LEDGER_ID;";
                        break;
                    }
                case SQLCommand.AssetItem.Update:
                    {
                        query = "UPDATE ASSET_ITEM\n" +
                                "   SET ASSET_GROUP_ID         = ?ASSET_GROUP_ID,\n" +
                                "       DEPRECIATION_LEDGER_ID = ?DEPRECIATION_LEDGER_ID,\n" +
                                "       DISPOSAL_LEDGER_ID     = ?DISPOSAL_LEDGER_ID,\n" +
                                "       ACCOUNT_LEDGER_ID      = ?ACCOUNT_LEDGER_ID,\n" +
                                "       CATEGORY_ID            = ?CATEGORY_ID,\n" +
                                "       ITEM_KIND              = ?ITEM_KIND,\n" +
                                "       NAME                   = ?NAME,\n" +
                                "       UNIT_ID                = ?UNIT_ID,\n" +
                                "       METHOD                 = ?METHOD,\n" +
                                "       PREFIX                 = ?PREFIX,\n" +
                                "       SUFFIX                 = ?SUFFIX,\n" +
                                "       STARTING_NO            = ?STARTING_NO,\n" +
                                "       QUANTITY               = ?QUANTITY,\n" +
                                "       RATE_PER_ITEM          = ?RATE_PER_ITEM,\n" +
                                "       TOTAL                  = ?TOTAL\n" +
                                " WHERE ITEM_ID = ?ITEM_ID;";
                        break;
                    }
                case SQLCommand.AssetItem.Fetch:
                    {
                        query = "SELECT ITEM_ID,\n" +
                                    "       ML.LEDGER_NAME AS LEDGER,\n" +
                                    "       ASSET_GROUP_ID,\n" +
                                    "       DEPRECIATION_LEDGER_ID,\n" +
                                    "       DISPOSAL_LEDGER_ID,\n" +
                                    "       ACCOUNT_LEDGER_ID,\n" +
                                    "       CATEGORY_ID,\n" +
                                    "       ITEM_KIND,\n" +
                                    "       NAME,\n" +
                                    "       UNIT_ID,\n" +
                                    "       METHOD,\n" +
                                    "       PREFIX,\n" +
                                    "       SUFFIX,\n" +
                                    "       STARTING_NO,\n" +
                                    "       QUANTITY,\n" +
                                    "       RATE_PER_ITEM,\n" +
                                    "       TOTAL\n" +
                                    "  FROM ASSET_ITEM AI\n" +
                                    " INNER JOIN MASTER_LEDGER ML\n" +
                                    "    ON ML.LEDGER_ID = AI.ACCOUNT_LEDGER_ID\n" +
                                    " WHERE ITEM_ID = ?ITEM_ID;";
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}
