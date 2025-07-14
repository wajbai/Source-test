using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

using Bosco.Utility;
using Bosco.Model;
using Bosco.Model.UIModel;
using Bosco.Model.Transaction;
using Bosco.Utility.ConfigSetting;
using Proshot.UtilityLib.CommonDialogs;
using System.IO;
using Payroll.Model.UIModel;
using Bosco.Utility.Common;
using System.Windows.Forms;

namespace AcMEService
{
    public class AcMeERPService : IAcMeERPService
    {
        #region Variables
        private string AcMEServiceKey = "AcMe$786*@";
        private bool IsSuccess = false;
        DataSet dsMasterDetails = new DataSet("dsMasters");
        DataSet dsPayrollMasterDetails = new DataSet("dsPayrollMasters");
        ResultArgs resultArgs = null;
        bool Isposted = false;
        int PostVoucherId = 0;
        int PostStaffId = 0;
        CommonMember UtilityMember = new CommonMember();
        TransProperty Transaction = new TransProperty();
        public DataTable dtTransaction = new DataTable();
        public DataTable dtCashTransaction = new DataTable();
        private DataSet dsCostCentre = new DataSet();
        DateTime? defVal = null;
        private static object objlock = new object();
        public int rtnVoucherId = 0;
        public int PayrollStaffReturnValue = 0;
        // Static variables

        private static string SOURCE = "SOURCE";
        private static string VOUCHER_DATE = "VOUCHER_DATE";
        private static string VOUCHER_TYPE = "VOUCHER_TYPE";
        private static string REF_CODE = "REF_CODE";
        private static string REF_NO = "REF_NO";
        private static string PROJECT = "PROJECT_NAME";
        private static string GENERAL_LEDGER = "LEDGER";
        private static string GENERAL_TRANS_MODE = "LEDGER_TRANS_MODE";
        private static string CASHBANK_LEDGER = "CASHBANK_LEDGER";
        private static string CASHBANK_TRANS_MODE = "CASHBANK_TRANS_MODE";
        private static string LEDGER_AMOUNT = "LEDGER_AMOUNT";
        private static string CASHBANK_AMOUNT = "CASHBANK_AMOUNT";
        private static string CASHBANK_FLAG = "FLAG";
        private static string MATERIALISED_ON = "MATERIALIZED_ON";
        private static string BANK_REF_NO = "BANK_REF_NO";
        private static string NARRATION = "NARRATION";
        private static string CLIENT_CODE = "CLIENT_CODE";
        private static string CLIENT_REFERENCE_ID = "CLIENT_REFERENCE_ID";
        #endregion

        private static string EMPNO = "EMPNO";
        private static string FIRSTNAME = "FIRSTNAME";
        private static string MIDDLE_NAME = "MIDDLE_NAME";
        private static string FATHER_HUSBAND_NAME = "FATHER_HUSBAND_NAME";
        private static string MOTHER_NAME = "MOTHER_NAME";
        private static string NO_OF_CHILDREN = "NO_OF_CHILDREN";
        private static string BLOOD_GROUP = "BLOOD_GROUP";
        private static string LAST_DATE_OF_CONTRACT = "LAST_DATE_OF_CONTRACT";
        private static string LASTNAME = "LASTNAME";
        private static string GENDER = "GENDER";
        private static string CATEGORY = "CATEGORY";
        private static string DATEOFBIRTH = "DATEOFBIRTH";
        private static string KNOWNAS = "KNOWNAS";
        private static string GROUP = "GROUP";
        private static string DEGREE = "DEGREE";
        private static string DESIGNATION = "DESIGNATION";
        private static string DATEOFJOIN = "DATEOFJOIN";
        private static string STAFF_REF_ID = "STAFF_REF_ID";
        private static string SCALEOFPAY = "SCALEOFPAY";
        private static string PAY = "PAY";
        private static string MONTH_INCREAMENT = "MONTH_INCREAMENT";
        private static string ACCOUNT_NO = "ACCOUNT_NO";
        private static string UAN = "PFNUMBER";
        private static string LEAVINGDATE = "LEAVINGDATE";
        private static string LEAVINGREMARKS = "LEAVEREMARKS";
        private static string RETIREMENTDATE = "RETIREMENTDATE";
        private static string YOS = "YOS";
        private static string COMMEND_ON_PERFORMANCE = "COMMEND_ON_PERFORMANCE";
        private static string ADDRESS = "ADDRESS";
        private static string MOBILE_NO = "MOBILE_NO";
        private static string TELEPHONE_NO = "TELEPHONE_NO";
        private static string EMERGENCY_CONTACT_NO = "EMERGENCY_CONTACT_NO";
        private static string EMAIL_ID = "EMAIL_ID";
        private static string DEPENDENT1 = "DEPENDENT1";
        private static string DEPENDENT2 = "DEPENDENT2";
        private static string DEPENDENT3 = "DEPENDENT3";
        private static string WORK_EXPERIENCE = "WORK_EXPERIENCE";
        private static string PAN_NO = "PAN_NO";
        private static string AADHAR_NO = "AADHAR_NO";
        //private static string OVERTIME_PAY = "OVERTIME_PAY";
        //private static string EARNING1 = "EARNING1";
        //private static string SPECIAL_PAY = "SPECIAL_PAY";
        //private static string PERFORMANCE_PAY = "PERFORMANCE_PAY";
        //private static string DECREMENT_AMOUNT = "DECREMENT_AMOUNT";
        //private static string PAYING_SALARY_DAYS = "PAYING_SALARY_DAYS";

