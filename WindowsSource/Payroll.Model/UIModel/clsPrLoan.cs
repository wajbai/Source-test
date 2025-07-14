using System;
using System.Collections.Generic;
using System.Text;
using Bosco.DAO.Data;
using Payroll.DAO.Schema;
using Bosco.Utility;
using System.Data;
using System.Windows.Forms;
using Bosco.Utility.Common;
using DevExpress.XtraEditors;

namespace Payroll.Model.UIModel
{
    public class clsPrLoan : SystemBase
    {
        ResultArgs resultArgs = null;
        clsLoanManagement loan = new clsLoanManagement();
        private DataTable rsLoanGet = new DataTable();
        ApplicationSchema.PayrollDataTable dtPayroll = null;
        #region Properties
        public int PRLoanGetId { get; set; }
        #endregion
        public clsPrLoan()
        {
            dtPayroll = this.AppSchema.Payroll;
            rsLoanGet = FetchLoanDetails();
        }

        public string RemoveTrailingSpace(string sString, int nCharPos)
        {
            if (sString.Trim() != "")
                return sString.Substring(0, sString.Length - nCharPos);
            else
                return sString;
        }
        public bool CheckDuplicate(string sTblName, string sWhereStr)
        {
            try
            {
                //dh.createDataSet("select * from " + sTblName + " where " + sWhereStr, "Check Duplicate Value");
                //return dh.getRecordCount() > 0;
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchFromTableWithWhere, "Check Duplicate Value"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    dataManager.Parameters.Add(dtPayroll.TABLENAMEColumn, sTblName);
                    dataManager.Parameters.Add(dtPayroll.CONDITIONColumn, sWhereStr);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
                return resultArgs.RowsAffected > 0;
            }
            catch
            {

                return false;
            }
        }
        public bool SaveLoanObtain(string sUpdateStr, long nLoanId)
        {
            int i;
            //string sWhere="";
            string[] aLoan;
            string[] aVal;
            string[] getaVal = new string[50];
            string[] getaField = new string[50];
            object sSql = "";
            try
            {

                aLoan = sUpdateStr.Split('@');
                sSql = "update prloanget set ";
                for (i = 0; i < aLoan.GetUpperBound(0); i++)
                {
                    aVal = aLoan[i].Split('|');

                    if (aVal.GetUpperBound(0) == 1)
                    {
                        getaVal[i] = (aVal[1] == null ? null : aVal[1]);
                        //System.Windows.Forms.MessageBox.Show(aVal[0]);
                        getaField[i] = aVal[0];
                        //sSql = sSql + getaField[i]+" = "+getaVal[i]+",";							   
                    }
                }

                //sSql = "update prloanget set staffid=<staffid>,loanid=<loanid>,amount=<amount>,installment=<installment>,fromdate=to_date('<fromdate>','dd/MM/YYYY'),todate=to_date('<todate>','dd/MM/YYYY'),interest=<interest>,intrestmode=<intrestmode>," +
                //      "intrestamount=<intrestamount>,currentinstallment=<currentinstallment>,completed=<completed> where prloangetid=" + nLoanId;


                //sSql = sSql.Replace("<staffid>", getaVal[0].ToString());
                //sSql = sSql.Replace("<loanid>", getaVal[1].ToString());
                //sSql = sSql.Replace("<amount>", getaVal[2].ToString());
                //sSql = sSql.Replace("<installment>", getaVal[3].ToString());
                //sSql = sSql.Replace("<fromdate>", Convert.ToDateTime(getaVal[4]).ToShortDateString());
                //if (getaVal[5] != "")
                //    sSql = sSql.Replace("<todate>", Convert.ToDateTime(getaVal[5]).ToShortDateString());
                //else
                //{
                //    sSql = sSql.Replace("to_date('<todate>','dd/MM/YYYY')", getaVal[5].ToString());
                //    sSql = sSql.Replace("todate=,", "");
                //}
                //sSql = sSql.Replace("<interest>", getaVal[6].ToString());
                //sSql = sSql.Replace("<intrestmode>", getaVal[7].ToString());
                //sSql = sSql.Replace("<intrestamount>", getaVal[8].ToString());
                //sSql = sSql.Replace("<currentinstallment>", getaVal[9].ToString());
                //sSql = sSql.Replace("<completed>", getaVal[10].ToString());

                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.PayrollLoanMntEdit))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    
                    dataManager.Parameters.Add(dtPayroll.STAFFIDColumn, getaVal[0].ToString());
                    dataManager.Parameters.Add(dtPayroll.LOANIDColumn, getaVal[1].ToString());
                    dataManager.Parameters.Add(dtPayroll.AMOUNTColumn, getaVal[2].ToString());
                    dataManager.Parameters.Add(dtPayroll.INSTALLMENTColumn, getaVal[3].ToString());
                    dataManager.Parameters.Add(dtPayroll.FROMDATEColumn, clsGeneral.GetMySQLDateTime(getaVal[4], DateDataType.DateTime));
                    if (getaVal[5] != "")
                        dataManager.Parameters.Add(dtPayroll.TODATEColumn, clsGeneral.GetMySQLDateTime(getaVal[5], DateDataType.DateTime));
                    else
                    {
                        dataManager.Parameters.Add(dtPayroll.TODATEColumn, getaVal[5].ToString());
                    }
                    dataManager.Parameters.Add(dtPayroll.INTERESTColumn, getaVal[6].ToString());
                    dataManager.Parameters.Add(dtPayroll.INTRESTMODEColumn, getaVal[7].ToString());
                    dataManager.Parameters.Add(dtPayroll.INTRESTAMOUNTColumn, getaVal[8].ToString());
                    dataManager.Parameters.Add(dtPayroll.CURRENTINSTALLMENTColumn, getaVal[9].ToString());
                    dataManager.Parameters.Add(dtPayroll.COMPLETEDColumn, getaVal[10].ToString());
                    dataManager.Parameters.Add(dtPayroll.PRLOANGETIDColumn, nLoanId);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.UpdateData();
                    PRLoanGetId =NumberSet.ToInteger(nLoanId.ToString());
                }
                //dh.insertRecord(sSql);
                //dh.createDataSet("select prloangetid, staffid, loanid, nvl(amount,0) as amount, " +
                //    "nvl(installment,0) as installment, to_char(fromdate, 'DD-Mon-YYYY') as fromdate, " +
                //    "to_char(todate, 'DD-Mon-YYYY') as todate, nvl(interest,0) as interest, " +
                //    "nvl(intrestmode,0) as intrestmode, nvl(intrestamount,0) as intrestamount, " +
                //    "nvl(currentinstallment,0) as currentinstallment, nvl(completed,0) as completed " +
                //    "from prloanget", "Loan");
                //using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollLoanDetails, "Loan"))
                //{
                //    resultArgs = dataManager.FetchData(DataSource.DataTable);
                //    if (resultArgs.Success)
                //        rsLoanGet = resultArgs.DataSource.Table;
                //}
                rsLoanGet = FetchLoanDetails();
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        private DataTable FetchLoanDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollLoanDetails, "Loan"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                    return resultArgs.DataSource.Table;
            }
            return null;
        }
        public bool SaveLoanObtain(string sUpdateStr, long nLoanId, long nLoanTypeId, long nStaffId)
        {
            int i;
            string sWhere = "";
            string[] aLoan;
            string[] aVal;
            string[] getaVal = new string[50];
            string[] getaField = new string[50];
            string sSql = "";
            try
            {
                sWhere = "staffid= " + nStaffId + " and loanid=" + nLoanTypeId + " " +
                       "and completed=" + (nLoanId > 0 ? " and prloangetid <> " + nLoanId : "0");

                //if (this.CheckDuplicate("prloanget", sWhere))
                //{
                //    MessageBox.Show("This Loan exists already.", "MedSysB", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return false;
                //}

                DataRow dr = rsLoanGet.NewRow();
                if (nLoanId == 0)
                {
                    rsLoanGet.Rows.Add(dr);
                }
                else
                {
                    if (rsLoanGet.Rows.Count > 0)
                    {
                        DataView dvLoanGet = new DataView(rsLoanGet);

                        dvLoanGet.RowFilter = "prloangetid =" + nLoanId;
                    }
                    if (rsLoanGet.Rows.Count == 0)
                        return false;
                }
                aLoan = sUpdateStr.Split('@');

                for (i = 0; i < aLoan.GetUpperBound(0); i++)
                {
                    aVal = aLoan[i].Split('|');
                    //aVal = Strings.Split(aLoan[i], "|");
                    //aVal = Strings.Split(aLoan[i],"|");
                    if (aVal.GetUpperBound(0) == 1)
                    {
                        getaVal[i] = (aVal[1] == null ? null : aVal[1]);
                        //System.Windows.Forms.MessageBox.Show(aVal[0]);
                        getaField[i] = aVal[0];

                        //sSql = "update prloanget set "+ getaField +" = "+getaVal +" where prloangetid="+nLoanId;  
                        //rsLoanGet.Tables[0].Rows.Add(aVal(1)="NULL",Null,aVal(1));
                        //dh.updateRecord(sSql);
                    }
                }
                //sSql = "insert into prloanget(prloangetid,staffid,loanid,amount,installment,fromdate,todate,interest,intrestmode,intrestamount,currentinstallment,completed)" +
                //    "values(scq_prloanmnt.nextval,<staffid>,<loanid>,<amount>,<installment>,to_date('<fromdate>','dd/MM/YYYY'),to_date('<todate>','dd/MM/YYYY'),<interest>,<intrestmode>,<intrestamount>,<currentinstallment>,<completed>)";


                //sSql ="insert into prloanget(prloangetid,staffid,loanid,amount,installment,fromdate,todate,interest,intrestmode,intrestamount,currentinstallment,completed)"+
                //	"values(scq_prloanmnt.nextval,"+getaVal[0]+","+getaVal[1]+","+getaVal[2]+","+getaVal[3]+",to_date('"+getaVal[4]+"','dd/MM/YYYY'),to_date('"+getaVal[5]+"','dd/MM/YYYY'),"+getaVal[6]+","+getaVal[7]+","+getaVal[8]+","+getaVal[9]+","+getaVal[10]+")";
                //ReplaceQuery();


                //sSql = sSql.Replace("<staffid>", getaVal[0].ToString());
                //sSql = sSql.Replace("<loanid>", getaVal[1].ToString());
                //sSql = sSql.Replace("<amount>", getaVal[2].ToString());
                //sSql = sSql.Replace("<installment>", getaVal[3].ToString());
                //sSql = sSql.Replace("<fromdate>", Convert.ToDateTime(getaVal[4]).ToShortDateString());
                //if (getaVal[5] != "")
                //    sSql = sSql.Replace("<todate>", Convert.ToDateTime(getaVal[5]).ToShortDateString());
                //else
                //{
                //    sSql = sSql.Replace("to_date('<todate>','dd/MM/YYYY'),", getaVal[5].ToString());
                //    sSql = sSql.Replace("todate,", "");
                //}
                //sSql = sSql.Replace("<interest>", getaVal[6].ToString());
                //sSql = sSql.Replace("<intrestmode>", getaVal[7].ToString());
                //sSql = sSql.Replace("<intrestamount>", getaVal[8].ToString());
                //sSql = sSql.Replace("<currentinstallment>", getaVal[9].ToString());
                //sSql = sSql.Replace("<completed>", getaVal[10].ToString());

                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.PayrollLoanMntAdd))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtPayroll.PRLOANGETIDColumn, true);
                    dataManager.Parameters.Add(dtPayroll.STAFFIDColumn, getaVal[0].ToString());
                    dataManager.Parameters.Add(dtPayroll.LOANIDColumn, getaVal[1].ToString());
                    dataManager.Parameters.Add(dtPayroll.AMOUNTColumn, getaVal[2].ToString());
                    dataManager.Parameters.Add(dtPayroll.INSTALLMENTColumn, getaVal[3].ToString());
                    dataManager.Parameters.Add(dtPayroll.FROMDATEColumn, clsGeneral.GetMySQLDateTime(getaVal[4], DateDataType.DateTime));
                    //if (getaVal[5] != "")
                    dataManager.Parameters.Add(dtPayroll.TODATEColumn, clsGeneral.GetMySQLDateTime(getaVal[5], DateDataType.DateTime));
                    //else
                    //{
                    //    dataManager.Parameters.Add(dtPayroll.TODATEColumn, getaVal[5].ToString());
                    //}
                    dataManager.Parameters.Add(dtPayroll.INTERESTColumn, getaVal[6].ToString());
                    dataManager.Parameters.Add(dtPayroll.INTRESTMODEColumn, getaVal[7].ToString());
                    dataManager.Parameters.Add(dtPayroll.INTRESTAMOUNTColumn, getaVal[8].ToString());
                    dataManager.Parameters.Add(dtPayroll.CURRENTINSTALLMENTColumn, getaVal[9].ToString());
                    dataManager.Parameters.Add(dtPayroll.COMPLETEDColumn, getaVal[10].ToString());
                    resultArgs = dataManager.UpdateData();
                    PRLoanGetId =NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                }
                
                //dh.insertRecord(sSql);
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollLoanDetails, "Loan"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                        rsLoanGet = resultArgs.DataSource.Table;
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }
        /*
         * Purpose    : This method is used to Delete the Selected Loan Get details.
         * Argument   : nId as long
         *              for the Selected Loan Information
         * */
        public bool DeleteLoanObtain(long nloangetid)
        {
            bool rtn = true; 
            object sDelSQL = "";
            if (CheckDuplicate("prloanpaid", "prloangetid = " + nloangetid))
            {
                //MessageBox.Show("Loan is not paid back fully,so it can not be deleted!", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //XtraMessageBox.Show("Loan is not paid back fully,so it can not be deleted!", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (XtraMessageBox.Show("Few Loan EMI(s) have been paid, Do you want still to delete Loan and its all EMI(s) ?", "Payroll", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    rtn = false;
                }
            }
            if (rtn)
            {
                //sDelSQL = "delete from prloanget where prloanget.prloangetid = " + nLoanId;
                sDelSQL = SQLCommand.Payroll.DeletePayrollLoan;
                rtn = insertRecord(sDelSQL, nloangetid);
                if (rtn)
                {
                    sDelSQL = SQLCommand.Payroll.DeletePrLoanPaidbyLoanId;
                    rtn = deleteLoanPaidByLoanId(sDelSQL, nloangetid);
                }
            }
            return rtn;
        }

        private bool insertRecord(object squery, long nLoanId)
        {
            using (DataManager dataManager = new DataManager(squery))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtPayroll.PRLOANGETIDColumn, nLoanId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs.Success;
        }

        private bool deleteLoanPaidByLoanId(object squery, long LoanGetId)
        {
            using (DataManager dataManager = new DataManager(squery))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtPayroll.PRLOANGETIDColumn, LoanGetId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs.Success;
        }

        public ResultArgs DeleteVoucherMasterTransByLoangetid(int PrloangetId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.DeleteVoucherMasterTransByPrloanGetId))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtPayroll.PRLOANGETIDColumn, PrloangetId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        public ResultArgs DeleteVoucherTransByLoangetId(int PrloangetId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.DeleteVoucherTransByPrloanGetId))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtPayroll.PRLOANGETIDColumn, PrloangetId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        private DataTable FetchRecords(object squery, string tableName, long nLoanId, string Completed)
        {
            using (DataManager dataManager = new DataManager(squery))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtPayroll.PRLOANGETIDColumn, nLoanId);
                dataManager.Parameters.Add(dtPayroll.COMPLETEDColumn, Completed);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                {
                    resultArgs.DataSource.Table.TableName = tableName;
                    return resultArgs.DataSource.Table;
                }
            }
            return null;
        }

        private DataTable FetchRecords(object squery, string tableName)
        {
            using (DataManager dataManager = new DataManager(squery))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                {
                    resultArgs.DataSource.Table.TableName = tableName;
                    return resultArgs.DataSource.Table;
                }
            }
            return null;
        }

        public string getCurrentInstallment(int iLoanGetId, int iLoanId)
        {
            //return dh.ExecuteScalar("select t.currentinstallment from prloanget t where t.prloangetid =" + iLoanGetId + " and t.loanid=" + iLoanId + " ");
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchCurrentInstallment))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                if (resultArgs.Success)
                    return resultArgs.DataSource.Sclar.ToString;
            }
            return string.Empty;
        }
        public DataTable GetLoanSQL(bool bActiveLoan, string table)
        {
            string sSql = "";// sName = "" ,sloanDate = "", iIns = ""  sWhere = ""
            //PayrollFetchDetailsBrowse
            string Completed = (bActiveLoan == true) ? "0" : "1";
            //sSql = "select " +
            //            "prloanget.prloangetid as \"PRLOANGETID\", stfpersonal.empno as \"STAFFID\", " +
            //            "stfpersonal.firstname || ' ' || stfpersonal.lastname as \"Name\", " +
            //            "prloanget.amount as AMOUNT,prloanget.installment,prloanget.interest," +
            //            " to_char(prloanget.fromdate, 'DD/MM/YYYY') as \"Pay From\", " +
            //            "to_char(prloanget.todate, 'DD/MM/YYYY') as \"Pay To\",prloanget.intrestmode,nvl(prloanget.currentinstallment,0) || '/' || nvl(prloanget.installment,0) as Installments, " +
            //            " hospital_departments.hdept_desc as \"Department\",prloan.loanname as Loan, " +
            //            "nvl(prloanget.completed,0) as Completed " +
            //        "from " +
            //            "prloan, prloanget, stfpersonal, hospital_departments " +
            //        "where " +
            //            "prloanget.completed = " + (bActiveLoan == true ? "0" : "1") + " and " +
            //            "prloan.loanid = prloanget.loanid and stfpersonal.staffid = prloanget.staffid and " +
            //            "stfpersonal.deptid = hospital_departments.hdept_id " +
            //        "order by " +
            //            "stfpersonal.firstname || ' ' || stfpersonal.lastname";

            return FetchRecords(SQLCommand.Payroll.PayrollFetchDetailsBrowse, table, 0, Completed);
        }

        public DataTable GetStaffLoanTypeSQL()
        {
            //string sSql = "";
            // FetchStaffLoanType
            //sSql = "select " +
            //            "prloanget.prloangetid as LoanId, " +
            //            "stfpersonal.firstname || ' ' || stfpersonal.lastname || ' - ' || prloan.loanname " +
            //            "as \"Staff Name\" " +
            //        "from " +
            //            "prloan, prloanget, stfpersonal " +
            //        "where " +
            //            "prloanget.completed = 0 and prloan.loanid = prloanget.loanid " +
            //            "and stfpersonal.staffid = prloanget.staffid " +
            //        "order by " +
            //            "stfpersonal.firstname || ' ' || stfpersonal.lastname || ' - ' || prloan.loanname";

            return FetchRecords(SQLCommand.Payroll.FetchStaffLoanType, " ");
        }

        public DataTable GetLoanPaidSQL(long nLoanId)
        {
            //string sSql = "";
            // FetchLoanPaid
            //sSql = "select " +
            //            "prcreate.prname as Payroll, to_char(prloanpaid.paiddate,'DD-Mon-YYYY') as \"Paid on\", " +
            //            "nvl(prloanpaid.amount,0) as \"Amount\", " +
            //            "nvl(prloanpaid.installment, '') || '/' || nvl(prloanget.installment, '') as Installment " +
            //        "from " +
            //            "prloanpaid, prloanget, prcreate " +
            //        "where " +
            //            "prloanget.prloangetid = prloanpaid.prloangetid and " +
            //            "prloanpaid.payrollid = prcreate.payrollid and " +
            //            "prloanget.prloangetid = " + nLoanId;

            return FetchRecords(SQLCommand.Payroll.FetchLoanPaid, "", nLoanId, "");
        }

        public DataTable GetStaffSQL()
        {
            // FetchStaff
            string sSql = "select staffid, stfpersonal.firstname || ' ' || stfpersonal.lastname as \"Staff Name\", " +
                            "from stfpersonal " +
                            "where staffid > 0 and leavingdate is null " +
                            "order by stfpersonal.firstname || ' ' || stfpersonal.lastname";
            return FetchRecords(SQLCommand.Payroll.FetchStaff, "");
        }

        public DataTable GetLoanTypeSQL()
        {
            //FetchLoanType
            //string sSql = "select loanid, loanname as \"Loan Type\" " +
            //                "from prloan " +
            //                "where loanid > 0";
            return FetchRecords(SQLCommand.Payroll.FetchLoanType, " ");
        }
        public ResultArgs FetchLoantaffDetailsById(int StaffId)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.Payroll.FetchLoanStaffDetailsByStaffId))
            {
                datamanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                datamanager.Parameters.Add(dtPayroll.STAFFIDColumn, StaffId);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
    }
}
