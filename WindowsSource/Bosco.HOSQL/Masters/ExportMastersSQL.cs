using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;
using Bosco.Utility;

namespace Bosco.HOSQL
{
    public class ExportMastersSQL : IDatabaseQuery
    {
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;
        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.ExportMasters).FullName)
            {
                query = GetImportVoucherQuery();
            }

            sqlType = this.sqlType;
            return query;
        }

        private string GetImportVoucherQuery()
        {
            string sQuery = string.Empty;
            SQLCommand.ExportMasters sqlCommandId = (SQLCommand.ExportMasters)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                case SQLCommand.ExportMasters.LegalEntityFetchAll:
                    {
                        //To insert Some Fields like REGDATE,PERMISSIONDATE,FCRINO,FCRIREGDATE ,EIGHTYGNO,OTHER_ASSOCIATION_NATURE,OTHER_DENOMINATION

                        sQuery = "SELECT SOCIETYNAME,\n" +
                                    "    CONTACTPERSON,\n" +
                                    "    ADDRESS,\n" +
                                    "    PLACE,\n" +
                                    "    STATE,\n" +
                                    "    COUNTRY,\n" +
                                    "    PINCODE,\n" +
                                    "    PHONE,\n" +
                                    "    FAX,\n" +
                                    "    EMAIL,\n" +
                                    "    URL,\n" +
                                    "    REGNO,\n" +
                                    "    REGDATE,\n" +
                                    "    REGDATE,\n" +
                                    "    PERMISSIONNO,\n" +
                                    "    PERMISSIONDATE,\n" +
                                    "    A12NO,\n" +
                                    "    PANNO,\n" +
                                    "    GIRNO,\n" +
                                    "    TANNO,\n" +
                                    "    FCRINO,\n" +
                                    "    FCRIREGDATE,\n" +
                                    "    EIGHTYGNO, EIGHTY_GNO_REG_DATE,\n" +
                                    "    ASSOCIATIONNATURE,\n" +
                                    "    OTHER_ASSOCIATION_NATURE,\n" +
                                    "    DENOMINATION,\n" +
                                    "    OTHER_DENOMINATION\n" +
                                    "  FROM master_insti_perference MLE\n" +
                                    "  LEFT JOIN MASTER_PROJECT MRP\n" +
                                    "    ON MRP.CUSTOMERID = MLE.CUSTOMERID\n" +
                                    "  LEFT JOIN PROJECT_BRANCH PB\n" +
                                    "    ON MRP.PROJECT_ID = PB.PROJECT_ID\n" +
                                    " WHERE PB.BRANCH_ID IN\n" +
                                    "       (SELECT BRANCH_OFFICE_ID\n" +
                                    "          FROM BRANCH_OFFICE\n" +
                                    "         WHERE BRANCH_OFFICE_CODE = ?BRANCH_OFFICE_CODE)\n" +
                                    " GROUP BY MRP.CUSTOMERID";

                        break;
                    }
                //case SQLCommand.ExportMasters.ProjectCategoryFetchAll:
                //    {
                //        sQuery = "SELECT PROJECT_CATOGORY_NAME FROM MASTER_PROJECT_CATOGORY";
                //        break;
                //    }
                case SQLCommand.ExportMasters.ProjectFetchAll:
                    {
                        sQuery = "SELECT MPRJ.PROJECT_CODE,\n" +
                                    "       MPRJ.PROJECT,\n" +
                                    "       MD.DIVISION_ID,\n" +
                                    "       MPRJ.ACCOUNT_DATE,\n" +
                                    "       MPRJ.DATE_STARTED,\n" +
                                    "       MPRJ.DATE_CLOSED, MPRJ.CLOSED_BY,\n" +
                                    "       MPRJ.DESCRIPTION,\n" +
                                    "       MPRJ.NOTES,\n" +
                                    "       MPRJC.PROJECT_CATOGORY_NAME,\n" +
                                    "       MPCITR.PROJECT_CATOGORY_ITRGROUP,\n" +
                                    "       MPRJ.DELETE_FLAG,\n" +
                                    "       MIS.SOCIETYNAME,\n" +
                                    "       BL.LOCATION_NAME\n" +
                                    "  FROM MASTER_PROJECT MPRJ\n" +
                                    "  LEFT JOIN MASTER_DIVISION MD\n" +
                                    "    ON MPRJ.DIVISION_ID = MD.DIVISION_ID\n" +
                                    "  LEFT JOIN MASTER_INSTI_PERFERENCE MIS\n" +
                                    "    ON MIS.CUSTOMERID = MPRJ.CUSTOMERID\n" +
                                    "  LEFT JOIN MASTER_PROJECT_CATOGORY MPRJC\n" +
                                    "    ON MPRJC.PROJECT_CATOGORY_ID = MPRJ.PROJECT_CATEGORY_ID\n" +
                                    "  LEFT JOIN MASTER_PROJECT_CATOGORY_ITRGROUP MPCITR\n" +
                                    "    ON MPCITR.PROJECT_CATOGORY_ITRGROUP_ID = MPRJC.PROJECT_CATOGORY_ITRGROUP_ID\n" +
                                    "  LEFT JOIN PROJECT_BRANCH PB\n" +
                                    "    ON PB.PROJECT_ID = MPRJ.PROJECT_ID\n" +
                                    "  LEFT JOIN BRANCH_LOCATION BL\n" +
                                    "    ON BL.LOCATION_ID=PB.LOCATION_ID\n" +
                                    " WHERE PB.BRANCH_ID IN\n" +
                                    "       (SELECT BRANCH_OFFICE_ID\n" +
                                    "          FROM BRANCH_OFFICE\n" +
                                    "         WHERE BRANCH_OFFICE_CODE = ?BRANCH_OFFICE_CODE)";
                        break;
                    }
                case SQLCommand.ExportMasters.LedgerGroupFetchAll:
                    {
                        sQuery = "SELECT lg.GROUP_CODE,\n" +
                                 "       lg.LEDGER_GROUP,\n" +
                                 "       t.ledger_group  as ParentGroup,\n" +
                                 "       mn.NATURE,\n" +
                                 "       t1.ledger_group as MainGroup,\n" +
                                 "       lg.IMAGE_ID,\n" +
                                 "       lg.ACCESS_FLAG,\n" +
                                 "       lg.SORT_ORDER\n" +
                                 "  FROM master_ledger_group lg\n" +
                                 "  LEFT JOIN MASTER_NATURE mn\n" +
                                 "    on (mn.nature_id = lg.nature_id)\n" +
                                 " INNER JOIN (SELECT GROUP_ID, LEDGER_GROUP\n" +
                                 "               FROM MASTER_LEDGER_GROUP\n" +
                                 "              WHERE PARENT_GROUP_ID) as t\n" +
                                 "    ON lg.parent_group_id = t.group_id\n" +
                                 " INNER JOIN (SELECT GROUP_ID, LEDGER_GROUP\n" +
                                 "               FROM MASTER_LEDGER_GROUP\n" +
                                 "              WHERE PARENT_GROUP_ID) as t1\n" +
                                 "    ON lg.main_group_id = t1.group_id ORDER BY LG.GROUP_ID";
                        break;
                    }
                case SQLCommand.ExportMasters.GeneralateLedgerFetchAll:
                    {
                        sQuery = "SELECT CN.CON_LEDGER_ID,\n" +
                        "       CN.CON_LEDGER_CODE,\n" +
                        "       CN.CON_LEDGER_NAME,\n" +
                        "       T.CON_LEDGER_NAME  AS PARENT,\n" +
                        "       T1.CON_LEDGER_NAME AS MAINPARENT\n" +
                        "  FROM CONGREGATION_LEDGER AS CN\n" +
                        " INNER JOIN (SELECT CON_LEDGER_ID, CON_LEDGER_NAME\n" +
                        "               FROM CONGREGATION_LEDGER\n" +
                        "              WHERE CON_PARENT_LEDGER_ID) as t\n" +
                        "    ON CN.CON_PARENT_LEDGER_ID = t.CON_LEDGER_ID\n" +
                        " INNER JOIN (SELECT CON_LEDGER_ID, CON_LEDGER_NAME\n" +
                        "               FROM CONGREGATION_LEDGER\n" +
                        "              WHERE CON_MAIN_PARENT_ID) as t1\n" +
                        "    ON CN.CON_MAIN_PARENT_ID = t1.CON_LEDGER_ID\n" +
                        " ORDER BY CN.CON_LEDGER_ID";
                        break;
                    }
                case SQLCommand.ExportMasters.GeneralateLedgerMapAll:
                    {
                        sQuery = "SELECT LEDGER_NAME, CON_LEDGER_NAME FROM CONGREGATION_LEDGER_MAP MLM\n" +
                         " INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = MLM.LEDGER_ID\n" +
                         " INNER JOIN CONGREGATION_LEDGER CL ON CL.CON_LEDGER_ID = MLM.CON_LEDGER_ID;";
                        break;
                    }
                case SQLCommand.ExportMasters.LedgerFetchAll:
                    {
                        sQuery = "SELECT ML.LEDGER_CODE,\n" +
                                  "       ML.LEDGER_NAME,\n" +
                                  "       MLG.LEDGER_GROUP,\n" +
                                  "       MLG.NATURE_ID,\n" +
                                  "       MLG.GROUP_CODE,\n" +
                                  "       ML.LEDGER_TYPE,\n" +
                                  "       ML.LEDGER_SUB_TYPE,\n" +
                                  "       ML.BANK_ACCOUNT_ID,\n" +
                                  "       ML.IS_COST_CENTER,\n" +
                                  "       ML.IS_BANK_INTEREST_LEDGER,\n" +
                                  "       ML.IS_DEPRECIATION_LEDGER,\n" +
                                  "       ML.IS_INKIND_LEDGER,\n" +
                                  "       ML.IS_ASSET_GAIN_LEDGER,\n" +
                                  "       ML.IS_ASSET_LOSS_LEDGER,\n" +
                                  "       ML.IS_DISPOSAL_LEDGER,\n" +
                                  "       ML.IS_TDS_LEDGER, ML.IS_BANK_FD_PENALTY_LEDGER, ML.IS_BANK_SB_INTEREST_LEDGER, ML.IS_BANK_COMMISSION_LEDGER,\n" +
                                  "       ML.NOTES,\n" +
                                  "       ML.SORT_ID,\n" +
                                  "       STATUS,\n" +
                                  "       ML.ACCESS_FLAG,\n" +
                                  "       GROUP_CONCAT(DISTINCT(PC.PROJECT_CATOGORY_NAME)) AS PROJECT_CATOGORY_NAME,\n" +
                                  "       GROUP_CONCAT(DISTINCT(BL.LOCATION_NAME) ORDER BY LOCATION_NAME SEPARATOR ',') AS LOCATION_NAME,\n" +
                                  "       IFNULL(BG.BUDGET_GROUP,'') AS BUDGET_GROUP, IFNULL(BSG.BUDGET_SUB_GROUP, '') AS BUDGET_SUB_GROUP,\n" +
                                  "       IFNULL(ML.BUDGET_GROUP_ID,0) AS BUDGET_GROUP_ID, IFNULL(ML.BUDGET_SUB_GROUP_ID, 0 ) AS BUDGET_SUB_GROUP_ID, IFNULL(ML.FD_INVESTMENT_TYPE_ID,0) AS FD_INVESTMENT_TYPE_ID,\n" +
                                  "       ML.DATE_CLOSED AS LEDGER_DATE_CLOSED, ML.CLOSED_BY\n" +
                                  " FROM MASTER_LEDGER ML\n" +
                                  " INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                                  "    ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                                  "   AND ML.IS_BRANCH_LEDGER = 0\n" +
                                  " INNER JOIN PROJECT_CATEGORY_LEDGER PCL\n" +
                                  "    ON PCL.LEDGER_ID = ML.LEDGER_ID\n" +
                                  " INNER JOIN MASTER_PROJECT_CATOGORY PC\n" +
                                  "    ON PC.PROJECT_CATOGORY_ID = PCL.PROJECT_CATEGORY_ID\n" +
                                  " INNER JOIN MASTER_PROJECT MP\n" +
                                  "    ON MP.PROJECT_CATEGORY_ID = PCL.PROJECT_CATEGORY_ID\n" +
                                  " INNER JOIN PROJECT_BRANCH PB\n" +
                                  "    ON PB.PROJECT_ID = MP.PROJECT_ID\n" +
                                  " INNER JOIN BRANCH_LOCATION BL\n" +
                                  "    ON BL.LOCATION_ID = PB.LOCATION_ID\n" +
                                  "LEFT JOIN BUDGET_GROUP BG ON BG.BUDGET_GROUP_ID = ML.BUDGET_GROUP_ID\n" +
                                  "LEFT JOIN BUDGET_SUB_GROUP BSG ON BSG.BUDGET_SUB_GROUP_ID = ML.BUDGET_SUB_GROUP_ID\n" +
                                  " WHERE PB.BRANCH_ID IN\n" +
                                  "       (SELECT BRANCH_OFFICE_ID\n" +
                                  "          FROM BRANCH_OFFICE\n" +
                                  "         WHERE BRANCH_OFFICE_CODE = ?BRANCH_OFFICE_CODE)\n" +
                                  " GROUP BY PCL.LEDGER_ID;";

                        //sQuery = "SELECT ML.LEDGER_CODE,\n" +
                        //            "       ML.LEDGER_NAME,\n" +
                        //            "       MLG.LEDGER_GROUP,\n" +
                        //            "       MLG.NATURE_ID,\n" +
                        //            "       MLG.GROUP_CODE,\n" +
                        //            "       ML.LEDGER_TYPE,\n" +
                        //            "       ML.LEDGER_SUB_TYPE,\n" +
                        //            "       ML.BANK_ACCOUNT_ID,\n" +
                        //            "       ML.IS_COST_CENTER,\n" +
                        //            "       ML.NOTES,\n" +
                        //            "       ML.IS_BANK_INTEREST_LEDGER,\n" +
                        //            "       ML.SORT_ID,\n" +
                        //            "       STATUS,\n" +
                        //            "       ML.ACCESS_FLAG,\n" +
                        //            "       ML.IS_TDS_LEDGER\n" +
                        //            "  FROM MASTER_LEDGER ML\n" +
                        //            " INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                        //            "    ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                        //    //  "   AND ML.GROUP_ID NOT IN (12, 13, 14)\n" +
                        //            "   AND ML.IS_BRANCH_LEDGER = 0 {INNER\n" +
                        //            "  JOIN PROJECT_CATEGORY_LEDGER PCL\n" +
                        //            "    ON PCL.LEDGER_ID = ML.LEDGER_ID\n" +
                        //            "   INNER JOIN MASTER_PROJECT MP \n" +
                        //            "    ON MP.PROJECT_CATEGORY_ID=PCL.PROJECT_CATEGORY_ID \n" +
                        //            " INNER JOIN PROJECT_BRANCH PB ON PB.PROJECT_ID=MP.PROJECT_ID \n" +
                        //            " WHERE PB.BRANCH_ID IN\n" +
                        //            "       (SELECT BRANCH_OFFICE_ID\n" +
                        //            "          FROM BRANCH_OFFICE\n" +
                        //            "         WHERE BRANCH_OFFICE_CODE = ?BRANCH_OFFICE_CODE)\n" +
                        //            " GROUP BY PCL.LEDGER_ID};";
                        break;
                    }

                case SQLCommand.ExportMasters.BudgetGroupFetchAll:
                    {
                        sQuery = "SELECT BUDGET_GROUP_ID, BUDGET_GROUP, BUDGET_GROUP_SORT_ID FROM BUDGET_GROUP";
                        break;
                    }
                case SQLCommand.ExportMasters.BudgetSubGroupFetchAll:
                    {
                        sQuery = "SELECT BUDGET_SUB_GROUP_ID, BUDGET_SUB_GROUP, BUDGET_SUB_GROUP_SORT_ID FROM BUDGET_SUB_GROUP";
                        break;
                    }
                case SQLCommand.ExportMasters.PurposeFetchAll:
                    {
                        sQuery = "SELECT CODE, FC_PURPOSE FROM master_contribution_head";
                        break;
                    }
                case SQLCommand.ExportMasters.GoverningMemberFetchAll:
                    {
                        sQuery = "SELECT MIS.SOCIETYNAME,\n" +
                                "       EC.EXECUTIVE,\n" +
                                "       EC.NAME,\n" +
                                "       EC.DATE_OF_BIRTH,\n" +
                                "       EC.RELIGION,\n" +
                                "       EC.ROLE,\n" +
                                "       EC.NATIONALITY,\n" +
                                "       EC.OCCUPATION,\n" +
                                "       EC.ASSOCIATION,\n" +
                                "       EC.OFFICE_BEARER,\n" +
                                "       EC.PLACE,\n" +
                                "       ST.STATE,\n" +
                                "       CY.COUNTRY,\n" +
                                "       EC.ADDRESS,\n" +
                                "       EC.PIN_CODE,\n" +
                                "       EC.PAN_SSN,\n" +
                                "       EC.PHONE,\n" +
                                "       EC.FAX,\n" +
                                "       EC.EMAIL,\n" +
                                "       EC.URL,\n" +
                                "       EC.DATE_OF_APPOINTMENT,\n" +
                                "       EC.DATE_OF_EXIT,\n" +
                                "       EC.IMAGES,\n" +
                                "       EC.NOTES\n" +
                                "  FROM MASTER_EXECUTIVE_COMMITTEE EC\n" +
                                " INNER JOIN EXECUTIVE_LEGAL_ENTITY EL\n" +
                                "    ON EC.EXECUTIVE_ID = EL.EXECUTIVE_ID\n" +
                                " INNER JOIN MASTER_INSTI_PERFERENCE MIS\n" +
                                "    ON MIS.CUSTOMERID = EL.CUSTOMERID\n" +
                                " INNER JOIN COUNTRY CY\n" +
                                "    ON CY.COUNTRY_ID = EC.COUNTRY_ID\n" +
                                " INNER JOIN STATE ST\n" +
                                "    ON ST.STATE_ID = EC.STATE_ID";
                        break;
                    }
                case SQLCommand.ExportMasters.TDSSectionFetchAll:
                    {
                        sQuery = "SELECT TDS_SECTION_ID,\n" +
                                "       CODE,\n" +
                                "       SECTION_NAME,\n" +
                                "       STATUS \n" +
                                "  FROM TDS_SECTION ORDER BY CODE ASC";

                        break;
                    }
                case SQLCommand.ExportMasters.TDSNatureOfPaymentsFetchAll:
                    {
                        sQuery = "SELECT TN.NATURE_PAY_ID,\n" +
                                "       TN.PAYMENT_CODE,\n" +
                                "       TN.NAME as PAYMENT_NAME,\n" +
                                "       TS.SECTION_NAME,\n" +
                                "       TN.DESCRIPTION AS NOTES,\n" +
                                "       TN.STATUS\n" +
                                "  FROM TDS_NATURE_PAYMENT TN\n" +
                                "  LEFT JOIN TDS_SECTION TS\n" +
                                "    ON TN.TDS_SECTION_ID = TS.TDS_SECTION_ID";
                        break;
                    }
                case SQLCommand.ExportMasters.TDSDeducteeTypesFetchAll:
                    {
                        sQuery = "   SELECT DEDUCTEE_TYPE_ID,NAME,\n" +
                                "           RESIDENTIAL_STATUS,\n" +
                                "           DEDUCTEE_TYPE,\n" +
                                "           STATUS\n" +
                                "  FROM TDS_DEDUCTEE_TYPE";
                        break;
                    }
                case SQLCommand.ExportMasters.TDSDutyTaxFetchAll:
                    {
                        sQuery = "SELECT TDS_DUTY_TAXTYPE_ID,\n" +
                               "       TAX_TYPE_NAME,\n" +
                               "       0 AS TDS_RATE,\n" +
                               "       0 AS TDS_EXEMPTION_LIMIT,\n" +
                               "       STATUS\n" +
                               "  FROM TDS_DUTY_TAXTYPE";
                        break;
                    }
                case SQLCommand.ExportMasters.TDSPolicyFetchAll:
                    {
                        sQuery = "SELECT TDS_POLICY_ID,\n" +
                                "       NP.NAME            AS PAYMENT_NAME,\n" +
                                "       DT.NAME            AS DEDUCTEE_TYPE,\n" +
                                "       TP.APPLICABLE_FROM\n" +
                                "  FROM TDS_POLICY TP\n" +
                                " INNER JOIN TDS_DEDUCTEE_TYPE DT\n" +
                                "    ON DT.DEDUCTEE_TYPE_ID = TP.TDS_DEDUCTEE_TYPE_ID\n" +
                                " INNER JOIN TDS_NATURE_PAYMENT NP\n" +
                                "    ON NP.NATURE_PAY_ID = TP.TDS_NATURE_PAYMENT_ID;";
                        break;
                    }
                case SQLCommand.ExportMasters.TDSPolicyDeducteesFetchAll:
                    {
                        sQuery = "SELECT TDS_POLICY_ID,\n" +
                                "       NP.NAME            AS PAYMENT_NAME,\n" +
                                "       DT.NAME            AS DEDUCTEE_TYPE,\n" +
                                "       TP.APPLICABLE_FROM\n" +
                                "  FROM TDS_POLICY TP\n" +
                                " INNER JOIN TDS_DEDUCTEE_TYPE DT\n" +
                                "    ON DT.DEDUCTEE_TYPE_ID = TP.TDS_DEDUCTEE_TYPE_ID\n" +
                                " INNER JOIN TDS_NATURE_PAYMENT NP\n" +
                                "    ON NP.NATURE_PAY_ID = TP.TDS_NATURE_PAYMENT_ID GROUP BY DT.NAME;";
                        break;
                    }
                case SQLCommand.ExportMasters.TDSTaxRateFetchAll:
                    {
                        sQuery = "SELECT TDS_POLICY_ID, TDS_RATE, TDS_EXEMPTION_LIMIT, TAX_TYPE_NAME\n" +
                                "  FROM TDS_TAX_RATE TR\n" +
                                " INNER JOIN TDS_DUTY_TAXTYPE DTT\n" +
                                "    ON DTT.TDS_DUTY_TAXTYPE_ID = TR.TDS_TAX_TYPE_ID;";
                        break;
                    }
                case SQLCommand.ExportMasters.FetchBudgetMasterByDateRange:
                    {
                        sQuery = "SELECT BM.BUDGET_ID, BM.BUDGET_NAME, BM.BUDGET_TYPE_ID, BM.DATE_FROM,BM.DATE_TO,\n" +
                                   "BM.IS_MONTH_WISE, BM.BUDGET_LEVEL_ID, IFNULL(BM.REMARKS,'') AS REMARKS,\n" +
                                   "GROUP_CONCAT(MP.PROJECT) AS PROJECT, GROUP_CONCAT(MP.PROJECT_ID) AS PROJECT_ID\n" +
                                   "FROM BUDGET_MASTER BM\n" +
                                   "INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID\n" +
                                   "INNER JOIN MASTER_PROJECT MP ON MP.PROJECT_ID = BP.PROJECT_ID\n" +
                                   "WHERE BM.IS_ACTIVE = 1 AND BM.BRANCH_ID = ?BRANCH_ID\n" +
                                   "AND IF(?BRANCH_ID=0, BUDGET_ACTION <= " + (int)BudgetAction.Approved + ", BUDGET_ACTION=" + (int)BudgetAction.Approved + ")\n" +
                                   " AND BP.PROJECT_ID IN (?PROJECT_ID) AND BM.DATE_FROM=?DATE_FROM AND BM.DATE_TO=?DATE_TO AND BM.BUDGET_TYPE_ID=?BUDGET_TYPE_ID";
                        break;
                    }
                case SQLCommand.ExportMasters.FetchBudgetProjectByDateRange:
                    {
                        sQuery = "SELECT BM.BUDGET_ID, MP.PROJECT_ID, MP.PROJECT\n" +
                                   "FROM BUDGET_MASTER BM\n" +
                                   "INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID\n" +
                                   "INNER JOIN MASTER_PROJECT MP ON MP.PROJECT_ID = BP.PROJECT_ID\n" +
                                   "WHERE BM.IS_ACTIVE = 1 AND BM.BRANCH_ID = ?BRANCH_ID\n" +
                                   "AND IF(?BRANCH_ID=0, BUDGET_ACTION <= " + (int)BudgetAction.Approved + ", BUDGET_ACTION=" + (int)BudgetAction.Approved + ")\n" +
                                   " AND BP.PROJECT_ID IN (?PROJECT_ID) AND BM.DATE_FROM=?DATE_FROM AND BM.DATE_TO=?DATE_TO AND BM.BUDGET_TYPE_ID=?BUDGET_TYPE_ID";
                        break;
                    }
                case SQLCommand.ExportMasters.FetchBudgetLedgerByDateRange:
                    {
                        sQuery = "SELECT BM.BUDGET_ID, ML.LEDGER_ID, ML.LEDGER_NAME, BL.PROPOSED_AMOUNT, BL.APPROVED_AMOUNT, BL.TRANS_MODE, NARRATION, IFNULL(HO_NARRATION,'') AS HO_NARRATION\n" +
                                   "FROM BUDGET_MASTER BM\n" +
                                   "INNER JOIN BUDGET_LEDGER BL ON BL.BUDGET_ID = BM.BUDGET_ID\n" +
                                   "INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = BL.LEDGER_ID\n" +
                                   "INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID\n" +
                                   "WHERE BM.IS_ACTIVE = 1 AND BM.BRANCH_ID = ?BRANCH_ID\n" +
                                   "AND IF(?BRANCH_ID=0, BUDGET_ACTION <= " + (int)BudgetAction.Approved + ", BUDGET_ACTION=" + (int)BudgetAction.Approved + ")\n" +
                                   " AND BP.PROJECT_ID IN (?PROJECT_ID) AND BM.DATE_FROM=?DATE_FROM AND BM.DATE_TO=?DATE_TO AND BM.BUDGET_TYPE_ID=?BUDGET_TYPE_ID\n" +
                                   "GROUP BY LEDGER_ID, BL.TRANS_MODE";
                        //"GROUP BY LEDGER_ID"; //On 23/03/2021, To get Asset and Liability ledgers too (CR and DR)
                        break;
                    }
                case SQLCommand.ExportMasters.FetchBudgetSubLedgerByDateRange:
                    {
                        sQuery = "SELECT BM.BUDGET_ID, ML.LEDGER_ID, MSL.SUB_LEDGER_ID, ML.LEDGER_NAME, MSL.SUB_LEDGER_NAME, \n" +
                                   "BSL.PROPOSED_AMOUNT, BSL.APPROVED_AMOUNT, BSL.TRANS_MODE, NARRATION, IFNULL(HO_NARRATION,'') AS HO_NARRATION\n" +
                                   "FROM BUDGET_MASTER BM\n" +
                                   "INNER JOIN BUDGET_SUB_LEDGER BSL ON BSL.BUDGET_ID = BM.BUDGET_ID\n" +
                                   "INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = BSL.LEDGER_ID\n" +
                                   "INNER JOIN MASTER_SUB_LEDGER MSL ON MSL.SUB_LEDGER_ID = BSL.SUB_LEDGER_ID\n" +
                                   "INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID\n" +
                                   "WHERE BM.IS_ACTIVE = 1 AND BM.BRANCH_ID = ?BRANCH_ID\n" +
                                   "AND IF(?BRANCH_ID=0, BUDGET_ACTION <= " + (int)BudgetAction.Approved + ", BUDGET_ACTION=" + (int)BudgetAction.Approved + ")\n" +
                                   " AND BP.PROJECT_ID IN (?PROJECT_ID) AND BM.DATE_FROM=?DATE_FROM AND BM.DATE_TO=?DATE_TO AND BM.BUDGET_TYPE_ID=?BUDGET_TYPE_ID";
                        //"GROUP BY SUB_LEDGER_ID";
                        break;
                    }
            }
            return sQuery;
        }
    }
}
