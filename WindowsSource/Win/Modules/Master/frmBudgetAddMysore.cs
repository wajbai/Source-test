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
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.EditForm.Helpers.Controls;
using DevExpress.XtraGrid.EditForm.Helpers;
using DevExpress.XtraGrid.Localization;
using DevExpress.Utils.Drawing;

namespace ACPP.Modules.Master
{
    public partial class frmAddAnnualBudgetMysore : frmFinanceBaseAdd
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
        int RecLedgersSerialNo = 1;
        int RecAlphabetSerialNo = 0;
       // private string[] FixedRecAlphabetLedgers = { "Net Amount", "Professional Tax", "LIC" };
        private string[] FixedRecAlphabetLedgers = { "Net Amount", "Professional Tax", "LIC" };
        private string[] FixedRecESICMainLedgers = { "Management ESIC", "Employee ESIC" };
        #endregion

        #region Properties
        private int Month1BudgetId { get; set; }
        private int Month2BudgetId { get; set; }

        private DateTime Month1DateFrom { get; set; }
        private DateTime Month1DateTo { get; set; }

        private DateTime Month2DateFrom { get; set; }
        private DateTime Month2DateTo { get; set; }

        public int ProjectId { get; set; }
        public string ProjectIds { get; set; }
        public string BudgetTypeId { get; set; }
        private DateTime PeriodFrom { get; set; }
        private DataTable dtRecBudgetSubwithLedgerRecords = new DataTable();
        private DataTable dtNonRecBudgetSubwithLedgerRecords = new DataTable();
        private DataTable dtBudgetStatisticsRecords = new DataTable();

        private bool IsTwoMonthBudget = false;
        #endregion

        #region Constructor

        public frmAddAnnualBudgetMysore()
        {
            InitializeComponent();

            RealColumnEditBudgetAmount();
            RealColumnEditBudgetExpenseAmount();
        }

        public frmAddAnnualBudgetMysore(int BudgetId, int month2budgetId, int projectid, bool isTwoMonthBudget)
            : this()
        {
            this.Month1BudgetId = BudgetId;
            this.Month2BudgetId = month2budgetId;
            this.ProjectId = projectid;
            this.IsTwoMonthBudget = isTwoMonthBudget;

        }
        #endregion

        #region Events
        private void frmAnnualBudget_Load(object sender, EventArgs e)
        {
            LoadDefaults();

            gvExpenseLedger.OptionsEditForm.ActionOnModifiedRowChange = EditFormModifiedAction.Nothing;

            //1. Hide Import and Export Button Always 
            lcImportBudget.Visibility = lcExportBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            //For ABE and Mysore
            //4. Show Import and Export Button only if ABE or Mysore
            if (Month1BudgetId > 0)
            {
                lcImportBudget.Visibility = lcExportBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }

            //gccolexpenseapprovedcurrentYR.OptionsColumn.ReadOnly = (((this.AppSetting.IS_ABEBEN_DIOCESE) || (this.AppSetting.IS_DIOMYS_DIOCESE)) ? true : false);
            colNRCurrentApproved.OptionsColumn.ReadOnly = (((this.AppSetting.IS_DIOMYS_DIOCESE)) ? true : false);
            colNRCurrentApproved.OptionsColumn.AllowEdit = !colNRCurrentApproved.OptionsColumn.ReadOnly;

            int Ids = isExistsMonthlyBudgetDistribution();
            colNRProposed.OptionsColumn.ReadOnly = Ids != 0 ? true : false;
            colNRProposed.OptionsColumn.AllowEdit = !colNRProposed.OptionsColumn.ReadOnly;

            // gccolIncomeCurrentYearApproved.OptionsColumn.ReadOnly = (((this.AppSetting.IS_ABEBEN_DIOCESE) || (this.AppSetting.IS_DIOMYS_DIOCESE)) ? true : false);
            colRecCurrentApproved.OptionsColumn.ReadOnly = (((this.AppSetting.IS_DIOMYS_DIOCESE)) ? true : false);
            colRecCurrentApproved.OptionsColumn.AllowEdit = !colRecCurrentApproved.OptionsColumn.ReadOnly;
        }

