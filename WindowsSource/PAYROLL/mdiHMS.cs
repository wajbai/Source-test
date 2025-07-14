using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Data;
using Bosco.Utility;
using Bosco.Utility.Common;
using PAYROLL.Modules;
using Payroll.Model.UIModel;
using PAYROLL.Modules.Payroll;

namespace PAYROLL
{
    public class mdiHMS : System.Windows.Forms.Form
    {
        public System.Windows.Forms.Panel picMain;
        public System.Windows.Forms.MainMenu MainMenu1;
        public System.Windows.Forms.ToolTip ToolTip1;
        private System.Windows.Forms.MenuItem mnuRegistration;
        private System.Windows.Forms.MenuItem mnuBeds;
        private System.Windows.Forms.MenuItem mnuSplit1;
        private System.Windows.Forms.MenuItem mnuPrinterSettings;
        private System.Windows.Forms.MenuItem mnuPharmacy;
        private System.Windows.Forms.MenuItem mnuStore;
        private System.Windows.Forms.MenuItem mnuSplit2;
        private System.Windows.Forms.MenuItem mnuBilling;
        private System.Windows.Forms.MenuItem mnuLaboratory;
        private System.Windows.Forms.MenuItem mnuRecording;
        private System.Windows.Forms.MenuItem mnuWardDept;
        private System.Windows.Forms.MenuItem mnuAdmin;
        private System.Windows.Forms.MenuItem mnuSplit3;
        private System.Windows.Forms.MenuItem mnuIndent;
        private System.Windows.Forms.MenuItem mnuReceiveMedicines;
        private System.Windows.Forms.MenuItem mnuIssueMedicines;
        private System.Windows.Forms.MenuItem mnuReplaceMedicines;
        private System.Windows.Forms.MenuItem mnuStockVerification;
        private System.Windows.Forms.MenuItem mnuS_Indent;
        private System.Windows.Forms.MenuItem mnuS_ReceiveMedicines;
        private System.Windows.Forms.MenuItem mnuS_IssueMedicines;
        private System.Windows.Forms.MenuItem mnuDamages;
        private System.Windows.Forms.MenuItem mnuDeptDamages;
        private System.Windows.Forms.MenuItem mnuInterDept;
        private System.Windows.Forms.MenuItem mnuS_Item;
        private System.Windows.Forms.MenuItem mnuS_Vendor;

        //Modified : JV on 01-09-2005 for Pharmacy Transaction History
        private System.Windows.Forms.MenuItem mnuPharmacyTransHistory;
        //------------------
        //Modified : JV on 12-09-2005 for Issue/Return drug to/from Ward-Patient
        private System.Windows.Forms.MenuItem mnuIssueDrugToWardPatient;
        private System.Windows.Forms.MenuItem mnuReturnDrugIssuedToWardPatient;
        private System.Windows.Forms.MenuItem mnuIssuedrugPharmacyToDept;
        //------------------
        private System.Windows.Forms.MenuItem mnuMaterials;
        private System.Windows.Forms.MenuItem mnuPeople;
        private System.Windows.Forms.MenuItem mnuChangePassword;
        private System.Windows.Forms.MenuItem mnuWard;
        private System.Windows.Forms.MenuItem mnuSettings;
        private System.Windows.Forms.MenuItem mnuSupportData;
        private System.Windows.Forms.MenuItem mnuExit;
        private System.Windows.Forms.MenuItem mnuS_ToVendor;
        private System.Windows.Forms.MenuItem mnuIndentList;
        private System.Windows.Forms.MenuItem mnuPrepareIndent;
        private System.Windows.Forms.MenuItem mnuReturnByPatient;
        private System.Windows.Forms.MenuItem mnuIssueToPatients;
        private System.Windows.Forms.MenuItem mnuStorePhrEntry;
        private System.Windows.Forms.MenuItem mnuDoctorConcession;
        private System.Windows.Forms.MenuItem mnuIssueToDepartments;
        private System.Windows.Forms.MenuItem mnuReturnToStore;
        private System.Windows.Forms.MenuItem mnuRepMedFromStore;
        private System.Windows.Forms.MenuItem mnuStockVerifyList;
        private System.Windows.Forms.MenuItem mnuVerifyStock;
        private System.Windows.Forms.MenuItem mnuViewStockStatus;
        private System.Windows.Forms.MenuItem mnuS_IndentList;
        private System.Windows.Forms.MenuItem mnuS_PrepareIndent;
        private System.Windows.Forms.MenuItem mnuS_RecMedFromVendors;
        private System.Windows.Forms.MenuItem mnuS_RecMedAsOffer;
        private System.Windows.Forms.MenuItem mnuS_IssMedToDepartment;
        private System.Windows.Forms.MenuItem mnuS_ReturnMedicines;
        //private System.Windows.Forms.MenuItem mnuS_ReplaceMedicines;
        private System.Windows.Forms.MenuItem mnuS_MaterialList;
        private System.Windows.Forms.MenuItem mnuS_MaterialStockLevel;
        private System.Windows.Forms.MenuItem mnuDoctors;
        private System.Windows.Forms.MenuItem mnuUsers;
        private System.Windows.Forms.MenuItem mnuVendors;
        private System.Windows.Forms.MenuItem mnuDepartments;
        private System.Windows.Forms.MenuItem mnuCases;
        private System.Windows.Forms.MenuItem mnuLabs;
        private System.Windows.Forms.MenuItem mnuLabTest;
        private System.Windows.Forms.MenuItem mnuPaymentTypes;
        private System.Windows.Forms.MenuItem mnuPatientTypes;
        private System.Windows.Forms.MenuItem mnuDoctorDegree;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.MenuItem mnuPlacingOrderToVendor;
        private System.Windows.Forms.MenuItem mnuOrder;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem mnuStoreStockVerification;
        private System.Windows.Forms.MenuItem mnuUpdateSellingPrice;
        private System.Windows.Forms.MenuItem mnuUpdatePharmacySP;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem mnuS_RecMedFromDepartments;
        private System.Windows.Forms.MenuItem mnuAdminSubItem;
        private System.Windows.Forms.MenuItem mnuAdminRoles;
        private System.Windows.Forms.MenuItem mnuAdminAccounts;
        private System.Windows.Forms.MenuItem mnuAdminAccountGroup;
        private System.Windows.Forms.MenuItem mnuAdminAccountHead;
        private System.Windows.Forms.MenuItem mnuAdRegistration;
        private System.Windows.Forms.MenuItem mnuAdRegIP;
        private System.Windows.Forms.MenuItem mnuAdRegOP;
        private System.Windows.Forms.MenuItem mnuAdRegBedRes;
        private System.Windows.Forms.MenuItem mnuBillGroup;
        private System.ComponentModel.IContainer components;

        //private clsAccount objRights = new clsAccount();


        private int iRegisModuId = 0;
        private int iRegisDeptId = 0;
        private DataTable dtRegisters;
        //private clsStore objStore= new clsStore(0);

        private string[,] strStores;
        private int iNoOfStores = 0;
        private int iStoreId = 0;
        //private int iRepModuleId = 0;		 //Commented by PE to avoid warning 

        private string[,] strPharmacies;
        private int iNoOfPharmacies = 0;
        private int iPharmacyId = 0;

        /* -----------------------------------------------------
         * Name		: Joseph Gerald J.
         * Date		: 30-Oct-2005 09:45 AM
         * Purpose	: To Build the Billing Menu dynamically 
         *			  based on the No. of Billing Sections
         * */

        private string[,] strBillingSections;
        private int iNoOfBillingSections = 0;
        private int iBillingSectionId = 0;

        // -----------------------------------------------------

        private string[,] strLabs;
        private int iNoOfLabs = 0;
        private int iLabId = 0;

        private string[,] strRecording;
        private int iNoOfRecording = 0;
        private int iRecordingId = 0;

        private string[,] strWards;
        private int iNoOfWards = 0;
        private int iWardId = 0;

        // introduced to add Report. by - ntanand
        private int iRptModule = 0;
        private int iRptDeptId = 0;
        private string[,] strIPRegiReports;
        private int iNoOfIPRegiReports;

        private string[,] strOPRegiReports;
        private int iNoOfOPRegiReports;

        private string[,] strStoreReports;
        private int iNoOfStoreReports;

        private string[,] strPharmacyReports;
        private int iNoOfPharmacyReports;

        private string[,] strLabReports;
        private int iNoOfLabReports;

        private string[,] strBillingReports;
        private int iNoOfBillingReports;

        private string[,] strRecordingReports;
        private int iNoOfRecordingReports;

        private clsGeneral objGeneral = new clsGeneral();
        private clsPrGateWay objPRGW =new clsPrGateWay();
        //private ClsReportMapping objReptMapping=new ClsReportMapping();
        //private DataHandling dh = new DataHandling();

        private DataView dvUserDept = null;
        private bool bAddSplitMenu = false;
        // end  report declaration

        private int iUserDepartmentId = 0;
        private string iUserDepartmentType = "";

        private System.Windows.Forms.MenuItem mnuRegSetting;
        private System.Windows.Forms.MenuItem mnuPharSetting;
        private System.Windows.Forms.MenuItem mnuStoreSetting;
        private System.Windows.Forms.MenuItem mnuHospitalDepartment;
        private System.Windows.Forms.MenuItem mnuAmenities;
        private System.Windows.Forms.MenuItem mnuLabSetting;
        private System.Windows.Forms.MenuItem mnuWardSetting;
        private System.Windows.Forms.MenuItem mnuDayReport;
        private System.Windows.Forms.MenuItem mnuReOrderLevel;
        private System.Windows.Forms.MenuItem mnuItemGroup;
        private System.Windows.Forms.MenuItem mnuItemMeasure;
        private System.Windows.Forms.MenuItem mnuGenericItem;
        private System.Windows.Forms.MenuItem mnuManufacture;
        private System.Windows.Forms.MenuItem mnuProcessDayRpt;
        private System.Windows.Forms.MenuItem mnuReleaseWardBill;
        private System.Windows.Forms.MenuItem mnuChangePrintStatus;
        private System.Windows.Forms.MenuItem mnuReport;
        private System.Windows.Forms.MenuItem mnuDataCleaning;
        private System.Windows.Forms.MenuItem mnuDataRestoring;
        private System.Windows.Forms.MenuItem mnuReportItem;
        private System.Windows.Forms.MenuItem mnuReportMoulde;
        private System.Windows.Forms.MenuItem mnuRptRegistration;
        private System.Windows.Forms.MenuItem mnuRptStroe;
        private System.Windows.Forms.MenuItem mnuRptPharmacy;
        private System.Windows.Forms.MenuItem mnuRptLab;
        private System.Windows.Forms.MenuItem mnuRptBilling;
        private System.Windows.Forms.MenuItem mnuRptRecording;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem menuItem10;
        private System.Windows.Forms.MenuItem mnuOPRegRegister;
        private System.Windows.Forms.MenuItem mnuIPRegRegister;
        private System.Windows.Forms.MenuItem mnuPharmacyRegister;
        private System.Windows.Forms.MenuItem mnuBillingRegister;
        private System.Windows.Forms.MenuItem mnuLabRegister;
        private System.Windows.Forms.MenuItem mnuStoreRegister;
        private System.Windows.Forms.MenuItem mnuOPCensus;
        private System.Windows.Forms.MenuItem mnuIPCensus;
        private System.Windows.Forms.MenuItem mnuBirthRegister;
        private System.Windows.Forms.MenuItem mnuDeathRegister;
        private System.Windows.Forms.MenuItem mnuPharmacyStockRegister;
        private System.Windows.Forms.MenuItem mnuStorePurchaseRegister;
        private System.Windows.Forms.MenuItem mnuStoreStockRegister;
        private System.Windows.Forms.MenuItem mnuRegDailySales;
        private System.Windows.Forms.MenuItem mnuRegPurcAccount;
        public System.Windows.Forms.Label lblHospitalName;
        private int iMenuIndex = 0;
        private MenuStrip mnuMain;
        private ToolStripMenuItem homeToolStripMenuItem;
        private ToolStripMenuItem loginToolStripMenuItem;
        private ToolStripMenuItem payRollToolStripMenuItem;
        private ToolStripMenuItem logoutToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private Panel panel1;
        private DataTable dtJobs = null;

        //[STAThread]
        //static void Main() 
        //{
        //    //Application.CurrentCulture = new System.Globalization.CultureInfo("en-GB", false);
        //    //MessageBox.Show(Application.CurrentCulture.Name + ", " +  Application.CurrentCulture.DateTimeFormat.FullDateTimePattern); 
        //    Application.Run(new frmLogin());
        //}

        public mdiHMS()
        {
            InitializeComponent();
        }

