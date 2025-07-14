using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Bosco.Utility.Common;
using Bosco.DAO.Data;
using Payroll.DAO.Schema;
using Bosco.Utility;
using System.Windows.Forms;

namespace Payroll.Model.UIModel
{
    public class clsAllocateComponent:SystemBase
    {
        clsPrComponent objComponent = new clsPrComponent();
        ApplicationSchema.PRCOMPMONTHDataTable dtcomp = null;
        ResultArgs resultArgs = null;
        private object sQuery = "";
        private string tableName = "";
        public clsAllocateComponent()
        {
            dtcomp = this.AppSchema.PRCOMPMONTH;
        }
        private string type = "";
        private string defvalue = "";
        private string lnkvalue = "";
        private string equation = "";
        private string equationid = "";
        private int comporder = 0;
        private int compround = 0;
        private int ifcondition = 0;
        private int maxslap = 0;
        public DataTable getCompDetails(long getCompId)
        {
            sQuery = objComponent.getPayrollComponentAllocateQry(clsPayrollConstants.PAYROLL_FULLCOMP_LIST);
           // sQuery = sQuery.Replace("<componentid>", getCompId.ToString());
            tableName = "Component";
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.PayrollFullCompList))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(dtcomp.COMPONENTIDColumn, getCompId.ToString());
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                    return resultArgs.DataSource.Table;
            }
            return null;
        }
        public int InsertSelect(long igetId, long iCompId, int iGroupId)
        {
            sQuery = objComponent.getPayrollComponentAllocateQry(clsPayrollConstants.PAYROLL_COMPCHECK_SELECT);
            //sQuery = sQuery.Replace("<payrollid>", igetId.ToString());
            //sQuery = sQuery.Replace("<compid>", iCompId.ToString());
            //sQuery = sQuery.Replace("<groupid>", iGroupId.ToString());
            tableName = "Component";
            try
            {
               DataTable dtComp = objComponent.FetchcompCheckSelect(sQuery, igetId, iCompId, iGroupId);
                if (dtComp!=null && dtComp.Rows.Count > 0)
                    return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return 0;
        }
        public int DeleteInserted(long igetId, long iCompId, int iGroupId)
        {
            sQuery = objComponent.getPayrollComponentAllocateQry(clsPayrollConstants.PAYROLL_COMP_DELETE);
            tableName = "Component";
            try
            {
                using (DataManager dataManager = new DataManager(sQuery, tableName))
                {
                    dataManager.Parameters.Add(dtcomp.PAYROLLIDColumn, igetId.ToString());
                    dataManager.Parameters.Add(dtcomp.SALARYGROUPIDColumn, iGroupId.ToString());
                    dataManager.Parameters.Add(dtcomp.COMPONENTIDColumn, iCompId.ToString());
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    resultArgs = dataManager.UpdateData();
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        return 1;
                    else
                        return 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return 0;
        }

    }
}
