/*  Class Name      : MessageCatalog.cs
 *  Purpose         : Declare common messages
 *  Author          : Salamon Raj M
 *  Created on      : 26-07-2013
 */

using System;

namespace Bosco.Utility
{
    public class MessageCatalog
    {
        #region Common Messages
        public class Common
        {
            public const string COMMON_MESSAGE_TITLE = "COMMON_MESSAGE_TITLE";
            public const string COMMON_DELETE_CONFIRMATION = "COMMON_DELETE_CONFIRMATION";
            public const string COMMON_WELCOME_NOTE = "COMMON_WELCOME_NOTE";
            public const string COMMON_WAIT_DIALOG_CAPTION = "COMMON_WAIT_DIALOG_CAPTION";
            public const string COMMON_PROCESS_DIALOG_CAPTION = "COMMON_PROCESS_DIALOG_CAPTION";
            public const string COMMON_GRID_EMPTY = "COMMON_GRID_EMPTY";
            public const string COMMON_EMAIL_INVALID = "COMMON_EMAIL_INVALID";
            public const string COMMON_URL_INVALID = "COMMON_URL_INVALID";
            public const string COMMON_SAVE_FAILURE = "COMMON_SAVE_FAILURE";
            public const string COMMON_SAVED_CONFIRMATION = "COMMON_SAVED_CONFIRMATION";
            public const string COMMON_DELETED_CONFIRMATION = "COMMON_DELETED_CONFIRMATION";
            public const string COMMON_PRINT_MESSAGE = "COMMON_PRINT_MESSAGE";
            public const string COMMON_INVALID_EXCEPTION = "COMMON_INVALID_EXCEPTION";
            public const string COMMON_NOSELECTION_FOR_EDIT = "COMMON_NOSELECTION_FOR_EDIT";
            public const string NO_RIGHTS_FOR_ADDITION = "NO_RIGHTS_FOR_ADDITION";
            public const string NO_RIGHTS_FOR_PRINT = "NO_RIGHTS_FOR_PRINT";
            public const string NO_RIGHTS_FOR_DELETION = "NO_RIGHTS_FOR_DELETION";
            public const string COMMON_NOSELECTION_FOR_DELETE = "COMMON_NOSELECTION_FOR_DELETE";
            public const string COMMON_DELETE_FAILURE = "COMMON_DELETE_FAILURE";
            public const string COMMON_USER_RIGHTS = "COMMON_USER_RIGHTS";
            public const string COST_CENTRE_COST_CATEGORY_UNMAP = "COST_CENTRE_COST_CATEGORY_UNMAP";
            public const string COMMON_NO_RECORDS_TO_SAVE = "COMMON_NO_RECORDS_TO_SAVE";
            public const string COMMON_NO_RECORDS_TO_EXPORT = "COMMON_NO_RECORDS_TO_EXPORT";
            public const string COMMON_IMPORT_CONFIRMATION = "COMMON_IMPORT_CONFIRMATION";
            public const string COMMON_IMPORT_SUCCESS = "COMMON_IMPORT_SUCCESS";
            //frmMain Form Messages
            public const string COMMON_ACMEERP_TITLE = "COMMON_ACMEERP_TITLE";
            public const string COMMON_ACMEERP_BACKUP_DATA = "COMMON_ACMEERP_BACKUP_DATA";
            public const string COMMON_SUCCESS_BACKUP_DATA = "COMMON_SUCCESS_BACKUP_DATA";
            public const string COMMON_FILE_BACKUP_INFO = "COMMON_FILE_BACKUP_INFO";
            public const string COMMON_FINISHED_BACKUP_DATA = "COMMON_FINISHED_BACKUP_DATA";
            public const string COMMON_RESTORE_BACKUP_DATA = "COMMON_RESTORE_BACKUP_DATA";
            public const string COMMON_RESTORE_INFO = "COMMON_RESTORE_INFO";
            public const string COMMON_RESTORING_INFO = "COMMON_RESTORING_INFO";
            public const string COMMON_RESTORE_SUCCESS_INFO = "COMMON_RESTORE_SUCCESS_INFO";
            public const string COMMON_RESTORE_FINISHED_INFO = "COMMON_RESTORE_FINISHED_INFO";
            public const string COMMON_USER_WELCOME_INFO = "COMMON_USER_WELCOME_INFO";
            public const string COMMON_QUERY_EXECUTE_SUCESS = "COMMON_QUERY_EXECUTE_SUCESS";
            public const string COMMON_QUERY_FINISHED = "COMMON_QUERY_FINISHED";
            //public const string COMMON_QUERY_FINISHED = "COMMON_QUERY_FINISHED";
            public const string COMMON_EXIT_APPLICATION = "COMMON_EXIT_APPLICATION";
            public const string COMMON_CLOSE_APPLICATION = "COMMON_CLOSE_APPLICATION";
            public const string COMMON_HO_LEDGERS_NOTAVAILABLE_INFO = "COMMON_HO_LEDGERS_NOTAVAILABLE_INFO";
            public const string COMMON_CONNECTING_PORTAL = "COMMON_CONNECTING_PORTAL";
            public const string COMMON_CHECK_INTERNET_CONNECTIVITY = "COMMON_CHECK_INTERNET_CONNECTIVITY";
            public const string COMMON_CLOSE_ACTIVE_TABS = "COMMON_CLOSE_ACTIVE_TABS";
            public const string COMMON_LICENSE_PERIOD = "COMMON_LICENSE_PERIOD";
            public const string COMMON_VOUCHER_LOCKED = "COMMON_VOUCHER_LOCKED";
            public const string COMMON_LICENSE_TO_INFO = "COMMON_LICENSE_TO_INFO";
            public const string COMMON_TRANS_PEROID_CREATE_INFO = "COMMON_TRANS_PEROID_CREATE_INFO";
            public const string COMMON_LICENSE_PEROID_EXPIRESIN_INFO = "COMMON_LICENSE_PEROID_EXPIRESIN_INFO";
            public const string COMMON_LICENSE_PEROID_EXPIRESIN_DAYS_INFO = "COMMON_LICENSE_PEROID_EXPIRESIN_DAYS_INFO";
            public const string COMMON_LICENSE_PEROID_EXPIRED_INFO = "COMMON_LICENSE_PEROID_EXPIRED_INFO";
            public const string COMMON_FINANCE_SETTING_SUCCESS_INFO = "COMMON_FINANCE_SETTING_SUCCESS_INFO";
            public const string COMMON_TDS_SETTING_SUCCESS_INFO = "COMMON_TDS_SETTING_SUCCESS_INFO";

            // Payroll messages starts
            public const string COMMON_MESSAGE_BOX_CAPTION = "COMMON_MESSAGE_BOX_CAPTION";//"Payroll";
            public const string COMMON_NO_RECORD_SELECTED = "COMMON_NO_RECORD_SELECTED"; //"No record is selected to Delete";
            public const string PAYROLL_FIXED_COMPONENT_CANNOT_DELETE = "PAYROLL_FIXED_COMPONENT_CANNOT_DELETE"; //"Standard/Default Components can't be deleted";
            public const string PAYROLL_FIXED_COMPONENT_CANNOT_EDIT = "PAYROLL_FIXED_COMPONENT_CANNOT_EDIT"; //"Standard/Default Components can't be edited";
            public const string EMPTY_SHEET = "EMPTY_SHEET";
            public const string INVALID_SHEET = "INVALID_SHEET";
            public const string FILE_OPEND = "FILE_OPEND";
            public const string LOCK_MASTERS_MESSAGE = "LOCK_MASTERS_MESSAGE";
            // Payroll messages ends

            public const string COMMON_RECEIPT_DISABLED_MESSAGE = "As per the Province regulation, Receipt Module is locked";// Province Officer has to give rights to use Receipt Module and its related activities";
            public const string COMMON_RECEIPT_ENABLED_MESSAGE = "As per the Province regulation, Receipt Module is being tracked";
            public const string COMMON_RECEIPT_DISABLED_VOUCHER_ENTRY = "As per the Province regulation, Receipt Module is locked, You can't make/modify Receipt Voucher and its related activities.";
            public const string COMMON_RECEIPT_DISABLED_VOUCHER_AMOUNT = "As per the Province regulation, Receipt Module is being tracked, You can't change Receipt Voucher Amount";
            public const string COMMON_RECEIPT_DISABLED_CHANGE_DATEPROJECT = "As per the Province regulation, Receipt Module is being tracked, You can't change Voucher Date, Project in Receipt Voucher";
            public const string COMMON_RECEIPT_DISABLED_CHANGE_RECEIPT_METHOD = "As per the Province regulation, Receipt Number generation method must be Automatic";

            public const string LOGOUT = "LOGOUT";
            public const string CANNOT_DELETE = "CANNOT_DELETE";
        }
        #endregion

        #region User
        public class User
        {
            public const string USER_VIEW = "USER_VIEW";
            public const string USER_ADD_CAPTION = "USER_ADD_CAPTION";
            public const string USER_EDIT_CAPTION = "USER_EDIT_CAPTION";
            public const string USER_CHANGE_PASSWORD = "USER_CHANGE_PASSWORD";
            public const string USER_FORGOT_PASSWORD = "USER_FORGOT_PASSWORD";
            public const string USER_LOGIN = "USER_LOGIN";
            public const string USER_INVALID = "USER_INVALID";
            public const string USER_LOGIN_SUCCESS = "USER_LOGIN_SUCCESS";
            public const string USER_SESSIONEXPIRY_INVALID = "USER_SESSIONEXPIRY_INVALID";
            public const string USER_NAME_EMPTY = "USER_NAME_EMPTY";
            public const string USER_PASSWORD_EMPTY = "USER_PASSWORD_EMPTY";
            public const string USER_CONFIRM_PASSWORD_EMPTY = "USER_CONFIRM_PASSWORD_EMPTY";
            public const string USER_PASSWORD_UNMATCHED = "USER_PASSWORD_UNMATCHED";
            public const string USER_SAVED_SUCCESS = "USER_SAVED_SUCCESS";
            public const string USER_DELETE_SUCCESS = "USER_DELETE_SUCCESS";
            public const string USER_PRINT_CAPTION = "USER_PRINT_CAPTION";
            public const string USER_FIRST_NAME_EMPTY = "USER_FIRST_NAME_EMPTY";
            public const string USER_CURRENT_PASSWORD_EMPTY = "USER_CURRENT_PASSWORD_EMPTY";
            public const string USER_NEW_PASSWORD_MISMATCH = "USER_NEW_PASSWORD_MISMATCH";
            public const string USER_CURRENT_PASSWORD_FAIL = "USER_CURRENT_PASSWORD_FAIL";
            public const string USER_NEW_PASSWORD_EMPTY = "USER_NEW_PASSWORD_EMPTY";
            public const string USER_RESET_PASSWORD_SUCCESS = "USER_RESET_PASSWORD_SUCCESS";
            public const string USER_RIGHTS_RESTART_CONFIRM = "USER_RIGHTS_RESTART_CONFIRM";
        }

        public class UserRole
        {
            //public const string USERROLE_DELETE_SUCCESS = "USERROLE_DELETE_SUCCESS";
            // public const string USERROLE_SAVE_SUCCESS = "USERROLE_SAVE_SUCCESS";
            // public const string USERROLE_CAPTION = "USERROLE_CAPTION";
            //public const string USERROLE_NAME = "USERROLE_NAME";
            // public const string USERROLE_DELETE = "USERROLE_DELETE";
            //public const string USERROLE_DELETE_ASSOCIATION = "USERROLE_DELETE_ASSOCIATION";
            //  public const string USERROLE_EXITS = "USERROLE_EXITS";
            public const string USER_ROLE_ADD_CAPTION = "USER_ROLE_ADD_CAPTION";
            public const string USER_ROLE_EDIT_CAPTION = "USER_ROLE_EDIT_CAPTION";
            public const string USER_ROLE_DELETE_SUCCESS = "USER_ROLE_DELETE_SUCCESS";
            public const string USERROLE_EMPTY = "USERROLE_EMPTY";
            public const string USER_ROLE_SAVE_SUCCESS = "USER_ROLE_SAVE_SUCCESS";
            public const string USER_ROLE_PRINT_CAPTION = "USER_ROLE_PRINT_CAPTION";
            public const string USER_ROLE_DELETE_FAILS = "USER_ROLE_DELETE_FAILS";

        }
        #endregion

        #region Master
        public static class Master
        {
            public class Module
            {
                public const string MODULE_FINANCE_TITLE = "MODULE_FINANCE_TITLE";
                public const string MODULE_SETTING_TITLE = "MODULE_SETTING_TITLE";
                public const string MODULE_DATA_UTILITY_TITLE = "MODULE_DATA_UTILITY_TITLE";
                public const string MODULE_USER_MANAGEMENT = "MODULE_USER_MANAGEMENT";
                public const string MODULE_NETWOKING_TITTLE = "MODULE_NETWOKING_TITTLE";
                public const string MODULE_STOCK_TITTLE = "MODULE_STOCK_TITTLE";
                public const string MODULE_FIXED_ASSET_TITTLE = "MODULE_FIXED_ASSET_TITTLE";
                public const string MODULE_STATUTORY_TITTLE = "MODULE_STATUTORY_TITTLE";

                // Common Shortcuts Messages
                public const string MODULE_SET_VISIBLE_CLOSE_ALL_TABS = "MODULE_SET_VISIBLE_CLOSE_ALL_TABS";
                public const string MODULE_SET_VISIBLE_DATE_TAB = "MODULE_SET_VISIBLE_DATE_TAB";
                public const string MODULE_SET_VISIBLE_PROJECT_TAB = "MODULE_SET_VISIBLE_PROJECT_TAB";
                public const string MODULE_SET_VISIBLE_CONFIGURATION_TAB = "MODULE_SET_VISIBLE_CONFIGURATION_TAB";
            }

            public class Bank
            {
                public const string BANK_DELETE_SUCCESS = "BANK_DELETE_SUCCESS";
                public const string BANK_SAVE_SUCCESS = "BANK_SAVE_SUCCESS";
                public const string BANK_CODE_EMPTY = "BANK_CODE_EMPTY";
                public const string BANK_NAME_EMPTY = "BANK_NAME_EMPTY";
                public const string BANK_BRANCH_EMPTY = "BANK_BRANCH_EMPTY";
                public const string BANK_SAVE_FAILURE = "BANK_SAVE_FAILURE";
                public const string BANK_ADD_CAPTION = "BANK_ADD_CAPTION";
                public const string BANK_EDIT_CAPTION = "BANK_EDIT_CAPTION";
                public const string BANK_ACCOUNT = "BANK_ACCOUNT";
                public const string BANK_OPERATING_CAPTION = "BANK_OPERATING_CAPTION";
                public const string BANK_DELETE = "BANK_DELETE";
                public const string BANK_DELETE_ASSOCIATION = "BANK_DELETE_ASSOCIATION";
                public const string BANK_PRINT_CAPTION = "BANK_PRINT_CAPTION";
                public const string TDS_LEDGER_ADD_CAPTION = "TDS_LEDGER_ADD_CAPTION";
                public const string TDS_LEDGER_EDIT_CAPTION = "TDS_LEDGER_EDIT_CAPTION";

            }

            public class GST
            {
                public const string GST_PRINT_CAPTION = "GST_PRINT_CAPTION";

                public const string GST_INVOICE_DETAILS_IS_NOT_AVAILABLE = "GST INVOICE DETAILS IS NOT AVAILABLE";

            }

            public class State
            {
                public const string STATE_DELETE_SUCCESS = "STATE_DELETE_SUCCESS";
                public const string STATE_SAVE_SUCCESS = "STATE_SAVE_SUCCESS";
                public const string STATE_NAME_EMPTY = "STATE_NAME_EMPTY";
                public const string STATE_COUNTRY_NAME_EMPTY = "STATE_COUNTRY_NAME_EMPTY";
                public const string STATE_SAVE_FAILURE = "STATE_SAVE_FAILURE";
                public const string STATE_ADD_CAPTION = "STATE_ADD_CAPTION";
                public const string STATE_EDIT_CAPTION = "STATE_EDIT_CAPTION";
                public const string STATE_DELETE = "STATE_DELETE";
                public const string STATE_PRINT_CAPTION = "STATE_PRINT_CAPTION";
            }

            public class Audit
            {
                public const string AUDIT_DELETE_SUCCESS = "AUDIT_DELETE_SUCCESS";
                public const string AUDIT_SAVE_SUCCESS = "AUDIT_SAVE_SUCCESS";
                public const string AUDIT_NAME_EMPTY = "AUDIT_NAME_EMPTY";
                public const string AUDIT_SAVE_FAILURE = "AUDIT_SAVE_FAILURE";
                public const string AUDIT_ADD_CAPTION = "AUDIT_ADD_CAPTION";
                public const string AUDIT_EDIT_CAPTION = "AUDIT_EDIT_CAPTION";
                public const string AUDIT_DELETE = "AUDIT_DELETE";
                public const string AUDIT_PRINT_CAPTION = "AUDIT_PRINT_CAPTION";
                public const string AUDITTYPE_PRINT_CAPTION = "AUDITTYPE_PRINT_CAPTION";
            }

            public class Budget
            {
                public const string BUDGET_ADD_CAPTION = "BUDGET_ADD_CAPTION";
                public const string BUDGET_EDIT_CAPTION = "BUDGET_EDIT_CAPTION";
                public const string BUDGET_ANNUAL_ADD_CAPTION = "BUDGET_ANNUAL_ADD_CAPTION";
                public const string BUDGET_ANNUAL_EDIT_CAPTION = "BUDGET_ANNUAL_EDIT_CAPTION";
                public const string BUDGET_NAME_EMPTY = "BUDGET_NAME_EMPTY";
                public const string BUDGET_TYPE_EMPTY = "BUDGET_TYPE_EMPTY";
                public const string BUDGET_DATE_FROM_EMPTY = "BUDGET_DATE_FROM_EMPTY";
                public const string BUDGET_DATE_TO_EMPTY = "BUDGET_DATE_TO_EMPTY";
                public const string BUDGET_PROJECT_EMPTY = "BUDGET_PROJECT_EMPTY";
                public const string BUDGET_LEDGER_NOT_MAPPED_TO_PROJECT = "BUDGET_LEDGER_NOT_MAPPED_TO_PROJECT";
                public const string BUDGET_IMPORT_PERCENTAGE_EMPTY = "BUDGET_IMPORT_PERCENTAGE_EMPTY";
                public const string BUDGET_IMPORT_NO_BUDGET_FOR_PROJECT = "BUDGET_IMPORT_NO_BUDGET_FOR_PROJECT";
                public const string BUDGET_CANNOT_ACTIVE_DELETE = "BUDGET_CANNOT_ACTIVE_DELETE";
                public const string PGCGRANDTOTAL_CAPTION = "PGCGRANDTOTAL_CAPTION";
                public const string BUDGET_ALREADY_MADE = "BUDGET_ALREADY_MADE";
                public const string BUDGET_NO_PROJECTS = "BUDGET_NO_PROJECTS";
                public const string BUDGET_PERCENTAGE_VALIDATION = "BUDGET_PERCENTAGE_VALIDATION";
                public const string ACTIVE_BUDGET_IS_MADE = "ACTIVE_BUDGET_IS_MADE";
                public const string BUDGET_DETAILS_ARE_SAVED = "BUDGET_DETAILS_ARE_SAVED";
                public const string MAP_LEDGERS_TO_PROJECT = "MAP_LEDGERS_TO_PROJECT";
                public const string MAPPED_LEDGERS_TO_PROJECT_TO_ALLOTFUND = "MAPPED_LEDGERS_TO_PROJECT_TO_ALLOTFUND";
                public const string BUDGET_NAME_SHOULD_NOT_EMPTY = "BUDGET_NAME_SHOULD_NOT_EMPTY";
                public const string PROJECT_IS_NOT_CREATED_FOR_MAKING_BUDGET = "PROJECT_IS_NOT_CREATED_FOR_MAKING_BUDGET";
                public const string ALLOT_FUND_AND_TRY_AGAIN = "ALLOT_FUND_AND_TRY_AGAIN";
                public const string ANNUAL_BUDGET = "ANNUAL_BUDGET";
                public const string PERIODL_BUDGET = "PERIODL_BUDGET";
                public const string BUDGET_FOR = "BUDGET_FOR";
                public const string OPEN_THE_FILE = "OPEN_THE_FILE";
                public const string BUDGET_HAS_TRANSACTION = "BUDGET_HAS_TRANSACTION";
                public const string NO_SPLIT_AMOUNT = "NO_SPLIT_AMOUNT";
                public const string FILE_OPEN_ALREADY_CLOSE = "FILE_OPEN_ALREADY_CLOSE";
            }

            public class Country
            {
                public const string COUNTRY_SAVE_SUCCESS = "COUNTRY_SAVE_SUCCESS";
                public const string COUNTRY_DELETE_SUCCESS = "COUNTRY_DELETE_SUCCESS";
                public const string COUNTRY_DELETE_FAILURE = "COUNTRY_DELETE_FAILURE";
                public const string COUNTRY_NAME_EXIST = "COUNTRY_NAME_EXIST";
                public const string COUNTRY_CODE_EMPTY = "COUNTRY_CODE_EMPTY";
                public const string CURRENCY_SYMBOL_EMPTY = "CURRENCY_SYMBOL_EMPTY";
                public const string CURRENCY_CODE_EMPTY = "CURRENCY_CODE_EMPTY";
                public const string COUNTRY_ADD_CAPTION = "COUNTRY_ADD_CAPTION";
                public const string COUNTRY_EDIT_CAPTION = "COUNTRY_EDIT_CAPTION";
                public const string COUNTRY_NAME_EMPTY = "COUNTRY_NAME_EMPTY";
                public const string COUNTRY_ASSOCIATION = "COUNTRY_ASSOCIATION";
                public const string COUNTRY_PRINT_CAPTION = "COUNTRY_PRINT_CAPTION";
                public const string CURRENCY_NAME_EMPTY = "CURRENCY_NAME_EMPTY";
            }

            public class Mapping
            {
                public const string MAPPING_NEGATIVE_BALANCE = "MAPPING_NEGATIVE_BALANCE";
                public const string LEDGER_MAPPING_SUCCESS = "LEDGER_MAPPING_SUCCESS";
                public const string PROJECT_MAPPING_SUCESS = "PROJECT_MAPPING_SUCCESS";
                public const string COST_CENTER_MAPPING_SUCESS = "COST_CENTER_MAPPING_SUCESS";
                public const string MAPPING_DONOR_SUCCESS = "MAPPING_DONOR_SUCCESS";
                public const string MAPPING_CONTRIBUTION_SUCCESS = "MAPPING_CONTRIBUTION_SUCCESS";
                public const string MAPPING_TRANSACTION_MADE_ALREADY = "MAPPING_TRANSACTION_MADE_ALREADY";
                public const string MAPPING_MAKE_AMOUNT_ZERO = "MAPPING_MAKE_AMOUNT_ZERO";
                public const string MAPPING_BUDGET_MADE = "MAPPING_BUDGET_MADE";
                public const string MAPPING_FD_LEDGER_RESTRICTION = "MAPPING_FD_LEDGER_RESTRICTION";
                public const string NO_RECORD = "NO_RECORD";
                public const string BOOK_BEGINNING_DATE_EMPTY = "BOOK_BEGINNING_DATE_EMPTY";
                public const string COST_CENTER_TYPE_EMPTY = "COST_CENTER_TYPE_EMPTY";
                public const string TRANSACTION_FD = "TRANSACTION_FD";
                public const string MASTER_VOUCHER_MAPPING = "MASTER_VOUCHER_MAPPING";
                public const string UNMAP_CASH_LEDGER = "UNMAP_CASH_LEDGER";
                public const string TRANSACTION_MADE_ALREADY_FOR_LEDGER = "TRANSACTION_MADE_LEDGER";
                public const string LEDGER_MAPPED = "LEDGER_MAPPED";
                public const string MAPPING_LEDGER_CANNOT_SET_OP_FD_LEDGER = "MAPPING_LEDGER_CANNOT_SET_OP_FD_LEDGER";
                public const string FD_UNMAPPING = "FD_UNMAPPING";
                public const string FD_CANNOT_UNMAP_MAKE_ZERO = "FD_CANNOT_UNMAP_MAKE_ZERO";
                public const string FD_OPENING_UNMAPPING = "FD_OPENING_UNMAPPING";
                public const string FD_INVESTMENT_UNMAPPING = "FD_INVESTMENT_UNMAPPING";
                public const string MAPPING_COST_CATEGORY_SUCCESS = "MAPPING_COST_CATEGORY_SUCCESS";
                public const string LOCAL_PROJECT_RESTRICTED_WITH_BANK_ACCOUNT = "LOCAL_PROJECT_RESTRICTED_WITH_BANK_ACCOUNT";
                public const string DEFERENT_LEGAL_ENTITY_RESTRICTED_WITH_BANK_ACCOUNT = "DEFERENT_LEGAL_ENTITY_RESTRICTED_WITH_BANK_ACCOUNT";
                public const string SELECT_ANYTDS_LEDGER = "SELECT_ANYTDS_LEDGER";
                public const string DEDUCT_TDS = "DEDUCT_TDS";
                public const string TDS_BALANCE = "TDS_BALANCE";
                public const string PARTY_PAYMENTS = "PARTY_PAYMENTS";
                public const string BALANCE = "BALANCE";
                public const string TDSPAYMENTS = "TDSPAYMENTS";
                public const string BANKACCOUNT_NOT_IN_SAME_LEGALENTITY = "BANKACCOUNT_NOT_IN_SAME_LEGALENTITY";
                public const string SELECT_THE_PURPOSE_TYPE = "SELECT_THE_PURPOSE_TYPE";
                public const string NO_RECORDS_TO_SAVE = "NO_RECORDS_TO_SAVE";
                public const string NO_RECORD_IS_AVAILABLE = "NO_RECORD_IS_AVAILABLE";
                public const string PURPOSE_MAPPED = "PURPOSE_MAPPED";
                public const string UNSELECT_ALL = "UNSELECT_ALL";
                public const string SELECT_ALL = "SELECT_ALL";
                public const string BALANCE_UPDATED = "BALANCE_UPDATED";
                public const string NO_DIFFERENCE = "NO_DIFFERENCE";
                public const string LEDGER_MERGED_WITH = "LEDGER_MERGED_WITH";
                public const string LEDGER_MERGED = "LEDGER_MERGED";
                public const string MERGED_SUCCESSFULLY = "MERGED_SUCCESSFULLY";
                public const string UPDATING_BALANCE = "UPDATING_BALANCE";
                public const string LEDGERS_TOBE_MERGED = "LEDGERS_TOBE_MERGED";
                public const string CANNOT_SELECT_ALL_LEDGER = "CANNOT_SELECT_ALL_LEDGER";
                public const string CANNOT_CHANGE_FDLEDGE_BANKACCOUNT = "CANNOT_CHANGE_FDLEDGE_BANKACCOUNT";
                public const string CANNOT_CHANGE_PROJECT_FDLEDGER = "CANNOT_CHANGE_PROJECT_FDLEDGER";
                public const string CANNOT_WITHDRAW = "CANNOT_WITHDRAW";
                public const string ACCOUNT_NUMBER_AVAILABLE = "ACCOUNT_NUMBER_AVAILABLE";
                public const string STARTDATE_LESSTHAN_INVESTMENTDATE = "STARTDATE_LESSTHAN_INVESTMENTDATE";
                public const string RENEWAL_MADE_ALREADY = "RENEWAL_MADE_ALREADY";
                public const string INTEREST_DATE_EMPTY = "INTEREST_DATE_EMPTY";
                public const string INTEREST_DATE_IS_LESS_THAN = "INTEREST_DATE_IS_LESS_THAN";
                public const string INTEREST_DATE_IS_GREATER_THAN = "INTEREST_DATE_IS_GREATER_THAN";
                public const string WANT_TO_PROCEED = "WANT_TO_PROCEED";
                public const string WITHDRAW = "WITHDRAW";
                public const string FD_POST_INTEREST_MODIFY = "FD_POST_INTEREST_MODIFY";
                public const string TYPE = "TYPE";
                public const string NO_OF_POSTS = "NO_OF_POSTS";
                public const string LAST_POST_ON = "LAST_POST_ON";
                public const string CLOSED_ON = "CLOSED_ON";
                public const string WITHDRAW_AMOUNT = "WITHDRAW_AMOUNT";
                public const string PRINCIPAL_AMOUNT = "PRINCIPAL_AMOUNT";
                public const string RENEWAL_ON = "RENEWAL_ON";
                public const string ACCUMULATED_INTEREST = "ACCUMULATED_INTEREST";
                public const string WITHDRAW_DATE_GREATER_THAN_RENEWAL_DATE = "WITHDRAW_DATE_GREATER_THAN_RENEWAL_DATE";
                public const string WITHDRAW_DATE_GREATER_THAN_CREATED_DATE = "WITHDRAW_DATE_GREATER_THAN_CREATED_DATE";
                public const string LEDGER_MAPPING_LOCKED_HEADOFFICE_ADMIN = "LEDGER_MAPPING_LOCKED_HEADOFFICE_ADMIN";
                public const string CONTACT_HEADOFFICE_ADMIN_FURTHER_ASSISTANCE = "CONTACT_HEADOFFICE_ADMIN_FURTHER_ASSISTANCE";
                public const string LEDGER_UNMAPPING_HEADOFFICE_ADMIN = "LEDGER_UNMAPPING_HEADOFFICE_ADMIN";
                public const string SHOWALL_LEDGERS_BLUE = "SHOWALL_LEDGERS_BLUE";
                public const string SHOWALL_LEDGERS_DARKGREEN = "SHOWALL_LEDGERS_DARKGREEN";

                //User Control all Forms Messages
                public const string ACC_MAP_OPENNNG_BAL_SET_CANNOT_UNMAP_LEDGER = "ACC_MAP_OPENNNG_BAL_SET_CANNOT_UNMAP_LEDGER";
                public const string ACC_MAP_INVESTMENT_MADE_CANNOT_UNMAP_LEDGER = "ACC_MAP_INVESTMENT_MADE_CANNOT_UNMAP_LEDGER";
                public const string ACC_MAP_AVAIL_ZERO_BAL_UNMAP_LEDGER = "ACC_MAP_AVAIL_ZERO_BAL_UNMAP_LEDGER";
                public const string ACC_MAP_TRANS_MADE_LEDGER_CANNOT_UNMAP_LEDGER = "ACC_MAP_TRANS_MADE_LEDGER_CANNOT_UNMAP_LEDGER";
                public const string ACC_MAP_OPENNING_BAL_SET_FOR_LEDGER = "ACC_MAP_OPENNING_BAL_SET_FOR_LEDGER";
                public const string ACC_MAP_CANNOT_UNMAP_INVERST_MADE_LEDGER = "ACC_MAP_CANNOT_UNMAP_INVERST_MADE_LEDGER";
                public const string ACC_MAP_CANNOT_UNMAP_MAKE_AMOUNT_ZERO = "ACC_MAP_CANNOT_UNMAP_MAKE_AMOUNT_ZERO";
                public const string ACC_MAP_TRANS_MADE_CANNOT_UNMAP = "ACC_MAP_TRANS_MADE_CANNOT_UNMAP";

