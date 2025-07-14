using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcMEDSync.SQL
{
    public class ImportVoucherSQL
    {
        public string GetQuery(AcMEDSync.SQL.EnumDataSyncSQLCommand.ImportVoucher queryId)
        {
            string sQuery = string.Empty;
            switch (queryId)
            {
                case EnumDataSyncSQLCommand.ImportVoucher.AuthenticateBranchCode:
                    {
                        sQuery = "SELECT BRANCH_OFFICE_CODE FROM BRANCH_OFFICE WHERE BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.AuthenticateHeadOfficeCode:
                    {
                        sQuery = "SELECT HEAD_OFFICE_CODE FROM HEAD_OFFICE WHERE HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.FetchDataBase:
                    {
                        sQuery = "SELECT HEAD_OFFICE_ID, HEAD_OFFICE_CODE, " +
                                "HEAD_OFFICE_NAME, HOST_NAME, DB_NAME, USERNAME, PASSWORD, " +
                                "CONCAT('server=', HOST_NAME, ';database=', DB_NAME, " +
                                "';uid=', USERNAME, ';pwd=', PASSWORD, ';pooling=false') AS DB_CONNECTION " +
                                "FROM HEAD_OFFICE " +
                                "WHERE HEAD_OFFICE_CODE = ?HEAD_OFFICE_CODE";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.FetchBranchOfficeId:
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
                case EnumDataSyncSQLCommand.ImportVoucher.FetchTransactions:
                    {
                        sQuery = "SELECT VOUCHER_DATE, VOUCHER_ID, PROJECT_ID\n" +
                                    "  FROM VOUCHER_MASTER_TRANS\n" +
                                    " WHERE BRANCH_ID=?BRANCH_OFFICE_ID "+
                                    " AND VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO "+
                                    " AND PROJECT_ID IN (?PROJECT_ID) ";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.InsertVoucherMaster:
                    {
                        sQuery = "INSERT INTO VOUCHER_MASTER_TRANS ( " +
                               "VOUCHER_ID, " +
                               "VOUCHER_DATE, " +
                               "PROJECT_ID, " +
                               "VOUCHER_NO, " +
                               "VOUCHER_TYPE," +
                               "DONOR_ID," +
                               "PURPOSE_ID," +
                               "CONTRIBUTION_TYPE," +
                               "CONTRIBUTION_AMOUNT," +
                               "CURRENCY_COUNTRY_ID," +
                               "EXCHANGE_RATE," +
                               "CALCULATED_AMOUNT," +
                               "ACTUAL_AMOUNT," +
                               "EXCHANGE_COUNTRY_ID," +
                               "NARRATION," +
                               "BRANCH_ID," +
                               "NAME_ADDRESS ) VALUES( " +
                               "?VOUCHER_ID, " +
                               "?VOUCHER_DATE, " +
                               "?PROJECT_ID, " +
                               "?VOUCHER_NO, " +
                               "?VOUCHER_TYPE," +
                               "?DONOR_ID," +
                               "?PURPOSE_ID," +
                               "?CONTRIBUTION_TYPE," +
                               "?CONTRIBUTION_AMOUNT," +
                               "?CURRENCY_COUNTRY_ID," +
                               "?EXCHANGE_RATE," +
                               "?CALCULATED_AMOUNT," +
                               "?ACTUAL_AMOUNT," +
                               "?EXCHANGE_COUNTRY_ID," +
                               "?NARRATION," +
                               "?BRANCH_OFFICE_ID," +
                               "?NAME_ADDRESS)";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.InsertVoucherTrans:
                    {
                        sQuery = "INSERT INTO VOUCHER_TRANS ( " +
                               "VOUCHER_ID, " +
                               "SEQUENCE_NO, " +
                               "LEDGER_ID, " +
                               "AMOUNT," +
                               "TRANS_MODE," +
                               "CHEQUE_NO," +
                               "BRANCH_ID, " +
                               "MATERIALIZED_ON ) VALUES( " +
                               "?VOUCHER_ID, " +
                               "?SEQUENCE_NO, " +
                               "?LEDGER_ID, " +
                               "?AMOUNT," +
                               "?TRANS_MODE," +
                               "?CHEQUE_NO," +
                               "?BRANCH_OFFICE_ID, " +
                               "?MATERIALIZED_ON)";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.InsertVoucherCostCentre:
                    {
                        sQuery = "INSERT INTO VOUCHER_CC_TRANS ( " +
                               "VOUCHER_ID, " +
                               "SEQUENCE_NO, "+
                               "LEDGER_ID," +
                               "COST_CENTRE_ID, " +
                               "BRANCH_ID, " +
                               "AMOUNT ) VALUES( " +
                               "?VOUCHER_ID, " +
                               "?SEQUENCE_NO, " +
                               "?LEDGER_ID, " +
                               "?COST_CENTRE_ID, " +
                               "?BRANCH_OFFICE_ID, " +
                               "?AMOUNT)";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.InsertLedger:
                    {
                        sQuery = "INSERT INTO MASTER_LEDGER ( " +
                                   "LEDGER_CODE, " +
                                   "LEDGER_NAME, " +
                                   "GROUP_ID,  " +
                                   "LEDGER_TYPE, " +
                                   "LEDGER_SUB_TYPE, " +
                                   "BRANCH_ID, " +
                                   "SORT_ID) " +
                                   "VALUES( " +
                                   "?LEDGER_CODE, " +
                                   "?LEDGER_NAME, " +
                                   "?GROUP_ID,  " +
                                   "?LEDGER_TYPE, " +
                                   "?LEDGER_SUB_TYPE, " +
                                   "?BRANCH_OFFICE_ID," +
                                    "?SORT_ID) ";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.InsertCountry:
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
                case EnumDataSyncSQLCommand.ImportVoucher.InsertDonor:
                    {
                        sQuery = "INSERT INTO MASTER_DONAUD ( " +
                               "NAME, " +
                               "PLACE," +
                               "COMPANY_NAME," +
                               "COUNTRY_ID," +
                               "PINCODE," +
                               "PHONE," +
                               "FAX," +
                               "EMAIL," +
                               "URL," +
                               "STATE," +
                               "ADDRESS," +
                               "PAN ) VALUES( " +
                               "?NAME, " +
                               "?PLACE," +
                               "?COMPANY_NAME," +
                               "?COUNTRY_ID," +
                               "?PINCODE," +
                               "?PHONE," +
                               "?FAX," +
                               "?EMAIL," +
                               "?URL," +
                               "?STATE," +
                               "?ADDRESS," +
                               "?PAN)";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.InsertCostCentre:
                    {
                        sQuery = "INSERT INTO MASTER_COST_CENTRE ( " +
                               "COST_CENTRE_NAME) " +
                               " VALUES( " +
                               "?COST_CENTRE_NAME)";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.InsertBank:
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
                case EnumDataSyncSQLCommand.ImportVoucher.InsertBankAccount:
                    {
                        sQuery = "INSERT INTO MASTER_BANK_ACCOUNT ( " +
                                    "ACCOUNT_CODE, " +
                                    "ACCOUNT_NUMBER, " +
                                    "ACCOUNT_HOLDER_NAME, " +
                                    "BANK_ID, " +
                                    "LEDGER_ID,IS_FCRA_ACCOUNT ) " +
                                    "VALUES( " +
                                    "?ACCOUNT_CODE, " +
                                    "?ACCOUNT_NUMBER, " +
                                    "?ACCOUNT_HOLDER_NAME, " +
                                    "?BANK_ID, " +
                                    "?LEDGER_ID,?IS_FCRA_ACCOUNT) ";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.InsertLedgerBank:
                    {
                        sQuery = "INSERT INTO MASTER_LEDGER ( " +
                                    "LEDGER_CODE, " +
                                    "LEDGER_NAME, " +
                                    "GROUP_ID ) " +
                                    "VALUES( " +
                                    "?LEDGER_CODE, " +
                                    "?LEDGER_NAME, " +
                                    "?GROUP_ID) ";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.InsertLedgerBalance:
                    {
                        sQuery = "INSERT INTO LEDGER_BALANCE ( " +
                                 "BALANCE_DATE, " +
                                 "PROJECT_ID, " +
                                 "LEDGER_ID, " +
                                 "AMOUNT, " +
                                 "TRANS_MODE, " +
                                 "TRANS_FLAG)VALUES( " +
                                 "?BALANCE_DATE, " +
                                 "?PROJECT_ID, " +
                                 "?LEDGER_ID, " +
                                 "?AMOUNT, " +
                                 "?TRANS_MODE, " +
                                 "?TRANS_FLAG";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.IsProjectExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM MASTER_PROJECT\n" +
                                            " WHERE PROJECT =?PROJECT";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.IsPurposeExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM MASTER_CONTRIBUTION_HEAD\n" +
                                            " WHERE FC_PURPOSE =?FC_PURPOSE";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.IsLedgerExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM MASTER_LEDGER\n" +
                                            " WHERE LEDGER_NAME =?LEDGER_NAME";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.IsCountryExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM MASTER_COUNTRY\n" +
                                            " WHERE COUNTRY=?COUNTRY";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.IsDonorExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM MASTER_DONAUD\n" +
                                            " WHERE NAME =?NAME";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.IsBankExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM MASTER_BANK\n" +
                                            " WHERE BANK =?BANK";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.IsBankAccountExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM MASTER_BANK_ACCOUNT\n" +
                                            " WHERE ACCOUNT_NUMBER =?ACCOUNT_NUMBER";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.IsLedgerBankExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM MASTER_LEDGER\n" +
                                            " WHERE LEDGER_NAME =?LEDGER_NAME";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.IsCostCentreExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM MASTER_COST_CENTRE\n" +
                                            " WHERE COST_CENTRE_NAME =?COST_CENTRE_NAME";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.GetProjectId:
                    {
                        sQuery = "SELECT PROJECT_ID\n" +
                                            "  FROM MASTER_PROJECT\n" +
                                            " WHERE PROJECT =?PROJECT";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.GetPurposeId:
                    {
                        sQuery = "SELECT CONTRIBUTION_ID\n" +
                                            "  FROM MASTER_CONTRIBUTION_HEAD\n" +
                                            " WHERE FC_PURPOSE =?FC_PURPOSE";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.GetBankId:
                    {
                        sQuery = "SELECT BANK_ID\n" +
                                            "  FROM MASTER_BANK\n" +
                                            " WHERE BANK =?BANK";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.GetCostCentreId:
                    {
                        sQuery = "SELECT COST_CENTRE_ID\n" +
                                            "  FROM MASTER_COST_CENTRE\n" +
                                            " WHERE COST_CENTRE_NAME =?COST_CENTRE_NAME";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.GetDonorId:
                    {
                        sQuery = "SELECT DONAUD_ID\n" +
                                            "  FROM MASTER_DONAUD\n" +
                                            " WHERE NAME =?NAME";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.GetLedgerId:
                    {
                        sQuery = "SELECT LEDGER_ID\n" +
                                            "  FROM MASTER_LEDGER\n" +
                                            " WHERE LEDGER_NAME =?LEDGER_NAME";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.GetCountryId:
                    {
                        sQuery = "SELECT COUNTRY_ID\n" +
                                            "  FROM MASTER_COUNTRY\n" +
                                            " WHERE COUNTRY =?COUNTRY";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.MapProjectLedger:
                    {
                        sQuery = "INSERT INTO PROJECT_LEDGER\n" +
                                "(PROJECT_ID, LEDGER_ID)\n" +
                                "VALUES(?PROJECT_ID,?LEDGER_ID) \n" +
                                " ON DUPLICATE KEY UPDATE PROJECT_ID =?PROJECT_ID, LEDGER_ID =?LEDGER_ID";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.MapProjectDonor:
                    {
                        sQuery = "INSERT INTO PROJECT_DONOR\n" +
                                "  (PROJECT_ID, DONOR_ID)\n" +
                                "VALUES(?PROJECT_ID,?DONAUD_ID)\n" +
                                " ON DUPLICATE KEY UPDATE PROJECT_ID =?PROJECT_ID, DONOR_ID =?DONAUD_ID";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.MapProjectCostcentre:
                    {
                        sQuery = "INSERT INTO PROJECT_COSTCENTRE\n" +
                                "  (PROJECT_ID, COST_CENTRE_ID)\n" +
                                "VALUES (?PROJECT_ID,?COST_CENTRE_ID)\n" +
                                "ON DUPLICATE KEY UPDATE PROJECT_ID = ?PROJECT_ID, COST_CENTRE_ID = ?COST_CENTRE_ID";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.DeleteLedgerBalance:
                    {
                        sQuery = "DELETE FROM LEDGER_BALANCE";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.DeleteVoucherTrans:
                    {
                        sQuery = "DELETE FROM VOUCHER_TRANS WHERE VOUCHER_ID=?VOUCHER_ID";
                        //sQuery = "DELETE FROM VOUCHER_TRANS\n" +
                        //            " WHERE VOUCHER_ID IN\n" +
                        //            "       (SELECT VOUCHER_ID\n" +
                        //            "          FROM VOUCHER_MASTER_TRANS\n" +
                        //            "         WHERE VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO);";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.DeleteVoucherMasterTrans:
                    {
                        sQuery = "DELETE FROM VOUCHER_MASTER_TRANS WHERE VOUCHER_ID=?VOUCHER_ID";
                        //sQuery = "DELETE FROM VOUCHER_CC_TRANS\n" +
                        //            " WHERE VOUCHER_ID IN\n" +
                        //            "       (SELECT VOUCHER_ID\n" +
                        //            "          FROM VOUCHER_MASTER_TRANS\n" +
                        //            "         WHERE VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO);";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportVoucher.DeleteVoucherCostCentre:
                    {
                        sQuery = "DELETE FROM VOUCHER_CC_TRANS WHERE VOUCHER_ID=?VOUCHER_ID";
                        //sQuery = "DELETE FROM VOUCHER_MASTER_TRANS\n" +
                        //            " WHERE VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO";
                        break;
                    }

            }
            return sQuery;
        }
    }
}
