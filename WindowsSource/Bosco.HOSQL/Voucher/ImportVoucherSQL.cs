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
    public class ImportVoucherSQL : IDatabaseQuery
    {
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;
        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.ImportVoucher).FullName)
            {
                query = GetImportVoucherQuery();
            }

            sqlType = this.sqlType;
            return query;
        }

        private string GetImportVoucherQuery()
        {
            string sQuery = string.Empty;
            SQLCommand.ImportVoucher sqlCommandId = (SQLCommand.ImportVoucher)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                case SQLCommand.ImportVoucher.AuthenticateBranchCode:
                    {
                        sQuery = "SELECT BRANCH_OFFICE_ID,\n" +
                                    "       BRANCH_OFFICE_CODE,\n" +
                                    "       BRANCH_OFFICE_NAME,\n" +
                                    "       HEAD_OFFICE_CODE,\n" +
                                    "       CREATED_DATE,\n" +
                                    "       CREATED_BY,\n" +
                                    "       DEPLOYMENT_TYPE,\n" +
                                    "       ADDRESS,\n" +
                                    "       STATE_ID,\n" +
                                    "       PINCODE,\n" +
                                    "       COUNTRY_ID,\n" +
                                    "       PHONE_NO,\n" +
                                    "       MOBILE_NO,\n" +
                                    "       BRANCH_EMAIL_ID,\n" +
                                    "       STATUS,\n" +
                                    "       MODIFIED_DATE,\n" +
                                    "       MODIFIED_BY,\n" +
                                    "       USER_CREATED_STATUS,\n" +
                                    "       CITY,\n" +
                                    "       BRANCH_PART_CODE,\n" +
                                    "       COUNTRY_CODE,\n" +
                                    "       BRANCH_KEY_CODE,\n" +
                                    "       IS_SUBBRANCH\n" +
                            //      "       ASSOCIATE_BRANCH_CODE,\n" +
                            //       "       INCHARGE_NAME\n" +
                                    "  FROM BRANCH_OFFICE\n" +
                                    " WHERE BRANCH_OFFICE_CODE = ?BRANCH_OFFICE_CODE;";
                        break;
                    }
                case SQLCommand.ImportVoucher.AuthenticateHeadOfficeCode:
                    {
                        sQuery = "SELECT HEAD_OFFICE_ID, HEAD_OFFICE_CODE FROM HEAD_OFFICE WHERE HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE";
                        break;
                    }
                case SQLCommand.ImportVoucher.FetchLatestLicense:
                    {
                        sQuery = "SELECT HO.HEAD_OFFICE_NAME,IF(BO.IS_SUBBRANCH=1,BO.ASSOCIATE_BRANCH_CODE, " +
                                 "BO.HEAD_OFFICE_CODE) AS   HEAD_OFFICE_CODE,BO.BRANCH_OFFICE_CODE,BO.BRANCH_OFFICE_NAME, " +
                                 "CASE WHEN BO.DEPLOYMENT_TYPE=0 THEN 'Standalone' ELSE 'Client/Server' END AS DEPLOYMENT_TYPE, " +
                                 "BO.ADDRESS,C.COUNTRY,S.STATE,BO.PINCODE, " +
                                 "BO.PHONE_NO  AS PHONE,CONCAT(BO.COUNTRY_CODE,'',BO.MOBILE_NO) AS MOBILE_NO, " +
                                 "BO.BRANCH_EMAIL_ID  AS EMAIL,BO.CITY AS PLACE,LC.LICENSE_KEY_NUMBER,CAST(LC.LICENSE_QUANTITY AS CHAR) AS NoOfNodes, " +
                                 "CAST(LC.LICENSE_COST AS CHAR) AS LICENSE_COST,CAST(LC.YEAR_FROM AS CHAR) AS YEAR_FROM, " +
                                 "CAST(LC.YEAR_TO AS CHAR) AS YEAR_TO,LC.INSTITUTE_NAME AS InstituteName,'' AS SocietyName, " +
                                 "CAST(LC.IS_MULTILOCATION AS CHAR) AS IS_MULTILOCATION,CAST(LC.ENABLE_PORTAL AS CHAR) AS ENABLE_PORTAL, " +
                                 "CAST(LC.KEY_GENERATED_DATE AS CHAR) AS KEY_GENERATED_DATE, " +
                                 "LC.MODULE_ITEM AS NoOfModules,LC.LOGIN_URL AS URL, " +
                                  "LC.BRANCH_KEY_CODE,CAST(LC.IS_LICENSE_MODEL AS CHAR) AS IS_LICENSE_MODEL, " +
                                 "CAST(LC.ACCESS_MULTI_DB AS CHAR) AS AccessToMultiDB, " +
                                  "'' AS FAX,'Primary' AS LOCATION, " +
                                  "'' AS CONTACTPERSON, CAST(LC.LOCK_MASTER AS CHAR) AS LOCK_MASTER, CAST(LC.MAP_LEDGER AS CHAR) AS MAP_LEDGER " +
                                 "FROM BRANCH_LICENSE LC " +
                                 "INNER JOIN BRANCH_OFFICE BO " +
                                 "ON LC.BRANCH_ID=BO.BRANCH_OFFICE_ID " +
                                 "INNER JOIN HEAD_OFFICE HO " +
                                 "ON HO.HEAD_OFFICE_CODE=BO.HEAD_OFFICE_CODE " +
                                 "INNER JOIN COUNTRY C " +
                                 "ON C.COUNTRY_ID=BO.COUNTRY_ID " +
                                 "INNER JOIN STATE S " +
                                 "ON S.STATE_ID=BO.STATE_ID " +
                                 "WHERE BO.BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE ORDER BY LC.LICENSE_ID DESC LIMIT 1";
                        break;
                    }
                case SQLCommand.ImportVoucher.FetchDataBase:
                    {
                        sQuery = "SELECT HEAD_OFFICE_ID, HEAD_OFFICE_CODE, " +
                                "HEAD_OFFICE_NAME, HOST_NAME, DB_NAME, USERNAME, PASSWORD, " +
                                "CONCAT('server=', HOST_NAME, ';database=', DB_NAME, " +
                                "';uid=', USERNAME, ';pwd=', PASSWORD, '; Connect Timeout=600; pooling=false') AS DB_CONNECTION " +
                                "FROM HEAD_OFFICE " +
                                "WHERE HEAD_OFFICE_CODE = ?HEAD_OFFICE_CODE";
                        break;
                    }
                case SQLCommand.ImportVoucher.FetchBranchOfficeId:
                    {
                        sQuery = "SELECT BRANCH_OFFICE_ID,\n" +
                                            "       BRANCH_OFFICE_CODE,\n" +
                                            "       BRANCH_OFFICE_NAME,\n" +
                                            "       HEAD_OFFICE_CODE,\n" +
                                            "       CREATED_DATE\n" +
                                            "  FROM BRANCH_OFFICE\n" +
                                            " WHERE BRANCH_OFFICE_CODE =\n" +
                                            "?BRANCH_OFFICE_CODE";

                        break;
                    }
                case SQLCommand.ImportVoucher.FetchLocationId:
                    {
                        sQuery = "SELECT LOCATION_ID FROM BRANCH_LOCATION WHERE LOCATION_NAME=?LOCATION AND BRANCH_ID=?BRANCH_OFFICE_ID";
                        break;
                    }

                case SQLCommand.ImportVoucher.FetchTransactions:
                    {
                        sQuery = "SELECT VOUCHER_DATE, VOUCHER_ID, PROJECT_ID\n" +
                                    "  FROM VOUCHER_MASTER_TRANS\n" +
                                    " WHERE BRANCH_ID=?BRANCH_OFFICE_ID " +
                                    " AND VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO " +
                                    " AND PROJECT_ID IN (?PROJECT_ID) " +
                                    " AND VOUCHER_SUB_TYPE <> 'FD' " +
                                    " AND STATUS=1" +
                                    " AND LOCATION_ID=?LOCATION_ID";
                        break;
                    }
                case SQLCommand.ImportVoucher.InsertVoucherMaster:
                    {
                        sQuery = "INSERT INTO VOUCHER_MASTER_TRANS ( " +
                               "VOUCHER_ID, " +
                               "VOUCHER_DATE, " +
                               "PROJECT_ID, " +
                               "VOUCHER_NO, " +
                               "VOUCHER_TYPE," +
                               "VOUCHER_SUB_TYPE, " +
                               "DONOR_ID," +
                               "PURPOSE_ID," +
                               "CONTRIBUTION_TYPE," +
                               "CONTRIBUTION_AMOUNT," +
                               "CURRENCY_COUNTRY_ID," +
                               "EXCHANGE_RATE," +
                               "CALCULATED_AMOUNT," +
                               "ACTUAL_AMOUNT," +
                               "CLIENT_CODE,\n" +
                               "CLIENT_REFERENCE_ID,\n" +
                               "EXCHANGE_COUNTRY_ID," +
                               "NARRATION," +
                               "CREATED_ON," +
                               "CREATED_BY," +
                               "CREATED_BY_NAME," +
                               "MODIFIED_ON," +
                               "MODIFIED_BY," +
                               "MODIFIED_BY_NAME," +
                            //  "IS_MULTI_CURRENCY," +
                               "BRANCH_ID," +
                               "LOCATION_ID," +
                               "NAME_ADDRESS ) VALUES( " +
                               "?VOUCHER_ID, " +
                               "?VOUCHER_DATE, " +
                               "?PROJECT_ID, " +
                               "?VOUCHER_NO, " +
                               "?VOUCHER_TYPE," +
                               "?VOUCHER_SUB_TYPE, " +
                               "?DONOR_ID," +
                               "?PURPOSE_ID," +
                               "?CONTRIBUTION_TYPE," +
                               "?CONTRIBUTION_AMOUNT," +
                               "?CURRENCY_COUNTRY_ID," +
                               "?EXCHANGE_RATE," +
                               "?CALCULATED_AMOUNT," +
                               "?ACTUAL_AMOUNT," +
                               "?CLIENT_CODE,\n" +
                               "?CLIENT_REFERENCE_ID,\n" +
                               "?EXCHANGE_COUNTRY_ID," +
                               "?NARRATION," +
                               "?CREATED_ON," +
                               "?CREATED_BY," +
                               "?CREATED_BY_NAME," +
                               "?MODIFIED_ON," +
                               "?MODIFIED_BY," +
                               "?MODIFIED_BY_NAME," +
                            //  "?IS_MULTI_CURRENCY," +
                               "?BRANCH_OFFICE_ID," +
                               "?LOCATION_ID," +
                               "?NAME_ADDRESS)";
                        break;
                    }
                case SQLCommand.ImportVoucher.InsertVoucherMasterBranch:
                    {
                        sQuery = "INSERT INTO VOUCHER_MASTER_TRANS ( " +
                               "VOUCHER_DATE, " +
                               "PROJECT_ID, " +
                               "VOUCHER_NO, " +
                               "VOUCHER_TYPE," +
                               "VOUCHER_SUB_TYPE, " +
                               "DONOR_ID," +
                               "PURPOSE_ID," +
                               "CONTRIBUTION_TYPE," +
                               "CONTRIBUTION_AMOUNT," +
                               "CURRENCY_COUNTRY_ID," +
                               "EXCHANGE_RATE," +
                               "CALCULATED_AMOUNT," +
                               "ACTUAL_AMOUNT," +
                               "CLIENT_CODE,\n" +
                               "CLIENT_REFERENCE_ID,\n" +
                               "EXCHANGE_COUNTRY_ID," +
                               "NARRATION," +
                               "CREATED_ON," +
                               "CREATED_BY," +
                               "CREATED_BY_NAME," +
                            //"MODIFIED_ON," +
                            //"MODIFIED_BY," +
                            //"MODIFIED_BY_NAME," +
                               "BRANCH_ID," +
                               "LOCATION_ID," +
                               "GST_VENDOR_ID, GST_VENDOR_INVOICE_NO, GST_VENDOR_INVOICE_TYPE, GST_VENDOR_INVOICE_DATE,IS_CASH_BANK_STATUS," +
                               "NAME_ADDRESS,VOUCHER_DEFINITION_ID) " +
                               "VALUES( " +
                               "?VOUCHER_DATE, " +
                               "?PROJECT_ID, " +
                               "?VOUCHER_NO, " +
                               "?VOUCHER_TYPE," +
                               "?VOUCHER_SUB_TYPE, " +
                               "?DONOR_ID," +
                               "?PURPOSE_ID," +
                               "?CONTRIBUTION_TYPE," +
                               "?CONTRIBUTION_AMOUNT," +
                               "?CURRENCY_COUNTRY_ID," +
                               "?EXCHANGE_RATE," +
                               "?CALCULATED_AMOUNT," +
                               "?ACTUAL_AMOUNT," +
                               "?CLIENT_CODE,\n" +
                               "?CLIENT_REFERENCE_ID,\n" +
                               "?EXCHANGE_COUNTRY_ID," +
                               "?NARRATION," +
                               "NOW()," +
                               "?CREATED_BY," +
                               "?CREATED_BY_NAME," +
                            //"?MODIFIED_ON," +
                            //"?MODIFIED_BY," +
                            //"?MODIFIED_BY_NAME," +
                               "?BRANCH_OFFICE_ID," +
                               "?LOCATION_ID," +
                               "?GST_VENDOR_ID, ?GST_VENDOR_INVOICE_NO, ?GST_VENDOR_INVOICE_TYPE, ?GST_VENDOR_INVOICE_DATE,?IS_CASH_BANK_STATUS," +
                               "?NAME_ADDRESS,?VOUCHER_DEFINITION_ID)";
                        break;
                    }
                case SQLCommand.ImportVoucher.UpdateModifiedOn:
                    {
                        sQuery = "UPDATE VOUCHER_MASTER_TRANS SET MODIFIED_ON=CREATED_ON;";
                        break;
                    }
                case SQLCommand.ImportVoucher.InsertVoucherTrans:
                    {
                        sQuery = "INSERT INTO VOUCHER_TRANS (\n" +
                               "VOUCHER_ID,\n" +
                               "SEQUENCE_NO,\n" +
                               "LEDGER_ID,\n" +
                               "AMOUNT,\n" +
                               "TRANS_MODE,\n" +
                               "CHEQUE_NO,\n" +
                               "BRANCH_ID,\n" +
                               "LOCATION_ID, MATERIALIZED_ON, NARRATION,\n" +
                               "CHEQUE_REF_DATE, CHEQUE_REF_BANKNAME, CHEQUE_REF_BRANCH, FUND_TRANSFER_TYPE_NAME,\n" +
                               "LEDGER_GST_CLASS_ID, GST, CGST, SGST, IGST)\n" +
                               "VALUES(\n" +
                               "?VOUCHER_ID,\n" +
                               "?SEQUENCE_NO,\n" +
                               "?LEDGER_ID,\n" +
                               "?AMOUNT,\n" +
                               "?TRANS_MODE,\n" +
                               "?CHEQUE_NO,\n" +
                               "?BRANCH_OFFICE_ID,\n" +
                               "?LOCATION_ID, ?MATERIALIZED_ON, ?NARRATION,\n" +
                               "?CHEQUE_REF_DATE, ?CHEQUE_REF_BANKNAME, ?CHEQUE_REF_BRANCH, ?FUND_TRANSFER_TYPE_NAME,\n" +
                               "?LEDGER_GST_CLASS_ID, ?GST, ?CGST, ?SGST, ?IGST)";
                        break;
                    }
                case SQLCommand.ImportVoucher.IsProjectBranchMapped:
                    {
                        sQuery = "SELECT COUNT(*) FROM PROJECT_BRANCH WHERE PROJECT_ID=?PROJECT_ID AND BRANCH_ID=?BRANCH_OFFICE_ID";
                        break;
                    }
                case SQLCommand.ImportVoucher.InsertVoucherCostCentre:
                    {
                        sQuery = "INSERT INTO VOUCHER_CC_TRANS ( " +
                               "VOUCHER_ID, " +
                               "SEQUENCE_NO, " +
                               "LEDGER_ID," +
                               "COST_CENTRE_ID, " +
                               "BRANCH_ID, " +
                               "LOCATION_ID," +
                               "AMOUNT,COST_CENTRE_TABLE,LEDGER_SEQUENCE_NO) VALUES( " +
                               "?VOUCHER_ID, " +
                               "?SEQUENCE_NO, " +
                               "?LEDGER_ID, " +
                               "?COST_CENTRE_ID, " +
                               "?BRANCH_OFFICE_ID, " +
                               "?LOCATION_ID," +
                               "?AMOUNT,?COST_CENTRE_TABLE,?LEDGER_SEQUENCE_NO)";
                        break;
                    }
                case SQLCommand.ImportVoucher.InsertVoucherSubLedger:
                    {
                        sQuery = "INSERT INTO VOUCHER_SUB_LEDGER_TRANS " +
                               "(VOUCHER_ID, SEQUENCE_NO, LEDGER_ID, SUB_LEDGER_ID, AMOUNT, TRANS_MODE, BRANCH_ID, LOCATION_ID) " +
                               "VALUES(?VOUCHER_ID, ?SEQUENCE_NO, ?LEDGER_ID, ?SUB_LEDGER_ID, ?AMOUNT, ?TRANS_MODE, ?BRANCH_OFFICE_ID, ?LOCATION_ID)";
                        break;
                    }
                case SQLCommand.ImportVoucher.InsertProjectCategory:
                    {
                        sQuery = "INSERT INTO MASTER_PROJECT_CATOGORY ( " +
                                "PROJECT_CATOGORY_NAME) VALUES( " +
                                "?PROJECT_CATOGORY_NAME)";
                        break;
                    }
                case SQLCommand.ImportVoucher.InsertProject:
                    {
                        sQuery = "INSERT INTO MASTER_PROJECT ( " +
                                   "PROJECT_CODE, " +
                                   "PROJECT, " +
                                   "DIVISION_ID, " +
                                   "ACCOUNT_DATE," +
                                   "DATE_STARTED," +
                                   "DATE_CLOSED," +
                                   "DESCRIPTION," +
                                   "CUSTOMERID," +
                                   "NOTES, " +
                                   "PROJECT_CATEGORY_ID ) VALUES( " +
                                   "?PROJECT_CODE, " +
                                   "?PROJECT, " +
                                   "?DIVISION_ID, " +
                                   "?ACCOUNT_DATE," +
                                   "?DATE_STARTED," +
                                   "?DATE_CLOSED," +
                                   "?DESCRIPTION," +
                                   "?CUSTOMERID," +
                                   "?NOTES," +
                                   "?PROJECT_CATEGORY_ID)";
                        break;
                    }
                case SQLCommand.ImportVoucher.MapDefaultVoucher:
                    {
                        //Added by Carmel Raj on October-08-2015
                        //Purpose :Map default voucher to Project
                        //Default Voucher Id
                        //1 - Receipts
                        //2 - Payments
                        //3 - Contra
                        //4 - Journal 
                        //Since the Default Voucher Id are fixed as per the architecture Ids are passed as 1,2,3,4
                        sQuery = "INSERT INTO PROJECT_VOUCHER (PROJECT_ID, VOUCHER_ID) VALUES (?PROJECT_ID, 1);\n" +
                                "INSERT INTO PROJECT_VOUCHER (PROJECT_ID, VOUCHER_ID) VALUES (?PROJECT_ID, 2);\n" +
                                "INSERT INTO PROJECT_VOUCHER (PROJECT_ID, VOUCHER_ID) VALUES (?PROJECT_ID, 3);\n" +
                                "INSERT INTO PROJECT_VOUCHER (PROJECT_ID, VOUCHER_ID) VALUES (?PROJECT_ID, 4);";
                        break;
                    }
                case SQLCommand.ImportVoucher.FetchMergeProject:
                    {
                        sQuery = "SELECT PROJECT_CODE,\n" +
                                    "       PROJECT,\n" +
                                    "       PROJECT_ID,\n" +
                                    "       DIVISION_ID,\n" +
                                    "       ACCOUNT_DATE,\n" +
                                    "       DATE_STARTED,\n" +
                                    "       DATE_CLOSED,\n" +
                                    "       DESCRIPTION,\n" +
                                    "       PROJECT_CATOGORY_NAME\n" +
                                    "  FROM MASTER_PROJECT MP\n" +
                                    "  LEFT JOIN MASTER_PROJECT_CATOGORY MPC\n" +
                                    "    ON MP.PROJECT_CATEGORY_ID = MPC.PROJECT_CATOGORY_ID\n" +
                                    " WHERE PROJECT_ID =?PROJECT_ID;";
                        break;
                    }

                case SQLCommand.ImportVoucher.GetPreviousOPBalance:
                    {
                        sQuery = "SELECT AMOUNT\n" +
                                "  FROM LEDGER_BALANCE\n" +
                                " WHERE TRANS_FLAG = 'OP'\n" +
                                "   AND LEDGER_ID =?LEDGER_ID\n" +
                                "   AND PROJECT_ID =?PROJECT_ID;";
                        break;
                    }
                case SQLCommand.ImportVoucher.InsertLedgerGroup:
                    {
                        sQuery = "INSERT INTO MASTER_LEDGER_GROUP ( " +
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
                case SQLCommand.ImportVoucher.UpdateLedgerGroup:
                    {
                        sQuery = "UPDATE MASTER_LEDGER_GROUP SET " +
                                        "GROUP_ID = ?GROUP_ID, " +
                                        "GROUP_CODE =?GROUP_CODE, " +
                                        "LEDGER_GROUP =?LEDGER_GROUP, " +
                                        "PARENT_GROUP_ID=?PARENT_GROUP_ID, " +
                                        "NATURE_ID=?NATURE_ID, " +
                                        "MAIN_GROUP_ID=?MAIN_GROUP_ID,IMAGE_ID=?IMAGE_ID " +
                                        " WHERE GROUP_ID=?GROUP_ID";
                        break;
                    }
                //,CUR_COUNTRY_ID,OP_EXCHANGE_RATE, ?CUR_COUNTRY_ID, ?OP_EXCHANGE_RATE,
                case SQLCommand.ImportVoucher.InsertLedger:
                    {
                        sQuery = "INSERT INTO MASTER_LEDGER ( " +
                                   "LEDGER_CODE, " +
                                   "LEDGER_NAME, " +
                                   "GROUP_ID,  " +
                                   "LEDGER_TYPE, " +
                                   "LEDGER_SUB_TYPE, " +
                                   "IS_BRANCH_LEDGER," +
                                   "SORT_ID," +
                                   "IS_COST_CENTER, IS_BANK_INTEREST_LEDGER, IS_TDS_LEDGER, IS_INKIND_LEDGER, IS_DEPRECIATION_LEDGER," +
                                   "IS_ASSET_GAIN_LEDGER, IS_ASSET_LOSS_LEDGER, IS_DISPOSAL_LEDGER," +
                                   "IS_BANK_SB_INTEREST_LEDGER, IS_BANK_COMMISSION_LEDGER, IS_BANK_FD_PENALTY_LEDGER," +
                                   "GST_HSN_SAC_CODE," +
                                   "IS_SUBSIDY_LEDGER, IS_GST_LEDGERS, GST_SERVICE_TYPE, BUDGET_GROUP_ID, BUDGET_SUB_GROUP_ID, FD_INVESTMENT_TYPE_ID,DATE_CLOSED) " +
                                   "VALUES( " +
                                   "?LEDGER_CODE, " +
                                   "?LEDGER_NAME, " +
                                   "?GROUP_ID,  " +
                                   "?LEDGER_TYPE, " +
                                   "?LEDGER_SUB_TYPE, " +
                                   "?IS_BRANCH_LEDGER, ?SORT_ID," +
                                   "?IS_COST_CENTER, ?IS_BANK_INTEREST_LEDGER, ?IS_TDS_LEDGER, ?IS_INKIND_LEDGER, ?IS_DEPRECIATION_LEDGER," +
                                   "?IS_ASSET_GAIN_LEDGER, ?IS_ASSET_LOSS_LEDGER, ?IS_DISPOSAL_LEDGER," +
                                   "?IS_BANK_SB_INTEREST_LEDGER, ?IS_BANK_COMMISSION_LEDGER, ?IS_BANK_FD_PENALTY_LEDGER," +
                                   "?GST_HSN_SAC_CODE," +
                                   "?IS_SUBSIDY_LEDGER, ?IS_GST_LEDGERS, ?GST_SERVICE_TYPE, ?BUDGET_GROUP_ID, ?BUDGET_SUB_GROUP_ID, ?FD_INVESTMENT_TYPE_ID,?DATE_CLOSED)";
                        break;
                    }
                case SQLCommand.ImportVoucher.EnableLedgerPropertiesDetails:
                    { //24/06/2021, to update ledger
                        sQuery = "UPDATE MASTER_LEDGER\n" +
                                   "SET IS_COST_CENTER = IF(IS_COST_CENTER=0, ?IS_COST_CENTER, IS_COST_CENTER),\n" +
                                   "IS_BANK_INTEREST_LEDGER = IF(IS_BANK_INTEREST_LEDGER=0, ?IS_BANK_INTEREST_LEDGER, IS_BANK_INTEREST_LEDGER),\n" +
                                   "IS_TDS_LEDGER = IF(IS_TDS_LEDGER=0, ?IS_TDS_LEDGER, IS_TDS_LEDGER),\n" +
                                   "IS_INKIND_LEDGER = IF(IS_INKIND_LEDGER=0, ?IS_INKIND_LEDGER, IS_INKIND_LEDGER),\n" +
                                   "IS_DEPRECIATION_LEDGER = IF(IS_DEPRECIATION_LEDGER =0, ?IS_DEPRECIATION_LEDGER , IS_DEPRECIATION_LEDGER),\n" +
                                   "IS_ASSET_GAIN_LEDGER = IF(IS_ASSET_GAIN_LEDGER=0, ?IS_ASSET_GAIN_LEDGER, IS_ASSET_GAIN_LEDGER),\n" +
                                   "IS_ASSET_LOSS_LEDGER = IF(IS_ASSET_LOSS_LEDGER=0, ?IS_ASSET_LOSS_LEDGER, IS_ASSET_LOSS_LEDGER),\n" +
                                   "IS_DISPOSAL_LEDGER = IF(IS_DISPOSAL_LEDGER=0, ?IS_DISPOSAL_LEDGER, IS_DISPOSAL_LEDGER),\n" +
                                   "IS_SUBSIDY_LEDGER = IF(IS_SUBSIDY_LEDGER=0, ?IS_SUBSIDY_LEDGER, IS_SUBSIDY_LEDGER),\n" +
                                   "IS_GST_LEDGERS = IF(IS_GST_LEDGERS=0, ?IS_GST_LEDGERS, IS_GST_LEDGERS),\n" +
                                   "IS_BANK_SB_INTEREST_LEDGER = IF(IS_BANK_SB_INTEREST_LEDGER=0, ?IS_BANK_SB_INTEREST_LEDGER, IS_BANK_SB_INTEREST_LEDGER),\n" +
                                   "IS_BANK_COMMISSION_LEDGER = IF(IS_BANK_COMMISSION_LEDGER=0, ?IS_BANK_COMMISSION_LEDGER, IS_BANK_COMMISSION_LEDGER),\n" +
                                   "IS_BANK_FD_PENALTY_LEDGER = IF(IS_BANK_FD_PENALTY_LEDGER=0, ?IS_BANK_FD_PENALTY_LEDGER, IS_BANK_FD_PENALTY_LEDGER),\n" +
                                   "GST_SERVICE_TYPE = ?GST_SERVICE_TYPE,\n" +
                                   "GST_HSN_SAC_CODE=?GST_HSN_SAC_CODE,\n" +
                                   "BUDGET_GROUP_ID=?BUDGET_GROUP_ID,\n" +
                                   "BUDGET_SUB_GROUP_ID=?BUDGET_SUB_GROUP_ID,\n" +
                                   "FD_INVESTMENT_TYPE_ID=?FD_INVESTMENT_TYPE_ID,\n" +
                                   "DATE_CLOSED = ?DATE_CLOSED, CLOSED_BY=?CLOSED_BY\n" +
                                   "WHERE LEDGER_ID = ?LEDGER_ID";

                        break;
                    }
                case SQLCommand.ImportVoucher.CheckTransactionExistsByDateClose:
                    {
                        sQuery = "SELECT VMT.VOUCHER_ID, VT.LEDGER_ID\n" +
                                "  FROM VOUCHER_MASTER_TRANS VMT\n" +
                                " INNER JOIN VOUCHER_TRANS VT\n" +
                                "    ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                                " WHERE VMT.VOUCHER_DATE > ?DATE_CLOSED\n" +
                                "   AND VT.LEDGER_ID = ?LEDGER_ID AND VMT.STATUS = 1 LIMIT 1";
                        break;
                    }
                case SQLCommand.ImportVoucher.CheckFDAccountsExistsByLedger:
                    {
                        sQuery = @"SELECT FD_ACCOUNT_ID, IFNULL(ML.FD_INVESTMENT_TYPE_ID,0) AS FD_INVESTMENT_TYPE_ID  
                                    FROM FD_ACCOUNT AS FD 
                                    INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = FD.LEDGER_ID
                                    WHERE FD.LEDGER_ID = ?LEDGER_ID AND FD.STATUS = 1";
                        break;
                    }
                case SQLCommand.ImportVoucher.InsertLedgerProfile:
                    { //06/07/2021, to update ledger
                        sQuery = "INSERT INTO TDS_CREDTIORS_PROFILE\n" +
                                " (LEDGER_ID, DEDUTEE_TYPE_ID, NATURE_OF_PAYMENT_ID, NAME, ADDRESS, STATE_ID, PIN_CODE, CONTACT_PERSON, CONTACT_NUMBER,\n" +
                                " EMAIL, PAN_NUMBER, PAN_IT_HOLDER_NAME, COUNTRY_ID, GST_ID, GST_NO)\n" +
                                "VALUES\n" +
                                " (?LEDGER_ID, ?DEDUTEE_TYPE_ID, ?NATURE_OF_PAYMENT_ID, ?NAME, ?ADDRESS, ?STATE_ID, ?PIN_CODE, ?CONTACT_PERSON, ?CONTACT_NUMBER,\n" +
                                " ?EMAIL, ?PAN_NUMBER, ?PAN_IT_HOLDER_NAME, ?COUNTRY_ID, ?GST_Id, ?GST_NO)";
                        break;
                    }
                case SQLCommand.ImportVoucher.UpdateLedgerProfile:
                    { //06/07/2021, to update ledger
                        sQuery = "UPDATE TDS_CREDTIORS_PROFILE\n" +
                                "   SET DEDUTEE_TYPE_ID    = ?DEDUTEE_TYPE_ID,\n" +
                                "       NATURE_OF_PAYMENT_ID=?NATURE_OF_PAYMENT_ID,\n" +
                                "       NAME               = ?NAME,\n" +
                                "       ADDRESS            = ?ADDRESS,\n" +
                                "       STATE_ID           = ?STATE_ID,\n" +
                                "       PIN_CODE           = ?PIN_CODE,\n" +
                                "       CONTACT_PERSON     = ?CONTACT_PERSON,\n" +
                                "       CONTACT_NUMBER     = ?CONTACT_NUMBER,\n" +
                                "       EMAIL              = ?EMAIL,\n" +
                                "       LEDGER_ID          = ?LEDGER_ID,\n" +
                                "       PAN_NUMBER         = ?PAN_NUMBER,\n" +
                                "       PAN_IT_HOLDER_NAME = ?PAN_IT_HOLDER_NAME,\n" +
                                "       COUNTRY_ID         = ?COUNTRY_ID,\n" +
                                "       GST_NO             = ?GST_NO,\n" +
                                "       GST_ID             = ?GST_Id\n" +
                                " WHERE LEDGER_ID = ?LEDGER_ID";
                        break;
                    }
                case SQLCommand.ImportVoucher.IsLedgerProfileExists:
                    { //06/07/2021, to update ledger
                        sQuery = "SELECT COUNT(*) FROM TDS_CREDTIORS_PROFILE WHERE LEDGER_ID = ?LEDGER_ID";
                        break;
                    }
                case SQLCommand.ImportVoucher.InsertCountry:
                    {
                        sQuery = "INSERT INTO MASTER_COUNTRY ( " +
                              "COUNTRY, " +
                              "COUNTRY_CODE, " +
                               "CURRENCY_CODE, " +
                              "CURRENCY_SYMBOL, " +
                              "CURRENCY_NAME ) VALUES( " +
                              "?COUNTRY, " +
                              "?COUNTRY_CODE, " +
                               "?CURRENCY_CODE, " +
                              "?CURRENCY_SYMBOL, " +
                              "?CURRENCY_NAME) ";

                        break;
                    }
                case SQLCommand.ImportVoucher.InsertState:
                    {
                        sQuery = "INSERT INTO MASTER_STATE (STATE_NAME, STATE_CODE, COUNTRY_ID) VALUES (?STATE_NAME, ?STATE_CODE, ?COUNTRY_ID)";
                        break;
                    }
                case SQLCommand.ImportVoucher.InsertGSTClass:
                    {
                        sQuery = "INSERT INTO MASTER_GST_CLASS(SLAB, GST, CGST, SGST, APPLICABLE_FROM, STATUS, SORT_ORDER)" +
                            "VALUES(?SLAB, ?GST, ?CGST, ?SGST, ?APPLICABLE_FROM, ?STATUS, ?SORT_ORDER)";
                        break;
                    }
                case SQLCommand.ImportVoucher.InsertAssetStockVendor:
                    {
                        sQuery = "INSERT INTO ASSET_STOCK_VENDOR(VENDOR, ADDRESS, STATE_ID, COUNTRY_ID, PAN_NO, GST_NO, CONTACT_NO, EMAIL_ID, BRANCH_ID)\n" +
                            "VALUES(?VENDOR, ?ADDRESS, IF(?STATE_ID=0, NULL, ?STATE_ID), IF(?COUNTRY_ID=0, NULL, ?COUNTRY_ID), ?PAN_NO, ?GST_NO, ?CONTACT_NO, ?EMAIL_ID, ?BRANCH_ID)";
                        break;
                    }
                case SQLCommand.ImportVoucher.InsertDonor:
                    {
                        sQuery = "INSERT INTO MASTER_DONAUD ( " +
                               "NAME, " +
                               "PLACE," +
                               "TYPE," +
                               "COMPANY_NAME," +
                               "COUNTRY_ID," +
                               "PINCODE," +
                               "PHONE," +
                               "FAX," +
                               "EMAIL," +
                               "IDENTITYKEY," +
                               "URL," +
                               "STATE," +
                               "STATE_ID," +
                               "ADDRESS," +
                               "PAN," +
                               "BRANCH_ID ) VALUES( " +
                               "?NAME, " +
                               "?PLACE," +
                               "?TYPE," +
                               "?COMPANY_NAME," +
                               "?COUNTRY_ID," +
                               "?PINCODE," +
                               "?PHONE," +
                               "?FAX," +
                               "?EMAIL," +
                               "?IDENTITYKEY," +
                               "?URL," +
                               "?STATE," +
                               "?STATE_ID," +
                               "?ADDRESS," +
                               "?PAN," +
                               "?BRANCH_OFFICE_ID)";
                        break;
                    }
                case SQLCommand.ImportVoucher.InsertCostCentre:
                    {
                        sQuery = "INSERT INTO MASTER_COST_CENTRE ( " +
                               "COST_CENTRE_NAME) " +
                               " VALUES( " +
                               "?COST_CENTRE_NAME)";
                        break;
                    }

                case SQLCommand.ImportVoucher.InsertCostCategory:
                    {
                        sQuery = "INSERT INTO MASTER_COST_CENTRE_CATEGORY ( " +
                               "COST_CENTRE_CATEGORY_NAME) " +
                               " VALUES( " +
                               "?COST_CENTRE_CATEGORY_NAME)";
                        break;
                    }



                case SQLCommand.ImportVoucher.InsertBank:
                    {
                        sQuery = "INSERT INTO MASTER_BANK ( " +
                                "BANK_CODE, " +
                                "BANK, " +
                                "BRANCH, " +
                                "ADDRESS," +
                                "IFSCCODE," +
                                "MICRCODE," +
                                "CONTACTNUMBER," +
                                "SWIFTCODE ) VALUES( " +
                                "?BANK_CODE, " +
                                "?BANK, " +
                                "?BRANCH, " +
                                "?ADDRESS," +
                                "?IFSCCODE," +
                                "?MICRCODE," +
                                "?CONTACTNUMBER," +
                                "?SWIFTCODE)";
                        break;
                    }
                case SQLCommand.ImportVoucher.InsertBankAccount:
                    {
                        sQuery = "INSERT INTO MASTER_BANK_ACCOUNT ( " +
                                    "ACCOUNT_CODE, " +
                                    "ACCOUNT_NUMBER, " +
                                    "ACCOUNT_HOLDER_NAME, " +
                                    "BRANCH_ID, " +
                                    "BANK_ID, " +
                                    "ACCOUNT_TYPE_ID, " +
                                    "DATE_OPENED, DATE_CLOSED," +
                                    "LEDGER_ID,IS_FCRA_ACCOUNT ) " +
                                    "VALUES( " +
                                    "?ACCOUNT_CODE, " +
                                    "?ACCOUNT_NUMBER, " +
                                    "?ACCOUNT_HOLDER_NAME, " +
                                    "?BRANCH_OFFICE_ID, " +
                                    "?BANK_ID, " +
                                    "?ACCOUNT_TYPE_ID, " +
                                    "?DATE_OPENED,?DATE_CLOSED," +
                                    "?LEDGER_ID,?IS_FCRA_ACCOUNT) ";
                        break;
                    }
                case SQLCommand.ImportVoucher.UpdateBankAccount:
                    {
                        sQuery = "UPDATE MASTER_BANK_ACCOUNT\n" +
                                    "   SET ACCOUNT_CODE        = ?ACCOUNT_CODE,\n" +
                                    "       ACCOUNT_NUMBER      = ?ACCOUNT_NUMBER,\n" +
                                    "       ACCOUNT_HOLDER_NAME = ?ACCOUNT_HOLDER_NAME,\n" +
                                    "       BRANCH_ID           = ?BRANCH_OFFICE_ID,\n" +
                                    "       BANK_ID             = ?BANK_ID,\n" +
                                    "       ACCOUNT_TYPE_ID     = ?ACCOUNT_TYPE_ID,\n" +
                                    "       DATE_OPENED         = ?DATE_OPENED,\n" +
                                    "       DATE_CLOSED         = ?DATE_CLOSED,\n" +
                                    "       LEDGER_ID           = ?LEDGER_ID,\n" +
                                    "       IS_FCRA_ACCOUNT     = ?IS_FCRA_ACCOUNT\n" +
                                    " WHERE BANK_ACCOUNT_ID = ?BANK_ACCOUNT_ID;";
                        break;
                    }
                case SQLCommand.ImportVoucher.InsertLedgerBank:
                    {
                        sQuery = "INSERT INTO MASTER_LEDGER ( " +
                                    "LEDGER_CODE, " +
                                    "LEDGER_NAME, " +
                                    "LEDGER_SUB_TYPE, " +
                                    "IS_BRANCH_LEDGER," +
                            //  "CUR_COUNTRY_ID," +
                                    "GROUP_ID ) " +
                                    "VALUES( " +
                                    "?LEDGER_CODE, " +
                                    "?LEDGER_NAME, " +
                                    "'BK'," +
                                    "?IS_BRANCH_LEDGER," +
                            //  "?CUR_COUNTRY_ID," +
                                    "?GROUP_ID) ";
                        break;
                    }
                case SQLCommand.ImportVoucher.InsertSubLedger:
                    {
                        sQuery = "INSERT INTO MASTER_SUB_LEDGER (SUB_LEDGER_ID, SUB_LEDGER_NAME) " +
                                              "VALUES(?SUB_LEDGER_ID, ?SUB_LEDGER_NAME)";
                        break;
                    }
                case SQLCommand.ImportVoucher.InsertLedgerBalance:
                    {
                        sQuery = "INSERT INTO LEDGER_BALANCE ( " +
                                 "BALANCE_DATE, " +
                                 "PROJECT_ID, " +
                                 "LEDGER_ID, " +
                                 "AMOUNT, " +
                                 "TRANS_MODE, TRANS_FC_MODE," +
                                 "TRANS_FLAG)VALUES( " +
                                 "?BALANCE_DATE, " +
                                 "?PROJECT_ID, " +
                                 "?LEDGER_ID, " +
                                 "?AMOUNT, " +
                                 "?TRANS_MODE, ?TRANS_MODE," +
                                 "?TRANS_FLAG";
                        break;
                    }
                case SQLCommand.ImportVoucher.IsProjectCategoryExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM MASTER_PROJECT_CATOGORY\n" +
                                            " WHERE PROJECT_CATOGORY_NAME =?PROJECT_CATOGORY_NAME";
                        break;
                    }
                case SQLCommand.ImportVoucher.IsProjectExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM MASTER_PROJECT\n" +
                                            " WHERE PROJECT =?PROJECT";
                        break;
                    }
                case SQLCommand.ImportVoucher.IsPurposeExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM MASTER_CONTRIBUTION_HEAD\n" +
                                            " WHERE FC_PURPOSE =?FC_PURPOSE";
                        break;
                    }
                case SQLCommand.ImportVoucher.IsLedgerExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM MASTER_LEDGER\n" +
                                            " WHERE  LEDGER_NAME =?LEDGER_NAME";
                        break;
                    }
                case SQLCommand.ImportVoucher.IsLedgerGroupExists:
                    {
                        sQuery = "SELECT COUNT(*) FROM MASTER_LEDGER_GROUP WHERE LEDGER_GROUP=?LEDGER_GROUP";
                        break;
                    }
                //case SQLCommand.ImportVoucher.IsLedgerCodeExists:
                //    {
                //        sQuery = "SELECT COUNT(*) FROM MASTER_LEDGER WHERE LEDGER_CODE=?LEDGER_CODE";
                //        break;
                //    }
                case SQLCommand.ImportVoucher.IsCountryExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM MASTER_COUNTRY\n" +
                                            " WHERE COUNTRY=?COUNTRY";
                        break;
                    }
                case SQLCommand.ImportVoucher.IsStateExists:
                    {
                        sQuery = "SELECT COUNT(*) FROM MASTER_STATE\n" +
                                   " WHERE STATE_NAME = ?STATE_NAME";
                        break;
                    }
                case SQLCommand.ImportVoucher.IsGSTClassExists:
                    {
                        sQuery = "SELECT COUNT(*) FROM MASTER_GST_CLASS WHERE SLAB = ?SLAB";
                        break;
                    }
                case SQLCommand.ImportVoucher.IsVendorExists:
                    {
                        sQuery = "SELECT COUNT(*) FROM ASSET_STOCK_VENDOR WHERE VENDOR = ?VENDOR";
                        break;
                    }
                case SQLCommand.ImportVoucher.IsDonorExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM MASTER_DONAUD\n" +
                                            " WHERE NAME =?NAME AND BRANCH_ID=?BRANCH_OFFICE_ID";
                        break;
                    }
                case SQLCommand.ImportVoucher.IsBankExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM MASTER_BANK\n" +
                                            " WHERE BANK =?BANK AND BRANCH=?BRANCH";
                        break;
                    }
                //case SQLCommand.ImportVoucher.IsBankCodeExists:
                //    {
                //        sQuery = "SELECT COUNT(*)\n" +
                //                            "  FROM MASTER_BANK\n" +
                //                            " WHERE BANK_CODE =?BANK_CODE";
                //        break;
                //    }
                case SQLCommand.ImportVoucher.IsBankAccountExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM MASTER_BANK_ACCOUNT\n" +
                                            " WHERE ACCOUNT_NUMBER =?ACCOUNT_NUMBER AND BRANCH_ID=?BRANCH_OFFICE_ID";
                        break;
                    }
                //case SQLCommand.ImportVoucher.IsBankAccountCodeExits:
                //    {
                //        sQuery = "SELECT COUNT(*)\n" +
                //                            "  FROM MASTER_BANK_ACCOUNT\n" +
                //                            " WHERE ACCOUNT_CODE =?ACCOUNT_CODE";
                //        break;
                //    }
                case SQLCommand.ImportVoucher.IsLedgerBankExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM MASTER_LEDGER\n" +
                                            " WHERE LEDGER_NAME =?LEDGER_NAME";
                        break;
                    }
                case SQLCommand.ImportVoucher.IsCostCentreExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM MASTER_COST_CENTRE\n" +
                                            " WHERE COST_CENTRE_NAME =?COST_CENTRE_NAME";
                        break;
                    }
                case SQLCommand.ImportVoucher.IsCostCategoryExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM MASTER_COST_CENTRE_CATEGORY\n" +
                                            " WHERE COST_CENTRE_CATEGORY_NAME =?COST_CENTRE_CATEGORY_NAME";
                        break;
                    }
                case SQLCommand.ImportVoucher.IsLegalEntityExists: //12/04/2017, to check legal entity exists in db..for project import and export
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM MASTER_INSTI_PERFERENCE\n" +
                                            " WHERE SOCIETYNAME=?SOCIETYNAME";
                        break;
                    }
                case SQLCommand.ImportVoucher.IsVoucherExists:
                    {
                        sQuery = "SELECT COUNT(*) FROM VOUCHER_MASTER_TRANS";
                        break;
                    }

                case SQLCommand.ImportVoucher.GetProjectCategoryId:
                    {
                        sQuery = "SELECT PROJECT_CATOGORY_ID\n" +
                                            "  FROM MASTER_PROJECT_CATOGORY\n" +
                                            " WHERE PROJECT_CATOGORY_NAME =?PROJECT_CATOGORY_NAME";
                        break;
                    }
                case SQLCommand.ImportVoucher.GetProjectId:
                    {
                        sQuery = "SELECT PROJECT_ID\n" +
                                            "  FROM MASTER_PROJECT\n" +
                                            " WHERE PROJECT =?PROJECT";
                        break;
                    }
                case SQLCommand.ImportVoucher.GetPurposeId:
                    {
                        sQuery = "SELECT CONTRIBUTION_ID\n" +
                                            "  FROM MASTER_CONTRIBUTION_HEAD\n" +
                                            " WHERE FC_PURPOSE =?FC_PURPOSE";
                        break;
                    }
                case SQLCommand.ImportVoucher.GetBankId:
                    {
                        sQuery = "SELECT BANK_ID\n" +
                                            "  FROM MASTER_BANK\n" +
                                            " WHERE BANK =?BANK {AND BRANCH=?BRANCH}";
                        break;
                    }
                case SQLCommand.ImportVoucher.GetCostCentreId:
                    {
                        sQuery = "SELECT COST_CENTRE_ID\n" +
                                            "  FROM MASTER_COST_CENTRE\n" +
                                            " WHERE COST_CENTRE_NAME =?COST_CENTRE_NAME";
                        break;
                    }
                case SQLCommand.ImportVoucher.GetCostCategoryId:
                    {
                        sQuery = "SELECT COST_CENTRECATEGORY_ID\n" +
                                            "  FROM MASTER_COST_CENTRE_CATEGORY\n" +
                                            " WHERE COST_CENTRE_CATEGORY_NAME =?COST_CENTRE_CATEGORY_NAME";
                        break;
                    }
                case SQLCommand.ImportVoucher.GetDonorId:
                    {
                        sQuery = "SELECT DONAUD_ID\n" +
                                            "  FROM MASTER_DONAUD\n" +
                                            " WHERE NAME =?NAME AND BRANCH_ID=?BRANCH_OFFICE_ID";
                        break;
                    }
                case SQLCommand.ImportVoucher.GetBankAccountId:
                    {
                        sQuery = "SELECT BANK_ACCOUNT_ID\n" +
                                "  FROM MASTER_BANK_ACCOUNT\n" +
                                " WHERE ACCOUNT_NUMBER = ?ACCOUNT_NUMBER\n" +
                                "   AND BRANCH_ID = ?BRANCH_OFFICE_ID;";
                        break;
                    }
                case SQLCommand.ImportVoucher.GetLedgerId:
                    {
                        sQuery = "SELECT LEDGER_ID\n" +
                                            "  FROM MASTER_LEDGER\n" +
                                            " WHERE LEDGER_NAME =?LEDGER_NAME";
                        break;
                    }
                case SQLCommand.ImportVoucher.GetLedgerGroupId:
                    {
                        sQuery = "SELECT GROUP_ID FROM MASTER_LEDGER_GROUP WHERE LEDGER_GROUP=?LEDGER_GROUP";
                        break;
                    }
                case SQLCommand.ImportVoucher.GetParentGroupId:
                    {
                        sQuery = "SELECT GROUP_ID FROM MASTER_LEDGER_GROUP WHERE LEDGER_GROUP=?LEDGER_GROUP";
                        break;
                    }
                case SQLCommand.ImportVoucher.GetMainParentId:
                    {
                        sQuery = "SELECT GROUP_ID FROM MASTER_LEDGER_GROUP WHERE LEDGER_GROUP=?LEDGER_GROUP";
                        break;
                    }
                case SQLCommand.ImportVoucher.GetNatureId:
                    {
                        sQuery = "SELECT NATURE_ID FROM MASTER_NATURE WHERE NATURE=?NATURE";
                        break;
                    }
                case SQLCommand.ImportVoucher.GetCountryId:
                    {
                        sQuery = "SELECT COUNTRY_ID\n" +
                                            "  FROM MASTER_COUNTRY\n" +
                                            " WHERE COUNTRY =?COUNTRY";
                        break;
                    }
                case SQLCommand.ImportVoucher.GetStateId:
                    {
                        sQuery = "SELECT STATE_ID\n" +
                                            "  FROM MASTER_STATE\n" +
                                            " WHERE STATE_NAME =?STATE_NAME";
                        break;
                    }
                case SQLCommand.ImportVoucher.GetSubLedgerId:
                    {
                        sQuery = "SELECT SUB_LEDGER_ID\n" +
                                            "  FROM MASTER_SUB_LEDGER\n" +
                                            " WHERE SUB_LEDGER_NAME =?SUB_LEDGER_NAME";
                        break;
                    }
                case SQLCommand.ImportVoucher.GetLegalEntityId:
                    {
                        sQuery = "SELECT CUSTOMERID FROM master_insti_perference WHERE SOCIETYNAME=?SocietyName";
                        break;
                    }
                case SQLCommand.ImportVoucher.GetFDAccountId:
                    {
                        sQuery = "SELECT FD_ACCOUNT_ID FROM FD_ACCOUNT WHERE FD_ACCOUNT_NUMBER=?FD_ACCOUNT_NUMBER";
                        break;
                    }
                case SQLCommand.ImportVoucher.GetStatisticsTypeId:
                    {
                        sQuery = "SELECT STATISTICS_TYPE_ID FROM MASTER_STATISTICS_TYPE WHERE STATISTICS_TYPE=?STATISTICS_TYPE";
                        break;
                    }
                case SQLCommand.ImportVoucher.GetGSTSlabClassId:
                    {
                        sQuery = "SELECT GST_ID FROM MASTER_GST_CLASS WHERE SLAB=?SLAB";
                        break;
                    }
                case SQLCommand.ImportVoucher.GetVendorId:
                    {
                        sQuery = "SELECT VENDOR_ID FROM ASSET_STOCK_VENDOR WHERE VENDOR=?VENDOR";
                        break;
                    }
                case SQLCommand.ImportVoucher.UpdateAutoIncrementNumber:
                    {
                        sQuery = "ALTER TABLE VOUCHER_MASTER_TRANS AUTO_INCREMENT = ?LAST_VOUCHER_ID;";
                        break;
                    }
                case SQLCommand.ImportVoucher.MapProjectLedger:
                    {
                        sQuery = "INSERT INTO PROJECT_LEDGER\n" +
                                "(PROJECT_ID, LEDGER_ID)\n" +
                                "VALUES(?PROJECT_ID,?LEDGER_ID) \n" +
                                " ON DUPLICATE KEY UPDATE PROJECT_ID =?PROJECT_ID, LEDGER_ID =?LEDGER_ID";
                        break;
                    }
                case SQLCommand.ImportVoucher.MapProjectDonor:
                    {
                        sQuery = "INSERT INTO PROJECT_DONOR\n" +
                                "  (PROJECT_ID, DONOR_ID)\n" +
                                "VALUES(?PROJECT_ID,?DONAUD_ID)\n" +
                                " ON DUPLICATE KEY UPDATE PROJECT_ID =?PROJECT_ID, DONOR_ID =?DONAUD_ID";
                        break;
                    }
                case SQLCommand.ImportVoucher.MapProjectCostcentre:
                    {
                        sQuery = "INSERT INTO PROJECT_COSTCENTRE\n" +
                                "  (PROJECT_ID, LEDGER_ID, COST_CENTRE_ID) VALUES (?PROJECT_ID, ?LEDGER_ID, ?COST_CENTRE_ID)\n" +
                                "ON DUPLICATE KEY UPDATE PROJECT_ID = ?PROJECT_ID, LEDGER_ID = ?LEDGER_ID, COST_CENTRE_ID = ?COST_CENTRE_ID";
                        break;
                    }
                case SQLCommand.ImportVoucher.MapCostCategory:
                    {
                        sQuery = "INSERT INTO COSTCATEGORY_COSTCENTRE(COST_CATEGORY_ID,COST_CENTRE_ID)VALUES (?COST_CENTRECATEGORY_ID,?COST_CENTRE_ID)\n" +
                                 "ON DUPLICATE KEY UPDATE COST_CATEGORY_ID=?COST_CENTRECATEGORY_ID,COST_CENTRE_ID=?COST_CENTRE_ID";
                        break;
                    }
                case SQLCommand.ImportVoucher.MapProjectPurpose:
                    {
                        sQuery = "INSERT INTO PROJECT_PURPOSE\n" +
                                "  (PROJECT_ID, CONTRIBUTION_ID, TRANS_MODE)\n" +
                                "VALUES\n" +
                                "  (?PROJECT_ID, ?CONTRIBUTION_ID, 'DR') ON DUPLICATE KEY UPDATE PROJECT_ID = ?PROJECT_ID, CONTRIBUTION_ID = ?CONTRIBUTION_ID, TRANS_MODE = 'DR'";
                        break;
                    }
                case SQLCommand.ImportVoucher.MapProjectBudgetLedger:
                    {
                        sQuery = "DELETE FROM PROJECT_BUDGET_LEDGER WHERE PROJECT_ID=?PROJECT_ID AND LEDGER_ID=?LEDGER_ID;\n" +
                                 "INSERT INTO PROJECT_BUDGET_LEDGER (PROJECT_ID, LEDGER_ID) VALUES(?PROJECT_ID, ?LEDGER_ID) \n" +
                                 "ON DUPLICATE KEY UPDATE PROJECT_ID =?PROJECT_ID, LEDGER_ID =?LEDGER_ID";
                        break;
                    }
                case SQLCommand.ImportVoucher.MapSubLedger:
                    {
                        sQuery = "DELETE FROM LEDGER_SUB_LEDGER WHERE LEDGER_ID=?LEDGER_ID AND SUB_LEDGER_ID=?SUB_LEDGER_ID;\n" +
                                 "INSERT INTO LEDGER_SUB_LEDGER (LEDGER_ID, SUB_LEDGER_ID)\n" +
                                 "VALUES (?LEDGER_ID, ?SUB_LEDGER_ID)";
                        break;
                    }
                case SQLCommand.ImportVoucher.DeleteLedgerBalance:
                    {
                        sQuery = "DELETE FROM LEDGER_BALANCE";
                        break;
                    }
                case SQLCommand.ImportVoucher.DeleteVoucherTrans:
                    {
                        sQuery = "DELETE FROM VOUCHER_TRANS WHERE VOUCHER_ID=?VOUCHER_ID AND BRANCH_ID=?BRANCH_OFFICE_ID AND LOCATION_ID=?LOCATION_ID";
                        //sQuery = "DELETE FROM VOUCHER_TRANS\n" +
                        //            " WHERE VOUCHER_ID IN\n" +
                        //            "       (SELECT VOUCHER_ID\n" +
                        //            "          FROM VOUCHER_MASTER_TRANS\n" +
                        //            "         WHERE VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO);";
                        break;
                    }
                case SQLCommand.ImportVoucher.DeleteFDVoucherMasterTrans:
                    {
                        sQuery = "DELETE FROM VOUCHER_MASTER_TRANS\n" +
                                " WHERE VOUCHER_SUB_TYPE = 'FD'\n" +
                                "   AND STATUS = 1\n" +
                                "   AND BRANCH_ID = ?BRANCH_OFFICE_ID AND LOCATION_ID=?LOCATION_ID\n" +
                                "   AND PROJECT_ID IN (?PROJECT_ID);";
                        break;

                    }
                case SQLCommand.ImportVoucher.FetchFDTransaction:
                    {
                        sQuery = "SELECT VOUCHER_DATE, VOUCHER_ID, PROJECT_ID FROM VOUCHER_MASTER_TRANS\n" +
                                " WHERE VOUCHER_SUB_TYPE ='FD'\n" +
                                "   AND STATUS = 1\n" +
                                "   AND BRANCH_ID = ?BRANCH_OFFICE_ID AND LOCATION_ID=?LOCATION_ID\n" +
                                "   AND PROJECT_ID IN (?PROJECT_ID);";
                        break;
                    }
                case SQLCommand.ImportVoucher.DeleteFDVoucherTrans:
                    {
                        sQuery = "DELETE FROM VOUCHER_TRANS\n" +
                                " WHERE VOUCHER_ID IN (SELECT VOUCHER_ID\n" +
                                "                        FROM VOUCHER_MASTER_TRANS VMT\n" +
                                "                       WHERE VMT.VOUCHER_SUB_TYPE = 'FD'\n" +
                                "                         AND VMT.STATUS = 1\n" +
                                "                         AND VMT.BRANCH_ID = ?BRANCH_OFFICE_ID AND VMT.LOCATION_ID=?LOCATION_ID\n" +
                                "                         AND PROJECT_ID IN (?PROJECT_ID));";
                        break;
                    }
                case SQLCommand.ImportVoucher.DeleteVoucherMasterTrans:
                    {
                        sQuery = "DELETE FROM VOUCHER_MASTER_TRANS WHERE VOUCHER_ID=?VOUCHER_ID AND BRANCH_ID=?BRANCH_OFFICE_ID AND LOCATION_ID=?LOCATION_ID";
                        //sQuery = "DELETE FROM VOUCHER_CC_TRANS\n" +
                        //            " WHERE VOUCHER_ID IN\n" +
                        //            "       (SELECT VOUCHER_ID\n" +
                        //            "          FROM VOUCHER_MASTER_TRANS\n" +
                        //            "         WHERE VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO);";
                        break;
                    }
                case SQLCommand.ImportVoucher.DeleteVoucherCostCentre:
                    {
                        sQuery = "DELETE FROM VOUCHER_CC_TRANS WHERE VOUCHER_ID=?VOUCHER_ID AND BRANCH_ID=?BRANCH_OFFICE_ID AND LOCATION_ID=?LOCATION_ID";
                        //sQuery = "DELETE FROM VOUCHER_MASTER_TRANS\n" +
                        //            " WHERE VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO";
                        break;
                    }
                case SQLCommand.ImportVoucher.DeleteVoucherSubLedger:
                    {
                        sQuery = "DELETE FROM VOUCHER_SUB_LEDGER_TRANS WHERE VOUCHER_ID=?VOUCHER_ID AND BRANCH_ID=?BRANCH_OFFICE_ID AND LOCATION_ID=?LOCATION_ID";
                        break;
                    }
                case SQLCommand.ImportVoucher.UpdateDataSynStatus:
                    {
                        // By Alex. To update the date time, even when the status is faild. 
                        // Received = 1,InProgress = 2, Closed = 3,Failed = 4.
                        sQuery = "UPDATE DATASYNC_TASK\n" +
                                "   SET STARTED_ON   = IF(?STATUS=" + (int)DataSyncMailType.InProgress + " OR ?STATUS=" + (int)DataSyncMailType.Failed + " ,NOW(),STARTED_ON)," +
                                "       COMPLETED_ON = IF(?STATUS=" + (int)DataSyncMailType.Closed + " OR ?STATUS=" + (int)DataSyncMailType.Failed + ",NOW(),COMPLETED_ON)," +
                                "       TRANS_DATE_FROM= ?DATE_FROM, \n" +
                                "       TRANS_DATE_TO = ?DATE_TO, \n" +
                                "       STATUS       = ?STATUS,\n" +
                                "       REMARKS      = ?REMARKS\n" +
                                " WHERE HEAD_OFFICE_ID = ?HEAD_OFFICE_ID AND BRANCH_OFFICE_ID = \n" +
                                " ?BRANCH_OFFICE_ID AND XML_FILENAME = ?XML_FILENAME";
                        break;
                    }
                case SQLCommand.ImportVoucher.UpdateSubBranchDsyncStatus:
                    {
                        // By Alex. To update the date time, even when the status is faild. 
                        // Received = 1,InProgress = 2, Closed = 3,Failed = 4.
                        sQuery = "UPDATE DATASYNC_TASK\n" +
                                    "   SET STARTED_ON   = IF(?STATUS=" + (int)DataSyncMailType.InProgress + " OR ?STATUS=" + (int)DataSyncMailType.Failed + " ,NOW(),STARTED_ON)," +
                                    "       COMPLETED_ON = IF(?STATUS=" + (int)DataSyncMailType.Closed + "OR ?STATUS=" + (int)DataSyncMailType.Failed + ",NOW(),COMPLETED_ON)," +
                                    "       TRANS_DATE_FROM= ?DATE_FROM, \n" +
                                    "       TRANS_DATE_TO = ?DATE_TO, \n" +
                                    "       STATUS       = ?STATUS,\n" +
                                    "       REMARKS      = ?REMARKS\n" +
                                    " WHERE BRANCH_OFFICE_ID = \n" +
                                    " ?BRANCH_OFFICE_ID AND XML_FILENAME = ?XML_FILENAME";
                        break;
                    }
                // ,IS_MULTI_CURRENCY,CURRENCY_COUNTRY_ID,CONTRIBUTION_AMOUNT,EXCHANGE_RATE,ACTUAL_AMOUNT
                //"   ?IS_MULTI_CURRENCY, \n" +
                //       "   ?CURRENCY_COUNTRY_ID, \n" +
                //       "   ?CONTRIBUTION_AMOUNT, \n" +
                //       "   ?EXCHANGE_RATE, \n" +
                //       "   ?ACTUAL_AMOUNT)";
                case SQLCommand.ImportVoucher.InsertFDAccount:
                    {
                        //FD_ACCOUNT_ID, \n"+
                        sQuery = "INSERT INTO FD_ACCOUNT\n" +
                                "  (FD_ACCOUNT_ID, \n" +
                                "   FD_ACCOUNT_NUMBER,\n" +
                                "   FD_SCHEME,\n" +
                                "   PROJECT_ID,\n" +
                                "   LEDGER_ID,\n" +
                                "   BANK_ID,\n" +
                                "   BANK_LEDGER_ID,\n" +
                                "   FD_VOUCHER_ID,\n" +
                                "   AMOUNT,\n" +
                                "   TRANS_TYPE,\n" +
                                "   RECEIPT_NO,\n" +
                                "   ACCOUNT_HOLDER,\n" +
                                "   INVESTMENT_DATE,\n" +
                                "   TRANS_MODE,\n" +
                                "   INTEREST_TYPE,\n" +
                                "   MATURED_ON,\n" +
                                "   INTEREST_RATE,\n" +
                                "   BRANCH_ID, \n" +
                                "   LOCATION_ID,\n" +
                                "   INTEREST_AMOUNT,FD_SUB_TYPES,\n" +
                                "   STATUS,NOTES)\n" +
                                "   VALUES\n" +
                                "  (?FD_ACCOUNT_ID, \n" +
                                "   ?FD_ACCOUNT_NUMBER,\n" +
                                "   ?FD_SCHEME,\n" +
                                "   ?PROJECT_ID,\n" +
                                "   ?LEDGER_ID,\n" +
                                "   ?BANK_ID,\n" +
                                "   ?BANK_LEDGER_ID,\n" +
                                "   ?FD_VOUCHER_ID,\n" +
                                "   ?AMOUNT,\n" +
                                "   ?TRANS_TYPE,\n" +
                                "   ?RECEIPT_NO,\n" +
                                "   ?ACCOUNT_HOLDER,\n" +
                                "   ?INVESTMENT_DATE,\n" +
                                "   ?TRANS_MODE,\n" +
                                "   ?INTEREST_TYPE,\n" +
                                "   ?MATURED_ON,\n" +
                                "   ?INTEREST_RATE,\n" +
                                "   ?BRANCH_OFFICE_ID,\n " +
                                "   ?LOCATION_ID,\n" +
                                "   ?INTEREST_AMOUNT,?FD_SUB_TYPES,\n" +
                                "   ?STATUS,\n" +
                                "   ?NOTES)";

                        break;
                    }

                case SQLCommand.ImportVoucher.InsertFDAcountForSplitProject:
                    {
                        sQuery = "INSERT INTO FD_ACCOUNT(\n" +
                                "   FD_ACCOUNT_NUMBER,\n" +
                                "   FD_SCHEME,\n" +
                                "   PROJECT_ID,\n" +
                                "   LEDGER_ID,\n" +
                                "   BANK_ID,\n" +
                                "   BANK_LEDGER_ID,\n" +
                                "   FD_VOUCHER_ID,\n" +
                                "   AMOUNT,\n" +
                                "   TRANS_TYPE,\n" +
                                "   RECEIPT_NO,\n" +
                                "   ACCOUNT_HOLDER,\n" +
                                "   INVESTMENT_DATE,\n" +
                                "   TRANS_MODE,\n" +
                                "   INTEREST_TYPE,\n" +
                                "   MATURED_ON,\n" +
                                "   INTEREST_RATE,\n" +
                                "   BRANCH_ID, \n" +
                                "   LOCATION_ID,\n" +
                                "   INTEREST_AMOUNT,FD_SUB_TYPES,\n" +
                                "   MF_FOLIO_NO, MF_SCHEME_NAME, MF_NAV_PER_UNIT, MF_NO_OF_UNITS, MF_MODE_OF_HOLDING,\n" +
                                "   STATUS,NOTES)\n" +
                                "   VALUES(\n" +
                                "   ?FD_ACCOUNT_NUMBER,\n" +
                                "   ?FD_SCHEME,\n" +
                                "   ?PROJECT_ID,\n" +
                                "   ?LEDGER_ID,\n" +
                                "   ?BANK_ID,\n" +
                                "   ?BANK_LEDGER_ID,\n" +
                                "   ?FD_VOUCHER_ID,\n" +
                                "   ?AMOUNT,\n" +
                                "   ?TRANS_TYPE,\n" +
                                "   ?RECEIPT_NO,\n" +
                                "   ?ACCOUNT_HOLDER,\n" +
                                "   ?INVESTMENT_DATE,\n" +
                                "   ?TRANS_MODE,\n" +
                                "   ?INTEREST_TYPE,\n" +
                                "   ?MATURED_ON,\n" +
                                "   ?INTEREST_RATE,\n" +
                                "   ?BRANCH_OFFICE_ID,\n " +
                                "   ?LOCATION_ID,\n" +
                                "   ?INTEREST_AMOUNT,?FD_SUB_TYPES,\n" +
                                "   ?MF_FOLIO_NO, ?MF_SCHEME_NAME, ?MF_NAV_PER_UNIT, ?MF_NO_OF_UNITS, ?MF_MODE_OF_HOLDING,\n" +
                                "   ?STATUS,?NOTES)";
                        break;
                    }
                case SQLCommand.ImportVoucher.InsertFDRenewal:
                    {
                        sQuery = @"INSERT INTO FD_RENEWAL
                                  (FD_ACCOUNT_ID,
                                   INTEREST_LEDGER_ID,
                                   BANK_LEDGER_ID,
                                   FD_INTEREST_VOUCHER_ID,
                                   FD_VOUCHER_ID,
                                   INTEREST_AMOUNT,
                                   WITHDRAWAL_AMOUNT,
                                   REINVESTED_AMOUNT,
                                   RENEWAL_DATE,
                                   MATURITY_DATE, CLOSED_DATE,
                                   INTEREST_RATE,
                                   INTEREST_TYPE,
                                   RECEIPT_NO, 
                                   RENEWAL_TYPE,
                                   BRANCH_ID,
                                   LOCATION_ID,
                                   STATUS,FD_TYPE, TDS_AMOUNT, CHARGE_MODE, CHARGE_AMOUNT, CHARGE_LEDGER_ID, FD_TRANS_MODE) 
                                VALUES
                                  (?FD_ACCOUNT_ID,
                                   ?INTEREST_LEDGER_ID,
                                   ?BANK_LEDGER_ID,
                                   ?FD_INTEREST_VOUCHER_ID,
                                   ?FD_VOUCHER_ID,
                                   ?INTEREST_AMOUNT,
                                   ?WITHDRAWAL_AMOUNT,
                                   ?REINVESTED_AMOUNT,
                                   ?RENEWAL_DATE,
                                   ?MATURITY_DATE, ?CLOSED_DATE,
                                   ?INTEREST_RATE,
                                   ?INTEREST_TYPE,
                                   ?RECEIPT_NO,
                                   ?RENEWAL_TYPE,
                                   ?BRANCH_OFFICE_ID,
                                   ?LOCATION_ID,
                                   ?STATUS,?FD_TYPE, ?TDS_AMOUNT, ?CHARGE_MODE, ?CHARGE_AMOUNT, ?CHARGE_LEDGER_ID, ?FD_TRANS_MODE)";
                        break;
                    }
                case SQLCommand.ImportVoucher.UpdateLedgerInterestLedger:
                    { //11/06/2021, to update ledger as interest ledger
                        sQuery = "UPDATE MASTER_LEDGER SET IS_BANK_INTEREST_LEDGER=1 WHERE LEDGER_ID IN (?INTEREST_LEDGER_ID)";
                        break;
                    }
                case SQLCommand.ImportVoucher.DeleteFDAccount:
                    {
                        sQuery = "DELETE FROM FD_ACCOUNT WHERE PROJECT_ID IN(?PROJECT_ID) AND BRANCH_ID=?BRANCH_OFFICE_ID AND LOCATION_ID=?LOCATION_ID;";
                        //sQuery = "DELETE FROM FD_ACCOUNT WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID AND BRANCH_ID=?BRANCH_OFFICE_ID";
                        break;
                    }
                case SQLCommand.ImportVoucher.DeleteFDRenewal:
                    {
                        sQuery = "DELETE FROM FD_RENEWAL\n" +
                                " WHERE FD_ACCOUNT_ID IN (SELECT FD_ACCOUNT_ID\n" +
                                "                           FROM FD_ACCOUNT\n" +
                                "                          WHERE PROJECT_ID IN (?PROJECT_ID)\n" +
                                "                            AND BRANCH_ID =?BRANCH_OFFICE_ID AND LOCATION_ID=?LOCATION_ID);";
                        //sQuery = "DELETE FROM FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID AND BRANCH_ID=?BRANCH_OFFICE_ID";
                        break;
                    }
                case SQLCommand.ImportVoucher.FetchOPBalanceDate:
                    {
                        sQuery = "SELECT MIN(BALANCE_DATE) AS BALANCE_DATE from ledger_balance where TRANS_FLAG='OP';";
                        break;
                    }
                case SQLCommand.ImportVoucher.UpdateOPBalanceDate:
                    {
                        // sQuery = "UPDATE LEDGER_BALANCE SET BALANCE_DATE=?BALANCE_DATE WHERE TRANS_FLAG='OP';";

                        sQuery = "\n" +
                                "UPDATE LEDGER_BALANCE LB,\n" +
                                "       (SELECT MIN(BALANCE_DATE) AS MIN_BALANCE_DATE FROM LEDGER_BALANCE) T\n" +
                                "   SET LB.BALANCE_DATE = T.MIN_BALANCE_DATE\n" +
                                " WHERE LB.TRANS_FLAG = 'OP';";

                        break;
                    }
                case SQLCommand.ImportVoucher.UpdateOPBalanceDateByDate:
                    {
                        sQuery = "UPDATE LEDGER_BALANCE SET BALANCE_DATE = ?BALANCE_DATE WHERE TRANS_FLAG = 'OP';";

                        break;
                    }
                case SQLCommand.ImportVoucher.DeleteOPBalanceDateMoreThanOne:
                    {
                        sQuery = "DELETE LB FROM LEDGER_BALANCE LB\n" +
                                    "INNER JOIN (SELECT PROJECT_ID, LEDGER_ID, MAX(BALANCE_DATE) as MIN_BALANCE_DATE\n" +
                                    "FROM LEDGER_BALANCE WHERE TRANS_FLAG = 'OP' GROUP BY PROJECT_ID, LEDGER_ID\n" +
                                    "HAVING COUNT(BALANCE_DATE) > 1) T ON\n" +
                                    "T.PROJECT_ID = LB.PROJECT_ID AND T.LEDGER_ID = LB.LEDGER_ID AND T.MIN_BALANCE_DATE = LB.BALANCE_DATE\n" +
                                    "WHERE LB.TRANS_FLAG = 'OP';";

                        break;
                    }
                case SQLCommand.ImportVoucher.UpdateFDLedgerOpeningBalanceByProject: // 17/01/2025, to change un_columns from amount_fc, we have updated fd amount as amount_fc
                    {
                        sQuery = "INSERT INTO LEDGER_BALANCE (BALANCE_DATE, PROJECT_ID, LEDGER_ID, AMOUNT, AMOUNT_FC, TRANS_MODE, TRANS_FC_MODE, TRANS_FLAG, BRANCH_ID)\n" +
                                    "SELECT ?BALANCE_DATE AS BALANCE_DATE, PROJECT_ID, LEDGER_ID, SUM(AMOUNT) AMOUNT, SUM(AMOUNT) AMOUNT_FC, 'DR' TRANS_MODE, 'DR' TRANS_MODE, 'OP' TRANS_FLAG, BRANCH_ID\n" +
                                    "FROM FD_ACCOUNT WHERE TRANS_TYPE = 'OP' AND PROJECT_ID =?PROJECT_ID GROUP BY PROJECT_ID, LEDGER_ID\n" +
                                    "ON DUPLICATE KEY UPDATE BALANCE_DATE = VALUES(BALANCE_DATE), PROJECT_ID = VALUES(PROJECT_ID),\n" +
                                    "LEDGER_ID = VALUES(LEDGER_ID), BRANCH_ID = VALUES(BRANCH_ID)";
                        break;
                    }
                case SQLCommand.ImportVoucher.DeleteFDLedgerOpeningBalanceByProject:
                    {
                        sQuery = "DELETE FROM LEDGER_BALANCE\n" +
                                 "WHERE TRANS_FLAG = '" + FDTypes.OP.ToString() + "' AND PROJECT_ID IN (?PROJECT_ID) AND \n" +
                                 "LEDGER_ID IN (SELECT LEDGER_ID FROM MASTER_LEDGER WHERE GROUP_ID =" + (Int32)FixedLedgerGroup.FixedDeposit + ");";
                        break;
                    }
                case SQLCommand.ImportVoucher.IsGeneralLedger:
                    {
                        sQuery = "SELECT LEDGER_ID FROM MASTER_LEDGER \n" +
                                    "WHERE LEDGER_ID=?LEDGER_ID AND GROUP_ID NOT IN(12,13,14)";

                        break;
                    }
                case SQLCommand.ImportVoucher.FetchBranchProjects:
                    {
                        sQuery = "SELECT \n" +
                                "      MP.PROJECT,\n" +
                                "       0 AS STATUS\n" +
                            //"       DIVISION_ID,\n" +
                            //"       ACCOUNT_DATE,\n" +
                            //"       DATE_STARTED,\n" +
                            //"       DATE_CLOSED,\n" +
                            //"       DESCRIPTION,\n" +
                            //"       NOTES,\n" +
                            //"       PROJECT_CATEGORY_ID,\n" +
                            //"       DELETE_FLAG,\n" +
                            //"       CUSTOMERID,\n" +
                            //"       CONTRIBUTION_ID\n" +
                                "  FROM MASTER_PROJECT MP\n" +
                                " INNER JOIN PROJECT_BRANCH PB\n" +
                                "    ON MP.PROJECT_ID = PB.PROJECT_ID\n" +
                                " WHERE BRANCH_ID = ?BRANCH_OFFICE_ID;";
                        break;
                    }
                case SQLCommand.ImportVoucher.FetchProjects:
                    {
                        sQuery = "SELECT MP.PROJECT_ID, MP.PROJECT \n" +
                                " FROM MASTER_PROJECT MP";
                        break;
                    }
                case SQLCommand.ImportVoucher.GetBudgetId:
                    {
                        sQuery = "SELECT BM.BUDGET_ID\n" +
                                  " FROM BUDGET_MASTER BM\n" +
                                  "INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID\n" +
                                   "WHERE BM.BRANCH_ID = ?BRANCH_ID\n" +
                                   " AND BP.PROJECT_ID IN (?PROJECT_ID) AND BM.DATE_FROM=?DATE_FROM AND BM.DATE_TO=?DATE_TO AND BM.BUDGET_TYPE_ID=?BUDGET_TYPE_ID";
                        break;
                    }
                case SQLCommand.ImportVoucher.InsertBudgetMaster:
                    {
                        sQuery = "INSERT INTO BUDGET_MASTER\n" +
                                 "  (BUDGET_NAME, BUDGET_TYPE_ID, BUDGET_LEVEL_ID, IS_MONTH_WISE, DATE_FROM, DATE_TO, IS_ACTIVE, REMARKS, BUDGET_ACTION, BRANCH_ID)\n" +
                                 "VALUES\n" +
                                 "  (?BUDGET_NAME, ?BUDGET_TYPE_ID, ?BUDGET_LEVEL_ID,?IS_MONTH_WISE, ?DATE_FROM, ?DATE_TO, ?IS_ACTIVE, ?REMARKS, ?BUDGET_ACTION, ?BRANCH_ID)";

                        break;
                    }
                case SQLCommand.ImportVoucher.UpdateBudgetMaster:
                    {
                        sQuery = "UPDATE BUDGET_MASTER BM\n" +
                                 "INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID= BM.BUDGET_ID\n" +
                                 "SET BUDGET_NAME=?BUDGET_NAME, BUDGET_TYPE_ID=?BUDGET_TYPE_ID, BUDGET_LEVEL_ID=?BUDGET_LEVEL_ID,\n" +
                                 "DATE_FROM=?DATE_FROM, DATE_TO=?DATE_TO, IS_MONTH_WISE=?IS_MONTH_WISE, IS_ACTIVE =?IS_ACTIVE,REMARKS=?REMARKS\n" +
                                 " WHERE BRANCH_ID=?BRANCH_ID AND DATE_FROM=?DATE_FROM AND DATE_TO=?DATE_TO AND BP.PROJECT_ID IN (?PROJECT_ID) AND BUDGET_TYPE_ID=?BUDGET_TYPE_ID";
                        break;
                    }
                case SQLCommand.ImportVoucher.InsertBudgetLedger:
                    {
                        sQuery = "INSERT INTO BUDGET_LEDGER\n" +
                                "   (BUDGET_ID,LEDGER_ID,PROPOSED_AMOUNT,APPROVED_AMOUNT,TRANS_MODE, NARRATION, HO_NARRATION)\n" +
                                "VALUES\n" +
                                "   (?BUDGET_ID,?LEDGER_ID,?PROPOSED_AMOUNT,?APPROVED_AMOUNT,?TRANS_MODE, ?NARRATION, ?HO_NARRATION)";
                        break;
                    }
                case SQLCommand.ImportVoucher.InsertBudgetStatisticsDetails:
                    { //18/06/2021
                        sQuery = "INSERT INTO BUDGET_STATISTICS_DETAIL \n" +
                                "(BUDGET_ID,STATISTICS_TYPE_ID,TOTAL_COUNT)\n" +
                                "VALUES(?BUDGET_ID,?STATISTICS_TYPE_ID,?TOTAL_COUNT)";
                        break;
                    }
                case SQLCommand.ImportVoucher.InsertStatisticsType:
                    { //18/06/2021
                        sQuery = "INSERT INTO MASTER_STATISTICS_TYPE (STATISTICS_TYPE) VALUES (?STATISTICS_TYPE)";
                        break;
                    }

                case SQLCommand.ImportVoucher.InsertBudgetSubLedger:
                    {
                        sQuery = "INSERT INTO BUDGET_SUB_LEDGER\n" +
                                "   (BUDGET_ID,LEDGER_ID,SUB_LEDGER_ID,PROPOSED_AMOUNT,APPROVED_AMOUNT,TRANS_MODE, NARRATION, HO_NARRATION)\n" +
                                "VALUES\n" +
                                "   (?BUDGET_ID,?LEDGER_ID,?SUB_LEDGER_ID,?PROPOSED_AMOUNT,?APPROVED_AMOUNT,?TRANS_MODE, ?NARRATION, ?HO_NARRATION)";
                        break;
                    }
                case SQLCommand.ImportVoucher.DeleteBudgetLedger:
                    {
                        sQuery = "DELETE FROM BUDGET_LEDGER WHERE BUDGET_ID=?BUDGET_ID";
                        break;
                    }
                case SQLCommand.ImportVoucher.DeleteBudgetStatisticsDetails:
                    { //18/06/2021
                        sQuery = "DELETE FROM BUDGET_STATISTICS_DETAIL WHERE BUDGET_ID=?BUDGET_ID";
                        break;
                    }
                case SQLCommand.ImportVoucher.DeleteBudgetSubLedger:
                    {
                        sQuery = "DELETE FROM BUDGET_SUB_LEDGER WHERE BUDGET_ID=?BUDGET_ID";
                        break;
                    }
                case SQLCommand.ImportVoucher.InsertBudgetProject:
                    {
                        sQuery = "INSERT INTO BUDGET_PROJECT (BUDGET_ID,PROJECT_ID) VALUES (?BUDGET_ID,?PROJECT_ID)";
                        break;
                    }
                case SQLCommand.ImportVoucher.DeleteBudgetProject:
                    {
                        sQuery = "DELETE FROM BUDGET_PROJECT WHERE BUDGET_ID=?BUDGET_ID";
                        break;
                    }

                case SQLCommand.ImportVoucher.GetBudgetIdForSplitProject:
                    { //17/06/2021
                        sQuery = "SELECT GROUP_CONCAT(DISTINCT BM.BUDGET_ID) AS BUDGET_IDs, GROUP_CONCAT(BP.PROJECT_ID) AS PROJECT_IDs\n" +
                                  "FROM BUDGET_MASTER BM INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID\n" +
                                  "WHERE ( (DATE_FROM >= ?DATE_FROM AND DATE_TO <=?DATE_TO) OR\n" +
                                  "(BM.BUDGET_TYPE_ID = " + (int)BudgetType.BudgetByCalendarYear + " AND DATE_FROM >= ?YEAR_FROM AND DATE_TO <= ?YEAR_TO))\n" +
                                  "GROUP BY BM.BUDGET_ID HAVING Find_In_Set(?PROJECT_ID, PROJECT_IDs ) > 0";
                        break;
                    }
                case SQLCommand.ImportVoucher.DeleteBudgetForSplitProject:
                    { //17/06/2021
                        sQuery = "DELETE FROM BUDGET_MASTER WHERE BUDGET_ID IN (?BUDGET_ID);\n" +
                                  "DELETE FROM BUDGET_PROJECT WHERE BUDGET_ID IN (?BUDGET_ID);\n" +
                                  "DELETE FROM BUDGET_LEDGER WHERE BUDGET_ID IN (?BUDGET_ID);\n" +
                                  "DELETE FROM BUDGET_STATISTICS_DETAIL WHERE BUDGET_ID IN (?BUDGET_ID);";
                        break;
                    }
                case SQLCommand.ImportVoucher.IsAuditVouchersLocked:
                    {
                        sQuery = " SELECT LOCK_TRANS_ID FROM MASTER_LOCK_TRANS WHERE PROJECT_ID IN (?PROJECT_ID)\n" +
                                    " AND ((?DATE_FROM BETWEEN DATE_FROM AND DATE_TO)  OR (?DATE_TO BETWEEN DATE_FROM AND DATE_TO))";
                        break;
                    }

                case SQLCommand.ImportVoucher.DeleteGSTInvoicesByProjectSplit:
                    {
                        sQuery = @"DELETE FROM VOUCHER_GST_INVOICE WHERE GST_INVOICE_ID IN (SELECT GST_INVOICE_ID FROM GST_INVOICE_MASTER 
                                       WHERE BOOKING_VOUCHER_ID IN (SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS WHERE PROJECT_ID IN (?PROJECT_ID) AND VOUCHER_DATE BETWEEN DATE_FROM=?DATE_FROM AND DATE_TO=?DATE_TO) );
                                  DELETE FROM GST_INVOICE_MASTER_DETAILS WHERE IN (SELECT GST_INVOICE_ID FROM GST_INVOICE_MASTER 
                                       WHERE BOOKING_VOUCHER_ID IN (SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS WHERE PROJECT_ID IN (?PROJECT_ID) AND VOUCHER_DATE BETWEEN DATE_FROM=?DATE_FROM AND DATE_TO=?DATE_TO) );
                                  DELETE FROM GST_INVOICE_MASTER 
                                       WHERE BOOKING_VOUCHER_ID IN (SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS WHERE PROJECT_ID IN (?PROJECT_ID) AND VOUCHER_DATE BETWEEN DATE_FROM=?DATE_FROM AND DATE_TO=?DATE_TO) )";
                        break;
                    }
                case SQLCommand.ImportVoucher.InsertGSTInvoiceMaster:
                    {
                        sQuery = @"INSERT INTO GST_INVOICE_MASTER (GST_VENDOR_INVOICE_NO, BOOKING_VOUCHER_ID, BOOKING_VOUCHER_TYPE, GST_VENDOR_INVOICE_DATE, GST_VENDOR_INVOICE_TYPE, GST_VENDOR_ID, 
                                TRANSPORT_MODE, VEHICLE_NUMBER, SUPPLY_DATE, SUPPLY_PLACE, BILLING_NAME, BILLING_GST_NO, BILLING_ADDRESS, BILLING_STATE_ID, BILLING_COUNTRY_ID,
                                SHIPPING_NAME, SHIPPING_GST_NO, SHIPPING_ADDRESS, SHIPPING_STATE_ID, SHIPPING_COUNTRY_ID, CHEQUE_IN_FAVOUR, 
                                TOTAL_AMOUNT, TOTAL_CGST_AMOUNT, TOTAL_SGST_AMOUNT, TOTAL_IGST_AMOUNT,
                                IS_REVERSE_CHARGE, REVERSE_CHARGE_AMOUNT, STATUS)
                                VALUES (?GST_VENDOR_INVOICE_NO, ?BOOKING_VOUCHER_ID, ?BOOKING_VOUCHER_TYPE, ?GST_VENDOR_INVOICE_DATE, ?GST_VENDOR_INVOICE_TYPE, ?GST_VENDOR_ID,
                                ?TRANSPORT_MODE, ?VEHICLE_NUMBER, ?SUPPLY_DATE, ?SUPPLY_PLACE, ?BILLING_NAME, ?BILLING_GST_NO, ?BILLING_ADDRESS, 
                                IF(?BILLING_STATE_ID=0, null,?BILLING_STATE_ID), IF(?BILLING_COUNTRY_ID=0, null, ?BILLING_COUNTRY_ID),
                                ?SHIPPING_NAME, ?SHIPPING_GST_NO, ?SHIPPING_ADDRESS, IF(?SHIPPING_STATE_ID=0, null, ?SHIPPING_STATE_ID), 
                                IF(?SHIPPING_COUNTRY_ID=0, null, ?SHIPPING_COUNTRY_ID), 
                                ?CHEQUE_IN_FAVOUR, ?TOTAL_AMOUNT, ?TOTAL_CGST_AMOUNT, ?TOTAL_SGST_AMOUNT, 
                                ?TOTAL_IGST_AMOUNT, ?IS_REVERSE_CHARGE, ?REVERSE_CHARGE_AMOUNT,  ?STATUS)";
                        break;
                    }
                case SQLCommand.ImportVoucher.InsertGSTInvoiceMasterLedgerDetail:
                    {
                        sQuery = @"INSERT INTO GST_INVOICE_MASTER_DETAILS (GST_INVOICE_ID, LEDGER_ID, LEDGER_GST_CLASS_ID, AMOUNT, TRANS_MODE, GST_HSN_SAC_CODE,
                                QUANTITY, UNIT_MEASUREMENT, UNIT_AMOUNT, DISCOUNT,  
                                CGST, SGST, IGST, BRANCH_ID)
                                VALUES (?GST_INVOICE_ID, ?LEDGER_ID, ?LEDGER_GST_CLASS_ID, ?AMOUNT, ?TRANS_MODE, ?GST_HSN_SAC_CODE,  
                                ?QUANTITY, ?UNIT_MEASUREMENT, ?UNIT_AMOUNT, ?DISCOUNT,
                                ?CGST, ?SGST, ?IGST, ?BRANCH_ID)";
                        break;
                    }
                case SQLCommand.ImportVoucher.InsertGSTInvoiceVoucher:
                    {
                        sQuery = @"INSERT INTO VOUCHER_GST_INVOICE (GST_INVOICE_ID, AMOUNT, VOUCHER_ID) VALUES(?GST_INVOICE_ID, ?AMOUNT, ?VOUCHER_ID)";
                        break;
                    }
                case SQLCommand.ImportVoucher.FetchMasterByBranchLocationVoucherId:
                    {
                        sQuery = @"SELECT VOUCHER_ID, MP.PROJECT, '' AS LOCATION_NAME, VOUCHER_DATE, VOUCHER_NO, VOUCHER_TYPE, VOUCHER_SUB_TYPE, STATUS
                                FROM VOUCHER_MASTER_TRANS MT 
                                INNER JOIN MASTER_PROJECT MP ON MT.PROJECT_ID = MP.PROJECT_ID 
                                -- LEFT JOIN BRANCH_LOCATION BL ON BL.LOCATION_ID = MT.LOCATION_ID AND BL.LOCATION_ID=?LOCATION_ID
                                WHERE MT.BRANCH_ID = ?BRANCH_ID AND MT.LOCATION_ID = ?LOCATION_ID AND MT.VOUCHER_ID = ?VOUCHER_ID";
                        break;
                    }
            }
            return sQuery;
        }
    }
}