                //UcAssetJornal
                public const string UCASSET_CASH_BANK_LEDGER_EMPTY = "UCASSET_CASH_BANK_LEDGER_EMPTY";
                public const string UCASSET_CASH_BANK_AMOUNT_EMPTY = "UCASSET_CASH_BANK_AMOUNT_EMPTY";
            }

            public class FinanceUIControls
            {
                public const string UCASSET_LIST_DELETE_CONFIRMATION_INFO = "UCASSET_LIST_DELETE_CONFIRMATION_INFO";
                public const string UCASSET_LIST_CANNOT_DELETE_INFO = "UCASSET_LIST_CANNOT_DELETE_INFO";
                public const string UCASSET_LIST_ASSETID_EMPTY = "UCASSET_LIST_ASSETID_EMPTY";
                public const string UCASSET_LIST_ITEM_SOLD_DISPOSE_DONATE_INFO = "UCASSET_LIST_ITEM_SOLD_DISPOSE_DONATE_INFO";
                public const string UCASSET_LIST_NOITEM_SHOULDBE_GREATETHAN_ZERO = "UCASSET_LIST_NOITEM_SHOULDBE_GREATETHAN_ZERO";
                public const string UCASSET_LIST_LOCATINS_ALREADY_FILLED_INFO = "UCASSET_LIST_LOCATINS_ALREADY_FILLED_INFO";
                public const string UCASSET_LIST_ASSETID_DUPLICATED_INFO = "UCASSET_LIST_ASSETID_DUPLICATED_INFO";
                public const string UCASSET_LIST_SALVAGE_VALUE_INFO = "UCASSET_LIST_SALVAGE_VALUE_INFO";
            }

            public class UserManagementForms
            {
                public const string USERNAME_PASSWORD_CANNOT_SAME_FOR_ADMIN_USER = "USERNAME_PASSWORD_CANNOT_SAME_FOR_ADMIN_USER";
                public const string NO_PROJECT = "NO_PROJECT";
                public const string PROJECT_NOT_SELECT = "PROJECT_NOT_SELECT";
                public const string USER_ROLE_CANNOT_EMPTY = "USER_ROLE_CANNOT_EMPTY";
                public const string NO_ACCESS_EDIT_USER_ROLE = "NO_ACCESS_EDIT_USER_ROLE";
                public const string NO_RIGHTS_TO_DELETE_USER = "NO_RIGHTS_TO_DELETE_USER";
                public const string NO_ACCESS_EDIT_USER = "NO_ACCESS_EDIT_USER";
            }

            public class AcmeLogin
            {
                public const string LICENSE_KEY_NOT_AVAILABLE = "LICENSE_KEY_NOT_AVAILABLE";
                public const string UILANGUAGE_CAPTION = "UILANGUAGE_CAPTION";
            }

            public class LoginDashBoard
            {
                public const string SHOWALL_CAPTION = "SHOWALL_CAPTION";
                public const string LATEST_CATPTION = "LATEST_CATPTION";
                public const string TICKET_STATUS_CAPTION = "TICKET_STATUS_CAPTION";
                public const string DATE_CAPTION = "DATE_CAPTION";
                public const string MESSAGE_CAPTION = "MESSAGE_CAPTION";
                public const string SUBJECT_CAPTION = "SUBJECT_CAPTION";
                public const string PORATL_MESSAGE_CAPTION = "PORATL_MESSAGE_CAPTION";
                public const string DESCRIPTION_CAPTION = "DESCRIPTION_CAPTION";
                public const string TICKET_NO_CAPTION = "TICKET_NO_CAPTION";
            }

            public class DataUtilityForms
            {
                //frmUploadBranchOfficeDBase.cs
                public const string DATABASE_HAS_BEEN_UPLOADED_SUCCESS = "DATABASE_HAS_BEEN_UPLOADED_SUCCESS";
                public const string ERROR_UPLOADING_DATABASE = "ERROR_UPLOADING_DATABASE";
                public const string NO_INTERNET_CONNECTION = "NO_INTERNET_CONNECTION";
                //frmUpdateLicense.cs
                public const string LICENSE_KEY_UPDATE = "LICENSE_KEY_UPDATE";
                public const string LICENSE_KEY_SELECT = "LICENSE_KEY_SELECT";
                public const string LICENSE_KEY_NOT_UPDATE_CLOSE = "LICENSE_KEY_NOT_UPDATE_CLOSE";
                public const string CONFIRMATION_DELETE = "CONFIRMATION_DELETE";
                //frmTemplates.cs
                public const string DOWNLOAD_SUCCESS = "DOWNLOAD_SUCCESS";
                public const string DOWNLOAD_TEMPLATE = "DOWNLOAD_TEMPLATE";

                //frmTallyMigration.cs
                public const string CONNECTING_TALLY = "CONNECTING_TALLY";
                public const string COULD_NOT_START_TALLY_MIGRATION = "COULD_NOT_START_TALLY_MIGRATION";
                public const string FETCHING_COUNTRY_FROM_TALLY = "FETCHING_COUNTRY_FROM_TALLY";
                public const string FETCHING_STATE_FROM_TALLY = "FETCHING_STATE_FROM_TALLY";
                public const string FETCHING_PURPOSE_LIST_FROM_TALLY = "FETCHING_PURPOSE_LIST_FROM_TALLY";
                public const string FETCHING_VOUCHER_TYPE_FROM_TALLY = "FETCHING_VOUCHER_TYPE_FROM_TALLY";
                public const string FETCHING_GROUP_DETAILS_FROM_TALLY = "FETCHING_GROUP_DETAILS_FROM_TALLY";
                public const string FETCHING_COST_CATEGORY_FROM_TALLY = "FETCHING_COST_CATEGORY_FROM_TALLY";
                public const string FETCHING_COST_CENTER_FROM_TALLY = "FETCHING_COST_CENTER_FROM_TALLY";
                public const string FETCHING_DONOR_DETAILS_FROM_TALLY = "FETCHING_DONOR_DETAILS_FROM_TALLY";
                public const string FETCHING_LEDGER_DETAILS_FROM_TALLY = "FETCHING_LEDGER_DETAILS_FROM_TALLY";
                public const string FETCHING_VOUCHER_DETAILS_FROM_TALLY = "FETCHING_VOUCHER_DETAILS_FROM_TALLY";
                public const string COULD_NOT_PROCESS_DATA_MIGRATION = "COULD_NOT_PROCESS_DATA_MIGRATION";
                public const string COULD_NOT_ESTABILISH_CONNECTION_WITH_TALLY = "COULD_NOT_ESTABILISH_CONNECTION_WITH_TALLY";
                public const string TALLY_MIGRATION_MODE_OPTION = "TALLY_MIGRATION_MODE_OPTION";
                //public const string TALLY_MIGRATION_NONE = "TALLY_MIGRATION_NONE";
                //public const string TALLY_MIGRATION_END = "TALLY_MIGRATION_END";

                //frmRestoreMultipleDB.cs
                public const string SELECT_DB_TO_RESTORE = "SELECT_DB_TO_RESTORE";
                public const string PROVIDE_DB_NAME_RESTORE = "PROVIDE_DB_NAME_RESTORE";
                public const string SELECT_LICENSE_KEY_FOR_DB = "SELECT_LICENSE_KEY_FOR_DB";
                public const string RESTORING_INFO = "RESTORING_INFO";
                public const string CONFIRMATION_FOR_DB_REPLACE = "CONFIRMATION_FOR_DB_REPLACE";
                public const string SELECTED_FILE_INVALID = "SELECTED_FILE_INVALID";
                public const string DB_RESORE_SUCCESS = "DB_RESORE_SUCCESS";
                public const string DB_THE_FILE = "DB_THE_FILE";
                public const string SELECT_MULTIPLE_DB = "SELECT_MULTIPLE_DB";

                //frmPostTicket.cs
                public const string TICKET_SAVE_SUCCESS = "TICKET_SAVE_SUCCESS";
                public const string SUBJUECT_EMPTY = "SUBJUECT_EMPTY";
                public const string DESCRIPTION_EMPTY = "DESCRIPTION_EMPTY";

                //frmMapMigration.cs
                public const string SELECT_LEDGERS_TO_BE_MAPPED = "SELECT_LEDGERS_TO_BE_MAPPED";
                public const string SELECT_ACMEERP_LEDGER_TO_MAP = "SELECT_ACMEERP_LEDGER_TO_MAP";

                //frmManageMultiDB.cs
                public const string BRANCH_DB_SAVE = "BRANCH_DB_SAVE";
                public const string BRANCH_DB_DELETE_CONFIRMATION = "BRANCH_DB_DELETE_CONFIRMATION";
                public const string ONCE_DB_DELETE_CANNOT_RETRIVE = "ONCE_DB_DELETE_CANNOT_RETRIVE";
                public const string BRANCH_DB_DELETED = "BRANCH_DB_DELETED";
                public const string CURRENT_DB_CANNOT_BE_DELETED = "CURRENT_DB_CANNOT_BE_DELETED";
                public const string BRANCH_NAME_EMPTY = "BRANCH_NAME_EMPTY";
                public const string BRANCH_NAME_EXISTS_ALREADY = "BRANCH_NAME_EXISTS_ALREADY";

                //frmExcelSupport.cs
                public const string IMPORTING_PROSPECTS = "IMPORTING_PROSPECTS";
                public const string IMPORTING_PROSPECTS_SUCCESS = "IMPORTING_PROSPECTS_SUCCESS";
                public const string INVALID_EXCEL_FILE = "INVALID_EXCEL_FILE";
                public const string PROSPECTS_EMPTY_lIST = "PROSPECTS_EMPTY_lIST";
                public const string IMPORTING_DONORS = "IMPORTING_DONORS";
                public const string DONOR_IMPORTING_SUCCESS = "IMPORTING_DONORS";
                public const string TRANSACTION_IMPORTING_SUCCESS = "TRANSACTION_IMPORTING_SUCCESS";
                public const string IMPORTING_TRANSACTIONS = "IMPORTING_TRANSACTIONS";

                //frmDeleteUnusedLedger.cs
                public const string DELETE_UNUSED_lEDGER_CONFIRMATION = "DELETE_UNUSED_lEDGER_CONFIRMATION";
                public const string UNUSED_LEDGER_DELETE_SUCCESS = "UNUSED_LEDGER_DELETE_SUCCESS";
                public const string NO_LEDGER_SELECT = "NO_LEDGER_SELECT";
                public const string LEDGER_DETAIL_NOT_AVAILABLE_TO_PROCEED = "LEDGER_DETAIL_NOT_AVAILABLE_TO_PROCEED";

                //frmDeletedunusedLedgerGroups.cs
                public const string DELETE_UNUSED_GROUPS_CONFIRMATION = "DELETE_UNUSED_GROUPS_CONFIRMATION";
                public const string LEDGER_GROUPS_DELETE_SUCCESS = "LEDGER_GROUPS_DELETE_SUCCESS";
                public const string NO_LEDGER_GROUPS_SELECT = "NO_LEDGER_GROUPS_SELECT";
                public const string DELETE_TRANSACTION_SUCCESS = "DELETE_TRANSACTION_SUCCESS";

                //frmConnectDatabase.cs
                public const string CHANGE_DB_CONFIRMATION = "CHANGE_DB_CONFIRMATION";
                public const string DB_CHANGED_SUCCESS = "DB_CHANGED_SUCCESS";
                public const string DB_CHANGED_CONFIRMATION = "DB_CHANGED_CONFIRMATION";//CHECK

                //frmBackupDB.cs
                public const string NO_DB_EXITS_IN_CONFIG_FILE = "NO_DB_EXITS_IN_CONFIG_FILE";
                public const string ENTER_DB_NAME_FOR_BACKUP = "ENTER_DB_NAME_FOR_BACKUP";
                public const string BACKUP_FINISED_SUCCESS = "BACKUP_FINISED_SUCCESS";
                public const string DELETE_RECORD_AND_CONTINUE_MIGRATION = "DELETE_RECORD_AND_CONTINUE_MIGRATION";
                public const string DATA_MIGRATION_COULD_NOT_PROCEED_INFO = "DATA_MIGRATION_COULD_NOT_PROCEED_INFO";
                public const string CLOSE_CAPTION = "CLOSE_CAPTION";
                public const string OLEDB_CONN_TAG_MISSING_IN_APPCONFIG_FILE = "OLEDB_CONN_TAG_MISSING_IN_APPCONFIG_FILE";
                public const string COMMON_ACMEERP_CAPTION = "COMMON_ACMEERP_CAPTION";
                public const string ACMEERP_UP_TO_DATE_INFO = "ACMEERP_UP_TO_DATE_INFO";
                public const string DOWNLOADING_LATEST_VERSION = "DOWNLOADING_LATEST_VERSION";
                public const string CHECK_INTERNET_FTP_CONNECTION = "CHECK_INTERNET_FTP_CONNECTION";
                public const string COULD_NOT_UPDATE_LATEST_VERSION = "COULD_NOT_UPDATE_LATEST_VERSION";
                public const string CHECKING_FOR_LATEST_VERSION = "CHECKING_FOR_LATEST_VERSION";
                public const string ACMEERP_UP_TO_DATE = "ACMEERP_UP_TO_DATE";
                public const string CONNECTING_TO_PORTAL_INFO = "CONNECTING_TO_PORTAL_INFO";
                public const string UPDATER_VERSION = "UPDATER_VERSION";
            }


            public class FinanceDataSynch
            {
                public const string AMENDMENTS_NOTES_INTERNET_CONNECTION_AVAIL = "AMENDMENTS_NOTES_INTERNET_CONNECTION_AVAIL";
                public const string AMENDMENTS_NOTES_CONNECTING_PORTAL = "AMENDMENTS_NOTES_CONNECTING_PORTAL";

                public const string DATA_SYNCH_SUB_BRANCH_VOUCHERS_SYNCH_SUCCESS = "DATA_SYNCH_SUB_BRANCH_VOUCHERS_SYNCH_SUCCESS";
                public const string DATA_SYNCH_SUB_BRANCH_VOUCHERS_FILE_SELECT = "DATA_SYNCH_SUB_BRANCH_VOUCHERS_FILE_SELECT";

                public const string EXPORT_VOUCHER_LEDGER_MOVE_BASED_NATURE = "EXPORT_VOUCHER_LEDGER_MOVE_BASED_NATURE";
                public const string EXPORT_HO_LEDGER_NOT_AVAIL_INFO = "EXPORT_HO_LEDGER_NOT_AVAIL_INFO";
                public const string EXPORT_BO_NOT_MAPPED_HO_LEDGER_INFO = "EXPORT_BO_NOT_MAPPED_HO_LEDGER_INFO";
                public const string EXPORT_BO_LEDGERS_INFO = "EXPORT_BO_LEDGERS_INFO";
                public const string EXPORT_LICENSEKEY_NOT_UPTO_DATE_INFO = "EXPORT_LICENSEKEY_NOT_UPTO_DATE_INFO";
                public const string EXPORT_UPDATE_LICENSE_INFO = "EXPORT_UPDATE_LICENSE_INFO";
                public const string EXPORT_VOUCHER_EXPORT_SUCCESS_INFO = "EXPORT_VOUCHER_EXPORT_SUCCESS_INFO";
                public const string EXPORT_VOUCHER_EXPORT_INFO = "EXPORT_VOUCHER_EXPORT_INFO";
                public const string EXPORT_VOUCHER_CANCEL_INFO = "EXPORT_VOUCHER_CANCEL_INFO";
                public const string EXPORT_VOUCHER_CONNECTING_INTERNET_INFO = "EXPORT_VOUCHER_CONNECTING_INTERNET_INFO";
                public const string EXPORT_VOUCHER_INTERNET_CONNECTION_SUCCESS_INFO = "EXPORT_VOUCHER_INTERNET_CONNECTION_SUCCESS_INFO";
                public const string EXPORT_VOUCHER_SEND_TO_PORTAL_INFO = "EXPORT_VOUCHER_SEND_TO_PORTAL_INFO";
                public const string EXPORT_VOUCHER_CHECKING_MISMATCH_PROJECT = "EXPORT_VOUCHER_CHECKING_MISMATCH_PROJECT";
                public const string EXPORT_VOUCHER_UPLOADING_VOUCHER_FILE_START = "EXPORT_VOUCHER_UPLOADING_VOUCHER_FILE_START";
                public const string EXPORT_VOUCHER_SYNCH_STATUS_NOT_UPDATE_INFO = "EXPORT_VOUCHER_SYNCH_STATUS_NOT_UPDATE_INFO";
                public const string EXPORT_VOUCHER_BO_PROJECT_MISMACTH_HO_INFO = "EXPORT_VOUCHER_BO_PROJECT_MISMACTH_HO_INFO";
                public const string EXPORT_VOUCHER_UNABLE_REACH_PORTAL_INFO = "EXPORT_VOUCHER_UNABLE_REACH_PORTAL_INFO";
                public const string EXPORT_UNABLE_REACH_PORTAL_CHECK_FTP_INFO = "EXPORT_UNABLE_REACH_PORTAL_CHECK_FTP_INFO";
                public const string EXPORT_ERROR_EXPORT_XML_ONLINE = "EXPORT_ERROR_EXPORT_XML_ONLINE";
                public const string EXPORT_ERROR_EXPORT_VOUCHER_FILE = "EXPORT_ERROR_EXPORT_VOUCHER_FILE";
                public const string EXPORT_MASTERS_SUCCESS_INFO = "EXPORT_MASTERS_SUCCESS_INFO";
                public const string EXPORT_MASTERS_BRANCH_NOT_SELECT_INFO = "EXPORT_MASTERS_BRANCH_NOT_SELECT_INFO";
                public const string EXPORT_MASTERS_NO_PROJECT_AVAIL_INFO = "EXPORT_MASTERS_NO_PROJECT_AVAIL_INFO";
                public const string EXPORT_MASTERS_ATLEAST_PROJECT_SELECT_INFO = "EXPORT_MASTERS_ATLEAST_PROJECT_SELECT_INFO";
                public const string EXPORT_MASTERS_CANCELL_INFO = "EXPORT_MASTERS_CANCELL_INFO";
                public const string EXPORT_VOUCHERS_INFO = "EXPORT_VOUCHERS_INFO";
                public const string IMPORT_HO_MASTERS_FILE_NAME_INFO = "IMPORT_HO_MASTERS_FILE_PATH_INFO";
                public const string IMPORT_HO_MASTERS_FILE_PATH_INFO = "IMPORT_HO_MASTERS_FILE_PATH_INFO";
                public const string IMPORT_HO_MASTERS_FILE_NOT_EXITS_INFO = "IMPORT_HO_MASTERS_FILE_NOT_EXITS_INFO";
                public const string IMPORT_HO_MASTERS_SUCCESS_INFO = "IMPORT_HO_MASTERS_SUCCESS_INFO";
                public const string MAP_LEDGER_LEDGER_CANNOT_DELETE_INFO = "MAP_LEDGER_LEDGER_CANNOT_DELETE_INFO";
                public const string MAP_HO_PROJECTS_UPDATING_MASTERS_BO_INFO = "MAP_HO_PROJECTS_UPDATING_MASTERS_BO_INFO";
                public const string MAP_HO_MORETHAN_HO_PROJECTS_MAP_BO_PROJECT_INFO = "MAP_HO_MORETHAN_HO_PROJECTS_MAP_BO_PROJECT_INFO";
                public const string PORTAL_LOGIN_INTERNET_CONNECTION_NOT_AVAIL_INFO = "PORTAL_LOGIN_INTERNET_CONNECTION_NOT_AVAIL_INFO";
                public const string PORTAL_LOGIN_USERNAME_EMPTY = "PORTAL_LOGIN_USERNAME_EMPTY";
                public const string PORTAL_LOGIN_PASSWORD_EMPTY = "PORTAL_LOGIN_PASSWORD_EMPTY";
                public const string PORTAL_LOGIN_LICENSEKEY_UPDATE_INFO = "PORTAL_LOGIN_LICENSEKEY_UPDATE_INFO";
                public const string PORTAL_LOGIN_LINCENSE_FILE_INVALID_INFO = "PORTAL_LOGIN_LINCENSE_FILE_INVALID_INFO";

                //Portal Updates
                public const string IMPORT_MASTERS_FROM_HO_INFO = "IMPORT_MASTERS_FROM_HO_INFO";
                public const string IMPORT_MODE = "IMPORT_MODE";
                public const string EXPORT_VOUCHERS_TO_HO_INFO = "EXPORT_VOUCHERS_TO_HO_INFO";
                public const string EXPORT_MODE = "EXPORT_MODE";
                public const string UPDATE_LICENSE = "UPDATE_LICENSE";
                public const string UPDATE_MODE = "UPDATE_MODE";
                public const string IMPORT_TDS_MASTER_FROM_HO = "IMPORT_TDS_MASTER_FROM_HO";
                public const string UNABLE_TO_REACH_PORTAL_CHECK_INTERNET_CONNECTION = "UNABLE_TO_REACH_PORTAL_CHECK_INTERNET_CONNECTION";
                public const string DATASET_EMPTY_DOES_NOT_MASTER_DETAILS = "DATASET_EMPTY_DOES_NOT_MASTER_DETAILS";
                public const string UPDATING_MASTER_HO_INFO = "UPDATING_MASTER_HO_INFO";
                public const string FETCHING_MASTER_FROM_PORTAL = "UPDATING_MASTER_HO_INFO";
                public const string CONNECTING_ACMEERP_PORTAL = "CONNECTING_ACMEERP_PORTAL";
                public const string UPDATING_MASTER_BO = "UPDATING_MASTER_BO";
                public const string IMPORT_CAPTION = "IMPORT_CAPTION";
                public const string EXPORT_CAPTION = "EXPORT_CAPTION";
                public const string UPDATE_CAPTION = "UPDATE_CAPTION";
                public const string UPLOAD_CAPTION = "UPLOAD_CAPTION";
                public const string LICENSE_FILE_NOT_UPDATE = "LICENSE_FILE_NOT_UPDATE";
                public const string LICENSE_KEY_UPDATED = "LICENSE_KEY_UPDATED";
                public const string LICENSE_INFORMATION_EMPTY_FROM_HO = "LICENSE_INFORMATION_EMPTY_FROM_HO";
                public const string DOWNLODING_LICENSE_FROM_PORTAL = "DOWNLODING_LICENSE_FROM_PORTAL";
                public const string UPDATE_LICENSE_INFO = "UPDATE_LICENSE_INFO";
                public const string UPDATING_MASTER_IN_BO = "UPDATING_MASTER_IN_BO";
                public const string MASTER_DOWNLOADED_SUCCESSFULLY_INFO = "MASTER_DOWNLOADED_SUCCESSFULLY_INFO";
                public const string UPDATE_LEDGER_MAP_WITH_PROJECT = "UPDATE_LEDGER_MAP_WITH_PROJECT";
                public const string YES_COLON_DISCARD_PREVIOUS_MAPPING_AND_UPDATE_NEW_MAPPING = "YES_COLON_DISCARD_PREVIOUS_MAPPING_AND_UPDATE_NEW_MAPPING";
                public const string NO_COLON_KEEP_PREVIOUS_MAPPING = "NO_COLON_KEEP_PREVIOUS_MAPPING";
                public const string LICENSE_KEY_CAPTION = "LICENSE_KEY_CAPTION";
                public const string MASTERS_CAPTION = "MASTERS_CAPTION";
                public const string DOWNLOAD_DEFINED_TDS_MASTERS_FROM_PORTAL = "DOWNLOAD_DEFINED_TDS_MASTERS_FROM_PORTAL";
                public const string IMPORT_TDS_MASTERS = "IMPORT_TDS_MASTERS";
                public const string LICENSE_MODEL_FOR_BRANCH = "LICENSE_MODEL_FOR_BRANCH";
                public const string DOWNLOAD_DEFINED_MASTERS_FROM_PORTAL = "DOWNLOAD_DEFINED_MASTERS_FROM_PORTAL";
                public const string FILE_DOES_NOT_EXISTS_IN_PATH = "FILE_DOES_NOT_EXISTS_IN_PATH";
                public const string MASTER_FILE_NOT_SELECT = "MASTER_FILE_NOT_SELECT";
                public const string LICENSE_FILE_INVALID = "LICENSE_FILE_INVALID";
                public const string CONNECTING_TO_PORTAL = "CONNECTING_TO_PORTAL";
                public const string HO_CODE_AND_BO_CODE_NOT_FOUND_IN_BO = "HO_CODE_AND_BO_CODE_NOT_FOUND_IN_BO";
                public const string DOWNLOAD_BO_LICENSE_FROM_HO = "DOWNLOAD_BO_LICENSE_FROM_HO";

                //frmSplit Project
                public const string CHECK_INTERNET_CONNECTION = "CHECK_INTERNET_CONNECTION";
                public const string REMOVE_PROJECT_SELECT_PROJECT_CONFIRMATION = "REMOVE_PROJECT_SELECT_PROJECT_CONFIRMATION";
                public const string LEDGER_GROUP_CREATE_UNDER_PRIMARY_GROUP = "LEDGER_GROUP_CREATE_UNDER_PRIMARY_GROUP";
                public const string PROJECT_EXPORT_SUCCESS = "PROJECT_EXPORT_SUCCESS";
                public const string SPLIT_PROJECT_HAS_BEEN_CANCELLED = "SPLIT_PROJECT_HAS_BEEN_CANCELLED";
                public const string SPLIT_VOUCHER = "SPLIT_VOUCHER";
                public const string MAP_MISMATCHED_LEDGER_WITH_HO_LEDGER = "MAP_MISMATCHED_LEDGER_WITH_HO_LEDGER";
                public const string MAP_BRANCH_LEDGER_WITH_HO_LEDGER = "MAP_BRANCH_LEDGER_WITH_HO_LEDGER";
                public const string MAP_BO_LEDGER_MAPPED_WITH_HO_LEDGER_SUCCESS = "MAP_BO_LEDGER_MAPPED_WITH_HO_LEDGER_SUCCESS";
            }

            public class CostCentre
            {
                public const string COST_CENTER_SUCCESS = "COST_CENTER_SUCSESS";
                public const string COST_CENTER_SAVE_FAILURE = "COST_CENTER_SAVE_FAILURE";
                public const string COST_CENTER_CODE_EMPTY = "COST_CENTER_CODE_EMPTY";
                public const string COST_CENTER_NAME_EMPTY = "COST_CENTER_NAME_EMPTY";
                public const string COST_CENTER_ADD_CAPTION = "COST_CENTER_ADD_CAPTION";
                public const string COST_CENTER_EDIT_CAPTION = "COST_CENTER_EDIT_CAPTION";
                public const string COST_CENTER_PRINT_CAPTION = "COST_CENTER_PRINT_CAPTION";
                public const string COST_CENTER_DELETE_SUCCESS = "COST_CENTER_DELETE_SUCCESS";
                public const string COST_CENTER_LEDGER_OPTION_FAILURE = "COST_CENTER_LEDGER_OPTION_FAILURE";
                public const string COST_CENTER_CODE_CAPTION = "COST_CENTER_CODE_CAPTION";
            }

            public class CostCentreCategory
            {
                public const string COST_CENTRE_CATEGORY_DELETE_SUCCESS = "COST_CENTRE_CATEGORY_DELETE_SUCCESS";
                public const string COST_CENTRE_CATEGORY_SAVE_SUCCESS = "COST_CENTRE_CATEGORY_SAVE_SUCCESS";
                public const string COST_CENTRE_CATEGORY_EMPTY = "COST_CENTRE_CATEGORY_EMPTY";
                public const string COST_CENTRE_CATEGORY_ADD_CAPTION = "COST_CENTRE_CATEGORY_ADD_CAPTION";
                public const string COST_CENTRE_CATEGORY_EDIT_CAPTION = "COST_CENTRE_CATEGORY_EDIT_CAPTION";
                public const string COST_CENTRE_CATOGORY_AVAILABLE = "COST_CENTRE_CATOGORY_AVAILABLE";
                public const string COST_CENTRE_CATEGORY_PRINT_CAPTION = "COST_CENTRE_CATEGORY_PRINT_CAPTION";
            }

            public class Donor
            {
                public const string DONOR_ADD_CAPTION = "DONOR_ADD_CAPTION";
                public const string DONOR_EDIT_CAPTION = "DONOR_EDIT_CAPTION";
                public const string DONOR_NAME_EMPTY = "DONOR_NAME_EMPTY";
                public const string DONOR_COUNTRY_EMPTY = "DONOR_COUNTRY_EMPTY";
                public const string DONOR_STATE_EMPTY = "DONOR_STATE_EMPTY";
                public const string DONOR_SAVE_SUCCESS = "DONOR_SAVE_SUCCESS";
                public const string DONOR_DELETE_SUCCESS = "DONOR_DELETE_SUCCESS";
                public const string DONOR_PRINT_CAPTION = "DONOR_PRINT_CAPTION";
                public const string DONOR_REGISTRATION_TYPE_EMPTY = "DONOR_REGISTRATION_TYPE_EMPTY";
                public const string DONOR_REGISTRATION_TYPE_ADD_CAPTION = "DONOR_REGISTRATION_TYPE_ADD_CAPTION";
                public const string DONOR_REGISTRATION_TYPE_EDIT_CAPTION = "DONOR_REGISTRATION_TYPE_EDIT_CAPTION";
                public const string DONOR_REFERRED_STAFF_DETAILS = "DONOR_REFERRED_STAFF_DETAILS";
                public const string REFERRED_STAFF_ADD_CAPTION = "REFERRED_STAFF_ADD_CAPTION";
                public const string REFERRED_STAFF_EDIT_CAPTION = "REFERRED_STAFF_EDIT_CAPTION";
                public const string DONOR_DOJ_EMPTY = "DONOR_DOJ_EMPTY";
                public const string DONOR_DATE_OF_VALIDATION = "DONOR_DATE_OF_VALIDATION";
            }