        private static string ColPayrollId = "PAYROLLID";
        private static string ColStaffId = "STAFFID";
        private static string ColComponentId = "COMPONENTID";
        private static string ColComponentValue = "COMPVALUE";


        #region Properties
        private int voucherId = 0;
        private int VoucherId
        {
            get { return voucherId; }
            set { voucherId = value; }
        }

        private int projectId = 0;
        private int ProjectId
        {
            set { projectId = value; }
            get { return projectId; }
        }
        private int ledgerid = 0;
        private int LedgerId
        {
            set { ledgerid = value; }
            get { return ledgerid; }
        }
        private int cashbankLedgerid = 0;
        private int CashBankLedgerId
        {
            set { cashbankLedgerid = value; }
            get { return cashbankLedgerid; }
        }
        private double ledgeramount = 0;
        private double LedgerAmount
        {
            set { ledgeramount = value; }
            get { return ledgeramount; }
        }
        private string chequeno = string.Empty;
        private string ChequeNo
        {
            set { chequeno = value; }
            get { return chequeno; }
        }
        private string ledgerflag = string.Empty;
        private string LedgerFlag
        {
            set { ledgerflag = value; }
            get { return ledgerflag; }
        }
        private DateTime materialisedon = DateTime.Now;
        private DateTime MaterialisedOn
        {
            set { materialisedon = value; }
            get { return materialisedon; }
        }
        private DateTime voucherdate = DateTime.Now;
        private DateTime VoucherDate
        {
            set { voucherdate = value; }
            get { return voucherdate; }
        }
        #endregion

