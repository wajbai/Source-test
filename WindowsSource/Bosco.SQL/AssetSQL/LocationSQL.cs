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
                //Area Query
                case SQLCommand.AssetLocation.AreaAdd:
                    {
                        query = "INSERT INTO ASSET_AREA\n" +
                                "  (NAME)\n" +
                                "VALUES\n" +
                                "  (?NAME);";
                        break;
                    }
                case SQLCommand.AssetLocation.AreaUpdate:
                    {
                        query = "UPDATE ASSET_AREA\n" +
                                "   SET NAME   = ?NAME \n" +
                                " WHERE AREA_ID = ?AREA_ID";
                        break;
                    }
                case SQLCommand.AssetLocation.AreaDelete:
                    {
                        query = "DELETE FROM ASSET_AREA WHERE AREA_ID= ?AREA_ID";
                        break;
                    }
                case SQLCommand.AssetLocation.AreaFetch:
                    {
                        query = "SELECT AREA_ID, NAME AS 'AREA_NAME' \n" +
                              "  FROM ASSET_AREA\n" +
                              " WHERE AREA_ID = ?AREA_ID";
                        break;
                    }
                case SQLCommand.AssetLocation.AreaFetchAll:
                    {
                        query = "SELECT AREA_ID, NAME AS 'AREA_NAME' FROM ASSET_AREA ORDER BY AREA_NAME ASC";
                        break;
                    }

                //Building
                case SQLCommand.AssetLocation.BuildingAdd:
                    {
                        query = "INSERT INTO ASSET_BUILDING\n" +
                                "  (AREA_ID,NAME)\n" +
                                "VALUES\n" +
                                "  (?AREA_ID,?NAME);";
                        break;
                    }
                case SQLCommand.AssetLocation.BuildingUpdate:
                    {
                        query = "UPDATE ASSET_BUILDING\n" +
                                "   SET AREA_ID=?AREA_ID, NAME   = ?NAME\n" +
                                " WHERE BUILDING_ID =?BUILDING_ID";
                        break;
                    }
                case SQLCommand.AssetLocation.BuildingDelete:
                    {
                        query = "DELETE FROM ASSET_BUILDING WHERE BUILDING_ID= ?BUILDING_ID";
                        break;
                    }
                case SQLCommand.AssetLocation.BuildingFetch:
                    {
                        query = "SELECT BUILDING_ID,AREA_ID, NAME AS 'BUILDING_NAME'\n" +
                              "  FROM ASSET_BUILDING\n" +
                              " WHERE BUILDING_ID =?BUILDING_ID";
                        break;
                    }
                case SQLCommand.AssetLocation.BuildingFetchAll:
                    {
                        query = "SELECT BUILDING_ID,\n" +
                                "       A.AREA_ID,\n" +
                                "       A.NAME      AS 'AREA_NAME',\n" +
                                "       B.NAME      AS 'BUILDING_NAME'\n" +
                                "  FROM ASSET_BUILDING B\n" +
                                " INNER JOIN ASSET_AREA A\n" +
                                "    ON A.AREA_ID = B.AREA_ID ORDER BY A.NAME ASC";
                        break;
                    }

                //Block
                case SQLCommand.AssetLocation.BlockAdd:
                    {
                        query = "INSERT INTO ASSET_BLOCK\n" +
                                "  (BUILDING_ID,NAME)\n" +
                                "VALUES\n" +
                                "  (?BUILDING_ID,?NAME);";
                        break;
                    }
                case SQLCommand.AssetLocation.BlockUpdate:
                    {
                        query = "UPDATE ASSET_BLOCK\n" +
                                "   SET BUILDING_ID=?BUILDING_ID, NAME=?NAME\n" +
                                " WHERE BLOCK_ID= ?BLOCK_ID";
                        break;
                    }
                case SQLCommand.AssetLocation.BlockDelete:
                    {
                        query = "DELETE FROM ASSET_BLOCK WHERE BLOCK_ID= ?BLOCK_ID";
                        break;
                    }
                case SQLCommand.AssetLocation.BlockFetch:
                    {
                        query = "SELECT BLOCK_ID,BUILDING_ID, NAME  AS 'BLOCK_NAME'\n" +
                              "  FROM ASSET_BLOCK\n" +
                              " WHERE BLOCK_ID = ?BLOCK_ID";
                        break;
                    }
                case SQLCommand.AssetLocation.BlockFetchAll:
                    {
                        query= "SELECT BLOCK_ID,\n" +
                                "       BI.BUILDING_ID,\n" +
                                "       A.NAME         AS 'AREA_NAME',\n" +
                                "       B.NAME         AS 'BLOCK_NAME',\n" +
                                "       BI.NAME        AS 'BUILDING_NAME'\n" +
                                "  FROM ASSET_BLOCK B\n" +
                                " INNER JOIN ASSET_BUILDING BI\n" +
                                "    ON B.BUILDING_ID = BI.BUILDING_ID\n" +
                                " INNER JOIN ASSET_AREA A\n" +
                                "    ON BI.AREA_ID = A.AREA_ID ORDER BY A.NAME ASC;";
                        break;
                    }


                //Floor
                case SQLCommand.AssetLocation.FloorAdd:
                    {
                        query = "INSERT INTO ASSET_FLOOR\n" +
                                "  (BLOCK_ID,NAME)\n" +
                                "VALUES\n" +
                                "  (?BLOCK_ID,?NAME);";
                        break;
                    }
                case SQLCommand.AssetLocation.FloorUpdate:
                    {
                        query = "UPDATE ASSET_FLOOR\n" +
                                "   SET BLOCK_ID=?BLOCK_ID, NAME=?NAME\n" +
                                " WHERE FLOOR_ID= ?FLOOR_ID";
                        break;
                    }
                case SQLCommand.AssetLocation.FloorDelete:
                    {
                        query = "DELETE FROM ASSET_FLOOR WHERE FLOOR_ID= ?FLOOR_ID";
                        break;
                    }
                case SQLCommand.AssetLocation.FloorFetch:
                    {
                        query = "SELECT FLOOR_ID,BLOCK_ID, NAME AS 'FLOOR_NAME'\n" +
                              "  FROM ASSET_FLOOR\n" +
                              " WHERE FLOOR_ID= ?FLOOR_ID";
                        break;
                    }
                case SQLCommand.AssetLocation.FloorByBlock:
                    {
                        query = "SELECT FLOOR_ID,BLOCK_ID, NAME AS 'FLOOR_NAME'\n" +
                              "  FROM ASSET_FLOOR\n" +
                              " WHERE BLOCK_ID= ?BLOCK_ID";
                        break;
                    }
                case SQLCommand.AssetLocation.FloorFetchAll:
                    {
                        query = "SELECT FLOOR_ID,\n" +
                                "       B.BLOCK_ID,\n" +
                                "       A.NAME     AS 'AREA_NAME',\n" +
                                "       BI.NAME    AS 'BUILDING_NAME',\n" +
                                "       F.NAME     AS 'FLOOR_NAME',\n" +
                                "       B.NAME     AS 'BLOCK_NAME'\n" +
                                "  FROM ASSET_FLOOR F\n" +
                                " INNER JOIN ASSET_BLOCK B\n" +
                                "    ON B.BLOCK_ID = F.BLOCK_ID\n" +
                                " INNER JOIN ASSET_BUILDING BI\n" +
                                "    ON B.BUILDING_ID = BI.BUILDING_ID\n" +
                                " INNER JOIN ASSET_AREA A\n" +
                                "    ON BI.AREA_ID = A.AREA_ID ORDER BY A.NAME ASC;";
                        break;
                    }

                //Room
                case SQLCommand.AssetLocation.RoomAdd:
                    {
                        query = "INSERT INTO ASSET_ROOM\n" +
                                "  (BLOCK_ID,FLOOR_ID,ROOM_NO)\n" +
                                "VALUES\n" +
                                "  (?BLOCK_ID,?FLOOR_ID,?NAME);";
                        break;
                    }
                case SQLCommand.AssetLocation.RoomUpdate:
                    {
                        query = "UPDATE ASSET_ROOM\n" +
                                "   SET BLOCK_ID=?BLOCK_ID,FLOOR_ID=?FLOOR_ID, ROOM_NO=?NAME\n" +
                                " WHERE ROOM_ID= ?ROOM_ID";
                        break;
                    }
                case SQLCommand.AssetLocation.RoomDelete:
                    {
                        query = "DELETE FROM ASSET_ROOM WHERE ROOM_ID= ?ROOM_ID";
                        break;
                    }
                case SQLCommand.AssetLocation.RoomFetch:
                    {
                        query = "SELECT ROOM_ID,BLOCK_ID,FLOOR_ID, R.ROOM_NO AS 'ROOM_NO'\n" +
                              "  FROM ASSET_ROOM R\n" +
                              " WHERE ROOM_ID= ?ROOM_ID";
                        break;
                    }
                case SQLCommand.AssetLocation.RoomFetchAll:
                    {
                        query = "SELECT R.ROOM_ID, \n" +
                                "       B.BLOCK_ID,\n" +
                                "       R.ROOM_NO,\n" +
                                "       A.NAME     AS 'AREA_NAME',\n" +
                                "       BI.NAME    AS 'BUILDING_NAME',\n" +
                                "       AF.NAME     AS 'FLOOR_NAME',\n" +
                                "       B.NAME     AS 'BLOCK_NAME'\n" +
                                "  FROM ASSET_ROOM R\n" +
                                " INNER JOIN ASSET_FLOOR AF\n" +
                                "    ON AF.FLOOR_ID=R.FLOOR_ID \n"+
                                " INNER JOIN ASSET_BLOCK B\n" +
                                "    ON B.BLOCK_ID = AF.BLOCK_ID\n" +
                                " INNER JOIN ASSET_BUILDING BI\n" +
                                "    ON B.BUILDING_ID = BI.BUILDING_ID\n" +
                                " INNER JOIN ASSET_AREA A\n" +
                                "    ON BI.AREA_ID = A.AREA_ID ORDER BY A.NAME ASC;";
                        break;
                    }

                //case SQLCommand.AssetLocation.Add:
                //    {
                //        query = "INSERT INTO ASSET_LOCATION\n" +
                //                "  (LOCATION_ID, NAME, ADDRESS, LOCATION_TYPE)\n" + 
                //                "VALUES\n" + 
                //                "  (?LOCATION_ID, ?NAME, ?ADDRESS, ?LOCATION_TYPE);";
                //        break;
                //    }

                //case SQLCommand.AssetLocation.Update:
                //    {
                //        query = "UPDATE ASSET_LOCATION\n" +
                //                "   SET LOCATION_ID   = ?LOCATION_ID,\n" + 
                //                "       NAME          = ?NAME,\n" + 
                //                "       ADDRESS       = ?ADDRESS,\n" + 
                //                "       LOCATION_TYPE = ?LOCATION_TYPE\n" + 
                //                " WHERE LOCATION_ID = ?LOCATION_ID";
                //        break;
                //    }

                //case SQLCommand.AssetLocation.Delete:
                //    {
                //        query="DELETE FROM ASSET_LOCATION WHERE LOCATION_ID = ?LOCATION_ID";
                //        break;
                //    }

                //case SQLCommand.AssetLocation.Fetch:
                //    {
                //        query="SELECT LOCATION_ID, NAME, ADDRESS, LOCATION_TYPE\n" +
                //              "  FROM ASSET_LOCATION\n" + 
                //              " WHERE LOCATION_ID = ?LOCATION_ID";


                //        break;
                //    }

                //case SQLCommand.AssetLocation.FetchAll:
                //    {
                //        query ="SELECT LOCATION_ID, NAME, ADDRESS, LOCATION_TYPE FROM ASSET_LOCATION";


                //        break;
                //    }
            }
            return query;
        }
        #endregion
    }
}