            public class DonorTitle
            {
                public const string DONORTITLE_ADD_CAPTION = "DONORTITLE_ADD_CAPTION";
                public const string DONOR_TITLENAME_EMPTY = "DONOR_TITLENAME_EMPTY";
                public const string DONORTITLE_EDIT_CAPTION = "DONORTITLE_EDIT_CAPTION";
                public const string DONORTITLE_PRINT_CAPTION = "DONORTITLE_PRINT_CAPTION";
                public const string DONORTITLE_VIEW_CAPTION = "DONORTITLE_VIEW_CAPTION";
                public const string DONORTITLE_DELETE_CAPTION = " DONORTITLE_DELETE_CAPTION";
            }

            public class Prospects
            {
                public const string PROSPECT_ADD_CAPTION = "PROSPECT_ADD_CAPTION";
                public const string PROSPECT_EDIT_CAPTION = "PROSPECT_EDIT_CAPTION";
                public const string PROSPECT_CITY_EMPTY = "PROSPECT_CITY_EMPTY";
                public const string PROSPECT_ADDRESS_EMPTY = "PROSPECT_ADDRESS_EMPTY";
            }

            public class Auditor
            {
                public const string AUDITOR_ADD_CAPTION = "AUDITOR_ADD_CAPTION";
                public const string AUDITOR_EDIT_CAPTION = "AUDITOR_EDIT_CAPTION";
                public const string AUDITOR_SAVE_SUCCESS = "AUDITOR_SAVE_SUCCESS";
                public const string AUDITOR_DELETE_SUCCESS = "AUDITOR_DELETE_SUCCESS";
                public const string AUDITOR_PRINT_CAPTION = "AUDITOR_PRINT_CAPTION";
                public const string AUDITOR_NAME_EMPTY = "AUDITOR_NAME_EMPTY";
            }

            public class AccountingPeriod
            {
                public const string ACCOUNTING_PERIOD_ADD_CAPTION = "ACCOUNTING_PERIOD_ADD_CAPTION";
                public const string ACCOUNTING_PERIOD_EDIT_CAPTION = "ACCOUNTING_PERIOD_EDIT_CAPTION";
                public const string ACCOUNTING_PERIOD_SAVE_SUCCESS = "ACCOUNTING_PERIOD_SAVE_SUCCESS";
                public const string ACCOUNTING_PERIOD_DELETE_SUCCESS = "ACCOUNTING_PERIOD_DELETE_SUCCESS";
                public const string ACCOUNTING_PERIOD_PRINT_CAPTION = "ACCOUNTING_PERIOD_PRINT_CAPTION";
                public const string ACCOUNTING_PERIOD_YEAR_FROM_EMPTY = "ACCOUNTING_PERIOD_YEAR_FROM_EMPTY";
                public const string ACCOUNTING_PERIOD_YEAR_TO_EMPTY = "ACCOUNTING_PERIOD_YEAR_TO_EMPTY";
                public const string ACCOUNTING_PERIOD_YEAR_EQUAL_EMPTY = "ACCOUNTING_PERIOD_YEAR_EQUAL_EMPTY";
                public const string ACCOUNTING_PERIOD_ACTIVE = "ACCOUNTING_PERIOD_ACTIVE";
                public const string ACCOUNTING_PERIOD_CANNOT_DELETE = "ACCOUNTING_PERIOD_CANNOT_DELETE";
                public const string ACCOUNTING_PERIOD_CHANGE_PERIOD = "ACCOUNTING_PERIOD_CHANGE_PERIOD";
                public const string ACCOUNTING_PERIOD_ONE_ACTIVE = "ACCOUNTING_PERIOD_ONE_ACTIVE";
                public const string COMMON_BACKUP_CONFIRMATION = "COMMON_BACKUP_CONFIRMATION";
                public const string COMMON_RESTORE_CONFIRMATION = "COMMON_RESTORE_CONFIRMATION";
                public const string TRANSACTION_PERIOD_SET_EMPTY = "TRANSACTION_PERIOD_SET_EMPTY";
                public const string ACCOUNTING_PERIOD_NOT_CREATED = "ACCOUNTING_PERIOD_NOT_CREATED";
                public const string ACCOUNTING_SET_YEAR_VALIDATION = "ACCOUNTING_SET_YEAR_VALIDATION";

            }

            public class AddressBook
            {
                public const string ADDRESS_ADD_CAPTION = "ADDRESS_ADD_CAPTION";
                public const string ADDRESS_EDIT_CAPTION = "ADDRESS_EDIT_CAPTION";
                public const string ADDRESS_SAVE_SUCCESS = "ADDRESS_SAVE_SUCCESS";
                public const string ADDRESS_DELETE_SUCCESS = "ADDRESS_DELETE_SUCCESS";
                public const string ADDRESS_PRINT_CAPTION = "ADDRESS_PRINT_CAPTION";
                public const string ADDRESS_COUNTRY_EMPTY = "ADDRESS_COUNTRY_EMPTY";
                public const string ADDRESS_NAME_EMPTY = "ADDRESS_NAME_EMPTY";
            }

            public class InKindArticle
            {
                public const string INKINDARTICLE_ADD_CAPTION = "INKINDARTICLE_ADD_CAPTION";
                public const string INKINDARTICLE_EDIT_CAPTION = "INKINDARTICLE_EDIT_CAPTION";
                public const string INKINDARTICLE_DELETE_SUCCESS = "INKINDARTICLE_DELETE_SUCCESS";
                public const string INKINDARTICLE_SAVE_SUCCESS = "INKINDARTICLE_SAVE_SUCCESS";
                public const string INKINDARTICLE_PRINT_CAPTION = "INKINDARTICLE_PRINT_CAPTION";
                public const string INKINDARTICLE_ABBREVATION_EMPTY = "INKINDARTICLE_ABBREVATION_EMPTY";
                public const string INKINDARTICLE_NAME_EMPTY = "INKINDARTICLE_NAME_EMPTY";
            }

            public class ExecutiveMembers
            {
                public const string EXECUTIVE_ADD_CAPTION = "EXECUTIVE_ADD_CAPTION";
                public const string EXECUTIVE_EDIT_CAPTION = "EXECUTIVE_EDIT_CAPTION";
                public const string EXECUTIVE_DELETE_SUCCESS = "EXECUTIVE_DELETE_SUCCESS";
                public const string EXECUTIVE_DELETE_FAILURE = "EXECUTIVE_DELETE_FAILURE";
                public const string EXECUTIVE_SAVE_SUCCESS = "EXECUTIVE_SAVE_SUCCESS";
                public const string EXECUTIVE_NAME_EMPTY = "EXECUTIVE_NAME_EMPTY";
                public const string EXECUTIVE_JOIN_DOB = "EXECUTIVE_JOIN_DOB";
                public const string EXECUTIVE_DOB_EXIT = "EXECUTIVE_DOB_EXIT";
                public const string EXECUTIVE_EXIT_JOIN = "EXECUTIVE_EXIT_JOIN";
                public const string EXECUTIVE_NAIONALITY_EMPTY = "EXECUTIVE_NATIONALITY_EMPTY";
                public const string EXECUTIVE_COUNTRY_EMPTY = "EXECUTIVE_COUNTRY_EMPTY";
                public const string EXECUTIVE_PRINT_CAPTION = "EXECUTIVE_PRINT_CAPTION";
                public const string EXECUTIVE_EMAIL_EMPTY = "EXECUTIVE_EMAIL_EMPTY";
                public const string SOCIETY_NAME_EMPTY = "SOCIETY_NAME_EMPTY";
                public const string GOVERNING_BODIES = "GOVERNING_BODIES";
            }

            public class Purposes
            {
                public const string PURPOSE_ADD_CAPTION = "PURPOSE_ADD_CAPTION";
                public const string PURPOSE_EDIT_CAPTION = "PURPOSE_EDIT_CAPTION";
                public const string PURPOSE_DELETE_SUCCESS = "PURPOSE_DELETE_SUCCESS";
                public const string PURPOSE_DELETE_FAILURE = "PURPOSE_DELETE_FAILURE";
                public const string PURPOSE_SAVE_SUCCESS = "PURPOSE_SAVE_SUCCESS";
                public const string PURPOSE_CODE_EMPTY = "PURPOSE_CODE_EMPTY";
                public const string PURPOSE_HEAD_EMPTY = "PURPOSE_HEAD_EMPTY";
                public const string PURPOSE_PRINT_CAPTION = "PURPOSE_PRINT_CAPTION";
            }

            public class StatisticsType
            {
                public const string STATISTICSTYPE_ADD_CAPTION = "STATISTICSTYPE_ADD_CAPTION";
                public const string STATISTICSTYPE_EDIT_CAPTION = "STATISTICSTYPE_EDIT_CAPTION";
                public const string STATISTICSTYPE_DELETE_SUCCESS = "STATISTICSTYPE_DELETE_SUCCESS";
                public const string STATISTICSTYPE_DELETE_FAILURE = "STATISTICSTYPE_DELETE_FAILURE";
                public const string STATISTICSTYPE_SAVE_SUCCESS = "STATISTICSTYPE_SAVE_SUCCESS";
                public const string STATISTICSTYPE_CODE_EMPTY = "STATISTICSTYPE_CODE_EMPTY";
                public const string STATISTICSTYPE_EMPTY = "STATISTICSTYPE_EMPTY";
                public const string STATISTICSTYPE_PRINT_CAPTION = "STATISTICSTYPE_PRINT_CAPTION";
            }

            public class Group
            {
                public const string GROUP_ADD_CAPTION = "GROUP_ADD_CAPTION";
                public const string GROUP_EDIT_CAPTION = "GROUP_EDIT_CAPTION";
                public const string GROUP_CODE_EMPTY = "GROUP_CODE_EMPTY";
                public const string GROUP_NAME_EMPTY = "GROUP_NAME_EMPTY";
                public const string GROUP_LEVEL_CHECK = "GROUP_LEVEL_CHECK";
                public const string GROUP_SAVE_SUCCESS = "GROUP_SAVE_SUCCESS";
                public const string GROUP_SAVE_FAILURE = "GROUP_SAVE_FAILURE";
                public const string GROUP_DELETE_SUCCESS = "GROUP_DELETE_SUCCESS";
                public const string GROUP_PARENT_EMPTY = "GROUP_PARENT_EMPTY";
                public const string GROUP_CAN_DELETE = "GROUP_CAN_DELETE";
                public const string GROUP_CAN_EDIT = "GROUP_CAN_EDIT";
                public const string GROUP_NATURE_DELETE = "GROUP_NATURE_DELETE";
                public const string GROUP_FIXED_EDIT = "GROUP_FIXED_EDIT";
            }

            public class Voucher
            {
                public const string VOUCHER_ADD_CAPTION = "VOUCHER_ADD_CAPTION";
                public const string VOUCHER_EDIT_CAPTION = "VOUCHER_EDIT_CAPTION";
                public const string VOUCHER_NAME_EMPTY = "VOUCHER_NAME_EMPTY";
                public const string VOUCHER_TYPE_EMPTY = "VOUCHER_TYPE_EMPTY";
                public const string VOUCHER_METHOD_EMPTY = "VOUCHER_METHOD_EMPTY";
                public const string VOUCHER_SUCCESS = "VOUCHER_SUCCESS";
                public const string VOUCHER_DELETE_SUCCESS = "VOUCHER_DELETE";
                public const string VOUCHER_PRINT_CAPTION = "VOUCHER_PRINT_CAPTION";
                public const string VOUCHER_EXISTS = "VOUCHER_NAME_EXISTS";
                public const string VOUCHER_PROJECT_EMPTY = "VOUCHER_PROJECT_EMPTY";
                public const string VOUCHER_CANCELLED_REVERT_SUCCESS = "VOUCHER_CANCELLED_REVERT_SUCCESS";
                public const string VOUCHER_RECEIPT_NORIGHTS_DELETE = "VOUCHER_RECEIPT_NORIGHTS_DELETE";
                public const string VOUCHER_PAYMENT_NORIGHTS_DELETE = "VOUCHER_PAYMENT_NORIGHTS_DELETE";
                public const string VOUCHER_CONTRA_NORIGHTS_DELETE = "VOUCHER_CONTRA_NORIGHTS_DELETE";
            }

            public class Transaction
            {   
                //Dinesh 03/07/2025
                public const string ENRTY_CAN_BE_EDITED_IN_THE_FIXED_ASSTE_MODULE = "ENRTY_CAN_BE_EDITED_IN_THE_FIXED_ASSTE_MODULE";
                public const string VOUCHER_IS_LOCKED_CANNOT_PRINT_VOUCHER_FOR_THE_PROJECT = "VOUCHER_IS_LOCKED_CANNOT_PRINT_VOUCHER_FOR_THE_PROJECT ";
                public const string ENTRY_CAN_BE_EDITED_IN_THE_FIXED_ASSET_MODULE = "ENTRY_CAN_BE_EDITED_IN_THE_FIXED_ASSET_MODULE";
                public const string SELECTED_VOUCHER_IS_FIXED_DEPOSIT_VOUCHER_ONLY_JOURNAL_VOUCHER_ALONE_CAN_BE_DUPLICATED = "SELECTED_VOUCHER_IS_FIXED_DEPOSIT_VOUCHER_ONLY_JOURNAL_VOUCHER_ALONE_CAN_BE_DUPLICATED";
                public const string TO_MAKE_REPLICATE_VOUCHER_ALT_U = "TO_MAKE_REPLICATE_VOUCHER _ALT_U";

                public const string DENOMINATION_TOT_AMT_GRATERTHAN_ALLOT_AMT = "DENOMINATION_TOT_AMT_GRATERTHAN_ALLOT_AMT";
                public const string DENOMINATION_TOT_AMT_LESSERHTAN_ALLOT_AMT = "DENOMINATION_TOT_AMT_LESSERHTAN_ALLOT_AMT";
                public const string FDREGISTER_VIEW_PRINT_CAPTION = "FDREGISTER_VIEW_PRINT_CAPTION";
                public const string FDCLOSEDREGISTER_VIEW_PRINT_CAPTION = "FDCLOSEDREGISTER_VIEW_PRINT_CAPTION";
                public const string PROJECT_SELECTION_CREATE_PROJECT = "PROJECT_SELECTION_CREATE_PROJECT";
                public const string PROJECT_SELECTION_IMPORT_MASTER_FROM_PORTAL = "PROJECT_SELECTION_IMPORT_MASTER_FROM_PORTAL";
                public const string TRANS_COSTCENTER_VALIDATION_MESSAGE = "TRANS_COSTCENTER_VALIDATION_MESSAGE";
                public const string TRANS_COSTCENTER_NAME_EMPTY = "TRANS_COSTCENTER_NAME_EMPTY";
                public const string TRANS_COSTCENTER_AMOUNT_EMPTY = "TRANS_COSTCENTER_AMOUNT_EMPTY";
                public const string TRANS_JOURNAL_VIEW_TRANS_LOCKED_INFO = "TRANS_JOURNAL_VIEW_TRANS_LOCKED_INFO";
                public const string TRANS_JOURNAL_ENTRY_CAN_DELETED_FIXED_ASSET_MODULE = "TRANS_JOURNAL_ENTRY_CAN_DELETED_FIXED_ASSET_MODULE";
                public const string TRANS_JOURNAL_VOUCHER_LOCKED_INFO = "TRANS_JOURNAL_VOUCHER_LOCKED_INFO";
                public const string TRANS_JOURNAL_TDS_JOURNAL_DONE_VOUCHER = "TRANS_JOURNAL_TDS_JOURNAL_DONE_VOUCHER";
                public const string TRANS_VIEW_ENTRY_CAN_DEL_ASSET_MODULE = "TRANS_VIEW_ENTRY_CAN_DEL_ASSET_MODULE";
                public const string TRANS_VIEW_ENTRY_CAN_EDIT_ASSET_MODULE = "TRANS_VIEW_ENTRY_CAN_EDIT_ASSET_MODULE";
                public const string TRANS_JOURNAL_VIEW_DELETE_REFERERED_VOUCHER = "TRANS_JOURNAL_VIEW_DELETE_REFERERED_VOUCHER";
            }

            public class Project
            {
                public const string PROJECT_SUCCESS = "PROJECT_SUCCESS";
                public const string PROJECT_FAILURE = "PROJECT_FAILURE";
                public const string PROJECT_CODE_EMPTY = "PROJECT_CODE_EMPTY";
                public const string PROJECT_NAME_EMPTY = "PROJECT_NAME_EMPTY";
                public const string PROJECT_CATEGORY_EMPTY = "PROJECT_CATEGORY_EMPTY";
                public const string PROJECT_ADD_CAPTION = "PROJECT_ADD_CAPTION";
                public const string PROJECT_EDIT_CAPTION = "PROJECT_EDIT_CAPTION";
                public const string PROJECT_PRINT_CAPTION = "PROJECT_PRINT_CAPTION";
                public const string PROJECT_DELETE_SUCCESS = "PROJECT_DELETE_SUCCESS";
                public const string PROJECT_DELETE_ASSOCIATION = "PROJECT_DELETE_ASSOCIATION";
                public const string PROJECT_AVAILABLE_VOUCHERS = "PROJECT_AVAILABLE_VOUCHERS";
                public const string PROJECT_PROJECT_VOUCHERS = "PROJECT_PROJECT_VOUCHERS";
                public const string PROJECT_VOUCHER_INFO = "PROJECT_VOUCHER_INFO";
                public const string PROJECT_DATE_VALIDATION = "PROJECT_DATE_VALIDATION";
                public const string PROJECT_VOUCHER = "PROJECT_VOUCHER";
                public const string PROJECT_MAP_LEDGER = "PROJECT_MAP_LEDGER";
                public const string PROJECT_START_DATE_EMPTY = "PROJECT_START_DATE_EMPTY";
                public const string PROJECT_CANNOT_DELETE_OPENING_BALANCE = "PROJECT_CANNOT_DELETE_OPENING_BALANCE";
                public const string PROJECT__LEDGER_MAPPED = "PROJECT__LEDGER_MAPPED";
                public const string PROJECT_TRANSACTION_MADE = "PROJECT_TRANSACTION_MADE";
                public const string PROJECT_PURPOSE_EMPTY = "PROJECT_PURPOSE_EMPTY";
                public const string PROJECT_PURPOSE_TRANSACTION_MADE = "PROJECT_PURPOSE_TRANSACTION_MADE";
                public const string BANKACCOUNT_NOT_BELONG_TO_SAME_LEGALENTITY = "BANKACCOUNT_NOT_BELONG_TO_SAME_LEGALENTITY";
                public const string PURPOSE = "PURPOSE";
                public const string TRANSACTION_MADE_CANT_UNMAP = "TRANSACTION_MADE_CANT_UNMAP";
                public const string TRANSACTION_MADE_SOME_LEDGER_CANT_UNMAP = "TRANSACTION_MADE_SOME_LEDGER_CANT_UNMAP";
            }

            public class ProjectCatogory
            {
                public const string PROJECT_CATOGORY_DELETE_SUCCESS = "PROJECT_CATOGORY_DELETE_SUCCESS";
                public const string PROJECT_CATOGORY_SAVE_SUCCESS = "PROJECT_CATOGORY_SAVE_SUCCESS";
                public const string PROJECT_CATOGORY_EMPTY = "PROJECT_CATOGORY_EMPTY";
                public const string PROJECT_CATEGORY_ADD_CAPTION = "PROJECT_CATEGORY_ADD_CAPTION";
                public const string PROJECT_CATEGORY_EDIT_CAPTION = "PROJECT_CATEGORY_EDIT_CAPTION";
                public const string PROJECT_CATOGORY_AVAILABLE = "PROJECT_CATOGORY_AVAILABLE";
                public const string PROJECT_CATEGORY_PRINT_CAPTION = "PROJECT_CATEGORY_PRINT_CAPTION";
            }

            public class AuditorInfo
            {
                public const string AUDITOR_INFO_SUCCESS = "AUDITOR_INFO_SUCCESS";
                public const string AUDITOR_INFO_PROJECT_EMPTY = "AUDITOR_INFO_PROJECT_EMPTY";
                public const string AUDITOR_INFO_STARTEDON_EMPTY = "AUDITOR_INFO_STARTEDON_EMPTY";
                public const string AUDITOR_INFO_CLOSEDON_EMPTY = "AUDITOR_INFO_CLOSEDON_EMPTY";
                public const string AUDITOR_INFO_ADD = "AUDITOR_INFO_ADD";
                public const string AUDITOR_INFO_EDIT = "AUDITOR_INFO_EDIT";
                public const string AUDITOR_INFO_PRINT_CAPTION = "AUDITOR_INFO_PRINT_CAPTION";
                public const string AUDITOR_INFO_AUDITOR_EMPTY = "AUDITOR_INFO_AUDITOR_EMPTY";
                public const string AUDITOR_INFO_DELETE_SUCCESS = "AUDITOR_INFO_DELETE_SUCCESS";
                public const string AUDITOR_BEGIN_DATE = "AUDITOR_BEGIN_DATE";
                public const string AUDITOR_ON_FROM = "AUDITOR_ON_FROM";
                public const string AUDITOR_ON_TO = "AUDITOR_ON_TO";
            }

            public class Ledger
            {
                public const string LEDGER_TITLE = "LEDGER_TITLE";
                public const string LEDGER_SUCCESS = "LEDGER_SUCCESS";
                public const string LEDGER_FAILURE = "LEDGER_FAILURE";
                public const string LEDGER_DELETED = "LEDGER_DELETED";
                public const string lEDGER_CODE_EMPTY = "lEDGER_CODE_EMPTY";
                public const string LEDGER_NAME_EMPTY = "LEDGER_NAME_EMPTY";
                public const string LEDGER_GROUP_EMPTY = "LEDGER_GROUP_EMPTY";
                public const string LEDGER_ADD_CAPTION = "LEDGER_ADD_CAPTION";
                public const string LEDGER_EDIT_CAPTION = "LEDGER_EDIT_CAPTION";
                public const string LEDGER_PRINT_CAPTION = "LEDGER_PRINT_CAPTION";
                public const string LEDGER_ACCOUNT_TYPE_EMPTY = "LEDGER_ACCOUNT_TYPE_EMPTY";
                public const string LEDGER_BANK_EMPTY = "LEDGER_BANK_EMPTY";
                public const string LEDGER_ACCOUNT_NUMBER_EMPTY = "LEDGER_ACCOUNT_NUMBER_EMPTY";
                public const string LEDGER_DATE_OPEN_EMPTY = "LEDGER_DATE_OPEN_EMPTY";
                public const string LEDGER_ACCOUNT_DATE_EMPTY = "LEDGER_ACCOUNT_DATE_EMPTY";
                public const string BANK_ACCOUNT_SUCCESS = "BANK_ACCOUNT_SUCCESS";
                public const string BANK_ACCOUNT_DELETED = "BANK_ACCOUNT_DELETED";
                public const string BANK_ACCCOUNT_FAILURE = "BANK_ACCOUNT_FAILURE";
                public const string BANK_ACCOUNT_CODE_EMPTY = "BANK_ACCOUNT_CODE_EMPTY";
                public const string BANK_ACCOUNT_ADD = "BANK_ACCOUNT_ADD";
                public const string BANK_ACCOUNT_EDIT = "BANK_ACCOUNT_EDIT";
                public const string BANK_ACCOUNT_PRINT_CAPTION = "BANK_ACCOUNT_PRINT_CAPTION";
                public const string BANK_ACCOUNT_CLOSEDATE_VALIDATION = "BANK_ACCOUNT_CLOSEDATE_VALIDATION";
                public const string BANK_ACCOUNT_CLOSEDATE_TRANSACTION = "BANK_ACCOUNT_CLOSEDATE_TRANSACTION";
                public const string FD_LEDGER_PRINT_CAPTION = "FD_LEDGER_PRINT_CAPTION";
                public const string FD_CREATED_DATE = "FD_CREATED_DATE";
                public const string FIXED_LEDGER_DELETE = "FIXED_LEDGER_DELETE";
                public const string FIXED_LEDGER_EDIT = "FIXED_LEDGER_EDIT";
                public const string LEDGER_TRANSACTION_MADE = "LEDGER_TRANSACTION_MADE";
                public const string LEDGER_TYPE_EMPTY = "LEDGER_TYPE_EMPTY";
                public const string NATURE_OF_PAYMENT = "NATURE_OF_PAYMENT";
                public const string DEDUCTEE_TYPE = "DEDUCTEE_TYPE";
                public const string DEDUCTEE_TYPE_IS_REQUIRED = "DEDUCTEE_TYPE_IS_REQUIRED";
                public const string NATURE_OF_PAYMENT_IS_REQUIRED = "NATURE_OF_PAYMENT_IS_REQUIRED";
                public const string LEDGER_NAME = "LEDGER_NAME";
                public const string ACCOUNT_NUMBER = "ACCOUNT_NUMBER";
                public const string LEDGERS_CAPTION = "LEDGERS_CAPTION";

            }

            public class FixedDeposit
            {
                public const string FD_SUCCESS = "FD_SUCCESS";
                public const string FD_DELETED = "FD_DELETED";
                public const string FD_ADD = "FD_ADD";
                public const string FD_EDIT = "FD_EDIT";
                public const string FD_PRINT_CAPTION = "FD_PRINT_CAPTION";
                public const string FD_CREATED_DATE_EMPTY = "FD_CREATED_DATE_EMPTY";
                public const string FD_MATURITY_DATE_EMPTY = "FD_MATURITY_DATE_EMPTY";
                public const string FD_MATURITY_DATE_LESS_THAN_CREATED_DATE = "FD_MATURITY_DATE_LESS_THAN_CREATED_DATE";
                public const string FD_INTEREST_RATE_EMPTY = "FD_INTEREST_RATE_EMPTY";
                public const string FD_AMOUNT_EMPTY = "FD_AMOUNT_EMPTY";
                public const string FD_NUMBER_EMPTY = "FD_NUMBER_EMPTY";
                public const string FD_MATURITY_DATE = "FD_MATURITY_DATE";
                public const string FD_RENEWED_DATE = "FD_RENEWED_DATE";
                public const string FD_RENEWAL_CAPTION = "FD_RENEWAL_CAPTION";
                public const string FD_DEPOSIT = "FD_DEPOSIT";
                public const string FD_CONFIRMATION_DELETE = "FD_CONFIRMATION_DELETE";
                public const string FD_MATURITY_DATE_GREATER_THAN_CREATED_DATE = "FD_MATURITY_DATE_GREATER_THAN_CREATED_DATE";
                public const string FD_AMOUNT_GREATER_THAN_ZERO = "FD_AMOUNT_GREATER_THAN_ZERO";
                public const string FD_REALISED = "FD_REALISED";
                public const string FD_CANNOT_RENEW = "FD_CANNOT_RENEW";
                public const string FD_CANNOT_POST_INTEREST = "FD_CANNOT_POST_INTEREST";
                public const string FD_CANNOT_INVEST = "FD_CANNOT_INVEST";
                public const string FD_CANNOT_REALIZED = "FD_CANNOT_REALIZED";
                public const string OP_BALANCE = "OP_BALANCE";
                public const string INVESTED_AMOUNT = "INVESTED_AMOUNT";
                public const string AMOUNT = "AMOUNT";
                public const string FD_WITHDRAWAL_AVAILABLE = "FD_WITHDRAWAL_AVAILABLE";
                public const string FD_RENEWALS_AVAILABLE = "FD_RENEWALS_AVAILABLE";
            }

            public class FDOpening
            {

                //public const string FD_SET_OPENING_BALANCE_ALERT = "FD_SET_OPENING_BALANCE_ALERT";
            }

            public class LedgerType
            {
                public const string General = "GN";
                public const string BankAccounts = "SA";
                public const string FixedDeposit = "FD";
            }

            public class BreakUp
            {
                public const string BREAKUP_AMOUT_ZERO = "BREAKUP_AMOUT_ZERO";
                public const string BREAKUP_REQUIRED_FIELD = "BREAKUP_REQUIRED_FIELD";
                public const string BREAKUP_DATE_EXCEPTION = "BREAKUP_DATE_EXCEPTION";
                public const string BREAKUP_AMOUNT_EXCEEDS = "BREAKUP_AMOUNT_EXCEEDS";
                public const string BREAKUP_SAVED_SUCCESS = "BREAKUP_SAVED_SUCCESS";
                public const string BREAKUP_NOT_TALLIED = "BREAKUP_NOT_TALLIED";
                public const string PERCENTAGE_NEGATIVE = "PERCENTAGE_NEGATIVE";
                public const string BREAKUP_AMOUNT_LESS_THAN = "BREAKUP_AMOUNT_LESS_THAN";
                public const string BREAKUP_AMOUNT_GREATER_THAN = "BREAKUP_AMOUNT_GREATER_THAN";
                public const string BREAKUP_GRID_ERROR = "BREAKUP_GRID_ERROR";
                public const string BREAKUP_INTEREST_RATE_EMPTY_ZERO = "BREAKUP_INTEREST_RATE_EMPTY_ZERO";
                public const string BREAKUP_FD_NUMBER_EMPTY = "BREAKUP_FD_NUMBER_EMPTY";
                public const string BREAKUP_ZERO_ENTRY = "BREAKUP_ZERO_ENTRY";
                public const string BREAKUP_NEGATIVE_BALANCE = "BREAKUP_NEGATIVE_BALANCE";
                public const string BREAKUP_INTEREST_RATE_NEGATIVE = "BREAKUP_INTEREST_RATE_NEGATIVE";
                public const string FD_WITHDRAWALAMOUNT_EXCEED = "FD_WITHDRAWALAMOUNT_EXCEED";
            }

            public class FDRenewal
            {
                public const string FD_RENEWAL = "FD_RENEWAL";
                public const string FD_NUMBER = "FD_NUMBER";
                public const string FD_AMOUNT = "FD_AMOUNT";
                public const string FD_INTEREST_RATE = "FD_INTEREST_RATE";
                public const string FD_TITLE = "FD_TITLE";
                public const string RENEWAL_NO_RECORD = "RENEWAL_NO_RECORD";
                public const string RENEWAL_VIEW_NO_RECORD_TO_RENEW = "RENEWAL_VIEW_NO_RECORD_TO_RENEW";
                public const string WITHDRAW_VIEW_NO_RECORD = "WITHDRAW_VIEW_NO_RECORD";
                public const string FD_RENEWAL_CANNOT_EDIT = "FD_RENEWAL_CANNOT_EDIT";
                public const string FD_RENEWAL_CANNOT_DELETE = "FD_RENEWAL_CANNOT_DELETE";
                public const string FD_RENEWAL_DETAILS = "FD_RENEWAL_DETAILS";
                public const string FD_RENEWAL_DELETE = "FD_RENEWAL_DELETE";
                public const string FDPOST_INTEREST_DELETE = "FDPOST_INTEREST_DELETE";
                public const string FD_RENEWAL_MODIFY = "FD_RENEWAL_MODIFY";
                public const string FDPOST_INTEREST_MODIFY = "FDPOST_INTEREST_MODIFY";
                public const string WITHDRAWAL = "WITHDRAWAL";
            }

