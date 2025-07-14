using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Bosco.Utility;
using Bosco.Model.UIModel;
using DevExpress.XtraBars;
using Bosco.Utility.ConfigSetting;
using AcMEDSync.Model;
using ACPP.Modules.Dsync;
using System.ServiceModel;
using DevExpress.XtraGrid.Views.Grid;

namespace ACPP.Modules.Master
{
    public partial class frmBudgetView : frmFinanceBase
    {
        #region Event Decelaration
        public event EventHandler UpdateHeld;
        #endregion

        #region Variable Decelartion
        ResultArgs resultArgs = null;
        BudgetType budgetType = BudgetType.BudgetPeriod;
        private int RowIndex = 0;
        #endregion

        #region Properties
        private int budgetId = 0;
        private int BudgetId
        {
            get
            {
                RowIndex = gvBudget.FocusedRowHandle;
                budgetId = gvBudget.GetFocusedRowCellValue(colBudgetId) != null ? this.UtilityMember.NumberSet.ToInteger(gvBudget.GetFocusedRowCellValue(colBudgetId).ToString()) : 0;
                return budgetId;
            }
        }

        private string BudgetName
        {
            get
            {
                // RowIndex = gvBudget.FocusedRowHandle;
                string budgetname = gvBudget.GetFocusedRowCellValue(colBudgetName) != null ? gvBudget.GetFocusedRowCellValue(colBudgetName).ToString() : "";
                return budgetname;
            }
        }

        private int budgetTypeId = 0;
        private int BudgetTypeId
        {
            get
            {
                // RowIndex = gvBudget.FocusedRowHandle;
                budgetTypeId = gvBudget.GetFocusedRowCellValue(colBudgetTypeId) != null ? this.UtilityMember.NumberSet.ToInteger(gvBudget.GetFocusedRowCellValue(colBudgetTypeId).ToString()) : 0;
                return budgetId;
            }
        }

        private bool IsActive
        {
            get
            {
                bool result = true;
                //RowIndex = gvBudget.FocusedRowHandle;
                if (UtilityMember.NumberSet.ToInteger(gvBudget.GetFocusedRowCellValue(colIsActive).ToString()).Equals(1))
                    result = false;
                return result;
            }
        }

        private DateTime GetDateFrom
        {
            get { return gvBudget.GetFocusedRowCellValue(colBudgetId) != null ? this.UtilityMember.DateSet.ToDate(gvBudget.GetFocusedRowCellValue(colDateFrom).ToString(), false) : new DateTime(); }
        }
        private DateTime GetDateTo
        {
            get { return gvBudget.GetFocusedRowCellValue(colBudgetId) != null ? this.UtilityMember.DateSet.ToDate(gvBudget.GetFocusedRowCellValue(colDateTo).ToString(), false) : new DateTime(); }
        }

        private DataTable dtBudgetInfo = null;
        private DataTable BudgetInfo
        {
            get
            {
                return dtBudgetInfo;
            }
            set
            {
                dtBudgetInfo = value;
            }
        }

        private string selectedProjectId;
        private string SelectedProjectId
        {
            get
            {
                SelectedProjectId = gvBudget.GetFocusedRowCellValue(colProjectIds).ToString() != null ? gvBudget.GetFocusedRowCellValue(colProjectIds).ToString() : string.Empty;
                return selectedProjectId;
            }
            set
            {
                selectedProjectId = value;
            }
        }

        private string selectedProjecteName;
        private string SelectedProjectName
        {
            get
            {
                selectedProjecteName = gvBudget.GetFocusedRowCellValue(colProject).ToString() != null ? gvBudget.GetFocusedRowCellValue(colProject).ToString() : string.Empty;
                return selectedProjecteName;
            }
        }
        #endregion

        #region Constructor
        public frmBudgetView()
        {
            InitializeComponent();
        }
        public frmBudgetView(BudgetType type)
            : this()
        {
            budgetType = type;
        }
        #endregion

