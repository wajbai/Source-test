using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.Model;
using Bosco.Model.Setting;
using Bosco.Utility.ConfigSetting;
using System.IO;
using Bosco.DAO;
using Bosco.DAO.Schema;
using System.Resources;
using ACPP.Modules.Master;
using System.Globalization;
using Bosco.Model.UIModel.Master;
using System.Configuration;
using Bosco.DAO.Configuration;
using ACPP.Modules.Data_Utility;
using System.ServiceModel;
using ACPP.Modules.Dsync;
using AcMEDSync;
using Bosco.Model.Transaction;

namespace ACPP
{
    public partial class frmAcMELogin : frmFinanceBaseAdd
    {
        #region Variables
        ResultArgs resultArgs = null;
        private const string colVoucherDate = "VOUCHER_DATE";

        const string GST_ZERO_CLASS = "0.0";
        private string LicensePath = Path.Combine(Application.StartupPath.ToString(), "AcMEERPLicense.xml");
        private string DefaultDBLoggedinUserNamePath = Path.Combine(Application.StartupPath.ToString(), "LoggedInUser.txt");
        public string[] preference = new string[5];
        SimpleEncrypt.SimpleEncDec objSimpleEncrypt = new SimpleEncrypt.SimpleEncDec();
        DataView dtSetting = new DataView();
        private static string RecentDBName = string.Empty;
        private string ActiveConnstring = string.Empty;
        private string connection_string = ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString;
        public string MultiDBLicenseKeyName = string.Empty;
        public DataTable dtLicenseFile = new DataTable();
        #endregion

        #region Constructor
        public frmAcMELogin()
        {
            this.LoginUser.UserInfo = null;
            this.AppSetting.SettingInfo = null;
            this.UIAppSetting.UISettingInfo = null;
            InitializeComponent();

            if (CheckEncryptComponent())
            {

            }
        }
        #endregion

        #region Events
        private void frmAcMELogin_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("1 " + Application.StartupPath + " " + LicensePath);
            this.ShowWaitDialog();
            BackupAndRestore restore = new BackupAndRestore();
            restore.RestoreDefaultDatabase();
            this.CloseWaitDialog();
            if (!CheckIsMultiDB(LicensePath))
            {
                ApplyLicensePeriod();
                this.Height = this.Height - 20;
                lciBranch.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                LoadDatabasesfromXml();
                lciBranch.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }

            this.LoadCultureInfo(glkpUILanguage);
            //lciLanguage.Visibility = (SettingProperty.EnableLocalization) ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always :
            //    DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            lciLanguage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            //if (this.AppSetting.AccesstoMultiDB == (int)YesNo.Yes)
            //{

            //}
            //else
            //{

            //}