            public class FDPostInterest
            {
                public const string FD_POSTINTEREST = "FD_POSTINTEREST";
            }

            public class InKindReceived
            {
                public const string INKIND_LEDGER = "INKIND_LEDGER";
                public const string INKIND_ITEM = "INKIND_ITEM";
                public const string INKIND_PURPOSE = "INKIND_PURPOSE";
                public const string INKIND_VALUE = "INKIND_VALUE";
                public const string INKIND_LEDGER_ITEM = "INKIND_LEDGER_ITEM";
            }

            public class FDLedger
            {
                public const string FD_LEDGER_CODE = "FD_LEDGER_CODE";
                public const string FD_LEDGER_NAME = "FD_LEDGER_NAME";
                public const string FD_CASH_BANK_LEDGERS = "FD_CASH_BANK_LEDGERS";
                public const string FD_ACCOUNT_ADD = "FD_ACCOUNT_ADD";
                public const string FD_ACCOUNT_EDIT = "FD_ACCOUNT_EDIT";
                public const string FD_INVESTMENT_ADD = "FD_INVESTMENT_ADD";
                public const string FD_INVESTMENT_EDIT = "FD_INVESTMENT_EDIT";
                public const string FD_OP_DATE_AS_ON = "FD_OP_DATE_AS_ON";
                public const string FD_INVESTENT_DATE_AS_ON = "FD_INVESTENT_DATE_AS_ON";
                public const string FD_OP_AMOUNT = "FD_OP_AMOUNT";
                public const string FD_INVESTMENT_AMOUNT = "FD_INVESTMENT_AMOUNT";
                public const string FD_OPENING_CAPTION = "FD_OPENING_CAPTION";
                public const string FD_INVESTMENT_CAPTION = "FD_INVESTMENT_CAPTION";
                public const string FD_OPENING_GROUP_CAPTION = "FD_OPENING_GROUP_CAPTION";
                public const string FD_INVESTMENT_GROUP_CAPTION = "FD_INVESTMENT_GROUP_CAPTION";
                public const string FD_ACCOUNT_OP_PRINT_CAPTION = "FD_ACCOUNT_OP_PRINT_CAPTION";
                public const string FD_ACCOUNT_INV_PRINT_CAPTION = "FD_ACCOUNT_INV_PRINT_CAPTION";
                public const string FIXED_DEPOSIT_LEDGER_EMPTY = "FIXED_DEPOSIT_LEDGER_EMPTY";
                public const string FD_BANK_INTEREST_LED_EMPTY = "FD_BANK_INTEREST_LED_EMPTY";
                public const string FD_RENEWAL_DATE_WITHIN_TRANSACTION = "FD_RENEWAL_DATE_WITHIN_TRANSACTION";
                public const string FD_BANK_IS_NOT_CORRECT = "FD_BANK_IS_NOT_CORRECT";
                public const string FD_RENEWAL_DATE_NOT_GREATER_THAN_MATURITY_DATE = "FD_RENEWAL_DATE_NOT_GREATER_THAN_MATURITY_DATE";
                public const string FD_CREATED_DATE_LESS_THAN_FINANCIAL_YEAR = "FD_CREATED_DATE_LESS_THAN_FINANCIAL_YEAR";
                public const string FD_MATURITY_DATE_FINANCIAL_YEAR = "FD_MATURITY_DATE_FINANCIAL_YEAR";
                public const string FD_WITHDRAWAL_ON_EMPTY = "FD_WITHDRAWAL_ON_EMPTY";
                public const string FD_WITHDRAWAL_ON_GREATER_THAN_RENEWAL_DATE = "FD_WITHDRAWAL_ON_GREATER_THAN_RENEWAL_DATE";
                public const string FD_TRANSACTION_MADE_ANOTHER_PERIOD = "FD_TRANSACTION_MADE_ANOTHER_PERIOD";
                public const string FD_NOT_APPROPRIATE_BANK = "FD_NOT_APPROPRIATE_BANK";
                public const string FD_BANK_CONFIRMATION = "FD_BANK_CONFIRMATION";
                public const string FD_BANK_CASH_GOES_CREDIT = "FD_BANK_CASH_GOES_CREDIT";
                public const string FD_CASH_BANK_EMPTY = "FD_CASH_BANK_EMPTY";
                public const string FD_CANNOT_DELETE_ASSCOCIATE_RENEWAL = "FD_CANNOT_DELETE_ASSCOCIATE_RENEWAL";
                public const string FD_LEDGER_NOT_MAPPED_TO_PROJECT = "FD_LEDGER_NOT_MAPPED_TO_PROJECT";
                public const string FD_WITHDRAWAL = "FD_WITHDRAWAL";
                public const string FD_WITHDRAW_AMOUNT_EMPTY = "FD_WITHDRAW_AMOUNT_EMPTY";
                public const string FD_WITHDRAW_AMOUNT_VALIDATION = "FD_WITHDRAW_AMOUNT_VALIDATION";
                public const string FD_LEDGER = "FD_LEDGER";
                public const string FD_LEDGER_ADD = "FD_LEDGER_ADD";
                public const string FD_LEDGER_EDIT = "FD_LEDGER_EDIT";
                public const string TDS_ADD = "TDS_ADD";
                public const string TDS_EDIT = "TDS_EDIT";
                public const string HAS_FD_ACCOUNT_NUMBER = "HAS_FD_ACCOUNT_NUMBER";
            }

            public class LegalEntity
            {
                public const string LEGAL_ENTITY_PRINT_CAPTION = "LEGAL_ENTITY_PRINT_CAPTION";
                public const string LEGAL_ENTITY_ADD = "LEGAL_ENTITY_ADD";
                public const string LEGAL_ENTITY_EDIT = "LEGAL_ENTITY_EDIT";
                public const string LEGAL_ENTITY_UNMAP = "LEGAL_ENTITY_UNMAP";
                public const string PROBLEM_IN_SAVING = "PROBLEM_IN_SAVING";
                public const string TRANSACTION_EXISTS = "TRANSACTION_EXISTS";
                public const string ALREADY_MAPPPED_WITH_ANOTHER_LEGAL_ENTITY = "ALREADY_MAPPPED_WITH_ANOTHER_LEGAL_ENTITY";
                public const string CANNOT_BE_UNMAPPED = "CANNOT_BE_UNMAPPED";
            }

            public class DashBoard
            {
                public const string DASHBOARD_PROJECT_TITLE = "DASHBOARD_PROJECT_TITLE";
                public const string DASHBOARD_RECEIPT_PAYMENT_TITLE = "DASHBOARD_RECEIPT_PAYMENT_TITLE";
                public const string DASHBOARD_BANK_RECONCILIATION_TITLE = "DASHBOARD_BANK_RECONCILIATION_TITLE";
                public const string DASHBOARD_BALANCE_TITLE = "DASHBOARD_BALANCE_TITLE";
                public const string DASHBOARD_BRS_TITLE = "DASHBOARD_BRS_TITLE";
                public const string DASHBOARD_CASH_TITLE = "DASHBOARD_CASH_TITLE";
                public const string DASHBOARD_ALERTS_TITLE = "DASHBOARD_ALERTS_TITLE";
                public const string DASHBOARD_BANK_TITLE = "DASHBOARD_BANK_TITLE";
                public const string DASHBOARD_FD_TITLE = "DASHBOARD_FD_TITLE";
                public const string DASHBOARD_CHEQUE_HAS_TITLE = "DASHBOARD_CHEQUE_HAS_TITLE";
                public const string DASHBOARD_CHEQUE_HAVE_TITLE = "DASHBOARD_CHEQUE_HAVE_TITLE";
                public const string DASHBOARD_NEGATIVE_BALANCE_TITLE = "DASHBOARD_NEGATIVE_BALANCE_TITLE";
                public const string DASHBOARD_CASH_BANK_FD_TITLE = "DASHBOARD_CASH_BANK_FD_TITLE";
                public const string DASHBOARD_MATURES_ON_TITLE = "DASHBOARD_MATURES_ON_TITLE";
                public const string DASHBOARD_FD_ACCOUNT_TITLE = "DASHBOARD_FD_ACCOUNT_TITLE";
                public const string DASHBOARD_FIXED_DEPOSIT_TITLE = "DASHBOARD_FIXED_DEPOSIT_TITLE";
                public const string DASHBOARD_PORTALMESSAGE_TITLE = "DASHBOARD_PORTALMESSAGE_TITLE";
                public const string DASHBOARD_LICENSE_TITLE = "DASHBOARD_LICENSE_TITLE";
                public const string DASHBOARD_TICKET_STATUS = "DASHBOARD_TICKET_STATUS";
                public const string DASHBOARD_AMEDMENTS_TITLE = "DASHBOARD_AMEDMENTS_TITLE";
                public const string DASH_REC_PAY_CAP = "DASH_REC_PAY_CAP";

            }

            public class AuditLockType
            {
                public const string AUDITLOCKTYPEEMPTY = "AUDITLOCKTYPEEMPTY";
                public const string DATE_FROM_EMPTY = "DATE_FROM_EMPTY";
                public const string DATETOEMPTY = "DATETOEMPTY";
                public const string PASSWORDEMPTY = "PASSWORDEMPTY";
                public const string PASSWORD_HINT_EMPTY = "PASSWORD_HINT_EMPTY";
                public const string AUDIT_LOCK_TYPE = "AUDIT_LOCK_TYPE";
                public const string LOCK_TRANS_TYPE = "LOCK_TRANS_TYPE";
                public const string DIFFERENT_LOCKING_PERIOD = "DIFFERENT_LOCKING_PERIOD";
                public const string PROJECT = "PROJECT";
                public const string LOCK_VOUCHER_ADD_CAPTION = "LOCK_VOUCHER_ADD_CAPTION";
                public const string LOCK_VOUCHER_EDIT_CAPTION = "LOCK_VOUCHER_EDIT_CAPTION";
                public const string LOCK_TYPE_ADD_CAPTION = "LOCK_TYPE_ADD_CAPTION";
                public const string LOCK_TYPE_EDIT_CAPTION = "LOCK_TYPE_EDIT_CAPTION";
                public const string AUDIT_LOCK_TYPE_VIEW_CAPTION = "AUDIT_LOCK_TYPE_VIEW_CAPTION";
                public const string AUDIT_TYPE_IS_EMPTY = "AUDIT_TYPE_IS_EMPTY";
                public const string AUDITOR_IS_EMPTY = "AUDITOR_IS_EMPTY";
                public const string AMOUNT_MISMATCHED_WITH_SPLITED_AMOUNT = "AMOUNT_MISMATCHED_WITH_SPLITED_AMOUNT";
                public const string RESET_PASSWORD = "RESET_PASSWORD";
                public const string PASSWORD_INVALID = "PASSWORD_INVALID";
                public const string PASSWORD_RESET = "PASSWORD_RESET";
                public const string HINT_INVALID = "HINT_INVALID";
            }
        }
        #endregion

        #region Settings
        public class Settings
        {
            public const string SETTING_SUCCESS = "SETTING_SUCCESS";
            public const string SETTING_FAILURE = "SETTING_FAILURE";
            public const string SETTING_LANGUAGE_INVALID = "SETTING_LANGUAGE_INVALID";
            public const string SETTING_DATE_FORMAT_INVALID = "SETTING__DATE_FORMAT_INVALID";
            public const string SETTING_DATE_SEPARATOR_INVALID = "SETTING_DATE_SEPARATOR_INVALID";
            public const string SETTING_CURRENCY_INVALID = "SETTING_CURRENCY_INVALID";
            public const string SETTING_CURRENCY_CODE_INVALID = "SETTING_CURRENCY_CODE_INVALID";
            public const string SETTING_CURRENCY__POSITION_INVALID = "SETTING_CURRENCY__POSITION_INVALID";
            public const string SETTING_DIGIT_GROUPING_INVALID = "SETTING_DIGIT_GROUPING_INVALID";
            public const string SETTING_DIGIT_GROUPING_SEPARATOR_INVALID = "SETTING_DIGIT_GROUPING_SEPARATOR_INVALID";
            public const string SETTING_DECIMAL_PLACES_INVALID = "SETTING_DECIMAL_PLACES_INVALID";
            public const string SETTING_DECIMAL_PLACES_SEPARATOR_INVALID = "SETTING_DECIMAL_PLACES_SEPARATOR_INVALID";
            public const string SETTING_NEGATIVE_SIGN_INVALID = "SETTING_NEGATIVE_SIGN_INVALID";
            public const string SETTING_DEFAULT_LANGUAGE_SET = "SETTING_DEFAULT_LANGUAGE_SET";
            public const string SETTING_TRANSACTION_FAILURE = "SETTING_TRANSACTION_FAILURE";
            public const string SETTING_BOOK_BEGINNING_EMPTY = "SETTING_BOOK_BEGINNING_EMPTY";
            public const string SETTING_APPLICATION_RESTART_CONFIRMATION = "SETTING_APPLICATION_RESTART_CONFIRMATION";
            public const string SETTING_RESTART_PREVIEW = "SETTING_RESTART_PREVIEW";
            public const string SETTING_FOREIGN_BANKACCOUNT = "SETTING_FOREIGN_BANKACCOUNT";
        }
        #endregion

        #region Transaction
        public class Transaction
        {
            public class VocherTransaction
            {    
                //Dinesh 02/07/2025
                public const string ADD_VOUCHER_FILE ="ADD _VOUCHER_FILE";



                public const string VOUCHER_RECEIPT_ADD_CAPTION = "VOUCHER_RECEIPT_ADD_CAPTION";
                public const string VOUCHER_PAYMENT_ADD_CAPTION = "VOUCHER_PAYMENT_ADD_CAPTION";
                public const string VOUCHER_CONTRA_ADD_CAPTION = "VOUCHER_CONTRA_ADD_CAPTION";
                public const string VOUCHER_EDIT_CAPTION = "VOUCHER_EDIT_CAPTION";
                public const string VOUCHER_VALID_AMOUNT = "VOUCHER_VALID_AMOUNT";
                public const string VOUCHER_MULTY_ENTRY_CHECK = "VOUCHER_MULTY_ENTRY_CHECK";
                public const string VOUCHER_MULTY_RECEIPT_CONFIRM = "VOUCHER_MULTY_RECEIPT_CONFIRM";
                public const string VOUCHER_MULTY_PAYMENT_CONFIRM = "VOUCHER_MULTY_PAYMENT_CONFIRM";
                public const string VOUCHER_MULTY_INVALID_TRANSACTION_LEDGER = "VOUCHER_MULTY_INVALID_TRANSACTION_LEDGER";
                public const string VOUCHER_MULTY_INVALID_TRANSACTION_AMOUNT = "VOUCHER_MULTY_INVALID_TRANSACTION_LEDGER";
                public const string VOUCHER_MULTY_INVALID_CASH_TRANSACTION_LEDGER = "VOUCHER_MULTY_INVALID_TRANSACTION_LEDGER";
                public const string VOUCHER_MULTY_INVALID_CASH_TRANSACTION_AMOUNT = "VOUCHER_MULTY_INVALID_TRANSACTION_LEDGER";
                public const string TRANS_DATE = "TRANS_DATE";
                public const string BRS_MATERIALIZED_DATE = "BRS_MATERIALIZED_DATE";
                public const string VOUCHER_SAVE = "VOUCHER_SAVE";
                public const string VOUCHER_AMOUNT_LESS_THAN_ZERO = "VOUCHER_AMOUNT_LESS_THAN_ZERO";
                public const string VOUCHER_TRANSACTION_CHANGE_TYPE = "VOUCHER_TRANSACTION_CHANGE_TYPE";
                public const string VOUCHER_TRANSACTION_CONTRA_CHANGE_TYPE = "VOUCHER_TRANSACTION_CONTRA_CHANGE_TYPE";
                public const string VOUCHER_TRANSACTION_DATE = "VOUCHER_TRANSACTION_DATE";
                public const string VOUCHER_NUMBER_EMPTY = "VOUCHER_NUMBER_EMPTY";
                public const string REFRENCE_NUMBER_EMPTY = "REFRENCE_NUMBER_EMPTY";
                public const string VOUCHER_AMOUNT_MISMATCH = "VOUCHER_AMOUNT_MISMATCH";
                public const string VOUCHER_TRANS_DATETO_MISMATCH = "VOUCHER_TRANS_DATETO_MISMATCH";
                public const string VOUCHER_NEGATIVE_BALANCE_CASHBANK = "VOUCHER_NEGATIVE_BALANCE_CASHBANK";
                public const string VOUCHER_NEGATIVE_BALANCE_CASH = "VOUCHER_NEGATIVE_BALANCE_CASH";
                public const string VOUCHER_NEGATIVE_BALANCE_BANK = "VOUCHER_NEGATIVE_BALANCE_BANK";
                public const string VOUCHER_NEGATIVE_BALANCE_FD = "VOUCHER_NEGATIVE_BALANCE_FD";
                public const string DONOR_PUPOSE_EMPTY = "DONOR_PUPOSE_EMPTY";
                public const string DONOR_CONTRIBUTION_AMOUNT_EMPTY = "DONOR_CONTRIBUTION_AMOUNT_EMPTY";
                public const string DONOR_CONTRIBUTION_ACTUAL_AMOUNT_EMPTY = "DONOR_CONTRIBUTION_ACTUAL_AMOUNT_EMPTY";
                public const string TRANSACTION_AMOUNT_NOT_EQUAL_ACTUAL_AMOUNT = "TRANSACTION_AMOUNT_NOT_EQUAL_ACTUAL_AMOUNT";
                public const string VOUCHER_NO_RECORD = "VOUCHER_NO_RECORD";
                public const string JOURNAL_ADD_CAPTION = "JOURNAL_ADD_CAPTION";
                public const string JOURNAL_EDIT_CAPTION = "JOURNAL_EDIT_CAPTION";
                public const string VOUCHER_TRANSACTION_RECEIPT = "VOUCHER_TRANSACTION_RECEIPT";
                public const string VOUCHER_TRANSACTION_PAYMENT = "VOUCHER_TRANSACTION_PAYMENT";
                public const string VOUCHER_TRANSACTION_CONTRA = "VOUCHER_TRANSACTION_CONTRA";
                public const string VOUCHER_FD_REALIZE_CONFIRMATION = "VOUCHER_FD_REALIZE_CONFIRMATION";
                public const string VOUCHER_CASHBANK_MAPPING_TO_PROJECT = "VOUCHER_CASHBANK_MAPPING_TO_PROJECT";
                public const string VOUCHER_LEDGER_MAPPING_TO_PROJECT = "VOUCHER_LEDGER_MAPPING_TO_PROJECT";
                public const string SAVED_PRINT_VOUCHER = "SAVED_PRINT_VOUCHER";
                public const string CONFIRM_PRINT_VOUCHER = "CONFIRM_PRINT_VOUCHER";
                public const string VOUCHER_DEFAULT_CANNOT_DELETE = "VOUCHER_DEFAULT_CANNOT_DELETE";
                public const string TRANSACTION_MOVE_SUCCESS = "TRANSACTION_MOVE_SUCCESS";
                public const string TRANSACTION_MOVE_SUCCESS_NOT_BANK_ACCOUNT = "TRANSACTION_MOVE_SUCCESS_NOT_BANK_ACCOUNT";
                public const string TRANSACTION_MOVE_GREATER_THAN_ONE = "TRANSACTION_MOVE_GREATER_THAN_ONE";
                public const string TRANSACTION_MOVE_CONFIRMATION = "TRANSACTION_MOVE_CONFIRMATION";
                public const string TRANSACTION_MOVE_GRID_EMPTY = "TRANSACTION_MOVE_GRID_EMPTY";
                public const string TRANSACTION_MOVE_PROJECT_NOT_SELECTED = "TRANSACTION_MOVE_PROJECT_NOT_SELECTED";
                public const string TRANSACTION_MOVE_NOT_SELECTED = "TRANSACTION_MOVE_NOT_SELECTED";
                public const string VOUCHER_REGENERATION_SUCCESS = "VOUCHER_REGENERATION_SUCCESS";
                public const string VOUCHER_REGENERATION_FAILS = "VOUCHER_REGENERATION_FAILS";
                public const string VOUCHER_TYPE_NO_SELECTION = "VOUCHER_TYPE_NO_SELECTION";
                public const string VOUCHER_REGENERATE_SELECT_PROJECT = "VOUCHER_REGENERATE_SELECT_PROJECT";
                public const string REGENERATION_FAILS = "REGENERATION_FAILS";
                public const string VOUCHER_METHOD_MANUAL = "VOUCHER_METHOD_MANUAL";
                public const string JOURNAL_CANNOT_EDIT = "JOURNAL_CANNOT_EDIT";
                public const string JOURNAL_CANNOT_DELETE = "JOURNAL_CANNOT_DELETE";
                public const string VOUCHER_CANNOT_EDIT_FD_RECEIPT_ENTRY = "VOUCHER_CANNOT_EDIT_FD_RECEIPT_ENTRY";
                public const string VOUCHER_CANNOT_EDIT_FD_CONTRA_ENTRY = "VOUCHER_CANNOT_EDIT_FD_CONTRA_ENTRY";
                public const string VOUCHER_MOVE_TRANSACTION_SELECTION_EMPTY = "VOUCHER_MOVE_TRANSACTION_SELECTION_EMPTY";
                public const string VOUCHER_CANNOT_DELETE_ASSOCIATION_OCCURS = "VOUCHER_CANNOT_DELETE_ASSOCIATION_OCCURS";
                public const string VOUCHER_CANNOT_DELETE_FD_RECEIPT_ENTRY = "VOUCHER_CANNOT_DELETE_FD_RECEIPT_ENTRY";
                public const string TRANSACTION_CASH_LEDGER = "TRANSACTION_CASH_LEDGER";
                public const string TRANSACTION_BANK_LEDGER = "TRANSACTION_BANK_LEDGER";
                public const string TRANSACTION_FD_LEDGER = "TRANSACTION_FD_LEDGER";
                public const string REGENARATION_PERIODTO_MISIMATCH = "REGENARATION_PERIODTO_MISIMATCH";
                public const string YES_KEYDOWN = "YES_KEYDOWN";
                public const string SOLD_TO_EMPTY = "SOLD_TO_EMPTY";
                public const string ASSOCIATION_WITH_TDS = "ASSOCIATION_WITH_TDS";
                public const string START_DATE_AND_CLOSED_DATE_DOES_NOT_FALL = "START_DATE_AND_CLOSED_DATE_DOES_NOT_FALL";
                public const string VOUCHER_IS_LOCKED = "VOUCHER_IS_LOCKED";
                public const string ENTRIES_WILL_BE_CLEARED = "ENTRIES_WILL_BE_CLEARED";
                public const string TRANSACTION_AMOUNT_EXCEEDS = "TRANSACTION_AMOUNT_EXCEEDS";
                public const string VOUCHER_IS_LOCKED_CANNOT_DELETE = "VOUCHER_IS_LOCKED_CANNOT_DELETE";
                public const string DOES_NOT_FALL_BETWEEN_TRANSACTION_PERIOD = "DOES_NOT_FALL_BETWEEN_TRANSACTION_PERIOD";
                public const string CHANGE_THE_PROJECT_DATE = "CHANGE_THE_PROJECT_DATE";
                public const string RECORD_HAS_BEEN_DELETED = "RECORD_HAS_BEEN_DELETED";
                public const string VOUCHER_IS_LOCKED_CANNOT_EDIT = "VOUCHER_IS_LOCKED_CANNOT_EDIT";
                public const string SAVE_WITHOUT_DONOR = "SAVE_WITHOUT_DONOR";
                public const string RECEIPT = "RECEIPT";
                public const string PAYMENT = "PAYMENT";
                public const string CONTRA = "CONTRA";
                public const string ERROR_SHOWING_TDS = "ERROR_SHOWING_TDS";
                public const string ASSOCICATION_WITH_TDS_PAYMENT = "ASSOCICATION_WITH_TDS_PAYMENT";
                public const string REQUIRED_INFORMATION_NOT_FILLED = "REQUIRED_INFORMATION_NOT_FILLED";
                public const string REQUIRED_INFORMATION_LEDGER_EMPTY = "REQUIRED_INFORMATION_LEDGER_EMPTY";
                public const string REQUIRED_INFORMATION_AMOUNT_EMPTY = "REQUIRED_INFORMATION_AMOUNT_EMPTY";
                public const string REQUIRED_INFORMATION_TRANSMODE_EMPTY = "REQUIRED_INFORMATION_TRANSMODE_EMPTY";
                public const string CASHBANK_MUST_DEBITED = "CASHBANK_MUST_DEBITED";
                public const string CASHBANK_MUST_CREDITED = "CASHBANK_MUST_CREDITED";
                public const string CASHBANK_LEDGER_EMPTY = "CASHBANK_LEDGER_EMPTY";
                public const string CASHBANK_AMOUNT_EMPTY = "CASHBANK_AMOUNT_EMPTY";
                public const string CASHBANK_NEGATIVE = "CASHBANK_NEGATIVE";
                public const string LOADING_VOUCHERS = "LOADING_VOUCHERS";
                public const string NO_RIGHTS_TO_MOVE_RECEIPT_VOUCHER = "NO_RIGHTS_TO_MOVE_RECEIPT_VOUCHER";
                public const string NO_RIGHTS_TO_MOVE_PAYMENT_VOUCHER = "NO_RIGHTS_TO_MOVE_PAYMENT_VOUCHER";
                public const string NO_RIGHTS_TO_MOVE_CONTRA_VOUCHER = "NO_RIGHTS_TO_MOVE_CONTRA_VOUCHER";
                public const string VOUCHER_IS_LOCKED_CANNOT_MOVE = "VOUCHER_IS_LOCKED_CANNOT_MOVE";
                public const string VOUCHER_IS_LOCKED_CANNOT_PRINT = "VOUCHER_IS_LOCKED_CANNOT_PRINT";
                public const string NO_RIGHTS_TO_TAKE_PRINTOUT = "NO_RIGHTS_TO_TAKE_PRINTOUT";
                public const string NO_RIGHTS_TO_TAKE_PRINTOUT_PAYMENT = "NO_RIGHTS_TO_TAKE_PRINTOUT_PAYMENT";
                public const string REGENERATE_VOUCHERS = "REGENERATE_VOUCHERS";
                public const string VOUCHER_NUMBER_FORMAT_IS_MANUAL = "VOUCHER_NUMBER_FORMAT_IS_MANUAL";
                public const string VOUCHER = "VOUCHER";
                public const string NO_RIGHTS_TO_EDIT_RECEIPT = "NO_RIGHTS_TO_EDIT_RECEIPT";
                public const string NO_RIGHTS_TO_EDIT_PAYMENT = "NO_RIGHTS_TO_EDIT_PAYMENT";
                public const string NO_RIGHTS_TO_EDIT_CONTRA = "NO_RIGHTS_TO_EDIT_CONTRA";
                public const string NO_RIGHTS_TO_DELETE_RECEIPT = "NO_RIGHTS_TO_DELETE_RECEIPT";
                public const string NO_RIGHTS_TO_DELETE_PAYMENT = "NO_RIGHTS_TO_DELETE_PAYMENT";
                public const string NO_RIGHTS_TO_DELETE_CONTRA = "NO_RIGHTS_TO_DELETE_CONTRA";
                public const string BANK_RECONCILIATION = "BANK_RECONCILIATION";
                public const string BUDGET_ANNUAL = "BUDGET_ANNUAL";
                public const string BUDGET_PERIOD = "BUDGET_PERIOD";
                public const string BUDGET_MADE_ALREADY = "BUDGET_MADE_ALREADY";
                public const string BUDGET_DETAILS = "BUDGET_DETAILS";
                public const string PROJECT_IS_NOT_CREATED = "PROJECT_IS_NOT_CREATED";
                public const string REGENERATING_VOUCHER_NUMBER = "REGENERATING_VOUCHER_NUMBER";
                public const string PRESS = "PRESS";
                public const string TO_SINGLE_ENTRY = "TO_SINGLE_ENTRY";
                public const string TO_DOUBLE_ENTRY = "TO_DOUBLE_ENTRY";
                public const string VOUCHER_ENTRY = "VOUCHER_ENTRY";
                public const string VOUCHER_VIEW = "VOUCHER_VIEW";
                public const string VOUCHER_DATE_LICENSE_PERIOD_VALIDATION = "VOUCHER_DATE_LICENSE_PERIOD_VALIDATION";
                public const string MOVE_TRANSACTION_ALL_THE_LEDGER_NOT_MAPPED = "MOVE_TRANSACTION_ALL_THE_LEDGER_NOT_MAPPED";
                public const string TDS_BOOKING_DATE_VALIDATE_PARTYPAYMENT_DATE = "TDS_BOOKING_DATE_VALIDATE_PARTYPAYMENT_DATE";

                public const string IS_AUTHORIZED = "IS_AUTHORIZED";
                public const string DURING_PERIOD = "DURING_PERIOD";
                public const string VOCHER_ENTRY = "VOCHER_ENTRY";
                public const string GST_ALREADY = "GST_ALREADY";
                public const string IS_GST_PERCENTAGE = "IS_GST_PERCENTAGE";
                public const string GST_INVOICE = "GST_INVOICE";
                public const string ARE_REP_ENTRY = "ARE_REP_ENTRY";
                public const string BANK_CHEQUE_NUMBER = "BANK_CHEQUE_NUMBER";
                public const string CHEQUE_NUMBER = "CHEQUE_NUMBER";
                public const string SHOULD_DUPLICATED = "SHOULD_DUPLICATED";
                public const string VOUCHER_DUPLICATE_CHEQUE = "VOUCHER_DUPLICATE_CHEQUE";
                public const string PRINT_CHQUE = "PRINT_CHQUE";
                public const string CASH_EXCEEDS = "CASH_EXCEEDS";
                public const string TRANSACTIONS_EXCEEDS = "TRANSACTIONS_EXCEEDS";
                public const string PROVINCE_REGULATIONS = "PROVINCE_REGULATIONS";
                public const string GST_LEDGER_AVAI = "GST_LEDGER_AVAI";
                public const string GST_AMOUNT_AVAI = "GST_AMOUNT_AVAI";
                public const string SUB_LEDGER = "SUB_LEDGER";
                public const string TRANSACTIONS_REF = "TRANSACTIONS_REF";
                public const string CHANGE = "CHANGE";
                public const string REF_AMOUNT = "REF_AMOUNT";
                public const string PAY_REF_VOUCHER = "PAY_REF_VOUCHER";
                public const string REF_AMOUNT_MIS = "REF_AMOUNT_MIS";
                public const string LEDGER_CC_ALLOCATIONS = "LEDGER_CC_ALLOCATIONS";
                public const string CC_LEDGER_AMOUNT = "CC_LEDGER_AMOUNT";
                public const string GST_ATTACH = "GST_ATTACH";
                public const string LEDGER_GST_INV = "LEDGER_GST_INV";
                public const string REM_GST_INVOICE = "REM_GST_INVOICE";
                public const string GST_INVOICE_REM = "GST_INVOICE_REM";
            }

