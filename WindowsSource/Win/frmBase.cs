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



namespace ACPP
{
    public class frmBase : DevExpress.XtraEditors.XtraForm
    {
        #region Declaration
        frmMain appMain = null;
        CommonMember utilityMember = null;
        private string PrintPageTitle = string.Empty;
        public const string ACME_ERP_VERSION_FILE = "AcMEERP_Vouchers";
        public event EventHandler ShowFilterClicked;
        public event EventHandler EnterClicked;
        bool CheckFilter = true;
        int Ledgerflag = 0;
        public bool isEditable = false;
        public bool isPrintable = false;
        public bool isAddable = false;
        public bool isDeleteable = false;
        public List<Enum> enumUserRights = new List<Enum>();
        public List<int> userRights = new List<int>();


        #endregion

        protected frmBase()
        {

        }

        protected virtual void OnUpdateHeld(object sender, EventArgs e)
        {

        }

        protected virtual void OnEditHeld(object sender, EventArgs e)
        {
        }

        protected void ShowMessageBox(string Msg)
        {
            XtraMessageBox.Show(Msg, this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected void ShowMessageBoxError(string Msg)
        {
            XtraMessageBox.Show(Msg, this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected void ShowMessageBoxWarning(string Msg)
        {
            XtraMessageBox.Show(Msg, this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        protected Bosco.Utility.ConfigSetting.UserProperty LoginUser
        {
            get { return new Bosco.Utility.ConfigSetting.UserProperty(); }
        }

        protected Bosco.Utility.ConfigSetting.SettingProperty AppSetting
        {
            get { return new Bosco.Utility.ConfigSetting.SettingProperty(); }
        }

        protected Bosco.Utility.ConfigSetting.UISettingProperty UIAppSetting
        {
            get { return new Bosco.Utility.ConfigSetting.UISettingProperty(); }
        }

        protected Bosco.Utility.ConfigSetting.TransProperty Transaction
        {
            get { return new Bosco.Utility.ConfigSetting.TransProperty(); }
        }

        protected CommonMember UtilityMember
        {
            get
            {
                if (utilityMember == null) { utilityMember = new CommonMember(); }
                return utilityMember;
            }
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

        //public virtual void OnUpdateHappened

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
                    //appMain.RecentVoucherDate = (!string.IsNullOrEmpty(this.AppSetting.RecentVoucherDate)) ? this.AppSetting.RecentVoucherDate : (dtbookbeginfrom > dtyearfrom) ? dtbookbeginfrom.ToString("d") : dtyearfrom.ToString("d");
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
        protected virtual void SetLanguage()
        {

        }
        protected virtual void SetTransacationPeriod()
        {
            if (appMain != null)
            {
                //if (!string.IsNullOrEmpty(this.AppSetting.YearFrom) && !string.IsNullOrEmpty(this.AppSetting.YearTo) || !string.IsNullOrEmpty(this.AppSetting.UserProject))
                //{
                //    appMain.SetTransaction = appMain.RecentProject + " (" + Convert.ToDateTime(this.AppSetting.YearFrom).ToString("d") + " To " + Convert.ToDateTime(this.AppSetting.YearTo).ToString("d") + ")";
                //}
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

        protected string GetMessage(string keyCode)
        {
            ResourceManager resourceManger = new ResourceManager("ACPP.Resources.Messages.Messages", Assembly.GetExecutingAssembly());
            string msg = "";
            try
            {
                msg = resourceManger.GetString(keyCode);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage("Resoure File is not available " + keyCode, false);
            }
            return msg;
        }

        protected void ShowSuccessMessage(string Msg)
        {
            try
            {
                CloseWaitDialog();
                if (!SettingProperty.IsWaitformShown)
                {
                    SplashScreenManager.ShowForm(typeof(frmSuccessWait));
                    SplashScreenManager.Default.SetWaitFormCaption(Msg.TrimEnd('.') + " .");
                    System.Threading.Thread.Sleep(1000);
                    CloseWaitDialog();
                }
            }
            catch (Exception ex)
            {
                AcMELog.WriteLog(ex.Message);
                CloseWaitDialog();
            }
        }

        public void ShowWaitDialog(string Message = "")
        {
            try
            {
                CloseWaitDialog();
                if (!SettingProperty.IsWaitformShown)
                {
                    SplashScreenManager.ShowForm(typeof(frmWait));
                    SplashScreenManager.Default.SetWaitFormDescription(string.IsNullOrEmpty(Message) ? "Processing..." : Message + "...");
                }
            }
            catch (Exception ex)
            {
                AcMELog.WriteLog(ex.Message);
                CloseWaitDialog();
            }
        }

        protected void CloseWaitDialog()
        {
            try
            {
                if (SettingProperty.IsWaitformShown)
                {
                    SplashScreenManager.CloseForm();
                }
            }
            catch (Exception)
            {
                // Have to find out
            }
        }

        protected void ShowPopUp(string Title, string Content)
        {
            frmPopup popup = new frmPopup(PopupSkins.SmileySkin);
            popup.CloseClickable = true;
            popup.ShowPopup(Title, Content, 500, 5000, 500);
        }

        protected void PrintGridViewDetails(DevExpress.XtraGrid.GridControl GridView, string Title, 
            PrintType printType, GridView gvControl, bool isLandscape = false)
        {

            Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry();
            SettingProperty.ReportModuleId = (int)ReportModule.Finance;
            report.ShowStandardReport(gvControl, Title);

            //DevExpress.XtraReports.UI.XtraReport rptReceipt = report.GetReport();
            //ReportPrintTool printTool = new ReportPrintTool(rptReceipt);
            //printTool.ShowPreviewDialog();
            
            //if (printType == PrintType.DT)
            //{
            //    DataTable dtGridView = GridView.DataSource as DataTable;
            //    if (GridView.DataSource != null && dtGridView != null && dtGridView.Rows.Count > 0)
            //    {
            //        PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
            //        link.Component = GridView;
            //        PrintPageTitle = Title;
            //        link.Landscape = isLandscape;
            //        link.CreateMarginalHeaderArea += new CreateAreaEventHandler(link_CreateMarginalHeaderArea);
            //        link.CreateDocument();
            //        link.ShowPreviewDialog();
            //    }
            //    else
            //    {
            //        ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_PRINT_MESSAGE));
            //    }
            //}
            //else
            //{
            //    DataSet dsDataView = GridView.DataSource as DataSet;

            //    if (GridView.DataSource != null && dsDataView != null && dsDataView.Tables.Count > 0)
            //    {
            //        PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
            //        link.Component = GridView;
            //        PrintPageTitle = Title;
            //        link.Landscape = isLandscape;
            //        link.CreateMarginalHeaderArea += new CreateAreaEventHandler(link_CreateMarginalHeaderArea);
            //        link.CreateDocument();
            //        link.ShowPreviewDialog();
            //    }
            //    else
            //    {
            //        ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_PRINT_MESSAGE));
            //    }
            //}
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

        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    if (keyData == (Keys.Control | Keys.A))
        //    {
        //        MessageBox.Show("What the Ctrl+A?");
        //        return true;
        //    }
        //    return base.ProcessCmdKey(ref msg, keyData);
        //}

        protected DialogResult ShowConfirmationMessage(string Message, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageBoxIcon)
        {
            DialogResult drResult = DialogResult.None;
            drResult = XtraMessageBox.Show(Message, this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), messageBoxButtons, messageBoxIcon);
            return drResult;
        }

        protected virtual void ShowSuccessMessageOnToolTip(string Message)
        {
            ToolTipController toolTipControllerMessage = new ToolTipController();
            ToolTipControllerShowEventArgs args = toolTipControllerMessage.CreateShowArgs();
            args.ToolTip = Message;
            args.ToolTipType = ToolTipType.SuperTip;
            args.ToolTipLocation = ToolTipLocation.TopCenter;
            toolTipControllerMessage.ShowHint(args, this);
            toolTipControllerMessage.AutoPopDelay = 5000;
        }

        protected virtual void SetFocusRowFilter(DevExpress.XtraGrid.Views.Grid.GridView gridview, DevExpress.XtraGrid.Columns.GridColumn colGridColumn)
        {
            gridview.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
            gridview.FocusedColumn = colGridColumn;
            gridview.OptionsFind.AllowFindPanel = false;
            gridview.ShowEditor();
        }

        /// <summary>
        /// Method to predict the next code
        /// </summary>
        /// <param name="parCode"></param>
        /// <returns></returns>
        public static string CodePredictor(string parCode, DataTable dtCode)
        {
            parCode = parCode != string.Empty ? parCode : "000";
            string finalCode = "";
            string tempstr = "";
            string Code = string.Empty;
            try
            {
                string prefixCode = Regex.Match(parCode, @"^[A-Z|a-z]+").Value;
                string digitCode = Regex.Match(parCode, @"\d+").Value;
                string suffixCode = Regex.Match(parCode, @"[A-Z|a-z]+$").Value;
                int tempCode = Convert.ToInt32(digitCode) + 1;
                if (prefixCode.Length != parCode.Length)
                {
                    if (digitCode.Length != parCode.Length)
                    {
                        // To check no of zero available in the code                     
                        if (ZeroCount(digitCode) != 0)
                            finalCode = prefixCode + tempCode.ToString(tempstr = AddZero(digitCode.Length)) + suffixCode;
                        else
                            finalCode = prefixCode + tempCode.ToString() + suffixCode;
                    }
                    else
                    {
                        // To check no of zero available in the code                     
                        if (ZeroCount(digitCode) != 0)
                            finalCode = prefixCode + tempCode.ToString(tempstr = AddZero(digitCode.Length)) + suffixCode;
                        else
                            finalCode = prefixCode + tempCode.ToString() + suffixCode;
                    }
                }
                // To check the generated code is present already or not
                for (int i = 0; i < dtCode.Rows.Count && dtCode != null && dtCode.Rows.Count > 0; i++)
                {
                    if (finalCode.Equals(dtCode.Rows[i][0].ToString())) { finalCode = CodePredictor(finalCode, dtCode); i = 0; }
                }
            }
            catch (Exception ex)
            {
                string exception = ex.ToString();
            }

            return finalCode;
        }

        /// <summary>
        /// To add zero to string 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static string AddZero(int length)
        {
            string tempstr = "";
            for (int i = 1; i <= length; i++)
                tempstr = tempstr + "0";
            return tempstr;
        }
        /// <summary>
        /// To count zeros present in the digitCode
        /// </summary>
        /// <param name="digit"></param>
        /// <returns></returns>
        private static int ZeroCount(string digit)
        {
            Regex reg = new Regex(@"0+");
            Match mat = reg.Match(digit);
            string tempstr = mat.Value;
            return tempstr.Length;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            EventArgs e = new EventArgs();
            if (CheckFilter)
            {
                if (KeyData == (Keys.Control | Keys.F))
                {
                    if (ShowFilterClicked != null)
                    {
                        ShowFilterClicked(this, e);
                    }
                    CheckFilter = false;
                }
            }
            else
            {
                CheckFilter = true;
            }
            if (KeyData == Keys.Enter)
            {
                if (EnterClicked != null)
                {
                    EnterClicked(this, e);
                }
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        //public void ShowMessageWaitDialog(string Msg)
        //{
        //    Iswaitformshown = true;
        //    SplashScreenManager.ShowForm(typeof(frmWait));
        //    Msg = Msg + "...";
        //    SplashScreenManager.Default.SetWaitFormDescription(Msg);
        //}

        public void LockMasters(ACPP.Modules.UIControls.ucToolBar ucToolBar)
        {
            if (this.AppSetting.LockMasters == (int)YesNo.Yes)
            {
                ucToolBar.VisibleAddButton = ucToolBar.VisibleEditButton = ucToolBar.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        public void ApplyUserRights(ACPP.Modules.UIControls.ucToolBar ucToolBar, List<Enum> userRights, int ParentId)
        {
            DataTable dtUserRights = new DataTable();
            Forms activityType;
            string Activities = string.Empty;
            try
            {
                if (this.UtilityMember.NumberSet.ToInteger(LoginUser.LoginUserId) != (int)UserRights.Admin)
                {
                    dtUserRights = CommonMethod.ApplyUserRightsForForms(ParentId);
                    if (dtUserRights != null && dtUserRights.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dtUserRights.Rows)
                        {
                            for (int i = 0; i < userRights.Count; i++)
                            {
                                if (userRights[i].ToString() == dr["ENUMTYPE"].ToString())
                                {
                                    activityType = (Forms)UtilityMember.EnumSet.GetEnumItemType(typeof(Forms), userRights[i].ToString());
                                    Activities = UtilityMember.EnumSet.GetDescriptionFromEnumValue(activityType);
                                    if (Activities == OperationsList.ADD.ToString())
                                    {
                                        ucToolBar.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
                                        isAddable = true;
                                    }
                                    else if (Activities == OperationsList.EDIT.ToString())
                                    {
                                        ucToolBar.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
                                        isEditable = true;
                                    }
                                    else if (Activities == OperationsList.DELETE.ToString())
                                    {
                                        ucToolBar.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
                                        isDeleteable = true;
                                    }
                                    else if (Activities == OperationsList.MOVE.ToString())
                                    {
                                        ucToolBar.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Always;
                                    }
                                    else if (Activities == OperationsList.PRINT.ToString())
                                    {
                                        ucToolBar.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
                                        isPrintable = true;
                                    }
                                    else if (Activities == OperationsList.INSERT.ToString())
                                    {
                                        ucToolBar.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Always;
                                        isPrintable = true;
                                    }
                                    else if (Activities == OperationsList.NAGATIVEBALANCE.ToString())
                                    {
                                        ucToolBar.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Always;
                                        isPrintable = true;
                                    }
                                    else if (Activities == OperationsList.IMPORT.ToString())
                                    {
                                        ucToolBar.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Always;
                                        isPrintable = true;
                                    }
                                    else if (Activities == OperationsList.CONVERT.ToString())
                                    {
                                        ucToolBar.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Always;
                                        isPrintable = true;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (this.enumUserRights.Count == 5)
                    {
                        ucToolBar.VisibleAddButton = ucToolBar.VisibleEditButton = ucToolBar.VisibleDeleteButton = ucToolBar.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        ucToolBar.VisibleAddButton = ucToolBar.VisibleEditButton = ucToolBar.VisibleDeleteButton = ucToolBar.VisiblePrintButton =
                         ucToolBar.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    isEditable = true;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // frmBase
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "frmBase";
            this.Load += new System.EventHandler(this.frmBase_Load);
            this.ResumeLayout(false);

        }

        private void frmBase_Load(object sender, EventArgs e)
        {

        }

        public bool CheckForInternetConnection()
        {
            AcMELog.WriteLog("Checking for the Internet Connection..");
            bool hasInternet = false;
            ResultArgs result = new ResultArgs();
            using (AcMEERPFTP ftpUpload = new AcMEERPFTP())
            {
                result = ftpUpload.CheckIfFileExistsOnServer();
                if (result.Success)
                {
                    hasInternet = true;
                    AcMELog.WriteLog("Connection established successfully.");
                }
                else
                {
                    AcMELog.WriteLog("Problem in connecting to Internet." + result.Message);
                }
            }
            return hasInternet;
        }

        public bool CheckForInternetConnectionhttp()
        {
            AcMELog.WriteLog("Checking for the Internet Connection..");
            bool hasInternet = false;
            ResultArgs result = new ResultArgs();
            using (AcMEERPFTP http = new AcMEERPFTP())
            {
                result = http.CheckIfFileExists();
                if (result.Success)
                {
                    hasInternet = true;
                    AcMELog.WriteLog("Connection established successfully.");
                }
                else
                {
                    AcMELog.WriteLog("Problem in connecting to Internet." + result.Message);
                }
            }
            return hasInternet;
        }
        /// <summary>
        /// by Alex for Currencr formate
        /// </summary>
        public string SetCurrencyFormat(string Caption)
        {
            SettingProperty settingProperty = new SettingProperty();
            string CurrencyFormat = string.Empty;
            if (!Caption.Contains(settingProperty.Currency))
                CurrencyFormat = String.Format("{0} ({1})", Caption, settingProperty.Currency);
            else { CurrencyFormat = Caption; }
            return CurrencyFormat;
        }
    }
}