        public mdiHMS(string strUsername)
        {
            this.strUsername = strUsername;
            InitializeComponent();
            ////objStore.getAvailableDepartments();	
            clsGeneral.USER_NAME = strUsername;
           // clsGeneral.getUserDetails(strUsername);
            clsGeneral.USER_ID = clsGeneral.iUserId;
            clsGeneral.USER_LEVEL = clsGeneral.UserLevel;
            iUserDepartmentId = clsGeneral.iDepartmentId;
            iUserDepartmentType = clsGeneral.sDepartmentTypeId;
            //dvUserDept = clsGeneral.getUserDepartments(strUsername);
            //buildFileMenu();
            //if(clsGeneral.ShowReportMenu(clsGeneral.USER_NAME))
            //    buildReportMenu();
            //if(clsGeneral.ShowRegisterMenu(clsGeneral.USER_NAME))
            //{
            //    dtRegisters = new clsGeneral().getRegisters();
            //    buildRegisterMenu();
            //}
            //dtJobs = new clsGeneral().getJobs();
            BuildPayrollMenu();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mdiHMS));
            this.mnuDayReport = new System.Windows.Forms.MenuItem();
            this.MainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.mnuOPRegRegister = new System.Windows.Forms.MenuItem();
            this.mnuOPCensus = new System.Windows.Forms.MenuItem();
            this.mnuIPRegRegister = new System.Windows.Forms.MenuItem();
            this.mnuIPCensus = new System.Windows.Forms.MenuItem();
            this.mnuBirthRegister = new System.Windows.Forms.MenuItem();
            this.mnuDeathRegister = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.mnuPharmacyRegister = new System.Windows.Forms.MenuItem();
            this.mnuPharmacyStockRegister = new System.Windows.Forms.MenuItem();
            this.mnuRegDailySales = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.mnuStoreRegister = new System.Windows.Forms.MenuItem();
            this.mnuStorePurchaseRegister = new System.Windows.Forms.MenuItem();
            this.mnuStoreStockRegister = new System.Windows.Forms.MenuItem();
            this.mnuRegPurcAccount = new System.Windows.Forms.MenuItem();
            this.mnuReportItem = new System.Windows.Forms.MenuItem();
            this.mnuRegistration = new System.Windows.Forms.MenuItem();
            this.mnuBeds = new System.Windows.Forms.MenuItem();
            this.mnuSplit1 = new System.Windows.Forms.MenuItem();
            this.mnuSplit2 = new System.Windows.Forms.MenuItem();
            this.mnuBilling = new System.Windows.Forms.MenuItem();
            this.mnuAdmin = new System.Windows.Forms.MenuItem();
            this.mnuPeople = new System.Windows.Forms.MenuItem();
            this.mnuChangePassword = new System.Windows.Forms.MenuItem();
            this.mnuDoctors = new System.Windows.Forms.MenuItem();
            this.mnuUsers = new System.Windows.Forms.MenuItem();
            this.mnuVendors = new System.Windows.Forms.MenuItem();
            this.mnuWard = new System.Windows.Forms.MenuItem();
            this.mnuSettings = new System.Windows.Forms.MenuItem();
            this.mnuRegSetting = new System.Windows.Forms.MenuItem();
            this.mnuPharSetting = new System.Windows.Forms.MenuItem();
            this.mnuWardSetting = new System.Windows.Forms.MenuItem();
            this.mnuLabSetting = new System.Windows.Forms.MenuItem();
            this.mnuStoreSetting = new System.Windows.Forms.MenuItem();
            this.mnuReOrderLevel = new System.Windows.Forms.MenuItem();
            this.mnuSupportData = new System.Windows.Forms.MenuItem();
            this.mnuDepartments = new System.Windows.Forms.MenuItem();
            this.mnuCases = new System.Windows.Forms.MenuItem();
            this.mnuLabs = new System.Windows.Forms.MenuItem();
            this.mnuLabTest = new System.Windows.Forms.MenuItem();
            this.mnuPaymentTypes = new System.Windows.Forms.MenuItem();
            this.mnuPatientTypes = new System.Windows.Forms.MenuItem();
            this.mnuDoctorDegree = new System.Windows.Forms.MenuItem();
            this.mnuAmenities = new System.Windows.Forms.MenuItem();
            this.mnuHospitalDepartment = new System.Windows.Forms.MenuItem();
            this.mnuAdminSubItem = new System.Windows.Forms.MenuItem();
            this.mnuItemGroup = new System.Windows.Forms.MenuItem();
            this.mnuItemMeasure = new System.Windows.Forms.MenuItem();
            this.mnuManufacture = new System.Windows.Forms.MenuItem();
            this.mnuAdminRoles = new System.Windows.Forms.MenuItem();
            this.mnuAdminAccounts = new System.Windows.Forms.MenuItem();
            this.mnuAdminAccountGroup = new System.Windows.Forms.MenuItem();
            this.mnuAdminAccountHead = new System.Windows.Forms.MenuItem();
            this.mnuAdRegistration = new System.Windows.Forms.MenuItem();
            this.mnuAdRegIP = new System.Windows.Forms.MenuItem();
            this.mnuAdRegOP = new System.Windows.Forms.MenuItem();
            this.mnuAdRegBedRes = new System.Windows.Forms.MenuItem();
            this.mnuReleaseWardBill = new System.Windows.Forms.MenuItem();
            this.mnuProcessDayRpt = new System.Windows.Forms.MenuItem();
            this.mnuSplit3 = new System.Windows.Forms.MenuItem();
            this.mnuExit = new System.Windows.Forms.MenuItem();
            this.mnuPharmacy = new System.Windows.Forms.MenuItem();
            this.mnuIssueMedicines = new System.Windows.Forms.MenuItem();
            this.mnuIssueToPatients = new System.Windows.Forms.MenuItem();
            this.mnuIssueToDepartments = new System.Windows.Forms.MenuItem();
            this.mnuReturnToStore = new System.Windows.Forms.MenuItem();
            this.mnuReceiveMedicines = new System.Windows.Forms.MenuItem();
            this.mnuReturnByPatient = new System.Windows.Forms.MenuItem();
            this.mnuReplaceMedicines = new System.Windows.Forms.MenuItem();
            this.mnuRepMedFromStore = new System.Windows.Forms.MenuItem();
            this.mnuIndent = new System.Windows.Forms.MenuItem();
            this.mnuIndentList = new System.Windows.Forms.MenuItem();
            this.mnuPrepareIndent = new System.Windows.Forms.MenuItem();
            this.mnuStockVerification = new System.Windows.Forms.MenuItem();
            this.mnuStockVerifyList = new System.Windows.Forms.MenuItem();
            this.mnuVerifyStock = new System.Windows.Forms.MenuItem();
            this.mnuViewStockStatus = new System.Windows.Forms.MenuItem();
            this.mnuStore = new System.Windows.Forms.MenuItem();
            this.mnuS_IssueMedicines = new System.Windows.Forms.MenuItem();
            this.mnuS_IssMedToDepartment = new System.Windows.Forms.MenuItem();
            this.mnuS_ReceiveMedicines = new System.Windows.Forms.MenuItem();
            this.mnuS_RecMedFromDepartments = new System.Windows.Forms.MenuItem();
            this.mnuS_RecMedAsOffer = new System.Windows.Forms.MenuItem();
            this.mnuDamages = new System.Windows.Forms.MenuItem();
            this.mnuS_Indent = new System.Windows.Forms.MenuItem();
            this.mnuS_IndentList = new System.Windows.Forms.MenuItem();
            this.mnuS_PrepareIndent = new System.Windows.Forms.MenuItem();
            this.mnuOrder = new System.Windows.Forms.MenuItem();
            this.mnuPlacingOrderToVendor = new System.Windows.Forms.MenuItem();
            this.mnuS_RecMedFromVendors = new System.Windows.Forms.MenuItem();
            this.mnuS_ToVendor = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.mnuS_ReturnMedicines = new System.Windows.Forms.MenuItem();
            this.mnuMaterials = new System.Windows.Forms.MenuItem();
            this.mnuS_MaterialList = new System.Windows.Forms.MenuItem();
            this.mnuS_MaterialStockLevel = new System.Windows.Forms.MenuItem();
            this.mnuStoreStockVerification = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.mnuReport = new System.Windows.Forms.MenuItem();
            this.mnuReportMoulde = new System.Windows.Forms.MenuItem();
            this.mnuRptRegistration = new System.Windows.Forms.MenuItem();
            this.mnuRptStroe = new System.Windows.Forms.MenuItem();
            this.mnuRptPharmacy = new System.Windows.Forms.MenuItem();
            this.mnuRptLab = new System.Windows.Forms.MenuItem();
            this.mnuRptBilling = new System.Windows.Forms.MenuItem();
            this.mnuRptRecording = new System.Windows.Forms.MenuItem();
            this.picMain = new System.Windows.Forms.Panel();
            this.lblHospitalName = new System.Windows.Forms.Label();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.homeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.payRollToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picMain.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuDayReport
            // 
            this.mnuDayReport.Index = 1;
            this.mnuDayReport.Text = "Day Report";
            this.mnuDayReport.Click += new System.EventHandler(this.mnuDayReport_Click);
            // 
            // mnuOPRegRegister
            // 
            this.mnuOPRegRegister.Index = -1;
            this.mnuOPRegRegister.Text = "";
            // 
            // mnuOPCensus
            // 
            this.mnuOPCensus.Index = -1;
            this.mnuOPCensus.Text = "";
            // 
            // mnuIPRegRegister
            // 
            this.mnuIPRegRegister.Index = -1;
            this.mnuIPRegRegister.Text = "";
            // 
            // mnuIPCensus
            // 
            this.mnuIPCensus.Index = -1;
            this.mnuIPCensus.Text = "";
            // 
            // mnuBirthRegister
            // 
            this.mnuBirthRegister.Index = -1;
            this.mnuBirthRegister.Text = "";
            // 
            // mnuDeathRegister
            // 
            this.mnuDeathRegister.Index = -1;
            this.mnuDeathRegister.Text = "";
            // 
            // menuItem8
            // 
            this.menuItem8.Index = -1;
            this.menuItem8.Text = "";
            // 
            // mnuPharmacyRegister
            // 
            this.mnuPharmacyRegister.Index = -1;
            this.mnuPharmacyRegister.Text = "";
            // 
            // mnuPharmacyStockRegister
            // 
            this.mnuPharmacyStockRegister.Index = -1;
            this.mnuPharmacyStockRegister.Text = "";
            // 
            // mnuRegDailySales
            // 
            this.mnuRegDailySales.Index = -1;
            this.mnuRegDailySales.Text = "";
            // 
            // menuItem10
            // 
            this.menuItem10.Index = -1;
            this.menuItem10.Text = "";
            // 
            // mnuStoreRegister
            // 
            this.mnuStoreRegister.Index = -1;
            this.mnuStoreRegister.Text = "";
            // 
            // mnuStorePurchaseRegister
            // 
            this.mnuStorePurchaseRegister.Index = -1;
            this.mnuStorePurchaseRegister.Text = "";
            // 
            // mnuStoreStockRegister
            // 
            this.mnuStoreStockRegister.Index = -1;
            this.mnuStoreStockRegister.Text = "";
            // 
            // mnuRegPurcAccount
            // 
            this.mnuRegPurcAccount.Index = -1;
            this.mnuRegPurcAccount.Text = "";
            // 
            // mnuReportItem
            // 
            this.mnuReportItem.Index = -1;
            this.mnuReportItem.Text = "&Sample Report";
            this.mnuReportItem.Click += new System.EventHandler(this.mnuReportItem_Click);
            // 
            // mnuRegistration
            // 
            this.mnuRegistration.Index = -1;
            this.mnuRegistration.Text = "";
            // 
            // mnuBeds
            // 
            this.mnuBeds.Index = -1;
            this.mnuBeds.Text = "";
            // 
            // mnuSplit1
            // 
            this.mnuSplit1.Index = -1;
            this.mnuSplit1.Text = "";
            // 
            // mnuSplit2
            // 
            this.mnuSplit2.Index = -1;
            this.mnuSplit2.Text = "";
            // 
            // mnuBilling
            // 
            this.mnuBilling.Index = -1;
            this.mnuBilling.Text = "";
            // 
            // mnuAdmin
            // 
            this.mnuAdmin.Index = -1;
            this.mnuAdmin.Text = "";
            // 
            // mnuPeople
            // 
            this.mnuPeople.Index = -1;
            this.mnuPeople.Text = "";
            // 
            // mnuChangePassword
            // 
            this.mnuChangePassword.Index = -1;
            this.mnuChangePassword.Text = "";
            // 
            // mnuDoctors
            // 
            this.mnuDoctors.Index = -1;
            this.mnuDoctors.Text = "";
            // 
            // mnuUsers
            // 
            this.mnuUsers.Index = -1;
            this.mnuUsers.Text = "";
            // 
            // mnuVendors
            // 
            this.mnuVendors.Index = -1;
            this.mnuVendors.Text = "";
            // 
            // mnuWard
            // 
            this.mnuWard.Index = -1;
            this.mnuWard.Text = "";
            // 
            // mnuSettings
            // 
            this.mnuSettings.Index = -1;
            this.mnuSettings.Text = "";
            // 
            // mnuRegSetting
            // 
            this.mnuRegSetting.Index = -1;
            this.mnuRegSetting.Text = "";
            // 
            // mnuPharSetting
            // 
            this.mnuPharSetting.Index = -1;
            this.mnuPharSetting.Text = "";
            // 
            // mnuWardSetting
            // 
            this.mnuWardSetting.Index = -1;
            this.mnuWardSetting.Text = "";
            // 
            // mnuLabSetting
            // 
            this.mnuLabSetting.Index = -1;
            this.mnuLabSetting.Text = "";
            // 
            // mnuStoreSetting
            // 
            this.mnuStoreSetting.Index = -1;
            this.mnuStoreSetting.Text = "";
            // 
            // mnuReOrderLevel
            // 
            this.mnuReOrderLevel.Index = -1;
            this.mnuReOrderLevel.Text = "";
            // 
            // mnuSupportData
            // 
            this.mnuSupportData.Index = -1;
            this.mnuSupportData.Text = "";
            // 
            // mnuDepartments
            // 
            this.mnuDepartments.Index = -1;
            this.mnuDepartments.Text = "";
            // 
            // mnuCases
            // 
            this.mnuCases.Index = -1;
            this.mnuCases.Text = "";
            // 
            // mnuLabs
            // 
            this.mnuLabs.Index = -1;
            this.mnuLabs.Text = "";
            // 
            // mnuLabTest
            // 
            this.mnuLabTest.Index = -1;
            this.mnuLabTest.Text = "";
            // 
            // mnuPaymentTypes
            // 
            this.mnuPaymentTypes.Index = -1;
            this.mnuPaymentTypes.Text = "";
            // 
            // mnuPatientTypes
            // 
            this.mnuPatientTypes.Index = -1;
            this.mnuPatientTypes.Text = "";
            // 
            // mnuDoctorDegree
            // 
            this.mnuDoctorDegree.Index = -1;
            this.mnuDoctorDegree.Text = "";
            // 
            // mnuAmenities
            // 
            this.mnuAmenities.Index = -1;
            this.mnuAmenities.Text = "";
            // 
            // mnuHospitalDepartment
            // 
            this.mnuHospitalDepartment.Index = -1;
            this.mnuHospitalDepartment.Text = "";
            // 
            // mnuAdminSubItem
            // 
            this.mnuAdminSubItem.Index = -1;
            this.mnuAdminSubItem.Text = "";
            // 
            // mnuItemGroup
            // 
            this.mnuItemGroup.Index = -1;
            this.mnuItemGroup.Text = "";
            // 
            // mnuItemMeasure
            // 
            this.mnuItemMeasure.Index = -1;
            this.mnuItemMeasure.Text = "";
            // 
            // mnuManufacture
            // 
            this.mnuManufacture.Index = -1;
            this.mnuManufacture.Text = "";
            // 
            // mnuAdminRoles
            // 
            this.mnuAdminRoles.Index = -1;
            this.mnuAdminRoles.Text = "";
            // 
            // mnuAdminAccounts
            // 
            this.mnuAdminAccounts.Index = -1;
            this.mnuAdminAccounts.Text = "";
            // 
            // mnuAdminAccountGroup
            // 
            this.mnuAdminAccountGroup.Index = -1;
            this.mnuAdminAccountGroup.Text = "";
            // 
            // mnuAdminAccountHead
            // 
            this.mnuAdminAccountHead.Index = -1;
            this.mnuAdminAccountHead.Text = "";
            // 
            // mnuAdRegistration
            // 
            this.mnuAdRegistration.Index = -1;
            this.mnuAdRegistration.Text = "";
            // 
            // mnuAdRegIP
            // 
            this.mnuAdRegIP.Index = -1;
            this.mnuAdRegIP.Text = "";
            // 
            // mnuAdRegOP
            // 
            this.mnuAdRegOP.Index = -1;
            this.mnuAdRegOP.Text = "";
            // 
            // mnuAdRegBedRes
            // 
            this.mnuAdRegBedRes.Index = -1;
            this.mnuAdRegBedRes.Text = "";
            // 
            // mnuReleaseWardBill
            // 
            this.mnuReleaseWardBill.Index = -1;
            this.mnuReleaseWardBill.Text = "";
            // 
            // mnuProcessDayRpt
            // 
            this.mnuProcessDayRpt.Index = 0;
            this.mnuProcessDayRpt.Text = "";
            // 
            // mnuSplit3
            // 
            this.mnuSplit3.Index = -1;
            this.mnuSplit3.Text = "";
            // 
            // mnuExit
            // 
            this.mnuExit.Index = -1;
            this.mnuExit.Text = "";
            // 
            // mnuPharmacy
            // 
            this.mnuPharmacy.Index = -1;
            this.mnuPharmacy.Text = "";
            // 
            // mnuIssueMedicines
            // 
            this.mnuIssueMedicines.Index = -1;
            this.mnuIssueMedicines.Text = "";
            // 
            // mnuIssueToPatients
            // 
            this.mnuIssueToPatients.Index = -1;
            this.mnuIssueToPatients.Text = "";
            // 
            // mnuIssueToDepartments
            // 
            this.mnuIssueToDepartments.Index = -1;
            this.mnuIssueToDepartments.Text = "";
            // 
            // mnuReturnToStore
            // 
            this.mnuReturnToStore.Index = -1;
            this.mnuReturnToStore.Text = "";
            // 
            // mnuReceiveMedicines
            // 
            this.mnuReceiveMedicines.Index = -1;
            this.mnuReceiveMedicines.Text = "";
            // 
            // mnuReturnByPatient
            // 
            this.mnuReturnByPatient.Index = -1;
            this.mnuReturnByPatient.Text = "";
            // 
            // mnuReplaceMedicines
            // 
            this.mnuReplaceMedicines.Index = -1;
            this.mnuReplaceMedicines.Text = "";
            // 
            // mnuRepMedFromStore
            // 
            this.mnuRepMedFromStore.Index = -1;
            this.mnuRepMedFromStore.Text = "";
            // 
            // mnuIndent
            // 
            this.mnuIndent.Index = -1;
            this.mnuIndent.Text = "";
            // 
            // mnuIndentList
            // 
            this.mnuIndentList.Index = -1;
            this.mnuIndentList.Text = "";
            // 
            // mnuPrepareIndent
            // 
            this.mnuPrepareIndent.Index = -1;
            this.mnuPrepareIndent.Text = "";
            // 
            // mnuStockVerification
            // 
            this.mnuStockVerification.Index = -1;
            this.mnuStockVerification.Text = "";
            // 
            // mnuStockVerifyList
            // 
            this.mnuStockVerifyList.Index = -1;
            this.mnuStockVerifyList.Text = "";
            // 
            // mnuVerifyStock
            // 
            this.mnuVerifyStock.Index = -1;
            this.mnuVerifyStock.Text = "";
            // 
            // mnuViewStockStatus
            // 
            this.mnuViewStockStatus.Index = -1;
            this.mnuViewStockStatus.Text = "";
            // 
            // mnuStore
            // 
            this.mnuStore.Index = -1;
            this.mnuStore.Text = "";
            // 
            // mnuS_IssueMedicines
            // 
            this.mnuS_IssueMedicines.Index = -1;
            this.mnuS_IssueMedicines.Text = "";
            // 
            // mnuS_IssMedToDepartment
            // 
            this.mnuS_IssMedToDepartment.Index = -1;
            this.mnuS_IssMedToDepartment.Text = "";
            // 
            // mnuS_ReceiveMedicines
            // 
            this.mnuS_ReceiveMedicines.Index = -1;
            this.mnuS_ReceiveMedicines.Text = "";
            // 
            // mnuS_RecMedFromDepartments
            // 
            this.mnuS_RecMedFromDepartments.Index = -1;
            this.mnuS_RecMedFromDepartments.Text = "";
            // 
            // mnuS_RecMedAsOffer
            // 
            this.mnuS_RecMedAsOffer.Index = -1;
            this.mnuS_RecMedAsOffer.Text = "";
            // 
            // mnuDamages
            // 
            this.mnuDamages.Index = -1;
            this.mnuDamages.Text = "";
            // 
            // mnuS_Indent
            // 
            this.mnuS_Indent.Index = -1;
            this.mnuS_Indent.Text = "";
            // 
            // mnuS_IndentList
            // 
            this.mnuS_IndentList.Index = -1;
            this.mnuS_IndentList.Text = "";
            // 
            // mnuS_PrepareIndent
            // 
            this.mnuS_PrepareIndent.Index = -1;
            this.mnuS_PrepareIndent.Text = "";
            // 
            // mnuOrder
            // 
            this.mnuOrder.Index = -1;
            this.mnuOrder.Text = "";
            // 
            // mnuPlacingOrderToVendor
            // 
            this.mnuPlacingOrderToVendor.Index = -1;
            this.mnuPlacingOrderToVendor.Text = "";
            // 
            // mnuS_RecMedFromVendors
            // 
            this.mnuS_RecMedFromVendors.Index = -1;
            this.mnuS_RecMedFromVendors.Text = "";
            // 
            // mnuS_ToVendor
            // 
            this.mnuS_ToVendor.Index = -1;
            this.mnuS_ToVendor.Text = "";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = -1;
            this.menuItem3.Text = "";
            // 
            // mnuS_ReturnMedicines
            // 
            this.mnuS_ReturnMedicines.Index = -1;
            this.mnuS_ReturnMedicines.Text = "";
            // 
            // mnuMaterials
            // 
            this.mnuMaterials.Index = -1;
            this.mnuMaterials.Text = "";
            // 
            // mnuS_MaterialList
            // 
            this.mnuS_MaterialList.Index = -1;
            this.mnuS_MaterialList.Text = "";
            // 
            // mnuS_MaterialStockLevel
            // 
            this.mnuS_MaterialStockLevel.Index = -1;
            this.mnuS_MaterialStockLevel.Text = "";
            // 
            // mnuStoreStockVerification
            // 
            this.mnuStoreStockVerification.Index = -1;
            this.mnuStoreStockVerification.Text = "";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = -1;
            this.menuItem4.Text = "";
            // 
            // menuItem5
            // 
            this.menuItem5.Index = -1;
            this.menuItem5.Text = "";
            // 
            // menuItem6
            // 
            this.menuItem6.Index = -1;
            this.menuItem6.Text = "";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            // 
            // mnuReport
            // 
            this.mnuReport.Index = -1;
            this.mnuReport.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuProcessDayRpt,
            this.mnuDayReport});
            this.mnuReport.Text = "Reports";
            // 
            // mnuReportMoulde
            // 
            this.mnuReportMoulde.Index = -1;
            this.mnuReportMoulde.Text = "ReportMoudle";
            this.mnuReportMoulde.Select += new System.EventHandler(this.mnuReportMoulde_Select);
            // 
            // mnuRptRegistration
            // 
            this.mnuRptRegistration.Index = -1;
            this.mnuRptRegistration.Text = "Registration";
            this.mnuRptRegistration.Select += new System.EventHandler(this.mnuRptRegistration_Select);
            // 
            // mnuRptStroe
            // 
            this.mnuRptStroe.Index = -1;
            this.mnuRptStroe.Text = "Store";
            this.mnuRptStroe.Select += new System.EventHandler(this.mnuRptStroe_Select);
            // 
            // mnuRptPharmacy
            // 
            this.mnuRptPharmacy.Index = -1;
            this.mnuRptPharmacy.Text = "Pharmacy";
            this.mnuRptPharmacy.Select += new System.EventHandler(this.mnuRptPharmacy_Select);
            // 
            // mnuRptLab
            // 
            this.mnuRptLab.Index = -1;
            this.mnuRptLab.Text = "Lab";
            this.mnuRptLab.Select += new System.EventHandler(this.mnuRptLab_Select);
            // 
            // mnuRptBilling
            // 
            this.mnuRptBilling.Index = -1;
            this.mnuRptBilling.Text = "Billing";
            this.mnuRptBilling.Select += new System.EventHandler(this.mnuRptBilling_Select);
            // 
            // mnuRptRecording
            // 
            this.mnuRptRecording.Index = -1;
            this.mnuRptRecording.Text = "Recording";
            this.mnuRptRecording.Select += new System.EventHandler(this.mnuRptRecording_Select);
            // 
            // picMain
            // 
            this.picMain.BackColor = System.Drawing.SystemColors.Info;
            this.picMain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picMain.BackgroundImage")));
            this.picMain.Controls.Add(this.lblHospitalName);
            this.picMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.picMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picMain.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.picMain.ForeColor = System.Drawing.SystemColors.WindowText;
            this.picMain.Location = new System.Drawing.Point(0, 0);
            this.picMain.Name = "picMain";
            this.picMain.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.picMain.Size = new System.Drawing.Size(1022, 62);
            this.picMain.TabIndex = 1;
            this.picMain.TabStop = true;
            // 
            // lblHospitalName
            // 
            this.lblHospitalName.BackColor = System.Drawing.Color.Transparent;
            this.lblHospitalName.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblHospitalName.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHospitalName.ForeColor = System.Drawing.Color.Navy;
            this.lblHospitalName.Location = new System.Drawing.Point(472, 32);
            this.lblHospitalName.Name = "lblHospitalName";
            this.lblHospitalName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblHospitalName.Size = new System.Drawing.Size(544, 23);
            this.lblHospitalName.TabIndex = 10;
            this.lblHospitalName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.homeToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(1024, 24);
            this.mnuMain.TabIndex = 3;
            this.mnuMain.Text = "menuStrip1";
            this.mnuMain.Paint += new System.Windows.Forms.PaintEventHandler(this.mnuMain_Paint);
            // 
            // homeToolStripMenuItem
            // 
            this.homeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem,
            this.payRollToolStripMenuItem,
            this.logoutToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.homeToolStripMenuItem.Name = "homeToolStripMenuItem";
            this.homeToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.homeToolStripMenuItem.Text = "Start";
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.loginToolStripMenuItem.Text = "Login";
            this.loginToolStripMenuItem.Click += new System.EventHandler(this.loginToolStripMenuItem_Click);
            // 
            // payRollToolStripMenuItem
            // 
            this.payRollToolStripMenuItem.Enabled = false;
            this.payRollToolStripMenuItem.Name = "payRollToolStripMenuItem";
            this.payRollToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.payRollToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.payRollToolStripMenuItem.Text = "Payroll";
            this.payRollToolStripMenuItem.Click += new System.EventHandler(this.payRollToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Enabled = false;
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.logoutToolStripMenuItem.Text = "Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "About...";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.picMain);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 64);
            this.panel1.TabIndex = 4;
            // 
            // mdiHMS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1024, 201);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mnuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Location = new System.Drawing.Point(50, 50);
            this.MainMenuStrip = this.mnuMain;
            this.Menu = this.MainMenu1;
            this.Name = "mdiHMS";
            this.Text = "MedSysB";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.mdiPayroll_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mdiPayroll_KeyDown);
            this.picMain.ResumeLayout(false);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private string strUsername = "";
        private string[] strShortcuts, strDeptIds;

        //private frmRegistration objOPRegList;
        //public bool isOPRegListOpened = false;

        //private UI.Registration.frmBedBrowse objIPRegList;
        //public bool isIPRegListOpened = false;

        //public bool isAdminOpened=false;

        //private frmAdminBrowse AdminBrowse;
        //private frmBilling billing;
        //private frmLaboratory laboratory;
        //private frmWardAndBeds wardAndBeds;
        //private frmSupportData supportData;
        //private frmStockLevelReport objStkReport = new frmStockLevelReport(0,false);
        //private frmIndentBrowse indentList;
        //private frmDrugDamageBrowse DrugDamageBrowse;
        //private frmRoles objRoles;
        //private frmBillGroup objBillGroup;
        //private frmPharmacyPrintSettings frmPrintPharm;
        //private frmUpdateDrugSellingPrice objDSP;
        //private frmUpdateSellingPrice objSP;
        //private frmReportConditions reportCondition;
        //private frmDeptDamageBrowse DeptDamageBrowse;
        //private frmOrderBrowse Order;
        //private frmReplaceMedicineBrowse replaceMedicine;
        //private frmPatientReturnDrug IPPatientReturnDrug;
        public bool isIndentListOpened = false;

        //private frmReceiveMedicinesBrowse receiveMedicine;
        public bool isRMedicinesOpened = false;

        //private frmStoreReceiveBrowse sReceiveMedicines;
        public bool isStoreReceiveBrowseOpened = false;

        //private frmOfferDrugBrowse sReceiveOffer;
        public bool isStoreReceiveOfferBrowseOpened = false;

        //private frmStoreToVendorBrowse sToVendor;
        public bool isStoreToVendorOpened = false;

        //private frmStoreIssueBrowse sIssueMedicines;
        public bool isStoreIssueBrowseOpened = false;

        //private frmAccountBrowse objAccBrowse;
        //private frmWPReturns objWPatientReturns;

        //private UI.Admin.frmBedBrowse objReg;

        //private frmPharSetting objPharSetting;
        //private frmLabSetting objLabSetting;
        //private frmWardSetting objWardSetting;
        //private frmStroeSetting objStoreSetting;
        //private frmGeneralChargesSettings objGenChrgSetting;
        //private frmDayReport objDayReport;
        //private frmDayReportCase objCaseReport;
        //private CommonFunctions.UI.frmStockLevel objStockLevel;
        //private UI.Reports.frmReportMapping objReportMap;
        //private UI.Admin.frmDayReportRights objDayRepRights;
        //private frmReleaseBill objReleaseBill;
        //private frmBillingSettlement objBS;
        //private frmReportConditions objReportCond;
        //private frmPrinterSettingsRegister objPSR;
        //private frmPharmacyIndentBrowse objPharIndentBrowse;
        //private frmPatientReturnDrug PatientReturnDrug;
        //private frmInterDepartmentBrowse interdept;
        //private frmItemSupportData objItemSupportData;
        private frmPayrollBrowse objPayrollBrowse;
        //private UI.Store.frmStockRepositionBrowse objStkReposition;
        //private UI.Store.frmStockRepositionBrowse objDeptStkReposition;
        //frmMedicineIssueBrowse issueMedicines;
        //frmWardBrowse WardBrowse;
        //private frmRegFeeAccount objRegAc;

        //private frmRegistrationReport objRegReport;

        private void openPeople(string subModuleName)
        {
            //if (AdminBrowse== null) 
            //    AdminBrowse = new frmAdminBrowse();
            //else if(AdminBrowse.IsDisposed)
            //    AdminBrowse = new frmAdminBrowse();
            //AdminBrowse.MdiParent=this;
            //AdminBrowse.subModule = subModuleName;			
            //AdminBrowse.fillGridValues();
            //AdminBrowse.Show();
            //AdminBrowse.BringToFront();
        }

        private void openWardAndRooms(string subModuleName)
        {
            //if (wardAndBeds== null) 
            //    wardAndBeds = new frmWardAndBeds();
            //else if(wardAndBeds.IsDisposed)
            //    wardAndBeds = new frmWardAndBeds();
            //wardAndBeds.MdiParent = this;
            //wardAndBeds.subModule = subModuleName;
            //wardAndBeds.fillGridValues();			
            //wardAndBeds.Show();
            //wardAndBeds.BringToFront();			
        }

        private void openSupportData(Int32 subModule)
        {
            //if (supportData== null) 
            //    supportData = new frmSupportData();
            //else if(supportData.IsDisposed)
            //    supportData = new frmSupportData();
            //supportData.MdiParent = this;
            //supportData.FillGrid(subModule);
            //supportData.Show();
            //supportData.BringToFront();			
        }

        private void openIndentList(string subModuleName, string moduleName)
        {
            //if(indentList == null || indentList.IsDisposed) indentList = new frmIndentBrowse(iStoreId);
            //indentList.MdiParent = this;
            //indentList.moduleName = moduleName;
            //indentList.Show();
            //if (subModuleName == "Prepare Indent")
            //    indentList.prepareIndent();
        }

        private void openReceiveMedicines(string subModuleName)
        {
            //if (isRMedicinesOpened == false)
            //{
            //    receiveMedicine = new frmReceiveMedicinesBrowse();
            //    receiveMedicine.MdiParent = this;
            //    receiveMedicine.subModule = subModuleName;
            //    receiveMedicine.Show();
            //    if (subModuleName == "Return by Patient")
            //        receiveMedicine.openReceiveForm();
            //    isRMedicinesOpened = true;
            //}
            //else
            //{
            //    receiveMedicine.fillGridValues();
            //    receiveMedicine.subModule = subModuleName;
            //    if (subModuleName == "Return by Patient")
            //        receiveMedicine.openReceiveForm();
            //}
        }

        private void openStoreToVendor(string subModuleName)
        {
            //if(sToVendor == null) sToVendor = new frmStoreToVendorBrowse(iStoreId);
            //if(sToVendor.IsDisposed) sToVendor = new frmStoreToVendorBrowse(iStoreId);

            //sToVendor.MdiParent = this;
            //sToVendor.subModule = subModuleName;
            //sToVendor.Show();
            //sToVendor.fillGridValues();
            //sToVendor.returnMedicinesForms();
        }

        private void mnuRegistration_Click(object sender, System.EventArgs e)
        {
            //if(objOPRegList == null || objOPRegList.IsDisposed)	
            //    objOPRegList = new frmRegistration();
            //objOPRegList.MdiParent = this;
            //objOPRegList.Show();
        }

        private void mnuDoctors_Click(object sender, System.EventArgs e)
        {
            openPeople("Doctors");
        }

        private void mnuUsers_Click(object sender, System.EventArgs e)
        {
            openPeople("Users");
        }

        private void mnuStaffMembers_Click(object sender, System.EventArgs e)
        {
            openPeople("Staff Members");
        }

        private void mnuVendors_Click(object sender, System.EventArgs e)
        {
            openPeople("Vendors");
        }
        private void mnuStorePhrEntry_Click(object sender, System.EventArgs e)
        {
            //new frmAddItem().ShowDialog();
        }
        private void mnuExit_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
            this.Close();
        }

        private void mnuPayroll_Click(object sender, System.EventArgs e)
        {

            
        }

        private void mnuWards_Click(object sender, System.EventArgs e)
        {
            openWardAndRooms("Wards");
        }

        private void mnuRooms_Click(object sender, System.EventArgs e)
        {
            openWardAndRooms("Rooms");
        }

        private void mnuA_Beds_Click(object sender, System.EventArgs e)
        {
            openWardAndRooms("Beds");
        }

        private void mnuBedReservation_Click(object sender, System.EventArgs e)
        {
            openWardAndRooms("Bed Reservation");
        }

        private void mnuBilling_Click(object sender, System.EventArgs e)
        {
            //billing = new frmBilling(iBillingSectionId);
            //billing.MdiParent = this;
            //billing.Show();
        }

        private void mnuLaboratory_Click(object sender, System.EventArgs e)
        {
            MenuItem mnu = (MenuItem)sender;
            for (int i = 0; i < iNoOfLabs; i++)
            {
                if (strLabs[i, 1] == mnu.Text)
                {
                    iLabId = Convert.ToInt32(strLabs[i, 0]);
                    break;
                }
            }

            //laboratory = new frmLaboratory(iLabId);
            //laboratory.MdiParent = this;
            //laboratory.Show();
        }

        private void mnuRecording_Click(object sender, System.EventArgs e)
        {
            MenuItem mnu = (MenuItem)sender;

            for (int i = 0; i < iNoOfLabs; i++)
            {
                if (strRecording[i, 1] == mnu.Text)
                {
                    iRecordingId = Convert.ToInt32(strRecording[i, 0]);
                    break;
                }
            }

            //frmMedicalRecords objMR = new frmMedicalRecords(iRecordingId);
            //objMR.MdiParent = this;
            //objMR.Show();
        }
        private void mnuWardDept_Click(object sender, System.EventArgs e)
        {
            MenuItem mnu = (MenuItem)sender;

            for (int i = 0; i < iNoOfWards; i++)
            {
                if (strWards[i, 1] == mnu.Text)
                {
                    iWardId = Convert.ToInt32(strWards[i, 0]);
                    break;
                }
            }

            //if(WardBrowse == null || WardBrowse.IsDisposed) WardBrowse = new frmWardBrowse(iWardId,clsWardConstants.WP_INDENT_LIST);
            //WardBrowse.MdiParent = this;
            //WardBrowse.subModule = "Ward Patient Indent";
            //WardBrowse.Show();
            //WardBrowse.BringToFront();
            //WardBrowse.RefreshGrid();
        }

        private void mnuCreate_Click(object sender, System.EventArgs e)
        {
            //frmCounters counters = new frmCounters();
            //counters.MdiParent = this;
            //counters.Show();
        }

        private void mnuRegSetting_Click(object sender, System.EventArgs e)
        {
            //if(objRegAc == null) objRegAc = new frmRegFeeAccount();
            //if(objRegAc.IsDisposed)	objRegAc = new frmRegFeeAccount();
            //objRegAc.Show();
            //objRegAc.BringToFront();
        }

        private void mnuAdminSubItem_Click(object sender, System.EventArgs e)
        {
            //openSupportData(clsAdminConstants.SUB_ITEMS);
        }

        private void mnuDepartments_Click(object sender, System.EventArgs e)
        {
            //openSupportData(clsAdminConstants.SUP_DEPARTMENT_LIST);
        }

        private void mnuCases_Click(object sender, System.EventArgs e)
        {
            //openSupportData(clsAdminConstants.SUP_CASE_LIST);
        }

        private void mnuLabs_Click(object sender, System.EventArgs e)
        {
            // openSupportData(clsAdminConstants.SUP_LAB_LIST);
        }

        private void mnuLabTest_Click(object sender, System.EventArgs e)
        {
            //openSupportData(clsAdminConstants.SUP_LAB_TEST_LIST);
        }

        private void mnuPaymentTypes_Click(object sender, System.EventArgs e)
        {
            ///openSupportData(clsAdminConstants.SUP_PAYMENT_TYPE_LIST);
        }

        private void mnuPatientTypes_Click(object sender, System.EventArgs e)
        {
            //openSupportData(clsAdminConstants.SUP_PATIENT_TYPE_LIST);
        }

        private void mnuDoctorDegree_Click(object sender, System.EventArgs e)
        {
            //openSupportData(clsAdminConstants.SUP_DEGREE_LIST);
        }

        private void mnuIndentList_Click(object sender, System.EventArgs e)
        {
            //openIndentList("Indent List","OP Pharmacy");
        }

        private void mnuPrepareIndent_Click(object sender, System.EventArgs e)
        {
            //openIndentList("Prepare Indent","OP Pharmacy");
        }

        private void mnuPharmacyIndentList_Click(object sender, System.EventArgs e)
        {
            //openPharmacyIndent(false);
        }

        private void mnuPharmacyPrepareIndent_Click(object sender, System.EventArgs e)
        {
            //openPharmacyIndent(true);
        }

        private void mnuReceiveFromStore_Click(object sender, System.EventArgs e)
        {
            openReceiveMedicines("Receive from Store");
        }

        private void mnuReturnByPatient_Click(object sender, System.EventArgs e)
        {
            //if(PatientReturnDrug == null || PatientReturnDrug.IsDisposed)PatientReturnDrug = new frmPatientReturnDrug(iPharmacyId);
            //PatientReturnDrug.ShowDialog();
        }


        private void mnuIssueToPatients_Click(object sender, System.EventArgs e)
        {
            openIssueFromPharmacy("IssuePatient");
        }
        private void mnuReturnToStore_Click(object sender, System.EventArgs e)
        {
            openIssueFromPharmacy("ReturnStore");
        }

        //Modified : JV 
        //Date	   : 01-09-2005
        //Purpose  : Pharmacy Transaction History

        private void mnuPharmacyTransHistory_Click(object sender, System.EventArgs e)
        {
            //frmPharmacyTransHistory objTransHistory = new frmPharmacyTransHistory(iPharmacyId);
            //objTransHistory.ShowDialog();
        }
        //--------------

        //Modified : JV 
        //Date	   : 12-09-2005
        //Purpose  : Issue drug Pharmacy to Dept(Ward), Issue drug to Ward-Patient & Return drug by Ward-Patient

        private void mnuReturnDrugIssuedToWardPatient_Click(object sender, System.EventArgs e)
        {
            openIssueFromPharmacy("ReturnByWardPatient");
        }
        private void mnuReturnDrugIssuedToWardPatient_new_Click(object sender, System.EventArgs e)
        {
            openIssueFromPharmacy("ReturnByWardPatient-New");
        }
        private void mnuReturnDrugIssuedToWardPatient_afterDischarge_Click(object sender, System.EventArgs e)
        {
            //if(objWPatientReturns == null || objWPatientReturns.IsDisposed)
            //{
            //    objWPatientReturns = new frmWPReturns(iPharmacyId);
            //}
            //objWPatientReturns.ShowDialog();

        }
        private void mnuIssueDrugToWardPatient_Click(object sender, System.EventArgs e)
        {
            openIssueFromPharmacy("IssueToWardPatient");
        }
        private void mnuIssueDrugToWardPatient_New_Click(object sender, System.EventArgs e)
        {
            //openIssueFromPharmacy("IssueToWardPatient-new");
        }
        private void mnuIssueDrugPharmacyToDept_Click(object sender, System.EventArgs e)
        {
            mnuRepMedFromPatients_Click(sender, e);
        }
        //--------------
        private void mnuDepositAmount_Click(object sender, System.EventArgs e)
        {
            openIssueFromPharmacy("Deposit Refund");
        }

        private void mnuInterDept_Click(Object sender, System.EventArgs e)
        {
            //if(interdept == null || interdept.IsDisposed)interdept = new frmInterDepartmentBrowse(iPharmacyId);
            //interdept.MdiParent = this;
            //interdept.Show();
        }
        private void mnuInterDeptstore_Click(Object sender, System.EventArgs e)
        {
            //if(interdept == null || interdept.IsDisposed)interdept = new frmInterDepartmentBrowse(iStoreId);
            //interdept.MdiParent = this;
            //interdept.Show();
        }
        private void mnuS_Item_Click(Object sender, System.EventArgs e)
        {
            //if (objItemSupportData == null || objItemSupportData.IsDisposed)
            //    objItemSupportData = new frmItemSupportData();
            //objItemSupportData.MdiParent = this;
            //objItemSupportData.FillGrid(clsAdminConstants.SUB_ITEMS);
            //objItemSupportData.Show();
            //objItemSupportData.BringToFront();	
        }

        private void mnuS_Vendor_Click(Object sender, System.EventArgs e)
        {
            //if (AdminBrowse == null || AdminBrowse.IsDisposed) 
            //    AdminBrowse = new frmAdminBrowse(true);
            //AdminBrowse.MdiParent = this;
            //AdminBrowse.subModule = "Vendors";			
            //AdminBrowse.fillGridValues();
            //AdminBrowse.Show();
            //AdminBrowse.BringToFront();
        }

        private void openIssueFromPharmacy(string strSubModule)
        {
            //if(issueMedicines == null || issueMedicines.IsDisposed) issueMedicines = new frmMedicineIssueBrowse(strUsername,iPharmacyId);
            //issueMedicines.MdiParent = this;
            //issueMedicines.subModule = strSubModule;
            //issueMedicines.Show();
            //issueMedicines.BringToFront();
            //issueMedicines.openIssue();
        }
        private void mnuRepMedFromPatients_Click(object sender, System.EventArgs e)
        {
            //if(replaceMedicine==null || replaceMedicine.IsDisposed)replaceMedicine = new frmReplaceMedicineBrowse(iPharmacyId);
            //replaceMedicine.MdiParent = this;
            //replaceMedicine.Show();
        }

        private void mnuBeds_Click(object sender, System.EventArgs e)
        {
            //if(objIPRegList == null || objIPRegList.IsDisposed) 
            //    objIPRegList = new UI.Registration.frmBedBrowse();

            //objIPRegList.MdiParent = this;
            //objIPRegList.Show();
        }

        private void mnuS_IndentList_Click(object sender, System.EventArgs e)
        {
            openIndentList("Indent List", "Store");
        }

        private void mnuS_PrepareIndent_Click(object sender, System.EventArgs e)
        {
            openIndentList("Prepare Indent", "Store");
        }

        private void mnuS_RecMedFromVendors_Click(object sender, System.EventArgs e)
        {
            //if(Order == null || Order.IsDisposed)Order = new frmOrderBrowse(iStoreId);
            //Order.MdiParent = this;
            //Order.Show();
            //frmGoodsReceived goodsReceived = new frmGoodsReceived(iStoreId);
            //goodsReceived.ShowDialog();
            //Order.refreshGrid();
        }
        private void mnuS_MultipleSelectionDrug_Click(Object sender, System.EventArgs e)
        {
            //if(Order == null || Order.IsDisposed)Order = new frmOrderBrowse(iStoreId);
            //Order.MdiParent = this;
            //Order.Show();
            //frmMultipleLookupSelection DrugDetails = new frmMultipleLookupSelection(iStoreId);
            //DrugDetails.ShowDialog();
            //Order.refreshGrid();

        }

        private void mnuS_RecMedFromDepartments_Click(object sender, System.EventArgs e)
        {
            //if (sReceiveMedicines == null || sReceiveMedicines.IsDisposed)
            //{
            //    sReceiveMedicines = new frmStoreReceiveBrowse(iStoreId);
            //    sReceiveMedicines.MdiParent = this;
            //    sReceiveMedicines.Show();
            //    isStoreReceiveBrowseOpened	= true;
            //}
        }

        private void mnuS_RecMedAsOffer_Click(object sender, System.EventArgs e)
        {
            //if (sReceiveOffer ==null || sReceiveOffer.IsDisposed)
            //{
            //    sReceiveOffer			= new frmOfferDrugBrowse(iStoreId);
            //    sReceiveOffer.MdiParent = this;
            //    sReceiveOffer.Show();
            //    isStoreReceiveOfferBrowseOpened = true;
            //}
        }

        private void mnuS_ReturnMedicines_Click(object sender, System.EventArgs e)
        {
            openStoreToVendor("Return Medicines");
        }

        private void mnuS_ReplaceMedicines_Click(object sender, System.EventArgs e)
        {
            openStoreToVendor("Replace Medicines");
        }

        private void mnuDamages_Click(object sender, System.EventArgs e)
        {
            //if(DrugDamageBrowse == null || DrugDamageBrowse.IsDisposed)
            //{
            //    DrugDamageBrowse = new frmDrugDamageBrowse(strUsername,iStoreId);
            //    DrugDamageBrowse.MdiParent = this;
            //    DrugDamageBrowse.Show();
            //}
        }

        private void mnuDeptDamages_Click(object sender, System.EventArgs e)
        {
            //if(DeptDamageBrowse == null || DeptDamageBrowse.IsDisposed)
            //{
            //    DeptDamageBrowse = new frmDeptDamageBrowse(strUsername,iPharmacyId);
            //    DeptDamageBrowse.MdiParent = this;
            //    DeptDamageBrowse.Show();
            //}
        }

        private void mnuS_IssMedToDepartment_Click(object sender, System.EventArgs e)
        {
            //if (sIssueMedicines == null || sIssueMedicines.IsDisposed)
            //{
            //    sIssueMedicines				= new frmStoreIssueBrowse(strUsername, iStoreId);
            //    sIssueMedicines.MdiParent	= this;
            //    sIssueMedicines.Show();
            //    frmDrugIssueToDepartment DrugIssue = new frmDrugIssueToDepartment(iStoreId);
            //    DrugIssue.Text = "Issue Drugs to Departments";
            //    DrugIssue.ShowDialog();

            //    sIssueMedicines.refreshGrid();
            //    isStoreIssueBrowseOpened	= true;
            //}
            //else
            //{
            //    frmDrugIssueToDepartment DrugIssue = new frmDrugIssueToDepartment(iStoreId);
            //    DrugIssue.Text = "Issue Drugs to Departments";
            //    DrugIssue.ShowDialog();
            //    sIssueMedicines.refreshGrid();
            //}
        }

        #region Ward Module
        private void mnuS_IssMedToWP_Click(object sender, System.EventArgs e)
        {

        }

        private void mnuS_IssMedToIndWP_Click(object sender, System.EventArgs e)
        {

        }

        private void mnuP_IssMedToWP_Click(object sender, System.EventArgs e)
        {

        }

        private void mnuP_IssMedToIndWP_Click(object sender, System.EventArgs e)
        {
            openIssueFromPharmacy("IssueToIndWardPatient");
            //			Ward.UI.frmIssueDrugstoInterDept objWPIssueInter = new frmIssueDrugstoInterDept(iPharmacyId);
            //			objWPIssueInter.Show();
        }
        private void mnuP_PostChargesToIPBill_Click(object sender, System.EventArgs e)
        {
            openIssueFromPharmacy("Post Charges to IPBill");
            //			Ward.UI.frmPatientsAvailableDrugs objPostCharges = new frmPatientsAvailableDrugs(iPharmacyId);
            //			objPostCharges.Show();
        }
        #endregion

        private void mnuRepMedFromStore_Click(object sender, System.EventArgs e)
        {
            //if(replaceMedicine == null || replaceMedicine.IsDisposed)replaceMedicine = new frmReplaceMedicineBrowse(iPharmacyId);
            //replaceMedicine.MdiParent = this;
            //replaceMedicine.Show();
        }

        private void mnuPlacingOrderToVendor_Click(object sender, System.EventArgs e)
        {
            //if(Order == null || Order.IsDisposed)Order = new frmOrderBrowse(iStoreId);
            //Order.MdiParent = this;
            //Order.Show();
            //frmOrderToVendor placingOrderToVendor = new frmOrderToVendor(iStoreId);
            //placingOrderToVendor.ShowDialog();
            //Order.refreshGrid();
        }

        private void mnuIPIndentList_Click(object sender, System.EventArgs e)
        {
            openIndentList("Indent List", "IP Pharmacy");
        }

        private void mnuIPPrepareIndent_Click(object sender, System.EventArgs e)
        {
            openIndentList("Prepare Indent", "IP Pharmacy");
        }

        private void mnuIPReceiveFromStore_Click(object sender, System.EventArgs e)
        {
            openReceiveMedicines("Receive from Store");
        }

        private void mnuIPReturnByPatient_Click(object sender, System.EventArgs e)
        {
            //if(IPPatientReturnDrug == null || IPPatientReturnDrug.IsDisposed)PatientReturnDrug = new frmPatientReturnDrug(iPharmacyId);
            //IPPatientReturnDrug.Text = "Drug Returns By Patient to IP Pharmacy";
            //IPPatientReturnDrug.ShowDialog();
        }

        private void menuItem3_Click(object sender, System.EventArgs e)
        {
            openStoreToVendor("Return List");
        }

        private void mnuWard_Click(object sender, System.EventArgs e)
        {
            openWardAndRooms("Beds");
        }

        private void mnuAdminRoles_Click(object sender, System.EventArgs e)
        {
            //if(objRoles == null || objRoles.IsDisposed)
            //{
            //    objRoles = new frmRoles();
            //    objRoles.ShowDialog();
            //}
        }

        private void mnuBillGroup_Click(object sender, System.EventArgs e)
        {
            //if(objBillGroup == null || objBillGroup.IsDisposed)
            //{
            //    objBillGroup = new frmBillGroup();
            //    objBillGroup.ShowDialog();
            //}
        }

        private void mnuAdminAccountGroup_Click(object sender, System.EventArgs e)
        {
            openAccountBrowse("Account Group");
        }

        private void mnuAdminAccountHead_Click(object sender, System.EventArgs e)
        {
            openAccountBrowse("Account Head");
        }
        private void openAccountBrowse(string strSubMoudle)
        {
            //if(objAccBrowse == null) objAccBrowse = new frmAccountBrowse();
            //if(objAccBrowse.IsDisposed) objAccBrowse = new frmAccountBrowse();
            //objAccBrowse.strSubModule = strSubMoudle;
            //objAccBrowse.MdiParent = this;
            //objAccBrowse.fillGridValues();
            //objAccBrowse.Show();
            //objAccBrowse.BringToFront();
        }

        private void mnuAdRegIP_Click(object sender, System.EventArgs e)
        {
            openAdminRegistration("IP Registration");
        }
        private void openAdminRegistration(string strSubModule)
        {
            //if (objReg == null || objReg.IsDisposed) objReg = new UI.Admin.frmBedBrowse();
            //objReg.strSubModule = strSubModule;
            //objReg.MdiParent = this;
            //objReg.fillGridValues();
            //objReg.Show();
            //objReg.BringToFront();
            //if(strSubModule == "Bed Reservation")
            //{
            //    frmReserveBed Reserve = new frmReserveBed();
            //    Reserve.ShowDialog();
            //}
            //else if (strSubModule == "Modify Patient Details")
            //{
            //    if (clsRegParamSettings.showCorporateDetails() == 1)
            //        new frmOPCorpRegistration("Modify Patient Details", true).ShowDialog();
            //    else
            //        new frmOPRegistration("Modify Patient Details", true).ShowDialog();
            //}
        }

        private void mnuAdRegOP_Click(object sender, System.EventArgs e)
        {
            openAdminRegistration("OP Registration");
        }

        private void mnuAdRegBedRes_Click(object sender, System.EventArgs e)
        {
            openAdminRegistration("Bed Reservation");
        }

        private void mnuAdModifyPatient_Click(object sender, System.EventArgs e)
        {
            openAdminRegistration("Modify Patient Details");
        }

        private void mnuStore_Select(object sender, System.EventArgs e)
        {
            MenuItem mnu = (MenuItem)sender;
            for (int i = 0; i < iNoOfStores; i++)
            {
                if (strStores[i, 1] == mnu.Text)
                {
                    iStoreId = Convert.ToInt32(strStores[i, 0]);
                    clsGeneral.STORE_NAME = strStores[i, 1];
                    break;
                }
            }
        }

        private void mnuPharmacy_Select(object sender, System.EventArgs e)
        {
            MenuItem mnu = (MenuItem)sender;
            for (int i = 0; i < iNoOfPharmacies; i++)
            {
                if (strPharmacies[i, 1] == mnu.Text)
                {
                    iPharmacyId = Convert.ToInt32(strPharmacies[i, 0]);
                    clsGeneral.PHARMACY_NAME = strPharmacies[i, 1];
                    break;
                }
            }
        }

        /* ---------------------------------------------------------------
         * Name		: Joseph Gerald J.
         * Date		: 30-Oct-2005 10:05 AM
         * Purpose	: To get the selected Billing Section ID
         * */

        private void mnuBilling_Select(object sender, System.EventArgs e)
        {
            MenuItem mnu = (MenuItem)sender;
            for (int i = 0; i < iNoOfBillingSections; i++)
            {
                if (strBillingSections[i, 1] == mnu.Text)
                {
                    iBillingSectionId = Convert.ToInt32(strBillingSections[i, 0]);
                    clsGeneral.BILLINGSECTION_NAME = strBillingSections[i, 1];
                    break;
                }
            }
        }
        private void BuildPayrollMenu()
        {

            //mnuPayroll = new MenuItem("-");
            //mnuPayroll.Index = iMenuIndex;
            //iMenuIndex++;
            //mnuPayroll.MenuItems.Add(this.mnuPayroll);

            //this.mnuPayroll.MenuItems.Add("Payroll", new System.EventHandler(this.mnuPayroll_Click));

            //---------------------------------------------------------------
        }
        /**
         * author  : ntanand
         * date	   : 26-Oct-2005
         * purpose : to Build Report Menu Dynamically..
         * */

        //private void buildReportMenu()
        //{
        //    iMenuIndex = 0;
        //    clsGeneral.RPT_USER_NAME = objReptMapping.getReptUser();
        //    clsGeneral.IS_REPORTS_DEPT_WISE=objReptMapping.getReportShowConstraint();
        //    clsGeneral.USER_DEPARTMENT =objReptMapping.getLoggedUsrDepartment(clsGeneral.USER_NAME);
        //    objGeneral.getAvailableReports();

        //    if(iUserDepartmentType.IndexOf(clsGeneral.DEPARTMENT_REGISTRATION.ToString())>=0 || iUserDepartmentType.IndexOf(clsGeneral.DEPARTMENT_ADMIN.ToString())>=0)
        //    {
        //        this.mnuRptRegistration = new MenuItem("&OP Registration");
        //        this.mnuRegistration.Index = iMenuIndex;
        //        iMenuIndex++;
        //        this.mnuRptRegistration.Select += new System.EventHandler(this.mnuRptRegistration_Select);				

        //        iNoOfOPRegiReports = objGeneral.getNoOfOPRegistrationReports();
        //        strOPRegiReports = objGeneral.getOPRegistrationReports();

        //        for (int j =0 ; j<iNoOfOPRegiReports; j++)
        //        {
        //            this.mnuReportItem = new MenuItem(strOPRegiReports[j,1]);

        //            mnuReportItem.Index = j;
        //            this.mnuReportItem.Click += new System.EventHandler(this.mnuReportItem_Click);
        //            this.mnuRptRegistration.MenuItems.Add(this.mnuReportItem);
        //        }
        //        this.mnuCusReport.MenuItems.Add(this.mnuRptRegistration);


        //        this.mnuRptRegistration = new MenuItem("&IP Registration");
        //        this.mnuRptRegistration.Index = iMenuIndex++;;
        //        iMenuIndex++;
        //        this.mnuRptRegistration.Select += new System.EventHandler(this.mnuRptRegistration_Select);

        //        iNoOfIPRegiReports = objGeneral.getNoOfIPRegistrationReports();
        //        strIPRegiReports = objGeneral.getIPRegistrationReports();

        //        for (int j=0 ; j< iNoOfIPRegiReports; j++)
        //        {
        //            this.mnuReportItem = new MenuItem(strIPRegiReports[j,1]);
        //            mnuReportItem.Index = j;
        //            this.mnuReportItem.Click += new System.EventHandler(this.mnuReportItem_Click);
        //            this.mnuRptRegistration.MenuItems.Add(this.mnuReportItem);
        //        }
        //        this.mnuCusReport.MenuItems.Add(this.mnuRptRegistration);				
        //    }

        //    if(iUserDepartmentType.IndexOf(clsGeneral.DEPARTMENT_STORE.ToString())>=0 || iUserDepartmentType.IndexOf(clsGeneral.DEPARTMENT_ADMIN.ToString())>=0)
        //        buildStoreReportMenu();

        //    if(iUserDepartmentType.IndexOf(clsGeneral.DEPARTMENT_PHARMACY.ToString())>=0 || iUserDepartmentType.IndexOf(clsGeneral.DEPARTMENT_ADMIN.ToString())>=0)
        //        buildPharmacyReportMenu();			

        //    if(iUserDepartmentType.IndexOf(clsGeneral.DEPARTMENT_BILLING.ToString())>=0 || iUserDepartmentType.IndexOf(clsGeneral.DEPARTMENT_ADMIN.ToString())>=0)
        //    {
        //        if(iUserDepartmentType.IndexOf(clsGeneral.DEPARTMENT_ADMIN.ToString())>=0)
        //        {
        //            MenuItem mnuSplit = new MenuItem("-");
        //            mnuCusReport.MenuItems.Add(mnuSplit);
        //            iMenuIndex++;
        //        }

        //        this.mnuRptBilling = new MenuItem("Billing");

        //        /* --------------------------------------------------------
        //         * Name		: Joseph Gerald J
        //         * Date		: 30-Oct-2005 10:45 AM
        //         * Purpose	: To set the MenuIndex to mnuRptBilling 
        //         *			  as of now it is set to mnuBilling
        //         * */

        //        //this.mnuBilling.Index = iMenuIndex;
        //        this.mnuRptBilling.Index = iMenuIndex;
        //        iMenuIndex++;
        //        this.mnuRptBilling.Select += new System.EventHandler(this.mnuRptBilling_Select);

        //        // --------------------------------------------------------
        //        //iNoOfBillingSections = objStore.getNoOfBillingSections();
        //        //strBillingSections = new string[iNoOfBillingSections,2];
        //        //strBillingSections = objStore.getBillingSections();

        //        for(int i = 0; i < iNoOfBillingSections; i++)
        //        {
        //            if(iUserDepartmentId == Convert.ToInt32(strBillingSections[i,0]) || iUserDepartmentType.IndexOf(clsGeneral.DEPARTMENT_ADMIN.ToString())>=0 ||
        //                iUserDepartmentType.IndexOf(clsGeneral.DEPARTMENT_BILLING.ToString())>=0)
        //            {
        //                this.mnuRptBilling = new MenuItem(strBillingSections[i,1]);
        //                this.mnuRptBilling.Select += new System.EventHandler(this.mnuRptBilling_Select);
        //                if(clsGeneral.IS_REPORTS_DEPT_WISE!="")
        //                    objGeneral.getRptsHdptIdwise(strBillingSections[i,0],clsGeneral.DEPARTMENT_BILLING);
        //                iNoOfBillingReports = objGeneral.getNoOfBillingReports();
        //                strBillingReports = objGeneral.getBillingReports();

        //                for (int j=0 ; j< iNoOfBillingReports; j++)
        //                {
        //                    this.mnuReportItem = new MenuItem(strBillingReports[j,1]);
        //                    mnuReportItem.Index = j;
        //                    this.mnuReportItem.Click += new System.EventHandler(this.mnuReportItem_Click);
        //                    this.mnuRptBilling.MenuItems.Add(this.mnuReportItem);
        //                }
        //                this.mnuCusReport.MenuItems.Add(mnuRptBilling);
        //                iMenuIndex++;
        //            }
        //        }

        //    }

        //    if(iUserDepartmentType.IndexOf(clsGeneral.DEPARTMENT_LAB.ToString())>=0 || iUserDepartmentType.IndexOf(clsGeneral.DEPARTMENT_ADMIN.ToString())>=0)
        //        buildLabReportMenu();
        //    if(iUserDepartmentType.IndexOf(clsGeneral.DEPARTMENT_RECORDING.ToString())>=0 || iUserDepartmentType.IndexOf(clsGeneral.DEPARTMENT_ADMIN.ToString())>=0)
        //        buildRecordingReportMenu();
        //    if(clsGeneral.IS_REPORTS_DEPT_WISE!="")
        //    {
        //        objGeneral.getAvailableReports();
        //    }
        //}

        //private void buildFileMenu()
        //{
        //    iMenuIndex = 0;
        //    dvUserDept.RowFilter = "";
        //    dvUserDept.RowFilter = "HDEPT_TYPE IN (" + clsGeneral.DEPARTMENT_REGISTRATION + "," + clsGeneral.DEPARTMENT_ADMIN + ")";
        //    //if(iUserDepartmentType == clsGeneral.DEPARTMENT_REGISTRATION || iUserDepartmentType == clsGeneral.DEPARTMENT_ADMIN)
        //    if(dvUserDept.Count > 0)
        //    {
        //        this.mnuRegistration = new MenuItem("&OP Registration");
        //        this.mnuRegistration.Index = iMenuIndex;
        //        iMenuIndex++;
        //        this.mnuRegistration.Click += new System.EventHandler(this.mnuRegistration_Click);
        //        this.mnuFile.MenuItems.Add(this.mnuRegistration);

        //        this.mnuBeds =new MenuItem("&IP Registration");
        //        this.mnuBeds.Index = iMenuIndex;
        //        iMenuIndex++;
        //        this.mnuBeds.Click += new System.EventHandler(this.mnuBeds_Click);
        //        this.mnuFile.MenuItems.Add(this.mnuBeds);
        //        bAddSplitMenu = true;
        //    }

        //    dvUserDept.RowFilter = "";
        //    dvUserDept.RowFilter = "HDEPT_TYPE IN (" + clsGeneral.DEPARTMENT_STORE + "," + clsGeneral.DEPARTMENT_ADMIN + ")";
        //    //if(iUserDepartmentType == clsGeneral.DEPARTMENT_STORE || iUserDepartmentType == clsGeneral.DEPARTMENT_ADMIN)
        //    if(dvUserDept.Count > 0)
        //        buildStoreMenu();

        //    dvUserDept.RowFilter = "";
        //    dvUserDept.RowFilter = "HDEPT_TYPE IN (" + clsGeneral.DEPARTMENT_PHARMACY + "," + clsGeneral.DEPARTMENT_ADMIN + ")";
        //    //if(iUserDepartmentType == clsGeneral.DEPARTMENT_PHARMACY || iUserDepartmentType == clsGeneral.DEPARTMENT_ADMIN)
        //    if(dvUserDept.Count > 0)
        //        buildPharmacyMenu();

        //    dvUserDept.RowFilter = "";
        //    dvUserDept.RowFilter = "HDEPT_TYPE IN (" + clsGeneral.DEPARTMENT_BILLING + "," + clsGeneral.DEPARTMENT_ADMIN + ")";
        //    //if(iUserDepartmentType == clsGeneral.DEPARTMENT_BILLING || iUserDepartmentType == clsGeneral.DEPARTMENT_ADMIN)
        //    if(dvUserDept.Count > 0)
        //        buildBillingMenu();

        //    dvUserDept.RowFilter = "";
        //    dvUserDept.RowFilter = "HDEPT_TYPE IN (" + clsGeneral.DEPARTMENT_LAB + "," + clsGeneral.DEPARTMENT_ADMIN + ")";
        //    //if(iUserDepartmentType == clsGeneral.DEPARTMENT_LAB || iUserDepartmentType == clsGeneral.DEPARTMENT_ADMIN)
        //    if(dvUserDept.Count > 0)
        //        buildLabMenu();

        //    dvUserDept.RowFilter = "";
        //    dvUserDept.RowFilter = "HDEPT_TYPE IN (" + clsGeneral.DEPARTMENT_RECORDING + "," + clsGeneral.DEPARTMENT_ADMIN + ")";
        //    //if(iUserDepartmentType == clsGeneral.DEPARTMENT_RECORDING || iUserDepartmentType == clsGeneral.DEPARTMENT_ADMIN)
        //    if(dvUserDept.Count > 0)
        //        buildRecordingMenu();

        //    if(clsGeneral.ShowWardMenu())
        //    {
        //        dvUserDept.RowFilter = "";
        //        dvUserDept.RowFilter = "HDEPT_TYPE IN (" + clsGeneral.DEPARTMENT_WARD + "," + clsGeneral.DEPARTMENT_ADMIN + ")";
        //        //if(iUserDepartmentType == clsGeneral.DEPARTMENT_WARD || iUserDepartmentType == clsGeneral.DEPARTMENT_ADMIN)
        //        if(dvUserDept.Count > 0)
        //            buildWardMenu();
        //    }
        //    dvUserDept.RowFilter = "";
        //    dvUserDept.RowFilter = "HDEPT_TYPE = " + clsGeneral.DEPARTMENT_ADMIN;
        //    //if(iUserDepartmentType == clsGeneral.DEPARTMENT_ADMIN)
        //    if(dvUserDept.Count > 0)
        //        buildAdminMenu();

        //    //---
        //    if(clsGeneral.addPayrollMenu() && clsGeneral.ShowPayrollMenu())
        //    {
        //        mnuSplit1 = new MenuItem("-");
        //        mnuSplit1.Index = iMenuIndex;
        //        iMenuIndex++;
        //        mnuFile.MenuItems.Add(this.mnuSplit1);

        //        this.mnuFile.MenuItems.Add("Payroll", new System.EventHandler(this.mnuPayroll_Click));

        //    }
        //    //--
        //    mnuSplit1 = new MenuItem("-");
        //    mnuSplit1.Index = iMenuIndex;
        //    iMenuIndex++;
        //    mnuFile.MenuItems.Add(this.mnuSplit1);

        //    this.mnuExit = new MenuItem("E&xit");
        //    this.mnuExit.Index = iMenuIndex;
        //    iMenuIndex++;
        //    mnuFile.MenuItems.Add(this.mnuExit);
        //    this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
        //}
        //private void buildStoreMenu()
        //{
        //    if(bAddSplitMenu)
        //    {
        //        MenuItem mnuSplit = new MenuItem("-");
        //        mnuFile.MenuItems.Add(iMenuIndex,mnuSplit);
        //        iMenuIndex++;
        //    }
        //    //iNoOfStores = objStore.getNoOfStores();
        //    //strStores = new string[iNoOfStores,2];
        //    //strStores = objStore.getStores();
        //    for(int i = 0; i < iNoOfStores; i++)
        //    {
        //        dvUserDept.RowFilter = "";
        //        dvUserDept.RowFilter = "HDEPT_TYPE = " + clsGeneral.DEPARTMENT_ADMIN + " OR HDEPT_ID = " + strStores[i,0];
        //        //if(iUserDepartmentId == Convert.ToInt32(strStores[i,0]) || iUserDepartmentType == clsGeneral.DEPARTMENT_ADMIN)
        //        if(dvUserDept.Count > 0)
        //        {
        //            this.mnuStore = new MenuItem(strStores[i,1]);
        //            this.mnuStore.Select += new System.EventHandler(this.mnuStore_Select);

        //            this.mnuS_IssueMedicines = new MenuItem("&Issue " + clsGeneral.getVocabulary(Convert.ToInt32(strStores[i,0])));
        //            this.mnuS_IssueMedicines.Index = 0;
        //            this.mnuS_IssueMedicines.MenuItems.Add("To Departments",new System.EventHandler(this.mnuS_IssMedToDepartment_Click));
        //            this.mnuStore.MenuItems.Add(this.mnuS_IssueMedicines);

        //            this.mnuReceiveMedicines = new MenuItem("&Receive " + clsGeneral.getVocabulary(Convert.ToInt32(strStores[i,0])));
        //            this.mnuReceiveMedicines.Index = 1;
        //            this.mnuReceiveMedicines.MenuItems.Add("From Departments",new System.EventHandler(this.mnuS_RecMedFromDepartments_Click));
        //            this.mnuReceiveMedicines.MenuItems.Add("As Offer",new System.EventHandler(this.mnuS_RecMedAsOffer_Click));
        //            this.mnuStore.MenuItems.Add(this.mnuReceiveMedicines);

        //            this.mnuDamages = new MenuItem("Damages");
        //            this.mnuDamages.Index = 2;
        //            this.mnuDamages.Click += new System.EventHandler(this.mnuDamages_Click);
        //            this.mnuStore.MenuItems.Add(this.mnuDamages);

        //            this.mnuS_Indent = new MenuItem("Indent");
        //            this.mnuS_Indent.Index = 3;
        //            this.mnuS_Indent.MenuItems.Add("Indent List",new System.EventHandler(this.mnuS_IndentList_Click));
        //            this.mnuS_Indent.MenuItems.Add("Prepare Indent",new System.EventHandler(this.mnuS_PrepareIndent_Click));
        //            this.mnuStore.MenuItems.Add(this.mnuS_Indent);

        //            this.mnuOrder = new MenuItem("Order Details");
        //            this.mnuOrder.Index = 4;
        //            this.mnuOrder.MenuItems.Add("Placing Order",new System.EventHandler(this.mnuPlacingOrderToVendor_Click));
        //            this.mnuOrder.MenuItems.Add("Receive Order",new System.EventHandler(this.mnuS_RecMedFromVendors_Click));
        //            this.mnuOrder.MenuItems.Add(clsGeneral.getVocabulary(Convert.ToInt32(strStores[i,0])) + " Details",new System.EventHandler(this.mnuS_MultipleSelectionDrug_Click));
        //            this.mnuStore.MenuItems.Add(this.mnuOrder);

        //            this.mnuS_ToVendor = new MenuItem("Vendors Returns");
        //            this.mnuS_ToVendor.Index = 5;
        //            this.mnuS_ToVendor.MenuItems.Add("Prepare Return List",new System.EventHandler(this.menuItem3_Click));
        //            this.mnuS_ToVendor.MenuItems.Add("Return " + clsGeneral.getVocabulary(Convert.ToInt32(strStores[i,0])),new System.EventHandler(this.mnuS_ReturnMedicines_Click));
        //            this.mnuStore.MenuItems.Add(this.mnuS_ToVendor);

        //            this.mnuStoreStockVerification = new MenuItem("Stock Verification");
        //            this.mnuStoreStockVerification.Index = 7;
        //            this.mnuStoreStockVerification.MenuItems.Add("View Stock Position", new System.EventHandler(this.mnuViewStoreStockPosition_Click));
        //            this.mnuStoreStockVerification.MenuItems.Add("Stock Reposition", new System.EventHandler(this.mnuStoreStockReposition_Click));
        //            this.mnuStoreStockVerification.MenuItems.Add(clsGeneral.getVocabulary(Convert.ToInt32(strStores[i,0])) + " Below Reorder Level", new System.EventHandler(this.mnuStoreBelowReorderLevel_Click));
        //            this.mnuStore.MenuItems.Add(this.mnuStoreStockVerification);

        //            this.mnuUpdateSellingPrice = new MenuItem("Update Selling Price");
        //            this.mnuUpdateSellingPrice.Index = 8;
        //            this.mnuUpdateSellingPrice.MenuItems.Add("Based on " + clsGeneral.getVocabulary(Convert.ToInt32(strStores[i,0])) + " Name", new System.EventHandler(this.mnuStoreDrugName_Click));
        //            this.mnuUpdateSellingPrice.MenuItems.Add("Based on " + clsGeneral.getVocabulary(Convert.ToInt32(strStores[i,0])) + "'s Batch No", new System.EventHandler(this.mnuStoreDrugBatchNo_Click));
        //            this.mnuStore.MenuItems.Add(this.mnuUpdateSellingPrice);

        //            this.mnuInterDept = new MenuItem("Inter Dept Services");
        //            this.mnuInterDept.Index		=	9;
        //            this.mnuInterDept.Click += new System.EventHandler(this.mnuInterDeptstore_Click);
        //            this.mnuStore.MenuItems.Add(this.mnuInterDept);

        //            this.mnuS_Item = new MenuItem("Item");
        //            this.mnuS_Item.Index = 10;
        //            this.mnuS_Item.Click += new System.EventHandler(this.mnuS_Item_Click);
        //            this.mnuStore.MenuItems.Add(this.mnuS_Item);

        //            this.mnuS_Vendor = new MenuItem("Vendor");
        //            this.mnuS_Vendor.Index = 11;
        //            this.mnuS_Vendor.Click += new System.EventHandler(this.mnuS_Vendor_Click);
        //            this.mnuStore.MenuItems.Add(this.mnuS_Vendor);

        //            this.mnuFile.MenuItems.Add(iMenuIndex,this.mnuStore);
        //            iMenuIndex++;
        //        }
        //    }
        //    bAddSplitMenu = true;
        //}


        ///**
        // *  author : ntanand
        // *  date : 27-Oct-2005
        // *  purpose : To build menu with report names belong to Lab Module
        // * */
        //private void buildLabReportMenu()
        //{
        //    if(iUserDepartmentType.IndexOf(clsGeneral.DEPARTMENT_ADMIN.ToString())>=0)
        //    {
        //        MenuItem mnuSplit = new MenuItem("-");
        //        mnuCusReport.MenuItems.Add(mnuSplit);
        //        iMenuIndex++;
        //    }
        //    //iNoOfLabs = objStore.getNoOfLabs();
        //    //strLabs = new string[iNoOfLabs,2];
        //    //strLabs = objStore.getLabs();

        //    for(int i = 0; i < iNoOfLabs; i++)
        //    {
        //        if(iUserDepartmentId == Convert.ToInt32(strLabs[i,0]) || iUserDepartmentType.IndexOf( clsGeneral.DEPARTMENT_ADMIN.ToString())>=0 
        //            || iUserDepartmentType.IndexOf( clsGeneral.DEPARTMENT_LAB.ToString())>=0)
        //        {
        //            this.mnuRptLab = new MenuItem(strLabs[i,1]);
        //            this.mnuRptLab.Select += new System.EventHandler(this.mnuRptLab_Select);

        //            if(clsGeneral.IS_REPORTS_DEPT_WISE!="")
        //              objGeneral.getRptsHdptIdwise(strLabs[i,0],clsGeneral.DEPARTMENT_LAB);
        //            iNoOfLabReports = objGeneral.getNoOfLabReports();
        //            strLabReports = objGeneral.getLabReports();

        //            for (int j=0 ; j< iNoOfLabReports; j++)
        //            {
        //                this.mnuReportItem = new MenuItem(strLabReports[j,1]);
        //                mnuReportItem.Index = j;
        //                this.mnuReportItem.Click += new System.EventHandler(this.mnuReportItem_Click);
        //                this.mnuRptLab.MenuItems.Add(this.mnuReportItem);
        //            }
        //            this.mnuCusReport.MenuItems.Add(mnuRptLab);
        //            iMenuIndex++;

        //        }
        //    }
        //}

        ///**
        // *  author : ntanand
        // *  date : 27-Oct-2005
        // *  purpose : To build menu with report names belong to Recording Module
        // * */

        //private void buildRecordingReportMenu()
        //{
        //    if(iUserDepartmentType.IndexOf(clsGeneral.DEPARTMENT_ADMIN.ToString())>=0)
        //    {
        //        MenuItem mnuSplit = new MenuItem("-");
        //        mnuCusReport.MenuItems.Add(mnuSplit);
        //        iMenuIndex++;
        //    }

        //    //iNoOfRecording = objStore.getNoOfRecording();
        //    //strRecording = new string[iNoOfRecording,2];
        //    //strRecording = objStore.getRecording();

        //    for(int i = 0; i < iNoOfRecording; i++)
        //    {
        //        if(iUserDepartmentId == Convert.ToInt32(strRecording[i,0]) || iUserDepartmentType.IndexOf(clsGeneral.DEPARTMENT_ADMIN.ToString())>0)
        //        {
        //            this.mnuRptRecording = new MenuItem(strRecording[i,1]);
        //            this.mnuRptRecording.Select += new System.EventHandler(this.mnuRptRecording_Select);
        //            if(clsGeneral.IS_REPORTS_DEPT_WISE!="")
        //                objGeneral.getRptsHdptIdwise(strRecording[i,0],clsGeneral.DEPARTMENT_RECORDING);

        //            iNoOfRecordingReports = objGeneral.getNoOfRecordingReports();
        //            strRecordingReports = objGeneral.getRecordingReports();

        //            for (int j=0 ; j< iNoOfRecordingReports; j++)
        //            {
        //                this.mnuReportItem = new MenuItem(strRecordingReports[j,1]);
        //                mnuReportItem.Index = j;
        //                this.mnuReportItem.Click += new System.EventHandler(this.mnuReportItem_Click);
        //                this.mnuRptRecording.MenuItems.Add(this.mnuReportItem);
        //            }
        //            this.mnuCusReport.MenuItems.Add(mnuRptRecording);
        //            iMenuIndex++;
        //        }
        //    }
        //}


        ///**
        // * author : ntanand
        // * date : 27-Oct-2005
        // * purpose : To build menu with report names belong to Store
        // * */
        //private void buildStoreReportMenu()
        //{
        //    if(iUserDepartmentType.IndexOf(clsGeneral.DEPARTMENT_ADMIN.ToString())>=0 ||
        //        iUserDepartmentType.IndexOf( clsGeneral.DEPARTMENT_STORE.ToString())>=0)
        //    {
        //        MenuItem mnuSplit = new MenuItem("-");
        //        mnuCusReport.MenuItems.Add(mnuSplit);
        //        iMenuIndex++;
        //    }

        //    //iNoOfStores = objStore.getNoOfStores();
        //    //strStores = new string[iNoOfStores,2];
        //    //strStores = objStore.getStores();



        //    for(int i = 0; i < iNoOfStores; i++)
        //    {
        //        if(iUserDepartmentId == Convert.ToInt32(strStores[i,0]) || iUserDepartmentType.IndexOf(clsGeneral.DEPARTMENT_ADMIN.ToString())>=0
        //            || iUserDepartmentType.IndexOf( clsGeneral.DEPARTMENT_STORE.ToString())>=0)
        //        {
        //            this.mnuRptStroe = new MenuItem(strStores[i,1]);
        //            this.mnuRptStroe.Select += new System.EventHandler(this.mnuRptStroe_Select);
        //            if(clsGeneral.IS_REPORTS_DEPT_WISE!="")
        //                objGeneral.getRptsHdptIdwise(strStores[i,0],clsGeneral.DEPARTMENT_STORE);
        //            iNoOfStoreReports = objGeneral.getNoOfStoreReports();
        //            strStoreReports = objGeneral.getStoresReports();

        //            for (int j=0 ; j< iNoOfStoreReports; j++)
        //            {
        //                this.mnuReportItem = new MenuItem(strStoreReports[j,1]);
        //                mnuReportItem.Index = j;
        //                this.mnuReportItem.Click += new System.EventHandler(this.mnuReportItem_Click);
        //                this.mnuRptStroe.MenuItems.Add(this.mnuReportItem);
        //            }
        //            this.mnuCusReport.MenuItems.Add(mnuRptStroe);
        //            iMenuIndex++;

        //        }
        //    }
        //}

        ///* *
        // * author : ntanand
        // * date : 27-Oct-2005
        // * purpose : To build menu with report names belong to Pharmacy
        // * */

        //private void buildPharmacyReportMenu()
        //{
        //    if(iUserDepartmentType.IndexOf(clsGeneral.DEPARTMENT_ADMIN.ToString())>=0 ||
        //        iUserDepartmentType.IndexOf( clsGeneral.DEPARTMENT_PHARMACY.ToString())>=0)
        //    {
        //        MenuItem mnuSplit = new MenuItem("-");
        //        mnuCusReport.MenuItems.Add(mnuSplit);
        //        iMenuIndex++;
        //    }

        //    //iNoOfPharmacies = objStore.getNoOfPharmacies();
        //    //strPharmacies = new string[iNoOfPharmacies,2];
        //    //strPharmacies = objStore.getPharmacies();



        //    for(int i = 0; i < iNoOfPharmacies; i++)
        //    {
        //        if(iUserDepartmentId == Convert.ToInt32(strPharmacies[i,0]) || iUserDepartmentType.IndexOf(clsGeneral.DEPARTMENT_ADMIN.ToString())>=0
        //            || iUserDepartmentType.IndexOf( clsGeneral.DEPARTMENT_PHARMACY.ToString())>=0)
        //        {
        //            this.mnuRptPharmacy = new MenuItem(strPharmacies[i,1]);
        //            this.mnuRptPharmacy.Select += new System.EventHandler(this.mnuRptPharmacy_Select);
        //            if(clsGeneral.IS_REPORTS_DEPT_WISE!="")
        //                objGeneral.getRptsHdptIdwise(strPharmacies[i,0],clsGeneral.DEPARTMENT_PHARMACY);
        //            iNoOfPharmacyReports = objGeneral.getNoOfPharmacyReports();
        //            strPharmacyReports = objGeneral.getPharmacyReports();

        //            for (int j=0 ; j< iNoOfPharmacyReports; j++)
        //            {
        //                this.mnuReportItem = new MenuItem(strPharmacyReports[j,1]);
        //                mnuReportItem.Index = j;
        //                this.mnuReportItem.Click += new System.EventHandler(this.mnuReportItem_Click);
        //                this.mnuRptPharmacy.MenuItems.Add(this.mnuReportItem);

        //            }
        //            this.mnuCusReport.MenuItems.Add(mnuRptPharmacy);
        //            iMenuIndex++;

        //        }
        //    }

        //}

        //private void buildPharmacyMenu()
        //{
        //            //if(iUserDepartmentType == clsGeneral.DEPARTMENT_ADMIN)
        //            if(bAddSplitMenu)
        //            {
        //                MenuItem mnuSplit = new MenuItem("-");
        //                mnuFile.MenuItems.Add(iMenuIndex,mnuSplit);
        //                iMenuIndex++;
        //            }

        //            iNoOfPharmacies = objStore.getNoOfPharmacies();
        //            strPharmacies = new string[iNoOfPharmacies,2];
        //            strPharmacies = objStore.getPharmacies();
        //            dh.createDataSet(clsAdminQuery.getMenuConstructorQuery(clsGeneral.DEPARTMENT_PHARMACY),"Pharmacy");
        //            DataTable dtPharmacy  = dh.getDataSet().Tables["Pharmacy"];
        //            DataView  dvPharmacy  = dtPharmacy.DefaultView;
        //            dvPharmacy.RowFilter = "MODULE_ID = "+ clsGeneral.DEPARTMENT_PHARMACY +" AND IS_MENU = 1 ";
        //            try
        //            {	
        //                for(int i = 0; i < iNoOfPharmacies; i++)
        //                {
        //                    dvUserDept.RowFilter = "";
        //                    dvUserDept.RowFilter = "HDEPT_TYPE = " + clsGeneral.DEPARTMENT_ADMIN + " OR HDEPT_ID = " + strPharmacies[i,0];
        //                    //if(iUserDepartmentId == Convert.ToInt32(strPharmacies[i,0]) || iUserDepartmentType == clsGeneral.DEPARTMENT_ADMIN)
        //                    if(dvUserDept.Count > 0)
        //                    {
        //                        this.mnuPharmacy = new MenuItem(strPharmacies[i,1]);
        //                        this.mnuPharmacy.Select += new System.EventHandler(this.mnuPharmacy_Select);
        //                        if(dvPharmacy.Count > 0)
        //                        {
        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = " + (int)clsAdminConstants.MENU_PHARMACY.ISSUE_ITEM +" AND SUB_MENU = 0";
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuIssueMedicines = new MenuItem(dvPharmacy[0]["CAPTION"].ToString());
        //                                    else
        //                                        this.mnuIssueMedicines = new MenuItem("&Issue " + clsGeneral.getVocabulary(Convert.ToInt32(strPharmacies[i,0])));
        //                                }
        //                            }
        //                            //this.mnuIssueMedicines = new MenuItem("&Issue " + clsGeneral.getVocabulary(Convert.ToInt32(strPharmacies[i,0])));
        //                            this.mnuIssueMedicines.Index = 0;

        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = 1 AND SUB_MENU = " + (int)clsAdminConstants.MENU_PHARMACY_ISSUE_ITEM.TO_PATIENTS ;
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuIssueMedicines.MenuItems.Add(dvPharmacy[0]["CAPTION"].ToString(),new System.EventHandler(this.mnuIssueToPatients_Click));
        //                                    else
        //                                        this.mnuIssueMedicines.MenuItems.Add("To Patients",new System.EventHandler(this.mnuIssueToPatients_Click));
        //                                }
        //                            }
        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = 1 AND SUB_MENU = " + (int)clsAdminConstants.MENU_PHARMACY_ISSUE_ITEM.TO_STORE ;
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuIssueMedicines.MenuItems.Add(dvPharmacy[0]["CAPTION"].ToString(),new System.EventHandler(this.mnuReturnToStore_Click));
        //                                    else
        //                                        this.mnuIssueMedicines.MenuItems.Add("To Store",new System.EventHandler(this.mnuReturnToStore_Click));
        //                                }
        //                            }

        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = 1 AND SUB_MENU = " + (int)clsAdminConstants.MENU_PHARMACY_ISSUE_ITEM.TO_WARD_PATIENTS ;
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuIssueMedicines.MenuItems.Add(dvPharmacy[0]["CAPTION"].ToString(), new System.EventHandler(this.mnuIssueDrugToWardPatient_Click));
        //                                    else
        //                                        this.mnuIssueMedicines.MenuItems.Add("To Ward Patient", new System.EventHandler(this.mnuIssueDrugToWardPatient_Click));
        //                                }
        //                            }

        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = 1 AND SUB_MENU = " + (int)clsAdminConstants.MENU_PHARMACY_ISSUE_ITEM.TO_WARD_PATIENTS_NEW ;
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuIssueMedicines.MenuItems.Add(dvPharmacy[0]["CAPTION"].ToString(), new System.EventHandler(this.mnuIssueDrugToWardPatient_New_Click));
        //                                    else
        //                                        this.mnuIssueMedicines.MenuItems.Add("To Ward Patient-New", new System.EventHandler(this.mnuIssueDrugToWardPatient_New_Click));
        //                                }
        //                            }

        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = 1 AND SUB_MENU = " + (int)clsAdminConstants.MENU_PHARMACY_ISSUE_ITEM.TO_INDIVIDUAL_WARD_PATIENTS;
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuIssueMedicines.MenuItems.Add(dvPharmacy[0]["CAPTION"].ToString(), new System.EventHandler(this.mnuP_IssMedToIndWP_Click));
        //                                    else
        //                                        this.mnuIssueMedicines.MenuItems.Add("To Individual Ward Patient", new System.EventHandler(this.mnuP_IssMedToIndWP_Click));
        //                                }
        //                            }

        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = 1 AND SUB_MENU = " + (int)clsAdminConstants.MENU_PHARMACY_ISSUE_ITEM.POST_CHARGES_TO_IP_BILL;
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuIssueMedicines.MenuItems.Add(dvPharmacy[0]["CAPTION"].ToString(), new System.EventHandler(this.mnuP_PostChargesToIPBill_Click));
        //                                    else
        //                                        this.mnuIssueMedicines.MenuItems.Add("Post Charges to IPBill", new System.EventHandler(this.mnuP_PostChargesToIPBill_Click));
        //                                }
        //                            }
        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = 1 AND SUB_MENU = " + (int)clsAdminConstants.MENU_PHARMACY_ISSUE_ITEM.TO_DEPARTMENT;
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuIssueMedicines.MenuItems.Add(dvPharmacy[0]["CAPTION"].ToString(), new System.EventHandler(this.mnuIssueDrugPharmacyToDept_Click));
        //                                    else
        //                                        this.mnuIssueMedicines.MenuItems.Add("&To Department", new System.EventHandler(this.mnuIssueDrugPharmacyToDept_Click));
        //                                }
        //                            }
        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = 1 AND SUB_MENU = " + (int)clsAdminConstants.MENU_PHARMACY_ISSUE_ITEM.REFUND_DEPOSIT_AMOUNT;
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuIssueMedicines.MenuItems.Add(dvPharmacy[0]["CAPTION"].ToString(), new System.EventHandler(this.mnuDepositAmount_Click));
        //                                    else
        //                                        this.mnuIssueMedicines.MenuItems.Add("&Refund Deposit Amount", new System.EventHandler(this.mnuDepositAmount_Click));
        //                                }
        //                            }

        //                            this.mnuPharmacy.MenuItems.Add(this.mnuIssueMedicines);

        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = " + (int)clsAdminConstants.MENU_PHARMACY.RECIEVE_ITEM +" AND SUB_MENU = 0";
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuReceiveMedicines = new MenuItem(dvPharmacy[0]["CAPTION"].ToString());
        //                                    else
        //                                        this.mnuReceiveMedicines = new MenuItem("&Receive " + clsGeneral.getVocabulary(Convert.ToInt32(strPharmacies[i,0])));
        //                                }
        //                            }
        //                            //this.mnuReceiveMedicines = new MenuItem("&Receive " + clsGeneral.getVocabulary(Convert.ToInt32(strPharmacies[i,0])));
        //                            this.mnuReceiveMedicines.Index = 1;

        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = 2 AND SUB_MENU = " + (int)clsAdminConstants.MENU_PHARMACY_RECIEVE_ITEM.RETURN_BY_PATIENTS;
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuReceiveMedicines.MenuItems.Add(dvPharmacy[0]["CAPTION"].ToString(),new System.EventHandler(this.mnuReturnByPatient_Click));
        //                                    else
        //                                        this.mnuReceiveMedicines.MenuItems.Add("Return by &Patients",new System.EventHandler(this.mnuReturnByPatient_Click));
        //                                }
        //                            }

        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = 2 AND SUB_MENU = " + (int)clsAdminConstants.MENU_PHARMACY_RECIEVE_ITEM.RETURN_BY_WARD_PATIENTS;
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuReceiveMedicines.MenuItems.Add(dvPharmacy[0]["CAPTION"].ToString() ,new System.EventHandler(this.mnuReturnDrugIssuedToWardPatient_Click));
        //                                    else
        //                                        this.mnuReceiveMedicines.MenuItems.Add("&Return by Ward-Patient",new System.EventHandler(this.mnuReturnDrugIssuedToWardPatient_Click));
        //                                }
        //                            }

        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = 2 AND SUB_MENU = " + (int)clsAdminConstants.MENU_PHARMACY_RECIEVE_ITEM.RETURN_BY_WARD_PATIENTS_NEW;
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuReceiveMedicines.MenuItems.Add(dvPharmacy[0]["CAPTION"].ToString() ,new System.EventHandler(this.mnuReturnDrugIssuedToWardPatient_new_Click));
        //                                    else
        //                                        this.mnuReceiveMedicines.MenuItems.Add("&Return by Ward-Patient-New",new System.EventHandler(this.mnuReturnDrugIssuedToWardPatient_new_Click));
        //                                }
        //                            }

        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = 2 AND SUB_MENU = " + (int)clsAdminConstants.MENU_PHARMACY_RECIEVE_ITEM.RETURN_BY_WARD_PATIENTS_AFTER_DISCHARGE;
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuReceiveMedicines.MenuItems.Add(dvPharmacy[0]["CAPTION"].ToString() ,new System.EventHandler(this.mnuReturnDrugIssuedToWardPatient_afterDischarge_Click));
        //                                    else
        //                                        this.mnuReceiveMedicines.MenuItems.Add("&Return by Ward-Patient-After Discharge",new System.EventHandler(this.mnuReturnDrugIssuedToWardPatient_afterDischarge_Click));
        //                                }
        //                            }

        //                            this.mnuPharmacy.MenuItems.Add(this.mnuReceiveMedicines);

        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = " + (int)clsAdminConstants.MENU_PHARMACY.REPLACE_ITEM +" AND SUB_MENU = 0";
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuReplaceMedicines = new MenuItem(dvPharmacy[0]["CAPTION"].ToString());
        //                                    else
        //                                        this.mnuReplaceMedicines = new MenuItem("Re&place " + clsGeneral.getVocabulary(Convert.ToInt32(strPharmacies[i,0])));
        //                                }
        //                            }
        ////							this.mnuReplaceMedicines = new MenuItem("Re&place " + clsGeneral.getVocabulary(Convert.ToInt32(strPharmacies[i,0])));
        //                            this.mnuReplaceMedicines.Index = 2;

        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = 3 AND SUB_MENU = " + (int)clsAdminConstants.MENU_PHARMACY_REPLACE_ITEM.FROM_DEPARTMENTS;
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuReplaceMedicines.MenuItems.Add(dvPharmacy[0]["CAPTION"].ToString(), new System.EventHandler(this.mnuRepMedFromStore_Click));
        //                                    else
        //                                        this.mnuReplaceMedicines.MenuItems.Add("From Departments", new System.EventHandler(this.mnuRepMedFromStore_Click));
        //                                }
        //                            }

        //                            this.mnuPharmacy.MenuItems.Add(mnuReplaceMedicines);

        //                            //Damages
        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = " + (int)clsAdminConstants.MENU_PHARMACY.DAMAGES +" AND SUB_MENU = 0";
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuDeptDamages = new MenuItem(dvPharmacy[0]["CAPTION"].ToString());
        //                                    else
        //                                        this.mnuDeptDamages = new MenuItem("Damages");
        //                                }
        //                            }
        ////							this.mnuDeptDamages = new MenuItem("Damages");
        //                            this.mnuDeptDamages.Index = 3;
        //                            this.mnuDeptDamages.Click += new System.EventHandler(this.mnuDeptDamages_Click);
        //                            this.mnuPharmacy.MenuItems.Add(this.mnuDeptDamages);

        //                            //Indent
        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = " + (int)clsAdminConstants.MENU_PHARMACY.INDENT +" AND SUB_MENU = 0";
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuIndent = new MenuItem(dvPharmacy[0]["CAPTION"].ToString());
        //                                    else
        //                                        this.mnuIndent = new MenuItem("&Indent");
        //                                }
        //                            }
        //                            //this.mnuIndent = new MenuItem("&Indent");
        //                            this.mnuIndent.Index = 4;

        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = " + (int)clsAdminConstants.MENU_PHARMACY.INDENT +" AND SUB_MENU = " + (int)clsAdminConstants.MENU_PHARMACY_INDENT.INDENT_LIST;
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuIndent.MenuItems.Add(dvPharmacy[0]["CAPTION"].ToString() , new System.EventHandler(this.mnuPharmacyIndentList_Click));
        //                                    else
        //                                        this.mnuIndent.MenuItems.Add("Indent List", new System.EventHandler(this.mnuPharmacyIndentList_Click));
        //                                }
        //                            }

        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = " + (int)clsAdminConstants.MENU_PHARMACY.INDENT +" AND SUB_MENU = " + (int)clsAdminConstants.MENU_PHARMACY_INDENT.PREPARE_INDENT;
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuReplaceMedicines.MenuItems.Add(dvPharmacy[0]["CAPTION"].ToString(), new System.EventHandler(this.mnuPharmacyPrepareIndent_Click));
        //                                    else
        //                                        this.mnuIndent.MenuItems.Add("Prepare Indent", new System.EventHandler(this.mnuPharmacyPrepareIndent_Click));
        //                                }
        //                            }

        //                            this.mnuPharmacy.MenuItems.Add(mnuIndent);
        //                            //Stock Verification
        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = " + (int)clsAdminConstants.MENU_PHARMACY.STOCK_VERIFICATION +" AND SUB_MENU = 0";
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuStockVerification = new MenuItem(dvPharmacy[0]["CAPTION"].ToString());
        //                                    else
        //                                        this.mnuStockVerification = new MenuItem("Stock &Verification");
        //                                }
        //                            }
        //                            //this.mnuStockVerification = new MenuItem("Stock &Verification");
        //                            this.mnuStockVerification.Index = 5;

        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = 5 AND SUB_MENU = " + (int)clsAdminConstants.MENU_PHARMACY_STOCK_VERIFICATION.VIEW_STOCK_POSITION;
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuReplaceMedicines.MenuItems.Add(dvPharmacy[0]["CAPTION"].ToString(), new System.EventHandler(this.mnuViewStockPosition_Click));
        //                                    else
        //                                        this.mnuStockVerification.MenuItems.Add("View Stock Position", new System.EventHandler(this.mnuViewStockPosition_Click));
        //                                }
        //                            }

        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = 5 AND SUB_MENU = " + (int)clsAdminConstants.MENU_PHARMACY_STOCK_VERIFICATION.STOCK_REPOSITION;
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuReplaceMedicines.MenuItems.Add(dvPharmacy[0]["CAPTION"].ToString(), new System.EventHandler(this.mnuDeptStockReposition_Click));
        //                                    else
        //                                        this.mnuStockVerification.MenuItems.Add("Stock Reposition", new System.EventHandler(this.mnuDeptStockReposition_Click));
        //                                }
        //                            }

        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = 5 AND SUB_MENU = " + (int)clsAdminConstants.MENU_PHARMACY_STOCK_VERIFICATION.DRUG_BELOW_REORDER_LEVEL;
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuReplaceMedicines.MenuItems.Add(dvPharmacy[0]["CAPTION"].ToString(), new System.EventHandler(this.mnuPhrBelowReorderLevel_Click));
        //                                    else
        //                                        this.mnuStockVerification.MenuItems.Add("Drug Below Reorder Level", new System.EventHandler(this.mnuPhrBelowReorderLevel_Click));
        //                                }
        //                            }

        //                            this.mnuPharmacy.MenuItems.Add(this.mnuStockVerification);

        //                            //Transaction History
        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = " + (int)clsAdminConstants.MENU_PHARMACY.TRANSACTION_HISTORY +" AND SUB_MENU = 0";
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuPharmacyTransHistory = new MenuItem(dvPharmacy[0]["CAPTION"].ToString(),new System.EventHandler(this.mnuPharmacyTransHistory_Click));
        //                                    else
        //                                        this.mnuPharmacyTransHistory = new MenuItem("&Transaction History",new System.EventHandler(this.mnuPharmacyTransHistory_Click));
        //                                }
        //                            }
        //                            this.mnuPharmacyTransHistory = new MenuItem("&Transaction History",new System.EventHandler(this.mnuPharmacyTransHistory_Click));
        //                            this.mnuPharmacyTransHistory.Index = 6;
        //                            this.mnuPharmacy.MenuItems.Add(this.mnuPharmacyTransHistory);

        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = " + (int)clsAdminConstants.MENU_PHARMACY.UPDATE_SELLING_PRICE +" AND SUB_MENU = 0";
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuUpdatePharmacySP = new MenuItem(dvPharmacy[0]["CAPTION"].ToString());
        //                                    else
        //                                        this.mnuUpdatePharmacySP = new MenuItem("Update Selling Price");
        //                                }
        //                            }
        //                            //this.mnuUpdatePharmacySP = new MenuItem("Update Selling Price");
        //                            this.mnuUpdatePharmacySP.Index = 7;

        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = 9 AND SUB_MENU = " + (int)clsAdminConstants.MENU_PHARMACY_UPDATE_SELLING_PRICE.BASED_ON_ITEM_NAME;
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuReplaceMedicines.MenuItems.Add(dvPharmacy[0]["CAPTION"].ToString(), new System.EventHandler(this.mnuPharmacyDrugName_Click));
        //                                    else
        //                                        this.mnuUpdatePharmacySP.MenuItems.Add("Based on " + clsGeneral.getVocabulary(Convert.ToInt32(strPharmacies[i,0])) + " Name", new System.EventHandler(this.mnuPharmacyDrugName_Click));
        //                                }
        //                            }

        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = 9 AND SUB_MENU = " + (int)clsAdminConstants.MENU_PHARMACY_UPDATE_SELLING_PRICE.BASED_ON_ITEMS_BATCH_NO;
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuReplaceMedicines.MenuItems.Add(dvPharmacy[0]["CAPTION"].ToString(),  new System.EventHandler(this.mnuPharmacyDrugBatchNo_Click));
        //                                    else
        //                                        this.mnuUpdatePharmacySP.MenuItems.Add("Based on " + clsGeneral.getVocabulary(Convert.ToInt32(strPharmacies[i,0])) + "'s Batch No", new System.EventHandler(this.mnuPharmacyDrugBatchNo_Click));
        //                                }
        //                            }

        //                            this.mnuPharmacy.MenuItems.Add(this.mnuUpdatePharmacySP);

        //                            //Inter Dept Service
        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = " + (int)clsAdminConstants.MENU_PHARMACY.INTER_DEPARTMENT_SERVICE +" AND SUB_MENU = 0";
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuInterDept = new MenuItem(dvPharmacy[0]["CAPTION"].ToString());
        //                                    else
        //                                        this.mnuInterDept = new MenuItem("Inter Dept Service");
        //                                }
        //                            }
        //                            //this.mnuInterDept = new MenuItem("Inter Dept Service");
        //                            this.mnuInterDept.Index = 8;
        //                            this.mnuInterDept.Click += new System.EventHandler(this.mnuInterDept_Click);
        //                            this.mnuPharmacy.MenuItems.Add(this.mnuInterDept);

        //                            //Printer Settings
        //                            dvPharmacy.RowFilter =  "IS_MENU = 1 AND MENU_ITEM = " + (int)clsAdminConstants.MENU_PHARMACY.PRINTER_SETTINGS +" AND SUB_MENU = 0";
        //                            if (dvPharmacy.Count > 0)
        //                            {
        //                                if ( Convert.ToInt32(dvPharmacy[0]["IS_REQUIRED"].ToString()) == 1)
        //                                {
        //                                    if (dvPharmacy[0]["CAPTION"].ToString() != "")
        //                                        this.mnuPrinterSettings = new MenuItem(dvPharmacy[0]["CAPTION"].ToString(),new System.EventHandler(this.mnuPrinterSettings_Click));
        //                                    else
        //                                        this.mnuPrinterSettings = new MenuItem("&Printer Settings",new System.EventHandler(this.mnuPrinterSettings_Click));
        //                                }
        //                            }
        //                            //this.mnuPrinterSettings = new MenuItem("&Printer Settings",new System.EventHandler(this.mnuPrinterSettings_Click));
        //                            this.mnuPrinterSettings.Index = 9;
        //                            this.mnuPharmacy.MenuItems.Add(this.mnuPrinterSettings);
        //                        }
        //                        else
        //                        {
        //                            this.mnuIssueMedicines = new MenuItem("&Issue " + clsGeneral.getVocabulary(Convert.ToInt32(strPharmacies[i,0])));
        //                            this.mnuIssueMedicines.Index = 0;
        //                            this.mnuIssueMedicines.MenuItems.Add("To Patients",new System.EventHandler(this.mnuIssueToPatients_Click));
        //                            this.mnuIssueMedicines.MenuItems.Add("To Store",new System.EventHandler(this.mnuReturnToStore_Click));
        //                            this.mnuIssueMedicines.MenuItems.Add("To Ward Patient", new System.EventHandler(this.mnuIssueDrugToWardPatient_Click));
        //                            this.mnuIssueMedicines.MenuItems.Add("To Ward Patient-New", new System.EventHandler(this.mnuIssueDrugToWardPatient_New_Click));
        //                            this.mnuIssueMedicines.MenuItems.Add("To Individual Ward Patient", new System.EventHandler(this.mnuP_IssMedToIndWP_Click));
        //                            this.mnuIssueMedicines.MenuItems.Add("Post Charges to IPBill", new System.EventHandler(this.mnuP_PostChargesToIPBill_Click));
        //                            this.mnuIssueMedicines.MenuItems.Add("&To Department", new System.EventHandler(this.mnuIssueDrugPharmacyToDept_Click));
        //                            this.mnuIssueMedicines.MenuItems.Add("&Refund Deposit Amount", new System.EventHandler(this.mnuDepositAmount_Click));
        //                            this.mnuPharmacy.MenuItems.Add(this.mnuIssueMedicines);

        //                            this.mnuReceiveMedicines = new MenuItem("&Receive " + clsGeneral.getVocabulary(Convert.ToInt32(strPharmacies[i,0])));
        //                            this.mnuReceiveMedicines.Index = 1;
        //                            this.mnuReceiveMedicines.MenuItems.Add("Return by &Patients",new System.EventHandler(this.mnuReturnByPatient_Click));
        //                            this.mnuReceiveMedicines.MenuItems.Add("&Return by Ward-Patient",new System.EventHandler(this.mnuReturnDrugIssuedToWardPatient_Click));
        //                            this.mnuReceiveMedicines.MenuItems.Add("&Return by Ward-Patient-New",new System.EventHandler(this.mnuReturnDrugIssuedToWardPatient_new_Click));
        //                            this.mnuReceiveMedicines.MenuItems.Add("&Return by Ward-Patient-After Discharge",new System.EventHandler(this.mnuReturnDrugIssuedToWardPatient_afterDischarge_Click));
        //                            this.mnuPharmacy.MenuItems.Add(this.mnuReceiveMedicines);

        //                            this.mnuReplaceMedicines = new MenuItem("Re&place " + clsGeneral.getVocabulary(Convert.ToInt32(strPharmacies[i,0])));
        //                            this.mnuReplaceMedicines.Index = 2;
        //                            this.mnuReplaceMedicines.MenuItems.Add("From Departments", new System.EventHandler(this.mnuRepMedFromStore_Click));
        //                            this.mnuPharmacy.MenuItems.Add(mnuReplaceMedicines);

        //                            this.mnuDeptDamages = new MenuItem("Damages");
        //                            this.mnuDeptDamages.Index = 3;
        //                            this.mnuDeptDamages.Click += new System.EventHandler(this.mnuDeptDamages_Click);
        //                            this.mnuPharmacy.MenuItems.Add(this.mnuDeptDamages);

        //                            this.mnuIndent = new MenuItem("&Indent");
        //                            this.mnuIndent.Index = 4;
        //                            this.mnuIndent.MenuItems.Add("Indent List", new System.EventHandler(this.mnuPharmacyIndentList_Click));
        //                            this.mnuIndent.MenuItems.Add("Prepare Indent", new System.EventHandler(this.mnuPharmacyPrepareIndent_Click));
        //                            this.mnuPharmacy.MenuItems.Add(mnuIndent);

        //                            this.mnuStockVerification = new MenuItem("Stock &Verification");
        //                            this.mnuStockVerification.Index = 5;
        //                            this.mnuStockVerification.MenuItems.Add("View Stock Position", new System.EventHandler(this.mnuViewStockPosition_Click));
        //                            this.mnuStockVerification.MenuItems.Add("Stock Reposition", new System.EventHandler(this.mnuDeptStockReposition_Click));
        //                            this.mnuStockVerification.MenuItems.Add("Drug Below Reorder Level", new System.EventHandler(this.mnuPhrBelowReorderLevel_Click));
        //                            this.mnuPharmacy.MenuItems.Add(this.mnuStockVerification);

        //                            this.mnuPrinterSettings = new MenuItem("&Printer Settings",new System.EventHandler(this.mnuPrinterSettings_Click));
        //                            this.mnuPrinterSettings.Index = 6;
        //                            this.mnuPharmacy.MenuItems.Add(this.mnuPrinterSettings);

        //                            this.mnuPharmacyTransHistory = new MenuItem("&Transaction History",new System.EventHandler(this.mnuPharmacyTransHistory_Click));
        //                            this.mnuPharmacyTransHistory.Index = 7;
        //                            this.mnuPharmacy.MenuItems.Add(this.mnuPharmacyTransHistory);

        //                            this.mnuUpdatePharmacySP = new MenuItem("Update Selling Price");
        //                            this.mnuUpdatePharmacySP.Index = 8;
        //                            this.mnuUpdatePharmacySP.MenuItems.Add("Based on " + clsGeneral.getVocabulary(Convert.ToInt32(strPharmacies[i,0])) + " Name", new System.EventHandler(this.mnuPharmacyDrugName_Click));
        //                            this.mnuUpdatePharmacySP.MenuItems.Add("Based on " + clsGeneral.getVocabulary(Convert.ToInt32(strPharmacies[i,0])) + "'s Batch No", new System.EventHandler(this.mnuPharmacyDrugBatchNo_Click));
        //                            this.mnuPharmacy.MenuItems.Add(this.mnuUpdatePharmacySP);

        //                            this.mnuInterDept = new MenuItem("Inter Dept Service");
        //                            this.mnuInterDept.Index = 9;
        //                            this.mnuInterDept.Click += new System.EventHandler(this.mnuInterDept_Click);
        //                            this.mnuPharmacy.MenuItems.Add(this.mnuInterDept);
        //                        }
        //                        this.mnuFile.MenuItems.Add(iMenuIndex,mnuPharmacy);
        //                        iMenuIndex++;
        //                    }
        //                }
        //                bAddSplitMenu = true;
        //            }
        //            catch{}

        //}

        /* *
         * author	: Joseph Gerald J
         * date		: 30-Oct-2005 09:30 AM
         * purpose	: To build Billing Menu based on the No. of Billing Sections
         * */

        //private void buildBillingMenu()
        //{
        //if (iUserDepartmentType == clsGeneral.DEPARTMENT_ADMIN)
        //    if (bAddSplitMenu)
        //    {
        //        MenuItem mnuSplit = new MenuItem("-");
        //        mnuFile.MenuItems.Add(iMenuIndex, mnuSplit);
        //        iMenuIndex++;
        //    }

        //iNoOfBillingSections = objStore.getNoOfBillingSections();
        //strBillingSections = new string[iNoOfBillingSections, 2];
        //strBillingSections = objStore.getBillingSections();

        //for (int i = 0; i < iNoOfBillingSections; i++)
        //{
        //    dvUserDept.RowFilter = "";
        //    dvUserDept.RowFilter = "HDEPT_TYPE = " + clsGeneral.DEPARTMENT_ADMIN + " OR HDEPT_ID = " + strBillingSections[i, 0];
        //    if (iUserDepartmentId == Convert.ToInt32(strBillingSections[i, 0]) || iUserDepartmentType == clsGeneral.DEPARTMENT_ADMIN)
        //        if (dvUserDept.Count > 0)
        //        {
        //            this.mnuBilling = new MenuItem(strBillingSections[i, 1]);
        //            this.mnuBilling.Select += new System.EventHandler(this.mnuBilling_Select);
        //            this.mnuBilling.Click += new System.EventHandler(this.mnuBilling_Click);
        //            mnuFile.MenuItems.Add(iMenuIndex, this.mnuBilling);
        //            iMenuIndex++;
        //        }
        //}
        //bAddSplitMenu = true;
        //}

        // -----------------------------------------------------------------------

        //private void buildAdminMenu()
        //{
        //    MenuItem mnuSplit = new MenuItem("-");
        //    mnuFile.MenuItems.Add(iMenuIndex,mnuSplit);
        //    iMenuIndex++;

        //    this.mnuAdmin = new MenuItem("Admin");

        //    this.mnuPeople = new MenuItem("People");
        //    this.mnuPeople.Index = 0;
        //    this.mnuPeople.MenuItems.Add("Doctors", new System.EventHandler(this.mnuDoctors_Click));
        //    this.mnuPeople.MenuItems.Add("Users", new System.EventHandler(this.mnuUsers_Click));
        //    this.mnuPeople.MenuItems.Add("Staff Members", new System.EventHandler(this.mnuStaffMembers_Click));
        //    this.mnuPeople.MenuItems.Add("Vendors", new System.EventHandler(this.mnuVendors_Click));
        //    if(this.strUsername.ToLower()=="admin" | this.strUsername.ToUpper()=="ADMIN")
        //    {
        //        this.mnuPeople.MenuItems.Add("Day Report Processing Rights", new System.EventHandler(this.frmDayReportRights_Click));
        //    }
        //    this.mnuAdmin.MenuItems.Add(this.mnuPeople);

        //    this.mnuWard = new MenuItem("Beds");
        //    this.mnuWard.Index = 1;
        //    this.mnuWard.Click += new System.EventHandler(this.mnuWard_Click);
        //    this.mnuAdmin.MenuItems.Add(this.mnuWard);

        //    this.mnuSettings = new MenuItem("Settings");
        //    this.mnuSettings.Index = 2;
        //    this.mnuSettings.MenuItems.Add("Registration", new System.EventHandler(this.mnuRegSetting_Click));
        //    this.mnuSettings.MenuItems.Add("Pharmacy", new System.EventHandler(this.mnuPharSetting_Click));
        //    this.mnuSettings.MenuItems.Add("Ward", new System.EventHandler(this.mnuWardSetting_Click));
        //    this.mnuSettings.MenuItems.Add("Lab", new System.EventHandler(this.mnuLabSetting_Click));
        //    this.mnuSettings.MenuItems.Add("Store", new System.EventHandler(this.mnuStoreSetting_Click));
        //    this.mnuSettings.MenuItems.Add("General Charges", new System.EventHandler(this.mnuGeneralChargesSetting_Click));
        //    this.mnuSettings.MenuItems.Add("Reorder Level",new System.EventHandler(this.mnuReOrderLevel_Click));
        //    this.mnuSettings.MenuItems.Add("Report",new System.EventHandler(this.mnuReportMapping_Click));
        //    this.mnuAdmin.MenuItems.Add(this.mnuSettings);

        //    this.mnuSupportData = new MenuItem("Support Data");
        //    this.mnuSupportData.Index = 3;
        //    this.mnuSupportData.MenuItems.Add("Departments", new System.EventHandler(this.mnuDepartments_Click));
        //    this.mnuSupportData.MenuItems.Add("Cases", new System.EventHandler(this.mnuCases_Click));			

        //    this.mnuSupportData.MenuItems.Add("Payment Types", new System.EventHandler(this.mnuPaymentTypes_Click));
        //    this.mnuSupportData.MenuItems.Add("Patient Types", new System.EventHandler(this.mnuPatientTypes_Click));
        //    this.mnuSupportData.MenuItems.Add("Doctor Degree", new System.EventHandler(this.mnuDoctorDegree_Click));
        //    this.mnuSupportData.MenuItems.Add("Amenities", new System.EventHandler(this.mnuAmenities_Click));
        //    this.mnuSupportData.MenuItems.Add("Hospital Department", new System.EventHandler(this.mnuHospitalDepartment_Click));
        //    this.mnuSupportData.MenuItems.Add("Generic Item", new System.EventHandler(this.mnuGenericItem_Click));
        //    this.mnuSupportData.MenuItems.Add("Item", new System.EventHandler(this.mnuAdminSubItem_Click));
        //    this.mnuSupportData.MenuItems.Add("Item Type", new System.EventHandler(this.mnuItemGroup_Click));
        //    this.mnuSupportData.MenuItems.Add("Measurements", new System.EventHandler(this.mnuItemMeasure_Click));
        //    this.mnuSupportData.MenuItems.Add("Manufacture", new System.EventHandler(this.mnuManufacture_Click));
        //    this.mnuSupportData.MenuItems.Add("Item Mapping", new System.EventHandler(this.mnuItemMapping_Click));
        //    this.mnuSupportData.MenuItems.Add("Issue Dept Mapping", new System.EventHandler(this.mnuIssDeptMapping_Click));
        //    this.mnuSupportData.MenuItems.Add("Doctor Shifts", new System.EventHandler(this.mnuDoctorShifts_Click));
        //    this.mnuSupportData.MenuItems.Add("Corporate Details", new System.EventHandler(this.mnuCorporateDetails_Click));
        //    this.mnuSupportData.MenuItems.Add("Investigation Doctors", new System.EventHandler(this.mnuInvestigationDoctors_Click));
        //    this.mnuSupportData.MenuItems.Add("Ward Charge Category", new System.EventHandler(this.mnuWardChargeCategory_Click));
        //    this.mnuSupportData.MenuItems.Add("Fee Classes", new System.EventHandler(this.mnuFeeClasses_Click));
        //    this.mnuSupportData.MenuItems.Add("Operation Category", new System.EventHandler(this.mnuOperationCategory_Click));
        //    this.mnuSupportData.MenuItems.Add("Operations", new System.EventHandler(this.mnuOperations_Click));
        //    this.mnuSupportData.MenuItems.Add("Map Operation Heads", new System.EventHandler(this.mnuMapOperetionsHeads_Click));

        //    this.mnuAdmin.MenuItems.Add(this.mnuSupportData);

        //    this.mnuAdminRoles = new MenuItem("Roles");
        //    this.mnuAdminRoles.Index= 4;
        //    this.mnuAdminRoles.Click += new System.EventHandler(this.mnuAdminRoles_Click);
        //    this.mnuAdmin.MenuItems.Add(this.mnuAdminRoles);

        //    this.mnuAdminAccounts = new MenuItem("Accounts");
        //    this.mnuAdminAccounts.Index = 5;
        //    this.mnuAdminAccounts.MenuItems.Add("Account Group", new System.EventHandler(this.mnuAdminAccountGroup_Click));
        //    this.mnuAdminAccounts.MenuItems.Add("Account Head", new System.EventHandler(this.mnuAdminAccountHead_Click));
        //    this.mnuAdmin.MenuItems.Add(this.mnuAdminAccounts);

        //    this.mnuAdRegistration = new MenuItem("Registration");
        //    this.mnuAdRegistration.Index = 6;
        //    this.mnuAdRegistration.MenuItems.Add("IP Registration", new System.EventHandler(this.mnuAdRegIP_Click));
        //    this.mnuAdRegistration.MenuItems.Add("OP Registration", new System.EventHandler(this.mnuAdRegOP_Click));
        //    this.mnuAdRegistration.MenuItems.Add("Bed Reservation", new System.EventHandler(this.mnuAdRegBedRes_Click));
        //    this.mnuAdRegistration.MenuItems.Add("Modify Patient Details", new System.EventHandler(this.mnuAdModifyPatient_Click));
        //    this.mnuAdmin.MenuItems.Add(this.mnuAdRegistration);

        //    this.mnuReleaseWardBill = new MenuItem("Release Ward Bill");
        //    this.mnuReleaseWardBill.Index = 7;
        //    this.mnuReleaseWardBill.Click += new System.EventHandler(this.mnuReleaseWardBill_Click);
        //    this.mnuAdmin.MenuItems.Add(this.mnuReleaseWardBill);

        //    this.mnuReport = new MenuItem("Reports");
        //    this.mnuReport.Index = 8;
        //    //string check =objRights.checkStatus(this.strUsername);
        //    //if(this.strUsername.ToLower()=="admin" | this.strUsername.ToUpper()=="ADMIN" |this.strUsername.ToLower()=="administrator" |this.strUsername.ToUpper()=="ADMINISTRATOR")
        //    //{
        //    //    this.mnuReport.MenuItems.Add("Process Day Report", new System.EventHandler(this.mnuProcessDayRpt_Click));
        //    //}
        //    //else if(check =="true")
        //    //{
        //    //    this.mnuReport.MenuItems.Add("Process Day Report", new System.EventHandler(this.mnuProcessDayRpt_Click));
        //    //}

        //    this.mnuReport.MenuItems.Add("Day Report", new System.EventHandler(this.mnuDayReport_Click));
        //    this.mnuReport.MenuItems.Add("Case Report", new System.EventHandler(this.mnuCaseReport_Click));
        //    this.mnuReport.MenuItems.Add("Update Day Report", new System.EventHandler(this.mnuUpdateDayReport_Click));
        //    this.mnuReport.MenuItems.Add("Settle Account", new System.EventHandler(this.mnuSettle_Click));
        //    this.mnuAdmin.MenuItems.Add(this.mnuReport);

        //    this.mnuDataCleaning = new MenuItem("Data Cleaning");
        //    this.mnuDataCleaning.Index = 9;
        //    this.mnuDataCleaning.Click += new EventHandler(mnuDataCleaning_Click);
        //    this.mnuAdmin.MenuItems.Add(this.mnuDataCleaning);

        //    this.mnuDataRestoring = new MenuItem("Data Restoring");
        //    this.mnuDataRestoring.Index = 10;
        //    this.mnuDataRestoring.Click += new EventHandler(mnuDataRestoring_Click);
        //    this.mnuAdmin.MenuItems.Add(this.mnuDataRestoring);

        //    //--Bill Group
        //    this.mnuBillGroup = new MenuItem("Bill Group Mapping");
        //    this.mnuBillGroup.Index= 11;
        //    this.mnuBillGroup.Click += new System.EventHandler(this.mnuBillGroup_Click);
        //    this.mnuAdmin.MenuItems.Add(this.mnuBillGroup);
        //    //--

        //    //--Change Print Status
        //    this.mnuChangePrintStatus = new MenuItem("Change Print Status");
        //    this.mnuChangePrintStatus.Index= 12;
        //    this.mnuChangePrintStatus.Click += new EventHandler(mnuChangePrintStatus_Click);
        //    this.mnuAdmin.MenuItems.Add(this.mnuChangePrintStatus);

        //    this.mnuStorePhrEntry = new MenuItem("Add Item Stock");
        //    this.mnuStorePhrEntry.Index= 13;
        //    this.mnuStorePhrEntry.Click += new System.EventHandler(this.mnuStorePhrEntry_Click);
        //    this.mnuAdmin.MenuItems.Add(this.mnuStorePhrEntry);

        //    this.mnuDoctorConcession = new MenuItem("Doctor Concession");
        //    this.mnuDoctorConcession.Index= 14;
        //    this.mnuDoctorConcession.Click += new EventHandler(mnuDoctorConcession_Click);
        //    this.mnuAdmin.MenuItems.Add(this.mnuDoctorConcession);

        //    this.mnuChangePassword = new MenuItem("Change Password");
        //    this.mnuChangePassword.Index= 14;
        //    this.mnuChangePassword.Click += new EventHandler(mnuChangePassword_Click);
        //    this.mnuAdmin.MenuItems.Add(this.mnuChangePassword);

        //    //this.mnuFile.MenuItems.Add(this.mnuAdmin);
        //}

        private void mnuUpdateDayReport_Click(object sender, EventArgs e)
        {
            //new frmDayReportUpdate().ShowDialog();
        }
        private void mnuSettle_Click(object sender, EventArgs e)
        {
            //new frmBillingSettlement(true).ShowDialog();
        }

        //private void buildLabMenu()
        //{
        //    //if(iUserDepartmentType == clsGeneral.DEPARTMENT_ADMIN)
        //    if(bAddSplitMenu)
        //    {
        //        MenuItem mnuSplit = new MenuItem("-");
        //        mnuFile.MenuItems.Add(iMenuIndex,mnuSplit);
        //        iMenuIndex++;
        //    }

        //    //iNoOfLabs = objStore.getNoOfLabs();
        //    //strLabs = new string[iNoOfLabs,2];
        //    //strLabs = objStore.getLabs();

        //    for(int i = 0; i < iNoOfLabs; i++)
        //    {
        //        dvUserDept.RowFilter = "";
        //        dvUserDept.RowFilter = "HDEPT_TYPE = " + clsGeneral.DEPARTMENT_ADMIN + " OR HDEPT_ID = " + strLabs[i,0];
        //        //if(iUserDepartmentId == Convert.ToInt32(strLabs[i,0]) || iUserDepartmentType == clsGeneral.DEPARTMENT_ADMIN)
        //        if(dvUserDept.Count > 0)
        //        {
        //            this.mnuLaboratory = new MenuItem(strLabs[i,1]);
        //            this.mnuLaboratory.Click += new System.EventHandler(this.mnuLaboratory_Click);
        //            this.mnuFile.MenuItems.Add(iMenuIndex, this.mnuLaboratory);
        //            iMenuIndex++;
        //        }
        //    }
        //    bAddSplitMenu = true;
        //}

        //private void buildRecordingMenu()
        //{
        //    //if(iUserDepartmentType == clsGeneral.DEPARTMENT_ADMIN)
        //    if(bAddSplitMenu)
        //    {
        //        MenuItem mnuSplit = new MenuItem("-");
        //        mnuFile.MenuItems.Add(iMenuIndex,mnuSplit);
        //        iMenuIndex++;
        //    }

        //    //iNoOfRecording = objStore.getNoOfRecording();
        //    //strRecording = new string[iNoOfRecording,2];
        //    //strRecording = objStore.getRecording();

        //    for(int i = 0; i < iNoOfRecording; i++)
        //    {
        //        dvUserDept.RowFilter = "";
        //        dvUserDept.RowFilter = "HDEPT_TYPE = " + clsGeneral.DEPARTMENT_ADMIN + " OR HDEPT_ID = " + strRecording[i,0];
        //        //if(iUserDepartmentId == Convert.ToInt32(strRecording[i,0]) || iUserDepartmentType == clsGeneral.DEPARTMENT_ADMIN)
        //        if(dvUserDept.Count > 0)
        //        {
        //            this.mnuRecording = new MenuItem(strRecording[i,1]);
        //            this.mnuRecording.Index = iMenuIndex;
        //            this.mnuRecording.Click += new System.EventHandler(this.mnuRecording_Click);
        //            iMenuIndex++;
        //            this.mnuFile.MenuItems.Add(this.mnuRecording);
        //        }
        //    }
        //    bAddSplitMenu = true;
        //}
        //To build Ward Menu
        //private void buildWardMenu()
        //{
        //    //if(iUserDepartmentType == clsGeneral.DEPARTMENT_ADMIN)
        //    if(bAddSplitMenu)
        //    {
        //        MenuItem mnuSplit = new MenuItem("-");
        //        mnuFile.MenuItems.Add(iMenuIndex,mnuSplit);
        //        iMenuIndex++;
        //    }

        //    //iNoOfWards = objStore.getNoOfWards();
        //    //strWards = new string[iNoOfWards,2];
        //    //strWards = objStore.getWards();

        //    for(int i = 0; i < iNoOfWards; i++)
        //    {
        //        dvUserDept.RowFilter = "";
        //        dvUserDept.RowFilter = "HDEPT_TYPE = " + clsGeneral.DEPARTMENT_ADMIN + " OR HDEPT_ID = " + strWards[i,0];
        //        //if(iUserDepartmentId == Convert.ToInt32(strWards[i,0]) || iUserDepartmentType == clsGeneral.DEPARTMENT_ADMIN)
        //        if(dvUserDept.Count > 0)
        //        {
        //            this.mnuWardDept = new MenuItem(strWards[i,1]);
        //            this.mnuWardDept.Index = iMenuIndex;
        //            this.mnuWardDept.Click += new System.EventHandler(this.mnuWardDept_Click);
        //            iMenuIndex++;
        //            this.mnuFile.MenuItems.Add(this.mnuWardDept);
        //        }
        //    }
        //    bAddSplitMenu = true;
        //}

        //private void buildJobsMenu()
        //{
        //    if(dtJobs == null) return;
        //    foreach (DataRow drJob in dtJobs.Rows)
        //    {
        //        mnuJobs.MenuItems.Add(drJob["TITLE"].ToString(), new EventHandler(mnuJobs_Click));
        //    }
        //}

        //private void buildRegisterMenu()
        //{
        //    DataTable dtRegModule = new clsGeneral().getRegistersModules();
        //    if(dtRegModule == null) return;
        //    int iIndex = 0;
        //    foreach (DataRow drModu in dtRegModule.Rows)
        //    {
        //        int iModu = Convert.ToInt32(drModu["RPT_MODULE"].ToString());
        //        if(iModu == -1 & (iUserDepartmentType.IndexOf(clsGeneral.DEPARTMENT_REGISTRATION.ToString())>=0 || iUserDepartmentType.IndexOf(clsGeneral.DEPARTMENT_ADMIN.ToString())>=0))
        //        {
        //            mnuIPRegRegister = new MenuItem("IP Registration");
        //            mnuRegisters.MenuItems.Add(iIndex,mnuIPRegRegister);
        //            this.mnuIPRegRegister.Select += new System.EventHandler(this.mnuIPRegRegister_Select);
        //            iIndex++;

        //            DataView dvReg = new DataView(dtRegisters);
        //            dvReg.RowFilter = "RPT_MODULE = -1";
        //            for(int i = 0; i< dvReg.Count; i++)
        //            {
        //                mnuIPRegRegister.MenuItems.Add(dvReg[i]["RPT_NAME"].ToString(), new System.EventHandler(this.mnuReigter_Click));
        //            }

        //        }
        //        else if(iModu == 1 & (iUserDepartmentType == clsGeneral.DEPARTMENT_REGISTRATION.ToString() || iUserDepartmentType == clsGeneral.DEPARTMENT_ADMIN.ToString()))
        //        {
        //            mnuRegisters.MenuItems.Add(iIndex, new MenuItem("-"));
        //            iIndex++;
        //            mnuOPRegRegister = new MenuItem("OP Registration");
        //            mnuRegisters.MenuItems.Add(iIndex, mnuOPRegRegister);
        //            this.mnuOPRegRegister.Select += new System.EventHandler(this.mnuOPRegRegister_Select);
        //            iIndex++;

        //            DataView dvReg = new DataView(dtRegisters);
        //            dvReg.RowFilter = "RPT_MODULE = 1";
        //            for(int i = 0; i< dvReg.Count; i++)
        //            {
        //                mnuOPRegRegister.MenuItems.Add(dvReg[i]["RPT_NAME"].ToString(), new System.EventHandler(this.mnuReigter_Click));
        //            }
        //        }
        //        else if(iModu == 2 & (iUserDepartmentType == clsGeneral.DEPARTMENT_STORE.ToString() || iUserDepartmentType == clsGeneral.DEPARTMENT_ADMIN.ToString()))
        //        {
        //            //iNoOfStores = objStore.getNoOfStores();
        //            //strStores = new string[iNoOfStores,2];
        //            //strStores = objStore.getStores();
        //            for(int i = 0; i < iNoOfStores; i++)
        //            {
        //                if(i==0)
        //                {
        //                    mnuRegisters.MenuItems.Add(iIndex, new MenuItem("-"));
        //                    iIndex++;
        //                }
        //                mnuStoreRegister = new MenuItem(strStores[i,1]);
        //                mnuRegisters.MenuItems.Add(iIndex, mnuStoreRegister);
        //                this.mnuStoreRegister.Select += new System.EventHandler(this.mnuStoreRegister_Select);
        //                iIndex++;

        //                DataView dvReg = new DataView(dtRegisters);
        //                dvReg.RowFilter = "RPT_MODULE = 2";
        //                for(int j = 0; j< dvReg.Count; j++)
        //                {
        //                    mnuStoreRegister.MenuItems.Add(dvReg[j]["RPT_NAME"].ToString(), new System.EventHandler(this.mnuReigter_Click));
        //                }
        //            }
        //        }
        //        else if(iModu == 3 & (iUserDepartmentType == clsGeneral.DEPARTMENT_PHARMACY.ToString() || iUserDepartmentType == clsGeneral.DEPARTMENT_ADMIN.ToString()))
        //        {
        //            //iNoOfPharmacies = objStore.getNoOfPharmacies();
        //            //strPharmacies = new string[iNoOfPharmacies,2];
        //            //strPharmacies = objStore.getPharmacies();

        //            for(int i = 0; i < iNoOfPharmacies; i++)
        //            {
        //                if(i==0)
        //                {
        //                    mnuRegisters.MenuItems.Add(iIndex, new MenuItem("-"));
        //                    iIndex++;
        //                }
        //                mnuPharmacyRegister = new MenuItem(strPharmacies[i,1]);
        //                mnuRegisters.MenuItems.Add(iIndex, mnuPharmacyRegister);
        //                this.mnuPharmacyRegister.Select += new System.EventHandler(this.mnuPharmacyRegister_Select);
        //                iIndex++;

        //                DataView dvReg = new DataView(dtRegisters);
        //                dvReg.RowFilter = "RPT_MODULE = 3";
        //                for(int k = 0; k< dvReg.Count; k++)
        //                {
        //                    mnuPharmacyRegister.MenuItems.Add(dvReg[k]["RPT_NAME"].ToString(), new System.EventHandler(this.mnuReigter_Click));
        //                }
        //            }
        //        }
        //        else if(iModu == 5 & (iUserDepartmentType == clsGeneral.DEPARTMENT_BILLING.ToString() || iUserDepartmentType == clsGeneral.DEPARTMENT_ADMIN.ToString()))
        //        {
        //            //iNoOfBillingSections = objStore.getNoOfBillingSections();
        //            //strBillingSections = new string[iNoOfBillingSections, 2];
        //            //strBillingSections = objStore.getBillingSections();

        //            for(int i = 0; i < iNoOfBillingSections; i++)
        //            {
        //                if(i==0)
        //                {
        //                    mnuRegisters.MenuItems.Add(iIndex, new MenuItem("-"));
        //                    iIndex++;
        //                }
        //                mnuBillingRegister = new MenuItem(strBillingSections[i,1]);
        //                mnuRegisters.MenuItems.Add(iIndex, mnuBillingRegister);
        //                this.mnuBillingRegister.Select += new System.EventHandler(this.mnuBillingRegister_Select);
        //                iIndex++;

        //                DataView dvReg = new DataView(dtRegisters);
        //                dvReg.RowFilter = "RPT_MODULE = 5";
        //                for(int k = 0; k< dvReg.Count; k++)
        //                {
        //                    mnuBillingRegister.MenuItems.Add(dvReg[k]["RPT_NAME"].ToString(), new System.EventHandler(this.mnuReigter_Click));
        //                }
        //            }
        //        }
        //        else if(iModu == 7 & (iUserDepartmentType == clsGeneral.DEPARTMENT_LAB.ToString() || iUserDepartmentType == clsGeneral.DEPARTMENT_ADMIN.ToString()))
        //        {
        //            //iNoOfLabs = objStore.getNoOfLabs();
        //            //strLabs = new string[iNoOfLabs, 2];
        //            //strLabs = objStore.getLabs();

        //            for(int i = 0; i < iNoOfLabs; i++)
        //            {
        //                if(i==0)
        //                {
        //                    mnuRegisters.MenuItems.Add(iIndex, new MenuItem("-"));
        //                    iIndex++;
        //                }
        //                mnuLabRegister = new MenuItem(strLabs[i,1]);
        //                mnuRegisters.MenuItems.Add(iIndex, mnuLabRegister);
        //                this.mnuLabRegister.Select += new System.EventHandler(this.mnuLabRegister_Select);
        //                iIndex++;

        //                DataView dvReg = new DataView(dtRegisters);
        //                dvReg.RowFilter = "RPT_MODULE = 7";
        //                for(int k = 0; k< dvReg.Count; k++)
        //                {
        //                    mnuLabRegister.MenuItems.Add(dvReg[k]["RPT_NAME"].ToString(), new System.EventHandler(this.mnuReigter_Click));
        //                }
        //            }
        //        }
        //    }
        //    mnuRegisters.MenuItems.Add(iIndex, new MenuItem("-"));
        //    iIndex++;
        //    MenuItem mnuRegPrintSetting = new MenuItem("Printer Settings");
        //    mnuRegisters.MenuItems.Add(iIndex, mnuRegPrintSetting);
        //    mnuRegPrintSetting.Click += new System.EventHandler(this.mnuRegPrintSetting_Click);
        //    iIndex++;
        //}

        //private void mnuPharSetting_Click(object sender, System.EventArgs e)
        //{
        //    //    if(objPharSetting == null) objPharSetting = new frmPharSetting();
        //    //    if(objPharSetting.IsDisposed) objPharSetting = new frmPharSetting();
        //    //    objPharSetting.Show();
        //    //    objPharSetting.BringToFront();
        //}

        //private void mnuPrinterSettings_Click(object sender, System.EventArgs e)
        //{
        //    //if(frmPrintPharm == null || frmPrintPharm.IsDisposed)
        //    //{
        //    // frmPrintPharm = new frmPharmacyPrintSettings();
        //    // frmPrintPharm.ShowDialog();
        //    //}
        //}
        //private void openPharmacyIndent(bool isPrepareIndent)
        //{
        //    //if(objPharIndentBrowse == null) objPharIndentBrowse = new frmPharmacyIndentBrowse(iPharmacyId);
        //    //if(objPharIndentBrowse.IsDisposed) objPharIndentBrowse = new frmPharmacyIndentBrowse(iPharmacyId);
        //    //objPharIndentBrowse.MdiParent = this;
        //    //objPharIndentBrowse.Show();
        //    //objPharIndentBrowse.BringToFront();

        //    //if (isPrepareIndent)
        //    //{
        //    //    objPharIndentBrowse.btnPrepareIndent_Click(new Object(), new System.EventArgs());
        //    //}			
        //}

        //private void mnuAmenities_Click(object sender, System.EventArgs e)
        //{
        //    //openSupportData(clsAdminConstants.SUP_AMENITIES_LIST);
        //}

        //private void mnuHospitalDepartment_Click(object sender, System.EventArgs e)
        //{
        //    //openSupportData(clsAdminConstants.SUP_HOSPITAL_DEPARTMENT_LIST);
        //}

        //private void mnuLabSetting_Click(object sender, System.EventArgs e)
        //{
        ////    if(objLabSetting == null) objLabSetting = new frmLabSetting();
        ////    if(objLabSetting.IsDisposed) objLabSetting = new frmLabSetting();
        ////    objLabSetting.Show();
        ////    objLabSetting.BringToFront();
        //}

        //private void mnuWardSetting_Click(object sender, System.EventArgs e)
        //{
        //    //if(objWardSetting == null) objWardSetting = new frmWardSetting(); 
        //    //if(objWardSetting.IsDisposed) objWardSetting = new frmWardSetting(); 
        //    //objWardSetting.Show();
        //    //objWardSetting.BringToFront();
        //}

        //private void mnuStoreSetting_Click(object sender, System.EventArgs e)
        //{
        //    //if(objStoreSetting == null) objStoreSetting = new frmStroeSetting();
        //    //if(objStoreSetting.IsDisposed) objStoreSetting = new frmStroeSetting();
        //    //objStoreSetting.Show();
        //    //objStoreSetting.BringToFront();
        //}

        private void mnuGeneralChargesSetting_Click(object sender, System.EventArgs e)
        {
            //if(objGenChrgSetting == null) objGenChrgSetting = new frmGeneralChargesSettings();
            //if(objGenChrgSetting.IsDisposed) objGenChrgSetting = new frmGeneralChargesSettings();
            //objGenChrgSetting.Show();
            //objGenChrgSetting.BringToFront();
        }

        private void mnuDayReport_Click(object sender, System.EventArgs e)
        {
            if (PrinterSettings.InstalledPrinters.Count == 0)
            {
                MessageBox.Show("Printer is not available. Please install printer", "Payroll");
                return;
            }
            //if(objDayReport == null) objDayReport = new frmDayReport();
            //if(objDayReport.IsDisposed) objDayReport = new frmDayReport();
            //objDayReport.MdiParent=this;
            //objDayReport.BringToFront();
            //objDayReport.Show();
        }
        private void mnuCaseReport_Click(object sender, System.EventArgs e)
        {
            if (PrinterSettings.InstalledPrinters.Count == 0)
            {
                MessageBox.Show("Printer is not available. Please install printer", "Payroll");
                return;
            }
            //if(objCaseReport == null) objCaseReport = new frmDayReportCase();
            //if(objCaseReport.IsDisposed) objCaseReport = new frmDayReportCase();
            //objCaseReport.MdiParent=this;
            //objCaseReport.BringToFront();
            //objCaseReport.Show();
        }

        private void mnuReOrderLevel_Click(object sender, System.EventArgs e)
        {
            //if(objStockLevel == null || objStockLevel.IsDisposed) objStockLevel = new CommonFunctions.UI.frmStockLevel();
            //objStockLevel.Show();
            //objStockLevel.BringToFront();
        }
        private void mnuReportMapping_Click(object sender, System.EventArgs e)
        {
            //if(objReportMap == null || objReportMap.IsDisposed) objReportMap = new UI.Reports.frmReportMapping();
            //objReportMap.Show();
            //objReportMap.BringToFront();
        }
        private void frmDayReportRights_Click(object sender, System.EventArgs e)
        {
            //if(objDayRepRights == null || objDayRepRights.IsDisposed) objDayRepRights = new UI.Admin.frmDayReportRights();
            //objDayRepRights.Show();
            //objDayRepRights.BringToFront();
        }
        private void mnuItemGroup_Click(object sender, System.EventArgs e)
        {
            //openSupportData(clsAdminConstants.ITEM_GROUP);
        }

        private void mnuItemMeasure_Click(object sender, System.EventArgs e)
        {
            //openSupportData(clsAdminConstants.ITEM_MEASURE);
        }

        private void mnuManufacture_Click(object sender, System.EventArgs e)
        {
            //openSupportData(clsAdminConstants.ITEM_MANUFAT);
        }
        private void mnuGenericItem_Click(object sender, System.EventArgs e)
        {
            //openSupportData(clsAdminConstants.SUB_GENERIC_ITEM);
        }
        private void mnuItemMapping_Click(object sender, System.EventArgs e)
        {
            //new frmMapDrugsWithDept().ShowDialog();
        }
        private void mnuIssDeptMapping_Click(object sender, System.EventArgs e)
        {
            //new frmMapIssueDept().ShowDialog();
        }
        private void mnuDoctorShifts_Click(object sender, System.EventArgs e)
        {
            //openSupportData(clsAdminConstants.SUB_DOCTOR_SHIFTS);
        }
        private void mnuCorporateDetails_Click(object sender, System.EventArgs e)
        {
            //openSupportData(clsAdminConstants.SUB_CORPORATE_DETAILS);
        }
        private void mnuInvestigationDoctors_Click(object sender, System.EventArgs e)
        {
            //new frmInvestigationDoctors().ShowDialog();
        }
        //Modified by Peter on 05-01-2007
        private void mnuWardChargeCategory_Click(object sender, System.EventArgs e)
        {
            //openSupportData(clsAdminConstants.SUB_WARD_CHARGE_CATEGORY);
        }
        private void mnuFeeClasses_Click(object sender, System.EventArgs e)
        {
            //openSupportData(clsAdminConstants.FEE_CLASSES);
        }
        private void mnuOperationCategory_Click(object sender, System.EventArgs e)
        {
            //openSupportData(clsAdminConstants.OPERATION_CATEGORY);
        }
        private void mnuOperations_Click(object sender, System.EventArgs e)
        {
            //openSupportData(clsAdminConstants.OPERATION);
        }
        private void mnuMapOperetionsHeads_Click(object sender, System.EventArgs e)
        {
            //new frmWardDefaultCharges(true).ShowDialog();
        }

        private void mnuStoreStockReposition_Click(object sender, System.EventArgs e)
        {
            //if(objStkReposition == null || objStkReposition.IsDisposed)  objStkReposition = new UI.Store.frmStockRepositionBrowse(iStoreId,true);
            //objStkReposition.MdiParent = this;
            //objStkReposition.Show();
            //objStkReposition.BringToFront();
        }

        private void mnuStoreBelowReorderLevel_Click(object sender, System.EventArgs e)
        {
            //if(objStkReport != null || (objStkReport.IsDisposed == false)) objStkReport.Dispose();

            //objStkReport = new frmStockLevelReport(iStoreId, true);
            //objStkReport.MdiParent = this;
            //objStkReport.Show();
            //objStkReport.BringToFront();
        }

        private void mnuStoreDrugName_Click(object sender, System.EventArgs e)
        {
            //if(objDSP == null || objDSP.IsDisposed)
            //{objDSP = new frmUpdateDrugSellingPrice(iStoreId);
            //    objDSP.ShowDialog();
            //}
        }

        private void mnuStoreDrugBatchNo_Click(object sender, System.EventArgs e)
        {
            //if(objSP == null || objSP.IsDisposed)
            //{
            //    objSP = new frmUpdateSellingPrice(iStoreId);
            //    objSP.ShowDialog();
            //}
        }

        private void mnuPharmacyDrugName_Click(object sender, System.EventArgs e)
        {
            //if(objDSP == null || objDSP.IsDisposed)
            //{
            //    objDSP = new frmUpdateDrugSellingPrice(iPharmacyId);
            //    objDSP.ShowDialog();
            //}
        }

        private void mnuPharmacyDrugBatchNo_Click(object sender, System.EventArgs e)
        {
            //if(objSP == null || objSP.IsDisposed)
            //{
            //    objSP = new frmUpdateSellingPrice(iPharmacyId);
            //    objSP.ShowDialog();
            //}
        }

        private void mnuPhrBelowReorderLevel_Click(object sender, System.EventArgs e)
        {
            //if(objStkReport != null || (objStkReport.IsDisposed == false)) objStkReport.Dispose();

            //objStkReport = new frmStockLevelReport(iPharmacyId, false);
            //objStkReport.MdiParent = this;
            //objStkReport.Show();
            //objStkReport.BringToFront();
        }

        private void mnuDeptStockReposition_Click(object sender, System.EventArgs e)
        {
            //if(objDeptStkReposition == null || objDeptStkReposition.IsDisposed)  objDeptStkReposition = new UI.Store.frmStockRepositionBrowse(iPharmacyId,false);
            //objDeptStkReposition.MdiParent = this;
            //objDeptStkReposition.Show();
            //objDeptStkReposition.BringToFront();
        }

        private void mnuViewStockPosition_Click(object sender, System.EventArgs e)
        {
            //new CommonFunctions.UI.frmCurrentStockPosition(iPharmacyId, "PHARMACY").ShowDialog();
        }

        private void mnuProcessDayRpt_Click(object sender, System.EventArgs e)
        {
            //if(objBS == null || objBS.IsDisposed)
            //{
            //    objBS = new frmBillingSettlement(false);
            //    objBS.ShowDialog();
            //}
        }

        private void mnuReleaseWardBill_Click(object sender, System.EventArgs e)
        {
            //if(objReleaseBill == null) objReleaseBill = new frmReleaseBill();
            //if(objReleaseBill.IsDisposed) objReleaseBill = new frmReleaseBill();
            //objReleaseBill.Show();
            //objReleaseBill.BringToFront();
        }

        private void mnuOPRegReport_Click(object sender, System.EventArgs e)
        {
            //if (objRegReport == null)
            //{
            //    objRegReport = new frmRegistrationReport("OP Registration Report");
            //    objRegReport.MdiParent = this;
            //}
            //else if (objRegReport.IsDisposed)
            //{
            //    objRegReport = new frmRegistrationReport("OP Registration Report");
            //    objRegReport.MdiParent = this;
            //}
            //else
            //{
            //    objRegReport.sCaption = "OP Registration Report";
            //    objRegReport.refreshGrid();
            //}
            //objRegReport.Show();
            //objRegReport.BringToFront();
        }

        private void mnuCurrentAdmissinReport_Click(object sender, System.EventArgs e)
        {
            //if (objRegReport == null)
            //{
            //    objRegReport = new frmRegistrationReport("IP Current Admissions");
            //    objRegReport.MdiParent = this;
            //}
            //else if (objRegReport.IsDisposed)
            //{
            //    objRegReport = new frmRegistrationReport("IP Current Admissions");
            //    objRegReport.MdiParent = this;
            //}
            //else
            //{
            //    objRegReport.sCaption = "IP Current Admissions";
            //    objRegReport.refreshGrid();
            //}
            //objRegReport.Show();
            //objRegReport.BringToFront();
        }

        private void mnuIPRegReport_Click(object sender, System.EventArgs e)
        {
            //if (objRegReport == null)
            //{
            //    objRegReport = new frmRegistrationReport("IP Registration Report");
            //    objRegReport.MdiParent = this;
            //}
            //else if (objRegReport.IsDisposed)
            //{
            //    objRegReport = new frmRegistrationReport("IP Registration Report");
            //    objRegReport.MdiParent = this;
            //}
            //else
            //{
            //    objRegReport.sCaption = "IP Registration Report";
            //    objRegReport.refreshGrid();
            //}
            //objRegReport.Show();
            //objRegReport.BringToFront();
        }

        private void mnuReportItem_Click(object sender, System.EventArgs e)
        {
            MenuItem objMenu = (MenuItem)sender;
            string sReportCode = "";

            if (iRptModule == 1)
            {
                for (int i = 0; i < iNoOfOPRegiReports; i++)
                {

                    if (objMenu.Text == strOPRegiReports[i, 1])
                    {
                        sReportCode = strOPRegiReports[i, 0];
                        break;
                    }
                }
            }
            else if (iRptModule == -1)
            {
                for (int i = 0; i < iNoOfIPRegiReports; i++)
                {

                    if (objMenu.Text == strIPRegiReports[i, 1])
                    {
                        sReportCode = strIPRegiReports[i, 0];
                        break;
                    }
                }
            }
            else if (iRptModule == clsGeneral.DEPARTMENT_PHARMACY)
            {
                if (clsGeneral.IS_REPORTS_DEPT_WISE != "")
                {
                    iNoOfPharmacyReports = objGeneral.getNoOfPharmacyReports();
                    strPharmacyReports = objGeneral.getPharmacyReports();
                }

                for (int i = 0; i < iNoOfPharmacyReports; i++)
                {

                    if (objMenu.Text == strPharmacyReports[i, 1])
                    {
                        sReportCode = strPharmacyReports[i, 0];
                        break;
                    }
                }
            }
            else if (iRptModule == clsGeneral.DEPARTMENT_STORE)
            {
                if (clsGeneral.IS_REPORTS_DEPT_WISE != "")
                {
                    iNoOfStoreReports = objGeneral.getNoOfStoreReports();
                    strStoreReports = objGeneral.getStoresReports();
                }
                for (int i = 0; i < iNoOfStoreReports; i++)
                {
                    if (objMenu.Text == strStoreReports[i, 1])
                    {
                        sReportCode = strStoreReports[i, 0];
                        break;
                    }
                }
            }
            else if (iRptModule == clsGeneral.DEPARTMENT_LAB)
            {
                if (clsGeneral.IS_REPORTS_DEPT_WISE != "")
                {
                    iNoOfLabReports = objGeneral.getNoOfLabReports();
                    strLabReports = objGeneral.getLabReports();
                }
                for (int i = 0; i < iNoOfLabReports; i++)
                {
                    if (objMenu.Text == strLabReports[i, 1])
                    {
                        sReportCode = strLabReports[i, 0];
                        break;
                    }
                }
            }
            else if (iRptModule == clsGeneral.DEPARTMENT_BILLING)
            {

                if (clsGeneral.IS_REPORTS_DEPT_WISE != "")
                {
                    iNoOfBillingReports = objGeneral.getNoOfBillingReports();
                    strBillingReports = objGeneral.getBillingReports();
                }

                for (int i = 0; i < iNoOfBillingReports; i++)
                {
                    if (objMenu.Text == strBillingReports[i, 1])
                    {
                        sReportCode = strBillingReports[i, 0];
                        break;
                    }
                }
            }
            else if (iRptModule == clsGeneral.DEPARTMENT_RECORDING)
            {
                for (int i = 0; i < iNoOfRecordingReports; i++)
                {
                    if (objMenu.Text == strRecordingReports[i, 1])
                    {
                        sReportCode = strRecordingReports[i, 0];
                        break;
                    }
                }
            }

            //if(reportCondition== null || reportCondition.IsDisposed)
            //  reportCondition = new frmReportConditions(sReportCode,iRptDeptId,false);
            //reportCondition.ShowDialog();

            /*
            UI.Admin.frmReport customReport = new UI.Admin.frmReport();		
            customReport.MdiParent=this;			
			
            if(customReport.showReport(sReportCode,iRptDeptId))
            {	
                customReport.Show();
                customReport.BringToFront();
            }
            */
        }

        private void mnuReportMoulde_Select(object sender, System.EventArgs e)
        {

        }

        private void mnuRptStroe_Select(object sender, System.EventArgs e)
        {
            MenuItem mnu = (MenuItem)sender;
            for (int i = 0; i < iNoOfStores; i++)
            {
                if (strStores[i, 1] == mnu.Text)
                {
                    iRptDeptId = Convert.ToInt32(strStores[i, 0]);
                    break;
                }
            }
            iRptModule = clsGeneral.DEPARTMENT_STORE;
        }

        private void mnuRptRegistration_Select(object sender, System.EventArgs e)
        {
            MenuItem mnu = (MenuItem)sender;
            if (mnu.Text == "&OP Registration")
                iRptModule = 1;
            else
                iRptModule = -1;
        }

        private void mnuRptPharmacy_Select(object sender, System.EventArgs e)
        {
            MenuItem mnu = (MenuItem)sender;
            for (int i = 0; i < iNoOfPharmacies; i++)
            {
                if (strPharmacies[i, 1] == mnu.Text)
                {
                    iRptDeptId = Convert.ToInt32(strPharmacies[i, 0]);
                    clsGeneral.PHARMACY_NAME = strPharmacies[i, 1];
                    break;
                }
            }
            iRptModule = clsGeneral.DEPARTMENT_PHARMACY;
        }

        private void mnuRptLab_Select(object sender, System.EventArgs e)
        {
            MenuItem mnu = (MenuItem)sender;
            for (int i = 0; i < iNoOfLabs; i++)
            {
                if (strLabs[i, 1] == mnu.Text)
                {
                    iRptDeptId = Convert.ToInt32(strLabs[i, 0]);
                    break;
                }
            }
            iRptModule = clsGeneral.DEPARTMENT_LAB;
        }

        private void mnuRptBilling_Select(object sender, System.EventArgs e)
        {
            MenuItem mnu = (MenuItem)sender;
            for (int i = 0; i < iNoOfBillingSections; i++)
            {
                if (strBillingSections[i, 1] == mnu.Text)
                {
                    iRptDeptId = Convert.ToInt32(strBillingSections[i, 0]);
                    break;
                }
            }
            iRptModule = clsGeneral.DEPARTMENT_BILLING;
        }

        private void mnuRptRecording_Select(object sender, System.EventArgs e)
        {
            MenuItem mnu = (MenuItem)sender;
            for (int i = 0; i < iNoOfRecordingReports; i++)
            {
                if (strRecording[i, 1] == mnu.Text)
                {
                    iRptDeptId = Convert.ToInt32(strRecording[i, 0]);
                    break;
                }
            }
            iRptModule = clsGeneral.DEPARTMENT_RECORDING;
        }

        private void label2_Click(object sender, System.EventArgs e)
        {
            //frmDrugPosition objDP = new frmDrugPosition("Drug Reposition");
            //objDP.ShowDialog();
        }

        private void mnuOPCensus_Click(object sender, System.EventArgs e)
        {
            //if(objReportCond==null || objReportCond.IsDisposed)objReportCond = new frmReportConditions("OPCENSUSREGISTER",0,true);
            //objReportCond.ShowDialog();
        }

        private void mnuIPCensus_Click(object sender, System.EventArgs e)
        {
            //if(objReportCond==null || objReportCond.IsDisposed)objReportCond = new frmReportConditions("IPCENSUSREGISTER",0,true);
            //objReportCond.ShowDialog();
        }

        private void mnuBirthRegister_Click(object sender, System.EventArgs e)
        {
            //if(objReportCond==null || objReportCond.IsDisposed)objReportCond = new frmReportConditions("BIRTHREGISTER",0,true);
            //objReportCond.ShowDialog();
        }

        private void mnuDeathRegister_Click(object sender, System.EventArgs e)
        {
            //if(objReportCond==null || objReportCond.IsDisposed)objReportCond = new frmReportConditions("DEATHREGISTER",0,true);
            //objReportCond.ShowDialog();
        }

        private void mnuPharmacyStockRegister_Click(object sender, System.EventArgs e)
        {
            //if(objReportCond==null || objReportCond.IsDisposed)objReportCond = new frmReportConditions("", 0,true);
            //objReportCond.ShowDialog();
        }

        private void mnuStorePurchaseRegister_Click(object sender, System.EventArgs e)
        {
            //if(objReportCond==null || objReportCond.IsDisposed)objReportCond = new frmReportConditions("PURCHASEREGISTER",0,true);
            //objReportCond.ShowDialog();
        }

        private void mnuStoreStockRegister_Click(object sender, System.EventArgs e)
        {
            //if(objReportCond==null || objReportCond.IsDisposed)objReportCond = new frmReportConditions("",0,true);
            //objReportCond.ShowDialog();
        }

        private void mnuRegDailySales_Click(object sender, System.EventArgs e)
        {
            //if(objReportCond==null || objReportCond.IsDisposed)objReportCond = new frmReportConditions("DAILY_SALES_REG", 0,true);
            //objReportCond.ShowDialog();
        }

        private void mnuRegPurcAccount_Click(object sender, System.EventArgs e)
        {
            //if(objReportCond==null || objReportCond.IsDisposed)objReportCond = new frmReportConditions("PURC_ACC_RETAILER", 0,true);
            //objReportCond.ShowDialog();
        }

        private void mnuJobs_Click(object sender, EventArgs e)
        {
            MenuItem mnu = (MenuItem)sender;
            string sReptName = mnu.Text;
            string sReptCode = "";
            DataView dvJobs = new DataView(dtJobs);
            foreach (DataRow dr in dtJobs.Rows)
            {
                if (dr["TITLE"].ToString() == sReptName)
                {
                    sReptCode = dr["CODE"].ToString();
                    break;
                }
            }
            //if(objReportCond==null || objReportCond.IsDisposed)
            //    objReportCond = new frmReportConditions(sReptCode, true);
            //objReportCond.ShowDialog();
        }

        private void mnuReigter_Click(object sender, System.EventArgs e)
        {
            MenuItem mnu = (MenuItem)sender;
            string sReptName = mnu.Text;
            string sReptCode = "";
            DataView dvReg = new DataView(dtRegisters);
            dvReg.RowFilter = "RPT_MODULE = " + iRegisModuId.ToString();
            for (int i = 0; i < dvReg.Count; i++)
            {
                if (dvReg[i]["RPT_NAME"].ToString() == sReptName)
                {
                    sReptCode = dvReg[i]["RPT_CODE"].ToString();
                    break;
                }
            }
            //if(objReportCond==null || objReportCond.IsDisposed)objReportCond = new frmReportConditions(sReptCode,iRegisDeptId,true);
            //objReportCond.ShowDialog();
        }
        private void mnuIPRegRegister_Select(object sender, System.EventArgs e)
        {
            iRegisModuId = -1;
        }
        private void mnuOPRegRegister_Select(object sender, System.EventArgs e)
        {
            iRegisModuId = 1;
        }
        private void mnuStoreRegister_Select(object sender, System.EventArgs e)
        {
            MenuItem mnu = (MenuItem)sender;
            for (int i = 0; i < iNoOfStores; i++)
            {
                if (strStores[i, 1] == mnu.Text)
                {
                    iRegisDeptId = Convert.ToInt32(strStores[i, 0]);
                    break;
                }
            }
            iRegisModuId = 2;
        }
        private void mnuPharmacyRegister_Select(object sender, System.EventArgs e)
        {
            MenuItem mnu = (MenuItem)sender;
            for (int i = 0; i < iNoOfPharmacies; i++)
            {
                if (strPharmacies[i, 1] == mnu.Text)
                {
                    iRegisDeptId = Convert.ToInt32(strPharmacies[i, 0]);
                    break;
                }
            }
            iRegisModuId = 3;
        }
        private void mnuBillingRegister_Select(object sender, System.EventArgs e)
        {
            MenuItem mnu = (MenuItem)sender;
            for (int i = 0; i < iNoOfBillingSections; i++)
            {
                if (strBillingSections[i, 1] == mnu.Text)
                {
                    iRegisDeptId = Convert.ToInt32(strBillingSections[i, 0]);
                    break;
                }
            }
            iRegisModuId = 5;
        }

        private void mnuLabRegister_Select(object sender, System.EventArgs e)
        {
            MenuItem mnu = (MenuItem)sender;
            for (int i = 0; i < iNoOfLabs; i++)
            {
                if (strLabs[i, 1] == mnu.Text)
                {
                    iRegisDeptId = Convert.ToInt32(strLabs[i, 0]);
                    break;
                }
            }
            iRegisModuId = 7;
        }

        private void mnuRegPrintSetting_Click(object sender, System.EventArgs e)
        {
            //if(objPSR == null || objPSR.IsDisposed)objPSR = new frmPrinterSettingsRegister();
            //objPSR.ShowDialog(); 			
        }

        //		private void label1_Click(object sender, System.EventArgs e)
        //		{
        //			Ward.UI.frmWardPatientReturns objWPReturns = new frmWardPatientReturns(245);
        //			objWPReturns.ShowDialog();
        //		}

        private void mdiPayroll_Load(object sender, System.EventArgs e)
        {
            //	lblHospitalName.Text = clsGeneral.GetHospitalName();
            ////	clsGeneral.setSampleNoCaption();
            //	new clsGeneral().loadShortCuts(ref strShortcuts, ref strDeptIds);
            //if (File.Exists(Application.StartupPath + "\\PayrollLOGO.jpg"))
            //    picHospitalLogo.Image = Image.FromFile(Application.StartupPath + "\\PayrollLOGO.jpg");
            //else
            //    picHospitalLogo.Image = null;
        }

        private void mnuAbount_Click(object sender, System.EventArgs e)
        {
            //new frmAbount().ShowDialog();
        }

        private void mdiPayroll_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //Name : James , Date : 18-May-2006
            //The Shortcut keys for the modules are to be captures here
            //			string sDeptId = checkShortCuts(e.KeyCode.ToString().ToUpper());
            //			if(sDeptId!="")
            //			{
            //				MessageBox.Show(sDeptId);
            //			}

        }
        private string checkShortCuts(string sKey)
        {
            string sDeptId = "";
            try
            {
                for (int i = 0; i <= strShortcuts.GetUpperBound(0); i++)
                    if (sKey == strShortcuts[i])
                    {
                        sDeptId = strDeptIds[i];
                        break;
                    }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Payroll");
            }
            return sDeptId;
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin objLogin = new frmLogin();
            objLogin.ShowDialog();
            if (clsGeneral.USER_NAME != "")
            {
                enableMenu(true);
                OpenPayroll();
            }
        }

        private void enableMenu(bool isLogin)
        {
            if (isLogin)
            {
                loginToolStripMenuItem.Enabled = false;
                logoutToolStripMenuItem.Enabled = payRollToolStripMenuItem.Enabled = true;
            }
            else
            {
                loginToolStripMenuItem.Enabled = true;
                logoutToolStripMenuItem.Enabled = payRollToolStripMenuItem.Enabled = false;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateLogout();
            foreach (Form form in this.MdiChildren)
            {
                form.Close();
            }
            clsGeneral.USER_NAME = string.Empty;
            enableMenu(false);
        }

        private void updateLogout()
        {
 
        }

        private void mnuMain_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            LinearGradientBrush lb = new LinearGradientBrush(mnuMain.ClientRectangle, GeneralColor.Color1, GeneralColor.Color2, GeneralColor.ColorAngle);
            g.FillRectangle(lb, mnuMain.ClientRectangle);
        }

        private void payRollToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
            {
                form.Close();
            }
            OpenPayroll();
        }

        private void OpenPayroll()
        {
            if (!File.Exists(Application.StartupPath + "\\" + clsGeneral.PAYROLL_REPORT_INPUT_FILE))
            {
                File.Create(Application.StartupPath + "\\" + clsGeneral.PAYROLL_REPORT_INPUT_FILE);
            }
            if (objPayrollBrowse == null || objPayrollBrowse.IsDisposed)
                objPayrollBrowse = new frmPayrollBrowse();
            objPayrollBrowse.MdiParent = this;
            objPayrollBrowse.Dock = DockStyle.Fill;
            
            long pr = objPRGW.GetCurrentPayroll();
            if (pr == 0)
            {
                if (MessageBox.Show("No Payroll exists. Do you want to Create a New Payroll ?", "Payroll", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //frmCreatePayroll objCreate = new frmCreatePayroll();
                    //if (objCreate.ShowDialog() == DialogResult.No)
                    //    return;
                    objPayrollBrowse.Show();
                }
                else
                    return;

            }
            else
                objPayrollBrowse.Show();
        }

    }
}