/*  Class Name      : ReportProperty.cs
 *  Purpose         : To get Assembly Qualified Name of a report
 *                    by selected report id and get report source 
 *                    from mapped report interface object
 *  Author          : CS
 *  Created on      : 21-Jul-2009
 */

using System;
using System.Data;
using Bosco.Report;
using System.Xml;
using Bosco.Utility;
using System.Resources;
using System.Reflection;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Utility.ConfigSetting;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;

namespace Bosco.Report.Base
{
    public class ReportProperty : CommonMember
    {
        private static ReportProperty current = null;
        private DataView dvReportSettingInfo = null;
        private ReportSetting.ReportParameterDataTable ReportParameters = new ReportSetting.ReportParameterDataTable();
        private ReportSetting.ReportSettingDataTable dtReportSettingSchema = new ReportSetting.ReportSettingDataTable();
        ReportSetting.ReportSignDataTable dtReportSignSchema = new ReportSetting.ReportSignDataTable();
        ReportSetting.ReportBudgetNewProjectDataTable dtReportBudgjetNewProjectSchema = new ReportSetting.ReportBudgetNewProjectDataTable();

        private static object objLoc = new object();
        private string reportSettingFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ReportSetting.xml"); //"ReportSetting.xml"; //Application.StartupPath.ToString()// Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
        private string reportId = "";
        public string PrevreportId = "";
        string AcmeerpInstalledPath = AppDomain.CurrentDomain.BaseDirectory;

        public static ReportProperty Current
        {
            get
            {
                lock (objLoc)
                {
                    if (current == null)
                    {
                        current = new ReportProperty();
                        current.LoadReportSetting();
                    }
                }

                return current;
            }
            set
            {
                current = value;
            }
        }

        public string ReportId
        {
            get { return reportId; }
            set
            {
                PrevreportId = !string.IsNullOrEmpty(PrevreportId) ? reportId : value;
                reportId = value;
                SetReportSettingInfo();
            }
        }


        public string ReportGroup { get; set; }
        public string ReportName { get; set; }
        private string rpttitle = string.Empty;
        public string ReportTitle
        {
            get
            {
                string ReportLangTitleId = reportId.Replace("-", "_") + "_TITLE";
                string RptTitleMessage = MessageRender.GetReportMessage(ReportLangTitleId);
                rpttitle = string.IsNullOrEmpty(RptTitleMessage) ? rpttitle : RptTitleMessage;
                return rpttitle;
            }
            set
            {
                rpttitle = value;
            }
        }

        public string ReportDescription { get; set; }
        public string ReportAssembly { get; set; }
        public string AccounYear { get; set; }

        public Stack<EventDrillDownArgs> stackActiveDrillDownHistory = new Stack<EventDrillDownArgs>();

        string datefrom = string.Empty;
        public string DateFrom
        {
            get
            {
                return datefrom;
            }
            set
            {
                datefrom = value;
            }
        }
        string dateto = string.Empty;
        public string DateTo
        {
            get { return dateto; }
            set { dateto = value; }
        }
        public string DateAsOn { get; set; }
        public int IncludeAllLedger { get; set; }
        public string LedgerNature { get; set; }
        public int ShowAllCountry { get; set; }
        public int ShowLedgerOpBalance { get; set; }
        public int ShowAssetLiabilityLedgerOpBalance { get; set; } //As On 25/06/2021, To show only Asset and Liabilities Ledger Opening aAlone
        public int ShowByLedger { get; set; }
        public int ShowByLedgerGroup { get; set; }
        public int ShowByCostCentre { get; set; }
        public int ShowByCostCentreCategory { get; set; }
        public int ShowByLedgerSummary { get; set; }
        public int BreakByCostCentre { get; set; }
        public int BreakbyDonor { get; set; }
        public int BreakByLedger { get; set; }
        public int Consolidated { get; set; }
        public int ShowIndividualLedger { get; set; } //Show all Individual ledgers (Cash, Bank Journal, if same ledger used multi times)
        public int ShowDailyBalance { get; set; }
        public int IncludeJournal { get; set; }
        public int IncludeInKind { get; set; }
        public int IncludeLedgerGroupTotal { get; set; }
        public int ShowNarrationMonthwiseCumulativeTotal { get; set; }
        public int ShowNarrationMonthwiseCumulativeOpBalance { get; set; }
        public int IncludeLedgerGroup { get; set; }
        public int IncludeCostCentre { get; set; }
        public int ShowMonthTotal { get; set; }
        public int ShowDonorAddress { get; set; }
        public int ShowDonorCategory { get; set; }
        public int IncludeBankAccount { get; set; }
        public int IncludeBankDetails { get; set; }
        public int ShowDetailedBalance { get; set; }
        public int ShowDetailedCashBalance { get; set; }
        public int ShowDetailedBankBalance { get; set; }
        public int ShowDetailedFDBalance { get; set; }
        public int IncludeDetailed { get; set; }
        public string Project { get; set; }
        public DataTable dtProjectSelected { get; set; } //On 15/11/2024
        public int SocietyId { get; set; }
        public int ITRGroupId { get; set; }
        public string ProjectId { get; set; }
        public string BankAccount { get; set; }
        public string CashBankGroupId { get; set; }
        public string InstituteName { get; set; }
        public string GSTState { get; set; }
        public string GSTStateCode { get; set; }
        public string SocietyName { get; set; }
        public string LegalName { get; set; }
        public string LegalAddress { get; set; }
        public string LegalTelephone { get; set; }
        public string LegalGSTNo { get; set; }
        public string LedgalEntityId { get; set; }
        public int ShowTitleSocietyName { get; set; }
        public int ReportBorderStyle { get; set; }
        public int ColumnCaptionFontStyle { get; set; }
        public int HideContraNote { get; set; }
        public int HideLedgerName { get; set; }
        public int ShowCash { get; set; }
        public int ShowBank { get; set; }

        public int ReportCodeType { get; set; }

