using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using Payroll.DAO.Schema;
using Bosco.Utility.Common;

namespace Payroll.Model.UIModel
{
    public class clsLoanManagement : SystemBase
    {
        ApplicationSchema.PayrollDataTable dtPayroll = new ApplicationSchema.PayrollDataTable();
        ApplicationSchema.PAYROLL_PROJECTDataTable dtPayrollproject = new ApplicationSchema.PAYROLL_PROJECTDataTable();
        ApplicationSchema.LedgerDataTable dtLedger = new ApplicationSchema.LedgerDataTable();

        CommonMember UtilityMember = new CommonMember();
        DataTable dtcashBank = new DataTable();
        public clsLoanManagement()
        {
            dtPayroll = this.AppSchema.Payroll;
        }
        public clsLoanManagement(int id)
        {
            FillLoanMgt(id);
        }

        private object strQuery = "";
        public int prloangetid;
        public int StaffId;
        public int LoanId;
        public int Amount;
        public int NoInstallment;
        public double IntrestRate;
        public DateTime PayFrom;
        public DateTime PayTo;
        public int IntrestMode;
        public int IntrestAmount;
        public int CurrentInstallment;
        public int Completed;
        public int CashBank;

        public int prloanid
        {
            set { this.prloangetid = value; }
            get { return this.prloangetid; }
        }
        public int Staff
        {
            set { this.StaffId = value; }
            get { return this.StaffId; }
        }
        public int Loan
        {
            set { this.LoanId = value; }
            get { return this.LoanId; }
        }
        public int Amt
        {
            set { this.Amount = value; }
            get { return this.Amount; }
        }
        public int Installment
        {
            set { this.NoInstallment = value; }
            get { return this.NoInstallment; }
        }
        public double Intrest
        {
            set { this.IntrestRate = value; }
            get { return this.IntrestRate; }
        }
        public int IntrestAmt
        {
            set { this.IntrestAmount = value; }
            get { return this.IntrestAmount; }
        }
        public DateTime From
        {
            set { this.PayFrom = value; }
            get { return this.PayFrom; }
        }
        public DateTime To
        {
            set { this.PayTo = value; }
            get { return this.PayTo; }
        }
        public int Mode
        {
            set { this.IntrestMode = value; }
            get { return this.IntrestMode; }
        }
        public int CurrentInst
        {
            set { this.CurrentInstallment = value; }
            get { return this.CurrentInstallment; }
        }
        public int Complete
        {
            set { this.Completed = value; }
            get { return this.Completed; }
        }

        /// <summary>
        /// On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
        /// 
        /// This date is apart from balance date. If we take reports for given period (DATE_FROM and DATE_TO)
        /// When we show closing balance for DATE_TO, we have to check bank ledger closed date for DATE_FROM
        /// </summary>
        public string BankClosedDate { get; set; }