            public class VoucherCostCentre
            {   
                public const string VOUCHER_COST_CENTRE_NAME_EXIST = "VOUCHER_COST_CENTRE_NAME_EXIST";
                public const string VOUCHER_COST_CENTRE_NAME_EMPTY = "VOUCHER_COST_CENTRE_NAME_EMPTY";
                public const string VOUCHER_VALID_AMOUNT = "VOUCHER_VALID_AMOUNT";
                public const string VOUCHER_COST_CENTRE_SAVED_SUCCESSFULLY = "VOUCHER_COST_CENTRE_SAVED_SUCCESSFULLY";
                public const string VOUCHER_COST_CENTRE_DELETED_SUCCESSFULLY = "VOUCHER_COST_CENTRE_DELETED_SUCCESSFULLY";
                public const string VOUCHER_COST_CENTRE_AMOUNT_EMPTY = "VOUCHER_COST_CENTRE_AMOUNT_EMPTY";
                public const string VOUCHER_COST_CENTRE_ALLOCATION_AMOUNT_GREATER = "VOUCHER_COST_CENTRE_ALLOCATION_AMOUNT_GREATER";
                public const string VOUCHER_COST_CENTRE_ALLOCATION_AMOUNT_LESS = "VOUCHER_COST_CENTRE_ALLOCATION_AMOUNT_LESS";
                public const string ALLOCATION_AMOUNT_IS_NOT_EQUAL = "ALLOCATION_AMOUNT_IS_NOT_EQUAL";
            }
        }
        #endregion

        #region Cropping Image
        public class Cropping
        {
            public const string CROPPING_TITLE = "CROPPING_TITLE";
            public const string CROPPING_CONFIRMATION = "CROPPING_CONFIRMATION";
            public const string CROPPING_IMAGE_RESIZE = "CROPPING_IMAGE_RESIZE";
            public const string CROPPING_EXCEPTION = "CROPPING_EXCEPTION";
            public const string CROPPING_EXCEPTION_MESSAGE = "CROPPING_EXCEPTION_MESSAGE";
        }
        #endregion

        #region Report
        public class ReportMessage
        {
            public const string DATEFROMEMPTY = "DATEFROMEMPTY";
            public const string DATE_VALIDATION = "DATE_VALIDATION";
            public const string REPORT_PROJECT_EMPTY = "REPORT_PROJECT_EMPTY";
            public const string REPORT_BANK_EMPTY = "REPORT_BANK_EMPTY";
            public const string REPORT_COSTCENTRE_EMPTY = "REPORT_COSTCENTRE_EMPTY";
            public const string REPORT_LEDGER_EMPTY = "REPORT_LEDGER_EMPTY";
            public const string REPORT_BUDGET_EMPTY = "REPORT_BUDGET_EMPTY";
            public const string REPORT_FINACIAL_RECORDS_CASH_BANK_EMPTY = "REPORT_FINACIAL_RECORDS_CASH_BANK_EMPTY";

            public const string PROJECT_MUST_SEL = "PROJECT_MUST_SEL";
            public const string MULTI_ABS_YEAR = "MULTI_ABS_YEAR";
            public const string BUDGET_ANN_YEAR = "BUDGET_ANN_YEAR";
            public const string DATE_RANGE_CUR_YEAR = "DATE_RANGE_CUR_YEAR";
            public const string QUAR_ABS_QUAR = "QUAR_ABS_QUAR";
            public const string MAX_EXPORT = "MAX_EXPORT";
            public const string MORE_BANK_LEDGER = "MORE_BANK_LEDGER";
            public const string FD_ACC_SELECTED = "FD_ACC_SELECTED";
            public const string CL_SELECTED = "CL_SELECTED";
            public const string BA_SELECTED = "BA_SELECTED";
            public const string BUDGET_MUST_SELECTED = "BUDGET_MUST_SELECTED";
            public const string ONEBUDGET_SELECTED = "ONEBUDGET_SELECTED";
            public const string BUDGET_AVAI_COM = "BUDGET_AVAI_COM";
            public const string BUDGET_AVAI = "BUDGET_AVAI";
            public const string LEDGER_SELECTED = "LEDGER_SELECTED";
            public const string CC_SELECTED = "CC_SELECTED";
            public const string TDS_LEDGER_SELECTED = "TDS_LEDGER_SELECTED";
            public const string NP_MUST_SELECTED = "NP_MUST_SELECTED";
            public const string DED_MUST_SELECTED = "DED_MUST_SELECTED";
            public const string LOCA_SELECTED = "LOCA_SELECTED";
            public const string REGISTRATION_SELECTED = "REGISTRATION_SELECTED";
            public const string COUNTRY_SELECTED = "COUNTRY_SELECTED";
            public const string STATE_SELECTED = "STATE_SELECTED";
            public const string DONOR_SELECTED = "DONOR_SELECTED";
            public const string ONEBANK_FD_SELECTED = "ONEBANK_FD_SELECTED";
            public const string AMOUNT_EMPTY = "AMOUNT_EMPTY";
            public const string OPENING_DISABLED = "OPENING_DISABLED";
            public const string CONSOLIDATED_OPTION = "CONSOLIDATED_OPTION";
            public const string NARRATION_CUM_DISABLED = "NARRATION_CUM_DISABLED";
            public const string LCC_DISABLED = "LCC_DISABLED";
            public const string OP_LEDGERS = "OP_LEDGERS";
            public const string REPORTS_EXCEL = "REPORTS_EXCEL";
            public const string FEW_PROJECT_DUP = "FEW_PROJECT_DUP";
            public const string PROJECT_NAME = "PROJECT_NAME";
            public const string IS_ALREADY = "IS_ALREADY";
            public const string SING_EQUAL = "SING_EQUAL";
            public const string SING_HEIGHT = "SING_HEIGHT";


        }
        public class ReportCommonTitle
        {
            public const string AMOUNT = "Amount";
            public const string DEBIT = "Debit";
            public const string CREDIT = "Credit";
            public const string OPBALANCE = "O/p Balance";
            public const string INCOME = "Income";
            public const string EXPENDITURE = "Expenditure";
            public const string PERIOD = "For the Period:";
            public const string DR = "DR";
            public const string CR = "CR";
            public const string INFLOW = "In Flow";
            public const string OUTFLOW = "Out Flow";
            public const string BALANCE = "Balance";
            public const string RECEIPT = "Receipts";
            public const string PAYMENTS = "Payments";
            public const string UNREALIZED = "Unrealized";
            public const string UNCLEARED = "Uncleared";
            public const string CASH = "Cash";
            public const string BANK = "Bank";
            public const string CLOSINGBALANCE = "Closing Balance";
            public const string FORTHEPERIOD = "For the Period";
            public const string ASON = "As on";
            public const string PROGRESSIVETOTAL = "Progressive Total";
            public const string PREVIOUS = "Previous";
            public const string AMOUNTIN = "Amount in";
            public const string FDAMOUNT = "FD Amount";
            public const string FDEXPMATAMOUNT = "Exp.Mat Amt";
            // public const string RPT_001_GROUP = "RPT_001_GROUP";
        }
        #endregion

        #region TDS
        public class TDS
        {
            public class NatureofPayments
            {
                public const string TDS_NATUREPAYMENT_PRINT_CAPTION = "TDS_NATUREPAYMENT_PRINT_CAPTION";
                public const string TDS_NATUREPAYMENT_ADD_CAPTION = "TDS_NATUREPAYMENT_ADD_CAPTION";
                public const string TDS_NATUREPAYMENT_EDIT_CAPTION = "TDS_NATUREPAYMENT_EDIT_CAPTION";
                public const string TDS_NATUREPAYMENT_PAYMENT_NAME_EMPTY = "TDS_NATUREPAYMENT_PAYMENT_NAME_EMPTY";
                public const string TDS_NATUREPAYMENT_SECTION_CODE_EMPTY = "TDS_NATUREPAYMENT_SECTION_CODE_EMPTY";
                public const string TDS_NATUREPAYMENT_PAYMENT_CODE_EMPTY = "TDS_NATUREPAYMENT_PAYMENT_CODE_EMPTY";
                public const string TDS_NATUREPAYMENT_SECTION_NAME_EMPTY = "TDS_NATUREPAYMENT_SECTION_NAME_EMPTY";
                public const string TDS_NATURE_OF_PAYMENT_EMPTY = "TDS_NATURE_OF_PAYMENT_EMPTY";
                public const string TDS_NATUREOFPAYEMNTS_SECTION_EMPTY = "TDS_NATUREOFPAYEMNTS_SECTION_EMPTY";
                public const string TDS_NATUREOFPAYMENTS_SECTION_EMPTYADD = "TDS_NATUREOFPAYMENTS_SECTION_EMPTYADD";
                public const string NATURE_OF_PAYMENT_EMPTY = "NATURE_OF_PAYMENT_EMPTY";
                public const string PARTY_LEDGER_EMPTY = "PARTY_LEDGER_EMPTY";
                public const string TDS_DEDUCTEE_TYPE_SUCCESS = "TDS_DEDUCTEE_TYPE_SUCCESS";
                public const string TDS_NORECORD_AVAILABLE_GRID_INFO = "TDS_NORECORD_AVAILABLE_GRID_INFO";
                public const string TDS_CANNOT_SET_INACTIVE_INFO = "TDS_CANNOT_SET_INACTIVE_INFO";
            }

            public class TDSSection
            {
                public const string TDS_SECTION_ADD_CAPTION = "TDS_SECTION_ADD_CAPTION";
                public const string TDS_SECTION_EDIT_CAPTION = "TDS_SECTION_EDIT_CAPTION";
                public const string TDS_CODE_EMPTY = "TDS_CODE_EMPTY";
                public const string TDS_NAME_EMPTY = "TDS_NAME_EMPTY";
                public const string TDS_SAVE_SUCCESS = "TDS_SAVE_SUCCESS";
                public const string TDS_CANNOT_SET_INACTIVE = "TDS_CANNOT_SET_INACTIVE";
                public const string TDS_SECTION_PRINT_CAPTION = "TDS_SECTION_PRINT_CAPTION";
                public const string TDS_SECTION_DELETE_INFO = "TDS_SECTION_DELETE_INFO";
                public const string TDS_DEDUCT_PREVIOUS_BILLS_INFO = "TDS_DEDUCT_PREVIOUS_BILLS_INFO";
            }
            public class DeducteeTypes
            {
                public const string TDS_DEDUCTEETYPES_PRINT_CAPTION = "TDS_DEDUCTEETYPES_PRINT_CAPTION";
                public const string TDS_DEDUCTEETYPES_ADD_CAPTION = "TDS_DEDUCTEETYPES_ADD_CAPTION";
                public const string TDS_DEDUCTEETYPES_EDIT_CAPTION = "TDS_DEDUCTEETYPES_EDIT_CAPTION";
                public const string TDS_DEDUCTEE_NAME_EMPTY = "TDS_DEDUCTEE_NAME_EMPTY";
                public const string TDS_DEDUCTEE_TYPE_EMPTY = "TDS_DEDUCTEE_TYPE_EMPTY";
                public const string TDS_DEDUCTEE_TYPES_INACTIVE_INFO = "TDS_DEDUCTEE_TYPES_INACTIVE_INFO";
                public const string TDS_VOUCHER_CANNOT_DELETE_INFO = "TDS_VOUCHER_CANNOT_DELETE_INFO";

            }

            public class CompanyTDSDedutors
            {
                public const string TDS_PROBLEM_SOLVING = "TDS_PROBLEM_SOLVING";
            }

            public class DutyTax
            {
                public const string TDS_DUTY_TAX_PRINT_CAPTION = "TDS_DUTY_TAX_PRINT_CAPTION";
                public const string TDS_DUTY_TAX_ADD_CAPTION = "TDS_DUTY_TAX_ADD_CAPTION";
                public const string TDS_DUTY_TAX_EDIT_CAPTION = "TDS_DUTY_TAX_EDIT_CAPTION";
                public const string TDS_DUTY_TAX_NAME_EMPTY = "TDS_DUTY_TAX_NAME_EMPTY";
                public const string TDS_NORIGHTS_DELETE_INFO = "TDS_NORIGHTS_DELETE_INFO";
                public const string TDS_DUTY_TAX_TYPE_CANNOT_EDIT_INFO = "TDS_DUTY_TAX_TYPE_CANNOT_EDIT_INFO";
                public const string TDS_LEDGER_PROFILE_DEFAULT_NOP = "TDS_LEDGER_PROFILE_DEFAULT_NOP";
                public const string TDS_LEDGER_PROFILE_DEFAULT_DEDUCTEE_TYPE = "TDS_LEDGER_PROFILE_DEFAULT_DEDUCTEE_TYPE";
            }

            public class TDSBooking
            {
                public const string TDS_BOOKING_DATE = "TDS_BOOKING_DATE";
                public const string TDS_EXPENSE_LEDGER_EMPTY = "TDS_EXPENSE_LEDGER_EMPTY";
                public const string TDS_PARTY_LEDGER_EMPTY = "TDS_PARTY_LEDGER_EMPTY";
                public const string TDS_VOUCHER_NUMBER_EMPTY = "TDS_VOUCHER_NUMBER_EMPTY";
                public const string TDS_AMOUNT = "TDS_AMOUNT";
                public const string TDS_DEDUCT_TYPE = "TDS_DEDUCT_TYPE";
                public const string TDS_AMOUNT_VALIDATION = "TDS_AMOUNT_VALIDATION";
                public const string TDS_AMOUNT_LESS_VALIDATION = "TDS_AMOUNT_LESS_VALIDATION";
                public const string TDS_GRID_EMPTY = "TDS_GRID_EMPTY";
                public const string TDS_PRINT_CAPTION = "TDS_PRINT_CAPTION";
            }

            public class TDSPolicy
            {
                public const string TDS_APPLICABLE_FROM_EMPTY = "TDS_APPLICABLE_FROM_EMPTY";
                public const string TDS_DATE_DUPLICATE = "TDS_DATE_DUPLICATE";
                public const string TDS_POLICY_NATURE_OF_PAYMENT_EMPTY = "TDS_POLICY_NATURE_OF_PAYMENT_EMPTY";
                public const string TDS_CONFIRM_DELETE_POLICY_DEFINED = "TDS_CONFIRM_DELETE_POLICY_DEFINED";
                public const string TDS_TAX_TYPE_NOT_EXISTS = "TDS_TAX_TYPE_NOT_EXISTS";
                public const string TDS_SELECT_EMPTY = "TDS_SELECT_EMPTY";
            }

            public class TDSTrasaction
            {
                public const string TDS_PENDING_TRANS_CONFIRMATION = "TDS_PENDING_TRANS_CONFIRMATION";
                public const string TDS_TRASN_DEDUCTEE_TYPE_EMPTY = "TDS_TRASN_DEDUCTEE_TYPE_EMPTY";
                public const string TDS_PARTY_LEDGER_EMPTY = "TDS_PARTY_LEDGER_EMPTY";
            }

            public class TDSPayment
            {
                public const string TDS_PAYMENT_PRINT = "TDS_PAYMENT_PRINT";
                public const string TDS_PAYMENT_CAPTION = "TDS_PAYMENT_CAPTION";
                public const string TDS_PARTY_PAYMENT_CAPTION = "TDS_PARTY_PAYMENT_CAPTION";
                public const string TDS_PAYMENT_AMOUNT_VALIDATION = "TDS_PAYMENT_AMOUNT_VALIDATION";
                public const string TDS_AMOUNT_VERIFICATION = "TDS_AMOUNT_VERIFICATION";
                public const string TDS_OK_BUTTON_CAPTION = "TDS_OK_BUTTON_CAPTION";
                public const string TDS_CANCEL_BUTTON_CAPTION = "TDS_CANCEL_BUTTON_CAPTION";
                public const string TDS_LEDGER_EMTPY = "TDS_LEDGER_EMTPY";
            }

        }
        #endregion

        #region License
        public class License
        {
            public const string LICENSE_GENERATE_CONFIRMATION = "LICENSE_GENERATE_CONFIRMATION";
            public const string INSTITUTE_NAME_EMPTY = "INSTITUTE_NAME_EMPTY";
            public const string SOCIETY_NAME_EMPTY = "SOCIETY_NAME_EMPTY";
            public const string HEAD_OFFICE_CODE_EMPTY = "HEAD_OFFICE_CODE_EMPTY";
            public const string BRANCH_OFFICE_CODE_EMPTY = "BRANCH_OFFICE_CODE_EMPTY";
            public const string LICENSE_ASSOCIATION_NATURE_EMPTY = "LICENSE_ASSOCIATION_NATURE_EMPTY";
            public const string LICENSE_MULTIPLE_ASSOCIATION_NATURE_EMPTY = "LICENSE_MULTIPLE_ASSOCIATION_NATURE_EMPTY";
            public const string LICENSE_DENOMIATION_EMPTY = "LICENSE_DENOMIATION_EMPTY";
            public const string SOCIETY_REG_NO = "SOCIETY_REG_NO";
            public const string LEGAL_ENTITY_REGNO_EMPTY = "LEGAL_ENTITY_REGNO_EMPTY";
        }
        #endregion

        #region Utility
        public class Utility
        {
            public class LedgerMerging
            {
                public const string MERGE_LEDGER_STRONG_MESSAGE = "MERGE_LEDGER_STRONG_MESSAGE";
                public const string PROJECT_VALIDATION = "PROJECT_VALIDATION";
                public const string MERGE_LEDGER_TDS_CONFIRMATION = "MERGE_LEDGER_TDS_CONFIRMATION";
                public const string TALLYERPSUCCESSFULLYIMPORTED = "TALLYERPSUCCESSFULLYIMPORTED";
                public const string ERRORMESSAGE = "ERRORMESSAGE";
                public const string FILENOTSELECTED = "FILENOTSELECTED";
            }
        }
        #endregion

        #region Migration
        public static class DataMigration
        {
            public const string MIGRATION_FILE_EMPTY = "MIGRATION_FILE_EMPTY";
            public const string MIGRATION_CONNECTION_FAILED = "MIGRATION_CONNECTION_FAILED";
            public const string MIGRATION_RECORD_AVAILABLE = "MIGRATION_RECORD_AVAILABLE";
            public const string MIGRATION_LOG_FILE_OPEN = "MIGRATION_LOG_FILE_OPEN";
            public const string MIGRATION_COMPLETED_SUCCESSFULLY = "MIGRATION_COMPLETED_SUCCESSFULLY";
            public const string MIGRATION_ACCESS_DB_EMPTY = "MIGRATION_ACCESS_DB_EMPTY";
            public const string MIGRATION_TITLE = "MIGRATION_TITLE";
            public const string MIGRATION_CONNECTION_SUCCEED = "MIGRATION_CONNECTION_SUCCEED";
            public const string MIGRATION_DESTINATION_DB_FAILS = "MIGRATION_DESTINATION_DB_FAILS";
            public const string MIGRATION_SOURCE_DB_FAILS = "MIGRATION_SOURCE_DB_FAILS";
            public const string MIGRATION_VOUCHER_TRANS_FAILS = "MIGRATION_VOUCHER_TRANS_FAILS";
            public const string MIGRATION_ERROR_TRANSACTION_BALANCE = "MIGRATION_ERROR_TRANSACTION_BALANCE";
            public const string MIGRATION_CONFIRMATION = "MIGRATION_CONFIRMATION";

            public static class TallyMigration
            {
                public const string INVALID_EXCEL_FORMAT = "INVALID_EXCEL_FORMAT";
                public const string CONFIRMATION_MESSAGE = "CONFIRMATION_MESSAGE";
                public const string MASTER_EMPTY = "MASTER_EMPTY";
                public const string OPEN_FILE = "OPEN_FILE";
                public const string SUCCESS_MESSAGE = "SUCCESS_MESSAGE";
                public const string ERROR_MESSAGE = "ERROR_MESSAGE";
                public const string INVALID_TRANSACTION = "INVALID_TRANSACTION";
                public const string PROJECT_EXISTS = "PROJECT_EXISTS";
                public const string WITHOUT_TRANS = "WITHOUT_TRANS";

            }

        }
        #endregion

        #region Data Synchronization
        public class DataSynchronization
        {
            public class Common
            {
                public const string DATA_SYNCHRONISATION_SUCCESS = "DATA_SYNCHRONISATION_SUCCESS";
                public const string DATA_SYNCHRONISATION_SUCCESSLOG = "DATA_SYNCHRONISATION_SUCCESSLOG";
                public const string DSYNC_IMPORT_CONFIRMATION_MESSAGE = "DSYNC_IMPORT_CONFIRMATION_MESSAGE";
                public const string DATA_SYNCHRONISATION_ONLINE = "DATA_SYNCHRONISATION_ONLINE";
                public const string DATA_SYN_FILE_SELECTION = "DATA_SYN_FILE_SELECTION";
                public const string DSYNC_SELECT_XML_IMPORT = "DSYNC_SELECT_XML_IMPORT";
                public const string DSYNC_WELCOME_SYNCHRONISATION = "DSYNC_WELCOME_SYNCHRONISATION";
                public const string ACMEERP = "ACMEERP";
            }
            public class Import
            {
                public const string DSYNC_IMPORT_MASTER_PROBLEM = "DSYNC_IMPORT_MASTER_PROBLEM";
                public const string DSYNC_IMPORT_BRANCH_TO_HEADOFFICE = "DSYNC_IMPORT_BRANCH_TO_HEADOFFICE";
                public const string DSYNC_DATAFROM_HEADOFFICE_BRANCHOFFICE = "DSYNC_DATAFROM_HEADOFFICE_BRANCHOFFICE";
                public const string DSYNC_CONFIRMATION_IMPORT_MASTERS = "DSYNC_CONFIRMATION_IMPORT_MASTERS";
                public const string DSYNC_SUCCESS_DATAFROM_HEADOFFICE_BRANCHOFFICE = "DSYNC_SUCCESS_DATAFROM_HEADOFFICE_BRANCHOFFICE";
                public const string DSYNC_IMPORT_LEGALENTITY_HEADOFFICE_BRACHOFFICE = "DSYNC_IMPORT_LEGALENTITY_HEADOFFICE_BRACHOFFICE";
                public const string DSYNC_SUCCESS_LEGALENTITY_HEADOFFICE_BRACHOFFICE = "DSYNC_SUCCESS_LEGALENTITY_HEADOFFICE_BRACHOFFICE";
                public const string DSYNC_IMPORT_PROJECTCATOGORY_HEADOFFICE_BRACHOFFICE = "DSYNC_IMPORT_PROJECTCATOGORY_HEADOFFICE_BRACHOFFICE";
                public const string DSYNC_SUCCESS_PROJECTCATOGORY_HEADOFFICE_BRACHOFFICE = "DSYNC_SUCCESS_PROJECTCATOGORY_HEADOFFICE_BRACHOFFICE";
                public const string DSYNC_IMPORT_PROJECT_HEADOFFICE_BRACHOFFICE = "DSYNC_IMPORT_PROJECT_HEADOFFICE_BRACHOFFICE";
                public const string DSYNC_SUCCESS_PROJECT_HEADOFFICE_BRACHOFFICE = "DSYNC_SUCCESS_PROJECT_HEADOFFICE_BRACHOFFICE";
                public const string DSYNC_IMPORT_LEDGER_HEADOFFICE_BRACHOFFICE = "DSYNC_IMPORT_LEDGER_HEADOFFICE_BRACHOFFICE";
                public const string DSYNC_SUCCESS_LEDGER_HEADOFFICE_BRACHOFFICE = "DSYNC_SUCCESS_LEDGER_HEADOFFICE_BRACHOFFICE";
                public const string DSYNC_IMPORT_LEDGERGROUP_HEADOFFICE_BRACHOFFICE = "DSYNC_IMPORT_LEDGERGROUP_HEADOFFICE_BRACHOFFICE";
                public const string DSYNC_SUCCESS_LEDGERGROUP_HEADOFFICE_BRACHOFFICE = "DSYNC_SUCCESS_LEDGERGROUP_HEADOFFICE_BRACHOFFICE";
                public const string DSYNC_IMPORT_FCPURPOSE_HEADOFFICE_BRACHOFFICE = "DSYNC_IMPORT_FCPURPOSE_HEADOFFICE_BRACHOFFICE";
                public const string DSYNC_SUCCESS_FCPURPOSE_HEADOFFICE_BRACHOFFICE = "DSYNC_SUCCESS_FCPURPOSE_HEADOFFICE_BRACHOFFICE";
                public const string DSYNC_DATA_SYN_ONLINE = "DSYNC_DATA_SYN_ONLINE";
                public const string DSYNC_CHECK_INTERNET_CONNECTION = "DSYNC_CHECK_INTERNET_CONNECTION";
                public const string DSYNC_MASTER_SUCCESS = "DSYNC_MASTER_SUCCESS";

                //DataImport,Export and Merge Project
                public const string OVERWRITE_CONFORM = "OVERWRITE_CONFORM";
                public const string APPEND_CONFORM = "APPEND_CONFORM";
                public const string IMPORT_SUCCESSFULLY = "IMPORT_SUCCESSFULLY";
                public const string FILE_NAME_EMPTY = "FILE_NAME_EMPTY";
                public const string BRANCH_CODE_MATCH_ERROR = "BRANCH_CODE_MATCH_ERROR";
                public const string HEAD_OFFICE_CODE_MATCH_ERROR = "HEAD_OFFICE_CODE_MATCH_ERROR";
            }

            public class ImportExcel
            {
                public const string CUSTODIAN_IMPORT_SUCCESSFULLY = "CUSTODIAN_IMPORT_SUCCESSFULLY";
                public const string DONOR_IMPORT_SUCCESSFULLY = "DONOR_IMPORT_SUCCESSFULLY";
                public const string GROUP_IMPORT_SUCCESSFULLY = "GROUP_IMPORT_SUCCESSFULLY";
                public const string SELECT_VALID_FILE = "SELECT_VALID_FILE";
                public const string LOCATION_NOT_AVAIL = "LOCATION_NOT_AVAIL";
                public const string FILE_EMPTY = "FILE_EMPTY";
                public const string GROUP_NOT_AVAIL = "GROUP_NOT_AVAIL";
                public const string ITEM__NOT_AVAIL = "ITEM__NOT_AVAIL";
                public const string CUSTODIAN_NOT_AVAIL = "CUSTODIAN_NOT_AVAIL";
                public const string VENDOR_NOT_AVAIL = "VENDOR_NOT_AVAIL";
                public const string MANUFACTURER_NOT_AVAIL = "MANUFACTURER_NOT_AVAIL";
                public const string FILE_NOT_EXIT = "FILE_NOT_EXIT";
            }
            public class Mapping
            {
                public const string DSYNC_MAP_HEADOFFICE_lEDGERS = "DSYNC_MAP_HEADOFFICE_lEDGERS";
                public const string DSYNC_VALIDATE_OP_TRANS = "DSYNC_VALIDATE_OP_TRANS";
            }
            public class Export
            {
                public const string DSYNC_SELECT_ONE_PROJECT = "DSYNC_SELECT_ONE_PROJECT";
                public const string DSYNC_VALIDATE_DATE = "DSYNC_VALIDATE_DATE";
                public const string DSYNC_NO_RECORD_EXISTS = "DSYNC_NO_RECORD_EXISTS";
                public const string DSYNC_EXPORT_VOUCHERS_SUCCESS = "DSYNC_EXPORT_VOUCHERS_SUCCESS";
                public const string DSYNC_EXPORT_VOUCHERS_FAILURE = "DSYNC_EXPORT_VOUCHERS_FAILURE";
            }
            public class ImportVoucher
            {
                public const string DSYNC_CONFIRMATION_BRANCH_HEADOFFICE_VOUCHERS = "DSYNC_CONFIRMATION_BRANCH_HEADOFFICE_VOUCHERS";
                public const string DSYNC_PROBLEM_IMPORT_RECORDS = "DSYNC_PROBLEM_IMPORT_RECORDS";
                public const string DSYNC_PROBLEM_IMPORT_BRANCH_HEADOFFICE = "DSYNC_PROBLEM_IMPORT_BRANCH_HEADOFFICE";
                public const string DSYNC_IMPORT_BRANCH_TO_HEADOFFICEVOUCHER = "DSYNC_IMPORT_BRANCH_TO_HEADOFFICEVOUCHER";
                public const string DSYNC_SUCCESS_BRANCH_TO_HEADOFFICE_VOUCHER = "DSYNC_SUCCESS_BRANCH_TO_HEADOFFICE_VOUCHER";
                public const string DSYNC_VALIDATEPROJECT_BRANCH_TO_HEADOFFICE = "DSYNC_VALIDATEPROJECT_BRANCH_TO_HEADOFFICE";

            }
        }
        #endregion

        #region Payroll

