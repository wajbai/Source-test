using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Bosco.Utility.Common;
using Payroll.DAO.Schema;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data.SqlClient;
using Bosco.Utility.ConfigSetting;


namespace Payroll.Model.UIModel
{
    public class clsPayrollStaff : SystemBase
    {
        CommonMember commmem = new CommonMember();
        ApplicationSchema.STFPERSONALDataTable dtStaff = null;
        ApplicationSchema.PRSTAFFGROUPDataTable dtprstaffgroup = null;
        PayrollSystem paysys = new PayrollSystem();

        public clsPayrollStaff()
        {
            dtStaff = this.AppSchema.STFPERSONAL;
            dtprstaffgroup = this.AppSchema.PRSTAFFGROUP;
        }
        //string sTemp = "";

        #region Payroll Staff Properties

        public string RtnStaffid = string.Empty;

        private Int32 iStaffId = 0;
        private string sEmpNo = "";
        private string sFirstName = "";
        private string sMiddleName = "";
        private string sFatherHusbandName = "";
        private string sMotherName = "";
        private string sNoofchildren = "";
        private string sBloodGroup = "";
        private string sLastDateofContract = "";
        private string sLastName = "";
        private string sGender = "";
        private string dtDOB;
        private string dtDOJ;
        private string sCategory = "";
        private string dtRetireDate = "";
        private string sKnownAs = "";
        private string dtLeavingDate = "";
        private string sLeaveRemarks = "";
        private string sDegree = "";
        private string sDesignation = "";
        private string sScaleofPay = "";
        private string spanno = "";
        private string saadharno = "";
        private string sPay = "";
        private string sIncrementMonth = "";
        private string sDepartment = "";
        private string leavingReason = string.Empty;
        private bool bEditMode = false;
        private object sQuery = "";
        private int optiondata = 0;
        private int affectedRows = 0;
        private string tableName = "Staff";
        private string sLoanName = "";
        private int iCount = 0;
        private int iDeptId = 0;
        private string sMaxWagesBasic = "";
        private string sMaxWagesHRA = "";
        private string sEarning1 = "";
        private string sEarning2 = "";
        private string sEarning3 = "";
        private string sDeduction1 = "";
        private string sDeduction2 = "";
        private string sPayingSalaryDays = "";
        private string sUAN = "";
        private double iyos;
        private string sCommandonPerformance;
        private string sAddress = "";
        private string sTelephone = "";
        private string sMobile = "";
        private string sEmergencyContactno = "";
        private string sEmail = "";
        private string sDependent1 = "";
        private string sDependent2 = "";
        private string sDependent3 = "";
        private string sWorkExperience = "";
        private string thirdpartyid = "";
        private int accountYearId = 0;
        private string sAccountIFSCCODE = string.Empty;
        private string sAccountBankBranch = string.Empty;
        private string staffstatutorycompliance = string.Empty;
        private string staffESIIPNo = string.Empty;
        
        public Int32 StaffId
        {
            get { return iStaffId; }
            set { iStaffId = value; }
        }
        public string EmpNo
        {
            get { return sEmpNo; }
            set { sEmpNo = value; }
        }
        public string MaxWagesBasic
        {
            get { return sMaxWagesBasic; }
            set { sMaxWagesBasic = value; }
        }
        public string MaxWagesHRA
        {
            get { return sMaxWagesHRA; }
            set { sMaxWagesHRA = value; }
        }
        public string Earning1
        {
            get { return sEarning1; }
            set { sEarning1 = value; }
        }
        public string Earning2
        {
            get { return sEarning2; }
            set { sEarning2 = value; }
        }
        public string Earning3
        {
            get { return sEarning3; }
            set { sEarning3 = value; }
        }
        public string Deduction1
        {
            get { return sDeduction1; }
            set { sDeduction1 = value; }
        }
        public string Deduction2
        {
            get { return sDeduction2; }
            set { sDeduction2 = value; }
        }
        public string PayingSalaryDays
        {
            get { return sPayingSalaryDays; }
            set { sPayingSalaryDays = value; }
        }
        public string UAN
        {
            get { return sUAN; }
            set { sUAN = value; }
        }
        public string FirstName
        {
            get { return sFirstName; }
            set { sFirstName = value; }
        }
        public string MiddleName
        {
            get { return sMiddleName; }
            set { sMiddleName = value; }
        }
        public string FatherHusbandName
        {
            get { return sFatherHusbandName; }
            set { sFatherHusbandName = value; }
        }
        public string MotherName
        {
            get { return sMotherName; }
            set { sMotherName = value; }
        }
        public string NoofChildren
        {
            get { return sNoofchildren; }
            set { sNoofchildren = value; }
        }
        public string BloodGroup
        {
            get { return sBloodGroup; }
            set { sBloodGroup = value; }
        }
        public string lastDateofContract
        {
            get { return sLastDateofContract; }
            set { sLastDateofContract = value; }
        }
        public string LastName
        {
            get { return sLastName; }
            set { sLastName = value; }
        }
        public string Gender
        {
            get { return sGender; }
            set { sGender = value; }
        }
        public string DOB
        {
            get { return dtDOB; }
            set { dtDOB = value; }
        }
        public string DOJ
        {
            get { return dtDOJ; }
            set { dtDOJ = value; }
        }
        public string Category
        {
            get { return sCategory; }
            set { sCategory = value; }
        }
        public string RetireDate
        {
            get { return dtRetireDate; }
            set { dtRetireDate = value; }
        }
        public string KnownAs
        {
            get { return sKnownAs; }
            set { sKnownAs = value; }
        }
        public string LeaveDate
        {
            get { return dtLeavingDate; }
            set { dtLeavingDate = value; }
        }
        public string Degree
        {
            get { return sDegree; }
            set { sDegree = value; }
        }
        public string Designation
        {
            get { return sDesignation; }
            set { sDesignation = value; }
        }
        public string LeaveRemarks
        {
            get { return sLeaveRemarks; }
            set { sLeaveRemarks = value; }
        }
        public int OptionData
        {
            get { return optiondata; }
            set { optiondata = value; }
        }
        public string ScaleofPay
        {
            get { return sScaleofPay; }
            set { sScaleofPay = value; }
        }
        public string PanNo
        {
            get { return spanno; }
            set { spanno = value; }
        }
        public string AAdharNo
        {
            get { return saadharno; }
            set { saadharno = value; }
        }
        public string Pay
        {
            get { return sPay; }
            set { sPay = value; }
        }
        public string IncrementMonth
        {
            get { return sIncrementMonth; }
            set { sIncrementMonth = value; }
        }
        public bool EditMode
        {
            get { return bEditMode; }
            set { bEditMode = value; }
        }
        public int SCount
        {
            get { return iCount; }
            set { iCount = value; }
        }
        public string LoanName
        {
            get { return sLoanName; }
            set { sLoanName = value; }
        }
        public string Department
        {
            get { return sDepartment; }
            set { sDepartment = value; }
        }
        public string LeavingReason
        {
            get { return leavingReason; }
            set { leavingReason = value; }
        }
        public int DepartmentId
        {
            get { return iDeptId; }
            set { iDeptId = value; }
        }

        
        public double YOS
        {
            get { return iyos; }
            set { iyos = value; }
        }
        public string Commandonperformance
        {
            get { return sCommandonPerformance; }
            set { sCommandonPerformance = value; }
        }
        public string Address
        {
            get { return sAddress; }
            set { sAddress = value; }
        }
        public string Telephone
        {
            get { return sTelephone; }
            set { sTelephone = value; }
        }
        public string Mobile
        {
            get { return sMobile; }
            set { sMobile = value; }
        }
        public string EmergencyContact
        {
            get { return sEmergencyContactno; }
            set { sEmergencyContactno = value; }
        }
        public string Email
        {
            get { return sEmail; }
            set { sEmail = value; }
        }
        public string Dependent1
        {
            get { return sDependent1; }
            set { sDependent1 = value; }
        }
        public string Dependent2
        {
            get { return sDependent2; }
            set { sDependent2 = value; }
        }
        public string Dependent3
        {
            get { return sDependent3; }
            set { sDependent3 = value; }
        }
        public string WorkExperience
        {
            get { return sWorkExperience; }
            set { sWorkExperience = value; }
        }
        public string ThirdPartyId
        {
            get { return thirdpartyid; }
            set { thirdpartyid = value; }
        }
        public int AccountYearId
        {
            get { return accountYearId; }
            set { accountYearId = value; }
        }

