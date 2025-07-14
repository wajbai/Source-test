using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Payroll.DAO.Schema;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Utility.Common;

namespace Payroll.Model.UIModel
{
    public class clsPrGroupStaff : SystemBase
    {
        ApplicationSchema.PRSTAFFGROUPDataTable dtGrade = new ApplicationSchema.PRSTAFFGROUPDataTable();
        ApplicationSchema.PRPROJECT_STAFFDataTable dtProjectStaff = new ApplicationSchema.PRPROJECT_STAFFDataTable();
        ApplicationSchema.STFPERSONALDataTable dtStaff = new ApplicationSchema.STFPERSONALDataTable();

        public string AccountNumber { get; set; }
        public string AccountIFSCCODE { get; set; }
        public string AccountBankBranch { get; set; }
        public string PayrollPaymentMode { get; set; }
        public Int32 PayrollPaymentModeId { get; set; }

        //On 21/11/2023, To assign Payroll department and Payroll Work location
        public Int32 PayrollDepartmentId { get; set; }
        public Int32 PayrollWorkLocationId { get; set; }

        public clsPrGroupStaff()
        {
            dtGrade = this.AppSchema.PRSTAFFGROUP;
        }



        private DataTable rsMax = new DataTable();
        object sSql = string.Empty;
        ResultArgs resultArgs = null;
        clsModPay objModPay = new clsModPay();
        public object GetGroupStaffSQL(string sGroupId)
        {
            //object sSql = "SELECT STFPERSONAL.STAFFID AS \"STAFFID\",PRSALARYGROUP.GROUPID AS \"GROUPID\"," +
            //                "PRSALARYGROUP.GROUPNAME AS \"GROUP\" ,STFPERSONAL.EMPNO AS \"STAFFCODE\"," +
            //                "STFPERSONAL.FIRSTNAME ||' '|| STFPERSONAL.LASTNAME AS \"NAME\",DEPARTMENT " +
            //                "FROM STFPERSONAL,PRSTAFFGROUP,PRSALARYGROUP,HOSPITAL_DEPARTMENTS " +
            //                "WHERE STFPERSONAL.STAFFID = PRSTAFFGROUP.STAFFID AND " +
            //                "PRSTAFFGROUP.GROUPID = PRSALARYGROUP.GROUPID AND " +
            //                "PRSTAFFGROUP.PAYROLLID = " + clsGeneral.PAYROLL_ID +
            //                " AND STFPERSONAL.DEPTID = HOSPITAL_DEPARTMENTS.HDEPT_ID AND " +
            //                "PRSTAFFGROUP.GROUPID IN (" + sGroupId + ") ORDER BY PRSTAFFGROUP.STAFFORDER"; // order by staff code

            sSql = SQLCommand.Payroll.GetGroupStaffSQL;
            return sSql;
        }
        public object GetUnDefinedStaffGroupSQL()
        {
            //string sSql = "SELECT DISTINCT '0' AS \"SELECT\",STFPERSONAL.EMPNO AS \"STAFFCODE\"," +
            //                "STFPERSONAL.FIRSTNAME ||' '|| STFPERSONAL.LASTNAME AS \"NAME\",DEPARTMENT,STFPERSONAL.STAFFID " +
            //                "FROM PRCREATE,STFPERSONAL,HOSPITAL_DEPARTMENTS WHERE " +
            //                "STFPERSONAL.DEPTID = HOSPITAL_DEPARTMENTS.HDEPT_ID AND STFPERSONAL.STAFFID > 0 " +
            //                "AND STFPERSONAL.STAFFID NOT IN (SELECT DISTINCT STAFFID FROM PRSTAFFGROUP WHERE PAYROLLID = " + clsModPay.g_PayRollId + ") " +
            //                " AND (STFPERSONAL.LEAVINGDATE IS NULL  OR PRCREATE.PRDATE < STFPERSONAL.LEAVINGDATE) ORDER BY STFPERSONAL.FIRSTNAME || ' ' || STFPERSONAL.LASTNAME ";
            sSql = SQLCommand.Payroll.GetUnDefinedStaffGroupSQL;
            return sSql;
        }
        public object GetUnAssignedStaff()
        {
            sSql = SQLCommand.Payroll.GetUnassignedStaff;
            return sSql;
        }
        public DataTable GetMappedStaffs(string projectId)
        {
            sSql = SQLCommand.Payroll.GetMappedStaffs;
            DataTable dtTable = null;
            using (DataManager dataManger = new DataManager(SQLCommand.Payroll.GetMappedStaffs))
            {
                dataManger.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManger.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManger.Parameters.Add(dtProjectStaff.PROJECT_IDColumn, projectId);
                resultArgs = dataManger.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                {
                    dtTable = resultArgs.DataSource.Table;
                }
            }
            return dtTable;

        }

