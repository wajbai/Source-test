using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using Bosco.Utility.ConfigSetting;

namespace Bosco.Model.UIModel
{

    public class BudgetSystem : SystemBase
    {
        #region Variable Declaration
        ResultArgs resultArgs = null;
        CommonMethod UtilityMethod = new CommonMethod();
        UISettingProperty setting = new UISettingProperty();
        #endregion

        public BudgetSystem()
        {
            dtBudgetDevelopmentalNewProjects = AppSchema.ReportNewBudgetProject.DefaultView.ToTable();
            dtBudgetDevelopmentalNewProjectsCCDetails = AppSchema.BudgetCostCentre.DefaultView.ToTable();
        }

        public BudgetSystem(int Ledger_ID, int Budget_id)
            : base()
        {
            FillFundAllotingProperties(Ledger_ID, Budget_id);
            //  GetLedger(Ledger_ID);
        }
        public BudgetSystem(int BudgetId, string BudgetTypeId)
            : base()
        {
            this.BudgetTypeId = NumberSet.ToInteger(BudgetTypeId);
            FillBudgetProperties(BudgetId);
        }

        public int BudgetId { get; set; }
        public int PreviousBudgetId { get; set; }
        public int M1BudgetId { get; set; }
        public int M2BudgetId { get; set; }
        public bool IsTwoMonthBudget { get; set; }
        public decimal Percentage { get; set; }
        public string BudgetName { get; set; }
        public int ProjectId { get; set; }
        public string MultipleProjectId { get; set; }
        public string[] TwoMonthIds { get; set; }

        public string DateFrom { get; set; }
        public string DateTo { get; set; }

        public string NextDateFrom { get; set; }
        public string NextDateTo { get; set; }

        public int BudgetTypeId { get; set; }
        public int BudgetMonth { get; set; }
        public string BudgetTransMode { get; set; }
        public int BudgetLevelId { get; set; }
        public int monthwiseDistribution { get; set; }
        public int BudAction { get; set; }
        public decimal HOHelpPropsedAmount { get; set; }
        public decimal HOHelpApprovedAmount { get; set; }
        public string Remarks { get; set; }
        public int LedgerId { get; set; }
        public decimal Amount { get; set; }
        public string TransMode { get; set; }
        public int Status { get; set; }
        public BudgetAction BudgetAction { get; set; }
        public bool isActive { get; set; }
        public DateTime VoucherDate { get; set; }
        public decimal Month1 { get; set; }
        public decimal Month2 { get; set; }
        public decimal Month3 { get; set; }
        public decimal Month4 { get; set; }
        public decimal Month5 { get; set; }
        public decimal Month6 { get; set; }
        public decimal Month7 { get; set; }
        public decimal Month8 { get; set; }
        public decimal Month9 { get; set; }
        public decimal Month10 { get; set; }
        public decimal Month11 { get; set; }
        public decimal Month12 { get; set; }
        public DataTable dtBudgetLedgers { get; set; }
        public DataTable dtBudgetSubLedgers { get; set; }
        public DataTable dtBudgetStatisticsDetails { get; set; }
        public DataTable dtBudgetCostCentre { get; set; }
        public DataTable dtBudgetDistribution { get; set; }
        public DataTable dtBudgetStrengthRecords { get; set; }
        public DataTable dtBudgetDevelopmentalNewProjects { get; set; }
        public DataTable dtBudgetDevelopmentalNewProjectsCCDetails { get; set; }
        //public DataSet dsBudgetCostCentreTables { get; set; }
        //public int CostCentreSequenceNo { get; set; }
        //public string CostCenterTable { get; set; }


        //public string SubLedgerName { get; set; }
        //public int SubLedgerId { get; set; }


        /// <summary>
        /// On 28/06/2018, This property is used to skip projects which is closed on or equal to this date
        /// </summary>
        public string ProjectClosedDate { get; set; }

