using System;
using System.Collections.Generic;
using System.Text;
using Bosco.DAO.Data;
using Bosco.Utility;
using Payroll.DAO.Schema;
using System.Data;
using Bosco.Utility.Common;

namespace Payroll.Model.UIModel
{
    public class PayrollSystem : SystemBase
    {
        ResultArgs resultArgs=null;
        ApplicationSchema.PAYROLL_PROJECTDataTable dtProjectPayroll = new ApplicationSchema.PAYROLL_PROJECTDataTable();
        public long GetCurrentPayroll()
        {
            DataTable dtCurrentPayroll = null;
            using (DataManager dataManager = new DataManager((SQLCommand.Payroll.GetCurrentPayroll)))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    dtCurrentPayroll = resultArgs.DataSource.Table;
                    return long.Parse(dtCurrentPayroll.Rows[0]["PRId"].ToString());
                }
            }
            return 0;
        }

        public ResultArgs SaveProjectPayroll(int ProjectId, int PayrollId)
        {
            using (DataManager dataManager = new DataManager((SQLCommand.Payroll.MapProjectToPayroll)))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(dtProjectPayroll.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(dtProjectPayroll.PAYROLLIDColumn, PayrollId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        public int CheckComponentsProcessedforProject(int ProjectId)
        {
            using (DataManager dataManager = new DataManager((SQLCommand.Payroll.CheckComponentsProcessedforProject)))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(dtProjectPayroll.PROJECT_IDColumn,ProjectId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        public ResultArgs DeleteProjectPayroll()
        {
            using (DataManager dataManager = new DataManager((SQLCommand.Payroll.DeleteProjectpayroll)))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        public ResultArgs FetchMappedPayrollProjects()
        {
            using(DataManager datamanager=new DataManager(SQLCommand.Payroll.FetchMappedPayrollProjects))
            {
                datamanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchPayrollProjects()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.Payroll.FetchProjectsforPayroll))
            {
                datamanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        public ResultArgs FetchPayrollPaymentMode()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.Payroll.FetchPayrollPaymentMode))
            {
                datamanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public static string getServerDateTime()
        {
        string strDate = "";
        //string sSql = "SELECT TO_CHAR(SYSDATE, 'MM/DD/YYYY HH:MI AM') AS \"Current Date\" FROM DUAL";

        //try
        //{
        //    DataHandling dh = new DataHandling(sSql, "System Date");
        //    DataSet objDS_SystemDate = new DataSet();
        //    objDS_SystemDate = dh.getDataSet();

        //    if (objDS_SystemDate.Tables["System Date"].Rows.Count > 0)
        //        strDate = objDS_SystemDate.Tables["System Date"].Rows[0]["Current Date"].ToString();
        //}
        //catch (Exception e)
        //{
        //    MessageBox.Show(e.Message.ToString(), "MedSysB", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //}
        //return strDate;

        ResultArgs resultArgs;
        object dtDate = null;
        using (DataManager dataManager = new DataManager(SQLCommand.Payroll.GetServerDateTimeFormat))
        {
            dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
            resultArgs = dataManager.FetchData(DataSource.Scalar);
            dtDate = resultArgs.DataSource.Sclar.ToString;
            if(dtDate!=null)
            {
                return Convert.ToString(dtDate);
            }
            return null;
        }
     }
    }
}
