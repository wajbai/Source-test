using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Bosco.Utility;
using Bosco.Utility.Common;
using Payroll.DAO.Schema;
using Bosco.DAO.Data;


namespace Payroll.Model.UIModel
{
    public class clsPayrollGrade : SystemBase
    {
        ApplicationSchema.PRSTAFFGROUPDataTable dtGrade = null;
        ApplicationSchema.STFPERSONALDataTable dtstaff = null;
        public clsPayrollGrade()
        {
            dtGrade = this.AppSchema.PRSTAFFGROUP;
            dtstaff = this.AppSchema.STFPERSONAL;
        }

        public clsPayrollGrade(int groupId)
            : this()
        {
            resultArgs = getPropertiesfromDB(groupId);
            if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                PayrollGrade = resultArgs.DataSource.Table.Rows[0][dtGrade.GROUPNAMEColumn.ColumnName].ToString();
            }
        }

        public ResultArgs getPropertiesfromDB(int groupId)
        {
            strQuery = getGradeQuery(clsPayrollConstants.GET_PAYROLL_GROUP_BY_ID);
            //ReplaceQuery();
            // return insertRecord(strQuery);
            using (DataManager dataManager = new DataManager(strQuery, "PRSALARYGROUP"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtGrade.GROUPIDColumn, groupId);
                //dataManager.Parameters.Add(dtGrade.GROUPNAMEColumn, strGrade);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        #region Declaration
        DataTable dtTable = null;
        ResultArgs resultArgs = null;
        #endregion
        # region Private variables
        public string strGrade = "";

        private object strQuery = "";
        private Int32 iId;
        private Int32 iPayrollCreate;
        private DateTime dtPayrollDate;
        private string strPayrollMonth = "";
        private string strListGrade = "";
        private int iCount;
        private int iStaff;
        private int pMonth;
        private int pId;
        private string strGradeId;
        # endregion
        # region Properties
        public string PayrollGrade
        {
            set { this.strGrade = value; }
            get { return this.strGrade; }
        }
        public Int32 PayrollId
        {
            set { this.iId = value; }
            get { return this.iId; }
        }
        public Int32 PayrollCreateId
        {
            set { this.iPayrollCreate = value; }
            get { return this.iPayrollCreate; }
        }

        public DateTime PayrollDate
        {
            set { this.dtPayrollDate = value; }
            get { return this.dtPayrollDate; }
        }
        public string PayrollMonth
        {
            set { this.strPayrollMonth = value; }
            get { return this.strPayrollMonth; }
        }
        public string Grades
        {
            set { strListGrade = value; }
            get { return strListGrade; }
        }

        public Int32 OrderValue
        {
            set { iCount = value; }
            get { return iCount; }
        }
        public Int32 StaffId
        {
            set { iStaff = value; }
            get { return iStaff; }
        }
        public Int32 GradeId
        {
            set { iId = value; }
            get { return iId; }
        }
        public Int32 StatusId
        {
            set { pId = value; }
            get { return pId; }
        }
        public Int32 PayrollMonthId
        {
            set { pMonth = value; }
            get { return pMonth; }
        }
        public string StaffGradeId
        {
            set { strGradeId = value; }
            get { return strGradeId; }
        }
        # endregion
        public DataTable getPayrollMonth()
        {
            dtTable = null;
            object Query = getPayrollCreateQuery(clsPayrollConstants.PAYROLL_CHECK);
            using (DataManager dataManager = new DataManager(Query, "PRCREATE"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    dtTable = resultArgs.DataSource.Table;
                }
            }
            return dtTable;
        }

        public DataTable getPayrollGroupByPayrollId(int PayId)
        {
            dtTable = null;
            object Query = getGradeQuery(clsPayrollConstants.GET_PAYROLL_GROUP_BY_PAYROLL_ID);
            using (DataManager dataManager = new DataManager(Query))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                if (PayId > 0)
                {
                    dataManager.Parameters.Add(dtGrade.PAYROLLIDColumn, PayId);
                }
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    dtTable = resultArgs.DataSource.Table;
                }
            }
            return dtTable;
        }

