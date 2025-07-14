using System;
using System.Windows.Forms;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using AcMEDSync.Model;
using DevExpress.XtraLayout.Utils;
using Bosco.Model.Dsync;
using Bosco.Model.Transaction;
using Bosco.Model.Setting;
using System.Data;
using Bosco.Model;
using System.IO;
using Bosco.Model.UIModel;

namespace ACPP.Modules.Dsync
{
    public partial class frmImportSplitedProject : frmFinanceBaseAdd
    {
        #region Variables
        ResultArgs resultargs = new ResultArgs();
        SettingProperty setting = new SettingProperty();
        frmMain AppMainFrm = null;
        bool isBulkImport = false;
        string[] SplitFYFiles;
        #endregion

        #region Properties

        private ImportType XML_ImportType
        {
            get;
            set;
        }

        private string FileName { get; set; }

        private string XMLDateFrom
        {
            get;
            set;
        }

        private string XMLDateTo
        {
            get;
            set;
        }

        private bool XML_IsFYSplit
        {
            get;
            set;
        }

        private string XML_ProjectName
        {
            get;
            set;
        }

        private Int32 XML_ProjectId
        {
            get;
            set;
        }

        private bool XML_ProjectVoucherExitsInDB
        {
            get;
            set;
        }

        private bool XML_ProjectFDVoucherExitsInDB
        {
            get;
            set;
        }

        private bool FYProjectFDVoucherExistsInXML
        {
            get;
            set;
        }

        private DateTime FYProjectFDVoucherFirstDateInXML
        {
            get;
            set;
        }

        private bool FYProjectBudgetExistsInXML
        {
            get;
            set;
        }

        private DateTime FYProjectFirstVoucherDateInDB
        {
            get;
            set;
        }

        private DateTime FYProjectLastVoucherDateInDB
        {
            get;
            set;
        }
                

        private bool MismatchBudgetProjectsWithDBProjects
        {
            get;
            set;
        }

        private Int32 NoOfBudgetProjects
        {
            get;
            set;
        }


        private bool IsGSTEnabled
        {
            get;
            set;
        }

        private bool IsGSTVendorDetailsEnabled
        {
            get;
            set;
        }

        private DataTable XML_Ledgers
        {
            get;
            set;
        }

        private DataTable MappedLedgers
        {
            get;
            set;
        }

        private bool XML_IsShowMappingLedger
        {
            get;
            set;
        }

        //private bool IsLicenceDetailsValid
        //{
        //    get
        //    {
        //        lcMergeProject.Visibility = LayoutVisibility.Always;
        //        string datefrom = string.Empty;
        //        string dateto = string.Empty;
        //        string projectname = string.Empty;
        //        bool IsValid = true;
        //        this.Height = 235;

        //        XML_IsFYSplit = false;
        //        XMLDateFrom = string.Empty;
        //        XMLDateTo = string.Empty;
        //        XML_ProjectName = string.Empty;
        //        XML_ProjectId = 0;
        //        XML_ProjectVoucherExitsInDB = false;
        //        XML_ProjectFDVoucherExitsInDB = false;
        //        FYProjectFirstVoucherDateInDB = DateTime.MinValue;
        //        FYProjectLastVoucherDateInDB = DateTime.MinValue;
        //        FYProjectFDVoucherExistsInXML = false;

        //        using (ImportVoucherSystem importVoucher = new ImportVoucherSystem())
        //        {
        //            lcCapHO.Visibility = lcHO.Visibility = LayoutVisibility.Always;
        //            lcCapBO.Visibility = lcBO.Visibility = LayoutVisibility.Always;
        //            lcCapSplitFY.Visibility = lcSplitFY.Visibility = LayoutVisibility.Always;
        //            lcDateRange.Visibility = lcProjectName.Visibility = LayoutVisibility.Always;
        //            lcWithFDVouchers.Visibility = lcIncludeOPBalance.Visibility = LayoutVisibility.Always;

        //            lblDateRange.Text = string.Empty;
        //            lblProjectName.Text = string.Empty;
        //            lblSplitFYOption.Text = " No";
        //            lblFDVoucherExisting.Text =string.Empty;

        //            //On 14/04/2021
        //            CommonMethod.BranchOfficeCode = string.Empty;
        //            CommonMethod.HeadOfficeCode = string.Empty;

        //            importVoucher.MergeProject = glkpMergeProject.EditValue != null ? glkpMergeProject.Text : string.Empty;
        //            importVoucher.MergerProjectId = glkpMergeProject.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkpMergeProject.EditValue.ToString()) : 0;

        //            importVoucher.InitializeLicenceDetails(FileName);
        //            lblBOCode.Text = CommonMethod.BranchOfficeCode.ToUpper();
        //            lblHOCode.Text = CommonMethod.HeadOfficeCode.ToUpper();
        //            if (!lblBOCode.Text.ToLower().Equals(SettingProperty.branachOfficeCode.ToLower()))
        //            {
        //                if (ShowConfirmationMessage(GetMessage(MessageCatalog.DataSynchronization.Import.BRANCH_CODE_MATCH_ERROR), MessageBoxButtons.YesNo, MessageBoxIcon.Warning).Equals(DialogResult.No))
        //                {
        //                    IsValid = false;
        //                }
        //            }
        //            else if (!lblHOCode.Text.ToLower().Equals(SettingProperty.headofficecode.ToLower()))
        //            {
        //                if (ShowConfirmationMessage(GetMessage(MessageCatalog.DataSynchronization.Import.HEAD_OFFICE_CODE_MATCH_ERROR), MessageBoxButtons.YesNo, MessageBoxIcon.Warning).Equals(DialogResult.No))
        //                {
        //                    IsValid = false;
        //                }
        //            }