        public class Payroll
        {
            public class Staff
            {
                public const string STAFF_CODE_NULL = "STAFF_CODE_NULL"; //"Code is empty";
                public const string STAFF_FIRST_NAME_NULL = "STAFF_FIRST_NAME_NULL"; //"First name is empty";
                public const string STAFF_GENDGER_NULL = "STAFF_GENDGER_NULL"; //"Gender is empty";
                public const string STAFF_DESIGNATION_NULL = "STAFF_DESIGNATION_NULL";//"Designation is empty";
                public const string STAFF_DATE_OF_JOINING_NULL = "STAFF_DATE_OF_JOINING_NULL";//"Date of Joining is empty";
                public const string STAFF_DATE_OF_BIRTH_NULL = "STAFF_DATE_OF_BIRTH_NULL";//"Date of Birth is empty";
                public const string STAFF_NOT_VALID_DATE_OF_BIRTH = "STAFF_NOT_VALID_DATE_OF_BIRTH"; //"Date of Birth can't be greater than or equal to today";
                public const string STAFF_NOT_VALID_DATE_OF_JOINING_DATE = "STAFF_NOT_VALID_DATE_OF_JOINING_DATE";//"Date of Join can't be greater than today";
                public const string STAFF_NOT_VALID_DATE_OF_JOINING_DATE_AGAINEST_DATE_OF_BIRTH = "STAFF_NOT_VALID_DATE_OF_JOINING_DATE_AGAINEST_DATE_OF_BIRTH"; //"Date of Joining should be greater than the Date of Birth";
                public const string STAFF_NOT_VALID_RETIREMENT_DATE = "STAFF_NOT_VALID_RETIREMENT_DATE";//"Retirement date can't be less than today";
                public const string STAFF_NOT_VALID_LEAVEING_DATE = "STAFF_NOT_VALID_LEAVEING_DATE"; //"Date of Joining is greater than or Equal to leaving date";
                public const string STAFF_NOT_BETWEEN_DATEOFJOIN = "STAFF_NOT_BETWEEN_DATEOFJOIN"; //"Date of Joining sholud be less than the payroll period end date";
                public const string STAFF_DETAILS_SAVED = "STAFF_DETAILS_SAVED"; //"Record saved";
                public const string STAFF_DETAILS_NOT_SAVED = "STAFF_DETAILS_NOT_SAVED";//"Record not saved";

                public const string COMMON_DELETE_CONFIRMATION = "COMMON_DELETE_CONFIRMATION";//" Are you sure to delete?";//CHECK
                public const string STAFF_DELETE_SUCCESS = "STAFF_DELETE_SUCCESS";//"Record deleted";
                public const string STAFF_DELETE_FAILURE = "STAFF_DELETE_FAILURE"; //"Record not deleted";
                public const string STAFF_CAN_NOT_DELETE = "STAFF_CAN_NOT_DELETE";//"Can not delete, It has association";
                public const string STAFF_SCALE_OF_PAY_EMPTY = "STAFF_SCALE_OF_PAY_EMPTY"; //"Scale of pay is empty";
                public const string STAFF_SCALE_OF_PAY_ZERO = "STAFF_SCALE_OF_PAY_ZERO"; //"Enter Valid Scale of Pay Amount";
                public const string STAFF_PROJECT_EMPTY = "STAFF_PROJECT_EMPTY"; //"Project is empty";
                public const string STAFF_GROUP_EMPTY = "STAFF_GROUP_EMPTY";//"Payroll group is empty";
                public const string STAFF_CANNOT_UNMAP_INFO = "STAFF_CANNOT_UNMAP_INFO";//Cannot unmap, It has association 
                public const string STAFF_ADD_CAPTION = "STAFF_ADD_CAPTION";
                public const string STAFF_EDIT_CAPTION = "STAFF_EDIT_CAPTION";
                public const string STAFF_PRINT_CAPTION = "STAFF_PRINT_CAPTION";
                public const string STAFF_VIEW_CAPTION = "STAFF_VIEW_CAPTION";
            }

            public class LoanManagement
            {
                public const string LOAN_MGT_PRINT_CAPTION = "LOAN_MGT_PRINT_CAPTION";//"Printed";
                public const string COMMON_DELETED_CONFIRMATION = "COMMON_DELETED_CONFIRMATION"; //" Are you sure to delete?"; CHECK
                public const string COMPONENT_DELETE_SUCCESS = "COMPONENT_DELETE_SUCCESS"; //"Record deleted";
                public const string COMPONENT_DELETE_FAILURE = "COMPONENT_DELETE_FAILURE";//"Record not deleted";
                public const string COMPONENT_CAN_NOT_DELETE = "COMPONENT_CAN_NOT_DELETE"; //"Can not delete, It has association";

                public const string LOAN_MGT_DELETE_SUCCESS = "LOAN_MGT_DELETE_SUCCESS"; //"Record deleted";
                public const string LOAN_MGT_DELETE_FAILURE = "LOAN_MGT_DELETE_FAILURE";//"Record not deleted";
                public const string LOAN_MGT_CAN_NOT_DELETE = "LOAN_MGT_CAN_NOT_DELETE"; //"Can not delete, It has association";

                public const string LOAN_MGT_DETAILS_SAVED = "LOAN_MGT_DETAILS_SAVED";//"Record saved";
                public const string LOAN_MGT_DETAILS_NOT_SAVED = "LOAN_MGT_DETAILS_NOT_SAVED"; //"Record not saved";
                public const string LOAN_MGT_ADD_CAPTION = "LOAN_MGT_ADD_CAPTION";
                public const string LOAN_MGT_EDIT_CAPTION = "LOAN_MGT_EDIT_CAPTION";
                public const string LOAN_MGT_VIEW_CAPTION = "LOAN_MGT_VIEW_CAPTION";
                public const string LOAN_MGT_RATE_INTEREST_EMTPY = "LOAN_MGT_RATE_INTEREST_EMTPY";
                public const string LOAN_MGT_EXITS_ALREADY_INFO = "LOAN_MGT_EXITS_ALREADY_INFO";
                public const string LOAN_MGT_NO_INSTALLMENT_GREATERTHAN_ZERO_INFO = "LOAN_MGT_NO_INSTALLMENT_GREATERTHAN_ZERO_INFO";
                public const string LOAN_MGT_RETIRED_STAFF_LOAN_NOTPROVIDE_INFO = "LOAN_MGT_RETIRED_STAFF_LOAN_NOTPROVIDE_INFO";
                public const string LOAN_MGT_MAXNO_INSTALLMENTS_SELECT_STAFF_INFO = "LOAN_MGT_MAXNO_INSTALLMENTS_SELECT_STAFF_INFO";
                public const string LOAN_MGT_MAXNO_RETIREDON_INFO = "LOAN_MGT_MAXNO_RETIREDON_INFO";
                public const string LOAN_MGT_MAXNO_INSTALL_GREATERTHAN_NEGATIVE_VALUE_INFO = "LOAN_MGT_MAXNO_INSTALL_GREATERTHAN_NEGATIVE_VALUE_INFO";
                public const string LOAN_MGT_STAFF_NAME_EMPTY = "LOAN_MGT_STAFF_NAME_EMPTY";
                public const string LOAN_MGT_LOAN_NAME_EMPTY = "LOAN_MGT_LOAN_NAME_EMPTY";
                public const string LOAN_MGT_LOAN_AMOUNT_EMPTY = "LOAN_MGT_LOAN_AMOUNT_EMPTY";
                public const string LOAN_MGT_LOAN_INSTALLMENT_EMPTY = "LOAN_MGT_LOAN_INSTALLMENT_EMPTY";
                public const string LOAN_MGT_LOAN_RATE_INTEREST_EMPTY = "LOAN_MGT_LOAN_RATE_INTEREST_EMPTY";
                public const string LOAN_MGT_PAYFROM_DATE_EMPTY = "LOAN_MGT_PAYFROM_DATE_EMPTY";
                public const string LOAN_MGT_PAYTODATE_GREATERTHAN_PAYFROMDATE_INFO = "LOAN_MGT_PAYTODATE_GREATERTHAN_PAYFROMDATE_INFO";
                public const string LOAN_MGT_LOAN_CANNOT_PROVIDE_PAST_DAYS = "LOAN_MGT_LOAN_CANNOT_PROVIDE_PAST_DAYS";
                public const string LOAN_MGT_LOAN_CANNOT_PROVIDE_PAST_DAYS_CHECK_PAYTO_DATE_INFO = "LOAN_MGT_LOAN_CANNOT_PROVIDE_PAST_DAYS_CHECK_PAYTO_DATE_INFO";
                public const string LOAN_MGT_LOAN_CASHBANK_LEDGER_EMPTY = "LOAN_MGT_LOAN_CASHBANK_LEDGER_EMPTY";
                public const string LOAN_MGT_LOAN_MONTH_INFO = "LOAN_MGT_LOAN_MONTH_INFO";
                public const string LOAN_MGT_LEDGER_MAPPINGWITH_PROJECT_END_INFO = "LOAN_MGT_LEDGER_MAPPINGWITH_PROJECT_END_INFO";
            }
            public class Loan
            {
                public const string LOAN_PRINT_CAPTION = "LOAN_PRINT_CAPTION";//"Printed";
                public const string COMMON_DELETED_CONFIRMATION = "COMMON_DELETED_CONFIRMATION";//" Are you sure to delete?"; CHECK
                public const string LOAN_DELETE_SUCCESS = "LOAN_DELETE_SUCCESS"; //"Record deleted";
                public const string LOAN_DELETE_FAILURE = "LOAN_DELETE_FAILURE";//"Record not deleted";
                public const string LOAN_CAN_NOT_DELETE = "LOAN_CAN_NOT_DELETE"; //"Can not delete, It has association";
                public const string LOAN_DETAILS_SAVED = "LOAN_DETAILS_SAVED"; //"Record saved";
                public const string LOAN_DETAILS_NOT_SAVED = "LOAN_DETAILS_NOT_SAVED"; //"Record not saved";
                public const string LOAN_DETAILS_EMPTY = "LOAN_DETAILS_EMPTY";//"No record is available in the grid to delete";
                public const string LOAN_ADD_CAPTION = "LOAN_ADD_CAPTION";
                public const string LOAN_EDIT_CAPTION = "LOAN_EDIT_CAPTION";
                public const string LOAN_VIEW_CAPTION = "LOAN_VIEW_CAPTION";
                public const string LOAN_NAME_EMPTY = "LOAN_NAME_EMPTY";
            }

            public class LockPayroll
            {
                public const string LOCK_PAYROLL_CAPTION = "LOCK_PAYROLL_CAPTION";//"Printed";
                public const string LOCK_DELETE_SUCCESS = "LOCK_DELETE_SUCCESS"; //"Record deleted";
                public const string LOCK_VIEW_CAPTION = "LOCK_VIEW_CAPTION";
                public const string LOCK_PAYROLL_NOT_AVAILABLE_INFO = "LOCK_PAYROLL_NOT_AVAILABLE_INFO";
                public const string UNLOCK_PAYROLL_CAPTION = "UNLOCK_PAYROLL_CAPTION";
                public const string LOCK_PAYROLL_MONTH = "LOCK_PAYROLL_MONTH";
                public const string LOCK_PAYROLL_CHANGE_INFO = "LOCK_PAYROLL_CHANGE_INFO";
                public const string LOCK_PAYROLL_INFO = "LOCK_PAYROLL_INFO";
                public const string LOCK_PAYROLL_MONTH_INFO = "LOCK_PAYROLL_MONTH_INFO";
                public const string LOCK_PAYROLL_AFTERLOCK_INFO = "LOCK_PAYROLL_AFTERLOCK_INFO";
            }
            public class OpenPayroll
            {
                public const string OPENPAYROLL_SELECT_PAYROLL_LOCKED_INFO = "OPENPAYROLL_SELECT_PAYROLL_LOCKED_INFO";//"Printed";
                public const string LOCK_DELETE_SUCCESS = "LOCK_DELETE_SUCCESS"; //"Record deleted";
                public const string LOCK_PAYROLL_CAPTION = "LOCK_PAYROLL_CAPTION";
            }
            public class CreatePayroll
            {
                public const string CREATE_PAYROLL_IMPORT_INFO = "CREATE_PAYROLL_IMPORT_INFO";
                public const string CREATE_PAYROLL_CREATE_INFO = "CREATE_PAYROLL_CREATE_INFO";
                public const string CREATE_PAYROLL_IMPORT_PAYROLLFROM_INFO = "CREATE_PAYROLL_IMPORT_PAYROLLFROM_INFO";
                public const string CREATE_PAYROLL_IMPORT_CAPTION = "CREATE_PAYROLL_IMPORT_CAPTION";
                public const string CREATE_PAYROLL_CREATE_CAPTION = "CREATE_PAYROLL_CREATE_CAPTION";
                public const string CREATE_PAYROLL_FOR_INFO = "CREATE_PAYROLL_FOR_INFO";
            }
            public class BuildFormula
            {
                public const string BUILD_FORMULA_COMPONENT1_FIELD_EMPTY = "BUILD_FORMULA_COMPONENT1_FIELD_EMPTY";
                public const string BUILD_FORMULA_OPERATOR1_FIELD_EMPTY = "BUILD_FORMULA_OPERATOR1_FIELD_EMPTY";
                public const string BUILD_FORMULA_VALUE1_FIELD_EMPTY = "BUILD_FORMULA_VALUE1_FIELD_EMPTY";
                public const string BUILD_FORMULA_FIELD_EMPTY = "BUILD_FORMULA_FIELD_EMPTY";

                public const string BUILD_FORMULA_COMPONENT2_FIELD_EMPTY = "BUILD_FORMULA_COMPONENT2_FIELD_EMPTY";
                public const string BUILD_FORMULA_OPERATOR2_FIELD_EMPTY = "BUILD_FORMULA_OPERATOR2_FIELD_EMPTY";
                public const string BUILD_FORMULA_VALUE2_FIELD_EMPTY = "BUILD_FORMULA_VALUE2_FIELD_EMPTY";
                public const string BUILD_FORMULA2_FIELD_EMPTY = "BUILD_FORMULA2_FIELD_EMPTY";
                public const string BUILD_FORMULA_ADDED_INFO = "BUILD_FORMULA_ADDED_INFO";
                public const string BUILD_INVALID_FORMULA_INFO = "BUILD_INVALID_FORMULA_INFO";
                public const string BUILD_COMPONENT_EMPTY = "BUILD_COMPONENT_EMPTY";
            }

            public class ConstructFormula
            {
                public const string COSNSTRUCT_FORMULA_COMPONENT_INFO = "COSNSTRUCT_FORMULA_COMPONENT_INFO";
                public const string COSNSTRUCT_FORMULA_ENTER_FORMULA_INFO = "COSNSTRUCT_FORMULA_ENTER_FORMULA_INFO";
                public const string COSNSTRUCT_FORMULA_COMP_REFER_INFO = "COSNSTRUCT_FORMULA_COMP_REFER_INFO";
                public const string COSNSTRUCT_FORMULA_VALID_FORMULA_INFO = "COSNSTRUCT_FORMULA_VALID_FORMULA_INFO";
                public const string COSNSTRUCT_FORMULA_INVALID_FORMULA_INFO = "COSNSTRUCT_FORMULA_INVALID_FORMULA_INFO";
            }

            public class MapComponentstoGroups
            {
                public const string PAYOLL_GROUP_CAPTION = "PAYOLL_GROUP_CAPTION";
                public const string PAYROLL_GROUP_EMPTY = "PAYROLL_GROUP_EMPTY";
                public const string PAYROLL_COMP_MAP_INFO = "PAYROLL_COMP_MAP_INFO";
                public const string PAYROLL_COMP_SELECT_INFO = "PAYROLL_COMP_SELECT_INFO";
                public const string PAYROLL_COMPONENT_MAP_INFO = "PAYROLL_COMPONENT_MAP_INFO";
                public const string PAYROLL_COMPONENT_UNMAP_INFO = "PAYROLL_COMPONENT_UNMAP_INFO";
                public const string PAYROLL_COMPONENT_ORDER_INFO = "PAYROLL_COMPONENT_ORDER_INFO";
            }

            public class PaySlipViewer
            {
                public const string PAY_SLIP_SELECT_PRINTER_INFO = "PAY_SLIP_SELECT_PRINTER_INFO";
                public const string PAY_SLIP_TEMPLATE_SELECT_INFO = "PAY_SLIP_TEMPLATE_SELECT_INFO";
                public const string PAY_SLIP_SEELCT_STAFF_INFO = "PAY_SLIP_SEELCT_STAFF_INFO";
            }

            //public class PaySlipViewer
            //{
            //    public const string PAYOLL_GROUP_CAPTION = "PAYOLL_GROUP_CAPTION";
            //    public const string PAYROLL_GROUP_EMPTY = "PAYROLL_GROUP_EMPTY";
            //    public const string PAYROLL_COMP_MAP_INFO = "PAYROLL_COMP_MAP_INFO";
            //    public const string PAYROLL_COMP_SELECT_INFO = "PAYROLL_COMP_SELECT_INFO";
            //    public const string PAYROLL_COMPONENT_MAP_INFO = "PAYROLL_COMPONENT_MAP_INFO";
            //    public const string PAYROLL_COMPONENT_UNMAP_INFO = "PAYROLL_COMPONENT_UNMAP_INFO";
            //    public const string PAYROLL_COMPONENT_ORDER_INFO = "PAYROLL_COMPONENT_ORDER_INFO";
            //}

            public class PayrollView
            {
                public const string PAYOLL_VIEW_PROJECT_EMPTY = "PAYOLL_VIEW_PROJECT_EMPTY";
                public const string PAYOLL_VIEW_GROUP_EMPTY = "PAYOLL_VIEW_GROUP_EMPTY";
            }

            public class MapProjectPayroll
            {
                public const string MAP_PROJECT_PAYROLL_PROJECT_EMPTY = "MAP_PROJECT_PAYROLL_PROJECT_EMPTY";
                public const string MAP_PROJECT_PAYROLL_MAP_PROJECT_INFO = "MAP_PROJECT_PAYROLL_MAP_PROJECT_INFO";
                public const string MAP_PROJECT_PAYROLL_UNMAP_PROJECT_INFO = "MAP_PROJECT_PAYROLL_UNMAP_PROJECT_INFO";
                public const string MAP_PROJECT_PAYROLL_UNMAP_INFO = "MAP_PROJECT_PAYROLL_UNMAP_INFO";
            }

            public class MapProcessLedgers
            {
                public const string MAP_PROCESS_LEDGER_MAP_INFO = "MAP_PROCESS_LEDGER_MAP_INFO";
                public const string MAP_PROCESS_LEDGER_UNMAP_INFO = "MAP_PROCESS_LEDGER_UNMAP_INFO";
                public const string MAP_PROCESS_TYPE_EMPTY = "MAP_PROCESS_TYPE_EMPTY";
                public const string MAP_PROCESS_LEDGER_EMPTY = "MAP_PROCESS_LEDGER_EMPTY";
            }

            public class GetTemplate
            {
                public const string GET_TEMP_INVALID_DATA_INFO = "GET_TEMP_INVALID_DATA_INFO";
                public const string GET_TEMP_VALIDATE_EXCEL_INFO = "GET_TEMP_VALIDATE_EXCEL_INFO";
                public const string GET_TEMP_VALIDATE_FILE_INFO = "GET_TEMP_VALIDATE_FILE_INFO";
                public const string GET_TEMP_FETCH_DATA_EXCEL_INFO = "GET_TEMP_FETCH_DATA_EXCEL_INFO";
                public const string GET_TEMP_EXCEL_EMPTY_FIELD_INFO = "GET_TEMP_EXCEL_EMPTY_FIELD_INFO";
                public const string GET_TEMP_EXCEL_IMPORT_INFO = "GET_TEMP_EXCEL_IMPORT_INFO";
                public const string GET_TEMP_NOVALID_RECORD_INFO = "GET_TEMP_NOVALID_RECORD_INFO";
                public const string GET_TEMP_PROJECT_AVAILABLE_INFO = "GET_TEMP_PROJECT_AVAILABLE_INFO";
            }

            public class ACCPMain
            {
                //Payroll
                public const string PAYROLL_CREATE_INFO = "PAYROLL_CREATE_INFO";
                public const string PAYROLL_MONTHFOR_INFO = "PAYROLL_MONTHFOR_INFO";
                public const string PAYROLL_NOPAYROLL_EXISTS_INFO = "PAYROLL_NOPAYROLL_EXISTS_INFO";
                public const string PAYROLL_UNLOCK_CAPTION = "PAYROLL_UNLOCK_CAPTION";
                public const string PAYROLL_LOCK_CAPTION = "PAYROLL_LOCK_CAPTION";
                public const string PAYROLL_ISLOCK_CAPTION = "PAYROLL_ISLOCK_CAPTION";
                public const string PAYROLL_MONTH_FOR_INFO = "PAYROLL_MONTH_FOR_INFO";
            }

            public class ProcessPayroll
            {
                public const string PROCESS_PAYROLL_PROCESS_INFO = "PROCESS_PAYROLL_PROCESS_INFO";
                public const string PROCESS_PAYROLL_MAP_PROJECT_INFO = "PROCESS_PAYROLL_MAP_PROJECT_INFO";
            }

            public class RangeFormula
            {
                public const string RANGE_FORMULA_COMPONENT_EMPTY = "RANGE_FORMULA_COMPONENT_EMPTY";
                public const string RANGE_FORMULA_MIN_VALUE_EMPTY = "RANGE_FORMULA_MIN_VALUE_EMPTY";
                public const string RANGE_FORMULA_MAX_VALUE_EMPTY = "RANGE_FORMULA_MAX_VALUE_EMPTY";
                public const string RANGE_FORMULA_MAX_SALP_EMPTY = "RANGE_FORMULA_MAX_SALP_EMPTY";
                public const string RANGE_FORMULA_MAX_MIN_VALID_INFO = "RANGE_FORMULA_MAX_MIN_VALID_INFO";
                public const string RANGE_FORMULA_COMMON_DELETE_INFO = "RANGE_FORMULA_COMMON_DELETE_INFO";
                public const string RANGE_FORMULA_DEFINE_RANGE_INFO = "RANGE_FORMULA_DEFINE_RANGE_INFO";
            }

            public class PayrollBrowseView
            {
                public const string BROWSE_INVALID_COMPONENT_INFO = "BROWSE_INVALID_COMPONENT_INFO";
                public const string BROWSE_PROJECT_EMPTY = "BROWSE_PROJECT_EMPTY";
                public const string BROWSE_GROUP_EMPTY = "BROWSE_GROUP_EMPTY";
                public const string BROWSE_MAP_LEDGER_PROJECT_INFO = "BROWSE_MAP_LEDGER_PROJECT_INFO";
                public const string BROWSE_MAP_LEDGER_COMPONENT_INFO = "BROWSE_MAP_LEDGER_COMPONENT_INFO";
                // public const string BROWSE_MAP_LEDGER_COMPONENT_INFO = "BROWSE_MAP_LEDGER_COMPONENT_INFO";
                public const string BROWSE_MAP_PROCESS_TYPE_LEDGER_MAP_INFO = "BROWSE_MAP_PROCESS_TYPE_LEDGER_MAP_INFO";
                public const string BROWSE_MAP_PROCESS_TYPE_LEDGER_NOTMAP_INFO = "BROWSE_MAP_PROCESS_TYPE_LEDGER_NOTMAP_INFO";
                public const string BROWSE_MAP_PROCESS_INFO = "BROWSE_MAP_PROCESS_INFO";
                public const string BROWSE_MAP_STAFF_MAPWITH_GROUP_INFO = "BROWSE_MAP_STAFF_MAPWITH_GROUP_INFO";
                public const string BROWSE_MAP_COMPONENT_MAPWITH_GROUP_INFO = "BROWSE_MAP_COMPONENT_MAPWITH_GROUP_INFO";
                public const string BROWSE_MAP_PROJECT_NOT_AVAILABLE_INFO = "BROWSE_MAP_PROJECT_NOT_AVAILABLE_INFO";
                public const string BROWSE_MAP_PAYROLL_PROCESS_MONTH_INFO = "BROWSE_MAP_PAYROLL_PROCESS_MONTH_INFO";
            }
            public class Component
            {
                public const string COMMON_DELETED_CONFIRMATION = "COMMON_DELETED_CONFIRMATION";//" Are you sure to delete?"; CHECK
                public const string COMPONENT_SAVE_SUCCESS = "COMPONENT_SAVE_SUCCESS"; //"Record saved";
                public const string COMPONENT_SAVE_FAILTURE = "COMPONENT_SAVE_FAILTURE"; //"Record not saved";
                public const string COMPONENT_DELETE_SUCCESS_INFO = "COMPONENT_DELETE_SUCCESS_INFO"; //"Record deleted";
                public const string COMPONENT_DELETE_FAILURE = "COMPONENT_DELETE_FAILURE";//"Record not deleted";
                public const string COMPONENT_CAN_NOT_DELETE = "COMPONENT_CAN_NOT_DELETE"; //"Can not delete, It has association"; CHECK
                public const string COMPONENT_ADD_CAPTION = "COMPONENT_ADD_CAPTION";
                public const string COMPONENT_EDIT_CAPTION = "COMPONENT_EDIT_CAPTION";
                public const string COMPONENT_VIEW_CAPTION = "COMPONENT_VIEW_CAPTION";
                public const string COMPONENT_PRINT_CAPTION = "COMPONENT_PRINT_CAPTION";
                public const string COMPONENT_NAME_INVALID_INFO = "COMPONENT_NAME_INVALID_INFO";
                public const string COMPONENT_NAME_EMPTY = "COMPONENT_NAME_EMPTY";
                public const string COMPONENT_TYPE_EMPTY = "COMPONENT_TYPE_EMPTY";
                public const string COMPONENT_LEDGER_EMPTY = "COMPONENT_LEDGER_EMPTY";
                public const string COMPONENT_PROCESS_TYPE_LEDGER_EMPTY = "COMPONENT_PROCESS_TYPE_LEDGER_EMPTY";
                public const string COMPONENT_LINK_VALUE_EMPTY = "COMPONENT_LINK_VALUE_EMPTY";
                public const string COMPONENT_EQUATION_EMPTY = "COMPONENT_EQUATION_EMPTY";
                public const string COMPONENT_CIRCULAR_REF_INFO = "COMPONENT_CIRCULAR_REF_INFO";
                public const string COMPONENT_REMOVE_FORMULA_INFO = "COMPONENT_REMOVE_FORMULA_INFO";
                public const string COMPONENT_EXITS_ALREADY_INFO = "COMPONENT_EXITS_ALREADY_INFO";
                public const string COMPONENT_FORMULA_CANNOT_REFER_ITSELF_INFO = "COMPONENT_FORMULA_CANNOT_REFER_ITSELF_INFO";
                public const string COMPONENT_CANNOT_UPDATE_INFO = "COMPONENT_CANNOT_UPDATE_INFO";
            }

            public class PayrollGroup
            {
                public const string COMMON_DELETED_CONFIRMATION = "COMMON_DELETED_CONFIRMATION"; //" Are you sure to delete?";
                public const string GROUP_DELETE_SUCCESS = "GROUP_DELETE_SUCCESS";//"Record deleted";
                public const string GROUP_DELETE_FAILURE = "GROUP_DELETE_FAILURE"; //"Record not deleted";
                public const string GROUP_CAN_NOT_DELETE = "GROUP_CAN_NOT_DELETE";//"Can not delete, It has association";
                public const string PAYROLL_GROUP_DETAILS_SAVED = "PAYROLL_GROUP_DETAILS_SAVED";
                public const string GROUP_DETAILS_NOT_SAVED = "GROUP_DETAILS_NOT_SAVED";//"Record not saved";
                public const string PAYROLL_GROUP_NAME_EMPTY = "PAYROLL_GROUP_NAME_EMPTY";
                public const string PAYROLL_GROUP_EXISTS = "PAYROLL_GROUP_EXISTS";
                public const string PAYROLL_GROUP_ADD_CAPTION = "PAYROLL_GROUP_ADD_CAPTION";
                public const string PAYROLL_GROUP_EDIT_CAPTION = "PAYROLL_GROUP_EDIT_CAPTION";
                public const string PAYROLL_GROUP_EXISTS_ALREADY = "PAYROLL_GROUP_EXISTS_ALREADY";
                public const string PAYROLL_GROUP_NORECORD_SELECT_EDIT_INFO = "PAYROLL_GROUP_NORECORD_SELECT_EDIT_INFO";
                public const string PAYROLL_GROUP_VIEW_CAPTION = "PAYROLL_GROUP_VIEW_CAPTION";
            }

            public class PayrollDepartment
            {                                
                public const string PAYROLL_DEPARTMENT_NAME_EMPTY = "PAYROLL_DEPARTMENT_NAME_EMPTY";
                public const string PAYROLL_DEPARTMENT_EXISTS = "PAYROLL_DEPARTMENT_EXISTS";
                public const string PAYROLL_DEPARTMENT_ADD_CAPTION = "PAYROLL_DEPARTMENT_ADD_CAPTION";
                public const string PAYROLL_DEPARTMENT_EDIT_CAPTION = "PAYROLL_DEPARTMENT_EDIT_CAPTION";
                public const string PAYROLL_DEPARTMENT_VIEW_CAPTION = "PAYROLL_DEPARTMENT_VIEW_CAPTION";
            }

            public class PayrollWorkLocation
            {
                public const string PAYROLL_WORKLOCATION_NAME_EMPTY = "PAYROLL_WORKLOCATION_NAME_EMPTY";
                public const string PAYROLL_WORKLOCATION_EXISTS = "PAYROLL_WORKLOCATION_EXISTS";
                public const string PAYROLL_WORKLOCATION_ADD_CAPTION = "PAYROLL_WORKLOCATION_ADD_CAPTION";
                public const string PAYROLL_WORKLOCATION_EDIT_CAPTION = "PAYROLL_WORKLOCATION_EDIT_CAPTION";
                public const string PAYROLL_WORKLOCATION_VIEW_CAPTION = "PAYROLL_WORKLOCATION_VIEW_CAPTION";
            }

            public class StaffNameTitle
            {
                public const string PAYROLL_STAFF_NAME_TITLE_EMPTY = "PAYROLL_STAFF_NAME_TITLE_EMPTY";
                public const string PAYROLL_STAFF_NAME_TITLE_EXISTS = "PAYROLL_STAFF_NAME_TITLE_EXISTS";
                public const string PAYROLL_STAFF_NAME_TITLE_ADD_CAPTION = "PAYROLL_STAFF_NAME_TITLE_ADD_CAPTION";
                public const string PAYROLL_STAFF_NAME_TITLE_EDIT_CAPTION = "PAYROLL_STAFF_NAME_TITLE_EDIT_CAPTION";
                public const string PAYROLL_STAFF_NAME_TITLE_VIEW_CAPTION = "PAYROLL_STAFF_NAME_TITLE_VIEW_CAPTION";
            }

