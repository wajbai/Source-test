using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.DAO;

namespace Bosco.SQL
{
    public class AssetClassSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.AssetClass).FullName)
            {
                query = GetClassSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        #region SQL Script
        public string GetClassSQL()
        {
            string query = "";
            SQLCommand.AssetClass SqlcommandId = (SQLCommand.AssetClass)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.AssetClass.Add:
                    {
                        query = "INSERT INTO ASSET_CLASS (" +
                                "ASSET_CLASS," +
                                "PARENT_CLASS_ID," +
                                "METHOD_ID," +
                                "DEP_PERCENTAGE)VALUES(" +
                                "?ASSET_CLASS, " +
                                "?PARENT_CLASS_ID," +
                                "?METHOD_ID," +
                                "?DEP_PERCENTAGE)";
                        break;
                    }
                case SQLCommand.AssetClass.Update:
                    {
                        query = "UPDATE ASSET_CLASS SET " +
                                "ASSET_CLASS=?ASSET_CLASS," +
                                "PARENT_CLASS_ID=?PARENT_CLASS_ID," +
                                "METHOD_ID=?METHOD_ID," +
                                "DEP_PERCENTAGE=?DEP_PERCENTAGE " +
                                "WHERE ASSET_CLASS_ID =?ASSET_CLASS_ID";
                        break;
                    }
                case SQLCommand.AssetClass.FetchAll:
                    {
                        query = "SELECT ASSET_CLASS_ID,\n" +
                                "       PARENT_CLASS_ID,\n" +
                                "       ASSET_CLASS,\n" +
                                "       ADM.DEP_METHOD,\n" +
                                "       DEP_PERCENTAGE\n" +
                                "  FROM ASSET_CLASS AC\n" +
                                " LEFT JOIN ASSET_DEP_METHOD ADM\n" +
                                "    ON AC.METHOD_ID = ADM.METHOD_ID\n" +
                                " WHERE ASSET_CLASS_ID NOT IN (1)\n" +
                                " ORDER BY AC.ASSET_CLASS_ID";
                        //"ORDER BY GROUP_NAME";
                        break;
                    }
                case SQLCommand.AssetClass.FetchSelectedClass:
                    {
                        //query = "SELECT AI.ITEM_ID,\n" +
                        //        "       AI.ASSET_ITEM,\n" +
                        //        "       AC.ASSET_CLASS \n" +
                        //        "  FROM ASSET_ITEM AI\n" +
                        //        "  LEFT JOIN ASSET_CLASS AC\n" +
                        //        "    ON AI.ASSET_CLASS_ID = AC.ASSET_CLASS_ID\n" +
                        //        "  LEFT JOIN ASSET_ITEM_DETAIL AD\n" +
                        //        "    ON AD.ITEM_ID = AI.ITEM_ID\n" +
                        //        " WHERE AI.ASSET_CLASS_ID IN (?ASSET_CLASS_ID)\n" +
                        //        " GROUP BY ITEM_ID\n" +
                        //        " ORDER BY AC.ASSET_CLASS_ID";

                        // Commanded by Chinna On 16.09.2022
                        //query = "SELECT AC.PARENT_CLASS_ID,\n" +
                        //"       AC.ASSET_CLASS_ID,\n" +
                        //"       PARENT.ASSET_CLASS AS PARENT_CLASS,\n" +
                        //"       AC.ASSET_CLASS,\n" +
                        //"       AI.ASSET_ITEM,\n" +
                        //"       ADM.DEP_METHOD,\n" +
                        //"       AC.DEP_PERCENTAGE\n" +
                        //"  FROM ASSET_CLASS AC\n" +
                        //"  LEFT JOIN ASSET_DEP_METHOD ADM\n" +
                        //"    ON AC.METHOD_ID = ADM.METHOD_ID\n" +
                        //"  LEFT JOIN ASSET_ITEM AI\n" +
                        //"    ON AC.ASSET_CLASS_ID = AI.ASSET_CLASS_ID\n" +
                        //"  INNER JOIN ASSET_CLASS PARENT\n" +
                        //"    ON AC.PARENT_CLASS_ID = PARENT.ASSET_CLASS_ID\n" +
                        //" WHERE AC.ASSET_CLASS_ID IN(?ASSET_CLASS_ID) AND AI.ITEM_ID >0 \n" +
                        //" ORDER BY AC.ASSET_CLASS, AI.ASSET_ITEM ASC;";

                        query = " SELECT AC.PARENT_CLASS_ID,IF(PARENT.ASSET_CLASS ='Primary', AC.ASSET_CLASS,PARENT.ASSET_CLASS) AS PARENT_CLASS,\n" +
                              " AC.ASSET_CLASS_ID,PARENT.ASSET_CLASS_ID AS MAIN_CLASS_ID, AC2.ASSET_CLASS_ID AS MAIN_CLASS_ID2,\n" +
                              " AC.ASSET_CLASS,\n" +
                              " AI.ASSET_ITEM,\n" +
                              " ADM.DEP_METHOD,\n" +
                              " AC.DEP_PERCENTAGE\n" +
                         " FROM ASSET_CLASS AC\n" +
                         " LEFT JOIN ASSET_DEP_METHOD ADM\n" +
                           " ON AC.METHOD_ID = ADM.METHOD_ID\n" +
                         " LEFT JOIN ASSET_ITEM AI\n" +
                           " ON AC.ASSET_CLASS_ID = AI.ASSET_CLASS_ID\n" +
                         " INNER JOIN ASSET_CLASS PARENT\n" +
                           " ON PARENT.ASSET_CLASS_ID = AC.PARENT_CLASS_ID\n" +
                         " LEFT JOIN ASSET_CLASS AC2\n" +
                          "  ON AC2.ASSET_CLASS_ID = PARENT.PARENT_CLASS_ID\n" +
                        " WHERE (AC.ASSET_CLASS_ID IN (?ASSET_CLASS_ID) OR PARENT.ASSET_CLASS_ID IN (?ASSET_CLASS_ID) OR AC2.ASSET_CLASS_ID IN (?ASSET_CLASS_ID)) AND AI.ITEM_ID >0\n" +
                         " ORDER BY AC.ASSET_CLASS, AI.ASSET_ITEM ASC;";

                        break;
                    }
                case SQLCommand.AssetClass.Delete:
                    {
                        query = "DELETE FROM ASSET_CLASS WHERE ASSET_CLASS_ID=?ASSET_CLASS_ID OR PARENT_CLASS_ID =?ASSET_CLASS_ID";
                        //query = "DELETE FROM ASSET_CLASS WHERE ASSET_CLASS_ID=?ASSET_CLASS_ID AND PARENT_CLASS_ID=?PARENT_CLASS_ID";
                        break;
                    }
                case SQLCommand.AssetClass.DeleteClass:
                    {
                        query = "DELETE FROM ASSET_CLASS WHERE ASSET_CLASS_ID=?ASSET_CLASS_ID ";
                        break;
                    }
                case SQLCommand.AssetClass.FetchbyID:
                    {
                        query = "SELECT ASSET_CLASS_ID, ASSET_CLASS, PARENT_CLASS_ID, METHOD_ID, DEP_PERCENTAGE\n" +
                                "  FROM ASSET_CLASS\n" +
                                " WHERE ASSET_CLASS_ID = ?ASSET_CLASS_ID";
                        break;
                    }
                case SQLCommand.AssetClass.FetchAssetClassList:
                    {
                        query = "SELECT ASSET_CLASS_ID,CLASS_NAME FROM ASSET_CLASS";
                        break;
                    }

                case SQLCommand.AssetClass.FetchAssetClassNameByID:
                    {
                        query = "SELECT ASSET_CLASS_ID FROM ASSET_CLASS WHERE ASSET_CLASS=?ASSET_CLASS";
                        break;
                    }

                case SQLCommand.AssetClass.FetchAssetParentClassNameByID:
                    {
                        query = "SELECT ASSET_CLASS_ID FROM ASSET_CLASS WHERE CLASS_NAME=?CLASS_NAME";
                        break;
                    }
                case SQLCommand.AssetClass.FetchAssetParentClassIdByParentClassName:
                    {
                        query = "SELECT ASSET_CLASS_ID FROM ASSET_CLASS WHERE ASSET_CLASS=?ASSET_CLASS";
                        break;
                    }
                case SQLCommand.AssetClass.FetchAccessFlagClassId:
                    {
                        query = "SELECT ACCESS_FLAG FROM ASSET_CLASS WHERE ASSET_CLASS_ID=?ASSET_CLASS_ID";
                        break;

                    }
                case SQLCommand.AssetClass.DeleteAssetDetails:
                    {
                        query = "DELETE FROM ASSET_CLASS WHERE CLASS_NAME NOT IN ('PRIMARY')";
                        break;
                    }
                case SQLCommand.AssetClass.FetchClassNameByParentID:
                    {
                        //query = "SELECT GROUP_ID,\n" +
                        //        "       PARENT_GROUP_ID,\n" +
                        //        "       GROUP_NAME,\n" +
                        //        "       ADM.NAME AS DEP_NAME,\n" +
                        //        "       DEP_PERCENTAGE\n" +
                        //        "  FROM ASSET_GROUP AG\n" +
                        //        " INNER JOIN ASSET_DEPRECIATION_METHOD ADM\n" +
                        //        "    ON AG.METHOD_ID = ADM.METHOD_ID\n" +
                        //        " WHERE GROUP_ID NOT IN (1)AND PARENT_GROUP_ID=1\n" +
                        //        "ORDER BY GROUP_NAME";
                        query = "SELECT ASSET_CLASS_ID,PARENT_CLASS_ID,ASSET_CLASS FROM ASSET_CLASS WHERE  PARENT_CLASS_ID IN( " +
                                "   SELECT ASSET_CLASS_ID FROM ASSET_CLASS WHERE PARENT_CLASS_ID IN(1))";
                        ;
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}
