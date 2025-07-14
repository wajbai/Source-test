using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.Utils.Frames;
using ACPP.Modules.Master;
using ACPP.Modules.Transaction;
using Bosco.Utility;
using DevExpress.XtraSplashScreen;
using ACPP.Modules.User_Management;
using DevExpress.XtraNavBar;
using System.Reflection;
using DevExpress.XtraNavBar.ViewInfo;
using Bosco.Model.UIModel;
using DevExpress.XtraBars.Docking;
using Bosco.Model.Setting;
using Bosco.Model;
using Bosco.Utility.ConfigSetting;
using ACPP.Modules.Data_Utility;
using DevExpress.XtraEditors;
using ACPP.Modules.TDS;
using Bosco.DAO;
using System.IO;
using ACPP.Modules.Dsync;
using Bosco.Model.Dsync;
using PAYROLL.Modules;
using Bosco.Model.UIModel.Master;
using System.Configuration;
using ACPP.Modules.Inventory;
using ACPP.Modules.Inventory.Asset;
using ACPP.Modules.Inventory.Stock;
using ACPP.Modules.Asset;
using ACPP.Modules.Asset.Transactions;
using ACPP.Modules;
using DevExpress.XtraTabbedMdi;
using System.Collections.Generic;
using ACPP.Modules.ProspectsDonor;
using System.ComponentModel;
using Bosco.Model.TallyMigration;
using DevExpress.Utils;
using System.Collections;
using Bosco.DAO.Configuration;
using System.Net.NetworkInformation;
using System.Resources;
using System.ServiceModel;
using System.Xml.Linq;
using System.Xml;
using System.Diagnostics;
using DevExpress.XtraEditors.Container;
using DevExpress.Utils.Controls;
using Bosco.Model.Transaction;


namespace ACPP
{
    public partial class frmMain : frmFinanceBase
    {
        #region Declaration
        ApplicationCaption8_1 captionPanel;
        //by alwar on 14/12/2015 to track clikced mdi child forms and its main and sub means..
        public Dictionary<string, object> dicOpenedFormmLeftPanel = new Dictionary<string, object>();
        // int AccessRights = 0;
        string projectSelectionWindow = "";
        public static bool IsProfileClicked = true;
        public static string groupNavName = "";
        private bool isEnableMenu = false;
        private bool isEnableSettings = false;
        private bool isEnableMasters = false;
        private bool isEnableView = false;
        private bool isEnableMembers = false;
        private bool isEnableOptions = false;
        private bool isEnableFCPurpose = false;
        private bool isEnableTransaction = false;
        private bool isEnableFixedDeposit = false;
        private bool isEnableUserRights = false;
        private bool isEnableDataUtility = false;
        private bool isEnableAssetMasters = false;
        private bool isEnableAssetTransaction = false;
        private bool isEnableAssetView = false;
        private bool isEnableAssetOptions = false;
        private bool isEnablePayrollMaster = false;
        private bool isEnablePayrollLoanIssues = false;
        private bool isEnablePayrollGateway = false;
        private bool isEnableInventory = false;
        private bool isEnableStock = false;
        private bool isEnableStockMaster = false;
        private bool isEnableStockTransaction = false;
        private bool isEnablePayroll = false;
        private bool isEnableHeadOffieInterface = false;
        private bool isEnableDonorMasters = false;
        private bool isEnableDonorMailDesk = false;
        private bool isEnableDonorContactDesk = false;
        private string MenuCaption = string.Empty;

        //public static string SettingsTitle = "Settings";
        //public static string FinanceTitle = "Finance";
        //public static string NetworkTitle = "Networking";
        //public static string UserManagement = "";
        //public static string DataUtility = "Utilities";

        public static string SettingsTitle = "";
        public static string FinanceTitle = "";
        public static string NetworkTitle = "";
        public static string UserManagement = "";
        public static string DataUtility = "";
        public static string PayrollTitle = "";

        string BackupDirectoryPath = string.Empty;
        string RestorePath = string.Empty;
        //private static string LoginUserId = "1";
        BackupAndRestore BackRestore = new BackupAndRestore();
        private bool AutomaticUpdate = false;
        ResultArgs resultArgs = null;
        //For automatic upload database, current database is getting uploaded during the development
        //so lock upload database logic if system is local  development systmn
        bool isDevelopmentSystem = false;
        string NewFeaturesInPortal = string.Empty;


        //On 09/12/2020, If Branch is De-activated in Portal, We should give proper message and close the application -----------
        bool Is_Branch_Active = true; //By default, will be active becusae of the internet. If internet is avilable, will validate it with Portal Branch
        //------------------------------------------------------------------------------------------------------------

        bool ReceiptModuleStatusShownToolTip = false;
        ToolTipController ctltooltipProviceRightsTileControl = new ToolTipController();
        string VoucherEntryGraceDaysToolTipMessage = string.Empty;

        #endregion

        #region Payroll Declaration
        clsPayrollBase PayrollSubMenu = new clsPayrollBase();
        private DialogResult diares { get; set; }
        #endregion

        #region CommonInterface Properties
        public DateEdit dtBaseDate;
        public GridLookUpEdit dtBaseProject;
        public const string PROJECTTAG = "PROJECT";
        public const string DATETAG = "DATE";
        #endregion

        #region Properties
        //Added by Carmel Raj 
        #region Set Visible Global Properties
        public bool SetVisibleCloseAllTabs
        {
            set
            {
                if (value)
                {
                    //bbiCloseAll.Caption = "Close all Tabs  <Color=Blue><b>Ctrl+Shift+F4</b></color>";
                    bbiCloseAll.Caption = this.GetMessage(MessageCatalog.Master.Module.MODULE_SET_VISIBLE_CLOSE_ALL_TABS);
                }
                else
                {
                    //bbiCloseAll.Caption = "Close all Tabs  <Color=Gray><b>Ctrl+Shift+F4</b></color>";
                    bbiCloseAll.Caption = this.GetMessage(MessageCatalog.Master.Module.MODULE_SET_VISIBLE_CLOSE_ALL_TABS);
                }
                bbiCloseAll.Enabled = value;
            }
        }
        public bool SetVisibleDate
        {
            set
            {
                if (value)
                {
                    //bbidateSelecteionShortCut.Caption = "Date  <Color=Blue><b>(F3)</b></color>";
                    bbidateSelecteionShortCut.Caption = this.GetMessage(MessageCatalog.Master.Module.MODULE_SET_VISIBLE_DATE_TAB);
                }
                else
                {
                    //bbidateSelecteionShortCut.Caption = "Date  <Color=Gray><b>(F3)</b></color>";
                    bbidateSelecteionShortCut.Caption = this.GetMessage(MessageCatalog.Master.Module.MODULE_SET_VISIBLE_DATE_TAB);
                }
                bbidateSelecteionShortCut.Enabled = value;
            }
        }
        public bool SetVisibleProject
        {
            set
            {
                if (value)
                {
                    //bbiProject.Caption = "Project <Color=Blue><b>(F5)</b></color>";
                    bbiProject.Caption = this.GetMessage(MessageCatalog.Master.Module.MODULE_SET_VISIBLE_PROJECT_TAB);
                }
                else
                {
                    //bbiProject.Caption = "Project <Color=Gray><b>(F5)</b></color>";
                    bbiProject.Caption = this.GetMessage(MessageCatalog.Master.Module.MODULE_SET_VISIBLE_PROJECT_TAB);
                }
                bbiProject.Enabled = value;
            }
        }
        public bool SetVisibleConfiguration
        {
            set
            {
                if (value)
                {
                    //bbiConfiguration.Caption = "Configuration <Color=Blue><b>(F12)</b></color>";
                    bbiConfiguration.Caption = this.GetMessage(MessageCatalog.Master.Module.MODULE_SET_VISIBLE_CONFIGURATION_TAB);
                }
                else
                {
                    //bbiConfiguration.Caption = "Configuration <Color=Gray><b>(F12)</b></color>";
                    bbiConfiguration.Caption = this.GetMessage(MessageCatalog.Master.Module.MODULE_SET_VISIBLE_CONFIGURATION_TAB);
                }
                bbiConfiguration.Enabled = value;
            }
        }

        public bool SetCloseAllTabsVisibility
        {
            get
            {
                bool returnVal = false;
                if (this.MdiChildren.Count() > 1)
                {
                    returnVal = true;
                }
                else if (this.MdiChildren.Count() == 1 && this.MdiChildren[0].Name == typeof(frmLoginDashboard).Name)
                {
                    returnVal = false;
                }
                else if (this.MdiChildren.Count() != 0 && this.MdiChildren[0].Name != typeof(frmLoginDashboard).Name)
                {
                    returnVal = true;
                }
                return returnVal;
            }
        }
        #endregion

        public bool ShowHideLeftMenuBar
        {
            set
            {
                // ribbonMain.Enabled = value;
                //rpHome.Visible = value;
                leftMenuBar.Visible = value;
            }
        }

        public DockVisibility ShowHideDockPanel
        {
            set
            {
                dpAcme.Visibility = value;
            }
        }

        public string PageTitle
        {
            set { captionPanel.Text = value; }
        }

        //public string NavGroupName
        //{
        //    get { return groupNavName; }
        //    set { groupNavName = value; }
        //}

        private int activeNavGroupId = 0;
        public int ActiveNavGroupId
        {
            get { return activeNavGroupId; }
            set { activeNavGroupId = value; }
        }

        private string activeAcId = string.Empty;
        public string ActiveAccId
        {
            get { return this.AppSetting.AccPeriodId; }
            set { activeAcId = value; }
        }

        public string UserName
        {
            set
            {
                // bsiUserName.Caption = "";

                if (value != "")
                {
                    //bsiUserName.Caption = "Welcome! " + value;
                    //bsiLoggedUser.Edit.NullText = "Welcome " + value;
                    pceLoggedUser.Text = value;

                    //29/11/2018, To show active connected database if multi db feature enabled
                    if (!String.IsNullOrEmpty(SettingProperty.ActiveDatabaseName) && (this.AppSetting.AccesstoMultiDB == (int)YesNo.Yes))
                    {
                        pceLoggedUser.Width = 200;
                        pceLoggedUser.Text = value + " (" + SettingProperty.ActiveDatabaseAliasName + ")";
                        pceLoggedUser.Left = this.Width - (pceLoggedUser.Width + 25);
                    }

                    //pceLoggedUser.Properties.NullText = "Welcome " + value;
                    pceLoggedUser.Properties.NullText = this.GetMessage(MessageCatalog.Common.COMMON_USER_WELCOME_INFO) + value;
                    // visiblePageHeaderLinks(BarItemVisibility.Always);
                    visiblePageHeaders(true);
                    //pceLoggedUser.Visible = true;
                }
            }
        }

        private string transactionPeriod = "";
        public string TransactionPeriod
        {
            set
            {
                if (value != "")
                {
                    transactionPeriod = value;
                }
            }
            get { return transactionPeriod; }
        }

        public string SetTransaction
        {
            set
            {
                //lblProject.Text = value;
                this.Text = value;
            }
        }

        private string userId = "";
        public string UserId
        {
            set
            {
                if (value != "")
                {
                    userId = value;
                }
            }
            get { return userId; }
        }

        private int UserRoleId;
        public int RoleId
        {
            set
            {
                if (value != 0)
                {
                    UserRoleId = value;
                }

            }
            get { return UserRoleId; }
        }

        private int defaultProjectId = 0;
        public int DefaultProjectId
        {
            set
            {
                if (value != 0)
                {
                    defaultProjectId = value;
                }
            }
            get { return defaultProjectId; }
        }

        private string defaultProject = "";
        public string DefaultProject
        {
            set
            {
                if (value != "")
                {
                    defaultProject = value;
                }
            }
            get { return defaultProject; }
        }

        public string ProjectSelection
        {
            set
            {
                projectSelectionWindow = value;
            }
            get
            {
                return projectSelectionWindow;
            }
        }

        private int recentProjectId = 0;
        public int RecentProjectId
        {
            get { return recentProjectId; }
            set { recentProjectId = value; }
        }

        private int selectionType = 0;
        public int SelectionType
        {
            get { return selectionType; }
            set { selectionType = value; }
        }

        private string recentProject = "";
        public string RecentProject
        {
            get { return recentProject; }
            set { recentProject = value; }
        }

        private string recentVoucherDate = "";
        public string RecentVoucherDate
        {
            get { return recentVoucherDate; }
            set { recentVoucherDate = value; }
        }

        private DateTime TransactionLockFromDate { get; set; }

        private DateTime TransactionLockToDate { get; set; }


        /// <summary>
        /// On 26/06/2019, reload home page
        /// </summary>
        /// <returns></returns>
        private frmLoginDashboard DasboardForm()
        {
            frmLoginDashboard dashboard = null;
            foreach (frmFinanceBase frm in this.MdiChildren)
            {
                if (frm.Name == typeof(frmLoginDashboard).Name)
                {
                    dashboard = (frmLoginDashboard)frm;
                    break;
                }
            }
            return dashboard;
        }
        #endregion

        #region AppCaption
        public class AppCpation : ApplicationCaption8_1
        {
            protected override Image DXLogo
            {
                get
                {
                    return new Bitmap(5, 5);
                }
            }

            protected override void ResetImage()
            {
                base.ResetImage();
            }
        }

        #endregion

        #region constructor
        public frmMain()
        {
            InitializeComponent();
            InitializeTitlePanel();
            // InitSkinGallery();
            ShowHideLeftMenuBar = false;
            // dpAcme.Visibility = DockVisibility.Hidden;

            // Module Titles
            SettingsTitle = this.GetMessage(MessageCatalog.Master.Module.MODULE_SETTING_TITLE);
            FinanceTitle = this.GetMessage(MessageCatalog.Master.Module.MODULE_FINANCE_TITLE);
            NetworkTitle = this.GetMessage(MessageCatalog.Master.Module.MODULE_NETWOKING_TITTLE);
            DataUtility = this.GetMessage(MessageCatalog.Master.Module.MODULE_DATA_UTILITY_TITLE);

            //Only for SDBINM..Check and Update in Master ledger and HO ledger
            CorrectSDBINMLedgersName();

            // Temp
            // it is temporalily i commands
            InsertAssetItem();

            if (AppSetting.IS_CMF_CONGREGATION && AppSetting.HeadofficeCode.ToUpper() == "CMFNED")
            {
                this.UpdateDefaultCMFNEReportApprovedSign();
            }
            else if (AppSetting.IS_BSG_CONGREGATION && AppSetting.HeadofficeCode.ToUpper() == "BSGESP")
            {    //On 28/02/2024, Update Default Sign4 and Sign5 (Budget Approval Image) For Montfort Pune
                this.UpdateDefaultMontfortESPReportApprovedSign();
            }
        }
        #endregion

        #region Events
        private void frmMain_Load(object sender, EventArgs e)
        {
            // leftMenuBar.AllowSelectedLink = true;   //to get selected item and its mian group 
            // tmrAlert.Interval = 550;
            // tmrAlert.Start();

            //On 27/02/2024, To Hide TDS Module Permanently --------------------------------------------------------
            SettingProperty.EnableTDS = false;
            SettingProperty.EnableStock = false;
            bbiTDSReports.Visibility = BarItemVisibility.Never;
            //------------------------------------------------------------------------------------------------------

            //On 28/06/2019, while backgroup process running, enable, after locking backup.restore------
            BackupNew.Enabled = navRestoreData.Enabled = nbiDBBackup.Enabled = nbiDBRestore.Enabled = false;
            //-------------------------------------------------------------------------------------------

            //On 25/08/2021, To show Audit Log only for Admin or Auditor User--------
            AssignAuditorUserDetails();
            //----------------------------------------------------------------------

            //On 07/06/2017, To Invoke Background work Asynchronous to communicate Acmeerp poral --------------
            bbiLatestVersion.Visibility = BarItemVisibility.Never;
            bbiShowLatestVersionFeature.Visibility = BarItemVisibility.Never;
            bworkerWithPoral.WorkerReportsProgress = true;
            bworkerWithPoral.WorkerSupportsCancellation = true;
            bworkerWithPoral.WorkerReportsProgress = true;

            //For automatic upload database, current database is getting uploaded during the development
            //so lock upload database logic if system is local  development systmn
            isDevelopmentSystem = IsLocalDevelopmentIP();
            if (bworkerWithPoral.IsBusy != true)
            {
                bworkerWithPoral.RunWorkerAsync();
            }
            //---------------------------------------------------------------------------------------------------

            nbgTransaction.Expanded = true;
            nbiImportMasters.Visible = nbiExportVouchers.Visible = nbiMapLedgers.Visible = nbiSubBranchList.Visible =
                nbiUploadVouchers.Visible = nbiExportMasters.Visible = nbiUploadVouchers.Visible = nbiPortalUpdates.Visible =
                nbiInKind.Visible = nbiInKindArticle.Visible = nbiInKind.Enabled = nbiInKindArticle.Enabled = nbiAddress.Visible = false;
            EnableMenu();
            ValidateLicensewithCurrentdate();
            LoadHomeAccountingPeriod();
            // LoadLoginHome();
            SetTransPeriod();
            ActiveNavGroupId = (int)Moudule.Finance;

            // Chinna 07.12.2021
            if (SettingProperty.EnablePayrollOnly)
            {
                ActiveNavGroupId = (int)Moudule.Payroll;
            }

            nbiUploadBranchOfficeDB.Visible = false;
            //On 09/05/2020, to autobackuptoportal based on settings
            //if ((this.AppSetting.EnablePortal == 1) &&
            //    (this.AppSetting.IS_SDB_CONGREGATION || this.AppSetting.IS_SCCGSB_CONGREGATION || this.AppSetting.IS_BOSCOS_DEMO))
            if ((this.AppSetting.EnablePortal == 1) &&
                (AppSetting.AutomaticDBBackupToPortal == 1))
            {
                // nbiUploadBranchOfficeDB.Caption = "Upload Database to Portal"; 05/10/2023, Caption
                nbiUploadBranchOfficeDB.Visible = true;
            }
            nbiMapLedgerOption.Visible = true; //Chinna has to change
            nbiFDReInvestment.Visible = (this.UIAppSetting.EnableFlexiFD == "1"); //28/11/2018, to lock reinvestment feature based on setting

            //if (this.LoginUser.LoginUserId != AppSetting.DefaultAdminUserId.ToString())
            if (!LoginUser.IsFullRightsReservedUser)
            {
                ApplyUserRights();
            }

            nbiAuditLog.Visible = (this.LoginUser.IsFullRightsReservedUser); //Show Audit Log for Admin and Audit user alone

            //if (IsValidUser())
            //{

            //On 26/08/2019, don't show project selection window after login--------------------
            //ShowProjectSelectionWindow();
            //-----------------------------------------------------------------------------------
            //}
            //leftMenuBar.View = new CustomNavPaneViewInfoRegistrator();
            //On 24/10/2019, to show project selection and ac year creation at first time when new db
            if (string.IsNullOrEmpty(TransactionPeriod))
            {
                ShowProjectSelectionWindow();
            }

            //On 25/03/2021, For SDBINM Auditors suggested to skip below mentioned Ledgers for Voucher Entries
            if (AppSetting.IS_SDB_INM)
            {
                using (LedgerSystem ledger = new LedgerSystem())
                {
                    AppSetting.SDBINM_SkippedLedgerIds = ledger.GetSDBINMAuditorSkippedLedgerIds();
                }
            }

            //On 07/06/2021 ******************************************************************************************************
            //Check Licenses Locations with DB locations in settings, if mismatching just alert message
            if (IsLicenseKeyMismatchedByLicenseKeyDBLocation())
            {
                string msg = "Branch Location '" + this.AppSetting.BranchLocations + "' is mismatching with License Key locations." +
                              System.Environment.NewLine + "Check in Utilities -> Configuration -> Institute Info -> Branch Locations." +
                              System.Environment.NewLine + System.Environment.NewLine + "Note : Update License Key or Correct Branch Location in Setting.";
                this.ShowMessageBoxWarning(msg);
            }

            //********************************************************************************************************************

            nbiMergeCashBankLedger.Visible = true;
            nbiUpdateNarration.Visible = true;
            this.nbiInsuranceTypes.Visible = false;

            SetRightsPayrollOnly();

            //Enforce receipt module rights
            EnforceReceiptModuleRightsInMainMenus();

            //14/06/2022
            nbiFDPostCharge.Visible = false;

            //On 07/02/2024, Lock Utilities features for previous year data ------------------------------------------------------------
            if (this.AppSetting.IsSplitPreviousYearAcmeerpDB)
            {
                bbiDataUtlity.Visibility = BarItemVisibility.Never;
            }
            //--------------------------------------------------------------------------------------------------------------------------

            //10/07/2024, If other than india country 
            if (this.AppSetting.IsCountryOtherThanIndia)
            {
                nbiGSTPolicy.Visible = false;
                nbiVendor.Visible = false;
            }

            //On 03/09/2024, To lock the following featues for multi currency enabled version
            nbiCashAccounts.Visible = false;
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                //nbiBudget.Visible = nbiBudgetAnnual.Visible = 
                //nbgFixedDeposit.Visible = 
                nbiThirdPartyIntegration.Visible = false;
                nbiMapMigration.Visible = nbiMergeCashBankLedger.Visible = nbiDeleteunusedLedger.Visible = nbiMoveDeleteVouchers.Visible = false;
                nbiGSTPolicy.Visible = false;
                nbiImportSplitProject.Visible = nbiSplitLedger.Visible = nbiExportToTally.Visible = nbiProjectBulkExportData.Visible
                    = nbiProjectBulkImportData.Visible = nbiDataMigration.Visible = false;
                nbiVendor.Visible = true;
                nbiCashAccounts.Visible = true;
            }
            if (this.AppSetting.IS_SDB_RMG)
                nbiThirdPartyIntegration.Visible = true;
            //On 17/09/2024, To lock datamanagement features for few license keys -----------------------------------------------------------
            if (this.AppSetting.IsLockedDataManagementFeatures)
            {
                nbgDataUtility.Visible = false;
            }

            //nbiCashAccounts.Visible = false;
            //-------------------------------------------------------------------------------------------------------------------------------

            if (this.AppSetting.IS_SDB_INM)
            {
                nbiManufacture.Visible = nbiAssetReceiveVoucher.Visible = nbiAssetReceiveVoucher.Visible = nbiDepreciation.Visible = false;
            }
            else
            {
                nbiManufacture.Visible = nbiAssetReceiveVoucher.Visible = nbiAssetReceiveVoucher.Visible = true;
            }
        }

        private DevExpress.XtraEditors.TileItemFrame GetAlertEmptyTileItem(string caption, string message, string Type, Int32 id)
        {
            DevExpress.XtraEditors.TileItemFrame tileFrame = new DevExpress.XtraEditors.TileItemFrame();
            DevExpress.XtraEditors.TileItemElement tileMessage = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileCaption = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileType = new DevExpress.XtraEditors.TileItemElement();

            tileFrame.Tag = id;// jsut to indendity (1- Receipt Module Rights, 2- Province Voucher Lock
            tileCaption.Text = caption;
            tileMessage.Text = message;
            tileType.Text = Type;

            // Add TileItem to the Frame
            tileFrame.Elements.Add(tileCaption);
            tileFrame.Elements.Add(tileMessage);
            tileFrame.Elements.Add(tileType);
            //Set Properties for Tile Frame
            tileFrame.Animation = DevExpress.XtraEditors.TileItemContentAnimationType.ScrollTop;
            tileFrame.Interval = 5000;
            tileType.Appearance.Normal.Options.UseFont = true;

            tileCaption.Appearance.Normal.ForeColor = tileType.Appearance.Normal.ForeColor = Color.Thistle;
            tileCaption.Appearance.Normal.Options.UseFont = true;

            tileMessage.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopCenter;
            tileMessage.UseTextInTransition = true;
            tileMessage.Appearance.Normal.ForeColor = Color.DarkBlue;
            tileMessage.Appearance.Normal.Font = new Font(tileMessage.Appearance.Normal.Font.FontFamily, 8, FontStyle.Bold);
            tileMessage.Appearance.Normal.Options.UseFont = true;
            if (id == 2) //Change Color for voucher lock 
            {
                tileMessage.Appearance.Normal.ForeColor = Color.DarkRed;
            }
            tileCtlReceiptModueStatus.Size = new Size(tileCtlReceiptModueStatus.Width, tileCtlReceiptModueStatus.Height);

            return tileFrame;
        }

        public void LoadHomeAccountingPeriod()
        {
            try
            {
                using (AccouingPeriodSystem accPeriodSystem = new AccouingPeriodSystem())
                {
                    resultArgs = accPeriodSystem.FetchAccountingPeriodDetailsForSettings();
                    glkTransPeriod.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtTemp = resultArgs.DataSource.Table.Clone();
                        dtTemp.Columns.Add("TRANSPERIOD", typeof(string));
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            dtTemp.Rows.Add(dr[accPeriodSystem.AppSchema.AccountingPeriod.ACC_YEAR_IDColumn.ColumnName],
                                dr[accPeriodSystem.AppSchema.AccountingPeriod.YEAR_FROMColumn.ColumnName],
                                dr[accPeriodSystem.AppSchema.AccountingPeriod.YEAR_TOColumn.ColumnName],
                                dr[accPeriodSystem.AppSchema.AccountingPeriod.BOOKS_BEGINNING_FROMColumn.ColumnName],
                                dr["STATUS"],
                              UtilityMember.DateSet.ToDate(dr[accPeriodSystem.AppSchema.AccountingPeriod.YEAR_FROMColumn.ColumnName].ToString(), "dd-MMM-yyyy") + "   to   " +
                              UtilityMember.DateSet.ToDate(dr[accPeriodSystem.AppSchema.AccountingPeriod.YEAR_TOColumn.ColumnName].ToString(), "dd-MMM-yyyy"));
                        }
                        UtilityMember.ComboSet.BindGridLookUpCombo(glkTransPeriod, dtTemp, "TRANSPERIOD", accPeriodSystem.AppSchema.AccountingPeriod.ACC_YEAR_IDColumn.ColumnName);
                        glkTransPeriod.EditValue = this.UtilityMember.NumberSet.ToInteger(this.AppSetting.AccPeriodId) != 0 ?
                            this.UtilityMember.NumberSet.ToInteger(this.AppSetting.AccPeriodId) : 0;
                        glkTransPeriod.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void bbiHome_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadLoginHome();
        }

        private void bbiDashBoard_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadLoginHome();
        }

        private void bbiFinance_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if (UserId != AppSetting.DefaultAdminUserId.ToString())
            if (!LoginUser.IsFullRightsReservedUser)
            {
                MenuCaption = nbgTransaction.Caption;
                ActiveNavGroupId = bbiFinance.Id;
                ApplyUserRights();
                nbgMaster.Visible = false;
                ShowLeftMenuBar(true);
                dpAcme.Text = FinanceTitle;
            }
            else
            {
                CollapseNavBar(nbgTransaction.Caption, bbiFinance.Id);
            }

            // This to Load from another module to asset module
            LoadLoginHome();
        }


        private void bbiNetworking_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!LoginUser.IsFullRightsReservedUser)
            {
                MenuCaption = nbgDonorMasters.Caption;
                ActiveNavGroupId = bbiNetworking.Id;
                ApplyUserRights();
                ShowLeftMenuBar(true);
                dpAcme.Text = NetworkTitle;
            }
            else
            {
                CollapseNavBar(nbgDonorMasters.Caption, bbiNetworking.Id);
            }
        }

        private void bbiStock_ItemClick(object sender, ItemClickEventArgs e)
        {
            CollapseNavBar(nbgInventory.Caption, bbiStock.Id);
        }

        private void bbiAsset_ItemClick(object sender, ItemClickEventArgs e)
        {
            //using (AssetLedgerMapping assetLedgerMapping = new AssetLedgerMapping())
            //{
            //    if (assetLedgerMapping.AccountLedgerId == 0 || assetLedgerMapping.DepLedgerId == 0 || assetLedgerMapping.DisposalLedgerId == 0)
            //    {
            //        frmAssetLedgerMapping frmAsset = new frmAssetLedgerMapping();
            //        frmAsset.ShowDialog();
            //    }
            //    //LoadAssetDashBoard();
            //}

            // CollapseNavBar(nbgInventory.Caption, bbiAsset.Id);
            if (!LoginUser.IsFullRightsReservedUser)
            {
                ActiveNavGroupId = bbiAsset.Id;
                ApplyUserRights();
                ShowLeftMenuBar(true);
                dpAcme.Text = "Fixed Asset";
            }
            else
            {
                CollapseNavBar(nbgInventory.Caption, bbiAsset.Id);
                ShowLeftMenuBar(true);
                dpAcme.Text = "Fixed Asset";
            }

            // Temp
            // 20/03/2024 to stop the load the fixed Asset Details error due to invalid database changes
            //LoadFixedAssetRegister();

            ShowLeftMenuBar(true);

            //  LoadAssetDashBoard();

            ////if (UserId != AppSetting.DefaultAdminUserId.ToString())
            //if (!LoginUser.IsFullRightsReservedUser)
            //{
            //    MenuCaption = nbgTransaction.Caption;
            //    ActiveNavGroupId = bbiFinance.Id;
            //    ApplyUserRights();
            //    nbgMaster.Visible = false;
            //    ShowLeftMenuBar(true);
            //    dpAcme.Text = FinanceTitle;
            //}



            //Enabled due to user Rights

        }

        private void nbgStock_ItemClick(object sender, ItemClickEventArgs e)
        {
            //using (StockLedgerMapping stockLedgerMapping = new StockLedgerMapping())
            //{
            //    if (stockLedgerMapping.AccountLedgerId == 0 || stockLedgerMapping.DisposalLedgerId == 0)
            //    {
            //        frmStockLedgerMapping frmstkMap = new frmStockLedgerMapping();
            //        frmstkMap.ShowDialog();
            //    }
            //}
            CollapseNavBar(nbgStockMasters.Caption, nbgStock.Id);
            LoadStockDashboard();
        }

        private void LoadStockDashboard()
        {
            bool hasHome = HasFormInMDI(typeof(frmStockHomeView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmStockHomeView frmregister = new frmStockHomeView();
                frmregister.MdiParent = this;
                frmregister.Show();
            }
            CloseWaitDialog();
        }

        private void UpdatePayrollSymbols()
        {
            using (DashBoardSystem dashboard = new DashBoardSystem())
            {
                resultArgs = dashboard.UpdatePayrollSymbols();
            }
        }

        /// <summary>
        /// On 11/12/2024 to remove default componetns like
        /// 'DA', 'HRA', 'EPF', 'GROSS WAGES', 'DEDUCTIONS', 'NETPAY', 'LOPDays'
        /// </summary>
        private void RemoveDefaultComponentsForMultiCurrency()
        {
            PayrollSubMenu.RemoveDefaultComponentsForMultiCurrency();
        }

        private void bbiPayroll_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!LoginUser.IsFullRightsReservedUser)
            {
                UpdatePayrollSymbols();
                DefinePayroll();
                ActiveNavGroupId = bbiPayroll.Id;
                ApplyUserRights();
                ShowLeftMenuBar(true);
                dpAcme.Text = "Payroll";
            }
            else
            {
                CollapseNavBar(nbgDefinition.Caption, bbiPayroll.Id);
            }
            PayrollSubMenu.RemoveDefaultComponentsForMultiCurrency();
        }

        private void DefinePayroll()
        {
            nbg1Reports.Visible = false;
            //dpAcme.Text = "Payroll";
            dpAcme.Text = this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE);
            // CloseAllFormInMDI();
            nbgPayroll.Visible = false;
            nbgFixedDeposit.Visible = nbgDataUtility.Visible = nbgInventory.Visible = nbgMaster.Visible = nbgReports.Visible = nbgStockMasters.Visible = nbgStockVouchers.Visible = nbgAssetMasters.Visible = nbgAssetTransactions.Visible = nbgAssetOptions.Visible = nbgAssetViews.Visible =
                nbiHeadOfficeInterface.Visible =
            nbiVoucherMasters.Visible = nbiMaster.Visible = nbiViews.Visible = nbgAudit.Visible = nbgTDSMasters.Visible = nbgTDSVouchers.Visible = nbgTransaction.Visible = nbgUserManagement.Visible = nbgVehicle.Visible =
            nbiHeadOfficeInterface.Visible = false;
            CloseForm(typeof(PAYROLL.Modules.Payroll_app.frmPayrollview).Name);
            RemoveDefaultComponentsForMultiCurrency();
            if (!PayrollSubMenu.IsPayrollExists())
            {
                //DialogResult dr = XtraMessageBox.Show("Payroll Does not Exists.Do you want to create?", "Payroll", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                DialogResult dr = XtraMessageBox.Show(this.GetMessage(MessageCatalog.Payroll.ACCPMain.PAYROLL_CREATE_INFO), this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    PAYROLL.Modules.Payroll_app.frmCreatePayroll createpayroll = new PAYROLL.Modules.Payroll_app.frmCreatePayroll();
                    createpayroll.ShowDialog();
                    PAYROLL.Modules.Payroll_app.frmPayrollview PayView = new PAYROLL.Modules.Payroll_app.frmPayrollview();
                    PayView.MdiParent = this;
                    PayView.Show();
                }
            }
            else
            {
                PAYROLL.Modules.Payroll_app.frmPayrollview PayView = new PAYROLL.Modules.Payroll_app.frmPayrollview();
                PayView.MdiParent = this;
                //lblCaption.Text = PayView.Text;
                PayView.Show();
            }
            PayrollSubMenu.SetRecentPayRoll();
            PayrollSubMenu.SetValues();
            // label to frmmain text
            //this.Text = (!string.IsNullOrEmpty(PayrollSubMenu.PAYROLL_MONTH)) ? "Payroll for " + PayrollSubMenu.PAYROLL_MONTH : "No Payroll exists";
            this.Text = (!string.IsNullOrEmpty(PayrollSubMenu.PAYROLL_MONTH)) ? this.GetMessage(MessageCatalog.Payroll.ACCPMain.PAYROLL_MONTHFOR_INFO) + " " + PayrollSubMenu.PAYROLL_MONTH : this.GetMessage(MessageCatalog.Payroll.ACCPMain.PAYROLL_NOPAYROLL_EXISTS_INFO);

            //On 15/05/2019, Attach Current PayRoll name
            if (PayrollSubMenu.PAYROLL_Id > 0)
            {
                dpAcme.Text += "-" + PayrollSubMenu.PAYROLL_MONTH;
            }
            glkTransPeriod.Visible = false;
            ActiveNavGroupId = bbiPayroll.Id;
            // LoadPayroll();
            SetPayRollMenu();
            //ShowLeftMenuBar(true);
            glkTransPeriod.Visible = false;
        }

        private void bbiStatutory_ItemClick(object sender, ItemClickEventArgs e)
        {
            CollapseNavBar(nbgVehicle.Caption, 0);
        }

        //private void bbiUsers_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    LoadUsers();
        //    ShowLeftMenuBar(true);
        //}

        private void bbiLogout_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadLogoutFunc();
        }

        //private void bbiTransaction_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    
        //}

        //private void bbiInventory_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    
        //}

        //private void bbiPayroll_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    
        //}

        //private void bbiMaster_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    
        //}

        //private void bbiUserManagement_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //   
        //}

        //private void bbiReports_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //   
        //}

        //private void bbiVehicle_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    CollapseNavBar(nbgVehicle.Caption);
        //}

        void report_ReportDrillDown(object sender, Bosco.Report.Base.EventDrillDownArgs e)
        {
            throw new NotImplementedException();
        }

        //private void bbiClose_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    AppLogout();
        //}

        private void nbiHome_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            LoadLoginHome();
        }

        private void nbiClose_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            CloseAll(true);
        }

        private void nbiLocalization_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            frmSettings frmsetting = new frmSettings();
            frmsetting.ShowDialog();
        }

        private void nbiProjectCatogory_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadProjectCategory();
        }

        private void nbiSubBranchList_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadSubBranchList();
        }
        private void nbiBudget_LinkClicked_1(object sender, NavBarLinkEventArgs e)
        {
            LoadBudget();
        }

        private void nbiAuditor_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            LoadAuditor();
        }

        //            frmTransactionMultiAdd frmTransactionMultiEntry = new frmTransactionMultiAdd();
        //    frmTransactionMultiEntry.ShowDialog();

        private void nbiDonor_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            LoadDonor();
        }

        private void nbiProject_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            LoadProject();
        }

        private Rectangle GetLinkRect(NavBarItemLink link)
        {
            NavBarControl navBar = link.NavBar;
            PropertyInfo pi = navBar.GetType().GetProperty("ViewInfo", BindingFlags.Instance | BindingFlags.NonPublic);
            NavBarViewInfo viewInfo = pi.GetValue(navBar, null) as NavBarViewInfo;
            NavLinkInfoArgs linkInfo = viewInfo.GetLinkInfo(link);
            return linkInfo.CaptionRectangle;
        }

        private void nbiCountry_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            LoadCountry();
        }

        private void nbiBank_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            LoadBank();
        }

        private void nbiLedgerGroup_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            LoadLedgerGroup();
        }

        private void nbiJournal_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            ShowTransactionViewForm((int)DefaultVoucherTypes.Journal - 1, DefaultVoucherTypes.Journal);
        }

        private void nbiAuditorAddressBook_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

        }

        private void leftMenuBar_GroupExpanded(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            //string Press = leftMenuBar.PressedGroup.Caption;
            //CollapseNavBar(leftMenuBar.PressedGroup.Caption);
        }

        private void xtMdiManager_PageRemoved(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
        {
            XtraTabbedMdiManager manager = sender as XtraTabbedMdiManager;
            if (manager.Pages.Count == 1)
            {
                if (manager.SelectedPage.MdiChild.Name == typeof(frmLoginDashboard).Name)
                {
                    bbiCloseAll.Enabled = false;
                    SetVisibleCloseAllTabs = false;
                }
            }
            else if (manager.Pages.Count == 0)
            {
                bbiCloseAll.Enabled = false;
                SetVisibleCloseAllTabs = false;
            }
        }

        private void nbiCostCentre_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            LoadCostCentre();
        }

        private void nbiInKindArticle_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            LoadInkindArticle();
        }

        private void nbiExecutiveMember_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            LoadGoverningMember();
        }

        private void nbiLedger_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //bool hasHome = HasFormInMDI(typeof(frmLedgerView).Name);
            //ShowWaitDialog();
            //if (!hasHome)
            //{
            //    frmLedgerView frmLedger = new frmLedgerView(LedgerTypes.GN);
            //    frmLedger.MdiParent = this;
            //    frmLedger.Show();
            //}
            //CloseWaitDialog();

            LoadLedger();
        }

        private void nbiMapLedProject_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //ShowWaitDialog();
            if (!string.IsNullOrEmpty(TransactionPeriod))
            {
                //if (!string.IsNullOrEmpty(DefaultProject))
                //{
                frmMapProjectLedger frmLedgerPro = new frmMapProjectLedger();
                frmLedgerPro.ShowDialog();
                //}
                //else
                //{
                //    if (XtraMessageBox.Show("Project is not yet created. Do you want to create Project now?","AcMeERP", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                //    {
                //        frmProjectAdd frmProject = new frmProjectAdd((int)AddNewRow.NewRow);
                //        frmProject.ShowDialog();
                //    }
                //}
            }
            else
            {
                if (CommonMethod.ApplyUserRights((int)TransactionPeriods.CreateTransaction) != 0)
                {
                    //if (XtraMessageBox.Show("Transaction Period is not yet created  or not active. Do you want to create the Transaction Period now or set active?", "AcMeERP", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    if (XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.AccountingPeriod.ACCOUNTING_PERIOD_NOT_CREATED), this.GetMessage(MessageCatalog.Common.COMMON_ACMEERP_TITLE), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        LoadAccountPeriod();
                    }
                }
                //else
                //{
                //    Application.Restart();
                //}
            }
            //  CloseWaitDialog();
        }

        private void nbiMapProjectLedger_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            LoadMapAccounts();
        }

        private void LoadMapAccounts()
        {
            bool hasHome = HasFormInMDI(typeof(frmMapProjectLedger).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmMapProjectLedger frmproLedger = new frmMapProjectLedger();
                frmproLedger.MdiParent = this;
                frmproLedger.Show();
            }
            CloseWaitDialog();
        }

        private void nbiVoucher_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            LoadVoucher();
        }



        private void nbiPurpose_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            LoadPurpose();
        }

        private void nbiAudit_Info_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            LoadAuditInfo();
        }

        private void xtMdiManager_SelectedPageChanged(object sender, EventArgs e)
        {
            if (xtMdiManager != null)
            {
                if (xtMdiManager.SelectedPage != null)
                {
                    dtBaseDate = null;
                    dtBaseProject = null;
                    captionPanel.Text = xtMdiManager.SelectedPage.Text;

                    //by alwar on 12/12/2015, to trach selected mdi child forms main and sub menus
                    //ActivateLeftMenuProperty();
                    //xtMdiManager.SelectedPage.MdiChild.Refresh();
                    //xtMdiManager.SelectedPage.MdiChild.Parent.Refresh();
                }
                else
                {
                    captionPanel.Text = string.Empty;
                }
            }
        }


        private void nbiReceipt_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowTransactionViewForm((int)DefaultVoucherTypes.Receipt - 1, DefaultVoucherTypes.Receipt);
        }

        private void nbiUser_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            LoadUserView();
        }

        private void nbiUserRole_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            LoadRole();
        }

        private void nbiUserRights_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            LoadRights();
        }

        private void nbiManageSecurity_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            LoadUserSecurity();
        }

        private void nbiPayment_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            ShowTransactionViewForm((int)DefaultVoucherTypes.Payment - 1, DefaultVoucherTypes.Payment);
        }

        private void nbiContra_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            ShowTransactionViewForm((int)DefaultVoucherTypes.Contra - 1, DefaultVoucherTypes.Contra);
        }

        private void nbiBankAccount_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadBankAccount();
        }

        private void nbiFixedDeposit_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadFDAccount();
        }

        private void nbiStatutory_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        //private void nviAccountingPeriod_LinkClicked(object sender, NavBarLinkEventArgs e)
        //{
        //    LoadAccountPeriod();
        //}

        private void bsiUserProfile_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if (IsProfileClicked)
            //{
            //    var t = Task.Factory.StartNew(() => Test());
            //    IsProfileClicked = false;
            //}
        }

        //private void Test()
        //{
        //    frmUserPhoto profile = new frmUserPhoto(ribbonMain);
        //    profile.ShowDialog();
        //}

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            // CloseAll(true);
            //Application.Restart();
            LoadLogoutFunc();

        }

        private void popupContainerControl1_Paint(object sender, PaintEventArgs e)
        {
            // LoadUserProfile();
        }

        private void visiblePageHeaderLinks(BarItemVisibility visibility)
        {
            //bsiTransactionPeriod.Visibility = visibility;

            //bsiPeriod.Visibility = visibility;
            //bsiSpace1.Visibility = visibility;
            // bsiLoggedUser.Visibility = visibility;
        }



        private void nviBankReconciliation_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadBRS();
            //CloseWaitDialog();
        }

        private void nbiFDRenewal_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //bool hasHome = HasFormInMDI(typeof(frmFDRenewal).Name);
            //CloseFormInMDI(typeof(frmFDRenewal).Name);
            //ShowWaitDialog();
            //frmFDRenewal frmFDRenew = new frmFDRenewal();
            //frmFDRenew.MdiParent = this;
            //frmFDRenew.Show();
            //CloseWaitDialog();
            LoadFdRenewal();

        }

        private void nbiAddress_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            loadAddress();
        }

        private void leftMenuBar_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void nbiFDRegisters_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //bool hasHome = HasFormInMDI(typeof(frmBudgetView).Name);

            //ShowWaitDialog();

            //if (!hasHome) 
            //{
            //    frmBudgetView frmBudget = new frmBudgetView();
            //    frmBudget.MdiParent = this;
            //    frmBudget.Show();
            //}

            //CloseWaitDialog();
            LoadFdRegistersView();
        }

        private void nbiFDLedger_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadFDLedgers();
        }

        private void nbiFDInvestment_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadFDInvestment();
        }

        private void bbiDataUtlity_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!LoginUser.IsFullRightsReservedUser)
            {
                MenuCaption = DataUtility;
                ActiveNavGroupId = bbiDataUtlity.Id;
                ApplyUserRights();
                ShowLeftMenuBar(true);
                dpAcme.Text = DataUtility;
            }
            else
            {
                CollapseNavBar(nbgDataUtility.Caption, bbiDataUtlity.Id);
            }
            //Import Asset Starts ( it can be later
            if (SettingProperty.EnableAsset == true)
            {
                // nbiImportAssetMasters.Visible = true;
            }
        }

        private void nbiDBBackup_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                DateTime Time = DateTime.Now;
                int Year = Time.Year;
                int Month = Time.Month;
                int Day = Time.Day;
                int Hour = Time.Hour;
                int Minites = Time.Minute;
                int Seconds = Time.Second;
                int MilliSecond = Time.Millisecond;
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.FileName = "AcpERP" + "-" + Year + Month + Day + Hour + Minites + Seconds + MilliSecond + ".sql";
                saveDialog.DefaultExt = "sql";
                saveDialog.Filter = "Sql files (*.Sql)|*.Sql";
                saveDialog.Title = "Backup";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    //if (XtraMessageBox.Show("Do you want to backup AcMeERP data?", "AcMeERP", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    if (XtraMessageBox.Show(this.GetMessage(MessageCatalog.Common.COMMON_ACMEERP_BACKUP_DATA), this.GetMessage(MessageCatalog.Common.COMMON_ACMEERP_TITLE), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        BackupDirectoryPath = saveDialog.FileName;
                        BackupDirectoryPath = Path.ChangeExtension(BackupDirectoryPath, ".sql");
                        this.ShowWaitDialog();
                        //On 26/11/2024 - To keep number of voucher
                        Int32 noofactivevouchers = this.GetNoActiveVouchers();
                        BackRestore.No_of_Active_Vouchers = noofactivevouchers;
                        ResultArgs result = BackRestore.MySqlBackup(BackupDirectoryPath, "");
                        if (result.Success)
                        {
                            this.CloseWaitDialog();
                            //XtraMessageBox.Show("The Backup was finished successfully" + Environment.NewLine + "The File " + BackupDirectoryPath, "Backup finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            XtraMessageBox.Show(this.GetMessage(MessageCatalog.Common.COMMON_ACMEERP_BACKUP_DATA) + Environment.NewLine + (this.GetMessage(MessageCatalog.Common.COMMON_FILE_BACKUP_INFO)) + BackupDirectoryPath, (this.GetMessage(MessageCatalog.Common.COMMON_FINISHED_BACKUP_DATA)), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            this.CloseWaitDialog();
                            //XtraMessageBox.Show(result.Message + Environment.NewLine + "The File " + BackupDirectoryPath, "Backup finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            XtraMessageBox.Show(result.Message + Environment.NewLine + (this.GetMessage(MessageCatalog.Common.COMMON_FILE_BACKUP_INFO)) + BackupDirectoryPath, (this.GetMessage(MessageCatalog.Common.COMMON_FINISHED_BACKUP_DATA)), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                XtraMessageBox.Show(ee.ToString());
            }
        }

        private void nbiDBRestore_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                OpenFileDialog Openfile = new OpenFileDialog();
                Openfile.Title = "Restore";
                if (Openfile.ShowDialog() == DialogResult.OK)
                {
                    //if (XtraMessageBox.Show("Do you want to Restore AcMeERP data?", "Restored", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    if (XtraMessageBox.Show(this.GetMessage(MessageCatalog.Common.COMMON_RESTORE_BACKUP_DATA), (this.GetMessage(MessageCatalog.Common.COMMON_RESTORE_INFO)), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        RestorePath = Openfile.FileName;
                        //this.ShowWaitDialog("  Restoring..... ");
                        this.ShowWaitDialog(this.GetMessage(MessageCatalog.Common.COMMON_RESTORING_INFO));
                        ResultArgs result = BackRestore.RestoreDB(RestorePath, "ACPERP", "");
                        this.CloseWaitDialog();
                        if (result.Success)
                        {
                            //MessageBox.Show("Restored successfully" + Environment.NewLine + "The File " + RestorePath, "Restored finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            XtraMessageBox.Show(this.GetMessage(MessageCatalog.Common.COMMON_RESTORE_SUCCESS_INFO) + Environment.NewLine + (this.GetMessage(MessageCatalog.Common.COMMON_FILE_BACKUP_INFO)) + RestorePath, (this.GetMessage(MessageCatalog.Common.COMMON_RESTORE_FINISHED_INFO)), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadLogoutFunc();
                        }
                        else
                        {
                            //MessageBox.Show(result.Message + Environment.NewLine + "The File " + RestorePath, "Restored finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MessageBox.Show(result.Message + Environment.NewLine + (this.GetMessage(MessageCatalog.Common.COMMON_FILE_BACKUP_INFO)) + RestorePath, (this.GetMessage(MessageCatalog.Common.COMMON_RESTORE_FINISHED_INFO)), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                XtraMessageBox.Show(ee.ToString());
            }
        }

        private void bbiConfiguration_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!LoginUser.IsFullRightsReservedUser)
            {
                if (CommonMethod.ApplyUserRights((int)Menus.MasterSetting) != 0)
                {
                    frmSettings frmsetting = new frmSettings();
                    frmsetting.ShowDialog();
                }
            }
            else
            {
                frmSettings frmsetting = new frmSettings();
                frmsetting.ShowDialog();
            }
        }

        //private void bbiProject_ItemClick_1(object sender, ItemClickEventArgs e)
        //{
        //    frmProjectSelection frmprojectSelection = new frmProjectSelection(Bosco.Utility.ProjectSelection.EnableVoucherSelectionMethod);
        //    frmprojectSelection.ShowDialog();
        //}

        private void pceLoggedUser_Click(object sender, EventArgs e)
        {
            flyoutPanel1.ShowPopup();
            LoadUserProfile();
        }

        private void nbiFinanceFDRenewal_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadFdRenewal();
        }

        private void nbiFianceFDRegister_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadFdRegistersView();
        }

        private void nbiMasterFD_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //LoadFixedDeposit();
        }

        private void nbiLegalEntity_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(frmLegalEntityView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmLegalEntityView frmLegalEntityView = new frmLegalEntityView();
                frmLegalEntityView.MdiParent = this;
                frmLegalEntityView.Show();
            }
            CloseWaitDialog();
        }

        private void bbiProject_ItemClick(object sender, ItemClickEventArgs e)
        {
            //frmProjectSelection frmprojectSelection = new frmProjectSelection(Bosco.Utility.ProjectSelection.EnableVoucherSelectionMethod);
            //frmprojectSelection.ShowDialog();
            if (bbiProject.Enabled)
                ShowProjectSelectionWindow();
        }

        private void nbiDataMigration_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadDataMigration();
        }

        private void LoadDataMigration()
        {
            if (AppSetting.ENABLE_TRACK_RECEIPT_MODULE)
            {
                bool hasHome = HasFormInMDI(typeof(frmMigrationSelection).Name);
                CloseFormInMDI(typeof(frmMigrationSelection).Name);
                frmMigrationSelection DataMigration = new frmMigrationSelection();
                DataMigration.ShowDialog();
                if (DataMigration.MigrationMode == MigrationType.AcMePlus)
                {
                    using (DataMigrationConnectionSetUp AcMePlus = new DataMigrationConnectionSetUp())
                    {
                        AcMePlus.ShowDialog();
                    }
                }
                else if (DataMigration.MigrationMode == MigrationType.Tally)
                {
                    using (frmTallyMigration Tally = new frmTallyMigration())
                    {
                        Tally.ShowDialog();
                    }
                }
                else if (DataMigration.MigrationMode == MigrationType.BOSCOPAC)
                {
                    using (frmMigrationBOSCOPAC BoscoPAC = new frmMigrationBOSCOPAC())
                    {
                        BoscoPAC.ShowDialog();
                    }
                }
            }
        }

        private void nbiFDWithdraw_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadFDWithdraw();
        }

        private void nbiLeageEntityf_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadLegalEntity();
        }

        private void nbiBalanceRefresh_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadRefreshBalance();
        }

        private void nbiRegenerateVoucher_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadRegenerateVoucher();
        }

        private static void LoadRegenerateVoucher()
        {
            frmRegenerateVoucher voucher = new frmRegenerateVoucher();
            voucher.ShowDialog();
        }

        private void nbiMapMigration_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(frmMergeAllLedgers).Name);
            CloseFormInMDI(typeof(frmMergeAllLedgers).Name);
            frmMergeAllLedgers MergeLedgers = new frmMergeAllLedgers();
            MergeLedgers.ShowDialog();
        }

        private void bbiTDS_ItemClick(object sender, ItemClickEventArgs e)
        {
            CollapseNavBar(nbgTDSMasters.Caption, bbiTDS.Id);
        }

        private void nbiNatureofPayments_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadTDSNatureofPyments();
        }

        private void nbiDeduteeType_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadTDSDeducteeTypes();
        }

        private void nbiTaxPolicy_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadTaxPolicy();
        }

        private void nbiDutyTax_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadDutyTax();
        }



        private void navbarUpdates_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                OpenFileDialog Openfile = new OpenFileDialog();
                //Openfile.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                if (Openfile.ShowDialog() == DialogResult.OK)
                {
                    RestorePath = Openfile.FileName;
                    this.ShowWaitDialog();
                    BackRestore.ExecuteQuery(RestorePath);
                    this.CloseWaitDialog();
                    //MessageBox.Show("query executed Sucessfully" + Environment.NewLine + "The File " + RestorePath, "finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show(this.GetMessage(MessageCatalog.Common.COMMON_QUERY_EXECUTE_SUCESS) + Environment.NewLine + this.GetMessage(MessageCatalog.Common.COMMON_FILE_BACKUP_INFO) + RestorePath, this.GetMessage(MessageCatalog.Common.COMMON_QUERY_FINISHED), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ee)
            {
                XtraMessageBox.Show(ee.ToString());
            }
        }

        private void hideContainerLeft_Click(object sender, EventArgs e)
        {

        }



        private void nbiDataMigrationTally_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(frmTallyMigration).Name);
            CloseFormInMDI(typeof(frmTallyMigration).Name);
            frmTallyMigration DataMigrationTally = new frmTallyMigration();
            DataMigrationTally.ShowDialog();
        }

        private void navRestoreData_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadRestoreDatabase();
            //frmDatabaseRestore frmRestore = new frmDatabaseRestore();
            //frmRestore.ShowDialog();
        }

        private static void LoadRestoreDatabase()
        {
            frmRestoreMultipleDB frmRestore = new frmRestoreMultipleDB();
            frmRestore.ShowDialog();
        }


        private void BackupNew_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadDBBackup();
        }

        private void LoadDBBackup(bool forcetotake = false)
        {
            //On 17/09/2024, Don't alert to take back for locked keys
            if (!this.AppSetting.IsLockedDataManagementFeatures)
            {
                frmBackup frmBack = new frmBackup(forcetotake);
                frmBack.ShowDialog();
            }
        }

        private void nbiMapLedgerOption_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadLedgerOptions();
        }

        private static void LoadLedgerOptions()
        {
            frmLedgerOptions ledgeroption = new frmLedgerOptions();
            ledgeroption.ShowDialog();
        }

        private void nbiTDSJournal_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadTDSBookingView();
        }

        private void LoadFinnanceSettings()
        {
            frmFinanceSetting finnanceSettings = new frmFinanceSetting();
            finnanceSettings.ShowDialog();

            //02/05/2017, Reassign project selectoion window
            ReAssignSetting();

            // In Visible the GST Polocy
            if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableGST).Equals((int)YesNo.Yes))
            {
                nbiGSTPolicy.Visible = true;
            }
            else
            {
                nbiGSTPolicy.Visible = false;
            }
        }

        private void nbiDeductTDS_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            frmTDSPartyTrans frmDeduct = new frmTDSPartyTrans();
            frmDeduct.ShowDialog();
        }

        private void nbiTDSPayments_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //frmPaymentsTDS frmTDSPayments = new frmPaymentsTDS();
            //frmTDSPayments.ShowDialog();
            LoadTDSPaymentView(TDSPayTypes.TDSPayment);
        }

        private void nbiPartyPayment_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadTDSPaymentView(TDSPayTypes.PartyPayment);
        }

        private void nviAccountPeriod_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadAccountPeriod();
        }

        private void nbiCostCentreCategory_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadCostCentreCategory();
        }

        private void nbiTDSLedger_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadTDSLedgers();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            string[] DB;
            string databaseName = string.Empty;

            if (Is_Branch_Active && !AppSetting.IsSplitPreviousYearAcmeerpDB)
            {
                //1. 18/01/2018 Show export voucher form if setting is enbaled ------------------------------------------------------------------------------------
                //If Export voucher is enabled in finance setting, if user does not do it, disable closing the application
                if ((e.CloseReason == CloseReason.UserClosing && this.AppSetting.ExportVouchersBeforeClose == "1") || this.AppSetting.IS_SDB_ING)
                {
                    DialogResult dialogVoucherExport = AlertToExportVoucher();
                    if (dialogVoucherExport == DialogResult.Cancel)
                    {
                        e.Cancel = true; //disable closing the application
                    }
                }
                //-------------------------------------------------------------------------------------------------------------------------------------------------

                //2. Take Backup
                if (!SettingProperty.Is_Application_Logout && !e.Cancel)
                {
                    //if (XtraMessageBox.Show("Do you want to Exit the Application?", "Acme.erp", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    if (XtraMessageBox.Show(this.GetMessage(MessageCatalog.Common.COMMON_EXIT_APPLICATION), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        //3. Alert to take backup on 12/09/2018
                        if (UtilityMember.NumberSet.ToInteger(AppSetting.DontAlertTakeBackupBeforeClose) == 0)
                            LoadDBBackup(true);

                        //ShowWaitDialog("Closing the Application");
                        ShowWaitDialog(this.GetMessage(MessageCatalog.Common.COMMON_CLOSE_APPLICATION));
                        if (string.IsNullOrEmpty(CommonMethod.MultiDataBaseName))
                        {
                            string mysqlDefaultConnection = ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString;
                            string[] ConnectionString = mysqlDefaultConnection.Split(';');
                            if (mysqlDefaultConnection.Contains("port") == true)
                            {
                                string[] DefaultAppString = mysqlDefaultConnection.Split(';');
                                string[] SName = DefaultAppString[0].ToString().Split('=');
                                string[] Port = DefaultAppString[1].ToString().Split('=');
                                string[] CheckDataBase = DefaultAppString[2].ToString().Split('=');
                                if (CheckDataBase[0].Contains("connection timeout"))
                                {
                                    DB = DefaultAppString[3].ToString().Split('=');
                                }
                                else
                                {
                                    DB = DefaultAppString[2].ToString().Split('=');
                                }
                                databaseName = DB[1].ToString();   //"acperp";// DB[1];
                            }
                        }
                        else
                        {
                            databaseName = CommonMethod.MultiDataBaseName;
                        }

                        //On 26/11/2024 - To keep number of voucher
                        Int32 noofactivevouchers = this.GetNoActiveVouchers();
                        BackRestore.No_of_Active_Vouchers = noofactivevouchers;
                        resultArgs = BackRestore.MySqlBackup("", databaseName, 1);
                        if (resultArgs.Success)
                        {
                            e.Cancel = false;
                        }
                        CloseWaitDialog();
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void nbiTDSSection_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadTDSSection();
        }

        private void nbiMapLedgers_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadHOMapLedgers();
        }

        private void LoadHOMapLedgers()
        {
            ResultArgs resultArgs = null;
            if (isHeadOfficeLedgersExists())
            {
                using (ExportVoucherSystem exportVoucher = new ExportVoucherSystem())
                {
                    resultArgs = exportVoucher.FetchMapLedger();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        frmUnMappedLedgers unMappedLedgers = new frmUnMappedLedgers(resultArgs.DataSource.Table, false);
                        unMappedLedgers.ShowDialog();
                    }
                }
            }
            else
            {
                //XtraMessageBox.Show("Head Office Ledgers are not available to export vouchers. Kindly download the Head Office masters and try again.", "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                XtraMessageBox.Show(this.GetMessage(MessageCatalog.Common.COMMON_HO_LEDGERS_NOTAVAILABLE_INFO), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void nbiExportMasters_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            frmExportMastersToSubBranch frmExportMasters = new frmExportMastersToSubBranch();
            frmExportMasters.ShowDialog();
        }

        private void nbiUploadVouchers_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadSyncmasters();
        }

        private void nbiExportVouchers_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //frmPortalUpdatesMode updatemode = new frmPortalUpdatesMode(PortalUpdates.UploadVouchers);
            //updatemode.ShowDialog();
            LoadHOExportVouchers();
            //frmExportBranchOfficeVouchers frmExportVouchers = new frmExportBranchOfficeVouchers();
            //frmExportVouchers.ShowDialog();
        }

        private void LoadHOExportVouchers()
        {
            frmPortalUpdates updatemode = new frmPortalUpdates(PortalUpdates.UploadVouchers);
            updatemode.ShowDialog();

            //On 03/02/2022, always refresh footer portal communications
            //On 26/06/2019---------------------------------------------------------------------------------------
            //if (updatemode.DialogResult != System.Windows.Forms.DialogResult.Cancel)
            //{

            // 30/10/2024 - chinna
            //DataSyncService.DataSynchronizerClient dataClient = new DataSyncService.DataSynchronizerClient();
            //if (IsServiceAlive(dataClient.Endpoint.Address.ToString()))
            //{
            //    if (!this.IsLicenseKeyMismatchedByLicenseKeyDBLocation())
            //    {
            //        //To get portal message after
            //        frmLoginDashboard dashboard = DasboardForm();
            //        if (dashboard != null)
            //        {
            //            dashboard.GetPortalMessages();
            //        }
            //    }
            //}
            //}
        }

        private void nbiImportMasters_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //frmPortalUpdatesMode updatemode = new frmPortalUpdatesMode(PortalUpdates.ImportMasters);
            //updatemode.ShowDialog();

            LoadHOImportMasters();
            //frmImportHeadofficeMasters frmMasters = new frmImportHeadofficeMasters();
            //frmMasters.ShowDialog();
        }

        private void LoadHOImportMasters()
        {
            frmPortalUpdates updatemode = new frmPortalUpdates(PortalUpdates.ImportMasters);
            updatemode.ShowDialog();

            //On 26/06/2019, reload home page
            if (updatemode.DialogResult != System.Windows.Forms.DialogResult.Cancel)
            {
                if (!this.IsLicenseKeyMismatchedByLicenseKeyDBLocation())
                {
                    DataSyncService.DataSynchronizerClient dataClient = new DataSyncService.DataSynchronizerClient();
                    if (IsServiceAlive(dataClient.Endpoint.Address.ToString()))
                    {
                        frmLoginDashboard dashboard = DasboardForm();
                        if (dashboard != null)
                        {
                            try
                            {
                                this.ShowWaitDialog();
                                dashboard.IsSelected = true;
                                dashboard.LoadDashBoardDefaults();
                                dashboard.GetPortalMessages();
                                dashboard.IsSelected = false;
                                this.CloseWaitDialog();
                            }
                            catch (Exception err)
                            {
                                MessageRender.ShowMessage(err.Message);
                            }
                        }
                    }
                }
            }
        }

        private void nbiLicenseKey_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //frmPortalUpdatesMode updatemode = new frmPortalUpdatesMode(PortalUpdates.UpdateLicense);
            //updatemode.ShowDialog();

            LoadHOLicenseKey();
        }

        private static void LoadHOLicenseKey()
        {
            SettingProperty.Is_Application_Logout = true;
            frmPortalUpdates updatemode = new frmPortalUpdates(PortalUpdates.UpdateLicense);
            updatemode.ShowDialog();
        }

        private void nbiPortalUpdates_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                frmPortalUpdatesMode updatemode = new frmPortalUpdatesMode();
                updatemode.ShowDialog();
                //this.ShowDialog("Connecting to Portal..");
                //if (CommonMethod.CheckForInternetConnection())
                //{
                //    bool IsExists = false;
                //    CloseWaitDialog();
                //    if (!string.IsNullOrEmpty(this.AppSetting.BranchOfficeCode) && !string.IsNullOrEmpty(this.AppSetting.HeadofficeCode))
                //    {
                //        try
                //        {
                //            DataSyncService.DataSynchronizerClient dataClient = new DataSyncService.DataSynchronizerClient();
                //            IsExists = dataClient.IsBranchExists(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode);
                //        }
                //        catch (FaultException<DataSyncService.AcMeServiceException> ex)
                //        {
                //            IsExists = false;
                //            this.ShowMessageBox(ex.Detail.Message);
                //        }
                //    }
                //    else
                //    {
                //        IsExists = false;
                //        this.ShowMessageBox("Head Office Code and Branch Office Code not found in Branch Office.</br>Kindly download your Branch License from Head Office using your credentials");
                //    }

                //    if (IsExists)
                //    {
                //        frmPortalUpdates frmUpdates = new frmPortalUpdates();
                //        frmUpdates.ShowDialog();
                //    }
                //    else
                //    {
                //        frmPortalLogin frmportalLogin = new frmPortalLogin();
                //        frmportalLogin.ShowDialog();
                //    }
                //}
                //else
                //{
                //    CloseWaitDialog();
                //    this.ShowMessageBox("Internet Connection is not  available.");
                //}

            }
            catch (Exception ex)
            {
                AcMELog.WriteLog(ex.ToString());
            }
        }



        private void bbiCloseAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            closeAllActiveTabs();
        }


        private void nbiState_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadState();
        }

        private void LoadState()
        {
            bool hasHome = HasFormInMDI(typeof(frmStateView).Name);

            ShowWaitDialog();

            if (!hasHome)
            {
                frmStateView frmState = new frmStateView();
                frmState.MdiParent = this;
                frmState.Show();
            }

            CloseWaitDialog();
        }

        private void nbiInstPreference_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadInstitutepreferenceDetails();
        }

        private void nbiAuditType_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadAuditType();
        }

        private void LoadAuditType()
        {
            bool hasHome = HasFormInMDI(typeof(frmAuditTypeView).Name);

            ShowWaitDialog();

            if (!hasHome)
            {
                frmAuditTypeView frmAuditType = new frmAuditTypeView();
                frmAuditType.MdiParent = this;
                frmAuditType.Show();
            }

            CloseWaitDialog();
        }

        private void hlkAccountInfo_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            frmAddNewUser frmAddUser = new frmAddNewUser(this.UtilityMember.NumberSet.ToInteger(this.UserId), (int)UserVisibleOptions.DisableRights);
            frmAddUser.ShowDialog();
        }

        //private void bbiBranch_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    frmInstiPrefernce frmInstitutePre = new frmInstiPrefernce();
        //    frmInstitutePre.ShowDialog();
        //}

        private void nbiAmendments_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                //this.ShowWaitDialog("Connecting to Portal..");
                this.ShowWaitDialog(this.GetMessage(MessageCatalog.Common.COMMON_CONNECTING_PORTAL));
                if (this.CheckForInternetConnection())
                {
                    bool hasHome = HasFormInMDI(typeof(frmAmendmentNotes).Name);
                    if (!hasHome)
                    {
                        frmAmendmentNotes frmAmendmentNotes = new frmAmendmentNotes();
                        frmAmendmentNotes.MdiParent = this;
                        frmAmendmentNotes.Show();
                    }
                }
                else
                {
                    CloseWaitDialog();
                    //this.ShowMessageBox("Internet connection is not  available. Check your internet connectivity");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_CHECK_INTERNET_CONNECTIVITY));
                }
            }
            catch (Exception ex)
            {
                AcMELog.WriteLog(ex.ToString());
            }
        }

        private void bbiFinanceReport_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetVisibleConfiguration = true;
            SetVisibleDate = false;
            SetVisibleProject = false;
            SetVisibleCloseAllTabs = SetCloseAllTabsVisibility;
            SettingProperty.ReportModuleId = (int)ReportModule.Finance;
            LoadReport();
        }

        private void bbiTDSReports_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetVisibleConfiguration = true;
            SetVisibleDate = false;
            SetVisibleProject = false;
            SetVisibleCloseAllTabs = SetCloseAllTabsVisibility;
            SettingProperty.ReportModuleId = (int)ReportModule.TDS;
            LoadReport();
        }

        private void nbiTDSDeduction_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadTDSDeductions();
        }

        private static void LoadTDSDeductions()
        {
            frmTDSPartyTrans frmTDSDeduction = new frmTDSPartyTrans();
            frmTDSDeduction.ShowDialog();
        }

        private static void LoadTDSSettings()
        {
            TDSSettings tdsSettings = new TDSSettings();
            tdsSettings.ShowDialog();
        }

        private void nbiTDSCompanyInfo_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadTDSCompanyInfo();
        }

        private static void LoadTDSCompanyInfo()
        {
            frmCompanyTDSDeductors frmTDSCompany = new frmCompanyTDSDeductors();
            frmTDSCompany.ShowDialog();
        }

        #endregion

        #region Common Interface
        private void SetEventHandlers(Control ctrlContainer, string ControlTag)
        {
            switch (ControlTag)
            {
                case PROJECTTAG:
                    Control.ControlCollection projectControlls = ctrlContainer.Controls;
                    projectControlls.OfType<GridLookUpEdit>();
                    foreach (Control ctrl in projectControlls)
                    {
                        if (ctrl != null)
                        {
                            if (ctrl is GridLookUpEdit && ctrl.Tag == "PR")
                            {
                                dtBaseProject = ctrl as GridLookUpEdit;
                                if (dtBaseProject != null)
                                {
                                    dtBaseProject.EditValue = this.AppSetting.UserProjectId;
                                }
                            }
                            if (ctrl.HasChildren)
                            {
                                SetEventHandlers(ctrl, ControlTag);
                            }
                        }
                    }
                    break;

                case DATETAG:
                    Control.ControlCollection DateControlls = ctrlContainer.Controls;
                    DateControlls.OfType<DateEdit>();
                    foreach (Control ctrl in DateControlls)
                    {
                        if (ctrl != null)
                        {
                            if (ctrl is DateEdit && dtBaseDate == null)
                            {
                                dtBaseDate = ctrl as DateEdit;
                                if (dtBaseDate != null)
                                {
                                    dtBaseDate.Select();
                                }
                            }
                            if (ctrl.HasChildren)
                            {
                                SetEventHandlers(ctrl, ControlTag);
                            }
                        }
                    }
                    break;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///Created by alwar on 14/12/2015, to keep/store in dicOpenedFormmLeftPanel  selected left menu bar's group name (Accounts) 
        ///and its main menu  (Finance) when user selects particular menu.
        ///When user selectes on particulate tab/form in mdi tabs, concern main menu and left menu group would be selected and expaneded
        /// Storing in dictionary based on formname and its tabs count (frmvoucherview2)
        /// </summary>
        private void ActivateLeftMenuProperty()
        {
            try
            {
                if (!(leftMenuBar.SelectedLink == null))
                {
                    Form frmactive = xtMdiManager.SelectedPage.MdiChild;
                    string activeformkey = frmactive.Name;
                    if (dicOpenedFormmLeftPanel.ContainsKey(activeformkey)) //when select tab, get properties from dictionay 
                    {
                        string[] activeproperties = dicOpenedFormmLeftPanel[activeformkey] as string[];
                        if (activeproperties.Length == 2)
                        {
                            if (this.UtilityMember.NumberSet.ToInteger(activeproperties.GetValue(1).ToString()) != (int)Moudule.Reports)   //If Reports, dont get anything
                            {
                                //Get main menu 
                                ActiveNavGroupId = this.UtilityMember.NumberSet.ToInteger(activeproperties.GetValue(1).ToString());
                                //Get submenu group
                                NavBarGroup activebargroup = leftMenuBar.Groups[activeproperties.GetValue(0).ToString()];
                                if (activebargroup != null)
                                {
                                    CollapseNavBar(activebargroup.Caption, ActiveNavGroupId);
                                    activebargroup.Expanded = true;
                                }
                            }
                        }
                    }
                    else if (!dicOpenedFormmLeftPanel.ContainsKey(activeformkey)) //First Time: If dictionay does not contain property, assign it
                    {
                        //keep or store left menu groupname and its active main menu
                        string[] activeproperties = new string[] { leftMenuBar.SelectedLink.Group.Name, activeNavGroupId.ToString() };
                        dicOpenedFormmLeftPanel.Add(activeformkey, activeproperties);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        private void closeAllActiveTabs()
        {
            //if (XtraMessageBox.Show("Do you want to close Active Tabs?", "Acme.erp", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            if (XtraMessageBox.Show(this.GetMessage(MessageCatalog.Common.COMMON_CLOSE_ACTIVE_TABS), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                foreach (Form frmActive in this.MdiChildren)
                {
                    frmActive.Close();
                }
                LoadLoginHome();
            }
        }

        private void InitializeTitlePanel()
        {
            System.ComponentModel.ComponentResourceManager resources = new DevExpress.ExpressApp.Win.Templates.XafComponentResourceManager(typeof(frmMain));
            captionPanel = new AppCpation();
            resources.ApplyResources(this.captionPanel, "captionPanel");
            captionPanel.MinimumSize = new System.Drawing.Size(600, 25);
            captionPanel.Font = new Font("", 15);
            captionPanel.Name = "captionPanel";
            captionPanel.Text = "";
            captionPanel.TabStop = false;
            pnlTitle.Controls.Add(captionPanel);
            captionPanel.Location = new Point(-15, 0);
        }

        //private void LoadFdRegisters()
        //{
        //    bool hasHome = HasFormInMDI(typeof(frmFDRegisters).Name);
        //    ShowWaitDialog();

        //    if (!hasHome)
        //    {
        //        frmFDRegisters frmRegisters = new frmFDRegisters();
        //        frmRegisters.MdiParent = this;
        //        frmRegisters.Show();
        //    }

        //    CloseWaitDialog();
        //}

        /// <summary>
        /// Locking the module based on the license key
        /// Modules allowed for the branch is kept in the view at the time of login.
        /// </summary>
        private void EnableMenu()
        {
            bbiTDS.Visibility = bbiTDSReports.Visibility = SettingProperty.EnableTDS ? BarItemVisibility.Always : BarItemVisibility.Never;
            bbiAsset.Visibility = nbiAsset.Visibility = SettingProperty.EnableAsset ? BarItemVisibility.Always : BarItemVisibility.Never;
            bbiPayroll.Visibility = bbiPayrollReport.Visibility = SettingProperty.EnablePayroll ? BarItemVisibility.Always : BarItemVisibility.Never;
            nbgStock.Visibility = bbiStockMenu.Visibility = SettingProperty.EnableStock ? BarItemVisibility.Always : BarItemVisibility.Never;
            bbiNetworking.Visibility = bbiNetworkReport.Visibility = SettingProperty.EnableNetworking ? BarItemVisibility.Always : BarItemVisibility.Never;
            //bbiTDSReports.Visibility = BarItemVisibility.Always;
            navConnectDBConnection.Visible = navRestoreData.Visible = true;
            nbiLicenseKey.Visible = SettingProperty.EnableLicenseKeyFile;
            nbiMapLedgerOption.Visible = true;
            navConnectDBConnection.Visible = false;
            nbiImportMasters.Visible = nbiExportVouchers.Visible = nbiMapLedgers.Visible = nbiUploadBranchOfficeDB.Visible = (this.AppSetting.EnablePortal == 1);

            nbiManageMultiDB.Visible = (this.AppSetting.AccesstoMultiDB == 1);
            //bbiLicensePeriod.Caption = "License Period :  " + this.UtilityMember.DateSet.ToDate(SettingProperty.Current.LicenseKeyYearFrom, "dd-MMM-yyyy").ToString() + " to " + this.UtilityMember.DateSet.ToDate(SettingProperty.Current.LicenseKeyYearTo, "dd-MMM-yyyy").ToString();
            bbiLicensePeriod.Caption = this.GetMessage(MessageCatalog.Common.COMMON_LICENSE_PERIOD) + "  " + this.UtilityMember.DateSet.ToDate(SettingProperty.Current.LicenseKeyYearFrom, "dd-MMM-yyyy").ToString() + " " + this.GetMessage(MessageCatalog.Common.COMMON_LICENSE_TO_INFO) + " " + this.UtilityMember.DateSet.ToDate(SettingProperty.Current.LicenseKeyYearTo, "dd-MMM-yyyy").ToString();

            // In Visible the GST Polocy
            if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableGST).Equals((int)YesNo.Yes))
            {
                nbiGSTPolicy.Visible = true;
            }

        }

        private void SetRightsPayrollOnly()
        {
            if (SettingProperty.EnablePayrollOnly)
            {
                bbiPayroll.Visibility = bbiPayrollReport.Visibility = bbiDataUtlity.Visibility = SettingProperty.EnablePayrollOnly ? BarItemVisibility.Always : BarItemVisibility.Never;

                bbiHome.Visibility = bbiTDS.Visibility = bbiFinance.Visibility = bbiAsset.Visibility = nbgStock.Visibility = bbiNetworking.Visibility = bbiTDSReports.Visibility = bbiFinanceReport.Visibility = BarItemVisibility.Never;

                nbgUserManagement.Visible = nbiManageMultiDB.Visible = false;

                nbiImportSplitProject.Visible = nbiSplitLedger.Visible = nbiDataMigration.Visible = nbiExportToTally.Visible =
                    nbiImportSplitProject.Visible = nbiSplitLedger.Visible = nbiDataMigration.Visible = nbiExportToTally.Visible =
                    nbiImportAssetMasters.Visible = nbiSSPIntegration.Visible = nbiPortalUpdates.Visible = nbiAmendments.Visible = navBarSeparatorItem6.Visible = nbiDownloadTemplates.Visible = false;

                CollapseNavBar(nbgDefinition.Caption, bbiPayroll.Id);
            }
        }


        public void LoadLogoutFunc()
        {
            string[] DB;
            string databaseName = string.Empty;
            SettingProperty.Is_Application_Logout = true;

            if (Is_Branch_Active && !AppSetting.IsSplitPreviousYearAcmeerpDB)
            {
                //1. 18/01/2018 Show export voucher form if setting is enbaled ------------------------------------------------------------------------------------
                //If Export voucher is enabled in finance setting, if user does not do it, diable logout
                // 15/04/2025, Chinna*
                if (this.AppSetting.ExportVouchersBeforeClose == "1" || this.AppSetting.IS_SDB_ING)
                {
                    DialogResult dialogVoucherExport = AlertToExportVoucher();
                    if (dialogVoucherExport == System.Windows.Forms.DialogResult.Cancel)
                    {
                        SettingProperty.Is_Application_Logout = false; //diable logout
                    }
                }
                //-------------------------------------------------------------------------------------------------------------------------------------------------

                if (SettingProperty.Is_Application_Logout)
                {
                    //3. Alert to take backup on 12/09/2018
                    if (UtilityMember.NumberSet.ToInteger(AppSetting.DontAlertTakeBackupBeforeClose) == 0)
                        LoadDBBackup(true);

                    ShowWaitDialog(this.GetMessage(MessageCatalog.Common.LOGOUT));
                    string mysqlDefaultConnection = ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString;
                    string[] ConnectionString = mysqlDefaultConnection.Split(';');
                    if (mysqlDefaultConnection.Contains("port") == true)
                    {
                        string[] DefaultAppString = mysqlDefaultConnection.Split(';');
                        string[] SName = DefaultAppString[0].ToString().Split('=');
                        string[] Port = DefaultAppString[1].ToString().Split('=');
                        string[] CheckDataBase = DefaultAppString[2].ToString().Split('=');
                        if (CheckDataBase[0].Contains("connection timeout"))
                        {
                            DB = DefaultAppString[3].ToString().Split('=');
                        }
                        else
                        {
                            DB = DefaultAppString[2].ToString().Split('=');
                        }
                        databaseName = DB[1].ToString();
                    }
                    //On 10/09/2019, to check corrupted db or not
                    if (!BackRestore.IsDBCorrupted())
                    {
                        //On 26/11/2024 - To keep number of voucher
                        Int32 noofactivevouchers = this.GetNoActiveVouchers();
                        BackRestore.No_of_Active_Vouchers = noofactivevouchers;

                        resultArgs = BackRestore.MySqlBackup("", databaseName, 1);
                    }
                    if (!resultArgs.Success)
                    {
                        AcMELog.WriteLog("Problem in taking Backup" + resultArgs.Message);
                    }
                    CloseWaitDialog();
                    Application.Restart();
                }
            }
            else
            {
                CloseWaitDialog();
                Application.Restart();
            }
        }

        private void LoadFdRegistersView()
        {
            bool hasHome = HasFormInMDI(typeof(frmFDRegistersView).Name);
            ShowWaitDialog();

            if (!hasHome)
            {
                frmFDRegistersView frmRegisters = new frmFDRegistersView();
                frmRegisters.MdiParent = this;
                frmRegisters.Show();
            }

            CloseWaitDialog();
        }

        private void LoadBRS()
        {
            bool hasHome = HasFormInMDI(typeof(frmBRS).Name);
            CloseFormInMDI(typeof(frmBRS).Name);
            // ShowWaitDialog();
            frmBRS frmBRS = new frmBRS();
            frmBRS.ShowDialog();
        }

        private void LoadAccountPeriod()
        {
            if (!string.IsNullOrEmpty(TransactionPeriod))
            {
                bool hasHome = HasFormInMDI(typeof(frmAccountingPeriodView).Name);
                CloseFormInMDI(typeof(frmAccountingPeriodView).Name);
                ShowWaitDialog();
                frmAccountingPeriodView frmAccountingPeriod = new frmAccountingPeriodView();
                frmAccountingPeriod.MdiParent = this;
                frmAccountingPeriod.Show();
                CloseWaitDialog();
            }
            else
            {
                bool hasHome = HasFormInMDI(typeof(frmAccountingPeriodView).Name);
                CloseFormInMDI(typeof(frmAccountingPeriodView).Name);
                frmAccountingPeriodView frmAccountingPeriod = new frmAccountingPeriodView(true);
                frmAccountingPeriod.MdiParent = this;
                frmAccountingPeriod.Show();
                frmAccountingPeriod.LoadTransPeriodAdd();
            }
        }

        private void ShowTransactionViewForm(int transactionType, DefaultVoucherTypes VoucherType)
        {
            try
            {
                if ((VoucherType == DefaultVoucherTypes.Payment || VoucherType == DefaultVoucherTypes.Contra || VoucherType == DefaultVoucherTypes.Journal)
                    || (VoucherType == DefaultVoucherTypes.Receipt && AppSetting.ENABLE_TRACK_RECEIPT_MODULE))
                {
                    DialogResult = DialogResult.Cancel;
                    if (ProjectSelection == "1")
                    {
                        //if (!string.IsNullOrEmpty(DefaultProject))
                        //{
                        frmProjectSelection frmprojSelection = new frmProjectSelection(Bosco.Utility.ProjectSelection.EnableVoucherSelectionMethod);
                        frmprojSelection.VoucherTypes = VoucherType;
                        frmprojSelection.ShowDialog();
                        DialogResult = frmprojSelection.DialogResult;
                        if (frmprojSelection.ProjectName != string.Empty)
                        {
                            RecentProjectId = frmprojSelection.ProjectId;
                            RecentProject = frmprojSelection.ProjectName;
                            SelectionType = frmprojSelection.SelectionTye;
                            RecentVoucherDate = frmprojSelection.RecentVoucherDate;
                            //lblProject.Text = RecentProject;// +" (" + TransactionPeriod + ")";
                            this.Text = RecentProject;
                        }
                        FetchDateDuration(RecentProjectId, this.UtilityMember.DateSet.ToDate(RecentVoucherDate, false));
                        // }
                        //else
                        //{
                        //    if (XtraMessageBox.Show("Project is not yet created. Do you want to create Project now?", "AcMeERP", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        //    {
                        //        frmProjectAdd frmProject = new frmProjectAdd((int)AddNewRow.NewRow);
                        //        frmProject.ShowDialog();
                        //    }
                        //}
                    }
                    else
                    {
                        DialogResult = DialogResult.OK;
                    }

                    if (VoucherType == DefaultVoucherTypes.Journal)
                    {
                        if (RecentProject != string.Empty)
                        {
                            if (selectionType == (int)YesNo.Yes && ProjectSelection == AppSetting.DefaultAdminUserId.ToString())
                            {
                                bool hasfrmHome = HasFormInMDI(typeof(frmTransactionJournalView).Name);
                                CloseFormInMDI(typeof(frmTransactionJournalView).Name);
                                frmTransactionJournalView frmTransaction = new frmTransactionJournalView(RecentVoucherDate, RecentProjectId, RecentProject, transactionType, SelectionType);
                                frmTransaction.MdiParent = this;
                                frmTransaction.Show();
                            }
                            else
                            {
                                if (!LoginUser.IsFullRightsReservedUser)
                                {
                                    if (CommonMethod.ApplyUserRights((int)Journal.CreateJournalVoucher) != 0)
                                    {
                                        JournalAdd frmTransaction = new JournalAdd(RecentVoucherDate, RecentProjectId, RecentProject, (int)AddNewRow.NewRow, SelectionType, (Int32)DefaultVoucherTypes.Journal);
                                        frmTransaction.ShowDialog();

                                        //On 18/02/2025 - Don't validate audit lock while showing view or entry screen
                                        /*if (!IsLockedTransaction(!string.IsNullOrEmpty(RecentVoucherDate) ? this.UtilityMember.DateSet.ToDate(RecentVoucherDate, false) : DateTime.MinValue))
                                        {
                                            JournalAdd frmTransaction = new JournalAdd(RecentVoucherDate, RecentProjectId, RecentProject, (int)AddNewRow.NewRow, SelectionType, (Int32)DefaultVoucherTypes.Journal);
                                            frmTransaction.ShowDialog();
                                        }
                                        else
                                        {
                                            //this.ShowMessageBox("Voucher is locked, cannot make voucher entry for '" + RecentProject + "'");
                                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_VOUCHER_LOCKED) + " '" + RecentProject + "'" +
                                                " during the period " + this.UtilityMember.DateSet.ToDate(TransactionLockFromDate.ToShortDateString()) +
                                                " - " + this.UtilityMember.DateSet.ToDate(TransactionLockToDate.ToShortDateString()));
                                        }*/
                                    }
                                }
                                else
                                {
                                    JournalAdd frmTransaction = new JournalAdd(RecentVoucherDate, RecentProjectId, RecentProject, (int)AddNewRow.NewRow, SelectionType, (Int32)DefaultVoucherTypes.Journal);
                                    frmTransaction.ShowDialog();

                                    //On 18/02/2025 - Don't validate audit lock while showing view or entry screen
                                    /*if (!IsLockedTransaction(!string.IsNullOrEmpty(RecentVoucherDate) ? this.UtilityMember.DateSet.ToDate(RecentVoucherDate, false) : DateTime.MinValue))
                                    {
                                        JournalAdd frmTransaction = new JournalAdd(RecentVoucherDate, RecentProjectId, RecentProject, (int)AddNewRow.NewRow, SelectionType, (Int32)DefaultVoucherTypes.Journal);
                                        frmTransaction.ShowDialog();
                                    }
                                    else
                                    {
                                        //this.ShowMessageBox("Voucher is locked, cannot make voucher entry for '" + RecentProject + "'");
                                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_VOUCHER_LOCKED) + " '" + RecentProject + "'" +
                                            " during the period " + this.UtilityMember.DateSet.ToDate(TransactionLockFromDate.ToShortDateString()) +
                                                " - " + this.UtilityMember.DateSet.ToDate(TransactionLockToDate.ToShortDateString()));
                                    }*/
                                }
                            }
                        }
                    }
                    else
                    {
                        if (RecentProject != string.Empty && DialogResult == DialogResult.OK)
                        {
                            if (selectionType == (int)YesNo.No && ProjectSelection == AppSetting.DefaultAdminUserId.ToString())
                            {
                                if (!LoginUser.IsFullRightsReservedUser)
                                {
                                    if (CommonMethod.ApplyUserRights((int)Receipt.CreateReceiptVoucher) != 0 && transactionType == 0)
                                    {
                                        bool hasfrmHome = HasFormInMDI(typeof(frmTransactionMultiAdd).Name);
                                        frmTransactionMultiAdd frmTransaction = new frmTransactionMultiAdd(RecentVoucherDate, RecentProjectId, RecentProject, (int)AddNewRow.NewRow, transactionType);
                                        frmTransaction.Show();

                                        //On 18/02/2025 - Don't validate audit lock while showing view or entry screen
                                        /*if (!IsLockedTransaction(!string.IsNullOrEmpty(RecentVoucherDate) ? this.UtilityMember.DateSet.ToDate(RecentVoucherDate, false) : DateTime.MinValue))
                                        {
                                            bool hasfrmHome = HasFormInMDI(typeof(frmTransactionMultiAdd).Name);
                                            frmTransactionMultiAdd frmTransaction = new frmTransactionMultiAdd(RecentVoucherDate, RecentProjectId, RecentProject, (int)AddNewRow.NewRow, transactionType);
                                            frmTransaction.Show();
                                        }
                                        else
                                        {
                                            //this.ShowMessageBox("Voucher is locked, cannot make voucher entry for '" + RecentProject + "'");
                                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_VOUCHER_LOCKED) + " '" + RecentProject + "'" +
                                                " during the period " + this.UtilityMember.DateSet.ToDate(TransactionLockFromDate.ToShortDateString()) +
                                                " - " + this.UtilityMember.DateSet.ToDate(TransactionLockToDate.ToShortDateString()));
                                        }*/
                                    }
                                    else if (CommonMethod.ApplyUserRights((int)Payment.CreatePaymentVoucher) != 0 && transactionType == 1)
                                    {
                                        bool hasfrmHome = HasFormInMDI(typeof(frmTransactionMultiAdd).Name);
                                        frmTransactionMultiAdd frmTransaction = new frmTransactionMultiAdd(RecentVoucherDate, RecentProjectId, RecentProject, (int)AddNewRow.NewRow, transactionType);
                                        frmTransaction.Show();

                                        //On 18/02/2025 - Don't validate audit lock while showing view or entry screen
                                        /*if (!IsLockedTransaction(!string.IsNullOrEmpty(RecentVoucherDate) ? this.UtilityMember.DateSet.ToDate(RecentVoucherDate, false) : DateTime.MinValue))
                                        {
                                            bool hasfrmHome = HasFormInMDI(typeof(frmTransactionMultiAdd).Name);
                                            frmTransactionMultiAdd frmTransaction = new frmTransactionMultiAdd(RecentVoucherDate, RecentProjectId, RecentProject, (int)AddNewRow.NewRow, transactionType);
                                            frmTransaction.Show();
                                        }
                                        else
                                        {
                                            //this.ShowMessageBox("Voucher is locked, cannot make voucher entry for '" + RecentProject + "'");
                                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_VOUCHER_LOCKED) + " '" + RecentProject + "'" +
                                                " during the period " + this.UtilityMember.DateSet.ToDate(TransactionLockFromDate.ToShortDateString()) +
                                                " - " + this.UtilityMember.DateSet.ToDate(TransactionLockToDate.ToShortDateString()));
                                        }*/
                                    }
                                    else if (CommonMethod.ApplyUserRights((int)Contra.CreateContraVoucher) != 0 && transactionType == 2)
                                    {
                                        bool hasfrmHome = HasFormInMDI(typeof(frmTransactionMultiAdd).Name);
                                        frmTransactionMultiAdd frmTransaction = new frmTransactionMultiAdd(RecentVoucherDate, RecentProjectId, RecentProject, (int)AddNewRow.NewRow, transactionType);
                                        frmTransaction.Show();

                                        //On 18/02/2025 - Don't validate audit lock while showing view or entry screen
                                        /*if (!IsLockedTransaction(!string.IsNullOrEmpty(RecentVoucherDate) ? this.UtilityMember.DateSet.ToDate(RecentVoucherDate, false) : DateTime.MinValue))
                                        {
                                            bool hasfrmHome = HasFormInMDI(typeof(frmTransactionMultiAdd).Name);
                                            frmTransactionMultiAdd frmTransaction = new frmTransactionMultiAdd(RecentVoucherDate, RecentProjectId, RecentProject, (int)AddNewRow.NewRow, transactionType);
                                            frmTransaction.Show();
                                        }
                                        else
                                        {
                                            //this.ShowMessageBox("Voucher is locked.Cannot make voucher entry for '" + RecentProject + "'");
                                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_VOUCHER_LOCKED) + " '" + RecentProject + "'" +
                                                " during the period " + this.UtilityMember.DateSet.ToDate(TransactionLockFromDate.ToShortDateString()) +
                                                " - " + this.UtilityMember.DateSet.ToDate(TransactionLockToDate.ToShortDateString()));
                                        }*/
                                    }
                                }
                                else
                                {
                                    bool hasfrmHome = HasFormInMDI(typeof(frmTransactionMultiAdd).Name);
                                    Int32 voucherdefinitionId = transactionType + 1;
                                    frmTransactionMultiAdd frmTransaction = new frmTransactionMultiAdd(RecentVoucherDate, RecentProjectId, RecentProject, (int)AddNewRow.NewRow, transactionType, false, voucherdefinitionId);
                                    frmTransaction.Show();

                                    //On 18/02/2025 - Don't validate audit lock while showing view or entry screen
                                    /*if (!IsLockedTransaction(!string.IsNullOrEmpty(RecentVoucherDate) ? this.UtilityMember.DateSet.ToDate(RecentVoucherDate, false) : DateTime.MinValue))
                                    {
                                        bool hasfrmHome = HasFormInMDI(typeof(frmTransactionMultiAdd).Name);
                                        Int32 voucherdefinitionId = transactionType + 1;
                                        frmTransactionMultiAdd frmTransaction = new frmTransactionMultiAdd(RecentVoucherDate, RecentProjectId, RecentProject, (int)AddNewRow.NewRow, transactionType, false, voucherdefinitionId);
                                        frmTransaction.Show();
                                    }
                                    else
                                    {
                                        //this.ShowMessageBox("Voucher is locked, cannot make voucher entry for '" + RecentProject + "'");
                                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_VOUCHER_LOCKED) + " '" + RecentProject + "'" +
                                            " during the period " + this.UtilityMember.DateSet.ToDate(TransactionLockFromDate.ToShortDateString()) +
                                                " - " + this.UtilityMember.DateSet.ToDate(TransactionLockToDate.ToShortDateString()));
                                    }*/
                                }
                            }
                            else
                            {
                                bool hasfrmHome = HasFormInMDI(typeof(frmTransactionView).Name);
                                CloseFormInMDI(typeof(frmTransactionView).Name);
                                frmTransactionView frmTransaction = new frmTransactionView(RecentVoucherDate, RecentProjectId, RecentProject, transactionType, SelectionType);
                                frmTransaction.MdiParent = this;
                                frmTransaction.Show();
                            }
                        }
                    }
                    // SetTransPeriod();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            {
                //CloseWaitDialog();
            }
        }

        private void ShowProjectSelectionWindow()
        {
            int prvProjectId = RecentProjectId;
            if (string.IsNullOrEmpty(TransactionPeriod))
            {
                if (this.LoginUser.LoginUserId != AppSetting.DefaultAdminUserId.ToString())
                {
                    if (CommonMethod.ApplyUserRights((int)TransactionPeriods.CreateTransaction) != 0)
                    {
                        // if (XtraMessageBox.Show("Transaction Period is not created . Do you want to create the Transaction Period ?", "Acme.erp", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        // {
                        LoadAccountPeriod();
                        // }
                        // else
                        // {
                        // LoadLogoutFunc();
                        //}
                    }
                    else
                    {
                        this.ShowMessageBoxWarning("Logged User does not have previleges to create Accounting Year.");
                    }
                }
                else
                {
                    //if (XtraMessageBox.Show("Transaction Period is not created . Do you want to create the Transaction Period?", "Acme.erp", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    // {
                    LoadAccountPeriod();
                    //}
                    // else
                    // {
                    //  LoadLogoutFunc();
                    // }
                }
            }

            if (!string.IsNullOrEmpty(TransactionPeriod))
            {
                //if (!string.IsNullOrEmpty(DefaultProject))
                //{
                frmProjectSelection frmprojectSelection = new frmProjectSelection(Bosco.Utility.ProjectSelection.DisableVoucherSelectionMethod, RecentProjectId, RecentProject);
                frmprojectSelection.ShowDialog();
                if (frmprojectSelection.ProjectName != string.Empty && frmprojectSelection.DialogResult == DialogResult.OK)
                {
                    RecentProjectId = frmprojectSelection.ProjectId;
                    RecentProject = frmprojectSelection.ProjectName;
                    RecentVoucherDate = frmprojectSelection.RecentVoucherDate;
                    SelectionType = frmprojectSelection.SelectionTye;
                    //lblProject.Text = RecentProject; // +" (" + TransactionPeriod + ")";
                    this.Text = RecentProject;

                    if (this.ActiveMdiChild.Name == typeof(frmLoginDashboard).Name && prvProjectId != RecentProjectId)
                    {
                        Form frmdashboard = this.ActiveMdiChild;
                        this.ActivateMdiChild(null);
                        this.ActivateMdiChild(frmdashboard);
                    }
                }
                FetchDateDuration(RecentProjectId, this.UtilityMember.DateSet.ToDate(RecentVoucherDate, false));
                //}
                //else
                //{
                //    if (XtraMessageBox.Show("Project is not yet created. Do you want to create Project now?", "AcMeERP", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                //    {
                //        frmProjectAdd frmProject = new frmProjectAdd((int)AddNewRow.NewRow);
                //        frmProject.ShowDialog();
                //    }
                //}
            }
        }

        //private void LoadLogout()
        //{
        //    this.Hide();
        //    frmLogin login = new frmLogin();
        //    if (login.ShowDialog() == DialogResult.OK)
        //    {
        //        frmMain main = new frmMain();
        //        main.ShowDialog();
        //    }
        //    LoadLogoutFunc();
        //    //Application.Restart();
        //}
        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.Escape))
            {
                this.Close();
            }

            if (Is_Branch_Active)
            {
                //Main Menu
                KeyData = keysMainmenu(KeyData);

                if (ActiveNavGroupId == (int)Moudule.Finance)
                {
                    KeyData = KeysModuleFinanace(KeyData);
                }
                else if (ActiveNavGroupId == (int)Moudule.Utlities && bbiDataUtlity.Visibility == BarItemVisibility.Always)
                {
                    KeyData = KeysModuleUtilities(KeyData);
                }
                else if (ActiveNavGroupId == (int)Moudule.TDS)
                {
                    KeyData = KeysModuleTDS(KeyData);
                }
                else if (ActiveNavGroupId == (int)Moudule.Payroll)
                {
                    KeyData = KeysPayroll(KeyData);
                }
                else if (ActiveNavGroupId == (int)Moudule.Stock)
                {
                    KeyData = KeysStock(KeyData);
                }
                else if ((ActiveNavGroupId == (int)Moudule.Reports))
                {
                    //KeyData = KeysMasters(KeyData);
                    //KeyData = KeysMembers(KeyData);
                    //KeyData = KeysPurpose(KeyData);
                    //KeyData = KeysSettings(KeyData);
                    //KeyData = KeysFinance(KeyData);
                    //KeyData = KeysFixedDeposit(KeyData);
                    //KeyData = KeysUsers(KeyData);
                    //KeyData = KeysDataUtility(KeyData);
                }
                else if ((ActiveNavGroupId == (int)Moudule.FixedAsset))
                {
                    KeyData = KeysAsset(KeyData);
                }
            }

            return base.ProcessCmdKey(ref msg, KeyData);
        }

        public Keys keysMainmenu(Keys KeyData)
        {
            if (KeyData == (Keys.Control | Keys.I))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    ActiveNavGroupId = bbiFinance.Id;
                    ApplyUserRights();
                    //dpAcme.Text = "Finance";
                    dpAcme.Text = this.GetMessage(MessageCatalog.Master.Module.MODULE_FINANCE_TITLE);
                }
                else
                {
                    CollapseNavBar(nbgTransaction.Caption, bbiFinance.Id);
                }
            }
            if (KeyData == (Keys.Control | Keys.H))
            {
                LoadLoginHome();
            }
            if (KeyData == (Keys.Control | Keys.R))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (bbiReportsMenu.Visibility == BarItemVisibility.Always)
                    {
                        SettingProperty.ReportModuleId = (int)ReportModule.Finance;
                        LoadReport();
                    }
                }
                else
                {
                    SettingProperty.ReportModuleId = (int)ReportModule.Finance;
                    LoadReport();
                }

            }
            if (KeyData == (Keys.Control | Keys.S))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (SettingProperty.EnableStock)
                    {
                        ApplyUserRights();
                        dpAcme.Options.AllowDockFill = true;
                        //dpAcme.Text = "Stock";
                        dpAcme.Text = this.GetMessage(MessageCatalog.Master.Module.MODULE_STOCK_TITTLE);
                    }
                }
                else
                {
                    if (SettingProperty.EnableStock)
                    {
                        //CollapseNavBar("Stock", nbgStock.Id);
                        CollapseNavBar(this.GetMessage(MessageCatalog.Master.Module.MODULE_STOCK_TITTLE), nbgStock.Id);
                        //dpAcme.Text = "Stock";
                        dpAcme.Text = this.GetMessage(MessageCatalog.Master.Module.MODULE_STOCK_TITTLE);
                    }
                }
            }

            if (KeyData == (Keys.Control | Keys.O))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (SettingProperty.EnablePayroll)
                    {
                        ApplyUserRights();
                        dpAcme.Options.AllowDockFill = true;
                        //dpAcme.Text = "Payroll";
                        dpAcme.Text = this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE);
                    }
                }
                else
                {
                    if (SettingProperty.EnablePayroll)
                    {
                        DefinePayroll();
                        //dpAcme.Text = "Payroll";
                        dpAcme.Text = this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE);
                    }
                }
            }
            if (KeyData == (Keys.Control | Keys.U))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    ApplyUserRights();
                    //CollapseNavBar("Utilities", bbiDataUtlity.Id);
                    CollapseNavBar(this.GetMessage(MessageCatalog.Master.Module.MODULE_DATA_UTILITY_TITLE), bbiDataUtlity.Id);
                    //dpAcme.Text = "Utilities";
                    dpAcme.Text = this.GetMessage(MessageCatalog.Master.Module.MODULE_DATA_UTILITY_TITLE);
                }
                else if (bbiDataUtlity.Visibility == BarItemVisibility.Always)
                {
                    //CollapseNavBar("Utilities", bbiDataUtlity.Id);
                    CollapseNavBar(this.GetMessage(MessageCatalog.Master.Module.MODULE_DATA_UTILITY_TITLE), bbiDataUtlity.Id);
                    //dpAcme.Text = "Utilities";
                    dpAcme.Text = this.GetMessage(MessageCatalog.Master.Module.MODULE_DATA_UTILITY_TITLE);
                }
            }
            if (KeyData == (Keys.Control | Keys.A))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (SettingProperty.EnableAsset)
                    {
                        MenuCaption = nbgInventory.Caption;
                        ApplyUserRights();
                        CollapseNavBar(nbgInventory.Caption, bbiAsset.Id);
                        //dpAcme.Text = "Fixed Asset";
                        dpAcme.Text = this.GetMessage(MessageCatalog.Master.Module.MODULE_FIXED_ASSET_TITTLE);
                    }
                }
                else
                {
                    if (SettingProperty.EnableAsset)
                    {
                        CollapseNavBar(nbgInventory.Caption, bbiAsset.Id);
                        //dpAcme.Text = "Fixed Asset";
                        dpAcme.Text = this.GetMessage(MessageCatalog.Master.Module.MODULE_FIXED_ASSET_TITTLE);
                    }
                }
            }
            if (KeyData == (Keys.Control | Keys.T))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    MenuCaption = nbgStockMasters.Caption;
                    ApplyUserRights();
                    //CollapseNavBar("Statutory", bbiTDS.Id);
                    CollapseNavBar(this.GetMessage(MessageCatalog.Master.Module.MODULE_FIXED_ASSET_TITTLE), bbiTDS.Id);
                }
                else
                {
                    //CollapseNavBar("Statutory", bbiTDS.Id);
                    CollapseNavBar(this.GetMessage(MessageCatalog.Master.Module.MODULE_FIXED_ASSET_TITTLE), bbiTDS.Id);
                }
            }

            if (KeyData == (Keys.Control | Keys.L))
            {
                LoadLogoutFunc();
            }
            if (KeyData == (Keys.Control | Keys.D))
            {
                GetLastestUpdater();
            }
            if (KeyData == (Keys.Control | Keys.D))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    groupNavName = DataUtility;
                    MenuCaption = nbgDataUtility.Caption;
                    ApplyUserRights();
                }
                else
                {
                    CollapseNavBar(nbgDataUtility.Caption, bbiDataUtlity.Id);
                }
            }
            if (KeyData == (Keys.F12))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (bbiConfiguration.Enabled)
                    {
                        if (CommonMethod.ApplyUserRights((int)Menus.MasterSetting) != 0)
                        {
                            frmSettings setting = new frmSettings();
                            setting.ShowDialog();
                        }
                    }
                }
                else
                {
                    frmSettings setting = new frmSettings();
                    setting.ShowDialog();
                }
            }
            if (KeyData == (Keys.F5))
            {
                if (bbiProject.Enabled)
                    ShowProjectSelectionWindow();
                SetEventHandlers(this.ActiveMdiChild, PROJECTTAG);
            }

            if (KeyData == (Keys.Alt | Keys.F3))
            {
                glkTransPeriod.Focus();
                if (!glkTransPeriod.IsPopupOpen)
                {
                    glkTransPeriod.ShowPopup();
                }
            }

            if (KeyData == (Keys.F3))
            {
                SetEventHandlers(this.ActiveMdiChild, DATETAG);
            }

            if (KeyData == (Keys.Control | Keys.Shift | Keys.F4))
            {
                if (this.MdiChildren.Count() == 1 && this.MdiChildren[0].Name == typeof(frmLoginDashboard).Name)
                {
                    LoadLoginHome();
                }
                else
                {
                    if (bbiCloseAll.Enabled)
                    {
                        closeAllActiveTabs();
                    }

                }
            }
            //if (KeyData == (Keys.Control | Keys.K))
            //{
            //    frmAllShortcuts shortcuts = new frmAllShortcuts();
            //    shortcuts.ShowDialog();
            //}

            //On 10/04/2018, to show clean vouchers (show deleted vouchers list)
            if (KeyData == (Keys.Alt | Keys.D | Keys.E))
            {
                nbiDeleteVoucherTrans.Visible = !nbiDeleteVoucherTrans.Visible;
            }

            return KeyData;
        }

        //public Keys KeysSettings(Keys KeyData)
        //{
        //    if (KeyData == (Keys.Alt | Keys.T))
        //    {
        //        if (UserId != LoginUserId)
        //        {
        //            if (CommonMethod.ApplyRights((int)Menus.TransactionPeriod) != 0)
        //            {
        //                LoadAccountPeriod();
        //            }
        //        }
        //        else
        //        {
        //            LoadAccountPeriod();
        //        }
        //    }
        //    if (KeyData == (Keys.Alt | Keys.Control | Keys.L))
        //    {
        //        if (UserId != LoginUserId)
        //        {
        //            if (CommonMethod.ApplyRights((int)Menus.LegalEntity) != 0)
        //            {
        //                LoadLegalEntity();
        //            }
        //        }
        //        else
        //        {
        //            LoadLegalEntity();
        //        }
        //    }

        //    if (KeyData == (Keys.Alt | Keys.M))
        //    {
        //        if (UserId != LoginUserId)
        //        {
        //            if (CommonMethod.ApplyRights((int)Menus.AccountMapping) != 0)
        //            {
        //                if (!string.IsNullOrEmpty(TransactionPeriod))
        //                {
        //                    frmMapProjectLedger accountmapp = new frmMapProjectLedger();
        //                    accountmapp.ShowDialog();
        //                }
        //                else
        //                {
        //                    if (XtraMessageBox.Show("Transaction Period is not created . Do you want to create the Transaction Period?", "Acme.erp", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
        //                    {
        //                        LoadAccountPeriod();
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (!string.IsNullOrEmpty(TransactionPeriod))
        //            {
        //                frmMapProjectLedger accountmapp = new frmMapProjectLedger();
        //                accountmapp.ShowDialog();
        //            }
        //            else
        //            {
        //                if (XtraMessageBox.Show("Transaction Period is not created . Do you want to create the Transaction Period??", "Acme.erp", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
        //                {
        //                    LoadAccountPeriod();
        //                }
        //            }
        //        }
        //    }
        //    return KeyData;
        //}

        //public Keys KeysDataUtility(Keys KeyData)
        //{
        //    if (KeyData == (Keys.Alt | Keys.R))
        //    {
        //        if (UserId != LoginUserId)
        //        {
        //            if (CommonMethod.ApplyRights((int)Menus.User) != 0)
        //            {
        //                LoadRefreshBalance();
        //            }
        //        }
        //        else
        //        {
        //            LoadRefreshBalance();
        //        }
        //    }
        //    if (KeyData == (Keys.Alt | Keys.V))
        //    {
        //        if (UserId != LoginUserId)
        //        {
        //            if (CommonMethod.ApplyRights((int)Menus.User) != 0)
        //            {
        //                frmRegenerateVoucher voucher = new frmRegenerateVoucher();
        //                voucher.ShowDialog();
        //            }
        //        }
        //        else
        //        {
        //            frmRegenerateVoucher voucher = new frmRegenerateVoucher();
        //            voucher.ShowDialog();
        //        }
        //    }
        //    if (KeyData == (Keys.Alt | Keys.G))
        //    {
        //        if (UserId != LoginUserId)
        //        {
        //            if (CommonMethod.ApplyRights((int)Menus.User) != 0)
        //            {
        //                bool hasHome = HasFormInMDI(typeof(frmMigrationSelection).Name);
        //                CloseFormInMDI(typeof(frmMigrationSelection).Name);
        //                frmMigrationSelection DataMigration = new frmMigrationSelection();
        //                DataMigration.ShowDialog();
        //                if (DataMigration.MigrationMode == MigrationType.AcMePlus)
        //                {
        //                    using (DataMigrationConnectionSetUp AcMePlus = new DataMigrationConnectionSetUp())
        //                    {
        //                        AcMePlus.ShowDialog();
        //                    }
        //                }
        //                else if (DataMigration.MigrationMode == MigrationType.Tally)
        //                {
        //                    using (frmTallyMigration Tally = new frmTallyMigration())
        //                    {
        //                        Tally.ShowDialog();
        //                    }
        //                }

        //            }
        //        }
        //        else
        //        {
        //            bool hasHome = HasFormInMDI(typeof(frmMigrationSelection).Name);
        //            CloseFormInMDI(typeof(frmMigrationSelection).Name);
        //            frmMigrationSelection DataMigration = new frmMigrationSelection();
        //            DataMigration.ShowDialog();
        //            if (DataMigration.MigrationMode == MigrationType.AcMePlus)
        //            {
        //                using (DataMigrationConnectionSetUp AcMePlus = new DataMigrationConnectionSetUp())
        //                {
        //                    AcMePlus.ShowDialog();
        //                }
        //            }
        //            else if (DataMigration.MigrationMode == MigrationType.Tally)
        //            {
        //                using (frmTallyMigration Tally = new frmTallyMigration())
        //                {
        //                    Tally.ShowDialog();
        //                }
        //            }

        //        }
        //    }
        //    if (KeyData == (Keys.Alt | Keys.M))
        //    {
        //        if (UserId != LoginUserId)
        //        {
        //            if (CommonMethod.ApplyRights((int)Menus.User) != 0)
        //            {
        //                bool hasHome = HasFormInMDI(typeof(frmMergeLedgers).Name);
        //                CloseFormInMDI(typeof(frmMergeLedgers).Name);
        //                frmMergeLedgers MergeLedgers = new frmMergeLedgers();
        //                MergeLedgers.ShowDialog();
        //            }
        //        }
        //        else
        //        {
        //            bool hasHome = HasFormInMDI(typeof(frmMergeLedgers).Name);
        //            CloseFormInMDI(typeof(frmMergeLedgers).Name);
        //            frmMergeLedgers MergeLedgers = new frmMergeLedgers();
        //            MergeLedgers.ShowDialog();
        //        }
        //    }
        //    if (KeyData == (Keys.Alt | Keys.K))
        //    {
        //        if (UserId != LoginUserId)
        //        {
        //            if (CommonMethod.ApplyRights((int)Menus.User) != 0)
        //            {
        //                frmBackup frmBack = new frmBackup();
        //                frmBack.ShowDialog();
        //            }
        //        }
        //        else
        //        {
        //            frmBackup frmBack = new frmBackup();
        //            frmBack.ShowDialog();
        //        }
        //    }
        //    if (KeyData == (Keys.Alt | Keys.D))
        //    {
        //        if (UserId != LoginUserId)
        //        {
        //            if (CommonMethod.ApplyRights((int)Menus.User) != 0)
        //            {
        //                frmRestoreMultipleDB frmRestore = new frmRestoreMultipleDB();
        //                frmRestore.ShowDialog();

        //            }
        //        }
        //        else
        //        {
        //            frmRestoreMultipleDB frmRestore = new frmRestoreMultipleDB();
        //            frmRestore.ShowDialog();

        //        }
        //    }
        //    //if (KeyData == (Keys.Alt | Keys.T))
        //    //{
        //    //    if (UserId != LoginUserId)
        //    //    {
        //    //        if (CommonMethod.ApplyRights((int)Menus.User) != 0)
        //    //        {
        //    //            frmImportHeadofficeMasters frmMasters = new frmImportHeadofficeMasters();
        //    //            frmMasters.ShowDialog();
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        frmImportHeadofficeMasters frmMasters = new frmImportHeadofficeMasters();
        //    //        frmMasters.ShowDialog();
        //    //    }
        //    //}
        //    //if (KeyData == (Keys.Alt | Keys.E))
        //    //{
        //    //    if (UserId != LoginUserId)
        //    //    {
        //    //        if (CommonMethod.ApplyRights((int)Menus.User) != 0)
        //    //        {
        //    //            frmExportBranchOfficeVouchers frmExportVouchers = new frmExportBranchOfficeVouchers();
        //    //            frmExportVouchers.ShowDialog();

        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        frmExportBranchOfficeVouchers frmExportVouchers = new frmExportBranchOfficeVouchers();
        //    //        frmExportVouchers.ShowDialog();

        //    //    }
        //    //}
        //    if (KeyData == (Keys.Alt | Keys.P))
        //    {
        //        if (UserId != LoginUserId)
        //        {
        //            if (CommonMethod.ApplyRights((int)Menus.User) != 0)
        //            {
        //                ResultArgs resultArgs = null;
        //                using (ExportVoucherSystem exportVoucher = new ExportVoucherSystem())
        //                {
        //                    resultArgs = exportVoucher.FetchMapLedger();
        //                }
        //                frmUnMappedLedgers unMappedLedgers = new frmUnMappedLedgers(resultArgs.DataSource.Table, false);
        //                unMappedLedgers.ShowDialog();
        //            }
        //        }
        //        else
        //        {
        //            ResultArgs resultArgs = null;
        //            using (ExportVoucherSystem exportVoucher = new ExportVoucherSystem())
        //            {
        //                resultArgs = exportVoucher.FetchMapLedger();
        //            }
        //            frmUnMappedLedgers unMappedLedgers = new frmUnMappedLedgers(resultArgs.DataSource.Table, false);
        //            unMappedLedgers.ShowDialog();
        //        }
        //    }
        //    return KeyData;
        //}

        #region Payroll Shortcuts
        public Keys KeysPayroll(Keys KeyData)
        {
            //Gateway
            if (KeyData == (Keys.Alt | Keys.T))
            {
                LoadCreatePayroll();
            }
            else if (KeyData == (Keys.Alt | Keys.O))
            {
                LoadOpenPayroll();
            }
            else if (KeyData == (Keys.Alt | Keys.D))
            {
                //DeletePayroll();
            }
            else if (KeyData == (Keys.Alt | Keys.K))
            {
                //LockUnlockPayroll();
            }
            //staff
            else if (KeyData == (Keys.Alt | Keys.F))
            {
                LoadStaffDetails();
            }
            else if (KeyData == (Keys.Alt | Keys.G))
            {
                LoadPayrollGroup();
            }
            else if (KeyData == (Keys.Alt | Keys.N))
            {
                Loadloan();
            }
            else if (KeyData == (Keys.Alt | Keys.L))
            {
                LoadPayrollComponent();
            }
            else if (KeyData == (Keys.Alt | Keys.J))
            {
                LoadMapStaffProject();
            }
            //Allotment
            else if (KeyData == (Keys.Alt | Keys.U))
            {
                LoadLoanManagement();
            }
            //else if (KeyData == (Keys.Alt | Keys.U))
            //{
            //    LoadGroupAllocation();
            //}
            else if (KeyData == (Keys.Alt | Keys.S))
            {
                LoadOrderComponents();
            }
            else if (KeyData == (Keys.Alt | Keys.I))
            {
                LoadMapComponentAllocation();
            }
            else if (KeyData == (Keys.Alt | Keys.Y))
            {
                LoadProcessPayroll();
            }
            else if (KeyData == (Keys.Alt | Keys.V))
            {
                LoadPayrollView();
            }
            else if (KeyData == (Keys.Alt | Keys.C))
            {
                LoadMapProcessTypeLedgers();
            }
            else if (KeyData == (Keys.Alt | Keys.P))
            {
                LoadPayrollProjects();
            }
            else if (KeyData == (Keys.Alt | Keys.H))
            {
                LoadPayrollPostpayment();
            }
            return KeyData;
        }
        #endregion

        #region Asset Shortcut Keys
        public Keys KeysAsset(Keys KeyData)
        {
            //Masters
            if (KeyData == (Keys.Alt | Keys.S))
            {
                LoadAssetGroup(FinanceModule.Asset);
            }
            else if (KeyData == (Keys.Alt | Keys.I))
            {
                LoadAssetItem();
            }
            else if (KeyData == (Keys.Alt | Keys.U))
            {
                LoadUnitOfMeasure(FinanceModule.Asset);
            }
            else if (KeyData == (Keys.Alt | Keys.L))
            {
                LoadAssetStockLocation(FinanceModule.Asset);
            }
            else if (KeyData == (Keys.Alt | Keys.N))
            {
                LoadInsuranceType();
            }
            //else if (KeyData == (Keys.F6))
            //{
            //    LoadAssetOpeningBalance();
            //}
            else if (KeyData == (Keys.Alt | Keys.T))
            {
                LoadCustodian(FinanceModule.Asset);
            }
            else if (KeyData == (Keys.Alt | Keys.B))
            {
                LoadBlockDetails(FinanceModule.Asset);
            }
            else if (KeyData == (Keys.Alt | Keys.F))
            {
                LoadVendorManufacture(VendorManufacture.Manufacture, FinanceModule.Asset);
            }
            else if (KeyData == (Keys.Alt | Keys.Control | Keys.E))
            {
                LoadMapAssetLedger();
            }
            //Vouchers
            else if (KeyData == (Keys.F6))
            {
                LoadAssetOpeningBalance();
            }
            else if (KeyData == (Keys.F7))
            {
                LoadPurchaseVoucher();
            }
            else if (KeyData == (Keys.F8))
            {
                LoadAssetInkindVoucher();
            }
            else if (KeyData == (Keys.F9))
            {
                LoadSalesVoucher();
            }
            else if (KeyData == (Keys.F10))
            {
                LoadInsuranceVoucher();
            }
            else if (KeyData == (Keys.F11))
            {
                LoadAMCVoucher();
            }
            else if (KeyData == (Keys.Alt | Keys.H))
            {
                LoadAssetSearch();
            }
            else if (KeyData == (Keys.Alt | Keys.Y))
            {
                LoadDepreciationVoucher();
            }
            else if (KeyData == (Keys.Alt | Keys.Control | Keys.U))
            {
                LoadUpdateAssetDetails();
            }
            else if (KeyData == (Keys.Alt | Keys.G))
            {
                LoadFixedAssetRegister();
            }
            else if (KeyData == (Keys.Alt | Keys.K))
            {
                LoadImportAssetDetails();
            }
            else if (KeyData == (Keys.Alt | Keys.M))
            {
                LoadMapLocations();
            }
            else if (KeyData == (Keys.Alt | Keys.F12))
            {
                LoadConfigure();
            }
            return KeyData;
        }
        #endregion

        #region Stock Shortcuts
        public Keys KeysStock(Keys KeyData)
        {
            //Stock Vouchers
            if (KeyData == (Keys.Alt | Keys.O))
            {
                LoadStockOpeningBalance();
            }
            else if (KeyData == (Keys.Alt | Keys.S))
            {
                LoadStockRegister();
            }
            else if (KeyData == (Keys.Alt | Keys.H))
            {
                LoadStockPurchase();
            }
            else if (KeyData == (Keys.Alt | Keys.L))
            {
                LoadStockSales();
            }
            else if (KeyData == (Keys.Alt | Keys.U))
            {
                LoadStockUtilised();
            }
            else if (KeyData == (Keys.Alt | Keys.I))
            {
                LoadStockDisposal();
            }
            else if (KeyData == (Keys.Alt | Keys.T))
            {
                LoadStockItemTrasfer();
            }
            else if (KeyData == (Keys.Alt | Keys.K))
            {
                LoadStockUpdation();
            }
            else if (KeyData == (Keys.Alt | Keys.N))
            {
                LoadPurchaseReturn();
            }


            return KeyData;
        }
        #endregion


        #region Finance Shortcuts

        public Keys KeysModuleFinanace(Keys KeyData)
        {
            KeyData = KeysFinanceMasters(KeyData);
            KeyData = KeysFinanceTransaction(KeyData);
            KeyData = KeysFinanceFixedDeposit(KeyData);
            KeyData = KeysFinanceOptions(KeyData);
            return KeyData;
        }

        public Keys KeysFinanceMasters(Keys KeyData)
        {
            // Load Project Category
            if (KeyData == (Keys.Alt | Keys.Y))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.ProjectCategory) != 0)
                    {
                        LoadProjectCategory();
                    }
                }
                else
                {
                    LoadProjectCategory();
                }
            }
            // Load Project 
            if (KeyData == (Keys.Alt | Keys.J))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.Project) != 0)
                    {
                        LoadProject();
                    }
                }
                else
                {
                    LoadProject();
                }
            }
            // Load Ledger Groups
            if (KeyData == (Keys.Alt | Keys.G))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.LedgerGroup) != 0)
                    {
                        LoadLedgerGroup();
                    }
                }
                else
                {
                    LoadLedgerGroup();
                }
            }
            // Load Ledger
            if (KeyData == (Keys.Alt | Keys.L))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.Ledger) != 0)
                    {
                        LoadLedger();
                    }
                }
                else
                {
                    LoadLedger();
                }
            }
            // Load Bank
            if (KeyData == (Keys.Alt | Keys.K))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.Bank) != 0)
                    {
                        LoadBank();
                    }
                }
                else
                {
                    LoadBank();
                }
            }
            // Load Bank Account
            if (KeyData == (Keys.Alt | Keys.B))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.BankAccounts) != 0)
                    {
                        LoadBankAccount();
                    }
                }
                else
                {
                    LoadBankAccount();
                }
            }
            // Load costcentre category
            if (KeyData == (Keys.Alt | Keys.Control | Keys.A))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.CostCentreCategory) != 0)
                    {
                        LoadCostCentreCategory();
                    }
                }
                else
                {
                    LoadCostCentreCategory();
                }
            }
            // Load costcentre
            if (KeyData == (Keys.Alt | Keys.Control | Keys.C))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.CostCentre) != 0)
                    {
                        LoadCostCentre();
                    }
                }
                else
                {
                    LoadCostCentre();
                }
            }
            // Country
            if (KeyData == (Keys.Alt | Keys.O))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.Country) != 0)
                    {
                        LoadCountry();
                    }
                }
                else
                {
                    LoadCountry();
                }
            }
            // Load State
            if (KeyData == (Keys.Alt | Keys.S))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.State) != 0)
                    {
                        LoadState();
                    }
                }
                else
                {
                    LoadState();
                }
            }
            // Load Lock Type
            if (KeyData == (Keys.Alt | Keys.Control | Keys.T))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.AuditLockTypes) != 0)
                    {
                        LoadAuditTypeView();
                    }
                }
                else
                {
                    LoadAuditTypeView();
                }
            }
            // Load Donor
            if (KeyData == (Keys.Alt | Keys.N))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.Donor) != 0)
                    {
                        LoadDonor();
                    }
                }
                else
                {
                    LoadDonor();
                }
            }
            // Load Purpose
            if (KeyData == (Keys.Alt | Keys.U))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.Purpose) != 0)
                    {
                        LoadPurpose();
                    }
                }
                else
                {
                    LoadPurpose();
                }
            }

            return KeyData;
        }

        public Keys KeysFinanceTransaction(Keys KeyData)
        {
            // Receipt
            if (KeyData == (Keys.F6))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.Receipt) != 0)
                    {
                        ShowTransactionViewForm((int)DefaultVoucherTypes.Receipt - 1, DefaultVoucherTypes.Receipt);
                    }
                }
                else
                {
                    ShowTransactionViewForm(((int)DefaultVoucherTypes.Receipt) - 1, DefaultVoucherTypes.Receipt);
                }
            }
            // Payments
            if (KeyData == (Keys.F7))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.Payments) != 0)
                    {
                        ShowTransactionViewForm(((int)DefaultVoucherTypes.Payment) - 1, DefaultVoucherTypes.Payment);
                    }
                }
                else
                {
                    ShowTransactionViewForm(((int)DefaultVoucherTypes.Payment) - 1, DefaultVoucherTypes.Payment);
                }
            }
            // Contra
            if (KeyData == (Keys.F8))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.Contra) != 0)
                    {
                        ShowTransactionViewForm(((int)DefaultVoucherTypes.Contra) - 1, DefaultVoucherTypes.Contra);
                    }
                }
                else
                {
                    ShowTransactionViewForm(((int)DefaultVoucherTypes.Contra) - 1, DefaultVoucherTypes.Contra);
                }
            }
            // Journal
            if (KeyData == (Keys.F9))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.Journal) != 0)
                    {
                        ShowTransactionViewForm(((int)DefaultVoucherTypes.Journal) - 1, DefaultVoucherTypes.Journal);
                    }
                }
                else
                {
                    ShowTransactionViewForm(((int)DefaultVoucherTypes.Journal) - 1, DefaultVoucherTypes.Journal);
                }
            }
            // BRS
            if (KeyData == (Keys.Alt | Keys.Control | Keys.B))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.BankReconciliation) != 0)
                    {
                        LoadBRS();
                    }
                }
                else
                {
                    LoadBRS();
                }
            }
            // Budget - Period
            if (KeyData == (Keys.Alt | Keys.Control | Keys.P))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.Budget) != 0)
                    {
                        LoadBudget();
                    }
                }
                else
                {
                    LoadBudget();
                }
            }
            // Budget - Annual
            if (KeyData == (Keys.Alt | Keys.Control | Keys.N))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.BudgetAnnual) != 0)
                    {
                        LoadBudget(BudgetType.BudgetByAnnualYear);
                    }
                }
                else
                {
                    LoadBudget(BudgetType.BudgetByAnnualYear);
                }
            }
            return KeyData;
        }

        public Keys KeysFinanceFixedDeposit(Keys KeyData)
        {
            // FD Ledgers
            if (KeyData == (Keys.Alt | Keys.Control | Keys.L))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.FixedDepositLedger) != 0)
                    {
                        LoadFDLedgers();
                    }
                }
                else
                {
                    LoadFDLedgers();
                }
            }
            // FD Opening
            if (KeyData == (Keys.Alt | Keys.Control | Keys.O))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.FixedDeposit) != 0)
                    {
                        LoadFDAccount();
                    }
                }
                else
                {
                    LoadFDAccount();
                }
            }
            // FD Investment
            if (KeyData == (Keys.Alt | Keys.I))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.FixedInvestment) != 0)
                    {
                        LoadFDInvestment();
                    }
                }
                else
                {
                    LoadFDInvestment();
                }
            }
            // FD Renewal
            if (KeyData == (Keys.Alt | Keys.Control | Keys.E))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.FixedDepositRenewal) != 0)
                    {
                        LoadFdRenewal();
                    }
                }
                else
                {
                    LoadFdRenewal();
                }
            }
            // FD Withdrawal
            if (KeyData == (Keys.Alt | Keys.W))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.FDWithdrawal) != 0)
                    {
                        LoadFDWithdraw();
                    }
                }
                else
                {
                    LoadFDWithdraw();
                }
            }
            // FD Register
            if (KeyData == (Keys.Alt | Keys.Control | Keys.G))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.FixedDepositRegister) != 0)
                    {
                        LoadFdRegistersView();
                    }
                }
                else
                {
                    LoadFdRegistersView();
                }
            }
            // FD Post Interest
            if (KeyData == (Keys.Alt | Keys.Control | Keys.I))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.FixedInvestment) != 0)
                    {
                        LoadFdPostInterest();
                    }
                }
                else
                {
                    LoadFdPostInterest();
                }
            }
            return KeyData;
        }

        public Keys KeysFinanceOptions(Keys KeyData)
        {
            // Load Balance Refresh
            if (KeyData == (Keys.Alt | Keys.F))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.RefreshBalance) != 0)
                    {
                        LoadRefreshBalance();
                    }
                }
                else
                {
                    LoadRefreshBalance();
                }
            }
            // Load Voucher Number
            if (KeyData == (Keys.Alt | Keys.D))
            {
                //if (UserId != LoginUserId)
                //{
                //    if (CommonMethod.ApplyRights((int)Menus.VoucherNumberDefinition) != 0)
                //    {
                //        LoadVoucher();
                //    }
                //}
                //else
                //{
                //    LoadVoucher();
                //}
            }
            // Load Regenerate Voucher
            if (KeyData == (Keys.Alt | Keys.T))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.RegenarateVoucher) != 0)
                    {
                        LoadRegenerateVoucher();
                    }
                }
                else
                {
                    LoadRegenerateVoucher();
                }
            }
            // Load Map Accounts
            if (KeyData == (Keys.Alt | Keys.Control | Keys.S))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.AccountMapping) != 0)
                    {
                        if (!string.IsNullOrEmpty(TransactionPeriod))
                        {
                            frmMapProjectLedger accountmapp = new frmMapProjectLedger();
                            accountmapp.ShowDialog();
                        }
                        else
                        {
                            //if (XtraMessageBox.Show("Transaction Period is not created . Do you want to create the Transaction Period?", "Acme.erp", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            if (XtraMessageBox.Show(this.GetMessage(MessageCatalog.Common.COMMON_TRANS_PEROID_CREATE_INFO), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                LoadAccountPeriod();
                            }
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(TransactionPeriod))
                    {
                        frmMapProjectLedger accountmapp = new frmMapProjectLedger();
                        accountmapp.ShowDialog();
                    }
                    else
                    {
                        //if (XtraMessageBox.Show("Transaction Period is not created . Do you want to create the Transaction Period??", "Acme.erp", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        if (XtraMessageBox.Show(this.GetMessage(MessageCatalog.Common.COMMON_TRANS_PEROID_CREATE_INFO), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            LoadAccountPeriod();
                        }
                    }
                }
            }
            // Merge Ledgers
            if (KeyData == (Keys.Alt | Keys.Control | Keys.R))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.User) != 0)
                    {
                        bool hasHome = HasFormInMDI(typeof(frmMergeAllLedgers).Name);
                        CloseFormInMDI(typeof(frmMergeAllLedgers).Name);
                        frmMergeAllLedgers MergeLedgers = new frmMergeAllLedgers();
                        MergeLedgers.ShowDialog();
                    }
                }
                else
                {
                    bool hasHome = HasFormInMDI(typeof(frmMergeAllLedgers).Name);
                    CloseFormInMDI(typeof(frmMergeAllLedgers).Name);
                    frmMergeAllLedgers MergeLedgers = new frmMergeAllLedgers();
                    MergeLedgers.ShowDialog();
                }
            }
            // Load Lock Voucher
            if (KeyData == (Keys.Alt | Keys.V))
            {
                bool hasfrmHome = HasFormInMDI(typeof(frmTransactionView).Name);
                CloseFormInMDI(typeof(frmTransactionView).Name);
                frmTransactionView frmTransaction = new frmTransactionView(RecentVoucherDate, RecentProjectId, RecentProject, (int)DefaultVoucherTypes.Receipt - 1, 1);//0-Add Mode,1-View Mode 
                frmTransaction.MdiParent = this;
                frmTransaction.Show();

                /*if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.AuditLockTrans) != 0)
                    {
                        LoadAuditLockTransactions();
                    }
                }
                else
                {
                    LoadAuditLockTransactions();
                }*/
            }
            // Load Ledger Options
            if (KeyData == (Keys.Alt | Keys.Control | Keys.D))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.LedgerOptions) != 0)
                    {
                        LoadLedgerOptions();
                    }
                }
                else
                {
                    LoadLedgerOptions();
                }
            }

            // Load Finnance Settings

            if (KeyData == (Keys.Alt | Keys.F12))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.FinanceSettings) != 0)
                    {
                        LoadFinnanceSettings();
                    }
                }
                else
                {
                    LoadFinnanceSettings();
                }
                ShowProjectWindow();
            }
            return KeyData;
        }
        #endregion

        #region TDS Shortcuts
        public Keys KeysModuleTDS(Keys KeyData)
        {
            KeyData = KeysTDSMasters(KeyData);
            KeyData = KeysTDSTransactions(KeyData);
            KeyData = KeysTDSOptions(KeyData);
            return KeyData;
        }

        public Keys KeysTDSMasters(Keys KeyData)
        {
            if (KeyData == (Keys.Alt | Keys.L))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.LegalEntity) != 0)
                    {
                        LoadLegalEntity();
                    }
                }
                else
                {
                    LoadLegalEntity();
                }
            }
            if (KeyData == (Keys.Alt | Keys.B))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.GoverningMembers) != 0)
                    {
                        LoadGoverningMember();
                    }
                }
                else
                {
                    LoadGoverningMember();
                }
            }
            if (KeyData == (Keys.Alt | Keys.U))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.AuditInfo) != 0)
                    {
                        LoadAuditInfo();
                    }
                }
                else
                {
                    LoadAuditInfo();
                }
            }
            if (KeyData == (Keys.Alt | Keys.Control | Keys.D))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.AuditType) != 0)
                    {
                        LoadAuditType();
                    }
                }
                else
                {
                    LoadAuditType();
                }
            }
            if (KeyData == (Keys.Alt | Keys.I))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.Auditor) != 0)
                    {
                        LoadAuditor();
                    }
                }
                else
                {
                    LoadAuditor();
                }
            }
            if (KeyData == (Keys.Alt | Keys.F))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.TDSCompanyInfo) != 0)
                    {
                        LoadTDSCompanyInfo();
                    }
                }
                else
                {
                    LoadTDSCompanyInfo();
                }
            }

            if (KeyData == (Keys.Alt | Keys.S))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.TDSSection) != 0)
                    {
                        LoadTDSSection();
                    }
                }
                else
                {
                    LoadTDSSection();
                }
            }
            if (KeyData == (Keys.Alt | Keys.N))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.TDSNatureofPayments) != 0)
                    {
                        LoadTDSNatureofPyments();
                    }
                }
                else
                {
                    LoadTDSNatureofPyments();

                }
            }
            if (KeyData == (Keys.Alt | Keys.Y))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.TDSDeducteeType) != 0)
                    {
                        LoadTDSDeducteeTypes();
                    }
                }
                else
                {
                    LoadTDSDeducteeTypes();
                }
            }

            if (KeyData == (Keys.Alt | Keys.X))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.TDSPolicy) != 0)
                    {
                        LoadTaxPolicy();
                    }
                }
                else
                {
                    LoadTaxPolicy();
                }
            }

            if (KeyData == (Keys.Alt | Keys.G))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.TDSLedger) != 0)
                    {
                        LoadTDSLedgers();
                    }
                }
                else
                {
                    LoadTDSLedgers();
                }
            }
            if (KeyData == (Keys.Alt | Keys.T))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.TDSDutyTax) != 0)
                    {
                        LoadDutyTax();
                    }
                }
                else
                {
                    LoadDutyTax();
                }
            }
            return KeyData;
        }
        public Keys KeysTDSTransactions(Keys KeyData)
        {
            if (KeyData == (Keys.Alt | Keys.O))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.TDSDeduction) != 0)
                    {
                        LoadTDSDeductions();
                    }
                }
                else
                {
                    LoadTDSDeductions();
                }
            }
            return KeyData;
        }

        // TDS Setting
        public Keys KeysTDSOptions(Keys KeyData)
        {
            if (KeyData == (Keys.Alt | Keys.F5))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.TDSSetting) != 0)
                    {
                        LoadTDSSettings();
                    }
                }
                else
                {
                    LoadTDSSettings();
                }
            }
            return KeyData;
        }

        #endregion

        #region Utilities Shortcuts
        public Keys KeysModuleUtilities(Keys KeyData)
        {
            if (bbiDataUtlity.Visibility == BarItemVisibility.Always)
            {
                KeyData = KeysUtilityConfiguration(KeyData);
                KeyData = KeysUtilityUsers(KeyData);
                KeyData = KeysUtilityDatamanagement(KeyData);
                KeyData = KeysUtilityHOInterface(KeyData);
            }
            return KeyData;
        }

        public Keys KeysUtilityConfiguration(Keys KeyData)
        {
            if (KeyData == (Keys.Alt | Keys.F))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    LoadInstitutepreferenceDetails();
                }
                else
                {
                    LoadInstitutepreferenceDetails();
                }
            }
            //if (KeyData == (Keys.F12))
            //{
            //    if (UserId != LoginUserId)
            //    {
            //        if (CommonMethod.ApplyUserRights((int)Menus.MasterSetting) != 0)
            //        {
            //            frmSettings frmsetting = new frmSettings();
            //            frmsetting.ShowDialog();
            //        }
            //    }
            //    else
            //    {
            //        frmSettings frmsetting = new frmSettings();
            //        frmsetting.ShowDialog();
            //    }
            //}
            if (KeyData == (Keys.Alt | Keys.T))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.TransactionPeriod) != 0)
                    {
                        LoadAccountPeriod();
                    }
                }
                else
                {
                    LoadAccountPeriod();
                }
            }
            return KeyData;
        }

        public Keys KeysUtilityUsers(Keys KeyData)
        {
            if (KeyData == (Keys.Alt | Keys.S))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.UserRole) != 0)
                    {
                        LoadRole();
                    }
                }
                else
                {
                    LoadRole();
                }
            }
            if (KeyData == (Keys.Alt | Keys.Y))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.ManageSecurity) != 0)
                    {
                        LoadUserSecurity();
                    }
                }
                else
                {
                    LoadUserSecurity();
                }
            }
            if (KeyData == (Keys.Alt | Keys.H))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.UserRightsManagement) != 0)
                    {
                        LoadRights();
                    }
                }
                else
                {
                    LoadRights();
                }
            }
            if (KeyData == (Keys.Alt | Keys.U))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.User) != 0)
                    {
                        LoadUserView();
                    }
                }
                else
                {
                    LoadUserView();
                }
            }
            return KeyData;
        }

        public Keys KeysUtilityDatamanagement(Keys KeyData)
        {
            if (bbiDataUtlity.Visibility == BarItemVisibility.Always)
            {
                if (KeyData == (Keys.Alt | Keys.O))
                {
                    if (!LoginUser.IsFullRightsReservedUser)
                    {
                        if (CommonMethod.ApplyRights((int)Menus.ImportData) != 0)
                        {
                            LoadImportData();
                        }
                    }
                    else
                    {
                        LoadImportData();
                    }
                }
                if (KeyData == (Keys.Alt | Keys.X))
                {
                    if (!LoginUser.IsFullRightsReservedUser)
                    {
                        if (CommonMethod.ApplyRights((int)Menus.ExportData) != 0)
                        {
                            LoadExportData();
                        }
                    }
                    else
                    {
                        LoadExportData();
                    }
                }
                if (KeyData == (Keys.Alt | Keys.G))
                {
                    if (!LoginUser.IsFullRightsReservedUser)
                    {
                        if (CommonMethod.ApplyRights((int)Menus.DataMigration) != 0)
                        {
                            LoadDataMigration();
                        }
                    }
                    else
                    {
                        LoadDataMigration();
                    }
                }
                if (KeyData == (Keys.Alt | Keys.Control | Keys.D))
                {
                    if (!LoginUser.IsFullRightsReservedUser)
                    {
                        if (CommonMethod.ApplyRights((int)Menus.Restore) != 0)
                        {
                            LoadRestoreDatabase();
                        }
                    }
                    else
                    {
                        LoadRestoreDatabase();
                    }
                }
                if (KeyData == (Keys.Alt | Keys.L))
                {
                    if (!LoginUser.IsFullRightsReservedUser)
                    {
                        if (CommonMethod.ApplyRights((int)Menus.ManageMultiBranch) != 0)
                        {
                            LoadMultiDB();
                        }
                    }
                    else
                    {
                        LoadMultiDB();
                    }
                }
                if (KeyData == (Keys.Alt | Keys.K))
                {
                    if (!LoginUser.IsFullRightsReservedUser)
                    {
                        if (CommonMethod.ApplyRights((int)Menus.Backup) != 0)
                        {
                            LoadDBBackup();
                        }
                    }
                    else
                    {
                        LoadDBBackup();
                    }
                }
            }
            return KeyData;
        }

        public Keys KeysUtilityHOInterface(Keys KeyData)
        {
            if (KeyData == (Keys.Alt | Keys.Control | Keys.L))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.LicenseKey) != 0)
                    {
                        LoadHOLicenseKey();
                    }
                }
                else
                {
                    LoadHOLicenseKey();
                }
            }
            if (KeyData == (Keys.Alt | Keys.I))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.ShowImportMasters) != 0)
                    {
                        LoadHOImportMasters();
                    }
                }
                else
                {
                    LoadHOImportMasters();
                }
            }

            if (KeyData == (Keys.Alt | Keys.M))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.ShowMapLedgers) != 0)
                    {
                        LoadHOMapLedgers();
                    }
                }
                else
                {
                    LoadHOMapLedgers();
                }
            }
            if (KeyData == (Keys.Alt | Keys.B))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyRights((int)Menus.UploadDatabase) != 0)
                    {
                        LoadHOUploadDatabase();
                    }
                }
                else
                {
                    LoadHOUploadDatabase();
                }
            }
            if (KeyData == (Keys.Alt | Keys.M))
            {
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    //if (CommonMethod.ApplyRights((int)Menus.UploadDatabase) != 0)
                    //{
                    LoadHOUploadDatabase();
                    //}
                }
                else
                {
                    LoadHOUploadDatabase();
                }
            }

            return KeyData;
        }


        #endregion

        //void InitSkinGallery()
        //{
        //    ribbonMain.ForceInitialize();
        //    GalleryDropDown skins = new GalleryDropDown();
        //    skins.Ribbon = ribbonMain;
        //    DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGalleryDropDown(skins);
        //    DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(rgbiSkin, true);
        //}

        //private void LoadHome()
        //{
        //    bool hasHome = HasFormInMDI(typeof(frmHome).Name);
        //    ShowWaitDialog();

        //    if (!hasHome)
        //    {
        //        frmHome frmhome = new frmHome();
        //        frmhome.MdiParent = this;
        //        frmhome.Show();
        //        // captionPanel.Text = frmhome.Text;
        //    }

        //    ShowHideLeftMenuBar = false;
        //    ShowHideDockPanel = DockVisibility.Hidden;
        //    CloseWaitDialog();
        //}

        private void LoadLogin()
        {
            // bool hasHome = HasFormInMDI(typeof(frmHome).Name);
            // ShowWaitDialog();
            this.Close();
            //if (!hasHome)
            //{
            //frmHome frmhome = new frmHome();
            //frmhome.MdiParent = this;
            // frmhome.Show();
            frmAcMELogin login = new frmAcMELogin();
            login.ShowDialog();
            // captionPanel.Text = frmhome.Text;
            //}

            // ShowHideLeftMenuBar = false;
            // ShowHideDockPanel = DockVisibility.Hidden;
            // CloseWaitDialog();
        }

        private void LoadLoginHome()
        {
            bool hasHome = HasFormInMDI(typeof(frmLoginDashboard).Name);
            //ShowWaitDialog();
            ShowLeftMenuBar(true);
            if (!hasHome)
            {
                frmLoginDashboard frmLoginhome = new frmLoginDashboard();
                frmLoginhome.MdiParent = this;
                //frmLoginhome.LoadDashBoardDefaults();
                if (!SetCloseAllTabsVisibility)
                    bbiCloseAll.Enabled = false;
                frmLoginhome.Show();
                captionPanel.Text = frmLoginhome.Text;
            }
            //CloseWaitDialog();
        }

        private void LoadProject()
        {
            if (!string.IsNullOrEmpty(TransactionPeriod))
            {
                bool hasHome = HasFormInMDI(typeof(frmProjectView).Name);
                ShowWaitDialog();
                if (!hasHome)
                {
                    frmProjectView frmProject = new frmProjectView();
                    frmProject.MdiParent = this;
                    frmProject.Show();
                }
                CloseWaitDialog();
            }
            else
            {
                //if (XtraMessageBox.Show("Transaction Period is not created . Do you want to create the Transaction Period?", "Acme.erp", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                if (XtraMessageBox.Show(this.GetMessage(MessageCatalog.Common.COMMON_TRANS_PEROID_CREATE_INFO), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    LoadAccountPeriod();
                }
            }

        }

        private void CloseAll(bool logout)
        {
            //  if (logout) visiblePageHeaderLinks(BarItemVisibility.Never);
            if (logout) visiblePageHeaders(false);
            foreach (Form frmActive in this.MdiChildren)
            {
                frmActive.Close();
            }

            if (logout) { LoadLogin(); }
        }

        /// <summary>
        /// This method is used to close all payroll open forms
        /// </summary>
        /// <param name="logout"></param>
        private void CloseAllPayrollForms()
        {
            foreach (Form frmActive in this.MdiChildren)
            {
                if (frmActive.GetType().BaseType.Name == typeof(PAYROLL.frmPayrollBase).Name)
                {
                    frmActive.Close();
                }
            }
        }

        private void CloseReportAll()
        {
            //  if (logout) visiblePageHeaderLinks(BarItemVisibility.Never);
            foreach (Form frmActive in this.MdiChildren)
            {
                if (frmActive.Text == "Report")
                    frmActive.Close();
            }

        }

        private bool HasFormInMDI(string formName)
        {
            bool hasForm = false;

            foreach (Form frmActive in this.MdiChildren)
            {
                hasForm = (frmActive.Name.ToLower() == formName.ToLower());

                if (hasForm)
                {
                    frmActive.Select();
                    break;
                }

                //frmActive.Select();
            }

            return hasForm;
        }

        //private void LoadUsers()
        //{
        //    if (UserId != LoginUserId)
        //    {
        //        MenuCaption = nbgUserManagement.Caption;
        //        ActiveNavGroupId = bbiUsers.Id;
        //        ApplyUserRights();
        //    }
        //    else
        //    {
        //        CollapseNavBar(nbgUserManagement.Caption, bbiUsers.Id);
        //    }
        //}

        private void CloseFormInMDI(string formName)
        {
            bool hasForm = false;
            foreach (Form frmActive in this.MdiChildren)
            {
                hasForm = (frmActive.Name.ToLower() == formName.ToLower());

                if (hasForm)
                {
                    frmActive.Close();
                    break;
                }

                //  frmActive.Select();
            }

            //return hasForm;
        }

        private void LoadReport()
        {
            // ShowWaitDialog();
            CloseReportAll();
            ShowLeftMenuBar(false);
            Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry(this);
            report.ShowReport();
            // CloseWaitDialog();

        }

        private void ShowLeftMenuBar(bool show)
        {
            if (show)
            {
                if (dpAcme.Visibility == DockVisibility.AutoHide)
                {
                    dpAcme.Visibility = DockVisibility.Visible;
                    //CloseFormInMDI("frmReportGallery");
                }
            }
            else
            {
                if (dpAcme.Visibility != DockVisibility.AutoHide)
                {
                    dpAcme.Visibility = DockVisibility.AutoHide;
                }
            }
        }

        private void LoadFinance()
        {
            nbgTransaction.Visible = nbgTransaction.Expanded = true;
            nbgFixedDeposit.Visible = true;
            //nbgFixedDeposit.Visible = (this.AppSetting.AllowMultiCurrency == 1 ? false : true);
            nbiMaster.Expanded = nbgAudit.Expanded = nbgFixedDeposit.Expanded = nbiVoucherMasters.Expanded = nbiViews.Expanded = false;
            nbiVoucherMasters.Visible = nbiViews.Visible = nbiMaster.Visible = nbiHeadOfficeInterface.Visible = nbgDataUtility.Visible = false;
            nbgTDSVouchers.Visible = nbgTDSVouchers.Expanded =
            nbgMaster.Visible = false;
            nbgPayroll.Visible = nbgInventory.Visible = nbgReports.Visible = nbgUserManagement.Visible =
            nbgReports.Visible = nbgTDSMasters.Visible = false;
            nbgAssetTransactions.Visible = nbgAssetMasters.Visible = nbgAssetViews.Visible = nbgAssetOptions.Visible = false;
            nbgStockMasters.Visible = nbgStockVouchers.Visible = false;
            //nbgPayroll.Visible = nbgGateway.Visible = nbgDefinition.Visible = nbg1Allotement.Visible = nbgStaff.Visible = nbg1Reports.Visible = false;
            nbgPayroll.Visible = nbgGateway.Visible = nbgDefinition.Visible = nbg1Allotement.Visible = nbg1Reports.Visible = false;
            nbgAssetMasters.Visible = nbgAssetTransactions.Visible = nbgStockMasters.Visible = nbgStockVouchers.Visible = false;

            nbiVoucherMasters.Visible = nbiMaster.Visible = nbiViews.Visible = true;
            nbgDonorMasters.Visible = nbgDonorContactDesk.Visible = nbgDonorMailDesk.Visible = false;
        }

        private void LoadAsset()
        {
            nbgAssetMasters.Visible = nbgAssetMasters.Expanded = true;
            nbgAssetTransactions.Visible = nbgAssetTransactions.Expanded = true;
            nbgAssetViews.Visible = nbgAssetOptions.Visible = true;
            nbgTransaction.Visible = nbgTransaction.Expanded = false;
            nbgFixedDeposit.Visible = nbgFixedDeposit.Expanded = false;
            nbiHeadOfficeInterface.Expanded = nbiMaster.Expanded = nbgAudit.Expanded = nbiVoucherMasters.Expanded = nbiViews.Expanded = false;
            nbiVoucherMasters.Visible = nbiViews.Visible = nbgAudit.Visible = nbiMaster.Visible = nbiHeadOfficeInterface.Visible = nbgDataUtility.Visible = false;
            nbgTDSVouchers.Visible = nbgTDSVouchers.Expanded = false;
            nbgMaster.Visible = false;
            nbgPayroll.Visible = nbgInventory.Visible = nbgReports.Visible = nbgUserManagement.Visible =
            nbgReports.Visible = nbgTDSMasters.Visible = false;
            nbgStockMasters.Visible = nbgStockVouchers.Visible = false;
            nbgAssetMasters.Expanded = false;
            //nbgPayroll.Visible = nbgGateway.Visible = nbgDefinition.Visible = nbg1Allotement.Visible = nbgStaff.Visible = nbg1Reports.Visible = false;
            nbgPayroll.Visible = nbgGateway.Visible = nbgDefinition.Visible = nbg1Allotement.Visible = nbg1Reports.Visible = false;
            nbgDonorMasters.Visible = nbgDonorContactDesk.Visible = nbgDonorMailDesk.Visible = false;
        }

        private void LoadStock()
        {
            nbgStockMasters.Expanded = nbgStockMasters.Visible = nbgStockVouchers.Visible = nbgStockVouchers.Expanded = true;
            nbgAssetMasters.Visible = nbgAssetMasters.Expanded = false;
            nbgTransaction.Visible = nbgTransaction.Expanded = false;
            nbgAssetViews.Visible = nbgAssetOptions.Visible = false;
            nbgAssetTransactions.Visible = nbgAssetTransactions.Expanded = false;
            nbgFixedDeposit.Visible = nbgFixedDeposit.Expanded = false;
            nbiHeadOfficeInterface.Expanded = nbiMaster.Expanded = nbgAudit.Expanded = nbiViews.Expanded = nbiVoucherMasters.Expanded = false;
            nbiVoucherMasters.Visible = nbiMaster.Visible = nbiViews.Visible = nbgAudit.Visible = nbiHeadOfficeInterface.Visible = nbgDataUtility.Visible = false;
            nbgTDSVouchers.Visible = nbgTDSVouchers.Expanded = false;
            nbgMaster.Visible = false;
            nbgPayroll.Visible = nbgInventory.Visible = nbgReports.Visible = nbgUserManagement.Visible =
            nbgReports.Visible = nbgTDSMasters.Visible = false;
            nbgStockMasters.Expanded = false;
            nbgPayroll.Visible = nbgGateway.Visible = nbgDefinition.Visible = nbg1Allotement.Visible = nbg1Reports.Visible = false;
            nbgDonorMasters.Visible = nbgDonorContactDesk.Visible = nbgDonorMailDesk.Visible = false;
        }

        private void LoadTDS()
        {
            nbgTransaction.Visible = nbgTransaction.Expanded = nbgAssetMasters.Visible = nbgAssetTransactions.Visible = false;
            nbgAssetViews.Visible = nbgAssetOptions.Visible = false;
            nbgMaster.Visible = nbgTransaction.Visible = nbgFixedDeposit.Visible = false;
            nbgMaster.Expanded = nbgTransaction.Expanded = nbgFixedDeposit.Expanded = false;
            nbiHeadOfficeInterface.Expanded = nbiMaster.Expanded = nbgAudit.Expanded = nbiViews.Expanded = nbiVoucherMasters.Expanded = false;
            nbiVoucherMasters.Visible = nbiMaster.Visible = nbiViews.Visible = nbgAudit.Visible = nbiHeadOfficeInterface.Visible = nbgDataUtility.Visible = false;
            nbgPayroll.Visible = nbgInventory.Visible = nbgReports.Visible = nbgUserManagement.Visible =
            nbgReports.Visible = nbgTDSMasters.Visible = false;
            nbgTDSMasters.Visible = nbgTDSMasters.Expanded = nbgTDSVouchers.Visible = nbgTDSVouchers.Expanded = true;
            nbgStockMasters.Visible = nbgStockVouchers.Visible = false;
            //nbgPayroll.Visible = nbgGateway.Visible = nbgDefinition.Visible = nbg1Allotement.Visible = nbgStaff.Visible = nbg1Reports.Visible = false;
            nbgPayroll.Visible = nbgGateway.Visible = nbgDefinition.Visible = nbg1Allotement.Visible = nbg1Reports.Visible = false;
            nbgDonorMasters.Visible = nbgDonorContactDesk.Visible = nbgDonorMailDesk.Visible = false;
        }

        private void LoadDataUtility()
        {
            nbgDataUtility.Visible = nbgMaster.Expanded = nbgDataUtility.Expanded = true;
            nbiHeadOfficeInterface.Visible = nbgUserManagement.Visible = nbgMaster.Visible = true;
            nbiMaster.Visible = nbiViews.Visible = nbgAudit.Visible = nbiVoucherMasters.Visible = nbgTransaction.Visible = nbgFixedDeposit.Visible = false;
            nbgStockVouchers.Visible = nbgStockMasters.Visible = nbgAssetMasters.Visible = nbgAssetTransactions.Visible = false;
            nbgAssetViews.Visible = nbgAssetOptions.Visible = false;
            nbgTDSVouchers.Visible = nbgTDSVouchers.Expanded = false;
            nbgPayroll.Visible = nbgGateway.Visible = nbgDefinition.Visible = nbg1Allotement.Visible = nbg1Reports.Visible = false;
            nbgPayroll.Visible = nbgGateway.Visible = nbgDefinition.Visible = nbiMaster.Visible = nbiViews.Visible = nbgTDSMasters.Visible = nbgTDSVouchers.Visible = nbg1Allotement.Visible = nbg1Reports.Visible = false;
            nbgDonorMasters.Visible = nbgDonorContactDesk.Visible = nbgDonorMailDesk.Visible = false;

            nbgUserManagement.Visible = !(this.LoginUser.IsLoginUserAuditor); //Dont show user management for auditor user

            nbgUserManagement.Visible = SettingProperty.EnablePayrollOnly ? false : true;

            //On 17/09/2024, To lock datamanagement features for few license keys -----------------------------------------------------------
            nbgDataUtility.Visible = !(this.AppSetting.IsLockedDataManagementFeatures);

        }

        private void LoadNetworking()
        {
            nbgDonorMasters.Visible = nbgDonorContactDesk.Visible = nbgDonorMailDesk.Visible = true;
            nbgDonorContactDesk.Expanded = nbgDonorMasters.Expanded = false;
            nbgDonorMailDesk.Expanded = true;
            nbgDataUtility.Visible = nbgMaster.Expanded = nbgDataUtility.Expanded = false;
            nbiMaster.Visible = nbiViews.Visible = nbiVoucherMasters.Visible = nbgTransaction.Visible = nbgFixedDeposit.Visible = false;
            nbgStockVouchers.Visible = nbgStockMasters.Visible = nbgAssetMasters.Visible = nbgAssetTransactions.Visible = false;
            nbgAssetViews.Visible = nbgAssetOptions.Visible = false;
            nbgTDSVouchers.Visible = nbgTDSVouchers.Expanded = false;
            nbgPayroll.Visible = nbgGateway.Visible = nbgDefinition.Visible = nbg1Allotement.Visible = nbg1Reports.Visible = false;
            nbiHeadOfficeInterface.Visible = nbgUserManagement.Visible = nbgMaster.Visible = false;
            nbgPayroll.Visible = nbgGateway.Visible = nbgDefinition.Visible = nbiMaster.Visible = nbiViews.Visible = nbgAudit.Visible = nbgTDSMasters.Visible = nbgTDSVouchers.Visible = nbg1Allotement.Visible = nbg1Reports.Visible = false;
        }

        private void LoadSettings()
        {
            nbiVoucherMasters.Visible = nbiVoucherMasters.Expanded = nbiViews.Visible = nbiViews.Expanded = true;  // Voucher Master
            nbiMaster.Visible = nbiViews.Visible = true;  //Legal Entity
            nbgMaster.Visible = true;      //Setting
            nbgTDSVouchers.Visible = false;
            nbgPayroll.Visible = nbgDataUtility.Visible =
            nbgFixedDeposit.Visible = nbgInventory.Visible = nbgReports.Visible = nbgTransaction.Visible =
            nbgUserManagement.Visible = nbgVehicle.Visible = nbgReports.Visible = nbgTDSMasters.Visible = false;
            if (this.UtilityMember.NumberSet.ToInteger(this.LoginUser.TDSEnabled.ToString()).Equals((int)SetDefaultValue.DefaultValue))
            {
                nbgTDSMasters.Visible = true;
                nbgTDSMasters.Expanded = false;
            }
            nbgAssetMasters.Visible = nbgAssetMasters.Expanded = false;
            nbgStockMasters.Visible = nbgStockVouchers.Visible = false;
            nbgAssetTransactions.Visible = nbgAssetTransactions.Expanded = false;
            nbgAssetViews.Visible = nbgAssetOptions.Visible = false;
            //nbgPayroll.Visible = nbgGateway.Visible = nbgDefinition.Visible = nbg1Allotement.Visible = nbgStaff.Visible = nbg1Reports.Visible = false;
            nbgPayroll.Visible = nbgGateway.Visible = nbgDefinition.Visible = nbg1Allotement.Visible = nbg1Reports.Visible = false;
            nbgDonorMasters.Visible = nbgDonorContactDesk.Visible = nbgDonorMailDesk.Visible = false;
        }

        private void LoadPayroll()
        {
            groupNavName = nbgPayroll.Caption;
            ActiveNavGroupId = bbiPayroll.Id;
            //dpAcme.Text = "Payroll";
            dpAcme.Text = this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE);
            nbgPayroll.Visible = false;
            nbgFixedDeposit.Visible = nbgDataUtility.Visible = nbgInventory.Visible = nbgMaster.Visible = nbgReports.Visible =
            nbiVoucherMasters.Visible = nbiMaster.Visible = nbiViews.Visible = nbgAudit.Visible = nbgTDSMasters.Visible = nbgTDSVouchers.Visible = nbgTransaction.Visible = nbgUserManagement.Visible = nbgVehicle.Visible = false;
            nbgDonorMasters.Visible = nbgDonorContactDesk.Visible = nbgDonorMailDesk.Visible = nbiHeadOfficeInterface.Visible = nbgStockMasters.Visible = nbgStockVouchers.Visible = nbgAssetMasters.Visible = nbgAssetTransactions.Visible = nbgAssetOptions.Visible = nbgAssetViews.Visible = false; // To hide stock and asset module

            if (!PayrollSubMenu.IsPayrollExists())
            {
                //DialogResult dr = XtraMessageBox.Show("Payroll does not exists.Do you want to create?", "Acme.erp", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                DialogResult dr = XtraMessageBox.Show(this.GetMessage(MessageCatalog.Payroll.ACCPMain.PAYROLL_CREATE_INFO), this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    PAYROLL.Modules.Payroll_app.frmCreatePayroll createpayroll = new PAYROLL.Modules.Payroll_app.frmCreatePayroll();
                    createpayroll.ShowDialog();
                }
                else
                {
                    SetPayRollMenu();
                }
            }
            else
            {
                if (PayrollSubMenu.PAYROLL_Id == 0)
                {
                    PAYROLL.Modules.Payroll_app.frmOpenPayroll openpay = new PAYROLL.Modules.Payroll_app.frmOpenPayroll();
                    openpay.ShowDialog();
                }
            }
            PayrollSubMenu.SetRecentPayRoll();
            PayrollSubMenu.SetValues();
            // label to frmmain text
            //this.Text = (!string.IsNullOrEmpty(PayrollSubMenu.PAYROLL_MONTH)) ? "Payroll for " + PayrollSubMenu.PAYROLL_MONTH : "No Payroll exists";
            //this.Text = (!string.IsNullOrEmpty(PayrollSubMenu.PAYROLL_MONTH)) ?  + " " +  PayrollSubMenu.PAYROLL_MONTH : this.GetMessage(MessageCatalog.Payroll.ACCPMain.PAYROLL_NOPAYROLL_EXISTS_INFO);
            this.Text = (!string.IsNullOrEmpty(PayrollSubMenu.PAYROLL_MONTH)) ? this.GetMessage(MessageCatalog.Payroll.ACCPMain.PAYROLL_MONTHFOR_INFO) + " " + PayrollSubMenu.PAYROLL_MONTH : this.GetMessage(MessageCatalog.Payroll.ACCPMain.PAYROLL_NOPAYROLL_EXISTS_INFO);
            glkTransPeriod.Visible = false;
            SetPayRollMenu();

            if (PayrollSubMenu.PAYROLL_Id > 0)
            {
                dpAcme.Text = "Payroll -" + PayrollSubMenu.PAYROLL_MONTH;
            }
        }

        private void CollapseNavBar(string NavigationBarGroupName, int ActiveNavGrpId)
        {
            glkTransPeriod.Enabled = true;
            glkTransPeriod.Visible = true;
            SetTransPeriod();
            groupNavName = NavigationBarGroupName;
            ActiveNavGroupId = ActiveNavGrpId;
            ShowWaitDialog();
            if (ActiveNavGroupId != (int)Moudule.Reports) { ShowLeftMenuBar(true); }

            for (int i = 0; i < leftMenuBar.Groups.Count; i++)
            {
                if (ActiveNavGroupId == (int)Moudule.Settings)
                {
                    LoadSettings();
                    dpAcme.Text = Moudule.Settings.ToString();
                    break;
                }
                else if (ActiveNavGroupId == (int)Moudule.Finance)
                {
                    LoadFinance();
                    dpAcme.Text = FinanceTitle; //  Moudule.Finance.ToString();
                    break;
                }
                else if (ActiveNavGroupId == (int)Moudule.Reports)
                {
                    LoadReport();
                    dpAcme.Text = Moudule.Reports.ToString();
                    break;
                }
                else if (ActiveNavGroupId == (int)Moudule.TDS)
                {
                    LoadTDS();
                    //dpAcme.Text = "Statutory";
                    dpAcme.Text = this.GetMessage(MessageCatalog.Master.Module.MODULE_STATUTORY_TITTLE);
                    break;
                }
                else if (activeNavGroupId == (int)Moudule.FixedAsset)
                {
                    LoadAsset();
                    //dpAcme.Text = "Fixed Asset";
                    dpAcme.Text = this.GetMessage(MessageCatalog.Master.Module.MODULE_FIXED_ASSET_TITTLE);
                    break;
                }
                else if (activeNavGroupId == (int)Moudule.Stock)
                {
                    LoadStock();
                    dpAcme.Text = Moudule.Stock.ToString();
                    break;
                }
                else if (activeNavGroupId == (int)Moudule.Utlities && bbiDataUtlity.Visibility == BarItemVisibility.Always)
                {
                    dpAcme.Text = DataUtility;
                    LoadDataUtility();
                    break;
                }
                else if (activeNavGroupId == (int)Moudule.Networking)
                {
                    //dpAcme.Text = "Networking";
                    dpAcme.Text = this.GetMessage(MessageCatalog.Master.Module.MODULE_NETWOKING_TITTLE);
                    LoadNetworking();
                    break;
                }
                else if (ActiveNavGroupId == (int)Moudule.Payroll)
                {
                    CloseWaitDialog();
                    LoadPayroll();
                    break;
                }
                else
                {
                    leftMenuBar.Groups[i].Visible = leftMenuBar.Groups[i].Caption == NavigationBarGroupName ? true : false;
                    leftMenuBar.Groups[i].Expanded = leftMenuBar.Groups[i].Caption == NavigationBarGroupName ? true : false;
                }
            }
            nbiInKind.Visible = nbiInKindArticle.Visible = false;
            nbiInKind.Enabled = nbiInKindArticle.Enabled = false;
            CloseWaitDialog();
        }

        private void LoadUserProfile()
        {
            using (UserSystem userSystem = new UserSystem())
            {
                ResultArgs resultArgs = userSystem.FetchUserProfile();

                using (UserProperty userProperty = new UserProperty())
                {
                    Bitmap UserPhoto = userProperty.GetUserPhoto("USER_PHOTO");
                    peUserPhoto.Image = UserPhoto == null ? global::ACPP.Properties.Resources.Default_Photo : UserPhoto;
                    lblWelcomeNote.Text = "Welcome " + userProperty.LoginUserFullName;
                    lblUserRole.Text = userProperty.GetUserRole;
                    lblName.Text = userProperty.FirstName + " " + userProperty.LastName;
                    //DevExpress.XtraEditors.Controls.EditorButton editButton = new DevExpress.XtraEditors.Controls.EditorButton();
                    //editButton.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                    //editButton.Image = peUserPhoto.Image;

                    //repositoryItemPopupContainerEdit1.Buttons.Clear();
                    //repositoryItemPopupContainerEdit1.Buttons.Add(editButton);               
                }
            }
        }

        private void LoadFdRenewal()
        {
            bool hasHome1 = HasFormInMDI(typeof(frmRenewalView).Name);
            CloseFormInMDI(typeof(frmRenewalView).Name);
            ShowWaitDialog();
            frmRenewalView frmRenewal = new frmRenewalView(FDTypes.RN);
            frmRenewal.MdiParent = this;
            frmRenewal.Show();
            CloseWaitDialog();
        }
        private void LoadFdPostInterest()
        {
            bool hasHome1 = HasFormInMDI(typeof(frmRenewalView).Name);
            CloseFormInMDI(typeof(frmRenewalView).Name);
            ShowWaitDialog();
            frmRenewalView frmRenewal = new frmRenewalView(FDTypes.POI);
            frmRenewal.MdiParent = this;
            frmRenewal.Show();
            CloseWaitDialog();
        }
        private void LoadFDWithdraw()
        {
            bool hasHome1 = HasFormInMDI(typeof(frmRenewalView).Name);
            CloseFormInMDI(typeof(frmRenewalView).Name);
            ShowWaitDialog();
            frmRenewalView frmRenewal = new frmRenewalView(FDTypes.WD);
            frmRenewal.MdiParent = this;
            frmRenewal.Show();
            CloseWaitDialog();
        }

        private void LoadFDPostCharge()
        {
            bool hasHome1 = HasFormInMDI(typeof(frmRenewalView).Name);
            CloseFormInMDI(typeof(frmRenewalView).Name);
            ShowWaitDialog();
            frmRenewalView frmRenewal = new frmRenewalView(FDTypes.POC);
            frmRenewal.MdiParent = this;
            frmRenewal.Show();
            CloseWaitDialog();
        }

        private void LoadFDLedgers()
        {
            bool hasHome = HasFormInMDI(typeof(frmLedgerView).Name);
            CloseFormInMDI(typeof(frmLedgerView).Name);
            ShowWaitDialog();
            frmLedgerView frmAccountView = new frmLedgerView(ledgerSubType.FD);
            frmAccountView.MdiParent = this;
            frmAccountView.Show();
            CloseWaitDialog();
        }

        public void LoadFDAccount()
        {
            bool hasHome = HasFormInMDI(typeof(frmFDAccountView).Name);
            CloseFormInMDI(typeof(frmFDAccountView).Name);
            ShowWaitDialog();
            frmFDAccountView frmAccountView = new frmFDAccountView(FDTypes.OP);
            frmAccountView.MdiParent = this;
            frmAccountView.Show();
            CloseWaitDialog();
        }

        public void LoadUserSecurity()
        {
            frmManageSecurity frmSecurity = new frmManageSecurity();
            frmSecurity.ShowDialog();
        }

        public void LoadRights()
        {
            frmUserManagement frmRights = new frmUserManagement();
            frmRights.ShowDialog();
        }

        public void LoadRole()
        {
            bool hasHome = HasFormInMDI(typeof(frmUserRoleView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmUserRoleView frmuserRole = new frmUserRoleView();
                frmuserRole.MdiParent = this;
                frmuserRole.Show();
            }
            CloseWaitDialog();
        }

        public void LoadUserView()
        {
            bool hasHome = HasFormInMDI(typeof(frmUserView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmUserView frmUser = new frmUserView();
                frmUser.MdiParent = this;
                frmUser.Show();
            }
            CloseWaitDialog();
        }

        private void LoadProjectCategory()
        {
            bool hasHome = HasFormInMDI(typeof(frmProjectCategoryView).Name);
            ShowWaitDialog();

            if (!hasHome)
            {
                frmProjectCategoryView frmCategory = new frmProjectCategoryView();
                frmCategory.MdiParent = this;
                frmCategory.Show();
            }

            CloseWaitDialog();
        }

        private void LoadSubBranchList()
        {
            bool hasHome = HasFormInMDI(typeof(frmSubBranchList).Name);
            ShowWaitDialog();

            if (!hasHome)
            {
                frmSubBranchList frmbranch = new frmSubBranchList();
                frmbranch.MdiParent = this;
                frmbranch.Show();
            }

            CloseWaitDialog();
        }

        private void LoadSyncmasters()
        {
            bool hasHome = HasFormInMDI(typeof(frmDataSyncStatus).Name);
            ShowWaitDialog();

            if (!hasHome)
            {
                frmDataSyncStatus frmdataSyncStatus = new frmDataSyncStatus();
                frmdataSyncStatus.MdiParent = this;
                frmdataSyncStatus.Show();
            }

            CloseWaitDialog();
        }

        private void LoadCostCentreCategory()
        {
            bool hasHome = HasFormInMDI(typeof(frmCostCentreCategoryView).Name);
            ShowWaitDialog();

            if (!hasHome)
            {
                frmCostCentreCategoryView frmCategory = new frmCostCentreCategoryView();
                frmCategory.MdiParent = this;
                frmCategory.Show();
            }

            CloseWaitDialog();
        }

        private void LoadTDSNatureofPyments()
        {
            bool hasHome = HasFormInMDI(typeof(frmNatureofPaymentsView).Name);
            ShowWaitDialog();

            if (!hasHome)
            {
                frmNatureofPaymentsView frmNoPayments = new frmNatureofPaymentsView();
                frmNoPayments.MdiParent = this;
                frmNoPayments.Show();
            }

            CloseWaitDialog();
        }

        private void LoadTDSDeducteeTypes()
        {
            bool hasHome = HasFormInMDI(typeof(frmDeducteeTypesView).Name);
            ShowWaitDialog();

            if (!hasHome)
            {
                frmDeducteeTypesView frmDeductee = new frmDeducteeTypesView();
                frmDeductee.MdiParent = this;
                frmDeductee.Show();
            }

            CloseWaitDialog();
        }

        private void LoadTaxPolicy()
        {
            frmDeducteeTaxView frmTax = new frmDeducteeTaxView();
            frmTax.ShowDialog();
        }

        private void LoadDutyTax()
        {
            bool hasHome = HasFormInMDI(typeof(frmDutyTaxView).Name);
            ShowWaitDialog();

            if (!hasHome)
            {
                frmDutyTaxView frmTax = new frmDutyTaxView();
                frmTax.MdiParent = this;
                frmTax.Show();
            }

            CloseWaitDialog();
        }

        private void LoadBudget(BudgetType type = BudgetType.BudgetPeriod)
        {
            this.CloseFormInMDI(typeof(frmBudgetView).Name);
            bool hasHome = HasFormInMDI(typeof(frmBudgetView).Name);
            if (!hasHome)
            {
                frmBudgetView frmBudget = new frmBudgetView(type);
                frmBudget.MdiParent = this;
                frmBudget.Show();
            }
        }

        private void LoadBudgetMysore(BudgetType type = BudgetType.BudgetPeriod)
        {
            this.CloseFormInMDI(typeof(frmMysoreBudgetView).Name);
            bool hasHome = HasFormInMDI(typeof(frmMysoreBudgetView).Name);
            if (!hasHome)
            {
                frmMysoreBudgetView frmBudgetdetails = new frmMysoreBudgetView(type);
                frmBudgetdetails.MdiParent = this;
                frmBudgetdetails.Show();
            }
        }

        private void LoadAuditor()
        {
            bool hasHome = HasFormInMDI(typeof(frmAuditorView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmAuditorView frmDonorAuditor = new frmAuditorView();
                frmDonorAuditor.MdiParent = this;
                frmDonorAuditor.Show();
            }

            CloseWaitDialog();
        }

        private void LoadDonor()
        {
            bool hasHome = HasFormInMDI(typeof(frmDonorView).Name);

            ShowWaitDialog();

            if (!hasHome)
            {
                frmDonorView frmDonorAuditor = new frmDonorView();
                frmDonorAuditor.MdiParent = this;
                frmDonorAuditor.Show();
            }

            CloseWaitDialog();
        }

        private void LoadCountry()
        {
            bool hasHome = HasFormInMDI(typeof(frmCountryView).Name);
            ShowWaitDialog();

            if (!hasHome)
            {
                frmCountryView frmCountry = new frmCountryView();
                frmCountry.MdiParent = this;
                frmCountry.Show();
            }

            CloseWaitDialog();
        }

        private void LoadBank()
        {
            bool hasHome = HasFormInMDI(typeof(frmBankView).Name);

            ShowWaitDialog();

            if (!hasHome)
            {
                frmBankView frmBank = new frmBankView();
                frmBank.MdiParent = this;
                frmBank.Show();
            }

            CloseWaitDialog();
        }

        private void LoadLedgerGroup()
        {
            bool hasHome = HasFormInMDI(typeof(frmLedgerGroup).Name);

            ShowWaitDialog();

            if (!hasHome)
            {
                frmLedgerGroup frmLedgerGroup = new frmLedgerGroup();
                frmLedgerGroup.MdiParent = this;
                frmLedgerGroup.Show();
            }

            CloseWaitDialog();
        }

        private void LoadCostCentre()
        {
            bool hasHome = HasFormInMDI(typeof(frmCostCentreView).Name);
            ShowWaitDialog();

            if (!hasHome)
            {
                frmCostCentreView frmCostCentre = new frmCostCentreView();
                frmCostCentre.MdiParent = this;
                frmCostCentre.Show();
            }

            CloseWaitDialog();
        }

        private void LoadInkindArticle()
        {
            bool hasHome = HasFormInMDI(typeof(frmInKindArticleView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmInKindArticleView frmInKindArticle = new frmInKindArticleView();
                frmInKindArticle.MdiParent = this;
                frmInKindArticle.Show();
            }
            CloseWaitDialog();
        }

        private void LoadGoverningMember()
        {
            bool hasHome = HasFormInMDI(typeof(frmExecutiveMemberView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmExecutiveMemberView frmExecutive = new frmExecutiveMemberView();
                frmExecutive.MdiParent = this;
                frmExecutive.Show();
            }
            CloseWaitDialog();
        }

        private void LoadLedger()
        {
            if (!string.IsNullOrEmpty(TransactionPeriod))
            {
                bool hasHome = HasFormInMDI(typeof(frmLedgerView).Name);
                CloseFormInMDI(typeof(frmLedgerView).Name);
                ShowWaitDialog();
                frmLedgerView frmAccountView = new frmLedgerView(ledgerSubType.GN);
                frmAccountView.MdiParent = this;
                frmAccountView.Show();
                CloseWaitDialog();
            }
            else
            {
                //if (XtraMessageBox.Show("Transaction Period is not created. Do you want to create the Transaction Period ?", "Acme.erp", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                if (XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.AccountingPeriod.ACCOUNTING_PERIOD_NOT_CREATED), this.GetMessage(MessageCatalog.Common.COMMON_ACMEERP_TITLE), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    LoadAccountPeriod();
                }
            }
        }

        private void LoadLedgerSundryCreditorsDebtors()
        {
            if (!string.IsNullOrEmpty(TransactionPeriod))
            {
                string name = typeof(frmLedgerView).Name;
                bool hasHome = HasFormInMDI(name);
                CloseFormInMDI(name);
                ShowWaitDialog();
                frmLedgerView frmAccountView = new frmLedgerView(ledgerSubType.GN, true, false);
                frmAccountView.MdiParent = this;
                frmAccountView.Show();
                CloseWaitDialog();
            }
            else
            {
                //if (XtraMessageBox.Show("Transaction Period is not created. Do you want to create the Transaction Period ?", "Acme.erp", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                if (XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.AccountingPeriod.ACCOUNTING_PERIOD_NOT_CREATED), this.GetMessage(MessageCatalog.Common.COMMON_ACMEERP_TITLE), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    LoadAccountPeriod();
                }
            }
        }

        private void LoadCashAccountsLedgers()
        {
            if (!string.IsNullOrEmpty(TransactionPeriod))
            {
                string name = typeof(frmLedgerView).Name;
                bool hasHome = HasFormInMDI(name);
                CloseFormInMDI(name);
                ShowWaitDialog();
                frmLedgerView frmAccountView = new frmLedgerView(ledgerSubType.CA, false, true);
                frmAccountView.MdiParent = this;
                frmAccountView.Show();
                CloseWaitDialog();
            }
            else
            {
                //if (XtraMessageBox.Show("Transaction Period is not created. Do you want to create the Transaction Period ?", "Acme.erp", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                if (XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.AccountingPeriod.ACCOUNTING_PERIOD_NOT_CREATED), this.GetMessage(MessageCatalog.Common.COMMON_ACMEERP_TITLE), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    LoadAccountPeriod();
                }
            }
        }

        private void LoadVoucher()
        {
            bool hasHome = HasFormInMDI(typeof(frmVoucherView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmVoucherView frmVoucher = new frmVoucherView();
                frmVoucher.MdiParent = this;
                frmVoucher.Show();
            }
            CloseWaitDialog();
        }

        private void LoadPurpose()
        {
            bool hasHome = HasFormInMDI(typeof(frmPurposeView).Name);
            ShowWaitDialog();

            if (!hasHome)
            {
                frmPurposeView frmPurpose = new frmPurposeView();
                frmPurpose.MdiParent = this;
                frmPurpose.Show();
            }

            CloseWaitDialog();
        }

        private void loadGoverningBody()
        {
            bool hasHome = HasFormInMDI(typeof(frmExecutiveMemberView).Name);
            ShowWaitDialog();

            if (!hasHome)
            {
                frmExecutiveMemberView frmMembers = new frmExecutiveMemberView();
                frmMembers.MdiParent = this;
                frmMembers.Show();
            }

            CloseWaitDialog();
        }

        private void LoadStatisticsType()
        {
            bool hasHome = HasFormInMDI(typeof(StatisticsTypeView).Name);
            ShowWaitDialog();

            if (!hasHome)
            {
                StatisticsTypeView frmStatisticsTypeView = new StatisticsTypeView();
                frmStatisticsTypeView.MdiParent = this;
                frmStatisticsTypeView.Show();
            }

            CloseWaitDialog();
        }


        private void LoadTDSLedgers()
        {
            bool hasHome = HasFormInMDI(typeof(frmLedgerView).Name);
            CloseFormInMDI(typeof(frmLedgerView).Name);
            ShowWaitDialog();
            frmLedgerView frmAccountView = new frmLedgerView(ledgerSubType.TDS);
            frmAccountView.MdiParent = this;
            frmAccountView.Show();
            CloseWaitDialog();
        }

        private void LoadTDSBookingView()
        {
            bool hasHome = HasFormInMDI(typeof(frmTDSBookingView).Name);
            CloseFormInMDI(typeof(frmTDSBookingView).Name);
            ShowWaitDialog();
            frmTDSBookingView frmBookingView = new frmTDSBookingView();
            frmBookingView.MdiParent = this;
            frmBookingView.Show();
            CloseWaitDialog();
        }

        private void LoadTDSPaymentView(TDSPayTypes tdsPayTrans)
        {
            bool hasHome = HasFormInMDI(typeof(frmPartyPaymentView).Name);
            CloseFormInMDI(typeof(frmPartyPaymentView).Name);
            ShowWaitDialog();
            frmPartyPaymentView frmPayments = new frmPartyPaymentView(tdsPayTrans);
            frmPayments.MdiParent = this;
            frmPayments.Show();
            CloseWaitDialog();
        }


        private void LoadTDSSection()
        {
            bool hasHome = HasFormInMDI(typeof(frmTDSSectionView).Name);
            ShowWaitDialog();

            if (!hasHome)
            {
                frmTDSSectionView frmTDSSection = new frmTDSSectionView();
                frmTDSSection.MdiParent = this;
                frmTDSSection.Show();
            }

            CloseWaitDialog();
        }

        private void LoadAuditInfo()
        {
            bool hasHome = HasFormInMDI(typeof(frmAuditingInfoView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmAuditingInfoView frmAudit = new frmAuditingInfoView();
                frmAudit.MdiParent = this;
                frmAudit.Show();
            }
            CloseWaitDialog();
        }

        private void loadAddress()
        {
            bool hasHome = HasFormInMDI(typeof(frmAuditorBook).Name);

            ShowWaitDialog();

            if (!hasHome)
            {
                frmAuditorBook frmAuditorbook = new frmAuditorBook();
                frmAuditorbook.MdiParent = this;
                frmAuditorbook.Show();
            }

            CloseWaitDialog();
        }

        private void LoadBankAccount()
        {
            if (!string.IsNullOrEmpty(TransactionPeriod))
            {
                bool hasHome = HasFormInMDI(typeof(frmBankAccountView).Name);
                CloseFormInMDI(typeof(frmBankAccountView).Name);
                ShowWaitDialog();
                frmBankAccountView frmBankAccount = new frmBankAccountView();
                frmBankAccount.MdiParent = this;
                frmBankAccount.Show();
                CloseWaitDialog();
            }
            else
            {
                //if (XtraMessageBox.Show("Transaction Period is not created. Do you want to create the Transaction Period ?", "Acme.erp", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                if (XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.AccountingPeriod.ACCOUNTING_PERIOD_NOT_CREATED), this.GetMessage(MessageCatalog.Common.COMMON_ACMEERP_TITLE), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    LoadAccountPeriod();
                }
            }

        }

        /// <summary>
        /// On 20/04/2022, to enforce Receipt Module Rights
        /// </summary>
        public void EnforceReceiptModuleRightsInMainMenus()
        {
            //04/04/2022, To Enable/Disable ----------------------------------------------------------------
            nbiReceipt.Enabled = nbiImportSplitProject.Enabled = nbiDataMigration.Enabled = true;
            EnforceReceiptModule(new object[] { nbiReceipt, nbiImportSplitProject, nbiProjectBulkImportData, nbiDataMigration });

            btnRequestModuleRights.Visible = false;
            tileCtlReceiptModueStatus.Visible = false;
            if (AppSetting.IS_SDB_INM && (UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false) >= UtilityMember.DateSet.ToDate(SettingProperty.Enforce_Receipt_Module_FY, false)))
            {
                btnRequestModuleRights.Visible = true;
                tileCtlReceiptModueStatus.Visible = true;

                tltItemReceiptModuleStatus.Frames.Clear();
                if (AppSetting.ENABLE_TRACK_RECEIPT_MODULE)
                {
                    //1 --> For Receipt Module Rights
                    TileItemFrame item = GetAlertEmptyTileItem("", MessageCatalog.Common.COMMON_RECEIPT_ENABLED_MESSAGE, "", 1);
                    tltItemReceiptModuleStatus.Frames.Add(item);

                    tileCtlReceiptModueStatus.Left = this.Width - (tileCtlReceiptModueStatus.Width - 5);
                    btnRequestModuleRights.Left = tileCtlReceiptModueStatus.Left - (btnRequestModuleRights.Width - 20);
                }
                else
                {
                    //1 --> For Receipt Module Rights
                    TileItemFrame item = GetAlertEmptyTileItem("", MessageCatalog.Common.COMMON_RECEIPT_DISABLED_MESSAGE, "", 1);
                    item.Tag = 1; //For Receipt Module Rights
                    tltItemReceiptModuleStatus.Frames.Add(item);
                    tileCtlReceiptModueStatus.Left = this.Width - (tileCtlReceiptModueStatus.Width - 30);
                    btnRequestModuleRights.Left = tileCtlReceiptModueStatus.Left - (btnRequestModuleRights.Width - 40);
                }
            }
            //On 28/01/2025 - To set alert message saying that vouchers are locked
            AttachAlertForGraceDaysForVoucherEntry();
        }

        /// <summary>
        /// On 23/01/2025 - To set alert message saying that vouchers are locked
        /// </summary>
        private void AttachAlertForGraceDaysForVoucherEntry()
        {
            if (AppSetting.VoucherGraceDays > 0)
            {
                string msg = "As per the Province regulation, You are allowed to enter Voucher(s) from " +
                        UtilityMember.DateSet.ToDate(this.AppSetting.GraceLockDateTo.ToShortDateString(), false).ToShortDateString();

                foreach (TileItemFrame fram in tltItemReceiptModuleStatus.Frames)
                {
                    if (fram.Tag != null && fram.Tag.ToString() == "2")
                    {
                        tltItemReceiptModuleStatus.Frames.Remove(fram);
                        break;
                    }
                }

                tileCtlReceiptModueStatus.Visible = true;
                TileItemFrame framitem = GetAlertEmptyTileItem("", msg, "", 2);
                tltItemReceiptModuleStatus.Frames.Add(framitem);

                tileCtlReceiptModueStatus.Left = this.Width - (tileCtlReceiptModueStatus.Width);
                btnRequestModuleRights.Left = tileCtlReceiptModueStatus.Left - (btnRequestModuleRights.Width - 20);
                ShowGraceDaysTootip();
            }
            //---------------------------------------------------------------------------------------------
        }

        private void ShowGraceDaysTootip()
        {
            //Attach Tool Tip
            if (AppSetting.VoucherGraceDays > 0)
            {
                string enforce = this.AppSetting.VoucherEnforceGraceMode == (Int32)VoucherEntryGraceDaysMode.Yes ? "Yes" :
                        this.AppSetting.VoucherEnforceGraceMode == (Int32)VoucherEntryGraceDaysMode.No ? "No" : "None";
                string tempgracedays = "";
                if (!string.IsNullOrEmpty(this.AppSetting.VoucherGraceTmpDateFrom) && !string.IsNullOrEmpty(this.AppSetting.VoucherGraceTmpDateTo)
                        && !string.IsNullOrEmpty(this.AppSetting.VoucherGraceTmpValidUpto))
                {
                    tempgracedays = UtilityMember.DateSet.ToDate(this.AppSetting.VoucherGraceTmpDateFrom, false).ToShortDateString() + " and  " +
                                        UtilityMember.DateSet.ToDate(this.AppSetting.VoucherGraceTmpDateTo, false).ToShortDateString() + " - " +
                                        UtilityMember.DateSet.ToDate(this.AppSetting.VoucherGraceTmpValidUpto, false).ToShortDateString();
                }
                VoucherEntryGraceDaysToolTipMessage = "Enforce Grace Days :" + enforce + ", Day(s) : " + AppSetting.VoucherGraceDays.ToString() +
                             ", Temporary Date Range : " + AppSetting.IsVoucherGraceTmpActive.ToString() + (string.IsNullOrEmpty(tempgracedays) ? "" : " (" + tempgracedays + ")");
            }
        }

        private void ApplyUserRights()
        {
            try
            {
                CommonMethod.Flag = 0;
                //Master Options 
                CommonMethod.ApplyRights(nbiBalanceRefresh, (int)Menus.RefreshBalance);
                CommonMethod.ApplyRights(nbiDeleteunusedLedger, (int)Menus.UnusedLedger);
                CommonMethod.ApplyRights(nbiMoveDeleteVouchers, (int)Menus.MoveDeleteMultipleLedgers);
                CommonMethod.ApplyRights(nbiRegenerateVoucher, (int)Menus.RegenarateVoucher);
                CommonMethod.ApplyRights(nbiVoucher, (int)Menus.VoucherNumberDefinition);
                CommonMethod.ApplyRights(nbiMapLedProject, (int)Menus.AccountMapping);
                //CommonMethod.ApplyRights(nbiMapLedgerOption, (int)Menus.LedgerOptions); //It has to be changed by Chinna
                CommonMethod.ApplyRights(nbiAuditLockTrans, (int)Menus.AuditLockTrans);
                CommonMethod.ApplyRights(nbiFinnanceSettings, (int)Menus.FinanceSettings);
                isEnableOptions = EnableGroups(nbiMaster);

                //Master Details
                CommonMethod.ApplyRights(nbiProjectCategory, (int)Menus.ProjectCategory);
                CommonMethod.ApplyRights(nbiProject, (int)Menus.Project);
                CommonMethod.ApplyRights(nbiLedgerGroup, (int)Menus.LedgerGroup);
                CommonMethod.ApplyRights(nbiLedger, (int)Menus.Ledger);
                CommonMethod.ApplyRights(nbiBankAccount, (int)Menus.BankAccounts);
                CommonMethod.ApplyRights(nbiCostCentre, (int)Menus.CostCentre);
                CommonMethod.ApplyRights(nbiBank, (int)Menus.Bank);
                CommonMethod.ApplyRights(nbiCountry, (int)Menus.Country);
                //CommonMethod.ApplyRights(nbiAudit_Info, (int)Menus.AuditInfo);
                //CommonMethod.ApplyRights(nbiAuditor, (int)Menus.Auditor);
                CommonMethod.ApplyRights(nbiDonor, (int)Menus.Donor);
                CommonMethod.ApplyRights(nbiPurpose, (int)Menus.Purpose);
                CommonMethod.ApplyRights(nbiAuditLockType, (int)Menus.AuditLockTypes);
                CommonMethod.ApplyRights(nbiCostCentreCategory, (int)Menus.CostCentreCategory);
                CommonMethod.ApplyRights(nbiState, (int)Menus.State);
                isEnableMasters = EnableGroups(nbiVoucherMasters);


                // Asset module User Rights
                CommonMethod.ApplyRights(nbiItems, (int)Menus.AssetItem);
                CommonMethod.ApplyRights(nbiAssetVendor, (int)Menus.Vendor);
                CommonMethod.ApplyRights(nbiManufacture, (int)Menus.Manufacturer);
                CommonMethod.ApplyRights(nbiAssetCustodian, (int)Menus.Custodian);
                CommonMethod.ApplyRights(nbiBlockArea, (int)Menus.Block);
                CommonMethod.ApplyRights(nbiLocations, (int)Menus.Location);
                CommonMethod.ApplyRights(nbiUnitOfMeasure, (int)Menus.UoM);
                isEnableAssetMasters = EnableGroups(nbgAssetMasters);

                CommonMethod.ApplyRights(nbiAssetOPBalance, (int)Menus.OpeningAsset);
                CommonMethod.ApplyRights(nbiPurchaseVoucher, (int)Menus.Purchase);
                CommonMethod.ApplyRights(nbiAssetReceiveVoucher, (int)Menus.ReceiveInKind);
                CommonMethod.ApplyRights(nbiSaleVoucher, (int)Menus.SalesDisposalDonation);
                CommonMethod.ApplyRights(nbiDepreciation, (int)Menus.Depreciation);
                CommonMethod.ApplyRights(nbiUpdateAssetDetails, (int)Menus.UpdateAssetDetails);
                isEnableAssetTransaction = EnableGroups(nbgAssetTransactions);

                CommonMethod.ApplyRights(nbiAssetRegisters, (int)Menus.FixedAssetRegister);
                isEnableAssetView = EnableGroups(nbgAssetViews);

                CommonMethod.ApplyRights(nbiAssetItemLedgerMapping, (int)Menus.FixedAssetRegister);
                CommonMethod.ApplyRights(nbiMapLocations, (int)Menus.MapAssetLedger);
                CommonMethod.ApplyRights(nbiImportAssets, (int)Menus.ImportOpeningAsset);
                CommonMethod.ApplyRights(nbiConfigure, (int)Menus.Configure);
                isEnableAssetOptions = EnableGroups(nbgAssetOptions);

                // 28.10.2021
                // Payroll
                CommonMethod.ApplyRights(nbiCreatePayroll, (int)Menus.CreateNewPayrollMonth);
                CommonMethod.ApplyRights(nbiPayrollGroup, (int)Menus.PayrollGroup);
                CommonMethod.ApplyRights(nbiPayrollDepartment, (int)Menus.PayrollGroup);
                CommonMethod.ApplyRights(nbiPayrollWorkLocation, (int)Menus.PayrollGroup);
                CommonMethod.ApplyRights(nbiStaffDetails, (int)Menus.Staff);
                CommonMethod.ApplyRights(nbiPayrollComponent, (int)Menus.PayrollComponent);
                CommonMethod.ApplyRights(nbiLoan, (int)Menus.Loan);
                isEnablePayrollMaster = EnableGroups(nbgDefinition);

                CommonMethod.ApplyRights(nbi1LoanMgt, (int)Menus.IssuesLoan);
                isEnablePayrollLoanIssues = EnableGroups(nbg1Allotement);

                CommonMethod.ApplyRights(nbiOpenPayroll, (int)Menus.OpenPayrollMonth);
                CommonMethod.ApplyRights(nbiDeletePayroll, (int)Menus.DeletePayrollMonth);
                CommonMethod.ApplyRights(nbiComponentAlloaction, (int)Menus.componentAllocation);
                CommonMethod.ApplyRights(nbiPayrollView, (int)Menus.ViewPayroll);
                CommonMethod.ApplyRights(nbiPayrollPostpyament, (int)Menus.PostPayrollvouchertoFinanceTransaction);
                isEnablePayrollGateway = EnableGroups(nbgGateway);

                // 25.11.2021
                // Stock Details
                CommonMethod.ApplyRights(nbiStockGroup, (int)Menus.StockGroup);
                CommonMethod.ApplyRights(nbiStockItem, (int)Menus.StockItem);
                CommonMethod.ApplyRights(nbiStockVendor, (int)Menus.StockVendor);
                CommonMethod.ApplyRights(nbiStockCustodian, (int)Menus.StockCustodian);
                CommonMethod.ApplyRights(nbiStockBlock, (int)Menus.StockBlockArea);
                CommonMethod.ApplyRights(nbiStockLocation, (int)Menus.StockLocation);
                CommonMethod.ApplyRights(nbiStockUnitMeasure, (int)Menus.StockUOM);
                CommonMethod.ApplyRights(nbiStockOP, (int)Menus.StockOpeningBalance);
                CommonMethod.ApplyRights(nbiMastersImport, (int)Menus.StockImportMaster);
                isEnableStockMaster = EnableGroups(nbgStockMasters);

                CommonMethod.ApplyRights(nbiStockHome, (int)Menus.StockRegister);
                CommonMethod.ApplyRights(nbiPurchase, (int)Menus.StockPurchase);
                CommonMethod.ApplyRights(nbiStockReceive, (int)Menus.StockReceiveInkind);
                CommonMethod.ApplyRights(nbiSale, (int)Menus.StockSales);
                CommonMethod.ApplyRights(nbiUtilised, (int)Menus.StockUtilize);
                CommonMethod.ApplyRights(bbiDisposal, (int)Menus.StockDispose);
                CommonMethod.ApplyRights(nbiItemTransfer, (int)Menus.StockReceiveInkind);
                CommonMethod.ApplyRights(nbiGoodsReturn, (int)Menus.StockPurchaseReturn);
                isEnableStockTransaction = EnableGroups(nbgStockVouchers);

                //Views Details 

                CommonMethod.ApplyRights(nbiReceiptView, (int)Menus.ReceiptView);
                CommonMethod.ApplyRights(nbiJournalView, (int)Menus.JournalView);

                isEnableView = EnableGroups(nbiViews);

                // Finance Transaction
                CommonMethod.Flag = 0;
                CommonMethod.ApplyRights(nbiReceipt, (int)Menus.Receipt);
                CommonMethod.ApplyRights(nbiPayment, (int)Menus.Payments);
                CommonMethod.ApplyRights(nbiContra, (int)Menus.Contra);
                CommonMethod.ApplyRights(nbiJournal, (int)Menus.Journal);
                CommonMethod.ApplyRights(nviBankReconciliation, (int)Menus.BankReconciliation);
                CommonMethod.ApplyRights(nbiBudget, (int)Menus.Budget);
                CommonMethod.ApplyRights(nbiBudgetAnnual, (int)Menus.BudgetAnnual);

                isEnableTransaction = EnableGroups(nbgTransaction);

                // Fixed Deposit 
                CommonMethod.ApplyRights(nbiFDLedger, (int)Menus.FixedDepositLedger);
                CommonMethod.ApplyRights(nbiFixedDeposit, (int)Menus.FixedDeposit);
                CommonMethod.ApplyRights(nbiFDInvestment, (int)Menus.FixedInvestment);
                CommonMethod.ApplyRights(nbiFDRenewal, (int)Menus.FixedDepositRenewal);
                CommonMethod.ApplyRights(nbiFDReInvestment, (int)Menus.FixedDepositReInvestment);
                CommonMethod.ApplyRights(nbiFDPostInterst, (int)Menus.FixedDepositPostInterest);
                CommonMethod.ApplyRights(nbiFDWithdraw, (int)Menus.FDWithdrawal);
                CommonMethod.ApplyRights(nbiFDRegisters, (int)Menus.FixedDepositRegister);
                isEnableFixedDeposit = EnableGroups(nbgFixedDeposit);
                bbiFinance.Visibility = isEnableMenu ? BarItemVisibility.Always : BarItemVisibility.Never;
                if (bbiFinance.Visibility == BarItemVisibility.Always)
                {
                    dpAcme.Text = FinanceTitle; // "Finance";
                }
                else
                {
                    dpAcme.Text = "";
                }
                isEnableMenu = false;

                // TDS Module Level Setting
                if (ActiveNavGroupId == (int)Moudule.TDS)
                    CommonMethod.ApplyRights(nbiTDSSettings, (int)Menus.TDSSetting);
                else
                    nbgOptions.Visible = false;

                // Asset Module Level Setting
                if (ActiveNavGroupId == (int)Moudule.FixedAsset)
                    CommonMethod.ApplyRights(nbiConfigure, (int)Menus.AssetSetting);
                else
                    nbgAssetOptions.Visible = false;
                //  Data Utitlity

                CommonMethod.ApplyRights(nbiLocalization, (int)Menus.MasterSetting);
                CommonMethod.ApplyRights(nviAccountPeriod, (int)Menus.TransactionPeriod);
                isEnableSettings = EnableGroups(nbgMaster);
                bbiDataUtlity.Visibility = isEnableMenu ? BarItemVisibility.Always : BarItemVisibility.Never;
                if (bbiDataUtlity.Visibility == BarItemVisibility.Always && bbiFinance.Visibility == BarItemVisibility.Never)
                {
                    dpAcme.Text = DataUtility; // "Utilities";
                }

                CommonMethod.Flag = 0;

                CommonMethod.ApplyRights(nbiUser, (int)Menus.User);
                CommonMethod.ApplyRights(nbiUserRole, (int)Menus.UserRole);
                CommonMethod.ApplyRights(nbiUserRights, (int)Menus.UserRightsManagement);
                CommonMethod.ApplyRights(nbiManageSecurity, (int)Menus.ManageSecurity);
                isEnableUserRights = EnableGroups(nbgUserManagement);
                bbiDataUtlity.Visibility = isEnableMenu ? BarItemVisibility.Always : BarItemVisibility.Never;

                CommonMethod.ApplyRights(nbiDataMigration, (int)Menus.DataMigration);
                CommonMethod.ApplyRights(BackupNew, (int)Menus.Backup);
                CommonMethod.ApplyRights(navRestoreData, (int)Menus.Restore);
                CommonMethod.ApplyRights(nbiDataImport, (int)Menus.DataExport);
                CommonMethod.ApplyRights(nbiImportSplitProject, (int)Menus.ImportData);
                CommonMethod.ApplyRights(nbiSplitLedger, (int)Menus.ExportData);
                CommonMethod.ApplyRights(nbiMapMigration, (int)Menus.MigrationMapping);
                CommonMethod.ApplyRights(nbiSubBranchList, (int)Menus.SubBranchList);
                CommonMethod.ApplyRights(nbiManageMultiDB, (int)Menus.ManageMultiBranch);
                CommonMethod.ApplyRights(nbiUploadVouchers, (int)Menus.UploadSubBranchVouchers);
                CommonMethod.ApplyRights(nbiExportMasters, (int)Menus.ExportMastertoSubBranch);
                CommonMethod.ApplyRights(nbiPortalUpdates, (int)Menus.ExportMastertoSubBranch);

                isEnableDataUtility = EnableGroups(nbgDataUtility);

                CommonMethod.ApplyRights(nbiImportMasters, (int)Menus.ShowImportMasters);
                CommonMethod.ApplyRights(nbiExportVouchers, (int)Menus.ShowExportVouchers);
                CommonMethod.ApplyRights(nbiMapLedgers, (int)Menus.ShowMapLedgers);
                CommonMethod.ApplyRights(nbiLicenseKey, (int)Menus.LicenseKey);
                CommonMethod.ApplyRights(nbiUploadBranchOfficeDB, (int)Menus.UploadDatabase);
                isEnableHeadOffieInterface = EnableGroups(nbiHeadOfficeInterface);

                bbiDataUtlity.Visibility = isEnableMenu ? BarItemVisibility.Always : BarItemVisibility.Never;
                isEnableMenu = false;

                //Networking
                CommonMethod.Flag = 0;

                CommonMethod.ApplyRights(nbiMembers, (int)Menus.Member);
                CommonMethod.ApplyRights(nbiProspectsView, (int)Menus.Prospect);
                CommonMethod.ApplyRights(nbiTemplates, (int)Menus.MailTemplate);
                CommonMethod.ApplyRights(nbiSMSTemplates, (int)Menus.SMSTemplate);
                isEnableDonorMasters = EnableGroups(nbgDonorMasters);
                bbiNetworking.Visibility = isEnableMenu ? BarItemVisibility.Always : BarItemVisibility.Never;

                CommonMethod.ApplyRights(nbiThanksgiving, (int)Menus.ThanksgivingMail);
                CommonMethod.ApplyRights(nbiAppealLetter, (int)Menus.AppealMail);
                CommonMethod.ApplyRights(nbiNewsLetter, (int)Menus.NewsletterMail);
                CommonMethod.ApplyRights(nbiAnniversary, (int)Menus.AnniversaryMail);
                CommonMethod.ApplyRights(nbiFeastDay, (int)Menus.FeastMail);
                isEnableDonorMailDesk = EnableGroups(nbgDonorMailDesk);
                bbiNetworking.Visibility = isEnableMenu ? BarItemVisibility.Always : BarItemVisibility.Never;

                CommonMethod.ApplyRights(nbiSMSThanksgiving, (int)Menus.ThanksgivingSMS);
                CommonMethod.ApplyRights(nbiSMSAppeal, (int)Menus.AppealSMS);
                CommonMethod.ApplyRights(nbiSMSAnniversary, (int)Menus.AnniversarySMS);
                CommonMethod.ApplyRights(nbiSMSFeast, (int)Menus.FeastSMS);
                isEnableDonorContactDesk = EnableGroups(nbgDonorContactDesk);
                bbiNetworking.Visibility = isEnableMenu ? BarItemVisibility.Always : BarItemVisibility.Never;
                isEnableMenu = false;

                //Report 
                if (CommonMethod.ApplyUserRightsForTransaction((int)Reports.Reports) == 0)
                {
                    bbiReportsMenu.Visibility = BarItemVisibility.Never;
                }
                else
                {
                    bbiReportsMenu.Visibility = BarItemVisibility.Always;
                }
                EnableGroups();

                if (CommonMethod.ApplyUserRights((int)Menus.Dashboard).Equals(0))
                {
                    bbiHome.Visibility = BarItemVisibility.Never;
                    LoadLoginHome();
                }


                //On 17/09/2024, To lock datamanagement features for few license keys -----------------------------------

                // Commanded by chinna 29/10/2024 to show in the finance module itself
                if (nbgDataUtility.Visible == true)
                {
                    nbgDataUtility.Visible = !(this.AppSetting.IsLockedDataManagementFeatures);
                }
                //-------------------------------------------------------------------------------------------------------
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void EnableGroups()
        {
            if (ActiveNavGroupId == (int)Moudule.Finance)
            {
                if (bbiFinance.Visibility == BarItemVisibility.Always)
                {
                    nbgTransaction.Visible = nbgTransaction.Expanded = isEnableTransaction;
                    nbgFixedDeposit.Visible = nbgFixedDeposit.Expanded = isEnableFixedDeposit;
                    nbiVoucherMasters.Visible = isEnableMasters;
                    nbiViews.Visible = isEnableView;
                    nbiMaster.Visible = isEnableOptions;
                    nbiHeadOfficeInterface.Visible = false;
                    nbgTDSVouchers.Visible = nbgTDSMasters.Visible = false;
                    nbgPayroll.Visible = nbgInventory.Visible = false;
                    nbgDefinition.Visible = nbg1Allotement.Visible = nbgGateway.Visible = false; // nbi1LoanMgt.Visible =
                    nbgDataUtility.Visible = nbgUserManagement.Visible = false;
                    nbgMaster.Visible = false;
                    nbgAssetTransactions.Visible = nbgAssetMasters.Visible = false;
                    nbgAssetViews.Visible = nbgAssetOptions.Visible = false;
                    nbgStockMasters.Visible = nbgStockVouchers.Visible = false;
                    nbgDonorMasters.Visible = nbgDonorMasters.Expanded = false;
                    nbgDonorContactDesk.Visible = false;
                    nbgDonorMailDesk.Visible = false;
                }
            }
            else if (ActiveNavGroupId == (int)Moudule.Utlities)
            {
                if (bbiDataUtlity.Visibility == BarItemVisibility.Always)
                {
                    nbgTransaction.Visible = nbgFixedDeposit.Visible = false;
                    nbgMaster.Visible = nbgMaster.Expanded = false;
                    nbiVoucherMasters.Visible = nbiVoucherMasters.Expanded = false;
                    nbiViews.Visible = nbiViews.Expanded = false;
                    nbiMaster.Visible = nbiMaster.Expanded = nbgAudit.Visible = nbgAudit.Expanded = false;
                    nbiHeadOfficeInterface.Visible = nbiHeadOfficeInterface.Expanded = false;
                    nbgDataUtility.Visible = nbgDataUtility.Expanded = false;
                    nbgUserManagement.Visible = nbgUserManagement.Expanded = false;
                    nbgPayroll.Visible = nbgPayroll.Expanded = nbgInventory.Visible = false;
                    nbgTDSVouchers.Visible = nbgTDSMasters.Visible = false;
                    nbgDefinition.Visible = nbg1Allotement.Visible = nbgGateway.Visible = false; //  nbi1LoanMgt.Visible =
                    nbgAssetTransactions.Visible = nbgAssetMasters.Visible = nbgAssetViews.Visible = nbgAssetOptions.Visible = false;
                    nbgStockMasters.Visible = nbgStockVouchers.Visible = false;
                    nbgDonorMasters.Visible = nbgDonorMasters.Expanded = false;
                    nbgDonorContactDesk.Visible = false;
                    nbgDonorMailDesk.Visible = false;
                    nbgDataUtility.Visible = nbgDataUtility.Expanded = isEnableDataUtility;
                    nbgMaster.Visible = nbgMaster.Expanded = isEnableSettings;
                    nbgUserManagement.Visible = nbgUserManagement.Expanded = isEnableUserRights;
                    nbiHeadOfficeInterface.Visible = nbiHeadOfficeInterface.Expanded = isEnableHeadOffieInterface;
                }
            }
            else if (ActiveNavGroupId == (int)Moudule.FixedAsset)
            {
                if (bbiAsset.Visibility == BarItemVisibility.Always)
                {
                    nbgTransaction.Visible = nbgFixedDeposit.Visible = false;
                    nbgMaster.Visible = nbgMaster.Expanded = false;
                    nbiVoucherMasters.Visible = nbiVoucherMasters.Expanded = false;
                    nbiViews.Visible = nbiViews.Expanded = false;
                    nbiMaster.Visible = nbiMaster.Expanded = nbgAudit.Expanded = nbgAudit.Visible = false;
                    nbiHeadOfficeInterface.Visible = nbiHeadOfficeInterface.Expanded = false;
                    nbgDataUtility.Visible = nbgDataUtility.Expanded = false;
                    nbgUserManagement.Visible = nbgUserManagement.Expanded = false;
                    nbgPayroll.Visible = nbgPayroll.Expanded = nbgInventory.Visible = false;
                    nbgTDSVouchers.Visible = nbgTDSMasters.Visible = false;
                    nbgAssetTransactions.Visible = nbgAssetMasters.Visible = nbgAssetViews.Visible = nbgAssetOptions.Visible = false;
                    nbgStockMasters.Visible = nbgStockVouchers.Visible = false;
                    nbgDonorMasters.Visible = nbgDonorMasters.Expanded = false;
                    nbgDonorContactDesk.Visible = false;
                    nbgDonorMailDesk.Visible = false;
                    nbgDataUtility.Visible = nbgDataUtility.Expanded = false;
                    nbgMaster.Visible = nbgMaster.Expanded = false;
                    nbgUserManagement.Visible = nbgUserManagement.Expanded = false;
                    nbiHeadOfficeInterface.Visible = nbiHeadOfficeInterface.Expanded = false;
                    nbgDefinition.Visible = nbg1Allotement.Visible = nbgGateway.Visible = false; // nbi1LoanMgt.Visible

                    nbgAssetMasters.Visible = nbgAssetMasters.Expanded = isEnableAssetMasters;
                    nbgAssetTransactions.Visible = nbgAssetTransactions.Expanded = isEnableAssetTransaction;
                    nbgAssetViews.Visible = nbgAssetViews.Expanded = isEnableAssetView;
                    nbgAssetOptions.Visible = nbgAssetOptions.Expanded = isEnableAssetOptions;
                }
            }
            else if (activeNavGroupId == (int)Moudule.Payroll)
            {
                if (bbiPayroll.Visibility == BarItemVisibility.Always)
                {
                    nbgTransaction.Visible = nbgFixedDeposit.Visible = false;
                    nbgMaster.Visible = nbgMaster.Expanded = false;
                    nbiVoucherMasters.Visible = nbiVoucherMasters.Expanded = false;
                    nbiViews.Visible = nbiViews.Expanded = false;
                    nbiMaster.Visible = nbiMaster.Expanded = nbgAudit.Expanded = nbgAudit.Visible = false;
                    nbiHeadOfficeInterface.Visible = nbiHeadOfficeInterface.Expanded = false;
                    nbgDataUtility.Visible = nbgDataUtility.Expanded = false;
                    nbgUserManagement.Visible = nbgUserManagement.Expanded = false;
                    nbgPayroll.Visible = nbgPayroll.Expanded = nbgInventory.Visible = false;
                    nbgTDSVouchers.Visible = nbgTDSMasters.Visible = false;
                    nbgAssetTransactions.Visible = nbgAssetMasters.Visible = nbgAssetViews.Visible = nbgAssetOptions.Visible = false;
                    nbgStockMasters.Visible = nbgStockVouchers.Visible = false;
                    nbgDonorMasters.Visible = nbgDonorMasters.Expanded = false;
                    nbgDonorContactDesk.Visible = false;
                    nbgDonorMailDesk.Visible = false;
                    nbgDataUtility.Visible = nbgDataUtility.Expanded = false;
                    nbgMaster.Visible = nbgMaster.Expanded = false;
                    nbgUserManagement.Visible = nbgUserManagement.Expanded = false;
                    nbiHeadOfficeInterface.Visible = nbiHeadOfficeInterface.Expanded = false;

                    nbgDefinition.Visible = nbgDefinition.Expanded = isEnablePayrollMaster;
                    nbg1Allotement.Visible = nbg1Allotement.Expanded = isEnablePayrollLoanIssues;
                    nbgGateway.Visible = nbgGateway.Expanded = isEnablePayrollGateway;
                }
            }
            else if (activeNavGroupId == (int)Moudule.Stock)
            {
                if (bbiStock.Visibility == BarItemVisibility.Always)
                {
                    nbgTransaction.Visible = nbgFixedDeposit.Visible = false;
                    nbgMaster.Visible = nbgMaster.Expanded = false;
                    nbiVoucherMasters.Visible = nbiVoucherMasters.Expanded = false;
                    nbiViews.Visible = nbiViews.Expanded = false;
                    nbiMaster.Visible = nbiMaster.Expanded = nbgAudit.Expanded = nbgAudit.Visible = false;
                    nbiHeadOfficeInterface.Visible = nbiHeadOfficeInterface.Expanded = false;
                    nbgDataUtility.Visible = nbgDataUtility.Expanded = false;
                    nbgUserManagement.Visible = nbgUserManagement.Expanded = false;
                    nbgPayroll.Visible = nbgPayroll.Expanded = nbgInventory.Visible = false;
                    nbgTDSVouchers.Visible = nbgTDSMasters.Visible = false;
                    nbgAssetTransactions.Visible = nbgAssetMasters.Visible = nbgAssetViews.Visible = nbgAssetOptions.Visible = false;

                    nbgDonorMasters.Visible = nbgDonorMasters.Expanded = false;
                    nbgDonorContactDesk.Visible = false;
                    nbgDonorMailDesk.Visible = false;
                    nbgDataUtility.Visible = nbgDataUtility.Expanded = false;
                    nbgMaster.Visible = nbgMaster.Expanded = false;
                    nbgUserManagement.Visible = nbgUserManagement.Expanded = false;
                    nbiHeadOfficeInterface.Visible = nbiHeadOfficeInterface.Expanded = false;

                    nbgDefinition.Visible = nbgDefinition.Expanded = false;
                    nbg1Allotement.Visible = nbg1Allotement.Expanded = false;
                    nbgGateway.Visible = nbgGateway.Expanded = false;

                    nbgStockMasters.Visible = nbgStockMasters.Expanded = isEnableStockMaster;
                    nbgStockVouchers.Visible = nbgStockVouchers.Expanded = isEnableStockTransaction;
                }
            }
            else if (ActiveNavGroupId == (int)Moudule.Networking)
            {
                if (bbiNetworking.Visibility == BarItemVisibility.Always)
                {
                    nbgDonorMasters.Visible = nbgDonorMasters.Expanded = isEnableDonorMasters;
                    nbgDonorContactDesk.Visible = isEnableDonorContactDesk;
                    nbgDonorMailDesk.Visible = isEnableDonorMailDesk;
                    nbgTransaction.Visible = nbgFixedDeposit.Visible = false;
                    nbgMaster.Visible = nbgMaster.Expanded = false;
                    nbiVoucherMasters.Visible = nbiVoucherMasters.Expanded = false;
                    nbiViews.Visible = nbiViews.Expanded = false;
                    nbiMaster.Visible = nbiMaster.Expanded = nbgAudit.Expanded = nbgAudit.Visible = false;
                    nbiHeadOfficeInterface.Visible = nbiHeadOfficeInterface.Expanded = false;
                    nbgDataUtility.Visible = nbgDataUtility.Expanded = false;
                    nbgUserManagement.Visible = nbgUserManagement.Expanded = false;
                    nbgPayroll.Visible = nbgPayroll.Expanded = nbgInventory.Visible = false;
                    nbgTDSVouchers.Visible = nbgTDSMasters.Visible = false;
                    nbgAssetTransactions.Visible = nbgAssetMasters.Visible = false;
                    nbgStockMasters.Visible = nbgStockVouchers.Visible = false;
                    nbgDataUtility.Visible = false;
                    nbgMaster.Visible = false;
                    nbgUserManagement.Visible = nbgUserManagement.Expanded = false;
                    nbiHeadOfficeInterface.Visible = nbiHeadOfficeInterface.Expanded = false;
                }
            }
        }

        private bool EnableGroups(DevExpress.XtraNavBar.NavBarGroup navBarGroup)
        {
            navBarGroup.Visible = CommonMethod.Flag == 1 ? true : false;
            if (CommonMethod.Flag == 1)
            {
                isEnableMenu = true;
            }
            CommonMethod.Flag = 0;
            return navBarGroup.Visible;
        }

        public void LoadLegalEntity()
        {
            if (!string.IsNullOrEmpty(TransactionPeriod))
            {
                bool hasHome = HasFormInMDI(typeof(frmLegalEntityView).Name);
                if (!hasHome)
                {
                    ShowWaitDialog();
                    frmLegalEntityView frmLegalEntity = new frmLegalEntityView();
                    frmLegalEntity.MdiParent = this;
                    frmLegalEntity.Show();
                    CloseWaitDialog();
                }
            }
            else
            {
                //if (XtraMessageBox.Show("Transaction Period is not created. Do you want to create the Transaction Period ?", "Acme.erp", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                if (XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.AccountingPeriod.ACCOUNTING_PERIOD_NOT_CREATED), this.GetMessage(MessageCatalog.Common.COMMON_ACMEERP_TITLE), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    LoadAccountPeriod();
                }
            }
        }

        private void LoadInstitutepreferenceDetails()
        {
            frmInstiPrefernce frmInstiprefer = new frmInstiPrefernce();
            frmInstiprefer.ShowDialog();
        }

        private void LoadRefreshBalance()
        {
            bool hasHome = HasFormInMDI(typeof(frmBalanceRefresh).Name);
            CloseFormInMDI(typeof(frmBalanceRefresh).Name);
            frmBalanceRefresh frmBalRefresh = new frmBalanceRefresh();
            frmBalRefresh.ShowDialog();
        }

        private bool isHeadOfficeLedgersExists()
        {
            ResultArgs resultArgs = new ResultArgs();
            bool isExists = false;

            using (ImportMasterSystem masterSystem = new ImportMasterSystem())
            {
                resultArgs = masterSystem.FetchBrachHeadOfficeLedgers();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    isExists = true;
                }
            }
            return isExists;
        }

        private void SetTransPeriod()
        {
            if (!string.IsNullOrEmpty(TransactionPeriod))
            {
                this.Text = RecentProject; // +" (" + TransactionPeriod + ")";
            }
        }

        /// <summary>
        /// On 30/08/2021, To assign auditor details
        /// </summary>
        private void AssignAuditorUserDetails()
        {
            using (UserRoleSystem usersystem = new UserRoleSystem())
            {
                resultArgs = usersystem.FetchAuditorUserDetailsByName(this.LoginUser.DefaultAuditorUserName);

                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    AppSetting.DefaultAuditorUserId = UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][usersystem.AppSchema.User.USER_IDColumn.ColumnName].ToString());
                    AppSetting.DefaultAuditorRoleId = UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["ROLE_ID"].ToString());
                }
            }
        }

        public void LoadFDInvestment()
        {
            bool hasHome = HasFormInMDI(typeof(frmFDAccountView).Name);
            CloseFormInMDI(typeof(frmFDAccountView).Name);
            ShowWaitDialog();
            frmFDAccountView frmAccountView = new frmFDAccountView(FDTypes.IN);
            frmAccountView.MdiParent = this;
            frmAccountView.Show();
            CloseWaitDialog();

        }

        public void LoadFDReInvestment()
        {
            bool hasHome1 = HasFormInMDI(typeof(frmRenewalView).Name);
            CloseFormInMDI(typeof(frmRenewalView).Name);
            ShowWaitDialog();
            frmRenewalView frmRenewal = new frmRenewalView(FDTypes.RIN);
            frmRenewal.MdiParent = this;
            frmRenewal.Show();
            CloseWaitDialog();

        }

        private void visiblePageHeaders(bool isTrue)
        {
            pceLoggedUser.Visible = isTrue;
            // lblPeriod.Visible = isTrue;
            //lblCurrentPeriod.Visible = isTrue;
        }


        #endregion

        #region Payroll Methods

        private void SetPayRollMenu()
        {
            // 28.10.2021 to disable the rights
            // chinna - 27.07.2021 to disable based on user (if admin user alone to can validate other wise leave it
            if (this.LoginUser.IsFullRightsReservedUser)
            {
                if (!PayrollSubMenu.SetPayRollCaption())
                {
                    glkTransPeriod.Visible = false;
                    //this.Text = "No Payroll exist!...";
                    this.Text = this.GetMessage(MessageCatalog.Payroll.ACCPMain.PAYROLL_NOPAYROLL_EXISTS_INFO);
                    //nbgDefinition.Visible = nbg1Allotement.Visible = nbgStaff.Visible = nbg1Reports.Visible =
                    //nbgDefinition.Visible = nbg1Allotement.Visible = nbg1Reports.Visible =
                    //nbiDeletePayroll.Visible = nbiMapProjectsforPayroll.Visible = nbiLock.Visible = nbiOpenPayroll.Visible = nbiReprocess.Visible = false;
                    //nbgGateway.Visible = nbiCreatePayroll.Visible =true;

                    nbgDefinition.Visible = nbiCreatePayroll.Visible = nbiStaffDetails.Visible = nbiPayrollComponent.Visible = nbiPayrollGroup.Visible = nbiLoan.Visible = true;
                    nbg1Allotement.Visible = nbiLoanmanagement.Visible = true;
                    //nbgGateway.Visible = nbiOpenPayroll.Visible = nbiDeletePayroll.Visible = nbiLock.Visible = nbiMapProcesstypeLedgers.Visible = nbiMapProjectsforPayroll.Visible = nbi1ComponentAllocation.Visible = nbi1Payroll.Visible = true; // Lock
                    nbgGateway.Visible = nbiOpenPayroll.Visible = nbiDeletePayroll.Visible = nbiMapProcesstypeLedgers.Visible = nbiMapProjectsforPayroll.Visible = nbi1ComponentAllocation.Visible = nbi1Payroll.Visible = true;
                    nbiOpenPayroll.Enabled = false;
                    nbiDeletePayroll.Enabled = false;
                    nbiLock.Enabled = false;
                    nbiStaffDetails.Enabled = false;
                    nbi1ComponentAllocation.Enabled = false;
                    nbiCustomizeStaffOrder.Enabled = false;
                    nbgDefinition.Expanded = nbgGateway.Expanded = nbg1Allotement.Expanded = true;
                    if (SettingProperty.PayrollFinanceEnabled == false)
                    {
                        this.nbiMapProjectsforPayroll.Visible = false;
                        this.nbiMapProcesstypeLedgers.Visible = false;
                    }
                    //CloseAllFormInMDI();

                }
                else
                {
                    nbgDefinition.Visible = nbiCreatePayroll.Visible = nbiStaffDetails.Visible = nbiPayrollComponent.Visible = nbiPayrollGroup.Visible = nbiLoan.Visible = true;
                    nbg1Allotement.Visible = nbiLoanmanagement.Visible = true;
                    nbgGateway.Visible = nbiOpenPayroll.Visible = nbiDeletePayroll.Visible = nbiMapProcesstypeLedgers.Visible = nbiMapProjectsforPayroll.Visible = nbi1ComponentAllocation.Visible = nbi1Payroll.Visible = true;
                    //nbgGateway.Visible = nbiOpenPayroll.Visible = nbiDeletePayroll.Visible = nbiLock.Visible = nbiMapProcesstypeLedgers.Visible = nbiMapProjectsforPayroll.Visible = nbi1ComponentAllocation.Visible = nbi1Payroll.Visible = true; -- Lock
                    nbiOpenPayroll.Enabled = true;
                    nbiDeletePayroll.Enabled = true;
                    // nbiLock.Enabled = true; // Lock
                    nbiStaffDetails.Enabled = true;
                    nbgDefinition.Expanded = nbgGateway.Expanded = nbg1Allotement.Expanded = true;
                    nbi1ComponentAllocation.Enabled = true;
                    nbiCustomizeStaffOrder.Enabled = true;
                    //nbgGateway.Visible = nbgDefinition.Visible = nbg1Allotement.Visible = nbgStaff.Visible =
                    //nbgGateway.Visible = nbgDefinition.Visible = nbg1Allotement.Visible =
                    // nbiDeletePayroll.Visible = nbiMapProjectsforPayroll.Visible = nbiLock.Visible = nbiOpenPayroll.Visible = true;  // nbiReprocess.Visible =
                    //nbgGateway.Expanded = nbgDefinition.Expanded = nbg1Allotement.Expanded = nbgStaff.Expanded = true;// nbg1Reports.Visible = nbg1Reports.Expanded = true;
                    //nbgGateway.Expanded = nbgDefinition.Expanded = nbg1Allotement.Expanded = true;// nbg1Reports.Visible = nbg1Reports.Expanded = true;  
                    if (SettingProperty.PayrollFinanceEnabled == false)
                    {
                        this.nbiMapProjectsforPayroll.Visible = false;
                        this.nbiMapProcesstypeLedgers.Visible = false;
                    }
                }
            }
        }

        private void CloseForm(string FormName)
        {
            foreach (Form frmActive in this.MdiChildren)
            {
                if (frmActive.Name.ToLower() == FormName.ToLower())
                    frmActive.Close();
            }

        }

        private void CloseAllFormInMDI()
        {
            foreach (Form frmActive in this.MdiChildren)
            {
                if (frmActive.Name.ToLower() != typeof(PAYROLL.Modules.Payroll_app.frmPayrollview).Name.ToLower())
                {
                    frmActive.Close();
                }
            }
        }

        private void EnablePayrollMenu()
        {
            if (PayrollSubMenu.GetLockStatus())
            {
                //nbiLock.Caption = "Unloc&k";
                nbiLock.Caption = this.GetMessage(MessageCatalog.Payroll.ACCPMain.PAYROLL_UNLOCK_CAPTION);
                //nbgDefinition.Visible = nbg1Allotement.Visible = nbgStaff.Visible = false;

                //nbgDefinition.Visible = nbg1Allotement.Visible = false; // Lock chinna
                // nbiDeletePayroll.Visible = nbiReprocess.Visible = false; // Lock chinna

                //this.Text += " is Locked";
                this.Text += this.GetMessage(MessageCatalog.Payroll.ACCPMain.PAYROLL_ISLOCK_CAPTION);

                glkTransPeriod.Visible = false;

                CloseAllFormInMDI();

            }
            else
            {
                //nbiLock.Caption = "Loc&k";
                nbiLock.Caption = this.GetMessage(MessageCatalog.Payroll.ACCPMain.PAYROLL_LOCK_CAPTION);
                //nbgDefinition.Visible = nbg1Allotement.Visible = nbgStaff.Visible = true;
                nbgDefinition.Visible = nbg1Allotement.Visible = true;
                nbiDeletePayroll.Visible = true; // nbiReprocess.Visible =
                this.Text = PayrollSubMenu.PAYROLL_MONTH;
                glkTransPeriod.Visible = false;
            }

        }

        //Gateway
        private void LoadCreatePayroll()
        {
            PAYROLL.Modules.Payroll_app.frmCreatePayroll createpayroll = new PAYROLL.Modules.Payroll_app.frmCreatePayroll();
            DialogResult diaresult = createpayroll.ShowDialog();
            if (diaresult == System.Windows.Forms.DialogResult.OK)
            {
                CloseAllPayrollForms();

                PAYROLL.Modules.Payroll_app.frmPayrollview PayView = new PAYROLL.Modules.Payroll_app.frmPayrollview();
                PayView.MdiParent = this;
                PayView.Show();
            }

            PayrollSubMenu.SetRecentPayRoll();
            SetPayRollMenu();

            //On 26/06/2018, to avoid groupid duplication in payrollstaffgroup  ----------------------------------------
            //PayrollSubMenu.UpdatePrstaffGroupByPayrollId();
            //----------------------------------------------------------------------------------------------------------

            //this.Text = (!string.IsNullOrEmpty(PayrollSubMenu.PAYROLL_MONTH)) ? "Payroll for " + PayrollSubMenu.PAYROLL_MONTH : "No Payroll exists";
            this.Text = (!string.IsNullOrEmpty(PayrollSubMenu.PAYROLL_MONTH)) ? this.GetMessage(MessageCatalog.Payroll.ACCPMain.PAYROLL_MONTH_FOR_INFO) + PayrollSubMenu.PAYROLL_MONTH : this.GetMessage(MessageCatalog.Payroll.ACCPMain.PAYROLL_NOPAYROLL_EXISTS_INFO);
            glkTransPeriod.Visible = false;


            //On 07/10/2021, Attach Current PayRoll name
            EnablePayrollMenu();
            if (PayrollSubMenu.PAYROLL_Id > 0)
            {
                dpAcme.Text = "Payroll -" + PayrollSubMenu.PAYROLL_MONTH;
            }
        }
        private void LoadOpenPayroll()
        {
            bool hasHome = HasFormInMDI(typeof(PAYROLL.Modules.Payroll_app.frmPayrollview).Name);
            //CloseFormInMDI(typeof(PAYROLL.Modules.Payroll_app.frmPayrollview).Name);
            PAYROLL.Modules.Payroll_app.frmMain frmmmain = new PAYROLL.Modules.Payroll_app.frmMain();
            PAYROLL.Modules.Payroll_app.frmOpenPayroll OpenPayroll = new PAYROLL.Modules.Payroll_app.frmOpenPayroll(frmmmain);
            if (OpenPayroll.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CloseAllPayrollForms();
            }
            //CloseForm(typeof(PAYROLL.Modules.Payroll_app.frmPayrollview).Name);
            PAYROLL.Modules.Payroll_app.frmPayrollview PayView = new PAYROLL.Modules.Payroll_app.frmPayrollview();
            PayView.MdiParent = this;
            PayView.Show();
            //lblPayrollMonth.Text = SettingProperty.PayrollMonth;
            //lblcurrPayroll.Text = "Payroll for ";
            EnablePayrollMenu();
            //this.Text = (!string.IsNullOrEmpty(PayrollSubMenu.PAYROLL_MONTH)) ? "Payroll for " + PayrollSubMenu.PAYROLL_MONTH : "No Payroll exists";
            this.Text = (!string.IsNullOrEmpty(PayrollSubMenu.PAYROLL_MONTH)) ? this.GetMessage(MessageCatalog.Payroll.ACCPMain.PAYROLL_MONTH_FOR_INFO) + PayrollSubMenu.PAYROLL_MONTH : this.GetMessage(MessageCatalog.Payroll.ACCPMain.PAYROLL_NOPAYROLL_EXISTS_INFO);
            //On 15/05/2019, Attach Current PayRoll name
            if (PayrollSubMenu.PAYROLL_Id > 0)
            {
                dpAcme.Text = "Payroll -" + PayrollSubMenu.PAYROLL_MONTH;
            }
            glkTransPeriod.Visible = false;

        }
        //private void ReprocessPayroll()
        //{
        //    bool hasHome = HasFormInMDI(typeof(PAYROLL.Modules.Payroll_app.frmPayrollview).Name);
        //    CloseFormInMDI(typeof(PAYROLL.Modules.Payroll_app.frmPayrollview).Name);
        //    PAYROLL.Modules.Payroll_app.frmPayrollview PayView = new PAYROLL.Modules.Payroll_app.frmPayrollview();
        //    PayView.MdiParent = this;
        //    //lblCaption.Text = PayView.Text;
        //    PayView.Show();
        //    PAYROLL.Modules.Payroll_app.frmReprocess process = new PAYROLL.Modules.Payroll_app.frmReprocess(PayrollSubMenu.PAYROLL_Id);
        //    process.ShowDialog();
        //}
        private void DeletePayroll()
        {
            bool Rtn = PayrollSubMenu.DeletePayroll();
            if (Rtn)
            {
                if (!PayrollSubMenu.SetPayRollCaption())
                {
                    SetPayRollMenu();
                }
                //this.Text = (!string.IsNullOrEmpty(PayrollSubMenu.PAYROLL_MONTH)) ? "Payroll for " + PayrollSubMenu.PAYROLL_MONTH : "No Payroll exists";
                this.Text = (!string.IsNullOrEmpty(PayrollSubMenu.PAYROLL_MONTH)) ? this.GetMessage(MessageCatalog.Payroll.ACCPMain.PAYROLL_MONTH_FOR_INFO) + PayrollSubMenu.PAYROLL_MONTH : this.GetMessage(MessageCatalog.Payroll.ACCPMain.PAYROLL_NOPAYROLL_EXISTS_INFO);

                //On 15/05/2019, Attach Current PayRoll name
                if (PayrollSubMenu.PAYROLL_Id > 0)
                {
                    dpAcme.Text = "Payroll -" + PayrollSubMenu.PAYROLL_MONTH;
                }

                CloseAllPayrollForms();

                glkTransPeriod.Visible = false;
            }
        }

        private void LockUnlockPayroll()
        {
            bool hasHome = HasFormInMDI(typeof(PAYROLL.Modules.Payroll_app.frmPayrollview).Name);
            CloseFormInMDI(typeof(PAYROLL.Modules.Payroll_app.frmPayrollview).Name);
            PAYROLL.Modules.Payroll_app.frmPayrollview PayView = new PAYROLL.Modules.Payroll_app.frmPayrollview();
            PayView.MdiParent = this;
            //lblCaption.Text = PayView.Text;
            PayView.Show();
            PAYROLL.Modules.Payroll_app.frmLockPayroll LockPayroll = new PAYROLL.Modules.Payroll_app.frmLockPayroll();
            LockPayroll.ShowDialog();
            EnablePayrollMenu();
            //this.Text = (!string.IsNullOrEmpty(PayrollSubMenu.PAYROLL_MONTH)) ? "Payroll for " + PayrollSubMenu.PAYROLL_MONTH : "No Payroll exists";
            this.Text = (!string.IsNullOrEmpty(PayrollSubMenu.PAYROLL_MONTH)) ? this.GetMessage(MessageCatalog.Payroll.ACCPMain.PAYROLL_MONTH_FOR_INFO) + PayrollSubMenu.PAYROLL_MONTH : this.GetMessage(MessageCatalog.Payroll.ACCPMain.PAYROLL_NOPAYROLL_EXISTS_INFO);
            glkTransPeriod.Visible = false;
        }

        //Definition
        private void LoadStaffDetails()
        {
            if (nbiStaffDetails.Enabled)
            {
                this.ShowWaitDialog("Loading");
                bool hasHome = HasFormInMDI(typeof(PAYROLL.Modules.Payroll_app.frmStaffView).Name);
                if (!hasHome)
                {
                    PAYROLL.Modules.Payroll_app.frmStaffView Staff = new PAYROLL.Modules.Payroll_app.frmStaffView();
                    Staff.MdiParent = this;
                    Staff.Show();
                    //lblCaption.Text = Staff.Text;
                }
                this.CloseWaitDialog();
            }
        }
        private void LoadPayrollGroup()
        {
            this.ShowWaitDialog("Loading");
            bool hasHome = HasFormInMDI(typeof(PAYROLL.Modules.Payroll_app.frmPayrollGroupView).Name);
            if (!hasHome)
            {
                PAYROLL.Modules.Payroll_app.frmPayrollGroupView paygrp = new PAYROLL.Modules.Payroll_app.frmPayrollGroupView();
                paygrp.MdiParent = this;
                //lblCaption.Text = paygrp.Text;
                paygrp.Show();
            }
            this.CloseWaitDialog();
        }

        private void LoadPayrollDepartment()
        {
            this.ShowWaitDialog("Loading");
            bool hasHome = HasFormInMDI(typeof(PAYROLL.Modules.Payroll_app.frmPayrollDepartmentView).Name);
            if (!hasHome)
            {
                PAYROLL.Modules.Payroll_app.frmPayrollDepartmentView paydepartment = new PAYROLL.Modules.Payroll_app.frmPayrollDepartmentView();
                paydepartment.MdiParent = this;
                paydepartment.Show();
            }
            this.CloseWaitDialog();
        }

        private void LoadPayrollWorkLocation()
        {
            this.ShowWaitDialog("Loading");
            bool hasHome = HasFormInMDI(typeof(PAYROLL.Modules.Payroll_app.frmPayrollWorkLocationView).Name);
            if (!hasHome)
            {
                PAYROLL.Modules.Payroll_app.frmPayrollWorkLocationView payworklocation = new PAYROLL.Modules.Payroll_app.frmPayrollWorkLocationView();
                payworklocation.MdiParent = this;
                payworklocation.Show();
            }
            this.CloseWaitDialog();
        }

        private void LoadStaffNameTitle()
        {
            this.ShowWaitDialog("Loading");
            bool hasHome = HasFormInMDI(typeof(PAYROLL.Modules.Payroll_app.frmStaffNameTitleView).Name);
            if (!hasHome)
            {
                PAYROLL.Modules.Payroll_app.frmStaffNameTitleView staffnametitle = new PAYROLL.Modules.Payroll_app.frmStaffNameTitleView();
                staffnametitle.MdiParent = this;
                staffnametitle.Show();
            }
            this.CloseWaitDialog();
        }

        private void Loadloan()
        {
            bool hasHome = HasFormInMDI(typeof(PAYROLL.Modules.Payroll_app.frmLoanView).Name);
            if (!hasHome)
            {
                PAYROLL.Modules.Payroll_app.frmLoanView LoanView = new PAYROLL.Modules.Payroll_app.frmLoanView();
                LoanView.MdiParent = this;
                LoanView.Show();
                //lblCaption.Text = LoanView.Text;
            }
        }
        private void LoadPayrollComponent()
        {
            this.ShowWaitDialog("Loading");
            bool hasHome = HasFormInMDI(typeof(PAYROLL.Modules.Payroll_app.frmComponentView).Name);
            if (!hasHome)
            {
                PAYROLL.Modules.Payroll_app.frmComponentView componentview = new PAYROLL.Modules.Payroll_app.frmComponentView();
                componentview.MdiParent = this;
                //lblCaption.Text = componentview.Text;
                componentview.Show();
            }
            this.CloseWaitDialog();
        }
        private void LoadMapStaffProject()
        {
            PAYROLL.Modules.Payroll_app.frmMapProjectStaff MapProjectStaff = new PAYROLL.Modules.Payroll_app.frmMapProjectStaff();
            MapProjectStaff.ShowDialog();
        }
        private void LoadMapProcessTypeLedgers()
        {
            PAYROLL.Modules.Payroll_app.frmMapProcessLedgers MapProjectStaff = new PAYROLL.Modules.Payroll_app.frmMapProcessLedgers();
            MapProjectStaff.ShowDialog();
        }
        private void LoadPayrollProjects()
        {
            PAYROLL.Modules.Payroll_app.frmMapProjectPayroll MapProjectpayroll = new PAYROLL.Modules.Payroll_app.frmMapProjectPayroll();
            MapProjectpayroll.ShowDialog();
        }
        //Allotment
        private void LoadLoanManagement()
        {
            bool hasHome = HasFormInMDI(typeof(PAYROLL.Modules.Payroll_app.frmLoanManagementView).Name);
            if (!hasHome)
            {
                PAYROLL.Modules.Payroll_app.frmLoanManagementView LoanManagement = new PAYROLL.Modules.Payroll_app.frmLoanManagementView();
                LoanManagement.MdiParent = this;
                LoanManagement.Show();
                //lblCaption.Text = LoanManagement.Text;
            }
        }
        private void LoadGroupAllocation()
        {
            bool hasHome = HasFormInMDI(typeof(PAYROLL.Modules.Payroll_app.frmGroupAllocation).Name);
            if (!hasHome)
            {
                PAYROLL.Modules.Payroll_app.frmGroupAllocation Groupallocation = new PAYROLL.Modules.Payroll_app.frmGroupAllocation();
                Groupallocation.MdiParent = this;
                Groupallocation.Show();
                //lblCaption.Text = LoanManagement.Text;
            }
        }
        private void LoadOrderComponents()
        {
            PAYROLL.Modules.Payroll_app.frmOrderComponents orderComponents = new PAYROLL.Modules.Payroll_app.frmOrderComponents();
            orderComponents.ShowDialog();
        }
        private void LoadMapComponentAllocation()
        {
            PAYROLL.Modules.Payroll_app.frmMapComponenttoGroups mapComponenttoGroups = new PAYROLL.Modules.Payroll_app.frmMapComponenttoGroups();
            mapComponenttoGroups.ShowDialog();
        }
        private void LoadProcessPayroll()
        {
            PAYROLL.Modules.Payroll_app.frmProcessPayroll processpayroll = new PAYROLL.Modules.Payroll_app.frmProcessPayroll();
            processpayroll.ShowDialog();
            diares = processpayroll.drs;
        }
        private void LoadPayrollView()
        {
            this.ShowWaitDialog();
            CloseForm(typeof(PAYROLL.Modules.Payroll_app.frmPayrollBrowseView).Name);
            PAYROLL.Modules.Payroll_app.frmPayrollBrowseView PayrollView = new PAYROLL.Modules.Payroll_app.frmPayrollBrowseView();
            PayrollView.MdiParent = this;
            PayrollView.Show();
            this.CloseWaitDialog();
        }
        private void LoadPayrollPostpayment()
        {
            bool hasHome = HasFormInMDI(typeof(PAYROLL.Modules.Payroll_app.frmPostVoucherview).Name);
            if (!hasHome)
            {
                PAYROLL.Modules.Payroll_app.frmPostVoucherview PostPaymentView = new PAYROLL.Modules.Payroll_app.frmPostVoucherview();
                PostPaymentView.MdiParent = this;
                PostPaymentView.Show();
                //lblCaption.Text = LoanManagement.Text;
            }
        }

        #endregion

        #region Payroll Events
        /// <summary>
        /// To create payroll
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiCreatePayroll_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //PAYROLL.Modules.Payroll_app.frmCreatePayroll createpayroll = new PAYROLL.Modules.Payroll_app.frmCreatePayroll();
            //createpayroll.ShowDialog();
            //PayrollSubMenu.SetRecentPayRoll();
            //SetPayRollMenu();
            //this.Text = (!string.IsNullOrEmpty(PayrollSubMenu.PAYROLL_MONTH)) ? "Payroll for " + PayrollSubMenu.PAYROLL_MONTH : "No Payroll exists";
            //glkTransPeriod.Visible = false;
            LoadCreatePayroll();
        }

        private void nbiOpenPayroll_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //bool hasHome = HasFormInMDI(typeof(PAYROLL.Modules.Payroll_app.frmPayrollview).Name);
            //CloseFormInMDI(typeof(PAYROLL.Modules.Payroll_app.frmPayrollview).Name);
            //PAYROLL.Modules.Payroll_app.frmMain frmmmain = new PAYROLL.Modules.Payroll_app.frmMain();
            //PAYROLL.Modules.Payroll_app.frmOpenPayroll OpenPayroll = new PAYROLL.Modules.Payroll_app.frmOpenPayroll(frmmmain);
            //OpenPayroll.UpdateHeld += new EventHandler(OnUpdateHeld);
            //OpenPayroll.ShowDialog();
            //PAYROLL.Modules.Payroll_app.frmPayrollview PayView = new PAYROLL.Modules.Payroll_app.frmPayrollview();
            //PayView.MdiParent = this;
            ////lblCaption.Text = PayView.Text;
            //PayView.Show();
            ////lblPayrollMonth.Text = SettingProperty.PayrollMonth;
            ////lblcurrPayroll.Text = "Payroll for ";
            //EnablePayrollMenu();
            //this.Text = (!string.IsNullOrEmpty(PayrollSubMenu.PAYROLL_MONTH)) ? "Payroll for " + PayrollSubMenu.PAYROLL_MONTH : "No Payroll exists";
            //glkTransPeriod.Visible = false;
            LoadOpenPayroll();
        }

        private void nbiReprocess_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(PAYROLL.Modules.Payroll_app.frmPayrollview).Name);
            CloseFormInMDI(typeof(PAYROLL.Modules.Payroll_app.frmPayrollview).Name);
            PAYROLL.Modules.Payroll_app.frmPayrollview PayView = new PAYROLL.Modules.Payroll_app.frmPayrollview();
            PayView.MdiParent = this;
            //lblCaption.Text = PayView.Text;
            PayView.Show();
            PAYROLL.Modules.Payroll_app.frmReprocess process = new PAYROLL.Modules.Payroll_app.frmReprocess(PayrollSubMenu.PAYROLL_Id);
            process.ShowDialog();
        }

        private void nbiDeletePayroll_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            DeletePayroll();
        }

        private void nbiLock_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //bool hasHome = HasFormInMDI(typeof(PAYROLL.Modules.Payroll_app.frmPayrollview).Name);
            //CloseFormInMDI(typeof(PAYROLL.Modules.Payroll_app.frmPayrollview).Name);
            //PAYROLL.Modules.Payroll_app.frmPayrollview PayView = new PAYROLL.Modules.Payroll_app.frmPayrollview();
            //PayView.MdiParent = this;
            ////lblCaption.Text = PayView.Text;
            //PayView.Show();
            //PAYROLL.Modules.Payroll_app.frmLockPayroll LockPayroll = new PAYROLL.Modules.Payroll_app.frmLockPayroll();
            //LockPayroll.ShowDialog();
            //EnablePayrollMenu();
            //this.Text = (!string.IsNullOrEmpty(PayrollSubMenu.PAYROLL_MONTH)) ? "Payroll for " + PayrollSubMenu.PAYROLL_MONTH : "No Payroll exists";
            //glkTransPeriod.Visible = false;
            LockUnlockPayroll();
        }

        private void nbiPayrollGroup_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //bool hasHome = HasFormInMDI(typeof(PAYROLL.Modules.Payroll_app.frmPayrollGroupView).Name);
            //if (!hasHome)
            //{
            //    PAYROLL.Modules.Payroll_app.frmPayrollGroupView paygrp = new PAYROLL.Modules.Payroll_app.frmPayrollGroupView();
            //    paygrp.MdiParent = this;
            //    //lblCaption.Text = paygrp.Text;
            //    paygrp.Show();
            //}
            LoadPayrollGroup();
        }

        private void nbiLoan_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //bool hasHome = HasFormInMDI(typeof(PAYROLL.Modules.Payroll_app.frmLoanView).Name);
            //if (!hasHome)
            //{
            //    PAYROLL.Modules.Payroll_app.frmLoanView LoanView = new PAYROLL.Modules.Payroll_app.frmLoanView();
            //    LoanView.MdiParent = this;
            //    LoanView.Show();
            //    //lblCaption.Text = LoanView.Text;
            //}
            Loadloan();
        }

        private void nbiPayrollComponent_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //bool hasHome = HasFormInMDI(typeof(PAYROLL.Modules.Payroll_app.frmComponentView).Name);
            //if (!hasHome)
            //{
            //    PAYROLL.Modules.Payroll_app.frmComponentView componentview = new PAYROLL.Modules.Payroll_app.frmComponentView();
            //    componentview.MdiParent = this;
            //    //lblCaption.Text = componentview.Text;
            //    componentview.Show();

            //}
            LoadPayrollComponent();
        }

        private void nbiStaffDetails_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //bool hasHome = HasFormInMDI(typeof(PAYROLL.Modules.Payroll_app.frmStaffView).Name);
            //if (!hasHome)
            //{
            //    PAYROLL.Modules.Payroll_app.frmStaffView Staff = new PAYROLL.Modules.Payroll_app.frmStaffView();
            //    Staff.MdiParent = this;
            //    Staff.Show();
            //    //lblCaption.Text = Staff.Text;
            //}
            LoadStaffDetails();
        }

        private void nbiMapProjectStaff_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadMapStaffProject();
        }

        private void nbi1LoanMgt_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //bool hasHome = HasFormInMDI(typeof(PAYROLL.Modules.Payroll_app.frmLoanManagementView).Name);
            //if (!hasHome)
            //{
            //    CloseForm(typeof(PAYROLL.Modules.Payroll_app.frmLoanManagementView).Name);
            //    PAYROLL.Modules.Payroll_app.frmLoanManagementView LoanManagement = new PAYROLL.Modules.Payroll_app.frmLoanManagementView();
            //    LoanManagement.MdiParent = this;
            //    LoanManagement.Show();
            //    //lblCaption.Text = LoanManagement.Text;
            //}
            LoadLoanManagement();
        }

        private void nbi1GrpAllocation_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //bool hasHome = HasFormInMDI(typeof(PAYROLL.Modules.Payroll_app.frmGroupAllocation).Name);
            //if (!hasHome)
            //{
            //    CloseForm(typeof(PAYROLL.Modules.Payroll_app.frmGroupAllocation).Name);
            //    PAYROLL.Modules.Payroll_app.frmGroupAllocation Groupallocation = new PAYROLL.Modules.Payroll_app.frmGroupAllocation();
            //    Groupallocation.MdiParent = this;
            //    Groupallocation.Show();
            //}
            LoadGroupAllocation();
        }

        private void nbi1ComponentAllocation_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //PAYROLL.Modules.Payroll_app.frmMapComponenttoGroups MapcomponenttoGroups = new PAYROLL.Modules.Payroll_app.frmMapComponenttoGroups();
            //MapcomponenttoGroups.ShowDialog();
            LoadMapComponentAllocation();
        }

        private void nbiProcessPayroll_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadProcessPayroll();
            if (diares == DialogResult.OK)
            {
                LoadPayrollView();
            }
        }

        private void nbi1Payroll_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //    CloseForm(typeof(PAYROLL.Modules.Payroll_app.frmPayrollBrowseView).Name);
            //    PAYROLL.Modules.Payroll_app.frmPayrollBrowseView PayrollView = new PAYROLL.Modules.Payroll_app.frmPayrollBrowseView();
            //    PayrollView.MdiParent = this;
            //    PayrollView.Show();
            //lblCaption.Text = PayrollView.Text;
            LoadPayrollView();
        }

        private void nbi1PayRegister_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //PAYROLL.Modules.Payroll.frmPRRptPayReg objRptPayReg = new PAYROLL.Modules.Payroll.frmPRRptPayReg(0, PayrollSubMenu.PAYROLL_Id, PayrollSubMenu.PAYROLL_MONTH);
            //objRptPayReg.ShowDialog();
        }

        private void nbi1LoanLedger_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //PAYROLL.Modules.Payroll.frmPRRptPayReg objRptPayReg = new PAYROLL.Modules.Payroll.frmPRRptPayReg(3, PayrollSubMenu.PAYROLL_Id, PayrollSubMenu.PAYROLL_MONTH);
            //objRptPayReg.ShowDialog();
        }

        private void nbi1PaySlip_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            PAYROLL.Modules.Payroll_app.frmPaySlipViewer frmpayslip = new PAYROLL.Modules.Payroll_app.frmPaySlipViewer();
            frmpayslip.ShowDialog();
        }

        private void nbi1CustomizeReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //PAYROLL.Modules.Payroll.frmCustomizeReport paySlipViewer = new PAYROLL.Modules.Payroll.frmCustomizeReport(); //change form name
            //try
            //{
            //    paySlipViewer.ShowDialog();
            //}
            //catch
            //{

            //}
        }

        private void nbi1AbstractPayroll_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //PAYROLL.Modules.Payroll.frmPRRptPayReg objRptPayReg = new PAYROLL.Modules.Payroll.frmPRRptPayReg(1, PayrollSubMenu.PAYROLL_Id, PayrollSubMenu.PAYROLL_MONTH);
            //objRptPayReg.ShowDialog();
        }

        private void nbi1AbstractComponent_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //PAYROLL.Modules.Payroll.frmPRRptPayReg objRptPayReg = new PAYROLL.Modules.Payroll.frmPRRptPayReg(2, PayrollSubMenu.PAYROLL_Id, PayrollSubMenu.PAYROLL_MONTH);
            //objRptPayReg.ShowDialog();
        }

        private void nbi1WagesReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //PAYROLL.Modules.Payroll.frmPRRptPayReg objRptPayReg = new PAYROLL.Modules.Payroll.frmPRRptPayReg(6, PayrollSubMenu.PAYROLL_Id, PayrollSubMenu.PAYROLL_MONTH);
            //objRptPayReg.ShowDialog();
        }

        private void nbiOrderComponets_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            PAYROLL.Modules.Payroll_app.frmOrderComponents orderComponents = new PAYROLL.Modules.Payroll_app.frmOrderComponents();
            orderComponents.ShowDialog();
        }
        private void nbiMapProjectsforPayroll_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadPayrollProjects();
        }

        private void nbiMapProcesstypeLedgers_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadMapProcessTypeLedgers();
        }

        private void nbiPayrollPostpyament_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadPayrollPostpayment();
        }

        #endregion

        #region LicensevalidationMethods

        private void ValidateLicensewithCurrentdate()
        {
            //On 24/05/2017, In few license kyes LicenseKeyYearTo is empty, if so, we fix YearFrom as YearTo
            if (this.UtilityMember.DateSet.ToDate(SettingProperty.Current.LicenseKeyYearTo, false).Year < 2000)
            {
                SettingProperty.Current.LicenseKeyYearTo = SettingProperty.Current.YearFrom;
            }

            DateTime LicenseDateFrom = this.UtilityMember.DateSet.ToDate(SettingProperty.Current.LicenseKeyYearTo, false).AddDays(-30);
            string DateFrom = LicenseDateFrom.ToString();
            string DiffDate = (this.UtilityMember.DateSet.ToDate(SettingProperty.Current.LicenseKeyYearTo, false) - DateTime.Now).Days.ToString();
            lblAlert.Text = string.Empty;
            SettingProperty.Current.IsLicenseExpired = false;
            //Will altert if license model =1 (YES)
            if (SettingProperty.Current.IsLicenseModel == 1)
            {
                if (DateTime.Now.Date > this.UtilityMember.DateSet.ToDate(SettingProperty.Current.LicenseKeyYearTo, false))
                {
                    //lblAlert.Text = "License expired";
                    lblAlert.Text = this.GetMessage(MessageCatalog.Common.COMMON_LICENSE_PEROID_EXPIRED_INFO);
                    SettingProperty.Current.IsLicenseExpired = true;
                }
                else if (CommonMethod.ValidateLicensePeriod(DateTime.Now.Date, DateFrom, SettingProperty.Current.LicenseKeyYearTo))
                {
                    //lblAlert.Text = "License expires in " + DiffDate + " days";
                    if (this.UtilityMember.NumberSet.ToInteger(DiffDate) <= 30) //For one month
                    {
                        lblAlert.Text = this.GetMessage(MessageCatalog.Common.COMMON_LICENSE_PEROID_EXPIRESIN_INFO) + string.Empty + DiffDate + " " + this.GetMessage(MessageCatalog.Common.COMMON_LICENSE_PEROID_EXPIRESIN_DAYS_INFO);
                    }
                }
            }
            lblAlert.Left = glkTransPeriod.Left - (lblAlert.Width + 10);
        }

        #endregion

        private void bbiCheckUpdates_ItemClick(object sender, ItemClickEventArgs e)
        {
            GetLastestUpdater();
        }

        private void GetLastestUpdater()
        {
            //bool automaticdownlaod1 = (repositoryItemPicLatestVersion.Visibility == BarItemVisibility.Always);
            if (!SettingProperty.Current.IsLicenseExpired)
            {
                frmAcMEERPUpdater updater = new frmAcMEERPUpdater(AutomaticUpdate);
                updater.ShowDialog();
                if (updater.DialogResult == DialogResult.OK)
                {
                    SettingProperty.Is_Application_Logout = true;
                    Application.Exit();
                }
            }
            else
            {
                this.ShowMessageBox("Your License key is expired!. You can not update latest version of Acme.erp. Please contact Bosco Soft Team");
            }
        }

        private void bbiPayrollReport_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetVisibleConfiguration = true;
            SetVisibleDate = false;
            SetVisibleProject = false;
            SetVisibleCloseAllTabs = SetCloseAllTabsVisibility;
            SettingProperty.ReportModuleId = (int)ReportModule.Payroll;
            LoadReport();
        }

        private void nbiAuditLockType_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadAuditTypeView();
        }

        private void LoadAuditTypeView()
        {
            bool hasHome = HasFormInMDI(typeof(frmAuditLockTypeView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmAuditLockTypeView frmAuditView = new frmAuditLockTypeView();
                frmAuditView.MdiParent = this;
                frmAuditView.Show();
            }
            CloseWaitDialog();
        }

        private void nbiAuditLockTrans_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadAuditLockTransactions();
        }

        private void LoadAuditLockTransactions()
        {
            bool hasHome = HasFormInMDI(typeof(frmAuditLockTransView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmAuditLockTransView frmAuditView = new frmAuditLockTransView();
                frmAuditView.MdiParent = this;
                frmAuditView.Show();
            }
            CloseWaitDialog();
        }

        private void FetchDateDuration(int ProjectID, DateTime dtTransDate)
        {
            try
            {
                resultArgs = this.GetAuditVoucherLockedDetails(ProjectID, dtTransDate);
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    DataTable dtLockDetails = resultArgs.DataSource.Table;
                    using (AuditLockTransSystem AuditSystem = new AuditLockTransSystem())
                    {
                        TransactionLockFromDate = this.UtilityMember.DateSet.ToDate(dtLockDetails.Rows[0][AuditSystem.AppSchema.AuditLockTransType.DATE_FROMColumn.ColumnName].ToString(), false);
                        TransactionLockToDate = this.UtilityMember.DateSet.ToDate(dtLockDetails.Rows[0][AuditSystem.AppSchema.AuditLockTransType.DATE_TOColumn.ColumnName].ToString(), false);
                    }
                }
                else
                {
                    TransactionLockFromDate = TransactionLockToDate = DateTime.MinValue;
                }

                //On 07/02/2024, To lock vouchers based on defined grace days in portal
                if (this.AppSetting.IS_SDB_INM && this.AppSetting.VoucherGraceDays > 0)
                {
                    bool rtn = (this.AppSetting.GraceLockDateFrom <= dtTransDate) && (dtTransDate <= this.AppSetting.GraceLockDateTo);
                    if (rtn)
                    {
                        TransactionLockFromDate = this.AppSetting.GraceLockDateFrom;
                        TransactionLockToDate = this.AppSetting.GraceLockDateTo;
                    }
                    //LockToDate = DateSet.ToDate(Result.DataSource.Table.Rows[0][AuditSystem.AppSchema.AuditLockTransType.DATE_TOColumn.ColumnName].ToString(), false);
                    //LockProjectName = Result.DataSource.Table.Rows[0][AuditSystem.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                }

            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private bool IsLockedTransaction(DateTime dtVoucherDate)
        {
            bool isSuccess = false;
            try
            {
                if (TransactionLockToDate != DateTime.MinValue)
                {
                    if (dtVoucherDate <= TransactionLockToDate)
                    {
                        isSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
            return isSuccess;
        }

        private void glkTransPeriod_EditValueChanged(object sender, EventArgs e)
        {
            if (glkTransPeriod.EditValue != null)
            {
                UpdateTransacationPeriod(glkTransPeriod.EditValue.ToString());
                CloseAll(false);
                LoadLoginHome();
                this.EnableReceiptModule();
                EnforceReceiptModuleRightsInMainMenus();

                //On 06/03/2024, Update Approved Sign 4 and Sign 5 to existing FYs ----------------------------------------------------------
                if (AppSetting.IS_CMF_CONGREGATION && AppSetting.HeadofficeCode.ToUpper() == "CMFNED")
                {
                    this.UpdateDefaultCMFNEReportApprovedSign();
                }
                else if (AppSetting.IS_BSG_CONGREGATION && AppSetting.HeadofficeCode.ToUpper() == "BSGESP")
                {   //On 28/02/2024, Update Default Sign4 and Sign5 (Budget Approval Image) For Montfort Pune
                    this.UpdateDefaultMontfortESPReportApprovedSign();
                }
                //---------------------------------------------------------------------------------------------------------------------------
            }
        }

        private void UpdateTransacationPeriod(string AccPeriodId)
        {
            using (GlobalSetting globalSystem = new GlobalSetting())
            {
                resultArgs = globalSystem.UpdateAccountingPeriod(AccPeriodId);
                if (resultArgs.Success)
                {
                    using (AccouingPeriodSystem accountingSystem = new AccouingPeriodSystem())
                    {
                        resultArgs = accountingSystem.FetchActiveTransactionPeriod();
                        if (resultArgs.DataSource != null && resultArgs.RowsAffected > 0)
                        {
                            this.AppSetting.AccPeriodInfo = resultArgs.DataSource.Table.DefaultView;
                            ApplyRecentPrjectDetails();
                            this.SetTransacationPeriod();
                        }

                        this.AppSetting.AccPeriodInfoPrevious = null;
                        resultArgs = accountingSystem.FetchPreviousYearAC(this.AppSetting.YearFrom);
                        if (resultArgs.DataSource != null && resultArgs.RowsAffected > 0)
                        {
                            this.AppSetting.AccPeriodInfoPrevious = resultArgs.DataSource.Table.DefaultView;
                        }

                        //On 03/05/2018, reset report year from and to
                        Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry(this);
                        report.ResetReportPropertyTransYearChange();
                    }
                }
            }
        }

        private void ApplyRecentPrjectDetails()
        {
            string colVoucherDate = "VOUCHER_DATE";
            try
            {
                DateTime dtyearfrom = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
                DateTime dtbookbeginfrom = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false);
                DateTime dtYearTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
                using (AccouingPeriodSystem accountingSystem = new AccouingPeriodSystem())
                {
                    accountingSystem.YearFrom = this.AppSetting.YearFrom;
                    accountingSystem.YearTo = this.AppSetting.YearTo;
                    resultArgs = accountingSystem.FetchRecentProjectDetails(this.LoginUser.LoginUserId);
                    if (resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtProject = resultArgs.DataSource.Table;
                        foreach (DataRow dr in dtProject.Rows)
                        {
                            if (string.IsNullOrEmpty(dr[colVoucherDate].ToString()))
                            {
                                dr[colVoucherDate] = dtbookbeginfrom > dtyearfrom ? dtbookbeginfrom : dtyearfrom;
                            }
                        }
                        this.AppSetting.UserProjectInfor = resultArgs.DataSource.Table.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        private void bbiReportsMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetVisibleConfiguration = true;
            SetVisibleDate = false;
            SetVisibleProject = false;
            SetVisibleCloseAllTabs = SetCloseAllTabsVisibility;
        }

        private void nbiMapLedger_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //LoadMapLedger();
        }

        #region Asset
        private void nbiDepreciationMethod_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadDepreciationMethod();
        }

        private void nbiGroups_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadAssetGroup(FinanceModule.Asset);
        }

        //private void nbiCategories_LinkClicked(object sender, NavBarLinkEventArgs e)
        //{
        // //   LoadCategory(FinanceModule.Asset);
        //}

        private void nbiUnitOfMeasure_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadUnitOfMeasure(FinanceModule.Asset);
        }

        private void nbiLocations_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadAssetStockLocation(FinanceModule.Asset);
        }

        private void LoadAssetStockLocation(FinanceModule module)
        {
            this.CloseFormInMDI(typeof(frmLocationsView).Name);
            bool hasHome = HasFormInMDI(typeof(frmLocationsView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmLocationsView frmLocation = new frmLocationsView(module);
                frmLocation.MdiParent = this;
                frmLocation.Show();
            }
            CloseWaitDialog();
        }

        private void nbiInsuranceTypes_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadInsuranceType();
        }

        private void nbiItems_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadAssetItem();
        }

        private void nbiAssetOPBalance_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadAssetOpeningBalance();
        }

        private void nbiAssetCustodian_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadCustodian(FinanceModule.Asset);
        }

        private void nbiAssetVendor_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadVendorManufacture(VendorManufacture.Vendor, FinanceModule.Asset);
        }


        private void nbiManufacture_LinkClicked(object sender, NavBarLinkEventArgs e)
        {

            LoadVendorManufacture(VendorManufacture.Manufacture);
        }

        private void nbiUploadBranchOfficeDB_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadHOUploadDatabase();
        }

        private static void LoadHOUploadDatabase()
        {
            ACPP.Modules.Data_Utility.frmUploadBranchOfficeDBase frmUploadDatabase = new frmUploadBranchOfficeDBase();
            frmUploadDatabase.ShowDialog();
        }

        private static void LoadImportTDsMasters()
        {
            ACPP.Modules.Dsync.frmPortalUpdates frmPortalupdate = new frmPortalUpdates(PortalUpdates.ImportTDSMasters);
            frmPortalupdate.ShowDialog();
        }

        private void nbiAsset_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetVisibleConfiguration = true;
            SetVisibleDate = false;
            SetVisibleProject = false;
            SetVisibleCloseAllTabs = SetCloseAllTabsVisibility;
            SettingProperty.ReportModuleId = (int)ReportModule.FixedAsset;
            LoadReport();
        }

        private void nbiAssetSearch_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadAssetSearch();
        }

        #endregion

        #region Stock
        private void nbiStockGroup_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadStockGroup();
        }

        private void nbiStockCategory_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadCategory(FinanceModule.Stock);
        }

        private void nbiStockLocation_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadAssetStockLocation(FinanceModule.Stock);
        }

        private void nbiStockUnitMeasure_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadUnitOfMeasure(FinanceModule.Stock);
        }

        private void nbiStockItem_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadStockItem();
        }

        private void nbiStockCustodian_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadCustodian(FinanceModule.Stock);
        }

        private void nbiStockVendor_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadVendorManufacture(VendorManufacture.Vendor, FinanceModule.Stock);
        }

        private void bbiStockMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetVisibleConfiguration = true;
            SetVisibleDate = false;
            SetVisibleProject = false;
            SetVisibleCloseAllTabs = SetCloseAllTabsVisibility;
            SettingProperty.ReportModuleId = (int)ReportModule.Stock;
            LoadReport();
        }
        #endregion

        #region Asset & Stock Methods
        private void LoadAssetGroup(FinanceModule module)
        {
            this.CloseFormInMDI(typeof(frmAssetClassView).Name);
            bool hasHome = HasFormInMDI(typeof(frmAssetClassView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmAssetClassView ClassView = new frmAssetClassView(module);
                ClassView.MdiParent = this;
                ClassView.Show();
            }
            CloseWaitDialog();
        }
        private void LoadStockGroup()
        {
            this.CloseFormInMDI(typeof(frmStockGroupView).Name);
            bool hasHome = HasFormInMDI(typeof(frmStockGroupView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmStockGroupView GroupView = new frmStockGroupView();
                GroupView.MdiParent = this;
                GroupView.Show();
            }
            CloseWaitDialog();
        }
        private void LoadMapLocations()
        {
            frmMapLocation mapLocation = new frmMapLocation(RecentProjectId);
            mapLocation.ShowDialog();
        }
        private void LoadImportAssetDetails()
        {
            frmAssetImportDetails assetImportDetails = new frmAssetImportDetails();
            assetImportDetails.ShowDialog();
        }
        private void LoadFixedAssetRegister()
        {
            // this.CloseFormInMDI(typeof(frmFixedAssetRegister).Name);
            bool hasForm = HasFormInMDI(typeof(frmFixedAssetRegister).Name);
            ShowWaitDialog();
            if (!hasForm)
            {
                frmFixedAssetRegister fixedAssetRegister = new frmFixedAssetRegister();
                fixedAssetRegister.MdiParent = this;
                fixedAssetRegister.Show();
            }
            CloseWaitDialog();
        }

        private void LoadDepreciationMethod()
        {
            bool hasHome = HasFormInMDI(typeof(frmDepreciationView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmDepreciationView DepreciationView = new frmDepreciationView();
                DepreciationView.MdiParent = this;
                DepreciationView.Show();
            }
            CloseWaitDialog();
        }

        private void LoadCategory(FinanceModule module)
        {
            this.CloseFormInMDI(typeof(frmCategoriesView).Name);
            bool hasHome = HasFormInMDI(typeof(frmCategoriesView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmCategoriesView categoryView = new frmCategoriesView(module);
                categoryView.MdiParent = this;
                categoryView.Show();
            }
            CloseWaitDialog();
        }

        private void LoadUnitOfMeasure(FinanceModule module)
        {
            bool hasHome = HasFormInMDI(typeof(frmUnitofMeasureView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmUnitofMeasureView UnitOfMeasureView = new frmUnitofMeasureView(module);
                UnitOfMeasureView.MdiParent = this;
                UnitOfMeasureView.Show();
            }
            CloseWaitDialog();
        }

        private void LoadStockItem()
        {
            bool hasHome = HasFormInMDI(typeof(frmStockItemView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmStockItemView frmStockView = new frmStockItemView(FinanceModule.Stock);
                frmStockView.MdiParent = this;
                frmStockView.Show();
            }
            CloseWaitDialog();
        }

        private void LoadInsuranceType()
        {
            bool hasHome = HasFormInMDI(typeof(frmInsurancePlanView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmInsurancePlanView InsurnceType = new frmInsurancePlanView();
                InsurnceType.MdiParent = this;
                InsurnceType.Show();
            }
            CloseWaitDialog();
        }

        private void LoadAssetItem()
        {
            bool hasHome = HasFormInMDI(typeof(frmAssetItemView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmAssetItemView ItemView = new frmAssetItemView();
                ItemView.MdiParent = this;
                ItemView.Show();
            }
            CloseWaitDialog();
        }

        private void LoadAssetOpeningBalance()
        {
            frmAssetOPBalance objOPBalance = new frmAssetOPBalance();
            objOPBalance.ShowDialog();
        }
        private void LoadCustodian(FinanceModule module)
        {
            bool hasHome = HasFormInMDI(typeof(frmCustodiansView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmCustodiansView frmcustodian = new frmCustodiansView(module);
                frmcustodian.MdiParent = this;
                frmcustodian.Show();
            }
            CloseWaitDialog();
        }

        private void LoadBlockDetails(FinanceModule module)
        {
            bool hasHome = HasFormInMDI(typeof(frmBlockView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmBlockView frmBlockView = new frmBlockView(module);
                frmBlockView.MdiParent = this;
                frmBlockView.Show();
            }
            CloseWaitDialog();
        }

        private void LoadVendorManufacture(VendorManufacture vendorManufacture, FinanceModule module = 0)
        {
            this.CloseFormInMDI(typeof(frmVendorInfoView).Name);
            bool hasHome = HasFormInMDI(typeof(frmVendorInfoView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                if (vendorManufacture == VendorManufacture.Vendor)
                {
                    frmVendorInfoView frmVendor = new frmVendorInfoView("Vendor", vendorManufacture, MasterImport.Vendor, module);
                    frmVendor.MdiParent = this;
                    frmVendor.Show();
                }
                else
                {
                    frmVendorInfoView frmManufacture = new frmVendorInfoView("Manufacture", vendorManufacture, MasterImport.Manufacture, module);
                    frmManufacture.MdiParent = this;
                    frmManufacture.Show();
                }

            }
            CloseWaitDialog();
        }

        private void LoadBlockView()
        {
            bool hasHome = HasFormInMDI(typeof(frmBlockView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmBlockView frmblock = new frmBlockView();
                frmblock.MdiParent = this;
                frmblock.Show();
            }
            CloseWaitDialog();

        }
        private void LoadMapAssetLedger()
        {
            frmAssetLedgerMapping frmAsset = new frmAssetLedgerMapping();
            frmAsset.ShowDialog();
        }
        #endregion

        private void nbiSplitLedger_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadExportData();
        }

        private void LoadExportData(bool isbulkImport = false)
        {
            bool hasHome = HasFormInMDI(typeof(frmSplitProject).Name);
            CloseFormInMDI(typeof(frmSplitProject).Name);
            frmSplitProject SplitProject = new frmSplitProject(isbulkImport);
            SplitProject.ShowDialog();
        }

        private void nbiImportSplitProject_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadImportData();
        }

        private void LoadImportData(bool isbulkImport = false)
        {
            if (AppSetting.ENABLE_TRACK_RECEIPT_MODULE)
            {
                bool hasHome = HasFormInMDI(typeof(frmImportSplitedProject).Name);
                CloseFormInMDI(typeof(frmImportSplitedProject).Name);
                frmImportSplitedProject ImportSplitProject = new frmImportSplitedProject(this, isbulkImport);
                ImportSplitProject.ShowDialog();

                ReAssignSetting();

                //On 14/04/2021 Rrfresh Dashboard after Project Imoprt ----------------------------------------------
                DasboardForm().IsSelected = true;
                DasboardForm().LoadDashBoardDefaults();
                DasboardForm().IsSelected = false;
                //---------------------------------------------------------------------------------------------------
            }
        }


        #region Asset Transaction Events
        private void nbiTransferVoucher_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //  LoadTransferVoucher();
        }

        private void nbiSaleVoucher_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadSalesVoucher();
        }

        private void nbiAMCVoucher_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadAMCVoucher();
        }

        private void nbiPurchaseVoucher_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadPurchaseVoucher();
        }

        private void nbiDepreciation_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadDepreciationVoucher();
        }

        private void nbiInsurance_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadInsuranceVoucher();
        }

        private void nbiAssetReceiveVoucher_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadAssetInkindVoucher();
        }
        #endregion

        #region Asset Transaction Methods

        private void LoadTransferVoucher()
        {
            bool hasHome = HasFormInMDI(typeof(frmTransferView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmTransferView frmAssetTransferVoucher = new frmTransferView();
                frmAssetTransferVoucher.MdiParent = this;
                frmAssetTransferVoucher.Show();
            }
            CloseWaitDialog();
        }

        private void LoadSalesVoucher()
        {
            DialogResult = DialogResult.Cancel;
            if (ProjectSelection == AppSetting.DefaultAdminUserId.ToString())
            {
                frmProjectSelection frmprojSelection = new frmProjectSelection(Bosco.Utility.ProjectSelection.EnableVoucherSelectionMethod);
                frmprojSelection.SetVoucherAddCaption = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_ENTRY);
                frmprojSelection.SetVoucherViewCaption = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_VIEW);
                frmprojSelection.ShowDialog();
                DialogResult = frmprojSelection.DialogResult;
                if (frmprojSelection.ProjectName != string.Empty)
                {
                    RecentProjectId = frmprojSelection.ProjectId;
                    RecentProject = frmprojSelection.ProjectName;
                    SelectionType = frmprojSelection.SelectionTye;
                    RecentVoucherDate = frmprojSelection.RecentVoucherDate;
                    this.Text = RecentProject;
                }
            }
            if (SelectionType == 1 && DialogResult == DialogResult.OK)
            {
                bool hasHome = HasFormInMDI(typeof(frmAssetOutwardView).Name);
                ShowWaitDialog();
                if (!hasHome)
                {
                    frmAssetOutwardView frmAssetSalesVoucher = new frmAssetOutwardView(RecentProjectId, RecentVoucherDate);
                    frmAssetSalesVoucher.MdiParent = this;
                    frmAssetSalesVoucher.Show();
                }
            }
            else
            {
                if (recentProject != string.Empty && DialogResult == DialogResult.OK)
                {
                    frmAssetOutward frmAssetSalesVoucherAdd = new frmAssetOutward(RecentProjectId, RecentProject, (int)AddNewRow.NewRow, RecentVoucherDate);
                    frmAssetSalesVoucherAdd.ShowDialog();
                }
            }
            CloseWaitDialog();
        }

        private void LoadAMCVoucher()
        {
            //DialogResult = DialogResult.Cancel;
            //if (ProjectSelection == LoginUserId)
            //{
            //    frmProjectSelection frmprojSelection = new frmProjectSelection(Bosco.Utility.ProjectSelection.EnableVoucherSelectionMethod);
            //    frmprojSelection.SetVoucherAddCaption = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_ENTRY);
            //    frmprojSelection.SetVoucherViewCaption = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_VIEW);
            //    frmprojSelection.ShowDialog();
            //    DialogResult = frmprojSelection.DialogResult;
            //    if (frmprojSelection.ProjectName != string.Empty)
            //    {
            //        RecentProjectId = frmprojSelection.ProjectId;
            //        RecentProject = frmprojSelection.ProjectName;
            //        SelectionType = frmprojSelection.SelectionTye;
            //        RecentVoucherDate = frmprojSelection.RecentVoucherDate;
            //        this.Text = RecentProject;
            //    }
            //}
            //if (SelectionType == 1 && DialogResult == DialogResult.OK)
            //{
            bool hasHome = HasFormInMDI(typeof(frmAMCRenewalView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmAMCRenewalView frmAssetAMCVoucher = new frmAMCRenewalView(RecentProjectId, RecentVoucherDate);
                frmAssetAMCVoucher.MdiParent = this;
                frmAssetAMCVoucher.Show();
            }
            // }
            //else
            //{
            //    if (recentProject != string.Empty && DialogResult == DialogResult.OK)
            //    {
            //        frmAMCRenewAdd frmAssetAMCVoucherAdd = new frmAMCRenewAdd();
            //        frmAssetAMCVoucherAdd.ShowDialog();
            //    }
            //}
            CloseWaitDialog();
        }
        private void LoadUpdateAssetDetails()
        {
            frmUpdateAssetDetails updateAsset = new frmUpdateAssetDetails();
            updateAsset.ShowDialog();
        }
        private void LoadPurchaseVoucher()
        {
            DialogResult = DialogResult.Cancel;
            if (ProjectSelection == AppSetting.DefaultAdminUserId.ToString())
            {
                frmProjectSelection frmprojSelection = new frmProjectSelection(Bosco.Utility.ProjectSelection.EnableVoucherSelectionMethod);
                frmprojSelection.SetVoucherAddCaption = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_ENTRY);
                frmprojSelection.SetVoucherViewCaption = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_VIEW);
                frmprojSelection.ShowDialog();
                DialogResult = frmprojSelection.DialogResult;
                if (frmprojSelection.ProjectName != string.Empty)
                {
                    RecentProjectId = frmprojSelection.ProjectId;
                    RecentProject = frmprojSelection.ProjectName;
                    SelectionType = frmprojSelection.SelectionTye;
                    RecentVoucherDate = frmprojSelection.RecentVoucherDate;
                    this.Text = RecentProject;
                }
            }
            if (SelectionType == 1 && DialogResult == DialogResult.OK)
            {
                bool hasHome = HasFormInMDI(typeof(frmInwardView).Name);
                CloseFormInMDI(typeof(frmInwardView).Name);
                ShowWaitDialog();
                //if (!hasHome)
                //{
                frmInwardView frmAssetPurchase = new frmInwardView(RecentVoucherDate, RecentProjectId, RecentProject, AssetInOut.PU);
                frmAssetPurchase.MdiParent = this;
                frmAssetPurchase.Show();
                //}
            }
            else
            {
                if (recentProject != string.Empty && DialogResult == DialogResult.OK)
                {
                    frmInwardVoucherAdd frmAssetPurchaseVoucherAdd = new frmInwardVoucherAdd(RecentVoucherDate, RecentProjectId, RecentProject, (int)AddNewRow.NewRow, AssetInOut.PU);
                    frmAssetPurchaseVoucherAdd.ShowDialog();
                }
            }
            CloseWaitDialog();
        }

        private void LoadDepreciationVoucher()
        {
            DialogResult = DialogResult.Cancel;
            if (ProjectSelection == AppSetting.DefaultAdminUserId.ToString())
            {
                frmProjectSelection frmprojSelection = new frmProjectSelection(Bosco.Utility.ProjectSelection.EnableVoucherSelectionMethod);
                frmprojSelection.SetVoucherAddCaption = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_ENTRY);
                frmprojSelection.SetVoucherViewCaption = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_VIEW);
                frmprojSelection.ShowDialog();
                DialogResult = frmprojSelection.DialogResult;
                if (frmprojSelection.ProjectName != string.Empty)
                {
                    RecentProjectId = frmprojSelection.ProjectId;
                    RecentProject = frmprojSelection.ProjectName;
                    SelectionType = frmprojSelection.SelectionTye;
                    RecentVoucherDate = frmprojSelection.RecentVoucherDate;
                    this.Text = RecentProject;
                }
            }
            if (SelectionType == 1 && DialogResult == DialogResult.OK)
            {
                this.CloseFormInMDI(typeof(frmDepreciationViewScreen).Name);
                bool hasHome = HasFormInMDI(typeof(frmDepreciationViewScreen).Name);
                ShowWaitDialog();
                //if (!hasHome)
                //{
                frmDepreciationViewScreen Depreciation = new frmDepreciationViewScreen(0, RecentProjectId, RecentProject);
                Depreciation.MdiParent = this;
                Depreciation.Show();
                // }
            }
            else
            {
                if (recentProject != string.Empty && DialogResult == DialogResult.OK)
                {
                    frmDepreciation frmAssetDepreciation = new frmDepreciation(0, RecentProjectId, RecentProject);
                    frmAssetDepreciation.ShowDialog();
                }
            }
            CloseWaitDialog();


            //this.CloseFormInMDI(typeof(frmDepreciationViewScreen).Name);
            //bool hasHome = HasFormInMDI(typeof(frmDepreciationViewScreen).Name);
            //ShowWaitDialog();
            //if (!hasHome)
            //{
            //    frmDepreciationViewScreen Depreciation = new frmDepreciationViewScreen();
            //    Depreciation.MdiParent = this;
            //    Depreciation.Show();
            //}
            //CloseWaitDialog();
        }

        private void LoadInsuranceVoucher()
        {
            bool hasHome = HasFormInMDI(typeof(frmInsuranceView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmInsuranceView frmInsurance = new frmInsuranceView(RecentProjectId, RecentVoucherDate);
                frmInsurance.MdiParent = this;
                frmInsurance.Show();
            }
            CloseWaitDialog();
        }

        private void LoadAssetDashBoard()
        {
            bool hasHome = HasFormInMDI(typeof(frmAssetDashboard).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmAssetDashboard assetDashBoard = new frmAssetDashboard();
                assetDashBoard.MdiParent = this;
                assetDashBoard.Show();
            }
            CloseWaitDialog();
        }

        private void LoadAssetSearch()
        {
            bool hasHome = HasFormInMDI(typeof(frmAssetSearch).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmAssetSearch frmAsset = new frmAssetSearch();
                frmAsset.MdiParent = this;
                frmAsset.Show();
            }
            CloseWaitDialog();
        }

        private void LoadConfigure()
        {
            frmAssetSettings assetSetting = new frmAssetSettings();
            assetSetting.ShowDialog();
        }

        private void LoadAssetInkindVoucher()
        {
            DialogResult = DialogResult.Cancel;
            if (ProjectSelection == AppSetting.DefaultAdminUserId.ToString())
            {
                frmProjectSelection frmprojSelection = new frmProjectSelection(Bosco.Utility.ProjectSelection.EnableVoucherSelectionMethod);
                frmprojSelection.SetVoucherAddCaption = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_ENTRY);
                frmprojSelection.SetVoucherViewCaption = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_VIEW);
                frmprojSelection.ShowDialog();
                DialogResult = frmprojSelection.DialogResult;
                if (frmprojSelection.ProjectName != string.Empty)
                {
                    RecentProjectId = frmprojSelection.ProjectId;
                    RecentProject = frmprojSelection.ProjectName;
                    SelectionType = frmprojSelection.SelectionTye;
                    RecentVoucherDate = frmprojSelection.RecentVoucherDate;
                    this.Text = RecentProject;
                }
            }
            if (SelectionType == 1 && DialogResult == DialogResult.OK)
            {
                bool hasHome = HasFormInMDI(typeof(frmInwardView).Name);
                CloseFormInMDI(typeof(frmInwardView).Name);
                ShowWaitDialog();
                //if (!hasHome)
                //{
                frmInwardView ReceiveVoucher = new frmInwardView(RecentProject, RecentProjectId, RecentVoucherDate, AssetInOut.IK);
                ReceiveVoucher.MdiParent = this;
                ReceiveVoucher.Show();
                //}
            }
            else
            {
                if (recentProject != string.Empty && DialogResult == DialogResult.OK)
                {
                    frmInwardVoucherAdd frmAssetReceiveVoucherAdd = new frmInwardVoucherAdd(RecentVoucherDate, RecentProjectId, RecentProject, (int)AddNewRow.NewRow, AssetInOut.IK);
                    frmAssetReceiveVoucherAdd.ShowDialog();
                }
            }
            CloseWaitDialog();
        }
        #endregion


        private void nbiManageMultiDB_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadMultiDB();
        }

        private static void LoadMultiDB()
        {
            frmManageMultiDB multidb = new frmManageMultiDB();
            multidb.ShowDialog();
        }

        #region Stock Events

        private void nbiMapStockLedgers_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            frmStockLedgerMapping frmledgermapping = new frmStockLedgerMapping();
            frmledgermapping.ShowDialog();
        }

        private void nbiStockOP_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadStockOpeningBalance();
        }

        private void nbiPurchase_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadStockPurchase();
        }

        private void nbiStockReceive_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadStockPurchaseReceive();
        }

        private void nbiSale_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadStockSales();
        }

        private void nbiItemTransfer_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadStockItemTrasfer();
            //bool hasHome = HasFormInMDI(typeof(frmStockTransferView).Name);
            //ShowWaitDialog();
            //if (!hasHome)
            //{
            //    frmStockTransferView frmstocktransferview = new frmStockTransferView();
            //    frmstocktransferview.MdiParent = this;
            //    frmstocktransferview.Show();
            //}
            //CloseWaitDialog();
        }

        private void nbiItemDisposal_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            // frmItemDisposal frmItemdisposal = new frmItemDisposal();
            //frmItemdisposal.ShowDialog();
        }

        private void nbiGoodsReturn_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadPurchaseReturn();
        }

        private void nbiUtilised_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadStockUtilised();
        }

        private void bbiDisposal_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadStockDisposal();
        }

        private void nbiStockUpdation_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadStockUpdation();
        }

        private void nbiStockHome_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadStockRegister();
        }


        #endregion

        #region Stock Methods

        private void LoadStockRegister()
        {
            bool hasHome = HasFormInMDI(typeof(frmStockHomeView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmStockHomeView stockhome = new frmStockHomeView();
                stockhome.MdiParent = this;
                stockhome.Show();
            }
            CloseWaitDialog();
        }

        private static void LoadStockUpdation()
        {
            frmStockUpdation stockupdation = new frmStockUpdation();
            stockupdation.ShowDialog();
        }

        private void LoadStockDisposal()
        {
            DialogResult = DialogResult.Cancel;
            if (ProjectSelection == AppSetting.DefaultAdminUserId.ToString())
            {
                frmProjectSelection frmprojSelection = new frmProjectSelection(Bosco.Utility.ProjectSelection.EnableVoucherSelectionMethod);
                frmprojSelection.SetVoucherAddCaption = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_ENTRY);
                frmprojSelection.SetVoucherViewCaption = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_VIEW);
                frmprojSelection.ShowDialog();
                DialogResult = frmprojSelection.DialogResult;
                if (frmprojSelection.ProjectName != string.Empty)
                {
                    RecentProjectId = frmprojSelection.ProjectId;
                    RecentProject = frmprojSelection.ProjectName;
                    SelectionType = frmprojSelection.SelectionTye;
                    RecentVoucherDate = frmprojSelection.RecentVoucherDate;
                    this.Text = RecentProject;
                }
            }
            if (SelectionType == 1 && DialogResult == DialogResult.OK)
            {
                bool hasHome = HasFormInMDI(typeof(frmSoldUtilizedView).Name);
                CloseFormInMDI(typeof(frmSoldUtilizedView).Name);
                ShowWaitDialog();
                frmSoldUtilizedView frmsoldutilisedview = new frmSoldUtilizedView(recentVoucherDate, RecentProjectId, StockSalesTransType.Disposal);
                frmsoldutilisedview.MdiParent = this;
                frmsoldutilisedview.Show();
            }
            else
            {
                if (recentProject != string.Empty && DialogResult == DialogResult.OK)
                {
                    frmUtiliseOrSoldItems frmsoldutilised = new frmUtiliseOrSoldItems((int)AddNewRow.NewRow, RecentVoucherDate, RecentProjectId, RecentProject, (int)StockSalesTransType.Disposal);
                    frmsoldutilised.ShowDialog();
                }
            }
            CloseWaitDialog();
        }

        private void LoadStockUtilised()
        {
            DialogResult = DialogResult.Cancel;
            if (ProjectSelection == AppSetting.DefaultAdminUserId.ToString())
            {
                frmProjectSelection frmprojSelection = new frmProjectSelection(Bosco.Utility.ProjectSelection.EnableVoucherSelectionMethod);
                frmprojSelection.SetVoucherAddCaption = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_ENTRY);
                frmprojSelection.SetVoucherViewCaption = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_VIEW);
                frmprojSelection.ShowDialog();
                DialogResult = frmprojSelection.DialogResult;
                if (frmprojSelection.ProjectName != string.Empty)
                {
                    RecentProjectId = frmprojSelection.ProjectId;
                    RecentProject = frmprojSelection.ProjectName;
                    SelectionType = frmprojSelection.SelectionTye;
                    RecentVoucherDate = frmprojSelection.RecentVoucherDate;
                    this.Text = RecentProject;
                }
            }
            if (SelectionType == 1 && DialogResult == DialogResult.OK)
            {
                bool hasHome = HasFormInMDI(typeof(frmSoldUtilizedView).Name);
                CloseFormInMDI(typeof(frmSoldUtilizedView).Name);
                ShowWaitDialog();
                frmSoldUtilizedView frmsoldutilisedview = new frmSoldUtilizedView(recentVoucherDate, RecentProjectId, StockSalesTransType.Utilized);
                frmsoldutilisedview.MdiParent = this;
                frmsoldutilisedview.Show();
            }
            else
            {
                if (recentProject != string.Empty && DialogResult == DialogResult.OK)
                {
                    frmUtiliseOrSoldItems frmsoldutilised = new frmUtiliseOrSoldItems((int)AddNewRow.NewRow, RecentVoucherDate, RecentProjectId, RecentProject, (int)StockSalesTransType.Utilized);
                    frmsoldutilised.ShowDialog();
                }
            }
            CloseWaitDialog();
        }

        private void LoadPurchaseReturn()
        {
            DialogResult = DialogResult.Cancel;
            if (ProjectSelection == AppSetting.DefaultAdminUserId.ToString())
            {
                frmProjectSelection frmprojSelection = new frmProjectSelection(Bosco.Utility.ProjectSelection.EnableVoucherSelectionMethod);
                frmprojSelection.SetVoucherAddCaption = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_ENTRY);
                frmprojSelection.SetVoucherViewCaption = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_VIEW);
                frmprojSelection.ShowDialog();
                DialogResult = frmprojSelection.DialogResult;
                if (frmprojSelection.ProjectName != string.Empty)
                {
                    RecentProjectId = frmprojSelection.ProjectId;
                    RecentProject = frmprojSelection.ProjectName;
                    SelectionType = frmprojSelection.SelectionTye;
                    RecentVoucherDate = frmprojSelection.RecentVoucherDate;
                    this.Text = RecentProject;
                }
            }
            if (SelectionType == 1 && DialogResult == DialogResult.OK)
            {
                bool hasHome = HasFormInMDI(typeof(frmPurchaseReturnsView).Name);
                ShowWaitDialog();
                if (!hasHome)
                {
                    frmPurchaseReturnsView frmstockGoodReturnview = new frmPurchaseReturnsView(RecentProjectId, RecentVoucherDate);
                    frmstockGoodReturnview.MdiParent = this;
                    frmstockGoodReturnview.Show();
                }
            }
            else
            {
                if (recentProject != string.Empty && DialogResult == DialogResult.OK)
                {
                    frmGoodsReturnAdd GoodsReturnsAdd = new frmGoodsReturnAdd((int)AddNewRow.NewRow, recentProjectId, RecentProject, this.UtilityMember.DateSet.ToDate(recentVoucherDate, false));
                    GoodsReturnsAdd.ShowDialog();
                }
            }
            CloseWaitDialog();
        }

        private void LoadStockItemTrasfer()
        {
            DialogResult = DialogResult.Cancel;
            if (ProjectSelection == AppSetting.DefaultAdminUserId.ToString())
            {
                frmProjectSelection frmprojSelection = new frmProjectSelection(Bosco.Utility.ProjectSelection.EnableVoucherSelectionMethod);
                frmprojSelection.SetVoucherAddCaption = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_ENTRY);
                frmprojSelection.SetVoucherViewCaption = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_VIEW);
                frmprojSelection.ShowDialog();
                DialogResult = frmprojSelection.DialogResult;
                if (frmprojSelection.ProjectName != string.Empty)
                {
                    RecentProjectId = frmprojSelection.ProjectId;
                    RecentProject = frmprojSelection.ProjectName;
                    SelectionType = frmprojSelection.SelectionTye;
                    RecentVoucherDate = frmprojSelection.RecentVoucherDate;
                    this.Text = RecentProject;
                }
            }
            if (SelectionType == 1 && DialogResult == DialogResult.OK)
            {
                bool hasHome = HasFormInMDI(typeof(frmStockTransferView).Name);
                ShowWaitDialog();
                if (!hasHome)
                {
                    frmStockTransferView frmstockTransferview = new frmStockTransferView(RecentProjectId, RecentVoucherDate);
                    frmstockTransferview.MdiParent = this;
                    frmstockTransferview.Show();
                }
            }
            else
            {
                if (recentProject != string.Empty && DialogResult == DialogResult.OK)
                {
                    frmStockTransferAdd StockTransferAdd = new frmStockTransferAdd(RecentProject, this.UtilityMember.DateSet.ToDate(recentVoucherDate, false));
                    StockTransferAdd.ProjectId = RecentProjectId;
                    StockTransferAdd.ShowDialog();
                }
            }
            CloseWaitDialog();
        }

        private void LoadStockSales()
        {
            DialogResult = DialogResult.Cancel;
            if (ProjectSelection == AppSetting.DefaultAdminUserId.ToString())
            {
                frmProjectSelection frmprojSelection = new frmProjectSelection(Bosco.Utility.ProjectSelection.EnableVoucherSelectionMethod);
                frmprojSelection.SetVoucherAddCaption = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_ENTRY);
                frmprojSelection.SetVoucherViewCaption = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_VIEW);
                frmprojSelection.ShowDialog();
                DialogResult = frmprojSelection.DialogResult;
                if (frmprojSelection.ProjectName != string.Empty)
                {
                    RecentProjectId = frmprojSelection.ProjectId;
                    RecentProject = frmprojSelection.ProjectName;
                    SelectionType = frmprojSelection.SelectionTye;
                    RecentVoucherDate = frmprojSelection.RecentVoucherDate;
                    this.Text = RecentProject;
                }
            }
            if (SelectionType == 1 && DialogResult == DialogResult.OK)
            {
                bool hasHome = HasFormInMDI(typeof(frmSoldUtilizedView).Name);
                CloseFormInMDI(typeof(frmSoldUtilizedView).Name);
                ShowWaitDialog();
                frmSoldUtilizedView frmsoldutilisedview = new frmSoldUtilizedView(recentVoucherDate, RecentProjectId, StockSalesTransType.Sold);
                frmsoldutilisedview.MdiParent = this;
                frmsoldutilisedview.Show();
            }
            else
            {
                if (recentProject != string.Empty && DialogResult == DialogResult.OK)
                {
                    frmUtiliseOrSoldItems frmsoldutilised = new frmUtiliseOrSoldItems((int)AddNewRow.NewRow, RecentVoucherDate, RecentProjectId, RecentProject, (int)StockSalesTransType.Sold);
                    frmsoldutilised.ShowDialog();
                }
            }
            CloseWaitDialog();
        }

        private void LoadStockPurchase()
        {
            DialogResult = DialogResult.Cancel;
            if (ProjectSelection == AppSetting.DefaultAdminUserId.ToString())
            {
                //if (!string.IsNullOrEmpty(DefaultProject))
                //{
                frmProjectSelection frmprojSelection = new frmProjectSelection(Bosco.Utility.ProjectSelection.EnableVoucherSelectionMethod);
                frmprojSelection.SetVoucherAddCaption = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_ENTRY);
                frmprojSelection.SetVoucherViewCaption = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_VIEW);
                frmprojSelection.ShowDialog();
                DialogResult = frmprojSelection.DialogResult;
                if (frmprojSelection.ProjectName != string.Empty)
                {
                    RecentProjectId = frmprojSelection.ProjectId;
                    RecentProject = frmprojSelection.ProjectName;
                    SelectionType = frmprojSelection.SelectionTye;
                    RecentVoucherDate = frmprojSelection.RecentVoucherDate;
                    //lblProject.Text = RecentProject;// +" (" + TransactionPeriod + ")";
                    this.Text = RecentProject;
                }
            }
            if (SelectionType == 1 && DialogResult == DialogResult.OK)
            {
                this.CloseFormInMDI(typeof(frmStockPurchaseView).Name);
                bool hasHome = HasFormInMDI(typeof(frmStockPurchaseView).Name);
                CloseFormInMDI(typeof(frmSoldUtilizedView).Name);
                ShowWaitDialog();
                frmStockPurchaseView frmStockPurchase = new frmStockPurchaseView(RecentProjectId, StockPurchaseTransType.Purchase);
                frmStockPurchase.MdiParent = this;
                frmStockPurchase.Show();
            }
            else
            {
                if (recentProject != string.Empty && DialogResult == DialogResult.OK)
                {
                    frmPurchaseStockAdd frmPurchaseAdd = new frmPurchaseStockAdd((int)AddNewRow.NewRow, RecentProjectId, RecentProject, this.UtilityMember.DateSet.ToDate(RecentVoucherDate, false), (int)StockPurchaseTransType.Purchase);
                    frmPurchaseAdd.ShowDialog();
                }
            }
            CloseWaitDialog();
        }

        private void LoadStockPurchaseReceive()
        {
            DialogResult = DialogResult.Cancel;
            if (ProjectSelection == AppSetting.DefaultAdminUserId.ToString())
            {
                //if (!string.IsNullOrEmpty(DefaultProject))
                //{
                frmProjectSelection frmprojSelection = new frmProjectSelection(Bosco.Utility.ProjectSelection.EnableVoucherSelectionMethod);
                frmprojSelection.SetVoucherAddCaption = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_ENTRY);
                frmprojSelection.SetVoucherViewCaption = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_VIEW);
                frmprojSelection.ShowDialog();
                DialogResult = frmprojSelection.DialogResult;
                if (frmprojSelection.ProjectName != string.Empty)
                {
                    RecentProjectId = frmprojSelection.ProjectId;
                    RecentProject = frmprojSelection.ProjectName;
                    SelectionType = frmprojSelection.SelectionTye;
                    RecentVoucherDate = frmprojSelection.RecentVoucherDate;
                    //lblProject.Text = RecentProject;// +" (" + TransactionPeriod + ")";
                    this.Text = RecentProject;
                }
            }
            if (SelectionType == 1 && DialogResult == DialogResult.OK)
            {
                this.CloseFormInMDI(typeof(frmStockPurchaseView).Name);
                bool hasHome = HasFormInMDI(typeof(frmStockPurchaseView).Name);
                CloseFormInMDI(typeof(frmSoldUtilizedView).Name);
                ShowWaitDialog();
                frmStockPurchaseView frmStockPurchase = new frmStockPurchaseView(RecentProjectId, StockPurchaseTransType.Receive);
                frmStockPurchase.MdiParent = this;
                frmStockPurchase.Show();
            }
            else
            {
                if (recentProject != string.Empty && DialogResult == DialogResult.OK)
                {
                    frmPurchaseStockAdd frmPurchaseAdd = new frmPurchaseStockAdd((int)AddNewRow.NewRow, RecentProjectId, RecentProject, this.UtilityMember.DateSet.ToDate(RecentVoucherDate, false), (int)StockPurchaseTransType.Receive);
                    frmPurchaseAdd.ShowDialog();
                }
            }
            CloseWaitDialog();
        }

        private void LoadStockOpeningBalance()
        {
            frmStockOpeningBalance frmStockOP = new frmStockOpeningBalance();
            frmStockOP.ShowDialog();
        }

        #endregion

        //#region Validate Username and Password
        //private bool IsValidUser()
        //{
        //    bool isValidUser = true;
        //    try
        //    {
        //        if (this.AppSetting.LoginUserId.Equals("1"))
        //        {
        //            string Password = "admin";
        //            if (Password.Equals(SettingProperty.LoginPassword))
        //            {
        //                frmChangePassword frmChangePwd = new frmChangePassword(SettingProperty.LoginUserName, this.AppSetting.RoleId, true);
        //                frmChangePwd.ShowDialog();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally { }
        //    return isValidUser;
        //}
        //#endregion

        private void nbiMapAssetLedger_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadMapAssetLedger();
        }

        private void nbiBudgetAnnual_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            if (this.AppSetting.IS_DIOMYS_DIOCESE)
                LoadBudgetMysore(BudgetType.BudgetByAnnualYear);
            else
                LoadBudget(BudgetType.BudgetByAnnualYear);

        }

        private void nbiFDPostInterst_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadFdPostInterest();
        }

        private void nbiTDSMasterImport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadImportTDsMasters();
        }

        private void nbiReceiptView_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            bool hasfrmHome = HasFormInMDI(typeof(frmTransactionView).Name);
            CloseFormInMDI(typeof(frmTransactionView).Name);
            frmTransactionView frmTransaction = new frmTransactionView(RecentVoucherDate, RecentProjectId, RecentProject, (int)DefaultVoucherTypes.Receipt - 1, 1);//0-Add Mode,1-View Mode 
            frmTransaction.MdiParent = this;
            frmTransaction.Show();
        }

        private void nbiJournalView_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            bool hasfrmHome = HasFormInMDI(typeof(frmTransactionJournalView).Name);
            CloseFormInMDI(typeof(frmTransactionJournalView).Name);
            frmTransactionJournalView frmTransaction = new frmTransactionJournalView(RecentVoucherDate, RecentProjectId, RecentProject, (int)DefaultVoucherTypes.Journal - 1, SelectionType);
            frmTransaction.MdiParent = this;
            frmTransaction.Show();
        }

        private void leftMenuBar_MouseDown(object sender, MouseEventArgs e)
        {
            //Added by Carmel Raj M on August-19-2015
            //Purpose : Expand clicking on the Navigation bar itself
            if (e.Button == MouseButtons.Left)
            {
                NavBarControl navBar = sender as NavBarControl;
                NavBarHitInfo hitInfo = navBar.CalcHitInfo(new Point(e.X, e.Y));
                if (hitInfo.InGroupCaption && !hitInfo.InGroupButton)
                    hitInfo.Group.Expanded = !hitInfo.Group.Expanded;
            }
        }

        private void leftMenuBar_MouseMove(object sender, MouseEventArgs e)
        {
            //Added by Carmel Raj M on August-19-2015
            //Purpose : Making mouse cursor as hand when the mouse moves over the Navigation bar
            NavBarControl navBar = sender as NavBarControl;
            NavBarHitInfo hitInfo = navBar.CalcHitInfo(new Point(e.X, e.Y));
            if (hitInfo.InGroupCaption && !hitInfo.InGroupButton)
                Cursor = System.Windows.Forms.Cursors.Hand;
            else
                Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void nbiDownloadTemplates_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            using (frmTemplates downloadTemp = new frmTemplates())
            {
                downloadTemp.ShowDialog();
            }
        }

        private void nbiMastersImport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            using (frmExcelSupport importmaster = new frmExcelSupport("", MasterImport.Masters, FinanceModule.Stock))
            {
                importmaster.ShowDialog();
            }
        }

        private void nbiAssetImportMasters_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            using (frmExcelSupport importmaster = new frmExcelSupport("", MasterImport.Masters, FinanceModule.Asset))
            {
                importmaster.ShowDialog();
            }
        }

        //private void nbiAssetDetails_LinkClicked(object sender, NavBarLinkEventArgs e)
        //{
        //    frmStockSettings settingDetails = new frmStockSettings();
        //    settingDetails.ShowDialog();
        //}

        private void nbiStockMapLedgers_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            frmStockSettings stockSettings = new frmStockSettings();
            stockSettings.ShowDialog();
        }

        private void nbiImportAssetMasters_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            using (frmExcelSupport importmaster = new frmExcelSupport("", MasterImport.Masters, FinanceModule.Asset))
            {
                importmaster.ShowDialog();
            }
        }

        private void nbiFinnanceSettings_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //frmFinanceSetting finnanceSettings = new frmFinanceSetting();
            //finnanceSettings.ShowDialog();
            LoadFinnanceSettings();
        }

        private void nbiTDSSettings_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            TDSSettings tdsSetttings = new TDSSettings();
            tdsSetttings.ShowDialog();
        }

        //private void nbiConfigure_LinkClicked(object sender, NavBarLinkEventArgs e)
        //{
        //    frmAssetSettings assetSettings = new frmAssetSettings();
        //    assetSettings.ShowDialog();
        //}

        private void nbiBlock_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadBlockDetails(FinanceModule.Asset);
        }

        private void nbiMapLocations_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            frmMapLocation mapLocation = new frmMapLocation(RecentProjectId);
            mapLocation.ShowDialog();
        }

        private void nbiClass_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadAssetGroup(FinanceModule.Asset);
        }

        private void nbiConfigure_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            frmAssetSettings assetSetting = new frmAssetSettings();
            assetSetting.ShowDialog();
        }

        private void nbiAssetRegisters_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadFixedAssetRegister();
        }

        private void nbiImportAssets_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            frmAssetImportDetails assetImportDetails = new frmAssetImportDetails();
            assetImportDetails.ShowDialog();
        }

        private void nbiUpdateAssetDetails_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //bool hasHome = HasFormInMDI(typeof(frmUpdateAssetDetails).Name);
            //ShowWaitDialog();
            //if (!hasHome)
            //{
            //    frmUpdateAssetDetails frmUpdateAsset = new frmUpdateAssetDetails();
            //    frmUpdateAsset.MdiParent = this;
            //    frmUpdateAsset.ShowDialog();
            //}
            //CloseWaitDialog();
            frmUpdateAssetDetails updateAsset = new frmUpdateAssetDetails();
            updateAsset.ShowDialog();
        }

        private void nbiProspectsView_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(frmProspectsView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmProspectsView frmProspectView = new frmProspectsView();
                frmProspectView.MdiParent = this;
                frmProspectView.Show();
            }
            CloseWaitDialog();
        }

        private void nbiTemplates_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            frmDonorMailTemplate frmTemplate = new frmDonorMailTemplate();
            frmTemplate.ShowDialog();
        }

        private void nbiThanksgiving_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(frmDonorMailThanksgiving).Name);
            CloseFormInMDI(typeof(frmDonorMailThanksgiving).Name);
            ShowWaitDialog();
            //if (!hasHome)
            //{
            frmDonorMailThanksgiving frmThankgiving = new frmDonorMailThanksgiving();
            frmThankgiving.communicationmode = CommunicationMode.MailDesk;
            frmThankgiving.MdiParent = this;
            frmThankgiving.Show();
            //}
            CloseWaitDialog();
        }

        private void nbiAppealLetter_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(frmDonorMailAppealList).Name);
            CloseFormInMDI(typeof(frmDonorMailAppealList).Name);
            ShowWaitDialog();
            //if (!hasHome)
            //{
            frmDonorMailAppealList frmDonorList = new frmDonorMailAppealList();
            frmDonorList.communicationmode = CommunicationMode.MailDesk;
            frmDonorList.MdiParent = this;
            frmDonorList.Show();
            //}
            CloseWaitDialog();
        }

        private void nbiNewsLetter_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(frmDonorMailNewsLetter).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmDonorMailNewsLetter frmDonorLetters = new frmDonorMailNewsLetter();
                frmDonorLetters.communicationmode = CommunicationMode.MailDesk;
                frmDonorLetters.MdiParent = this;
                frmDonorLetters.Show();
            }
            CloseWaitDialog();
        }

        private void nbiSMSTemplates_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            frmDonorSMSTemplate frmSMSTemplate = new frmDonorSMSTemplate();
            frmSMSTemplate.ShowDialog();
        }

        private void nbiFeastDay_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(frmDonorMailFeast).Name);
            CloseFormInMDI(typeof(frmDonorMailFeast).Name);
            ShowWaitDialog();
            //if (!hasHome)
            //{
            frmDonorMailFeast frmFeast = new frmDonorMailFeast();
            frmFeast.communicationmode = CommunicationMode.MailDesk;
            frmFeast.MdiParent = this;
            frmFeast.Show();
            //}
            CloseWaitDialog();
        }

        private void nbiAnniversary_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(frmDonorAnniversaries).Name);
            CloseFormInMDI(typeof(frmDonorAnniversaries).Name);
            ShowWaitDialog();
            frmDonorAnniversaries frmAnniversaries = new frmDonorAnniversaries();
            frmAnniversaries.communcationMode = CommunicationMode.MailDesk;
            frmAnniversaries.MdiParent = this;
            frmAnniversaries.Show();
            CloseWaitDialog();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetVisibleConfiguration = true;
            SetVisibleDate = false;
            SetVisibleProject = false;
            SetVisibleCloseAllTabs = SetCloseAllTabsVisibility;
            SettingProperty.ReportModuleId = (int)ReportModule.NetWorking;
            LoadReport();
        }

        private void nbiSMSThanksgiving_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(frmDonorMailThanksgiving).Name);
            CloseFormInMDI(typeof(frmDonorMailThanksgiving).Name);
            ShowWaitDialog();
            //if (!hasHome)
            //{
            frmDonorMailThanksgiving frmThankgiving = new frmDonorMailThanksgiving();
            frmThankgiving.communicationmode = CommunicationMode.ContactDesk;
            frmThankgiving.MdiParent = this;
            frmThankgiving.Show();
            //}
            CloseWaitDialog();
        }

        private void nbiSMSAnniversary_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(frmDonorAnniversaries).Name);
            CloseFormInMDI(typeof(frmDonorAnniversaries).Name);
            ShowWaitDialog();
            frmDonorAnniversaries frmAnniversaries = new frmDonorAnniversaries();
            frmAnniversaries.communcationMode = CommunicationMode.ContactDesk;
            frmAnniversaries.MdiParent = this;
            frmAnniversaries.Show();
            CloseWaitDialog();
        }

        private void nbiSMSAppeal_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(frmDonorMailAppealList).Name);
            CloseFormInMDI(typeof(frmDonorMailAppealList).Name);
            ShowWaitDialog();
            //if (!hasHome)
            //{
            frmDonorMailAppealList frmDonorList = new frmDonorMailAppealList();
            frmDonorList.communicationmode = CommunicationMode.ContactDesk;
            frmDonorList.MdiParent = this;
            frmDonorList.Show();
            //}
            CloseWaitDialog();
        }

        private void nbiSMSFeast_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(frmDonorMailFeast).Name);
            CloseFormInMDI(typeof(frmDonorMailFeast).Name);
            ShowWaitDialog();
            //if (!hasHome)
            //{
            frmDonorMailFeast frmFeast = new frmDonorMailFeast();
            frmFeast.communicationmode = CommunicationMode.ContactDesk;
            frmFeast.MdiParent = this;
            frmFeast.Show();
            // }
            CloseWaitDialog();
        }

        private void nbiMembers_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadDonor();
        }

        private void bbiBranch_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmInstiPrefernce frmInstitutePre = new frmInstiPrefernce();
            frmInstitutePre.ShowDialog();
        }


        private void nbiAssetItemLedgerMapping_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            frmAssetItemLedgerMapping assetitemledgerMapping = new frmAssetItemLedgerMapping();
            assetitemledgerMapping.ShowDialog();
        }

        private void nbiMasterDonorReference_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(frmMasterDonorReferenceView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmMasterDonorReferenceView donorReferenceView = new frmMasterDonorReferenceView();
                donorReferenceView.MdiParent = this;
                donorReferenceView.Show();
            }
            CloseWaitDialog();
        }

        private void nbiRegistrationType_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(frmDonorViewRegistrationType).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmDonorViewRegistrationType donorViewRegistrationType = new frmDonorViewRegistrationType();
                donorViewRegistrationType.MdiParent = this;
                donorViewRegistrationType.Show();
            }
            CloseWaitDialog();
        }

        private void nbiInstitutionType_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(frmInstitutionTypeView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmInstitutionTypeView frmInstitutionTypeView = new frmInstitutionTypeView();
                frmInstitutionTypeView.MdiParent = this;
                frmInstitutionTypeView.Show();
            }
            CloseWaitDialog();
        }

        private void nbiDonorTitle_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(frmDonorTitleView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmDonorTitleView frmdonorTitleView = new frmDonorTitleView();
                frmdonorTitleView.MdiParent = this;
                frmdonorTitleView.Show();
            }
            CloseWaitDialog();
            frmDonorTitleAdd frmdonorTitleAdd = new frmDonorTitleAdd();

        }

        private void tmrAlert_Tick(object sender, EventArgs e)
        {
            ValidateLicensewithCurrentdate();
            //this.lblAlert.Visible = !this.lblAlert.Visible;
        }

        private void nbiReportCustomization_LinkClicked(object sender, NavBarLinkEventArgs e)
        {

        }

        private void nbiImportExportRegisters_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //frmImportRegisters frmimportexport = new frmImportRegisters();
            //frmimportexport.ShowDialog();
        }

        private void nbgImportMasters_LinkClicked(object sender, NavBarLinkEventArgs e)
        {

        }

        private void nbiExportRegisters_LinkClicked(object sender, NavBarLinkEventArgs e)
        {

        }

        private void nbiConfiguration_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            frmNetworkingSettings networkSetting = new frmNetworkingSettings();
            networkSetting.ShowDialog();
        }

        private void nbiDeleteunusedLedger_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            frmDeleteunusedLedgers frmdeleteledgers = new frmDeleteunusedLedgers();
            frmdeleteledgers.ShowDialog();
        }

        private void nbiDeleteVoucherTrans_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            frmDeleteVouchers frmvouchers = new frmDeleteVouchers();
            frmvouchers.ShowDialog();
        }

        private void nbiDeleteUnusedGroup_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            frmDeleteunusedLedgerGroups frmdelgrop = new frmDeleteunusedLedgerGroups();
            frmdelgrop.ShowDialog();
        }

        private void nbiMoveDeleteVouchers_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            frmBulkTransaction bulkTransaction = new frmBulkTransaction();
            bulkTransaction.ShowDialog();
        }

        private void nbiBCC_LinkClicked(object sender, NavBarLinkEventArgs e)
        {

        }

        private void nbiClosedFd_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(frmClosedFDView).Name);
            ShowWaitDialog();

            if (!hasHome)
            {
                frmClosedFDView frmClosedRegisters = new frmClosedFDView();
                frmClosedRegisters.MdiParent = this;
                frmClosedRegisters.Show();
            }

            CloseWaitDialog();
        }


        /// <summary>
        /// this method is used to reassing setting property after closing setting screen
        /// 
        /// example
        /// 1. FinanceSetting page (after Closing FinanceSetting Screen, project selection property should be reset)
        /// 
        /// </summary>
        public void ReAssignSetting()
        {
            ProjectSelection = this.UIAppSetting.UIProjSelection;
            nbiFDReInvestment.Visible = (this.UIAppSetting.EnableFlexiFD == "1"); //28/11/2018, to lock reinvestment feature based on setting
            nbiAuthorizeVoucher.Visible = (this.UIAppSetting.ConfirmAuthorizationVoucherEntry == 1); // On 26/07/2023, to show authorization column based on the setting
        }

        /// <summary>
        /// On 19/07/2021, to Check Budget properties in Finance Settings
        /// </summary>
        private void CheckBudgetPropertiesFinanceSettings()
        {
            bool settingchanged = false;
            using (BudgetSystem budgetsystem = new BudgetSystem())
            {
                using (UISettingSystem uisystemsetting = new UISettingSystem())
                {
                    ResultArgs resultarg = new ResultArgs();
                    //For Budget Satistics
                    if (budgetsystem.HasBudgetStatistics())
                    {
                        resultarg = uisystemsetting.SaveUISettingDetails(FinanceSetting.IncludeBudgetStatistics, "1", this.LoginUser.LoginUserId);
                        settingchanged = true;
                    }
                    //For Budget Income LEdgers
                    if (budgetsystem.HasBudgetIncomeLedger())
                    {
                        resultarg = uisystemsetting.SaveUISettingDetails(FinanceSetting.IncludeIncomeLedgersInBudget, "1", this.LoginUser.LoginUserId);
                        settingchanged = true;
                    }

                    if (settingchanged)
                    {
                        ApplySettingAgain();
                    }
                }
            }
        }




        #region Background worker process (Communicating with Portal)
        private void hyperlnkLatestUpdater_Click(object sender, EventArgs e)
        {
            GetLastestUpdater();
        }

        /// <summary>
        /// This is asynchronous method, this will communicate acmerp portal and do the following taks
        /// 
        /// 1. Check Internet connection
        /// 2. Get latest version details from potal and verify local version, if there is difference, it will prompt to update
        /// 3. Upload Recent Database to portal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bworkerWithPoral_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            bool MismatchedLicenseKey = true;
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                //Check Internet connection for FTP and WEB Service, do BG work based on this
                if (!e.Cancel)
                {
                    ResultArgs resultarg = new ResultArgs();

                    //28/01/2025, Get portal date and timing ----------------------------------------------
                    if (AppSetting.IS_SDB_INM)
                    {
                        // and get vocuher grace days details 
                        resultArgs = UpdateVoucherGraceDetailsFromAcmeerpPortal(true);
                        worker.ReportProgress(0, resultArgs); //To show grace details
                    }

                    //On 19/07/2021, to Check Budget properties in Finance Settings
                    CheckBudgetPropertiesFinanceSettings();


                    //FTP BG Work*******************************************************************************************
                    if (this.CheckForInternetConnection())
                    {
                        MismatchedLicenseKey = IsMismatchedLicenseKeyWithPortalProjects();

                        //1. Get new featues list from portal
                        using (AcMEERPFTP ftp = new AcMEERPFTP())
                        {
                            resultarg = ftp.GetLatestUpdaterFeaturesFromPortal();
                            if (resultarg.Success)
                            {
                                NewFeaturesInPortal = resultarg.ReturnValue.ToString();
                            }
                            else
                            {
                                string msg = "Background process :: Could not get new features list from Acme.erp portal :: " + resultarg.Message;
                                //AcMELog.WriteLog(msg);
                            }
                        }

                        if (CheckAcmeerpWebserviceRunning())
                        {
                            //2. Check Latest Updater is avilable in portal, If so prompt to update it
                            //alert avilability of last version 
                            //if (server version and local db product version) or (db product version or local file version)
                            //Some times we may get latest version DB from client but we may have old setup (old exe, dll files)
                            DataSyncService.DataSynchronizerClient objservice = new DataSyncService.DataSynchronizerClient();
                            string serverversion = string.Empty;

                            try
                            {
                                serverversion = objservice.GetAcmeERPProductVersion();
                            }
                            catch (Exception err)
                            {
                                //AcMELog.WriteLog("Error in Background process :: Getting Latest version from portal " + err.Message); 
                            }

                            if (!string.IsNullOrEmpty(serverversion) && serverversion != this.AppSetting.AcmeerpVersionFromDB)
                            {
                                resultarg.Success = true;
                                Application.DoEvents();
                                worker.ReportProgress(1, resultarg); //To visible latest update link
                            }

                            //3. Upload Recent Database to portal (This sould be based on lincense key validation)
                            //For temp we provided only for SDB congregations
                            //On 09/05/2020, to autobackuptoportal based on settings
                            //if (this.AppSetting.IS_SDB_CONGREGATION || this.AppSetting.IS_SCCGSB_CONGREGATION || this.AppSetting.IS_BOSCOS_DEMO)
                            //On 07/02/2024, To lock auto backup to portal if splited previous year db
                            if (AppSetting.AutomaticDBBackupToPortal == 1 && !AppSetting.IsSplitPreviousYearAcmeerpDB)
                            {
                                //Upload current db backup to portal
                                //For automatic upload database, current database is getting uploaded during the development
                                //so lock upload database logic if system is local  development systmn
                                if (isDevelopmentSystem == false && MismatchedLicenseKey == false)
                                {
                                    resultarg.Success = true;
                                    UploadLatestDBToPortal();
                                }
                            }
                            //---------------------------------------------------------------------------------------------------
                        }
                        else
                        {
                            string msg = "Background process :: Acme.erp Web Service is not running";
                            //AcMELog.WriteLog(msg);
                        }
                    }
                    else
                    {
                        string msg = "Background process :: Check Internet " + this.GetMessage(MessageCatalog.Master.DataUtilityForms.CHECK_INTERNET_FTP_CONNECTION);
                        //AcMELog.WriteLog(msg);
                    }
                    //--FTP BG work ************************************************************************************************************************


                    //WEB SERVICE BG WORK *******************************************************************************************************************
                    if (CheckAcmeerpWebserviceRunning())
                    {
                        resultarg.Success = true;
                        DataSyncService.DataSynchronizerClient objservice = new DataSyncService.DataSynchronizerClient();
                        if (isDevelopmentSystem == false && MismatchedLicenseKey == false)
                        {
                            //Get Voucher locks from portal and update it to voucher master_lock_trans.
                            GetVoucherLocksFromPortal(); // chinna
                            resultarg.Success = true;

                            //4.On 10/01/2019, Update branch office logged history to Acmeerp portal ----------------------------
                            try
                            {
                                string loggeddate = this.UtilityMember.DateSet.GetDateToday(true);
                                string branchremarks = GetMACIPAddress(); // GetMACAddress();
                                DateTime loggeddatetime = this.UtilityMember.DateSet.ToDate(loggeddate, true);
                                bool logged = objservice.UpdateBranchLoggedHistory(this.AppSetting.BranchOfficeCode, this.AppSetting.HeadofficeCode, this.AppSetting.InstituteName,
                                                     this.AppSetting.HeadOfficeName, this.AppSetting.Location, loggeddatetime, this.AppSetting.LicenseKeyNumber, branchremarks);
                            }
                            catch (Exception err)
                            {
                                //AcMELog.WriteLog("Error in Background process :: updating branch logged details " + err.Message);
                            }

                            //On 09/12/2020, By default, will be active becusae of the internet. If internet is avilable, will validate it with Portal Branch
                            try
                            {
                                Is_Branch_Active = objservice.IsBranchExists(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode);
                            }
                            catch (FaultException<DataSyncService.AcMeServiceException> ex)
                            {
                                string message = ex.Detail.Message;
                                if (message.Contains("Branch Office is not available"))
                                {
                                    Is_Branch_Active = false;
                                    AcMELog.WriteLog("Branch is locked");
                                }
                            }
                            catch (Exception errBranchActive)
                            {
                                //AcMELog.WriteLog("Error in Background process :: updating branch logged details " + err.Message);
                            }

                            if (!Is_Branch_Active)
                            {
                                LockLicenseKey();
                            }
                            //------------------------------------------------------------------------------------------------------------------------------------

                            //5. On 26/06/2019--------------------------------------------------------------------------------------------------------------------
                            //While loginging in get portal messages in background process
                            try
                            {
                                if (isDevelopmentSystem == false)
                                {
                                    frmLoginDashboard dashboard = DasboardForm();
                                    if (dashboard != null)
                                    {
                                        ResultArgs resultMessage = dashboard.UpdateMessagesFromPortalByBackgroundProcess();
                                        Application.DoEvents();
                                        worker.ReportProgress(2, resultMessage); //To load message from portal
                                    }
                                }
                            }
                            catch (Exception err)
                            {
                                //AcMELog.WriteLog("Error in Background process :: Getting portal message " + err.Message);
                            }
                        }

                        //6. On 18/04/2022, To check and get Receipt Module rights for SDBINM
                        if (AppSetting.IS_SDB_INM && Is_Branch_Active)
                        {
                            //string rtnModuleRights = string.Empty;
                            //try
                            //{
                            //    string ipaddress = base.GetIPAddress();
                            //    string macaddress = base.GetMACAddress();
                            //    rtnModuleRights = objservice.GetLocalCommunityReceiptModuleRightStatus(AppSetting.LicenseKeyNumber, AppSetting.BaseLicenseHeadOfficeCode,
                            //                    AppSetting.BaseLicenseBranchCode, AppSetting.BaseLicenseLocation, ipaddress, macaddress);
                            //}
                            //catch (Exception err)
                            //{
                            //    rtnModuleRights = "Error in Background process :: GetLocalCommunityReceiptModuleRightStatus " + err.Message;
                            //}
                            //finally
                            //{
                            //    if (!String.IsNullOrEmpty(rtnModuleRights) && this.AppSetting.BaseLicenseBranchStatus== LCBranchModuleStatus.Approved)
                            //    {
                            //        using (UISettingSystem uisystemsetting = new UISettingSystem())
                            //        {
                            //            uisystemsetting.BaseAcmeerpSaveUISettingDetails(FinanceSetting.BranchReceiptModuleStatus, ((Int32)LCBranchModuleStatus.Disabled).ToString(), this.LoginUser.LoginUserId);
                            //            AcMELog.WriteLog("Receipt module is disabled in Acme.erp portal, So locked Receipt module by automatically " + rtnModuleRights);
                            //            ResultArgs resultRCRights = new ResultArgs();
                            //            resultRCRights.Success = true;
                            //            Application.DoEvents();
                            //            worker.ReportProgress(3, resultRCRights); //To enforce receipt module automatically
                            //        }
                            //    }
                            //}
                        }
                    }
                    else
                    {
                        string msg = "Background process :: Acme.erp Web Service is not running";
                    }
                    //--WEB SERVICE BG WORK******************************************************************************************************************
                    e.Result = resultarg;
                }

            }

            //as on 12/12/2020, net is not available, check licese key
            if (Is_Branch_Active)
            {
                Is_Branch_Active = !IsLicenseKeyLocked();
            }
        }

        /// <summary>
        /// On completion of Background asynchronous processs, do the following logic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bworkerWithPoral_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ResultArgs resultarg = e.Result as ResultArgs;

            //On 08/07/2024, For SDBINM, If receipt module enabled, allow to use acme.erp
            /*if (this.AppSetting.IS_SDB_INM && this.AppSetting.FINAL_RECEIPT_MODULE_STATUS != LCBranchModuleStatus.Approved)
            {
                Is_Branch_Active = false;
            }*/

            //On 09/12/2020, If Branch is De-activated in Portal, We should give proper message and close the application -----------
            //By default, will be active becusae of the internet. If internet is avilable, will validate it with Portal Branch
            if (!Is_Branch_Active)
            {
                glkTransPeriod.Visible = false;

                //Hide all main menus
                for (int i = 0; i < bar1.ItemLinks.Count; i++)
                {
                    BarItemLink baritem = bar1.ItemLinks[i];
                    bar1.ItemLinks[i].Visible = false;
                }

                bbiDateSelection.Visibility = bbiProject.Visibility = bbiConfiguration.Visibility = BarItemVisibility.Never;
                dpAcme.Visibility = DockVisibility.Hidden;

                //Close All Active Forms
                foreach (Form frmActive in this.MdiChildren)
                {
                    if (frmActive.Name != typeof(frmLoginDashboard).Name)
                    {
                        frmActive.Close();
                    }
                }

                this.ShowMessageBoxError("Your Branch is locked in Acmeerp Portal, Contact Acme.erp Support Team");
            }
            //------------------------------------------------------------------------------------------------------------

            //alert avilability of last version 
            //if (server version and local db product version) or (db product version or local file version)
            //Some times we may get latest version DB from client but we may have old setup (old exe, dll files)
            if (bbiLatestVersion.Visibility == BarItemVisibility.Never)
            {
                if (SettingProperty.Current.AcmeerpVersionFromDB != SettingProperty.Current.AcmeerpVersionFromEXE)
                {
                    ShowLatestVersion();


                }
            }

            //Refresh Home page alert message about last uploaded on
            //On 09/05/2020, to autobackuptoportal based on settings
            //if (this.AppSetting.IS_SDB_CONGREGATION || this.AppSetting.IS_SCCGSB_CONGREGATION || this.AppSetting.IS_BOSCOS_DEMO)
            if (AppSetting.AutomaticDBBackupToPortal == 1)
            {
                foreach (frmFinanceBase frm in this.MdiChildren)
                {
                    if (frm.Name == typeof(frmLoginDashboard).Name)
                    {
                        ((frmLoginDashboard)frm).CheckAlerts();
                        break;
                    }
                }
            }

            //On 28/06/2019, while backgroup process running, enable, after locking backup.restore------
            BackupNew.Enabled = navRestoreData.Enabled = nbiDBBackup.Enabled = nbiDBRestore.Enabled = true;
            //-------------------------------------------------------------------------------------------

            // 13.09.2022... To get the Localhost or IP chinna
            string dbServer = GetAppConfigDBServerName();
            if (dbServer == "localhost")
            {
                navRestoreData.Enabled = true;
            }
            else
            {
                navRestoreData.Enabled = false;
            }
        }

        /// <summary>
        /// Update UI changes in between back ground asynchronous process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bworkerWithPoral_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ResultArgs resultarg = e.UserState as ResultArgs;

            //On 28/01/2025 - To set alert message saying that vouchers are locked
            if (resultarg.Success && e.ProgressPercentage == 0) //0. show grace alert 
            {
                AttachAlertForGraceDaysForVoucherEntry();
            }
            //-------------------------------------------------------------------------------------

            //1. make visible latest update link
            tileCtlLatestVersion.Visible = false;
            if (resultarg.Success && e.ProgressPercentage == 1) //1. Show link
            {
                ShowLatestVersion();
            }

            //On 26/06/2019---------------------------------------------------------------------------------------
            //While loginging in get portal messages in background process
            if (resultarg.Success && e.ProgressPercentage == 2)
            {
                frmLoginDashboard dashboard = DasboardForm();
                if (dashboard != null)
                {
                    dashboard.LoadPortalMessages();
                }
            }

            //On 20/04/2022, if Receipt Module rights are disabled in Portal, if still active in local application, 
            //Enforce to disable receipt module rights
            if (this.AppSetting.IS_SDB_INM && e.ProgressPercentage == 3)
            {
                //this.AppSetting.BaseLicenseBranchStatus = LCBranchModuleStatus.Disabled;
                //this.AppSetting.FINAL_RECEIPT_MODULE_STATUS = LCBranchModuleStatus.Disabled;

                //Enforce receipt module rights
                //EnforceReceiptModuleRightsInMainMenus();
            }
            //----------------------------------------------------------------------------------------------------
        }

        /// <summary>
        /// to show latest version alert
        /// </summary>
        private void ShowLatestVersion()
        {
            bbiLatestVersion.EditValue = Properties.Resources.LatestVersion;
            bbiLatestVersion.Visibility = BarItemVisibility.Always;
            bbiCheckUpdates.Visibility = BarItemVisibility.Never;
            AutomaticUpdate = true;

            //On 08/02/2018------------------------------------------------------------------------------------------------------
            //Show new featues is list in tooltip
            SuperToolTip sTooltip1 = new SuperToolTip();
            // Create a tooltip item that represents a header.
            ToolTipTitleItem titleItem1 = new ToolTipTitleItem();
            titleItem1.Text = "Latest Updater is available in acmeerp.org portal";
            // Add the tooltip items to the SuperTooltip.
            sTooltip1.Items.Add(titleItem1);

            //Add Featues List
            if (!string.IsNullOrEmpty(NewFeaturesInPortal))
            {
                // Create a tooltip item that represents the SuperTooltip's contents.
                ToolTipItem item1 = new ToolTipItem();
                item1.Text = NewFeaturesInPortal;
                sTooltip1.Items.Add(item1);
            }

            ToolTipController tooltipcontroler = new ToolTipController();
            tooltipcontroler.AutoPopDelay = 15000;
            sTooltip1.FixedTooltipWidth = false;
            sTooltip1.DistanceBetweenItems = 5;
            this.barManager1.ToolTipController = tooltipcontroler;
            bbiLatestVersion.SuperTip = sTooltip1;
            //-----------------------------------------------------------------------------------------------------------------

            //(barEditItem2.Edit as RepositoryItemPictureEdit).SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;

            /*tileCtlLatestVersion.Top = (pceLoggedUser.Top + pceLoggedUser.Height);
            tileCtlLatestVersion.Left = this.Width - (tileCtlLatestVersion.Width + 25);
            tltItemLatestVersion.Frames.Clear();
            tltItemLatestVersion.Frames.Add(GetAlertEmptyTileItem("Latest Version is available", "Latest Version is available", ""));
            tileCtlLatestVersion.Visible = true;
            tileCtlLatestVersion.BringToFront();*/

            //On 15/04/2021, To have latest version features local and show in notepad
            UpdateLatestVersionLocally();
        }


        /// <summary>
        /// This method is used to upload current datatbase to portal in background worker
        /// 
        /// Time being, this feature is enabled only for SDB
        /// </summary>
        private void UploadLatestDBToPortal()
        {
            ResultArgs resultargs = new ResultArgs();
            try
            {
                if (!AppSetting.IsSplitPreviousYearAcmeerpDB)
                {
                    string LastDBUploadedOn = this.AppSetting.DBUploadedOn;
                    string automaticbackpath = Path.Combine(SettingProperty.ApplicationStartUpPath, "AcpBackUp");
                    var directory = new DirectoryInfo(automaticbackpath);
                    string searchlatestdb = string.Empty;
                    //Get latest Database backup file, if multi db, take particular db's latest one
                    //11/10/2018
                    if (AppSetting.AccesstoMultiDB == (int)YesNo.Yes)
                    {
                        searchlatestdb = SettingProperty.ActiveDatabaseAliasName;
                    }
                    else
                    {
                        searchlatestdb = "Default";
                    }
                    searchlatestdb += "_*";

                    if (directory.Exists && directory.GetFiles(searchlatestdb).Length > 0)
                    {
                        //Get latest Database backup file, if multi db, take particular db's latest one
                        var myFile = directory.GetFiles(searchlatestdb).OrderByDescending(f => f.LastWriteTime).First();
                        string dbbackfilepath = Path.Combine(automaticbackpath, myFile.ToString());

                        //1. Get Server Date
                        using (AcMEERPFTP acmeerpftp = new AcMEERPFTP())
                        {
                            resultargs = acmeerpftp.GetAcMEERPServerDateTime();
                        }
                        if (resultargs.Success)
                        {
                            string serverdatetime = resultargs.ReturnValue.ToString(); //objservice.GetCurrentServerDate(); chinna

                            //07/06/2021 Check is Empty Database
                            bool isEmptyDB = false;
                            using (ProjectSystem projectsys = new ProjectSystem())
                            {
                                resultargs = projectsys.FetchProjects();
                                if (resultargs.Success && resultargs.DataSource.Table != null && resultargs.DataSource.Table.Rows.Count == 0)
                                {
                                    isEmptyDB = true;
                                }
                            }

                            DataSyncService.DataSynchronizerClient objservice = new DataSyncService.DataSynchronizerClient();

                            if (this.UtilityMember.DateSet.ToDate(LastDBUploadedOn) != this.UtilityMember.DateSet.ToDate(serverdatetime) && !isEmptyDB)
                            {
                                //CAll backup uploader, this will compress and upload current dtatbase to portal
                                using (BackupAndRestore dbBackupUpload = new BackupAndRestore())
                                {
                                    //2. Upload Current Datatabse to portal
                                    resultargs = dbBackupUpload.UploadDatabaseToPortal(dbbackfilepath);

                                    //Update Date and time
                                    if (resultargs.Success)
                                    {
                                        //2. Update current date and time
                                        using (UISetting uisetting = new UISetting())
                                        {
                                            resultargs = uisetting.SaveSettingDetails(Setting.DBUploadedOn.ToString(), serverdatetime, ADMIN_USER_DEFAULT_ID);
                                            if (resultargs.Success)
                                            {
                                                this.AppSetting.DBUploadedOn = serverdatetime;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                resultargs.Message = "Automatic upload Database " + err.Message;
            }

            if (!resultargs.Success)
            {
                AcMELog.WriteLog("Background process :: Automatic upload DB " + resultargs.Message);
            }
        }

        /// <summary>
        /// This method is used to get Voucher locking detials from portal and 
        /// update into master_lock_trans.
        /// 
        /// This portal locking is high priority, delete lock detials if it alreay exists in local for a project
        /// 
        /// </summary>
        private void GetVoucherLocksFromPortal()
        {
            ResultArgs resultargs = new ResultArgs();
            try
            {
                DataTable dtPortalLockVouchers = new DataTable();
                DataSyncService.DataSynchronizerClient objservice = new DataSyncService.DataSynchronizerClient(); //objservice.GetLockVoucher(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode);
                dtPortalLockVouchers = objservice.GetLockVoucher(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode);  //GetLockVouchersFromPortal1(); //objservice.GetLockVoucher("sdbinm", "sdbinminmdbc");  //GetLockVouchersFromPortal1();
                resultargs = DeleteLockVouchersUnUsedLocal(dtPortalLockVouchers);
                if (resultargs.Success)
                {
                    using (AuditLockTransSystem AuditTranSystem = new AuditLockTransSystem())
                    {
                        foreach (DataRow drPortallockvoucher in dtPortalLockVouchers.Rows)
                        {
                            AuditTranSystem.ProjectId = AuditTranSystem.GetProjectId(drPortallockvoucher["PROJECT"].ToString());  // this.UtilityMember.NumberSet.ToInteger(drPortallockvoucher["PROJECT_ID"].ToString());
                            AuditTranSystem.LockType = drPortallockvoucher["LOCK_TYPE"].ToString();
                            AuditTranSystem.LockTypeId = AuditTranSystem.GetAuditTypeIdByLockType();

                            //1. Check lock type in local, insert it if not exists in local
                            if (AuditTranSystem.LockTypeId == 0)
                            {
                                resultargs = AuditTranSystem.SaveAuditType();
                                if (resultargs.Success || this.UtilityMember.NumberSet.ToInteger(resultargs.RowUniqueId.ToString()) > 0)
                                {
                                    AuditTranSystem.LockTypeId = this.UtilityMember.NumberSet.ToInteger(resultargs.RowUniqueId.ToString());
                                }
                            }
                            else if (AuditTranSystem.LockTypeId > 0)
                            {
                                resultargs.Success = true;
                            }

                            //2. Voucher lock details : Get list of vouchers locking periods from portal and update it.
                            if (resultargs.Success && AuditTranSystem.LockTypeId > 0 && AuditTranSystem.ProjectId > 0)
                            {
                                if (resultargs.Success)
                                {
                                    SimpleEncrypt.SimpleEncDec objsimpleencryption = new SimpleEncrypt.SimpleEncDec();
                                    AuditTranSystem.DateFrom = this.UtilityMember.DateSet.ToDate(drPortallockvoucher["DATE_FROM"].ToString(), false);
                                    AuditTranSystem.DateTo = this.UtilityMember.DateSet.ToDate(drPortallockvoucher["DATE_TO"].ToString(), false);
                                    AuditTranSystem.LockTransId = AuditTranSystem.FetchAuditDetailIdByProjectDateRange();
                                    AuditTranSystem.Password = objsimpleencryption.EncryptString(drPortallockvoucher["PASSWORD"].ToString());
                                    AuditTranSystem.Reason = drPortallockvoucher["REASON"].ToString();
                                    AuditTranSystem.PasswordHint = objsimpleencryption.EncryptString(drPortallockvoucher["PASSWORD"].ToString());
                                    AuditTranSystem.LockByPortal = (Int32)YesNo.Yes;
                                    AuditTranSystem.SaveAuditTrans();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                AcMELog.WriteLog("Error in Background process :: GetVoucherLocksFromPortal " + err.Message);
            }
        }


        #endregion

        private void bbiLatestVersion_ItemClick(object sender, ItemClickEventArgs e)
        {
            GetLastestUpdater();
        }

        private void barManager1_HighlightedLinkChanged(object sender, HighlightedLinkChangedEventArgs e)
        {
            if (e.Link != null && e.Link.Item is BarEditItem && (e.Link.Item as DevExpress.XtraBars.BarEditItem) == bbiLatestVersion)
            {
                Cursor = Cursors.Hand;
            }
            else
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// This method is used to delete lock vouchers which are deleted in portal
        /// </summary>
        /// <returns></returns>
        private ResultArgs DeleteLockVouchersUnUsedLocal(DataTable dtPortalLockVouchers)
        {
            ResultArgs resultargs = new ResultArgs();
            using (AuditLockTransSystem AuditTranSystem = new AuditLockTransSystem())
            {
                resultargs = AuditTranSystem.FetchAllAuditTrans();
                if (resultargs.Success && resultargs.DataSource.Table != null)
                {
                    DataTable dtLocalLockVouchers = resultargs.DataSource.Table;
                    dtLocalLockVouchers.DefaultView.RowFilter = AuditTranSystem.AppSchema.AuditLockTransType.LOCK_BY_PORTALColumn + "='" + YesNo.Yes.ToString() + "'";
                    DataTable dtLocalLockVouchersLockbyPortal = dtLocalLockVouchers.DefaultView.ToTable();
                    if (dtLocalLockVouchersLockbyPortal.Rows.Count > 0)
                    {
                        List<DataRow> drList = dtLocalLockVouchersLockbyPortal.AsEnumerable().Where(r => !dtPortalLockVouchers.AsEnumerable().Select(x => x["PROJECT"]).ToList().Contains(r["PROJECT"])).ToList();
                        foreach (DataRow dr in drList)
                        {
                            Int32 projectid = this.UtilityMember.NumberSet.ToInteger(dr[AuditTranSystem.AppSchema.AuditLockTransType.PROJECT_IDColumn.ColumnName].ToString());
                            if (projectid > 0)
                            {
                                AuditTranSystem.ProjectId = projectid;
                                resultargs = AuditTranSystem.DeleteAduitTransByProject();
                                if (!resultargs.Success)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                AcMELog.WriteLog("DeleteLockVouchersUnUsedLocal: Project is not found in local branch");
                            }
                        }
                    }
                }
            }
            return resultargs;
        }

        /// <summary>
        /// Test Purpose
        /// </summary>
        /// <returns></returns>
        private DataTable GetLockVouchersFromPortal1()
        {
            DataTable dtVoucherLock = new DataTable();
            using (AuditLockTransSystem AuditTranSystem = new AuditLockTransSystem())
            {
                dtVoucherLock = AuditTranSystem.AppSchema.AuditLockTransType;
                dtVoucherLock.Columns.Add(AuditTranSystem.AppSchema.Project.PROJECTColumn.ColumnName, typeof(string));
                dtVoucherLock.Columns.Add("LOCK_TYPE", typeof(string));

                DataRow dr = dtVoucherLock.NewRow();
                dr[AuditTranSystem.AppSchema.Project.PROJECTColumn.ColumnName] = "BOSCO SOFT - TPT";
                dr[AuditTranSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName] = AuditTranSystem.GetProjectId("BOSCO SOFT - TPT");
                dr["LOCK_TYPE"] = "Pre Audit";
                dr[AuditTranSystem.AppSchema.AuditLockTransType.DATE_FROMColumn.ColumnName] = this.UtilityMember.DateSet.ToDate(this.LoginUser.YearFrom, false);
                dr[AuditTranSystem.AppSchema.AuditLockTransType.DATE_TOColumn.ColumnName] = this.UtilityMember.DateSet.ToDate(this.LoginUser.YearTo, false);
                dr[AuditTranSystem.AppSchema.AuditLockTransType.REASONColumn.ColumnName] = "Voucher entries are locked by Province admin " + this.UtilityMember.DateSet.GetDateToday(true);
                dr[AuditTranSystem.AppSchema.AuditLockTransType.PASSWORDColumn.ColumnName] = "p";
                dr[AuditTranSystem.AppSchema.AuditLockTransType.PASSWORD_HINTColumn.ColumnName] = "p";
                dr[AuditTranSystem.AppSchema.AuditLockTransType.LOCK_BY_PORTALColumn.ColumnName] = (Int32)YesNo.Yes;
                dtVoucherLock.Rows.Add(dr);

                //dr = dtVoucherLock.NewRow();
                //dr[AuditTranSystem.AppSchema.Project.PROJECTColumn.ColumnName] = "SOCIETY ACCOUNT";
                //dr[AuditTranSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName] = AuditTranSystem.GetProjectId("SOCIETY ACCOUNT");
                //dr["LOCK_TYPE"] = "Pre Audit1";
                //dr[AuditTranSystem.AppSchema.AuditLockTransType.DATE_FROMColumn.ColumnName] = this.UtilityMember.DateSet.ToDate(this.LoginUser.YearFrom, false);
                //dr[AuditTranSystem.AppSchema.AuditLockTransType.DATE_TOColumn.ColumnName] = this.UtilityMember.DateSet.ToDate(this.LoginUser.YearTo, false);
                //dr[AuditTranSystem.AppSchema.AuditLockTransType.REASONColumn.ColumnName] = "Voucher entries are locked by Province admin " + this.UtilityMember.DateSet.GetDateToday(true);
                //dr[AuditTranSystem.AppSchema.AuditLockTransType.PASSWORDColumn.ColumnName] = "p";
                //dr[AuditTranSystem.AppSchema.AuditLockTransType.PASSWORD_HINTColumn.ColumnName] = "p";
                //dr[AuditTranSystem.AppSchema.AuditLockTransType.LOCK_BY_PORTALColumn.ColumnName] = (Int32)YesNo.Yes;
                //dtVoucherLock.Rows.Add(dr);

            }
            return dtVoucherLock;
        }

        private void nbiGSTPolicy_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            bool hasHome = HasFormInMDI(typeof(frmGSTPolicy).Name);
            ShowWaitDialog();

            if (!hasHome)
            {
                frmGSTPolicy gstPolicy = new frmGSTPolicy();
                gstPolicy.MdiParent = this;
                gstPolicy.Show();
            }
            CloseWaitDialog();
        }

        private void nbiChequePrintingSetting_ItemChanged(object sender, EventArgs e)
        {

        }

        private void nbiChequePrintingSetting_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            frmChequePrintingSetting frmchequeprintingsetting = new frmChequePrintingSetting();
            frmchequeprintingsetting.ShowDialog();
        }

        /// <summary>
        /// For automatic upload database, current database is getting uploaded during the development
        /// 
        /// so lock upload database logic if system is local  development systmn
        /// 
        /// On 11/10/2018, for SDBIM, Branch Code "SDBSPC" is using for test purpose, so we lock uploading current databse
        /// </summary>
        /// <returns></returns>
        private bool IsLocalDevelopmentIP()
        {
            //192.168.1.171 - Alwar, 192.168.1.172 - Chinna, 192.168.1.173 - Kalis, 192.168.1.174 - Acme.erp (Salamon), 192.168.1.175 - Alex (YLG)
            //192.168.1.201 - Alex (YLG), 192.168.1.10 - Baskar, 192.168.1.8 - Anand, 192.168.1.201 - Charles

            bool Rtn = false;
            string[] localIPS = new string[] { "192.168.1.171", "192.168.1.172", "192.168.1.173", "192.168.1.174", "192.168.1.175" , 
                                               "192.168.1.201","192.168.1.10","192.168.1.8", "192.168.1.201"};

            try
            {
                string localipaddress = this.GetLocalIPv4(System.Net.NetworkInformation.NetworkInterfaceType.Ethernet);
                if (!string.IsNullOrEmpty(localipaddress))
                {
                    int pos = Array.IndexOf(localIPS, localipaddress);
                    if (pos > -1)
                    {
                        Rtn = true;
                    }
                }
            }
            catch (Exception err)
            {
                AcMELog.WriteLog("Error in checking IsLocalDevelopmentIP " + err.Message);
            }

            //On 11/10/2018, for SDBIM, Branch Code "SDBSPC" is using for test purpose, so we lock uploading current databse
            if (Rtn == false)
            {
                if (AppSetting.PartBranchOfficeCode.Trim().ToUpper() == "SDBSPC")
                {
                    Rtn = true;
                }
            }



            return Rtn;
        }

        private bool IsMismatchedLicenseKeyWithPortalProjects()
        {
            bool Rtn = true;
            //On 30/05/2019, to lock uploading databalse if mmismatching license or mismatching projects
            try
            {
                Rtn = this.IsLicenseKeyMismatchedByHoProjects();
            }
            catch (Exception err)
            {
                AcMELog.WriteLog("Error in checking IsLocalDevelopmentIP - Mismatching License Key " + err.Message);
                Rtn = true;
            }


            //On 05/06/2019, to lock uploading databalse if mmismatching license or mismatching locations (DB and License Key)
            if (Rtn == false)
            {
                try
                {
                    Rtn = this.IsLicenseKeyMismatchedByLicenseKeyDBLocation();
                }
                catch (Exception err)
                {
                    AcMELog.WriteLog("Error in checking IsLocalDevelopmentIP - Mismatching License Key " + err.Message);
                    Rtn = true;
                }
            }
            return Rtn;
        }

        private void nbiExportToTally_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            TallyConnector tallyconnector = new TallyConnector();
            this.ShowWaitDialog("Connecting Tally...");
            ResultArgs resultArgs = tallyconnector.IsMoreThanOneTallyRunningInstance;
            if (!resultArgs.Success)
            {
                resultArgs = tallyconnector.FetchCurrentCompanyDetails();
                if (resultArgs != null && resultArgs.Success)
                {
                    frmTallyExport frmexporttally = new frmTallyExport();
                    frmexporttally.ShowDialog();
                }
                else
                {
                    this.CloseWaitDialog();
                    this.ShowMessageBox(resultArgs.Message);
                }
            }
            else
            {
                this.CloseWaitDialog();
                this.ShowMessageBox(resultArgs.Message);
            }
        }


        private void nbiStatisticsType_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadStatisticsType();
        }

        /// <summary>
        /// ONLY FROM SDBINM
        /// 
        /// On 28/04/2018, In SDBINM province all the branch office are using invlaid ledger names, auditor suggested to change its name
        /// 
        /// So these leger names will be updated/changed to new name in MASTER_LEDGER, MASTER_HEADOFFICE_LEDGER
        /// </summary>
        public void CorrectSDBINMLedgersName()
        {
            if (AppSetting.IS_SDB_CONGREGATION && AppSetting.HeadofficeCode.ToUpper() == "SDBINM")
            {
                string[,] ToCorrectLedgers = new string[,] {{"Hostel Establishment Fees","Boarding Establishment Fees"},
                                                            {"Dumb Box Collections","Dumb Box Offerings"}, 
                                                            {"Mission Sunday collections","Mission Sunday Offerings"},
                                                            {"Sale of Candles","Offering Candles"}, 
                                                            {"Sale of Religious Articles", "Offering Religious Articles"},
                                                            {"Tuesday Collections", "Offering Tuesday Dumbox"},
                                                            {"Sale of Note Books", "Text & Note Book Fees"},
                                                            {"Purchase of Land", "Land"},
                                                            {"Purchase of Computer", "Computer"},
                                                            {"Purchase of Vehicle", "Vehicle"},
                                                            {"Purchase of Equipments", "Equipments"},
                                                            {"TDS Deducted / Remitted", "TDS Recovered / Remitted"},
                                                            {"EPF Deducted / Remitted", "EPF Recovered / Remitted"},
                                                            {"ESI Deducted / Remitted", "EPF Recovered / Remitted"},
                                                            {"TDS Recover from IT Department", "ESI Recovered / Remitted"},
                                                            {"Staff Loan", "Staff Loan given / Recovered"}, //On 25/03/2021
                                                            {"Interest on Staff Loan", "Staff Contribution"}, 
                                                            {"Celebration & Feast Expenses", "Functions Expenses"},
                                                            {"Admission Fees / Application Fees", "Admission Fees"}, 
                                                            {"Professional Tax", "Professional Tax Recovered / Remitted"}, 
                                                            {"Over Time Payment to Workers", "Over Time Payment"}, 
                                                            {"Carpentry", "Carpentry Training Centre Exp"}, 
                                                            {"Welding Works", "Welding Training Centre Exp"}, 
                                                            {"University Associations Expenses", "University Expenses"},
                                                            {"Text & Note Book Fees", "Miscellaneous Fees(New)"},
                                                            {"Management Fees", "Miscellaneous Fees(New1)"},
                                                            {"Uniform, Diary & Stationery Fees", "Miscellaneous Fees(New2)"},
                                                            {"Contribution from DB Tech", "DB Tech Income(New)"},
                                                            {"Repairs and Maintenance - Building", "Repairs and Renovation"},
                                                            {"Printing & Binding", "Printing & Xerox(New)"},
                                                            {"Magazine Printing", "Printing & Xerox(New1)"},
                                                            {"Domestic Staff Salary", "Staff Salary(New)"},
                                                            {"Society Registration / Renewal Charges", "Society Renewal Charges"},
                                                            {"Feast & Celebrations", "Function & Celebrations"},
                                                            {"Functions Expenses", "Function & Celebrations(New)"},
                                                            {"", ""},
                                                         };

                for (int i = 0; i < ToCorrectLedgers.GetUpperBound(0); i++)
                {
                    string oldledgername = ToCorrectLedgers.GetValue(i, 0).ToString();
                    string newedgername = ToCorrectLedgers.GetValue(i, 1).ToString();

                    if (!string.IsNullOrEmpty(oldledgername) && !string.IsNullOrEmpty(newedgername))
                    {
                        //1. Update in Master ledger, Master Head Office ledger
                        using (LedgerSystem ledgersys = new LedgerSystem())
                        {
                            ResultArgs resultarg = ledgersys.ChangeLedgerName(oldledgername, newedgername);
                        }
                    }
                }
            }
        }

        // This is to Insert the Asset Item with its Ledgers
        // 19.04.2021 
        // Asset Class, Asset Item, Ledger
        // 
        public void InsertAssetItem()
        {
            if (AppSetting.IS_SDB_CONGREGATION && AppSetting.HeadofficeCode.ToUpper() == "SDBINM")
            {
                string[,] AssetItemDetails = new string[,] {{"Office Equipments","Monitor","Computer","Moni","40","2"}, 
                                                            {"Office Equipments","Computer","Computer","Comp","40","2"},
                                                            {"Office Equipments","Scanner","Computer","Scan","40","2"}, 
                                                            {"Office Equipments","Printer","Computer","Prin","40","2"},
                                                            {"Office Equipments","Laptop","Computer","Lap","40","2"},

                                                            {"Vehicles","Three Wheeler","Vehicle","Thre","15","2"},
                                                            {"Vehicles","Two Wheelers","Vehicle","Two","15","2"},
                                                            {"Vehicles","Cycle","Vehicle","Cyc","15","2"},
                                                            {"Vehicles","Bus","Vehicle","Bus","15","2"},
                                                            {"Vehicles","Car","Vehicle","Car","15","2"},
                                                            {"Vehicles","Jeep","Vehicle","Jeep","15","2"},

                                                            {"Other Tangible Assets","Borewell","Bore Well","Bore","10","2"},

                                                            {"Other Tangible Assets","Well","Well & Water supply","Well","10","2"},
                                                            {"Other Tangible Assets","Motot Pumbset","Well & Water supply","Moto","10","2"},

                                                            {"Fixtures & Fittings","Table","Furniture & Fixtures","Tab","10","2"},
                                                            {"Fixtures & Fittings","Library Books and Journal","Furniture & Fixtures","Lib","10","2"},
                                                            {"Fixtures & Fittings","Buero","Furniture & Fixtures","Buer","10","2"},
                                                            {"Fixtures & Fittings","Smart Board","Furniture & Fixtures","Samt","10","2"},
                                                            {"Fixtures & Fittings","Rack","Furniture & Fixtures","Rack","10","2"},
                                                            {"Fixtures & Fittings","Writing Board","Furniture & Fixtures","Writ","10","2"},
                                                            {"Fixtures & Fittings","Chair","Furniture & Fixtures","Cha","10","2"},
                                                            {"Fixtures & Fittings","Bench","Furniture & Fixtures","Ben","10","2"},
                                                            {"Fixtures & Fittings","Desk","Furniture & Fixtures","Desk","10","2"},
                                                            {"Fixtures & Fittings","Cupboard","Furniture & Fixtures","Cub","10","2"},

                                                            {"Office Equipments","Sewing Machine","Equipments","Sew","15","2"},
                                                            {"Office Equipments","Laboratory Equipment","Equipments","Lab","15","2"},
                                                            {"Office Equipments","Micro Ovan","Equipments","Mic","15","2"},
                                                            {"Office Equipments","Air Cooler / Tower Fan","Equipments","Air","15","2"},
                                                            {"Office Equipments","Washing Machine","Equipments","Wash","15","2"},
                                                            {"Office Equipments","Musicial Instruments","Equipments","Mus","15","2"},
                                                            {"Office Equipments","Water Dispenser","Equipments","W.dis","15","2"},
                                                            {"Office Equipments","Water Heater","Equipments","W.He","15","2"},
                                                            {"Office Equipments","Vacc. Cleaner","Equipments","Vacc","15","2"},
                                                            {"Office Equipments","Ro Water System","Equipments","Ro","15","2"},
                                                            {"Office Equipments","Solar System","Equipments","Sola","15","2"},
                                                            {"Office Equipments","Generator","Equipments","Gen","15","2"},
                                                            {"Office Equipments","Cctv","Equipments","Cub","15","2"},
                                                            {"Office Equipments","Invertor","Equipments","Inve","15","2"},
                                                            {"Office Equipments","Ups / Battery","Equipments","Ups","15","2"},
                                                            {"Office Equipments","Fridge","Equipments","Fri","15","2"},
                                                            {"Office Equipments","Mobile Phone","Equipments","Mob","15","2"},
                                                            {"Office Equipments","Airconditionar","Equipments","Airc","15","2"},
                                                            {"Office Equipments","Television","Equipments","Tel","15","2"},
                                                            {"Office Equipments","Projector","Equipments","Proj","15","2"},
                                                            {"Office Equipments","Projector Screen","Equipments","P.Sc","15","2"},
                                                            {"Office Equipments","Biometric Machine","Equipments","Bio","15","2"},
                                                            {"Office Equipments","Speaker","Equipments","Spea","15","2"},
                                                            {"Office Equipments","Gym Equipment","Equipments","Gym","15","2"},
                                                            {"Office Equipments","Head Phone","Equipments","H.PH","15","2"},
                                                            {"Office Equipments","Home Theater","Equipments","H.Th","15","2"},
                                                            {"Office Equipments","Microphone","Equipments","Micr","15","2"},
                                                            {"Office Equipments","Photo Copier","Equipments","P.Co","15","2"},
                                                            {"Office Equipments","Amplifier","Equipments","Amp","15","2"},
                                                            {"Office Equipments","Camera","Equipments","Cam","15","2"},

                                                            {"Software - Programs - licences","Software","Software","Soft","40","2"},

                                                            {"Plant & Machinery","Machinery","Plant & Machinery","Mach","15","2"},

                                                            {"Fixtures & Fittings","Fan","Electrical Fittings","Fan","15","2"},

                                                            {"Buildings","Buildings","New Constructions","Buil","10","2"},
                                                            {"Land","Land","Land","Land","0","2"},
                                                            
                                                            {"", "","","","",""},
                                                         };
                for (int i = 0; i < AssetItemDetails.GetUpperBound(0); i++)
                {
                    string AssetClass = AssetItemDetails.GetValue(i, 0).ToString();
                    string AssetItem = AssetItemDetails.GetValue(i, 1).ToString();
                    string LedgerName = AssetItemDetails.GetValue(i, 2).ToString();
                    string Prefix = AssetItemDetails.GetValue(i, 3).ToString();
                    string Depreciation = AssetItemDetails.GetValue(i, 4).ToString();
                    string AssetAccessFlag = AssetItemDetails.GetValue(i, 5).ToString();

                    if (!string.IsNullOrEmpty(AssetClass) && !string.IsNullOrEmpty(AssetItem) && !string.IsNullOrEmpty(LedgerName) && !string.IsNullOrEmpty(Prefix) && !string.IsNullOrEmpty(Depreciation))
                    {
                        using (AssetItemSystem assetitemsystem = new AssetItemSystem())
                        {
                            ResultArgs result = assetitemsystem.SaveDefaultAssetItem(AssetClass, AssetItem, LedgerName, Prefix, Depreciation, AssetAccessFlag);

                            //On 07/05/2021, Check any problem ocurred for bulk update, show message and break it
                            //If any DB column mismatching or DB structure exceptions
                            if (!result.Success && result.IsDBException)
                            {
                                this.ShowMessageBoxWarning("Not able to update default Asset Items.");
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 18/01/2018 Show export voucher form if setting is enbaled 
        /// </summary>
        private DialogResult AlertToExportVoucher()
        {
            DialogResult dialogResult = System.Windows.Forms.DialogResult.Cancel;

            //1. 18/01/2018 Show export voucher form if setting is enbaled ------------------------------------------------------------------------------------
            //If Export voucher is enabled in finance setting, if user does not do it, diable logout
            if (this.AppSetting.ExportVouchersBeforeClose == "1" || this.AppSetting.IS_SDB_ING)
            {
                frmExportBranchOfficeVouchers frmexportVouchers = new frmExportBranchOfficeVouchers(ExportMode.Online, true);
                dialogResult = frmexportVouchers.ShowDialog();
            }
            //-------------------------------------------------------------------------------------------------------------------------------------------------
            return dialogResult;
        }

        private void nbiFDReInvestment_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadFDReInvestment();
        }

        private void LockLicenseKey()
        {
            try
            {
                if (!Is_Branch_Active && IsLicenseKeyLocked() == false)
                {
                    string stringkeydate = string.Empty;
                    string licensepath = SettingProperty.ActiveDatabaseLicenseKeypath;
                    SimpleEncrypt.SimpleEncDec objDec = new SimpleEncrypt.SimpleEncDec();
                    using (UserSystem userSystem = new UserSystem())
                    {
                        stringkeydate = userSystem.AppSchema.LicenseDataTable.KEY_GENERATED_DATEColumn.ColumnName;

                        if (File.Exists(licensepath) && !string.IsNullOrEmpty(stringkeydate))
                        {
                            XmlDocument xDoc = new XmlDocument();
                            xDoc.Load(licensepath);
                            //string newSetting = xDoc.GetElementsByTagName(stringkeydate);
                            //string licensekeydate = objDec.DecryptString("//LicenseKey/KEY_GENERATED_DATE");

                            XmlNodeList userNodes = xDoc.SelectNodes("//LicenseKey/KEY_GENERATED_DATE");
                            if (userNodes.Count > 0)
                            {
                                string nodeValue = userNodes[0].InnerText;
                                userNodes[0].InnerText = objDec.EncryptString(nodeValue);
                                xDoc.Save(licensepath);
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                AcMELog.WriteLog(err.Message);
            }
        }


        private bool IsLicenseKeyLocked()
        {
            bool rtn = false;
            try
            {

                if (!string.IsNullOrEmpty(AppSetting.LicenseKeyExprDate))
                {
                    bool isvalidate = false;
                    DateTime licensekeydate = DateTime.Today;
                    isvalidate = DateTime.TryParse(AppSetting.LicenseKeyGeneratedDate, out licensekeydate);
                    rtn = (isvalidate == false);
                }
            }
            catch (Exception err)
            {
                AcMELog.WriteLog(err.Message);
            }
            return rtn;
        }

        /// <summary>
        /// On 15/04/2021, To have latest version features local and show in notepad
        /// </summary>
        private void UpdateLatestVersionLocally()
        {
            try
            {
                string acmerpinstalledpath = Application.StartupPath.ToString();
                if (!string.IsNullOrEmpty(NewFeaturesInPortal))
                {
                    string latestfeaturepath = Path.Combine(acmerpinstalledpath, "LatestVersionFeatures.txt");
                    using (TextWriter writer = File.CreateText(latestfeaturepath))
                    {
                        writer.WriteLine(NewFeaturesInPortal);
                    }

                    bbiShowLatestVersionFeature.Visibility = BarItemVisibility.Always;
                }
            }
            catch (Exception err)
            {
                string msg = err.Message;
            }
        }

        private void ShowFeaturesListinNotepad()
        {
            try
            {
                string acmerpinstalledpath = Application.StartupPath.ToString();
                string latestfeaturepath = Path.Combine(acmerpinstalledpath, "LatestVersionFeatures.txt");
                if (File.Exists(latestfeaturepath))
                {
                    Process.Start("Notepad.exe", latestfeaturepath);
                }
            }
            catch (Exception err)
            {
                string msg = err.Message;
            }
        }
        /// <summary>
        /// This method is used to get mac address and ip address of current system
        /// </summary>
        /// <returns></returns>
        private string GetMACIPAddress()
        {
            string Rtn = string.Empty;
            String MacAddress = string.Empty;
            String IPAddress = string.Empty;
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface adapter in nics)
                {
                    if (MacAddress == String.Empty)// only return MAC Address from first card
                    {
                        IPInterfaceProperties properties = adapter.GetIPProperties();

                        //1. Get Mac Address
                        if (adapter.GetPhysicalAddress() != null)
                        {
                            MacAddress = adapter.GetPhysicalAddress().ToString() + ",";
                        }

                        //2. Get IP Address
                        foreach (UnicastIPAddressInformation ip in properties.UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                IPAddress += ip.Address.ToString() + ",";
                            }
                        }
                    }
                }

            }
            catch (Exception err)
            {
                AcMELog.WriteLog("Error in getting MAC address details :: updating branch logged details " + err.Message);
            }

            MacAddress = MacAddress.TrimEnd(',');
            IPAddress = IPAddress.TrimEnd(',');

            if (!string.IsNullOrEmpty(MacAddress))
            {
                Rtn = "MAC Address : " + MacAddress;
            }

            if (!string.IsNullOrEmpty(MacAddress))
            {
                Rtn += " IP Address : " + IPAddress;
            }
            return Rtn;
        }


        private void nbiVendor_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadVendorManufacture(VendorManufacture.Vendor, FinanceModule.Asset);
        }

        private void nbiSSPIntegration_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //frmGetPostMasterVoucher GetPostVoucher = new frmGetPostMasterVoucher();
            //GetPostVoucher.ShowDialog();

            //AcMEService.frmGetPostMasterVoucherold GetPostVoucher = new AcMEService.frmGetPostMasterVoucherold();
            //GetPostVoucher.ShowDialog();
        }

        private void nbiMergeCashBankLedger_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            frmMergeCashBankLedgers frmmergeCB = new frmMergeCashBankLedgers();
            frmmergeCB.ShowDialog();
        }

        /// <summary>
        /// Stock Block Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiStockBlock_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadBlockDetails(FinanceModule.Stock);
        }

        private void nbiSubClass_LinkClicked(object sender, NavBarLinkEventArgs e)
        {

            bool hasHome = HasFormInMDI(typeof(frmAssetSubClassView).Name);
            ShowWaitDialog();
            if (!hasHome)
            {
                frmAssetSubClassView frmsubcls = new frmAssetSubClassView();
                frmsubcls.MdiParent = this;
                frmsubcls.Show();
            }
            CloseWaitDialog();
        }

        private void bbiShowLatestVersionFeature_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowFeaturesListinNotepad();
        }

        private void nbiUpdateNarration_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            frmBulkUpdateNarration bulkUpdateNarration = new frmBulkUpdateNarration();
            bulkUpdateNarration.ShowDialog();
        }

        private void nbiAuditLog_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            bool hasfrmHome = HasFormInMDI(typeof(frmVouchersAuditLog).Name);
            CloseFormInMDI(typeof(frmVouchersAuditLog).Name);
            frmVouchersAuditLog frmAuditLog = new frmVouchersAuditLog(RecentVoucherDate, RecentProjectId, RecentProject, ((int)DefaultVoucherTypes.Receipt) - 1, ((int)DefaultVoucherTypes.Receipt));
            frmAuditLog.MdiParent = this;
            frmAuditLog.Show();
        }


        /// <summary>
        /// This is to be shown in the Payroll module ( 29.10.2021)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiBankBranch_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadBank();
        }

        /// <summary>
        /// This is to be shown in the Payroll module ( 29.10.2021)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiBankAccountPayroll_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadBankAccount();
        }

        private void nbiPayrollSetting_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            PAYROLL.Modules.Payroll_app.frmPayrollSetting frmpayrollsetting = new PAYROLL.Modules.Payroll_app.frmPayrollSetting();
            frmpayrollsetting.ShowDialog();
        }

        private void tltItemReceiptModuleStatus_ItemClick(object sender, TileItemEventArgs e)
        {
            //Show Receipt Module request if and if only for Receipt module title
            if (tltItemReceiptModuleStatus.Frames.Count > 0)
            {
                if (e.Item != null && e.Item.CurrentFrame.Tag != null && e.Item.CurrentFrame.Tag.ToString() == "1")
                {
                    frmRequestModuleRights frmrequestmodulerights = new frmRequestModuleRights();
                    frmrequestmodulerights.ShowDialog();

                    //On 22/01/2024, For offline temporarily
                    if (AppSetting.FINAL_RECEIPT_MODULE_STATUS == LCBranchModuleStatus.Approved && !SettingProperty.Is_Application_Logout)
                    {
                        //Enforce receipt module rights
                        EnforceReceiptModuleRightsInMainMenus();
                    }
                }
            }
        }

        private void tileCtlReceiptModueStatus_Paint(object sender, PaintEventArgs e)
        {
            //Show Receipt Module request if and if only for Receipt module title
            if (tltItemReceiptModuleStatus.Frames.Count > 0)
            {
                if (tltItemReceiptModuleStatus.CurrentFrame.Tag != null && tltItemReceiptModuleStatus.CurrentFrame.Tag.ToString() == "2")
                {
                    tileCtlReceiptModueStatus.Width = 525;
                    tileCtlReceiptModueStatus.ItemSize = 250;
                    tileCtlReceiptModueStatus.Left = this.Width - (tileCtlReceiptModueStatus.Width - 5);
                    btnRequestModuleRights.Visible = false;
                }
                else if (tltItemReceiptModuleStatus.CurrentFrame.Tag != null && tltItemReceiptModuleStatus.CurrentFrame.Tag.ToString() == "1")
                {
                    tileCtlReceiptModueStatus.Width = 405;
                    tileCtlReceiptModueStatus.ItemSize = 185;
                    tileCtlReceiptModueStatus.Left = this.Width - (tileCtlReceiptModueStatus.Width - 5);
                    btnRequestModuleRights.Visible = true;
                }
            }
        }

        private void btnRequestModuleRights_Click(object sender, EventArgs e)
        {
            frmRequestModuleRights frmrequestmodulerights = new frmRequestModuleRights();
            frmrequestmodulerights.ShowDialog();

            //On 22/01/2024, For offline temporarily
            if (AppSetting.FINAL_RECEIPT_MODULE_STATUS == LCBranchModuleStatus.Approved && !SettingProperty.Is_Application_Logout)
            {
                //Enforce receipt module rights
                EnforceReceiptModuleRightsInMainMenus();
            }
        }

        private void nbiThirdPartyIntegration_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            frmGetPostMasterVoucher GetPostVoucher = new frmGetPostMasterVoucher();
            GetPostVoucher.ShowDialog();
        }

        private void nbiFDPostCharge_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadFDPostCharge();
        }

        private void nbiProjectBulkImportData_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadImportData(true);
        }

        private void nbiProjectBulkExportData_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadExportData(true);
        }

        private void nbiCustomizeStaffOrder_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            PAYROLL.Modules.Payroll_app.frmStaffOrder frmStafforder = new PAYROLL.Modules.Payroll_app.frmStaffOrder();
            frmStafforder.ShowDialog();
        }

        private void bbiHome_ItemDoubleClick(object sender, ItemClickEventArgs e)
        {
            if (this.ShowConfirmationMessage("Are you sure to re-order ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                this.ShowWaitDialog("Processing...");
                using (VoucherTransactionSystem vouchertranssystem = new VoucherTransactionSystem())
                {
                    vouchertranssystem.MakeReindex();
                }
                this.CloseWaitDialog();
            }
        }

        private void nbiAuthorizeVoucher_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            frmAuthorizeVoucher frmauthorize = new frmAuthorizeVoucher();
            frmauthorize.ShowDialog();
        }

        private void nbiCreditorsDebtors_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadLedgerSundryCreditorsDebtors();
        }

        private void nbiPayrollDepartment_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadPayrollDepartment();
        }

        private void nbiPayrollWorkLocation_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadPayrollWorkLocation();
        }

        private void nbiStaffNameTitle_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadStaffNameTitle();
        }

        private void leftMenuBar_Click(object sender, EventArgs e)
        {

        }

        private void nbiCashAccounts_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            LoadCashAccountsLedgers();
        }

        private void nbiGoverningBodyMember_LinkClicked(object sender, NavBarLinkEventArgs e)
        {

        }

        private void nbiGBView_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            loadGoverningBody();
        }


        private void tileCtlReceiptModueStatus_MouseEnter(object sender, EventArgs e)
        {
            if (tltItemReceiptModuleStatus.Frames.Count > 0)
            {
                if (tltItemReceiptModuleStatus.CurrentFrame.Tag != null && tltItemReceiptModuleStatus.CurrentFrame.Tag.ToString() == "2")
                {
                    ctltooltipProviceRightsTileControl.AutoPopDelay = 10000;
                    ctltooltipProviceRightsTileControl.ShowHint(VoucherEntryGraceDaysToolTipMessage, "Voucher Entry Grace Days");
                }
            }
        }

        private void tileCtlReceiptModueStatus_MouseLeave(object sender, EventArgs e)
        {
            ctltooltipProviceRightsTileControl.SetSuperTip(tileCtlReceiptModueStatus, null);
            ctltooltipProviceRightsTileControl.HideHint();
        }

    }

    public class CustomNavPaneViewInfo : NavigationPaneViewInfo
    {
        public CustomNavPaneViewInfo(NavBarControl navBar) : base(navBar) { }
        public override void OnMouseUp(MouseEventArgs e)
        {
            NavBarHitInfo hitInfo = CalcHitInfo(new Point(e.X, e.Y));
            if (hitInfo.InGroupCaption)

                if (hitInfo.Group.ItemLinks.Count == 1)
                {
                    return;
                }
            base.OnMouseUp(e);
        }
    }



    public class CustomNavPaneViewInfoRegistrator : SkinNavigationPaneViewInfoRegistrator
    {
        public override string ViewName { get { return "CustomNavPaneView"; } }
        public override NavBarViewInfo CreateViewInfo(NavBarControl navBar)
        {
            return new CustomNavPaneViewInfo(navBar);
        }
    }
}
