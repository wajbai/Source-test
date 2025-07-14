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
                                "GROUP_NAME," +
                                "PARENT_GROUP_ID)VALUES(" +
                                "?GROUP_NAME, " +
                                "?PARENT_GROUP_ID)";
                        break;
                    }
                case SQLCommand.StockGroup.Update:
                    {
                        query = "UPDATE STOCK_GROUP SET " +
                                "GROUP_NAME=?GROUP_NAME," +
                                "PARENT_GROUP_ID=?PARENT_GROUP_ID " +
                                "WHERE GROUP_ID =?GROUP_ID";
                        break;
                    }
                case SQLCommand.StockGroup.FetchAll:
                    {
                        query = "SELECT GROUP_ID,PARENT_GROUP_ID, GROUP_NAME FROM STOCK_GROUP WHERE GROUP_NAME NOT IN ('Primary')";
                        break;
                    }

                case SQLCommand.StockGroup.FetchbyID:
                    {
                        query = "SELECT GROUP_ID, GROUP_NAME, PARENT_GROUP_ID\n" +
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

                        //query = "SELECT AI.ASSET_GROUP_ID,AI.NAME AS 'NAME', AG.NAME AS 'GROUP_NAME'\n" +
                        //            "  FROM ASSET_ITEM AI INNER JOIN STOCK_GROUP AG ON AI.ASSET_GROUP_ID=AG.GROUP_ID\n" +
                        //            "  JOIN (SELECT AG.GROUP_ID, '' AS L\n" +
                        //            "          FROM STOCK_GROUP AG\n" +
                        //            "         INNER JOIN ASSET_ITEM AI\n" +
                        //            "            ON AI.ASSET_GROUP_ID = AG.GROUP_ID\n" +
                        //            "         WHERE AI.ASSET_GROUP_ID IN (?GROUP_ID)\n" +
                        //            "        UNION\n" +
                        //            "       SELECT GROUP_ID, '' AS L\n" +
                        //            "          FROM STOCK_GROUP\n" +
                        //            "         WHERE GROUP_ID IN\n" +
                        //            "               (SELECT GROUP_ID\n" +
                        //            "                  FROM STOCK_GROUP AG\n" +
                        //            "                 WHERE AG.GROUP_ID IN (?GROUP_ID))\n" +
                        //            "        UNION\n" +
                        //            "        SELECT GROUP_ID, '' AS L\n" +
                        //            "          FROM STOCK_GROUP AG2\n" +
                        //            "         WHERE AG2.GROUP_ID IN (?GROUP_ID)) AS T\n" +
                        //            "    ON T.GROUP_ID = AI.ASSET_GROUP_ID AND AG.GROUP_ID = AI.ASSET_GROUP_ID ORDER BY AI.NAME;";


                        //query = "SELECT SI.GROUP_ID AS GROUP_ID,\n" +  //MODIFIED BY SALAMON
                        //        "       SI.NAME     AS 'NAME',\n" +
                        //        "       SG.GROUP_NAME     AS 'GROUP_NAME'\n" +
                        //        "  FROM STOCK_ITEM SI\n" +
                        //        " INNER JOIN STOCK_GROUP SG\n" +
                        //        "    ON SI.GROUP_ID = SG.GROUP_ID\n" +
                        //        "  JOIN (SELECT SG.GROUP_ID, '' AS L\n" +
                        //        "          FROM STOCK_GROUP SG\n" +
                        //        "         INNER JOIN STOCK_ITEM SI\n" +
                        //        "            ON SI.GROUP_ID = SG.GROUP_ID\n" +
                        //        "         WHERE SI.GROUP_ID IN (?GROUP_ID)\n" +
                        //        "        UNION\n" +
                        //        "        SELECT GROUP_ID, '' AS L\n" +
                        //        "          FROM STOCK_GROUP\n" +
                        //        "         WHERE GROUP_ID IN\n" +
                        //        "               (SELECT GROUP_ID FROM STOCK_GROUP SG WHERE SG.GROUP_ID IN (?GROUP_ID))\n" +
                        //        "        UNION\n" +
                        //        "        SELECT GROUP_ID, '' AS L\n" +
                        //        "          FROM STOCK_GROUP SG2\n" +
                        //        "         WHERE SG2.GROUP_ID IN (?GROUP_ID)) AS T\n" +
                        //        "    ON T.GROUP_ID = SI.GROUP_ID\n" +
                        //        "   AND SG.GROUP_ID = SI.GROUP_ID\n" +
                        //        " ORDER BY SI.NAME";

                        query = "SELECT SI.ITEM_ID,\n" +
                                "       SI.NAME,\n" +
                                "       T.QUANTITY,\n" +
                                "       SG.GROUP_NAME,\n" +
                                "       SGG.GROUP_NAME AS PARENT_GROUP\n" +
                                "  FROM STOCK_ITEM AS SI\n" +
                                "  LEFT JOIN STOCK_GROUP AS SG\n" +
                                "    ON SI.GROUP_ID = SG.GROUP_ID\n" +
                                "  LEFT JOIN STOCK_GROUP AS SGG\n" +
                                "    ON SG.PARENT_GROUP_ID = SGG.GROUP_ID\n" +
                                "  LEFT JOIN (SELECT BALANCE_DATE, ITEM_ID, QUANTITY\n" +
                                "               FROM INVENTORY_STOCK\n" +
                                "              WHERE QUANTITY NOT IN (0)\n" +
                                "              GROUP BY ITEM_ID\n" +
                                "              ORDER BY BALANCE_DATE DESC) AS T\n" +
                                "    ON SI.ITEM_ID = T.ITEM_ID\n" +
                                " WHERE SI.GROUP_ID IN (?GROUP_ID)\n" +
                                " GROUP BY ITEM_ID;";


                        break;
                    }

                case SQLCommand.StockGroup.FetchGroupId:
                    {
                        query = "SELECT GROUP_ID FROM STOCK_GROUP WHERE GROUP_NAME=?GROUP_NAME";
                        break;
                    }

                case SQLCommand.StockGroup.FetchParentGroupId:
                    {
                        query = "SELECT GROUP_ID FROM STOCK_GROUP WHERE GROUP_NAME=?GROUP_NAME";
                        break;
                    }

                case SQLCommand.StockGroup.DeleteStockGroupDetails:
                    {
                        query = "DELETE FROM STOCK_GROUP WHERE GROUP_NAME NOT IN ('PRIMARY')";
                        break;
                    }

                case SQLCommand.StockGroup.FetchGroupNameByParentID:
                    {
                        query = "SELECT GROUP_ID AS ASSET_CLASS_ID,PARENT_GROUP_ID AS PARENT_CLASS_ID,GROUP_NAME AS ASSET_CLASS FROM STOCK_GROUP WHERE  PARENT_GROUP_ID IN( " +
                                "   SELECT GROUP_ID FROM STOCK_GROUP WHERE PARENT_GROUP_ID IN(1))";
                        ;
                        break;
                    }

                case SQLCommand.StockGroup.FetchAssetParentGroupNameByID:
                    {
                        query = "SELECT GROUP_ID FROM GROUP_NAME WHERE GROUP_NAME=?GROUP_NAME";
                        break;
                    }

                //case SQLCommand.StockGroup.FetchstocNameByID:
                //    {
                //        query = "SELECT ASSET_CLASS_ID FROM ASSET_CLASS WHERE ASSET_CLASS=?ASSET_CLASS";
                //        break;
                //    }
            }
            return query;
        }
        #endregion
    }
}
