/********************************************************************************************
 *                                              Class      :MappingSQL.cs
 *                                              Purpose    :All the queries Mapping 
 *                                              Author     : Carmel Raj M
 *********************************************************************************************/
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class MappingSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.Mapping).FullName)
            {
                query = GetMappingSQL();
            }
            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        private string GetMappingSQL()
        {
            string Query = "";
            SQLCommand.Mapping sqlCommandId = (SQLCommand.Mapping)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                #region Projecct SQL
                case SQLCommand.Mapping.FetchProjectforLookup:
                    {
                        //Query = "SELECT " +
                        //            "MP.PROJECT_ID," +
                        //            "MP.DATE_STARTED," +
                        //            "MP.DATE_CLOSED," +
                        //            "CONCAT(MP.PROJECT,CONCAT(' - ',MD.DIVISION)) AS 'PROJECT',(SELECT ' ') AS  TRANS_MODE ,MP.CUSTOMERID " +
                        //        "FROM " +
                        //            " MASTER_PROJECT MP " +
                        //            " INNER JOIN MASTER_DIVISION MD ON " +
                        //            " MP.DIVISION_ID=MD.DIVISION_ID WHERE  MP.DELETE_FLAG<>1 ORDER BY MP.PROJECT ASC ";

                        Query = "SELECT " +
                                    "MP.PROJECT_ID," +
                                    "MP.DATE_STARTED," +
                                    "MP.DATE_CLOSED," +
                                    "MD.DIVISION_ID,MP.CONTRIBUTION_ID," +
                                    "MP.PROJECT AS PROJECT_NAME," +
                                    "CONCAT(MP.PROJECT,CONCAT(' - ',MD.DIVISION)) AS 'PROJECT',(SELECT ' ') AS  TRANS_MODE ,MP.CUSTOMERID " +
                                "FROM " +
                                    " MASTER_PROJECT MP " +
                                    " INNER JOIN MASTER_DIVISION MD ON " +
                                    " MP.DIVISION_ID=MD.DIVISION_ID " +
                                    " LEFT JOIN USER_PROJECT UP ON MP.PROJECT_ID=UP.PROJECT_ID " +
                                    " WHERE MP.DELETE_FLAG<>1 " +
                                    " {AND (MP.DATE_CLOSED IS NULL OR MP.DATE_CLOSED >= ?DATE_CLOSED) } " + //On 28/06/2018, This property is used to skip Projects which is closed on or equal to this date
                                    " {AND (MP.DATE_STARTED <= ?DATE_STARTED ) } " + //On 09/02/2022, This property is used to skip Projects which is started on or equal to this date
                                    " { AND UP.ROLE_ID=?USERROLE_ID } GROUP BY MP.PROJECT ASC ";
                        break;
                    }
                case SQLCommand.Mapping.IsExistMapVoucherTransaction:
                    {
                        Query = @" SELECT COUNT(VOUCHER_DEFINITION_ID) AS DEFINITIONID FROM VOUCHER_MASTER_TRANS " +
                                    " WHERE STATUS = 1 AND VOUCHER_DEFINITION_ID IN (?VOUCHER_ID) AND PROJECT_ID =?PROJECT_ID";
                        break;
                    }
                case SQLCommand.Mapping.FetchPJLookUp:
                    {
                        Query = " SELECT " +
                            "PROJECT_ID, " +
                            "PROJECT FROM MASTER_PROJECT " +
                            "ORDER BY PROJECT";
                        break;
                    }

                case SQLCommand.Mapping.FetchProjects:
                    {
                        Query = "SELECT " +
                                  "MP.PROJECT_ID,MD.DIVISION, " +
                                  "CONCAT(MP.PROJECT,CONCAT(' - ',MD.DIVISION)) AS 'PROJECT',(SELECT ' ') AS  TRANS_MODE,MP.CUSTOMERID " +
                              "FROM " +
                                  " MASTER_PROJECT MP " +
                                  " INNER JOIN MASTER_DIVISION MD ON " +
                                  " MP.DIVISION_ID=MD.DIVISION_ID " +
                                  " LEFT JOIN USER_PROJECT UP ON MP.PROJECT_ID=UP.PROJECT_ID" +
                                  " WHERE MP.DELETE_FLAG<>1 { AND UP.ROLE_ID=?USERROLE_ID} " +
                                  " {AND (MP.DATE_CLOSED IS NULL OR MP.DATE_CLOSED >= ?DATE_CLOSED) } " + //On 28/06/2018, This property is used to skip Projects which is closed on or equal to this date
                                  " {AND (MP.DATE_STARTED <= ?DATE_STARTED ) } " + //On 09/02/2022, This property is used to skip Projects which is started on or equal to this date
                                  "GROUP BY MP.PROJECT ASC";
                        break;

                    }
                case SQLCommand.Mapping.FetchProjectForGridView:
                    {
                        Query = "SELECT " +
                                    "MP.PROJECT_ID," +
                                    "CONCAT(MP.PROJECT,CONCAT(' - ',MD.DIVISION)) AS 'PROJECT',(SELECT 'DR') AS  TRANS_MODE,(SELECT 0.00) AS AMOUNT " +
                                "FROM " +
                                    " MASTER_PROJECT MP " +
                                    " INNER JOIN MASTER_DIVISION MD ON " +
                                    " MP.DIVISION_ID=MD.DIVISION_ID WHERE MP.DATE_CLOSED IS NULL AND MP.DELETE_FLAG<>1 ORDER BY MP.PROJECT ASC ";
                        break;
                    }
                case SQLCommand.Mapping.LoadProjectMappingGrid:
                    {
                        //                        Query = @"SELECT  IF(IFNULL(LB.TRANS_MODE, '') = '',
                        //                                            IF((SELECT NATURE_ID FROM MASTER_LEDGER_GROUP WHERE GROUP_ID=?GROUP_ID) IN (1, 4), 'CR', 'DR'),
                        //                                            LB.TRANS_MODE) AS TRANS_MODE,
                        //                                      P.PROJECT_ID, PROJECT, PL.LEDGER_ID,
                        //                                    IF(PL.LEDGER_ID IS NULL, 0,1) AS 'SELECT',
                        //                                    IF(AMOUNT IS NULL,0.00,AMOUNT) AS AMOUNT ,LB.TRANS_FLAG
                        //                                    FROM MASTER_PROJECT P
                        //                                    LEFT JOIN PROJECT_LEDGER PL ON PL.PROJECT_ID = P. PROJECT_ID AND PL.LEDGER_ID=?LEDGER_ID
                        //                                    LEFT JOIN LEDGER_BALANCE LB ON LB.PROJECT_ID=PL.PROJECT_ID
                        //                                                                AND LB.LEDGER_ID=PL.LEDGER_ID
                        //                                                                AND LB.trans_flag='OP'
                        //                                     WHERE P.DELETE_FLAG<>1;";
                        Query = @"SELECT IF(IFNULL(LB.TRANS_MODE, '') = '',
                                          IF((SELECT NATURE_ID
                                                FROM MASTER_LEDGER_GROUP
                                               WHERE GROUP_ID = ?GROUP_ID) IN (1, 4),
                                             'CR',
                                             'DR'),
                                          LB.TRANS_MODE) AS TRANS_MODE,
                                       P.PROJECT_ID,
                                       PROJECT,
                                       PL.LEDGER_ID,
                                       IF(PL.LEDGER_ID IS NULL, 0, 1) AS 'SELECT_TEMP',
                                       IF(PL.LEDGER_ID IS NULL, 0, 1) AS 'SELECT_TEMP_EDIT',
                                       IF(AMOUNT IS NULL, 0.00, IF(ML.GROUP_ID IN (12, 13, 14) AND ML.CUR_COUNTRY_ID > 0, AMOUNT_FC, AMOUNT)) AS AMOUNT,
                                       LB.TRANS_FLAG,
                                       P.CUSTOMERID,MIP.LEDGER_ID AS LEGAL_LEDGER_ID,DIVISION_ID,SOCIETYNAME
                                  FROM MASTER_PROJECT P
                                  LEFT JOIN PROJECT_LEDGER PL ON PL.PROJECT_ID = P. PROJECT_ID AND PL.LEDGER_ID = ?LEDGER_ID
                                  LEFT JOIN LEDGER_BALANCE LB ON LB.PROJECT_ID = PL.PROJECT_ID AND LB.LEDGER_ID = PL.LEDGER_ID AND LB.TRANS_FLAG = 'OP'
                                  LEFT JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = LB.LEDGER_ID   
                                  LEFT JOIN USER_PROJECT UP ON P.PROJECT_ID = UP.PROJECT_ID
                                  LEFT JOIN MASTER_INSTI_PERFERENCE MIP ON MIP.CUSTOMERID=P.CUSTOMERID 
                                  WHERE P.DELETE_FLAG <> 1 {AND UP.ROLE_ID = ?USERROLE_ID}
                                  GROUP BY P.PROJECT";
                        break;
                    }
                case SQLCommand.Mapping.LoadProjectDonorGrid:
                    {
                        Query = @"SELECT P.PROJECT_ID, PROJECT, PD.DONOR_ID,IF(PD.DONOR_ID IS NULL, 0,1) AS 'SELECT_TEMP'
                                    FROM MASTER_PROJECT P
                                    LEFT JOIN PROJECT_DONOR PD ON P.PROJECT_ID = PD.PROJECT_ID AND PD.DONOR_ID=?DONAUD_ID
                                    LEFT JOIN USER_PROJECT UP ON P.PROJECT_ID=UP.PROJECT_ID
                                    WHERE P.DELETE_FLAG<>1 {AND UP.ROLE_ID=?USERROLE_ID} GROUP BY P.PROJECT";
                        break;
                    }
                case SQLCommand.Mapping.LoadProjectCostCentreGrid:
                    {
                        Query = @"SELECT P.PROJECT_ID, PROJECT, PCC.COST_CENTRE_ID,IF(PCC.COST_CENTRE_ID IS NULL, 0,1) AS 'SELECT_TEMP',
                                IF(PCC.COST_CENTRE_ID IS NULL, 0,1) AS 'SELECT_TEMP_EDIT',
                                IF(PCC.AMOUNT IS NULL OR PCC.AMOUNT = 0, 'DR', PCC.TRANS_MODE) AS  TRANS_MODE,IF(AMOUNT IS NULL,0.00,AMOUNT) AS AMOUNT
                                FROM MASTER_PROJECT P
                                LEFT JOIN PROJECT_COSTCENTRE PCC ON P.PROJECT_ID = PCC.PROJECT_ID
                                AND PCC.COST_CENTRE_ID=?COST_CENTRE_ID
                                LEFT JOIN USER_PROJECT UP ON P.PROJECT_ID=UP.PROJECT_ID
                                WHERE P.DELETE_FLAG<>1 {AND UP.ROLE_ID=?USERROLE_ID} GROUP BY P.PROJECT";
                        break;
                    }
                case SQLCommand.Mapping.LoadProjectFDLedgerGrid:
                    {
                        Query = "SELECT P.PROJECT_ID,\n" +
                                "       PROJECT,\n" +
                                "       PL.LEDGER_ID,\n" +
                                "       IF(PL.LEDGER_ID IS NULL, 0, 1) AS 'SELECT',\n" +
                                "       (SELECT 'DR') AS TRANS_MODE,\n" +
                            // "       IFNULL(SUM(AMOUNT), 0.00) AS AMOUNT\n" +
                                "       IFNULL(AMOUNT, 0.00) AS AMOUNT\n" +
                                "  FROM MASTER_PROJECT P\n" +
                                "  LEFT JOIN PROJECT_LEDGER PL\n" +
                                "    ON PL.PROJECT_ID = P. PROJECT_ID\n" +
                                "   AND PL.LEDGER_ID =?LEDGER_ID\n" +
                                "  LEFT JOIN FD_ACCOUNT FD\n" +
                                "    ON FD.PROJECT_ID = PL.PROJECT_ID\n" +
                                "   AND FD.LEDGER_ID = PL.LEDGER_ID\n" +
                                "   AND TRANS_TYPE = 'OP'\n" +
                                "   AND FD.STATUS = 1\n" +
                                " WHERE P.DELETE_FLAG <> 1"; // GROUP BY PROJECT_ID";
                        break;
                    }
                case SQLCommand.Mapping.FetchMappedProject:
                    {
                        Query = @"SELECT
                                        MP.PROJECT,
                                        PL.PROJECT_ID,
                                        PL.LEDGER_ID,
                                        ML.LEDGER_NAME,
                                        LG.LEDGER_GROUP,
                                        IFNULL(LB.AMOUNT, 0) AS AMOUNT,
                                        IF(IFNULL(LB.TRANS_MODE, '') = '',
                                            IF(LG.NATURE_ID IN (1, 4), 'CR', 'DR'),
                                            LB.TRANS_MODE) AS TRANS_MODE,
                                        CASE
                                            WHEN LEDGER_TYPE = 'GN' THEN
                                            'General'
                                            ELSE
                                            CASE
                                            WHEN LEDGER_TYPE = 'IK' THEN
                                                'In kind'
                                            END
                                        END 'GROUP',
                                        IFNULL(LB.LEDGER_ID, 0) AS UPDATE_MODE
                                    FROM
                                    MASTER_PROJECT MP INNER JOIN
                                    PROJECT_LEDGER AS PL ON MP.PROJECT_ID=PL.PROJECT_ID
                                    LEFT JOIN MASTER_LEDGER AS ML
                                    ON PL.LEDGER_ID = ML.LEDGER_ID
                                    LEFT JOIN MASTER_LEDGER_GROUP AS LG
                                    ON ML.GROUP_ID = LG.GROUP_ID
                                    LEFT JOIN LEDGER_BALANCE AS LB
                                    ON PL.PROJECT_ID = LB.PROJECT_ID
                                    AND PL.LEDGER_ID = LB.LEDGER_ID
                                    AND LB.TRANS_FLAG = 'OP'
                                    WHERE PL.LEDGER_ID = ?LEDGER_ID AND MP.DELETE_FLAG<>1";
                        break;
                    }
                case SQLCommand.Mapping.LedgerProjectMappingDelete:
                    {
                        Query = "DELETE FROM PROJECT_LEDGER WHERE LEDGER_ID=?LEDGER_ID;";
                        break;
                    }
                case SQLCommand.Mapping.LoadLedgerByProId:
                    {
                        Query = @"SELECT ML.LEDGER_ID,MG.LEDGER_GROUP,IF(MIP.LEDGER_ID IS NULL,0,MIP.LEDGER_ID) AS LEGAL_ENTITY_LEDGER_ID,
                                        IF(CUSTOMERID IS NULL,0,CUSTOMERID) AS CUSTOMER_ID,
                                        IF(AMOUNT IS NULL, 0.00, IF(ML.GROUP_ID IN (12, 13, 14) AND ML.CUR_COUNTRY_ID > 0, AMOUNT_FC, AMOUNT) ) AS AMOUNT,
                                        -- IF(ML.GROUP_ID = 13, 1, IF(PL.LEDGER_ID IS NULL, 0, 1)) AS 'SELECT_TMP',
                                        -- IF(ML.LEDGER_ID = 1, 1, IF(PL.LEDGER_ID IS NULL, 0, 1)) AS 'SELECT_TMP',
                                        IF(ML.ACCESS_FLAG = 2, 1, IF(PL.LEDGER_ID IS NULL, 0, 1)) AS 'SELECT_TMP', -- Added by alex to mapp the default ledgers to the project.
                                        ML.GROUP_ID,
                                        LEDGER_CODE,
                                        LEDGER_NAME,
                                        ML.SORT_ID,
                                        ML.ACCESS_FLAG,
                                        CASE
                                            WHEN LEDGER_SUB_TYPE = 'BK' THEN
                                            'Bank Accounts'
                                            ELSE
                                            CASE
                                            WHEN LEDGER_SUB_TYPE = 'FD' THEN
                                                'Fixed Deposit'
                                            ELSE
                                                LEDGER_GROUP
                                            END
                                        END AS 'TYPE',
                                        -- IF(MG.NATURE_ID IN (1, 4), 'CR', 'DR') AS TRANS_MODE, (On 04/09/2019, fixing issue (wrongly showing transmode after it made in mappig screen)
                                        IF (IFNULL(LB.TRANS_MODE, '') = '', IF(MG.NATURE_ID IN (1,4), 'CR', 'DR'), LB.TRANS_MODE) AS TRANS_MODE, +
                                        CASE
                                            WHEN LEDGER_TYPE = 'GN' THEN
                                            'General'
                                            ELSE
                                            CASE
                                            WHEN LEDGER_TYPE = 'IK' THEN
                                                'In kind'
                                            END
                                        END 'GROUP',
                                        ML.BANK_ACCOUNT_ID, ML.IS_COST_CENTER
                                    FROM MASTER_LEDGER ML
                                    LEFT JOIN PROJECT_LEDGER PL ON PL.LEDGER_ID = ML.LEDGER_ID AND PL.PROJECT_ID = ?PROJECT_ID
                                    LEFT JOIN LEDGER_BALANCE LB ON LB.LEDGER_ID = ML.LEDGER_ID AND LB.PROJECT_ID = ?PROJECT_ID AND TRANS_FLAG = 'OP'
                                    LEFT JOIN MASTER_LEDGER_GROUP MG ON ML.GROUP_ID = MG.GROUP_ID
                                    LEFT JOIN MASTER_INSTI_PERFERENCE MIP ON MIP.LEDGER_ID=ML.LEDGER_ID
                                    WHERE STATUS = 0
                                    -- AND ML.LEDGER_ID NOT IN
                                      --   (SELECT LEDGER_ID
                                         --    FROM PROJECT_LEDGER
                                          --  WHERE LEDGER_ID IN
                                            --    (SELECT LEDGER_ID FROM MASTER_LEDGER WHERE GROUP_ID = 14))
                                    ORDER BY ML.SORT_ID;";
                        break;
                    }
                case SQLCommand.Mapping.LoadLedgerByProjectIds:
                    {
                        Query = @"SELECT ML.LEDGER_ID, ML.LEDGER_CODE, ML.LEDGER_NAME, ML.SORT_ID, ML.ACCESS_FLAG,
                                  ML.IS_COST_CENTER, SUM(LB.AMOUNT) AS AMOUNT
                                  FROM MASTER_LEDGER ML
                                  INNER JOIN PROJECT_LEDGER PL ON PL.LEDGER_ID = ML.LEDGER_ID {AND PL.PROJECT_ID IN (?PROJECT_IDs)}
                                  LEFT JOIN LEDGER_BALANCE LB ON LB.LEDGER_ID = ML.LEDGER_ID {AND LB.PROJECT_ID IN (?PROJECT_IDs)} AND TRANS_FLAG = 'OP' 
                                  WHERE STATUS = 0 {AND PL.PROJECT_ID IN (?PROJECT_IDs)} GROUP BY ML.LEDGER_ID";
                        break;
                    }
                case SQLCommand.Mapping.LoadCCLedgerByProjectId:
                    {
                        Query = "SELECT PL.PROJECT_ID, PL.LEDGER_ID,IF(MIP.LEDGER_ID IS NULL,0,MIP.LEDGER_ID) AS LEGAL_ENTITY_LEDGER_ID,IF(MIP.CUSTOMERID IS NULL,0,MIP.CUSTOMERID) AS CUSTOMER_ID, " +
                                "ML.BANK_ACCOUNT_ID, ML.GROUP_ID,ML.ACCESS_FLAG, " +
                                "CASE " +
                                    "WHEN LEDGER_GROUP='Bank Accounts' THEN " +
                                     "CONCAT(CONCAT(ML.LEDGER_NAME,' - '),CONCAT(MB.BANK,' - '),MB.BRANCH) " +
                                    "ELSE " +
                                     "ML.LEDGER_NAME " +
                                "END AS 'LEDGER_NAME', " +
                                "LG.LEDGER_GROUP, " +
                                "IFNULL(LB.AMOUNT,0) AS AMOUNT,ML.IS_TDS_LEDGER, LG.NATURE_ID, " +
                                "LEDGER_CODE," +
                                "IF (IFNULL(LB.TRANS_MODE, '') = '', " +
                                "    IF(LG.NATURE_ID IN (1,4), 'CR', 'DR'), LB.TRANS_MODE) AS TRANS_MODE, " +
                                "CASE WHEN LEDGER_TYPE='GN' " +
                                          "THEN 'General'   ELSE CASE WHEN LEDGER_TYPE='IK' " +
                                          "THEN 'In kind' END END 'GROUP', " +
                                  "CASE " +
                                        "WHEN LEDGER_SUB_TYPE = 'BK' THEN " +
                                        "'Bank Accounts' " +
                                        "ELSE " +
                                        "CASE " +
                                        "WHEN LEDGER_SUB_TYPE = 'FD' THEN " +
                                            "'Fixed Deposit' " +
                                        "ELSE " +
                                            "LEDGER_GROUP " +
                                        "END " +
                                    "END AS 'TYPE', " +
                                "IFNULL(LB.LEDGER_ID, 0) AS UPDATE_MODE, ML.IS_COST_CENTER, PC.PROJECT_CATOGORY_NAME " +
                                "FROM PROJECT_LEDGER AS PL " +
                                "LEFT JOIN MASTER_LEDGER AS ML  ON PL.LEDGER_ID = ML.LEDGER_ID " +
                                "LEFT JOIN MASTER_LEDGER_GROUP AS LG  ON ML.GROUP_ID = LG.GROUP_ID " +
                                "LEFT JOIN LEDGER_BALANCE AS LB ON PL.PROJECT_ID = LB.PROJECT_ID " +
                                "AND PL.LEDGER_ID = LB.LEDGER_ID AND LB.TRANS_FLAG = 'OP' " +
                                "LEFT JOIN MASTER_BANK_ACCOUNT MBA  ON MBA.LEDGER_ID=ML.LEDGER_ID " +
                                "LEFT JOIN MASTER_BANK MB  ON MB.BANK_ID=MBA.BANK_ID " +
                                "LEFT JOIN MASTER_INSTI_PERFERENCE MIP ON ML.LEDGER_ID=MIP.LEDGER_ID " +
                                "LEFT JOIN (SELECT PCL.LEDGER_ID, GROUP_CONCAT(MPC.PROJECT_CATOGORY_NAME SEPARATOR  ', ') AS PROJECT_CATOGORY_NAME " +
                                "            FROM PROJECT_CATEGORY_LEDGER PCL " +
                                "            INNER JOIN MASTER_PROJECT_CATOGORY MPC ON MPC.PROJECT_CATOGORY_ID = PCL.PROJECT_CATEGORY_ID " +
                                "            GROUP BY PCL.LEDGER_ID) AS PC ON PC.LEDGER_ID = PL.LEDGER_ID " +
                                "WHERE PL.PROJECT_ID = ?PROJECT_ID AND ML.IS_COST_CENTER=1" +
                                "{AND PL.LEDGER_ID = ?LEDGER_ID} {AND ML.GROUP_ID = ?GROUP_ID} ORDER BY ML.SORT_ID";
                        break;
                    }
                case SQLCommand.Mapping.subLedgerLoadLedgerByProdId:
                    {
                        Query = @"SELECT ML.LEDGER_ID,MG.LEDGER_GROUP,IF(MIP.LEDGER_ID IS NULL,0,MIP.LEDGER_ID) AS LEGAL_ENTITY_LEDGER_ID,
                                        IF(CUSTOMERID IS NULL,0,CUSTOMERID) AS CUSTOMER_ID,
                                        IF(AMOUNT IS NULL, 0.00, AMOUNT) AS AMOUNT,
                                        IF(ML.ACCESS_FLAG = 2, 1, IF(PL.LEDGER_ID IS NULL, 0, 1)) AS 'SELECT_TMP',
                                        ML.GROUP_ID,
                                        LEDGER_CODE,
                                        LEDGER_NAME,
                                        ML.SORT_ID,
                                        ML.ACCESS_FLAG,
                                        IF (IFNULL(LB.TRANS_MODE, '') = '', IF(MG.NATURE_ID IN (1,4), 'CR', 'DR'), LB.TRANS_MODE) AS TRANS_MODE,
                                    FROM MASTER_LEDGER ML 
                                    LEFT JOIN PROJECT_LEDGER PL ON PL.LEDGER_ID = ML.LEDGER_ID AND PL.PROJECT_ID = ?PROJECT_ID
                                    LEFT JOIN LEDGER_BALANCE LB ON LB.LEDGER_ID = ML.LEDGER_ID AND LB.PROJECT_ID = ?PROJECT_ID AND TRANS_FLAG = 'OP' 
                                    LEFT JOIN MASTER_LEDGER_GROUP MG ON ML.GROUP_ID = MG.GROUP_ID
                                    WHERE STATUS = 0 ORDER BY ML.SORT_ID;";
                        break;
                    }


                #endregion

                #region Ledger SQL
                case SQLCommand.Mapping.LoadLedgerFD:
                    {
                        Query = "SELECT LEDGER_ID," +
                                    "LEDGER_CODE," +
                                    "LEDGER_NAME," +
                                    "ML.SORT_ID, " +
                                    "CASE " +
                                        "WHEN LEDGER_SUB_TYPE = 'BK' THEN " +
                                        "'Bank Accounts' " +
                                        "ELSE " +
                                        "CASE " +
                                        "WHEN LEDGER_SUB_TYPE = 'FD' THEN " +
                                            "'Fixed Deposit' " +
                                        "ELSE " +
                                            "LEDGER_GROUP " +
                                        "END " +
                                    "END AS 'TYPE', " +
                                    "IF(MG.NATURE_ID IN (1, 4), 'CR', 'DR') AS TRANS_MODE, " +
                                    "CASE " +
                                        "WHEN LEDGER_TYPE = 'GN' THEN " +
                                        "'General' " +
                                        "ELSE " +
                                        "CASE " +
                                        "WHEN LEDGER_TYPE = 'IK' THEN " +
                                            "'In kind' " +
                                        "END " +
                                    "END 'GROUP', " +
                                    "ML.BANK_ACCOUNT_ID " +
                                "FROM MASTER_LEDGER ML " +
                                "LEFT JOIN MASTER_LEDGER_GROUP MG " +
                                "ON ML.GROUP_ID = MG.GROUP_ID " +
                                "WHERE STATUS = 0 " +
                                    "AND ML.LEDGER_ID NOT IN " +
                                "(SELECT LEDGER_ID " +
                                "FROM PROJECT_LEDGER " +
                                "WHERE LEDGER_ID IN " +
                                        "(SELECT LEDGER_ID FROM MASTER_LEDGER WHERE GROUP_ID = 14)) " +
                                "ORDER BY ML.SORT_ID;";
                        break;
                    }
                case SQLCommand.Mapping.IsMadeBudgetForLedger:
                    {
                        Query = @"SELECT LEDGER_ID FROM BUDGET_MASTER BA
                                INNER JOIN BUDGET_LEDGER BL ON BA.BUDGET_ID=BL.BUDGET_ID AND LEDGER_ID=?IDs
                                INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BA.BUDGET_ID
                                WHERE PROJECT_ID=?PROJECT_ID;";
                        break;
                    }
                case SQLCommand.Mapping.LoadAllLedgers:
                    {
                        //On 03/02/2021, to include Project Category
                        Query = @"SELECT ML.LEDGER_ID,ML.GROUP_ID,IF(MIP.LEDGER_ID IS NULL,0,MIP.LEDGER_ID) AS LEGAL_ENTITY_LEDGER_ID,IF(MIP.CUSTOMERID IS NULL,0,MIP.CUSTOMERID) AS CUSTOMER_ID,
                                     LEDGER_CODE,
                                     CASE
                                         WHEN ML.LEDGER_SUB_TYPE = 'BK' THEN
                                          CONCAT(CONCAT(ML.LEDGER_NAME, ' - '), CONCAT(BANK, ' - '), BRANCH)
                                         ELSE
                                          ML.LEDGER_NAME
                                       END AS 'LEDGER_NAME',
                                     ML.SORT_ID,
                                     CASE
                                         WHEN LEDGER_SUB_TYPE = 'BK' THEN
                                         'Bank Accounts'
                                         ELSE
                                         CASE
                                         WHEN LEDGER_SUB_TYPE = 'FD' THEN
                                             'Fixed Deposit'
                                         ELSE
                                             LEDGER_GROUP
                                         END
                                     END AS 'TYPE',
                                     IF(MG.NATURE_ID IN (1, 4), 'CR', 'DR') AS TRANS_MODE,
                                     CASE
                                         WHEN LEDGER_TYPE = 'GN' THEN
                                            'General'
                                         ELSE
                                         CASE
                                         WHEN LEDGER_TYPE = 'IK' THEN
                                             'In kind'
                                         END
                                     END 'GROUP',
                                     ML.BANK_ACCOUNT_ID, ML.IS_COST_CENTER, PC.PROJECT_CATOGORY_NAME
                                 FROM MASTER_LEDGER ML
                                 LEFT JOIN MASTER_LEDGER_GROUP MG ON ML.GROUP_ID = MG.GROUP_ID
                                 LEFT JOIN MASTER_BANK_ACCOUNT MBA ON ML.LEDGER_ID = MBA.LEDGER_ID
                                 LEFT JOIN MASTER_BANK MB ON MB.BANK_ID = MBA.BANK_ID
                                 LEFT JOIN MASTER_INSTI_PERFERENCE MIP ON ML.LEDGER_ID=MIP.LEDGER_ID
                                 LEFT JOIN (SELECT PCL.LEDGER_ID, GROUP_CONCAT(MPC.PROJECT_CATOGORY_NAME SEPARATOR  ', ') AS PROJECT_CATOGORY_NAME
                                            FROM PROJECT_CATEGORY_LEDGER PCL
                                            INNER JOIN MASTER_PROJECT_CATOGORY MPC ON MPC.PROJECT_CATOGORY_ID = PCL.PROJECT_CATEGORY_ID
                                            GROUP BY PCL.LEDGER_ID) AS PC ON PC.LEDGER_ID = ML.LEDGER_ID
                                 WHERE STATUS = 0                                 
                                 ORDER BY ML.SORT_ID;";
                        //AND ML.LEDGER_ID NOT IN
                        //(SELECT LEDGER_ID
                        //FROM PROJECT_LEDGER
                        //WHERE LEDGER_ID IN
                        //        (SELECT LEDGER_ID FROM MASTER_LEDGER WHERE GROUP_ID = 14))

                        break;
                    }
                case SQLCommand.Mapping.ProjectLedgerMappingDelete:
                    {
                        Query = "DELETE FROM PROJECT_LEDGER WHERE PROJECT_ID=?PROJECT_ID;";
                        break;
                    }
                case SQLCommand.Mapping.ProjectBudgetLedgerMappingDelete:
                    {
                        Query = "DELETE FROM PROJECT_BUDGET_LEDGER WHERE PROJECT_ID=?PROJECT_ID;";
                        break;
                    }
                case SQLCommand.Mapping.ProjectBudgetLedgerByBudgetLedgerProject:
                    {
                        Query = "SELECT COUNT(PROJECT_ID) FROM PROJECT_BUDGET_LEDGER WHERE LEDGER_ID =?LEDGER_ID AND PROJECT_ID =?PROJECT_ID";
                        break;
                    }
                case SQLCommand.Mapping.DeleteUnMappedProjectLedgersInProjectBudgetLedger:
                    {
                        Query = @"DELETE FROM PROJECT_BUDGET_LEDGER WHERE PROJECT_ID=?PROJECT_ID AND 
                                    LEDGER_ID NOT IN (SELECT LEDGER_ID FROM PROJECT_LEDGER WHERE PROJECT_ID = ?PROJECT_ID) AND 
                                    LEDGER_ID NOT IN (SELECT LEDGER_ID FROM BUDGET_LEDGER WHERE PROJECT_ID = ?PROJECT_ID);";
                        break;
                    }
                case SQLCommand.Mapping.DeleteProjectOpBalance:
                    {
                        Query = "DELETE FROM LEDGER_BALANCE WHERE PROJECT_ID=?PROJECT_ID AND TRANS_FLAG='OP'";
                        break;
                    }
                case SQLCommand.Mapping.DeleteBudgetLedgerAmountByLedger:
                    {
                        Query = @"DELETE FROM BUDGET_LEDGER WHERE LEDGER_ID IN (?ID_COLLECTIONS) AND 
                                BUDGET_ID IN (SELECT BM.BUDGET_ID FROM BUDGET_MASTER BM
                                        INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID 
                                        WHERE 1=1 {AND PROJECT_ID IN (?PROJECT_ID)} GROUP BY BM.BUDGET_ID)";
                        break;
                    }
                case SQLCommand.Mapping.DeleteMappedBudgetByLedger:
                    {
                        Query = @"DELETE FROM PROJECT_BUDGET_LEDGER WHERE LEDGER_ID IN (?ID_COLLECTIONS) {AND PROJECT_ID IN (?PROJECT_ID)}";
                        break;
                    }
                case SQLCommand.Mapping.UnMapProjectLedger:
                    {
                        Query = "DELETE FROM PROJECT_LEDGER WHERE LEDGER_ID=?LEDGER_ID;";
                        break;
                    }
                //case SQLCommand.Mapping.CheckLedgerMapped:
                //    {
                //        Query = "SELECT PROJECT_ID,LEDGER_ID FROM PROJECT_LEDGER WHERE PROJECT_ID = ?PROJECT_ID AND LEDGER_ID = ?LEDGER_ID;";
                //        break;
                //    }
                case SQLCommand.Mapping.CheckCostCentreMapped:
                    {
                        Query = "SELECT PROJECT_ID,COST_CENTRE_ID FROM PROJECT_COSTCENTRE WHERE PROJECT_ID = ?PROJECT_ID AND COST_CENTRE_ID = ?COST_CENTRE_ID;";
                        break;
                    }
                //case SQLCommand.Mapping.MapCostCentreToProject:
                //    {
                //        Query = "INSERT INTO PROJECT_COSTCENTRE\n" +
                //                "  (PROJECT_ID, COST_CENTRE_ID, AMOUNT,TRANS_MODE)\n" +
                //                "VALUES\n" +Mapping.DeleteMappedLedgerBalance
                //                "  (?PROJECT_ID, ?COST_CENTRE_ID, 0, ?TRANS_MODE) ON DUPLICATE KEY UPDATE PROJECT_ID = ?PROJECT_ID, COST_CENTRE_ID = ?COST_CENTRE_ID;";
                //        break;
                //    }
                case SQLCommand.Mapping.FetchMappedLedgers:
                    {
                        Query = @"SELECT PL.LEDGER_ID, MP.PROJECT_ID,PROJECT,AMOUNT,TRANS_MODE
                                        FROM PROJECT_LEDGER PL
                                        LEFT JOIN MASTER_LEDGER ML ON ML.LEDGER_ID=PL.LEDGER_ID
                                        LEFT JOIN MASTER_PROJECT MP ON MP.PROJECT_ID=PL.PROJECT_ID
                                        LEFT JOIN LEDGER_BALANCE LB ON LB.PROJECT_ID=PL.PROJECT_ID
                                                                    AND LB.LEDGER_ID=PL.LEDGER_ID
                                                                    AND TRANS_FLAG='OP'
                                        WHERE PL.LEDGER_ID=?LEDGER_ID ORDER BY PROJECT_ID;";
                        break;
                    }

                case SQLCommand.Mapping.FetchMappedFDByFDId:
                    {
                        Query = @"SELECT PL.PROJECT_ID,AMOUNT,TRANS_MODE
                                    FROM PROJECT_LEDGER PL
                                    LEFT JOIN LEDGER_BALANCE LB ON LB.LEDGER_ID=PL.LEDGER_ID AND LB.TRANS_FLAG='OP'
                                    WHERE PL.LEDGER_ID=?LEDGER_ID;";
                        break;
                    }
                case SQLCommand.Mapping.MapMigratedLedgers:
                    {
                        Query = @"UPDATE VOUCHER_TRANS SET LEDGER_ID=?MAP_LEDGER_ID WHERE FIND_IN_SET (LEDGER_ID,?LEDGER_ID_COLLECTION);
                                    UPDATE VOUCHER_CC_TRANS SET LEDGER_ID=?MAP_LEDGER_ID,COST_CENTRE_TABLE = CONCAT('0LDR',?MAP_LEDGER_ID) WHERE FIND_IN_SET (LEDGER_ID,?LEDGER_ID_COLLECTION);
                                    UPDATE TDS_CREDTIORS_PROFILE SET LEDGER_ID=?MAP_LEDGER_ID WHERE FIND_IN_SET (LEDGER_ID,?LEDGER_ID_COLLECTION);
                                    UPDATE TDS_BOOKING SET EXPENSE_LEDGER_ID=?MAP_LEDGER_ID WHERE FIND_IN_SET (EXPENSE_LEDGER_ID,?LEDGER_ID_COLLECTION);
                                 -- UPDATE VOUCHER_FD_INTEREST SET FD_LEDGER_ID=?MAP_LEDGER_ID WHERE FIND_IN_SET(FD_LEDGER_ID,?LEDGER_ID_COLLECTION); -- This table is not in use.
                                    UPDATE ALLOT_FUND SET LEDGER_ID=?MAP_LEDGER_ID WHERE FIND_IN_SET (LEDGER_ID,?LEDGER_ID_COLLECTION);
                                    UPDATE BUDGET_LEDGER SET LEDGER_ID=?MAP_LEDGER_ID WHERE FIND_IN_SET (LEDGER_ID,?LEDGER_ID_COLLECTION);
                                 -- UPDATE VOUCHER_TRANS SET LEDGER_ID=?MAP_LEDGER_ID WHERE FIND_IN_SET (LEDGER_ID,?LEDGER_ID_COLLECTION);
                                    UPDATE FD_RENEWAL SET INTEREST_LEDGER_ID=?MAP_LEDGER_ID WHERE FIND_IN_SET (INTEREST_LEDGER_ID,?LEDGER_ID_COLLECTION);
                                    UPDATE FD_ACCOUNT SET LEDGER_ID=?MAP_LEDGER_ID WHERE FIND_IN_SET (LEDGER_ID,?LEDGER_ID_COLLECTION);
                                    DELETE FROM HEADOFFICE_MAPPED_LEDGER WHERE FIND_IN_SET(LEDGER_ID,?LEDGER_ID_COLLECTION);
                                 -- UPDATE HEADOFFICE_MAPPED_LEDGER SET LEDGER_ID=?MAP_LEDGER_ID WHERE FIND_IN_SET (LEDGER_ID,?LEDGER_ID_COLLECTION);
                                 -- UPDATE MASTER_BANK_ACCOUNT SET LEDGER_ID=?MAP_LEDGER_ID WHERE FIND_IN_SET (LEDGER_ID,?LEDGER_ID_COLLECTION);";
                        break;
                    }
                case SQLCommand.Mapping.MapMigratedLedgersByProject:
                    {
                        Query = @"UPDATE VOUCHER_TRANS SET LEDGER_ID=?MAP_LEDGER_ID WHERE FIND_IN_SET (LEDGER_ID,?LEDGER_ID_COLLECTION)
                                          AND VOUCHER_ID IN (SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS WHERE PROJECT_ID = ?PROJECT_ID);
                                    UPDATE VOUCHER_CC_TRANS SET LEDGER_ID=?MAP_LEDGER_ID,COST_CENTRE_TABLE = CONCAT('0LDR',?MAP_LEDGER_ID) WHERE FIND_IN_SET (LEDGER_ID,?LEDGER_ID_COLLECTION)
                                          AND VOUCHER_ID IN (SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS WHERE PROJECT_ID = ?PROJECT_ID);
                                    UPDATE TDS_CREDTIORS_PROFILE SET LEDGER_ID=?MAP_LEDGER_ID WHERE FIND_IN_SET (LEDGER_ID,?LEDGER_ID_COLLECTION);
                                    UPDATE ALLOT_FUND SET LEDGER_ID=?MAP_LEDGER_ID WHERE FIND_IN_SET (LEDGER_ID,?LEDGER_ID_COLLECTION)
                                           AND BUDGET_ID IN (SELECT BM.BUDGET_ID FROM BUDGET_MASTER BM
                                           INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID GROUP BY BM.BUDGET_ID);
                                    UPDATE BUDGET_LEDGER SET LEDGER_ID=?MAP_LEDGER_ID WHERE FIND_IN_SET (LEDGER_ID,?LEDGER_ID_COLLECTION)
                                            AND BUDGET_ID IN (SELECT BM.BUDGET_ID FROM BUDGET_MASTER BM
                                            INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID GROUP BY BM.BUDGET_ID);
                                    UPDATE BUDGET_COSTCENTER SET LEDGER_ID=?MAP_LEDGER_ID WHERE FIND_IN_SET (LEDGER_ID,?LEDGER_ID_COLLECTION)
                                            AND BUDGET_ID IN (SELECT BM.BUDGET_ID FROM BUDGET_MASTER BM
                                            INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID GROUP BY BM.BUDGET_ID);
                                    UPDATE FD_RENEWAL SET INTEREST_LEDGER_ID=?MAP_LEDGER_ID WHERE FIND_IN_SET (INTEREST_LEDGER_ID,?LEDGER_ID_COLLECTION)
                                          AND FD_ACCOUNT_ID IN (SELECT FD_ACCOUNT_ID FROM FD_ACCOUNT WHERE PROJECT_ID=?PROJECT_ID);
                                    UPDATE FD_ACCOUNT SET LEDGER_ID=?MAP_LEDGER_ID WHERE FIND_IN_SET (LEDGER_ID,?LEDGER_ID_COLLECTION) AND PROJECT_ID=?PROJECT_ID;";
                        break;
                    }
                case SQLCommand.Mapping.MergeCashBankLedgersFromBeginning:
                    {
                        //UPDATE VOUCHER_MASTER_TRANS VM, (SELECT VT.VOUCHER_ID FROM VOUCHER_MASTER_TRANS VM
                        //            INNER JOIN VOUCHER_TRANS VT ON VM.VOUCHER_ID = VT.VOUCHER_ID
                        //            WHERE VT.LEDGER_ID = ?FROM_LEDGER_ID AND VM.PROJECT_ID = ?FROM_PROJECT_ID) AS LEDGER_VOUCHER
                        //            SET PROJECT_ID = ?TO_PROJECT_ID
                        //            WHERE LEDGER_VOUCHER.VOUCHER_ID = VM.VOUCHER_ID;

                        //UPDATE FD_RENEWAL SET BANK_LEDGER_ID = ?TO_LEDGER_ID 
                        //                WHERE BANK_LEDGER_ID = ?FROM_LEDGER_ID AND FD_ACCOUNT_ID IN (SELECT FD_ACCOUNT_ID FROM FD_ACCOUNT WHERE PROJECT_ID = ?FROM_PROJECT_ID);
                        //, IS_AUDITOR_MODIFIED = IF(?IS_AUDITOR = 1, 1, IS_AUDITOR_MODIFIED)
                        Query = @"UPDATE VOUCHER_TRANS VT
                                    INNER JOIN VOUCHER_MASTER_TRANS VM ON VM.VOUCHER_ID = VT.VOUCHER_ID
                                    SET VT.LEDGER_ID = ?TO_LEDGER_ID, PROJECT_ID = ?TO_PROJECT_ID, MODIFIED_ON = NOW(), MODIFIED_BY=?MODIFIED_BY, 
                                    MODIFIED_BY_NAME=?USER_NAME
                                    WHERE VT.LEDGER_ID = ?FROM_LEDGER_ID AND VM.PROJECT_ID = ?FROM_PROJECT_ID;
                                    UPDATE VOUCHER_MASTER_TRANS SET PROJECT_ID = ?TO_PROJECT_ID
                                        WHERE VOUCHER_ID IN (SELECT FDR.FD_INTEREST_VOUCHER_ID
                                        FROM FD_ACCOUNT FD INNER JOIN FD_RENEWAL FDR ON FD.FD_ACCOUNT_ID = FDR.FD_ACCOUNT_ID
                                        WHERE FD.PROJECT_ID=?FROM_PROJECT_ID AND FD.BANK_LEDGER_ID IN (?FROM_LEDGER_ID) AND FD.STATUS=1 AND RENEWAL_TYPE = 'ACI');
                                    UPDATE FD_RENEWAL SET BANK_LEDGER_ID = ?TO_LEDGER_ID 
                                        WHERE BANK_LEDGER_ID IN (0, ?FROM_LEDGER_ID) AND FD_ACCOUNT_ID IN (SELECT FD_ACCOUNT_ID FROM FD_ACCOUNT WHERE PROJECT_ID = ?FROM_PROJECT_ID AND BANK_LEDGER_ID = ?FROM_LEDGER_ID);
                                    UPDATE FD_ACCOUNT SET BANK_ID = IF(?BANK_ID>0, ?BANK_ID, BANK_ID)  WHERE PROJECT_ID = ?FROM_PROJECT_ID AND BANK_LEDGER_ID  = ?FROM_LEDGER_ID;
                                    UPDATE FD_ACCOUNT SET PROJECT_ID = ?TO_PROJECT_ID, BANK_LEDGER_ID = ?TO_LEDGER_ID WHERE PROJECT_ID = ?FROM_PROJECT_ID AND BANK_LEDGER_ID  = ?FROM_LEDGER_ID;
                                    DELETE FROM LEDGER_BALANCE WHERE TRANS_FLAG <> 'OP' AND LEDGER_ID IN (?FROM_LEDGER_ID , ?TO_LEDGER_ID ) AND PROJECT_ID IN (?FROM_PROJECT_ID , ?TO_PROJECT_ID )";
                        break;
                    }
                case SQLCommand.Mapping.MergeCashBankLedgersForPeriod:
                    {
                        //UPDATE VOUCHER_MASTER_TRANS VM, (SELECT VT.VOUCHER_ID FROM VOUCHER_MASTER_TRANS VM
                        //            INNER JOIN VOUCHER_TRANS VT ON VM.VOUCHER_ID = VT.VOUCHER_ID
                        //            WHERE VT.LEDGER_ID = ?FROM_LEDGER_ID AND VM.PROJECT_ID = ?FROM_PROJECT_ID AND VM.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO) AS LEDGER_VOUCHER
                        //            SET PROJECT_ID = ?TO_PROJECT_ID
                        //            WHERE LEDGER_VOUCHER.VOUCHER_ID = VM.VOUCHER_ID;

                        //, IS_AUDITOR_MODIFIED = IF(?IS_AUDITOR = 1, 1, IS_AUDITOR_MODIFIED)
                        Query = @"UPDATE VOUCHER_TRANS VT
                                    INNER JOIN VOUCHER_MASTER_TRANS VM ON VM.VOUCHER_ID = VT.VOUCHER_ID
                                    SET VT.LEDGER_ID = ?TO_LEDGER_ID, PROJECT_ID = ?TO_PROJECT_ID, MODIFIED_ON = NOW(), MODIFIED_BY=?MODIFIED_BY, 
                                    MODIFIED_BY_NAME=?USER_NAME
                                    WHERE VT.LEDGER_ID = ?FROM_LEDGER_ID AND VM.PROJECT_ID = ?FROM_PROJECT_ID {AND VM.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO};
                                    DELETE FROM LEDGER_BALANCE WHERE TRANS_FLAG <> 'OP' AND LEDGER_ID IN (?FROM_LEDGER_ID , ?TO_LEDGER_ID ) 
                                            AND PROJECT_ID IN (?FROM_PROJECT_ID , ?TO_PROJECT_ID ) AND BALANCE_DATE BETWEEN ?DATE_FROM AND ?DATE_TO";
                        break;
                    }
                case SQLCommand.Mapping.MapProjectAgainstLedgerByCashBankLedger:
                    {
                        Query = "INSERT INTO PROJECT_LEDGER (LEDGER_ID, PROJECT_ID)\n" +
                                    "(SELECT DISTINCT LEDGER_ID, ?TO_PROJECT_ID AS PROEJCT_ID\n" +
                                    " FROM VOUCHER_TRANS VT WHERE VOUCHER_ID IN (SELECT VM.VOUCHER_ID FROM VOUCHER_MASTER_TRANS VM\n" +
                                    " INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VM.VOUCHER_ID\n" +
                                    " WHERE VM.PROJECT_ID = ?FROM_PROJECT_ID AND VT.LEDGER_ID = ?FROM_LEDGER_ID AND VM.STATUS=1))\n" +
                                    " ON DUPLICATE KEY UPDATE PROJECT_ID = ?TO_PROJECT_ID, LEDGER_ID = VT.LEDGER_ID;\n" +
                                "INSERT INTO PROJECT_LEDGER (LEDGER_ID, PROJECT_ID)\n" +
                                    "(SELECT DISTINCT FDR.INTEREST_LEDGER_ID, ?TO_PROJECT_ID AS PROEJCT_ID\n" +
                                    "FROM FD_ACCOUNT FD\n" +
                                    "INNER JOIN FD_RENEWAL FDR ON FD.FD_ACCOUNT_ID = FDR.FD_ACCOUNT_ID\n" +
                                    "WHERE FD.PROJECT_ID = ?FROM_PROJECT_ID AND FD.BANK_LEDGER_ID  = ?FROM_LEDGER_ID AND FD.STATUS=1 AND RENEWAL_TYPE = 'ACI')\n" +
                                    " ON DUPLICATE KEY UPDATE PROJECT_ID = ?TO_PROJECT_ID, LEDGER_ID = INTEREST_LEDGER_ID;";
                        break;
                    }
                case SQLCommand.Mapping.MapProjectAgainstLedgerByCashBankLedgerForPeriod:
                    {
                        Query = "INSERT INTO PROJECT_LEDGER (LEDGER_ID, PROJECT_ID)\n" +
                                    "(SELECT DISTINCT LEDGER_ID, ?TO_PROJECT_ID AS PROEJCT_ID\n" +
                                    " FROM VOUCHER_TRANS VT WHERE VOUCHER_ID IN (SELECT VM.VOUCHER_ID FROM VOUCHER_MASTER_TRANS VM\n" +
                                    " INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VM.VOUCHER_ID\n" +
                                    " WHERE VM.PROJECT_ID = ?FROM_PROJECT_ID AND VT.LEDGER_ID = ?FROM_LEDGER_ID AND VM.STATUS=1 AND VM.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO))\n" +
                                    " ON DUPLICATE KEY UPDATE PROJECT_ID = ?TO_PROJECT_ID, LEDGER_ID = VT.LEDGER_ID";
                        break;
                    }
                case SQLCommand.Mapping.MergeCashBankLedgersOPBalance:
                    {
                        Query = @"DELETE FROM LEDGER_BALANCE WHERE PROJECT_ID = ?FROM_PROJECT_ID AND LEDGER_ID = ?FROM_LEDGER_ID AND TRANS_FLAG=?TRANS_FLAG;
                                  DELETE FROM LEDGER_BALANCE WHERE PROJECT_ID = ?PROJECT_ID AND LEDGER_ID = ?LEDGER_ID AND TRANS_FLAG=?TRANS_FLAG;
                                  INSERT INTO LEDGER_BALANCE (BALANCE_DATE, PROJECT_ID, LEDGER_ID, AMOUNT, TRANS_MODE, TRANS_FC_MODE, TRANS_FLAG, BRANCH_ID)
                                  VALUES(?BALANCE_DATE, ?PROJECT_ID, ?LEDGER_ID, ?AMOUNT, ?TRANS_MODE, ?TRANS_MODE, ?TRANS_FLAG, ?BRANCH_ID);";
                        break;
                    }
                case SQLCommand.Mapping.FetchOpLedgers:
                    {
                        Query = "SELECT * FROM LEDGER_BALANCE WHERE TRANS_FLAG='OP' AND FIND_IN_SET(LEDGER_ID,?LEDGER_ID_COLLECTION) {AND PROJECT_ID IN (?PROJECT_ID)};";
                        break;
                    }
                case SQLCommand.Mapping.DeleteMergedLedgers:
                    {
                        Query = "DELETE FROM MASTER_LEDGER WHERE FIND_IN_SET(LEDGER_ID,?LEDGER_ID_COLLECTION); ";
                        break;
                    }
                case SQLCommand.Mapping.DeleteMergedLedgersByProject:
                    {
                        Query = @"DELETE FROM MASTER_LEDGER WHERE FIND_IN_SET(LEDGER_ID,?LEDGER_ID_COLLECTION) AND 
                                LEDGER_ID NOT IN (SELECT LEDGER_ID FROM PROJECT_LEDGER WHERE LEDGER_ID IN (?LEDGER_ID_COLLECTION) 
                                    AND PROJECT_ID <> ?PROJECT_ID GROUP BY LEDGER_ID)";
                        break;
                    }
                case SQLCommand.Mapping.UpdateLedgerCostCentre:
                    {
                        Query = "UPDATE MASTER_LEDGER SET IS_COST_CENTER =1 WHERE LEDGER_ID =?MAP_LEDGER_ID;";
                        break;
                    }
                case SQLCommand.Mapping.IsExistTDSLedger:
                    {
                        Query = "SELECT COUNT(*) AS TDS_LEDGER FROM MASTER_LEDGER WHERE IS_TDS_LEDGER=1 AND FIND_IN_SET(LEDGER_ID,?LEDGER_ID_COLLECTION);";
                        break;
                    }
                case SQLCommand.Mapping.IsExistBudgetEnabledLedger:
                    {
                        Query = "SELECT COUNT(*) FROM PROJECT_BUDGET_LEDGER PBL\n" +
                                "LEFT JOIN BUDGET_LEDGER BL ON BL.LEDGER_ID = PBL.LEDGER_ID\n" +
                                "WHERE PBL.LEDGER_ID IN (?LEDGER_ID);";
                        break;
                    }
                case SQLCommand.Mapping.UpdateLedgerTDS:
                    {
                        Query = "UPDATE MASTER_LEDGER SET IS_TDS_LEDGER =1 WHERE LEDGER_ID =?MAP_LEDGER_ID;";
                        break;
                    }
                case SQLCommand.Mapping.DeleteMergedOpBalance:
                    {
                        Query = "DELETE FROM LEDGER_BALANCE WHERE TRANS_FLAG='OP' AND FIND_IN_SET(LEDGER_ID,?LEDGER_ID_COLLECTION) {AND PROJECT_ID IN (?PROJECT_ID)};";
                        break;
                    }
                case SQLCommand.Mapping.FetchCommonProjectForMerge:
                    {
                        Query = "SELECT * FROM PROJECT_LEDGER WHERE FIND_IN_SET(LEDGER_ID,?LEDGER_ID_COLLECTION) { AND PROJECT_ID = ?PROJECT_ID};";
                        break;
                    }
                case SQLCommand.Mapping.DeleteCommonProjectForMerge:
                    {
                        Query = "DELETE FROM PROJECT_LEDGER WHERE FIND_IN_SET(LEDGER_ID,?LEDGER_ID_COLLECTION) { AND PROJECT_ID = ?PROJECT_ID};";
                        break;
                    }
                case SQLCommand.Mapping.FetchIEMergeLedgersBudget:
                    {
                        Query = "SELECT BL.BUDGET_ID, SUM(IF(BL.TRANS_MODE='CR', BL.PROPOSED_AMOUNT, -BL.PROPOSED_AMOUNT)) AS PROPOSED_AMOUNT,\n" +
                                "SUM(IF(BL.TRANS_MODE='CR', BL.APPROVED_AMOUNT, -BL.APPROVED_AMOUNT)) AS APPROVED_AMOUNT\n" +
                                "FROM BUDGET_LEDGER BL\n" +
                                "INNER JOIN (SELECT BM.BUDGET_ID, GROUP_CONCAT(BP.PROJECT_ID) FROM BUDGET_MASTER BM\n" +
                                "  INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID WHERE 1=1 { AND PROJECT_ID = ?PROJECT_ID}\n" +
                                "  GROUP BY BM.BUDGET_ID) BM ON BM.BUDGET_ID = BL.BUDGET_ID\n" +
                                "WHERE LEDGER_ID IN (?ID_COLLECTIONS)\n" +
                                "GROUP BY BL.BUDGET_ID;";

                        break;
                    }
                case SQLCommand.Mapping.FetchAIMergeLedgersBudget:
                    {
                        Query = "SELECT BL.BUDGET_ID, SUM(BL.PROPOSED_AMOUNT) AS PROPOSED_AMOUNT, SUM(BL.APPROVED_AMOUNT) AS APPROVED_AMOUNT, BL.TRANS_MODE\n" +
                                "FROM BUDGET_LEDGER BL\n" +
                                "INNER JOIN (SELECT BM.BUDGET_ID, GROUP_CONCAT(BP.PROJECT_ID) FROM BUDGET_MASTER BM\n" +
                                "  INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID WHERE 1=1  { AND PROJECT_ID = ?PROJECT_ID}\n" +
                                "  GROUP BY BM.BUDGET_ID) BM ON BM.BUDGET_ID = BL.BUDGET_ID\n" +
                                "WHERE LEDGER_ID IN (?ID_COLLECTIONS)\n" +
                                "GROUP BY BL.BUDGET_ID, BL.TRANS_MODE;";
                        break;
                    }
                #endregion

                #region Cost Center SQL
                case SQLCommand.Mapping.LoadAllCostCentreforcostcategory:
                    {
                        Query = "SELECT " +
                                    "COST_CENTRE_ID, " +
                                    "(SELECT 'DR') AS TRANS_MODE," +
                                    "COST_CENTRE_NAME, " +
                                    "(SELECT 0.0) AS AMOUNT " +
                                 "FROM MASTER_COST_CENTRE " +
                                 "WHERE COST_CENTRE_ID NOT IN (SELECT COST_CENTRE_ID FROM COSTCATEGORY_COSTCENTRE) " +
                                " ORDER BY COST_CENTRE_NAME";
                        break;
                    }
                case SQLCommand.Mapping.LoadAllCostCentre:
                    {
                        Query = "SELECT MCC.COST_CENTRE_ID,\n" +
                     "       (SELECT 'DR') AS TRANS_MODE,\n" +
                     "   --   CONCAT(CONCAT(MCC.COST_CENTRE_NAME, '  ('),\n" +
                     "   --         MC.COST_CENTRE_CATEGORY_NAME,\n" +
                     "    --          ')') AS COST_CENTRE_NAME,\n" +
                      " MCC.COST_CENTRE_NAME, MC.COST_CENTRE_CATEGORY_NAME,\n" +
                     "       (SELECT 0.0) AS AMOUNT\n" +
                     "  FROM MASTER_COST_CENTRE MCC\n" +
                     " INNER JOIN COSTCATEGORY_COSTCENTRE CC\n" +
                     "    on MCC.COST_CENTRE_ID = CC.COST_CENTRE_ID\n" +
                     " INNER JOIN MASTER_COST_CENTRE_CATEGORY MC\n" +
                     "    ON mc.COST_CENTRECATEGORY_ID = cc.COST_CATEGORY_ID\n" +
                     " ORDER BY mcc.COST_CENTRE_NAME";
                        break;
                    }
                case SQLCommand.Mapping.FetchMappedCostCenter:
                    {
                        Query = "SELECT PC.COST_CENTRE_ID, ABBREVATION, MCC.COST_CENTRE_NAME, MC.COST_CENTRE_CATEGORY_NAME, AMOUNT, TRANS_MODE " +
                                "FROM PROJECT_COSTCENTRE AS PC " +
                                "LEFT JOIN MASTER_COST_CENTRE AS MCC ON PC.COST_CENTRE_ID=MCC.COST_CENTRE_ID " +
                                "INNER JOIN COSTCATEGORY_COSTCENTRE CC ON MCC.COST_CENTRE_ID = CC.COST_CENTRE_ID\n" +
                                "INNER JOIN MASTER_COST_CENTRE_CATEGORY MC ON MC.COST_CENTRECATEGORY_ID = CC.COST_CATEGORY_ID\n" +
                                "WHERE 1=1 AND IF(?COSTCENTRE_MAPPING=1, PC.LEDGER_ID >0, PC.LEDGER_ID =0) {AND PC.PROJECT_ID=?PROJECT_ID} {AND PC.LEDGER_ID = ?LEDGER_ID} ";
                        break;
                    }
                case SQLCommand.Mapping.FetchMappedCostCenterByProjectLedger:
                    {
                        Query = "SELECT PC.COST_CENTRE_ID, ABBREVATION, MCC.COST_CENTRE_NAME, MC.COST_CENTRE_CATEGORY_NAME,\n" +
                                   "IF(CC_TRANS.COST_CENTRE_ID IS NULL,0,1) AS CC_TRANS_EXISTS, IF(PC.COST_CENTRE_ID IS NULL, 0, 1) AS SEL\n" +
                                   "FROM MASTER_COST_CENTRE AS MCC\n" +
                                   "LEFT JOIN PROJECT_COSTCENTRE AS PC ON PC.COST_CENTRE_ID = MCC.COST_CENTRE_ID {AND PC.PROJECT_ID IN (?PROJECT_IDs)} {AND PC.LEDGER_ID=?LEDGER_ID} \n" +
                                   "INNER JOIN COSTCATEGORY_COSTCENTRE CC ON MCC.COST_CENTRE_ID = CC.COST_CENTRE_ID\n" +
                                   "INNER JOIN MASTER_COST_CENTRE_CATEGORY MC ON MC.COST_CENTRECATEGORY_ID = CC.COST_CATEGORY_ID\n" +
                                   "LEFT JOIN (SELECT VCT.COST_CENTRE_ID\n" +
                                    "    FROM VOUCHER_MASTER_TRANS VM INNER JOIN VOUCHER_CC_TRANS VCT ON VM.VOUCHER_ID = VCT.VOUCHER_ID\n" +
                                    "    WHERE VM.STATUS=1 AND {VM.PROJECT_ID IN (?PROJECT_IDs)} {AND VCT.LEDGER_ID = ?LEDGER_ID}\n" +
                                    "    GROUP BY VCT.COST_CENTRE_ID) AS CC_TRANS ON CC_TRANS.COST_CENTRE_ID = MCC.COST_CENTRE_ID\n" +
                                   "WHERE 1=1 GROUP BY MCC.COST_CENTRE_ID;";
                        break;
                    }
                case SQLCommand.Mapping.FetchMappedCostCenterByCostCenterId:
                    {
                        Query = @"SELECT PD.PROJECT_ID,PROJECT,AMOUNT,TRANS_MODE
                                    FROM PROJECT_COSTCENTRE PD
                                    LEFT JOIN MASTER_COST_CENTRE PCC ON PCC.COST_CENTRE_ID=PD.COST_CENTRE_ID
                                    LEFT JOIN MASTER_PROJECT MP ON MP.PROJECT_ID=PD.PROJECT_ID
                                WHERE PD.COST_CENTRE_ID=?COST_CENTRE_ID ORDER BY PROJECT;";
                        break;
                    }

                case SQLCommand.Mapping.ProjectCostCentreMappingAdd:
                    {
                        Query = "INSERT INTO PROJECT_COSTCENTRE(PROJECT_ID, LEDGER_ID, COST_CENTRE_ID, AMOUNT, TRANS_MODE)\n" +
                                "VALUES(?PROJECT_ID, ?LEDGER_ID, ?COST_CENTRE_ID, ?AMOUNT, ?TRANS_MODE)";
                        break;
                    }
                case SQLCommand.Mapping.ProjectGeneralateMappingAdd:
                    {
                        Query = "INSERT INTO PORTAL_CONGREGATION_LEDGER_MAP (CON_LEDGER_ID, LEDGER_ID) VALUES (?CON_LEDGER_ID,?LEDGER_ID)";
                        break;
                    }
                case SQLCommand.Mapping.ProjectPurposeMappingAdd:
                    {
                        Query = "INSERT INTO PROJECT_PURPOSE(PROJECT_ID,CONTRIBUTION_ID,AMOUNT,TRANS_MODE) VALUES(?PROJECT_ID,?CONTRIBUTION_ID,?AMOUNT,?TRANS_MODE)";
                        break;
                    }
                case SQLCommand.Mapping.DeletePurposeCCDistribution:
                    {
                        Query = @"DELETE FROM PROJECT_PURPOSE_COSTCENTRE WHERE PROJECT_ID = ?PROJECT_ID";
                        break;
                    }
                case SQLCommand.Mapping.InsertUpdateProjectPurposeCCDistribution:
                    {
                        Query = @"INSERT INTO PROJECT_PURPOSE_COSTCENTRE (PROJECT_ID, CONTRIBUTION_ID, COST_CENTRE_ID, AMOUNT, TRANS_MODE)
                                   VALUES(?PROJECT_ID, ?CONTRIBUTION_ID, ?COST_CENTRE_ID, ?AMOUNT, ?TRANS_MODE)
                                   ON DUPLICATE KEY UPDATE PROJECT_ID =?PROJECT_ID, CONTRIBUTION_ID =?CONTRIBUTION_ID, COST_CENTRE_ID=?COST_CENTRE_ID";
                        break;
                    }
                case SQLCommand.Mapping.UpdateProjectLedgerCCDistribution:
                    {
                        Query = @"UPDATE PROJECT_COSTCENTRE SET AMOUNT=?AMOUNT, TRANS_MODE =?TRANS_MODE
                                   WHERE PROJECT_ID =?PROJECT_ID AND LEDGER_ID =?LEDGER_ID AND COST_CENTRE_ID=?COST_CENTRE_ID";
                        break;
                    }
                case SQLCommand.Mapping.UpdateProjectLedgerCCDistributionZero:
                    {
                        Query = @"UPDATE PROJECT_COSTCENTRE SET AMOUNT=0
                                   WHERE PROJECT_ID =?PROJECT_ID AND LEDGER_ID =?LEDGER_ID";
                        break;
                    }
                case SQLCommand.Mapping.UpdateProjectCostCentreOPBalance:
                    {
                        Query = @"UPDATE PROJECT_COSTCENTRE SET AMOUNT=?AMOUNT, TRANS_MODE =?TRANS_MODE
                                   WHERE PROJECT_ID =?PROJECT_ID AND COST_CENTRE_ID=?COST_CENTRE_ID";
                        break;
                    }
                case SQLCommand.Mapping.DeleteProjectCostCenterMapping:
                    {
                        Query = "DELETE FROM PROJECT_COSTCENTRE WHERE PROJECT_ID=?PROJECT_ID AND LEDGER_ID = ?LEDGER_ID;";
                        break;
                    }
                case SQLCommand.Mapping.DeleteProjectGeneralateMapping:
                    {
                        Query = "DELETE FROM PORTAL_CONGREGATION_LEDGER_MAP WHERE CON_LEDGER_ID = ?CON_LEDGER_ID";
                        break;
                    }
                case SQLCommand.Mapping.DeleteProjectPurposeMapping:
                    {
                        Query = "DELETE FROM PROJECT_PURPOSE WHERE PROJECT_ID=?PROJECT_ID;";
                        break;
                    }
                case SQLCommand.Mapping.UnMapCostCentreByCCId:
                    {
                        Query = "DELETE FROM PROJECT_COSTCENTRE WHERE COST_CENTRE_ID=?COST_CENTRE_ID AND LEDGER_ID=?LEDGER_ID;";
                        break;
                    }
                #endregion

                #region Donor SQL
                case SQLCommand.Mapping.FetchDonorMapped:
                    {
                        Query = "SELECT DONAUD_ID,NAME,COUNTRY,MC.COUNTRY_ID,CONCAT(NAME, ', ', ADDRESS ) AS NAMEADDRESS," +
                                    "CASE WHEN TYPE=1 THEN 'Institutional' ELSE " +
                                    "CASE WHEN TYPE=2 THEN 'Individual ' END END AS TYPE " +
                                "FROM PROJECT_DONOR PD " +
                                    "INNER JOIN MASTER_DONAUD MD ON MD.DONAUD_ID=PD.DONOR_ID " +
                                    "INNER JOIN MASTER_COUNTRY MC ON MC.COUNTRY_ID=MD.COUNTRY_ID " +
                                "WHERE PROJECT_ID=?PROJECT_ID ORDER BY DONAUD_ID DESC";
                        break;
                    }

                case SQLCommand.Mapping.FetchPurposeMapped:
                    {
                        Query = "SELECT  PP.CONTRIBUTION_ID,CODE," +
                                     "MCH.FC_PURPOSE, AMOUNT,TRANS_MODE FROM PROJECT_PURPOSE AS PP LEFT JOIN MASTER_CONTRIBUTION_HEAD AS MCH " +
                                    "ON PP.CONTRIBUTION_ID=MCH.CONTRIBUTION_ID " +
                                    "WHERE PROJECT_ID=?PROJECT_ID";
                        break;
                    }
                case SQLCommand.Mapping.FetchPurposeCostCentreDistribution:
                    {
                        Query = "SELECT PP.PROJECT_ID, PP.CONTRIBUTION_ID, PPC.COST_CENTRE_ID, PPC.AMOUNT, PPC.TRANS_MODE\n" +
                                 "FROM PROJECT_PURPOSE AS PP\n" +
                                 "INNER JOIN MASTER_CONTRIBUTION_HEAD AS MCH ON PP.CONTRIBUTION_ID=MCH.CONTRIBUTION_ID\n" +
                                 "INNER JOIN PROJECT_PURPOSE_COSTCENTRE AS PPC ON PPC.PROJECT_ID = PP.PROJECT_ID AND PPC.CONTRIBUTION_ID = PP.CONTRIBUTION_ID\n" +
                                 "                AND PPC.CONTRIBUTION_ID = MCH.CONTRIBUTION_ID\n" +
                                 "WHERE PPC.PROJECT_ID= ?PROJECT_ID;";
                        break;
                    }
                case SQLCommand.Mapping.FetchInvestmentType:
                    {
                        Query = @"SELECT INVESTMENT_TYPE_ID, INVESTMENT_TYPE FROM MASTER_INVESTMENT_TYPE ORDER BY INVESTMENT_TYPE";
                        break;
                    }
                case SQLCommand.Mapping.FetchInvestmentTypeIdByInvestmentType:
                    {
                        Query = @"SELECT INVESTMENT_TYPE_ID, INVESTMENT_TYPE FROM MASTER_INVESTMENT_TYPE WHERE INVESTMENT_TYPE = ?INVESTMENT_TYPE";
                        break;
                    }
                case SQLCommand.Mapping.FetchProjectLedgerCostCentreDistribution:
                    {
                        Query = "SELECT PL.PROJECT_ID, PL.LEDGER_ID, PC.COST_CENTRE_ID, PC.AMOUNT, PC.TRANS_MODE\n" +
                                 "FROM PROJECT_LEDGER AS PL\n" +
                                 "INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = PL.LEDGER_ID\n" +
                                 "INNER JOIN PROJECT_COSTCENTRE AS PC ON PC.PROJECT_ID = PL.PROJECT_ID AND PC.LEDGER_ID = PL.LEDGER_ID\n" +
                                 "      AND PC.LEDGER_ID = ML.LEDGER_ID\n" +
                                 "WHERE 1=1 {AND PL.PROJECT_ID = ?PROJECT_ID} {AND PC.PROJECT_ID = ?PROJECT_ID};";
                        break;
                    }
                case SQLCommand.Mapping.FetchProjectLedgerApplicable:
                    {
                        Query = @"SELECT PROJECT_ID, LEDGER_ID, APPLICABLE_FROM, APPLICABLE_TO FROM PROJECT_LEDGER_APPLICABLE 
                                    WHERE 1=1 {AND PROJECT_ID = ?PROJECT_ID} {AND LEDGER_ID = ?LEDGER_ID}";
                        break;
                    }
                case SQLCommand.Mapping.DeleteProjectLedgerApplicable:
                    {
                        Query = @"DELETE FROM PROJECT_LEDGER_APPLICABLE WHERE 1=1 {AND PROJECT_ID = ?PROJECT_ID} {AND LEDGER_ID = ?LEDGER_ID}";
                        break;
                    }
                case SQLCommand.Mapping.UpdateProjectLedgerApplicableByProject:
                    {
                        Query = @"INSERT INTO PROJECT_LEDGER_APPLICABLE (PROJECT_ID, LEDGER_ID, APPLICABLE_FROM, APPLICABLE_TO) " +
                                  "VALUES( ?PROJECT_ID, ?LEDGER_ID, ?APPLICABLE_FROM, ?APPLICABLE_TO)";
                        break;
                    }
                case SQLCommand.Mapping.CheckCostCentreMappingBySetting:
                    {
                        Query = "SELECT PC.COST_CENTRE_ID\n" +
                                 "FROM PROJECT_COSTCENTRE AS PC \n" +
                                 "WHERE IF(?COSTCENTRE_MAPPING = 1, LEDGER_ID > 0, LEDGER_ID = 0) LIMIT 1";
                        break;
                    }
                case SQLCommand.Mapping.DeleteAllCostCentreMapping:
                    {
                        Query = "DELETE FROM PROJECT_COSTCENTRE;";
                        break;
                    }
                case SQLCommand.Mapping.ChangeAllCostCentreMappingLedgerBased:
                    {
                        Query = "INSERT INTO PROJECT_COSTCENTRE (PROJECT_ID, LEDGER_ID, COST_CENTRE_ID, AMOUNT, TRANS_MODE)\n" +
                               "SELECT * FROM\n" +
                               "(SELECT VM.PROJECT_ID, VCT.LEDGER_ID, VCT.COST_CENTRE_ID, 0 AS AMOUNT, IF(NATURE_ID IN (1, 4),'CR','DR') AS TRANS_MODE\n" +
                               "  FROM VOUCHER_MASTER_TRANS VM\n" +
                               "  INNER JOIN VOUCHER_CC_TRANS VCT ON VCT.VOUCHER_ID = VM.VOUCHER_ID\n" +
                               "  INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VCT.LEDGER_ID\n" +
                               "  INNER JOIN MASTER_LEDGER_GROUP LG ON LG.GROUP_ID = ML.GROUP_ID\n" +
                               "  WHERE VM.STATUS = 1\n" +
                               "  GROUP BY VM.PROJECT_ID, VCT.LEDGER_ID, VCT.COST_CENTRE_ID\n" +
                               "  UNION\n" +
                               "  SELECT PL.PROJECT_ID, BCC.LEDGER_ID, BCC.COST_CENTRE_ID, 0 AS AMOUNT, BCC.TRANS_MODE\n" +
                               "  FROM BUDGET_COSTCENTER BCC\n" +
                               "  INNER JOIN PROJECT_LEDGER PL ON PL.LEDGER_ID = BCC.LEDGER_ID\n" +
                               "  WHERE (BCC.PROPOSED_AMOUNT >0 OR BCC.APPROVED_AMOUNT >0)\n" +
                               "  GROUP BY BCC.LEDGER_ID, BCC.COST_CENTRE_ID, BCC.TRANS_MODE) AS T";
                        break;
                    }
                case SQLCommand.Mapping.ChangeAllCostCentreMappingProjectBased:
                    {
                        Query = "INSERT INTO PROJECT_COSTCENTRE (PROJECT_ID, LEDGER_ID, COST_CENTRE_ID, AMOUNT, TRANS_MODE)\n" +
                                   "SELECT PROJECT_ID, 0 AS LEDGER_ID, COST_CENTRE_ID, 0 AS AMOUNT, TRANS_MODE\n" +
                                    "   FROM (SELECT VM.PROJECT_ID, VCT.COST_CENTRE_ID, 'DR' AS TRANS_MODE\n" +
                                    "      FROM VOUCHER_MASTER_TRANS VM\n" +
                                    "      INNER JOIN VOUCHER_CC_TRANS VCT ON VCT.VOUCHER_ID = VM.VOUCHER_ID\n" +
                                    "      WHERE VM.STATUS = 1 GROUP BY PROJECT_ID, COST_CENTRE_ID\n" +
                                    "      UNION\n" +
                                    "      SELECT PC.PROJECT_ID, PC.COST_CENTRE_ID, 'DR' AS TRANS_MODE FROM PROJECT_COSTCENTRE PC GROUP BY PROJECT_ID, COST_CENTRE_ID\n" +
                                    "      UNION\n" +
                                    "      SELECT PL.PROJECT_ID, BCC.COST_CENTRE_ID, 'DR' AS TRANS_MODE\n" +
                                    "      FROM BUDGET_COSTCENTER BCC\n" +
                                    "      INNER JOIN PROJECT_LEDGER PL ON PL.LEDGER_ID = BCC.LEDGER_ID\n" +
                                    "      WHERE (BCC.PROPOSED_AMOUNT >0 OR BCC.APPROVED_AMOUNT >0)\n" +
                                    "      GROUP BY BCC.LEDGER_ID, BCC.COST_CENTRE_ID, BCC.TRANS_MODE) AS T";
                        break;
                    }
                case SQLCommand.Mapping.FetchMappedDonorByDonorId:
                    {
                        Query = @"SELECT PD.PROJECT_ID,PROJECT
                                    FROM PROJECT_DONOR PD
                                    LEFT JOIN MASTER_DONAUD MD ON MD.DONAUD_ID=PD.DONOR_ID
                                    LEFT JOIN MASTER_PROJECT MP ON MP.PROJECT_ID=PD.PROJECT_ID
                                    WHERE DONOR_ID=?DONAUD_ID ORDER BY PROJECT;";
                        break;
                    }
                case SQLCommand.Mapping.LoadAllDonor:
                    {
                        Query = "SELECT DONAUD_ID,NAME,COUNTRY," +
                                "CASE WHEN TYPE=1 THEN 'Institutional' ELSE " +
                                "CASE WHEN TYPE=2 THEN 'Individual ' END " +
                                "END AS TYPE " +
                                "FROM MASTER_DONAUD MD " +
                                      "INNER JOIN MASTER_COUNTRY MC ON MD.COUNTRY_ID=MC.COUNTRY_ID " +
                                "WHERE IDENTITYKEY=0;";
                        break;
                    }
                case SQLCommand.Mapping.LoadAllPurpose:
                    {
                        Query = "SELECT MCH.CONTRIBUTION_ID, " +
                                      " (SELECT 'DR') AS TRANS_MODE, " +
                                  "MCH.FC_PURPOSE, (SELECT 0.0) AS AMOUNT " +
                                  "FROM MASTER_CONTRIBUTION_HEAD MCH " +
                                 "ORDER BY MCH.FC_PURPOSE";
                        break;
                    }
                case SQLCommand.Mapping.LoadGeneralateLedger:
                    {
                        Query = "SELECT CON_LEDGER_ID," +
                                 "CON_LEDGER_CODE," +
                                 "CONCAT(IFNULL(CON_LEDGER_CODE,' '),'  ',CON_LEDGER_NAME) AS CON_LEDGER_NAME, " +
                                 "CON_PARENT_LEDGER_ID " +
                               "FROM PORTAL_CONGREGATION_LEDGER " +
                               "WHERE (CON_LEDGER_ID <> CON_PARENT_LEDGER_ID) OR CON_LEDGER_CODE IN ('A.2','A.3','A.5','B.1','B.4','C.6','C.7','C.8', 'C.9', 'D.6', 'D.7') " +
                               "ORDER BY CON_LEDGER_CODE, CON_LEDGER_NAME";
                        break;
                    }
                case SQLCommand.Mapping.DonorMap:
                    {
                        Query = "INSERT INTO PROJECT_DONOR(PROJECT_ID,DONOR_ID) VALUES(?PROJECT_ID,?DONAUD_ID);";
                        break;
                    }
                case SQLCommand.Mapping.CheckDonorMapped:
                    {
                        Query = "SELECT DONOR_ID FROM PROJECT_DONOR WHERE PROJECT_ID=?PROJECT_ID AND DONOR_ID =?DONAUD_ID";
                        break;
                    }

                case SQLCommand.Mapping.DonorUnMap:
                    {
                        Query = "DELETE FROM PROJECT_DONOR WHERE PROJECT_ID=?PROJECT_ID;";
                        break;
                    }
                case SQLCommand.Mapping.DonorUnMapByDonorId:
                    {
                        Query = "DELETE FROM PROJECT_DONOR WHERE DONOR_ID=?DONAUD_ID;";
                        break;
                    }
                case SQLCommand.Mapping.FetchMappedActiveDonors:
                    {
                        //Query = "SELECT DONAUD_ID,NAME,COUNTRY,MC.COUNTRY_ID,CONCAT(NAME, ', ', ADDRESS ) AS NAMEADDRESS," +
                        //           "CASE WHEN TYPE=1 THEN 'Institutional' ELSE " +
                        //           "CASE WHEN TYPE=2 THEN 'Individual ' END END AS TYPE " +
                        //       "FROM PROJECT_DONOR PD " +
                        //           "INNER JOIN MASTER_DONAUD MD ON MD.DONAUD_ID=PD.DONOR_ID " +
                        //           "INNER JOIN MASTER_COUNTRY MC ON MC.COUNTRY_ID=MD.COUNTRY_ID " +
                        //       "WHERE PROJECT_ID=?PROJECT_ID AND MD.STATUS=1ORDER BY DONAUD_ID DESC";

                        // Query = "SELECT DONAUD_ID,\n" +
                        // "       NAME,\n" +
                        // "       COUNTRY,\n" +
                        // "       MC.COUNTRY_ID,\n" +
                        // "       CONCAT(NAME, ', ', ADDRESS) AS NAMEADDRESS,\n" +
                        //// "       CONCAT(NAME, ', ', ADDRESS) AS NAMEADDRESS,\n" +
                        // "       CASE\n" +
                        // "         WHEN TYPE = 1 THEN\n" +
                        // "          'Institutional'\n" +
                        // "         ELSE\n" +
                        // "          CASE\n" +
                        // "            WHEN TYPE = 2 THEN\n" +
                        // "             'Individual '\n" +
                        // "          END\n" +
                        // "       END AS TYPE\n" +
                        // "  FROM PROJECT_DONOR PD\n" +
                        // " INNER JOIN MASTER_DONAUD MD\n" +
                        // "    ON MD.DONAUD_ID = PD.DONOR_ID\n" +
                        // " INNER JOIN MASTER_COUNTRY MC\n" +
                        // "    ON MC.COUNTRY_ID = MD.COUNTRY_ID\n" +
                        // " WHERE PROJECT_ID = ?PROJECT_ID\n" +
                        // "   AND MD.STATUS = 1\n" +
                        // "{UNION\n" +
                        // "SELECT DONAUD_ID,\n" +
                        // "       NAME,\n" +
                        // "       COUNTRY,\n" +
                        // "       MC.COUNTRY_ID,\n" +
                        // "       CONCAT(NAME, ', ', ADDRESS) AS NAMEADDRESS,\n" +
                        // "       CASE\n" +
                        // "         WHEN TYPE = 1 THEN\n" +
                        // "          'Institutional'\n" +
                        // "         ELSE\n" +
                        // "          CASE\n" +
                        // "            WHEN TYPE = 2 THEN\n" +
                        // "             'Individual '\n" +
                        // "          END\n" +
                        // "       END AS TYPE\n" +
                        // "  FROM MASTER_DONAUD MD\n" +
                        //// " INNER JOIN MASTER_DONAUD MD\n" +
                        // //"    ON MD.DONAUD_ID = PD.DONOR_ID\n" +
                        // " INNER JOIN MASTER_COUNTRY MC\n" +
                        // "    ON MC.COUNTRY_ID = MD.COUNTRY_ID\n" +
                        //// " WHERE PROJECT_ID = ?PROJECT_ID\n" +
                        // "   WHERE MD.DONAUD_ID = ?DONAUD_ID}\n" +
                        // " ORDER BY DONAUD_ID DESC;";

                        Query = "\n" +
                        "SELECT DONAUD_ID,\n" +
                        "       NAME,\n" +
                        "       LASTNAME,\n" +
                        "       CONCAT(NAME, ' ',IFNULL(LASTNAME,'')) AS FIRSTLASTNAME,\n" +
                        "       MC.COUNTRY,\n" +
                        "       MC.COUNTRY_ID,\n" +
                        "       STATE_NAME AS STATE,\n" +
                        "       ADDRESS,\n" +
                        "       CASE\n" +
                        "         WHEN TYPE = 1 THEN\n" +
                        "          'Institutional'\n" +
                        "         ELSE\n" +
                        "          CASE\n" +
                        "            WHEN TYPE = 2 THEN\n" +
                        "             'Individual '\n" +
                        "          END\n" +
                        "       END AS TYPE\n" +
                        "  FROM PROJECT_DONOR PD\n" +
                        " INNER JOIN MASTER_DONAUD MD\n" +
                        "    ON MD.DONAUD_ID = PD.DONOR_ID\n" +
                        " INNER JOIN MASTER_COUNTRY MC\n" +
                        "    ON MC.COUNTRY_ID = MD.COUNTRY_ID\n" +
                        " LEFT JOIN MASTER_STATE MS\n" +
                        "ON MD.STATE_ID = MS.STATE_ID\n" +
                        " WHERE PROJECT_ID = ?PROJECT_ID\n" +
                        "   AND MD.STATUS = 1 {UNION  SELECT DONAUD_ID,\n" +
                        "               NAME,\n" +
                        "               LASTNAME,\n" +
                        "               CONCAT(NAME, ' ', LASTNAME) AS FIRSTLASTNAME,\n" +
                        "               COUNTRY,\n" +
                        "               MC.COUNTRY_ID,\n" +
                        "               STATE_NAME AS STATE,\n" +
                        "               ADDRESS,\n" +
                        "               CASE\n" +
                        "                 WHEN TYPE = 1 THEN\n" +
                        "                  'Institutional'\n" +
                        "                 ELSE\n" +
                        "                  CASE\n" +
                        "                    WHEN TYPE = 2 THEN\n" +
                        "                     'Individual '\n" +
                        "                  END\n" +
                        "               END AS TYPE\n" +
                        "          FROM MASTER_DONAUD MD\n" +
                            //  "           // " INNER JOIN MASTER_DONAUD MD\n" +
                            // "         //"    ON MD.DONAUD_ID = PD.DONOR_ID\n" +
                        "         INNER JOIN MASTER_COUNTRY MC\n" +
                        "            ON MC.COUNTRY_ID = MD.COUNTRY_ID\n" +
                        " INNER JOIN MASTER_STATE MS\n" +
                        "ON MD.STATE_ID = MS.STATE_ID\n" +
                            // "         // " WHERE PROJECT_ID = ?PROJECT_ID\n" +
                        "         WHERE MD.DONAUD_ID = ?DONAUD_ID}\n" +
                        "         ORDER BY DONAUD_ID DESC;";
                        break;
                    }
                case SQLCommand.Mapping.FetchDonorStatus:
                    {
                        Query = "SELECT STATUS FROM MASTER_DONAUD WHERE DONAUD_ID=?DONAUD_ID;";
                        break;
                    }
                #endregion

                #region Common SQL
                case SQLCommand.Mapping.ProjectLedgerMappingAdd:
                    {
                        Query = "INSERT INTO PROJECT_LEDGER(PROJECT_ID,LEDGER_ID) VALUES(?PROJECT_ID,?LEDGER_ID);";
                        break;
                    }
                case SQLCommand.Mapping.ProjectBudgetLedgerMappingAdd:
                    {
                        Query = "INSERT INTO PROJECT_BUDGET_LEDGER(PROJECT_ID,LEDGER_ID) VALUES(?PROJECT_ID,?LEDGER_ID);";
                        break;
                    }
                case SQLCommand.Mapping.MapProjectLedger:
                    {
                        Query = "INSERT INTO PROJECT_LEDGER\n" +
                                "(PROJECT_ID, LEDGER_ID)\n" +
                                "VALUES(?PROJECT_ID,?LEDGER_ID) \n" +
                                " ON DUPLICATE KEY UPDATE PROJECT_ID =?PROJECT_ID, LEDGER_ID =?LEDGER_ID";
                        break;
                    }
                case SQLCommand.Mapping.MapBudgetRecLedgerForAllProjects:
                    {
                        Query = "INSERT INTO PROJECT_BUDGET_LEDGER (PROJECT_ID, LEDGER_ID) SELECT MP.PROJECT_ID, ML.LEDGER_ID\n" +
                                "FROM MASTER_PROJECT MP\n" +
                                "CROSS JOIN MASTER_LEDGER ML\n" +
                                "WHERE BUDGET_GROUP_ID=1 ON DUPLICATE KEY UPDATE PROJECT_ID = MP.PROJECT_ID, LEDGER_ID = ML.LEDGER_ID";
                        break;
                    }
                case SQLCommand.Mapping.MapCostCentreToProject:
                    {
                        Query = @"INSERT INTO PROJECT_COSTCENTRE(PROJECT_ID,COST_CENTRE_ID) VALUES(?PROJECT_ID,?COST_CENTRE_ID) ON DUPLICATE KEY UPDATE PROJECT_ID=?PROJECT_ID, COST_CENTRE_ID=?COST_CENTRE_ID;";
                        break;
                    }
                case SQLCommand.Mapping.CostcentreCostCategoryMappingAdd:
                    {
                        Query = @"INSERT INTO COSTCATEGORY_COSTCENTRE(COST_CATEGORY_ID, COST_CENTRE_ID) VALUES(?COST_CENTRECATEGORY_ID,?COST_CENTRE_ID);";
                        break;
                    }
                case SQLCommand.Mapping.DeleteCostCategory:
                    {
                        Query = @"DELETE FROM COSTCATEGORY_COSTCENTRE WHERE COST_CENTRE_ID=?COST_CENTRE_ID;";
                        break;
                    }
                case SQLCommand.Mapping.BankIdByLedgerId:
                    {
                        Query = "SELECT BANK_ACCOUNT_ID FROM MASTER_LEDGER WHERE LEDGER_ID=?LEDGER_ID;";
                        break;
                    }
                case SQLCommand.Mapping.FixedDepositId:
                    {
                        Query = "SELECT LEDGER_ID FROM  MASTER_LEDGER WHERE GROUP_ID=14;";
                        break;
                    }
                case SQLCommand.Mapping.FetchcategoryLedgerByProject:
                    {
                        Query = "SELECT GROUP_CONCAT(LEDGER_ID) AS LEDGER_ID\n" +
                                "  FROM PROJECT_CATEGORY_LEDGER\n" +
                                " WHERE PROJECT_CATEGORY_ID IN\n" +
                                "       (SELECT PROJECT_CATEGORY_ID FROM MASTER_PROJECT WHERE PROJECT_ID = ?PROJECT_ID);";

                        break;
                    }
                case SQLCommand.Mapping.FetchBudgetActualLedgers:
                    {
                        //Query = "SELECT PROJECT_ID,LEDGER_ID, 0.00 AS AMOUNT, 'DR' AS TRANS_MODE  FROM VOUCHER_TRANS VT\n" +
                        //        "INNER JOIN VOUCHER_MASTER_TRANS VMT ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        //        "WHERE VMT.STATUS = 1 AND VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                        //        "GROUP BY VT.LEDGER_ID ORDER BY PROJECT_ID";

                        //Query = " SELECT VMT.PROJECT_ID, VT.LEDGER_ID, SUM(VT.AMOUNT) AS AMOUNT, 'DR' AS TRANS_MODE\n" +
                        //         "   FROM VOUCHER_TRANS VT INNER JOIN VOUCHER_MASTER_TRANS VMT ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        //         "   WHERE VMT.STATUS = 1 AND VMT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                        //         "   GROUP BY VMT.PROJECT_ID, VT.LEDGER_ID ORDER BY VT.LEDGER_ID, VMT.PROJECT_ID";

                        Query = "SELECT VMT.PROJECT_ID, VT.LEDGER_ID, SUM(VT.AMOUNT) AS AMOUNT, 'DR' AS TRANS_MODE\n" +
                                  " FROM VOUCHER_TRANS VT INNER JOIN VOUCHER_MASTER_TRANS VMT ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                                  " LEFT JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                                  " WHERE VMT.STATUS = 1 AND VMT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO AND GROUP_ID NOT IN (12,13,14)\n" +
                                  " GROUP BY VMT.PROJECT_ID, VT.LEDGER_ID ORDER BY VT.LEDGER_ID, VMT.PROJECT_ID";
                        break;
                    }
                #endregion

                #region CostCategory SQL
                case SQLCommand.Mapping.DeleteMapCostCategory:
                    {
                        Query = "DELETE FROM COSTCATEGORY_COSTCENTRE WHERE COST_CATEGORY_ID=?COST_CENTRECATEGORY_ID";
                        break;
                    }
                case SQLCommand.Mapping.FetchMappedCostCategory:
                    {
                        Query = "SELECT MCC.COST_CENTRE_ID,MCC.COST_CENTRE_NAME\n" +
                                "  FROM MASTER_COST_CENTRE MCC\n" +
                                " INNER JOIN COSTCATEGORY_COSTCENTRE CCC\n" +
                                "    ON CCC.COST_CENTRE_ID = MCC.COST_CENTRE_ID\n" +
                                " WHERE CCC.COST_CATEGORY_ID IN (?COST_CENTRECATEGORY_ID)";
                        break;
                    }
                case SQLCommand.Mapping.CostCentreCategoryUnmap:
                    {
                        Query = @"SELECT COST_CENTRE_ID FROM VOUCHER_CC_TRANS VCCT
                                 LEFT JOIN VOUCHER_MASTER_TRANS VMT ON VMT.VOUCHER_ID = VCCT.VOUCHER_ID
                                 WHERE FIND_IN_SET(VCCT.COST_CENTRE_ID,?IDs) AND PROJECT_ID=?PROJECT_ID {AND VCCT.LEDGER_ID =?LEDGER_ID} AND STATUS = 1
                                 UNION ALL
                                 SELECT PPC.COST_CENTRE_ID FROM PROJECT_PURPOSE_COSTCENTRE PPC WHERE PPC.PROJECT_ID = ?PROJECT_ID AND FIND_IN_SET(PPC.COST_CENTRE_ID,?IDs)
                                 UNION ALL
                                 SELECT COST_CENTRE_ID FROM BUDGET_COSTCENTER BCC INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BCC.BUDGET_ID
                                 WHERE BP.PROJECT_ID IN (?PROJECT_ID) AND FIND_IN_SET(BCC.COST_CENTRE_ID,?IDs)
                                 UNION ALL
                                 SELECT COST_CENTRE_ID FROM BUDGET_STRENGTH_DETAIL BSD INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BSD.BUDGET_ID
                                 WHERE BP.PROJECT_ID IN (?PROJECT_ID) AND FIND_IN_SET(BSD.COST_CENTRE_ID,?IDs)
                                 UNION ALL
                                 SELECT COST_CENTRE_ID
                                 FROM MASTER_REPORT_BUDGET_NEW_PROJECTS_DETAILS NRD INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = NRD.BUDGET_ID
                                 WHERE BP.PROJECT_ID IN (?PROJECT_ID) AND FIND_IN_SET(NRD.COST_CENTRE_ID,?IDs)";
                        //Query = "SELECT COST_CENTRE_ID FROM VOUCHER_CC_TRANS WHERE COST_CENTRE_ID IN (?IDs)";
                        break;
                    }
                case SQLCommand.Mapping.FetchCostCentreUnmapTransaction:
                    {
                        Query = @"SELECT COST_CENTRE_ID,VMT.VOUCHER_ID
                                  FROM VOUCHER_CC_TRANS VCCT
                                  INNER JOIN VOUCHER_MASTER_TRANS VMT
                                    ON VMT.VOUCHER_ID = VCCT.VOUCHER_ID
                                 WHERE  FIND_IN_SET(VCCT.COST_CENTRE_ID,?IDs) {AND PROJECT_ID=?PROJECT_ID}
                                   AND STATUS = 1;";
                        break;
                    }
                case SQLCommand.Mapping.FetchFCPurposeUnmapTransaction:
                    {
                        Query = @"SELECT VMT.PURPOSE_ID, VMT.VOUCHER_ID
                                FROM VOUCHER_MASTER_TRANS VMT
                                WHERE FIND_IN_SET(VMT.PURPOSE_ID,?IDs) {AND PROJECT_ID=?PROJECT_ID} AND STATUS = 1;";
                        break;
                    }
                #endregion
            }
            return Query;
        }
        #endregion
    }
}
