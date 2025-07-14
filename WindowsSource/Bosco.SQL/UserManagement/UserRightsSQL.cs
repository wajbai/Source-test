using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class UserRightsSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.UserRights).FullName)
            {
                query = GetUserRights();
            }

            sqlType = this.sqlType;
            return query;
        }

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the inkind article details.
        /// </summary>
        /// <returns></returns>
        private string GetUserRights()
        {
            string query = "";
            SQLCommand.UserRights sqlCommandId = (SQLCommand.UserRights)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case
                SQLCommand.UserRights.FetchAll: // to remove the stock and statutory values
                    {
                        //                        query = @"SELECT IF(UR.ACTIVITY_ID IS NULL, 0, 1) AS HAS_RIGHTS,
                        //                                            AR. ACTIVITY_ID,
                        //                                            PARENT_ID,
                        //                                            OBJECT_NAME,
                        //                                            OBJECT_TYPE,
                        //                                            OBJECT_SUB_TYPE
                        //                                        FROM ACTIVITIY_RIGHTS AS AR
                        //                                        LEFT JOIN USER_RIGHTS UR
                        //                                        ON UR.ACTIVITY_ID = AR.ACTIVITY_ID
                        //                                         AND UR.USER_ROLE_ID=?USER_ROLE_ID        
                        //                                    WHERE PARENT_ID NOT IN (0,1,21) ORDER BY ACTIVITY_ID;";
                        query = @"SELECT IF(UR.ACTIVITY_ID IS NULL, 0, 1) AS HAS_RIGHTS,
                                    AR.ACTIVITY_ID,
                                    PARENT_ID,
                                    OBJECT_NAME,
                                    OBJECT_TYPE,
                                    OBJECT_SUB_TYPE FROM ACTIVITIY_RIGHTS AS AR
                                    LEFT JOIN USER_RIGHTS UR
                                    ON UR.ACTIVITY_ID = AR.ACTIVITY_ID
                                    AND UR.USER_ROLE_ID=?USER_ROLE_ID WHERE OBJECT_TYPE <> 'TDS' AND OBJECT_TYPE <> 'stock' GROUP BY ACTIVITY_ID  ORDER BY OBJECT_NAME ASC";
                        break;
                    }
                case SQLCommand.UserRights.FetchProjectMapped:
                    {
                        query = @"SELECT IF( UP.PROJECT_ID IS NULL,0,1) AS SELECT_COL, MP.PROJECT_ID, PROJECT_CODE,PROJECT,DIVISION,
                                 DATE_STARTED,DATE_CLOSED,DESCRIPTION
                                 FROM MASTER_PROJECT MP
                                     INNER JOIN MASTER_DIVISION MD ON
                                     MP.DIVISION_ID=MD.DIVISION_ID
                                    LEFT JOIN USER_PROJECT UP ON
                                    UP.PROJECT_ID=MP.PROJECT_ID AND ROLE_ID=?USER_ROLE_ID
                                     WHERE DELETE_FLAG<>1 ORDER BY SELECT_COL DESC";
                        break;
                    }
                case SQLCommand.UserRights.FetchUserProject:
                    {
                        query = @"SELECT GROUP_CONCAT(MP.PROJECT_ID) AS PROJECT_ID FROM MASTER_PROJECT MP
                                INNER JOIN USER_PROJECT UP ON UP.PROJECT_ID = MP.PROJECT_ID WHERE UP.ROLE_ID =?USER_ROLE_ID";
                        break;
                    }
                case SQLCommand.UserRights.Fetch:
                    {
                        query = " SELECT " +
                                " USER_NAME, " +
                                " CASE ROLE_ID WHEN  1  THEN 'Admin' " +
                                " ELSE '' END AS USERROLE, " +
                                " ADDRESS,CONTACT_NO, " +
                                " EMAIL_ID " +
                                " FROM " +
                                " USER_INFO" +
                                " WHERE USER_NAME IS NOT NULL " +
                                " ORDER BY USER_NAME ASC";
                        break;
                    }
                case SQLCommand.UserRights.Update:
                    {
                        query = "UPDATE ACTIVITIY_RIGHTS AS AR SET " +
                            "OBJECT_NAME=?OBJECT_NAME ," +
                            " AR.ADD=?ADD ," +
                            " AR.EDIT=?EDIT ," +
                            " AR.DELETE=?DELETE ," +
                            " AR.VIEW=?VIEW ," +
                            " AR.PRINT=?PRINT ," +
                            " AR.EXPORT=?EXPORT " +
                            " WHERE AR.ID=?ID";
                        break;
                    }
                case SQLCommand.UserRights.UpdateUserRights:
                    {
                        query = "INSERT INTO USER_RIGHTS(USER_ROLE_ID, ACTIVITY_ID) VALUES(?USER_ROLE_ID, ?ACTIVITY_ID);";
                        break;
                    }
                case SQLCommand.UserRights.UpdateUserProject:
                    {
                        query = "INSERT INTO USER_PROJECT(ROLE_ID, PROJECT_ID) VALUES(?USER_ROLE_ID, ?PROJECT_ID);";
                        break;
                    }
                case SQLCommand.UserRights.DeleteUserRights:
                    {
                        query = "DELETE FROM USER_RIGHTS WHERE USER_ROLE_ID=?USER_ROLE_ID;";
                        break;
                    }
                case SQLCommand.UserRights.DeleteUserProject:
                    {
                        query = "DELETE FROM USER_PROJECT WHERE ROLE_ID=?USER_ROLE_ID;";
                        break;
                    }
                case SQLCommand.UserRights.FetchUserRightsByRole:
                    {
                        query = "SELECT AR.ACTIVITY_ID,\n" +
                        "       PARENT_ID,\n" +
                        "       OBJECT_NAME, AR.ENUMTYPE,\n" +
                        "       OBJECT_TYPE,\n" +
                        "       OBJECT_SUB_TYPE,\n" +
                        "       USER_ROLE_ID,\n" +
                        "       UR.ACTIVITY_ID AS 'USER_ACTIVITY_ID'\n" +
                        "  FROM ACTIVITIY_RIGHTS AS AR\n" +
                        "  LEFT JOIN USER_RIGHTS AS UR\n" +
                        "    ON AR.ACTIVITY_ID = UR.ACTIVITY_ID\n" +
                        " WHERE UR.USER_ROLE_ID = ?USER_ROLE_ID;";

                        break;
                    }
            }

            return query;
        }
        #endregion Bank SQL

        #endregion
    }
}