            SettingProperty.ActiveDatabaseName = glkpDatabase.EditValue.ToString();
            LoadLastLoggedinUser(" ", true);
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                txtUserName.Select();
            }
            else if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                txtPassword.Select();
            }
            else
            {
                btnLogin.Select();
            }

            LoadCultureNames();
        }

        private bool IsValidInput()
        {
            bool isValid = true;
            if (glkpDatabase.Visible && string.IsNullOrEmpty(glkpDatabase.Text))
            {
                isValid = false;
                this.SetBorderColorForGridLookUpEdit(glkpDatabase);
                glkpDatabase.Focus();
            }
            else if (string.IsNullOrEmpty(txtUserName.Text))
            {
                isValid = false;
                this.SetBorderColor(txtUserName);
                txtUserName.Focus();
            }
            else if (string.IsNullOrEmpty(txtPassword.Text))
            {
                isValid = false;
                this.SetBorderColor(txtPassword);
                txtPassword.Focus();
            }
            return isValid;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (IsValidInput())
            {
                if (File.Exists(LicensePath))
                {
                    //lciActivateLciense.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    if (this.AppSetting.AccesstoMultiDB == (int)YesNo.Yes)
                    {
                        this.ShowWaitDialog();
                        SetConnectionString(ActiveConnstring);
                        CommonMethod.MultiDataBaseName = glkpDatabase.EditValue.ToString();
                        this.CloseWaitDialog();
                    }

                    using (UserSystem userSystem = new UserSystem())
                    {
                        this.Cursor = Cursors.WaitCursor;
                        ResultArgs result = userSystem.AuthenticateUser(txtUserName.Text, objSimpleEncrypt.EncryptString(txtPassword.Text));

                        if (result.Success)
                        {
                            //For 10/09/2024, To have proper size
                            if (lciBranch.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always) this.Height = 210;
                            Application.DoEvents();

                            SettingProperty.LoginPassword = txtPassword.Text;
                            SettingProperty.LoginUserName = txtUserName.Text;
                            CheckShowingCCOpeningBalanceSetting(); //On 27/03/2020, to check ShowingCCOpeningBalanceSetting
                            ApplySetting();
                            //GetCurrentDate();
                            ApplyUISetting();
                            ApplyAccPeriod();
                            AssignDefaultLedgers();
                            ApplyRecentPrjectDetails();
                            ApplyLicensePeriod();
                            SetModuleProperties();
                            FetchUserRights();
                            FetchAcMeERPReportLogo();
                            SettingProperty.ActiveDatabaseName = RecentDBName;
                            SettingProperty.ActiveDatabaseAliasName = glkpDatabase.Text;
                            SettingProperty.ApplicationStartUpPath = Application.StartupPath.ToString();
                            LoadLastLoggedinUser(txtUserName.Text.Trim(), false);

                            // Instead of Getting from Language choose from Login Screen 17/10/2023
                            //  this.AppSetting.LanguageId = glkpUILanguage.EditValue.ToString();

                            BackupAndRestore restore = new BackupAndRestore();
                            resultArgs = restore.UpdateDBChanges(ActiveConnstring);
                            if (resultArgs.Success)
                            {
                                ApplySetting();
                                AcMELog.WriteLog("DB Changes updated Successfully.");
                            }
                            HostAcMEService();
                            AcMELog.ClearLogbySize();       //Clear general Log, when it reaches to defined size
                            AcMEDataSynLog.ClearLogbySize(); //Clear general Log, when it reaches to defined size

                            //On 15/12/2020, To take multiple db backup file for saftey puropose
                            this.TakeBackup_MultipleDBXML();
                            //-------------------------------------------------------------------------------------------------------------------------------------

                            //-------------------------------------------------------------------------------------------------------------------------------------
                            //On 26/03/2022, For SDB INM, They have separate rights to use Receipts Module,
                            //Rights will be defined in Acme.erp poral.
                            resultArgs = this.EnableReceiptModule();
                            //-------------------------------------------------------------------------------------------------------------------------------------

                            //On 16/03/2023, to set licensekey mismatching-----------------------------------------------------------------------------------------
                            this.AppSetting.AcmeerpPortalServerDateTime = this.GetAcmeerpPortalDateTime();
                            this.AppSetting.IsLicenseKeyMismatchedByLicenseKeyDBLocation = this.IsLicenseKeyMismatchedByLicenseKeyDBLocation();
                            this.AppSetting.IsLicenseKeyMismatchedByHoProjects = this.IsLicenseKeyMismatchedByHoProjects();
                            //-------------------------------------------------------------------------------------------------------------------------------------
                            this.AppSetting.ValidateVoucherGraceTmpDetails();
                            
                            //On 06/02/2024 -----------------------------------------------------------------------------------------------------------------------
                            Int32 noofallowedmultiDBs = this.AppSetting.NoOfAllowedMultiDBs;
                            bool issplitpreviousyearDB = this.AppSetting.IsSplitPreviousYearAcmeerpDB;

                            //For SDBINM, enforce to manage split previous year acmeerp data and enforce allowed multi dbs
                            // Enforce Disable Portal Interface 
                            if (issplitpreviousyearDB)
                            {
                                this.AppSetting.EnablePortal = 0;
                            }
                            AcMELog.WriteLog("Number of allowed multiple DBs:" + noofallowedmultiDBs.ToString());
                            AcMELog.WriteLog("Is it split previous year DB:" + issplitpreviousyearDB.ToString());
                            //-------------------------------------------------------------------------------------------------------------------------------------

                            //On 06/05/2024, Update Multi DB xml file into database
                            UpdateMutiDBXMLConfiguration();

                            //On 14/05/2024, for Temp- to correct Mutual Fund FD Accoutns corrections 
                            using (FDAccountSystem fdsystem = new FDAccountSystem())
                            {
                                fdsystem.CorrectACKPMAFDMutualFund_Temp();
                            }

                            UtilityMember.FileSet.CreateDirectory(SettingProperty.ApplicationStartUpPath, SettingProperty.Folder_VoucherFiles);
                            UtilityMember.FileSet.CreateDirectory(SettingProperty.ApplicationStartUpPath, SettingProperty.Folder_UserManuals);

                            //On 14/08/2024, For other country of India, They Don't have Fixed Depoist, they have only investments
                            if (AppSetting.IsCountryOtherThanIndia)
                            {
                                using (LedgerGroupSystem ledgergroup = new LedgerGroupSystem())
                                {
                                    ledgergroup.ChangeFixedDepositToInvestments();
                                }
                            }

                            //On 10/09/2024, If License Key is not multi currency enabled, but cash/bank are currency enabled, let us enforce multi currency
                            if (AppSetting.AllowMultiCurrency==0)
                            {
                                using (LedgerSystem ledgersystem = new LedgerSystem())
                                {
                                    if (ledgersystem.IsCurrencyEnabledCashBankLedgerExists())
                                    {
                                        AppSetting.AllowMultiCurrency = 1;
                                    }
                                }
                            }

                            //On 13/12/2024 - lock maximum cas limit
                            if (AppSetting.AllowMultiCurrency == 1 || AppSetting.IsCountryOtherThanIndia)
                            {
                                FixMulticurrencyOtherThanIndiaSettings();
                            }

                            this.DialogResult = DialogResult.OK;

                            //For temp purpose mysore 08/01/2020
                            //UpdateBudgetGroupForMysore();
                        }
                        else
                        {
                            if (result.Message == "")
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.User.USER_INVALID));
                            }

                            txtUserName.Focus();
                        }

                        //On 08/07/2024, To lockOLD DBS
                        /*if (isOLDProjectDB())
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.AcmeLogin.LICENSE_KEY_NOT_AVAILABLE));
                            this.DialogResult = DialogResult.Cancel;
                        }
                        else
                        {
                            this.DialogResult = DialogResult.OK;
                        }*/
                        
                        this.Cursor = Cursors.Default;
                    }
                }
                else
                {
                    //this.ShowMessageBox("License Key is not available.");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.AcmeLogin.LICENSE_KEY_NOT_AVAILABLE));
                    lciActivateLciense.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
            }
        }

        

        /// <summary>
        /// To lock login process if current database contains old projects
        /// </summary>
        /// <returns></returns>
        private bool isOLDProjectDB()
        {
            bool rtn = false;
            string[] oldprojects = { "BEST PROJECT, SURABI", "Bosconet CSO Project - INN.18.252 / BMZ 4155", "BRAVE PROJECT - LOCAL",
                        "BRAVE PROJECT - SURABI", "BRIDGE Project", "BRIGHT Project","Dream Project", "Emergency Relief Aid Support - Michaung Cyclone",
                        "Emergency Relief to The Inter-State Displaced Persons (Manipur)", "Empowering Futures - Ennore", "Green Hands - 1", "Green Hands - 2",
                        "INM 20-010 Promotion of women empowerment and rights (POWER)", "INM 20-192 COVER Emergency Response COVID 19", "Integrated Village Development (IVD)",
                        "Leaders for Environmental Awareness and Protection", "MICHAEL DOSS FOUNDATION" , "My Body My Right Project" , "My Body, My Right - Holistic Menstrual Hygiene Management - INM 18-009 (SWISS)",
                        "My Body, My Right (JEW, AUSTRIA)", "Nedungadu Lab Project", "Night Study for Katpadi Beedi Workers Children", "Providing Care - DBAI Maintenance",
                        "Response Project", "School to School Project", "Solar Army Project", "Sponsorship & Maintenance - Gedilam", "SUFINS", "SURABI - DBACTION",
                        "SURABI - LRM", "SURABI (FC)", "SURABI (Local)" , "SURABI Projects", "Thurumbar Housing Project", "Veeralur School Benches Project", "Vehicle Project",
                        "Walking A New - CEI Project", "Women Development Project", "Youth in Action"};

            using (MappingSystem mappingsystem = new MappingSystem())
            {
                using (CommonMethod UtilityMethod = new CommonMethod())
                {
                    ResultArgs result = mappingsystem.FetchPJLookup();
                    if (result.Success && result.DataSource.Table != null)
                    {
                        DataTable dtProjects = result.DataSource.Table;
                        foreach (string project in oldprojects)
                        {
                            ResultArgs resultFind = UtilityMethod.CheckValueCotainsInDataTable(dtProjects, mappingsystem.AppSchema.Project.PROJECTColumn.ColumnName, project);
                            if (resultFind.DataSource.Data != null && resultFind.Success)
                            {
                                rtn = true;
                                break;
                            }
                        }
                    }
                }
            }
            return rtn;
        }

        /// <summary>
        /// Fix Acmeerp settings for Multi Currency and Other than India 
        /// </summary>
        private void FixMulticurrencyOtherThanIndiaSettings()
        {
            if (AppSetting.AllowMultiCurrency == 1 || AppSetting.IsCountryOtherThanIndia)
            {
                bool RefillSetting = false;
                using (UISetting uisetting = new UISetting())
                {
                    //1. Disable For Max cash Amount
                    if (AppSetting.MaxCashLedgerAmountInReceiptsPayments > 0)
                    {
                        resultArgs = uisetting.SaveSettingDetails(Bosco.Utility.FinanceSetting.MaxCashLedgerAmountInReceiptsPayments.ToString(), string.Empty,
                                uisetting.NumberSet.ToInteger(AppSetting.LoginUserId));
                        if (resultArgs.Success)
                        {
                            RefillSetting = true;
                        }
                    }

                    //2. Trans mode fix to CR/DR
                    if (AppSetting.TransMode != "1")
                    {
                        resultArgs = uisetting.SaveSettingDetails(Bosco.Utility.FinanceSetting.UITransMode.ToString(), "1",
                                uisetting.NumberSet.ToInteger(AppSetting.LoginUserId));
                        if (resultArgs.Success)
                        {
                            RefillSetting = true;
                        }
                    }

                    if (RefillSetting)
                    {
                        ApplySetting();
                        ApplyUISetting();
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void glkpUILanguage_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void glkpDatabase_EditValueChanged(object sender, EventArgs e)
        {
            if (this.AppSetting.AccesstoMultiDB == (int)YesNo.Yes)
            {
                ActiveConnstring = changeConnStringItem(connection_string, "database", (!string.IsNullOrEmpty(glkpDatabase.EditValue.ToString())) ? (glkpDatabase.EditValue.ToString()) : "acperp");
                GetMultiLicenseFilename(glkpDatabase.EditValue.ToString());
                SettingProperty.ActiveDatabaseName = glkpDatabase.EditValue.ToString();
                LicensePath = Path.Combine(Application.StartupPath.ToString(), MultiDBLicenseKeyName);
                SettingProperty.ActiveDatabaseLicenseKeypath = MultiDBLicenseKeyName;

                //On 24/05/2021, to retain remember logged user for multi branch --------
                SettingProperty.ActiveDatabaseAliasName = glkpDatabase.Text;
                LoadLastLoggedinUser("", true);
                //-----------------------------------------------------------------------
            }
        }

        private void lnkActivateLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ActivateLicenseKey();
        }

        #endregion

        #region Methods

        public Bitmap GetDefaultUserPhoto()
        {
            return global::ACPP.Properties.Resources.Default_Photo;
        }

        private void ApplySetting()
        {
            try
            {
                ISetting isetting = new GlobalSetting();
                ResultArgs resultArgs = isetting.FetchSettingDetails(1);

                // SettingProperty setting = new SettingProperty();
                // Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(setting.Language);

                // System.Globalization.NumberFormatInfo nformat = new CultureInfo(setting.Language, true).NumberFormat;

                if (resultArgs.Success && resultArgs.DataSource.TableView != null && resultArgs.DataSource.TableView.Count > 0)
                {
                    //string LanguageValue =// glkpUILanguage.EditValue.ToString(); //"fr-FR";
                    //string Caption = "UILanguage";
                    //dtSetting = resultArgs.DataSource.TableView;
                    //dtSetting.RowFilter = "Name= '" + Caption + "'";
                    //foreach (DataRowView dr in dtSetting)
                    //{
                    //    dr["Value"] = LanguageValue;
                    //}
                    //dtSetting.RowFilter = "";
                    // this.AppSetting.SettingInfo = dtSetting;

                    // Instead of Getting from Language choose from Login Screen 17/10/2023
                    this.AppSetting.SettingInfo = resultArgs.DataSource.TableView;
                    isetting.ApplySetting();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        /// <summary>
        /// 27/03/2020, to check ShowingCCOpeningBalanceSetting
        /// </summary>
        private void CheckShowingCCOpeningBalanceSetting()
        {
            using (UISetting uisetting = new UISetting())
            {
                ResultArgs resultArgs = uisetting.EnableShowCCOPBalanceInCCReports();
            }
        }

        //private void GetCurrentDate()
        //{
        //    using (SettingSystem settingsystem = new SettingSystem())
        //    {
        //        SettingProperty.returnCurrentdate = settingsystem.FetchCurrentDate();
        //    }
        //}

        private void ApplyUISetting()
        {
            try
            {
                SettingProperty setting = new SettingProperty();
                ISetting isetting = new UISetting();
                ResultArgs resultArgs = isetting.FetchSettingDetails(this.UtilityMember.NumberSet.ToInteger(this.LoginUser.LoginUserId.ToString()));

                if (resultArgs.Success && resultArgs.DataSource.TableView != null && resultArgs.DataSource.TableView.Count != 0 && resultArgs.DataSource.TableView.Count >= 22)
                {
                    //string LanguageValue = setting.UILanguage; // glkpUILanguage.EditValue.ToString();
                    ////string Caption = "UILanguage";
                    //string Caption = this.GetMessage(MessageCatalog.Master.AcmeLogin.UILANGUAGE_CAPTION);
                    //dtSetting = resultArgs.DataSource.TableView;
                    //dtSetting.RowFilter = "Name= '" + Caption + "'";
                    //foreach (DataRowView dr in dtSetting)
                    //{
                    //    dr["Value"] = LanguageValue;
                    //}
                    //dtSetting.RowFilter = "";

                    // Instead of Getting from Language choose from Login Screen 17/10/2023
                    this.UIAppSetting.UISettingInfo = resultArgs.DataSource.TableView;
                    isetting.ApplySetting();
                }
                else
                {
                    // to set the Rights if not available UI Common Settings..(Chinna)
                    this.UIAppSetting.UISettingInfo = this.AppSetting.SettingInfo;
                    ISetting iisetting;
                    frmSettings Setting = new frmSettings();
                    Setting.LoadCultureNames();
                    Setting.LoadTheme();
                    Setting.BindValues();
                    Setting.SaveUISetting(this.UtilityMember.NumberSet.ToInteger(this.LoginUser.LoginUserId.ToString()));
                    iisetting = new UISetting();
                    resultArgs = iisetting.SaveSetting(Setting.dtUISetting);
                    if (resultArgs.Success)
                    {
                        iisetting.ApplySetting();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        private void ApplyAccPeriod()
        {
            try
            {
                
                using (AccouingPeriodSystem accountingSystem = new AccouingPeriodSystem())
                {
                    accountingSystem.ValidateAndSetActiveFY();
                    ResultArgs resultArgs = accountingSystem.FetchActiveTransactionPeriod();
                    if (resultArgs.Success)
                    {
                        this.AppSetting.AccPeriodInfo = resultArgs.DataSource.Table.DefaultView;

                        //Assign previous year 
                        resultArgs = accountingSystem.FetchPreviousYearAC(this.AppSetting.YearFrom);
                        if (resultArgs.Success)
                        {
                            this.AppSetting.AccPeriodInfoPrevious = resultArgs.DataSource.Table.DefaultView;
                        }

                        //#On 30/04/2021, Assign All FYs
                        resultArgs = accountingSystem.FetchAccountingPeriodDetails();
                        if (resultArgs.Success && resultArgs.DataSource.Table != null)
                        {
                            this.AppSetting.AllAccountingPeriods = resultArgs.DataSource.Table;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        private void AssignDefaultLedgers()
        {
            this.AppSetting.CGSTLedgerId = GetDefaultLedgers(SettingProperty.CGST_LEDGER);
            this.AppSetting.SGSTLedgerId = GetDefaultLedgers(SettingProperty.SGST_LEDGER);
            this.AppSetting.IGSTLedgerId = GetDefaultLedgers(SettingProperty.IGST_LEDGER);

            this.AppSetting.TDSOnFDInterestLedgerId = GetDefaultLedgers(SettingProperty.TDS_ON_FD_INTEREST_LEDGER);

            // To fix the GSt Start Date
            this.AppSetting.GSTStartDate = new DateTime(2017, 7, 1).AddMonths(0).AddDays(0);

            //To fix default zero gst class id
            this.AppSetting.GSTZeroClassId = GetZeroGSTClassId();
        }

        public int GetDefaultLedgers(string LedgerName)
        {
            int LedgerId = 0;
            try
            {

                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    ledgersystem.LedgerName = LedgerName;
                    ResultArgs resultArg = ledgersystem.FetchLedgerIdByLedgerName(LedgerName);
                    if (resultArg != null && resultArg.Success)
                    {
                        LedgerId = resultArg.DataSource.Sclar.ToInteger;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }

            return LedgerId;
        }

        /// <summary>
        /// On 28/12/2019, to get 0 values gst class id
        /// </summary>
        /// <returns></returns>
        public int GetZeroGSTClassId()
        {
            int zeroGSTClassid = 0;
            try
            {
                using (GSTClassSystem GstClass = new GSTClassSystem())
                {
                    resultArgs = GstClass.FetchGSTClassList();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtGSTClass = resultArgs.DataSource.Table;
                        dtGSTClass.DefaultView.RowFilter = "GST_NAME='" + GST_ZERO_CLASS + "'";
                        if (dtGSTClass.DefaultView.Count == 1)
                        {
                            zeroGSTClassid = UtilityMember.NumberSet.ToInteger(dtGSTClass.DefaultView[0]["GST_ID"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }

            return zeroGSTClassid;
        }

        private void ApplyRecentPrjectDetails()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.AppSetting.YearFrom) && !string.IsNullOrEmpty(this.AppSetting.YearTo) && !string.IsNullOrEmpty(this.AppSetting.BookBeginFrom))
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
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        private void ApplyLicensePeriod()
        {
            try
            {
                using (LegalEntitySystem legalEntitySystem = new LegalEntitySystem())
                {
                    resultArgs = legalEntitySystem.ApplyLicensePeriod(LicensePath);
                    if (SettingProperty.EnableMultipleDBBrowseLicenseKey && this.AppSetting.AccesstoMultiDB == (int)YesNo.Yes)
                    {
                        if (!string.IsNullOrEmpty(RecentDBName))
                        {
                            GetMultiLicenseFilename(RecentDBName);
                            MultiDBLicenseKeyName = (string.IsNullOrEmpty(MultiDBLicenseKeyName)) ? LicensePath : Path.Combine(Application.StartupPath.ToString(), MultiDBLicenseKeyName);
                            if (File.Exists(MultiDBLicenseKeyName))
                            {
                                SettingProperty.ActiveDatabaseLicenseKeypath = MultiDBLicenseKeyName;
                                resultArgs = legalEntitySystem.ApplyLicensePeriod(MultiDBLicenseKeyName);
                            }
                        }
                    }
                    else
                    {
                        resultArgs = legalEntitySystem.ApplyLicensePeriod(LicensePath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        /// <summary>
        /// This is to enable/disable modules based on the license key
        /// </summary>
        private void SetModuleProperties()
        {
            SettingProperty.EnableAsset = false;
            SettingProperty.EnableStock = false;
            SettingProperty.EnablePayroll = false;
            SettingProperty.EnableNetworking = false;

            string LicenseModules = this.AppSetting.NoOfModules;
            if (!string.IsNullOrEmpty(LicenseModules))
            {
                string[] LicModules = LicenseModules.Split(',');
                if (LicModules.Count() > 1)
                {
                    foreach (string licmodule in LicModules)
                    {
                        if (!string.IsNullOrEmpty(licmodule))
                        {
                            if (licmodule == AcmeerpModules.AcmeerpFixedAsset.ToString())
                            {
                                SettingProperty.EnableAsset = true;
                            }
                            else if (licmodule == AcmeerpModules.AcmeerpStock.ToString())
                            {
                                SettingProperty.EnableStock = true;
                            }
                            else if (licmodule == AcmeerpModules.AcmeerpPayroll.ToString())
                            {
                                SettingProperty.EnablePayroll = true;
                            }
                            else if (licmodule == AcmeerpModules.AcmeerpNetworking.ToString())
                            {
                                SettingProperty.EnableNetworking = true;
                            }
                        }
                    }
                }
                else if (LicModules.Count() == 1)
                {
                    foreach (string licModule in LicModules)
                    {
                        if (!string.IsNullOrEmpty(licModule))
                        {
                            if (licModule == AcmeerpModules.AcmeerpPayroll.ToString())
                            {
                                SettingProperty.EnablePayrollOnly = true;
                                SettingProperty.EnablePayroll = true;
                            }
                        }
                    }
                }

            }
        }

        public void FetchUserRights()
        {
            try
            {
                using (UserRightsSystem userRightsSystem = new UserRightsSystem())
                {
                    userRightsSystem.UserRoleId = this.LoginUser.LoginUserRoleId;
                    CommonMethod.dtApplyUserRights = userRightsSystem.FetchUserRightsDetails();
                    this.AppSetting.RoleId = this.LoginUser.LoginUserRoleId;
                    this.AppSetting.LoginUserId = this.LoginUser.LoginUserId;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        /// <summary>
        /// On 03/05/2024, Update Multi DB xml file into database
        /// </summary>
        public void UpdateMutiDBXMLConfiguration()
        {
            try
            {
                if (AppSetting.AccesstoMultiDB == 1)
                {
                    using (SettingSystem settingsys = new SettingSystem())
                    {
                        settingsys.UpdateMultiDBXMLConfigurationInAcperp();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        public void FetchAcMeERPReportLogo()
        {
            try
            {
                using (UserSystem userSystem = new UserSystem())
                {
                    resultArgs = userSystem.FetchLogo();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                        this.AppSetting.AcMeERPLogo = (byte[])resultArgs.DataSource.Table.Rows[0][userSystem.AppSchema.User.LOGOColumn.ColumnName] == null ?
                           ImageProcessing.ImageToByteArray(global::ACPP.Properties.Resources.Default_Photo) :
                            (byte[])resultArgs.DataSource.Table.Rows[0][userSystem.AppSchema.User.LOGOColumn.ColumnName];
                    else
                    {
                        this.AppSetting.AcMeERPLogo = ImageProcessing.ImageToByteArray(global::ACPP.Properties.Resources.Default_Photo);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void LoadCultureNames()
        {
            DataTable dtCultureSource = null;
            DataRow drCultureSource = null;
            ApplicationSchema.CultureDataTable dtCulure = new ApplicationSchema.CultureDataTable();
            ResourceManager resourceManger = new ResourceManager(typeof(frmSettings));
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
                this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpUILanguage, dtCultureSource, dtCulure.DISPLAY_NAMEColumn.ColumnName, dtCulure.CULTUREColumn.ColumnName);

                //  glkpUILanguage.EditValue = SettingProperty.EnableLocalization ? "fr-FR" : glkpUILanguage.Properties.GetKeyValue(0);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        /// <summary>
        /// Method to construct connection string 
        /// </summary>
        /// <param name="connString">Connection string from app.config</param>
        /// <param name="option">Data to find Default:database</param>
        /// <param name="value">Data to replace Default:connection string db name</param>
        /// <returns>New connection string</returns>
        private String changeConnStringItem(string connString, string option, string value)
        {
            String[] conItems = connString.Split(';');
            String result = "";
            foreach (String item in conItems)
            {
                if (item.Trim().StartsWith(option.Trim()))
                {
                    result += option + "=" + value + ";";
                    RecentDBName = value;
                }
                else
                {
                    result += item + ";";
                }
            }
            return result.TrimEnd(';');
        }

        /// <summary>
        /// Method to Find Databse Name from Connection string
        /// </summary>
        /// <param name="connString">Connection string from app.config</param>
        /// <param name="option">Data to find Default:database</param>
        /// <returns>Databse Name</returns>
        private String FetchDbFromConnectionsstring(string connString, string option)
        {
            String[] conItems = connString.Split(';');
            String result = string.Empty;
            foreach (String item in conItems)
            {
                if (item.Trim().StartsWith(option.Trim()))
                {
                    result = item.Trim().Remove(0, 9);
                }
            }
            return result;
        }

        /// <summary>
        /// Assign Connection string to app.config file
        /// </summary>
        /// <param name="constring"></param>
        private void SetConnectionString(string constring)
        {
            try
            {
                string path = Path.Combine(Application.StartupPath.ToString(), "ACPP.exe.Config");
                ExeConfigurationFileMap map = new ExeConfigurationFileMap();
                map.ExeConfigFilename = path;
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
                config.ConnectionStrings.ConnectionStrings[AppSettingName.AppConnectionString.ToString()].ConnectionString = constring;
                config.Save(ConfigurationSaveMode.Modified, true);
                ConfigurationHandler.Instance.ConnectionString = constring;
                ConfigurationManager.RefreshSection(AppSettingName.AppConnectionString.ToString());
                path = Path.Combine(Application.StartupPath.ToString(), "ACPP.vshost.exe.Config");
                if (File.Exists(path))
                {
                    map.ExeConfigFilename = path;
                    config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
                    config.ConnectionStrings.ConnectionStrings[AppSettingName.AppConnectionString.ToString()].ConnectionString = constring;
                    config.Save(ConfigurationSaveMode.Modified, true);
                    ConfigurationHandler.Instance.ConnectionString = constring;
                    ConfigurationManager.RefreshSection(AppSettingName.AppConnectionString.ToString());
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message);
            }
            finally { }

        }

        /// <summary>
        /// Method to load restored database name 
        /// </summary>
        private void LoadDatabasesfromXml()
        {
            try
            {
                DataSet dtDb = new DataSet();
                //Fetch current databse name connection string
                string dbname = RecentDBName = FetchDbFromConnectionsstring(connection_string, "database");
                SettingProperty.RestoreMultipleDBPath = @"C:\AcME_ERP\";
                // Check for Default Directory
                if (!Directory.Exists(SettingProperty.RestoreMultipleDBPath))
                {
                    Directory.CreateDirectory(SettingProperty.RestoreMultipleDBPath);
                }
                // Check for Default MultiDB.xml File
                if (!File.Exists(Path.Combine(SettingProperty.RestoreMultipleDBPath.ToString(), SettingProperty.RestoreMultipleDBFileName)))
                {
                    File.Create(SettingProperty.RestoreMultipleDBFileName);
                    string ts = @"<Databases>  <MultiBranch>  <Restore_Db>" + dbname + "</Restore_Db> <MultipleLicenseKey>AcMEERPLicense.xml</MultipleLicenseKey> <RestoreDBName>" + dbname + "</RestoreDBName> </MultiBranch></Databases>";
                    File.AppendAllText(Path.Combine(SettingProperty.RestoreMultipleDBPath.ToString(), SettingProperty.RestoreMultipleDBFileName), ts);
                }
                SettingProperty.RestoreMultipleDBPath = Path.Combine(SettingProperty.RestoreMultipleDBPath.ToString(), SettingProperty.RestoreMultipleDBFileName);
                //Fetch data from xml file
                dtDb = XMLConverter.ConvertXMLToDataSet(SettingProperty.RestoreMultipleDBPath);

                if (dtDb.Tables.Count > 0)
                {
                    if (!dtDb.Tables[0].Columns.Contains("MultipleLicenseKey"))
                    {
                        dtDb.Tables[0].Columns.Add("MultipleLicenseKey", typeof(string));
                    }
                    if (!dtDb.Tables[0].Columns.Contains("RestoreDBName"))
                    {
                        dtDb.Tables[0].Columns.Add("RestoreDBName", typeof(string));
                    }
                    //dtDb.Tables[0].Rows.Add(RecentDBName);
                    if (dtDb.Tables[0].Rows.Count > 0)
                    {
                        DataTable dtTemp = dtDb.Tables[0].Clone();
                        dtLicenseFile = dtDb.Tables[0];
                        using (DashBoardSystem dashboardsystem = new DashBoardSystem())
                        {
                            foreach (DataRow dr in dtDb.Tables[0].Rows)
                            {
                                //Check for the DB exists in the database
                                resultArgs = dashboardsystem.CheckDatabase(dr[0].ToString());
                                if (string.IsNullOrEmpty(resultArgs.Message))
                                {
                                    if (resultArgs.Success && resultArgs.RowsAffected == 1)
                                    {
                                        dtTemp.Rows.Add(dr["Restore_Db"].ToString(),
                                            !string.IsNullOrEmpty(dr["MultipleLicenseKey"].ToString()) ? dr["MultipleLicenseKey"].ToString() : "AcMEERPLicense.xml",
                                            !string.IsNullOrEmpty(dr["RestoreDBName"].ToString()) ? dr["RestoreDBName"].ToString() : dr[0].ToString());
                                        LicensePath = Path.Combine(Application.StartupPath.ToString(), dr["MultipleLicenseKey"].ToString());
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            if (dtTemp != null && dtTemp.Rows.Count > 0)
                            {
                                DataSet dsTempUpMulti = new DataSet("MultiBranch");
                                dsTempUpMulti.Tables.Add(dtTemp);
                                XMLConverter.WriteToXMLFile(dsTempUpMulti, SettingProperty.RestoreMultipleDBPath);
                            }
                            SettingProperty.dtLoginDB = dtTemp;
                        }
                        if (dtTemp.Rows.Count > 0)
                        {
                            this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpDatabase, dtTemp, "RestoreDBName", "Restore_Db");
                            //glkpDatabase.EditValue = glkpDatabase.Properties.GetKeyValue(0);
                            glkpDatabase.EditValue = !string.IsNullOrEmpty(RecentDBName) ? RecentDBName : SettingProperty.ActiveDatabaseName;
                        }
                        else
                        {
                            dtTemp.Rows.Add(RecentDBName);
                            this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpDatabase, dtTemp, "RestoreDBName", "Restore_Db");
                            glkpDatabase.EditValue = glkpDatabase.Properties.GetKeyValue(0);
                        }
                    }
                }
                else
                {
                    this.ShowMessageBoxError("Multiple database is not configured correctly,  Contact Acme.erp Support Team");
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private static bool CheckEncryptComponent()
        {
            bool Rtn = false;
            try
            {
                SimpleEncrypt.SimpleEncDec test = new SimpleEncrypt.SimpleEncDec();
                Rtn = true;
            }
            catch
            {
                Rtn = false;
            }
            return Rtn;
        }

        public void ActivateLicenseKey()
        {
            object lasteditvals = glkpDatabase.EditValue;
            this.TopMost = false;
            //frmUpdateLicence updateLicense = new frmUpdateLicence();
            //updateLicense.ShowDialog();

            frmPortalUpdates frmLicenseKey = new frmPortalUpdates(PortalUpdates.UpdateLicense);
            frmLicenseKey.ShowDialog();
            frmLicenseKey.TopMost = true;
            if (frmLicenseKey.DialogResult == DialogResult.OK)
            {
                if (this.AppSetting.AccesstoMultiDB == (int)YesNo.Yes)
                {
                    LoadDatabasesfromXml();
                }
            }

            //SettingProperty.Is_Application_Logout = false;
            //Application.Exit();

            glkpDatabase.EditValue = lasteditvals;
        }

        public void LoadLastLoggedinUser(string Uname, bool IsFirst)
        {
            //On 24/05/2021, to retain remember logged user for multi branch 
            //Get and update already logged user information based on Branch
            string[] dftreadVal = new string[5];
            try
            {
                if (IsFirst)
                {
                    //On 24/05/2021, to retain/remember logged user for multi branch 
                    //to clear values
                    txtUserName.Text = txtPassword.Text = string.Empty;
                    chkRememberPassword.Checked = false;

                    //On 24/05/2021, to retain remember logged user for multi branch 
                    //get logged user file name based on multi db file/defualt db file
                    string loggedinusernamefile = DefaultDBLoggedinUserNamePath;
                    if (AppSetting.AccesstoMultiDB == 1 && SettingProperty.ActiveDatabaseName.ToUpper() != "ACPERP")
                    {
                        //If Branch logged user files not exists, let us take from default logged user
                        if (File.Exists(Path.Combine(Application.StartupPath.ToString(), SettingProperty.ActiveDatabaseAliasName + "LoggedInUser.txt")))
                        {
                            loggedinusernamefile = Path.Combine(Application.StartupPath.ToString(), SettingProperty.ActiveDatabaseAliasName + "LoggedInUser.txt");
                        }
                    }

                    //On 24/05/2021, to retain remember logged user for multi branch 
                    /*if (!File.Exists(loggedinusernmae))
                    {
                        File.Create(loggedinusernmae);
                    }
                    string LastUname = File.ReadAllText(loggedinusernmae);*/

                    if (File.Exists(loggedinusernamefile))
                    {
                        string LastUname = string.Empty;
                        using (StreamReader reader = new StreamReader(loggedinusernamefile))
                        {
                            LastUname = reader.ReadLine();
                        }

                        if (!(string.IsNullOrEmpty(LastUname)))
                        {
                            dftreadVal = LastUname.Split(',');
                            txtUserName.Text = string.IsNullOrEmpty(LastUname) ? Uname : dftreadVal[0]; // index 0 to get user Name
                            if (dftreadVal.Length > 1)
                            {
                                txtPassword.Text = this.objSimpleEncrypt.DecryptString(dftreadVal[1]); // index 0 to get Password
                                chkRememberPassword.Checked = true;
                            }
                        }
                    }
                }
                else
                {
                    if (AppSetting.AccesstoMultiDB == 0 || SettingProperty.ActiveDatabaseName.ToUpper() == "ACPERP")
                    {
                        string DftVal = (chkRememberPassword.Checked) ? Uname + "," + this.objSimpleEncrypt.EncryptString(txtPassword.Text.Trim()) : Uname;
                        File.WriteAllText(DefaultDBLoggedinUserNamePath, DftVal);
                    }
                    else
                    {
                        string BranchDBLoggedinUserName = Path.Combine(Application.StartupPath.ToString(), SettingProperty.ActiveDatabaseAliasName + "LoggedInUser.txt");
                        string branchvalue = (chkRememberPassword.Checked) ? Uname + "," + this.objSimpleEncrypt.EncryptString(txtPassword.Text.Trim()) : Uname;
                        File.WriteAllText(BranchDBLoggedinUserName, branchvalue);
                    }
                }
            }
            catch (Exception err)
            {
                string errormessage = err.Message;
            }
        }

        public void GetMultiLicenseFilename(string DBName)
        {
            try
            {
                DataView dvLicense = null;
                if (dtLicenseFile != null && dtLicenseFile.Rows.Count > 0)
                {
                    dvLicense = dtLicenseFile.DefaultView;
                    dvLicense.RowFilter = "Restore_Db= '" + CommonMethod.EscapeLikeValue(DBName) + "'";
                    if (dvLicense.Count > 0)
                    {
                        MultiDBLicenseKeyName = (!string.IsNullOrEmpty(dvLicense.ToTable().Rows[0]["MultipleLicenseKey"].ToString())) ? dvLicense.ToTable().Rows[0]["MultipleLicenseKey"].ToString() : string.Empty;
                    }
                    //foreach (DataRow dr in dtLicenseFile.Rows)
                    //{
                    //    if (dr["Restore_Db"].Equals(DBName))
                    //    {
                    //        MultiDBLicenseKeyName = dr["MultipleLicenseKey"].ToString();
                    //        break;
                    //    }
                    //}
                }

            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        public void HostAcMEService()
        {
            try
            {
                ServiceHost host;
                host = new ServiceHost(typeof(AcMEService.AcMeERPService));
                host.Open();
                AcMELog.WriteLog("AcME ERP Service started at :" + DateTime.Now);
            }
            catch (Exception ed)
            {
                AcMELog.WriteLog(ed.ToString());
            }
        }

        /// <summary>
        /// For temp purpose mysore 08/01/2020
        /// </summary>
        private void UpdateBudgetGroupForMysore()
        {
            if (AppSetting.IS_DIOMYS_DIOCESE)
            {
                using (LedgerSystem ledger = new LedgerSystem())
                {
                    //For Non REC
                    ledger.UpdateBudgetGroupForMysore();
                }

                //For REC
                string[] RCLedgers = { "Net Amount", "Employees PF", "Management", "Admin Charges", "LIC", "Telephone Bill", "Security/ House Keeping", "Income Tax Deduction", "Water Bill" };
                int budgetgroupid = 1; //Rece
                int budgetsubgroupid = 1; //1. Salary, 2. PF, 3. ESIC
                int sortorder = 1;

                foreach (string item in RCLedgers)
                {
                    if (item == "Net Amount") //1. Salaary
                    {
                        budgetsubgroupid = 1;
                    }
                    else if (item == "Employees PF" || item == "Management" || item == "Admin Charges") //2. PF
                    {
                        budgetsubgroupid = 2;
                    }
                    else  //3. ESIC
                    {
                        budgetsubgroupid = 3;
                    }


                    using (LedgerSystem ledger = new LedgerSystem())
                    {
                        ledger.UpdateBudgetSubGroupForMysore(item, budgetsubgroupid, sortorder);
                        sortorder++;
                    }
                }

            }
        }
        #endregion

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtUserName);
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtPassword);
        }

        private void glkpDatabase_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpDatabase);
        }

        public bool CheckIsMultiDB(string licensePath)
        {
            bool IsMultiDb = false;
            if (File.Exists(licensePath))
            {
                DataSet ds = new DataSet();
                ds.ReadXml(licensePath);
                if (ds != null && ds.Tables[0].Rows.Count != 0)
                {
                    DataTable dtLicense = ds.Tables[0];
                    foreach (DataRow dr in dtLicense.Rows)
                    {
                        this.AppSetting.AccesstoMultiDB = UtilityMember.NumberSet.ToInteger(dr["AccessToMultiDB"].ToString());
                        //On 31/03/2022, To have base license  details--------------------------------------------------------------------
                        this.AppSetting.BaseLicensekeyNumber = this.objSimpleEncrypt.DecryptString(dr["LICENSE_KEY_NUMBER"].ToString());
                        this.AppSetting.BaseLicenseHeadOfficeCode = this.objSimpleEncrypt.DecryptString(dr["HEAD_OFFICE_CODE"].ToString());
                        this.AppSetting.BaseLicenseBranchCode = this.objSimpleEncrypt.DecryptString(dr["BRANCH_OFFICE_CODE"].ToString());
                        this.AppSetting.BaseLicenseBranchName = this.objSimpleEncrypt.DecryptString(dr["BRANCH_OFFICE_NAME"].ToString());
                        //-----------------------------------------------------------------------------------------------------------------

                        if (this.AppSetting.AccesstoMultiDB > 0)
                        {
                            IsMultiDb = true;
                        }
                        else
                        {
                            IsMultiDb = false;
                        }
                    }
                }
            }

            return IsMultiDb;
        }

        private void frmAcMELogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F5 && AppSetting.AccesstoMultiDB == 1)
            {
                string rtn = "Are you sure to update Multi Database XML from backup ?";
                if (MessageRender.ShowConfirmationMessage(rtn, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    using (SettingSystem settingsys = new SettingSystem())
                    {
                        settingsys.UpdateMultiDBXMLConfigurationFromBackup();
                        LoadDatabasesfromXml();
                    }
                }
                
                /*string refcode = string.Empty;
                DialogResult diaresult = InputBox("Acme.erp Configuration", "Enter password to enable Acme.erp configuration options", ref refcode, true);
                if (diaresult == System.Windows.Forms.DialogResult.OK)
                {
                    if (refcode == SettingProperty.AcmeerpHiddenOperationPassword)
                    {
                        string rtn = "Are you sure to update Multi Database XML from backup ?";
                        if (MessageRender.ShowConfirmationMessage(rtn, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            using (SettingSystem settingsys = new SettingSystem())
                            {
                                settingsys.UpdateMultiDBXMLConfigurationFromBackup();
                                LoadDatabasesfromXml();
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessageBoxWarning("Invalid password!");
                    }
                }*/
            }
        }

    }
}