        ResultArgs resultArgs = null;
        public void getPayrollLoanManagementLoan(ComboBox cboName)
        {
            // string strSql = "SELECT LOANID,LOANNAME FROM PRLOAN";
            object strSql = SQLCommand.Payroll.PayrollLoan;
            this.fillList(cboName, "loan", strSql, AppSchema.STFPERSONAL.FIRSTNAMEColumn.ColumnName, AppSchema.STFPERSONAL.STAFFIDColumn.ColumnName);
        }
        public void getPayrollLoanManagementStaff(ComboBox cboName)
        {
            //string strSql = "SELECT t.staffid AS \"STAFF ID\",t.firstname||' '||t.lastname AS \"FIRST NAME\"" +
            //                " FROM stfpersonal t WHERE t.staffid <> 0 and (t.leavingdate is null or t.leavingdate >= TO_DATE('" + DateTime.Parse(clsGeneral.getServerDateTime(),clsGeneral.DATE_FORMAT).ToShortDateString() + "','DD/MM/YYYY')) "+
            //                "and t.dateofjoin <= TO_DATE('" + DateTime.Parse(clsGeneral.getServerDateTime(),clsGeneral.DATE_FORMAT).ToShortDateString() + "','DD/MM/YYYY') order by \"FIRST NAME\"";
            //string strSql = "SELECT t.staffid AS \"STAFF ID\",t.firstname||' '||t.lastname AS \"FIRST NAME\"" +
            //                " FROM stfpersonal t WHERE t.staffid <> 0 and (t.leavingdate is null or t.leavingdate >= TO_DATE('" + clsGeneral.getServerDateyyyyFormat() + "','DD/MM/YYYY')) " +
            //                "and t.dateofjoin <= TO_DATE('" + clsGeneral.getServerDateyyyyFormat() + "','DD/MM/YYYY') order by \"FIRST NAME\"";

            object strSql = SQLCommand.Payroll.PayrollLoanManagementStaff;
            this.fillList(cboName, "staff", strSql, AppSchema.STFPERSONAL.FIRSTNAMEColumn.ColumnName, AppSchema.STFPERSONAL.STAFFIDColumn.ColumnName);

        }
        public ResultArgs GetStaff()
        {
            object query = SQLCommand.Payroll.PayrollLoanMgtDetails;
            using (DataManager dataManger = new DataManager(query))
            {
                dataManger.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManger.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs GetLoan()
        {
            object query = SQLCommand.Payroll.PayrollLoan;
            using (DataManager dataManger = new DataManager(query))
            {
                dataManger.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManger.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs GetLoanDetails()
        {
            object query = SQLCommand.Payroll.PayrollLoanDetailfroComponent;
            using (DataManager dataManger = new DataManager(query))
            {
                dataManger.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManger.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs GetIncomeDetails(bool allEarnings=false)
        {
            object query = SQLCommand.Payroll.PayrollIncomeforComponent;
            using (DataManager dataManager = new DataManager(query))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            //On 22/02/2022, to load all earninngs, for earnining type, remove YOS
            if (!allEarnings)
            {
                if (resultArgs != null && resultArgs.Success)
                {
                    DataTable dtEarninings =  resultArgs.DataSource.Table;
                    dtEarninings.DefaultView.RowFilter = "INCOME_NAME NOT IN ('" + PayRollExtraPayInfo.YOS.ToString() + "')";
                    resultArgs.DataSource.Data =   dtEarninings.DefaultView.ToTable();
                }
            }

            return resultArgs;
        }
        public ResultArgs GetTextValues(bool allTextLink=false)
        {
            object query = SQLCommand.Payroll.PayrollTextvalforComponent;
            using (DataManager datamanager = new DataManager(query))
            {
                datamanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }

            //On 22/02/2022, to load all text links, except scale of pay
            if (!allTextLink)
            {
                if (resultArgs != null && resultArgs.Success)
                {
                    DataTable dtEarninings = resultArgs.DataSource.Table;
                    dtEarninings.DefaultView.RowFilter = "INCOME_NAME NOT IN ('" + PayRollExtraPayInfo.SCALEOFPAY.ToString() + "')";
                    resultArgs.DataSource.Data = dtEarninings.DefaultView.ToTable();
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// On 18/02/2022, To list all link values combining income and expense link values
        /// 
        /// </summary>
        /// <returns></returns>
        public ResultArgs GetAllIncomeExpensesLinkValues()
        {
            try
            {
                resultArgs = GetIncomeDetails(true);
                if (resultArgs.Success)
                {
                    DataTable dtAllEDLinkvalues = resultArgs.DataSource.Table;
                    resultArgs = GetLoanDetails();
                    if (resultArgs.Success)
                    {
                        DataTable dtAllDeductionLinkvalues = resultArgs.DataSource.Table;
                        Int32 MaxIncomelinkid = UtilityMember.NumberSet.ToInteger(dtAllEDLinkvalues.Compute("MAX([INCOME_ID])", string.Empty).ToString());
                        foreach (DataRow dr in dtAllDeductionLinkvalues.Rows)
                        {
                            MaxIncomelinkid += 1;
                            DataRow drExpense = dtAllEDLinkvalues.NewRow();
                            drExpense["INCOME_ID"] = MaxIncomelinkid;
                            drExpense["INCOME_NAME"] = dr["INCOME_NAME"].ToString();
                            dtAllEDLinkvalues.Rows.Add(drExpense);
                        }
                    }

                    resultArgs.DataSource.Data = dtAllEDLinkvalues;
                    resultArgs.Success = true;
                }
            }
            catch (Exception err)
            {
                resultArgs.Message = err.Message;
            }
            return resultArgs;
        }


        /// <summary>
        /// On 21/02/2022, To get actual link value id by combining income and expense link values
        /// </summary>
        /// <param name="linkvalues"></param>
        /// <returns></returns>
        public Int32 GetActualLinkValueId(string linkvalues)
        {
            Int32 rtn = 0;
            resultArgs =  GetAllIncomeExpensesLinkValues();
            if (resultArgs.Success)
            {
                DataTable dtLink = resultArgs.DataSource.Table;
                if (dtLink.Rows.Count > 0)
                {
                    dtLink.DefaultView.RowFilter = "INCOME_NAME = '" + linkvalues + "'";
                    if (dtLink.DefaultView.Count > 0)
                    {
                        rtn = UtilityMember.NumberSet.ToInteger(dtLink.DefaultView[0]["INCOME_ID"].ToString());
                    }
                }
            }

            return rtn;
        }

        public ResultArgs FetchCashBankLedgersofpayrollProjects(int Projectid)
        {
            object query = SQLCommand.Payroll.FetchMappedProjectCashBankLedgers;
            using (DataManager dataManager = new DataManager(query))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtPayrollproject.PROJECT_IDColumn, Projectid);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                if (!string.IsNullOrEmpty(BankClosedDate))
                {
                    dataManager.Parameters.Add(dtLedger.BANK_CLOSED_DATEColumn, BankClosedDate);
                }
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FillLoanMgt(int Id)
        {
            resultArgs = FetchLoanMgtId(Id);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                string[] strSplit = resultArgs.DataSource.Table.Rows[0]["Installments"].ToString().Split('/');
                CurrentInstallment = this.NumberSet.ToInteger(strSplit[0]);
                StaffId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["STAFF_ID"].ToString());
                LoanId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["LOAN_ID"].ToString());
                Amount = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][dtPayroll.AMOUNTColumn.ColumnName].ToString());
                NoInstallment = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][dtPayroll.INSTALLMENTColumn.ColumnName].ToString());
                IntrestRate = this.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0]["Rate_of_Interest"].ToString());
                PayFrom = this.UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["Pay_From"].ToString(), false);
                IntrestMode = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["intrestmode"].ToString());

