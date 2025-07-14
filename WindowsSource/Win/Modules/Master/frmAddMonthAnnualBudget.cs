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
    public partial class frmAddMonthAnnualBudget : frmFinanceBaseAdd
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

        #endregion

        #region Constructor

        public frmAddMonthAnnualBudget()
        {
            InitializeComponent();

            RealColumnEditBudgetExpenseAmount();
        }

        public frmAddMonthAnnualBudget(int BudgetId, string ProjectId)
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
        }

        private void AssignValues()
        {
            if (BudgetId > 0)
                SetBudgetEdit();
            else
            {
                using (BudgetSystem budgetSystem = new BudgetSystem())
                {
                    resultArgs = budgetSystem.GetAnnualBudget();
                    int Id = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["PROJECT_ID"].ToString());
                    string ProjectNames = resultArgs.DataSource.Table.Rows[0]["PROJECT"].ToString();

                    if (Id == 0)
                    {
                        FetchBudgetDetails();
                    }
                    else
                    {
                        if (this.AppSetting.IS_ABEBEN_DIOCESE)
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
        }

        private void GetLastMonthProperty()
        {
            if (this.UtilityMember.NumberSet.ToInteger(BudgetTypeId) == (int)BudgetType.BudgetMonth)
            {
                if (BudgetId == 0)
                {
                    using (BudgetSystem budgetsystem = new BudgetSystem())
                    {
                        //budgetsystem.MultipleProjectId = GridselectedProjectIds;
                        resultArgs = budgetsystem.FetchLastMonthBudget();
                        if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            // DateTime dtValue = resultArgs.DataSource.Table.Rows[0]["NEXT_DATE"] == DBNull.Value ? dePeriodFrom.DateTime : this.UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["NEXT_DATE"].ToString(), false);

                            // string Current = new DateTime(this.UtilityMember.DateSet.ToDate(dtValue.ToString(), false).Year, this.UtilityMember.DateSet.ToDate(dtValue.ToString(), false).AddMonths(1).Month, this.UtilityMember.DateSet.ToDate(dtValue.ToString(), false).Day).ToString("MMM", CultureInfo.InvariantCulture);
                            //  string MonthCurrentYear = String.Format("{0}-{1}", Current, UtilityMember.DateSet.ToDate(dtValue.ToString(), true).AddMonths(1).Year);
                            //   glkpMonth.Text = MonthCurrentYear;
                        }
                        else
                        {
                            LoadMonths();
                        }
                    }
                }
            }
        }

        private void meNote_Leave(object sender, EventArgs e)
        {
            btnSave.Select();
            btnSave.Focus();
        }

        private void gcAnnualBudget_ProcessGridKey(object sender, KeyEventArgs e)
        {
            //if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control &&
            //    (gvIncomeLedger.FocusedColumn == gccolIncomeCurrentYearProposed))
            //{
            //    gvIncomeLedger.UpdateCurrentRow();
            //}
        }

        private void gvAnnualBudget_CustomDrawRowFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            //if (e.Column == gccolIncomeLedger)
            //{
            //    e.Info.DisplayText = "Total ";
            //}
            //else
            //{
            //    e.Info.DisplayText = string.Format("{0:n}", e.Info.Value);
            //}
            e.Appearance.DrawString(e.Cache, e.Info.DisplayText, e.Bounds, new SolidBrush(Color.Black));
            e.Handled = true;
        }

        private void RealColumnEditTransAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
        }

        private void peExportExcel_Click(object sender, EventArgs e)
        {
            // Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry(this.MdiParent);
            // report.ShowBudgetView1(BudgetId, txtBudgetName.Text, gvIncomeLedger);
        }

        private void pePrintBudget_Click(object sender, EventArgs e)
        {
            //if (gcIncomeLedger.DataSource != null && gcIncomeLedger.DataSource != null)
            //{
            //    if (BudgetId > 0)
            //    {
            //        MessageRender.ShowMessage("Proposed and Actual Budget Ledger will be Printed / Exported");
            //        Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry(this.MdiParent);
            //        report.ShowBudgetView1(BudgetId, txtBudgetName.Text, gvIncomeLedger);
            //    }
            //    else
            //    {
            //        //MessageRender.ShowMessage("Save Budget details and print");
            //        //this.PrintGridViewDetails(gcExpenseLedger, "Budget View - ", PrintType.DT, gvExpenseLedger);
            //        string projectnames = GridselectedProjects;
            //        string RpTitle = (string.IsNullOrEmpty(txtBudgetName.Text) ? "Budget" : txtBudgetName.Text);
            //        if (lcIncomeBudgetLedgers.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            //        {
            //            string AdditionTitle = "Budget Income Ledgers" + System.Environment.NewLine +
            //                          "Budget Expense Ledgers" + System.Environment.NewLine +
            //                          MessageCatalog.ReportCommonTitle.PERIOD + " " + this.dePeriodFrom.Text + " - " + this.dePeriodTo.Text + System.Environment.NewLine +
            //                          RpTitle;
            //            this.PrintGridViewDetails(gvIncomeLedger, projectnames, gvExpenseLedger, AdditionTitle);
            //        }
            //        else
            //        {
            //            string AdditionTitle = "Budget Expense Ledgers" + System.Environment.NewLine +
            //                          string.Empty + System.Environment.NewLine +
            //                          MessageCatalog.ReportCommonTitle.PERIOD + " " + this.dePeriodFrom.Text + " - " + this.dePeriodTo.Text + System.Environment.NewLine +
            //                          RpTitle;
            //            this.PrintGridViewDetails(gvIncomeLedger, projectnames, null, AdditionTitle);
            //        }
            //    }
            //}
            //else
            // {
            //  MessageRender.ShowMessage("Budget Ledgers are not available");
            // }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsFormValid())
            {
                DataTable dtExpenseSource = gcExpenseLedger.DataSource as DataTable;

                using (BudgetSystem budgetSystem = new BudgetSystem())
                {
                    budgetSystem.dtBudgetLedgers = dtExpenseSource;
                    budgetSystem.dtBudgetStatisticsDetails = dtBudgetStatisticsRecords;
                    budgetSystem.BudgetId = BudgetId;
                    // budgetSystem.BudgetName = txtBudgetName.Text;
                    //  budgetSystem.DateFrom = dePeriodFrom.DateTime.ToShortDateString();
                    // budgetSystem.DateTo = dePeriodTo.DateTime.ToShortDateString();
                    // budgetSystem.BudgetTypeId = UtilityMember.NumberSet.ToInteger(glkpBudgetType.EditValue.ToString());
                    budgetSystem.BudgetLevelId = 1;
                    budgetSystem.monthwiseDistribution = 0;
                    // budgetSystem.MultipleProjectId = GridselectedProjectIds;
                    //  budgetSystem.HOHelpPropsedAmount = UtilityMember.NumberSet.ToDecimal(txtHOHelpPropsedAmount.Text);
                    //  budgetSystem.HOHelpApprovedAmount = UtilityMember.NumberSet.ToDecimal(txtHOHelpApprovedAmount.Text);
                    //if ((!this.AppSetting.IS_ABEBEN_DIOCESE))
                    //{
                    //    budgetSystem.Status = chkActive.Checked ? 1 : 0;
                    //}
                    //  else { budgetSystem.Status = 1; }

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
                            BudgetId = UtilityMember.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                            //if (this.AppSetting.IS_ABEBEN_DIOCESE || this.AppSetting.IS_DIOMYS_DIOCESE)
                            //    lcImportBudget.Visibility = lcExportBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        }
                    }
                    this.CloseWaitDialog();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvExpenseLedger.OptionsView.ShowAutoFilterRow = chkFilter.Checked;
            if (chkFilter.Checked)
            {
                this.SetFocusRowFilter(gvExpenseLedger, gccolexpenseLedgerName);
            }
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
                        //if (year == UtilityMember.DateSet.ToDate(setting.YearTo, false).Year)
                        //dePeriodFrom.DateTime = PeriodFrom = new DateTime(year - 1, monthcount, day);
                        // else
                        // dePeriodFrom.DateTime = PeriodFrom = new DateTime(year, monthcount, day);
                        // dePeriodFrom.Enabled = false;
                    }
                    else
                    {
                        // dePeriodFrom.Enabled = true;
                        // dePeriodFrom.DateTime = new DateTime(UtilityMember.DateSet.ToDate(dePeriodFrom.DateTime.ToString(), false).Year, 1, 1);
                        // dePeriodTo.DateTime = new DateTime(UtilityMember.DateSet.ToDate(dePeriodFrom.DateTime.ToString(), false).Year, 12, 31);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
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

        private void gcExpenseLedger_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control &&
                (gvExpenseLedger.FocusedColumn == gccolexpenseproposedcurrentYR))
            {
                gvExpenseLedger.UpdateCurrentRow();
            }
        }

        private void btnExportBudget_Click(object sender, EventArgs e)
        {
            if (this.ShowConfirmationMessage("Are you sure to export Budget to Head Office Portal ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                this.ShowWaitDialog("Uploading Budget from Head Office Portal");
                ResultArgs result = this.UploadBudgetToAcmeerpPortal(BudgetId);
                this.CloseWaitDialog();
                if (result.Success)
                {
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
                    AssignValues();
                    GetLastMonthProperty();
                }
                else
                {
                    this.CloseWaitDialog();
                    this.ShowMessageBoxError(result.Message);
                }
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
            FetchUpdateBudgetStatisticDetails();
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

        private void FetchBudgetDetails()
        {
            using (BudgetSystem budgetAnnual = new BudgetSystem())
            {
                budgetAnnual.BudgetId = (int)AddNewRow.NewRow;
                //budgetAnnual.MultipleProjectId = GridselectedProjectIds;
                // budgetAnnual.DateFrom = dePeriodFrom.DateTime.ToString();
                // budgetAnnual.DateTo = dePeriodTo.DateTime.ToString();
                //budgetAnnual.BudgetTypeId = UtilityMember.NumberSet.ToInteger(glkpBudgetType.EditValue.ToString());
                budgetAnnual.BudgetTransMode = TransactionMode.CR.ToString();
                resultArgs = budgetAnnual.FetchBudgetAdd();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    if (resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable IncomeSource = resultArgs.DataSource.Table;

                        DataTable dtIncomeGridSource = new DataTable();
                        // dtIncomeGridSource = gcIncomeLedger.DataSource as DataTable;

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
                            // gcIncomeLedger.DataSource = dtSourceGrid;
                            // gcIncomeLedger.RefreshDataSource();
                        }
                        else
                        {
                            //gcIncomeLedger.DataSource = IncomeSource;
                        }
                    }
                }
                else ShowMessageBox(resultArgs.Message);


                //Expense
                //budgetAnnual.MultipleProjectId = GridselectedProjectIds;
                // budgetAnnual.DateFrom = dePeriodFrom.DateTime.ToString();
                // budgetAnnual.DateTo = dePeriodTo.DateTime.ToString();
                // budgetAnnual.BudgetTypeId = UtilityMember.NumberSet.ToInteger(glkpBudgetType.EditValue.ToString());
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
                }
                else ShowMessageBox(resultArgs.Message);
            }
        }

        private void SetBudgetEdit()
        {
            using (BudgetSystem budgetAnnual = new BudgetSystem(BudgetId, BudgetTypeId))
            {
                budgetAnnual.BudgetId = BudgetId;

                budgetAnnual.BudgetTransMode = TransactionMode.CR.ToString();

                resultArgs = budgetAnnual.FetchBudgetEdit();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    if (resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable IncomeSource = resultArgs.DataSource.Table;
                        // gcIncomeLedger.DataSource = IncomeSource;
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

            // if (UtilityMember.NumberSet.ToInteger(glkpBudgetType.EditValue.ToString()) == (int)BudgetType.BudgetMonth)
            //{
            //   lblPeriodFrom.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            // lblPeriodTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //  dePeriodTo.Enabled = dePeriodFrom.Enabled = false;

            // dePeriodFrom.DateTime = this.UtilityMember.DateSet.ToDate(SelectedMonth.ToShortDateString(), false);
            // dePeriodTo.DateTime = dePeriodFrom.DateTime.AddMonths(1).AddDays(-1);
            //}
        }

        private bool IsFormValid()
        {
            bool IsValid = true;
            if (!IsValidDataGrid(IsValid))
            {
                IsValid = false;
            }
            else if (!isValidActiveBudget())
            {
                IsValid = false;
            }
            return IsValid;
        }

        private bool isValidActiveBudget()
        {
            bool isValid = true;
            //if (!this.AppSetting.IS_ABEBEN_DIOCESE)
            //{
            //    using (BudgetSystem budgetSystem = new BudgetSystem())
            //    {
            //        if (budgetSystem.BudgetTypeId != (int)BudgetType.BudgetMonth)
            //        {
            //            if (budgetSystem.BudgetTypeId != (int)BudgetType.BudgetPeriod)
            //            {
            //                if (chkActive.Checked)
            //                {
            //                    budgetSystem.MultipleProjectId = GridselectedProjectIds;
            //                    int ActiveBudgetId = budgetSystem.CheckStatus();
            //                    if (!ActiveBudgetId.Equals(BudgetId))
            //                    {
            //                        if (this.ShowConfirmationMessage("Budget already exists for selected Project, Type and Date range, are you sure to save this budget as In-Active mode?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            //                        {
            //                            if (!ActiveBudgetId.Equals(0))
            //                            {
            //                                budgetSystem.Status = 0;
            //                                chkActive.Checked = false;
            //                                ShowMessageBox(this.GetMessage(MessageCatalog.Master.Budget.ACTIVE_BUDGET_IS_MADE));
            //                            }
            //                        }
            //                        else
            //                        {
            //                            isValid = false;
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            return isValid;
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
                    dvRecords.RowFilter = "(APPROVED_CURRENT_YR>0)";
                    if (dvRecords.Count <= 0)
                    {
                        if (this.ShowConfirmationMessage("Approved Amount are Zero, Do you want to continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            gvExpenseLedger.FocusedColumn = gccolexpenseapprovedcurrentYR;
                            isValid = false;
                        }
                    }
                    DataTable dtex = gcExpenseLedger.DataSource as DataTable;
                }
            }
            return isValid;
        }

        private void ExportBudget()
        {
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
                    dtExpense = gcExpenseLedger.DataSource as DataTable;
                    dtCommon.Merge(dtExpense);
                    gcExpenseLedger.DataSource = dtCommon;
                    gcExpenseLedger.ExportToXlsx(save.FileName);
                    if (ShowConfirmationMessage(this.GetMessage(MessageCatalog.Master.Budget.OPEN_THE_FILE), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        System.Diagnostics.Process.Start(save.FileName);
                    }
                    gcExpenseLedger.DataSource = dtExpense;
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

        private void RealColumnEditBudgetExpenseAmount()
        {
            gccolexpenseproposedcurrentYR.RealColumnEdit.EditValueChanged += new EventHandler(RealColumnEditTransExpenseAmount_EditValueChanged);
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
            DataView dvBudgetExpenseLedger = (DataView)gvExpenseLedger.DataSource;
            try
            {
                if (dvBudgetExpenseLedger != null && dvBudgetExpenseLedger != null)
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

            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
            finally
            {
                if (dvBudgetExpenseLedger != null)
                {
                    dvBudgetExpenseLedger.RowFilter = string.Empty;
                }
            }
            this.Cursor = Cursors.Default;
        }

        private string GetExcelColumn(string columnname, DataTable dtExcel)
        {
            string Rtn = string.Empty;
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

        #endregion

    }
}