            public class CommonPayroll
            {
                public const string PAYROLL_COMMON_TITLE = "PAYROLL_COMMON_TITLE";
                public const string PAYROLL_COMMON_DELETE_GRID_EMTPY = "PAYROLL_COMMON_DELETE_GRID_EMTPY";
                public const string PAYROLL_COMMON_DELETED_CONFIRMATION = "PAYROLL_COMMON_DELETED_CONFIRMATION";
                public const string PAYROLL_COMMON_EDIT_GRID_EMTPY = "PAYROLL_COMMON_EDIT_GRID_EMTPY";
                public const string PAYROLL_COMMON_PRINT_INFO = "PAYROLL_COMMON_PRINT_INFO";
                public const string PAYROLL_COMMON_RESOURCE_FILE_NOT_AVAILABLE_INFO = "PAYROLL_COMMON_RESOURCE_FILE_NOT_AVAILABLE_INFO";
                public const string PAYROLL_COMMON_GRID_EMTPY = "PAYROLL_COMMON_GRID_EMTPY";
                public const string PAYROLL_COMMON_GRID_EMTPY_INFO = "PAYROLL_COMMON_GRID_EMTPY_INFO";
            }
            public class GroupAllocation
            {
                public const string GROUP_ALLOCATION_SUCCESS = "Staff are allotted for selected Groups.";
                public const string PROJECT_STAFF_ALLOCATION_SUCCESS = "Staff are mapped successfully.";
                public const string PROJECT_STAFF_UNMAP = "Staff are unmapped successfully.";
                public const string PROJECT_STAFF_ALLOCATION_FAILURE = "Staff are not mapped.";
                public const string GROUP_ALLOCATION_FAILURE = "Staff are not allotted for selected Groups.";
                public const string STAFF_ORDER_SUCCESS = "Staff order is set.";
                public const string STAFF_ORDER_FAILURE = "Staff order is not set.";
            }
            public class PostPayment
            {
                public const string POST_GROUP_EMPTY = "POST_GROUP_EMPTY"; //"Group is empty.";
                public const string POST_DATE_EMPTY = "POST_DATE_EMPTY"; //"Date is empty.";
                public const string POST_PROJECT_EMPTY = "POST_PROJECT_EMPTY"; //"Project is empty.";
                public const string POST_GRID_EMPTY = "POST_GRID_EMPTY"; //"No record is available in the grid to post.";
                public const string LEDGER_EMPTY = "LEDGER_EMPTY"; //"Ledger is empty.";
                public const string POST_AMOUNT_EMPTY = "POST_AMOUNT_EMPTY"; //"Amount is empty.";
                public const string COMPONENT_AMOUNT_EMPTY = "COMPONENT_AMOUNT_EMPTY";//"Amount is empty, select the component to post the amount.";
                public const string AMOUNT_MISMATCH = "AMOUNT_MISMATCH"; //"Allocated amount cannot greater/less than actual payment amount";
                public const string POST_CASHBANK_EMPTY = "POST_CASHBANK_EMPTY";//"Cash/Bank ledger is empty.";
                public const string POST_AMOUNT_EXCEEDS_INFO = "POST_AMOUNT_EXCEEDS_INFO"; //"Group is empty.";
                public const string POST_ADD_CAPTION = "POST_ADD_CAPTION"; //"Group is empty.";
                public const string POST_EDIT_CAPTION = "POST_EDIT_CAPTION"; //"Group is empty.";
                public const string POST_VIEW_CAPTION = "POST_VIEW_CAPTION"; //"Group is empty.";
                public const string POST_PRINT_CAPTION = "POST_PRINT_CAPTION"; //"Group is empty.";
                public const string POST_PAYROLL_MONTH_FOR_INFO = "POST_PAYROLL_MONTH_FOR_INFO"; //"Group is empty.";
            }
        }

        #endregion

        #region Asset
        public class Asset
        {
            public class Service
            {
                public const string SERVICE_NAME_EMPTY = "SERVICE_NAME_EMPTY";
                public const string SERVICE_ADD_CAPTION = "SERVICE_ADD_CAPTION";
                public const string SERVICE_EDIT_CAPTION = "SERVICE_EDIT_CAPTION";
                public const string SERVICE_PRINT_CAPTION = "SERVICE_PRINT_CAPTION";

            }
            public class Depreciation
            {
                public const string DEPRECIATION_NAME_EMPTY = "DEPRECIATION_NAME_EMPTY";
                public const string DEPRECIATION_ADD_CAPTION = "DEPRECIATION_ADD_CAPTION";
                public const string DEPRECIATION_EDIT_CAPTION = "DEPRECIATION_EDIT_CAPTION";
                public const string DEPRECIATION_PRINT_CAPTION = "DEPRECIATION_PRINT_CAPTION";
                public const string DEPRECIATION_VIEW_SCREEN = "DEPRECIATION_VIEW_SCREEN";
                public const string DEP_APPLY_CURRENT_TRANS_PEROID = "DEP_APPLY_CURRENT_TRANS_PEROID";
                public const string DEPRECIATION_SUCCESS_INFO = "DEPRECIATION_SUCCESS_INFO";
                public const string DEP_VOUCHER_DATE_EMPTY = "DEP_VOUCHER_DATE_EMPTY";
                public const string DEP_VOUCHER_PEROID_FROM_EMPTY = "DEP_VOUCHER_PEROID_FROM_EMPTY";
                public const string DEP_VOUCHER_PEROID_TO_EMPTY = "DEP_VOUCHER_PEROID_TO_EMPTY";
                public const string DEP_VOUCHER_PEROID_FROM_TO_DATE = "DEP_VOUCHER_PEROID_FROM_TO_DATE";
                public const string DEP_DETAILS_EMPTY = "DEP_DETAILS_EMPTY";
                public const string DEP_FINANCE_LEDGER_EMPTY = "DEP_FINANCE_LEDGER_EMPTY";
                public const string DEP_AMOUNT_EMPTY = "DEP_AMOUNT_EMPTY";
                public const string DEP_LEDGER_DETAILS_EMPTY = "DEP_LEDGER_DETAILS_EMPTY";
            }
            public class Location
            {
                public const string LOCATION_NAME_EMPTY = "LOCATION_NAME_EMPTY";
                public const string LOCATION_ADD_CAPTION = "LOCATION_ADD_CAPTION";
                public const string LOCATION_EDIT_CAPTION = "LOCATION_EDIT_CAPTION";
                public const string LOCATION_TYPE_EMPTY = "LOCATION_TYPE_CAPTION";
                public const string LOCATION_PRINT_CAPTION = "LOCATION_PRINT_CAPTION";
                public const string LOCATION_VIEW_CAPTION = "LOCATION_VIEW_CAPTION";
                public const string LOCATION_AVAILABLE = "LOCATION_AVAILABLE";
                public const string LOCATION_NORECORD_MOVE_INFO = "LOCATION_NORECORD_MOVE_INFO";
                public const string LOCATION_MAP_SUCCESS_INFO = "LOCATION_MAP_SUCCESS_INFO";
                public const string LOCATION_MAP_TITLE = "LOCATION_MAP_TITLE";
                public const string TODATE_VALIDATION = "TODATE_VALIDATION";
            }

            public class Insurance
            {
                public const string INSURANCE_NAME_EMPTY = "INSURANCE_NAME_EMPTY";
                public const string INSURANCE_ADD_CAPTION = "INSURANCE_ADD_CAPTION";
                public const string INSURANCE_EDIT_CAPTION = "INSURANCE_EDIT_CAPTION";
                public const string INSURANCE_PRINT_CAPTION = "INSURANCE_PRINT_CAPTION";
                public const string INSURANCE_RENEWALADD_CAPTION = "INSURANCE_RENEWALADD_CAPTION";
                public const string INSURANCE_RENEWALEDIT_CAPTION = "INSURANCE_RENEWALEDIT_CAPTION";
                public const string INSURANCE_ENTRY = "INSURANCE_ENTRY";
                public const string INSURANCE_VIEW = "INSURANCE_VIEW";
                public const string INSURANCE_TYPE_EMPTY = "INSURANCE_TYPE_EMPTY";
                public const string PROVIDER_EMPTY = "PROVIDER_EMPTY";
                public const string AGENT_EMPTY = "AGENT_EMPTY";
                public const string VALIDATE_DUE_DATE = "VALIDATE_DUE_DATE";
                public const string POLICY_EMPTY = "POLICY_EMPTY";
                public const string POLICY_NO_EMPTY = "POLICY_NO_EMPTY";
                public const string PREMIUM_AMOUNT_EMPTY = "PREMIUM_AMOUNT_EMPTY";
                public const string START_DATE_EMPTY = "START_DATE_EMPTY";
                public const string DUE_DATE_EMPTY = "DUE_DATE_EMPTY";
                public const string INSURABLE_VALUE_EMPTY = "INSURABLE_VALUE_EMPTY";
                public const string ASSET_ID_EMPTY = "ASSET_ID_EMPTY";
                public const string ASSET_NAME = "ASSET_NAME";
                public const string RENEWAL_DATE_EMPTY = "RENEWAL_DATE_EMPTY";
                public const string RENEWAL_AMOUNT_EMPTY = "RENEWAL_AMOUNT_EMPTY";
                public const string INSURANCE_COMPANY_NAME_EMPTY = "INSURANCE_COMPANY_NAME_EMPTY";
                public const string INSURANCE_PRODUCT_NAME_EMPTY = "INSURANCE_PRODUCT_NAME_EMPTY";
                public const string INSURANCE_NORECORD_CREATE_INSURANCE = "INSURANCE_NORECORD_CREATE_INSURANCE";
                public const string INSURANCE_NORENEWAL_HISTORY_AVAIL_INFO = "INSURANCE_NORENEWAL_HISTORY_AVAIL_INFO";
                public const string INS_CANNOT_CREATE_SOLD_DISPOSE_DONATE_ITEM = "INS_CANNOT_CREATE_SOLD_DISPOSE_DONATE_ITEM";
                public const string INS_NORECORD_EDIT_INFO = "INS_NORECORD_EDIT_INFO";
                public const string INS_VIEW_CAPTION = "INS_VIEW_CAPTION";
            }
            public class AssetClass
            {
                public const string ASSETCLASS_NAME_EMPTY = "ASSETCLASS_NAME_EMPTY";
                public const string ASSETCLASS_PARENTCLASS_EMPTY = "ASSETCLASS_PARENTCLASS_EMPTY";
                public const string ASSETCLASS_METHOD_EMPTY = "ASSETCLASS_METHOD_EMPTY";
                public const string ASSETCLASS_DEPRECIATION_EMPTY = "ASSETCLASS_DEPRECIATION_EMPTY";
                public const string ASSETCLASS_ADD_CAPTION = "ASSETCLASS_ADD_CAPTION";
                public const string ASSETCLASS_EDIT_CAPTION = "ASSETCLASS_EDIT_CAPTION";
                public const string ASSETCLASS_PRINT_CAPTION = "ASSETCLASS_PRINT_CAPTION";
                public const string ASSETCLASS_VIEW_CAPTION = "ASSETCLASS_VIEW_CAPTION";
                public const string ASSETCLASS_CANNOT_DELETE = "ASSETCLASS_CANNOT_DELETE";
            }

            public class UnitOfMeassure
            {
                public const string UNITOFMEASSURE_TYPE_EMPTY = "UNITOFMEASSURE_TYPE_EMPTY";
                public const string SAME_UNIT_ERROR = "SAME_UNIT_ERROR";
                public const string SYMBOL_EMPTY = "SYMBOL_EMPTY";
                public const string DECIMAL_PLACE_EMPTY = "DECIMAL_PLACE_EMPTY";
                public const string FIRSTUNIT_EMPTY = "FIRSTUNIT_EMPTY";
                public const string CONVERSIONOF_EMPTY = "CONVERSIONOF_EMPTY";
                public const string SECONDUNIT_EMPTY = "SECONDUNIT_EMPTY";
                public const string TYPE_EMPTY = "TYPE_EMPTY";
                public const string UNIT_OF_MEASURE_ADD = "UNIT_OF_MEASURE_ADD";
                public const string UNIT_OF_MEASURE_EDIT = "UNIT_OF_MEASURE_EDIT";
                public const string UNITOFMEASSURE_PRINT_CAPTION = "UNITOFMEASSURE_PRINT_CAPTION";

            }
            public class AssetItem
            {
                public const string ASSETITEM_ADD_CAPTION = "ASSETITEM_ADD_CAPTION";
                public const string ASSETITEM_EDIT_CAPTION = "ASSETITEM_EDIT_CAPTION";
                public const string ASSETITEM_ASSETCLASS_EMPTY = "ASSETITEM_ASSETCLASS_EMPTY";
                public const string ASSETITEM_DEPRECIATIONLEDGER_EMPTY = "ASSETITEM_DEPRECIATIONLEDGER_EMPTY";
                public const string ASSETITEM_DISPOSALLEDGER_EMPTY = "ASSETITEM_DISPOSALLEDGER_EMPTY";
                public const string ASSETITEM_ACCOUNTLEDGER_EMPTY = "ASSETITEM_ACCOUNTLEDGER_EMPTY";
                public const string ASSETITEM_NAME_EMPTY = "ASSETITEM_NAME_EMPYTY";
                public const string ASSETITEM_UNIT_EMPTY = "ASSETITEM_UNIT_EMPTY";
                public const string ASSETITEM_METHOD_EMPTY = "ASSETITEM_METHOD_EMPTY";
                public const string ASSETITEM_STARTINNO_EMPTY = "ASSETITEM_STARTINNO_EMPTY";
                public const string ASSETITEM_WIDTH_EMPTY = "ASSETITEM_WIDTH_EMPTY";
                public const string ASSETITEM_PRINT_CAPTION = "ASSETITEM_PRINT_CAPTION";
                public const string ASSET_PRINT_CAPTION = "ASSET_PRINT_CAPTION";
                public const string ASSET_PREFIX_EMPTY = "ASSET_PREFIX_EMPTY";
                public const string ASSET_SUFFIX_EMPTY = "ASSET_SUFFIX_EMPTY";
                public const string RETENTION_YRS_EMPTY = "RETENTION_YRS_EMPTY";
                public const string ASSET_ACCOUNT_LEDGER = "ASSET_ACCOUNT_LEDGER";
                public const string ASSET_DEPRECIATION_LEDGER = "ASSET_DEPRECIATION_LEDGER";
                public const string ASSET_DISPOSAL_LEDGER = "ASSET_DISPOSAL_LEDGER";
                public const string ASSET_AMC_RENEWAL_ALERT = "ASSET_AMC_RENEWAL_ALERT";
                public const string ASSET_INSURANCE_ALERT = "ASSET_INSURANCE_ALERT";
                public const string DEPRECIATION_YRS_EMPTY = "DEPRECIATION_YRS_EMPTY";
                public const string ASSET_UPDATE_DETAILS = "ASSET_UPDATE_DETAILS";
                public const string ASSET_ITEM_VIEW = "ASSET_ITEM_VIEW";
                public const string ASSET_ITEM_CAPTION = "ASSET_ITEM_CAPTION";
                public const string ASSET_OPENNING_ITEM_DETAIL = "ASSET_OPENNING_ITEM_DETAIL";
                public const string ASSET_PUCHASE_ITEM_DETAIL = "ASSET_PUCHASE_ITEM_DETAIL";
                public const string ASSET_INKIND_ITEM_DETAIL = "ASSET_INKIND_ITEM_DETAIL";
                public const string ASSET_AMC_ITEM_DETAIL = "ASSET_AMC_ITEM_DETAIL";
                public const string ASSET_SALES_ITEM_DETAIL = "ASSET_SALES_ITEM_DETAIL";
                public const string ASSETID_EMPTY = "ASSETID_EMPTY";
                public const string ASSETID_DUPLICATE_DETAIL = "ASSETID_DUPLICATE_DETAIL";
                public const string ASSETID_AMOUNT_GREATERHTAN_ZERO = "ASSETID_AMOUNT_GREATERHTAN_ZERO";
                public const string ASSET_UPDATE_INS_COSTCENTER_DETAIL = "ASSET_UPDATE_INS_COSTCENTER_DETAIL";
                public const string ASSETLIST_ITEMS_CAPTION = "ASSETLIST_ITEMS_CAPTION";
                public const string ASSET_UPDATE_INS_DETAIL = "ASSET_UPDATE_INS_DETAIL";
                public const string ASSET_ASSETID_CAPTION = "ASSET_ASSETID_CAPTION";
                public const string ASSET_ALREADY_EXISTS_ITEM = "ASSET_ALREADY_EXISTS_ITEM";
                public const string ASSET_PREFIX_SUFFIX_INFO = "ASSET_PREFIX_SUFFIX_INFO";
                public const string ASSET_DEP_YEAR_LESSTHAN_RETN_YEAR_INFO = "ASSET_DEP_YEAR_LESSTHAN_RETN_YEAR_INFO";
                public const string ASSET_ITEM_LIST_TITLE = "ASSET_ITEM_LIST_TITLE";
            }

            public class AssetCategory
            {
                public const string ASSETCATEGORY_ADD_CAPTION = "ASSETCATEGORY_ADD_CAPTION";
                public const string ASSETCATEGORY_NAME_EMPTY = "ASSETCATEGORY_NAME_EMPTY";
                public const string ASSETCATEGORY_EDIT_CAPTION = "ASSETCATEGORY_EDIT_CAPTION";
                public const string ASSETCATEGORY_PARENTGROUP_EMPTY = "ASSETCATEGORY_PARENTGROUP_EMPTY";
                public const string ASSETCATEGORY_PRINT_CAPTION = "ASSETCATEGORY_PRINT_CAPTION";
                public const string ASSETCATEGORY_VIEW_CAPTION = "ASSETCATEGORY_VIEW_CAPTION";
                public const string CATEGORY_CAN_DELETE = "CATEGORY_CAN_DELETE";
            }

            public class FixeAssetRegister
            {
                public const string FIXED_ASSET_REG_DATE_AS_ON_EMPTY = "FIXED_ASSET_REG_DATE_AS_ON_EMPTY";
                public const string FIXED_ASSET_REG_PROJECT_SELECT_INFO = "FIXED_ASSET_REG_PROJECT_SELECT_INFO";
                public const string FIXED_ASSET_REG_TITLE = "FIXED_ASSET_REG_TITLE";
                public const string UPDATE_ASSET_DETAIL_TITLE = "UPDATE_ASSET_DETAIL_TITLE";
            }
            public class FixedAssetTransfer
            {
                public const string FIXED_ASSET_TRANS_REGNO_EMTPY = "FIXED_ASSET_TRANS_REGNO_EMTPY";
                public const string FIXED_ASSET_TITLE = "FIXED_ASSET_TITLE";
            }

            public class AssetItemLedgerMapping
            {
                public const string ASSET_LEDGER_MAPPING_SUCCESS = "ASSET_LEDGER_MAPPING_SUCCESS";
                public const string ASSET_LEDGER_MAPPING_TITLE = "ASSET_LEDGER_MAPPING_TITLE";
                public const string ASSET_SETTING_SUCCESS_INFO = "ASSET_SETTING_SUCCESS_INFO";
            }

            public class AssetImport
            {
                public const string ASSET_IMPORT_FILE_NOT_EXITS_INFO = "ASSET_IMPORT_FILE_NOT_EXITS_INFO";
                public const string ASSET_IMPORT_ASSET_DETAILS_EMPTY = "ASSET_IMPORT_ASSET_DETAILS_EMPTY";
                public const string ASSET_IMPORT_SUCCESS_INFO = "ASSET_IMPORT_SUCCESS_INFO";
                public const string ASSET_IMPORT_EXCEL_FILE_ALREADY_OPENED = "ASSET_IMPORT_EXCEL_FILE_ALREADY_OPENED";
                public const string ASSET_IMPORT_PROBLEM_WHILE_IMPORT_INFO = "ASSET_IMPORT_PROBLEM_WHILE_IMPORT_INFO";
                public const string ASSET_IMPORT_EXCEL_FILE_OPEN_CLOSE_INFO = "ASSET_IMPORT_EXCEL_FILE_OPEN_CLOSE_INFO";
                public const string ASSET_IMPORT_MADATORY_FIELD_DOESNOT_EXITS = "ASSET_IMPORT_MADATORY_FIELD_DOESNOT_EXITS";
            }

            public class Block
            {
                public const string ASSETBLOCK_ADD_CAPTION = "ASSETBLOCK_ADD_CAPTION";
                public const string ASSETBLOCK_NAME_EMPTY = "ASSETBLOCK_NAME_EMPTY";
                public const string ASSETBLOCK_EDIT_CAPTION = "ASSETBLOCK_EDIT_CAPTION";
                public const string ASSETBLOCK_PRINT_CAPTION = "ASSETBLOCK_PRINT_CAPTION";
                public const string ASSETBLOCK_VIEW_CAPTION = "ASSETBLOCK_VIEW_CAPTION";
            }
            public class SubClass
            {
                public const string ASSETSUBCLASS_ADD_CAPTION = "ASSETSUBCLASS_ADD_CAPTION";
                public const string ASSETSUBCLASS_NAME_EMPTY = "ASSETSUBCLASS_NAME_EMPTY";
                public const string ASSETSUBCLASS_EDIT_CAPTION = "ASSETSUBCLASS_EDIT_CAPTION";
                public const string ASSETSUBCLASS_PRINT_CAPTION = "ASSETSUBCLASS_PRINT_CAPTION";
                public const string ASSETSUBCLASS_VIEW_CAPTION = "ASSETSUBCLASS_VIEW_CAPTION";
            }

            public class AssetCustodians
            {
                public const string ASSETCUSTODIANS_ADD_CAPTION = "ASSETCUSTODIANS_ADD_CAPTION";
                public const string ASSETCUSTODIANS_EDIT_CAPTION = "ASSETCUSTODIANS_EDIT_CAPTION";
                public const string ASSETCUSTODIANS_NAME_EMPTY = "ASSETCUSTODIANS_NAME_EMPTY";
                public const string ASSETCUSTODIANS_TYPE_EMPTY = "ASSETCUSTODIANS_TYPE_EMPTY";
                public const string ASSETCUSTODIANS_STARTDATE_EMPTY = "ASSETCUSTODIANS_STARTDATE_EMPTY";
                public const string ASSETCUSTODIANS_ENDDATE_EMPTY = "ASSETCUSTODIANS_ENDDATE_EMPTY";
                public const string ASSETCUSTODIANS_ROLE_EMPTY = "ASSETCUSTODIANS_ROLE_EMPTY";
                public const string ASSETCUSTODIANS_PRINT_CAPTION = "ASSETCUSTODIANS_PRINT_CAPTION";
            }

            public class AssetOutward
            {
                public const string ASSET_OUTWARD_BILL_INVOCENO_EMPTY = "ASSET_OUTWARD_BILL_INVOCENO_EMPTY";
                public const string ASSET_OUTWARD_TO_EMPTY = "ASSET_OUTWARD_TO_EMPTY";
                public const string ASSET_QYT_MISMATCH_INFO = "ASSET_QYT_MISMATCH_INFO";
                public const string ASSET_LEDGER_EMPTY = "ASSET_LEDGER_EMPTY";
                public const string ASSET_AMOUNT_EMPTY = "ASSET_AMOUNT_EMPTY";
                public const string ASSET_CASH_BANK_DETAIL_EMTPY = "ASSET_CASH_BANK_DETAIL_EMTPY";
            }

            public class AssetOpenningBalance
            {
                public const string ASSET_OPBAL_ASON_INFO = "ASSET_OPBAL_ASON_INFO";
                public const string ASSET_LEDGER_OPENNING_BAL_INFO = "ASSET_LEDGER_OPENNING_BAL_INFO";
                public const string ASSET_AMOUNT_MISMATCH_INFO = "ASSET_AMOUNT_MISMATCH_INFO";
                public const string ASSET_INS_MADE_ENTRY_DELETE_CONFIRMATION = "ASSET_INS_MADE_ENTRY_DELETE_CONFIRMATION";
                public const string ASSET_SOLD_ITEM_CANNOT_DELETE_INFO = "ASSET_SOLD_ITEM_CANNOT_DELETE_INFO";
                public const string ASSET_OPENNING_BALANCE_TITLE = "ASSET_OPENNING_BALANCE_TITLE";
            }

            public class VendorInfo
            {
                public const string VENDOR_ADD_CAPTION = "VENDOR_ADD_CAPTION";
                public const string VENDOR_VIEW_CAPTION = "VENDOR_VIEW_CAPTION";
                public const string VENDOR_EDIT_CAPTION = "VENDOR_EDIT_CAPTION";
                public const string VENDOR_NAME_EMPTY = "VENDOR_NAME_EMPTY";
                public const string VENDOR_PRINT_CAPTION = "VENDOR_PRINT_CAPTION";
                public const string MANUFACTURER_PRINT_CAPTION = "MANUFACTURER_PRINT_CAPTION";
            }

            public class Manufacture
            {
                public const string MANUFACTURE_ADD_CAPTION = "MANUFACTURE_ADD_CAPTION";
                public const string MANUFACTURE_EDIT_CAPTION = "MANUFACTURE_EDIT_CAPTION";
                public const string MANUFACTURE_VIEW_CAPTION = "MANUFACTURE_VIEW_CAPTION";
            }

            public class TransferVoucher
            {
                public const string TRANSFER_ADD_CAPTION = "TRANSFER_ADD_CAPTION";
                public const string TRANSFER_EDIT_CAPTION = "TRANSFER_EDIT_CAPTION";
                public const string TRANSFER_FROM_TO_SAME = "TRANSFER_FROM_TO_SAME";
                public const string TRANSFER_AMOUNT_EMPTY = "TRANSFER_AMOUNT_EMPTY";
                public const string TRANSFER_TOLOCATION_EMPTY = "TRANSFER_TOLOCATION_EMPTY";
                public const string TRANSFER_ASSETITEM_EMPTY = "TRANSFER_ASSETITEM_EMPTY";
                public const string TRANSFER_FROMLOCATION_EMPTY = "TRANSFER_FROMLOCATION_EMPTY";
                public const string TRANSFER_DATE_EMPTY = "TRANSFER_DATE_EMPTY";
                public const string TRANSFER_PRINT_CAPTION = "TRANSFER_PRINT_CAPTION";
                public const string TRANSFER_FROMLOCATION_GRID_EMPTY = "TRANSFER_FROMLOCATION_GRID_EMPTY";
                public const string TRANSFER_TOLOCATION_GRID_EMPTY = "TRANSFER_TOLOCATION_GRID_EMPTY";
                public const string TRANSFER_REFRENCENO_EMPTY = "TRANSFER_REFRENCENO_EMPTY";
                public const string TRANSFER_DATE_FUTURE = "TRANSFER_DATE_FUTURE";
                public const string NO_ASSET_SELECTED = "NO_ASSET_SELECTED";
            }

            public class InwardVoucher
            {
                public const string PURCHASE_ADD_CAPTION = "PURCHASE_ADD_CAPTION";
                public const string PURCHASE_EDIT_CAPTION = "PURCHASE_EDIT_CAPTION";
                public const string RECEIVE_ADD_CAPTION = "RECEIVE_ADD_CAPTION";
                public const string RECEIVE_EDIT_CAPTION = "RECEIVE_EDIT_CAPTION";
                public const string PURCHASE_DATE_EMPTY = "PURCHASE_DATE_EMPTY";
                public const string PURCHASE_VENDOR_EMPTY = "PURCHASE_VENDOR_EMPTY";
                public const string PURCHASE_LEDGER_EMPTY = "PURCHASE_LEDGER_EMPTY";
                public const string PURCHASE_NETAMOUNT_EMPTY = "PURCHASE_NETAMOUNT_EMPTY";
                public const string PURCHASE_DISCOUNT_VALIDATION = "PURCHASE_DISCOUNT_VALIDATION";
                public const string PURCHASE_ASSETLOCATION_VALIDATION = "PURCHASE_ASSETLOCATION_VALIDATION";
                public const string PURCHASE_ASSET_CASH_BANK_LEDGER_EMPTY = "PURCHASE_ASSET_CASH_BANK_LEDGER_EMPTY";
                public const string PURCHASE_ENTRY = "PURCHASE_ENTRY";
                public const string PURCHASE_VIEW = "PURCHASE_VIEW";
                public const string PURCHASE_PRINT_CAPTION = "PURCHASE_PRINT_CAPTION";
                public const string INWARD_BILL_NO_EMPTY = "INWARD_BILL_NO_EMPTY";
                public const string PURCHASE = "PURCHASE";
                public const string INKIND = "INKIND";
                public const string PURCHASE_DETAILS = "PURCHASE_DETAILS";
                public const string INKIND_DETAILS = "INKIND_DETAILS";
                public const string RECEIVE_VIEW = "RECEIVE_VIEW";
                public const string SALAVAGE_VALUE_EMPTY = "SALAVAGE_VALUE_EMPTY";
                public const string INWARD_SERVICE_PROVIDER_INFO = "INWARD_SERVICE_PROVIDER_INFO";
                public const string INWARD_ASSET_ITEM_MISMATCH_INFO = "INWARD_ASSET_ITEM_MISMATCH_INFO";
                public const string INWARD_AMOUNT_MISMATCH_INFO = "INWARD_AMOUNT_MISMATCH_INFO";
                public const string INSURANCE_MADE_ENTRY_DELETE_CONFIRMATION_INFO = "INSURANCE_MADE_ENTRY_DELETE_CONFIRMATION_INFO";
                public const string SOLD_ITEM_CANNOT_CANNOT_DELETE_INFO = "SOLD_ITEM_CANNOT_CANNOT_DELETE_INFO";

            }