        #region Events
        private void frmBudgetView_Load(object sender, EventArgs e)
        {
            ApplyUserRights();

            //For mysore show options months (one and two months)
            lcMysoreMonths.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            colStatus.Visible = colBudgetType.Visible = true;
            //colBudgetAction.Visible = false;
            colBudgetMonthRow.Visible = false;
            gccolDistributionIcon.Visible = false;
            if (AppSetting.IS_DIOMYS_DIOCESE)
            {
                lcMysoreMonths.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                colStatus.Visible = colBudgetType.Visible = false;
                colBudgetMonthRow.Visible = false;
                OptOneTwoMonth.SelectedIndex = 0;
                colStatus.Visible = false;
                colBudgetAction.Visible = true;
                colBudgetAction.VisibleIndex = 7;

                gvBudget.FocusRectStyle = DrawFocusRectStyle.RowFocus;
            }

            //09/04/2020, to hide 
            lcUserDefinedBudgetDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (this.AppSetting.HeadofficeCode.ToUpper().Equals("BSGNEI"))
            {
                lcUserDefinedBudgetDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }

            
            
        }

        private void frmBudgetView_Activated(object sender, EventArgs e)
        {
            //Added by Carmel Raj
            SetVisibileShortCuts(false, true);
            SetTitle();
            LoadBudget();

            this.rbtnBudgetDistributeIcon.Buttons[0].ToolTip = "To distribute Annual Budget by Monthly";
        }

