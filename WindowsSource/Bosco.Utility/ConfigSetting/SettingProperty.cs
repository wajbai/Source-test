using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Newtonsoft.Json;

using Bosco.Utility;
using DevExpress.XtraSplashScreen;
using System.Reflection;
using System.Net;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Xml;

namespace Bosco.Utility.ConfigSetting
{
    public class SettingProperty : UISettingProperty
    {
        private static DataView dvSetting = null;
        private static DataView dvAccPeriod = null;
        private static DataTable dtAllAccountingPeriods = null;
        private static DataView dvAccPeriodPrevious = null;
        private static DataView dvuserProjectInfo = null;
        private static string headOfficeName = string.Empty;
        private static string societyName = string.Empty;
        private static string institudeName = string.Empty;
        private static int UserRoleId = 0;
        private static int CGSTLedgerID = 0;
        private static int SGSTLedgerID = 0;
        private static int IGSTLedgerID = 0;
        private static int tdsOnFDInterestLedgerId = 0;
        private static string LanguageSelectedId = string.Empty;

        private static string UserId = string.Empty;
        private static string noofNodes = string.Empty;
        private static string noOfModules = string.Empty;
        private static string address = string.Empty;
        private static string place = string.Empty;
        private static string pincode = string.Empty;
        private static string phone = string.Empty;
        private static string contactperson = string.Empty;
        private static int accessToMultiDB = 0;
        public static string headofficecode = "";
        public static string branachOfficeCode = "";
        private static string partbranchofficecode = "";
        private static string fax = string.Empty;
        private static string email = string.Empty;
        private static string url = string.Empty;
        public static byte[] acmeerpLogo = null;
        private static string state = string.Empty;
        private static string statecode = string.Empty;
        private static string country_info = string.Empty;
        private static string licensekeynumber = string.Empty;
        private static string licensekeyexprdate = string.Empty;
        private static string licenseKeyGeneratedDate = string.Empty;
        private static string licenseKeyYearFrom = string.Empty;
        private static string licenseKeyYearTo = string.Empty;

        private static string baseLicensekeyNumber = string.Empty;
        private static string baseLicenseHeadOfficeCode = string.Empty;
        private static string baseLicenseBranchCode = string.Empty;
        private static string baseLicenseBranchName = string.Empty;
        private static string baseLicenseLocation = string.Empty;
        private static LCBranchModuleStatus baseLicenseBranchStatus = LCBranchModuleStatus.Disabled;

        private static string baselcref1 = string.Empty;
        private static string baselcref2 = string.Empty;
        private static string baselcref3 = string.Empty;
        private static string baselcref4 = string.Empty;
        private static string baselcref5 = string.Empty;
        private static string baselcref6 = string.Empty;
        private static string baselcref7 = string.Empty;


        private static int islicensemodel = 1; //By default "yes", should validate license
        private static bool islicenseexpired = true; //This property to get license is expired or not 
        private static string dbUploadedOn = string.Empty;
        private static string dbRestoedOn = string.Empty;
        private static string dbRestoedRemarks = string.Empty;
        public static string ManagementCodeIntegration = "";
        public static string thirdParty = "";
        public static string ThirdPartyMode = "";
        public static string ThirdPartyURL = "";

        private static int multiLocation = 0;
        private static int enablePortal = 0;
        private static int lockMasters = 0;
        private static int mapHeadOfficeLedger = 1;
        private static string branchLocations = string.Empty;
        //public static string returnCurrentdate = "";
        public static string ApplicationStartUpPath = string.Empty;
        private const string SettingNameField = "Name";
        private const string SettingValueField = "Value";

        private const string YearFromField = "YEAR_FROM";
        private const string YearToField = "YEAR_TO";
        private const string AccyearField = "ACC_YEAR_ID";
        private const string BookBeginningFromField = "BOOKS_BEGINNING_FROM";
        private const string ProjectIdField = "PROJECT_ID";
        private const string ProjectField = "PROJECT";
        private const string RecentVoucherDateField = "VOUCHER_DATE";
        public static string ActiveDatabaseName = "acperp";
        public static string ActiveDatabaseAliasName = "acperp";
        public static string ActiveDatabaseLicenseKeypath = "AcMEERPLicense.xml";
        public static string AcmeerpLCFile = "AcmeerpRefLC.acp";
        private static SettingProperty current = null;
        public static int ActiveProjectId = 0;
        private static int istwomonthbudget = 0;
        private static int isapproveBudgetByPortal = 0;
        private static int isapproveBudgetByExcel = 0;
        private static int automaticdbbackuptoportal = 0;
        private static string enabledreports = "";
        private static Int32 allowMultiCurrency = 0;
        private static Int32 attachVoucherFiles = 0;
        private static Int32 enforceNatureBasedLedgersInVoucherEntry = 0;

        private static bool isLicenseKeyMismatchedByLicenseKeyDBLocation = true;
        private static bool isLicenseKeyMismatchedByHoProjects = true;

        public static string AcmeerpHiddenOperationPassword = "acmeerp@123";
        public static string RestoreMultipleDBPath = @"C:\AcME_ERP\";
        public static string RestoreMultipleDBFileName = @"MultipleDB.xml";
        public static string LoginPassword = string.Empty;
        public static string LoginUserName = string.Empty;

        private static DateTime voucherDate;
        private static DateTime voucherDateFrom;
        private static DateTime voucherDateTo;
        private static DateTime gstStartDate;
        private static Int32 gstzeroclassid;
        public static bool next = false;
        public static string AllProjectID = string.Empty;

        private static DateTime acmeerpportalserverdatetime;
        private static DateTime acmeerpgracetodaydate;

        public static bool Is_Application_Logout = false;
        // Selected Report's Module Id
        public static int ReportModuleId = 0;

        //Temporary settings to enable/disable Localization, Integrate Payroll with finance and Mutiple database features
        public static bool EnableLocalization = true;
        public static bool PayrollFinanceEnabled = false;
        public static bool EnableLicenseKeyFile = true;
        public static bool EnableMultipleDBBrowseLicenseKey = true;

        //Properties to validate the modules based on the license key.
        //These properties will be set soon after logging into the system by validating the license key file.
        public static bool EnableTDS = true;
        public static bool EnablePayroll = false;
        public static bool EnableAsset = false;
        public static bool EnableStock = false;
        public static bool EnableNetworking = false;
        public static bool EnablePayrollOnly = false;

        // Asset Common Properties starts
        public static Dictionary<int, DataTable> AssetListCollection = new Dictionary<int, DataTable>();
        public static Dictionary<int, DataTable> AssetInsuranceCollection = new Dictionary<int, DataTable>();
        public static Dictionary<int, DataTable> AssetAMCCollection = new Dictionary<int, DataTable>();
        public static Dictionary<int, DataTable> AssetCostcentreCollection = new Dictionary<int, DataTable>();

        public static Dictionary<Tuple<int, int>, DataTable> AssetMultiInsuranceCollection = new Dictionary<Tuple<int, int>, DataTable>();
        public static Dictionary<Tuple<int, int>, DataTable> AssetMultiInsuranceVoucherCollection = new Dictionary<Tuple<int, int>, DataTable>();

        public static DataTable AssetOutCollection = new DataTable();
        public static string AssetDeletedInoutIds = string.Empty;
        public static string AssetDeletedItemDetailIds = string.Empty;
        // Asset Common Properties ends

        // ------------------------Payroll- Common Members starts-----------------------------------------------

        public static string PayrollMonth = string.Empty;
        public static DataTable dtData = new DataTable();
        public static DataTable dtOpen = new DataTable();

        // ------------------------Payroll- Common Members ends-----------------------------------------------

        public const string CGST_LEDGER = "Central Goods & Service Tax (CGST)";
        public const string SGST_LEDGER = "State Goods & Service Tax (SGST)";
        public const string IGST_LEDGER = "Integrated Good & Service Tax (IGST)";
        public const string TDS_ON_FD_INTEREST_LEDGER = "TDS on FD Interest";

        //-----------------------------------------------------------------------------------------------------------------
        private static string SDBINM_skipped_ledger_Ids = string.Empty;


        //On 27/08/2021-----------------------------------------------------------
        public static Int32 DEFAULT_ADMIN_USER_ID = 1;
        public static string DEFAULT_ADMIN_USER_NAME = "ADMIN";

        public static Int32 DEFAULT_AUDITOR_ROLE_ID = 0;//it will be assigned after login
        public static Int32 DEFAULT_AUDITOR_USER_ID = 0;//it will be assigned after login
        public static string DEFAULT_AUDITOR_USER_NAME = "AUDITOR";
        public static string DEFAULT_AUDITOR_USER_ROLE = "AUDITOR";
        //------------------------------------------------------------------------

        private static string modulerightsdetails = String.Empty;

        //26/03/2022, Receipt Module
        private static LCBranchModuleStatus receipts_module_status = LCBranchModuleStatus.Disabled;
        public static string Enforce_Receipt_Module_FY = "01/04/2022";

        public static string Folder_VoucherFiles = "Voucher Files";
        public static string Folder_UserManuals = "User Manual";

        //On 23/01/2025 - voucher entry grace days details
        // Let us get it from Acmee.erp license key, based on on provice we can set default grace days
        // 
        //private static Int32 DEFAULT_GRACE_DAYS = 30;
        //07/04/2025, *Chinna, to Grace Days is 30 commanded and we fix it 0 for default days

        private static Int32 DEFAULT_GRACE_DAYS = 0;
        private static string voucherGraceTmpDateFrom = "";
        private static string voucherGraceTmpDateTo = "";
        private static string voucherGraceTmpValidUpTo = "";
        private static bool isvoucherGraceTmpActive = false;

