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

namespace ACPP.Modules.Master
{
    public partial class frmAnnualBudget : frmFinanceBaseAdd
    {
        #region Variables
        public const string NATURE = "NATURE";
        public const string INCOMES = "Incomes";
        public const string EXPENSES = "Expenses";
        public const string ASSETS = "Assets";
        public const string LIABILLITES = "Liabillites";
        public const string PROPOSED_INCOME_PREVIOUS_YR = "PROPOSED_INCOME_PREVIOUS_YR";
        public const string ACTUAL_INCOME = "ACTUAL_INCOME";
        public const string PROPOSED_INCOME_CURRENT_YR = "PROPOSED_INCOME_CURRENT_YR";
        ResultArgs resultArgs = null;
        public event EventHandler UpdateHeld;
        private DataSet dsCostCentre = new DataSet();

        SettingProperty setting = new SettingProperty();

        #endregion

        #region Properties
        private int BudgetId { get; set; }
        public int ProjectId { get; set; }
        public string BudgetTypeId { get; set; }
        private DateTime PeriodFrom { get; set; }
        private int LedgerId
        {
            get
            {
                int ledgerId = 0;
                ledgerId = gvAnnualBudget.GetFocusedRowCellValue(colLedgerId) != null ? this.UtilityMember.NumberSet.ToInteger(gvAnnualBudget.GetRowCellValue(gvAnnualBudget.FocusedRowHandle, colLedgerId).ToString()) : 0;
                return ledgerId;
            }
        }
        private double LedgerAmount
        {
            get
            {
                double ledgerAmount;
                ledgerAmount = gvAnnualBudget.GetFocusedRowCellValue(colProposedIncomeCurrentYear) != null ? this.UtilityMember.NumberSet.ToDouble(gvAnnualBudget.GetFocusedRowCellValue(colProposedIncomeCurrentYear).ToString()) : 0;
                return ledgerAmount;
            }
        }
        #endregion

        #region Constructor
        public frmAnnualBudget()
        {
            InitializeComponent();
            RealColumnEditBudgetAmount();
        }
        public frmAnnualBudget(int BudgetId, int ProjectId)
            : this()
        {
            this.BudgetId = BudgetId;
            this.ProjectId = ProjectId;
        }
        #endregion

        #region Events
        private void frmAnnualBudget_Load(object sender, EventArgs e)
        {
            LoadDefaults();
            //AssignValues();
            dePeriodTo.Enabled = false;
        }

        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            if (glkpProject.EditValue != null)
            {
                txtPercentage.Enabled = btnApply.Enabled = true;
                ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
            }
            AssignValues();
            gvAnnualBudget.ExpandAllGroups();
            //Binding the Budget Summary
            BindSummaryValue();
        }

        private void AssignValues()
        {
            if (BudgetId > 0)
                SetBudgetEdit();
            else
            {
                using (BudgetSystem budgetSystem = new BudgetSystem())
                {
                    budgetSystem.ProjectId = ProjectId;
                    budgetSystem.BudgetTypeId = UtilityMember.NumberSet.ToInteger(glkpBudgetType.EditValue.ToString());
                    budgetSystem.DateFrom = dePeriodFrom.DateTime.ToString();
                    budgetSystem.DateTo = dePeriodTo.DateTime.ToString();
                    //if (budgetSystem.GetAnnualBudget() == 0)
                    //{
                    //    SetBudgetAdd();
                    //}
                    //else
                    //{
                    //    txtPercentage.Enabled = btnApply.Enabled = false;
                    //    gcAnnualBudget.DataSource = null;
                    //    ShowMessageBox(GetMessage(MessageCatalog.Master.Budget.BUDGET_ALREADY_MADE));
                    //}
                }
            }
        }