        private string accountno = string.Empty;
        public string AccountNo
        {
            get { return accountno; }
            set { accountno = value; }
        }


        public string AccountIFSCCODE
        {
            get { return sAccountIFSCCODE; }
            set { sAccountIFSCCODE = value; }
        }

        public string AccountBankBranch
        {
            get { return sAccountBankBranch; }
            set { sAccountBankBranch = value; }
        }

        public string StaffStatutoryCompliance
        {
            get { return staffstatutorycompliance; }
            set { staffstatutorycompliance = value; }
        }

        public string StaffESIIPNo
        {
            get { return staffESIIPNo; }
            set { staffESIIPNo = value; }
        }

        public string PayrollPaymentMode { get; set; }
        public Int32 PayrollPaymentModeId { get; set; }


        //On 21/11/2023, To assign Payroll department and Payroll Work location
        public Int32 PayrollDepartmentId { get; set; }
        public Int32 PayrollWorkLocationId { get; set; }
        public Int32 NameTitleId { get; set; }
        
        #endregion


        ResultArgs resultArgs = null;
        public void fillList(ListBox lst, string strTable,
        string strDisplayMember, string strValueMember)
        {
            object strSql = "";
            if (strTable == "Group")
                strSql = clsPayrollGrade.getGradeQuery(clsPayrollConstants.GET_PAYROLL_GROUP);
            else
                strSql = clsPayrollActivities.getPayrollActivitiesQry(clsPayrollConstants.PAYROLL_EXIST_OPEN);
            DataTable objDS = new DataTable();
            DataView objDV = new DataView();
            //if (objDBHand.createDataSet(strSql, strTable) == null)
            //    objDS = objDBHand.getDataSet();
            //else
            //    return;
            using (DataManager dataManager = new DataManager(strSql, strTable))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            if (resultArgs.Success)
                objDS = resultArgs.DataSource.Table;
            else
                return;
            objDV = new DataView(objDS);
            objDV.RowStateFilter = DataViewRowState.OriginalRows;

            lst.DisplayMember = strDisplayMember;
            lst.ValueMember = strValueMember;
            lst.DataSource = objDV;
            objDS.Dispose();
        }
        public void fillCombo(ComboBox combo, string strTable,
            string strDisplayMember, string strValueMember)
        {
            object strSql = "";
            if (strTable == "")
                strSql = getPayrollStaffQry(clsPayrollConstants.PAYROLL_STAFF_DEPT_LIST);
            else if (strTable == "FormulaGroup")
                strSql = SQLCommand.Payroll.FetchFormulaGroup;
            //				if(strTable=="DEGREE")
            //					strSql=clsPayrollQuery.getPayrollStaffQry(PAYROLL_STAFF_DEGREE_LIST);
            //				else
            //					strSql=clsPayrollQuery.getPayrollStaffQry(PAYROLL_STAFF_DESIG_LIST);
            DataTable objDS = new DataTable();
            DataView objDV = new DataView();
            //if (objDBHand.createDataSet(strSql, strTable) == null)
            //    objDS = objDBHand.getDataSet();
            //else
            //    return;

            using (DataManager dataManager = new DataManager(strSql, strTable))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            if (resultArgs.Success)
                objDS = resultArgs.DataSource.Table;
            else
                return;
            //MessageBox.Show(objDBHand.getRecordCount().ToString());
            objDV = new DataView(objDS);
            objDV.RowStateFilter = DataViewRowState.OriginalRows;

            combo.DisplayMember = strDisplayMember;
            combo.ValueMember = strValueMember;
            combo.DataSource = objDV;
            objDS.Dispose();
        }
        public int findOccurence(string getText, string getTextName)
        {

            //if (getTextName == "Staff")
            //    sQuery = clsPayrollQuery.getPayrollStaffQry(PAYROLL_STAFF_OCCUR);
            //else if (getTextName == "Loan")
            //{
            //    sQuery = clsPayrollQuery.getPayrollLoanQry(PAYROLL_LOAN_OCCUR);
            //    tableName = "Loan";
            //}
            //else if (getTextName == "Degree")
            //    sQuery = clsPayrollQuery.getPayrollStaffQry(PAYROLL_STAFF_DEGREE_OCCUR);
            //else
            //    sQuery = clsPayrollQuery.getPayrollStaffQry(PAYROLL_STAFF_DESIG_OCCUR);
            //this.ModifyQuery();
            //try
            //{
            //    objDBHand.createDataSet(sQuery, tableName);
            //    if (objDBHand.getRecordCount() > 0)
            //        return 1;
            //    return 0;
            //}
            //catch (OracleException ex)
            //{
            //    return 0;
            //}
            //return 0;


            try
            {
                if (getTextName == "Staff")
                {
                    sQuery = getPayrollStaffQry(clsPayrollConstants.PAYROLL_STAFF_OCCUR);
                }
                else if (getTextName == "Loan")
                {
                    sQuery = clsPayrollLoan.getPayrollLoanQry(clsPayrollConstants.PAYROLL_LOAN_OCCUR);
                }

                else if (getTextName == "Degree")
                {
                    sQuery = getPayrollStaffQry(clsPayrollConstants.PAYROLL_STAFF_DEGREE_OCCUR);
                }
                else
                {
                    sQuery = getPayrollStaffQry(clsPayrollConstants.PAYROLL_STAFF_DESIG_OCCUR);
                }

                using (DataManager dataManager = new DataManager(sQuery, tableName))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtStaff.EMPNOColumn, EmpNo);
                    dataManager.Parameters.Add(dtStaff.DEGREEColumn, Degree);
                    dataManager.Parameters.Add(dtStaff.DESIGNATIONColumn, Designation);
                    dataManager.Parameters.Add(dtStaff.DEPTIDColumn, Convert.ToString(StaffId));
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        return 1;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 0;
        }