        public static DataTable dtLoginDB = new DataTable();
        public static SettingProperty Current
        {
            get
            {
                if (current == null) { current = new SettingProperty(); }
                return current;
            }
        }

        private string GetSettingInfo(string name)
        {
            string val = "";

            try
            {
                if (dvSetting != null && dvSetting.Count > 0)
                {
                    for (int i = 0; i < dvSetting.Count; i++)
                    {
                        string record = dvSetting[i][SettingNameField].ToString();

                        if (name == record)
                        {
                            val = dvSetting[i][SettingValueField].ToString();
                            break; //25/01/2025 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }

            return val;
        }

        /// <summary>
        /// Set Setting Info as Dataview
        /// </summary>
        public DataView SettingInfo
        {
            set
            {
                SettingProperty.dvSetting = value;
            }
            get
            {
                return dvSetting;
            }
        }

        public string HeadOfficeName
        {
            get { return headOfficeName; }
            set { SettingProperty.headOfficeName = value; }
        }

        public string SocietyName
        {
            get { return societyName; }
            set { SettingProperty.societyName = value; }
        }

        public string InstituteName
        {
            get { return institudeName; }
            set { SettingProperty.institudeName = value; }
        }

        public string NoOfNodes
        {
            get { return noofNodes; }
            set { SettingProperty.noofNodes = value; }
        }

        public string NoOfModules
        {
            get { return noOfModules; }
            set { SettingProperty.noOfModules = value; }
        }
        public string Address
        {
            get { return address; }
            set { SettingProperty.address = value; }
        }
        public string Place
        {
            get { return place; }
            set { SettingProperty.place = value; }
        }
        public string PinCode
        {
            get { return pincode; }
            set { SettingProperty.pincode = value; }
        }
        public string ContactPerson
        {
            get { return contactperson; }
            set { SettingProperty.contactperson = value; }
        }
        public int AccesstoMultiDB
        {
            get { return accessToMultiDB; }
            set { SettingProperty.accessToMultiDB = value; }
        }
        public string ManagementCode
        {
            get { return ManagementCodeIntegration; }
            set { SettingProperty.ManagementCodeIntegration = value; }
        }
        public string ManagementMode
        {
            get { return ThirdPartyMode; }
            set { SettingProperty.ThirdPartyMode = value; }
        }
        public string ManagementURL
        {
            get { return ThirdPartyURL; }
            set { SettingProperty.ThirdPartyURL = value; }
        }
        public int MultiLocation
        {
            get { return multiLocation; }
            set { multiLocation = value; }
        }
        public int EnablePortal
        {
            get { return enablePortal; }
            set { enablePortal = value; }
        }
        public int LockMasters
        {
            get { return lockMasters; }
            set { lockMasters = value; }
        }
        public int MapHeadOfficeLedger
        {
            get { return mapHeadOfficeLedger; }
            set { mapHeadOfficeLedger = value; }
        }
        public string BranchLocations
        {
            get { return branchLocations; }
            set { branchLocations = value; }
        }
        public string LicenseKeyNumber
        {
            get { return licensekeynumber; }
            set { licensekeynumber = value; }
        }
        public string LicenseKeyExprDate
        {
            get { return licensekeyexprdate; }
            set { licensekeyexprdate = value; }
        }
        public string LicenseKeyGeneratedDate
        {
            get { return licenseKeyGeneratedDate; }
            set { licenseKeyGeneratedDate = value; }
        }
        public string LicenseKeyYearFrom
        {
            get { return licenseKeyYearFrom; }
            set { licenseKeyYearFrom = value; }
        }
        public string LicenseKeyYearTo
        {
            get { return licenseKeyYearTo; }
            set { licenseKeyYearTo = value; }
        }

        public string BaseLicensekeyNumber
        {
            get { return baseLicensekeyNumber; }
            set { baseLicensekeyNumber = value; }
        }

        public string BaseLicenseHeadOfficeCode
        {
            get { return baseLicenseHeadOfficeCode; }
            set { baseLicenseHeadOfficeCode = value; }
        }

        public string BaseLicenseBranchCode
        {
            get { return baseLicenseBranchCode; }
            set { baseLicenseBranchCode = value; }
        }

        public string BaseLicensePartBranchOfficeCode
        {
            get
            {
                string rtn = string.Empty;
                if (!string.IsNullOrEmpty(BaseLicenseBranchCode))
                {
                    rtn = BaseLicenseBranchCode.Substring(BaseLicenseBranchCode.Length - 6);
                }
                return rtn;

            }
        }

        public string BaseLicenseBranchName
        {
            get { return baseLicenseBranchName; }
            set { baseLicenseBranchName = value; }
        }

        public string BaseLicenseLocation
        {
            get { return baseLicenseLocation; }
            set { baseLicenseLocation = value; }
        }

        public LCBranchModuleStatus BaseLicenseBranchStatus
        {
            get { return baseLicenseBranchStatus; }
            set { baseLicenseBranchStatus = value; }
        }

        public string BaseLCRef1
        {
            get { return baselcref1; }
            set { baselcref1 = value; }
        }

        public string BaseLCRef2
        {
            get { return baselcref2; }
            set { baselcref2 = value; }
        }

        public string BaseLCRef3
        {
            get { return baselcref3; }
            set { baselcref3 = value; }
        }

        public string BaseLCRef4
        {
            get { return baselcref4; }
            set { baselcref4 = value; }
        }

        public string BaseLCRef5
        {
            get { return baselcref5; }
            set { baselcref5 = value; }
        }

        public string BaseLCRef6
        {
            get { return baselcref6; }
            set { baselcref6 = value; }
        }

        public string BaseLCRef7
        {
            get { return baselcref7; }
            set { baselcref7 = value; }
        }


        //10/03/2020, for Mysore
        public int IsTwoMonthBudget
        {
            get { return istwomonthbudget; }
            set { istwomonthbudget = value; }
        }

        // 26.08.2020
        public int ApproveBudgetByPortal
        {
            get { return isapproveBudgetByPortal; }
            set { isapproveBudgetByPortal = value; }
        }

        /// <summary>
        /// On 22/03/2021, to approve budget by excel sheet
        /// </summary>
        public int ApproveBudgetByExcel
        {
            //isapproveBudgetByExcel = 1;
            get
            {
                return isapproveBudgetByExcel;
            }
            set { isapproveBudgetByExcel = value; }

        }

        /// <summary>
        /// On 09/11/2023, License Based Reports
        /// </summary>
        public string EnabledReports
        {
            //isapproveBudgetByExcel = 1;
            get
            {
                return enabledreports.Trim();
            }
            set { enabledreports = value; }

        }

        //On 09/09/2024, To allow multi currency
        public Int32 AllowMultiCurrency
        {
            get
            {
                //On 28/10/2024, To enfource Multi currency for few
                if (allowMultiCurrency == 0 && (HeadofficeCode.ToUpper() == "SDBANN" || PartBranchOfficeCode.ToUpper() == "SDBROMA"))
                {
                    allowMultiCurrency = 1;
                }
                return allowMultiCurrency;
            }
            set
            {
                allowMultiCurrency = value;
            }
        }

        //On 15/11/2024, To Attach Voucher Files
        public Int32 AttachVoucherFiles
        {
            get
            {
                return attachVoucherFiles;
            }
            set
            {
                attachVoucherFiles = value;
            }
        }

        //On 19/11/2024, Show Nature based Ledgers in Voucher Entry
        public Int32 EnforceNatureBasedLedgersInVoucherEntry
        {
            get
            {
                enforceNatureBasedLedgersInVoucherEntry = this.AllowMultiCurrency;
                return enforceNatureBasedLedgersInVoucherEntry;
            }
            set
            {
                enforceNatureBasedLedgersInVoucherEntry = value;
            }
        }

        //07/05/2020, Send current backup automatically to Acmeerp Portal
        /// <summary>
        /// 0 - Disable Automatic Backup to Portal
        /// 1 - Enable Automatic Backup to Portal
        /// </summary>
        public int AutomaticDBBackupToPortal
        {
            set { automaticdbbackuptoportal = value; }
            get
            {
                //by default, automatic backup feature is enabled for all sdb congregation provinces, SCCGSB and demo all licenses
                if (IS_SDB_CONGREGATION || IS_SCCGSB_CONGREGATION || IS_BOSCOS_DEMO)
                {
                    automaticdbbackuptoportal = 1;
                }
                return automaticdbbackuptoportal;
            }
        }

        /// <summary>
        /// On 16/03/2023, to set whether current licensekey mismatching based on location
        /// </summary>
        public bool IsLicenseKeyMismatchedByLicenseKeyDBLocation
        {
            get
            {
                return isLicenseKeyMismatchedByLicenseKeyDBLocation;
            }

            set
            {
                isLicenseKeyMismatchedByLicenseKeyDBLocation = value;
            }
        }

        /// <summary>
        /// On 16/03/2023, to set whether current licensekey mismatching based on projects
        /// </summary>
        public bool IsLicenseKeyMismatchedByHoProjects
        {
            get
            {
                return isLicenseKeyMismatchedByHoProjects;
            }

            set
            {
                isLicenseKeyMismatchedByHoProjects = value;
            }
        }

        /// <summary>
        /// It will return true, if head office code starts with SDB
        /// </summary>
        public bool IS_SDB_CONGREGATION
        {
            get
            {
                return headofficecode.ToUpper().Substring(0, 3) == "SDB";
            }
        }

        /// <summary>
        /// For all FMA congregation license keys
        /// On 23/11/2021, It will return true, if head office code starts with FMA
        /// </summary>
        public bool IS_FMA_CONGREGATION
        {
            get
            {
                return headofficecode.ToUpper().Substring(0, 3) == "FMA";
            }
        }

        //06/07/2020, define cmf congregation
        public bool IS_CMF_CONGREGATION
        {
            get
            {
                return (headofficecode.ToUpper().Substring(0, 3) == "CMF");
                //return false;
            }
        }

        public bool IS_CMFBPI
        {
            get
            {
                return headofficecode.ToUpper() == "CMFBPI";
            }
        }

        // 22/08/2023, to show budget mapped ledgers, instead of showing all ledgers for cmf
        public bool IS_CMFKOL
        {
            get
            {
                return headofficecode.ToUpper() == "CMFKOL";
            }
        }

        public bool IS_SCCGSB_CONGREGATION
        {
            get
            {
                return headofficecode.ToUpper() == "SCCGSB";
            }
        }

        public bool IS_ABEBEN_DIOCESE
        {
            get { return headofficecode.ToUpper() == "ABEBEN"; }
        }

        public bool IS_DIOMYS_DIOCESE
        {
            get { return headofficecode.ToUpper() == "DIOMYS"; }
        }

        public bool IS_BOSCOS_DEMO
        {
            get
            {
                return headofficecode.ToUpper() == "BOSCOS";
            }
        }

        public bool IS_SDB_INM
        {
            get
            {
                return headofficecode.ToUpper() == "SDBINM";
            }
        }

        public bool IS_SDB_ING
        {
            get
            {
                return headofficecode.ToUpper() == "SDBING";
            }
        }

        public bool IS_SDB_RMG
        {
            get
            {
                return headofficecode.ToUpper() == "SDBRMG";
            }
        }


        public bool IS_FDCCSI
        {
            get
            {
                return headofficecode.ToUpper() == "FDCCSI";
            }
        }


        public bool IS_SAPPIC
        {
            get
            {
                return headofficecode.ToUpper().Substring(0, 3) == "SAP";
            }
        }


        /// <summary>
        /// on 28/02/2024, for MONTFORT congregation license keys
        /// </summary>
        public bool IS_BSG_CONGREGATION
        {
            get
            {
                return (headofficecode.ToUpper().Substring(0, 3) == "BSG");
                //return false;
            }
        }

        public LCBranchModuleStatus FINAL_RECEIPT_MODULE_STATUS
        {
            set { receipts_module_status = value; }
            get
            {
                return receipts_module_status;
            }
        }


        /// <summary>
        /// Always it will return allowed to use Receipt module for all other congregations, 
        /// for SDBINM, decide based on receipt module enable status from acme.erp portal
        /// </summary>
        public bool ENABLE_TRACK_RECEIPT_MODULE
        {
            get
            {
                bool rtn = true;
                if (IS_SDB_INM)
                {
                    rtn = (FINAL_RECEIPT_MODULE_STATUS == LCBranchModuleStatus.Approved);
                }

                return rtn;
            }
        }

        public bool IS_CMF_SLA
        {
            get
            {
                return headofficecode.ToUpper() == "CMFSLA";
            }

        }


        /// <summary>
        /// On 25/03/2021
        /// 
        /// For SDBINM Auditors suggested to skip below mentioned Ledgers for Voucher Entries
        /// </summary>
        public DateTime SDBINM_EnforceLedgersYearFrom
        {
            get
            {
                DateTime enforceLedgersYearFrom = DateSet.ToDate("01/04/2021", false);
                return enforceLedgersYearFrom;
            }
        }

        /// <summary>
        /// On 25/03/2021
        /// 
        /// For SDBINM Auditors suggested to skip below mentioned Ledgers for Voucher Entries
        /// </summary>
        public string SDBINM_SkippedLedgerIds
        {
            get
            {
                return SDBINM_skipped_ledger_Ids;
            }
            set
            {
                SDBINM_skipped_ledger_Ids = value;
            }
        }

        /// <summary>
        /// On 29/06/2019, For Mumbai Province, In Budget, They need to maintain Province Help amount separately in Budget.
        /// It should be added into Budget income and should not be affected Finance
        /// </summary>
        public string BUDGET_HO_HELP_AMOUNT_CAPTION
        {
            get
            {
                return "Province Help";
            }
        }

        /// <summary>
        /// On 29/06/2019, For Mumbai Province, In Budget, They need to maintain Province Help amount separately in Budget.
        /// It should be added into Budget income and should not be affected Finance
        /// </summary>
        public bool ENABLE_BUDGET_HO_HELP_AMOUNT
        {
            get
            {
                return headofficecode.ToUpper() == "SDBINB";
            }
        }

        /// <summary>
        /// On 28/08/2021, default admin user id
        /// </summary>
        public Int32 DefaultAdminUserId
        {
            get
            {
                return DEFAULT_ADMIN_USER_ID;
            }
        }

        /// <summary>
        /// On 28/08/2021, default admin user name
        /// </summary>
        public string DefaultAdminUserName
        {
            get
            {
                return DEFAULT_ADMIN_USER_NAME;
            }
        }

        /// <summary>
        /// On 28/08/2021, default auditor user id
        /// </summary>
        public Int32 DefaultAuditorUserId
        {
            get
            {
                return DEFAULT_AUDITOR_USER_ID;
            }
            set
            {
                DEFAULT_AUDITOR_USER_ID = value;
            }
        }

        /// <summary>
        /// On 28/08/2021, default auditor user name
        /// </summary>
        public string DefaultAuditorUserName
        {
            get
            {
                return DEFAULT_AUDITOR_USER_NAME;
            }
        }

        /// <summary>
        /// On 30/08/2021, default Auditor Role
        /// </summary>
        public Int32 DefaultAuditorRoleId
        {
            get
            {
                return DEFAULT_AUDITOR_ROLE_ID;
            }
            set
            {
                DEFAULT_AUDITOR_ROLE_ID = value;
            }
        }

        /// <summary>
        /// On 30/08/2021, to get defautl role name
        /// </summary>
        public string DefaultAuditorRoleName
        {
            get
            {
                return DEFAULT_AUDITOR_USER_ROLE;
            }
        }

        /// <summary>
        /// To keep license model form key
        /// 1. This will enable, if license key generated date is greate than 01/04/2017
        /// 
        /// 2.
        /// 1: Yes: sholud validate license period when user makes transactions and show alert in home page
        /// 0. No : No validataion (open, user can make transaction for any period) 
        /// 
        /// 3. FIXED ::::: For SDB congregation, no License validation, they are open to make transaction 
        /// 
        /// 4. By default "yes", should validate license
        /// 
        /// </summary>
        public int IsLicenseModel
        {
            get
            {
                //Eventhough license model =yes, it will return "NO" if head office code is SDB, BOSCOdemo or license key generated date is < 01/04/2017
                if (islicensemodel == 1)
                {
                    if (DateSet.ToDate(licenseKeyGeneratedDate, false) < DateSet.ToDate("01/04/2017", false) || IS_SDB_CONGREGATION || IS_BOSCOS_DEMO)
                    {
                        islicensemodel = 0;
                    }
                }
                return islicensemodel;
            }
            set { islicensemodel = value; }
        }

        /// <summary>
        /// This property to get license is expired or not 
        /// </summary>
        public bool IsLicenseExpired
        {
            get
            {
                return islicenseexpired;
            }
            set { islicenseexpired = value; }
        }

        /// <summary>
        /// This property is used to form local upload db back name
        /// 
        /// If branch has multi-Location, upload db to portal should be based on location, for exmaple one branch as location1, location2, when we upload brach db
        /// there shouuld be two backup files instatuion_location1.gz, instatuion_location2.gz
        /// 
        /// if there is no location based, will take only one db as instatuion.gz
        /// </summary>
        public string BranchUploadDBName
        {
            get
            {
                string branchDBUploadName = CommonMethod.RemoveSpecialCharacter(InstituteName) + "-" + PartBranchOfficeCode;
                string[] branchlocations = BranchLocations.Split(',');
                if (branchlocations.GetUpperBound(0) > 0) //(this.AppSetting.MultiLocation == 1)
                {
                    branchDBUploadName = CommonMethod.RemoveSpecialCharacter(InstituteName) + " (" + Location + ") - " + PartBranchOfficeCode;
                }
                return branchDBUploadName;
            }
        }


        /// <summary>
        /// 24/08/2020, To form Acmeerp Branch details as Referecne code. It will be used in many places
        /// 
        /// #1. When Backup is taking, insert this reference code into Backup file. it will be used to valudated when it is restored
        /// </summary>
        public string AcmeerpBranchDetailsReference
        {
            get
            {
                string Rtn = HeadofficeCode + Delimiter.ECap + PartBranchOfficeCode + Delimiter.ECap + Location;
                return Rtn;
            }
        }

        public string HeadofficeCode
        {
            get { return headofficecode; }
            set { SettingProperty.headofficecode = value; }
        }
        public string BranchOfficeCode
        {
            get { return branachOfficeCode; }
            set
            {
                SettingProperty.branachOfficeCode = value;
                SettingProperty.partbranchofficecode = SettingProperty.branachOfficeCode.ToString().Remove(0, 6);
            }
        }

        public string PartBranchOfficeCode
        {
            get { return partbranchofficecode; }
        }
        public string Phone
        {
            get { return phone; }
            set { SettingProperty.phone = value; }
        }
        public string CountryInfo
        {
            get { return country_info; }
            set { SettingProperty.country_info = value; }
        }

        public string StateCode
        {
            get { return statecode; }
            set { SettingProperty.statecode = value; }
        }

        public string State
        {
            get { return state; }
            set { SettingProperty.state = value; }
        }
        public string Fax
        {
            get { return fax; }
            set { SettingProperty.fax = value; }
        }
        public string Email
        {
            get { return email; }
            set { SettingProperty.email = value; }
        }
        public string URL
        {
            get { return url; }
            set { SettingProperty.url = value; }
        }

        public byte[] AcMeERPLogo
        {
            get { return acmeerpLogo; }
            set { SettingProperty.acmeerpLogo = value; }
        }

        public byte[] InstituteLogo { get; set; }

        public string Country
        {
            get
            {
                return GetSettingInfo(Setting.Country.ToString());
            }
        }

        public string CreditBalance
        {
            get
            {
                return GetSettingInfo(Setting.CreditBalance.ToString());
            }
        }

        public string Location
        {
            get
            {
                return GetSettingInfo(Setting.Location.ToString());
            }
        }

        public string ForeignBankAccount
        {
            get
            {
                return GetSettingInfo(Setting.UIForeignBankAccount.ToString());
            }

        }
        public string Months
        {
            get
            {
                return GetSettingInfo(Setting.Months.ToString());
            }
        }

        public string Currency
        {
            get
            {
                return GetSettingInfo(Setting.Currency.ToString());
            }
        }

        public string CurrencyName
        {
            get
            {
                return GetSettingInfo(Setting.CurrencyName.ToString());
            }
        }

        public string CurrencyPosition
        {
            get
            {
                return GetSettingInfo(Setting.CurrencyPosition.ToString());
            }
        }

        public string CurrencyPositivePattern
        {
            get
            {
                return GetSettingInfo(Setting.CurrencyPositivePattern.ToString());
            }
        }

        public string CurrencyNegativePattern
        {
            get
            {
                return GetSettingInfo(Setting.CurrencyNegativePattern.ToString());
            }
        }

        public string CurrencyNegativeSign
        {
            get
            {
                return GetSettingInfo(Setting.CurrencyNegativeSign.ToString());
            }
        }

        public string CurrencyCode
        {
            get
            {
                return GetSettingInfo(Setting.CurrencyCode.ToString());
            }
        }

        public string CurrencyCodePosition
        {
            get
            {
                return GetSettingInfo(Setting.CurrencyCodePosition.ToString());
            }
        }

        public string DigitGrouping
        {
            get
            {
                return GetSettingInfo(Setting.DigitGrouping.ToString());
            }
        }

        public string GroupingSeparator
        {
            get
            {
                return GetSettingInfo(Setting.GroupingSeparator.ToString());
            }
        }

        public string DecimalPlaces
        {
            get
            {
                return GetSettingInfo(Setting.DecimalPlaces.ToString());
            }
        }

        public string DecimalSeparator
        {
            get
            {
                return GetSettingInfo(Setting.DecimalSeparator.ToString());
            }
        }

        public string HighNaturedAmt
        {
            get
            {
                return GetSettingInfo(Setting.HighNaturedAmt.ToString());
            }
        }

        public string TransEntryMethod
        {
            get
            {
                return GetSettingInfo(Setting.TransEntryMethod.ToString());
            }
        }

        public string Language
        {
            get
            {
                return GetSettingInfo(Setting.UILanguage.ToString());
            }
        }

        public string DateFormat
        {
            get
            {
                return GetSettingInfo(Setting.UIDateFormat.ToString());
            }
        }

        public string DateSeparator
        {
            get
            {
                return GetSettingInfo(Setting.UIDateSeparator.ToString());
            }
        }

        public string Themes
        {
            get
            {
                return GetSettingInfo(Setting.UIThemes.ToString());
            }
        }

        public string TransClose
        {
            get
            {
                return GetSettingInfo(Setting.UITransClose.ToString());
            }

        }

        public string ShowGSTPan
        {
            get
            {
                return GetSettingInfo(Setting.UITransGSTPan.ToString());
            }
        }

        public string UpdaterDownloadBy
        {
            get
            {
                string ftpHtpp = GetSettingInfo(Setting.UpdaterDownloadBy.ToString());
                ftpHtpp = string.IsNullOrEmpty(ftpHtpp) ? "0" : ftpHtpp;
                return ftpHtpp;
            }
        }


        public string ProxyUse
        {
            get
            {
                return GetSettingInfo(Setting.ProxyUse.ToString());
            }
        }

        public string ProxyAddress
        {
            get
            {
                return GetSettingInfo(Setting.ProxyAddress.ToString());
            }
        }

        public string ProxyPort
        {
            get
            {
                return GetSettingInfo(Setting.ProxyPort.ToString());
            }
        }

        public string ProxyAuthenticationUse
        {
            get
            {
                return GetSettingInfo(Setting.ProxyAuthenticationUse.ToString());
            }
        }

        public string ProxyUserName
        {
            get
            {
                return GetSettingInfo(Setting.ProxyUserName.ToString());
            }
        }

        public string ProxyPassword
        {
            get
            {
                return GetSettingInfo(Setting.ProxyPassword.ToString());
            }
        }

        public string ProjSelection
        {
            get
            {
                return GetSettingInfo(Setting.UIProjSelection.ToString());
            }
        }

        public string VoucherPrint
        {
            get
            {
                return GetSettingInfo(Setting.PrintVoucher.ToString());
            }
        }

        public string TransMode
        {
            get
            {
                return GetSettingInfo(Setting.UITransMode.ToString());
            }
        }

        public string PayrollPassword
        {
            get
            {
                string value = GetSettingInfo(Setting.PayrollPassword.ToString());
                ResultArgs result = CommonMethod.DecreptWithResultArg(value);
                if (result.Success)
                {
                    value = result.ReturnValue.ToString();
                }
                value = (string.IsNullOrEmpty(value) ? string.Empty : value);
                return value;
            }
        }


        public DataView AccPeriodInfo
        {
            set
            {
                SettingProperty.dvAccPeriod = value;
            }
        }

        public DataView AccPeriodInfoPrevious
        {
            set
            {
                SettingProperty.dvAccPeriodPrevious = value;
            }
        }


        /// <summary>
        /// # 30/04/2021, to have all Finance Years
        /// </summary>
        public DataTable AllAccountingPeriods
        {
            set
            {
                SettingProperty.dtAllAccountingPeriods = value;
            }
        }

        /// <summary>
        /// Get first FY Date from
        /// </summary>
        public DateTime FirstFYDateFrom
        {
            get
            {
                DateTime rtn = DateSet.ToDate(YearFrom, false);
                if (SettingProperty.dtAllAccountingPeriods != null)
                {
                    DataTable dt = SettingProperty.dtAllAccountingPeriods;
                    dt.DefaultView.Sort = YearFromField;
                    if (dt.DefaultView.Count > 0)
                    {
                        rtn = DateSet.ToDate(dt.DefaultView[0][YearFromField].ToString(), false);
                    }
                }

                return rtn;
            }
        }

        /// <summary>
        /// Get first FY Date from
        /// </summary>
        public DateTime FirstFYDateTo
        {
            get
            {
                DateTime rtn = DateSet.ToDate(YearFrom, false);
                if (SettingProperty.dtAllAccountingPeriods != null)
                {
                    DataTable dt = SettingProperty.dtAllAccountingPeriods;
                    dt.DefaultView.Sort = YearFromField;
                    if (dt.DefaultView.Count > 0)
                    {
                        rtn = DateSet.ToDate(dt.DefaultView[0][YearToField].ToString(), false);
                    }
                }

                return rtn;
            }
        }


        /// <summary>
        /// Get Last FY Date to
        /// </summary>
        public DateTime LastFYDateTo
        {
            get
            {
                DateTime rtn = DateSet.ToDate(YearTo, false);
                if (SettingProperty.dtAllAccountingPeriods != null)
                {
                    DataTable dt = SettingProperty.dtAllAccountingPeriods;
                    dt.DefaultView.Sort = YearFromField;
                    if (dt.DefaultView.Count > 0)
                    {
                        rtn = DateSet.ToDate(dt.DefaultView[dt.DefaultView.Count - 1][YearToField].ToString(), false);
                    }
                }

                return rtn;
            }
        }

        /// <summary>
        /// Get Last FY Date to
        /// </summary>
        public DateTime LastFYDateFrom
        {
            get
            {
                DateTime rtn = DateSet.ToDate(YearTo, false);
                if (SettingProperty.dtAllAccountingPeriods != null)
                {
                    DataTable dt = SettingProperty.dtAllAccountingPeriods;
                    dt.DefaultView.Sort = YearFromField;
                    if (dt.DefaultView.Count > 0)
                    {
                        rtn = DateSet.ToDate(dt.DefaultView[dt.DefaultView.Count - 1][YearFromField].ToString(), false);
                    }
                }

                return rtn;
            }
        }

        public DataView UserProjectInfor
        {
            set
            {
                SettingProperty.dvuserProjectInfo = value;
            }
        }



        private string GetAccPeriodInfo(string name)
        {
            string val = "";

            if (dvAccPeriod != null && dvAccPeriod.Count > 0)
            {
                val = dvAccPeriod[0][name].ToString();// dvAccPeriod[0][name].ToString();
            }
            return val;
        }

        private string GetAccPeriodInfoPrevious(string name)
        {
            string val = "";

            if (dvAccPeriodPrevious != null && dvAccPeriodPrevious.Count > 0)
            {
                val = dvAccPeriodPrevious[0][name].ToString();// dvAccPeriod[0][name].ToString();
            }
            return val;
        }

        private string GetUserProjectInfo(string name)
        {
            string val = "";

            if (dvuserProjectInfo != null && dvuserProjectInfo.Count > 0)
            {
                val = dvuserProjectInfo[0][name].ToString();// dvAccPeriod[0][name].ToString();
            }
            return val;
        }


        public string YearFrom
        {
            get
            {
                return GetAccPeriodInfo(YearFromField);
            }
        }

        public string YearTo
        {
            get
            {
                return GetAccPeriodInfo(YearToField);
            }
        }

        /// <summary>
        /// Return previous ac year date from, if there is not previous year..will return bookg begning -2
        /// </summary>
        public string YearFromPrevious
        {
            get
            {
                string rtn = GetAccPeriodInfoPrevious(YearFromField);

                if (String.IsNullOrEmpty(rtn))
                {
                    DateTime dBookBegin = this.DateSet.ToDate(this.BookBeginFrom, false).AddDays(-2);
                    rtn = dBookBegin.ToShortDateString();
                }
                return rtn;
            }
        }

        /// <summary>
        /// Return previous ac year date to, if there is not previous year..will return bookg begning -2
        /// </summary>
        public string YearToPrevious
        {
            get
            {
                string rtn = GetAccPeriodInfoPrevious(YearToField);

                if (String.IsNullOrEmpty(rtn))
                {
                    DateTime dBookBegin = this.DateSet.ToDate(this.BookBeginFrom, false).AddDays(-2);
                    rtn = dBookBegin.ToShortDateString();
                }
                return rtn;
            }
        }

        public string AccPeriodId
        {
            get
            {
                return GetAccPeriodInfo(AccyearField);
            }
        }

        public string BookBeginFrom
        {
            get
            {
                return GetAccPeriodInfo(BookBeginningFromField);
            }
        }

        public string UserProjectId
        {
            get
            {
                return GetUserProjectInfo(ProjectIdField);
            }
        }

        public string UserAllProjectId
        {
            get
            {
                return AllProjectID;
            }
            set { AllProjectID = value; }
        }

        private static string hobconnectionString = string.Empty;
        public static string HOBConnectionString
        {
            set
            {
                hobconnectionString = value;
            }
            get
            {
                return hobconnectionString;
            }
        }

        public string RecentVoucherDate
        {
            get
            {
                return GetUserProjectInfo(RecentVoucherDateField);
            }
        }
        /// <summary>
        /// This is to maintain user selected date from the popup window
        /// </summary>
        public DateTime VoucherDate
        {
            set { voucherDate = value; }
            get { return voucherDate; }
        }

        public DateTime VoucherDateFrom
        {
            set { voucherDateFrom = value; }
            get { return voucherDateFrom; }
        }

        public DateTime VoucherDateTo
        {
            set { voucherDateTo = value; }
            get { return voucherDateTo; }
        }

        public DateTime GSTStartDate
        {
            set { gstStartDate = value; }
            get { return gstStartDate; }
        }

        public Int32 GSTZeroClassId
        {
            set { gstzeroclassid = value; }
            get { return gstzeroclassid; }
        }

        public DateTime AcmeerpPortalServerDateTime
        {
            set { acmeerpportalserverdatetime = value; }
            get { return acmeerpportalserverdatetime; }
        }

        public DateTime AcmeerpGraceTodayDateTime
        {
            set { acmeerpgracetodaydate = value; }
            get { return acmeerpgracetodaydate; }
        }

        public string UserProject
        {
            get
            {
                return GetUserProjectInfo(ProjectField);
            }
        }

        /// <summary>
        /// This property is used in Report Filter Criteria form.
        /// </summary>
        public DateTime deDateFromValue;
        public DateTime AssignDateFrom
        {
            get
            {
                return deDateFromValue;
            }
            set
            {
                deDateFromValue = value;
            }
        }



        /// <summary>
        /// This property is used in Report Filter Criteria form.
        /// </summary>
        public DateTime deDateToValue;
        public DateTime AssignDateTo
        {
            get
            {
                return deDateToValue;
            }
            set
            {
                deDateToValue = value;
            }
        }

        public int RoleId
        {
            get
            {
                return UserRoleId;
            }
            set
            {
                SettingProperty.UserRoleId = value;
            }
        }

        public int CGSTLedgerId
        {
            get
            {
                return CGSTLedgerID;
            }
            set
            {
                SettingProperty.CGSTLedgerID = value;
            }
        }

        public int SGSTLedgerId
        {
            get
            {
                return SGSTLedgerID;
            }
            set
            {
                SettingProperty.SGSTLedgerID = value;
            }
        }

        public int IGSTLedgerId
        {
            get
            {
                return IGSTLedgerID;
            }
            set
            {
                SettingProperty.IGSTLedgerID = value;
            }
        }

        public int TDSOnFDInterestLedgerId
        {
            get
            {
                return tdsOnFDInterestLedgerId;
            }
            set
            {
                SettingProperty.tdsOnFDInterestLedgerId = value;
            }
        }

        public string LanguageId
        {
            get
            {
                return LanguageSelectedId;
            }
            set
            {
                SettingProperty.LanguageSelectedId = value;
            }
        }

        public string LoginUserId
        {
            get
            {
                return UserId;
            }
            set
            {
                SettingProperty.UserId = value;
            }
        }

        //public string CurrentDate
        //{
        //    get
        //    {
        //        return returnCurrentdate;
        //    }
        //    set
        //    {
        //        SettingProperty.returnCurrentdate = value;
        //    }
        //}

        /// <summary>
        /// On 12/06/2017, get product version from master.settings table
        /// </summary>
        public string AcmeerpVersionFromDB
        {
            get
            {
                return GetSettingInfo(Setting.ProductVersion.ToString());
            }
        }

        /// <summary>
        /// On 12/06/2017, get product version from Local files like exe an DLL
        /// </summary>
        public string AcmeerpVersionFromEXE
        {
            get
            {
                return Assembly.GetEntryAssembly().GetName().Version.ToString();
            }
        }

        /// <summary>
        /// On 12/06/2017, get DBUploadedon from master.settings table
        /// </summary>
        public string DBUploadedOn
        {
            get
            {
                if (string.IsNullOrEmpty(dbUploadedOn))
                {
                    dbUploadedOn = GetSettingInfo(Setting.DBUploadedOn.ToString());
                }
                return dbUploadedOn;
            }
            set
            {
                dbUploadedOn = value;
            }
        }

        /// <summary>
        /// On 25/11/2024, get DBRestoredOn from master.settings table
        /// </summary>
        public string DBRestoredOn
        {
            get
            {
                if (string.IsNullOrEmpty(dbRestoedOn))
                {
                    dbRestoedOn = GetSettingInfo(Setting.DBRestoredOn.ToString());
                }
                return dbRestoedOn;
            }
            set
            {
                dbRestoedOn = value;
            }
        }

        /// <summary>
        /// On 25/11/2024, get DBRestoredOn from master.settings table
        /// </summary>
        public string DBRestoredRemarks
        {
            get
            {
                if (string.IsNullOrEmpty(dbRestoedRemarks))
                {
                    dbRestoedRemarks = GetSettingInfo(Setting.DBRestoredRemarks.ToString());
                }
                return dbRestoedRemarks;
            }
            set
            {
                dbRestoedRemarks = value;
            }
        }

        /// <summary>
        /// For Temp;
        /// 06/02/2024, Number of multi database allowed
        /// Later, it will be handled in Acme.erp license key
        /// 
        /// = 0 : Any number of Multi DBs can be created
        /// > 0 : If it is greater than 0, only mentioned DBs can be created
        /// 
        /// For SDBINM : As of now fix 1.
        /// For Others : no restriction
        /// 
        /// Later, it will be handled in Acme.erp license key
        /// </summary>
        /// <returns></returns>
        public Int32 NoOfAllowedMultiDBs
        {
            get
            {
                Int32 Rtn = 0;

                if ((this.IS_SDB_INM || this.BaseLicenseHeadOfficeCode.ToString().ToUpper() == "SDBINM") && this.AccesstoMultiDB == 1 &&
                    this.BaseLicensePartBranchOfficeCode.ToUpper() != "SDBSPC" && this.BaseLicensePartBranchOfficeCode.ToUpper() != "INMSIS" && this.BaseLicensePartBranchOfficeCode.ToUpper() != "SURA24")
                {
                    // Rtn = 1;

                }

                return Rtn;
            }
        }

        /// <summary>
        /// For Temp;
        /// 06/02/2024, To get is it splited acmeerp previous year database
        /// Later, it will be handled properly
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsSplitPreviousYearAcmeerpDB
        {
            get
            {
                bool rtn = false;


                if ((this.IS_SDB_INM || this.BaseLicenseHeadOfficeCode.ToString().ToUpper() == "SDBINM") &&
                    (!string.IsNullOrEmpty(SettingProperty.ActiveDatabaseName) && SettingProperty.ActiveDatabaseName.ToUpper() != "ACPERP") &&
                    this.BaseLicensePartBranchOfficeCode.ToUpper() != "SDBSPC" && this.BaseLicensePartBranchOfficeCode.ToUpper() != "INMSIS" && this.BaseLicensePartBranchOfficeCode.ToUpper() != "INMSIS" && this.BaseLicensePartBranchOfficeCode.ToUpper() != "SURA24")
                {
                    rtn = true;
                }
                rtn = false;
                return rtn;
            }
        }

        /// <summary>
        /// On 07/02/3034
        /// </summary>
        public DateTime GraceLockDateFrom
        {
            get
            {
                DateTime startDate = DateSet.ToDate(this.BookBeginFrom, false);
                return startDate;
            }
        }

        /// <summary>
        /// On 07/02/3034
        /// </summary>
        public DateTime GraceLockDateTo
        {
            get
            {
                //Take from local system, If acmeerp portal is not accessable otherwise take from portal date and time
                DateTime todaydate = DateSet.ToDate(DateTime.Now.ToShortDateString(), false);
                DateTime openfromDate = DateSet.ToDate(DateTime.Now.ToShortDateString(), false);
                if (AcmeerpPortalServerDateTime != DateTime.MinValue)
                {
                    todaydate = DateSet.ToDate(AcmeerpPortalServerDateTime.ToShortDateString(), false);
                }
                else if (!string.IsNullOrEmpty(RecentVoucherDate))
                {   //If Not able to get current datetime from acmeerp portal server, let us take recent voucher date as current date,
                    //past grace days allowed from recent voucehr date
                    todaydate = DateSet.ToDate(this.RecentVoucherDate, false);
                }
                else
                {
                    todaydate = DateSet.ToDate(this.YearFrom, false).AddDays(DEFAULT_GRACE_DAYS);
                }

                AcmeerpGraceTodayDateTime = todaydate;
                if (this.VoucherGraceDays > 0)
                {
                    openfromDate = DateSet.ToDate(todaydate.AddDays(-this.VoucherGraceDays).ToShortDateString(), false);
                }

                return openfromDate;
            }
        }

        /// <summary>
        /// On 23/01/2025 - voucher entry grace days details
        /// Enforce Mode - 0 - default (Others), 1- No, 2- Yes
        /// 0-Default - there no setting information, 1- dont enforce
        /// </summary>
        public Int32 VoucherEnforceGraceMode
        {
            get
            {
                Int32 voucherEnforceGraceMode = (int)VoucherEntryGraceDaysMode.None;
                if ((this.IS_SDB_INM || this.BaseLicenseHeadOfficeCode.ToString().ToUpper() == "SDBINM"))
                {
                    string value = GetSettingInfo(Setting.VoucherEnforceGraceMode.ToString());
                    ResultArgs result = CommonMethod.DecreptWithResultArg(value);
                    if (result.Success)
                    {
                        value = result.ReturnValue.ToString();
                    }
                    else if (!string.IsNullOrEmpty(value)) value = ((int)VoucherEntryGraceDaysMode.Yes).ToString();

                    voucherEnforceGraceMode = NumberSet.ToInteger(value);
                }

                return voucherEnforceGraceMode;
            }
        }

        /// <summary>
        /// 
        /// 23/01/2025, Number days allowed to entry vouchers

        /// For SDBINM : As of now fix 60 for temp.
        /// For Others : no restriction
        /// 
        /// </summary>
        /// <returns></returns>
        public Int32 VoucherGraceDays
        {
            get
            {
                Int32 voucherGraceDays = 0; // No restrictions
                if (VoucherEnforceGraceMode == (int)VoucherEntryGraceDaysMode.None)
                {
                    if ((this.IS_SDB_INM))
                    {
                        //for sdbinm, number of grace periods are 15days, 
                        //It wil be fixed in license key based on provinces, 
                        // Let us get it from Acmee.erp license key, based on on provice we can set default grace days
                        voucherGraceDays = DEFAULT_GRACE_DAYS;
                    }
                }
                else if (VoucherEnforceGraceMode == (int)VoucherEntryGraceDaysMode.Yes)
                {
                    string value = GetSettingInfo(Setting.VoucherGraceDays.ToString());
                    ResultArgs result = CommonMethod.DecreptWithResultArg(value);
                    if (result.Success)
                    {
                        value = result.ReturnValue.ToString();
                    }
                    else if (!string.IsNullOrEmpty(value)) value = "0";

                    voucherGraceDays = NumberSet.ToInteger(value);
                    if (voucherGraceDays == 0) voucherGraceDays = 1;
                }

                return voucherGraceDays;
            }
        }

        public string VoucherGraceTmpDateFrom
        {
            get
            {
                return voucherGraceTmpDateFrom;
            }
            set
            {
                voucherGraceTmpDateFrom = value;
            }
        }

        public string VoucherGraceTmpDateTo
        {
            get
            {
                return voucherGraceTmpDateTo;
            }
            set
            {
                voucherGraceTmpDateTo = value;
            }
        }

        public string VoucherGraceTmpValidUpto
        {
            get
            {
                return voucherGraceTmpValidUpTo;
            }
            set
            {
                voucherGraceTmpValidUpTo = value;
            }
        }

        public bool IsVoucherGraceTmpActive
        {
            get
            {
                return isvoucherGraceTmpActive;
            }
            set
            {
                isvoucherGraceTmpActive = value;
            }
        }

        public string GetParticularSettingInfo(string name)
        {
            return GetSettingInfo(name);
        }

        public string ThirdPartyIntegration
        {
            get
            {
                if (string.IsNullOrEmpty(thirdParty))
                {
                    thirdParty = GetSettingInfo(Setting.ThirdParty.ToString());
                }
                return thirdParty;
            }
            set
            {
                thirdParty = value;
            }
        }

        /// <summary>
        /// On 22/08/2020, get default ledger ids with condition format
        /// </summary>
        /// <returns></returns>
        public string GetDefaultLedgersNamesCondition
        {
            get
            {
                string Rtn = "'" + DefaultLedgers.Cash.ToString() + "','" + DefaultLedgers.FixedDeposit.ToString() + "','" + DefaultLedgers.CapitalFund.ToString() + "'" +
                             ",'" + SettingProperty.CGST_LEDGER + "','" + SettingProperty.SGST_LEDGER + "','" + SettingProperty.IGST_LEDGER + "'" +
                             ",'" + SettingProperty.TDS_ON_FD_INTEREST_LEDGER + "'";
                return Rtn;
            }
        }

        /// <summary>
        /// On 22/08/2020, get default ledger names with coma sep
        /// </summary>
        /// <returns></returns>
        public string GetDefaultLedgersNames
        {
            get
            {
                string Rtn = DefaultLedgers.Cash.ToString() + "," + DefaultLedgers.FixedDeposit.ToString() + "," + DefaultLedgers.CapitalFund.ToString() +
                             "," + SettingProperty.CGST_LEDGER + "," + SettingProperty.SGST_LEDGER + "," + SettingProperty.IGST_LEDGER +
                             "," + SettingProperty.TDS_ON_FD_INTEREST_LEDGER;
                return Rtn;
            }
        }

        /// <summary>
        /// On 22/08/2020, get default ledger ids with coma sep
        /// </summary>
        /// <returns></returns>
        public string GetDefaultLedgersIds
        {
            get
            {
                string Rtn = (int)DefaultLedgers.Cash + "," + (int)DefaultLedgers.FixedDeposit + "," + (int)DefaultLedgers.CapitalFund +
                             "," + SettingProperty.SGSTLedgerID + "," + SettingProperty.CGSTLedgerID + "," + SettingProperty.IGSTLedgerID +
                             "," + SettingProperty.tdsOnFDInterestLedgerId;
                return Rtn;
            }
        }

        /// <summary>
        /// On 10/07/2024, To get default genearl Ledger Details
        /// </summary>
        /// On 06/05/2025, To Remove Capital Fund Ledger for all OtherThanIndia,Multicurrency
        public string GetDefaultGeneralLedgersIds
        {
            get
            {

                //string Rtn = (int)DefaultLedgers.FixedDeposit + "," + (int)DefaultLedgers.CapitalFund + "," +
                //             SettingProperty.SGSTLedgerID + "," + SettingProperty.CGSTLedgerID + "," + SettingProperty.IGSTLedgerID + "," +
                //             SettingProperty.tdsOnFDInterestLedgerId;

                string Rtn = (int)DefaultLedgers.FixedDeposit + "," +
                             SettingProperty.SGSTLedgerID + "," + SettingProperty.CGSTLedgerID + "," + SettingProperty.IGSTLedgerID + "," +
                             SettingProperty.tdsOnFDInterestLedgerId;

                if (this.AllowMultiCurrency == 1)
                {
                    Rtn += "," + (int)DefaultLedgers.Cash;
                }
                return Rtn;
            }
        }

        /// <summary>
        /// On 30/07/2024, To form prefix of Voucher Iamge
        /// </summary>
        public string PrefixVoucherImageName
        {
            get
            {
                string rtn = this.PartBranchOfficeCode.ToUpper() + "_" + this.Location;
                return rtn;
            }
        }

        public bool CheckDefaultLedgerName(string LedgerName)
        {
            bool Rtn = true;
            string defaultledgers = GetDefaultLedgersNames;
            string[] arraydefaultledgers = defaultledgers.Split(',');
            int pos = Array.IndexOf(arraydefaultledgers, LedgerName);
            Rtn = (pos > -1);
            return Rtn;
        }

        public bool CheckDefaultLedgerId(Int32 LedgerId)
        {
            bool Rtn = true;
            string defaultledgerIds = GetDefaultLedgersIds;
            string[] arraydefaultledgerIds = defaultledgerIds.Split(',');
            int pos = Array.IndexOf(arraydefaultledgerIds, LedgerId);
            Rtn = (pos > -1);
            return Rtn;
        }

        /// <summary>
        /// On 09/08/2024, To Set Project Currency/Global Currency Setting 
        /// </summary>
        public void SetProjectCurrencySetting()
        {

        }

        /// <summary>
        /// On 10/07/2024, If other than india license key
        /// 
        /// It is used to lock few featues if other than india is selected
        /// 
        /// 1. GST related features (Finance Setting)
        /// 2. PAN details (ledger, legal entiry and donor)
        /// </summary>
        /// <returns></returns>
        public bool IsCountryOtherThanIndia
        {
            get
            {
                bool rnt = false;

                if (SettingProperty.Current.CountryInfo.ToUpper() != "INDIA")
                {
                    rnt = true;
                }

                //rnt = true;
                return rnt;
            }
        }


        /// <summary>
        /// 18/09/2024, To lock datamanagemnt featues in data utiliites for few license keys
        /// For SDBINM Surabi - They wanted to lock datamanagement features
        /// </summary>
        public bool IsLockedDataManagementFeatures
        {
            get
            {
                bool rnt = (SettingProperty.Current.PartBranchOfficeCode.ToUpper() == "SUBMZP" ||
                            SettingProperty.Current.BaseLicensePartBranchOfficeCode.ToUpper() == "SUBMZP");

                return rnt;
            }
        }

        //On To get Currency Exchange Rate
        public double CurrencyLiveExchangeRate(DateTime dDate, string fromCurrency, string toCurrency)
        {
            double rtnamt = 0;
            string stramt = string.Empty;
            try
            {
                string ApiKey = "cur_live_5x1s6GEzaqUrf90nRykqhOV0z3Xh2Htg81ckeqwy"; //alwar@dbcyelagiri.edu.in
                ApiKey = "cur_live_nNy1OBzmj0E2fMEtPkXQzAPhxxTx4t76ZJ74wWrF"; //chinna@boscoits.com
                ApiKey = "cur_live_oZwKZiSQzgnJCfd9xtcAPOHNuFx2HlhZyxm9TNqT"; //aadurai


                string date = "2023-" + DateTime.Today.Date.Month.ToString() + "-" + DateTime.Today.Date.Day.ToString();

                if (!string.IsNullOrEmpty(fromCurrency) && !string.IsNullOrEmpty(toCurrency))
                {
                    using (WebClient client = new WebClient())
                    {
                        string url = "https://freecurrencyapi.net/api/v2/latest?apikey=" + ApiKey + "&base_currency=" + fromCurrency;
                        //url = "https://freecurrencyapi.net/api/v2/historical?apikey=cur_live_5x1s6GEzaqUrf90nRykqhOV0z3Xh2Htg81ckeqwy&base_currency=" + fromCurrency +
                        //         "&date=" + date + "&currencies=" + toCurrency;
                        //string myJsonResponse = client.DownloadString(url); //USD
                        //Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

                        string myJsonResponse = client.DownloadString(url); //USD
                        dynamic dynCurrencyExchange = JsonConvert.DeserializeObject(myJsonResponse);

                        if (dynCurrencyExchange.data != null)
                        {
                            var dataExchangeRates = dynCurrencyExchange.data;
                            string datajson = Convert.ToString(dataExchangeRates);
                            //dynCurrencyExchange = JsonConvert.DeserializeObject(datajson);
                            //DataTable dt = ConvertJsonStringDatatble(dataExchangeRates);
                            foreach (var rate in dataExchangeRates)
                            {
                                if (rate != null && rate.Name != null && rate.Value != null)
                                {
                                    string strname = Convert.ToString(rate.Name);
                                    if (strname.ToUpper() == toCurrency.ToUpper())
                                    {
                                        stramt = Convert.ToString(rate.Value);
                                        rtnamt = NumberSet.ToDouble(stramt);
                                        rtnamt = Math.Round(rtnamt, 2);
                                        break;
                                    }
                                }
                            }
                        }

                        //string txt = RequestHelper.Historical(ApiKey, date, "USD", "KES");
                        //Data CurrencyFrom = JsonConvert.DeserializeObject<Data>(txt);
                        //JObject.Parse(getString).First.First["val"].ToObject<double>();
                        //var jsonobject = JsonConvert.DeserializeObject<dynamic>(getString);
                        //DataTable dt = ConvertJsonStringDatatble(jsonobject);
                        //DataTable dt =  ConvertJsonStringDatatble(getString);
                        //Root R1 = JsonConvert.DeserializeObject<Root>(getString);
                        //Data D1 = JsonConvert.DeserializeObject<Data>(getString);

                        /*if (jdata.Count > 0)
                        {
                            foreach (KeyValuePair<string, JToken> property in jdata)
                            {
                                string key = property.Key;
                                string value = property.Value;
                            }

                        }*/
                    }
                }
            }
            catch (Exception err)
            {
                AcMELog.WriteLog("Could not get live exchange Rate : " + err.Message);
                //MessageRender.ShowMessage(err.Message);
            }

            //AcMELog.WriteLog("Could not get live exchange Rate : ");
            return rtnamt;
        }


        //On To get Currency Exchange Rate
        public double CurrencyExchangeRate1(string fromCurrency, string toCurrency)
        {
            double rtnamt = 0;
            string stramt = string.Empty;
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("https://www.ecb.int/stats/eurofxref/eurofxref-daily.xml");

                foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes[2].ChildNodes[0].ChildNodes)
                {
                    //ExchangeRateToEuro.Add(node.Attributes["currency"].Value, decimal.Parse(node.Attributes["rate"].Value));
                    //FromCurrency.Properties.Items.Add(node.Attributes["currency"].Value);
                    //ToCurrency.Properties.Items.Add(node.Attributes["currency"].Value);
                }
            }
            catch (Exception err)
            {
                //MessageRender.ShowMessage(err.Message);
            }

            return rtnamt;
        }

        /// <summary>
        /// Convert Json string Datatable
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable ConvertJsonStringDatatble(string json)
        {
            var jsonLinq = JObject.Parse(json);

            // Find the first array using Linq
            var SourceDataArray = jsonLinq.Descendants().Where(d => d is JArray).First();
            var trgArray = new JArray();
            foreach (JObject row in SourceDataArray.Children<JObject>())
            {
                var NewObjectClean = new JObject();
                foreach (JProperty column in row.Properties())
                {
                    // Only include JValue types
                    if (column.Value is JValue)
                    {
                        NewObjectClean.Add(column.Name, column.Value);
                    }
                }
                trgArray.Add(NewObjectClean);
            }
            return JsonConvert.DeserializeObject<DataTable>(trgArray.ToString());
        }

        /// <summary>
        /// On 29/01/2025 - to valudate grace tmp date details 
        /// </summary>
        public void ValidateVoucherGraceTmpDetails()
        {
            //To invoke, fill grace details before validate tmp grace details 
            DateTime gracedateto = this.GraceLockDateTo;

            //If Temp date range is crossed valid upto let us clear it --------------------------------------------------------------------
            isvoucherGraceTmpActive = false;
            voucherGraceTmpDateFrom = voucherGraceTmpDateTo = voucherGraceTmpValidUpTo = string.Empty;

            string value = GetSettingInfo(Setting.VoucherGraceTmpDateFrom.ToString());
            ResultArgs result = CommonMethod.DecreptWithResultArg(value);
            if (result.Success)
            {
                voucherGraceTmpDateFrom = result.ReturnValue.ToString();
            }

            value = GetSettingInfo(Setting.VoucherGraceTmpDateTo.ToString());
            result = CommonMethod.DecreptWithResultArg(value);
            if (result.Success)
            {
                voucherGraceTmpDateTo = result.ReturnValue.ToString();
            }

            value = GetSettingInfo(Setting.VoucherGraceTmpValidUpTo.ToString());
            result = CommonMethod.DecreptWithResultArg(value);
            if (result.Success)
            {
                voucherGraceTmpValidUpTo = result.ReturnValue.ToString();
            }

            //voucherGraceTmpDateFrom = "01/05/2024 00:00:00";
            //voucherGraceTmpDateTo = "31/05/2024 00:00:00";
            //voucherGraceTmpValidUpTo = "30/01/2025 00:00:00";
            if (!string.IsNullOrEmpty(voucherGraceTmpDateFrom) && !string.IsNullOrEmpty(voucherGraceTmpDateTo) && !string.IsNullOrEmpty(voucherGraceTmpValidUpTo) &&
                 AcmeerpGraceTodayDateTime != DateTime.MinValue)
            {
                //If tmp date is expired, let us clear it.
                if (AcmeerpGraceTodayDateTime > DateSet.ToDate(voucherGraceTmpValidUpTo, false))
                {
                    isvoucherGraceTmpActive = false;
                    //voucherGraceTmpDateFrom = voucherGraceTmpDateTo = VoucherGraceTmpValidUpto= string.Empty;
                }
                else
                {
                    isvoucherGraceTmpActive = true;
                }
            }
            //-------------------------------------------------------------------------------------------------------------------------------
        }

        /// <summary>
        /// On 27/01/2025 - To check Temporary relaxation is applied
        /// </summary>
        /// <param name="dDate"></param>
        /// <returns></returns>
        public bool IsTemporaryGraceLockRelaxDate(DateTime dDate)
        {
            //Check temporary relaxation  ------------------------------------------------------------------
            bool isEnforceTmpRelaxation = false;
            try
            {
                if (IsVoucherGraceTmpActive && (!string.IsNullOrEmpty(VoucherGraceTmpDateFrom) && !string.IsNullOrEmpty(VoucherGraceTmpDateTo)
                        && !string.IsNullOrEmpty(VoucherGraceTmpValidUpto)))
                {
                    if (DateSet.ToDate(dDate.ToShortDateString(), false) >= DateSet.ToDate(this.VoucherGraceTmpDateFrom, false) &&
                        DateSet.ToDate(dDate.ToShortDateString(), false) <= DateSet.ToDate(this.VoucherGraceTmpDateTo, false))
                    {
                        isEnforceTmpRelaxation = true;
                    }
                }
            }
            catch (Exception err)
            {
                isEnforceTmpRelaxation = false;
                AcMELog.WriteLog("Could check IsTemporaryGraceLockRelaxation : " + err.Message);
                //MessageRender.ShowMessage(err.Message);
            }
            //--------------------------------------------------------------------------------------------
            return isEnforceTmpRelaxation;
        }

        #region IDisposable Members

        public override void Dispose()
        {
            //GC.Collect();
        }

        #endregion

        #region Networking Settings
        //public string ServerName
        //{
        //    get
        //    {
        //        return GetSettingInfo(NetworkingSetting.ServerName.ToString());
        //    }
        //}
        //public string Port
        //{
        //    get
        //    {
        //        return GetSettingInfo(NetworkingSetting.Port.ToString());
        //    }
        //}
        //public string SMTPUserName
        //{
        //    get
        //    {
        //        return GetSettingInfo(NetworkingSetting.SMTPUsername.ToString());
        //    }
        //}
        //public string SMTPPassword
        //{
        //    get
        //    {
        //        return GetSettingInfo(NetworkingSetting.SMTPPassword.ToString());
        //    }
        //}
        public string ThanksGivingSubject
        {
            get
            {
                return GetSettingInfo(NetworkingSetting.ThanksGivingSubject.ToString());
            }
        }
        public string AppealSubject
        {
            get
            {
                return GetSettingInfo(NetworkingSetting.AppealSubject.ToString());
            }
        }
        public string WeddingdaySubject
        {
            get
            {
                return GetSettingInfo(NetworkingSetting.WeddingdaySubject.ToString());
            }
        }
        public string BirthdaySubject
        {
            get
            {
                return GetSettingInfo(NetworkingSetting.BirthdaySubject.ToString());
            }
        }

        public string ModuleRightsDetails
        {
            get { return modulerightsdetails; }
            set { modulerightsdetails = value; }
        }

        //public string SMSUserName
        //{
        //    get
        //    {
        //        return GetSettingInfo(NetworkingSetting.SMSUserName.ToString());
        //    }
        //}
        //public string SMSPassword
        //{
        //    get
        //    {
        //        return GetSettingInfo(NetworkingSetting.SMSPassKey.ToString());
        //    }
        //}
        //public string SenderId
        //{
        //    get
        //    {
        //        return GetSettingInfo(NetworkingSetting.SenderId.ToString());
        //    }
        //}
        #endregion
    }

    public class RequestHelper
    {
        public const string BaseUrl = "https://freecurrencyapi.net/api/v2/"; //https://api.freecurrencyapi.com/v1/";

        public RequestHelper()
        {
        }

        public static string Status(string apiKey = null)
        {
            string url;
            url = BaseUrl + "/status?apikey=" + apiKey;

            return GetResponse(url);
        }

        public static string Currencies(string apiKey, string currencies)
        {
            string url;
            url = BaseUrl + "/currencies?currencies=" + currencies + "&apikey=" + apiKey;

            return GetResponse(url);
        }

        public static string Latest(string apiKey, string baseCurrency, string currencies)
        {
            string url;
            url = BaseUrl + "/latest?currencies=" + currencies + "&base_currency=" + baseCurrency + "&apikey=" + apiKey;

            return GetResponse(url);
        }

        public static string Historical(string apiKey, string date, string baseCurrency, string currencies)
        {
            string url;
            url = BaseUrl + "/historical?currencies=" + currencies + "&base_currency=" + baseCurrency + "&date=" + date + "&apikey=" + apiKey;

            return GetResponse(url);
        }

        private static string GetResponse(string url)
        {
            string jsonString;

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    jsonString = reader.ReadToEnd();
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        jsonString = reader.ReadToEnd();
                    }
                }
            }