        public string FDAccountID { get; set; }
        public string FDAccount { get; set; }
        public string SelectedBankFD { get; set; }
        public string BudgetId { get; set; }
        public string Budget { get; set; }
        public string BudgetProject { get; set; }
        public string CompareBudgetId { get; set; }
        public string UnSelectedBudgetId { get; set; }
        public string UnSelectedBankAccountId { get; set; }
        public string BankAccountId { get; set; }
        public string CashBankLedger { get; set; }
        public string Ledger { get; set; }
        public string Country { get; set; }
        public string SelectedLedgerName { get; set; }
        public string LedgerGroup { get; set; }
        public string State { get; set; }
        public string StateDonaud { get; set; }
        public int TaskID { get; set; }
        public string DonarName { get; set; }
        public string UnSelectedLedgerId { get; set; }
        public string UnSelectedCountryId { get; set; }
        public string CostCentre { get; set; }
        public int CCCategoryId { get; set; }
        public string Narration { get; set; }
        public string NatureofPaymets { get; set; }
        public Dictionary<string, object> DrillDownProperties { get; set; }
        public string ReportCriteria { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectNameWithoutDivision { get; set; }
        public string ProjectNamewithSno { get; set; }
        public string ProjectNamewithITRGroup { get; set; }
        public string ProjectITRGroup { get; set; }
        public string ProjectITRGroupIds { get; set; }
        public Int32 AllProjectsCount { get; set; }
        public string CostCentreName { get; set; }
        public string BudgetName { get; set; }
        public string BudgetDateRangeInMonths { get; set; }
        public string VoucherType { get; set; }
        public int BudgetEditValue { get; set; }
        public int BudgetCompareEditValue { get; set; }
        public string CostCentreLedgerId { get; set; }

        public string PayrollId { get; set; }
        public string PayrollStaffId { get; set; }
        public string PayrollGroupId { get; set; }
        public string PayrollComponentId { get; set; }
        public string PayrollComponentId1 { get; set; }
        public string PayrollComponentId2 { get; set; }
        public string PayrollName { get; set; }
        public string PayrollPayrollDate { get; set; }
        public string PayrollGroupName { get; set; }
        public string PayrollComponentName { get; set; }
        public string PayrollComponentName1 { get; set; }
        public string PayrollComponentName2 { get; set; }
        public string PayrollStaffName { get; set; }
        public string PayrollProjectId { get; set; }
        public Int32 PayrollTitleType { get; set; }
        public string PayrollProjectTitle { get; set; }
        public string PayrollProjectAddress { get; set; }
        public string PayrollSignOfEmployer { get; set; }
        public string PayrollAuthorisedSignatory2 { get; set; }
        public Int32 PayrollPaymentModeId { get; set; }
        public string PayrollPaymentMode { get; set; }
        public Int32 PayrollPaymentModeByBank { get; set; }
        public string PayrollPaymentBankAccountLedgerId { get; set; }
        public string PayrollPaymentBankAccountNo { get; set; }
        public string PayrollPaymentBankAddress { get; set; }
        public int PayrollShowComponentDescription { get; set; }
        public int PayrollPayslipInSeparatePages { get; set; }
        public int PayrollGroupConsolidation { get; set; }

        public string PayrollDepartmentId { get; set; }
        public string PayrollDepartmentName { get; set; }

        public int TitleAlignment { get; set; }

        public int Count { get; set; }
        public string BankAccountName { get; set; }
        public string ReportPath { get; set; }
        string _reportdate = "";
        public string ReportDate
        {
            get { return _reportdate; }
            set
            {
                _reportdate = value;
                ShowPrintDate = ((_reportdate == "") ? 0 : 1);
            }
        }

        public int ShowLedgerCode { get; set; }
        public int ShowGroupCode { get; set; }
        public int SortByLedger { get; set; }
        public int SortByGroup { get; set; }
        public int IncludeNarration { get; set; }
        public int IncludeNarrationwithRefNo { get; set; }
        public int IncludePanwithGSTNo { get; set; }
        public int IncludeNarrationwithNameAddress { get; set; }
        public int IncludeNarrationwithCurrencyDetails { get; set; }
        public int IncludeMale { get; set; }
        public int IncludeFemale { get; set; }
        public int IncludeSent { get; set; }
        public int IncludeNotSent { get; set; }
        public int AnniversaryType { get; set; }
        public int Wedding { get; set; }
        public int IncludeInstitutional { get; set; }
        public int IncludeIndividual { get; set; }
        public int IncludeAllPurposes { get; set; }
        public int FDRegisterStatus { get; set; }
        public int FDScheme { get; set; }
        public string FDSchemeName { get; set; }
        public int FDInvestmentType { get; set; }
        public string FDInvestmentTypeName { get; set; }
        public int ShowByBank { get; set; }
        public int ShowByInvestment { get; set; }
        public int ShowHorizontalLine { get; set; }
        public int ShowVerticalLine { get; set; }
        public int SupressZeroValues { get; set; }
        public int ShowIndividualProjects { get; set; }
        public int ShowTitles { get; set; }
        public int ShowLogo { get; set; }
        public int ShowPageNumber { get; set; }
        public int ShowPrintDate { get; set; }
        public int ShowReportDate { get; set; }
        public string SetWithForCode { get; set; }
        public int RecordCount { get; set; }
        public int HeaderInstituteSocietyName { get; set; }
        public int HeaderInstituteSocietyAddress { get; set; }
        public int HeaderWithInstituteName { get; set; }
        public int IncludeOutsideParishioner { get; set; }

        public int SelectedProjectCount { get; set; }
        public DataTable CashBankVoucher { get; set; }
        public static DataTable dtLedgerEntity { get; set; }
        public DataTable dtCBJ = null;
        public DataTable CashBankJouranlByVoucher
        {
            get { return dtCBJ; }
            set { dtCBJ = value; }
        }
        public int CashBankProjectId { get; set; }
        public string PrintCashBankVoucherId { get; set; }
        public Int32 PrintPurchaseInoutVoucherId { get; set; }
        public string ModuleType { get; set; }
        public Int32 ChequeBankVoucherId { get; set; }
        public Int32 ChequeBankId { get; set; }
        public DateTime CashBankVoucherDate { get; set; }
        public DateTime CashBankVoucherDateFrom { get; set; }
        public DateTime CashBankVoucherDateTo { get; set; }
        public DataTable dtTDSChallan { get; set; }
        public List<int> enumUserRights = new List<int>();
        public int ReportFlag = 0;
        public int ShowOpeningBalance = 0;
        public int ShowClosingBalance = 0;
        public int ShowCurrentTransaction = 0;
        public int ShowByDonorGroup = 0;
        public int DayBookVoucherType { get; set; }
        public string UserName { get; set; }
        public string CreatedByName { get; set; }
        public string ModifiedByName { get; set; }
        public string AuditAction { get; set; }

        public string DayBookVoucherTypeName { get; set; }
        public int NoOfYears { get; set; }
        //public string LocationId = string.Empty;
        public string StockItemId { get; set; }
        public string AssetItemID { get; set; }
        public int ShowByLocation { get; set; }
        public string LocationId { get; set; }
        public string RegistrationTypeId { get; set; }
        public string CountryId { get; set; }
        public string StateId { get; set; }
        public string Language { get; set; }
        public int ShowByGroup { get; set; }

        public string DonorConditionSymbol { get; set; }
        public int DonorCondtionName { get; set; }
        public double DonorFilterAmount { get; set; }
        public string SelectedTaskName { get; set; }
        public int SelectedTaskCount { get; set; }
        public string Task { get; set; }

        public int SelectedAssetclassCount { get; set; }
        public string Assetclass { get; set; }
        public Int32 IncludeSignDetails { get; set; }
        public Int32 IncludeAuditorSignNote { get; set; }
        public string PrintReceiptId { get; set; }

        public int ShowOnlyReceipts = 0;
        public int ExcludeCashWithdrawal = 0;
        public int ShowOnlyPayments = 0;
        public int ExcludeCashDeposit = 0;

        //This is used for Standard Report, This property is used to have common ui and look and feel for master list standard reports ------------------
        //It is only for Standard Report
        public GridView GridViewStandardReport { get; set; }
        public GridView AdditionalGridViewStandardReport { get; set; }
        public string AdditionText { get; set; }

        // This is used to print the labels for this datasource
        public DataTable DonorLabelPrint { get; set; }

        // GST Invoice Print Preview without updating Vouchers
        public DataTable dtGSTInvoicePrintPreview { get; set; }

        public int ShowTableofContent { get; set; }

        //On 13/09/2024, To show set average euro exchange rate for local currency and dolloar
        double avgEuroExchangeRate = 1;
        public double AvgEuroExchangeRate
        {
            get { return (avgEuroExchangeRate <= 0 ? 1 : avgEuroExchangeRate); }
            set { avgEuroExchangeRate = value; }
        }

        double avgEuroDollarExchangeRate = 1;
        public double AvgEuroDollarExchangeRate
        {
            get { return (avgEuroDollarExchangeRate <= 0 ? 1 : avgEuroDollarExchangeRate); }
            set { avgEuroDollarExchangeRate = value; }
        }

        public int ShowForexDetail { get; set; }

        public int CurrencyCountryId { get; set; }
        public string CurrencyCountry { get; set; }
        public string CurrencyCountrySymbol { get; set; }

        /// <summary>
        /// On 25/10/2021, to select Report Chart Type
        /// 
        /// On 25/01/2021, to retain View Chart Type in Report Settings
        /// </summary>
        public Int32 ChartViewType { get; set; }

        public Int32 ChartInPercentage { get; set; }

        /// <summary>
        //On 22/02/2021, whether to show all against ledger names or show only top of the agaist ledger
        /// </summary>
        public Int32 ShowAllAgainstLedgers { get; set; }

        /// <summary>
        //On 22/02/2021, whether to show Cost Center Details 
        /// </summary>
        public Int32 ShowCCDetails { get; set; }

        /// <summary>
        //On 19/05/2023, whether to show Donor Details 
        /// </summary>
        public Int32 ShowDonorDetails { get; set; }

        public Int32 LicenseBased { get; set; }
        /// <summary>
        //On 19/02/2023, whether to show Ledger-wise Cost Center Details 
        /// </summary>
        public Int32 ShowLedgerwiseCCDetails { get; set; }

        //On 11/12/2021, Show by Soceity, Show by Project Name and Show by Budget Group
        public Int32 ShowBySociety { get; set; }
        public Int32 ShowByProject { get; set; }
        public Int32 ShowByBudgetGroup { get; set; }
        public Int32 ShowGSTVouchers { get; set; }
        public Int32 ShowGSTInvoiceVouchers { get; set; }
        public Int32 ShowIndividualDepreciationLedgers { get; set; }
        public Int32 ShowFixedDepositVoucherDetail { get; set; }

        public Int32 IncludeAllBudgetLedgers { get; set; }

        public Int32 IncludeFDSimpleInterest { get; set; }
        public Int32 IncludeFDAccumulatedInterest { get; set; }

        //On 03/02/2023, To retain Page Setting ---------------------------------------------
        public System.Drawing.Printing.PaperKind PaperKind { get; set; }
        public Int32 PaperLandscape { get; set; }
        public Int32 MarginLeft { get; set; }
        public Int32 MarginRight { get; set; }
        public Int32 MarginTop { get; set; }
        public Int32 MarginBottom { get; set; }
        //------------------------------------------------------------------------------------

        //On 09/01/2025 - To have define Receipt, Payment Sort order
        //0 - Default, 1- Receipt Side 2- Payment Side (for Cash and Bank book, if Receipt vno manual generation
        // and Payment automatic
        public Int32 RandPSortOrder { get; set; }

        // Voucher Print setting
        private static DataView dvSetting = null;

        private string GetReportSettingInfo(string name)
        {
            string val = "";

            try
            {
                if (dvSetting != null && dvSetting.Count > 0)
                {
                    dvSetting.RowFilter = "SETTING_NAME ='" + name + "'";
                    if (dvSetting != null && dvSetting.Count > 0)
                    {
                        val = dvSetting[0]["VALUE"].ToString();
                    }
                    dvSetting.RowFilter = "";
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }

            return val;
        }

        public DataView VoucherPrintSettingInfo
        {
            set
            {
                ReportProperty.dvSetting = value;
            }
            get
            {
                return dvSetting;
            }
        }

        public string VoucherPrintSign1Row1
        {
            get
            {
                return GetReportSettingInfo(AcmeReportSetting.VoucherPrintSign1Row1.ToString());
            }
        }

        public string VoucherPrintSign1Row2
        {
            get
            {
                return GetReportSettingInfo(AcmeReportSetting.VoucherPrintSign1Row2.ToString());
            }
        }

        public string VoucherPrintSign2Row1
        {
            get
            {
                return GetReportSettingInfo(AcmeReportSetting.VoucherPrintSign2Row1.ToString());
            }
        }

        public string VoucherPrintSign2Row2
        {
            get
            {
                return GetReportSettingInfo(AcmeReportSetting.VoucherPrintSign2Row2.ToString());
            }
        }

        public string VoucherPrintSign3Row1
        {
            get
            {
                return GetReportSettingInfo(AcmeReportSetting.VoucherPrintSign3Row1.ToString());
            }
        }

        public string VoucherPrintSign3Row2
        {
            get
            {
                return GetReportSettingInfo(AcmeReportSetting.VoucherPrintSign3Row2.ToString());
            }
        }

        public string VoucherPrintCaptionBold
        {
            get
            {
                return GetReportSettingInfo(AcmeReportSetting.VoucherPrintCaptionBold.ToString());
            }
        }

        public string VoucherPrintValueBold
        {
            get
            {
                return GetReportSettingInfo(AcmeReportSetting.VoucherPrintValueBold.ToString());
            }
        }

        public string VoucherPrintShowLogo
        {
            get
            {
                return GetReportSettingInfo(AcmeReportSetting.VoucherPrintShowLogo.ToString());
            }
        }

        public string VoucherPrintProject
        {
            get
            {
                return GetReportSettingInfo(AcmeReportSetting.VoucherPrintProject.ToString());
            }
        }

        public string VoucherPrintIncludeSigns
        {
            get
            {
                return GetReportSettingInfo(AcmeReportSetting.VoucherPrintIncludeSigns.ToString());
            }
        }

        public string VoucherPrintReportTitleType
        {
            get
            {
                return GetReportSettingInfo(AcmeReportSetting.VoucherPrintReportTitleType.ToString());
            }
        }

        public string VoucherPrintReportTitleAddress
        {
            get
            {
                return GetReportSettingInfo(AcmeReportSetting.VoucherPrintReportTitleAddress.ToString());
            }
        }

        public string VoucherPrintLegalEntityFields
        {
            get
            {
                string voucherprintlegalentirydetials = GetReportSettingInfo(AcmeReportSetting.VoucherPrintLegalEntityDetails.ToString());
                return voucherprintlegalentirydetials;
            }
        }

        public Int32 VoucherPrintShowCostCentre
        {
            get
            {
                Int32 showcc = NumberSet.ToInteger(GetReportSettingInfo(AcmeReportSetting.VoucherPrintShowCostCentre.ToString()));
                return showcc;
            }
        }

        public Int32 VoucherPrintHideVoucherReceiptNo
        {
            get
            {
                Int32 hidevoucherreceiptno = NumberSet.ToInteger(GetReportSettingInfo(AcmeReportSetting.VoucherPrintHideVoucherReceiptNo.ToString()));
                return hidevoucherreceiptno;
            }
        }

        public string VoucherPrintLegalEntityFieldsDetails
        {
            get
            {
                string rtn = string.Empty;
                string legaldetails = string.Empty;
                string regno = string.Empty;
                string regdate = string.Empty;
                string panno = string.Empty;
                string eightGNo = string.Empty;
                string eightGRegDate = string.Empty;
                string fcrano = string.Empty;
                string fcradate = string.Empty;


                string voucherprintlegalentirydetials = GetReportSettingInfo(AcmeReportSetting.VoucherPrintLegalEntityDetails.ToString());

                //On 26/08/2022, To assign Legal Entiry's Legal Details----------------------------------------
                if (!string.IsNullOrEmpty(voucherprintlegalentirydetials))
                {
                    if (!string.IsNullOrEmpty(voucherprintlegalentirydetials) && !string.IsNullOrEmpty(ReportProperty.Current.LedgalEntityId))
                    {
                        if (dtLedgerEntity != null && dtLedgerEntity.Rows.Count > 0)
                        {
                            string[] voucherprintlegalentiryfields = voucherprintlegalentirydetials.Split(',');
                            DataTable dtLG = dtLedgerEntity.DefaultView.ToTable();
                            dtLG.DefaultView.RowFilter = "CUSTOMERID IN (" + ReportProperty.Current.LedgalEntityId + ")";
                            if (dtLG.DefaultView.Count > 0)
                            {
                                if (Array.IndexOf(voucherprintlegalentiryfields, "REGNO") >= 0)
                                    regno = dtLG.DefaultView.ToTable().Rows[0]["REGNO"].ToString().Trim();
                                if (Array.IndexOf(voucherprintlegalentiryfields, "REGDATE") >= 0)
                                    regdate = dtLG.DefaultView.ToTable().Rows[0]["REGDATE"].ToString().Trim();
                                if (Array.IndexOf(voucherprintlegalentiryfields, "PANNO") >= 0)
                                    panno = dtLG.DefaultView.ToTable().Rows[0]["PANNO"].ToString().Trim();
                                if (Array.IndexOf(voucherprintlegalentiryfields, "EIGHTYGNO") >= 0)
                                    eightGNo = dtLG.DefaultView.ToTable().Rows[0]["EIGHTYGNO"].ToString().Trim();

                                if (Array.IndexOf(voucherprintlegalentiryfields, "EIGHTY_GNO_REG_DATE") >= 0)
                                {
                                    eightGRegDate = dtLG.DefaultView.ToTable().Rows[0]["EIGHTY_GNO_REG_DATE"].ToString().Trim();
                                    if (!string.IsNullOrEmpty(eightGRegDate)) eightGRegDate = DateSet.ToDate(eightGRegDate);
                                }

                                if (Array.IndexOf(voucherprintlegalentiryfields, "FCRINO") >= 0)
                                    fcrano = dtLG.DefaultView.ToTable().Rows[0]["FCRINO"].ToString().Trim();
                                if (Array.IndexOf(voucherprintlegalentiryfields, "FCRIREGDATE") >= 0)
                                {
                                    fcradate = dtLG.DefaultView.ToTable().Rows[0]["FCRIREGDate"].ToString().Trim();
                                    if (!string.IsNullOrEmpty(fcradate)) fcradate = DateSet.ToDate(fcradate);
                                }

                                legaldetails = string.IsNullOrEmpty(regno) ? string.Empty : ("Reg.No: " + regno + "  ");
                                legaldetails += string.IsNullOrEmpty(regdate) ? string.Empty : ("Reg.Date: " + regdate + "  ");
                                legaldetails += string.IsNullOrEmpty(panno) ? string.Empty : ("PAN No: " + panno + "  ");
                                legaldetails += string.IsNullOrEmpty(eightGNo) ? string.Empty : ("80G No: " + eightGNo + "  ");
                                legaldetails += string.IsNullOrEmpty(eightGRegDate) ? string.Empty : ("80G Reg.Date: " + eightGRegDate + "  ");
                                legaldetails += string.IsNullOrEmpty(fcrano) ? string.Empty : ("FCRA No: " + fcrano + "  ");
                                legaldetails += string.IsNullOrEmpty(fcradate) ? string.Empty : ("FCRA Reg.Date: " + fcradate);
                                rtn = string.IsNullOrEmpty(legaldetails) ? string.Empty : "(" + legaldetails.Trim() + ")";
                                //ReportProperty.Current.LegalDetails = string.IsNullOrEmpty(legaldetails) ? string.Empty : "(" + legaldetails.Trim() + ")";
                            }
                        }
                    }
                }
                //---------------------------------------------------------------------------------------------

                return rtn;
            }
        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------


        public string RoleName1 { get; set; }
        public string Role1 { get; set; }
        public byte[] Sign1Image { get; set; }

        public string RoleName2 { get; set; }
        public string Role2 { get; set; }
        public byte[] Sign2Image { get; set; }

        public string RoleName3 { get; set; }
        public string Role3 { get; set; }
        public byte[] Sign3Image { get; set; }

        public string RoleName4 { get; set; }
        public string Role4 { get; set; }
        public byte[] Sign4Image { get; set; }

        public string RoleName5 { get; set; }
        public string Role5 { get; set; }
        public byte[] Sign5Image { get; set; }

        public bool HideReportSignNoteInFooter { get; set; }

        public string SignNote { get; set; }
        public Int32 SignNoteAlignment { get; set; }
        public Int32 SignNoteLocation { get; set; }

        //public string AuditorSignNote { get; set; }

        public DataTable dtAuditorNoteSign { get; set; }

        public DataTable BudgetNewProjects { get; set; }

        #region Multi Column Report Properties
        public string MultiColumn1BankName { get; set; }
        public string MultiColumn2BankName { get; set; }
        public int MultiColumn1LedgerId { get; set; }
        public int MultiColumn2LedgerId { get; set; }
        #endregion

        #region TDS Property
        public string DeducteeTypeId { get; set; }
        public string TDSVoucherId { get; set; }
        #endregion

        public bool isShowProjectTitle { get; set; } // To show project title on the header (set true, not to show (set False))

        private void LoadReportSetting()
        {
            try
            {
                //To set the current directory to avoid taking reportsetting.xml from recent path
                Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                DataTable dtReportSettingInfo = new DataTable();
                dtReportSettingInfo = dtReportSettingSchema.Copy();
                dtReportSettingInfo.ReadXml(reportSettingFile);
                dvReportSettingInfo = dtReportSettingInfo.DefaultView;
                dvReportSettingInfo.Sort = dtReportSettingSchema.ReportGroupOrderColumn.ColumnName + "," + dtReportSettingSchema.ReportOrderColumn.ColumnName;
                DataTable dt = dvReportSettingInfo.ToTable();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }

        private void SetReportSettingInfo()
        {
            if (dvReportSettingInfo != null)
            {
                dvReportSettingInfo.RowFilter = dtReportSettingSchema.ReportIdColumn.ColumnName + " = '" + reportId + "'";

                if (dvReportSettingInfo.Count > 0)
                {
                    DataRowView drvReportSettingInfo = dvReportSettingInfo[0];
                    ReportGroup = drvReportSettingInfo[dtReportSettingSchema.ReportGroupColumn.ColumnName].ToString();
                    ReportName = drvReportSettingInfo[dtReportSettingSchema.ReportNameColumn.ColumnName].ToString();
                    ReportTitle = drvReportSettingInfo[dtReportSettingSchema.ReportTitleColumn.ColumnName].ToString();
                    ReportDescription = drvReportSettingInfo[dtReportSettingSchema.ReportDescriptionColumn.ColumnName].ToString();
                    ReportAssembly = drvReportSettingInfo[dtReportSettingSchema.ReportAssemblyColumn.ColumnName].ToString();

                    //DateFrom = drvReportSettingInfo[dtReportSettingSchema.DateFromColumn.ColumnName].ToString();
                    //DateTo = drvReportSettingInfo[dtReportSettingSchema.DateToColumn.ColumnName].ToString();
                    //DateAsOn = drvReportSettingInfo[dtReportSettingSchema.DateAsOnColumn.ColumnName].ToString();
                    //IncludeAllLedger = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeAllLedgerColumn.ColumnName].ToString());
                    LedgerNature = drvReportSettingInfo[dtReportSettingSchema.LedgerNatureColumn.ColumnName].ToString();
                    IncludeAllPurposes = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeAllPurposeColumn.ColumnName].ToString());
                    FDRegisterStatus = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.FDRegisterStatusColumn.ColumnName].ToString());
                    ShowByLedger = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowByLedgerColumn.ColumnName].ToString());
                    ShowByLedgerGroup = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowByLedgerGroupColumn.ColumnName].ToString());
                    ShowByCostCentre = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowByCostCentreColumn.ColumnName].ToString());
                    ShowByCostCentreCategory = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowByCostcentreCategoryColumn.ColumnName].ToString());
                    ShowByLedgerSummary = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowByLedgerSummaryColumn.ColumnName].ToString());
                    BreakByCostCentre = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.BrakByCostCentreColumn.ColumnName].ToString());
                    BreakByLedger = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.BreakByLedgerColumn.ColumnName].ToString());
                    ShowDailyBalance = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowDailyBalanceColumn.ColumnName].ToString());
                    IncludeJournal = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeJournalColumn.ColumnName].ToString());
                    IncludeInKind = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeInKindColumn.ColumnName].ToString());
                    IncludeBankDetails = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeBankDetailsColumn.ColumnName].ToString());
                    ShowDetailedBalance = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowBankDetailsColumn.ColumnName].ToString());
                    IncludeDetailed = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeDetailedColumn.ColumnName].ToString());
                    IncludeLedgerGroupTotal = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeLedgerGroupTotalColumn.ColumnName].ToString());
                    IncludeLedgerGroup = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeLedgerGroupColumn.ColumnName].ToString());
                    IncludeCostCentre = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeCostCentreColumn.ColumnName].ToString());
                    ShowMonthTotal = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowMonthTotalColumn.ColumnName].ToString());
                    ShowDonorAddress = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowDonorAddressColumn.ColumnName].ToString());
                    ShowDonorCategory = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowDonorCategoryColumn.ColumnName].ToString());
                    IncludeBankAccount = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeBankAccountColumn.ColumnName].ToString());
                    FDAccountID = drvReportSettingInfo[dtReportSettingSchema.FDAccountIdColumn.ColumnName].ToString();
                    ShowLedgerOpBalance = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowLedgerOpBalanceColumn.ColumnName].ToString());
                    // new trail balance options
                    ShowOpeningBalance = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowOpeningBalanceColumn.ColumnName].ToString());
                    ShowCurrentTransaction = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowCurrentTransactionColumn.ColumnName].ToString());
                    ShowClosingBalance = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowClosingBalanceColumn.ColumnName].ToString());

                    //BankAccount = drvReportSettingInfo[dtReportSettingSchema.BankAccountColumn.ColumnName].ToString();
                    UnSelectedBankAccountId = drvReportSettingInfo[dtReportSettingSchema.UnSelectedAccountIdColumn.ColumnName].ToString();
                    if (string.IsNullOrEmpty(Project)) { drvReportSettingInfo[dtReportSettingSchema.ProjectColumn.ColumnName].ToString(); }
                    if (SocietyId == 0) { this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.SocietyColumn.ColumnName].ToString()); };
                    if (string.IsNullOrEmpty(Project)) { Project = "0"; }
                    // Payroll
                    if (string.IsNullOrEmpty(PayrollId)) { PayrollId = "0"; }
                    if (string.IsNullOrEmpty(PayrollStaffId)) { PayrollStaffId = "0"; }
                    if (string.IsNullOrEmpty(PayrollGroupId)) { PayrollGroupId = "0"; }
                    if (string.IsNullOrEmpty(PayrollComponentId)) { PayrollComponentId = "0"; }
                    if (string.IsNullOrEmpty(PayrollComponentId1)) { PayrollComponentId1 = "0"; }
                    if (string.IsNullOrEmpty(PayrollComponentId2)) { PayrollComponentId2 = "0"; }

                    if (string.IsNullOrEmpty(PayrollComponentName)) { PayrollComponentName = "0"; }
                    if (string.IsNullOrEmpty(PayrollComponentName1)) { PayrollComponentName1 = "0"; }
                    if (string.IsNullOrEmpty(PayrollComponentName2)) { PayrollComponentName2 = "0"; }
                    if (string.IsNullOrEmpty(PayrollDepartmentId)) { PayrollDepartmentId = "0"; }

                    //ProjectTitle = drvReportSettingInfo[dtReportSettingSchema.ProjectTitleColumn.ColumnName].ToString();
                    //BankAccount = drvReportSettingInfo[dtReportSettingSchema.BankAccountColumn.ColumnName].ToString();
                    Ledger = drvReportSettingInfo[dtReportSettingSchema.LedgerColumn.ColumnName].ToString();
                    SelectedLedgerName = drvReportSettingInfo[dtReportSettingSchema.SelectedLedgerNameColumn.ColumnName].ToString();

                    if (Ledger == "") { Ledger = "0"; }
                    LedgerGroup = drvReportSettingInfo[dtReportSettingSchema.LedgerGroupColumn.ColumnName].ToString();
                    if (LedgerGroup == "") { LedgerGroup = "0"; }
                    UnSelectedLedgerId = drvReportSettingInfo[dtReportSettingSchema.UnSelectedLedgerIdColumn.ColumnName].ToString();
                    if (UnSelectedLedgerId == "") { UnSelectedLedgerId = "0"; }
                    CostCentre = drvReportSettingInfo[dtReportSettingSchema.CostCentreColumn.ColumnName].ToString();
                    if (CostCentre == "") { CostCentre = "0"; }
                    CCCategoryId = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.CCCategoryIdColumn.ColumnName].ToString());
                    Narration = drvReportSettingInfo[dtReportSettingSchema.NarrationColumn.ColumnName].ToString();

                    TitleAlignment = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.TitleAlignmentColumn.ColumnName].ToString());
                    ReportDate = drvReportSettingInfo[dtReportSettingSchema.ReportDateColumn.ColumnName].ToString();
                    ShowLedgerCode = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowByLedgerCodeColumn.ColumnName].ToString());
                    ShowGroupCode = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowByGroupCodeColumn.ColumnName].ToString());
                    SortByLedger = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.SortByLedgerColumn.ColumnName].ToString());
                    SortByGroup = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.SortByGroupColumn.ColumnName].ToString());
                    IncludeNarration = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeNarrationColumn.ColumnName].ToString());

                    IncludeNarrationwithRefNo = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeNarrationwithRefNoColumn.ColumnName].ToString());
                    IncludeNarrationwithNameAddress = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeNarrationwithNameAddressColumn.ColumnName].ToString());

                    IncludePanwithGSTNo = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludePanGSTColumn.ColumnName].ToString());

                    NatureofPaymets = drvReportSettingInfo[dtReportSettingSchema.NatureofPaymentsColumn.ColumnName].ToString();
                    DeducteeTypeId = drvReportSettingInfo[dtReportSettingSchema.DeducteeTypeColumn.ColumnName].ToString();
                    // Payroll
                    PayrollId = drvReportSettingInfo[dtReportSettingSchema.PayrollIdColumn.ColumnName].ToString();
                    PayrollStaffId = drvReportSettingInfo[dtReportSettingSchema.PayrollStaffIdColumn.ColumnName].ToString();
                    PayrollGroupId = drvReportSettingInfo[dtReportSettingSchema.PayrollGroupIdColumn.ColumnName].ToString();
                    PayrollComponentId = drvReportSettingInfo[dtReportSettingSchema.PayrollComponentIdColumn.ColumnName].ToString();
                    PayrollComponentId1 = drvReportSettingInfo[dtReportSettingSchema.PayrollComponentId1Column.ColumnName].ToString();
                    PayrollComponentId2 = drvReportSettingInfo[dtReportSettingSchema.PayrollComponentId2Column.ColumnName].ToString();
                    PayrollComponentName = drvReportSettingInfo[dtReportSettingSchema.PayrollComponentNameColumn.ColumnName].ToString();
                    PayrollComponentName1 = drvReportSettingInfo[dtReportSettingSchema.PayrollComponentName1Column.ColumnName].ToString();
                    PayrollComponentName2 = drvReportSettingInfo[dtReportSettingSchema.PayrollComponentName2Column.ColumnName].ToString();
                    PayrollDepartmentId = drvReportSettingInfo[dtReportSettingSchema.PayrollDepartmentIdColumn.ColumnName].ToString();

                    PayrollName = drvReportSettingInfo[dtReportSettingSchema.PayrollNameColumn.ColumnName].ToString();
                    PayrollProjectId = drvReportSettingInfo[dtReportSettingSchema.PayrollProjectIdColumn.ColumnName].ToString();
                    PayrollProjectAddress = drvReportSettingInfo[dtReportSettingSchema.PayrollProjectAddressColumn.ColumnName].ToString();
                    PayrollTitleType = NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.PayrollTitleTypeColumn.ColumnName].ToString());
                    PayrollProjectTitle = drvReportSettingInfo[dtReportSettingSchema.PayrollProjectTitleColumn.ColumnName].ToString();
                    PayrollSignOfEmployer = drvReportSettingInfo[dtReportSettingSchema.PayrollSignOfEmployeeColumn.ColumnName].ToString();
                    if (drvReportSettingInfo.DataView.Table.Columns.Contains(dtReportSettingSchema.PayrollAuthorisedSignatory2Column.ColumnName))
                    {
                        PayrollAuthorisedSignatory2 = drvReportSettingInfo[dtReportSettingSchema.PayrollAuthorisedSignatory2Column.ColumnName].ToString();
                    }
                    PayrollPaymentBankAccountLedgerId = drvReportSettingInfo[dtReportSettingSchema.PayrollPaymentBankAccountLedgerIdColumn.ColumnName].ToString();
                    PayrollPaymentBankAccountNo = drvReportSettingInfo[dtReportSettingSchema.PayrollPaymentBankAccountNoColumn.ColumnName].ToString();
                    PayrollPaymentBankAddress = drvReportSettingInfo[dtReportSettingSchema.PayrollPaymentBankAddressColumn.ColumnName].ToString();
                    PayrollShowComponentDescription = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.PayrollShowComponentDescriptionColumn.ColumnName].ToString());
                    PayrollPayslipInSeparatePages = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.PayrollPayslipSeparatePagesColumn.ColumnName].ToString());

                    ShowAllCountry = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowAllCountryColumn.ColumnName].ToString());
                    ShowHorizontalLine = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.HorizontalLineColumn.ColumnName].ToString());
                    ShowVerticalLine = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.VerticalLineColumn.ColumnName].ToString());
                    SupressZeroValues = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.SupressZeroValuesColumn.ColumnName].ToString());
                    ShowIndividualProjects = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowIndividualProjectColumn.ColumnName].ToString());
                    ShowTitles = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowTitlesColumn.ColumnName].ToString());
                    ShowLogo = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowLogoColumn.ColumnName].ToString());
                    ShowPageNumber = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowPageNumberColumn.ColumnName].ToString());
                    ShowPrintDate = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowPrintDateColumn.ColumnName].ToString());
                    ShowReportDate = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowReportDateColumn.ColumnName].ToString());
                    SetWithForCode = drvReportSettingInfo[dtReportSettingSchema.SetWidthForCodeColumn.ColumnName].ToString();
                    //Don't Update and GET DrillDownProperties, it is dynamic properties for all the reports, 
                    //it will be used when user cliks drill-down in all the reports 
                    //DrillDownProperties= this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.DrillDownPropertiesColumn.ColumnName].ToString());
                    ReportCriteria = drvReportSettingInfo[dtReportSettingSchema.ReportCriteriaColumn.ColumnName].ToString();
                    ShowProjectsinFooter = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowProjectsinFooterColumn.ColumnName].ToString());
                    ReportBorderStyle = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ReportBorderStyleColumn.ColumnName].ToString());
                    ShowByDonorGroup = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowByDonorGroupColumn.ColumnName].ToString());

                    MultiColumn1BankName = drvReportSettingInfo[dtReportSettingSchema.MultiBankColumn1Column.ColumnName].ToString();
                    MultiColumn2BankName = drvReportSettingInfo[dtReportSettingSchema.MultiBankColumn2Column.ColumnName].ToString();
                    MultiColumn1LedgerId = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.MultiBankColumn1LedgerIdColumn.ColumnName].ToString());
                    MultiColumn2LedgerId = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.MultiBankColumn2LedgerIdColumn.ColumnName].ToString());

                    ShowByDonorGroup = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.MultiBankColumn1LedgerIdColumn.ColumnName].ToString());
                    ShowByDonorGroup = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.MultiBankColumn2LedgerIdColumn.ColumnName].ToString());

                    //Prospect Donor Reports
                    RegistrationTypeId = drvReportSettingInfo[dtReportSettingSchema.RegistrationTypeIdColumn.ColumnName].ToString();
                    CountryId = drvReportSettingInfo[dtReportSettingSchema.CountryIdColumn.ColumnName].ToString();
                    StateId = drvReportSettingInfo[dtReportSettingSchema.StateIdColumn.ColumnName].ToString();
                    Language = drvReportSettingInfo[dtReportSettingSchema.LanguageColumn.ColumnName].ToString();
                    IncludeMale = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeMaleColumn.ColumnName].ToString());
                    IncludeFemale = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeFemaleColumn.ColumnName].ToString());
                    IncludeInstitutional = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeInstitutionalColumn.ColumnName].ToString());
                    IncludeIndividual = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeIndividualColumn.ColumnName].ToString());
                    StateDonaud = drvReportSettingInfo[dtReportSettingSchema.StateDonaudColumn.ColumnName].ToString();
                    DonarName = drvReportSettingInfo[dtReportSettingSchema.DonaudIdColumn.ColumnName].ToString();
                    TaskID = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.FeestDayTaskColumn.ColumnName].ToString());
                    IncludeSent = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeSentColumn.ColumnName].ToString());
                    IncludeNotSent = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeNotSentColumn.ColumnName].ToString());
                    AnniversaryType = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.AnniversaryTypeColumn.ColumnName].ToString());

                    SelectedTaskName = drvReportSettingInfo[dtReportSettingSchema.TaskColumn.ColumnName].ToString();
                    IncludeSignDetails = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeSignDetailsColumn.ColumnName].ToString());
                    IncludeAuditorSignNote = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeAuditorSignNoteColumn.ColumnName].ToString());

                    //On 25/01/2021, to retain View Chart Type in Report Settings
                    ChartViewType = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ChartViewTypeColumn.ColumnName].ToString());
                    ChartInPercentage = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ChartInPercentageColumn.ColumnName].ToString());

                    //on 22/02/2021, show all against ledgers and show cc details 
                    ShowAllAgainstLedgers = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowAllAgainstLedgersColumn.ColumnName].ToString());
                    ShowCCDetails = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowCCDetailsColumn.ColumnName].ToString());
                    ShowDonorDetails = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowDonorDetailsColumn.ColumnName].ToString());

                    //On 28/06/2018, to retain show by Insution Header/Socity header ---------------------------------------------
                    HeaderInstituteSocietyName = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowHeaderInstituteSocietyNameColumn.ColumnName].ToString());
                    HeaderInstituteSocietyAddress = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowHeaderInstituteSocietyAddressColumn.ColumnName].ToString());
                    //-------------------------------------------------------------------------------------------------------------


                    //On 16/09/2021, To save created, modified and audtion action properties-------------------
                    CreatedByName = drvReportSettingInfo[dtReportSettingSchema.CreatedByNameColumn.ColumnName].ToString();
                    ModifiedByName = drvReportSettingInfo[dtReportSettingSchema.ModifiedByNameColumn.ColumnName].ToString();
                    AuditAction = drvReportSettingInfo[dtReportSettingSchema.AuditActionColumn.ColumnName].ToString();

                    ReportCodeType = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ReportCodeTypeColumn.ColumnName].ToString());

                    ShowNarrationMonthwiseCumulativeTotal = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowMonthwiseCumulativeTotalColumn.ColumnName].ToString());
                    ShowNarrationMonthwiseCumulativeOpBalance = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowMonthwiseCumulativeOPBalanceColumn.ColumnName].ToString());
                    ShowTableofContent = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowTableofContentColumn.ColumnName].ToString());
                    IncludeAllBudgetLedgers = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeAllBudgetedLedgersColumn.ColumnName].ToString());

                    //-----------------------------------------------------------------------------------------

                    //On 03/02/2023, Retain when we change page and margin changes and Paper setting---------
                    PaperKind = GetPaperKind(drvReportSettingInfo[dtReportSettingSchema.PaperKindColumn.ColumnName].ToString());
                    PaperLandscape = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.PaperLandscapeColumn.ColumnName].ToString());

                    MarginLeft = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.MarginLeftColumn.ColumnName].ToString());
                    MarginRight = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.MarginRightColumn.ColumnName].ToString());
                    MarginTop = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.MarginTopColumn.ColumnName].ToString());
                    MarginBottom = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.MarginBottomColumn.ColumnName].ToString());
                    //---------------------------------------------------------------------------------------

                    LicenseBased = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.LicenseBasedColumn.ColumnName].ToString());

                    HideContraNote = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.HideContraNoteColumn.ColumnName].ToString());
                    HideLedgerName = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.HideLedgerNameColumn.ColumnName].ToString());
                    this.FDInvestmentType = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.FDInvestmentTypeColumn.ColumnName].ToString());
                    FDInvestmentTypeName = drvReportSettingInfo[dtReportSettingSchema.FDInvestmentTypeNameColumn.ColumnName].ToString();
                    IncludeFDSimpleInterest = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeFDSimpleInterestColumn.ColumnName].ToString());
                    IncludeFDAccumulatedInterest = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeFDAccumulatedInterestColumn.ColumnName].ToString());
                    FDScheme = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.FDSchemeColumn.ColumnName].ToString());

                    CurrencyCountryId = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.CurrencyCountryIdColumn.ColumnName].ToString());
                    CurrencyCountry = drvReportSettingInfo[dtReportSettingSchema.CurrencyCountryColumn.ColumnName].ToString();
                    CurrencyCountrySymbol = drvReportSettingInfo[dtReportSettingSchema.CurrencyCountrySymbolColumn.ColumnName].ToString();

                    AvgEuroExchangeRate = this.NumberSet.ToDouble(drvReportSettingInfo[dtReportSettingSchema.AverageEuroExchangeColumn.ColumnName].ToString());
                    AvgEuroDollarExchangeRate = this.NumberSet.ToDouble(drvReportSettingInfo[dtReportSettingSchema.AverageEuroDollorExchangeColumn.ColumnName].ToString());
                    ShowForexDetail = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowForexDetailColumn.ColumnName].ToString());
                    RandPSortOrder = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ReceiptAndPaymentSortOrderColumn.ColumnName].ToString());
                    ShowIndividualLedger = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowIndividualLedgerColumn.ColumnName].ToString());
                }

                dvReportSettingInfo.RowFilter = "";
            }
        }

        public System.Drawing.Printing.PaperKind GetPaperKind(string PaperName)
        {
            System.Drawing.Printing.PaperKind rnt = System.Drawing.Printing.PaperKind.A4;
            try
            {

                if (!string.IsNullOrEmpty(PaperName))
                {
                    PrinterSettings settings = new PrinterSettings();
                    foreach (PaperSize size in settings.PaperSizes)
                    {
                        if (size.Kind.ToString().ToUpper() == PaperName.ToUpper())
                        {
                            rnt = size.Kind;
                            break;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
                rnt = System.Drawing.Printing.PaperKind.A4;
            }

            return rnt;
        }


        public ResultArgs SaveReportSetting()
        {
            ResultArgs result = new ResultArgs();
            result.Success = true;

            if (dvReportSettingInfo != null && dvReportSettingInfo.Count > 0)
            {
                DataTable dtReportSettingInfo = dvReportSettingInfo.Table;
                dvReportSettingInfo.RowFilter = dtReportSettingSchema.ReportIdColumn.ColumnName + " = '" + reportId + "'";

                if (dvReportSettingInfo.Count > 0)
                {
                    DataRow drReportSettingInfo = dvReportSettingInfo[0].Row;
                    //drReportSettingInfo[dtReportSettingSchema.DateFromColumn.ColumnName] = DateFrom;
                    //drReportSettingInfo[dtReportSettingSchema.DateToColumn.ColumnName] = DateTo;
                    //drReportSettingInfo[dtReportSettingSchema.DateAsOnColumn.ColumnName] = DateAsOn;

                    //On 10/04/2019, to retain show by Insution Header/Socity header ---------------------------------------------
                    drReportSettingInfo[dtReportSettingSchema.ShowHeaderInstituteSocietyNameColumn.ColumnName] = HeaderInstituteSocietyName;
                    drReportSettingInfo[dtReportSettingSchema.ShowHeaderInstituteSocietyAddressColumn.ColumnName] = HeaderInstituteSocietyAddress;
                    //-------------------------------------------------------------------------------------------------------------

                    //drReportSettingInfo[dtReportSettingSchema.IncludeAllLedgerColumn.ColumnName] = IncludeAllLedger;
                    drReportSettingInfo[dtReportSettingSchema.LedgerNatureColumn.ColumnName] = LedgerNature;
                    drReportSettingInfo[dtReportSettingSchema.IncludeAllPurposeColumn.ColumnName] = IncludeAllPurposes;
                    drReportSettingInfo[dtReportSettingSchema.FDRegisterStatusColumn.ColumnName] = FDRegisterStatus;
                    drReportSettingInfo[dtReportSettingSchema.ShowByLedgerColumn.ColumnName] = ShowByLedger;
                    drReportSettingInfo[dtReportSettingSchema.ShowByLedgerGroupColumn.ColumnName] = ShowByLedgerGroup;
                    drReportSettingInfo[dtReportSettingSchema.ShowByCostCentreColumn.ColumnName] = ShowByCostCentre;
                    drReportSettingInfo[dtReportSettingSchema.ShowByCostcentreCategoryColumn.ColumnName] = ShowByCostCentreCategory;
                    drReportSettingInfo[dtReportSettingSchema.ShowByLedgerSummaryColumn.ColumnName] = ShowByLedgerSummary;
                    drReportSettingInfo[dtReportSettingSchema.BrakByCostCentreColumn.ColumnName] = BreakByCostCentre;
                    drReportSettingInfo[dtReportSettingSchema.BreakByLedgerColumn.ColumnName] = BreakByLedger;
                    drReportSettingInfo[dtReportSettingSchema.ShowDailyBalanceColumn.ColumnName] = ShowDailyBalance;
                    drReportSettingInfo[dtReportSettingSchema.IncludeJournalColumn.ColumnName] = IncludeJournal;
                    drReportSettingInfo[dtReportSettingSchema.IncludeInKindColumn.ColumnName] = IncludeInKind;
                    drReportSettingInfo[dtReportSettingSchema.IncludeLedgerGroupTotalColumn.ColumnName] = IncludeLedgerGroupTotal;
                    drReportSettingInfo[dtReportSettingSchema.IncludeLedgerGroupColumn.ColumnName] = IncludeLedgerGroup;
                    drReportSettingInfo[dtReportSettingSchema.IncludeCostCentreColumn.ColumnName] = IncludeCostCentre;
                    drReportSettingInfo[dtReportSettingSchema.ShowBankDetailsColumn.ColumnName] = ShowDetailedBalance;
                    drReportSettingInfo[dtReportSettingSchema.IncludeDetailedColumn.ColumnName] = IncludeDetailed;
                    drReportSettingInfo[dtReportSettingSchema.ShowMonthTotalColumn.ColumnName] = ShowMonthTotal;
                    drReportSettingInfo[dtReportSettingSchema.ShowDonorAddressColumn.ColumnName] = ShowDonorAddress;
                    drReportSettingInfo[dtReportSettingSchema.ShowDonorCategoryColumn.ColumnName] = ShowDonorCategory;
                    drReportSettingInfo[dtReportSettingSchema.IncludeBankAccountColumn.ColumnName] = IncludeBankAccount;
                    drReportSettingInfo[dtReportSettingSchema.IncludeBankDetailsColumn.ColumnName] = IncludeBankDetails;
                    drReportSettingInfo[dtReportSettingSchema.ProjectColumn.ColumnName] = Project;
                    drReportSettingInfo[dtReportSettingSchema.SocietyColumn.ColumnName] = SocietyId;
                    drReportSettingInfo[dtReportSettingSchema.FDAccountIdColumn.ColumnName] = FDAccountID;
                    drReportSettingInfo[dtReportSettingSchema.ShowLedgerOpBalanceColumn.ColumnName] = ShowLedgerOpBalance;

                    //drReportSettingInfo[dtReportSettingSchema.ProjectTitleColumn.ColumnName] = ProjectTitle;
                    //drReportSettingInfo[dtReportSettingSchema.BankAccountColumn.ColumnName] = BankAccount;
                    drReportSettingInfo[dtReportSettingSchema.UnSelectedAccountIdColumn.ColumnName] = UnSelectedBankAccountId;
                    drReportSettingInfo[dtReportSettingSchema.LedgerColumn.ColumnName] = Ledger;
                    drReportSettingInfo[dtReportSettingSchema.LedgerGroupColumn.ColumnName] = LedgerGroup;
                    drReportSettingInfo[dtReportSettingSchema.SelectedLedgerNameColumn.ColumnName] = SelectedLedgerName;
                    drReportSettingInfo[dtReportSettingSchema.UnSelectedLedgerIdColumn.ColumnName] = UnSelectedLedgerId;
                    drReportSettingInfo[dtReportSettingSchema.CostCentreColumn.ColumnName] = CostCentre;
                    drReportSettingInfo[dtReportSettingSchema.CCCategoryIdColumn.ColumnName] = CCCategoryId;
                    drReportSettingInfo[dtReportSettingSchema.BUDGET_IDColumn.ColumnName] = Budget;
                    drReportSettingInfo[dtReportSettingSchema.NarrationColumn.ColumnName] = Narration;
                    //Don't Update and GET DrillDownProperties, it is dynamic properties for all the reports, 
                    //it will be used when user cliks drill-down in all the reports 
                    //drReportSettingInfo[dtReportSettingSchema.DrillDownPropertiesColumn.ColumnName] = DrillDownProperties;
                    drReportSettingInfo[dtReportSettingSchema.VoucherTypeColumn.ColumnName] = VoucherType;
                    drReportSettingInfo[dtReportSettingSchema.TitleAlignmentColumn.ColumnName] = TitleAlignment;
                    drReportSettingInfo[dtReportSettingSchema.ReportDateColumn.ColumnName] = ReportDate;
                    drReportSettingInfo[dtReportSettingSchema.ShowByLedgerCodeColumn.ColumnName] = ShowLedgerCode;
                    drReportSettingInfo[dtReportSettingSchema.ShowByGroupCodeColumn.ColumnName] = ShowGroupCode;
                    drReportSettingInfo[dtReportSettingSchema.SortByLedgerColumn.ColumnName] = SortByLedger;
                    drReportSettingInfo[dtReportSettingSchema.SortByGroupColumn.ColumnName] = SortByGroup;
                    drReportSettingInfo[dtReportSettingSchema.IncludeNarrationColumn.ColumnName] = IncludeNarration;

                    drReportSettingInfo[dtReportSettingSchema.IncludeNarrationwithRefNoColumn.ColumnName] = IncludeNarrationwithRefNo;
                    drReportSettingInfo[dtReportSettingSchema.IncludeNarrationwithNameAddressColumn.ColumnName] = IncludeNarrationwithNameAddress;

                    drReportSettingInfo[dtReportSettingSchema.IncludePanGSTColumn.ColumnName] = IncludePanwithGSTNo;

                    drReportSettingInfo[dtReportSettingSchema.NatureofPaymentsColumn.ColumnName] = NatureofPaymets;
                    drReportSettingInfo[dtReportSettingSchema.DeducteeTypeColumn.ColumnName] = DeducteeTypeId;
                    drReportSettingInfo[dtReportSettingSchema.HorizontalLineColumn.ColumnName] = ShowHorizontalLine;
                    drReportSettingInfo[dtReportSettingSchema.SupressZeroValuesColumn.ColumnName] = SupressZeroValues;
                    drReportSettingInfo[dtReportSettingSchema.ShowIndividualProjectColumn.ColumnName] = ShowIndividualProjects;
                    drReportSettingInfo[dtReportSettingSchema.VerticalLineColumn.ColumnName] = ShowVerticalLine;
                    drReportSettingInfo[dtReportSettingSchema.ShowTitlesColumn.ColumnName] = ShowTitles;
                    drReportSettingInfo[dtReportSettingSchema.ShowLogoColumn.ColumnName] = ShowLogo;
                    drReportSettingInfo[dtReportSettingSchema.ShowPageNumberColumn.ColumnName] = ShowPageNumber;
                    drReportSettingInfo[dtReportSettingSchema.ShowPrintDateColumn.ColumnName] = ShowPrintDate;
                    drReportSettingInfo[dtReportSettingSchema.ShowReportDateColumn.ColumnName] = ShowReportDate;
                    drReportSettingInfo[dtReportSettingSchema.SetWidthForCodeColumn.ColumnName] = SetWithForCode;
                    drReportSettingInfo[dtReportSettingSchema.ShowHeaderInstituteSocietyNameColumn.ColumnName] = HeaderInstituteSocietyName;
                    drReportSettingInfo[dtReportSettingSchema.ShowHeaderInstituteSocietyAddressColumn.ColumnName] = HeaderInstituteSocietyAddress;
                    drReportSettingInfo[dtReportSettingSchema.ShowProjectsinFooterColumn.ColumnName] = ShowProjectsinFooter;
                    drReportSettingInfo[dtReportSettingSchema.ReportBorderStyleColumn.ColumnName] = ReportBorderStyle;
                    drReportSettingInfo[dtReportSettingSchema.PayrollIdColumn.ColumnName] = PayrollId;
                    drReportSettingInfo[dtReportSettingSchema.PayrollStaffIdColumn.ColumnName] = PayrollStaffId;
                    drReportSettingInfo[dtReportSettingSchema.PayrollGroupIdColumn.ColumnName] = PayrollGroupId;
                    drReportSettingInfo[dtReportSettingSchema.PayrollComponentIdColumn.ColumnName] = PayrollComponentId;
                    drReportSettingInfo[dtReportSettingSchema.PayrollComponentId1Column.ColumnName] = PayrollComponentId1;
                    drReportSettingInfo[dtReportSettingSchema.PayrollComponentId2Column.ColumnName] = PayrollComponentId2;

                    drReportSettingInfo[dtReportSettingSchema.PayrollComponentNameColumn.ColumnName] = PayrollComponentName;
                    drReportSettingInfo[dtReportSettingSchema.PayrollComponentName1Column.ColumnName] = PayrollComponentName1;
                    drReportSettingInfo[dtReportSettingSchema.PayrollComponentName2Column.ColumnName] = PayrollComponentName2;
                    drReportSettingInfo[dtReportSettingSchema.PayrollDepartmentIdColumn.ColumnName] = PayrollDepartmentId;

                    drReportSettingInfo[dtReportSettingSchema.PayrollNameColumn.ColumnName] = PayrollName;
                    drReportSettingInfo[dtReportSettingSchema.PayrollProjectIdColumn.ColumnName] = PayrollProjectId;
                    drReportSettingInfo[dtReportSettingSchema.PayrollTitleTypeColumn.ColumnName] = PayrollTitleType;
                    drReportSettingInfo[dtReportSettingSchema.PayrollProjectTitleColumn.ColumnName] = PayrollProjectTitle;
                    drReportSettingInfo[dtReportSettingSchema.PayrollProjectAddressColumn.ColumnName] = PayrollProjectAddress;
                    drReportSettingInfo[dtReportSettingSchema.PayrollPaymentBankAccountLedgerIdColumn.ColumnName] = PayrollPaymentBankAccountLedgerId;
                    drReportSettingInfo[dtReportSettingSchema.PayrollPaymentBankAccountNoColumn.ColumnName] = PayrollPaymentBankAccountNo;
                    drReportSettingInfo[dtReportSettingSchema.PayrollPaymentBankAddressColumn.ColumnName] = PayrollPaymentBankAddress;
                    drReportSettingInfo[dtReportSettingSchema.PayrollSignOfEmployeeColumn.ColumnName] = PayrollSignOfEmployer;
                    drReportSettingInfo[dtReportSettingSchema.PayrollAuthorisedSignatory2Column.ColumnName] = PayrollAuthorisedSignatory2;
                    drReportSettingInfo[dtReportSettingSchema.PayrollShowComponentDescriptionColumn.ColumnName] = PayrollShowComponentDescription;
                    drReportSettingInfo[dtReportSettingSchema.PayrollPayslipSeparatePagesColumn.ColumnName] = PayrollPayslipInSeparatePages;

                    drReportSettingInfo[dtReportSettingSchema.ShowAllCountryColumn.ColumnName] = ShowAllCountry;

                    //trail balance new options
                    drReportSettingInfo[dtReportSettingSchema.ShowOpeningBalanceColumn.ColumnName] = ShowOpeningBalance;
                    drReportSettingInfo[dtReportSettingSchema.ShowCurrentTransactionColumn.ColumnName] = ShowCurrentTransaction;
                    drReportSettingInfo[dtReportSettingSchema.ShowClosingBalanceColumn.ColumnName] = ShowClosingBalance;

                    //Cash Bank Transaction Reports
                    drReportSettingInfo[dtReportSettingSchema.ShowByDonorGroupColumn.ColumnName] = ShowByDonorGroup;

                    //Cash Bank Transaction Reports
                    drReportSettingInfo[dtReportSettingSchema.MultiBankColumn1Column.ColumnName] = MultiColumn1BankName;
                    drReportSettingInfo[dtReportSettingSchema.MultiBankColumn2Column.ColumnName] = MultiColumn2BankName;
                    drReportSettingInfo[dtReportSettingSchema.MultiBankColumn1LedgerIdColumn.ColumnName] = MultiColumn1LedgerId;
                    drReportSettingInfo[dtReportSettingSchema.MultiBankColumn2LedgerIdColumn.ColumnName] = MultiColumn2LedgerId;

                    //Prospect Donor Reports
                    drReportSettingInfo[dtReportSettingSchema.RegistrationTypeIdColumn.ColumnName] = RegistrationTypeId;
                    drReportSettingInfo[dtReportSettingSchema.CountryIdColumn.ColumnName] = CountryId;
                    drReportSettingInfo[dtReportSettingSchema.StateIdColumn.ColumnName] = StateId;
                    drReportSettingInfo[dtReportSettingSchema.LanguageColumn.ColumnName] = Language;
                    drReportSettingInfo[dtReportSettingSchema.IncludeMaleColumn.ColumnName] = IncludeMale;
                    drReportSettingInfo[dtReportSettingSchema.IncludeFemaleColumn.ColumnName] = IncludeFemale;
                    drReportSettingInfo[dtReportSettingSchema.IncludeInstitutionalColumn.ColumnName] = IncludeInstitutional;
                    drReportSettingInfo[dtReportSettingSchema.IncludeIndividualColumn.ColumnName] = IncludeIndividual;
                    drReportSettingInfo[dtReportSettingSchema.StateDonaudColumn.ColumnName] = StateDonaud;
                    drReportSettingInfo[dtReportSettingSchema.DonaudIdColumn.ColumnName] = DonarName;
                    drReportSettingInfo[dtReportSettingSchema.FeestDayTaskColumn.ColumnName] = TaskID;
                    drReportSettingInfo[dtReportSettingSchema.IncludeSentColumn.ColumnName] = IncludeSent;
                    drReportSettingInfo[dtReportSettingSchema.IncludeNotSentColumn.ColumnName] = IncludeNotSent;
                    drReportSettingInfo[dtReportSettingSchema.AnniversaryTypeColumn.ColumnName] = AnniversaryType;

                    drReportSettingInfo[dtReportSettingSchema.TaskColumn.ColumnName] = SelectedTaskName;
                    drReportSettingInfo[dtReportSettingSchema.IncludeSignDetailsColumn.ColumnName] = IncludeSignDetails;
                    drReportSettingInfo[dtReportSettingSchema.IncludeAuditorSignNoteColumn.ColumnName] = IncludeAuditorSignNote;

                    //On 25/01/2021, to retain View Chart Type in Report Settings
                    drReportSettingInfo[dtReportSettingSchema.ChartViewTypeColumn.ColumnName] = ChartViewType;
                    drReportSettingInfo[dtReportSettingSchema.ChartInPercentageColumn.ColumnName] = ChartInPercentage;

                    //on 22/02/2021, show all against ledgers and show cc details 
                    drReportSettingInfo[dtReportSettingSchema.ShowAllAgainstLedgersColumn.ColumnName] = ShowAllAgainstLedgers;
                    drReportSettingInfo[dtReportSettingSchema.ShowCCDetailsColumn.ColumnName] = ShowCCDetails;
                    drReportSettingInfo[dtReportSettingSchema.ShowDonorDetailsColumn.ColumnName] = ShowDonorDetails;

                    //On 16/09/2021, To save created, modified and audtion action properties-------------------
                    drReportSettingInfo[dtReportSettingSchema.CreatedByNameColumn.ColumnName] = CreatedByName;
                    drReportSettingInfo[dtReportSettingSchema.ModifiedByNameColumn.ColumnName] = ModifiedByName;
                    drReportSettingInfo[dtReportSettingSchema.AuditActionColumn.ColumnName] = AuditAction;

                    drReportSettingInfo[dtReportSettingSchema.ReportCodeTypeColumn.ColumnName] = ReportCodeType;
                    //-----------------------------------------------------------------------------------------

                    drReportSettingInfo[dtReportSettingSchema.ShowMonthwiseCumulativeTotalColumn.ColumnName] = ShowNarrationMonthwiseCumulativeTotal;
                    drReportSettingInfo[dtReportSettingSchema.ShowMonthwiseCumulativeOPBalanceColumn.ColumnName] = ShowNarrationMonthwiseCumulativeOpBalance;
                    drReportSettingInfo[dtReportSettingSchema.ShowTableofContentColumn.ColumnName] = ShowTableofContent;
                    drReportSettingInfo[dtReportSettingSchema.IncludeAllBudgetedLedgersColumn.ColumnName] = IncludeAllBudgetLedgers;

                    //On 03/02/2023, Retain when we change page and margin changes and Paper setting---------
                    drReportSettingInfo[dtReportSettingSchema.PaperKindColumn.ColumnName] = PaperKind;
                    drReportSettingInfo[dtReportSettingSchema.PaperLandscapeColumn.ColumnName] = PaperLandscape;
                    drReportSettingInfo[dtReportSettingSchema.MarginLeftColumn.ColumnName] = MarginLeft;
                    drReportSettingInfo[dtReportSettingSchema.MarginRightColumn.ColumnName] = MarginRight;
                    drReportSettingInfo[dtReportSettingSchema.MarginTopColumn.ColumnName] = MarginTop;
                    drReportSettingInfo[dtReportSettingSchema.MarginBottomColumn.ColumnName] = MarginBottom;
                    //---------------------------------------------------------------------------------------

                    drReportSettingInfo[dtReportSettingSchema.LicenseBasedColumn.ColumnName] = LicenseBased;
                    drReportSettingInfo[dtReportSettingSchema.HideContraNoteColumn.ColumnName] = HideContraNote;
                    drReportSettingInfo[dtReportSettingSchema.HideLedgerNameColumn.ColumnName] = HideLedgerName;
                    drReportSettingInfo[dtReportSettingSchema.FDInvestmentTypeColumn.ColumnName] = this.FDInvestmentType;
                    drReportSettingInfo[dtReportSettingSchema.FDInvestmentTypeNameColumn.ColumnName] = FDInvestmentTypeName;
                    drReportSettingInfo[dtReportSettingSchema.IncludeFDSimpleInterestColumn.ColumnName] = IncludeFDSimpleInterest;
                    drReportSettingInfo[dtReportSettingSchema.IncludeFDAccumulatedInterestColumn.ColumnName] = IncludeFDAccumulatedInterest;
                    drReportSettingInfo[dtReportSettingSchema.FDSchemeColumn.ColumnName] = FDScheme;

                    drReportSettingInfo[dtReportSettingSchema.CurrencyCountryIdColumn.ColumnName] = CurrencyCountryId;
                    drReportSettingInfo[dtReportSettingSchema.CurrencyCountryColumn.ColumnName] = CurrencyCountry;
                    drReportSettingInfo[dtReportSettingSchema.CurrencyCountrySymbolColumn.ColumnName] = CurrencyCountrySymbol;

                    drReportSettingInfo[dtReportSettingSchema.AverageEuroExchangeColumn.ColumnName] = AvgEuroExchangeRate;
                    drReportSettingInfo[dtReportSettingSchema.AverageEuroDollorExchangeColumn.ColumnName] = AvgEuroDollarExchangeRate;
                    drReportSettingInfo[dtReportSettingSchema.ShowForexDetailColumn.ColumnName] = ShowForexDetail;

                    drReportSettingInfo[dtReportSettingSchema.ReceiptAndPaymentSortOrderColumn.ColumnName] = RandPSortOrder;
                    drReportSettingInfo[dtReportSettingSchema.ShowIndividualLedgerColumn.ColumnName] = ShowIndividualLedger;

                    drReportSettingInfo.AcceptChanges();
                    Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                    //To Use the xml path at the run time
                    //SHGetFolderPath(0, CSIDL_LOCAL_APPDATA, 0, SHGFP_TYPE_CURRENT, reportSettingFile);
                    dtReportSettingInfo.WriteXml(reportSettingFile, XmlWriteMode.IgnoreSchema);
                }
            }

            LoadReportSetting();
            SetReportSettingInfo();
            //SaveSignDetails();
            SaveReportBudgetNewProject();

            return result;
        }


        /// <summary>
        /// Compressing Bitmap images 
        /// </summary>
        /// <param name="image">UnCompress Bitmap Image</param>
        /// <returns>Compressed Bitmap Image</returns>
        //  Added by Carmel Raj M on July-07-2015
        private Bitmap CompressImage(Bitmap image)
        {
            long ImageSize;
            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
            Encoder myEncoder = Encoder.Quality;

            EncoderParameters encoderParameters = new EncoderParameters(1);
            EncoderParameter encoderParameter = new EncoderParameter(myEncoder, 50L);
            encoderParameters.Param[0] = encoderParameter;
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, jpgEncoder, encoderParameters);
            ImageSize = memoryStream.Length;
            image = (Bitmap)Image.FromStream(memoryStream);
            ImageSize = memoryStream.Length;
            return image;
        }

        private ImageCodecInfo GetEncoder(ImageFormat Format)
        {
            ImageCodecInfo Encoder = null;
            ImageCodecInfo[] Codec = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in Codec)
            {
                if (codec.FormatID == Format.Guid)
                {
                    Encoder = codec;
                }
            }
            return Encoder;
        }

        public bool IsMoreThanOneCashBankLedger(string lids)
        {
            bool rtn = true;
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchCashBankCurrencySymbolByLedger))
                {
                    dataManager.Parameters.Add(ReportParameters.LEDGER_IDColumn, string.IsNullOrEmpty(lids) ? "0" : lids);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    ResultArgs resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);

                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        rtn = (resultArgs.RowsAffected > 1);
                    }
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
            return rtn;
        }

        public string GetCashBankLedgerCurrencySymbol(string lids)
        {
            string rtn = string.Empty;
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchCashBankCurrencySymbolByLedger))
                {
                    dataManager.Parameters.Add(ReportParameters.LEDGER_IDColumn, string.IsNullOrEmpty(lids) ? "0" : lids);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    ResultArgs resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);

                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.RowsAffected > 0)
                    {
                        rtn = resultArgs.DataSource.Table.Rows[0]["CURRENCY_SYMBOL"].ToString();
                    }
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
            return rtn;
        }

        /// <summary>
        /// 12/09/2024, To Enforce to skip Default Ledgers for other than country 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public ResultArgs EnforceSkipDefaultLedgers(ResultArgs result, string fieldname)
        {
            try
            {
                if (SettingProperty.Current.AllowMultiCurrency == 1 || SettingProperty.Current.IsCountryOtherThanIndia)
                {
                    if (result.Success)
                    {
                        if (result.DataSource.Table != null)
                        {
                            if (result.DataSource.GetType() == typeof(DataTable))
                            {

                            }
                            DataTable dt = result.DataSource.Table;
                            dt.DefaultView.RowFilter = fieldname + " NOT IN (" + SettingProperty.Current.GetDefaultGeneralLedgersIds + ")";
                            dt = dt.DefaultView.ToTable();
                            result.DataSource.Data = dt;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                result.Message = err.Message;
            }
            return result;
        }

        /// <summary>
        /// this method is used to assign sign details for curretn financial year
        /// 
        //If More than one project is selected, it will take common signature details other wise
        //only one project is selected, it will get concern project's signature details
        /// </summary>
        public void AssignSignDetails(string selectedprojects)
        {
            ReportProperty.Current.RoleName1 = string.Empty;
            ReportProperty.Current.Role1 = string.Empty;
            ReportProperty.Current.Sign1Image = null;

            ReportProperty.Current.RoleName2 = string.Empty;
            ReportProperty.Current.Role2 = string.Empty;
            ReportProperty.Current.Sign2Image = null;

            ReportProperty.Current.RoleName3 = string.Empty;
            ReportProperty.Current.Role3 = string.Empty;
            ReportProperty.Current.Sign3Image = null;

            ReportProperty.Current.RoleName4 = string.Empty;
            ReportProperty.Current.Role4 = string.Empty;
            ReportProperty.Current.Sign4Image = null;

            ReportProperty.Current.RoleName5 = string.Empty;
            ReportProperty.Current.Role5 = string.Empty;
            ReportProperty.Current.Sign5Image = null;

            //ReportProperty.Current.AuditorSignNote = string.Empty;

            HideReportSignNoteInFooter = false;
            SignNote = string.Empty;
            SignNoteLocation = 0; // By default Above Signature
            SignNoteAlignment = 1; // By default Center Alignment

            ReportSetting.ReportSignDataTable dtReportSignSchema = new ReportSetting.ReportSignDataTable();
            //string ReportSignSQL = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.FetchSignDetails);
            using (DataManager dataManager = new DataManager(SQLCommand.Setting.FetchSignDetails))
            {
                dataManager.Parameters.Add(dtReportSignSchema.ACC_YEAR_IDColumn, SettingProperty.Current.AccPeriodId);
                ResultArgs resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);

                if (resultArgs.Success)
                {
                    DataTable dtSign = resultArgs.DataSource.Table;

                    // On 27/02/2021, ----------------------------------------------------------
                    //If More than one project is selected, it will take common signature details
                    //If only one project is selected, it will get concern project's signature details
                    Int32 projectid = ReportProperty.Current.NumberSet.ToInteger(selectedprojects);
                    dtSign.DefaultView.RowFilter = "PROJECT_ID = " + projectid;
                    dtSign = dtSign.DefaultView.ToTable();

                    //If no sign details are available for concern project, take common sign details  
                    if (projectid > 0 && dtSign.Rows.Count == 0)
                    {
                        dtSign = resultArgs.DataSource.Table;
                        dtSign.DefaultView.RowFilter = string.Empty;
                        dtSign.DefaultView.RowFilter = "PROJECT_ID = 0";
                        dtSign = dtSign.DefaultView.ToTable();
                    }
                    //-------------------------------------------------------------------------

                    if (dtSign.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtSign.Rows)
                        {
                            int signorder = ReportProperty.Current.NumberSet.ToInteger(dr[dtReportSignSchema.SIGN_ORDERColumn.ColumnName].ToString());
                            int hidereportsignnote = ReportProperty.Current.NumberSet.ToInteger(dr[dtReportSignSchema.HIDE_REQUIRE_SIGN_NOTEColumn.ColumnName].ToString());

                            if (hidereportsignnote == 1)
                            {
                                HideReportSignNoteInFooter = true;
                            }

                            if (!string.IsNullOrEmpty(dr[dtReportSignSchema.SIGN_NOTEColumn.ColumnName].ToString()))
                            {
                                SignNote = dr[dtReportSignSchema.SIGN_NOTEColumn.ColumnName].ToString();
                                SignNoteAlignment = ReportProperty.Current.NumberSet.ToInteger(dr[dtReportSignSchema.SIGN_NOTE_ALIGNMENTColumn.ColumnName].ToString());
                                SignNoteLocation = ReportProperty.Current.NumberSet.ToInteger(dr[dtReportSignSchema.SIGN_NOTE_LOCATIONColumn.ColumnName].ToString());
                            }

                            switch (signorder)
                            {
                                case 1:
                                    ReportProperty.Current.RoleName1 = dr[dtReportSignSchema.ROLE_NAMEColumn.ColumnName].ToString().Trim();
                                    ReportProperty.Current.Role1 = dr[dtReportSignSchema.ROLEColumn.ColumnName].ToString().Trim();
                                    ReportProperty.Current.Sign1Image = null;
                                    if (!string.IsNullOrEmpty(dr[dtReportSignSchema.SIGN_IMAGEColumn.ColumnName].ToString()))
                                    {
                                        ReportProperty.Current.Sign1Image = (byte[])dr[dtReportSignSchema.SIGN_IMAGEColumn.ColumnName];
                                    }
                                    break;
                                case 2:
                                    ReportProperty.Current.RoleName2 = dr[dtReportSignSchema.ROLE_NAMEColumn.ColumnName].ToString().Trim();
                                    ReportProperty.Current.Role2 = dr[dtReportSignSchema.ROLEColumn.ColumnName].ToString().Trim();
                                    ReportProperty.Current.Sign2Image = null;
                                    if (!string.IsNullOrEmpty(dr[dtReportSignSchema.SIGN_IMAGEColumn.ColumnName].ToString()))
                                    {
                                        ReportProperty.Current.Sign2Image = (byte[])dr[dtReportSignSchema.SIGN_IMAGEColumn.ColumnName];
                                    }
                                    break;
                                case 3:
                                    ReportProperty.Current.RoleName3 = dr[dtReportSignSchema.ROLE_NAMEColumn.ColumnName].ToString().Trim();
                                    ReportProperty.Current.Role3 = dr[dtReportSignSchema.ROLEColumn.ColumnName].ToString().Trim();
                                    ReportProperty.Current.Sign3Image = null;
                                    if (!string.IsNullOrEmpty(dr[dtReportSignSchema.SIGN_IMAGEColumn.ColumnName].ToString()))
                                    {
                                        ReportProperty.Current.Sign3Image = (byte[])dr[dtReportSignSchema.SIGN_IMAGEColumn.ColumnName];
                                    }
                                    break;
                                case 4:
                                    ReportProperty.Current.RoleName4 = dr[dtReportSignSchema.ROLE_NAMEColumn.ColumnName].ToString().Trim();
                                    ReportProperty.Current.Role4 = dr[dtReportSignSchema.ROLEColumn.ColumnName].ToString().Trim();
                                    ReportProperty.Current.Sign4Image = null;
                                    if (!string.IsNullOrEmpty(dr[dtReportSignSchema.SIGN_IMAGEColumn.ColumnName].ToString()))
                                    {
                                        ReportProperty.Current.Sign4Image = (byte[])dr[dtReportSignSchema.SIGN_IMAGEColumn.ColumnName];
                                    }
                                    break;
                                case 5:
                                    ReportProperty.Current.RoleName5 = dr[dtReportSignSchema.ROLE_NAMEColumn.ColumnName].ToString().Trim();
                                    ReportProperty.Current.Role5 = dr[dtReportSignSchema.ROLEColumn.ColumnName].ToString().Trim();
                                    ReportProperty.Current.Sign5Image = null;
                                    if (!string.IsNullOrEmpty(dr[dtReportSignSchema.SIGN_IMAGEColumn.ColumnName].ToString()))
                                    {
                                        ReportProperty.Current.Sign5Image = (byte[])dr[dtReportSignSchema.SIGN_IMAGEColumn.ColumnName];
                                    }
                                    break;
                            }
                        }
                    }
                }
            }

            //For Auditor Sign Note Details
            using (DataManager dataManager = new DataManager(SQLCommand.Setting.FetchAuditorNoteSign))
            {
                dataManager.Parameters.Add(dtReportSignSchema.ACC_YEAR_IDColumn, SettingProperty.Current.AccPeriodId);
                ResultArgs resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);

                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    dtAuditorNoteSign = resultArgs.DataSource.Table;
                }
            }

        }

        /// <summary>
        /// This method is used to delete/updtae report budget new project
        /// </summary>
        private void SaveReportBudgetNewProject()
        {
            if (SettingProperty.Current.CreateBudgetDevNewProjects == 0 && (reportId == "RPT-179" || reportId == "RPT-189"))
            {
                ResultArgs resultArgs = new ResultArgs();
                if (BudgetNewProjects != null)
                {
                    DataView dv = new DataView(BudgetNewProjects);

                    using (DataManager dataManager = new DataManager(SQLCommand.Setting.DeleteBudgetNewProjectsByAcYear))
                    {
                        dataManager.Parameters.Add(dtReportBudgjetNewProjectSchema.ACC_YEAR_IDColumn, SettingProperty.Current.AccPeriodId);
                        //On 20/04/2023, Load developmental/new project for concern budgets
                        if (SettingProperty.Current.CreateBudgetDevNewProjects == 1)
                        {
                            string budgetids = string.IsNullOrEmpty(ReportProperty.Current.Budget) ? "0" : ReportProperty.Current.Budget;
                            dataManager.Parameters.Add(dtReportBudgjetNewProjectSchema.DEVELOPMENTAL_NEW_BUDGETIDColumn, budgetids);
                        }
                        else
                            dataManager.Parameters.Add(dtReportBudgjetNewProjectSchema.DEVELOPMENTAL_NEW_BUDGETIDColumn, "0");
                        resultArgs = dataManager.UpdateData();
                    }

                    if (resultArgs.Success)
                    {
                        dv.RowFilter = dtReportBudgjetNewProjectSchema.NEW_PROJECTColumn.ColumnName + "<>'' " +
                                        " AND (" + dtReportBudgjetNewProjectSchema.PROPOSED_INCOME_AMOUNTColumn.ColumnName + "> 0 OR " +
                                        dtReportBudgjetNewProjectSchema.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName + "> 0 OR " +
                                        dtReportBudgjetNewProjectSchema.GN_HELP_PROPOSED_AMOUNTColumn.ColumnName + "> 0 OR " +
                                        dtReportBudgjetNewProjectSchema.HO_HELP_PROPOSED_AMOUNTColumn.ColumnName + "> 0)";

                        foreach (DataRowView drv in dv)
                        {
                            string budgetnewproject = drv[dtReportBudgjetNewProjectSchema.NEW_PROJECTColumn.ColumnName].ToString().Trim();
                            double proposedincome = NumberSet.ToDouble(drv[dtReportBudgjetNewProjectSchema.PROPOSED_INCOME_AMOUNTColumn.ColumnName].ToString());
                            double proposedexpense = NumberSet.ToDouble(drv[dtReportBudgjetNewProjectSchema.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName].ToString());
                            double proposedgovthelp = NumberSet.ToDouble(drv[dtReportBudgjetNewProjectSchema.GN_HELP_PROPOSED_AMOUNTColumn.ColumnName].ToString());
                            double proposedprovincehelp = NumberSet.ToDouble(drv[dtReportBudgjetNewProjectSchema.HO_HELP_PROPOSED_AMOUNTColumn.ColumnName].ToString());
                            Int32 includereports = NumberSet.ToInteger(drv[dtReportBudgjetNewProjectSchema.INCLUDE_REPORTSColumn.ColumnName].ToString());
                            string budgetremarks = drv[dtReportBudgjetNewProjectSchema.REMARKSColumn.ColumnName].ToString().Trim();

                            if (!string.IsNullOrEmpty(budgetnewproject) && (proposedincome > 0 || proposedexpense > 0 || proposedprovincehelp > 0))
                            {
                                using (DataManager dataManager = new DataManager(SQLCommand.Setting.UpdateBudgetNewProjectsByAcYear))
                                {
                                    dataManager.Parameters.Add(dtReportBudgjetNewProjectSchema.ACC_YEAR_IDColumn, SettingProperty.Current.AccPeriodId);
                                    dataManager.Parameters.Add(dtReportBudgjetNewProjectSchema.BUDGET_IDColumn, "0"); //For Attached with Report 
                                    dataManager.Parameters.Add(dtReportBudgjetNewProjectSchema.SEQUENCE_NOColumn, 1); //For Attached with Report 
                                    dataManager.Parameters.Add(dtReportBudgjetNewProjectSchema.NEW_PROJECTColumn, budgetnewproject);
                                    dataManager.Parameters.Add(dtReportBudgjetNewProjectSchema.PROPOSED_INCOME_AMOUNTColumn, proposedincome);
                                    dataManager.Parameters.Add(dtReportBudgjetNewProjectSchema.PROPOSED_EXPENSE_AMOUNTColumn, proposedexpense);
                                    dataManager.Parameters.Add(dtReportBudgjetNewProjectSchema.GN_HELP_PROPOSED_AMOUNTColumn, proposedgovthelp);
                                    dataManager.Parameters.Add(dtReportBudgjetNewProjectSchema.HO_HELP_PROPOSED_AMOUNTColumn, proposedprovincehelp);
                                    dataManager.Parameters.Add(dtReportBudgjetNewProjectSchema.INCLUDE_REPORTSColumn.ColumnName, includereports);
                                    dataManager.Parameters.Add(dtReportBudgjetNewProjectSchema.REMARKSColumn, budgetremarks);
                                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                                    resultArgs = dataManager.UpdateData();
                                }
                            }
                        }
                    }
                }
            }

            //On 20/04/2023, Load developmental/new project for concern budgets
            if (BudgetNewProjects != null && SettingProperty.Current.CreateBudgetDevNewProjects == 1 && (reportId == "RPT-179" || reportId == "RPT-189"))
            {
                string budgetids = string.IsNullOrEmpty(ReportProperty.Current.Budget) ? "0" : ReportProperty.Current.Budget;
                BudgetNewProjects.DefaultView.RowFilter = dtReportBudgjetNewProjectSchema.BUDGET_IDColumn.ColumnName + " IN (" + budgetids + ")";
                BudgetNewProjects = BudgetNewProjects.DefaultView.ToTable();
            }
        }

        public DataView ReportSettingInfo
        {
            get { return dvReportSettingInfo; }
        }

        public void ShowMessageBox(string Msg)
        {
            XtraMessageBox.Show(Msg, this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public string GetMessage(string keyCode)
        {

            ResourceManager resourceManger = new ResourceManager("ACPP.Resources.Messages.Messages", Assembly.GetExecutingAssembly());
            string msg = "";
            try
            {
                msg = resourceManger.GetString(keyCode);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage("Resoure File is not available", false);
            }
            return msg;
        }



        public int ShowProjectsinFooter { get; set; }

    }
}

