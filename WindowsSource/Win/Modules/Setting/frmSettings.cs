using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Threading;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.XtraBars.Ribbon;

using Bosco.Utility;
using Bosco.DAO.Schema;
using Bosco.Model;
using Bosco.Model.UIModel;
using Bosco.Model.Setting;
using DevExpress.XtraGrid.Views.Grid;
using ACPP.Modules.TDS;
using Bosco.Model.TDS;
using Bosco.Utility.ConfigSetting;
using System.IO;
using Bosco.DAO;

namespace ACPP.Modules.Master
{
    public partial class frmSettings : frmFinanceBaseAdd
    {
        #region Declaration
        ResultArgs resultArgs = null;

        DataTable dtSetting = null;
        public DataTable dtUISetting = null;
        #endregion

        #region Property
        private static DataView dvSettingTemp = null;
        public DataView SettingInfoTemp
        {
            set
            {
                dvSettingTemp = value;
            }
            get
            {
                return dvSettingTemp;
            }
        }

        private static DataView dvUISettingTemp = null;
        public DataView UISettingInfoTemp
        {
            set
            {
                dvUISettingTemp = value;
            }
            get
            {
                return dvUISettingTemp;
            }
        }

        private int negativeValue = 0;
        private int NegativeValue
        {
            get
            {
                return negativeValue;
            }
            set
            {
                negativeValue = value;
            }
        }

        private int positiveValue = 0;
        private int PositiveValue
        {
            get
            {
                return positiveValue;
            }
            set
            {
                positiveValue = value;
            }
        }

        private int accperiodYearId = 0;
        private int AccperiodYearId
        {
            get
            {
                return accperiodYearId;
            }
            set
            {
                accperiodYearId = value;
            }
        }

        private string yearFrom = "";
        private string YearFrom
        {
            get
            {
                return yearFrom;
            }
            set
            {
                yearFrom = value;
            }
        }

        private string booksBeginningFrom = "";
        private string BooksBeginningFrom
        {
            get
            {
                return booksBeginningFrom;
            }
            set
            {
                booksBeginningFrom = value;
            }
        }

        private string yearTo = "";
        private string YearTo
        {
            get
            {
                return yearTo;
            }
            set
            {
                yearTo = value;
            }
        }

        private string currencycode = string.Empty;

        #endregion

        #region Constructor
        public frmSettings()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void frmSettings_Load(object sender, EventArgs e)
        {
            lblCurrencyName.Text = string.Empty;
            System.Resources.ResourceManager resoureManager = new ResourceManager(typeof(frmSettings));
            string width = resoureManager.GetString("width");
            if (!string.IsNullOrEmpty(width))
            {
                this.Width = Convert.ToInt32(width);
            }
            LoadMonths();
            LoadTheme();
            LoadCultureNames();
            LoadCurrency();
            LoadCreditType();
            LoadLocations();
            LockLocation();
            //  LoadBankAccount();
            BindValues();
            SettingInfoTemp = this.AppSetting.SettingInfo;
            UISettingInfoTemp = this.AppSetting.UISettingInfo;
            DateTime dtToday = DateTime.Now;
            txtDatePreview.Text = dtToday.ToString("d"); //Convert.ToDateTime(this.AppSetting.YearFrom).ToString("d");
            //btnApply.Enabled = true;
            //glkpMonths.Enabled = true;
            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                ApplyRights();
                tcLocalization.TabPages[0].PageVisible = false; //General Setting 
                tcLocalization.TabPages[2].PageVisible = false; //Proxy Setting 
            }
            else
            {
                tcLocalization.TabPages[0].PageVisible = true; //General Setting 
                tcLocalization.TabPages[1].PageVisible = true; //UI
                tcLocalization.TabPages[2].PageVisible = true; //Proxy Setting 
                tcLocalization.SelectedTabPageIndex = 0;
            }
            // LoadProjects();

            //On 03/09/2019, to enable payroll password
            lcPayrollPassword.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (SettingProperty.EnablePayroll)
            {
                lcPayrollPassword.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }

            //On 22/10/2021, hide proxy option ---------------------------------
            lcChkProxy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            tpProxyServer.Text = "Configuration";
            ShowBranchDatabaseDetails();
            lcRestoreEmptyDB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcRestoreEmptySpace.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcConfigureMutlDBXML.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcConfigureAllMutlDBXML.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcMultiDBEmptySpace.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //------------------------------------------------------------------

