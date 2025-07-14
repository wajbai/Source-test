using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;


namespace Bosco.SQL
{
    public class SubBranchSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.SubBranchList).FullName)
            {
                query = GetCostCentreSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the cost centre details.
        /// </summary>
        /// <returns></returns>
        private string GetCostCentreSQL()
        {
            string query = "";
            SQLCommand.SubBranchList sqlCommandId = (SQLCommand.SubBranchList)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.SubBranchList.MapProjectBranch:
                    {
                        query = "INSERT INTO PROJECT_BRANCH\n" +
                                "(PROJECT_ID, BRANCH_ID)\n" +
                                "VALUES\n" +
                                "(?PROJECT_ID, ?BRANCH_OFFICE_ID);";
                        break;
                    }
                case SQLCommand.SubBranchList.DeleteMappedProjects:
                    {
                        query = "DELETE FROM PROJECT_BRANCH WHERE BRANCH_ID = ?BRANCH_OFFICE_ID;";
                        break;
                    }
                case SQLCommand.SubBranchList.FetchAllBranches:
                    {
                        query = "   SELECT BRANCH_OFFICE_ID,\n" +
                                 "   BRANCH_OFFICE_CODE,\n" +
                                 "   BRANCH_OFFICE_NAME,\n" +
                                 "   HEAD_OFFICE_CODE,\n" +
                                 "   CREATED_DATE,\n" +
                                 "   CREATED_BY,\n" +
                                 "   DEPLOYMENT_TYPE,\n" +
                                 "   ADDRESS,\n" +
                                 "   STATE_ID,\n" +
                                 "   PINCODE,\n" +
                                 "   COUNTRY_ID,\n" +
                                 "   PHONE_NO,\n" +
                                 "   MOBILE_NO,\n" +
                                 "   BRANCH_EMAIL_ID,\n" +
                                 "   STATUS,\n" +
                                 "   MODIFIED_DATE,\n" +
                                 "   MODIFIED_BY,\n" +
                                 "   USER_CREATED_STATUS,\n" +
                                 "   CITY,\n" +
                                 "   BRANCH_PART_CODE,\n" +
                                 "   COUNTRY_CODE,\n" +
                                 "   BRANCH_KEY_CODE\n" +
                                 "  FROM BRANCH_OFFICE;";

                        break;
                    }
                case SQLCommand.SubBranchList.AddBranch:
                    {
                        query = "INSERT INTO BRANCH_OFFICE( " +
                                 "   BRANCH_OFFICE_CODE,\n" +
                                 "   BRANCH_OFFICE_NAME,\n" +
                                 "   HEAD_OFFICE_CODE,\n" +
                                 "   DEPLOYMENT_TYPE,\n" +
                                 "   ADDRESS,\n " +
                                 "   PINCODE, \n" +
                                 "   PHONE_NO,\n " +
                                 "   MOBILE_NO,\n " +
                                 "   BRANCH_EMAIL_ID,\n" +
                                 "   STATUS,\n" +
                                 "   CITY,\n" +
                                 "   BRANCH_PART_CODE, \n" +
                                 "   COUNTRY_CODE,\n " +
                                 "   BRANCH_KEY_CODE )" +
                                 "VALUES( " +
                                 "   ?BRANCH_OFFICE_CODE,\n" +
                                 "   ?BRANCH_OFFICE_NAME,\n" +
                                 "   ?HEAD_OFFICE_CODE,\n" +
                                 "   ?DEPLOYMENT_TYPE,\n" +
                                 "   ?ADDRESS,\n " +
                                 "   ?PINCODE,\n " +
                                 "   ?PHONE_NO,\n " +
                                 "   ?MOBILE_NO,\n " +
                                 "   ?BRANCH_EMAIL_ID,\n" +
                                 "   ?STATUS,\n" +
                                 "   ?CITY,\n" +
                                 "   ?BRANCH_PART_CODE,\n " +
                                 "   ?COUNTRY_CODE,\n " +
                                 "   ?BRANCH_KEY_CODE )";
                        break;
                    }
                case SQLCommand.SubBranchList.UpdateBranch:
                    {
                        query = "UPDATE BRANCH_OFFICE SET " +
                                "   BRANCH_OFFICE_NAME=?BRANCH_OFFICE_NAME,\n" +
                                "   HEAD_OFFICE_CODE= ?HEAD_OFFICE_CODE,\n" +
                                "   DEPLOYMENT_TYPE=?DEPLOYMENT_TYPE,\n" +
                                "   ADDRESS =?ADDRESS,\n " +
                                "   PINCODE =?PINCODE,\n " +
                                "   PHONE_NO =?PHONE_NO,\n " +
                                "   MOBILE_NO =?MOBILE_NO,\n " +
                                "   BRANCH_EMAIL_ID=?BRANCH_EMAIL_ID,\n" +
                                "   STATUS =?STATUS,\n" +
                                "   CITY =?CITY,\n" +
                                "   BRANCH_PART_CODE =?BRANCH_PART_CODE,\n " +
                                "   COUNTRY_CODE =?COUNTRY_CODE,\n " +
                                "   BRANCH_KEY_CODE=?BRANCH_KEY_CODE" +
                                "  WHERE BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE";
                        break;
                    }
                case SQLCommand.SubBranchList.FetchMappedProjects:
                    {
                        //query = " SELECT MP.PROJECT_ID, MP.PROJECT, IF(PB.BRANCH_ID = ?BRANCH_OFFICE_ID, 1, 0) AS 'MAPPED_STATUS',\n" +
                        //        " CASE WHEN SUM(LB.AMOUNT) > 0\n" +
                        //        " THEN 1\n" + 
                        //        " ELSE 0\n" + 
                        //        " END AS 'HAS_BALANCE'\n" +
                        //        "  FROM PROJECT_BRANCH PB\n" +
                        //        " RIGHT JOIN MASTER_PROJECT MP\n" +
                        //        "    ON PB.PROJECT_ID = MP.PROJECT_ID\n" +
                        //        " LEFT JOIN LEDGER_BALANCE LB ON\n" +
                        //        " MP.PROJECT_ID=LB.PROJECT_ID GROUP BY PROJECT_ID;";
                        query = " SELECT MP.PROJECT_ID,\n" +
                                "       CONCAT(MP.PROJECT, CONCAT(' - ', MD.DIVISION)) AS 'PROJECT',\n" +
                                "       (SELECT ' ') AS TRANS_MODE,\n" +
                                "       IF(PB.BRANCH_ID =?BRANCH_OFFICE_ID, 1, 0) AS 'MAPPED_STATUS',TP.PROJECT_ID AS HAS_BALANCE\n" +
                                "  FROM MASTER_PROJECT MP\n" +
                                " INNER JOIN MASTER_DIVISION AS MD\n" +
                                "    ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                                "  LEFT JOIN PROJECT_BRANCH PB\n" +
                                "    ON MP.PROJECT_ID = PB.PROJECT_ID\n" +
                                "   AND PB.BRANCH_ID = ?BRANCH_OFFICE_ID\n" +
                                " LEFT JOIN\n" +
                                " (SELECT PROJECT_ID,BRANCH_ID FROM LEDGER_BALANCE\n" +
                                " WHERE BRANCH_ID=?BRANCH_OFFICE_ID GROUP BY BRANCH_ID,PROJECT_ID ) AS TP\n" +
                                " ON MP.PROJECT_ID=TP.PROJECT_ID\n" +
                                " WHERE MP.DELETE_FLAG <> 1\n" +
                                " ORDER BY MP.PROJECT ASC;";


                        break;
                    }
                case SQLCommand.SubBranchList.FetchBranchCode:
                    {
                        query = "SELECT BRANCH_OFFICE_CODE FROM BRANCH_OFFICE WHERE BRANCH_OFFICE_ID=?BRANCH_OFFICE_ID";
                        break;
                    }
                case SQLCommand.SubBranchList.FetchDSyncStatus:
                    {
                        query = "SELECT DTS.ID,BO.BRANCH_OFFICE_CODE,\n" +
                                "       DATE_FORMAT(DTS.UPLOADED_ON,'%d/%m/%Y %h:%i %p') AS UPLOADED_ON,\n" +
                                "       DATE_FORMAT(DTS.STARTED_ON,'%d/%m/%Y %h:%i %p') AS STARTED_ON,\n" +
                                "       DATE_FORMAT(DTS.COMPLETED_ON,'%d/%m/%Y %h:%i %p') AS COMPLETED_ON,\n" +
                                "       XML_FILENAME,\n" +
                                "       DSS.STATUS,\n" +
                                "       REMARKS,\n" +
                                "       TRANS_DATE_FROM,\n" +
                                "       TRANS_DATE_TO\n" +
                                "  FROM DATASYNC_TASK DTS\n" +
                                "  LEFT JOIN DATASYNC_STATUS DSS\n" +
                                "    ON DTS.STATUS = DSS.ID\n" +
                                "  LEFT JOIN BRANCH_OFFICE BO\n" +
                                "    ON BO.BRANCH_OFFICE_ID = DTS.BRANCH_OFFICE_ID;";
                        break;
                    }
                case SQLCommand.SubBranchList.FetchDsyncStatusById:
                    {
                        query = "SELECT BO.BRANCH_OFFICE_CODE,\n" +
                                "       DATE_FORMAT(DTS.UPLOADED_ON,'%d/%m/%Y %h:%i %p') AS UPLOADED_ON,\n" +
                                "       DATE_FORMAT(DTS.STARTED_ON,'%d/%m/%Y %h:%i %p') AS STARTED_ON,\n" +
                                "       DATE_FORMAT(DTS.COMPLETED_ON,'%d/%m/%Y %h:%i %p') AS COMPLETED_ON,\n" +
                                "       XML_FILENAME,\n" +
                                "       DSS.STATUS,\n" +
                                "       REMARKS,\n" +
                                "       TRANS_DATE_FROM,\n" +
                                "       TRANS_DATE_TO\n" +
                                "  FROM DATASYNC_TASK DTS\n" +
                                "  LEFT JOIN DATASYNC_STATUS DSS\n" +
                                "    ON DTS.STATUS = DSS.ID\n" +
                                "  LEFT JOIN BRANCH_OFFICE BO\n" +
                                "    ON BO.BRANCH_OFFICE_ID = DTS.BRANCH_OFFICE_ID\n" +
                                "  WHERE DTS.ID=?ID;";
                        break;
                    }
                case SQLCommand.SubBranchList.SaveDSyncStatus:
                    {
                        query= "INSERT INTO DATASYNC_TASK\n" +
                                "  (BRANCH_OFFICE_ID, UPLOADED_ON, XML_FILENAME, STATUS, REMARKS)\n" +
                                "VALUES\n" +
                                "  (?BRANCH_OFFICE_ID, NOW(), ?XML_FILENAME, ?STATUS, ?REMARKS);";
                        break;
                    }
                case SQLCommand.SubBranchList.AuthenticateBranchCode:
                    {
                        query = "SELECT BRANCH_OFFICE_ID,BRANCH_OFFICE_CODE FROM BRANCH_OFFICE WHERE BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE";
                        break;
                    }
            }
            return query;
        }
        #endregion Bank SQL
    }
}
