using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Utility;

namespace Bosco.SQL
{
    public class TallyMigrationSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.TallyMigration).FullName)
            {
                query = GetSettingSQL();
            }
            else if (sqlCommandName == typeof(SQLCommand.TallyExport).FullName)
            {
                query = GetExportSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        #region SQL Script
        private string GetSettingSQL()
        {
            string Query = string.Empty;
            SQLCommand.TallyMigration sqlCommandId = (SQLCommand.TallyMigration)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                #region Removing Old Data
                case SQLCommand.TallyMigration.ClearData:
                    {
                        
                        //-----------------------Note: In case of new table is added that must be added appropriately ------------------------------------
                        Query = @"DELETE FROM PROJECT_COSTCENTRE WHERE PROJECT_ID=?PROJECT_ID;
                                DELETE FROM PROJECT_LEDGER WHERE PROJECT_ID=?PROJECT_ID;
                                DELETE FROM PROJECT_DONOR WHERE PROJECT_ID=?PROJECT_ID;
                                DELETE FROM PROJECT_VOUCHER WHERE PROJECT_ID=?PROJECT_ID;
                                DELETE FROM PROJECT_COSTCENTRE WHERE PROJECT_ID=?PROJECT_ID;
                                DELETE FROM USER_PROJECT WHERE PROJECT_ID=?PROJECT_ID;
                                DELETE FROM LEDGER_BALANCE WHERE PROJECT_ID=?PROJECT_ID;
                                DELETE FROM PROJECT_PURPOSE WHERE PROJECT_ID=?PROJECT_ID;
                                DELETE FROM PROJECT_BRANCH WHERE PROJECT_ID=?PROJECT_ID;
                                
                                DELETE FD.* FROM FD_RENEWAL FD INNER JOIN FD_ACCOUNT FA ON FD.FD_ACCOUNT_ID = FA.FD_ACCOUNT_ID
                                    WHERE FA.PROJECT_ID =?PROJECT_ID;

                                DELETE FROM FD_ACCOUNT WHERE PROJECT_ID =?PROJECT_ID;

                                DELETE TDSPD.*, TDSP.* FROM TDS_PAYMENT TDSP INNER JOIN TDS_PAYMENT_DETAIL TDSPD ON TDSP.TDS_PAYMENT_ID = TDSPD.TDS_PAYMENT_ID
                                    WHERE TDSP.PROJECT_ID =?PROJECT_ID;

                                DELETE TPPD.*, TPP.* FROM TDS_PARTY_PAYMENT TPP INNER JOIN TDS_PARTY_PAYMENT_DETAIL TPPD ON TPP.PARTY_PAYMENT_ID = TPPD.PARTY_PAYMENT_ID
                                    WHERE TPP.PROJECT_ID =?PROJECT_ID;

                                DELETE FROM TDS_PARTY_PAYMENT WHERE PROJECT_ID =?PROJECT_ID;

                                DELETE TDD.*, TD.* FROM TDS_DEDUCTION TD INNER JOIN TDS_DEDUCTION_DETAIL TDD ON TD.DEDUCTION_ID = TDD.DEDUCTION_ID
                                    WHERE TD.PROJECT_ID =?PROJECT_ID;

                                DELETE TB.*, TBB.* FROM TDS_BOOKING TB INNER JOIN TDS_BOOKING_DETAIL TBB ON TB.BOOKING_ID = TBB.BOOKING_ID
                                    WHERE TB.PROJECT_ID =?PROJECT_ID;

                                DELETE FROM PAYROLL_VOUCHER WHERE VOUCHER_ID IN (SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS
                                    WHERE PROJECT_ID = ?PROJECT_ID);

                                DELETE VR.* FROM VOUCHER_MASTER_TRANS VMT JOIN VOUCHER_REFERENCE VR ON VR.REF_VOUCHER_ID = VMT.VOUCHER_ID
                                 WHERE VMT.PROJECT_ID =?PROJECT_ID;

                                DELETE VCCT.* FROM VOUCHER_MASTER_TRANS VMT JOIN VOUCHER_CC_TRANS VCCT ON VMT.VOUCHER_ID = VCCT.VOUCHER_ID
                                    WHERE VMT.PROJECT_ID =?PROJECT_ID;

                                DELETE VST.* FROM VOUCHER_MASTER_TRANS VMT JOIN VOUCHER_SUB_LEDGER_TRANS VST ON VMT.VOUCHER_ID = VST.VOUCHER_ID
                                    WHERE VMT.PROJECT_ID =?PROJECT_ID;

                                DELETE VT.*, VMT.* FROM VOUCHER_MASTER_TRANS VMT LEFT JOIN VOUCHER_TRANS VT ON VMT.VOUCHER_ID = VT.VOUCHER_ID
                                    WHERE VMT.PROJECT_ID =?PROJECT_ID;
                                 
                                DELETE FROM FD_ACCOUNT WHERE PROJECT_ID =?PROJECT_ID;

                             -- PayRoll,Asset and Stock should not be executed unless those modules used by the client
                              /*  DELETE FROM PAYROLL_PROJECT WHERE PROJECT_ID =?PROJECT_ID;

                                DELETE FROM PRPROJECT_STAFF WHERE PROJECT_ID =?PROJECT_ID;

                                DELETE AIMD.* FROM ASSET_INSURANCE_MASTER AIM LEFT JOIN ASSET_INSURANCE_MASTER_DETAIL AIMD ON AIM.INS_ID = AIMD.INS_ID
                                 WHERE AIM.PROJECT_ID =?PROJECT_ID;

                                DELETE FROM ASSET_INSURANCE_MASTER WHERE PROJECT_ID =?PROJECT_ID;

                                DELETE ADMD.* FROM ASSET_DEPRE_MASTER ADM LEFT JOIN ASSET_DEPRE_DETAIL ADMD ON ADM.DEP_ID = ADMD.DEP_ID
                                 WHERE ADM.PROJECT_ID =?PROJECT_ID;

                                 DELETE FROM ASSET_DEPRE_MASTER
                                 WHERE PROJECT_ID =?PROJECT_ID;

                                DELETE AIRMD.* FROM ASSET_INSURANCE_RENEWAL_MASTER AIRM LEFT JOIN ASSET_INSURANCE_RENEWAL_DETAIL AIRMD ON AIRM.RENEWAL_ID = AIRMD.RENEWAL_ID
                                 WHERE AIRM.PROJECT_ID =?PROJECT_ID;

                                DELETE FROM ASSET_INSURANCE_RENEWAL_MASTER WHERE PROJECT_ID =?PROJECT_ID;

                                DELETE APD.* FROM ASSET_PURCHASE_MASTER APM LEFT JOIN ASSET_PURCHASE_DETAIL APD ON APM.PURCHASE_ID = APD.PURCHASE_ID
                                 WHERE APM.PROJECT_ID =?PROJECT_ID;

                                DELETE FROM ASSET_PURCHASE_MASTER WHERE PROJECT_ID =?PROJECT_ID;

                                DELETE FROM ASSET_SALES_MASTER WHERE PROJECT_ID =?PROJECT_ID;

                                DELETE AAD.* FROM ASSET_AMC_MASTER AAM LEFT JOIN ASSET_AMC_DETAIL AAD ON AAM.AMC_ID = AAD.AMC_ID
                                 WHERE AAM.PROJECT_ID =?PROJECT_ID;

                                DELETE FROM ASSET_AMC_MASTER WHERE PROJECT_ID=?PROJECT_ID;

                                DELETE FROM STOCK_ITEM_TRANSFER WHERE PROJECT_ID=?PROJECT_ID;

                                DELETE SPD.* FROM STOCK_MASTER_PURCHASE SMP LEFT JOIN STOCK_PURCHASE_DETAILS SPD ON SMP.PURCHASE_ID = SPD.PURCHASE_ID
                                 WHERE SMP.PROJECT_ID =?PROJECT_ID;

                                DELETE FROM STOCK_MASTER_PURCHASE WHERE PROJECT_ID=?PROJECT_ID;

                                DELETE SPRD.* FROM STOCK_MASTER_PURCHASE_RETURNS SMPR LEFT JOIN STOCK_PURCHASE_RETURNS_DETAILS SPRD ON SMPR.RETURN_ID=SPRD.RETURN_ID
                                 WHERE SMPR.PROJECT_ID =?PROJECT_ID;

                                DELETE FROM STOCK_MASTER_PURCHASE_RETURNS WHERE PROJECT_ID=?PROJECT_ID;

                                DELETE SSUD.* FROM STOCK_MASTER_SOLD_UTILIZED SMSU LEFT JOIN STOCK_SOLD_UTILIZED_DETAILS SSUD ON SMSU.SALES_ID=SSUD.SALES_ID
                                 WHERE SMSU.PROJECT_ID =?PROJECT_ID;

                                DELETE FROM STOCK_MASTER_SOLD_UTILIZED WHERE PROJECT_ID=?PROJECT_ID; */

                                DELETE BM.*, BP.*, BL.*, AF.* FROM BUDGET_MASTER BM INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID
                                INNER JOIN BUDGET_LEDGER BL ON BL.BUDGET_ID = BM.BUDGET_ID
                                LEFT JOIN ALLOT_FUND AF ON BL.BUDGET_ID = AF.BUDGET_ID
                                WHERE BP.PROJECT_ID = ?PROJECT_ID;

                                DELETE FROM MASTER_PROJECT WHERE PROJECT_ID =?PROJECT_ID;";
                        break;
                    }
                case SQLCommand.TallyMigration.ClearDataByDateRange:
                    {
                        //On 27/10/2017, When tally migration, clear given Date Range (Vouchers - Master, trans, cc, FD, TDS and Voucher refefrence)
                        //-----------------------Note: In case of new table is added that must be added appropriately which is related to Voucher------------------------------------
                        Query = @"DELETE FROM LEDGER_BALANCE WHERE PROJECT_ID=?PROJECT_ID AND TRANS_FLAG <> 'OP' AND BALANCE_DATE BETWEEN ?DATE_FROM AND ?DATE_TO;
                                                                
                                DELETE FD.* FROM FD_RENEWAL FD INNER JOIN FD_ACCOUNT FA ON FD.FD_ACCOUNT_ID = FA.FD_ACCOUNT_ID
                                    WHERE FA.PROJECT_ID =?PROJECT_ID AND FA.INVESTMENT_DATE BETWEEN ?DATE_FROM AND ?DATE_TO;

                                DELETE FROM FD_ACCOUNT WHERE PROJECT_ID =?PROJECT_ID AND INVESTMENT_DATE BETWEEN ?DATE_FROM AND ?DATE_TO;

                                DELETE TDSPD.*, TDSP.* FROM TDS_PAYMENT TDSP INNER JOIN TDS_PAYMENT_DETAIL TDSPD ON TDSP.TDS_PAYMENT_ID = TDSPD.TDS_PAYMENT_ID
                                    WHERE TDSP.PROJECT_ID =?PROJECT_ID AND TDSP.PAYMENT_DATE BETWEEN ?DATE_FROM AND ?DATE_TO;

                                DELETE TPPD.*, TPP.* FROM TDS_PARTY_PAYMENT TPP INNER JOIN TDS_PARTY_PAYMENT_DETAIL TPPD ON TPP.PARTY_PAYMENT_ID = TPPD.PARTY_PAYMENT_ID
                                    WHERE TPP.PROJECT_ID =?PROJECT_ID AND TPP.PAYMENT_DATE BETWEEN ?DATE_FROM AND ?DATE_TO;
                                
                                DELETE TDD.*, TD.* FROM TDS_DEDUCTION TD INNER JOIN TDS_DEDUCTION_DETAIL TDD ON TD.DEDUCTION_ID = TDD.DEDUCTION_ID
                                    WHERE TD.PROJECT_ID =?PROJECT_ID AND TD.DEDUCTION_DATE BETWEEN ?DATE_FROM AND ?DATE_TO;

                                DELETE TB.*, TBB.* FROM TDS_BOOKING TB INNER JOIN TDS_BOOKING_DETAIL TBB ON TB.BOOKING_ID = TBB.BOOKING_ID
                                    WHERE TB.PROJECT_ID =?PROJECT_ID AND TB.BOOKING_DATE BETWEEN ?DATE_FROM AND ?DATE_TO;
                                
                                DELETE FROM PAYROLL_VOUCHER WHERE VOUCHER_ID IN (SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS
                                    WHERE PROJECT_ID = ?PROJECT_ID AND VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO);

                                DELETE VR.* FROM VOUCHER_MASTER_TRANS VMT JOIN VOUCHER_REFERENCE VR ON VR.REF_VOUCHER_ID = VMT.VOUCHER_ID
                                 WHERE VMT.PROJECT_ID =?PROJECT_ID AND VMT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO;
                                
                                DELETE VR.* FROM VOUCHER_MASTER_TRANS VMT JOIN VOUCHER_REFERENCE VR ON VR.REC_PAY_VOUCHER_ID = VMT.VOUCHER_ID
                                WHERE VMT.PROJECT_ID =?PROJECT_ID AND VMT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO;

                                DELETE VCCT.* FROM VOUCHER_MASTER_TRANS VMT JOIN VOUCHER_CC_TRANS VCCT ON VMT.VOUCHER_ID = VCCT.VOUCHER_ID
                                    WHERE VMT.PROJECT_ID =?PROJECT_ID AND VMT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO;

                                DELETE VST.* FROM VOUCHER_MASTER_TRANS VMT JOIN VOUCHER_SUB_LEDGER_TRANS VST ON VMT.VOUCHER_ID = VST.VOUCHER_ID
                                    WHERE VMT.PROJECT_ID =?PROJECT_ID AND VMT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO;

                                DELETE VT.*, VMT.* FROM VOUCHER_MASTER_TRANS VMT LEFT JOIN VOUCHER_TRANS VT ON VMT.VOUCHER_ID = VT.VOUCHER_ID
                                    WHERE VMT.PROJECT_ID =?PROJECT_ID AND VMT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO;

                                
                             -- PayRoll,Asset and Stock should not be executed unless those modules used by the client
                              /*  DELETE FROM PAYROLL_PROJECT WHERE PROJECT_ID =?PROJECT_ID;

                                DELETE FROM PRPROJECT_STAFF WHERE PROJECT_ID =?PROJECT_ID;

                                DELETE AIMD.* FROM ASSET_INSURANCE_MASTER AIM LEFT JOIN ASSET_INSURANCE_MASTER_DETAIL AIMD ON AIM.INS_ID = AIMD.INS_ID
                                 WHERE AIM.PROJECT_ID =?PROJECT_ID;

                                DELETE FROM ASSET_INSURANCE_MASTER WHERE PROJECT_ID =?PROJECT_ID;

                                DELETE ADMD.* FROM ASSET_DEPRE_MASTER ADM LEFT JOIN ASSET_DEPRE_DETAIL ADMD ON ADM.DEP_ID = ADMD.DEP_ID
                                 WHERE ADM.PROJECT_ID =?PROJECT_ID;

                                 DELETE FROM ASSET_DEPRE_MASTER
                                 WHERE PROJECT_ID =?PROJECT_ID;

                                DELETE AIRMD.* FROM ASSET_INSURANCE_RENEWAL_MASTER AIRM LEFT JOIN ASSET_INSURANCE_RENEWAL_DETAIL AIRMD ON AIRM.RENEWAL_ID = AIRMD.RENEWAL_ID
                                 WHERE AIRM.PROJECT_ID =?PROJECT_ID;

                                DELETE FROM ASSET_INSURANCE_RENEWAL_MASTER WHERE PROJECT_ID =?PROJECT_ID;

                                DELETE APD.* FROM ASSET_PURCHASE_MASTER APM LEFT JOIN ASSET_PURCHASE_DETAIL APD ON APM.PURCHASE_ID = APD.PURCHASE_ID
                                 WHERE APM.PROJECT_ID =?PROJECT_ID;

                                DELETE FROM ASSET_PURCHASE_MASTER WHERE PROJECT_ID =?PROJECT_ID;

                                DELETE FROM ASSET_SALES_MASTER WHERE PROJECT_ID =?PROJECT_ID;

                                DELETE AAD.* FROM ASSET_AMC_MASTER AAM LEFT JOIN ASSET_AMC_DETAIL AAD ON AAM.AMC_ID = AAD.AMC_ID
                                 WHERE AAM.PROJECT_ID =?PROJECT_ID;

                                DELETE FROM ASSET_AMC_MASTER WHERE PROJECT_ID=?PROJECT_ID;

                                DELETE FROM STOCK_ITEM_TRANSFER WHERE PROJECT_ID=?PROJECT_ID;

                                DELETE SPD.* FROM STOCK_MASTER_PURCHASE SMP LEFT JOIN STOCK_PURCHASE_DETAILS SPD ON SMP.PURCHASE_ID = SPD.PURCHASE_ID
                                 WHERE SMP.PROJECT_ID =?PROJECT_ID;

                                DELETE FROM STOCK_MASTER_PURCHASE WHERE PROJECT_ID=?PROJECT_ID;

                                DELETE SPRD.* FROM STOCK_MASTER_PURCHASE_RETURNS SMPR LEFT JOIN STOCK_PURCHASE_RETURNS_DETAILS SPRD ON SMPR.RETURN_ID=SPRD.RETURN_ID
                                 WHERE SMPR.PROJECT_ID =?PROJECT_ID;

                                DELETE FROM STOCK_MASTER_PURCHASE_RETURNS WHERE PROJECT_ID=?PROJECT_ID;

                                DELETE SSUD.* FROM STOCK_MASTER_SOLD_UTILIZED SMSU LEFT JOIN STOCK_SOLD_UTILIZED_DETAILS SSUD ON SMSU.SALES_ID=SSUD.SALES_ID
                                 WHERE SMSU.PROJECT_ID =?PROJECT_ID;

                                DELETE FROM STOCK_MASTER_SOLD_UTILIZED WHERE PROJECT_ID=?PROJECT_ID; */";
                        break;
                    }
                #endregion

                #region Account Periods
                case SQLCommand.TallyMigration.MigrateAcYears:
                    {
                        Query = "INSERT INTO ACCOUNTING_YEAR(YEAR_FROM,YEAR_TO,BOOKS_BEGINNING_FROM,STATUS,IS_FIRST_ACCOUNTING_YEAR) VALUES(?YEAR_FROM,?YEAR_TO,?BOOKS_BEGINNING_FROM,?STATUS,?IS_FIRST_ACCOUNTING_YEAR);";
                        break;
                    }
                case SQLCommand.TallyMigration.SetActiveAccountingYear:
                    {
                        Query = @"UPDATE ACCOUNTING_YEAR
                                       SET STATUS                   = 0,
                                           IS_FIRST_ACCOUNTING_YEAR = 0,
                                           BOOKS_BEGINNING_FROM     = null;
                                    UPDATE ACCOUNTING_YEAR
                                       SET STATUS                   = 1,
                                           IS_FIRST_ACCOUNTING_YEAR = 1,
                                           BOOKS_BEGINNING_FROM     = ?BOOKS_BEGINNING_FROM
                                     WHERE ACC_YEAR_ID = ?ACC_YEAR_ID;";
                        break;
                    }
                case SQLCommand.TallyMigration.GetCurrentMigrationYear:
                    {
                        Query = "SELECT * FROM  ACCOUNTING_YEAR WHERE YEAR_FROM=?YEAR_FROM";
                        break;
                    }
                case SQLCommand.TallyMigration.SetCurrentMigrationYear:
                    {
                        Query = "";
                        break;
                    }
                #endregion

                #region Project
                case SQLCommand.TallyMigration.MigrateProject:
                    {
                        Query = @"INSERT INTO MASTER_PROJECT
                                      (PROJECT_CODE,
                                       PROJECT,
                                       DIVISION_ID,
                                       ACCOUNT_DATE,
                                       DATE_STARTED,
                                       DATE_CLOSED,
                                       DESCRIPTION,
                                       PROJECT_CATEGORY_ID)
                                    VALUES
                                      (?PROJECT_CODE,
                                       ?PROJECT,
                                       ?DIVISION_ID,
                                       ?ACCOUNT_DATE,
                                       ?DATE_STARTED,
                                       ?DATE_CLOSED,
                                       ?DESCRIPTION,
                                       ?PROJECT_CATEGORY_ID);";
                        break;
                    }
                case SQLCommand.TallyMigration.MapProjectVoucher:
                    {
                        // 1 -Receipts
                        // 2 -Payments
                        // 3 -Contra
                        // 4 -Journal
                        Query = "INSERT INTO PROJECT_VOUCHER(PROJECT_ID, VOUCHER_ID) VALUES(?PROJECT_ID,1),(?PROJECT_ID,2),(?PROJECT_ID,3),(?PROJECT_ID,4);";
                        break;
                    }
                case SQLCommand.TallyMigration.IsDefaultVoucherExists:
                    {
                        Query = "SELECT COUNT(VOUCHER_ID) FROM MASTER_VOUCHER WHERE VOUCHER_ID IN (1,2,3,4);";
                        break;
                    }
                #endregion

                #region Voucher
                case SQLCommand.TallyMigration.InsertMasterVocher:
                    {
                        Query = @"INSERT INTO MASTER_VOUCHER
                                  (VOUCHER_NAME,
                                   VOUCHER_TYPE,
                                   VOUCHER_METHOD,
                                   PREFIX_CHAR,
                                   SUFFIX_CHAR,
                                   STARTING_NUMBER,NUMBERICAL_WITH,
                                   PREFIX_WITH_ZERO,MONTH,DURATION,NOTE)
                                VALUES
                                  (?VOUCHER_NAME,
                                   ?VOUCHER_TYPE,
                                   ?VOUCHER_METHOD,
                                   ?PREFIX_CHAR,
                                   ?SUFFIX_CHAR,
                                   ?STARTING_NUMBER,?NUMBERICAL_WITH,
                                   ?PREFIX_WITH_ZERO,?MONTH,?DURATION,?NOTE)";
                        break;
                    }

                case SQLCommand.TallyMigration.IsMasterVoucherExists:
                    {
                        Query = "SELECT VOUCHER_ID FROM MASTER_VOUCHER WHERE VOUCHER_NAME=?VOUCHER_NAME";
                        break;
                    }

                case SQLCommand.TallyMigration.MigrateLedger:
                    {
                        Query = "INSERT INTO MASTER_LEDGER(LEDGER_CODE,LEDGER_NAME,GROUP_ID,LEDGER_TYPE,LEDGER_SUB_TYPE,IS_COST_CENTER) VALUES(?LEDGER_CODE,?LEDGER_NAME,?GROUP_ID,?LEDGER_TYPE,?LEDGER_SUB_TYPE,?IS_COST_CENTER);";
                        break;
                    }
                case SQLCommand.TallyMigration.EnableCostCentreLedger:
                    {
                        Query = "UPDATE MASTER_LEDGER SET IS_COST_CENTER =1 WHERE LEDGER_ID IN (SELECT DISTINCT(LEDGER_ID) FROM VOUCHER_CC_TRANS);";
                        break;
                    }
                #endregion

                #region Master Bank
                case SQLCommand.TallyMigration.MigrateMasterBank:
                    {
                        Query = "INSERT INTO MASTER_BANK(BANK_CODE,BANK,BRANCH,ADDRESS,IFSCCODE) VALUES(?BANK_CODE,?BANK,?BRANCH,?ADDRESS,?IFSCCODE)";
                        break;
                    }
                #endregion

                #region Op Balance

                case SQLCommand.TallyMigration.IsProjectLedgerMapped:
                    {
                        Query = "SELECT PROJECT_ID FROM PROJECT_LEDGER  WHERE PROJECT_ID=?PROJECT_ID AND LEDGER_ID=?LEDGER_ID;";
                        break;
                    }
                #endregion

                #region Cost Centre Category
                case SQLCommand.TallyMigration.IsCostCentreCategoryExists:
                    {
                        Query = @"SELECT COST_CENTRECATEGORY_ID FROM MASTER_COST_CENTRE_CATEGORY WHERE COST_CENTRE_CATEGORY_NAME=?COST_CENTRE_CATEGORY_NAME";
                        break;
                    }
                case SQLCommand.TallyMigration.InsertCostCentreCategory:
                    {
                        Query = "INSERT INTO MASTER_COST_CENTRE_CATEGORY(COST_CENTRE_CATEGORY_NAME) VALUES(?COST_CENTRE_CATEGORY_NAME);";
                        break;
                    }
                case SQLCommand.TallyMigration.MapCostCentreToCostCategory:
                    {
                        Query = "INSERT INTO COSTCATEGORY_COSTCENTRE(COST_CATEGORY_ID, COST_CENTRE_ID) VALUES(?COST_CENTRECATEGORY_ID,?COST_CENTRE_ID);";
                        break;
                    }
                case SQLCommand.TallyMigration.GetCostCentreCategoryId:
                    {
                        Query = "SELECT COST_CENTRECATEGORY_ID FROM MASTER_COST_CENTRE_CATEGORY WHERE COST_CENTRE_CATEGORY_NAME=?COST_CENTRE_CATEGORY_NAME";
                        break;
                    }
                #endregion

                #region Country and Sate
                case SQLCommand.TallyMigration.GetCountryId:
                    {
                        Query = "SELECT COUNT(COUNTRY_ID) FROM MASTER_COUNTRY WHERE COUNTRY_CODE=?COUNTRY_CODE";
                        break;
                    }
                case SQLCommand.TallyMigration.InsertState:
                    {
                        Query = "INSERT INTO MASTER_STATE(STATE_NAME, COUNTRY_ID) VALUES(?STATE_NAME,?COUNTRY_ID);";
                        break;
                    }
                case SQLCommand.TallyMigration.IsSateExists:
                    {
                        Query = "SELECT STATE_ID FROM MASTER_STATE  WHERE STATE_NAME=?STATE_NAME";
                        break;
                    }
                #endregion

                #region Migration Donor
                case SQLCommand.TallyMigration.MigrateDonorAuditor:
                    {
                        Query = @"INSERT INTO MASTER_DONAUD(NAME,TYPE,STATE_ID,COUNTRY_ID,ADDRESS,PHONE,EMAIL,IDENTITYKEY) VALUES(?NAME,?TYPE,?STATE_ID,?COUNTRY_ID,?ADDRESS,?PHONE,?EMAIL,?IDENTITYKEY);";
                        break;
                    }
                case SQLCommand.TallyMigration.IsDonorExists:
                    {
                        Query = "SELECT COUNT(DONAUD_ID) AS COUNT FROM MASTER_DONAUD WHERE NAME=?NAME AND COUNTRY_ID=?COUNTRY_ID;";
                        break;
                    }
                #endregion

                #region Purpose
                case SQLCommand.TallyMigration.MigrateFCPurpose:
                    {
                        Query = "INSERT INTO MASTER_CONTRIBUTION_HEAD(CODE, FC_PURPOSE) VALUES(?CODE,?FC_PURPOSE);";
                        break;
                    }
                case SQLCommand.TallyMigration.GetPurposeId:
                    {
                        Query = "SELECT CONTRIBUTION_ID FROM MASTER_CONTRIBUTION_HEAD  WHERE FC_PURPOSE=?FC_PURPOSE";
                        break;
                    }
                case SQLCommand.TallyMigration.GetDonorAuditorId:
                    {
                        Query = "SELECT DONAUD_ID FROM MASTER_DONAUD WHERE NAME=?NAME"; //? is at the end.
                        break;
                    }
                case SQLCommand.TallyMigration.MapDonor:
                    {
                        // To avoid duplicate key error

                        //  Query = "INSERT INTO PROJECT_PURPOSE(PROJECT_ID,CONTRIBUTION_ID,TRANS_MODE) VALUES(?PROJECT_ID,?CONTRIBUTION_ID,?TRANS_MODE)";
                        Query =
                            "INSERT INTO PROJECT_PURPOSE(PROJECT_ID,CONTRIBUTION_ID,TRANS_MODE) VALUES(?PROJECT_ID,?CONTRIBUTION_ID,?TRANS_MODE) " +
                          "     ON DUPLICATE KEY UPDATE PROJECT_ID= ?PROJECT_ID , CONTRIBUTION_ID=?CONTRIBUTION_ID , TRANS_MODE=?TRANS_MODE";
                        break;
                    }
                #endregion

                #region Migrate Voucher Transaction
                case SQLCommand.TallyMigration.MigrateVoucherMaster:
                    {
                        Query = @"INSERT INTO VOUCHER_MASTER_TRANS
                                    (VOUCHER_DATE,
                                    PROJECT_ID,
                                    VOUCHER_NO,
                                    VOUCHER_TYPE,
                                    VOUCHER_DEFINITION_ID,
                                    VOUCHER_SUB_TYPE,
                                    NARRATION,
                                    CREATED_ON, CREATED_BY, CREATED_BY_NAME)
                                VALUES
                                    (?VOUCHER_DATE,
                                    ?PROJECT_ID,
                                    ?VOUCHER_NO,
                                    ?VOUCHER_TYPE,
                                    ?VOUCHER_DEFINITION_ID,
                                    ?VOUCHER_SUB_TYPE,
                                    ?NARRATION,
                                    NOW(), ?CREATED_BY, ?CREATED_BY_NAME);";
                        break;
                    }
                case SQLCommand.TallyMigration.MigrateVoucherMasterWithNameAddress:
                    {
                        Query = @"INSERT INTO VOUCHER_MASTER_TRANS
                                    (VOUCHER_DATE,
                                    PROJECT_ID,
                                    VOUCHER_NO,
                                    VOUCHER_TYPE,
                                    VOUCHER_DEFINITION_ID,
                                    VOUCHER_SUB_TYPE,
                                    NARRATION,
                                    NAME_ADDRESS,
                                    CREATED_ON, CREATED_BY, CREATED_BY_NAME)
                                VALUES
                                    (?VOUCHER_DATE,
                                    ?PROJECT_ID,
                                    ?VOUCHER_NO,
                                    ?VOUCHER_TYPE,
                                    ?VOUCHER_DEFINITION_ID,
                                    ?VOUCHER_SUB_TYPE,
                                    ?NARRATION,
                                    ?NAME_ADDRESS,
                                    NOW(), ?CREATED_BY, ?CREATED_BY_NAME);";
                        break;
                    }
                case SQLCommand.TallyMigration.UpdateDonorTransaction:
                    {
                        Query = @"UPDATE VOUCHER_MASTER_TRANS
                                    SET DONOR_ID            = ?DONOR_ID,
                                        CONTRIBUTION_TYPE   = 'F',
                                        CONTRIBUTION_AMOUNT = ?CONTRIBUTION_AMOUNT,
                                        CALCULATED_AMOUNT   = ?CALCULATED_AMOUNT,
                                        ACTUAL_AMOUNT       = ?ACTUAL_AMOUNT,
                                        PURPOSE_ID          = ?PURPOSE_ID
                                    WHERE VOUCHER_ID = ?VOUCHER_ID;";
                        break;
                    }
                case SQLCommand.TallyMigration.MigrateCostCentreTransaction:
                    {
                        Query = @"INSERT INTO VOUCHER_CC_TRANS
                                      (VOUCHER_ID, LEDGER_ID,COST_CENTRE_TABLE, COST_CENTRE_ID, AMOUNT, SEQUENCE_NO, LEDGER_SEQUENCE_NO)
                                    VALUES
                                      (?VOUCHER_ID, ?LEDGER_ID,?COST_CENTRE_TABLE, ?COST_CENTRE_ID, ?AMOUNT, ?SEQUENCE_NO, ?LEDGER_SEQUENCE_NO);";
                        break;
                    }
                case SQLCommand.TallyMigration.GetBookBeginningDate:
                    {
                        Query = "SELECT BOOKS_BEGINNING_FROM FROM ACCOUNTING_YEAR WHERE IS_FIRST_ACCOUNTING_YEAR=1;";
                        break;
                    }
                case SQLCommand.TallyMigration.UpdateLedgerOpBalaceDate:
                    {
                        Query = "UPDATE LEDGER_BALANCE SET BALANCE_DATE=?BALANCE_DATE WHERE TRANS_FLAG='OP';";
                        break;
                    }
                case SQLCommand.TallyMigration.UpdateVoucherMasterTransTableDates:
                    {
//                        Query = @"UPDATE VOUCHER_MASTER_TRANS SET CREATED_ON=VOUCHER_DATE,MODIFIED_ON=VOUCHER_DATE;
//                                  UPDATE VOUCHER_TRANS SET MATERIALIZED_ON= NULL WHERE MATERIALIZED_ON='0001-01-01';";

                        Query = @"UPDATE VOUCHER_TRANS SET MATERIALIZED_ON= NULL WHERE MATERIALIZED_ON='0001-01-01';";
                        break;
                    }
                case SQLCommand.TallyMigration.UpdateVoucherCountry:
                    {
                        Query = @"UPDATE VOUCHER_MASTER_TRANS, MASTER_DONAUD  SET CURRENCY_COUNTRY_ID =MASTER_DONAUD.COUNTRY_ID
                                   WHERE VOUCHER_MASTER_TRANS.DONOR_ID = MASTER_DONAUD.DONAUD_ID;";
                        break;
                    }
                #endregion

                #region Delete Migration by Date
                case SQLCommand.TallyMigration.FetchAllOpeningBalace:
                    {
                        Query = @"SELECT PROJECT_ID,LEDGER_ID FROM  " +
                                        "PROJECT_LEDGER ";
                        break;
                    }
                case SQLCommand.TallyMigration.DeleteOPBalance:
                    {
                        Query = "DELETE FROM LEDGER_BALANCE  WHERE TRANS_FLAG='OP';";
                        break;
                    }

                case SQLCommand.TallyMigration.UpdateDeleteOPBalance:
                    {
                        Query = @"INSERT INTO LEDGER_BALANCE(BALANCE_DATE, PROJECT_ID, LEDGER_ID, AMOUNT, TRANS_MODE, TRANS_FLAG)
                                  VALUES(?BALANCE_DATE, ?PROJECT_ID, ?LEDGER_ID, ?AMOUNT, ?TRANS_MODE, 'OP')";
                        break;
                    }
                case SQLCommand.TallyMigration.DeleteTransaction:
                    {
                        Query = @"DELETE VCCT.* FROM VOUCHER_MASTER_TRANS VMT LEFT JOIN VOUCHER_CC_TRANS VCCT ON VMT.VOUCHER_ID = VCCT.VOUCHER_ID
                                   WHERE VOUCHER_DATE<?VOUCHER_DATE;
                                  
                                  DELETE VST.* FROM VOUCHER_MASTER_TRANS VMT JOIN VOUCHER_SUB_LEDGER_TRANS VST ON VMT.VOUCHER_ID = VST.VOUCHER_ID
                                   WHERE VOUCHER_DATE<?VOUCHER_DATE;

                                  DELETE VT.*, VMT.* FROM VOUCHER_MASTER_TRANS VMT LEFT JOIN VOUCHER_TRANS VT ON VMT.VOUCHER_ID = VT.VOUCHER_ID
                                                                    WHERE VOUCHER_DATE<?VOUCHER_DATE;

                                  DELETE  FROM LEDGER_BALANCE WHERE TRANS_FLAG='TR';";
                        break;
                    }
                case SQLCommand.TallyMigration.UpdateOPDate:
                    {
                        Query = @"UPDATE LEDGER_BALANCE SET BALANCE_DATE=?BALANCE_DATE WHERE TRANS_FLAG='OP';
                                  UPDATE ACCOUNTING_YEAR SET BOOKS_BEGINNING_FROM=?BALANCE_DATE WHERE  IS_FIRST_ACCOUNTING_YEAR=1;";

                        break;
                    }

                case SQLCommand.TallyMigration.DeleteUnusedLedgers: //1= Cash,2=FD,3=Capital fund (Default Ledgers
                    {
                        Query = @"DELETE FROM
                                        PROJECT_LEDGER  WHERE
                                        LEDGER_ID NOT IN (
                                        SELECT LEDGER_ID FROM LEDGER_BALANCE) AND LEDGER_ID NOT IN(1,2,3);

                                 DELETE FROM
                                        MASTER_LEDGER  WHERE
                                        LEDGER_ID NOT IN (
                                        SELECT LEDGER_ID FROM LEDGER_BALANCE) AND LEDGER_ID NOT IN(1,2,3); ";
                        break;
                    }
                #endregion
            }
            return Query;
        }

        private string GetExportSQL()
        {
            string Query = string.Empty;
            SQLCommand.TallyExport sqlCommandId = (SQLCommand.TallyExport)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                case SQLCommand.TallyExport.FetchVoucherType:
                    {
                        Query = "SELECT VOUCHER_ID, VOUCHER_NAME," +
                                  " CASE  WHEN  VOUCHER_TYPE=1 THEN 'Receipt' WHEN VOUCHER_TYPE=2 THEN 'Payment'" +
                                  " WHEN VOUCHER_TYPE=3 THEN 'Contra'  ELSE 'Journal' END  AS 'VOUCHER_TYPE'," +
                                  " CASE  WHEN  VOUCHER_METHOD=1 THEN 'Automatic'  ELSE  'Manual' END AS  VOUCHER_METHOD," +
                                  " PREFIX_CHAR, SUFFIX_CHAR, STARTING_NUMBER, NUMBERICAL_WITH, PREFIX_WITH_ZERO, MONTH" +
                                  " FROM MASTER_VOUCHER WHERE VOUCHER_ID >4" +
                                  " ORDER BY VOUCHER_NAME ASC;";
                        break;
                    }
                case SQLCommand.TallyExport.FetchLedgerGroup:
                    {
                        Query = "SELECT lg.GROUP_CODE,\n" +
                                  "       lg.LEDGER_GROUP,\n" +
                                  "       t.ledger_group  as ParentGroup,\n" +
                                  "      IF(mn.NATURE ='Incomes','Income', mn.NATURE) AS NATURE,\n" + 
                                  "       t1.ledger_group as MainGroup,\n" +
                                  "       lg.IMAGE_ID,\n" +
                                  "       lg.ACCESS_FLAG,\n" +
                                  "       lg.SORT_ORDER\n" +
                                  "  FROM master_ledger_group lg\n" +
                                  "  LEFT JOIN MASTER_NATURE mn\n" +
                                  "    on (mn.nature_id = lg.nature_id)\n" +
                                  " INNER JOIN (SELECT GROUP_ID, IF(GROUP_ID = " + (int)Natures.Income + ",'" + Natures.Income + "',LEDGER_GROUP) AS LEDGER_GROUP\n" +
                                  "               FROM MASTER_LEDGER_GROUP\n" +
                                  "              WHERE PARENT_GROUP_ID) as t\n" +
                                  "    ON lg.parent_group_id = t.group_id\n" +
                                  " INNER JOIN (SELECT GROUP_ID, IF(GROUP_ID = " + (int)Natures.Income + ",'" + Natures.Income + "',LEDGER_GROUP) AS LEDGER_GROUP\n" +
                                  "               FROM MASTER_LEDGER_GROUP\n" +
                                  "              WHERE PARENT_GROUP_ID) as t1\n" +
                                  "    ON lg.main_group_id = t1.group_id\n" +
                                  " WHERE lg.GROUP_ID NOT IN (" + (int)Natures.Income + "," + (int)Natures.Expenses + "," + (int)Natures.Assert + "," + (int)Natures.Libilities + ")\n" +
                                  " ORDER BY LG.GROUP_ID";
                        break;
                    }
                case SQLCommand.TallyExport.FetchLedger:
                    {
                        Query = "SELECT ML.LEDGER_CODE,\n" +
                                  "       ML.LEDGER_NAME,\n" +
                                  "       MLG.LEDGER_GROUP,\n" +
                                  "       MLG.NATURE_ID,\n" +
                                  "       MLG.GROUP_CODE,\n" +
                                  "       ML.LEDGER_TYPE,\n" +
                                  "       ML.LEDGER_SUB_TYPE,\n" +
                                  "       ML.BANK_ACCOUNT_ID,\n" +
                                  "       ML.IS_COST_CENTER,\n" +
                                  "       ML.NOTES,\n" +
                                  "       ML.IS_BANK_INTEREST_LEDGER,\n" +
                                  "       BA.BANK, BA.BRANCH, MBA.ACCOUNT_HOLDER_NAME, \n" +
                                  "       IF(ML.GROUP_ID = " + (Int32)TDSDefaultLedgers.SunderyCreditors + " OR ML.GROUP_ID = " + (Int32)TDSDefaultLedgers.SundryDebtors +
                                  "       OR ML.GROUP_ID = " + (Int32)FixedLedgerGroup.BankAccounts + " , IF(ML.GROUP_ID = " + (Int32)FixedLedgerGroup.BankAccounts + ",ML.LEDGER_NAME,  TCR.NAME), '') AS NAME, \n" +
                                  "       TCR.ADDRESS, TCR.PIN_CODE, TCR.PAN_NUMBER, \n" +
                                  "       CASE WHEN BALANCE.TRANS_MODE = 'CR' THEN IFNULL(BALANCE.AMOUNT,0) ELSE - IFNULL(BALANCE.AMOUNT,0) END AS AMOUNT\n" +
                                  "  FROM MASTER_LEDGER ML\n" +
                                  "  INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                                  "    ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                                  " INNER JOIN PROJECT_LEDGER PL ON PL.LEDGER_ID = ML.LEDGER_ID AND PL.PROJECT_ID = ?PROJECT_ID" +
                                  " LEFT JOIN MASTER_BANK_ACCOUNT MBA ON MBA.LEDGER_ID = ML.LEDGER_ID AND ML.GROUP_ID = " + (Int32)FixedLedgerGroup.BankAccounts +
                                  " LEFT JOIN MASTER_BANK BA ON BA.BANK_ID = MBA.BANK_ID\n" +
                                  " LEFT JOIN TDS_CREDTIORS_PROFILE TCR ON TCR.LEDGER_ID = ML.LEDGER_ID\n" +
                                  " LEFT JOIN (SELECT LB.BALANCE_DATE, LB.BRANCH_ID, LB.PROJECT_ID, LB.LEDGER_ID, LB.AMOUNT, LB.TRANS_MODE\n" +
                                  "      FROM LEDGER_BALANCE AS LB\n" +
                                  "      LEFT JOIN (SELECT LBA.BRANCH_ID, LBA.PROJECT_ID, LBA.LEDGER_ID, MAX(LBA.BALANCE_DATE) AS BAL_DATE\n" +
                                  "      FROM LEDGER_BALANCE LBA\n" +
                                  "      WHERE 1 = 1 AND LBA.BALANCE_DATE < ?BALANCE_DATE\n" +
                                  "      GROUP BY LBA.BRANCH_ID, LBA.PROJECT_ID, LBA.LEDGER_ID) AS LB1\n" +
                                  "      ON LB.BRANCH_ID = LB1.BRANCH_ID\n" +
                                  "      AND LB.PROJECT_ID = LB1.PROJECT_ID\n" +
                                  "      AND LB.LEDGER_ID = LB1.LEDGER_ID\n" +
                                  "      WHERE LB.PROJECT_ID IN (?PROJECT_ID)\n" +
                                  "      AND LB.BALANCE_DATE = LB1.BAL_DATE) AS BALANCE\n" +
                                  "      ON BALANCE.LEDGER_ID = ML.LEDGER_ID";
                                  //"      WHERE ML.IS_BRANCH_LEDGER = 0"; On 13/12/2018, to export all ledgers which includs Branch Ledger and HO ledger
                        break;
                    }
                case SQLCommand.TallyExport.FetchCostCenter:
                    {
                        Query = "SELECT MC.COST_CENTRE_ID, ABBREVATION, COST_CENTRE_NAME, CC.COST_CENTRE_CATEGORY_NAME, NOTES FROM MASTER_COST_CENTRE  MC \n" +
                                "INNER JOIN COSTCATEGORY_COSTCENTRE CCA \n" +
                                "ON MC.COST_CENTRE_ID=CCA.COST_CENTRE_ID \n" +
                                "INNER JOIN MASTER_COST_CENTRE_CATEGORY CC \n" +
                                "ON CC.COST_CENTRECATEGORY_ID=CCA.COST_CATEGORY_ID\n"+
                                "INNER JOIN PROJECT_COSTCENTRE PC ON PC.COST_CENTRE_ID = MC.COST_CENTRE_ID AND PC.PROJECT_ID = ?PROJECT_ID";
                        break;
                    }
                case SQLCommand.TallyExport.FetchCostCategory:
                    {
                        Query = "SELECT COST_CENTRECATEGORY_ID,COST_CENTRE_CATEGORY_NAME FROM MASTER_COST_CENTRE_CATEGORY";
                        break;
                    }
                case SQLCommand.TallyExport.FetchMasterVoucher:
                    {
                        Query = "SELECT MT.VOUCHER_ID,\n" +
                                "       DATE(MT.VOUCHER_DATE) AS VOUCHER_DATE,\n" +
                                "       MT.VOUCHER_NO,\n" +
                                "       MP.PROJECT,\n" +
                                "       MT.VOUCHER_TYPE,\n" +
                                "       MV.VOUCHER_NAME,\n" +
                                "       MT.VOUCHER_SUB_TYPE,\n" +
                                "       MCH.CODE,\n" +
                                "       MCH.FC_PURPOSE,\n" +
                                "       MD.NAME,\n" +
                                "       MT.DONOR_ID,\n" +
                                "       MT.PURPOSE_ID,\n" +
                                "       MT.NAME_ADDRESS,\n" +
                                "       MT.CONTRIBUTION_TYPE,\n" +
                                "       MT.CONTRIBUTION_AMOUNT,\n" +
                                "       ME.COUNTRY AS CURRENCY_COUNTRY,\n" +
                                "       MT.EXCHANGE_RATE,\n" +
                                "       MC.COUNTRY AS EXCHANGE_COUNTRY,\n" +
                                "       MT.NARRATION,\n" +
                                "       MT.CALCULATED_AMOUNT,\n" +
                                "       MT.ACTUAL_AMOUNT,\n" +
                                "       MT.CREATED_ON,\n" +
                                "       MT.CREATED_BY,\n" +
                                "       MT.MODIFIED_ON,\n" +
                                "       MT.MODIFIED_BY,\n" +
                                "       UC.USER_NAME AS CREATED_BY_NAME,\n" +
                                "       UM.USER_NAME AS MODIFIED_BY_NAME, MT.VOUCHER_DEFINITION_ID\n" +
                                "\n" +
                                "  FROM VOUCHER_MASTER_TRANS MT\n" +
                                "  INNER JOIN MASTER_VOUCHER MV ON MV.VOUCHER_ID = MT.VOUCHER_DEFINITION_ID\n" +
                                "  LEFT JOIN MASTER_DONAUD MD\n" +
                                "    ON MD.DONAUD_ID = MT.DONOR_ID\n" +
                                "  LEFT JOIN master_contribution_head MCH\n" +
                                "    ON MCH.CONTRIBUTION_ID = MT.PURPOSE_ID\n" +
                                "  LEFT JOIN MASTER_PROJECT MP\n" +
                                "    ON MP.PROJECT_ID = MT.PROJECT_ID\n" +
                                "  LEFT JOIN MASTER_COUNTRY MC\n" +
                                "    ON MC.COUNTRY_ID = MT.EXCHANGE_COUNTRY_ID\n" +
                                "   LEFT JOIN MASTER_COUNTRY ME\n" +
                                "    ON ME.COUNTRY_ID = MT.CURRENCY_COUNTRY_ID\n" +
                                "  LEFT JOIN USER_INFO UC\n" +
                                "    ON UC.USER_ID=MT.CREATED_BY\n" +
                                "  LEFT JOIN USER_INFO UM\n" +
                                "    ON UM.USER_ID=MT.MODIFIED_BY\n" +
                                " WHERE MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n AND" +
                                "   MT.PROJECT_ID in (?PROJECT_ID)\n" +
                                "   AND MT.STATUS = 1\n" +
                                " ORDER BY MT.VOUCHER_DATE";
                        break;
                    }
                case SQLCommand.TallyExport.FetchVoucherDetails:
                    {
                        Query = "SELECT MT.VOUCHER_ID,\n" +
                                "       VT.SEQUENCE_NO,\n" +
                                "       MLG.GROUP_ID,\n" +
                                "       ML.LEDGER_ID,\n" +
                                "       ML.LEDGER_NAME,\n" +
                                "       MLG.LEDGER_GROUP,\n" +
                                "       VT.AMOUNT,\n" +
                                "       VT.TRANS_MODE,\n" +
                                "       VT.CHEQUE_NO,\n" +
                                "       VT.MATERIALIZED_ON,\n" +
                                "       VT.CHEQUE_REF_DATE,\n" +
                                "       VT.CHEQUE_REF_BANKNAME,\n" +
                                "       VT.CHEQUE_REF_BRANCH\n" +
                                "  FROM VOUCHER_MASTER_TRANS MT\n" +
                                "  LEFT JOIN VOUCHER_TRANS VT\n" +
                                "    ON MT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                "  LEFT JOIN MASTER_LEDGER ML\n" +
                                "    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                                "  INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                                  "    ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                                " WHERE MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                "   AND MT.PROJECT_ID in (?PROJECT_ID)\n" +
                                "   AND MT.STATUS = 1\n" +
                                " ORDER BY MT.VOUCHER_DATE";

                        break;
                    }
                case SQLCommand.TallyExport.FetchCCVoucherDetails:
                    {
                        Query = "SELECT MT.VOUCHER_ID,\n" +
                                "       VCC.SEQUENCE_NO,\n" +
                                "       VCC.LEDGER_SEQUENCE_NO ,\n" +
                                "       ML.LEDGER_ID,\n"+
                                "       MCC.COST_CENTRE_ID,\n" + 
                                "       MCCC.COST_CENTRE_CATEGORY_NAME,\n" +
                                "       MCC.COST_CENTRE_NAME,\n" +
                                "       VCC.AMOUNT\n" +
                                "  FROM VOUCHER_MASTER_TRANS MT\n" +
                                " INNER JOIN VOUCHER_CC_TRANS VCC\n" +
                                "    ON VCC.VOUCHER_ID = MT.VOUCHER_ID\n" +
                                "  LEFT JOIN MASTER_LEDGER ML\n" +
                                "    ON VCC.LEDGER_ID = ML.LEDGER_ID\n" +
                                "  LEFT JOIN MASTER_COST_CENTRE MCC\n" +
                                "    ON MCC.COST_CENTRE_ID = VCC.COST_CENTRE_ID\n" +
                                "  LEFT JOIN COSTCATEGORY_COSTCENTRE CCCC\n" +
                                "    ON CCCC.COST_CENTRE_ID = MCC.COST_CENTRE_ID\n" +
                                "  LEFT JOIN MASTER_COST_CENTRE_CATEGORY MCCC\n" +
                                "    ON MCCC.COST_CENTRECATEGORY_ID = CCCC.COST_CATEGORY_ID\n" +
                                " WHERE MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                "   AND MT.PROJECT_ID in (?PROJECT_ID)\n" +
                                "   AND MT.STATUS = 1";
                        break;
                    }
            }
            return Query;
        }
        #endregion
    }
}