        void editform_CloseEditForm(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AssignValues()
        {
            if (Month1BudgetId > 0)
                SetBudgetEdit();
            else
            {
                using (BudgetSystem budgetSystem = new BudgetSystem())
                {
                    budgetSystem.MultipleProjectId = ProjectId.ToString();
                    budgetSystem.BudgetTypeId = 5;  //UtilityMember.NumberSet.ToInteger(glkpBudgetType.EditValue.ToString());
                    budgetSystem.DateFrom = dePeriodFrom.DateTime.ToString();
                    budgetSystem.DateTo = dePeriodTo.DateTime.ToString();

                    //resultArgs = budgetSystem.GetAnnualBudget();
                    //int Id = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["PROJECT_ID"].ToString());
                    //string ProjectNames = resultArgs.DataSource.Table.Rows[0]["PROJECT"].ToString();

                    LoadLedger(rglkpLedgerdetails);

                    int BudId = ValidateBudgetGrid();

                    if (BudId == 0)
                    {
                        FetchBudgetDetails();
                    }
                    else
                    {
                        gcRecuringGridSource.DataSource = gcNonRecuringGridSource.DataSource = null;
                        if (ShowConfirmationMessage("Budget is already made for this this month. Are you sure to show Not-Budgeted Month?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            if (Month1BudgetId == 0)
                                ClearControls();
                            GetLastMonthProperty();
                            FetchBudgetDetails();
                        }
                    }
                }
            }
        }

        private int ValidateBudgetGrid()
        {
            int rtn = 0;
            using (BudgetSystem system = new BudgetSystem())
            {
                system.MultipleProjectId = ProjectId.ToString();
                system.BudgetTypeId = 5;
                system.DateFrom = dePeriodFrom.DateTime.ToString();
                system.DateTo = dePeriodTo.DateTime.ToString();
                rtn = system.GetBudgetIdByDateRangeProjectd(dePeriodFrom.DateTime, dePeriodTo.DateTime, ProjectId.ToString());
            }
            return rtn;
        }

        private void LoadLedger(RepositoryItemGridLookUpEdit glkpLedger)
        {
            try
            {
                using (BudgetSystem BudgetSystem = new BudgetSystem())
                {
                    BudgetSystem.MultipleProjectId = ProjectId.ToString();
                    BudgetSystem.ProjectId = ProjectId;
                    resultArgs = BudgetSystem.FetchLedgerDetails();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        glkpLedger.DisplayMember = "LEDGER_NAME";
                        glkpLedger.ValueMember = "BIND_LEDGER_ID";

                        glkpLedger.DataSource = resultArgs.DataSource.Table;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void GetLastMonthProperty()
        {
            if ((int)BudgetType.BudgetMonth == (int)BudgetType.BudgetMonth)
            {
                if (Month1BudgetId == 0)
                {
                    using (BudgetSystem budgetsystem = new BudgetSystem())
                    {
                        budgetsystem.MultipleProjectId = ProjectId.ToString();
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

        private void meNote_Leave(object sender, EventArgs e)
        {
            btnSave.Select();
            btnSave.Focus();
        }

        private void gcAnnualBudget_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control &&
                (gvIncomeLedger.FocusedColumn == colRecProposed))
            {
                gvIncomeLedger.PostEditor();
                gvIncomeLedger.UpdateCurrentRow();
            }
        }

        private void gvAnnualBudget_CustomDrawRowFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            if (e.Column == colRecLedger)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsFormValid())
            {
                // create the table 
                DataTable dtUpdateBudgetAllLedgers = new DataTable();
                DataTable dtUpdateBudgetLedgers = new DataTable();
                DataTable dtUpdateBudgetSubLedgers = new DataTable();

                // Get the grid Source
                DataTable dtRecuring = gcRecuringGridSource.DataSource as DataTable;
                DataTable dtNonRecuring = gcNonRecuringGridSource.DataSource as DataTable;

                // Merge the Ledger details
                dtUpdateBudgetAllLedgers = dtRecuring.Copy();
                dtUpdateBudgetAllLedgers.Merge(dtNonRecuring);


                // To ledger the Records

                DataView dvRecords = new DataView(dtUpdateBudgetAllLedgers);
                if (IsTwoMonthBudget)
                {
                    dvRecords.RowFilter = "(PROPOSED_CURRENT_YR>0 OR APPROVED_CURRENT_YR>0) AND (M2_PROPOSED_AMOUNT>0 OR M2_APPROVED_AMOUNT>0)";
                }
                else
                {
                    dvRecords.RowFilter = "PROPOSED_CURRENT_YR>0 OR APPROVED_CURRENT_YR>0";
                }

                dtUpdateBudgetAllLedgers = dvRecords.ToTable();

                if (dtUpdateBudgetAllLedgers != null && dtUpdateBudgetAllLedgers.Rows.Count > 0)
                {
                    using (BudgetSystem budgetSystem = new BudgetSystem())
                    {
                        //Set Master Details
                        //1. Set Budget Ledgers and Assign to system
                        dtUpdateBudgetAllLedgers.DefaultView.RowFilter = "LEDGER_ID >0 AND IS_SUB_LEDGER=0";
                        dtUpdateBudgetLedgers = dtUpdateBudgetAllLedgers.DefaultView.ToTable();
                        dtUpdateBudgetAllLedgers.DefaultView.RowFilter = string.Empty;
                        budgetSystem.dtBudgetLedgers = dtUpdateBudgetLedgers;

                        //2. set Budget Sub Ledgers and Assign to system
                        dtUpdateBudgetAllLedgers.DefaultView.RowFilter = "LEDGER_ID >0 AND IS_SUB_LEDGER=1";
                        dtUpdateBudgetLedgers = dtUpdateBudgetAllLedgers.DefaultView.ToTable();
                        dtUpdateBudgetAllLedgers.DefaultView.RowFilter = string.Empty;
                        budgetSystem.dtBudgetSubLedgers = dtUpdateBudgetLedgers;
                        
                        budgetSystem.BudgetId = budgetSystem.M1BudgetId = Month1BudgetId;
                        if (IsTwoMonthBudget)
                        {
                            budgetSystem.M2BudgetId = Month2BudgetId;
                            budgetSystem.NextDateFrom = Month2DateFrom.ToShortDateString(); ;
                            budgetSystem.NextDateTo = Month2DateTo.ToShortDateString(); ;
                        }
                        budgetSystem.IsTwoMonthBudget = IsTwoMonthBudget;
                        budgetSystem.BudgetName = txtBudgetName.Text;
                        budgetSystem.DateFrom = dePeriodFrom.DateTime.ToShortDateString();
                        budgetSystem.DateTo = dePeriodTo.DateTime.ToShortDateString();
                        
                        budgetSystem.BudgetTypeId = 5;  //UtilityMember.NumberSet.ToInteger(glkpBudgetType.EditValue.ToString());
                        budgetSystem.ProjectId = UtilityMember.NumberSet.ToInteger(grdProject.EditValue.ToString());
                        budgetSystem.BudgetLevelId = 1;
                        budgetSystem.monthwiseDistribution = 0;
                        budgetSystem.HOHelpPropsedAmount = 0;
                        budgetSystem.HOHelpApprovedAmount = 0;
                        budgetSystem.Status = 1;

                        this.ShowWaitDialog();

                        resultArgs = budgetSystem.SaveAnnualBudgetForMysore();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            ShowSuccessMessage(GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            if (UpdateHeld != null)
                                UpdateHeld(this, e);
                            if (Month1BudgetId == 0)
                            {
                                Month1BudgetId = UtilityMember.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                lcImportBudget.Visibility = lcExportBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            }
                        }
                        this.CloseWaitDialog();
                    }
                }
                else
                {
                    if (IsTwoMonthBudget)
                    {
                        ShowMessageBox("Fill Proposed Amount for any one of the Ledgers for both Months");
                    }
                    else
                    {
                        ShowMessageBox("Fill Proposed Amount for any one of the Ledgers");
                    }
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
                this.SetFocusRowFilter(gvIncomeLedger, colRecLedger);
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
                Percentage = gvIncomeLedger.GetRowCellValue(gvIncomeLedger.FocusedRowHandle, colRecProposed) != null ? UtilityMember.NumberSet.ToInteger(gvIncomeLedger.GetRowCellValue(gvIncomeLedger.FocusedRowHandle, colRecProposed).ToString()) : 0;
                ActualAmount = gvIncomeLedger.GetRowCellValue(gvIncomeLedger.FocusedRowHandle, colRecActual) != null ? UtilityMember.NumberSet.ToDouble(gvIncomeLedger.GetRowCellValue(gvIncomeLedger.FocusedRowHandle, colRecActual).ToString()) : 0;
                if ((e.KeyCode == Keys.D5) && e.Shift)
                {
                    CalculatedAmount = (ActualAmount * Percentage) / 100;
                    gvIncomeLedger.SetFocusedRowCellValue(colRecProposed, CalculatedAmount);
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
                dePeriodFrom.Properties.MinValue = UtilityMember.DateSet.ToDate(setting.YearFrom, false);
                dePeriodTo.Properties.MaxValue = UtilityMember.DateSet.ToDate(setting.YearTo, false);
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

            lblPeriodFrom.Visibility = lblPeriodTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            SetTitle();
        }

        private void dePeriodFrom_EditValueChanged(object sender, EventArgs e)
        {
            //chinna 12.03.2019
            if (Month1BudgetId == 0)
            {
                DateTime date = new DateTime(dePeriodFrom.DateTime.Year, dePeriodFrom.DateTime.Month, dePeriodFrom.DateTime.Day);
                dePeriodTo.DateTime = date.AddMonths(12).AddDays(-1);
            }
            //On 12/07/2018, For closed Projects----
            //LoadProjectDetails(sender);
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

        private void peReplaceValue_Click(object sender, EventArgs e)
        {
            if (gcRecuringGridSource.DataSource != null && gcRecuringGridSource.DataSource != null)
            {
                if (ShowConfirmationMessage("Are you sure to assign Proposed Amount to Approved Amount ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    //All other Ledgers
                    DataTable dtIncomeSource = gcRecuringGridSource.DataSource as DataTable;
                    foreach (DataRow drIncome in dtIncomeSource.Rows)
                    {
                        drIncome[APPROVED_CURRENT_YR] = this.UtilityMember.NumberSet.ToDecimal(drIncome[PROPOSED_CURRENT_YR].ToString());
                    }
                    gcRecuringGridSource.DataSource = dtIncomeSource;

                    DataTable dtExpenseSource = gcNonRecuringGridSource.DataSource as DataTable;
                    foreach (DataRow drExpense in dtExpenseSource.Rows)
                    {
                        drExpense[APPROVED_CURRENT_YR] = this.UtilityMember.NumberSet.ToDecimal(drExpense[PROPOSED_CURRENT_YR].ToString());
                    }
                    gcNonRecuringGridSource.DataSource = dtExpenseSource;
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
                Percentage = gvIncomeLedger.GetRowCellValue(gvIncomeLedger.FocusedRowHandle, colRecProposed) != null ? UtilityMember.NumberSet.ToInteger(gvIncomeLedger.GetRowCellValue(gvIncomeLedger.FocusedRowHandle, colRecProposed).ToString()) : 0;
                ActualAmount = gvIncomeLedger.GetRowCellValue(gvIncomeLedger.FocusedRowHandle, colRecActual) != null ? UtilityMember.NumberSet.ToDouble(gvIncomeLedger.GetRowCellValue(gvIncomeLedger.FocusedRowHandle, colRecActual).ToString()) : 0;
                if ((e.KeyCode == Keys.D5) && e.Shift)
                {
                    CalculatedAmount = (ActualAmount * Percentage) / 100;
                    gvIncomeLedger.SetFocusedRowCellValue(colRecProposed, CalculatedAmount);
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
                Percentage = gvExpenseLedger.GetRowCellValue(gvExpenseLedger.FocusedRowHandle, colNRProposed) != null ? UtilityMember.NumberSet.ToInteger(gvExpenseLedger.GetRowCellValue(gvExpenseLedger.FocusedRowHandle, colNRProposed).ToString()) : 0;
                ActualAmount = gvExpenseLedger.GetRowCellValue(gvExpenseLedger.FocusedRowHandle, colNRActual) != null ? UtilityMember.NumberSet.ToDouble(gvExpenseLedger.GetRowCellValue(gvExpenseLedger.FocusedRowHandle, colNRActual).ToString()) : 0;
                if ((e.KeyCode == Keys.D5) && e.Shift)
                {
                    CalculatedAmount = (ActualAmount * Percentage) / 100;
                    gvExpenseLedger.SetFocusedRowCellValue(colNRProposed, CalculatedAmount);
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
                (gvIncomeLedger.FocusedColumn == colRecProposed || gvIncomeLedger.FocusedColumn == colRecNarration))
            {
                gvIncomeLedger.PostEditor();
                gvIncomeLedger.UpdateCurrentRow();

                if (gvIncomeLedger.FocusedColumn == colRecNarration && gvIncomeLedger.IsLastRow)
                {
                    FoucsNonRecGrid(0);
                }

            }
        }

        private void gcExpenseLedger_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control &&
                (gvExpenseLedger.FocusedColumn == colNRProposed || gvExpenseLedger.FocusedColumn == colNRNarration))
            {
                gvExpenseLedger.PostEditor();
                gvExpenseLedger.UpdateCurrentRow();

                if (gvExpenseLedger.FocusedColumn == colNRNarration)
                {
                    if (gvExpenseLedger.IsLastRow)
                    {
                        btnSave.Select();
                        btnSave.Focus();
                    }
                    else
                    {
                        gvExpenseLedger.MoveNext();
                        gvExpenseLedger.FocusedColumn = colNRProposed;
                        e.Handled = true;
                    }
                }
            }
            else if (e.Shift && e.KeyCode == Keys.Tab && gvExpenseLedger.FocusedColumn == colNRProposed)
            {
                gvExpenseLedger.PostEditor();
                gvExpenseLedger.UpdateCurrentRow();
                if (gvExpenseLedger.IsFirstRow && e.Shift && e.KeyCode == Keys.Tab)
                {
                    DataTable dtRC = gcRecuringGridSource.DataSource as DataTable;
                    if (dtRC != null)
                    {
                        FoucsRecGrid(dtRC.Rows.Count - 1);
                        e.Handled = true;
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
                if (Month1BudgetId > 0)
                {
                    FoucsRecGrid(0);
                }
                else
                {
                    txtBudgetName.Focus();
                }
            }
        }

        private void gvExpenseLedger_EditFormPrepared(object sender, EditFormPreparedEventArgs e)
        {

            foreach (var btn in e.Panel.Controls.OfType<PanelControl>().FirstOrDefault().Controls.OfType<SimpleButton>())
            {
                if (btn.Text.ToUpper() == "UPDATE")
                {
                    btn.Click += new EventHandler(NRLedgersupdateButton_Click);
                    btn.PreviewKeyDown += new PreviewKeyDownEventHandler(NRLedgersupdateButton_PreviewKeyDown);
                }
                else if (btn.Text.ToUpper() == "CANCEL")
                {
                    btn.Click += new EventHandler(NRLedgerscancelButton_Click);
                }
            }

            /*var NRLedgersupdateButton = e.Panel.Controls.OfType<PanelControl>().FirstOrDefault().Controls.OfType<SimpleButton>().Select(x => x.Text == GridLocalizer.Active.GetLocalizedString(GridStringId.EditFormUpdateButton) ? x : null).FirstOrDefault();
            var NRLedgerscancelButton = e.Panel.Controls.OfType<PanelControl>().FirstOrDefault().Controls.OfType<SimpleButton>().Select(x => x.Text == GridLocalizer.Active.GetLocalizedString(GridStringId.EditFormUpdateButton) ? x : null).FirstOrDefault();

            if (NRLedgersupdateButton != null)
            {
                NRLedgersupdateButton.Click += new EventHandler(NRLedgersupdateButton_Click);
            }

            if (NRLedgerscancelButton != null)
            {
                NRLedgerscancelButton.Click += new EventHandler(NRLedgerscancelButton_Click);
            }*/

            //New Sub Ledger Name
            e.BindableControls[colNRNewSubLedgerName].Text = string.Empty;
            TextEdit txtNewSubLedger = e.BindableControls[colNRNewSubLedgerName] as TextEdit;
            txtNewSubLedger.Properties.MaxLength = 50;

            //New Sub Ledger Amount
            TextEdit txtPropsedAmt = e.BindableControls[colNRNewSubLedgerAmount] as TextEdit;
            txtPropsedAmt.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtPropsedAmt.Properties.Mask.EditMask = "n";
            txtPropsedAmt.Properties.Mask.UseMaskAsDisplayFormat = this.rtxtExpenseProposedCurrentYR.Mask.UseMaskAsDisplayFormat;
            //txtPropsedAmt.Properties.Mask.EditMask = "###,###,###,##0.00";
            e.BindableControls[colNRNewSubLedgerAmount].Text = "0.00";

            //New Sub Ledger Narration
            e.BindableControls[colNRNewSubLedgerNarration].Text = "";
            TextEdit txtNarration = e.BindableControls[colNRNewSubLedgerNarration] as TextEdit;
            txtNarration.Properties.MaxLength = 100;

            e.BindableControls[colNRNewSubLedgerName].Select();
            e.BindableControls[colNRNewSubLedgerName].Focus();

            e.Panel.BackColor = gcNonRecuringGridSource.BackColor;
        }

        private void NRLedgersupdateButton_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DoAddSubLedger();
            }
        }


        //void txtPropsedAmt_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyData != Keys.Tab && e.KeyData != Keys.Enter)
        //    {
        //        TextEdit txt = sender as TextEdit;
        //        txt.Select();
        //        txt.Focus();
        //    }
        //}

        private void NRLedgerscancelButton_Click(object sender, EventArgs e)
        {
            gvExpenseLedger.OptionsBehavior.EditingMode = GridEditingMode.Default;
        }
        /// <summary>
        /// This is to Select Recuring and Non Recuring Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRecNonRecuringSelection_Click(object sender, EventArgs e)
        {
            frmSubCreationsSelectionLedger frmLedgerCreation = new frmSubCreationsSelectionLedger(ProjectId, Month1BudgetId, 0, dtNonRecBudgetSubwithLedgerRecords);
            frmLedgerCreation.ShowDialog();
            if (frmLedgerCreation.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (frmLedgerCreation.ReturnValue != null)
                {
                    dtRecBudgetSubwithLedgerRecords = frmLedgerCreation.ReturnValue as DataTable;
                    BindBudgetGrid(BudgetMainGroup.NonRecurring);
                }
            }
        }

        private void rbtnInsert_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                //Force to update, when click insert
                gvExpenseLedger.PostEditor();
                gvExpenseLedger.UpdateCurrentRow();
                DataTable dtNonRecuring = gcNonRecuringGridSource.DataSource as DataTable;

                if (dtNonRecuring != null)
                {
                    int isSubLedger = UtilityMember.NumberSet.ToInteger(gvExpenseLedger.GetRowCellValue(gvExpenseLedger.FocusedRowHandle, colIsSubLedger.FieldName).ToString());

                    //For Tmp Purpose, to update grid changes in process NC table - ---------------------------------------------------------
                    foreach (DataRow drModifed in dtNonRecuring.Rows)
                    {
                        string ledgername = drModifed["LEDGER_NAME"].ToString();
                        int ledgerid = UtilityMember.NumberSet.ToInteger(drModifed[colExpenseLedgerId.FieldName].ToString());
                        int subledgerid = UtilityMember.NumberSet.ToInteger(drModifed[colExpSubLedgerId.FieldName].ToString());
                        int IsSubLedgerid = UtilityMember.NumberSet.ToInteger(drModifed[colIsSubLedger.FieldName].ToString());
                        string sublegertmpid = drModifed[colTmpSubLedgerId.FieldName].ToString();

                        if (IsSubLedgerid == 1)
                        {
                            if (subledgerid > 0)
                            {
                                dtNonRecBudgetSubwithLedgerRecords.DefaultView.RowFilter = "LEDGER_ID = " + ledgerid + " AND SUB_LEDGER_ID = " + subledgerid;
                            }
                            else
                            {
                                dtNonRecBudgetSubwithLedgerRecords.DefaultView.RowFilter = "LEDGER_ID = " + ledgerid + " AND TMP_SUB_LEDGER_ID = '" + sublegertmpid + "'";
                            }
                        }
                        else
                        {
                            dtNonRecBudgetSubwithLedgerRecords.DefaultView.RowFilter = "LEDGER_ID = " + ledgerid + " AND SUB_LEDGER_ID = 0";
                        }

                        if (dtNonRecBudgetSubwithLedgerRecords.DefaultView.Count == 1)
                        {
                            dtNonRecBudgetSubwithLedgerRecords.DefaultView[0][PROPOSED_CURRENT_YR] = drModifed[PROPOSED_CURRENT_YR];
                            dtNonRecBudgetSubwithLedgerRecords.DefaultView[0][NARRATION] = drModifed[NARRATION];
                            dtNonRecBudgetSubwithLedgerRecords.AcceptChanges();
                        }
                        dtNonRecBudgetSubwithLedgerRecords.DefaultView.RowFilter = string.Empty;
                    }

                    if (isSubLedger == 0) //Allow to add sub ledger only for Main Ledger
                    {
                        gvExpenseLedger.OptionsBehavior.EditingMode = GridEditingMode.EditFormInplace;
                        gvExpenseLedger.ShowInplaceEditForm();
                    }
                    //-----------------------------------------------------------------------------------------------------------------------
                }
            }
        }

        private void gvIncomeLedger_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo info;
            info = e.Info as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo;
            info.ButtonBounds = Rectangle.Empty;
            Rectangle rc = e.Bounds;
            string budgetgroupid = info.EditValue.ToString().Trim();
            string budgetgrp = string.Empty;

            switch (budgetgroupid)
            {
                case "1":
                    budgetgrp = "1.          Salary";
                    break;
                case "2":
                    budgetgrp = "            b. P.F :";
                    break;
                case "3":
                    budgetgrp = "            c. ESIC :";
                    break;
                default:
                    break;
            }

            info.GroupText = budgetgrp;//; " " + info.GroupText.TrimStart();

            e.Cache.FillRectangle(e.Appearance.GetBackBrush(e.Cache), rc);
            ObjectPainter.DrawObject(e.Cache, e.Painter, e.Info);

            e.Handled = true;
        }

        private void gvIncomeLedger_GroupRowCollapsing(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            e.Allow = false;
        }

        private void gvExpenseLedger_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column == this.gccolInsert)
            {
                DataRow drRow = gvExpenseLedger.GetDataRow(e.RowHandle);
                if (drRow != null)
                {
                    Int32 IsSubLedger = UtilityMember.NumberSet.ToInteger(drRow[colIsSubLedger.FieldName].ToString());
                    rbtnInsertDummary.Buttons[0].Visible = false;
                    //By default assign dummary RepositoryItemButtonEdit (without button)
                    e.RepositoryItem = rbtnInsertDummary;
                    if (IsSubLedger == 0)
                    {
                        rbtnInsert.Buttons[0].ToolTip = "Click to add new Sub Ledger";
                        e.RepositoryItem = rbtnInsert; //Attach RepositoryItemButtonEdit 
                    }
                }
            }
            else if (e.Column == this.gccolDelete)
            {
                DataRow drRow = gvExpenseLedger.GetDataRow(e.RowHandle);
                if (drRow != null)
                {
                    rbtnDelete.Buttons[0].ToolTip = "Click here to remove Main/Sub Ledgers";
                    /*Int32 IsSubLedger = UtilityMember.NumberSet.ToInteger(drRow[colIsSubLedger.FieldName].ToString());
                    rbtnInsertDummary.Buttons[0].Visible = false;
                    //By default assign dummary RepositoryItemButtonEdit (without button)
                    e.RepositoryItem = rbtnInsertDummary;
                    if (IsSubLedger == 0)
                    {
                        rbtnInsert.Buttons[0].ToolTip = "Click to add new Sub Ledger";
                        e.RepositoryItem = rbtnInsert; //Attach RepositoryItemButtonEdit 
                    }*/
                }
            }
        }

        private void NRLedgersupdateButton_Click(object sender, EventArgs e)
        {
            DoAddSubLedger();
        }

        private void btnReplaceProposedtoApproved_Click(object sender, EventArgs e)
        {
            //if (gcRecuringGridSource.DataSource != null && gcRecuringGridSource.DataSource != null)
            //{
            //    if (ShowConfirmationMessage("Are you sure to assign Proposed Amount to Approved Amount ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            //    {
            //        //All other Ledgers
            //        DataTable dtRecuring = gcRecuringGridSource.DataSource as DataTable;
            //        foreach (DataRow drIncome in dtRecuring.Rows)
            //        {
            //            drIncome[APPROVED_CURRENT_YR] = this.UtilityMember.NumberSet.ToDecimal(drIncome[PROPOSED_CURRENT_YR].ToString());
            //        }
            //        gcRecuringGridSource.DataSource = dtRecuring;

            //        DataTable dtNonRecuring = gcNonRecuringGridSource.DataSource as DataTable;
            //        foreach (DataRow drExpense in dtNonRecuring.Rows)
            //        {
            //            drExpense[APPROVED_CURRENT_YR] = this.UtilityMember.NumberSet.ToDecimal(drExpense[PROPOSED_CURRENT_YR].ToString());
            //        }
            //        gcNonRecuringGridSource.DataSource = dtNonRecuring;
            //    }
            //}
            //else
            //{
            //    MessageRender.ShowMessage("Budget Ledgers are not available");
            //}
        }
        #endregion

        #region Methods
        private void LoadDefaults()
        {
            LoadBudgetType();
            LoadMonths();
            LoadProjectDetails();
            if (Month1BudgetId > 0)
            {
                AssignValues();
            }
            SetTitle();

        }

        private void LoadMonths()
        {
            try
            {
                DataTable dtTrans = new DataTable();
                dtTrans.Columns.Add("Month", typeof(string));
                dtTrans.Columns.Add("MonthName", typeof(string));

                var startDate = new DateTime(this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Year, this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Month, this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Day);
                var endDate = new DateTime(this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false).Year, this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false).Month, this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false).Day);

                endDate = new DateTime(endDate.Year, endDate.Month, DateTime.DaysInMonth(endDate.Year, endDate.Month));

                string[] Records = Enumerable.Range(0, Int32.MaxValue)
                                     .Select(e => startDate.AddMonths(e))
                                     .TakeWhile(e => e <= endDate)
                                     .Select(e => e.ToString("MMM-yyyy")).ToArray();


                if (IsTwoMonthBudget)
                {
                    string prevMonth = string.Empty;
                    int monthrow = 0;
                    foreach (string tlid in Records)
                    {
                        if ((monthrow % 2) != 0)
                        {
                            DataRow dr = dtTrans.NewRow();
                            dr["Month"] = prevMonth;
                            dr["MonthName"] = prevMonth + "  " + tlid;
                            dtTrans.Rows.Add(dr);
                        }
                        monthrow++;
                        prevMonth = tlid;
                    }
                }
                else
                {
                    foreach (string tlid in Records)
                    {
                        DataRow dr = dtTrans.NewRow();
                        dr["Month"] = tlid;
                        dr["MonthName"] = tlid;
                        dtTrans.Rows.Add(dr);
                    }
                }

                glkpMonth.Properties.DataSource = dtTrans;
                glkpMonth.Properties.ValueMember = "Month";
                glkpMonth.Properties.DisplayMember = "MonthName";
                glkpMonth.EditValue = glkpMonth.Properties.GetKeyValue(0);
            }
            catch (Exception)
            {
                this.ShowMessageBoxWarning("Problems occured");
            }
        }

        private void SetTitle()
        {
            this.Text = Month1BudgetId > 0 ? GetMessage(MessageCatalog.Master.Budget.BUDGET_ANNUAL_EDIT_CAPTION) : GetMessage(MessageCatalog.Master.Budget.BUDGET_ANNUAL_ADD_CAPTION);

            string Previous = new DateTime(this.UtilityMember.DateSet.ToDate(dePeriodFrom.DateTime.ToString(), false).Year, this.UtilityMember.DateSet.ToDate(dePeriodFrom.DateTime.ToString(), false).AddMonths(-1).Month, this.UtilityMember.DateSet.ToDate(dePeriodFrom.DateTime.ToString(), false).Day).ToString("MMM", CultureInfo.InvariantCulture);
            string Current = new DateTime(this.UtilityMember.DateSet.ToDate(dePeriodFrom.DateTime.ToString(), false).Year, this.UtilityMember.DateSet.ToDate(dePeriodFrom.DateTime.ToString(), false).Month, this.UtilityMember.DateSet.ToDate(dePeriodFrom.DateTime.ToString(), false).Day).ToString("MMM", CultureInfo.InvariantCulture);

            //  string MonthPreviousYear = String.Format("{0}-{1}", Enum.GetName(typeof(Month), UtilityMember.DateSet.ToDate(dePeriodFrom.DateTime.ToString(), true).AddMonths(-1).Month), UtilityMember.DateSet.ToDate(AppSetting.YearFrom, true).Year);
            string MonthPreviousYear = String.Format("{0}-{1}", Previous, UtilityMember.DateSet.ToDate(dePeriodFrom.DateTime.ToString(), true).AddMonths(-1).Year);
            string MonthCurrentYear = String.Format("{0}-{1}", Current, UtilityMember.DateSet.ToDate(dePeriodFrom.DateTime.ToString(), true).Year);

            //Rec Grid
            colRecProposed.Caption = String.Format("Proposed {0}", MonthCurrentYear);
            colRecCurrentApproved.Caption = string.Format("Approved {0}", MonthCurrentYear);
            colRecActual.Caption = String.Format("Actual {0}", MonthPreviousYear);
            colRecPreviousApproved.Caption = String.Format("Budgeted {0}", MonthPreviousYear);
            colNRNewSubLedgerAmount.Caption = MonthCurrentYear + " Amount";

            //For Next Month
            colRecNextMonthProposed.Width = colRecProposed.Width;
            colRecNextMonthProposed.Visible = colRecNextMonthCurrentApproved.Visible = (this.IsTwoMonthBudget);
            colRecCurrentApproved.Visible = (!this.IsTwoMonthBudget);
            colRecNextMonthCurrentApproved.Visible =false;

            if (IsTwoMonthBudget)
            {
                colRecNextMonthProposed.Caption = string.Format("Proposed {0}", Month2DateFrom.ToString("MMM") + "-" + Month2DateFrom.Year.ToString()); ;
                colRecNextMonthCurrentApproved.Caption = string.Format("Approved {0}", Month2DateFrom.ToString("MMM") + "-" + Month2DateFrom.Year.ToString()); ;
            }

            //NR grid
            colNRProposed.Caption = String.Format("Proposed {0}", MonthCurrentYear);
            colNRCurrentApproved.Caption = string.Format("Approved {0}", MonthCurrentYear);
            colNRActual.Caption = String.Format("Actual Spent {0}", MonthPreviousYear);
            colNRPreviousApproved.Caption = String.Format("Budgeted {0}", MonthPreviousYear);
            colNRNewSubLedgerAmount.Caption = Month1DateFrom.ToString("MMM") + "-" + Month1DateFrom.Year.ToString() + " Amount";

            //For Next Month
            colNRNextMonthProposals.Width = colNRProposed.Width;
            colNRNextMonthProposals.Visible = colNextMonthCurrentApproved.Visible = (this.IsTwoMonthBudget);
            colNRCurrentApproved.Visible = (!this.IsTwoMonthBudget);
            colNextMonthCurrentApproved.Visible = false;

            colNRNewNextMonthSubLedgerAmount.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
            if (IsTwoMonthBudget)
            {
                colNRNewNextMonthSubLedgerAmount.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.True;
                colNRNewNextMonthSubLedgerAmount.Caption = Month2DateFrom.ToString("MMM") + "-" + Month2DateFrom.Year.ToString() + " Amount";
                colNRNextMonthProposals.Caption = string.Format("Proposed {0}", Month2DateFrom.ToString("MMM") + "-" + Month2DateFrom.Year.ToString()); ;
                colNextMonthCurrentApproved.Caption = string.Format("Approved {0}", Month2DateFrom.ToString("MMM") + "-" + Month2DateFrom.Year.ToString()); ;
            }

            if (this.UtilityMember.NumberSet.ToInteger(BudgetTypeId) != (int)BudgetType.BudgetPeriod || this.UtilityMember.NumberSet.ToInteger(BudgetTypeId) != (int)BudgetType.BudgetByAnnualYear || this.UtilityMember.NumberSet.ToInteger(BudgetTypeId) != (int)BudgetType.BudgetByCalendarYear || this.UtilityMember.NumberSet.ToInteger(BudgetTypeId) != (int)BudgetType.BudgetYear)
            {
                lciMonth.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                lciMonth.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //lciStatisticsButton.Visibility = (this.AppSetting.IS_DIOMYS_DIOCESE ? DevExpress.XtraLayout.Utils.LayoutVisibility.Never : DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
            }

            // lcBudgetGroup.Text = String.Format(this.GetMessage(MessageCatalog.Master.Budget.BUDGET_FOR), MonthCurrentYear);
            gccolBudgetGroup.Visible = gccolBudgetSubGroup.Visible = false;
            // colIncomeHONarration.Visible = colExeHONarration.Visible = true;

            // To Set the Size 
            colNRSno.Width = ColRSNo.Width;
            colNRLedger.Width = colRecLedger.Width + 50;
            colNRPreviousApproved.Width = colRecPreviousApproved.Width;
            colNRActual.Width = colRecActual.Width;
            colNRBalance.Width = colRecBalance.Width - 11;
            colNRProposed.Width = colRecProposed.Width;
            colNRCurrentApproved.Width = colRecCurrentApproved.Width;
            colNRNarration.Width = colRecNarration.Width - 54;
            // colExeHONarration.Width = colIncomeHONarration.Width - 55;
        }

        private void LoadBudgetType()
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

        private void FetchBudgetDetails()
        {
            using (BudgetSystem budgetAnnual = new BudgetSystem())
            {
                //Expense Ledger Details
                // budgetAnnual.MultipleProjectId = 1; // grdProject.EditValue.ToString();
                budgetAnnual.ProjectId = UtilityMember.NumberSet.ToInteger(grdProject.EditValue.ToString()); //ProjectId;
                budgetAnnual.BudgetId = budgetAnnual.M1BudgetId = Month1BudgetId;
                budgetAnnual.M2BudgetId = Month2BudgetId;
                budgetAnnual.DateFrom = dePeriodFrom.DateTime.ToString();
                budgetAnnual.DateTo = dePeriodTo.DateTime.ToString();
                budgetAnnual.BudgetTypeId = 5; //UtilityMember.NumberSet.ToInteger(glkpBudgetType.EditValue.ToString());
                budgetAnnual.BudgetTransMode = TransactionMode.DR.ToString();

                resultArgs = budgetAnnual.FetchBudgetMysoreAddDetails();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    if (resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtMainSource = resultArgs.DataSource.Table;

                        //Seriall No
                        DataColumn dcSnoColumn = new DataColumn("S.No", typeof(string));
                        dtMainSource.Columns.Add(dcSnoColumn);

                        // RE
                        dtMainSource.DefaultView.RowFilter = "";
                        dtMainSource.DefaultView.RowFilter = "REC_NONREC=1";
                        dtMainSource.DefaultView.Sort = "SORT_ID";
                        dtRecBudgetSubwithLedgerRecords = dtMainSource.DefaultView.ToTable();
                        dtMainSource.DefaultView.RowFilter = "";

                        // Non Re
                        dtMainSource.DefaultView.RowFilter = "";
                        dtMainSource.DefaultView.RowFilter = Month1BudgetId == 0 ? "REC_NONREC=0 AND IS_SUB_LEDGER=0" : "REC_NONREC=0";
                        dtNonRecBudgetSubwithLedgerRecords = dtMainSource.DefaultView.ToTable();
                        dtMainSource.DefaultView.RowFilter = "";

                        RecLedgersSerialNo = 1;
                        RecAlphabetSerialNo = 0;
                        BindBudgetGrid(BudgetMainGroup.Recurring);
                        BindBudgetGrid(BudgetMainGroup.NonRecurring);

                        FoucsNonRecGrid(0);

                        // DataTable dtExpenseGridSource = new DataTable();
                        // dtExpenseGridSource = gcExpenseLedger.DataSource as DataTable;

                        // DataTable dtSourceExpenseGrid = dtExpenseGridSource;
                        // DataTable dtExpenseSourceDB = ExpenseSource;

                        //if (dtSourceExpenseGrid != null && dtSourceExpenseGrid.Rows.Count > 0)
                        //{
                        //    foreach (DataRow drDB in dtExpenseSourceDB.Rows)
                        //    {
                        //        Int32 LedgerId = UtilityMember.NumberSet.ToInteger(drDB[LEDGER_ID].ToString());
                        //        dtSourceExpenseGrid.DefaultView.RowFilter = string.Empty;
                        //        dtSourceExpenseGrid.DefaultView.RowFilter = "LEDGER_ID = " + LedgerId;
                        //        if (dtSourceExpenseGrid.DefaultView.Count == 0)
                        //        {
                        //            DataRow drGrid = dtSourceExpenseGrid.NewRow();
                        //            drGrid["LEDGER_ID"] = drDB["LEDGER_ID"];
                        //            drGrid["LEDGER_CODE"] = drDB["LEDGER_CODE"];
                        //            drGrid["LEDGER_NAME"] = drDB["LEDGER_NAME"];
                        //            drGrid["NATURE"] = drDB["NATURE"];
                        //            drGrid["APPROVED_PREVIOUS_YR"] = drDB["APPROVED_PREVIOUS_YR"];
                        //            drGrid["ACTUAL"] = drDB["ACTUAL"];
                        //            drGrid["PROPOSED_CURRENT_YR"] = drDB["PROPOSED_CURRENT_YR"];
                        //            drGrid["APPROVED_CURRENT_YR"] = drDB["APPROVED_CURRENT_YR"];
                        //            drGrid["NARRATION"] = drDB["NARRATION"];
                        //            drGrid["BUDGET_TRANS_MODE"] = drDB["BUDGET_TRANS_MODE"];
                        //            drGrid["NATURE_ID"] = drDB["NATURE_ID"];
                        //            dtSourceExpenseGrid.Rows.Add(drGrid);
                        //            dtSourceExpenseGrid.AcceptChanges();
                        //        }
                        //        else if (dtSourceExpenseGrid.DefaultView.Count == 1)
                        //        {
                        //            decimal actual = UtilityMember.NumberSet.ToDecimal(drDB["ACTUAL"].ToString());
                        //            dtSourceExpenseGrid.DefaultView[0]["ACTUAL"] = actual;
                        //            dtSourceExpenseGrid.AcceptChanges();
                        //        }
                        //    }

                        //    //This is to Remove the Datasource for Income
                        //    for (int i = dtSourceExpenseGrid.Rows.Count - 1; i >= 0; i--)
                        //    {
                        //        Int32 LedgerId = UtilityMember.NumberSet.ToInteger(dtSourceExpenseGrid.Rows[i]["LEDGER_ID"].ToString());
                        //        dtExpenseSourceDB.DefaultView.RowFilter = string.Empty;
                        //        dtExpenseSourceDB.DefaultView.RowFilter = "LEDGER_ID = " + LedgerId;
                        //        if (dtExpenseSourceDB.DefaultView.Count == 0)
                        //        {
                        //            dtSourceExpenseGrid.Rows[i].Delete();
                        //        }
                        //    }
                        //    dtSourceExpenseGrid.DefaultView.RowFilter = string.Empty;
                        //    dtSourceExpenseGrid.AcceptChanges();
                        //}

                        //if (dtSourceExpenseGrid != null && dtSourceExpenseGrid.Rows.Count > 0)
                        //{

                        //    dtSourceExpenseGrid.DefaultView.RowFilter = string.Empty;

                        //    dtSourceExpenseGrid.DefaultView.Sort = "NATURE_ID ASC";
                        //    dtSourceExpenseGrid.DefaultView.RowFilter = string.Empty;

                        //    dtSourceExpenseGrid.DefaultView.RowFilter = "REC_NONREC =0";
                        //    gcIncomeLedger.DataSource = dtSourceExpenseGrid;
                        //    gcIncomeLedger.RefreshDataSource();

                        //    dtSourceExpenseGrid.DefaultView.RowFilter = string.Empty;
                        //    dtSourceExpenseGrid.DefaultView.RowFilter = "REC_NONREC =1";
                        //    gcExpenseLedger.DataSource = dtSourceExpenseGrid;
                        //    gcExpenseLedger.RefreshDataSource();
                        //}
                        // else
                        // {
                        // }
                    }
                    else
                    {
                        gcNonRecuringGridSource.DataSource = gcRecuringGridSource.DataSource = null;

                    }
                }
                else ShowMessageBox(resultArgs.Message);
            }
        }

        private int AttachSerialNo(BudgetMainGroup budgetgroup, int startno, DataTable dtBind)
        {
            if (dtBind != null)
            {
                foreach (DataRow dr in dtBind.Rows)
                {
                    int budgetgroupid = UtilityMember.NumberSet.ToInteger(dr["BUDGET_SUB_GROUP_ID"].ToString());
                    string ledgername = dr["LEDGER_NAME"].ToString();
                    int issubledger = UtilityMember.NumberSet.ToInteger(dr["IS_SUB_LEDGER"].ToString());

                    //Skip Salaru/PF/ESIC Groups
                    if (budgetgroup == BudgetMainGroup.Recurring)
                    {
                        ////For Alpha serial number
                        bool existsAlphaLedger = Array.IndexOf(FixedRecAlphabetLedgers, ledgername) >= 0;
                        bool existsESICLedger = Array.IndexOf(FixedRecESICMainLedgers, ledgername) >= 0;

                        if (budgetgroupid >= 3 && !existsAlphaLedger && !existsESICLedger)
                        {
                            startno++;
                        }

                        if (existsAlphaLedger)
                        {
                            dr["LEDGER_NAME"] = getAlphabetSerialNo() + "." + ledgername;
                        }
                    }
                    else
                    {
                        //for non Rec, generate only for main ledgers
                        if (issubledger == 0)
                        {
                            startno++;
                        }
                    }

                    if (budgetgroup == BudgetMainGroup.Recurring)
                    {
                        dr["S.No"] = (startno > 1 ? startno.ToString() : "");
                    }
                    else
                    {
                        dr["S.No"] = ((issubledger == 0) ? startno.ToString() : "");
                    }
                }
                dtBind.AcceptChanges();
            }
            return startno;
        }

        private void BindBudgetGrid(BudgetMainGroup budgetgroup)
        {
            DataTable BindSource = new DataTable();
            if (budgetgroup == BudgetMainGroup.Recurring)
            {
                dtRecBudgetSubwithLedgerRecords.DefaultView.RowFilter = "IS_ALLOTED IN (0,1)";
                // dtRecBudgetSubwithLedgerRecords.DefaultView.Sort = "MAIN_LEDGER_NAME, SUB_LEDGER_ID";
                dtRecBudgetSubwithLedgerRecords.DefaultView.Sort = "SORT_ID";
                BindSource = dtRecBudgetSubwithLedgerRecords.DefaultView.ToTable();
                RecLedgersSerialNo = AttachSerialNo(budgetgroup, RecLedgersSerialNo, BindSource);
                gcRecuringGridSource.DataSource = null;
                gcRecuringGridSource.DataSource = BindSource;
                gcRecuringGridSource.RefreshDataSource();
                dtRecBudgetSubwithLedgerRecords.DefaultView.RowFilter = "";
            }
            else if (budgetgroup == BudgetMainGroup.NonRecurring)
            {
                if (IsTwoMonthBudget)
                {
                    //(Month1BudgetId == 0 && Month1BudgetId == 0) ? "IS_ALLOTED IN (0,1)" : "IS_ALLOTED IN (1)";
                    dtNonRecBudgetSubwithLedgerRecords.DefaultView.RowFilter = "IS_ALLOTED IN (1)";
                }
                else
                {
                    //Month2BudgetId == 0 ? "IS_ALLOTED IN (0,1)" : "IS_ALLOTED IN (1)"; 
                    dtNonRecBudgetSubwithLedgerRecords.DefaultView.RowFilter = "IS_ALLOTED IN (1)";
                }
                dtNonRecBudgetSubwithLedgerRecords.DefaultView.Sort = "MAIN_LEDGER_NAME, SUB_LEDGER_ID";
                BindSource = dtNonRecBudgetSubwithLedgerRecords.DefaultView.ToTable();
                AttachSerialNo(budgetgroup, RecLedgersSerialNo, BindSource);
                gcNonRecuringGridSource.DataSource = null;
                gcNonRecuringGridSource.DataSource = BindSource;
                gcNonRecuringGridSource.RefreshDataSource();
                dtNonRecBudgetSubwithLedgerRecords.DefaultView.RowFilter = "";
            }
        }

        private void SetBudgetEdit()
        {
            using (BudgetSystem budgetAnnual = new BudgetSystem(Month1BudgetId, "5"))
            {
                budgetAnnual.BudgetId = Month1BudgetId;
                txtBudgetName.Text = budgetAnnual.BudgetName;
                AssignProjectIds = budgetAnnual.MultipleProjectId = ProjectId.ToString();
                this.glkpBudgetType.EditValueChanged -= new System.EventHandler(this.glkpBudgetType_EditValueChanged);
                glkpBudgetType.EditValue = 5; // budgetAnnual.BudgetTypeId;
                glkpBudgetType.Enabled = false;
                this.glkpBudgetType.EditValueChanged += new System.EventHandler(this.glkpBudgetType_EditValueChanged);
                //if (budgetAnnual.BudgetTypeId != (int)BudgetType.BudgetPeriod)
                //{
                //    dePeriodTo.DateTime = UtilityMember.DateSet.ToDate(budgetAnnual.DateTo, false);
                //    dePeriodFrom.DateTime = UtilityMember.DateSet.ToDate(budgetAnnual.DateFrom, false);
                //}
                if (budgetAnnual.BudgetTypeId == (int)BudgetType.BudgetMonth)
                {
                    lblPeriodFrom.Visibility = lblPeriodTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    dePeriodFrom.Enabled = dePeriodTo.Enabled = glkpMonth.Enabled = grdProject.Enabled = false;
                    dePeriodFrom.DateTime = UtilityMember.DateSet.ToDate(budgetAnnual.DateFrom, false);
                    dePeriodTo.DateTime = UtilityMember.DateSet.ToDate(budgetAnnual.DateTo, false);
                    string Current = new DateTime(this.UtilityMember.DateSet.ToDate(budgetAnnual.DateFrom, false).Year, this.UtilityMember.DateSet.ToDate(budgetAnnual.DateFrom, false).Month, this.UtilityMember.DateSet.ToDate(budgetAnnual.DateFrom, false).Day).ToString("MMM", CultureInfo.InvariantCulture);
                    string MonthCurrentYear = String.Format("{0}-{1}", Current, UtilityMember.DateSet.ToDate(dePeriodFrom.DateTime.ToString(), true).Year);
                    glkpMonth.EditValue = MonthCurrentYear;
                    glkpBudgetType.EditValue = 5;
                    int projectId = this.UtilityMember.NumberSet.ToInteger(grdProject.EditValue.ToString());
                    Month1BudgetId = budgetAnnual.GetBudgetIdByDateRangeProjectd(dePeriodFrom.DateTime, dePeriodTo.DateTime, ProjectId.ToString());
                    if (Month1BudgetId == 0)
                        ClearControls();
                }

                lblPeriodFrom.Visibility = lblPeriodTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                budgetAnnual.BudgetTransMode = TransactionMode.DR.ToString();
                FetchBudgetDetails();
                //budgetAnnual.BudgetTransMode = TransactionMode.CR.ToString();

                //chinna 20.02.2020
                //resultArgs = budgetAnnual.FetchBudgetEdit();
                //if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null)
                //{
                //    if (resultArgs.DataSource.Table.Rows.Count > 0)
                //    {
                //        DataTable IncomeSource = resultArgs.DataSource.Table;
                //        gcRecuringGridSource.DataSource = IncomeSource;
                //    }
                //}
                //else ShowMessageBox(resultArgs.Message);

                //budgetAnnual.BudgetTransMode = TransactionMode.DR.ToString();

                //resultArgs = budgetAnnual.FetchBudgetEdit();
                //if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null)
                //{
                //    if (resultArgs.DataSource.Table.Rows.Count > 0)
                //    {
                //        DataTable ExpenseSource = resultArgs.DataSource.Table;
                //        gcNonRecuringGridSource.DataSource = ExpenseSource;
                //    }
                //}
                //else ShowMessageBox(resultArgs.Message);
                FoucsRecGrid(0);
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

                //Assign Month dates (Month1)
                Month1DateFrom = dePeriodFrom.DateTime;
                Month1DateTo = dePeriodTo.DateTime;

                //Assign Month dates (Month2)
                Month2DateFrom = dePeriodFrom.DateTime.AddMonths(1);
                Month2DateTo = Month2DateFrom.AddMonths(1).AddDays(-1);
            }
            lblPeriodFrom.Visibility = lblPeriodTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            SetTitle();

        }

        private void LoadProjectDetails()
        {
            try
            {
                using (BudgetSystem budgetSystem = new BudgetSystem())
                {
                    budgetSystem.DateFrom = dePeriodFrom.DateTime.ToString();
                    budgetSystem.DateTo = dePeriodTo.DateTime.ToString();
                    budgetSystem.ProjectClosedDate = dePeriodFrom.DateTime.ToString();
                    resultArgs = budgetSystem.FetchBudgetProjectsLookup();
                    grdProject.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(grdProject, resultArgs.DataSource.Table, budgetSystem.AppSchema.Project.PROJECTColumn.ColumnName, budgetSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                        grdProject.EditValue = ProjectId == 0 ? 0 : ProjectId;
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
            else if (string.IsNullOrEmpty(ProjectId.ToString()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Budget.BUDGET_PROJECT_EMPTY));
                this.SetBorderColorForGridLookUpEdit(grdProject);
                IsValid = false;
                grdProject.Focus();
            }
            else if (glkpMonth.EditValue == null || glkpMonth.Properties.GetIndexByKeyValue(glkpMonth.EditValue) < 0)
            {
                this.ShowMessageBox("Invalid Month");
                this.SetBorderColorForGridLookUpEdit(glkpMonth);
                IsValid = false;
                glkpMonth.Focus();
            }
            return IsValid;
        }

        private bool IsBudgetProposedLedgers(BudgetMainGroup budgetgroup)
        {
            bool rtn = false;
            if (budgetgroup == BudgetMainGroup.Recurring)
            {
                dtNonRecBudgetSubwithLedgerRecords.DefaultView.RowFilter = "PROPOSED_AMOUNT >0 ";
                rtn = (dtNonRecBudgetSubwithLedgerRecords.DefaultView.Count > 0);
                dtNonRecBudgetSubwithLedgerRecords.DefaultView.RowFilter = "PROPOSED_AMOUNT >0 ";
            }
            else if (budgetgroup == BudgetMainGroup.Recurring)
            {
                dtRecBudgetSubwithLedgerRecords.DefaultView.RowFilter = "PROPOSED_AMOUNT >0 ";
                rtn = (dtRecBudgetSubwithLedgerRecords.DefaultView.Count > 0);
                dtRecBudgetSubwithLedgerRecords.DefaultView.RowFilter = "PROPOSED_AMOUNT >0 ";
            }
            return rtn;
        }

        private void RealColumnEditBudgetAmount()
        {
            colRecProposed.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditTransAmount_EditValueChanged);
            this.gvIncomeLedger.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvIncomeLedger.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colRecProposed)
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
            colNRProposed.RealColumnEdit.EditValueChanged += new EventHandler(RealColumnEditTransExpenseAmount_EditValueChanged);
            this.gvExpenseLedger.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvExpenseLedger.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colNRProposed)
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

        private void ClearControls()
        {
            txtBudgetName.Text = string.Empty;
            glkpMonth.Enabled = true;
        }

        private string getAlphabetSerialNo()
        {
            string rtn = string.Empty;

            try
            {
                int pos = (RecAlphabetSerialNo == 1 || RecAlphabetSerialNo == 2) ? RecAlphabetSerialNo + 2 : RecAlphabetSerialNo;
                string[] alphabetArray = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                rtn = alphabetArray.GetValue(pos).ToString();
                RecAlphabetSerialNo++;
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
            return rtn.ToLower();
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
                resultArgs = budgetsystem.FetchBudgetExists(Month1BudgetId);
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

        private void InsertNewSubLeger(BudgetMainGroup budgetgroup, string SubLedgerName, double PropsedAmount, double NextMonthPropsedAmount, string Narration)
        {
            try
            {
                int ledgerid = UtilityMember.NumberSet.ToInteger(gvExpenseLedger.GetRowCellValue(gvExpenseLedger.FocusedRowHandle, colExpenseLedgerId.FieldName).ToString());
                string mainledgername = gvExpenseLedger.GetRowCellValue(gvExpenseLedger.FocusedRowHandle, colMainLedger).ToString();

                if (!string.IsNullOrEmpty(SubLedgerName))
                {
                    dtNonRecBudgetSubwithLedgerRecords.DefaultView.RowFilter = "LEDGER_ID=" + ledgerid + " AND SUB_LEDGER_NAME='" + SubLedgerName.Replace("'", "''") + "'";

                    bool isSubLedgerExists = false;
                    Int32 isalloted = (Month1BudgetId == 0 ? 0 : 1);
                    if (dtNonRecBudgetSubwithLedgerRecords.DefaultView.Count > 0)
                    {
                        isSubLedgerExists = (dtNonRecBudgetSubwithLedgerRecords.DefaultView.Count > 0);

                        //check if subledger is already added and deleted (trying to re add again for same ledger, jsut change mode) -------------------------
                        isalloted = UtilityMember.NumberSet.ToInteger(dtNonRecBudgetSubwithLedgerRecords.DefaultView[0]["IS_ALLOTED"].ToString());
                        //if (isalloted == -1)
                        //{
                        //    dtNonRecBudgetSubwithLedgerRecords.DefaultView[0]["IS_ALLOTED"] = (Month1BudgetId == 0 ? 0 : 1);
                        //    dtNonRecBudgetSubwithLedgerRecords.AcceptChanges();
                        //    isSubLedgerExists = false;
                        //}
                        //-----------------------------------------------------------------------------------
                    }

                    if (!isSubLedgerExists)
                    {
                        if (budgetgroup == BudgetMainGroup.Recurring)
                        {
                            //replace
                        }
                        else if (budgetgroup == BudgetMainGroup.NonRecurring)
                        {
                            ledgerid = UtilityMember.NumberSet.ToInteger(gvExpenseLedger.GetRowCellValue(gvExpenseLedger.FocusedRowHandle, colExpenseLedgerId.FieldName).ToString());
                            mainledgername = gvExpenseLedger.GetRowCellValue(gvExpenseLedger.FocusedRowHandle, colMainLedger).ToString();

                            DataRow drNewSubLedger = dtNonRecBudgetSubwithLedgerRecords.NewRow();
                            Guid obj = Guid.NewGuid();
                            string TmpNewSubuniqueid = obj.ToString();
                            drNewSubLedger["LEDGER_ID"] = ledgerid;
                            drNewSubLedger["SUB_LEDGER_ID"] = 0;
                            drNewSubLedger["TMP_SUB_LEDGER_ID"] = TmpNewSubuniqueid;
                            drNewSubLedger["IS_ALLOTED"] = (Month1BudgetId == 0 ? 0 : 1);
                            drNewSubLedger["GROUP_ID"] = 0;
                            drNewSubLedger["NATURE_ID"] = 0;
                            drNewSubLedger["NATURE"] = string.Empty;
                            drNewSubLedger["MAIN_LEDGER_NAME"] = mainledgername;
                            drNewSubLedger["LEDGER_NAME"] = "  ->" + SubLedgerName + " - " + mainledgername;
                            drNewSubLedger["SUB_LEDGER_NAME"] = SubLedgerName.Trim();
                            drNewSubLedger["APPROVED_PREVIOUS_YR"] = 0.00;
                            drNewSubLedger["PROPOSED_CURRENT_YR"] = PropsedAmount;
                            drNewSubLedger["APPROVED_CURRENT_YR"] = 0.00;
                            drNewSubLedger["M2_PROPOSED_AMOUNT"] = 0.00;
                            drNewSubLedger["M2_APPROVED_AMOUNT"] = 0.00;
                            if (IsTwoMonthBudget)
                            {
                                drNewSubLedger["M2_PROPOSED_AMOUNT"] = NextMonthPropsedAmount;
                                drNewSubLedger["M2_APPROVED_AMOUNT"] = 0.00;
                            }
                            drNewSubLedger["ACTUAL"] = 0.00;
                            drNewSubLedger["NARRATION"] = Narration;
                            drNewSubLedger["HO_NARRATION"] = string.Empty;
                            drNewSubLedger["IS_SUB_LEDGER"] = 1;
                            drNewSubLedger["BUDGET_TRANS_MODE"] = "DR";
                            dtNonRecBudgetSubwithLedgerRecords.Rows.Add(drNewSubLedger);
                        }
                        BindBudgetGrid(budgetgroup);
                    }
                    else
                    {
                        //check if subledger is already added and deleted (trying to re add again for same ledger, jsut change mode) -------------------------
                        isalloted = UtilityMember.NumberSet.ToInteger(dtNonRecBudgetSubwithLedgerRecords.DefaultView[0]["IS_ALLOTED"].ToString());
                        if (isalloted == -1)
                        {
                            dtNonRecBudgetSubwithLedgerRecords.DefaultView[0]["IS_ALLOTED"] = (Month1BudgetId == 0 ? 0 : 1);
                            dtNonRecBudgetSubwithLedgerRecords.AcceptChanges();

                            dtNonRecBudgetSubwithLedgerRecords.DefaultView.RowFilter = string.Empty;
                            BindBudgetGrid(budgetgroup);
                        }
                        //-----------------------------------------------------------------------------------
                        else
                        {
                            MessageRender.ShowMessage(SubLedgerName + " already exists.");
                        }
                        dtNonRecBudgetSubwithLedgerRecords.DefaultView.RowFilter = string.Empty;
                    }
                }

                dtNonRecBudgetSubwithLedgerRecords.DefaultView.RowFilter = string.Empty;
            }
            catch (Exception err)
            {
                dtNonRecBudgetSubwithLedgerRecords.DefaultView.RowFilter = string.Empty;
                MessageRender.ShowMessage(err.Message);
            }
        }

        /// <summary>
        /// This is to export the budget to Portal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportBudget_Click(object sender, EventArgs e)
        {
            if (this.ShowConfirmationMessage("Are you sure to export Budget to Head Office Portal ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                this.ShowWaitDialog("Uploading Budget from Head Office Portal");
                ResultArgs result = this.UploadBudgetToAcmeerpPortal(Month1BudgetId);
                this.CloseWaitDialog();
                if (result.Success)
                {
                    if (IsTwoMonthBudget)
                    {
                        result = this.UploadBudgetToAcmeerpPortal(Month2BudgetId);
                    }
                    if (result.Success)
                    {
                        this.ShowMessageBox("Budget has been uploaded to Head Office Portal, It will be approved by Head Office");
                    }
                }

                if (!result.Success)
                {
                    this.CloseWaitDialog();
                    this.ShowMessageBoxError(result.Message);
                }
            }
        }

        /// <summary>
        /// This is to import the budget from  portal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportBudget_Click(object sender, EventArgs e)
        {
            if (this.ShowConfirmationMessage("Are you sure to import Budget from Head Office Portal ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                this.ShowWaitDialog("Downloading Budget from Head Office Portal");
                ResultArgs result = this.GetApprovedBudgetFromAcmeerpPortal(Month1BudgetId);
                this.CloseWaitDialog();
                if (result.Success)
                {
                    result = this.GetApprovedBudgetFromAcmeerpPortal(Month2BudgetId);
                    if (result.Success)
                    {
                        this.ShowMessageBox("Budget has been approved by Head Office Portal");
                    }

                    if (UtilityMember.NumberSet.ToInteger(grdProject.EditValue.ToString()) > 0)
                    {
                        AssignValues();
                        ProjectId = UtilityMember.NumberSet.ToInteger(grdProject.EditValue.ToString());
                        GetLastMonthProperty();
                    }
                }

                if (!result.Success)
                {
                    this.CloseWaitDialog();
                    this.ShowMessageBoxError(result.Message);
                }
            }
        }

        /// <summary>
        /// This is to get the form of Insertion & Selection Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnInsert_Click(object sender, EventArgs e)
        {
            //gvExpenseLedger.OptionsBehavior.EditingMode = GridEditingMode.EditFormInplace;

            //frmSubCreationsSelectionLedger frmLedgerCreation = new frmSubCreationsSelectionLedger(ProjectId, 0, dtNonRecBudgetSubwithLedgerRecords);
            //frmLedgerCreation.ShowDialog();
            //if (frmLedgerCreation.DialogResult == System.Windows.Forms.DialogResult.OK)
            //{
            //    if (frmLedgerCreation.ReturnValue != null)
            //    {
            //        dtRecBudgetSubwithLedgerRecords = frmLedgerCreation.ReturnValue as DataTable;
            //        BindBudgetGrid(1);
            //    }
            //}
        }

        /// <summary>
        /// Filling the Non Recuring Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnDelete_Click(object sender, EventArgs e)
        {
            if (gvExpenseLedger.RowCount > 0)
            {
                int ledgerid = UtilityMember.NumberSet.ToInteger(gvExpenseLedger.GetRowCellValue(gvExpenseLedger.FocusedRowHandle, colExpenseLedgerId.FieldName).ToString());
                int subledgerid = UtilityMember.NumberSet.ToInteger(gvExpenseLedger.GetRowCellValue(gvExpenseLedger.FocusedRowHandle, colExpSubLedgerId.FieldName).ToString());
                int IsSubLedgerid = UtilityMember.NumberSet.ToInteger(gvExpenseLedger.GetRowCellValue(gvExpenseLedger.FocusedRowHandle, colIsSubLedger.FieldName).ToString());
                string sublegertmpid = gvExpenseLedger.GetRowCellValue(gvExpenseLedger.FocusedRowHandle, colTmpSubLedgerId.FieldName).ToString();
                bool SubLedgersExists = false;
                bool Rebind = false;
                string msg = this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION);
                string findledgers = string.Empty;
                if (ledgerid > 0)
                {
                    if (IsSubLedgerid == 1) //For Sub Ledger
                    {
                        if (subledgerid > 0) //For existing sub ledgers
                        {
                            findledgers = "LEDGER_ID = " + ledgerid + " AND SUB_LEDGER_ID = " + subledgerid;
                        }
                        else //For newly Added sub ledgers
                        {
                            findledgers = "LEDGER_ID = " + ledgerid + " AND TMP_SUB_LEDGER_ID = '" + sublegertmpid + "'";
                        }
                        Rebind = true;
                    }
                    else //For Main Ledgers
                    {
                        dtNonRecBudgetSubwithLedgerRecords.DefaultView.RowFilter = "LEDGER_ID = " + ledgerid + " AND IS_SUB_LEDGER=1";
                        SubLedgersExists = (dtNonRecBudgetSubwithLedgerRecords.DefaultView.Count > 0);
                        dtNonRecBudgetSubwithLedgerRecords.DefaultView.RowFilter = string.Empty;

                        if (SubLedgersExists)
                        {
                            msg = "Sub Ledgers are availalbe, Do you want to delete all its Sub Ledgers too ?";
                        }
                        findledgers = findledgers = "LEDGER_ID = " + ledgerid;

                        Rebind = true;
                    }

                    if (Rebind)
                    {
                        if (this.ShowConfirmationMessage(msg, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            dtNonRecBudgetSubwithLedgerRecords.DefaultView.RowFilter = findledgers;
                            foreach (DataRowView drv in dtNonRecBudgetSubwithLedgerRecords.DefaultView)
                            {
                                drv["IS_ALLOTED"] = -1;
                                dtNonRecBudgetSubwithLedgerRecords.AcceptChanges();
                            }
                            dtNonRecBudgetSubwithLedgerRecords.DefaultView.RowFilter = string.Empty;
                            BindBudgetGrid(BudgetMainGroup.NonRecurring);
                        }
                    }
                }
            }
        }

        private void btnsubLedger_Click(object sender, EventArgs e)
        {
            frmSubLeder ledger = new frmSubLeder(ProjectId);
            ledger.ShowDialog();
        }

        private void grdProject_EditValueChanged(object sender, EventArgs e)
        {
            this.ProjectId = UtilityMember.NumberSet.ToInteger(grdProject.EditValue.ToString());

            SetTitle();

            AssignValues();

        }

        /// <summary>
        /// this is this is Income Side Recuring Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnRecInsert_Click(object sender, EventArgs e)
        {
            //frmSubCreationsSelectionLedger frmLedgerCreation = new frmSubCreationsSelectionLedger(ProjectId, 0, dtRecBudgetSubwithLedgerRecords);
            //frmLedgerCreation.ShowDialog();
            //if (frmLedgerCreation.DialogResult == System.Windows.Forms.DialogResult.OK)
            //{
            //    if (frmLedgerCreation.ReturnValue != null)
            //    {
            //        dtRecBudgetSubwithLedgerRecords = frmLedgerCreation.ReturnValue as DataTable;
            //        BindBudgetGrid(BudgetMainGroup.Recurring);
            //    }
            //}

        }

        /// <summary>
        /// Filling the Recuring Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnRecDelete_Click(object sender, EventArgs e)
        {
            if (gvIncomeLedger.RowCount > 1)
            {
                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int ledgerid = UtilityMember.NumberSet.ToInteger(gvIncomeLedger.GetRowCellValue(gvIncomeLedger.FocusedRowHandle, colIncomeLedgerId.FieldName).ToString());
                    int subledgerid = UtilityMember.NumberSet.ToInteger(gvIncomeLedger.GetRowCellValue(gvIncomeLedger.FocusedRowHandle, colIncomeSubLederId.FieldName).ToString());

                    dtRecBudgetSubwithLedgerRecords.DefaultView.RowFilter = "LEDGER_ID = " + ledgerid + " AND SUB_LEDGER_ID = " + subledgerid;
                    if (dtRecBudgetSubwithLedgerRecords.DefaultView.Count == 1)
                    {
                        dtRecBudgetSubwithLedgerRecords.DefaultView[0]["IS_ALLOTED"] = -1;
                        dtRecBudgetSubwithLedgerRecords.AcceptChanges();
                    }
                    dtRecBudgetSubwithLedgerRecords.DefaultView.RowFilter = string.Empty;
                    BindBudgetGrid(BudgetMainGroup.Recurring);
                }

            }

        }

        /// <summary>
        /// Show Sub Ledger creation/modify panel
        /// </summary>
        private void DoAddSubLedger()
        {
            gvExpenseLedger.PostEditor();
            gvExpenseLedger.UpdateCurrentRow();
            DataTable dtNonRecuring = gcNonRecuringGridSource.DataSource as DataTable;
            int RowIndex = gvExpenseLedger.FocusedRowHandle;
            string newsubledgername = gvExpenseLedger.GetRowCellValue(gvExpenseLedger.FocusedRowHandle, colNRNewSubLedgerName.FieldName).ToString();
            double newsubledgerproposeamt = UtilityMember.NumberSet.ToDouble(gvExpenseLedger.GetRowCellValue(gvExpenseLedger.FocusedRowHandle, colNRNewSubLedgerAmount.FieldName).ToString());
            double newsubledgernextmonthproposeamt = UtilityMember.NumberSet.ToDouble(gvExpenseLedger.GetRowCellValue(gvExpenseLedger.FocusedRowHandle, colNRNewNextMonthSubLedgerAmount.FieldName).ToString());
            string newsubledgernarration = gvExpenseLedger.GetRowCellValue(gvExpenseLedger.FocusedRowHandle, colNRNewSubLedgerNarration.FieldName).ToString();
            if (!string.IsNullOrEmpty(newsubledgername))
            {
                InsertNewSubLeger(BudgetMainGroup.NonRecurring, newsubledgername, newsubledgerproposeamt, newsubledgernextmonthproposeamt, newsubledgernarration);
            }
            gvExpenseLedger.OptionsBehavior.EditingMode = GridEditingMode.Default;
            FoucsNonRecGrid(RowIndex);
        }


        /// <summary>
        /// Focus to recurring grid
        /// </summary>
        /// <param name="RowIndex"></param>
        private void FoucsRecGrid(int RowIndex)
        {
            if (gvIncomeLedger.RowCount > 0)
            {
                gvIncomeLedger.Focus();
                gvIncomeLedger.FocusedRowHandle = RowIndex;
                gvIncomeLedger.ShowEditor();
            }
        }

        /// <summary>
        /// Focus to non recurring grid
        /// </summary>
        /// <param name="RowIndex"></param>
        private void FoucsNonRecGrid(int RowIndex)
        {
            if (gvExpenseLedger.RowCount > 0)
            {
                gvExpenseLedger.Focus();
                gvExpenseLedger.FocusedRowHandle = RowIndex;
                gvExpenseLedger.ShowEditor();
            }
        }


       
        #endregion
    }
}
