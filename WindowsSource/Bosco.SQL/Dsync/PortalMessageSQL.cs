using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class PortalMessageSQL : IDatabaseQuery
    {
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;
        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.PortalMessage).FullName)
            {
                query = GetMasterQuery();
            }

            sqlType = this.sqlType;
            return query;
        }
        private string GetMasterQuery()
        {
            string query = string.Empty;
            SQLCommand.PortalMessage sqlCommandId = (SQLCommand.PortalMessage)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {

                case SQLCommand.PortalMessage.AddDataSynMessage:
                    {
                        query = "INSERT INTO PORTAL_DATASYN_MESSAGE(\n" +
                                    "ID,\n" +
                                    "UPLOADED_ON,\n" +
                                    "STATUS,\n" +
                                    "TRANS_DATE_FROM,\n" +
                                    "REFRESH_DATE,\n" +
                                    "TRANS_DATE_TO,\n" +
                                    "STARTED_ON,\n" +
                                    "COMPLETED_ON,\n" +
                                    "REMARKS)\n" +
                                    "VALUES(\n" +
                                    "?ID,\n" +
                                    "?UPLOADED_ON,\n" +
                                    "?STATUS,\n" +
                                    "?TRANS_DATE_FROM,\n" +
                                    "?REFRESH_DATE,\n" +
                                    "?TRANS_DATE_TO,\n" +
                                    "?STARTED_ON,\n" +
                                    "?COMPLETED_ON,\n" +
                                    "?REMARKS)";
                        break;
                    }
                case SQLCommand.PortalMessage.AddAmenmentMessage:
                    {
                        query = "INSERT INTO PORTAL_AMENDMENT_MESSAGE(\n" +
                                    "VOUCHER_ID,\n" +
                                    "VOUCHER_DATE,\n" +
                                    "AMENDMENT_DATE,\n" +
                                    "LEDGER_NAME,\n" +
                                    "PROJECT,\n" +
                                    "VOUCHER_NO,\n" +
                                    "VOUCHER_TYPE,\n" +
                                    "AMOUNT,\n" +
                                    "REMARKS)\n" +
                                    "VALUES\n" +
                                    "(?VOUCHER_ID,\n" +
                                    "?VOUCHER_DATE,\n" +
                                    "?AMENDMENT_DATE,\n" +
                                    "?LEDGER_NAME,\n" +
                                    "?PROJECT,\n" +
                                    "?VOUCHER_NO,\n" +
                                    "?VOUCHER_TYPE,\n" +
                                    "?AMOUNT,\n" +
                                    "?REMARKS)";
                        break;
                    }
                case SQLCommand.PortalMessage.DeleteDataSynMessage:
                    {
                        query = "DELETE FROM PORTAL_DATASYN_MESSAGE";
                        break;
                    }
                case SQLCommand.PortalMessage.DeleteAmendmentMessage:
                    {
                        query = "DELETE  FROM PORTAL_AMENDMENT_MESSAGE";
                        break;
                    }
                case SQLCommand.PortalMessage.FetchPortalDataSynMessage:
                    {
                        query = "SELECT ID,\n" +
                                "     DATE_FORMAT(UPLOADED_ON,'%d-%b-%Y-%r') AS  UPLOADED_ON,\n" +
                                "       STATUS,\n" +
                                "       REMARKS,\n" +
                                "       REFRESH_DATE,\n" +
                                "      DATE(TRANS_DATE_FROM) AS  TRANS_DATE_FROM,\n" +
                                "      DATE(TRANS_DATE_TO) AS  TRANS_DATE_TO,\n" +
                                "     STARTED_ON,\n" +
                                "      COMPLETED_ON \n" +
                                "       TYPE\n" +
                                "  FROM PORTAL_DATASYN_MESSAGE ORDER BY CAST(UPLOADED_ON as DATETIME) DESC;"; //ID // UPLOADED_ON
                        break;
                    }
                case SQLCommand.PortalMessage.FetchPortalAmendmentMessage:
                    {
                        query = "SELECT ID,DATE_FORMAT(AMENDMENT_DATE,'%d-%b-%Y') AS AMENDMENT_DATE,DATE_FORMAT(VOUCHER_DATE,'%d-%b-%Y') AS VOUCHER_DATE,VOUCHER_TYPE,LEDGER_NAME,VOUCHER_NO,PROJECT, REMARKS \n" +
                                "  FROM PORTAL_AMENDMENT_MESSAGE PAM ORDER BY CAST(AMENDMENT_DATE as DATETIME) DESC";  //ID  // AMENDMENT_DATE
                        // " INNER JOIN MASTER_PROJECT MP\n" +
                        //"    ON MP.PROJECT = PAM.PROJECT\n" +
                        //" WHERE PROJECT_ID = ?PROJECT_ID;";
                        break;
                    }
                case SQLCommand.PortalMessage.AddBroadCastMessage:
                    {
                        query = "INSERT INTO HEAD_MESSAGE(\n" +
                                    "ID,\n" +
                                    "SUBJECT,\n" +
                                    "CONTENT,\n" +
                                    "DATE,\n" +
                                    "TYPE)\n" +
                                    "VALUES\n" +
                                    "(?ID,\n" +
                                    "?SUBJECT,\n" +
                                    "?CONTENT,\n" +
                                    "?DATE,\n" +
                                    "?TYPE)";
                        break;
                    }
                case SQLCommand.PortalMessage.DeleteBroadCastMessage:
                    {
                        query = "DELETE FROM HEAD_MESSAGE";
                        break;
                    }
                case SQLCommand.PortalMessage.FetchBroadCastMessage:
                    {
                        query = "SELECT ID,DATE_FORMAT(DATE,'%d-%b-%Y') AS DATE,SUBJECT,CONTENT,TYPE FROM HEAD_MESSAGE ORDER BY CAST(DATE AS DATETIME) DESC";
                        break;
                    }

                #region Trouble Tickets
                case SQLCommand.PortalMessage.FetchTroubleTickets:
                    {
                        //query = "SELECT TICKET_ID,\n" +
                        //        "       SUBJECT,\n" +
                        //        "       DESCRIPTION,\n" +
                        //        "       PRIORITY,\n" +
                        //        "       POSTED_DATE,\n" +
                        //        "       COMPLETED_DATE,\n" +
                        //        "       ATTACH_FILE_NAME,\n" +
                        //        "       POSTED_BY,\n" +
                        //        "       REPLIED_TICKET_ID,\n" +
                        //        "       USER_NAME,\n" +
                        //        "       PHYSICAL_FILE_NAME,\n" +
                        //        "       STATUS\n" +
                        //        "  FROM TROUBLE_TICKET";
                        //query = "SELECT TICKET_ID,\n" +
                        //        "       IF(REPLIED_TICKET_ID>0,CONCAT('  ',SUBJECT),SUBJECT) AS SUBJECT,\n" +
                        //        "       DESCRIPTION,PRIORITY,USER_NAME,POSTED_BY,\n" +
                        //        "       DATE_FORMAT(POSTED_DATE,'%d-%b-%Y') AS POSTED_DATE,REPLIED_TICKET_ID\n" +
                        //        "  FROM TROUBLE_TICKET";
                        query = "SELECT TICKET_ID,\n" +
                                "       IF(REPLIED_TICKET_ID > 0, CONCAT('  ', SUBJECT), SUBJECT) AS SUBJECT,\n" +
                                "       DESCRIPTION,\n" +
                                "       PRIORITY,\n" +
                                "       IF(REPLIED_TICKET_ID > 0, @pvname, USER_NAME) AS USER_NAME,\n" +
                                "       POSTED_BY,\n" +
                                "       @PRVID := IF(REPLIED_TICKET_ID = 0, TICKET_ID, @PRVID) AS PRVID,\n" +
                                "       @PVNAME := USER_NAME AS D,\n" +
                                "       DATE_FORMAT(POSTED_DATE, '%d-%b-%Y') AS POSTED_DATE,\n" +
                                "       REPLIED_TICKET_ID\n" +
                                "  FROM TROUBLE_TICKET,\n" +
                                "       (SELECT @PVNAME := 0) AS X,\n" +
                                "       (SELECT @PRVID := 0) AS Y;";

                        break;
                    }
                case SQLCommand.PortalMessage.AddTroubleTickets:
                    {
                        query = "INSERT INTO TROUBLE_TICKET\n" +
                                "  (TICKET_ID,\n" +
                                "   SUBJECT,\n" +
                                "   DESCRIPTION,\n" +
                                "   PRIORITY,\n" +
                                "   POSTED_DATE,\n" +
                                "   COMPLETED_DATE,\n" +
                                "   ATTACH_FILE_NAME,\n" +
                                "   POSTED_BY,\n" +
                                "   REPLIED_TICKET_ID,\n" +
                                "   USER_NAME,\n" +
                                "   PHYSICAL_FILE_NAME,\n" +
                                "   STATUS)\n" +
                                "VALUES\n" +
                                "  (?TICKET_ID,\n" +
                                "   ?SUBJECT,\n" +
                                "   ?DESCRIPTION,\n" +
                                "   ?PRIORITY,\n" +
                                "   ?POSTED_DATE,\n" +
                                "   ?COMPLETED_DATE,\n" +
                                "   ?ATTACH_FILE_NAME,\n" +
                                "   ?POSTED_BY,\n" +
                                "   ?REPLIED_TICKET_ID,\n" +
                                "   ?USER_NAME,\n" +
                                "   ?PHYSICAL_FILE_NAME,\n" +
                                "   ?STATUS);";
                        break;
                    }
                case SQLCommand.PortalMessage.DeleteTroubleTickets:
                    {
                        query = "DELETE FROM TROUBLE_TICKET";
                        break;
                    }
                #endregion

                #region Trouble Tickets
                case SQLCommand.PortalMessage.FetchUserManualFeature:
                    {
                        query = @"SELECT FEATURE_CODE, FEATURE_GROUP_CODE,FEATURE_GROUP, FEATURE, LINK_FILENAME
                                    FROM MASTER_USERMANUAL_FEATURE";
                        break;
                    }
                #endregion

                #region User Manual and Paid Features
                case SQLCommand.PortalMessage.DeleteUserMaualsPaidFeatures:
                    {
                        query = @"DELETE FROM MASTER_USERMANUAL_FEATURE";
                        break;
                    }
                case SQLCommand.PortalMessage.UpdateUserMaualsPaidFeatures:
                    {
                        query = "INSERT INTO MASTER_USERMANUAL_FEATURE (FEATURE_CODE, FEATURE_GROUP_CODE, FEATURE_GROUP, FEATURE, LINK_FILENAME)\n" +
                                 "VALUES (?FEATURE_CODE, ?FEATURE_GROUP_CODE, ?FEATURE_GROUP, ?FEATURE, ?LINK_FILENAME)\n";
                        break;
                    }
                #endregion


            }
            return query;
        }
    }
}