        public static object getPayrollCreateQuery(int iConstId)
        {
            object PayrollCreateEnum = null;
            switch (iConstId)
            {
                case clsPayrollConstants.NEW_PAYROLL_CREATE:
                    PayrollCreateEnum = SQLCommand.Payroll.PayrollAdd;
                    break;
                case clsPayrollConstants.PAYROLL_DEFINE_STATUS:
                    PayrollCreateEnum = SQLCommand.Payroll.PayrollDefineStatus;
                    break;

                case clsPayrollConstants.PAYROLL_CHECK:
                    PayrollCreateEnum = SQLCommand.Payroll.PayrollCheck;
                    break;
                case clsPayrollConstants.GET_PAYROLL_CREATION:
                    PayrollCreateEnum = SQLCommand.Payroll.GetPayrollCreation;
                    break;
                case clsPayrollConstants.GET_GRADE_UNALLOTED://GET_GRADE_ALLOTED
                    PayrollCreateEnum = SQLCommand.Payroll.GetGradeUnalloted;
                    break;
                case clsPayrollConstants.SHOW_ALLOCATED_GRADE:
                    PayrollCreateEnum = SQLCommand.Payroll.ShowAllocatedGrade;
                    break;
                case clsPayrollConstants.GET_GRADE_ALLOTED:
                    PayrollCreateEnum = SQLCommand.Payroll.GetGradeAlloted;
                    break;
                case clsPayrollConstants.UPDATE_STAFF_GRADE:
                    PayrollCreateEnum = SQLCommand.Payroll.UpdateStaffGrade;
                    break;
                case clsPayrollConstants.ALLOCATE_STAFF_GRADE:
                    PayrollCreateEnum = SQLCommand.Payroll.AllocateStaffGrade;
                    break;
                case clsPayrollConstants.GET_PAYROLL_ID:
                    PayrollCreateEnum = SQLCommand.Payroll.GetPayrollId;
                    break;
                case clsPayrollConstants.GET_PAYROLL_DATA:
                    PayrollCreateEnum = SQLCommand.Payroll.GetPayrollData;
                    break;
            }
            return PayrollCreateEnum;

        }


        public DataTable getPayrollGradeList()
        {
            DataTable dtTable = null;
            object Query = getGradeQuery(clsPayrollConstants.GET_PAYROLL_GROUP);
            using (DataManager dataManager = new DataManager(Query, "PRSALARYGROUP"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    dtTable = resultArgs.DataSource.Table;
                    dtTable.TableName = "PRSALARYGROUP";
                }
            }
            return dtTable;
        }

