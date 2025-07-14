/* 
 * ## On 29/06/2019, For Mumbai Province, In Budget, They need to maintain Province Help amount separately in Budget.
  It should be added into Budget income and should not be affected Finance
 */

using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model.UIModel;
using Bosco.Model.Transaction;
using System.IO;
using Bosco.Utility;
using ACPP.Modules.Transaction;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid;
using Bosco.Utility.ConfigSetting;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System.Globalization;
using AcMEDSync.Model;

namespace ACPP.Modules.Master
{
    public partial class frmAddAnnualBudget : frmFinanceBaseAdd
    {
        #region Variables
        public const string NATURE = "NATURE";
        public const string INCOMES = "Incomes";
        public const string EXPENSES = "Expenses";
        public const string ASSETS = "Assets";
        public const string LIABILLITES = "Liabillites";
        public const string ACTUAL = "ACTUAL";
        public const string PROPOSED_CURRENT_YR = "PROPOSED_CURRENT_YR";
        public const string APPROVED_CURRENT_YR = "APPROVED_CURRENT_YR";
        public const string APPROVED_PREVIOUS_YR = "APPROVED_PREVIOUS_YR";
        public const string LEDGER_ID = "LEDGER_ID";
        public const string NARRATION = "NARRATION";
        ResultArgs resultArgs = null;
        public event EventHandler UpdateHeld;
        private DataSet dsCostCentre = new DataSet();
        SettingProperty setting = new SettingProperty();
        string AssignProjectIds = string.Empty;

        #endregion

        #region Properties
        private int BudgetId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectIds { get; set; }
        public string BudgetTypeId { get; set; }
        private DateTime PeriodFrom { get; set; }
        private DataTable dtBudgetStatisticsRecords = new DataTable();
        private DataTable dtBudgetStrengthRecords = null;

        private DataTable dtBudgetDevelopmentalNewProjects = null;
        private DataTable dtBudgetDevelopmentalNewProjectsCCDetails = null;
        private DataTable dtBudgetCCDetails = null;

        public string GridselectedProjectIds
        {
            // i made this as a set property for selecting Date
            get
            {
                string selectedprojectids = string.Empty;
                foreach (int index in gvProject.GetSelectedRows())
                {
                    DataRow dr = gvProject.GetDataRow(index) as DataRow;
                    selectedprojectids += dr["PROJECT_ID"].ToString().Trim() + ",";
                }
                return selectedprojectids.TrimEnd(',');
            }
        }

        public string GridselectedProjects
        {
            // i made this as a set property for selecting Date
            get
            {
                string selectedprojectids = string.Empty;
                foreach (int index in gvProject.GetSelectedRows())
                {
                    DataRow dr = gvProject.GetDataRow(index) as DataRow;
                    selectedprojectids += dr["PROJECT"].ToString().Trim() + ",";
                }
                return selectedprojectids.TrimEnd(',');
            }
        }

        #endregion

        #region Constructor

        public frmAddAnnualBudget()
        {
            InitializeComponent();

            lcLinkNewProjects.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcLink.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            //On 29/06/2019, Show HO help amount ---------------------------------------------------------------------------------------
            lblHOHelp.Visibility = lcHOHelpProposedAmt.Visibility = lcHOHelpApprovedAmt.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (AppSetting.ENABLE_BUDGET_HO_HELP_AMOUNT)
            {
                lblHOHelp.Text = AppSetting.BUDGET_HO_HELP_AMOUNT_CAPTION;
                lblHOHelp.Visibility = lcHOHelpProposedAmt.Visibility = lcHOHelpApprovedAmt.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtHOHelpApprovedAmount.Enabled = false;
            }
            //--------------------------------------------------------------------------------------------------------------------------

            RealColumnEditBudgetAmount();
            RealColumnEditBudgetExpenseAmount();

            //On 19/04/2023, Enable/Disable Developmental/New Projects and Streanth details --------------------------------------------
            lcDevelopmentProjects.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcCCStrengthDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            btnDevProjects.Text = "New Projects";
            if (AppSetting.CreateBudgetDevNewProjects == 1)
            {
                lcDevelopmentProjects.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            if (AppSetting.IncludeBudgetCCStrengthDetails == 1)
            {
                lcCCStrengthDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            if (AppSetting.ConsiderBudgetNewProject == 1)
            {
                btnDevProjects.Text = "Developmental Projects";
            }
            //--------------------------------------------------------------------------------------------------------------------------

            using (BudgetSystem budgetsystem = new BudgetSystem())
            {
                if (dtBudgetDevelopmentalNewProjects == null) dtBudgetDevelopmentalNewProjects = budgetsystem.AppSchema.ReportNewBudgetProject.DefaultView.ToTable();
                if (dtBudgetDevelopmentalNewProjectsCCDetails == null) dtBudgetDevelopmentalNewProjectsCCDetails = budgetsystem.AppSchema.BudgetCostCentre.DefaultView.ToTable();
                if (dtBudgetStrengthRecords == null) dtBudgetStrengthRecords = budgetsystem.AppSchema.BudgetStrength.DefaultView.ToTable();
                if (dtBudgetCCDetails == null) dtBudgetCCDetails = budgetsystem.AppSchema.BudgetCostCentre.DefaultView.ToTable();
            }


        }

        public frmAddAnnualBudget(int BudgetId, string ProjectId)
            : this()
        {
            this.BudgetId = BudgetId;
            this.ProjectIds = ProjectId;
        }

        #endregion

        #region Events
        private void frmAnnualBudget_Load(object sender, EventArgs e)
        {
            LoadDefaults();

            //Hide income ledges, Import.export functions ABE
            /*lcExportBudget.Visibility = lcImportBudget.Visibility = ((this.AppSetting.IS_ABEBEN_DIOCESE) || (this.AppSetting.IS_DIOMYS_DIOCESE) ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
            lcBudgetActive.Visibility = ((this.AppSetting.IS_ABEBEN_DIOCESE) ? DevExpress.XtraLayout.Utils.LayoutVisibility.Never : DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
            lcMoveProToApproved.Visibility = lcExcellImport.Visibility = ((this.AppSetting.IS_ABEBEN_DIOCESE) || (this.AppSetting.IS_DIOMYS_DIOCESE) ? DevExpress.XtraLayout.Utils.LayoutVisibility.Never : DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
            lcIncomeBudgetLedgers.Visibility = ((this.AppSetting.IS_ABEBEN_DIOCESE) || (this.AppSetting.IS_DIOMYS_DIOCESE) ? DevExpress.XtraLayout.Utils.LayoutVisibility.Never : DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
            gccolexpenseapprovedcurrentYR.OptionsColumn.ReadOnly = (((this.AppSetting.IS_ABEBEN_DIOCESE) || (this.AppSetting.IS_DIOMYS_DIOCESE)) ? true : false);
            gccolexpenseapprovedcurrentYR.OptionsColumn.AllowEdit = !gccolexpenseapprovedcurrentYR.OptionsColumn.ReadOnly;*/


            //1. Hide Import and Export Button Always 
            lcImportBudget.Visibility = lcExportBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            //2. For SDB, show all features like statistics, Income Ledgers etc lock approved amount
            //if (this.AppSetting.IS_SDB_CONGREGATION || this.AppSetting.IS_CMF_CONGREGATION) On 22/03/2021
            //if (this.AppSetting.IS_CMF_CONGREGATION)
            //{
            //    lcMoveProToApproved.Visibility = lcExcellImport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //    lcIncomeBudgetLedgers.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

            //    gccolexpenseapprovedcurrentYR.OptionsColumn.ReadOnly = (this.AppSetting.IS_CMF_CONGREGATION && this.AppSetting.ApproveBudgetByPortal == 0) ? gccolexpenseapprovedcurrentYR.OptionsColumn.ReadOnly = true : gccolexpenseapprovedcurrentYR.OptionsColumn.ReadOnly = false;
            //    gccolexpenseapprovedcurrentYR.OptionsColumn.AllowEdit = !gccolexpenseapprovedcurrentYR.OptionsColumn.ReadOnly;

            //    gccolIncomeCurrentYearApproved.OptionsColumn.ReadOnly = (this.AppSetting.IS_CMF_CONGREGATION && this.AppSetting.ApproveBudgetByPortal == 0) ? gccolIncomeCurrentYearApproved.OptionsColumn.ReadOnly = true : gccolIncomeCurrentYearApproved.OptionsColumn.ReadOnly = false;
            //    gccolIncomeCurrentYearApproved.OptionsColumn.AllowEdit = !gccolIncomeCurrentYearApproved.OptionsColumn.ReadOnly;
            //    lciStatisticsButton.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

            //    lcMoveProToApproved.Visibility = (this.AppSetting.IS_CMF_CONGREGATION && this.AppSetting.ApproveBudgetByPortal == 0) ? DevExpress.XtraLayout.Utils.LayoutVisibility.Never : DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //}
            //else //3. Based on setting
            //{
            lcIncomeBudgetLedgers.Visibility = (this.AppSetting.IncludeIncomeLedgersInBudget == "1" ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
            lciStatisticsButton.Visibility = (this.AppSetting.IncludeBudgetStatistics == "1" ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never);

            //For ABE and Mysore
            //4. Show Import and Export Button only if ABE or Mysore
            if (BudgetId > 0)
            {
                //if (this.AppSetting.IS_ABEBEN_DIOCESE || this.AppSetting.IS_DIOMYS_DIOCESE)
                // ABE 03.12.2019
                if (this.AppSetting.IS_ABEBEN_DIOCESE || this.AppSetting.IS_DIOMYS_DIOCESE)
                {
                    lcImportBudget.Visibility = lcExportBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
            }

            // ABE 03.12.2019
            //lcMoveProToApproved.Visibility = lcExcellImport.Visibility = ((this.AppSetting.IS_ABEBEN_DIOCESE) || (this.AppSetting.IS_DIOMYS_DIOCESE) ? DevExpress.XtraLayout.Utils.LayoutVisibility.Never : DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
            lcMoveProToApproved.Visibility = lcExcellImport.Visibility = lcPrint.Visibility = ((this.AppSetting.IS_DIOMYS_DIOCESE) ? DevExpress.XtraLayout.Utils.LayoutVisibility.Never : DevExpress.XtraLayout.Utils.LayoutVisibility.Always);

            // mysore 07.01.2020 to disable the Active check box
            lcBudgetActive.Visibility = lciPercentage.Visibility = lciapply.Visibility = lableText.Visibility = ((this.AppSetting.IS_DIOMYS_DIOCESE) ? DevExpress.XtraLayout.Utils.LayoutVisibility.Never : DevExpress.XtraLayout.Utils.LayoutVisibility.Always);

            //gccolexpenseapprovedcurrentYR.OptionsColumn.ReadOnly = (((this.AppSetting.IS_ABEBEN_DIOCESE) || (this.AppSetting.IS_DIOMYS_DIOCESE)) ? true : false);
            gccolexpenseapprovedcurrentYR.OptionsColumn.ReadOnly = (((this.AppSetting.IS_ABEBEN_DIOCESE) || (this.AppSetting.IS_DIOMYS_DIOCESE)) ? true : false);
            gccolexpenseapprovedcurrentYR.OptionsColumn.AllowEdit = !gccolexpenseapprovedcurrentYR.OptionsColumn.ReadOnly;

            int Ids = isExistsMonthlyBudgetDistribution();

            gccolexpenseproposedcurrentYR.OptionsColumn.ReadOnly = Ids != 0 ? true : false;
            gccolexpenseproposedcurrentYR.OptionsColumn.AllowEdit = !gccolexpenseproposedcurrentYR.OptionsColumn.ReadOnly;

            // gccolIncomeCurrentYearApproved.OptionsColumn.ReadOnly = (((this.AppSetting.IS_ABEBEN_DIOCESE) || (this.AppSetting.IS_DIOMYS_DIOCESE)) ? true : false);
            gccolIncomeCurrentYearApproved.OptionsColumn.ReadOnly = (((this.AppSetting.IS_ABEBEN_DIOCESE) || (this.AppSetting.IS_DIOMYS_DIOCESE)) ? true : false);
            gccolIncomeCurrentYearApproved.OptionsColumn.AllowEdit = !gccolIncomeCurrentYearApproved.OptionsColumn.ReadOnly;

            //For ABE alone
            //5. Hide Budget Active for ABE alone
            //lcBudgetActive.Visibility = ((this.AppSetting.IS_ABEBEN_DIOCESE) ? DevExpress.XtraLayout.Utils.LayoutVisibility.Never : DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
            //lcBudgetActive.Visibility = ((this.AppSetting.IS_ABEBEN_DIOCESE) ? DevExpress.XtraLayout.Utils.LayoutVisibility.Never : DevExpress.XtraLayout.Utils.LayoutVisibility.Always);

            //On 22/03/2021
            EnforceBudgetFeatuersByLicenseKeys();

            // }
        }


        /// <summary>
        /// On 22/03/2021, Enable/Disable Budget featues based on license Keys
        /// 
        /// Except CMF, ABE, Mysore (some logic was fixed)
        /// </summary>
        private void EnforceBudgetFeatuersByLicenseKeys()
        {
            // if (!this.AppSetting.IS_CMF_CONGREGATION && !this.AppSetting.IS_ABEBEN_DIOCESE && !this.AppSetting.IS_DIOMYS_DIOCESE)
            if (!this.AppSetting.IS_ABEBEN_DIOCESE && !this.AppSetting.IS_DIOMYS_DIOCESE)
            {
                //Approve By Portal
                if (this.AppSetting.ApproveBudgetByPortal == 1 || this.AppSetting.ApproveBudgetByExcel == 1)
                {
                    gccolexpenseapprovedcurrentYR.OptionsColumn.ReadOnly = true;
                    gccolexpenseapprovedcurrentYR.OptionsColumn.AllowEdit = !gccolexpenseapprovedcurrentYR.OptionsColumn.ReadOnly;

                    gccolIncomeCurrentYearApproved.OptionsColumn.ReadOnly = true;
                    gccolIncomeCurrentYearApproved.OptionsColumn.AllowEdit = !gccolIncomeCurrentYearApproved.OptionsColumn.ReadOnly;

                    lcMoveProToApproved.Visibility = lcExcellImport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                    //if (BudgetId > 0)
                    if (this.AppSetting.ApproveBudgetByPortal == 1)
                    {
                        lcImportBudget.Visibility = lcExportBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    }

                    if (this.AppSetting.ApproveBudgetByExcel == 1)
                    {
                        lcExcellImport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    }

                    if (lblBudgetAction.Text.ToUpper() == BudgetAction.Approved.ToString().ToUpper())
                    {
                        colIncomeHONarration.Visible = true;
                        colExeHONarration.Visible = true;
                    }
                }
            }
        }

        private void AssignValues()
        {
            lblBudgetAction.Text = string.Empty;
            lcBudgetAction.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (BudgetId > 0)
                SetBudgetEdit();
            else
            {
                using (BudgetSystem budgetSystem = new BudgetSystem())
                {
                    budgetSystem.MultipleProjectId = ProjectIds = GridselectedProjectIds;
                    budgetSystem.BudgetTypeId = UtilityMember.NumberSet.ToInteger(glkpBudgetType.EditValue.ToString());
                    budgetSystem.DateFrom = dePeriodFrom.DateTime.ToString();
                    budgetSystem.DateTo = dePeriodTo.DateTime.ToString();

                    resultArgs = budgetSystem.GetAnnualBudget();
                    int Id = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["PROJECT_ID"].ToString());
                    string ProjectNames = resultArgs.DataSource.Table.Rows[0]["PROJECT"].ToString();

                    if (Id == 0)
                    {
                        FetchBudgetDetails();
                    }
                    else
                    {
                        txtPercentage.Enabled = btnApply.Enabled = false;
                        gcIncomeLedger.DataSource = gcExpenseLedger.DataSource = null;
                        //  ShowMessageBox(GetMessage(MessageCatalog.Master.Budget.BUDGET_ALREADY_MADE) + " -  " + ProjectNames);
                        // ABE 03.12.2019
                        if (this.AppSetting.IS_DIOMYS_DIOCESE)
                        {
                            ShowMessageBox("Budget is already made for this Project and for this Budget Type. When you click, Month is changed to Another Month " + " - " + ProjectNames);
                        }
                        else
                        {
                            ShowMessageBox(GetMessage(MessageCatalog.Master.Budget.BUDGET_ALREADY_MADE) + " -  " + ProjectNames);
                        }
                    }
                }
            }


            //08/10/2024, TO link unliked budget new projects 
            //For Temp --------------------------------------------------------------------------------------
            if (BudgetId > 0) LoadUnLinkedNewProjects();
            //-----------------------------------------------------------------------------------------------
        }

        private void GetLastMonthProperty()
        {
            if (this.UtilityMember.NumberSet.ToInteger(BudgetTypeId) == (int)BudgetType.BudgetMonth)
            {
                if (BudgetId == 0)
                {
                    using (BudgetSystem budgetsystem = new BudgetSystem())
                    {
                        budgetsystem.MultipleProjectId = GridselectedProjectIds;
                        resultArgs = budgetsystem.FetchLastMonthBudget();
                        if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            DateTime dtValue = resultArgs.DataSource.Table.Rows[0]["NEXT_DATE"] == DBNull.Value ? dePeriodFrom.DateTime : this.UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["NEXT_DATE"].ToString(), false);

                            string Current = new DateTime(this.UtilityMember.DateSet.ToDate(dtValue.ToString(), false).Year, this.UtilityMember.DateSet.ToDate(dtValue.ToString(), false).AddMonths(1).Month, this.UtilityMember.DateSet.ToDate(dtValue.ToString(), false).Day).ToString("MMM", CultureInfo.InvariantCulture);
                            string MonthCurrentYear = String.Format("{0}-{1}", Current, UtilityMember.DateSet.ToDate(dtValue.ToString(), true).AddMonths(1).Year);
                            glkpMonth.Text = MonthCurrentYear;
                            SetTitle();
                        }
                        else
                        {
                            LoadMonths();
                        }
                    }
                }
            }
        }

        private void txtBudgetName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtBudgetName);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (gvIncomeLedger.RowCount > 0 || gvExpenseLedger.RowCount > 0)
            {
                SetPercentage();
            }
            else
            {
                ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }
        }

