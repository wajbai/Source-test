using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;

using Bosco.Report.Base;
using Bosco.Utility;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraEditors.Repository;
using System.Collections;

using Bosco.Report.SQL;
using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;
using DevExpress.XtraEditors;
using Bosco.Utility.ConfigSetting;
using DevExpress.XtraBars.Docking;
using Bosco.Report.View;
using PAYROLL.Modules;
using DevExpress.XtraGrid.Views.Grid;
using System.IO;
using System.Reflection;
using DevExpress.XtraEditors.Controls;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Columns;

namespace Bosco.Report.View
{
    public partial class frmReportFilter : DevExpress.XtraEditors.XtraForm
    {
        #region Declaration
        public event EventHandler ReportFilterApplied;
        private GridEditorCollection gridEditors;
        //SettingProperty settingProperty = new SettingProperty();
        UserProperty settinguserProperty = new UserProperty();
        private const string SCONSTATEMENT = "Consolidated Statement";
        private const string DateCaption = "Date As on";
        private const string SELECT = "SELECT";
        private string Selectedlang = string.Empty;
        private DataTable dtLedgers = new DataTable();
        private DataTable dtBank = new DataTable();
        private DataTable dtState = new DataTable();
        ReportProperty reportProperty = new Base.ReportProperty();
        private AppSchemaSet.ApplicationSchemaSet appSchema = new AppSchemaSet.ApplicationSchemaSet();
        private Payroll.DAO.Schema.AppSchemaSet.ApplicationSchemaSet payappschema = new Payroll.DAO.Schema.AppSchemaSet.ApplicationSchemaSet();
        private ReportSetting reportSchema = new ReportSetting();

        bool success = true;
        string sDateFrom = string.Empty;
        string sDateTo = string.Empty;
        string AcmeerpInstalledPath = AppDomain.CurrentDomain.BaseDirectory;

        public bool IsDateLoaded = false;
        ResultArgs resultArgs = null;
        public clsPayrollBase Paybase = new clsPayrollBase();

        public static DataTable dtMultiColumnBankSource = new DataTable();

        #endregion

        #region Constructor
        public frmReportFilter()
        {
            InitializeComponent();
            this.gridEditors = new GridEditorCollection();
        }
        #endregion

        #region Properties
        private string groupIds = "";
        private string GroupIds
        {
            get { return groupIds; }
            set { groupIds = value; }
        }
        public DataTable ProjectSelected { get; set; }
        public DataTable BankSelected { get; set; }
        public DataTable CostCentreSelected { get; set; }
        public DataTable LedgerSelected { get; set; }
        public DataTable LedgerGroupSelected { get; set; }
        public DataTable NarrationSelected { get; set; }
        public DataTable BudgetSelected { get; set; }
        public DataTable FDSelected { get; set; }
        public DataTable AssignDefaultProjects { get; set; }
        public DataTable PayrollSelected { get; set; }
        public DataTable PayrollGroupSelected { get; set; }
        private DataTable dtselectedProjectId { get; set; }
        public DataTable TaskSelected { get; set; }
        private int societyId = 0;
        public int SocietyId
        {
            get { return societyId; }
            set { societyId = value; }
        }

        private int itrgroupId = 0;
        public int ITRGroupId
        {
            get { return itrgroupId; }
            set { itrgroupId = value; }
        }

        private int cccategoryId;
        public int CCCategoryId
        {
            get { return cccategoryId; }
            set { cccategoryId = value; }
        }

        private int UserRoleId { get; set; }
        private int AssetClassId { get; set; }
        public DataTable AssignDefaultAssetClasses { get; set; }
        public DataTable AssetclassSelected { get; set; }
        DataTable dtCostCentreWithProject;

        //On 25/01/2021, to retain View Chart Type in Report Settings
        DevExpress.XtraEditors.Repository.RepositoryItemComboBox ricbChartType = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
        Int32 repcbSelectedChartType = 0;

        Int32 repcbDayBookVoucherType = 0;
        string repcbDayBookVoucherTypeName = string.Empty;

        Int32 repcbNoOfCompareYears = 3;
        string repcbUserName = string.Empty;
        string repcbCreatedBy = string.Empty;
        Int32 repFDScheme = -1;
        string repFDSchemeName = string.Empty;
        //On 16/09/2021, User Modified, If you use two combo in the same report -----------------------------------
        DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
        string repcbModifiedBy = string.Empty;
        Int32 repcbFDInvestmentTypeId = (int)FDInvestmentType.None;
        string repcbFDInvestmentType = FDInvestmentType.None.ToString();
        DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
        string repcbAuditAction = string.Empty;

        DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox3 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
        //----------------------------------------------------------------------------------------------------------
        DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit repositoryMultiItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();

        double EuroExhangeRate = 0;
        double EuroDollarExhangeRate = 0;
        DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryText;

        DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repositoryItemGridlookup1 = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();

        Int32 repcbCountryCurrencyId = 0;
        string repcbCountryCurrency = string.Empty;
        string repcbCurrencySymbol = string.Empty;

        Int32 repcbRPSortOrder = 0;

        public string SelectedProjectId
        {
            get
            {
                string projectid = string.Empty;
                DataTable dtProject = gcProject.DataSource as DataTable;
                if (dtProject != null && dtProject.Rows.Count != 0)
                {
                    foreach (int i in gvProject.GetSelectedRows())
                    {
                        DataRow row = gvProject.GetDataRow(i);
                        projectid += row[this.appSchema.Project.PROJECT_IDColumn.ColumnName] + ",";
                    }
                    projectid = projectid.TrimEnd(',');
                }
                return projectid;
            }
        }

        private string BudgetNewProject
        {
            get
            {
                string budgetnewproject = "";
                budgetnewproject = gvBudgetNewProject.GetFocusedRowCellValue(ColBudgetNewProject) != null ?
                    gvBudgetNewProject.GetRowCellValue(gvBudgetNewProject.FocusedRowHandle, ColBudgetNewProject).ToString() : "";
                return budgetnewproject;
            }
        }

        private double BudgetNewProjectPIncomeAmount
        {
            get
            {
                double budgetNewProjectPIncomeAmount = 0;
                budgetNewProjectPIncomeAmount = gvBudgetNewProject.GetFocusedRowCellValue(ColBudgetPIncomeAmount) != null ?
                    ReportProperty.Current.NumberSet.ToDouble(gvBudgetNewProject.GetRowCellValue(gvBudgetNewProject.FocusedRowHandle, ColBudgetPIncomeAmount).ToString()) : 0;
                return budgetNewProjectPIncomeAmount;
            }
        }

        private double BudgetNewProjectPExpenseAmount
        {
            get
            {
                double budgetNewProjectPExpenseAmount = 0;
                budgetNewProjectPExpenseAmount = gvBudgetNewProject.GetFocusedRowCellValue(ColBudgetPExpenseAmount) != null ?
                   ReportProperty.Current.NumberSet.ToDouble(gvBudgetNewProject.GetRowCellValue(gvBudgetNewProject.FocusedRowHandle, ColBudgetPExpenseAmount).ToString()) : 0;
                return budgetNewProjectPExpenseAmount;
            }
        }

        private double BudgetNewProjectPGovtHelp
        {
            get
            {
                double budgetNewProjectPGovtHelp = 0;
                budgetNewProjectPGovtHelp = gvBudgetNewProject.GetFocusedRowCellValue(ColBudgetPGovtIncomeAmount) != null ?
                    ReportProperty.Current.NumberSet.ToDouble(gvBudgetNewProject.GetRowCellValue(gvBudgetNewProject.FocusedRowHandle, ColBudgetPGovtIncomeAmount).ToString()) : 0;
                return budgetNewProjectPGovtHelp;
            }
        }

        private double BudgetNewProjectPProvinceHelp
        {
            get
            {
                double budgetNewProjectPProvinceHelp = 0;
                budgetNewProjectPProvinceHelp = gvBudgetNewProject.GetFocusedRowCellValue(ColPProvinceHelp) != null ?
                    ReportProperty.Current.NumberSet.ToDouble(gvBudgetNewProject.GetRowCellValue(gvBudgetNewProject.FocusedRowHandle, ColPProvinceHelp).ToString()) : 0;
                return budgetNewProjectPProvinceHelp;
            }
        }

        private double BudgetNewProjectIncludeProject
        {
            get
            {
                Int32 budgetNewProjectPIncludeProject = 0;
                budgetNewProjectPIncludeProject = gvBudgetNewProject.GetFocusedRowCellValue(colBudgetIncludeReports) != null ?
                    ReportProperty.Current.NumberSet.ToInteger(gvBudgetNewProject.GetRowCellValue(gvBudgetNewProject.FocusedRowHandle, colBudgetIncludeReports).ToString()) : 0;
                return budgetNewProjectPIncludeProject;
            }
        }

        private string BudgetNewProjectRemarks
        {
            get
            {
                string budgetNewProjectRemakrs = string.Empty;
                budgetNewProjectRemakrs = gvBudgetNewProject.GetFocusedRowCellValue(ColBudgetPRemakrs) != null ?
                    gvBudgetNewProject.GetRowCellValue(gvBudgetNewProject.FocusedRowHandle, ColBudgetPRemakrs).ToString() : string.Empty;
                return budgetNewProjectRemakrs;
            }
        }

        //Focus to new row
        private bool BudgetNewProjectGridNewItem
        {
            set
            {
                if (value)
                {

                    DataTable dtTransaction = gcBudgetNewProject.DataSource as DataTable;
                    dtTransaction.Rows.Add(dtTransaction.NewRow());
                    gcBudgetNewProject.DataSource = dtTransaction;
                    gvBudgetNewProject.FocusedColumn = ColBudgetNewProject;
                    gvBudgetNewProject.ShowEditor();
                }
            }
        }

        #endregion

        #region Events
        /// <summary>
        /// Load the basic records to the forms.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmReportFilter_Load(object sender, EventArgs e)
        {
            //For Temp purpose ----------------------------------------
            /*lcSign1.Visibility = LayoutVisibility.Never;
            lcSign1Btn.Visibility = LayoutVisibility.Never;

            lcSign2.Visibility = LayoutVisibility.Never;
            lcSign2Btn.Visibility = LayoutVisibility.Never;

            lcSign3.Visibility = LayoutVisibility.Never;
            lcSign3Btn.Visibility = LayoutVisibility.Never;*/
            //-----------------------------------------------------------
            lcReportSetupCurrency.Visibility = LayoutVisibility.Never;
            xtbSign.PageVisible = false;
            Selectedlang = this.settinguserProperty.LanguageId;
            if (Selectedlang == "pt-PT")
            {
                this.Width = 750;
                xtbLocation.TabPages[5].TabPageWidth = 150;
                lblReportDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Default;
                lblTitle.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Default;
                //chkLedgerGroup.Visible = chkLedgerSelect.Visible = chkProjectSelectAll.Visible = chkBankSelectAll.Visible = false;
            }
            else if (Selectedlang == "id-ID")
            {
                this.Width = 760;
                xtbLocation.TabPages[5].TabPageWidth = 150;
                lblReportDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Default;
                lblTitle.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Default;
                //chkLedgerGroup.Visible = chkLedgerSelect.Visible = chkProjectSelectAll.Visible = chkBankSelectAll.Visible = false;
            }
            else
            {
                this.Width = 508;
                xtbLocation.TabPages[5].TabPageWidth = 80;
            }

            if (ReportProperty.Current.ReportId.Equals("RPT-072") || ReportProperty.Current.ReportId.Equals("RPT-168"))
            {
                chkProject.Visible = chkBank.Visible = chkLedger.Visible = chkLedgerGroup.Visible = chkPayrollComponents.Visible = chkCostCentre.Visible = chkPartyLedger.Visible = chkNatureofPayments.Visible = chkDeducteeType.Visible = chkPayroll.Visible = chkPayrollStaff.Visible = false;
            }

            //On 10/06/2024, Option to update Generate Fixed Asset or Depreciation Ledges
            lcUpdateAssetDepreciation.Visibility = LayoutVisibility.Never;
            if (ReportProperty.Current.ReportId == "RPT-191" || ReportProperty.Current.ReportId == "RPT-218")
            {
                lcUpdateAssetDepreciation.Visibility = LayoutVisibility.Always;
            }

            lblWithInKind.Visibility = LayoutVisibility.Never;
            UserRoleId = settinguserProperty.RoleId;
            EnableTabs();

            if (!ReportProperty.Current.ReportId.Equals("RPT-STD") && !ReportProperty.Current.ReportId.Equals("RPT-152")) //temp purpose, have to discuss the below lines of code 
            {
                //On 07/06/2019, Corrected to make proper project tab visiable based on modules
                xtpProject.PageVisible = SettingProperty.ReportModuleId == (int)ReportModule.Payroll && (!SettingProperty.PayrollFinanceEnabled) ? false : true;
                xtpProject.PageVisible = SettingProperty.ReportModuleId == (int)ReportModule.NetWorking ? false : true;

                if (ReportProperty.Current.ReportId.Equals("RPT-087")) { xtpProject.PageVisible = false; }
                if (ReportProperty.Current.ReportId.Equals("RPT-109")) { lcgDate.Visibility = LayoutVisibility.Never; }
                if (ReportProperty.Current.ReportId.Equals("RPT-113")) { lcgDate.Visibility = LayoutVisibility.Never; }
                if (ReportProperty.Current.ReportId.Equals("RPT-027") || ReportProperty.Current.ReportId.Equals("RPT-031")) { lciReportCode.Visibility = LayoutVisibility.Always; }
                //For AC Sign details, Budget Annual Summary, CC enabled Ledgers List
                if (ReportProperty.Current.ReportId.Equals("RPT-162") ||
                    ReportProperty.Current.ReportId.Equals("RPT-198")) { xtpProject.PageVisible = false; }

                if (ReportProperty.Current.ReportId.Equals("RPT-069") || ReportProperty.Current.ReportId.Equals("RPT-071") || ReportProperty.Current.ReportId.Equals("RPT-072")
                    || ReportProperty.Current.ReportId.Equals("RPT-168"))
                { xtpProject.PageVisible = false; } //For PayRoll Reports
            }
            if (ReportProperty.Current.ReportId.Equals("RPT-127"))
            {
                xtbLocation.SelectedTabPageIndex = 17;
            }
            if (ReportProperty.Current.ReportId.Equals("RPT-016") || ReportProperty.Current.ReportId.Equals("RPT-038"))
            {
                lcgBankAccount.Text = "Cash Account";
            }
            IncreaseFormWidth();
            repositoryItemComboBox.SelectedIndexChanged += new EventHandler(repositoryItemComboBox_SelectedIndexChanged);
            this.repositoryItemComboBox1.SelectedIndexChanged += new EventHandler(repositoryItemComboBox1_SelectedIndexChanged);
            this.repositoryItemComboBox1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.repositoryItemComboBox1.BorderStyle = BorderStyles.Simple;
            this.repositoryItemComboBox2.SelectedIndexChanged += new EventHandler(repositoryItemComboBox2_SelectedIndexChanged);
            this.repositoryItemComboBox2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.repositoryItemComboBox2.BorderStyle = BorderStyles.Simple;
            this.repositoryItemComboBox3.SelectedIndexChanged += new EventHandler(repositoryItemComboBox3_SelectedIndexChanged);
            this.repositoryItemComboBox3.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.repositoryItemComboBox3.BorderStyle = BorderStyles.Simple;
            repositoryItemRadioGroup1.SelectedIndexChanged += new EventHandler(repositoryItemRadioGroup1_SelectedIndexChanged);
            this.repositoryMultiItemComboBox1.BorderStyle = BorderStyles.Simple;
            repositoryMultiItemComboBox1.EditValueChanged += new EventHandler(repositoryMultiItemComboBox1_EditValueChanged);
            repositoryItemGridlookup1.BorderStyle = BorderStyles.Simple;
            ReportDate.Tag = false;
            ReportDate.Properties.Buttons[1].Image = (ReportProperty.Current.ShowReportDate == 1) ? imgCollection.Images[1] : imgCollection.Images[0];
            EnableReportSetupProperties();

            // Enable multi column chooser for the Multi Column cash bank report
            lcgMultiColumnBankChooser.Visibility = (ReportProperty.Current.ReportId.Equals("RPT-101")) ? LayoutVisibility.Always : LayoutVisibility.Never;
            if (lcgMultiColumnBankChooser.Visibility == LayoutVisibility.Always)
            {
                setmultibank();
            }

            //On 10/04/2019, to retain show by Insution Header/Socity header ---------------------------------------------
            CheckLegalEntity();
            //------------------------------------------------------------------------------------------------------------

            //On 03/03/2021, to lock sing details in Report page
            txtRoleName1.Properties.ReadOnly = txtRoleName2.Properties.ReadOnly = txtRoleName3.Properties.ReadOnly = true;
            txtRole1.Properties.ReadOnly = txtRole2.Properties.ReadOnly = txtRole3.Properties.ReadOnly = true;

            this.KeyPreview = true;
            this.Focus();

            if (this.settinguserProperty.IS_SAPPIC)
            {
                cboReportType.Enabled = true;
                //cboReportType.SelectedIndex = 1;
            }
            else
            {
                cboBorderStyle.SelectedIndex = 0;
                cboBorderStyle.SelectedIndex = ReportProperty.Current.ReportBorderStyle;

                cboReportType.Enabled = false;
                // cboReportType.SelectedIndex = 0;
                lciReportCode.Visibility = LayoutVisibility.Never;
            }

            //lcUpdateAssetDepreciation.Visibility = LayoutVisibility.Never;


            if (ReportProperty.Current.ReportId == "RPT-228")
            {
                //lcSociety.Visibility = LayoutVisibility.Never;
                lciITRGroup.Visibility = LayoutVisibility.Always;
            }
            else
            {
                lcSociety.Visibility = LayoutVisibility.Always;
            }
        }

        void repositoryMultiItemComboBox1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.repositoryMultiItemComboBox1.Items.Count > 0 && this.repositoryMultiItemComboBox1.Items.GetCheckedValues() != null)
            {
                List<object> selecteditems = this.repositoryMultiItemComboBox1.Items.GetCheckedValues();
                ReportProperty.Current.LedgerNature = string.Join(", ", selecteditems);
            }
        }

        void repositoryItemRadioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioGroup radioGroup = (RadioGroup)sender;
            ReportProperty.Current.AnniversaryType = radioGroup.SelectedIndex;
        }

        private void repositoryItemComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*ComboBoxEdit cmbedit = (ComboBoxEdit)sender;
            ReportProperty.Current.FDRegisterStatus = cmbedit.SelectedIndex;*/
            ComboBoxEdit cmbedit = (ComboBoxEdit)sender;
            if (ReportProperty.Current.ReportId == "RPT-047" || ReportProperty.Current.ReportId == "RPT-094" || //FD Register
                ReportProperty.Current.ReportId == "RPT-219" || ReportProperty.Current.ReportId == "RPT-229") //Mutual Fund Register
            {
                ReportProperty.Current.FDRegisterStatus = cmbedit.SelectedIndex;
            }
            else if (ReportProperty.Current.ReportId == "RPT-195" || ReportProperty.Current.ReportId == "RPT-052" ||
                ReportProperty.Current.ReportId == "RPT-053" || ReportProperty.Current.ReportId == "RPT-204" ||
                ReportProperty.Current.ReportId == "RPT-012" || //Ledger
                ReportProperty.Current.ReportId == "RPT-208")   //Day Book and CC Daybook and Audit Log
            {
                //ReportProperty.Current.DayBookVoucherType = cmbedit.SelectedIndex;
                repcbDayBookVoucherType = GetVoucherTypeId(cmbedit.Text);
                repcbDayBookVoucherTypeName = cmbedit.Text;
                //ReportProperty.Current.DayBookVoucherType = GetVoucherTypeId(cmbedit.Text);
                //ReportProperty.Current.DayBookVoucherTypeName = cmbedit.Text;
            }
            else if (ReportProperty.Current.ReportId == "RPT-155" || ReportProperty.Current.ReportId == "RPT-156" || //Multi Abstract Year 
                    ReportProperty.Current.ReportId == "RPT-187" || ReportProperty.Current.ReportId == "RPT-182")    //Annual Budget Yealy Comparision
            {
                repcbNoOfCompareYears = ReportProperty.Current.NumberSet.ToInteger(cmbedit.Text);
                //ReportProperty.Current.NoOfYears = ReportProperty.Current.NumberSet.ToInteger(cmbedit.Text);

                //06/01/2021 This property is used to skip projects which are closed on or equal to this date----------------
                SetProjectSource();
                //-----------------------------------------------------------------------------------------------------------
            }

        }

        //On 25/01/2021, to retain View Chart Type in Report Settings
        private void ricbChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var editor = sender as ComboBoxEdit;
            if (editor != null)
            {
                repcbSelectedChartType = editor.SelectedIndex;
            }
        }

        private void repositoryItemComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*ComboBoxEdit cmbedit = (ComboBoxEdit)sender;
            ReportProperty.Current.FDRegisterStatus = cmbedit.SelectedIndex;*/
            ComboBoxEdit cmbedit = (ComboBoxEdit)sender;
            if (ReportProperty.Current.ReportId == "RPT-195" || ReportProperty.Current.ReportId == "RPT-196" ||
                ReportProperty.Current.ReportId == "RPT-197" || ReportProperty.Current.ReportId == "RPT-211")
            {
                //Audit Related Reports
                repcbModifiedBy = string.Empty;
                if (cmbedit.SelectedIndex != 0)
                {
                    repcbModifiedBy = cmbedit.Text;
                }
            }
            else if (ReportProperty.Current.ReportId == "RPT-047" || ReportProperty.Current.ReportId == "RPT-015" ||
                    ReportProperty.Current.ReportId == "RPT-150") //FD Register , FD Deposit Statement and FD Investment Register
            {
                //FD Investment Type
                repcbFDInvestmentTypeId = (int)FDInvestmentType.None;
                repcbFDInvestmentType = FDInvestmentType.None.ToString();
                if (cmbedit.SelectedIndex != 0)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchInvestmentTypeIdByInvestmentType))
                    {
                        dataManager.Parameters.Add(this.appSchema.FDInvestmentType.INVESTMENT_TYPEColumn, cmbedit.Text);

                        ResultArgs result = dataManager.FetchData(DataSource.DataTable);
                        if (result.Success && result.DataSource.Table != null)
                        {
                            DataTable dtInvestmentType = result.DataSource.Table;
                            if (dtInvestmentType.Rows.Count > 0)
                            {
                                repcbFDInvestmentTypeId = ReportProperty.Current.NumberSet.ToInteger(dtInvestmentType.Rows[0][this.appSchema.FDInvestmentType.INVESTMENT_TYPE_IDColumn.ColumnName].ToString());
                                repcbFDInvestmentType = dtInvestmentType.Rows[0][this.appSchema.FDInvestmentType.INVESTMENT_TYPEColumn.ColumnName].ToString();
                            }
                        }
                    }
                }
            }
            else if (ReportProperty.Current.ReportId == "RPT-062") //On 09/01/2025 for sort order - Receipt side/Payment Side
            {
                repcbRPSortOrder = cmbedit.SelectedIndex;
            }
        }

        private void repositoryItemComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*ComboBoxEdit cmbedit = (ComboBoxEdit)sender;
               ReportProperty.Current.FDRegisterStatus = cmbedit.SelectedIndex;*/
            ComboBoxEdit cmbedit = (ComboBoxEdit)sender;
            if (ReportProperty.Current.ReportId == "RPT-195" || ReportProperty.Current.ReportId == "RPT-196" ||
                ReportProperty.Current.ReportId == "RPT-197" || ReportProperty.Current.ReportId == "RPT-204" || ReportProperty.Current.ReportId == "RPT-211")
            {
                //Audit Related Reports
                repcbAuditAction = string.Empty;
                if (cmbedit.SelectedIndex != 0)
                {
                    repcbAuditAction = cmbedit.Text;
                    //ReportProperty.Current.AuditAction = cmbedit.Text;
                }
            }
            else if (ReportProperty.Current.ReportId == "RPT-047") //FD Scheme
            {   //FD Register 
                repFDScheme = -1;
                repFDSchemeName = string.Empty;
                if (cmbedit.SelectedIndex > 0)
                {
                    repFDScheme = cmbedit.SelectedIndex - 1;
                    repFDSchemeName = cmbedit.Text;
                }
            }

        }

        private void repositoryItemComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*ComboBoxEdit cmbedit = (ComboBoxEdit)sender;
               ReportProperty.Current.FDRegisterStatus = cmbedit.SelectedIndex;*/
            ComboBoxEdit cmbedit = (ComboBoxEdit)sender;
            if (ReportProperty.Current.ReportId == "RPT-194")
            {
                repcbUserName = string.Empty;
                if (cmbedit.SelectedIndex != 0)
                {
                    repcbUserName = cmbedit.Text;
                }
            }
            else if (ReportProperty.Current.ReportId == "RPT-195" || ReportProperty.Current.ReportId == "RPT-211")
            {
                repcbCreatedBy = string.Empty;
                if (cmbedit.SelectedIndex != 0)
                {
                    repcbCreatedBy = cmbedit.Text;
                }
            }
        }


        /// <summary>
        /// Add items to collection of grid controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvReportCriteria_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column == this.colCriteriaType)
            {
                GridEditorItem item = gvReportCriteria.GetRow(e.RowHandle) as GridEditorItem;
                if (item != null)
                {
                    e.RepositoryItem = item.RepositoryItem;
                }
            }
        }

        /// <summary>
        /// Save the settings to 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidReportCriteria())
                {
                    //Save Report Setting Properties
                    gridEditors = gcReportCriteria.DataSource as GridEditorCollection;
                    string ReportCriteria = ReportProperty.Current.ReportCriteria;
                    string[] aReportCriteria = ReportCriteria.Split('ÿ');

                    for (int i = 0; i < aReportCriteria.Length; i++)
                    {
                        if (aReportCriteria[i] == "DA")
                        {
                            ReportProperty.Current.DateAsOn = DateFrom.Text;
                            break;
                        }
                        else
                        {
                            if (aReportCriteria[i] == "DF")
                            {
                                sDateFrom = aReportCriteria[i];
                            }
                            if (aReportCriteria[i] == "DT")
                            {
                                sDateTo = aReportCriteria[i];
                            }
                            if (!string.IsNullOrEmpty(sDateFrom) && !string.IsNullOrEmpty(sDateTo))
                            {
                                if (!string.IsNullOrEmpty(DateFrom.Text.Trim()) && !string.IsNullOrEmpty(DateTo.Text.Trim()))
                                {
                                    if (DateFrom.DateTime > DateTo.DateTime)
                                    {
                                        DateTime dateTo = DateTo.DateTime;
                                        DateTo.DateTime = DateFrom.DateTime;
                                        DateFrom.DateTime = dateTo.Date;
                                        ReportProperty.Current.DateFrom = DateFrom.Text;
                                        ReportProperty.Current.DateTo = DateTo.Text;
                                        break;
                                    }

                                    //On 09/03/2018, For Multi Abstract Reports (Upto One Year we can take report)
                                    int noOfMOnth = MonthDiff(DateFrom.DateTime, DateTo.DateTime);
                                    if (noOfMOnth > 11 && (ReportProperty.Current.ReportId == "RPT-004" || ReportProperty.Current.ReportId == "RPT-005" ||
                                                            ReportProperty.Current.ReportId == "RPT-006" ||
                                                            ReportProperty.Current.ReportId == "RPT-174" || ReportProperty.Current.ReportId == "RPT-175" || ReportProperty.Current.ReportId == "RPT-176"))
                                    {
                                        xtbLocation.SelectedTabPageIndex = 0;
                                        DateTo.Select();
                                        DateTo.Focus();
                                        MessageRender.ShowMessage(MessageRender.GetMessage(MessageCatalog.ReportMessage.MULTI_ABS_YEAR));
                                        return;
                                    }
                                    else if (noOfMOnth > 11 && (ReportProperty.Current.ReportId == "RPT-184" || ReportProperty.Current.ReportId == "RPT-185" || ReportProperty.Current.ReportId == "RPT-186"))
                                    {
                                        MessageRender.ShowMessage(MessageRender.GetMessage(MessageCatalog.ReportMessage.BUDGET_ANN_YEAR));
                                        xtbLocation.SelectedTabPageIndex = 0;
                                        DateTo.Select();
                                        DateTo.Focus();
                                        return;
                                    }
                                    else if (noOfMOnth != 11 && (ReportProperty.Current.ReportId == "RPT-205"))
                                    {
                                        MessageRender.ShowMessage("Fixed Deposit Interest Quarter-wise Register can be taken for one year");
                                        xtbLocation.SelectedTabPageIndex = 0;
                                        DateTo.Select();
                                        DateTo.Focus();
                                        return;
                                    }
                                    else if ((ReportProperty.Current.ReportId == "RPT-185" || ReportProperty.Current.ReportId == "RPT-186" || ReportProperty.Current.ReportId == "RPT-218")
                                            && !CheckDateRangeWithInFY())
                                    {
                                        MessageRender.ShowMessage(MessageRender.GetMessage(MessageCatalog.ReportMessage.DATE_RANGE_CUR_YEAR));
                                        xtbLocation.SelectedTabPageIndex = 0;
                                        DateTo.Select();
                                        DateTo.Focus();
                                        return;
                                    }
                                    else if (ReportProperty.Current.ReportId == "RPT-174" || ReportProperty.Current.ReportId == "RPT-175" || ReportProperty.Current.ReportId == "RPT-176")
                                    { //Check report date range with in Quaters
                                        DateTime dateQuaterFrom = ReportProperty.Current.DateSet.FirstDayOfQuater(DateFrom.DateTime, settinguserProperty.YearFrom, settinguserProperty.YearTo);
                                        DateTime dateQuaterTo = ReportProperty.Current.DateSet.LastDayOfQuater(DateTo.DateTime, settinguserProperty.YearFrom, settinguserProperty.YearTo);

                                        if ((DateFrom.DateTime != dateQuaterFrom) || (dateQuaterTo != DateTo.DateTime))
                                        {
                                            xtbLocation.SelectedTabPageIndex = 0;
                                            DateTo.Select();
                                            DateTo.Focus();
                                            MessageRender.ShowMessage(MessageRender.GetMessage(MessageCatalog.ReportMessage.QUAR_ABS_QUAR));
                                            return;
                                        }
                                        else
                                        {
                                            ReportProperty.Current.DateFrom = DateFrom.Text;
                                            ReportProperty.Current.DateTo = DateTo.Text;
                                        }
                                    }
                                    else
                                    {
                                        ReportProperty.Current.DateFrom = DateFrom.Text;
                                        ReportProperty.Current.DateTo = DateTo.Text;
                                    }
                                    break;
                                }
                            }
                        }
                    }

                    for (int i = 0; i < aReportCriteria.Length; i++)
                    {
                        switch (aReportCriteria[i])
                        {
                            case "PJ":
                                {

                                    string ProjectId = SelectedProject();
                                    if (!string.IsNullOrEmpty(ProjectId))
                                    {
                                        Bosco.Report.Base.ReportProperty.Current.Project = ProjectId;
                                        clsPayrollBase.projectid = SettingProperty.PayrollFinanceEnabled ? ProjectId : string.Empty; ;
                                        Bosco.Report.Base.ReportProperty.Current.SocietyId = societyId;
                                        Bosco.Report.Base.ReportProperty.Current.ITRGroupId = itrgroupId;
                                        if (ReportProperty.Current.ReportId.Equals("RPT-101"))
                                        {
                                            ReportProperty.Current.MultiColumn1LedgerId = glkBankColumn1.EditValue != null ? ReportProperty.Current.NumberSet.ToInteger(glkBankColumn1.EditValue.ToString()) : 0;
                                            ReportProperty.Current.MultiColumn2LedgerId = glkbankColumn2.EditValue != null ? ReportProperty.Current.NumberSet.ToInteger(glkbankColumn2.EditValue.ToString()) : 0;
                                            //object objname = gvMultiColumn1.GetFocusedRowCellValue(gcolMulticolumnbankName);
                                            //ReportProperty.Current.MultiColumn1BankName = objname != null ? objname.ToString() : string.Empty;
                                            //objname = gvMultiColumn2.GetFocusedRowCellValue(gcolMulticolumnbankName);
                                            //ReportProperty.Current.MultiColumn2BankName = objname != null ? objname.ToString() : string.Empty;

                                            object objname = glkBankColumn1.Text;
                                            ReportProperty.Current.MultiColumn1BankName = objname != null && !string.IsNullOrEmpty(objname.ToString())
                                                ? objname.ToString() : string.Empty;
                                            objname = glkbankColumn2.Text;
                                            ReportProperty.Current.MultiColumn2BankName = objname != null && !string.IsNullOrEmpty(objname.ToString())
                                                ? objname.ToString() : string.Empty;
                                        }
                                        else if ((ReportProperty.Current.ReportId.Equals("RPT-164") || ReportProperty.Current.ReportId.Equals("RPT-165"))
                                            && ProjectId.Split(',').Length > 6)
                                        {
                                            //XtraMessageBox.Show("Maximum 6 Projects can be selected for '" + ReportProperty.Current.ReportTitle + "' Report", "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            //xtbLocation.SelectedTabPageIndex = 1;
                                            //return;
                                            MessageRender.ShowMessage(MessageRender.GetMessage(MessageCatalog.ReportMessage.MAX_EXPORT));
                                        }
                                        else if ((ReportProperty.Current.ReportId.Equals("RPT-214") || ReportProperty.Current.ReportId.Equals("RPT-215") || ReportProperty.Current.ReportId.Equals("RPT-216"))
                                                && ProjectId.Split(',').Length > 3)
                                        {
                                            //XtraMessageBox.Show("Maximum 6 Projects can be selected for '" + ReportProperty.Current.ReportTitle + "' Report", "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            //xtbLocation.SelectedTabPageIndex = 1;
                                            //return;
                                            MessageRender.ShowMessage("Maximum 3 Projects can be viewed properly, Change/Fix proper paper size accordingly or Export as Excel file");
                                        }
                                    }
                                    else
                                    {
                                        if (SettingProperty.ReportModuleId == (int)ReportModule.Payroll)
                                        {
                                            if (SettingProperty.PayrollFinanceEnabled)
                                            {
                                                XtraMessageBox.Show(MessageRender.GetMessage(MessageCatalog.ReportMessage.PROJECT_MUST_SEL), "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                xtbLocation.SelectedTabPageIndex = 1;
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            XtraMessageBox.Show(MessageRender.GetMessage(MessageCatalog.ReportMessage.PROJECT_MUST_SEL), "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            xtbLocation.SelectedTabPageIndex = 1;
                                            return;
                                        }
                                    }
                                    break;
                                }
                            case "BK":
                                {
                                    string LedgerId = SelectedBankDetails();

                                    if (!string.IsNullOrEmpty(LedgerId))
                                    {
                                        //On 27/08/2024, To check Cash or bank same currecy should be selected
                                        if (this.settinguserProperty.AllowMultiCurrency == 1)
                                        {
                                            if (ReportProperty.Current.IsMoreThanOneCashBankLedger(LedgerId))
                                            {
                                                MessageRender.ShowMessage("Same Currency Cash/Bank/Investment Ledger(s) should be selected.");
                                                return;
                                            }
                                        }

                                        if (this.BankSelected.Rows.Count > 1
                                            && (ReportProperty.Current.ReportId.Equals("RPT-153") || ReportProperty.Current.ReportId.Equals("RPT-154")))
                                        {
                                            MessageRender.ShowMessage(MessageRender.GetMessage(MessageCatalog.ReportMessage.MORE_BANK_LEDGER));
                                            return;
                                        }
                                        else
                                        {
                                            //05/12/2019, to keep Cash Bank LedgerId
                                            //Bosco.Report.Base.ReportProperty.Current.Ledger = LedgerId; 
                                            Bosco.Report.Base.ReportProperty.Current.CashBankLedger = LedgerId;
                                        }
                                    }
                                    else
                                    {
                                        if (ReportProperty.Current.ReportId == "RPT-094")
                                        {
                                            XtraMessageBox.Show(MessageRender.GetMessage(MessageCatalog.ReportMessage.FD_ACC_SELECTED), "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else if (ReportProperty.Current.ReportId == "RPT-016" || ReportProperty.Current.ReportId == "RPT-038")
                                        {
                                            XtraMessageBox.Show(MessageRender.GetMessage(MessageCatalog.ReportMessage.CL_SELECTED), "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                        {
                                            //ReportProperty.Current.Ledger = "0";
                                            XtraMessageBox.Show(MessageRender.GetMessage(MessageCatalog.ReportMessage.BA_SELECTED), "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        xtbLocation.SelectedTabPageIndex = 1;
                                        return;
                                    }

                                    break;
                                }
                            case "BU":
                                {
                                    string BudgetId = SelectedBudgetDetails();

                                    if (!string.IsNullOrEmpty(BudgetId))
                                    {
                                        if (ReportProperty.Current.ReportId.Equals("RPT-048"))
                                        {
                                            if (ReportProperty.Current.DateFrom.Equals("") && ReportProperty.Current.DateTo.Equals(""))
                                            {
                                                ReportProperty.Current.DateFrom = ReportProperty.Current.DateSet.ToDate(this.settinguserProperty.YearFrom);
                                                ReportProperty.Current.DateTo = ReportProperty.Current.DateSet.ToDate(this.settinguserProperty.YearTo);
                                            }
                                        }
                                        else if ((ReportProperty.Current.ReportId == "RPT-163" && BudgetId.Split(',').Length != 1))
                                        {
                                            XtraMessageBox.Show(MessageRender.GetMessage(MessageCatalog.ReportMessage.BUDGET_MUST_SELECTED), "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            xtbLocation.SelectedTabPageIndex = 22;
                                            return;
                                        }
                                        else if (ReportProperty.Current.ReportId == "RPT-180" && BudgetId.Split(',').Length != 2)
                                        {
                                            XtraMessageBox.Show(MessageRender.GetMessage(MessageCatalog.ReportMessage.BUDGET_MUST_SELECTED), "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            xtbLocation.SelectedTabPageIndex = 22;
                                            return;
                                        }

                                        if (!ReportProperty.Current.ReportId.Equals("RPT-046"))
                                        {
                                            ReportProperty.Current.ProjectTitle = ReportProperty.Current.BudgetProject;
                                        }
                                        ReportProperty.Current.Budget = BudgetId;
                                    }
                                    else
                                    {
                                        XtraMessageBox.Show(MessageRender.GetMessage(MessageCatalog.ReportMessage.ONEBUDGET_SELECTED), "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        xtbLocation.SelectedTabPageIndex = 22;
                                        return;
                                    }

                                    if (settinguserProperty.CreateBudgetDevNewProjects == 0 &&
                                        (ReportProperty.Current.ReportId == "RPT-179" || ReportProperty.Current.ReportId == "RPT-189"))
                                    {
                                        if (gcBudgetNewProject.DataSource != null)
                                        {
                                            DataTable dtBindSource = gcBudgetNewProject.DataSource as DataTable;
                                            dtBindSource.AcceptChanges();
                                            ReportProperty.Current.BudgetNewProjects = dtBindSource;
                                        }
                                    }
                                    break;
                                }
                            case "BB":
                                {
                                    if (glkpBudget.EditValue != null && ReportProperty.Current.NumberSet.ToInteger(glkpBudget.EditValue.ToString()) > 0)
                                    {
                                        if (glkpBudgetCompare.EditValue != null && ReportProperty.Current.NumberSet.ToInteger(glkpBudgetCompare.EditValue.ToString()) > 0)
                                        {
                                            ReportProperty.Current.BudgetId = glkpBudget.EditValue.ToString();
                                            ReportProperty.Current.CompareBudgetId = glkpBudgetCompare.EditValue.ToString();
                                            ReportProperty.Current.BudgetName = String.Format("{0} - {1}", glkpBudget.Text, glkpBudgetCompare.Text);
                                            ReportProperty.Current.BudgetCompareEditValue = ReportProperty.Current.NumberSet.ToInteger(glkpBudgetCompare.EditValue.ToString());
                                            ReportProperty.Current.BudgetEditValue = ReportProperty.Current.NumberSet.ToInteger(glkpBudget.EditValue.ToString());
                                        }
                                        else
                                        {
                                            XtraMessageBox.Show(MessageRender.GetMessage(MessageCatalog.ReportMessage.BUDGET_AVAI_COM), "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;

                                        }
                                    }
                                    else
                                    {
                                        XtraMessageBox.Show(MessageRender.GetMessage(MessageCatalog.ReportMessage.BUDGET_AVAI), "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    break;
                                }
                            case "LG":
                                {
                                    string LedgerId = SelectedLedgerGroupDetails();
                                    if (LedgerId != string.Empty || ReportProperty.Current.ReportId == "RPT-208" || ReportProperty.Current.ReportId == "RPT-041")
                                    {
                                        //On 19/01/2023, Ledger-wise collection(RPT-208) Ledger selection is not mandatory and CC R&P
                                        ReportProperty.Current.Ledger = LedgerId;
                                        string LedgerGroupId = SelectedLedgerGroup();
                                        if (!string.IsNullOrEmpty(LedgerGroupId))
                                        {
                                            ReportProperty.Current.LedgerGroup = LedgerGroupId;
                                        }
                                        else
                                        {
                                            ReportProperty.Current.LedgerGroup = "0";
                                        }
                                    }
                                    //else if (ReportProperty.Current.ReportId == "RPT-167" || ReportProperty.Current.ReportId == "RPT-178") //05/12/2019, for cheque register,ledger is not mandatory
                                    //{
                                    //    Bosco.Report.Base.ReportProperty.Current.Ledger = "";
                                    //    Bosco.Report.Base.ReportProperty.Current.SelectedLedgerName = "";
                                    //}
                                    else
                                    {
                                        XtraMessageBox.Show(MessageRender.GetMessage(MessageCatalog.ReportMessage.LEDGER_SELECTED), "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        xtbLocation.SelectedTabPageIndex = 2;
                                        return;
                                    }
                                    break;
                                }
                            case "CC":
                                {
                                    //On 30/01/2025, Show CC details for all or budget related based on enable CC budget
                                    if (ReportProperty.Current.ReportGroup != "Budget" ||
                                    (ReportProperty.Current.ReportGroup == "Budget" && SettingProperty.Current.EnableCostCentreBudget == 1))
                                    {
                                        string CostCentreId = SelectedCostCentre();
                                        if (!string.IsNullOrEmpty(CostCentreId))
                                        {
                                            ReportProperty.Current.CCCategoryId = CCCategoryId;
                                            ReportProperty.Current.CostCentre = CostCentreId;
                                        }
                                        else
                                        {
                                            XtraMessageBox.Show(MessageRender.GetMessage(MessageCatalog.ReportMessage.CC_SELECTED), "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            // ReportProperty.Current.CostCentre = "0";
                                            xtbLocation.SelectedTabPageIndex = 3;
                                            return;
                                        }
                                    }
                                    break;
                                }
                            case "NN":
                                {
                                    //string NarrationId = SelectedNarration();
                                    //if (!string.IsNullOrEmpty(NarrationId))
                                    //{
                                    //    ReportProperty.Current.Narration = NarrationId;
                                    //}
                                    //else
                                    //{ }
                                    break;
                                }
                            case "PL":
                                {
                                    string PartyLedger = SelectedPartyLedgers();
                                    if (!string.IsNullOrEmpty(PartyLedger))
                                    {
                                        ReportProperty.Current.Ledger = PartyLedger;
                                    }
                                    else
                                    {
                                        //XtraMessageBox.Show(MessageRender.GetMessage(MessageCatalog.ReportMessage.TDS_LEDGER_SELECTED), "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        MessageRender.ShowMessage(MessageRender.GetMessage(MessageCatalog.ReportMessage.LEDGER_SELECTED));
                                        //  ReportProperty.Current.Ledger = "0";
                                        xtbLocation.SelectedTabPageIndex = 4;
                                        return;
                                    }
                                    break;
                                }
                            case "NP":
                                {
                                    //string NatureofPayments = SelectedNarration();
                                    string NatureofPayments = SelectedNatureofPaymentsDetails();
                                    if (!string.IsNullOrEmpty(NatureofPayments))
                                    {
                                        ReportProperty.Current.NatureofPaymets = NatureofPayments;
                                    }
                                    else
                                    {
                                        XtraMessageBox.Show(MessageRender.GetMessage(MessageCatalog.ReportMessage.NP_MUST_SELECTED), "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        // ReportProperty.Current.NatureofPaymets = "0";
                                        xtbLocation.SelectedTabPageIndex = 5;
                                        return;
                                    }
                                    break;
                                }
                            case "DD":
                                {
                                    string DeductorType = SelectedDeductorDetails();
                                    if (!string.IsNullOrEmpty(DeductorType))
                                    {
                                        ReportProperty.Current.DeducteeTypeId = DeductorType;
                                    }
                                    else
                                    {
                                        XtraMessageBox.Show(MessageRender.GetMessage(MessageCatalog.ReportMessage.DED_MUST_SELECTED), "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        // ReportProperty.Current.DeducteeTypeId = "0";
                                        xtbLocation.SelectedTabPageIndex = 6;
                                        return;
                                    }
                                    break;
                                }
                            case "IK":
                                {
                                    if (rboInKind.SelectedIndex != 0)
                                    {
                                        ReportProperty.Current.IncludeInKind = rboInKind.SelectedIndex;
                                    }
                                    else
                                    {
                                        ReportProperty.Current.IncludeInKind = rboInKind.SelectedIndex;
                                    }
                                    break;
                                }
                            case "PY":
                                {
                                    string PayrollGId = SelectedPayrollGroupId();
                                    if (!string.IsNullOrEmpty(PayrollGId))
                                    {
                                        Bosco.Report.Base.ReportProperty.Current.PayrollGroupId = PayrollGId;
                                        Paybase.PAYROLL_Id = ReportProperty.Current.NumberSet.ToInteger(ReportProperty.Current.PayrollId);
                                        clsPayrollBase.PAYROLL_GROUP_ID = PayrollGId;
                                        Bosco.Report.Base.ReportProperty.Current.PayrollName = glkpPayroll.Text;
                                    }
                                    else
                                    {
                                        if (ReportProperty.Current.PayrollId == string.Empty)
                                        {
                                            XtraMessageBox.Show("A Payroll must be selected.", "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else if (PayrollGId == string.Empty)
                                        {
                                            XtraMessageBox.Show("A Payroll Group must be selected.", "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        xtbLocation.SelectedTabPageIndex = 7;
                                        return;
                                    }
                                    break;
                                }
                            case "PC":
                                {
                                    string PayrollComponentIds = SelectedPayrollComponentId();
                                    if (!string.IsNullOrEmpty(PayrollComponentIds))
                                    {
                                        Bosco.Report.Base.ReportProperty.Current.PayrollComponentId = PayrollComponentIds;
                                        Paybase.PAYROLL_Id = ReportProperty.Current.NumberSet.ToInteger(ReportProperty.Current.PayrollId);
                                        clsPayrollBase.PAYROLL_COMPONENT_ID = PayrollComponentIds;
                                    }
                                    else
                                    {
                                        if (PayrollComponentIds == string.Empty)
                                        {
                                            XtraMessageBox.Show("A Payroll Component must be selected.", "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        xtbLocation.SelectedTabPageIndex = 8;
                                        return;
                                    }
                                    break;
                                }
                            case "PS":
                                {
                                    string PayrollStaffIds = SelectedPayrollStaffId();
                                    if (!string.IsNullOrEmpty(PayrollStaffIds))
                                    {
                                        Bosco.Report.Base.ReportProperty.Current.PayrollStaffId = PayrollStaffIds;
                                        Paybase.PAYROLL_Id = ReportProperty.Current.NumberSet.ToInteger(ReportProperty.Current.PayrollId);
                                        clsPayrollBase.PAYROLL_STAFF_ID = PayrollStaffIds;
                                    }
                                    else
                                    {
                                        if (PayrollStaffIds == string.Empty)
                                        {
                                            XtraMessageBox.Show("A Payroll Staff must be selected.", "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        xtbLocation.SelectedTabPageIndex = 8;
                                        return;
                                    }
                                    break;
                                }
                            case "SI":
                                {
                                    ReportProperty.Current.StockItemId = SelectedStockItem();
                                    break;
                                }
                            case "AI":
                                {
                                    //string assetItem = SelectedAssetItem();
                                    //if (assetItem != string.Empty)
                                    //{
                                    //    ReportProperty.Current.AssetItemID = assetItem;
                                    //}
                                    //else
                                    //{
                                    //    XtraMessageBox.Show("An Item must be selected!", "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //    xtbLocation.SelectedTabPageIndex = 9;
                                    //    return;
                                    //}
                                    break;
                                }
                            case "LO":
                                {
                                    string assetLocation = SelectedLocation();
                                    if (assetLocation != string.Empty)
                                    {
                                        ReportProperty.Current.LocationId = assetLocation;
                                    }
                                    else
                                    {
                                        XtraMessageBox.Show(MessageRender.GetMessage(MessageCatalog.ReportMessage.LOCA_SELECTED), "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        xtbLocation.SelectedTabPageIndex = 10;
                                        return;
                                    }
                                    break;
                                }
                            case "REG":
                                {
                                    string ProspectRegistration = RegistrationType();
                                    if (ProspectRegistration != string.Empty)
                                    {
                                        ReportProperty.Current.RegistrationTypeId = ProspectRegistration;
                                    }
                                    else
                                    {
                                        XtraMessageBox.Show(MessageRender.GetMessage(MessageCatalog.ReportMessage.REGISTRATION_SELECTED), "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        xtbLocation.SelectedTabPageIndex = 14;
                                        return;
                                    }
                                    break;
                                }
                            case "COU":
                                {
                                    string SelectedCountry = Country();
                                    if (SelectedCountry != string.Empty)
                                    {
                                        ReportProperty.Current.CountryId = SelectedCountry;
                                    }
                                    else
                                    {
                                        XtraMessageBox.Show(MessageRender.GetMessage(MessageCatalog.ReportMessage.COUNTRY_SELECTED), "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        xtbLocation.SelectedTabPageIndex = 15;
                                        return;
                                    }
                                    break;
                                }
                            case "STE":
                                {
                                    string SelectedState = State();
                                    if (SelectedState != string.Empty)
                                    {
                                        ReportProperty.Current.StateId = SelectedState;
                                    }
                                    else
                                    {
                                        XtraMessageBox.Show(MessageRender.GetMessage(MessageCatalog.ReportMessage.STATE_SELECTED), "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        xtbLocation.SelectedTabPageIndex = 15;
                                        return;
                                    }
                                    break;
                                }
                            case "LAG":
                                {
                                    string SelectedLanguage = Language();
                                    ReportProperty.Current.Language = SelectedLanguage;
                                    xtbLocation.SelectedTabPageIndex = 16;
                                    break;
                                }
                            case "STED":
                                {
                                    string SelectedStateDonaud = StateDonaud();
                                    if (SelectedStateDonaud != string.Empty)
                                    {
                                        ReportProperty.Current.StateDonaud = SelectedStateDonaud;
                                    }
                                    else
                                    {
                                        XtraMessageBox.Show(MessageRender.GetMessage(MessageCatalog.ReportMessage.STATE_SELECTED), "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        xtbLocation.SelectedTabPageIndex = 17;
                                        return;
                                    }
                                    break;
                                }
                            case "DONA":
                                {
                                    string SelectedDonaud = Donaud();
                                    if (SelectedDonaud != string.Empty)
                                    {
                                        ReportProperty.Current.DonarName = SelectedDonaud;
                                    }
                                    else
                                    {
                                        XtraMessageBox.Show(MessageRender.GetMessage(MessageCatalog.ReportMessage.DONOR_SELECTED), "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        xtbLocation.SelectedTabPageIndex = 18;
                                        return;
                                    }
                                    break;
                                }
                            case "TASK":
                                {
                                    ReportProperty.Current.TaskID = ReportProperty.Current.NumberSet.ToInteger(glkpFeestDatTask.EditValue.ToString());
                                    if (ReportProperty.Current.TaskID != 0)
                                    {
                                        ReportProperty.Current.TaskID = ReportProperty.Current.NumberSet.ToInteger(glkpFeestDatTask.EditValue.ToString());
                                    }
                                    else
                                    {
                                        XtraMessageBox.Show("Task name must be selected!", "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        xtbLocation.SelectedTabPageIndex = 19;
                                        return;
                                    }
                                    break;
                                }
                            case "TK":
                                {
                                    string Task = SelectedTask();
                                    if (Task != string.Empty)
                                    {
                                        ReportProperty.Current.SelectedTaskName = Task;
                                    }
                                    else
                                    {
                                        XtraMessageBox.Show("Task must be selected!", "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        xtbLocation.SelectedTabPageIndex = 20;
                                        return;
                                    }
                                    break;
                                }
                            case "BF":
                                {
                                    SelectedBankFDAccountDetails();
                                    //05/12/2019, to keep Cash Bank LedgerId
                                    //string tmpBkId = ReportProperty.Current.Ledger;
                                    string tmpBkId = string.IsNullOrEmpty(ReportProperty.Current.CashBankLedger) ? "0" : ReportProperty.Current.CashBankLedger;
                                    string tmpFDAId = string.IsNullOrEmpty(ReportProperty.Current.FDAccountID) ? "0" : ReportProperty.Current.FDAccountID;
                                    if (tmpBkId == "0" && tmpFDAId == "0")
                                    {
                                        XtraMessageBox.Show(MessageRender.GetMessage(MessageCatalog.ReportMessage.ONEBANK_FD_SELECTED), "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        xtbLocation.SelectedTabPageIndex = 12;
                                        return;
                                    }
                                    else if (settinguserProperty.AllowMultiCurrency == 1)
                                    {
                                        if (ReportProperty.Current.IsMoreThanOneCashBankLedger(tmpBkId))
                                        {
                                            MessageRender.ShowMessage("Same Currency Cash/Bank Ledger(s) should be selected.");
                                            return;
                                        }
                                    }
                                    //On 01/04/2019 for temp puporse for chennai SDBINMVIZ
                                    //else if (ReportProperty.Current.ReportId == "RPT-013") //On 07/02/2018, For BRS report, fix bank account selection single
                                    //{
                                    //    if (tmpBkId.Split(',').Length > 1 || tmpFDAId.Split(',').Length > 1)
                                    //    {
                                    //        XtraMessageBox.Show("One Bank Account or FD Account to be selected", "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //        xtbLocation.SelectedTabPageIndex = 12;
                                    //        return;
                                    //    }
                                    //}
                                    break;
                                }
                            case "CN":
                                {
                                    ReportProperty.Current.DonorCondtionName = cboCondition.SelectedIndex;
                                    ReportProperty.Current.DonorFilterAmount = cboCondition.SelectedText.ToUpper() != "NONE" ? ReportProperty.Current.NumberSet.ToDouble(txtAmount.Text) : 0;
                                    ReportProperty.Current.DonorConditionSymbol = string.Empty;
                                    switch (cboCondition.SelectedItem.ToString())
                                    {
                                        case "Equals":
                                            ReportProperty.Current.DonorConditionSymbol = "=";
                                            break;
                                        case "Does not equal":
                                            ReportProperty.Current.DonorConditionSymbol = "<>";
                                            break;
                                        case "Is greater than":
                                            ReportProperty.Current.DonorConditionSymbol = ">";
                                            break;
                                        case "Is greater than or equal to":
                                            ReportProperty.Current.DonorConditionSymbol = ">=";
                                            break;
                                        case "Is less than":
                                            ReportProperty.Current.DonorConditionSymbol = "<";
                                            break;
                                        case "Is less than or equal to":
                                            ReportProperty.Current.DonorConditionSymbol = "<=";
                                            break;
                                    }
                                    if (cboCondition.SelectedIndex > 0 && string.IsNullOrEmpty(txtAmount.Text))
                                    {
                                        XtraMessageBox.Show(MessageRender.GetMessage(MessageCatalog.ReportMessage.AMOUNT_EMPTY), "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        xtbLocation.SelectedTabPageIndex = 11;
                                        txtAmount.Focus();
                                        return;
                                    }
                                    break;
                                }
                            case "ACL":
                                {

                                    string AssetClassId = SelectedAssetClass();
                                    if (AssetClassId != string.Empty)
                                    {
                                        ReportProperty.Current.Assetclass = AssetClassId;
                                    }
                                    else
                                    {
                                        XtraMessageBox.Show("Asset class must be selected!", "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        xtbLocation.SelectedTabPageIndex = 13;
                                        return;
                                    }
                                    break;
                                }
                            case "SD": //Sing details
                                {
                                    if (chkIncludeSignDetails.Checked)
                                    {
                                        ReportProperty.Current.IncludeSignDetails = 1;
                                    }
                                    else
                                    {
                                        ReportProperty.Current.IncludeSignDetails = 0;
                                    }
                                    break;
                                }
                            case "ASD": //For Auditor Note Sign details
                                {
                                    //For Auditor Sign Note 
                                    ReportProperty.Current.IncludeAuditorSignNote = (chkIncludeAuditorSignNote.Checked ? 1 : 0);
                                    break;
                                }
                            case "NY":
                                {
                                    ReportProperty.Current.NoOfYears = repcbNoOfCompareYears;
                                    break;
                                }
                            case "VT":
                                {
                                    ReportProperty.Current.DayBookVoucherType = repcbDayBookVoucherType;
                                    ReportProperty.Current.DayBookVoucherTypeName = repcbDayBookVoucherTypeName;
                                    break;
                                }
                            case "UN":
                                {
                                    ReportProperty.Current.UserName = repcbUserName;
                                    break;
                                }
                            case "UC": //On 16/09/2021, To retain selected repsotiroy combo values
                                {
                                    ReportProperty.Current.CreatedByName = repcbCreatedBy;
                                    break;
                                }
                            case "UM": //On 16/09/2021, To retain selected repsotiroy combo values
                                {
                                    ReportProperty.Current.ModifiedByName = repcbModifiedBy;
                                    break;
                                }
                            case "AA": //On 16/09/2021, To retain selected repsotiroy combo values
                                {
                                    ReportProperty.Current.AuditAction = repcbAuditAction;
                                    break;
                                }
                            case "VCT": //On 25/01/2021, to retain View Chart Type in Report Settings
                                {
                                    ReportProperty.Current.ChartViewType = repcbSelectedChartType;
                                    break;
                                }
                            case "FDS": //FD Scheme
                                {
                                    ReportProperty.Current.FDScheme = repFDScheme;
                                    ReportProperty.Current.FDSchemeName = repFDSchemeName;
                                    break;
                                }
                            case "FDT": //FD Investment Type
                                {
                                    ReportProperty.Current.FDInvestmentType = repcbFDInvestmentTypeId;
                                    ReportProperty.Current.FDInvestmentTypeName = repcbFDInvestmentType;
                                    break;
                                }
                            case "CR": //Currencies
                                {
                                    ReportProperty.Current.CurrencyCountryId = repcbCountryCurrencyId;
                                    ReportProperty.Current.CurrencyCountry = repcbCountryCurrency;
                                    ReportProperty.Current.CurrencyCountrySymbol = repcbCurrencySymbol;
                                    break;
                                }
                            case "RPS": //Receipt - Payment Order 
                                {
                                    ReportProperty.Current.RandPSortOrder = repcbRPSortOrder;
                                    break;
                                }

                        }
                    }

                    if (gridEditors != null && gridEditors.Count != 0)
                    {
                        for (int i = 0; i < gridEditors.Count; i++)
                        {
                            switch (gridEditors[i].Criteria.Trim())
                            {
                                case "AL": //Attach Values to show all Ledgers
                                    {
                                        ReportProperty.Current.IncludeAllLedger = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "NA": //Attach Values to filter ledger natures
                                    {
                                        /*ReportProperty.Current.LedgerNature = string.Empty;
                                        if (this.repositoryMultiItemComboBox1.Items.Count > 0 && this.repositoryMultiItemComboBox1.Items.GetCheckedValues() != null)
                                        {
                                            List<object> selecteditems = this.repositoryMultiItemComboBox1.Items.GetCheckedValues();
                                            ReportProperty.Current.LedgerNature = string.Join(", ", selecteditems);
                                        }*/
                                        break;
                                    }
                                case "CU": //Attach Values to show all Country
                                    {
                                        ReportProperty.Current.ShowAllCountry = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "OP":
                                    {
                                        ReportProperty.Current.ShowLedgerOpBalance = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "OPAL":
                                    { //As On 25/06/2021, To show only Asset and Liabilities Ledger Opening alone
                                        int value = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        if (value == 1 && ReportProperty.Current.ShowLedgerOpBalance == 1)
                                        {
                                            string msg = MessageRender.GetMessage(MessageCatalog.ReportMessage.OPENING_DISABLED); // "As \"Show Opening Balance (All Nature) Ledgers\" option is enabled, \"Show Opening Balance only for Asset/Liabilities Ledgers\" option will be disabled";
                                            MessageRender.ShowMessage(msg);
                                            ReportProperty.Current.ShowAssetLiabilityLedgerOpBalance = 0;
                                        }
                                        else
                                        {
                                            ReportProperty.Current.ShowAssetLiabilityLedgerOpBalance = value;
                                        }
                                        break;
                                    }
                                case "BL": //Set Values to Show By Ledgers
                                    {
                                        ReportProperty.Current.ShowByLedger = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "BG": // Set Values to Ledger Groups.
                                    {
                                        ReportProperty.Current.ShowByLedgerGroup = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "BC": // Set Values to cost Centre
                                    {
                                        ReportProperty.Current.ShowByCostCentre = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "CT": // Set Values to cost Centre category
                                    {
                                        if (ReportProperty.Current.ReportGroup != "Budget" ||
                                            (ReportProperty.Current.ReportGroup == "Budget" && SettingProperty.Current.EnableCostCentreBudget == 1))
                                        {
                                            ReportProperty.Current.ShowByCostCentreCategory = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        }
                                        break;
                                    }
                                case "LS": // Set Values to Ledger Summary
                                    {
                                        ReportProperty.Current.ShowByLedgerSummary = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "CB": // Set Values to cost Centre Breakup
                                    {
                                        ReportProperty.Current.BreakByCostCentre = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "DN":
                                    {
                                        ReportProperty.Current.BreakbyDonor = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "CL": // Set Values to Ledger Breakup
                                    {
                                        ReportProperty.Current.BreakByLedger = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        // ReportProperty.Current.IncludeLedgerGroupTotal = ReportProperty.Current.IncludeLedgerGroupTotal == 1 ? ReportProperty.Current.IncludeLedgerGroupTotal : ReportProperty.Current.BreakByLedger;
                                        break;
                                    }
                                case "CO": // Set Values to Ledger Breakup
                                    {
                                        ReportProperty.Current.Consolidated = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "SIL": // Show Individual Ledger 
                                    {   //Show all Individual ledgers (Cash, Bank Journal, if same ledger used multi times)
                                        ReportProperty.Current.ShowIndividualLedger = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "AB": // Set Values to Detailed Balance.
                                    {

                                        ReportProperty.Current.ShowDetailedBalance = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "AB1": // Set Values to Detailed Cash Balance.
                                    {
                                        ReportProperty.Current.ShowDetailedCashBalance = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "AB2": // Set Values to Detailed Bank Balance.
                                    {
                                        ReportProperty.Current.ShowDetailedBankBalance = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "AB3": // Set Values to Detailed FD Balance.
                                    {
                                        ReportProperty.Current.ShowDetailedFDBalance = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "ID": // Set Values to Detailed Balance.
                                    {
                                        if (gridEditors[i].Value.ToString() == "True" && ReportProperty.Current.Consolidated == 1)
                                        {
                                            string msg = MessageRender.GetMessage(MessageCatalog.ReportMessage.CONSOLIDATED_OPTION); // "As \"Consolidated\" option is already enabled, You can't set \"Show Detailed\" option";
                                            MessageRender.ShowMessage(msg);
                                            ReportProperty.Current.IncludeDetailed = 0;
                                        }
                                        else
                                        {
                                            ReportProperty.Current.IncludeDetailed = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        }
                                        break;
                                    }
                                case "BA": // Include Bank Account Details. 
                                    {
                                        ReportProperty.Current.IncludeBankAccount = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "DB": //Set Values to Daily Balance.
                                    {
                                        ReportProperty.Current.ShowDailyBalance = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "BD": //Include Bank Details
                                    {
                                        ReportProperty.Current.IncludeBankDetails = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "IK": //Set Values to Include In Kind.
                                    {
                                        ReportProperty.Current.IncludeInKind = rboInKind.SelectedIndex;
                                        break;
                                    }
                                case "IJ": //Set Values to Include Journal
                                    {
                                        ReportProperty.Current.IncludeJournal = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "GT": //Set Values to Group Total
                                    {
                                        ReportProperty.Current.IncludeLedgerGroupTotal = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "NMT": //Set Values to Narration - Month-wise cumulative total
                                    {
                                        ReportProperty.Current.ShowNarrationMonthwiseCumulativeTotal = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);

                                        if (ReportProperty.Current.ShowNarrationMonthwiseCumulativeTotal == 1)
                                        {
                                            string msg = MessageRender.GetMessage(MessageCatalog.ReportMessage.NARRATION_CUM_DISABLED);  // "As \"Show Narration Month-Wise Cumulative Total\" option is enabled, all other options will be disabled (Except Showing Opening Balances)";
                                            MessageRender.ShowMessage(msg);
                                            //ReportProperty.Current.ShowAssetLiabilityLedgerOpBalance = 0;
                                        }

                                        break;
                                    }
                                case "NMB": //Set Values to Narration - Month-wise opening balance
                                    {
                                        ReportProperty.Current.ShowNarrationMonthwiseCumulativeOpBalance = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "AG": //Set Values to Attach Group
                                    {
                                        ReportProperty.Current.IncludeLedgerGroup = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "AC": //Set Values to Attach Cost Centre
                                    {
                                        ReportProperty.Current.IncludeCostCentre = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "MT": //Set Values to Month Wise Total
                                    {
                                        ReportProperty.Current.ShowMonthTotal = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "AD": // Set Values to Donor Address
                                    {
                                        ReportProperty.Current.ShowDonorAddress = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "CD": // Set Values to Donor Category
                                    {
                                        break;
                                    }
                                case "IN": //include Narration
                                    {
                                        ReportProperty.Current.IncludeNarration = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "RN": //include Narration WITH REF NO
                                    {
                                        ReportProperty.Current.IncludeNarrationwithRefNo = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "NPG":
                                    {
                                        ReportProperty.Current.IncludePanwithGSTNo = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }

                                case "INC": //include Currency details wtih Narration
                                    {
                                        ReportProperty.Current.IncludeNarrationwithCurrencyDetails = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "ND": //include Narration WITH NAME AND ADDRESS
                                    {
                                        ReportProperty.Current.IncludeNarrationwithNameAddress = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "IS": //include Outside Donors
                                    {
                                        ReportProperty.Current.IncludeOutsideParishioner = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "ML": //include Male
                                    {
                                        ReportProperty.Current.IncludeMale = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "FL": //include Female
                                    {
                                        ReportProperty.Current.IncludeFemale = (gridEditors[i].Value.ToString() == "True" ? 2 : 0);
                                        break;
                                    }
                                case "SENT": //include Thanksgiving Sent
                                    {
                                        ReportProperty.Current.IncludeSent = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);

                                        break;
                                    }
                                case "NSENT": //include Thanksgiving Not Sent
                                    {
                                        ReportProperty.Current.IncludeNotSent = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "INST": //include Institutional
                                    {
                                        ReportProperty.Current.IncludeInstitutional = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "IND": //include Individual
                                    {
                                        ReportProperty.Current.IncludeIndividual = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "IP": //include all Purposes
                                    {
                                        ReportProperty.Current.IncludeAllPurposes = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "TBO":
                                    {
                                        ReportProperty.Current.ShowOpeningBalance = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "TBT":
                                    {
                                        ReportProperty.Current.ShowCurrentTransaction = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "TBC":
                                    {
                                        ReportProperty.Current.ShowClosingBalance = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                        //if (ReportProperty.Current.ShowOpeningBalance == 0 && ReportProperty.Current.ShowCurrentTransaction == 0 && ReportProperty.Current.ShowClosingBalance == 0)
                                        //{
                                        //    XtraMessageBox.Show("At least one 'Show Opening Balance,Current Transaction and Closing Balance' option has to be selected", "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //    xtbLocation.SelectedTabPageIndex = 0;
                                        //    return;
                                        //}
                                    }
                                case "DG":
                                    {
                                        ReportProperty.Current.ShowByDonorGroup = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "SB":
                                    {
                                        ReportProperty.Current.ShowByBank = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "SV":
                                    {
                                        ReportProperty.Current.ShowByInvestment = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "SRC": //Show only Receipts
                                    {
                                        ReportProperty.Current.ShowOnlyReceipts = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "ECW": //Exculde Cash Withdrawal
                                    {
                                        ReportProperty.Current.ExcludeCashWithdrawal = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "SPY": //Show only Receipts
                                    {
                                        ReportProperty.Current.ShowOnlyPayments = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "ECD": //Exculde Cash Deposit
                                    {
                                        ReportProperty.Current.ExcludeCashDeposit = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "CIP":
                                    {
                                        ReportProperty.Current.ChartInPercentage = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "SAL": //On 22/02/2021, whether to show all against ledger names or show only top of the agaist ledger
                                    {
                                        ReportProperty.Current.ShowAllAgainstLedgers = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "SCD": //On 22/02/2021, whether to show CC details
                                    {
                                        if (ReportProperty.Current.ReportGroup != "Budget" ||
                                            (ReportProperty.Current.ReportGroup == "Budget" && SettingProperty.Current.EnableCostCentreBudget == 1))
                                        {
                                            ReportProperty.Current.ShowCCDetails = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        }
                                        break;
                                    }
                                case "SDD": //On 19/05/2023, whether to show Donor details
                                    {
                                        ReportProperty.Current.ShowDonorDetails = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "LCD": //On 19/01/2023, whether to show Ledger-wise CC details
                                    {
                                        ReportProperty.Current.ShowLedgerwiseCCDetails = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        ReportProperty.Current.ShowCCDetails = ReportProperty.Current.ShowLedgerwiseCCDetails;

                                        if (ReportProperty.Current.ShowLedgerwiseCCDetails == 1 &&
                                            (ReportProperty.Current.ShowByCostCentre == 1 || ReportProperty.Current.ShowByCostCentreCategory == 1))
                                        {
                                            ReportProperty.Current.ShowByCostCentre = 0;
                                            ReportProperty.Current.ShowByCostCentreCategory = 0;
                                            string msg = MessageRender.GetMessage(MessageCatalog.ReportMessage.LCC_DISABLED); // "As \"Show Ledger-wise Cost Centre Details\" option is enabled, \"Show By Cost Centre\" and \"Show By Cost Centre Category\" will be disabled";
                                            MessageRender.ShowMessage(msg);
                                        }
                                        break;
                                    }
                                case "SS": //Set Values to Show by Society
                                    {
                                        ReportProperty.Current.ShowBySociety = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "SP": //Set Values to Show by Project
                                    {
                                        ReportProperty.Current.ShowByProject = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "SBG": //Set Values to Show by Budget Groyp
                                    {
                                        ReportProperty.Current.ShowByBudgetGroup = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "SGV":
                                    {
                                        ReportProperty.Current.ShowGSTVouchers = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "SGI":
                                    {
                                        ReportProperty.Current.ShowGSTInvoiceVouchers = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "SDL":
                                    {
                                        ReportProperty.Current.ShowIndividualDepreciationLedgers = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "SFDV":
                                    {
                                        ReportProperty.Current.ShowFixedDepositVoucherDetail = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "HCN": //Hide Contra Note On 14/03/2024, Hide contra note
                                    {
                                        ReportProperty.Current.HideContraNote = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "HLN": //Hide Contra Note On 02/07/2024, Hide Ledger Name
                                    {
                                        ReportProperty.Current.HideLedgerName = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "SCO":
                                    {
                                        ReportProperty.Current.ShowCash = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "SBO":
                                    {
                                        ReportProperty.Current.ShowBank = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "FDSI": //Include FD Simple Interest 
                                    {
                                        ReportProperty.Current.IncludeFDSimpleInterest = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "FDAI": //Include FD Accumulated Interest 
                                    {
                                        ReportProperty.Current.IncludeFDAccumulatedInterest = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }
                                case "EEX": //Average Euro Exchange Rate for local 
                                    {
                                        ReportProperty.Current.AvgEuroExchangeRate = EuroExhangeRate;
                                        break;
                                    }
                                case "DEEX": //Average Euro Exchange Rate for Dollar 
                                    {
                                        ReportProperty.Current.AvgEuroDollarExchangeRate = EuroDollarExhangeRate;
                                        break;
                                    }
                                case "FRXD":
                                    {
                                        ReportProperty.Current.ShowForexDetail = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                        break;
                                    }

                                //case "BBL": //Break up by location
                                //    {
                                //        ReportProperty.Current.ShowByLocation = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                //        break;
                                //    }
                                //case "BBG": //Break up by Group
                                //    {
                                //        ReportProperty.Current.ShowByGroup = (gridEditors[i].Value.ToString() == "True" ? 1 : 0);
                                //        break;
                                //    }
                            }
                        }
                    }
                    //On 03/02/2023, to Retain Page Margina and Page Setting ------------------
                    // Have to get from report
                    //-------------------------------------------------------------------------

                    SaveReportSetup();
                    ReportProperty.Current.SaveReportSetting();
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;

                    if (success)
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        /// <summary>
        /// This is to close the form 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Enable table based on the selected values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtbReportCriteria_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            try
            {
                chkBudget.Visible = false;
                if (xtbLocation.SelectedTabPageIndex == 0)
                {
                    chkProject.Visible = chkCountry.Visible = chkStateDonaud.Visible = chkDonaud.Visible = chkState.Visible = chkLanguage.Visible = chkRegistrationType.Visible = chkBank.Visible = chkItems.Visible = chkLedger.Visible = chkLedgerGroup.Visible = chkPayrollComponents.Visible = chkLocationSelectAll.Visible = chkCostCentre.Visible = chkPartyLedger.Visible = chkNatureofPayments.Visible = chkDeducteeType.Visible = chkPayroll.Visible = chkPayrollStaff.Visible = chkselectAllAssetclass.Visible = false;
                    chkSelectAllTask.Visible = false;
                    DateFrom.Focus();
                }
                else if (xtbLocation.SelectedTabPageIndex == 1)
                {
                    chkBank.Visible = chkCountry.Visible = chkStateDonaud.Visible = chkDonaud.Visible = chkState.Visible = chkLanguage.Visible = chkLedger.Visible = chkRegistrationType.Visible = chkItems.Visible = chkLedgerGroup.Visible = chkLocationSelectAll.Visible = chkCostCentre.Visible = chkPayrollComponents.Visible = chkPartyLedger.Visible = chkNatureofPayments.Visible = chkDeducteeType.Visible = chkPayroll.Visible = chkPayrollStaff.Visible = chkselectAllAssetclass.Visible = false;
                    chkProject.Visible = true;
                    chkSelectAllTask.Visible = false;
                    EnableCheckBox();
                    gvProject.Focus();
                }
                else if (xtbLocation.SelectedTabPageIndex == 2)
                {
                    chkProject.Visible = chkCountry.Visible = chkStateDonaud.Visible = chkDonaud.Visible = chkState.Visible = chkLanguage.Visible = chkBank.Visible = chkRegistrationType.Visible = chkItems.Visible = chkLocationSelectAll.Visible = chkCostCentre.Visible = chkPartyLedger.Visible = chkPayrollComponents.Visible = chkNatureofPayments.Visible = chkDeducteeType.Visible = chkPayroll.Visible = chkPayrollStaff.Visible = chkselectAllAssetclass.Visible = false;
                    chkSelectAllTask.Visible = false;
                    chkLedger.Visible = chkLedgerGroup.Visible = true;
                    gvLedger.Focus();
                    lcGrpLedger.Text = "Ledger";
                    chkLedger.Left = lcGrpLedger.Location.X + 28;
                }
                else if (xtbLocation.SelectedTabPageIndex == 3)
                {
                    chkProject.Visible = chkCountry.Visible = chkStateDonaud.Visible = chkDonaud.Visible = chkState.Visible = chkLanguage.Visible = chkBank.Visible = chkRegistrationType.Visible = chkItems.Visible = chkLocationSelectAll.Visible = chkLedger.Visible = chkLedgerGroup.Visible = chkPayrollComponents.Visible = chkPartyLedger.Visible = chkNatureofPayments.Visible = chkDeducteeType.Visible = chkPayroll.Visible = chkPayrollStaff.Visible = chkselectAllAssetclass.Visible = false;
                    chkCostCentre.Visible = true;
                    chkSelectAllTask.Visible = false;
                    gvCostCentre.Focus();
                }
                else if (xtbLocation.SelectedTabPageIndex == 4)
                {
                    chkProject.Visible = chkCountry.Visible = chkStateDonaud.Visible = chkDonaud.Visible = chkBank.Visible = chkLanguage.Visible = chkRegistrationType.Visible = chkItems.Visible = chkLocationSelectAll.Visible = chkLedger.Visible = chkLedgerGroup.Visible = chkPayrollComponents.Visible = chkCostCentre.Visible = chkNatureofPayments.Visible = chkPartyLedger.Visible = chkDeducteeType.Visible = chkPayroll.Visible = chkPayrollStaff.Visible = chkselectAllAssetclass.Visible = false;
                    chkSelectAllTask.Visible = false;
                    chkPartyLedger.Visible = true;
                    gvNarration.Focus();
                }
                else if (xtbLocation.SelectedTabPageIndex == 5)
                {
                    chkProject.Visible = chkCountry.Visible = chkStateDonaud.Visible = chkDonaud.Visible = chkState.Visible = chkLanguage.Visible = chkBank.Visible = chkRegistrationType.Visible = chkItems.Visible = chkPartyLedger.Visible = chkLedger.Visible = chkLocationSelectAll.Visible = chkLedgerGroup.Visible = chkPayrollComponents.Visible = chkCostCentre.Visible = chkNatureofPayments.Visible = chkDeducteeType.Visible = chkPayroll.Visible = chkPayrollStaff.Visible = chkselectAllAssetclass.Visible = false;
                    chkSelectAllTask.Visible = false;
                    chkNatureofPayments.Visible = true;
                    cboSortByLedger.Focus();
                    CheckLegalEntity();
                }
                else if (xtbLocation.SelectedTabPageIndex == 6)
                {
                    chkProject.Visible = chkCountry.Visible = chkStateDonaud.Visible = chkDonaud.Visible = chkState.Visible = chkLanguage.Visible = chkNatureofPayments.Visible = chkRegistrationType.Visible = chkItems.Visible = chkBank.Visible = chkLedger.Visible = chkLedgerGroup.Visible = chkPayrollComponents.Visible = chkCostCentre.Visible = chkPartyLedger.Visible = chkDeducteeType.Visible = chkPayroll.Visible = chkPayrollStaff.Visible = chkselectAllAssetclass.Visible = false;
                    chkSelectAllTask.Visible = false;
                    chkDeducteeType.Visible = true;
                    chkLocationSelectAll.Visible = false;
                    gvNarration.Focus();
                }
                else if (xtbLocation.SelectedTabPageIndex == 7)
                {
                    chkProject.Visible = chkCountry.Visible = chkStateDonaud.Visible = chkDonaud.Visible = chkState.Visible = chkLanguage.Visible = chkBank.Visible = chkRegistrationType.Visible = chkLocationSelectAll.Visible = chkLedger.Visible = chkItems.Visible = chkLedgerGroup.Visible = chkPayrollComponents.Visible = chkCostCentre.Visible = chkPartyLedger.Visible = chkNatureofPayments.Visible = chkPayroll.Visible = chkPayrollStaff.Visible = chkselectAllAssetclass.Visible = false;
                    chkSelectAllTask.Visible = false;
                    chkPayroll.Visible = true;
                    gvDeducteeType.Focus();
                }
                else if (xtbLocation.SelectedTabPageIndex == 8)
                {
                    chkProject.Visible = chkCountry.Visible = chkStateDonaud.Visible = chkDonaud.Visible = chkState.Visible = chkLanguage.Visible = chkPayroll.Visible = chkRegistrationType.Visible = chkBank.Visible = chkItems.Visible = chkLocationSelectAll.Visible = chkLedger.Visible = chkLedgerGroup.Visible = chkPayrollComponents.Visible = chkCostCentre.Visible = chkPartyLedger.Visible = chkNatureofPayments.Visible = chkDeducteeType.Visible = chkPayrollStaff.Visible = chkselectAllAssetclass.Visible = false;
                    chkSelectAllTask.Visible = false;
                    chkPayrollStaff.Visible = true;
                    //if (ReportProperty.Current.ReportId != "RPT-074")
                    //{
                    //    chkPayrollComponents.Visible = true;
                    //}
                    //else
                    //{
                    //    chkPayrollStaff.Visible = chkPayrollComponents.Visible = true;
                    //}
                    if (ReportProperty.Current.ReportId == "RPT-168")
                    {
                        chkPayrollStaff.Left = gcPayStaff.Left + 15;
                        chkPayrollStaff.Visible = true;
                        chkPayrollComponents.Visible = false;
                    }
                    else
                    {
                        chkPayrollStaff.Visible = chkPayrollComponents.Visible = true;
                    }

                    SetComponentStaffSource();
                    SetStaffSource();
                }
                else if (xtbLocation.SelectedTabPageIndex == 9)
                {
                    chkProject.Visible = chkCountry.Visible = chkStateDonaud.Visible = chkDonaud.Visible = chkState.Visible = chkLanguage.Visible = chkBank.Visible = chkRegistrationType.Visible = chkPayrollStaff.Visible = chkPayrollComponents.Visible = chkLedger.Visible = chkLedgerGroup.Visible = chkPayrollComponents.Visible = chkCostCentre.Visible = chkPartyLedger.Visible = chkNatureofPayments.Visible = chkDeducteeType.Visible = chkPayroll.Visible = chkLocationSelectAll.Visible = chkselectAllAssetclass.Visible = false;
                    chkSelectAllTask.Visible = false;
                    chkItems.Visible = true;
                    chkLocationSelectAll.Visible = false;
                }
                else if (xtbLocation.SelectedTabPageIndex == 10)
                {
                    chkProject.Visible = chkCountry.Visible = chkStateDonaud.Visible = chkDonaud.Visible = chkState.Visible = chkLanguage.Visible = chkBank.Visible = chkRegistrationType.Visible = chkPayrollStaff.Visible = chkItems.Visible = chkPayrollComponents.Visible = chkLedger.Visible = chkLedgerGroup.Visible = chkPayrollComponents.Visible = chkCostCentre.Visible = chkPartyLedger.Visible = chkNatureofPayments.Visible = chkDeducteeType.Visible = chkPayroll.Visible = chkselectAllAssetclass.Visible = false;
                    chkSelectAllTask.Visible = false;
                    chkLocationSelectAll.Visible = true;
                }

                else if (xtbLocation.SelectedTabPageIndex == 11)
                {
                    chkProject.Visible = chkCountry.Visible = chkStateDonaud.Visible = chkDonaud.Visible = chkState.Visible = chkLanguage.Visible = chkBank.Visible = chkRegistrationType.Visible = chkPayrollStaff.Visible = chkItems.Visible = chkPayrollComponents.Visible = chkLedger.Visible = chkLedgerGroup.Visible = chkPayrollComponents.Visible = chkCostCentre.Visible = chkPartyLedger.Visible = chkNatureofPayments.Visible = chkDeducteeType.Visible = chkPayroll.Visible = chkLocationSelectAll.Visible = chkselectAllAssetclass.Visible = false;
                    chkSelectAllTask.Visible = false;
                    if (txtAmount.Visible) txtAmount.Focus();
                }
                else if (xtbLocation.SelectedTabPageIndex == 12)
                {
                    chkProject.Visible = chkCountry.Visible = chkStateDonaud.Visible = chkDonaud.Visible = chkState.Visible = chkLanguage.Visible = chkselectAllAssetclass.Visible = chkRegistrationType.Visible = false;
                    chkSelectAllTask.Visible = false;
                    AssignBankFDAccountDetails();
                }
                else if (xtbLocation.SelectedTabPageIndex == 13)
                {
                    chkProject.Visible = chkCountry.Visible = chkStateDonaud.Visible = chkDonaud.Visible = chkState.Visible = chkLanguage.Visible = chkBank.Visible = chkRegistrationType.Visible = chkLedger.Visible = chkItems.Visible = chkLedgerGroup.Visible = chkLocationSelectAll.Visible = chkCostCentre.Visible = chkPayrollComponents.Visible = chkPartyLedger.Visible = chkNatureofPayments.Visible = chkDeducteeType.Visible = chkPayroll.Visible = chkPayrollStaff.Visible = false;
                    chkSelectAllTask.Visible = false;
                    chkselectAllAssetclass.Visible = true;
                    gvAssetClass.Focus();
                }
                else if (xtbLocation.SelectedTabPageIndex == 14)
                {
                    chkProject.Visible = chkBank.Visible = chkStateDonaud.Visible = chkDonaud.Visible = chkState.Visible = chkLanguage.Visible = chkCountry.Visible = chkPayrollStaff.Visible = chkItems.Visible = chkPayrollComponents.Visible = chkLedger.Visible = chkLedgerGroup.Visible = chkPayrollComponents.Visible = chkCostCentre.Visible = chkPartyLedger.Visible = chkNatureofPayments.Visible = chkDeducteeType.Visible = chkPayroll.Visible = chkselectAllAssetclass.Visible = false;
                    chkSelectAllTask.Visible = false;
                    chkLocationSelectAll.Visible = false;
                    chkRegistrationType.Visible = true;
                }
                else if (xtbLocation.SelectedTabPageIndex == 15)
                {
                    chkProject.Visible = chkBank.Visible = chkStateDonaud.Visible = chkDonaud.Visible = chkPayrollStaff.Visible = chkLanguage.Visible = chkItems.Visible = chkPayrollComponents.Visible = chkLedger.Visible = chkLedgerGroup.Visible = chkPayrollComponents.Visible = chkCostCentre.Visible = chkPartyLedger.Visible = chkNatureofPayments.Visible = chkDeducteeType.Visible = chkPayroll.Visible = chkselectAllAssetclass.Visible = false;
                    chkSelectAllTask.Visible = false;
                    chkLocationSelectAll.Visible = false;
                    chkCountry.Visible = chkState.Visible = true;

                }
                else if (xtbLocation.SelectedTabPageIndex == 16)
                {
                    chkProject.Visible = chkBank.Visible = chkStateDonaud.Visible = chkDonaud.Visible = chkPayrollStaff.Visible = chkCountry.Visible = chkState.Visible = chkItems.Visible = chkPayrollComponents.Visible = chkLedger.Visible = chkLedgerGroup.Visible = chkPayrollComponents.Visible = chkCostCentre.Visible = chkPartyLedger.Visible = chkNatureofPayments.Visible = chkDeducteeType.Visible = chkPayroll.Visible = chkselectAllAssetclass.Visible = false;
                    chkSelectAllTask.Visible = false;
                    chkLocationSelectAll.Visible = false;
                    chkLanguage.Visible = true;

                }
                else if (xtbLocation.SelectedTabPageIndex == 17)
                {
                    chkProject.Visible = chkBank.Visible = chkState.Visible = chkDonaud.Visible = chkLanguage.Visible = chkCountry.Visible = chkPayrollStaff.Visible = chkItems.Visible = chkPayrollComponents.Visible = chkLedger.Visible = chkLedgerGroup.Visible = chkPayrollComponents.Visible = chkCostCentre.Visible = chkPartyLedger.Visible = chkNatureofPayments.Visible = chkDeducteeType.Visible = chkPayroll.Visible = chkselectAllAssetclass.Visible = false;
                    chkSelectAllTask.Visible = false;
                    chkLocationSelectAll.Visible = chkRegistrationType.Visible = false;
                    chkStateDonaud.Visible = true;
                }
                else if (xtbLocation.SelectedTabPageIndex == 18)
                {
                    chkProject.Visible = chkBank.Visible = chkStateDonaud.Visible = chkRegistrationType.Visible = chkState.Visible = chkLanguage.Visible = chkCountry.Visible = chkPayrollStaff.Visible = chkItems.Visible = chkPayrollComponents.Visible = chkLedger.Visible = chkLedgerGroup.Visible = chkPayrollComponents.Visible = chkCostCentre.Visible = chkPartyLedger.Visible = chkNatureofPayments.Visible = chkDeducteeType.Visible = chkPayroll.Visible = chkselectAllAssetclass.Visible = false;
                    chkSelectAllTask.Visible = false;
                    chkLocationSelectAll.Visible = false;
                    chkDonaud.Visible = true;
                }
                else if (xtbLocation.SelectedTabPageIndex == 19)
                {
                    chkProject.Visible = chkBank.Visible = chkState.Visible = chkDonaud.Visible = chkStateDonaud.Visible = chkRegistrationType.Visible = chkCountry.Visible = chkLanguage.Visible = chkPayrollStaff.Visible = chkItems.Visible = chkPayrollComponents.Visible = chkLedger.Visible = chkLedgerGroup.Visible = chkPayrollComponents.Visible = chkCostCentre.Visible = chkPartyLedger.Visible = chkNatureofPayments.Visible = chkDeducteeType.Visible = chkPayroll.Visible = chkselectAllAssetclass.Visible = false;
                    chkSelectAllTask.Visible = false;
                    chkLocationSelectAll.Visible = false;

                }
                else if (xtbLocation.SelectedTabPageIndex == 20)
                {
                    chkProject.Visible = chkBank.Visible = chkState.Visible = chkDonaud.Visible = chkStateDonaud.Visible = chkRegistrationType.Visible = chkCountry.Visible = chkLanguage.Visible = chkPayrollStaff.Visible = chkItems.Visible = chkPayrollComponents.Visible = chkLedger.Visible = chkLedgerGroup.Visible = chkPayrollComponents.Visible = chkCostCentre.Visible = chkPartyLedger.Visible = chkNatureofPayments.Visible = chkDeducteeType.Visible = chkPayroll.Visible = chkselectAllAssetclass.Visible = false;
                    chkSelectAllTask.Visible = true;
                    chkLocationSelectAll.Visible = false;

                }
                else if (xtbLocation.SelectedTabPageIndex == 21)
                {
                    chkProject.Visible = chkBank.Visible = chkState.Visible = chkDonaud.Visible = chkStateDonaud.Visible = chkRegistrationType.Visible = chkCountry.Visible = chkLanguage.Visible = chkPayrollStaff.Visible = chkItems.Visible = chkPayrollComponents.Visible = chkLedger.Visible = chkLedgerGroup.Visible = chkPayrollComponents.Visible = chkCostCentre.Visible = chkPartyLedger.Visible = chkNatureofPayments.Visible = chkDeducteeType.Visible = chkPayroll.Visible = chkselectAllAssetclass.Visible = false;
                    chkSelectAllTask.Visible = false;
                    chkLocationSelectAll.Visible = false;
                    //   EnableCheckBox();
                }
                else if (xtbLocation.SelectedTabPageIndex == 22) //For Budget tab
                {
                    chkBudget.Visible = true;
                    chkProject.Visible = chkBank.Visible = chkState.Visible = chkDonaud.Visible = chkStateDonaud.Visible = chkRegistrationType.Visible = chkCountry.Visible = chkLanguage.Visible = chkPayrollStaff.Visible = chkItems.Visible = chkPayrollComponents.Visible = chkLedger.Visible = chkLedgerGroup.Visible = chkPayrollComponents.Visible = chkCostCentre.Visible = chkPartyLedger.Visible = chkNatureofPayments.Visible = chkDeducteeType.Visible = chkPayroll.Visible = chkSelectAllTask.Visible = chkselectAllAssetclass.Visible = false;

                    if (settinguserProperty.CreateBudgetDevNewProjects == 0 &&
                        (ReportProperty.Current.ReportId == "RPT-179" || ReportProperty.Current.ReportId == "RPT-189"))
                    {
                        FocusBudgetNewProjectGrid();
                    }
                }
                else if (xtbLocation.SelectedTabPageIndex == 23) //25/07/2017, For Standard Report (only Report setup tabe)
                {
                    chkProject.Visible = chkBank.Visible = chkState.Visible = chkDonaud.Visible = chkStateDonaud.Visible = chkRegistrationType.Visible = chkCountry.Visible = chkLanguage.Visible = chkPayrollStaff.Visible = chkItems.Visible = chkPayrollComponents.Visible = chkLedger.Visible = chkLedgerGroup.Visible = chkPayrollComponents.Visible = chkCostCentre.Visible = chkPartyLedger.Visible = chkNatureofPayments.Visible = chkDeducteeType.Visible = chkPayroll.Visible = chkselectAllAssetclass.Visible = false;
                    chkSelectAllTask.Visible = false;
                    chkLocationSelectAll.Visible = false;

                    //On 15/10/2019, Reset Title-----------------------
                    CheckLegalEntity();
                    //--------------------------------------------------------------------------------------
                }
                else if (xtbLocation.SelectedTabPageIndex == 24) //03/03/2021, For Sign details
                {
                    chkProject.Visible = chkBank.Visible = chkState.Visible = chkDonaud.Visible = chkStateDonaud.Visible = chkRegistrationType.Visible = chkCountry.Visible = chkLanguage.Visible = chkPayrollStaff.Visible = chkItems.Visible = chkPayrollComponents.Visible = chkLedger.Visible = chkLedgerGroup.Visible = chkPayrollComponents.Visible = chkCostCentre.Visible = chkPartyLedger.Visible = chkNatureofPayments.Visible = chkDeducteeType.Visible = chkPayroll.Visible = chkselectAllAssetclass.Visible = false;
                    chkSelectAllTask.Visible = false;
                    chkLocationSelectAll.Visible = false;
                }


                if (ReportProperty.Current.ReportId == "RPT-228")
                {
                    lcSociety.Visibility = LayoutVisibility.Never;
                    lciITRGroup.Visibility = LayoutVisibility.Always;
                }
                else
                {
                    lcSociety.Visibility = LayoutVisibility.Always;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        /// <summary>
        /// Enable tab based on the properties
        /// </summary>
        private void EnableCheckBox()
        {
            string reportCriteria = ReportProperty.Current.ReportCriteria;
            string[] criteria = reportCriteria.Split('ÿ');
            for (int i = 0; i < criteria.Length; i++)
            {
                switch (criteria[i])
                {
                    case "PJ":
                        {
                            chkProject.Visible = true;
                            break;
                        }
                    case "BK":
                        {
                            //Commanded By chinna On 03.01.2017
                            //chkBank.Visible = (ReportProperty.Current.ReportId == "RPT-094") ? true : true; // FD History Select All to be Hidden

                            //On 10/05/2018, for Cash/Bank Receipts/Payments, only one bank selection and it should load cash or bank ledgers
                            if (ReportProperty.Current.ReportId == "RPT-153" || ReportProperty.Current.ReportId == "RPT-154")
                            {
                                lcgBankAccount.Text = "Bank/Cash Account";
                                chkBank.Visible = false;
                            }
                            else
                            {
                                chkBank.Visible = true;
                                chkBank.Left = gcBank.Left + 12;
                            }
                            break;
                        }
                }
            }
        }
        /// <summary>
        /// Assign Report Date.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportDate_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)
                {
                    if (Convert.ToBoolean(ReportDate.Tag))
                    {
                        ReportDate.Properties.Buttons[1].Image = imgCollection.Images[0];
                        ReportDate.Tag = false;
                        ReportProperty.Current.ReportDate = "";
                        ReportDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                    }
                    else
                    {
                        ReportDate.Properties.Buttons[1].Image = imgCollection.Images[1];
                        ReportDate.Tag = true;
                        ReportDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                        ReportProperty.Current.ReportDate = ReportDate.Text;
                        ReportDate.Properties.Appearance.BackColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        public void CheckLegalEntity()
        {
            int legalCount = 0;
            string Pid = string.IsNullOrEmpty(SelectedProject()) ? "0" : SelectedProject();
            if (Pid != "0")
            {
                using (DataManager dataManager = new DataManager(SQLCommand.LegalEntity.CheckLegalEntity))
                {
                    dataManager.Parameters.Add(this.appSchema.Project.PROJECT_IDColumn, Pid);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                    DataTable dtres = resultArgs.DataSource.Table;
                    legalCount = (dtres.Rows.Count == 0) ? 0 : (dtres.Rows.Count == 1) ? 1 : 2;
                    if (legalCount == 1 && dtres != null)
                    {
                        if (!string.IsNullOrEmpty(dtres.Rows[0]["CUSTOMERID"].ToString()))
                        {
                            rgbReportTitle.Properties.Items[1].Enabled = true;
                            rgbAddress.Properties.Items[1].Enabled = true;
                            rgbReportTitle.SelectedIndex = ReportProperty.Current.HeaderInstituteSocietyName == 0 ? 0 : 1;
                            rgbAddress.SelectedIndex = ReportProperty.Current.HeaderInstituteSocietyAddress == 0 ? 0 : 1;

                        }
                        else
                        {
                            rgbReportTitle.Properties.Items[1].Enabled = false;
                            rgbAddress.Properties.Items[1].Enabled = false;
                            rgbAddress.SelectedIndex = rgbReportTitle.SelectedIndex = 0;

                        }
                    }
                    else if (legalCount == 0)
                    {
                        rgbReportTitle.Properties.Items[1].Enabled = false;
                        rgbAddress.Properties.Items[1].Enabled = false;
                        rgbAddress.SelectedIndex = rgbReportTitle.SelectedIndex = 0;
                    }
                    else if (legalCount >= 2 && dtres != null)
                    {
                        rgbReportTitle.Properties.Items[1].Enabled = false;
                        rgbAddress.Properties.Items[1].Enabled = false;
                        rgbAddress.SelectedIndex = rgbReportTitle.SelectedIndex = 0;
                    }
                }
            }
            else
            {
                rgbReportTitle.Properties.Items[1].Enabled = true;
                rgbAddress.Properties.Items[1].Enabled = true;
                rgbReportTitle.SelectedIndex = 1;

                //On 17/09/2019
                rgbReportTitle.SelectedIndex = ReportProperty.Current.HeaderInstituteSocietyName == 0 ? 0 : 1;
                rgbAddress.SelectedIndex = ReportProperty.Current.HeaderInstituteSocietyAddress == 0 ? 0 : 1;
            }
            //rgbAddress.SelectedIndex = ReportProperty.Current.HeaderInstituteSocietyAddress == 0 ? 0 : 1;

            //15/10/2019,---------------------------------------------------------------------------------------------------------------------
            lcSocietyWithInstutionName.Visibility = (rgbReportTitle.SelectedIndex == 0 ? LayoutVisibility.Never : LayoutVisibility.Always);
            chkSocietyWithInstutionName.Checked = false;
            if (ReportProperty.Current.HeaderWithInstituteName == 1 && lcSocietyWithInstutionName.Visibility == LayoutVisibility.Always)
            {
                chkSocietyWithInstutionName.Checked = true;
            }
            //---------------------------------------------------------------------------------------------------------------------
        }

        /// <summary>
        /// Filter the records based on the selected groups.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvLedger_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string GroupId = string.Empty;
            //try
            //{
            //    DataTable dtGroupId = gcLedger.DataSource as DataTable;
            //    if (dtGroupId != null && dtGroupId.Rows.Count != 0)
            //    {
            //        foreach (int i in gvLedger.GetSelectedRows())
            //        {
            //            DataRow dr = gvLedger.GetDataRow(i);
            //            GroupId += dr[this.appSchema.LedgerGroup.GROUP_IDColumn.ColumnName] + ",";
            //        }
            //        GroupId = GroupId.TrimEnd(',');
            //        DataView dvGroup = null;
            //        if (LedgerSelected != null && LedgerSelected.Rows.Count != 0)
            //        {
            //            dvGroup = LedgerSelected.DefaultView;
            //            dvGroup.RowFilter = "GROUP_ID IN (" + GroupId + ")";
            //            gcLedgerDetail.DataSource = dvGroup.ToTable();
            //        }
            //        else
            //        {
            //            SetLedgerDetailSource();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            //}
            //finally { }

            this.chkLedgerGroup.CheckedChanged -= new System.EventHandler(this.chkLedgerGroup_CheckedChanged);
            chkLedgerGroup.Checked = false;
            this.chkLedgerGroup.CheckedChanged += new System.EventHandler(this.chkLedgerGroup_CheckedChanged);
            this.chkLedger.CheckedChanged -= new System.EventHandler(this.chkLedger_CheckedChanged);
            chkLedger.Checked = false;
            this.chkLedger.CheckedChanged += new System.EventHandler(this.chkLedger_CheckedChanged);
            try
            {
                DataTable dtGroupId = gcLedger.DataSource as DataTable;
                if (dtGroupId != null && dtGroupId.Rows.Count != 0)
                {
                    var Selected = (from d in dtGroupId.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);

                    if (Selected.Count() != 0)
                    {
                        DataTable dtGroup = Selected.CopyToDataTable();
                        foreach (DataRow dr in dtGroup.Rows)
                        {
                            GroupId += dr[this.appSchema.LedgerGroup.GROUP_IDColumn.ColumnName] + ",";
                        }
                        GroupId = GroupId.TrimEnd(',');
                        DataView dvGroup = null;
                        if (LedgerSelected != null && LedgerSelected.Rows.Count != 0)
                        {
                            dvGroup = LedgerSelected.DefaultView;
                            dvGroup.RowFilter = "GROUP_ID IN (" + GroupId + ")";
                            gcLedgerDetail.DataSource = dvGroup.ToTable();
                        }
                        dvGroup.RowFilter = "";
                    }
                    else
                    {
                        SetLedgerDetailSource();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void chkProjectFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvProject.OptionsView.ShowAutoFilterRow = chkProjectFilter.Checked;
            if (chkProjectFilter.Checked)
            {
                gvProject.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                gvProject.FocusedColumn = colProjectName;
                gvProject.ShowEditor();
            }
        }

        private void chkBankFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvBank.OptionsView.ShowAutoFilterRow = chkBankFilter.Checked;
            if (chkBankFilter.Checked)
            {
                gvBank.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                gvBank.FocusedColumn = colBankName;
                gvBank.ShowEditor();
            }
        }

        private void chkShowFilterCostCenter_CheckedChanged(object sender, EventArgs e)
        {
            gvCostCentre.OptionsView.ShowAutoFilterRow = chkShowFilterCostCenter.Checked;
            if (chkShowFilterCostCenter.Checked)
            {
                gvCostCentre.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                gvCostCentre.FocusedColumn = colCostCentreName;
                gvCostCentre.ShowEditor();
            }
        }

        private void chkLedgerFilter_CheckedChanged_1(object sender, EventArgs e)
        {
            gvLedgerDetails.OptionsView.ShowAutoFilterRow = chkLedgerFilter.Checked;
            if (chkLedgerFilter.Checked)
            {
                gvLedgerDetails.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                gvLedgerDetails.FocusedColumn = colLedgerName;
                gvLedgerDetails.ShowEditor();
            }
        }

        private void chkLedgerGroupFilter_CheckedChanged_1(object sender, EventArgs e)
        {
            gvLedger.OptionsView.ShowAutoFilterRow = chkLedgerGroupFilter.Checked;
            if (chkLedgerGroupFilter.Checked)
            {
                gvLedger.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                gvLedger.FocusedColumn = colledgerGroup;
                gvLedger.ShowEditor();
            }
        }

        private void chkNarration_CheckedChanged(object sender, EventArgs e)
        {
            gvNarration.OptionsView.ShowAutoFilterRow = chkNarration.Checked;
            if (chkNarration.Checked)
            {
                gvNarration.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                gvNarration.FocusedColumn = colNarration;
                gvNarration.ShowEditor();
            }
        }

        /// <summary>
        /// cell click events while filtering - mic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvProject_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                if (e.Column.FieldName == "SELECT")
                {
                    DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryTextEdit = new RepositoryItemTextEdit();
                    e.RepositoryItem = repositoryTextEdit;
                    //gvProject.OptionsSelection.InvertSelection = false;
                }
            }
        }

        private void gvBank_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                if (e.Column.FieldName == "SELECT")
                {
                    DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryTextEdit = new RepositoryItemTextEdit();
                    e.RepositoryItem = repositoryTextEdit;
                }
            }
        }

        private void gvLedger_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                if (e.Column.FieldName == "SELECT")
                {
                    DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryTextEdit = new RepositoryItemTextEdit();
                    e.RepositoryItem = repositoryTextEdit;
                }
            }
        }

        private void gvLedgerDetails_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                if (e.Column.FieldName == "SELECT")
                {
                    DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryTextEdit = new RepositoryItemTextEdit();
                    e.RepositoryItem = repositoryTextEdit;
                }
            }
        }

        private void gvCostCentre_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                if (e.Column.FieldName == "SELECT")
                {
                    DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryTextEdit = new RepositoryItemTextEdit();
                    e.RepositoryItem = repositoryTextEdit;
                }
            }
        }

        private void gvNarration_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                if (e.Column.FieldName == "SELECT")
                {
                    DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryTextEdit = new RepositoryItemTextEdit();
                    e.RepositoryItem = repositoryTextEdit;
                }
            }
        }

        private void frmReportFilter_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == (Keys.Control | Keys.D))
                {
                    xtbLocation.SelectedTabPageIndex = 0;
                }
                if (e.KeyData == (Keys.Control | Keys.P))
                {

                    xtbLocation.SelectedTabPageIndex = 1;
                }
                if (e.KeyData == (Keys.Control | Keys.L))
                {
                    if (e.KeyData == (Keys.Control | Keys.W))
                    {
                        chkLedgerGroupFilter.Checked = (chkProjectFilter.Checked) ? false : true;
                    }
                    if (e.KeyData == (Keys.Control | Keys.H))
                    {
                        chkLedgerFilter.Checked = (chkBankFilter.Checked) ? false : true;
                    }
                    xtbLocation.SelectedTabPageIndex = 2;
                }
                if (e.KeyData == (Keys.Control | Keys.C))
                {
                    if (e.KeyData == (Keys.Control | Keys.W))
                    {
                        chkShowFilterCostCenter.Checked = (chkProjectFilter.Checked) ? false : true;
                    }
                    xtbLocation.SelectedTabPageIndex = 3;
                }
                if (e.KeyData == (Keys.Control | Keys.N))
                {
                    if (e.KeyData == (Keys.Control | Keys.W))
                    {
                        chkNarration.Checked = (chkProjectFilter.Checked) ? false : true;
                    }
                    xtbLocation.SelectedTabPageIndex = 4;
                }
                if (e.KeyData == (Keys.Control | Keys.R))
                {
                    xtbLocation.SelectedTabPageIndex = 5;
                }
                if (e.KeyData == (Keys.Control | Keys.T))
                {
                    xtbLocation.SelectedTabPageIndex = 0;
                    DateTo.Focus();
                }
                if (e.KeyData == (Keys.Control | Keys.F))
                {
                    xtbLocation.SelectedTabPageIndex = 0;
                    DateFrom.Focus();
                }
                if (e.KeyData == (Keys.Control | Keys.W) && xtbLocation.SelectedTabPageIndex == 1)
                {
                    chkProjectFilter.Checked = (chkProjectFilter.Checked) ? false : true;
                }
                if (e.KeyData == (Keys.Control | Keys.H) && xtbLocation.SelectedTabPageIndex == 1)
                {
                    chkBankFilter.Checked = (chkBankFilter.Checked) ? false : true;
                }
                if (e.KeyData == (Keys.Control | Keys.W) && xtbLocation.SelectedTabPageIndex == 2)
                {
                    chkLedgerGroupFilter.Checked = (chkLedgerGroupFilter.Checked) ? false : true;
                }
                if (e.KeyData == (Keys.Control | Keys.H) && xtbLocation.SelectedTabPageIndex == 2)
                {
                    chkLedgerFilter.Checked = (chkLedgerFilter.Checked) ? false : true;
                }
                if (e.KeyData == (Keys.Control | Keys.W) && xtbLocation.SelectedTabPageIndex == 3)
                {
                    chkShowFilterCostCenter.Checked = (chkShowFilterCostCenter.Checked) ? false : true;
                }
                if (e.KeyData == (Keys.Control | Keys.W) && xtbLocation.SelectedTabPageIndex == 4)
                {
                    chkNarration.Checked = (chkNarration.Checked) ? false : true;
                }
                //if (e.KeyData == (Keys.Control | Keys.F) && xtbLocation.SelectedTabPageIndex == 21)
                //{
                //    chkBCCShowFilter.Checked = (chkBCCShowFilter.Checked) ? false : true;
                //}
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.F3))
            {
                DateFrom.Focus();
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        private void DateFrom_EditValueChanged(object sender, EventArgs e)
        {
            //if (ReportProperty.Current.ReportTitle.Substring(0, 5) != "Multi")
            //{
            //    DateTo.DateTime = DateFrom.DateTime.AddMonths(1).AddDays(-1);
            //}
            //else
            //{
            //    DateTo.DateTime = DateFrom.DateTime.AddMonths(1).AddDays(-1);
            //    DateTo.Properties.MaxValue = DateFrom.DateTime.AddMonths(12).AddDays(-1);
            //}
            string ReportCriteria = ReportProperty.Current.ReportCriteria;
            string[] aReportCriteria = ReportCriteria.Split('ÿ');


            //On 12/07/2018, This property is used to skip projects which are closed on or equal to this date------------------------
            if (Array.IndexOf(aReportCriteria, "PJ") >= 0)
            {
                string previousDateFrom = (DateFrom.OldEditValue == null ? string.Empty : ReportProperty.Current.DateSet.ToDate(DateFrom.OldEditValue.ToString()));
                if (previousDateFrom != DateFrom.Text)
                {
                    SetProjectSource();
                }
            }
            //-----------------------------------------------------------------------------------------------------------------------

            //On 28/06/2018, This property is used to skip bank ledger's which are closed on or equal to this date---------------------------------------
            if (Array.IndexOf(aReportCriteria, "BK") >= 0)
            {
                //Skip FD Reports,Cash Journal
                if (ReportProperty.Current.ReportId != "RPT-047" && ReportProperty.Current.ReportId != "RPT-094"
                    && ReportProperty.Current.ReportId != "RPT-016" && ReportProperty.Current.ReportId != "RPT-038")
                {
                    string previousDateFrom = (DateFrom.OldEditValue == null ? string.Empty : ReportProperty.Current.DateSet.ToDate(DateFrom.OldEditValue.ToString()));
                    string projectids = SelectedProjectId;
                    if (previousDateFrom != DateFrom.Text && !string.IsNullOrEmpty(projectids))
                    {
                        //On 03/05/2022
                        SetBankAccountSource();
                        /*DataTable dtBankInfo = FetchBankByProjectId(projectids);
                        gcBank.DataSource = null;
                        gcBank.DataSource = dtBankInfo;*/
                    }
                }
            }
            else if (ReportProperty.Current.ReportId.Equals("RPT-013"))
            {
                SetBankFDAccountSource();
            }
            else if (ReportProperty.Current.ReportId.Equals("RPT-101"))
            {
                setmultibank();
            }

            //On 21/10/2021, This property is used to skip ledger's which are closed on or equal to this date---------------------------------------
            if (Array.IndexOf(aReportCriteria, "LG") >= 0)
            {
                string previousDateFrom = (DateFrom.OldEditValue == null ? string.Empty : ReportProperty.Current.DateSet.ToDate(DateFrom.OldEditValue.ToString()));
                if (previousDateFrom != DateFrom.Text)
                {
                    SetLedgerDetailSource();
                }
            }

            //On 21/10/2021, This property is used to skip ledger's which are closed on or equal to this date---------------------------------------
            if (Array.IndexOf(aReportCriteria, "PL") >= 0)
            {
                string previousDateFrom = (DateFrom.OldEditValue == null ? string.Empty : ReportProperty.Current.DateSet.ToDate(DateFrom.OldEditValue.ToString()));
                if (previousDateFrom != DateFrom.Text)
                {
                    SetPartyLedgerSource();
                }
            }
            //------------------------------------------------------------------------------------------
        }

        private void DateTo_EditValueChanged(object sender, EventArgs e)
        {
            //On 12/07/2018, This property is used to skip projects which are closed on or equal to this date------------------------
            string ReportCriteria = ReportProperty.Current.ReportCriteria;
            string[] aReportCriteria = ReportCriteria.Split('ÿ');
            if (Array.IndexOf(aReportCriteria, "PJ") >= 0)
            {
                string previousDateTo = (DateTo.OldEditValue == null ? string.Empty : ReportProperty.Current.DateSet.ToDate(DateTo.OldEditValue.ToString()));
                if (previousDateTo != DateTo.Text)
                {
                    SetProjectSource();
                }
            }
            //On 28/09/2023, This property is used to skip bank ledger's Applicable date rnage ---------------------------------------
            if (Array.IndexOf(aReportCriteria, "BK") >= 0)
            {
                //Skip FD Reports,Cash Journal
                if (ReportProperty.Current.ReportId != "RPT-047" && ReportProperty.Current.ReportId != "RPT-094" &&
                    ReportProperty.Current.ReportId != "RPT-016" && ReportProperty.Current.ReportId != "RPT-038")
                {
                    string previousDateTo = (DateTo.OldEditValue == null ? string.Empty : ReportProperty.Current.DateSet.ToDate(DateTo.OldEditValue.ToString()));
                    string projectids = SelectedProjectId;
                    if (previousDateTo != DateTo.Text && !string.IsNullOrEmpty(projectids))
                    {
                        SetBankAccountSource();
                    }
                }
            }
            //-----------------------------------------------------------------------------------------------------------------------
        }

        private void gcProject_Click(object sender, EventArgs e)
        {
            DataTable dtProject = (DataTable)gcProject.DataSource;
            if (dtProject != null && dtProject.Rows.Count > 0)
            {
                int select = gvProject.GetFocusedRowCellValue(colCheck) == null ? 0 : ReportProperty.Current.NumberSet.ToInteger(gvProject.GetFocusedRowCellValue(colCheck).ToString());
                //SetCostCentreSource();
                gvProject.SetFocusedRowCellValue(colCheck, select == 0 ? 1 : 0);
            }

        }

        private void gcBank_Click(object sender, EventArgs e)
        {
            DataTable dtBank = (DataTable)gcBank.DataSource;
            if (dtBank != null && dtBank.Rows.Count > 0)
            {
                int select = gvBank.GetFocusedRowCellValue(colBankCheck) == DBNull.Value ? 0 : ReportProperty.Current.NumberSet.ToInteger(gvBank.GetFocusedRowCellValue(colBankCheck).ToString());
                gvBank.SetFocusedRowCellValue(colBankCheck, select == 0 ? 1 : 0);
            }
        }

        private void ReportDate_EditValueChanged(object sender, EventArgs e)
        {
            ReportProperty.Current.ReportDate = ReportDate.Text;
        }

        private void DateFrom_Leave(object sender, EventArgs e)
        {
            string reportCriteria = ReportProperty.Current.ReportCriteria;
            string[] aReportCriteria = reportCriteria.Split('ÿ');
            for (int i = 0; i < aReportCriteria.Length; i++)
            {
                switch (aReportCriteria[i])
                {
                    case "DF":
                    case "DT":
                        {
                            if (DateFrom.DateTime > DateTo.DateTime)
                            {
                                DateTo.DateTime = DateFrom.DateTime;
                            }
                            break;
                        }
                }
            }
        }

        private void glkpSociety_EditValueChanged(object sender, EventArgs e)
        {
            SocietyId = reportProperty.NumberSet.ToInteger(glkpSociety.EditValue.ToString());
            SetProjectSource();
            setmultibank();
        }

        private void rgbReportTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            lcSocietyWithInstutionName.Visibility = LayoutVisibility.Never;
            chkSocietyWithInstutionName.Checked = false;
            if (rgbReportTitle.SelectedIndex == 0)
            {
                rgbAddress.SelectedIndex = 0;
            }
            else
            {
                rgbAddress.SelectedIndex = 1;
                lcSocietyWithInstutionName.Visibility = LayoutVisibility.Always;
            }
        }

        private void gvLedger_Click(object sender, EventArgs e)
        {
            string GroupId = string.Empty;
            try
            {
                DataTable dtGroupId = gcLedger.DataSource as DataTable;
                if (dtGroupId != null && dtGroupId.Rows.Count != 0)
                {
                    foreach (int i in gvLedger.GetSelectedRows())
                    {
                        DataRow dr = gvLedger.GetDataRow(i);
                        GroupId += dr[this.appSchema.LedgerGroup.GROUP_IDColumn.ColumnName] + ",";
                    }
                    GroupId = GroupId.TrimEnd(',');
                    DataView dvGroup = null;
                    if (LedgerSelected != null && LedgerSelected.Rows.Count != 0 && !string.IsNullOrEmpty(GroupId))
                    {
                        dvGroup = LedgerSelected.DefaultView;
                        dvGroup.RowFilter = "GROUP_ID IN (" + GroupId + ")";
                        gcLedgerDetail.DataSource = dvGroup.ToTable();
                        dvGroup.RowFilter = "";
                    }
                    else
                    {
                        SetLedgerDetailSource();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void gvProject_Click(object sender, EventArgs e)
        {
            string ProjectId = string.Empty;
            string[] aCriteria = ReportProperty.Current.ReportCriteria.Split('ÿ');
            try
            {
                for (int i = 0; i < aCriteria.Length; i++)
                {
                    switch (aCriteria[i])
                    {
                        case "BK":
                            {
                                DataTable dtProjectId = (DataTable)gcProject.DataSource;
                                if (dtProjectId != null && dtProjectId.Rows.Count != 0)
                                {
                                    foreach (int j in gvProject.GetSelectedRows())
                                    {
                                        DataRow row = gvProject.GetDataRow(j);
                                        ProjectId += row[this.appSchema.Project.PROJECT_IDColumn.ColumnName] + ",";
                                    }
                                    ProjectId = ProjectId.TrimEnd(',');
                                    if (!string.IsNullOrEmpty(ProjectId))
                                    {
                                        DataTable dtBankInfo = FetchBankByProjectId(ProjectId);
                                        if (ReportProperty.Current.ReportId == "RPT-047" || ReportProperty.Current.ReportId == "RPT-094")
                                        {
                                            dtBankInfo = FetchFDByProjectId(ProjectId);
                                        }
                                        gcBank.DataSource = null;
                                        gcBank.DataSource = dtBankInfo;
                                    }
                                    else
                                    {
                                        SetBankAccountSource();
                                    }
                                }
                                break;
                            }
                        case "BU":
                            {
                                SetBudgetSource();

                                /*DataTable dtProjectId = (DataTable)gcProject.DataSource;
                                DataTable dtBudgetId = (DataTable)gcBank.DataSource;
                                if (dtProjectId != null && dtProjectId.Rows.Count != 0)
                                {
                                    foreach (int j in gvProject.GetSelectedRows())
                                    {
                                        DataRow row = gvProject.GetDataRow(j);
                                        ProjectId += row[this.appSchema.Project.PROJECT_IDColumn.ColumnName] + ",";
                                    }
                                    ProjectId = ProjectId.TrimEnd(',');

                                    if (!string.IsNullOrEmpty(ProjectId))
                                    {
                                        dtBudgetId = FetchBudgetsByProjects(ProjectId, true);
                                        dtBudgetId.Columns.Add(SELECT, typeof(Int32));
                                        gcBank.DataSource = dtBudgetId;
                                        gcBank.RefreshDataSource();
                                    }
                                    else
                                    {
                                        using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchBudgetNames))
                                        {
                                            ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                                            if (resultArgs.Success)
                                            {
                                                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                                                {
                                                    resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                                                    BudgetSelected = resultArgs.DataSource.Table;
                                                    ReportProperty.Current.RecordCount = resultArgs.DataSource.Table.Rows.Count;
                                                    gcBank.DataSource = null;
                                                    gcBank.DataSource = resultArgs.DataSource.Table;
                                                    gcBank.RefreshDataSource();
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    SetBudgetSource();
                                }*/
                                break;
                            }

                        case "BB":
                            {
                                SetBudgetSourceforGridLookup();
                                break;
                            }
                        case "CC": //On 15/11/2019, to load Project's CC
                            {
                                //On 30/01/2025, Show CC details for all or budget related based on enable CC budget
                                if (ReportProperty.Current.ReportGroup != "Budget" ||
                                (ReportProperty.Current.ReportGroup == "Budget" && SettingProperty.Current.EnableCostCentreBudget == 1))
                                {
                                    string projects = SelectedProject();
                                    SetCostCentreSource(projects);
                                }
                                break;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void glkpBudget_EditValueChanged(object sender, EventArgs e)
        {
            BindCompareBudget();
        }

        private void gvProject_MouseDown(object sender, MouseEventArgs e)
        {
            MakeCheckBoxSelection(e, gvProject);
        }

        private void gvProject_MouseUp(object sender, MouseEventArgs e)
        {
            MakeCheckBoxSelection(e, gvProject);
        }

        private void gvLedger_MouseDown(object sender, MouseEventArgs e)
        {
            MakeCheckBoxSelection(e, gvLedger);
        }

        private void gvLedger_MouseUp(object sender, MouseEventArgs e)
        {
            MakeCheckBoxSelection(e, gvLedger);
        }

        private void gvLedgerDetails_MouseDown(object sender, MouseEventArgs e)
        {
            MakeCheckBoxSelection(e, gvLedgerDetails);
        }

        private void gvLedgerDetails_MouseUp(object sender, MouseEventArgs e)
        {
            MakeCheckBoxSelection(e, gvLedgerDetails);
        }

        private void gvCostCentre_MouseDown(object sender, MouseEventArgs e)
        {
            MakeCheckBoxSelection(e, gvCostCentre);
        }

        private void gvCostCentre_MouseUp(object sender, MouseEventArgs e)
        {
            MakeCheckBoxSelection(e, gvCostCentre);
        }

        private void gvTDSParties_MouseDown(object sender, MouseEventArgs e)
        {
            MakeCheckBoxSelection(e, gvTDSParties);
        }

        private void gvTDSParties_MouseUp(object sender, MouseEventArgs e)
        {
            MakeCheckBoxSelection(e, gvTDSParties);
        }

        private void gvNarration_MouseDown(object sender, MouseEventArgs e)
        {
            MakeCheckBoxSelection(e, gvNarration);
        }

        private void gvNarration_MouseUp(object sender, MouseEventArgs e)
        {
            MakeCheckBoxSelection(e, gvNarration);
        }

        private void gvBank_MouseDown(object sender, MouseEventArgs e)
        {
            MakeCheckBoxSelection(e, gvBank);
        }

        private void gvBank_MouseUp(object sender, MouseEventArgs e)
        {
            MakeCheckBoxSelection(e, gvBank);
        }

        private void gvDeducteeType_MouseDown(object sender, MouseEventArgs e)
        {
            MakeCheckBoxSelection(e, gvDeducteeType);
        }

        private void gvDeducteeType_MouseUp(object sender, MouseEventArgs e)
        {
            MakeCheckBoxSelection(e, gvDeducteeType);
        }

        private void gvGroups_MouseDown(object sender, MouseEventArgs e)
        {
            MakeCheckBoxSelection(e, gvGroups);
        }

        private void gvGroups_MouseUp(object sender, MouseEventArgs e)
        {
            MakeCheckBoxSelection(e, gvGroups);
        }

        private void gvPayComponent_MouseDown(object sender, MouseEventArgs e)
        {
            MakeCheckBoxSelection(e, gvPayComponent);
        }

        private void gvPayComponent_MouseUp(object sender, MouseEventArgs e)
        {
            MakeCheckBoxSelection(e, gvPayComponent);
        }

        private void gvPayrollStaff_MouseDown(object sender, MouseEventArgs e)
        {
            MakeCheckBoxSelection(e, gvPayrollStaff);
        }

        private void gvPayrollStaff_MouseUp(object sender, MouseEventArgs e)
        {
            MakeCheckBoxSelection(e, gvPayrollStaff);
        }


        #endregion

        #region Methods
        /// <summary>
        /// Enable tabs and add controls based on the criteria.
        /// </summary>
        private void EnableTabs()
        {
            try
            {
                AssignReportCriteria();//Load the current date for Date From and Date To Control.
                cboSortByLedger.SelectedIndex = 0;
                cboSortByLedger.SelectedIndex = ReportProperty.Current.SortByLedger;
                cboSoryByGroup.SelectedIndex = 0;
                lblReportTitle.Text = ReportProperty.Current.ReportTitle;
                string reportCriteria = ReportProperty.Current.ReportCriteria;
                string[] aReportCriteria = reportCriteria.Split('ÿ');
                ReportProperty.Current.AccounYear = settinguserProperty.YearFrom;
                cboColumnHeaderFontStyle.SelectedIndex = ReportProperty.Current.ColumnCaptionFontStyle;
                lcIncludeAuditorSignNote.Visibility = LayoutVisibility.Never;
                for (int i = 0; i < aReportCriteria.Length; i++)
                {
                    switch (aReportCriteria[i])
                    {
                        case "DF": //Date From 
                            {
                                xtpDate.PageVisible = true;
                                xtbLocation.SelectedTabPageIndex = 0;
                                DateFrom.Visible = true;
                                LockDateRangeWithFinanceYear();
                                break;
                            }
                        case "DT": //Date To
                            {
                                xtpDate.PageVisible = true;
                                DateTo.Visible = true;
                                LockDateRangeWithFinanceYear();
                                break;
                            }

                        case "DA": //Date As On 
                            {
                                xtpDate.PageVisible = true;
                                xtbLocation.SelectedTabPageIndex = 0;
                                lblDateTo.Visibility = LayoutVisibility.Never;
                                emptySpaceItem8.Visibility = LayoutVisibility.Never;
                                DateFrom.Visible = true;
                                lblDateFrom.Text = DateCaption;
                                // lblGridControl.Visibility = LayoutVisibility.Never;
                                break;
                            }
                        case "OP":
                            {
                                if (ReportProperty.Current.ShowLedgerOpBalance != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "OP", "Show Opening Balance", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "OP", "Show Opening Balance", false);
                                }

                                break;
                            }
                        case "OPAL": //As On 25/06/2021, To show only Asset and Liabilities Ledger Opening alone
                            {
                                string caption = MessageRender.GetMessage(MessageCatalog.ReportMessage.OP_LEDGERS);  //"Show Opening Balance only for Asset/Liabilities Ledgers";
                                if (ReportProperty.Current.ShowAssetLiabilityLedgerOpBalance != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "OPAL", caption, true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "OPAL", caption, false);
                                }
                                break;
                            }
                        case "AL": //Show All Ledgers.
                            {
                                if (ReportProperty.Current.IncludeAllLedger != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "AL", "Include All Ledgers", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "AL", "Include All Ledgers", false);
                                }

                                break;
                            }
                        case "NA": //Show Ledger Nature
                            {
                                repositoryMultiItemComboBox1.BorderStyle = BorderStyles.Simple;
                                using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchLedgerGroupNature))
                                {
                                    ResultArgs result = dataManager.FetchData(DataSource.DataTable);
                                    if (result.Success && result.DataSource.Table != null)
                                    {
                                        DataTable dtNature = result.DataSource.Table;
                                        this.repositoryMultiItemComboBox1.DataSource = dtNature;
                                        this.repositoryMultiItemComboBox1.DisplayMember = appSchema.LedgerGroup.NATUREColumn.ColumnName;
                                        this.repositoryMultiItemComboBox1.ValueMember = appSchema.LedgerGroup.NATURE_IDColumn.ColumnName;
                                    }
                                }

                                //For Monthly Abstracts, let us have left indent to have with "Include All Ledgers"
                                string Caption = (ReportProperty.Current.ReportId == "RPT-001" || ReportProperty.Current.ReportId == "RPT-002" ||
                                        ReportProperty.Current.ReportId == "RPT-003" ? "    Ledger Nature" : "Ledger Nature") + "    ";

                                this.gridEditors.Add(this.repositoryMultiItemComboBox1, "NA", Caption, ReportProperty.Current.LedgerNature);

                                break;
                            }
                        case "CU": //Show All Country.
                            {
                                if (ReportProperty.Current.ShowAllCountry != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "CU", "Show All Country", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "CU", "Show All Country", false);
                                }
                                break;
                            }
                        case "BL": //Show Ledgers 
                            {
                                if (ReportProperty.Current.ShowByLedger != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "BL", "Show By Ledger", true);
                                }
                                else
                                {

                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "BL", "Show By Ledger", false);
                                }
                                break;
                            }
                        case "BG": //Show Groups
                            {
                                if (ReportProperty.Current.ShowByLedgerGroup != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "BG", "Show By Ledger Group", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "BG", "Show By Ledger Group", false);
                                }
                                break;
                            }
                        case "DB": //Daily Balance
                            {
                                if (ReportProperty.Current.ShowDailyBalance != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "DB", "Show Daily Balance", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "DB", "Show Daily Balance", false);
                                }
                                break;
                            }
                        case "IK": //Include In Kind
                            {
                                if (ReportProperty.Current.IncludeInKind != 0)
                                {
                                    rboInKind.SelectedIndex = 1;
                                }
                                else
                                {
                                    rboInKind.SelectedIndex = 0;
                                }
                                break;
                            }
                        case "IJ": //Include Journal
                            {
                                if (ReportProperty.Current.IncludeJournal != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "IJ", "Include Journal", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "IJ", "Include Journal", false);
                                }
                                break;
                            }
                        case "BD": //Include Bank Details
                            {
                                if (ReportProperty.Current.IncludeBankDetails != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "BD", "Include Bank Details", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "BD", "Include Bank Details", false);
                                }
                                break;
                            }
                        case "BA": //Include Bank Account Number.
                            {
                                if (ReportProperty.Current.IncludeBankAccount != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "BA", "Include A/c No", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "BA", "Include A/c No", false);
                                }
                                break;
                            }
                        case "GT": //Group Total
                            {
                                //if (ReportProperty.Current.IncludeLedgerGroupTotal != 0)
                                //{
                                //    this.gridEditors.Add(this.repositoryItemCheckEdit, "GT", "Group Total", true);
                                //}
                                //else
                                //{
                                //    this.gridEditors.Add(this.repositoryItemCheckEdit, "GT", "Group Total", false);
                                //}
                                //break;
                                if (ReportProperty.Current.IncludeLedgerGroupTotal != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "GT", "Show Month-Wise Total", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "GT", "Show Month-Wise Total", false);
                                }
                                break;
                            }
                        case "NMT": //Month-wise Cumulative Total
                            {
                                if (ReportProperty.Current.ShowNarrationMonthwiseCumulativeTotal != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "NMT", "Show Narration Month-Wise Cumulative Total", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "NMT", "Show Narration Month-Wise Cumulative Total", false);
                                }
                                break;
                            }
                        case "NMB":
                            {
                                if (ReportProperty.Current.ShowNarrationMonthwiseCumulativeOpBalance != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "NMB", "  Show Narration Month-Wise Opening Balance", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "NMB", "  Show Narration Month-Wise Opening Balance", false);
                                }
                                break;
                            }
                        case "AG": //Attach All Groups
                            {
                                if (ReportProperty.Current.IncludeLedgerGroup != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "AG", "Attach Group", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "AG", "Attach Group", false);
                                }
                                break;
                            }
                        case "AC": //Attach Cost Centre
                            {
                                if (ReportProperty.Current.IncludeCostCentre != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "AC", "Attach Cost Centre", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "AC", "Attach Cost Centre", false);
                                }
                                break;
                            }
                        case "MT": //Month Wise Total.
                            {
                                if (ReportProperty.Current.ShowMonthTotal != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "MT", "Show Month-Total", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "MT", "Show Month-Total", false);
                                }
                                break;
                            }
                        case "AD": //Attach Donor Details
                            {
                                if (ReportProperty.Current.ShowDonorAddress != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "AD", "Attach Donor Address", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "AD", "Attach Donor Address", false);
                                }
                                break;
                            }
                        case "AB": //Attach Detailed Balance
                            {
                                //if (ReportProperty.Current.ShowDetailedBalance != 0)
                                //{
                                //    this.gridEditors.Add(this.repositoryItemCheckEdit, "AB", "Include Detailed Balance", true);
                                //}
                                //else
                                //{
                                //    this.gridEditors.Add(this.repositoryItemCheckEdit, "AB", "Include Detailed Balance", false);
                                //}
                                //break;
                                if (ReportProperty.Current.ShowDetailedBalance != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "AB", "Show Detailed Cash/Bank/FD Balance", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "AB", "Show Detailed Cash/Bank/FD Balance", false);
                                }
                                break;
                            }
                        case "AB1": // Set Values to Detailed Cash Balance.
                            {
                                if (ReportProperty.Current.ShowDetailedCashBalance != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "AB1", "   Show Detailed Cash Balance", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "AB1", "   Show Detailed Cash Balance", false);
                                }
                                break;
                            }
                        case "AB2": // Set Values to Detailed Bank Balance.
                            {
                                if (ReportProperty.Current.ShowDetailedBankBalance != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "AB2", "   Show Detailed Bank Balance", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "AB2", "   Show Detailed Bank Balance", false);
                                }
                                break;
                            }
                        case "AB3": // Set Values to Detailed FD Balance.
                            {
                                if (ReportProperty.Current.ShowDetailedFDBalance != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "AB3", "   Show Detailed FD Balance", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "AB3", "   Show Detailed FD Balance", false);
                                }
                                break;
                            }
                        case "ID":
                            {
                                if (ReportProperty.Current.IncludeDetailed != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "ID", "Show Detailed", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "ID", "Show Detailed", false);
                                }
                                break;
                            }
                        case "IN": //Attach Narration
                            {
                                if (ReportProperty.Current.IncludeNarration != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "IN", "Include Narration", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "IN", "Include Narration", false);
                                }
                                break;
                            }
                        case "RN": //Include Narration with Reference No
                            {
                                if (ReportProperty.Current.IncludeNarrationwithRefNo != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "RN", "Show Narration with Reference No. and Mode", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "RN", "Show Narration with Reference No. and Mode", false);
                                }
                                break;
                            }
                        case "NPG": //Include Pan and GST Number
                            {
                                if (ReportProperty.Current.IncludePanwithGSTNo != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "NPG", "Show Narration with Pan & GST Number", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "NPG", "Show Narration with Pan & GST Number", false);
                                }
                                break;
                            }
                        case "INC": //Include Narration with Currency deatils
                            {
                                if (ReportProperty.Current.IncludeNarrationwithCurrencyDetails != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "INC", "Show Narration with Currency Details", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "INC", "Show Narration with Currency Details", false);
                                }
                                break;
                            }
                        case "ND": //Include Narration with Name & Address
                            {
                                if (ReportProperty.Current.IncludeNarrationwithNameAddress != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "ND", "Show Narration with Name & Address", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "ND", "Show Narration with Name & Address", false);
                                }
                                break;
                            }

                        case "IS": //Attach Include Parishioner
                            {
                                if (ReportProperty.Current.IncludeOutsideParishioner != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "IS", "Show Outside Parishioner", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "IS", "Show Outside Parishioner", false);
                                }
                                break;
                            }
                        case "ML": //Attach Male 
                            {
                                if (ReportProperty.Current.IncludeMale != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "ML", "Male", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "ML", "Male", false);
                                }
                                break;
                            }
                        case "FL": //Attach Female 
                            {
                                if (ReportProperty.Current.IncludeFemale != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "FL", "Female", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "FL", "Female", false);
                                }
                                break;
                            }
                        case "SENT": //Attach Thanksgiving Sent 
                            {
                                if (ReportProperty.Current.IncludeSent != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SENT", "Sent", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SENT", "Sent", false);
                                }
                                break;
                            }
                        case "NSENT": //Attach Thanksgiving Not Sent 
                            {
                                if (ReportProperty.Current.IncludeNotSent != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "NSENT", "Not Sent", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "NSENT", "Not Sent", false);
                                }
                                break;
                            }
                        case "BIRTH": //Attach Anniversary Birthday  
                            {
                                if (ReportProperty.Current.AnniversaryType != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemRadioGroup1, "BIRTH", "Anniversary", repositoryItemRadioGroup1.Items[ReportProperty.Current.AnniversaryType]);

                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemRadioGroup1, "BIRTH", "Anniversary", repositoryItemRadioGroup1.Items[ReportProperty.Current.AnniversaryType]);
                                }
                                break;
                            }
                        case "INST": //Attach Institutional 
                            {
                                if (ReportProperty.Current.IncludeInstitutional != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "INST", "Institutional", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "INST", "Institutional", false);
                                }
                                break;
                            }
                        case "IND": //Attach Individual 
                            {
                                if (ReportProperty.Current.IncludeIndividual != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "IND", "Individual", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "IND", "Individual", false);
                                }
                                break;
                            }
                        case "IP": //Attach All Purposes
                            {
                                if (ReportProperty.Current.IncludeAllPurposes != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "IP", "Include All Purposes", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "IP", "Include All Purposes", false);
                                }
                                break;
                            }
                        case "FD": //Show By Status
                            {
                                this.repositoryItemComboBox.Name = "FD";
                                this.repositoryItemComboBox.Items.Clear();
                                this.repositoryItemComboBox.Items.Add("<-- All -->");
                                this.repositoryItemComboBox.Items.Add("Active");
                                this.repositoryItemComboBox.Items.Add("Closed");

                                if (ReportProperty.Current.ReportId == "RPT-229")
                                {
                                    for (int a = 0; a < repositoryItemComboBox.Items.Count; a++)
                                    {
                                        string currentItems = repositoryItemComboBox.Items[a].ToString();

                                        if (currentItems == "Closed")
                                        {
                                            repositoryItemComboBox.Items[a] = "De Active";
                                        }
                                    }
                                }

                                if (ReportProperty.Current.FDRegisterStatus != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemComboBox, "FD", "Status", repositoryItemComboBox.Items[ReportProperty.Current.FDRegisterStatus]);

                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemComboBox, "FD", "Status", repositoryItemComboBox.Items[ReportProperty.Current.FDRegisterStatus]);
                                }
                                break;
                            }
                        case "FDS": //Show By FD Scheme
                            {
                                if (settinguserProperty.AllowMultiCurrency == 1)
                                {
                                    this.repositoryItemComboBox2.Name = "FDScheme";
                                    this.repositoryItemComboBox2.Items.Clear();
                                    this.repositoryItemComboBox2.Items.Add("<-- All -->");
                                    this.repositoryItemComboBox2.Items.Add(FDScheme.Normal.ToString());
                                    this.repositoryItemComboBox2.Items.Add(FDScheme.Flexi.ToString());

                                    int actualvtypeindex = 0;
                                    if (this.repositoryItemComboBox2.Items.IndexOf(ReportProperty.Current.FDSchemeName) >= 0)
                                    {
                                        actualvtypeindex = this.repositoryItemComboBox2.Items.IndexOf(ReportProperty.Current.FDSchemeName);
                                    }
                                    this.gridEditors.Add(this.repositoryItemComboBox2, "FDS", "Scheme", repositoryItemComboBox2.Items[actualvtypeindex]);
                                    repFDScheme = actualvtypeindex == 0 ? -1 : actualvtypeindex;
                                    repFDSchemeName = repositoryItemComboBox2.Items[actualvtypeindex].ToString();
                                }
                                break;
                            }
                        case "FDT": //Show By FD Investment Type
                            {
                                LoadInvestmentType(this.repositoryItemComboBox1);

                                repcbFDInvestmentTypeId = ReportProperty.Current.FDInvestmentType;
                                repcbFDInvestmentType = ReportProperty.Current.FDInvestmentTypeName;

                                if (ReportProperty.Current.FDInvestmentType != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemComboBox1, "FDT", "Investment Type", repcbFDInvestmentType);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemComboBox1, "FDT", "Investment Type", "<-- All Investments-->");
                                }
                                break;
                            }
                        case "FDSI": //Include FD Simple Interest 
                            {
                                if (ReportProperty.Current.IncludeFDSimpleInterest != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "FDSI", "Include Investment Simple Interest", true); //Include FD Simple Interest
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "FDSI", "Include Investment Simple Interest", false); //Include FD Simple Interest
                                }

                                break;
                            }
                        case "FDAI": //Include FD Accumulated Interest 
                            {
                                if (ReportProperty.Current.IncludeFDAccumulatedInterest != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "FDAI", "Include Investment Accumulated Interest", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "FDAI", "Include Investment Accumulated Interest", false);
                                }

                                break;
                            }
                        case "SB": //Show By Bank (in FD Reports)
                            {
                                if (ReportProperty.Current.ShowByBank != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SB", "Show by Bank", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SB", "Show by Bank", false);
                                }
                                break;
                            }
                        case "SV": //Show By Investment (in FD Reports)
                            {
                                if (ReportProperty.Current.ShowByInvestment != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SV", "Show by Investment Name", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SV", "Show by Investment Name", false);
                                }
                                break;
                            }
                        case "BC": //Show By Cost Centre
                            {
                                if (ReportProperty.Current.ShowByCostCentre != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "BC", "Show By Cost Centre", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "BC", "Show By Cost Centre", false);
                                }
                                break;
                            }
                        case "CT": //Show By Cost Centre Category
                            {
                                if (ReportProperty.Current.ReportGroup != "Budget" ||
                                            (ReportProperty.Current.ReportGroup == "Budget" && SettingProperty.Current.EnableCostCentreBudget == 1))
                                {
                                    if (ReportProperty.Current.ShowByCostCentreCategory != 0)
                                    {
                                        this.gridEditors.Add(this.repositoryItemCheckEdit, "CT", "Show By Cost Centre Category", true);
                                    }
                                    else
                                    {
                                        this.gridEditors.Add(this.repositoryItemCheckEdit, "CT", "Show By Cost Centre Category", false);
                                    }
                                }
                                break;
                            }
                        case "LS": //Show By Ledger Summary
                            {
                                if (ReportProperty.Current.ShowByLedgerSummary != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "LS", "Ledger Summary", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "LS", "Ledger Summary", false);
                                }
                                break;
                            }
                        case "DN": //Show By break up by Cost Centre
                            {
                                if (ReportProperty.Current.BreakbyDonor != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "DN", "Break Up By Donor", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "DN", "Break Up By Donor", false);
                                }
                                break;
                            }
                        case "CB": //Show By break up by Cost Centre
                            {
                                if (ReportProperty.Current.BreakByCostCentre != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "CB", "Break Up By Cost Centre", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "CB", "Break Up By Cost Centre", false);
                                }
                                break;
                            }
                        case "CL": //Show By break up by Ledger
                            {
                                if (ReportProperty.Current.BreakByLedger != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "CL", "Break Up By Ledger", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "CL", "Break Up By Ledger", false);
                                }
                                break;
                            }
                        case "CO": // Consolidated Ledger
                            {
                                if (ReportProperty.Current.Consolidated != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "CO", "Consolidated", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "CO", "Consolidated", false);
                                }
                                break;
                            }
                        case "SIL": // Show Individual Ledger
                            {   //Show all Individual ledgers (Cash, Bank Journal, if same ledger used multi times)
                                if (ReportProperty.Current.ShowIndividualLedger != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SIL", "Show Always Individual Ledger", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SIL", "Show Always Individual Ledger", false);
                                }
                                break;
                            }
                        case "DG":
                            {
                                if (ReportProperty.Current.ShowByDonorGroup != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "DG", "Show By Donor", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "DG", "Show By Donor", false);
                                }
                                break;
                            }
                        case "VT": //Voucher Types (Receipts, Payments, Contra and Journal)
                            {
                                /*this.repositoryItemComboBox.Items.Clear();
                                this.repositoryItemComboBox.Items.Add("<-- All -->");
                                this.repositoryItemComboBox.Items.Add("Receipts");
                                this.repositoryItemComboBox.Items.Add("Payments");
                                this.repositoryItemComboBox.Items.Add("Contra");
                                this.repositoryItemComboBox.Items.Add("Journal");*/
                                LoadVoucherType();
                                string caption = (ReportProperty.Current.ReportId == "RPT-208" ? "Show By" : "Voucher Type");
                                if (ReportProperty.Current.DayBookVoucherType != 0)
                                {
                                    int actualvtypeindex = 0;
                                    if (this.repositoryItemComboBox.Items.IndexOf(ReportProperty.Current.DayBookVoucherTypeName) >= 0)
                                    {
                                        actualvtypeindex = this.repositoryItemComboBox.Items.IndexOf(ReportProperty.Current.DayBookVoucherTypeName);
                                    }
                                    this.gridEditors.Add(this.repositoryItemComboBox, "VT", caption, repositoryItemComboBox.Items[actualvtypeindex]);
                                    this.repcbDayBookVoucherType = actualvtypeindex;
                                    this.repcbDayBookVoucherTypeName = repositoryItemComboBox.Items[actualvtypeindex].ToString();
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemComboBox, "VT", caption, repositoryItemComboBox.Items[ReportProperty.Current.DayBookVoucherType]);
                                }
                                break;
                            }
                        case "NY": //No of Years
                            {
                                this.repositoryItemComboBox.Name = "NY";
                                this.repositoryItemComboBox.Items.Clear();
                                this.repositoryItemComboBox.Items.Add("1");
                                this.repositoryItemComboBox.Items.Add("2");
                                this.repositoryItemComboBox.Items.Add("3");
                                this.repositoryItemComboBox.Items.Add("4");
                                this.repositoryItemComboBox.Items.Add("5");

                                if (ReportProperty.Current.NoOfYears != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemComboBox, "NY", "No of previous years with current year", repositoryItemComboBox.Items[ReportProperty.Current.NoOfYears - 1]);
                                }
                                else
                                {
                                    ReportProperty.Current.NoOfYears = 3;//by default
                                    this.gridEditors.Add(this.repositoryItemComboBox, "NY", "No of previous years with current year", 3);
                                }
                                break;
                            }
                        case "UN": //User Name
                            {
                                LoadUsers(this.repositoryItemComboBox3);
                                if (!string.IsNullOrEmpty(ReportProperty.Current.UserName))
                                {
                                    int actualvtypeindex = 0;
                                    if (this.repositoryItemComboBox3.Items.IndexOf(ReportProperty.Current.UserName) >= 0)
                                    {
                                        actualvtypeindex = this.repositoryItemComboBox3.Items.IndexOf(ReportProperty.Current.UserName);
                                    }
                                    this.gridEditors.Add(this.repositoryItemComboBox3, "UN", "User ", repositoryItemComboBox3.Items[actualvtypeindex]);

                                    if (actualvtypeindex != 0)
                                    {
                                        repcbUserName = repositoryItemComboBox3.Items[actualvtypeindex].ToString();
                                    }
                                }
                                else
                                {
                                    string defaultItem = string.Empty;
                                    if (this.repositoryItemComboBox3.Items.Count > 0)
                                    {
                                        defaultItem = this.repositoryItemComboBox3.Items[0].ToString();
                                    }
                                    this.gridEditors.Add(this.repositoryItemComboBox3, "UN", "User ", defaultItem);
                                }
                                break;
                            }
                        case "UC": //Created User
                            {
                                LoadUsers(this.repositoryItemComboBox3);
                                if (!string.IsNullOrEmpty(ReportProperty.Current.CreatedByName))
                                {
                                    int actualvtypeindex = 0;
                                    if (this.repositoryItemComboBox3.Items.IndexOf(ReportProperty.Current.CreatedByName) >= 0)
                                    {
                                        actualvtypeindex = this.repositoryItemComboBox3.Items.IndexOf(ReportProperty.Current.CreatedByName);
                                    }
                                    this.gridEditors.Add(this.repositoryItemComboBox3, "UC", "Created By ", repositoryItemComboBox3.Items[actualvtypeindex]);

                                    if (actualvtypeindex != 0)
                                    {
                                        repcbCreatedBy = repositoryItemComboBox3.Items[actualvtypeindex].ToString();
                                    }
                                }
                                else
                                {
                                    string defaultItem = string.Empty;
                                    if (this.repositoryItemComboBox3.Items.Count > 0)
                                    {
                                        defaultItem = this.repositoryItemComboBox3.Items[0].ToString();
                                    }
                                    this.gridEditors.Add(this.repositoryItemComboBox3, "UC", "Created By ", defaultItem);
                                }
                                break;
                            }
                        case "UM": //Modified User
                            {
                                LoadUsers(this.repositoryItemComboBox1);

                                if (!string.IsNullOrEmpty(ReportProperty.Current.ModifiedByName))
                                {
                                    int actualvtypeindex = 0;
                                    if (this.repositoryItemComboBox1.Items.IndexOf(ReportProperty.Current.ModifiedByName) >= 0)
                                    {
                                        actualvtypeindex = this.repositoryItemComboBox1.Items.IndexOf(ReportProperty.Current.ModifiedByName);
                                    }
                                    this.gridEditors.Add(this.repositoryItemComboBox1, "UM", "Modified By ", repositoryItemComboBox1.Items[actualvtypeindex]);
                                    if (actualvtypeindex != 0)
                                    {
                                        repcbModifiedBy = repositoryItemComboBox1.Items[actualvtypeindex].ToString();
                                    }
                                }
                                else
                                {
                                    string defaultItem = string.Empty;
                                    if (this.repositoryItemComboBox1.Items.Count > 0)
                                    {
                                        defaultItem = this.repositoryItemComboBox1.Items[0].ToString();
                                    }
                                    this.gridEditors.Add(this.repositoryItemComboBox1, "UM", "Modified By ", defaultItem);
                                }
                                break;
                            }
                        case "AA": //Audit Action
                            {
                                LoadAuditAction(this.repositoryItemComboBox2);

                                if (!string.IsNullOrEmpty(ReportProperty.Current.AuditAction))
                                {
                                    int actualvtypeindex = 0;
                                    if (this.repositoryItemComboBox2.Items.IndexOf(ReportProperty.Current.AuditAction) >= 0)
                                    {
                                        actualvtypeindex = this.repositoryItemComboBox2.Items.IndexOf(ReportProperty.Current.AuditAction);
                                    }
                                    this.gridEditors.Add(this.repositoryItemComboBox2, "UM", "Action ", repositoryItemComboBox2.Items[actualvtypeindex]);

                                    if (actualvtypeindex != 0)
                                    {
                                        repcbAuditAction = repositoryItemComboBox2.Items[actualvtypeindex].ToString();
                                    }
                                    else
                                    {
                                        repcbAuditAction = string.Empty;
                                    }
                                }
                                else
                                {
                                    string defaultItem = string.Empty;
                                    if (this.repositoryItemComboBox2.Items.Count > 0)
                                    {
                                        defaultItem = this.repositoryItemComboBox2.Items[0].ToString();
                                    }
                                    this.gridEditors.Add(this.repositoryItemComboBox2, "AA", "Action ", defaultItem);
                                }
                                break;
                            }
                        case "CD": //Attach Donor Details.
                            {
                                break;
                            }
                        case "PJ": //Show Project tabs.
                            {
                                ReportProperty.Current.Project = ReportProperty.Current.Project.Equals("0") ? SettingProperty.ActiveProjectId.ToString() : ReportProperty.Current.Project;
                                xtpProject.PageVisible = true;
                                if (!xtpDate.PageVisible)
                                {
                                    xtbLocation.SelectedTabPageIndex = 1;
                                }
                                SetSocietySource();
                                SetProjectSource();
                                SetITRGroupDetails();
                                break;
                            }
                        case "DD":
                            {
                                xrtabDeducteeType.PageVisible = true;
                                FetchDeductorType();
                                break;
                            }
                        case "BK": //Bank Account Details.
                            {
                                lcgBankAccount.Visibility = LayoutVisibility.Always;
                                lcgBankAccount.Text = ReportProperty.Current.ReportId != "RPT-094" ? "Bank Account" : "FD Account";
                                SetBankAccountSource();
                                break;
                            }
                        case "BU": // Get Budget Details.
                            {
                                xtbBudget.PageVisible = true;
                                //lcgProjectwithDivision.Visibility = LayoutVisibility.Never;
                                //lcgSociety.Visibility = LayoutVisibility.Never;
                                //lcCompareBudget1.Visibility = LayoutVisibility.Never;
                                //lcCompareBudget2.Visibility = LayoutVisibility.Never;
                                //lcgBudgetCompare.Visibility = LayoutVisibility.Always;
                                //lcgBankAccount.Text = "Budget";
                                //lcgBankAccount.Visibility = LayoutVisibility.Always;
                                //lcbudgetList.Visibility = LayoutVisibility.Always;
                                //xtpProject.Text = "Budget";
                                SetBudgetSource();
                                break;
                            }
                        case "BB": // Budget Comparison
                            {
                                lcgBudgetCompare.Visibility = LayoutVisibility.Always;
                                SetBudgetSourceforGridLookup();
                                break;
                            }

                        case "LG": //Show Ledgers With Groups.
                            {
                                xtpLedger.PageVisible = true;

                                if (!String.IsNullOrEmpty(ReportProperty.Current.Ledger) && ReportProperty.Current.Ledger != "0")
                                {
                                    AssignSelectedLedgerDetails();
                                }
                                //On 05 08/2019, To fix TDSs ledgers by default
                                else if (!String.IsNullOrEmpty(ReportProperty.Current.LedgerGroup) && ReportProperty.Current.LedgerGroup != "0")
                                {
                                    SetLedgerDetailSource();
                                    AssignSelectedLedgerGroup();
                                    gvLedger_CellValueChanged(null, new DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs(0, null, null));
                                }
                                else
                                {
                                    SetLedgerSource();
                                }
                                break;
                            }
                        case "CC": //Show Cost Centre Details.
                            {
                                SetCCCategorySource();
                                //On 30/01/2025, Show CC details for all or budget related based on enable CC budget
                                if (ReportProperty.Current.ReportGroup != "Budget" ||
                                (ReportProperty.Current.ReportGroup == "Budget" && SettingProperty.Current.EnableCostCentreBudget == 1))
                                {
                                    xtpCostCentre.PageVisible = true;
                                    if (!String.IsNullOrEmpty(ReportProperty.Current.CostCentre) && ReportProperty.Current.CostCentre != "0")
                                    {
                                        AssignSelectedCostCentreDetails();
                                    }
                                    else
                                    {
                                        SetCostCentreSource();
                                    }
                                }
                                break;
                            }
                        case "NN": //Show Narration Details.
                            {
                                xtpNarration.PageVisible = true;
                                if (!String.IsNullOrEmpty(ReportProperty.Current.Narration))
                                {
                                }
                                else
                                {
                                }
                                break;
                            }
                        case "PL":
                            {
                                xrtpPartyLedger.PageVisible = true;
                                SetPartyLedgerSource();
                                break;
                            }
                        case "NP": //Show Nature of Payments
                            {
                                xtpNarration.PageVisible = true;
                                SetNatureofPaymentSource();
                                break;
                            }
                        case "PY": //Show Payroll
                            {
                                xtpPayroll.PageVisible = true;
                                SetPayrollSource();
                                SetPayrollGroupSource();
                                xtbLocation.SelectedTabPageIndex = 7;
                                break;
                            }
                        case "PC": //Show Component
                            {
                                if (ReportProperty.Current.ReportId == "RPT-073" ||
                                    ReportProperty.Current.ReportId == "RPT - 070" ||
                                    ReportProperty.Current.ReportId == "RPT - 071" ||
                                    ReportProperty.Current.ReportId == "RPT - 072")
                                {
                                    xtbLocation.SelectedTabPageIndex = 7;
                                    lcgPayRollStaff.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    xtpPayroll.PageVisible = true;
                                    xtpPayrollComponent.PageVisible = true;
                                    SetComponentStaffSource();
                                    xtbLocation.SelectedTabPageIndex = 7;
                                }
                                else
                                {
                                    lcgPayRollStaff.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                    xtpPayroll.PageVisible = true;
                                    xtpPayrollComponent.PageVisible = true;
                                    SetComponentStaffSource();
                                    xtbLocation.SelectedTabPageIndex = 7;
                                }
                                break;
                            }
                        case "PS": //Show satff
                            {
                                if (ReportProperty.Current.ReportId == "RPT-073" ||
                                    ReportProperty.Current.ReportId == "RPT - 070" ||
                                    ReportProperty.Current.ReportId == "RPT - 071" ||
                                    ReportProperty.Current.ReportId == "RPT - 072")
                                {
                                    xtbLocation.SelectedTabPageIndex = 7;
                                    lcgPayRollStaff.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                }
                                else if (ReportProperty.Current.ReportId == "RPT-168") // For Yearly PayRegister
                                {
                                    xtpPayrollComponent.Text = "Staff";
                                    xtpPayrollComponent.PageVisible = true;
                                    lcgPayRollStaff.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                    lcPayroll.Visibility = lcgComponent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    SetStaffSource();
                                }
                                else
                                {
                                    lcgPayRollStaff.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                    xtpPayroll.PageVisible = true;
                                    xtpPayrollComponent.PageVisible = true;
                                    SetStaffSource();
                                    xtbLocation.SelectedTabPageIndex = 7;
                                }
                                break;
                            }
                        case "TBO":
                            {
                                if (ReportProperty.Current.ShowOpeningBalance != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "TBO", "Show Opening Balance", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "TBO", "Show Opening Balance", false);
                                }
                                break;
                            }
                        case "TBT":
                            {
                                if (ReportProperty.Current.ShowCurrentTransaction != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "TBT", "Show Current Transaction", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "TBT", "Show Current Transaction", false);
                                }
                                break;
                            }
                        case "TBC":
                            {
                                if (ReportProperty.Current.ShowClosingBalance != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "TBC", "Show Closing Balance", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "TBC", "Show Closing Balance", false);
                                }
                                break;
                            }
                        case "SI":
                            {
                                SetStockItemSource();
                                xtpItem.PageVisible = true;
                                break;
                            }
                        case "AI":
                            {
                                if (ReportProperty.Current.ReportId == "RPT-075")
                                {
                                    SetAssetPurchaseSource();
                                    xtpItem.PageVisible = true;
                                }
                                //else if (ReportProperty.Current.ReportId == "RPT-076")
                                //{
                                //   // SetAssetReceiveSource();
                                //    xtpProject.PageVisible = false;
                                //}
                                ////else if (ReportProperty.Current.ReportId == "RPT-085")
                                ////{
                                ////    SetAssetPurchaseource();
                                ////    xtpItem.PageVisible = true;
                                ////}
                                else if (ReportProperty.Current.ReportId == "RPT-077")
                                {
                                    // SetAssetSalesSource();
                                    xtpProject.PageVisible = false;
                                }

                                else if (ReportProperty.Current.ReportId == "RPT-084")
                                {
                                    SetAssetDisposalSource();
                                    xtpItem.PageVisible = true;
                                }
                                else if (ReportProperty.Current.ReportId == "RPT-097")
                                {
                                    SetAssetRegisterSummarySource();
                                    xtpItem.PageVisible = true;
                                }
                                else if (ReportProperty.Current.ReportId == "RPT-097")
                                {
                                    SetAssetRegisterSummarySource();
                                    xtpItem.PageVisible = true;
                                }
                                break;
                            }

                        case "LO":
                            {
                                SetLocationSource();
                                xtpLocation.PageVisible = true;
                                break;
                            }
                        case "REG":
                            {
                                SetRegistrationTypeSource();
                                xtbRegistrationType.PageVisible = true;
                                break;
                            }
                        case "COU":
                            {
                                SetCountrySource();
                                xtbCountry.PageVisible = true;
                                break;
                            }
                        case "STE":
                            {
                                SetStateSource();
                                xtbCountry.PageVisible = true;
                                break;
                            }
                        case "LAG":
                            {
                                SetLanguageSource();
                                xtpLanguage.PageVisible = true;
                                break;
                            }
                        case "STED":
                            {
                                SetStateDonaudSource();
                                xtbLocation.SelectedTabPageIndex = 17;
                                xtpStateDonaud.PageVisible = true;
                                break;
                            }
                        case "DONA":
                            {
                                SetDonaudSource();
                                xtpDonaud.PageVisible = true;
                                break;
                            }
                        case "TASK":
                            {
                                SetTaskNameSource();
                                xtpFeestDayTask.PageVisible = true;
                                break;
                            }
                        case "TK":
                            {
                                xtpNetworkingTasks.PageVisible = true;
                                //xtbLocation.SelectedTabPageIndex = 20;
                                if (!String.IsNullOrEmpty(ReportProperty.Current.SelectedTaskName) && ReportProperty.Current.SelectedTaskName != "0")
                                {
                                    AssignSelectedTaskSource();
                                }
                                else
                                {
                                    SetTaskSource();
                                }
                                break;
                            }
                        case "CN":
                            {
                                xtbDynamicConditions.PageVisible = true;
                                cboCondition.SelectedIndex = ReportProperty.Current.DonorCondtionName;
                                if (ReportProperty.Current.DonorFilterAmount > 0)
                                {
                                    txtAmount.Text = ReportProperty.Current.DonorFilterAmount.ToString();
                                }
                                break;
                            }
                        case "BF": //Show Bank Account and Fixed Deposit account details (For BRS Statement)
                            {
                                xtpBankFDAccount.PageVisible = true;
                                //05/12/2019, to keep Cash Bank LedgerId
                                //if (!String.IsNullOrEmpty(ReportProperty.Current.Ledger) && ReportProperty.Current.Ledger != "0" ||
                                //    !String.IsNullOrEmpty(ReportProperty.Current.FDAccountID))
                                if (!String.IsNullOrEmpty(ReportProperty.Current.CashBankLedger) && ReportProperty.Current.CashBankLedger != "0" ||
                                    !String.IsNullOrEmpty(ReportProperty.Current.FDAccountID))
                                {
                                    AssignBankFDAccountDetails();
                                }
                                else
                                {
                                    SetBankFDAccountSource();
                                }
                                break;
                            }
                        case "ACL": //Show Project tabs.
                            {
                                xtpAssetClass.PageVisible = true;
                                SetAssetClassSource();
                                break;
                            }
                        case "SD": //Show Sign Details
                            {
                                xtbSign.PageVisible = true;
                                chkIncludeSignDetails.Checked = false;
                                if (ReportProperty.Current.IncludeSignDetails == 1)
                                {
                                    chkIncludeSignDetails.Checked = true;
                                }

                                //On 27/02/2021, to lock setting sign Image option
                                lcSign1Btn.Visibility = LayoutVisibility.Never;
                                lcSign2Btn.Visibility = LayoutVisibility.Never;
                                lcSign3Btn.Visibility = LayoutVisibility.Never;

                                break;
                            }
                        case "ASD":
                            {
                                lcIncludeAuditorSignNote.Visibility = LayoutVisibility.Always;
                                chkIncludeAuditorSignNote.Checked = false;
                                if (ReportProperty.Current.IncludeAuditorSignNote == 1)
                                {
                                    chkIncludeAuditorSignNote.Checked = true;
                                }
                                break;
                            }
                        case "SRC": //Show Only Receipts
                            {
                                if (ReportProperty.Current.ShowOnlyReceipts != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SRC", "Show Receipts", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SRC", "Show Receipts", false);
                                }
                                break;
                            }
                        case "ECW": //Exclude Cash Withdrawal
                            {
                                if (ReportProperty.Current.ExcludeCashWithdrawal != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "ECW", "  Exclude Cash Withdrawal", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "ECW", "  Exclude Cash Withdrawal", false);
                                }
                                break;
                            }
                        case "SPY": //Show Only Payments
                            {
                                if (ReportProperty.Current.ShowOnlyPayments != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SPY", "Show Payments", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SPY", "Show Payments", false);
                                }
                                break;
                            }
                        case "ECD": //Exclude Cash Deposit
                            {
                                if (ReportProperty.Current.ExcludeCashDeposit != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "ECD", "  Exclude Cash Deposit", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "ECD", "  Exclude Cash Deposit", false);
                                }
                                break;
                            }
                        case "VCT": //View Chart TYpe
                            {
                                ricbChartType.SelectedIndexChanged += new EventHandler(ricbChartType_SelectedIndexChanged);
                                ricbChartType.BorderStyle = BorderStyles.Simple;
                                ricbChartType.TextEditStyle = TextEditStyles.DisableTextEditor;
                                ricbChartType.AutoHeight = false;
                                ricbChartType.Items.Clear();
                                string[] types = Enum.GetNames(typeof(ChartViewType));
                                foreach (string name in types)
                                {
                                    ricbChartType.Items.Add(name.ToString());
                                }
                                repcbSelectedChartType = ReportProperty.Current.ChartViewType;
                                string chartname = settinguserProperty.EnumSet.GetEnumItemNameByValue(typeof(ChartViewType), repcbSelectedChartType);
                                this.gridEditors.Add(ricbChartType, "VCT", "Chart View Type", chartname);

                                break;
                            }
                        case "CIP": //Chart in Percentage
                            {
                                if (ReportProperty.Current.ChartInPercentage != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "CIP", "    Show Chart in Percentage", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "CIP", "    Show Chart in Percentage", false);
                                }

                                break;
                            }
                        case "SAL": //On 22/02/2021, whether to show all against ledger names or show only top of the agaist ledger
                            {
                                if (ReportProperty.Current.ShowAllAgainstLedgers != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SAL", "Show All against Ledgers", true); // (Journal Vouchers)
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SAL", "Show All against Ledgers", false); // (Journal Vouchers)
                                }
                                break;
                            }
                        case "SCD": //On 22/02/2021, whether to show CC details
                            {
                                if (ReportProperty.Current.ReportGroup != "Budget" ||
                                    (ReportProperty.Current.ReportGroup == "Budget" && SettingProperty.Current.EnableCostCentreBudget == 1))
                                {
                                    if (ReportProperty.Current.ShowCCDetails != 0)
                                    {
                                        this.gridEditors.Add(this.repositoryItemCheckEdit, "SCD", "Show Cost Centre Details", true);
                                    }
                                    else
                                    {
                                        this.gridEditors.Add(this.repositoryItemCheckEdit, "SCD", "Show Cost Centre Details", false);
                                    }
                                }
                                break;
                            }
                        case "SDD": //On 19/05/2023, whether to show Donor details
                            {
                                if (ReportProperty.Current.ShowDonorDetails != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SDD", "Show Donor Details", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SDD", "Show Donor Details", false);
                                }
                                break;
                            }
                        case "LCD": //On 19/01/2023, Show Ledger-based CC
                            {
                                if (ReportProperty.Current.ShowLedgerwiseCCDetails != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "LCD", "Show Ledger-wise Cost Centre Details", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "LCD", "Show Ledger-wise Cost Centre Details", false);
                                }
                                break;
                            }
                        case "SS": //On 11/12/2021, Show by Society
                            {
                                if (ReportProperty.Current.ShowBySociety != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SS", "Show by Society", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SS", "Show by Society", false);
                                }
                                break;
                            }
                        case "SP": //On 11/12/2021, Show by Project
                            {
                                if (ReportProperty.Current.ShowByProject != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SP", "Show by Project", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SP", "Show by Project", false);
                                }
                                break;
                            }
                        case "SBG": //On 11/12/2021, Show by Budget Group
                            {
                                if (ReportProperty.Current.ShowByBudgetGroup != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SBG", "Show by Budget Group", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SBG", "Show by Budget Group", false);
                                }
                                break;
                            }
                        case "SGV": //Show GST Vouchers alone
                            {
                                if (ReportProperty.Current.ShowGSTVouchers != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SGV", "Show GST Vouchers", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SGV", "Show GST Vouchers", false);
                                }
                                break;
                            }
                        case "SGI": //Show GST Invoices Vouchers alone
                            {
                                if (ReportProperty.Current.ShowGSTInvoiceVouchers != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SGI", "Show GST Invoice Vouchers", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SGI", "Show GST Invoice Vouchers", false);
                                }
                                break;
                            }
                        case "SDL":
                            {
                                if (ReportProperty.Current.ShowIndividualDepreciationLedgers != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SDL", "Show Individual Depreciation Ledgers", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SDL", "Show Individual Depreciation Ledgers", false);
                                }
                                break;
                            }
                        case "SFDV":
                            {
                                if (ReportProperty.Current.ShowFixedDepositVoucherDetail != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SFDV", "Show Fixed Deposit Voucher Detail", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SFDV", "Show Fixed Deposit Voucher Detail", false);
                                }
                                break;
                            }
                        case "HCN": //Hide Contra Note
                            {   //On 14/03/2024, Hide contra note
                                if (ReportProperty.Current.HideContraNote != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "HCN", "Hide Contra Note (Cash Deposit/Cash Withdrawal/Transfer)", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "HCN", "Hide Contra Note (Cash Deposit/Cash Withdrawal/Transfer)", false);
                                }
                                break;
                            }
                        case "HLN": //Hide Ledger Name
                            {   //On 02/07/2024, Hide ledger name
                                if (ReportProperty.Current.HideLedgerName != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "HLN", "Hide Ledger Name", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "HLN", "Hide Ledger Name", false);
                                }
                                break;
                            }
                        case "SCO": //On 01/04/2024 Show Cash only 
                            {
                                if (ReportProperty.Current.ShowCash != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SCO", "Show Cash Only", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SCO", "Show Cash Only", false);
                                }
                                break;
                            }
                        case "SBO": //On 01/04/2024 Show Bank only 
                            {
                                if (ReportProperty.Current.ShowBank != 0)
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SBO", "Show Bank Only", true);
                                }
                                else
                                {
                                    this.gridEditors.Add(this.repositoryItemCheckEdit, "SBO", "Show Bank Only", false);
                                }
                                break;
                            }
                        case "EEX": //Average Euro Exchange Rate for local 
                        case "DEEX": //Average Euro Exchange Rate for Dollor 
                            {
                                this.repositoryText = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
                                this.repositoryText.Tag = aReportCriteria[i]; //EEX, DEEX
                                this.repositoryText.EditValueChanged += new EventHandler(repositoryText_EditValueChanged);
                                this.repositoryText.BorderStyle = BorderStyles.Simple;
                                this.repositoryText.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                                this.repositoryText.Mask.EditMask = "n";
                                this.repositoryText.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                                this.repositoryText.DisplayFormat.FormatString = "n";
                                this.repositoryText.Mask.UseMaskAsDisplayFormat = true;

                                string caption = aReportCriteria[i] == "EEX" ? "Average Euro Exchange Rate" : "Average Euro Exchange Rate for Dollar";
                                double exchangerate = 1;
                                if (aReportCriteria[i] == "EEX")
                                {
                                    exchangerate = ReportProperty.Current.AvgEuroExchangeRate;
                                    EuroExhangeRate = exchangerate;
                                }
                                else
                                {
                                    exchangerate = ReportProperty.Current.AvgEuroDollarExchangeRate;
                                    EuroDollarExhangeRate = exchangerate;
                                }

                                if (exchangerate == 0) exchangerate = 1;
                                this.gridEditors.Add(this.repositoryText, aReportCriteria[i], caption, exchangerate);
                                break;
                            }
                        case "FRXD": //Show Forex details
                            {
                                if (settinguserProperty.AllowMultiCurrency == 1)
                                {
                                    if (ReportProperty.Current.ShowForexDetail != 0)
                                    {
                                        this.gridEditors.Add(this.repositoryItemCheckEdit, "FRXD", "Show Forex split in Detail", true);
                                    }
                                    else
                                    {
                                        this.gridEditors.Add(this.repositoryItemCheckEdit, "FRXD", "Show Forex split in Detail", false);
                                    }
                                }
                                break;
                            }
                        case "CR": //On 22/10/2024, To load curencies
                            {
                                if (settinguserProperty.AllowMultiCurrency == 1)
                                {
                                    if (!string.IsNullOrEmpty(ReportProperty.Current.CurrencyCountry))
                                    {
                                        repcbCountryCurrencyId = ReportProperty.Current.CurrencyCountryId;
                                        repcbCountryCurrency = ReportProperty.Current.CurrencyCountry;
                                        repcbCurrencySymbol = ReportProperty.Current.CurrencyCountrySymbol;
                                    }
                                    else
                                    {
                                        repcbCountryCurrencyId = 0;//reportProperty.NumberSet.ToInteger(settinguserProperty.Country);
                                        repcbCountryCurrency = settinguserProperty.CurrencyName;
                                        repcbCurrencySymbol = settinguserProperty.Currency;
                                    }

                                    //On 10/12/2024 - To show currenceis in budget view report alone in Report report criteria
                                    bool budgetReportsetupenabled = (ReportProperty.Current.ReportId == "RPT-152" || ReportProperty.Current.ReportId == "RPT-048" ||
                                                                ReportProperty.Current.ReportId == "RPT-193");
                                    if (budgetReportsetupenabled)
                                    {
                                        lcReportSetupCurrency.Visibility = LayoutVisibility.Always;
                                        LoadReportSettingCurrencies();
                                    }
                                    else
                                    {
                                        LoadCurrencies(this.repositoryItemGridlookup1);
                                        this.gridEditors.Add(this.repositoryItemGridlookup1, "CR", "Currency", repcbCountryCurrencyId);
                                    }
                                }
                                break;
                            }
                        case "RPS": //On 09/01/2025 - Receipt - Payment side order
                            {
                                //repcbRPSortOrder
                                repositoryItemComboBox1.Items.Clear();
                                repositoryItemComboBox1.Items.Add("Default");
                                repositoryItemComboBox1.Items.Add("Receipt Side");
                                repositoryItemComboBox1.Items.Add("Payment Side");
                                repcbRPSortOrder = ReportProperty.Current.RandPSortOrder;
                                this.gridEditors.Add(this.repositoryItemComboBox1, "RPS", "Sort Order (Manual R.No/V.No Generation)", repositoryItemComboBox1.Items[repcbRPSortOrder]);
                                break;
                            }
                        default:
                            if (string.IsNullOrEmpty(reportCriteria))
                            {
                                xtbLocation.SelectedTabPageIndex = 21;
                                xtbReportCriteria_SelectedPageChanged(null, new DevExpress.XtraTab.TabPageChangedEventArgs(null, xtbLocation.SelectedTabPage));
                            }
                            break;

                        //case "BBL": //Show By break up by Location
                        //    {
                        //        if (ReportProperty.Current.ShowByLocation != 0)
                        //        {
                        //            this.gridEditors.Add(this.repositoryItemCheckEdit, "BBL", "Show By Location", true);
                        //        }
                        //        else
                        //        {
                        //            this.gridEditors.Add(this.repositoryItemCheckEdit, "BBL", "Show By Location", false);
                        //        }
                        //        break;
                        //    }
                        //case "BBG": //Show By break up by Location
                        //    {
                        //        if (ReportProperty.Current.ShowByGroup != 0)
                        //        {
                        //            this.gridEditors.Add(this.repositoryItemCheckEdit, "BBG", "Show By Group", true);
                        //        }
                        //        else
                        //        {
                        //            this.gridEditors.Add(this.repositoryItemCheckEdit, "BBG", "Show By Group", false);
                        //        }
                        //        break;
                        //    }

                    }
                }

                if (gridEditors.Count != 0)
                {
                    gcReportCriteria.DataSource = gridEditors;

                    //Done by alwar on 06/02/2016 to design proper alignment for general criteria -------------------------------------
                    gvReportCriteria.OptionsView.ColumnAutoWidth = false;
                    colCriteriaName.BestFit();

                    colCriteriaType.Width = gcReportCriteria.Width - (colCriteriaName.Width + 20);
                    //colCriteriaType.Width = 120;
                    //gvReportCriteria.BestFitColumns();
                    //-----------------------------------------------------------------------------------------------------------------
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void repositoryText_EditValueChanged(object sender, EventArgs e)
        {
            if (sender != null)
            {
                TextEdit rtxt = sender as TextEdit;

                if (rtxt.Properties.Tag != null && rtxt.Properties.Tag.ToString() == "EEX")
                {
                    EuroExhangeRate = ReportProperty.Current.NumberSet.ToDouble(rtxt.Text);
                }
                else if (rtxt.Properties.Tag != null && rtxt.Properties.Tag.ToString() == "DEEX")
                {
                    EuroDollarExhangeRate = ReportProperty.Current.NumberSet.ToDouble(rtxt.Text);
                }
            }
        }

        public class GridEditorItem
        {
            string fName;
            object fValue;
            string CriteriaValue;
            RepositoryItem fRepositoryItem;

            public GridEditorItem(RepositoryItem fRepositoryItem, string CriteriaName, string fName, object fValue)
            {
                this.fRepositoryItem = fRepositoryItem;
                CriteriaValue = CriteriaName;
                this.fName = fName;
                this.fValue = fValue;
            }

            public GridEditorItem(RepositoryItem fRepositoryItem, string CriteriaName, string fName)
            {
                this.fRepositoryItem = fRepositoryItem;
                CriteriaValue = CriteriaName;
                this.fName = fName;
            }

            public string Name { get { return this.fName; } }
            public string Criteria { get { return this.CriteriaValue; } }
            public object Value { get { return this.fValue; } set { this.fValue = value; } }
            public RepositoryItem RepositoryItem { get { return this.fRepositoryItem; } }
        }

        class GridEditorCollection : ArrayList
        {
            public GridEditorCollection()
            {
            }
            public new GridEditorItem this[int index] { get { return base[index] as GridEditorItem; } }
            public void Add(RepositoryItem fRepositoryItem, string Criteria, string fName, object fValue)
            {
                base.Add(new GridEditorItem(fRepositoryItem, Criteria, fName, fValue));
            }
            public void Add(RepositoryItem fRepositoryItem, string Criteria, string fName)
            {
                base.Add(new GridEditorItem(fRepositoryItem, Criteria, fName));
            }
        }

        /// <summary>
        /// Fetch project details
        /// </summary>
        private void SetProjectSource()
        {
            string selectedids = string.Empty;
            ResultArgs resultArgs = null;
            try
            {
                //On 03/05/2022, to get already selected project ids (Before click ok button)  -----------------
                if (gcProject.DataSource != null)
                {
                    DataTable dtAlreadySelected = gcProject.DataSource as DataTable;
                    dtAlreadySelected.DefaultView.RowFilter = SELECT + " = 1";
                    var listSelProjects = dtAlreadySelected.DefaultView.ToTable().AsEnumerable().Select(r => r[this.appSchema.Project.PROJECT_IDColumn.ColumnName].ToString());
                    if (listSelProjects != null)
                    {
                        selectedids = string.Join(",", listSelProjects);
                    }
                }
                //----------------------------------------------------------------------------------------------
                if (ReportProperty.Current.ReportId != "RPT-228")
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchProjectBySociety))
                    {
                        if (!settinguserProperty.IsFullRightsReservedUser)
                        {
                            dataManager.Parameters.Add(this.appSchema.UserRole.USERROLE_IDColumn, settinguserProperty.RoleId);
                        }
                        if (SocietyId != 0)
                        {
                            dataManager.Parameters.Add(this.appSchema.Project.CUSTOMERIDColumn, SocietyId);
                        }

                        //if (ITRGroupId != 0)
                        //{
                        //    dataManager.Parameters.Add(this.appSchema.ProjectCatogoryITRGroup.PROJECT_CATOGORY_ITRGROUP_IDColumn, ITRGroupId);
                        //}

                        //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                        //dataManager.Parameters.Add(this.appSchema.Project.DATE_CLOSEDColumn, DateFrom.Text);

                        //On 06/01/2021, For Multi Abstract Year comparisaion (Skip closed projects) --------------------------------
                        if (ReportProperty.Current.ReportId == "RPT-155" || ReportProperty.Current.ReportId == "RPT-156" || ReportProperty.Current.ReportId == "RPT-187")
                        {
                            string datefrom = DateFrom.Text;
                            int noofyears = ReportProperty.Current.NoOfYears;
                            if (noofyears == 0)
                            {
                                noofyears = 3;
                            }
                            datefrom = ReportProperty.Current.DateSet.ToDate(settinguserProperty.YearFrom, false).AddYears(-noofyears).ToShortDateString();
                            dataManager.Parameters.Add(this.appSchema.Project.DATE_CLOSEDColumn, datefrom);

                        }
                        else
                        {
                            dataManager.Parameters.Add(this.appSchema.Project.DATE_CLOSEDColumn, DateFrom.Text);
                        }
                        //------------------------------------------------------------------------------------------------------------

                        //On 09/02/2022, load Projects based on start of the project (Current FY To)
                        if (DateTo.Visible && !string.IsNullOrEmpty(DateTo.Text) && ReportProperty.Current.DateSet.ToDate(DateTo.Text, false) != DateTime.MinValue)
                        {
                            dataManager.Parameters.Add(this.appSchema.Project.DATE_STARTEDColumn, DateTo.Text);
                        }
                        else if (DateFrom.Visible && !string.IsNullOrEmpty(DateFrom.Text) && ReportProperty.Current.DateSet.ToDate(DateFrom.Text, false) != DateTime.MinValue)
                        { //On 24/09/2024, To set date from for project date start conditon
                            dataManager.Parameters.Add(this.appSchema.Project.DATE_STARTEDColumn, DateFrom.Text);
                        }

                        resultArgs = dataManager.FetchData(DataSource.DataTable);
                        //resultArgs.DataSource.Data = FetchProjectsByDateRange(resultArgs.DataSource.Table);
                        if (resultArgs.Success)
                        {
                            AssignDefaultProjects = resultArgs.DataSource.Table;
                            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                            {
                                if (ReportProperty.Current.ReportId == "RPT-043")
                                {
                                    resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                                    DataTable dtForeignPro = FetchForeignProjects(resultArgs.DataSource.Table);
                                    ProjectSelected = dtForeignPro;
                                    ReportProperty.Current.RecordCount = dtForeignPro.Rows.Count;
                                    gcProject.DataSource = null;
                                    gcProject.DataSource = dtForeignPro;
                                    gcProject.RefreshDataSource();

                                    // To select all the project default
                                    DataTable dtProject = gcProject.DataSource as DataTable;
                                    if (dtProject != null && dtProject.Rows.Count > 0)
                                    {
                                        foreach (DataRow dr in dtProject.Rows)
                                        {
                                            dr["SELECT"] = 1;
                                        }
                                    }
                                    // dtselectedProjectId = dtProject;
                                }
                                else
                                {
                                    resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                                    ProjectSelected = resultArgs.DataSource.Table;
                                    //dtselectedProjectId = resultArgs.DataSource.Table;
                                    //foreach (DataRow dr in dtselectedProjectId.Rows)
                                    //{
                                    //    dr["SELECT"] = 0;
                                    //}
                                    ReportProperty.Current.RecordCount = resultArgs.DataSource.Table.Rows.Count;
                                    gcProject.DataSource = null;
                                    gcProject.DataSource = resultArgs.DataSource.Table;
                                    gcProject.RefreshDataSource();
                                }
                            }
                            else
                            {
                                gcProject.DataSource = null;
                            }
                        }
                    }
                }


                // 14/11/2024 ------------------------------------------------------------------------------------------------------
                if (ReportProperty.Current.ReportId == "RPT-228")
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchProjectByITRGroup))
                    {
                        if (!settinguserProperty.IsFullRightsReservedUser)
                        {
                            dataManager.Parameters.Add(this.appSchema.UserRole.USERROLE_IDColumn, settinguserProperty.RoleId);
                        }
                        if (ITRGroupId != 0)
                        {
                            dataManager.Parameters.Add(this.appSchema.ProjectCatogoryITRGroup.PROJECT_CATOGORY_ITRGROUP_IDColumn, ITRGroupId);
                        }

                        dataManager.Parameters.Add(this.appSchema.Project.DATE_CLOSEDColumn, DateFrom.Text);
                        //-----------------------------------------------------------------------------------------------------------------------------------------

                        //On 09/02/2022, load Projects based on start of the project (Current FY To)
                        if (DateTo.Visible && !string.IsNullOrEmpty(DateTo.Text) && ReportProperty.Current.DateSet.ToDate(DateTo.Text, false) != DateTime.MinValue)
                        {
                            dataManager.Parameters.Add(this.appSchema.Project.DATE_STARTEDColumn, DateTo.Text);
                        }
                        else if (DateFrom.Visible && !string.IsNullOrEmpty(DateFrom.Text) && ReportProperty.Current.DateSet.ToDate(DateFrom.Text, false) != DateTime.MinValue)
                        { //On 24/09/2024, To set date from for project date start conditon
                            dataManager.Parameters.Add(this.appSchema.Project.DATE_STARTEDColumn, DateFrom.Text);
                        }

                        resultArgs = dataManager.FetchData(DataSource.DataTable);
                        //resultArgs.DataSource.Data = FetchProjectsByDateRange(resultArgs.DataSource.Table);
                        if (resultArgs.Success)
                        {
                            AssignDefaultProjects = resultArgs.DataSource.Table;
                            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                            {
                                resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                                ProjectSelected = resultArgs.DataSource.Table;
                                //dtselectedProjectId = resultArgs.DataSource.Table;
                                //foreach (DataRow dr in dtselectedProjectId.Rows)
                                //{
                                //    dr["SELECT"] = 0;
                                //}
                                ReportProperty.Current.RecordCount = resultArgs.DataSource.Table.Rows.Count;
                                gcProject.DataSource = null;
                                gcProject.DataSource = resultArgs.DataSource.Table;
                                gcProject.RefreshDataSource();
                            }
                            else
                            {
                                gcProject.DataSource = null;
                            }
                        }
                    }
                }
                // -----------------------------------------------------------------------------------------------------------------

                if ((!String.IsNullOrEmpty(ReportProperty.Current.Project) && ReportProperty.Current.Project != "0") ||
                    (!string.IsNullOrEmpty(selectedids)))
                {
                    DataTable dtProject = gcProject.DataSource as DataTable;
                    if (dtProject != null && dtProject.Rows.Count > 0)
                    {
                        string projectids = string.IsNullOrEmpty(ReportProperty.Current.Project) ? string.Empty : ReportProperty.Current.Project;
                        string[] projectid = projectids.Split(',');
                        string[] projectid1 = selectedids.Split(',');

                        foreach (DataRow dr in dtProject.Rows)
                        {
                            for (int i = 0; i < projectid.Length; i++)
                            {
                                if (dr[this.appSchema.Project.PROJECT_IDColumn.ColumnName].ToString() == projectid[i])
                                {
                                    //int index = dtProject.Rows.IndexOf(dr);
                                    //gvProject.SelectRow(index);
                                    dr[SELECT] = 1;
                                }
                            }

                            for (int i = 0; i < projectid1.Length; i++)
                            {
                                if (dr[this.appSchema.Project.PROJECT_IDColumn.ColumnName].ToString() == projectid1[i])
                                {
                                    dr[SELECT] = 1;
                                }
                            }
                        }
                        DataView dv = new DataView(dtProject);
                        dv.Sort = SELECT + " DESC";
                        gcProject.DataSource = dv.ToTable();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        /// <summary>
        /// Fetch project details
        /// </summary>
        private void SetAssetClassSource()
        {

            ResultArgs resultArgs = null;
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AssetClass.FetchAll))
                {
                    if (!settinguserProperty.IsFullRightsReservedUser)
                    {
                        dataManager.Parameters.Add(this.appSchema.UserRole.USERROLE_IDColumn, settinguserProperty.RoleId);
                    }

                    dataManager.Parameters.Add(this.appSchema.ASSETClassDetails.ASSET_CLASS_IDColumn, AssetClassId);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    AssignDefaultAssetClasses = resultArgs.DataSource.Table;
                    if (resultArgs.Success)
                    {
                        if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                        {

                            resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                            AssetclassSelected = resultArgs.DataSource.Table;
                            //dtselectedProjectId = resultArgs.DataSource.Table;
                            //foreach (DataRow dr in dtselectedProjectId.Rows)
                            //{
                            //    dr["SELECT"] = 0;
                            //}
                            ReportProperty.Current.RecordCount = resultArgs.DataSource.Table.Rows.Count;
                            gcAssetClass.DataSource = null;
                            gcAssetClass.DataSource = resultArgs.DataSource.Table;
                            gcAssetClass.RefreshDataSource();

                        }
                        else
                        {
                            gcAssetClass.DataSource = null;
                        }
                    }
                }

                if (!String.IsNullOrEmpty(ReportProperty.Current.Assetclass) && ReportProperty.Current.Assetclass != "0")
                {
                    DataTable dtAssetClass = gcAssetClass.DataSource as DataTable;
                    if (dtAssetClass != null && dtAssetClass.Rows.Count > 0)
                    {
                        string[] AssetClassid = ReportProperty.Current.Assetclass.ToString().Split(',');
                        foreach (DataRow dr in dtAssetClass.Rows)
                        {
                            for (int i = 0; i < AssetClassid.Length; i++)
                            {
                                if (dr[this.appSchema.ASSETClassDetails.ASSET_CLASS_IDColumn.ColumnName].ToString() == AssetClassid[i])
                                {
                                    //int index = dtProject.Rows.IndexOf(dr);
                                    //gvProject.SelectRow(index);
                                    dr[SELECT] = 1;
                                }
                            }
                        }
                        DataView dv = new DataView(dtAssetClass);
                        dv.Sort = SELECT + " DESC";
                        gcAssetClass.DataSource = dv.ToTable();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }


        /// <summary>
        /// Fetch Society details
        /// </summary>
        private void SetSocietySource()
        {
            ResultArgs resultArgs = null;
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchSocietyNames))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        if (ReportProperty.Current.ReportId == "RPT-043" || ReportProperty.Current.ReportId == "RPT-093")
                        {
                            BindGridLookUpCombo(glkpSociety, resultArgs.DataSource.Table, this.appSchema.LegalEntity.SOCIETYNAMEColumn.ColumnName, this.appSchema.Project.CUSTOMERIDColumn.ColumnName, false, "");
                        }
                        else
                        {
                            BindGridLookUpCombo(glkpSociety, resultArgs.DataSource.Table, this.appSchema.LegalEntity.SOCIETYNAMEColumn.ColumnName, this.appSchema.Project.CUSTOMERIDColumn.ColumnName, true, "");
                        }
                        glkpSociety.EditValue = (Bosco.Report.Base.ReportProperty.Current.SocietyId != 0) ? Bosco.Report.Base.ReportProperty.Current.SocietyId : glkpSociety.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }


        /// <summary>
        /// Fetch ITR Group details
        /// </summary>
        private void SetITRGroupDetails()
        {
            ResultArgs resultArgs = null;
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Project.ITRGroupNames))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        BindGridLookUpCombo(glkpITRGroup, resultArgs.DataSource.Table, "PROJECT_CATOGORY_ITRGROUP", "PROJECT_CATOGORY_ITRGROUP_ID", true, "");

                        glkpITRGroup.EditValue = (Bosco.Report.Base.ReportProperty.Current.ITRGroupId != 0) ? Bosco.Report.Base.ReportProperty.Current.ITRGroupId : glkpITRGroup.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }


        /// <summary>
        /// To bind grid lookupedit value
        /// </summary>
        /// <param name="dropDownCombo"></param>
        /// <param name="dataSource"></param>
        /// <param name="listField"></param>
        /// <param name="valueField"></param>
        /// <param name="isAddEmptyItem"></param>
        /// <param name="emptyItemName"></param>
        private void BindGridLookUpCombo(DevExpress.XtraEditors.GridLookUpEdit dropDownCombo, object dataSource, string listField, string valueField, bool isAddEmptyItem, string emptyItemName)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)dataSource;
            if (isAddEmptyItem)
            {
                DataRow dr = null;
                if (dt.Columns.Contains(valueField) && dt.Columns.Contains(listField))
                {
                    dr = dt.NewRow();
                    dr[valueField] = 0;
                    dr[listField] = "<--All-->";
                    dt.Rows.InsertAt(dr, 0);
                }
            }

            dropDownCombo.Properties.DataSource = dt;
            dropDownCombo.Properties.DisplayMember = listField;
            dropDownCombo.Properties.ValueMember = valueField;
        }

        /// <summary>
        /// Fetch Society details
        /// </summary>
        private void SetCCCategorySource()
        {
            ResultArgs resultArgs = null;
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.CostCentreCategory.FetchAll))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        BindGridLookUpCombo(glkpCCCategory, resultArgs.DataSource.Table, this.appSchema.CostCentreCategory.COST_CENTRE_CATEGORY_NAMEColumn.ColumnName,
                                this.appSchema.CostCentreCategory.COST_CENTRECATEGORY_IDColumn.ColumnName, true, "");
                        glkpCCCategory.EditValue = (Bosco.Report.Base.ReportProperty.Current.CCCategoryId != 0) ? Bosco.Report.Base.ReportProperty.Current.CCCategoryId : glkpCCCategory.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        /// <summary>
        /// SEt Cost centere Details
        /// </summary>
        private void SetCostCentreSource(string selectedProjects = "", string selectedLedger = "")
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.CostCentre.SetCostCentreSource))
                {
                    //On 15/11/2019, to filter CC for selected proejcts
                    if (string.IsNullOrEmpty(selectedProjects))
                    {
                        dataManager.Parameters.Add(this.appSchema.Project.PROJECT_IDColumn, string.IsNullOrEmpty(ReportProperty.Current.Project) ? "0" : ReportProperty.Current.Project);
                    }
                    else
                    {
                        dataManager.Parameters.Add(this.appSchema.Project.PROJECT_IDColumn, selectedProjects);
                    }

                    if (settinguserProperty.CostCeterMapping == 1 && !string.IsNullOrEmpty(selectedLedger))
                    {
                        dataManager.Parameters.Add(this.appSchema.Ledger.LEDGER_ID_COLLECTIONColumn.ColumnName, selectedLedger);
                    }

                    if (CCCategoryId > 0)
                    {
                        dataManager.Parameters.Add(this.appSchema.CostCentreCategory.COST_CENTRECATEGORY_IDColumn.ColumnName, CCCategoryId);
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                    {
                        //if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                        if (resultArgs.DataSource.Table != null)
                        {
                            resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                            CostCentreSelected = resultArgs.DataSource.Table;
                            gcCostCentre.DataSource = resultArgs.DataSource.Table;
                            gcCostCentre.RefreshDataSource();

                            SelectCC();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }



        /// <summary>
        /// Fetch Nature of Payments
        /// </summary>
        private void SetNatureofPaymentSource()
        {
            ResultArgs resultArgs = null;
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.NatureofPayments.FetchAll))
                {
                    if (!settinguserProperty.IsFullRightsReservedUser)
                    {
                        dataManager.Parameters.Add(this.appSchema.UserRole.USERROLE_IDColumn, settinguserProperty.RoleId);
                    }
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                    {
                        if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                        {
                            resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                            NarrationSelected = resultArgs.DataSource.Table;
                            ReportProperty.Current.RecordCount = resultArgs.DataSource.Table.Rows.Count;
                            gcNarration.DataSource = null;
                            gcNarration.DataSource = resultArgs.DataSource.Table;
                            gcNarration.RefreshDataSource();
                        }
                    }
                }

                if (!String.IsNullOrEmpty(ReportProperty.Current.NatureofPaymets) && ReportProperty.Current.NatureofPaymets != "0")
                {
                    DataTable dtNatureofPayments = gcNarration.DataSource as DataTable;
                    string[] natureofPaymentId = ReportProperty.Current.NatureofPaymets.ToString().Split(',');
                    foreach (DataRow dr in dtNatureofPayments.Rows)
                    {
                        for (int i = 0; i < natureofPaymentId.Length; i++)
                        {
                            if (dr[this.appSchema.NatureofPayment.NATURE_PAY_IDColumn.ColumnName].ToString() == natureofPaymentId[i])
                            {
                                dr[SELECT] = 1;
                            }
                        }
                    }
                    dtNatureofPayments.DefaultView.Sort = "SELECT DESC";
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }



        private void FilterCostCentres()
        {
            try
            {
                ReportProperty.Current.ProjectId = SelectedProject();
                using (DataManager dataManager = new DataManager(SQLCommand.CostCentre.SetCostCentreSource))
                {
                    //  dataManager.Parameters.A(this.appSchema.Project.PROJECT_IDColumn, string.IsNullOrEmpty(ReportProperty.Current.ProjectId) ? "0" : ReportProperty.Current.ProjectId);

                    //On 15/11/2019, to filter CC for selected proejcts
                    dataManager.Parameters.Add(this.appSchema.Project.PROJECT_IDColumn, string.IsNullOrEmpty(ReportProperty.Current.ProjectId) ? "0" : ReportProperty.Current.ProjectId);
                    if (CCCategoryId > 0)
                    {
                        dataManager.Parameters.Add(this.appSchema.CostCentreCategory.COST_CENTRECATEGORY_IDColumn.ColumnName, CCCategoryId);
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                        gcCostCentre.DataSource = resultArgs.DataSource.Table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
        }

        /// <summary>
        /// Fetch ledger source
        /// </summary>
        private void SetLedgerSource()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.SetLedgerSource))
                {


                    ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                    {
                        if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                            //Skip Cashinhand anb bank accounts

                            //On 13/10/2018, To skip bank and cash ledgers
                            //resultArgs.DataSource.Table.DefaultView.RowFilter = this.appSchema.LedgerGroup.GROUP_IDColumn.ColumnName +
                            //        " NOT IN (" + (Int32)FixedLedgerGroup.BankAccounts + "," + (Int32)FixedLedgerGroup.Cash + ")";
                            resultArgs.DataSource.Table.DefaultView.RowFilter = this.appSchema.LedgerGroup.GROUP_IDColumn.ColumnName +
                                    " NOT IN (" + (Int32)FixedLedgerGroup.BankAccounts + ")";
                            DataTable dtLegers = resultArgs.DataSource.Table.DefaultView.ToTable();

                            LedgerGroupSelected = dtLegers;
                            gcLedger.DataSource = dtLegers;
                            gcLedger.RefreshDataSource();
                        }
                        if (ReportProperty.Current.Ledger == "0")
                        {
                            SetLedgerDetailSource();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        /// <summary>
        /// Fetch ledger source
        /// </summary>
        private void SetPartyLedgerSource()
        {
            DataTable dtPartyLedger = new DataTable();
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.SetLedgerDetailSource))
                {
                    //On 21/10/2021, This property is used to skip ledger's which are closed on or equal to this date
                    if (!string.IsNullOrEmpty(DateFrom.Text))
                    {
                        dataManager.Parameters.Add(this.appSchema.Project.DATE_CLOSEDColumn, DateFrom.Text);
                    }
                    ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                    {
                        if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            DataView dvPartyLedger = resultArgs.DataSource.Table.DefaultView;
                            //dvPartyLedger.RowFilter = "GROUP_ID IN(26)";
                            dvPartyLedger.RowFilter = this.appSchema.Ledger.GROUP_IDColumn.ColumnName + " IN (" + (int)TDSDefaultLedgers.SunderyCreditors + "," + (int)TDSDefaultLedgers.SundryDebtors + ")";
                            dtPartyLedger = dvPartyLedger.ToTable();
                            dtPartyLedger.Columns.Add(SELECT, typeof(int));
                            gcTDSParties.DataSource = dtPartyLedger;
                            gcTDSParties.RefreshDataSource();
                            dvPartyLedger.RowFilter = "";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(ReportProperty.Current.Ledger) && ReportProperty.Current.Ledger != "0")
                {
                    DataTable dtTDSPartyLedger = gcTDSParties.DataSource as DataTable;
                    string[] partyLedgerId = ReportProperty.Current.Ledger.ToString().Split(',');
                    foreach (DataRow dr in dtTDSPartyLedger.Rows)
                    {
                        for (int i = 0; i < partyLedgerId.Length; i++)
                        {
                            if (dr[this.appSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString() == partyLedgerId[i])
                            {
                                dr[SELECT] = 1;
                            }
                        }
                    }
                    DataView dv = new DataView(dtPartyLedger);
                    dv.Sort = SELECT + " DESC";
                    dtPartyLedger = dv.ToTable();
                    gcTDSParties.DataSource = dtPartyLedger;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        /// <summary>
        /// Fetch ledger group Source
        /// </summary>
        private void SetLedgerDetailSource()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.SetLedgerDetailSource))
                {
                    //On 21/10/2021, This property is used to skip ledger's which are closed on or equal to this date
                    if (!string.IsNullOrEmpty(DateFrom.Text))
                    {
                        dataManager.Parameters.Add(this.appSchema.Project.DATE_CLOSEDColumn, DateFrom.Text);
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                    {
                        resultArgs = ReportProperty.Current.EnforceSkipDefaultLedgers(resultArgs, this.appSchema.LedgerBalance.LEDGER_IDColumn.ColumnName);
                        if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                            //On 13/10/2018, To skip bank and cash ledgers
                            //resultArgs.DataSource.Table.DefaultView.RowFilter = this.appSchema.LedgerGroup.GROUP_IDColumn.ColumnName +
                            //        " NOT IN (" + (Int32)FixedLedgerGroup.BankAccounts + "," + (Int32)FixedLedgerGroup.Cash + ")";

                            //on 22.10.2019 to disable the Cash ledger 
                            resultArgs.DataSource.Table.DefaultView.RowFilter = this.appSchema.LedgerGroup.GROUP_IDColumn.ColumnName +
                                    " NOT IN (" + (Int32)FixedLedgerGroup.BankAccounts + "," + (Int32)FixedLedgerGroup.Cash + ")";
                            DataTable dtLegers = resultArgs.DataSource.Table.DefaultView.ToTable();

                            LedgerSelected = dtLegers;
                            gcLedgerDetail.DataSource = dtLegers;
                            gcLedgerDetail.RefreshDataSource();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        /// <summary>
        /// Fetch bank account details
        /// </summary>
        private void SetBankAccountSource()
        {
            string selectedids = string.Empty;
            string ReportCriteria = ReportProperty.Current.ReportCriteria;
            string[] aReportCriteria = ReportCriteria.Split('ÿ');

            try
            {
                //On 03/05/2022, to get already selected project ids (Before click ok button)  -----------------
                if (gcBank.DataSource != null)
                {
                    DataTable dtAlreadySelected = gcBank.DataSource as DataTable;
                    dtAlreadySelected.DefaultView.RowFilter = SELECT + " = 1";
                    var listSelLedgers = dtAlreadySelected.DefaultView.ToTable().AsEnumerable().Select(r => r[this.appSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString());
                    if (listSelLedgers != null)
                    {
                        selectedids = string.Join(",", listSelLedgers);
                    }
                }
                //----------------------------------------------------------------------------------------------

                if (string.IsNullOrEmpty(ReportProperty.Current.Project) || ReportProperty.Current.Project == "0")
                {

                    using (DataManager dataManager = new DataManager(SQLCommand.Bank.SelectAllBank))
                    {
                        dataManager.Parameters.Add(this.appSchema.BankAccount.DATE_CLOSEDColumn, DateFrom.Text);

                        if (!string.IsNullOrEmpty(SelectedProjectId))
                        {
                            dataManager.Parameters.Add(this.appSchema.Project.PROJECT_IDColumn, SelectedProjectId);
                        }
                        //On 29/09/2023, This property is used to skip bank ledger project based
                        if (Array.IndexOf(aReportCriteria, "DA") >= 0)
                        {
                            dataManager.Parameters.Add(this.appSchema.Ledger.APPLICABLE_FROMColumn, settinguserProperty.FirstFYDateFrom);
                            dataManager.Parameters.Add(this.appSchema.Ledger.APPLICABLE_TOColumn, DateFrom.Text);
                        }
                        else
                        {
                            dataManager.Parameters.Add(this.appSchema.Ledger.APPLICABLE_FROMColumn, DateFrom.Text);
                            dataManager.Parameters.Add(this.appSchema.Ledger.APPLICABLE_TOColumn, DateTo.Text);
                        }

                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                        if (resultArgs.Success)
                        {
                            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                            {
                                if (ReportProperty.Current.ReportId == "RPT-047" || ReportProperty.Current.ReportId == "RPT-094")
                                {
                                    DataTable dtFD = FetchAllFixedDeposit();
                                    gcBank.DataSource = null;
                                    gcBank.DataSource = dtFD;
                                    gcBank.RefreshDataSource();
                                }
                                else
                                {
                                    resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                                    DataTable dtBankLedgers = resultArgs.DataSource.Table;
                                    AttachCashLedgerByProject(ReportProperty.Current.Project, dtBankLedgers);
                                    BankSelected = dtBankLedgers;
                                    ReportProperty.Current.RecordCount = resultArgs.DataSource.Table.Rows.Count;
                                    gcBank.DataSource = null;
                                    gcBank.DataSource = dtBankLedgers;
                                    gcBank.RefreshDataSource();
                                }
                            }
                        }
                    }
                }
                else
                {
                    DataTable dtBankDetails = FetchBankByProjectId(ReportProperty.Current.Project);
                    if (ReportProperty.Current.ReportId == "RPT-047" || ReportProperty.Current.ReportId == "RPT-094")
                    {
                        dtBankDetails = FetchFDByProjectId(ReportProperty.Current.Project);
                    }
                    //  gvBank.FocusedRowChanged -= new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gvBank_FocusedRowChanged);
                    AttachCashLedgerByProject(ReportProperty.Current.Project, dtBankDetails);
                    gcBank.DataSource = dtBankDetails;

                    BankSelected = dtBankDetails;
                    gcBank.RefreshDataSource();
                }
                if (!string.IsNullOrEmpty(ReportProperty.Current.FDAccountID) && ReportProperty.Current.FDAccountID != "0" && ReportProperty.Current.ReportId == "RPT-094")
                {
                    DataTable dtBank = (DataTable)gcBank.DataSource;
                    string[] projectid = ReportProperty.Current.FDAccountID.ToString().Split(',');
                    int rtempfocusrow = 0;
                    if (dtBank != null && dtBank.Rows.Count > 0)
                    {
                        for (int drw = 0; drw < dtBank.Rows.Count; drw++)
                        {
                            for (int i = 0; i < projectid.Length; i++)
                            {
                                if (dtBank.Rows[drw][this.appSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName].ToString() == projectid[i])
                                {
                                    dtBank.Rows[drw][SELECT] = 1;
                                    rtempfocusrow = drw;
                                }
                            }
                        }
                        DataView dv = new DataView(dtBank);
                        dv.Sort = SELECT + " DESC";
                        gcBank.DataSource = dv.ToTable();
                        gvBank.FocusedRowHandle = rtempfocusrow;
                    }
                }
                //else if (!string.IsNullOrEmpty(ReportProperty.Current.Ledger) && ReportProperty.Current.Ledger != "0") //05/12/2019, to keep Cash Bank LedgerId
                else if ((!string.IsNullOrEmpty(ReportProperty.Current.CashBankLedger) && ReportProperty.Current.CashBankLedger != "0") || !string.IsNullOrEmpty(selectedids))
                {
                    string cashbnkledger = string.IsNullOrEmpty(ReportProperty.Current.CashBankLedger) ? string.Empty : ReportProperty.Current.CashBankLedger;
                    DataTable dtBank = (DataTable)gcBank.DataSource;
                    //string[] projectid = ReportProperty.Current.Ledger.ToString().Split(','); //05/12/2019, to keep Cash Bank LedgerId
                    string[] cashbankledgerid = cashbnkledger.Split(',');
                    string[] cashbankledgerid1 = selectedids.Split(',');

                    foreach (DataRow dr in dtBank.Rows)
                    {
                        for (int i = 0; i < cashbankledgerid.Length; i++)
                        {
                            if (dr[this.appSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString() == cashbankledgerid[i])
                            {
                                dr[SELECT] = 1;
                            }
                        }

                        for (int i = 0; i < cashbankledgerid1.Length; i++)
                        {
                            if (dr[this.appSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString() == cashbankledgerid1[i])
                            {
                                dr[SELECT] = 1;
                            }
                        }
                    }
                    DataView dv = new DataView(dtBank);
                    dv.Sort = SELECT + " DESC";
                    gcBank.DataSource = dv.ToTable();
                }
                // gvBank.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gvBank_FocusedRowChanged);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        /// <summary>
        /// Get budget details.
        /// </summary>
        private void SetBudgetSource()
        {
            //On 11/12/2019, to show budget new projects ---------------------------------------------
            if (settinguserProperty.CreateBudgetDevNewProjects == 0 &&
                (ReportProperty.Current.ReportId == "RPT-179" || ReportProperty.Current.ReportId == "RPT-189"))
            {
                lcGrpBudgetNewProject.Visibility = LayoutVisibility.Always;
                //lcGrpBudgetNewProject.Text = "Budget for New Projects - " + ReportProperty.Current.DateSet.ToDate(settingProperty.YearFrom, false).ToString("MMM yyyy") +
                //                                         " - " + ReportProperty.Current.DateSet.ToDate(settingProperty.YearTo, false).ToString("MMM yyyy");
                string newBudgetProjectTitle = "Projects";
                string newBudgetProjectCaption = "New Projects";
                string ExpenditureCaption = "Expenditure";
                string IncomeCaption = "Income";
                string IncomeGovtCaption = "Govt./Foreign";
                string IncomeProvinceCaption = "Province Help";

                string newBudgetProjectCaptionToolTip = "New Projects";
                string ExpenditureCaptionToolTip = "Expenditure";
                string IncomeCaptionToolTip = "Income";
                string IncomeGovtCaptionToolTip = "From Govt./ Foreign Agencies";
                string IncomeProvinceCaptionToolTip = "Province Help";

                if (settinguserProperty.ConsiderBudgetNewProject == 1)
                {
                    newBudgetProjectTitle = "Developmental Projects";
                    newBudgetProjectCaption = "Activity/Project";
                    ExpenditureCaption = "Total Cost";
                    IncomeCaption = "Own/Local";
                    IncomeGovtCaption = "Govt./Foreign";
                    IncomeProvinceCaption = "Province";

                    newBudgetProjectCaptionToolTip = "Activity / Project";
                    ExpenditureCaptionToolTip = "Total Cost of the Project/Activity";
                    IncomeCaptionToolTip = "From Own / Local Sources";
                    IncomeGovtCaptionToolTip = "From Govt./ Foreign Agencies";
                    IncomeProvinceCaptionToolTip = "Province Help";
                }

                ColBudgetNewProject.Caption = newBudgetProjectCaption;
                ColBudgetNewProject.ToolTip = newBudgetProjectCaptionToolTip;

                ColBudgetPExpenseAmount.Caption = ExpenditureCaption;
                ColBudgetPExpenseAmount.ToolTip = ExpenditureCaptionToolTip;

                ColBudgetPIncomeAmount.Caption = IncomeCaption;
                ColBudgetPIncomeAmount.ToolTip = IncomeCaptionToolTip;

                ColBudgetPGovtIncomeAmount.Caption = IncomeGovtCaption;
                ColBudgetPGovtIncomeAmount.ToolTip = IncomeGovtCaptionToolTip;

                ColPProvinceHelp.Caption = IncomeProvinceCaption;
                ColPProvinceHelp.ToolTip = IncomeProvinceCaptionToolTip;

                //ColBudgetPGovtIncomeAmount.Visible = ColBudgetPGovtIncomeAmount.Visible = ColPProvinceHelp.Visible = ColBudgetPRemakrs.Visible = (settinguserProperty.ConsiderBudgetNewProject == 1);
                ColBudgetPGovtIncomeAmount.Visible = ColBudgetPGovtIncomeAmount.Visible = ColBudgetPRemakrs.Visible = (settinguserProperty.ConsiderBudgetNewProject == 1);

                lcGrpBudgetNewProject.Text = "For New " + newBudgetProjectTitle + " - " + ReportProperty.Current.DateSet.ToDate(settinguserProperty.YearFrom, false).ToString("MMM yyyy") +
                                                             " - " + ReportProperty.Current.DateSet.ToDate(settinguserProperty.YearTo, false).ToString("MMM yyyy");

                lcGrpBudgetGroup.Height = 150;

            }
            else
            {
                lcGrpBudgetNewProject.Visibility = LayoutVisibility.Never;
                //lcGrpBudgetGroup.Visibility = LayoutVisibility.Always;
            }
            //-----------------------------------------------------------------------------------------

            try
            {
                SQLCommand.Budget sQLCommandBudgetFetchBudgetNames = SQLCommand.Budget.FetchBudgetNames;
                if (settinguserProperty.IS_DIOMYS_DIOCESE && ReportProperty.Current.ReportId == "RPT-180") //# 03/01/2019, show all only few Budget names list two months
                {
                    sQLCommandBudgetFetchBudgetNames = SQLCommand.Budget.FetchBudgetNamesByTwoMonths;
                }

                using (DataManager dataManager = new DataManager(sQLCommandBudgetFetchBudgetNames))
                {
                    string Pid = string.IsNullOrEmpty(SelectedProject()) ? "0" : SelectedProject();

                    //Date from must be calender date, it will cover, calender budget of this year
                    DateTime DateCalenderFrom = new DateTime(ReportProperty.Current.DateSet.ToDate(this.settinguserProperty.YearFrom, false).Year, 1, 1);
                    DateTime DateCalenderTo = new DateTime(ReportProperty.Current.DateSet.ToDate(this.settinguserProperty.YearTo, false).Year, 12, 31);

                    //show only current financcial year budgets, only for AB
                    if (settinguserProperty.IS_ABEBEN_DIOCESE)
                    {
                        DateCalenderFrom = ReportProperty.Current.DateSet.ToDate(this.settinguserProperty.YearFrom, false);
                        DateCalenderTo = ReportProperty.Current.DateSet.ToDate(this.settinguserProperty.YearTo, false);
                    }

                    dataManager.Parameters.Add(this.appSchema.Budget.DATE_FROMColumn, ReportProperty.Current.DateSet.ToDate(this.settinguserProperty.YearFrom, false));
                    dataManager.Parameters.Add(this.appSchema.Budget.DATE_TOColumn, ReportProperty.Current.DateSet.ToDate(this.settinguserProperty.YearTo, false));

                    //dataManager.Parameters.Add(this.appSchema.Budget.DATE_FROMColumn, DateFrom.DateTime);
                    //dataManager.Parameters.Add(this.appSchema.Budget.DATE_TOColumn, DateTo.DateTime);

                    dataManager.Parameters.Add(this.appSchema.AccountingPeriod.YEAR_FROMColumn, DateCalenderFrom);
                    dataManager.Parameters.Add(this.appSchema.AccountingPeriod.YEAR_TOColumn, DateCalenderTo);
                    dataManager.Parameters.Add(this.appSchema.Project.PROJECT_IDColumn, Pid);

                    if (!settinguserProperty.IsFullRightsReservedUser)
                    {
                        dataManager.Parameters.Add(this.appSchema.UserRole.USERROLE_IDColumn, settinguserProperty.RoleId);
                    }

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                    {
                        if (resultArgs.DataSource.Table != null)
                        {
                            resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                            BudgetSelected = resultArgs.DataSource.Table;
                            ReportProperty.Current.RecordCount = resultArgs.DataSource.Table.Rows.Count;
                            gcBudget.DataSource = null;
                            gcBudget.DataSource = resultArgs.DataSource.Table;
                            gcBudget.RefreshDataSource();
                        }
                    }
                    else
                    {
                        MessageRender.ShowMessage(resultArgs.Message);
                    }
                    //}


                    if (!string.IsNullOrEmpty(ReportProperty.Current.Budget) && ReportProperty.Current.Budget != "0")
                    {
                        DataTable dtBudget = (DataTable)gcBudget.DataSource;
                        string[] BudgetId = ReportProperty.Current.Budget.ToString().Split(',');
                        foreach (DataRow dr in dtBudget.Rows)
                        {
                            for (int i = 0; i < BudgetId.Length; i++)
                            {
                                if (dr[this.appSchema.Budget.BUDGET_IDColumn.ColumnName].ToString() == BudgetId[i] ||
                                    dr[this.appSchema.Budget.BUDGET_IDColumn.ColumnName].ToString() == ReportProperty.Current.Budget)
                                {
                                    dr[SELECT] = 1;
                                }
                            }

                            //DataView dv = new DataView(dtBudget);
                            //dv.Sort = SELECT + " DESC";
                            gcBudget.DataSource = dtBudget;//dv.ToTable();
                        }
                    }
                }

                colBudgetMonthRowName.Visible = false;
                if (ReportProperty.Current.ReportId == "RPT-180")
                {
                    colBudgetMonthRowName.Visible = true;
                }

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.StackTrace, true);
            }
            finally { }
        }

        private void SetStockItemSource()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.StockPurchaseSales.FetchItem))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        if (!resultArgs.DataSource.Table.Columns.Contains(SELECT))
                        {
                            resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                        }
                        gcItem.DataSource = resultArgs.DataSource.Table;
                    }

                    if (!string.IsNullOrEmpty(ReportProperty.Current.StockItemId) && !ReportProperty.Current.StockItemId.Equals("0"))
                    {
                        string[] StockItemId = ReportProperty.Current.StockItemId.Split(',');

                        foreach (string item in StockItemId)
                        {
                            foreach (DataRow drItem in (gcItem.DataSource as DataTable).Rows)
                            {
                                if (ReportProperty.Current.NumberSet.ToInteger(item).Equals(ReportProperty.Current.NumberSet.ToInteger(drItem[this.appSchema.StockItemTransfer.ITEM_IDColumn.ColumnName].ToString())))
                                {
                                    drItem[SELECT] = 1;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally { }
        }

        private void SetAssetPurchaseSource()
        {
            try
            {
                //    using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchAssetItemNameForReport))
                //    {
                //        resultArgs = dataManager.FetchData(DataSource.DataTable);
                //        if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                //        {
                //            if (!resultArgs.DataSource.Table.Columns.Contains(SELECT))
                //            {
                //                resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                //            }
                //            gcItem.DataSource = resultArgs.DataSource.Table;
                //        }

                //        if (!string.IsNullOrEmpty(ReportProperty.Current.AssetItemID) && !ReportProperty.Current.AssetItemID.Equals("0"))
                //        {
                //            string[] AssetItemId = ReportProperty.Current.AssetItemID.Split(',');

                //            foreach (string item in AssetItemId)
                //            {
                //                foreach (DataRow drItem in (gcItem.DataSource as DataTable).Rows)
                //                {
                //                    if (ReportProperty.Current.NumberSet.ToInteger(item).Equals(ReportProperty.Current.NumberSet.ToInteger(drItem[this.appSchema.ASSETItem.ITEM_IDColumn.ColumnName].ToString())))
                //                    {
                //                        drItem[SELECT] = 1;
                //                    }
                //                }
                //            }
                //        }
                //    }
            }
            catch (Exception ex)
            {
            }
            finally { }
        }
        private void SetAssetSalesSource()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchAssetSalesNameForReport))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        if (!resultArgs.DataSource.Table.Columns.Contains(SELECT))
                        {
                            resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                        }
                        gcItem.DataSource = resultArgs.DataSource.Table;
                    }

                    if (!string.IsNullOrEmpty(ReportProperty.Current.AssetItemID) && !ReportProperty.Current.AssetItemID.Equals("0"))
                    {
                        string[] AssetItemId = ReportProperty.Current.AssetItemID.Split(',');

                        foreach (string item in AssetItemId)
                        {
                            foreach (DataRow drItem in (gcItem.DataSource as DataTable).Rows)
                            {
                                if (ReportProperty.Current.NumberSet.ToInteger(item).Equals(ReportProperty.Current.NumberSet.ToInteger(drItem[this.appSchema.ASSETItem.ITEM_IDColumn.ColumnName].ToString())))
                                {
                                    drItem[SELECT] = 1;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally { }
        }
        private void SetAssetReceiveSource()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchAssetReceiveNameForReport))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        if (!resultArgs.DataSource.Table.Columns.Contains(SELECT))
                        {
                            resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                        }
                        gcItem.DataSource = resultArgs.DataSource.Table;
                    }

                    if (!string.IsNullOrEmpty(ReportProperty.Current.AssetItemID) && !ReportProperty.Current.AssetItemID.Equals("0"))
                    {
                        string[] AssetItemId = ReportProperty.Current.AssetItemID.Split(',');

                        foreach (string item in AssetItemId)
                        {
                            foreach (DataRow drItem in (gcItem.DataSource as DataTable).Rows)
                            {
                                if (ReportProperty.Current.NumberSet.ToInteger(item).Equals(ReportProperty.Current.NumberSet.ToInteger(drItem[this.appSchema.ASSETItem.ITEM_IDColumn.ColumnName].ToString())))
                                {
                                    drItem[SELECT] = 1;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally { }
        }
        private void SetAssetDisposalSource()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchAssetDisposalNameForReport))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        if (!resultArgs.DataSource.Table.Columns.Contains(SELECT))
                        {
                            resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                        }
                        gcItem.DataSource = resultArgs.DataSource.Table;
                    }

                    if (!string.IsNullOrEmpty(ReportProperty.Current.AssetItemID) && !ReportProperty.Current.AssetItemID.Equals("0"))
                    {
                        string[] AssetItemId = ReportProperty.Current.AssetItemID.Split(',');

                        foreach (string item in AssetItemId)
                        {
                            foreach (DataRow drItem in (gcItem.DataSource as DataTable).Rows)
                            {
                                if (ReportProperty.Current.NumberSet.ToInteger(item).Equals(ReportProperty.Current.NumberSet.ToInteger(drItem[this.appSchema.ASSETItem.ITEM_IDColumn.ColumnName].ToString())))
                                {
                                    drItem[SELECT] = 1;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally { }
        }
        private void SetAssetRegisterSummarySource()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchAssetRegisterSummaryReport))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        if (!resultArgs.DataSource.Table.Columns.Contains(SELECT))
                        {
                            resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                        }
                        gcItem.DataSource = resultArgs.DataSource.Table;
                    }

                    if (!string.IsNullOrEmpty(ReportProperty.Current.AssetItemID) && !ReportProperty.Current.AssetItemID.Equals("0"))
                    {
                        string[] AssetItemId = ReportProperty.Current.AssetItemID.Split(',');

                        foreach (string item in AssetItemId)
                        {
                            foreach (DataRow drItem in (gcItem.DataSource as DataTable).Rows)
                            {
                                if (ReportProperty.Current.NumberSet.ToInteger(item).Equals(ReportProperty.Current.NumberSet.ToInteger(drItem[this.appSchema.ASSETItem.ITEM_IDColumn.ColumnName].ToString())))
                                {
                                    drItem[SELECT] = 1;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally { }
        }

        private void SetLocationSource()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AssetLocation.FetchAll))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        if (!resultArgs.DataSource.Table.Columns.Contains(SELECT))
                        {
                            resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                        }
                        gcLocation.DataSource = resultArgs.DataSource.Table;
                    }

                    if (!string.IsNullOrEmpty(ReportProperty.Current.LocationId) && !ReportProperty.Current.LocationId.Equals("0"))
                    {
                        string[] LocationId = ReportProperty.Current.LocationId.Split(',');

                        foreach (string item in LocationId)
                        {
                            foreach (DataRow drLocation in (gcLocation.DataSource as DataTable).Rows)
                            {
                                if (ReportProperty.Current.NumberSet.ToInteger(item).Equals(ReportProperty.Current.NumberSet.ToInteger(drLocation[this.appSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName].ToString())))
                                {
                                    drLocation[SELECT] = 1;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally { }
        }
        private void SetRegistrationTypeSource()
        {
            DataTable dtRegistrationType = new DataTable();
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.DonorProspect.FetchRegistrationType))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        if (!resultArgs.DataSource.Table.Columns.Contains(SELECT))
                        {
                            resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                        }
                        dtRegistrationType = resultArgs.DataSource.Table;
                        gcRegistrationType.DataSource = dtRegistrationType;
                    }

                    if (!string.IsNullOrEmpty(ReportProperty.Current.RegistrationTypeId) && !ReportProperty.Current.RegistrationTypeId.Equals("0"))
                    {
                        string[] RegistrationTypeID = ReportProperty.Current.RegistrationTypeId.Split(',');

                        foreach (string item in RegistrationTypeID)
                        {
                            foreach (DataRow drRegistrationType in (gcRegistrationType.DataSource as DataTable).Rows)
                            {
                                if (ReportProperty.Current.NumberSet.ToInteger(item).Equals(ReportProperty.Current.NumberSet.ToInteger(drRegistrationType[this.appSchema.DonorProspects.REGISTRATION_TYPE_IDColumn.ColumnName].ToString())))
                                {
                                    drRegistrationType[SELECT] = 1;
                                }
                            }
                        }
                        DataView dv = new DataView(dtRegistrationType);
                        dv.Sort = SELECT + " DESC";
                        dtRegistrationType = dv.ToTable();
                        gcRegistrationType.DataSource = dtRegistrationType;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally { }
        }
        private void SetCountrySource()
        {
            DataTable dtCountrySource = new DataTable();
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Country.FetchAll))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        if (!resultArgs.DataSource.Table.Columns.Contains(SELECT))
                        {
                            resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                        }
                        dtCountrySource = resultArgs.DataSource.Table;
                        gcCountry.DataSource = dtCountrySource;
                    }

                    if (!string.IsNullOrEmpty(ReportProperty.Current.CountryId) && !ReportProperty.Current.CountryId.Equals("0"))
                    {
                        string[] CountryID = ReportProperty.Current.CountryId.Split(',');

                        foreach (string item in CountryID)
                        {
                            foreach (DataRow drCountry in (gcCountry.DataSource as DataTable).Rows)
                            {
                                if (ReportProperty.Current.NumberSet.ToInteger(item).Equals(ReportProperty.Current.NumberSet.ToInteger(drCountry[this.appSchema.Country.COUNTRY_IDColumn.ColumnName].ToString())))
                                {
                                    drCountry[SELECT] = 1;
                                }
                            }
                        }
                        DataView dv = new DataView(dtCountrySource);
                        dv.Sort = SELECT + " DESC";
                        dtCountrySource = dv.ToTable();
                        gcCountry.DataSource = dtCountrySource;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally { }
        }

        private void SetStateSource()
        {
            DataTable dtStateSource = new DataTable();
            try
            {
                using (DataManager dataManager = new DataManager(!string.IsNullOrEmpty(ReportProperty.Current.StateId) &&
                    ReportProperty.Current.StateId != "0" ? SQLCommand.State.FetchStateByCountryID : SQLCommand.State.FetchAll))
                {
                    if (!string.IsNullOrEmpty(ReportProperty.Current.CountryId) && ReportProperty.Current.CountryId != "0")
                        dataManager.Parameters.Add(this.appSchema.Country.COUNTRY_IDColumn, ReportProperty.Current.CountryId);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        if (!resultArgs.DataSource.Table.Columns.Contains(SELECT))
                        {
                            resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                        }
                        dtStateSource = resultArgs.DataSource.Table;
                        gcState.DataSource = dtStateSource;
                    }

                    if (!string.IsNullOrEmpty(ReportProperty.Current.StateId) && !ReportProperty.Current.StateId.Equals("0"))
                    {
                        string[] StateID = ReportProperty.Current.StateId.Split(',');

                        foreach (string item in StateID)
                        {
                            foreach (DataRow drState in (gcState.DataSource as DataTable).Rows)
                            {
                                if (ReportProperty.Current.NumberSet.ToInteger(item).Equals(ReportProperty.Current.NumberSet.ToInteger(drState[this.appSchema.State.STATE_IDColumn.ColumnName].ToString())))
                                {
                                    drState[SELECT] = 1;
                                }
                            }
                        }
                        DataView dv = new DataView(dtStateSource);
                        dv.Sort = SELECT + " DESC";
                        dtStateSource = dv.ToTable();
                        gcState.DataSource = dtStateSource;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally { }
        }

        private void SetLanguageSource()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.DonorProspect.GetLanguage))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        if (!resultArgs.DataSource.Table.Columns.Contains(SELECT))
                        {
                            resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                        }
                        gcLanguage.DataSource = resultArgs.DataSource.Table;
                    }

                    if (!string.IsNullOrEmpty(ReportProperty.Current.Language))
                    {
                        string[] Language = ReportProperty.Current.CountryId.Split(',');

                        foreach (string item in Language)
                        {
                            foreach (DataRow drLanguage in (gcLanguage.DataSource as DataTable).Rows)
                            {
                                if (ReportProperty.Current.Language.Equals(drLanguage[this.appSchema.DonorProspects.LANGUAGEColumn.ColumnName].ToString()))
                                {
                                    drLanguage[SELECT] = 1;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally { }
        }

        private void SetStateDonaudSource()
        {
            DataTable dtStateDonaudSource = new DataTable();
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.DonorProspect.GetStateDonaud))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        if (!resultArgs.DataSource.Table.Columns.Contains(SELECT))
                        {
                            resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                        }
                        dtStateDonaudSource = resultArgs.DataSource.Table;
                        gcStateDonaud.DataSource = dtStateDonaudSource;
                    }

                    if (!string.IsNullOrEmpty(ReportProperty.Current.StateDonaud))
                    {
                        string[] StateDonaud = ReportProperty.Current.StateDonaud.Split(',');

                        foreach (string item in StateDonaud)
                        {
                            foreach (DataRow drStateDonaud in (gcStateDonaud.DataSource as DataTable).Rows)
                            {
                                if (ReportProperty.Current.StateDonaud.Equals(drStateDonaud[this.appSchema.State.STATE_IDColumn.ColumnName].ToString()))
                                {
                                    drStateDonaud[SELECT] = 1;
                                }
                            }
                        }
                        DataView dv = new DataView(dtStateDonaudSource);
                        dv.Sort = SELECT + " DESC";
                        dtStateDonaudSource = dv.ToTable();
                        gcStateDonaud.DataSource = dtStateDonaudSource;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally { }
        }

        private void SetDonaudSource()
        {
            DataTable dtDonaudSource = new DataTable();
            try
            {
                using (DataManager dataManager = new DataManager(!string.IsNullOrEmpty(ReportProperty.Current.StateDonaud) &&
                    ReportProperty.Current.StateDonaud != "0" ? SQLCommand.DonorProspect.GetDonaudByStateID : SQLCommand.DonorProspect.GetStateDonaud))
                {
                    if (!string.IsNullOrEmpty(ReportProperty.Current.StateDonaud) && ReportProperty.Current.StateDonaud != "0")
                    {
                        dataManager.Parameters.Add(this.appSchema.Project.PROJECT_IDColumn, ReportProperty.Current.StateDonaud);
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        if (!resultArgs.DataSource.Table.Columns.Contains(SELECT))
                        {
                            resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                        }
                        dtDonaudSource = resultArgs.DataSource.Table;
                        DataView dv = dtDonaudSource.AsDataView();
                        dv.RowFilter = "NAME is not null";
                        gcDonaud.DataSource = dv.ToTable();    //dtDonaudSource;
                    }

                    if (!string.IsNullOrEmpty(ReportProperty.Current.DonarName))
                    {
                        string[] Donar = ReportProperty.Current.DonarName.Split(',');

                        foreach (string item in Donar)
                        {
                            foreach (DataRow drDonar in (gcDonaud.DataSource as DataTable).Rows)
                            {
                                if (ReportProperty.Current.DonarName.Equals(drDonar[this.appSchema.DonorAuditor.DONAUD_IDColumn.ColumnName].ToString()))
                                {
                                    drDonar[SELECT] = 1;
                                }
                            }
                        }
                        DataView dv = new DataView(dtDonaudSource);
                        dv.Sort = SELECT + " DESC";
                        dtDonaudSource = dv.ToTable();
                        gcDonaud.DataSource = dtDonaudSource;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally { }
        }

        private void SetTaskNameSource()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.DonorProspect.GetTagID))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        glkpFeestDatTask.Properties.DataSource = resultArgs.DataSource.Table;
                        glkpFeestDatTask.Properties.ValueMember = "TAG_ID";
                        glkpFeestDatTask.Properties.DisplayMember = "TAG_NAME";
                    }

                    if (ReportProperty.Current.BudgetEditValue == 0)
                    {
                        glkpFeestDatTask.EditValue = glkpFeestDatTask.Properties.GetKeyValue(0);
                    }
                    else
                        glkpFeestDatTask.EditValue = ReportProperty.Current.TaskID;
                }
            }
            catch (Exception ex)
            {
            }
            finally { }

        }

        /// <summary>
        /// Re assign the selected cost center details.
        /// </summary>
        private void AssignSelectedTaskSource()
        {
            DataTable dtCheckedtask = new DataTable();
            DataTable dtTask = new DataTable();
            DataTable dtUnselectedProject = new DataTable();
            try
            {
                if (!String.IsNullOrEmpty(ReportProperty.Current.SelectedTaskName) && ReportProperty.Current.SelectedTaskName != "0")
                {
                    SetTaskSource();
                    if (TaskSelected != null)
                    {
                        dtCheckedtask = TaskSelected.Clone();
                        dtTask = TaskSelected.Copy();
                        string[] Task = ReportProperty.Current.SelectedTaskName.Split(',');
                        for (int i = 0; i < TaskSelected.Rows.Count; i++)
                        {
                            for (int j = 0; j < Task.Length; j++)
                            {
                                if (TaskSelected.Rows[i]["TAG_ID"].ToString() == Task[j].ToString())
                                {
                                    DataRow drTask = dtCheckedtask.NewRow();
                                    drTask["TAG_ID"] = ReportProperty.Current.NumberSet.ToInteger(TaskSelected.Rows[i][0].ToString());
                                    drTask["TAG_NAME"] = TaskSelected.Rows[i][1].ToString();
                                    drTask["SELECT"] = 1;
                                    dtCheckedtask.Rows.Add(drTask);
                                }
                            }
                        }
                        //DataTable dtTaskNew = RemoveChecked(dtTask, dtCheckedtask, "TAG_ID", "TAG_ID");

                        if (dtTask.Rows.Count != dtCheckedtask.Rows.Count)
                        {
                            var UncheckVouchers = dtTask.AsEnumerable()
                                          .Where(row => !dtCheckedtask.AsEnumerable()
                                                                .Select(r => r.Field<UInt32>("TAG_ID"))
                                                                .Any(x => x == row.Field<UInt32>("TAG_ID"))
                                         ).CopyToDataTable();
                            dtUnselectedProject = UncheckVouchers.DefaultView.Table;
                        }
                        DataTable dtTaskNew = dtUnselectedProject;

                        if (dtTaskNew != null && dtTaskNew.Rows.Count != 0)
                        {
                            dtCheckedtask.Merge(dtTaskNew);
                        }
                        else
                        {
                            chkSelectAllTask.Checked = true;
                        }

                        DataView dvCheckedtask = dtCheckedtask.DefaultView;
                        //dvCheckedCostCentre.RowFilter = "PROJECT_ID IN (" + ReportProperty.Current.Project + ")";
                        DataView dv = dvCheckedtask;
                        dv.Sort = SELECT + " DESC";
                        gcTasks.DataSource = dv.ToTable();
                        gcTasks.DataSource = dv.ToTable();
                        gcTasks.RefreshDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void SetTaskSource()
        {
            DataTable dtTaskSource = new DataTable();
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.DonorProspect.GetDonorTags))
                {
                    //if (!string.IsNullOrEmpty(ReportProperty.Current.SelectedTaskName) && ReportProperty.Current.SelectedTaskName != "0")
                    //{
                    //    dataManager.Parameters.Add(this.appSchema.DonorTags.TAG_IDColumn, ReportProperty.Current.SelectedTaskName);
                    //}
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                    {
                        if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                        {
                            resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                            TaskSelected = resultArgs.DataSource.Table;
                            gcTasks.DataSource = resultArgs.DataSource.Table;
                            gcTasks.RefreshDataSource();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally { }
        }

        /// <summary>
        /// Get budget details for grildlookup.
        /// </summary>
        private void SetBudgetSourceforGridLookup()
        {
            try
            {
                gvProject.UpdateCurrentRow();

                int[] rows = gvProject.GetSelectedRows();
                string ProjectId = SelectedProject();
                //foreach (int j in df)
                //{
                //    DataRow row = gvProject.GetDataRow(j);
                //    ProjectId += row[this.appSchema.Project.PROJECT_IDColumn.ColumnName] + ",";
                //}
                //ProjectId = ProjectId.TrimEnd(',');

                if (ProjectId != "0" && ProjectId != string.Empty)
                {
                    DataTable dtProjectId = (DataTable)gcProject.DataSource;
                    if (dtProjectId != null && dtProjectId.Rows.Count != 0)
                    {
                        glkpBudget.Properties.DataSource = FetchBudgetsByProjects(ProjectId, false);
                        glkpBudget.Properties.DisplayMember = "BANK"; //BUDGER_NAME has been renamed to BANK for reusability
                        glkpBudget.Properties.ValueMember = "BANK_ID"; //BUDGET_ID has been renamed to BANK_ID for reusability
                        if (ReportProperty.Current.BudgetEditValue == 0)
                        {
                            glkpBudget.EditValue = glkpBudget.Properties.GetKeyValue(0);
                        }
                        else
                            glkpBudget.EditValue = ReportProperty.Current.BudgetEditValue;
                        BindCompareBudget();
                    }
                }
                else
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchBudgetNames))
                    {
                        dataManager.Parameters.Add(this.appSchema.Budget.DATE_FROMColumn, ReportProperty.Current.DateSet.ToDate(this.settinguserProperty.YearFrom, false));
                        dataManager.Parameters.Add(this.appSchema.Budget.DATE_TOColumn, ReportProperty.Current.DateSet.ToDate(this.settinguserProperty.YearTo, false));
                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                        if (resultArgs.Success)
                        {
                            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                            {
                                resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                                BudgetSelected = resultArgs.DataSource.Table;
                                ReportProperty.Current.RecordCount = resultArgs.DataSource.Table.Rows.Count;
                                glkpBudget.Properties.DataSource = resultArgs.DataSource.Table;
                                glkpBudget.Properties.DisplayMember = "BANK"; //BUDGER_NAME has been renamed to BANK for reusability
                                glkpBudget.Properties.ValueMember = "BANK_ID"; //BUDGET_ID has been renamed to BANK_ID for reusability
                                glkpBudget.EditValue = glkpBudget.Properties.GetKeyValue(0);
                                BindCompareBudget();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.StackTrace, true);
            }
            finally { }
        }

        private void BindCompareBudget()
        {
            //Binding Budget to compare
            DataTable dtBudget = glkpBudget.Properties.DataSource as DataTable;
            if (dtBudget != null)
            {
                DataView dvFilteredBudget = new DataView(dtBudget);
                dvFilteredBudget.RowFilter = String.Format("BANK_ID<>{0}", glkpBudget.EditValue != null ? ReportProperty.Current.NumberSet.ToInteger(glkpBudget.EditValue.ToString()) : 0);
                glkpBudgetCompare.Properties.DataSource = null;
                glkpBudgetCompare.Properties.DataSource = dvFilteredBudget.ToTable();
                glkpBudgetCompare.Properties.DisplayMember = "BANK"; //BUDGER_NAME has been renamed to BANK for reusability
                glkpBudgetCompare.Properties.ValueMember = "BANK_ID"; //BUDGET_ID has been renamed to BANK_ID for reusability
                if (ReportProperty.Current.BudgetCompareEditValue == 0)
                {
                    glkpBudgetCompare.EditValue = glkpBudgetCompare.Properties.GetKeyValue(0);
                }
                else
                    glkpBudgetCompare.EditValue = ReportProperty.Current.BudgetCompareEditValue;
            }
            else
            {
                glkpBudgetCompare.Properties.DataSource = null;
            }
        }

        /// <summary>
        /// Get the selected project id 
        /// </summary>
        /// <returns></returns>
        private string SelectedProject()
        {
            string selectedProjectId = string.Empty;
            string ProjectName = string.Empty;
            string ProjectNameWithNo = string.Empty;
            string ProjectNameWithDivision = string.Empty;

            string ProjectITRGroup = string.Empty;
            string ProjectITRGroupIds = string.Empty;

            ReportProperty.Current.dtProjectSelected = null;
            ReportProperty.Current.LedgalEntityId = string.Empty;
            ReportProperty.Current.AllProjectsCount = 0;
            try
            {
                DataTable dtProject = gcProject.DataSource as DataTable;
                if (dtProject != null && dtProject.Rows.Count != 0)
                {
                    ReportProperty.Current.AllProjectsCount = dtProject.Rows.Count;

                    var Selected = (from d in dtProject.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);
                    if (Selected.Count() > 0)
                    {
                        ProjectSelected = Selected.CopyToDataTable();
                        ReportProperty.Current.SelectedProjectCount = ProjectSelected.Rows.Count;
                        if (ProjectSelected != null && ProjectSelected.Rows.Count > 0)
                        {
                            ProjectSelected.DefaultView.Sort = this.appSchema.Project.PROJECTColumn.ColumnName;

                            foreach (DataRow dr in ProjectSelected.Rows)
                            {
                                selectedProjectId += dr[this.appSchema.Project.PROJECT_IDColumn.ColumnName] + ",";
                                ProjectName += dr[this.appSchema.Project.PROJECTColumn.ColumnName].ToString() + ",";
                                ReportProperty.Current.LedgalEntityId += dr[this.appSchema.LegalEntity.CUSTOMERIDColumn.ColumnName].ToString() + ",";
                                ProjectNameWithDivision += dr["PROJECT_NAME"].ToString() + ",";

                                if (dr.Table.Columns.Contains("PROJECT_CATOGORY_ITRGROUP_ID"))
                                {
                                    ProjectITRGroup += dr["PROJECT_CATOGORY_ITRGROUP_ID"].ToString() + ",";
                                    ProjectITRGroupIds += dr["PROJECT_CATOGORY_ITRGROUP"].ToString() + ",";
                                }
                            }
                            selectedProjectId = selectedProjectId.TrimEnd(',');
                            ProjectName = ProjectName.TrimEnd(',');
                            ProjectNameWithDivision = ProjectNameWithDivision.TrimEnd(',');
                            ProjectITRGroup = ProjectITRGroup.TrimEnd(',');
                            ProjectITRGroupIds = ProjectITRGroupIds.TrimEnd(',');
                            ReportProperty.Current.ProjectITRGroup = ProjectITRGroup;
                            ReportProperty.Current.ProjectITRGroupIds = ProjectITRGroupIds;

                            ReportProperty.Current.ProjectNameWithoutDivision = ProjectNameWithDivision;
                            ReportProperty.Current.LedgalEntityId = ReportProperty.Current.LedgalEntityId.TrimEnd(',');

                            // 30/12/2024 ----------------------------------------------------------------------------
                            //To get projectname with no order by projectid---------------------------
                            ProjectSelected.DefaultView.Sort = this.appSchema.Project.PROJECT_IDColumn.ColumnName;
                            DataTable dtProjectsByProjectId = ProjectSelected.DefaultView.ToTable();
                            foreach (DataRow dr in dtProjectsByProjectId.Rows)
                            {
                                ProjectNameWithNo += "P" + (dtProjectsByProjectId.Rows.IndexOf(dr) + 1).ToString() + " - " +
                                                    dr[this.appSchema.Project.PROJECTColumn.ColumnName].ToString() + Environment.NewLine;
                            }
                            ReportProperty.Current.ProjectNamewithSno = ProjectNameWithNo;

                            if (ReportProperty.Current.ReportId == "RPT-228")
                            {
                                // This is to group the ITRGroup wise Project details
                                string Combineoutput = "";

                                // Create a dictionary to group projects by category
                                Dictionary<string, List<string>> groupedProjects = new Dictionary<string, List<string>>();

                                // Iterate through the rows and group projects manually
                                foreach (DataRow dr in dtProjectsByProjectId.Rows)
                                {
                                    string category = dr["PROJECT_CATOGORY_ITRGROUP"].ToString();
                                    string projectDetails = "P" + (dtProjectsByProjectId.Rows.IndexOf(dr) + 1).ToString() + " - " +
                                                            dr["PROJECT"].ToString();

                                    if (!groupedProjects.ContainsKey(category))
                                    {
                                        groupedProjects[category] = new List<string>();
                                    }
                                    groupedProjects[category].Add(projectDetails);
                                }

                                // Construct the output string
                                foreach (var category in groupedProjects.Keys)
                                {
                                    Combineoutput += category + ":\n";
                                    foreach (var project in groupedProjects[category])
                                    {
                                        Combineoutput += project + Environment.NewLine;
                                    }
                                }
                                ReportProperty.Current.ProjectNamewithITRGroup = Combineoutput;
                            }
                            //------------------------------------------------------------------------

                            ProjectSelected.DefaultView.Sort = this.appSchema.Project.PROJECTColumn.ColumnName;

                            if (ProjectName.Trim() != string.Empty)
                            {
                                if (ReportProperty.Current.RecordCount == ProjectSelected.Rows.Count)
                                {
                                    ReportProperty.Current.ShowIndividualProjects = chkShowIndividualProjects.Checked ? 1 : 0;
                                    if (ReportProperty.Current.ShowIndividualProjects == 0)
                                    {
                                        ReportProperty.Current.ProjectTitle = ProjectSelected.Rows.Count == 1 ? "  " + ProjectName : "  " + SCONSTATEMENT;

                                        //On 24/09/2020, for cmf attach Instution Name
                                        if (settinguserProperty.IS_CMF_CONGREGATION)
                                        {
                                            ReportProperty.Current.ProjectTitle = ProjectSelected.Rows.Count == 1 ? ReportProperty.Current.ProjectTitle : settinguserProperty.InstituteName + " - " + SCONSTATEMENT;
                                        }
                                    }
                                    else
                                    {
                                        ReportProperty.Current.ProjectTitle = "  " + ProjectName;
                                    }
                                }
                                else
                                {
                                    ReportProperty.Current.ProjectTitle = "  " + ProjectName;
                                }
                            }
                        }

                        //On 15/11/2024
                        ReportProperty.Current.dtProjectSelected = ProjectSelected.DefaultView.ToTable();
                    }
                }


            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
            return selectedProjectId;
        }

        /// <summary>
        /// Get the selected project id 
        /// </summary>
        /// <returns></returns>
        private string SelectedAssetClass()
        {
            string selectedAssetClassId = string.Empty;
            string AssetClassName = string.Empty;

            ReportProperty.Current.LedgalEntityId = string.Empty;
            try
            {
                DataTable dtAssetclass = gcAssetClass.DataSource as DataTable;
                if (dtAssetclass != null && dtAssetclass.Rows.Count != 0)
                {
                    var Selected = (from d in dtAssetclass.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);
                    if (Selected.Count() > 0)
                    {
                        AssetclassSelected = Selected.CopyToDataTable();
                        ReportProperty.Current.SelectedAssetclassCount = AssetclassSelected.Rows.Count;
                        if (AssetclassSelected != null && AssetclassSelected.Rows.Count > 0)
                        {
                            foreach (DataRow dr in AssetclassSelected.Rows)
                            {
                                selectedAssetClassId += dr[this.appSchema.ASSETClassDetails.ASSET_CLASS_IDColumn.ColumnName] + ",";
                                AssetClassName += dr[this.appSchema.ASSETClassDetails.ASSET_CLASSColumn.ColumnName].ToString() + ",";
                                //ReportProperty.Current.LedgalEntityId += dr[this.appSchema.LegalEntity.CUSTOMERIDColumn.ColumnName].ToString() + ",";
                            }
                            selectedAssetClassId = selectedAssetClassId.TrimEnd(',');
                            AssetClassName = AssetClassName.TrimEnd(',');

                            ReportProperty.Current.LedgalEntityId = ReportProperty.Current.LedgalEntityId.TrimEnd(',');
                            if (AssetClassName.Trim() != string.Empty)
                            {
                                if (ReportProperty.Current.RecordCount == AssetclassSelected.Rows.Count)
                                {
                                    ReportProperty.Current.CostCentreName = AssetclassSelected.Rows.Count == 1 ? "  " + AssetClassName : "  " + SCONSTATEMENT;
                                }
                                else
                                {
                                    ReportProperty.Current.CostCentreName = "  " + AssetClassName;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
            return selectedAssetClassId;
        }

        //private string SelectedPayroll()
        //{
        //    string selectedPayrollId = string.Empty;
        //    string PayrollName = string.Empty;
        //    int SelectedPayrollCount = 0;
        //    ReportProperty.Current.LedgalEntityId = string.Empty;
        //    try
        //    {
        //        DataTable dtProject = gcPayroll.DataSource as DataTable;
        //        if (dtProject != null && dtProject.Rows.Count != 0)
        //        {
        //            foreach (int i in gvPayroll.GetSelectedRows())
        //            {
        //                DataRow row = gvPayroll.GetDataRow(i);
        //                if (row != null)
        //                {
        //                    selectedPayrollId += row["PAYROLLID"].ToString() + ",";
        //                    PayrollName += row["PRNAME"] + ",";
        //                    SelectedPayrollCount++;
        //                }
        //            }
        //            selectedPayrollId = selectedPayrollId.TrimEnd(',');
        //            ReportProperty.Current.SelectedProjectCount = SelectedPayrollCount;
        //            PayrollName = PayrollName.TrimEnd(',');
        //            ReportProperty.Current.LedgalEntityId = ReportProperty.Current.LedgalEntityId.TrimEnd(',');

        //            if (PayrollName.Trim() != string.Empty)
        //            {
        //                if (ReportProperty.Current.RecordCount == SelectedPayrollCount)
        //                {
        //                    ReportProperty.Current.ProjectTitle = SelectedPayrollCount == 1 ? "  " + PayrollName : "  " + SCONSTATEMENT;
        //                }
        //                else
        //                {
        //                    ReportProperty.Current.ProjectTitle = "  " + PayrollName;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
        //    }
        //    finally { }
        //    return selectedPayrollId;
        //}



        /// <summary>
        /// Get the selected cost centre id.
        /// </summary>
        /// <returns></returns>
        private string SelectedCostCentre()
        {
            string SelectedCostCentre = string.Empty;
            string CostCentreName = string.Empty;
            try
            {
                DataTable dtCostCentre = gcCostCentre.DataSource as DataTable;
                if (dtCostCentre != null && dtCostCentre.Rows.Count != 0)
                {
                    DataView dvCostCentre = dtCostCentre.DefaultView;
                    dvCostCentre.RowFilter = "SELECT=1";
                    if (dvCostCentre != null && dvCostCentre.Count > 0)
                    {
                        CostCentreSelected = dvCostCentre.ToTable();

                        foreach (DataRow dr in dvCostCentre.ToTable().Rows)
                        {
                            SelectedCostCentre += dr[this.appSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName] + ",";
                            CostCentreName += dr[this.appSchema.CostCentre.COST_CENTRE_NAMEColumn.ColumnName].ToString() != string.Empty ? dr[this.appSchema.CostCentre.COST_CENTRE_NAMEColumn.ColumnName].ToString() + "," : string.Empty;
                        }
                        SelectedCostCentre = SelectedCostCentre.TrimEnd(',');
                        CostCentreName = CostCentreName.TrimEnd(',');
                        if (CostCentreName.Trim() != string.Empty)
                        {
                            if (CostCentreSelected.Rows.Count == dtCostCentre.Rows.Count)
                            {
                                ReportProperty.Current.CostCentreName = CostCentreSelected.Rows.Count == 1 ? CostCentreName : SCONSTATEMENT;
                            }
                            else
                            {
                                ReportProperty.Current.CostCentreName = CostCentreName;
                            }
                        }
                    }
                    dvCostCentre.RowFilter = "";
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
            return SelectedCostCentre;
        }

        /// <summary>
        /// get bank details id based on the selected bank accuont
        /// </summary>
        /// <returns></returns>
        //private string SelectedBankDetails()
        //{
        //    string selectedBankAccount = string.Empty;
        //    string BankAccountId = string.Empty;
        //    string BankAccountName = string.Empty;
        //    string UnSelectedAccountId = string.Empty;
        //    string SelectedLedgerId = string.Empty;
        //    ReportProperty.Current.Count = 0;
        //    try
        //    {
        //        DataTable dtBank = gcBank.DataSource as DataTable;
        //        if (dtBank != null && dtBank.Rows.Count != 0)
        //        {
        //            foreach (int i in gvBank.GetSelectedRows())
        //            {
        //                DataRow dr = gvBank.GetDataRow(i);
        //                selectedBankAccount += dr[this.appSchema.Bank.BANK_IDColumn.ColumnName] + ",";
        //                BankAccountId += dr[this.appSchema.BankAccount.BANK_ACCOUNT_IDColumn.ColumnName] + ",";
        //                SelectedLedgerId += dr[this.appSchema.Ledger.LEDGER_IDColumn.ColumnName] + ",";
        //                ReportProperty.Current.Count++;
        //                BankAccountName += dr[this.appSchema.Bank.BANKColumn.ColumnName].ToString() + ",";
        //            }
        //            BankAccountName = BankAccountName.TrimEnd(',');
        //            ReportProperty.Current.BankAccountName = ReportProperty.Current.Count == 1 ? BankAccountName : string.Empty;
        //            selectedBankAccount = selectedBankAccount.TrimEnd(',');
        //            BankAccountId = BankAccountId.TrimEnd(',');
        //            SelectedLedgerId = SelectedLedgerId.TrimEnd(',');
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
        //    }
        //    finally { }
        //    return SelectedLedgerId;
        //}

        /// <summary>
        /// get bank details id based on the selected bank accuont
        /// </summary>
        /// <returns></returns>
        private string SelectedBudgetDetails()
        {
            string SelectedBudgetname = string.Empty;
            string BudgetId = string.Empty;
            string UnSelectedAccountId = string.Empty;
            string budgetprojectname = string.Empty;
            string projectIds = string.Empty;
            string budgetdaterangeinMonths = string.Empty;
            string DateFrom = string.Empty;
            string DateTo = string.Empty;

            try
            {
                DataTable dtBudget = gcBudget.DataSource as DataTable;
                if (dtBudget != null && dtBudget.Rows.Count != 0)
                {
                    var Selected = (from d in dtBudget.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);
                    if (Selected.Count() > 0)
                    {
                        BudgetSelected = Selected.CopyToDataTable();
                        if (BudgetSelected != null && BudgetSelected.Rows.Count != 0)
                        {
                            foreach (DataRow dr in BudgetSelected.Rows)
                            {
                                BudgetId += dr[this.appSchema.Budget.BUDGET_IDColumn.ColumnName] + ",";
                                SelectedBudgetname += dr[this.appSchema.Budget.BUDGET_NAMEColumn.ColumnName].ToString().Trim() + ", ";
                                if (!budgetprojectname.Contains(dr[this.appSchema.Project.PROJECTColumn.ColumnName] + ", "))
                                    budgetprojectname += dr[this.appSchema.Project.PROJECTColumn.ColumnName] + ", ";
                                projectIds += dr[this.appSchema.Project.PROJECT_IDColumn.ColumnName] + ",";
                                budgetdaterangeinMonths += ReportProperty.Current.DateSet.ToDate(dr[this.appSchema.Budget.DATE_FROMColumn.ColumnName].ToString(), false).ToString("MMMM") + ", ";
                                DateFrom = ReportProperty.Current.DateSet.ToDate(dr[this.appSchema.Budget.DATE_FROMColumn.ColumnName].ToString(), false).ToShortDateString();
                                DateTo = ReportProperty.Current.DateSet.ToDate(dr[this.appSchema.Budget.DATE_TOColumn.ColumnName].ToString(), false).ToShortDateString();
                            }
                            BudgetId = BudgetId.TrimEnd(',');
                            SelectedBudgetname = SelectedBudgetname.Trim().TrimEnd(',');
                            budgetprojectname = budgetprojectname.Trim().TrimEnd(',');
                            projectIds = projectIds.Trim().TrimEnd(',');
                            budgetdaterangeinMonths = budgetdaterangeinMonths.Trim().TrimEnd(',');

                            if (SelectedBudgetname.Trim() != string.Empty)
                            {
                                ReportProperty.Current.BudgetName = SelectedBudgetname;
                                ReportProperty.Current.BudgetProject = budgetprojectname;
                                //On 12/03/2019, Check Selected Budget Projects and set report projects
                                if (ReportProperty.Current.ReportId == "RPT-046" || ReportProperty.Current.ReportId == "RPT-193" || ReportProperty.Current.ReportId == "RPT-200"
                                    || ReportProperty.Current.ReportId == "RPT-201")
                                {
                                    ReportProperty.Current.Project = AlertBudgetProjects(projectIds); //ReportProperty.Current.Project = projectIds;
                                }

                                if ((ReportProperty.Current.ReportId == "RPT-048"))
                                {
                                    if (!BudgetId.Contains(","))
                                    {
                                        ReportProperty.Current.DateFrom = DateFrom;
                                        ReportProperty.Current.DateTo = DateTo;
                                        ReportProperty.Current.Project = projectIds;
                                    }
                                    else
                                    {
                                        ReportProperty.Current.DateFrom = "";
                                        ReportProperty.Current.DateTo = "";
                                    }
                                }
                                else if ((ReportProperty.Current.ReportId == "RPT-180") || (ReportProperty.Current.ReportId == "RPT-163")) //For Mysore Budget reports
                                {
                                    ReportProperty.Current.DateFrom = DateFrom;
                                    ReportProperty.Current.DateTo = DateTo;
                                    ReportProperty.Current.Project = projectIds;
                                }


                                //set date range
                                ReportProperty.Current.BudgetDateRangeInMonths = budgetdaterangeinMonths;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
            return BudgetId;
        }

        /// <summary>
        /// Get the selected ledger group id
        /// </summary>
        /// <returns></returns>
        private string SelectedLedgerGroup()
        {
            //string SelectedLedgerGroup = string.Empty;
            //try
            //{
            //    DataTable dtLedger = gcLedger.DataSource as DataTable;
            //    if (dtLedger != null && dtLedger.Rows.Count != 0)
            //    {
            //        foreach (int i in gvLedger.GetSelectedRows())
            //        {
            //            DataRow dr = gvLedger.GetDataRow(i);
            //            SelectedLedgerGroup += dr[0] + ",";
            //        }
            //        SelectedLedgerGroup = SelectedLedgerGroup.TrimEnd(',');
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            //}
            //finally { }
            //return SelectedLedgerGroup;
            DataTable dtLedgerGroup = null;
            string SelectedLedgerGroup = string.Empty;
            try
            {
                DataTable dtLedger = gcLedger.DataSource as DataTable;
                if (dtLedger != null && dtLedger.Rows.Count != 0)
                {
                    var Selected = (from d in dtLedger.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);
                    if (Selected.Count() > 0)
                    {
                        dtLedgerGroup = Selected.CopyToDataTable();
                        if (dtLedgerGroup != null && dtLedgerGroup.Rows.Count != 0)
                        {
                            foreach (DataRow dr in dtLedgerGroup.Rows)
                            {
                                SelectedLedgerGroup += dr[0] + ",";
                            }
                            SelectedLedgerGroup = SelectedLedgerGroup.TrimEnd(',');
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
            return SelectedLedgerGroup;
        }

        /// <summary>
        /// Get the selected group details id.
        /// </summary>
        /// <returns></returns>
        private string SelectedLedgerGroupDetails()
        {
            //string UnSelectedLedgerId = string.Empty;
            //string SelectedLedgerDetailsId = string.Empty;
            //string LedgerName = string.Empty;
            //int LedgerCount = 0;
            //try
            //{
            //    DataTable dtLedger = gcLedgerDetail.DataSource as DataTable;
            //    if (dtLedger != null && dtLedger.Rows.Count != 0)
            //    {
            //        foreach (int i in gvLedgerDetails.GetSelectedRows())
            //        {
            //            DataRow dr = gvLedgerDetails.GetDataRow(i);
            //            SelectedLedgerDetailsId += dr[0] + ",";
            //            LedgerName += dr[1] + ",";
            //            LedgerCount++;
            //        }
            //        SelectedLedgerDetailsId = SelectedLedgerDetailsId.TrimEnd(',');
            //        ReportProperty.Current.SelectedLedgerName = LedgerName.TrimEnd(',');
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            //}
            //finally { }
            //return SelectedLedgerDetailsId;
            DataTable dtLedgerDetails = null;
            string UnSelectedLedgerId = string.Empty;
            string SelectedLedgerDetailsId = string.Empty;
            string SelectedLedgerName = string.Empty;
            try
            {
                DataTable dtLedger = gcLedgerDetail.DataSource as DataTable;
                if (dtLedger != null && dtLedger.Rows.Count != 0)
                {
                    var Selected = (from d in dtLedger.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);

                    var UnSelected = (from d in dtLedger.AsEnumerable()
                                      where ((d.Field<Int32?>(SELECT) != 1))
                                      select d);

                    if (Selected.Count() > 0)
                    {
                        dtLedgerDetails = Selected.CopyToDataTable();
                        if (dtLedgerDetails != null && dtLedgerDetails.Rows.Count != 0)
                        {
                            foreach (DataRow dr in dtLedgerDetails.Rows)
                            {
                                SelectedLedgerDetailsId += dr[0] + ",";
                                SelectedLedgerName += dr["LEDGER"] + ",";
                            }
                            SelectedLedgerDetailsId = SelectedLedgerDetailsId.TrimEnd(',');
                            ReportProperty.Current.SelectedLedgerName = dtLedger.Rows.Count != Selected.Count() ? SelectedLedgerName.TrimEnd(',') : string.Empty;
                        }
                    }
                    if (UnSelected.Count() > 0)
                    {
                        DataTable dtUnSelectedLedgerId = UnSelected.CopyToDataTable();
                        if (dtUnSelectedLedgerId != null && dtUnSelectedLedgerId.Rows.Count != 0)
                        {
                            foreach (DataRow dr in dtUnSelectedLedgerId.Rows)
                            {
                                UnSelectedLedgerId += dr[0] + ",";
                            }
                            UnSelectedLedgerId = UnSelectedLedgerId.TrimEnd(',');
                        }
                    }
                    ReportProperty.Current.UnSelectedLedgerId = UnSelectedLedgerId;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
            return SelectedLedgerDetailsId;
        }

        /// <summary>
        /// Get the selected Narration id
        /// </summary>
        /// <returns></returns>
        private string SelectedNarration()
        {
            DataView dvNarration = null;
            string selectedNarration = string.Empty;
            try
            {
                DataTable dtNarraton = gcNarration.DataSource as DataTable;
                if (dtNarraton != null && dtNarraton.Rows.Count != 0)
                {

                    dvNarration = dtNarraton.DefaultView;
                    dvNarration.RowFilter = "SELECT =1";

                    if (dvNarration.Count > 0)
                    {
                        if (dvNarration != null && dvNarration.ToTable().Rows.Count != 0)
                        {
                            foreach (DataRow dr in dvNarration.ToTable().Rows)
                            {
                                selectedNarration += dr[this.appSchema.NatureofPayment.NATURE_PAY_IDColumn.ColumnName] + ",";
                            }
                            selectedNarration = selectedNarration.TrimEnd(',');
                        }
                    }
                    dvNarration.RowFilter = "";
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
            return selectedNarration;
        }

        private string SelectedNatureofPaymentsDetails()
        {
            string NatureofPaymentId = string.Empty;
            try
            {
                DataTable dtNarration = gcNarration.DataSource as DataTable;
                if (dtNarration != null && dtNarration.Rows.Count != 0)
                {
                    var Selected = (from d in dtNarration.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);
                    if (Selected.Count() > 0)
                    {
                        BankSelected = Selected.CopyToDataTable();
                        if (BankSelected != null && BankSelected.Rows.Count != 0)
                        {
                            foreach (DataRow dr in BankSelected.Rows)
                            {
                                NatureofPaymentId += dr[this.appSchema.NatureofPayment.NATURE_PAY_IDColumn.ColumnName] + ",";
                            }
                            NatureofPaymentId = NatureofPaymentId.TrimEnd(',');
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
            return NatureofPaymentId;
        }

        private string SelectedDeductorDetails()
        {
            string DeductorId = string.Empty;
            try
            {
                DataTable dtNarration = gcDeducteeType.DataSource as DataTable;
                if (dtNarration != null && dtNarration.Rows.Count != 0)
                {
                    var Selected = (from d in dtNarration.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);
                    if (Selected.Count() > 0)
                    {
                        BankSelected = Selected.CopyToDataTable();
                        if (BankSelected != null && BankSelected.Rows.Count != 0)
                        {
                            foreach (DataRow dr in BankSelected.Rows)
                            {
                                DeductorId += dr["DEDUCTEE_TYPE_ID"] + ",";
                            }
                            DeductorId = DeductorId.TrimEnd(',');
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
            return DeductorId;
        }

        private string SelectedPartyLedgers()
        {
            string PartyLedger = string.Empty;

            try
            {
                DataTable dtPartyLedger = gcTDSParties.DataSource as DataTable;
                if (dtPartyLedger != null && dtPartyLedger.Rows.Count != 0)
                {
                    var Selected = (from d in dtPartyLedger.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);
                    if (Selected.Count() > 0)
                    {
                        BankSelected = Selected.CopyToDataTable();
                        if (BankSelected != null && BankSelected.Rows.Count != 0)
                        {
                            foreach (DataRow dr in BankSelected.Rows)
                            {
                                PartyLedger += dr["LEDGER_ID"] + ",";
                            }
                            PartyLedger = PartyLedger.TrimEnd(',');
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
            return PartyLedger;
        }

        /// <summary>
        /// Re assign the selected ledger group based on the selected ledger group.
        /// </summary>
        private void AssignSelectedLedgerGroup()
        {
            DataTable dtCheckedLedgers = new DataTable();
            DataTable dtLedgers = new DataTable();
            try
            {
                if (!String.IsNullOrEmpty(ReportProperty.Current.LedgerGroup) && ReportProperty.Current.LedgerGroup != "0")
                {
                    SetLedgerSource();
                    if (LedgerGroupSelected != null)
                    {
                        dtCheckedLedgers = LedgerGroupSelected.Clone();
                        dtLedgers = LedgerGroupSelected.Copy();
                        string[] ledgerGroup = ReportProperty.Current.LedgerGroup.Split(',');
                        for (int i = 0; i < LedgerGroupSelected.Rows.Count; i++)
                        {
                            for (int j = 0; j < ledgerGroup.Length; j++)
                            {
                                if (LedgerGroupSelected.Rows[i][0].ToString() == ledgerGroup[j].ToString())
                                {
                                    DataRow drLedgerAccount = dtCheckedLedgers.NewRow();
                                    drLedgerAccount["GROUP_ID"] = ReportProperty.Current.NumberSet.ToInteger(LedgerGroupSelected.Rows[i][0].ToString());
                                    drLedgerAccount["GROUP"] = LedgerGroupSelected.Rows[i][1].ToString();
                                    drLedgerAccount["SELECT"] = 1;
                                    dtCheckedLedgers.Rows.Add(drLedgerAccount);
                                }
                            }
                        }
                        DataTable dtLedgerAccount = RemoveChecked(dtLedgers, dtCheckedLedgers, dtLedgers.Columns[1].ColumnName, dtCheckedLedgers.Columns[1].ColumnName);
                        if (dtLedgerAccount != null && dtLedgerAccount.Rows.Count != 0)
                        {
                            dtCheckedLedgers.Merge(dtLedgerAccount);
                        }
                        else
                        {
                            this.chkLedger.CheckedChanged -= new System.EventHandler(this.chkLedger_CheckedChanged);
                            chkLedger.Checked = true;
                            this.chkLedger.CheckedChanged += new System.EventHandler(this.chkLedger_CheckedChanged);
                            this.chkLedgerGroup.CheckedChanged -= new System.EventHandler(this.chkLedgerGroup_CheckedChanged);
                            chkLedgerGroup.Checked = true;
                            this.chkLedgerGroup.CheckedChanged += new System.EventHandler(this.chkLedgerGroup_CheckedChanged);
                        }
                        DataView dvCheckedLedger = new DataView(dtCheckedLedgers);
                        dvCheckedLedger.Sort = SELECT + " DESC";
                        gcLedger.DataSource = dvCheckedLedger.ToTable();
                        gcLedger.RefreshDataSource();
                    }
                }
                else
                {
                    SetLedgerSource();
                }
            }

            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        /// <summary>
        /// Get the selected project id 
        /// </summary>
        /// <returns></returns>
        private string SelectedStockItem()
        {
            string stockItem = string.Empty;
            try
            {
                DataTable dtStockItem = gcItem.DataSource as DataTable;
                if (dtStockItem != null && dtStockItem.Rows.Count != 0)
                {
                    var Selected = (from d in dtStockItem.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);
                    if (Selected.Count() > 0)
                    {
                        ProjectSelected = Selected.CopyToDataTable();
                        ReportProperty.Current.SelectedProjectCount = ProjectSelected.Rows.Count;
                        if (ProjectSelected != null && ProjectSelected.Rows.Count > 0)
                        {
                            foreach (DataRow dr in ProjectSelected.Rows)
                            {
                                stockItem += dr[this.appSchema.StockItemTransfer.ITEM_IDColumn.ColumnName] + ",";
                            }
                            stockItem = stockItem.TrimEnd(',');
                            ReportProperty.Current.LedgalEntityId = ReportProperty.Current.LedgalEntityId.TrimEnd(',');
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
            return stockItem;
        }
        private string SelectedAssetItem()
        {
            string assetItem = string.Empty;
            try
            {
                DataTable dtAssetItem = gcItem.DataSource as DataTable;
                if (dtAssetItem != null && dtAssetItem.Rows.Count != 0)
                {
                    var Selected = (from d in dtAssetItem.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);
                    if (Selected.Count() > 0)
                    {
                        ProjectSelected = Selected.CopyToDataTable();
                        ReportProperty.Current.SelectedProjectCount = ProjectSelected.Rows.Count;
                        if (ProjectSelected != null && ProjectSelected.Rows.Count > 0)
                        {
                            foreach (DataRow dr in ProjectSelected.Rows)
                            {
                                assetItem += dr[this.appSchema.ASSETItem.ITEM_IDColumn.ColumnName] + ",";
                            }
                            assetItem = assetItem.TrimEnd(',');
                            ReportProperty.Current.LedgalEntityId = ReportProperty.Current.LedgalEntityId.TrimEnd(',');
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
            return assetItem;
        }


        private string SelectedLocation()
        {
            string AssetLocation = string.Empty;
            try
            {
                DataTable dtLocation = gcLocation.DataSource as DataTable;
                if (dtLocation != null && dtLocation.Rows.Count != 0)
                {
                    var Selected = (from d in dtLocation.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);
                    if (Selected.Count() > 0)
                    {
                        ProjectSelected = Selected.CopyToDataTable();
                        ReportProperty.Current.SelectedProjectCount = ProjectSelected.Rows.Count;
                        if (ProjectSelected != null && ProjectSelected.Rows.Count > 0)
                        {
                            foreach (DataRow dr in ProjectSelected.Rows)
                            {
                                AssetLocation += dr[this.appSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName] + ",";
                            }
                            AssetLocation = AssetLocation.TrimEnd(',');
                            //ReportProperty.Current.LedgalEntityId = ReportProperty.Current.LedgalEntityId.TrimEnd(',');
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
            return AssetLocation;
        }

        private string RegistrationType()
        {
            string registrationtype = string.Empty;
            try
            {
                DataTable dtRegistrationType = gcRegistrationType.DataSource as DataTable;
                if (dtRegistrationType != null && dtRegistrationType.Rows.Count != 0)
                {
                    var Selected = (from d in dtRegistrationType.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);
                    if (Selected.Count() > 0)
                    {
                        ProjectSelected = Selected.CopyToDataTable();
                        ReportProperty.Current.SelectedProjectCount = ProjectSelected.Rows.Count;
                        if (ProjectSelected != null && ProjectSelected.Rows.Count > 0)
                        {
                            foreach (DataRow dr in ProjectSelected.Rows)
                            {
                                registrationtype += dr[this.appSchema.DonorRegistrationType.REGISTRATION_TYPE_IDColumn.ColumnName] + ",";
                            }
                            registrationtype = registrationtype.TrimEnd(',');
                            //ReportProperty.Current.LedgalEntityId = ReportProperty.Current.LedgalEntityId.TrimEnd(',');
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
            return registrationtype;
        }

        private string Country()
        {
            string Country = string.Empty;
            try
            {
                DataTable dtCountry = gcCountry.DataSource as DataTable;
                if (dtCountry != null && dtCountry.Rows.Count != 0)
                {
                    var Selected = (from d in dtCountry.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);
                    if (Selected.Count() > 0)
                    {
                        ProjectSelected = Selected.CopyToDataTable();
                        ReportProperty.Current.SelectedProjectCount = ProjectSelected.Rows.Count;
                        if (ProjectSelected != null && ProjectSelected.Rows.Count > 0)
                        {
                            foreach (DataRow dr in ProjectSelected.Rows)
                            {
                                Country += dr[this.appSchema.Country.COUNTRY_IDColumn.ColumnName] + ",";
                            }
                            Country = Country.TrimEnd(',');
                            //ReportProperty.Current.LedgalEntityId = ReportProperty.Current.LedgalEntityId.TrimEnd(',');
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
            return Country;
        }

        private string State()
        {
            string State = string.Empty;
            try
            {
                DataTable dtState = gcState.DataSource as DataTable;
                if (dtState != null && dtState.Rows.Count != 0)
                {
                    var Selected = (from d in dtState.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);
                    if (Selected.Count() > 0)
                    {
                        ProjectSelected = Selected.CopyToDataTable();
                        ReportProperty.Current.SelectedProjectCount = ProjectSelected.Rows.Count;
                        if (ProjectSelected != null && ProjectSelected.Rows.Count > 0)
                        {
                            foreach (DataRow dr in ProjectSelected.Rows)
                            {
                                State += dr[this.appSchema.State.STATE_IDColumn.ColumnName] + ",";
                            }
                            State = State.TrimEnd(',');
                            //ReportProperty.Current.LedgalEntityId = ReportProperty.Current.LedgalEntityId.TrimEnd(',');
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
            return State;
        }

        private string Language()
        {
            string Language = string.Empty;
            try
            {
                DataTable dtState = gcLanguage.DataSource as DataTable;
                if (dtState != null && dtState.Rows.Count != 0)
                {
                    var Selected = (from d in dtState.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);
                    if (Selected.Count() > 0)
                    {
                        ProjectSelected = Selected.CopyToDataTable();
                        ReportProperty.Current.SelectedProjectCount = ProjectSelected.Rows.Count;
                        if (ProjectSelected != null && ProjectSelected.Rows.Count > 0)
                        {
                            foreach (DataRow dr in ProjectSelected.Rows)
                            {
                                Language += dr[this.appSchema.DonorProspects.LANGUAGEColumn.ColumnName] + ",";
                            }
                            Language = Language.TrimEnd(',');
                            //ReportProperty.Current.LedgalEntityId = ReportProperty.Current.LedgalEntityId.TrimEnd(',');
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
            return Language;
        }

        private string StateDonaud()
        {
            string StateDonaud = string.Empty;
            try
            {
                DataTable dtStateDonaud = gcStateDonaud.DataSource as DataTable;
                if (dtStateDonaud != null && dtStateDonaud.Rows.Count != 0)
                {
                    var Selected = (from d in dtStateDonaud.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);
                    if (Selected.Count() > 0)
                    {
                        ProjectSelected = Selected.CopyToDataTable();
                        ReportProperty.Current.SelectedProjectCount = ProjectSelected.Rows.Count;
                        if (ProjectSelected != null && ProjectSelected.Rows.Count > 0)
                        {
                            foreach (DataRow dr in ProjectSelected.Rows)
                            {
                                StateDonaud += dr[this.appSchema.DonorAuditor.STATE_IDColumn.ColumnName] + ",";
                            }
                            StateDonaud = StateDonaud.TrimEnd(',');
                            //ReportProperty.Current.LedgalEntityId = ReportProperty.Current.LedgalEntityId.TrimEnd(',');
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
            return StateDonaud;
        }

        private string Donaud()
        {
            string Donaud = string.Empty;
            try
            {
                DataTable dtDonaud = gcDonaud.DataSource as DataTable;
                if (dtDonaud != null && dtDonaud.Rows.Count != 0)
                {
                    var Selected = (from d in dtDonaud.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);
                    if (Selected.Count() > 0)
                    {
                        ProjectSelected = Selected.CopyToDataTable();
                        ReportProperty.Current.SelectedProjectCount = ProjectSelected.Rows.Count;
                        if (ProjectSelected != null && ProjectSelected.Rows.Count > 0)
                        {
                            foreach (DataRow dr in ProjectSelected.Rows)
                            {
                                Donaud += dr[this.appSchema.DonorAuditor.DONAUD_IDColumn.ColumnName] + ",";
                            }
                            Donaud = Donaud.TrimEnd(',');
                            //ReportProperty.Current.LedgalEntityId = ReportProperty.Current.LedgalEntityId.TrimEnd(',');
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
            return Donaud;
        }

        private string SelectedTask()
        {
            string SelectedTask = string.Empty;
            try
            {
                DataTable dtTaskList = gcTasks.DataSource as DataTable;
                if (dtTaskList != null && dtTaskList.Rows.Count != 0)
                {
                    var Selected = (from d in dtTaskList.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);
                    if (Selected.Count() > 0)
                    {
                        TaskSelected = Selected.CopyToDataTable();
                        ReportProperty.Current.SelectedTaskCount = TaskSelected.Rows.Count;
                        if (TaskSelected != null && TaskSelected.Rows.Count > 0)
                        {
                            foreach (DataRow dr in TaskSelected.Rows)
                            {
                                SelectedTask += dr[this.appSchema.DonorTags.TAG_IDColumn.ColumnName] + ",";
                            }
                            SelectedTask = SelectedTask.TrimEnd(',');
                            //ReportProperty.Current.LedgalEntityId = ReportProperty.Current.LedgalEntityId.TrimEnd(',');
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
            return SelectedTask;
        }
        //private string TaskName()
        //{
        //    string TaskName = string.Empty;
        //    try
        //    {
        //        DataTable dtTaskName = glkpFeestDatTask.Properties.DataSource as DataTable;
        //        if (dtTaskName != null && dtTaskName.Rows.Count != 0)
        //        {
        //            //var Selected = (from d in dtTaskName.AsEnumerable()
        //            //                where ((d.Field<Int32?>(SELECT) == 1))
        //            //                select d);
        //            if (Selected.Count() > 0)
        //            {
        //                ProjectSelected = Selected.CopyToDataTable();
        //                ReportProperty.Current.SelectedProjectCount = ProjectSelected.Rows.Count;
        //                if (ProjectSelected != null && ProjectSelected.Rows.Count > 0)
        //                {
        //                    foreach (DataRow dr in ProjectSelected.Rows)
        //                    {
        //                        TaskName += dr[this.appSchema.DonorTags.TAG_IDColumn.ColumnName] + ",";
        //                    }
        //                    TaskName = TaskName.TrimEnd(',');
        //                    //ReportProperty.Current.LedgalEntityId = ReportProperty.Current.LedgalEntityId.TrimEnd(',');
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
        //    }
        //    finally { }
        //    return TaskName;
        //}
        /// <summary>
        /// Re assign the selected ledger 
        /// </summary>
        private void AssignSelectedLedgerDetails()
        {
            DataTable dtCheckedLedgers = new DataTable();
            DataTable dtLedgers = new DataTable();
            try
            {
                if (!String.IsNullOrEmpty(ReportProperty.Current.Ledger) && ReportProperty.Current.Ledger != "0")
                {
                    SetLedgerDetailSource();
                    if (LedgerSelected != null)
                    {
                        dtCheckedLedgers = LedgerSelected.Clone();
                        dtLedgers = LedgerSelected.Copy();
                        string[] ledgerGroup = ReportProperty.Current.Ledger.Split(',');
                        for (int i = 0; i < LedgerSelected.Rows.Count; i++)
                        {
                            for (int j = 0; j < ledgerGroup.Length; j++)
                            {
                                if (LedgerSelected.Rows[i][0].ToString() == ledgerGroup[j].ToString())
                                {
                                    DataRow drLedgerAccount = dtCheckedLedgers.NewRow();
                                    drLedgerAccount["LEDGER_ID"] = ReportProperty.Current.NumberSet.ToInteger(LedgerSelected.Rows[i][0].ToString());
                                    drLedgerAccount["LEDGER"] = LedgerSelected.Rows[i][1].ToString();
                                    drLedgerAccount["GROUP_ID"] = ReportProperty.Current.NumberSet.ToInteger(LedgerSelected.Rows[i][2].ToString());
                                    drLedgerAccount["SELECT"] = 1;
                                    dtCheckedLedgers.Rows.Add(drLedgerAccount);
                                }
                            }
                        }

                        if (ReportProperty.Current.UnSelectedLedgerId != "0")
                        {
                            string[] UnSelectedLedgerGroup = ReportProperty.Current.UnSelectedLedgerId.Split(',');
                            for (int i = 0; i < LedgerSelected.Rows.Count; i++)
                            {
                                for (int j = 0; j < UnSelectedLedgerGroup.Length; j++)
                                {
                                    if (LedgerSelected.Rows[i][0].ToString() == UnSelectedLedgerGroup[j].ToString())
                                    {
                                        DataRow drLedgerAccount = dtCheckedLedgers.NewRow();
                                        drLedgerAccount["LEDGER_ID"] = ReportProperty.Current.NumberSet.ToInteger(LedgerSelected.Rows[i][0].ToString());
                                        drLedgerAccount["LEDGER"] = LedgerSelected.Rows[i][1].ToString();
                                        drLedgerAccount["GROUP_ID"] = ReportProperty.Current.NumberSet.ToInteger(LedgerSelected.Rows[i][2].ToString());
                                        dtCheckedLedgers.Rows.Add(drLedgerAccount);
                                    }
                                }
                            }
                        }
                        gcLedgerDetail.DataSource = dtCheckedLedgers;
                        gcLedgerDetail.RefreshDataSource();
                    }
                }
                AssignSelectedLedgerGroup();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        /// <summary>
        /// Re assign the selected cost center details.
        /// </summary>
        private void AssignSelectedCostCentreDetails()
        {

            try
            {
                if (!String.IsNullOrEmpty(ReportProperty.Current.CostCentre) && ReportProperty.Current.CostCentre != "0")
                {
                    string pids = string.IsNullOrEmpty(ReportProperty.Current.Project) ? "" : ReportProperty.Current.Project;
                    string lids = string.Empty;

                    string[] reportCriteria = ReportProperty.Current.ReportCriteria.Split('ÿ');
                    if (Array.IndexOf(reportCriteria, "LG") >= 0)
                    {
                        lids = string.IsNullOrEmpty(ReportProperty.Current.Ledger) ? "" : ReportProperty.Current.Ledger;
                        lids = (lids == "0" ? "" : lids);
                    }

                    SetCostCentreSource(pids, lids);
                    SelectCC();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void SelectCC()
        {
            DataTable dtCheckedCostCentre = new DataTable();
            DataTable dtCostCentre = new DataTable();

            try
            {
                if (CostCentreSelected != null && !string.IsNullOrEmpty(ReportProperty.Current.CostCentre) && ReportProperty.Current.CostCentre != "0")
                {
                    dtCheckedCostCentre = CostCentreSelected.Clone();
                    dtCostCentre = CostCentreSelected.Copy();
                    string[] costCentre = ReportProperty.Current.CostCentre.Split(',');
                    for (int i = 0; i < CostCentreSelected.Rows.Count; i++)
                    {
                        for (int j = 0; j < costCentre.Length; j++)
                        {
                            if (CostCentreSelected.Rows[i]["COST_CENTRE_ID"].ToString() == costCentre[j].ToString())
                            {
                                DataRow drLedgerAccount = dtCheckedCostCentre.NewRow();
                                drLedgerAccount["PROJECT_ID"] = ReportProperty.Current.NumberSet.ToInteger(CostCentreSelected.Rows[i]["PROJECT_ID"].ToString()); //PROJECT_ID
                                drLedgerAccount["COST_CENTRE_ID"] = ReportProperty.Current.NumberSet.ToInteger(CostCentreSelected.Rows[i]["COST_CENTRE_ID"].ToString()); //1
                                drLedgerAccount["COST_CENTRE_NAME"] = CostCentreSelected.Rows[i]["COST_CENTRE_NAME"].ToString(); //2

                                if (drLedgerAccount.Table.Columns.Contains(this.appSchema.CostCentreCategory.COST_CENTRECATEGORY_IDColumn.ColumnName) &&
                                    CostCentreSelected.Columns.Contains(this.appSchema.CostCentreCategory.COST_CENTRECATEGORY_IDColumn.ColumnName))
                                {
                                    drLedgerAccount[this.appSchema.CostCentreCategory.COST_CENTRECATEGORY_IDColumn.ColumnName] =
                                        ReportProperty.Current.NumberSet.ToInteger(CostCentreSelected.Rows[i][this.appSchema.CostCentreCategory.COST_CENTRECATEGORY_IDColumn.ColumnName].ToString());
                                    drLedgerAccount["SELECT"] = 1;
                                }

                                dtCheckedCostCentre.Rows.Add(drLedgerAccount);
                            }
                        }
                    }
                    DataTable dtCostCentreAccount = RemoveChecked(dtCostCentre, dtCheckedCostCentre, "COST_CENTRE_NAME", "COST_CENTRE_NAME");
                    if (dtCostCentreAccount != null && dtCostCentreAccount.Rows.Count != 0)
                    {
                        dtCheckedCostCentre.Merge(dtCostCentreAccount);
                    }
                    else
                    {
                        chkCostCentre.Checked = true;
                    }

                    DataView dvCheckedCostCentre = dtCheckedCostCentre.DefaultView;
                    //dvCheckedCostCentre.RowFilter = "PROJECT_ID IN (" + ReportProperty.Current.Project + ")";
                    DataView dv = dvCheckedCostCentre;
                    dv.Sort = SELECT + " DESC";
                    gcCostCentre.DataSource = dv.ToTable();
                    //gcCostCentre.DataSource = dv.ToTable();
                    gcCostCentre.RefreshDataSource();

                    if (gcCostCentre.DataSource != null)
                    {
                        object selcount = dv.ToTable().Compute("COUNT(" + SELECT + ")", SELECT + " = 1");
                        if (selcount != null)
                        {
                            if (ReportProperty.Current.NumberSet.ToInteger(selcount.ToString()) != gvCostCentre.RowCount)
                            {
                                chkCostCentre.Checked = false;
                            }
                        }


                        //gvCostCentre.RowCount
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        /// <summary>
        /// Set Report Criteria for Date From and Date To
        /// </summary>
        private void AssignReportCriteria()
        {
            DateTime Today = DateTime.Now;
            DateTime dtyearFrom = ReportProperty.Current.DateSet.ToDate(settinguserProperty.YearFrom, false);
            DateTime dtyearTo = ReportProperty.Current.DateSet.ToDate(settinguserProperty.YearTo, false);
            DateTime dtBookBegining = ReportProperty.Current.DateSet.ToDate(settinguserProperty.BookBeginFrom, false);
            try
            {
                string reportCriteria = ReportProperty.Current.ReportCriteria;
                string[] aReportCriteria = reportCriteria.Split('ÿ');

                foreach (string Criteria in aReportCriteria)
                {
                    switch (Criteria)
                    {
                        case "DF":
                        case "DT":
                            {
                                if (ReportProperty.Current.ReportId == "RPT-027" || ReportProperty.Current.ReportId == "RPT-028" || ReportProperty.Current.ReportId == "RPT-030" ||
                                     ReportProperty.Current.ReportId == "RPT-046" || ReportProperty.Current.ReportId == "RPT-048" || ReportProperty.Current.ReportId == "RPT-068" ||
                                     ReportProperty.Current.ReportId == "RPT-148" || ReportProperty.Current.ReportId == "RPT-075" || ReportProperty.Current.ReportId == "RPT-081" ||
                                     ReportProperty.Current.ReportId == "RPT-084" || ReportProperty.Current.ReportId == "RPT-085" || ReportProperty.Current.ReportId == "RPT-086" ||
                                     ReportProperty.Current.ReportId == "RPT-089" || ReportProperty.Current.ReportId == "RPT-091" || ReportProperty.Current.ReportId == "RPT-092" ||
                                     ReportProperty.Current.ReportId == "RPT-095" || ReportProperty.Current.ReportId == "RPT-096" || ReportProperty.Current.ReportId == "RPT-191" ||
                                     ReportProperty.Current.ReportId == "RPT-199" || ReportProperty.Current.ReportId == "RPT-200" || ReportProperty.Current.ReportId == "RPT-201" ||
                                     ReportProperty.Current.ReportId == "RPT-205" || ReportProperty.Current.ReportId == "RPT-206" || ReportProperty.Current.ReportId == "RPT-218")
                                {
                                    if (!string.IsNullOrEmpty(ReportProperty.Current.DateFrom))
                                    {
                                        DateFrom.DateTime = ReportProperty.Current.DateSet.ToDate(ReportProperty.Current.DateFrom, false);
                                    }
                                    else
                                    {
                                        DateFrom.DateTime = ReportProperty.Current.DateSet.ToDate(settinguserProperty.YearFrom, false);
                                    }

                                    if (!string.IsNullOrEmpty(ReportProperty.Current.DateTo))
                                    {
                                        DateTo.DateTime = ReportProperty.Current.DateSet.ToDate(ReportProperty.Current.DateTo, false);
                                    }
                                    else
                                    {
                                        DateTo.DateTime = ReportProperty.Current.DateSet.ToDate(settinguserProperty.YearTo, false);
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(ReportProperty.Current.DateFrom) && !String.IsNullOrEmpty(ReportProperty.Current.DateTo))
                                    {
                                        if (ReportProperty.Current.DateSet.ToDate(settinguserProperty.YearFrom, false) > ReportProperty.Current.DateSet.ToDate(ReportProperty.Current.DateFrom, false))
                                        {
                                            if (!string.IsNullOrEmpty(ReportProperty.Current.DateFrom))
                                            {
                                                DateFrom.DateTime = ReportProperty.Current.DateSet.ToDate(ReportProperty.Current.DateFrom, false);
                                            }
                                            else
                                            {
                                                DateFrom.DateTime = ReportProperty.Current.DateSet.ToDate(settinguserProperty.YearFrom, false);
                                            }

                                            //DateTo.DateTime = ReportProperty.Current.DateSet.ToDate(settingProperty.YearFrom, false);
                                            DateTo.DateTime = ReportProperty.Current.DateSet.ToDate(ReportProperty.Current.DateTo, false);
                                        }
                                        else
                                        {
                                            DateFrom.DateTime = ReportProperty.Current.DateSet.ToDate(ReportProperty.Current.DateFrom, false);
                                            DateTo.DateTime = ReportProperty.Current.DateSet.ToDate(ReportProperty.Current.DateTo, false);//.AddDays(-1)
                                        }
                                    }
                                    else
                                    {
                                        //DateFrom.DateTime = ReportProperty.Current.DateSet.ToDate(settingProperty.BookBeginFrom, false);
                                        DateFrom.DateTime = (dtyearFrom > dtBookBegining) ? dtyearFrom : dtBookBegining;
                                        DateTo.DateTime = DateFrom.DateTime.AddMonths(1).AddDays(-1);
                                        DateTo.Properties.MaxValue = ReportProperty.Current.DateSet.ToDate(FetchMaxYearTo(), false);
                                    }
                                }
                                break;
                            }
                        case "DA":
                            {
                                // commanded to set Current Year To always but we need to open based on the date as on - chinna (08.09.2018)
                                // if ((ReportProperty.Current.DateAsOn == null))
                                // {
                                //      DateFrom.DateTime = ReportProperty.Current.DateSet.ToDate(settingProperty.YearTo, false);
                                //  }
                                //  else
                                // {
                                if (!string.IsNullOrEmpty(ReportProperty.Current.DateAsOn))
                                {
                                    DateFrom.DateTime = ReportProperty.Current.DateSet.ToDate(ReportProperty.Current.DateAsOn, false);
                                }
                                else
                                {
                                    //DateFrom.DateTime = ReportProperty.Current.DateSet.ToDate(settingProperty.BookBeginFrom, false);
                                    // DateFrom.DateTime = (dtyearFrom > dtBookBegining) ? dtyearFrom : dtBookBegining;
                                    DateFrom.DateTime = (dtyearFrom > dtBookBegining) ? dtyearTo : dtBookBegining;
                                }
                                //  }
                                break;
                            }
                    }
                }

                if (!string.IsNullOrEmpty(ReportProperty.Current.ReportDate))
                {
                    ReportDate.DateTime = ReportProperty.Current.DateSet.ToDate(ReportProperty.Current.ReportDate, false);
                }
                else
                {
                    ReportDate.DateTime = ReportProperty.Current.DateSet.ToDate(DateTime.Today.ToString(), false);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        /// <summary>
        /// Save  REport Set up Values.
        /// </summary>
        private void SaveReportSetup()
        {
            try
            {
                ReportProperty.Current.TitleAlignment = cboTitleAlignment.SelectedIndex;
                ReportProperty.Current.ShowLogo = chkShowReportLogo.Checked ? 1 : 0;
                ReportProperty.Current.ShowPageNumber = chkPageNumber.Checked ? 1 : 0;
                ReportProperty.Current.ShowLedgerCode = chkLedgerCode.Checked ? 1 : 0;
                ReportProperty.Current.ShowGroupCode = chkGroupCode.Checked ? 1 : 0;
                ReportProperty.Current.SortByLedger = cboSortByLedger.SelectedIndex;
                ReportProperty.Current.SortByGroup = cboSoryByGroup.SelectedIndex;
                ReportProperty.Current.ShowHorizontalLine = chkHorizontalLine.Checked ? 1 : 0;
                ReportProperty.Current.SupressZeroValues = chkShowZeroValues.Checked ? 1 : 0;
                ReportProperty.Current.ShowIndividualProjects = chkShowIndividualProjects.Checked ? 1 : 0;
                ReportProperty.Current.ShowVerticalLine = chkVerticalLine.Checked ? 1 : 0;
                ReportProperty.Current.ShowTitles = chkDisplayTitles.Checked ? 1 : 0;
                ReportProperty.Current.HeaderInstituteSocietyName = (rgbReportTitle.SelectedIndex == 0) ? 0 : 1;
                ReportProperty.Current.HeaderInstituteSocietyAddress = (rgbAddress.SelectedIndex == 0) ? 0 : 1;
                ReportProperty.Current.ShowProjectsinFooter = chkShowProjectinFooter.Checked ? 1 : 0;
                ReportProperty.Current.ReportBorderStyle = (cboBorderStyle.SelectedIndex); //cboBorderStyle.SelectedIndex
                ReportProperty.Current.ShowReportDate = (ReportDate.Properties.Buttons[1].Image == imgCollection.Images[1]) ? 1 : 0;
                ReportProperty.Current.ReportDate = (ReportProperty.Current.ShowReportDate == 0) ? string.Empty : ReportDate.Text;
                ReportProperty.Current.ColumnCaptionFontStyle = cboColumnHeaderFontStyle.SelectedIndex;
                ReportProperty.Current.HeaderWithInstituteName = (rgbReportTitle.SelectedIndex == 0 ? 0 : (chkSocietyWithInstutionName.Checked ? 1 : 0));

                if (this.settinguserProperty.IS_SAPPIC)
                {
                    //ReportProperty.Current.ReportCodeType = cboReportType.SelectedIndex;
                    //  ReportProperty.Current.ReportCodeType = cboReportType.SelectedIndex == 0 ? (int)ReportCodeType.Province : (int)ReportCodeType.Generalate;

                    ReportProperty.Current.ReportCodeType = cboReportType.SelectedIndex == 0 ? (int)ReportCodeType.Province : (int)ReportCodeType.Generalate;
                }
                else
                {
                    ReportProperty.Current.ReportCodeType = (int)ReportCodeType.Standard;
                }

                ReportProperty.Current.ShowTableofContent = (chkShowTOC.Checked ? 1 : 0);
                ReportProperty.Current.IncludeAllBudgetLedgers = (chkIncludeAllBudgetLedgers.Checked ? 1 : 0);

                //Assign sign details
                /*
                ReportProperty.Current.RoleName1 = txtRoleName1.Text.Trim();
                ReportProperty.Current.Role1 = txtRole1.Text.Trim();
                ReportProperty.Current.RoleName2 = txtRoleName2.Text.Trim();
                ReportProperty.Current.Role2 = txtRole2.Text.Trim();
                ReportProperty.Current.RoleName3 = txtRoleName3.Text.Trim();
                ReportProperty.Current.Role3 = txtRole3.Text.Trim();
                //ReportProperty.Current.RoleName4 = txtRoleName3.Text.Trim();
                //ReportProperty.Current.Role4 = txtRole3.Text.Trim();
                //ReportProperty.Current.RoleName5 = txtRoleName3.Text.Trim();
                //ReportProperty.Current.Role5 = txtRole3.Text.Trim();

                if (lcSign1.Visibility == LayoutVisibility.Always)
                {
                    //For Sign 1
                    ReportProperty.Current.Sign1Image = null;
                    if (picSign1.Image != null)
                    {
                        picSign1.BorderStyle = BorderStyles.NoBorder;
                        byte[] signImage = ImageProcessing.ImageToByteArray(picSign1.Image as Bitmap);
                        ReportProperty.Current.Sign1Image = signImage;
                    }

                    //For Sign 2
                    ReportProperty.Current.Sign2Image = null;
                    if (picSign2.Image != null)
                    {
                        picSign2.BorderStyle = BorderStyles.NoBorder;
                        byte[] signImage = ImageProcessing.ImageToByteArray(picSign2.Image as Bitmap);
                        ReportProperty.Current.Sign2Image = signImage;
                    }

                    //For Sign 3
                    ReportProperty.Current.Sign3Image = null;
                    if (picSign3.Image != null)
                    {
                        picSign3.BorderStyle = BorderStyles.NoBorder;
                        byte[] signImage = ImageProcessing.ImageToByteArray(picSign3.Image as Bitmap);
                        ReportProperty.Current.Sign3Image = signImage;
                    }
                }
                */

                //Assign Budget New Project
                if (settinguserProperty.CreateBudgetDevNewProjects == 0 &&
                    (ReportProperty.Current.ReportId == "RPT-179" || ReportProperty.Current.ReportId == "RPT-189"))
                {
                    DataTable dtBudgetNewProject = gcBudgetNewProject.DataSource as DataTable;

                    dtBudgetNewProject.DefaultView.RowFilter = reportSchema.ReportBudgetNewProject.NEW_PROJECTColumn.ColumnName + "<>'' " +
                                                        " AND (" + reportSchema.ReportBudgetNewProject.PROPOSED_INCOME_AMOUNTColumn.ColumnName + ">0 OR " +
                                                        reportSchema.ReportBudgetNewProject.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName + ">0 OR " +
                                                        reportSchema.ReportBudgetNewProject.GN_HELP_PROPOSED_AMOUNTColumn.ColumnName + ">0 OR " +
                                                        reportSchema.ReportBudgetNewProject.HO_HELP_PROPOSED_AMOUNTColumn.ColumnName + ">0)";

                    ReportProperty.Current.BudgetNewProjects = dtBudgetNewProject.DefaultView.ToTable();
                }

                //ReportProperty.Current.IncludeSent = chkSent.Checked ? 1 : 0;
                //ReportProperty.Current.IncludeNotSent = chkNotSent.Checked ? 1 : 0;
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        /// <summary>
        /// Check Roles are duplicated with in current fy year
        /// </summary>
        /// <returns></returns>
        private bool IsValidReportCriteria()
        {
            bool Rtn = true;

            /*
            //1. Check Valid roles (must be unique)
            if (!string.IsNullOrEmpty(txtRole1.Text.Trim()))
            {
                Rtn = !(txtRole1.Text == txtRole2.Text) && !(txtRole1.Text == txtRole3.Text);
            }

            if (!string.IsNullOrEmpty(txtRole2.Text.Trim()) && Rtn)
            {
                Rtn = !(txtRole2.Text == txtRole1.Text) && !(txtRole2.Text == txtRole3.Text);
            }

            if (!string.IsNullOrEmpty(txtRole3.Text.Trim()) && Rtn)
            {
                Rtn = !(txtRole3.Text == txtRole1.Text) && !(txtRole3.Text == txtRole2.Text);
            }

            if (!Rtn)
            {
                MessageRender.ShowMessage("Role should not be repeated for current year");
                xtbLocation.SelectedTabPageIndex = xtbSign.TabIndex;
                txtRole1.Focus();
            }
            */

            if (Rtn && settinguserProperty.CreateBudgetDevNewProjects == 0 && (ReportProperty.Current.ReportId == "RPT-179" || ReportProperty.Current.ReportId == "RPT-189"))
            {
                Rtn = ValidateBudgetNewProjects();
                if (!Rtn)
                {
                    FocusBudgetNewProjectGrid();
                }
            }

            if (Rtn && ReportProperty.Current.ReportId == "RPT-209")
            {
                MessageRender.ShowMessage(MessageRender.GetMessage(MessageCatalog.ReportMessage.REPORTS_EXCEL));
                //MessageRender.ShowMessage("Report will be splitted basd on number of Ledgers, Please Export to Excel.");
            }

            return Rtn;
        }

        /// <summary>
        /// Remove un selected records from selected records.
        /// </summary>
        /// <param name="dtTable"></param>
        /// <param name="dtProject"></param>
        /// <param name="colName"></param>
        /// <param name="colName2"></param>
        /// <returns></returns>
        private DataTable RemoveChecked(DataTable dtTable, DataTable dtProject, string colName, string colName2)
        {
            DataTable dtUnselectedProject = new DataTable();
            try
            {
                if (dtTable.Rows.Count != dtProject.Rows.Count)
                {
                    var UncheckVouchers = dtTable.AsEnumerable()
                                  .Where(row => !dtProject.AsEnumerable()
                                                        .Select(r => r.Field<string>(colName))
                                                        .Any(x => x == row.Field<string>(colName2))
                                 ).CopyToDataTable();
                    dtUnselectedProject = UncheckVouchers.DefaultView.Table;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
            return dtUnselectedProject;
        }

        /// <summary>
        /// Enable report setup criteria based on the reports
        /// </summary>
        public void EnableReportSetupProperties()
        {
            //On 12/03/2019, For Temp purpose, it should be handled based on report setting 
            chkVerticalLine.Checked = chkPageNumber.Checked = true;
            lcShowTOC.Visibility = LayoutVisibility.Never;

            lcIncludeAllBudgetLedgers.Visibility = LayoutVisibility.Never;
            chkIncludeAllBudgetLedgers.Checked = false;

            switch (ReportProperty.Current.ReportId)
            {
                // case "RPT-007": Chque Cleared to Show Ledger Code - 24.09.2018 (Chinna)
                // case "RPT-008": Chque UnCleared to Show Ledger Code - 24.09.2018 (Chinna)
                //case "RPT-009": Chque Realized to Show Ledger Code - 24.09.2018 (Chinna)
                //case "RPT-010": Chque UnRealized to Show Ledger Code - 24.09.2018 (Chinna)
                // case "RPT-013": BRS to show Ledger Code - 24.09.2018 (Chinna)
                case "RPT-047":
                case "RPT-075": //Cost Centre Summary
                    {
                        cboSortByLedger.Enabled = chkGroupCode.Enabled = chkLedgerCode.Enabled = cboSoryByGroup.Enabled = false;
                        break;
                    }
                case "RPT-014":
                case "RPT-015":
                case "RPT-018": // Cash Flow
                case "RPT-019": // Bank Flow
                    {
                        chkLedgerCode.Enabled = cboSortByLedger.Enabled = chkGroupCode.Enabled = cboSoryByGroup.Enabled = false;
                        break;
                    }

                case "RPT-007": // cheque Cleared
                case "RPT-008": // cheque Uncleared
                case "RPT-009": // cheque realized
                case "RPT-010": // cheque unrealized
                case "RPT-013": // BRS
                case "RPT-016": // CashJournal
                case "RPT-017": // Bank Journal
                case "RPT-038":  // Cost Centre Cash Journal
                case "RPT-039": // Cost Centre Bank Journal
                case "RPT-050": //Cost Centre Ledger
                    {
                        cboSortByLedger.Enabled = chkGroupCode.Enabled = cboSoryByGroup.Enabled = false;
                        break;
                    }
                case "RPT-027": // Receipts and Payments
                case "RPT-028": //Income and Expenditure
                case "RPT-041": // Cost Centre Receipts and Payments
                case "RPT-049":// Cost Centre Income and Expenditure
                    //case "RPT-029":
                    {
                        chkLedgerCode.Enabled = cboSortByLedger.Enabled = chkGroupCode.Enabled = cboSoryByGroup.Enabled = true;
                        chkHorizontalLine.Checked = chkVerticalLine.Checked = true;
                        chkHorizontalLine.Enabled = chkVerticalLine.Enabled = false;
                        break;
                    }
                case "RPT-030":
                    {
                        chkLedgerCode.Enabled = cboSortByLedger.Enabled = chkGroupCode.Enabled = cboSoryByGroup.Enabled = true;
                        chkHorizontalLine.Enabled = true;
                        break;
                    }
                case "RPT-040":
                case "RPT-011"://Cash/Bank Book - Multi
                case "RPT-062"://Cash/Bank Book - Single
                    {
                        cboSortByLedger.Enabled = chkGroupCode.Enabled = cboSoryByGroup.Enabled = false;
                        chkHorizontalLine.Checked = chkVerticalLine.Checked = true;
                        chkVerticalLine.Enabled = chkHorizontalLine.Enabled = false;
                        break;
                    }
                case "RPT-052":
                    {
                        chkLedgerCode.Checked = chkGroupCode.Checked = true;
                        chkLedgerCode.Enabled = chkGroupCode.Enabled = false;
                        break;
                    }
                case "RPT-012"://Ledger
                    {
                        chkLedgerCode.Checked = (ReportProperty.Current.ShowLedgerCode == 1);
                        cboSortByLedger.Enabled = true;
                        lcShowTOC.Visibility = LayoutVisibility.Always;
                        chkShowTOC.Checked = (ReportProperty.Current.ShowTableofContent == 1);
                        break;
                    }
                case "RPT-167"://Cheque Issued Register
                    {
                        chkLedgerCode.Checked = chkGroupCode.Checked = true;
                        cboSortByLedger.Enabled = chkLedgerCode.Enabled = chkGroupCode.Enabled = cboSoryByGroup.Enabled = false;
                        break;
                    }
                case "RPT-031"://Balance Sheet
                    {
                        chkLedgerCode.Checked = chkGroupCode.Checked = true;
                        chkLedgerCode.Enabled = chkGroupCode.Enabled = false;
                        cboSortByLedger.Enabled = cboSoryByGroup.Enabled = false;

                        break;
                    }
                case "RPT-020": // FC Purpose
                case "RPT-021": // FC Purpose Donor Institutional
                case "RPT-022": // FC Purpose Donor Individual
                case "RPT-042": // FC Country. 
                case "RPT-023": // Governing Members
                case "RPT-051": // Cost Centre Consolidated Report
                case "RPT-053": //  Cost Centre Day Book
                case "RPT-060": // Cost Centre Journal Transaction
                case "RPT-044": // Cash Bank Transaction
                case "RPT-045": // Journal Transaction
                case "RPT-056": // TDS Computation Payable
                case "RPT-058": // TDS Ledger Wise
                case "RPT-059": // TDS Nature of Payments
                case "RPT-057": // TDS Outstanding Payable
                case "RPT-055": // TDS Paid
                case "RPT-054": //  TDS Party Wise  
                case "RPT-186": //CMF Budget Reports 
                case "RPT-162": //Sign details
                case "RPT-198": //CC enabled Ledger List
                case "RPT-152": //Budget view, fix ledger code
                    {
                        chkProject.Visible = chkBank.Visible = chkLedger.Visible = chkLedgerGroup.Visible = chkPayrollComponents.Visible = chkCostCentre.Visible =
                        chkPartyLedger.Visible = chkNatureofPayments.Visible = chkDeducteeType.Visible = chkPayroll.Visible = chkPayrollStaff.Visible = false;
                        chkSelectAllTask.Visible = chkState.Visible = false;

                        chkLedgerCode.Checked = true;
                        chkLedgerCode.Enabled = false;
                        lcIncludeAllBudgetLedgers.Visibility = LayoutVisibility.Always;
                        chkIncludeAllBudgetLedgers.Checked = (ReportProperty.Current.IncludeAllBudgetLedgers == 1);

                        break;
                    }
                case "RPT-046": // Budget Variance
                case "RPT-048": // Budget Details
                case "RPT-068": // Budget Year Comparision
                case "RPT-169": // Budget Project-wise
                case "RPT-179": //Budget Annual Summary
                case "RPT-184": //Budget Approved
                case "RPT-185": //Budget Realization
                case "RPT-193": //BUdget Quarterly Realization
                    {
                        chkLedgerCode.Checked = true;
                        chkLedgerCode.Enabled = false;
                        break;
                    }
                case "RPT-STD": //Standard Reports
                    {
                        chkLedgerCode.Enabled = cboSortByLedger.Enabled = chkGroupCode.Enabled = cboSoryByGroup.Enabled = chkShowProjectinFooter.Enabled = chkShowIndividualProjects.Enabled = false;
                        break;
                    }
            }

            chkShowProjectinFooter.Checked = (ReportProperty.Current.ShowProjectsinFooter == 0 ? false : true);

            //Assign report sign setting
            //On 03/03/2021, to load proper sign details based on selected projects in previous reports ------------------
            ReportProperty.Current.AssignSignDetails(ReportProperty.Current.Project);
            //------------------------------------------------------------------------------------------------------------
            FillSignDetails();

            //22/07/2020, to keep back 
            //cboBorderStyle.SelectedIndex = ReportProperty.Current.ReportBorderStyle;
            cboBorderStyle.SelectedIndex = (ReportProperty.Current.ReportBorderStyle);

            // 03.06.2022 to keep back . chinna
            cboReportType.SelectedIndex = 0;

            if (this.settinguserProperty.IS_SAPPIC)
            {
                //  ReportProperty.Current.ReportCodeType = cboReportType.SelectedIndex;
                cboReportType.SelectedIndex = ReportProperty.Current.ReportCodeType == 0 || ReportProperty.Current.ReportCodeType == 1 ? 0 : 1;
            }
            else
            {
                cboReportType.SelectedIndex = (int)ReportCodeType.Standard;
            }
            //cboReportType.SelectedIndex = ReportProperty.Current.ReportCodeType;

            //Assign Budget new Projects 
            if (settinguserProperty.CreateBudgetDevNewProjects == 0 &&
                (ReportProperty.Current.ReportId == "RPT-179" || ReportProperty.Current.ReportId == "RPT-189"))
            {
                gcBudgetNewProject.DataSource = ReportProperty.Current.BudgetNewProjects;
                ConstractEmptyDatasource();
            }
        }


        /// <summary>
        /// On 27/02/2021, To fill sign details
        /// </summary>
        private void FillSignDetails()
        {
            txtRoleName1.Text = ReportProperty.Current.RoleName1; txtRole1.Text = ReportProperty.Current.Role1;
            txtRoleName2.Text = ReportProperty.Current.RoleName2; txtRole2.Text = ReportProperty.Current.Role2;
            txtRoleName3.Text = ReportProperty.Current.RoleName3; txtRole3.Text = ReportProperty.Current.Role3;

            lcSign1.Text = "Sign 1";
            lcSign2.Text = "Sign 2";
            lcSign3.Text = "Sign 3";

            //For Sign1
            picSign1.Image = null;
            if (ReportProperty.Current.Sign1Image != null)
            {
                picSign1.Image = ImageProcessing.ByteArrayToImage(ReportProperty.Current.Sign1Image);
            }

            //For Sign2
            picSign2.Image = null;
            if (ReportProperty.Current.Sign2Image != null)
            {
                picSign2.Image = ImageProcessing.ByteArrayToImage(ReportProperty.Current.Sign2Image);
            }

            //For Sign3
            picSign3.Image = null;
            if (ReportProperty.Current.Sign3Image != null)
            {
                picSign3.Image = ImageProcessing.ByteArrayToImage(ReportProperty.Current.Sign3Image);
            }
        }

        ///// <summary>
        ///// On 27/06/2024, to get actual border style for existing user
        ///// ** add "None" option and handle existing users, 
        ///// </summary>
        ///// <returns></returns>
        //private Int32 GetRealReportBorderStyle(Int32 index)
        //{
        //    Int32 rtn = (int)ReportBorderStyle.Regular;

        //    if (index == 0)  rtn = (int)ReportBorderStyle.Regular;
        //    if (index == 1) rtn = (int)ReportBorderStyle.None;
        //    if (index == 2) rtn = (int)ReportBorderStyle.Bold;
        //    return rtn;
        //}

        ///// <summary>
        ///// On 27/06/2024, to get actual border style for existing user
        ///// ** add "None" option and handle existing users, 
        ///// </summary>
        ///// <returns></returns>
        //private Int32 AssignRealReportBorderStyle(Int32 index)
        //{
        //    Int32 rtn = (int)ReportBorderStyle.None;

        //    if (index == 0) rtn = (int)ReportBorderStyle.Regular;
        //    if (index == 1) rtn = (int)ReportBorderStyle.None;
        //    if (index == 2) rtn = (int)ReportBorderStyle.Regular;
        //    return rtn;
        //}

        /// <summary>
        /// On RPT-153, RPT154 (for Cahs/Bank Recepts and Payment with VNo, attach cash ledger along with Bank Accounts
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public void AttachCashLedgerByProject(string ProjectId, DataTable dtBankLedgers)
        {
            ResultArgs resultArgs = null;
            try
            {
                if (ReportProperty.Current.ReportId == "RPT-153" || ReportProperty.Current.ReportId == "RPT-154")
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Bank.FetchAllCashLedgerByProject))
                    {
                        if (!string.IsNullOrEmpty(ProjectId) && ProjectId != "0")
                        {
                            dataManager.Parameters.Add(this.appSchema.LedgerBalance.PROJECT_IDColumn, ProjectId);
                        }
                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        resultArgs = dataManager.FetchData(DataSource.DataTable);
                    }

                    if (resultArgs.Success && resultArgs.DataSource.Table != null && dtBankLedgers != null)
                    {
                        DataTable dtCashLedgers = resultArgs.DataSource.Table;

                        foreach (DataRow drcash in dtCashLedgers.Rows)
                        {
                            DataRow drBank = dtBankLedgers.NewRow();
                            drBank["LEDGER_ID"] = drcash["LEDGER_ID"];
                            drBank["BANK"] = drcash["LEDGER_NAME"];
                            drBank["GROUP_ID"] = drcash["GROUP_ID"];
                            drBank["PROJECT_ID"] = drcash["PROJECT_ID"];
                            drBank["BANK_ID"] = 0;
                            drBank["BANK_ACCOUNT_ID"] = 0;

                            dtBankLedgers.Rows.Add(drBank);
                        }
                        dtBankLedgers.DefaultView.Sort = "GROUP_ID DESC, BANK";
                    }
                    else
                    {
                        MessageRender.ShowMessage(resultArgs.Message);
                    }
                }
                else if (ReportProperty.Current.ReportId == "RPT-016" || ReportProperty.Current.ReportId == "RPT-038")
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Bank.FetchAllCashLedgerByProject))
                    {
                        if (!string.IsNullOrEmpty(ProjectId) && ProjectId != "0")
                        {
                            dataManager.Parameters.Add(this.appSchema.LedgerBalance.PROJECT_IDColumn, ProjectId);
                        }
                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        resultArgs = dataManager.FetchData(DataSource.DataTable);
                        resultArgs = ReportProperty.Current.EnforceSkipDefaultLedgers(resultArgs, this.appSchema.LedgerBalance.LEDGER_IDColumn.ColumnName);
                    }
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && dtBankLedgers != null)
                    {
                        DataTable dtCashLedgers = resultArgs.DataSource.Table;
                        dtBankLedgers.Clear();
                        foreach (DataRow drcash in dtCashLedgers.Rows)
                        {
                            DataRow drBank = dtBankLedgers.NewRow();
                            drBank["LEDGER_ID"] = drcash["LEDGER_ID"];
                            drBank["BANK"] = drcash["LEDGER_NAME"];
                            drBank["GROUP_ID"] = drcash["GROUP_ID"];
                            drBank["PROJECT_ID"] = drcash["PROJECT_ID"];
                            drBank["BANK_ID"] = 0;
                            drBank["BANK_ACCOUNT_ID"] = 0;

                            dtBankLedgers.Rows.Add(drBank);
                        }
                        dtBankLedgers.DefaultView.Sort = "GROUP_ID DESC, BANK";

                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        public DataTable FetchStateByCountry(string CountryID)
        {
            ResultArgs resultArgs = null;
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.State.FetchStateByCountryID))
                {
                    dataManager.Parameters.Add(this.appSchema.Country.COUNTRY_IDColumn, CountryID);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
                resultArgs.DataSource.Table.Columns.Add("SELECT", typeof(Int32));
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
            return resultArgs.DataSource.Table;
        }
        public DataTable FetchDonorByStateID(string StateID)
        {
            ResultArgs resultArgs = null;
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.DonorProspect.GetDonaudByStateID))
                {
                    dataManager.Parameters.Add(this.appSchema.Project.PROJECT_IDColumn, StateID);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
                resultArgs.DataSource.Table.Columns.Add("SELECT", typeof(Int32));
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
            return resultArgs.DataSource.Table;
        }
        public DataTable FetchCostCentreByProject(string ProjectId)
        {
            ResultArgs resultArgs = null;
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.CostCentre.SetCostCentreSource))
                {
                    dataManager.Parameters.Add(this.appSchema.LedgerBalance.PROJECT_IDColumn, ProjectId);

                    if (CCCategoryId > 0)
                    {
                        dataManager.Parameters.Add(this.appSchema.CostCentreCategory.COST_CENTRECATEGORY_IDColumn.ColumnName, CCCategoryId);
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
                resultArgs.DataSource.Table.Columns.Add("SELECT", typeof(Int32));
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
            return resultArgs.DataSource.Table;
        }

        //public DataTable FetchBankByProject(string ProjectId)
        //{
        //    ResultArgs resultArgs = null;
        //    try
        //    {
        //        using (DataManager dataManager = new DataManager(SQLCommand.Bank.FetchBankByProject))
        //        {
        //            dataManager.Parameters.Add(this.appSchema.LedgerBalance.PROJECT_IDColumn, ProjectId);
        //            //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
        //            dataManager.Parameters.Add(this.appSchema.BankAccount.DATE_CLOSEDColumn, DateFrom.Text);
        //            dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
        //            resultArgs = dataManager.FetchData(DataSource.DataTable);
        //        }
        //        resultArgs.DataSource.Table.Columns.Add("SELECT", typeof(Int32));
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
        //    }
        //    finally { }
        //    return resultArgs.DataSource.Table;
        //}

        public DataTable FetchBankByProjectId(string ProjectId)
        {
            ResultArgs resultArgs = null;
            string ReportCriteria = ReportProperty.Current.ReportCriteria;
            string[] aReportCriteria = ReportCriteria.Split('ÿ');

            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Bank.FetchBankByProject))
                {
                    dataManager.Parameters.Add(this.appSchema.LedgerBalance.PROJECT_IDColumn, ProjectId);
                    //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                    dataManager.Parameters.Add(this.appSchema.BankAccount.DATE_CLOSEDColumn, DateFrom.Text);

                    //On 29/09/2023, This property is used to skip bank ledger project based
                    if (Array.IndexOf(aReportCriteria, "DA") >= 0)
                    {
                        dataManager.Parameters.Add(this.appSchema.Ledger.APPLICABLE_FROMColumn, settinguserProperty.FirstFYDateFrom);
                        dataManager.Parameters.Add(this.appSchema.Ledger.APPLICABLE_TOColumn, DateFrom.Text);
                    }
                    else
                    {
                        dataManager.Parameters.Add(this.appSchema.Ledger.APPLICABLE_FROMColumn, DateFrom.Text);
                        dataManager.Parameters.Add(this.appSchema.Ledger.APPLICABLE_TOColumn, DateTo.Text);
                    }

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
                resultArgs.DataSource.Table.Columns.Add("SELECT", typeof(Int32));
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
            return resultArgs.DataSource.Table;
        }


        public DataTable FetchFDByProjectId(string ProjectId)
        {
            ResultArgs resultArgs = null;
            using (DataManager dataManager = new DataManager(SQLCommand.Bank.FetchFDByProject))
            {
                dataManager.Parameters.Add(this.appSchema.LedgerBalance.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.appSchema.FDRegisters.DATE_FROMColumn, ReportProperty.Current.DateFrom);
                dataManager.Parameters.Add(this.appSchema.FDRegisters.DATE_TOColumn, ReportProperty.Current.DateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            resultArgs.DataSource.Table.Columns.Add("SELECT", typeof(Int32));
            return resultArgs.DataSource.Table;
        }

        public DataTable FetchForeignProjects(DataTable dtForegin)
        {
            if (dtForegin != null && dtForegin.Rows.Count > 0)
            {
                DataView dv = new DataView(dtForegin);
                dv.RowFilter = "DIVISION='Foreign'";
                return dv.ToTable();
            }
            return dtForegin;
        }

        public DataTable FetchFDAccounts(DataTable dtFDAccounts)
        {
            if (dtFDAccounts != null && dtFDAccounts.Rows.Count > 0)
            {
                DataView dv = new DataView(dtFDAccounts);
                dv.RowFilter = "GROUP_ID=14";
                return dv.ToTable();
            }
            return dtFDAccounts;
        }

        public DataTable FetchAllFixedDeposit()
        {
            using (DataManager data = new DataManager(SQLCommand.Bank.SelectAllFD))
            {
                ResultArgs resultArgs = data.FetchData(DataSource.DataTable);
                resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                BankSelected = resultArgs.DataSource.Table;
                ReportProperty.Current.RecordCount = resultArgs.DataSource.Table.Rows.Count;
                return resultArgs.DataSource.Table;
            }
        }

        public DataTable FetchBudgetsByProjects(string ProjectId, bool ActiveTrue)
        {
            ResultArgs resultargs = null;
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchBudgetByProject))
            {
                dataManager.Parameters.Add(this.appSchema.Budget.DATE_FROMColumn, DateFrom.DateTime);
                dataManager.Parameters.Add(this.appSchema.Budget.DATE_TOColumn, DateTo.DateTime);
                dataManager.Parameters.Add(this.appSchema.Budget.PROJECT_IDColumn, ProjectId);
                if (ActiveTrue)
                    dataManager.Parameters.Add(this.appSchema.Budget.IS_ACTIVEColumn, 1);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultargs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultargs.DataSource.Table;
        }

        //private DataTable FetchProjectsByDateRange(DataTable dtProjectlist)
        //{
        //    DataView dvclosedProjects = dtProjectlist.DefaultView;
        //    dvclosedProjects.RowFilter = "DATE_CLOSED >='" + DateFrom.DateTime.ToString("yyyy-MM-dd") + "' OR DATE_CLOSED IS NULL ";
        //    DataTable dtclosedprojects = dvclosedProjects.ToTable();
        //    dvclosedProjects.RowFilter = "";
        //    return dtclosedprojects;
        //}


        /// <summary>
        /// Fetch Max Accounting Year To
        /// </summary>
        private string FetchMaxYearTo()
        {
            string MaxAccTo = string.Empty;
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.FetchmaxDate))
                {
                    ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                    {
                        if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                        {
                            MaxAccTo = resultArgs.DataSource.Table.Rows[0][appSchema.AccountingPeriod.YEAR_TOColumn.ColumnName].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
            return MaxAccTo;
        }

        private void FetchDeductorType()
        {
            ResultArgs resultArgs = null;
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.DeducteeType.FetchActiveDeductTypes))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(int));
                        gcDeducteeType.DataSource = resultArgs.DataSource.Table;
                        gcDeducteeType.RefreshDataSource();
                    }
                }

                if (!string.IsNullOrEmpty(ReportProperty.Current.DeducteeTypeId) && ReportProperty.Current.DeducteeTypeId != "0")
                {
                    if (!string.IsNullOrEmpty(ReportProperty.Current.DeducteeTypeId))
                    {
                        DataTable dtDeductorDetails = gcDeducteeType.DataSource as DataTable;
                        string[] DeducteeTypes = ReportProperty.Current.DeducteeTypeId.Split(',');
                        if (dtDeductorDetails != null && dtDeductorDetails.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtDeductorDetails.Rows)
                            {
                                for (int i = 0; i < DeducteeTypes.Count(); i++)
                                {
                                    if (ReportProperty.Current.NumberSet.ToInteger(dr["DEDUCTEE_TYPE_ID"].ToString()) == ReportProperty.Current.NumberSet.ToInteger(DeducteeTypes[i]))
                                    {
                                        dr[SELECT] = 1;
                                    }
                                }
                            }
                        }
                        DataView dv = new DataView(dtDeductorDetails);
                        dv.Sort = SELECT + " DESC";
                        gcDeducteeType.DataSource = dv.ToTable();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.StackTrace, true);
            }
            finally { }
        }

        private void IncreaseFormWidth()
        {
            try
            {
                switch (ReportProperty.Current.ReportId)
                {
                    case "RPT-063":
                        {
                            this.Width = 640;
                            break;
                        }
                    case "RPT-012":
                    case "RPT-167"://Cheque Issued Register
                        {
                            this.Width = 530;
                            break;
                        }
                    case "RPT-007":
                    case "RPT-008":
                    case "RPT-009":
                    case "RPT-010":
                    case "RPT-011":
                    case "RPT-013":
                    case "RPT-014":
                    case "RPT-039":
                    case "RPT-046":

                    case "RPT-048":
                    case "RPT-016":
                        {
                            this.Width = 530;
                            break;
                        }
                    case "RPT-017":
                        {
                            this.Width = 530;
                            break;
                        }
                    case "RPT-064":
                    case "RPT-065":
                    case "RPT-066":
                    case "RPT-067":
                        {
                            this.Width = 640;
                            break;
                        }
                    case "RPT-074":
                    case "RPT-073":
                    case "RPT-075":
                    case "RPT-050":
                        {
                            this.Width = 530;
                            break;
                        }
                    case "RPT-078":
                        {
                            this.Width = 535;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.StackTrace, true);
            }
            finally { }
        }

        /// <summary>
        /// This is to avoid unneccessarily unselect the Records (chinna)
        /// </summary>
        /// <param name="e"></param>
        /// <param name="view"></param>
        private void MakeCheckBoxSelection(MouseEventArgs e, GridView view)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = view.CalcHitInfo(e.Location);
            try
            {
                if (hi.InRow)
                {
                    if (hi.Column.FieldName != "DX$CheckboxSelectorColumn")
                    {
                        int[] selectedRows = view.GetSelectedRows();
                        bool isSelected = view.IsRowSelected(hi.RowHandle);
                        BeginInvoke(new Action(() =>
                        {
                            for (int i = 0; i < selectedRows.Length; i++)
                                view.SelectRow(selectedRows[i]);
                            if (!isSelected) view.UnselectRow(hi.RowHandle);
                        }));
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region Payroll Methods
        /// <summary>
        /// Fetch Payroll List
        /// </summary>
        private void SetPayrollSource()
        {
            try
            {
                //DataTable dtPayroll = Paybase.PayrollList;
                //if (dtPayroll != null && dtPayroll.Rows.Count > 0)
                //{
                //    dtPayroll.Columns.Add(SELECT, typeof(Int32));
                //    PayrollSelected = dtPayroll;
                //    ReportProperty.Current.RecordCount = dtPayroll.Rows.Count;
                //    gcPayroll.DataSource = null;
                //    gcPayroll.DataSource = dtPayroll;
                //    gcPayroll.RefreshDataSource();
                //}
                DataTable dtPayroll = Paybase.PayrollList;
                if (dtPayroll != null && dtPayroll.Rows.Count > 0)
                {
                    ReportProperty.Current.ComboSet.BindGridLookUpCombo(glkpPayroll, dtPayroll, "PRNAME", "PAYROLLID");
                    glkpPayroll.EditValue = glkpPayroll.Properties.GetKeyValue(0);
                    ReportProperty.Current.PayrollId = glkpPayroll.EditValue.ToString();
                    ReportProperty.Current.RecordCount = dtPayroll.Rows.Count;
                    PayrollSelected = dtPayroll;
                }

                //if (!String.IsNullOrEmpty(ReportProperty.Current.PayrollId) && ReportProperty.Current.PayrollId != "0")
                //{
                //    DataTable dtPayrollList = gcPayroll.DataSource as DataTable;
                //    string[] PayrollId = ReportProperty.Current.PayrollId.ToString().Split(',');
                //    foreach (DataRow dr in dtPayrollList.Rows)
                //    {
                //        for (int i = 0; i < PayrollId.Length; i++)
                //        {
                //            if (dr["PAYROLLID"].ToString() == PayrollId[i])
                //            {
                //                //int index = dtPayrollList.Rows.IndexOf(dr);
                //                //gvNarration.SelectRow(index);
                //                dr["SELECT"] = 1;
                //            }
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        /// <summary>
        /// Get the selected payroll Group id
        /// </summary>
        /// <returns></returns>
        private string SelectedPayrollGroupId()
        {
            string selectedGroupId = string.Empty;
            string PayrollGroup = string.Empty;
            int SelectedGroupCount = 0;
            ReportProperty.Current.LedgalEntityId = string.Empty;
            try
            {
                //DataTable dtProject = gcGroups.DataSource as DataTable;
                //if (dtProject != null && dtProject.Rows.Count != 0)
                //{
                //    foreach (int i in gvGroups.GetSelectedRows())
                //    {
                //        DataRow row = gvGroups.GetDataRow(i);
                //        if (row != null)
                //        {
                //            selectedGroupId += row["GROUPID"].ToString() + ",";
                //            PayrollGroup += row["GROUPNAME"] + ",";
                //            SelectedGroupCount++;
                //        }
                //    }
                //    selectedGroupId = selectedGroupId.TrimEnd(',');
                //    PayrollGroup = PayrollGroup.TrimEnd(',');
                //    ReportProperty.Current.LedgalEntityId = ReportProperty.Current.LedgalEntityId.TrimEnd(',');
                //    if (PayrollGroup.Trim() != string.Empty)
                //    {
                //        if (PayrollGroupSelected.Rows.Count == SelectedGroupCount)
                //        {
                //            ReportProperty.Current.PayrollGroupName = SelectedGroupCount == 1 ? PayrollGroup : SCONSTATEMENT;
                //        }
                //        else
                //        {
                //            ReportProperty.Current.PayrollGroupName = PayrollGroup;
                //        }
                //    }
                //}

                DataTable dtPayroll = gcGroups.DataSource as DataTable;
                if (dtPayroll != null && dtPayroll.Rows.Count != 0)
                {
                    var Selected = (from d in dtPayroll.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);

                    if (Selected.Count() > 0)
                    {
                        BankSelected = Selected.CopyToDataTable();
                        if (BankSelected != null && BankSelected.Rows.Count != 0)
                        {
                            foreach (DataRow dr in BankSelected.Rows)
                            {
                                selectedGroupId += dr["GROUPID"].ToString() + ",";
                                PayrollGroup += dr["GROUPNAME"] + ",";
                                SelectedGroupCount++;
                            }
                            selectedGroupId = selectedGroupId.TrimEnd(',');
                            PayrollGroup = PayrollGroup.TrimEnd(',');
                            ReportProperty.Current.LedgalEntityId = ReportProperty.Current.LedgalEntityId.TrimEnd(',');
                            if (PayrollGroup.Trim() != string.Empty)
                            {
                                if (PayrollGroupSelected.Rows.Count == SelectedGroupCount)
                                {
                                    ReportProperty.Current.PayrollGroupName = SelectedGroupCount == 1 ? PayrollGroup : SCONSTATEMENT;
                                }
                                else
                                {
                                    ReportProperty.Current.PayrollGroupName = PayrollGroup;
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
            return selectedGroupId;
        }

        /// <summary>
        /// Fetch Payroll Group
        /// </summary>
        private void SetPayrollGroupSource()
        {
            try
            {
                //DataTable dtPayrollGroup = Paybase.Payrollgroup;
                //if (dtPayrollGroup != null && dtPayrollGroup.Rows.Count > 0)
                //{
                //    dtPayrollGroup.Columns.Add(SELECT, typeof(Int32));
                //    PayrollGroupSelected = dtPayrollGroup;
                //    ReportProperty.Current.RecordCount = dtPayrollGroup.Rows.Count;
                //    gcGroups.DataSource = null;
                //    gcGroups.DataSource = dtPayrollGroup;
                //    gcGroups.RefreshDataSource();
                //}
                DataTable dtPayrollGroup = Paybase.FetchPayrollGroupByPayroll(ReportProperty.Current.NumberSet.ToInteger(ReportProperty.Current.PayrollId));
                if (dtPayrollGroup != null && dtPayrollGroup.Rows.Count > 0)
                {
                    dtPayrollGroup.Columns.Add(SELECT, typeof(Int32));
                    PayrollGroupSelected = dtPayrollGroup;
                    ReportProperty.Current.RecordCount = dtPayrollGroup.Rows.Count;
                    gcGroups.DataSource = null;
                    gcGroups.DataSource = dtPayrollGroup;
                    gcGroups.RefreshDataSource();
                }
                if (!String.IsNullOrEmpty(ReportProperty.Current.PayrollId) && ReportProperty.Current.PayrollId != "0")
                {
                    DataTable dtPayrollGroupList = gcGroups.DataSource as DataTable;
                    string[] PayrollgrpId = ReportProperty.Current.PayrollGroupId.ToString().Split(',');
                    if (dtPayrollGroupList != null && dtPayrollGroupList.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtPayrollGroupList.Rows)
                        {
                            for (int i = 0; i < PayrollgrpId.Length; i++)
                            {
                                if (dr["GROUPID"].ToString() == PayrollgrpId[i])
                                {
                                    dr[SELECT] = 1;
                                }
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(ReportProperty.Current.PayrollGroupId) && ReportProperty.Current.PayrollGroupId != "0")
                {
                    DataTable dtGroups = gcGroups.DataSource as DataTable;
                    if (dtGroups != null && dtGroups.Rows.Count > 0)
                    {
                        string[] Groupid = ReportProperty.Current.PayrollGroupId.ToString().Split(',');
                        foreach (DataRow dr in dtGroups.Rows)
                        {
                            for (int i = 0; i < Groupid.Length; i++)
                            {
                                if (dr[this.payappschema.PRSTAFFGROUP.GROUPIDColumn.ColumnName].ToString() == Groupid[i])
                                {
                                    dr[SELECT] = 1;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        /// <summary>
        /// Get the selected payroll Group id
        /// </summary>
        /// <returns></returns>
        private string SelectedPayrollComponentId()
        {
            string selectedComponentId = string.Empty;
            string PayrollComponent = string.Empty;
            int SelectedGroupCount = 0;
            ReportProperty.Current.LedgalEntityId = string.Empty;
            try
            {
                DataTable dtComponentSelected = gcPayComponent.DataSource as DataTable;
                if (dtComponentSelected != null && dtComponentSelected.Rows.Count != 0)
                {
                    var Selected = (from d in dtComponentSelected.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);

                    if (Selected.Count() > 0)
                    {
                        BankSelected = Selected.CopyToDataTable();
                        if (BankSelected != null && BankSelected.Rows.Count != 0)
                        {
                            foreach (DataRow dr in BankSelected.Rows)
                            {
                                selectedComponentId += dr["COMPONENTID"].ToString() + ",";
                                PayrollComponent += dr["COMPONENT"] + ",";
                                SelectedGroupCount++;
                            }
                            selectedComponentId = selectedComponentId.TrimEnd(',');
                            PayrollComponent = PayrollComponent.TrimEnd(',');
                            ReportProperty.Current.LedgalEntityId = ReportProperty.Current.LedgalEntityId.TrimEnd(',');

                            if (PayrollComponent.Trim() != string.Empty)
                            {
                                clsPayrollBase.payrollcomponent = PayrollComponent;
                                //if (PayrollGroupSelected.Rows.Count == SelectedGroupCount)
                                //{
                                //    ReportProperty.Current.PayrollComponentName = SelectedGroupCount == 1 ? PayrollComponent : SCONSTATEMENT;
                                //}
                                //else
                                //{
                                //    ReportProperty.Current.PayrollComponentName = PayrollComponent;
                                //}
                                ReportProperty.Current.PayrollComponentName = PayrollComponent;
                            }
                        }
                    }
                }

                //if (!string.IsNullOrEmpty(ReportProperty.Current.PayrollGroupId) && ReportProperty.Current.PayrollGroupId != "0")
                //{
                //    DataTable dtSelectedComponent = gcPayComponent.DataSource as DataTable;
                //    if (dtSelectedComponent != null && dtSelectedComponent.Rows.Count > 0)
                //    {
                //        string[] SelectedComponentsId = ReportProperty.Current.PayrollComponentId.ToString().Split(',');
                //        foreach (DataRow dr in dtSelectedComponent.Rows)
                //        {
                //            for (int i = 0; i < SelectedComponentsId.Length; i++)
                //            {
                //                if (dr[this.payappschema.PRCOMPONENT.COMPONENTIDColumn.ColumnName].ToString() == SelectedComponentsId[i])
                //                {
                //                    int index = dtSelectedComponent.Rows.IndexOf(dr);
                //                    gvPayComponent.SelectRow(index);
                //                }
                //            }
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
            return selectedComponentId;
        }

        private void SetComponentStaffSource()
        {
            try
            {
                DataTable dtPayrollComponent = new DataTable();
                string payrollGroupIds = SelectedPayrollGroupId();
                string payrollid = !string.IsNullOrEmpty(ReportProperty.Current.PayrollId) ? ReportProperty.Current.PayrollId : "0";
                resultArgs = Paybase.FetchPayrollComponent(!string.IsNullOrEmpty(payrollGroupIds) ? payrollGroupIds : "0", payrollid);
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    if (ReportProperty.Current.ReportId != "RPT-074")
                    {
                        DataTable dtTemp = resultArgs.DataSource.Table;
                        DataView dvtype = dtTemp.AsDataView();
                        dvtype.RowFilter = "TYPE <> 2";
                        dtPayrollComponent = dvtype.ToTable();
                    }
                    else
                    {
                        dtPayrollComponent = resultArgs.DataSource.Table;
                    }

                }
                if (dtPayrollComponent != null && dtPayrollComponent.Rows.Count > 0)
                {
                    gcPayComponent.DataSource = null;
                    dtPayrollComponent.Columns.Add(SELECT, typeof(int));
                    gcPayComponent.DataSource = dtPayrollComponent;
                    gcPayComponent.RefreshDataSource();
                }
                if (!string.IsNullOrEmpty(ReportProperty.Current.PayrollGroupId) && ReportProperty.Current.PayrollGroupId != "0")
                {
                    DataTable dtSelectedComponent = gcPayComponent.DataSource as DataTable;
                    if (dtSelectedComponent != null && dtSelectedComponent.Rows.Count > 0)
                    {
                        string[] SelectedComponentsId = ReportProperty.Current.PayrollComponentId.ToString().Split(',');
                        foreach (DataRow dr in dtSelectedComponent.Rows)
                        {
                            for (int i = 0; i < SelectedComponentsId.Length; i++)
                            {
                                if (dr[this.payappschema.PRCOMPONENT.COMPONENTIDColumn.ColumnName].ToString() == SelectedComponentsId[i])
                                {
                                    dr[SELECT] = 1;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }
        /// <summary>
        /// Get the selected payroll Group id
        /// </summary>
        /// <returns></returns>
        private string SelectedPayrollStaffId()
        {
            string selectedStaffId = string.Empty;
            string PayrollStaff = string.Empty;
            int SelectedGroupCount = 0;
            ReportProperty.Current.LedgalEntityId = string.Empty;
            try
            {
                //DataTable dtStaffSelected = gcPayStaff.DataSource as DataTable;
                //if (dtStaffSelected != null && dtStaffSelected.Rows.Count != 0)
                //{
                //    foreach (int i in gvPayrollStaff.GetSelectedRows())
                //    {
                //        DataRow row = gvPayrollStaff.GetDataRow(i);
                //        if (row != null)
                //        {
                //            selectedStaffId += row["STAFFID"].ToString() + ",";
                //            PayrollStaff += row["STAFFNAME"] + ",";
                //            SelectedGroupCount++;
                //        }
                //    }
                //    selectedStaffId = selectedStaffId.TrimEnd(',');
                //    PayrollStaff = PayrollStaff.TrimEnd(',');
                //    ReportProperty.Current.LedgalEntityId = ReportProperty.Current.LedgalEntityId.TrimEnd(',');
                //    if (PayrollStaff.Trim() != string.Empty)
                //    {
                //        if (PayrollGroupSelected.Rows.Count == SelectedGroupCount)
                //        {
                //            ReportProperty.Current.PayrollStaffName = SelectedGroupCount == 1 ? PayrollStaff : SCONSTATEMENT;
                //        }
                //        else
                //        {
                //            ReportProperty.Current.PayrollStaffName = PayrollStaff;
                //        }
                //    }
                //}

                DataTable dtStaffComponent = gcPayStaff.DataSource as DataTable;
                if (dtStaffComponent != null && dtStaffComponent.Rows.Count != 0)
                {
                    var Selected = (from d in dtStaffComponent.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);

                    if (Selected.Count() > 0)
                    {
                        BankSelected = Selected.CopyToDataTable();
                        if (BankSelected != null && BankSelected.Rows.Count != 0)
                        {
                            foreach (DataRow dr in BankSelected.Rows)
                            {
                                selectedStaffId += dr["STAFFID"].ToString() + ",";
                                PayrollStaff += dr["STAFFNAME"] + ",";
                                SelectedGroupCount++;
                            }
                            selectedStaffId = selectedStaffId.TrimEnd(',');
                            PayrollStaff = PayrollStaff.TrimEnd(',');
                            ReportProperty.Current.LedgalEntityId = ReportProperty.Current.LedgalEntityId.TrimEnd(',');
                            if (PayrollStaff.Trim() != string.Empty)
                            {
                                if (PayrollGroupSelected.Rows.Count == SelectedGroupCount)
                                {
                                    ReportProperty.Current.PayrollStaffName = SelectedGroupCount == 1 ? PayrollStaff : SCONSTATEMENT;
                                }
                                else
                                {
                                    ReportProperty.Current.PayrollStaffName = PayrollStaff;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
            return selectedStaffId;
        }
        private void SetStaffSource()
        {
            try
            {
                gcPayStaff.DataSource = null;
                DataTable dtPayrollStaff = new DataTable();
                string payrollGroupIds = SelectedPayrollGroupId();
                string payrollid = !string.IsNullOrEmpty(ReportProperty.Current.PayrollId) ? ReportProperty.Current.PayrollId : "0";
                resultArgs = Paybase.FetchPayrollStaff(!string.IsNullOrEmpty(payrollGroupIds) ? payrollGroupIds : "0", payrollid,
                    SettingProperty.PayrollFinanceEnabled ? SelectedProject() : string.Empty);
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    dtPayrollStaff = resultArgs.DataSource.Table;
                }
                if (dtPayrollStaff != null && dtPayrollStaff.Rows.Count > 0)
                {
                    gcPayStaff.DataSource = null;
                    dtPayrollStaff.Columns.Add(SELECT, typeof(int));
                    gcPayStaff.DataSource = dtPayrollStaff;
                    gcPayStaff.RefreshDataSource();
                }
                if (!string.IsNullOrEmpty(ReportProperty.Current.PayrollGroupId) && ReportProperty.Current.PayrollGroupId != "0")
                {
                    DataTable dtSelectedStaff = gcPayStaff.DataSource as DataTable;
                    if (dtSelectedStaff != null && dtSelectedStaff.Rows.Count > 0)
                    {
                        string[] SelectedStaffId = ReportProperty.Current.PayrollStaffId.ToString().Split(',');
                        foreach (DataRow dr in dtSelectedStaff.Rows)
                        {
                            for (int i = 0; i < SelectedStaffId.Length; i++)
                            {
                                if (dr[this.payappschema.PRSTAFF.STAFFIDColumn.ColumnName].ToString() == SelectedStaffId[i])
                                {
                                    dr[SELECT] = 1;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }
        #endregion

        #region Payroll Events
        private void rchkPayrollSelect_CheckedChanged(object sender, EventArgs e)
        {
            //if (gvPayroll.RowCount > 0)
            //{
            //    int payrid = gvPayroll.GetFocusedRowCellValue(colPayrollId) != null ? ReportProperty.Current.NumberSet.ToInteger(gvPayroll.GetFocusedRowCellValue(colPayrollId).ToString()) : 0;
            //    CheckEdit chkEdit = sender as CheckEdit;
            //    int status = Convert.ToInt32(chkEdit.CheckState);
            //    UnSelectPayroll(payrid, status);
            //}
        }

        //private void UnSelectPayroll(int payid, int status)
        //{
        //    DataTable dtPayrollSource = (DataTable)gcPayroll.DataSource;

        //    for (int i = 0; i < dtPayrollSource.Rows.Count; i++)
        //    {
        //        int payrollid = dtPayrollSource.Rows[i]["PAYROLLID"] != DBNull.Value ? ReportProperty.Current.NumberSet.ToInteger(dtPayrollSource.Rows[i]["PAYROLLID"].ToString()) : 0;
        //        if (payrollid == payid)
        //        {
        //            dtPayrollSource.Rows[i]["SELECT"] = status;
        //        }
        //        else
        //        {
        //            dtPayrollSource.Rows[i]["SELECT"] = (int)YesNo.No;
        //        }
        //    }
        //}

        //private void UnSelectPayrollGroup(int PayGroupid, int status)
        //{
        //    DataTable dtPayrollGroupSource = (DataTable)gcGroups.DataSource;

        //    for (int i = 0; i < dtPayrollGroupSource.Rows.Count; i++)
        //    {
        //        int payrollGroupid = dtPayrollGroupSource.Rows[i]["GROUPID"] != DBNull.Value ? ReportProperty.Current.NumberSet.ToInteger(dtPayrollGroupSource.Rows[i]["GROUPID"].ToString()) : 0;
        //        if (payrollGroupid == PayGroupid)
        //        {
        //            dtPayrollGroupSource.Rows[i]["SELECT"] = status;
        //        }
        //        else
        //        {
        //            dtPayrollGroupSource.Rows[i]["SELECT"] = (int)YesNo.No;
        //        }
        //    }
        //}

        private void glkpPayroll_EditValueChanged(object sender, EventArgs e)
        {
            ReportProperty.Current.PayrollId = glkpPayroll.EditValue.ToString();
            SetPayrollGroupSource();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvProject_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            // SelectProjects(Convert.ToInt32(gvProject.GetRowCellValue(e.ControllerRow, colProjectId)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProjectIds"></param>
        private void SelectProjects(int ProjectIds)
        {
            //try
            //{
            //    if (ProjectIds != 0)
            //    {
            //        for (int i = 0; i < dtselectedProjectId.Rows.Count; i++)
            //        {
            //            if (ProjectIds == Convert.ToInt32(dtselectedProjectId.Rows[i][this.appSchema.Project.PROJECT_IDColumn.ColumnName].ToString()))
            //            {
            //                if (dtselectedProjectId.Rows[i]["SELECT"].ToString() == "0")
            //                    dtselectedProjectId.Rows[i]["SELECT"] = 1;
            //                else
            //                    dtselectedProjectId.Rows[i]["SELECT"] = 0;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        for (int i = 0; i < dtselectedProjectId.Rows.Count; i++)
            //        {
            //            if (dtselectedProjectId.Rows[i]["SELECT"].ToString() == "1")
            //            {
            //                gvProject.SelectRow(i);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ex.ToString();
            //}
        }

        private void chkDeducteeType_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtDeducteeType = gcDeducteeType.DataSource as DataTable;
                if (dtDeducteeType != null && dtDeducteeType.Rows.Count > 0)
                {
                    if (!dtDeducteeType.Columns.Contains(SELECT))
                    {
                        dtDeducteeType.Columns.Add(SELECT, typeof(Int32));
                    }
                    foreach (DataRow dr in dtDeducteeType.Rows)
                    {
                        dr[SELECT] = chkDeducteeType.Checked;
                    }
                    gcDeducteeType.DataSource = dtDeducteeType;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void chkPayroll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtPayroll = gcGroups.DataSource as DataTable;
                if (dtPayroll != null && dtPayroll.Rows.Count > 0)
                {
                    if (!dtPayroll.Columns.Contains(SELECT))
                    {
                        dtPayroll.Columns.Add(SELECT, typeof(Int32));
                    }
                    foreach (DataRow dr in dtPayroll.Rows)
                    {
                        dr[SELECT] = chkPayroll.Checked;
                    }
                    gcGroups.DataSource = dtPayroll;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void chkPayrollStaff_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtPayrollStaff = gcPayStaff.DataSource as DataTable;
                if (dtPayrollStaff != null && dtPayrollStaff.Rows.Count > 0)
                {
                    if (!dtPayrollStaff.Columns.Contains(SELECT))
                    {
                        dtPayrollStaff.Columns.Add(SELECT, typeof(Int32));
                    }
                    foreach (DataRow dr in dtPayrollStaff.Rows)
                    {
                        dr[SELECT] = chkPayrollStaff.Checked;
                    }
                    gcPayStaff.DataSource = dtPayrollStaff;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void chkPayrollComponents_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtPayrollComponents = gcPayComponent.DataSource as DataTable;
                if (dtPayrollComponents != null && dtPayrollComponents.Rows.Count > 0)
                {
                    if (!dtPayrollComponents.Columns.Contains(SELECT))
                    {
                        dtPayrollComponents.Columns.Add(SELECT, typeof(Int32));
                    }
                    foreach (DataRow dr in dtPayrollComponents.Rows)
                    {
                        dr[SELECT] = chkPayrollComponents.Checked;
                    }
                    gcPayComponent.DataSource = dtPayrollComponents;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void chkBank_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtAllBank = gcBank.DataSource as DataTable;
                if (dtAllBank != null && dtAllBank.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtAllBank.Rows)
                    {
                        dr[SELECT] = chkBank.Checked;
                    }
                    gcBank.DataSource = dtAllBank;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void chkLedger_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string ledgerids = string.Empty;
                DataTable dtAllLedger = gcLedgerDetail.DataSource as DataTable;
                if (dtAllLedger != null && dtAllLedger.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtAllLedger.Rows)
                    {
                        dr[SELECT] = chkLedger.Checked;
                        ledgerids += dr[this.appSchema.Ledger.LEDGER_IDColumn.ColumnName] + ",";
                    }
                    ledgerids = ledgerids.TrimEnd(',');
                    ledgerids = String.IsNullOrEmpty(ledgerids) ? "" : ledgerids;

                    gcLedgerDetail.DataSource = dtAllLedger;

                    string[] reportCriteria = ReportProperty.Current.ReportCriteria.Split('ÿ');
                    for (int i = 0; i < reportCriteria.Length; i++)
                    {
                        switch (reportCriteria[i])
                        {

                            case "CC": //On 29/11/2022, to load Project's Ledger's CC
                                {
                                    //On 30/01/2025, Show CC details for all or budget related based on enable CC budget
                                    if (ReportProperty.Current.ReportGroup != "Budget" ||
                                    (ReportProperty.Current.ReportGroup == "Budget" && SettingProperty.Current.EnableCostCentreBudget == 1))
                                    {
                                        string projectids = SelectedProject();
                                        SetCostCentreSource(projectids, ledgerids);
                                    }
                                    break;
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void chkProject_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string ProjectId = string.Empty;
                DataTable dtAllProject = gcProject.DataSource as DataTable;
                if (dtAllProject != null && dtAllProject.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtAllProject.Rows)
                    {
                        if (chkProject.Checked)
                        {
                            ProjectId += dr[this.appSchema.Project.PROJECT_IDColumn.ColumnName] + ",";
                        }
                        dr[SELECT] = chkProject.Checked;
                    }
                    ProjectId = ProjectId.TrimEnd(',');
                    ProjectId = String.IsNullOrEmpty(ProjectId) ? "0" : ProjectId;

                    string[] reportCriteria = ReportProperty.Current.ReportCriteria.Split('ÿ');
                    for (int i = 0; i < reportCriteria.Length; i++)
                    {
                        switch (reportCriteria[i])
                        {
                            case "BK":
                                {
                                    dtBank = FetchBankByProjectId(ProjectId);
                                    if (ReportProperty.Current.ReportId == "RPT-047" || ReportProperty.Current.ReportId == "RPT-094")
                                    {
                                        dtBank = FetchFDByProjectId(ProjectId);
                                    }
                                    AttachCashLedgerByProject(ProjectId, dtBank);
                                    gcBank.DataSource = dtBank;
                                    gcBank.RefreshDataSource();
                                    break;
                                }
                            case "BU":
                                {
                                    SetBudgetSource();
                                    break;
                                }
                            case "CC": //On 15/11/2019, to load Project's CC
                                {
                                    //On 30/01/2025, Show CC details for all or budget related based on enable CC budget
                                    if (ReportProperty.Current.ReportGroup != "Budget" ||
                                    (ReportProperty.Current.ReportGroup == "Budget" && SettingProperty.Current.EnableCostCentreBudget == 1))
                                    {
                                        string ledgerids = SelectedLedgerGroupDetails();
                                        SetCostCentreSource(ProjectId, ledgerids);
                                    }
                                    break;
                                }
                            case "SD": //On 27/02/2021, Reload sign details based on project selection
                                {
                                    string projects = SelectedProject();
                                    ReportProperty.Current.AssignSignDetails(projects);
                                    FillSignDetails();
                                    break;
                                }
                        }
                    }
                    gcProject.DataSource = null;
                    gcProject.DataSource = dtAllProject;
                }
                this.chkBank.CheckedChanged -= new System.EventHandler(this.chkBank_CheckedChanged);
                chkBank.Checked = false;
                this.chkBank.CheckedChanged += new System.EventHandler(this.chkBank_CheckedChanged);
                setmultibank(ProjectId);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void chkCostCentre_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtCostCentre = gcCostCentre.DataSource as DataTable;
                if (dtCostCentre != null && dtCostCentre.Rows.Count > 0)
                {
                    if (!dtCostCentre.Columns.Contains(SELECT))
                    {
                        dtCostCentre.Columns.Add(SELECT, typeof(Int32));
                    }
                    foreach (DataRow dr in dtCostCentre.Rows)
                    {
                        dr[SELECT] = chkCostCentre.Checked;
                    }
                    gcCostCentre.DataSource = dtCostCentre;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private string SelectedBankDetails()
        {
            string selectedBankAccount = string.Empty;
            string BankAccountId = string.Empty;
            string BankAccountName = string.Empty;
            string UnSelectedAccountId = string.Empty;
            string SelectedLedgerId = string.Empty;
            ReportProperty.Current.Count = 0;
            try
            {
                DataTable dtBank = gcBank.DataSource as DataTable;
                if (dtBank != null && dtBank.Rows.Count != 0)
                {
                    var Selected = (from d in dtBank.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);

                    var UnSelectedBank = (from d in dtBank.AsEnumerable()
                                          where ((d.Field<Int32?>(SELECT) != 1))
                                          select d);

                    if (Selected.Count() > 0)
                    {
                        BankSelected = Selected.CopyToDataTable();
                        ReportProperty.Current.FDAccountID = "";
                        if (BankSelected != null && BankSelected.Rows.Count != 0)
                        {
                            foreach (DataRow dr in BankSelected.Rows)
                            {
                                selectedBankAccount += dr[this.appSchema.Bank.BANK_IDColumn.ColumnName] + ",";
                                BankAccountId += dr[this.appSchema.BankAccount.BANK_ACCOUNT_IDColumn.ColumnName] + ",";
                                SelectedLedgerId += dr[this.appSchema.Ledger.LEDGER_IDColumn.ColumnName] + ",";
                                ReportProperty.Current.Count++;
                                BankAccountName += dr[this.appSchema.Bank.BANKColumn.ColumnName].ToString() + ",";
                                ReportProperty.Current.CashBankGroupId = dr[this.appSchema.Ledger.GROUP_IDColumn.ColumnName].ToString(); //RPT-153, RPT-154
                                if (ReportProperty.Current.ReportId == "RPT-094")
                                {
                                    // This is to enable Multiple Selected Ids rather than Single Selections

                                    // ReportProperty.Current.FDAccountID = dr[this.appSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName].ToString(); // + ",";
                                    ReportProperty.Current.FDAccountID += dr[this.appSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName].ToString() + ",";
                                }
                            }
                            BankAccountName = BankAccountName.TrimEnd(',');
                            ReportProperty.Current.FDAccountID = ReportProperty.Current.FDAccountID.Trim(',');
                            ReportProperty.Current.BankAccountName = ReportProperty.Current.Count == 1 ? BankAccountName : string.Empty;
                            selectedBankAccount = selectedBankAccount.TrimEnd(',');
                            BankAccountId = BankAccountId.TrimEnd(',');
                            SelectedLedgerId = SelectedLedgerId.TrimEnd(',');
                        }
                    }
                    if (UnSelectedBank.Count() > 0)
                    {
                        DataTable UnSelectedBankAccount = UnSelectedBank.CopyToDataTable();
                        if (UnSelectedBankAccount != null && UnSelectedBankAccount.Rows.Count != 0)
                        {
                            foreach (DataRow dr in UnSelectedBankAccount.Rows)
                            {
                                UnSelectedAccountId += dr[this.appSchema.BankAccount.BANK_ACCOUNT_IDColumn.ColumnName] + ",";
                            }
                            UnSelectedAccountId = UnSelectedAccountId.TrimEnd(',');
                        }
                    }
                    ReportProperty.Current.UnSelectedBankAccountId = UnSelectedAccountId;
                    //ReportProperty.Current.Ledger = SelectedLedgerId;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
            return SelectedLedgerId;
        }

        private void gvProject_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string ProjectId = string.Empty;
            string[] aCriteria = ReportProperty.Current.ReportCriteria.Split('ÿ');
            try
            {
                for (int i = 0; i < aCriteria.Length; i++)
                {
                    switch (aCriteria[i])
                    {
                        case "BK":
                            {
                                chkBank.Checked = false;
                                DataTable dtProjectId = (DataTable)gcProject.DataSource;
                                if (dtProjectId != null && dtProjectId.Rows.Count != 0)
                                {
                                    var Selected = (from d in dtProjectId.AsEnumerable()
                                                    where ((d.Field<Int32?>(SELECT) == 1))
                                                    select d);

                                    if (Selected.Count() != 0)
                                    {
                                        DataTable dtBank = Selected.CopyToDataTable();
                                        foreach (DataRow dr in dtBank.Rows)
                                        {
                                            ProjectId += dr[this.appSchema.Project.PROJECT_IDColumn.ColumnName] + ",";
                                        }
                                        ProjectId = ProjectId.TrimEnd(',');

                                        DataTable dtBankInfo = FetchBankByProjectId(ProjectId);
                                        AttachCashLedgerByProject(ProjectId, dtBankInfo);
                                        if (ReportProperty.Current.ReportId == "RPT-047" || ReportProperty.Current.ReportId == "RPT-094")
                                        {
                                            dtBankInfo = FetchFDByProjectId(ProjectId);
                                        }
                                        gcBank.DataSource = null;
                                        gcBank.DataSource = dtBankInfo;
                                    }
                                    else
                                    {
                                        SetBankAccountSource();
                                    }
                                }
                                break;
                            }
                        case "BU":
                            {
                                SetBudgetSource();

                                /*chkBank.Checked = false;
                                DataTable dtProjectId = (DataTable)gcProject.DataSource;
                                DataTable dtBudgetId = (DataTable)gcBank.DataSource;
                                if (dtProjectId != null && dtProjectId.Rows.Count != 0)
                                {
                                    var Selected = (from d in dtProjectId.AsEnumerable()
                                                    where ((d.Field<Int32?>(SELECT) == 1))
                                                    select d);
                                    if (Selected.Count() != 0)
                                    {
                                        DataTable dtBank = Selected.CopyToDataTable();
                                        foreach (DataRow dr in dtBank.Rows)
                                        {
                                            ProjectId += dr[this.appSchema.Project.PROJECT_IDColumn.ColumnName] + ",";
                                        }
                                        ProjectId = ProjectId.TrimEnd(',');

                                        dtBudgetId = FetchBudgetsByProjects(ProjectId, true);
                                        if (dtBudgetId != null)
                                            dtBudgetId.Columns.Add(SELECT, typeof(Int32));

                                        gcBank.DataSource = dtBudgetId;
                                        gcBank.RefreshDataSource();
                                    }
                                    else
                                    {
                                        using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchBudgetNames))
                                        {
                                            dataManager.Parameters.Add(this.appSchema.Budget.DATE_FROMColumn, DateFrom.DateTime);
                                            dataManager.Parameters.Add(this.appSchema.Budget.DATE_TOColumn, DateTo.DateTime);
                                            dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                                            ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                                            if (resultArgs.Success)
                                            {
                                                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                                                {
                                                    resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                                                    BudgetSelected = resultArgs.DataSource.Table;
                                                    ReportProperty.Current.RecordCount = resultArgs.DataSource.Table.Rows.Count;
                                                    gcBank.DataSource = null;
                                                    gcBank.DataSource = resultArgs.DataSource.Table;
                                                    gcBank.RefreshDataSource();
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    SetBudgetSource();
                                }*/
                                break;
                            }
                        case "BB":
                            {
                                SetBudgetSourceforGridLookup();
                                break;
                            }
                        case "CC": //On 15/11/2019, to load Project's CC
                            {
                                //On 30/01/2025, Show CC details for all or budget related based on enable CC budget
                                if (ReportProperty.Current.ReportGroup != "Budget" ||
                                (ReportProperty.Current.ReportGroup == "Budget" && SettingProperty.Current.EnableCostCentreBudget == 1))
                                {
                                    string ledgerids = SelectedLedgerGroupDetails();
                                    string projects = SelectedProject();
                                    SetCostCentreSource(projects, ledgerids);
                                }
                                break;
                            }
                        case "SD": //On 27/02/2021, Reload sign details based on project selection
                            {
                                string projects = SelectedProject();
                                ReportProperty.Current.AssignSignDetails(projects);
                                FillSignDetails();
                                break;
                            }

                    }
                }
                setmultibank(ProjectId);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.StackTrace, true);
            }
            finally { }
        }

        private void gvLedgerDetails_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string[] reportCriteria = ReportProperty.Current.ReportCriteria.Split('ÿ');
            for (int i = 0; i < reportCriteria.Length; i++)
            {
                switch (reportCriteria[i])
                {

                    case "CC": //On 29/11/2022, to load Project's Ledger's CC
                        {
                            //On 30/01/2025, Show CC details for all or budget related based on enable CC budget
                            if (ReportProperty.Current.ReportGroup != "Budget" ||
                            (ReportProperty.Current.ReportGroup == "Budget" && SettingProperty.Current.EnableCostCentreBudget == 1))
                            {
                                string projectids = SelectedProject();
                                string ledgerids = SelectedLedgerGroupDetails();
                                SetCostCentreSource(projectids, ledgerids);
                            }
                            break;
                        }
                }
            }
        }

        private void chkLedgerGroup_CheckedChanged(object sender, EventArgs e)
        {
            string ledgerGroup = string.Empty;
            try
            {
                this.chkLedger.CheckedChanged -= new System.EventHandler(this.chkLedger_CheckedChanged);
                chkLedger.Checked = false;
                this.chkLedger.CheckedChanged += new System.EventHandler(this.chkLedger_CheckedChanged);
                DataTable dtLedger = gcLedger.DataSource as DataTable;
                if (dtLedger != null && dtLedger.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtLedger.Rows)
                    {
                        ledgerGroup += dr[this.appSchema.LedgerGroup.GROUP_IDColumn.ColumnName] + ",";
                        dr[SELECT] = chkLedgerGroup.Checked;
                    }
                    ledgerGroup = ledgerGroup.TrimEnd(',');

                    DataView dvGroup = null;
                    if (LedgerSelected != null && LedgerSelected.Rows.Count != 0)
                    {
                        dvGroup = LedgerSelected.DefaultView;
                        dvGroup.RowFilter = "GROUP_ID IN (" + ledgerGroup + ")";
                        gcLedgerDetail.DataSource = dvGroup.ToTable();
                    }
                    dvGroup.RowFilter = "";
                    gcLedger.DataSource = dtLedger;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void rchkSelectProject_CheckedChanged(object sender, EventArgs e)
        {
            this.chkProject.CheckedChanged -= new System.EventHandler(this.chkProject_CheckedChanged);
            chkProject.Checked = false;
            gvBank.Focus();
            //SetStaffSource(); On 23/12/2022
            this.chkProject.CheckedChanged += new System.EventHandler(this.chkProject_CheckedChanged);
        }

        private void rchkBankCheck_CheckedChanged(object sender, EventArgs e)
        {
            this.chkBank.CheckedChanged -= new System.EventHandler(this.chkBank_CheckedChanged);
            chkBank.Checked = false;
            this.chkBank.CheckedChanged += new System.EventHandler(this.chkBank_CheckedChanged);
        }

        private void rchkledger_CheckedChanged(object sender, EventArgs e)
        {
            this.chkLedger.CheckedChanged -= new System.EventHandler(this.chkLedger_CheckedChanged);
            chkLedger.Checked = false;
            this.chkLedger.CheckedChanged += new System.EventHandler(this.chkLedger_CheckedChanged);
        }

        private void rchkSelect_CheckedChanged(object sender, EventArgs e)
        {
            this.chkCostCentre.CheckedChanged -= new System.EventHandler(this.chkCostCentre_CheckedChanged);
            chkCostCentre.Checked = false;
            this.chkCostCentre.CheckedChanged += new System.EventHandler(this.chkCostCentre_CheckedChanged);
        }

        private void rchkCheck_CheckedChanged(object sender, EventArgs e)
        {
            gvLedgerDetails.Focus();
        }

        /// <summary>
        /// Reassign the selected project with checked 
        /// </summary>
        private void AssignSelectedProject()
        {
            DataTable dtCheckedProject = new DataTable();
            DataTable dtProjectAll = new DataTable();
            try
            {
                if (!String.IsNullOrEmpty(ReportProperty.Current.Project) && ReportProperty.Current.Project != "0")
                {
                    SetProjectSource();
                    if (ProjectSelected != null)
                    {
                        dtCheckedProject = ProjectSelected.Clone();
                        dtProjectAll = ProjectSelected.Copy();
                        string[] project = ReportProperty.Current.Project.Split(',');
                        for (int i = 0; i < ProjectSelected.Rows.Count; i++)
                        {
                            for (int j = 0; j < project.Length; j++)
                            {
                                if (ProjectSelected.Rows[i][0].ToString() == project[j].ToString())
                                {
                                    DataRow drProject = dtCheckedProject.NewRow();
                                    drProject["PROJECT_ID"] = ReportProperty.Current.NumberSet.ToInteger(ProjectSelected.Rows[i][0].ToString());
                                    drProject["PROJECT"] = ProjectSelected.Rows[i][2].ToString();
                                    drProject["SELECT"] = 1;
                                    dtCheckedProject.Rows.Add(drProject);
                                }
                            }
                        }
                        DataTable dtProjectTable = RemoveChecked(dtProjectAll, dtCheckedProject, dtProjectAll.Columns[1].ColumnName, dtCheckedProject.Columns[1].ColumnName);
                        if (dtProjectTable != null && dtProjectTable.Rows.Count != 0)
                        {
                            dtCheckedProject.Merge(dtProjectTable);
                        }
                        else
                        {
                            this.chkProject.CheckedChanged -= new System.EventHandler(this.chkProject_CheckedChanged);
                            chkProject.Checked = true;
                            this.chkProject.CheckedChanged += new System.EventHandler(this.chkProject_CheckedChanged);
                        }
                        gcProject.DataSource = dtCheckedProject;
                        gcProject.RefreshDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        /// <summary>
        /// Re assign the selected bank details.
        /// </summary>
        private void AssignSelectedBankDetails()
        {
            DataTable dtCheckedBankAccount = new DataTable();
            DataTable dtBankAll = new DataTable();
            try
            {
                if (!String.IsNullOrEmpty(ReportProperty.Current.Ledger) && ReportProperty.Current.Ledger != "0")
                {
                    SetBankAccountSource();
                    if (BankSelected != null)
                    {
                        dtCheckedBankAccount = BankSelected.Clone();
                        dtBankAll = BankSelected.Copy();
                        string[] bank = ReportProperty.Current.Ledger.Split(',');
                        for (int i = 0; i < BankSelected.Rows.Count; i++)
                        {
                            for (int j = 0; j < bank.Length; j++)
                            {
                                if (BankSelected.Rows[i][4].ToString() == bank[j].ToString())
                                {
                                    DataRow drBankAccount = dtCheckedBankAccount.NewRow();
                                    drBankAccount["BANK_ID"] = ReportProperty.Current.NumberSet.ToInteger(BankSelected.Rows[i][0].ToString());
                                    drBankAccount["BANK"] = BankSelected.Rows[i][1].ToString();
                                    drBankAccount["GROUP_ID"] = ReportProperty.Current.NumberSet.ToInteger(BankSelected.Rows[i][2].ToString());
                                    drBankAccount["PROJECT_ID"] = ReportProperty.Current.NumberSet.ToInteger(BankSelected.Rows[i][3].ToString());
                                    drBankAccount["BANK_ACCOUNT_ID"] = ReportProperty.Current.NumberSet.ToInteger(BankSelected.Rows[i][4].ToString());
                                    drBankAccount["LEDGER_ID"] = ReportProperty.Current.NumberSet.ToInteger(BankSelected.Rows[i][5].ToString());
                                    drBankAccount["SELECT"] = 1;
                                    dtCheckedBankAccount.Rows.Add(drBankAccount);
                                }
                            }
                        }
                        if (ReportProperty.Current.UnSelectedBankAccountId != "")
                        {
                            string[] UnSelectedBankId = ReportProperty.Current.UnSelectedBankAccountId.Split(',');
                            for (int i = 0; i < BankSelected.Rows.Count; i++)
                            {
                                for (int j = 0; j < UnSelectedBankId.Length; j++)
                                {
                                    if (BankSelected.Rows[i][4].ToString() == UnSelectedBankId[j].ToString())
                                    {
                                        DataRow drBankAccount = dtCheckedBankAccount.NewRow();
                                        drBankAccount["BANK_ID"] = ReportProperty.Current.NumberSet.ToInteger(BankSelected.Rows[i][0].ToString());
                                        drBankAccount["BANK"] = BankSelected.Rows[i][1].ToString();
                                        drBankAccount["GROUP_ID"] = ReportProperty.Current.NumberSet.ToInteger(BankSelected.Rows[i][2].ToString());
                                        drBankAccount["PROJECT_ID"] = ReportProperty.Current.NumberSet.ToInteger(BankSelected.Rows[i][3].ToString());
                                        drBankAccount["BANK_ACCOUNT_ID"] = ReportProperty.Current.NumberSet.ToInteger(BankSelected.Rows[i][4].ToString());
                                        drBankAccount["LEDGER_ID"] = ReportProperty.Current.NumberSet.ToInteger(BankSelected.Rows[i][5].ToString());
                                        dtCheckedBankAccount.Rows.Add(drBankAccount);
                                    }
                                }
                            }
                        }
                        else
                        {
                            this.chkBank.CheckedChanged -= new System.EventHandler(this.chkBank_CheckedChanged);
                            chkBank.Checked = true;
                            this.chkBank.CheckedChanged += new System.EventHandler(this.chkBank_CheckedChanged);
                        }

                        if (dtCheckedBankAccount != null && dtCheckedBankAccount.Rows.Count != 0)
                        {
                            var UniqueRows = dtCheckedBankAccount.AsEnumerable().Distinct(DataRowComparer.Default);
                            dtCheckedBankAccount = UniqueRows.CopyToDataTable();
                        }
                        gcBank.DataSource = dtCheckedBankAccount;
                        gcBank.RefreshDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }
        /// <summary>
        /// Re assign the selected budget details.
        /// </summary>
        private void AssignSelectedBudgetDetails()
        {
            DataTable dtCheckedBudget = new DataTable();
            DataTable dtBudgetAll = new DataTable();
            try
            {
                if (!String.IsNullOrEmpty(ReportProperty.Current.Budget) && ReportProperty.Current.Budget != "0")
                {
                    SetBudgetSource();
                    if (BudgetSelected != null)
                    {
                        dtCheckedBudget = BudgetSelected.Clone();
                        dtBudgetAll = BudgetSelected.Copy();
                        string[] budget = ReportProperty.Current.Ledger.Split(',');
                        for (int i = 0; i < BudgetSelected.Rows.Count; i++)
                        {
                            for (int j = 0; j < budget.Length; j++)
                            {
                                if (BudgetSelected.Rows[i][4].ToString() == budget[j].ToString())
                                {
                                    DataRow drBankAccount = dtCheckedBudget.NewRow();
                                    drBankAccount["BANK_ID"] = ReportProperty.Current.NumberSet.ToInteger(BudgetSelected.Rows[i][0].ToString());
                                    drBankAccount["BANK"] = BankSelected.Rows[i][1].ToString();
                                    drBankAccount["PROJECT_ID"] = ReportProperty.Current.NumberSet.ToInteger(BudgetSelected.Rows[i][3].ToString());
                                    drBankAccount["SELECT"] = 1;
                                    dtCheckedBudget.Rows.Add(drBankAccount);
                                    gcBank.DataSource = dtCheckedBudget;
                                }
                            }
                        }

                        this.chkBank.CheckedChanged -= new System.EventHandler(this.chkBank_CheckedChanged);
                        chkBank.Checked = true;
                        this.chkBank.CheckedChanged += new System.EventHandler(this.chkBank_CheckedChanged);
                        if (dtCheckedBudget != null && dtCheckedBudget.Rows.Count != 0)
                        {
                            var UniqueRows = dtCheckedBudget.AsEnumerable().Distinct(DataRowComparer.Default);
                            dtCheckedBudget = UniqueRows.CopyToDataTable();
                        }
                        gcBank.DataSource = dtCheckedBudget;
                        gcBank.RefreshDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }

        }

        private void gvTDSParties_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                if (e.Column.FieldName == "SELECT")
                {
                    DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryTextEdit = new RepositoryItemTextEdit();
                    e.RepositoryItem = repositoryTextEdit;
                }
            }
        }

        private void gvDeducteeType_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                if (e.Column.FieldName == "SELECT")
                {
                    DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryTextEdit = new RepositoryItemTextEdit();
                    e.RepositoryItem = repositoryTextEdit;
                }
            }
        }

        private void gvGroups_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                if (e.Column.FieldName == "SELECT")
                {
                    DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryTextEdit = new RepositoryItemTextEdit();
                    e.RepositoryItem = repositoryTextEdit;
                }
            }
        }

        private void gvPayComponent_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                if (e.Column.FieldName == "SELECT")
                {
                    DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryTextEdit = new RepositoryItemTextEdit();
                    e.RepositoryItem = repositoryTextEdit;
                }
            }
        }

        private void gvPayrollStaff_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                if (e.Column.FieldName == "SELECT")
                {
                    DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryTextEdit = new RepositoryItemTextEdit();
                    e.RepositoryItem = repositoryTextEdit;
                }
            }
        }

        private void chkPartyLedger_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTDSPartyLedger = gcTDSParties.DataSource as DataTable;
                if (dtTDSPartyLedger != null && dtTDSPartyLedger.Rows.Count > 0)
                {
                    if (!dtTDSPartyLedger.Columns.Contains("SELECT"))
                    {
                        dtTDSPartyLedger.Columns.Add(SELECT, typeof(Int32));
                    }
                    foreach (DataRow dr in dtTDSPartyLedger.Rows)
                    {
                        dr[SELECT] = chkPartyLedger.Checked;
                    }
                    gcTDSParties.DataSource = dtTDSPartyLedger;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void chkNatureofPayments_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTDSNarration = gcNarration.DataSource as DataTable;
                if (dtTDSNarration != null && dtTDSNarration.Rows.Count > 0)
                {
                    if (!dtTDSNarration.Columns.Contains("SELECT"))
                    {
                        dtTDSNarration.Columns.Add(SELECT, typeof(Int32));
                    }
                    foreach (DataRow dr in dtTDSNarration.Rows)
                    {
                        dr[SELECT] = chkNatureofPayments.Checked;
                    }
                    gcNarration.DataSource = dtTDSNarration;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void chkStockShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvItem.OptionsView.ShowAutoFilterRow = chkStockShowFilter.Checked;
        }

        private void chkItems_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtStockItem = gcItem.DataSource as DataTable;
                if (dtStockItem != null && dtStockItem.Rows.Count > 0)
                {
                    if (!dtStockItem.Columns.Contains(SELECT))
                    {
                        dtStockItem.Columns.Add(SELECT, typeof(Int32));
                    }
                    foreach (DataRow dr in dtStockItem.Rows)
                    {
                        dr[SELECT] = chkItems.Checked;
                    }
                    gcItem.DataSource = dtStockItem;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void chkLocationSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtLocation = gcLocation.DataSource as DataTable;
                if (dtLocation != null && dtLocation.Rows.Count > 0)
                {
                    if (!dtLocation.Columns.Contains(SELECT))
                    {
                        dtLocation.Columns.Add(SELECT, typeof(Int32));
                    }
                    foreach (DataRow dr in dtLocation.Rows)
                    {
                        dr[SELECT] = chkLocationSelectAll.Checked;
                    }
                    gcLocation.DataSource = dtLocation;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void chkLocationShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvLocation.OptionsView.ShowAutoFilterRow = chkLocationShowFilter.Checked;
        }

        private void chkPartyFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvTDSParties.OptionsView.ShowAutoFilterRow = chkPartyFilter.Checked;
            if (chkPartyFilter.Checked)
            {
                gvTDSParties.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                gvTDSParties.FocusedColumn = colPartyLedger;
                gvTDSParties.ShowEditor();
            }
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvDeducteeType.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                gvDeducteeType.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                gvDeducteeType.FocusedColumn = colDeductee;
                gvDeducteeType.ShowEditor();
            }
        }

        //Commanded By chinna  - allow to select Multi Fd Id

        //private void gvBank_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        //{
        //    if (ReportProperty.Current.ReportId == "RPT-094")
        //    {
        //        DataTable dtBank = (DataTable)gcBank.DataSource;
        //        if (dtBank != null && dtBank.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dtBank.Rows.Count; i++)
        //            {
        //                if (i != gvBank.FocusedRowHandle)
        //                {
        //                    dtBank.Rows[i]["SELECT"] = 0;
        //                }
        //                else
        //                {
        //                    dtBank.Rows[i]["SELECT"] = 1;
        //                }
        //            }
        //            gcBank.DataSource = dtBank;
        //        }
        //    }
        //}

        public void setmultibank(string ProId = "")
        {
            string Pid = string.Empty;
            glkBankColumn1.Properties.DataSource = glkbankColumn2.Properties.DataSource = null;
            using (DataManager dataManager = new DataManager(SQLCommand.Bank.FetchBankDetailsByProjectIds))
            {
                DataTable dtProjectId = (DataTable)gcProject.DataSource;
                if (dtProjectId != null && dtProjectId.Rows.Count != 0)
                {
                    var Selected = (from d in dtProjectId.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);

                    if (Selected.Count() != 0)
                    {
                        DataTable dtBank = Selected.CopyToDataTable();
                        foreach (DataRow dr in dtBank.Rows)
                        {
                            Pid += dr[this.appSchema.Project.PROJECT_IDColumn.ColumnName] + ",";
                        }
                    }
                }
                Pid = (!string.IsNullOrEmpty(ProId)) ? ProId : Pid.TrimEnd(',');
                dataManager.Parameters.Add(this.appSchema.Project.PROJECT_IDColumn, string.IsNullOrEmpty(Pid) ? "0" : Pid);
                dataManager.Parameters.Add(this.appSchema.BankAccount.DATE_CLOSEDColumn, DateFrom.Text);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                {
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        dtMultiColumnBankSource = resultArgs.DataSource.Table;
                        using (CommonMethod SelectAll = new CommonMethod())
                        {
                            DataTable dtBankList = SelectAll.AddEmptyHeaderColumn(resultArgs.DataSource.Table, "LEDGER_ID", "BANK", "Not Applicable");
                            ReportProperty.Current.ComboSet.BindGridLookUpCombo(glkBankColumn1, resultArgs.DataSource.Table, "BANK", "LEDGER_ID"); //"BANKNAME"
                            ReportProperty.Current.ComboSet.BindGridLookUpCombo(glkbankColumn2, resultArgs.DataSource.Table, "BANK", "LEDGER_ID"); //"BANKNAME"

                            glkBankColumn1.EditValue = (ReportProperty.Current.MultiColumn1LedgerId == 0) ?
                                glkBankColumn1.Properties.GetKeyValue(0) : ReportProperty.Current.MultiColumn1LedgerId;
                            glkbankColumn2.EditValue = (ReportProperty.Current.MultiColumn2LedgerId == 0) ?
                                glkbankColumn2.Properties.GetKeyValue(0) : ReportProperty.Current.MultiColumn2LedgerId;

                            Column1FilterData();
                            Column2FilterData();
                        }
                    }
                }
            }
        }

        private void glkBankColumn1_EditValueChanged(object sender, EventArgs e)
        {
            Column1FilterData();
        }

        private void Column1FilterData()
        {
            DataTable dtbankDetails = new DataTable();
            if (glkBankColumn1.EditValue != null && dtMultiColumnBankSource != null)
            {
                string bank1Ledid = glkBankColumn1.EditValue.ToString();
                if (!bank1Ledid.Equals("0"))
                {
                    DataView dvtemp = dtMultiColumnBankSource.AsDataView();
                    if (dvtemp != null && dvtemp.ToTable().Rows.Count > 0)
                    {
                        dvtemp.RowFilter = "LEDGER_ID NOT IN (" + bank1Ledid + ")";
                        dtbankDetails = dvtemp.ToTable();
                    }
                    else
                    {
                        glkbankColumn2.EditValue = glkbankColumn2.Properties.GetKeyValue(0);
                    }
                }
                else
                {
                    dtbankDetails = dtMultiColumnBankSource;
                }
                if (dtbankDetails != null && dtbankDetails.Rows.Count > 0)
                {
                    using (CommonMethod SelectAll = new CommonMethod())
                    {
                        ReportProperty.Current.ComboSet.BindGridLookUpCombo(glkbankColumn2, dtbankDetails, "BANK", "LEDGER_ID"); //"BANKNAME"
                    }
                }

            }
        }

        private void glkbankColumn2_EditValueChanged(object sender, EventArgs e)
        {
            Column2FilterData();
        }

        private void Column2FilterData()
        {
            DataTable dtbankDetails = new DataTable();
            string bank1Ledid = string.Empty;
            if (glkBankColumn1.EditValue != null && dtMultiColumnBankSource != null)
            {
                bank1Ledid = glkbankColumn2.EditValue.ToString();
                if (!bank1Ledid.Equals("0"))
                {
                    DataView dvtemp = dtMultiColumnBankSource.AsDataView();
                    if (dvtemp != null && dvtemp.ToTable().Rows.Count > 0)
                    {
                        dvtemp.RowFilter = "LEDGER_ID NOT IN (" + bank1Ledid + ")";
                        dtbankDetails = dvtemp.ToTable();
                    }
                    else
                    {
                        glkBankColumn1.EditValue = glkBankColumn1.Properties.GetKeyValue(0);
                    }
                }
                else
                {
                    dtbankDetails = dtMultiColumnBankSource;
                }
                if (dtbankDetails != null && dtbankDetails.Rows.Count > 0)
                {
                    using (CommonMethod SelectAll = new CommonMethod())
                    {
                        ReportProperty.Current.ComboSet.BindGridLookUpCombo(glkBankColumn1, dtbankDetails, "BANK", "LEDGER_ID"); //"BANKNAME"
                    }
                }

            }
        }

        #region BRS Bank and FD Account Details
        /// <summary>
        /// Set Bank FD Account Details
        /// </summary>
        private void SetBankFDAccountSource()
        {
            try
            {
                gcBankFDAccounts.DataSource = null;
                string ReportCriteria = ReportProperty.Current.ReportCriteria;
                string[] aReportCriteria = ReportCriteria.Split('ÿ');

                using (DataManager dataManager = new DataManager(SQLCommand.Bank.FetchBankFDAccountDetailsByProjectId))
                {
                    string Pid = string.IsNullOrEmpty(SelectedProject()) ? "0" : SelectedProject();
                    if (Pid != "0")
                    {
                        dataManager.Parameters.Add(this.appSchema.Project.PROJECT_IDColumn, Pid);
                    }
                    dataManager.Parameters.Add(this.appSchema.BankAccount.DATE_CLOSEDColumn, DateFrom.Text);

                    //On 29/09/2023, This property is used to skip bank ledger project based
                    if (Array.IndexOf(aReportCriteria, "DA") >= 0)
                    {
                        dataManager.Parameters.Add(this.appSchema.Ledger.APPLICABLE_FROMColumn, settinguserProperty.FirstFYDateFrom);
                        dataManager.Parameters.Add(this.appSchema.Ledger.APPLICABLE_TOColumn, DateFrom.Text);
                    }
                    else
                    {
                        dataManager.Parameters.Add(this.appSchema.Ledger.APPLICABLE_FROMColumn, DateFrom.Text);
                        dataManager.Parameters.Add(this.appSchema.Ledger.APPLICABLE_TOColumn, DateTo.Text);
                    }

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                    {
                        if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                        {
                            resultArgs.DataSource.Table.Columns.Add(SELECT, typeof(Int32));
                            BankSelected = resultArgs.DataSource.Table;
                            gcBankFDAccounts.DataSource = resultArgs.DataSource.Table;
                            gcBankFDAccounts.RefreshDataSource();
                            gvBankFDAccounts.ExpandAllGroups();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }
        /// <summary>
        /// Select the Bank FD Account Details
        /// </summary>
        private void SelectedBankFDAccountDetails()
        {
            string BankIDs = string.Empty;
            string FDAccountIDs = string.Empty;
            string SelectedBankFD = string.Empty;
            int SelectedCount = 0;
            try
            {
                DataTable dtBankFD = gcBankFDAccounts.DataSource as DataTable;
                if (dtBankFD != null && dtBankFD.Rows.Count != 0)
                {
                    int tmpfd = 0;
                    int[] SelectedIds = gvBankFDAccounts.GetSelectedRows();
                    int[] sCheckedAccount = new int[SelectedIds.Count()];
                    if (SelectedIds.Count() > 0)
                    {
                        foreach (int RowIndex in SelectedIds)
                        {
                            if (RowIndex >= 0)
                            {
                                DataRow drProject = gvBankFDAccounts.GetDataRow(RowIndex);
                                if (drProject != null)
                                {
                                    BankIDs += drProject[this.appSchema.Ledger.LEDGER_IDColumn.ColumnName] + ",";
                                    SelectedBankFD += drProject[this.appSchema.Bank.BANKColumn.ColumnName] + ",";
                                    SelectedCount++;
                                    tmpfd = ReportProperty.Current.NumberSet.ToInteger(drProject[this.appSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName].ToString());
                                    if (tmpfd > 0)
                                    {
                                        FDAccountIDs += tmpfd + ",";
                                    }
                                }
                            }
                        }
                    }
                    BankIDs = BankIDs.TrimEnd(',');
                    FDAccountIDs = FDAccountIDs.TrimEnd(',');
                    SelectedBankFD = SelectedBankFD.TrimEnd(',');
                    //05/12/2019, to keep Cash Bank LedgerId
                    //ReportProperty.Current.Ledger = !string.IsNullOrEmpty(BankIDs) ? BankIDs : "0";
                    ReportProperty.Current.CashBankLedger = !string.IsNullOrEmpty(BankIDs) ? BankIDs : "0";
                    ReportProperty.Current.FDAccountID = !string.IsNullOrEmpty(FDAccountIDs) ? FDAccountIDs : "0";

                    if (dtBankFD.Rows.Count == SelectedCount)
                    {
                        ReportProperty.Current.SelectedBankFD = "  " + SCONSTATEMENT;
                    }
                    else
                    {
                        ReportProperty.Current.SelectedBankFD = !string.IsNullOrEmpty(SelectedBankFD) ? "  " + SelectedBankFD : string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        /// <summary>
        /// Assign the Bank FD Account Details
        /// </summary>
        private void AssignBankFDAccountDetails()
        {
            string BankIDs = string.Empty;
            string FDAccountIDs = string.Empty;
            string tmpBId = string.Empty;
            try
            {
                SetBankFDAccountSource();
                DataTable dtBankFD = gcBankFDAccounts.DataSource as DataTable;
                if (dtBankFD != null && dtBankFD.Rows.Count != 0)
                {
                    //05/12/2019, to keep Cash Bank LedgerId
                    //BankIDs = ReportProperty.Current.Ledger;
                    BankIDs = string.IsNullOrEmpty(ReportProperty.Current.CashBankLedger) ? string.Empty : ReportProperty.Current.CashBankLedger;
                    FDAccountIDs = ReportProperty.Current.FDAccountID;
                    string[] IdCollection = BankIDs.Split(',');
                    string[] FDIdCollection = FDAccountIDs.Split(',');

                    if (!string.IsNullOrEmpty(BankIDs) && BankIDs != "0")
                    {
                        gvBankFDAccounts.ClearSelection();

                        foreach (DataRow dr in dtBankFD.Rows)
                        {
                            foreach (string tlid in IdCollection)
                            {
                                if (dr["LEDGER_ID"].ToString().Equals(tlid) && dr["GROUP_ID"].ToString() == "12")
                                {
                                    int Index = dtBankFD.Rows.IndexOf(dr);
                                    gvBankFDAccounts.SelectRow(Index);

                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(FDAccountIDs) && FDAccountIDs != "0")
                    {
                        foreach (DataRow dr in dtBankFD.Rows)
                        {
                            foreach (string tlid in FDIdCollection)
                            {
                                if (dr["FD_ACCOUNT_ID"].ToString().Equals(tlid) && dr["GROUP_ID"].ToString() == "14")
                                {
                                    int Index = dtBankFD.Rows.IndexOf(dr);
                                    gvBankFDAccounts.SelectRow(Index);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        #endregion

        private void chkbankFDFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvBankFDAccounts.OptionsView.ShowAutoFilterRow = chkbankFDFilter.Checked;
            if (chkbankFDFilter.Checked)
            {
                gvBankFDAccounts.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                gvBankFDAccounts.FocusedColumn = gcolaccBankName;
                gvBankFDAccounts.ShowEditor();
            }
        }

        private void chkselectAllAssetclass_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string Assetclassid = string.Empty;
                DataTable dtAllAssetclass = gcAssetClass.DataSource as DataTable;
                if (dtAllAssetclass != null && dtAllAssetclass.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtAllAssetclass.Rows)
                    {
                        Assetclassid += dr[this.appSchema.ASSETClassDetails.ASSET_CLASS_IDColumn.ColumnName] + ",";
                        dr[SELECT] = chkselectAllAssetclass.Checked;
                    }
                    Assetclassid = Assetclassid.TrimEnd(',');

                    string[] reportCriteria = ReportProperty.Current.ReportCriteria.Split('ÿ');

                    gcAssetClass.DataSource = null;
                    gcAssetClass.DataSource = dtAllAssetclass;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void chkAssetShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvAssetClass.OptionsView.ShowAutoFilterRow = chkAssetShowFilter.Checked;
            if (chkProjectFilter.Checked)
            {
                gvAssetClass.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                gvAssetClass.FocusedColumn = colAssetClass;
                gvAssetClass.ShowEditor();
            }
        }

        private void chkRegistrationType_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtRegistrationType = gcRegistrationType.DataSource as DataTable;
                if (dtRegistrationType != null && dtRegistrationType.Rows.Count > 0)
                {
                    if (!dtRegistrationType.Columns.Contains(SELECT))
                    {
                        dtRegistrationType.Columns.Add(SELECT, typeof(Int32));
                    }
                    foreach (DataRow dr in dtRegistrationType.Rows)
                    {
                        dr[SELECT] = chkRegistrationType.Checked;
                    }
                    gcRegistrationType.DataSource = dtRegistrationType;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void chkCountry_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtCountry = gcCountry.DataSource as DataTable;
                if (dtCountry != null && dtCountry.Rows.Count > 0)
                {
                    if (!dtCountry.Columns.Contains(SELECT))
                    {
                        dtCountry.Columns.Add(SELECT, typeof(Int32));
                    }
                    foreach (DataRow dr in dtCountry.Rows)
                    {
                        dr[SELECT] = chkCountry.Checked;
                    }
                    gcCountry.DataSource = dtCountry;
                }
                SetStateSource();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void chkState_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtState = gcState.DataSource as DataTable;
                if (dtState != null && dtState.Rows.Count > 0)
                {
                    if (!dtState.Columns.Contains(SELECT))
                    {
                        dtState.Columns.Add(SELECT, typeof(Int32));
                    }
                    foreach (DataRow dr in dtState.Rows)
                    {
                        dr[SELECT] = chkState.Checked;
                    }
                    gcState.DataSource = dtState;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void chkShowRegistrationFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvRegistrationType.OptionsView.ShowAutoFilterRow = chkShowRegistrationFilter.Checked;
            if (chkShowRegistrationFilter.Checked)
            {
                gvRegistrationType.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                gvRegistrationType.FocusedColumn = colRegistrationType;
                gvRegistrationType.ShowEditor();
            }
        }

        private void chkShowCountryFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvCountry.OptionsView.ShowAutoFilterRow = chkShowCountryFilter.Checked;
            if (chkShowCountryFilter.Checked)
            {
                gvCountry.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                gvCountry.FocusedColumn = colCountry;
                gvCountry.ShowEditor();
            }
        }

        private void chkShowStateFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvState.OptionsView.ShowAutoFilterRow = chkShowStateFilter.Checked;
            if (chkShowStateFilter.Checked)
            {
                gvState.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                gvState.FocusedColumn = colState;
                gvState.ShowEditor();
            }
        }

        private void rchkCountrySelect_CheckedChanged(object sender, EventArgs e)
        {
            gvState.Focus();
        }

        private void gvCountry_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string CountryID = string.Empty;
            gvCountry.UpdateCurrentRow();
            gvCountry.CloseEditForm();
            gvCountry.CloseEditor();

            DataTable dtCountry = gcCountry.DataSource as DataTable;
            if (dtCountry != null && dtCountry.Rows.Count != 0)
            {
                var Selected = (from d in dtCountry.AsEnumerable()
                                where ((d.Field<Int32?>(SELECT) == 1))
                                select d);

                if (Selected.Count() != 0)
                {
                    DataTable dtCountryState = Selected.CopyToDataTable();
                    foreach (DataRow dr in dtCountryState.Rows)
                    {
                        CountryID += dr[this.appSchema.Country.COUNTRY_IDColumn.ColumnName] + ",";
                    }
                    CountryID = CountryID.TrimEnd(',');
                }
            }
            if (!string.IsNullOrEmpty(CountryID))
            {
                dtState = FetchStateByCountry(CountryID);
                gcState.DataSource = dtState;
                gcState.RefreshDataSource();
            }
            else
            {
                SetStateSource();
            }


            gvState.Focus();
        }

        private void gvCountry_Click(object sender, EventArgs e)
        {
            string cntryId = string.Empty;
            try
            {
                DataTable dtGroupId = gcCountry.DataSource as DataTable;
                if (dtGroupId != null && dtGroupId.Rows.Count != 0)
                {
                    foreach (int i in gvCountry.GetSelectedRows())
                    {
                        DataRow dr = gvCountry.GetDataRow(i);
                        cntryId += dr[this.appSchema.Country.COUNTRY_IDColumn.ColumnName] + ",";
                    }
                    cntryId = cntryId.TrimEnd(',');
                    DataView dvGroup = null;
                    if (!string.IsNullOrEmpty(cntryId))
                    {
                        dtState = FetchStateByCountry(cntryId);
                        gcState.DataSource = dtState;
                        gcState.RefreshDataSource();
                    }
                    else
                    {
                        SetStateSource();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void chkLanguage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtLanguage = gcLanguage.DataSource as DataTable;
                if (dtLanguage != null && dtLanguage.Rows.Count > 0)
                {
                    if (!dtLanguage.Columns.Contains(SELECT))
                    {
                        dtLanguage.Columns.Add(SELECT, typeof(Int32));
                    }
                    foreach (DataRow dr in dtLanguage.Rows)
                    {
                        dr[SELECT] = chkLanguage.Checked;
                    }
                    gcLanguage.DataSource = dtLanguage;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void chkShowLanguageFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvLanguage.OptionsView.ShowAutoFilterRow = chkShowLanguageFilter.Checked;
            if (chkShowLanguageFilter.Checked)
            {
                gvLanguage.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                gvLanguage.FocusedColumn = colLanguage;
                gvLanguage.ShowEditor();
            }
        }

        private void chkDonaud_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtDonaud = gcDonaud.DataSource as DataTable;
                if (dtDonaud != null && dtDonaud.Rows.Count > 0)
                {
                    if (!dtDonaud.Columns.Contains(SELECT))
                    {
                        dtDonaud.Columns.Add(SELECT, typeof(Int32));
                    }
                    foreach (DataRow dr in dtDonaud.Rows)
                    {
                        dr[SELECT] = chkDonaud.Checked;
                    }
                    gcDonaud.DataSource = dtDonaud;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void chkShowState_CheckedChanged(object sender, EventArgs e)
        {
            gvStateDonaud.OptionsView.ShowAutoFilterRow = chkShowState.Checked;
            if (chkShowState.Checked)
            {
                gvStateDonaud.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                gvStateDonaud.FocusedColumn = colStateDonaudName;
                gvStateDonaud.ShowEditor();
            }
        }

        private void chkShowDonaud_CheckedChanged(object sender, EventArgs e)
        {
            gvDonaud.OptionsView.ShowAutoFilterRow = chkShowDonaud.Checked;
            if (chkShowDonaud.Checked)
            {
                gvDonaud.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                gvDonaud.FocusedColumn = colDonaud;
                gvDonaud.ShowEditor();
            }
        }

        private void gvStateDonaud_Click(object sender, EventArgs e)
        {
            string StateID = string.Empty;
            gvStateDonaud.UpdateCurrentRow();
            gvStateDonaud.CloseEditForm();
            gvStateDonaud.CloseEditor();
            try
            {
                DataTable dtStateid = gcStateDonaud.DataSource as DataTable;
                if (dtStateid != null && dtStateid.Rows.Count != 0)
                {
                    foreach (int i in gvStateDonaud.GetSelectedRows())
                    {
                        DataRow dr = gvStateDonaud.GetDataRow(i);
                        StateID += dr[this.appSchema.State.STATE_IDColumn.ColumnName] + ",";
                    }
                    StateID = StateID.TrimEnd(',');
                    DataView dvGroup = null;
                    if (!string.IsNullOrEmpty(StateID))
                    {
                        dtState = FetchDonorByStateID(StateID);
                        gcDonaud.DataSource = dtState;
                        gcDonaud.RefreshDataSource();
                    }
                    else
                    {
                        SetDonaudSource();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void gvStateDonaud_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string StateID = string.Empty;
            gvStateDonaud.UpdateCurrentRow();
            gvStateDonaud.CloseEditForm();
            gvStateDonaud.CloseEditor();
            try
            {
                DataTable dtStateid = gcStateDonaud.DataSource as DataTable;
                if (dtStateid != null && dtStateid.Rows.Count != 0)
                {
                    var Selected = (from d in dtStateid.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);

                    if (Selected.Count() != 0)
                    {
                        DataTable dtGroup = Selected.CopyToDataTable();
                        foreach (DataRow dr in dtGroup.Rows)
                        {
                            StateID += dr[this.appSchema.State.STATE_IDColumn.ColumnName] + ",";
                        }
                        StateID = StateID.TrimEnd(',');
                        if (!string.IsNullOrEmpty(StateID))
                        {
                            dtState = FetchDonorByStateID(StateID);
                            gcDonaud.DataSource = null;
                            gcDonaud.DataSource = dtState;
                            gcDonaud.RefreshDataSource();
                        }
                    }


                    //foreach (int i in gvStateDonaud.GetSelectedRows())
                    //{
                    //    DataRow dr = gvStateDonaud.GetDataRow(i);
                    //    StateID += dr[this.appSchema.State.STATE_IDColumn.ColumnName] + ",";
                    //}
                    //StateID = StateID.TrimEnd(',');
                    //DataView dvGroup = null;
                    //if (!string.IsNullOrEmpty(StateID))
                    //{
                    //    dtState = FetchDonorByStateID(StateID);
                    //    gcDonaud.DataSource = dtState;
                    //    gcDonaud.RefreshDataSource();
                    //}
                    else
                    {
                        SetDonaudSource();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }



        private void rchStateCheck_Click(object sender, EventArgs e)
        {
            gvDonaud.Focus();
        }

        private void chkStateDonaud_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtStateDonaud = gcStateDonaud.DataSource as DataTable;
                if (dtStateDonaud != null && dtStateDonaud.Rows.Count > 0)
                {
                    if (!dtStateDonaud.Columns.Contains(SELECT))
                    {
                        dtStateDonaud.Columns.Add(SELECT, typeof(Int32));
                    }
                    foreach (DataRow dr in dtStateDonaud.Rows)
                    {
                        dr[SELECT] = chkStateDonaud.Checked;
                    }
                    gcStateDonaud.DataSource = dtStateDonaud;
                }
                SetDonaudSource();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void gvReportCriteria_CalcRowHeight(object sender, RowHeightEventArgs e)
        {
            //Modified by alwar on 06/02/2016
            //This is used to set row height for report general or dynamic criteria
            //If critea is radio check box, incrase row height to have better look and feel
            //default row height is 20
            if (e.RowHandle >= 0)
            {
                int rowheight = 20;
                GridView view = sender as GridView;
                GridEditorItem activerow = view.GetRow(e.RowHandle) as GridEditorItem;
                if (activerow.RepositoryItem.GetType() == typeof(RepositoryItemRadioGroup)) //For radio list 
                {
                    rowheight = 30;
                    activerow.RepositoryItem.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
                }
                e.RowHeight = rowheight;
            }
        }



        private void gvReportCriteria_RowCellDefaultAlignment(object sender, DevExpress.XtraGrid.Views.Base.RowCellAlignmentEventArgs e)
        {
            if (e.Column == colCriteriaType && e.RowHandle >= 0)
            {
                //colCriteriaType.AppearanceCell.Options.UseTextOptions = true;
                //colCriteriaType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                //e.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;

                //colCriteriaType.AppearanceCell.Options.g
            }
        }

        private void chkSelectAllTask_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTasks = gcTasks.DataSource as DataTable;
                if (dtTasks != null && dtTasks.Rows.Count > 0)
                {
                    if (!dtTasks.Columns.Contains(SELECT))
                    {
                        dtTasks.Columns.Add(SELECT, typeof(Int32));
                    }
                    foreach (DataRow dr in dtTasks.Rows)
                    {
                        dr[SELECT] = chkSelectAllTask.Checked;
                    }
                    gcTasks.DataSource = dtTasks;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void rchkSelectTask_CheckedChanged(object sender, EventArgs e)
        {
            this.chkSelectAllTask.CheckedChanged -= new System.EventHandler(this.chkSelectAllTask_CheckedChanged);
            chkSelectAllTask.Checked = false;
            this.chkSelectAllTask.CheckedChanged += new System.EventHandler(this.chkSelectAllTask_CheckedChanged);
        }

        private void chkTaskFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvTasks.OptionsView.ShowAutoFilterRow = chkTaskFilter.Checked;
            if (chkTaskFilter.Checked)
            {
                gvTasks.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                gvTasks.FocusedColumn = colTaskName;
                gvTasks.ShowEditor();
            }
        }

        public int MonthDiff(DateTime date1, DateTime date2)
        {
            if (date1.Month < date2.Month)
            {
                return (date2.Year - date1.Year) * 12 + date2.Month - date1.Month;
            }
            else
            {
                return (date2.Year - date1.Year - 1) * 12 + date2.Month - date1.Month + 12;
            }
        }

        /// <summary>
        /// On 18/09/2018, for few reports date range must be curent financeyear and lock date range
        /// </summary>
        private void LockDateRangeWithFinanceYear()
        {
            if (ReportProperty.Current.ReportId == "RPT-155" || ReportProperty.Current.ReportId == "RPT-156" || ReportProperty.Current.ReportId == "RPT-187" || //For Multi Abstract Year based
                ReportProperty.Current.ReportId == "RPT-182" || ReportProperty.Current.ReportId == "RPT-200" || ReportProperty.Current.ReportId == "RPT-201" || ReportProperty.Current.ReportId == "RPT-191")
            {
                DateFrom.Text = ReportProperty.Current.DateSet.ToDate(this.settinguserProperty.YearFrom);
                DateTo.Text = ReportProperty.Current.DateSet.ToDate(this.settinguserProperty.YearTo);
                DateFrom.Enabled = DateTo.Enabled = false;
            }
            else if ((ReportProperty.Current.ReportId == "RPT-174" || ReportProperty.Current.ReportId == "RPT-175" || ReportProperty.Current.ReportId == "RPT-176") && //For Multi Abstract quarterly based
                    (string.IsNullOrEmpty(ReportProperty.Current.DateFrom) || string.IsNullOrEmpty(ReportProperty.Current.DateTo)))
            {
                List<Tuple<DateTime, DateTime>> quarterDates = ReportProperty.Current.DateSet.GetQuarterDates(ReportProperty.Current.DateSet.ToDate(this.settinguserProperty.YearFrom, false),
                        ReportProperty.Current.DateSet.ToDate(this.settinguserProperty.YearTo, false));
                DateFrom.DateTime = ReportProperty.Current.DateSet.ToDate(quarterDates[0].Item1.ToShortDateString(), false);
                DateTo.DateTime = ReportProperty.Current.DateSet.ToDate(quarterDates[0].Item2.ToShortDateString(), false);
            }
            else if (ReportProperty.Current.ReportId == "RPT-184")
            {
                DateFrom.Text = ReportProperty.Current.DateSet.ToDate(this.settinguserProperty.YearFrom);
                DateTo.Text = ReportProperty.Current.DateSet.ToDate(this.settinguserProperty.YearTo);
                DateFrom.Enabled = DateTo.Enabled = false;
            }
            //else if (ReportProperty.Current.ReportId == "RPT-186")
            //{
            //    DateFrom.Text = ReportProperty.Current.DateSet.ToDate(this.settingProperty.YearFrom);
            //    if (ReportProperty.Current.DateSet.ToDate(DateTo.Text, false) > ReportProperty.Current.DateSet.ToDate(this.settingProperty.YearTo, false))
            //    {
            //        DateTo.Text = ReportProperty.Current.DateSet.ToDate(this.settingProperty.YearTo);
            //    }
            //    DateFrom.Enabled = false;
            //}
            else if (ReportProperty.Current.ReportId == "RPT-185" || ReportProperty.Current.ReportId == "RPT-186")
            {
                if (!(ReportProperty.Current.DateSet.ToDate(DateFrom.Text, false) >= ReportProperty.Current.DateSet.ToDate(settinguserProperty.YearFrom, false) &&
                    ReportProperty.Current.DateSet.ToDate(DateFrom.Text, false) <= ReportProperty.Current.DateSet.ToDate(settinguserProperty.YearTo, false)))
                {
                    DateFrom.Text = ReportProperty.Current.DateSet.ToDate(this.settinguserProperty.YearFrom);
                }

                if (!(ReportProperty.Current.DateSet.ToDate(DateTo.Text, false) >= ReportProperty.Current.DateSet.ToDate(settinguserProperty.YearFrom, false) &&
                        ReportProperty.Current.DateSet.ToDate(DateTo.Text, false) <= ReportProperty.Current.DateSet.ToDate(settinguserProperty.YearTo, false)))
                {
                    DateTo.Text = ReportProperty.Current.DateSet.ToDate(this.settinguserProperty.YearTo);
                }
            }
        }

        private void chkBudgetFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvBudget.OptionsView.ShowAutoFilterRow = chkBudgetFilter.Checked;
            if (chkBudgetFilter.Checked)
            {
                gvBudget.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                gvBudget.FocusedColumn = colBudgetName;
                gvBudget.ShowEditor();
            }
        }

        private void xtbLocation_Click(object sender, EventArgs e)
        {
            if (xtbLocation.SelectedTabPage == xtbSign)
            {
                txtRoleName1.Focus();
                chkBudget.Visible = false;
            }
        }

        private void chkBudget_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtBudget = gcBudget.DataSource as DataTable;
                if (dtBudget != null && dtBudget.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtBudget.Rows)
                    {
                        dr[SELECT] = chkBudget.Checked;
                    }
                    gcBudget.DataSource = dtBudget;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        /// <summary>
        /// This method is used to load list of voucher types
        /// </summary>
        private void LoadVoucherType()
        {
            this.repositoryItemComboBox.Name = "VT";
            this.repositoryItemComboBox.Items.Clear();
            this.repositoryItemComboBox.Items.Add("<-- All -->");


            if (ReportProperty.Current.ReportId == "RPT-208")
            {
                this.repositoryItemComboBox.Items.Add("Receipts");
                this.repositoryItemComboBox.Items.Add("Payments");
            }
            else if (ReportProperty.Current.ReportId == "RPT-012")
            {
                this.repositoryItemComboBox.Items.Add("Receipts");
                this.repositoryItemComboBox.Items.Add("Payments");
                this.repositoryItemComboBox.Items.Add("Journal");
            }
            else
            {
                this.repositoryItemComboBox.Items.Add("Receipts");
                this.repositoryItemComboBox.Items.Add("Payments");
                this.repositoryItemComboBox.Items.Add("Contra");
                this.repositoryItemComboBox.Items.Add("Journal");
            }

            using (DataManager dataManager = new DataManager(SQLCommand.Voucher.FetchAll))
            {
                ResultArgs result = dataManager.FetchData(DataSource.DataTable);
                if (result.Success && result.DataSource.Table != null)
                {
                    DataTable dtVouhterTypes = result.DataSource.Table;

                    //Get voucher types except Receipts/Payments/Contra/Journal
                    dtVouhterTypes.DefaultView.RowFilter = "VOUCHER_ID > 4";

                    dtVouhterTypes = dtVouhterTypes.DefaultView.ToTable();
                    foreach (DataRow drVoucherType in dtVouhterTypes.Rows)
                    {
                        this.repositoryItemComboBox.Items.Add(drVoucherType[this.appSchema.Voucher.VOUCHER_NAMEColumn.ColumnName].ToString());
                    }
                }
            }
        }

        /// <summary>
        /// on 16/09/2021, This method is used to load list user list
        /// </summary>
        private void LoadUsers(RepositoryItemComboBox ritmcb)
        {
            ritmcb.Items.Clear();
            ritmcb.Items.Add("<-- All -->");

            using (DataManager dataManager = new DataManager(SQLCommand.User.FetchVoucherUsers))
            {
                ResultArgs result = dataManager.FetchData(DataSource.DataTable);
                if (result.Success && result.DataSource.Table != null)
                {
                    DataTable dtUsers = result.DataSource.Table;

                    foreach (DataRow druser in dtUsers.Rows)
                    {
                        if (!String.IsNullOrEmpty(druser[this.appSchema.User.FIRSTNAMEColumn.ColumnName].ToString()))
                        {
                            ritmcb.Items.Add(druser[this.appSchema.User.FIRSTNAMEColumn.ColumnName].ToString());
                        }
                    }
                }
            }
        }

        /// <summary>
        /// on 03/06/2024, This method is used to load list fd investment type
        /// </summary>
        private void LoadInvestmentType(RepositoryItemComboBox ritmcb)
        {
            ritmcb.Items.Clear();
            ritmcb.Items.Add("<-- All Investments-->");

            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchInvestmentType))
            {
                ResultArgs result = dataManager.FetchData(DataSource.DataTable);
                if (result.Success && result.DataSource.Table != null)
                {
                    DataTable dtInvestmentType = result.DataSource.Table;

                    foreach (DataRow druser in dtInvestmentType.Rows)
                    {
                        if (!String.IsNullOrEmpty(druser[this.appSchema.FDInvestmentType.INVESTMENT_TYPEColumn.ColumnName].ToString()))
                        {
                            ritmcb.Items.Add(druser[this.appSchema.FDInvestmentType.INVESTMENT_TYPEColumn.ColumnName].ToString());

                            if (ReportProperty.Current.FDInvestmentType ==
                                ReportProperty.Current.NumberSet.ToInteger(druser[this.appSchema.FDInvestmentType.INVESTMENT_TYPE_IDColumn.ColumnName].ToString()))
                            {
                                repcbFDInvestmentType = druser[this.appSchema.FDInvestmentType.INVESTMENT_TYPEColumn.ColumnName].ToString();
                                repcbFDInvestmentTypeId = ReportProperty.Current.FDInvestmentType;
                            }
                        }
                    }

                }
            }
        }

        /// <summary>
        /// on 22/10/2024, This method is used to load currencies
        /// </summary>
        private void LoadCurrencies(RepositoryItemGridLookUpEdit ritglkp)
        {
            bool DontddAllIteam = (ReportProperty.Current.ReportId == "RPT-015" || ReportProperty.Current.ReportId == "RPT-047" || ReportProperty.Current.ReportId == "RPT-150"
                    || ReportProperty.Current.ReportId == "RPT-204");

            using (DataManager dataManager = new DataManager(SQLCommand.Country.FetchAll))
            {
                dataManager.Parameters.Add(this.appSchema.VoucherMaster.VOUCHER_DATEColumn, settinguserProperty.YearFrom);
                resultArgs = dataManager.FetchData(DataSource.DataTable);

                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    DataTable dtCurrencies = resultArgs.DataSource.Table;

                    dtCurrencies.DefaultView.RowFilter = this.appSchema.VoucherMaster.EXCHANGE_RATEColumn.ColumnName + " > 0";
                    dtCurrencies = dtCurrencies.DefaultView.ToTable();

                    //26/11/2024 - For Consolidated Reports - Include <All>
                    if (!DontddAllIteam)
                    {
                        DataRow dr = dtCurrencies.NewRow();
                        dr[this.appSchema.Country.COUNTRY_IDColumn.ColumnName] = 0;
                        dr[this.appSchema.Country.COUNTRYColumn.ColumnName] = "<-- All -->";
                        dr[this.appSchema.Country.CURRENCY_NAMEColumn.ColumnName] = "<-- All -->";
                        dr[this.appSchema.Country.CURRENCY_SYMBOLColumn.ColumnName] = settinguserProperty.Currency;
                        dtCurrencies.Rows.InsertAt(dr, 0);
                    }

                    ritglkp.DataSource = dtCurrencies;
                    ritglkp.DisplayMember = this.appSchema.Country.CURRENCY_NAMEColumn.ColumnName;
                    ritglkp.ValueMember = this.appSchema.Country.COUNTRY_IDColumn.ColumnName;
                    ReportProperty.Current.ComboSet.BindRepositoryItemGridLookUpEdit(ritglkp, dtCurrencies,
                            this.appSchema.Country.CURRENCY_NAMEColumn.ToString(), this.appSchema.Country.COUNTRY_IDColumn.ToString());
                    ritglkp.View.CustomRowCellEdit += new CustomRowCellEditEventHandler(View_CustomRowCellEdit);
                    ritglkp.CloseUp += new CloseUpEventHandler(ritglkp_CloseUp);
                }
            }
        }

        /// <summary>
        /// On 10/12/2024, To load load currencies in report criteria tab page
        /// </summary>
        private void LoadReportSettingCurrencies()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Country.FetchAll))
            {
                dataManager.Parameters.Add(this.appSchema.VoucherMaster.VOUCHER_DATEColumn, settinguserProperty.YearFrom);
                resultArgs = dataManager.FetchData(DataSource.DataTable);

                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    DataTable dtCurrencies = resultArgs.DataSource.Table;

                    dtCurrencies.DefaultView.RowFilter = this.appSchema.VoucherMaster.EXCHANGE_RATEColumn.ColumnName + " > 0";
                    dtCurrencies = dtCurrencies.DefaultView.ToTable();
                    dtCurrencies.DefaultView.RowFilter = string.Empty;
                    ReportProperty.Current.ComboSet.BindGridLookUpComboEmptyItem(glkpReportSetupCurrencyCountry, dtCurrencies, this.appSchema.Country.CURRENCY_NAMEColumn.ColumnName,
                            this.appSchema.Country.COUNTRY_IDColumn.ColumnName, true, "-All-");
                    glkpReportSetupCurrencyCountry.EditValue = repcbCountryCurrencyId;
                }
            }
        }

        private void ritglkp_CloseUp(object sender, CloseUpEventArgs e)
        {
            if (sender != null)
            {
                GridLookUpEdit edit = sender as GridLookUpEdit;

                if (e.AcceptValue)
                {
                    this.repcbCountryCurrencyId = 0;
                    this.repcbCountryCurrency = string.Empty;
                    this.repcbCurrencySymbol = string.Empty;
                    bool loadcurrency = false;
                    //On 26/11/2024, To check currency is enabled ----------------------------------------------------
                    string[] aReportCriteria = ReportProperty.Current.ReportCriteria.Split('ÿ');
                    if (Array.IndexOf(aReportCriteria, "CR") >= 0)
                    {
                        loadcurrency = true;
                    }
                    //------------------------------------------------------------------------------------------------
                    if (loadcurrency || ReportProperty.Current.ReportId == "RPT-015" || ReportProperty.Current.ReportId == "RPT-047"
                        || ReportProperty.Current.ReportId == "RPT-150" || ReportProperty.Current.ReportId == "RPT-204")
                    {   //FD related reports
                        this.repcbCountryCurrencyId = (e.Value == null ? 0 : ReportProperty.Current.NumberSet.ToInteger(e.Value.ToString()));

                        if (edit.Text != null)
                        {
                            this.repcbCountryCurrency = (edit.Properties.GetDisplayValueByKeyValue(repcbCountryCurrencyId) == null ? ""
                                : edit.Properties.GetDisplayValueByKeyValue(repcbCountryCurrencyId).ToString());

                        }

                        if (this.repcbCountryCurrencyId >= 0)
                        {
                            if (edit.Properties.GetRowByKeyValue(this.repcbCountryCurrencyId) != null)
                            {
                                DataRow dr = edit.Properties.View.GetDataRow(edit.Properties.View.FocusedRowHandle) as DataRow;
                                this.repcbCurrencySymbol = dr[this.appSchema.Country.CURRENCY_SYMBOLColumn.ColumnName].ToString();
                            }
                        }
                    }
                }
            }
        }

        private void View_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (sender != null)
            {
                GridView view = sender as GridView;
                foreach (GridColumn gc in view.Columns)
                {
                    if (gc.FieldName == repositoryItemGridlookup1.DisplayMember)
                    {
                        gc.AppearanceHeader.Font = new Font(gc.AppearanceHeader.Font.FontFamily, gc.AppearanceHeader.Font.Size, FontStyle.Bold);
                        if (gc.FieldName == this.appSchema.Country.CURRENCY_NAMEColumn.ColumnName) gc.Caption = this.appSchema.Country.CURRENCYColumn.ColumnName;
                        gc.Visible = true;
                    }
                    else gc.Visible = false;
                }
            }
        }

        /// <summary>
        /// on 16/09/2021, This method is used to Auditon Action like Created/Modified/Deleted
        /// </summary>
        private void LoadAuditAction(RepositoryItemComboBox ritmcb)
        {
            ritmcb.Items.Clear();
            ritmcb.Items.Add("<-- All -->");
            ritmcb.Items.Add(AuditAction.Created.ToString());
            ritmcb.Items.Add(AuditAction.Modified.ToString());
            ritmcb.Items.Add(AuditAction.Deleted.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vouchertypename"></param>
        /// <returns></returns>
        private Int32 GetVoucherTypeId(string vouchertypename)
        {
            Int32 vouchertypeid = 0;
            using (DataManager dataManager = new DataManager(SQLCommand.Voucher.FetchByVoucherTypeName))
            {
                dataManager.Parameters.Add(this.appSchema.Voucher.VOUCHER_NAMEColumn, vouchertypename);
                ResultArgs result = dataManager.FetchData(DataSource.DataTable);
                if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                {
                    DataTable dtVouhterTypes = result.DataSource.Table;
                    vouchertypeid = ReportProperty.Current.NumberSet.ToInteger(dtVouhterTypes.Rows[0][this.appSchema.Voucher.VOUCHER_IDColumn.ColumnName].ToString());
                }
            }
            return vouchertypeid;
        }

        private string AlertBudgetProjects(string budgetprojects)
        {
            string Rtn = budgetprojects;
            bool mismatched = false;
            try
            {
                //1. Check Mismatched Budget projects
                string[] projectids = ReportProperty.Current.Project.Trim().Split(',');
                string checkprojectid = "," + budgetprojects + ",";
                for (int i = 0; i < projectids.Length; i++)
                {
                    string projectid = projectids[i].ToString();
                    if (checkprojectid.IndexOf("," + projectid + ",") < 0)
                    {
                        MessageRender.ShowMessage("Selected project(s) are mis-matched with Budget Project(s), Budget Project alone will be selected automatically.");
                        mismatched = true;
                        break;
                    }
                }

                //2. Alert partial budgets projects alone selected
                if (!mismatched)
                {
                    if (ReportProperty.Current.Project.Trim().Split(',').Length != budgetprojects.Split(',').Length)
                    {
                        if (XtraMessageBox.Show("Only few Project(s) alone have been selected for given Budget, Do you want to Proceed ?", "Acme.erp", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                        {
                            Rtn = budgetprojects;
                        }
                        else
                        {
                            Rtn = ReportProperty.Current.Project;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Rtn = budgetprojects;
            }
            return Rtn;
        }

        private void gcBudgetNewProject_ProcessGridKey(object sender, KeyEventArgs e)
        {
            bool canFoucsCashTrnasaction = false;
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control &&
                (gvBudgetNewProject.FocusedColumn == ColBudgetPRemakrs || (settinguserProperty.ConsiderBudgetNewProject == 0 && gvBudgetNewProject.FocusedColumn == ColPProvinceHelp))) //gvBudgetNewProject.FocusedColumn == ColPProvinceHelp
            {
                gvBudgetNewProject.PostEditor();
                gvBudgetNewProject.UpdateCurrentRow();

                if (string.IsNullOrEmpty(BudgetNewProject) && BudgetNewProjectPIncomeAmount == 0 && BudgetNewProjectPExpenseAmount == 0 && BudgetNewProjectPProvinceHelp == 0)
                {
                    if (IsValidSource()) { canFoucsCashTrnasaction = true; }
                    else { FocusBudgetNewProjectGrid(); }
                }

                if (canFoucsCashTrnasaction)
                {
                    gvBudgetNewProject.CloseEditor();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    btnOk.Select();
                    btnOk.Focus();
                }

                //This will assign new row, active LedgerId and LedgerAmount will be cleared
                if ((!string.IsNullOrEmpty(BudgetNewProject) && (BudgetNewProjectPIncomeAmount != 0 || BudgetNewProjectPExpenseAmount != 0 || BudgetNewProjectPProvinceHelp != 0))
                    && gvBudgetNewProject.IsLastRow)
                {
                    BudgetNewProjectGridNewItem = true;
                    if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                    {
                        e.SuppressKeyPress = true;
                    }
                }
            }
            else if (gvBudgetNewProject.IsFirstRow && gvBudgetNewProject.FocusedColumn == colLedger && e.Shift && e.KeyCode == Keys.Tab)
            {
                btnOk.Select();
                btnOk.Focus();
            }
        }

        private void rtxtBudgetNewProject_Validating(object sender, CancelEventArgs e)
        {
            TextEdit txtBudgetnew = sender as TextEdit;

            if (string.IsNullOrEmpty(txtBudgetnew.Text))
            {
                txtBudgetnew.Text = txtBudgetnew.OldEditValue.ToString();
            }

        }

        private void rbtnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DeleteBudgetNewProject();
        }

        /// <summary>
        /// Focus first row of the Budget new project grid
        /// </summary>
        private void FocusBudgetNewProjectGrid()
        {
            gcBudgetNewProject.Select();
            gcBudgetNewProject.Focus();
            gvBudgetNewProject.MoveFirst();
            gvBudgetNewProject.FocusedColumn = gvBudgetNewProject.Columns.ColumnByName(ColBudgetNewProject.Name);
            gvBudgetNewProject.ShowEditor();
        }

        /// <summary>
        /// Check Valid Budget new proejct row
        /// </summary>
        /// <returns></returns>
        private bool IsValidSource()
        {
            bool Rtn = false;
            DataTable dtBudgetNewProjects = gcBudgetNewProject.DataSource as DataTable;
            DataView dv = new DataView(dtBudgetNewProjects);
            dv.RowFilter = reportSchema.ReportBudgetNewProject.NEW_PROJECTColumn.ColumnName + "<>'' " +
                                                " AND (" + reportSchema.ReportBudgetNewProject.PROPOSED_INCOME_AMOUNTColumn.ColumnName + ">0 OR " +
                                                    reportSchema.ReportBudgetNewProject.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName + ">0 OR " +
                                                    reportSchema.ReportBudgetNewProject.HO_HELP_PROPOSED_AMOUNTColumn.ColumnName + ">0)";
            if (dv.Count > 0)
            {
                Rtn = true;
            }

            return Rtn;
        }

        /// <summary>
        /// Add Empty row 
        /// </summary>
        /// <param name="dtSource"></param>
        /// <returns></returns>
        private DataTable AddBudgetNewProjectRow(DataTable dtSource)
        {
            DataRow dr = dtSource.NewRow();
            dr[reportSchema.ReportBudgetNewProject.ACC_YEAR_IDColumn.ColumnName] = 0;
            dr[reportSchema.ReportBudgetNewProject.NEW_PROJECTColumn.ColumnName] = "";
            dr[reportSchema.ReportBudgetNewProject.PROPOSED_INCOME_AMOUNTColumn.ColumnName] = 0;
            dr[reportSchema.ReportBudgetNewProject.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName] = 0;
            dr[reportSchema.ReportBudgetNewProject.HO_HELP_PROPOSED_AMOUNTColumn.ColumnName] = 0;
            dtSource.Rows.Add(dr);
            return dtSource;
        }

        /// <summary>
        /// Delete current budget new project row
        /// </summary>
        private void DeleteBudgetNewProject()
        {
            try
            {
                if (gvBudgetNewProject.RowCount >= 1)
                {
                    if (!string.IsNullOrEmpty(BudgetNewProject))
                    {
                        if (MessageBox.Show("Are you sure to delete current row ?", "Acmeerp", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (gvBudgetNewProject.RowCount == 1)
                            {
                                gvBudgetNewProject.DeleteRow(gvBudgetNewProject.FocusedRowHandle);
                                ConstractEmptyDatasource();
                            }
                            else
                            {
                                gvBudgetNewProject.DeleteRow(gvBudgetNewProject.FocusedRowHandle);
                            }
                        }
                    }
                }
                FocusBudgetNewProjectGrid();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// Add empty new row 
        /// </summary>
        private void ConstractEmptyDatasource()
        {
            if (gcBudgetNewProject.DataSource != null)
            {
                DataTable dtBindSource = gcBudgetNewProject.DataSource as DataTable;
                dtBindSource.AcceptChanges();
                if (dtBindSource.Rows.Count == 0)
                {
                    dtBindSource.Rows.Add(dtBindSource.NewRow());
                }
                FocusBudgetNewProjectGrid();

            }
        }

        private bool ValidateBudgetNewProjects()
        {
            bool rtn = false;

            if (settinguserProperty.CreateBudgetDevNewProjects == 0)
            {
                DataTable dtBindSource = gcBudgetNewProject.DataSource as DataTable;
                dtBindSource.AcceptChanges();
                DataView dv = new DataView(dtBindSource);

                DataTable dtUniqueBudgetNewProjects = dv.ToTable(true, reportSchema.ReportBudgetNewProject.NEW_PROJECTColumn.ColumnName);

                //#. Check duplicate budget new projects in grid
                if (dtUniqueBudgetNewProjects.Rows.Count != dtBindSource.Rows.Count)
                {
                    //MessageBox.Show("Few Project(s) are duplicated in the list", "Acmeerp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    MessageBox.Show(MessageRender.GetMessage(MessageCatalog.ReportMessage.FEW_PROJECT_DUP), "Acmeerp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rtn = false;
                }
                else { rtn = true; }

                //#. Check duplicate budget new projects in table
                if (rtn)
                {
                    //On 22/01/2024, To Skip checking development/activity/new project name 
                    //dv.RowFilter = reportSchema.ReportBudgetNewProject.NEW_PROJECTColumn.ColumnName + "<>'' " +
                    //                           " AND (" + reportSchema.ReportBudgetNewProject.PROPOSED_INCOME_AMOUNTColumn.ColumnName + "> 0 OR " +
                    //                           reportSchema.ReportBudgetNewProject.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName + "> 0 OR " +
                    //                           reportSchema.ReportBudgetNewProject.HO_HELP_PROPOSED_AMOUNTColumn.ColumnName + "> 0)";

                    //foreach (DataRowView drv in dv)
                    //{
                    //    string budgetnewproject = drv[reportSchema.ReportBudgetNewProject.NEW_PROJECTColumn.ColumnName].ToString().Trim();
                    //    Int32 sequenceno = settinguserProperty.NumberSet.ToInteger(drv[reportSchema.ReportBudgetNewProject.SEQUENCE_NOColumn.ColumnName].ToString());

                    //    if (!string.IsNullOrEmpty(budgetnewproject))
                    //    {
                    //        using (DataManager dataManager = new DataManager(SQLCommand.Setting.ExistsBudgetNewProjectsByAcYear))
                    //        {
                    //            dataManager.Parameters.Add(reportSchema.ReportBudgetNewProject.ACC_YEAR_IDColumn, SettingProperty.Current.AccPeriodId);
                    //            dataManager.Parameters.Add(reportSchema.ReportBudgetNewProject.BUDGET_IDColumn, "0");
                    //            dataManager.Parameters.Add(reportSchema.ReportBudgetNewProject.SEQUENCE_NOColumn, sequenceno);
                    //            dataManager.Parameters.Add(reportSchema.ReportBudgetNewProject.NEW_PROJECTColumn, budgetnewproject);

                    //            resultArgs = dataManager.FetchData(DataSource.DataTable);
                    //            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count == 0)
                    //            {
                    //                rtn = true;
                    //            }
                    //            else if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    //            {
                    //                MessageBox.Show("Project '" + budgetnewproject + "' is already available", "Acmeerp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //                rtn = false;
                    //                break;
                    //            }
                    //        }
                    //    }
                    //}
                }
            }
            else
            {
                rtn = true;
            }

            return rtn;
        }

        private void btnSign1_Click(object sender, EventArgs e)
        {
            SetSignImage(1);
        }

        private void btnSign2_Click(object sender, EventArgs e)
        {
            SetSignImage(2);
        }

        private void btnSign3_Click(object sender, EventArgs e)
        {
            SetSignImage(3);
        }

        private void picSign1_MouseUp(object sender, MouseEventArgs e)
        {
            PictureEdit picedit = sender as PictureEdit;
            HideContextMenu(picedit);
        }

        private void picSign2_MouseUp(object sender, MouseEventArgs e)
        {
            PictureEdit picedit = sender as PictureEdit;
            HideContextMenu(picedit);
        }

        private void picSign3_MouseUp(object sender, MouseEventArgs e)
        {
            PictureEdit picedit = sender as PictureEdit;
            HideContextMenu(picedit);
        }

        private void HideContextMenu(PictureEdit picedit)
        {
            DXPopupMenu menu = null;
            if (menu == null)
            {
                PropertyInfo info = typeof(PictureEdit).GetProperty("Menu", BindingFlags.NonPublic | BindingFlags.Instance);
                menu = info.GetValue(picedit, null) as DXPopupMenu;
                foreach (DXMenuItem item in menu.Items)
                {
                    //if (item.Caption.ToUpper() == "LOAD" || item.Caption.ToUpper() == "SAVE")
                    //{
                    item.Visible = false;
                    //}
                }
            }
        }

        private void SetSignImage(Int32 signnumber)
        {
            //string sign = Path.Combine(AcmeerpInstalledPath, "Sign" + signnumber + ".jpg");
            OpenFileDialog openfileSign = new OpenFileDialog();
            openfileSign.InitialDirectory = AcmeerpInstalledPath;
            openfileSign.Title = "Select Sign Image (Width <= 800 and Height <= 260)";

            //Filter the filedialog, so that it will show only the mentioned format images
            openfileSign.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.ico";

            if (openfileSign.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string signSelected = openfileSign.FileName;
                if (!string.IsNullOrEmpty(signSelected))
                {
                    //Bitmap Selectimage = new Bitmap(signSelected);
                    //Bitmap signimage = new Bitmap(350, 200);
                    //Graphics g = Graphics.FromImage(signimage);
                    //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    //g.FillRectangle(Brushes.White, 0, 0, 350, 200);
                    //g.DrawImage(Selectimage, 0, 0, 350, 200);

                    Bitmap signimage = new Bitmap(signSelected);
                    FileInfo file = new FileInfo(signSelected);
                    double sizeInBytes = file.Length;
                    double filesize = Math.Round(sizeInBytes / 1024);
                    var imageHeight = signimage.Height;
                    var imageWidth = signimage.Width;

                    if (filesize > 50)
                    {
                        MessageRender.ShowMessage("Sign Image file size big, please select a file less than or equal 50 KB");
                    }
                    else if (imageWidth > 800 || imageHeight > 260)
                    {
                        MessageRender.ShowMessage("Sign Image file size must be (Width is <=800 and Height is <=260)");
                    }
                    else
                    {
                        byte[] byteSignImage = ImageProcessing.ImageToByteArray(signimage);
                        if (signnumber == 1)
                            picSign1.Image = ImageProcessing.ByteArrayToImage(byteSignImage);
                        else if (signnumber == 2)
                            picSign2.Image = ImageProcessing.ByteArrayToImage(byteSignImage);
                        else if (signnumber == 3)
                            picSign3.Image = ImageProcessing.ByteArrayToImage(byteSignImage);
                    }
                }
            }
        }

        private bool CheckDateRangeWithInFY()
        {
            bool Rtn = false;
            if (ReportProperty.Current.DateSet.ToDate(DateFrom.Text, false) >= ReportProperty.Current.DateSet.ToDate(settinguserProperty.YearFrom, false) &&
                ReportProperty.Current.DateSet.ToDate(DateFrom.Text, false) <= ReportProperty.Current.DateSet.ToDate(settinguserProperty.YearTo, false))
            {
                Rtn = true;
            }

            if (Rtn && (ReportProperty.Current.DateSet.ToDate(DateTo.Text, false) >= ReportProperty.Current.DateSet.ToDate(settinguserProperty.YearFrom, false) &&
                        ReportProperty.Current.DateSet.ToDate(DateTo.Text, false) <= ReportProperty.Current.DateSet.ToDate(settinguserProperty.YearTo, false)))
            {
                Rtn = true;
            }
            else
            {
                Rtn = false;
            }

            return Rtn;
        }

        private void gcBudgetNewProject_Click(object sender, EventArgs e)
        {
            DataTable dtBudgetProject = (DataTable)gcBudgetNewProject.DataSource;
            if (dtBudgetProject != null && dtBudgetProject.Rows.Count > 0)
            {
                int select = gvBudgetNewProject.GetFocusedRowCellValue(colBudgetIncludeReports) == null ? 0 : ReportProperty.Current.NumberSet.ToInteger(gvBudgetNewProject.GetFocusedRowCellValue(colBudgetIncludeReports).ToString());
                //SetCostCentreSource();
                gvBudgetNewProject.SetFocusedRowCellValue(colBudgetIncludeReports, select == 0 ? 1 : 0);
            }
        }

        private void btnUpdateAssetDepreciation_Click(object sender, EventArgs e)
        {
            //On show Fixed Asset and Depreciation details
            object[] argsVoucher = new object[1];
            argsVoucher[0] = true;
            if (ReportProperty.Current.ReportId == "RPT-218")
            {
                argsVoucher = new object[3];
                argsVoucher[0] = true;

                argsVoucher[1] = this.DateFrom.DateTime;
                argsVoucher[2] = this.DateTo.DateTime;
            }

            string transactionScreen = ReportProperty.Current.EnumSet.GetDescriptionFromEnumValue(DrillDownType.GENERALATE_MAPPING);
            if (transactionScreen != string.Empty)
            {
                Form transactionForm = ReportProperty.Current.GetDynamicInstance(transactionScreen, argsVoucher) as Form;
                if (transactionForm != null)
                {
                    transactionForm.ShowDialog();
                }
            }
        }

        // 14/11/2024
        private void glkpITRGroup_EditValueChanged(object sender, EventArgs e)
        {
            ITRGroupId = reportProperty.NumberSet.ToInteger(glkpITRGroup.EditValue.ToString());
            SetProjectSource();
            setmultibank();
        }

        private void glkpReportSetupCurrencyCountry_EditValueChanged(object sender, EventArgs e)
        {
            int CountryId = (glkpReportSetupCurrencyCountry.EditValue == null ? 0 : ReportProperty.Current.NumberSet.ToInteger(glkpReportSetupCurrencyCountry.EditValue.ToString()));
            this.repcbCountryCurrencyId = 0;
            this.repcbCountryCurrency = string.Empty;
            this.repcbCurrencySymbol = string.Empty;

            try
            {
                if (CountryId != 0)
                {
                    if (glkpReportSetupCurrencyCountry.EditValue != null)
                    {
                        if (glkpReportSetupCurrencyCountry.GetSelectedDataRow() != null)
                        {
                            DataRowView drv = glkpReportSetupCurrencyCountry.GetSelectedDataRow() as DataRowView;
                            this.repcbCountryCurrencyId = ReportProperty.Current.NumberSet.ToInteger(drv[this.appSchema.Country.COUNTRY_IDColumn.ColumnName].ToString());
                            this.repcbCountryCurrency = drv[this.appSchema.Country.CURRENCY_NAMEColumn.ColumnName].ToString();
                            this.repcbCurrencySymbol = drv[this.appSchema.Country.CURRENCY_SYMBOLColumn.ColumnName].ToString();
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

        private void glkpCCCategory_EditValueChanged(object sender, EventArgs e)
        {
            CCCategoryId = glkpCCCategory.EditValue == null ? 0 : reportProperty.NumberSet.ToInteger(glkpCCCategory.EditValue.ToString());

            string projectids = SelectedProject();
            string ledgerids = SelectedLedgerGroupDetails();
            SetCostCentreSource(projectids, ledgerids);

        }
    }
}