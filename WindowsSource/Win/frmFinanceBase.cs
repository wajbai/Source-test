/*
 * On 08/06/2022, Receipt Module Rights Changes Ref 1 : 
 Few institutions are using DHCP (Dynamic Host Configuration Protocol), every time a new ip will be
 assigned to a local community PC. so receipt module rights will be locked as ip is changed the next day even though it was approved the previous day 
 So we plan to check the MAC address alone in the Local community system, so it will not lock even though ip is changed.
 if they again request a portal and ip is changed, this will be treated as a new request.
 
  
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Resources;
using System.Reflection;
using Proshot.UtilityLib.CommonDialogs;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraPrinting;
using System.Windows.Forms;
using Bosco.Utility;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking;
using Bosco.Utility.ConfigSetting;
using System.Data;
using DevExpress.XtraEditors.Repository;
using System.Text.RegularExpressions;
using Bosco.DAO.Schema;
using System.Globalization;
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid.Views.Grid;
using Bosco.Model.Setting;
using System.Configuration;
using Bosco.DAO.Configuration;
using Bosco.Model.UIModel;
using System.ServiceModel;
using AcMEDSync.Model;
using ACPP.Modules.Dsync;
using ACPP.Modules.Transaction;
using System.Net;
using System.IO;
using Bosco.Model.UIModel.Master;
using Bosco.DAO;
using Microsoft.Win32;
using Bosco.Model;
using Bosco.Model.Transaction;


namespace ACPP
{
    public class frmFinanceBase : Bosco.Utility.Base.frmBase
    {
        #region Declaration
        frmMain appMain = null;
        CommonMember utilityMember = null;
        private string PrintPageTitle = string.Empty;
        public const string ACME_ERP_VERSION_FILE = "AcMEERP_Vouchers";
        bool CheckFilter = true;
        int Ledgerflag = 0;
        private DataTable dtBaseDatabaseSettingDetails = null;


        string ApprovedSign4RoleName = string.Empty;
        string ApprovedSign5RoleName = string.Empty;
        string ApprovedSign4Role = string.Empty;
        string ApprovedSign5Role = string.Empty;
        byte[] byteApprovedSign4Image = null;
        byte[] byteApprovedSign5Image = null;

        #endregion

        protected frmFinanceBase()
        {

        }

        protected override void OnLoad(EventArgs e)
        {
            appMain = this.MdiParent as frmMain;
            ShowUserName();
            SetPageTitle();
            ShowCurrentPeriod();
            ShowProjectWindow();
            SetDefaultProject();
            ShowBranchDeatils();
            base.OnLoad(e);
        }

        //Added by Carmel Raj

        /// <summary>
        /// To Set Visibility of the Shorcut Properties which is shown on the Global View Screen
        /// </summary>
        /// <param name="Date">Set Date Keys(F3) Visibility</param>
        /// <param name="Project">Set Project Keys(F5) Visibility</param>
        /// <param name="Configuration">Set Configuation Keys(F12) Visibility.By default the property is set true</param>
        public void SetVisibileShortCuts(bool Date, bool Project, bool Configuration = true)
        {
            appMain.SetVisibleConfiguration = Configuration;
            appMain.SetVisibleDate = Date;
            appMain.SetVisibleProject = Project;
            appMain.SetVisibleCloseAllTabs = appMain.SetCloseAllTabsVisibility;
        }



        protected bool ShowHideLeftMenuBar
        {
            set
            {
                if (appMain != null)
                {
                    appMain.ShowHideLeftMenuBar = value;
                }
            }
        }

        protected bool ShowMdiDockManager
        {
            set
            {
                if (appMain != null)
                {
                    appMain.ShowHideLeftMenuBar = value;
                }
            }
        }

        protected bool ShowRibbonHomePage
        {
            set
            {
                if (appMain != null)
                {
                    appMain.ShowHideLeftMenuBar = value;
                }
            }
        }

        protected DockVisibility ShowHideDockPanel
        {
            set
            {
                if (appMain != null)
                {
                    appMain.ShowHideDockPanel = value;
                }
            }
        }
        protected virtual void ShowUserName()
        {
            if (appMain != null)
            {
                appMain.UserName = this.LoginUser.LoginUserName;
                appMain.UserId = this.LoginUser.LoginUserId;
                appMain.RoleId = this.LoginUser.LoginUserRoleId;

            }
        }

        protected virtual void ShowBranchDeatils()
        {
            if (appMain != null)
            {
                appMain.bbiBranch.Caption = (string.IsNullOrEmpty(this.AppSetting.InstituteName)) ? "" : "<u><b><color=blue>" + this.AppSetting.InstituteName.ToString().ToUpper() + "</color><b></u>";
                appMain.bbiHoCode.Caption = "HO : " + ((string.IsNullOrEmpty(this.AppSetting.HeadofficeCode)) ? "" : this.AppSetting.HeadofficeCode.ToString().ToUpper());
                appMain.bbiBoCode.Caption = "BO : " + ((string.IsNullOrEmpty(this.AppSetting.PartBranchOfficeCode)) ? "" : this.AppSetting.PartBranchOfficeCode.ToString().ToUpper());

                //29/11/2018, To show active connected database if multi db feature enabled
                appMain.bbiActiveBranchDB.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                if (!String.IsNullOrEmpty(SettingProperty.ActiveDatabaseName) && (this.AppSetting.AccesstoMultiDB == (int)YesNo.Yes))
                {
                    appMain.bbiActiveBranchDB.Caption = "Logged in Branch : " + SettingProperty.ActiveDatabaseAliasName;
                    appMain.bbiActiveBranchDB.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
            }
        }

        protected virtual void SetDefaultProject()
        {
            if (appMain != null)
            {
                appMain.DefaultProjectId = string.IsNullOrEmpty(this.AppSetting.UserProjectId) ? 0 : Convert.ToInt32(this.AppSetting.UserProjectId);
                appMain.DefaultProject = this.AppSetting.UserProject;
            }
        }

        protected virtual void ShowCurrentPeriod()
        {
            if (appMain != null)
            {
                DateTime dtyearfrom = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
                DateTime dtbookbeginfrom = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false);
                if (!string.IsNullOrEmpty(this.AppSetting.YearFrom) && !string.IsNullOrEmpty(this.AppSetting.YearTo))
                {
                    appMain.TransactionPeriod = Convert.ToDateTime(this.AppSetting.YearFrom).ToString("dd/MMM/yyyy") + " to " + Convert.ToDateTime(this.AppSetting.YearTo).ToString("dd/MMM/yyyy");
                    if (!string.IsNullOrEmpty(this.AppSetting.UserProject)) { appMain.RecentProject = AppSetting.UserProject; }
                    if (!string.IsNullOrEmpty(this.AppSetting.UserProjectId)) { appMain.RecentProject = AppSetting.UserProject; }
                    if (!string.IsNullOrEmpty(this.AppSetting.RecentVoucherDate)) { appMain.RecentVoucherDate = AppSetting.RecentVoucherDate; }
                }
            }
        }

        protected virtual void LoadCultureInfo(GridLookUpEdit glpCulture)
        {
            DataTable dtCultureSource = null;
            DataRow drCultureSource = null;
            ApplicationSchema.CultureDataTable dtCulure = new ApplicationSchema.CultureDataTable();
            ResourceManager resourceManger = new ResourceManager(typeof(ACPP.Modules.Master.frmSettings));
            CultureInfo[] allCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            try
            {
                foreach (CultureInfo culture in allCultures)
                {
                    ResourceSet rs = resourceManger.GetResourceSet(culture, true, false);
                    if (rs != null)
                    {
                        if (culture.Name != "")
                        {
                            drCultureSource = dtCulure.NewRow();
                            drCultureSource[dtCulure.CULTUREColumn.ColumnName] = culture.Name;
                            drCultureSource[dtCulure.ENGLISH_NAMEColumn.ColumnName] = culture.EnglishName;
                            drCultureSource[dtCulure.NATIVE_NAMEColumn.ColumnName] = culture.NativeName;
                            drCultureSource[dtCulure.DISPLAY_NAMEColumn.ColumnName] = culture.DisplayName;
                            dtCulure.Rows.Add(drCultureSource);
                        }
                    }
                }
                dtCulure.AcceptChanges();
                dtCultureSource = dtCulure;
                this.UtilityMember.ComboSet.BindGridLookUpCombo(glpCulture, dtCultureSource, dtCulure.DISPLAY_NAMEColumn.ColumnName, dtCulure.CULTUREColumn.ColumnName);
                glpCulture.EditValue = glpCulture.Properties.GetKeyValue(0);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        protected virtual void SetTransacationPeriod()
        {
            if (appMain != null)
            {
                appMain.LoadHomeAccountingPeriod();
            }

        }

        protected virtual void TransacationPeriod()
        {
            if (appMain != null)
            {
                if (!string.IsNullOrEmpty(this.AppSetting.YearFrom) && !string.IsNullOrEmpty(this.AppSetting.YearTo) && !string.IsNullOrEmpty(this.AppSetting.UserProject))
                {
                    appMain.SetTransaction = this.AppSetting.UserProject; // +" (" + Convert.ToDateTime(this.AppSetting.YearFrom).ToString("d") + " To " + Convert.ToDateTime(this.AppSetting.YearTo).ToString("d") + ")";
                }
            }
        }

        protected virtual void ShowProjectWindow()
        {
            if (appMain != null)
            {
                appMain.ProjectSelection = this.UIAppSetting.UIProjSelection;
            }
        }

        protected string PageTitle
        {
            set
            {
                if (appMain != null)
                {
                    appMain.PageTitle = value;
                }
            }
        }

        private void SetPageTitle()
        {
            if (appMain != null)
            {
                appMain.PageTitle = this.Text;
            }
        }
        protected int LedgerAccessRights
        {

            get { return Ledgerflag; }
            set { Ledgerflag = value; }
        }

        /// <summary>
        /// To set access level for the default ledgers  
        /// 0 - (All rights),
        /// 1 - (view, edit),
        /// 2 - (cannot edit the ledger name)
        /// </summary>
        /// <param name="LedgerId"></param>
        /// <returns></returns>
        protected void LedgerAccessFlag(int LedgerId)
        {
            if (LedgerId == 1 || LedgerId == 2 || LedgerId == 3)
            {
                LedgerAccessRights = 1;
            }
            //else if (LedgerId == 2)
            //{
            //    LedgerAccessRights = 2;
            //}
            else
            {
                LedgerAccessRights = 0;
            }
        }

        protected void PrintGridViewDetails(DevExpress.XtraGrid.GridControl GridView, string Title,
            PrintType printType, GridView gvControl, bool isLandscape = false)
        {

            Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry();
            SettingProperty.ReportModuleId = (int)ReportModule.Finance;
            report.ShowStandardReport(gvControl, Title);
        }

        protected void PrintGridViewDetails(GridView gvMain, string Title, GridView gvAdditional, string AditionGridTitle)
        {

            Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry();
            SettingProperty.ReportModuleId = (int)ReportModule.Finance;
            report.ShowStandardReport(gvMain, Title, gvAdditional, AditionGridTitle);
        }


        void link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            PageInfoBrick brick = e.Graph.DrawPageInfo(PageInfo.DateTime, this.AppSetting.InstituteName, Color.DarkBlue,

      new RectangleF(0, 0, 100, 80), BorderSide.None);

            PageInfoBrick brick2 = e.Graph.DrawPageInfo(PageInfo.DateTime, PrintPageTitle, Color.DarkBlue,

      new RectangleF(0, 0, 100, 20), BorderSide.None);

            brick.LineAlignment = BrickAlignment.Center;
            brick2.LineAlignment = BrickAlignment.Center;

            brick.Alignment = BrickAlignment.Center;
            brick2.Alignment = BrickAlignment.Center;

            brick.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            brick.AutoWidth = true;

            brick2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            brick2.AutoWidth = true;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "frmFinanceBase";
            this.Load += new System.EventHandler(this.frmFinanceBase_Load);
            this.ResumeLayout(false);
        }

        private void frmFinanceBase_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// This method is used to check Acme.erp webserive running 
        /// </summary>
        /// <returns></returns>
        public bool CheckAcmeerpWebserviceRunning()
        {
            bool Rtn = false;
            try
            {
                DataSyncService.DataSynchronizerClient objservice = new DataSyncService.DataSynchronizerClient();

                //To check Web service is running or n ot
                //On 11/12/2020, If branch is locked, portal web service throws error
                //objservice.IsLatestLicenseAvailable(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode, this.UtilityMember.DateSet.ToDate(this.AppSetting.LicenseKeyGeneratedDate, false));

                string productversion = objservice.GetAcmeERPProductVersion();
                Rtn = true;
                AcMELog.WriteLog("CheckAcmeerpWebserviceRunning, Acmeerp Webservice is running");
            }
            catch (Exception err)
            {
                AcMELog.WriteLog("CheckAcmeerpWebserviceRunning, Acmeerp Webservice is not running " + err.Message);
            }
            return Rtn;
        }

        /// <summary>
        /// This is to get the Appconfig database Name
        /// </summary>
        /// <returns></returns>
        public string GetAppConfigDB()
        {
            string dbname = string.Empty;
            string connString = string.Empty;
            try
            {
                if (this.AppSetting.AccesstoMultiDB == (int)YesNo.Yes && !(string.IsNullOrEmpty(ConfigurationHandler.Instance.ConnectionString)))
                {
                    connString = ConfigurationHandler.Instance.ConnectionString;
                }
                else { connString = ConfigurationManager.ConnectionStrings[AppSettingName.AppConnectionString.ToString()].ToString(); }
                string option = "database=";
                if (!string.IsNullOrEmpty(option))
                {
                    string dbcon = connString.Substring(connString.IndexOf(option) + option.Length);
                    string[] dbconfig = dbcon.Split(';');
                    dbname = dbconfig[0];
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
            return dbname;
        }

        /// <summary>
        /// On 22/10/2021, To get DB Server name from configure file
        /// </summary>
        /// <returns></returns>
        public string GetAppConfigDBServerName()
        {
            string dbservername = string.Empty;
            try
            {
                using (BackupAndRestore backuprestore = new BackupAndRestore())
                {
                    dbservername = backuprestore.GetAppConfigDBServerName();
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
            return dbservername;
        }

        /// <summary>
        /// On 25/10/2021, To get DB name from configure file
        /// </summary>
        /// <returns></returns>
        public string GetAppConfigDBName()
        {
            string dbname = string.Empty;
            try
            {
                using (BackupAndRestore backuprestore = new BackupAndRestore())
                {
                    dbname = backuprestore.GetAppConfigDBName();
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
            return dbname;
        }

        /// <summary>
        /// On 25/10/2021, To get DB port configure file
        /// </summary>
        /// <returns></returns>
        public string GetAppConfigDBServerPort()
        {
            string dbserverport = string.Empty;
            try
            {
                using (BackupAndRestore backuprestore = new BackupAndRestore())
                {
                    dbserverport = backuprestore.GetAppConfigDBServerPort();
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
            return dbserverport;
        }

        /// <summary>
        /// This method is used to show voucher type selection if given projects has multi vouchers execpet default vouchers
        /// On 28/01/2019, show list of voucher types ---------------------------------------------------------------------------------------------
        /// this list will be shown only when more than one voucher type exists except base vouchers for selected project
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        public ResultArgs ShowVoucherTypeSelection(Int32 projectid, string BaseVouchers = "", Int32 definitionvouhcertypeid = 0)
        {
            string loadbasevouchers = (int)DefaultVoucherTypes.Receipt + "," + (int)DefaultVoucherTypes.Payment + "," +
                                                          (int)DefaultVoucherTypes.Contra + "," + (int)DefaultVoucherTypes.Journal;
            if (!string.IsNullOrEmpty(BaseVouchers))
            {
                loadbasevouchers = BaseVouchers;
            }

            ResultArgs result = IsMultiVoucherTypes(projectid, loadbasevouchers);
            if (result.Success)
            {
                bool IsMultiVoucher = (bool)result.ReturnValue;
                result.Success = false;
                result.ReturnValue = null;
                if (IsMultiVoucher)
                {

                    frmVoucherTypeSelection frmvouchertypeselection = new frmVoucherTypeSelection(projectid, loadbasevouchers, definitionvouhcertypeid);
                    if (frmvouchertypeselection.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (frmvouchertypeselection.ReturnValue != null)
                        {
                            string[] VoucherTypeSelected = frmvouchertypeselection.ReturnValue as string[];
                            result.ReturnValue = VoucherTypeSelected;
                            result.Success = true;
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Check multi voucher type execept default voucher types
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        public ResultArgs IsMultiVoucherTypes(Int32 projectid, string BaseVouchers = "")
        {
            ResultArgs resultarg = new ResultArgs();
            resultarg.Success = false;
            resultarg.ReturnValue = false;
            using (ProjectSystem projectsys = new ProjectSystem())
            {
                ResultArgs resultArg = projectsys.ProjectVouchers(projectid);
                if (resultArg != null && resultArg.Success)
                {
                    DataTable dtVoucherTypes = resultArg.DataSource.Table;
                    if (!string.IsNullOrEmpty(BaseVouchers))
                    {
                        dtVoucherTypes.DefaultView.RowFilter = projectsys.AppSchema.Voucher.VOUCHER_TYPEColumn.ColumnName + " IN (" + BaseVouchers + ")";
                        dtVoucherTypes = dtVoucherTypes.DefaultView.ToTable();
                    }

                    var query = from row in dtVoucherTypes.AsEnumerable()
                                group row by row.Field<UInt32>("VOUCHER_TYPE") into MultiVoucherTypes
                                where MultiVoucherTypes.Count() > 1
                                select new
                                {
                                    VooucherType = MultiVoucherTypes.Key,
                                    NoOfVoucherTypes = MultiVoucherTypes.Count()
                                };

                    foreach (var vtype in query)
                    {
                        if (vtype.NoOfVoucherTypes > 1)
                        {
                            resultarg.Success = true;
                            resultarg.ReturnValue = true;
                            break;
                        }
                    }
                }
            }
            return resultarg;
        }


        /// <summary>
        /// This method is used to send/recommand selected budget to acmeerp portal for approve
        /// 1. Check basic validation and initialize sync budget details
        /// 2. Upload budget details to Acmeerp portal
        /// 3. If budget is uploaded sucessfully and its action is not approved, update budget action as Recommanded
        /// leave it if already approved
        /// </summary>
        /// <returns></returns>
        public ResultArgs UploadBudgetToAcmeerpPortal(Int32 BudgetId, bool IsMailSend)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                //1. Check basic validation and initialize sync budget details
                result = InitializeBudgetSync(BudgetId);
                if (result.Success)
                {
                    CloseWaitDialog();
                    using (BudgetSystem budgetSystem = new BudgetSystem())
                    {
                        budgetSystem.FillBudgetProperties(BudgetId);
                        if (budgetSystem.BudgetId > 0)
                        {
                            DateTime DateFrom = this.UtilityMember.DateSet.ToDate(budgetSystem.DateFrom, false);
                            DateTime DateTo = this.UtilityMember.DateSet.ToDate(budgetSystem.DateTo, false);
                            string BudgetProjectIds = budgetSystem.MultipleProjectId;
                            Int32 BudgetTypeId = budgetSystem.BudgetTypeId;
                            BudgetAction budgetaction = budgetSystem.BudgetAction;
                            DataSet dsBOBudgetDetails = result.DataSource.TableSet;
                            //2. Upload budget details to Acmeerp portal
                            DataSyncService.DataSynchronizerClient dataclientService = new DataSyncService.DataSynchronizerClient();
                            bool uploaded = dataclientService.UploadBudgetsDetails(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode, dsBOBudgetDetails, IsMailSend);

                            //3. If budget is uploaded sucessfully and its action is not approved, update budget action as Recommanded
                            //leave it if already approved
                            if (uploaded)
                            {
                                if (budgetaction != BudgetAction.Approved)
                                {
                                    using (BudgetSystem budgetsyste = new BudgetSystem())
                                    {
                                        result = budgetsyste.UpdateBudgetAction(BudgetId.ToString(), BudgetAction.Recommended);
                                        if (!result.Success)
                                        {
                                            result.Message = "Budget has been uploaded but Budget action is not updated";
                                        }
                                    }
                                }
                            }
                            else
                                result.Message = "Budget has not been uploaded to Head Office Portal";
                            this.CloseWaitDialog();
                        }
                    }
                }
            }
            catch (FaultException<DataSyncService.AcMeServiceException> ex)
            {
                // Modified by Mr Alwar 
                CloseWaitDialog();
                AcMELog.WriteLog(ex.Message);
                result.Message = ex.Detail.Message;
            }
            catch (CommunicationException ex)
            {
                CloseWaitDialog();
                result.Message = ex.Message;
                AcMELog.WriteLog(ex.Message);
            }
            catch (Exception ex)
            {
                CloseWaitDialog();
                result.Message = ex.Message;
                AcMELog.WriteLog(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                CloseWaitDialog();
            }
            return result;
        }

        /// <summary>
        /// This method is used to send/recommand selected budget to acmeerp portal for approve
        /// 1. Check basic validation and initialize sync budget details
        /// 2. Upload budget details to Acmeerp portal
        /// 3. Updated dudget details in BO db from acmeerp portal
        /// </summary>
        /// <returns></returns>
        public ResultArgs GetApprovedBudgetFromAcmeerpPortal(Int32 BudgetId)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                //1. Check basic validation and initialize sync budget details
                result = InitializeBudgetSync(BudgetId);
                if (result.Success)
                {
                    CloseWaitDialog();
                    using (BudgetSystem budgetSystem = new BudgetSystem())
                    {
                        budgetSystem.FillBudgetProperties(BudgetId);
                        if (budgetSystem.BudgetId > 0)
                        {
                            DateTime DateFrom = this.UtilityMember.DateSet.ToDate(budgetSystem.DateFrom, false);
                            DateTime DateTo = this.UtilityMember.DateSet.ToDate(budgetSystem.DateTo, false);
                            string BudgetProjectIds = budgetSystem.MultipleProjectId;
                            Int32 BudgetTypeId = budgetSystem.BudgetTypeId;
                            BudgetAction budgetaction = budgetSystem.BudgetAction;
                            DataSet dsBOBudgetDetails = result.DataSource.TableSet;
                            DataTable dtBOProjects = dsBOBudgetDetails.Tables["BudgetProject"];
                            //2. download selected budget details from Acmeerp portal
                            DataSyncService.DataSynchronizerClient dataclientService = new DataSyncService.DataSynchronizerClient();
                            DataSet dsHOBudgetDetails = dataclientService.GetApprovedBudgetsDetails(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode, DateFrom, DateTo, BudgetTypeId, dtBOProjects);
                            if (dsHOBudgetDetails.Tables.Count >= 3 && dsHOBudgetDetails.Tables.Contains("BudgetMaster") &&
                                dsHOBudgetDetails.Tables.Contains("BudgetProject") && dsHOBudgetDetails.Tables.Contains("BudgetLedger"))
                            {
                                using (ImportVoucherSystem importbudget = new ImportVoucherSystem())
                                {
                                    //3. Updated dudget details in BO db from acmeerp portal
                                    result = importbudget.UpdateBudgetDetails(0, dsHOBudgetDetails);

                                    //4. Update budget action as approved
                                    if (result.Success)
                                    {
                                        using (BudgetSystem budgetsyste = new BudgetSystem())
                                        {
                                            result = budgetsyste.UpdateBudgetAction(BudgetId.ToString(), BudgetAction.Approved);
                                            if (!result.Success)
                                            {
                                                result.Message = "Budget has been downloaded but Budget action is not updated";
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                result.Message = "HO Budget details are not available or Budget is not yet approved in portal";
                            }
                            this.CloseWaitDialog();
                        }
                    }
                }
            }
            catch (FaultException<DataSyncService.AcMeServiceException> ex)
            {
                // Modified by Mr Alwar 
                CloseWaitDialog();
                AcMELog.WriteLog(ex.Message);
                result.Message = ex.Detail.Message;
            }
            catch (CommunicationException ex)
            {
                CloseWaitDialog();
                result.Message = ex.Message;
                AcMELog.WriteLog(ex.Message);
            }
            catch (Exception ex)
            {
                CloseWaitDialog();
                result.Message = ex.Message;
                AcMELog.WriteLog(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                CloseWaitDialog();
            }
            return result;
        }

        /// <summary>
        /// This method is used to make initialize for sync for budget
        /// 1. Check internet connection, latest license
        /// 2. Verify budget projects, ledgers are avilable in acmeerp portal
        /// 3. Return selected budget details
        /// </summary>
        /// <returns></returns>
        private ResultArgs InitializeBudgetSync(Int32 budgetid)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                this.ShowWaitDialog(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.CONNECTING_ACMEERP_PORTAL));
                //1. Check internet connection, latest license
                if (this.CheckForInternetConnectionhttp())
                {
                    CloseWaitDialog();
                    if (!string.IsNullOrEmpty(this.AppSetting.BranchOfficeCode) && !string.IsNullOrEmpty(this.AppSetting.HeadofficeCode))
                    {
                        DataSyncService.DataSynchronizerClient dataclientService = new DataSyncService.DataSynchronizerClient();
                        //1. Check internet connection, latest license
                        if (dataclientService.IsLatestLicenseAvailable(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode, this.UtilityMember.DateSet.ToDate(this.AppSetting.LicenseKeyGeneratedDate, false)))
                        {
                            using (BudgetSystem budgetSystem = new BudgetSystem())
                            {
                                budgetSystem.FillBudgetProperties(budgetid);
                                if (budgetSystem.BudgetId > 0)
                                {
                                    DateTime DateFrom = this.UtilityMember.DateSet.ToDate(budgetSystem.DateFrom, false);
                                    DateTime DateTo = this.UtilityMember.DateSet.ToDate(budgetSystem.DateTo, false);
                                    string BudgetProjectIds = budgetSystem.MultipleProjectId;
                                    Int32 BudgetTypeId = budgetSystem.BudgetTypeId;
                                    BudgetAction budgetaction = budgetSystem.BudgetAction;

                                    bool ExportStatus = true;
                                    if (this.AppSetting.IS_SDB_INM)
                                    {
                                        ExportStatus = dataclientService.GetExportDatatoPortalExists(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode, DateFrom);

                                        if (!ExportStatus)
                                        {
                                            this.Cursor = Cursors.Default;
                                            MessageBox.Show("Previous Year Export data not available in portal. Please check with Head Office.", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            result.Success = false;
                                            result.Message = "Export validation failed – previous year data not found.";
                                            return result;
                                        }
                                    }

                                    using (ExportMasters exportbuget = new ExportMasters())
                                    {
                                        this.ShowWaitDialog("Validating Budget Projects and Ledgers in Portal");
                                        this.Cursor = Cursors.WaitCursor;
                                        result = exportbuget.GetBudget(0, DateFrom, DateTo, BudgetProjectIds, BudgetTypeId);
                                        if (result.Success)
                                        {
                                            DataSet dsBOBudgetDetails = result.DataSource.TableSet;
                                            this.Cursor = Cursors.WaitCursor;
                                            //On 27/02/2020, 
                                            if (dsBOBudgetDetails.Tables.Count >= 3 && dsBOBudgetDetails.Tables.Contains("BudgetMaster") && dsBOBudgetDetails.Tables.Contains("BudgetProject") && dsBOBudgetDetails.Tables.Contains("BudgetLedger"))
                                            {
                                                //2. Verify budget projects, ledgers are avilable in acmeerp portal
                                                DataTable dtMistMatchedProjects = dataclientService.GetMismatchedProjects(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode, dsBOBudgetDetails.Tables["BudgetProject"]);
                                                if (dtMistMatchedProjects.Rows.Count == 0)
                                                {
                                                    DataTable dtMismatchedLedgers = dataclientService.GetMismatchedLedgers(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode, dsBOBudgetDetails.Tables["BUdgetLedger"]);
                                                    if (dtMismatchedLedgers.Rows.Count == 0)
                                                    {
                                                        //3. Return selected budget detials which contains Budget master, Budget ledger and budget project
                                                        this.Cursor = Cursors.Default;
                                                        result.DataSource.Data = dsBOBudgetDetails;
                                                        result.Success = true;
                                                    }
                                                    else
                                                    {
                                                        CloseWaitDialog();
                                                        result.Message = "Branch Office Ledgers are mismatched in Head Office.";
                                                        this.Cursor = Cursors.Default;
                                                        frmMismatchedLedgerList mismatched = new frmMismatchedLedgerList(dtMismatchedLedgers);
                                                        mismatched.Owner = this;
                                                        mismatched.ShowDialog();
                                                    }
                                                }
                                                else
                                                {
                                                    CloseWaitDialog();
                                                    this.Cursor = Cursors.Default;
                                                    //resultArgs.Message = "Branch Office Projects are mismatched in Head Office.";
                                                    result.Message = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_VOUCHER_BO_PROJECT_MISMACTH_HO_INFO);
                                                    frmMismatchedDetails mismatched = new frmMismatchedDetails(dtMistMatchedProjects);
                                                    mismatched.Owner = this;
                                                    mismatched.ShowDialog();
                                                }
                                            }
                                            else
                                            {
                                                this.Cursor = Cursors.Default;
                                                result.Message = "Budget details are not available";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    this.Cursor = Cursors.Default;
                                    result.Message = "Budget details are not available";
                                }
                            }
                        }
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        result.Message = "License branch details are not available";
                    }
                }
                else
                {
                    CloseWaitDialog();
                    this.Cursor = Cursors.Default;
                    //resultArgs.Message = "Unable to reach Portal. Please check your internet connection.";
                    result.Message = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.UNABLE_TO_REACH_PORTAL_CHECK_INTERNET_CONNECTION);
                }
            }
            catch (FaultException<DataSyncService.AcMeServiceException> ex)
            {
                // Modified by Mr Alwar 
                this.Cursor = Cursors.Default;
                CloseWaitDialog();
                AcMELog.WriteLog(ex.Message);
                result.Message = ex.Detail.Message;
            }
            catch (CommunicationException ex)
            {
                this.Cursor = Cursors.Default;
                CloseWaitDialog();
                result.Message = ex.Message;
                AcMELog.WriteLog(ex.Message);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                CloseWaitDialog();
                result.Message = ex.Message;
                AcMELog.WriteLog(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                CloseWaitDialog();
            }

            return result;
        }

        /// <summary>
        /// On 30/05/2019
        /// Check current license key is mismtached
        /// Check current db projects and compare with HO projects, it means db and license key differ
        /// </summary>
        public bool IsLicenseKeyMismatchedByHoProjects()
        {
            bool Rtn = true;
            try
            {
                //if (this.CheckAcmeerpWebserviceRunning())
                //{
                DataSyncService.DataSynchronizerClient dataclientService = new DataSyncService.DataSynchronizerClient();
                using (ProjectSystem projectsys = new ProjectSystem())
                {
                    ResultArgs resultarg = projectsys.FetchProjects();
                    if (resultarg.Success && resultarg.DataSource.Table != null)
                    {
                        DataTable dtBOProjects = resultarg.DataSource.Table;

                        //On 27/02/2024, If Acmeerp portal database base is not connecting, it takes long time to validate,
                        //So fix time frame.
                        var time = new TimeSpan(0, 0, 6);
                        dataclientService.Endpoint.Binding.CloseTimeout = time;
                        dataclientService.Endpoint.Binding.OpenTimeout = time;
                        dataclientService.Endpoint.Binding.ReceiveTimeout = time;
                        dataclientService.Endpoint.Binding.SendTimeout = time;

                        DataTable dtMisMatchedProejcts = dataclientService.GetMismatchedProjects(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode, dtBOProjects);
                        //On 03/10/2019, Atleast any one project must be matached with portal projects, eraliter it will check, must be matached with all projects
                        //Rtn = !(dtMisMatchedProejcts.Rows.Count == 0);
                        Rtn = !(dtMisMatchedProejcts.Rows.Count == 0 || dtBOProjects.Rows.Count > dtMisMatchedProejcts.Rows.Count);
                    }
                    else
                    {
                        Rtn = true;
                        AcMELog.WriteLog("Error in checking IsLicenseKeyMismatchedByHoProjects - Mismatching License Key " + resultarg.Message);
                    }
                }
                //}
            }
            catch (Exception err)
            {
                AcMELog.WriteLog("Error in checking IsLicenseKeyMismatchedByHoProjects - Mismatching License Key " + err.Message);
                Rtn = true;
            }
            return Rtn;
        }

        /// <summary>
        /// On 05/06/2019
        /// Check current license key's Location and DB location
        /// </summary>
        public bool IsLicenseKeyMismatchedByLicenseKeyDBLocation()
        {
            bool Rtn = true;
            try
            {
                string[] branchlocations = AppSetting.BranchLocations.Split(',');
                int pos = Array.IndexOf(branchlocations, AppSetting.Location);
                if (pos > -1)
                {
                    Rtn = false;
                }
                else
                {
                    Rtn = true;
                    AcMELog.WriteLog("Checking IsLicenseKeyMismatchedByLicenseKeyDBLocation - Mismatching License Key, DB Location is not found in License Key");
                }
            }
            catch (Exception err)
            {
                AcMELog.WriteLog("Error in checking IsLicenseKeyMismatchedByLicenseKeyDBLocation - Mismatching License Key " + err.Message);
                Rtn = true;
            }
            return Rtn;
        }

        /// <summary>
        /// On 23/01/2025 - To current date time from Acmeeerp portal
        /// </summary>
        /// <returns></returns>
        public DateTime GetAcmeerpPortalDateTime()
        {
            DateTime rtn = DateTime.MinValue;

            try
            {
                //1. Check internet connection, latest license
                if (this.CheckForInternetConnectionhttp())
                {
                    using (DataSyncService.DataSynchronizerClient objservice = new DataSyncService.DataSynchronizerClient())
                    {
                        //On 27/02/2024, If Acmeerp portal database base is not connecting, it takes long time to validate,
                        //So fix time frame.
                        var time = new TimeSpan(0, 0, 6);
                        objservice.Endpoint.Binding.CloseTimeout = time;
                        objservice.Endpoint.Binding.OpenTimeout = time;
                        objservice.Endpoint.Binding.ReceiveTimeout = time;
                        objservice.Endpoint.Binding.SendTimeout = time;

                        string serverdatetime = objservice.GetCurrentServerDate();
                        rtn = UtilityMember.DateSet.ToDate(serverdatetime, true);
                    }
                }
            }
            catch (Exception err)
            {
                AcMELog.WriteLog("Not able to get current date time " + err.Message);
            }
            return rtn;
        }

        public bool IsServiceAlive(string URL)
        {
            bool Rtn = false;
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead(URL))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                Rtn = false;
            }

            return Rtn;
        }

        /// <summary>
        /// On 15/12/2020, To copy MultipleDBXML to application path. For safety purpose
        /// </summary>
        public void TakeBackup_MultipleDBXML()
        {
            try
            {
                if (AppSetting.AccesstoMultiDB == (int)YesNo.Yes && File.Exists(SettingProperty.RestoreMultipleDBPath))
                {
                    DataSet dtDb = XMLConverter.ConvertXMLToDataSet(SettingProperty.RestoreMultipleDBPath);

                    //as on 21/12/2021, again check multidb xml before taking backup file
                    if (dtDb.Tables.Count > 0)
                    {
                        string multipledbbackfile = Path.Combine(SettingProperty.ApplicationStartUpPath, "Backup_" + SettingProperty.RestoreMultipleDBFileName);
                        File.Copy(SettingProperty.RestoreMultipleDBPath, multipledbbackfile, true);
                    }
                }
            }
            catch (Exception err)
            {
                ShowMessageBoxWarning("Could not take backup of Multiple DB XML file " + err.Message);
            }
        }

        /// <summary>
        /// On 25/03/2021,
        /// 
        /// For SDBINM Auditors suggested to skip below mentioned Ledgers for Voucher Entries
        /// </summary>
        public DataTable ForSDBINMSkipLedgers(string voucherdate, DataTable dtLedgers)
        {
            if (AppSetting.IS_SDB_INM)
            {
                if (dtLedgers != null && dtLedgers.Rows.Count > 0)
                {
                    //#. For Skipping Auditor mentioned Ledgers ***********************************************************************
                    bool canSkipLedgers = false;
                    //For Few Places, Forcely skip those ledgers like Merge Ledgers
                    if (string.IsNullOrEmpty(voucherdate))
                    {
                        canSkipLedgers = true;
                    }
                    else
                    {
                        DateTime vdate = UtilityMember.DateSet.ToDate(voucherdate, false);
                        //canSkipLedgers = (vdate >= AppSetting.SDBINM_EnforceLedgersYearFrom && 
                        //        UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false) >= AppSetting.SDBINM_EnforceLedgersYearFrom);

                        canSkipLedgers = (vdate >= AppSetting.SDBINM_EnforceLedgersYearFrom);
                    }

                    //1. If Voucher Date and Current FY is greater then 01/04/2021, We have to skip those ledgers
                    if (canSkipLedgers && !string.IsNullOrEmpty(AppSetting.SDBINM_SkippedLedgerIds))
                    {
                        string filter = "LEDGER_ID NOT IN (" + AppSetting.SDBINM_SkippedLedgerIds + ")";

                        //If already row filter assigned
                        dtLedgers = dtLedgers.DefaultView.ToTable();
                        dtLedgers.DefaultView.RowFilter = filter;
                        dtLedgers = dtLedgers.DefaultView.ToTable();
                    }
                    //********************************************************************************************************************

                    //#. For Skipping Fixed Asset Ledgers If Asset Module enabled ********************************************************

                    //# Time being commanded by Chinna in order to allow to enter fixed Asset Transaction
                    //if (SettingProperty.EnableAsset && !string.IsNullOrEmpty(voucherdate))
                    //{
                    //    DateTime vdate = UtilityMember.DateSet.ToDate(voucherdate, false);

                    //    if (vdate >= AppSetting.SDBINM_EnforceLedgersYearFrom)
                    //    {
                    //        dtLedgers.DefaultView.RowFilter = "GROUP_ID NOT IN (" + (Int32)TDSLedgerGroup.FixedAsset + ")";
                    //        dtLedgers = dtLedgers.DefaultView.ToTable();
                    //    }
                    //}
                    //********************************************************************************************************************

                }

            }

            return dtLedgers;
        }

        /// <summary>
        ///  On 30/04/2021, to check given License key is valid
        /// </summary>
        /// <param name="licensekayPath"></param>
        /// <returns></returns>
        public bool ValidateLiceseKey(string licensekayPath)
        {
            bool rtn = false;
            try
            {
                if (!String.IsNullOrEmpty(licensekayPath) && File.Exists(licensekayPath))
                {
                    DataSet dsLicenseKey = new DataSet();
                    dsLicenseKey.ReadXml(licensekayPath);
                    if (dsLicenseKey.Tables.Count > 0)
                    {
                        if (dsLicenseKey.Tables[0].TableName.ToUpper() == "LICENSEKEY" && dsLicenseKey.Tables[0].Columns.Contains("LICENSE_KEY_NUMBER"))
                        {
                            rtn = true;
                        }
                    }

                }
            }
            catch (Exception err)
            {
                ShowMessageBoxWarning("Invalid License Key " + err.Message);
            }
            return rtn;
        }

        /// <summary>
        /// On 19/07/2021, to set lookup popup window size
        /// </summary>
        public void SetGridLookPopupWindowSize(GridLookUpEdit grdlk)
        {
            if (grdlk != null)
            {
                RepositoryItemGridLookUpEdit properties = grdlk.Properties;
                properties.PopupFormSize = new Size(grdlk.Width, properties.PopupFormSize.Height);
                properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            }
        }

        /// <summary>
        /// On 21/09/2021, To get Voucehr Locked details
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="ProjectName"></param>
        /// <param name="dVoucherDate"></param>
        /// <returns></returns>
        public ResultArgs GetAuditVoucherLockedDetails(int ProjectId, DateTime dVoucherDate)
        {
            ResultArgs Result = new ResultArgs();
            try
            {
                using (AuditLockTransSystem AuditSystem = new AuditLockTransSystem())
                {
                    AuditSystem.ProjectId = ProjectId;
                    AuditSystem.VoucherDateTo = dVoucherDate;
                    Result = AuditSystem.FetchAuditLockDetailsForProjectAndDate();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox("Unable to check Voucher date is locked - " + ex.Message + Environment.NewLine + ex.Source);
            }
            finally
            {

            }
            return Result;
        }

        /// <summary>
        /// On 20/09/2021, To Check given Date is locked for Voucher
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="dtTransDate"></param>
        /// <returns></returns>
        public bool IsVoucherLockedForDate(int ProjectId, DateTime dVoucherDate, bool showMessage, string ProjectName = "")
        {
            bool rtn = false;
            bool lockexists = false;
            DateTime LockFromDate = DateTime.MinValue;
            DateTime LockToDate = DateTime.MinValue;
            string LockProjectName = string.Empty;
            try
            {
                ResultArgs Result = new ResultArgs();
                using (AuditLockTransSystem AuditSystem = new AuditLockTransSystem())
                {
                    AuditSystem.ProjectId = ProjectId;
                    AuditSystem.VoucherDateTo = dVoucherDate;
                    Result = AuditSystem.FetchAuditLockDetailsForProjectAndDate();
                    if (Result != null && Result.Success && Result.DataSource.Table != null && Result.DataSource.Table.Rows.Count > 0)
                    {
                        LockFromDate = this.UtilityMember.DateSet.ToDate(Result.DataSource.Table.Rows[0][AuditSystem.AppSchema.AuditLockTransType.DATE_FROMColumn.ColumnName].ToString(), false);
                        LockFromDate = this.UtilityMember.DateSet.ToDate(Result.DataSource.Table.Rows[0][AuditSystem.AppSchema.AuditLockTransType.DATE_TOColumn.ColumnName].ToString(), false);
                        LockProjectName = Result.DataSource.Table.Rows[0][AuditSystem.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                        lockexists = true;
                        rtn = true;
                    }
                    else
                    {
                        //On 07/02/2024, For SDBINM, Lock Voucehrs before grace period
                        if (this.AppSetting.IS_SDB_INM && this.AppSetting.VoucherGraceDays > 0)
                        {
                            LockFromDate = this.AppSetting.GraceLockDateFrom;
                            LockToDate = this.AppSetting.GraceLockDateTo;

                            //Check temporary relaxation
                            bool isEnforceTmpRelaxation = this.AppSetting.IsTemporaryGraceLockRelaxDate(dVoucherDate);
                            if (LockFromDate != DateTime.MinValue && LockToDate != DateTime.MinValue)
                            {
                                if ((dVoucherDate >= LockFromDate && dVoucherDate <= LockToDate) && !isEnforceTmpRelaxation)
                                {
                                    LockProjectName = ProjectName;
                                    if (string.IsNullOrEmpty(ProjectName))
                                    {
                                        using (ProjectSystem projectsys = new ProjectSystem(ProjectId))
                                        {
                                            LockProjectName = projectsys.ProjectName;
                                        }
                                    }
                                    rtn = true;
                                }
                            }
                        }
                        else
                        {
                            LockFromDate = LockFromDate = DateTime.MinValue;
                            rtn = false;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox("Unable to Check Voucher date is locked - " + ex.Message + Environment.NewLine + ex.Source);
                rtn = true;
            }
            finally
            {
                if (rtn & lockexists && showMessage)
                {
                    this.ShowMessageBox("Unable to Post/Delete Vouchers." + System.Environment.NewLine +
                             this.GetMessage(MessageCatalog.Common.COMMON_VOUCHER_LOCKED) + " '" + LockProjectName + "'" +
                             " during the period of '" + this.UtilityMember.DateSet.ToDate(LockFromDate.ToShortDateString()) + "'" +
                             " - " + this.UtilityMember.DateSet.ToDate(LockToDate.ToShortDateString()));
                }
                else if (rtn & showMessage)
                {
                    this.ShowMessageBox("Unable to Post/Delete Vouchers." + System.Environment.NewLine +
                             this.GetMessage(MessageCatalog.Common.COMMON_VOUCHER_LOCKED) + " '" + LockProjectName + "'");
                }
            }
            return rtn;
        }

        /// <summary>
        /// On 25/09/2021, To Check given Date is locked for Voucher
        /// </summary>
        public bool IsVoucherLockedForDateRange(int ProjectId, DateTime dVoucherFrmDate, DateTime dVoucherToDate, bool showMessage, string ProjectName = "")
        {
            bool rtn = false;
            bool lockexists = false;
            //DateTime dLockDateFrom = DateTime.MinValue;
            //DateTime dLockDateTo = DateTime.MinValue;
            string LockProjectName = string.Empty;
            try
            {
                DataTable dtAuditLockDetails = null;
                using (AuditLockTransSystem AuditSystem = new AuditLockTransSystem())
                {
                    AuditSystem.ProjectId = ProjectId;
                    AuditSystem.DateFrom = dVoucherFrmDate;
                    AuditSystem.DateTo = dVoucherToDate;
                    ResultArgs result = AuditSystem.FetchAuditDetailByProjectDateRange();
                    if (result != null && result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                    {
                        dtAuditLockDetails = result.DataSource.Table;
                        //dLockDateFrom = this.UtilityMember.DateSet.ToDate(dtAuditLockDetails.Rows[0][AuditSystem.AppSchema.AuditLockTransType.DATE_FROMColumn.ColumnName].ToString(), false);
                        //dLockDateTo = this.UtilityMember.DateSet.ToDate(dtAuditLockDetails.Rows[0][AuditSystem.AppSchema.AuditLockTransType.DATE_TOColumn.ColumnName].ToString(), false);
                        LockProjectName = dtAuditLockDetails.Rows[0][AuditSystem.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                        rtn = true;
                        lockexists = true;
                    }
                    else
                    {
                        //On 07/02/2024, For SDBINM, Lock Voucehrs before grace period
                        if (this.AppSetting.IS_SDB_INM && this.AppSetting.VoucherGraceDays > 0)
                        {
                            //On 07/02/2024, For SDBINM, Lock Voucehrs before grace period
                            if (dtAuditLockDetails == null && this.AppSetting.IS_SDB_INM && this.AppSetting.VoucherGraceDays > 0)
                            {
                                dtAuditLockDetails = AuditSystem.AppSchema.AuditLockTransType.DefaultView.ToTable();
                                DataRow dr = dtAuditLockDetails.NewRow();
                                dr[AuditSystem.AppSchema.AuditLockTransType.DATE_FROMColumn.ColumnName] = this.AppSetting.GraceLockDateFrom;
                                dr[AuditSystem.AppSchema.AuditLockTransType.DATE_TOColumn.ColumnName] = this.AppSetting.GraceLockDateTo;
                                dtAuditLockDetails.Rows.Add(dr);

                                //bool b = (startTime1 <= endTime2 && startTime2 <= endTime1;);
                                if ((dVoucherFrmDate <= this.AppSetting.GraceLockDateTo || dVoucherToDate <= this.AppSetting.GraceLockDateFrom))
                                {
                                    LockProjectName = ProjectName;
                                    rtn = true;
                                    lockexists = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
                rtn = true;
            }
            finally
            {
                if (rtn & lockexists && showMessage)
                {
                    this.ShowMessageBox("Unable to Post/Delete Vouchers." + System.Environment.NewLine +
                             this.GetMessage(MessageCatalog.Common.COMMON_VOUCHER_LOCKED) + " '" + LockProjectName + "'" +
                             " during the period of '" + dVoucherFrmDate.ToShortDateString() + "' - " + dVoucherToDate.ToShortDateString());
                }
                else if (rtn & showMessage)
                {
                    this.ShowMessageBox("Unable to Post/Delete Vouchers." + System.Environment.NewLine +
                             this.GetMessage(MessageCatalog.Common.COMMON_VOUCHER_LOCKED) + " '" + LockProjectName + "'");
                }
            }

            return rtn;
        }


        /// <summary>
        /// On 25/03/2022, To request to enable Receipt Module key file
        /// </summary>
        /// <returns></returns>
        public ResultArgs RequestReceiptModule()
        {
            bool requestRightsProblem = false;
            //string file = Path.Combine(SettingProperty.ApplicationStartUpPath, SettingProperty.AcmeerpLCFile);
            string file = Path.Combine(Application.StartupPath, SettingProperty.AcmeerpLCFile);

            ResultArgs result = new ResultArgs();
            DataTable dtResultFromPortal = new DataTable();
            string requestcode = string.Empty;
            Int32 requestStatus = (Int32)LCBranchModuleStatus.Disabled;
            string rtnmessage = string.Empty; ;
            byte[] filedetails = new byte[0];

            try
            {
                string macaddress = this.GetMACAddress();
                string ipaddress = this.GetIPAddress();
                if (this.AppSetting.IS_SDB_INM)
                {
                    this.ShowWaitDialog("Connecting to Acmeerp.erp portal");
                    AcMELog.WriteLog("Start to make request to use Receipt Module from Acme.erp Portal");
                    if (this.CheckForInternetConnectionhttp())
                    {
                        CloseWaitDialog();
                        if (!string.IsNullOrEmpty(this.AppSetting.BranchOfficeCode) && !string.IsNullOrEmpty(this.AppSetting.HeadofficeCode) && !string.IsNullOrEmpty(this.AppSetting.Location))
                        {
                            DataSyncService.DataSynchronizerClient dataclientService = new DataSyncService.DataSynchronizerClient();
                            //1. Check internet connection, latest license
                            if (dataclientService.IsLatestLicenseAvailable(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode, this.UtilityMember.DateSet.ToDate(this.AppSetting.LicenseKeyGeneratedDate, false)))
                            {
                                requestRightsProblem = true;
                                AcMELog.WriteLog("Before request rights from Acme.erp portal");

                                //string tst = dataclientService.GetAcmeERPProductVersion();

                                dtResultFromPortal = dataclientService.RequestLocalCommunityKey(this.AppSetting.BaseLicensekeyNumber, this.AppSetting.BaseLicenseHeadOfficeCode,
                                               this.AppSetting.BaseLicenseBranchCode, this.AppSetting.BaseLicenseLocation, ipaddress, macaddress, this.LoginUser.LoginUserName);

                                if (dtResultFromPortal != null && dtResultFromPortal.Rows.Count > 0)
                                {
                                    using (AuditorSystem aduditorsystem = new AuditorSystem())
                                    {
                                        requestcode = dtResultFromPortal.Rows[0][aduditorsystem.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_REQUEST_CODEColumn.ColumnName].ToString();
                                        requestStatus = this.UtilityMember.NumberSet.ToInteger(dtResultFromPortal.Rows[0][aduditorsystem.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_RECEIPT_MODULE_STATUSColumn.ColumnName].ToString());
                                        rtnmessage = dtResultFromPortal.Rows[0][aduditorsystem.AppSchema.LcBranchEnableTrackModules.RETURN_MESSAGEColumn.ColumnName].ToString();

                                        if (string.IsNullOrEmpty(rtnmessage) && requestStatus == (Int32)LCBranchModuleStatus.Approved)
                                        { //Approved sucessfully and get rights
                                            AcMELog.WriteLog("Approved sucessfully and get rights");
                                            if (dtResultFromPortal.Rows[0]["DATA"] != null)
                                            {
                                                filedetails = (byte[])dtResultFromPortal.Rows[0]["DATA"];
                                            }
                                            result.Success = true;
                                        }
                                        else
                                        {
                                            result.Message = rtnmessage;
                                            result.Success = true;
                                            //To update Recent Module rights
                                            AcMELog.WriteLog("Not Approved (Update Recent Module rights)");
                                            using (UISettingSystem uisystemsetting = new UISettingSystem())
                                            {
                                                uisystemsetting.BaseAcmeerpSaveUISettingDetails(FinanceSetting.BranchReceiptModuleStatus, requestStatus.ToString(), this.LoginUser.LoginUserId);
                                                if (requestStatus == (Int32)LCBranchModuleStatus.Approved)
                                                    this.AppSetting.BaseLicenseBranchStatus = LCBranchModuleStatus.Approved;
                                                else if (requestStatus == (Int32)LCBranchModuleStatus.Requested)
                                                    this.AppSetting.BaseLicenseBranchStatus = LCBranchModuleStatus.Requested;
                                                else
                                                    this.AppSetting.BaseLicenseBranchStatus = LCBranchModuleStatus.Disabled;
                                            }
                                        }
                                        result.ReturnValue = requestStatus;
                                    }
                                }

                                AcMELog.WriteLog("after got rights from Acme.erp portal");
                                if (result.Success && filedetails.Length > 0)
                                {
                                    AcMELog.WriteLog("Got approved and allowing to convert to file");
                                    if (filedetails.Length > 0)
                                    {
                                        FileStream fs = new FileStream(file, FileMode.Create, FileAccess.ReadWrite);
                                        BinaryWriter bw = new BinaryWriter(fs);
                                        bw.Write(filedetails);
                                        bw.Close();
                                        File.WriteAllBytes(file, filedetails); // Requires System.IO
                                        result.Success = true;

                                        if (File.Exists(file))
                                        {
                                            result = UpdateLCdetails1(requestcode, this.AppSetting.LicenseKeyNumber, this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode,
                                                                this.AppSetting.Location, ipaddress, macaddress);
                                            if (result.Success)
                                            {
                                                result = UpdateLCdetails2(requestcode, this.AppSetting.LicenseKeyNumber, this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode,
                                                                this.AppSetting.Location, ipaddress, macaddress);
                                                if (result.Success)
                                                {
                                                    result.ReturnValue = LCBranchModuleStatus.Approved;

                                                    //To update Recent Module rights
                                                    using (UISettingSystem uisystemsetting = new UISettingSystem())
                                                    {
                                                        uisystemsetting.BaseAcmeerpSaveUISettingDetails(FinanceSetting.BranchReceiptModuleStatus, ((Int32)LCBranchModuleStatus.Approved).ToString(), this.LoginUser.LoginUserId);
                                                    }

                                                    AcMELog.WriteLog("Sucessfully updated rights to use Receipts Module in local system.");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            AcMELog.WriteLog("Receipts Module rights not found in local system");
                                        }
                                    }
                                    else
                                    {
                                        result.Message = "This license/system don't have permission to use Receipts Modue. Make Request to Acme.erp portal.";
                                        AcMELog.WriteLog("Not recevied premisson to use Receipt Module from Acme.erp Portal");
                                    }
                                }
                                else
                                {
                                    AcMELog.WriteLog("Not recevied premisson to use Receipt Module from Acme.erp Portal");
                                }
                            }
                        }
                        else
                        {
                            CloseWaitDialog();
                            result.Message = "Branch details are not found in License Key";
                        }
                    }
                    else
                    {
                        CloseWaitDialog();
                        result.Message = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.UNABLE_TO_REACH_PORTAL_CHECK_INTERNET_CONNECTION);
                    }
                    AcMELog.WriteLog("End to make request to use Receipt Module to Acme.erp Portal");
                }
            }
            catch (FaultException<DataSyncService.AcMeServiceException> ex)
            {
                // Modified by Mr Alwar 
                this.Cursor = Cursors.Default;
                CloseWaitDialog();
                AcMELog.WriteLog(ex.Message);
                result.Message = ex.Detail.Message;
            }
            catch (CommunicationException ex)
            {
                this.Cursor = Cursors.Default;
                CloseWaitDialog();
                result.Message = ex.Message;
                AcMELog.WriteLog(ex.Message);
            }
            catch (IOException errIO)
            {
                file = string.Empty;
                this.Cursor = Cursors.Default;
                CloseWaitDialog();
                result.Message = errIO.Message;
                AcMELog.WriteLog(errIO.Message);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                CloseWaitDialog();
                result.Message = ex.Message;
                AcMELog.WriteLog(ex.Message);
            }
            finally
            {
                if (!result.Success || filedetails.Length == 0)
                {
                    if (requestRightsProblem && File.Exists(file))
                    {
                        File.Delete(file);
                    }
                }

                this.Cursor = Cursors.Default;
                CloseWaitDialog();
            }

            return result;
        }

        /// <summary>
        /// On 07/04/2022, This method should be called end of the form load event
        /// To enforce Enable and Track Receipt Module
        /// # to disable Change Date, Change Project Always for Modify Voucher
        /// 
        /// On 05/05/2022, As SDBINM Province wanted to enforce and track Receipt module from FY 01/04/2022 onwards
        /// </summary>
        public void EnforceReceiptModule(object[] ctls, bool modifyMode = false)
        {
            //On 07/04/2022, to lock receipt module -----------------------------------------------------------------
            //On 05/05/2022, As SDBINM Province wanted to enforce and track Receipt module from FY 01/04/2022 onwards

            if (AppSetting.IS_SDB_INM && (UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false) >= UtilityMember.DateSet.ToDate(SettingProperty.Enforce_Receipt_Module_FY, false)))
            {
                bool enabled = AppSetting.ENABLE_TRACK_RECEIPT_MODULE;
                foreach (object ctl in ctls)
                {
                    if (ctl.GetType() == typeof(DevExpress.XtraNavBar.NavBarItem))
                    {
                        DevExpress.XtraNavBar.NavBarItem barItem = (ctl as DevExpress.XtraNavBar.NavBarItem);
                        barItem.Enabled = enabled;
                        if (barItem.SuperTip != null && !enabled)
                        {
                            barItem.SuperTip.Items.Add(MessageCatalog.Common.COMMON_RECEIPT_DISABLED_MESSAGE);
                        }
                    }
                    else if (ctl.GetType() == typeof(Bosco.Utility.Controls.ucToolBar))
                    {
                        Bosco.Utility.Controls.ucToolBar ucctl = (ctl as Bosco.Utility.Controls.ucToolBar);
                        ucctl.DisableEditButton = ucctl.DisableDeleteButton = ucctl.DisableInsertVoucher = ucctl.DisableMoveTransaction = enabled;

                        if (enabled)
                        {
                            ucctl.DisableMoveTransaction = false;
                        }
                    }
                    else if (ctl.GetType() == typeof(ACPP.Modules.UIControls.ucVoucherShortcut))
                    {
                        ACPP.Modules.UIControls.ucVoucherShortcut ucctl = (ctl as ACPP.Modules.UIControls.ucVoucherShortcut);
                        ucctl.LockReceipt = enabled;

                        if (modifyMode)
                        {
                            ucctl.LockProject = false;
                            ucctl.LockDate = false;
                            ucctl.LockNextVoucherDate = false;
                        }
                    }
                    else if (ctl.GetType() == typeof(ACPP.Modules.UIControls.ucAdditionalInfoMenu))
                    {
                        ACPP.Modules.UIControls.ucAdditionalInfoMenu ucctl = (ctl as ACPP.Modules.UIControls.ucAdditionalInfoMenu);
                        ucctl.LockDeleteVocuher = false;
                        ucctl.LockPrintVoucher = false;
                    }
                    else if (ctl.GetType() == typeof(DevExpress.XtraEditors.DateEdit))
                    {
                        if (modifyMode)
                        {
                            DevExpress.XtraEditors.DateEdit ctldate = (ctl as DevExpress.XtraEditors.DateEdit);
                            if (modifyMode)
                            {
                                ctldate.ToolTip = MessageCatalog.Common.COMMON_RECEIPT_DISABLED_MESSAGE;
                                ctldate.Enabled = false;
                            }
                        }
                    }
                    else if (ctl.GetType() == typeof(DevExpress.XtraEditors.RadioGroup))
                    {
                        DevExpress.XtraEditors.RadioGroup raGrp = (ctl as DevExpress.XtraEditors.RadioGroup);
                        raGrp.Enabled = enabled;
                        if (!enabled)
                        {
                            raGrp.ToolTip = MessageCatalog.Common.COMMON_RECEIPT_DISABLED_MESSAGE;
                        }

                        if (modifyMode)
                        {
                            raGrp.Enabled = false;
                        }
                        //raGrp.SelectedIndex = -1;
                    }
                    else if (ctl.GetType() == typeof(DevExpress.XtraEditors.Controls.RadioGroupItem))
                    {
                        DevExpress.XtraEditors.Controls.RadioGroupItem raGrpitem = (ctl as DevExpress.XtraEditors.Controls.RadioGroupItem);
                        raGrpitem.Enabled = enabled;
                    }
                    else if (ctl.GetType() == typeof(DevExpress.XtraEditors.CheckEdit))
                    {
                        DevExpress.XtraEditors.CheckEdit chk = (ctl as DevExpress.XtraEditors.CheckEdit);
                        if (enabled && modifyMode)
                        {
                            enabled = false;
                        }

                        chk.Enabled = enabled;
                        if (!enabled)
                        {
                            chk.ToolTip = MessageCatalog.Common.COMMON_RECEIPT_DISABLED_MESSAGE;
                        }
                    }
                }
            }
            //-------------------------------------------------------------------------------------------------------
        }

        /// <summary>
        /// On 30/03/2022, To update Client LC details
        /// </summary>
        /// <returns></returns>
        public ResultArgs UpdateLCdetails1(string ref1, string ref2, string ref3, string ref4, string ref5, string ref6, string ref7)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                AcMELog.WriteLog("Start:Update Rights2");
                result = UpdateLCdetailsInSystem(@"Software\BoscoSoft\AcMEERP", ref1, ref2, ref3, ref4, ref5, ref6, ref7);
                bool oldreg = (result.Success);

                //this.ShowMessageBox("Update in old path " + oldreg);

                result = UpdateLCdetailsInSystem(@"Software\BoscoSoft\Acme.erp", ref1, ref2, ref3, ref4, ref5, ref6, ref7);
                bool reg = (result.Success);
                //this.ShowMessageBox("Update in old path " + reg);

                if (!oldreg && !reg)
                {
                    result.Message = "Problem in updating rights";
                }
                else
                {
                    result.Message = string.Empty;
                    result.Success = true;
                }

                AcMELog.WriteLog("End:Update Rights2");
            }
            catch (Exception err)
            {
                result.Message = err.Message;
                AcMELog.WriteLog("Problem in updating rights2 " + err.Message);
            }
            finally
            {
                if (!result.Success && string.IsNullOrEmpty(result.Message))
                {
                    result.Message = "Problem in updating rights";
                }
            }

            return result;
        }

        /// <summary>
        /// Update LC details in system
        /// </summary>
        /// <returns></returns>
        private ResultArgs UpdateLCdetailsInSystem(string path, string ref1, string ref2, string ref3, string ref4, string ref5, string ref6, string ref7)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                RegistryKey root = Registry.LocalMachine.OpenSubKey(path, true);
                if (root != null)
                {
                    root.SetValue("ref1", CommonMethod.Encrept(ref1));
                    root.SetValue("ref2", CommonMethod.Encrept(ref2));
                    root.SetValue("ref3", CommonMethod.Encrept(ref3));
                    root.SetValue("ref4", CommonMethod.Encrept(ref4));
                    root.SetValue("ref5", CommonMethod.Encrept(ref5));
                    root.SetValue("ref6", CommonMethod.Encrept(ref6));
                    root.SetValue("ref7", CommonMethod.Encrept(ref7));
                    result.Success = true;
                    //this.ShowMessageBox("Update in Local Machine " + path);
                }

                root = Registry.CurrentUser.OpenSubKey(path, true);
                if (root != null)
                {
                    root.SetValue("ref1", CommonMethod.Encrept(ref1));
                    root.SetValue("ref2", CommonMethod.Encrept(ref2));
                    root.SetValue("ref3", CommonMethod.Encrept(ref3));
                    root.SetValue("ref4", CommonMethod.Encrept(ref4));
                    root.SetValue("ref5", CommonMethod.Encrept(ref5));
                    root.SetValue("ref6", CommonMethod.Encrept(ref6));
                    root.SetValue("ref7", CommonMethod.Encrept(ref7));
                    result.Success = true;
                    //this.ShowMessageBox("Update in Local USER " + path);
                }

                AcMELog.WriteLog("End:Update Rights2");
            }
            catch (Exception err)
            {
                result.Message = err.Message;
                AcMELog.WriteLog("Problem in updating rights2 " + err.Message);
            }

            return result;
        }


        /// <summary>
        /// Checking level1, dont check any null values and objects, if it is null it means corrupted
        /// </summary>
        /// <param name="ref1"></param>
        /// <param name="ref2"></param>
        /// <param name="ref3"></param>
        /// <param name="ref4"></param>
        /// <param name="ref5"></param>
        /// <param name="ref6"></param>
        /// <returns></returns>
        private ResultArgs CheckLCdetails2(string ref1, string ref2, string ref3, string ref4, string ref5, string ref6, string ref7)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                AcMELog.WriteLog("Start:Check Rights Level2");
                result = CheckLCdetailsInRegistry(@"Software\BoscoSoft\AcMEERP", ref1, ref2, ref3, ref4, ref5, ref6, ref7);
                bool oldreg = result.Success;

                result = CheckLCdetailsInRegistry(@"Software\BoscoSoft\Acme.erp", ref1, ref2, ref3, ref4, ref5, ref6, ref7);
                bool reg = result.Success;

                if (!oldreg && !reg)
                {
                    result.Success = false;
                }
                else
                {
                    result.Success = true;
                }

                AcMELog.WriteLog("Leve2 Status :: " + result.Success.ToString());
                AcMELog.WriteLog("End:Check Rights Level2");
            }
            catch (Exception err)
            {
                result.Message = err.Message;
                AcMELog.WriteLog("Problem in checking Rights Level2 " + err.Message);
            }

            return result;
        }

        private ResultArgs CheckLCdetailsInRegistry(string path, string ref1, string ref2, string ref3, string ref4, string ref5, string ref6, string ref7)
        {
            ResultArgs result = new ResultArgs();
            string logsno = (path == @"Software\BoscoSoft\AcMEERP" ? "2.1" : "2.2");
            try
            {
                AcMELog.WriteLog("Start:Check Rights Local Machine Level" + logsno);
                RegistryKey root = Registry.LocalMachine.OpenSubKey(path, false);
                if (root != null)
                {
                    //On 08/06/2022 Receipt Module Rights Changes Ref 1, root.GetValue("ref6").ToString() == CommonMethod.Decrept(ref6) &&
                    result.Success = (root.GetValue("ref1").ToString() == CommonMethod.Decrept(ref1) && root.GetValue("ref2").ToString() == CommonMethod.Decrept(ref2) &&
                    root.GetValue("ref3").ToString() == CommonMethod.Decrept(ref3) && root.GetValue("ref4").ToString() == CommonMethod.Decrept(ref4) &&
                    root.GetValue("ref5").ToString() == CommonMethod.Decrept(ref5) &&
                    root.GetValue("ref7").ToString() == CommonMethod.Decrept(ref7));
                    result.Success = true;

                    //On 08/06/2022 Receipt Module Rights Changes Ref 1, CommonMethod.Decrept(root.GetValue("ref6").ToString()) + Delimiter.Mew +
                    this.AppSetting.ModuleRightsDetails += "<br>2.1" + Delimiter.PipeLine + CommonMethod.Decrept(root.GetValue("ref1").ToString()) + Delimiter.Mew +
                                                          CommonMethod.Decrept(root.GetValue("ref2").ToString()) + Delimiter.Mew +
                                                          CommonMethod.Decrept(root.GetValue("ref3").ToString()) + Delimiter.Mew +
                                                          CommonMethod.Decrept(root.GetValue("ref4").ToString()) + Delimiter.Mew +
                                                          CommonMethod.Decrept(root.GetValue("ref5").ToString()) + Delimiter.Mew +
                                                          CommonMethod.Decrept(root.GetValue("ref7").ToString());

                    AcMELog.WriteLog("Local Machine Level" + logsno + " Status :: " + result.Success.ToString());
                }
                AcMELog.WriteLog("End:Check Rights Local Machine Level" + logsno);

                AcMELog.WriteLog("Start:Check Rights Current User Level" + logsno);
                root = Registry.CurrentUser.OpenSubKey(path, true);
                if (root != null)
                {
                    //On 08/06/2022 Receipt Module Rights Changes Ref 1, root.GetValue("ref6").ToString() == CommonMethod.Decrept(ref6) &&
                    result.Success = (root.GetValue("ref1").ToString() == CommonMethod.Decrept(ref1) && root.GetValue("ref2").ToString() == CommonMethod.Decrept(ref2) &&
                    root.GetValue("ref3").ToString() == CommonMethod.Decrept(ref3) && root.GetValue("ref4").ToString() == CommonMethod.Decrept(ref4) &&
                    root.GetValue("ref5").ToString() == CommonMethod.Decrept(ref5) &&
                    root.GetValue("ref7").ToString() == CommonMethod.Decrept(ref7));
                    result.Success = true;

                    //On 08/06/2022 Receipt Module Rights Changes Ref 1, CommonMethod.Decrept(root.GetValue("ref6").ToString()) + Delimiter.Mew +
                    this.AppSetting.ModuleRightsDetails += "<br>2.2" + Delimiter.PipeLine + CommonMethod.Decrept(root.GetValue("ref1").ToString()) + Delimiter.Mew +
                                                          CommonMethod.Decrept(root.GetValue("ref2").ToString()) + Delimiter.Mew +
                                                          CommonMethod.Decrept(root.GetValue("ref3").ToString()) + Delimiter.Mew +
                                                          CommonMethod.Decrept(root.GetValue("ref4").ToString()) + Delimiter.Mew +
                                                          CommonMethod.Decrept(root.GetValue("ref5").ToString()) + Delimiter.Mew +
                                                          CommonMethod.Decrept(root.GetValue("ref7").ToString());

                    AcMELog.WriteLog("Current Current User Level" + logsno + " Status :: " + result.Success.ToString());
                }
                AcMELog.WriteLog("End:Check Rights Current User Level" + logsno);
            }
            catch (Exception err)
            {
                result.Message = err.Message;
                AcMELog.WriteLog("Problem in CheckLCdetailsInRegistry Level " + logsno + err.Message);
            }

            return result;
        }

        /// <summary>
        /// On 30/03/2022, To update Client LC details
        /// </summary>
        /// <returns></returns>
        public ResultArgs UpdateLCdetails2(string ref1, string ref2, string ref3, string ref4, string ref5, string ref6, string ref7)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                AcMELog.WriteLog("Start:Update Rights3");
                using (UISettingSystem uisystemsetting = new UISettingSystem())
                {
                    uisystemsetting.BaseAcmeerpSaveUISettingDetails(FinanceSetting.LCRef1, CommonMethod.Encrept(ref1), this.LoginUser.LoginUserId);
                    uisystemsetting.BaseAcmeerpSaveUISettingDetails(FinanceSetting.LCRef2, CommonMethod.Encrept(ref2), this.LoginUser.LoginUserId);
                    uisystemsetting.BaseAcmeerpSaveUISettingDetails(FinanceSetting.LCRef3, CommonMethod.Encrept(ref3), this.LoginUser.LoginUserId);
                    uisystemsetting.BaseAcmeerpSaveUISettingDetails(FinanceSetting.LCRef4, CommonMethod.Encrept(ref4), this.LoginUser.LoginUserId);
                    uisystemsetting.BaseAcmeerpSaveUISettingDetails(FinanceSetting.LCRef5, CommonMethod.Encrept(ref5), this.LoginUser.LoginUserId);
                    uisystemsetting.BaseAcmeerpSaveUISettingDetails(FinanceSetting.LCRef6, CommonMethod.Encrept(ref6), this.LoginUser.LoginUserId);
                    uisystemsetting.BaseAcmeerpSaveUISettingDetails(FinanceSetting.LCRef7, CommonMethod.Encrept(ref7), this.LoginUser.LoginUserId);
                }
                result.Success = true;
                AcMELog.WriteLog("End:Update Rights3");
            }
            catch (Exception err)
            {
                result.Message = err.Message;
                AcMELog.WriteLog("Problem in updating rights3 " + err.Message);
            }

            return result;
        }

        /// <summary>
        /// Checking level1, dont check any null values and objects, if it is null it means corrupted
        /// </summary>
        /// <param name="ref1"></param>
        /// <param name="ref2"></param>
        /// <param name="ref3"></param>
        /// <param name="ref4"></param>
        /// <param name="ref5"></param>
        /// <param name="ref6"></param>
        /// <returns></returns>
        private ResultArgs CheckLCdetails3(string ref1, string ref2, string ref3, string ref4, string ref5, string ref6, string ref7)
        {
            string Ref1 = this.AppSetting.BaseLCRef1;
            string Ref2 = this.AppSetting.BaseLCRef2;
            string Ref3 = this.AppSetting.BaseLCRef3;
            string Ref4 = this.AppSetting.BaseLCRef4;
            string Ref5 = this.AppSetting.BaseLCRef5;
            string Ref6 = this.AppSetting.BaseLCRef6;
            string Ref7 = this.AppSetting.BaseLCRef7;

            ResultArgs result = new ResultArgs();
            try
            {
                AcMELog.WriteLog("Start:Check Rights Level3");

                //string test = CommonMethod.Decrept(Ref7);
                //test = CommonMethod.Decrept(test);
                //bool t = (Ref7 == CommonMethod.Decrept(ref7));

                //On 08/06/2022 Receipt Module Rights Changes Ref 1, Ref6 == CommonMethod.Decrept(ref6) &&
                result.Success = (Ref1 == CommonMethod.Decrept(ref1) && Ref2 == CommonMethod.Decrept(ref2) &&
                    Ref3 == CommonMethod.Decrept(ref3) && Ref4 == CommonMethod.Decrept(ref4) &&
                    Ref5 == CommonMethod.Decrept(ref5) &&
                    Ref7 == CommonMethod.Decrept(ref7));

                AcMELog.WriteLog("Level3 Status :: " + result.Success.ToString());
                AcMELog.WriteLog("End:Check Rights Level3");


                //On 08/06/2022 Receipt Module Rights Changes Ref 1, CommonMethod.Decrept(CommonMethod.Decrept(ref6)) + Delimiter.Mew + 
                this.AppSetting.ModuleRightsDetails += "<br>3" + Delimiter.PipeLine + CommonMethod.Decrept(Ref1) + Delimiter.Mew +
                                          CommonMethod.Decrept(Ref2) + Delimiter.Mew + CommonMethod.Decrept(Ref3) + Delimiter.Mew +
                                          CommonMethod.Decrept(Ref4) + Delimiter.Mew + CommonMethod.Decrept(Ref5) + Delimiter.Mew +
                                          CommonMethod.Decrept(Ref7);
            }
            catch (Exception err)
            {
                result.Message = err.Message;
                AcMELog.WriteLog("Problem in checking Rights level3 " + err.Message);
            }

            return result;
        }

        /// <summary>
        /// On 31/03/2022, To get base database location
        /// </summary>
        private void AssignBaseDatabaseReferenceDetails()
        {
            using (UISettingSystem uisystemsetting = new UISettingSystem())
            {
                //Base database branch receipt module status for login user
                ResultArgs result = uisystemsetting.BaseAcmeerpFetchUISettingDetails(UtilityMember.NumberSet.ToInteger(this.LoginUser.LoginUserId));
                if (result.Success && result.DataSource.TableView != null && result.DataSource.TableView.Count > 0)
                {
                    dtBaseDatabaseSettingDetails = result.DataSource.TableView.Table;
                    if (dtBaseDatabaseSettingDetails != null && dtBaseDatabaseSettingDetails.Rows.Count > 0)
                    {
                        dtBaseDatabaseSettingDetails.DefaultView.RowFilter = string.Empty;
                        dtBaseDatabaseSettingDetails.DefaultView.RowFilter = "NAME = 'BranchReceiptModuleStatus'";
                        if (dtBaseDatabaseSettingDetails.DefaultView.Count > 0)
                        {
                            Int32 basebranchreceiptstatus = UtilityMember.NumberSet.ToInteger(dtBaseDatabaseSettingDetails.DefaultView[0]["VALUE"].ToString());
                            this.AppSetting.BaseLicenseBranchStatus = (LCBranchModuleStatus)basebranchreceiptstatus;
                        }
                    }

                    string values = "'" + FinanceSetting.LCRef1.ToString() + "','" + FinanceSetting.LCRef2.ToString() + "'," +
                                        "'" + FinanceSetting.LCRef3.ToString() + "','" + FinanceSetting.LCRef4.ToString() + "'," +
                                        "'" + FinanceSetting.LCRef5.ToString() + "','" + FinanceSetting.LCRef6.ToString() + "'," +
                                        "'" + FinanceSetting.LCRef7.ToString() + "'";

                    dtBaseDatabaseSettingDetails.DefaultView.RowFilter = "NAME IN (" + values + ")";
                    DataTable dt = dtBaseDatabaseSettingDetails.DefaultView.ToTable();
                    dtBaseDatabaseSettingDetails.DefaultView.RowFilter = string.Empty;

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            FinanceSetting SettingName = (FinanceSetting)Enum.Parse(typeof(FinanceSetting), dr["NAME"].ToString());

                            switch (SettingName)
                            {
                                case FinanceSetting.LCRef1:
                                    {
                                        AppSetting.BaseLCRef1 = dr["VALUE"].ToString();
                                        break;
                                    }
                                case FinanceSetting.LCRef2:
                                    {
                                        AppSetting.BaseLCRef2 = dr["VALUE"].ToString();
                                        break;
                                    }
                                case FinanceSetting.LCRef3:
                                    {
                                        AppSetting.BaseLCRef3 = dr["VALUE"].ToString();
                                        break;
                                    }
                                case FinanceSetting.LCRef4:
                                    {
                                        AppSetting.BaseLCRef4 = dr["VALUE"].ToString();
                                        break;
                                    }
                                case FinanceSetting.LCRef5:
                                    {
                                        AppSetting.BaseLCRef5 = dr["VALUE"].ToString();
                                        break;
                                    }
                                case FinanceSetting.LCRef6:
                                    {
                                        AppSetting.BaseLCRef6 = dr["VALUE"].ToString();
                                        break;
                                    }
                                case FinanceSetting.LCRef7:
                                    {
                                        AppSetting.BaseLCRef7 = dr["VALUE"].ToString();
                                        break;
                                    }
                            }
                        }

                    }
                }

                //Base database location base on admin user
                result = uisystemsetting.BaseAcmeerpFetchUISettingDetails(this.LoginUser.DefaultAdminUserId);
                if (result.Success && result.DataSource.TableView != null && result.DataSource.TableView.Count > 0)
                {
                    result.DataSource.TableView.RowFilter = "NAME = 'Location'";
                    if (result.DataSource.TableView.Count > 0)
                    {
                        this.AppSetting.BaseLicenseLocation = result.DataSource.TableView[0]["VALUE"].ToString();
                    }
                }
            }
        }

        /// <summary>
        /// On 26/03/2022, to check valid AcmeerpLC details
        /// 
        /// On 05/05/2022, As SDBINM Province wanted to enforce and track Receipt module from FY 01/04/2022 onwards
        /// 
        /// On 08/06/2022 Receipt Module Rights Changes Ref 1, Few instutions are using DHCP (Dynamic Host Configuration Protocol), every time new ip will be
        /// assigned to local community PC. so receipt module rights will be locked as ip is changed in next day even though it was approved in previous day
        /// so we plan to check only MAC address
        /// </summary>
        /// <returns></returns>
        public ResultArgs EnableReceiptModule()
        {
            ResultArgs result = new ResultArgs();
            //string file = Path.Combine(SettingProperty.ApplicationStartUpPath, SettingProperty.AcmeerpLCFile);
            string file = Path.Combine(Application.StartupPath, SettingProperty.AcmeerpLCFile);

            //MessageBox.Show("2" + Application.StartupPath + " " + file);

            string macaddress = this.GetMACAddress(true);
            string ipaddress = this.GetIPAddress();
            this.AppSetting.ModuleRightsDetails = string.Empty;

            try
            {
                string baseBOPartCode = string.Empty;
                if (!string.IsNullOrEmpty(this.AppSetting.BaseLicenseBranchCode))
                {
                    baseBOPartCode = this.AppSetting.BaseLicenseBranchCode;
                    baseBOPartCode = baseBOPartCode.Substring(baseBOPartCode.Length - 6, 6);
                }

                //On 05/05/2022, As SDBINM Province wanted to enforce and track Receipt module from FY 01/04/2022 onwards
                if (this.AppSetting.IS_SDB_INM && (UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false) >= UtilityMember.DateSet.ToDate(SettingProperty.Enforce_Receipt_Module_FY, false)) &&
                    (this.AppSetting.PartBranchOfficeCode.ToUpper() != "SDBSPC" && this.AppSetting.PartBranchOfficeCode.ToUpper() != "INMSIS" && this.AppSetting.PartBranchOfficeCode.ToUpper() != "SURA24" &&
                      baseBOPartCode.ToUpper() != "SDBSPC" && baseBOPartCode.ToUpper() != "INMSIS" && baseBOPartCode.ToUpper() != "SURA24")) //&& !this.AppSetting.IsSplitPreviousYearAcmeerpDB
                {
                    //On 31/02/2022, If multi db enable license key -------------------------
                    AssignBaseDatabaseReferenceDetails();
                    //-----------------------------------------------------------------------

                    AcMELog.WriteLog("Start to check rights to use Receipt Module");
                    if (File.Exists(file))
                    {
                        //Assembly acperp1 = Assembly.LoadFrom(SettingProperty.AcmeerpLCFile);
                        Assembly acperp = Assembly.Load(File.ReadAllBytes(file)); //SettingProperty.AcmeerpLCFile

                        //AppDomain dom = AppDomain.CreateDomain("Acme.erpLC");
                        //AssemblyName assemblyName = new AssemblyName();
                        //assemblyName.CodeBase = "D:/ACP SOURCE/Build/AcmeerpRefLC.acp";
                        //Assembly acperp = dom.Load(assemblyName);
                        ////Do something with the loaded 'assembly'
                        ////AppDomain.Unload(dom);

                        if (acperp != null)
                        {
                            object acperpproperties = acperp.CreateInstance("Acme.erpLC.AcmeerpLC");

                            if (acperpproperties != null)
                            {
                                Type type = acperpproperties.GetType();
                                string ref1 = (type.GetField("ref1") == null ? String.Empty : type.GetField("ref1").GetValue(acperpproperties).ToString());
                                string ref2 = (type.GetField("ref2") == null ? String.Empty : type.GetField("ref2").GetValue(acperpproperties).ToString());
                                string ref3 = (type.GetField("ref3") == null ? String.Empty : type.GetField("ref3").GetValue(acperpproperties).ToString());
                                string ref4 = (type.GetField("ref4") == null ? String.Empty : type.GetField("ref4").GetValue(acperpproperties).ToString());
                                string ref5 = (type.GetField("ref5") == null ? String.Empty : type.GetField("ref5").GetValue(acperpproperties).ToString());
                                string ref6 = (type.GetField("ref6") == null ? String.Empty : type.GetField("ref6").GetValue(acperpproperties).ToString());
                                string ref7 = (type.GetField("ref7") == null ? String.Empty : type.GetField("ref7").GetValue(acperpproperties).ToString());
                                string approvedmacaddress = CommonMethod.Decrept(CommonMethod.Decrept(ref7));

                                //On 08/06/2022, ipaddress == CommonMethod.Decrept(CommonMethod.Decrept(ref6)) && 
                                AcMELog.WriteLog("Start:Check Level1");
                                AcMELog.WriteLog("System MACAddress(s) :: " + macaddress + "/Approved MACAddress ::" + approvedmacaddress);

                                //On 08/06/2022, ipaddress + Delimiter.Mew + 
                                this.AppSetting.ModuleRightsDetails = "1" + Delimiter.PipeLine + this.AppSetting.BaseLicensekeyNumber + Delimiter.Mew +
                                                                      this.AppSetting.BaseLicenseHeadOfficeCode + Delimiter.Mew +
                                                                      this.AppSetting.BaseLicenseBranchCode + Delimiter.Mew + this.AppSetting.BaseLicenseLocation + Delimiter.Mew +
                                                                      "MAC:" + macaddress + "/" + CommonMethod.Decrept(CommonMethod.Decrept(ref7));

                                //On 21/06/2022, Since few systems have more than one MAC Address, If when we approve first MAC Addres, the next day 
                                //System takes second MAC Address. so it will forced to lock receipt module.
                                //so we changed logic, approved MAC should be matched with any one of the local MAC Address and consider that is MAC Address
                                //take matached MAC address is approved and change it ----------------------------------------------------------------------
                                if (macaddress.Split('ê').Contains(approvedmacaddress))
                                {
                                    if (macaddress != approvedmacaddress)
                                    {
                                        macaddress = approvedmacaddress;
                                        AcMELog.WriteLog("Changed MAC Address with approved MAC");
                                    }
                                }
                                else
                                {
                                    //On 25/07/2022, for few systems (Basin Bridge) use Dongal, it generates new MAC address for every time,
                                    //so we fixed referebce DLL (already approved) MAC Address
                                    macaddress = approvedmacaddress;
                                    AcMELog.WriteLog("Changed MAC Address with approved MAC (For every time new MAC)");
                                }
                                //--------------------------------------------------------------------------------------------------------------------------

                                result.Success = (this.AppSetting.BaseLicensekeyNumber == CommonMethod.Decrept(CommonMethod.Decrept(ref2)) &&
                                                   this.AppSetting.BaseLicenseHeadOfficeCode == CommonMethod.Decrept(CommonMethod.Decrept(ref3)) &&
                                                   this.AppSetting.BaseLicenseBranchCode == CommonMethod.Decrept(CommonMethod.Decrept(ref4)) &&
                                                   this.AppSetting.BaseLicenseLocation == CommonMethod.Decrept(CommonMethod.Decrept(ref5)) &&
                                                   macaddress == CommonMethod.Decrept(CommonMethod.Decrept(ref7)));
                                AcMELog.WriteLog("Leve1 Status :: " + result.Success.ToString());
                                AcMELog.WriteLog("End:Check Level1");


                                //On 08/06/2022, CommonMethod.Decrept(CommonMethod.Decrept(ref6)) + Delimiter.Mew +
                                this.AppSetting.ModuleRightsDetails += "<br>2" + Delimiter.PipeLine + CommonMethod.Decrept(CommonMethod.Decrept(ref1)) + Delimiter.Mew +
                                                                      CommonMethod.Decrept(CommonMethod.Decrept(ref2)) + Delimiter.Mew +
                                                                      CommonMethod.Decrept(CommonMethod.Decrept(ref3)) + Delimiter.Mew +
                                                                      CommonMethod.Decrept(CommonMethod.Decrept(ref4)) + Delimiter.Mew +
                                                                      CommonMethod.Decrept(CommonMethod.Decrept(ref5)) + Delimiter.Mew +
                                                                      CommonMethod.Decrept(CommonMethod.Decrept(ref7));
                                if (result.Success)
                                {
                                    result = CheckLCdetails2(ref1, ref2, ref3, ref4, ref5, ref6, ref7);
                                    if (result.Success)
                                    {
                                        result = CheckLCdetails3(ref1, ref2, ref3, ref4, ref5, ref6, ref7);
                                        if (result.Success)
                                        {
                                            if (this.AppSetting.BaseLicenseBranchStatus == LCBranchModuleStatus.Approved)
                                            {
                                                result.ReturnValue = LCBranchModuleStatus.Approved;
                                                AcMELog.WriteLog("Sucessfully received rights to use Receipts Module");
                                            }
                                            else
                                            {
                                                result.ReturnValue = this.AppSetting.BaseLicenseBranchStatus;
                                                AcMELog.WriteLog("Receipt module rights, mismatching with DB and live status");
                                                AcMELog.WriteLog("BASE DB :: " + this.AppSetting.BaseLicenseBranchStatus.ToString());
                                            }
                                        }
                                        else
                                        {
                                            AcMELog.WriteLog("Problem in checking rights level3 Mismatching");
                                            AcMELog.WriteLog("Level 3 :: " + this.AppSetting.ModuleRightsDetails);
                                        }
                                    }
                                    else
                                    {
                                        AcMELog.WriteLog("Problem in checking rights level2 Mismatching");
                                        AcMELog.WriteLog("Level 2 :: " + this.AppSetting.ModuleRightsDetails);
                                    }
                                }
                                else
                                {
                                    AcMELog.WriteLog("Problem in checking rights level1 Mismatching");
                                    AcMELog.WriteLog("Level 1 :: " + this.AppSetting.ModuleRightsDetails);
                                }

                                result.Success = true;
                                if (!result.Success)
                                {
                                    result.ReturnValue = LCBranchModuleStatus.Disabled;
                                    result.Message = "This License/System does not have permission to use Receipts Module. Make Request to Acme.erp portal.";
                                    AcMELog.WriteLog(result.Message);
                                    AcMELog.WriteLog("Final Mismatching :: " + this.AppSetting.ModuleRightsDetails);
                                }
                            }
                            else
                            {
                                AcMELog.WriteLog(".acp file properties are empty");
                            }
                        }
                        else
                        {
                            AcMELog.WriteLog(".acp file content is empty");
                        }

                        //AppDomain.Unload(dom);
                        acperp = null;
                    }
                    else
                    {
                        AcMELog.WriteLog("Receipts Module rights not found");
                        this.AppSetting.ModuleRightsDetails = "Receipts Module rights not found";
                    }
                    AcMELog.WriteLog("End to check rights to use Receipt Module");
                }
                else
                {
                    result.ReturnValue = LCBranchModuleStatus.Approved;
                    result.Success = true; //For other Congregations
                }
            }
            catch (Exception err)
            {
                AcMELog.WriteLog("Problem in checking rights to use Receipt Module " + err.Message);
                result.Message = err.Message;
            }
            finally
            {
                //By Default if not sdbinm, always approved
                if (!this.AppSetting.IS_SDB_INM || (UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false) < UtilityMember.DateSet.ToDate(SettingProperty.Enforce_Receipt_Module_FY, false)))
                {
                    result.ReturnValue = LCBranchModuleStatus.Approved;
                    result.Success = true; //For other Congregations
                }

                this.AppSetting.FINAL_RECEIPT_MODULE_STATUS = LCBranchModuleStatus.Disabled;
                if (result.Success && result.ReturnValue != null)
                {
                    if (UtilityMember.NumberSet.ToInteger(result.ReturnValue.ToString()) == (Int32)LCBranchModuleStatus.Disabled ||
                        UtilityMember.NumberSet.ToInteger(result.ReturnValue.ToString()) == (Int32)LCBranchModuleStatus.Requested ||
                        UtilityMember.NumberSet.ToInteger(result.ReturnValue.ToString()) == (Int32)LCBranchModuleStatus.Approved)
                    {
                        this.AppSetting.FINAL_RECEIPT_MODULE_STATUS = (LCBranchModuleStatus)result.ReturnValue;
                    }
                }

                if (!result.Success && File.Exists(file))
                {
                    //File.Delete(file); //On 17/06/2022, To avoid removing license file even though mismatching to trace what was available
                }


                //For Temp purpose enabled Receipt module always for SDBINM Training Program-------------------------
                DateTime dAllowDate = UtilityMember.DateSet.ToDate("14/04/2025", false);
                DateTime dToday = UtilityMember.DateSet.ToDate(DateTime.Today.ToShortDateString(), false);
                if ((dToday - dAllowDate).TotalDays <= 6)
                {
                    result.ReturnValue = LCBranchModuleStatus.Approved;
                    result.Success = true; //For other Congregations
                    this.AppSetting.FINAL_RECEIPT_MODULE_STATUS = LCBranchModuleStatus.Approved;
                }
                this.AppSetting.FINAL_RECEIPT_MODULE_STATUS = LCBranchModuleStatus.Approved;
                //---------------------------------------------------------------------------------------------------

            }

            return result;
        }


        public ResultArgs EnforceLockMastersInVoucherEntry(Id SourceMasterType, DataTable dtEntyMaster, DateTime VoucherDate)
        {
            ResultArgs result = new ResultArgs();
            DataTable dtRtn = dtEntyMaster;
            try
            {
                using (AuditLockTransSystem auditlocksystem = new AuditLockTransSystem())
                {
                    result = auditlocksystem.EnforceLockMastersInVoucherEntry(SourceMasterType, dtEntyMaster, VoucherDate);
                }
            }
            catch (Exception err)
            {
                result.Message = err.Message;
            }
            finally
            {
                result.DataSource.Data = dtRtn;
            }
            return result;
        }

        private ResultArgs EnforceLockMastersInVoucherEntry(Id SourceMasterType, Int32 SourceMasterId, DataTable dtEntyMaster, DateTime VoucherDate)
        {
            ResultArgs result = new ResultArgs();
            DataTable dtRtn = dtEntyMaster;
            try
            {
                using (AuditLockTransSystem auditlocksystem = new AuditLockTransSystem())
                {
                    result = auditlocksystem.EnforceLockMastersInVoucherEntry(SourceMasterType, SourceMasterId, dtEntyMaster, VoucherDate);
                }
            }
            catch (Exception err)
            {
                result.Message = err.Message;
            }
            finally
            {
                result.DataSource.Data = dtRtn;
            }
            return result;
        }

        /// <summary>
        /// *************************************** For TEMP *****************************************************
        /// On 17/09/2020, Update Default Sign4 and Sign5 (Budget Approval Image)
        /// </summary>
        /// <returns></returns>
        public void UpdateDefaultCMFNEReportApprovedSign()
        {
            ResultArgs resultArgs = new ResultArgs();
            if (AppSetting.IS_CMF_CONGREGATION && AppSetting.HeadofficeCode.ToUpper() == "CMFNED")
            {
                using (UISetting uisetting = new UISetting())
                {
                    resultArgs = uisetting.FetchReportSignDetails();
                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        DataTable dtSign = resultArgs.DataSource.Table;
                        //ApprovedSign4RoleName = "Fr. Pius Thuruthiyil cmf";
                        //ApprovedSign5RoleName = "Fr. John Arackaparampil cmf";
                        ApprovedSign4RoleName = UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false).Year < 2024 ? "Fr. Pius Thuruthiyil cmf" : "Fr. Joseph Mappilaparampil";
                        ApprovedSign5RoleName = UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false).Year < 2024 ? "Fr. John Arackaparampil cmf" : "Fr. Walter Naveen";

                        ApprovedSign4Role = UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false).Year < 2023 ? "Major Superior" : "Provincial Superior";
                        ApprovedSign5Role = UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false).Year < 2023 ? "Delegation Econome" : "Provincial Econome";

                        byteApprovedSign4Image = null;
                        byteApprovedSign5Image = null;

                        //Size imagesize = new System.Drawing.Size(50, 20);
                        //Check Sign 4 and update
                        //On 18/12/2023, Updtae always, update role name 
                        dtSign.DefaultView.RowFilter = uisetting.AppSchema.ReportSign.SIGN_ORDERColumn.ColumnName + "= 4 AND " +
                                    uisetting.AppSchema.ReportSign.ROLEColumn.ColumnName + "='" + ApprovedSign4Role + "'";

                        Bitmap signimage = UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false).Year < 2024 ? ACPP.Properties.Resources.CMFNE_Sign4 :
                                    ACPP.Properties.Resources.CMFNE_Sign4_2024;
                        byteApprovedSign4Image = ImageProcessing.ImageToByteArray(signimage);
                        if (dtSign.DefaultView.Count == 0)
                        {
                            //Bitmap signimage = UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false).Year < 2024 ? ACPP.Properties.Resources.CMFNE_Sign4 :
                            //        ACPP.Properties.Resources.CMFNE_Sign4_2024;
                            //byteApprovedSign4Image = ImageProcessing.ImageToByteArray(signimage);
                            uisetting.ReportSignInsertUpdate(0, ApprovedSign4RoleName, ApprovedSign4Role, byteApprovedSign4Image, 0, string.Empty, 1, 0, 4);
                        }
                        //uisetting.UpdateSignDetailsForAllProjects(4, ApprovedSign4RoleName, ApprovedSign4Role, byteApprovedSign4Image, 0, string.Empty, 1, 0);
                        uisetting.ReportSignInsertUpdate(0, ApprovedSign4RoleName, ApprovedSign4Role, byteApprovedSign4Image, 0, string.Empty, 1, 0, 4);

                        //Check Sign 5 and update
                        //On 18/12/2023, Updtae always, update role name 
                        dtSign.DefaultView.RowFilter = uisetting.AppSchema.ReportSign.SIGN_ORDERColumn.ColumnName + "= 5 AND " +
                                    uisetting.AppSchema.ReportSign.ROLEColumn.ColumnName + "='" + ApprovedSign5Role + "'";
                        signimage = UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false).Year < 2024 ? ACPP.Properties.Resources.CMFNE_Sign5 :
                                                    ACPP.Properties.Resources.CMFNE_Sign5_2024;
                        byteApprovedSign5Image = ImageProcessing.ImageToByteArray(signimage);
                        if (dtSign.DefaultView.Count == 0)
                        {
                            //Bitmap signimage = UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false).Year < 2024 ? ACPP.Properties.Resources.CMFNE_Sign5 :
                            //                        ACPP.Properties.Resources.CMFNE_Sign5_2025;

                            //byteApprovedSign5Image = ImageProcessing.ImageToByteArray(signimage);
                            uisetting.ReportSignInsertUpdate(0, ApprovedSign5RoleName, ApprovedSign5Role, byteApprovedSign5Image, 0, string.Empty, 1, 0, 5);
                        }
                        //uisetting.UpdateSignDetailsForAllProjects(5, ApprovedSign5RoleName, ApprovedSign5Role, byteApprovedSign5Image, 0, string.Empty, 1, 0);
                        uisetting.ReportSignInsertUpdate(0, ApprovedSign5RoleName, ApprovedSign5Role, byteApprovedSign5Image, 0, string.Empty, 1, 0, 5);
                    }


                    //as on 29/07/2021, to fix budget ledger actual balance 
                    if (this.AppSetting.ShowBudgetLedgerActualBalance != "2")
                    {
                        using (UISettingSystem uisystemsetting = new UISettingSystem())
                        {
                            uisystemsetting.SaveUISettingDetails(FinanceSetting.ShowBudgetLedgerActualBalance, "2", this.LoginUser.LoginUserId);
                        }
                    }
                }

                ApplySettingAgain();
            }
        }

        /// <summary>
        /// *************************************** For TEMP *****************************************************
        /// On 28/02/2024, Update Default Sign4 and Sign5 (Budget Approval Image) For Montfort Pune
        /// </summary>
        /// <returns></returns>
        public void UpdateDefaultMontfortESPReportApprovedSign()
        {
            ResultArgs resultArgs = new ResultArgs();
            if (AppSetting.IS_BSG_CONGREGATION && AppSetting.HeadofficeCode.ToUpper() == "BSGESP")
            {
                using (UISetting uisetting = new UISetting())
                {
                    resultArgs = uisetting.FetchReportSignDetails();
                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        DataTable dtSign = resultArgs.DataSource.Table;
                        ApprovedSign4RoleName = "Br. Jaico Gervais";
                        ApprovedSign4Role = "Provincial Superior";
                        ApprovedSign5RoleName = "Br. Linto Emmanuel";
                        ApprovedSign5Role = "Provincial Bursar";
                        byteApprovedSign4Image = null;
                        byteApprovedSign5Image = null;

                        dtSign.DefaultView.RowFilter = uisetting.AppSchema.ReportSign.SIGN_ORDERColumn.ColumnName + "= 4 AND " +
                                    uisetting.AppSchema.ReportSign.ROLEColumn.ColumnName + "='" + ApprovedSign4Role + "'";
                        if (dtSign.DefaultView.Count == 0)
                        {
                            //Bitmap signimage = ACPP.Properties.Resources.CMFNE_Sign4;
                            //signimage = new Bitmap(signimage, 175, 20);
                            uisetting.ReportSignInsertUpdate(0, ApprovedSign4RoleName, ApprovedSign4Role, byteApprovedSign4Image, 0, string.Empty, 1, 0, 4);
                        }
                        uisetting.UpdateSignDetailsForAllProjects(4, ApprovedSign4RoleName, ApprovedSign4Role, byteApprovedSign4Image, 0, string.Empty, 1, 0);

                        //Check Sign 5 and update
                        dtSign.DefaultView.RowFilter = uisetting.AppSchema.ReportSign.SIGN_ORDERColumn.ColumnName + "= 5 AND " +
                                    uisetting.AppSchema.ReportSign.ROLEColumn.ColumnName + "='" + ApprovedSign5Role + "'";
                        if (dtSign.DefaultView.Count == 0)
                        {
                            //Bitmap signimage = ACPP.Properties.Resources.CMFNE_Sign5;
                            //signimage = new Bitmap(signimage, 175, 20);
                            uisetting.ReportSignInsertUpdate(0, ApprovedSign5RoleName, ApprovedSign5Role, byteApprovedSign5Image, 0, string.Empty, 1, 0, 5);
                        }
                        uisetting.UpdateSignDetailsForAllProjects(5, ApprovedSign5RoleName, ApprovedSign5Role, byteApprovedSign5Image, 0, string.Empty, 1, 0);
                    }
                }

                ApplySettingAgain();
            }
        }

        public void ApplySettingAgain()
        {
            ISetting isetting;
            isetting = new GlobalSetting();

            ResultArgs resultArg = isetting.FetchSettingDetails(this.UtilityMember.NumberSet.ToInteger(this.LoginUser.LoginUserId.ToString()));
            if (resultArg.Success && resultArg.DataSource.TableView != null && resultArg.DataSource.TableView.Count != 0)
            {
                this.UIAppSetting.UISettingInfo = resultArg.DataSource.TableView;
            }
        }

        /// <summary>
        /// On 26/11/2024 to get number of active voucehrs in current database
        /// </summary>
        /// <returns></returns>
        public Int32 GetNoActiveVouchers()
        {
            Int32 rtn = 0;
            using (VoucherTransactionSystem vsystem = new VoucherTransactionSystem())
            {
                rtn = vsystem.FetchActiveVouchersCount();
            }

            return rtn;
        }


        /// <summary>
        /// 23/01/2025 - Update Voucher Grace Details to enter vouchers
        /// </summary>
        public ResultArgs UpdateVoucherGraceDetailsFromAcmeerpPortal(bool isBGProcess = false)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                if (CheckAcmeerpWebserviceRunning())
                {
                    using (UISetting uisetting = new UISetting())
                    {
                        //Get the details from portal
                        if (this.AppSetting.IS_SDB_INM && !this.IsLicenseKeyMismatchedByLicenseKeyDBLocation() && !this.IsLicenseKeyMismatchedByHoProjects())
                        {
                            Int32 voucherenforcegracemode = 0;
                            Int32 voucheregracedays = 0;
                            string vouchergracetmpdatefrom = string.Empty;
                            string vouchergracetmpdateto = string.Empty;
                            string vouchergracevalidupto = string.Empty;

                            DataTable dtPortalLockVouchersGraceDays = new DataTable();
                            DataSyncService.DataSynchronizerClient objservice = new DataSyncService.DataSynchronizerClient();
                            dtPortalLockVouchersGraceDays = objservice.GetLockVoucherGraceDays(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode, this.AppSetting.Location);

                            if (dtPortalLockVouchersGraceDays != null && dtPortalLockVouchersGraceDays.Rows.Count > 0)
                            {
                                voucherenforcegracemode = UtilityMember.NumberSet.ToInteger(dtPortalLockVouchersGraceDays.Rows[0][uisetting.AppSchema.BranchVoucherGraceDays.ENFORCE_GRACE_DAYSColumn.ColumnName].ToString());
                                voucheregracedays = UtilityMember.NumberSet.ToInteger(dtPortalLockVouchersGraceDays.Rows[0][uisetting.AppSchema.BranchVoucherGraceDays.GRACE_DAYSColumn.ColumnName].ToString());
                                if (!string.IsNullOrEmpty(dtPortalLockVouchersGraceDays.Rows[0][uisetting.AppSchema.BranchVoucherGraceDays.GRACE_TMP_DATE_FROMColumn.ColumnName].ToString()))
                                {
                                    vouchergracetmpdatefrom = UtilityMember.DateSet.ToDate(dtPortalLockVouchersGraceDays.Rows[0][uisetting.AppSchema.BranchVoucherGraceDays.GRACE_TMP_DATE_FROMColumn.ColumnName].ToString(), false).ToShortDateString();
                                }
                                if (!string.IsNullOrEmpty(dtPortalLockVouchersGraceDays.Rows[0][uisetting.AppSchema.BranchVoucherGraceDays.GRACE_TMP_DATE_TOColumn.ColumnName].ToString()))
                                {
                                    vouchergracetmpdateto = UtilityMember.DateSet.ToDate(dtPortalLockVouchersGraceDays.Rows[0][uisetting.AppSchema.BranchVoucherGraceDays.GRACE_TMP_DATE_TOColumn.ColumnName].ToString(), false).ToShortDateString();
                                }
                                if (!string.IsNullOrEmpty(dtPortalLockVouchersGraceDays.Rows[0][uisetting.AppSchema.BranchVoucherGraceDays.GRACE_TMP_VALID_UPTOColumn.ColumnName].ToString()))
                                {
                                    vouchergracevalidupto = UtilityMember.DateSet.ToDate(dtPortalLockVouchersGraceDays.Rows[0][uisetting.AppSchema.BranchVoucherGraceDays.GRACE_TMP_VALID_UPTOColumn.ColumnName].ToString(), false).ToShortDateString();
                                }
                            }

                            //If any changes between local and Acme.erp portal, let update in local
                            string localTmpFrom = string.Empty;
                            string value = this.AppSetting.GetParticularSettingInfo(Setting.VoucherGraceTmpDateFrom.ToString());
                            result = CommonMethod.DecreptWithResultArg(value);
                            if (result.Success)
                            {
                                localTmpFrom = result.ReturnValue.ToString();
                            }

                            string localTmpTo = string.Empty;
                            value = this.AppSetting.GetParticularSettingInfo(Setting.VoucherGraceTmpDateTo.ToString());
                            result = CommonMethod.DecreptWithResultArg(value);
                            if (result.Success)
                            {
                                localTmpTo = result.ReturnValue.ToString();
                            }

                            string localTmpUpTo = string.Empty;
                            value = this.AppSetting.GetParticularSettingInfo(Setting.VoucherGraceTmpValidUpTo.ToString());
                            result = CommonMethod.DecreptWithResultArg(value);
                            if (result.Success)
                            {
                                localTmpUpTo = result.ReturnValue.ToString();
                            }

                            if ((this.AppSetting.VoucherEnforceGraceMode != voucherenforcegracemode) || (this.AppSetting.VoucherGraceDays != voucheregracedays) ||
                                (localTmpFrom != vouchergracetmpdatefrom) || (localTmpTo != vouchergracetmpdateto) || (localTmpUpTo != vouchergracevalidupto))
                            {
                                result = uisetting.UpdateVoucherGraceDetailsInGlobalSetting(voucherenforcegracemode, voucheregracedays,
                                vouchergracetmpdatefrom, vouchergracetmpdateto, vouchergracevalidupto);
                                if (result.Success && isBGProcess)
                                {
                                    ISetting isetting = new GlobalSetting();
                                    result = isetting.FetchSettingDetails(ADMIN_USER_DEFAULT_ID);
                                    if (result.Success && result.DataSource.TableView != null)
                                    {
                                        this.AppSetting.SettingInfo = result.DataSource.TableView;
                                        this.AppSetting.ValidateVoucherGraceTmpDetails();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                AcMELog.WriteLog("Not able to update grace days : " + err.Message);
                result.Message = err.Message;
            }
            return result;
        }

        /*
        public void ApplyProjectCurrencySetting(Int32 ProjectId)
        {
            ISetting isetting = new GlobalSetting();
            isetting.ApplyProjectCurrencySetting(ProjectId);
        }

        public void ApplyGlobalSetting()
        {
            ISetting isetting = new GlobalSetting();
            isetting.ApplySetting();
        }
        */
    }
}
