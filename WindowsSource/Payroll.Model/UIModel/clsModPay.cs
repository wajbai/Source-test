using System;
using System.Collections.Generic;
using System.Text;
using Bosco.Utility.Common;
using System.Data;
using Bosco.DAO.Data;
using Payroll.DAO.Schema;
using Bosco.Utility;

namespace Payroll.Model.UIModel
{
    public class clsModPay :SystemBase
    {
        ApplicationSchema.PayrollDataTable dtPayroll = null;
        public clsModPay()
        {
            dtPayroll = this.AppSchema.Payroll;
        }
        public enum TableTask { TableLock = 0, TableUnLock = 1, TableCheck = 2 };
        public static long g_PayRollId = 0;
        public static string g_PayRollDate = clsGeneral.PAYROLLDATE;
        DataTable dtTable = null;
        ResultArgs resultArgs = null;

        public string GetValue(string sTblName, string sFldValue, string sWhereSql)
        {
            //    Purpose    : This method is used to Get the Needed values
            //    Arguments  : tblName as String, sFldValue as String, sWhereSql as String
            //    ReturnType : As string
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.ConstructQuery,sTblName))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(dtPayroll.FIELDVALUEColumn, sFldValue);
                dataManager.Parameters.Add(dtPayroll.CONDITIONColumn, sWhereSql);
                dataManager.Parameters.Add(dtPayroll.TABLENAMEColumn, sTblName);
                resultArgs = dataManager.FetchData(DataSource.DataTable);

                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    dtTable = resultArgs.DataSource.Table;
                    return dtTable.Rows[0][0].ToString();
                }
            }
            return string.Empty;
        }
        //pragasam mod
        public string GetValueTocombo(string sTblName, string sFldValue, string sWhereSql)
        {
            //    Purpose    : This method is used to Get the Needed values
            //    Arguments  : tblName as String, sFldValue as String, sWhereSql as String
            //    ReturnType : As string
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.ConstructQueryToaddcombo, sTblName))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(dtPayroll.FIELDVALUEColumn, sFldValue);
                dataManager.Parameters.Add(dtPayroll.CONDITIONColumn, sWhereSql);
                dataManager.Parameters.Add(dtPayroll.TABLENAMEColumn, sTblName);
                resultArgs = dataManager.FetchData(DataSource.DataTable);

                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    dtTable = resultArgs.DataSource.Table;
                    return dtTable.Rows[0][0].ToString();
                }
            }
            return string.Empty;
        }


        public static bool ProcessRunning(bool bLock, long nPRId, bool bStaff)
        {
            if (bLock)  //(Insert Modules to the table 'TableLock')
            {
                if (bStaff)
                {
                    if (TableStatus("STAFF", "STAFF", (int)TableTask.TableLock, true, false))
                    {
                        // MsgBox "Process is going on.", vbInformation, g_Message
                        return true;
                    }
                }
                if (TableStatus("PAYROLL", "PR_" + nPRId, (int)TableTask.TableLock, false, false))
                {
                    //MsgBox "Process is going on.", vbInformation, g_Message
                    return true;
                }
            }
            else	//Release Lock (Remove Modules from the table 'TableLock')
            {
                if (bStaff)
                    TableStatus("STAFF", "STAFF", (int)TableTask.TableUnLock, false, false);    //Remove Staff
                TableStatus("PAYROLL", "PR_" + nPRId, (int)TableTask.TableUnLock, false, true); //Commit Trans
                return false;
            }
            return false;
        }
        public static bool TableStatus(string sModuleName, string sAffectedModule, int tTask, bool bBeginTrans, bool bCommitTrans)
        {
            return false;
        }
        public  bool CheckDuplicate(string sTblName)
        {
            ResultArgs resultArgs = null;
            try
            {
                //DataHandling objDH = new DataHandling();
                //objDH.createDataSet("select * from " + sTblName, "Check Duplicate Value");
                //return objDH.getRecordCount() > 0;
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchFromTable, "Check Duplicate Value"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    dataManager.Parameters.Add(dtPayroll.TABLENAMEColumn, sTblName);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
                return resultArgs.DataSource.Table.Rows.Count > 0; 
            }
            catch
            {
                return false;
            }
        }
        public bool CheckDuplicate(string sTblName, string sWhereStr)
        {
            ResultArgs resultArgs = null;
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchFromTableWithWhere, "Check Duplicate Value"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    dataManager.Parameters.Add(dtPayroll.TABLENAMEColumn, sTblName);
                    dataManager.Parameters.Add(dtPayroll.CONDITIONColumn, sWhereStr);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
                return resultArgs.DataSource.Table.Rows.Count > 0; 
            }
            catch
            {
                return false;
            }
        }
    }
}
