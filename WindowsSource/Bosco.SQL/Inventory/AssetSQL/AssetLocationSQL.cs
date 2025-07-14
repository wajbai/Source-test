using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    class LocationSQL : IDatabaseQuery
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
                #region Locations
                case SQLCommand.AssetLocation.Add:
                    {
                        query = "INSERT INTO ASSET_LOCATION\n" +
                                "  (LOCATION, BLOCK_ID,CUSTODIAN_ID,RESPONSIBLE_FROM,LOCATION_TYPE,RESPONSIBLE_TO)\n" +
                                "VALUES\n" +
                                "  (?LOCATION,?BLOCK_ID,?CUSTODIAN_ID,?RESPONSIBLE_FROM,?LOCATION_TYPE,?RESPONSIBLE_TO)";
                        break;
                    }

                case SQLCommand.AssetLocation.Update:
                    {
                        query = "UPDATE ASSET_LOCATION\n" +
                                "   SET LOCATION_ID   = ?LOCATION_ID,\n" +
                                "       LOCATION          = ?LOCATION,\n" +
                                "       BLOCK_ID     = ?BLOCK_ID,\n" +
                                "       CUSTODIAN_ID = ?CUSTODIAN_ID,\n" +
                                "       RESPONSIBLE_FROM=?RESPONSIBLE_FROM,\n" +
                                "       LOCATION_TYPE=?LOCATION_TYPE, \n" +
                                "       RESPONSIBLE_TO=?RESPONSIBLE_TO \n" +
                                " WHERE LOCATION_ID = ?LOCATION_ID";
                        break;
                    }

                case SQLCommand.AssetLocation.Delete:
                    {
                        query = "DELETE FROM ASSET_LOCATION WHERE LOCATION_ID = ?LOCATION_ID"; //OR BLOCK_ID=?BLOCK_ID";
                        break;
                    }

                // 20/09/2024
                //case SQLCommand.AssetLocation.Fetch:
                //    {
                //        query = "SELECT AL.LOCATION_ID, LOCATION, APL.PROJECT_ID, BLOCK_ID, CUSTODIAN_ID,RESPONSIBLE_FROM,LOCATION_TYPE,RESPONSIBLE_TO\n" +
                //            // "IF(RESPONSIBLE_TO = '0001-01-01 0000-00-00 00:00:00', '', DATE_FORMAT(RESPONSIBLE_TO, '%d/%m/%Y')) AS RESPONSIBLE_TO\n" +
                //              "  FROM ASSET_LOCATION AL\n" +
                //              "  INNER JOIN ASSET_PROJECT_LOCATION APL\n" +
                //              "     ON APL.LOCATION_ID = AL.LOCATION_ID\n" +
                //              " WHERE AL.LOCATION_ID = ?LOCATION_ID";
                //        break;
                //    }

                case SQLCommand.AssetLocation.Fetch:
                    {
                        query = "SELECT AL.LOCATION_ID, LOCATION, GROUP_CONCAT(APL.PROJECT_ID) AS PROJECT_ID, BLOCK_ID, CUSTODIAN_ID,RESPONSIBLE_FROM,LOCATION_TYPE,RESPONSIBLE_TO\n" +
                            // "IF(RESPONSIBLE_TO = '0001-01-01 0000-00-00 00:00:00', '', DATE_FORMAT(RESPONSIBLE_TO, '%d/%m/%Y')) AS RESPONSIBLE_TO\n" +
                              "  FROM ASSET_LOCATION AL\n" +
                              "  INNER JOIN ASSET_PROJECT_LOCATION APL\n" +
                              "     ON APL.LOCATION_ID = AL.LOCATION_ID\n" +
                              " WHERE AL.LOCATION_ID = ?LOCATION_ID";
                        break;
                    }

                case SQLCommand.AssetLocation.FetchAll:
                    {
                        query = "SELECT AL.LOCATION_ID,\n" +
                                "       AL.LOCATION_ID AS FROM_LOCATION_ID,\n" +
                                "       AL.LOCATION_ID AS TO_LOCATION_ID,AB.BLOCK,\n" +
                                "       LOCATION,\n" +
                            //  "       APL.PROJECT_ID,\n" +
                                "       GROUP_CONCAT(MP.PROJECT SEPARATOR ',') AS PROJECT,\n" +
                                "       GROUP_CONCAT(APL.PROJECT_ID SEPARATOR ' , ') AS PROJECT_ID,\n" +
                                "       LOCATION AS FROM_LOCATION,\n" +
                                "       LOCATION AS TO_LOCATION,\n" +
                                "       AC.CUSTODIAN_ID,\n" +
                                "       CUSTODIAN,\n" +
                            //"       IF(RESPONSIBLE_FROM = '0001-01-01 00:00:00',\n" +
                            //"          '',\n" +
                            //"          DATE_FORMAT(RESPONSIBLE_FROM, '%D/%M/%Y')) AS RESPONSIBLE_FROM,\n" +
                            //"       IF(RESPONSIBLE_TO = '0001-01-01 00:00:00', '',\n" +
                            //"          DATE_FORMAT(RESPONSIBLE_TO, '%D/%M/%Y')) AS RESPONSIBLE_TO,\n" +
                                "IF(RESPONSIBLE_FROM='0001-01-01 00:00:00','',DATE_FORMAT(RESPONSIBLE_FROM,'%d/%m/%Y')) AS RESPONSIBLE_FROM,\n" +
                                "IF(RESPONSIBLE_TO='0001-01-01 00:00:00','',DATE_FORMAT(RESPONSIBLE_TO,'%d/%m/%Y')) AS RESPONSIBLE_TO,\n" +
                                "       CASE\n" +
                                "         WHEN LOCATION_TYPE = 0 THEN\n" +
                                "          'OWN'\n" +
                                "         ELSE\n" +
                                "          'THIRD PARTY'\n" +
                                "       END AS LOCATION_TYPE\n" +
                                "  FROM ASSET_LOCATION AL\n" +
                                "  INNER JOIN ASSET_BLOCK AB\n" +
                                "  ON AL.BLOCK_ID=AB.BLOCK_ID\n" +
                                "  INNER JOIN ASSET_PROJECT_LOCATION APL\n" +
                                "  ON APL.LOCATION_ID=AL.LOCATION_ID\n" +
                                "    INNER JOIN MASTER_PROJECT MP\n" +
                                "    ON MP.PROJECT_ID = APL.PROJECT_ID\n" +
                                " INNER JOIN ASSET_CUSTODIAN AC\n" +
                                "    ON AL.CUSTODIAN_ID = AC.CUSTODIAN_ID GROUP BY AL.LOCATION_ID;";
                        break;
                    }
                case SQLCommand.AssetLocation.FetchToLocation:
                    {
                        query = "SELECT LOCATION_ID,LOCATION AS TO_LOCATION_NAME FROM ASSET_LOCATION";
                        break;
                    }
                case SQLCommand.AssetLocation.FetchLocationByItem:
                    {
                        query = "SELECT AD.LOCATION_ID, AD.ITEM_ID, LOCATION\n" +
                                "  FROM ASSET_ITEM_DETAIL AD\n" +
                                " INNER JOIN ASSET_LOCATION ASL\n" +
                                "    ON AD.LOCATION_ID = ASL.LOCATION_ID\n" +
                                " GROUP BY ITEM_ID, LOCATION_ID";
                        break;
                    }

                case SQLCommand.AssetLocation.FetchSelectedLocationDetails:
                    {
                        query = "SELECT AI.ITEM_ID,\n" +
                                "       ASL.LOCATION,AC.CUSTODIAN\n" +
                                "  FROM ASSET_ITEM AI\n" +
                                " INNER JOIN ASSET_ITEM_DETAIL AD\n" +
                                "    ON AD.ITEM_ID = AI.ITEM_ID\n" +
                                " INNER JOIN ASSET_LOCATION ASL\n" +
                                "    ON AD.LOCATION_ID = ASL.LOCATION_ID\n" +
                                " INNER JOIN ASSET_CUSTODIAN AC\n" +
                                "    ON AD.CUSTODIAN_ID = AC.CUSTODIAN_ID\n" +
                                " WHERE AD.LOCATION_ID IN(?LOCATION_ID)\n" +
                                " GROUP BY ASL.LOCATION";
                        break;
                    }
                case SQLCommand.AssetLocation.FetchStockLocation:
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
                        "    ON SI.UNIT_ID = UM.UNIT_ID\n" +
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
                case SQLCommand.AssetLocation.FetchAssetLocationNameByID:
                    {
                        query = "SELECT LOCATION_ID FROM ASSET_LOCATION WHERE LOCATION =?LOCATION";
                        break;
                    }
                case SQLCommand.AssetLocation.FetchLocationByItemId:
                    {
                        query = "SELECT ASL.LOCATION_ID,AID.ITEM_ID, ASL.LOCATION_ID AS LOCATION_FROM_ID,ASL.LOCATION_ID AS LOCATION_TO_ID,\n " +
                                 "LOCATION_NAME,LOCATION_NAME AS TO_LOCATION_NAME,\n" +
                                 "PARENT_LOCATION_ID,IMAGE_ID FROM ASSET_STOCK_LOCATION ASL INNER JOIN\n " +
                                 "ASSET_ITEM_DETAIL AID ON AID.LOCATION_ID=ASL.LOCATION_ID GROUP BY ASL.LOCATION_ID, AID.ITEM_ID";
                        break;
                    }

                case SQLCommand.AssetLocation.DeleteLocationDetails:
                    {
                        query = "DELETE FROM ASSET_STOCK_LOCATION WHERE LOCATION_NAME NOT IN ('PRIMARY')";
                        break;
                    }

                case SQLCommand.AssetLocation.FetchBlockDetails:
                    {
                        query = "SELECT BLOCK_ID,BLOCK FROM ASSET_BLOCK";
                        break;
                    }

                case SQLCommand.AssetLocation.FetchValidateLocation:
                    {
                        query = "SELECT AB.BLOCK_ID, BLOCK, LOCATION, LOCATION_ID\n" +
                            "  FROM ASSET_BLOCK AB\n" +
                            " INNER JOIN ASSET_LOCATION AL\n" +
                            "    ON AB.BLOCK_ID = AL.BLOCK_ID\n" +
                            " WHERE AB.BLOCK_ID =?BLOCK_ID ";
                        break;
                    }

                case SQLCommand.AssetLocation.FetchAssetLocationByProjectID:
                    {
                        query = "SELECT APL.LOCATION_ID,LOCATION,AC.CUSTODIAN_ID,CUSTODIAN,AB.BLOCK,\n" +
                                "IF(RESPONSIBLE_FROM='0001-01-01 00:00:00','',DATE_FORMAT(RESPONSIBLE_FROM,'%d/%m/%Y')) AS RESPONSIBLE_FROM,\n" +
                                "IF(RESPONSIBLE_TO='0001-01-01 00:00:00','',DATE_FORMAT(RESPONSIBLE_TO,'%d/%m/%Y')) AS RESPONSIBLE_TO,\n" +
                                "CASE WHEN LOCATION_TYPE=0 THEN\n" +
                                " \"Own\"\n" +
                                "ELSE\n" +
                                " \"Third Party\"\n" +
                                "END AS LOCATION_TYPE\n" +
                                "     FROM ASSET_LOCATION AL\n" +
                                "INNER JOIN ASSET_BLOCK AB\n" +
                                "ON AL.BLOCK_ID=AB.BLOCK_ID\n" +
                                "INNER JOIN ASSET_PROJECT_LOCATION APL\n" +
                                "ON AL.LOCATION_ID=APL.LOCATION_ID\n" +
                                "INNER JOIN ASSET_CUSTODIAN AC\n" +
                                "ON AL.CUSTODIAN_ID=AC.CUSTODIAN_ID WHERE PROJECT_ID = ?PROJECT_ID;";

                        break;
                    }

                case SQLCommand.AssetLocation.FetchEditValidateLocation:
                    {
                        //query = "SELECT AB.BLOCK_ID, BLOCK, LOCATION, LOCATION_ID\n" +
                        //    "  FROM ASSET_BLOCK AB\n" +
                        //    " INNER JOIN ASSET_LOCATION AL\n" +
                        //    "    ON AB.BLOCK_ID = AL.BLOCK_ID\n" +
                        //    " WHERE AB.BLOCK_ID NOT IN (?BLOCK_ID) AND LOCATION=?LOCATION";
                        query = "SELECT AL.LOCATION\n" +
                                "  FROM ASSET_BLOCK AB\n" +
                                " INNER JOIN ASSET_LOCATION AL\n" +
                                "    ON AB.BLOCK_ID = AL.BLOCK_ID\n" +
                                " INNER JOIN (SELECT LOCATION\n" +
                                "               FROM ASSET_BLOCK AB\n" +
                                "              INNER JOIN ASSET_LOCATION AL\n" +
                                "                 ON AB.BLOCK_ID = AL.BLOCK_ID\n" +
                                "              WHERE AL.BLOCK_ID IN (?BLOCK_ID)) AS T\n" +
                                "    ON T.LOCATION IN (?LOCATION) AND LOCATION_ID <> ?LOCATION_ID AND LOCATION_TYPE <> ?LOCATION_TYPE";
                        break;
                    }
                case SQLCommand.AssetLocation.FetchProjectLocationProjectId:
                    {

                        query = "SELECT APL.LOCATION_ID, APL.PROJECT_ID, AL.LOCATION\n" +
                        "  FROM ASSET_PROJECT_LOCATION APL\n" +
                        " INNER JOIN ASSET_LOCATION AL\n" +
                        "    ON APL.LOCATION_ID = AL.LOCATION_ID\n" +
                        " WHERE APL.PROJECT_ID = ?PROJECT_ID;";

                        break;
                    }
                #endregion
            }
            return query;
        }
        #endregion
    }
}