        //            lblSplitFYOption.Text = importVoucher.IsFYSplit ? " Yes" : " No";
        //            datefrom = importVoucher.FYDateFrom.ToShortDateString();
        //            dateto = importVoucher.FYDateTo.ToShortDateString();
        //            projectname = importVoucher.FYProjectName;
        //            if (datefrom == DateTime.MinValue.ToShortDateString() || dateto == DateTime.MinValue.ToShortDateString() || String.IsNullOrEmpty(projectname))
        //            {
        //                this.ShowMessageBox("Invalid Project Data XML file.");
        //                IsValid = false;
        //            }

        //            lcOverwrite.Visibility = LayoutVisibility.Always;
        //            this.Height = 235;
        //            if (importVoucher.IsFYSplit) //For Split FY
        //            {
        //                chkCanOverride.Checked = false;
        //                lcOverwrite.Visibility = LayoutVisibility.Never;
        //                chkWithFDVouchers.Checked = chkIncludeOPBalance.Checked = true;
        //                chkWithFDVouchers.Enabled = chkIncludeOPBalance.Enabled = false;
        //                chkWithFDVouchers.Font = chkIncludeOPBalance.Font = new System.Drawing.Font(chkIncludeOPBalance.Font.FontFamily, chkIncludeOPBalance.Font.Size, System.Drawing.FontStyle.Bold);

        //                //Hide Merge project, if split FY option is enabled
        //                glkpMergeProject.EditValue = 0;
        //                lcMergeProject.Visibility = LayoutVisibility.Never;
        //                this.Height = 215;
        //            }

        //            //Assign XML properties into local variables
        //            XML_IsFYSplit = importVoucher.IsFYSplit;
        //            XMLDateFrom = datefrom;
        //            XMLDateTo = dateto;
        //            XML_ProjectId = importVoucher.FYProjectId;
        //            XML_ProjectName = projectname;
        //            XML_ProjectVoucherExitsInDB = importVoucher.FYProjectVoucherExistsInDB;
        //            XML_ProjectFDVoucherExitsInDB = importVoucher.FYProjectFDVoucherExistsInDB;
        //            FYProjectFirstVoucherDateInDB = importVoucher.FYProjectFirstVoucherDateInDB;
        //            FYProjectLastVoucherDateInDB = importVoucher.FYProjectLastVoucherDateInDB;
        //            FYProjectFDVoucherExistsInXML= importVoucher.FYProjectFDVoucherExistsInXML;

        //            this.CenterToScreen();
        //        }

        //        lblDateRange.Text = string.Empty;
        //        lblProjectName.Text = string.Empty;
        //        lblFDVoucherExisting.Text = string.Empty;
        //        if (!string.IsNullOrEmpty(datefrom) && !string.IsNullOrEmpty(dateto))
        //        {
        //            lblDateRange.Text = "From     : <b>" + datefrom + "</b>  To : <b>" + dateto + "</b>";
        //        }

        //        if (!string.IsNullOrEmpty(projectname))
        //        {
        //            lblProjectName.Text = "Project : <b>" + projectname + "</b>";
        //        }

        //        lblFDVoucherExisting.Text = "FD Vouchers : <b>" + (FYProjectFDVoucherExistsInXML?"Yes":"No") + "</b>";

        //        //this.GenerateFYPeriods(UtilityMember.DateSet.ToDate(XMLDateTo, false));
        //        if (!IsValid)
        //        {
        //            txtPath.Select();
        //            txtPath.Focus();
        //        }
        //        return IsValid;
        //    }
        //}

        #endregion

        #region Constructor
        public frmImportSplitedProject(frmMain appmainfrm = null, bool isbulkImport = false)
        {
            InitializeComponent();
            if (appmainfrm != null)
            {
                AppMainFrm = appmainfrm;
            }
            isBulkImport = isbulkImport;
        }
        #endregion

        #region Events
        private void frmImportSplitedProject_Load(object sender, EventArgs e)
        {
            if (isBulkImport)
            {
                this.Text = this.Text + " - Split Finance Year";
            }

            InitalImport();
        }

