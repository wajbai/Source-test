using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class AcMePlusMigrationSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.AcMePlusMigration).FullName)
            {
                query = GetSettingSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        #region SQL Script
        private string GetSettingSQL()
        {
            string Query = string.Empty;
            SQLCommand.AcMePlusMigration sqlCommandId = (SQLCommand.AcMePlusMigration)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {

                #region Removing Old Data
                case SQLCommand.AcMePlusMigration.ClearData:
                    {
                        //-----------------------Note: In case of new table is added that must be added appropriately ------------------------------------
                        Query = @"DELETE FROM PROJECT_COSTCENTRE;
                            DELETE FROM USER_INFO WHERE USER_ID <> 1;
                            DELETE FROM PROJECT_DONOR;
                            DELETE FROM PROJECT_LEDGER;
                            DELETE FROM PROJECT_PURPOSE;
                            DELETE FROM PROJECT_VOUCHER;
                            DELETE FROM PROJECT_CATEGORY_LEDGER;
                            DELETE FROM VOUCHER_REFERENCE;
                            DELETE FROM VOUCHER_CC_TRANS;
                            DELETE FROM VOUCHER_TRANS;
                            DELETE FROM LEDGER_BALANCE;
                            DELETE FROM VOUCHER_MASTER_TRANS;
                            DELETE FROM ALLOT_FUND;
                            DELETE FROM BUDGET_LEDGER;
                            DELETE FROM BUDGET_PROJECT;
                            DELETE FROM BUDGET_MASTER;
                            DELETE FROM MASTER_EXECUTIVE_COMMITTEE;
                            DELETE FROM MASTER_DONAUD;
                            DELETE FROM PR_STAFF_PERFORMANCE;
                            DELETE FROM ACCOUNTING_YEAR;
                            DELETE FROM FD_RENEWAL;
                            DELETE FROM FD_ACCOUNT;
                            DELETE FROM MASTER_BANK_ACCOUNT;
                            DELETE FROM MASTER_BANK;
                            DELETE FROM TDS_BOOKING;
                            DELETE FROM TDS_BOOKING_DETAIL;
                            DELETE FROM TDS_CREDTIORS_PROFILE;
                            DELETE FROM TDS_TAX_RATE;
                            DELETE FROM TDS_POLICY;
                            DELETE FROM TDS_DEDUCTEE_TYPE;
                            DELETE FROM TDS_DEDUCTION;
                            DELETE FROM TDS_DEDUCTION_DETAIL;
                            DELETE FROM TDS_DEDUCTION_DETAIL;
                            DELETE FROM TDS_DUTY_TAXTYPE;
                            DELETE FROM TDS_NATURE_PAYMENT;
                            DELETE FROM TDS_PARTY_PAYMENT;
                            DELETE FROM TDS_PAYMENT;
                            DELETE FROM TDS_PAYMENT_DETAIL;
                            DELETE FROM TDS_POLICY;
                            DELETE FROM TDS_SECTION;
                            DELETE FROM TDS_TAX_RATE;

                            DELETE FROM PAYROLL_VOUCHER;

                           -- PayRoll,Asset and Stock should not be executed unless those modules used by the client
                           -- DELETE FROM prComponent;
                           -- DELETE FROM PAYROLL_PROJECT;
                           -- DELETE FROM PAYROLL_LEDGER;
                           -- DELETE FROM PRPROJECT_STAFF;
                           -- DELETE FROM ASSET_LEDGER;
                           -- DELETE FROM ASSET_INSURANCE_MASTER_DETAIL;
                           -- DELETE FROM ASSET_INSURANCE_MASTER;
                           -- DELETE FROM ASSET_INSURANCE_MASTER_DETAIL;
                           -- DELETE FROM ASSET_DEPRE_DETAIL;
                           -- DELETE FROM ASSET_DEPRE_MASTER;
                           -- DELETE FROM ASSET_DEPRECIATION_DETAIL;
                           -- DELETE FROM ASSET_DEPRECIATION_MASTER;
                          --  DELETE FROM ASSET_INSURANCE_RENEWAL_DETAIL;
                           -- DELETE FROM ASSET_INSURANCE_RENEWAL_MASTER;
                           -- DELETE FROM ASSET_PURCHASE_DETAIL;
                           -- DELETE FROM ASSET_PURCHASE_MASTER;
                           -- DELETE FROM ASSET_SALES_MASTER;
                           -- DELETE FROM ASSET_AMC_DETAIL;
                           -- DELETE FROM ASSET_AMC_MASTER;
                           -- DELETE FROM STOCK_ITEM_TRANSFER;
                           -- DELETE FROM STOCK_LEDGER;
                           -- DELETE FROM STOCK_PURCHASE_DETAILS;
                           -- DELETE FROM STOCK_MASTER_PURCHASE;
                          --  DELETE FROM STOCK_PURCHASE_RETURNS_DETAILS;
                           -- DELETE FROM STOCK_MASTER_PURCHASE_RETURNS;
                           -- DELETE FROM STOCK_SOLD_UTILIZED_DETAILS;
                           -- DELETE FROM STOCK_MASTER_SOLD_UTILIZED;

                            DELETE FROM MASTER_LEDGER WHERE LEDGER_ID NOT IN (1, 2, 3); 
                            DELETE FROM MASTER_LEDGER_GROUP WHERE GROUP_ID >31 AND LEDGER_GROUP <> 'Capital Account';
                            DELETE FROM USER_PROJECT;
                            DELETE FROM PROJECT_BRANCH;
                            DELETE FROM MASTER_PROJECT;
                            DELETE FROM MASTER_PROJECT_CATOGORY;
                           -- DELETE FROM MASTER_CONTRIBUTION_HEAD;
                            DELETE FROM MASTER_COST_CENTRE_CATEGORY;
                            DELETE FROM MASTER_COST_CENTRE;
                            DELETE FROM MASTER_INSTI_PERFERENCE;
                            DELETE FROM MASTER_STATE;
                            DELETE FROM MASTER_COUNTRY;";
                        break;
                    }
                #endregion

                #region Default Ledgers
                case SQLCommand.AcMePlusMigration.CashDefaultLedger:
                    {
                        Query = "SELECT LEDGER_ID FROM MASTER_LEDGER WHERE LEDGER_NAME='Cash';";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.FixedDepositeDefaultLedger:
                    {
                        Query = "SELECT LEDGER_ID FROM MASTER_LEDGER WHERE LEDGER_NAME='Fixed Deposit';";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.CapitalFundDefaultLedger:
                    {
                        Query = "SELECT LEDGER_ID FROM MASTER_LEDGER WHERE LEDGER_NAME='Capital Fund'";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.DefaultSateId:
                    {
                        Query = "SELECT STATE_ID FROM MASTER_STATE WHERE STATE_NAME='Tamil Nadu';";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.InsertCashDefaultLedger:
                    {
                        Query = "INSERT INTO MASTER_LEDGER(LEDGER_ID,LEDGER_CODE,LEDGER_NAME,GROUP_ID,LEDGER_TYPE,LEDGER_SUB_TYPE) VALUES(1,'CS911','Cash',13,'GN','GN')";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.InsertFixedDepositeDefaultLedger:
                    {
                        Query = "INSERT INTO MASTER_LEDGER(LEDGER_ID,LEDGER_CODE,LEDGER_NAME,GROUP_ID,LEDGER_TYPE,LEDGER_SUB_TYPE) VALUES(2,'FD912','Fixed Deposit',14,'GN','FD');";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.InsertCapitalFundDefaultLedger:
                    {
                        Query = "INSERT INTO MASTER_LEDGER(LEDGER_ID,LEDGER_CODE,LEDGER_NAME,GROUP_ID,LEDGER_TYPE,LEDGER_SUB_TYPE) VALUES(3,'CF913','Capital Fund',21,'GN','GN');";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.InsertDefaultState:
                    {
                        Query = "INSERT INTO MASTER_STATE(STATE_NAME) VALUES('Tamil Nadu');";
                        break;
                    }
                #endregion

                #region  Migrate Users
                case SQLCommand.AcMePlusMigration.MigrateUsers:
                    {
                        Query = "INSERT INTO USER_INFO(USER_NAME,PASSWORD,NAME) VALUES(?USER_NAME,?PASSWORD,?USER_NAME);";
                        break;
                    }
                #endregion

                #region Migrate Accouting Years
                case SQLCommand.AcMePlusMigration.MigrateAcYears:
                    {
                        Query = "INSERT INTO ACCOUNTING_YEAR(YEAR_FROM,YEAR_TO,BOOKS_BEGINNING_FROM,STATUS,IS_FIRST_ACCOUNTING_YEAR) VALUES(?YEAR_FROM,?YEAR_TO,?BOOKS_BEGINNING_FROM,?STATUS,?IS_FIRST_ACCOUNTING_YEAR);";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.FindAccountingYear:
                    {
                        Query = "SELECT COUNT(*) FROM ACCOUNTING_YEAR WHERE YEAR_FROM<=?YEAR_FROM AND YEAR_TO>=?YEAR_TO;";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.FindAccountingYearForTALLY:
                    {
                        //Query = "SELECT COUNT(*) FROM ACCOUNTING_YEAR WHERE YEAR_FROM<=?YEAR_FROM AND YEAR_TO>=?YEAR_TO;";
                        Query = "SELECT COUNT(*) FROM ACCOUNTING_YEAR WHERE (YEAR_FROM BETWEEN ?YEAR_FROM AND ?YEAR_TO) OR (YEAR_TO BETWEEN ?YEAR_FROM AND ?YEAR_TO);";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.SetActiveAccountingYear:
                    {
                        Query = @"UPDATE ACCOUNTING_YEAR SET STATUS=1,IS_FIRST_ACCOUNTING_YEAR=1 WHERE ACC_YEAR_ID=?ACC_YEAR_ID;
                                  UPDATE ACCOUNTING_YEAR SET BOOKS_BEGINNING_FROM=NULL ,IS_FIRST_ACCOUNTING_YEAR=0 WHERE STATUS=0;";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GetLeastAccountingDate:
                    {
                        Query = "SELECT YEAR_FROM FROM ACCOUNTING_YEAR ORDER BY  YEAR_FROM ASC LIMIT 1;";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GetActiveAccountingYearId:
                    {
                        Query = "SELECT ACC_YEAR_ID FROM ACCOUNTING_YEAR ORDER BY YEAR_FROM LIMIT 1;";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GetLeastBookBeginningYear:
                    {
                        Query = "SELECT BOOKS_BEGINNING_FROM FROM ACCOUNTING_YEAR WHERE IS_FIRST_ACCOUNTING_YEAR=1";
                        break;
                    }
                #endregion

                #region Migrate Master Bank
                case SQLCommand.AcMePlusMigration.GetBankId:
                    {
                        Query = "SELECT BANK_ID FROM MASTER_BANK WHERE BANK=?BANK AND BRANCH=?BRANCH;";
                        break;
                    }

                case SQLCommand.AcMePlusMigration.GetBankCode:
                    {
                        Query = "SELECT BANK_CODE FROM MASTER_BANK WHERE BANK_CODE=?BANK_CODE;";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GenerateBankCode:
                    {
                        Query = "SELECT COUNT(*) FROM MASTER_BANK;";
                        break;
                    }

                case SQLCommand.AcMePlusMigration.MigrateMasterBank:
                    {
                        Query = "INSERT INTO MASTER_BANK(BANK_CODE,BANK,BRANCH,ADDRESS) VALUES(?BANK_CODE,?BANK,?BRANCH,?ADDRESS)";
                        break;
                    }
                #endregion

                #region Migrate Country
                case SQLCommand.AcMePlusMigration.MigrateCountry:
                    {
                        Query = "INSERT INTO MASTER_COUNTRY(COUNTRY, COUNTRY_CODE,CURRENCY_SYMBOL) VALUES(?COUNTRY, ?COUNTRY_CODE,?CURRENCY_SYMBOL);";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GenereateCountryCode:
                    {
                        Query = "SELECT COUNT(*) FROM MASTER_COUNTRY;";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GetCountryId:
                    {
                        Query = "SELECT COUNTRY_ID FROM MASTER_COUNTRY WHERE COUNTRY=?COUNTRY";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GetStateId:
                    {
                        Query = "SELECT STATE_ID FROM MASTER_STATE  WHERE STATE_NAME=?STATE_NAME;";
                        break;
                    }

                #endregion

                #region Migrate Project

                case SQLCommand.AcMePlusMigration.GetProjetCategoryId:
                    {
                        Query = "SELECT PROJECT_CATOGORY_ID FROM MASTER_PROJECT_CATOGORY WHERE PROJECT_CATOGORY_NAME='Primary';";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.MigrateProjectCategory:
                    {
                        Query = "INSERT INTO MASTER_PROJECT_CATOGORY(PROJECT_CATOGORY_NAME) VALUES(?PROJECT_CATOGORY_NAME);";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GetProjectCode:
                    {
                        //Query = "SELECT PROJECT_CODE FROM MASTER_PROJECT WHERE PROJECT_CODE=?PROJECT_CODE";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GenerateProjectCode:
                    {
                        Query = "SELECT COUNT(*) FROM MASTER_PROJECT;";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.MigrateProject:
                    {
                        Query = @"INSERT INTO MASTER_PROJECT(PROJECT_CODE,PROJECT,DIVISION_ID,
                                  ACCOUNT_DATE,DATE_STARTED,DATE_CLOSED,DESCRIPTION,PROJECT_CATEGORY_ID)  
                                VALUES(?PROJECT_CODE,?PROJECT,?DIVISION_ID,
                                  ?ACCOUNT_DATE,?DATE_STARTED,?DATE_CLOSED,?DESCRIPTION,?PROJECT_CATEGORY_ID);";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GetAllMasterVouchers:
                    {
                        Query = "SELECT VOUCHER_ID FROM MASTER_VOUCHER;";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.MapProjectVoucher:
                    {
                        Query = "INSERT INTO PROJECT_VOUCHER(PROJECT_ID, VOUCHER_ID) VALUES(?PROJECT_ID, ?VOUCHER_ID);";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GetProjectId:
                    {
                        Query = "SELECT PROJECT_ID FROM MASTER_PROJECT WHERE PROJECT=?PROJECT";
                        break;
                    }
                #endregion

                #region Migrate Ledger Group
                case SQLCommand.AcMePlusMigration.MigrateLedgerGroup:
                    {
                        Query = "INSERT INTO MASTER_LEDGER_GROUP(GROUP_CODE,LEDGER_GROUP,PARENT_GROUP_ID,NATURE_ID,MAIN_GROUP_ID,ACCESS_FLAG) VALUES(?GROUP_CODE,?LEDGER_GROUP,?PARENT_GROUP_ID,?NATURE_ID,?MAIN_GROUP_ID,?ACCESS_FLAG);";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GetLedgerGroupId:
                    {
                        Query = "SELECT GROUP_ID FROM MASTER_LEDGER_GROUP WHERE LEDGER_GROUP=?LEDGER_GROUP;";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GetNatureId:
                    {
                        Query = "SELECT NATURE_ID FROM MASTER_LEDGER_GROUP WHERE LEDGER_GROUP=?LEDGER_GROUP;";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GetLedgerGroupCode:
                    {
                        //  Query = "SELECT GROUP_CODE FROM MASTER_LEDGER_GROUP WHERE GROUP_CODE=?GROUP_CODE;";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GenerateGroupCode:
                    {
                        Query = "SELECT COUNT(*) FROM MASTER_LEDGER_GROUP;";
                        break;
                    }
                #endregion

                #region Migrate Ledgers
                case SQLCommand.AcMePlusMigration.MigrateLedger:
                    {
                        Query = "INSERT INTO MASTER_LEDGER(LEDGER_CODE,LEDGER_NAME,GROUP_ID,LEDGER_TYPE,LEDGER_SUB_TYPE) VALUES(?LEDGER_CODE,?LEDGER_NAME,?GROUP_ID,?LEDGER_TYPE,?LEDGER_SUB_TYPE);";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GetLedgerCode:
                    {
                        //  Query = "SELECT LEDGER_CODE FROM MASTER_LEDGER WHERE LEDGER_CODE=?LEDGER_CODE";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GenerateLedgerCode:
                    {
                        Query = "SELECT COUNT(*) FROM MASTER_LEDGER";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GetLedgerId:
                    {
                        Query = "SELECT LEDGER_ID FROM MASTER_LEDGER WHERE LEDGER_NAME=?LEDGER_NAME";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.UpdateLedgerCostCentre:
                    {
                        Query = "UPDATE MASTER_LEDGER SET IS_COST_CENTER =1 WHERE LEDGER_ID IN (SELECT DISTINCT(LEDGER_ID) FROM VOUCHER_CC_TRANS);";
                        break;
                    }
                #endregion

                #region Mapping and Updating Opening Balance
                case SQLCommand.AcMePlusMigration.MapProjectCash:
                    {
                        Query = "SELECT PROJECT_ID,DIVISION_ID FROM MASTER_PROJECT WHERE PROJECT_ID NOT IN (SELECT PROJECT_ID FROM PROJECT_LEDGER);";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.UpdateOpBalance:
                    {
                        Query = "INSERT INTO LEDGER_BALANCE(BALANCE_DATE,PROJECT_ID,LEDGER_ID,AMOUNT,TRANS_MODE,TRANS_FLAG) VALUES(?BALANCE_DATE,?PROJECT_ID,?LEDGER_ID,?AMOUNT,?TRANS_MODE,'OP');";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.SumUpdateOPBalance:
                    {
                        Query = "UPDATE LEDGER_BALANCE\n" +
                                "   SET BALANCE_DATE = ?BALANCE_DATE,\n" +
                                "       PROJECT_ID   = ?PROJECT_ID,\n" +
                                "       LEDGER_ID    = ?LEDGER_ID,\n" +
                                "       AMOUNT       = ?AMOUNT,\n" +
                                "       TRANS_MODE   = ?TRANS_MODE,\n" +
                                "       TRANS_FLAG   ='OP'\n" +
                                " WHERE BALANCE_DATE = ?BALANCE_DATE\n" +
                                "   AND PROJECT_ID = ?PROJECT_ID\n" +
                                "   AND LEDGER_ID = ?LEDGER_ID";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.CheckOPBalUpdate:
                    {
                        Query = "SELECT AMOUNT\n" +
                                "  FROM LEDGER_BALANCE\n" +
                                " WHERE BALANCE_DATE = ?BALANCE_DATE\n" +
                                "   AND PROJECT_ID = ?PROJECT_ID\n" +
                                "   AND LEDGER_ID = ?LEDGER_ID;";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.IsProjectLedgerMapped:
                    {
                        Query = "SELECT LEDGER_ID FROM PROJECT_LEDGER WHERE LEDGER_ID=?LEDGER_ID AND PROJECT_ID=?PROJECT_ID";
                        break;
                    }
                #endregion

                #region Migrate Master Bank Accounts
                case SQLCommand.AcMePlusMigration.MigrateBankAccount:
                    {
                        Query = "INSERT INTO MASTER_BANK_ACCOUNT(LEDGER_ID,ACCOUNT_CODE,ACCOUNT_NUMBER,BANK_ID,DATE_OPENED,DATE_CLOSED,ACCOUNT_TYPE_ID) VALUES(?LEDGER_ID,?ACCOUNT_CODE,?ACCOUNT_NUMBER,?BANK_ID,?DATE_OPENED,?DATE_CLOSED,?ACCOUNT_TYPE_ID);";
                        break;
                    }

                case SQLCommand.AcMePlusMigration.GetBankAccountNo:
                    {
                        Query = "SELECT ACCOUNT_NUMBER FROM MASTER_BANK_ACCOUNT WHERE ACCOUNT_NUMBER=?ACCOUNT_NUMBER;";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GetBankAccountCode:
                    {
                        // Query = "SELECT ACCOUNT_CODE FROM MASTER_BANK_ACCOUNT WHERE ACCOUNT_CODE=?ACCOUNT_CODE;";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GenerateBankAccountCode:
                    {
                        Query = "SELECT COUNT(*) FROM MASTER_BANK_ACCOUNT;";
                        break;
                    }
                #endregion

                #region Migrate Donor Auditor
                case SQLCommand.AcMePlusMigration.MigrateDonorAuditor:
                    {
                        Query = "INSERT INTO MASTER_DONAUD(NAME,TYPE,PLACE,STATE_ID,COUNTRY_ID,ADDRESS,PINCODE,PHONE,FAX,EMAIL,IDENTITYKEY,URL,FCDONOR) VALUES(?NAME,?TYPE,?PLACE,?STATE_ID,?COUNTRY_ID,?ADDRESS,?PINCODE,?PHONE,?FAX,?EMAIL,?IDENTITYKEY,?URL,?AUDIT_TYPE_ID);";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GetDonorAuditorId:
                    {
                        Query = "SELECT COUNT(*) FROM MASTER_DONAUD WHERE NAME=?NAME AND COUNTRY_ID=?COUNTRY_ID;";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.IsProjectDonorMapped:
                    {
                        Query = "SELECT COUNT(*) FROM PROJECT_DONOR WHERE PROJECT_ID=?PROJECT_ID AND DONOR_ID=?DONAUD_ID;";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.IsProjectPurposeMapped:
                    {
                        Query = "SELECT COUNT(*) FROM PROJECT_PURPOSE WHERE PROJECT_ID=?PROJECT_ID AND CONTRIBUTION_ID=?DONAUD_ID;";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.NewDonorId:
                    {
                        Query = "SELECT DONAUD_ID FROM MASTER_DONAUD WHERE NAME=?NAME AND PLACE=?PLACE;";
                        break;
                    }
                #endregion

                #region Migrate Cost Centre and Category
                case SQLCommand.AcMePlusMigration.MigrateCostCentre:
                    {
                        Query = "INSERT INTO MASTER_COST_CENTRE(ABBREVATION,COST_CENTRE_NAME) VALUES(?ABBREVATION,?COST_CENTRE_NAME);";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GetCostCentreId:
                    {
                        Query = "SELECT COST_CENTRE_ID FROM MASTER_COST_CENTRE WHERE COST_CENTRE_NAME=?COST_CENTRE_NAME;";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GetCostCentreCode:
                    {
                        Query = "SELECT ABBREVATION FROM MASTER_COST_CENTRE WHERE ABBREVATION=?ABBREVATION";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GenerateCostCentreCode:
                    {
                        Query = "SELECT COUNT(*) FROM MASTER_COST_CENTRE;";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.ICostCategoryExists:
                    {
                        Query = "SELECT COST_CENTRECATEGORY_ID FROM MASTER_COST_CENTRE_CATEGORY WHERE COST_CENTRE_CATEGORY_NAME='General';";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.AddDefaultCostCategory:
                    {
                        Query = "INSERT INTO MASTER_COST_CENTRE_CATEGORY(COST_CENTRE_CATEGORY_NAME) VALUES('General');";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.MapCostCentreCategory:
                    {
                        Query = "INSERT INTO COSTCATEGORY_COSTCENTRE(COST_CATEGORY_ID,COST_CENTRE_ID) VALUES(?COST_CENTRECATEGORY_ID,?COST_CENTRE_ID);";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.IsCostCategoryMapped:
                    {
                        Query = "SELECT COST_CATEGORY_ID FROM COSTCATEGORY_COSTCENTRE WHERE COST_CATEGORY_ID=?COST_CENTRECATEGORY_ID AND COST_CENTRE_ID=?COST_CENTRE_ID;";
                        break;
                    }
                #endregion

                #region Migrate Executive Committee Members
                case SQLCommand.AcMePlusMigration.MigrateExecutiveCommittee:
                    {
                        Query = "INSERT INTO MASTER_EXECUTIVE_COMMITTEE(EXECUTIVE,NAME,NATIONALITY,OCCUPATION,ASSOCIATION," +
                                 "OFFICE_BEARER,PLACE,STATE_ID,COUNTRY_ID,PIN_CODE,PHONE,FAX,EMAIL,URL) VALUES(?EXECUTIVE,?NAME,?NATIONALITY,?OCCUPATION,?ASSOCIATION," +
                                 "?OFFICE_BEARER,?PLACE,?STATE_ID,?COUNTRY_ID,?PIN_CODE,?PHONE,?FAX,?EMAIL,?URL);";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GetExecutiveCommitteeId:
                    {
                        Query = "SELECT COUNT(*) FROM MASTER_EXECUTIVE_COMMITTEE WHERE NAME=?NAME AND COUNTRY_ID=?COUNTRY_ID;";
                        break;
                    }
                #endregion

                #region Migrate FC Purpose
                case SQLCommand.AcMePlusMigration.MigrateFCPurpose:
                    {
                        Query = "INSERT INTO MASTER_CONTRIBUTION_HEAD(CODE, FC_PURPOSE) VALUES(?CODE,?FC_PURPOSE);";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.MigrateFCPurposeOpening:
                    {
                        Query = "INSERT INTO PROJECT_PURPOSE(PROJECT_ID, CONTRIBUTION_ID, AMOUNT, TRANS_MODE)VALUES(?PROJECT_ID, ?CONTRIBUTION_ID, ?AMOUNT, ?TRANS_MODE)\n" +
                                "ON DUPLICATE KEY UPDATE PROJECT_ID=?PROJECT_ID,CONTRIBUTION_ID=?CONTRIBUTION_ID,AMOUNT=?AMOUNT,TRANS_MODE=?TRANS_MODE";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GetPurposeId:
                    {
                        Query = "SELECT COUNT(FC_PURPOSE) FROM MASTER_CONTRIBUTION_HEAD WHERE FC_PURPOSE=?FC_PURPOSE";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GetPurposeIdByPurpose:
                    {
                        Query = "SELECT CONTRIBUTION_ID FROM MASTER_CONTRIBUTION_HEAD WHERE FC_PURPOSE=?FC_PURPOSE";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GetPurposeCode:
                    {
                        Query = "SELECT CODE FROM MASTER_CONTRIBUTION_HEAD WHERE CODE=?CODE;";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GeneratePurposeCode:
                    {
                        Query = "SELECT COUNT(*) FROM MASTER_CONTRIBUTION_HEAD;";
                        break;
                    }

                case SQLCommand.AcMePlusMigration.MapPurpose:
                    {
                        Query = @"INSERT INTO PROJECT_PURPOSE
                                    (PROJECT_ID, CONTRIBUTION_ID, AMOUNT, TRANS_MODE)
                                VALUES
                                    (?PROJECT_ID,?PURPOSE_ID, 0, 'CR');";
                        break;
                    }

                #endregion

                #region Migrate Journal Voucher
                case SQLCommand.AcMePlusMigration.MigrateVoucherMaster:
                    {
                        Query = "INSERT INTO VOUCHER_MASTER_TRANS(VOUCHER_DATE,PROJECT_ID,VOUCHER_NO,VOUCHER_TYPE,NARRATION, CREATED_ON, CREATED_BY, CREATED_BY_NAME,VOUCHER_DEFINITION_ID) VALUES(?VOUCHER_DATE,?PROJECT_ID,?VOUCHER_NO,?VOUCHER_TYPE,?NARRATION, NOW(), ?CREATED_BY, ?CREATED_BY_NAME,?VOUCHER_DEFINITION_ID);";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.MigrateVoucherTransJournal:
                    {
                        Query = "INSERT INTO VOUCHER_TRANS(VOUCHER_ID,SEQUENCE_NO,LEDGER_ID,AMOUNT,TRANS_MODE,NARRATION) VALUES(?VOUCHER_ID,?SEQUENCE_NO,?LEDGER_ID,?AMOUNT,?TRANS_MODE,?NARRATION);";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.MigrateVoucherTransWithChequeNo:
                    {
                        Query = "INSERT INTO VOUCHER_TRANS(VOUCHER_ID,SEQUENCE_NO,LEDGER_ID,AMOUNT,TRANS_MODE,CHEQUE_NO,CHEQUE_REF_DATE,CHEQUE_REF_BANKNAME,CHEQUE_REF_BRANCH,MATERIALIZED_ON) " +
                                "VALUES(?VOUCHER_ID,?SEQUENCE_NO,?LEDGER_ID,?AMOUNT,?TRANS_MODE,?CHEQUE_NO,?CHEQUE_REF_DATE,?CHEQUE_REF_BANKNAME,?CHEQUE_REF_BRANCH,?MATERIALIZED_ON);";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.MigrateVoucherMasterDonor:
                    {
                        Query = @"INSERT INTO VOUCHER_MASTER_TRANS
                                    (VOUCHER_DATE,
                                    PROJECT_ID,
                                    VOUCHER_NO,
                                    VOUCHER_TYPE,
                                    NARRATION,
                                    CREATED_ON, CREATED_BY, CREATED_BY_NAME,
                                    DONOR_ID,
                                    PURPOSE_ID,
                                    CONTRIBUTION_TYPE,
                                    CONTRIBUTION_AMOUNT,
                                    CURRENCY_COUNTRY_ID,
                                    EXCHANGE_RATE,
                                    EXCHANGE_COUNTRY_ID,
                                    CALCULATED_AMOUNT,
                                    ACTUAL_AMOUNT,
                                    VOUCHER_DEFINITION_ID)
                                VALUES
                                    (?VOUCHER_DATE,
                                    ?PROJECT_ID,
                                    ?VOUCHER_NO,
                                    ?VOUCHER_TYPE,
                                    ?NARRATION,
                                    NOW(), ?CREATED_BY, ?CREATED_BY_NAME,
                                    ?DONAUD_ID,
                                    ?PURPOSE_ID,
                                    ?CONTRIBUTION_TYPE,
                                    ?CONTRIBUTION_AMOUNT,
                                    ?CURRENCY_COUNTRY_ID,
                                    ?EXCHANGE_RATE,
                                    ?EXCHANGE_COUNTRY_ID,
                                    ?CALCULATED_AMOUNT,
                                    ?ACTUAL_AMOUNT,
                                    ?VOUCHER_DEFINITION_ID);";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.MigrateCostCentreTransaction:
                    {
                        //Query = "INSERT INTO VOUCHER_CC_TRANS(VOUCHER_ID,LEDGER_ID,COST_CENTRE_TABLE,COST_CENTRE_ID,AMOUNT) VALUES(?VOUCHER_ID,?LEDGER_ID,?COST_CENTRE_TABLE,?COST_CENTRE_ID,?AMOUNT);";
                        Query = "INSERT INTO VOUCHER_CC_TRANS(VOUCHER_ID, LEDGER_ID, COST_CENTRE_TABLE, COST_CENTRE_ID, AMOUNT, SEQUENCE_NO, LEDGER_SEQUENCE_NO) " +
                                 " VALUES(?VOUCHER_ID, ?LEDGER_ID, ?COST_CENTRE_TABLE, ?COST_CENTRE_ID, ?AMOUNT, ?SEQUENCE_NO, ?LEDGER_SEQUENCE_NO);";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.MapProjectDonor:
                    {
                        // To avoid duplicate key error
                        Query = "INSERT INTO PROJECT_DONOR(PROJECT_ID, DONOR_ID) VALUES(?PROJECT_ID,?DONOR_ID) ON DUPLICATE KEY UPDATE PROJECT_ID=?PROJECT_ID , DONOR_ID=?DONOR_ID";
                        //  Query = "INSERT INTO PROJECT_DONOR(PROJECT_ID, DONOR_ID) VALUES(?PROJECT_ID,?DONOR_ID);";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.MapVoucherCostCentre:
                    {
                        Query = "INSERT INTO PROJECT_COSTCENTRE(PROJECT_ID, LEDGER_ID, COST_CENTRE_ID, AMOUNT, TRANS_MODE)\n" +
                                "VALUES(?PROJECT_ID, ?LEDGER_ID, ?COST_CENTRE_ID, ?AMOUNT, ?TRANS_MODE) ON DUPLICATE KEY\n"+ 
                                "UPDATE AMOUNT=?AMOUNT AND LEDGER_ID=?LEDGER_ID AND TRANS_MODE=?TRANS_MODE\n;";
                        break;
                    }
                case SQLCommand.AcMePlusMigration.GetBankAccountLedgerId:
                    {
                        Query = "SELECT LEDGER_ID FROM MASTER_BANK_ACCOUNT WHERE ACCOUNT_NUMBER=?ACCOUNT_NUMBER;";
                        break;
                    }
                #endregion

            }
            return Query;
        }
        #endregion

    }
}
