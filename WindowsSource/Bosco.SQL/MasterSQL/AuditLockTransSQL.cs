using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class AuditLockTransSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.AuditLockTrans).FullName)
            {
                query = GetStateSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the bank details.
        /// </summary>
        /// <returns></returns>
        private string GetStateSQL()
        {
            string query = "";
            SQLCommand.AuditLockTrans sqlCommandId = (SQLCommand.AuditLockTrans)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.AuditLockTrans.AddAuditType:
                    {
                        query = "INSERT INTO MASTER_LOCK_TYPE(LOCK_TYPE)VALUES(?LOCK_TYPE)";
                        break;
                    }
                case SQLCommand.AuditLockTrans.AddAuditTrans:
                    {
                        query = "INSERT INTO MASTER_LOCK_TRANS\n" +
                         "  (LOCK_TYPE_ID, PROJECT_ID, DATE_FROM, DATE_TO, PASSWORD, REASON,PASSWORD_HINT,LOCK_BY_PORTAL)\n" +
                         "VALUES\n" +
                         "  (?LOCK_TYPE_ID, ?PROJECT_ID, ?DATE_FROM, ?DATE_TO, ?PASSWORD, ?REASON,?PASSWORD_HINT,?LOCK_BY_PORTAL)";
                        break;
                    }

                case SQLCommand.AuditLockTrans.UpdateAuditType:
                    {
                        query = "UPDATE MASTER_LOCK_TYPE SET LOCK_TYPE=?LOCK_TYPE WHERE LOCK_TYPE_ID=?LOCK_TYPE_ID";
                        break;
                    }

                case SQLCommand.AuditLockTrans.UpdateAuditTrans:
                    {
                        query = "UPDATE MASTER_LOCK_TRANS\n" +
                        "   SET LOCK_TYPE_ID  = ?LOCK_TYPE_ID,\n" +
                        "       PROJECT_ID    = ?PROJECT_ID,\n" +
                        "       DATE_FROM     = ?DATE_FROM,\n" +
                        "       DATE_TO       = ?DATE_TO,\n" +
                        "       PASSWORD      = ?PASSWORD,\n" +
                        "       REASON        = ?REASON,\n" +
                        "       PASSWORD_HINT = ?PASSWORD_HINT,\n" +
                        "       LOCK_BY_PORTAL = ?LOCK_BY_PORTAL\n" +
                        " WHERE LOCK_TRANS_ID = ?LOCK_TRANS_ID;";
                        break;
                    }
                case SQLCommand.AuditLockTrans.DeleteAuditType:
                    {
                        query = "DELETE FROM MASTER_LOCK_TYPE WHERE LOCK_TYPE_ID=?LOCK_TYPE_ID";
                        break;
                    }

                case SQLCommand.AuditLockTrans.DeleteAuditTrans:
                    {
                        query = "DELETE FROM MASTER_LOCK_TRANS WHERE LOCK_TRANS_ID=?LOCK_TRANS_ID";
                        break;
                    }
                case SQLCommand.AuditLockTrans.DeleteAuditTransByProject:
                    {
                        query = "DELETE FROM MASTER_LOCK_TRANS WHERE PROJECT_ID=?PROJECT_ID";
                        break;
                    }
                case SQLCommand.AuditLockTrans.DeleteAuditTransByLockbyPortal:
                    {
                        query = "DELETE FROM MASTER_LOCK_TRANS WHERE LOCK_BY_PORTAL=?LOCK_BY_PORTAL";
                        break;
                    }
                case SQLCommand.AuditLockTrans.FetchAuditType:
                    {
                        query = "SELECT LOCK_TYPE_ID ,LOCK_TYPE FROM MASTER_LOCK_TYPE WHERE LOCK_TYPE_ID=?LOCK_TYPE_ID";
                        break;
                    }
                case SQLCommand.AuditLockTrans.FetchAuditTypeByType:
                    {
                        query = "SELECT LOCK_TYPE_ID FROM MASTER_LOCK_TYPE WHERE LOCK_TYPE=?LOCK_TYPE";
                        break;
                    }
                case SQLCommand.AuditLockTrans.FetchAuditTrans:
                    {
                        query = "SELECT LOCK_TRANS_ID, LOCK_TYPE_ID, PROJECT_ID, DATE_FROM, DATE_TO, PASSWORD, REASON,PASSWORD_HINT,LOCK_BY_PORTAL FROM MASTER_LOCK_TRANS WHERE  LOCK_TRANS_ID=?LOCK_TRANS_ID";
                        break;
                    }
                case SQLCommand.AuditLockTrans.FetchAllAuditType:
                    {
                        query = "SELECT LOCK_TYPE_ID,LOCK_TYPE FROM MASTER_LOCK_TYPE ORDER BY LOCK_TYPE ASC";
                        break;
                    }
                case SQLCommand.AuditLockTrans.FetchAllAuditTrans:
                    {
                        query = "SELECT LOCK_TRANS_ID,\n" +
                         "       MLT.LOCK_TYPE_ID,\n" +
                         "       MLT.PROJECT_ID,\n" +
                         "       MP.PROJECT,\n" +
                         "       MT.LOCK_TYPE,\n" +
                         "       DATE_FROM,\n" +
                         "       DATE_TO,\n" +
                         "       PASSWORD,\n" +
                         "       REASON,\n" +
                         "      MLT.PASSWORD_HINT, CASE WHEN MLT.LOCK_BY_PORTAL=1 THEN 'Yes' ELSE 'No' END AS LOCK_BY_PORTAL\n" +
                         "  FROM MASTER_LOCK_TRANS AS MLT\n" +
                         "  LEFT JOIN MASTER_PROJECT AS MP\n" +
                         "    ON MLT.PROJECT_ID = MP.PROJECT_ID\n" +
                         "  LEFT JOIN MASTER_LOCK_TYPE AS MT\n" +
                         "    ON MLT.LOCK_TYPE_ID = MT.LOCK_TYPE_ID\n" +
                         " ORDER BY LOCK_BY_PORTAL DESC, DATE_FROM ASC ";

                        break;
                    }
                case SQLCommand.AuditLockTrans.FetchAuditLockDetailsForProject:
                    {
                        //query = " SELECT DATE_FROM,DATE_TO  FROM MASTER_LOCK_TRANS WHERE PROJECT_ID=?PROJECT_ID AND DATE_FROM<=?DATE_TO";
                        query = " SELECT MLT.DATE_FROM, MLT.DATE_TO FROM, MP.PROJECT MASTER_LOCK_TRANS MLT\n"+
                                    "INNER JOIN MASTER_PROJECT AS MP ON MP.PROJECT_ID = MLT.PROJECT_ID\n"+
                                    "WHERE MLT.PROJECT_ID=?PROJECT_ID";
                        break;
                    }
                case SQLCommand.AuditLockTrans.FetchAuditLockDetailsForProjectAndDate:
                    {
                        //query = " SELECT DATE_FROM, DATE_TO FROM MASTER_LOCK_TRANS WHERE PROJECT_ID=?PROJECT_ID AND DATE_FROM<=?DATE_TO";
                        query = " SELECT MLT.DATE_FROM, MLT.DATE_TO, MP.PROJECT FROM MASTER_LOCK_TRANS MLT\n" +
                                    "INNER JOIN MASTER_PROJECT AS MP ON MP.PROJECT_ID = MLT.PROJECT_ID\n" +
                                    "WHERE MLT.PROJECT_ID=?PROJECT_ID {AND ?VOUCHER_DATE BETWEEN MLT.DATE_FROM AND MLT.DATE_TO}";
                        break;
                    }
                case SQLCommand.AuditLockTrans.FetchAuditLockDetailByProjectDateRange:
                    {
                        query = " SELECT MLT.LOCK_TRANS_ID, MLT.DATE_FROM, MLT.DATE_TO, MP.PROJECT FROM MASTER_LOCK_TRANS MLT\n" +  
                                    "INNER JOIN MASTER_PROJECT AS MP ON MP.PROJECT_ID = MLT.PROJECT_ID\n" +
                                    "WHERE MLT.PROJECT_ID=?PROJECT_ID\n" +
                                    " AND ((?DATE_FROM BETWEEN MLT.DATE_FROM AND MLT.DATE_TO)  OR (?DATE_TO BETWEEN MLT.DATE_FROM AND MLT.DATE_TO))";
                        break;
                    }
                case SQLCommand.AuditLockTrans.FetchAuditLockDetailIdByProjectDateRange:
                    {
                        query = " SELECT LOCK_TRANS_ID FROM MASTER_LOCK_TRANS WHERE PROJECT_ID=?PROJECT_ID\n" +
                                    " AND ((?DATE_FROM BETWEEN DATE_FROM AND DATE_TO)  OR (?DATE_TO BETWEEN DATE_FROM AND DATE_TO))";
                        break;
                    }
                case SQLCommand.AuditLockTrans.FetchLockMastersInVoucherEntry:
                    {
                        query = @"SELECT MVE.MASTER_SOURCE_TYPE, MVE.MASTER_SOURCE_ID, MVE.LOCK_FROM, MVE.LOCK_TO, MVE.ACTION
                                    FROM MASTER_LOCK_VOUCHER_ENTRY MVE
                                    WHERE MVE.ACTION = 1 AND MVE.MASTER_SOURCE_TYPE = ?MASTER_SOURCE_TYPE {AND MVE.MASTER_SOURCE_ID = ?MASTER_SOURCE_ID}
                                    AND ((?VOUCHER_DATE BETWEEN MVE.LOCK_FROM AND MVE.LOCK_TO) OR (MVE.LOCK_FROM<=?VOUCHER_DATE AND MVE.LOCK_TO IS NULL ));";
                        break;
                    }
                case SQLCommand.AuditLockTrans.IsValidPassword:
                    {
                        query = "SELECT COUNT(*) FROM MASTER_LOCK_TRANS WHERE LOCK_TRANS_ID=?LOCK_TRANS_ID AND PASSWORD=?PASSWORD";
                        break;
                    }
                case SQLCommand.AuditLockTrans.ResetPassword:
                    {
                        query = "UPDATE MASTER_LOCK_TRANS SET PASSWORD=?PASSWORD WHERE LOCK_TRANS_ID=?LOCK_TRANS_ID";
                        break;
                    }
                case SQLCommand.AuditLockTrans.ValidatePasswordHint:
                    {
                        query = "SELECT COUNT(*) FROM MASTER_LOCK_TRANS WHERE LOCK_TRANS_ID=?LOCK_TRANS_ID AND PASSWORD_HINT=?PASSWORD_HINT";
                        break;
                    }
            }

            return query;
        }
        #endregion
    }
}