///// <summary>
///// This method is used to insert/updtae sign details
///// </summary>
//private void SaveSignDetails()
//{
//    using (DataManager dataManager = new DataManager(SQLCommand.Setting.FetchSignDetails))
//    {
//        //1.Sign 1
//        ResultArgs resultArgs =  ReportSignInsertUpdate(RoleName1, Role1,Sign1Image, 1);
//        if (resultArgs.Success)
//        {
//            //2.Sign 2
//            resultArgs = ReportSignInsertUpdate(RoleName2, Role2, Sign2Image, 2);
//            if (resultArgs.Success)
//            {
//                //3.Sign 3
//                resultArgs = ReportSignInsertUpdate(RoleName3, Role3, Sign3Image, 3);

//                if (resultArgs.Success)
//                {
//                    //4.Sign 4
//                    resultArgs = ReportSignInsertUpdate(RoleName4, Role4, Sign4Image, 4);
//                    if (resultArgs.Success)
//                    {
//                        //5.Sign 5
//                        resultArgs = ReportSignInsertUpdate(RoleName5, Role5, Sign5Image, 5);
//                        if (!resultArgs.Success)
//                        {
//                            Sign5Image = null;
//                        }
//                    }
//                    else
//                    {
//                        Sign4Image = null;
//                    }
//                }
//                else if (!resultArgs.Success)
//                {
//                    Sign3Image = null;
//                }
//            }
//            else
//            {
//                Sign2Image = null;
//            }
//        }
//        else
//        {
//            Sign1Image = null;
//        }