        private void btnLoadXMLFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (isBulkImport)
                {
                    AcMELog.WriteLog("Welcome to Acme.erp Import Splited Project...");
                    string folderpath = string.Empty;
                    FolderBrowserDialog folderPath = new FolderBrowserDialog();
                    folderPath.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
                    if (folderPath.ShowDialog() == DialogResult.OK)
                    {
                        folderpath = folderPath.SelectedPath;
                    }

                    if (!string.IsNullOrEmpty(folderpath))
                    {
                        SplitFYFiles = Directory.GetFiles(folderpath, "*.xml", SearchOption.AllDirectories);
                        if (SplitFYFiles.Length > 0)
                        {
                            FileName = SplitFYFiles.GetValue(0).ToString();
                            txtPath.Text = FileName;
                            this.Height = 250;
                            InitXMLSelection();
                        }
                        else
                        {
                            txtPath.Text = string.Empty;
                            InitalImport();
                        }
                    }
                }
                else
                {
                    AcMELog.WriteLog("Welcome to Acme.erp Import Splited Project...");
                    OpenFileDialog opendialog = new OpenFileDialog();
                    opendialog.Filter = "XML Files (.xml)|*.xml";
                    if (opendialog.ShowDialog() == DialogResult.OK)
                    {
                        FileName = opendialog.FileName;
                        txtPath.Text = FileName;
                        InitXMLSelection();
                    }
                }
            }
            catch (ArgumentException errmissing)
            {
                this.ShowMessageBoxWarning("Invalid Acmeerp Project Voucher Data XML file.");
            }
            catch (Exception ex)
            {
                this.ShowMessageBoxWarning("Invalid Acmeerp Project Voucher Data XML file.");
                AcMELog.WriteLog(ex.Message);
            }
            finally { }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            bool processed = false;
            if (isBulkImport)
            {

                if (IsInputValid())
                {

                    if (this.ShowConfirmationMessage("This XML is Finance Year splitting option enabled, Fixed Deposit Vouchers (Investments, Openings and Renewals) will be rearranged based on Books Beginning From.",
                          MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                    {
                        foreach (string file in SplitFYFiles)
                        {
                            Application.DoEvents();
                            FileName = file;
                            txtPath.Text = FileName;
                            Application.DoEvents();
                            InitXMLSelection();
                            Application.DoEvents();
                            if (IsInputValid())
                            {
                                ShowWaitDialog();
                                resultargs = ImportProject();
                                processed = true;
                                this.CloseWaitDialog();
                            }
                            else
                            {
                                break;
                            }
                        }

                        if (resultargs != null && resultargs.Success)
                        {
                            ShowWaitDialog("Balance Refreshing all the Projects");
                            resultargs = RefereshBalance(0);
                            this.CloseWaitDialog();
                        }
                    }
                }
            }
            else
            {
                if (IsInputValid())
                {
                    //On 28/05/2021, to have overwirte message between the duriation
                    string overwritemsg = "Overwritting will remove all the old records for the duration between '" + XMLDateFrom + "' and '" + XMLDateTo + "'. Do you really want to overwrite ?";
                    //GetMessage(MessageCatalog.DataSynchronization.Import.OVERWRITE_CONFORM)
                    if (chkCanOverride.Checked ? ShowConfirmationMessage(overwritemsg, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning).Equals(DialogResult.OK) :
                        ShowConfirmationMessage(GetMessage(MessageCatalog.DataSynchronization.Import.APPEND_CONFORM), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning).Equals(DialogResult.OK))
                    {
                        ShowWaitDialog();
                        resultargs = ImportProject();
                        processed = true;
                        this.CloseWaitDialog();
                    }
                }
            }

            if (processed)
            {
                if (resultargs != null && resultargs.Success)
                {
                    string msg = GetMessage(MessageCatalog.DataSynchronization.Import.IMPORT_SUCCESSFULLY);
                    if (isBulkImport) msg = "Project(s) have been imported successfully";
                    resultargs = GenerateNewFYPeriods(); //06/05/2021 to generate FYs
                    UpdateFinanceSetting(); //11/12/2018, to set finance setting bsed on imported project
                    if (resultargs.Success)
                    {
                        ShowMessageBox(msg);
                        this.Close();
                    }
                    else
                    {
                        msg += ", Could not create Accounting Finance Year(s) (" + resultargs.Message + ")";
                        ShowMessageBox(msg);
                    }
                }
                else if (resultargs != null && !string.IsNullOrEmpty(resultargs.Message))
                {
                    ShowMessageBox(resultargs.Message);
                }
            }
            else
            {
                this.CloseWaitDialog();
                txtPath.Select();
                txtPath.Focus();
            }

        }

        private ResultArgs ImportProject()
        {
            using (ImportVoucherSystem importVoucher = new ImportVoucherSystem())
            {
                importVoucher.CanOverride = chkCanOverride.Checked;
                importVoucher.MergeProject = glkpMergeProject.EditValue != null ? glkpMergeProject.Text : string.Empty;
                importVoucher.MergerProjectId = glkpMergeProject.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkpMergeProject.EditValue.ToString()) : 0;

                //On 03/05/2021, To set FD and opening Propertis
                importVoucher.With_FD_Vouchers = chkWithFDVouchers.Checked;
                //importVoucher.With_budget = chkWithFDVouchers.Checked;
                importVoucher.Include_AL_LedgerOpeningBalance = chkIncludeOPBalance.Checked;
                importVoucher.With_Budget = chkIncludeBudget.Checked;

                //On 04/01/2022, to assing show mappping ledger scren
                importVoucher.IsShowMappingLedgers = XML_IsShowMappingLedger;
                importVoucher.MappedLedgers = MappedLedgers;

                //On 30/11/2022, To set Cost Centre Mapping
                importVoucher.CostCentreMapping = this.AppSetting.CostCeterMapping;

                resultargs = importVoucher.ImportVouchers(FileName);
                if (resultargs.Success)
                {
                    //On 03/07/2020, it was all the Projects, now changed to refresh only imported project alone, 
                    if (!isBulkImport)
                    {
                        Int32 refreshprojectid = importVoucher.MergerProjectId > 0 ? importVoucher.MergerProjectId : importVoucher.ProjectId;
                        resultargs = RefereshBalance(refreshprojectid);
                    }

                    //06/02/2024, To update Finance Setting
                    if (importVoucher.dtFinanceSettings != null && importVoucher.dtFinanceSettings.Rows.Count > 0)
                    {
                         using (UISetting uisetting = new UISetting())
                         {
                             resultargs = uisetting.SaveSettingFromProjectImport(importVoucher.dtFinanceSettings);
                         }
                    }

                }
                else
                {
                    //22/02/2018, When import voucher/import project, if error occured, we need to show which data occurs error,
                    //for exmaple, name of the project, name of the master, date of voucher etc.
                    resultargs.Message += "\n\n" + importVoucher.ProblemDataDetails.Replace("<br>", "\n");
                }
            }
            return resultargs;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void glkpMergeProject_EditValueChanged(object sender, EventArgs e)
        {
            InitXMLSelection();
        }
        #endregion

        #region Methods


        /// <summary>
        /// On 26/05/2021, Check license key only when xml file selection
        /// </summary>
        /// <returns></returns>
        private bool ValidateLicenseKey()
        {
            bool IsValid = true;
            try
            {
                bool validXML = !string.IsNullOrEmpty(lblBOCode.Text) && !string.IsNullOrEmpty(lblHOCode.Text)
                     && !string.IsNullOrEmpty(XMLDateFrom) && !string.IsNullOrEmpty(XMLDateTo) && !string.IsNullOrEmpty(XML_ProjectName);

                if (validXML)
                {
                    if (!isBulkImport)
                    {
                        if (!lblBOCode.Text.ToLower().Equals(SettingProperty.branachOfficeCode.ToLower()))
                        {
                            if (ShowConfirmationMessage(GetMessage(MessageCatalog.DataSynchronization.Import.BRANCH_CODE_MATCH_ERROR), MessageBoxButtons.YesNo, MessageBoxIcon.Warning).Equals(DialogResult.No))
                            {
                                IsValid = false;
                            }
                        }
                        else if (!lblHOCode.Text.ToLower().Equals(SettingProperty.headofficecode.ToLower()))
                        {
                            if (ShowConfirmationMessage(GetMessage(MessageCatalog.DataSynchronization.Import.HEAD_OFFICE_CODE_MATCH_ERROR), MessageBoxButtons.YesNo, MessageBoxIcon.Warning).Equals(DialogResult.No))
                            {
                                IsValid = false;
                            }
                        }
                    }
                }
                else
                {
                    this.ShowMessageBoxError("Invalid Project XML data. License/Project details are not found.");
                    IsValid = false;
                }
            }
            catch (ArgumentException errmissing)
            {
                this.ShowMessageBoxWarning("Invalid Acmeerp Project Voucher Data XML file.");
                AcMELog.WriteLog(errmissing.Message);
            }
            catch (Exception ex)
            {
                this.ShowMessageBoxWarning("Invalid Acmeerp Project Voucher Data XML file.");
                AcMELog.WriteLog(ex.Message);
            }
            finally { }
            return IsValid;
        }

        /// <summary>
        /// 11/12/2018, to set finance setting bsed on imported project
        /// 1. enable flexi fd setting if imporeted project has flexi fd
        /// 2. enable GST 
        /// </summary>
        private void UpdateFinanceSetting()
        {
            bool reinvestedexists = false;
            bool fdadjustmententry = false;
            bool gstvouchersexists = false;
            bool gstvouchervendorsexists = false;
            bool zerovaluedCashBankVouchersExists = false;

            // FD related finance setting
            using (FDAccountSystem fdaccount = new FDAccountSystem())
            {
                reinvestedexists = fdaccount.HasFlxiFD();
                fdadjustmententry = fdaccount.HasFDAdjustmentEntries();
            }

            //10/07/2021, If GST enabled and Vendor details enabled ------------------------
            using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
            {
                gstvouchersexists = vouchersystem.IsExistsGSTVouchers();
                if (gstvouchersexists)
                {
                    gstvouchervendorsexists = vouchersystem.IsExistsGSTVendorDetails();
                }

                zerovaluedCashBankVouchersExists = vouchersystem.IsZeroValuedCashBankExistsInVouchers();

            }

            //Enable Finance Setting
            using (UISettingSystem uisystemsetting = new UISettingSystem())
            {
                if (reinvestedexists)
                {
                    uisystemsetting.SaveUISettingDetails(FinanceSetting.EnableFlexiFD, "1", this.LoginUser.LoginUserId);
                }

                if (fdadjustmententry)
                {
                    uisystemsetting.SaveUISettingDetails(FinanceSetting.EnableFDAdjustmentEntry, "1", this.LoginUser.LoginUserId);
                }

                if (gstvouchersexists)
                {
                    uisystemsetting.SaveUISettingDetails(FinanceSetting.EnableGST, "1", this.LoginUser.LoginUserId);
                    if (gstvouchervendorsexists) uisystemsetting.SaveUISettingDetails(FinanceSetting.IncludeGSTVendorInvoiceDetails, "1", this.LoginUser.LoginUserId);
                }

                if (zerovaluedCashBankVouchersExists)
                {
                    uisystemsetting.SaveUISettingDetails(FinanceSetting.AllowZeroValuedCashBankVoucherEntry, "1", this.LoginUser.LoginUserId);
                    uisystemsetting.SaveUISettingDetails(FinanceSetting.TransEntryMethod, "1", this.LoginUser.LoginUserId);
                    string msg = "Imported Project(s) has Zero Valued Cash and Bank Vouchers, so \"Double Entry\" and \"Allow Zero Valued Cash/Bank in Voucher Entry\" options willb enabled";
                    this.ShowMessageBox(msg);
                }
            }
            //------------------------------------------------------------------------------

            ApplySetting();
        }

        /// <summary>
        /// On 05/05/2021, To generate FYs years if xml date range does not fall with in FYS in Target DB
        /// </summary>
        private ResultArgs GenerateNewFYPeriods()
        {
            Int32 FYAccountId = 0;
            using (AccouingPeriodSystem accountingsystem = new AccouingPeriodSystem())
            {
                resultargs = accountingsystem.GenerateFYPeriods(XMLDateTo);
                if (resultargs.Success)
                {
                    FYAccountId = accountingsystem.AccPeriodId;
                }
            }

            //Refresh Home Page
            if (FYAccountId > 0)
            {
                if (FYAccountId > 0)
                {
                    using (GlobalSetting globalSystem = new GlobalSetting())
                    {
                        //Update Status active for last FY
                        resultargs = globalSystem.UpdateAccountingPeriod(FYAccountId.ToString());
                        if (resultargs.Success)
                        {
                            using (AccouingPeriodSystem accountingSystem = new AccouingPeriodSystem())
                            {
                                resultargs = accountingSystem.FetchActiveTransactionPeriod();
                                if (resultargs.DataSource != null && resultargs.RowsAffected > 0)
                                {
                                    this.AppSetting.AccPeriodInfo = resultargs.DataSource.Table.DefaultView;
                                    if (AppMainFrm != null)
                                    {
                                        AppMainFrm.LoadHomeAccountingPeriod();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return resultargs;
        }


        private void ApplySetting()
        {
            ISetting isetting;
            isetting = new GlobalSetting();

            ResultArgs resultArg = isetting.FetchSettingDetails(this.UtilityMember.NumberSet.ToInteger(this.LoginUser.LoginUserId.ToString()));
            if (resultArg.Success && resultArg.DataSource.TableView != null && resultArg.DataSource.TableView.Count != 0)
            {
                this.UIAppSetting.UISettingInfo = resultArg.DataSource.TableView;
                this.AppSetting.SettingInfo = resultArg.DataSource.TableView;
            }
        }

        private bool IsInputValid()
        {
            bool IsValid = true;
            if (string.IsNullOrEmpty(txtPath.Text))
            {
                ShowMessageBox(GetMessage(MessageCatalog.DataSynchronization.Import.FILE_NAME_EMPTY));
                IsValid = false;
            }
            else if (string.IsNullOrEmpty(XMLDateFrom) || string.IsNullOrEmpty(XMLDateTo))
            {
                ShowMessageBox("Date Range is not available in Project XMl Data");
                IsValid = false;
            }
            else if (!ValidateLicenseKey())
            {
                IsValid = false;
            }
            else if (XML_ImportType != ImportType.SplitProject)
            {
                this.ShowMessageBoxWarning("'" + FileName + "' is not valid Project Split Data file.");
                IsValid = false;
            }
            else if (AppSetting.FirstFYDateFrom > UtilityMember.DateSet.ToDate(XMLDateFrom, false))
            {
                //XML Date from should be equal to Books Begin/First FY Date From of Current Data DB
                //if (IsValid  && UtilityMember.DateSet.ToDate(AppSetting.BookBeginFrom, false) > importVoucher.FYDateFrom)
                string msg = "Could not import Project XML Data. " + System.Environment.NewLine +
                        "First Finance Year \"Year From\" (" + AppSetting.FirstFYDateFrom.ToShortDateString() + ") in target Branch " +
                        "should be less than or equal to given Project XML's \"Date From\" (" + XMLDateFrom + ").";
                //System.Environment.NewLine + " Note : Move/Re-order first Finance Year.";
                this.ShowMessageBoxWarning(msg);
                IsValid = false;
            }
            else if (chkWithFDVouchers.Checked && AppSetting.FirstFYDateFrom > UtilityMember.DateSet.ToDate(FYProjectFDVoucherFirstDateInXML.ToShortDateString(), false))
            {
                string msg = "Could not import Project XML Data." + System.Environment.NewLine +
                             "Fixed Deposit Vouchers are available from " + FYProjectFDVoucherFirstDateInXML.ToShortDateString() + " onwards in the Project XML." + System.Environment.NewLine +
                             "First Finance Year \"Year From\" (" + AppSetting.FirstFYDateFrom.ToShortDateString() + ") in target Branch should be less than or equal to " +
                             "given Project XML's Fixed Deposit Vouchers (" + FYProjectFDVoucherFirstDateInXML.ToShortDateString() + ").";

                //System.Environment.NewLine + " Note : Move/Re-order first Finance Year.";
                this.ShowMessageBoxWarning(msg);
                IsValid = false;
            }
            else
            {
                //If Vouchers availalbe in target database, get last voucher date's FY Year from
                //This will be enabled only if we import FD vouchers
                Int32 mergeprojectid = glkpMergeProject.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkpMergeProject.EditValue.ToString()) : 0;

                //For FD module validations
                if ((XML_ProjectId > 0 || mergeprojectid > 0) && chkWithFDVouchers.Checked)
                {
                    DialogResult showConfirmationMessage = System.Windows.Forms.DialogResult.OK;
                    if (!isBulkImport)
                    {
                        showConfirmationMessage = this.ShowConfirmationMessage("\"With Fixed Deposit Vouchers\" option is enabled, all the Fixed Deposit Vouchers (from Books Beginning) will be replaced from Project XML Data",
                                                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    }

                    if (showConfirmationMessage == System.Windows.Forms.DialogResult.OK)
                    {
                        //On 22/01/2024, If there is no FD Vouchers in Target DB, let us allow to import it.
                        //if (FYProjectLastVoucherDateInDB != DateTime.MinValue)
                        if (FYProjectLastVoucherDateInDB != DateTime.MinValue && XML_ProjectFDVoucherExitsInDB)
                        {
                            DateTime LastFYYearFromVoucherDateExisting = UtilityMember.DateSet.GetFinancialYearByDate(FYProjectLastVoucherDateInDB, AppSetting.YearFrom);
                            DateTime FirstFYYearFromVoucherDateExisting = UtilityMember.DateSet.GetFinancialYearByDate(FYProjectFirstVoucherDateInDB, AppSetting.YearFrom);
                            //If XML Project Data is less than already existing Vouchers FY Year from
                            if (chkCanOverride.Checked == false && UtilityMember.DateSet.ToDate(XMLDateFrom, false) < LastFYYearFromVoucherDateExisting)
                            {
                                string msg = "Could not import Project XML Data in between Finance Years, Project's Fixed Deposit Voucher(s) are already available in target Branch. " +
                                                System.Environment.NewLine + "You can import Project XML data from '" + LastFYYearFromVoucherDateExisting.ToShortDateString() + "' onwards." +
                                            System.Environment.NewLine + System.Environment.NewLine + "Note : Clear existing Project Vouchers and Import it again.";
                                this.ShowMessageBoxWarning(msg);
                                IsValid = false;
                            }
                        }

                        if (IsValid && XML_ProjectFDVoucherExitsInDB && !FYProjectFDVoucherExistsInXML)
                        {
                            string msg = "Could not import Project XML Data." +
                                     System.Environment.NewLine + "Note : Fixed Deposit Vouchers are available in target Branch, but not available in Project XML Data." +
                                     System.Environment.NewLine + "           If you still want to import Project data, uncheck \"With Fixed Deposit Vouchers\" and import it.";
                            this.ShowMessageBoxWarning(msg);
                            IsValid = false;
                        }
                        else if (IsValid && XML_ProjectFDVoucherExitsInDB && FYProjectFDVoucherExistsInXML)
                        {
                            if (mergeprojectid > 0 && XML_ProjectId != mergeprojectid)
                            {
                                string msg = "Both Projects (Merge Project and XML Project Data) are different and have Fixed Deposit Vouchers." +
                                     System.Environment.NewLine + "Are you sure to replace Fixed Deposit Vouchers from Project XML data to target Branch?";
                                if (this.ShowConfirmationMessage(msg, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.OK)
                                {
                                    IsValid = false;
                                }
                            }
                        }
                    }
                }

                //For Budget module
                if (IsValid)
                {
                    //if ((XML_ProjectId > 0 || mergeprojectid > 0) && chkIncludeBudget.Checked)
                    if (chkIncludeBudget.Checked)
                    {
                        if (MismatchBudgetProjectsWithDBProjects)
                        {
                            string msg = "XML Budget is combined with other Project(s), Few of the Projects are not available in the current Branch."
                                          + System.Environment.NewLine + "Import Project Data without Budget.";

                            if (NoOfBudgetProjects == 1)
                            {
                                msg = "XML Budget's Project is not available in the current Branch. Import Project Data without Budget.";
                            }

                            this.ShowMessageBoxError(msg);
                            IsValid = false;
                        }
                    }

                    if (IsValid && isBulkImport && !XML_IsFYSplit)
                    {
                        this.ShowMessageBoxError("Finance Year splitting option is not enabled in the XML File");
                        IsValid = false;
                    }
                }
            }

            if (IsValid && XML_IsFYSplit && !isBulkImport) //For Split FY
            {
                if (this.ShowConfirmationMessage("This XML is Finance Year splitting option enabled, Fixed Deposit Vouchers (Investments, Openings and Renewals) will be rearranged based on Books Beginning From.",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
                {
                    IsValid = false;
                }

                //Check Project already exits and lock it for Split FY
                if (IsValid && XML_ProjectId > 0 && XML_ProjectVoucherExitsInDB)
                {
                    this.ShowMessageBoxError("As Split Finance Year option is enabled, '" + XML_ProjectName + "' Voucher(s) are already available, You can't import it.");
                    IsValid = false;
                }
            }

            //On 04/01/2022, to show ledgernmapping
            if (IsValid && XML_IsShowMappingLedger)
            {
                //On 03/01/2022, To show Mapping screen if and if only mapping enabled project xml ---------------------
                frmProjectImportMapLedgers frmprojectimportmappledgers = new frmProjectImportMapLedgers(XML_Ledgers, true);
                if (frmprojectimportmappledgers.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    MappedLedgers = frmprojectimportmappledgers.dtProjectImportedMappedLedger;
                }
                else
                {
                    this.ShowMessageBoxError("As Map Ledger option is enabled, You have to map all the Imported Ledgers with existing Ledgers.");
                    IsValid = false;
                }

                //------------------------------------------------------------------------------------------------------
            }


            return (IsValid);
        }

        private ResultArgs RefereshBalance(Int32 ProjectId)
        {
            using (BalanceSystem RefereshBalance = new BalanceSystem())
            {
                RefereshBalance.VoucherDate = setting.BookBeginFrom;

                //On 20/09/2022, If not FD included and not include opening balance (Refresh alone from xml date from )
                if (!chkWithFDVouchers.Checked && !chkIncludeOPBalance.Checked)
                {
                    RefereshBalance.VoucherDate = XMLDateFrom;
                }

                //On 03/07/2020, it was all the Projects, now changed to refresh only imported project alone, 
                RefereshBalance.ProjectId = ProjectId;
                ResultArgs result = RefereshBalance.UpdateBulkTransBalance();
            }
            return resultargs;
        }

        private void InitalImport()
        {

            lblDateRange.Text = string.Empty;
            lblProjectName.Text = string.Empty;
            lblFDVoucherExisting.Text = string.Empty;
            //lcDateRange.Visibility = lcProjectName.Visibility = LayoutVisibility.Never;
            LoadDefaults();

            lcCapHO.Visibility = lcHO.Visibility = LayoutVisibility.Never;
            lcCapBO.Visibility = lcBO.Visibility = LayoutVisibility.Never;
            lcCapSplitFY.Visibility = lcSplitFY.Visibility = LayoutVisibility.Never;
            lcDateRange.Visibility = lcProjectName.Visibility = LayoutVisibility.Never;
            lcWithFDVouchers.Visibility = lcIncludeOPBalance.Visibility = lcIncludeBudgetModule.Visibility = LayoutVisibility.Never;
            lcCapShowMappingLedger.Visibility = lcShowMappingLedger.Visibility = LayoutVisibility.Never;

            //chkWithFDVouchers.Text = "With Fixed Deposit Vouchers (It will be overwritten always from Books Beginning '"+ UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom,false).ToShortDateString() +"' )";
            this.Height = 165; //By default
            this.CenterToScreen();
        }

        public void LoadDefaults()
        {
            using (ExportVoucherSystem vouchersystem = new ExportVoucherSystem())
            {
                ResultArgs resultArgs = vouchersystem.FetchProjectsLookup();
                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    UtilityMember.ComboSet.BindGridLookUpCombo(glkpMergeProject, resultArgs.DataSource.Table, vouchersystem.AppSchema.Project.PROJECTColumn.ColumnName, vouchersystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                }
            }
        }

        /// <summary>
        /// On 05/25/2021, To reset and ininilize xml details
        /// </summary>
        /// <returns></returns>
        private bool InitXMLSelection()
        {
            bool rtn = false;
            try
            {
                chkCanOverride.Checked = chkIncludeOPBalance.Checked = false;
                chkWithFDVouchers.Checked = true;
                chkWithFDVouchers.Enabled = true;
                chkIncludeBudget.Checked = true;
                chkIncludeBudget.Enabled = true;
                chkIncludeOPBalance.Enabled = true;
                chkWithFDVouchers.Font = chkIncludeOPBalance.Font = new System.Drawing.Font(chkIncludeOPBalance.Font.FontFamily, chkIncludeOPBalance.Font.Size, System.Drawing.FontStyle.Regular);
                lcMergeProject.Visibility = LayoutVisibility.Always;
                string datefrom = string.Empty;
                string dateto = string.Empty;
                string projectname = string.Empty;
                //this.Height = 235;

                XML_IsFYSplit = false;
                XMLDateFrom = string.Empty;
                XMLDateTo = string.Empty;
                XML_ProjectName = string.Empty;
                XML_ProjectId = 0;
                XML_ProjectVoucherExitsInDB = false;
                XML_ProjectFDVoucherExitsInDB = false;
                FYProjectFirstVoucherDateInDB = DateTime.MinValue;
                FYProjectLastVoucherDateInDB = DateTime.MinValue;
                FYProjectFDVoucherExistsInXML = false;
                FYProjectFDVoucherFirstDateInXML = DateTime.MinValue;
                MismatchBudgetProjectsWithDBProjects = false;
                NoOfBudgetProjects = 0;
                IsGSTEnabled = false;
                IsGSTVendorDetailsEnabled = false;
                XML_IsShowMappingLedger = false;
                MappedLedgers = null;
                XML_ImportType = ImportType.SplitProject;

                using (ImportVoucherSystem importVoucher = new ImportVoucherSystem())
                {
                    lcCapHO.Visibility = lcHO.Visibility = LayoutVisibility.Always;
                    lcCapBO.Visibility = lcBO.Visibility = LayoutVisibility.Always;
                    lcCapSplitFY.Visibility = lcSplitFY.Visibility = LayoutVisibility.Always;
                    lcDateRange.Visibility = lcProjectName.Visibility = LayoutVisibility.Always;
                    lcWithFDVouchers.Visibility = lcIncludeOPBalance.Visibility = lcIncludeBudgetModule.Visibility = LayoutVisibility.Always;
                    lcCapShowMappingLedger.Visibility = lcShowMappingLedger.Visibility = LayoutVisibility.Always;

                    lblDateRange.Text = string.Empty;
                    lblProjectName.Text = string.Empty;
                    lblSplitFYOption.Text = "No";
                    lblFDVoucherExisting.Text = string.Empty;
                    lblShowMapping.Text = "No";

                    //On 14/04/2021
                    CommonMethod.BranchOfficeCode = string.Empty;
                    CommonMethod.HeadOfficeCode = string.Empty;
                    importVoucher.MergeProject = glkpMergeProject.EditValue != null ? glkpMergeProject.Text : string.Empty;
                    importVoucher.MergerProjectId = glkpMergeProject.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkpMergeProject.EditValue.ToString()) : 0;
                    importVoucher.InitializeLicenceDetails(FileName);

                    lblBOCode.Text = CommonMethod.BranchOfficeCode.ToUpper();
                    lblHOCode.Text = CommonMethod.HeadOfficeCode.ToUpper();
                    lblSplitFYOption.Text = importVoucher.IsFYSplit ? "Yes" : "No";

                    datefrom = importVoucher.FYDateFrom.ToShortDateString();
                    dateto = importVoucher.FYDateTo.ToShortDateString();
                    projectname = importVoucher.FYProjectName;
                    lblShowMapping.Text = importVoucher.IsShowMappingLedgers ? "Yes" : "No";

                    lcOverwrite.Visibility = LayoutVisibility.Always;
                    if (!isBulkImport) this.Height = 255;
                    if (importVoucher.IsFYSplit) //For Split FY
                    {
                        chkCanOverride.Checked = false;
                        lcOverwrite.Visibility = LayoutVisibility.Never;
                        chkWithFDVouchers.Checked = chkIncludeOPBalance.Checked = true;
                        chkWithFDVouchers.Enabled = chkIncludeOPBalance.Enabled = false;
                        chkWithFDVouchers.Font = chkIncludeOPBalance.Font = new System.Drawing.Font(chkIncludeOPBalance.Font.FontFamily, chkIncludeOPBalance.Font.Size, System.Drawing.FontStyle.Bold);

                        //Hide Merge project, if split FY option is enabled
                        glkpMergeProject.EditValue = 0;
                        lcMergeProject.Visibility = LayoutVisibility.Never;
                        if (!isBulkImport) this.Height = 235;
                    }
                    else
                    {
                        if (!importVoucher.FYProjectFDVoucherExistsInXML)
                        {
                            chkWithFDVouchers.Enabled = chkWithFDVouchers.Checked = false;
                        }

                        if (!importVoucher.FYProjectBudgetExistsInXML)
                        {
                            chkIncludeBudget.Enabled = chkIncludeBudget.Checked = false;
                        }

                    }

                    //Assign XML properties into local variables
                    XML_IsFYSplit = importVoucher.IsFYSplit;
                    XMLDateFrom = datefrom;
                    XMLDateTo = dateto;
                    XML_ProjectId = importVoucher.FYProjectId;
                    XML_ProjectName = projectname;
                    XML_ProjectVoucherExitsInDB = importVoucher.FYProjectVoucherExistsInDB;
                    XML_ProjectFDVoucherExitsInDB = importVoucher.FYProjectFDVoucherExistsInDB;
                    FYProjectFirstVoucherDateInDB = importVoucher.FYProjectFirstVoucherDateInDB;
                    FYProjectLastVoucherDateInDB = importVoucher.FYProjectLastVoucherDateInDB;
                    FYProjectFDVoucherExistsInXML = importVoucher.FYProjectFDVoucherExistsInXML;
                    FYProjectFDVoucherFirstDateInXML = importVoucher.FYProjectFDVoucherFirstDateInXML;
                    FYProjectBudgetExistsInXML = importVoucher.FYProjectBudgetExistsInXML;
                    MismatchBudgetProjectsWithDBProjects = importVoucher.MismatchBudgetProjectsWithDBProjects;
                    NoOfBudgetProjects = importVoucher.NoOfBudgetProjects;

                    IsGSTEnabled = importVoucher.IsGSTEnabled;
                    IsGSTVendorDetailsEnabled = importVoucher.IsGSTVendorDetailsEnabled;
                    XML_IsShowMappingLedger = importVoucher.IsShowMappingLedgers;
                    XML_Ledgers = importVoucher.Ledgers;
                    XML_ImportType = importVoucher.VoucherImportType;
                    this.CenterToScreen();
                }

                lblDateRange.Text = string.Empty;
                lblProjectName.Text = string.Empty;
                lblFDVoucherExisting.Text = string.Empty;
                if (!string.IsNullOrEmpty(datefrom) && !string.IsNullOrEmpty(dateto))
                {
                    lblDateRange.Text = "From    : <b>" + datefrom + "</b>  To : <b>" + dateto + "</b>";
                }

                if (!string.IsNullOrEmpty(projectname))
                {
                    lblProjectName.Text = "Project : <b>" + projectname + "</b>";
                }

                lblFDVoucherExisting.Text = "FD Vouchers : <b>" + (FYProjectFDVoucherExistsInXML ? "Yes" : "No") + "</b>";

                if (isBulkImport)
                {
                    chkCanOverride.Checked = true;
                    chkIncludeBudget.Checked = false;
                    chkIncludeBudget.Enabled = chkIncludeOPBalance.Enabled = false;
                }

            }
            catch (ArgumentException errmissing)
            {
                this.ShowMessageBoxWarning("Invalid Acmeerp Project Voucher Data XML file.");
                AcMELog.WriteLog(errmissing.Message);
            }
            catch (Exception ex)
            {
                this.ShowMessageBoxWarning("Invalid Acmeerp Project Voucher Data XML file.");
                AcMELog.WriteLog(ex.Message);
            }
            finally { }
            return rtn;
        }




        #endregion



    }
}