        public ResultArgs getPayrollGroupByPosting(long PayrollId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollGroupByPosting, "PRSALARYGROUP"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(this.AppSchema.PayrollFinance.PAYROLL_IDColumn.ColumnName, PayrollId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        
        public int getGroupByCategoryGroup(string category)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.PayrollGroupbyGroupCategory, "PRSALARYGROUP"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtstaff.CATEGORYColumn, category);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public static object getGradeQuery(int iConstId)
        {
            object PayrollGradeeEnum = null;
            switch (iConstId)
            {
                case clsPayrollConstants.PAYROLL_GROUP_LIST:
                    PayrollGradeeEnum = SQLCommand.Payroll.PayrollGroupList;
                    break;
                case clsPayrollConstants.GET_PAYROLL_GROUP:
                    PayrollGradeeEnum = SQLCommand.Payroll.GetPayrollGroup;
                    break;
                case clsPayrollConstants.GET_PAYROLL_GROUP_BY_PAYROLL_ID:
                    PayrollGradeeEnum = SQLCommand.Payroll.GetGroupByPayrollId;
                    break;
                case clsPayrollConstants.GET_PAYROLL_GROUP_BY_ID:
                    PayrollGradeeEnum = SQLCommand.Payroll.GetPayrollGroupById;
                    break;
                case clsPayrollConstants.PAYROLL_GROUP_UPDATE:
                    PayrollGradeeEnum = SQLCommand.Payroll.PayrollGroupUpdate;
                    break;
                case clsPayrollConstants.PAYROLL_GROUP_DELETE:
                    PayrollGradeeEnum = SQLCommand.Payroll.PayrollGroupDelete;
                    break;
                case clsPayrollConstants.PAYROLL_GROUP_EXIST://GET_GRADE_ALLOTED
                    PayrollGradeeEnum = SQLCommand.Payroll.PayrollGroupExist;
                    break;
                case clsPayrollConstants.PAYROLL_GRADE_ID:
                    PayrollGradeeEnum = SQLCommand.Payroll.PayrollGradeId;
                    break;
                case clsPayrollConstants.GET_DEPARTMENTS:
                    PayrollGradeeEnum = SQLCommand.Payroll.GetDepartments;
                    break;
                //case clsPayrollConstants.SHOW_ALLOCATED_GRADE:
                //    PayrollGradeeEnum = SQLCommand.Payroll.ShowAllocatedGrade;
                //    break;
                //case clsPayrollConstants.GET_GRADE_ALLOTED:
                //    PayrollGradeeEnum = SQLCommand.Payroll.GetGradeAlloted;
                //    break;
                //case clsPayrollConstants.UPDATE_STAFF_GRADE:
                //    PayrollGradeeEnum = SQLCommand.Payroll.UpdateStaffGrade;
                //    break;
                //case clsPayrollConstants.ALLOCATE_STAFF_GRADE:
                //    PayrollGradeeEnum = SQLCommand.Payroll.AllocateStaffGrade;
                //break;
                //case clsPayrollConstants.GET_PAYROLL_ID:
                //    PayrollGradeeEnum = SQLCommand.Payroll.GetPayrollId;
                //    break;
                //case clsPayrollConstants.GET_PAYROLL_DATA:
                //    PayrollGradeeEnum = SQLCommand.Payroll.GetPayrollData;
                //    break;
            }
            return PayrollGradeeEnum;
        }
        public bool addPayrollStaffGrade()
        {
            strQuery = getGradeQuery(clsPayrollConstants.PAYROLL_GROUP_LIST);
            //ReplaceQuery();
            //return insertRecord(strQuery);
            using (DataManager dataManager = new DataManager(strQuery))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                // dataManager.Parameters.Add(dtGrade.GROUPIDColumn,);
                dataManager.Parameters.Add(dtGrade.GROUPNAMEColumn, strGrade);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs.Success;
        }

        public ResultArgs SavePayrollGroup()
        {
            strQuery = getGradeQuery(clsPayrollConstants.PAYROLL_GROUP_LIST);
            //ReplaceQuery();
            //return insertRecord(strQuery);
            using (DataManager dataManager = new DataManager(strQuery))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtGrade.GROUPIDColumn, true);
                dataManager.Parameters.Add(dtGrade.GROUPNAMEColumn, strGrade);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        //private void ReplaceQuery()
        //{
        //    //strQuery = strQuery.Replace("<groupname>", strGrade);
        //    //strQuery = strQuery.Replace("<groupid>", iId.ToString());
        //    //strQuery = strQuery.Replace("<pmonthid>", pMonth.ToString());
        //    //strQuery = strQuery.Replace("<payrollid>", iPayrollCreate.ToString());
        //    //strQuery = strQuery.Replace("<payrolldate>", dtPayrollDate.ToShortDateString());

        //    //strQuery = strQuery.Replace("<payrollMonth>", strPayrollMonth);
        //    //strQuery = strQuery.Replace("<allgrade>", strListGrade);
        //    //strQuery = strQuery.Replace("<nCount>", iCount.ToString());
        //    //strQuery = strQuery.Replace("<nStaffId>", iStaff.ToString());
        //    //strQuery = strQuery.Replace("<pid>", pId.ToString());
        //    //strQuery = strQuery.Replace("<strGradeId>", strGradeId);
        //}

        public bool isPayrollStaffGradeExist()
        {
            strQuery = getGradeQuery(clsPayrollConstants.PAYROLL_GROUP_EXIST);
            //ReplaceQuery();
            string strCheck = string.Empty;
            using (DataManager dataManager = new DataManager(strQuery))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtGrade.GROUPNAMEColumn, strGrade);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                if (resultArgs.Success)
                    strCheck = resultArgs.DataSource.Sclar.ToString;
            }
            if (strCheck == "")
                return true;
            return false;
        }
        public bool updatePayrollStaffGrade()
        {
            strQuery = getGradeQuery(clsPayrollConstants.PAYROLL_GROUP_UPDATE);
            //ReplaceQuery();
            using (DataManager dataManager = new DataManager(strQuery))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtGrade.GROUPIDColumn, PayrollId.ToString());
                dataManager.Parameters.Add(dtGrade.GROUPNAMEColumn, strGrade);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs.RowsAffected > 0;
        }

        //public bool deletePayrollStaffGrade()
        //{
        //    strQuery = getGradeQuery(clsPayrollConstants.PAYROLL_GROUP_DELETE);
        //    ReplaceQuery();
        //    return insertRecord(strQuery);
        //}

        public ResultArgs DeletePayrollGroup(long GrpId)
        {
            resultArgs = FetchPrStaffGroup(GrpId);
            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count == 0)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.PayrollGroupDelete))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtGrade.GROUPIDColumn, GrpId.ToString());
                    resultArgs = dataManager.UpdateData();
                }
            }
            else if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                resultArgs.Message = "Group has association with Staff";
            }

            return resultArgs;
        }

        //private bool insertRecord(object squery)
        //{
        //    using (DataManager dataManager = new DataManager(squery))
        //    {
        //        dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
        //        dataManager.Parameters.Add(dtGrade.GROUPIDColumn, iId.ToString());
        //        resultArgs = dataManager.UpdateData();
        //    }
        //    return resultArgs.Success;
        //}

        private ResultArgs FetchPrStaffGroup(long iGroupid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPrStaffGroup, "GROUP"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtGrade.GROUPIDColumn, iGroupid);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;

        }
    }
}