        #region Methods
        public bool CheckCredentials(string ServiceKey)
        {
            AcMELog.WriteLog("Checking Credentials started");
            AcMELog.WriteLog("Client Sent Key :" + ServiceKey);
            if (!string.IsNullOrEmpty(ServiceKey) && AcMEServiceKey.Equals(ServiceKey.Trim()))
            {
                AcMELog.WriteLog("Credentials Success");
                IsSuccess = true;
            }
            AcMELog.WriteLog("Checking Credentials ended");
            return IsSuccess;
        }
        public DataSet GetMasters(string ServiceKey)
        {
            try
            {
                if (CheckCredentials(ServiceKey))
                {
                    AcMELog.WriteLog("GetMasters Started..");
                    resultArgs = new ResultArgs();
                    using (LedgerSystem ledgersystem = new LedgerSystem())
                    {
                        using (ProjectSystem projectsystem = new ProjectSystem())
                        {
                            using (LedgerGroupSystem ledgergroupsystem = new LedgerGroupSystem())
                            {
                                using (AccouingPeriodSystem accountingPeriod = new AccouingPeriodSystem())
                                {
                                    AcMELog.WriteLog("Fetch Accounting Period started..");
                                    resultArgs = accountingPeriod.FetchAccountingPeriodDetails();
                                    if (resultArgs.Success)
                                    {
                                        resultArgs.DataSource.Table.TableName = "AccountingPeriod";
                                        dsMasterDetails.Tables.Add(resultArgs.DataSource.Table);
                                        AcMELog.WriteLog("Fetch Accounting Period ended..");

                                        AcMELog.WriteLog("Fetch Project started..");
                                        resultArgs = projectsystem.FetchProjects();
                                        if (resultArgs.Success)
                                        {
                                            resultArgs.DataSource.Table.TableName = "Project";
                                            dsMasterDetails.Tables.Add(resultArgs.DataSource.Table);
                                            AcMELog.WriteLog("Fetch Project  ended..");

                                            AcMELog.WriteLog("Fetch Ledger  started..");
                                            resultArgs = ledgersystem.FetchLedgerDetails();
                                            if (resultArgs.Success)
                                            {
                                                DataTable dtLedger = new DataTable();
                                                dtLedger = resultArgs.DataSource.Table;
                                                dtLedger.TableName = "Ledger";
                                                DataView dvLedger = new DataView();
                                                dvLedger = dtLedger.DefaultView;
                                                dvLedger.RowFilter = "NATURE_ID IN (1,2) OR GROUP_ID IN (12,13) ";
                                                dsMasterDetails.Tables.Add(dvLedger.ToTable());
                                                AcMELog.WriteLog("Fetch Ledger ended..");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (resultArgs.Success)
                    {
                        AcMELog.WriteLog("GetMasters Ended..");
                    }
                    else
                    {
                        AcMELog.WriteLog("Error in GetMasters" + resultArgs.Message);
                    }
                }
                else
                {
                    AcMELog.WriteLog("Provided Credentials is incorrect,try again..");
                    throw new Exception("Provided Credentials is incorrect,try again..");
                }
            }
            catch (Exception eg)
            {
                AcMELog.WriteLog("Problem in post vouchers : " + eg.Message.ToString());
                throw eg;
            }
            return dsMasterDetails;
        }
        public int PostVouchers(string dtsVouchers, string ServiceKey)
        {
            DataTable dtVouchers = new DataTable("Vouchers");
            TextReader sr = new StringReader(dtsVouchers);
            dtVouchers.ReadXml(sr);
            resultArgs = new ResultArgs();
            try
            {
                AcMELog.WriteLog("Post Vouchers Started..");
                if (CheckCredentials(ServiceKey))
                {
                    if (dtVouchers != null && dtVouchers.Rows.Count > 0)
                    {
                        using (ProjectSystem projectsystem = new ProjectSystem())
                        {
                            AcMELog.WriteLog("Fetch Project Id by Project started..");
                            AcMELog.WriteLog("Project Name  :" + dtVouchers.Rows[0][PROJECT].ToString());
                            resultArgs = projectsystem.FetchProjectIdByProjectName(dtVouchers.Rows[0][PROJECT].ToString());
                            AcMELog.WriteLog("Fetch Project Id by Project ended..");
                            if (!resultArgs.Success || resultArgs.DataSource.Sclar.ToInteger <= 0)
                            {
                                throw new Exception("Project does not exists");
                            }
                            else
                            {
                                projectId = resultArgs.DataSource.Sclar.ToInteger;
                            }
                            AcMELog.WriteLog("Construct General Trans Started..");
                            ConstructTransSource(dtVouchers);
                            AcMELog.WriteLog("Construct General Trans ended..");

                            AcMELog.WriteLog("Construct Cash/Bank Trans Started..");
                            ConstructCashTransSource(dtVouchers);
                            AcMELog.WriteLog("Construct Cash/Bank Trans ended..");

                            AcMELog.WriteLog("validating Source started..");
                            if (IsValidSource(dtVouchers))
                            {
                                AcMELog.WriteLog("validating  Source ended..");
                                voucherdate = this.UtilityMember.DateSet.ToDate(dtVouchers.Rows[0][VOUCHER_DATE].ToString(), false);
                                using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                                {
                                    //Voucher Master Details
                                    voucherTransaction.VoucherId = 0;
                                    voucherTransaction.ProjectId = ProjectId;
                                    voucherTransaction.VoucherDate = voucherdate;

                                    //On 15/02/2019, for multi voucher type
                                    //voucherTransaction.VoucherType = dtVouchers.Rows[0][VOUCHER_TYPE].ToString();
                                    string vtype = dtVouchers.Rows[0][VOUCHER_TYPE].ToString().ToUpper();
                                    voucherTransaction.VoucherType = vtype;
                                    voucherTransaction.VoucherDefinitionId = (Int32)DefaultVoucherTypes.Receipt;
                                    if (vtype == "RC") voucherTransaction.VoucherDefinitionId = (Int32)DefaultVoucherTypes.Receipt;
                                    if (vtype == "PY") voucherTransaction.VoucherDefinitionId = (Int32)DefaultVoucherTypes.Payment;
                                    if (vtype == "CN") voucherTransaction.VoucherDefinitionId = (Int32)DefaultVoucherTypes.Contra;
                                    if (vtype == "JN") voucherTransaction.VoucherDefinitionId = (Int32)DefaultVoucherTypes.Journal;

                                    voucherTransaction.VoucherSubType = LedgerTypes.GN.ToString();
                                    voucherTransaction.TransVoucherMethod = (int)TransactionVoucherMethod.Automatic;
                                    voucherTransaction.VoucherNo = "";
                                    voucherTransaction.DonorId = 0;
                                    voucherTransaction.PurposeId = 0;
                                    voucherTransaction.ContributionType = "N";
                                    voucherTransaction.ContributionAmount = 0.00m;
                                    voucherTransaction.CurrencyCountryId = 0;
                                    voucherTransaction.ExchangeRate = 1;
                                    voucherTransaction.CalculatedAmount = 0;
                                    voucherTransaction.ActualAmount = 0;
                                    voucherTransaction.ExchageCountryId = 0;
                                    voucherTransaction.Narration = dtVouchers.Rows[0][NARRATION].ToString();
                                    voucherTransaction.Status = 1;
                                    voucherTransaction.FDGroupId = 0;
                                    voucherTransaction.CreatedBy = UtilityMember.NumberSet.ToInteger(voucherTransaction.LoginUserId);
                                    voucherTransaction.ModifiedBy = UtilityMember.NumberSet.ToInteger(voucherTransaction.LoginUserId);
                                    voucherTransaction.NameAddress = "";
                                    voucherTransaction.ClientReferenceId = UtilityMember.NumberSet.ToInteger(dtVouchers.Rows[0][CLIENT_REFERENCE_ID].ToString()).ToString();
                                    voucherTransaction.ClientCode = dtVouchers.Rows[0][CLIENT_CODE].ToString();

                                    //Voucher Trans Details
                                    DataView dvTrans = new DataView(dtTransaction);
                                    this.Transaction.TransInfo = dvTrans;

                                    DataView dvCashTrans = new DataView(dtCashTransaction);
                                    this.Transaction.CashTransInfo = dvCashTrans;

                                    this.Transaction.CostCenterInfo = dsCostCentre;
                                    // Fetch Voucher id to update voucher details
                                    resultArgs = voucherTransaction.FetchVoucherIdByClientRefCode();
                                    if (resultArgs.Success && resultArgs.DataSource.Sclar.ToInteger > 0)
                                    {
                                        voucherTransaction.VoucherId = rtnVoucherId = resultArgs.DataSource.Sclar.ToInteger;
                                    }

                                    resultArgs = voucherTransaction.SaveTransactions();
                                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                                    {
                                        lock (objlock)
                                        {
                                            frmPopup alert = new frmPopup(PopupSkins.InfoSkin);
                                            string Vdate = UtilityMember.DateSet.ToDate(dtVouchers.Rows[0][VOUCHER_DATE].ToString(), false).ToShortDateString();
                                            PostVoucherId = voucherTransaction.VoucherId > 0 ? voucherTransaction.VoucherId : 0;
                                            alert.ShowPopup(dtVouchers.Rows[0][CLIENT_CODE].ToString() + " Source :  ", "Voucher Posted for the project  " + dtVouchers.Rows[0][PROJECT].ToString() + " on " + Vdate + "\nVoucher Reference No :" + PostVoucherId + "", 500, 6000, 500);
                                        }
                                        AcMELog.WriteLog("Post Vouchers ended..");
                                    }
                                    else
                                    {
                                        PostVoucherId = -1;
                                        AcMELog.WriteLog("Failed in Posted Voucher:" + resultArgs.Message);
                                    }
                                }
                            }
                            //else
                            //{
                            //    AcMELog.WriteLog("Provided Credentials is incorrect,try again..");
                            //    throw new Exception("Provided Credentials is incorrect,try again..");
                            //}
                        }
                    }
                    else
                    {
                        AcMELog.WriteLog("Provided Data is invalid,try again..");
                        throw new Exception("Provided Data is invalid,try again..");
                    }
                }
            }
            catch (Exception eg)
            {
                AcMELog.WriteLog("Problem in post vouchers : " + eg.Message.ToString());
                throw eg;
            }
            return PostVoucherId;
        }
        private void ConstructTransSource(DataTable dtvoucher)
        {
            dtTransaction.Columns.Add("SOURCE", typeof(string));
            dtTransaction.Columns.Add("LEDGER_ID", typeof(Int32));
            dtTransaction.Columns.Add("AMOUNT", typeof(decimal));
            dtTransaction.Columns.Add("CHEQUE_NO", typeof(string));
            dtTransaction.Columns.Add("MATERIALIZED_ON", typeof(DateTime));
            dtTransaction.Columns.Add("LEDGER_BALANCE", typeof(string));
            dtTransaction.Columns.Add("BUDGET_AMOUNT", typeof(string));
            dtTransaction.Columns.Add("TEMP_AMOUNT", typeof(decimal));
            dtTransaction.Columns.Add("NARRATION", typeof(string));
            using (LedgerSystem ledgersystem = new LedgerSystem())
            {
                ledgersystem.ProjectId = projectId;
                resultArgs = ledgersystem.FetchLedgerIdByLedgerName(dtvoucher.Rows[0][GENERAL_LEDGER].ToString(), false);
                if (!resultArgs.Success || resultArgs.DataSource.Sclar.ToInteger <= 0)
                {
                    throw new Exception("General Ledger does not exists or not mapped with this project");
                }
                else
                {
                    ledgerid = resultArgs.DataSource.Sclar.ToInteger;
                }

                ledgeramount = this.UtilityMember.NumberSet.ToDouble(dtvoucher.Rows[0][LEDGER_AMOUNT].ToString());
                string SourceType = (dtvoucher.Rows[0][GENERAL_TRANS_MODE].ToString().Equals("CR") || (dtvoucher.Rows[0][GENERAL_TRANS_MODE].ToString().Equals("Cr"))) ? "1" : "2";
                dtTransaction.Rows.Add(SourceType, ledgerid, ledgeramount, "", DBNull.Value, "", "", 0.00, "");
            }
        }
        private void ConstructCashTransSource(DataTable dtvoucher)
        {
            dtCashTransaction.Columns.Add("SOURCE", typeof(string));
            dtCashTransaction.Columns.Add("LEDGER_FLAG", typeof(string));
            dtCashTransaction.Columns.Add("LEDGER_ID", typeof(Int32));
            dtCashTransaction.Columns.Add("AMOUNT", typeof(decimal));
            dtCashTransaction.Columns.Add("CHEQUE_NO", typeof(string));
            dtCashTransaction.Columns.Add("MATERIALIZED_ON", typeof(DateTime));
            dtCashTransaction.Columns.Add("LEDGER_BALANCE", typeof(string));
            dtCashTransaction.Columns.Add("BUDGET_AMOUNT", typeof(string));
            dtCashTransaction.Columns.Add("TEMP_AMOUNT", typeof(decimal));

            using (LedgerSystem ledgersystem = new LedgerSystem())
            {
                ledgersystem.ProjectId = projectId;
                resultArgs = ledgersystem.FetchLedgerIdByLedgerName(dtvoucher.Rows[0][CASHBANK_LEDGER].ToString(), true);

                if (!resultArgs.Success || resultArgs.DataSource.Sclar.ToInteger <= 0)
                {
                    throw new Exception("Cash/Bank Ledger does not exists or not mapped with this project");
                }
                else
                {
                    ledgerid = resultArgs.DataSource.Sclar.ToInteger;
                }
                ledgeramount = this.UtilityMember.NumberSet.ToDouble(dtvoucher.Rows[0][CASHBANK_AMOUNT].ToString());
                chequeno = dtvoucher.Rows[0][BANK_REF_NO].ToString();
                materialisedon = (this.UtilityMember.DateSet.ToDate(dtvoucher.Rows[0][MATERIALISED_ON].ToString(), false));
                //  ledgerflag = dtvoucher.Rows[0][CASHBANK_FLAG].ToString();
                string SourceType = (dtvoucher.Rows[0][CASHBANK_TRANS_MODE].ToString().Equals("CR") || (dtvoucher.Rows[0][CASHBANK_TRANS_MODE].ToString().Equals("Cr"))) ? "1" : "2";
                dtCashTransaction.Rows.Add(SourceType, ledgerflag, ledgerid, ledgeramount, chequeno,
                    materialisedon.Equals(DateTime.MinValue) ? defVal : materialisedon,
                    "", "", 0.00);
            }
        }
        private bool IsValidSource(DataTable dtchkvoucher)
        {
            bool Isvalid = true;

            if (!dtchkvoucher.Columns.Contains(VOUCHER_DATE))
            {
                Isvalid = false;
                throw new Exception("Voucher date does not exists.");
            }
            else if (!IsValidVoucherDate(dtchkvoucher))
            {
                Isvalid = false;
                throw new Exception("Provided voucher date does not fall between the transaction period");
            }
            else if (!dtchkvoucher.Columns.Contains(PROJECT))
            {
                Isvalid = false;
                throw new Exception("Project Name does not exists.");
            }
            else if (!dtchkvoucher.Columns.Contains(VOUCHER_TYPE))
            {
                Isvalid = false;
                throw new Exception("Voucher Type does not exists.");
            }
            if (!dtchkvoucher.Columns.Contains(GENERAL_LEDGER))
            {
                Isvalid = false;
                throw new Exception("General Ledger does not exists.");
            }
            else if (!dtchkvoucher.Columns.Contains(LEDGER_AMOUNT))
            {
                Isvalid = false;
                throw new Exception("Ledger Amount does not exists.");
            }
            else if (UtilityMember.NumberSet.ToDouble(dtchkvoucher.Rows[0][LEDGER_AMOUNT].ToString()) <= 0)
            {
                Isvalid = false;
                throw new Exception("Provided ledger amount is invalid.");
            }
            else if (!dtchkvoucher.Columns.Contains(CASHBANK_LEDGER))
            {
                Isvalid = false;
                throw new Exception("Cash/Bank Ledger does not exists.");
            }
            else if (!dtchkvoucher.Columns.Contains(CASHBANK_AMOUNT))
            {
                Isvalid = false;
                throw new Exception("Cash/Bank Amount does not exists.");
            }
            else if (UtilityMember.NumberSet.ToDouble(dtchkvoucher.Rows[0][CASHBANK_AMOUNT].ToString()) <= 0)
            {
                Isvalid = false;
                throw new Exception("Provided Cash/Bank amount is invalid.");
            }
            else if (!dtchkvoucher.Columns.Contains(BANK_REF_NO))
            {
                Isvalid = false;
                throw new Exception("Bank Reference No does not exists.");
            }
            else if (!dtchkvoucher.Columns.Contains(MATERIALISED_ON))
            {
                Isvalid = false;
                throw new Exception("Materialised on does not exists.");
            }
            else if (dtchkvoucher.Rows[0][GENERAL_TRANS_MODE].Equals("CR") && dtchkvoucher.Rows[0][CASHBANK_TRANS_MODE].Equals("CR") ||
               dtchkvoucher.Rows[0][GENERAL_TRANS_MODE].Equals("DR") && dtchkvoucher.Rows[0][CASHBANK_TRANS_MODE].Equals("DR"))
            {
                Isvalid = false;
                throw new Exception("Provided voucher does not have valid transaction entry (CR/DR)");
            }

            else if (this.UtilityMember.NumberSet.ToDouble(dtchkvoucher.Compute("SUM(LEDGER_AMOUNT)", "").ToString()) != this.UtilityMember.NumberSet.ToDouble(dtchkvoucher.Compute("SUM(CASHBANK_AMOUNT)", "").ToString()))
            {
                Isvalid = false;
                throw new Exception("Provided voucher does have equal amount ");
            }
            return Isvalid;
        }
        private bool IsValidVoucherDate(DataTable dtdate)
        {
            bool IsValiddate = true;
            DateTime dtyearfrom;
            DateTime dtbookbeginfrom;
            DateTime dtYearTo;
            DateTime dtVoucherDate;

            dtyearfrom = UtilityMember.DateSet.ToDate(SettingProperty.Current.YearFrom, false);
            dtbookbeginfrom = UtilityMember.DateSet.ToDate(SettingProperty.Current.BookBeginFrom, false);
            dtYearTo = UtilityMember.DateSet.ToDate(SettingProperty.Current.YearTo, false);
            dtVoucherDate = UtilityMember.DateSet.ToDate(dtdate.Rows[0][VOUCHER_DATE].ToString(), false);

            if ((dtVoucherDate < dtyearfrom || dtVoucherDate > dtYearTo))
            {
                IsValiddate = false;
            }
            else if ((dtVoucherDate < dtbookbeginfrom && dtyearfrom > dtbookbeginfrom))
            {
                IsValiddate = false;
            }
            else if ((dtVoucherDate < dtbookbeginfrom && dtyearfrom < dtbookbeginfrom))
            {
                IsValiddate = false;
            }
            return IsValiddate;
        }

        public DataSet GetPayrollMasters(string ServiceKey)
        {
            try
            {
                if (CheckCredentials(ServiceKey))
                {
                    AcMELog.WriteLog("GetPayroll Masters Started..");
                    resultArgs = new ResultArgs();
                    using (clsprCompBuild PayrollComponent = new clsprCompBuild())
                    {
                        using (clsPrGateWay PayrollCreate = new clsPrGateWay())
                        {
                            AcMELog.WriteLog("Fetch Payroll Component started..");
                            resultArgs = PayrollComponent.PayrollComponentDetails();
                            if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                            {
                                resultArgs.DataSource.Table.TableName = "COMPONENTS";
                                dsPayrollMasterDetails.Tables.Add(resultArgs.DataSource.Table);
                                AcMELog.WriteLog("Fetch Payroll Component ended..");
                                resultArgs = PayrollCreate.FetchPayrollList();
                                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                                {
                                    AcMELog.WriteLog("Fetch Payroll List started..");
                                    resultArgs.DataSource.Table.TableName = "PAYROLL_LIST";
                                    dsPayrollMasterDetails.Tables.Add(resultArgs.DataSource.Table);
                                    AcMELog.WriteLog("Fetch Payroll List ended..");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AcMELog.WriteLog("Problem in sending Payroll Masters : " + ex.Message.ToString());
                throw;
            }
            return dsPayrollMasterDetails;
        }

        public int PostStaffDetails(string dtsStaffDetails, string ServiceKey)
        {
            DataTable dtStaff = new DataTable("Staff");
            TextReader srStaff = new StringReader(dtsStaffDetails);
            dtStaff.ReadXml(srStaff);
            resultArgs = new ResultArgs();
            try
            {
                AcMELog.WriteLog("Staff Saved Started..");
                if (CheckCredentials(ServiceKey))
                {
                    if (dtStaff != null && dtStaff.Rows.Count > 0)
                    {
                        foreach (DataRow drStaff in dtStaff.Rows)
                        {
                            using (clsPayrollStaff Staff = new clsPayrollStaff())
                            {
                                string LeaveDate = string.Empty;
                                string retirementdate = string.Empty;
                                Int32 GroupId = 0;
                                Staff.StaffId = 0;
                                AcMELog.WriteLog("Fetch Payroll Group Id by Staff started..");
                                resultArgs = Staff.FetchGroupNameBYID(drStaff[GROUP].ToString());
                                if (!resultArgs.Success || resultArgs.DataSource.Sclar.ToInteger <= 0)
                                {
                                    using (clsPayrollGrade GroupList = new clsPayrollGrade())
                                    {
                                        GroupList.strGrade = (!string.IsNullOrEmpty(drStaff[GROUP].ToString())) ? drStaff[GROUP].ToString() : string.Empty;
                                        resultArgs = GroupList.SavePayrollGroup();
                                        if (resultArgs.Success)
                                        {
                                            GroupId = UtilityMember.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                        }
                                    }
                                }
                                else
                                {
                                    GroupId = resultArgs.DataSource.Sclar.ToInteger;
                                }
                                AcMELog.WriteLog("Fetch Payroll Group Id by Staff ended..");

                                Staff.ScaleofPay = drStaff[SCALEOFPAY].ToString();
                                Staff.IncrementMonth = (!string.IsNullOrEmpty(drStaff[MONTH_INCREAMENT].ToString())) ? drStaff[MONTH_INCREAMENT].ToString() : "0";
                                string EmpNo = drStaff[EMPNO].ToString();
                                string FirstName = drStaff[FIRSTNAME].ToString();
                                string MiddleName = string.Empty;
                                string Fatherhusbandname = string.Empty;
                                string Mothername = string.Empty;
                                string Noofchildren = string.Empty;
                                string BloodGroup = string.Empty;
                                string PayingSalaryDays = string.Empty;
                                string LastDateofContract = string.Empty;
                                string LastName = drStaff[LASTNAME].ToString();
                                string Gender = drStaff[GENDER].ToString();
                                string Category = string.Empty;
                                string DOB = drStaff[DATEOFBIRTH].ToString();
                                string DOJ = drStaff[DATEOFJOIN].ToString();

                                if (RETIREMENTDATE.Equals(DateTime.MinValue.ToString()))
                                {
                                    retirementdate = (!string.IsNullOrEmpty(drStaff[RETIREMENTDATE].ToString())) ? drStaff[RETIREMENTDATE].ToString() : string.Empty;
                                }
                                else
                                {
                                    retirementdate = string.Empty;
                                }

                                string KnownAs = (!string.IsNullOrEmpty(drStaff[KNOWNAS].ToString())) ? drStaff[KNOWNAS].ToString() : string.Empty;
                                if (LEAVINGDATE.Equals(DateTime.MinValue.ToString()))
                                {
                                    LeaveDate = (!string.IsNullOrEmpty(drStaff[LEAVINGDATE].ToString())) ? drStaff[LEAVINGDATE].ToString() : string.Empty;
                                }
                                else
                                {
                                    LeaveDate = string.Empty;
                                }
                                string LeavingReason = (!string.IsNullOrEmpty(drStaff[LEAVINGREMARKS].ToString())) ? drStaff[LEAVINGREMARKS].ToString() : string.Empty;
                                string Designation = drStaff[DESIGNATION].ToString();
                                string Degree = string.Empty;
                                string Department = string.Empty;
                                string UANnumber = (!string.IsNullOrEmpty(drStaff[UAN].ToString())) ? drStaff[UAN].ToString() : string.Empty; ;
                                int OptionData = (int)clsPayrollConstants.PAYROLL_STAFF_INSERT;

                                Staff.ThirdPartyId = (!string.IsNullOrEmpty(drStaff[STAFF_REF_ID].ToString())) ? drStaff[STAFF_REF_ID].ToString() : string.Empty; ;

                                resultArgs = Staff.FetchStaffIdByStaffThirdPartyID();
                                if (resultArgs.Success && resultArgs.DataSource.Sclar.ToInteger > 0)
                                {
                                    Staff.StaffId = resultArgs.DataSource.Sclar.ToInteger;
                                }
                                if (Staff.StaffId > 0)
                                {
                                    OptionData = (int)clsPayrollConstants.PAYROLL_STAFF_EDIT;
                                }
                                string ScaleofPay1 = (!string.IsNullOrEmpty(drStaff[SCALEOFPAY].ToString())) ? drStaff[SCALEOFPAY].ToString() : string.Empty;
                                string AccountNo = (!string.IsNullOrEmpty(drStaff[ACCOUNT_NO].ToString())) ? drStaff[ACCOUNT_NO].ToString() : string.Empty;
                                string YearOfExperience = (!string.IsNullOrEmpty(drStaff[YOS].ToString())) ? drStaff[YOS].ToString() : "0.00";
                                string commandonperformance = string.Empty;
                                string address = string.Empty;
                                string mobileno = string.Empty;
                                string telephoneno = string.Empty;
                                string emergencycontactno = string.Empty;
                                string emailid = string.Empty;
                                string dependent1 = string.Empty;
                                string dependent2 = string.Empty;
                                string dependent3 = string.Empty;
                                string workexperience = string.Empty;
                                string panno = string.Empty;
                                string aadharno = string.Empty;

                                decimal MaxwagesHire = 0;
                                decimal MaxWagesBasic = 0;

                                string overtimepay = string.Empty;
                                string gradepay = string.Empty;
                                string specialpay = string.Empty;
                                string performancepay = string.Empty;
                                string decrementamount = string.Empty;
                                string payingsalarydays = string.Empty;

                                resultArgs = Staff.savePayrollStaffData(GroupId, EmpNo, EmpNo, FirstName, MiddleName, Fatherhusbandname, Mothername, Noofchildren, BloodGroup, LastDateofContract, LastName, Gender,
                                    DOB, DOJ, retirementdate, KnownAs, LeaveDate, LeavingReason, Designation, Degree, Department, MaxwagesHire, MaxWagesBasic, overtimepay, gradepay, specialpay, performancepay, decrementamount, payingsalarydays,
                                    UANnumber, OptionData, 0, ScaleofPay1, AccountNo, this.UtilityMember.NumberSet.ToDouble(YearOfExperience), commandonperformance, address, mobileno, telephoneno, emergencycontactno, emailid, dependent1, dependent2, dependent3, workexperience, panno, aadharno, string.Empty, string.Empty);
                                if (resultArgs.Success)
                                {
                                    int ProjectId = 0;
                                    string grpStaff = Staff.StaffId != 0 ? Staff.StaffId.ToString() : Staff.RtnStaffid;
                                    PayrollStaffReturnValue = Convert.ToInt32(grpStaff.ToString());
                                    /*using (clsPrGroupStaff GroupStaff = new clsPrGroupStaff())
                                    {
                                        resultArgs = GroupStaff.DeleteStaffGroup(grpStaff);
                                        if (resultArgs.Success)
                                        {
                                            string strResult = GroupStaff.SaveNewStaffInGroup(GroupId, grpStaff);
                                            if (SettingProperty.PayrollFinanceEnabled)
                                            {
                                                if (!string.IsNullOrEmpty(grpStaff))
                                                {
                                                    resultArgs = GroupStaff.DeleteProjectStaff(grpStaff);
                                                    if (resultArgs.Success)
                                                    {
                                                        resultArgs = GroupStaff.SaveProjectStaff(ProjectId, grpStaff);
                                                    }
                                                    else
                                                    {
                                                        if (resultArgs.Success)
                                                        {
                                                            AcMELog.WriteLog("Staff Details Successfully Saved ");
                                                        }
                                                    }
                                                }
                                            }
                                        }  
                                    }*/

                                    if (Staff.StaffId == 0)
                                    {
                                        AcMELog.WriteLog("Staff Details successfully Saved");
                                    }
                                    else
                                    {
                                        AcMELog.WriteLog("Staff Details successfully Updated");
                                    }
                                }
                            }
                        }

                        if (resultArgs.Success == true)
                        {
                            lock (objlock)
                            {
                                frmPopup alert = new frmPopup(PopupSkins.InfoSkin);
                                string Flag = resultArgs.Success == true ? "Posted Staff Details" : "";
                                alert.ShowPopup(" ", "Staff Posted  " + "" + " on " + DateTime.Now.ToShortDateString() + "\nPayroll Status :" + Flag + "", 500, 6000, 500);
                            }
                            AcMELog.WriteLog("Staff Payroll ended..");
                        }
                        else
                        {
                            resultArgs.Success = false;
                        }

                        //if (resultArgs.Success)
                        //{
                        //    lock (objlock)
                        //    {
                        //        frmPopup alert = new frmPopup(PopupSkins.InfoSkin);
                        //        string DataOfJoiningDate = UtilityMember.DateSet.ToDate(dtStaff.Rows[0][DATEOFJOIN].ToString(), false).ToShortDateString();
                        //        PostStaffId = Staff.CommonId > 0 ? Staff.CommonId : 0;
                        //        string Flag = Staff.CommonId > 0 ? "Updated" : "Added";
                        //        alert.ShowPopup(dtStaff.Rows[0]["STAFF_REF_ID"].ToString() + " Source :  ", "Staff Posted  " + dtStaff.Rows[0][FIRSTNAME].ToString() + " on " + DataOfJoiningDate + "\nStaff Status :" + Flag + "", 500, 6000, 500);
                        //    }
                        //    AcMELog.WriteLog("Post staff ended..");
                        //}
                        //else
                        //{
                        //    PostStaffId = -1;
                        //}


                    }
                    else
                    {
                        AcMELog.WriteLog("Provided Data is invalid,try again..");
                        throw new Exception("Provided Data is invalid,try again..");
                    }
                }
            }
            catch (Exception eg)
            {
                AcMELog.WriteLog("Problem in Staff Details : " + eg.Message.ToString());
                throw eg;
            }
            return PostStaffId;
        }

        public bool PostPayroll(DataTable dtPayrolldetails, string ServiceKey)
        {
            bool status = false;
            ProgressBar prograssbar = new ProgressBar();
            prograssbar.Value = 1;
            prograssbar.Minimum = 0;
            prograssbar.Maximum = 1;
            if (CheckCredentials(ServiceKey))
            {
                if (dtPayrolldetails != null && dtPayrolldetails.Rows.Count > 0)
                {
                    foreach (DataRow drPayroll in dtPayrolldetails.Rows)
                    {
                        using (clsprCompBuild ComponentBuild = new clsprCompBuild())
                        {
                            using (clsPayrollStaff PayrollStaff = new clsPayrollStaff())
                            {
                                string CompName = string.Empty;
                                string StaffId = string.Empty;
                                long PayrollId = this.UtilityMember.NumberSet.ToInteger(drPayroll[ColPayrollId].ToString());
                                PayrollStaff.ThirdPartyId = (!string.IsNullOrEmpty(drPayroll[ColStaffId].ToString())) ? drPayroll[ColStaffId].ToString() : string.Empty; ;
                                resultArgs = PayrollStaff.FetchStaffIdByStaffThirdPartyID();
                                if (resultArgs.Success && resultArgs.DataSource.Sclar.ToInteger > 0)
                                {
                                    StaffId = resultArgs.DataSource.Sclar.ToInteger.ToString();
                                }

                                ComponentBuild.ComponentId = this.UtilityMember.NumberSet.ToInteger(drPayroll[ColComponentId].ToString());
                                resultArgs = ComponentBuild.FetchPayrollComponentNameByCompId();
                                if (resultArgs.Success)
                                {
                                    CompName = resultArgs.DataSource.Data.ToString();
                                }
                                string CompValue = drPayroll[ColComponentValue].ToString();
                                status = ComponentBuild.ModifyStaffComponent(PayrollId, "", CompName, StaffId, CompValue, prograssbar);
                            }
                        }
                    }

                    if (status == true)
                    {
                        AcMELog.WriteLog("Post Payroll Details successfully Saved");
                    }
                    if (status == true)
                    {
                        lock (objlock)
                        {
                            frmPopup alert = new frmPopup(PopupSkins.InfoSkin);
                            string Flag = status == true ? "Updated" : "";
                            alert.ShowPopup(" ", "Payroll Posted  " + "" + " on " + DateTime.Now.ToShortDateString() + "\nPayroll Status :" + Flag + "", 500, 6000, 500);
                        }
                        AcMELog.WriteLog("Post Payroll ended..");
                    }
                    else
                    {
                        status = false;
                    }
                }
                else
                {
                    AcMELog.WriteLog("Provided Data is invalid,try again..");
                    throw new Exception("Provided Data is invalid,try again..");
                }
            }
            return status;
        }

        #endregion


        public int DeleteVouchers(string ClientCode, string ClientRefcode, string ServiceKey)
        {
            int successId = 0;
            bool isValid = true;
            try
            {
                AcMELog.WriteLog("Delete Vouchers Started..");
                if (CheckCredentials(ServiceKey))
                {
                    if (string.IsNullOrEmpty(ClientCode))
                    {
                        isValid = false;
                        throw new Exception("Client Code is empty or invalid");
                    }
                    else if (string.IsNullOrEmpty(ClientRefcode))
                    {
                        isValid = false;
                        throw new Exception("Client Reference Code is empty or invalid");
                    }
                    if (isValid)
                    {
                        using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                        {
                            voucherTransaction.ClientReferenceId = UtilityMember.NumberSet.ToInteger(ClientRefcode).ToString();
                            voucherTransaction.ClientCode = ClientCode;
                            resultArgs = voucherTransaction.FetchVoucherIdByClientRefCode();
                            if (resultArgs.Success && resultArgs.DataSource.Sclar.ToInteger > 0)
                            {
                                voucherTransaction.VoucherId = resultArgs.DataSource.Sclar.ToInteger;
                                resultArgs = voucherTransaction.DeleteVoucherTrans();
                                if (resultArgs.Success)
                                {
                                    lock (objlock)
                                    {
                                        frmPopup alert = new frmPopup(PopupSkins.InfoSkin);
                                        PostVoucherId = voucherTransaction.VoucherId > 0 ? voucherTransaction.VoucherId : 0;
                                        alert.ShowPopup("Acme.erp", "Voucher Deleted from source : " + ClientCode + ". \n Voucher Reference No :" + PostVoucherId + "", 500, 6000, 500);
                                    }
                                    successId = 1;
                                    AcMELog.WriteLog("Delete Vouchers ended..");
                                }
                            }
                        }
                    }
                }
                else
                {
                    AcMELog.WriteLog("Provided Credentials is incorrect,try again..");
                    throw new Exception("Provided Credentials is incorrect,try again..");
                }
            }
            catch (Exception eg)
            {
                AcMELog.WriteLog("Problem in Delete vouchers : " + eg.Message.ToString());
                throw eg;
            }
            return successId;
        }
    }
}

