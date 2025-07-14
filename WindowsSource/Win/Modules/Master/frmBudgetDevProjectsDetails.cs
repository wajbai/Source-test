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
    public partial class frmBudgetDevProjectsDetails: frmFinanceBaseAdd
    {
        #region Variables
        private Int32 BudgetId = 0;
        string Projectids;
        private DateTime DateFrom;
        private DateTime DateTo;       
        string ProjectNames;
        DataTable dtBudgetDevlopmentalNewProjectDetails = null;
        DataTable dtBudgetDevlopmentalNewProjectCCDetails = null;

        #endregion
        #region Properties
        public DataTable dtDevProjectDevelopmentBudgetDetails = new DataTable();
        
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
                    UtilityMember.NumberSet.ToDouble(gvBudgetNewProject.GetRowCellValue(gvBudgetNewProject.FocusedRowHandle, ColBudgetPIncomeAmount).ToString()) : 0;
                return budgetNewProjectPIncomeAmount;
            }
        }

        private double BudgetNewProjectPExpenseAmount
        {
            get
            {
                double budgetNewProjectPExpenseAmount = 0;
                budgetNewProjectPExpenseAmount = gvBudgetNewProject.GetFocusedRowCellValue(ColBudgetPExpenseAmount) != null ?
                   UtilityMember.NumberSet.ToDouble(gvBudgetNewProject.GetRowCellValue(gvBudgetNewProject.FocusedRowHandle, ColBudgetPExpenseAmount).ToString()) : 0;
                return budgetNewProjectPExpenseAmount;
            }
        }

        private double BudgetNewProjectPGovtHelp
        {
            get
            {
                double budgetNewProjectPGovtHelp = 0;
                budgetNewProjectPGovtHelp = gvBudgetNewProject.GetFocusedRowCellValue(ColBudgetPGovtIncomeAmount) != null ?
                    UtilityMember.NumberSet.ToDouble(gvBudgetNewProject.GetRowCellValue(gvBudgetNewProject.FocusedRowHandle, ColBudgetPGovtIncomeAmount).ToString()) : 0;
                return budgetNewProjectPGovtHelp;
            }
        }

        private double BudgetNewProjectPProvinceHelp
        {
            get
            {
                double budgetNewProjectPProvinceHelp = 0;
                budgetNewProjectPProvinceHelp = gvBudgetNewProject.GetFocusedRowCellValue(ColPProvinceHelp) != null ?
                    UtilityMember.NumberSet.ToDouble(gvBudgetNewProject.GetRowCellValue(gvBudgetNewProject.FocusedRowHandle, ColPProvinceHelp).ToString()) : 0;
                return budgetNewProjectPProvinceHelp;
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

        #region Constructor
        public frmBudgetDevProjectsDetails(Int32 budgetid, string projectids, string projectnames, DateTime datefrom, DateTime dateto,
            DataTable dtdevprojectdevelopmentBudgetDetails, DataTable dtdevprojectdevelopmentBudgetCCDetails)
        {
            InitializeComponent();
            BudgetId = budgetid;
            Projectids = projectids;
            ProjectNames = projectnames;

            dtBudgetDevlopmentalNewProjectDetails = dtdevprojectdevelopmentBudgetDetails.DefaultView.ToTable();
            dtBudgetDevlopmentalNewProjectCCDetails = dtdevprojectdevelopmentBudgetCCDetails;
            DateFrom = datefrom;
            DateTo= dateto;
        }   
        #endregion

        #region Events
        private void frmBudgetDevProjectsDetails_Load(object sender, EventArgs e)
        {
            AssignBudgetNewProject();
            FocusBudgetNewProjectGrid();
                        
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

            if (AppSetting.ConsiderBudgetNewProject == 1)
            {
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

            ColBudgetPGovtIncomeAmount.Visible = ColBudgetPGovtIncomeAmount.Visible = ColBudgetPRemakrs.Visible = (AppSetting.ConsiderBudgetNewProject == 1);

            this.Text = "New Projects";
            if (AppSetting.ConsiderBudgetNewProject == 1)
            {
                this.Text = "Developmental Project Budget Details";
            }
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dtBindSource = gcBudgetNewProject.DataSource as DataTable;
            dtBindSource.AcceptChanges();
            
            using (BudgetSystem budgetsystem = new BudgetSystem())
            {
                DataSet dsDevelopmentalProjectsDetails = new DataSet();
                
                dtBudgetDevlopmentalNewProjectDetails.TableName = budgetsystem.AppSchema.ReportNewBudgetProject.TableName;
                dtBudgetDevlopmentalNewProjectCCDetails.TableName = budgetsystem.AppSchema.BudgetCostCentre.TableName;

                if (ValidateCCAmount())
                {
                    //Assign only valid rows
                    dtBudgetDevlopmentalNewProjectDetails.DefaultView.RowFilter = budgetsystem.AppSchema.ReportNewBudgetProject.NEW_PROJECTColumn.ColumnName + "<>'' " +
                                            " AND (" + budgetsystem.AppSchema.ReportNewBudgetProject.PROPOSED_INCOME_AMOUNTColumn.ColumnName + "> 0 OR " +
                                            budgetsystem.AppSchema.ReportNewBudgetProject.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName + "> 0 OR " +
                                            budgetsystem.AppSchema.ReportNewBudgetProject.GN_HELP_PROPOSED_AMOUNTColumn.ColumnName + "> 0 OR " +
                                            budgetsystem.AppSchema.ReportNewBudgetProject.HO_HELP_PROPOSED_AMOUNTColumn.ColumnName + "> 0)";
                    dtBudgetDevlopmentalNewProjectDetails = dtBudgetDevlopmentalNewProjectDetails.DefaultView.ToTable();

                    dsDevelopmentalProjectsDetails.Tables.Clear();
                    dsDevelopmentalProjectsDetails.Tables.Add(dtBudgetDevlopmentalNewProjectDetails.DefaultView.ToTable());
                    dsDevelopmentalProjectsDetails.Tables.Add(dtBudgetDevlopmentalNewProjectCCDetails.DefaultView.ToTable());
                    this.ReturnValue = dsDevelopmentalProjectsDetails;
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void gcBudgetNewProject_ProcessGridKey(object sender, KeyEventArgs e)
        {
            bool canFoucsCashTrnasaction = false;
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control) //gvBudgetNewProject.FocusedColumn == ColPProvinceHelp
            {
                gvBudgetNewProject.PostEditor();
                gvBudgetNewProject.UpdateCurrentRow();

                if ((gvBudgetNewProject.FocusedColumn == ColBudgetPRemakrs || (this.AppSetting.ConsiderBudgetNewProject == 0 && gvBudgetNewProject.FocusedColumn == ColPProvinceHelp)))
                {
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
                else if (this.AppSetting.EnableCostCentreBudget==1 && gvBudgetNewProject.FocusedColumn == ColBudgetPExpenseAmount)
                {
                    if (!string.IsNullOrEmpty(BudgetNewProject) && BudgetNewProjectPExpenseAmount != 0) 
                    {
                        Int32 sequencenumber = 1;
                        sequencenumber = gvBudgetNewProject.GetFocusedRowCellValue(colSequenceNo) != null ? 
                                        UtilityMember.NumberSet.ToInteger(gvBudgetNewProject.GetFocusedRowCellValue(colSequenceNo).ToString()) : 1;

                        if (sequencenumber==0)
                        {
                            sequencenumber = gvBudgetNewProject.FocusedRowHandle + 1;
                        }

                        string DevNewProjectName = gvBudgetNewProject.GetFocusedRowCellValue(ColBudgetNewProject) != null ? gvBudgetNewProject.GetFocusedRowCellValue(ColBudgetNewProject).ToString() : string.Empty;
                        double PorposedExpenseAmount = gvBudgetNewProject.GetFocusedRowCellValue(ColBudgetPExpenseAmount) != null ?
                            UtilityMember.NumberSet.ToDouble(gvBudgetNewProject.GetFocusedRowCellValue(ColBudgetPExpenseAmount).ToString()) : 0;
                        string transmode = "";//gvBudgetNewProject.GetFocusedRowCellValue(colTransMode) != null ? gvBudgetNewProject.GetFocusedRowCellValue(colTransMode).ToString() : TransSource.Dr.ToString();
                        frmBudgetLedgerCCDistribution frmbudgetledgercc = new frmBudgetLedgerCCDistribution(false, BudgetId, Projectids, sequencenumber, DevNewProjectName,
                                                PorposedExpenseAmount, 0, transmode, dtBudgetDevlopmentalNewProjectCCDetails);
                        DialogResult dialogresult = frmbudgetledgercc.ShowDialog();
                        if (dialogresult == System.Windows.Forms.DialogResult.OK)
                        {
                            if (frmbudgetledgercc.ReturnValue != null)
                            {
                                dtBudgetDevlopmentalNewProjectCCDetails = frmbudgetledgercc.ReturnValue as DataTable;
                            }
                        }
                    }
                }
            }
            else if (gvBudgetNewProject.IsFirstRow && gvBudgetNewProject.FocusedColumn == ColBudgetNewProject && e.Shift && e.KeyCode == Keys.Tab)
            {
                btnOk.Select();
                btnOk.Focus();
            }
        }

        private void rbtnDelete_Click(object sender, EventArgs e)
        {
            DeleteBudgetNewProject();
        }
        #endregion

        #region Methods
       
        /// <summary>
        /// Check Total Expenses amount with its Distribution 
        /// </summary>
        /// <returns></returns>
        private bool ValidateCCAmount()
        {
            bool rtn = true;
            CommonMethod cm = new CommonMethod();
            DataView dv = new DataView(dtBudgetDevlopmentalNewProjectDetails);
            using (BudgetSystem budgetsystem = new BudgetSystem())
            {
                string filter = budgetsystem.AppSchema.ReportNewBudgetProject.NEW_PROJECTColumn.ColumnName + "<>'' " +
                                        " AND (" + budgetsystem.AppSchema.ReportNewBudgetProject.PROPOSED_INCOME_AMOUNTColumn.ColumnName + "> 0 OR " +
                                        budgetsystem.AppSchema.ReportNewBudgetProject.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName + "> 0 OR " +
                                        budgetsystem.AppSchema.ReportNewBudgetProject.GN_HELP_PROPOSED_AMOUNTColumn.ColumnName + "> 0 OR " +
                                        budgetsystem.AppSchema.ReportNewBudgetProject.HO_HELP_PROPOSED_AMOUNTColumn.ColumnName + "> 0)";

                dv.RowFilter = filter;
                ResultArgs resultValidation = budgetsystem.ValidateBudgetDevelopmentalProjects(null, dv.ToTable());
                if (resultValidation.Success)
                {
                    foreach (DataRowView drv in dv)
                    {
                        string devenewproject = drv[budgetsystem.AppSchema.ReportNewBudgetProject.NEW_PROJECTColumn.ColumnName].ToString();
                        ResultArgs result = cm.CheckValueCotainsInDataTable(dtBudgetDevlopmentalNewProjectCCDetails,
                                                budgetsystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, devenewproject);
                        if (result.Success && result.DataSource.Table != null)
                        {
                            string sumfilter = "SUM(" + budgetsystem.AppSchema.BudgetCostCentre.PROPOSED_AMOUNTColumn.ColumnName + ")";
                            DataTable dtCCDetails = result.DataSource.Table;
                            double ccamt = UtilityMember.NumberSet.ToDouble(dtCCDetails.Compute(sumfilter, "").ToString());
                            double proposedExpenseAmt = UtilityMember.NumberSet.ToDouble(drv[budgetsystem.AppSchema.ReportNewBudgetProject.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName].ToString());
                            if (proposedExpenseAmt>0 && proposedExpenseAmt != ccamt)
                            {
                                this.ShowMessageBox("'" + devenewproject + "' " + ColBudgetPExpenseAmount.Caption + " not yet fully distributed.");
                                rtn = false;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(resultValidation.Message);
                    rtn = false;
                }
            }

            return rtn;
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
            using (BudgetSystem budgetsystem = new BudgetSystem())
            {
                string filter = budgetsystem.AppSchema.ReportNewBudgetProject.NEW_PROJECTColumn.ColumnName + "<>'' " +
                                        " AND (" + budgetsystem.AppSchema.ReportNewBudgetProject.PROPOSED_INCOME_AMOUNTColumn.ColumnName + "> 0 OR " +
                                        budgetsystem.AppSchema.ReportNewBudgetProject.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName + "> 0 OR " +
                                        budgetsystem.AppSchema.ReportNewBudgetProject.GN_HELP_PROPOSED_AMOUNTColumn.ColumnName + "> 0 OR " +
                                        budgetsystem.AppSchema.ReportNewBudgetProject.HO_HELP_PROPOSED_AMOUNTColumn.ColumnName + "> 0)";

                dv.RowFilter = filter;
            }

            if (dv.Count > 0)
            {
                Rtn = true;
            }

            return Rtn;
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
                FocusBudgetNewProjectGrid();
            }
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
        /// On 11/12/2019, this method is used to assign Budget new projects for Budget Annual summary
        /// </summary>
        private void AssignBudgetNewProject()
        {
            using (BudgetSystem budgetsystem = new BudgetSystem())
            {
                if (dtBudgetDevlopmentalNewProjectDetails == null)
                {
                    dtBudgetDevlopmentalNewProjectDetails = budgetsystem.AppSchema.ReportNewBudgetProject.DefaultView.ToTable();
                }

                if (dtBudgetDevlopmentalNewProjectCCDetails == null)
                {
                    dtBudgetDevlopmentalNewProjectDetails = budgetsystem.AppSchema.BudgetCostCentre.DefaultView.ToTable();
                }

                dtBudgetDevlopmentalNewProjectDetails.Rows.Add(dtBudgetDevlopmentalNewProjectDetails.NewRow());
                //dtBudgetDevlopmentalNewProjectDetails.DefaultView.Sort = budgetsystem.AppSchema.ReportNewBudgetProject.SEQUENCE_NOColumn.ColumnName;
                //dtBudgetDevlopmentalNewProjectDetails = dtBudgetDevlopmentalNewProjectDetails.DefaultView.ToTable();
                gcBudgetNewProject.DataSource = dtBudgetDevlopmentalNewProjectDetails;
                
                
                /*ResultArgs resultArgs = budgetsystem.FetchBudgetDevelopmentalProjects(BudgetId);
                if (resultArgs.Success)
                {
                    DataTable dtBudgetNewProjects = resultArgs.DataSource.Table;
                    gcBudgetNewProject.DataSource = dtBudgetNewProjects;
                    ConstractEmptyDatasource();
                }*/
            }
                        
            ConstractEmptyDatasource();
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
                        if (this.ShowConfirmationMessage("Are you sure to delete current row '" + BudgetNewProject + "' ?",  MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
        #endregion        
       
    }
}