        public ResultArgs AutoFetchDesignation()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.AutoFetchDesignation))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(this.AppSchema.STFPERSONAL.DESIGNATIONColumn, Designation);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public DataTable getStaffNamesAndIds()
        {
            using (DataManager dataManager = new DataManager(getPayrollStaffQry(clsPayrollConstants.PAYROLL_STAFF_NAMES_AND_IDS), tableName))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            if (resultArgs.Success)
                return resultArgs.DataSource.Table;
            else
                return null;
        }
        public static object getPayrollStaffQry(int iQueryId)
        {
            object sQry = string.Empty;
            switch (iQueryId)
            {
                case clsPayrollConstants.PAYROLL_STAFF_LIST:
                    sQry = SQLCommand.Payroll.PayrollStaffList;
                    break;
                case clsPayrollConstants.PAYMONTH_STAFF_PROFILE:
                    sQry = SQLCommand.Payroll.PaymonthStaffProfile;
                    break;
                case clsPayrollConstants.PAYROLL_STAFF_INSERT:
                    sQry = SQLCommand.Payroll.PayrollStaffInsert;
                    break;
                case clsPayrollConstants.PAYROLL_STAFF_OCCUR:
                    sQry = SQLCommand.Payroll.PayrollStaffOccur;
                    break;
                case clsPayrollConstants.PAYROLL_STAFF_DEGREE_OCCUR:
                    sQry = SQLCommand.Payroll.PayrollStaffDegreeOccur;
                    break;
                case clsPayrollConstants.PAYROLL_STAFF_DESIG_OCCUR:
                    sQry = SQLCommand.Payroll.PayrollStaffDesigOccur;
                    break;
                case clsPayrollConstants.PAYROLL_STAFF_DEGREE_LIST:
                    sQry = SQLCommand.Payroll.PayrollStaffDegreeList;
                    break;
                case clsPayrollConstants.PAYROLL_STAFF_DESIG_LIST:
                    sQry = SQLCommand.Payroll.PayrollStaffDesigList;
                    break;

                case clsPayrollConstants.PAYROLL_STAFF_DETAILS:
                    sQry = SQLCommand.Payroll.PayrollStaffDetails;
                    break;
                case clsPayrollConstants.PAYROLL_STAFF_OUTOFSERVICE:
                    sQry = SQLCommand.Payroll.PayrollStaffOutOfService;
                    break;
                case clsPayrollConstants.PAYROLL_STAFF_INSERVICE:
                    sQry = SQLCommand.Payroll.PayrollStaffInservice;
                    break;

                case clsPayrollConstants.PAYROLL_STAFF_DEPT_LIST:
                    sQry = SQLCommand.Payroll.PayrollStaffDeptList;
                    break;
                case clsPayrollConstants.PAYROLL_STAFF_SCALE:
                    sQry = SQLCommand.Payroll.PayrollStaffScale;
                    break;
                case clsPayrollConstants.PAYROLL_STAFF_DELETE:
                    sQry = SQLCommand.Payroll.PayrollStaffDelete;
                    break;
                case clsPayrollConstants.PAYROLL_STAFF_DEL:
                    sQry = SQLCommand.Payroll.PayrollStaffDel;
                    break;
                case clsPayrollConstants.PAYROLL_STAFF_DEL_SEL:
                    sQry = SQLCommand.Payroll.PayrollStaffDelSel;
                    break;
                case clsPayrollConstants.PAYROLL_STAFF_NAMES_AND_IDS:
                    sQry = SQLCommand.Payroll.PayrollStaffNamesAndIds;
                    break;
                case clsPayrollConstants.PAYROLL_STAFF_DEL_COMMENDS:
                    sQry = SQLCommand.Payroll.PayrollStaffCommendsDelete;
                    break;
                case clsPayrollConstants.PAYROLL_STAFF_SELECTED_NAMES_AND_IDS:
                    sQry = SQLCommand.Payroll.PayrollStaffSelectedNamesAndIds;
                    break;
                case clsPayrollConstants.PAYROLL_STAFF_DELETE_PROFILE:
                    sQry = SQLCommand.Payroll.DeleteStaffProfile;
                    break;
            }
            return sQry;
        }
        public DataTable getPayrollStaffList()
        {
            //if (objDBHand.createDataSet(getPayrollStaffQry(clsPayrollConstants.PAYROLL_STAFF_LIST), tableName) == null)
            //{
            //    iCount = objDBHand.getRecordCount();
            //    return objDBHand.getDataSet();
            //}
            using (DataManager dataManager = new DataManager(getPayrollStaffQry(clsPayrollConstants.PAYROLL_STAFF_LIST), tableName))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtStaff.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                    resultArgs.DataSource.Table.TableName = tableName;
                return resultArgs.DataSource.Table;
            }
            return null;
        }

        public DataTable getPaymonthStaffProfile()
        {
            using (DataManager dataManager = new DataManager(getPayrollStaffQry(clsPayrollConstants.PAYMONTH_STAFF_PROFILE), tableName))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                    resultArgs.DataSource.Table.TableName = tableName;
                return resultArgs.DataSource.Table;
            }
            return null;
        }

        public DataTable getPayrollStaffDetails(int stfId)
        {
            sQuery = getPayrollStaffQry(clsPayrollConstants.PAYROLL_STAFF_SCALE);
            //sQuery = sQuery.Replace("<staffid>", stfId.ToString());
            //if (objDBHand.createDataSet(sQuery, tableName) == null)
            //{
            //    iCount = objDBHand.getRecordCount();
            //    return objDBHand.getDataSet();
            //} 
            using (DataManager dataManager = new DataManager(sQuery, tableName))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtStaff.STAFFIDColumn, stfId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                    return resultArgs.DataSource.Table;
            }
            return null;
        }
        public DataTable getSelectStaffNamesAndIds(string sStaffIdColl)
        {
            object sQuery = getPayrollStaffQry(clsPayrollConstants.PAYROLL_STAFF_SELECTED_NAMES_AND_IDS);
            //sQuery = sQuery.Replace("<formulagroupstaffids>", sStaffIdColl);
            //if (objDBHand.createDataSet(sQuery, tableName) == null)
            //{
            //    return objDBHand.getDataSet().Tables[tableName];
            //} 
            using (DataManager dataManager = new DataManager(sQuery, tableName))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                    return resultArgs.DataSource.Table;
            }
            return null;
        }
        public DataTable getInService(string getServiceDate)
        {

            sQuery = getPayrollStaffQry(clsPayrollConstants.PAYROLL_STAFF_INSERVICE);

            ////sQuery = sQuery.Replace("<inserdate>", getServiceDate);
            //if (objDBHand.createDataSet(sQuery, tableName) == null)
            //{
            //    return objDBHand.getDataSet();
            //}
            using (DataManager dataManager = new DataManager(sQuery, tableName))
            {

                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtStaff.INSERDATEColumn, clsGeneral.GetMySQLDateTime(DateTime.Parse(getServiceDate, clsGeneral.DATE_FORMAT).ToString(), DateDataType.DateTime));
                dataManager.Parameters.Add(dtStaff.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                {
                    resultArgs.DataSource.Table.TableName = "Staff";
                    return resultArgs.DataSource.Table;
                }
            }
            return null;
        }
        public int DeleteStaff(Int32 fetch_ID_toDel)
        {
            sQuery = getPayrollStaffQry(clsPayrollConstants.PAYROLL_STAFF_DEL_SEL);
            //sQuery = sQuery.Replace("<staffid>", fetch_ID_toDel.ToString());
            try
            {
                //objDBHand.createDataSet(sQuery, tableName);
                using (DataManager dataManager = new DataManager(sQuery, tableName))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtStaff.STAFFIDColumn, fetch_ID_toDel);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    //}
                    //if (objDBHand.getRecordCount() > 0)
                    //{
                    sQuery = getPayrollStaffQry(clsPayrollConstants.PAYROLL_STAFF_DEL);
                    //sQuery = sQuery.Replace("<staffid>", fetch_ID_toDel.ToString());
                    insertRecord(sQuery);
                    //objDBHand.deleteRecord(sQuery);
                    sQuery = getPayrollStaffQry(clsPayrollConstants.PAYROLL_STAFF_DELETE);
                    //sQuery = sQuery.Replace("<staffid>", fetch_ID_toDel.ToString());
                    //objDBHand.deleteRecord(sQuery);
                    insertRecord(sQuery);
                    return 1;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }


        /// <summary>
        /// On 22/02/2002, to delete full staff profile
        /// 
        /// </summary>
        /// <param name="fetch_ID_toDel"></param>
        /// <returns></returns>
        public ResultArgs DeleteUnmappedStaff(Int32 fetch_ID_toDel)
        {
            sQuery = getPayrollStaffQry(clsPayrollConstants.PAYROLL_STAFF_DELETE_PROFILE);
            using (DataManager dataManager = new DataManager(sQuery))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtStaff.STAFFIDColumn, fetch_ID_toDel);
                resultArgs = dataManager.UpdateData();
            }

            return resultArgs;

            /*sQuery = getPayrollStaffQry(clsPayrollConstants.PAYROLL_STAFF_DEL_COMMENDS);
            //sQuery = sQuery.Replace("<staffid>", fetch_ID_toDel.ToString());
            try
            {
                using (DataManager dataManager = new DataManager(sQuery))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtStaff.STAFFIDColumn, fetch_ID_toDel);
                    resultArgs = dataManager.UpdateData();
                }

                if (resultArgs.Success)
                {
                    //objDBHand.createDataSet(sQuery, tableName);
                    sQuery = getPayrollStaffQry(clsPayrollConstants.PAYROLL_STAFF_DEL_SEL);
                    using (DataManager dataManager = new DataManager(sQuery, tableName))
                    {
                        dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        dataManager.Parameters.Add(dtStaff.STAFFIDColumn, fetch_ID_toDel);
                        resultArgs = dataManager.UpdateData();
                    }
                }

                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    //}
                    //if (objDBHand.getRecordCount() > 0)
                    //{
                    sQuery = getPayrollStaffQry(clsPayrollConstants.PAYROLL_STAFF_DEL);
                    //sQuery = sQuery.Replace("<staffid>", fetch_ID_toDel.ToString());
                    using (DataManager dataManager = new DataManager(sQuery))
                    {
                        dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        dataManager.Parameters.Add(dtStaff.STAFFIDColumn, fetch_ID_toDel);
                        resultArgs = dataManager.UpdateData();
                    }
                    //objDBHand.deleteRecord(sQuery);
                    //sQuery = getPayrollStaffQry(clsPayrollConstants.PAYROLL_STAFF_DELETE);
                    ////sQuery = sQuery.Replace("<staffid>", fetch_ID_toDel.ToString());
                    ////objDBHand.deleteRecord(sQuery);
                    //insertRecord(sQuery);
                    return 1;
                }
                return 0;
            }
            catch
            {
                return 0;
            }*/
        }

        private bool insertRecord(object squery)
        {
            using (DataManager dataManager = new DataManager(squery))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs.Success;
        }
        public DataTable getOutofService(string getServiceDate)
        {
            sQuery = getPayrollStaffQry(clsPayrollConstants.PAYROLL_STAFF_OUTOFSERVICE);
            //sQuery = sQuery.Replace("<outserdate>", getServiceDate);
            //if (objDBHand.createDataSet(sQuery, tableName) == null)
            //{
            //    return objDBHand.getDataSet();
            //}
            using (DataManager dataManager = new DataManager(sQuery, tableName))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtStaff.OUTSERDATEColumn, clsGeneral.GetMySQLDateTime(DateTime.Parse(getServiceDate, clsGeneral.DATE_FORMAT).ToString(), DateDataType.DateTime));
                dataManager.Parameters.Add(dtStaff.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            if (resultArgs.Success)
                return resultArgs.DataSource.Table;

            return null;
        }
        private void ModifyQuery()
        {
            //sQuery = sQuery.Replace("<staffid>", Convert.ToString(CommonId));
            //sQuery = sQuery.Replace("<empno>", EmpNo);
            //sQuery = sQuery.Replace("<degree>", Degree);
            //sQuery = sQuery.Replace("<designation>", Designation);
            //sQuery = sQuery.Replace("<loanid>", Convert.ToString(CommonId));
            //sQuery = sQuery.Replace("<loanname>", LoanName);
            //sQuery = sQuery.Replace("<deptid>", Convert.ToString(DepartmentId));
        }
        public void getStaffDetails()
        {
            DataTable dtTable = null;
            if (Convert.ToString(StaffId) != "")
            {
                sQuery = getPayrollStaffQry(clsPayrollConstants.PAYROLL_STAFF_DETAILS);
                this.ModifyQuery();
                try
                {
                    using (DataManager dataManager = new DataManager(sQuery))
                    {
                        int StaffPerformanceCountByAcYearCount = GetStaffPerformanceCountByAcYear(this.UtilityMember.NumberSet.ToInteger(clsGeneral.EMP_No), AccountYearId);
                        dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        dataManager.Parameters.Add(dtStaff.STAFFIDColumn, clsGeneral.EMP_No);
                        dataManager.Parameters.Add(dtStaff.STAFF_IDColumn, clsGeneral.EMP_No);
                        dataManager.Parameters.Add(dtStaff.PAYROLL_IDColumn, clsGeneral.PAYROLL_ID);
                        if (StaffPerformanceCountByAcYearCount != 0)
                        {
                            dataManager.Parameters.Add(dtStaff.ACCOUNT_YEAR_IDColumn, AccountYearId);
                        }
                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        resultArgs = dataManager.FetchData(DataSource.DataTable);
                        dtTable = resultArgs.DataSource.Table;
                    }
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        //MessageBox.Show(objDBHand.getData(0,"STAFF ID"));
                        //CommonId=Convert.ToInt32(objDBHand.getData(0,"STAFF ID"));
                        //EmpNo = objDBHand.getData(0, "STAFF CODE");
                        //FirstName = objDBHand.getData(0, "STAFF NAME");
                        //LastName = objDBHand.getData(0, "LAST NAME");
                        //Gender = objDBHand.getData(0, "GENDER");
                        //DOB = objDBHand.getData(0, "DATE OF BIRTH");
                        //DOJ = objDBHand.getData(0, "DATE OF JOIN");
                        //Category = objDBHand.getData(0, "CATEGORY");
                        //RetireDate = objDBHand.getData(0, "RETIREMENT DATE");
                        //KnownAs = objDBHand.getData(0, "KNOWN AS");
                        //LeaveDate = objDBHand.getData(0, "LEAVING DATE");
                        //Degree = objDBHand.getData(0, "DEGREE");
                        //Designation = objDBHand.getData(0, "DESIGNATION");
                        //ScaleofPay = objDBHand.getData(0, "SCALE OF PAY");
                        //Department = objDBHand.getData(0, "DEPARTMENT");
                        //IncrementMonth = objDBHand.getData(0, "PAYINCM1");
                        EmpNo = dtTable.Rows[0]["STAFF CODE"].ToString();
                        FirstName = dtTable.Rows[0]["STAFF NAME"].ToString();
                        MiddleName = dtTable.Rows[0]["MIDDLE NAME"].ToString();
                        FatherHusbandName = dtTable.Rows[0]["FATHER HUSBAND NAME"].ToString();
                        MotherName = dtTable.Rows[0]["MOTHER NAME"].ToString();
                        NoofChildren = dtTable.Rows[0]["NO OF CHILDREN"].ToString();
                        BloodGroup = dtTable.Rows[0]["BLOOD GROUP"].ToString();
                        lastDateofContract =  UtilityMember.DateSet.ToDate(dtTable.Rows[0]["LAST DATE OF CONTRACT"].ToString());
                        LastName = dtTable.Rows[0]["LAST NAME"].ToString();
                        Gender = dtTable.Rows[0]["GENDER"].ToString();
                        DOB = dtTable.Rows[0]["DATE OF BIRTH"].ToString();
                        DOJ = dtTable.Rows[0]["DATE OF JOIN"].ToString();
                        RetireDate = dtTable.Rows[0]["RETIREMENT DATE"].ToString();
                        KnownAs = dtTable.Rows[0]["KNOWN AS"].ToString();
                        LeaveDate = dtTable.Rows[0]["LEAVING DATE"].ToString();
                        Degree = dtTable.Rows[0]["DEGREE"].ToString();
                        Designation = dtTable.Rows[0]["DESIGNATION"].ToString();
                        ScaleofPay = dtTable.Rows[0]["SCALE OF PAY"].ToString();
                        PanNo = dtTable.Rows[0]["PAN NO"].ToString();
                        AAdharNo = dtTable.Rows[0]["AADHAR NO"].ToString();
                        Department = dtTable.Rows[0]["DEPARTMENT"].ToString();
                        IncrementMonth = dtTable.Rows[0]["PAYINCM1"].ToString();
                        MaxWagesBasic = dtTable.Rows[0]["MAXIMUM WAGES BASIC"].ToString();
                        MaxWagesHRA = dtTable.Rows[0]["MAXIMUM WAGES HRA"].ToString();
                        Earning1 = dtTable.Rows[0][dtStaff.EARNING1Column.ColumnName].ToString();
                        Earning2 = dtTable.Rows[0][dtStaff.EARNING2Column.ColumnName].ToString();
                        Earning3 = dtTable.Rows[0][dtStaff.EARNING3Column.ColumnName].ToString();
                        Deduction1 = dtTable.Rows[0][dtStaff.DEDUCTION1Column.ColumnName].ToString();
                        Deduction2 = dtTable.Rows[0][dtStaff.DEDUCTION2Column.ColumnName].ToString();
                        PayingSalaryDays = dtTable.Rows[0]["PAYING SALARY DAYS"].ToString();
                        UAN = dtTable.Rows[0]["UAN"].ToString();
                        LeaveRemarks = dtTable.Rows[0]["LEAVEREMARKS"].ToString();
                        AccountNo = dtTable.Rows[0]["ACCOUNT_NUMBER"].ToString();
                        YOS = this.commmem.NumberSet.ToDouble(dtTable.Rows[0]["YOS"].ToString());
                        Commandonperformance = dtTable.Rows[0]["COMMENT ON PERFORMANCE"].ToString();
                        Address = dtTable.Rows[0]["ADDRESS"].ToString();
                        Mobile = dtTable.Rows[0]["MOBILE_NO"].ToString();
                        Telephone = dtTable.Rows[0]["TELEPHONE_NO"].ToString();
                        EmergencyContact = dtTable.Rows[0]["EMERGENCY_CONTACT_NO"].ToString();
                        Email = dtTable.Rows[0]["EMAIL_ID"].ToString();
                        Dependent1 = dtTable.Rows[0]["DEPENDENT1"].ToString();
                        Dependent2 = dtTable.Rows[0]["DEPENDENT2"].ToString();
                        Dependent3 = dtTable.Rows[0]["DEPENDENT3"].ToString();
                        WorkExperience = dtTable.Rows[0]["WORK_EXPERIENCE"].ToString();
                        AccountIFSCCODE = dtTable.Rows[0][dtStaff.ACCOUNT_IFSC_CODEColumn.ColumnName].ToString();
                        AccountBankBranch= dtTable.Rows[0][dtStaff.ACCOUNT_BANK_BRANCHColumn.ColumnName].ToString();
                        staffstatutorycompliance = dtTable.Rows[0][dtStaff.STATUTORY_COMPLIANCEColumn.ColumnName].ToString();
                        staffESIIPNo = dtTable.Rows[0][dtStaff.ESI_IP_NOColumn.ColumnName].ToString();
                        PayrollPaymentModeId = UtilityMember.NumberSet.ToInteger(dtTable.Rows[0][dtStaff.PAYMENT_MODE_IDColumn.ColumnName].ToString());
                        PayrollDepartmentId = UtilityMember.NumberSet.ToInteger(dtTable.Rows[0][AppSchema.PayrollDepartment.DEPARTMENT_IDColumn.ColumnName].ToString());
                        PayrollWorkLocationId = UtilityMember.NumberSet.ToInteger(dtTable.Rows[0][AppSchema.PayrollWorkLocation.WORK_LOCATION_IDColumn.ColumnName].ToString());
                        NameTitleId = UtilityMember.NumberSet.ToInteger(dtTable.Rows[0][AppSchema.NameTitle.NAME_TITLE_IDColumn.ColumnName].ToString());
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        public ResultArgs savePayrollStaffData(Int32 StaffPayGroupId, string StaffCode, string empno, string firstname, string middlename, string fatherhusbandname, string mothername, string noofchildren, 
                                    string bloodgroup, string contractdate, string lastname, string gender, string dateofbirth, string dateofjoin, string retirementdate,
                                    string knownas, string leavedate, string leaveremarks, string Designation, string Degree, string department, decimal max_wages_hra,
                                    decimal max_wages_basic, string earning1, string earning2, string earning3, string deduction1, string deduction2, string payingsalarydays,
                                    string uan, int Option, int fetch_ID_toDel, string pay, string AccountNo, double yos, string commandperformance, string address, string mobileno,
                                    string telephone, string emergencycontactno, string email, string dependent1, string dependent2, string dependent3,
                                    string workexperience, string pan, string aadhar, string accountifsccode, string accountbankbranchname)
        {
            bool isBeginTransaction = false;
            resultArgs = new ResultArgs();
            SQLCommand.Payroll paystafcmd = SQLCommand.Payroll.PayrollStaffEdit;
            switch (Option)
            {
                case (int)clsPayrollConstants.PAYROLL_STAFF_INSERT:
                    paystafcmd = SQLCommand.Payroll.PayrollStaffInsert;
                    break;
                case (int)clsPayrollConstants.PAYROLL_STAFF_EDIT:
                    paystafcmd = SQLCommand.Payroll.PayrollStaffEdit;
                    break;
                case (int)clsPayrollConstants.PAYROLL_STAFF_DELETE:
                    paystafcmd = SQLCommand.Payroll.PayrollStaffDelete;
                    break;
            }

            using (DataManager dataManager = new DataManager(paystafcmd))
            {
                try
                {
                    if (Option == clsPayrollConstants.PAYROLL_STAFF_DELETE)
                    {
                        dataManager.Parameters.Add(dtStaff.STAFFIDColumn, fetch_ID_toDel);
                    }
                    else
                    {
                        dataManager.BeginTransaction();
                        isBeginTransaction = true;
                        dataManager.Parameters.Add(dtStaff.STAFFIDColumn, StaffId, true);
                        dataManager.Parameters.Add(dtStaff.EMPNOColumn, empno);
                        dataManager.Parameters.Add(dtStaff.FIRSTNAMEColumn, firstname);
                        dataManager.Parameters.Add(dtStaff.MIDDLE_NAMEColumn, middlename);
                        dataManager.Parameters.Add(dtStaff.FATHER_HUSBAND_NAMEColumn, fatherhusbandname);
                        dataManager.Parameters.Add(dtStaff.MOTHER_NAMEColumn, mothername);
                        dataManager.Parameters.Add(dtStaff.NO_OF_CHILDRENColumn, noofchildren);
                        dataManager.Parameters.Add(dtStaff.BLOOD_GROUPColumn, bloodgroup);
                        if (!string.IsNullOrEmpty(contractdate))
                            dataManager.Parameters.Add(dtStaff.LAST_DATE_OF_CONTRACTColumn, clsGeneral.GetMySQLDateTime(contractdate, DateDataType.DateTime));
                        else
                            dataManager.Parameters.Add(dtStaff.LAST_DATE_OF_CONTRACTColumn, DBNull.Value);
                        dataManager.Parameters.Add(dtStaff.LASTNAMEColumn, lastname);
                        dataManager.Parameters.Add(dtStaff.GENDERColumn, gender);
                        dataManager.Parameters.Add(dtStaff.DATEOFBIRTHColumn, commmem.DateSet.ToDateTime(dateofbirth.ToString(), "yyyy-MM-dd", true));
                        dataManager.Parameters.Add(dtStaff.DATEOFJOINColumn, commmem.DateSet.ToDateTime(dateofjoin.ToString(), "yyyy-MM-dd", true));
                        if (!string.IsNullOrEmpty(retirementdate))
                            dataManager.Parameters.Add(dtStaff.RETIREMENTDATEColumn, clsGeneral.GetMySQLDateTime(retirementdate, DateDataType.DateTime));
                        else
                            dataManager.Parameters.Add(dtStaff.RETIREMENTDATEColumn, DBNull.Value);

                        dataManager.Parameters.Add(dtStaff.KNOWNASColumn, knownas);

                        if (!string.IsNullOrEmpty(leavedate))
                            dataManager.Parameters.Add(dtStaff.LEAVINGDATEColumn, clsGeneral.GetMySQLDateTime(leavedate, DateDataType.DateTime));
                        else
                            dataManager.Parameters.Add(dtStaff.LEAVINGDATEColumn, DBNull.Value);

                        dataManager.Parameters.Add(dtStaff.LEAVEREMARKSColumn, leaveremarks);
                        dataManager.Parameters.Add(dtStaff.DESIGNATIONColumn, Designation);
                        dataManager.Parameters.Add(dtStaff.DEGREEColumn, Degree);
                        dataManager.Parameters.Add(dtStaff.DEPARTMENTColumn, department);
                        dataManager.Parameters.Add(dtStaff.PAYINCM1Column, commmem.NumberSet.ToInteger(IncrementMonth));
                        dataManager.Parameters.Add(dtStaff.ACCOUNT_NUMBERColumn, AccountNo);
                        dataManager.Parameters.Add(dtStaff.YOSColumn, yos);
                        dataManager.Parameters.Add(dtStaff.ADDRESSColumn, address);
                        dataManager.Parameters.Add(dtStaff.MOBILE_NOColumn, mobileno);
                        dataManager.Parameters.Add(dtStaff.TELEPHONE_NOColumn, telephone);
                        dataManager.Parameters.Add(dtStaff.EMERGENCY_CONTACT_NOColumn, emergencycontactno);
                        dataManager.Parameters.Add(dtStaff.EMAIL_IDColumn, email);
                        dataManager.Parameters.Add(dtStaff.DEPENDENT1Column, dependent1);
                        dataManager.Parameters.Add(dtStaff.DEPENDENT2Column, dependent2);
                        dataManager.Parameters.Add(dtStaff.DEPENDENT3Column, dependent3);
                        dataManager.Parameters.Add(dtStaff.WORK_EXPERIENCEColumn, workexperience);
                        dataManager.Parameters.Add(dtStaff.PAN_NOColumn, pan);
                        dataManager.Parameters.Add(dtStaff.AADHAR_NOColumn, aadhar);
                        dataManager.Parameters.Add(dtStaff.THIRD_PARTY_IDColumn, ThirdPartyId);
                        dataManager.Parameters.Add(dtStaff.ACCOUNT_IFSC_CODEColumn, accountifsccode.Trim());
                        dataManager.Parameters.Add(dtStaff.ACCOUNT_BANK_BRANCHColumn, accountbankbranchname.Trim());
                        dataManager.Parameters.Add(dtStaff.ESI_IP_NOColumn, staffESIIPNo);

                        dataManager.Parameters.Add(AppSchema.PayrollDepartment.DEPARTMENT_IDColumn, PayrollDepartmentId);
                        dataManager.Parameters.Add(AppSchema.PayrollWorkLocation.WORK_LOCATION_IDColumn, PayrollWorkLocationId);
                        dataManager.Parameters.Add(dtStaff.TITLE_IDColumn, NameTitleId);
                    }
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = false;
                    resultArgs = dataManager.UpdateData();

                    if (resultArgs.Success && (resultArgs.Success && Option != (int)clsPayrollConstants.PAYROLL_STAFF_DELETE))
                    {
                        RtnStaffid = resultArgs.RowUniqueId.ToString();
                        int Existstaffid = GetstaffIdByStaffCode(empno);
                        if (resultArgs.Success)
                        {
                            //2. Staff Service and extra pay information
                            resultArgs = UpdateStaffServiceInformation(dataManager, Option, Existstaffid, dateofjoin, pay, max_wages_hra, max_wages_basic,
                                            earning1, earning2, earning3, deduction1, deduction2, payingsalarydays, uan);
                            if (resultArgs.Success)
                            {
                                //3. Staff Performance by Ac Year
                                resultArgs = UpdateStaffPerformanceByAcYear(dataManager, Existstaffid, empno, commandperformance);
                                if (resultArgs.Success)
                                {
                                    //4. Staff Statutory Compliances
                                    resultArgs = UpdateStaffApplicableStatutoryCompliance(dataManager, Existstaffid);
                                    if (resultArgs.Success)
                                    {
                                        //5. Mapp Staff with Paygroup
                                        using(clsPrGroupStaff groupStaff = new clsPrGroupStaff())
                                        {
                                            //On 14/02/2023, To assing Bank Account, IFSCOOCDE, BankBranch and payment Mode
                                            groupStaff.AccountNumber = AccountNo;
                                            groupStaff.AccountIFSCCODE = AccountIFSCCODE;
                                            groupStaff.AccountBankBranch = AccountBankBranch;
                                            groupStaff.PayrollPaymentModeId = PayrollPaymentModeId;
                                            groupStaff.PayrollDepartmentId = PayrollDepartmentId;
                                            groupStaff.PayrollWorkLocationId = PayrollWorkLocationId;
                                            resultArgs = groupStaff.MapStaffWithPayGroupByPayroll(dataManager, StaffPayGroupId, Existstaffid.ToString());
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch(Exception err)
                {
                    resultArgs.Message = err.Message;
                }
                finally
                {
                    if (!resultArgs.Success)
                    {
                        dataManager.TransExecutionMode = ExecutionMode.Fail;
                    }

                    if (isBeginTransaction)
                    {
                        dataManager.EndTransaction();
                    }
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// On 23/02/2022, to update or change Staff group
        /// </summary>
        /// <param name="dataManager"></param>
        /// <param name="GrpId"></param>
        /// <param name="Existstaffid"></param>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        private ResultArgs UpdateStaffGroupAndProject(DataManager dataManager, long GrpId, Int32 Existstaffid, int ProjectId)
        {
            //using (clsPrGroupStaff GroupStaff = new clsPrGroupStaff())
            //{
            //    resultArgs = GroupStaff.DeleteStaffGroup(Existstaffid.ToString());
            //    if (resultArgs.Success)
            //    {
            //        string strResult = GroupStaff.SaveNewStaffInGroup(GrpId, Existstaffid.ToString());

            //        if (SettingProperty.PayrollFinanceEnabled)
            //        {
            //            if (!string.IsNullOrEmpty(Existstaffid.ToString()))
            //            {
            //                resultArgs = GroupStaff.DeleteProjectStaff(Existstaffid.ToString());
            //                if (resultArgs.Success)
            //                {
            //                    resultArgs = GroupStaff.SaveProjectStaff(ProjectId, Existstaffid.ToString());
            //                }
            //            }
            //        }
            //    }
            //}
            return resultArgs;
        }
    

        private ResultArgs UpdateStaffServiceInformation(DataManager dataManager, Int32 Option, Int32 Existstaffid, string dateofjoin, string pay,
                                    decimal max_wages_hra, decimal max_wages_basic, string earning1, string earning2, string earning3, string deduction1, string deduction2,
                                    string payingsalarydays, string uan)
        {
            if (Option.Equals((int)clsPayrollConstants.PAYROLL_STAFF_INSERT) && RtnStaffid != "0")
            {
                using (DataManager dataManagerNew = new DataManager(SQLCommand.Payroll.PayrollStaffserviceInsert))
                {
                    dataManagerNew.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManagerNew.Database = dataManager.Database;
                    dataManagerNew.Parameters.Add(dtStaff.STAFFIDColumn, Existstaffid);
                    dataManagerNew.Parameters.Add(dtStaff.DATEOFJOINColumn, commmem.DateSet.ToDateTime(dateofjoin.ToString(), "yyyy-MM-dd", true));
                    dataManagerNew.Parameters.Add(dtStaff.SCALEOFPAYColumn, ScaleofPay);
                    dataManagerNew.Parameters.Add(dtStaff.PAYColumn, pay);
                    dataManagerNew.Parameters.Add(dtStaff.MAXWAGESBASICColumn, max_wages_basic);
                    dataManagerNew.Parameters.Add(dtStaff.UANColumn, uan);
                    dataManagerNew.Parameters.Add(dtStaff.MAXWAGESHRAColumn, max_wages_hra);
                    dataManagerNew.Parameters.Add(dtStaff.EARNING1Column, this.UtilityMember.NumberSet.ToDecimal(earning1));
                    dataManagerNew.Parameters.Add(dtStaff.EARNING2Column, this.UtilityMember.NumberSet.ToDecimal(earning2));
                    dataManagerNew.Parameters.Add(dtStaff.EARNING3Column, this.UtilityMember.NumberSet.ToDecimal(earning3));
                    dataManagerNew.Parameters.Add(dtStaff.DEDUCTION1Column, this.UtilityMember.NumberSet.ToDecimal(deduction1));
                    dataManagerNew.Parameters.Add(dtStaff.DEDUCTION2Column, this.UtilityMember.NumberSet.ToDecimal(deduction2));
                    dataManagerNew.Parameters.Add(dtStaff.PAYING_SALARY_DAYSColumn, this.UtilityMember.NumberSet.ToDecimal(payingsalarydays));
                    dataManagerNew.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManagerNew.DataCommandArgs.IsDirectReplaceParameter = false;
                    resultArgs = dataManagerNew.UpdateData();
                }
            }
            else if (Option.Equals((int)clsPayrollConstants.PAYROLL_STAFF_EDIT) || Option.Equals((int)clsPayrollConstants.PAYROLL_STAFF_INSERT))
            {
                using (DataManager dataManagerNew = new DataManager(SQLCommand.Payroll.PayrollStaffserviceEdit))
                {
                    dataManagerNew.Database = dataManager.Database;
                    dataManagerNew.Parameters.Add(dtStaff.STAFFIDColumn, Existstaffid);
                    dataManagerNew.Parameters.Add(dtStaff.DATEOFJOINColumn, clsGeneral.GetMySQLDateTime(DateTime.Parse(dateofjoin, clsGeneral.DATE_FORMAT).ToString(), DateDataType.DateTime));
                    dataManagerNew.Parameters.Add(dtStaff.SCALEOFPAYColumn, ScaleofPay);
                    dataManagerNew.Parameters.Add(dtStaff.PAYColumn, pay);
                    dataManagerNew.Parameters.Add(dtStaff.MAXWAGESBASICColumn, max_wages_basic);
                    dataManagerNew.Parameters.Add(dtStaff.UANColumn, uan);
                    dataManagerNew.Parameters.Add(dtStaff.MAXWAGESHRAColumn, max_wages_hra);
                    dataManagerNew.Parameters.Add(dtStaff.EARNING1Column, this.UtilityMember.NumberSet.ToDecimal(earning1));
                    dataManagerNew.Parameters.Add(dtStaff.EARNING2Column, this.UtilityMember.NumberSet.ToDecimal(earning2));
                    dataManagerNew.Parameters.Add(dtStaff.EARNING3Column, this.UtilityMember.NumberSet.ToDecimal(earning3));
                    dataManagerNew.Parameters.Add(dtStaff.DEDUCTION1Column, this.UtilityMember.NumberSet.ToDecimal(deduction1));
                    dataManagerNew.Parameters.Add(dtStaff.DEDUCTION2Column, this.UtilityMember.NumberSet.ToDecimal(deduction2));
                    dataManagerNew.Parameters.Add(dtStaff.PAYING_SALARY_DAYSColumn, this.UtilityMember.NumberSet.ToDecimal(payingsalarydays));
                    dataManagerNew.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = false;
                    resultArgs = dataManagerNew.UpdateData();
                }
            }

            return resultArgs;
        }

        /// <summary>
        /// Update Staff performance by Accounting Year
        /// </summary>
        /// <param name="dataManager"></param>
        /// <param name="empno"></param>
        /// <param name="commandperformance"></param>
        /// <returns></returns>
        private ResultArgs UpdateStaffPerformanceByAcYear(DataManager dataManager, Int32 Existstaffid, string empno, string commandperformance)
        {
            //int StaffPerformanceCountByAcYearCount = GetStaffPerformanceCountByAcYear(StaffId != 0 ? StaffId : Existstaffid, this.UtilityMember.NumberSet.ToInteger(AccPeriodId));
            int StaffPerformanceCountByAcYearCount = GetStaffPerformanceCountByAcYear(Existstaffid, this.UtilityMember.NumberSet.ToInteger(AccPeriodId));

            if (StaffPerformanceCountByAcYearCount == 0)
            {
                using (DataManager data = new DataManager(SQLCommand.Payroll.PayrollCommentPerformanceInsert))
                {
                    data.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    data.Database = dataManager.Database;
                    data.Parameters.Add(dtStaff.STAFFIDColumn, Existstaffid);
                    data.Parameters.Add(dtStaff.ACCOUNT_YEAR_IDColumn, this.UtilityMember.NumberSet.ToInteger(AccPeriodId));
                    data.Parameters.Add(dtStaff.COMMENT_ON_PERFORMANCEColumn, commandperformance);
                    resultArgs = data.UpdateData();
                }
            }
            else
            {
                using (DataManager data = new DataManager(SQLCommand.Payroll.PayrollCommentPerformanceEdit))
                {
                    data.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    data.Database = dataManager.Database;
                    data.Parameters.Add(dtStaff.STAFFIDColumn, Existstaffid);
                    data.Parameters.Add(dtStaff.ACCOUNT_YEAR_IDColumn, this.UtilityMember.NumberSet.ToInteger(AccPeriodId));
                    data.Parameters.Add(dtStaff.COMMENT_ON_PERFORMANCEColumn, commandperformance);
                    resultArgs = data.UpdateData();
                }
            }

            return resultArgs;
        }

        /// <summary>
        /// On 22/02/2022, To update Staff applicable statutory compliances
        /// </summary>
        /// <param name="dataManager"></param>
        /// <param name="Existstaffid"></param>
        /// <returns></returns>
        private ResultArgs UpdateStaffApplicableStatutoryCompliance(DataManager dataManager, Int32 Existstaffid)
        {
            using (DataManager dm = new DataManager(SQLCommand.Payroll.DeletePRStaffStatutoryCompliance))
            {
                dm.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dm.Parameters.Add(dtStaff.STAFF_IDColumn, Existstaffid);
                dm.Parameters.Add(dtStaff.PAYROLL_IDColumn, clsGeneral.PAYROLL_ID);
                dm.Database = dataManager.Database;
                resultArgs = dm.UpdateData();
            }
            if (resultArgs.Success && !string.IsNullOrEmpty(staffstatutorycompliance))
            {
                string[] arrstaffstatutorycompliance = staffstatutorycompliance.Split(',');
                foreach (string st in arrstaffstatutorycompliance)
                {
                    if (!String.IsNullOrEmpty(st) && NumberSet.ToInteger(st) > 0)
                    {
                        using (DataManager dm = new DataManager(SQLCommand.Payroll.InsertPRStaffStatutoryCompliance))
                        {
                            dm.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                            dm.Parameters.Add(dtStaff.STAFF_IDColumn, Existstaffid);
                            dm.Parameters.Add(dtStaff.PAYROLL_IDColumn, clsGeneral.PAYROLL_ID);
                            dm.Parameters.Add(dtStaff.STATUTORY_COMPLIANCEColumn, NumberSet.ToInteger(st));
                            dm.Database = dataManager.Database;
                            resultArgs = dm.UpdateData();
                        }
                    }
                }
            }
            return resultArgs;
        }


        /// <summary>
        /// On 27/02/2023
        /// </summary>
        /// <returns></returns>
        public ResultArgs UpdatePaymonthStaffProfileDetails(Int32 staffid, Int32 Paygroupid)
        {
            ResultArgs resultArgs = new ResultArgs();

            using (DataManager dataManager = new DataManager())
            {
                Boolean isBeginTransaction = false;
                try
                {
                    dataManager.BeginTransaction();
                    isBeginTransaction = true;
                    resultArgs = UpdateStaffApplicableStatutoryCompliance(dataManager, staffid);
                    if (resultArgs.Success)
                    {
                        //5. Mapp Staff with Paygroup
                        using (clsPrGroupStaff groupStaff = new clsPrGroupStaff())
                        {
                            //On 14/02/2023, To assing Bank Account, IFSCOOCDE, BankBranch and payment Mode
                            groupStaff.AccountNumber = AccountNo;
                            groupStaff.AccountIFSCCODE = AccountIFSCCODE;
                            groupStaff.AccountBankBranch = AccountBankBranch;
                            groupStaff.PayrollPaymentModeId = PayrollPaymentModeId;
                            groupStaff.PayrollDepartmentId = PayrollDepartmentId;
                            groupStaff.PayrollWorkLocationId = PayrollWorkLocationId;
                            resultArgs = groupStaff.MapStaffWithPayGroupByPayroll(dataManager, Paygroupid, staffid.ToString());
                        }
                    }
                }
                catch (Exception err)
                {
                    resultArgs.Message = err.Message;
                }
                finally
                {
                    if (!resultArgs.Success)
                    {
                        dataManager.TransExecutionMode = ExecutionMode.Fail;
                    }

                    if (isBeginTransaction)
                    {
                        dataManager.EndTransaction();
                    }
                }
            }

            return resultArgs;
        }

        public ResultArgs FetchStaffIdByStaffThirdPartyID()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.PayrollStaffIdByStaffRefUniqueId))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtStaff.THIRD_PARTY_IDColumn, ThirdPartyId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        public ResultArgs FetchGroups(string strTable)
        {
            object strSql = "";
            if (strTable == "Group")
            {
                strSql = clsPayrollGrade.getGradeQuery(clsPayrollConstants.GET_PAYROLL_GROUP);
            }
            else
            {
                strSql = clsPayrollActivities.getPayrollActivitiesQry(clsPayrollConstants.PAYROLL_EXIST_OPEN);
            }
            using (DataManager dataManager = new DataManager(strSql, strTable))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int GetProjectidByStaffId(string StaffId)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchProjectIdByStaffId))
                {
                    dataManager.Parameters.Add(dtStaff.STAFFIDColumn, StaffId);
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ers)
            {
                MessageRender.ShowMessage(ers.Message);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int GetGroupidByStaffId(string StaffId)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchGroupIdByStaffId))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtStaff.STAFFIDColumn, StaffId);
                    dataManager.Parameters.Add(dtStaff.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ers)
            {
                MessageRender.ShowMessage(ers.Message);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        public ResultArgs UpdateExcelgroups(DataTable dtGroups)
        {
            try
            {
                if (dtGroups != null && dtGroups.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtGroups.Rows)
                    {
                        if (dr != null && !string.IsNullOrEmpty(dr["Group"].ToString()))
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.InsertPayrollGroup))
                            {
                                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                                dataManager.Parameters.Add(dtprstaffgroup.GROUPNAMEColumn, dr["Group"].ToString());
                                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                                resultArgs = dataManager.UpdateData();
                            }
                        }
                    }
                }
            }
            catch (Exception ers)
            {
                MessageRender.ShowMessage(ers.Message);
            }
            return resultArgs;
        }
        public int CheckProjectExists(string ProjectName)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.CheckProjectExists))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    dataManager.Parameters.Add(dtprstaffgroup.GROUPNAMEColumn, ProjectName);
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ers)
            {
                MessageRender.ShowMessage(ers.Message);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        public ResultArgs MapExcelProjectstoPayroll(DataTable dtProject)
        {
            try
            {
                int ProjectId;
                int PayrollId = Convert.ToInt32(clsGeneral.PAYROLL_ID);
                if (dtProject != null && dtProject.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtProject.Rows)
                    {
                        if (!string.IsNullOrEmpty(dr["Project"].ToString()))
                        {
                            DataTable dtprojectid = FetchProjectIdByProjectName(dr["Project"].ToString()).DataSource.Table;
                            ProjectId = NumberSet.ToInteger(dtprojectid.Rows[0]["PROJECT_ID"].ToString());
                            if (ProjectId != 0)
                            {
                                int projectExistsinPayroll = CheckProjectExistsForPayroll(ProjectId);
                                if (projectExistsinPayroll == 0)
                                {
                                    resultArgs = paysys.SaveProjectPayroll(ProjectId, PayrollId);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ers)
            {
                MessageRender.ShowMessage(ers.Message);
            }
            return resultArgs;
        }
        public ResultArgs FetchProjectIdByProjectName(string ProjectName)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchprojectIdByProjectName))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    dataManager.Parameters.Add(dtprstaffgroup.GROUPNAMEColumn, ProjectName);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ers)
            {
                MessageRender.ShowMessage(ers.Message);
            }
            return resultArgs;
        }
        public ResultArgs FetchGroupIdByGroupName(string groupName)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.PayrollGradeId))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    dataManager.Parameters.Add(dtprstaffgroup.GROUPNAMEColumn, groupName);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ers)
            {
                MessageRender.ShowMessage(ers.Message);
            }
            return resultArgs;
        }

        public ResultArgs FetchGroupNameBYID(string groupName)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.PayrollThirdPartyGroupId))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    dataManager.Parameters.Add(dtprstaffgroup.GROUPNAMEColumn, groupName);
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ers)
            {
                MessageRender.ShowMessage(ers.Message);
            }
            return resultArgs;
        }

        private int CheckProjectExistsForPayroll(int ProjectId)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.CheckProjectExistforPayroll))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    dataManager.Parameters.Add(dtStaff.STAFFIDColumn, ProjectId);
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ers)
            {
                MessageRender.ShowMessage(ers.Message);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        public int GetstaffIdByStaffCode(string empno)
        {
            try
            {
                using (DataManager dtmanager = new DataManager(SQLCommand.Payroll.FetchStaffidBystaffCode))
                {
                    dtmanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dtmanager.Parameters.Add(dtStaff.EMPNOColumn, empno);
                    dtmanager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dtmanager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ers)
            {
                MessageRender.ShowMessage(ers.Message);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int GetStaffPerformanceCountByAcYear(int staffid, int accountingYearId)
        {
            try
            {
                using (DataManager dtmanager = new DataManager(SQLCommand.Payroll.FetchIdByStaffIDAccountId))
                {
                    dtmanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dtmanager.Parameters.Add(dtStaff.STAFFIDColumn, staffid);
                    dtmanager.Parameters.Add(dtStaff.ACCOUNT_YEAR_IDColumn, accountingYearId);
                    dtmanager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dtmanager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ers)
            {
                MessageRender.ShowMessage(ers.Message);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs FetchPaymonthStaffProfile(Int32 staffid, long payrollid)
        {
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                using (DataManager dtmanager = new DataManager(SQLCommand.Payroll.FetchPaymonthStaffProfile))
                {
                    dtmanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dtmanager.Parameters.Add(dtStaff.STAFFIDColumn, staffid);
                    dtmanager.Parameters.Add(dtStaff.PAYROLLIDColumn, payrollid);
                    dtmanager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dtmanager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ers)
            {
                MessageRender.ShowMessage(ers.Message);
            }
            return resultArgs;
        }

    }
}