            return jsonString;
        }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Data
    {
        public double ADA { get; set; }
        public double AED { get; set; }
        public double AFN { get; set; }
        public double ALL { get; set; }
        public double AMD { get; set; }
        public double ANG { get; set; }
        public double AOA { get; set; }
        public double ARB { get; set; }
        public double ARS { get; set; }
        public double AUD { get; set; }
        public double AVAX { get; set; }
        public double AWG { get; set; }
        public double AZN { get; set; }
        public double BAM { get; set; }
        public int BBD { get; set; }
        public double BDT { get; set; }
        public double BGN { get; set; }
        public double BHD { get; set; }
        public double BIF { get; set; }
        public int BMD { get; set; }
        public double BNB { get; set; }
        public double BND { get; set; }
        public double BOB { get; set; }
        public double BRL { get; set; }
        public int BSD { get; set; }
        public double BTC { get; set; }
        public double BTN { get; set; }
        public double BWP { get; set; }
        public double BYN { get; set; }
        public double BYR { get; set; }
        public int BZD { get; set; }
        public double CAD { get; set; }
        public double CDF { get; set; }
        public double CHF { get; set; }
        public double CLF { get; set; }
        public double CLP { get; set; }
        public double CNY { get; set; }
        public double COP { get; set; }
        public double CRC { get; set; }
        public int CUC { get; set; }
        public int CUP { get; set; }
        public double CVE { get; set; }
        public double CZK { get; set; }
        public double DAI { get; set; }
        public double DJF { get; set; }
        public double DKK { get; set; }
        public double DOP { get; set; }
        public double DOT { get; set; }
        public double DZD { get; set; }
        public double EGP { get; set; }
        public int ERN { get; set; }
        public double ETB { get; set; }
        public double ETH { get; set; }
        public double EUR { get; set; }
        public double FJD { get; set; }
        public double FKP { get; set; }
        public double GBP { get; set; }
        public double GEL { get; set; }
        public double GGP { get; set; }
        public double GHS { get; set; }
        public double GIP { get; set; }
        public double GMD { get; set; }
        public double GNF { get; set; }
        public double GTQ { get; set; }
        public double GYD { get; set; }
        public double HKD { get; set; }
        public double HNL { get; set; }
        public double HRK { get; set; }
        public double HTG { get; set; }
        public double HUF { get; set; }
        public double IDR { get; set; }
        public double ILS { get; set; }
        public double IMP { get; set; }
        public double INR { get; set; }
        public double IQD { get; set; }
        public double IRR { get; set; }
        public double ISK { get; set; }
        public double JEP { get; set; }
        public double JMD { get; set; }
        public double JOD { get; set; }
        public double JPY { get; set; }
        public double KES { get; set; }
        public double KGS { get; set; }
        public double KHR { get; set; }
        public double KMF { get; set; }
        public double KPW { get; set; }
        public double KRW { get; set; }
        public double KWD { get; set; }
        public double KYD { get; set; }
        public double KZT { get; set; }
        public double LAK { get; set; }
        public double LBP { get; set; }
        public double LKR { get; set; }
        public double LRD { get; set; }
        public double LSL { get; set; }
        public double LTC { get; set; }
        public double LTL { get; set; }
        public double LVL { get; set; }
        public double LYD { get; set; }
        public double MAD { get; set; }
        public double MATIC { get; set; }
        public double MDL { get; set; }
        public double MGA { get; set; }
        public double MKD { get; set; }
        public double MMK { get; set; }
        public double MNT { get; set; }
        public double MOP { get; set; }
        public double MRO { get; set; }
        public double MRU { get; set; }
        public double MUR { get; set; }
        public double MVR { get; set; }
        public double MWK { get; set; }
        public double MXN { get; set; }
        public double MYR { get; set; }
        public double MZN { get; set; }
        public double NAD { get; set; }
        public double NGN { get; set; }
        public double NIO { get; set; }
        public double NOK { get; set; }
        public double NPR { get; set; }
        public double NZD { get; set; }
        public double OMR { get; set; }
        public double OP { get; set; }
        public double PAB { get; set; }
        public double PEN { get; set; }
        public double PGK { get; set; }
        public double PHP { get; set; }
        public double PKR { get; set; }
        public double PLN { get; set; }
        public double PYG { get; set; }
        public double QAR { get; set; }
        public double RON { get; set; }
        public double RSD { get; set; }
        public double RUB { get; set; }
        public double RWF { get; set; }
        public double SAR { get; set; }
        public double SBD { get; set; }
        public double SCR { get; set; }
        public double SDG { get; set; }
        public double SEK { get; set; }
        public double SGD { get; set; }
        public double SHP { get; set; }
        public double SLL { get; set; }
        public double SOL { get; set; }
        public double SOS { get; set; }
        public double SRD { get; set; }
        public double STD { get; set; }
        public double STN { get; set; }
        public double SVC { get; set; }
        public double SYP { get; set; }
        public double SZL { get; set; }
        public double THB { get; set; }
        public double TJS { get; set; }
        public double TMT { get; set; }
        public double TND { get; set; }
        public double TOP { get; set; }
        public double TRY { get; set; }
        public double TTD { get; set; }
        public double TWD { get; set; }
        public double TZS { get; set; }
        public double UAH { get; set; }
        public double UGX { get; set; }
        public int USD { get; set; }
        public double USDC { get; set; }
        public double USDT { get; set; }
        public double UYU { get; set; }
        public double UZS { get; set; }
        public double VEF { get; set; }
        public double VES { get; set; }
        public double VND { get; set; }
        public double VUV { get; set; }
        public double WST { get; set; }
        public double XAF { get; set; }
        public double XAG { get; set; }
        public double XAU { get; set; }
        public double XCD { get; set; }
        public double XDR { get; set; }
        public double XOF { get; set; }
        public double XPD { get; set; }
        public double XPF { get; set; }
        public double XPT { get; set; }
        public double XRP { get; set; }
        public double YER { get; set; }
        public double ZAR { get; set; }
        public double ZMK { get; set; }
        public double ZMW { get; set; }
        public double ZWL { get; set; }
    }

    public class Query
    {
        public string apikey { get; set; }
        public int timestamp { get; set; }
        public string base_currency { get; set; }
    }

    public class Root
    {
        public Query query { get; set; }
        public Data data { get; set; }
    }
}
