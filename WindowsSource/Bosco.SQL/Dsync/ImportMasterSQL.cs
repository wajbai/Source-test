using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class ImportMasterSQL : IDatabaseQuery
    {
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;
        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.ImportMaster).FullName)
            {
                query = GetMasterQuery();
            }

            sqlType = this.sqlType;
            return query;
        }

        #region Head Office
        private string GetMasterQuery()
        {
            string sQuery = string.Empty;
            SQLCommand.ImportMaster sqlCommandId = (SQLCommand.ImportMaster)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {

                case SQLCommand.ImportMaster.FetchHeadOfficeLedgers:
                    {
                        sQuery = "SELECT HEADOFFICE_LEDGER_ID,\n" +
                                "       LEDGER_CODE,\n" +
                                "       LEDGER_NAME AS HEADOFFICE_LEDGER_NAME,\n" +
                                "       LG.LEDGER_GROUP,\n" +
                                "       LEDGER_TYPE,\n" +
                                "       LEDGER_SUB_TYPE,\n" +
                                "       BANK_ACCOUNT_ID,\n" +
                                "       IS_COST_CENTER,\n" +
                                "       NOTES,\n" +
                                "       IS_BANK_INTEREST_LEDGER,\n" +
                                "       SORT_ID,\n" +
                                "       STATUS,\n" +
                                "       HL.ACCESS_FLAG\n" +
                                "  FROM master_headoffice_ledger HL\n" +
                            //" INNER JOIN MASTER_LEDGER_GROUP LG\n" +
                                " LEFT JOIN MASTER_LEDGER_GROUP LG\n" +
                                "    ON LG.GROUP_ID = HL.GROUP_ID";

                        break;
                    }
                case SQLCommand.ImportMaster.MapHeadOfficeLedger:
                    {
                        sQuery = "INSERT INTO HEADOFFICE_MAPPED_LEDGER( " +
                                 "LEDGER_ID, " +
                                 "HEADOFFICE_LEDGER_ID) " +
                                 "VALUES( " +
                                 "?LEDGER_ID, " +
                                 "?HEADOFFICE_LEDGER_ID) " +
                                 " ON DUPLICATE KEY UPDATE LEDGER_ID =?LEDGER_ID, HEADOFFICE_LEDGER_ID =?HEADOFFICE_LEDGER_ID"; ;
                        break;
                    }
                case SQLCommand.ImportMaster.MapHeadOfficeLedgetWithGenealateLedgers:
                    {
                        sQuery = "INSERT INTO PORTAL_CONGREGATION_LEDGER_MAP( " +
                               "CON_LEDGER_ID, " +
                               "LEDGER_ID) " +
                               "VALUES( " +
                               "?CON_LEDGER_ID, " +
                               "?HEADOFFICE_LEDGER_ID) " +
                               " ON DUPLICATE KEY UPDATE CON_LEDGER_ID =?CON_LEDGER_ID, LEDGER_ID =?HEADOFFICE_LEDGER_ID"; ;
                        break;
                    }
                case SQLCommand.ImportMaster.UpdateLedgerBudgetGroup:
                    {
                        sQuery = "UPDATE MASTER_LEDGER ML INNER JOIN (SELECT  ?LEDGER_ID LEDGER_ID, ML.BUDGET_GROUP_ID, ML.BUDGET_SUB_GROUP_ID FROM MASTER_HEADOFFICE_LEDGER MHL" +
                                    " INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_NAME = MHL.LEDGER_NAME" +
                                    " WHERE MHL.HEADOFFICE_LEDGER_ID = ?HEADOFFICE_LEDGER_ID AND (ML.BUDGET_GROUP_ID > 0 OR ML.BUDGET_SUB_GROUP_ID > 0)) BG" +
                                    " ON BG.LEDGER_ID  = ML.LEDGER_ID" +
                                    " SET ML.BUDGET_GROUP_ID= BG.BUDGET_GROUP_ID, ML.BUDGET_SUB_GROUP_ID=BG.BUDGET_SUB_GROUP_ID" +
                                    " WHERE ML.LEDGER_ID = ?LEDGER_ID";
                        break;
                    }
                case SQLCommand.ImportMaster.UpdateHeadOfficeLedger:
                    {
                        sQuery = "UPDATE MASTER_HEADOFFICE_LEDGER SET " +
                                 "  LEDGER_NAME=?LEDGER_NAME " +
                                 "WHERE LEDGER_NAME=?HEADOFFICE_LEDGER_NAME ";
                        break;
                    }
                case SQLCommand.ImportMaster.UpdateMasterLedger:
                    {
                        sQuery = "UPDATE MASTER_LEDGER SET " +
                                 "  LEDGER_NAME=?LEDGER_NAME " +
                                 "WHERE LEDGER_NAME=?HEADOFFICE_LEDGER_NAME ";
                        break;
                    }
                case SQLCommand.ImportMaster.CheckTransactionCount:
                    {
                        //sQuery = "SELECT COUNT(*) AS TRANS_COUNT\n" +
                        //            "  FROM VOUCHER_TRANS VT\n" +
                        //            " INNER JOIN HEADOFFICE_MAPPED_LEDGER HML\n" +
                        //            "    ON HML.LEDGER_ID = VT.LEDGER_ID\n" +
                        //            " WHERE HEADOFFICE_LEDGER_ID = ?HEADOFFICE_LEDGER_ID;";

                        sQuery = "SELECT COUNT(VT.VOUCHER_ID) AS TRANS_COUNT\n" +
                                "  FROM VOUCHER_TRANS VT\n" +
                                " INNER JOIN VOUCHER_MASTER_TRANS VMT\n" +
                                "    ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                " WHERE VMT.STATUS = 1\n" +
                                "   AND LEDGER_ID = ?LEDGER_ID";
                        break;
                    }
                case SQLCommand.ImportMaster.CheckLedgerBalance:
                    {
                        sQuery = "SELECT SUM(AMOUNT) AS AMOUNT FROM ledger_balance WHERE LEDGER_ID IN(?LEDGER_ID);";
                        break;
                    }
                case SQLCommand.ImportMaster.CheckProjectBudgetLedger:
                    {
                        sQuery = "SELECT COUNT(LEDGER_ID) AS TRANS_COUNT FROM project_budget_ledger WHERE LEDGER_ID IN(?LEDGER_ID);";
                        break;
                    }
                case SQLCommand.ImportMaster.CheckBudgetLedgerAmount:
                    {
                        sQuery = "SELECT SUM(AMOUNT) FROM budget_ledger WHERE LEDGER_ID=?LEDGER_ID;";
                        break;
                    }
                case SQLCommand.ImportMaster.FetchMappedLedgers:
                    {
                        sQuery = "SELECT * FROM HEADOFFICE_MAPPED_LEDGER WHERE HEADOFFICE_LEDGER_ID=?HEADOFFICE_LEDGER_ID";
                        break;
                    }
                case SQLCommand.ImportMaster.FetchLedgerIdByLedgerName:
                    {
                        sQuery = "SELECT LEDGER_ID FROM MASTER_LEDGER WHERE LEDGER_NAME=?LEDGER_NAME";
                        break;
                    }
                case SQLCommand.ImportMaster.IsLegalEntityExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM MASTER_INSTI_PERFERENCE\n" +
                                            " WHERE SOCIETYNAME=?SOCIETYNAME";
                        break;
                    }
                case SQLCommand.ImportMaster.IsExecutiveMembers:
                    {
                        sQuery = "SELECT COUNT(*) FROM MASTER_EXECUTIVE_COMMITTEE WHERE EXECUTIVE =?EXECUTIVE";
                        break;
                    }

                case SQLCommand.ImportMaster.IsProjectCatogoryExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM MASTER_PROJECT_CATOGORY\n" +
                                            " WHERE PROJECT_CATOGORY_NAME = ?PROJECT_CATOGORY_NAME";
                        break;
                    }

                case SQLCommand.ImportMaster.IsProjectExists:
                    {
                        sQuery = "SELECT COUNT(*) FROM MASTER_PROJECT WHERE  PROJECT = ?PROJECT";
                        break;
                    }
                //case SQLCommand.ImportMaster.IsProjectCodeExists:
                //    {
                //        sQuery = "SELECT COUNT(*) FROM MASTER_PROJECT WHERE PROJECT_CODE=?PROJECT_CODE {AND PROJECT_ID!=PROJECT_ID}";
                //        break;
                //    }
                case SQLCommand.ImportMaster.IsLedgerGroupExists:
                    {
                        sQuery = "SELECT COUNT(*) FROM MASTER_LEDGER_GROUP WHERE LEDGER_GROUP=?LEDGER_GROUP";
                        break;
                    }
                case SQLCommand.ImportMaster.IsMainGroupExist:
                    {
                        sQuery = "SELECT COUNT(*) FROM MASTER_LEDGER_GROUP WHERE MAIN_GROUP_ID=?MAIN_GROUP_ID";
                        break;
                    }
                case SQLCommand.ImportMaster.IsParentGroupExist:
                    {
                        sQuery = "SELECT COUNT(*) FROM MASTER_LEDGER_GROUP WHERE PARENT_GROUP_ID=?PARENT_GROUP_ID";
                        break;
                    }
                case SQLCommand.ImportMaster.IsNatureExist:
                    {
                        sQuery = "SELECT COUNT(*) FROM MASTER_LEDGER_GROUP WHERE NATURE_ID=?NATURE_ID";
                        break;
                    }
                //case SQLCommand.ImportMaster.IsGroupCodeExist:
                //    {
                //        sQuery = "SELECT COUNT(*) FROM MASTER_LEDGER_GROUP WHERE GROUP_CODE=?GROUP_CODE {AND GROUP_ID!=?GROUP_ID}";
                //        break;
                //    }
                case SQLCommand.ImportMaster.AddLedger:
                    {
                        sQuery = "INSERT INTO MASTER_LEDGER ( " +
                                    "LEDGER_CODE, " +
                                    "LEDGER_NAME, " +
                                    "GROUP_ID,  " +
                                    "LEDGER_TYPE, " +
                                    "LEDGER_SUB_TYPE, " +
                                    "BANK_ACCOUNT_ID, " +
                                    "IS_COST_CENTER,NOTES,SORT_ID,IS_BANK_INTEREST_LEDGER) " +
                                    "VALUES( " +
                                    "?LEDGER_CODE, " +
                                    "?LEDGER_NAME, " +
                                    "?GROUP_ID,  " +
                                    "?LEDGER_TYPE, " +
                                    "?LEDGER_SUB_TYPE, " +
                                    "?BANK_ACCOUNT_ID, " +
                                    "?IS_COST_CENTER,?NOTES,?SORT_ID,?IS_BANK_INTEREST_LEDGER) ";
                        break;
                    }
                case SQLCommand.ImportMaster.IsFCPurposeExists:
                    {
                        sQuery = "SELECT COUNT(*) FROM MASTER_CONTRIBUTION_HEAD WHERE FC_PURPOSE=?FC_PURPOSE";
                        break;
                    }


                //case SQLCommand.ImportMaster.IsFCPurposeCodeExists:
                //    {
                //        sQuery = "SELECT COUNT(*) FROM MASTER_CONTRIBUTION_HEAD WHERE CODE=?CODE {AND CONTRIBUTION_ID!=CONTRIBUTION_ID}";
                //        break;
                //    }
                case SQLCommand.ImportMaster.IsCountryExists:
                    {
                        sQuery = "SELECT COUNT(*) FROM MASTER_COUNTRY WHERE COUNTRY=?COUNTRY";
                        break;
                    }
                case SQLCommand.ImportMaster.IsBranchOfficeExist:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM BRANCH_OFFICE\n" +
                                            " WHERE BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE";
                        break;
                    }
                case SQLCommand.ImportMaster.IsTDSSectionExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM TDS_SECTION\n" +
                                            " WHERE SECTION_NAME=?SECTION_NAME";
                        break;
                    }
                case SQLCommand.ImportMaster.IsTDSNatureOfPaymentExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM TDS_NATURE_PAYMENT\n" +
                                            " WHERE NAME=?PAYMENT_NAME";
                        break;
                    }
                case SQLCommand.ImportMaster.IsTDSDeducteeTypeExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM TDS_DEDUCTEE_TYPE\n" +
                                            " WHERE NAME=?NAME";
                        break;
                    }
                case SQLCommand.ImportMaster.IsDutyTaxExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM TDS_DUTY_TAXTYPE\n" +
                                            " WHERE TAX_TYPE_NAME=?TAX_TYPE_NAME";
                        break;
                    }
                case SQLCommand.ImportMaster.IsLedgerExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                          "  FROM MASTER_LEDGER\n" +
                                          " WHERE LEDGER_NAME =?LEDGER_NAME";
                        break;
                    }
                case SQLCommand.ImportMaster.IsHeadOfficeLedgerExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                          "  FROM MASTER_HEADOFFICE_LEDGER\n" +
                                          " WHERE LEDGER_NAME =?HEADOFFICE_LEDGER_NAME";
                        break;
                    }
                case SQLCommand.ImportMaster.IsGeneralateLedgerExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                         "  FROM PORTAL_CONGREGATION_LEDGER\n" +
                                         " WHERE CON_LEDGER_NAME =?CON_LEDGER_NAME";
                        break;
                    }
                //case SQLCommand.ImportMaster.IsHeadOfficeCodeExists:
                //    {
                //        sQuery = "SELECT COUNT(*)\n" +
                //                        "  FROM MASTER_HEADOFFICE_LEDGER\n" +
                //                        " WHERE LEDGER_CODE =?LEDGER_CODE {AND HEADOFFICE_LEDGER_ID!=?LEDGER_ID}";
                //        break;
                //    }
                //case SQLCommand.ImportMaster.IsLedgerCodeExists:
                //    {
                //        sQuery = "SELECT COUNT(*)\n" +
                //                          "  FROM MASTER_LEDGER\n" +
                //                          " WHERE LEDGER_CODE =?LEDGER_CODE {AND LEDGER_ID!=?LEDGER_ID}";
                //        break;
                //    }
                case SQLCommand.ImportMaster.GetTDSSectionId:
                    {
                        sQuery = "SELECT TDS_SECTION_ID FROM TDS_SECTION WHERE SECTION_NAME=?SECTION_NAME";
                        break;
                    }
                case SQLCommand.ImportMaster.GetTDSNatureOfPaymentId:
                    {
                        sQuery = "SELECT NATURE_PAY_ID FROM TDS_NATURE_PAYMENT WHERE NAME=?PAYMENT_NAME";
                        break;
                    }
                case SQLCommand.ImportMaster.GetTDSDeducteeTypeId:
                    {
                        sQuery = "SELECT DEDUCTEE_TYPE_ID FROM TDS_DEDUCTEE_TYPE WHERE NAME=?NAME";
                        break;
                    }
                case SQLCommand.ImportMaster.GetDutyTaxId:
                    {
                        sQuery = "SELECT TDS_DUTY_TAXTYPE_ID FROM TDS_DUTY_TAXTYPE WHERE TAX_TYPE_NAME=?TAX_TYPE_NAME";
                        break;
                    }
                case SQLCommand.ImportMaster.GetLegalEntityId:
                    {
                        sQuery = "SELECT CUSTOMERID FROM MASTER_INSTI_PERFERENCE WHERE  SOCIETYNAME=?SOCIETYNAME";
                        break;
                    }
                case SQLCommand.ImportMaster.GetExecutiveMemberId:
                    {
                        sQuery = "SELECT EXECUTIVE_ID FROM MASTER_EXECUTIVE_COMMITTEE WHERE EXECUTIVE =?EXECUTIVE";
                        break;
                    }
                case SQLCommand.ImportMaster.GetProjectCategoryId:
                    {
                        sQuery = "SELECT PROJECT_CATOGORY_ID FROM MASTER_PROJECT_CATOGORY WHERE PROJECT_CATOGORY_NAME=?PROJECT_CATOGORY_NAME";
                        break;
                    }
                case SQLCommand.ImportMaster.GetProjectCategoryITRId:
                    {
                        sQuery = "SELECT PROJECT_CATOGORY_ITRGROUP_ID FROM MASTER_PROJECT_CATOGORY_ITRGROUP WHERE PROJECT_CATOGORY_ITRGROUP =?PROJECT_CATOGORY_ITRGROUP";
                        break;
                    }
                case SQLCommand.ImportMaster.GetProjectId:
                    {
                        sQuery = "SELECT PROJECT_ID FROM MASTER_PROJECT WHERE PROJECT=?PROJECT";
                        break;
                    }
                case SQLCommand.ImportMaster.GetLedgerGroupId:
                    {
                        sQuery = "SELECT GROUP_ID FROM MASTER_LEDGER_GROUP WHERE LEDGER_GROUP=?LEDGER_GROUP";
                        break;
                    }
                case SQLCommand.ImportMaster.GetParentId:
                    {
                        sQuery = "SELECT GROUP_ID FROM MASTER_LEDGER_GROUP WHERE LEDGER_GROUP=?LEDGER_GROUP";
                        break;
                    }
                case SQLCommand.ImportMaster.GetNatureId:
                    {
                        sQuery = "SELECT NATURE_ID FROM MASTER_NATURE WHERE NATURE=?NATURE";
                        break;
                    }
                case SQLCommand.ImportMaster.GetMainParentId:
                    {
                        sQuery = "SELECT GROUP_ID FROM MASTER_LEDGER_GROUP WHERE LEDGER_GROUP=?LEDGER_GROUP";
                        break;
                    }
                case SQLCommand.ImportMaster.GetLedgerId:
                    {
                        sQuery = "SELECT LEDGER_ID FROM MASTER_LEDGER WHERE LEDGER_NAME=?LEDGER_NAME";
                        break;
                    }
                case SQLCommand.ImportMaster.GetGeneralateId:
                    {
                        sQuery = "SELECT CON_LEDGER_ID FROM PORTAL_CONGREGATION_LEDGER WHERE CON_LEDGER_NAME=?CON_LEDGER_NAME";
                        break;
                    }
                case SQLCommand.ImportMaster.GetGeneralateParentId:
                    {
                        sQuery = "SELECT CON_LEDGER_ID FROM PORTAL_CONGREGATION_LEDGER WHERE CON_LEDGER_NAME=?CON_LEDGER_NAME";
                        break;
                    }
                case SQLCommand.ImportMaster.GetGeneralateMainParentId:
                    {
                        sQuery = "SELECT CON_LEDGER_ID FROM PORTAL_CONGREGATION_LEDGER WHERE CON_LEDGER_NAME=?CON_LEDGER_NAME";
                        break;
                    }
                case SQLCommand.ImportMaster.GetBudgetGroupId:
                    {
                        sQuery = "SELECT BUDGET_GROUP_ID FROM BUDGET_GROUP WHERE BUDGET_GROUP=?BUDGET_GROUP";
                        break;
                    }
                case SQLCommand.ImportMaster.GetBudgetSubGroupId:
                    {
                        sQuery = "SELECT BUDGET_SUB_GROUP_ID FROM BUDGET_SUB_GROUP WHERE BUDGET_SUB_GROUP=?BUDGET_SUB_GROUP";
                        break;
                    }
                case SQLCommand.ImportMaster.GetHeadOfficeLedgerId:
                    {
                        sQuery = "SELECT HEADOFFICE_LEDGER_ID AS LEDGER_ID FROM MASTER_HEADOFFICE_LEDGER WHERE LEDGER_NAME=?LEDGER_NAME";
                        break;
                    }
                case SQLCommand.ImportMaster.GetFCPurposeId:
                    {
                        sQuery = "SELECT CONTRIBUTION_ID FROM MASTER_CONTRIBUTION_HEAD WHERE FC_PURPOSE=?FC_PURPOSE";
                        break;
                    }
                case SQLCommand.ImportMaster.GetCountryId:
                    {
                        sQuery = "SELECT COUNTRY_ID FROM MASTER_COUNTRY WHERE COUNTRY=?COUNTRY";
                        break;
                    }
                case SQLCommand.ImportMaster.GetStateId:
                    {
                        sQuery = "SELECT STATE_ID FROM MASTER_STATE WHERE STATE_NAME=?STATE_NAME";
                        break;
                    }
                case SQLCommand.ImportMaster.FetchLegalEntity:
                    {
                        sQuery = @"INSTITUTENAME, SOCIETYNAME, CONTACTPERSON, ADDRESS, PLACE, STATE, COUNTRY, PINCODE, PHONE, FAX, EMAIL, URL, REGNO, REGDATE, PERMISSIONNO,
                                   PERMISSIONDATE, A12NO, PANNO, GIRNO, TANNO, ASSOCIATIONNATURE, DENOMINATION FROM MASTER_INSTI_PERFERENCE";
                        break;
                    }
                case SQLCommand.ImportMaster.FetchProjectCategory:
                    {
                        sQuery = "SELECT PROJECT_CATOGORY_NAME FROM MASTER_PROJECT_CATOGORY";
                        break;
                    }

                case SQLCommand.ImportMaster.MapLedgers:
                    {
                        sQuery = "INSERT INTO PROJECT_LEDGER(PROJECT_ID,LEDGER_ID)VALUES(?PROJECT_ID,?LEDGER_ID) ON DUPLICATE KEY UPDATE PROJECT_ID=?PROJECT_ID,LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.ImportMaster.MapVouchers:
                    {
                        sQuery = "INSERT INTO PROJECT_VOUCHER(PROJECT_ID,VOUCHER_ID)VALUES(?PROJECT_ID,?VOUCHER_ID)";
                        break;
                    }
                case SQLCommand.ImportMaster.DeleteHeadOfficeMappedLedger:
                    {
                        sQuery = "DELETE FROM HEADOFFICE_MAPPED_LEDGER WHERE HEADOFFICE_LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.ImportMaster.DeleteHeadOfficewithGeneralateMappedLedgers:
                    {
                        sQuery = "DELETE FROM PORTAL_CONGREGATION_LEDGER_MAP";
                        break;
                    }
                case SQLCommand.ImportMaster.DeleteHeadOfficeLedger:
                    {
                        sQuery = "DELETE FROM MASTER_HEADOFFICE_LEDGER WHERE HEADOFFICE_LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.ImportMaster.DeleteMasterLedger:
                    {
                        sQuery = "DELETE FROM MASTER_LEDGER WHERE LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.ImportMaster.DeleteProjectMappedLedger:
                    {
                        sQuery = "DELETE FROM PROJECT_LEDGER WHERE LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.ImportMaster.DeleteLedgerBalance:
                    {
                        sQuery = "DELETE FROM LEDGER_BALANCE WHERE LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.ImportMaster.DeleteBudgetLedger:
                    {
                        sQuery = "DELETE FROM BUDGET_LEDGER WHERE LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.ImportMaster.DeleteProjectBudgetMappedLedger:
                    {
                        sQuery = "DELETE FROM PROJECT_BUDGET_LEDGER WHERE LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.ImportMaster.DeleteTDSCreditorProfile:
                    {
                        sQuery = "DELETE FROM tds_credtiors_profile WHERE LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.ImportMaster.FetchBranchOfficeProjects:
                    {
                        sQuery = "SELECT PROJECT_ID,PROJECT as PROJECT_NAME FROM MASTER_PROJECT WHERE DELETE_FLAG=0";
                        break;
                    }
                case SQLCommand.ImportMaster.FetchHeadOfficeLedgerCode:
                    {
                        sQuery = "SELECT LEDGER_CODE FROM MASTER_HEADOFFICE_LEDGER {WHERE HEADOFFICE_LEDGER_ID!=?LEDGER_ID} ORDER BY HEADOFFICE_LEDGER_ID DESC";
                        break;
                    }
                case SQLCommand.ImportMaster.FetchVoucherIdByLedgerId:
                    {
                        sQuery = "SELECT GROUP_CONCAT(VT.VOUCHER_ID) AS VOUCHER_ID\n" +
                                "  FROM VOUCHER_TRANS VT\n" +
                                " INNER JOIN VOUCHER_MASTER_TRANS VMT\n" +
                                "    ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                " WHERE VMT.STATUS = 0\n" +
                                "   AND LEDGER_ID =?LEDGER_ID;";
                        break;
                    }
                case SQLCommand.ImportMaster.DeleteVoucherCostCenter:
                    {
                        sQuery = "DELETE FROM VOUCHER_CC_TRANS WHERE VOUCHER_ID IN (?VOUCHER_ID);";
                        break;
                    }
                case SQLCommand.ImportMaster.DeleteVoucherTrans:
                    {
                        sQuery = "DELETE FROM VOUCHER_TRANS WHERE VOUCHER_ID IN(?VOUCHER_ID);";
                        break;
                    }
                case SQLCommand.ImportMaster.DeleteVoucherMasterTrans:
                    {
                        sQuery = "DELETE FROM VOUCHER_MASTER_TRANS WHERE VOUCHER_ID IN(?VOUCHER_ID)";
                        break;
                    }
                case SQLCommand.ImportMaster.MapCategoryLedgers:
                    {
                        sQuery = "INSERT INTO PROJECT_CATEGORY_LEDGER(PROJECT_CATEGORY_ID,LEDGER_ID)\n" +
                                "    VALUES(?PROJECT_CATOGORY_ID,?LEDGER_ID)\n" +
                                "   ON DUPLICATE KEY UPDATE PROJECT_CATEGORY_ID =?PROJECT_CATOGORY_ID, LEDGER_ID =?LEDGER_ID;";
                        break;
                    }
                case SQLCommand.ImportMaster.DeleteAllCategoryLedgers:
                    {
                        sQuery = "DELETE FROM PROJECT_CATEGORY_LEDGER";
                        break;
                    }
                case SQLCommand.ImportMaster.DeleteProjectCategoryByLedgerId:
                    {
                        sQuery = "DELETE FROM PROJECT_CATEGORY_LEDGER WHERE LEDGER_ID = ?LEDGER_ID";
                        break;
                    }

                case SQLCommand.ImportMaster.DeleteProjectCategoryByLedgerIdCollection:
                    {
                        sQuery = "DELETE FROM PROJECT_CATEGORY_LEDGER WHERE LEDGER_ID IN(?ID_COLLECTIONS)";
                        break;
                    }

                case SQLCommand.ImportMaster.DeleteFDLedger:
                    {
                        sQuery = "DELETE FROM FD_ACCOUNT WHERE LEDGER_ID =?LEDGER_ID";
                        break;
                    }
                case SQLCommand.ImportMaster.DeleteBankAccountByLedger:
                    {
                        //On 23/08/2017, to remove bank account by giving ledger id (when we remove ledger, we should remove its bankaccount if ledgers linked with Bank account
                        sQuery = "DELETE FROM MASTER_BANK_ACCOUNT WHERE LEDGER_ID =?LEDGER_ID";
                        break;
                    }
                case SQLCommand.ImportMaster.DeleteLedgerOtherMappedDetails:
                    {
                        sQuery = @"DELETE FROM PROJECT_COSTCENTRE WHERE LEDGER_ID IN (?LEDGER_ID);
                                    DELETE FROM PROJECT_LEDGER_APPLICABLE WHERE LEDGER_ID IN (?LEDGER_ID);
                                    DELETE FROM PORTAL_CONGREGATION_LEDGER_MAP WHERE LEDGER_ID IN (?LEDGER_ID);
                                    DELETE FROM PROJECT_CATEGORY_LEDGER WHERE LEDGER_ID IN (?LEDGER_ID);
                                    DELETE FROM BRANCH_CONGREGATION_LEDGER_MAP WHERE LEDGER_ID IN (?LEDGER_ID);
                                    DELETE FROM BRANCH_CONGREGATION_FIXEDASSET_DETAILS WHERE LEDGER_ID IN (?LEDGER_ID);";
                        break;
                    }
                case SQLCommand.ImportMaster.DeleteUnusedAllMappedLedgersByProject:
                    {
                        sQuery = "DELETE FROM PROJECT_LEDGER WHERE PROJECT_ID = ?PROJECT_ID AND LEDGER_ID NOT IN (\n" +
                                    "SELECT DISTINCT LEDGER_ID FROM VOUCHER_TRANS VT \n" +
                                    "  INNER JOIN VOUCHER_MASTER_TRANS VMT ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                    "  WHERE VMT.PROJECT_ID = ?PROJECT_ID AND VMT.STATUS = 1\n" +
                                    "UNION ALL\n" +
                                    "SELECT DISTINCT LEDGER_ID FROM LEDGER_BALANCE LB WHERE LB.PROJECT_ID = ?PROJECT_ID AND LB.TRANS_FLAG = 'OP' AND AMOUNT > 0\n" +
                                    "UNION ALL\n" +
                                    "SELECT DISTINCT LEDGER_ID FROM FD_ACCOUNT FD WHERE FD.PROJECT_ID = ?PROJECT_ID AND FD.STATUS= 1\n" +
                                    "UNION ALL\n" +
                                    "SELECT DISTINCT INTEREST_LEDGER_ID AS LEDGER_ID FROM FD_RENEWAL FDR\n" +
                                    "  WHERE FDR.FD_ACCOUNT_ID IN (SELECT FD_ACCOUNT_ID FROM FD_ACCOUNT FD WHERE FD.PROJECT_ID = ?PROJECT_ID AND FD.STATUS= 1)\n" +
                                    "UNION ALL\n" +
                                    "SELECT DISTINCT LEDGER_ID FROM BUDGET_LEDGER BL\n" +
                                    "    INNER JOIN BUDGET_MASTER BM ON BM.BUDGET_ID = BL.BUDGET_ID\n" +
                                    "    INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID AND BP.BUDGET_ID = BL.BUDGET_ID\n" +
                                    "    WHERE BP.PROJECT_ID = ?PROJECT_ID\n" +
                                    "UNION ALL\n" +
                                    "SELECT DISTINCT LEDGER_ID FROM BUDGET_SUB_LEDGER BSL\n" +
                                    "    INNER JOIN BUDGET_MASTER BM ON BM.BUDGET_ID = BSL.BUDGET_ID\n" +
                                    "    INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID AND BP.BUDGET_ID = BSL.BUDGET_ID\n" +
                                    "    WHERE BP.PROJECT_ID = ?PROJECT_ID\n" +
                                    "UNION ALL\n" +
                                    "SELECT LEDGER_ID FROM MASTER_LEDGER ML WHERE ML.GROUP_ID IN (12, 13, 14) OR ML.ACCESS_FLAG = 2\n" +
                                ")";
                        break;
                    }
                case SQLCommand.ImportMaster.UpdateLocalHeadofficeLedgerFlag:
                    {
                        sQuery = "UPDATE MASTER_LEDGER ML\n" +
                                     " LEFT JOIN headoffice_mapped_ledger hml on ml.ledger_id = hml.ledger_id\n" +
                                     " LEFT JOIN MASTER_HEADOFFICE_LEDGER MHL ON mhl.headoffice_ledger_id = hml.headoffice_ledger_id\n" +
                                     " set ML.IS_BRANCH_LEDGER = 1 where IFNULL(ml.LEDGER_NAME,'') <> IFNULL(mhl.LEDGER_NAME,'') and ml.access_flag <>2 and ml.group_id not in (12,13)";
                        break;
                    }
                case SQLCommand.ImportMaster.FetchBranchLedgerLocalDatabase:
                    {
                        sQuery = " SELECT LEDGER_ID, LEDGER_NAME,ACCESS_FLAG,IS_BRANCH_LEDGER,GROUP_ID FROM MASTER_LEDGER ML WHERE ML.IS_BRANCH_LEDGER=1 \n" +
                                " AND ML.GROUP_ID NOT IN (12, 13) AND ML.ACCESS_FLAG <>2";
                        break;
                    }
                case SQLCommand.ImportMaster.UpdateParentGroupMainGroupwithGroupId:
                    {
                        sQuery = " UPDATE MASTER_LEDGER_GROUP LG\n" +
                           " LEFT JOIN MASTER_LEDGER_GROUP PG ON PG.GROUP_ID = LG.PARENT_GROUP_ID\n" +
                           " LEFT JOIN MASTER_LEDGER_GROUP PG1 ON PG1.GROUP_ID = LG.MAIN_GROUP_ID\n" +
                           " SET LG.PARENT_GROUP_ID = CASE\n" +
                            "    WHEN LG.NATURE_ID = 1 THEN 5 \n" +
                            "    WHEN LG.NATURE_ID = 2 THEN 8 \n" +
                            "    WHEN LG.NATURE_ID = 3 THEN 18 \n" +
                            "    WHEN LG.NATURE_ID = 4 THEN 23 \n" +
                            "    ELSE LG.PARENT_GROUP_ID END, \n" +
                            "    LG.MAIN_GROUP_ID = CASE\n" +
                            "    WHEN LG.NATURE_ID = 1 THEN 5 \n" +
                            "    WHEN LG.NATURE_ID = 2 THEN 8 \n" +
                            "    WHEN LG.NATURE_ID = 3 THEN 18 \n" +
                            "    WHEN LG.NATURE_ID = 4 THEN 23 \n" +
                            "    ELSE LG.MAIN_GROUP_ID END\n" +
                            " WHERE (PG.PARENT_GROUP_ID IS NULL OR PG1.MAIN_GROUP_ID IS NULL)";
                        break;
                    }
                case SQLCommand.ImportMaster.DeleteLedgerGroupNotExistPortal:
                    {
                        sQuery = "DELETE LG.* FROM MASTER_LEDGER_GROUP LG \n" +
                        " LEFT JOIN master_headoffice_ledger mhl ON LG.GROUP_ID = mhl.GROUP_ID\n" +
                        " LEFT JOIN master_ledger ml ON ml.GROUP_ID = lg.GROUP_ID\n" +
                        " where mhl.GROUP_ID is null and ml.group_id is null and lg.access_flag <> 2  and LG.GROUP_ID NOT IN (12, 13, 14) AND LG.GROUP_ID >31";
                        break;
                    }
                case SQLCommand.ImportMaster.DeleteUnusedAllBudgetMappedLedgersByProject:
                    {
                        sQuery = "DELETE FROM PROJECT_BUDGET_LEDGER WHERE PROJECT_ID = ?PROJECT_ID AND LEDGER_ID NOT IN (\n" +
                                    "SELECT DISTINCT LEDGER_ID FROM BUDGET_LEDGER BL\n" +
                                    "    INNER JOIN BUDGET_MASTER BM ON BM.BUDGET_ID = BL.BUDGET_ID\n" +
                                    "    INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID AND BP.BUDGET_ID = BL.BUDGET_ID\n" +
                                    "    WHERE BP.PROJECT_ID = ?PROJECT_ID\n" +
                                    "UNION ALL\n" +
                                    "SELECT DISTINCT LEDGER_ID FROM BUDGET_SUB_LEDGER BSL\n" +
                                    "    INNER JOIN BUDGET_MASTER BM ON BM.BUDGET_ID = BSL.BUDGET_ID\n" +
                                    "    INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID AND BP.BUDGET_ID = BSL.BUDGET_ID\n" +
                                    "    WHERE BP.PROJECT_ID = ?PROJECT_ID\n" +
                                    "UNION ALL\n" +
                                    "SELECT LEDGER_ID FROM MASTER_LEDGER ML WHERE ML.GROUP_ID IN (12, 13, 14) OR ML.ACCESS_FLAG = 2\n" +
                                ")";
                        break;
                    }
            }
            return sQuery;
        }
        #endregion
    }
}