        private void meNote_Leave(object sender, EventArgs e)
        {
            btnSave.Select();
            btnSave.Focus();
        }

        private void gcAnnualBudget_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control &&
                (gvIncomeLedger.FocusedColumn == gccolIncomeCurrentYearProposed))
            {
                gvIncomeLedger.UpdateCurrentRow();
            }
        }

        private void gvAnnualBudget_CustomDrawRowFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            if (e.Column == gccolIncomeLedger)
            {
                e.Info.DisplayText = "Total ";
            }
            else
            {
                e.Info.DisplayText = string.Format("{0:n}", e.Info.Value);
            }
            e.Appearance.DrawString(e.Cache, e.Info.DisplayText, e.Bounds, new SolidBrush(Color.Black));
            e.Handled = true;
        }

        private void RealColumnEditTransAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvIncomeLedger.PostEditor();
            gvIncomeLedger.UpdateCurrentRow();
            if (gvIncomeLedger.ActiveEditor == null)
            {
                gvIncomeLedger.ShowEditor();
            }
        }

        private void peExportExcel_Click(object sender, EventArgs e)
        {
            Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry(this.MdiParent);
            report.ShowBudgetView1(BudgetId, GridselectedProjectIds, txtBudgetName.Text, gvIncomeLedger);
        }

        private void pePrintBudget_Click(object sender, EventArgs e)
        {
            if (gcIncomeLedger.DataSource != null && gcIncomeLedger.DataSource != null)
            {
                if (BudgetId > 0)
                {
                    //On 22/03/2021, To show only proposed ledgers alone
                    //MessageRender.ShowMessage("Proposed and Actual Budget Ledger will be Printed / Exported");
                    MessageRender.ShowMessage("Proposed Budget Ledger alone will be Printed / Exported");

                    Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry(this.MdiParent);
                    report.ShowBudgetView1(BudgetId, GridselectedProjectIds, txtBudgetName.Text, gvIncomeLedger);
                }
                else
                {
                    //MessageRender.ShowMessage("Save Budget details and print");
                    //this.PrintGridViewDetails(gcExpenseLedger, "Budget View - ", PrintType.DT, gvExpenseLedger);
                    string projectnames = GridselectedProjects;
                    string RpTitle = (string.IsNullOrEmpty(txtBudgetName.Text) ? "Budget" : txtBudgetName.Text);
                    if (lcIncomeBudgetLedgers.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                    {
                        string AdditionTitle = "Budget Income Ledgers" + System.Environment.NewLine +
                                      "Budget Expense Ledgers" + System.Environment.NewLine +
                                      MessageCatalog.ReportCommonTitle.PERIOD + " " + this.dePeriodFrom.Text + " - " + this.dePeriodTo.Text + System.Environment.NewLine +
                                      RpTitle;
                        this.PrintGridViewDetails(gvIncomeLedger, projectnames, gvExpenseLedger, AdditionTitle);
                    }
                    else
                    {
                        string AdditionTitle = "Budget Expense Ledgers" + System.Environment.NewLine +
                                      string.Empty + System.Environment.NewLine +
                                      MessageCatalog.ReportCommonTitle.PERIOD + " " + this.dePeriodFrom.Text + " - " + this.dePeriodTo.Text + System.Environment.NewLine +
                                      RpTitle;
                        this.PrintGridViewDetails(gvIncomeLedger, projectnames, null, AdditionTitle);
                    }
                }
            }
            else
            {
                MessageRender.ShowMessage("Budget Ledgers are not available");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsFormValid())
            {
                DataTable dtThird = new DataTable();
                DataTable dtIncomeSource = gcIncomeLedger.DataSource as DataTable;
                DataTable dtExpenseSource = gcExpenseLedger.DataSource as DataTable;

                if (this.AppSetting.IS_DIOMYS_DIOCESE || this.AppSetting.IS_ABEBEN_DIOCESE)
                {
                    dtThird.Merge(dtExpenseSource);
                }
                else
                {
                    if (dtIncomeSource != null)
                    {
                        dtThird = dtIncomeSource.Copy();
                    }

                    if (dtExpenseSource != null)
                    {
                        dtThird.Merge(dtExpenseSource);
                    }

                }
                DataTable dtAnnualBudget = new DataTable();
                dtAnnualBudget = dtThird;

                DataView dvRecords = new DataView(dtAnnualBudget);
                dvRecords.RowFilter = this.AppSetting.IS_SDB_INM ? "PROPOSED_CURRENT_YR>0 OR APPROVED_CURRENT_YR>0 OR ACTUAL>0" : "PROPOSED_CURRENT_YR>0 OR APPROVED_CURRENT_YR>0";
                dtAnnualBudget = dvRecords.ToTable();

                if (dtAnnualBudget != null && dtAnnualBudget.Rows.Count > 0)
                {
                    using (BudgetSystem budgetSystem = new BudgetSystem())
                    {
                        budgetSystem.dtBudgetLedgers = dtAnnualBudget;
                        budgetSystem.dtBudgetStatisticsDetails = dtBudgetStatisticsRecords;
                        budgetSystem.dtBudgetDevelopmentalNewProjects = dtBudgetDevelopmentalNewProjects;
                        budgetSystem.dtBudgetDevelopmentalNewProjectsCCDetails = dtBudgetDevelopmentalNewProjectsCCDetails;
                        budgetSystem.dtBudgetStrengthRecords = dtBudgetStrengthRecords;
                        budgetSystem.dtBudgetCostCentre = dtBudgetCCDetails;
                        budgetSystem.BudgetId = BudgetId;
                        budgetSystem.BudgetName = txtBudgetName.Text;
                        budgetSystem.DateFrom = dePeriodFrom.DateTime.ToShortDateString();
                        budgetSystem.DateTo = dePeriodTo.DateTime.ToShortDateString();
                        budgetSystem.BudgetTypeId = UtilityMember.NumberSet.ToInteger(glkpBudgetType.EditValue.ToString());
                        budgetSystem.BudgetLevelId = 1;
                        budgetSystem.monthwiseDistribution = this.AppSetting.IS_ABEBEN_DIOCESE ? 1 : 0;
                        budgetSystem.MultipleProjectId = GridselectedProjectIds;
                        budgetSystem.HOHelpPropsedAmount = UtilityMember.NumberSet.ToDecimal(txtHOHelpPropsedAmount.Text);
                        budgetSystem.HOHelpApprovedAmount = UtilityMember.NumberSet.ToDecimal(txtHOHelpApprovedAmount.Text);
                        // To Save Active Budget 07.01.2020
                        if ((!this.AppSetting.IS_DIOMYS_DIOCESE))
                        {
                            budgetSystem.Status = chkActive.Checked ? 1 : 0;
                        }
                        else { budgetSystem.Status = 1; }

                        DataView dvFilter = new DataView(dtAnnualBudget);
                        dvFilter.RowFilter = "APPROVED_CURRENT_YR>0";
                        // if ((this.AppSetting.IS_CMF_CONGREGATION) && dvFilter.Count > 0)
                        if (dvFilter.Count > 0)
                            budgetSystem.BudAction = 2;

                        this.ShowWaitDialog();

                        this.Transaction.CostCenterInfo = dsCostCentre;

                        resultArgs = budgetSystem.SaveAnnualBudget();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            ShowSuccessMessage(GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            if (UpdateHeld != null)
                                UpdateHeld(this, e);
                            if (BudgetId == 0)
                            {
                                //On 26/03/2024, returns empty, assigned from budget system (Confirm with chinna) 
                                //BudgetId = UtilityMember.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                BudgetId = budgetSystem.BudgetId;
                                //  if (this.AppSetting.IS_ABEBEN_DIOCESE || this.AppSetting.IS_DIOMYS_DIOCESE)
                                // ABE 03.12.2019
                                if (this.AppSetting.IS_ABEBEN_DIOCESE || this.AppSetting.IS_DIOMYS_DIOCESE)
                                    lcImportBudget.Visibility = lcExportBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                                ////On 22/03/2021, Show Budget action
                                //lcBudgetAction.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                //lblBudgetAction.Text = budgetSystem.BudgetAction.ToString();

                                EnforceBudgetFeatuersByLicenseKeys();

                            }

                            //On 28/07/2021, Show Budget action
                            lcBudgetAction.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            lblBudgetAction.Text = ((BudgetAction)budgetSystem.BudAction).ToString();   //budgetSystem.BudgetAction.ToString();
                        }
                        else if (resultArgs != null && !resultArgs.Success)
                        {
                            this.ShowMessageBoxError(resultArgs.Message);
                        }
                        this.CloseWaitDialog();
                    }
                }
                else
                {
                    ShowMessageBox("No Proposed Amount to Save the Budget");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvIncomeLedger.OptionsView.ShowAutoFilterRow = chkFilter.Checked;
            gvExpenseLedger.OptionsView.ShowAutoFilterRow = chkFilter.Checked;
            if (chkFilter.Checked)
            {
                this.SetFocusRowFilter(gvIncomeLedger, gccolIncomeLedger);
                this.SetFocusRowFilter(gvExpenseLedger, gccolexpenseLedgerName);
            }
        }

        private void rtxtProposedIncomeCurrentYear_KeyDown(object sender, KeyEventArgs e)
        {
            double ActualAmount = 0;
            double CalculatedAmount = 0;
            int Percentage = 0;
            try
            {
                Percentage = gvIncomeLedger.GetRowCellValue(gvIncomeLedger.FocusedRowHandle, gccolIncomeCurrentYearProposed) != null ? UtilityMember.NumberSet.ToInteger(gvIncomeLedger.GetRowCellValue(gvIncomeLedger.FocusedRowHandle, gccolIncomeCurrentYearProposed).ToString()) : 0;
                ActualAmount = gvIncomeLedger.GetRowCellValue(gvIncomeLedger.FocusedRowHandle, gccolIncomeActualIncome) != null ? UtilityMember.NumberSet.ToDouble(gvIncomeLedger.GetRowCellValue(gvIncomeLedger.FocusedRowHandle, gccolIncomeActualIncome).ToString()) : 0;
                if ((e.KeyCode == Keys.D5) && e.Shift)
                {
                    CalculatedAmount = (ActualAmount * Percentage) / 100;
                    gvIncomeLedger.SetFocusedRowCellValue(gccolIncomeCurrentYearProposed, CalculatedAmount);
                    gvIncomeLedger.MoveNext();
                }
            }
            catch (OverflowException of)
            {
                ShowMessageBox(of.Message + Environment.NewLine + of.Source);
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        private void glkpBudgetType_EditValueChanged(object sender, EventArgs e)
        {
            if (UtilityMember.NumberSet.ToInteger(glkpBudgetType.EditValue.ToString()) == (int)BudgetType.BudgetByAnnualYear)
            {
                dePeriodTo.Enabled = dePeriodFrom.Enabled = false;
                lblPeriodFrom.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lblPeriodTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                dePeriodFrom.DateTime = UtilityMember.DateSet.ToDate(setting.YearFrom, false);
                dePeriodTo.DateTime = UtilityMember.DateSet.ToDate(setting.YearTo, false);
                //dePeriodFrom.Properties.MinValue = UtilityMember.DateSet.ToDate(setting.YearFrom, false);
                //  dePeriodTo.Properties.MaxValue = UtilityMember.DateSet.ToDate(setting.YearTo, false);
                if (this.AppSetting.IS_ABEBEN_DIOCESE)
                    glkpBudgetType.Enabled = false;
            }
            else if (UtilityMember.NumberSet.ToInteger(glkpBudgetType.EditValue.ToString()) == (int)BudgetType.BudgetByCalendarYear)
            {
                dePeriodTo.Enabled = dePeriodFrom.Enabled = false;
                LockBudgetDatePeriod();
                dePeriodTo.Enabled = dePeriodFrom.Enabled = false;
                lblPeriodFrom.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lblPeriodTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else if (UtilityMember.NumberSet.ToInteger(glkpBudgetType.EditValue.ToString()) == (int)BudgetType.BudgetMonth)
            {
                dePeriodTo.Enabled = dePeriodFrom.Enabled = false;
                lblPeriodFrom.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lblPeriodTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                dePeriodFrom.DateTime = UtilityMember.DateSet.ToDate(setting.YearFrom, false);
                dePeriodTo.DateTime = UtilityMember.DateSet.ToDate(setting.YearTo, false);
                dePeriodFrom.Properties.MinValue = UtilityMember.DateSet.ToDate(setting.YearFrom, false);
                dePeriodTo.Properties.MaxValue = dePeriodFrom.DateTime.AddMonths(1).AddDays(-1);
            }
            else if (UtilityMember.NumberSet.ToInteger(glkpBudgetType.EditValue.ToString()) == (int)BudgetType.BudgetAcademic)
            {
                dePeriodTo.Enabled = dePeriodFrom.Enabled = false;

                lblPeriodFrom.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lblPeriodTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                dePeriodFrom.DateTime = UtilityMember.DateSet.ToDate(setting.YearFrom, false);
                dePeriodTo.DateTime = UtilityMember.DateSet.ToDate(setting.YearTo, false);

                dePeriodFrom.Properties.MinValue = new DateTime(UtilityMember.DateSet.ToDate(dePeriodFrom.DateTime.ToString(), false).Year, 6, 1);
                dePeriodTo.Properties.MaxValue = new DateTime(UtilityMember.DateSet.ToDate(dePeriodTo.DateTime.ToString(), false).Year, 5, 31);
            }
            else if (UtilityMember.NumberSet.ToInteger(glkpBudgetType.EditValue.ToString()) == (int)BudgetType.BudgetPeriod)
            {
                dePeriodTo.Enabled = dePeriodFrom.Enabled = true;
                lblPeriodFrom.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lblPeriodTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                dePeriodFrom.DateTime = UtilityMember.DateSet.ToDate(setting.YearFrom, false);
                dePeriodTo.DateTime = UtilityMember.DateSet.ToDate(setting.YearTo, false);
                dePeriodFrom.Properties.MinValue = UtilityMember.DateSet.ToDate(setting.YearFrom, false);
                dePeriodTo.Properties.MinValue = UtilityMember.DateSet.ToDate(setting.YearFrom, false);
            }
            else
            {
                dePeriodTo.Enabled = dePeriodFrom.Enabled = true;
                lblPeriodFrom.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lblPeriodTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                dePeriodFrom.DateTime = UtilityMember.DateSet.ToDate(setting.YearFrom, false);
                dePeriodTo.DateTime = UtilityMember.DateSet.ToDate(setting.YearTo, false);
                dePeriodFrom.Properties.MinValue = UtilityMember.DateSet.ToDate(setting.YearFrom, false);
                dePeriodFrom.Properties.MaxValue = UtilityMember.DateSet.ToDate(setting.YearTo, false);
                dePeriodTo.Properties.MinValue = UtilityMember.DateSet.ToDate(setting.YearFrom, false);
                dePeriodTo.Properties.MaxValue = UtilityMember.DateSet.ToDate(setting.YearTo, false);
            }
            SetTitle();
        }

        private void dePeriodFrom_EditValueChanged(object sender, EventArgs e)
        {
            //chinna 12.03.2019
            if (BudgetId == 0)
            {
                DateTime date = new DateTime(dePeriodFrom.DateTime.Year, dePeriodFrom.DateTime.Month, dePeriodFrom.DateTime.Day);
                dePeriodTo.DateTime = date.AddMonths(12).AddDays(-1);
            }
            //On 12/07/2018, For closed Projects----
            LoadProjectDetails(sender);
            dePeriodFrom.Select();
            dePeriodFrom.Focus();
        }

        private void LockBudgetDatePeriod()
        {
            int monthcount = 0;
            try
            {
                using (BudgetSystem budget = new BudgetSystem())
                {
                    budget.BudgetTypeId = (int)BudgetType.BudgetByCalendarYear;
                    budget.ProjectId = this.ProjectId;
                    budget.DateFrom = new DateTime(UtilityMember.DateSet.ToDate(setting.YearFrom, false).AddYears(-1).Year, 1, 1).ToString();
                    budget.DateTo = new DateTime(UtilityMember.DateSet.ToDate(budget.YearTo, false).AddYears(-1).Year, 12, 31).ToString();
                    resultArgs = budget.GetCalenderYearBudget();
                    if (resultArgs.Success && resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0 && UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["COUNT"].ToString()) > 0)
                    {
                        int year = UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][budget.AppSchema.Budget.DATE_TOColumn.ColumnName].ToString(), false).AddYears(1).Year;
                        int months = UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][budget.AppSchema.Budget.DATE_TOColumn.ColumnName].ToString(), false).Month;
                        if (months == 12)
                        {
                            monthcount = UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][budget.AppSchema.Budget.DATE_TOColumn.ColumnName].ToString(), false).AddMonths(1).Month;
                        }
                        else
                            monthcount = UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][budget.AppSchema.Budget.DATE_TOColumn.ColumnName].ToString(), false).AddMonths(1).Month;

                        int day = UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][budget.AppSchema.Budget.DATE_TOColumn.ColumnName].ToString(), false).AddDays(1).Day;
                        if (year == UtilityMember.DateSet.ToDate(setting.YearTo, false).Year)
                            dePeriodFrom.DateTime = PeriodFrom = new DateTime(year - 1, monthcount, day);
                        else
                            dePeriodFrom.DateTime = PeriodFrom = new DateTime(year, monthcount, day);
                        dePeriodFrom.Enabled = false;
                    }
                    else
                    {
                        dePeriodFrom.Enabled = true;
                        dePeriodFrom.DateTime = new DateTime(UtilityMember.DateSet.ToDate(dePeriodFrom.DateTime.ToString(), false).Year, 1, 1);
                        dePeriodTo.DateTime = new DateTime(UtilityMember.DateSet.ToDate(dePeriodFrom.DateTime.ToString(), false).Year, 12, 31);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        private void glkpProject_Properties_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            int[] selectedrows = gvProject.GetSelectedRows();
            e.DisplayText = "(" + selectedrows.Length.ToString() + ") project(s) are selected";
        }

        private void glkpProject_Popup(object sender, EventArgs e)
        {
            GridLookUpEdit edit = sender as GridLookUpEdit;

            string[] proIds = AssignProjectIds.Split(',');
            foreach (string project in proIds)
            {
                for (int i = 0; i < gvProject.DataRowCount; i++)
                {
                    string getvalue = gvProject.GetRowCellValue(i, colProjectId).ToString();
                    if (getvalue != null && getvalue.Equals(project))
                    {
                        int rowHandle = gvProject.GetRowHandle(i);

                        if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                        {
                            edit.Properties.View.SelectRow(rowHandle);
                        }
                    }
                }
            }
        }

        private void peReplaceValue_Click(object sender, EventArgs e)
        {
            if (gcIncomeLedger.DataSource != null && gcIncomeLedger.DataSource != null)
            {
                if (ShowConfirmationMessage(this.AppSetting.EnableCostCentreBudget == 1 ? "Are you sure to assign Budget and Cost Centre Proposed Amount to Approved Amount ?" : "Are you sure to assign Proposed Amount to Approved Amount ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    //Province Help
                    if (lcHOHelpApprovedAmt.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always && txtHOHelpApprovedAmount.Visible)
                    {
                        txtHOHelpApprovedAmount.Text = txtHOHelpPropsedAmount.Text;
                    }

                    //All other Ledgers
                    DataTable dtIncomeSource = gcIncomeLedger.DataSource as DataTable;
                    foreach (DataRow drIncome in dtIncomeSource.Rows)
                    {
                        drIncome[APPROVED_CURRENT_YR] = this.UtilityMember.NumberSet.ToDecimal(drIncome[PROPOSED_CURRENT_YR].ToString());
                    }
                    gcIncomeLedger.DataSource = dtIncomeSource;

                    DataTable dtExpenseSource = gcExpenseLedger.DataSource as DataTable;
                    foreach (DataRow drExpense in dtExpenseSource.Rows)
                    {
                        drExpense[APPROVED_CURRENT_YR] = this.UtilityMember.NumberSet.ToDecimal(drExpense[PROPOSED_CURRENT_YR].ToString());
                    }
                    gcExpenseLedger.DataSource = dtExpenseSource;


                    // CC Propose to Approved
                    if (this.AppSetting.EnableCostCentreBudget == 1)
                    {
                        DataTable dtBudgetCCSoure = dtBudgetCCDetails;
                        foreach (DataRow drCC in dtBudgetCCSoure.Rows)
                        {
                            drCC["APPROVED_AMOUNT"] = this.UtilityMember.NumberSet.ToDecimal(drCC["PROPOSED_AMOUNT"].ToString());
                        }
                        dtBudgetCCSoure.AcceptChanges();
                    }
                }
            }
            else
            {
                MessageRender.ShowMessage("Budget Ledgers are not available");
            }
        }

        private void rtxtIncomeCurrentYearproposedamt_KeyDown(object sender, KeyEventArgs e)
        {
            double ActualAmount = 0;
            double CalculatedAmount = 0;
            int Percentage = 0;
            try
            {
                Percentage = gvIncomeLedger.GetRowCellValue(gvIncomeLedger.FocusedRowHandle, gccolIncomeCurrentYearProposed) != null ? UtilityMember.NumberSet.ToInteger(gvIncomeLedger.GetRowCellValue(gvIncomeLedger.FocusedRowHandle, gccolIncomeCurrentYearProposed).ToString()) : 0;
                ActualAmount = gvIncomeLedger.GetRowCellValue(gvIncomeLedger.FocusedRowHandle, gccolIncomeActualIncome) != null ? UtilityMember.NumberSet.ToDouble(gvIncomeLedger.GetRowCellValue(gvIncomeLedger.FocusedRowHandle, gccolIncomeActualIncome).ToString()) : 0;
                if ((e.KeyCode == Keys.D5) && e.Shift)
                {
                    CalculatedAmount = (ActualAmount * Percentage) / 100;
                    gvIncomeLedger.SetFocusedRowCellValue(gccolIncomeCurrentYearProposed, CalculatedAmount);
                    gvIncomeLedger.MoveNext();
                }
            }
            catch (OverflowException of)
            {
                ShowMessageBox(of.Message + Environment.NewLine + of.Source);
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        private void rtxtExpenseProposedCurrentYR_KeyDown(object sender, KeyEventArgs e)
        {
            double ActualAmount = 0;
            double CalculatedAmount = 0;
            int Percentage = 0;
            try
            {
                Percentage = gvExpenseLedger.GetRowCellValue(gvExpenseLedger.FocusedRowHandle, gccolexpenseproposedcurrentYR) != null ? UtilityMember.NumberSet.ToInteger(gvExpenseLedger.GetRowCellValue(gvExpenseLedger.FocusedRowHandle, gccolexpenseproposedcurrentYR).ToString()) : 0;
                ActualAmount = gvExpenseLedger.GetRowCellValue(gvExpenseLedger.FocusedRowHandle, gccolexpenseActualincome) != null ? UtilityMember.NumberSet.ToDouble(gvExpenseLedger.GetRowCellValue(gvExpenseLedger.FocusedRowHandle, gccolexpenseActualincome).ToString()) : 0;
                if ((e.KeyCode == Keys.D5) && e.Shift)
                {
                    CalculatedAmount = (ActualAmount * Percentage) / 100;
                    gvExpenseLedger.SetFocusedRowCellValue(gccolexpenseproposedcurrentYR, CalculatedAmount);
                    gvExpenseLedger.MoveNext();
                }
            }
            catch (OverflowException of)
            {
                ShowMessageBox(of.Message + Environment.NewLine + of.Source);
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        private void gcIncomeLedger_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control &&
                (gvIncomeLedger.FocusedColumn == gccolIncomeCurrentYearProposed || gvIncomeLedger.FocusedColumn == gccolIncomeCurrentYearApproved))
            {
                gvIncomeLedger.UpdateCurrentRow();

                //On 03/05/2023, to distribute budget ledgers with Cost Centre
                if (this.AppSetting.EnableCostCentreBudget == 1)
                {
                    Int32 incomeledgerid = gvIncomeLedger.GetFocusedRowCellValue(colIncomeLedgerId) != null ?
                        UtilityMember.NumberSet.ToInteger(gvIncomeLedger.GetFocusedRowCellValue(colIncomeLedgerId).ToString()) : 0;
                    string incomeledgername = gvIncomeLedger.GetFocusedRowCellValue(gccolIncomeLedger) != null ?
                        gvIncomeLedger.GetFocusedRowCellValue(gccolIncomeLedger).ToString() : "";
                    double proposedincomeamount = gvIncomeLedger.GetFocusedRowCellValue(gccolIncomeCurrentYearProposed) != null ?
                        UtilityMember.NumberSet.ToDouble(gvIncomeLedger.GetFocusedRowCellValue(gccolIncomeCurrentYearProposed).ToString()) : 0;
                    double approvedincomeamount = gvIncomeLedger.GetFocusedRowCellValue(gccolIncomeCurrentYearApproved) != null ?
                        UtilityMember.NumberSet.ToDouble(gvIncomeLedger.GetFocusedRowCellValue(gccolIncomeCurrentYearApproved).ToString()) : 0;
                    string transmode = TransSource.Cr.ToString();

                    frmBudgetLedgerCCDistribution frmbudgetledgercc = new frmBudgetLedgerCCDistribution(true, BudgetId, GridselectedProjectIds, incomeledgerid,
                                            incomeledgername, proposedincomeamount, approvedincomeamount, transmode, dtBudgetCCDetails);
                    DialogResult dialogresult = frmbudgetledgercc.ShowDialog();
                    if (dialogresult == System.Windows.Forms.DialogResult.OK)
                    {
                        if (frmbudgetledgercc.ReturnValue != null)
                        {
                            dtBudgetCCDetails = frmbudgetledgercc.ReturnValue as DataTable;
                        }
                    }
                }
            }
        }

        private void gcExpenseLedger_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control &&
                (gvExpenseLedger.FocusedColumn == gccolexpenseproposedcurrentYR || gvExpenseLedger.FocusedColumn == gccolexpenseapprovedcurrentYR))
            {
                gvExpenseLedger.UpdateCurrentRow();

                //On 03/05/2023, to distribute budget ledgers with Cost Centre
                if (this.AppSetting.EnableCostCentreBudget == 1)
                {
                    Int32 expenseledgerid = gvExpenseLedger.GetFocusedRowCellValue(colExpenseLedgerId) != null ?
                       UtilityMember.NumberSet.ToInteger(gvExpenseLedger.GetFocusedRowCellValue(colExpenseLedgerId).ToString()) : 0;
                    string expenseledgername = gvExpenseLedger.GetFocusedRowCellValue(gccolexpenseLedgerName) != null ?
                        gvExpenseLedger.GetFocusedRowCellValue(gccolexpenseLedgerName).ToString() : "";
                    double proposedexpensemount = gvExpenseLedger.GetFocusedRowCellValue(gccolexpenseproposedcurrentYR) != null ?
                        UtilityMember.NumberSet.ToDouble(gvExpenseLedger.GetFocusedRowCellValue(gccolexpenseproposedcurrentYR).ToString()) : 0;
                    double approvedexpenseamount = gvExpenseLedger.GetFocusedRowCellValue(gccolexpenseapprovedcurrentYR) != null ?
                        UtilityMember.NumberSet.ToDouble(gvExpenseLedger.GetFocusedRowCellValue(gccolexpenseapprovedcurrentYR).ToString()) : 0;
                    double amt = (gvExpenseLedger.FocusedColumn == gccolexpenseproposedcurrentYR ? proposedexpensemount : approvedexpenseamount);
                    string transmode = TransSource.Dr.ToString();
                    frmBudgetLedgerCCDistribution frmbudgetledgercc = new frmBudgetLedgerCCDistribution(true, BudgetId, GridselectedProjectIds, expenseledgerid,
                                            expenseledgername, proposedexpensemount, approvedexpenseamount, transmode, dtBudgetCCDetails);
                    DialogResult dialogresult = frmbudgetledgercc.ShowDialog();
                    if (dialogresult == System.Windows.Forms.DialogResult.OK)
                    {
                        if (frmbudgetledgercc.ReturnValue != null)
                        {
                            dtBudgetCCDetails = frmbudgetledgercc.ReturnValue as DataTable;
                        }
                    }
                }
            }
        }

        private void frmAddAnnualBudget_Shown(object sender, EventArgs e)
        {
            gvIncomeLedger.PostEditor();
            gvIncomeLedger.UpdateCurrentRow();
            if (gvIncomeLedger.ActiveEditor == null)
            {
                gvIncomeLedger.ShowEditor();
                txtBudgetName.Focus();
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (gvProject.GetSelectedRows().Length > 0)
            {
                AssignValues();
                AssignProjectIds = GridselectedProjectIds;
                GetLastMonthProperty();

            }
        }

        #endregion

        #region Methods
        private void LoadDefaults()
        {
            LoadBudgetType();
            LoadMonths();
            LoadProjectDetails();
            if (BudgetId > 0)
            {
                AssignValues();
            }
            else
            {
                lblBudgetAction.Text = string.Empty;
                lcBudgetAction.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }

            FetchUpdateBudgetStatisticDetails();
            SetTitle();
        }

        private void LoadMonths()
        {
            try
            {
                DataTable dtTrans = new DataTable();
                dtTrans.Columns.Add("NAME", typeof(string));

                var startDate = new DateTime(this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Year, this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Month, this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Day);
                var endDate = new DateTime(this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false).Year, this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false).Month, this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false).Day);

                endDate = new DateTime(endDate.Year, endDate.Month, DateTime.DaysInMonth(endDate.Year, endDate.Month));

                string[] Records = Enumerable.Range(0, Int32.MaxValue)
                                     .Select(e => startDate.AddMonths(e))
                                     .TakeWhile(e => e <= endDate)
                                     .Select(e => e.ToString("MMM-yyyy")).ToArray();
                foreach (string tlid in Records)
                {
                    dtTrans.Rows.Add(tlid);
                }
                glkpMonth.Properties.DataSource = dtTrans;
                glkpMonth.Properties.ValueMember = "NAME";
                glkpMonth.Properties.DisplayMember = "NAME";
                glkpMonth.EditValue = glkpMonth.Properties.GetKeyValue(0);
            }
            catch (Exception)
            {
                this.ShowMessageBoxWarning("Problems occured");
            }
        }

        private void SetTitle()
        {
            this.Text = BudgetId > 0 ? GetMessage(MessageCatalog.Master.Budget.BUDGET_ANNUAL_EDIT_CAPTION) : GetMessage(MessageCatalog.Master.Budget.BUDGET_ANNUAL_ADD_CAPTION);
            if (this.AppSetting.IS_DIOMYS_DIOCESE)
            {
                // int dt = UtilityMember.DateSet.ToDate(dePeriodFrom.DateTime.ToString(), true).AddMonths(-1).Month;
                // string strMonthName = mfi.GetMonthName(dt).ToString();
                // DateTimeFormatInfo mfi = new DateTimeFormatInfo();

                string Previous = new DateTime(this.UtilityMember.DateSet.ToDate(dePeriodFrom.DateTime.ToString(), false).Year, this.UtilityMember.DateSet.ToDate(dePeriodFrom.DateTime.ToString(), false).AddMonths(-1).Month, this.UtilityMember.DateSet.ToDate(dePeriodFrom.DateTime.ToString(), false).Day).ToString("MMM", CultureInfo.InvariantCulture);
                string Current = new DateTime(this.UtilityMember.DateSet.ToDate(dePeriodFrom.DateTime.ToString(), false).Year, this.UtilityMember.DateSet.ToDate(dePeriodFrom.DateTime.ToString(), false).Month, this.UtilityMember.DateSet.ToDate(dePeriodFrom.DateTime.ToString(), false).Day).ToString("MMM", CultureInfo.InvariantCulture);

                //  string MonthPreviousYear = String.Format("{0}-{1}", Enum.GetName(typeof(Month), UtilityMember.DateSet.ToDate(dePeriodFrom.DateTime.ToString(), true).AddMonths(-1).Month), UtilityMember.DateSet.ToDate(AppSetting.YearFrom, true).Year);
                string MonthPreviousYear = String.Format("{0}-{1}", Previous, UtilityMember.DateSet.ToDate(dePeriodFrom.DateTime.ToString(), true).AddMonths(-1).Year);
                string MonthCurrentYear = String.Format("{0}-{1}", Current, UtilityMember.DateSet.ToDate(dePeriodFrom.DateTime.ToString(), true).Year);

                gccolIncomeCurrentYearProposed.Caption = String.Format("Proposed {0}", MonthCurrentYear);
                gccolIncomeCurrentYearApproved.Caption = string.Format("Approved {0}", MonthCurrentYear);
                gccolIncomeActualIncome.Caption = String.Format("Actual {0}", MonthPreviousYear);
                gccolIncomePreviousApproved.Caption = String.Format("Approved {0}", MonthPreviousYear);

                gccolexpenseproposedcurrentYR.Caption = String.Format("Proposed {0}", MonthCurrentYear);
                gccolexpenseapprovedcurrentYR.Caption = string.Format("Approved {0}", MonthCurrentYear);
                gccolexpenseActualincome.Caption = String.Format("Actual Spent {0}", MonthPreviousYear);
                gccolexpenseApprovedPreviousYR.Caption = String.Format("Budgeted {0}", MonthPreviousYear);

                if (this.UtilityMember.NumberSet.ToInteger(BudgetTypeId) != (int)BudgetType.BudgetPeriod || this.UtilityMember.NumberSet.ToInteger(BudgetTypeId) != (int)BudgetType.BudgetByAnnualYear || this.UtilityMember.NumberSet.ToInteger(BudgetTypeId) != (int)BudgetType.BudgetByCalendarYear)
                {
                    lciMonth.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
                else
                {
                    lciMonth.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    //lciStatisticsButton.Visibility = (this.AppSetting.IS_DIOMYS_DIOCESE ? DevExpress.XtraLayout.Utils.LayoutVisibility.Never : DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
                }
                lcBudgetGroup.Text = String.Format(this.GetMessage(MessageCatalog.Master.Budget.BUDGET_FOR), MonthCurrentYear);

                gccolBudgetGroup.Visible = gccolBudgetSubGroup.Visible = false;
                colIncomeHONarration.Visible = colExeHONarration.Visible = true;
                gccolexpenseCode.Visible = gccolExpenseNature.Visible = false;

                // To Set the Size 

                gccolexpenseCode.Width = gccolIncomeCode.Width;
                gccolexpenseLedgerName.Width = gccolIncomeLedger.Width;
                gccolExpenseNature.Width = gccolIncomeNature.Width;
                //gccolBudgetGroup.Width = 30;
                //gccolBudgetSubGroup.Width = 30;
                gccolexpenseApprovedPreviousYR.Width = gccolIncomePreviousApproved.Width; // 70;
                gccolexpenseActualincome.Width = gccolIncomeActualIncome.Width;
                gccolexpenseproposedcurrentYR.Width = gccolIncomeCurrentYearProposed.Width;
                gccolexpenseapprovedcurrentYR.Width = gccolIncomeCurrentYearApproved.Width;
                gccolExpNarration.Width = gccolIncomeNarration.Width;
                colExeHONarration.Width = colIncomeHONarration.Width;
            }
            else
            {
                lciMonth.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //lciStatisticsButton.Visibility = this.AppSetting.IS_DIOMYS_DIOCESE ? DevExpress.XtraLayout.Utils.LayoutVisibility.Never : DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                if (UtilityMember.NumberSet.ToInteger(glkpBudgetType.EditValue.ToString()) == (int)BudgetType.BudgetByAnnualYear || UtilityMember.NumberSet.ToInteger(glkpBudgetType.EditValue.ToString()) == (int)BudgetType.BudgetByCalendarYear || UtilityMember.NumberSet.ToInteger(glkpBudgetType.EditValue.ToString()) == (int)BudgetType.BudgetYear)
                {
                    string FinCalPerPreviousYear = String.Format("{0}-{1}", UtilityMember.DateSet.ToDate(AppSetting.YearFrom, true).AddYears(-1).Year,
                        UtilityMember.DateSet.ToDate(AppSetting.YearTo, true).AddYears(-1).ToString("yy"));
                    string FinCalPerioCurrentYear = String.Format("{0}-{1}", UtilityMember.DateSet.ToDate(AppSetting.YearFrom, true).Year,
                        UtilityMember.DateSet.ToDate(AppSetting.YearTo, true).ToString("yy"));

                    gccolIncomeCurrentYearProposed.Caption = String.Format("Proposed {0}", FinCalPerioCurrentYear);
                    gccolIncomeCurrentYearApproved.Caption = string.Format("Approved {0}", FinCalPerioCurrentYear);
                    gccolIncomeActualIncome.Caption = String.Format("Realized {0}", FinCalPerPreviousYear);
                    gccolIncomePreviousApproved.Caption = String.Format("Approved {0}", FinCalPerPreviousYear);

                    gccolexpenseproposedcurrentYR.Caption = String.Format("Proposed {0}", FinCalPerioCurrentYear);
                    gccolexpenseapprovedcurrentYR.Caption = string.Format("Approved {0}", FinCalPerioCurrentYear);
                    gccolexpenseActualincome.Caption = String.Format("Realized {0}", FinCalPerPreviousYear);
                    gccolexpenseApprovedPreviousYR.Caption = String.Format("Approved {0}", FinCalPerPreviousYear);
                    lcBudgetGroup.Text = String.Format(this.GetMessage(MessageCatalog.Master.Budget.BUDGET_FOR), FinCalPerioCurrentYear);
                }
                else if (UtilityMember.NumberSet.ToInteger(glkpBudgetType.EditValue.ToString()) == (int)BudgetType.BudgetPeriod)
                {
                    gccolIncomePreviousApproved.Caption = "Previous Approved";
                    gccolIncomeActualIncome.Caption = "Previous Actual";
                    gccolIncomeCurrentYearProposed.Caption = "Current Proposed";
                    gccolIncomeCurrentYearApproved.Caption = "Current Approved";

                    gccolexpenseApprovedPreviousYR.Caption = "Previous Approved";
                    gccolexpenseActualincome.Caption = "Previous Actual";
                    gccolexpenseproposedcurrentYR.Caption = "Current Proposed";
                    gccolexpenseapprovedcurrentYR.Caption = "Current Approved";
                }

                // To Set the Size 
                gccolexpenseCode.Width = gccolIncomeCode.Width;
                gccolexpenseLedgerName.Width = gccolIncomeLedger.Width;
                gccolExpenseNature.Width = gccolIncomeNature.Width;
                //gccolBudgetGroup.Width = 30;
                //gccolBudgetSubGroup.Width = 30;
                gccolexpenseApprovedPreviousYR.Width = gccolIncomePreviousApproved.Width; // 70;
                gccolexpenseActualincome.Width = gccolIncomeActualIncome.Width;
                gccolexpenseproposedcurrentYR.Width = gccolIncomeCurrentYearProposed.Width;
                gccolexpenseapprovedcurrentYR.Width = gccolIncomeCurrentYearApproved.Width;
                gccolExpNarration.Width = gccolIncomeNarration.Width;
                colExeHONarration.Width = colIncomeHONarration.Width;

                // This is to visible the Properties
                gccolBudgetGroup.Visible = gccolBudgetSubGroup.Visible = this.AppSetting.IS_DIOMYS_DIOCESE ? false : false;
                colIncomeHONarration.Visible = colExeHONarration.Visible = this.AppSetting.IS_DIOMYS_DIOCESE ? true : false;
            }
        }

        private void LoadBudgetType()
        {
            if (this.AppSetting.IS_DIOMYS_DIOCESE)
            {
                BudgetType budgetType = new BudgetType();
                DataView dvbudget = this.UtilityMember.EnumSet.GetEnumDataSource(budgetType, Sorting.Ascending);
                if (dvbudget.Count > 0)
                {
                    dvbudget.RowFilter = "Id in (5)";

                    DataTable dtBudgetType = dvbudget.ToTable();
                    string EnumValBudgetMonth = this.UtilityMember.EnumSet.GetDescriptionFromEnumValue(BudgetType.BudgetMonth);
                    dtBudgetType.Rows[0]["Name"] = EnumValBudgetMonth;

                    glkpBudgetType.Properties.DataSource = dtBudgetType;
                    glkpBudgetType.Properties.DisplayMember = "Name";
                    glkpBudgetType.Properties.ValueMember = "Id";
                    glkpBudgetType.EditValue = glkpBudgetType.Properties.GetKeyValue(0);
                    //lciStatisticsButton.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    glkpBudgetType.Enabled = false;
                    lciMonth.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
            }
            else
            {
                BudgetType budgetType = new BudgetType();
                DataView dvbudget = this.UtilityMember.EnumSet.GetEnumDataSource(budgetType, Sorting.Ascending);
                if (dvbudget.Count > 0)
                {
                    if (BudgetId > 0)
                        dvbudget.RowFilter = "Id in (2,3,4,6)";
                    else
                        dvbudget.RowFilter = "Id in (2,3,4,6)";


                    DataTable dtBudgetType = dvbudget.ToTable();
                    string EnumValAnualYear = this.UtilityMember.EnumSet.GetDescriptionFromEnumValue(BudgetType.BudgetByAnnualYear);     // 3
                    string EnumValCalenderYear = this.UtilityMember.EnumSet.GetDescriptionFromEnumValue(BudgetType.BudgetByCalendarYear); // 4
                    string EnumValAcamedicYear = this.UtilityMember.EnumSet.GetDescriptionFromEnumValue(BudgetType.BudgetAcademic);       // 6
                    string EnumValBudgetPeriod = this.UtilityMember.EnumSet.GetDescriptionFromEnumValue(BudgetType.BudgetPeriod);         // 2

                    //dtBudgetType.Rows[0]["Name"] = EnumValAnualYear;
                    //dtBudgetType.Rows[1]["Name"] = EnumValCalenderYear;
                    //dtBudgetType.Rows[2]["Name"] = EnumValAcamedicYear;
                    //dtBudgetType.Rows[2]["Name"] = EnumValBudgetPeriod;

                    // Correctly assign names based on Ids instead of index
                    foreach (DataRow row in dtBudgetType.Rows)
                    {
                        int id = this.UtilityMember.NumberSet.ToInteger(row["Id"].ToString());
                        if (id == (int)BudgetType.BudgetByAnnualYear)
                            row["Name"] = EnumValAnualYear;
                        else if (id == (int)BudgetType.BudgetByCalendarYear)
                            row["Name"] = EnumValCalenderYear;
                        else if (id == (int)BudgetType.BudgetAcademic)
                            row["Name"] = EnumValAcamedicYear;
                        else if (id == (int)BudgetType.BudgetPeriod)
                            row["Name"] = EnumValBudgetPeriod;
                    }

                    glkpBudgetType.Properties.DataSource = dtBudgetType;
                    glkpBudgetType.Properties.DisplayMember = "Name";
                    glkpBudgetType.Properties.ValueMember = "Id";
                    glkpBudgetType.EditValue = glkpBudgetType.Properties.GetKeyValue(1);

                }
            }
        }

        private void FetchBudgetDetails()
        {
            using (BudgetSystem budgetAnnual = new BudgetSystem())
            {
                budgetAnnual.BudgetId = (int)AddNewRow.NewRow;
                budgetAnnual.MultipleProjectId = GridselectedProjectIds;
                budgetAnnual.DateFrom = dePeriodFrom.DateTime.ToString();
                budgetAnnual.DateTo = dePeriodTo.DateTime.ToString();
                budgetAnnual.BudgetTypeId = UtilityMember.NumberSet.ToInteger(glkpBudgetType.EditValue.ToString());
                budgetAnnual.BudgetTransMode = TransactionMode.CR.ToString();
                resultArgs = budgetAnnual.FetchBudgetAdd();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    if (resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable IncomeSource = resultArgs.DataSource.Table;

                        DataTable dtIncomeGridSource = new DataTable();
                        dtIncomeGridSource = gcIncomeLedger.DataSource as DataTable;

                        DataTable dtSourceGrid = dtIncomeGridSource;
                        DataTable dtSourceDB = IncomeSource;

                        if (dtSourceGrid != null && dtSourceGrid.Rows.Count > 0)
                        {
                            //This loop should be changed to LINQ
                            foreach (DataRow drDB in dtSourceDB.Rows)
                            {
                                Int32 LedgerId = UtilityMember.NumberSet.ToInteger(drDB[LEDGER_ID].ToString());
                                dtSourceGrid.DefaultView.RowFilter = string.Empty;
                                dtSourceGrid.DefaultView.RowFilter = "LEDGER_ID = " + LedgerId;
                                if (dtSourceGrid.DefaultView.Count == 0)
                                {
                                    DataRow drGrid = dtSourceGrid.NewRow();
                                    drGrid["LEDGER_ID"] = drDB["LEDGER_ID"];
                                    drGrid["LEDGER_CODE"] = drDB["LEDGER_CODE"];
                                    drGrid["LEDGER_NAME"] = drDB["LEDGER_NAME"];
                                    drGrid["NATURE"] = drDB["NATURE"];
                                    drGrid["APPROVED_PREVIOUS_YR"] = drDB["APPROVED_PREVIOUS_YR"];
                                    drGrid["ACTUAL"] = drDB["ACTUAL"];
                                    drGrid["PROPOSED_CURRENT_YR"] = drDB["PROPOSED_CURRENT_YR"];
                                    drGrid["APPROVED_CURRENT_YR"] = drDB["APPROVED_CURRENT_YR"];
                                    drGrid["NARRATION"] = drDB["NARRATION"];
                                    drGrid["BUDGET_TRANS_MODE"] = drDB["BUDGET_TRANS_MODE"];
                                    drGrid["NATURE_ID"] = drDB["NATURE_ID"];
                                    dtSourceGrid.Rows.Add(drGrid);
                                    dtSourceGrid.AcceptChanges();
                                }
                                else if (dtSourceGrid.DefaultView.Count == 1)
                                {
                                    decimal actual = UtilityMember.NumberSet.ToDecimal(drDB[ACTUAL].ToString());
                                    dtSourceGrid.DefaultView[0][ACTUAL] = actual;
                                    dtSourceGrid.AcceptChanges();
                                }
                            }

                            //This is to Remove the Datasource for Income
                            for (int i = dtSourceGrid.Rows.Count - 1; i >= 0; i--)
                            {
                                Int32 LedgerId = UtilityMember.NumberSet.ToInteger(dtSourceGrid.Rows[i][LEDGER_ID].ToString());
                                dtSourceDB.DefaultView.RowFilter = string.Empty;
                                dtSourceDB.DefaultView.RowFilter = "LEDGER_ID = " + LedgerId;
                                if (dtSourceDB.DefaultView.Count == 0)
                                {
                                    dtSourceGrid.Rows[i].Delete();
                                }
                            }
                            dtSourceGrid.DefaultView.RowFilter = string.Empty;
                            dtSourceGrid.AcceptChanges();
                        }

                        if (dtSourceGrid != null && dtSourceGrid.Rows.Count > 0)
                        {
                            dtSourceGrid.DefaultView.RowFilter = string.Empty;
                            dtSourceGrid.DefaultView.Sort = "NATURE_ID ASC";
                            gcIncomeLedger.DataSource = dtSourceGrid;
                            gcIncomeLedger.RefreshDataSource();
                        }
                        else
                        {
                            gcIncomeLedger.DataSource = IncomeSource;
                        }
                    }
                    else
                    {
                        gcIncomeLedger.DataSource = null;

                    }
                }
                else ShowMessageBox(resultArgs.Message);


                //Expense
                budgetAnnual.MultipleProjectId = GridselectedProjectIds;
                budgetAnnual.DateFrom = dePeriodFrom.DateTime.ToString();
                budgetAnnual.DateTo = dePeriodTo.DateTime.ToString();
                budgetAnnual.BudgetTypeId = UtilityMember.NumberSet.ToInteger(glkpBudgetType.EditValue.ToString());
                budgetAnnual.BudgetTransMode = TransactionMode.DR.ToString();
                resultArgs = budgetAnnual.FetchBudgetAdd();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    if (resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable ExpenseSource = resultArgs.DataSource.Table;

                        DataTable dtExpenseGridSource = new DataTable();
                        dtExpenseGridSource = gcExpenseLedger.DataSource as DataTable;

                        DataTable dtSourceExpenseGrid = dtExpenseGridSource;
                        DataTable dtExpenseSourceDB = ExpenseSource;

                        if (dtSourceExpenseGrid != null && dtSourceExpenseGrid.Rows.Count > 0)
                        {
                            foreach (DataRow drDB in dtExpenseSourceDB.Rows)
                            {
                                Int32 LedgerId = UtilityMember.NumberSet.ToInteger(drDB[LEDGER_ID].ToString());
                                dtSourceExpenseGrid.DefaultView.RowFilter = string.Empty;
                                dtSourceExpenseGrid.DefaultView.RowFilter = "LEDGER_ID = " + LedgerId;
                                if (dtSourceExpenseGrid.DefaultView.Count == 0)
                                {
                                    DataRow drGrid = dtSourceExpenseGrid.NewRow();
                                    drGrid["LEDGER_ID"] = drDB["LEDGER_ID"];
                                    drGrid["LEDGER_CODE"] = drDB["LEDGER_CODE"];
                                    drGrid["LEDGER_NAME"] = drDB["LEDGER_NAME"];
                                    drGrid["NATURE"] = drDB["NATURE"];
                                    drGrid["APPROVED_PREVIOUS_YR"] = drDB["APPROVED_PREVIOUS_YR"];
                                    drGrid["ACTUAL"] = drDB["ACTUAL"];
                                    drGrid["PROPOSED_CURRENT_YR"] = drDB["PROPOSED_CURRENT_YR"];
                                    drGrid["APPROVED_CURRENT_YR"] = drDB["APPROVED_CURRENT_YR"];
                                    drGrid["NARRATION"] = drDB["NARRATION"];
                                    drGrid["BUDGET_TRANS_MODE"] = drDB["BUDGET_TRANS_MODE"];
                                    drGrid["NATURE_ID"] = drDB["NATURE_ID"];
                                    dtSourceExpenseGrid.Rows.Add(drGrid);
                                    dtSourceExpenseGrid.AcceptChanges();
                                }
                                else if (dtSourceExpenseGrid.DefaultView.Count == 1)
                                {
                                    decimal actual = UtilityMember.NumberSet.ToDecimal(drDB["ACTUAL"].ToString());
                                    dtSourceExpenseGrid.DefaultView[0]["ACTUAL"] = actual;
                                    dtSourceExpenseGrid.AcceptChanges();
                                }
                            }

                            //This is to Remove the Datasource for Income
                            for (int i = dtSourceExpenseGrid.Rows.Count - 1; i >= 0; i--)
                            {
                                Int32 LedgerId = UtilityMember.NumberSet.ToInteger(dtSourceExpenseGrid.Rows[i]["LEDGER_ID"].ToString());
                                dtExpenseSourceDB.DefaultView.RowFilter = string.Empty;
                                dtExpenseSourceDB.DefaultView.RowFilter = "LEDGER_ID = " + LedgerId;
                                if (dtExpenseSourceDB.DefaultView.Count == 0)
                                {
                                    dtSourceExpenseGrid.Rows[i].Delete();
                                }
                            }
                            dtSourceExpenseGrid.DefaultView.RowFilter = string.Empty;
                            dtSourceExpenseGrid.AcceptChanges();
                        }

                        if (dtSourceExpenseGrid != null && dtSourceExpenseGrid.Rows.Count > 0)
                        {
                            dtSourceExpenseGrid.DefaultView.RowFilter = string.Empty;
                            dtSourceExpenseGrid.DefaultView.Sort = "NATURE_ID ASC";
                            gcExpenseLedger.DataSource = dtSourceExpenseGrid;
                            gcExpenseLedger.RefreshDataSource();
                        }
                        else
                        {
                            gcExpenseLedger.DataSource = ExpenseSource;
                        }
                    }
                    else
                    {
                        gcExpenseLedger.DataSource = null;

                    }
                }
                else ShowMessageBox(resultArgs.Message);
            }
        }

        private void SetBudgetEdit()
        {
            lblBudgetAction.Text = string.Empty;
            lcBudgetAction.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            using (BudgetSystem budgetAnnual = new BudgetSystem(BudgetId, BudgetTypeId))
            {
                budgetAnnual.BudgetId = BudgetId;
                txtBudgetName.Text = budgetAnnual.BudgetName;
                txtHOHelpPropsedAmount.Text = budgetAnnual.HOHelpPropsedAmount.ToString();
                txtHOHelpApprovedAmount.Text = budgetAnnual.HOHelpApprovedAmount.ToString();
                AssignProjectIds = budgetAnnual.MultipleProjectId = GridselectedProjectIds.Equals(string.Empty) ? budgetAnnual.MultipleProjectId : GridselectedProjectIds;
                this.glkpBudgetType.EditValueChanged -= new System.EventHandler(this.glkpBudgetType_EditValueChanged);
                glkpBudgetType.EditValue = budgetAnnual.BudgetTypeId;
                glkpBudgetType.Enabled = false;
                this.glkpBudgetType.EditValueChanged += new System.EventHandler(this.glkpBudgetType_EditValueChanged);

                //On 22/03/2021, Show Budget action
                lblBudgetAction.Text = budgetAnnual.BudgetAction.ToString();

                dtBudgetDevelopmentalNewProjects = budgetAnnual.dtBudgetDevelopmentalNewProjects;
                dtBudgetDevelopmentalNewProjectsCCDetails = budgetAnnual.dtBudgetDevelopmentalNewProjectsCCDetails;
                dtBudgetStrengthRecords = budgetAnnual.dtBudgetStrengthRecords;
                dtBudgetCCDetails = budgetAnnual.dtBudgetCostCentre;

                if (budgetAnnual.BudgetTypeId != (int)BudgetType.BudgetPeriod)
                {
                    dePeriodTo.DateTime = UtilityMember.DateSet.ToDate(budgetAnnual.DateTo, false);
                    dePeriodFrom.DateTime = UtilityMember.DateSet.ToDate(budgetAnnual.DateFrom, false);
                }
                chkActive.Checked = budgetAnnual.Status == 0 ? false : true;

                if (budgetAnnual.BudgetTypeId == (int)BudgetType.BudgetByCalendarYear)
                {
                    lblPeriodFrom.Visibility = lblPeriodTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    dePeriodFrom.Enabled = dePeriodTo.Enabled = false;
                }
                else if (budgetAnnual.BudgetTypeId == (int)BudgetType.BudgetByAnnualYear)
                {
                    lblPeriodFrom.Visibility = lblPeriodTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    dePeriodFrom.Enabled = dePeriodTo.Enabled = false;
                }
                else if (budgetAnnual.BudgetTypeId == (int)BudgetType.BudgetAcademic)
                {
                    lblPeriodFrom.Visibility = lblPeriodTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    dePeriodFrom.Enabled = dePeriodTo.Enabled = false;
                }
                else if (budgetAnnual.BudgetTypeId == (int)BudgetType.BudgetPeriod)
                {
                    lblPeriodFrom.Visibility = lblPeriodTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    dePeriodFrom.Enabled = dePeriodTo.Enabled = true;
                    lciMonth.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                    // commanded on chinna 12.03.2018
                    //if (dePeriodTo.DateTime == UtilityMember.DateSet.ToDate(this.setting.YearTo, false) && dePeriodFrom.DateTime == UtilityMember.DateSet.ToDate(budgetAnnual.DateFrom, false))
                    //{
                    //    dePeriodTo.DateTime = UtilityMember.DateSet.ToDate(budgetAnnual.DateTo, false);
                    //    dePeriodFrom.DateTime = UtilityMember.DateSet.ToDate(budgetAnnual.DateFrom, false);
                    //}
                    //else
                    //{
                    //    dePeriodFrom.DateTime = dePeriodFrom.DateTime;
                    //    dePeriodTo.DateTime = dePeriodTo.DateTime;
                    //}

                    dePeriodTo.DateTime = UtilityMember.DateSet.ToDate(budgetAnnual.DateTo, false);
                    dePeriodFrom.DateTime = UtilityMember.DateSet.ToDate(budgetAnnual.DateFrom, false);
                    dePeriodFrom.Enabled = dePeriodTo.Enabled = false;

                }
                else if (budgetAnnual.BudgetTypeId == (int)BudgetType.BudgetMonth)
                {
                    lblPeriodFrom.Visibility = lblPeriodTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    dePeriodFrom.Enabled = dePeriodTo.Enabled = glkpMonth.Enabled = false;
                    dePeriodFrom.DateTime = UtilityMember.DateSet.ToDate(budgetAnnual.DateFrom, false);
                    dePeriodTo.DateTime = UtilityMember.DateSet.ToDate(budgetAnnual.DateTo, false);
                    string Current = new DateTime(this.UtilityMember.DateSet.ToDate(budgetAnnual.DateFrom, false).Year, this.UtilityMember.DateSet.ToDate(budgetAnnual.DateFrom, false).Month, this.UtilityMember.DateSet.ToDate(budgetAnnual.DateFrom, false).Day).ToString("MMM", CultureInfo.InvariantCulture);
                    string MonthCurrentYear = String.Format("{0}-{1}", Current, UtilityMember.DateSet.ToDate(dePeriodFrom.DateTime.ToString(), true).Year);
                    glkpMonth.EditValue = MonthCurrentYear;
                    glkpBudgetType.EditValue = 5;
                }

                budgetAnnual.BudgetTransMode = TransactionMode.CR.ToString();

                resultArgs = budgetAnnual.FetchBudgetEdit();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    if (resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable IncomeSource = resultArgs.DataSource.Table;
                        gcIncomeLedger.DataSource = IncomeSource;
                    }
                }
                else ShowMessageBox(resultArgs.Message);

                budgetAnnual.BudgetTransMode = TransactionMode.DR.ToString();

                resultArgs = budgetAnnual.FetchBudgetEdit();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    if (resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable ExpenseSource = resultArgs.DataSource.Table;
                        gcExpenseLedger.DataSource = ExpenseSource;
                    }
                }
                else ShowMessageBox(resultArgs.Message);
            }
        }

        private void glkpMonth_EditValueChanged(object sender, EventArgs e)
        {
            var startDate = new DateTime(this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Year, this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Month, this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Day);

            DateTime SelectedMonth = Convert.ToDateTime(glkpMonth.EditValue.ToString());

            if (UtilityMember.NumberSet.ToInteger(glkpBudgetType.EditValue.ToString()) == (int)BudgetType.BudgetMonth)
            {
                lblPeriodFrom.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lblPeriodTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                dePeriodTo.Enabled = dePeriodFrom.Enabled = false;

                dePeriodFrom.DateTime = this.UtilityMember.DateSet.ToDate(SelectedMonth.ToShortDateString(), false);
                dePeriodTo.DateTime = dePeriodFrom.DateTime.AddMonths(1).AddDays(-1);
            }
            SetTitle();
        }

        private void SetPercentage()
        {
            DataTable dtBudgetIncome = gcIncomeLedger.DataSource as DataTable;
            DataTable dtBudgetExpense = gcExpenseLedger.DataSource as DataTable;

            double Percentage = UtilityMember.NumberSet.ToDouble(txtPercentage.Text);

            //  if (Percentage < 0)
            // {
            if (dtBudgetIncome != null)
            {
                dtBudgetIncome.Select().ToList<DataRow>().ForEach(r =>
                {
                    double Actual = UtilityMember.NumberSet.ToDouble(r[ACTUAL].ToString());
                    double proposeincome = (Actual * Percentage) / 100;

                    double RoundoffIncome = Math.Floor(proposeincome / 1000) * 1000;

                    r[PROPOSED_CURRENT_YR] = (this.AppSetting.IS_SDB_INM) ? RoundoffIncome : proposeincome;

                });
                gcIncomeLedger.DataSource = dtBudgetIncome;
                gcIncomeLedger.RefreshDataSource();

                //dtBudgetIncome.Select().ToList<DataRow>().ForEach(r => { r[PROPOSED_CURRENT_YR] = (UtilityMember.NumberSet.ToDouble(r[ACTUAL].ToString()) * Percentage) / 100; });
                //gcIncomeLedger.DataSource = dtBudgetIncome;
                //gcIncomeLedger.RefreshDataSource();
            }
            if (dtBudgetExpense != null)
            {
                dtBudgetExpense.Select().ToList<DataRow>().ForEach(r =>
                {

                    double Actualexp = UtilityMember.NumberSet.ToDouble(r[ACTUAL].ToString());
                    double proposeexpense = (Actualexp * Percentage) / 100;

                    double RoundoffExp = Math.Floor(proposeexpense / 1000) * 1000;

                    r[PROPOSED_CURRENT_YR] = (this.AppSetting.IS_SDB_INM) ? RoundoffExp : proposeexpense;

                });
                gcExpenseLedger.DataSource = dtBudgetExpense;
                gcExpenseLedger.RefreshDataSource();
            }
            // }
            //else ShowMessageBox(GetMessage(MessageCatalog.Master.Budget.BUDGET_PERCENTAGE_VALIDATION));

        }

        private void LoadProjectDetails(object fromLoadingEventObject = null)
        {
            try
            {
                using (BudgetSystem budgetSystem = new BudgetSystem())
                {
                    budgetSystem.DateFrom = dePeriodFrom.DateTime.ToString();
                    budgetSystem.DateTo = dePeriodTo.DateTime.ToString();
                    budgetSystem.ProjectClosedDate = dePeriodFrom.DateTime.ToString();
                    resultArgs = budgetSystem.FetchBudgetProjectsLookup();
                    glkpProject.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, budgetSystem.AppSchema.Project.PROJECTColumn.ColumnName, budgetSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                        if (fromLoadingEventObject == null)
                        {
                            this.BeginInvoke(new MethodInvoker(glkpProject.ShowPopup));
                            this.BeginInvoke(new MethodInvoker(glkpProject.ClosePopup));
                        }
                    }
                    else
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Master.Budget.BUDGET_NO_PROJECTS), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (this.AppSetting.LockMasters == (int)YesNo.No)
                            {
                                frmProjectAdd frmProject = new frmProjectAdd((int)AddNewRow.NewRow);
                                frmProject.ShowDialog();
                                if (frmProject.DialogResult == DialogResult.Cancel)
                                {
                                    LoadProjectDetails();
                                }
                            }
                            else
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
                            }
                        }
                        else
                            this.Close();
                    }
                }


            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private bool IsFormValid()
        {
            bool IsValid = true;
            if (string.IsNullOrEmpty(txtBudgetName.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Budget.BUDGET_NAME_EMPTY));
                this.SetBorderColor(txtBudgetName);
                IsValid = false;
                txtBudgetName.Focus();
            }
            else if (string.IsNullOrEmpty(GridselectedProjectIds))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Budget.BUDGET_PROJECT_EMPTY));
                this.SetBorderColorForGridLookUpEdit(glkpProject);
                IsValid = false;
                glkpProject.Focus();
            }
            else if (AppSetting.ENABLE_BUDGET_HO_HELP_AMOUNT)
            {
                if (string.IsNullOrEmpty(txtHOHelpPropsedAmount.Text))
                {
                    this.ShowMessageBox("Province Help Proposed Amount is empty");
                    this.SetBorderColor(txtHOHelpPropsedAmount);
                    IsValid = false;
                    txtHOHelpPropsedAmount.Focus();
                }
            }
            else if (!IsvalidProjectDetails())
            {
                IsValid = false;
                btnGo.Focus();
            }
            else if (!isValidBudgetStatisticForm())
            {
                IsValid = false;
            }
            else if (!IsValidDataGrid(IsValid))
            {
                IsValid = false;
            }
            else if (!isValidActiveBudget())
            {
                IsValid = false;
            }
            else if (!ValidateCostCenter())
            {
                IsValid = false;
            }
            return IsValid;
        }

        //private bool isValidActiveBudget()
        //{
        //    bool isValid = true;
        //    //ABE 03.12.2019
        //    if (!this.AppSetting.IS_DIOMYS_DIOCESE)
        //    {
        //        using (BudgetSystem budgetSystem = new BudgetSystem())
        //        {
        //            if (budgetSystem.BudgetTypeId != (int)BudgetType.BudgetMonth)
        //            {
        //                if (budgetSystem.BudgetTypeId != (int)BudgetType.BudgetPeriod)
        //                {
        //                    if (chkActive.Checked)
        //                    {
        //                        budgetSystem.MultipleProjectId = GridselectedProjectIds;
        //                        int ActiveBudgetId = budgetSystem.CheckStatus();
        //                        if (!ActiveBudgetId.Equals(BudgetId))
        //                        {
        //                            if (this.ShowConfirmationMessage("Budget already exists for selected Project, Type and Date range, are you sure to save this budget as In-Active mode?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
        //                            {
        //                                if (!ActiveBudgetId.Equals(0))
        //                                {
        //                                    budgetSystem.Status = 0;
        //                                    chkActive.Checked = false;
        //                                    ShowMessageBox(this.GetMessage(MessageCatalog.Master.Budget.ACTIVE_BUDGET_IS_MADE));
        //                                }
        //                            }
        //                            else
        //                            {
        //                                isValid = false;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return isValid;
        //}

        private bool isValidActiveBudget()
        {
            bool isValid = true;
            //ABE 03.12.2019
            if (!this.AppSetting.IS_DIOMYS_DIOCESE)
            {
                using (BudgetSystem budgetSystem = new BudgetSystem())
                {
                    if (budgetSystem.BudgetTypeId != (int)BudgetType.BudgetMonth)
                    {
                        //if (budgetSystem.BudgetTypeId != (int)BudgetType.BudgetPeriod)
                        //  {
                        if (chkActive.Checked)
                        {
                            budgetSystem.MultipleProjectId = GridselectedProjectIds;
                            budgetSystem.DateFrom = dePeriodFrom.DateTime.ToString();
                            budgetSystem.DateTo = dePeriodTo.DateTime.ToString();
                            budgetSystem.BudgetTypeId = UtilityMember.NumberSet.ToInteger(glkpBudgetType.EditValue.ToString());
                            int ActiveBudgetId = budgetSystem.CheckStatus();

                            //  if (ActiveBudgetId != 0 || this.UtilityMember.NumberSet.ToInteger(BudgetTypeId) != (int)BudgetType.BudgetPeriod)
                            //{
                            // if (!ActiveBudgetId.Equals(BudgetId))

                            if (ActiveBudgetId != 0)
                            {
                                if (!ActiveBudgetId.Equals(BudgetId))
                                {
                                    if (this.ShowConfirmationMessage("Budget already exists for selected Project, Type and Date range, are you sure to save this budget as In-Active mode?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        if (!ActiveBudgetId.Equals(0))
                                        {
                                            budgetSystem.Status = 0;
                                            chkActive.Checked = false;
                                            ShowMessageBox(this.GetMessage(MessageCatalog.Master.Budget.ACTIVE_BUDGET_IS_MADE));
                                        }
                                    }
                                    else
                                    {
                                        isValid = false;
                                    }
                                }
                            }
                        }
                        // }
                    }
                }
            }
            return isValid;
        }
        private bool isValidBudgetStatisticForm()
        {
            bool isValid = true;
            if (lciStatisticsButton.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                if (dtBudgetStatisticsRecords != null || dtBudgetStatisticsRecords.Rows.Count <= 0)
                {
                    DataView dvRecords = new DataView(dtBudgetStatisticsRecords);
                    dvRecords.RowFilter = "(STATISTICS_TYPE_ID>0 OR TOTAL_COUNT>0)";
                    if (dvRecords.Count <= 0)
                    {
                        ShowBudgetStatisticsForms();

                        DataView dv = new DataView(dtBudgetStatisticsRecords);
                        dv.RowFilter = "(STATISTICS_TYPE_ID>0 OR TOTAL_COUNT>0)";
                        if (dv.Count <= 0)
                        {
                            MessageBox.Show("This is the mandatory to fill the Statistics about the Institutions", "", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            isValid = false;
                        }
                    }
                }
            }
            //DataTable dt = gcIncomeLedger.DataSource as DataTable;
            //DataTable dtex = gcExpenseLedger.DataSource as DataTable;
            return isValid;

        }

        private bool IsValidDataGrid(bool Istrue)
        {
            bool isValid = true;

            if (Istrue)
            {
                if (lcMoveProToApproved.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                {
                    if (BudgetId == 0)
                    {
                        DataTable dtIncomeSource = gcIncomeLedger.DataSource as DataTable;
                        DataTable dtExpenseSource = gcExpenseLedger.DataSource as DataTable;


                        DataTable dtCommonsSource = new DataTable();
                        if (dtIncomeSource != null)
                        {
                            dtCommonsSource = dtIncomeSource.Copy();
                        }
                        if (dtExpenseSource != null)
                        {
                            dtCommonsSource.Merge(dtExpenseSource);
                        }
                        if (dtCommonsSource != null && dtCommonsSource.Rows.Count > 0)
                        {
                            DataView dvRecords = new DataView(dtCommonsSource);
                            dvRecords.RowFilter = "(APPROVED_CURRENT_YR>0)";
                            if (dvRecords.Count <= 0)
                            {
                                if (this.ShowConfirmationMessage("Approved Amount are Zero, Do you want to continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                {
                                    gvIncomeLedger.FocusedColumn = gccolIncomeCurrentYearApproved;
                                    isValid = false;
                                }
                            }
                            DataTable dt = gcIncomeLedger.DataSource as DataTable;
                            DataTable dtex = gcExpenseLedger.DataSource as DataTable;
                        }
                        else
                        {
                            MessageRender.ShowMessage("This is the mandatory to fill any one of the Ledger(s) Amount");
                            isValid = false;
                        }
                    }
                }
            }
            return isValid;
        }

        private bool IsvalidProjectDetails()
        {
            bool isValid = true;
            bool Return = false;
            if (gvProject.GetSelectedRows().Length > 0)
            {
                using (BudgetSystem budgetSystem = new BudgetSystem())
                {
                    string[] GridSelectedProjects = GridselectedProjectIds.Split(',');
                    string[] SelectedProjects = AssignProjectIds.Split(',');

                    if (GridSelectedProjects.Count() > 0)
                    {
                        if (GridSelectedProjects.Length == SelectedProjects.Length)
                        {
                            foreach (string projects in GridSelectedProjects)
                            {
                                string gridproject = projects;
                                if (gridproject != null)
                                {
                                    Return = SelectedProjects.Contains(gridproject);
                                }
                                if (!Return)
                                {
                                    this.ShowMessageBoxWarning("Ledgers are not loaded fully, Click Apply Button");
                                    isValid = false;
                                }
                            }
                        }
                        else
                        {
                            this.ShowMessageBoxWarning("Ledgers are not loaded fully, Click Apply Button");
                            isValid = false;
                        }
                    }
                }
            }
            return isValid;
        }

        private bool ValidateCostCenter()
        {
            bool rtn = true;
            DataTable dtIncomeSource = gcIncomeLedger.DataSource as DataTable;
            DataTable dtExpenseSource = gcExpenseLedger.DataSource as DataTable;

            if (this.AppSetting.EnableCostCentreBudget == 1)
            {
                rtn = ValidateCCAmount(dtIncomeSource, TransSource.Cr.ToString());
                if (rtn)
                {
                    rtn = ValidateCCAmount(dtExpenseSource, TransSource.Dr.ToString());
                }
            }
            return rtn;
        }

        /// <summary>
        /// Check Ledger Amount with its Distribution 
        /// </summary>
        /// <returns></returns>
        private bool ValidateCCAmount(DataTable dtBudgetLeger, string transmode)
        {
            bool rtn = true;
            string msg = (transmode == TransSource.Cr.ToString() ? "Budget Income Ledger" : "Budget Expense Ledger");
            CommonMethod cm = new CommonMethod();
            DataView dv = new DataView(dtBudgetLeger);
            string fldProposed = gccolIncomeCurrentYearProposed.FieldName;
            string fldApproved = gccolIncomeCurrentYearApproved.FieldName;
            if (transmode == TransSource.Cr.ToString())
            {
                fldProposed = gccolexpenseproposedcurrentYR.FieldName;
                fldApproved = gccolexpenseapprovedcurrentYR.FieldName;
            }

            using (BudgetSystem budgetsystem = new BudgetSystem())
            {
                string filter = budgetsystem.AppSchema.Budget.LEDGER_IDColumn.ColumnName + " > 0" +
                                        " AND (" + fldProposed + "> 0 OR " + fldApproved + "> 0)";
                dv.RowFilter = filter;
                foreach (DataRowView drv in dv)
                {
                    Int32 lid = UtilityMember.NumberSet.ToInteger(drv[budgetsystem.AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString());
                    string lname = drv[budgetsystem.AppSchema.BudgetCostCentre.LEDGER_NAMEColumn.ColumnName].ToString();
                    Double proposedLedgeramt = UtilityMember.NumberSet.ToDouble(drv[fldProposed].ToString());
                    Double approvedLedgeramt = UtilityMember.NumberSet.ToDouble(drv[fldApproved].ToString());

                    if (proposedLedgeramt > 0 || approvedLedgeramt > 0)
                    {
                        string filterLedger = budgetsystem.AppSchema.Budget.LEDGER_IDColumn.ColumnName + " = " + lid + " AND " +
                                        budgetsystem.AppSchema.Budget.TRANS_MODEColumn.ColumnName + " = '" + transmode + "'";
                        string filtersumproposed = "SUM(" + budgetsystem.AppSchema.BudgetCostCentre.PROPOSED_AMOUNTColumn.ColumnName + ")";
                        string filtersumapproved = "SUM(" + budgetsystem.AppSchema.BudgetCostCentre.APPROVED_AMOUNTColumn.ColumnName + ")";
                        double ccproposedamt = UtilityMember.NumberSet.ToDouble(dtBudgetCCDetails.Compute(filtersumproposed, filterLedger).ToString());
                        double ccapprovedamt = UtilityMember.NumberSet.ToDouble(dtBudgetCCDetails.Compute(filtersumapproved, filterLedger).ToString());

                        if (ccproposedamt > 0 && proposedLedgeramt > 0 && proposedLedgeramt != ccproposedamt)
                        {
                            this.ShowMessageBox(msg + " '" + lname + "' Proposed Amount not yet fully distributed.");
                            rtn = false;
                            break;
                        }
                        else if (ccapprovedamt > 0 && approvedLedgeramt > 0 && approvedLedgeramt != ccapprovedamt)
                        {
                            this.ShowMessageBox(msg + " '" + lname + "' Approved Amount not yet fully distributed.");
                            rtn = false;
                            break;
                        }
                    }
                }
            }

            return rtn;
        }

        private void ExportBudget()
        {
            DataTable dtIncome;
            DataTable dtExpense;

            DataTable dtCommon = new DataTable();
            try
            {
                SaveFileDialog save = new SaveFileDialog();
                save.FileName = String.Format(this.GetMessage(MessageCatalog.Master.Budget.ANNUAL_BUDGET), UtilityMember.DateSet.ToDate(AppSetting.YearFrom, true).Year);
                save.DefaultExt = ".xlsx";
                save.Filter = "Excel(.xlsx)|*.xlsx";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    dtIncome = gcIncomeLedger.DataSource as DataTable;
                    dtExpense = gcExpenseLedger.DataSource as DataTable;

                    dtCommon = dtIncome;
                    dtCommon.Merge(dtExpense);
                    gcIncomeLedger.DataSource = dtCommon;
                    gcIncomeLedger.ExportToXlsx(save.FileName);
                    if (ShowConfirmationMessage(this.GetMessage(MessageCatalog.Master.Budget.OPEN_THE_FILE), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        System.Diagnostics.Process.Start(save.FileName);
                    }
                    gcIncomeLedger.DataSource = dtIncome;
                }
            }
            catch (IOException)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Budget.FILE_OPEN_ALREADY_CLOSE));
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        private void ExportBudgetNew()
        {

            DataTable dtCommon = new DataTable();
            try
            {
                SaveFileDialog theSaveFileDialog = new SaveFileDialog();
                theSaveFileDialog.Filter = "xlsx files (*.xlsx)|*.xlsx";
                theSaveFileDialog.FilterIndex = 2;
                theSaveFileDialog.RestoreDirectory = true;
                theSaveFileDialog.FileName = "Budget.xlsx";
                if (theSaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    String filename = theSaveFileDialog.FileName;
                    DataTable dtBudgetIncomeLedgers = gcIncomeLedger.DataSource as DataTable;
                    DataTable dtBudgetExpenseLedgers = gcExpenseLedger.DataSource as DataTable;

                    printingSys.Links.Clear();
                    printingSys.PageSettings.LeftMargin = 10;
                    PrintableComponentLink pclIncomeLedgers = new PrintableComponentLink(printingSys);
                    PrintableComponentLink pclExpenseLedgers = new PrintableComponentLink(printingSys);
                    PrintableComponentLink pclBudgetStatistics = new PrintableComponentLink(printingSys);


                    pclIncomeLedgers.RtfReportFooter = Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine +
                                                    "Date:              Signature of the House Council Members";
                    pclExpenseLedgers.RtfReportFooter = "Date:              Signature of the House Council Members";
                    pclBudgetStatistics.RtfReportFooter = " ";

                    pclIncomeLedgers.SetDataObject(dtBudgetIncomeLedgers);
                    pclExpenseLedgers.SetDataObject(dtBudgetExpenseLedgers);
                    pclBudgetStatistics.SetDataObject(dtBudgetStatisticsRecords);

                    CompositeLink complink = new CompositeLink(printingSys);
                    complink.Links.AddRange(new object[] { pclBudgetStatistics, pclIncomeLedgers, pclExpenseLedgers });

                    PageHeaderFooter phf = complink.PageHeaderFooter as PageHeaderFooter;
                    phf.Header.Content.Clear();
                    phf.Header.Content.Add("Don Bosco Center, Yellagiri Hills, Vellore, Don Bosco Center, Yellagiri Hills, Vellore" + Environment.NewLine + "Test" + Environment.NewLine);
                    phf.Header.LineAlignment = BrickAlignment.Near;
                    phf.Footer.LineAlignment = BrickAlignment.Near;

                    complink.CreateDocument();

                    XlsxExportOptions ept = new XlsxExportOptions();

                    ept.ShowGridLines = true;
                    ept.SheetName = "Budget";
                    ept.ExportMode = XlsxExportMode.SingleFile;
                    ept.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Value;

                    complink.ExportToXlsx(filename, ept);
                    if (System.IO.File.Exists(filename))
                    {
                        DialogResult dialogResult = MessageBox.Show("File has been exported to " + filename + " Did you want to open the file now?", "Export...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(filename);
                        }
                    }
                }
            }
            catch (IOException)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Budget.FILE_OPEN_ALREADY_CLOSE));
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        private void RealColumnEditBudgetAmount()
        {
            gccolIncomeCurrentYearProposed.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditTransAmount_EditValueChanged);
            gccolIncomeCurrentYearApproved.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditTransAmount_EditValueChanged);
            this.gvIncomeLedger.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvIncomeLedger.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == gccolIncomeCurrentYearProposed)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvIncomeLedger.ShowEditorByMouse();
                    }));
                }
            };
        }

        private void RealColumnEditBudgetExpenseAmount()
        {
            gccolexpenseproposedcurrentYR.RealColumnEdit.EditValueChanged += new EventHandler(RealColumnEditTransExpenseAmount_EditValueChanged);
            gccolexpenseapprovedcurrentYR.RealColumnEdit.EditValueChanged += new EventHandler(RealColumnEditTransExpenseAmount_EditValueChanged);

            this.gvExpenseLedger.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvExpenseLedger.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == gccolexpenseproposedcurrentYR)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvExpenseLedger.ShowEditorByMouse();
                    }));
                }
            };
        }

        private void RealColumnEditTransExpenseAmount_EditValueChanged(object sender, EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvExpenseLedger.PostEditor();
            gvExpenseLedger.UpdateCurrentRow();
            if (gvExpenseLedger.ActiveEditor == null)
            {
                gvExpenseLedger.ShowEditor();
            }
        }

        private void btnBudgetPreviousImportDetails_Click(object sender, EventArgs e)
        {
            ShowBudgetStatisticsForms();
        }

        private void ShowBudgetStatisticsForms()
        {
            frmBudgetStatisticsDetail frmbudgetDetails = new frmBudgetStatisticsDetail(BudgetId, dtBudgetStatisticsRecords);
            frmbudgetDetails.ShowDialog();
            if (frmbudgetDetails.DialogResult == DialogResult.OK)
            {
                dtBudgetStatisticsRecords = frmbudgetDetails.BudgetStatisticsDetails;
            }
        }

        private void FetchUpdateBudgetStatisticDetails()
        {
            ResultArgs resultarg = BindBudgetStatisticsDetails();
            if (resultarg.Success)
            {
                dtBudgetStatisticsRecords = resultarg.DataSource.Table;
            }
        }

        private ResultArgs BindBudgetStatisticsDetails()
        {
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                using (BudgetSystem budgetSystem = new BudgetSystem())
                {
                    resultArgs = budgetSystem.FetchBudgetDetailsByStatistics(BudgetId);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }

            return resultArgs;
        }

        private void peImportExcelData_Click(object sender, EventArgs e)
        {
            DataView dvBudgetIncomeLedger = (DataView)gvIncomeLedger.DataSource;
            DataView dvBudgetExpenseLedger = (DataView)gvExpenseLedger.DataSource;
            try
            {
                if (dvBudgetIncomeLedger != null && dvBudgetExpenseLedger != null)
                {
                    OpenFileDialog file = new OpenFileDialog() { Filter = "Excel Files|*.xls;*.xlsx" };
                    file.InitialDirectory = SettingProperty.ApplicationStartUpPath;
                    if (DialogResult.OK == file.ShowDialog())
                    {
                        string filepath = file.FileName;
                        if (!string.IsNullOrEmpty(file.FileName) && File.Exists(filepath))
                        {
                            //F2 : Leger Name, F9: Approved Amount
                            ResultArgs resultarg = Bosco.Utility.Common.clsGeneral.ExcelToDataTable(filepath, "Sheet");
                            if (resultarg.Success && resultarg.DataSource.Table != null)
                            {
                                if (this.ShowConfirmationMessage("Are you sure to update Budget Ledger's approved amount from Excel File ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                                {
                                    this.Cursor = Cursors.WaitCursor;

                                    DataTable dtBudgetLedgers = resultarg.DataSource.Table;
                                    string ledgercolumnname = GetExcelColumn("Ledger Name", dtBudgetLedgers);
                                    string approvedcolumnname = GetExcelColumn("Approved Income", dtBudgetLedgers);
                                    string narrationcolumnname = GetExcelColumn("Narration", dtBudgetLedgers); ;

                                    if (!string.IsNullOrEmpty(ledgercolumnname) && !string.IsNullOrEmpty(approvedcolumnname) && !string.IsNullOrEmpty(narrationcolumnname))
                                    {
                                        dtBudgetLedgers.DefaultView.RowFilter = ledgercolumnname + " <> ''";
                                        DataTable dtBudgetApprovedLedgers = dtBudgetLedgers.DefaultView.ToTable(false, new string[] { ledgercolumnname, approvedcolumnname, narrationcolumnname });
                                        bool IsbudgetIncomeLedger = false;
                                        foreach (DataRow drApprovedLedger in dtBudgetApprovedLedgers.Rows)
                                        {
                                            string ledgername = drApprovedLedger[ledgercolumnname].ToString().Trim();
                                            string approvedamount = drApprovedLedger[approvedcolumnname].ToString().Trim();
                                            string narration = drApprovedLedger[narrationcolumnname].ToString().Trim();

                                            if (narration.Length > 500)
                                                narration = narration.Substring(0, 500);

                                            if (ledgername.ToUpper() == "LEDGER NAME" && approvedamount.ToUpper() == "APPROVED INCOME")
                                            {
                                                IsbudgetIncomeLedger = true;
                                            }
                                            else if (ledgername.ToUpper() == "LEDGER NAME" && approvedamount.ToUpper() == "APPROVED EXPENDITURE")
                                            {
                                                IsbudgetIncomeLedger = false;
                                            }
                                            else
                                            {
                                                Int32 BudgetLedgerId = GetLedgerId(ledgername);
                                                if (IsbudgetIncomeLedger)
                                                {
                                                    dvBudgetIncomeLedger.RowFilter = string.Empty;
                                                    dvBudgetIncomeLedger.RowFilter = "LEDGER_ID = " + BudgetLedgerId;
                                                    if (dvBudgetIncomeLedger.Count > 0)
                                                    {
                                                        dvBudgetIncomeLedger[0][APPROVED_CURRENT_YR] = UtilityMember.NumberSet.ToDecimal(approvedamount);
                                                        dvBudgetIncomeLedger[0][NARRATION] = narration;
                                                        dvBudgetIncomeLedger.Table.AcceptChanges();
                                                    }
                                                    else if (AppSetting.ENABLE_BUDGET_HO_HELP_AMOUNT &&  //On 29/06/2019, to get Ho Help amount
                                                        dvBudgetIncomeLedger.Count == 0 && ledgername.Trim().ToUpper() == AppSetting.BUDGET_HO_HELP_AMOUNT_CAPTION.Trim().ToUpper())
                                                    {
                                                        txtHOHelpApprovedAmount.Text = UtilityMember.NumberSet.ToDecimal(approvedamount).ToString();
                                                    }
                                                    dvBudgetIncomeLedger.RowFilter = string.Empty;
                                                }
                                                else
                                                {
                                                    dvBudgetExpenseLedger.RowFilter = string.Empty;
                                                    dvBudgetExpenseLedger.RowFilter = "LEDGER_ID = " + BudgetLedgerId;
                                                    if (dvBudgetExpenseLedger.Count > 0)
                                                    {
                                                        dvBudgetExpenseLedger[0][APPROVED_CURRENT_YR] = UtilityMember.NumberSet.ToDecimal(approvedamount);
                                                        dvBudgetExpenseLedger[0][NARRATION] = narration;
                                                        dvBudgetExpenseLedger.Table.AcceptChanges();
                                                    }
                                                    dvBudgetExpenseLedger.RowFilter = string.Empty;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageRender.ShowMessage("Could not find Ledger Name/Approved amount column in selected excel file");
                                    }
                                }
                            }
                            else
                            {
                                MessageRender.ShowMessage(resultarg.Message);
                            }
                        }
                        else
                        {
                            MessageRender.ShowMessage("Budget file is not available");
                        }
                    }
                }
                else
                {
                    MessageRender.ShowMessage("Budget is not available");
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
            finally
            {
                if (dvBudgetIncomeLedger != null && dvBudgetExpenseLedger != null)
                {
                    dvBudgetIncomeLedger.RowFilter = string.Empty;
                    dvBudgetExpenseLedger.RowFilter = string.Empty;
                }
            }
            this.Cursor = Cursors.Default;
        }

        private string GetExcelColumn(string columnname, DataTable dtExcel)
        {
            string Rtn = string.Empty;
            string PrevRtn = string.Empty;
            dtExcel.DefaultView.RowFilter = string.Empty;

            try
            {
                foreach (DataRow dr in dtExcel.Rows)
                {
                    foreach (DataColumn dc in dtExcel.Columns)
                    {
                        if (dr[dc].ToString() == columnname)
                        {
                            Rtn = dc.Caption;
                            break;
                        }
                    }
                    if (!String.IsNullOrEmpty(Rtn))
                    {
                        break;
                    }
                }
            }
            catch (Exception err)
            {
                dtExcel.DefaultView.RowFilter = string.Empty;
                Rtn = string.Empty;
            }
            return Rtn;
        }

        private Int32 GetLedgerId(string ledgername)
        {
            Int32 Rtn = 0;
            using (LedgerSystem ledgersys = new LedgerSystem())
            {
                ResultArgs resultarg = ledgersys.FetchLedgerIdByLedgerName(ledgername);
                if (resultarg.Success)
                {
                    if (resultarg.DataSource.Sclar.ToInteger > 0)
                    {
                        Rtn = resultarg.DataSource.Sclar.ToInteger;
                    }
                }
            }
            return Rtn;
        }

        private Int32 isExistsMonthlyBudgetDistribution()
        {
            Int32 Rtn = 0;
            using (BudgetSystem budgetsystem = new BudgetSystem())
            {
                resultArgs = budgetsystem.FetchBudgetExists(BudgetId);
                if (resultArgs.Success)
                {
                    if (resultArgs.DataSource.Sclar.ToInteger > 0)
                    {
                        Rtn = resultArgs.DataSource.Sclar.ToInteger;
                    }
                }
            }
            return Rtn;
        }

        private void btnExportBudget_Click(object sender, EventArgs e)
        {
            if (this.ShowConfirmationMessage("Are you sure to export Budget to Head Office Portal ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                this.ShowWaitDialog("Uploading Budget from Head Office Portal");
                ResultArgs result = this.UploadBudgetToAcmeerpPortal(BudgetId, true);
                this.CloseWaitDialog();
                if (result.Success)
                {
                    //On 22/03/2021, Show Budget action
                    lcBudgetAction.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    lblBudgetAction.Text = BudgetAction.Recommended.ToString();

                    if (UpdateHeld != null)
                        UpdateHeld(this, e);

                    this.ShowMessageBox("Budget has been uploaded to Head Office Portal, It will be approved by Head Office");
                }
                else
                {
                    this.CloseWaitDialog();
                    this.ShowMessageBoxError(result.Message);
                }
            }
        }

        private void btnImportBudget_Click(object sender, EventArgs e)
        {
            if (this.ShowConfirmationMessage("Are you sure to import Budget from Head Office Portal ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                this.ShowWaitDialog("Downloading Budget from Head Office Portal");
                ResultArgs result = this.GetApprovedBudgetFromAcmeerpPortal(BudgetId);
                this.CloseWaitDialog();
                if (result.Success)
                {
                    this.ShowMessageBox("Budget has been approved by Head Office Portal");
                    if (gvProject.GetSelectedRows().Length > 0)
                    {
                        AssignValues();
                        AssignProjectIds = GridselectedProjectIds;
                        GetLastMonthProperty();
                    }

                    if (lblBudgetAction.Text.ToUpper() == BudgetAction.Approved.ToString().ToUpper())
                    {
                        colIncomeHONarration.Visible = true;
                        colExeHONarration.Visible = true;
                    }

                    if (UpdateHeld != null)
                        UpdateHeld(this, e);
                }
                else
                {
                    this.CloseWaitDialog();
                    this.ShowMessageBoxError(result.Message);
                }
            }
        }

        #endregion

        private void btnDevProjects_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ProjectIds))
            {
                frmBudgetDevProjectsDetails frmbugetdev = new frmBudgetDevProjectsDetails(BudgetId, ProjectIds, GridselectedProjects, dePeriodFrom.DateTime,
                    dePeriodTo.DateTime, dtBudgetDevelopmentalNewProjects, dtBudgetDevelopmentalNewProjectsCCDetails);
                frmbugetdev.ShowDialog();
                if (frmbugetdev.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    if (frmbugetdev.ReturnValue != null)
                    {
                        DataSet dsDevelopmentalProjectsDetails = new DataSet();
                        dsDevelopmentalProjectsDetails = frmbugetdev.ReturnValue as DataSet;
                        if (dsDevelopmentalProjectsDetails.Tables.Count == 2)
                        {
                            dtBudgetDevelopmentalNewProjects = dsDevelopmentalProjectsDetails.Tables[0];
                            dtBudgetDevelopmentalNewProjectsCCDetails = dsDevelopmentalProjectsDetails.Tables[1];
                        }
                    }
                }
            }
            else
            {
                this.ShowMessageBox("Select Project");
            }
        }

        private void glkpProject_QueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //19/07/2021, To set Popup widow size
            if (sender != null)
            {
                GridLookUpEdit editor = (GridLookUpEdit)sender;
                SetGridLookPopupWindowSize(editor);
            }
        }

        private void btnMergeAllCommuntities_Click(object sender, EventArgs e)
        {
            Int32 noofFiles = 0;
            Int32 failedFiles = 0;
            bool IsbudgetIncomeLedger = true;
            string folder = @"C:\BUDGET 2022 for Tabulation";
            string supportedExtensions = "*.xlsx,*.xls";
            string str = "";
            ResultArgs resultarg = new ResultArgs();
            try
            {
                using (BudgetSystem budsystem = new BudgetSystem())
                {
                    resultarg = budsystem.DeleteAllCommunity();
                }
                if (resultarg.Success)
                {
                    foreach (string xlfile in Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories).Where(s => supportedExtensions.Contains(Path.GetExtension(s).ToLower())))
                    {
                        if (File.Exists(xlfile))
                        {
                            //F2 : Leger Name, F9: Approved Amount
                            resultarg = Bosco.Utility.Common.clsGeneral.ExcelToDataTable(xlfile, "Sheet");
                            if (resultarg.Success && resultarg.DataSource.Table != null)
                            {
                                this.Cursor = Cursors.WaitCursor;

                                DataTable dtBudgetLedgers = resultarg.DataSource.Table;
                                string ledgercolumnname = GetExcelColumn("Ledger Name", dtBudgetLedgers);
                                string approvedcolumnname = GetExcelColumn("Approved Income", dtBudgetLedgers);
                                string narrationcolumnname = GetExcelColumn("Narration", dtBudgetLedgers);

                                string Proposed = "F10";
                                string PrevApproved = "F7";
                                string PrevRealized = "F8";

                                if (!string.IsNullOrEmpty(ledgercolumnname) && !string.IsNullOrEmpty(approvedcolumnname) && !string.IsNullOrEmpty(narrationcolumnname))
                                {
                                    dtBudgetLedgers.DefaultView.RowFilter = ledgercolumnname + " <> ''";
                                    DataTable dtBudgetApprovedLedgers = dtBudgetLedgers.DefaultView.ToTable(false, new string[] { ledgercolumnname, PrevApproved, PrevRealized, Proposed, approvedcolumnname, narrationcolumnname });
                                    foreach (DataRow drApprovedLedger in dtBudgetApprovedLedgers.Rows)
                                    {
                                        string ledgername = drApprovedLedger[ledgercolumnname].ToString().Trim();
                                        string approvedamount = drApprovedLedger[approvedcolumnname].ToString().Trim();
                                        string proposedamount = drApprovedLedger[Proposed].ToString().Trim();
                                        string prevapprovedamount = drApprovedLedger[PrevApproved].ToString().Trim();
                                        string prevactual = drApprovedLedger[PrevRealized].ToString().Trim();

                                        string narration = drApprovedLedger[narrationcolumnname].ToString().Trim();

                                        if (narration.Length > 500)
                                            narration = narration.Substring(0, 500);

                                        if (ledgername.ToUpper() == "LEDGER NAME" && approvedamount.ToUpper() == "APPROVED INCOME")
                                        {
                                            IsbudgetIncomeLedger = true;
                                        }
                                        else if (ledgername.ToUpper() == "LEDGER NAME" && approvedamount.ToUpper() == "APPROVED EXPENDITURE")
                                        {
                                            IsbudgetIncomeLedger = false;
                                        }
                                        bool iscashbankfd = false;
                                        if (ledgername.ToUpper() == "LEDGER NAME" || ledgername.ToUpper() == "CORPUS FUND" ||
                                            ledgername.ToUpper() == "CASH" || ledgername.ToUpper() == "FIXED DEPOSIT" || (char.IsDigit(ledgername.First())))
                                        {
                                            iscashbankfd = true;
                                        }
                                        /*if (!IsbudgetIncomeLedger)
                                        {
                                            if (ledgername.ToUpper() == "CASH" || ledgername.ToUpper() == "Fixed Deposit")
                                            {
                                                ledgername += " -  CL";
                                            }
                                            else
                                            {
                                                if (char.IsDigit(ledgername.First()))
                                                {
                                                    ledgername += " -  CL";
                                                }
                                            }
                                        }*/

                                        if (!iscashbankfd)
                                        {
                                            string budgettransmode = IsbudgetIncomeLedger ? TransactionMode.CR.ToString() : TransactionMode.DR.ToString();
                                            using (BudgetSystem budsystem = new BudgetSystem())
                                            {
                                                resultarg = budsystem.SaveAllCommunity(ledgername, UtilityMember.NumberSet.ToDouble(prevapprovedamount), UtilityMember.NumberSet.ToDouble(prevactual),
                                                                UtilityMember.NumberSet.ToDouble(proposedamount), UtilityMember.NumberSet.ToDouble(approvedamount), budgettransmode);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    MessageRender.ShowMessage("Could not find Ledger Name/Approved amount column in selected excel file");
                                }
                            }
                            else
                            {
                                failedFiles++;
                                MessageRender.ShowMessage(resultarg.Message + " (" + xlfile + ")");
                            }
                        }
                        else
                        {
                            failedFiles++;
                            MessageRender.ShowMessage("Budget file is not available " + " (" + xlfile + ")");
                        }

                        noofFiles = noofFiles + 1;
                    }

                }
                else
                {
                    MessageRender.ShowMessage(resultarg.Message);
                }

                this.ShowMessageBox(noofFiles.ToString() + " file(s) have been processed. " + failedFiles + " file(s) are failed");
                this.Cursor = Cursors.Default;
            }
            catch (Exception err)
            {
                this.ShowMessageBox(err.Message);
            }
        }

        private void btnStrengthDetails_Click(object sender, EventArgs e)
        {
            //this.ShowMessageBox("In Process");
            if (this.AppSetting.IncludeBudgetCCStrengthDetails == 1)
            {
                if (!string.IsNullOrEmpty(ProjectIds))
                {
                    frmBudgetStrengthDetail frmbudgetstrengthdetails = new frmBudgetStrengthDetail(BudgetId, GridselectedProjectIds, dtBudgetStrengthRecords);
                    frmbudgetstrengthdetails.ShowDialog();
                    if (frmbudgetstrengthdetails.DialogResult == DialogResult.OK)
                    {
                        if (frmbudgetstrengthdetails.ReturnValue != null)
                        {
                            dtBudgetStrengthRecords = frmbudgetstrengthdetails.ReturnValue as DataTable;
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox("Select Project");
                }
            }
        }

        /// <summary>
        /// on 08/10/2024, TO link unliked budget new projects  (Which are already exits and chnged to Report)
        /// </summary>
        private void LoadUnLinkedNewProjects()
        {
            //08/10/2024, TO link unliked budget new projects 
            //For Temp ----------------------------------------------------------------------------------
            lcLinkNewProjects.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcLink.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (this.BudgetId > 0 && AppSetting.CreateBudgetDevNewProjects == 1 && this.AppSetting.IS_BSG_CONGREGATION)
            {
                if (dtBudgetDevelopmentalNewProjects != null && dtBudgetDevelopmentalNewProjects.Rows.Count == 0)
                {
                    using (BudgetSystem budgetsystem = new BudgetSystem())
                    {
                        ResultArgs result = budgetsystem.FetchUnLinkedNewProjects();
                        if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                        {
                            DataTable dt = result.DataSource.Table;
                            lcLinkNewProjects.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            lcLink.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            UtilityMember.ComboSet.BindGridLookUpCombo(glkpNewProject, dt, budgetsystem.AppSchema.ReportNewBudgetProject.NEW_PROJECTColumn.ColumnName,
                                                    budgetsystem.AppSchema.Budget.BUDGET_IDColumn.ColumnName);
                        }
                    }
                }
            }
            //-----------------------------------------------------------------------------------------------
        }

        private void btnLink_Click(object sender, EventArgs e)
        {
            Int32 oldbudgetid = (glkpNewProject.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkpNewProject.EditValue.ToString()) : 0);
            if (this.BudgetId > 0 && oldbudgetid > 0)
            {
                if (oldbudgetid > 0 && MessageRender.ShowConfirmationMessage("Are you sure to link selected New Project(s) with current Budget ?",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                {

                    using (BudgetSystem budgetsystem = new BudgetSystem())
                    {
                        this.ShowWaitDialog();
                        ResultArgs result = budgetsystem.UpdateUnLinkedNewProjects(oldbudgetid, this.BudgetId);
                        if (result.Success && result.RowsAffected > 0)
                        {
                            AssignValues();
                        }
                        this.CloseWaitDialog();
                    }
                }
            }
        }

        private void btnMap_Click(object sender, EventArgs e)
        {

        }

    }
}