            if (this.AppSetting.IS_SDB_INM)
            {
                this.lciDefaultMerge.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateSettingDetails())
                {
                    if (this.UtilityMember.NumberSet.ToInteger(LoginUser.LoginUserId) == 1)
                    {
                        SaveGlobalSetting();
                    }
                    else
                    {
                        SaveUISetting();
                    }
                    //this.ShowMessageBox("Look at the preview of your settings and click on Ok to set for the entire application");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Settings.SETTING_RESTART_PREVIEW));
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateSettingDetails())
                {
                    ISetting isetting;
                    if (this.UtilityMember.NumberSet.ToInteger(LoginUser.LoginUserId) == 1)
                    {
                        SaveGlobalSetting();
                        isetting = new GlobalSetting();
                        resultArgs = isetting.SaveSetting(dtSetting);
                    }
                    else
                    {
                        SaveUISetting();
                        isetting = new UISetting();
                        resultArgs = isetting.SaveSetting(dtUISetting);
                    }
                    if (resultArgs.Success)
                    {
                        isetting.ApplySetting();
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Settings.SETTING_APPLICATION_RESTART_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Bosco.Utility.ConfigSetting.SettingProperty.Is_Application_Logout = true;
                            Application.Restart();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            // this.AppSetting.SettingInfo = SettingInfoTemp;

            //this.Close();
        }

        private void glkpCountry_EditValueChanged(object sender, EventArgs e)
        {
            //btnApply.Enabled = true;
            int CountryId = this.UtilityMember.NumberSet.ToInteger(glkpCountry.EditValue.ToString());
            lblCurrencyName.Text = string.Empty;
            lblCurrencySymbol.Text = string.Empty;
            currencycode = string.Empty;
            try
            {
                if (CountryId != 0)
                {
                    using (CountrySystem countrySystem = new CountrySystem(CountryId))
                    {
                        // lblCurrencyCode.Text = countrySystem.CurrencyCode;
                        lblCurrencySymbol.Text = countrySystem.CurrencySymbol;
                        lblCurrencyName.Text = countrySystem.CurrencyName;
                        currencycode = countrySystem.CurrencyCode;
                        LoadPositivePreview();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void icboTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserLookAndFeel.Default.SetSkinStyle(icboTheme.Text);
        }

        private void gvAccountingPeriod_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                if (view.GetRowCellDisplayText(e.RowHandle, view.Columns["STATUS"]).ToString() == "Active")
                {
                    e.Appearance.BackColor = Color.LightGreen;
                }
            }
        }

        private void chkyearSelect_CheckedChanged(object sender, EventArgs e)
        {
            //if (gvAccountingPeriod.RowCount != 1)
            //{
            //    string tarnsId = gvAccountingPeriod.GetFocusedRowCellValue(gvColAccYearId).ToString();
            //    string status = gvAccountingPeriod.GetFocusedRowCellValue(gvColSelect).ToString();
            //    SetCurrentTransPeriod(tarnsId, status);
            //}
            //else
            //{
            //    CheckEdit chkSelect = sender as CheckEdit;
            //    chkSelect.Checked = true;
            //}
        }
        #endregion

        #region Methods
        private bool ValidateSettingDetails()
        {
            bool isValidSetting = true;
            if (string.IsNullOrEmpty(glkpUILanguage.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Settings.SETTING_LANGUAGE_INVALID));
                isValidSetting = false;
                glkpUILanguage.Focus();
            }
            //else if (string.IsNullOrEmpty(glkBankAccount.Text))
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Settings.SETTING_FOREIGN_BANKACCOUNT));
            //    isValidSetting = false;
            //    glkBankAccount.Focus();
            //}
            else if (string.IsNullOrEmpty(cboUIDateFormat.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Settings.SETTING_DATE_FORMAT_INVALID));
                isValidSetting = false;
                cboUIDateFormat.Focus();
            }
            else if (string.IsNullOrEmpty(cboUIDateSeparator.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Settings.SETTING_DATE_SEPARATOR_INVALID));
                isValidSetting = false;
                cboUIDateSeparator.Focus();
            }
            else if (string.IsNullOrEmpty(glkpCountry.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Settings.SETTING_CURRENCY_INVALID));
                isValidSetting = false;
                glkpCountry.Focus();
            }
            else if (string.IsNullOrEmpty(cboCurrencyPosition.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Settings.SETTING_CURRENCY__POSITION_INVALID));
                isValidSetting = false;
                cboCurrencyPosition.Focus();
            }
            //else if (string.IsNullOrEmpty(cboCodePosition.Text))
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Settings.SETTING_CURRENCY__POSITION_INVALID));
            //    isValidSetting = false;
            //    cboCodePosition.Focus();
            //}
            else if (string.IsNullOrEmpty(cboDigitGrouping.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Settings.SETTING_DIGIT_GROUPING_INVALID));
                isValidSetting = false;
                cboDigitGrouping.Focus();
            }
            else if (string.IsNullOrEmpty(cboGroupingSeparator.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Settings.SETTING_DIGIT_GROUPING_SEPARATOR_INVALID));
                isValidSetting = false;
                cboGroupingSeparator.Focus();
            }
            else if (string.IsNullOrEmpty(cboDecimalPlaces.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Settings.SETTING_DECIMAL_PLACES_INVALID));
                isValidSetting = false;
                cboDecimalPlaces.Focus();
            }
            else if (string.IsNullOrEmpty(cboDecimalSeparator.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Settings.SETTING_DECIMAL_PLACES_SEPARATOR_INVALID));
                isValidSetting = false;
                cboDecimalSeparator.Focus();
            }
            else if (string.IsNullOrEmpty(cboNegativeSign.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Settings.SETTING_NEGATIVE_SIGN_INVALID));
                isValidSetting = false;
                cboNegativeSign.Focus();
            }
            else if (chkProxyUse.Checked)
            {
                if (string.IsNullOrEmpty(txtProxyAddress.Text) || string.IsNullOrEmpty(txtProxyPort.Text))
                {
                    this.ShowMessageBox("Proxy Address or Port is empty");
                    isValidSetting = false;
                    txtProxyAddress.Focus();
                }
                else
                {
                    if (chkProxyAuthentication.Checked)
                    {
                        if (string.IsNullOrEmpty(txtProxyUName.Text) || string.IsNullOrEmpty(txtProxyPassword.Text))
                        {
                            this.ShowMessageBox("Proxy User Name or Password is empty");
                            isValidSetting = false;
                            txtProxyUName.Focus();
                        }
                    }
                }
            }
            else if (string.IsNullOrEmpty(glkpLocation.Text))
            {
                this.ShowMessageBox("Branch Location is not found. Default Location 'Primary' must be selected. Check your License Key");
                isValidSetting = false;
                glkpLocation.Focus();
            }
            else if (lcPayrollPassword.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                if ((txtPayrollPassword.Text != this.AppSetting.PayrollPassword) && !String.IsNullOrEmpty(this.AppSetting.PayrollPassword))
                {
                    string oldPayrollpassword = string.Empty;
                    DialogResult dialogresult = this.InputBox("Acmeerp", "Enter current Payroll Password to change new Password", ref oldPayrollpassword, true);
                    if (oldPayrollpassword == "" || (oldPayrollpassword != this.AppSetting.PayrollPassword))
                    {
                        this.ShowMessageBox("Invalid Current Payroll Password");
                        isValidSetting = false;
                        txtPayrollPassword.Focus();
                    }
                }
            }
            //else if (!IsTransactionPeriodValid())
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Settings.SETTING_TRANSACTION_FAILURE));
            //    isValidSetting = false;
            //    gvAccountingPeriod.Focus();
            //}
            //else if (string.IsNullOrEmpty(detBookbeginingFrom.Text))
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Settings.SETTING_BOOK_BEGINNING_EMPTY));
            //    isValidSetting = false;
            //    detBookbeginingFrom.Focus();
            //}
            return isValidSetting;
        }

        public void LoadCultureNames()
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
                glkpUILanguage.EditValue = glkpUILanguage.Properties.GetKeyValue(0);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        private void LoadCreditType()
        {
            CreditBalance creditType = new CreditBalance();
            DataView dvCreditType = this.UtilityMember.EnumSet.GetEnumDataSource(creditType, Sorting.None);
            if (dvCreditType.Count > 0)
            {
                DataTable dtCreditType = dvCreditType.ToTable();
                string EnumValProjectWise = this.UtilityMember.EnumSet.GetDescriptionFromEnumValue(CreditBalance.ProjectWide);
                string EnumValSocietyWise = this.UtilityMember.EnumSet.GetDescriptionFromEnumValue(CreditBalance.SocietyWide);
                string EnumValApplicationWise = this.UtilityMember.EnumSet.GetDescriptionFromEnumValue(CreditBalance.ApplicationWide);

                dtCreditType.Rows[0]["Name"] = EnumValProjectWise;
                dtCreditType.Rows[1]["Name"] = EnumValSocietyWise;
                dtCreditType.Rows[2]["Name"] = EnumValApplicationWise;

                glkpCreditBalance.Properties.DataSource = dtCreditType;
                glkpCreditBalance.Properties.DisplayMember = "Name";
                glkpCreditBalance.Properties.ValueMember = "Id";
                glkpCreditBalance.EditValue = 1;

                glkpCreditBalance.Properties.ImmediatePopup = true;
                glkpCreditBalance.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                glkpCreditBalance.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;

            }
        }

        private void LockLocation()
        {
            using (MappingSystem mappingSystem = new MappingSystem())
            {
                resultArgs = mappingSystem.FetchProjectsLookup();
                if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    glkpLocation.Enabled = false;
                }
            }
        }

        private void LoadLocations()
        {
            string branchLocation = this.AppSetting.BranchLocations;
            DataTable dtBranches = new DataTable();
            dtBranches.Columns.Add("LOCATION_NAME", typeof(string));
            if (!string.IsNullOrEmpty(branchLocation))
            {
                string[] branches = branchLocation.Split(',');
                if (branches.Count() > 0)
                {
                    foreach (string name in branches)
                    {
                        if (!string.IsNullOrEmpty(name))
                        {
                            dtBranches.Rows.Add(name);
                        }
                    }
                }
                else
                {
                    dtBranches.Rows.Add(DefaultLocation.Primary.ToString());
                }

                if (dtBranches != null && dtBranches.Rows.Count > 0)
                {
                    glkpLocation.Properties.DataSource = dtBranches;
                    glkpLocation.Properties.DisplayMember = "LOCATION_NAME";
                    glkpLocation.Properties.ValueMember = "LOCATION_NAME";
                }
            }
        }

        private void LoadCurrency()
        {
            try
            {
                using (CountrySystem countrySystem = new CountrySystem())
                {
                    resultArgs = countrySystem.FetchCountryDetails();
                    if (resultArgs.Success)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpCountry, resultArgs.DataSource.Table, countrySystem.AppSchema.Country.COUNTRYColumn.ToString(), countrySystem.AppSchema.Country.COUNTRY_IDColumn.ToString());
                        glkpCountry.EditValue = glkpCountry.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void LoadMonths()
        {
            try
            {
                var Month = DateTimeFormatInfo.InvariantInfo.MonthNames.TakeWhile(m => m != string.Empty).ToList();
                //  glkpMonths.Properties.DataSource = Month;
                //  glkpMonths.EditValue = glkpMonths.Properties.GetKeyValue(8);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
        }

        public void LoadTheme()
        {
            foreach (SkinContainer cnt in SkinManager.Default.Skins)
            {
                DataTable dtThemes = ConstructThemes();
                ImageComboBoxItem imageComboBoxItem = new ImageComboBoxItem(cnt.SkinName, 0);
                foreach (DataRow dr in dtThemes.Rows)
                {
                    if (dr[1].ToString() == imageComboBoxItem.ToString())
                    {
                        icboTheme.Properties.Items.Add(imageComboBoxItem);
                    }
                }
            }
            icboTheme.EditValue = UserLookAndFeel.Default.ActiveSkinName;
        }

        public void BindValues()
        {
            this.glkpCountry.Text = this.AppSetting.Country;
            this.cboCurrencyPosition.Text = this.AppSetting.CurrencyPosition;
            this.cboCodePosition.Text = this.AppSetting.CurrencyCodePosition;
            this.cboGroupingSeparator.Text = this.AppSetting.GroupingSeparator;
            this.cboDecimalPlaces.Text = this.AppSetting.DecimalPlaces;
            this.cboDecimalSeparator.Text = this.AppSetting.DecimalSeparator;
            //  this.detBookbeginingFrom.Text = this.AppSetting.BookBeginFrom;
            this.cboNegativeSign.Text = this.AppSetting.CurrencyNegativeSign;
            this.glkpCreditBalance.Text = this.AppSetting.CreditBalance;
            //    this.txtHighNatAmt.Text = this.AppSetting.HighNaturedAmt;
            this.cboDigitGrouping.SelectedIndex = this.AppSetting.DigitGrouping == "3,2,2" ? 0 : this.AppSetting.DigitGrouping == "3,3,3" ? 1 : 2;
            //    this.cboTransMode.SelectedIndex = this.AppSetting.TransMode == "1" ? 0 : 1;
            //    this.glkpMonths.Text = !string.IsNullOrEmpty(this.AppSetting.Months) ? this.AppSetting.Months : Month.September.ToString();
            this.glkpLocation.Text = (!string.IsNullOrEmpty(this.AppSetting.Location)) ? this.AppSetting.Location : DefaultLocation.Primary.ToString();

            this.rgbDownloadBy.SelectedIndex = (string.IsNullOrEmpty(this.AppSetting.UpdaterDownloadBy) ? 0 : this.UtilityMember.NumberSet.ToInteger(this.AppSetting.UpdaterDownloadBy));

            //On 30/08/2017, Proxy Setting
            this.chkProxyUse.Checked = this.AppSetting.ProxyUse.ToString() == "1" ? true : false;
            this.txtProxyAddress.Text = this.AppSetting.ProxyAddress.ToString();
            this.txtProxyPort.Text = this.AppSetting.ProxyPort.ToString();
            this.chkProxyAuthentication.Checked = this.AppSetting.ProxyAuthenticationUse.ToString() == "1" ? true : false;
            this.txtProxyUName.Text = this.AppSetting.ProxyUserName.ToString();
            this.txtProxyPassword.Text = this.AppSetting.ProxyPassword.ToString();

            if (this.UtilityMember.NumberSet.ToInteger(LoginUser.LoginUserId) == 1)
            {
                this.glkpUILanguage.Text = this.AppSetting.Language;
                this.cboUIDateFormat.Text = this.AppSetting.DateFormat;
                this.cboUIDateSeparator.Text = this.AppSetting.DateSeparator;
                this.icboTheme.Text = this.AppSetting.Themes;
                this.chkProjectSelection.Checked = this.AppSetting.ProjSelection.ToString() == "1" ? true : false;
                this.chkTransclose.Checked = this.AppSetting.TransClose.ToString() == "1" ? true : false;
                //  this.chkPrintVoucherReport.Checked = this.AppSetting.VoucherPrint.ToString() == "1" ? true : false;
                //   this.rgbTransEntryMode.SelectedIndex = (string.IsNullOrEmpty(this.AppSetting.TransEntryMethod)) ? 0 : this.AppSetting.TransEntryMethod == "1" ? 0 : 1;
                this.chkTDS.Checked = this.UIAppSetting.TDSEnabled.ToString() == "1" ? true : false;
                this.chkVoucherPrint.Checked = this.UIAppSetting.UIVoucherPrint.ToString() == "1" ? true : false;
                //    this.chkEnableTransMode.Checked = this.UIAppSetting.EnableTransMode.ToString() == "1" ? true : false;
                this.chkCustomizationform.Checked = this.UIAppSetting.UICustomizationForm.ToString() == "1" ? true : false;
                //     this.chkTDSBooking.Checked = this.AppSetting.EnableBookingAtPayment.ToString() == "1" ? true : false;
                //     this.glkpMonths.Text = !string.IsNullOrEmpty(this.AppSetting.Months) ? this.AppSetting.Months : Month.September.ToString();
                this.glkpLocation.Text = (!string.IsNullOrEmpty(this.AppSetting.Location)) ? this.AppSetting.Location : DefaultLocation.Primary.ToString();
                //     this.rgDonationVoucherPrint.SelectedIndex = this.AppSetting.UIDonationVoucherPrint.ToString() == "1" ? 0 : 1;
                this.txtPayrollPassword.Text = this.AppSetting.PayrollPassword;
            }
            else
            {
                this.glkpUILanguage.Text = this.UIAppSetting.UILanguage;
                this.cboUIDateFormat.Text = this.UIAppSetting.UIDateFormat;
                this.cboUIDateSeparator.Text = this.UIAppSetting.UIDateSeparator;
                this.icboTheme.Text = this.UIAppSetting.UIThemes;
                this.chkProjectSelection.Checked = this.UIAppSetting.UIProjSelection.ToString() == "1" ? true : false;
                this.chkTransclose.Checked = this.UIAppSetting.UITransClose.ToString() == "1" ? true : false;
                this.chkVoucherPrint.Checked = this.UIAppSetting.UIVoucherPrint == "1" ? true : false;
                //  this.chkPrintVoucherReport.Checked = this.AppSetting.UIVoucherPrint.ToString() == "1" ? true : false;
                //   this.rgbTransEntryMode.SelectedIndex = this.UIAppSetting.TransEntryMethod == "1" ? 0 : 1;
                //    this.rgDonationVoucherPrint.SelectedIndex = this.AppSetting.UIDonationVoucherPrint.ToString() == "1" ? 0 : 1;
            }
        }

        private void SaveGlobalSetting()
        {
            // if (ValidateSettingDetails())
            //  {
            Setting setting = new Setting();
            DataView dvSetting = null;

            dvSetting = this.UtilityMember.EnumSet.GetEnumDataSource(setting, Sorting.Ascending);
            dtSetting = dvSetting.ToTable();
            if (dtSetting != null)
            {
                dtSetting.Columns.Add("Value", typeof(string));
                for (int i = 0; i < dtSetting.Rows.Count; i++)
                {
                    Setting SettingName = (Setting)Enum.Parse(typeof(Setting), dtSetting.Rows[i][1].ToString());
                    string Value = "";
                    switch (SettingName)
                    {
                        case Setting.Country:
                            {
                                Value = glkpCountry.EditValue.ToString();
                                break;
                            }
                        //case Setting.Months:
                        //    {
                        //        Value = glkpMonths.EditValue != null ? glkpMonths.Text.ToString() : "";
                        //        break;
                        //    }
                        case Setting.Currency:
                            {
                                Value = lblCurrencySymbol.Text.ToString();
                                break;
                            }
                        case Setting.CurrencyName:
                            {
                                Value = lblCurrencyName.Text.ToString();
                                break;
                            }
                        case Setting.CreditBalance:
                            {
                                Value = glkpCreditBalance.EditValue.ToString();
                                break;
                            }
                        case Setting.CurrencyPosition:
                            {
                                Value = cboCurrencyPosition.SelectedItem.ToString();
                                break;
                            }
                        case Setting.CurrencyPositivePattern:
                            {
                                PositiveValue = cboCurrencyPosition.SelectedIndex == 0 ? (int)CurrencyPositivePattern.Before : (int)CurrencyPositivePattern.After;
                                Value = PositiveValue.ToString();
                                break;
                            }
                        case Setting.CurrencyNegativePattern:
                            {
                                NegativeValue = cboCurrencyPosition.SelectedIndex == 0 ? (NegativeValue = cboNegativeSign.SelectedIndex == 0 ? (int)CurrencyNegativePatternBracket.Before : (int)CurrencyNegativePatternMinus.Before) : (cboNegativeSign.SelectedIndex == 0 ? (int)CurrencyNegativePatternBracket.After : (int)CurrencyNegativePatternMinus.After);
                                Value = NegativeValue.ToString();
                                break;
                            }
                        case Setting.CurrencyNegativeSign:
                            {
                                Value = cboNegativeSign.SelectedItem.ToString();
                                break;
                            }
                        case Setting.CurrencyCode:
                            {
                                Value = currencycode;
                                break;
                            }
                        case Setting.CurrencyCodePosition:
                            {
                                Value = cboCodePosition.SelectedItem.ToString();
                                break;
                            }
                        case Setting.DigitGrouping:
                            {
                                Value = GetDigitGroupSizes();
                                break;
                            }
                        case Setting.GroupingSeparator:
                            {
                                Value = cboGroupingSeparator.SelectedItem.ToString();
                                break;
                            }
                        case Setting.DecimalPlaces:
                            {
                                Value = cboDecimalPlaces.SelectedItem.ToString();
                                break;
                            }
                        case Setting.DecimalSeparator:
                            {
                                Value = cboDecimalSeparator.SelectedItem.ToString();
                                break;
                            }

                        //case Setting.HighNaturedAmt:
                        //    {
                        //        Value = txtHighNatAmt.Text;
                        //        break;
                        //    }
                        case Setting.UILanguage:
                            {
                                Value = glkpUILanguage.EditValue.ToString();
                                break;
                            }
                        case Setting.UIDateFormat:
                            {
                                Value = cboUIDateFormat.SelectedItem.ToString();
                                break;
                            }
                        case Setting.UIDateSeparator:
                            {
                                Value = cboUIDateSeparator.SelectedItem.ToString();
                                break;
                            }
                        case Setting.UIThemes:
                            {
                                Value = icboTheme.SelectedItem.ToString();
                                break;
                            }

                        case Setting.UIProjSelection:
                            {
                                Value = chkProjectSelection.Checked == true ? "1" : "0";
                                break;
                            }
                        case Setting.UITransClose:
                            {
                                Value = chkTransclose.Checked == true ? "1" : "0";
                                break;
                            }
                        //case Setting.TransEntryMethod:
                        //    {
                        //        Value = rgbTransEntryMode.Properties.Items[rgbTransEntryMode.SelectedIndex].Value.ToString();
                        //        break;
                        //    }
                        //case Setting.UIForeignBankAccount:
                        //    {
                        //        Value = glkBankAccount.EditValue.ToString();
                        //        break;
                        //    }
                        //case Setting.UITransMode:
                        //    {
                        //        Value = cboTransMode.SelectedIndex == 0 ? "1" : "2";
                        //        break;
                        //    }
                        case Setting.TDSEnabled:
                            {
                                Value = chkTDS.Checked == true ? "1" : "0";
                                break;
                            }
                        //case Setting.EnableBookingAtPayment:
                        //    {
                        //        Value = chkTDSBooking.Checked ? "1" : "0";
                        //        break;
                        // }
                        case Setting.PrintVoucher:
                            {
                                Value = chkVoucherPrint.Checked == true ? "1" : "0";
                                break;
                            }
                        case Setting.CustomizationForm:
                            {
                                Value = chkCustomizationform.Checked == true ? "1" : "0";
                                break;
                            }
                        //case Setting.EnableTransMode:
                        //    {
                        //        Value = chkEnableTransMode.Checked == true ? "1" : "0";
                        //        break;
                        //    }
                        case Setting.Location:
                            {
                                Value = (glkpLocation.EditValue != null) ? glkpLocation.Text : DefaultLocation.Primary.ToString();
                                break;
                            }
                        case Setting.DBUploadedOn:
                            {
                                Value = this.AppSetting.DBUploadedOn;
                                break;
                            }
                        case Setting.ThirdParty:
                            {
                                Value = this.AppSetting.ThirdPartyIntegration;
                                break;
                            }
                        case Setting.ProductVersion:
                            {
                                Value = this.AppSetting.AcmeerpVersionFromDB;
                                break;
                            }
                        case Setting.UpdaterDownloadBy:
                            {
                                Value = this.rgbDownloadBy.SelectedIndex.ToString();
                                break;
                            }
                        case Setting.ProxyUse:
                            {
                                Value = chkProxyUse.Checked == true ? "1" : "0";
                                if (chkProxyUse.Checked == false)
                                {
                                    txtProxyAddress.Text = string.Empty;
                                    txtProxyPort.Text = string.Empty;
                                    chkProxyAuthentication.Checked = false;
                                    txtProxyUName.Text = string.Empty;
                                    txtProxyPassword.Text = string.Empty;
                                }
                                break;
                            }
                        case Setting.ProxyAddress:
                            {
                                Value = txtProxyAddress.Text.Trim();
                                break;
                            }
                        case Setting.ProxyPort:
                            {
                                Value = txtProxyPort.Text.Trim();
                                break;
                            }
                        case Setting.ProxyAuthenticationUse:
                            {
                                Value = chkProxyAuthentication.Checked == true ? "1" : "0";
                                break;
                            }
                        case Setting.ProxyUserName:
                            {
                                Value = txtProxyUName.Text.Trim();
                                break;
                            }
                        case Setting.ProxyPassword:
                            {
                                Value = txtProxyPassword.Text.Trim();
                                break;
                            }
                        case Setting.PayrollPassword:
                            {
                                string PayrollLockPassword = CommonMethod.Encrept(txtPayrollPassword.Text);
                                Value = PayrollLockPassword;
                                break;
                            }
                        //case Setting.UIDonationVoucherPrint:
                        //    {
                        //        Value = rgDonationVoucherPrint.SelectedIndex == 0 ? "1" : "2";
                        //        break;
                        //    }
                    }
                    dtSetting.Rows[i][2] = Value;
                }
                this.AppSetting.SettingInfo = dtSetting.DefaultView;
                ISetting isetting = new GlobalSetting();
                isetting.ApplySetting();
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }
            // }
        }

        public void SaveUISetting(int UId = 0)
        {
            // if (ValidateSettingDetails())
            // {
            UserSetting setting = new UserSetting();
            DataView dvUISetting = null;

            dvUISetting = this.UtilityMember.EnumSet.GetEnumDataSource(setting, Sorting.Ascending);
            dtUISetting = dvUISetting.ToTable();
            if (dtUISetting != null)
            {
                dtUISetting.Columns.Add("Value", typeof(string));
                dtUISetting.Columns.Add("UserId", typeof(string));
                for (int i = 0; i < dtUISetting.Rows.Count; i++)
                {
                    UserSetting SettingName = (UserSetting)Enum.Parse(typeof(UserSetting), dtUISetting.Rows[i][1].ToString());
                    string Value = "";
                    switch (SettingName)
                    {
                        case UserSetting.UILanguage:
                            {
                                Value = glkpUILanguage.EditValue.ToString();
                                break;
                            }
                        case UserSetting.UIDateFormat:
                            {
                                Value = cboUIDateFormat.SelectedItem.ToString();
                                break;
                            }
                        case UserSetting.UIDateSeparator:
                            {
                                Value = cboUIDateSeparator.SelectedItem.ToString();
                                break;
                            }
                        case UserSetting.UIThemes:
                            {
                                Value = icboTheme.SelectedItem.ToString();
                                break;
                            }

                        case UserSetting.UIProjSelection:
                            {
                                Value = chkProjectSelection.Checked == true ? "1" : "0";
                                break;
                            }
                        case UserSetting.UITransClose:
                            {
                                Value = chkTransclose.Checked == true ? "1" : "0";
                                break;
                            }

                        //case UserSetting.TransEntryMethod:
                        //    {
                        //        Value = rgbTransEntryMode.Properties.Items[rgbTransEntryMode.SelectedIndex].Value.ToString();
                        //        break;
                        //    }
                        //case UserSetting.UIForeignBankAccount:
                        //    {
                        //        Value = glkBankAccount.EditValue.ToString();
                        //        break;
                        //    }
                        //case UserSetting.UITransMode:
                        //    {
                        //        Value = cboTransMode.SelectedIndex == 0 ? "1" : "2";
                        //        break;
                        //    }
                        //case UserSetting.UIDonationVoucherPrint:
                        //    {
                        //        Value = rgDonationVoucherPrint.SelectedIndex == 0 ? "1" : "2";
                        //        break;
                        //    }
                        //case UserSetting.UITDSEnabled:
                        //    {
                        //        Value = chkTDS.Checked == true ? "1" : "0";
                        //        break;
                        //    }
                        case UserSetting.PrintVoucher:
                            {
                                Value = chkVoucherPrint.Checked == true ? "1" : "0";
                                break;
                            }
                        case UserSetting.UpdaterDownloadBy:
                            {
                                Value = this.rgbDownloadBy.SelectedIndex.ToString();
                                break;
                            }
                        //case UserSetting.UIEnableTransMode:
                        //    {
                        //        Value = chkEnableTransMode.Checked == true ? "1" : "0";
                        //        break;
                        //    }
                    }
                    dtUISetting.Rows[i][2] = Value;
                    dtUISetting.Rows[i][3] = UId < 1 ? this.LoginUser.LoginUserId : UId.ToString();
                }
                this.AppSetting.UISettingInfo = dtUISetting.DefaultView;
                ISetting isetting = new UISetting();
                isetting.ApplySetting();

            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }
            // }
        }

        private string GetDigitGroupSizes()
        {
            int[] DigitGroupSizes = new int[3];
            if (cboDigitGrouping.SelectedIndex == 0)
            {
                DigitGroupSizes[0] = 3;
                DigitGroupSizes[1] = 2;
                DigitGroupSizes[2] = 2;
            }
            else if (cboDigitGrouping.SelectedIndex == 1)
            {
                DigitGroupSizes[0] = 3;
                DigitGroupSizes[1] = 3;
                DigitGroupSizes[2] = 3;
            }
            else
            {
                DigitGroupSizes[0] = 2;
                DigitGroupSizes[1] = 3;
                DigitGroupSizes[2] = 0;
            }
            string s = DigitGroupSizes[0].ToString() + "," + DigitGroupSizes[1].ToString() + "," + DigitGroupSizes[2].ToString();
            return s.Trim();
        }

        //private void LoadProjects()
        //{
        //    try
        //    {

        //        using (AccouingPeriodSystem accPeriodSystem = new AccouingPeriodSystem())
        //        {
        //            resultArgs = accPeriodSystem.FetchAccountingPeriodDetailsForSettings();
        //            resultArgs.DataSource.Table.Columns.Add("FLAG", typeof(int));
        //            gcAccountingPeriod.DataSource = SetAccountPeriodStatus(resultArgs.DataSource.Table);
        //            SetBookBeginingDate();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.ToString(), true);
        //    }
        //    finally { }
        //}

        //private void SetBookBeginingDate()
        //{
        //    if (string.IsNullOrEmpty(detBookbeginingFrom.Text.Trim()))
        //    {
        //        DataTable dtTransSource = (DataTable)gcAccountingPeriod.DataSource;
        //        if (dtTransSource.Rows.Count > 0)
        //        {
        //            detBookbeginingFrom.DateTime = this.UtilityMember.DateSet.ToDate(dtTransSource.Rows[0]["BOOKS_BEGINNING_FROM"].ToString(), true);
        //            detBookbeginingFrom.Properties.MinValue = this.UtilityMember.DateSet.ToDate(dtTransSource.Rows[0]["YEAR_FROM"].ToString(), true);
        //            detBookbeginingFrom.Properties.MaxValue = this.UtilityMember.DateSet.ToDate(dtTransSource.Rows[0]["YEAR_TO"].ToString(), true);
        //        }
        //    }
        //}

        //private DataTable SetAccountPeriodStatus(DataTable dtSource)
        //{
        //    DataTable dtAccSource = dtSource;
        //    for (int i = 0; i < dtAccSource.Rows.Count; i++)
        //    {
        //        if (dtAccSource.Rows[i]["STATUS"].ToString() == "Active")
        //        {
        //            dtAccSource.Rows[i]["FLAG"] = (int)YesNo.Yes;
        //        }
        //        else
        //        {
        //            dtAccSource.Rows[i]["FLAG"] = (int)YesNo.No;
        //        }
        //    }
        //    return dtAccSource;
        //}

        //private DataTable SetCurrentTransPeriod(string transPeriodId, string status)
        //{
        //    DataTable dtAccSource = (DataTable)gcAccountingPeriod.DataSource;
        //    for (int i = 0; i < dtAccSource.Rows.Count; i++)
        //    {
        //        if (dtAccSource.Rows[i]["ACC_YEAR_ID"].ToString() == transPeriodId)
        //        {
        //            dtAccSource.Rows[i]["FLAG"] = status;
        //        }
        //        else
        //        {
        //            dtAccSource.Rows[i]["FLAG"] = (int)YesNo.No;
        //        }
        //    }
        //    return dtAccSource;
        //}

        //private bool IsTransactionPeriodValid()
        //{
        //    bool isValid = true;
        //    DataTable dtSource = (DataTable)gcAccountingPeriod.DataSource;
        //    if (dtSource.Rows.Count <= 0)
        //    {
        //        isValid = false;
        //    }
        //    else
        //    {
        //        using (AccouingPeriodSystem accountingSystem = new AccouingPeriodSystem())
        //        {
        //            for (int i = 0; i < dtSource.Rows.Count; i++)
        //            {
        //                if (this.UtilityMember.NumberSet.ToInteger(dtSource.Rows[i]["FLAG"].ToString()) == (int)YesNo.Yes)
        //                {
        //                    AccperiodYearId = this.UtilityMember.NumberSet.ToInteger(dtSource.Rows[i]["ACC_YEAR_ID"].ToString());
        //                    yearFrom = dtSource.Rows[i]["YEAR_FROM"].ToString();
        //                    YearTo = dtSource.Rows[i]["YEAR_TO"].ToString();
        //                    BooksBeginningFrom = dtSource.Rows[i]["BOOKS_BEGINNING_FROM"].ToString();
        //                    isValid = true;
        //                }
        //            }
        //        }
        //    }
        //    return isValid;
        //}

        //private void UpdateTransacationPeriod()
        //{
        //    using (GlobalSetting globalSystem = new GlobalSetting())
        //    {
        //        resultArgs = globalSystem.UpdateAccountingPeriod(AccperiodYearId.ToString());
        //        if (resultArgs.Success)
        //        {
        //            using (AccouingPeriodSystem accountingSystem = new AccouingPeriodSystem())
        //            {
        //                resultArgs = accountingSystem.FetchActiveTransactionPeriod();
        //                this.AppSetting.AccPeriodInfo = resultArgs.DataSource.Table.DefaultView;
        //            }
        //        }
        //    }
        //}

        private DataTable ConstructThemes()
        {
            DataTable dtThemes = new DataTable();
            dtThemes.Columns.Add("Id", typeof(int));
            dtThemes.Columns.Add("Themes", typeof(string));
            dtThemes.Rows.Add(1, "DevExpress Style");
            dtThemes.Rows.Add(2, "VS2010");
            dtThemes.Rows.Add(3, "Office 2010 Blue");
            dtThemes.Rows.Add(4, "Office 2010 Silver");
            dtThemes.Rows.Add(5, "Office 2013");
            return dtThemes;
        }

        //private void LoadBankAccount()
        //{

        //    try
        //    {
        //        using (BankSystem bankSystem = new BankSystem())
        //        {
        //            resultArgs = bankSystem.FetchBankDetailsforSettings();
        //            glkBankAccount.Properties.DataSource = null;
        //            if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
        //            {
        //                this.UtilityMember.ComboSet.BindGridLookUpCombo(glkBankAccount, resultArgs.DataSource.Table, bankSystem.AppSchema.Bank.BANKColumn.ColumnName, bankSystem.AppSchema.BankAccount.LEDGER_IDColumn.ColumnName);
        //                glkBankAccount.EditValue = glkBankAccount.Properties.GetKeyValue(0);
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        MessageRender.ShowMessage(Ex.Message);
        //    }
        //    finally { }

        //}

        private void LoadPositivePreview()
        {
            string TextDftValue = FormattedDigitGrouping();
            string FormattedPositiveText = string.Empty;
            string FormattedNegativeText = string.Empty;
            txtPositivePreview.Text = string.Empty;
            string PreCountrySymbol = lblCurrencySymbol.Text;
            string PreCurrencyPosition = cboCurrencyPosition.Text;

            if (PreCurrencyPosition == CurrencyPosition.Before.ToString())
            {
                FormattedPositiveText += PreCountrySymbol + " " + TextDftValue;
                if (cboNegativeSign.SelectedIndex == 0)
                {
                    FormattedNegativeText += "( " + FormattedPositiveText + " )";
                }
                else if (cboNegativeSign.SelectedIndex == 1)
                {
                    FormattedNegativeText += "- " + FormattedPositiveText;
                }
            }
            if (PreCurrencyPosition == CurrencyPosition.After.ToString())
            {
                FormattedPositiveText += TextDftValue + " " + PreCountrySymbol;
                if (cboNegativeSign.SelectedIndex == 0)
                {
                    FormattedNegativeText += "( " + FormattedPositiveText + " )";
                }
                else if (cboNegativeSign.SelectedIndex == 1)
                {
                    FormattedNegativeText += "- " + FormattedPositiveText;
                }
            }

            txtPositivePreview.Text = FormattedPositiveText;
            txtNegativePreview.Text = FormattedNegativeText;
        }
        public string FormattedDigitGrouping()
        {
            string FVal = string.Empty;
            if (cboDigitGrouping.SelectedIndex == 0 && cboGroupingSeparator.SelectedIndex == 0)
            {
                FVal = "12,34,56,789";
            }
            else if (cboDigitGrouping.SelectedIndex == 0 && cboGroupingSeparator.SelectedIndex == 1)
            {
                FVal = "12-34-56-789";
            }
            else if (cboDigitGrouping.SelectedIndex == 1 && cboGroupingSeparator.SelectedIndex == 0)
            {
                FVal = "123,456,789";
            }
            else if (cboDigitGrouping.SelectedIndex == 1 && cboGroupingSeparator.SelectedIndex == 1)
            {
                FVal = "123-456-789";
            }
            else
            {
                FVal = "123456789";
            }
            FVal = FVal + cboDecimalSeparator.Text + FormatDecimalPlaces();
            return FVal;
        }
        public string FormatDecimalPlaces()
        {
            string FVal = string.Empty;
            int DecPla = this.UtilityMember.NumberSet.ToInteger(cboDecimalPlaces.Text);
            for (int i = 1; i <= DecPla; i++)
            {
                FVal += "0";
            }
            return FVal;
        }
        public void DatePreview()
        {
            txtDatePreview.Text = string.Empty;
            if (cboUIDateFormat.SelectedIndex == 0)
            {
                txtDatePreview.Text = "13/05/2014";
            }
            else if (cboUIDateFormat.SelectedIndex == 1)
            {
                txtDatePreview.Text = "2014/05/13";
            }
            else if (cboUIDateFormat.SelectedIndex == 2)
            {
                txtDatePreview.Text = "13/05/14";
            }
            else if (cboUIDateFormat.SelectedIndex == 3)
            {
                txtDatePreview.Text = "05/13/14";
            }
            if (cboUIDateSeparator.SelectedIndex == 0)
            {
                txtDatePreview.Text = txtDatePreview.Text.Replace('-', '/');
            }
            else
            {
                txtDatePreview.Text = txtDatePreview.Text.Replace('/', '-');
            }
        }

        /// <summary>
        /// On 22/10/2021, To show Branch Dataabse details
        /// </summary>
        private void ShowBranchDatabaseDetails()
        {
            //22/10/2021, To show active connected database if multi db feature enabled
            lblDatabaseServer.Text = string.Empty;
            lblDatabaseName.Text = string.Empty;
            lblBranchAliasName.Text = string.Empty;
            lblLicenseFileName.Text = string.Empty;
            lblResotedOn.Text = string.Empty;

            string port = GetAppConfigDBServerPort();
            lblDatabaseServer.Text = GetAppConfigDBServerName() + (String.IsNullOrEmpty(port) ? "" : " Port:" + port);
            if (!String.IsNullOrEmpty(SettingProperty.ActiveDatabaseName) && (this.AppSetting.AccesstoMultiDB == (int)YesNo.Yes))
            {
                lblDatabaseName.Text = SettingProperty.ActiveDatabaseName;
                lblBranchAliasName.Text = SettingProperty.ActiveDatabaseAliasName;
            }
            else
            {
                lblDatabaseName.Text = GetAppConfigDBName();
                lblBranchAliasName.Text = lblDatabaseName.Text;
            }

            //On 25/11/2024, To Show latest restored on ----------------------------------------------------------------------
            lblResotedOn.Text = string.IsNullOrEmpty(this.AppSetting.DBRestoredOn) ? string.Empty : this.AppSetting.DBRestoredOn.Trim();
            if (!string.IsNullOrEmpty(this.AppSetting.DBRestoredRemarks))
            {
                lblResotedOn.Text += "\n\r" + this.AppSetting.DBRestoredRemarks.Replace("--", " Voucher(s) replaced by ");
            }
            //----------------------------------------------------------------------------------------------------------------    

            //On 16/12/2024 - to show acmeerp portal service url -------------------------------------------------------------
            lblAcmeerpPortalService.Text = string.Empty;
            DataSyncService.DataSynchronizerClient dataClient = new DataSyncService.DataSynchronizerClient();
            if (dataClient.Endpoint.Address != null)
            {
                lblAcmeerpPortalService.Text = dataClient.Endpoint.Address.Uri.Host.ToString();
            }
            //-----------------------------------------------------------------------------------------------------------------

            if (File.Exists(SettingProperty.ActiveDatabaseLicenseKeypath))
            {
                lblLicenseFileName.Text = Path.GetFileName(SettingProperty.ActiveDatabaseLicenseKeypath);
            }
        }

        /// <summary>
        /// On 24/01/2024, to diagnose details
        /// </summary>
        private void LoadDiagnoseDetail(bool optimizeMainTables = false)
        {
            memDiagnoseDetail.Text = string.Empty;
            memDiagnoseDetail.Properties.ReadOnly = true;
            using (SettingSystem setting = new SettingSystem())
            {
                ResultArgs result = setting.ProcessAcmeerpDiagnoseDetails(optimizeMainTables);
                if (!result.Success)
                {
                    memDiagnoseDetail.Text = result.Message;
                }
                else
                {
                    memDiagnoseDetail.Text = "No observation found.";
                }
            }
        }
        #endregion

        #region User Rights
        private void ApplyRights()
        {
            DataTable dtSettings = new DataTable();
            try
            {
                dtSettings = CommonMethod.ApplyUserRightsForForms((int)Menus.MasterSetting);
                if (dtSettings != null && dtSettings.Rows.Count != 0)
                {
                    foreach (DataRow dr in dtSettings.Rows)
                    {
                        if ((int)Forms.GlobalSettings == this.UtilityMember.NumberSet.ToInteger(dr["ACTIVITY_ID"].ToString()))
                        {
                            tcLocalization.TabPages[0].PageVisible = true;
                        }
                        else if ((int)Forms.UISettings == this.UtilityMember.NumberSet.ToInteger(dr["ACTIVITY_ID"].ToString()))
                        {
                            tcLocalization.TabPages[1].PageVisible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        #endregion

        private void chkTransclose_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkProjectSelection_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cboCurrencyPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPositivePreview();
        }

        private void cboDigitGrouping_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPositivePreview();
        }

        private void cboNegativeSign_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPositivePreview();
        }

        private void cboGroupingSeparator_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPositivePreview();
        }

        private void cboDecimalSeparator_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPositivePreview();
        }

        private void cboDecimalPlaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPositivePreview();
        }

        private void cboUIDateFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatePreview();
        }

        private void cboUIDateSeparator_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatePreview();
        }

        private void chkUseProxy_CheckedChanged(object sender, EventArgs e)
        {
            lcGrpProxyServer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (chkProxyUse.Checked)
            {
                lcGrpProxyServer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtProxyAddress.Focus();
            }
        }

        private void chkProxyAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            lcGrpProxyAuthentication.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (chkProxyAuthentication.Checked)
            {
                lcGrpProxyAuthentication.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtProxyUName.Focus();
            }
        }

        private void tcLocalization_Click(object sender, EventArgs e)
        {
            if (tcLocalization.SelectedTabPage == tpProxyServer)
            {
                this.ShowWaitDialog();
                LoadDiagnoseDetail(false);
                this.CloseWaitDialog();
            }
        }

        private void picDiagnose_Click(object sender, EventArgs e)
        {
            this.ShowWaitDialog();
            LoadDiagnoseDetail(true);
            this.CloseWaitDialog();
        }

        private void btnRestoreEmptyDB_Click(object sender, EventArgs e)
        {
            string RestoreDBName = lblDatabaseName.Text;
            string Message = "Are you sure to Restore/Overwrite current Database '" + RestoreDBName + "' with Acmeerp emtpy Database ?";

            if (!string.IsNullOrEmpty(RestoreDBName))
            {
                if (this.ShowConfirmationMessage(Message, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    this.ShowWaitDialog("Restoring empty Database");
                    using (BackupAndRestore restore = new BackupAndRestore())
                    {
                        ResultArgs result = restore.RestoreEmptyDatabase();
                        this.Cursor = Cursors.Default;
                        if (result.Success)
                        {
                            this.ShowMessageBox("Acmeerp empty Database is restored successfully.");
                            Bosco.Utility.ConfigSetting.SettingProperty.Is_Application_Logout = true;
                            Application.Restart();
                        }
                        else
                        {
                            this.ShowMessageBox(result.Message);
                        }
                    }
                    this.CloseWaitDialog();
                }
            }
            else
            {
                this.ShowMessageBox("There is no Active Acmeerp Database");
            }
        }

        private void frmSettings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                string refcode = string.Empty;
                DialogResult diaresult = InputBox("Acme.erp Configuration", "Enter password to enable Acme.erp configuration options", ref refcode, true);
                if (diaresult == System.Windows.Forms.DialogResult.OK)
                {
                    if (refcode == SettingProperty.AcmeerpHiddenOperationPassword)
                    {
                        lcRestoreEmptyDB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        lcRestoreEmptySpace.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        if (AppSetting.AccesstoMultiDB == 1)
                        {
                            lcConfigureMutlDBXML.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            lcConfigureAllMutlDBXML.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            lcMultiDBEmptySpace.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        }
                    }
                    else
                    {
                        this.ShowMessageBoxWarning("Invalid password!");
                    }
                }
            }
        }

        private void btnConfigureAllMutlDBXML_Click(object sender, EventArgs e)
        {
            if (AppSetting.AccesstoMultiDB == 1)
            {
                string rtn = "Are you sure to generate Multi Database XML with all the avilable Database(s) in Acme.erp?";
                if (MessageRender.ShowConfirmationMessage(rtn, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    using (BackupAndRestore restore = new BackupAndRestore())
                    {
                        restore.GenerateMultiDBXMLFromServer();
                    }
                }
            }
        }

        private void btnConfigureMutlDBXML_Click(object sender, EventArgs e)
        {
            if (AppSetting.AccesstoMultiDB == 1)
            {
                string rtn = "Are you sure to update Multi Database XML from backup ?";
                if (MessageRender.ShowConfirmationMessage(rtn, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {

                    using (SettingSystem settingsys = new SettingSystem())
                    {
                        settingsys.UpdateMultiDBXMLConfigurationFromBackup();
                    }
                }
            }
        }

        /// <summary>
        /// This is to merge the Default Ledgers which is given by Mr.Antony
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMergeUnwantedLedgers_Click(object sender, EventArgs e)
        {
            string[,] ledgerMapping = new string[,]
            {
                { "Purchase of Religious Articles", "Chapel Expenses", "CR" },
                { "Canteen Income", "Rental Income", "DR" },
                { "Miscellaneous Fees(New1)", "Miscellaneous Fees","CR" },
                { "Miscellaneous Income", "Miscellaneous Fees","CR"},
                { "Calendar & Hand Book", "Printing & Xerox" ,"CR"},
                { "Canteen Expenses", "Repairs & Maintenance" ,"CR"},
                { "Charity & Donations", "Relief of Poor" ,"CR"},
                { "Educational expenses of Salesians", "Educational expenses","CR" },
                { "Electricity Bill", "Electricity" ,"CR"},
                { "Establishment Expenses", "Repairs & Maintenance","CR" },
                { "Fees paid to University", "University Fee Expenses" ,"CR"},
                { "Fuel/Gas and Firwood", "Gas and Firwood" ,"CR"},
                { "Function & Celebrations(New)", "Function & Celebrations","CR" },
                { "Garden Expenses", "Repairs & Maintenance" ,"CR"},
                { "Printing & Xerox(New)", "Printing & Xerox" ,"CR"},
                { "Printing & Xerox(New1)", "Printing & Xerox" ,"CR"},
                { "Programmes and Events", "Retreat & Seminar Income" ,"CR"},
                { "Remuneration paid for service", "Honorarium / Stipends / Incentives" ,"CR"},
                { "Repairs & Renovation", "Repairs & Renovations" ,"CR"},
                { "University Expenses", "University Fee Expenses" ,"CR"},
                { "Web Hosting Expenses", "Repairs & Maintenance" ,"CR"},
                { "Electrical Fittings", "Repairs & Maintenance" ,"CR"},
                { "ESI Deducted / Remitted", "ESI Recovered / Remitted - Employee Contribution" ,"CR"},
                { "Scholarship Disbursed", "Scholarship Received / Disbursed" ,"CR"},
                { "Scholarship Received", "Scholarship Received / Disbursed" ,"DR"},
                {"", "",""}
                };

            DataTable dtSource = ConvertArrayToDataTableWithIds(ledgerMapping);

            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                using (MappingSystem mergeMapping = new MappingSystem())
                {
                    dtSource.DefaultView.RowFilter = "LEDGER_ID<>0 AND MERGE_LEDGER_ID <>0";

                    mergeMapping.dtMergeLedgers = dtSource.DefaultView.ToTable();
                    if (mergeMapping.dtMergeLedgers != null && mergeMapping.dtMergeLedgers.Rows.Count > 0)
                    {
                        resultArgs = mergeMapping.MergeLedgersNew();
                        if (resultArgs.Success)
                        {
                            ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.Mapping.MERGED_SUCCESSFULLY));

                            using (AcMEDSync.Model.BalanceSystem balancesystem = new AcMEDSync.Model.BalanceSystem())
                            {
                                this.ShowWaitDialog(this.GetMessage(MessageCatalog.Master.Mapping.UPDATING_BALANCE));
                                balancesystem.VoucherDate = AppSetting.BookBeginFrom;
                                balancesystem.UpdateBulkTransBalance();
                                this.CloseWaitDialog();
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessageBox("There are no records available for merging, given old ledgers already merged!.");
                    }
                }
            }
        }

        static DataTable ConvertArrayToDataTableWithIds(string[,] array)
        {
            DataTable table = new DataTable();

            table.Columns.Add("LEDGER_ID", typeof(long));
            table.Columns.Add("OLD_LEDGER_NAME", typeof(string));
            table.Columns.Add("MERGE_LEDGER_ID", typeof(long));
            table.Columns.Add("NEW_LEDGER_NAME", typeof(string));
            table.Columns.Add("TRANS_MODE", typeof(string));
            table.Columns.Add("IS_COST_CENTER", typeof(int));
            table.Columns.Add("IS_TDS_LEDGER", typeof(int));

            for (int i = 0; i < array.GetLength(0); i++)
            {
                string old_ledger_name = array[i, 0];
                string new_ledger_name = array[i, 1];
                string Trans_mode = array[i, 2];
                if (!string.IsNullOrEmpty(old_ledger_name) && !string.IsNullOrEmpty(new_ledger_name))
                {
                    using (LedgerSystem ledgersystem = new LedgerSystem())
                    {
                        ResultArgs resultold = ledgersystem.FetchLedgerIdByLedgerName(old_ledger_name);
                        int oldLedgerId = resultold.DataSource.Sclar.ToInteger;
                        ResultArgs resultnew = ledgersystem.FetchLedgerIdByLedgerName(new_ledger_name);
                        int newLedgerId = resultnew.DataSource.Sclar.ToInteger;

                        table.Rows.Add(oldLedgerId, array[i, 0], newLedgerId, array[i, 1], Trans_mode, 0, 0);
                    }
                }
            }
            return table;
        }
    }
}