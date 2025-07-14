//Changes made in two methods : GetInstallmentAmount, GetLoanPaidAmount by suganthi

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Payroll.DAO.Schema;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using Bosco.Utility.Common;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Payroll.Model.UIModel
{
    public class clsprCompBuild : SystemBase
    {
        ApplicationSchema.PRCOMPONENTDataTable dtComponent = null;
        ApplicationSchema.PRCOMPMONTHDataTable dtComponentMonth = new ApplicationSchema.PRCOMPMONTHDataTable();
        ApplicationSchema.LedgerDataTable dtLedger = new ApplicationSchema.LedgerDataTable();
        ApplicationSchema.PAYROLL_RANGE_FORMULADataTable dtPayrollRange = new ApplicationSchema.PAYROLL_RANGE_FORMULADataTable();
        ApplicationSchema.STFPERSONALDataTable dtStaffPersonalSchema = new ApplicationSchema.STFPERSONALDataTable();
        //public clsprCompBuild()
        //{


        //}

        ResultArgs resultArgs = null;
        public DataTable dtTable = null;
        private clsModPay objmodPay = new clsModPay();
        private clsPayrollActivities objActivities = new clsPayrollActivities();
        private DataTable rsComponent = new DataTable();
        private DataTable rsLoan = new DataTable();
        private DataSet rsStaff = new DataSet();    //Personal Information of Staff
        private DataTable rsLoanG = new DataTable();    //Loan Received Information of Staff
        private DataTable rsLoanP = new DataTable();    //Loan Paid Information of Staff
        private DataTable rsLoanN = new DataTable();    //Import New Loan Due of Staff
        private DataTable rsDefVal = new DataTable();    //DataSet for retriving default values
        private DataSet rsCompM = new DataSet();    //DataSet for Component Month
        private DataTable rsCompF = new DataTable();    //DataSet for Component Filter
        private DataTable rsPR = new DataTable();    //DataSet for Staff Payroll Creation

        private DataView dvComponent = new DataView();	//rsComponent.Tables[0].DefaultView;
        private DataView dvStaff = new DataView();	//rsStaff.Tables[0].DefaultView;
        private DataView dvDefVal = new DataView();	//rsDefVal.Tables[0].DefaultView;
        private DataView dvCompM = new DataView();	//new DataView(rsCompM.Tables[0]);
        private DataView dvLoanP = new DataView();
        private DataTable dtComponentPaymonth = new DataTable();
        
        private string mvarSourceName;
        public long mvarComponentId;
        private string mvarFormulaId;
        private string mvarFormula;
        private int mvarIFFormulaType;
        private bool bNewPayRollProcess; //Process new payroll or Reprocess the Payroll
        private long nPayRollId { get; set; }
        private long nStaffId { get; set; }
        private bool bEquation;
        private string[][] aLink;
        private CommonMember commem = new CommonMember();
        // private DataHandling dh = new DataHandling();
        private clsPrLoan objPrLoan = new clsPrLoan();
        clsPrComponent clscomp;
        private clsEvalExpr objEvalExpr = new clsEvalExpr(); //Evaluate String Expression for Equation in payroll Process
        private bool bPAYROLL_DUPLICATED = false;
        DateTime dtprocess = new DateTime();

        //On 06/06/2019, to process no of paying salary days -----------------------------------
        //Int32 PayingSalaryDaysCompId = 0;
        private double TotalPayrollProcessingDays = 0;
        //--------------------------------------------------------------------------------------

        // On 30/09/2021, Total Days in Paymonth------------------------------------------------
        private double TotalDaysInPayMonth = 0;
        //--------------------------------------------------------------------------------------

        //On 03/07/2019, Get Salarydays component Id -------------------------------------------
        Int32 PayingSalaryDaysCompId = 0;
        Int32 BasicpayCompId = 0;
        //--------------------------------------------------------------------------------------
        //On 03/03/2022 Get have Actual Basic Pay after LOP processing and EPF Register---------
        Int32 LOPDaysCompId = 0;
        //--------------------------------------------------------------------------------------

        //On 03/09/2019 Reset Link Component for -----------------------------------------------
        bool ResetFromComponentStaffProfile = false;
        DataTable dtRangeComponents = new DataTable();
        //--------------------------------------------------------------------------------------

        //On 06/09/2019, Retrain process values to avoid reprocess for a component--------------
        //to make faster for next component
        DataTable dtProcessedEQCompValues = new DataTable();
        //--------------------------------------------------------------------------------------

        //On 24/09/2019, Retrain Actual process values without affecting Paying salary days-----
        //(For STAFF PF report)
        DataTable dtActualCompValues = new DataTable();
        //--------------------------------------------------------------------------------------
        //On 16/02/2022, Staff Applicable Statutory Compliance
        private DataTable dtApplicableStatutoryCompliance = new DataTable();
        //---------------------------------------------------------------------------------------

        public string SourceName
        {
            set { mvarSourceName = value; }
            get { return mvarSourceName; }
        }

        public long ComponentId
        {
            set { mvarComponentId = value; }
            get { return mvarComponentId; }
        }

        public string FormulaId
        {
            set { mvarFormulaId = value; }
            get { return mvarFormulaId; }
        }

        public string Formula
        {
            set { mvarFormula = value; }
            get { return mvarFormula; }
        }

        public int IFFormulaType
        {
            set { mvarIFFormulaType = value; }
            get { return mvarIFFormulaType; }
        }

        #region Range properties

        public int LinkComponentId { get; set; }
        public string MinValue { get; set; }
        public string MaxValue { get; set; }
        public string Maxslab { get; set; }

        #endregion

        private int SelectedGroupId { get; set; }
        //public DateTime DTProcessDate
        //{
        //    set { dtprocess = value; }
        //    get { return dtprocess; }
        //}

        public ResultArgs UpdateProcessDate(DateTime dtprocessdate)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.ReprocessDate, "PrComponent"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(this.AppSchema.PRCOMPONENT.PROCESS_DATEColumn, dtprocessdate);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }


        public ResultArgs AssignProcessDate()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchReprocessDate, "PrComponent"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public clsprCompBuild()
        {
            //0-Field Name, 1-Alias name
            dtComponent = this.AppSchema.PRCOMPONENT;
            aLink = new string[][] {   new string[] {"EmployeeNo", "Employee No"},   
									   new string[] {"Name", "Name"}, 
									   new string[] {"KnownAs", "Known As"},
									   new string[] {"Gender", "Gender"}, 
									   new string[] {"DateofBirth", "Date of Birth"},
									   new string[] {"DateofJoin", "Date of Join"}, 
									   new string[] {"DateofAppointment", "Date of Appointment"},
									   new string[] {"Designation", "Designation"},
									   //new string[] {"SubjAppointed", "Subject Appointed"},
									   //new string[] {"PFAcNo", "P.F. No"}, 
									   //new string[] {"ServiceRegNo", "Service Register No"},
									   //new string[] {"ITNo", "Income Tax No"},
									   new string[] {"RetirementDate", "Retirement Date"},
									   new string[] {"BasicPay", "Basic Pay"}, 
									   new string[] {"ScaleofPay", "Scale of Pay"},
                                       new string[] {"MaxWagesBasic", "Maximum wages Basic"},
                                       new string[] {"MaxWagesHRA", "Maximum wages HRA"},
                                       new string[] {"UAN", "UAN"},
									   new string[] {"IncrementMonth", "Increment Date"}
								   };


            //dh.createDataSet("SELECT * FROM prcomponent ORDER BY component", "Component");
            //rsComponent = dh.getDataSet();
            rsComponent = PayrollComponent();
            rsLoan = PayrollLoan();
            //dh.createDataSet("SELECT loanid,loanname FROM prloan ORDER BY loanname", "Loan");
            //rsLoan = dh.getDataSet();
        }

        public DataTable PayrollComponentType()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.PayrollComponentFetchAll, "Component"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

                if (resultArgs.Success)
                {
                    dtTable = resultArgs.DataSource.Table;
                }
                return dtTable;
            }
        }


        public DataTable PayrollComponent()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollcomponentTypeID, "Component"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

                if (resultArgs.Success)
                {
                    dtTable = resultArgs.DataSource.Table;
                }
                return dtTable;
            }
        }

        public ResultArgs PayrollComponentDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollComponent, "Components"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                return resultArgs;
            }
        }

        public ResultArgs FetchPayrollComponentNameByCompId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollComponetName, "Components"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(this.AppSchema.PRCOMPONENT.COMPONENTIDColumn, ComponentId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                return resultArgs;
            }
        }

        public DataTable PayrollLoan()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.PayrollLoan, "Loan"))
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

        /// <summary>
        /// existing method
        /// 
        /// //On 30/05/2019, bool DontProcess=false, added to avoid process for every modification of component value
        /// </summary>
        /// <param name="nPRId"></param>
        /// <param name="sGroupId"></param>
        /// <param name="sCompName"></param>
        /// <param name="sStaffId"></param>
        /// <param name="sAlterVal"></param>
        /// <param name="objProgress"></param>
        /// <param name="DontProcess"></param>
        /// <returns></returns>
        public bool ModifyStaffComponent(long nPRId, string sGroupId, string sCompName, string sStaffId, string sAlterVal, ProgressBar objProgress, bool DontProcess = false, bool ResetFromStaffProfile=false)
        {
            int nType = 0;
            int nMax = 0;
            long nLoanId = 0;
            long nCompId = 0;
            long nServiceId = 0;
            string sFldName = "";
            string sLoanName = "";
            string sStaffName = "";
            string[] aStaff = null;
            bool bProgress = false;
            bool bModifyStaffComponent = false;
            DataView dvComponent = new DataView(rsComponent);
            
            if (clsModPay.ProcessRunning(true, nPRId, true))
            {
                return false;
            }
            nPayRollId = nPRId;
            this.CreateStaffRSet(sGroupId, sStaffId);
            this.CreateLoanRSet(sStaffId);
            sAlterVal = sAlterVal.Trim();
            bModifyStaffComponent = true;
            int iSeperatorPos = sStaffId.LastIndexOf(',');
            if (iSeperatorPos > 0)
            {
                aStaff = sStaffId.Split(',');
                nMax = aStaff.Length;
            }
            else
                nMax = 1;

            bProgress = this.ResetProgress(objProgress, nMax);
            // dh.createDataSet("SELECT * FROM PRComponent where component ='" + sCompName + "' ORDER BY Component", "Component");
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.PayrollStaffmodify, "Component"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtComponent.COMPONENTColumn, sCompName);
                resultArgs = dataManager.FetchData(DataSource.DataTable);

                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    rsComponent = resultArgs.DataSource.Table;
                }
            }
            // rsComponent = dh.getDataSet();
            dvComponent = rsComponent.DefaultView;

            for (int i = 0; i < nMax; i++)
            {
                if (rsComponent.Rows.Count > 0)
                {
                    dvComponent.RowFilter = "Component = '" + sCompName + "'";
                    nCompId = long.Parse(dvComponent[0]["componentid"].ToString() + "");
                }
                nCompId = long.Parse(new clsModPay().GetValue("PRComponent", "componentid", "component = '" + sCompName + "'"));
                if (nCompId == 0) goto EndLine;
                if (nMax == 1)
                    nStaffId = long.Parse(sStaffId);
                else
                    nStaffId = int.Parse(aStaff[i]);
                if (!FindStaff(nStaffId))
                    goto NEXTLOOP;
                sStaffName = rsStaff.Tables[0].Rows[0]["Name"] + "";
                nServiceId = long.Parse(rsStaff.Tables[0].Rows[0]["ServiceId"] + "");

                if (Strings.Trim(dvComponent.Table.Rows[0]["linkvalue"] + "") != "")
                {
                    sFldName = dvComponent.Table.Rows[0]["linkvalue"].ToString().ToUpper().Trim() + "";

                    if (sFldName.Length >= 6 && sFldName.Substring(0, 6).ToUpper() == "LOAN :")
                    {
                        sLoanName = sFldName.Substring(7).Trim();
                        nLoanId = GetLoanId(sLoanName);
                        if (nLoanId <= 0) goto NEXTLOOP;
                        bModifyStaffComponent = AlterLoanDue(nLoanId, double.Parse(sAlterVal), sStaffName);
                        if (bModifyStaffComponent)
                            ProcessComponent(nPayRollId, sGroupId, "", nStaffId + "", false, null, false, ResetFromStaffProfile, true);
                    }
                    else if (sFldName == "BASIC PAY" || sFldName == "BASICPAY") //Alter Basic Pay
                    {
                        //this.UpdateBasicPay(double.Parse(sAlterVal), nServiceId);
                        //On 30/07/2019, Update basic in payrolltemp
                        nType = int.Parse(dvComponent.Table.Rows[0]["TYPE"] + "");
                        UpdateDefaultValue(sAlterVal, nCompId);

                        if (!DontProcess)
                        {
                            this.ProcessComponent(nPayRollId, sGroupId, "", nStaffId + "", false, null, false, ResetFromStaffProfile, true);
                        }
                    }
                    else if (sFldName.ToUpper() == PayRollExtraPayInfo.EARNING1.ToString() || sFldName.ToUpper() == PayRollExtraPayInfo.EARNING2.ToString() ||
                             sFldName.ToUpper() == PayRollExtraPayInfo.EARNING3.ToString() || sFldName.ToUpper() == PayRollExtraPayInfo.DEDUCTION1.ToString() ||
                             sFldName.ToUpper() == PayRollExtraPayInfo.DEDUCTION2.ToString() ) //on 15/05/2019 
                    {
                        //On 30/07/2019, Update Payextra information in payrolltemp
                        nType = int.Parse(dvComponent.Table.Rows[0]["TYPE"] + "");
                        UpdateDefaultValue(sAlterVal, nCompId);
                        if (!DontProcess)
                        {
                            ProcessComponent(nPayRollId, sGroupId, "", nStaffId + "", false, null, true, ResetFromStaffProfile, true);
                        }
                    }
                    else if (sFldName == "PAYING SALARY DAYS" || sFldName == "PAYINGSALARYDAYS" ||
                                sFldName == "TOTALDAYSINPAYMONTH") //Alter no of paying salary days on 15/05/2019
                    {
                        //1. Check given days with payroll month's days
                        double daysinmonth = GetTotalDaysInPayMonth();
                        if (double.Parse(sAlterVal) <= daysinmonth)
                        {
                            //2. Update given no of salary days
                            nType = int.Parse(dvComponent.Table.Rows[0]["TYPE"] + "");
                            UpdateDefaultValue(sAlterVal, nCompId);
                            if (!DontProcess)
                            {
                                ProcessComponent(nPayRollId, sGroupId, "", nStaffId + "", false, null, true, ResetFromStaffProfile, true);
                            }
                        }
                        else
                        {
                            bModifyStaffComponent = false;
                            MessageRender.ShowMessage("Given days must be less than or equal to number of days in Payroll month");
                            //MessageRender.ShowMessage("Given Salary days must be less than or equal to number of days in Payroll month");
                        }
                    }
                    //else if (dvComponent.Table.Rows[0]["EQUATIONID"].ToString().Trim() + "" == "") On 24/05/2019
                    //{
                    //    UpdateBasicPay(double.Parse(sAlterVal), nServiceId);
                    //    ProcessComponent(nPayRollId, sGroupId, "", nStaffId + "", false, null, true);
                    //}
                }
                else // if (Strings.Trim(dvComponent.Table.Rows[0]["EQUATIONID"] + "" )== "")  //Fixed Value
                {
                    string CompName = dvComponent.Table.Rows[0]["COMPONENT"].ToString();
                    if (CompName.Trim().ToUpper() == PayRollDefaultComponent.WORKINGDAYS.ToString().ToUpper() ||
                        CompName.Trim().ToUpper() == PayRollDefaultComponent.LEAVEDAYS.ToString().ToUpper() ||
                        CompName.Trim().ToUpper() == PayRollDefaultComponent.LOPDAYS.ToString().ToUpper())
                    {
                        double daysinmonth = GetTotalDaysInPayMonth();
                        if (double.Parse(sAlterVal) > daysinmonth)
                        {
                            bModifyStaffComponent = false;
                            MessageRender.ShowMessage("Given days must be less than or equal to number of days in Payroll month");
                        }
                    }

                    if (bModifyStaffComponent)
                    {
                        nType = int.Parse(dvComponent.Table.Rows[0]["TYPE"] + "");
                        UpdateDefaultValue(sAlterVal, nCompId);
                        if (!DontProcess)
                        {
                            ProcessComponent(nPayRollId, sGroupId, "", nStaffId + "", false, null, true, ResetFromStaffProfile, true);
                        }
                    }
                }

            NEXTLOOP:
                SetProgressValue(objProgress, bProgress);
            }
        EndLine:
            clsModPay.ProcessRunning(false, nPRId, true);
            return bModifyStaffComponent;
        }


        

        /* Update or Insert Default Value into PRStaffTemp Table*/
        private void UpdateDefaultValue(string sAlterVal, long nCompId)
        {	//the alternate value is inserted into prstafftemp table if compvalue for current payroll is not found. 
            //otherwise, it is updated in prstafftemp if compvalue found for the particular payroll.
            //so, from next time the current compvalue gets only updated.(not inserted..)
            string sFSQL = "", sAltSQL = "";
            bool bFound;
            string swhere = "";
            DataSet rsDef = new DataSet();
            DataTable prdt = new DataTable();
            try
            {
                //sFSQL = "SELECT staffid FROM prstafftemp WHERE componentid = " +
                //    nCompId + " AND staffid = " + nStaffId + " AND PayrollId = " + nPayRollId;
                using (DataManager datamanager = new DataManager(SQLCommand.Payroll.FetchprStaffDetails, "StaffTemp"))
                {
                    datamanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    datamanager.Parameters.Add(dtComponent.COMPONENTIDColumn, nCompId);
                    datamanager.Parameters.Add(dtComponent.STAFFIDColumn, nStaffId);
                    datamanager.Parameters.Add(dtComponent.PAYROLLIDColumn, nPayRollId);
                    resultArgs = datamanager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                    {
                        prdt = resultArgs.DataSource.Table;
                    }
                }
                //dh.createDataSet(sFSQL, "StaffTemp");
                // rsDef = dh.getDataSet();
                //rsDef = 
                bFound = (prdt.Rows.Count > 0);

                if (bFound)
                {
                    swhere = sAlterVal == "" ? "" : "'" + sAlterVal + "'";
                    //sAltSQL = "UPDATE PRStaffTemp SET compvalue = " +
                    //    (sAlterVal == "" ? "" : "'" + sAlterVal + "'") + " WHERE ComponentId = " +
                    //    nCompId + " AND staffid = " + nStaffId + " AND PayrollId = " + nPayRollId;
                    using (DataManager datamanager = new DataManager(SQLCommand.Payroll.PayrollStaffUpdate))
                    {
                        datamanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                        datamanager.Parameters.Add(dtComponent.CONDITIONColumn, swhere);
                        datamanager.Parameters.Add(dtComponent.COMPONENTIDColumn, nCompId);
                        datamanager.Parameters.Add(dtComponent.STAFFIDColumn, nStaffId);
                        datamanager.Parameters.Add(dtComponent.PAYROLLIDColumn, nPayRollId);
                        resultArgs = datamanager.UpdateData();
                    }
                }
                else
                {
                    // sAltSQL = "INSERT INTO prstafftemp (PAYROLLID,STAFFID,COMPONENTID,COMPVALUE) VALUES (" + nPayRollId + "," + nStaffId + "," + nCompId + "," + "'" + sAlterVal + "')";
                    //g_ObjPrs.parseIIf(sAlterVal = "", "Null", "'" & sAlterVal & "'") + ")";
                    //					"DECODE(" + sAlterVal + "," + "Null" + "," + sAlterVal + ")";
                    using (DataManager datamanager = new DataManager(SQLCommand.Payroll.PayrollStaffInsertDetails))
                    {
                        datamanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        datamanager.Parameters.Add(dtComponent.PAYROLLIDColumn, nPayRollId);
                        datamanager.Parameters.Add(dtComponent.STAFFIDColumn, nStaffId);
                        datamanager.Parameters.Add(dtComponent.COMPONENTIDColumn, nCompId);
                        datamanager.Parameters.Add(dtComponent.COMPVALUEColumn, sAlterVal);
                        resultArgs = datamanager.UpdateData();
                    }
                }
                //dh.insertRecord(sAltSQL);

                rsDef.Dispose();
            }
            catch
            {
                return;
            }
        }
        /* To alter loan due in the root tables prloanpaid and prloanget */
        private bool AlterLoanDue(long nLoanId, double dAlterLoanDueAmt, string sStaffName)
        {
            long nLoanGetId;
            int i, nInstallment = 0, nMode, nCurInstallment = 0;
            double dLGAmt = 0.0, dInterest = 0.0;
            double dLPAmt = 0.0, dTotLGAmt = 0.0, dTotLPAmt = 0.0;
            double dGrandTotLGAmt = 0.0, dGrandTotLPAmt = 0.0;
            int assignVal = 0;
            double dSplitDueAmt;
            double[,] aGrandTotLAmt = null;

            try
            {
                this.CreateLoanRSet(nStaffId.ToString());
                DataView dvTempLoanGet = rsLoanG.DefaultView;
                dvTempLoanGet.RowFilter = "LOANID =" + nLoanId + " AND STAFFID =" + nStaffId + " AND COMPLETED = 0";

                // dh.createDataSet("SELECT * FROM PRLoanPaid ORDER BY PRLoanGetId", "LoanPaid");
                //rsLoanP = dh.getDataSet();
                using (DataManager datamanager = new DataManager(SQLCommand.Payroll.FetchpayrollLoanPaid))
                {
                    datamanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    if (resultArgs.Success)
                        rsLoan = resultArgs.DataSource.Table;

                }
                dvLoanP = rsLoanP.DefaultView;

                dvLoanP.RowFilter = "PRLoanGetId = " + dvTempLoanGet[0]["PRLoanGetId"] +
                    " AND loanid  = " + nLoanId + " AND staffid = " + nStaffId;
                if (dvLoanP.Count > 0)
                {
                    assignVal = (dvLoanP.Count == 0 ? 0 : dvLoanP.Count - 1);
                    aGrandTotLAmt = new double[assignVal + 1, 2];
                }
                //aGrandTotLAmt[0,0] - Total Balance Loan
                //aGrandTotLAmt[0,1] - Loan Paid Amount for each loan type
                for (i = 0; i < dvLoanP.Count; i++)
                {
                    nLoanGetId = long.Parse(dvLoanP[i]["PRLoanGetId"] + "");
                    dLPAmt = dLPAmt + double.Parse(dvLoanP[i]["Amount"].ToString() + "");

                    DataView dvLoanG = new DataView(rsLoanG);
                    dvLoanG.RowFilter = "PRLoanGetId = " + nLoanGetId;
                    if (dvLoanG.Count == 0)
                        break;
                    try
                    {
                        dLGAmt = double.Parse(dvLoanG[0]["Amount"] + "");  // 0
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Payroll");
                    }
                    nInstallment = int.Parse(dvLoanG[0]["Installment"] + "");
                    nMode = int.Parse(dvLoanG[0]["IntrestMode"] + "");
                    nCurInstallment = int.Parse(dvLoanG[0]["Currentinstallment"] + ""); //line does not exist in vb..
                    dInterest = double.Parse(dvLoanG[0]["Interest"].ToString()) / 100; //Interest Rate

                    //Get Installment amount based on Interest type and given Interest Rate
                    this.GetInstallmentAmount(nMode, dLGAmt, dInterest, nInstallment, nCurInstallment, ref dTotLGAmt);
                    dGrandTotLGAmt = dGrandTotLGAmt + dTotLGAmt;

                    GetTotalLoanPaid(nLoanGetId, nLoanId, ref dTotLPAmt, ref nCurInstallment);
                    //Calculate  Total Loan Balance Amount and Last Due paid in each loan type
                    try
                    {
                        dGrandTotLPAmt = dGrandTotLPAmt + dTotLPAmt;
                        aGrandTotLAmt[i, 0] = dTotLGAmt - dTotLPAmt;
                        aGrandTotLAmt[i, 1] = double.Parse(dvLoanP[i]["Amount"].ToString() + "");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Payroll");
                    }
                }
                if (dLPAmt == dAlterLoanDueAmt)
                    goto EndLine;
                if (dvLoanP.Count > 0)
                {
                    i = 0;
                }
                //The given due amount exceeds the total balance amount abort the process
                if (dGrandTotLGAmt < (dGrandTotLPAmt + (dAlterLoanDueAmt - dLPAmt)))
                {
                    MessageBox.Show("Loan Due Amount Exceeds the Loan Received Amount for the Staff '" + sStaffName + "' !", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    goto EndLine;
                }
                //In case there is more than one loan in same type, if the due amount exceeds the
                //first first loan, split the due amount and pass it to each type

                for (i = 0; i < dvLoanP.Count; i++)
                {
                    nLoanGetId = long.Parse(dvLoanP[0]["PRLoanGetId"] + "");
                    if (aGrandTotLAmt[i, 0] >= (dAlterLoanDueAmt - aGrandTotLAmt[i, 1]))
                        dSplitDueAmt = dAlterLoanDueAmt;
                    else
                    {
                        dSplitDueAmt = dAlterLoanDueAmt - (dAlterLoanDueAmt - (aGrandTotLAmt[i, 1] + aGrandTotLAmt[i, 0]));
                        dAlterLoanDueAmt = dAlterLoanDueAmt - dSplitDueAmt;
                    }

                    // string sSql = "UPDATE PRLOANPAID SET AMOUNT=" + dSplitDueAmt + " WHERE PRLOANGETID=" + nLoanGetId + " AND PAIDDATE = TO_DATE('" + DateAndTime.DateValue(clsGeneral.PAYROLLDATE).ToShortDateString() + "', 'DD/MM/YYYY')";

                    using (DataManager datamanager = new DataManager(SQLCommand.Payroll.PayrollLoanUpdate))
                    {
                        datamanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        datamanager.Parameters.Add(dtComponent.CONDITIONColumn, dSplitDueAmt);
                        datamanager.Parameters.Add(dtComponent.PRLOANGETIDColumn, nLoanGetId);
                        datamanager.Parameters.Add(dtComponent.PAIDDATEColumn, DateAndTime.DateValue(clsGeneral.PAYROLLDATE).ToShortDateString());
                        resultArgs = datamanager.UpdateData();
                    }
                    if (!resultArgs.Success)
                    {
                        //MessageBox.Show("Loan Payment : Could not update.","MedSysB",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        return false;
                    }
                    // dh.createDataSet("SELECT * FROM PRLoanPaid ORDER BY PRLoanGetId", "LoanPaid");
                    using (DataManager datamanager = new DataManager(SQLCommand.Payroll.FetchpayrollLoanPaid))
                    {
                        datamanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        if (resultArgs.Success)
                            rsLoan = resultArgs.DataSource.Table;

                    }
                    // rsLoanP = dh.getDataSet();

                    dvLoanP = new DataView(rsLoanP);
                    //dvLoanP = rsLoanP.Tables["LoanPaid"].DefaultView; //to be corrected
                    dvLoanP.RowFilter = "PRLoanGetId = " + nLoanGetId;

                    //Update the Status in Loan Get Table
                    DataView dvLoanG = new DataView(rsLoanG);
                    dvLoanG.RowFilter = "PRLoanGetId = " + nLoanGetId;
                    int sComplete = ((aGrandTotLAmt[i, 0] + aGrandTotLAmt[i, 1]) == (dSplitDueAmt) ? 1 : 0); //to be added 
                    //string sSQL = "update prloanget set completed=" + sComplete + " where prloangetid=" + nLoanGetId;
                    using (DataManager datamanager = new DataManager(SQLCommand.Payroll.PayrollLoanGetUpdate))
                    {
                        datamanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                        datamanager.Parameters.Add(dtComponent.CONDITIONColumn, sComplete);
                        datamanager.Parameters.Add(dtComponent.PRLOANGETIDColumn, nLoanGetId);
                        resultArgs = datamanager.UpdateData();
                    }
                    if (!resultArgs.Success)
                    {
                        //MessageBox.Show("Loan Get : Could not update.","MedSysB",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        return false;
                    }

                    //dh.createDataSet("SELECT * FROM PRLOANGET", "LoanGet");
                    using (DataManager datamanager = new DataManager(SQLCommand.Payroll.FetchpayrollLoanPaid))
                    {
                        datamanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        if (resultArgs.Success)
                            rsLoan = resultArgs.DataSource.Table;

                    }
                    // rsLoanG = dh.getDataSet();
                    if (dSplitDueAmt == dAlterLoanDueAmt)
                        break;
                }
                return true;
            EndLine:
                return false;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Since Loan is not available for this staff, it is invalid to edit.","MedSysB",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
        }


        //Arguments : sGroupId, sStaffId, sCompId (Passing these Ids with ',' separator)
        //          : Ex -> sGroupId = 1,2,3... : sStaffId = 4,5,10... : sCompId = 120,125,128...
        public bool ProcessComponent(long nPRId, string sGroupId, string sCompId, string sStaffId, bool bNewPayRoll, ProgressBar objProgress, bool bModify, bool ResetFromStaffProfile, bool bApplySalaryDays)
        {
            bool bProgress;
            int nMax;
            long nCompId;
            string sEquationId = "";
            int nCompOrder = 0;
            string sRetVal = "";
            string sCompFSQL = "";
            string sWhere = "";
            string sgetVal = "";
            double setDVal = 0.0;
            int ProcessComponentType = 0;
            bool rtn = false;
            bool canproceed = false;
            
            clscomp = new clsPrComponent();
            if (!bModify)
            {
                if (!clsModPay.ProcessRunning(true, nPRId, true))
                {
                    canproceed = true;
                    ResetFromComponentStaffProfile = ResetFromStaffProfile;
                    rsComponent = PayrollComponent();
                    this.dvComponent = rsComponent.DefaultView;
                    rsLoan = PayrollLoan();

                    bNewPayRollProcess = bNewPayRoll;
                    nPayRollId = nPRId;

                    if (bNewPayRollProcess) this.ImportGroupComponent();

                    if (!bModify) //This RecordSet is Created in the Modification Function
                    {
                        this.CreateStaffRSet(sGroupId, sStaffId);
                        this.CreateLoanRSet(sStaffId);
                    }

                    //------ Create Group Component Recordset (To Process payroll according to given order)
                    sWhere = "PRCompMonth.PayRollId = " + nPayRollId + ((sGroupId != "") ? " AND " +
                             "SalaryGroupId IN (" + sGroupId + ")" : "") +
                             ((sCompId != "") ? " AND ComponentId IN (" + sCompId + ")" : "") +
                             ((sStaffId != "") ? " AND PRStaffGroup.StaffId IN (" + sStaffId + ")" : "");

                    string sToDate = commem.DateSet.ToDate(clsGeneral.PAYROLLDATE, false).AddMonths(1).AddDays(-1).ToShortDateString();

                    using (DataManager dataManager = new DataManager(SQLCommand.Payroll.ProcessPayrollInOrder, "CompMonth"))
                    {
                        dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        dataManager.Parameters.Add(dtComponent.CONDITIONColumn, sWhere);
                        dataManager.Parameters.Add(dtComponent.PAYROLLIDColumn, nPRId);
                        // dataManager.Parameters.Add(DateTime.Parse(clsGeneral.PAYROLLDATE,clsGeneral.DATE_FORMAT)
                        dataManager.Parameters.Add(dtComponent.TODATEColumn, commem.DateSet.ToDate(sToDate)); //26/07/2019
                        resultArgs = dataManager.FetchData(DataSource.DataTable);

                        if (resultArgs.Success)
                        {
                            dvCompM = resultArgs.DataSource.Table.DefaultView;
                            dtComponentPaymonth = resultArgs.DataSource.Table.DefaultView.ToTable();
                        }
                    }
                    dvCompM.Sort = "empno,comp_order";
                }
            }

            if (canproceed && CheckUsedComponentMapped()) //On 15/03/2022, To check all used compontents are mapped with selected group
            {
                //if ComponentId is empty get the ComponentId through GroupId for setting criteria
                if (sCompId == "" && !bNewPayRoll)
                {
                    sWhere = "PayRollId = " + nPayRollId + ((sGroupId != "") ? " AND " +
                             "SalaryGroupId IN (" + sGroupId + ")" : "");
                    //  sCompFSQL = "SELECT DISTINCT ComponentId FROM PRCompMonth WHERE " + sWhere;
                    DataView dvCompF = null;
                    using (DataManager dataManager = new DataManager(SQLCommand.Payroll.GetComponentIdByGroupId, "PrCompMonth"))
                    {
                        dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        dataManager.Parameters.Add(dtComponent.CONDITIONColumn, sWhere);
                        resultArgs = dataManager.FetchData(DataSource.DataTable);

                        if (resultArgs.Success)
                        {
                            dvCompF = resultArgs.DataSource.Table.DefaultView;
                        }
                    }

                    dvCompF.Sort = "componentid";

                    for (int i = 0; i < dvCompF.Table.Rows.Count; i++)
                    {
                        sCompId = sCompId + dvCompF.Table.Rows[i]["componentid"].ToString() + ",";
                    }
                    sCompId = objPrLoan.RemoveTrailingSpace(sCompId, 1);
                }

                //Component in PRStaff table and not in PRCompMonth table
                sCompFSQL = ((sGroupId != "") ? " AND " +
                      "prstaffgroup.GroupId IN (" + sGroupId + ")" : "");
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchComponentNotinPRMonth, "StaffGroup"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    dataManager.Parameters.Add(dtComponent.PAYROLLIDColumn, nPRId);
                    dataManager.Parameters.Add(dtComponent.CONDITIONColumn, sCompFSQL);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success)
                    {
                        rsCompF = resultArgs.DataSource.Table;
                    }
                }

                for (int i = 0; i < rsCompF.Rows.Count; i++)
                {
                    sCompId = ((sCompId != "") ? sCompId + "," : sCompId + "") + rsCompF.Rows[i]["componentid"].ToString();
                }
                if (sCompId != "") sCompId = sCompId.TrimEnd(','); //Line does not exist in vb..(Included Extra)


                //------ SQL for Payroll --------------------------------------------
                //Before going to Process the Payroll, Delete the existing Payroll from 'PRStaff'
                if (bNewPayRoll)
                {
                    sWhere = "PayRollId = " + nPayRollId;
                }
                else
                {
                    sWhere = "(PayRollId = " + nPayRollId + ((sStaffId != "") ?
                            " AND StaffId IN (" + sStaffId + ")" : "") +
                            ((sCompId != "") ? " AND ComponentId IN (" + sCompId + ")" : "") +
                            ((sGroupId != "") ? " AND StaffId IN (SELECT StaffId FROM PRStaffGroup " +
                            "WHERE GroupId IN (" + sGroupId + ") and PRStaffGroup.payrollid = " + nPayRollId + "))" : ")");
                    //  ((nExistCompId != "") ? " OR (ComponentId NOT IN (" + nExistCompId + ")))" : "" + ")");
                }
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.DeleteExistingPayroll))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    dataManager.Parameters.Add(dtComponent.PAYROLLIDColumn, nPRId);

                    //On 15/05/2019, to delete particular staff when componetns gets modified ----------------------------------
                    if (!string.IsNullOrEmpty(sGroupId))
                    {
                        dataManager.Parameters.Add(dtComponent.SALARYGROUPIDColumn, sGroupId);
                    }

                    if (!string.IsNullOrEmpty(sStaffId))
                    {
                        dataManager.Parameters.Add(dtComponent.STAFFIDColumn, sStaffId);
                    }
                    //-----------------------------------------------------------------------------------------------------------

                    //dataManager.Parameters.Add(dtComponent.CONDITIONColumn, sWhere);
                    resultArgs = dataManager.UpdateData();
                }

                if (!resultArgs.Success)
                {
                    MessageBox.Show("Processing Error:" + resultArgs.Message);
                    return rtn;
                }

                //Open the Table 'PRStaff' to Process the New Payroll
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffDetailsToProcessPayroll, "PrStaff"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    dataManager.Parameters.Add(dtComponent.CONDITIONColumn, sWhere);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                    {
                        rsPR = resultArgs.DataSource.Table;
                    }
                }

                //Open the Table 'PRStaffTemp': This table contains the component default values of each staff
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffTempDetailComponent, "StaffTemp"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    dataManager.Parameters.Add(dtComponent.CONDITIONColumn, sWhere);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                    {
                        rsDefVal = resultArgs.DataSource.Table;
                    }
                }

                try
                {
                    this.dvDefVal = rsDefVal.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Processing Error:" + ex.Message.ToString());
                    return rtn;
                }

                //Initialize Progress Bar Status
                nMax = dvCompM.Table.Rows.Count + 1;
                bProgress = this.ResetProgress(objProgress, nMax);

                //The component value is imported as the number of component is defined in the Equation,
                //to avoid this, first Import the Link and Default Value
                // and reset the bNewPayRollProcess as false
                if (bNewPayRollProcess)
                {
                    dvCompM.RowFilter = "equationId = null";
                    nMax = nMax + dvCompM.Table.Rows.Count + 1;
                    bProgress = ResetProgress(objProgress, nMax);

                    for (int i = 0; i < dvCompM.Table.Rows.Count; i++)
                    {
                        nStaffId = long.Parse(dvCompM.Table.Rows[i]["StaffId"] + "");
                        if (FindStaff(nStaffId)) //If Staff is found in 'Staff Personal' Process the Payroll of a Staff
                        {
                            nCompId = long.Parse(dvCompM.Table.Rows[i]["ComponentId"] + "");
                            sEquationId = dvCompM.Table.Rows[i]["EquationId"].ToString() + "";
                            //if(sEquationId=="") return;		//Modified by GP , KL
                            bEquation = (sEquationId.Trim() != "");
                            sRetVal = this.GetProcessedValue(long.Parse(dvCompM.Table.Rows[i]["ComponentId"] + ""), sEquationId, "",
                                double.Parse(dvCompM.Table.Rows[i]["MaxSlab"] + ""), int.Parse(dvCompM.Table.Rows[i]["CompRound"] + ""),
                                int.Parse(dvCompM.Table.Rows[i]["IFCondition"] + ""), bApplySalaryDays);

                            // Added by Praveen to calculate Range Value
                            if (this.MoveComponent(nCompId))
                            {
                                if ((commem.NumberSet.ToInteger(this.dvComponent[0]["DefValue"].ToString()) == 0) &&
                                     string.IsNullOrEmpty(this.dvComponent[0]["LinkValue"] + "") && sEquationId.Trim() == "")
                                {
                                    //clscomp = new clsPrComponent();
                                    if (clscomp.FetchRangeComponent(commem.NumberSet.ToInteger(nCompId.ToString())) > 0)
                                    {
                                        sRetVal = GetRangeofValue(nStaffId, nCompId, bApplySalaryDays);
                                    }
                                }
                            }
                        }
                        this.SetProgressValue(objProgress, bProgress);
                    }
                    bNewPayRollProcess = false;
                    dvCompM.RowFilter = "";
                }

                //Process Payroll
                //MessageBox.Show(dvCompM.Table.Rows.Count.ToString(),"MedSysB",MessageBoxButtons.OK,MessageBoxIcon.Information); //To view all the component id that is processed

                for (int i = 0; i < dvCompM.Table.Rows.Count; i++)
                {
                    nStaffId = long.Parse(dvCompM.Table.Rows[i]["StaffId"] + "");
                    SelectedGroupId = this.UtilityMember.NumberSet.ToInteger(dvCompM.Table.Rows[i]["SalaryGroupId"].ToString());//added to check wheather that replated component of equation type is mapped with group
                    if (this.FindStaff(nStaffId)) //If Staff in found in 'Staff Personal' Process the Payroll of a Staff
                    {
                        nCompId = long.Parse(dvCompM.Table.Rows[i]["ComponentId"] + "");
                        nCompOrder = int.Parse(dvCompM.Table.Rows[i]["comp_order"].ToString());
                        sEquationId = dvCompM.Table.Rows[i]["EquationId"].ToString() + "";
                        bEquation = (sEquationId.Trim() != "");
                        ProcessComponentType = int.Parse(dvCompM.Table.Rows[i]["PROCESS_COMPONENT_TYPE"].ToString());

                        if (dvCompM.Table.Rows[i]["MaxSlab"].ToString() != "")
                            setDVal = double.Parse(dvCompM.Table.Rows[i]["MaxSlab"].ToString());
                        else
                            setDVal = 0.0;

                        //On 16/02/2022, all the component can process by default
                        //If Process component is statutory compliance (ESF/ESI/PT), check those statutory compliance are applicable for Staff
                        sRetVal = "";
                        if (CanProceedForStaffProcess(nStaffId, ProcessComponentType))
                        {
                            sRetVal = GetProcessedValue(nCompId, sEquationId, "",
                                            double.Parse(setDVal + ""), int.Parse(dvCompM.Table.Rows[i]["CompRound"] + ""),
                                int.Parse(dvCompM.Table.Rows[i]["IFCondition"] + ""), true);
                        }
                        else if (ProcessComponentType == (Int32)PayRollProcessComponent.EPF || ProcessComponentType == (Int32)PayRollProcessComponent.ESI
                                || ProcessComponentType == (Int32)PayRollProcessComponent.PT)
                        { //On 25/02/2022, If staff statutory compliance is not applicable, fix 0 values
                            sRetVal = "0.0";
                        }

                        //On 24/09/2019, to Process again for Gross value without affecting paying salary to Get Actual value (For STAFF PF report)----
                        if (ProcessComponentType == (int)PayRollProcessComponent.GrossWages)
                        {
                            string ActualGrossValue = GetProcessedValue(nCompId, sEquationId, "",
                                       double.Parse(setDVal + ""), int.Parse(dvCompM.Table.Rows[i]["CompRound"] + ""),
                                            int.Parse(dvCompM.Table.Rows[i]["IFCondition"] + ""), false);
                            RetainProcssedActualValue(ActualGrossValue, nCompId);
                        }
                        //-----------------------------------------------------------------------------------------------------------------------------

                        // Added by Praveen to calculate Range Value
                        if (this.MoveComponent(nCompId))
                        {
                            if ((commem.NumberSet.ToInteger(this.dvComponent[0]["DefValue"].ToString()) == 0) &&
                                      string.IsNullOrEmpty(this.dvComponent[0]["LinkValue"] + "") && sEquationId.Trim() == "")
                            {
                                //clscomp = new clsPrComponent();
                                if (clscomp.FetchRangeComponent(commem.NumberSet.ToInteger(nCompId.ToString())) > 0)
                                {
                                    sRetVal = GetRangeofValue(nStaffId, nCompId, bApplySalaryDays);
                                }
                            }
                        }
                        sRetVal = sRetVal.Trim();

                        sgetVal = ((sRetVal == "") ? "" : sRetVal);
                        string ActualCompValue = GetProcssedActualCompValue(nCompId); //24/09/2019, to keep Actual processed value without affecting paying salary days (For STAFF PF report)

                        using (DataManager dataManager = new DataManager(SQLCommand.Payroll.UpdateProcessedPayroll))
                        {
                            dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                            dataManager.Parameters.Add(dtComponent.PAYROLLIDColumn, nPayRollId);
                            dataManager.Parameters.Add(dtComponent.STAFFIDColumn, nStaffId);
                            dataManager.Parameters.Add(dtComponent.COMPONENTIDColumn, nCompId);
                            dataManager.Parameters.Add(dtComponent.COMPVALUEColumn, sgetVal);
                            dataManager.Parameters.Add(dtComponent.ACTUAL_COMPVALUEColumn, ActualCompValue);
                            dataManager.Parameters.Add(dtComponent.COMPORDERColumn, nCompOrder);
                            dataManager.Parameters.Add(dtComponent.TRANSACTIONDATEColumn, commem.DateSet.ToDate(System.DateTime.Now.ToString()));

                            resultArgs = dataManager.UpdateData();
                            if (!resultArgs.Success)
                            {
                                return rtn;
                            }
                        }
                    }
                    this.SetProgressValue(objProgress, bProgress);
                }

                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffDetailsAfterProcess, "PrStaff"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    dataManager.Parameters.Add(dtComponent.CONDITIONColumn, sWhere);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                    {
                        rsPR = resultArgs.DataSource.Table;
                    }
                }

                if (!bModify)
                {
                    clsModPay.ProcessRunning(false, nPRId, true);
                }
                rtn = true;
            }

            return rtn;
        }

        private static List<DataTable> SplitTable(DataTable originalTable, int batchSize)
        {
            List<DataTable> tables = new List<DataTable>();
            int i = 0;
            int j = 1;
            DataTable newDt = originalTable.Clone();
            newDt.TableName = "Table_" + j;
            newDt.Clear();
            foreach (DataRow row in originalTable.Rows)
            {
                DataRow newRow = newDt.NewRow();
                newRow.ItemArray = row.ItemArray;
                newDt.Rows.Add(newRow);
                i++;
                if (i == batchSize)
                {
                    tables.Add(newDt);
                    j++;
                    newDt = originalTable.Clone();
                    newDt.TableName = "Table_" + j;
                    newDt.Clear();
                    i = 0;
                }
            }
            return tables;
        }


        /// <summary>
        /// Reprocess the componets which has range components in a equation
        /// </summary>
        /// <param name="Componentid"></param>
        /// <param name="sEquationId"></param>
        private void ReprocesstheComponents(DataView dvComponents, ProgressBar progressprocess, bool newprocess, bool bApplySalaryDays)
        {
            long nCompId;
            string sEquationId = "";
            int nCompOrder = 0;
            string sRetVal = "";
            string sgetVal = "";
            double setDVal = 0.0;
            
            clscomp = new clsPrComponent();

            for (int i = 0; i < dvCompM.Table.Rows.Count; i++)
            {
                nStaffId = long.Parse(dvCompM.Table.Rows[i]["StaffId"] + "");
                if (this.FindStaff(nStaffId)) //If Staff in found in 'Staff Personal' Process the Payroll of a Staff
                {
                    nCompId = long.Parse(dvCompM.Table.Rows[i]["ComponentId"] + "");
                    nCompOrder = int.Parse(dvCompM.Table.Rows[i]["comp_order"].ToString());
                    sEquationId = dvCompM.Table.Rows[i]["EquationId"].ToString() + "";
                    bEquation = (sEquationId.Trim() != "");
                    if (dvCompM.Table.Rows[i]["MaxSlab"].ToString() != "")
                        setDVal = double.Parse(dvCompM.Table.Rows[i]["MaxSlab"].ToString());
                    else
                        setDVal = 0.0;

                    sRetVal = GetProcessedValue(long.Parse(dvCompM.Table.Rows[i]["ComponentId"] + ""), sEquationId, "",
                                    double.Parse(setDVal + ""), int.Parse(dvCompM.Table.Rows[i]["CompRound"] + ""),
                        int.Parse(dvCompM.Table.Rows[i]["IFCondition"] + ""), false);

                    // Added by Praveen to calculate Range Value
                    if (this.MoveComponent(nCompId))
                    {
                        if ((commem.NumberSet.ToInteger(this.dvComponent[0]["DefValue"].ToString()) == 0) &&
                                  string.IsNullOrEmpty(this.dvComponent[0]["LinkValue"] + "") && sEquationId.Trim() == "")
                        {
                            //clscomp = new clsPrComponent();
                            if (clscomp.FetchRangeComponent(commem.NumberSet.ToInteger(nCompId.ToString())) > 0)
                            {
                                sRetVal = GetRangeofValue(nStaffId, nCompId, bApplySalaryDays);
                            }
                        }
                    }
                    sRetVal = sRetVal.Trim();


                    //Update Processed Value in the Table 'PRStaff'
                    //New field (CompOrder ) is added by Kulandai and Gnanam for ordering the components
                    // Remarks : add data value
                    //sgetVal = ((sRetVal == "") ? null : sRetVal);
                    //sSql = sSql.Replace("<compvalue>", sgetVal);
                    sgetVal = ((sRetVal == "") ? null : sRetVal);
                    //	sSql = sSql.Replace("<compvalue>",sgetVal);
                    using (DataManager dataManager = new DataManager(SQLCommand.Payroll.UpdateProcessedPayroll))
                    {
                        dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        dataManager.Parameters.Add(dtComponent.PAYROLLIDColumn, nPayRollId);
                        dataManager.Parameters.Add(dtComponent.STAFFIDColumn, nStaffId);
                        dataManager.Parameters.Add(dtComponent.COMPONENTIDColumn, nCompId);
                        dataManager.Parameters.Add(dtComponent.COMPVALUEColumn, sgetVal);
                        dataManager.Parameters.Add(dtComponent.COMPORDERColumn, nCompOrder);
                        dataManager.Parameters.Add(dtComponent.TRANSACTIONDATEColumn, commem.DateSet.ToDate(System.DateTime.Now.ToString()));

                        resultArgs = dataManager.UpdateData();
                        if (!resultArgs.Success)
                        {
                            return;
                        }
                    }


                    ////sSql = "INSERT INTO prstaff(payrollid,staffid,componentid,compvalue,comporder) VALUES(" + nPayRollId + "," + nStaffId + "," + nCompId + ",'<compvalue>'," + nCompOrder + ")";
                    //dh.insertRecord(sSql);
                }
                this.SetProgressValue(progressprocess, newprocess);
            }
        }

        /// <summary>
        ///  To Get Range of value by the component id
        /// </summary>
        /// <param name="Staffid"></param>
        /// <param name="CompId"></param>
        /// <returns></returns>
        public string GetRangeofValue(long Staffid, long CompId, bool bApplySalaryDays)
        {
            string rtnval = "0.00";
            string rtntmpval = "0.00";
            ResultArgs result = new ResultArgs();
            DataTable dtRangeList = new DataTable();
            string linkComId = string.Empty;

            DataTable dtTemp = new DataTable();

            try
            {
                if (ResetFromComponentStaffProfile || !RecordsExistsinPrStattTemp(CompId))
                {
                    result = FetchRangeValuesbyCompId(CompId);
                    if (result.Success && result.DataSource.Table.Rows.Count > 0)
                    {
                        dtRangeList = result.DataSource.Table;
                        DataRow dr = dtRangeList.Rows[0];
                        linkComId = dr["LINK_COMPONENT_ID"].ToString();

                        dvCompM.RowFilter = "STAFFID=" + Staffid + " AND COMPONENTID=" + linkComId + "";
                        dtTemp = dvCompM.ToTable();
                        if (dtTemp.Rows.Count>0)
                        {
                            DataRow drtemp = dtTemp.Rows[0];
                            bEquation = true;//((drtemp["EquationId"].ToString() + "").Trim() != ""); //On 25/07/2019, to equation if formula comp used in Range condition
                            rtntmpval = GetProcessedValue(long.Parse(linkComId), drtemp["EquationId"].ToString() + "", "",
                                                          double.Parse(drtemp["MaxSlab"].ToString() + ""), int.Parse(drtemp["CompRound"] + ""),
                                                          int.Parse(drtemp["IFCondition"] + ""), bApplySalaryDays);
                            rtnval = ProcessCompRangeValue(CompId.ToString(), rtntmpval);
                            //ResetProcssedEQCompValue(long.Parse(linkComId));
                            //rtnval = ProcessCompRangeValue(CompId.ToString(), rtntmpval);
                            //rtnval = ProcessCompRangeValueByConsolidated(CompId.ToString(), rtntmpval);
                            /*string rtntmpval1 = GetProcessedValue(long.Parse(linkComId), "<54>*5/100~0~0", "",
                                                         double.Parse(drtemp["MaxSlab"].ToString() + ""), int.Parse(drtemp["CompRound"] + ""),
                                                         int.Parse(drtemp["IFCondition"] + ""), bApplySalaryDays);                            
                            rtnval = rtntmpval1;*/

                            GetDefaultValue(rtnval, CompId);
                        }
                    }
                }
                else
                {
                    rtnval = GetDefaultValue(string.Empty, CompId);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
                return "0";
            }
            return rtnval;
        }

        public ResultArgs FetchRangeValuesbyCompId(long Compid)
        {
            using (DataManager dataManger = new DataManager(SQLCommand.Payroll.FetchRangeListbyCompId))
            {
                dataManger.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManger.Parameters.Add(dtComponent.COMPONENTIDColumn, Compid);
                resultArgs = dataManger.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public string ProcessCompRangeValue(string compId, string lnkCompValue)
        {
            string ptrtnVal = string.Empty;
            double lnkcomamount = commem.NumberSet.ToDouble(lnkCompValue);
            long compID = 0;
            double MinVal = 0;
            double MaxVal = 0;
            double RangeMaxSlab = 0;
            DataTable dtRangeList = new DataTable();
            ResultArgs result = new ResultArgs();

            compID = commem.NumberSet.ToInteger(compId);
            result = FetchRangeValuesbyCompId(compID);
            if (result.Success && result.DataSource.Table.Rows.Count > 0)
            {
                dtRangeList = result.DataSource.Table;
                foreach (DataRow dr in dtRangeList.Rows)
                {
                    MinVal = commem.NumberSet.ToDouble(dr["MIN_VALUE"].ToString());
                    MaxVal = commem.NumberSet.ToDouble(dr["MAX_VALUE"].ToString());
                    RangeMaxSlab = commem.NumberSet.ToDouble(dr["MAX_SLAB"].ToString());

                    if (lnkcomamount >= MinVal && lnkcomamount <= MaxVal)
                    {
                        ptrtnVal = RangeMaxSlab.ToString();

                        //string sCondEQVal1 = GetProcessedValue(13, "<13>*18/100~0~0", "", 0, 1, 0, true);
                        //double dCondEQVal1 = UtilityMember.NumberSet.ToDouble(sCondEQVal1);
                        //ptrtnVal = dCondEQVal1.ToString();

                        break; //On 12/08/2019
                    }
                }
            }

            //On 12/08/2019, if value does not fall in any range, return "0" value for component proecess
            if (string.IsNullOrEmpty(ptrtnVal))
            {
                ptrtnVal="0";
            }
            //-----------------------------------------------------------------------------------------------

            return ptrtnVal;
        }

        public string ProcessCompRangeValueByConsolidated(string compId, string lnkCompValue)
        {
            string ptrtnVal = string.Empty;
            double lnkcomamount = commem.NumberSet.ToDouble(lnkCompValue);
            long compID = 0;
            double MinVal = 0;
            double MaxVal = 0;
            double RangeMaxSlab = 0;
            DataTable dtRangeList = new DataTable();
            ResultArgs result = new ResultArgs();

            compID = commem.NumberSet.ToInteger(compId);
            result = FetchRangeValuesbyCompId(compID);
            if (result.Success && result.DataSource.Table.Rows.Count > 0)
            {
                dtRangeList = result.DataSource.Table;
                foreach (DataRow dr in dtRangeList.Rows)
                {
                    MinVal = commem.NumberSet.ToDouble(dr["MIN_VALUE"].ToString());
                    MaxVal = commem.NumberSet.ToDouble(dr["MAX_VALUE"].ToString());
                    RangeMaxSlab = commem.NumberSet.ToDouble(dr["MAX_SLAB"].ToString());

                    if (lnkcomamount >= MinVal )
                    {
                        var rangebalanceamt = Math.Min(MaxVal - MinVal, lnkcomamount - MinVal);
                        var rangeamt = rangebalanceamt * (RangeMaxSlab / 100);
                        ptrtnVal += rangeamt;
                    }
                }
            }

            //On 12/08/2019, if value does not fall in any range, return "0" value for component proecess
            if (string.IsNullOrEmpty(ptrtnVal))
            {
                ptrtnVal = "0";
            }


            //-----------------------------------------------------------------------------------------------

            return ptrtnVal;
        }

        public ResultArgs FetchLedger()
        {
            using (DataManager dataManger = new DataManager(SQLCommand.Payroll.FetchLedger))
            {
                dataManger.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManger.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        /* While Creating New Payroll Import All the Components from Previous Month Payroll */
        public void ImportGroupComponent()
        {
            long nPrevPRId;
            bool result = false;//,sDefValSQL="";
            DataTable rsLPR = new DataTable();
            //   sPRSQL = "SELECT payrollid FROM prcreate where rownum in (1,2) ORDER BY payrollid DESC";
            //sPRSQL = "SELECT TOP 2 payrollid FROM prcreate ORDER BY prdate DESC";

            //  dh.createDataSet(sPRSQL, "prcreate");
            rsLPR = FetchTopTwoPayrollId();

            if (rsLPR.Rows.Count == 2)
            {
                nPrevPRId = int.Parse(rsLPR.Rows[1]["PayrollId"] + "");
            }
            else
            {
                goto EndLine;
            }
            //sGrpSQL = "INSERT INTO prcompmonth(payrollid,salarygroupid,componentId,type,defvalue," +
            //    "lnkvalue,equation,equationId,maxslab,comp_order," +
            //    "compround,ifcondition) " +
            //    "SELECT DISTINCT " + nPayRollId + "," + "salarygroupid,componentid,type," +
            //    "defvalue,lnkvalue,equation,equationid,maxslab," +
            //    "comp_order,compround,ifcondition FROM prcompmonth " +
            //    "WHERE payrollid = " + nPrevPRId;

            //dh.insertRecord(sGrpSQL);
            if (PrcomponentAdd(nPrevPRId))
            {

                //// Import Default Value from previous Payroll to current Payroll in 'PRStaffTemp' table
                //sGrpSQL = "INSERT INTO prstafftemp (payrollid,staffid,componentid,compvalue) " +
                //    "SELECT DISTINCT " + nPayRollId + "," + "staffid,componentid,compvalue " +
                //    "FROM prstafftemp WHERE payrollid = " + nPrevPRId;
                //dh.insertRecord(sGrpSQL);
                ImportPreviousPayroll(nPrevPRId);
            }
        // Import Pay Slip Template from previous Payroll to current Payroll in 'PRPSRptSetting' table
        //sGrpSQL = "INSERT INTO prpsrptsetting (PayrollId,GroupId,SecProperty,RptProperty,HLine,VLine,BLine) " +
        //	"SELECT " + nPayRollId + "," + "groupid,secproperty,rptproperty,hline,vline,bline " +
        //	"FROM prpsrptsetting WHERE payrollid = " + nPrevPRId;
        //dh.insertRecord(sGrpSQL);
        EndLine:
            rsLPR.Dispose();
        }
        private DataTable FetchTopTwoPayrollId()
        {
            DataTable dtFetchTopId = null;

            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchTopTwoPayrollId, "Loan"))
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
        private bool PrcomponentAdd(long nPrevPRId)
        {
            DataTable dtFetchTopId = null;

            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.PrCreatecompMonthAdd))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtComponent.NEWPAYROLLIDColumn, nPayRollId);
                dataManager.Parameters.Add(dtComponent.PAYROLLIDColumn, nPrevPRId);
                resultArgs = dataManager.UpdateData();

                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    return true;
                }
            }
            return false;
        }
        private bool ImportPreviousPayroll(long nPrevPRId)
        {
            double totaldaysinmonth = GetTotalDaysInPayMonth(nPayRollId);

            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.ImportPreviousPayroll))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtComponent.NEWPAYROLLIDColumn, nPayRollId);
                dataManager.Parameters.Add(dtComponent.PAYROLLIDColumn, nPrevPRId);
                dataManager.Parameters.Add(dtComponent.NUMBER_OF_DAYSColumn, totaldaysinmonth);
                
                resultArgs = dataManager.UpdateData();
                

                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    return true;
                }
            }
            return false;
        }
        public void CreateStaffRSet(string sGroupId, string sStaffId)
        {
            string sStaffSQL, sWhere, sToDate;

            DataTable dtStaffTable = null;
            //------ Create Staff Information Recordset (for getting Link Value of a payroll) -------
            //It Contains Personal Info, Pay Info about Staff

            sToDate = commem.DateSet.ToDate(clsGeneral.PAYROLLDATE, false).AddMonths(1).AddDays(-1).ToShortDateString();
            sWhere = ((sStaffId != "") ? " AND stfPersonal.StaffId IN (" + sStaffId + ")" : "") +
                ((sGroupId != "") ? " AND PRStaffGroup.GroupId IN (" + sGroupId + ")" : "");
            //"AND " + "To_Date('" + sToDate + "', 'dd/mm/yyyy')" +
            //" >= stfPersonal.dateofJoin and (stfPersonal.LeavingDate is null or " +
            //"stfPersonal.LeavingDate > " + "To_Date('" + DateTime.Parse(clsGeneral.PAYROLLDATE).ToShortDateString() + "', 'dd/mm/yyyy')" + ") " +
            //" AND ((" + "To_Date('" + sToDate + "', 'dd/mm/yyyy')" + " BETWEEN " +
            //"stfService.DateofAppointment AND stfService.DateofTermination) " +
            //" OR (stfService.DateofTermination is null AND " +
            //"To_Date('" + sToDate + "', 'dd/mm/yyyy')" + " > stfService.DateofAppointment))";

            //sStaffSQL = "SELECT stfPersonal.StaffId,stfService.serviceid as ServiceId,EmpNo as \"EmployeeNo\",FirstName,LastName," +
            //    "firstname || ' ' || lastname as \"Name\"," +
            //    "KnownAs,Gender,DateofBirth,DateofJoin,DateofAppointment," +
            //    "Designation,Department," +
            //    "RetirementDate,stfService.Pay AS BasicPay," +
            //    "stfService.ScaleofPay,PayIncM1,PayIncM2 " +
            //    "FROM stfPersonal,stfService,PRStaffGroup,hospital_departments WHERE " +
            //    "stfPersonal.StaffId = PRStaffGroup.StaffId AND " +
            //    "PRStaffGroup.Payrollid = " + nPayRollId +
            //    " and stfPersonal.StaffId = stfService.StaffId AND " +
            //    "stfPersonal.deptid = hospital_departments.hdept_id AND " + sWhere +
            //    " ORDER BY stfPersonal.StaffId"; DataTable dtFetchTopId = null;

            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffDetails, "Staff"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(dtComponent.PAYROLLDATEColumn, commem.DateSet.ToDate(clsGeneral.PAYROLLDATE));
                dataManager.Parameters.Add(dtComponent.PAYROLLIDColumn, nPayRollId);
                dataManager.Parameters.Add(dtComponent.TODATEColumn, sToDate);
                dataManager.Parameters.Add(dtComponent.CONDITIONColumn, sWhere);
                dataManager.Parameters.Add(dtComponent.LEAVEREMARKSColumn, clsGeneral.LeavingReason.Resigned.ToString());
                resultArgs = dataManager.FetchData(DataSource.DataTable);

                if (resultArgs.Success)
                {
                    dtStaffTable = resultArgs.DataSource.Table;
                }
            }
            rsStaff = new DataSet();
            rsStaff.Tables.Add(dtStaffTable);
            rsStaff.AcceptChanges();
            dvStaff = dtStaffTable.DefaultView;

            //On 06/06/2019, to process no of paying salary days --------------------------------------------------------------------
            TotalPayrollProcessingDays = GetTotalPayrollProcessingDays();

            //On 30/09/2021, to get total 
            TotalDaysInPayMonth = GetTotalDaysInPayMonth(nPayRollId);

            PayingSalaryDaysCompId = 0;
            BasicpayCompId = 0;
            LOPDaysCompId = 0;
            clsPrComponent objPayroll = new clsPrComponent();
            DataTable dtComp = objPayroll.FetchPayroll(nPayRollId, UtilityMember.NumberSet.ToInteger(sGroupId));

            if (dtComp != null && dtComp.Rows.Count > 0)
            {
                dtComp.DefaultView.RowFilter = "LNKVALUE = '" + PayRollExtraPayInfo.PAYING_SALARY_DAYS.ToString().Replace("_", "") + "'";
                if (dtComp.DefaultView.Count > 0)
                {
                    PayingSalaryDaysCompId = UtilityMember.NumberSet.ToInteger(dtComp.DefaultView[0]["COMPONENTID"].ToString());
                }

                //For Basic pay
                dtComp.DefaultView.RowFilter = "LNKVALUE = 'BasicPay'";
                if (dtComp.DefaultView.Count > 0)
                {
                    BasicpayCompId = UtilityMember.NumberSet.ToInteger(dtComp.DefaultView[0]["COMPONENTID"].ToString());
                }

                /*
                dtComp.DefaultView.RowFilter = "COMPONENT = '"+ PayRollDefaultComponent.LOPDAYS.ToString()  +"'";
                if (dtComp.DefaultView.Count > 0)
                {
                    LOPDaysCompId = UtilityMember.NumberSet.ToInteger(dtComp.DefaultView[0]["COMPONENTID"].ToString());
                }
                 */
            }
            //--------------------------------------------------------------------------------------------

            //On 06/09/2019, List of Range Components------------------------------------------------------
            using (clsPrComponent comp = new clsPrComponent())
            {
                ResultArgs result = comp.FetchRangeComponent();
                if (result.Success)
                {
                    dtRangeComponents = result.DataSource.Table;
                }
            }

            dtProcessedEQCompValues.Clear();
            if (dtProcessedEQCompValues.Columns.Count == 0)
            {
                dtProcessedEQCompValues.Columns.Add(new DataColumn(dtComponent.STAFFIDColumn.ColumnName, typeof(System.Int32)));
                dtProcessedEQCompValues.Columns.Add(new DataColumn(dtComponent.COMPONENTIDColumn.ColumnName, typeof(System.Int32)));
                dtProcessedEQCompValues.Columns.Add(new DataColumn(dtComponent.COMPVALUEColumn.ColumnName, typeof(System.String)));
            }

            dtActualCompValues.Clear();
            if (dtActualCompValues.Columns.Count == 0)
            {
                dtActualCompValues.Columns.Add(new DataColumn(dtComponent.STAFFIDColumn.ColumnName, typeof(System.Int32)));
                dtActualCompValues.Columns.Add(new DataColumn(dtComponent.COMPONENTIDColumn.ColumnName, typeof(System.Int32)));
                dtActualCompValues.Columns.Add(new DataColumn(dtComponent.COMPVALUEColumn.ColumnName, typeof(System.String)));
            }
            //----------------------------------------------------------------------------------------------

            //On 16/02/2002, Applicalbe Staff Statutory Compliance ------------------------------------------
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPRStaffStatutoryComplianceByPayrollId, "StatutoryCompliance"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtStaffPersonalSchema.PAYROLL_IDColumn, nPayRollId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                
                if (resultArgs.Success)
                {
                    dtApplicableStatutoryCompliance = resultArgs.DataSource.Table;
                }
            }
            //-----------------------------------------------------------------------------------------------
        }

        public void CreateLoanRSet(string sStaffId)
        {
            string sLoanGSQL = "", sLoanPSQL = "", sWhere = "";

            //------ SQL for Staff Loan Get Information ----------------------------------
            sWhere = ((sStaffId != "") ? " StaffId IN (" + sStaffId + ")" : "");
            sWhere = ((sWhere != "") ? " WHERE " + sWhere : "");
            //sLoanGSQL = "SELECT * FROM PRLoanGet" + ((sWhere != "") ? " WHERE " + sWhere : "") +
            //    " ORDER BY PRLoanGetId";
            //dh.createDataSet(sLoanGSQL, "LoanGet");
            //rsLoanG = dh.getDataSet();


            //------ SQL for Staff Loan Get Information ----------------------------------
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffLoanGet, "LoanGet"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                //dataManager.Parameters.Add(sWhere);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(dtComponent.CONDITIONColumn, sWhere);
                resultArgs = dataManager.FetchData(DataSource.DataTable);

                if (resultArgs.Success)
                {
                    rsLoanG = resultArgs.DataSource.Table;
                }
            }
            //--------------------------------------------------------------------------

            //------ SQL for Staff Loan Paid Information ----------------------------------
            sWhere = "PayRollId = " + nPayRollId + ((sStaffId != "") ? " AND StaffId IN (" + sStaffId + ")" : "");
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffLoanPaid, "LoanPaid"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                //  dataManager.Parameters.Add(sWhere);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(dtComponent.CONDITIONColumn, sWhere);
                resultArgs = dataManager.FetchData(DataSource.DataTable);

                if (resultArgs.Success)
                {
                    rsLoanP = resultArgs.DataSource.Table;
                }
            }
            dvLoanP = rsLoanP.DefaultView;
            //----------------------------------------------------------------------------
            ////------ SQL for Staff Loan Paid Information ----------------------------------
            //sWhere = "PayRollId = " + nPayRollId + ((sStaffId != "") ? " AND StaffId IN (" + sStaffId + ")" : "");
            ////sLoanPSQL = "SELECT * FROM PRLoanPaid WHERE " + sWhere + " ORDER BY PRLoanGetId";
            ////dh.createDataSet(sLoanPSQL, "LoanPaid");
            //rsLoanP = dh.getDataSet();
            //dvLoanP = rsLoanP.Tables[0].DefaultView;
            //----------------------------------------------------------------------------
        }
        public bool ResetProgress(ProgressBar objProcess, int nMax)
        {
            if (objProcess != null)
            {
                objProcess.Minimum = 0;
                objProcess.Maximum = nMax;
                objProcess.Value = 0;
                return true;
            }
            return false;
        }

        public void SetProgressValue(ProgressBar objProg, bool bProgress)
        {
            if (bProgress)//Set the Progress value according to the loop
            {
                if (objProg.Value < objProg.Maximum)
                    objProg.Value = objProg.Value + 1;
            }
        }
        public string GetProcessedValue(long nCompId, string sEQ, string sVal, double dMaxSlab,
            int nCompRnd, int nIFType, bool bApplySalaryDays)
        {
            int k = 0; // i, nPos = 0, nPos1, nEvalExpr =0,k=0;
            int nType = 0, nCompRnd1 = 0;
            //long nCompId1;
            //double dMaxSlab1 = 0.0;
            //string[] getEQ;
            //double dRetVal;
            //string formulaGroup ="";
            string sVal1 = "", sDefVal = "", sLnkVal = ""; //, sCompId = "";
            string sRetVal = ""; //,sEQ1 = "", sCharExp = "";

            string formula = "";
            string[] aformulaGroup;
            string[] aformula;
            string staffGroup = "";
            int formulaStaffGroupId = 0; //Contains list of StaffId for the formula
            //bool bContainsRestriction = false;
            //if (nCompId == 1247)
            //    nCompId = nCompId;

            if (nCompId == 50)
            {

            }
                                   

            try //catches error..
            {
                if (!this.MoveComponent(nCompId)) goto EndLine;
                Int32 ProcessComponentType =  int.Parse(this.dvComponent[0]["PROCESS_COMPONENT_TYPE"].ToString());
                if (!CanProceedForStaffProcess(nStaffId, ProcessComponentType))
                {
                    sVal1 = "0.0";
                    goto EndLine;
                }

                //MessageBox.Show(this.dvComponent[0]["componentid"].ToString()); //To view the component id that is processed
                nType = int.Parse(this.dvComponent[0]["Type"] + "");
                sDefVal = Strings.Trim(this.dvComponent[0]["DefValue"] + "");
                //On 22/11/2023, To get default value from monthly compontnet ----------------------------------------------------------------------
                dtComponentPaymonth.DefaultView.RowFilter = AppSchema.PRCOMPMONTH.STAFFIDColumn.ColumnName + "=" + nStaffId + " AND " +
                                                            AppSchema.PRCOMPMONTH.COMPONENTIDColumn.ColumnName + "=" + nCompId;

                if (dtComponentPaymonth.DefaultView.Count > 0)
                {
                    sDefVal = Strings.Trim(dtComponentPaymonth.DefaultView[0]["DefValue"] + "");
                }
                //------------------------------------------------------------------------------------------------------------------
                sLnkVal = Strings.Trim(this.dvComponent[0]["LinkValue"] + "");
                nCompRnd1 = int.Parse(this.dvComponent[0]["CompRound"] + "");

                if (sEQ.Trim() != "") //Equation
                {
                    //As on 01/02/2022, For formula component to allow change values after processed
                    if (RecordsExistsinPrStattTemp(nCompId))
                    {
                        sVal1 = GetDefaultValue(sDefVal, nCompId);
                    }
                    else
                    {
                        //nCompId-Recent Component Id, sEQ-Equation, sVal-Recent Value
                        if (nIFType > 0) //It is an IF-Condition Equation
                        {
                            aformulaGroup = sEQ.Split('$');

                            for (k = 0; k < aformulaGroup.Length; k++)
                            {
                                aformula = aformulaGroup[k].Split('~');
                                formula = aformula[0];
                                nIFType = Convert.ToInt32(aformula[1]);
                                formulaStaffGroupId = Convert.ToInt32(aformula[2]);
                                string defaultValue = GetDefaultValueForFormula(nCompId);

                                //  double defaultValue = string.IsNullOrEmpty(defaultValue
                                if (nIFType > 0)
                                {
                                    sVal1 = this.EvaluateIFEquation(nCompId, formula, nIFType, bApplySalaryDays);
                                }
                                else if (!string.IsNullOrEmpty(defaultValue) && string.IsNullOrEmpty(sEQ))
                                {
                                    sVal1 = defaultValue.ToString();
                                }
                                else
                                {
                                    sVal1 = EvaluateEquation(nCompId, formula, sVal, nIFType, dMaxSlab, nCompRnd, bApplySalaryDays);
                                }

                                //Expression is true
                                if (Convert.ToDouble(sVal1) > 0)
                                {
                                    if (formulaStaffGroupId > 0)
                                    {
                                        staffGroup = objActivities.getFormulaGroupStaffIdCollection(formulaStaffGroupId);

                                        if (staffGroup.IndexOf("@" + nStaffId.ToString() + "@") >= 0)
                                        {
                                            break;
                                        }
                                    }
                                    else //for all staff
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            sVal1 = EvaluateEquation(nCompId, sEQ, sVal, nIFType, dMaxSlab, nCompRnd, bApplySalaryDays);
                        }
                    }
                }
                else //Link Value or Default Value
                {
                    //30/07/2019, for payextra information, update and get values from temp
                    bool PayExtraInfoFields = (sLnkVal.ToUpper() == PayRollExtraPayInfo.EARNING1.ToString() || sLnkVal.ToUpper() == PayRollExtraPayInfo.EARNING2.ToString() ||
                            sLnkVal.ToUpper() == PayRollExtraPayInfo.EARNING3.ToString() ||
                            sLnkVal.ToUpper() == PayRollExtraPayInfo.DEDUCTION1.ToString() || sLnkVal.ToUpper() == PayRollExtraPayInfo.DEDUCTION2.ToString() ||
                            sLnkVal.ToUpper() == PayRollExtraPayInfo.PAYING_SALARY_DAYS.ToString().Replace("_","") ||
                            sLnkVal.ToUpper() == "TOTALDAYSINPAYMONTH");

                    if (sLnkVal.Trim() != "" && !PayExtraInfoFields) //Link Value  from 'stfPersonal' and 'stfService'
                    {
                        sRetVal = GetLinkValue(sLnkVal, bApplySalaryDays);             //link values for income,deduction,text..
                    }
                    else if (sLnkVal.Trim() != "" && PayExtraInfoFields) //On 30/07/2019, For pay extra link values get it from temp
                    {
                        if (RecordsExistsinPrStattTemp(BasicpayCompId))
                        {
                            sRetVal = GetDefaultValue(sDefVal, nCompId);
                        }
                        else
                        {
                            sRetVal = GetLinkValue(sLnkVal, bApplySalaryDays);  
                        }

                        //On 30/07/2019, if salary days exists, deduct earning1, earning2 and earning3 based on days
                        if ((PayingSalaryDaysCompId>0 || LOPDaysCompId >0 ) && sLnkVal.ToUpper() == PayRollExtraPayInfo.EARNING1.ToString() || sLnkVal.ToUpper() == PayRollExtraPayInfo.EARNING2.ToString() ||
                            sLnkVal.ToUpper() == PayRollExtraPayInfo.EARNING3.ToString()) 
                        {
                            if (PayingSalaryDaysCompId>0)
                            {
                                double StaffPayingSalaryDays = UtilityMember.NumberSet.ToDouble(GetDefaultValue(sDefVal, PayingSalaryDaysCompId));
                                if (StaffPayingSalaryDays > 0 && bApplySalaryDays)
                                {
                                    RetainProcssedActualValue(sRetVal, nCompId); //Retain payingsalary days Actual Comp Value
                                    double EarningWithNoOfDay = (this.UtilityMember.NumberSet.ToDouble(sRetVal) / TotalPayrollProcessingDays) * StaffPayingSalaryDays;
                                    sRetVal = EarningWithNoOfDay.ToString();
                                }
                            }
                            else if (LOPDaysCompId>0)
                            {
                                sRetVal = AffectLOPDays(UtilityMember.NumberSet.ToDouble(sRetVal)).ToString();
                            }
                        }
                        else if (sLnkVal.ToUpper() == PayRollExtraPayInfo.PAYING_SALARY_DAYS.ToString().Replace("_", "").ToString()) //On 30/07/2019, For pay extra link values get it from temp
                        { //On 04/03/20
                            double StaffPayingSalaryDays = UtilityMember.NumberSet.ToDouble(GetDefaultValue(sDefVal, nCompId));
                            if (StaffPayingSalaryDays == 0)
                            {
                                sRetVal = TotalPayrollProcessingDays.ToString();
                            }
                            else
                            {
                                RetainProcssedActualValue(TotalPayrollProcessingDays.ToString(), nCompId); //Retain payingsalary days Actual Comp Value
                                sRetVal = StaffPayingSalaryDays.ToString();
                            }
                        }

                    }
                    else if (IsRangeComponent(nCompId)) //For Range Comp, set 0 values by defualt
                    {
                        sRetVal = "0";
                    }
                    else //Default Value  from 'PRComponent' or 'stfStaffTemp'
                    {
                        sRetVal = GetDefaultValue(sDefVal, nCompId); //Default value..
                    }

                    //For Temporary Purpose 11/01/2017 ---------------------------------------------------------------------------------------------------
                    //To calculate Text Components into formula components
                    //For Temporary purpose for Working Days, No CL Text fileds with number value
                    //to caluculate LOP formula componets with text fields
                    bool IsTextComponentwithnumber = false;
                    if (((nType == 2 || nType == 3) && String.IsNullOrEmpty(sLnkVal)))
                    {
                        double dttxtcomponentwithnumer = 0;
                        IsTextComponentwithnumber = double.TryParse(sRetVal, out dttxtcomponentwithnumer);
                    }
                    else if (nType == 2 && sLnkVal.ToUpper() == "TOTALDAYSINPAYMONTH")
                    {
                        //On 07/10/2021, for number of days
                        sRetVal = UtilityMember.NumberSet.ToDouble(sRetVal).ToString();
                        IsTextComponentwithnumber = true;
                    }
                    //On 10/07/2019, to fix dont round the salary days
                    else if (nType == 2 && sLnkVal.ToUpper() == PayRollExtraPayInfo.PAYING_SALARY_DAYS.ToString().Replace("_", "").ToString())
                    {
                        //double dttxtcomponentwithnumer = 0;
                        //IsTextComponentwithnumber = double.TryParse(sRetVal, out dttxtcomponentwithnumer);
                        sRetVal = UtilityMember.NumberSet.ToDouble(sRetVal).ToString();
                        IsTextComponentwithnumber = true;
                    }
                    //Temporary Purpose-----------------------------------------------------------------------------------------------


                    //if (nType == 0 || nType == 1) //Income or Deduction
                    if (nType == 0 || nType == 1 || nType == 3 || IsTextComponentwithnumber) //Income or Deduction or (text field with number value (Temporary purpose))
                    {

                        if (sRetVal != "")
                        {
                            if (nCompRnd == 0) //ceiling
                            {
                                sRetVal = Math.Ceiling(double.Parse(sRetVal)).ToString();
                            }
                            else if (nCompRnd == 1) //floor
                            {
                                sRetVal = Math.Floor(double.Parse(sRetVal)).ToString();
                            }
                            else if (nCompRnd == 2) //Round
                            {
                                sRetVal = Math.Round(double.Parse(sRetVal), 2).ToString(); //Round Value
                            }
                            else // None
                            {
                                sRetVal = Math.Round(double.Parse(sRetVal), 2).ToString(); //Round Value
                            }
                        }
                        else
                        {
                            sRetVal = "0.0";
                            sRetVal = Math.Round(double.Parse(sRetVal), 2).ToString();
                        }
                        if (bEquation) sVal1 = sRetVal;
                    }
                }

            EndLine:

                if (bEquation)
                {
                    sVal = sVal + sVal1;
                }
                else
                {
                    sVal = sRetVal;
                }

                return sVal;

                //				if (bEquation)
                //				{
                //					//It is a function which is used to evaluate the Expression
                //					nEvalExpr = this.EvaluateExpression(sVal1);
                //       
                //					if (nEvalExpr == 0) //Errors in Equation (Return Value : 0-Error, -1-Not error)
                //					{
                //						//MsgBox "Errors in Equation !", vbInformation, g_Message
                //					}
                //					//sVal1= dRetVal +""; the existing vb code
                //					sVal1 =nEvalExpr.ToString();
                //					sVal1 = RoundValue(double.Parse(sVal1), nCompRnd).ToString(); //Round Value
                //					//Check the Maximum Slab Value
                //					if (dMaxSlab > 0) sVal1 = ((double.Parse(sVal1) > dMaxSlab) ? dMaxSlab.ToString() : sVal1);
                //					sVal = sVal + sVal1+"";
                //					return sVal; //Return value for Equation component
                //				}
                //				else
                //				{
                //					return sRetVal; //Return value for non Equation component
                //				}


            }

            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
                return "0";
            }
        }

        private bool RecordsExistsinPrStattTemp(long nCompId)
        {
            bool isFound = false;
            try
            {
                this.dvDefVal.RowFilter = string.Empty;
                this.dvDefVal.RowFilter = "staffid =" + nStaffId + " and componentid = " + nCompId;
                isFound = (this.dvDefVal.Count > 0);
                this.dvDefVal.RowFilter = string.Empty;
            }
            catch(Exception err)
            {
                this.dvDefVal.RowFilter = string.Empty;
                isFound = false;
                MessageRender.ShowMessage("Problem in getting default temp value " + err.Message);
            }
            return isFound;
        }

        private bool IsRangeComponent(long nCompId)
        {
            bool isFound = false;
            try
            {
                dtRangeComponents.DefaultView.RowFilter = string.Empty;
                dtRangeComponents.DefaultView.RowFilter = "COMPONENTID = " + nCompId;
                isFound = (dtRangeComponents.DefaultView.Count > 0);
                dtRangeComponents.DefaultView.RowFilter = string.Empty;
            }
            catch (Exception err)
            {
                dtRangeComponents.DefaultView.RowFilter = string.Empty;
                isFound = false;
                MessageRender.ShowMessage("Problem in getting default temp value " + err.Message);
            }
            return isFound;
        }

        /// <summary>
        /// On 06/09/2019, To retain Processed value to make faster for next component
        /// </summary>
        /// <param name="sDefVal"></param>
        /// <param name="nCompId"></param>
        private void RetainProcssedEQCompValue(string sProcessedValue, long nCompId)
        {
            DataView dv = dtProcessedEQCompValues.DefaultView;
            try
            {
                dv.RowFilter = "STAFFID =" + nStaffId + " AND COMPONENTID = " + nCompId;
                if (dtProcessedEQCompValues != null && dv.Count == 0)
                {
                    DataRowView drv = dv.AddNew();
                    drv["STAFFID"] = nStaffId;
                    drv["COMPONENTID"] = nCompId;
                    drv["COMPVALUE"] = sProcessedValue;
                    drv.EndEdit();
                }
                //else if (dv.Count > 0)
                //{
                //    DataRowView drv = dv[0];
                //    drv.BeginEdit();
                //    drv["COMPVALUE"] = sProcessedValue;
                //    drv.EndEdit();
                //}
                dv.RowFilter = string.Empty;
            }
            catch (Exception err)
            {
                dv.RowFilter = string.Empty;
            }
        }

        /// <summary>
        /// On 06/09/2019, To reset Processed value to calculate again for range processs
        /// </summary>
        /// <param name="sDefVal"></param>
        /// <param name="nCompId"></param>
        private void ResetProcssedEQCompValue(long nCompId)
        {
            DataView dv = dtProcessedEQCompValues.DefaultView;
            try
            {
                dv.RowFilter = "STAFFID =" + nStaffId + " AND COMPONENTID = " + nCompId;
                if (dtProcessedEQCompValues != null && dv.Count == 1)
                {
                    dv.Delete(0);
                }
                dv.RowFilter = string.Empty;
            }
            catch (Exception err)
            {
                dv.RowFilter = string.Empty;
            }
        }

        /// <summary>
        /// On 06/09/2019, To retain Processed value to make faster for next component
        /// 
        /// If null returns, it means not processed
        /// </summary>
        /// <param name="sDefVal"></param>
        /// <param name="nCompId"></param>
        private string GetProcssedEQCompValue(long nCompId)
        {
            string ProcessedValue = null;
            DataView dv = dtProcessedEQCompValues.DefaultView;
            try
            {
                dv.RowFilter = "STAFFID =" + nStaffId + " AND COMPONENTID = " + nCompId;
                if (dtProcessedEQCompValues != null && dv.Count > 0)
                {
                    ProcessedValue = dv[0]["COMPVALUE"].ToString();
                }
                else
                {
                    ProcessedValue = null;
                }
                dv.RowFilter = string.Empty;
            }
            catch (Exception err)
            {
                ProcessedValue = null;
                dv.RowFilter = string.Empty;
            }
            return ProcessedValue;
        }

        /// <summary>
        /// On 24/09/2019, To retain Processed Actual value without applying no of paying salary
        /// (For STAFF PF report)
        /// </summary>
        /// <param name="sDefVal"></param>
        /// <param name="nCompId"></param>
        private void RetainProcssedActualValue(string sProcessedActualValue, long nCompId)
        {
            DataView dv = dtActualCompValues.DefaultView;
            try
            {
                dv.RowFilter = "STAFFID =" + nStaffId + " AND COMPONENTID = " + nCompId;
                if (dtActualCompValues != null && dv.Count == 0) //dtProcessedEQCompValues 
                {
                    DataRowView drv = dv.AddNew();
                    drv["STAFFID"] = nStaffId;
                    drv["COMPONENTID"] = nCompId;
                    drv["COMPVALUE"] = sProcessedActualValue;
                    drv.EndEdit();
                }
                //else if (dv.Count > 0)
                //{
                //    DataRowView drv = dv[0];
                //    drv.BeginEdit();
                //    drv["COMPVALUE"] = sProcessedValue;
                //    drv.EndEdit();
                //}
                dv.RowFilter = string.Empty;
            }
            catch (Exception err)
            {
                dv.RowFilter = string.Empty;
            }
        }

        /// <summary>
        /// On 24/09/2019, To retain Processed Actual value without applying no of paying salary
        /// (For STAFF PF report)
        /// If null returns, it means not processed
        /// </summary>
        /// <param name="sDefVal"></param>
        /// <param name="nCompId"></param>
        private string GetProcssedActualCompValue(long nCompId)
        {
            string ProcessedActualValue = string.Empty;
            DataView dv = dtActualCompValues.DefaultView;
            try
            {
                dv.RowFilter = "STAFFID =" + nStaffId + " AND COMPONENTID = " + nCompId;
                if (dtActualCompValues != null && dv.Count > 0) //dtProcessedEQCompValues 
                {
                    ProcessedActualValue = dv[0]["COMPVALUE"].ToString();
                }
                else
                {
                    ProcessedActualValue = string.Empty;
                }
                dv.RowFilter = string.Empty;
            }
            catch (Exception err)
            {
                ProcessedActualValue = string.Empty;
                dv.RowFilter = string.Empty;
            }
            return ProcessedActualValue;
        }
      

        //This function is used to get the default value from the 'PRStaffTemp' Table
        //If the Record is not found or value is empty Get the value from 'PRComponent' and Insert the value to 'PRStaffTemp' Table
        private string GetDefaultValue(string sDefVal, long nCompId)
        {
            string sRetVal = "";
            bool isdefaultvaluefound = false;
            this.dvDefVal.RowFilter = "staffid =" + nStaffId + " and componentid = " + nCompId;
            //if (dvDefVal.Table != null && dvDefVal.Table.Rows.Count > 0)
            //{
            //    var ResDefValues = (from SelectedItems in dvDefVal.ToTable().AsEnumerable()
            //                        where (SelectedItems.Field<System.UInt32>("staffid") == nStaffId
            //                                      && SelectedItems.Field<System.UInt32>("componentid") == nCompId)
            //                        select SelectedItems);
            //    if (ResDefValues.Count() > 0)
            //    {
            //        DataTable dtResDefValues = ResDefValues.CopyToDataTable();
            //        if (dtResDefValues != null && dtResDefValues.Rows.Count > 0)
            //        {
            //            sDefVal = dtResDefValues.Rows[0]["COMPVALUE"].ToString();
            //        }
            //    }
            //}
            
            if (dvDefVal.Table != null && dvDefVal.Count > 0)
            {
                sDefVal = dvDefVal[0]["COMPVALUE"].ToString();
                isdefaultvaluefound = true;
            }
            sRetVal = sDefVal.Trim();
            sRetVal = (sRetVal == "" ? "0" : sRetVal);

            if (!isdefaultvaluefound)
            {
                //using (DataManager dataManager = new DataManager(SQLCommand.Payroll.InsertNewDataValueForStaff))
                //{
                //    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                //    //  dataManager.Parameters.Add(sWhere);
                //    dataManager.Parameters.Add(dtComponent.PAYROLLIDColumn, nPayRollId.ToString());
                //    dataManager.Parameters.Add(dtComponent.STAFFIDColumn, nStaffId.ToString());
                //    dataManager.Parameters.Add(dtComponent.COMPONENTIDColumn, nCompId.ToString());
                //    dataManager.Parameters.Add(dtComponent.COMPVALUEColumn, sRetVal);
                //    resultArgs = dataManager.UpdateData();
                //    if (resultArgs.Success)
                //    {
                //        DataRowView drv = dvDefVal.AddNew();
                //        drv["PAYROLLID"] = nPayRollId;
                //        drv["STAFFID"] = nStaffId;
                //        drv["COMPONENTID"] = nCompId;
                //        drv["COMPVALUE"] = sRetVal;
                //        drv.EndEdit();
                //    }
                //    else
                //    {
                //        sRetVal=  string.Empty;
                //    }
                //}

                DataRowView drv = dvDefVal.AddNew();
                drv["PAYROLLID"] = nPayRollId;
                drv["STAFFID"] = nStaffId;
                drv["COMPONENTID"] = nCompId;
                drv["COMPVALUE"] = sRetVal;
                drv.EndEdit();
            }
            else
            {
                //using (DataManager datamanager = new DataManager(SQLCommand.Payroll.PayrollStaffUpdate))
                //{
                //    datamanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                //    datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                //    datamanager.Parameters.Add(dtComponent.CONDITIONColumn, sRetVal);
                //    datamanager.Parameters.Add(dtComponent.COMPONENTIDColumn, nCompId);
                //    datamanager.Parameters.Add(dtComponent.STAFFIDColumn, nStaffId);
                //    datamanager.Parameters.Add(dtComponent.PAYROLLIDColumn, nPayRollId);
                //    resultArgs = datamanager.UpdateData();
                //    if (resultArgs.Success)
                //    {
                //        DataRowView drv = dvDefVal[0];
                //        drv.BeginEdit();
                //        drv["COMPVALUE"] = sRetVal;
                //        drv.EndEdit();
                //    }
                //    else
                //    {
                //        sRetVal = string.Empty;
                //    }
                //}

                DataRowView drv = dvDefVal[0];
                drv.BeginEdit();
                drv["COMPVALUE"] = sRetVal;
                drv.EndEdit();
            }

            return sRetVal;
        }

        private string GetDefaultValueForFormula(long nCompId)
        {
            DataTable prdt = null;
            string defaultValue = string.Empty;
            using (DataManager datamanager = new DataManager(SQLCommand.Payroll.FetchprDefValueDetails, "StaffTemp"))
            {
                datamanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                datamanager.Parameters.Add(dtComponent.COMPONENTIDColumn, nCompId);
                datamanager.Parameters.Add(dtComponent.STAFFIDColumn, nStaffId);
                datamanager.Parameters.Add(dtComponent.PAYROLLIDColumn, nPayRollId);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                {
                    prdt = resultArgs.DataSource.Table;
                }
                if (prdt != null && prdt.Rows.Count > 0)
                {
                    string[] resultString = prdt.Rows[0][0].ToString().Split('.');
                    defaultValue = resultString[0];
                    defaultValue = defaultValue.Replace(",", "");
                }
            }
            return defaultValue;
        }
        
        /*	To Find the Given component Id and move the record pointer to selected position */
        public bool MoveComponent(long nCompId)
        {
            this.dvComponent.RowFilter = "";
            this.dvComponent = rsComponent.DefaultView;
            if (this.dvComponent.Count > 0)
            {
                this.dvComponent.RowFilter = "componentid = " + nCompId;
                //MessageBox.Show(this.dvComponent[0]["componentid"].ToString()); //To view the component id that is processed
            }
            return (this.dvComponent.Count > 0);
        }
        
        /* If condition is an if condition, then to evaluate the expression..*/
        private string EvaluateIFEquation(long nCompId, string sIFEQ, int nIFType, bool bApplySalaryDays)
        {
            int nOpr1, nOpr2, nLOpr = 0;

            long nCondCompId1 = 0, nCondCompId2;
            double dCondEQVal1 = 0.0, dCondEQVal2 = 0.0;
            double dCondVal1 = 0.0, dCondVal2 = 0.0, dRetVal = 0.0;
            string sEQ1 = "", sEQ2 = "", sEQ3 = "";
            string maxiSlab = "";
            string[] aIFEQ = null;
            bool bIF1 = false, bIF2 = false, bIF3 = false;

            aIFEQ = sIFEQ.Split((char)160);  //IF Equations are delimited by 'Chr$(160)'

            sEQ3 = this.RemoveBrace(aIFEQ[0], true);
            //...sugan nCondCompId1 = long.Parse(this.RemoveBrace(aIFEQ[0], true, true)); //Condition Component 1 (eg: IF 'a')
            if (MoveComponent(nCompId))
            { //Evaluate First Conditional Component Value
                if (dvComponent[0]["MaxSlap"].ToString() != "")
                    maxiSlab = dvComponent[0]["MaxSlap"].ToString();
                else
                    maxiSlab = "0.0";
                dCondEQVal1 = double.Parse(GetProcessedValue(nCompId, sEQ3,
                    "", double.Parse(maxiSlab + ""),
                    int.Parse(dvComponent[0]["CompRound"] + ""), 0, bApplySalaryDays));
            }
            nOpr1 = int.Parse(this.RemoveBrace(aIFEQ[1], true));
            dCondVal1 = double.Parse(this.RemoveBrace(aIFEQ[2], true));
            /* Common Condition for all IF Type (There must be atleast on condition in the IF equation)
             * Relational Operators (nOpr1) Id : 1 =, 2 >, 3 <, 4 >=, 5 <=, 6 <>
             * Find out, which condition is matched for calculated value
             * */
            switch (nOpr1)
            {
                case 0:// =
                    bIF1 = (dCondEQVal1 == dCondVal1);
                    break;
                case 2: // >
                    bIF1 = (dCondEQVal1 > dCondVal1);
                    break;
                case 1: // <
                    bIF1 = (dCondEQVal1 < dCondVal1);
                    break;
                case 3: // >=
                    bIF1 = (dCondEQVal1 >= dCondVal1);
                    break;
                case 4: // <=
                    bIF1 = (dCondEQVal1 <= dCondVal1);
                    break;
                case 5: //!=
                    bIF1 = (dCondEQVal1 != dCondVal1);
                    break;
            }
            switch (nIFType)
            {
                case 1: //IF...ELSE
                case 3:
                    if (bIF1)
                    {
                        sEQ1 = this.RemoveBrace(aIFEQ[3], true, false);
                        if (this.MoveComponent(nCompId))
                        {
                            dRetVal = double.Parse(GetProcessedValue(nCompId, sEQ1, "", double.Parse(dvComponent[0]["MaxSlap"] + ""), int.Parse(dvComponent[0]["CompRound"] + ""), 0, bApplySalaryDays));
                        }
                    }
                    else if (nIFType == 3)
                    {// ELSE block True :Equation for 'ELSE' Block
                        sEQ2 = RemoveBrace(aIFEQ[4], true);
                        if (this.MoveComponent(nCompId))
                        {
                            dRetVal = double.Parse(GetProcessedValue(nCompId, sEQ2, "", double.Parse(dvComponent[0]["MaxSlap"] + ""), int.Parse(rsComponent.Rows[0]["CompRound"] + ""), 0, bApplySalaryDays));
                        }
                    }
                    break;
                case 2:
                case 4: //IF...AND, IF...AND...ELSE
                    nLOpr = int.Parse(RemoveBrace(aIFEQ[3], true)); //Logical Operator 'AND or OR
                    nCondCompId2 = long.Parse(RemoveBrace(aIFEQ[4], true));
                    if (MoveComponent(nCondCompId2))
                    {
                        dCondEQVal2 = double.Parse(GetProcessedValue(nCondCompId2, Strings.Trim(dvComponent[0]["equationid"].ToString() + ""), "", double.Parse(dvComponent[0]["maxslap"].ToString() + ""), int.Parse(dvComponent[0]["compround"].ToString() + ""), int.Parse(dvComponent[0]["ifcondition"].ToString() + ""), bApplySalaryDays));
                    }

                    //Second Operator in After AND/OR Condition in IF (Condition Bock After AND/OR in IF)
                    nOpr2 = int.Parse(this.RemoveBrace(aIFEQ[5], true));
                    dCondVal2 = double.Parse(RemoveBrace(aIFEQ[6], true)); //Condition Value 2
                    //Find out, which condition is matched for calculated value
                    switch (nOpr2)
                    {
                        case 0: // =
                            bIF2 = (dCondEQVal2 == dCondVal2);
                            break;
                        case 2:// >
                            bIF2 = (dCondEQVal2 > dCondVal2);
                            break;
                        case 1: // <
                            bIF2 = (dCondEQVal2 < dCondVal2);
                            break;
                        case 3: // >=
                            bIF2 = (dCondEQVal2 >= dCondVal2);
                            break;
                        case 4: // <=
                            bIF2 = (dCondEQVal2 <= dCondVal2);
                            break;
                        case 5: // !=
                            bIF2 = (dCondEQVal2 != dCondVal2);
                            break;
                    }
                    if (nLOpr == 2) //OR (Logical Operator)
                        bIF3 = bIF1 || bIF2;
                    else  // AND
                        bIF3 = bIF1 && bIF2;

                    if (bIF3)
                    {//Both Condition is True in IF...AND
                        sEQ2 = this.RemoveBrace(aIFEQ[7], true);
                        if (MoveComponent(nCompId)) // nCondCompId2
                            dRetVal = double.Parse(GetProcessedValue(nCompId, sEQ2, "", double.Parse(dvComponent[0]["MaxSlap"].ToString() + ""), int.Parse(dvComponent[0]["compround"].ToString() + ""), 0, bApplySalaryDays));
                    }
                    else if (nIFType == 4)
                    { // IF...AND...ELSE :Both Condition is False in IF...AND
                        sEQ2 = this.RemoveBrace(aIFEQ[8], true);
                        if (this.MoveComponent(nCompId)) //nCondCompId2
                            dRetVal = double.Parse(GetProcessedValue(nCompId, sEQ2, "", double.Parse(dvComponent[0]["MaxSlap"].ToString() + ""), int.Parse(dvComponent[0]["compround"].ToString() + ""), 0, bApplySalaryDays));
                    }
                    break;
            }
            return (dRetVal.ToString() + "");
        }	/*	To Find the Given Staff Id and move the record pointer to selected position */
        
        public bool FindStaff(long nStaffId)
        {
            dvStaff.RowFilter = "";
            if (rsStaff != null && rsStaff.Tables.Count > 0)
            {
                this.dvStaff = rsStaff.Tables[0].DefaultView;
                if (dvStaff.Count > 0)
                    dvStaff.RowFilter = "staffid = " + nStaffId;
            }
            return (dvStaff.Count > 0);
        }

        /* To Remove the Braces with 2 args */
        public string RemoveBrace(string sText, bool bBrace)
        {
            string sVal = "";
            sVal = sText;
            if (bBrace)// This Brace meant only for IF Equation
            {
                sVal = sVal.Replace("{", "");
                sVal = sVal.Replace("}", "");
            }
            return sVal;
        }

        /* To Remove the Braces with 3 args */
        public string RemoveBrace(string sText, bool bBrace, bool bSeparator)
        {
            string sVal = "";
            sVal = sText;
            if (bBrace)// This Brace meant only for IF Equation
            {
                sVal = sVal.Replace("{", "");
                sVal = sVal.Replace("}", "");
            }
            if (bSeparator)//separator or Component Id
            {
                sVal = sVal.Replace("<", "");
                sVal = sVal.Replace(">", "");
            }
            return sVal;
        }

        /* To Remove the Braces with 4 args */
        public string RemoveBrace(string sText, bool bBrace, bool bSeparator, int nCount)
        {
            string sVal = "";
            sVal = sText;
            if (bBrace)// This Brace meant only for IF Equation
            {
                sVal = Strings.Replace(sVal, "{", "", 1, nCount, CompareMethod.Text);
                sVal = Strings.Replace(sVal, "}", "", 1, nCount, CompareMethod.Text);
            }
            if (bSeparator)//separator or Component Id
            {
                sVal = Strings.Replace(sVal, "<", "", 1, nCount, CompareMethod.Text);
                sVal = Strings.Replace(sVal, ">", "", 1, nCount, CompareMethod.Text);
            }
            return sVal;
        }

        /* To get the link values from stfpersonal,stfservice,prloan tables..
         * here if SFldName is BasicPay , then should not be any space */
        private string GetLinkValue(string sFldName, bool bApplySalaryDays)
        {
            string sVal = "", sLoanName = "";
            int nIncM1, nIncM2, nIncM = 0, nPRM = 0;
            int nMin, nMax;
            DataSet Rs = new DataSet();
            if (!FindStaff(nStaffId))
                goto EndLine;

            if (Strings.UCase(Strings.Left(sFldName, 6)) == "LOAN :") // Type : Deduction, No:1
            {
                sLoanName = sFldName.Substring(7).Trim();
                sVal = this.GetLoanPaidAmount(sLoanName).ToString();
            }
            else if (sFldName.ToUpper() == "INCREMENTMONTH")
            {
                nIncM1 = int.Parse(dvStaff[0][0].ToString() + "");
                nIncM2 = int.Parse(dvStaff[0][0].ToString() + "");

                if ((clsGeneral.PAYROLLDATE == "") || (nIncM1 <= 0 && nIncM2 <= 0))
                    goto EndLine;
                nPRM = DateTime.Parse(clsGeneral.PAYROLLDATE).Month;

                // Get the Next Increment Date
                if (nIncM1 > 0 && nIncM2 > 0)
                {
                    nMin = (nIncM1 <= nIncM2) ? nIncM1 : nIncM2;
                    nMax = (nIncM1 >= nIncM2) ? nIncM1 : nIncM2;

                    if ((nMin < nPRM && nMax < nPRM) || (nMin >= nPRM && nMax >= nPRM))
                        nIncM = nMin;
                    else
                        if (nMin >= nPRM) nIncM = nMin;
                    if (nMax >= nPRM) nIncM = nMax;
                }
                else
                    nIncM = (nIncM1 > 0) ? nIncM1 : nIncM2;

                if (nIncM == 0)
                    goto EndLine;
                sVal = "01/" + nIncM + "/" + ((nIncM >= nPRM) ? DateTime.Parse(clsGeneral.PAYROLLDATE).Year : DateTime.Parse(clsGeneral.PAYROLLDATE).Year + 1);
            }
            else if (sFldName.ToUpper() == "BASICPAY")  // Type : Income, No:0
            {
                //On 30/07/2019, Update basic in payrolltemp
                //sVal = dvStaff[0]["basicpay"].ToString();
                if (BasicpayCompId > 0 && RecordsExistsinPrStattTemp(BasicpayCompId)) //On 14/08/2019, If there is no basic pay value in temp talbe, take it from staff service
                {
                    sVal = GetDefaultValue("", BasicpayCompId);
                }
                else
                {
                    sVal = dvStaff[0]["basicpay"].ToString();
                }

                //sVal = dvStaff[0][sFldName].ToString() + "";
                nIncM1 = string.IsNullOrEmpty(dvStaff[0]["PayIncM1"].ToString()) ? 0 : int.Parse(dvStaff[0]["PayIncM1"].ToString());
                nIncM2 = string.IsNullOrEmpty(dvStaff[0]["PayIncM2"].ToString()) ? 0 : int.Parse(dvStaff[0]["PayIncM2"].ToString());
                if ((bNewPayRollProcess) && ((nIncM1 == DateTime.Parse(clsGeneral.PAYROLLDATE).Month) || (nIncM2 == DateTime.Parse(clsGeneral.PAYROLLDATE).Month)))
                {
                    sVal = this.GetBasicPay().ToString();
                }

                //On 30/07/2019, if salary days component exists, get basic pay based on number of days
                RetainProcssedActualValue(sVal, BasicpayCompId); //Retain Basicpay Actual Comp Value
                if ((PayingSalaryDaysCompId > 0 || LOPDaysCompId > 0) && bApplySalaryDays)
                {  //On 07/03/2022, Paying Salary days or LOPDays
                    
                    //For Paying Salary Days
                    if (PayingSalaryDaysCompId > 0)
                    {
                        double StaffPayingSalaryDays = UtilityMember.NumberSet.ToDouble(GetDefaultValue("", PayingSalaryDaysCompId));
                        if (StaffPayingSalaryDays > 0)
                        {
                            double BasicPayWithNoOfDay = (this.UtilityMember.NumberSet.ToDouble(sVal) / TotalPayrollProcessingDays) * StaffPayingSalaryDays;
                            sVal = BasicPayWithNoOfDay.ToString();
                        }
                    } // For LOP Days
                    else if (LOPDaysCompId > 0)
                    {
                        sVal = AffectLOPDays(this.UtilityMember.NumberSet.ToDouble(sVal)).ToString();
                    }

                    
                }
                //-----------------------------------------------------------------------------------------------------------------
            }
            else if (sFldName.ToUpper() == "DEPTID")
            {
                sVal = dvStaff[0]["DepartmentId"].ToString() + "";
            }
            else if (sFldName == "Account_Number")
            {
                sVal = dvStaff[0]["Account_Number"].ToString() + "";
            }
            else if (sFldName.ToUpper() == "TOTALDAYSINPAYMONTH") //On 01/10/2021, Total Days in Paymonth
            {
                sVal = TotalDaysInPayMonth.ToString();
            }
            else if (sFldName.ToUpper() == PayRollExtraPayInfo.YOS.ToString().ToUpper())
            {
                double PreviousYOS = 0;
                if (dvStaff[0][sFldName]!=null)
                {
                    sVal = dvStaff[0][sFldName].ToString() + "";
                    PreviousYOS = NumberSet.ToDouble(sVal);
                }

                double Institution_YOS = 0;
                if (dvStaff[0]["Institution_YOS"]!=null)
                {
                    Institution_YOS = NumberSet.ToDouble(dvStaff[0]["Institution_YOS"].ToString() + "");
                }
                sVal = (PreviousYOS + Institution_YOS).ToString();
            }
            else if (sFldName.ToUpper() == "DATEOFJOIN")
            { //on 14/02/2023, To format date of join
                sVal = dvStaff[0][sFldName].ToString() + "";  // Date of join
                if (!string.IsNullOrEmpty(sVal))
                {
                    sVal = UtilityMember.DateSet.ToDate(sVal, false).ToShortDateString();
                }
            }
            else
                sVal = dvStaff[0][sFldName].ToString() + "";  // Type : Text, No:2
        EndLine:
            return sVal;
        }
        /* If the increment month is current payroll month,
		 *  increase the basic pay depends upon the scale of pay */
        private double GetBasicPay()
        {
            string sScaleofPay = "";
            string[] aScaleofPay = new string[50];
            double dBPay = 0.0, dCurBP = 0.0;
            long nServiceId = 0;

            if (dvStaff[0]["BasicPay"].ToString() != "")
                dBPay = double.Parse(dvStaff[0]["BasicPay"].ToString() + "");
            else dBPay = 0.0;
            dCurBP = dBPay;
            sScaleofPay = Strings.Trim(dvStaff[0]["ScaleofPay"].ToString() + "");
            nServiceId = long.Parse(dvStaff[0]["ServiceId"].ToString() + "");

            if (sScaleofPay != "")
            {
                //ScaleofPay 5000-500-25000 (0-Basic Bay,1-Increment, 2-Max Slab)
                aScaleofPay = sScaleofPay.Split('-');
                if (aScaleofPay.GetUpperBound(0) == 2)
                {
                    //find if BPay Reaches the Maximum slab
                    if (UtilityMember.NumberSet.ToDouble(aScaleofPay[1].ToString()) > 0) //if (int.Parse(aScaleofPay[1].ToString()) > 0)
                    {
                        if (UtilityMember.NumberSet.ToDouble(aScaleofPay[2].ToString()) <= 0) //if (int.Parse(aScaleofPay[2].ToString()) <= 0)
                        {
                            dBPay = dBPay + UtilityMember.NumberSet.ToDouble(aScaleofPay[1].ToString());//int.Parse(aScaleofPay[1].ToString());
                        }
                        else
                        {
                            //if (dBPay + int.Parse(aScaleofPay[1].ToString()) <= int.Parse(aScaleofPay[2].ToString()))
                            if (dBPay + UtilityMember.NumberSet.ToDouble(aScaleofPay[1].ToString()) <= UtilityMember.NumberSet.ToDouble(aScaleofPay[2].ToString()))
                            {
                                dBPay = dBPay + UtilityMember.NumberSet.ToDouble(aScaleofPay[1].ToString());// int.Parse(aScaleofPay[1].ToString());
                            }
                        }
                    }
                    if (dBPay != dCurBP)
                    {
                        this.UpdateBasicPay(dBPay, nServiceId);
                    }
                }// end of if(aScaleofPay.GetUpperBound(0)== 2)
            } // end of if(sScaleofPay != "")
            return dBPay;
        }
        public void UpdateBasicPay(double dPay, long nServiceId)
        {
            string sSql = "";
            //In case there is any Increment for this month, also Increase Basic Pay for the staff
            //sSql = "update stfservice set pay = " + dPay + " where serviceid = " + nServiceId;
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.UpdateBasicPay))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtComponent.PAYColumn, dPay);
                    dataManager.Parameters.Add(dtComponent.SERVICEIDColumn, nServiceId);
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        /* If condition is an if condition, then to evaluate the expression..*/
        private string EvaluateEquation(long nCompId, string sEQ, string sVal, int nIFType,
            double dMaxSlab, int nCompRnd, bool bApplySalaryDays)
        {
            int i, nPos = 0, nPos1;
            double nEvalExpr = 0;
            int nIFType1, nCompRnd1;
            long nCompId1;
            double dMaxSlab1 = 0.0;
            //string[] getEQ;
            //double dRetVal;
            //string formulaGroup ="";
            string sCompId = "";
            string sVal1 = "", sEQ1 = "", sCharExp = "";

            //On 06/09/2019, To retain Processed value to make faster for next component
            //If null returns, force to process
            //Modify :: On 27/11/2020, for IF codition Component
            //string processedvalue = GetProcssedEQCompValue(nCompId);
            bool IsIFconditionComp = isIFConditionComponent(nCompId);
            string processedvalue = null;
            if (!IsIFconditionComp)
            {
                processedvalue = GetProcssedEQCompValue(nCompId);
            }

            if (processedvalue == null || !bApplySalaryDays)
            {
                //if (!this.MoveComponent(nCompId)) goto EndLine;
                clscomp = new clsPrComponent();

                for (i = 1; i <= sEQ.Length; i++)
                {
                    //Evaluate the Inter linked Equation and concatinate each character from the
                    //Resulted equation to form as Expression
                    sCharExp = Strings.Mid(sEQ, i, 1); //sEQ.Substring(i,1);

                    if (sCharExp != "<" && sCharExp != ">")
                    {
                        sVal1 = sVal1 + sCharExp;
                    }
                    else //Find out whether component is attached with the equation
                    //components are delimited between the brace '<' and '>'
                    {
                        nPos = Strings.InStr(i, sEQ, "<", CompareMethod.Text); // sEQ.IndexOf("<",i);
                        if (nPos > 0)
                        {
                            nPos1 = Strings.InStr(i, sEQ, ">", CompareMethod.Text); //sEQ.IndexOf(">",i);
                            if (nPos1 > 0)
                            {
                                sCompId = Strings.Mid(sEQ, nPos, nPos1 - (nPos - 1));
                                //sCompId = sEQ.Substring(nPos, nPos1 - (nPos - 1));
                                nCompId1 = long.Parse(this.RemoveBrace(sCompId, false, true, 1));
                                int IsComponentMapped = CheckComponentMappedtoGroup(nCompId1);

                                if (IsComponentMapped > 0) //true if component is mapped to group
                                {
                                    if (this.MoveComponent(nCompId1))
                                    {
                                        //These values are only for Equation Type Field
                                        //sEQ1="(<9>-<14>)";
                                        //clscomp = new clsPrComponent();
                                        if (clscomp.FetchRangeComponent(NumberSet.ToInteger(nCompId1.ToString())) > 0)
                                        {
                                            if ((this.dvComponent[0]["Type"].ToString() == "1" || this.dvComponent[0]["Type"].ToString() == "3")
                                                && this.dvComponent[0]["DefValue"].ToString() == "0"
                                                && this.dvComponent[0]["LinkValue"].ToString().ToString() == ""
                                                && this.dvComponent[0]["Equation"].ToString().ToString() == ""
                                                && this.dvComponent[0]["EquationId"].ToString() == "")
                                            {
                                                sVal1 += GetRangeofValue(nStaffId, nCompId1, bApplySalaryDays);
                                                i = nPos1;
                                            }
                                        }
                                        else
                                        {
                                            sEQ1 = Strings.Trim(this.dvComponent[0]["EquationId"] + "");
                                            //MessageBox.Show(rsComponent.Tables[0].Rows[i]["CompRound"].ToString());
                                            if (this.dvComponent[0]["CompRound"].ToString() != "")
                                                nCompRnd1 = int.Parse(this.dvComponent[0]["CompRound"].ToString() + "");
                                            else
                                                nCompRnd1 = 0;
                                            //MessageBox.Show(rsComponent.Tables[0].Rows[i]["MaxSlap"].ToString());
                                            if (this.dvComponent[0]["MaxSlap"].ToString() != "")
                                                dMaxSlab1 = double.Parse(this.dvComponent[0]["MaxSlap"].ToString() + "");
                                            else
                                                dMaxSlab1 = 0.0;
                                            if (this.dvComponent[0]["IFCondition"].ToString() != "")
                                                nIFType1 = int.Parse(this.dvComponent[0]["IFCondition"].ToString() + "");
                                            else
                                                nIFType1 = 0;
                                            //Recursive function to calculate the interlinked Components value
                                            sVal1 = GetProcessedValue(nCompId1, sEQ1, sVal1, dMaxSlab1, nCompRnd1, nIFType1, bApplySalaryDays);
                                            i = nPos1;
                                        }
                                    }
                                }
                                else
                                {
                                    sVal1 = sVal1 + "0.00";
                                    i = nPos1;
                                }
                            }
                        }
                    }
                }

            EndLine:

                //It is a function which is used to evaluate the Expression
                nEvalExpr = this.EvaluateExpression(sVal1);

                if (nEvalExpr == 0) //Errors in Equation (Return Value : 0-Error, -1-Not error)
                {
                    //MsgBox "Errors in Equation !", vbInformation, g_Message
                }
                //sVal1= dRetVal +""; the existing vb code
                sVal1 = nEvalExpr.ToString();
                sVal1 = RoundValue(double.Parse(sVal1), nCompRnd).ToString(); //Round Value
                //Check the Maximum Slab Value
                if (dMaxSlab > 0) sVal1 = ((double.Parse(sVal1) > dMaxSlab) ? dMaxSlab.ToString() : sVal1);
                sVal = sVal1; // sVal + sVal1 + "";

                //On 06/09/2019, Retrain process values to avoid reprocess for a component
                //Modify :: On 27/11/2020, for IF codition Component
                //RetainProcssedEQCompValue(sVal, nCompId);
                if (!IsIFconditionComp)
                {
                    RetainProcssedEQCompValue(sVal, nCompId);
                }
                //----------------------------------------------------------------------
            }
            else
            {
                sVal = GetProcssedEQCompValue(nCompId);
            }

            return sVal; //Return value for Equation component
        }
        /*To evaluate a given expression and to result the value */
        private double EvaluateExpression(string sVal1)
        {
            try
            {
                object objCheck = (clsGeneral.ObjExcel).Evaluate(sVal1);

                if (Convert.ToInt32(objCheck) >= 0)
                {
                    //return Convert.ToInt32(objCheck);
                    return Convert.ToDouble(objCheck);
                }
                else
                {
                    return 0.00; //Invalid formula
                }
            }
            catch (Exception ex)
            {
                //  MessageBox.Show("Evaluate Expression Err Msg: " + ex.ToString(), "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0.00;
            }
        }
        private double GetRoundValue(double dValue)
        {
            return double.Parse(Strings.Format(dValue, "##0"));
        }

        private double RoundValue(double dAmount, int nRType)
        {
            double dAmt;
            dAmt = dAmount;
            switch (nRType)
            {
                case 0: //Ceiling (Eg: 51.15 => 52)
                    dAmt = System.Math.Ceiling(dAmt);
                    break;
                case 1: //Floor (Eg: 51.99 => 51)
                    dAmt = Conversion.Int(dAmt);
                    break;
                case 2: //Rounded to Next or Previous Integer (Eg: 51.49 => 51 or 51.50 => 52)
                    //On 10/03/2023, to round up properly to next rounded integer
                    //dAmt = System.Math.Round(dAmt);
                    dAmt = System.Math.Round(dAmt, MidpointRounding.AwayFromZero);
                    break;
            }
            return dAmt;
        }
        public double GetLoanPaidAmount(string sLoanName)
        {
            long nLoanId = 0, nLoanGetId = 0;
            double dLPAmt = 0.0;
            string sDate = "", sSql = "";
            string sGetDate = "";

            nLoanId = GetLoanId(sLoanName);
            if (nLoanId <= 0) goto EndLine;

            /* This loop calculates the Loans. Somebody got the same loan type
             * at more than one time which is not in completed state
             */
            bNewPayRollProcess = true;//changed by sugan
            if (bNewPayRollProcess)
            {
                DateTime dttemp = DateTime.Parse(clsGeneral.PAYROLLDATE, clsGeneral.DATE_FORMAT);
                dttemp = dttemp.AddMonths(1);
                sDate = dttemp.AddDays(-1).ToShortDateString();
                sGetDate = DateTime.Parse(clsGeneral.PAYROLLDATE, clsGeneral.DATE_FORMAT).ToShortDateString();
                //sSql = "SELECT prloangetid from prloanget WHERE " +
                //    "loanId = " + nLoanId + " AND staffid = " + nStaffId +
                //    " AND FromDate <= " + "to_date('" + sDate + "','DD/MM/YYYY') " +
                //    " AND completed = 0 AND prloangetid IN (SELECT prloangetid " +
                //    "FROM prloanpaid WHERE paiddate = " +
                //    "to_date('" + sGetDate + "','DD/MM/YYYY')" + ")";
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollLoanId))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    dataManager.Parameters.Add(dtComponent.LOANIDColumn, nLoanId);
                    dataManager.Parameters.Add(dtComponent.STAFFIDColumn, nStaffId);
                    dataManager.Parameters.Add(dtComponent.FROMDATEColumn, clsGeneral.GetMySQLDateTime(sDate, DateDataType.Date));
                    dataManager.Parameters.Add(dtComponent.TODATEColumn, clsGeneral.GetMySQLDateTime(sGetDate, DateDataType.Date));
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                    {
                        rsLoanN = resultArgs.DataSource.Table;
                    }
                }
                //dh.createDataSet(sSql, "LoanPaid");
                //rsLoanN = dh.getDataSet();
                if (rsLoanN.Rows.Count == 1)
                    bPAYROLL_DUPLICATED = true;
                else
                    bPAYROLL_DUPLICATED = false;
                this.ImportLoanInstallmentAmount(nLoanId, 0);
            }
            else
            { // This Block Insert the Loan Due amount in the Current Payroll
                // if the Loan is added after Creating New Payroll
                //sDate = DateAdd("m", 1, DateValue(g_PayRollDate)) - 1
                //sDate = DateTime.Today.Date.Add(objmodPay.g_PayRollDate) - 1;

                DateTime dttemp = DateTime.Parse(clsGeneral.PAYROLLDATE, clsGeneral.DATE_FORMAT);
                dttemp = dttemp.AddMonths(1);
                sDate = dttemp.AddDays(-1).ToShortDateString();
                sGetDate = DateTime.Parse(clsGeneral.PAYROLLDATE, clsGeneral.DATE_FORMAT).ToShortDateString();
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchLoanDueAmount, "LoanPaid"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    dataManager.Parameters.Add(dtComponent.LOANIDColumn, nLoanId);
                    dataManager.Parameters.Add(dtComponent.STAFFIDColumn, nStaffId);
                    dataManager.Parameters.Add(dtComponent.FROMDATEColumn, clsGeneral.GetMySQLDateTime(sDate, DateDataType.Date));
                    dataManager.Parameters.Add(dtComponent.TODATEColumn, clsGeneral.GetMySQLDateTime(sGetDate, DateDataType.Date));
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                    {
                        rsLoanN = resultArgs.DataSource.Table;
                    }
                }
                //sSql = "SELECT prloangetid from prloanget WHERE " +
                //    "loanId = " + nLoanId + " AND staffid = " + nStaffId +
                //    " AND FromDate <= " + "to_date('" + sDate + "','DD/MM/YYYY') " +
                //    " AND completed = 0 AND prloangetid NOT IN (SELECT prloangetid " +
                //    "FROM prloanpaid WHERE paiddate = " +
                //    "to_date('" + sGetDate + "','DD/MM/YYYY')" + ")";

                //dh.createDataSet(sSql, "LoanPaid");
                //rsLoanN = dh.getDataSet();
                if (rsLoanN != null && rsLoanN.Rows.Count > 0)
                {
                    for (int i = 0; i < rsLoanN.Rows.Count; i++)
                    {
                        nLoanGetId = int.Parse(rsLoanN.Rows[i]["prloangetid"] + "");
                        ImportLoanInstallmentAmount(nLoanId, nLoanGetId);
                    }
                }
            }
            /*Get the Amount from the table 'PrLoanPaid (No need to Create new loan Installment amount)*/
            dvLoanP = rsLoanP.DefaultView;

            dvLoanP.RowFilter = "PaidDate = '" + Convert.ToDateTime(clsGeneral.PAYROLLDATE, clsGeneral.DATE_FORMAT).ToString("dd/MM/yyyy") + "' AND loanid = " + nLoanId + " AND staffid = " + nStaffId;

            for (int i = 0; i < dvLoanP.Count; i++)
            {
                //dLPAmt = dLPAmt + Convert.ToDouble(dvLoanP[i]["Amount"].ToString());
                dLPAmt = Convert.ToDouble(dvLoanP[i]["Amount"].ToString());//changed by sugan

            }

            dvLoanP.RowFilter = "";
        EndLine:
            return dLPAmt;
        }
        private long GetLoanId(string sLoanName)
        {
            long nLoanId = 0;
            if (rsLoan.Rows.Count == 0)
                goto EndLine;
            DataView dvLoan = new DataView(rsLoan);
            dvLoan.RowFilter = "LOAN_NAME ='" + sLoanName + "'";
            if (dvLoan.Count > 0)
            {
                for (int i = 0; i < dvLoan.Count; i++)
                {
                    nLoanId = long.Parse(dvLoan[i]["LOAN_ID"].ToString() + "");
                }
            }
            dvLoan.RowFilter = "";
        EndLine:
            return nLoanId;
        }/* Import Loan Installment Amount from Previous Payroll */
        private double ImportLoanInstallmentAmount(long nLoanId, long nLGetId)
        {
            int nInstallment, nMode, nCurInstallment = 0;
            long nLoanGetId;
            string sLoanGSQL, sLoanPSQL = "";
            double dLGAmt = 0.0, dLPAmt = 0.0, dInterest = 0.0, dCurInstallAmt = 0.0;//Commented by Pe to avoid warning
            double dTotLGAmt = 0.0, dTotPaidAmt = 0.0;
            string sDate = "", sGetDate = "";
            string sSql = "";

            DateTime dttemp = this.commem.DateSet.ToDate(clsGeneral.PAYROLLDATE, false);
            dttemp = dttemp.AddMonths(1);
            sDate = dttemp.AddDays(-1).ToString();

            DataView dvLoanGet = new DataView(rsLoanG);
            dvLoanGet.RowFilter = "LoanId = " + nLoanId + " AND StaffId = " + nStaffId +
                " AND FromDate <= '" + this.commem.DateSet.ToDate(sDate, "yyyy/MM/dd") +
                "' AND Completed = '0' " +
                (nLGetId > 0 ? "AND prloangetid = " + nLGetId : "");

            for (int i = 0; i < dvLoanGet.Count; i++)
            {
                dLPAmt = 0;
                nLoanGetId = long.Parse(dvLoanGet[i]["PRLoanGetId"].ToString() + "");
                dLGAmt = double.Parse(dvLoanGet[i]["Amount"].ToString() + "");
                nInstallment = int.Parse(dvLoanGet[i]["Installment"].ToString() + "");
                nMode = int.Parse(dvLoanGet[i]["IntrestMode"].ToString() + "");
                dInterest = double.Parse(dvLoanGet[i]["Interest"].ToString() + "") / 100; //Interest Rate

                //Get Total Paid Amount
                GetTotalLoanPaid(nLoanGetId, nLoanId, ref dTotPaidAmt, ref nCurInstallment);
                //Get Installment amount based on Interest type and given Interest Rate
                dCurInstallAmt = GetInstallmentAmount(nMode, dLGAmt, dInterest, nInstallment, nCurInstallment, ref dTotLGAmt);

                if ((dTotLGAmt - dTotPaidAmt) >= dCurInstallAmt)  //dLGAmt
                    dLPAmt = dCurInstallAmt;
                else
                    dLPAmt = (dTotLGAmt - dTotPaidAmt); //dLGAmt

                /* Update the paid installment and the status, if the loan is completed */
                if (dLPAmt > 0)
                {
                    ////Insert into Loan Paid Table
                    //sSql = "insert into prloanpaid(loan_rowid,payrollid,loanid,prloangetid,staffid,paiddate,amount,installment)" +
                    //    " values(scq_payroll_loanpaid_id.nextval,<payrollid>,<loanid>,<prloangetid>,<staffid>,to_date('<paiddate>','dd/mm/yyyy'),<amount>,<installment>)";
                    //sSql = sSql.Replace("<payrollid>", clsGeneral.PAYROLL_ID.ToString());
                    //sSql = sSql.Replace("<loanid>", nLoanId.ToString());
                    //sSql = sSql.Replace("<prloangetid>", nLoanGetId.ToString());
                    //sSql = sSql.Replace("<staffid>", nStaffId.ToString());
                    //sGetDate = DateTime.Parse(clsGeneral.PAYROLLDATE, clsGeneral.DATE_FORMAT).ToShortDateString();
                    //sSql = sSql.Replace("<paiddate>", sGetDate);
                    //sSql = sSql.Replace("<amount>", dLPAmt.ToString());
                    //sSql = sSql.Replace("<installment>", nCurInstallment.ToString());
                    if (!bPAYROLL_DUPLICATED)
                    {
                        using (DataManager dataManager = new DataManager(SQLCommand.Payroll.InsertLoanPaidTable))
                        {
                            dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                            dataManager.Parameters.Add(dtComponent.PAYROLLIDColumn, clsGeneral.PAYROLL_ID.ToString());
                            dataManager.Parameters.Add(dtComponent.LOANIDColumn, nLoanId.ToString());
                            dataManager.Parameters.Add(dtComponent.PRLOANGETIDColumn, nLoanGetId.ToString());
                            dataManager.Parameters.Add(dtComponent.STAFFIDColumn, nStaffId.ToString());
                            sGetDate = DateTime.Parse(clsGeneral.PAYROLLDATE, clsGeneral.DATE_FORMAT).ToShortDateString();
                            dataManager.Parameters.Add(dtComponent.PAIDDATEColumn, clsGeneral.GetMySQLDateTime(sGetDate, DateDataType.DateTime));
                            dataManager.Parameters.Add(dtComponent.AMOUNTColumn, dLPAmt.ToString());
                            dataManager.Parameters.Add(dtComponent.INSTALLMENTColumn, nCurInstallment.ToString());


                            resultArgs = dataManager.UpdateData();
                            if (!resultArgs.Success)
                                return 0;
                        }
                    }
                    //if (!bPAYROLL_DUPLICATED)
                    //    dh.insertRecord(sSql);

                    using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchpayrollLoanPaid, "LoanPaid"))
                    {
                        dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        resultArgs = dataManager.FetchData(DataSource.DataTable);
                        if (resultArgs.Success)
                        {
                            rsLoanP = resultArgs.DataSource.Table;
                            dvLoanP = rsLoanP.DefaultView;
                        }
                    }
                    //sLoanPSQL = "SELECT * FROM PRLoanPaid ORDER BY PRLoanGetId";
                    //dh.createDataSet(sLoanPSQL, "LoanPaid");
                    //rsLoanP = dh.getDataSet();
                    //dvLoanP = rsLoanP.Tables[0].DefaultView;
                    object SqlQuery = SQLCommand.Payroll.UpdateInstallmentByLoanId;
                    if ((dTotPaidAmt + dLPAmt) == dTotLGAmt)
                    {
                        SqlQuery = SQLCommand.Payroll.UpdateInstallmentStatusByLoanId;
                    }
                    if (!bPAYROLL_DUPLICATED)
                    {
                        bool Updated = false;
                        using (DataManager dataManager = new DataManager(SqlQuery))
                        {
                            dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                            dataManager.Parameters.Add(dtComponent.PRLOANGETIDColumn, nLoanGetId);
                            dataManager.Parameters.Add(dtComponent.CURRENTINSTALLMENTColumn, nCurInstallment);
                            resultArgs = dataManager.UpdateData();
                            if (resultArgs.Success && resultArgs.RowsAffected > 0)
                            {
                                Updated = true;
                            }
                        }
                    }
                    //sSql = "update prloanget set CurrentInstallment= " + nCurInstallment + " where prloangetid =" + nLoanGetId;
                    //if ((dTotPaidAmt + dLPAmt) == dTotLGAmt)
                    //{
                    //    sSql = "update prloanget set CurrentInstallment= " + nCurInstallment + ", completed =1 " +
                    //           "where prloangetid =" + nLoanGetId;
                    //}
                    //if (!bPAYROLL_DUPLICATED)
                    //    UpdatePayrollLoanInstallmentStatus(nCurInstallment, nLoanGetId);

                    //dh.createDataSet("select * from prloanget", "LoanGet");
                    //rsLoanG = dh.getDataSet();

                    rsLoanG = FetchLoanDetails();
                    dvLoanGet = new DataView(rsLoanG);
                    dvLoanGet.RowFilter = "prloangetid = " + nLoanGetId;
                    bPAYROLL_DUPLICATED = false;

                }
            }
            return dLPAmt;
        }

        public ResultArgs UpdateComponentNamesInEquations(string OldComponent, string NewComponent, int ComponentId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.UpdateComponentinEquation, "payroll"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtComponent.COMPONENTIDColumn, ComponentId);
                dataManager.Parameters.Add(dtComponent.COMPONENTColumn, NewComponent);
                dataManager.Parameters.Add(dtComponent.PREVCOMPONENTNAMEColumn, OldComponent);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private DataTable FetchLoanDetails()
        {
            DataTable dtLoan = null;
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchLoanDetails, "LoanGet"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                {
                    dtLoan = resultArgs.DataSource.Table;
                }
            }
            return dtLoan;
        }

        /// <summary>
        /// On 16/02/2022, 
        /// 
        /// 1. Check given component statutory compliance and staff applicable for it
        /// 
        /// </summary>
        private bool IsStaffApplicableForStatutoryCompliance(long nStaffId, int processcomponenttype)
        {
            bool rtn = false;

            try
            {
                //1. Check given component statutory compliance and staff applicable for it
                if (dtApplicableStatutoryCompliance != null && dtApplicableStatutoryCompliance.Rows.Count>0)
                {
                    dtApplicableStatutoryCompliance.DefaultView.RowFilter= string.Empty;
                    dtApplicableStatutoryCompliance.DefaultView.RowFilter = "STAFF_ID = " + nStaffId + " AND STATUTORY_COMPLIANCE=" + processcomponenttype;
                    rtn = (dtApplicableStatutoryCompliance.DefaultView.Count > 0);
                    dtApplicableStatutoryCompliance.DefaultView.RowFilter = string.Empty;
                }
            }
            catch (Exception err)
            {
                dtApplicableStatutoryCompliance.DefaultView.RowFilter = string.Empty;
                MessageRender.ShowMessage(err.Message);
                rtn = false;
            }

            return rtn;
        }

        /// <summary>
        /// //If Process component is statutory compliance (ESF/ESI/PT), check those statutory compliance are applicable for Staff
        /// </summary>
        /// <param name="nStaffId"></param>
        /// <param name="processcomponenttype"></param>
        /// <returns></returns>
        private bool CanProceedForStaffProcess(long nStaffId, int processcomponenttype)
        {
            bool rtn = true;
            try
            {
                //If Process component is statutory compliance (ESF/ESI/PT), check those statutory compliance are applicable for Staff
                bool canProceed = ((processcomponenttype == (Int32)PayRollProcessComponent.None || processcomponenttype == (Int32)PayRollProcessComponent.GrossWages ||
                                             processcomponenttype == (Int32)PayRollProcessComponent.NetPay || processcomponenttype == (Int32)PayRollProcessComponent.Deductions));

                rtn = (canProceed || IsStaffApplicableForStatutoryCompliance(nStaffId, processcomponenttype));
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);   
            }
            return rtn;
        }

        //private bool UpdatePayrollLoanInstallmentStatus(int currentInstallment, long loanId)
        //{
        //    bool Updated = false;
        //    using (DataManager dataManager = new DataManager(SQLCommand.Payroll.UpdateInstallmentStatusByLoanId))
        //    {
        //        resultArgs = dataManager.UpdateData();
        //        if (resultArgs.Success && resultArgs.RowsAffected >0)
        //        {
        //            Updated = true;
        //        }
        //    }
        //    return Updated;
        //}

        private void GetTotalLoanPaid(long nLoanGetId, long nLoanId, ref double dTotPaidAmt, ref int nCurInstallment)
        {
            string sSql = "";
            DataTable rsLoanSum = new DataTable();
            string sGetDate = "";
            sGetDate = DateTime.Parse(clsGeneral.PAYROLLDATE, clsGeneral.DATE_FORMAT).ToShortDateString();
            //Findout the Total Paid amount for the existing loan is finished or not
            //sSql = "SELECT SUM(Amount) as \"TotPaidAmount\",count(Amount) as \"NoofInstallment\" " +
            //    " from prloanpaid where prloangetid =" + nLoanGetId +
            //    " and paiddate <= " + "to_date('" + sGetDate + "','DD/MM/YYYY')" + " AND loanid = " + nLoanId +
            //    " and staffid = " + nStaffId;
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPaidAmountForExistingPayroll, "LoanPaid"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtComponent.PRLOANGETIDColumn, nLoanGetId);
                dataManager.Parameters.Add(dtComponent.PAIDDATEColumn, clsGeneral.GetMySQLDateTime(sGetDate, DateDataType.Date));
                dataManager.Parameters.Add(dtComponent.LOANIDColumn, nLoanId);
                dataManager.Parameters.Add(dtComponent.STAFFIDColumn, nStaffId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                {
                    rsLoanSum = resultArgs.DataSource.Table;
                }
            }
            //dh.createDataSet(sSql, "LoanPaid");
            //rsLoanSum = dh.getDataSet();

            nCurInstallment = 1;
            if (rsLoanSum != null)
            {
                for (int i = 0; i < rsLoanSum.Rows.Count; i++)
                {
                    if (rsLoanSum.Rows[i]["TotPaidAmount"].ToString().Trim() != "")
                        dTotPaidAmt = double.Parse(rsLoanSum.Rows[i]["TotPaidAmount"].ToString() + "");
                    else
                        dTotPaidAmt = 0.0;
                    if (rsLoanSum.Rows[i]["NoofInstallment"].ToString().Trim() != "")
                        nCurInstallment = int.Parse(rsLoanSum.Rows[i]["NoofInstallment"].ToString() + "") + 1;
                    else
                        nCurInstallment = 0 + 1;
                }
            }
        }
        /* Calculate Installment amount for the given Interest type and interest Rate*/
        private double GetInstallmentAmount(int nMode, double dTotAmount, double dInterest,
            int nInstallment, int nCurInstallment, ref double dTotLGAmt)
        {
            int i, nCurIns;
            double dVal;

            nCurIns = (nCurInstallment > nInstallment) ? nInstallment : nCurInstallment;

            switch (nMode)//changed by sugan
            {
                case 0://Microsoft.VisualBasic.Financial.Pmt(Interest/12,Installment,-Amount,0,DueDate.BegOfPeriod)//changed by sugan
                    dVal = Math.Round(Financial.Pmt(dInterest / 12, nInstallment, -dTotAmount, 0, DueDate.EndOfPeriod), 2);
                    dTotLGAmt = dVal * nInstallment;
                    break;
                case 1: //Financial.IPmt(Interest/12,i,Installment,-Amount,0,DueDate.BegOfPeriod)
                    dVal = Math.Round((dTotAmount / nInstallment) + Financial.IPmt(dInterest / 12, nCurIns, nInstallment, -dTotAmount, 0, DueDate.EndOfPeriod), 2);
                    for (i = 1; i <= nInstallment; i++) //Loan Get Amount + Total Interest Amount
                        dTotLGAmt = dTotLGAmt + Math.Round((dTotAmount / nInstallment) + Financial.IPmt(dInterest / 12, i, nInstallment, -dTotAmount, 0, DueDate.EndOfPeriod), 2);
                    break;
                case 2:
                    dVal = Math.Round(Financial.PPmt(dInterest / 12, nCurIns, nInstallment, -dTotAmount, 0, DueDate.EndOfPeriod), 2);
                    for (i = 1; i <= nInstallment; i++) // Loan Get Amount + Total Interest Amount
                        dTotLGAmt = dTotLGAmt + Math.Round(Financial.PPmt(dInterest / 12, i, nInstallment, -dTotAmount, 0, DueDate.EndOfPeriod), 2);
                    break;
                case 3:
                default://There is no Interest for the Loan Amount
                    dVal = Math.Round((dTotAmount / nInstallment), 2);
                    dTotLGAmt = dTotAmount;
                    break;
            }
            return dVal;
        }	//While creating New compnent the Link Value for combo Box shows the Meaningfull name, but
        //we store the actual field name into the Table. For Edit to get the Meaningful caption, for
        //storage to get the actual field name.
        public string GetLinkName(string sName, bool bAlias)
        {
            int i;
            string sCaption = "";

            //0-Field Name, 1-Alias name

            if ((sName.Length >= 6) && (sName.IndexOf("Loan :", 0, 6) >= 0))  //Loan type
            {
                sCaption = sName;
            }
            else
            {
                for (i = 0; i < aLink.GetUpperBound(0); i++)
                {
                    if (sName == (bAlias ? aLink[i][0] : aLink[i][1]).ToString())
                    {
                        sCaption = bAlias ? aLink[i][1] : aLink[i][0];
                        break;
                    }
                }
            }
            return sCaption;
        }
        public void FillLinkValue(ComboBox objCombo, int nType, string sDefault)
        {
            try
            {
                int i;
                int nIndex;

                objCombo.Items.Clear();
                nIndex = 0;
                objCombo.Items.Add("");

                //nType = 0-Income, 1-Deduction, 2-Text
                if (nType == 0)      //Income
                {
                    objCombo.Items.Add("Basic Pay");
                    if (sDefault == "Basic Pay") nIndex = 0;
                }
                else if (nType == 1) //Deduction
                {
                    //dh.createDataSet("SELECT loanid,loanname FROM prloan ORDER BY loanname", "Loan");
                    using (DataManager dataManager = new DataManager(SQLCommand.Payroll.PayrollLoan, "Loan"))
                    {
                        dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        resultArgs = dataManager.FetchData(DataSource.DataTable);
                    }
                    if (resultArgs.Success)
                        rsLoan = resultArgs.DataSource.Table;
                    //rsLoan = dh.getDataSet();
                    for (i = 0; i < rsLoan.Rows.Count; i++)
                    {
                        objCombo.Items.Add("Loan : " + rsLoan.Rows[i]["LoanName"]);
                        if (sDefault == rsLoan.Rows[i]["LoanName"].ToString() + "")
                            nIndex = objCombo.SelectedIndex;
                    }
                }
                else                //Text
                {
                    for (i = 0; i < aLink.GetUpperBound(0); i++)
                    {
                        objCombo.Items.Add(aLink[i][1] + "");
                    }
                }
                objCombo.SelectedIndex = nIndex;
            }
            catch
            {

            }
        }

        public DataTable CheckDuplicateComponent()
        {
            //string strQuery = "SELECT COMPONENT FROM PRCOMPONENT ORDER BY COMPONENT";
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchDuplicateComponent, "PRCOMPONENT"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            if (resultArgs.Success)
                return resultArgs.DataSource.Table;
            else
                return null;
        }

        /// <summary>
        /// On 07/03/2022, In old database, Component EQUATION contains corrupated characters 'Â'
        /// We used to correct in updater, so check and prompt it.
        /// </summary>
        /// <returns></returns>
        public bool IsComponentEquationCorrupted()
        {
            bool rnt = false;
            DataTable dtComponent = CheckDuplicateComponent();
            if (dtComponent != null && dtComponent.Rows.Count > 0)
            {
                dtComponent.DefaultView.RowFilter = "EQUATION LIKE '%Â%'";
                rnt = (dtComponent.DefaultView.Count>0);
            }
            return rnt;
        }

        public DataTable CheckDuplicateCaption()
        {
            //string strQuery = "SELECT CAPTION FROM PRCOMPONENT ORDER BY CAPTION";
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchDuplicateComponent, "PRCOMPONENT"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            if (resultArgs.Success)
                return resultArgs.DataSource.Table;
            else
                return null;
        }

        //To Check the Components are Mapped to Groups or Not Validation While process the Payroll

        public int CheckComponentsMappedORNot(string groupid, long payrollid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.CheckComponentsMappedORNOt))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                if (groupid != "0")
                {
                    dataManager.Parameters.Add(dtComponent.SALARYGROUPIDColumn, groupid);
                }
                dataManager.Parameters.Add(dtComponent.PAYROLLIDColumn, payrollid);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int CheckStaffGroupMapped(string groupid, long payrollid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.CheckStaffGroupMapped))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                if (groupid != "0")
                {
                    dataManager.Parameters.Add(dtComponent.SALARYGROUPIDColumn, groupid);
                }
                dataManager.Parameters.Add(dtComponent.PAYROLLIDColumn, payrollid);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        public DataTable CheckEditComponent(long ComId)
        {
            //  string strQuery = " SELECT COMPONENT FROM PRCOMPONENT WHERE COMPONENTID NOT IN (" + ComId + ") ORDER BY COMPONENT";
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.CheckEditComponent, "PRCOMPONENT"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtComponent.COMPONENTIDColumn, ComId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            if (resultArgs.Success)
                return resultArgs.DataSource.Table;
            else
                return null;
        }

        public DataTable FetchComponentDetailsById(int ComponentId)
        {
            DataTable dtcomp = new DataTable();
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchComponentByComponentId, "PRCOMPONENT"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(dtComponent.COMPONENTIDColumn, ComponentId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            dtcomp = resultArgs.DataSource.Table;
            return dtcomp;
        }

        public ResultArgs DeleteComponentDetails(int ExistingComponentId)
        {
            resultArgs = FetchComponentInEquationAndMapped(ExistingComponentId);
            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count==0)
            {
                using (DataManager datamanager = new DataManager(SQLCommand.Payroll.DeleteComponentById, "PRCOMPONENT"))
                {
                    datamanager.BeginTransaction();

                    DeletePayrollRangeFormulas(ExistingComponentId, datamanager);
                    if (resultArgs.Success)
                    {
                        datamanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        datamanager.Parameters.Add(dtComponent.COMPONENTIDColumn, ExistingComponentId);
                        resultArgs = datamanager.UpdateData();
                    }
                    datamanager.EndTransaction();
                }
            }
            else if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                resultArgs.Message = "Component might have used in any other Component's Equation or mapped to Group";
            }

            return resultArgs;
        }

        private ResultArgs DeletePayrollRangeFormulas(int cid, DataManager dm)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.Payroll.DeleteRangeByComponentId, "PRCOMPONENT"))
            {
                datamanager.Database = dm.Database;
                datamanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                datamanager.Parameters.Add(dtComponent.COMPONENTIDColumn, cid);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchComponentInEquationAndMapped(int cid)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.Payroll.FetchComponentInEquationAndMapped, "ComponentInEquationAndMapped"))
            {
                datamanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                datamanager.Parameters.Add(dtComponentMonth.COMPONENTIDColumn, cid);
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        public ResultArgs FetchComponentValueToProcessLedger(string staffids)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.Payroll.FetchComponentValuetoProcess, "PRCOMPONENTVALUE"))
            {
                datamanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                datamanager.Parameters.Add(dtComponent.STAFFIDColumn, staffids);
                datamanager.Parameters.Add(dtComponent.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.DataView);
            }
            return resultArgs;
        }
        public ResultArgs FetchLoanComponentValueToProcess(string staffids)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.Payroll.FetchLoanComponentValue, "PRCOMPONENTVALUE"))
            {
                datamanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                datamanager.Parameters.Add(dtComponent.STAFFIDColumn, staffids);
                datamanager.Parameters.Add(dtComponent.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.DataView);
            }
            return resultArgs;
        }
        public ResultArgs FetchComponentMappedLedgers()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.Payroll.IsComponentLedgerExists, "PRCOMPONENT"))
            {
                datamanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = datamanager.FetchData(DataSource.DataView);
            }
            return resultArgs;
        }

        public int IsCompLedgerMappedwithProject(string LedgerId, string ProjectId)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.Payroll.IsLedgerMappedWithProject, "PRCOMPONENTLEDGER"))
            {
                datamanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                datamanager.Parameters.Add(dtLedger.LEDGER_ID_COLLECTIONColumn, LedgerId);
                datamanager.Parameters.Add(dtLedger.PROJECT_IDColumn, ProjectId);
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        public int IsLoanCompLedgerMappedwithProject(string ProjectId)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.Payroll.IsLoanledgerMappedwithProject, "PRCOMPONENTLEDGER"))
            {
                datamanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                datamanager.Parameters.Add(dtLedger.PROJECT_IDColumn, ProjectId);
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        public ResultArgs GetPayrollPeriodMonth(long nPid = 0)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollDateInterval, "payroll"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtComponent.PAYROLLIDColumn, (nPid>0 ? nPid : clsGeneral.PAYROLL_ID));
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        public bool SaveComponent(long nCompId, string sCompStr, string sComp, string sDescr, int LedgerID,
            int ProcesstypeId, int iseditable, int payable, int ProcessComponentType, int DontImportModifiedValuefromPrevPR)
        {
            int i;
            int nDefSize;
            string[] aComp;
            string[] aVal;
            string sSql = "";
            string[] sgetField = new string[50];
            string[] sgetValue = new string[50];

            try
            {
                if (sCompStr == "") return false;
                if (nCompId > 0)          //Find Duplicate Component Exists or not (Edit Mode)
                {
                    if (objPrLoan.CheckDuplicate("PRComponent", "Component = '" + sComp + "'" +
                        ((sDescr.Trim() == "") ? "" : " AND Description ='" + sDescr + "'") +
                        " AND ComponentId <> " + nCompId))
                    {
                        return false;
                    }
                }
                else                       //Add Mode
                {
                    if (objPrLoan.CheckDuplicate("PRComponent", "Component = '" + sComp + "'"))
                    {
                        return false;
                    }
                }
                DataView dvComponent = new DataView(rsComponent);
                if (nCompId > 0)
                {
                    dvComponent.RowFilter = "componentid = " + nCompId;

                    if (dvComponent.Table.Rows.Count == 0)
                    {
                        MessageBox.Show("This Component has been deleted !", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
                else
                {
                    if (dvComponent != null && dvComponent.Table != null)
                        dvComponent.Table.NewRow();
                }
                aComp = sCompStr.Split('@');

                for (i = 0; i < aComp.GetUpperBound(0); i++) //Check the Equation Id Length
                {
                    aVal = aComp[i].Split('|');
                    if (aVal[0].ToUpper() == "EQUATIONID" && aVal[1] != "")
                    {
                        nDefSize = 500;
                        if (aVal[1].Length > nDefSize)
                        {
                            MessageBox.Show("The Equation size is not fit for the allocated size.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                        break;
                    }
                }
                for (i = 0; i < aComp.Length; i++)
                {
                    aVal = aComp[i].Split('|');

                    if (aVal[0].ToUpper() == "EQUATION" && aVal[1] != "")
                    {
                        nDefSize = 500;
                        if (aVal[1].Length > nDefSize)
                        {
                            aVal[1] = (aVal[1].Substring(0, nDefSize)).Trim();
                        }
                    }
                    if (aVal.GetUpperBound(0) == 1)
                    {
                        sgetValue[i] = (aVal[1] == "" ? string.Empty : aVal[1]);
                        sgetField[i] = aVal[0];
                    }
                }
                //sSql = "INSERT INTO PRCOMPONENT(COMPONENTID,COMPONENT,DESCRIPTION,TYPE,DEFVALUE," +
                //    "LINKVALUE,EQUATION,EQUATIONID,MAXSLAP,COMPROUND,IFCONDITION,SHOWINBROWSE,RELATEDCOMPONENTS)" + //,RELATEDCOMPONENTS
                //    "VALUES(SCQ_PRCOMPONENT.NEXTVAL,'<Component>','<Description>','<Type>','<DefValue>','<LinkValue>'," +
                //    "'<Equation>','<EquationId>',<ComMaxSlab>,<CompRound>,'<IFCondition>',<ShowInBrowse>,'<RelatedComponents>')"; //,'<RelatedComponents>'


                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.InsertPrComponent))
                {

                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtComponent.COMPONENTIDColumn, true);
                    if (sgetValue[1] != null && sgetValue[1].ToString() == "#")
                    {
                        //sSql = sSql.Replace("<Description>", "");

                        dataManager.Parameters.Add(dtComponent.DESCRIPTIONColumn, string.Empty);
                    }
                    else
                    {
                        dataManager.Parameters.Add(dtComponent.DESCRIPTIONColumn, (sgetValue[1] == null) ? "" : sgetValue[1].ToString());
                    }
                    dataManager.Parameters.Add(dtComponent.COMPONENTColumn, sgetValue[0].ToString());
                    dataManager.Parameters.Add(dtComponent.TYPEColumn, sgetValue[2].ToString());
                    dataManager.Parameters.Add(dtComponent.LEDGER_IDColumn, SettingProperty.PayrollFinanceEnabled == true ? LedgerID : 0);
                    // dataManager.Parameters.Add(dtComponent.PROCESS_LEDGER_IDColumn, ProcessLedgerId);
                    dataManager.Parameters.Add(dtComponent.PROCESS_TYPE_IDColumn, SettingProperty.PayrollFinanceEnabled == true ? ProcesstypeId : 0);
                    dataManager.Parameters.Add(dtComponent.DEFVALUEColumn, sgetValue[3].ToString());
                    dataManager.Parameters.Add(dtComponent.ISEDITABLEColumn, iseditable);

                    //sSql = sSql.Replace("<Component>", sgetValue[0].ToString());
                    //sSql = sSql.Replace("<Description>", sgetValue[1].ToString());
                    //sSql = sSql.Replace("<Type>", sgetValue[2].ToString());
                    //sSql = sSql.Replace("<DefValue>", sgetValue[3].ToString());
                    if (sgetValue[4] != null && sgetValue[4].ToString() == "#")
                    {
                        //sSql = sSql.Replace("<LinkValue>", "");
                        dataManager.Parameters.Add(dtComponent.LINKVALUEColumn, string.Empty);
                    }
                    else
                    {
                        //sSql = sSql.Replace("<LinkValue>", sgetValue[4].ToString());
                        dataManager.Parameters.Add(dtComponent.LINKVALUEColumn, (sgetValue[4] == null) ? string.Empty : sgetValue[4].ToString());
                    }
                    if (sgetValue[5] != null && sgetValue[5].ToString() == "#")
                    {
                        //sSql = sSql.Replace("<Equation>", "");
                        dataManager.Parameters.Add(dtComponent.EQUATIONColumn, string.Empty);
                    }
                    else
                    {
                        //sSql = sSql.Replace("<Equation>", sgetValue[5].ToString());
                        dataManager.Parameters.Add(dtComponent.EQUATIONColumn, (sgetValue[5] == null) ? string.Empty : sgetValue[5].ToString());
                    }
                    if (sgetValue[6] != null && sgetValue[6].ToString() == "#")
                    {
                        //sSql = sSql.Replace("<EquationId>", "");
                        dataManager.Parameters.Add(dtComponent.EQUATIONIDColumn, string.Empty);
                    }
                    else
                    {
                        //sSql = sSql.Replace("<EquationId>", sgetValue[6].ToString());
                        dataManager.Parameters.Add(dtComponent.EQUATIONIDColumn, (sgetValue[6] == null || sgetValue[6] == "0") ? string.Empty : sgetValue[6].ToString());
                    }
                    dataManager.Parameters.Add(dtComponent.MAXSLAPColumn, sgetValue[7].ToString());
                    //dataManager.Parameters.Add(dtComponent.COMPROUNDColumn, sgetValue[9].ToString());
                    dataManager.Parameters.Add(dtComponent.COMPROUNDColumn, sgetValue[8].ToString());
                    if (sgetValue[9] == null || sgetValue[9].ToString() == "#" || sgetValue[9].ToString() == "0")
                    {
                        dataManager.Parameters.Add(dtComponent.IFCONDITIONColumn, "0");
                    }
                    else
                    {
                        dataManager.Parameters.Add(dtComponent.IFCONDITIONColumn, sgetValue[9].ToString());
                    }

                    if (sgetValue[10] == null || sgetValue[10].ToString() == "#")
                    {
                        //sSql = sSql.Replace("<EquationId>", "");
                        dataManager.Parameters.Add(dtComponent.DONT_SHOWINBROWSEColumn, commem.NumberSet.ToInteger("0"));
                    }
                    else
                    {
                        //sSql = sSql.Replace("<EquationId>", sgetValue[6].ToString());
                        dataManager.Parameters.Add(dtComponent.DONT_SHOWINBROWSEColumn, commem.NumberSet.ToInteger(sgetValue[10].ToString()));
                    }

                    //dataManager.Parameters.Add(dtComponent.SHOWINBROWSEColumn, (sgetValue[10] == null || sgetValue[10].ToString() == "#" ? "": sgetValue[10].ToString()));
                    dataManager.Parameters.Add(dtComponent.RELATEDCOMPONENTSColumn, (sgetValue[11] == null || sgetValue[11].ToString() == "#" ? "" : sgetValue[11].ToString()));
                    dataManager.Parameters.Add(dtComponent.PAYABLEColumn, (sgetValue[14] == null || sgetValue[14].ToString() == "#" ? "" : sgetValue[14].ToString()));
                    dataManager.Parameters.Add(dtComponent.PROCESS_COMPONENT_TYPEColumn, ProcessComponentType);
                    dataManager.Parameters.Add(dtComponent.DONT_IMPORT_MODIFIED_VALUE_PREV_PRColumn, DontImportModifiedValuefromPrevPR );

                    //sSql = sSql.Replace("<ComMaxSlab>", sgetValue[7].ToString());
                    //sSql = sSql.Replace("<CompRound>", sgetValue[8].ToString());
                    //sSql = sSql.Replace("<IFCondition>", sgetValue[9].ToString());
                    //sSql = sSql.Replace("<ShowInBrowse>", sgetValue[10].ToString());
                    ////sSql=sSql.Replace("<Caption>",sgetValue[11].ToString());
                    //sSql = sSql.Replace("<RelatedComponents>", (sgetValue[11] == null || sgetValue[11].ToString() == "#" ? "" : sgetValue[11].ToString()));
                    resultArgs = dataManager.UpdateData();
                    ComponentId = commem.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                }
                //dh.insertRecord(sSql);
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.PayrollComponentFetchAll, "Component"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
                //dh.createDataSet("SELECT * FROM prcomponent ORDER BY component", "Component");
                // rsComponent = dh.getDataSet();
                if (resultArgs.Success)
                    rsComponent = resultArgs.DataSource.Table;

                //On 29/07/2022, To reset payroll process temp value for noneditable compontends -
                if (iseditable == 1)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Payroll.DeletePRStaffTempByNotEditableValue, "Component"))
                    {
                        dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        dataManager.Parameters.Add(dtComponent.PAYROLLIDColumn, (clsGeneral.PAYROLL_ID));
                        resultArgs = dataManager.UpdateData();
                    }
                }
                //----------------------------------------------------------------------------------
                return true;
            }
            catch (Exception Ex)
            {
                // Ex.Message
                return false;
            }
        }

        public bool UpdateComponent(long nCompId, string sCompStr, string sComp, string sDescr, int LedgerID, int ProcesstypeId,
                    int iseditable, int payable, int ProcessComponentType, int DontImportModifiedValuefromPrevPR)
        {
            int i;
            int nDefSize;
            string[] aComp;
            string[] aVal;
            string sSql = "";
            string[] sgetField = new string[50];
            string[] sgetValue = new string[50];

            if (sCompStr == "") return false;
            try
            {
                if (nCompId > 0) //Find Duplicate Component Exists or not (Edit Mode)
                {
                    if (objPrLoan.CheckDuplicate("PRComponent", "Component = '" + sComp + "'" +
                                        " AND ComponentId <> " + nCompId))
                    {
                        return false;
                    }
                }
                else             //Add Mode
                {
                    if (objPrLoan.CheckDuplicate("PRComponent", "Component = '" + sComp + "'" +
                        ((sDescr.Trim() == "") ? "" : " AND Description ='" + sDescr + "'")))
                    {
                        return false;
                    }
                }
                DataView dvComponent = new DataView(rsComponent);
                if (nCompId > 0)
                {
                    dvComponent.RowFilter = "componentid = " + nCompId;

                    if (dvComponent.Table.Rows.Count == 0)
                    {
                        return false;
                    }
                }
                else
                {
                    dvComponent.Table.NewRow();
                }
                aComp = sCompStr.Split('@');

                for (i = 0; i < aComp.GetUpperBound(0); i++) //Check the Equation Id Length
                {
                    aVal = aComp[i].Split('|');
                    if (aVal[0].ToUpper() == "EQUATIONID" && aVal[1] != "")
                    {
                        nDefSize = 500;
                        if (aVal[1].Length > nDefSize)
                        {
                            MessageBox.Show("The Equation size is not fit for the allocated size.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                        break;
                    }
                }
                for (i = 0; i < aComp.Length; i++)
                {
                    aVal = aComp[i].Split('|');

                    if (aVal[0].ToUpper() == "EQUATION" && aVal[1] != "")
                    {
                        nDefSize = 500;
                        if (aVal[1].Length > nDefSize)
                        {
                            aVal[1] = (aVal[1].Substring(0, nDefSize)).Trim();
                        }
                    }
                    if (aVal.GetUpperBound(0) == 1)
                    {
                        sgetValue[i] = (aVal[1] == "" ? null : aVal[1]);
                        sgetField[i] = aVal[0];
                        //sSql = "update prcomponent set "+ sgetField +" = "+ sgetValue +" where componentid ="+nCompId;
                    }
                }
                //sSql = "UPDATE PRCOMPONENT P SET P.COMPONENT='<Component>'," +
                //    "P.DESCRIPTION='<Description>',P.TYPE='<Type>',P.DEFVALUE='<DefValue>'," +
                //    "P.LINKVALUE='<LinkValue>',P.EQUATION='<Equation>',P.EQUATIONID='<EquationId>'," +
                //    "P.MAXSLAP=<ComMaxSlab>,P.COMPROUND=<CompRound>,P.IFCONDITION='<IFCondition>'," +
                //    "P.SHOWINBROWSE='<ShowinBrowse>',P.RELATEDCOMPONENTS='<RelatedComponents>' " +
                //    "WHERE P.COMPONENTID=<ComponentId> ";

                /*    sSql = sSql.Replace("<Component>", sgetValue[0].ToString());
                
                    sSql = sSql.Replace("<Type>", sgetValue[2].ToString());
                    sSql = sSql.Replace("<DefValue>", sgetValue[3].ToString());

                    sSql = sSql.Replace("<ComMaxSlab>", sgetValue[7].ToString());
                    sSql = sSql.Replace("<CompRound>", sgetValue[8].ToString());
                    sSql = sSql.Replace("<IFCondition>", sgetValue[9].ToString());
                    sSql = sSql.Replace("<ShowinBrowse>", sgetValue[10].ToString());
                    sSql = sSql.Replace("<RelatedComponents>", (sgetValue[11] == null || sgetValue[11].ToString() == "#" ? "" : sgetValue[11].ToString()));

                    sSql = sSql.Replace("<ComponentId>", nCompId.ToString());

                    if (sgetValue[1].ToString() == "#")
                    {
                        sSql = sSql.Replace("<Description>", "");
                    }
                    else
                    {
                        sSql = sSql.Replace("<Description>", sgetValue[1].ToString());
                    }
                    if (sgetValue[4].ToString() == "#")
                    {
                        sSql = sSql.Replace("<LinkValue>", "");
                    }
                    else
                    {
                        sSql = sSql.Replace("<LinkValue>", sgetValue[4].ToString());
                    }
                    if (sgetValue[5].ToString() == "#")
                    {
                        sSql = sSql.Replace("<Equation>", "");
                    }
                    else
                    {
                        sSql = sSql.Replace("<Equation>", sgetValue[5].ToString());
                    }
                    if (sgetValue[6].ToString() == "#")
                    {
                        sSql = sSql.Replace("<EquationId>", "");
                    }
                    else
                    {
                        sSql = sSql.Replace("<EquationId>", sgetValue[6].ToString());
                    }*/
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.UpdatePrComponent))
                {

                    // dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    dataManager.Parameters.Add(dtComponent.COMPONENTColumn, sgetValue[0].ToString());

                    dataManager.Parameters.Add(dtComponent.ISEDITABLEColumn, iseditable);
                    dataManager.Parameters.Add(dtComponent.PAYABLEColumn, payable);

                    dataManager.Parameters.Add(dtComponent.TYPEColumn, sgetValue[2].ToString());
                    dataManager.Parameters.Add(dtComponent.DEFVALUEColumn, sgetValue[3].ToString());
                    dataManager.Parameters.Add(dtComponent.LEDGER_IDColumn, SettingProperty.PayrollFinanceEnabled == true ? LedgerID : 0);
                    // dataManager.Parameters.Add(dtComponent.PROCESS_LEDGER_IDColumn, ProcessLedgerId);
                    dataManager.Parameters.Add(dtComponent.PROCESS_TYPE_IDColumn, SettingProperty.PayrollFinanceEnabled == true ? ProcesstypeId : 0);
                    dataManager.Parameters.Add(dtComponent.MAXSLAPColumn, sgetValue[7].ToString());
                    //dataManager.Parameters.Add(dtComponent.COMPROUNDColumn, sgetValue[9].ToString());
                    dataManager.Parameters.Add(dtComponent.COMPROUNDColumn, sgetValue[8].ToString());
                    if (sgetValue[9] == null || sgetValue[9].ToString() == "#" || sgetValue[9].ToString() == "0")
                    {
                        dataManager.Parameters.Add(dtComponent.IFCONDITIONColumn, "0");
                    }
                    else
                    {
                        dataManager.Parameters.Add(dtComponent.IFCONDITIONColumn, sgetValue[9].ToString());
                    }

                    if (sgetValue[10] == null || sgetValue[10].ToString() == "#")
                    {
                        //sSql = sSql.Replace("<EquationId>", "");
                        dataManager.Parameters.Add(dtComponent.DONT_SHOWINBROWSEColumn, commem.NumberSet.ToInteger("0"));
                    }
                    else
                    {
                        //sSql = sSql.Replace("<EquationId>", sgetValue[6].ToString());
                        dataManager.Parameters.Add(dtComponent.DONT_SHOWINBROWSEColumn, commem.NumberSet.ToInteger(sgetValue[10].ToString()));
                    }
                    //dataManager.Parameters.Add(dtComponent.SHOWINBROWSEColumn, sgetValue[10].ToString());
                    dataManager.Parameters.Add(dtComponent.RELATEDCOMPONENTSColumn, (sgetValue[11] == null || sgetValue[11].ToString() == "#" ? string.Empty : sgetValue[11].ToString()));

                    dataManager.Parameters.Add(dtComponent.COMPONENTIDColumn, nCompId.ToString());
                    // dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    if (sgetValue[1] != null && sgetValue[1].ToString() == "#")
                    {
                        dataManager.Parameters.Add(dtComponent.DESCRIPTIONColumn, string.Empty);
                    }
                    else
                    {
                        dataManager.Parameters.Add(dtComponent.DESCRIPTIONColumn, (sgetValue[1] == null) ? string.Empty : sgetValue[1].ToString());
                    }
                    if (sgetValue[4] != null && sgetValue[4].ToString() == "#")
                    {
                        dataManager.Parameters.Add(dtComponent.LINKVALUEColumn, string.Empty);
                    }
                    else
                    {
                        dataManager.Parameters.Add(dtComponent.LINKVALUEColumn, (sgetValue[4] == null) ? string.Empty : sgetValue[4].ToString());
                    }
                    if (sgetValue[5] != null && sgetValue[5].ToString() == "#")
                    {
                        dataManager.Parameters.Add(dtComponent.EQUATIONColumn, string.Empty);
                    }
                    else
                    {
                        dataManager.Parameters.Add(dtComponent.EQUATIONColumn, (sgetValue[5] == null) ? string.Empty : sgetValue[5].ToString());
                    }
                    if (sgetValue[6] != null && sgetValue[6].ToString() == "#")
                    {
                        dataManager.Parameters.Add(dtComponent.EQUATIONIDColumn, string.Empty);
                    }
                    else
                    {
                        dataManager.Parameters.Add(dtComponent.EQUATIONIDColumn, (sgetValue[6] == null) ? string.Empty : sgetValue[6].ToString());
                    }
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtComponent.PROCESS_COMPONENT_TYPEColumn, ProcessComponentType);
                    dataManager.Parameters.Add(dtComponent.DONT_IMPORT_MODIFIED_VALUE_PREV_PRColumn, DontImportModifiedValuefromPrevPR);

                    resultArgs = dataManager.UpdateData();
                    ComponentId = nCompId;
                    if (resultArgs.Success)
                    {
                        using (DataManager prDataManager = new DataManager(SQLCommand.Payroll.UpdatePrCompMonth))
                        {

                            // dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                            prDataManager.Parameters.Add(dtComponent.COMPONENTColumn, sgetValue[0].ToString());

                            prDataManager.Parameters.Add(dtComponent.TYPEColumn, sgetValue[2].ToString());
                            prDataManager.Parameters.Add(dtComponent.DEFVALUEColumn, sgetValue[3].ToString());

                            prDataManager.Parameters.Add(dtComponent.MAXSLAPColumn, sgetValue[7].ToString());
                            prDataManager.Parameters.Add(dtComponent.COMPROUNDColumn, sgetValue[9].ToString());
                            if (sgetValue[9] == null || sgetValue[9].ToString() == "#" || sgetValue[9].ToString() == "0")
                            {
                                prDataManager.Parameters.Add(dtComponent.IFCONDITIONColumn, "0");
                            }
                            else
                            {
                                prDataManager.Parameters.Add(dtComponent.IFCONDITIONColumn, sgetValue[9].ToString());
                            }

                            if (sgetValue[10] == null || sgetValue[10].ToString() == "#")
                            {
                                //sSql = sSql.Replace("<EquationId>", "");
                                prDataManager.Parameters.Add(dtComponent.DONT_SHOWINBROWSEColumn, commem.NumberSet.ToInteger("0"));
                            }
                            else
                            {
                                //sSql = sSql.Replace("<EquationId>", sgetValue[6].ToString());
                                prDataManager.Parameters.Add(dtComponent.DONT_SHOWINBROWSEColumn, commem.NumberSet.ToInteger(sgetValue[10].ToString()));
                            }
                            // prDataManager.Parameters.Add(dtComponent.SHOWINBROWSEColumn, sgetValue[10].ToString());
                            prDataManager.Parameters.Add(dtComponent.RELATEDCOMPONENTSColumn, (sgetValue[11] == null || sgetValue[11].ToString() == "#" ? string.Empty : sgetValue[11].ToString()));

                            prDataManager.Parameters.Add(dtComponent.COMPONENTIDColumn, nCompId.ToString());

                            if (sgetValue[1] != null && sgetValue[1].ToString() == "#")
                            {
                                prDataManager.Parameters.Add(dtComponent.DESCRIPTIONColumn, string.Empty);
                            }
                            else
                            {
                                prDataManager.Parameters.Add(dtComponent.DESCRIPTIONColumn, (sgetValue[1] == null) ? string.Empty : sgetValue[1].ToString());
                            }
                            if (sgetValue[4] != null && sgetValue[4].ToString() == "#")
                            {
                                prDataManager.Parameters.Add(dtComponent.LINKVALUEColumn, string.Empty);
                            }
                            else
                            {
                                prDataManager.Parameters.Add(dtComponent.LINKVALUEColumn, (sgetValue[4] == null) ? string.Empty : sgetValue[4].ToString());
                            }
                            if (sgetValue[5] != null && sgetValue[5].ToString() == "#")
                            {
                                prDataManager.Parameters.Add(dtComponent.EQUATIONColumn, string.Empty);
                            }
                            else
                            {
                                prDataManager.Parameters.Add(dtComponent.EQUATIONColumn, (sgetValue[5] == null) ? string.Empty : sgetValue[5].ToString());
                            }
                            if (sgetValue[6] != null && sgetValue[6].ToString() == "#")
                            {
                                prDataManager.Parameters.Add(dtComponent.EQUATIONIDColumn, string.Empty);
                            }
                            else
                            {
                                prDataManager.Parameters.Add(dtComponent.EQUATIONIDColumn, (sgetValue[6] == null) ? string.Empty : sgetValue[6].ToString());
                            }
                                                        
                            prDataManager.Parameters.Add(dtComponent.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                            prDataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                            resultArgs = prDataManager.UpdateData();

                        }
                    }
                }
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.PayrollComponentFetchAll, "Component"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
                if (resultArgs.Success)
                    rsComponent = resultArgs.DataSource.Table;

                //On 29/07/2022, To reset payroll process temp value for noneditable compontends -
                if (iseditable == 1)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Payroll.DeletePRStaffTempByNotEditableValue, "Component"))
                    {
                        dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        dataManager.Parameters.Add(dtComponent.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        resultArgs = dataManager.UpdateData();
                    }
                }
                //----------------------------------------------------------------------------------

                //    dh.updateRecord(sSql);
                //dh.createDataSet("SELECT * FROM prcomponent ORDER BY component", "Component");
                // rsComponent = dh.getDataSet();
                return resultArgs.Success;
            }

            catch
            {
                return resultArgs.Success;
            }
        }

        #region Payroll range formula methods
        public int SaveRangeFormula(DataTable dtRange)
        {
            if (dtRange != null && dtRange.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRange.Rows)
                {
                    if (dr.RowState != DataRowState.Deleted)
                    {
                        using (DataManager dManager = new DataManager(SQLCommand.Payroll.InsertRangeCondtions))
                        {
                            LinkComponentId = commem.NumberSet.ToInteger(dr["LINK_COMPONENT_ID"].ToString());
                            MinValue = dr["MIN_VALUE"].ToString();
                            MaxValue = dr["MAX_VALUE"].ToString();
                            Maxslab = dr["MAX_SLAB"].ToString();
                            dManager.Parameters.Add(dtPayrollRange.COMPONENTIDColumn, ComponentId);
                            dManager.Parameters.Add(dtPayrollRange.LINK_COMPONENT_IDColumn, LinkComponentId);
                            dManager.Parameters.Add(dtPayrollRange.MIN_VALUEColumn, MinValue);
                            dManager.Parameters.Add(dtPayrollRange.MAX_VALUEColumn, MaxValue);
                            dManager.Parameters.Add(dtPayrollRange.MAX_SLABColumn, Maxslab);
                            dManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                            dManager.DataCommandArgs.IsDirectReplaceParameter = true;
                            resultArgs = dManager.UpdateData();
                        }
                    }

                }
            }
            return resultArgs.RowsAffected;
        }
        public int UpdateRangeFormula(DataTable dtRange)
        {
            return 0;
        }
        #endregion


        public ResultArgs DeleteStaffTempDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.DeletePRStaffTemo))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(dtComponent.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        private int CheckComponentMappedtoGroup(long ComponentId)
        {
            int rtn = 0;
            using (DataManager dtManager = new DataManager(SQLCommand.Payroll.CheckComponentMappedToGroup))
            {
                dtManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dtManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dtManager.Parameters.Add(dtComponent.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                dtManager.Parameters.Add(dtComponent.SALARYGROUPIDColumn, SelectedGroupId);
                dtManager.Parameters.Add(dtComponent.COMPONENTIDColumn, ComponentId);
                resultArgs = dtManager.FetchData(DataSource.Scalar);
            }

            if (resultArgs.Success)
            {
                rtn = resultArgs.DataSource.Sclar.ToInteger;

                //On 10/07/2019 If link fields and not even mapped, we can consider those link fields can be used in formula process ------------------
                //for gradepay, special pay, performance, overtime
                if (rtn == 0)
                {
                    if (this.MoveComponent(ComponentId))
                    {
                        if (!string.IsNullOrEmpty(this.dvComponent[0]["LinkValue"].ToString().ToString()))
                        {
                            rtn = 1;
                        }
                    }
                }
            }
            //-------------------------------------------------------------------------------------------------------------------------------------

            return rtn;
        }

        /// <summary>
        /// To get Current Payroll Total Processing days
        /// </summary>
        /// <returns></returns>
        public double GetTotalPayrollProcessingDays()
        {
            double PayrollProcessingDays = 0;
            try
            {
                PayrollProcessingDays = 30;
                //ResultArgs result   = GetPayrollPeriodMonth();
                //if (result.Success && result.DataSource.Table != null)
                //{
                //    DataTable dtPayrollInfo = result.DataSource.Table;
                //    DateTime dtPayrollFrom = UtilityMember.DateSet.ToDate(dtPayrollInfo.Rows[0]["FROMDATE"].ToString(), false);
                //    DateTime dtPayrollTo = UtilityMember.DateSet.ToDate(dtPayrollInfo.Rows[0]["TODATE"].ToString(), false);
                //    DaysInMonth = (dtPayrollTo.Date - dtPayrollFrom.Date).TotalDays + 1;
                //}
                //else
                //{
                //    MessageRender.ShowMessage("Could not get Total Days in Payroll " + result.Message);
                //}

            }
            catch (Exception err)
            {
                 MessageRender.ShowMessage("Could not get Total Processing Pay days in Payroll " + err.Message);
            }
            return PayrollProcessingDays; 
        }

        public double GetTotalDaysInPayMonth(long nPid=0)
        {
            double DaysInPayMonth = 30;
            try
            {
                ResultArgs result = GetPayrollPeriodMonth(nPid);
                if (result.Success && result.DataSource.Table != null)
                {
                    DataTable dtPayrollInfo = result.DataSource.Table;
                    DateTime dtPayrollFrom = UtilityMember.DateSet.ToDate(dtPayrollInfo.Rows[0]["FROMDATE"].ToString(), false);
                    DateTime dtPayrollTo = UtilityMember.DateSet.ToDate(dtPayrollInfo.Rows[0]["TODATE"].ToString(), false);
                    DaysInPayMonth = (dtPayrollTo.Date - dtPayrollFrom.Date).TotalDays + 1;
                }
                else
                {
                    MessageRender.ShowMessage("Could not get Total Days in Payroll " + result.Message);
                }

            }
            catch (Exception err)
            {
                MessageRender.ShowMessage("Could not get Total Days in Payroll " + err.Message);
            }
            return DaysInPayMonth; 
        }
        /// <summary>
        /// On 27/11/2020, To check component is IF condition or not
        /// </summary>
        /// <param name="nCompId"></param>
        /// <returns></returns>
        private bool isIFConditionComponent(long nCompId)
        {
            bool rtn = false;
            this.dvComponent.RowFilter = "";
            this.dvComponent = rsComponent.DefaultView;
            if (this.dvComponent.Count > 0)
            {
                this.dvComponent.RowFilter = "componentid = " + nCompId;

                if (this.dvComponent.Count > 0)
                {
                    string Equation = this.dvComponent[0]["EquationId"].ToString();
                    string[] aformulaGroup = Equation.Split('$');
                    if (!string.IsNullOrEmpty(Equation) && aformulaGroup.Length > 0)
                    {
                        string[] aformula = aformulaGroup[0].Split('~');
                        if (aformula.Length > 0)
                        {
                            Int32 nIFType = Convert.ToInt32(aformula[1]);
                            rtn = (nIFType > 0);
                        }
                    }
                }
            }
            return rtn;
        }


        /// <summary>
        /// On 07/03/2022, To affect lop for given amount
        /// </summary>
        /// <param name="amt"></param>
        /// <returns></returns>
        private double AffectLOPDays(double amt)
        {
            double rtnAffectedamt = amt;
            if (LOPDaysCompId > 0 && amt > 0 && TotalPayrollProcessingDays > 0)
            {
                double lopdays = UtilityMember.NumberSet.ToDouble(GetDefaultValue("", LOPDaysCompId));
                if (lopdays > 0)
                {
                    double amtperday = (amt / TotalPayrollProcessingDays);
                    double amtWithLOPDay = amt - (amtperday * lopdays);
                    rtnAffectedamt = amtWithLOPDay;
                }
            }
            return rtnAffectedamt;
        }


        /// <summary>
        /// On 14/03/2022, To check all used compontents are mapped or not
        /// </summary>
        /// <returns></returns>
        private bool CheckUsedComponentMapped()
        {
            bool rtn = true;
            
            try
            {
                if (dvCompM != null && dvCompM.Table != null && dvComponent.Table!=null)
                {
                    dvCompM.RowFilter = string.Empty;
                    //DataTable dtUsedComponents =  dvCompM.ToTable(true, new string[] { dtComponent.COMPONENTIDColumn.ColumnName });

                    foreach (DataRow dr in dvComponent.Table.Rows)
                    {
                        int compid = NumberSet.ToInteger(dr[dtComponent.COMPONENTIDColumn.ColumnName].ToString());
                        string compname = dr[dtComponent.COMPONENTColumn.ColumnName].ToString();
                        string usedcomponent = "<" + compid + ">";
                        dvCompM.RowFilter = dtComponent.EQUATIONIDColumn.ColumnName + " LIKE '%" + usedcomponent + "%'";
                        if (dvCompM.Count > 0)
                        {
                            dvCompM.RowFilter = string.Empty;
                            dvCompM.RowFilter = dtComponent.COMPONENTIDColumn.ColumnName + " = " + compid + "";
                            if (dvCompM.Count == 0)
                            {
                                rtn = false;
                                MessageRender.ShowMessage("'" + compname + "' is not yet mapped for the selected Group, Map and Process again.");
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
            finally
            {
                dvCompM.RowFilter = string.Empty;
            }

            return rtn;
        }

        /// <summary>
        /// On 6/05/2019
        /// This method is used to get No of paying salary days values for each staff
        /// </summary>
        /// <param name="compid"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        //private double GetPayingSalaryDays(long staffid)
        //{
        //    double NoOfPayingDays = 0;
        //    try
        //    {
        //        dvDefVal.RowFilter = string.Empty;
        //        if (staffid > 0 && PayingSalaryDaysCompId > 0)
        //        {
        //            if (dvDefVal != null && dvDefVal.Table != null && dvDefVal.Table.Rows.Count>0 )
        //            {
        //                dvDefVal.RowFilter = "STAFFID=" + staffid + " AND COMPONENTID=" + PayingSalaryDaysCompId ;
        //                if (dvDefVal.Count > 0)
        //                {
        //                    NoOfPayingDays = UtilityMember.NumberSet.ToDouble(dvDefVal[0]["COMPVALUE"].ToString());

        //                    if (NoOfPayingDays > TotayDaysInPayMonth)
        //                    {
        //                        NoOfPayingDays = TotayDaysInPayMonth;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        dvDefVal.RowFilter = string.Empty;
        //        MessageRender.ShowMessage("Could not get salary days " + err.Message);
        //    }
        //    dvDefVal.RowFilter = string.Empty;
        //    return NoOfPayingDays;
        //}


    }
}