        private void SetTitle()
        {
            if (budgetType.Equals(BudgetType.BudgetByAnnualYear))
                this.Text = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.BUDGET_ANNUAL);
            else
                this.Text = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.BUDGET_PERIOD);
        }

        private void gvBudget_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvBudget.RowCount.ToString();
        }

        private void frmBudgetView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkFilterRow.Checked = (chkFilterRow.Checked) ? false : true;
        }

        private void frmBudgetView_EnterClicked(object sender, EventArgs e)
        {
            ShowEditBudgetForm();
        }

        private void ucBudget_AddClicked(object sender, EventArgs e)
        {
            if (budgetType.Equals(BudgetType.BudgetByAnnualYear))
            {
                SetAnnualBudgetForm((int)AddNewRow.NewRow, string.Empty);

                //using (BudgetSystem budgetSystem = new BudgetSystem())
                //{
                //frmAddAnnualBudget frmAnnualBudget = new frmAddAnnualBudget();
                //frmAnnualBudget.ShowDialog();
                //chinna
                //budgetSystem.ProjectId = ProjectId;
                //if (budgetSystem.GetAnnualBudget() == 0)
                //{
                //    SetAnnualBudgetForm((int)AddNewRow.NewRow, ProjectId);
                //}
                //else
                //{
                //    ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.BUDGET_MADE_ALREADY));
                //}
                // }
            }
            //else
            //chinna
            //  this.ShowMessageBox("");
            //ShowBudgetForm((int)AddNewRow.NewRow, ProjectId);
        }

        private void ucBudget_DeleteClicked(object sender, EventArgs e)
        {
            DeleteBudgetDetails();
        }

        private void chkFilterRow_CheckedChanged(object sender, EventArgs e)
        {
            gvBudget.OptionsView.ShowAutoFilterRow = chkFilterRow.Checked;
            if (chkFilterRow.Checked)
            {
                this.SetFocusRowFilter(gvBudget, colBudgetName);
            }
        }

        private void ucBudget_EditClicked(object sender, EventArgs e)
        {
            // if (ExistMonthDistribution() == 0)
            //{
            ShowEditBudgetForm();
            //  }
            //  else
            // {
            //this.ShowMessageBox("Annual Monthly Distribution is Made so you can not edit Budget");
            //}
        }

        private int ExistMonthDistribution()
        {
            int Id = 0;
            using (BudgetSystem system = new BudgetSystem())
            {
                system.BudgetId = BudgetId;
                Id = system.ExistsMonthDistribution();
            }
            return Id;
        }
        private void gvBudget_DoubleClick(object sender, EventArgs e)
        {
            //if (ExistMonthDistribution() == 0)
            //  {
            ShowEditBudgetForm();
            // }
            // else
            // {
            //  this.ShowMessageBox("Annual Monthly Distribution is Made so you can not edit Budget");
            // }
        }

        private void ucBudget_RefreshClicked(object sender, EventArgs e)
        {
            LoadBudget();
        }

        private void ucBudget_PrintClicked(object sender, EventArgs e)
        {
            if (gvBudget.RowCount != 0)
            {
                //MessageRender.ShowMessage("Proposed Budget Ledger will be Printed / Exported");
                Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry(this.MdiParent);
                //report.ShowBudgetView(BudgetId, BudgetName);
                SettingProperty.ReportModuleId = (int)ReportModule.Finance;
                if (this.AppSetting.IS_DIOMYS_DIOCESE)
                {
                    if (OptOneTwoMonth.SelectedIndex == 1)
                    {
                        Int32 monthrow = gvBudget.GetFocusedRowCellValue(colBudgetMonthRow).ToString() != null ? UtilityMember.NumberSet.ToInteger(gvBudget.GetFocusedRowCellValue(colBudgetMonthRow).ToString()) : 0;

                        DataTable dt = gcBudget.DataSource as DataTable;
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataView dv = new DataView(dt);
                            dv.RowFilter = "MONTH_ROW=" + monthrow;
                            dv.Sort = "DATE_FROM";
                            if (dv.Count == 2)
                            {
                                string budgetids = dv[0]["BUDGET_ID"].ToString();
                                budgetids += "," + dv[1]["BUDGET_ID"].ToString();

                                string projectids = dv[0]["PROJECT_ID"].ToString();
                                projectids += "," + dv[1]["PROJECT_ID"].ToString();

                                string project = dv[0]["PROJECT"].ToString();


                                string datefrom = dv[0]["DATE_FROM"].ToString();
                                string dateto = dv[1]["DATE_TO"].ToString();

                                report.ShowBudgetExpenseApprovalByMonth(budgetids, projectids, true, gvBudget, datefrom, dateto, "", project);
                            }
                            else
                            {
                                ShowMessageBox("Select Two Months Budget");
                            }
                        }
                    }
                    else
                    {
                        report.ShowBudgetExpenseApprovalByMonth(BudgetId.ToString(), SelectedProjectId.ToString(), false, gvBudget, GetDateFrom.ToShortDateString(), GetDateTo.ToShortDateString(), BudgetName, SelectedProjectName);
                    }
                }
                else
                {
                    //On 22/03/2021, To show only proposed ledgers alone
                    //MessageRender.ShowMessage("Proposed and Actual Budget Ledger will be Printed / Exported");
                    MessageRender.ShowMessage("Proposed Budget Ledger alone will be Printed / Exported");
                    report.ShowBudgetView1(BudgetId, SelectedProjectId, BudgetName, gvBudget);
                }
            }
        }

        private void ucBudget_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvBudget_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {

            if (!AppSetting.IS_DIOMYS_DIOCESE)
            {
                if (gvBudget.RowCount != 0)
                {
                    if (e.RowHandle >= 0)
                    {
                        int status = gvBudget.GetRowCellValue(e.RowHandle, colIsActive) != null ? this.UtilityMember.NumberSet.ToInteger(gvBudget.GetRowCellValue(e.RowHandle, colIsActive).ToString()) : 2;
                        if (status != 2)
                        {
                            if (status == 1) // 05.02.2020 to set color hiden
                            {
                                // e.Appearance.BackColor = Color.LightGreen;
                            }
                            else
                            {
                                //e.Appearance.BackColor = Color.LightGray;
                            }
                        }
                    }
                }
            }
            //gvBudget.FocusedRowHandle = RowIndex;
            e.Appearance.BackColor = Color.LightGray;

        }

        private void rbtnBudgetDistributeIcon_Click(object sender, EventArgs e)
        {
            if (BudgetId > 0)
            {
                frmAddMonthDistributionAnnualBudget budgetAdd = new frmAddMonthDistributionAnnualBudget(BudgetId, SelectedProjectId, BudgetTypeId.ToString(), BudgetName);
                budgetAdd.ShowDialog();
            }
        }

        private void OptOneTwoMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            colBudgetMonthRow.Visible = (OptOneTwoMonth.SelectedIndex == 1);
        }

        #endregion

        #region Methods

        private void ApplyUserRights()
        {
            if (budgetType.Equals(BudgetType.BudgetByAnnualYear))
            {
                this.enumUserRights.Add(BudgetAnnual.CreateBudgetAnnual);
                this.enumUserRights.Add(BudgetAnnual.EditBudgetAnnual);
                this.enumUserRights.Add(BudgetAnnual.DeleteBudgetAnnual);
                this.enumUserRights.Add(BudgetAnnual.PrintBudgetAnnual);
                this.enumUserRights.Add(BudgetAnnual.ViewBudgetAnnual);
                this.ApplyUserRights(ucBudget, this.enumUserRights, (int)Menus.BudgetAnnual);
            }
            else
            {
                this.enumUserRights.Add(Budget.CreateBudget);
                this.enumUserRights.Add(Budget.EditBudget);
                this.enumUserRights.Add(Budget.DeleteBudget);
                this.enumUserRights.Add(Budget.PrintBudget);
                this.enumUserRights.Add(Budget.ViewBudget);
                this.ApplyUserRights(ucBudget, this.enumUserRights, (int)Menus.Budget);
            }
        }

        private void LoadBudget()
        {
            gvBudget.OptionsView.AllowCellMerge = true;
            if (budgetType.Equals(BudgetType.BudgetByAnnualYear))
            {
                using (BudgetSystem AnnualBudgetAll = new BudgetSystem())
                {
                    resultArgs = AnnualBudgetAll.FetchAnnualBudgetDetails();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        gcBudget.DataSource = BudgetInfo = resultArgs.DataSource.Table;
                        colProject.Visible = true;
                        //colBudgetType.Visible = true;
                        int MonthDistributionId = this.UtilityMember.NumberSet.ToInteger(BudgetInfo.Rows[0]["IS_MONTH_WISE"].ToString());
                        if (MonthDistributionId == 0)
                            gccolDistributionIcon.Visible = false;
                        else
                            if (AppSetting.IS_ABEBEN_DIOCESE)
                                gccolDistributionIcon.Visible = true;

                        //if (resultArgs.DataSource.Table.Rows.Count == (grdlProjectName.Properties.DataSource as DataTable).Rows.Count)
                        //{
                        //    // ucBudget.VisibleAddButton = BudgetInfo.Rows.Count > 0 ? BarItemVisibility.Never : BarItemVisibility.Always;
                        //    ucBudget.VisibleAddButton = BarItemVisibility.Never;
                        //}
                        //else
                        //    ucBudget.VisibleAddButton = BarItemVisibility.Always;
                        //colBudgetAction.VisibleIndex = 6;
                    }
                    else
                    {
                        gcBudget.DataSource = null; ucBudget.VisibleAddButton = BarItemVisibility.Always;
                    }
                }
            }
            else
            {
                // chinna
                //if (grdlProjectName.EditValue != null)
                //{
                //    if (UtilityMember.NumberSet.ToInteger(grdlProjectName.EditValue.ToString()).Equals(0))
                //    {
                //        using (BudgetSystem budgetall = new BudgetSystem())
                //        {
                //            resultArgs = budgetall.FetchBudgetDetails();
                //            if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                //            {
                //                gcBudget.DataSource = BudgetInfo = resultArgs.DataSource.Table;
                //                colProject.Visible = true;
                //                colBudgetType.Visible = false;
                //            }
                //            else { gcBudget.DataSource = null; }

                //        }
                //    }
                //    else
                //    {
                //        gcBudget.DataSource = null;
                //        FetchBudgetdetails();
                //    }
                //}
            }
        }

        private void FilterBudgetDetails(string Criteria)
        {
            //chinna
            //if (grdlProjectName.EditValue != null && BudgetInfo != null)
            //{
            //    if (!UtilityMember.NumberSet.ToInteger(grdlProjectName.EditValue.ToString()).Equals(0))
            //    {
            //        DataView dvBudget = BudgetInfo.DefaultView;
            //        dvBudget.RowFilter = Criteria;
            //        gcBudget.DataSource = dvBudget.ToTable();
            //        gcBudget.RefreshDataSource();
            //    }
            //    else
            //        LoadBudget();
            //}
        }

        private void ShowBudgetForm(int BudgetId, string projectId)
        {
            //try
            //{
            //frmBudgetAdd frmBudget = new frmBudgetAdd(BudgetId, GetDateFrom, GetDateTo, UtilityMember.NumberSet.ToInteger(grdlProjectName.EditValue.ToString()));
            //frmAddAnnualBudget frmBudget = new frmAddAnnualBudget(BudgetId, GetDateFrom, GetDateTo, projectId);
            //frmBudget.UpdateHeld += new EventHandler(OnUpdateHeld);
            //frmBudget.ShowDialog();
            //}
            //catch (Exception ex)
            //{
            //    MessageRender.ShowMessage(ex.ToString(), true);
            //}
            //finally { }
        }

        private void SetAnnualBudgetForm(int BudgetId, string projectId)
        {
            try
            {
                frmAddAnnualBudget frmBudget = new frmAddAnnualBudget(BudgetId, projectId);
                frmBudget.BudgetTypeId = this.BudgetTypeId.ToString();
                frmBudget.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmBudget.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadBudget();
            gvBudget.FocusedRowHandle = RowIndex;
        }

        public void ShowEditBudgetForm()
        {
            if (this.isEditable)
            {
                if (gvBudget.RowCount != 0)
                {
                    if (BudgetId > 0)
                    {
                        if (budgetType.Equals(BudgetType.BudgetByAnnualYear))
                        {
                            SetAnnualBudgetForm(BudgetId, SelectedProjectId);
                        }
                        else ShowBudgetForm(BudgetId, SelectedProjectId);
                    }
                    else
                    {
                        if (!chkFilterRow.Checked)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NOSELECTION_FOR_EDIT));
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_USER_RIGHTS));
            }
        }

        //private void FetchBudgetdetails()
        //{
        //    try
        //    {
        //        using (BudgetSystem budgetSystem = new BudgetSystem())
        //        {
        //            resultArgs = budgetSystem.FetchBudgetDetails();
        //            if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
        //            {
        //                gcBudget.DataSource = BudgetInfo = resultArgs.DataSource.Table;
        //                //    gvBudget.Columns["colProject"].Visible = false;
        //                colProject.Visible = false;

        //                //chinna
        //                // int ProjectId = this.UtilityMember.NumberSet.ToInteger(grdlProjectName.EditValue.ToString());
        //                FilterBudgetDetails("PROJECT_ID =" + ProjectId);
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.ToString(), true);
        //    }
        //    finally { }
        //}

        private void DeleteBudgetDetails()
        {
            try
            {
                if (gvBudget.RowCount != 0)
                {
                    if (BudgetId != 0)
                    {
                        string confirmationMessaage = (IsActive ? "" : "Budget is in Active,") + "Delete this entry?" + " - " + BudgetName;
                        if (this.ShowConfirmationMessage(confirmationMessaage, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            using (BudgetSystem budgetSystem = new BudgetSystem())
                            {
                                budgetSystem.BudgetId = BudgetId;

                                resultArgs = budgetSystem.RemoveBudgetDetails();
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    LoadBudget();
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

        private bool HasTransaction()
        {
            bool NotHasTrans = true;
            int count = 0;
            using (BudgetSystem budget = new BudgetSystem())
            {
                budget.BudgetId = BudgetId;
                count = budget.CheckForBudgetEntry();
                if (count > 0)
                {
                    if (this.ShowConfirmationMessage("Transaction are available for the Budget, Do you want to delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        NotHasTrans = true;
                    }
                    else
                    {
                        NotHasTrans = false;
                    }
                }
                else if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    NotHasTrans = true;
                }
                else
                {
                    NotHasTrans = false;
                }
            }
            return NotHasTrans;
        }
        #endregion

        private void gvBudget_Click(object sender, EventArgs e)
        {
            //if (BudgetId > 0)
            //{
            //    frmAddMonthDistributionAnnualBudget budgetAdd = new frmAddMonthDistributionAnnualBudget(BudgetId, SelectedProjectId, BudgetTypeId.ToString(), BudgetName);
            //    budgetAdd.ShowDialog();
            //}
        }

        private void btnUserDefinedBudget_Click(object sender, EventArgs e)
        {
            frmBudgetUserDefined frmuserdefinedbudget = new frmBudgetUserDefined();
            frmuserdefinedbudget.ShowDialog();
        }
    }
}