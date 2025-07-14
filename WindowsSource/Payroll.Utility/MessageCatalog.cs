/*  Class Name      : MessageCatalog.cs
 *  Purpose         : Declare common messages
 *  Author          : Salamon Raj M
 *  Created on      : 26-07-2013
 */

using System;

namespace Payroll.Utility
{
    public class MessageCatalog
    {
        #region Common Messages
        public class Common
        {
            public const string COMMON_MESSAGE_TITLE = "COMMON_MESSAGE_TITLE";
            public const string COMMON_DELETE_CONFIRMATION = "Are you sure to Delete?";
            public const string COMMON_WELCOME_NOTE = "COMMON_WELCOME_NOTE";
            public const string COMMON_WAIT_DIALOG_CAPTION = "COMMON_WAIT_DIALOG_CAPTION";
            public const string COMMON_PROCESS_DIALOG_CAPTION = "COMMON_PROCESS_DIALOG_CAPTION";
            public const string COMMON_GRID_EMPTY = "COMMON_GRID_EMPTY";
            public const string COMMON_EMAIL_INVALID = "COMMON_EMAIL_INVALID";
            public const string COMMON_SAVE_FAILURE = "COMMON_SAVE_FAILURE";
            public const string COMMON_MESSAGE_BOX_CAPTION = "Payroll";
            public const string COMMON_NO_RECORD_SELECTED = "No record is selected";

        }
        #endregion

        #region User
        public class User
        {
            public const string USER_VIEW = "USER_VIEW";
            public const string USER_ADD_CAPTION = "USER_ADD_CAPTION ";
            public const string USER_EDIT_CAPTION = "USER_EDIT_CAPTION ";
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
            public const string USER_ROLE_PRINT_CAPTION = "USER_ROLE_PRINT_CAPTION";
            public const string USER_ROLE_ADD_CAPTION = "USER_ROLE_ADD_CAPTION";
            public const string USER_ROLE_EDIT_CAPTION = "USER_ROLE_EDIT_CAPTION";
            public const string USER_ROLE_DELETE_SUCCESS = "USER_ROLE_DELETE_SUCCESS";
            public const string USERROLE_EMPTY = "USERROLE_EMPTY";
            public const string USER_ROLE_SAVE_SUCCESS = "USER_ROLE_SAVE_SUCCESS";
        }
        #endregion

        #region Master
        public static class Master
        {
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
                public const string COST_CENTER_MAPPING_SUCESS = "COST_CENTER_MAPPING_SUCESS";
            }

            public class Donor
            {
                public const string DONOR_ADD_CAPTION = "DONOR_ADD_CAPTION";
                public const string DONOR_EDIT_CAPTION = "DONOR_EDIT_CAPTION";
                public const string DONOR_NAME_EMPTY = "DONOR_NAME_EMPTY";
                public const string DONOR_COUNTRY_EMPTY = "DONOR_COUNTRY_EMPTY";
                public const string DONOR_SAVE_SUCCESS = "DONOR_SAVE_SUCCESS";
                public const string DONOR_DELETE_SUCCESS = "DONOR_DELETE_SUCCESS";
                public const string DONOR_PRINT_CAPTION = "DONOR_PRINT_CAPTION";
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
                public const string EXECUTIVE_NAIONALITY_EMPTY = "EXECUTIVE_NATIONALITY_EMPTY";
                public const string EXECUTIVE_COUNTRY_EMPTY = "EXECUTIVE_COUNTRY_EMPTY";
                public const string EXECUTIVE_PRINT_CAPTION = "EXECUTIVE_PRINT_CAPTION";
                public const string EXECUTIVE_EMAIL_EMPTY = "EXECUTIVE_EMAIL_EMPTY";
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
            }

            public class Project
            {
                public const string PROJECT_SUCCESS = "PROJECT_SUCCESS";
                public const string PROJECT_FAILURE = "PROJECT_FAILURE";
                public const string PROJECT_CODE_EMPTY = "PROJECT_CODE_EMPTY";
                public const string PROJECT_NAME_EMPTY = "PROJECT_NAME_EMPTY";
                public const string PROJECT_ADD_CAPTION = "PROJECT_ADD_CAPTION";
                public const string PROJECT_EDIT_CAPTION = "PROJECT_EDIT_CAPTION";
                public const string PROJECT_PRINT_CAPTION = "PROJECT_PRINT_CAPTION";
                public const string PROJECT_DELETE_SUCCESS = "PROJECT_DELETE_SUCCESS";
                public const string PROJECT_DELETE_ASSOCIATION = "PROJECT_DELETE_ASSOCIATION";
                public const string PROJECT_AVAILABLE_VOUCHERS = "PROJECT_AVAILABLE_VOUCHERS";
                public const string PROJECT_PROJECT_VOUCHERS = "PROJECT_PROJECT_VOUCHERS";
                public const string PROJECT_MAPPING_SUCESS = "PROJECT_MAPPING_SUCCESS";
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
            }

