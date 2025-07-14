using System;
using System.Collections.Generic;
using System.Text;
using Bosco.Utility.Common;
using Payroll.DAO.Schema;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Payroll.Model.UIModel
{
    public class clsPayrollLoan : SystemBase
    {
        private Int32 iLoanId = 0;
        private string sLoanName = "";
        private string sLoanAbbri = "";
        private bool bEditMode = false;
        private object sQuery = "";
        //private int affectedRows		=0; //Commented by PE to avoid warning 
        private string tableName = "Loan";
        private int iCount = 0;

        ResultArgs resultArgs = null;
        ApplicationSchema.PayrollDataTable dtLoan = null;
        public clsPayrollLoan()
        {
            dtLoan = this.AppSchema.Payroll;

        }
        public Int32 CommonId
        {
            get { return iLoanId; }
            set { iLoanId = value; }
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
        public string LoanAbbrivation
        {
            get { return sLoanAbbri; }
            set { sLoanAbbri = value; }
        }
        public bool EditMode
        {
            get { return bEditMode; }
            set { bEditMode = value; }
        }
        public int savePayrollLoanData(int fetch_SP_Option)
        {
            return savePayrollLoanData(fetch_SP_Option, 0);
        }
        public int savePayrollLoanData(int fetch_SP_Option, int fetch_ID_toDel)
        {
            if (fetch_SP_Option == clsPayrollConstants.PAYROLL_LOAN_DELETE)
            {
                sQuery = getPayrollLoanQry(fetch_SP_Option);
                using (DataManager dataManager = new DataManager(sQuery))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtLoan.LOANIDColumn, Convert.ToString(fetch_ID_toDel));
                    resultArgs = dataManager.UpdateData();
                }
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    return 1;
            }
            else
            {
                sQuery = CommonId == 0 ? getPayrollLoanQry(clsPayrollConstants.PAYROLL_LOAN_INSERT) : getPayrollLoanQry(clsPayrollConstants.PAYROLL_LOAN_EDIT);
                using (DataManager dataManger = new DataManager(sQuery))
                {
                    dataManger.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManger.Parameters.Add(dtLoan.LOANIDColumn, Convert.ToString(CommonId));
                    dataManger.Parameters.Add(dtLoan.LOANNAMEColumn, LoanName);
                    dataManger.Parameters.Add(dtLoan.LOANABBRIVIATIONColumn, LoanAbbrivation);
                    resultArgs = dataManger.UpdateData();
                }
            //// chinna 
            ////sQuery = getPayrollLoanQry(fetch_SP_Option);
            //sQuery = CommonId == 0 ? getPayrollLoanQry(clsPayrollConstants.PAYROLL_LOAN_INSERT) : getPayrollLoanQry(clsPayrollConstants.PAYROLL_LOAN_EDIT);
            //using (DataManager dataManager = new DataManager(sQuery))
            //{
            //    //if (fetch_SP_Option == clsPayrollConstants.PAYROLL_LOAN_DELETE)
            //    //{
            //    //    //sQuery = sQuery.Replace("<loanid>", Convert.ToString(fetch_ID_toDel));
            //    //    dataManager.Parameters.Add(dtLoan.LOANIDColumn, Convert.ToString(fetch_ID_toDel));
            //    //}
            //    //else
            //    //{
            //    dataManager.Parameters.Add(dtLoan.LOANIDColumn, Convert.ToString(CommonId));
            //    dataManager.Parameters.Add(dtLoan.LOANNAMEColumn, LoanName);
            //    dataManager.Parameters.Add(dtLoan.LOANABBRIVIATIONColumn, LoanAbbrivation);

                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    return 1;
            }
            return 0;
        }
        private void ModifyQuery()
        {
            //sQuery = sQuery.Replace("<loanid>", Convert.ToString(CommonId));
            //sQuery = sQuery.Replace("<loanname>", LoanName);
            //sQuery = sQuery.Replace("<loanabbriviation>", LoanAbbrivation);
        }
        public DataTable getPayrollLoanList()
        {
            //if (objDBHand.createDataSet(getPayrollLoanQry(PAYROLL_LOAN_LIST), tableName) == null)
            //{
            //    iCount = objDBHand.getRecordCount();
            //    return objDBHand.getDataSet();
            //}
            using (DataManager dataManager = new DataManager(getPayrollLoanQry(clsPayrollConstants.PAYROLL_LOAN_LIST), tableName))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                {
                    iCount = resultArgs.DataSource.Table.Rows.Count;
                    resultArgs.DataSource.Table.TableName = tableName;
                    return resultArgs.DataSource.Table;
                }
            }
            return null;
        }
        public static object getPayrollLoanQry(int iQueryId)
        {
            object sQry = "";
            switch (iQueryId)
            {
                case clsPayrollConstants.PAYROLL_LOAN_LIST:
                    sQry = SQLCommand.Payroll.PayrollLoanList;
                    break;
                case clsPayrollConstants.PAYROLL_LOAN_INSERT:
                    sQry = SQLCommand.Payroll.PayrollLoanInsert;
                    break;
                case clsPayrollConstants.PAYROLL_LOAN_OCCUR:
                    sQry = SQLCommand.Payroll.PayrollLoanOccur;
                    break;
                case clsPayrollConstants.PAYROLL_LOAN_EDIT:
                    sQry = SQLCommand.Payroll.PayrollLoanEdit;
                    break;
                case clsPayrollConstants.PAYROLL_LOAN_DELETE:
                    sQry = SQLCommand.Payroll.PayrollLoanDelete;
                    break;
                case clsPayrollConstants.PAYROLL_LOAN_DETAILS:
                    sQry = SQLCommand.Payroll.PayRollLoanDetail;
                    break;
            }
            return sQry;
        }
        public bool checkLoanDuplicate(string getLoanName)
        {
            sQuery = getPayrollLoanQry(clsPayrollConstants.PAYROLL_LOAN_OCCUR);
            //sQuery = sQuery.Replace("<loanname>", getLoanName);
            try
            {
                using (DataManager dataManager = new DataManager(sQuery, getLoanName))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtLoan.LOANNAMEColumn, getLoanName);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }

                //objDBHand.createDataSet(sQuery, tableName);
                if (resultArgs.RowsAffected > 0)
                {
                    return true;
                }
            }
            catch { }
            return false;
        }
        public void getPayrollLoanDetails()
        {
            DataTable dtTable = null;
            if (Convert.ToString(CommonId) != "")
            {
                sQuery = getPayrollLoanQry(clsPayrollConstants.PAYROLL_LOAN_DETAILS);

                try
                {
                    //objDBHand.createDataSet(sQuery, tableName);
                    //LoanName = objDBHand.getData(0, "LoanName");
                    //LoanAbbrivation = objDBHand.getData(0, "LoanAbbriviation");
                    using (DataManager dataManager = new DataManager(sQuery, tableName))
                    {
                        dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        dataManager.Parameters.Add(dtLoan.LOANIDColumn, Convert.ToString(CommonId));
                        dataManager.Parameters.Add(dtLoan.LOANNAMEColumn, LoanName);
                        dataManager.Parameters.Add(dtLoan.LOANABBRIVIATIONColumn, LoanAbbrivation);

                        resultArgs = dataManager.FetchData(DataSource.DataTable);
                    }
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        dtTable = resultArgs.DataSource.Table;
                        LoanName = dtTable.Rows[0]["LoanName"].ToString();
                        LoanAbbrivation = dtTable.Rows[0]["LoanAbbriviation"].ToString();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }
        public void getPayrollLoanCount()
        {
            //if (objDBHand.createDataSet(getPayrollLoanQry(PAYROLL_LOAN_LIST), tableName) == null)
            //{
            //    iCount = objDBHand.getRecordCount();
            //}

            using (DataManager dataManager = new DataManager(getPayrollLoanQry(clsPayrollConstants.PAYROLL_LOAN_LIST), tableName))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            if (resultArgs.Success)
            {
                iCount = resultArgs.DataSource.Table.Rows.Count;
            }
        }
    }
}
