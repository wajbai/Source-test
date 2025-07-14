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
    public partial class frmAddMonthDistributionAnnualBudget : frmFinanceBaseAdd
    {
        #region Variables
        public const string ACTUAL = "ACTUAL";
        public const string PROPOSED_CURRENT_YR = "PROPOSED_CURRENT_YR";
        public const string APPROVED_CURRENT_YR = "APPROVED_CURRENT_YR";
        public const string APPROVED_PREVIOUS_YR = "APPROVED_PREVIOUS_YR";
        public const string LEDGER_ID = "LEDGER_ID";
        public const string NARRATION = "NARRATION";
        ResultArgs resultArgs = null;
        public event EventHandler UpdateHeld;
        SettingProperty setting = new SettingProperty();

        #endregion

        #region Properties
        private int BudgetId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectIds { get; set; }
        public string BudgetTypeId { get; set; }
        public string BudgetName { get; set; }

        private DateTime MonthFrom { get; set; }
        private DateTime MonthTo { get; set; }
        private DateTime YearFrom { get; set; }
        private DateTime YearTo { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Chinna 03.12.2019
        /// </summary>
        public frmAddMonthDistributionAnnualBudget()
        {
            InitializeComponent();

            RealColumnEditBudgetExpenseAmount();
        }

        /// <summary>
        /// Chinna 03.12.2019
        /// </summary>
        /// <param name="BudgetId"></param>
        /// <param name="ProjectId"></param>
        /// <param name="budgetTypeId"></param>
        public frmAddMonthDistributionAnnualBudget(int BudgetId, string ProjectId, string budgetTypeId, string budgetNamedata)
            : this()
        {
            this.BudgetId = BudgetId;
            this.ProjectIds = ProjectId;
            this.BudgetTypeId = budgetTypeId;
            this.BudgetName = budgetNamedata;

            FillBudgetInformation();
        }

        #endregion

        #region Events
        /// <summary>
        /// Chinna 03.12.2019
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAnnualBudget_Load(object sender, EventArgs e)
        {
            LoadDefaults();
        }

        /// <summary>
        /// Chinna 03.12.2019
        /// </summary>
        private void AssignValues()
        {
            SetBudgetDetails();
        }

        /// <summary>
        /// To enter the amount one by one Chinna 03.12.2019
        /// </summary>
        private void RealColumnEditBudgetExpenseAmount()
        {
            gccolApprovedMonthcurrentYR.RealColumnEdit.EditValueChanged += new EventHandler(RealColumnEditTransExpenseAmount_EditValueChanged);
            this.bgvBudget.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = bgvBudget.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == gccolApprovedMonthcurrentYR)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        bgvBudget.ShowEditorByMouse();
                    }));
                }
            };
        }

        /// <summary>
        /// Chinna 03.12.2019
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcAnnualBudget_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control &&
                (bgvBudget.FocusedColumn == gccolApprovedMonthcurrentYR))
            {
                bgvBudget.UpdateCurrentRow();
            }
        }

        /// <summary>
        /// Chinna 03.12.2019
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvAnnualBudget_CustomDrawRowFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            if (e.Column == gccolexpenseLedgerName)
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
        }

        private void pePrintBudget_Click(object sender, EventArgs e)
        {
            if (gcExpenseLedger.DataSource != null && gcExpenseLedger.DataSource != null)
            {
                if (BudgetId > 0)
                {
                    Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry(this.MdiParent);
                    report.ShowBudgetMonthDistribution(BudgetId, ProjectIds, bgvBudget, YearFrom, YearTo, MonthFrom, MonthTo, BudgetName);
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
                DataTable dtExpenseSource = gcExpenseLedger.DataSource as DataTable;

                using (BudgetSystem budgetSystem = new BudgetSystem())
                {
                    budgetSystem.dtBudgetDistribution = dtExpenseSource;
                    budgetSystem.BudgetId = BudgetId;
                    budgetSystem.DateFrom = MonthFrom.ToShortDateString();

                    DataView dvRecords = new DataView(budgetSystem.dtBudgetDistribution);
                    dvRecords.RowFilter = "MONTH_PROPOSED_BUDGET_AMOUNT>0 OR MONTH_APPROVED_BUDGET_AMOUNT>0";

                    dtExpenseSource = dvRecords.ToTable();
                    if (dtExpenseSource != null && dtExpenseSource.Rows.Count > 0)
                    {
                        budgetSystem.dtBudgetDistribution = dtExpenseSource;
                        this.ShowWaitDialog();

                        resultArgs = budgetSystem.SaveMonthDistributionBudget();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            ShowSuccessMessage(GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            if (UpdateHeld != null)
                                UpdateHeld(this, e);
                            LoadDefaults();
                        }
                        this.CloseWaitDialog();
                    }
                    else
                    {
                        ShowMessageBox("No Proposed Amount to Save the Budget");
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Chinna 03.12.2019
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
            bgvBudget.OptionsView.ShowAutoFilterRow = chkFilter.Checked;
            if (chkFilter.Checked)
            {
                this.SetFocusRowFilter(bgvBudget, gccolexpenseLedgerName);
            }
        }

        private void gcExpenseLedger_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control &&
                (bgvBudget.FocusedColumn == gccolApprovedMonthcurrentYR))
            {
                bgvBudget.UpdateCurrentRow();
            }
        }

        #endregion

        #region Methods
        private void LoadDefaults()
        {
            LoadMonths();
            if (BudgetId > 0)
            {
                AssignValues();
            }
        }

        /// <summary>
        /// Chinna 03.12.2019
        /// </summary>
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
            string Previous = new DateTime(this.UtilityMember.DateSet.ToDate(MonthFrom.ToString(), false).Year, this.UtilityMember.DateSet.ToDate(MonthFrom.ToString(), false).AddMonths(-1).Month, this.UtilityMember.DateSet.ToDate(MonthFrom.ToString(), false).Day).ToString("MMM", CultureInfo.InvariantCulture);
            string Current = new DateTime(this.UtilityMember.DateSet.ToDate(MonthTo.ToString(), false).Year, this.UtilityMember.DateSet.ToDate(MonthTo.ToString(), false).Month, this.UtilityMember.DateSet.ToDate(MonthTo.ToString(), false).Day).ToString("MMM", CultureInfo.InvariantCulture);

            string MonthPreviousYear = String.Format("{0}-{1}", Previous, UtilityMember.DateSet.ToDate(MonthFrom.ToString(), true).AddMonths(-1).Year);
            string MonthCurrentYear = String.Format("{0}-{1}", Current, UtilityMember.DateSet.ToDate(MonthTo.ToString(), true).Year);

            banColCurrentMonth.Caption = String.Format(MonthCurrentYear);
            banColPreviousMonth.Caption = string.Format("Expense for {0}", MonthPreviousYear);

            string FinCalPerPreviousYear = String.Format("{0}-{1}", UtilityMember.DateSet.ToDate(AppSetting.YearFrom, true).AddYears(-1).Year,
                    UtilityMember.DateSet.ToDate(AppSetting.YearTo, true).AddYears(-1).ToString("yy"));
            string FinCalPerioCurrentYear = String.Format("{0}-{1}", UtilityMember.DateSet.ToDate(AppSetting.YearFrom, true).Year,
                UtilityMember.DateSet.ToDate(AppSetting.YearTo, true).ToString("yy"));

            banColAnnual.Caption = String.Format("Expense for {0}", FinCalPerioCurrentYear);
            lcBudgetGroup.Text = String.Format("Budget for {0}", FinCalPerioCurrentYear);
        }

        /// <summary>
        /// Chinna 03.12.2019
        /// </summary>
        private void SetBudgetDetails()
        {
            using (BudgetSystem budgetAnnual = new BudgetSystem())
            {
                budgetAnnual.BudgetId = BudgetId;
                budgetAnnual.MultipleProjectId = ProjectIds;
                budgetAnnual.BudgetTypeId = (int)BudgetType.BudgetByAnnualYear;
                budgetAnnual.DateFrom = MonthFrom.ToShortDateString();
                budgetAnnual.DateTo = MonthTo.ToShortDateString();
                budgetAnnual.BudgetTransMode = TransactionMode.DR.ToString();

                resultArgs = budgetAnnual.FetchMonthDitributionbyAnnual();
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

        /// <summary>
        /// Chinna 03.12.2019
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkpMonth_EditValueChanged(object sender, EventArgs e)
        {
            FillDatasource();
        }

        private void FillDatasource()
        {
            var startDate = new DateTime(this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Year, this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Month, this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Day);

            DateTime SelectedMonth = Convert.ToDateTime(glkpMonth.EditValue.ToString());

            MonthFrom = this.UtilityMember.DateSet.ToDate(SelectedMonth.ToShortDateString(), false);
            MonthTo = MonthFrom.AddMonths(1).AddDays(-1);

            FillBudgetInformation();

            SetBudgetDetails();

            SetTitle();
        }

        private bool IsFormValid()
        {
            bool IsValid = true;
            if (BudgetId == 0)
            {
                IsValid = false;
            }
            else if (!IsValidDataGrid(IsValid))
            {
                IsValid = false;
            }
            return IsValid;
        }

        private bool IsValidDataGrid(bool Istrue)
        {
            bool isValid = true;

            if (Istrue)
            {
                if (BudgetId == 0)
                {
                    DataTable dtExpenseSource = gcExpenseLedger.DataSource as DataTable;
                    DataTable dtCommonsSource = new DataTable();
                    dtCommonsSource = dtExpenseSource;
                    if (dtExpenseSource != null)
                    {
                        dtCommonsSource.Merge(dtExpenseSource);
                    }
                    DataView dvRecords = new DataView(dtCommonsSource);
                    dvRecords.RowFilter = "(MONTH_APPROVED_BUDGET_AMOUNT>0)";
                    if (dvRecords.Count <= 0)
                    {
                        if (this.ShowConfirmationMessage("Approved Amount are Zero, Do you want to continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            bgvBudget.FocusedColumn = gccolApprovedMonthcurrentYR;
                            isValid = false;
                        }
                    }
                    DataTable dtex = gcExpenseLedger.DataSource as DataTable;
                }
            }
            return isValid;
        }

        private void RealColumnEditTransExpenseAmount_EditValueChanged(object sender, EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            bgvBudget.PostEditor();
            bgvBudget.UpdateCurrentRow();
            if (bgvBudget.ActiveEditor == null)
            {
                bgvBudget.ShowEditor();
            }
        }

        private void FillBudgetInformation()
        {
            using (BudgetSystem budgetsystem = new BudgetSystem(BudgetId, BudgetTypeId))
            {
                lcBudgetGroup.Text = budgetsystem.BudgetName + " (" + UtilityMember.DateSet.ToDate(budgetsystem.DateFrom) + "-" + UtilityMember.DateSet.ToDate(budgetsystem.DateTo) + ") - Monthly Distribution for " + glkpMonth.Text;
                YearFrom = UtilityMember.DateSet.ToDate(budgetsystem.DateFrom, false);
                YearTo = UtilityMember.DateSet.ToDate(budgetsystem.DateTo, false);
            }

            //Assign Selected Month Column for selected month and Budget
        }
        #endregion

        private void peDelete_Click(object sender, EventArgs e)
        {
            DataTable dtExpenseSource = gcExpenseLedger.DataSource as DataTable;
            DataView dvRecords = new DataView(dtExpenseSource);
            dvRecords.RowFilter = "(MONTH_APPROVED_BUDGET_AMOUNT>0)";
            try
            {
                if (dvRecords.Count != 0)
                {
                    if (BudgetId != 0)
                    {
                        if (this.ShowConfirmationMessage("Do you want to delete the Current Month Budget?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            using (BudgetSystem budgetSystem = new BudgetSystem())
                            {
                                budgetSystem.BudgetId = BudgetId;

                                DateTime SelectedMonth = Convert.ToDateTime(glkpMonth.EditValue.ToString());

                                MonthFrom = this.UtilityMember.DateSet.ToDate(SelectedMonth.ToShortDateString(), false);
                                budgetSystem.VoucherDateFrom = MonthFrom;

                                resultArgs = budgetSystem.DeleteAnnualBudgetDistribution();
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    FillDatasource();
                                }
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NOSELECTION_FOR_DELETE));
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
    }
}