        public void FillBudgetProperties(int BudgetId)
        {

            this.BudgetId = BudgetId;
            resultArgs = FetchBudgetDetailsById(BudgetId);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                BudgetName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.BUDGET_NAMEColumn.ColumnName].ToString();
                BudgetTypeId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.BUDGET_TYPE_IDColumn.ColumnName].ToString());
                BudgetLevelId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.BUDGET_LEVEL_IDColumn.ColumnName].ToString());
                MultipleProjectId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.PROJECT_IDColumn.ColumnName].ToString();
                ProjectId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.PROJECT_IDColumn.ColumnName].ToString());
                DateFrom = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.DATE_FROMColumn.ColumnName].ToString();
                DateTo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.DATE_TOColumn.ColumnName].ToString();
                Remarks = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.REMARKSColumn.ColumnName].ToString();
                Status = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.IS_ACTIVEColumn.ColumnName].ToString());
                HOHelpPropsedAmount = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.HO_HELP_PROPOSED_AMOUNTColumn.ColumnName].ToString());
                HOHelpApprovedAmount = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.HO_HELP_APPROVED_AMOUNTColumn.ColumnName].ToString());
                BudgetAction = (BudgetAction)this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.BUDGET_ACTIONColumn.ColumnName].ToString());

                //On 21/04/2023, To Get Budget Developmental Project/ New Project ----------------------------
                if (this.CreateBudgetDevNewProjects == 1)
                {
                    resultArgs = FetchBudgetDevelopmentalProjects(this.BudgetId);
                    if (resultArgs.Success && resultArgs.DataSource != null)
                    {
                        dtBudgetDevelopmentalNewProjects = resultArgs.DataSource.Table;
                        resultArgs = FetchBudgetDevelopmentalProjectsCCDetails(this.BudgetId);
                        if (resultArgs.Success && resultArgs.DataSource != null)
                        {
                            dtBudgetDevelopmentalNewProjectsCCDetails = resultArgs.DataSource.Table;
                        }
                        else
                        {
                            dtBudgetDevelopmentalNewProjectsCCDetails = this.AppSchema.ReportNewBudgetProject.DefaultView.ToTable();
                        }
                    }
                }
                //---------------------------------------------------------------------------------------------

                //On 02/05/2023, To get Budget Streanth detials ------------------------------------------------
                if (this.IncludeBudgetCCStrengthDetails == 1)
                {
                    resultArgs = FetchBudgetStrengthDetails(this.BudgetId);
                    if (resultArgs.Success && resultArgs.DataSource != null)
                    {
                        dtBudgetStrengthRecords = resultArgs.DataSource.Table;
                    }
                    else
                    {
                        dtBudgetStatisticsDetails = this.AppSchema.BudgetStatistics.DefaultView.ToTable();
                    }
                }

                //On 05/05/2023, To get Budget CC details
                if (this.EnableCostCentreBudget == 1)
                {
                    resultArgs = FetchBudgetCostCentre(this.BudgetId);
                    if (resultArgs.Success && resultArgs.DataSource != null)
                    {
                        dtBudgetCostCentre = resultArgs.DataSource.Table;
                    }
                    else
                    {
                        dtBudgetCostCentre = this.AppSchema.BudgetCostCentre.DefaultView.ToTable();
                    }
                }
                //----------------------------------------------------------------------------------------------
            }
        }

        private ResultArgs FetchBudgetDetailsById(int BudgetId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchById))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBudgetDetailsByStatistics(int BudgetId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchByStatisticId))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBudgetExists(int BudgetId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.isExistsMonthlyBudget))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        public ResultArgs FetchProjectsLookup()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchProjectList))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBudgetDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchAll))
            {
                if (!ProjectId.Equals(0))
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_FROMColumn, YearFrom);
                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_TOColumn, YearTo);
                if (DateFrom != null && DateFrom != string.Empty)
                {
                    dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, DateTo);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAnnualBudgetDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.AnnualBudgetFetch))
            {
                dataManager.Parameters.Add(AppSchema.Budget.DATE_FROMColumn, YearFrom);
                dataManager.Parameters.Add(AppSchema.Budget.DATE_TOColumn, YearTo);
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, new DateTime(DateSet.ToDate(YearFrom, false).Year, 1, 1));
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, new DateTime(DateSet.ToDate(YearTo, false).Year, 12, 31));

                dataManager.Parameters.Add(AppSchema.Project.ACCOUNT_DATEColumn, new DateTime(DateSet.ToDate(YearFrom, false).Year, 6, 1));
                dataManager.Parameters.Add(AppSchema.Project.DATE_CLOSEDColumn, new DateTime(DateSet.ToDate(YearTo, false).Year, 5, 31));

                if (!IsFullRightsReservedUser)
                {
                    dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, this.LoginUserRoleId);
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int ExistsMonthDistribution()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.ExistMonthDistributionCount))
            {
                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs FetchLedgerForNewBudget()
        {
            //using (DataManager dataManager = new DataManager(SQLCommand.Budget.AddNewBudgetFetchLedger))
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.BudgetLoad))
            {
                if (PreviousBudgetId == 0)
                    PreviousBudgetId = BudgetId;  //For Fetching Previous Budget Projected and Actual Amount
                if (BudgetId == 0)
                    BudgetId = PreviousBudgetId;

                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(AppSchema.Budget.PREVIOUS_BUDGET_IDColumn, PreviousBudgetId);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, this.DateSet.ToDateTime(DateTo, Bosco.Utility.DateFormatInfo.MySQLFormat.DateFormat, true));
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }



        public ResultArgs FetchMappedLedgers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchMappedLedgers))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBudgetBalance()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchBudgetBalance))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Budget.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, DateFrom);

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs CheckBudgetByDate()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.CheckBudgetByDate))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_DATEColumn, VoucherDate);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchActiveBudgetProjects()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchbyBudgetProject))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_DATEColumn, VoucherDate);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 20/04/2023, this method is used to assign Budget Developmental/New projects
        /// </summary>
        public ResultArgs FetchBudgetDevelopmentalProjects(Int32 budgetId)
        {
            ResultArgs resultArgs = new ResultArgs();
            if (this.CreateBudgetDevNewProjects == 1)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Setting.FetchBudgetNewProjects))
                {
                    dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.ACC_YEAR_IDColumn, SettingProperty.Current.AccPeriodId);
                    dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.DEVELOPMENTAL_NEW_BUDGETIDColumn, budgetId);
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
                }
            }
            else
            {
                resultArgs.Success = true;
            }
            return resultArgs;
        }

        public ResultArgs FetchBudgetDevelopmentalProjectsCCDetails(Int32 budgetId)
        {
            ResultArgs resultArgs = new ResultArgs();
            if (this.CreateBudgetDevNewProjects == 1)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Setting.FetchBudgetNewProjectsCCDetailsByAcYear))
                {
                    dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.ACC_YEAR_IDColumn, SettingProperty.Current.AccPeriodId);
                    dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.DEVELOPMENTAL_NEW_BUDGETIDColumn, budgetId);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
                }
            }
            else
            {
                resultArgs.Success = true;
            }
            return resultArgs;
        }

        public ResultArgs FetchBudgetStrengthDetails(Int32 budgetId)
        {
            ResultArgs resultArgs = new ResultArgs();
            if (this.IncludeBudgetCCStrengthDetails == 1)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Setting.FetchBudgetStrengthDetails))
                {
                    dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.BUDGET_IDColumn, budgetId);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
                }
            }
            else
            {
                resultArgs.Success = true;
            }
            return resultArgs;
        }

        public ResultArgs FetchBudgetCostCentre(Int32 budgetId)
        {
            ResultArgs resultArgs = new ResultArgs();
            if (this.EnableCostCentreBudget == 1)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchBudgetCostCentre))
                {
                    dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.BUDGET_IDColumn, budgetId);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
                }
            }
            else
            {
                resultArgs.Success = true;
            }
            return resultArgs;
        }


        /// <summary>
        /// On 20/04/2023, To update budget details
        /// </summary>
        /// <param name="BudgetDevelopmentalNewProjects"></param>
        /// <returns></returns>
        public ResultArgs SaveBudgetDevelopmentalProjects(DataManager dm, Int32 budgetId, DataTable BudgetDevelopmentalNewProjects)
        {
            ResultArgs resultArgs = new ResultArgs();
            Int32 sequecneno = 1;
            if (this.CreateBudgetDevNewProjects == 1 && BudgetDevelopmentalNewProjects != null)
            {
                resultArgs = ValidateBudgetDevelopmentalProjects(dm, BudgetDevelopmentalNewProjects);
                if (resultArgs.Success)
                {
                    //1. Delete all developmental new project details for concern budget
                    //2. Delete all developmental new project cc details for concern budget
                    resultArgs = DeleteDevelopmentalNewProjects(budgetId, this.AccPeriodId, dm);

                    //3. Update Developmental or new project detials
                    if (resultArgs.Success)
                    {
                        BudgetDevelopmentalNewProjects.DefaultView.RowFilter = this.AppSchema.ReportNewBudgetProject.NEW_PROJECTColumn.ColumnName + "<>'' " +
                                        " AND (" + this.AppSchema.ReportNewBudgetProject.PROPOSED_INCOME_AMOUNTColumn.ColumnName + "> 0 OR " +
                                        this.AppSchema.ReportNewBudgetProject.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName + "> 0 OR " +
                                        this.AppSchema.ReportNewBudgetProject.GN_HELP_PROPOSED_AMOUNTColumn.ColumnName + "> 0 OR " +
                                        this.AppSchema.ReportNewBudgetProject.HO_HELP_PROPOSED_AMOUNTColumn.ColumnName + "> 0)";

                        //For every Developmental Projects
                        foreach (DataRowView drv in BudgetDevelopmentalNewProjects.DefaultView)
                        {
                            string budgetnewproject = drv[this.AppSchema.ReportNewBudgetProject.NEW_PROJECTColumn.ColumnName].ToString().Trim();
                            double proposedincome = NumberSet.ToDouble(drv[this.AppSchema.ReportNewBudgetProject.PROPOSED_INCOME_AMOUNTColumn.ColumnName].ToString());
                            double proposedexpense = NumberSet.ToDouble(drv[this.AppSchema.ReportNewBudgetProject.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName].ToString());
                            double proposedgovthelp = NumberSet.ToDouble(drv[this.AppSchema.ReportNewBudgetProject.GN_HELP_PROPOSED_AMOUNTColumn.ColumnName].ToString());
                            double proposedprovincehelp = NumberSet.ToDouble(drv[this.AppSchema.ReportNewBudgetProject.HO_HELP_PROPOSED_AMOUNTColumn.ColumnName].ToString());
                            Int32 includereports = 1;// NumberSet.ToInteger(drv[this.AppSchema.ReportNewBudgetProject.INCLUDE_REPORTSColumn.ColumnName].ToString());
                            string budgetremarks = drv[this.AppSchema.ReportNewBudgetProject.REMARKSColumn.ColumnName].ToString().Trim();

                            if (!string.IsNullOrEmpty(budgetnewproject) && (proposedincome > 0 || proposedexpense > 0 || proposedprovincehelp > 0))
                            {
                                using (DataManager dataManager = new DataManager(SQLCommand.Setting.UpdateBudgetNewProjectsByAcYear))
                                {
                                    dataManager.Database = dm.Database;
                                    dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.ACC_YEAR_IDColumn, this.AccPeriodId);
                                    dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.BUDGET_IDColumn, budgetId.ToString());
                                    dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.SEQUENCE_NOColumn, sequecneno);
                                    dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.NEW_PROJECTColumn, budgetnewproject);
                                    dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.PROPOSED_INCOME_AMOUNTColumn, proposedincome);
                                    dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.PROPOSED_EXPENSE_AMOUNTColumn, proposedexpense);
                                    dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.GN_HELP_PROPOSED_AMOUNTColumn, proposedgovthelp);
                                    dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.HO_HELP_PROPOSED_AMOUNTColumn, proposedprovincehelp);
                                    dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.INCLUDE_REPORTSColumn.ColumnName, includereports);
                                    dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.REMARKSColumn, budgetremarks);
                                    //dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                                    resultArgs = dataManager.UpdateData();
                                }

                                if (resultArgs.Success)
                                {
                                    //For every Developmental Projects - Check CC is enabled and get cc distributions
                                    if (this.EnableCostCentreBudget == 1 && dtBudgetDevelopmentalNewProjectsCCDetails != null &&
                                                    dtBudgetDevelopmentalNewProjectsCCDetails.Rows.Count > 0)
                                    {
                                        ResultArgs resultFind = UtilityMethod.CheckValueCotainsInDataTable(dtBudgetDevelopmentalNewProjectsCCDetails,
                                            AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, budgetnewproject);
                                        if (resultFind.DataSource.Data != null && resultFind.Success)
                                        {
                                            DataTable dtDevelopmentCCDetails = resultFind.DataSource.Table;
                                            dtDevelopmentCCDetails.DefaultView.RowFilter = "(" + this.AppSchema.BudgetCostCentre.COST_CENTRE_IDColumn.ColumnName + ">0 AND " +
                                                                                this.AppSchema.BudgetCostCentre.PROPOSED_AMOUNTColumn.ColumnName + " > 0" + ")";
                                            //For every CC distribution for developmentenal project
                                            foreach (DataRowView drvCCDetails in dtDevelopmentCCDetails.DefaultView)
                                            {
                                                Int32 ccid = NumberSet.ToInteger(drvCCDetails[this.AppSchema.ReportNewBudgetProject.COST_CENTRE_IDColumn.ColumnName].ToString());
                                                double ccdistributeamount = NumberSet.ToDouble(drvCCDetails[this.AppSchema.BudgetCostCentre.PROPOSED_AMOUNTColumn.ColumnName].ToString());

                                                using (DataManager dataManager = new DataManager(SQLCommand.Setting.UpdateBudgetNewProjectsCCDetailsByAcYear))
                                                {
                                                    dataManager.Database = dm.Database;
                                                    dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.ACC_YEAR_IDColumn, SettingProperty.Current.AccPeriodId);
                                                    dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.BUDGET_IDColumn, budgetId.ToString());
                                                    dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.DEVELOPMENTAL_SEQUENCE_NOColumn, sequecneno);
                                                    dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.COST_CENTRE_IDColumn, ccid);
                                                    dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.PROPOSED_EXPENSE_AMOUNTColumn, ccdistributeamount);
                                                    resultArgs = dataManager.UpdateData();
                                                    if (!resultArgs.Success)
                                                    {
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    break;
                                }
                                sequecneno++;
                            }
                        }
                    }
                }
            }
            else
            {
                resultArgs.Success = true;
            }


            return resultArgs;
        }

        /// <summary>
        /// On 02/05/2023, To update strength 
        /// </summary>
        /// <param name="dm"></param>
        /// <param name="budgetId"></param>
        /// <param name="BudgetStrengthDetails"></param>
        /// <returns></returns>
        public ResultArgs SaveBudgetStrengthDetails(DataManager dm, Int32 budgetId, DataTable BudgetStrengthDetails)
        {
            ResultArgs resultArgs = new ResultArgs();
            if (this.IncludeBudgetCCStrengthDetails == 1 && BudgetStrengthDetails != null)
            {
                resultArgs = DeleteBudgetStrengthDetails(budgetId, dm);

                if (resultArgs.Success)
                {
                    BudgetStrengthDetails.DefaultView.RowFilter = this.AppSchema.BudgetStrength.COST_CENTRE_IDColumn.ColumnName + " > 0 " +
                                 " AND (" + this.AppSchema.BudgetStrength.NEW_COUNTColumn.ColumnName + "> 0 OR " + this.AppSchema.BudgetStrength.PRESENT_COUNTColumn.ColumnName + "> 0)";

                    //For every Developmental Projects
                    foreach (DataRowView drv in BudgetStrengthDetails.DefaultView)
                    {
                        Int32 ccid = NumberSet.ToInteger(drv[this.AppSchema.BudgetStrength.COST_CENTRE_IDColumn.ColumnName].ToString());
                        Int32 newtotal = NumberSet.ToInteger(drv[this.AppSchema.BudgetStrength.NEW_COUNTColumn.ColumnName].ToString());
                        Int32 presenttotal = NumberSet.ToInteger(drv[this.AppSchema.BudgetStrength.PRESENT_COUNTColumn.ColumnName].ToString());

                        if (ccid > 0 && (newtotal > 0 || presenttotal > 0))
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.Setting.UpdateBudgetStrengthDetails))
                            {
                                dataManager.Database = dm.Database;
                                dataManager.Parameters.Add(this.AppSchema.BudgetStrength.BUDGET_IDColumn, budgetId.ToString());
                                dataManager.Parameters.Add(this.AppSchema.BudgetStrength.COST_CENTRE_IDColumn, ccid);
                                dataManager.Parameters.Add(this.AppSchema.BudgetStrength.NEW_COUNTColumn, newtotal);
                                dataManager.Parameters.Add(this.AppSchema.BudgetStrength.PRESENT_COUNTColumn, presenttotal);
                                resultArgs = dataManager.UpdateData();
                            }
                        }
                    }
                }
            }
            else
            {
                resultArgs.Success = true;
            }

            return resultArgs;
        }

        public ResultArgs ValidateBudgetDevelopmentalProjects(DataManager dm, DataTable BudgetDevelopmentalNewProjects)
        {
            ResultArgs result = new ResultArgs();
            if (BudgetDevelopmentalNewProjects != null)
            {
                DataTable dtUniqueBudgetNewProjects = BudgetDevelopmentalNewProjects.DefaultView.ToTable(true, this.AppSchema.ReportNewBudgetProject.NEW_PROJECTColumn.ColumnName);

                //#. Check duplicate budget new projects in grid
                if (dtUniqueBudgetNewProjects.Rows.Count != BudgetDevelopmentalNewProjects.Rows.Count)
                {
                    var duplicates = BudgetDevelopmentalNewProjects.AsEnumerable().GroupBy(r => r[this.AppSchema.ReportNewBudgetProject.NEW_PROJECTColumn.ColumnName]).Where(gr => gr.Count() > 1).ToList();
                    if (duplicates.Any())
                    {
                        //string duplicatedNames = String.Join(System.Environment.NewLine, duplicates.Select(dupl => dupl.Key));
                        string duplicatedNames = String.Join(", ", duplicates.Select(dupl => dupl.Key));
                        result.Message = "The following Project(s) are duplicated in the list." + System.Environment.NewLine + duplicatedNames;
                    }
                }
                else { result.Success = true; }

                //#. Check duplicate budget new projects in table
                if (result.Success)
                {
                    //On 22/01/2024, To Skip checking development/activity/new project name 
                    //BudgetDevelopmentalNewProjects.DefaultView.RowFilter = this.AppSchema.ReportNewBudgetProject.NEW_PROJECTColumn.ColumnName + "<>'' " +
                    //                           " AND (" + this.AppSchema.ReportNewBudgetProject.PROPOSED_INCOME_AMOUNTColumn.ColumnName + "> 0 OR " +
                    //                           this.AppSchema.ReportNewBudgetProject.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName + "> 0 OR " +
                    //                           this.AppSchema.ReportNewBudgetProject.HO_HELP_PROPOSED_AMOUNTColumn.ColumnName + "> 0)";

                    //foreach (DataRowView drv in BudgetDevelopmentalNewProjects.DefaultView)
                    //{
                    //    string budgetnewproject = drv[this.AppSchema.ReportNewBudgetProject.NEW_PROJECTColumn.ColumnName].ToString().Trim();
                    //    Int32 sequenceno = NumberSet.ToInteger(drv[this.AppSchema.ReportNewBudgetProject.SEQUENCE_NOColumn.ColumnName].ToString());

                    //    if (!string.IsNullOrEmpty(budgetnewproject))
                    //    {
                    //        /* On 22/01/2024, To Skip checking development/activity/new project name same as existing ledger
                    //        //Check Development/New Project already availbe in Ledger name
                    //        using (LedgerSystem ledgersystem = new LedgerSystem())
                    //        {
                    //            result = ledgersystem.FetchLedgerIdByLedgerName(budgetnewproject);
                    //        }
                    //        if (result.Success && result.DataSource.Sclar.ToInteger == 0)
                    //        */

                    //        if (result.Success)
                    //        {
                    //            using (DataManager dataManager = new DataManager(SQLCommand.Setting.ExistsBudgetNewProjectsByAcYear))
                    //            {
                    //                if (dm != null) dataManager.Database = dm.Database;
                    //                dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.ACC_YEAR_IDColumn, SettingProperty.Current.AccPeriodId);
                    //                dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.BUDGET_IDColumn, BudgetId);
                    //                dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.SEQUENCE_NOColumn, sequenceno);
                    //                dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.NEW_PROJECTColumn, budgetnewproject);
                    //                result = dataManager.FetchData(DataSource.DataTable);
                    //                if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count == 0)
                    //                {
                    //                    result.Success = true;
                    //                }
                    //                else if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    //                {
                    //                    result.Message = "Project '" + budgetnewproject + "' is already available";
                    //                    break;
                    //                }
                    //            }
                    //        }
                    //        else
                    //        {
                    //            result.Message = "Project '" + budgetnewproject + "' is already available in the Ledgers list.";
                    //            break;
                    //        }
                    //    }
                    //}
                }
            }
            else
            {
                result.Success = true;
            }

            return result;
        }

        private ResultArgs SaveBudgetMasterDetails(DataManager dataManagers, bool IsAnnualBudget = false)
        {
            using (DataManager dataManager = new DataManager((BudgetId == 0) ? IsAnnualBudget ? SQLCommand.Budget.AddAnnual : SQLCommand.Budget.AddPeriod : SQLCommand.Budget.Update))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId, true);
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_NAMEColumn, BudgetName);
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_TYPE_IDColumn, BudgetTypeId);
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_LEVEL_IDColumn, BudgetLevelId);
                dataManager.Parameters.Add(this.AppSchema.Budget.IS_MONTH_WISEColumn, monthwiseDistribution);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, DateTo);
                dataManager.Parameters.Add(this.AppSchema.Budget.HO_HELP_PROPOSED_AMOUNTColumn, HOHelpPropsedAmount);
                dataManager.Parameters.Add(this.AppSchema.Budget.HO_HELP_APPROVED_AMOUNTColumn, HOHelpApprovedAmount);

                // if (!(IS_ABEBEN_DIOCESE || IS_DIOMYS_DIOCESE || IS_SDB_INM))
                // it can be set 
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_ACTIONColumn, BudAction);
                //On 22/02/2021, already defined above
                //dataManager.Parameters.Add(this.AppSchema.Budget.HO_HELP_APPROVED_AMOUNTColumn, HOHelpApprovedAmount);
                if (!IsAnnualBudget)
                    dataManager.Parameters.Add(this.AppSchema.Budget.REMARKSColumn, Remarks);
                dataManager.Parameters.Add(this.AppSchema.Budget.IS_ACTIVEColumn, Status);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true; //On 22/02/2021
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveFund()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = SaveAllotingFundDetails(dataManager);
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs SaveAllotingFundDetails(DataManager dataManagers)
        {
            int ledgerExistID = GetLedger();
            using (DataManager dataManager = new DataManager((ledgerExistID == 0) ? SQLCommand.Budget.AddAllotFund : SQLCommand.Budget.UpdateAllotFund))
            {
                dataManager.Database = dataManagers.Database;
                if (BudgetId > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.AllotFund.BUDGET_IDColumn, BudgetId);
                    dataManager.Parameters.Add(this.AppSchema.AllotFund.LEDGER_IDColumn, LedgerId);
                    dataManager.Parameters.Add(this.AppSchema.AllotFund.MONTH1Column, Month1);
                    dataManager.Parameters.Add(this.AppSchema.AllotFund.MONTH2Column, Month2);
                    dataManager.Parameters.Add(this.AppSchema.AllotFund.MONTH3Column, Month3);
                    dataManager.Parameters.Add(this.AppSchema.AllotFund.MONTH4Column, Month4);
                    dataManager.Parameters.Add(this.AppSchema.AllotFund.MONTH5Column, Month5);
                    dataManager.Parameters.Add(this.AppSchema.AllotFund.MONTH6Column, Month6);
                    dataManager.Parameters.Add(this.AppSchema.AllotFund.MONTH7Column, Month7);
                    dataManager.Parameters.Add(this.AppSchema.AllotFund.MONTH8Column, Month8);
                    dataManager.Parameters.Add(this.AppSchema.AllotFund.MONTH9Column, Month9);
                    dataManager.Parameters.Add(this.AppSchema.AllotFund.MONTH10Column, Month10);
                    dataManager.Parameters.Add(this.AppSchema.AllotFund.MONTH11Column, Month11);
                    dataManager.Parameters.Add(this.AppSchema.AllotFund.MONTH12Column, Month12);
                    resultArgs = dataManager.UpdateData();
                }
            }
            return resultArgs;
        }

        public ResultArgs GetBindDataRandomSource()
        {
            // using (DataManager dataManager = new DataManager(SQLCommand.Budget.GetRandomMonth))
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.BudgetLoad))
            {
                if (PreviousBudgetId == 0)
                    PreviousBudgetId = BudgetId;  //For Fetching Previous Budget Projected and Actual Amount
                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                dataManager.Parameters.Add(AppSchema.Budget.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(AppSchema.Budget.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(AppSchema.Budget.PREVIOUS_BUDGET_IDColumn, PreviousBudgetId);
                dataManager.Parameters.Add(AppSchema.Budget.DATE_TOColumn, this.DateSet.ToDateTime(DateTo, Bosco.Utility.DateFormatInfo.MySQLFormat.DateFormat, true));
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs ImportBudget()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.ImportBudget))
            {
                dataManager.Parameters.Add(AppSchema.Budget.PERCENTAGEColumn, Percentage);
                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                dataManager.Parameters.Add(AppSchema.Budget.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(AppSchema.Budget.DATE_TOColumn, this.DateSet.ToDateTime(DateTo, Bosco.Utility.DateFormatInfo.MySQLFormat.DateFormat, true));
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FillFundAllotingProperties(int Ledger_id, int Budget_id)
        {
            resultArgs = FetchBudgetAllotFund(Ledger_id, Budget_id);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                Month1 = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AllotFund.MONTH1Column.ColumnName].ToString());
                Month2 = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AllotFund.MONTH2Column.ColumnName].ToString());
                Month3 = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AllotFund.MONTH3Column.ColumnName].ToString());
                Month4 = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AllotFund.MONTH4Column.ColumnName].ToString());
                Month5 = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AllotFund.MONTH5Column.ColumnName].ToString());
                Month6 = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AllotFund.MONTH6Column.ColumnName].ToString());
                Month7 = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AllotFund.MONTH7Column.ColumnName].ToString());
                Month8 = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AllotFund.MONTH8Column.ColumnName].ToString());
                Month9 = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AllotFund.MONTH9Column.ColumnName].ToString());
                Month10 = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AllotFund.MONTH10Column.ColumnName].ToString());
                Month11 = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AllotFund.MONTH11Column.ColumnName].ToString());
                Month12 = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AllotFund.MONTH12Column.ColumnName].ToString());
            }
            return resultArgs;
        }

        public ResultArgs FetchBudgetAllotFund(int ledger_Id, int Budget_id)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchAllotFund))
            {
                dataManager.Parameters.Add(this.AppSchema.AllotFund.LEDGER_IDColumn, ledger_Id);
                dataManager.Parameters.Add(this.AppSchema.AllotFund.BUDGET_IDColumn, Budget_id);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int GetLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.GetLedgerExist))
            {
                dataManager.Parameters.Add(this.AppSchema.AllotFund.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(this.AppSchema.AllotFund.BUDGET_IDColumn, BudgetId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs RemoveBudgetDetails()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = DeleteBudgetLedgerDetails(dataManager);
                if (resultArgs.Success)
                    resultArgs = DeleteBudgetSubLedger(dataManager);
                if (resultArgs.Success)
                    resultArgs = DeleteAnnualBudgetDistributionDetails(dataManager);
                if (resultArgs.Success)
                    resultArgs = DeleteBudgetProjectDetails(dataManager);
                if (resultArgs.Success)
                    resultArgs = DeleteBudgetStatisticsDetails(dataManager);
                if (resultArgs.Success)
                    resultArgs = DeleteAllotFundDetails(dataManager);
                if (resultArgs.Success)
                    resultArgs = DeleteDevelopmentalNewProjects(BudgetId, this.AccPeriodId, dataManager);
                if (resultArgs.Success)
                    resultArgs = DeleteBudgetStrengthDetails(BudgetId, dataManager);
                if (resultArgs.Success)
                    resultArgs = DeleteBudgetMasterDetails(dataManager);

                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public ResultArgs RemoveMysoreBudgetDetails()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                foreach (string ids in TwoMonthIds)
                {
                    BudgetId = NumberSet.ToInteger(ids);
                    resultArgs = DeleteBudgetCostCenterDetails(dataManager);
                    if (resultArgs.Success)
                    {
                        resultArgs = DeleteBudgetLedgerDetails(dataManager);
                        if (resultArgs.Success)
                            resultArgs = DeleteBudgetSubLedger(dataManager);
                        if (resultArgs.Success)
                            resultArgs = DeleteAnnualBudgetDistributionDetails(dataManager);
                        if (resultArgs.Success)
                            resultArgs = DeleteBudgetProjectDetails(dataManager);
                        if (resultArgs.Success)
                            resultArgs = DeleteBudgetStatisticsDetails(dataManager);
                        if (resultArgs.Success)
                            resultArgs = DeleteAllotFundDetails(dataManager);
                        if (resultArgs.Success)
                            resultArgs = DeleteBudgetMasterDetails(dataManager);
                    }
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public int CheckForBudgetEntry()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.CheckForBudgetEntry))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        private ResultArgs DeleteAllotFundDetails(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.DeleteAllotFund))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteBudgetMasterDetails(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.Delete))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// On 29/10/2020, To save budget, merged ledger
        /// </summary>
        /// <param name="dataManagers"></param>
        /// <param name="transmode"></param>
        /// <param name="propsedamt"></param>
        /// <param name="approvedamt"></param>
        /// <param name="narration"></param>
        /// <param name="honarration"></param>
        /// <returns></returns>
        public ResultArgs SaveMergedBudgetLedger(DataManager dataManagers, string transmode,
                                double propsedamt, double approvedamt, string narration, string honarration)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.AnnualBudgetLedgerAdd))
            {
                dataManager.Database = dataManagers.Database;
                if (LedgerId > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                    dataManager.Parameters.Add(this.AppSchema.Budget.LEDGER_IDColumn, LedgerId);
                    dataManager.Parameters.Add(this.AppSchema.Budget.PROPOSED_AMOUNTColumn, propsedamt);
                    dataManager.Parameters.Add(this.AppSchema.Budget.APPROVED_AMOUNTColumn, approvedamt);
                    dataManager.Parameters.Add(this.AppSchema.Budget.TRANS_MODEColumn, transmode.ToString());
                    dataManager.Parameters.Add(this.AppSchema.Budget.NARRATIONColumn, narration);
                    dataManager.Parameters.Add(this.AppSchema.Budget.HO_NARRATIONColumn, honarration);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.UpdateData();
                }
            }
            return resultArgs;
        }

        private ResultArgs SaveBudgetLedgers(DataManager dataManagers)
        {
            resultArgs = DeleteBudgetLedgerDetails(dataManagers);
            if (resultArgs.Success)
            {
                if (dtBudgetLedgers != null && dtBudgetLedgers.Rows.Count > 0)
                {
                    DataTable dtLedgerAmount = dtBudgetLedgers.AsEnumerable().GroupBy(r => r.Field<UInt32?>("LEDGER_ID")).Select(g => g.First()).CopyToDataTable();
                    foreach (DataRow drBudget in dtLedgerAmount.Rows)
                    {
                        //    decimal Amount = NumberSet.ToDecimal(dtBudgetLedgers.Compute("SUM(AMOUNT)", String.Format("LEDGER_ID='{0}'", this.NumberSet.ToInteger(drBudget[this.AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString()))).ToString());
                        decimal Amount = NumberSet.ToDecimal(drBudget["TOTAL"].ToString());
                        using (DataManager dataManager = new DataManager(SQLCommand.Budget.BudgetLedgerAdd))
                        {
                            dataManager.Database = dataManagers.Database;
                            if (drBudget[this.AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString() != string.Empty)
                            {
                                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                                dataManager.Parameters.Add(this.AppSchema.Budget.LEDGER_IDColumn, this.NumberSet.ToInteger(drBudget[this.AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString()));
                                dataManager.Parameters.Add(this.AppSchema.Budget.AMOUNTColumn, Amount);
                                dataManager.Parameters.Add(this.AppSchema.Budget.TRANS_MODEColumn, drBudget[this.AppSchema.Budget.TRANS_MODEColumn.ColumnName].ToString());
                                resultArgs = dataManager.UpdateData();
                            }
                        }
                        if (!resultArgs.Success) { break; }
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs SaveBudgetLedgerDetails(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.BudgetLedgerAdd))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                dataManager.Parameters.Add(this.AppSchema.Budget.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(this.AppSchema.Budget.AMOUNTColumn, Amount);
                dataManager.Parameters.Add(this.AppSchema.Budget.TRANS_MODEColumn, TransMode);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteBudgetLedgerDetails(DataManager dataManagers)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.Budget.DeleteBudgetLedgerById))
            {
                datamanager.Database = dataManagers.Database;
                datamanager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                resultArgs = datamanager.UpdateData();
            }

            //On 04/05/2023, Delete Budget Ledger Cost Centers----------
            if (resultArgs.Success)
            {
                resultArgs = DeleteBudgetCostCentre(dataManagers);
            }
            //----------------------------------------------------------
            return resultArgs;
        }

        private ResultArgs DeleteBudgetSubLedger(DataManager dataManagers)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.Budget.DeleteBudgetSubLedgerById))
            {
                datamanager.Database = dataManagers.Database;
                datamanager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteAnnualBudgetDistributionDetails(DataManager dataManagers)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.Budget.DeleteBudgetDistributionById))
            {
                datamanager.Database = dataManagers.Database;
                datamanager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteAnnualBudgetDistribution()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.Budget.DeleteBudgetDistributionIDandDate))
            {
                datamanager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                datamanager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, VoucherDateFrom);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteBudgetProjectDetails(DataManager dataManager)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.Budget.DeleteBudgetProjectById))
            {
                datamanager.Database = datamanager.Database;
                datamanager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs SaveBudgetStatisticsDetails(DataManager dataManager)
        {
            if (dtBudgetStatisticsDetails != null && dtBudgetStatisticsDetails.Rows.Count > 0)
            {
                foreach (DataRow dr in dtBudgetStatisticsDetails.Rows)
                {
                    using (DataManager data = new DataManager(SQLCommand.Budget.AddStatisticDetails))
                    {
                        data.Database = dataManager.Database;
                        data.Parameters.Add(this.AppSchema.BudgetStatistics.BUDGET_IDColumn, BudgetId);
                        data.Parameters.Add(this.AppSchema.BudgetStatistics.STATISTICS_TYPE_IDColumn, dr[AppSchema.BudgetStatistics.STATISTICS_TYPE_IDColumn.ColumnName].ToString());
                        data.Parameters.Add(this.AppSchema.BudgetStatistics.TOTAL_COUNTColumn, dr[AppSchema.BudgetStatistics.TOTAL_COUNTColumn.ColumnName].ToString());
                        resultArgs = data.UpdateData();
                    }
                    if (!resultArgs.Success) { break; }
                }
            }
            return resultArgs;
        }

        private ResultArgs DeleteBudgetStatisticsDetails(DataManager dataManager)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.Budget.DeleteBudgetStatisticsDetails))
            {
                datamanager.Database = dataManager.Database;
                datamanager.Parameters.Add(this.AppSchema.BudgetStatistics.BUDGET_IDColumn, BudgetId);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// On 28/04/2023, To delete all delopmental or new projects details for concern budget ids
        /// </summary>
        /// <param name="budgetId"></param>
        /// <returns></returns>
        public ResultArgs DeleteDevelopmentalNewProjects(Int32 budgetId, string acid, DataManager dm)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Setting.DeleteBudgetNewProjectsByAcYear))
            {
                if (dm != null) dataManager.Database = dm.Database;
                dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.ACC_YEAR_IDColumn, acid);

                //On 09/10/2024, To delete new projects linked when main budget is deleted
                dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.DEVELOPMENTAL_NEW_BUDGETIDColumn, budgetId.ToString());

                /*//On 21/04/2023, Load developmental/new project for concern budgets
                if (SettingProperty.Current.CreateBudgetDevNewProjects == 1)
                {
                    dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.DEVELOPMENTAL_NEW_BUDGETIDColumn, budgetId.ToString());
                }
                else
                    dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.DEVELOPMENTAL_NEW_BUDGETIDColumn, "0");
                    */
                resultArgs = dataManager.UpdateData();
            }

            //2. Delete all developmental new project cc details for concern budget
            //On 09/10/2024, To delete cc budget when main budget is deleted
            if (resultArgs.Success) //this.EnableCostCentreBudget == 1 && 
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Setting.DeleteBudgetNewProjectsCCDetailsByAcYear))
                {
                    if (dm != null) dataManager.Database = dm.Database;
                    dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.ACC_YEAR_IDColumn, acid);
                    dataManager.Parameters.Add(this.AppSchema.ReportNewBudgetProject.DEVELOPMENTAL_NEW_BUDGETIDColumn, budgetId.ToString());
                    resultArgs = dataManager.UpdateData();
                }
            }

            return resultArgs;
        }

        private ResultArgs DeleteBudgetStrengthDetails(Int32 budgetId, DataManager dm)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Setting.DeleteBudgetStrengthDetails))
            {
                dataManager.Database = dm.Database;
                dataManager.Parameters.Add(this.AppSchema.BudgetStrength.BUDGET_IDColumn, budgetId.ToString());
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteBudgetCostCentre(DataManager dataManagers)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.Budget.DeleteBudgetCostCentre))
            {
                datamanager.Database = dataManagers.Database;
                datamanager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs ChangeStatusToInActive()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.ChangeStatusToInActive))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs ChangeRecentBudgetStatusToActive()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.BudgetLedgerUpdate))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
            }
            return resultArgs;
        }

        private ResultArgs AllotFund(DataManager dataManagers)
        {
            if (dtBudgetLedgers != null && dtBudgetLedgers.Rows.Count > 0)
            {
                DataTable LedgerGroup = dtBudgetLedgers.AsEnumerable().GroupBy(r => r.Field<UInt32?>("LEDGER_ID")).Select(g => g.First()).CopyToDataTable();
                foreach (DataRow dr in LedgerGroup.Rows)
                {
                    if (!NumberSet.ToInteger(dr[AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString()).Equals(0))
                    {
                        LedgerId = NumberSet.ToInteger(dr[AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString());
                        DataView dvFilter = new DataView(dtBudgetLedgers);
                        dvFilter.RowFilter = String.Format("LEDGER_ID={0}", NumberSet.ToInteger(dr[AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString()));
                        foreach (DataRow drFilter in dvFilter.ToTable().Rows)
                        {
                            switch (NumberSet.ToInteger(drFilter["MONTH"].ToString()))
                            {
                                case 1:
                                    Month1 = NumberSet.ToDecimal(drFilter[AppSchema.Budget.AMOUNTColumn.ColumnName].ToString());
                                    break;
                                case 2:
                                    Month2 = NumberSet.ToDecimal(drFilter[AppSchema.Budget.AMOUNTColumn.ColumnName].ToString());
                                    break;
                                case 3:
                                    Month3 = NumberSet.ToDecimal(drFilter[AppSchema.Budget.AMOUNTColumn.ColumnName].ToString());
                                    break;
                                case 4:
                                    Month4 = NumberSet.ToDecimal(drFilter[AppSchema.Budget.AMOUNTColumn.ColumnName].ToString());
                                    break;
                                case 5:
                                    Month5 = NumberSet.ToDecimal(drFilter[AppSchema.Budget.AMOUNTColumn.ColumnName].ToString());
                                    break;
                                case 6:
                                    Month6 = NumberSet.ToDecimal(drFilter[AppSchema.Budget.AMOUNTColumn.ColumnName].ToString());
                                    break;
                                case 7:
                                    Month7 = NumberSet.ToDecimal(drFilter[AppSchema.Budget.AMOUNTColumn.ColumnName].ToString());
                                    break;
                                case 8:
                                    Month8 = NumberSet.ToDecimal(drFilter[AppSchema.Budget.AMOUNTColumn.ColumnName].ToString());
                                    break;
                                case 9:
                                    Month9 = NumberSet.ToDecimal(drFilter[AppSchema.Budget.AMOUNTColumn.ColumnName].ToString());
                                    break;
                                case 10:
                                    Month10 = NumberSet.ToDecimal(drFilter[AppSchema.Budget.AMOUNTColumn.ColumnName].ToString());
                                    break;
                                case 11:
                                    Month11 = NumberSet.ToDecimal(drFilter[AppSchema.Budget.AMOUNTColumn.ColumnName].ToString());
                                    break;
                                case 12:
                                    Month12 = NumberSet.ToDecimal(drFilter[AppSchema.Budget.AMOUNTColumn.ColumnName].ToString());
                                    break;
                            }
                        }
                        if (dvFilter.ToTable().Rows.Count > 0)
                            resultArgs = SaveAllotingFundDetails(dataManagers);
                    }
                }
            }
            return resultArgs;
        }

        public int CheckStatus()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.CheckStatus))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, MultipleProjectId);

                // dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, DateSet.ToDate(YearFrom, false));
                // dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, DateSet.ToDate(YearTo, false));

                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, DateSet.ToDate(DateFrom, false));
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, DateSet.ToDate(DateTo, false));

                dataManager.Parameters.Add(AppSchema.AccountingPeriod.BOOKS_BEGINNING_FROMColumn, DateSet.ToDate(DateFrom, false));
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.PREVIOUS_YEAR_FROMColumn, DateSet.ToDate(DateTo, false));

                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_TYPE_IDColumn, BudgetTypeId);

                if (BudgetTypeId == (int)BudgetType.BudgetMonth)
                {
                    dataManager.Parameters.Add(AppSchema.Budget.BUDGET_TYPE_IDColumn, BudgetTypeId);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }

            return NumberSet.ToInteger(resultArgs.DataSource.Sclar.ToString);
        }

        public ResultArgs SaveBudgetDetails()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                //Saving Budget Master
                resultArgs = SaveBudgetMasterDetails(dataManager);
                if (resultArgs.Success)
                {
                    BudgetId = BudgetId.Equals(0) ? NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : BudgetId;
                    //Saving Budget Ledger
                    resultArgs = SaveBudgetLedgers(dataManager);
                    if (resultArgs.Success)
                        resultArgs = AllotFund(dataManager);//Saving Alot Fund Details
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public DataTable FetchBudgetsByProjects(string ProjectId)
        {
            ResultArgs resultargs = null;
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchBudgetByProject))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, ProjectId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultargs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultargs.DataSource.Table;
        }

        public ResultArgs UpdateBudgetAction(string BudgetIds, BudgetAction budgetaction)
        {
            ResultArgs resultargs = null;
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.UpdateBudgetAction))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetIds);
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_ACTIONColumn, (int)budgetaction);
                resultargs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultargs;
        }

        public ResultArgs FetchUserDefinedBudgetDetails(Int32 ProjectId, string DateFromYear, string DateToYear, TransactionMode tansmode)
        {
            ResultArgs resultargs = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchUserDefinedBudgetDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, DateFromYear);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, DateToYear);
                dataManager.Parameters.Add(this.AppSchema.Budget.TRANS_MODEColumn, tansmode);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultargs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultargs;
        }

        public ResultArgs FetchUserDefinedBalances(Int32 ProjectId, string DateFromY1, string DateToY1, string DateFromY2, string DateToY2)
        {
            ResultArgs resultargs = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchUserDefinedBudgetBalances))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, DateFromY1);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, DateToY1);
                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_FROMColumn, DateFromY2);
                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_TOColumn, DateToY2);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultargs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultargs;
        }

        public ResultArgs UpdateUserDefinedBudgetDetails(Int32 ProjectId, Int32 YearNo, string DateFromYear, string DateToYear,
                        DataTable dtUserDefinedBudgetIncomeLedgers, DataTable dtUserDefinedBudgetExpenseLedgers, DataTable dtOpeningBalance, DataTable dtClosingBalance)
        {
            ResultArgs resultargs = new ResultArgs();
            string column = (YearNo == 0 ? "ACTUAL_AMOUNT" : "ACTUAL_AMOUNT");
            //#. Clear existing user defined Ledgers for years
            resultargs = DeleteUserDefinedBudgetDetails(ProjectId, DateFromYear, DateToYear);
            if (resultargs.Success)
            {
                //# Income Ledgers
                dtUserDefinedBudgetIncomeLedgers.DefaultView.RowFilter = column + ">0";
                DataTable dtUpdateBudgetLedgers = dtUserDefinedBudgetIncomeLedgers.DefaultView.ToTable();
                resultargs = UpdateUserDefinedBudgetLedgers(ProjectId, YearNo, DateFromYear, DateToYear, dtUpdateBudgetLedgers, TransactionMode.CR);
                if (resultargs.Success)
                {
                    //# Expense Ledgers
                    dtUserDefinedBudgetExpenseLedgers.DefaultView.RowFilter = column + ">0";
                    dtUpdateBudgetLedgers = dtUserDefinedBudgetExpenseLedgers.DefaultView.ToTable();
                    resultargs = UpdateUserDefinedBudgetLedgers(ProjectId, YearNo, DateFromYear, DateToYear, dtUpdateBudgetLedgers, TransactionMode.DR);
                    if (resultargs.Success)
                    {
                        //Opening Balance
                        resultargs = UpdateUserDefinedBudgetBalances(ProjectId, DateFromYear, DateToYear, dtOpeningBalance, Business.BalanceSystem1.BalanceType.OpeningBalance);
                        if (resultargs.Success)
                        {
                            //Closing Balance
                            resultargs = UpdateUserDefinedBudgetBalances(ProjectId, DateFromYear, DateToYear, dtClosingBalance, Business.BalanceSystem1.BalanceType.ClosingBalance);
                        }
                    }
                }
            }

            dtUserDefinedBudgetIncomeLedgers.DefaultView.RowFilter = "";
            dtUserDefinedBudgetExpenseLedgers.DefaultView.RowFilter = "";

            return resultargs;
        }

        private ResultArgs DeleteUserDefinedBudgetDetails(Int32 ProjectId, string DateFromYear, string DateFromToYear)
        {
            ResultArgs resultargs = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.Budget.DeleteUserDefinedBudgetDetailsByYear))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, DateFromYear);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, DateFromToYear);
                resultargs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultargs;
        }

        private ResultArgs UpdateUserDefinedBudgetLedgers(Int32 ProjectId, Int32 YearNo, string DateFromYear, string DateFromToYear, DataTable dtUserBudgetLedgers, TransactionMode BudgetTransmode)
        {
            ResultArgs resultargs = new ResultArgs();
            string column = (YearNo == 0 ? "ACTUAL_AMOUNT" : "ACTUAL_AMOUNT");
            if (dtUserBudgetLedgers.Rows.Count > 0)
            {
                foreach (DataRow dr in dtUserBudgetLedgers.Rows)
                {
                    Int32 groupid = this.NumberSet.ToInteger(dr[this.AppSchema.Ledger.GROUP_IDColumn.ColumnName].ToString());
                    Int32 ledgerid = this.NumberSet.ToInteger(dr[this.AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString());
                    Int32 naturerid = this.NumberSet.ToInteger(dr[this.AppSchema.LedgerGroup.NATURE_IDColumn.ColumnName].ToString());
                    double amount = this.NumberSet.ToDouble(dr[column].ToString());
                    string transmode = BudgetTransmode.ToString(); //(naturerid == 1 || naturerid == 3 ? "CR" : "DR");
                    //string transflag = "TR";

                    if (ledgerid > 0 && amount > 0)
                    {
                        using (DataManager dataManager = new DataManager(SQLCommand.Budget.UpdateUserDefinedBudgetDetails))
                        {
                            dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                            dataManager.Parameters.Add(this.AppSchema.Ledger.GROUP_IDColumn, groupid);
                            dataManager.Parameters.Add(this.AppSchema.Budget.LEDGER_IDColumn, ledgerid);
                            dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, DateFromYear);
                            dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, DateFromToYear);
                            dataManager.Parameters.Add(this.AppSchema.Budget.AMOUNTColumn, amount);
                            dataManager.Parameters.Add(this.AppSchema.Budget.TRANS_MODEColumn, transmode);

                            resultargs = dataManager.UpdateData();
                        }
                    }
                    if (!resultargs.Success)
                    {
                        break;
                    }
                }
            }
            else
                resultargs.Success = true;

            return resultargs;
        }

        private ResultArgs UpdateUserDefinedBudgetBalances(Int32 ProjectId, string DateFromYear, string DateFromToYear, DataTable dtBalance, Bosco.Model.Business.BalanceSystem1.BalanceType balancetype)
        {
            ResultArgs resultargs = new ResultArgs();

            foreach (DataRow dr in dtBalance.Rows)
            {
                Int32 ledgerid = 0;
                Int32 groupid = this.NumberSet.ToInteger(dr["GROUP_ID"].ToString()); ;
                double amount = this.NumberSet.ToDouble(dr["AMOUNT"].ToString());
                string transmode = (balancetype == Business.BalanceSystem1.BalanceType.OpeningBalance ? "CR" : "DR");
                //string transflag = (dr["TRANS_FLAG"].ToString());

                //if (amount > 0)
                //{
                using (DataManager dataManager = new DataManager(SQLCommand.Budget.UpdateUserDefinedBudgetDetails))
                {
                    dataManager.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.GROUP_IDColumn, groupid);
                    dataManager.Parameters.Add(this.AppSchema.Budget.LEDGER_IDColumn, ledgerid);
                    dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, DateFromYear);
                    dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, DateFromToYear);
                    dataManager.Parameters.Add(this.AppSchema.Budget.AMOUNTColumn, amount);
                    dataManager.Parameters.Add(this.AppSchema.Budget.TRANS_MODEColumn, transmode);
                    //dataManager.Parameters.Add(this.AppSchema.LedgerBalance.TRANS_FLAGColumn, transflag);

                    resultargs = dataManager.UpdateData();
                }
                //}
                if (!resultargs.Success)
                {
                    break;
                }
            }
            return resultargs;
        }

        #region Annual Budget
        public ResultArgs GetAnnualBudget()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.AnnualBudgetProject))
            {
                //dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);

                //int projectids = this.NumberSet.ToInteger(MultipleProjectId);
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, MultipleProjectId.Equals(string.Empty) ? "0" : MultipleProjectId);
                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_TYPE_IDColumn, BudgetTypeId);
                //dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, DateSet.ToDate(YearFrom, false));
                //dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, DateSet.ToDate(YearTo, false));
                dataManager.Parameters.Add(AppSchema.Budget.DATE_FROMColumn, DateSet.ToDate(DateFrom, false));
                dataManager.Parameters.Add(AppSchema.Budget.DATE_TOColumn, DateSet.ToDate(DateTo, false));
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs GetCalenderYearBudget()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.CalendarYearBudget))
            {
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_TYPE_IDColumn, BudgetTypeId);
                dataManager.Parameters.Add(AppSchema.Budget.DATE_FROMColumn, DateSet.ToDate(DateFrom, false));
                dataManager.Parameters.Add(AppSchema.Budget.DATE_TOColumn, DateSet.ToDate(DateTo, false));
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBudgetAdd()
        {
            DateTime fromdate = DateSet.ToDate(DateFrom, false);
            DateTime todate = DateSet.ToDate(DateTo, false);

            SQLCommand.Budget budgetcmd = SQLCommand.Budget.BudgetAddEditDetails;
            //31/07/2020, for cmf, to get all mapped ledgers
            if (IS_CMF_CONGREGATION && (!IS_CMFKOL))
            {
                budgetcmd = SQLCommand.Budget.BudgetAddEditDetailsAllLedgers;
            }

            using (DataManager dataManager = new DataManager(budgetcmd))
            {
                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, MultipleProjectId.Equals(string.Empty) ? "0" : MultipleProjectId);
                if (BudgetTypeId == (int)BudgetType.BudgetMonth)
                {
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, fromdate.AddMonths(-1));
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, todate.AddMonths(-1));
                }
                else if (BudgetTypeId == (int)BudgetType.BudgetPeriod)
                {
                    FetchRecentBudgetList();
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, DateTo);
                }
                else
                {
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, fromdate.AddYears(-1));
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, todate.AddYears(-1));
                }

                /*if (setting.ShowBudgetLedgerActualBalance == "1")
                {
                    dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_TYPEColumn, "JN");
                }

                //On 31/07/2020, to fix ledger balacen
                dataManager.Parameters.Add(AppSchema.Budget.SHOWBUDGETLEDGERSEPARATERECEIPTPAYMENTACTUALBALANCEColumn, setting.ShowBudgetLedgerSeparateReceiptPaymentActualBalance);
                */

                //29/07/2021, to show ledger actual balance based on settings
                if (this.ShowBudgetLedgerActualBalance == "1") //Ledger Closing Balance without Journal Voucher
                {
                    //dataManager.Parameters.Add(reportSetting1.BUDGET_LEDGER.VOUCHER_TYPEColumn, VoucherSubTypes.JN.ToString());
                    dataManager.Parameters.Add(AppSchema.Budget.SHOWBUDGETLEDGERSEPARATERECEIPTPAYMENTACTUALBALANCEColumn, "1");
                }
                else if (this.ShowBudgetLedgerActualBalance == "2") // Receipts Vocuehr & Payments Voucher separately
                {
                    //dataManager.Parameters.Add(reportSetting1.BUDGET_LEDGER.VOUCHER_TYPEColumn, VoucherSubTypes.JN.ToString());
                    dataManager.Parameters.Add(AppSchema.Budget.SHOWBUDGETLEDGERSEPARATERECEIPTPAYMENTACTUALBALANCEColumn, "2");
                }
                else //Ledger Closing Balance with Journal Voucher
                {
                    dataManager.Parameters.Add(AppSchema.Budget.SHOWBUDGETLEDGERSEPARATERECEIPTPAYMENTACTUALBALANCEColumn, "0");
                }

                //For Closed Date
                dataManager.Parameters.Add(AppSchema.Ledger.DATE_CLOSEDColumn, fromdate);

                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_TYPE_IDColumn, this.BudgetTypeId);
                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_TRANS_MODEColumn, BudgetTransMode);

                //For SDBINM, Skip few ledgers
                if (IS_SDB_INM && !string.IsNullOrEmpty(SDBINM_SkippedLedgerIds))
                {
                    dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_ID_COLLECTIONColumn.ColumnName, SDBINM_SkippedLedgerIds);
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            //using (DataManager dataManager = new DataManager(SQLCommand.Budget.AnnualBudgetFetchAdd))
            //{
            //    dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, MultipleProjectId.Equals(string.Empty) ? "0" : MultipleProjectId);
            //    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, fromdate.AddYears(-1));
            //    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, todate.AddYears(-1));
            //    dataManager.Parameters.Add(AppSchema.Budget.BUDGET_TYPE_IDColumn, this.BudgetTypeId);
            //    dataManager.Parameters.Add(AppSchema.Budget.BUDGET_TRANS_MODEColumn, BudgetTransMode);
            //    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
            //    resultArgs = dataManager.FetchData(DataSource.DataTable);
            //}
            return resultArgs;
        }

        public ResultArgs FetchBudgetMysoreAddDetails()
        {
            DateTime fromdate = DateSet.ToDate(DateFrom, false);
            DateTime todate = DateSet.ToDate(DateTo, false);

            //Get Previous Budget Id
            DateTime PrevBudgetDateFrom = fromdate.AddMonths(-1);
            //DateTime PrevBudgetDateTo = fromdate.AddMonths(1).AddDays(-1);
            DateTime PrevBudgetDateTo = PrevBudgetDateFrom.AddMonths(1).AddDays(-1);
            PreviousBudgetId = GetBudgetIdByDateRangeProjectd(PrevBudgetDateFrom, PrevBudgetDateTo, ProjectId.ToString());

            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchMysoreBudget))
            {
                /*dataManager.Parameters.Add(AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId); //MultipleProjectId.Equals(string.Empty) ? "0" : MultipleProjectId);
                if (BudgetTypeId == (int)BudgetType.BudgetMonth)
                {
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, fromdate.AddMonths(-1));
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, todate.AddMonths(-1));
                }
                else if (BudgetTypeId == (int)BudgetType.BudgetPeriod)
                {
                    FetchRecentBudgetList();
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, DateTo);
                }
                else
                {
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, fromdate.AddYears(-1));
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, todate.AddYears(-1));
                }
                if (setting.ShowBudgetLedgerActualBalance == "1")
                {
                    dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_TYPEColumn, "JN");
                }

                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_TYPE_IDColumn, this.BudgetTypeId);
                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_TRANS_MODEColumn, BudgetTransMode);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;*/

                dataManager.Parameters.Add(AppSchema.Budget.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(AppSchema.Budget.MONTH1_BUDGET_IDColumn, M1BudgetId);
                dataManager.Parameters.Add(AppSchema.Budget.MONTH2_BUDGET_IDColumn, M2BudgetId);

                dataManager.Parameters.Add(AppSchema.Budget.PREVIOUS_BUDGET_IDColumn, PreviousBudgetId);
                dataManager.Parameters.Add(AppSchema.Budget.DATE_FROMColumn, PrevBudgetDateFrom);
                dataManager.Parameters.Add(AppSchema.Budget.DATE_TOColumn, PrevBudgetDateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgerDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.BindGrid))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public void FetchRecentBudgetList()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchRecentBudgetList))
            {
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, MultipleProjectId.Equals(string.Empty) ? "0" : MultipleProjectId);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, DateTo);
                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_TYPE_IDColumn, this.BudgetTypeId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    //chinna 08.03.2018
                    DateFrom = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.DATE_FROMColumn.ColumnName].ToString();
                    DateTo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.DATE_TOColumn.ColumnName].ToString();
                }
                else
                {
                    string fromdate = DateSet.ToDate(DateFrom, false).AddYears(-1).ToShortDateString();
                    string todate = DateSet.ToDate(DateTo, false).AddYears(-1).ToShortDateString();
                    DateFrom = fromdate;
                    DateTo = todate;
                }
            }
        }

        public ResultArgs FetchLastMonthBudget()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchLastBudgetMonth))
            {
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, YearFrom);
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, YearTo);
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, MultipleProjectId.Equals(string.Empty) ? "0" : MultipleProjectId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBudgetProjectsLookup()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchBudgetProjectforLookup))
            {
                if (!IsFullRightsReservedUser)
                {
                    dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, this.LoginUserRoleId);
                }

                if (!string.IsNullOrEmpty(ProjectClosedDate))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, ProjectClosedDate);
                }

                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, DateSet.ToDate(DateFrom, false));
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, DateSet.ToDate(DateTo, false));

                //On 09/02/2022, load Projects based on start of the project (Current FY To)
                if (!string.IsNullOrEmpty(DateTo) && DateSet.ToDate(DateTo, false) != DateTime.MinValue)
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, DateTo);
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBudgetProject()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchProjectforBudget))
            {
                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_IDColumn, BudgetId);

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBudgetEdit()
        {

            SQLCommand.Budget budgetcmd = SQLCommand.Budget.BudgetAddEditDetails;
            //31/07/2020, for cmf, to get all mapped ledgers
            if (IS_CMF_CONGREGATION && (!IS_CMFKOL))
            {
                budgetcmd = SQLCommand.Budget.BudgetAddEditDetailsAllLedgers;
            }

            using (DataManager dataManager = new DataManager(budgetcmd))
            {
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, MultipleProjectId.Equals(string.Empty) ? "0" : MultipleProjectId);
                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_TRANS_MODEColumn, BudgetTransMode);
                if (BudgetTypeId == (int)BudgetType.BudgetByAnnualYear)
                {
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, DateSet.ToDate(DateFrom, false).AddYears(-1));
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, DateSet.ToDate(DateTo, false).AddYears(-1)); //.AddDays(-1)); 04.04.2017 to allow 31 Date
                    dataManager.Parameters.Add(AppSchema.LedgerBalance.BALANCE_DATEColumn, DateSet.ToDate(YearFrom, false).AddYears(-1));
                    dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_DATEColumn, DateSet.ToDate(YearTo, false).AddYears(-1));
                }
                else if (BudgetTypeId == (int)BudgetType.BudgetMonth)
                {
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, DateSet.ToDate(DateFrom, false).AddMonths(-1));
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, DateSet.ToDate(DateTo, false).AddMonths(-1));
                }
                else if (BudgetTypeId == (int)BudgetType.BudgetPeriod)
                {
                    // chinna 08.03.2018
                    if (BudgetTransMode == TransactionMode.CR.ToString())
                    {
                        FetchRecentBudgetList();
                    }
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, DateTo);
                }
                else
                {
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, DateSet.ToDate(DateFrom, false).AddYears(-1));
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, DateSet.ToDate(DateTo, false).AddYears(-1));
                }
                /*if (setting.ShowBudgetLedgerActualBalance == "1")
                {
                    dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_TYPEColumn, "JN");
                }

                //On 31/07/2020, to fix ledger balacen
                dataManager.Parameters.Add(AppSchema.Budget.SHOWBUDGETLEDGERSEPARATERECEIPTPAYMENTACTUALBALANCEColumn, setting.ShowBudgetLedgerSeparateReceiptPaymentActualBalance);
                */

                //29/07/2021, to show ledger actual balance based on settings
                if (this.ShowBudgetLedgerActualBalance == "1") //Ledger Closing Balance without Journal Voucher
                {
                    //dataManager.Parameters.Add(reportSetting1.BUDGET_LEDGER.VOUCHER_TYPEColumn, VoucherSubTypes.JN.ToString());
                    dataManager.Parameters.Add(AppSchema.Budget.SHOWBUDGETLEDGERSEPARATERECEIPTPAYMENTACTUALBALANCEColumn, "1");
                }
                else if (this.ShowBudgetLedgerActualBalance == "2") // Receipts Vocuehr & Payments Voucher separately
                {
                    //dataManager.Parameters.Add(reportSetting1.BUDGET_LEDGER.VOUCHER_TYPEColumn, VoucherSubTypes.JN.ToString());
                    dataManager.Parameters.Add(AppSchema.Budget.SHOWBUDGETLEDGERSEPARATERECEIPTPAYMENTACTUALBALANCEColumn, "2");
                }
                else //Ledger Closing Balance with Journal Voucher
                {
                    dataManager.Parameters.Add(AppSchema.Budget.SHOWBUDGETLEDGERSEPARATERECEIPTPAYMENTACTUALBALANCEColumn, "0");
                }

                //For Closed Date
                dataManager.Parameters.Add(AppSchema.Ledger.DATE_CLOSEDColumn, DateFrom);

                //For SDBINM, Skip few ledgers
                if (IS_SDB_INM && !string.IsNullOrEmpty(SDBINM_SkippedLedgerIds))
                {
                    dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_ID_COLLECTIONColumn.ColumnName, SDBINM_SkippedLedgerIds);
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchMonthDitributionbyAnnual()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.BudgetMonthlyDistribution))
            {
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, MultipleProjectId.Equals(string.Empty) ? "0" : MultipleProjectId);
                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_TRANS_MODEColumn, BudgetTransMode);
                if (BudgetTypeId == (int)BudgetType.BudgetByAnnualYear)
                {
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, DateSet.ToDate(YearFrom, false));
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, DateSet.ToDate(YearTo, false));
                    dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, DateSet.ToDate(DateFrom, false));
                    dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, DateSet.ToDate(DateTo, false));
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// This Budget Details except Mysore Budget
        /// </summary>
        /// <returns></returns>
        public ResultArgs SaveAnnualBudget()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = SaveBudgetMasterDetails(dataManager, true);

                if ((IS_ABEBEN_DIOCESE))
                {
                    if (resultArgs != null && resultArgs.Success)
                    {
                        BudgetId = BudgetId.Equals(0) ? NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : BudgetId;
                        resultArgs = SaveAnnualBudgetLedger(dataManager);
                    }
                }
                else
                {
                    if (resultArgs != null && resultArgs.Success)
                    {
                        BudgetId = BudgetId.Equals(0) ? NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : BudgetId;

                        //On 21/04/2023, To update Developmental projects or new projects
                        if (resultArgs != null && resultArgs.Success && BudgetId > 0)
                        {
                            resultArgs = SaveBudgetDevelopmentalProjects(dataManager, BudgetId, dtBudgetDevelopmentalNewProjects);

                            if (resultArgs != null && resultArgs.Success)
                            {
                                resultArgs = SaveAnnualBudgetLedger(dataManager);

                                if (resultArgs != null && resultArgs.Success)
                                {
                                    resultArgs = SaveBudgetStrengthDetails(dataManager, BudgetId, dtBudgetStrengthRecords);
                                }
                            }
                        }
                    }
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }


        public ResultArgs DeleteAllCommunity()
        {
            ResultArgs result = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.DeleteAllCommunityLedger))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = false;
                result = dataManager.FetchData(DataSource.DataTable);
            }
            return result;
        }


        public ResultArgs SaveAllCommunity(string ledgername, double PrevApproved, double PrevActual, double Proposed, double approved, string budgettransmode)
        {
            ResultArgs result = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.CheckAllCommunityLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, ledgername);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = false;
                result = dataManager.FetchData(DataSource.DataTable);

                if (result.Success && result.DataSource.Table != null)
                {
                    bool exits = (result.DataSource.Table.Rows.Count == 0);
                    using (DataManager dataManager1 = new DataManager((exits ? SQLCommand.Budget.InsertAllCommunityLedger : SQLCommand.Budget.UpdateAllCommunityLedger)))
                    {
                        dataManager1.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, ledgername);
                        dataManager1.Parameters.Add(this.AppSchema.Budget.PREV_APPROVED_AMOUNTColumn, PrevApproved);
                        dataManager1.Parameters.Add(this.AppSchema.Budget.PREV_ACTUAL_AMOUNTColumn, PrevActual);

                        dataManager1.Parameters.Add(this.AppSchema.Budget.PROPOSED_AMOUNTColumn, Proposed);
                        dataManager1.Parameters.Add(this.AppSchema.Budget.APPROVED_AMOUNTColumn, approved);
                        dataManager1.Parameters.Add(this.AppSchema.Budget.TRANS_MODEColumn, budgettransmode);
                        dataManager1.DataCommandArgs.IsDirectReplaceParameter = false;
                        result = dataManager1.UpdateData();
                    }

                }
            }

            return result;
        }

        /// <summary>
        /// Save Mysore Budget Details
        /// </summary>
        /// <returns></returns>
        public ResultArgs SaveAnnualBudgetForMysore()
        {
            using (DataManager dataManager = new DataManager())
            {
                //For One month budget//first month of two months budget
                //BudgetName = DateSet.ToDate(DateFrom, false).ToString("MMM-yyyy");
                BudgetId = M1BudgetId;
                dataManager.BeginTransaction();
                resultArgs = SaveBudgetMasterDetails(dataManager, true);
                if (resultArgs != null && resultArgs.Success)
                {
                    BudgetId = M1BudgetId = BudgetId.Equals(0) ? NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : BudgetId;
                    resultArgs = SaveAnnualBudgetSubLedger(dataManager, false);
                }

                //For Next Month Budget
                if (IsTwoMonthBudget && resultArgs.Success)
                {
                    //BudgetName = DateSet.ToDate(NextDateFrom,false).ToString("MMM-yyyy");
                    BudgetId = M2BudgetId;
                    DateFrom = NextDateFrom;
                    DateTo = NextDateTo;

                    resultArgs = SaveBudgetMasterDetails(dataManager, true);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        BudgetId = M2BudgetId = BudgetId.Equals(0) ? NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : BudgetId;
                        resultArgs = SaveAnnualBudgetSubLedger(dataManager, true);
                    }
                }

                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        /// <summary>
        /// Sub Ledger Transaction Valid
        /// </summary>
        /// <param name="dataManagers"></param>
        /// <returns></returns>
        private ResultArgs SaveAnnualBudgetSubLedger(DataManager dataManagers, bool nextmont)
        {
            resultArgs = DeleteBudgetLedgerDetails(dataManagers);
            if (resultArgs.Success)
            {
                resultArgs = DeleteBudgetSubLedger(dataManagers);
                if (resultArgs.Success)
                {
                    resultArgs = DeleteAnnualBudgetDistributionDetails(dataManagers);
                    if (resultArgs.Success)
                    {
                        resultArgs = DeleteBudgetProjectDetails(dataManagers);
                        if (resultArgs.Success)
                        {
                            resultArgs = DeleteBudgetStatisticsDetails(dataManagers);
                            if (dtBudgetLedgers != null)
                            {
                                foreach (DataRow drItem in dtBudgetLedgers.Rows)
                                {
                                    using (DataManager dataManager = new DataManager(SQLCommand.Budget.AnnualBudgetLedgerAdd))
                                    {
                                        dataManager.Database = dataManagers.Database;
                                        if (drItem[this.AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString() != string.Empty)
                                        {
                                            dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                                            dataManager.Parameters.Add(this.AppSchema.Budget.LEDGER_IDColumn, NumberSet.ToInteger(drItem[this.AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString()));

                                            if (nextmont)
                                            {
                                                dataManager.Parameters.Add(this.AppSchema.Budget.PROPOSED_AMOUNTColumn, NumberSet.ToDecimal(drItem["M2_PROPOSED_AMOUNT"].ToString()));
                                                dataManager.Parameters.Add(this.AppSchema.Budget.APPROVED_AMOUNTColumn, NumberSet.ToDecimal(drItem["M2_APPROVED_AMOUNT"].ToString()));
                                            }
                                            else
                                            {
                                                dataManager.Parameters.Add(this.AppSchema.Budget.PROPOSED_AMOUNTColumn, NumberSet.ToDecimal(drItem["PROPOSED_CURRENT_YR"].ToString()));
                                                dataManager.Parameters.Add(this.AppSchema.Budget.APPROVED_AMOUNTColumn, NumberSet.ToDecimal(drItem["APPROVED_CURRENT_YR"].ToString()));
                                            }

                                            dataManager.Parameters.Add(this.AppSchema.Budget.TRANS_MODEColumn, drItem["BUDGET_TRANS_MODE"].ToString());
                                            dataManager.Parameters.Add(this.AppSchema.Budget.NARRATIONColumn, drItem[AppSchema.Budget.NARRATIONColumn.ColumnName].ToString());
                                            dataManager.Parameters.Add(this.AppSchema.Budget.HO_NARRATIONColumn, drItem[AppSchema.Budget.HO_NARRATIONColumn.ColumnName].ToString());
                                            dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                                            resultArgs = dataManager.UpdateData();
                                        }
                                    }
                                    if (!resultArgs.Success) { break; }
                                }
                                if (resultArgs.Success)
                                {
                                    using (DataManager dataManger = new DataManager(SQLCommand.Budget.AnnualBudgetProjectAdd))
                                    {
                                        dataManger.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                                        dataManger.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, ProjectId);
                                        dataManger.DataCommandArgs.IsDirectReplaceParameter = true;
                                        resultArgs = dataManger.UpdateData();
                                    }
                                }
                                if (resultArgs.Success)
                                {
                                    resultArgs = SaveBudgetSubLedger(dataManagers, nextmont);
                                }
                            }
                        }
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs SaveAnnualBudgetLedger(DataManager dataManagers)
        {
            resultArgs = DeleteBudgetLedgerDetails(dataManagers);
            if (resultArgs.Success)
            {
                resultArgs = DeleteAnnualBudgetDistributionDetails(dataManagers);
                if (resultArgs.Success)
                {
                    resultArgs = DeleteBudgetProjectDetails(dataManagers);
                    if (resultArgs.Success)
                    {
                        resultArgs = DeleteBudgetStatisticsDetails(dataManagers);
                        if (dtBudgetLedgers != null)
                        {
                            foreach (DataRow drItem in dtBudgetLedgers.Rows)
                            {
                                using (DataManager dataManager = new DataManager(SQLCommand.Budget.AnnualBudgetLedgerAdd))
                                {
                                    dataManager.Database = dataManagers.Database;
                                    if (drItem[this.AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString() != string.Empty)
                                    {
                                        dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                                        dataManager.Parameters.Add(this.AppSchema.Budget.LEDGER_IDColumn, NumberSet.ToInteger(drItem[this.AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString()));
                                        dataManager.Parameters.Add(this.AppSchema.Budget.PROPOSED_AMOUNTColumn, NumberSet.ToDecimal(drItem["PROPOSED_CURRENT_YR"].ToString()));
                                        dataManager.Parameters.Add(this.AppSchema.Budget.APPROVED_AMOUNTColumn, NumberSet.ToDecimal(drItem["APPROVED_CURRENT_YR"].ToString()));
                                        dataManager.Parameters.Add(this.AppSchema.Budget.TRANS_MODEColumn, drItem["BUDGET_TRANS_MODE"].ToString());
                                        dataManager.Parameters.Add(this.AppSchema.Budget.NARRATIONColumn, drItem[AppSchema.Budget.NARRATIONColumn.ColumnName].ToString());
                                        dataManager.Parameters.Add(this.AppSchema.Budget.HO_NARRATIONColumn, drItem[AppSchema.Budget.HO_NARRATIONColumn.ColumnName].ToString());
                                        dataManager.DataCommandArgs.IsDirectReplaceParameter = false;
                                        resultArgs = dataManager.UpdateData();
                                        if (resultArgs.Success && this.EnableCostCentreBudget == 1)
                                        {
                                            LedgerId = NumberSet.ToInteger(drItem[this.AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString());
                                            string transMode = drItem["BUDGET_TRANS_MODE"].ToString();
                                            resultArgs = SaveBudgetCostcentre(transMode, dataManager);
                                        }
                                    }
                                }
                                if (!resultArgs.Success) { break; }
                            }
                            if (resultArgs.Success)
                            {
                                string[] projects = MultipleProjectId.Split(',');
                                foreach (string project in projects)
                                {
                                    using (DataManager dataManger = new DataManager(SQLCommand.Budget.AnnualBudgetProjectAdd))
                                    {
                                        dataManger.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                                        dataManger.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, project);
                                        dataManger.DataCommandArgs.IsDirectReplaceParameter = true;
                                        resultArgs = dataManger.UpdateData();
                                    }
                                    if (!resultArgs.Success) { break; }
                                }
                            }
                            if (resultArgs.Success)
                            {
                                resultArgs = SaveBudgetStatisticsDetails(dataManagers);
                            }
                        }
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs SaveMonthDistributionBudget()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = SaveDistribution(dataManager);
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs SaveDistribution(DataManager dataManagers)
        {
            resultArgs = DeleteBudgetDistributionDetails(dataManagers);
            if (resultArgs.Success)
            {
                if (dtBudgetDistribution != null)
                {
                    foreach (DataRow drItem in dtBudgetDistribution.Rows)
                    {
                        using (DataManager dataManager = new DataManager(SQLCommand.Budget.AnnualBudgetDistributionAdd))
                        {
                            dataManager.Database = dataManagers.Database;
                            if (drItem[this.AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString() != string.Empty)
                            {
                                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                                dataManager.Parameters.Add(this.AppSchema.Budget.LEDGER_IDColumn, NumberSet.ToInteger(drItem[this.AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString()));
                                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, DateFrom);
                                dataManager.Parameters.Add(this.AppSchema.Budget.PROPOSED_AMOUNTColumn, NumberSet.ToDecimal(drItem["MONTH_PROPOSED_BUDGET_AMOUNT"].ToString()));
                                dataManager.Parameters.Add(this.AppSchema.Budget.APPROVED_AMOUNTColumn, NumberSet.ToDecimal(drItem["MONTH_APPROVED_BUDGET_AMOUNT"].ToString()));
                                dataManager.Parameters.Add(this.AppSchema.Budget.TRANS_MODEColumn, "DR");
                                dataManager.Parameters.Add(this.AppSchema.Budget.NARRATIONColumn, drItem["NARRATION"].ToString());
                                dataManager.Parameters.Add(this.AppSchema.Budget.HO_NARRATIONColumn, string.Empty);
                                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                                resultArgs = dataManager.UpdateData();
                            }
                        }
                        if (!resultArgs.Success) { break; }
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs DeleteBudgetDistributionDetails(DataManager dataManagers)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.Budget.DeleteBudgetDistributionbyBudgetandDate))
            {
                datamanager.Database = dataManagers.Database;
                datamanager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                datamanager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, DateFrom);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs SaveBudgetCostcentre(string transmode, DataManager dm)
        {
            if (resultArgs.Success)
            {
                if (dtBudgetCostCentre != null)
                {
                    dtBudgetCostCentre.DefaultView.RowFilter = string.Empty;
                    dtBudgetCostCentre.DefaultView.RowFilter = this.AppSchema.BudgetCostCentre.LEDGER_IDColumn.ColumnName + "=" + LedgerId + " AND " +
                                    this.AppSchema.BudgetCostCentre.TRANS_MODEColumn.ColumnName + " = '" + transmode + "'";

                    if (LedgerId == 1201)
                    {

                    }
                    foreach (DataRowView drv in dtBudgetCostCentre.DefaultView)
                    {
                        using (DataManager dataManager = new DataManager(SQLCommand.Budget.UpdateBudgetCostCentre))
                        {
                            dataManager.Database = dm.Database;
                            if (LedgerId > 0 && BudgetId > 0)
                            {
                                dataManager.Parameters.Add(this.AppSchema.BudgetCostCentre.BUDGET_IDColumn, BudgetId);
                                dataManager.Parameters.Add(this.AppSchema.BudgetCostCentre.LEDGER_IDColumn, LedgerId);
                                dataManager.Parameters.Add(this.AppSchema.BudgetCostCentre.COST_CENTRE_IDColumn, NumberSet.ToInteger(drv[this.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName].ToString()));
                                dataManager.Parameters.Add(this.AppSchema.BudgetCostCentre.PROPOSED_AMOUNTColumn, this.NumberSet.ToDecimal(drv[this.AppSchema.BudgetCostCentre.PROPOSED_AMOUNTColumn.ColumnName].ToString()));
                                dataManager.Parameters.Add(this.AppSchema.BudgetCostCentre.APPROVED_AMOUNTColumn, this.NumberSet.ToDecimal(drv[this.AppSchema.BudgetCostCentre.APPROVED_AMOUNTColumn.ColumnName].ToString()));
                                dataManager.Parameters.Add(this.AppSchema.BudgetCostCentre.TRANS_MODEColumn, drv[this.AppSchema.BudgetCostCentre.TRANS_MODEColumn.ColumnName].ToString().Trim());
                                resultArgs = dataManager.UpdateData();
                            }
                            if (!resultArgs.Success) { break; }
                        }
                    }

                    dtBudgetCostCentre.DefaultView.RowFilter = string.Empty;
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// Save Budget Sub Ledgers
        /// </summary>
        /// <param name="dataManagers"></param>
        /// <returns></returns>
        private ResultArgs SaveBudgetSubLedger(DataManager dataManagers, bool nextmonth)
        {
            if (resultArgs.Success)
            {
                if (dtBudgetSubLedgers != null && dtBudgetSubLedgers.Rows.Count > 0)
                {
                    foreach (DataRow drItem in dtBudgetSubLedgers.Rows)
                    {
                        int ledgerid = NumberSet.ToInteger(drItem[this.AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString());
                        int subledgerid = NumberSet.ToInteger(drItem[this.AppSchema.SubLedger.SUB_LEDGER_IDColumn.ColumnName].ToString());
                        string subledgername = drItem[this.AppSchema.SubLedger.SUB_LEDGER_NAMEColumn.ColumnName].ToString();

                        double subledgerproposedamount = 0;
                        double subledgerapprovedamount = 0;
                        double M2subledgerproposedamount = 0;
                        double M2subledgerapprovedamount = 0;
                        if (nextmonth)
                        {
                            M2subledgerproposedamount = this.NumberSet.ToDouble(drItem["M2_PROPOSED_AMOUNT"].ToString());
                            M2subledgerapprovedamount = this.NumberSet.ToDouble(drItem["M2_APPROVED_AMOUNT"].ToString());
                        }
                        else
                        {
                            subledgerproposedamount = this.NumberSet.ToDouble(drItem["PROPOSED_CURRENT_YR"].ToString());
                            subledgerapprovedamount = this.NumberSet.ToDouble(drItem["APPROVED_CURRENT_YR"].ToString());
                        }
                        string narration = drItem[this.AppSchema.Budget.NARRATIONColumn.ColumnName].ToString();

                        //If sub ledger is newly updated..insert into master sub ledger
                        if (subledgerid == 0)
                        {
                            resultArgs = InsertNewSubLedgerName(dataManagers, subledgerid, subledgername, ledgerid);
                            if (resultArgs.Success)
                            {
                                subledgerid = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                            }
                        }
                        if (resultArgs.Success)
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.Budget.SaveLedgerSubLedger))
                            {
                                dataManager.Database = dataManagers.Database;
                                if (ledgerid > 0 && subledgerid > 0 && (subledgerproposedamount > 0 || M2subledgerproposedamount > 0))
                                {
                                    dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                                    dataManager.Parameters.Add(this.AppSchema.Budget.LEDGER_IDColumn, ledgerid);
                                    dataManager.Parameters.Add(this.AppSchema.VoucherSubLedger.SUB_LEDGER_IDColumn, subledgerid);
                                    if (nextmonth)
                                    {
                                        dataManager.Parameters.Add(this.AppSchema.Budget.PROPOSED_AMOUNTColumn, M2subledgerproposedamount);
                                        dataManager.Parameters.Add(this.AppSchema.Budget.APPROVED_AMOUNTColumn, M2subledgerapprovedamount);
                                    }
                                    else
                                    {
                                        dataManager.Parameters.Add(this.AppSchema.Budget.PROPOSED_AMOUNTColumn, subledgerproposedamount);
                                        dataManager.Parameters.Add(this.AppSchema.Budget.APPROVED_AMOUNTColumn, subledgerapprovedamount);
                                    }
                                    dataManager.Parameters.Add(this.AppSchema.Budget.TRANS_MODEColumn, "DR");
                                    dataManager.Parameters.Add(this.AppSchema.Budget.NARRATIONColumn, narration);
                                    dataManager.Parameters.Add(this.AppSchema.Budget.HO_NARRATIONColumn, string.Empty);
                                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                                    resultArgs = dataManager.UpdateData();
                                }
                                if (!resultArgs.Success) { break; }
                            }
                        }
                        else
                        {
                            if (!resultArgs.Success) { break; }
                        }
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs InsertNewSubLedgerName(DataManager dataManager, Int32 subledgerid, string subledgername, Int32 LedgerId)
        {
            if (subledgerid == 0)
            {
                using (SubLedgerSystem subledgersystem = new SubLedgerSystem())
                {
                    subledgersystem.SubLedgerId = subledgerid;
                    subledgersystem.SubLedgerName = subledgername;
                    int id = subledgersystem.isExistSubLedgerDetails();
                    if (id == 0)
                    {
                        subledgersystem.SubLedgerName = subledgername;
                        resultArgs = subledgersystem.SaveSubLedgerDetails(dataManager);
                        if (resultArgs.Success)
                        {
                            subledgerid = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                        }
                    }
                    else
                    {
                        resultArgs.RowUniqueId = id;
                        resultArgs.Success = true;
                        subledgerid = id;
                    }

                    if (resultArgs.Success && LedgerId > 0 && subledgerid > 0)
                    {
                        subledgersystem.LedgerId = LedgerId;
                        subledgersystem.SubLedgerId = subledgerid;
                        subledgersystem.MapLedgerwithSubLedger(dataManager);
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs DeleteBudgetCostCenterDetails(DataManager dataManagers)
        {
            //using (DataManager datamanager = new DataManager(SQLCommand.Budget.DeleteBudgetCCdetailsByBudgetId))
            //{
            //    datamanager.Database = dataManagers.Database;
            //    datamanager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
            //    resultArgs = datamanager.UpdateData();
            //}
            return resultArgs;
        }

        //public ResultArgs GetCostCentreDetails()
        //{
        //    using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchCostCentreByLedger))
        //    {
        //        dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
        //        dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.LEDGER_IDColumn, LedgerId);
        //        dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.COST_CENTRE_TABLEColumn, CostCenterTable);
        //        resultArgs = dataManager.FetchData(DataSource.DataTable);
        //    }
        //    return resultArgs;
        //}

        public bool HasBudgetStatistics()
        {
            bool Rtn = false;
            using (DataManager data = new DataManager(SQLCommand.Budget.IsBudgetStatisticsExists))
            {
                resultArgs = data.FetchData(DataSource.Scalar);
            }

            if (resultArgs.Success)
            {
                Int32 noofReinvested = resultArgs.DataSource.Sclar.ToInteger;
                Rtn = (noofReinvested > 0);
            }
            else
            {
                Rtn = true; //if any issues comes, return to lock (exists) 
            }

            return Rtn;
        }

        public bool HasBudgetIncomeLedger()
        {
            bool Rtn = false;
            using (DataManager data = new DataManager(SQLCommand.Budget.IsBudgetIncomeLedgerExists))
            {
                resultArgs = data.FetchData(DataSource.Scalar);
            }

            if (resultArgs.Success)
            {
                Int32 noofReinvested = resultArgs.DataSource.Sclar.ToInteger;
                Rtn = (noofReinvested > 0);
            }
            else
            {
                Rtn = true; //if any issues comes, return to lock (exists) 
            }


            return Rtn;
        }

        public ResultArgs FetchBudgetLedgerByGroup()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchBudgetLedgerGroup))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public Int32 GetBudgetIdByDateRangeProjectd(DateTime budgetfrom, DateTime budgetto, string projectid)
        {
            Int32 rtn = 0;
            ResultArgs resultargs = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchBudgetIdByDateRangeProject))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, budgetfrom);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, budgetto);
                dataManager.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, projectid);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultargs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
            }
            if (resultargs.Success && resultargs.DataSource.Table != null && resultargs.DataSource.Table.Rows.Count > 0)
            {
                rtn = NumberSet.ToInteger(resultargs.DataSource.Table.Rows[0][this.AppSchema.Budget.BUDGET_IDColumn.ColumnName].ToString());
            }
            return rtn;
        }

        /// <summary>
        /// On 22/12/2021, To check Budget group existing
        /// </summary>
        /// <param name="dataManagers"></param>
        /// <returns></returns>
        public bool IsExistsBudgetGroup(string BudgetGroup)
        {
            bool rtn = true;
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.IsBudgetGroupExists))
            {
                dataManager.Parameters.Add(this.AppSchema.BudgetGroup.BUDGET_GROUPColumn, BudgetGroup);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            if (resultArgs.Success)
            {
                rtn = (resultArgs.DataSource.Sclar.ToInteger > 0);
            }
            return rtn;
        }

        /// <summary>
        /// On 22/12/2021, to save Budget Group
        /// </summary>
        /// <param name="dataManagers"></param>
        /// <returns></returns>
        public ResultArgs SaveBudgetGroup(string BudgetGroup, Int32 BudgetGroupSortId)
        {
            if (!string.IsNullOrEmpty(BudgetGroup))
            {
                if (!IsExistsBudgetGroup(BudgetGroup))
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Budget.SaveBudgetGroup))
                    {
                        dataManager.Parameters.Add(this.AppSchema.BudgetGroup.BUDGET_GROUPColumn, BudgetGroup);
                        dataManager.Parameters.Add(this.AppSchema.BudgetGroup.BUDGET_GROUP_SORT_IDColumn, BudgetGroupSortId);
                        resultArgs = dataManager.UpdateData();
                    }
                }
                else
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Budget.UpdateBudgetGroup))
                    {
                        dataManager.Parameters.Add(this.AppSchema.BudgetGroup.BUDGET_GROUPColumn, BudgetGroup);
                        dataManager.Parameters.Add(this.AppSchema.BudgetGroup.BUDGET_GROUP_SORT_IDColumn, BudgetGroupSortId);
                        resultArgs = dataManager.UpdateData();
                    }
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// On 22/12/2021, To check Budget sub group existing
        /// </summary>
        /// <param name="dataManagers"></param>
        /// <returns></returns>
        public bool IsExistsBudgetSubGroup(string BudgetSubGroup)
        {
            bool rtn = true;
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.IsBudgetSubGroupExists))
            {
                dataManager.Parameters.Add(this.AppSchema.BudgetSubGroup.BUDGET_SUB_GROUPColumn, BudgetSubGroup);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            if (resultArgs.Success)
            {
                rtn = (resultArgs.DataSource.Sclar.ToInteger > 0);
            }
            return rtn;
        }

        /// <summary>
        /// On 22/12/2021, to save Budget Sub Group
        /// </summary>
        /// <param name="dataManagers"></param>
        /// <returns></returns>
        public ResultArgs SaveBudgetSubGroup(string BudgetSubGroup, Int32 BudgetSubGroupSortId)
        {
            if (!string.IsNullOrEmpty(BudgetSubGroup))
            {
                if (!IsExistsBudgetSubGroup(BudgetSubGroup))
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Budget.SaveBudgetSubGroup))
                    {
                        dataManager.Parameters.Add(this.AppSchema.BudgetSubGroup.BUDGET_SUB_GROUPColumn, BudgetSubGroup);
                        dataManager.Parameters.Add(this.AppSchema.BudgetSubGroup.BUDGET_SUB_GROUP_SORT_IDColumn, BudgetSubGroupSortId);
                        resultArgs = dataManager.UpdateData();
                    }
                }
                else
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Budget.UpdateBudgetSubGroup))
                    {
                        dataManager.Parameters.Add(this.AppSchema.BudgetSubGroup.BUDGET_SUB_GROUPColumn, BudgetSubGroup);
                        dataManager.Parameters.Add(this.AppSchema.BudgetSubGroup.BUDGET_SUB_GROUP_SORT_IDColumn, BudgetSubGroupSortId);
                        resultArgs = dataManager.UpdateData();
                    }
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// On 20/01/2022, To delete given Budget Group
        /// </summary>
        /// <param name="BudgetSubGroup"></param>
        /// <returns></returns>
        public ResultArgs DeleteBudgetGroup(string BudgetGroup)
        {
            if (!string.IsNullOrEmpty(BudgetGroup))
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Budget.DeleteBudgetGroup))
                {
                    dataManager.Parameters.Add(this.AppSchema.BudgetGroup.BUDGET_GROUPColumn, BudgetGroup);
                    resultArgs = dataManager.UpdateData();
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// On 20/01/2022, To delete given Budget sub Group
        /// </summary>
        /// <param name="BudgetSubGroup"></param>
        /// <returns></returns>
        public ResultArgs DeleteBudgetSubGroup(string BudgetSubGroup)
        {
            if (!string.IsNullOrEmpty(BudgetSubGroup))
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Budget.DeleteBudgetSubGroup))
                {
                    dataManager.Parameters.Add(this.AppSchema.BudgetSubGroup.BUDGET_SUB_GROUPColumn, BudgetSubGroup);
                    resultArgs = dataManager.UpdateData();
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// On 09/10/2024 - new project/ development
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchNewDevelopmentProjectsByFY()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchNewDevelopmentProjectsByFY))
            {
                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.ACC_YEAR_IDColumn, this.AccPeriodId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }


        /// <summary>
        /// On 09/10/2024, To get list of new or development projects which are not linked with main budget (but those items are arleady linked with main budget)
        /// </summary>
        /// <param name="BudgetSubGroup"></param>
        /// <returns></returns>
        public ResultArgs FetchUnLinkedNewProjects()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchUnLinkedNewProjects))
            {
                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.ACC_YEAR_IDColumn, this.AccPeriodId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        /// <summary>
        /// On 09/10/2024, To link skipped main budget's new projects
        /// </summary>
        /// <param name="BudgetSubGroup"></param>
        /// <returns></returns>
        public ResultArgs UpdateUnLinkedNewProjects(Int32 OldBudgetId, Int32 NewBudgetId)
        {
            if (OldBudgetId > 0)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Budget.UpdateUnLinkedNewProjects))
                {
                    dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.ACC_YEAR_IDColumn, this.AccPeriodId);
                    dataManager.Parameters.Add(this.AppSchema.Budget.PREVIOUS_BUDGET_IDColumn.ColumnName, OldBudgetId);
                    dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, NewBudgetId);
                    resultArgs = dataManager.UpdateData();
                }
            }
            return resultArgs;
        }
        #endregion
    }
}
