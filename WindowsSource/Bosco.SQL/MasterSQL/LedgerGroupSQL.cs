using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    class LedgerGroupSQL : IDatabaseQuery
    {
        #region ISQLedgerQuery Members

        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.LedgerGroup).FullName)
            {
                query = GetLedgerGroupSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script

        private string GetLedgerGroupSQL()
        {
            string query = "";
            SQLCommand.LedgerGroup sqlCommandId = (SQLCommand.LedgerGroup)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.LedgerGroup.Add:
                    {
                        query = "INSERT INTO MASTER_LEDGER_GROUP ( " +
                                   "GROUP_CODE, " +
                                   "LEDGER_GROUP, " +
                                   "PARENT_GROUP_ID, " +
                                   "NATURE_ID, " +
                                   "MAIN_GROUP_ID,IMAGE_ID,SORT_ORDER ) VALUES " +
                                   "(?GROUP_CODE, " +
                                   "?LEDGER_GROUP, " +
                                   "?PARENT_GROUP_ID, " +
                                   "?NATURE_ID, " +
                                   "?MAIN_GROUP_ID,?IMAGE_ID,?SORT_ORDER )";
                        break;
                    }
                case SQLCommand.LedgerGroup.Update:
                    {
                        query = "UPDATE MASTER_LEDGER_GROUP SET " +
                                        "GROUP_ID = ?GROUP_ID, " +
                                        "GROUP_CODE =?GROUP_CODE, " +
                                        "LEDGER_GROUP =?LEDGER_GROUP, " +
                                        "PARENT_GROUP_ID=?PARENT_GROUP_ID, " +
                                        "NATURE_ID=?NATURE_ID, " +
                                        "MAIN_GROUP_ID=?MAIN_GROUP_ID,IMAGE_ID=?IMAGE_ID " +
                                        " WHERE GROUP_ID=?GROUP_ID";
                        break;
                    }
                case SQLCommand.LedgerGroup.AddGeneralate:
                    {
                        query = "INSERT INTO PORTAL_CONGREGATION_LEDGER ( " +
                                   "CON_LEDGER_CODE, " +
                                   "CON_LEDGER_NAME, " +
                                   "CON_PARENT_LEDGER_ID, " +
                                   "CON_MAIN_PARENT_ID) VALUES " +
                                   "(?CON_LEDGER_CODE, " +
                                   "?CON_LEDGER_NAME, " +
                                   "?CON_PARENT_LEDGER_ID, " +
                                   "?CON_MAIN_PARENT_ID)";
                        break;
                    }
                case SQLCommand.LedgerGroup.UpdateGeneralate:
                    {
                        query = "UPDATE PORTAL_CONGREGATION_LEDGER SET " +
                                        "CON_LEDGER_ID = ?CON_LEDGER_ID, " +
                                        "CON_LEDGER_CODE =?CON_LEDGER_CODE, " +
                                        "CON_LEDGER_NAME =?CON_LEDGER_NAME, " +
                                        "CON_PARENT_LEDGER_ID=?CON_PARENT_LEDGER_ID, " +
                                        "CON_MAIN_PARENT_ID=?CON_MAIN_PARENT_ID " +
                                        " WHERE CON_LEDGER_ID=?CON_LEDGER_ID";
                        break;
                    }
                case SQLCommand.LedgerGroup.UpdateParentGroupId:
                    {
                        query = "UPDATE MASTER_LEDGER_GROUP SET " +
                                        "PARENT_GROUP_ID=?PARENT_GROUP_ID, " +
                                        "MAIN_GROUP_ID=?MAIN_GROUP_ID " +
                                        " WHERE GROUP_ID=?GROUP_ID";
                        break;
                    }
                case SQLCommand.LedgerGroup.UpdateGeneralateParentGroupId:
                    {
                        query = "UPDATE PORTAL_CONGREGATION_LEDGER SET " +
                                        "CON_PARENT_LEDGER_ID=?CON_PARENT_LEDGER_ID, " +
                                        "CON_MAIN_PARENT_ID=?CON_MAIN_PARENT_ID " +
                                        " WHERE CON_LEDGER_ID=?CON_LEDGER_ID";
                        break;
                    }
                case SQLCommand.LedgerGroup.Delete:
                    {
                        query = "DELETE FROM MASTER_LEDGER_GROUP WHERE GROUP_ID=?GROUP_ID ";
                        break;
                    }
                case SQLCommand.LedgerGroup.Fetch:
                    {
                        query = "SELECT " +
                                   "GROUP_ID, " +
                                   "GROUP_CODE, " +
                                   "LEDGER_GROUP, " +
                                   "PARENT_GROUP_ID, " +
                                   "NATURE_ID, " +
                                   "MAIN_GROUP_ID, " +
                                   "IMAGE_ID " +
                               "FROM " +
                                   "MASTER_LEDGER_GROUP" +
                                   " WHERE GROUP_ID=?GROUP_ID ";
                        break;
                    }
                case SQLCommand.LedgerGroup.FetchAll:
                    {
                        query = "SELECT " +
                                    "GROUP_ID , " +
                                    "LEDGER_GROUP AS 'Ledger Group' , " +
                                    "PARENT_GROUP_ID,IMAGE_ID " +
                                "FROM " +
                                    "MASTER_LEDGER_GROUP ";
                        break;
                    }

                case SQLCommand.LedgerGroup.FetchLedgerList:
                    {
                        //query = "SELECT ML.LEDGER_CODE,\n" +
                        //        "       ML.LEDGER_NAME AS 'LEDGER NAME'\n" +
                        //        "  FROM MASTER_LEDGER_GROUP MLG\n" +
                        //        " INNER JOIN MASTER_LEDGER ML\n" +
                        //        "    ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                        //        " WHERE PARENT_GROUP_ID IN (?GROUP_ID)\n" +
                        //        "\n" +
                        //        "UNION\n" +
                        //        "SELECT GROUP_ID, '' AS 'LEDGER NAME'\n" +
                        //        "  FROM MASTER_LEDGER_GROUP\n" +
                        //        " WHERE PARENT_GROUP_ID IN\n" +
                        //        "       (SELECT GROUP_ID\n" +
                        //        "          FROM MASTER_LEDGER_GROUP MLG\n" +
                        //        "         WHERE MLG.PARENT_GROUP_ID IN (?GROUP_ID))\n" +
                        //        "\n" +
                        //        "UNION\n" +
                        //        "SELECT GROUP_ID, '' AS 'LEDGER_NAME'\n" +
                        //        "  FROM MASTER_LEDGER_GROUP MLG2\n" +
                        //        " WHERE MLG2.GROUP_ID IN (?GROUP_ID);";

                        query = "SELECT ML.LEDGER_CODE, ML.LEDGER_NAME AS 'LEDGER NAME'\n" +
                               "  FROM MASTER_LEDGER ML\n" +
                               "  JOIN (SELECT MLG.GROUP_ID, '' AS L\n" +
                               "          FROM MASTER_LEDGER_GROUP MLG\n" +
                               "         INNER JOIN MASTER_LEDGER ML\n" +
                               "            ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                               "         WHERE PARENT_GROUP_ID IN (?GROUP_ID)\n" +
                               "        UNION\n" +
                               "        SELECT GROUP_ID, '' AS L\n" +
                               "          FROM MASTER_LEDGER_GROUP\n" +
                               "         WHERE PARENT_GROUP_ID IN\n" +
                               "               (SELECT GROUP_ID\n" +
                               "                  FROM MASTER_LEDGER_GROUP MLG\n" +
                               "                 WHERE MLG.PARENT_GROUP_ID IN (?GROUP_ID))\n" +
                               "        UNION\n" +
                               "        SELECT GROUP_ID, '' AS L\n" +
                               "          FROM MASTER_LEDGER_GROUP MLG2\n" +
                               "         WHERE MLG2.GROUP_ID IN (?GROUP_ID)) AS T\n" +
                               "    ON T.GROUP_ID = ML.GROUP_ID  WHERE ML.STATUS=0 ORDER BY ML.LEDGER_NAME;";

                        break;
                    }
                case SQLCommand.LedgerGroup.FetchByGroupId:
                    {
                        query = "SELECT GROUP_ID , LEDGER_GROUP AS 'Ledger Sub Group' , " +
                                    "PARENT_GROUP_ID,IMAGE_ID FROM MASTER_LEDGER_GROUP " +
                                "WHERE " +
                                "FIND_IN_SET(GROUP_ID,?LEDGER_GROUP) >0 " +
                                "UNION " +
                                "SELECT GROUP_ID , CONCAT(LEDGER_GROUP,CONCAT(' -',GROUP_CODE )) AS 'Ledger Sub Group' , " +
                                    "PARENT_GROUP_ID,IMAGE_ID FROM MASTER_LEDGER_GROUP " +
                                "WHERE " +
                                "FIND_IN_SET(PARENT_GROUP_ID,?LEDGER_GROUP)>0;";
                        break;
                    }
                case SQLCommand.LedgerGroup.FetchforLookup:
                    {
                        query = "SELECT " +
                                    "GROUP_ID , GROUP_CODE, " +
                                    "LEDGER_GROUP  " +
                                "FROM " +
                                    "MASTER_LEDGER_GROUP WHERE GROUP_ID NOT IN (12,14) ORDER BY LEDGER_GROUP ASC "; //WHERE GROUP_ID NOT IN (1,2,3,4,12)
                        break;
                    }
                case SQLCommand.LedgerGroup.FetchforLedgerLookup:
                    {
                        query = "SELECT " +
                                    "GROUP_ID , " +
                                    "LEDGER_GROUP,NATURE_ID  " +
                                "FROM " +
                                    "MASTER_LEDGER_GROUP WHERE GROUP_ID NOT IN (1,2,3,4,14)  ORDER BY LEDGER_GROUP ASC "; //Not to show Account Nature in the Ledger Lookup & Bank Account & FD
                        break;
                    }
                case SQLCommand.LedgerGroup.FetchNatureId:
                    {
                        query = "SELECT " +
                                   "NATURE_ID " +
                               "FROM " +
                                   "MASTER_LEDGER_GROUP " +
                                   "WHERE GROUP_ID=?GROUP_ID ";
                        break;
                    }
                case SQLCommand.LedgerGroup.FetchValidateGroup:
                    {
                        query = "SELECT " +
                                   "NATURE_ID,PARENT_GROUP_ID " +
                               "FROM " +
                                   "MASTER_LEDGER_GROUP " +
                                   "WHERE GROUP_ID=?GROUP_ID ";
                        break;
                    }
                case SQLCommand.LedgerGroup.FetchAccessFlag:
                    {
                        query = "SELECT " +
                                   "ACCESS_FLAG " +
                               "FROM " +
                                   "MASTER_LEDGER_GROUP " +
                                   "WHERE GROUP_ID=?GROUP_ID ";
                        break;
                    }
                case SQLCommand.LedgerGroup.UpdateImageIndex:
                    {
                        query = "UPDATE MASTER_LEDGER_GROUP " +
                                        "SET IMAGE_ID=0  " +
                                "WHERE GROUP_ID=?GROUP_ID ";
                        break;
                    }
                case SQLCommand.LedgerGroup.FetchAccoutType:
                    {
                        query = "SELECT " +
                                      "ACCOUNT_TYPE_ID , " +
                                      "ACCOUNT_TYPE  " +
                                  "FROM " +
                                      "MASTER_ACCOUNT_TYPE ";
                        break;
                    }
                case SQLCommand.LedgerGroup.FetchLedgerGroupCodes:
                    {
                        query = "SELECT GROUP_CODE AS 'USED_CODE',LEDGER_GROUP AS 'NAME' FROM MASTER_LEDGER_GROUP ORDER BY GROUP_ID DESC";
                        break;
                    }
                case SQLCommand.LedgerGroup.FetchLedgerGroupByGroupCode:
                    {
                        query = "SELECT GROUP_CODE AS 'EXIST_CODE' FROM MASTER_LEDGER_GROUP WHERE GROUP_CODE=?GROUP_CODE";
                        break;
                    }
                case SQLCommand.LedgerGroup.FetchFDLedger:
                    {
                        query = "SELECT GROUP_ID, LEDGER_GROUP,NATURE_ID FROM  MASTER_LEDGER_GROUP WHERE GROUP_ID IN (14) ORDER BY LEDGER_GROUP ASC";
                        break;
                    }
                case SQLCommand.LedgerGroup.IsLedgerGroupCode:
                    {
                        query = "SELECT COUNT(*) FROM MASTER_LEDGER_GROUP WHERE GROUP_CODE=?GROUP_CODE";
                        break;
                    }
                case SQLCommand.LedgerGroup.IsLedgerGroupName:
                    {
                        query = "SELECT COUNT(*) FROM MASTER_LEDGER_GROUP WHERE LEDGER_GROUP=?LEDGER_GROUP";
                        break;
                    }
                case SQLCommand.LedgerGroup.GetParentId:
                    {
                        query = "SELECT GROUP_ID FROM MASTER_LEDGER_GROUP WHERE LEDGER_GROUP=?LEDGER_GROUP";
                        break;
                    }
                case SQLCommand.LedgerGroup.GetNatureId:
                    {
                        query = "SELECT NATURE_ID FROM MASTER_NATURE WHERE NATURE=?NATURE";
                        break;
                    }
                case SQLCommand.LedgerGroup.GetLedgerGroupId:
                    {
                        query = "SELECT GROUP_ID FROM MASTER_LEDGER_GROUP WHERE LEDGER_GROUP=?LEDGER_GROUP";
                        break;
                    }
                case SQLCommand.LedgerGroup.FetchLedgerGroupId:
                    {
                        query = "SELECT GROUP_ID FROM MASTER_LEDGER_GROUP ORDER BY GROUP_ID DESC";
                        break;
                    }
                case SQLCommand.LedgerGroup.GetNatureIdByLedgerGroup:
                    {
                        query = "SELECT NATURE_ID FROM MASTER_LEDGER_GROUP WHERE LEDGER_GROUP=?LEDGER_GROUP";
                        break;
                    }
                case SQLCommand.LedgerGroup.FetchSortOrder:
                    {
                        query = "SELECT MAX(SORT_ORDER) AS SORT_ORDER FROM MASTER_LEDGER_GROUP WHERE PARENT_GROUP_ID=?PARENT_GROUP_ID";
                        break;
                    }
                case SQLCommand.LedgerGroup.FetchMainGroupSortOrder:
                    {
                        query = "SELECT SORT_ORDER FROM MASTER_LEDGER_GROUP WHERE GROUP_ID=?GROUP_ID";
                        break;
                    }
                case SQLCommand.LedgerGroup.FetchLedgerGroupByNature:
                    {
                        query = "SELECT GROUP_ID, LEDGER_GROUP FROM MASTER_LEDGER_GROUP WHERE NATURE_ID IN (?NATURE_ID) AND MAIN_GROUP_ID IN (?NATURE_ID) ORDER BY LEDGER_GROUP;";
                        break;
                    }
                case SQLCommand.LedgerGroup.FetchLedgerGroupNature:
                    {
                        query = "SELECT NATURE_ID, NATURE FROM MASTER_NATURE;";
                        break;
                    }
                case SQLCommand.LedgerGroup.UpdateLedgerGroupByLedgerGroupId:
                    {
                        query = "UPDATE MASTER_LEDGER_GROUP SET LEDGER_GROUP = ?LEDGER_GROUP WHERE GROUP_ID = ?GROUP_ID";
                        break;
                    }
            }
            return query;
        }
        #endregion User SQL
    }
}