            //public class ReceiveVoucher
            //{
            //    public const string RECEIVE_ADD_CAPTION = "RECEIVE_ADD_CAPTION";
            //    public const string RECEIVE_EDIT_CAPTION = "RECEIVE_EDIT_CAPTION";
            //    public const string RECEIVE_DATE_EMPTY = "RECEIVE_DATE_EMPTY";
            //    public const string RECEIVE_VENDOR_EMPTY = "RECEIVE_VENDOR_EMPTY";
            //    public const string RECEIVE_LEDGER_EMPTY = "RECEIVE_LEDGER_EMPTY";
            //    public const string RECEIVE_NETAMOUNT_EMPTY = "RECEIVE_NETAMOUNT_EMPTY";
            //    public const string RECEIVE_DISCOUNT_VALIDATION = "RECEIVE_DISCOUNT_VALIDATION";
            //    public const string RECEIVE_ASSETLOCATION_VALIDATION = "RECEIVE_ASSETLOCATION_VALIDATION";
            //    public const string RECEIVE_GRID_EMPTY = "RECEIVE_GRID_EMPTY";
            //    public const string RECEIVE_PRINT_CAPTION = "RECEIVE_PRINT_CAPTION";
            //    public const string RECEIVE_ENTRY = "RECEIVE_ENTRY";
            //    public const string RECEIVE_VIEW = "RECEIVE_VIEW";
            //    public const string RECEIVE_MANUFACTURE_EMPTY = "RECEIVE_MANUFACTURE_EMPTY";
            //}
            public class DepreciationVoucher
            {
                public const string DEPRECIATION_VOUCHER_ADD_CAPTION = "DEPRECIATION_VOUCHER_ADD_CAPTION";
                public const string DEPRECIATION_VOUCHER_EDIT_CAPTION = "DEPRECIATION_VOUCHER_EDIT_CAPTION";
                public const string DEPRECIATON_LOCATION_EMPTY = "DEPRECIATON_LOCATION_EMPTY";
                public const string DEPRECIATION_GROUP_EMPTY = "DEPRECIATION_GROUP_EMPTY";
                public const string DEPRECIATION_TODATE_EMPTY = "DEPRECIATION_TODATE_EMPTY";
                public const string DEPRECIATION_ENTRY = "DEPRECIATION_ENTRY";
                public const string DEPRECIATION_VIEW = "DEPRECIATION_VIEW";
                public const string DEPRECIATION_EMPTY = "DEPRECIATION_EMPTY";
                public const string DEPRECIATIONVOUCHER_PRINT_CAPTION = "DEPRECIATIONVOUCHER_PRINT_CAPTION";
            }
            public class AMCVoucher
            {
                public const string AMC_PRINT_CAPTION = "AMC_PRINT_CAPTION";
                public const string AMCVOUCHER_ADD_CAPTION = "AMCVOUCHER_ADD_CAPTION";
                public const string AMCVOUCHER_EDIT_CAPTION = "AMCVOUCHER_EDIT_CAPTION";
                public const string AMCVOUCHER_START_DATE_EMPTY = "AMCVOUCHER_START_DATE_EMPTY";
                public const string AMCVOUCHER_DUE_DATE_EMPTY = "AMCVOUCHER_DUE_DATE_EMPTY";
                public const string AMCVOUCHER_AMOUNT_EMPTY = "AMCVOUCHER_AMOUNT_EMPTY";
                public const string AMCVOUCHER_ENTRY = "AMCVOUCHER_ENTRY";
                public const string AMCVOUCHER_VIEW = "AMCVOUCHER_VIEW";
                public const string AMC_EXPENSE_LEDGER_EMPTY = "AMC_EXPENSE_LEDGER_EMPTY";
                public const string AMCVOUCHER_PRINT_CAPTION = "AMCVOUCHER_PRINT_CAPTION";
                public const string DUE_DATE_GREATER_THAN_START_DATE = "DUE_DATE_GREATER_THAN_START_DATE";
                public const string ASSET_NO_ITEM_DETAIL = "ASSET_NO_ITEM_DETAIL";
                public const string ASSET_AMC_RENEWAL_EMPTY = "ASSET_AMC_RENEWAL_EMPTY";
                public const string AMCGROUP_IS_EMPTY = "AMCGROUP_IS_EMPTY";
                public const string AMCPROVIDER_IS_EMPYT = "AMCPROVIDER_IS_EMPYT";
                public const string AMCPREMIUM_AMOUNT = "AMCPREMIUM_AMOUNT";
                public const string AMCDATE_VALIDATION = "AMCDATE_VALIDATION";
                public const string AMC_RENEWAL_DATE_VALIDATION = "AMC_RENEWAL_DATE_VALIDATION";
                public const string AMC_RENEWAL_DELETE_CONFIRMATION = "AMC_RENEWAL_DELETE_CONFIRMATION";
                public const string AMC_FROM_EMPTY = "AMC_FROM_EMPTY";
                public const string AMC_TO_EMPTY = "AMC_TO_EMPTY";
                public const string RENEWAL_DATE_EMPTY = "RENEWAL_DATE_EMPTY";
                public const string AMC_NO_MAPPED_ITEMS = "AMC_NO_MAPPED_ITEMS";
                public const string AMC_NORECORD_SELECT_MOVE = "AMC_NORECORD_SELECT_MOVE";
                public const string AMC_SALES_DONATE_DISPOSE_SOLD_INFO = "AMC_SALES_DONATE_DISPOSE_SOLD_INFO";
                public const string AMC_RENEW_MODE_CAPTION = "AMC_RENEW_MODE_CAPTION";
                public const string AMC_RENEWAL_EDIT_MODE_CAPTION = "AMC_RENEWAL_EDIT_MODE_CAPTION";
                public const string AMC_DATEOF_AMC_CAPTION = "AMC_DATEOF_AMC_CAPTION";
                public const string AMC_VIEW_CAPTION = "AMC_VIEW_CAPTION";
            }

            public class SalesVoucher
            {
                public const string SALES_ADD_CAPTION = "SALES_ADD_CAPTION";
                public const string SALES_EDIT_CAPTION = "SALES_EDIT_CAPTION";
                public const string SALES_ASSET_NAME_EMPTY = "SALES_ASSET_NAME_EMPTY";
                public const string SALES_ASSET_ID_EMPTY = "SALES_ASSET_ID_EMPTY";
                public const string SALES_QUANTITY_EMPTY = "SALES_QUANTITY_EMPTY";
                public const string SALES_RATE_EMPTY = "SALES_RATE_EMPTY";
                public const string SALES_ENTRY = "SALES_ENTRY";
                public const string SALES_VIEW = "SALES_VIEW";
                public const string SALES_PRINT_CAPTION = "SALES_PRINT_CAPTION";
                public const string FIXED_ASSET_REGISTER_PRINT_CAPTION = "FIXED_ASSET_REGISTER_PRINT_CAPTION";
                public const string DISPOSAL_ENTRY = "DISPOSAL_ENTRY";
                public const string DISPOSAL_VIEW = "DISPOSAL_VIEW";
                public const string DISPOSAL_ADD_CAPTION = "DISPOSAL_ADD_CAPTION";
                public const string DISPOSAL_EDIT_CAPTION = "DISPOSAL_EDIT_CAPTION";
                public const string GRID_EMPTY = "GRID_EMPTY";
                public const string DISPOSAL_PRINT_CAPTION = "DISPOSAL_PRINT_CAPTION";
                public const string QUANTITY_EXCEEDS = "QUANTITY_EXCEEDS";
                public const string SALES = "SALES";
                public const string DISPOSE = "DISPOSE";
                public const string DONATE = "DONATE";
                public const string DONATE_ADD_CAPTION = "DONATE_ADD_CAPTION";
                public const string DONATE_EDIT_CAPTION = "DONATE_EDIT_CAPTION";
                public const string SALES_DETAILS = "SALES_DETAILS";
                public const string DISPOSAL_DETAILS = "DISPOSAL_DETAILS";
                public const string DONATE_DETAILS = "DONATE_DETAILS";
                public const string UPDATE_SALES_DISPOSE_INFO = "UPDATE_SALES_DISPOSE_INFO";
                public const string UPDATE_SALES_DISPOSE_VIEW_CAPTION = "UPDATE_SALES_DISPOSE_VIEW_CAPTION";
            }
            public class InsuranceRenew
            {
                public const string RENEW_ADD_CAPTION = "RENEW_ADD_CAPTION";
                public const string RENEW_EDIT_CAPTION = "RENEW_EDIT_CAPTION";
                public const string INSURANCE_PLAN_EMPTY = "INSURANCE_PLAN_EMPTY";
                public const string INSURANCE_POLICY_EMPTY = "INSURANCE_POLICY_EMPTY";
                public const string INSURANCE_PERION_FROM_EMPTY = "INSURANCE_PERION_FROM_EMPTY";
                public const string INSURANCE_PERION_TO_EMPTY = "INSURANCE_PERION_TO_EMPTY";
                public const string INSURANCE_PERIOD_FROM_AND_TO_VALIDATION = "INSURANCE_PERIOD_FROM_AND_TO_VALIDATION";
                public const string INSURANCE_SUM_INSURCED_EMPTY = "INSURANCE_SUM_INSURCED_EMPTY";
                public const string INSURANCE_PRIMIUM_AMOUNT_EMPTY = "INSURANCE_PRIMIUM_AMOUNT_EMPTY";
                public const string INSURANCE_RENEW_PRINT_CAPTION = "INSURANCE_RENEW_PRINT_CAPTION";
                public const string INSURANCE_CREATE_RENEW_CONFIRMATION = "INSURANCE_CREATE_RENEW_CONFIRMATION";
                public const string INSURANCE_PROJECT_INFO = "INSURANCE_PROJECT_INFO";
                public const string INSURANCE_VOUCHERNO_INFO = "INSURANCE_VOUCHERNO_INFO";
                public const string INSURANCE_OK_CAPTION = "INSURANCE_OK_CAPTION";
                public const string INSURANCE_CANCEL_CAPTION = "INSURANCE_CANCEL_CAPTION";
                public const string INSURANCE_REGISTRATION_DATE = "INSURANCE_REGISTRATION_DATE";
                public const string INSURANCE_VOUCHER_ADD_CAPTION = "INSURANCE_VOUCHER_ADD_CAPTION";
                public const string INSURANCE_VOUCHER_EDIT_CAPTION = "INSURANCE_VOUCHER_EDIT_CAPTION";
            }
        }
        #endregion

        #region Stock
        public class Stock
        {
            public class StockItem
            {
                public const string STOCK_ADD_CAPTION = "STOCK_ADD_CAPTION";
                public const string STOCK_EDIT_CAPTION = "STOCK_EDIT_CAPTION";
                public const string STOCK_CATEGORY_EMPTY = "STOCK_CATEGORY_EMPTY";
                public const string STOCK_GROUP_EMPTY = "STOCK_GROUP_EMPTY";
                public const string STOCK_UNIT_EMPTY = "STOCK_UNIT_EMPTY";
                public const string STOCK_NAME_EMPTY = "STOCK_NAME_EMPTY";
                public const string STOCK_PRINT_CAPTION = "STOCK_PRINT_CAPTION";
                public const string STOCK_LOCATION_EMPTY = "STOCK_LOCATION_EMPTY";
                public const string STOCK_QUANTITY_EMPTY = "STOCK_QUANTITY_EMPTY";
                public const string STOCK_RATE_PER_ITEM = "STOCK_RATE_PER_ITEM";
                public const string INCOME_LEDGER_EMPTY = "INCOME_LEDGER_EMPTY";
                public const string EXPENSE_LEDGER_EMPTY = "EXPENSE_LEDGER_EMPTY";
                public const string STOCK_REGISTER = "STOCK_REGISTER";
                public const string MAPPED_SUCESSFULLY = "MAPPED_SUCESSFULLY";
            }

            public class StockCategory
            {
                public const string STOCKCATEGORY_VIEW_CAPTION = "STOCKCATEGORY_VIEW_CAPTION";
            }

            public class StockGroup
            {
                public const string STOCKGROUP_VIEW_CAPTION = "STOCKGROUP_VIEW_CAPTION";
            }
            public class StockPurcahseReturns
            {
                public const string STOCK_ITEM_EMPTY = "STOCK_ITEM_EMPTY";
                public const string STOCK_LOCATION_EMPTY = "STOCK_LOCATION_EMPTY";
                public const string STOCK_VENDOR_EMPTY = "STOCK_VENDOR_EMPTY";
                public const string STOCK_QUANTITY_EMPTY = "STOCK_QUANTITY_EMPTY";
                public const string STOCK_UNIT_PRICE_EMPTY = "STOCK_UNIT_PRICE_EMPTY";
                public const string STOCK_AMOUNT_EMPTY = "STOCK_AMOUNT_EMPTY";
                public const string STOCK_LEDGER_EMPTY = "STOCK_LEDGER_EMPTY";
                public const string STOCK_RETURN_TYPE_EMPTY = "STOCK_RETURN_TYPE_EMPTY";
                public const string STOCK_TOTAL_AMOUTN_EMPTY = "STOCK_TOTAL_AMOUTN_EMPTY";
                public const string STOCK_QUANTITY_EXCEEDS = "STOCK_QUANTITY_EXCEEDS";
                public const string STOCK_PURCHASE_RETURN_ADD = "STOCK_PURCHASE_RETURN_ADD";
                public const string STOCK_PURCHASE_RETURN_EDIT = "STOCK_PURCHASE_RETURN_EDIT";
                public const string STOCK_PURCHASE_QUANTITY_ZERO = "STOCK_PURCHASE_QUANTITY_ZERO";
                public const string STOCK_NO_ITEM_ROWS = "STOCK_NO_ITEM_ROWS";
                public const string STOCK_RETURN_PRINT_CAPTION = "STOCK_RETURN_PRINT_CAPTION";
            }

            public class StokItemTransfer
            {
                public const string STOCK_TRANSFER_ADD_CAPTION = "STOCK_TRANSFER_ADD_CAPTION";
                public const string STOCK_TRANSFER_EDIT_CAPTION = "STOCK_TRANSFER_EDIT_CAPTION";
                public const string STOCK_INVALID_GRID_ENTRY = "STOCK_INVALID_GRID_ENTRY";
                public const string STOCK_TRANSFER_QTY_ZERO = "STOCK_TRANSFER_QTY_ZERO";
                public const string STOCK_TRANSFER_FROM_TO_LOCATION_SAME = "STOCK_TRANSFER_FROM_TO_LOCATION_SAME";
                public const string STOCK_TRANSFER_DATE_EMPTY = "STOCK_TRANSFER_DATE_EMPTY";
                public const string STOCK_QUANTITY_EXCEEDS = "STOCK_QUANTITY_EXCEEDS";
                public const string TRANSFER_PRINT_CAPTION = "TRANSFER_PRINT_CAPTION";
            }

            public class StockMasterPurchase
            {
                public const string PURCHASE_DATE_EMTPTY = "PURCHASE_DATE_EMTPTY";
                public const string BILL_NO_EMPTY = "BILL_NO_EMPTY";
                public const string TAX_AMOUNT_EXCEEDS = "TAX_AMOUNT_EXCEEDS";
                public const string DISCOUNT_AMOUNT_EXCEEDS = "DISCOUNT_AMOUNT_EXCEEDS";
                public const string VALIDATE_ACCOUNTING_YEAR = "VALIDATE_ACCOUNTING_YEAR";
                public const string PURCHASE_ADD_CAPTION = "PURCHASE_ADD_CAPTION";
                public const string PURCHASE_EDIT = "PURCHASE_EDIT";
                public const string RECEIVES_ADD_CAPTION = "RECEIVES_ADD_CAPTION";
                public const string RECEIVES_EDIT_CAPTION = "RECEIVES_EDIT_CAPTION";
                public const string PRINT_CAPTION = "PRINT_CAPTION";
                public const string PURCHASE_CAPTION = "PURCHASE_CAPTION";
                public const string PURCHASE_VIEW_CAPTION = "PURCHASE_VIEW_CAPTION";
                public const string RECEIVE_VIEW_CAPTION = "RECEIVE_VIEW_CAPTION";
                public const string RECEIVE_CAPTION = "RECEIVE_CAPTION";
                public const string CHANGE_PROJECT_START_DATE_CLOSED_DATE = "CHANGE_PROJECT_START_DATE_CLOSED_DATE";
                public const string PROJECT_START_DATE_CLOSED_DATE_FALL_BETWEEN_TRANS_PEROID = "PROJECT_START_DATE_CLOSED_DATE_FALL_BETWEEN_TRANS_PEROID";
            }

            public class StockSales
            {
                public const string SALES_CAPTION = "SALES_CAPTION";
                public const string DISPOSAL_CAPTION = "DISPOSAL_CAPTION";
                public const string UTILIZE_CAPTION = "UTILIZE_CAPTION";
                public const string PURCHASE_DATE_EMTPTY = "PURCHASE_DATE_EMTPTY";
                public const string SALES_GRID_EMPTY = "SALES_GRID_EMPTY";
                public const string TAX_AMOUNT_EMPTY = "TAX_AMOUNT_EMPTY";
                public const string RECIPIENT_NAME_EMPTY = "RECIPIENT_NAME_EMPTY";
            }
        }
        #endregion

        #region PortalMessage
        public class PortalMessage
        {
            public class PortalDataSynMessage
            {
                public const string PORTAL_DATASYN_MESSAGE_SAVED = "PORTAL_DATASYN_MESSAGE_SAVED";
                public const string PORTAL_SERVICE_NOT_AVIALABLE = "PORTAL_SERVICE_NOT_AVIALABLE";
                public const string PORTAL_NO_RECORD = "PORTAL_NO_RECORD";

            }
        }
        #endregion

        #region NetworkingConfiguration
        public class NetworkingSettings
        {
            public const string NETWORKING_SETTINGS_SERVER_EMPTY = "NETWORKING_SETTINGS_SERVER_EMPTY";
            public const string NETWORKING_SETTINGS_PORT_EMPTY = "NETWORKING_SETTINGS_PORT_EMPTY";
            public const string NETWORKING_SETTINGS_USERNAME_EMPTY = "NETWORKING_SETTINGS_USERNAME_EMPTY";
            public const string NETWORKING_SETTINGS_PASSWORD_EMPTY = "NETWORKING_SETTINGS_PASSWORD_EMPTY";
            public const string NETWORKING_SETTINGS_SENDERID_EMPTY = "NETWORKING_SETTINGS_SENDERID_EMPTY";
            public const string NETWORKING_SETTINGS_PASSKEY_EMPTY = "NETWORKING_SETTINGS_PASSKEY_EMPTY";
            public const string INSTITUTIONAL_TYPE_EMPTY = "INSTITUTIONAL_TYPE_EMPTY";
            public const string INSTITUTIONAL_TYPE_ADD = "INSTITUTIONAL_TYPE_ADD";
            public const string INSTITUTIONAL_TYPE_EDIT = "INSTITUTIONAL_TYPE_EDIT";
            public const string NETWORK_INS_TYPE_CAPTION = "NETWORK_INS_TYPE_CAPTION";
        }
        #endregion

        #region Networking Messages
        public class Networking
        {
            public class Donoranniversaries
            {
                public const string DONOR_ANNIVERSARIES_PRINT_CAPTION = "DONOR_ANNIVERSARIES_PRINT_CAPTION";
                public const string DONOR_ANNIVERSARIES_MODE_MAIL_CAPTION = "DONOR_ANNIVERSARIES_MODE_MAIL_CAPTION";
                public const string DONOR_ANNIVERSARIES_MODE_SMS_CAPTION = "DONOR_ANNIVERSARIES_MODE_SMS_CAPTION";
                public const string DONOR_ANNIVERSARIES_EMAIL_CAPTION = "DONOR_ANNIVERSARIES_EMAIL_CAPTION";
                public const string DONOR_ANNIVERSARIES_GENERATE_PREVIEW_CAPTION = "DONOR_ANNIVERSARIES_GENERATE_PREVIEW_CAPTION";
                public const string DONOR_ANNIVERSARIES_RECORDS_GENERATE_EMPTY = "DONOR_ANNIVERSARIES_RECORDS_GENERATE_EMPTY";
            }

            public class DonorFeastTask
            {
                public const string DONOR_ANNIVERSARIES_FEAST_TASK_SAVED_INFORMATION = "DONOR_ANNIVERSARIES_FEAST_TASK_SAVED_INFORMATION";
                public const string DONOR_ANNIVERSARIES_FEAST_TASK_EMPTY = "DONOR_ANNIVERSARIES_FEAST_TASK_EMPTY";
                public const string DONOR_ANNIVERSARIES_FEAST_TASK_TEMPLATE_EMPTY = "DONOR_ANNIVERSARIES_FEAST_TASK_TEMPLATE_EMPTY";
                public const string DONOR_ANNIVERSARIES_FEAST_TASK_ASSIGNED_TASK_INFORMATION = "DONOR_ANNIVERSARIES_FEAST_TASK_ASSIGNED_TASK_INFORMATION";
            }
            public class DonorMailFeast
            {
                public const string DONOR_MAIL_FEAST_TASKMAIL_INFORMATION = "DONOR_MAIL_FEAST_TASKMAIL_INFORMATION";
                public const string DONOR_MAIL_FEAST_PREVIEW = "DONOR_MAIL_FEAST_PREVIEW";
                public const string DONOR_MAIL_FEAST_TASKSMS_INFORMATION = "DONOR_MAIL_FEAST_TASKSMS_INFORMATION";
                public const string DONOR_MAIL_FEAST_TASK_WARNING_INFORMATION = "DONOR_MAIL_FEAST_TASK_WARNING_INFORMATION";
                public const string DONOR_MAIL_FEAST_RECORD_EMPTY = "DONOR_MAIL_FEAST_RECORD_EMPTY";
                public const string DONOR_MAIL_FEAST_TEMPLATE_EMPTY = "DONOR_MAIL_FEAST_TEMPLATE_EMPTY";
                public const string DONOR_MAIL_MERGE_INTERNET_CONNECTION_INFORMATION = "DONOR_MAIL_MERGE_INTERNET_CONNECTION_INFORMATION";
                public const string DONOR_MAIL_MERGE_SENDING_MAIL_INFORMATION = "DONOR_MAIL_MERGE_SENDING_MAIL_INFORMATION";
                public const string DONOR_MAIL_MERGE_SENDING_MAIL_INFO = "DONOR_MAIL_MERGE_SENDING_MAIL_INFO";
            }

            public class DonorMailNewsLetter
            {
                public const string DONOR_NEWSLETTER_MAIL_INFO = "DONOR_NEWSLETTER_MAIL_INFO";
                public const string DONOR_NEWSLETTER_MAIL_PREVIEW = "DONOR_NEWSLETTER_MAIL_PREVIEW";
                public const string DONOR_NEWSLETTER_SMS_CAPTION = "DONOR_NEWSLETTER_SMS_CAPTION";
                public const string DONOR_NEWSLETTER_SMS_PREVIEW = "DONOR_NEWSLETTER_SMS_PREVIEW";
                public const string DONOR_NEWSLETTER_PRINT_CAPTION = "DONOR_NEWSLETTER_PRINT_CAPTION";
                public const string DONOR_NEWSLETTER_RECORD_EMPTY = "DONOR_NEWSLETTER_RECORD_EMPTY";
                public const string DONOR_NEWSLETTER_NORECORD_INFO = "DONOR_NEWSLETTER_NORECORD_INFO";
                public const string DONOR_NEWSLETTER_NORECORD_EDIT_INFO = "DONOR_NEWSLETTER_NORECORD_EDIT_INFO";
            }
            public class DonorMailTask
            {
                public const string DONOR_MAIL_TASK_CREATE_INFO = "DONOR_MAIL_TASK_CREATE_INFO";
                public const string DONOR_MAIL_TASK_EMPTY_INFO = "DONOR_MAIL_TASK_EMPTY_INFO";
            }
            public class DonorMailTemplate
            {
                public const string DONOR_MAIL_TEMPLATE_EMPTY = "DONOR_MAIL_TEMPLATE_EMPTY";
                public const string DONOR_MAIL_TEMPLATE_SAVED_INFO = "DONOR_MAIL_TEMPLATE_SAVED_INFO";
                public const string DONOR_MAIL_TEMPLATE_TYPE_SELECT_INFO = "DONOR_MAIL_TEMPLATE_TYPE_SELECT_INFO";
                public const string DONOR_MAIL_TEMPLATE_SELECT_ANIVERSARY_INFO = "DONOR_MAIL_TEMPLATE_SELECT_ANIVERSARY_INFO";
                public const string DONOR_MAIL_TEMPLATE_FESAT_NAME_EMPTY = "DONOR_MAIL_TEMPLATE_FESAT_NAME_EMPTY";
                public const string DONOR_MAIL_TEMPLATE_FEAST_NAME_EXISTS_INFO = "DONOR_MAIL_TEMPLATE_FEAST_NAME_EXISTS_INFO";
                public const string DONOR_MAIL_TEMPLATE_FEAST_NAME_SELECT_INFO = "DONOR_MAIL_TEMPLATE_FEAST_NAME_SELECT_INFO";
            }
            public class DonorMailThanksgiving
            {
                public const string DONOR_MAIL_THANKSGIVING_MAIL_CAPTION = "DONOR_MAIL_THANKSGIVING_MAIL_CAPTION";
                public const string DONOR_MAIL_THANKSGIVING_MAIL_GENERATE_PREVIEW = "DONOR_MAIL_THANKSGIVING_MAIL_GENERATE_PREVIEW";
                public const string DONOR_MAIL_THANKSGIVING_SMS_CAPTION = "DONOR_MAIL_THANKSGIVING_SMS_CAPTION";
                public const string DONOR_MAIL_THANKSGIVING_SMS_GENERATE_PREVIEW = "DONOR_MAIL_THANKSGIVING_SMS_GENERATE_PREVIEW";
                public const string DONOR_MAIL_THANKSGIVING_NORECORD_SELECT_PREVIEW = "DONOR_MAIL_THANKSGIVING_NORECORD_SELECT_PREVIEW";
                public const string DONOR_MAIL_THANKSGIVING_PRINT_CAPTION = "DONOR_MAIL_THANKSGIVING_PRINT_CAPTION";
                public const string DONOR_SMS_MERGE_INTERNET_CONNECTION_INFO = "DONOR_SMS_MERGE_INTERNET_CONNECTION_INFO";
                public const string DONOR_SMS_MERGE_SENDING_SMS_INFO = "DONOR_SMS_MERGE_SENDING_SMS_INFO";
                public const string DONOR_SMS_MERGE_INTERNET_CONNECTION_CHECK_INFO = "DONOR_SMS_MERGE_INTERNET_CONNECTION_CHECK_INFO";
                public const string DONOR_SMS_MERGE_SMS_SEND_INFORMATION = "DONOR_SMS_MERGE_SMS_SEND_INFORMATION";
            }
            public class DonorSMSTemplate
            {
                public const string DONOR_SMS_TEMPLATE_EMPTY = "DONOR_SMS_TEMPLATE_EMPTY";
                public const string DONOR_SMS_TEMPLATE_SAVED_INFO = "DONOR_SMS_TEMPLATE_SAVED_INFO";
                public const string DONOR_SMS_TEMPLATE_TYPE_SELECT_INFO = "DONOR_SMS_TEMPLATE_TYPE_SELECT_INFO";
                public const string DONOR_SMS_TEMPLATE_SELECT_ANIVERSARY_INFO = "DONOR_SMS_TEMPLATE_SELECT_ANIVERSARY_INFO";
                public const string DONOR_SMS_TEMPLATE_FESAT_NAME_EMPTY = "DONOR_SMS_TEMPLATE_FESAT_NAME_EMPTY";
                public const string DONOR_SMS_TEMPLATE_FEAST_NAME_EXISTS_INFO = "DONOR_SMS_TEMPLATE_FEAST_NAME_EXISTS_INFO";
                public const string DONOR_SMS_TEMPLATE_FEAST_NAME_NOTSELECT_INFO = "DONOR_SMS_TEMPLATE_FEAST_NAME_NOTSELECT_INFO";
                public const string DONOR_TITLE_VIEW_CAPTION = "DONOR_TITLE_VIEW_CAPTION";
            }
            public class NetworkingDonorTitle
            {
                public const string NETWORKING_DONOR_TITLE_VIEW_CAPTION = "NETWORKING_DONOR_TITLE_VIEW_CAPTION";
                public const string NETWORKING_DONOR_TITLE_PRINT_CAPTION = "NETWORKING_DONOR_TITLE_PRINT_CAPTION";
            }
            public class NetworkingDonorRegistrationType
            {
                public const string NETWORKING_DONOR_REGISTRATION_VIEW_CAPTION = "NETWORKING_DONOR_REGISTRATION_VIEW_CAPTION";
                public const string NETWORKING_DONOR_REGISTRATION_PRINT_CAPTION = "NETWORKING_DONOR_REGISTRATION_PRINT_CAPTION";
            }
            public class ExportDonorData
            {
                public const string EXPORT_DONOR_DATA_VOUCHERS_INFO = "EXPORT_DONOR_DATA_VOUCHERS_INFO";
                public const string EXPORT_DONOR_DATA_PRINT_CAPTION = "EXPORT_DONOR_DATA_PRINT_CAPTION";
                public const string EXPORT_DONOR_DATA_SELECT_PROJECT_INFO = "EXPORT_DONOR_DATA_SELECT_PROJECT_INFO";
                public const string EXPORT_DONOR_DATA_EXPORT_VOUCHERS_INFO = "EXPORT_DONOR_DATA_EXPORT_VOUCHERS_INFO";
            }
            public class NetworkingDonorInstitutionType
            {
                public const string NETWORKING_DONOR_INS_TYPE_VIEW_CATPION = "NETWORKING_DONOR_INS_TYPE_VIEW_CATPION";
                public const string NETWORKING_DONOR_INS_TYPE_PRINT_CATPION = "NETWORKING_DONOR_INS_TYPE_PRINT_CATPION";
            }
            public class NetworkingMasterDonorReference
            {
                public const string NETWORKING_MASTER_DONOR_REF_STAFFNAME_EMTPY = "NETWORKING_MASTER_DONOR_REF_STAFFNAME_EMTPY";
                public const string NETWORKING_MASTER_DONOR_REF_VIEW_CAPTION = "NETWORKING_MASTER_DONOR_REF_VIEW_CAPTION";
                public const string NETWORKING_MASTER_DONOR_REF_PRINT_CAPTION = "NETWORKING_MASTER_DONOR_REF_PRINT_CAPTION";
            }
            public class DonorNetworkSettings
            {
                public const string NETWORKING_SETTING_THANKSGIVING_SUBJECT_EMPTY = "NETWORKING_SETTING_THANKSGIVING_SUBJECT_EMPTY";
                public const string NETWORKING_SETTING_APPEAL_SUBJECT_EMPTY = "NETWORKING_SETTING_APPEAL_SUBJECT_EMPTY";
                public const string NETWORKING_SETTING_WEDDING_SUBJECT_EMPTY = "NETWORKING_SETTING_WEDDING_SUBJECT_EMPTY";
                public const string NETWORKING_SETTING_BIRTHDAY_SUBJECT_EMPTY = "NETWORKING_SETTING_BIRTHDAY_SUBJECT_EMPTY";
            }
            public class DonorProspects
            {
                public const string DONOR_PROSPECTS_VIEW_CAPTION = "DONOR_PROSPECTS_VIEW_CAPTION";
                public const string DONOR_PROSPECTS_CONVERT_DONOR_CAPTION = "DONOR_PROSPECTS_CONVERT_DONOR_CAPTION";
            }
        }

        #endregion
    }
}