//        if (!resultArgs.Success)
//        {
//            MessageRender.ShowMessage(resultArgs.Message);
//        }
//    }
//}

//private ResultArgs UpdateSignDetails(byte[] bytesignImage, Int32 signorder)
//       {
//           ResultArgs resultArgs = new ResultArgs();

//           using (DataManager dataManager = new DataManager(SQLCommand.Setting.UpdateSignDetails))
//           {
//               dataManager.Parameters.Clear();
//               dataManager.DataCommandArgs.IsDirectReplaceParameter = false;
//               dataManager.Parameters.Add(dtReportSignSchema.ACC_YEAR_IDColumn, SettingProperty.Current.AccPeriodId);
//               dataManager.Parameters.Add(dtReportSignSchema.SIGN_ORDERColumn, signorder);
//               dataManager.Parameters.Add(dtReportSignSchema.SIGN_IMAGEColumn, bytesignImage);
//               resultArgs = dataManager.UpdateData();
//           }
//           return resultArgs;
//       }

//       private ResultArgs ReportSignInsertUpdate(string rolename, string role, byte[] bytesignImage ,Int32 signorder )
//       {
//           ResultArgs resultArgs = new ResultArgs();

//           if (string.IsNullOrEmpty(rolename) && string.IsNullOrEmpty(role))
//           {
//               using (DataManager dataManager = new DataManager(SQLCommand.Setting.DeleteSign))
//               {
//                   //1.Sign 1
//                   dataManager.Parameters.Clear();
//                   dataManager.DataCommandArgs.IsDirectReplaceParameter = false;
//                   dataManager.Parameters.Add(dtReportSignSchema.ACC_YEAR_IDColumn, SettingProperty.Current.AccPeriodId);
//                   dataManager.Parameters.Add(dtReportSignSchema.SIGN_ORDERColumn, signorder);
//                   resultArgs = dataManager.UpdateData();
//               }
//           }
//           else
//           {
//               using (DataManager dataManager = new DataManager(SQLCommand.Setting.InsertUpdateSignDetail))
//               {
//                   //1.Sign 1
//                   dataManager.Parameters.Clear();
//                   dataManager.DataCommandArgs.IsDirectReplaceParameter = false;
//                   dataManager.Parameters.Add(dtReportSignSchema.ACC_YEAR_IDColumn, SettingProperty.Current.AccPeriodId);
//                   dataManager.Parameters.Add(dtReportSignSchema.ROLE_NAMEColumn, rolename);
//                   dataManager.Parameters.Add(dtReportSignSchema.ROLEColumn, role);
//                   dataManager.Parameters.Add(dtReportSignSchema.SIGN_ORDERColumn, signorder);
//                   resultArgs = dataManager.UpdateData();
//               }
//           }

//           if (resultArgs.Success)
//           {
//               UpdateSignDetails(bytesignImage, signorder);
//           }
//           return resultArgs;
//       }