                //if (PayTo == DateTime.MinValue)
                //{
                //    PayTo = Convert.ToDateTime(null); // Convert.ToDateTime(resultArgs.DataSource.Table.Rows[0]["Pay_To"].ToString());
                //}
                //else
                //{
                PayTo = UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["Pay_To"].ToString(), false);
                if (SettingProperty.PayrollFinanceEnabled == true)
                {
                    dtcashBank = FetchCashBankLedgersofSelectedStaff(Id).DataSource.Table;
                    if (dtcashBank != null && dtcashBank.Rows.Count > 0)
                    {
                        CashBank = NumberSet.ToInteger(dtcashBank.Rows[0]["LEDGER_ID"].ToString());
                    }
                }
                else
                {
                    CashBank = 0;
                }
                //}
                //Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.NAMEColumn.ColumnName].ToString();
                //Type = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.TYPEColumn.ColumnName].ToString());
                //Place = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.PLACEColumn.ColumnName].ToString();
                //CompanyName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.COMPANY_NAMEColumn.ColumnName].ToString();
                //CountryId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.COUNTRY_IDColumn.ColumnName].ToString());
                //Pincode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.PINCODEColumn.ColumnName].ToString();
                //Phone = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.PHONEColumn.ColumnName].ToString();
                //Email = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.EMAILColumn.ColumnName].ToString();
                //IdentityKey = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.IDENTITYKEYColumn.ColumnName].ToString());
                //Fax = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.FAXColumn.ColumnName].ToString();
                //URL = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.URLColumn.ColumnName].ToString();
                //StateId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.STATE_IDColumn.ColumnName].ToString());
                //Address = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.ADDRESSColumn.ColumnName].ToString();
                //Notes = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.NOTESColumn.ColumnName].ToString();
                //PAN = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.PANColumn.ColumnName].ToString();

            }
            return resultArgs;
        }

        public ResultArgs FetchLoanMgtId(int id)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.PayrollFetchAssignDetiailBrowse))
            {
                long IdData = id;
                //long value = 4;
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtPayroll.PRLOANGETIDColumn, IdData);
                dataManager.Parameters.Add(dtPayroll.COMPLETEDColumn, Completed.ToString());
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchCashBankLedgersofSelectedStaff(int LoanGetId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchCashBankLedgersofSelectedStaff))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtPayroll.PRLOANGETIDColumn, LoanGetId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private void fillList(ComboBox lst, string strTable,
           object strQuery, string strDisplayMember, string strValueMember)
        {
            DataTable objDS = new DataTable();
            DataView objDV = new DataView();
            //if (createDataSet(strQuery, strTable) == null)
            //    objDS = getDataSet();
            //else
            //    return;
            using (DataManager dataManager = new DataManager(strQuery, strTable))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtPayroll.LEAVINGDATEColumn, clsGeneral.GetMySQLDateTime(PayrollSystem.getServerDateTime(), DateDataType.Date));
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                    objDS = resultArgs.DataSource.Table;
            }
            objDV = new DataView(objDS);
            objDV.RowStateFilter = DataViewRowState.OriginalRows;

            lst.DisplayMember = strDisplayMember;
            lst.ValueMember = strValueMember;
            lst.DataSource = objDV;
            objDS.Dispose();
        }
        public bool InsertPayrollLoanMnt()
        {
            //insert into prloanget(prloangetid,staffid,loanid,amount,installment,fromdate,todate,interest,intrestmode,intrestamount,currentinstallment,completed)
            //values(scq_prloanmnt.nextval,<staffid>,<loanid>,<amount>,<installment>,'<fromdate>','<todate>',<interest>,<intrestmode>,<intrestamount>,<currentinstallment>,<completed>)
            strQuery = getPayrollLoanMntQuery(clsPayrollConstants.PAYROLL_LOAN_MNT_ADD);
            ReplaceQuery();
            return insertRecord(strQuery);

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
        private void ReplaceQuery()
        {
            //strQuery = strQuery.Replace("<prloangetid>", prloangetid.ToString());
            //strQuery = strQuery.Replace("<staffid>", StaffId.ToString());
            //strQuery = strQuery.Replace("<loanid>", LoanId.ToString());
            //strQuery = strQuery.Replace("<amount>", Amount.ToString());
            //strQuery = strQuery.Replace("<installment>", NoInstallment.ToString());
            //strQuery = strQuery.Replace("<fromdate>", PayFrom.ToShortDateString());
            //strQuery = strQuery.Replace("<todate>", PayTo.ToShortDateString());
            //strQuery = strQuery.Replace("<interest>", IntrestRate.ToString());
            //strQuery = strQuery.Replace("<intrestmode>", IntrestMode.ToString());
            //strQuery = strQuery.Replace("<intrestamount>", IntrestAmount.ToString());
            //strQuery = strQuery.Replace("<currentinstallment>", CurrentInstallment.ToString());
            //strQuery = strQuery.Replace("<completed>", Completed.ToString());
        }
        public bool UpdatePayrollLoanMnt()
        {
            strQuery = getPayrollLoanMntQuery(clsPayrollConstants.PAYROLL_LOAN_MNT_EDIT);
            ReplaceQuery();
            return insertRecord(strQuery);

        }
        public string getStaffRetirementDate(string sStaffId)
        {
            //return ExecuteScalar("SELECT S.RETIREMENTDATE FROM STFPERSONAL S WHERE STAFFID =" + sStaffId);
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.GetStaffRetirementDate))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add("STAFFID", sStaffId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToString;
        }
        public static object getPayrollLoanMntQuery(int iConstId)
        {
            object strQuery = "";
            switch (iConstId)
            {
                case clsPayrollConstants.PAYROLL_LOAN_MNT_LIST:
                    //strQuery="select t.prloangetid,t.staffid,t.loanid,t.amount,t.installment,t.interest,t.fromdate,t.todate,t.intrestmode,t.currentinstallment || '/' || t.installment as Installments from prloanget t";
                    strQuery = SQLCommand.Payroll.PayrollLoanMntList;
                    break;
                case clsPayrollConstants.PAYROLL_LOAN_MNT_ADD:
                    strQuery = SQLCommand.Payroll.PayrollLoanMntAdd;
                    break;
                case clsPayrollConstants.PAYROLL_LOAN_MNT_EDIT:
                    strQuery = SQLCommand.Payroll.PayrollLoanMntEdit;
                    break;
                case clsPayrollConstants.PAYROLL_LOAN_MNT_DEL:
                    strQuery = "";
                    break;

            }
            return strQuery;
        }
    }
}
