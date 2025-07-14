using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Bosco.Utility.Common;
using Payroll.DAO.Schema;
using Bosco.DAO.Data;
using Bosco.Utility;

namespace Payroll.Model.UIModel
{
    public class clsPayrollProcess : SystemBase
    {
        #region Declaration
        DataTable dtTable = null;
        ResultArgs resultArgs = null;
        ApplicationSchema.PAYROLL_LEDGERDataTable payLedger = new ApplicationSchema.PAYROLL_LEDGERDataTable();
        ApplicationSchema.PRPROJECT_STAFFDataTable dtProjectStaff = new ApplicationSchema.PRPROJECT_STAFFDataTable();
        // Map Ledger to Process begins
        private int typeid = 0;
        public int TypeId
        {
            set { this.typeid = value; }
            get { return this.typeid; }
        }
        private int processledgerid = 0;
        public int ProcessLedgerId
        {
            set { this.processledgerid = value; }
            get { return this.processledgerid; }
        }
        private DateTime processdate = DateTime.Now;
        public DateTime ProcessDate
        {
            set { this.processdate = value; }
            get { return this.processdate; }
        }

        // Map Ledger to Process ends
        #endregion

        public clsPayrollProcess()
        {
        }
        private string strQuery = "";
        private int Payrollid;
        private int StaffId;
        private int Componentid;
        private string Componentvalue = "";

        public int payroll_id
        {
            set { this.Payrollid = value; }
            get { return this.Payrollid; }
        }
        public int Staff_id
        {
            set { this.StaffId = value; }
            get { return this.StaffId; }
        }
        public int Component_id
        {
            set { this.Componentid = value; }
            get { return this.Componentid; }
        }
        public string Component_value
        {
            set { this.Componentvalue = value; }
            get { return this.Componentvalue; }
        }
        public DataTable getPayrollProcess(int groupid, int payrollid)
        {
            object StrQuery = getPayrollProcessQuery(clsPayrollConstants.PAYROLL_PROCESS_LIST, groupid, payrollid);
            using (DataManager dataManager = new DataManager(StrQuery, "PRProcess"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    dtTable = resultArgs.DataSource.Table;
                }
                return dtTable;
            }
        }
        public DataTable getPayrollProcessList(DataTable dt, int groupid, int payrollid)
        {
            strQuery = "select";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strQuery = strQuery + " (select t.compvalue from prstaff t where t.componentid=" + dt.Rows[i][0].ToString() + " ) as " + dt.Rows[i][1].ToString() + ",";
            }

            strQuery = strQuery + " dual";
            strQuery = strQuery.Replace(", dual", " from dual");
            // createDataSet(strQuery, "PRProcess1");
            //  return getDataSet();
            using (DataManager dataManager = new DataManager(strQuery, "PRProcess1"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    dtTable = resultArgs.DataSource.Table;
                }
                return dtTable;
            }
        }
        public static object getPayrollProcessQuery(int iConstId, int groupid, int payrollid)
        {
            object strQuery = "";
            switch (iConstId)
            {
                case clsPayrollConstants.PAYROLL_PROCESS_LIST:
                    strQuery = SQLCommand.Payroll.PayrollProcessList;
                    break;
                case clsPayrollConstants.PAYROLL_PROCESS_UPDATE:
                    strQuery = SQLCommand.Payroll.PayrollProcessUpdate;
                    break;
            }
            return strQuery;
        }

        #region Map Ledgers
        public ResultArgs SaveMappedLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.AddMapLedger))
            {
                dataManager.BeginTransaction();
                resultArgs = DeleteMappedLedger();
                if (resultArgs.Success)
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(payLedger.TYPE_IDColumn, TypeId);
                    dataManager.Parameters.Add(payLedger.LEDGER_IDColumn, ProcessLedgerId);
                    // dataManager.Parameters.Add(payLedger.PROCESS_DATEColumn, ProcessDate);
                    resultArgs = dataManager.UpdateData();
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public ResultArgs DeleteMappedLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.DeleteMapLedger))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(payLedger.TYPE_IDColumn, TypeId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        public ResultArgs FetchMappedLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchMappedLedger))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchProcessMappedLedgers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchProcessByMappedLedger))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchMappedLedgerbyLedgerId(int TypeId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchMappedLedgersByTypeId))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(payLedger.TYPE_IDColumn, TypeId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchLedgerByLedgerId(int Ledgerid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchLedgerByLedgerId))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(payLedger.LEDGER_IDColumn, Ledgerid);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        //public ResultArgs FetchVouchersbyPayId(string Projectid)
        //{
        //    try
        //    {
        //        resultArgs = DeleteVoucherTransPayVoucherbyCId(Projectid);
        //        if (resultArgs.Success && resultArgs.RowsAffected > 0)
        //        {
        //            resultArgs = DeleteVoucherMasterPayVoucherbyCId(Projectid);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.ToString(), true);
        //    }
        //    return resultArgs;
        //}
        public ResultArgs DeleteVoucherTransPayVoucherbyCId(string Pid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.DeleteVoucherTransPayrollVoucher))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(payLedger.PROCESS_REF_PAY_IDColumn, clsGeneral.PAYROLL_ID);
                dataManager.Parameters.Add(dtProjectStaff.PROJECT_IDColumn, Pid);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        public int FetchVoucherMasterPayVoucherbyCId(string Pid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchVoucherMasterPayrollVoucher))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(payLedger.PROCESS_REF_PAY_IDColumn, clsGeneral.PAYROLL_ID);
                dataManager.Parameters.Add(dtProjectStaff.PROJECT_IDColumn, Pid);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        public ResultArgs FetchmappedComponentsByProjectId(int Pid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchMappedComponentsbyProjectId))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(dtProjectStaff.PROJECT_IDColumn, Pid);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion
    }
}
