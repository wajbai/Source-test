using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class ProjectSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.Project).FullName)
            {
                query = GetProjectSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the Project details.
        /// </summary>
        /// <returns></returns>
        private string GetProjectSQL()
        {
            string query = "";
            SQLCommand.Project sqlCommandId = (SQLCommand.Project)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.Project.Add:
                    {
                        query = "INSERT INTO MASTER_PROJECT ( " +
                               "PROJECT_CODE, " +
                               "PROJECT, " +
                               "DIVISION_ID, " +
                               "ACCOUNT_DATE," +
                               "DATE_STARTED," +
                               "DATE_CLOSED," +
                               "DESCRIPTION," +
                               "NOTES,CUSTOMERID, " +
                               "PROJECT_CATEGORY_ID,CONTRIBUTION_ID, CLOSED_BY) VALUES( " +
                               "?PROJECT_CODE, " +
                               "?PROJECT, " +
                               "?DIVISION_ID, " +
                               "?ACCOUNT_DATE," +
                               "?DATE_STARTED," +
                               "?DATE_CLOSED," +
                               "?DESCRIPTION," +
                               "?NOTES, ?CUSTOMERID," +
                               "?PROJECT_CATEGORY_ID,?CONTRIBUTION_ID, ?CLOSED_BY)";
                        break;
                    }
                case SQLCommand.Project.Update:
                    {
                        query = "UPDATE MASTER_PROJECT SET " +
                                    "PROJECT_CODE = ?PROJECT_CODE, " +
                                    "PROJECT =?PROJECT, " +
                                    "DIVISION_ID =?DIVISION_ID, " +
                                    "ACCOUNT_DATE=?ACCOUNT_DATE, " +
                                    "DATE_STARTED=?DATE_STARTED," +
                                    "DATE_CLOSED=?DATE_CLOSED," +
                                    "DESCRIPTION=?DESCRIPTION ," +
                                    "NOTES=?NOTES ," +
                                    "CUSTOMERID=?CUSTOMERID ," +
                                    "PROJECT_CATEGORY_ID =?PROJECT_CATEGORY_ID, " +
                                    "CONTRIBUTION_ID=?CONTRIBUTION_ID," +
                                    "CLOSED_BY=?CLOSED_BY " +
                                    " WHERE PROJECT_ID=?PROJECT_ID ";
                        break;
                    }
                case SQLCommand.Project.UpdateClosedDate:
                    {
                        query = "UPDATE MASTER_PROJECT SET\n" +
                                       "DATE_CLOSED=?DATE_CLOSED, CLOSED_BY=?CLOSED_BY\n" +
                                       "WHERE PROJECT_ID=?PROJECT_ID";
                        break;
                    }
                case SQLCommand.Project.Delete:
                    {
                        query = "DELETE FROM MASTER_PROJECT WHERE PROJECT_ID=?PROJECT_ID";
                        break;
                    }
                case SQLCommand.Project.Fetch:
                    {
                        query = "SELECT " +
                                "PROJECT_ID," +
                                "PROJECT_CODE, " +
                                "PROJECT, " +
                                "MD.DIVISION_ID,MD.DIVISION ," +
                                "ACCOUNT_DATE, " +
                                "DATE_STARTED, " +
                                "DATE_CLOSED, " +
                                "DESCRIPTION ," +
                                "NOTES , " +
                                " MP.PROJECT_CATEGORY_ID, MPC.PROJECT_CATOGORY_NAME, MIP.CUSTOMERID, MCH.CONTRIBUTION_ID, MP.CLOSED_BY " +
                            " FROM " +
                                " MASTER_PROJECT MP " +
                                " INNER JOIN MASTER_DIVISION MD ON " +
                                " MP.DIVISION_ID=MD.DIVISION_ID " +
                                " LEFT JOIN MASTER_PROJECT_CATOGORY MPC ON " +
                                " MP.PROJECT_CATEGORY_ID=MPC.PROJECT_CATOGORY_ID " +
                                " LEFT JOIN MASTER_INSTI_PERFERENCE MIP ON  " +
                                " MIP.CUSTOMERID=MP.CUSTOMERID  " +
                                " LEFT JOIN MASTER_CONTRIBUTION_HEAD MCH ON  " +
                                " MCH.CONTRIBUTION_ID=MP.CONTRIBUTION_ID  " +
                                " WHERE PROJECT_ID=?PROJECT_ID AND DELETE_FLAG<>1 ";
                        break;
                    }

                case SQLCommand.Project.FetchAll:
                    {
                        query = "SELECT " +
                                "PROJECT_ID," +
                                "PROJECT, " +
                                "DIVISION_ID, " +
                                "ACCOUNT_DATE, " +
                                "DATE_STARTED, " +
                                "DATE_CLOSED," +
                                "DESCRIPTION, CLOSED_BY" +
                            "FROM " +
                                "MASTER_PROJECT WHERE DELETE_FLAG<>1 ORDER BY PROJECT_CODE ";
                        break;
                    }
                case SQLCommand.Project.FetchVoucherTypes:
                    {
                        query = "SELECT VOUCHER_ID,VOUCHER_NAME,VOUCHER_TYPE FROM MASTER_VOUCHER WHERE VOUCHER_NAME IS NOT NULL GROUP BY VOUCHER_TYPE ";
                        break;
                    }
                case SQLCommand.Project.FetchSocietyNames:
                    {
                        query = "SELECT CUSTOMERID,SOCIETYNAME FROM  MASTER_INSTI_PERFERENCE";
                        break;
                    }
                case SQLCommand.Project.ITRGroupNames:
                    {
                        query = "SELECT PROJECT_CATOGORY_ITRGROUP_ID, PROJECT_CATOGORY_ITRGROUP FROM master_project_catogory_itrgroup";
                        break;
                    }
                case SQLCommand.Project.FetchDivision:
                    {
                        query = "SELECT DIVISION_ID, DIVISION FROM MASTER_DIVISION WHERE DIVISION <>'' AND DIVISION IS NOT NULL ";
                        break;
                    }

                case SQLCommand.Project.FetchProjects: // 15/11/2024, Chinna, Included the ITR
                    {
                        query = "SELECT MP.PROJECT_ID, PROJECT_CODE,PROJECT,DIVISION," +
                                " DATE_STARTED,DATE_CLOSED,MPC.PROJECT_CATOGORY_NAME, MP.CLOSED_BY,MPCITR.PROJECT_CATOGORY_ITRGROUP_ID,MPCITR.PROJECT_CATOGORY_ITRGROUP " +
                                " FROM MASTER_PROJECT MP" +
                                    " INNER JOIN MASTER_DIVISION MD ON" +
                                    " MP.DIVISION_ID=MD.DIVISION_ID" +
                                    " INNER JOIN MASTER_PROJECT_CATOGORY AS MPC" +
                                    " ON MP.PROJECT_CATEGORY_ID=MPC.PROJECT_CATOGORY_ID " +
                                    " LEFT JOIN MASTER_PROJECT_CATOGORY_ITRGROUP MPCITR " +
                                    " ON MPCITR.PROJECT_CATOGORY_ITRGROUP_ID =MPC.PROJECT_CATOGORY_ITRGROUP_ID " +
                                    " LEFT JOIN USER_PROJECT UP ON MP.PROJECT_ID=UP.PROJECT_ID " +
                                    " WHERE DELETE_FLAG<>1 {AND UP.ROLE_ID=?USERROLE_ID} " +
                                    " {AND (MP.DATE_CLOSED IS NULL OR MP.DATE_CLOSED >= ?DATE_CLOSED) } " + //On 12/07/2018, This property is used to skip Projects which is closed on or equal to this date
                                    " GROUP BY PROJECT ORDER BY MP.PROJECT ASC";
                        break;
                    }
                case SQLCommand.Project.FetchProjectsIntegration:
                    {
                        query =// "SELECT MP.PROJECT_ID,PROJECT " +
                                "SELECT MP.PROJECT_ID,PROJECT " +
                                " FROM MASTER_PROJECT MP" +
                                    " INNER JOIN MASTER_DIVISION MD ON" +
                                    " MP.DIVISION_ID=MD.DIVISION_ID" +
                                    " INNER JOIN MASTER_PROJECT_CATOGORY AS MPC" +
                                    " ON MP.PROJECT_CATEGORY_ID=MPC.PROJECT_CATOGORY_ID " +
                                    " LEFT JOIN USER_PROJECT UP ON MP.PROJECT_ID=UP.PROJECT_ID " +
                                    " WHERE DELETE_FLAG<>1 {AND UP.ROLE_ID=?USERROLE_ID} " +
                                    " {AND (MP.DATE_CLOSED IS NULL OR MP.DATE_CLOSED >= ?DATE_CLOSED) } " + //On 12/07/2018, This property is used to skip Projects which is closed on or equal to this date
                                    " GROUP BY PROJECT ORDER BY MP.PROJECT ASC";
                        break;
                    }
                case SQLCommand.Project.FetchVouchers:
                    {
                        query = "SELECT MV.VOUCHER_ID, MP.PROJECT_ID, VOUCHER_NAME ,CASE  VOUCHER_TYPE WHEN 1 THEN 'Receipt' " +
                                  " WHEN 2 THEN 'Payment'" +
                                  " WHEN 3 THEN 'Contra' " +
                                  " ELSE 'Journal' END AS VOUCHER_TYPE, " +
                                  " CASE VOUCHER_METHOD WHEN 1 THEN 'Automatic' " +
                                  " ELSE 'Manual' END AS VOUCHER_METHOD,PREFIX_CHAR ,SUFFIX_CHAR ,MONTH  " +
                                  " FROM MASTER_VOUCHER MV " +
                                  " INNER JOIN PROJECT_VOUCHER MPV  ON " +
                                  " MV.VOUCHER_ID =MPV.VOUCHER_ID " +
                                  " INNER JOIN MASTER_PROJECT MP ON " +
                                  " MPV.PROJECT_ID=MP.PROJECT_ID " +
                                  " LEFT JOIN USER_PROJECT AS UP " +
                                  " ON MP.PROJECT_ID=UP.PROJECT_ID " +
                                  " WHERE MP.DELETE_FLAG<>1 {AND UP.ROLE_ID=?USERROLE_ID } ORDER BY VOUCHER_TYPE ASC;";
                        break;
                    }
                case SQLCommand.Project.AddProjectVouchers:
                    {
                        query = "INSERT INTO PROJECT_VOUCHER ( " +
                                "PROJECT_ID," +
                                "VOUCHER_ID) VALUES( " +
                                "?PROJECT_ID," +
                                "?VOUCHER_ID)";
                        break;
                    }
                case SQLCommand.Project.AvailableVoucher:
                    {
                        query = " SELECT MV.VOUCHER_ID, VOUCHER_NAME ,VOUCHER_TYPE" +
                                " FROM MASTER_VOUCHER MV " +
                                " WHERE VOUCHER_ID NOT IN( " +
                                " SELECT VOUCHER_ID FROM PROJECT_VOUCHER MPV " +
                                " WHERE MV.VOUCHER_ID =MPV.VOUCHER_ID " +
                                " AND MPV.PROJECT_ID=?PROJECT_ID) ORDER BY VOUCHER_TYPE ASC";
                        break;
                    }
                case SQLCommand.Project.ProjectVoucher:
                    {
                        query = " SELECT MV.VOUCHER_ID, VOUCHER_NAME,VOUCHER_TYPE " +
                                " FROM MASTER_VOUCHER MV" +
                                " INNER JOIN PROJECT_VOUCHER MPV ON " +
                                " MV.VOUCHER_ID = MPV.VOUCHER_ID" +
                                " WHERE MPV.PROJECT_ID=?PROJECT_ID;";
                        break;
                    }
                case SQLCommand.Project.DeleteProjectVouchers:
                    {
                        query = "DELETE FROM PROJECT_VOUCHER WHERE PROJECT_ID=?PROJECT_ID;";
                        break;
                    }
                case SQLCommand.Project.FetchProjectList:
                    {
                        query = "SELECT " +
                                "PROJECT_ID, " +
                                "PROJECT " +
                                "FROM " +
                                "MASTER_PROJECT MP WHERE MP.DELETE_FLAG<>1 ORDER BY PROJECT ASC;";
                        break;
                    }
                case SQLCommand.Project.FetchLedgers:
                    {
                        //query = "SELECT MP.PROJECT_ID, ML.LEDGER_ID, LEDGER_CODE,LEDGER_NAME, GROUP_CODE,LEDGER_GROUP " +
                        //        " FROM MASTER_LEDGER ML " +
                        //        " INNER JOIN  MASTER_LEDGER_GROUP MLG ON ML.GROUP_ID=MLG.GROUP_ID " +
                        //        " INNER JOIN  PROJECT_LEDGER PLM ON ML.LEDGER_ID=PLM.LEDGER_ID " +
                        //        " INNER JOIN MASTER_PROJECT MP ON PLM.PROJECT_ID=MP.PROJECT_ID " +
                        //        " LEFT JOIN USER_PROJECT AS UP ON MP.PROJECT_ID=UP.PROJECT_ID " +
                        //        " WHERE MP.DELETE_FLAG<>1 {AND UP.ROLE_ID=?USERROLE_ID}";
                        query = "SELECT MP.PROJECT_ID,\n" +
                                "       ML.LEDGER_ID,\n" +
                                "       LEDGER_CODE,\n" +
                                "       CASE\n" +
                                "         WHEN LEDGER_GROUP = 'BANK ACCOUNTS' THEN\n" +
                                "          CONCAT(CONCAT(LEDGER_NAME, ' - '), CONCAT(BANK, ' - '), BRANCH)\n" +
                                "         ELSE\n" +
                                "          ML.LEDGER_NAME\n" +
                                "       END AS 'LEDGER_NAME',\n" +
                                "       GROUP_CODE,\n" +
                                "       LEDGER_GROUP\n" +
                                "  FROM MASTER_LEDGER ML\n" +
                                " INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                                "    ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                                " INNER JOIN PROJECT_LEDGER PLM\n" +
                                "    ON ML.LEDGER_ID = PLM.LEDGER_ID\n" +
                                " INNER JOIN MASTER_PROJECT MP\n" +
                                "    ON PLM.PROJECT_ID = MP.PROJECT_ID\n" +
                                "  LEFT JOIN USER_PROJECT AS UP\n" +
                                "    ON MP.PROJECT_ID = UP.PROJECT_ID\n" +
                                "  LEFT JOIN MASTER_BANK_ACCOUNT MBA\n" +
                                "    ON ML.LEDGER_CODE = MBA.ACCOUNT_CODE\n" +
                                "  LEFT JOIN MASTER_BANK MB\n" +
                                "    ON MBA.BANK_ID = MB.BANK_ID\n" +
                                " WHERE MP.DELETE_FLAG <> 1 {AND UP.ROLE_ID=?USERROLE_ID}";
                        break;
                    }

                case SQLCommand.Project.ProjectCategory:
                    {
                        query = " SELECT PROJECT_CATOGORY_ID ,PROJECT_CATOGORY_NAME " +
                                " FROM MASTER_PROJECT_CATOGORY" +
                                " WHERE  PROJECT_CATOGORY_NAME IS NOT NULL " +
                                " ORDER BY PROJECT_CATOGORY_NAME ASC ";
                        break;
                    }
                case SQLCommand.Project.LoadAllLedgerByProjectId:
                    {
                        //IF(MG.NATURE_ID IN (1, 4), 'CR', 'DR') AS TRANS_MODE,
                        query = @"SELECT ML.LEDGER_ID,IF(AMOUNT IS NULL,0.00,AMOUNT) AS AMOUNT,LEDGER_CODE,LEDGER_NAME,ML.SORT_ID,
                                  CASE WHEN LEDGER_SUB_TYPE = 'BK' THEN 'Bank Accounts'
                                  ELSE
                                  CASE WHEN LEDGER_SUB_TYPE = 'FD' THEN 'Fixed Deposit'
                                  ELSE
                                  LEDGER_GROUP
                                  END
                                  END AS 'TYPE',
                                  IF (IFNULL(LB.TRANS_MODE, '') = '', IF(MG.NATURE_ID IN (1,4), 'CR', 'DR'), LB.TRANS_MODE) AS TRANS_MODE,
                                  CASE
                                  WHEN LEDGER_TYPE = 'GN' THEN 'General'
                                  ELSE
                                  CASE
                                  WHEN LEDGER_TYPE = 'IK' THEN
                                  'In kind'
                                  END
                                  END 'GROUP',
                                  ML.BANK_ACCOUNT_ID
                                  FROM MASTER_LEDGER ML
                                  LEFT JOIN LEDGER_BALANCE LB ON LB.LEDGER_ID=ML.LEDGER_ID AND LB.PROJECT_ID=?PROJECT_ID AND TRANS_FLAG='OP'
                                  LEFT JOIN MASTER_LEDGER_GROUP MG ON ML.GROUP_ID = MG.GROUP_ID
                                  WHERE STATUS = 0 ORDER BY SORT_ID;";
                        break;
                    }

                case SQLCommand.Project.FetchDefaultVouchers:
                    {
                        query = "SELECT VOUCHER_ID,VOUCHER_NAME,VOUCHER_TYPE FROM MASTER_VOUCHER M " +
                                " WHERE VOUCHER_TYPE NOT IN(4) AND VOUCHER_NAME IS NOT NULL " +
                                " GROUP BY VOUCHER_TYPE";
                        break;
                    }
                case SQLCommand.Project.FetchAvailableVouchers:
                    {
                        query = " SELECT VOUCHER_ID,VOUCHER_NAME,VOUCHER_TYPE FROM MASTER_VOUCHER M " +
                                " WHERE NOT FIND_IN_SET(VOUCHER_ID, ?VOUCHER_ID) AND VOUCHER_NAME IS NOT NULL";
                        break;
                    }

                case SQLCommand.Project.FetchVoucherDetailsByProjectId:
                    {
                        query = "SELECT MV.VOUCHER_ID,VOUCHER_NAME,MV.IS_NARRATION_ENABLED,VOUCHER_TYPE,VOUCHER_METHOD,PREFIX_CHAR,SUFFIX_CHAR,STARTING_NUMBER,NUMBERICAL_WITH,PREFIX_WITH_ZERO, " +
                               " MONTH,DURATION,ALLOW_DUPLICATE,NOTE FROM MASTER_VOUCHER MV " +
                               " INNER JOIN PROJECT_VOUCHER PV ON MV.VOUCHER_ID=PV.VOUCHER_ID " +
                               " INNER JOIN MASTER_PROJECT MP  ON PV.PROJECT_ID=MP.PROJECT_ID " +
                               " WHERE PV.PROJECT_ID=?PROJECT_ID AND FIND_IN_SET(MV.VOUCHER_TYPE,?VOUCHER_TYPE) AND MP.DELETE_FLAG<>1 {AND MV.VOUCHER_ID = ?VOUCHER_ID}";
                        break;
                    }
                case SQLCommand.Project.FetchRecentProject:
                    {
                        query = "SELECT VMT.PROJECT_ID, MP.PROJECT " +
                                  " FROM VOUCHER_MASTER_TRANS VMT " +
                                 " INNER JOIN MASTER_PROJECT MP " +
                                    " ON VMT.PROJECT_ID = MP.PROJECT_ID " +
                                    " WHERE MP.DELETE_FLAG<>1 AND VMT.STATUS=1 AND VMT.CREATED_BY=?CREATED_BY" +
                                 " ORDER BY VOUCHER_ID DESC LIMIT 1";
                        break;

                    }
                case SQLCommand.Project.DeleteVoucher:
                    {
                        query = " DELETE FROM PROJECT_VOUCHER " +
                                " WHERE PROJECT_ID=?PROJECT_ID";
                        break;
                    }

                case SQLCommand.Project.DeleteProject:
                    {
                        // query = "UPDATE MASTER_PROJECT SET DELETE_FLAG=1 WHERE PROJECT_ID=?PROJECT_ID";
                        query = "DELETE FROM MASTER_PROJECT WHERE PROJECT_ID = ?PROJECT_ID";
                        break;
                    }
                case SQLCommand.Project.DeleteProjectLedgerBalance:
                    {
                        query = "DELETE FROM LEDGER_BALANCE WHERE PROJECT_ID = ?PROJECT_ID";
                        break;
                    }
                case SQLCommand.Project.DeleteProjectTransRenewal:
                    {
                        query = "DELETE FROM FD_RENEWAL WHERE FD_VOUCHER_ID IN(?VOUCHER_ID)";
                        break;
                    }
                case SQLCommand.Project.DeleteProjectTransFDAccount:
                    {
                        query = "DELETE FROM FD_ACCOUNT WHERE FD_VOUCHER_ID IN(?VOUCHER_ID)";
                        break;
                    }
                case SQLCommand.Project.DeleteProjectTransCostCentre:
                    {
                        query = "DELETE FROM VOUCHER_CC_TRANS WHERE VOUCHER_ID IN(?VOUCHER_ID)";
                        break;
                    }

                case SQLCommand.Project.DeleteProjectTransVoucher:
                    {
                        query = "DELETE FROM VOUCHER_TRANS WHERE VOUCHER_ID IN(?VOUCHER_ID)";
                        break;
                    }
                case SQLCommand.Project.DeleteProjectMasterVoucher:
                    {
                        query = "DELETE FROM VOUCHER_MASTER_TRANS WHERE VOUCHER_ID IN(?VOUCHER_ID)";
                        break;
                    }
                case SQLCommand.Project.FetchDeletedVouchersByProject:
                    {
                        query = "SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS WHERE STATUS=0 AND PROJECT_ID=?PROJECT_ID";
                        break;
                    }

                case SQLCommand.Project.FetchProjectCodes:
                    {
                        query = "SELECT PROJECT_CODE AS 'USED_CODE',PROJECT AS 'NAME'\n" +
                                    "  FROM MASTER_PROJECT\n" +
                                    " WHERE DELETE_FLAG = 0 {AND PROJECT_ID!=PROJECT_ID} \n" +
                                    " ORDER BY PROJECT_ID DESC";
                        break;
                    }

                case SQLCommand.Project.FetchProjectnameByProjectCode:
                    {
                        query = "SELECT PROJECT_CODE AS 'EXIST_CODE' FROM MASTER_PROJECT WHERE PROJECT_CODE=?PROJECT_CODE AND DELETE_FLAG = 0";
                        break;
                    }

                case SQLCommand.Project.FetchProjectDetails:
                    {
                        query = "SELECT PROJECT_ID, CONCAT(CONCAT(PROJECT, '-'), DIVISION) AS PROJECT\n" +
                                "  FROM MASTER_PROJECT AS MP\n" +
                                " INNER JOIN MASTER_DIVISION MD\n" +
                                "    ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                                " WHERE PROJECT_ID NOT IN (?PROJECT_ID)";
                        break;
                    }
                case SQLCommand.Project.FetchDefaultProjectVouchers:
                    {
                        query = "SELECT VOUCHER_ID,VOUCHER_NAME,VOUCHER_TYPE FROM MASTER_VOUCHER WHERE VOUCHER_NAME IS NOT NULL AND VOUCHER_ID NOT IN(?VOUCHER_ID)";
                        break;
                    }
                case SQLCommand.Project.FetchSelectedProjectVouchers:
                    {
                        //query = "SELECT MV.VOUCHER_ID,MV.VOUCHER_NAME,VOUCHER_TYPE FROM MASTER_VOUCHER MV " +
                        //        " INNER JOIN PROJECT_VOUCHER AS PV " +
                        //        " ON MV.VOUCHER_ID=PV.VOUCHER_ID " +
                        //        " WHERE PV.PROJECT_ID=?PROJECT_ID GROUP BY VOUCHER_TYPE ORDER BY VOUCHER_TYPE ASC";

                        query = "SELECT MV.VOUCHER_ID,MV.VOUCHER_NAME,VOUCHER_TYPE FROM MASTER_VOUCHER MV " +
                                " INNER JOIN PROJECT_VOUCHER AS PV " +
                                " ON MV.VOUCHER_ID=PV.VOUCHER_ID " +
                                " WHERE PV.PROJECT_ID=?PROJECT_ID ORDER BY VOUCHER_TYPE ASC";
                        break;
                    }
                case SQLCommand.Project.DeleteProjectLedger:
                    {
                        query = "DELETE FROM PROJECT_LEDGER WHERE PROJECT_ID=?PROJECT_ID";
                        break;
                    }
                case SQLCommand.Project.CheckLedgerBalanceForProject:
                    {
                        query = "SELECT AMOUNT FROM LEDGER_BALANCE WHERE PROJECT_ID=?PROJECT_ID AND LEDGER_ID=1";
                        break;
                    }
                case SQLCommand.Project.FetchProjectId:
                    {
                        query = "SELECT PROJECT_ID FROM MASTER_PROJECT WHERE PROJECT=?PROJECT";
                        break;
                    }
                case SQLCommand.Project.IsProjectCodeExists:
                    {
                        query = "SELECT COUNT(*) FROM MASTER_PROJECT WHERE PROJECT_CODE=?PROJECT_CODE";
                        break;
                    }
                case SQLCommand.Project.IsProjectNameExists:
                    {
                        query = "SELECT COUNT(*) FROM MASTER_PROJECT WHERE PROJECT=?PROJECT";
                        break;
                    }
                case SQLCommand.Project.DeleteProjectMappedLedgers:
                    {
                        query = "DELETE FROM PROJECT_LEDGER WHERE PROJECT_ID=?PROJECT_ID AND LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.Project.UpdateImportMasterProjectNames:
                    {
                        query = "UPDATE MASTER_PROJECT SET PROJECT=?PROJECT WHERE PROJECT_ID=?PROJECT_ID";
                        break;
                    }
                case SQLCommand.Project.FetchProjectBySociety:
                    {
                        // 30/12/2024 to command to have both in one place
                        query = "SELECT " +
                                  "MP.PROJECT_ID,MD.DIVISION, " +
                                  "CONCAT(MP.PROJECT,CONCAT(' - ',MD.DIVISION)) AS 'PROJECT',MP.DATE_CLOSED,(SELECT ' ') AS  TRANS_MODE,MP.CUSTOMERID, MP.PROJECT AS PROJECT_NAME " +
                              "FROM " +
                                  " MASTER_PROJECT MP " +
                                  " INNER JOIN MASTER_DIVISION MD ON " +
                                  " MP.DIVISION_ID=MD.DIVISION_ID " +
                                  " LEFT JOIN USER_PROJECT UP ON MP.PROJECT_ID=UP.PROJECT_ID" +
                                  " WHERE MP.DELETE_FLAG<>1 { AND UP.ROLE_ID=?USERROLE_ID } { AND MP.CUSTOMERID=?CUSTOMERID} " +
                                  " {AND (MP.DATE_CLOSED IS NULL OR MP.DATE_CLOSED >= ?DATE_CLOSED) } " + //On 28/06/2018, This property is used to skip Projects which is closed on or equal to this date
                                  " {AND (MP.DATE_STARTED <= ?DATE_STARTED ) } " + //On 09/02/2022, This property is used to skip Projects which is started on or equal to this date
                                  " GROUP BY MP.PROJECT ASC ";

                        //query = "SELECT " +
                        //          "MP.PROJECT_ID,MD.DIVISION, " +
                        //          "CONCAT(MP.PROJECT,CONCAT(' - ',MD.DIVISION)) AS 'PROJECT',MP.DATE_CLOSED,(SELECT ' ') AS  TRANS_MODE,MP.CUSTOMERID, MP.PROJECT AS PROJECT_NAME,MPCITR.PROJECT_CATOGORY_ITRGROUP_ID, MPCITR.PROJECT_CATOGORY_ITRGROUP " +
                        //      "FROM " +
                        //          " MASTER_PROJECT MP " +
                        //          " INNER JOIN MASTER_DIVISION MD ON " +
                        //          " MP.DIVISION_ID=MD.DIVISION_ID " +
                        //          " LEFT JOIN USER_PROJECT UP ON MP.PROJECT_ID=UP.PROJECT_ID" +
                        //          " LEFT JOIN MASTER_PROJECT_CATOGORY MPC ON MPC.PROJECT_CATOGORY_ID =MP.PROJECT_CATEGORY_ID" +
                        //          " LEFT JOIN MASTER_PROJECT_CATOGORY_ITRGROUP MPCITR ON MPCITR.PROJECT_CATOGORY_ITRGROUP_ID =MPC.PROJECT_CATOGORY_ITRGROUP_ID" +
                        //          " WHERE MP.DELETE_FLAG<>1 { AND UP.ROLE_ID=?USERROLE_ID } { AND MP.CUSTOMERID=?CUSTOMERID} { AND MPCITR.PROJECT_CATOGORY_ITRGROUP_ID=?PROJECT_CATOGORY_ITRGROUP_ID}  " +
                        //          " {AND (MP.DATE_CLOSED IS NULL OR MP.DATE_CLOSED >= ?DATE_CLOSED) } " + //On 28/06/2018, This property is used to skip Projects which is closed on or equal to this date
                        //          " {AND (MP.DATE_STARTED <= ?DATE_STARTED ) } " + //On 09/02/2022, This property is used to skip Projects which is started on or equal to this date
                        //          " GROUP BY MP.PROJECT ASC ";
                        break;
                    }
                case SQLCommand.Project.FetchProjectByITRGroup:
                    {
                        query = "SELECT " +
                                  "MP.PROJECT_ID,MD.DIVISION, " +
                                  "CONCAT(MP.PROJECT,CONCAT(' - ',MD.DIVISION)) AS 'PROJECT',MP.DATE_CLOSED,(SELECT ' ') AS  TRANS_MODE,MP.CUSTOMERID, MP.PROJECT AS PROJECT_NAME,MPCITR.PROJECT_CATOGORY_ITRGROUP_ID, MPCITR.PROJECT_CATOGORY_ITRGROUP " +
                              "FROM " +
                                  " MASTER_PROJECT MP " +
                                  " INNER JOIN MASTER_DIVISION MD ON " +
                                  " MP.DIVISION_ID=MD.DIVISION_ID " +
                                  " LEFT JOIN USER_PROJECT UP ON MP.PROJECT_ID=UP.PROJECT_ID" +
                                  " LEFT JOIN MASTER_PROJECT_CATOGORY MPC ON MPC.PROJECT_CATOGORY_ID =MP.PROJECT_CATEGORY_ID" +
                                  " LEFT JOIN MASTER_PROJECT_CATOGORY_ITRGROUP MPCITR ON MPCITR.PROJECT_CATOGORY_ITRGROUP_ID =MPC.PROJECT_CATOGORY_ITRGROUP_ID" +
                                  " WHERE MP.DELETE_FLAG<>1 { AND UP.ROLE_ID=?USERROLE_ID } { AND MPCITR.PROJECT_CATOGORY_ITRGROUP_ID=?PROJECT_CATOGORY_ITRGROUP_ID} " +
                                  " {AND (MP.DATE_CLOSED IS NULL OR MP.DATE_CLOSED >= ?DATE_CLOSED) } " + //On 28/06/2018, This property is used to skip Projects which is closed on or equal to this date
                                  " {AND (MP.DATE_STARTED <= ?DATE_STARTED ) } " + //On 09/02/2022, This property is used to skip Projects which is started on or equal to this date
                                  " GROUP BY MP.PROJECT ASC ";
                        break;
                    }

                case SQLCommand.Project.FetchTransactionDeatilsByProjectId:
                    {
                        //query = "SELECT " +
                        //        "VOUCHER_ID," +
                        //        "MT.PROJECT_ID," +
                        //        "MP.PROJECT," +
                        //        "VOUCHER_DATE," +
                        //        "VOUCHER_NO," +
                        //        "VOUCHER_TYPE," +
                        //        "DONOR_ID," +
                        //        "PURPOSE_ID," +
                        //        "CONTRIBUTION_TYPE," +
                        //        "CONTRIBUTION_AMOUNT," +
                        //        "CURRENCY_COUNTRY_ID," +
                        //        "EXCHANGE_RATE," +
                        //        "CALCULATED_AMOUNT," +
                        //        "ACTUAL_AMOUNT," +
                        //        "EXCHANGE_COUNTRY_ID," +
                        //        "STATUS," +
                        //    //  "CREATED_ON," +
                        //    //   "MODIFIED_ON," +
                        //        "CREATED_BY," +
                        //        "MODIFIED_BY," +
                        //        "NARRATION,NAME_ADDRESS " +
                        //        "FROM VOUCHER_MASTER_TRANS MT INNER JOIN MASTER_PROJECT MP ON MT.PROJECT_ID=MP.PROJECT_ID WHERE MP.PROJECT_ID=?PROJECT_ID AND MT.PURPOSE_ID=?CONTRIBUTION_ID AND MT.STATUS=1;";

                        query = " SELECT COUNT(*) AS PURPOSE,PURPOSE_ID\n" +
                                "  FROM VOUCHER_MASTER_TRANS MT\n" +
                                " WHERE PROJECT_ID = ?PROJECT_ID\n" +
                                "   AND PURPOSE_ID =?CONTRIBUTION_ID\n" +
                                "   AND STATUS = 1;";

                        break;
                    }
                case SQLCommand.Project.FetchProjectIdByProjectName:
                    {
                        query = "SELECT PROJECT_ID FROM MASTER_PROJECT WHERE PROJECT=?PROJECT";
                        break;
                    }
                case SQLCommand.Project.FetchProjectNameByProjectId:
                    {
                        query = "SELECT PROJECT FROM MASTER_PROJECT WHERE PROJECT_ID=?PROJECT_ID";
                        break;
                    }
            }

            return query;
        }
        #endregion Bank SQL
    }
}
