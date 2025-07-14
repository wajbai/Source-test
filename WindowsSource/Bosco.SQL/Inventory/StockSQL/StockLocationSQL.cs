using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.DAO.Schema;
using Bosco.DAO.Data;
using Bosco.DAO;

namespace Bosco.SQL
{
    class StockLocationSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.StockLocation).FullName)
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
            SQLCommand.StockLocation sqlCommandId = (SQLCommand.StockLocation)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.StockLocation.Add:
                    {
                        query = "INSERT INTO STOCK_LOCATION\n" +
                                "  (LOCATION_NAME, PARENT_LOCATION_ID,IMAGE_ID,CUSTODIANS_ID,START_DATE,LOCATION_TYPE)\n" +
                                "VALUES\n" +
                                "  (?LOCATION_NAME,?PARENT_LOCATION_ID,?IMAGE_ID,?CUSTODIANS_ID,?START_DATE,?LOCATION_TYPE)";
                        break;
                    }

                case SQLCommand.StockLocation.Update:
                    {
                        query = "UPDATE STOCK_LOCATION\n" +
                                "   SET LOCATION_ID   = ?LOCATION_ID,\n" +
                                "       LOCATION_NAME          = ?LOCATION_NAME,\n" +
                                "       PARENT_LOCATION_ID     = ?PARENT_LOCATION_ID,\n" +
                                "       IMAGE_ID     = ?IMAGE_ID,\n" +
                                "       CUSTODIANS_ID = ?CUSTODIANS_ID,\n" +
                                "       START_DATE=?START_DATE,\n" + //Start Date is responsible from date
                                "       LOCATION_TYPE=?LOCATION_TYPE \n" +
                                " WHERE LOCATION_ID = ?LOCATION_ID";
                        break;
                    }

                case SQLCommand.StockLocation.Delete:
                    {
                        query = "DELETE FROM STOCK_LOCATION WHERE LOCATION_ID = ?LOCATION_ID OR PARENT_LOCATION_ID=?LOCATION_ID";
                        break;
                    }

                case SQLCommand.StockLocation.Fetch:
                    {
                        query = "SELECT LOCATION_ID, LOCATION_NAME, PARENT_LOCATION_ID, CUSTODIANS_ID,START_DATE,LOCATION_TYPE\n" +
                              "  FROM STOCK_LOCATION\n" +
                              " WHERE LOCATION_ID = ?LOCATION_ID";
                        break;
                    }

                case SQLCommand.StockLocation.FetchAll:
                    {
                        query = "SELECT AL.LOCATION_ID, AL.CUSTODIANS_ID, AL.LOCATION_ID AS LOCATION_FROM_ID,AL.LOCATION_ID AS LOCATION_TO_ID,\n" +
                                "LOCATION_NAME,LOCATION_NAME AS TO_LOCATION_NAME, PARENT_LOCATION_ID,IMAGE_ID,\n" +
                                "START_DATE,LOCATION_TYPE\n" +
                                "FROM STOCK_LOCATION AL WHERE LOCATION_NAME NOT IN ('Primary')";
                        break;
                    }
                case SQLCommand.StockLocation.FetchToLocation:
                    {
                        query = "SELECT LOCATION_ID,LOCATION_NAME AS TO_LOCATION_NAME FROM ASSET_STOCK_LOCATION";
                        break;
                    }
                case SQLCommand.StockLocation.FetchLocationByItem:
                    {
                        query = "SELECT AD.LOCATION_ID, AD.ITEM_ID, LOCATION_NAME\n" +
                                "  FROM ASSET_ITEM_DETAIL AD\n" +
                                " INNER JOIN ASSET_STOCK_LOCATION ASL\n" +
                                "    ON AD.LOCATION_ID = ASL.LOCATION_ID\n" +
                                " GROUP BY ITEM_ID, LOCATION_ID";
                        break;
                    }

                case SQLCommand.StockLocation.FetchSelectedLocationDetails:
                    {
                        query = "SELECT AI.ITEM_ID,\n" +
                            //  "       AI.ASSET_NAME AS NAME,\n" +
                            //  "       AG.GROUP_NAME AS GROUP_NAME,\n" +
                            // "       AC.NAME       AS CATEGORY,\n" +
                                "       ASL.LOCATION_NAME,AC.NAME\n" +
                            // "       UM.SYMBOL     AS UNIT,\n" +
                            // "       SUM(if(AD.SOURCE_FLAG != (3), 1, 0)) AS QUANTITY\n" +
                                "  FROM ASSET_ITEM AI\n" +
                                " INNER JOIN ASSET_ITEM_DETAIL AD\n" +
                                "    ON AD.ITEM_ID = AI.ITEM_ID\n" +
                            // " INNER JOIN ASSET_GROUP AG\n" +
                            // "    ON AI.ASSET_GROUP_ID = AG.GROUP_ID\n" +
                                " INNER JOIN ASSET_STOCK_LOCATION ASL\n" +
                                "    ON AD.LOCATION_ID = ASL.LOCATION_ID\n" +
                                " INNER JOIN ASSET_CUSTODIANS AC\n" +
                                "    ON AD.CUSTODIANS_ID = AC.CUSTODIANS_ID\n" +
                            //  " INNER JOIN ASSET_CATEGORY AC\n" +
                            // "    ON AI.CATEGORY_ID = AC.CATEGORY_ID\n" +
                            //   " INNER JOIN ASSET_STOCK_UNITOFMEASURE UM\n" +
                            //    "    ON AI.UNIT_ID = UM.UNIT_ID\n" +
                                " WHERE AD.LOCATION_ID IN(?LOCATION_ID)\n" +
                                " GROUP BY ASL.LOCATION_NAME";

                        break;
                    }
                case SQLCommand.StockLocation.FetchStockLocation:
                    {
                        query = "SELECT SI.ITEM_ID,\n" +
                        "       SI.NAME,\n" +
                            //"       SI.CATEGORY_ID,\n" +
                            //"       SC.CATEGORY_NAME AS CATEGORY,\n" +
                        "       UM.SYMBOL        AS UNIT,\n" +
                        "       T.QUANTITY,\n" +
                        "       SG.GROUP_NAME\n" +
                        "  FROM STOCK_ITEM AS SI\n" +
                            //"  LEFT JOIN STOCK_CATEGORY AS SC\n" +
                            //"    ON SI.CATEGORY_ID = SC.CATEGORY_ID\n" +
                        "  INNER JOIN UOM UM\n" +
                        "    ON SI.UOM_ID = UM.UOM_ID\n" +
                        "  INNER JOIN STOCK_GROUP AS SG\n" +
                        "    ON SI.GROUP_ID = SG.GROUP_ID\n" +
                        "  INNER JOIN (SELECT BALANCE_DATE, ITEM_ID, QUANTITY, LOCATION_ID\n" +
                        "               FROM INVENTORY_STOCK WHERE QUANTITY NOT IN(0)\n" +
                        "              GROUP BY ITEM_ID, LOCATION_ID\n" +
                        "              ORDER BY BALANCE_DATE DESC) AS T\n" +
                        "    ON SI.ITEM_ID = T.ITEM_ID\n" +
                        " WHERE T.LOCATION_ID IN (?LOCATION_ID)";
                        break;
                    }
                case SQLCommand.StockLocation.FetchAssetLocationNameByID:
                    {
                        query = "SELECT LOCATION_ID FROM ASSET_STOCK_LOCATION WHERE LOCATION_NAME =?LOCATION_NAME";
                        break;
                    }
                case SQLCommand.StockLocation.FetchLocationByItemId:
                    {
                        query = "SELECT ASL.LOCATION_ID,AID.ITEM_ID, ASL.LOCATION_ID AS LOCATION_FROM_ID,ASL.LOCATION_ID AS LOCATION_TO_ID,\n " +
                                 "LOCATION_NAME,LOCATION_NAME AS TO_LOCATION_NAME,\n" +
                                 "PARENT_LOCATION_ID,IMAGE_ID FROM ASSET_STOCK_LOCATION ASL INNER JOIN\n " +
                                 "ASSET_ITEM_DETAIL AID ON AID.LOCATION_ID=ASL.LOCATION_ID GROUP BY ASL.LOCATION_ID, AID.ITEM_ID";
                        break;
                    }

                case SQLCommand.StockLocation.DeleteLocationDetails:
                    {
                        query = "DELETE FROM ASSET_STOCK_LOCATION WHERE LOCATION_NAME NOT IN ('PRIMARY')";
                        break;
                    }

                case SQLCommand.StockLocation.FetchValidateLocation:
                    {
                        //query = "DELETE FROM ASSET_STOCK_LOCATION WHERE LOCATION_NAME NOT IN ('PRIMARY')";
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}

