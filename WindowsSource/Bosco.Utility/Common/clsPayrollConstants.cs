//Created by Muthu on    : 15-Feb-2007
//Last Modified    on    : 15-Feb-2007
//Purpose				 : to maintain Payroll Constants
using System;

namespace Bosco.Utility.Common
{
    /// <summary>
    /// Summary description for clsPayrollConstants.
    /// </summary>
    public class clsPayrollConstants
    {
        public clsPayrollConstants()
        {

        }
        //Below are for maintaining payroll staff details
        public const int PAYROLL_STAFF_LIST = 1;
        public const int PAYROLL_STAFF_INSERT = 2;
        public const int PAYROLL_STAFF_EDIT = 3;
        public const int PAYROLL_STAFF_DELETE = 4;
        public const int PAYROLL_STAFF_OCCUR = 5;
        public const int PAYROLL_STAFF_DEGREE_OCCUR = 6;
        public const int PAYROLL_STAFF_DESIG_OCCUR = 7;
        public const int PAYROLL_STAFF_DEGREE_LIST = 8;
        public const int PAYROLL_STAFF_DESIG_LIST = 9;
        public const int PAYROLL_STAFF_DETAILS = 10;
        public const int PAYROLL_STAFF_OUTOFSERVICE = 11;
        public const int PAYROLL_STAFF_INSERVICE = 12;
        public const int PAYROLL_STAFF_DEPT_LIST = 13;
        public const int PAYROLL_STAFF_SCALE = 14;
        public const int PAYROLL_STAFF_DEL = 15;
        public const int PAYROLL_STAFF_DEL_SEL = 16;
        public const int PAYROLL_STAFF_NAMES_AND_IDS = 17;
        public const int PAYROLL_STAFF_ID_COLL = 18;
        public const int PAYROLL_STAFF_SELECTED_NAMES_AND_IDS = 19;
        public const int PAYROLL_STAFF_SELECTED_UPDATED_NAMES_AND_IDS = 20;
        public const int PAYROLL_AUTO_FETCH_DESIGNATION = 21;
        public const int PAYROLL_STAFF_DEL_COMMENDS = 22;
        public const int PAYROLL_STAFF_DELETE_PROFILE = 23;
        public const int PAYMONTH_STAFF_PROFILE = 24;

        //Below are for maintaining payroll loan details
        public const int PAYROLL_LOAN_LIST = 1;
        public const int PAYROLL_LOAN_OCCUR = 2;
        public const int PAYROLL_LOAN_INSERT = 3;
        public const int PAYROLL_LOAN_EDIT = 4;
        public const int PAYROLL_LOAN_DELETE = 5;
        public const int PAYROLL_LOAN_DETAILS = 6;
        // Below are for maintaining Payroll General Activities
        public const int PAYROLL_EXIST_OPEN = 1;
        public const int PAYROLL_LOCK_STATUS = 2;
        public const int PAYROLL_SETLOCK_STATUS = 3;
        public const int PAYROLL_CREATED_DELETE = 4;
        public const int PAYROLL_STATUS_DELETE = 5;
        //Below are to maintain Payroll Component Allocation
        public const int PAYROLL_COMPONENT_LIST = 1;
        public const int PAYROLL_GETGROUP_LIST = 2;
        public const int PAYROLL_FULLCOMP_LIST = 3;
        public const int PAYROLL_COMP_INSERT = 4;
        public const int PAYROLL_COMPCHECK_SELECT = 5;
        public const int PAYROLL_COMP_DELETE = 6;
        public const int PAYROLL_COMP_CHANGE = 7;
        public const int PAYROLL_COMP_SELECT = 8;
        public const int PAYROLL_COMP_STAFFID = 9;
        public const int PAYROLL_COMP_NAME = 10;
        public const int PAYROLL_PROCESS_DELETE = 11;
        public const int PAYROLL_PROCESS_CHECK = 12;
        public const int PAYROLL_INSERT_PROCESS = 13;
        public const int PAYROLL_COMPID_RETURN = 14;
        public const int PAYROLL_FORMULA_GROUP_ID = 15;
        public const int PAYROLL_FORMULA_UPDATE_GROUP_ID = 16;
        public const int PAYROLL_FORMULA_FOR_GROUP = 17;


        //It is for creating the Payroll Group
        public const string PAYROLL_GRADE_ADD = "Add";
        public const string PAYROLL_GRADE_EDIT = "Edit";

        public const int PAYROLL_GROUP_LIST = 101;
        public const int GET_PAYROLL_GROUP = 102;
        public const int PAYROLL_GROUP_UPDATE = 103;
        public const int PAYROLL_GROUP_DELETE = 104;
        public const int PAYROLL_GRADE_ID = 105;
        public const int PAYROLL_GROUP_EXIST = 106;
        public const int GET_PAYROLL_GROUP_BY_ID = 107;
        public const int AUTO_FETCH_DESIGNATION = 108;

        public const int NEW_PAYROLL_CREATE = 201;
        public const int GET_PAYROLL_CREATION = 202;
        public const int PAYROLL_CHECK = 203;
        public const int GET_GRADE_ALLOTED = 204;
        public const int GET_GRADE_UNALLOTED = 205;
        public const int ALLOCATE_STAFF_GRADE = 206;
        public const int SHOW_ALLOCATED_GRADE = 207;

        public const int PAYROLL_DEFINE_STATUS = 208;
        public const int GET_PAYROLL_ID = 209;
        public const int GET_DEPARTMENTS = 210;
        public const int UPDATE_STAFF_GRADE = 211;
        public const int GET_PAYROLL_DATA = 212;


        //For Payroll component creation
        public const int PAYROLL_COMPONENT_SELECT = 501;
        public const int PAYROLL_COMPONENT_ADD = 502;
        public const int PAYROLL_COMPONENT_EDIT = 503;
        public const int PAYROLL_COMPONENT_DELETE = 504;
        public const int PAYROLL_COMPONENT = 505;
        public const int PAYROLL_EDIT_VERIFY_COMP_LINK = 506;
        public const int PAYROLL_EDIT_COMP_UPDATE = 507;
        public const int PAYROLL_COMPONENT_WITH_TYPE = 508;


        public const int PAYROLL_LOAN_MNT_LIST = 600;
        public const int PAYROLL_LOAN_MNT_STAFF = 601;
        public const int PAYROLL_LOAN_MNT_ADD = 602;
        public const int PAYROLL_LOAN_MNT_EDIT = 603;
        public const int PAYROLL_LOAN_MNT_DEL = 604;
        public const int PAYROLL_LOAN_MNT_LOAN = 605;
        //For payroll process
        public const int PAYROLL_PROCESS_LIST = 600;
        public const int PAYROLL_PROCESS_UPDATE = 601;
        public const int PAYROLL_PROCESS_GET = 602;
        //public const int PAYROLL_LOAN_MNT_EDIT	=603;

        public const int GET_PAYROLL_GROUP_BY_PAYROLL_ID = 606;
    }
}