        private void txtBudgetName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtBudgetName);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (gvAnnualBudget.RowCount > 0)
            {
                SetPercentage();
                //Binding the Budget Summary
                BindSummaryValue();
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
            //if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control)
            //{
            //    if (gvAnnualBudget.IsLastRow)
            //        //meNote.Focus();
            //}

            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control &&
                (gvAnnualBudget.FocusedColumn == colProposedIncomeCurrentYear))
            {
                gvAnnualBudget.UpdateCurrentRow();

                // To show cost centre form.
                if (gvAnnualBudget.FocusedColumn == colProposedIncomeCurrentYear && LedgerAmount > 0 && LedgerId > 0)
                {

                    ShowCostCentre(this.UtilityMember.NumberSet.ToDouble(LedgerAmount.ToString()));
                }
            }
        }

        private void gvAnnualBudget_CustomDrawRowFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            if (e.Column == colLedgerName)
            {
                e.Info.DisplayText = "Total ";
            }
            else
            {
                e.Info.DisplayText = string.Format("{0:n}", e.Info.Value);
            }
            e.Appearance.DrawString(e.Cache, e.Info.DisplayText, e.Bounds, new SolidBrush(Color.Black));
            //Prevent default painting
            e.Handled = true;
        }

        private void RealColumnEditTransAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvAnnualBudget.PostEditor();
            gvAnnualBudget.UpdateCurrentRow();
            if (gvAnnualBudget.ActiveEditor == null)
            {
                gvAnnualBudget.ShowEditor();
            }
            DataTable dtBudgetValue = gcAnnualBudget.DataSource as DataTable;
            if (dtBudgetValue != null && dtBudgetValue.Rows.Count > 0)
            {
                DataTable dtSummary = gcBudgetSummary.DataSource as DataTable;
                int NatureId = UtilityMember.NumberSet.ToInteger(gvAnnualBudget.GetRowCellValue(gvAnnualBudget.FocusedRowHandle, colNatureId.FieldName).ToString());
                switch (NatureId)
                {
                    case 1: //Incomes
                        dtSummary.Rows[0][PROPOSED_INCOME_CURRENT_YR] = dtBudgetValue.Compute("SUM(PROPOSED_INCOME_CURRENT_YR)", "NATURE_ID=1");
                        break;
                    case 2://Expenes
                        dtSummary.Rows[1][PROPOSED_INCOME_CURRENT_YR] = dtBudgetValue.Compute("SUM(PROPOSED_INCOME_CURRENT_YR)", "NATURE_ID=2");
                        break;
                    case 3://Assets
                        dtSummary.Rows[2][PROPOSED_INCOME_CURRENT_YR] = dtBudgetValue.Compute("SUM(PROPOSED_INCOME_CURRENT_YR)", "NATURE_ID=3");
                        break;
                    case 4://Liabilities
                        dtSummary.Rows[3][PROPOSED_INCOME_CURRENT_YR] = dtBudgetValue.Compute("SUM(PROPOSED_INCOME_CURRENT_YR)", "NATURE_ID=4");
                        break;
                }
                lblTotalBudgetAmount.Text = UtilityMember.NumberSet.ToCurrency(UtilityMember.NumberSet.ToDouble(dtBudgetValue.Compute("SUM(PROPOSED_INCOME_CURRENT_YR)", string.Empty).ToString()));
                lblTotalBudgetAmount.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 15F);
                gcBudgetSummary.DataSource = dtSummary;
            }
        }

        private void peExportExcel_Click(object sender, EventArgs e)
        {
            if (gvAnnualBudget.RowCount > 0)
            {
                ExportBudget();
            }
            else
            {
                ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NO_RECORDS_TO_EXPORT));
            }
        }

        private void pePrintBudget_Click(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcAnnualBudget, this.GetMessage(MessageCatalog.Master.Budget.ANNUAL_BUDGET), PrintType.DT, gvAnnualBudget);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsFormValid())
            {
                DataTable dtAnnualBudget = gcAnnualBudget.DataSource as DataTable;
                if (dtAnnualBudget != null && dtAnnualBudget.Rows.Count > 0)
                {
                    using (BudgetSystem budgetSystem = new BudgetSystem())
                    {
                        budgetSystem.dtBudgetLedgers = dtAnnualBudget;
                        budgetSystem.BudgetId = BudgetId;
                        budgetSystem.BudgetName = txtBudgetName.Text;
                        budgetSystem.DateFrom = dePeriodFrom.DateTime.ToShortDateString();
                        budgetSystem.DateTo = dePeriodTo.DateTime.ToShortDateString();
                        budgetSystem.BudgetTypeId = UtilityMember.NumberSet.ToInteger(glkpBudgetType.EditValue.ToString());
                        // budgetSystem.Remarks = meNote.Text;
                        budgetSystem.ProjectId = this.ProjectId;
                        budgetSystem.Status = chkActive.Checked ? 1 : 0; ;

                        if (chkActive.Checked)
                        {
                            int ActiveBudgetId = budgetSystem.CheckStatus();
                            if (!ActiveBudgetId.Equals(BudgetId))
                            {
                                if (!ActiveBudgetId.Equals(0))
                                {
                                    budgetSystem.Status = 0;
                                    chkActive.Checked = false;
                                    ShowMessageBox(this.GetMessage(MessageCatalog.Master.Budget.ACTIVE_BUDGET_IS_MADE));
                                }
                            }
                        }
                        this.ShowWaitDialog();

                        //Cost Centre Details
                        this.Transaction.CostCenterInfo = dsCostCentre;

                        resultArgs = budgetSystem.SaveAnnualBudget();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            ShowSuccessMessage(GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            if (UpdateHeld != null)
                                UpdateHeld(this, e);
                            if (BudgetId == 0)
                                ClearControls();
                        }
                        this.CloseWaitDialog();
                    }
                }
                else
                {
                    ShowMessageBox(GetMessage(MessageCatalog.Common.COMMON_NO_RECORDS_TO_SAVE));
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvAnnualBudget_RowCountChanged(object sender, EventArgs e)
        {
            lblRowCount.Text = (gcAnnualBudget.DataSource as DataTable) == null ? "0" : (gcAnnualBudget.DataSource as DataTable).Rows.Count.ToString();
        }

        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvAnnualBudget.OptionsView.ShowAutoFilterRow = chkFilter.Checked;
            if (chkFilter.Checked)
            {
                this.SetFocusRowFilter(gvAnnualBudget, colLedgerName);
            }
        }
        #endregion

        #region Methods
        private void LoadDefaults()
        {
            SetTitle();
            LoadBudgetType();
            LoadProjectDetails();
        }

        private void SetTitle()
        {
            this.Text = BudgetId > 0 ? GetMessage(MessageCatalog.Master.Budget.BUDGET_ANNUAL_EDIT_CAPTION) : GetMessage(MessageCatalog.Master.Budget.BUDGET_ANNUAL_ADD_CAPTION);
            string PreviousYear = String.Format("{0}-{1}", UtilityMember.DateSet.ToDate(AppSetting.YearFrom, true).AddYears(-1).Year,
                UtilityMember.DateSet.ToDate(AppSetting.YearTo, true).AddYears(-1).ToString("yy"));
            string CurrentYear = String.Format("{0}-{1}", UtilityMember.DateSet.ToDate(AppSetting.YearFrom, true).Year,
                UtilityMember.DateSet.ToDate(AppSetting.YearTo, true).ToString("yy"));

            colProposedIncomeCurrentYear.Caption = String.Format("Proposed {0}", CurrentYear);
            colActualIncome.Caption = String.Format("Actual {0}", PreviousYear);
            colProposedIncomePreviousYear.Caption = String.Format("Proposed {0}", PreviousYear);
            lcBudgetGroup.Text = String.Format(this.GetMessage(MessageCatalog.Master.Budget.BUDGET_FOR), CurrentYear);
        }

        private void LoadBudgetType()
        {
            BudgetType budgetType = new BudgetType();
            DataView dvbudget = this.UtilityMember.EnumSet.GetEnumDataSource(budgetType, Sorting.Ascending);
            if (dvbudget.Count > 0)
            {
                dvbudget.RowFilter = "Id in (3,4)"; //3=Annual Budget which is defined in the enum, 4=Calender year Budget which is defined in the enum

                DataTable dtBudgetType = dvbudget.ToTable();
                string EnumValAnualYear = this.UtilityMember.EnumSet.GetDescriptionFromEnumValue(BudgetType.BudgetByAnnualYear);
                string EnumValCalenderYear = this.UtilityMember.EnumSet.GetDescriptionFromEnumValue(BudgetType.BudgetByCalendarYear);
                dtBudgetType.Rows[0]["Name"] = EnumValAnualYear;
                dtBudgetType.Rows[1]["Name"] = EnumValCalenderYear;

                glkpBudgetType.Properties.DataSource = dtBudgetType;
                glkpBudgetType.Properties.DisplayMember = "Name";
                glkpBudgetType.Properties.ValueMember = "Id";
                glkpBudgetType.EditValue = glkpBudgetType.Properties.GetKeyValue(0);
            }
        }

        private void SetBudgetAdd()
        {
            using (BudgetSystem budgetAnnual = new BudgetSystem())
            {
                budgetAnnual.ProjectId = ProjectId;
                budgetAnnual.DateFrom = dePeriodFrom.DateTime.ToString();
                budgetAnnual.DateTo = dePeriodTo.DateTime.ToString();
                budgetAnnual.BudgetTypeId = UtilityMember.NumberSet.ToInteger(glkpBudgetType.EditValue.ToString());
                resultArgs = null; // budgetAnnual.FetchAnnualBudgetAdd();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    gcAnnualBudget.DataSource = resultArgs.DataSource.Table;
                    gvAnnualBudget.ExpandAllGroups();
                }
                else ShowMessageBox(resultArgs.Message);

            }
        }

        private void SetBudgetEdit()
        {
            using (BudgetSystem budgetAnnual = new BudgetSystem(BudgetId, BudgetTypeId))
            {
                budgetAnnual.BudgetId = BudgetId;
                txtBudgetName.Text = budgetAnnual.BudgetName;
                this.glkpProject.EditValueChanged -= new System.EventHandler(this.glkpProject_EditValueChanged);
                glkpProject.EditValue = budgetAnnual.ProjectId;
                this.glkpBudgetType.EditValueChanged -= new System.EventHandler(this.glkpBudgetType_EditValueChanged);
                glkpBudgetType.EditValue = budgetAnnual.BudgetTypeId;
                glkpBudgetType.Enabled = false; // added by chinna
                dePeriodTo.DateTime = UtilityMember.DateSet.ToDate(budgetAnnual.DateTo, false);
                dePeriodFrom.DateTime = UtilityMember.DateSet.ToDate(budgetAnnual.DateFrom, false);
                this.glkpProject.EditValueChanged += new System.EventHandler(this.glkpProject_EditValueChanged);
                this.glkpBudgetType.EditValueChanged += new System.EventHandler(this.glkpBudgetType_EditValueChanged);
                chkActive.Checked = budgetAnnual.Status == 0 ? false : true;

                if (budgetAnnual.BudgetTypeId == (int)BudgetType.BudgetByCalendarYear)
                {
                    lblPeriodFrom.Visibility = lblPeriodTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    dePeriodFrom.Enabled = dePeriodTo.Enabled = false;
                }
                else
                    lblPeriodFrom.Visibility = lblPeriodTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                resultArgs = null; //budgetAnnual.FetchAnnualBudgetEdit();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    gcAnnualBudget.DataSource = resultArgs.DataSource.Table;
                }
                // DataTable dt = resultArgs.DataSource.Table;
                //dsCostCentre.Clear();

                //Commanded by chinna

                //if (dt != null && dt.Rows.Count > 0)
                // {
                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        int LedId = this.UtilityMember.NumberSet.ToInteger(dt.Rows[i][budgetAnnual.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                //        budgetAnnual.LedgerId = LedId;
                //        budgetAnnual.CostCenterTable = i + "LDR" + LedId;
                //        resultArgs = budgetAnnual.GetCostCentreDetails();
                //        if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                //        {
                //            DataTable CostCentreInfo = resultArgs.DataSource.Table;
                //            CostCentreInfo.TableName = i + "LDR" + LedId;
                //            if (CostCentreInfo != null) { dsCostCentre.Tables.Add(CostCentreInfo); }
                //        }
                //    }
                //}
                //else ShowMessageBox(resultArgs.Message);
            }
        }

        private void SetPercentage()
        {
            DataTable dtBudget = gcAnnualBudget.DataSource as DataTable;
            double Percentage = UtilityMember.NumberSet.ToDouble(txtPercentage.Text);

            if (Percentage > 0)
            {
                if (dtBudget != null)
                {

                    dtBudget.Select().ToList<DataRow>().ForEach(r => { r["PROPOSED_INCOME_CURRENT_YR"] = (UtilityMember.NumberSet.ToDouble(r["ACTUAL_INCOME"].ToString()) * Percentage) / 100; });
                    gcAnnualBudget.DataSource = dtBudget;
                }
            }
            else ShowMessageBox(GetMessage(MessageCatalog.Master.Budget.BUDGET_PERCENTAGE_VALIDATION));

        }

        private void LoadProjectDetails()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    resultArgs = mappingSystem.FetchProjectsLookup();
                    glkpProject.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                        glkpProject.EditValue = ProjectId == 0 ? 0 : ProjectId;
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
            if (string.IsNullOrEmpty(glkpProject.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Budget.BUDGET_PROJECT_EMPTY));
                this.SetBorderColorForGridLookUpEdit(glkpProject);
                IsValid = false;
                glkpProject.Focus();
            }
            else if (string.IsNullOrEmpty(txtBudgetName.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Budget.BUDGET_NAME_EMPTY));
                this.SetBorderColor(txtBudgetName);
                IsValid = false;
                txtBudgetName.Focus();
            }
            else if (BudgetId == 0)
            {
                using (BudgetSystem budgetSystemProject = new BudgetSystem())
                {
                    //budgetSystemProject.ProjectId = ProjectId;
                    //if (budgetSystemProject.GetAnnualBudget() != 0)
                    //{
                    //    IsValid = false;
                    //    ShowMessageBox(this.GetMessage(MessageCatalog.Master.Budget.BUDGET_ALREADY_MADE));
                    //}
                }
            }
            //To show the message in order it uses separate if condition

            return IsValid;
        }

        private void ClearControls()
        {
            txtBudgetName.Text = string.Empty;
            txtPercentage.Text = string.Empty;
            lblTotalBudgetAmount.Text = UtilityMember.NumberSet.ToCurrency(0);
            gcBudgetSummary.DataSource = ConstructSummaryTable();
            SetBudgetAdd();
        }

        private void ExportBudget()
        {
            DataTable dtOld;
            DataTable dt;
            try
            {
                SaveFileDialog save = new SaveFileDialog();
                save.FileName = String.Format(this.GetMessage(MessageCatalog.Master.Budget.ANNUAL_BUDGET), UtilityMember.DateSet.ToDate(AppSetting.YearFrom, true).Year);
                save.DefaultExt = ".xlsx";
                save.Filter = "Excel(.xlsx)|*.xlsx";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    dtOld = gcAnnualBudget.DataSource as DataTable;
                    dt = gcAnnualBudget.DataSource as DataTable;
                    dt = dt.AsEnumerable().Where(row => row.Field<decimal>("PROPOSED_INCOME_CURRENT_YR") > 0).CopyToDataTable();// .ForEach(r => { r["TOTAL"] = Amount; });
                    gcAnnualBudget.DataSource = dt;
                    gcAnnualBudget.ExportToXlsx(save.FileName);
                    gcAnnualBudget.DataSource = dtOld;
                    gvAnnualBudget.ExpandAllGroups();
                    if (ShowConfirmationMessage(this.GetMessage(MessageCatalog.Master.Budget.OPEN_THE_FILE), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        System.Diagnostics.Process.Start(save.FileName);
                    }
                }
            }
            catch (IOException)
            {
                //ShowMessageBox("File is already opened Close the file and try again");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Budget.FILE_OPEN_ALREADY_CLOSE));
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        private void RealColumnEditBudgetAmount()
        {
            colProposedIncomeCurrentYear.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditTransAmount_EditValueChanged);
            this.gvAnnualBudget.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvAnnualBudget.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colProposedIncomeCurrentYear)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvAnnualBudget.ShowEditorByMouse();
                    }));
                }
            };
        }

        private void BindSummaryValue()
        {
            DataTable dtSummary = gcBudgetSummary.DataSource as DataTable;
            if (dtSummary == null)
                dtSummary = ConstructSummaryTable();

            DataTable dtBudgetValue = gcAnnualBudget.DataSource as DataTable;
            if (dtSummary != null && dtSummary.Rows.Count == 4 && dtBudgetValue != null && dtBudgetValue.Rows.Count > 0)
            {
                decimal IncomePreYearIncome = UtilityMember.NumberSet.ToDecimal(dtBudgetValue.Compute("SUM(PROPOSED_INCOME_PREVIOUS_YR)", "NATURE_ID=1").ToString());
                decimal IncomeActualIncome = UtilityMember.NumberSet.ToDecimal(dtBudgetValue.Compute("SUM(ACTUAL_INCOME)", "NATURE_ID=1").ToString());
                decimal IncomeCurrentIncome = UtilityMember.NumberSet.ToDecimal(dtBudgetValue.Compute("SUM(PROPOSED_INCOME_CURRENT_YR)", "NATURE_ID=1").ToString());

                decimal ExpensePreYearIncome = UtilityMember.NumberSet.ToDecimal(dtBudgetValue.Compute("SUM(PROPOSED_INCOME_PREVIOUS_YR)", "NATURE_ID=2").ToString());
                decimal ExpenseActualIncome = UtilityMember.NumberSet.ToDecimal(dtBudgetValue.Compute("SUM(ACTUAL_INCOME)", "NATURE_ID=2").ToString());
                decimal ExpenseCurrentIncome = UtilityMember.NumberSet.ToDecimal(dtBudgetValue.Compute("SUM(PROPOSED_INCOME_CURRENT_YR)", "NATURE_ID=2").ToString());

                decimal AssetPreYearIncome = UtilityMember.NumberSet.ToDecimal(dtBudgetValue.Compute("SUM(PROPOSED_INCOME_PREVIOUS_YR)", "NATURE_ID=3").ToString());
                decimal AssetActualIncome = UtilityMember.NumberSet.ToDecimal(dtBudgetValue.Compute("SUM(ACTUAL_INCOME)", "NATURE_ID=3").ToString());
                decimal AssetCurrentIncome = UtilityMember.NumberSet.ToDecimal(dtBudgetValue.Compute("SUM(PROPOSED_INCOME_CURRENT_YR)", "NATURE_ID=3").ToString());

                decimal LiabillitesPreYearIncome = UtilityMember.NumberSet.ToDecimal(dtBudgetValue.Compute("SUM(PROPOSED_INCOME_PREVIOUS_YR)", "NATURE_ID=4").ToString());
                decimal LiabillitesActualIncome = UtilityMember.NumberSet.ToDecimal(dtBudgetValue.Compute("SUM(ACTUAL_INCOME)", "NATURE_ID=4").ToString());
                decimal LiabillitesCurrentIncome = UtilityMember.NumberSet.ToDecimal(dtBudgetValue.Compute("SUM(PROPOSED_INCOME_CURRENT_YR)", "NATURE_ID=4").ToString());

                dtSummary.Rows[0][PROPOSED_INCOME_PREVIOUS_YR] = IncomePreYearIncome;
                dtSummary.Rows[0][ACTUAL_INCOME] = IncomeActualIncome;
                dtSummary.Rows[0][PROPOSED_INCOME_CURRENT_YR] = IncomeCurrentIncome;

                dtSummary.Rows[1][PROPOSED_INCOME_PREVIOUS_YR] = ExpensePreYearIncome;
                dtSummary.Rows[1][ACTUAL_INCOME] = ExpenseActualIncome;
                dtSummary.Rows[1][PROPOSED_INCOME_CURRENT_YR] = ExpenseCurrentIncome;

                dtSummary.Rows[2][PROPOSED_INCOME_PREVIOUS_YR] = AssetPreYearIncome;
                dtSummary.Rows[2][ACTUAL_INCOME] = AssetActualIncome;
                dtSummary.Rows[2][PROPOSED_INCOME_CURRENT_YR] = AssetCurrentIncome;

                dtSummary.Rows[3][PROPOSED_INCOME_PREVIOUS_YR] = LiabillitesPreYearIncome;
                dtSummary.Rows[3][ACTUAL_INCOME] = LiabillitesActualIncome;
                dtSummary.Rows[3][PROPOSED_INCOME_CURRENT_YR] = LiabillitesCurrentIncome;
            }
            lblTotalBudgetAmount.Text = dtBudgetValue == null ? UtilityMember.NumberSet.ToCurrency(0) : UtilityMember.NumberSet.ToCurrency(UtilityMember.NumberSet.ToDouble(dtBudgetValue.Compute("SUM(PROPOSED_INCOME_CURRENT_YR)", "").ToString()));
            lblTotalBudgetAmount.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 15F);
            gcBudgetSummary.DataSource = dtSummary;
        }

        private DataTable ConstructSummaryTable()
        {
            DataTable dtSummary = new DataTable();
            dtSummary.Columns.Add(NATURE, typeof(System.String));
            dtSummary.Columns.Add(PROPOSED_INCOME_PREVIOUS_YR, typeof(System.Decimal));
            dtSummary.Columns.Add(ACTUAL_INCOME, typeof(System.Decimal));
            dtSummary.Columns.Add(PROPOSED_INCOME_CURRENT_YR, typeof(System.Decimal));
            dtSummary.Rows.Add(dtSummary.NewRow());
            dtSummary.Rows.Add(dtSummary.NewRow());
            dtSummary.Rows.Add(dtSummary.NewRow());
            dtSummary.Rows.Add(dtSummary.NewRow());
            if (dtSummary != null && dtSummary.Rows.Count == 4) //Count 4 means 4 nature of the accounts
            {
                dtSummary.Rows[0][NATURE] = INCOMES;
                dtSummary.Rows[1][NATURE] = EXPENSES;
                dtSummary.Rows[2][NATURE] = ASSETS;
                dtSummary.Rows[3][NATURE] = LIABILLITES;

                dtSummary.Rows[0][PROPOSED_INCOME_PREVIOUS_YR] = 0;
                dtSummary.Rows[1][PROPOSED_INCOME_PREVIOUS_YR] = 0;
                dtSummary.Rows[2][PROPOSED_INCOME_PREVIOUS_YR] = 0;
                dtSummary.Rows[3][PROPOSED_INCOME_PREVIOUS_YR] = 0;

                dtSummary.Rows[0][ACTUAL_INCOME] = 0;
                dtSummary.Rows[1][ACTUAL_INCOME] = 0;
                dtSummary.Rows[2][ACTUAL_INCOME] = 0;
                dtSummary.Rows[3][ACTUAL_INCOME] = 0;

                dtSummary.Rows[0][PROPOSED_INCOME_CURRENT_YR] = 0;
                dtSummary.Rows[1][PROPOSED_INCOME_CURRENT_YR] = 0;
                dtSummary.Rows[2][PROPOSED_INCOME_CURRENT_YR] = 0;
                dtSummary.Rows[3][PROPOSED_INCOME_CURRENT_YR] = 0;
            }
            return dtSummary;
        }
        #endregion

        private void glkpProject_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpProject);
        }

        #region CostCentre Methods
        private void ShowCostCentre(double LedgerAmount)
        {
            try
            {
                int CostCentre = 0;
                string LedgerName = string.Empty;
                DataView dvCostCentre = null;
                using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
                {
                    voucherSystem.LedgerId = LedgerId;
                    resultArgs = voucherSystem.FetchCostCentreLedger();
                    if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        LedgerName = resultArgs.DataSource.Table.Rows[0][voucherSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                        CostCentre = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][voucherSystem.AppSchema.Ledger.IS_COST_CENTERColumn.ColumnName].ToString());
                        if (CostCentre != 0 && !string.IsNullOrEmpty(LedgerName))
                        {
                            int RowIndex = this.UtilityMember.NumberSet.ToInteger(gvAnnualBudget.GetDataSourceRowIndex(gvAnnualBudget.FocusedRowHandle).ToString());

                            if (dsCostCentre.Tables.Contains(RowIndex + "LDR" + LedgerId))
                            {
                                dvCostCentre = dsCostCentre.Tables[RowIndex + "LDR" + LedgerId].DefaultView;
                            }
                            frmTransactionCostCenter frmCostCentre = new frmTransactionCostCenter(ProjectId, dvCostCentre, LedgerId, LedgerAmount, LedgerName);
                            frmCostCentre.ShowDialog();
                            if (frmCostCentre.DialogResult == DialogResult.OK)
                            {
                                DataTable dtValues = frmCostCentre.dtRecord;

                                if (dtValues != null)
                                {
                                    //              if (!dtValues.Columns.Contains("LEDGER_ID"))
                                    //                  dtValues.Columns.Add("LEDGER_ID", typeof(Int32));

                                    //              dtValues.AsEnumerable().ToList<DataRow>().ForEach(dr =>
                                    //{
                                    //    dr["LEDGER_ID"] = LedgerId;
                                    //});

                                    dtValues.TableName = RowIndex + "LDR" + LedgerId;
                                    if (dsCostCentre.Tables.Contains(dtValues.TableName))
                                    {
                                        dsCostCentre.Tables.Remove(dtValues.TableName);
                                    }
                                    dsCostCentre.Tables.Add(dtValues);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally { }
        }
        #endregion

        #region Costcentre Events
        private void rbtnViewCostCentre_Click(object sender, EventArgs e)
        {
            if (LedgerAmount > 0 && LedgerId > 0)
            {
                ShowCostCentre(this.UtilityMember.NumberSet.ToDouble(LedgerAmount.ToString()));
            }
        }
        #endregion

        private void rtxtProposedIncomeCurrentYear_KeyDown(object sender, KeyEventArgs e)
        {
            double ActualAmount = 0;
            double CalculatedAmount = 0;
            int Percentage = 0;
            try
            {
                Percentage = gvAnnualBudget.GetRowCellValue(gvAnnualBudget.FocusedRowHandle, colProposedIncomeCurrentYear) != null ? UtilityMember.NumberSet.ToInteger(gvAnnualBudget.GetRowCellValue(gvAnnualBudget.FocusedRowHandle, colProposedIncomeCurrentYear).ToString()) : 0;
                ActualAmount = gvAnnualBudget.GetRowCellValue(gvAnnualBudget.FocusedRowHandle, colActualIncome) != null ? UtilityMember.NumberSet.ToDouble(gvAnnualBudget.GetRowCellValue(gvAnnualBudget.FocusedRowHandle, colActualIncome).ToString()) : 0;
                if ((e.KeyCode == Keys.D5) && e.Shift)
                {
                    CalculatedAmount = (ActualAmount * Percentage) / 100;
                    gvAnnualBudget.SetFocusedRowCellValue(colProposedIncomeCurrentYear, CalculatedAmount);
                    gvAnnualBudget.MoveNext();
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
                lblPeriodFrom.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lblPeriodTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                dePeriodFrom.DateTime = UtilityMember.DateSet.ToDate(setting.YearFrom, false);
                dePeriodTo.DateTime = UtilityMember.DateSet.ToDate(setting.YearTo, false);

                glkpProject_EditValueChanged(this, e);  // this event is to validate already budget is exits for the type.
            }
            else
            {
                LockBudgetDatePeriod();

                lblPeriodFrom.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lblPeriodTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                glkpProject_EditValueChanged(this, e); // this event is to validate already budget is exits for the type.
            }
        }

        private void dePeriodFrom_EditValueChanged(object sender, EventArgs e)
        {
            DateTime date = new DateTime(dePeriodFrom.DateTime.Year, dePeriodFrom.DateTime.Month, dePeriodFrom.DateTime.Day);
            dePeriodTo.DateTime = date.AddMonths(12).AddDays(-1);
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
                        dePeriodTo.DateTime = new DateTime(UtilityMember.DateSet.ToDate(dePeriodTo.DateTime.ToString(), false).Year, 12, 31);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
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
    }
}