        public DataTable GetProjectGroupMappedStaffs(string projectId, string GroupId)
        {
            sSql = SQLCommand.Payroll.GetMappedStaffs;
            DataTable dtTable = null;
            using (DataManager dataManger = new DataManager(SQLCommand.Payroll.GetProjectGroupMappedStaffs))
            {
                dataManger.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManger.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManger.Parameters.Add(dtProjectStaff.PROJECT_IDColumn, projectId);
                dataManger.Parameters.Add(dtProjectStaff.GROUP_IDColumn, GroupId);
                resultArgs = dataManger.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                {
                    dtTable = resultArgs.DataSource.Table;
                }
            }
            return dtTable;

        }

        public DataTable GetUnmappedStaffs(string ProjectId)
        {
            DataTable dtTable = null;
            using (DataManager dataManger = new DataManager(SQLCommand.Payroll.GetUnMappedStaffs))
            {
                dataManger.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManger.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManger.Parameters.Add(dtProjectStaff.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManger.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                {
                    dtTable = resultArgs.DataSource.Table;
                }
            }
            return dtTable;

        }
        public DataTable GetAllUnmappedStaffs()
        {
            DataTable dtTable = null;
            using (DataManager dataManger = new DataManager(SQLCommand.Payroll.GetAllUnMappedStaffs))
            {
                dataManger.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManger.DataCommandArgs.IsDirectReplaceParameter = true;
                //dataManger.Parameters.Add(dtProjectStaff.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManger.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                {
                    dtTable = resultArgs.DataSource.Table;
                }
            }
            return dtTable;

        }
        public int CheckLoanExists(string staffids)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.Payroll.CheckLoanExists))
            {
                datamanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                datamanager.Parameters.Add(dtProjectStaff.STAFFIDColumn, staffids);
                resultArgs = datamanager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        public object GetGroupSQL()
        {
            //string sSql = "SELECT GROUPNAME AS \"GROUP NAME\",GROUPID FROM PRSALARYGROUP ORDER BY GROUPNAME";
            sSql = SQLCommand.Payroll.GetGroupSQL;
            return sSql;
        }

        //Assign New Staff for a Group
        public string SaveNewStaffInGroup(long nGroupId, string sStaffId)
        {
            long nStaffId;
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.GetMaxStaffSortOrder, "prstaffgroup"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtGrade.GROUPIDColumn, nGroupId);
                    dataManager.Parameters.Add(dtGrade.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
                if (resultArgs.Success)
                    rsMax = resultArgs.DataSource.Table;
             
                if (clsModPay.ProcessRunning(true, clsGeneral.PAYROLL_ID, false))
                    return "";
                int nCount = 1;

                if (rsMax.Rows.Count != 0)
                    nCount = int.Parse(rsMax.Rows[0][0].ToString()) + 1;
                string[] aStaffId = sStaffId.Split(',');

                for (int i = 0; i < aStaffId.Length; i++, nCount++)
                {
                    nStaffId = Convert.ToInt64(aStaffId[i].ToString());

                    if (objModPay.CheckDuplicate("PRSTAFFGROUP", "GROUPID = " + nGroupId +
                            " AND STAFFID = " + nStaffId + " AND PAYROLLID = " + clsGeneral.PAYROLL_ID) == false)
                    {
                        insertRecord(SQLCommand.Payroll.InsertPRStaffGroup, nStaffId, nGroupId, nCount, clsGeneral.PAYROLL_ID);
                    }
                    else
                    {
                        insertRecord(SQLCommand.Payroll.UpdatePRStaffGroup, nStaffId, nGroupId, nCount, clsGeneral.PAYROLL_ID);
                    }
                }
                rsMax = null;
                clsModPay.ProcessRunning(false, clsGeneral.PAYROLL_ID, false);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        /// <summary>
        /// On 23/04/2022, To map staff with paygroup for the current active pay month
        /// </summary>
        /// <param name="dm"></param>
        /// <returns></returns>
        public ResultArgs MapStaffWithPayGroupByPayroll(DataManager dm, Int32 paygroupid, string staffids)
        {
            Int32 MaxSortOrder = 1;
            //1. Get Maximum Staff order
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.GetMaxStaffSortOrder, "prstaffgroup"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Database = dm.Database;
                dataManager.Parameters.Add(dtGrade.GROUPIDColumn, paygroupid);
                dataManager.Parameters.Add(dtGrade.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            if (resultArgs.Success && resultArgs.DataSource.Table!=null)
            {
                DataTable dtMaximumMappedStaff = resultArgs.DataSource.Table;
                if (dtMaximumMappedStaff.Rows.Count > 0)
                {
                    MaxSortOrder = UtilityMember.NumberSet.ToInteger(dtMaximumMappedStaff.Rows[0]["STAFFORDER"].ToString()) + 1;
                }

                string[] aStaffId = staffids.Split(',');
                for (int i = 0; i < aStaffId.Length; i++, MaxSortOrder++)
                {
                    Int32 nStaffId = UtilityMember.NumberSet.ToInteger(aStaffId[i].ToString());
                    //2. Check already staff mapped 
                    using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchGroupIdByStaffId, "Check Duplicate Value"))
                    {
                        dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        dataManager.Database = dm.Database;
                        dataManager.Parameters.Add(dtGrade.STAFFIDColumn, nStaffId);
                        dataManager.Parameters.Add(dtGrade.GROUPIDColumn, paygroupid);
                        dataManager.Parameters.Add(dtGrade.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        resultArgs = dataManager.FetchData(DataSource.DataTable);
                    }

                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        bool alreadyMaped = (resultArgs.DataSource.Table.Rows.Count > 0);
                        //3. Insert or Update to Paygroup for current active pay month
                        using (DataManager dataManager = new DataManager(alreadyMaped ? SQLCommand.Payroll.UpdatePRStaffGroup : SQLCommand.Payroll.InsertPRStaffGroup))
                        {
                            dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                            dataManager.Database = dm.Database;
                            dataManager.Parameters.Add(dtGrade.STAFFIDColumn, nStaffId);
                            dataManager.Parameters.Add(dtGrade.GROUPIDColumn, paygroupid);
                            dataManager.Parameters.Add(dtGrade.STAFFORDERColumn, MaxSortOrder);
                            dataManager.Parameters.Add(dtStaff.ACCOUNT_NUMBERColumn, AccountNumber);
                            dataManager.Parameters.Add(dtStaff.ACCOUNT_IFSC_CODEColumn, AccountIFSCCODE);
                            dataManager.Parameters.Add(dtStaff.ACCOUNT_BANK_BRANCHColumn, AccountBankBranch);
                            dataManager.Parameters.Add(dtStaff.PAYMENT_MODE_IDColumn, PayrollPaymentModeId);
                            dataManager.Parameters.Add(AppSchema.PayrollDepartment.DEPARTMENT_IDColumn, PayrollDepartmentId);
                            dataManager.Parameters.Add(AppSchema.PayrollWorkLocation.WORK_LOCATION_IDColumn, PayrollWorkLocationId);
                            
                            dataManager.Parameters.Add(dtGrade.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                            dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                            resultArgs = dataManager.FetchData(DataSource.DataTable);
                        }
                         //On 21/12/2022, If this is new staff, let us mapp this staff to all payroll months
                        if (resultArgs.Success && !alreadyMaped)
                        {
                            using (DataManager dataMapManager = new DataManager(SQLCommand.Payroll.InsertPRStaffGroupForAllMonths))
                            {
                                dataMapManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                                dataMapManager.Database = dm.Database;
                                dataMapManager.Parameters.Add(dtGrade.STAFFIDColumn, nStaffId);
                                dataMapManager.Parameters.Add(dtGrade.GROUPIDColumn, paygroupid);
                                dataMapManager.Parameters.Add(dtGrade.STAFFORDERColumn, MaxSortOrder);
                                dataMapManager.Parameters.Add(dtStaff.ACCOUNT_NUMBERColumn, AccountNumber);
                                dataMapManager.Parameters.Add(dtStaff.ACCOUNT_IFSC_CODEColumn, AccountIFSCCODE);
                                dataMapManager.Parameters.Add(dtStaff.ACCOUNT_BANK_BRANCHColumn, AccountBankBranch);
                                dataMapManager.Parameters.Add(dtStaff.PAYMENT_MODE_IDColumn, PayrollPaymentModeId);
                                dataMapManager.Parameters.Add(AppSchema.PayrollDepartment.DEPARTMENT_IDColumn, PayrollDepartmentId);
                                dataMapManager.Parameters.Add(AppSchema.PayrollWorkLocation.WORK_LOCATION_IDColumn, PayrollWorkLocationId);
                                dataMapManager.Parameters.Add(dtGrade.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                                dataMapManager.DataCommandArgs.IsDirectReplaceParameter = true;
                                resultArgs = dataMapManager.FetchData(DataSource.DataTable);
                            }
                        }

                        if (!resultArgs.Success)
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return resultArgs;
        }

        public ResultArgs DeleteProjectStaff(string staffIds)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.DeleteProjectStaff))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                //dataManager.Parameters.Add(dtProjectStaff.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(dtProjectStaff.STAFFIDColumn, staffIds);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        public ResultArgs DeleteProjectIdStaff(int projectId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.DeleteProjectIdStaff))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                //dataManager.Parameters.Add(dtProjectStaff.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(dtProjectStaff.PROJECT_IDColumn, projectId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveProjectStaff(int projectId, DataTable dtStaffIds)
        {
            if (dtStaffIds != null && dtStaffIds.Rows.Count > 0)
            {
                foreach (DataRow dr in dtStaffIds.Rows)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Payroll.InsertPRProjectStaff))
                    {
                        dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        dataManager.Parameters.Add(dtProjectStaff.PROJECT_IDColumn, projectId);
                        dataManager.Parameters.Add(dtProjectStaff.STAFFIDColumn, dr[dtStaffIds.Columns["STAFFID"]]);
                        resultArgs = dataManager.UpdateData();
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs SaveProjectStaff(int projectId, string staffid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.InsertPRProjectStaff))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(dtProjectStaff.PROJECT_IDColumn, projectId);
                dataManager.Parameters.Add(dtProjectStaff.STAFFIDColumn, staffid);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }


        //Changes Staff from one Group to another
        public string SaveTransferStaffInGroup(long nGroupId, long nStaffId)
        {

            string sSql = "";
            try
            {
                if (clsModPay.ProcessRunning(true, clsModPay.g_PayRollId, false))
                    return "";
                if (objModPay.CheckDuplicate("PRSTAFFGROUP", "GROUPID = " + nGroupId +
                    " AND STAFFID = " + nStaffId + " AND PAYROLLID = " + clsModPay.g_PayRollId))
                {
                    return "Duplication";
                }

                //sSql = "UPDATE PRSTAFFGROUP SET GROUPID = " + nGroupId +
                //        " WHERE STAFFID = " + nStaffId + " AND PAYROLLID = " + clsModPay.g_PayRollId;
                if (insertRecord(SQLCommand.Payroll.UpdatePRStaffGroup, nStaffId, nGroupId, 0, clsModPay.g_PayRollId))
                {
                    return "";
                }
                clsModPay.ProcessRunning(false, clsModPay.g_PayRollId, false);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public bool SaveStaffGroupOrder(string sOrder)
        {
            string sSql = "";
            try
            {
                if (clsModPay.ProcessRunning(true, clsModPay.g_PayRollId, false))
                    return false;
                string[] aOrder = sOrder.Split('@');

                for (int i = 0; i < aOrder.Length; i++)
                {
                    string[] aVal = aOrder[i].ToString().Split('|');
                    //sSql = "UPDATE PRSTAFFGROUP SET STAFFORDER = " + Convert.ToInt32(aVal[2].ToString()) +
                    //        " WHERE GROUPID = " + Convert.ToInt32(aVal[1].ToString()) + " AND STAFFID = " + Convert.ToInt32(aVal[0].ToString()) +
                    //        " AND PAYROLLID = " + clsModPay.g_PayRollId;
                    insertRecord(SQLCommand.Payroll.SaveStaffGroupOrder, Convert.ToInt32(aVal[0].ToString()), Convert.ToInt32(aVal[1].ToString()), Convert.ToInt32(aVal[2].ToString()), clsModPay.g_PayRollId);
                }
                aOrder = null;
                clsModPay.ProcessRunning(false, clsModPay.g_PayRollId, false);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ResultArgs BulkUpdateStaffOrder(DataTable dtStaffOrder, long Payrollid)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                foreach (DataRow dr in dtStaffOrder.Rows)
                {
                    Int32 grpid =  NumberSet.ToInteger(dr[dtGrade.GROUPIDColumn.ColumnName].ToString());
                    Int32 stafforder = NumberSet.ToInteger(dr[dtGrade.STAFFORDERColumn.ColumnName].ToString());
                    Int32 staffid = NumberSet.ToInteger(dr["STAFF ID"].ToString());

                    using (DataManager dataManager = new DataManager(SQLCommand.Payroll.SaveStaffGroupOrder))
                    {
                        dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        dataManager.Parameters.Add(dtGrade.GROUPIDColumn, grpid);
                        dataManager.Parameters.Add(dtGrade.STAFFORDERColumn, stafforder);
                        dataManager.Parameters.Add(dtGrade.PAYROLLIDColumn, Payrollid);
                        dataManager.Parameters.Add(dtGrade.STAFFIDColumn, staffid);
                        result = dataManager.UpdateData();
                    }

                    if (!result.Success)
                    {
                        break;
                    }
                }
            }
            catch (Exception err)
            {
                result.Message = err.Message;
            }
            return result;
        }

        

        //To delete the entire staff details from the payroll information
        public bool DeleteStaffFromGroup(long nStaffId)
        {
            try
            {
                if (clsModPay.ProcessRunning(true, clsModPay.g_PayRollId, false))
                    return false;
                //deleteRecord("DELETE FROM PRSTAFF WHERE STAFFID = " + nStaffId);
                //deleteRecord("DELETE FROM PRLOANGET WHERE STAFFID = " + nStaffId);
                //deleteRecord("DELETE FROM PRLOANPAID WHERE STAFFID = " + nStaffId);
                //deleteRecord("DELETE FROM PRSTAFFGROUP WHERE STAFFID = " + nStaffId);
                insertRecord(SQLCommand.Payroll.DeletePRStaffByStaffId, nStaffId);
                insertRecord(SQLCommand.Payroll.DeletePrLoanGetByStaffId, nStaffId);
                insertRecord(SQLCommand.Payroll.DeletePrLoanPaidbyStaffId, nStaffId);
                insertRecord(SQLCommand.Payroll.DeletePrStaffGroupByStaffId, nStaffId);

                clsModPay.ProcessRunning(false, clsModPay.g_PayRollId, false);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool insertRecord(object sql, long nStaffId)
        {
            using (DataManager dataManager = new DataManager(sql))
            {
                dataManager.Parameters.Add(dtGrade.STAFFIDColumn, nStaffId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs.Success;
        }
        public bool insertRecord(object sql, long nStaffId, long nGroupId, int nCount, long PayrollId)
        {
            using (DataManager dataManager = new DataManager(sql))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtGrade.STAFFIDColumn, nStaffId);
                dataManager.Parameters.Add(dtGrade.GROUPIDColumn, nGroupId);
                dataManager.Parameters.Add(dtGrade.STAFFORDERColumn, nCount);
                dataManager.Parameters.Add(dtGrade.PAYROLLIDColumn, PayrollId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs.Success;
        }
        public DataTable FetchRecord(object sql, string tableName, string sGroupId)
        {
            DataTable dtTable = null;
            using (DataManager dataManger = new DataManager(sql, tableName))
            {
                dataManger.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManger.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManger.Parameters.Add(dtGrade.GROUPIDColumn, !string.IsNullOrEmpty(sGroupId) ? sGroupId : "0");
                dataManger.Parameters.Add(dtGrade.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                resultArgs = dataManger.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                {
                    dtTable = resultArgs.DataSource.Table;
                }
            }
            return dtTable;
        }
        public DataTable FetchRecord(object sql, string tableName)
        {
            DataTable dtTable = null;
            using (DataManager dataManger = new DataManager(sql, tableName))
            {
                dataManger.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                //dataManger.Parameters.Add(dtGrade.GROUPIDColumn, sGroupId);
                dataManger.Parameters.Add(dtGrade.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                resultArgs = dataManger.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                {
                    dtTable = resultArgs.DataSource.Table;
                }
            }
            return dtTable;
        }
        public ResultArgs DeleteStaffGroup(string staffId)  
        {
            using (DataManager dataManger = new DataManager(SQLCommand.Payroll.UnMapStaffGroup))
            {
                dataManger.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManger.Parameters.Add(dtGrade.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                dataManger.Parameters.Add(dtGrade.STAFFIDColumn, staffId);
                dataManger.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManger.UpdateData();
            }
            return resultArgs;
        }

    }
}
