using System;
using System.Collections.Generic;
using System.Text;
using Bosco.DAO.Data;
using Bosco.Utility;
using Payroll.DAO.Schema;
using Bosco.Utility.Common;
using System.Data;

namespace Payroll.Model.UIModel
{
    public class clsPayrollActivities
    {
        ResultArgs resultArgs = null;
        object sQuery = string.Empty;
        DataTable dtTable = null;
        private string tableName = "PrStatus";
        public string getFormulaGroupStaffIdCollection(int iFormulaId)
        {
            //sQuery = getPayrollActivitiesQry(clsPayrollConstants.PAYROLL_STAFF_ID_COLL);
            ////sQuery = sQuery.Replace("<formulagroupid>", iFormulaId.ToString());
            //if (objDBHand.createDataSet(sQuery, tableName) == null)
            //{
            //    if (objDBHand.getRecordCount() > 0)
            //        return objDBHand.getDataSet().Tables[tableName].Rows[0][0].ToString();
            //}
            //return "";
            using (DataManager dataManager = new DataManager(sQuery, tableName))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    dtTable = resultArgs.DataSource.Table;
                    return dtTable.Rows[0][0].ToString();
                }
               
            }
            return string.Empty;
        }
        public static object getPayrollActivitiesQry(int iQueryId)
        {
            object sQry = "";
            switch (iQueryId)
            {
                case  clsPayrollConstants.PAYROLL_EXIST_OPEN:
                    sQry = SQLCommand.Payroll.PayrollExistOpen;
                    break;
                case  clsPayrollConstants.PAYROLL_LOCK_STATUS:
                    sQry = SQLCommand.Payroll.PayrollLockStatus;
                    break;
                case  clsPayrollConstants.PAYROLL_SETLOCK_STATUS:
                    sQry =SQLCommand.Payroll.PayrollSetlockStatus;
                    break;
                case  clsPayrollConstants.PAYROLL_CREATED_DELETE:
                    sQry = SQLCommand.Payroll.PayrollCreatedDelete;
                    break;
                case  clsPayrollConstants.PAYROLL_STATUS_DELETE:
                    sQry = SQLCommand.Payroll.PayrollStatusDelete;
                    break;
                case  clsPayrollConstants.PAYROLL_STAFF_ID_COLL:
                    sQry = SQLCommand.Payroll.PayrollStaffIdColl;
                    break;
                case  clsPayrollConstants.PAYROLL_FORMULA_GROUP_ID:
                    sQry = SQLCommand.Payroll.PayrollFormulaGroupId;
                    break;
                case  clsPayrollConstants.PAYROLL_FORMULA_UPDATE_GROUP_ID:
                    sQry = SQLCommand.Payroll.PayrollFormulaUpdateGroupId;
                    break;
                case  clsPayrollConstants.PAYROLL_STAFF_SELECTED_UPDATED_NAMES_AND_IDS:
                    sQry = SQLCommand.Payroll.PayrollStaffSelectedUpdatedNamesAndIds;
                    break;
            }
            return sQry;
        }
        public int updateSelectedStaffGroup(string formula_desc, string staffIdColl, string formulaGroupid)
        {
            sQuery = getPayrollActivitiesQry(clsPayrollConstants.PAYROLL_STAFF_SELECTED_UPDATED_NAMES_AND_IDS);
            //sQuery = sQuery.Replace("<formula_desc>", formula_desc);
            //sQuery = sQuery.Replace("<staffid_collection>", staffIdColl);
            //sQuery = sQuery.Replace("<formulagroupid>", formulaGroupid);
            try
            {
                using (DataManager dataManager = new DataManager(sQuery))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    resultArgs = dataManager.UpdateData();
                }
                if (resultArgs.Success)
                {
                    return 1;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }
        public int saveSelectedStaffGroup(string formula_desc, string forumulaid_coll)
        {
            sQuery = getPayrollActivitiesQry(clsPayrollConstants.PAYROLL_FORMULA_GROUP_ID);
            //sQuery = sQuery.Replace("<formulagroup>", formula_desc);
            //sQuery = sQuery.Replace("<staffid_collection>", forumulaid_coll);
            try
            {
                using (DataManager dataManager = new DataManager(sQuery))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    resultArgs = dataManager.UpdateData();
                }
                if (resultArgs.Success)
                {
                    return 1;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }
        public int updateFormulaStaffGroup(string formula_desc, string formulagroupid)
        {
            sQuery = getPayrollActivitiesQry(clsPayrollConstants.PAYROLL_FORMULA_UPDATE_GROUP_ID);
            //sQuery = sQuery.Replace("<formula_desc>", formula_desc);
            //sQuery = sQuery.Replace("<formulagroupid>", formulagroupid);
            try
            {
                using (DataManager dataManager = new DataManager(sQuery))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    resultArgs = dataManager.UpdateData();
                }
                if (resultArgs.Success)
                {
                    return 1;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }
   
        public int deletePayroll(string getPRId)
        {
            sQuery = getPayrollActivitiesQry(clsPayrollConstants.PAYROLL_CREATED_DELETE);
            //sQuery = sQuery.Replace("<payrollid>", clsGeneral.PAYROLL_ID.ToString());
            using (DataManager dataManager = new DataManager(sQuery))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.UpdateData();
                if (resultArgs.Success)
                {
                    sQuery = getPayrollActivitiesQry(clsPayrollConstants.PAYROLL_STATUS_DELETE);
                    //sQuery = sQuery.Replace("<payrollid>", clsGeneral.PAYROLL_ID.ToString());
                    using (DataManager dataMan = new DataManager(sQuery))
                    {
                        dataMan.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        resultArgs = dataMan.UpdateData();
                    }
                    if (resultArgs.Success)
                        return 1;
                    else
                        return 0;
                }
                return 0;
            }
        }
        public int setStatus(string sStatus)
        {
            sQuery = getPayrollActivitiesQry(clsPayrollConstants.PAYROLL_SETLOCK_STATUS);
            //if (sStatus == "Lock") sQuery = sQuery.Replace("<lockstatus>", "Y");
            //else if (sStatus == "UnLock") sQuery = sQuery.Replace("<lockstatus>", "N");
            //sQuery = sQuery.Replace("<payrollid>", clsGeneral.PAYROLL_ID.ToString());

            using (DataManager dataManager = new DataManager(sQuery))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.UpdateData();
                if (resultArgs.Success)
                    return 1;
                else
                    return 0;
            }
            //if (objDBHand.updateRecord(sQuery)) return 1;
            //else return 0;
        }
        public string getStatus(Int32 getPRId)
        {
            sQuery = getPayrollActivitiesQry(clsPayrollConstants.PAYROLL_LOCK_STATUS);
            //sQuery = sQuery.Replace("<payrollid>", getPRId.ToString());
            //if (objDBHand.createDataSet(sQuery, tableName) == null)
            //{
            //    if (objDBHand.getRecordCount() > 0)
            //        return objDBHand.getDataSet().Tables[tableName].Rows[0][0].ToString();
            //}
            using (DataManager dataManager = new DataManager(sQuery, tableName))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    return resultArgs.DataSource.Table.Rows[0][0].ToString();
            }
            return string.Empty;
        }
    }
}