            public class Ledger
            {
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
                public const string LEDGER_MAPPING_SUCCESS = "LEDGER_MAPPING_SUCCESS";
                public const string BANK_ACCOUNT_SUCCESS = "BANK_ACCOUNT_SUCCESS";
                public const string BANK_ACCOUNT_DELETED = "BANK_ACCOUNT_DELETED";
                public const string BANK_ACCCOUNT_FAILURE = "BANK_ACCOUNT_FAILURE";
                public const string BANK_ACCOUNT_CODE_EMPTY = "BANK_ACCOUNT_CODE_EMPTY";
                public const string BANK_ACCOUNT_ADD = "BANK_ACCOUNT_ADD";
                public const string BANK_ACCOUNT_EDIT = "BANK_ACCOUNT_EDIT";
                public const string BANK_ACCOUNT_PRINT_CAPTION = "BANK_ACCOUNT_PRINT_CAPTION";
            }
            public class FixedDeposit
            {
                public const string FD_SUCCESS = "FD_SUCCESS";
                public const string FD_DELETED = "FD_DELETED";
                public const string FD_FAILURE = "FD_FAILURE";
                public const string FD_ADD = "FD_ADD";
                public const string FD_EDIT = "FD_EDIT";
                public const string FD_PRINT_CAPTION = "FD_PRINT_CAPTION";
                public const string FD_CREATED_DATE_EMPTY = "FD_CREATED_DATE_EMPTY";
                public const string FD_MATURITY_DATE_EMPTY = "FD_MATURITY_DATE_EMPTY";
            }

        }
        #endregion

        #region Settings
        public class Settings
        {
            public const string SETTING_SUCCESS = "SETTING_SUCCESS";
            public const string SETTING_FAILURE = "SETTING_FAILURE";
            public const string SETTING_LANGUAGE_INVALID = "";
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
        }
        #endregion

        #region

        public class Payroll
        {
            public class Staff
            {
                public const string STAFF_CODE_NULL = "code is required";
                public const string STAFF_FIRST_NAME_NULL = "First name is required";
                public const string STAFF_GENDGER_NULL = "Gender is required";
                public const string STAFF_DESIGNATION_NULL = "Designation is required";
                public const string STAFF_DATE_OF_JOINING_NULL = "Date of joining is required";
                public const string STAFF_DATE_OF_BIRTH_NULL = "Date of birth is required";
                public const string STAFF_NOT_VALID_DATE_OF_BIRTH = "Date of Birth can't be greater than or equal to Today";
                public const string STAFF_NOT_VALID_DATE_OF_JOINING_DATE = "Date of Join can't be greater than Today";
                public const string STAFF_NOT_VALID_DATE_OF_JOINING_DATE_AGAINEST_DATE_OF_BIRTH = "Date of Join should be greater than or Equal to Date of Birth";
                public const string STAFF_NOT_VALID_RETIREMENT_DATE = "Retirement date can't be less than Today";
                public const string STAFF_NOT_VALID_LEAVEING_DATE = "Date of Joining is greater than or Equal to Leaving Date";
                public const string STAFF_NOT_BETWEEN_DATEOFJOIN = "Date of joining sholud be less than the payroll period end date";
                public const string STAFF_DETAILS_SAVED = "Record is Saved";
                public const string STAFF_DETAILS_NOT_SAVED = "Record is not saved";

                public const string STAFF_DELETE_SUCCESS = "Record is deleted";
                public const string STAFF_DELETE_FAILURE = "Record is not deleted";
                public const string STAFF_CAN_NOT_DELETE = "Can't be Deleted.Staff is used somewhere else..!";
            }
            public class Component
            {
                public const string COMPONENT_DELETE_SUCCESS = "Record is deleted";
                public const string COMPONENT_DELETE_FAILURE = "Record is not deleted";
                public const string COMPONENT_CAN_NOT_DELETE = "Can't be Deleted.Component is used somewhere else..!";
            }
            public class GroupAllocation
            {
                public const string GROUP_ALLOCATION_SUCCESS = "Staffs are allotted for selected Groups";
                public const string GROUP_ALLOCATION_FAILURE = "Staffs are not allotted for selected Groups";
                public const string STAFF_ORDER_SUCCESS = "Staffs order is set";
                public const string STAFF_ORDER_FAILURE = "Staffs order is Not set";
            }
        }

        #endregion
    }
}

