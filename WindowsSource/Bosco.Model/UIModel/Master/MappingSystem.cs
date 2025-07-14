/********************************************************************************************
 *                                              Class      :MappingSystem.cs
 *                                              Purpose    :All the Major Logics for Mapping 
 *                                              Author     : Carmel Raj M
 *********************************************************************************************/
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using System.Data;
using Bosco.Model.Business;
using Bosco.Model.Transaction;
using System.Linq;
using System;
using AcMEDSync.Model;
using Bosco.Model.UIModel.Master;

namespace Bosco.Model.UIModel
{
    public class MappingSystem : SystemBase
    {
        #region  Decelaration
        ResultArgs resultArgs = null;
        const string AMOUNT = "AMOUNT";
        const string TRANS_MODE = "TRANS_MODE";
        public int VoucherId { get; set; }
        #endregion

        #region Constructor
        public MappingSystem()
        {

        }
        #endregion

        #region Properties
        public bool IsFDLedger { get; set; }
        public int LedgerGroupId { get; set; }
        public int ProjectId { get; set; }
        public int LedgerId { get; set; }
        public string LedgerIdCollection { get; set; }
        public int MapLedgerId { get; set; }
        public int CostCenterId { get; set; }
        public int CostCentreCategoryId { get; set; }
        public int DonorId { get; set; }
        public string CostCenterIDs { get; set; }
        public string FCPurposeIDs { get; set; }
        public string Trans_mode { get; set; }
        public string FDTransType { get; set; }
        public int CostCategoryId { get; set; }
        public int GeneralateId { get; set; }
        public DataTable dtLedgerIDCollection { get; set; }
        public DataTable dtMovedLedgerIDCollection { get; set; }
        public DataTable dtAmount { get; set; }
        public DataTable dtProjectIDCollection { get; set; }
        public DataTable dtCostCenterIDCollection { get; set; }
        public DataTable dtDonorMapping { get; set; }
        public DataTable dtPurposeIDCollection { get; set; }
        public DataTable dtPurposeCCDistribution { get; set; }
        public DataTable dtContributionMapping { get; set; }
        public DataTable dtMappingLedger { get; set; }
        public DataTable dtMergeLedgers { get; set; }
        public DataTable dtCostCategoryIDCollection { get; set; }
        public string OpeningBalanceDate { get; set; }
        public string LedgerNatureTransMode { get; set; }
        public DataTable dtLedgerCCDistribution { get; set; }
        public DataTable dtProjectLedgerApplicableDetails { get; set; }
        public DataTable dtGeneralateIDCollection { get; set; }


        public DataTable dtBudgetActualLedger { get; set; }
        public DataTable dtProjectList { get; set; }

        /// <summary>
        /// On 28/06/2018, This property is used to skip projects which is closed on or equal to this date
        /// </summary>
        public string ProjectClosedDate { get; set; }

        #endregion

        #region Project System
        public ResultArgs FetchProjectsLookup(string yearto = "")
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchProjectforLookup))
            {
                if (!IsFullRightsReservedUser)
                {
                    dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, this.LoginUserRoleId);
                }

                if (!string.IsNullOrEmpty(ProjectClosedDate))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, ProjectClosedDate);
                }

                //On 09/02/2022, load Projects based on start of the project (Current FY To)
                if (!string.IsNullOrEmpty(yearto) && DateSet.ToDate(yearto, false) != DateTime.MinValue)
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, yearto);
                }
                else if (!string.IsNullOrEmpty(YearTo) && DateSet.ToDate(YearTo, false) != DateTime.MinValue)
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, YearTo);
                }
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs IsVoucherTransactionExists()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.IsExistMapVoucherTransaction))
            {
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_IDColumn, VoucherId);
                dataManager.Parameters.Add(this.AppSchema.Voucher.PROJECT_IDColumn, ProjectId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        public ResultArgs FetchProjectsAnnualBudget()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchProjectforLookup))
            {
                if (!IsFullRightsReservedUser)
                {
                    dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, this.LoginUserRoleId);
                }
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchPJLookup()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchPJLookUp))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchProjectsGridView()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchProjectForGridView))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs LoadProjectMappingGrid()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.LoadProjectMappingGrid))
            {
                if (!IsFullRightsReservedUser)
                {
                    dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, this.LoginUserRoleId);
                }
                dataManager.Parameters.Add(this.AppSchema.Ledger.GROUP_IDColumn, LedgerGroupId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        //public ResultArgs FetchInvestedFdLedgers()
        //{
        //    using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchInvestedLedger))
        //    {
        //        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
        //        resultArgs = dataManager.FetchData(DataSource.DataTable);
        //    }
        //    return resultArgs;
        //}

        public ResultArgs LoadProjectDonorMappingGrid(int Id)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.LoadProjectDonorGrid))
            {
                if (!IsFullRightsReservedUser)
                {
                    dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, this.LoginUserRoleId);
                }
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.DONAUD_IDColumn, Id);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs LoadProjectCostCentreMappingGrid(int Id)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.LoadProjectCostCentreGrid))
            {
                if (!IsFullRightsReservedUser)
                {
                    dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, this.LoginUserRoleId);
                }
                dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, Id);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchMappedProjects()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchMappedProject))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs AccountMappingProject(DataTable dtBreakUpDetails, DataTable dtFDDetailsForBreakup, BankAccountSystem FDUpdation, bool IsFdLedger)
        {
            ResultArgs IsSuccess = null;
            if (IsFdLedger)
            {
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.BeginTransaction();
                    //Saving FD Details
                    IsSuccess = resultArgs = FDUpdation.UpdateFD(dataManager, dtFDDetailsForBreakup);
                    if (resultArgs.Success)
                    {
                        using (BreakUpSystem breakUpSystem = new BreakUpSystem())
                        {
                            //Saving Break Up details
                            resultArgs = breakUpSystem.UpdateBreakUpDetails(dtBreakUpDetails, dataManager, FDUpdation);
                        }
                        if (resultArgs != null)
                        {
                            if (resultArgs.Success)
                            {
                                resultArgs = DeleteMappedProject(dataManager, LedgerId);
                                if (resultArgs.Success)
                                    MapProject(dataManager, dtProjectIDCollection, LedgerId);
                            }
                        }
                        else
                            resultArgs = IsSuccess;
                    }
                    dataManager.EndTransaction();
                }
            }
            else
            {
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.BeginTransaction();
                    resultArgs = DeleteMappedProject(dataManager, LedgerId);
                    if (resultArgs.Success)
                        MapProject(dataManager, dtProjectIDCollection, LedgerId);
                    dataManager.EndTransaction();
                }
            }
            return resultArgs;
        }

        private ResultArgs MapProject(DataManager dataManagers, DataTable dtProjectId, int LedgerId)
        {
            if (dtProjectId != null && dtProjectId.Rows.Count > 0)
            {
                foreach (DataRow dr in dtProjectId.Rows)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectLedgerMappingAdd))
                    {
                        dataManager.Database = dataManagers.Database;
                        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName]);
                        resultArgs = dataManager.UpdateData();
                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            using (BalanceSystem balanceSystem = new BalanceSystem())
                            {
                                if (balanceSystem.HasBalance(NumberSet.ToInteger(dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString()), LedgerId))
                                    balanceSystem.UpdateOpBalance(OpeningBalanceDate, NumberSet.ToInteger(dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString()), LedgerId, this.NumberSet.ToDouble(dr[AMOUNT].ToString()), dr[TRANS_MODE].ToString(), TransactionAction.Cancel);
                                balanceSystem.UpdateOpBalance(OpeningBalanceDate, NumberSet.ToInteger(dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString()), LedgerId, this.NumberSet.ToDouble(dr[AMOUNT].ToString()), dr[TRANS_MODE].ToString(), TransactionAction.New);
                            }
                        }
                        if (!resultArgs.Success)
                            break;
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs DeleteMappedProject(DataManager dataManagers, int LedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.LedgerProjectMappingDelete))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        #endregion

        #region Ledger System
        public ResultArgs FetchLedgerDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.LoadAllLedgers))
            {
                // dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);

                //On 12/09/2024, To Skip default ledgers for multi currency or other than country
                resultArgs = EnforceSkipDefaultLedgers(resultArgs, AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgerFD()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.LoadLedgerFD))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs LoadAllLedgerByProjectId(int ProjectId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.LoadLedgerByProId))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);

                //On 12/09/2024, To Skip default ledgers for multi currency or other than country
                resultArgs = EnforceSkipDefaultLedgers(resultArgs, AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
            }
            return resultArgs;
        }

        public ResultArgs LoadLedgerByProjectIds(string ProjectIds)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.LoadLedgerByProjectIds))
            {
                if (!string.IsNullOrEmpty(ProjectIds))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDsColumn, ProjectIds);
                }
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs LoadCCLedgerByProjectId(Int32 Pid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.LoadCCLedgerByProjectId))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, Pid);
                /*if (Pid>0)
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, Pid);
                }*/
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs LoadAllLedgerBySubLedgerProjects(int ProjectId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.subLedgerLoadLedgerByProdId))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchMappedLedgers()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchOpBalanceList))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.Country.APPLICABLE_FROMColumn, this.FirstFYDateFrom.ToShortDateString());
                    dataManager.Parameters.Add(this.AppSchema.Country.APPLICABLE_TOColumn, this.FirstFYDateTo.ToShortDateString());

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    //On 12/09/2024, To Skip default ledgers for multi currency or other than country
                    resultArgs = EnforceSkipDefaultLedgers(resultArgs, AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                }
            }
            catch (Exception err)
            {
                resultArgs.Message = err.Message;
            }

            return resultArgs;
        }

        public ResultArgs FetchMappedLedgersGeneralate()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchOpBalanceListGeneralate))
            {
                //dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// 07.02.2020
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchExpenseMappedLedgerBudget()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchExpenseLedgerListBudget))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchMappedBudgetLedgers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchBudgetMappedLedgerList))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                //dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchMappedGeneralateLedgers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchGeneralateMappedLedgerList))
            {
                dataManager.Parameters.Add(this.AppSchema.GeneralateGroupLedger.CON_LEDGER_IDColumn, GeneralateId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        //public ResultArgs CheckLedgerMapped()
        //{
        //    using (DataManager dataManager = new DataManager(SQLCommand.Mapping.CheckLedgerMapped))
        //    {
        //        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
        //        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);

        //        resultArgs = dataManager.FetchData(DataSource.DataTable);
        //    }
        //    return resultArgs;
        //}

        //public ResultArgs CheckCostCentreMapped()
        //{
        //    using (DataManager dataManager = new DataManager(SQLCommand.Mapping.CheckCostCentreMapped))
        //    {
        //        dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, CostCenterId);
        //        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);

        //        resultArgs = dataManager.FetchData(DataSource.DataTable);
        //    }
        //    return resultArgs;
        //}

        public ResultArgs FetchMappedLedgersByLedgerId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchMappedLedgers))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs MapPurposeTransaction()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectPurposeMappingAdd))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Purposes.CONTRIBUTION_IDColumn, FCPurposeIDs);
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.AMOUNTColumn, "0.00");
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.TRANS_MODEColumn, "DR");

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchProjectIdByFDLedgerId(int FDId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchMappedFDByFDId))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, FDId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        public ResultArgs MappingLedgers()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                AccountMappingLedger(dataManager);
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        /// <summary>
        /// 04.01.2020
        /// </summary>
        /// <returns></returns>
        public ResultArgs MappingBudgetLedgers()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                AccountMappingBudgetLedger(dataManager);
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        //On 05/03/2020, To Map all recurring ledgers to all projects
        //For mysore 
        public ResultArgs MapBudgetRecLedgerForAll()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.MapBudgetRecLedgerForAllProjects))
            {
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs AccountMappingLedger(DataManager dataManagers)
        {
            //  ResultArgs IsSuccess = null;
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Database = dataManagers.Database;
                //if (IsFDLedger)
                //{
                //    //Saving FD Details
                //    using (BankAccountSystem FDUpdation = new BankAccountSystem())
                //    {
                //        IsSuccess = resultArgs = FDUpdation.UpdateFD(dataManager, dtMappingLedger);
                //        if (resultArgs.Success)
                //        {
                //            if (resultArgs != null)
                //            {
                //                if (resultArgs.Success)
                //                {
                //                    resultArgs = DeleteMapLedger(dataManager, ProjectId);
                //                    if (resultArgs.Success)
                //                        MapLedger(dataManager, dtLedgerIDCollection, ProjectId);
                //                }
                //            }
                //            else
                //                resultArgs = IsSuccess;
                //        }
                //    }
                //}
                //else
                //{
                resultArgs = DeleteMapLedger(dataManager, ProjectId);
                if (resultArgs.Success)
                {
                    // if (resultArgs.Success)
                    MapLedger(dataManager, dtLedgerIDCollection, ProjectId);

                    DeleteUnMappedProjectLedgersInProjectBudgetLedger(dataManager, ProjectId);
                }
                // }
            }
            return resultArgs;
        }

        /// <summary>
        /// 04.01.2020
        /// </summary>
        /// <param name="dataManagers"></param>
        /// <returns></returns>
        public ResultArgs AccountMappingBudgetLedger(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Database = dataManagers.Database;
                resultArgs = DeleteBudgetMapLedger(dataManager, ProjectId);
                if (resultArgs.Success)
                {
                    MapBudgetLedger(dataManager, dtLedgerIDCollection, ProjectId);
                }
            }
            return resultArgs;
        }
        public ResultArgs AccountMappingByLedgerId(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManagers.Database = dataManagers.Database;
                resultArgs = UnMapLedgerByLedgerId(dataManager);
                if (resultArgs.Success)
                    MapLedgerByLedger(dataManager);
            }
            return resultArgs;
        }

        public ResultArgs AccountMappingByFDId(DataTable dtBreakUpDetails, BankAccountSystem FDUpdation, int BankId)
        {
            ResultArgs IsSuccess = null;
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                if (dtBreakUpDetails != null)
                {
                    //Saving FD Details
                    IsSuccess = resultArgs = FDUpdation.UpdateFD(dataManager, dtBreakUpDetails, BankId);
                    if (resultArgs.Success)
                    {
                        if (resultArgs != null)
                        {
                            if (resultArgs.Success)
                            {
                                resultArgs = UnMapLedgerByLedgerId(dataManager);
                                if (resultArgs.Success)
                                    AccountMappingFDByFDId(dataManager, FDUpdation);
                            }
                        }
                        else
                            resultArgs = IsSuccess;
                    }
                }
                else
                {
                    resultArgs = UnMapLedgerByLedgerId(dataManager);
                    if (resultArgs.Success)
                        AccountMappingFDByFDId(dataManager, FDUpdation);
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public ResultArgs AccountMappingFDByFDId(DataManager dataManagers, BankAccountSystem FDUpdation)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectLedgerMappingAdd))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, FDUpdation.ProjectId);
                resultArgs = dataManager.UpdateData();
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    using (BalanceSystem balanceSystem = new BalanceSystem())
                    {
                        if (balanceSystem.HasBalance(FDUpdation.ProjectId, LedgerId))
                            balanceSystem.UpdateOpBalance(OpeningBalanceDate, FDUpdation.ProjectId, LedgerId, NumberSet.ToDouble(FDUpdation.Amount.ToString()), FDUpdation.TransMode, TransactionAction.Cancel);
                        balanceSystem.UpdateOpBalance(OpeningBalanceDate, FDUpdation.ProjectId, LedgerId, NumberSet.ToDouble(FDUpdation.Amount.ToString()), FDUpdation.TransMode, TransactionAction.New);
                    }
                }
            }
            return resultArgs;
        }

        //Added By salamon 
        // To Map Ledgers while Moving transaction to the other project if not mapped ( ON UPDATE DUPLICATE KEY)
        public ResultArgs MapProjectLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.MapProjectLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.ProjectVoucher.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        // To Map Ledgers while Moving transaction to the other project if not mapped ( ON UPDATE DUPLICATE KEY)
        public ResultArgs MapProjectLedger(DataManager dataBaseManager)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.MapProjectLedger))
            {
                dataManager.Database = dataBaseManager.Database;
                dataManager.Parameters.Add(this.AppSchema.ProjectVoucher.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        // To Map Ledgers while Moving transaction to the other project if not mapped ( ON UPDATE DUPLICATE KEY)
        public ResultArgs MapProjectLedger(DataManager dataBaseManager, Int32 fromprojectid, Int32 fromledgerid,
            Int32 toprojectid, Int32 toledgerid, string datefrom, string dateto)
        {
            bool isduration = (!string.IsNullOrEmpty(datefrom) && !string.IsNullOrEmpty(dateto));
            SQLCommand.Mapping sqlcmd = SQLCommand.Mapping.MapProjectAgainstLedgerByCashBankLedger;
            if (isduration)
            {
                sqlcmd = SQLCommand.Mapping.MapProjectAgainstLedgerByCashBankLedgerForPeriod;
            }

            using (DataManager dataManager = new DataManager(sqlcmd))
            {
                dataManager.Database = dataBaseManager.Database;
                dataManager.Parameters.Add(AppSchema.Project.FROM_PROJECT_IDColumn, fromprojectid);
                dataManager.Parameters.Add(AppSchema.Project.TO_PROJECT_IDColumn, toprojectid);
                dataManager.Parameters.Add(AppSchema.Ledger.FROM_LEDGER_IDColumn, fromledgerid);
                dataManager.Parameters.Add(AppSchema.Ledger.TO_LEDGER_IDColumn, toledgerid);
                if (!String.IsNullOrEmpty(datefrom))
                {
                    dataManager.Parameters.Add(AppSchema.FDRegisters.DATE_FROMColumn, datefrom);
                    dataManager.Parameters.Add(AppSchema.FDRegisters.DATE_TOColumn, dateto);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        //Added By salamon 
        // To Map Cost Centers while Moving transaction to the other project if not mapped ( ON UPDATE DUPLICATE KEY)
        public ResultArgs MapProjectCostCenter()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.MapCostCentreToProject))
            {
                dataManager.Parameters.Add(this.AppSchema.ProjectVoucher.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, CostCenterId);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }


        private ResultArgs MapLedger(DataManager dataManagers, DataTable LedgerIds, int ProjectId)
        {
            if (LedgerIds != null && LedgerIds.Rows.Count > 0)
            {
                foreach (DataRow dr in LedgerIds.Rows)
                {
                    using (BalanceSystem balanceSystem = new BalanceSystem())
                    {
                        if (balanceSystem.HasBalance(ProjectId, this.NumberSet.ToInteger(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString())))
                            balanceSystem.UpdateOpBalance(OpeningBalanceDate, ProjectId, this.NumberSet.ToInteger(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()), this.NumberSet.ToDouble(dr[AMOUNT].ToString()), dr[TRANS_MODE].ToString(), TransactionAction.EditBeforeSave);
                    }
                    using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectLedgerMappingAdd))
                    {

                        dataManager.Database = dataManagers.Database;
                        dataManager.Parameters.Add(this.AppSchema.ProjectVoucher.PROJECT_IDColumn, ProjectId);
                        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, this.NumberSet.ToInteger(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()));
                        resultArgs = dataManager.UpdateData();
                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            //22/11/2022, To update Ledger OP Amount to CC
                            if (CostCeterMapping == 1 && dtLedgerCCDistribution != null && dtLedgerCCDistribution.Rows.Count > 0)
                            {
                                resultArgs = UpdateLedgerCostCentreDistributionAmount(ProjectId, this.NumberSet.ToInteger(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()), dataManager);
                            }

                            if (resultArgs.Success && dtProjectLedgerApplicableDetails != null && dtProjectLedgerApplicableDetails.Rows.Count > 0)
                            {
                                resultArgs = UpdateProjectLedgerApplicable(MapForm.Ledger, ProjectId, this.NumberSet.ToInteger(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()), dataManager);
                            }

                            if (resultArgs.Success)
                            {
                                using (BalanceSystem balanceSystem = new BalanceSystem())
                                {
                                    if (balanceSystem.HasBalance(ProjectId, this.NumberSet.ToInteger(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString())))
                                        balanceSystem.UpdateOpBalance(OpeningBalanceDate, ProjectId, this.NumberSet.ToInteger(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()), this.NumberSet.ToDouble(dr[AMOUNT].ToString()), dr[TRANS_MODE].ToString(), TransactionAction.EditAfterSave);
                                    else if (this.NumberSet.ToDouble(dr[AMOUNT].ToString()) > 0)
                                    {
                                        balanceSystem.UpdateOpBalance(OpeningBalanceDate, ProjectId, this.NumberSet.ToInteger(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()), this.NumberSet.ToDouble(dr[AMOUNT].ToString()), dr[TRANS_MODE].ToString(), TransactionAction.New);
                                    }
                                }
                            }
                        }
                        if (!resultArgs.Success)
                            break;
                    }
                }
            }
            if (dtMovedLedgerIDCollection != null && dtMovedLedgerIDCollection.Rows.Count > 0)
            {
                CancelBalance();
                foreach (DataRow dr in dtMovedLedgerIDCollection.Rows)
                {
                    using (BalanceSystem balanceSystem = new BalanceSystem())
                    {
                        resultArgs = balanceSystem.DeleteBalance(ProjectId, this.NumberSet.ToInteger(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()));
                    }
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// Update distributed Ledger opening balance to costcentre
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="PurposeId"></param>
        /// <returns></returns>
        private ResultArgs UpdateLedgerCostCentreDistributionAmount(int PId, int LId, DataManager dm)
        {
            try
            {
                if (dtLedgerCCDistribution != null && dtLedgerCCDistribution.Rows.Count > 0 && PId > 0 && LId > 0)
                {
                    dtLedgerCCDistribution.DefaultView.RowFilter = AppSchema.Ledger.LEDGER_IDColumn.ColumnName + " = " + LId + " AND COST_CENTRE_ID>0 AND AMOUNT>0";
                    DataTable dtCCDistribution = dtLedgerCCDistribution.DefaultView.ToTable();
                    dtLedgerCCDistribution.DefaultView.RowFilter = string.Empty;

                    using (DataManager dataManager = new DataManager(SQLCommand.Mapping.UpdateProjectLedgerCCDistributionZero))
                    {
                        dataManager.Database = dm.Database;
                        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, PId);
                        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LId);
                        resultArgs = dataManager.UpdateData();
                    }

                    if (resultArgs.Success)
                    {
                        foreach (DataRow dr in dtCCDistribution.Rows)
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.UpdateProjectLedgerCCDistribution))
                            {
                                dataManager.Database = dm.Database;
                                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, PId);
                                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LId);
                                dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, dr[this.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName]);
                                dataManager.Parameters.Add(this.AppSchema.FDRegisters.AMOUNTColumn, dr[this.AppSchema.FDRegisters.AMOUNTColumn.ColumnName]);
                                dataManager.Parameters.Add(this.AppSchema.FDRegisters.TRANS_MODEColumn, dr[this.AppSchema.FDRegisters.TRANS_MODEColumn.ColumnName].ToString().ToUpper());
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
            catch (Exception err)
            {
                resultArgs.Message = err.Message;
                dtPurposeCCDistribution.DefaultView.RowFilter = string.Empty;
            }

            return resultArgs;
        }

        public ResultArgs DeleteProjectLedgerApplicableByLedger(int LId, DataManager dm)
        {
            try
            {
                DeleteProjectLedgerApplicable(0, LId, dm);
            }
            catch (Exception err)
            {
                resultArgs.Message = err.Message;
                dtPurposeCCDistribution.DefaultView.RowFilter = string.Empty;
            }

            return resultArgs;
        }


        public ResultArgs DeleteProjectLedgerApplicableByProject(int PId, DataManager dm)
        {
            try
            {
                DeleteProjectLedgerApplicable(PId, 0, dm);
            }
            catch (Exception err)
            {
                resultArgs.Message = err.Message;
                dtPurposeCCDistribution.DefaultView.RowFilter = string.Empty;
            }

            return resultArgs;
        }

        private ResultArgs DeleteProjectLedgerApplicable(int PId, int LId, DataManager dm = null)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Mapping.DeleteProjectLedgerApplicable))
                {
                    if (dm != null) dataManager.Database = dm.Database;
                    if (PId > 0)
                    {
                        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, PId);
                    }

                    if (LId > 0)
                    {
                        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LId);
                    }
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception err)
            {
                resultArgs.Message = err.Message;
                dtPurposeCCDistribution.DefaultView.RowFilter = string.Empty;
            }

            return resultArgs;
        }
        /// <summary>
        /// Update Project Ledger's Applicable details
        /// </summary>
        /// <param name="PId"></param>
        /// <param name="LId"></param>
        /// <param name="dm"></param>
        /// <returns></returns>
        private ResultArgs UpdateProjectLedgerApplicable(MapForm typeofMap, int PId, int LId, DataManager dm)
        {
            try
            {
                if (dtProjectLedgerApplicableDetails != null && dtProjectLedgerApplicableDetails.Rows.Count > 0 && PId > 0 && LId > 0)
                {
                    if (typeofMap == MapForm.Project)
                        dtProjectLedgerApplicableDetails.DefaultView.RowFilter = AppSchema.Project.PROJECT_IDColumn.ColumnName + " = " + PId;
                    else
                        dtProjectLedgerApplicableDetails.DefaultView.RowFilter = AppSchema.Ledger.LEDGER_IDColumn.ColumnName + " = " + LId;

                    DataTable dtledgerapplicableDetails = dtProjectLedgerApplicableDetails.DefaultView.ToTable();
                    dtProjectLedgerApplicableDetails.DefaultView.RowFilter = string.Empty;

                    resultArgs = DeleteProjectLedgerApplicable(PId, LId, dm);

                    if (resultArgs.Success)
                    {
                        foreach (DataRow dr in dtledgerapplicableDetails.Rows)
                        {
                            DateTime deApplicableTo = string.IsNullOrEmpty(dr[this.AppSchema.Ledger.APPLICABLE_TOColumn.ColumnName].ToString()) ? DateTime.MinValue :
                            DateSet.ToDate(dr[this.AppSchema.Ledger.APPLICABLE_TOColumn.ColumnName].ToString(), false);

                            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.UpdateProjectLedgerApplicableByProject))
                            {
                                dataManager.Database = dm.Database;
                                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, PId);
                                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LId);
                                dataManager.Parameters.Add(this.AppSchema.Ledger.APPLICABLE_FROMColumn, dr[this.AppSchema.Ledger.APPLICABLE_FROMColumn.ColumnName]);

                                if (deApplicableTo == DateTime.MinValue)
                                {
                                    dataManager.Parameters.Add(this.AppSchema.Ledger.APPLICABLE_TOColumn, null);
                                }
                                else
                                {
                                    dataManager.Parameters.Add(this.AppSchema.Ledger.APPLICABLE_TOColumn, deApplicableTo);
                                }
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
            catch (Exception err)
            {
                resultArgs.Message = err.Message;
                dtPurposeCCDistribution.DefaultView.RowFilter = string.Empty;
            }

            return resultArgs;
        }

        /// <summary>
        /// 04.01.2020
        /// </summary>
        /// <returns></returns>
        public ResultArgs MapBudgetActualLedgers()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                if (dtBudgetActualLedger != null && dtBudgetActualLedger.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtBudgetActualLedger.Rows)
                    {
                        resultArgs = CheckProjectLedgersExists(dataManager, ProjectId, this.NumberSet.ToInteger(dr["LEDGER_ID"].ToString()));
                        if (resultArgs.Success && resultArgs.DataSource.Sclar.ToInteger == 0)
                        {
                            int Projectid = ProjectId;
                            int LedgerId = this.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                            double amount = this.NumberSet.ToDouble("0.00");
                            string tmode = "DR";
                            DuplicateMapBudgetLedger(dataManager, LedgerId, ProjectId, amount, tmode);
                        }
                    }
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public ResultArgs FetchActualBalanceforBudget()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchBudgetActualLedgers))
            {
                dataManager.Parameters.Add(AppSchema.FDRegisters.DATE_FROMColumn, this.DateSet.ToDate(YearFrom, true).AddYears(-1));
                dataManager.Parameters.Add(AppSchema.FDRegisters.DATE_TOColumn, this.DateSet.ToDate(YearTo, true).AddYears(-1));
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchProjectList()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchProjectList))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 04.01.2020
        /// </summary>
        /// <param name="dataManagers"></param>
        /// <param name="LedgerIds"></param>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        private ResultArgs MapBudgetLedger(DataManager dataManagers, DataTable LedgerIds, int ProjectId)
        {
            if (LedgerIds != null && LedgerIds.Rows.Count > 0)
            {
                foreach (DataRow dr in LedgerIds.Rows)
                {
                    using (BalanceSystem balanceSystem = new BalanceSystem())
                    {
                        if (balanceSystem.HasBalance(ProjectId, this.NumberSet.ToInteger(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString())))
                            balanceSystem.UpdateOpBalance(OpeningBalanceDate, ProjectId, this.NumberSet.ToInteger(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()), this.NumberSet.ToDouble(dr[AMOUNT].ToString()), dr[TRANS_MODE].ToString(), TransactionAction.EditBeforeSave);
                    }
                    using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectBudgetLedgerMappingAdd))
                    {
                        dataManager.Database = dataManagers.Database;
                        dataManager.Parameters.Add(this.AppSchema.ProjectVoucher.PROJECT_IDColumn, ProjectId);
                        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, this.NumberSet.ToInteger(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()));
                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        resultArgs = dataManager.UpdateData();
                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            using (BalanceSystem balanceSystem = new BalanceSystem())
                            {
                                if (balanceSystem.HasBalance(ProjectId, this.NumberSet.ToInteger(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString())))
                                    balanceSystem.UpdateOpBalance(OpeningBalanceDate, ProjectId, this.NumberSet.ToInteger(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()), this.NumberSet.ToDouble(dr[AMOUNT].ToString()), dr[TRANS_MODE].ToString(), TransactionAction.EditAfterSave);
                                else if (this.NumberSet.ToDouble(dr[AMOUNT].ToString()) > 0)
                                {
                                    balanceSystem.UpdateOpBalance(OpeningBalanceDate, ProjectId, this.NumberSet.ToInteger(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()), this.NumberSet.ToDouble(dr[AMOUNT].ToString()), dr[TRANS_MODE].ToString(), TransactionAction.New);
                                }
                            }
                        }
                        if (!resultArgs.Success)
                            break;
                    }
                }
            }
            if (dtMovedLedgerIDCollection != null && dtMovedLedgerIDCollection.Rows.Count > 0)
            {
                CancelBalance();
                foreach (DataRow dr in dtMovedLedgerIDCollection.Rows)
                {
                    using (BalanceSystem balanceSystem = new BalanceSystem())
                    {
                        resultArgs = balanceSystem.DeleteBalance(ProjectId, this.NumberSet.ToInteger(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()));
                    }
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// On 04.01.2020
        /// </summary>
        /// <param name="dataManagers"></param>
        /// <param name="LedgerIds"></param>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        private ResultArgs DuplicateMapBudgetLedger(DataManager dataManagers, int LedgerId, int ProjectId, double amt, string tmode)
        {
            //using (BalanceSystem balanceSystem = new BalanceSystem())
            //{
            //    if (balanceSystem.HasBalance(ProjectId, ProjectId))
            //        balanceSystem.UpdateOpBalance(OpeningBalanceDate, ProjectId, LedgerId, amt, tmode, TransactionAction.EditBeforeSave);
            //}
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectBudgetLedgerMappingAdd))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.ProjectVoucher.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
                //if (resultArgs.Success && resultArgs.RowsAffected > 0)
                //{
                //    using (BalanceSystem balanceSystem = new BalanceSystem())
                //    {
                //        if (balanceSystem.HasBalance(ProjectId, LedgerId))
                //            balanceSystem.UpdateOpBalance(OpeningBalanceDate, ProjectId, LedgerId, amt, tmode, TransactionAction.EditAfterSave);
                //        else if (amt > 0)
                //        {
                //            balanceSystem.UpdateOpBalance(OpeningBalanceDate, ProjectId, LedgerId, amt, tmode, TransactionAction.New);
                //        }
                //    }
                //}
            }
            return resultArgs;
        }


        private void CancelBalance()
        {
            foreach (DataRow dr in dtMovedLedgerIDCollection.Rows)
            {
                using (BalanceSystem balanceSystem = new BalanceSystem())
                {
                    balanceSystem.UpdateOpBalance(OpeningBalanceDate, ProjectId, this.NumberSet.ToInteger(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()), this.NumberSet.ToDouble(dr[AMOUNT].ToString()), dr[TRANS_MODE].ToString(), TransactionAction.New);
                }
            }
        }

        private ResultArgs DeleteMapLedger(DataManager dataManagers, int ProjectId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectLedgerMappingDelete))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();

                //On 22/09/2023, to clear ledger applicable details for concern project
                if (resultArgs.Success)
                {
                    resultArgs = DeleteProjectLedgerApplicableByProject(ProjectId, dataManager);
                }
            }


            return resultArgs;
        }

        /// <summary>
        /// 04.01.2020
        /// </summary>
        /// <param name="dataManagers"></param>
        /// <returns></returns>
        private ResultArgs DeleteBudgetMapLedger(DataManager dataManagers, int ProjectId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectBudgetLedgerMappingDelete))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// 04.01.2020
        /// </summary>
        /// <param name="dataManagers"></param>
        /// <returns></returns>
        private ResultArgs CheckProjectLedgersExists(DataManager dataManagers, int ProId, int LedId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectBudgetLedgerByBudgetLedgerProject))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 07/03/2024, Delete mapped budget ledgers which are not available in Project ledger and budget ledgers
        /// </summary>
        /// <returns></returns>
        public ResultArgs DeleteUnMappedProjectLedgersInProjectBudgetLedger(DataManager dataManagers, int ProjectId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.DeleteUnMappedProjectLedgersInProjectBudgetLedger))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// On 19/10/2020, delete selected ledgers and mapped ledgers -Budgets
        /// </summary>
        /// <param name="dataManagers"></param>
        /// <param name="ledgers"></param>
        /// <returns></returns>
        private ResultArgs DeleteBudgetLedgerAmountByLedger(DataManager dataManagers, string ledgers)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.DeleteBudgetLedgerAmountByLedger))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.Ledger.ID_COLLECTIONSColumn, ledgers);

                //On 11/03/2024, To attach Project based merge
                if (ProjectId > 0)
                {
                    dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// On 19/10/2020, delete selected ledgers and mapped ledgers -Budgets
        /// </summary>
        /// <param name="dataManagers"></param>
        /// <param name="ledgers"></param>
        /// <returns></returns>
        private ResultArgs DeleteMappedBudgetByLedger(DataManager dataManagers, string ledgers)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.DeleteMappedBudgetByLedger))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.Ledger.ID_COLLECTIONSColumn, ledgers);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }


        private ResultArgs MapLedgerByLedgerId(DataManager dataManagers)
        {
            if (dtMappingLedger != null && dtMappingLedger.Rows.Count > 0)
            {

                foreach (DataRow dr in dtMappingLedger.Rows)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectLedgerMappingAdd))
                    {
                        dataManager.Database = dataManagers.Database;
                        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName]);
                        resultArgs = dataManager.UpdateData();
                        if (FDTransType != LedgerTypes.FD.ToString())
                        {
                            if (resultArgs.Success && resultArgs.RowsAffected > 0)
                            {
                                using (BalanceSystem balanceSystem = new BalanceSystem())
                                {
                                    if (balanceSystem.HasBalance(NumberSet.ToInteger(dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString()), LedgerId))
                                        balanceSystem.UpdateOpBalance(OpeningBalanceDate, NumberSet.ToInteger(dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString()), LedgerId, this.NumberSet.ToDouble(dr[AMOUNT].ToString()), dr[TRANS_MODE].ToString(), TransactionAction.Cancel);
                                    balanceSystem.UpdateOpBalance(OpeningBalanceDate, NumberSet.ToInteger(dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString()), LedgerId, this.NumberSet.ToDouble(dr[AMOUNT].ToString()), dr[TRANS_MODE].ToString(), TransactionAction.New);
                                }
                            }
                        }
                        if (!resultArgs.Success)
                            break;
                    }
                }
            }
            if (dtMovedLedgerIDCollection != null && dtMovedLedgerIDCollection.Rows.Count > 0)
            {
                CancelBalance();
                foreach (DataRow dr in dtMovedLedgerIDCollection.Rows)
                {
                    using (BalanceSystem balanceSystem = new BalanceSystem())
                    {
                        resultArgs = balanceSystem.DeleteBalance(NumberSet.ToInteger(dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString()), LedgerId);
                    }
                }
            }

            return resultArgs;
        }

        private ResultArgs MapLedgerByLedger(DataManager dataManagers)
        {
            DataTable dtLedgerProjectMapping = new DataTable();
            dtLedgerProjectMapping = dtMappingLedger;
            if (dtMappingLedger != null && dtMappingLedger.Rows.Count > 0)
            {
                DataView dvMapLedger = dtMappingLedger.DefaultView;
                dvMapLedger.RowFilter = "SELECT=1";

                foreach (DataRow dr in dvMapLedger.ToTable().Rows)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectLedgerMappingAdd))
                    {
                        dataManager.Database = dataManagers.Database;
                        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName]);
                        resultArgs = dataManager.UpdateData();

                        //On 23/09/2023, To update Project Ledger's applicable details
                        if (resultArgs.Success && dtProjectLedgerApplicableDetails != null && dtProjectLedgerApplicableDetails.Rows.Count > 0)
                        {
                            resultArgs = UpdateProjectLedgerApplicable(MapForm.Project, this.NumberSet.ToInteger(dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString()), LedgerId, dataManager);
                        }

                        if (!resultArgs.Success)
                            break;
                    }
                }

                if (resultArgs != null && resultArgs.Success && FDTransType != LedgerTypes.FD.ToString())
                {
                    foreach (DataRow dr in dtLedgerProjectMapping.Rows)
                    {
                        using (BalanceSystem balanceSystem = new BalanceSystem())
                        {
                            if (balanceSystem.HasBalance(NumberSet.ToInteger(dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString()), LedgerId))
                                resultArgs = balanceSystem.UpdateOpBalance(OpeningBalanceDate, NumberSet.ToInteger(dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString()), LedgerId, this.NumberSet.ToDouble(dr[AMOUNT].ToString()), dr[TRANS_MODE].ToString(), TransactionAction.Cancel);
                            resultArgs = balanceSystem.UpdateOpBalance(OpeningBalanceDate, NumberSet.ToInteger(dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString()), LedgerId, this.NumberSet.ToDouble(dr[AMOUNT].ToString()), dr[TRANS_MODE].ToString(), TransactionAction.New);

                            if (!resultArgs.Success)
                                break;
                        }
                    }
                }
            }
            if (dtMovedLedgerIDCollection != null && dtMovedLedgerIDCollection.Rows.Count > 0)
            {
                CancelBalance();
                foreach (DataRow dr in dtMovedLedgerIDCollection.Rows)
                {
                    using (BalanceSystem balanceSystem = new BalanceSystem())
                    {
                        resultArgs = balanceSystem.DeleteBalance(NumberSet.ToInteger(dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString()), LedgerId);
                    }
                }
            }

            return resultArgs;
        }

        private ResultArgs UnMapLedgerByLedgerId(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.UnMapProjectLedger))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.UpdateData();
            }

            //On 23/09/2023, to clear ledger applicable details for concern ledger
            if (resultArgs.Success)
            {
                resultArgs = DeleteProjectLedgerApplicableByLedger(LedgerId, dataManagers);
            }
            return resultArgs;
        }


        public ResultArgs MergeMigratedLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.MapMigratedLedgers))
            {
                dataManager.BeginTransaction();
                resultArgs = DeleteMergedProjectLedgers(dataManager);
                if (resultArgs.Success)
                {
                    dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_ID_COLLECTIONColumn, LedgerIdCollection);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.MAP_LEDGER_IDColumn, MapLedgerId);
                    resultArgs = dataManager.UpdateData();
                    if (resultArgs.Success)
                    {
                        resultArgs = DeleteCategoryLedgers(dataManager);
                        if (resultArgs.Success)
                        {
                            resultArgs = DeleteMergedLedgers(dataManager);
                        }
                    }
                    dataManager.EndTransaction();
                }
            }
            return resultArgs;
        }

        private ResultArgs MergeAllLedgers(DataManager dataManagers)
        {
            DataTable dtMappedLedger = dtMergeLedgers.AsEnumerable().GroupBy(r => r.Field<String>("MAPPED_LEDGER_ID")).Select(g => g.First()).CopyToDataTable();
            foreach (DataRow dr in dtMappedLedger.Rows)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Mapping.MapMigratedLedgers))
                {
                    MapLedgerId = 0;
                    MapLedgerId = NumberSet.ToInteger(dr["MAPPED_LEDGER_ID"].ToString());

                    LedgerNatureTransMode = dr["TRANS_MODE"].ToString();
                    DataView dvFilter = new DataView(dtMergeLedgers);
                    dvFilter.RowFilter = String.Format("MAPPED_LEDGER_ID={0} AND LEDGER_ID<>{1}", NumberSet.ToInteger(dr["MAPPED_LEDGER_ID"].ToString()), NumberSet.ToInteger(dr["MAPPED_LEDGER_ID"].ToString()));
                    LedgerIdCollection = string.Empty;
                    int CostcentreCollection = 0;
                    int TDSCollection = 0;
                    foreach (DataRow drLedgerId in dvFilter.ToTable().Rows)
                    {
                        LedgerIdCollection += drLedgerId[AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString() + ',';
                        CostcentreCollection += NumberSet.ToInteger(drLedgerId[AppSchema.Ledger.IS_COST_CENTERColumn.ColumnName].ToString());
                        TDSCollection += NumberSet.ToInteger(drLedgerId[AppSchema.Ledger.IS_TDS_LEDGERColumn.ColumnName].ToString());

                    }
                    LedgerIdCollection = LedgerIdCollection.TrimEnd(',');

                    resultArgs = DeleteMergedProjectLedgers(dataManager);
                    if (resultArgs.Success)
                    {
                        dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_ID_COLLECTIONColumn, LedgerIdCollection);
                        dataManager.Parameters.Add(this.AppSchema.Ledger.MAP_LEDGER_IDColumn, MapLedgerId);
                        dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.COST_CENTRE_TABLEColumn, MapLedgerId);
                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        resultArgs = dataManager.UpdateData();

                        if (resultArgs.Success)
                        {
                            resultArgs = UpdateMergedLedgerBalance(dataManager);
                            if (resultArgs.Success)
                            {
                                resultArgs = DeleteCategoryLedgers(dataManager);
                                if (resultArgs.Success)
                                {
                                    resultArgs = DeleteMergedLedgers(dataManager);
                                }
                                if (resultArgs.Success)
                                {
                                    if (CostcentreCollection > 0)
                                    {
                                        resultArgs = MapLedgerToCostCentre(dataManager);
                                    }
                                }
                                if (resultArgs.Success)
                                {
                                    if (TDSCollection > 0)
                                    {
                                        resultArgs = MapLedgerToTDS(dataManager);
                                    }

                                }
                            }
                        }
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs MergeLedgers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.MapMigratedLedgers))
            {
                dataManager.BeginTransaction();
                MergeAllLedgers(dataManager);
                dataManager.EndTransaction();

            }
            return resultArgs;
        }


        private ResultArgs MergeAllLedgersNew(DataManager dataManagers)
        {
            DataTable dtMappedLedger = dtMergeLedgers.AsEnumerable().GroupBy(r => r.Field<Int64>("MERGE_LEDGER_ID")).Select(g => g.First()).CopyToDataTable();
            foreach (DataRow dr in dtMappedLedger.Rows)
            {
                using (DataManager dataManager = new DataManager(ProjectId == 0 ? SQLCommand.Mapping.MapMigratedLedgers : SQLCommand.Mapping.MapMigratedLedgersByProject))
                {
                    MapLedgerId = 0;
                    MapLedgerId = NumberSet.ToInteger(dr["MERGE_LEDGER_ID"].ToString());

                    LedgerNatureTransMode = dr["TRANS_MODE"].ToString();
                    DataView dvFilter = new DataView(dtMergeLedgers);
                    dvFilter.RowFilter = String.Format("MERGE_LEDGER_ID={0} AND LEDGER_ID<>{1}", NumberSet.ToInteger(dr["MERGE_LEDGER_ID"].ToString()), NumberSet.ToInteger(dr["MERGE_LEDGER_ID"].ToString()));
                    LedgerIdCollection = string.Empty;
                    int CostcentreCollection = 0;
                    int TDSCollection = 0;
                    foreach (DataRow drLedgerId in dvFilter.ToTable().Rows)
                    {
                        LedgerIdCollection += drLedgerId[AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString() + ',';
                        CostcentreCollection += NumberSet.ToInteger(drLedgerId[AppSchema.Ledger.IS_COST_CENTERColumn.ColumnName].ToString());
                        TDSCollection += NumberSet.ToInteger(drLedgerId[AppSchema.Ledger.IS_TDS_LEDGERColumn.ColumnName].ToString());
                    }
                    LedgerIdCollection = LedgerIdCollection.TrimEnd(',');

                    resultArgs = DeleteMergedProjectLedgers(dataManager);
                    if (resultArgs.Success)
                    {
                        //On 26/08/2021, to update modified details
                        if (ProjectId > 0)
                            resultArgs = UpdateVoucherModifiedDetails(LedgerIdCollection, ProjectId.ToString(), dataManager);
                        else
                            resultArgs = UpdateVoucherModifiedDetailsByLedgerIds(LedgerIdCollection, dataManager);

                        dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_ID_COLLECTIONColumn, LedgerIdCollection);
                        dataManager.Parameters.Add(this.AppSchema.Ledger.MAP_LEDGER_IDColumn, MapLedgerId);
                        dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.COST_CENTRE_TABLEColumn, MapLedgerId);
                        //On 11/03/2024, To attach Project based merge
                        if (ProjectId > 0)
                        {
                            dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                        }
                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        resultArgs = dataManager.UpdateData();

                        if (resultArgs.Success)
                        {
                            //On 19/10/2020, to check and update budget enabled vouchers
                            if (resultArgs.Success)
                            {
                                resultArgs = MergeBudgetedLedgers(dataManager);
                            }

                            if (resultArgs.Success)
                            {
                                resultArgs = UpdateMergedLedgerBalance(dataManager);

                                if (resultArgs.Success)
                                {
                                    resultArgs = DeleteCategoryLedgers(dataManager);
                                }

                                if (resultArgs.Success)
                                {
                                    resultArgs = DeleteMergedLedgers(dataManager);
                                }
                                if (resultArgs.Success)
                                {
                                    if (CostcentreCollection > 0)
                                    {
                                        resultArgs = MapLedgerToCostCentre(dataManager);
                                    }
                                }
                                if (resultArgs.Success)
                                {
                                    if (TDSCollection > 0)
                                    {
                                        resultArgs = MapLedgerToTDS(dataManager);
                                    }

                                }
                            }
                        }
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs MergeLedgersNew()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.MapMigratedLedgers))
            {
                dataManager.BeginTransaction();
                MergeAllLedgersNew(dataManager);
                dataManager.EndTransaction();
            }
            return resultArgs;
        }


        /// <summary>
        /// On 05/02/2021
        /// Move/Merge selected Cash/Bank Ledger's Vouchers which includes FD Vouchers from one Project to another Project
        /// 1.Check Both Cash Ledgers are in the same Ledger Group
        ///   A. If Duriation is selected, If FD vouchers is available during this period, alert message
        ///   B. If Duriation is enabled, We can't move FD Vouchers its renewals and withdrwas.
        ///   C. Check Vouchers are availalbe
        /// 2.If Duriation is enabled, We can't move FD Vouchers its renewals and withdrwas, so we move only generl Vouchers
        /// 3. Map Source Cash/Bank Ledger with Merge Project
        /// 4. Move Ledger Opening Balance Too, If Merge is going to be done from the begining
        /// </summary>
        /// <returns></returns>
        public ResultArgs MergeCashBankLedgersByProject(Int32 SourceProjectId, Int32 SourceCashBankLedgerId,
                                    Int32 MergeProjectId, Int32 MergeCashBankLedgerId, string DateFrom, string DateTo, bool bincludeLedgerOpeningBalance)
        {
            resultArgs = new ResultArgs();
            bool IsDuriation = (!string.IsNullOrEmpty(DateFrom) && !string.IsNullOrEmpty(DateTo));
            Int32 SourceBankLedgerBankId = GetBankId(SourceCashBankLedgerId);
            Int32 SourceLedgerGroupId = FetchLedgerGroup(SourceCashBankLedgerId);
            Int32 MergeLedgerGroupId = 0;

            if (MergeCashBankLedgerId > 0)
            {
                MergeLedgerGroupId = FetchLedgerGroup(MergeCashBankLedgerId);
            }
            else
            {
                MergeLedgerGroupId = SourceLedgerGroupId;
                MergeCashBankLedgerId = SourceCashBankLedgerId;
            }

            //1. Check Both Cash Ledgers are in the same Ledger Group
            if (SourceLedgerGroupId == 0)
            {
                resultArgs.Message = "Invalid Ledger Group, Unable to Merge Cash/Bank Ledgers";
            }
            else if (SourceLedgerGroupId != MergeLedgerGroupId)
            {
                resultArgs.Message = "Both Ledgers should be with in same the Ledger Group";
            }
            else if (!string.IsNullOrEmpty(DateFrom) && !string.IsNullOrEmpty(DateTo) && IsDuriation) //&& SourceLedgerGroupId== (Int32)FixedLedgerGroup.BankAccounts
            {
                //1. A If Duriation is selected, If FD vouchers is available during this period, alert message
                using (FDAccountSystem fdsystem = new FDAccountSystem())
                {
                    //1.B If Duriation is enabled, We can't move FD Vouchers its renewals and withdrwas.
                    if (fdsystem.IsFDVouchersExists(SourceProjectId, SourceCashBankLedgerId, DateFrom, DateTo))
                    {
                        resultArgs.Message = "FD Vouchers are available for given duration";
                    }
                }
            }

            if (string.IsNullOrEmpty(resultArgs.Message))
            {
                //1.C Check Vouchers are availalbe
                string lastdate = string.Empty;
                using (VoucherTransactionSystem vouchertranssystem = new VoucherTransactionSystem())
                {
                    lastdate = vouchertranssystem.FetchLastVoucherDate(SourceProjectId, DateFrom, DateTo);
                }

                if (String.IsNullOrEmpty(lastdate))
                {
                    resultArgs.Message = "Vouchers are not available";
                    resultArgs.Success = false;
                }
                else
                {
                    resultArgs.Message = "";
                    resultArgs.Success = true;
                }
            }

            if (resultArgs.Success)
            {
                //2. If Duriation is enabled, We can't move FD Vouchers its renewals and withdrwas, so we move only generl Vouchers
                SQLCommand.Mapping sqlcmd = SQLCommand.Mapping.MergeCashBankLedgersFromBeginning;
                if (IsDuriation)
                {
                    sqlcmd = SQLCommand.Mapping.MergeCashBankLedgersForPeriod;
                }
                using (DataManager dataManager = new DataManager(sqlcmd))
                {
                    dataManager.BeginTransaction();
                    //3. Map Source Cash/Bank Ledger with Merge Project
                    this.ProjectId = MergeProjectId;
                    this.LedgerId = (MergeCashBankLedgerId > 0 ? MergeCashBankLedgerId : SourceCashBankLedgerId);
                    resultArgs = MapProjectLedger(dataManager, SourceProjectId, SourceCashBankLedgerId, MergeProjectId, MergeCashBankLedgerId, DateFrom, DateTo);

                    //4.Merge/Move Source Cash/Bank Ledgers from source Project to Merge Project
                    if (resultArgs.Success)
                    {
                        dataManager.Parameters.Add(AppSchema.Project.FROM_PROJECT_IDColumn, SourceProjectId);
                        dataManager.Parameters.Add(AppSchema.Project.TO_PROJECT_IDColumn, MergeProjectId);
                        dataManager.Parameters.Add(AppSchema.Ledger.FROM_LEDGER_IDColumn, SourceCashBankLedgerId);
                        dataManager.Parameters.Add(AppSchema.Ledger.TO_LEDGER_IDColumn, MergeCashBankLedgerId);
                        dataManager.Parameters.Add(AppSchema.Bank.BANK_IDColumn, SourceBankLedgerBankId);
                        dataManager.Parameters.Add(AppSchema.Ledger.GROUP_IDColumn, MergeLedgerGroupId);

                        if (!String.IsNullOrEmpty(DateFrom))
                        {
                            dataManager.Parameters.Add(AppSchema.FDRegisters.DATE_FROMColumn, DateFrom);
                            dataManager.Parameters.Add(AppSchema.FDRegisters.DATE_TOColumn, DateTo);
                        }

                        //On 27/08/2021, to update modified details ---------------------------------------------------------------
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.MODIFIED_BYColumn, this.NumberSet.ToInteger(this.LoginUserId.ToString()));
                        dataManager.Parameters.Add(this.AppSchema.User.USER_NAMEColumn, this.FirstName); //MODIFIED_BY_NAME
                        //---------------------------------------------------------------------------------------------------------

                        //On 02/09/2021, To update Auditor Modification flag for Auditor users/for other user leave as it is.
                        //dataManager.Parameters.Add(this.AppSchema.User.IS_AUDITORColumn, (IsLoginUserAuditor ? 1 : 0));

                        //dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        resultArgs = dataManager.UpdateData();
                    }

                    //4. Move Ledger Opening Balance Too, If Merge is going to be done from the begining
                    if (resultArgs.Success && !IsDuriation && bincludeLedgerOpeningBalance)
                    {
                        resultArgs = UpdateCashBankLedgerOpeningBalance(dataManager, SourceProjectId, SourceCashBankLedgerId, MergeProjectId, MergeCashBankLedgerId);
                    }

                    dataManager.EndTransaction();
                }
            }
            return resultArgs;
        }

        private ResultArgs DeleteMergedProjectLedgers(DataManager dataManages)
        {
            resultArgs = FetchCommonProjectForMerge(dataManages);
            if (resultArgs.Success)
            {
                DataTable dtMappLedger = resultArgs.DataSource.Table;
                using (DataManager dataManager = new DataManager(SQLCommand.Mapping.DeleteCommonProjectForMerge))
                {
                    dataManager.Database = dataManages.Database;
                    // dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDsColumn, FetcheCommonProjectForMerge(dataManages));
                    dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_ID_COLLECTIONColumn, LedgerIdCollection + ',' + MapLedgerId);
                    //On 11/03/2024, To attach Project based merge
                    if (ProjectId > 0)
                    {
                        dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    }
                    resultArgs = dataManager.UpdateData();
                }
                //To replace the old Ledger Id to new one for mapping 
                DataView dvChangeLedgerId = dtMappLedger.DefaultView;
                dvChangeLedgerId.RowFilter = String.Format(AppSchema.Ledger.LEDGER_IDColumn.ColumnName + "  IN ({0})", LedgerIdCollection);
                DataTable dtFinalLedgers = dvChangeLedgerId.ToTable();
                dtFinalLedgers.Select().ToList<DataRow>().ForEach(r => r[AppSchema.Ledger.LEDGER_IDColumn.ColumnName] = MapLedgerId);
                //Mapping the remaining the ledgers
                DataView dvOldLedgerId = dtMappLedger.DefaultView;
                dvOldLedgerId.RowFilter = String.Format(AppSchema.Ledger.LEDGER_IDColumn.ColumnName + " NOT IN ({0})", LedgerIdCollection);
                dtFinalLedgers.Merge(dvOldLedgerId.ToTable());
                dtFinalLedgers = dtFinalLedgers.DefaultView.ToTable(true, new string[] { this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName, this.AppSchema.Project.PROJECT_IDColumn.ColumnName });

                foreach (DataRow dr in dtFinalLedgers.Rows)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectLedgerMappingAdd))
                    {
                        dataManager.Database = dataManages.Database;
                        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName]);
                        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName]);
                        resultArgs = dataManager.UpdateData();
                        if (!resultArgs.Success)
                            break;
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs DeleteMergedLedgers(DataManager dataManages)
        {
            object cmdsql = ProjectId > 0 ? SQLCommand.Mapping.DeleteMergedLedgersByProject : SQLCommand.Mapping.DeleteMergedLedgers;
            using (DataManager dataManager = new DataManager(cmdsql))
            {
                dataManager.Database = dataManages.Database;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_ID_COLLECTIONColumn, LedgerIdCollection);
                if (ProjectId > 0)
                {
                    dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                }
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }


        private ResultArgs MapLedgerToCostCentre(DataManager dataManager)
        {
            using (DataManager data = new DataManager(SQLCommand.Mapping.UpdateLedgerCostCentre))
            {
                data.Database = dataManager.Database;
                data.Parameters.Add(AppSchema.Ledger.MAP_LEDGER_IDColumn, MapLedgerId);
                resultArgs = data.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs IsTDSExists(string ledgerIdCollectionDetails)
        {
            using (DataManager data = new DataManager(SQLCommand.Mapping.IsExistTDSLedger))
            {
                data.Parameters.Add(AppSchema.Ledger.LEDGER_ID_COLLECTIONColumn, ledgerIdCollectionDetails);
                resultArgs = data.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }


        /// <summary>
        /// On 03/08/2020, To check given ledgers are budget made or mapped with budget
        /// </summary>
        /// <param name="ledgerIdCollectionDetails"></param>
        /// <returns></returns>
        public ResultArgs IsBudgetEnabledLedger(string ledgerIdCollectionDetails)
        {
            using (DataManager data = new DataManager(SQLCommand.Mapping.IsExistBudgetEnabledLedger))
            {
                data.Parameters.Add(AppSchema.Ledger.LEDGER_IDColumn, ledgerIdCollectionDetails);
                data.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = data.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }


        private ResultArgs MapLedgerToTDS(DataManager dataManager)
        {
            using (DataManager data = new DataManager(SQLCommand.Mapping.UpdateLedgerTDS))
            {
                data.Database = dataManager.Database;
                data.Parameters.Add(AppSchema.Ledger.MAP_LEDGER_IDColumn, MapLedgerId);
                resultArgs = data.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs UpdateVoucherModifiedDetailsByLedgerIds(string ledgerids, DataManager dManager)
        {
            using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
            {
                return vouchersystem.UpdateVoucherModifiedDetailsByLedgerIds(ledgerids, string.Empty, dManager);
            }
        }

        private ResultArgs UpdateVoucherModifiedDetails(string ledgerids, string projectid, DataManager dManager)
        {
            using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
            {
                return vouchersystem.UpdateVoucherModifiedDetailsByLedgerIds(ledgerids, projectid, dManager);
            }
        }

        /// <summary>
        /// On 14/10/2020, To merge budget and budget enabled ledgers
        /// 
        /// Update Budget Ledgers for Merged ledger like the below cases
        /// 1. Merge with Income Ledgers (Sum of all mapped Ledgers CRs - Sum of all mapped Ledgers DRs) as amount and transmode will be CR 
        /// 2. Merge with Expense Ledgers (Sum of all mapped Ledgers CRs - Sum of all mapped Ledgers DRs) as amount and transmode will be DR 
        /// 3. Merge with Asset/Liabilities Ledgers, Sum of all mapped Ledgers CRs as amount and CR
        ///    and  Sum of all mapped ledgers DRs as amount and transmode will be DR 
        ///    
        /// </summary>
        /// <param name="dataManages"></param>
        /// <returns></returns>
        private ResultArgs MergeBudgetedLedgers(DataManager dataManages)
        {
            ResultArgs resularg = new ResultArgs();
            Int32 MergeLedgerNatureId = 0;
            string ledgerids = LedgerIdCollection + "," + MapLedgerId.ToString();
            DataTable dtBudgetVouchers = new DataTable();
            using (LedgerSystem ledgetsystem = new LedgerSystem())
            {
                ledgetsystem.LedgerId = MapLedgerId;
                MergeLedgerNatureId = ledgetsystem.FetchLedgerNature();
            }

            //1. Get selected and mapped ledgers budget vouchers
            resularg = getMergeLedgerBudget(MergeLedgerNatureId, dataManages);
            if (resularg.Success && resularg.DataSource.Table != null)
            {
                dtBudgetVouchers = resularg.DataSource.Table;

                //2. Delete selected and mapped Ledgers - budget vouchers if existing
                if (dtBudgetVouchers != null && dtBudgetVouchers.Rows.Count > 0)
                {
                    resularg = DeleteBudgetLedgerAmountByLedger(dataManages, ledgerids);
                }

                //3. Insert mapped ledger budget vouchers with cummulative amount
                if (resularg.Success && dtBudgetVouchers != null && dtBudgetVouchers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtBudgetVouchers.Rows)
                    {
                        using (BudgetSystem budgetsystem = new BudgetSystem())
                        {
                            string tramsmode = TransSource.Cr.ToString();
                            if (MergeLedgerNatureId == (Int32)Natures.Assert || MergeLedgerNatureId == (Int32)Natures.Libilities)
                                tramsmode = dr[AppSchema.Budget.TRANS_MODEColumn.ColumnName].ToString();
                            else if (MergeLedgerNatureId == (Int32)Natures.Income)
                                tramsmode = TransSource.Cr.ToString();
                            else if (MergeLedgerNatureId == (Int32)Natures.Expenses)
                                tramsmode = TransSource.Dr.ToString();

                            budgetsystem.BudgetId = NumberSet.ToInteger(dr[AppSchema.Budget.BUDGET_IDColumn.ColumnName].ToString());
                            budgetsystem.LedgerId = MapLedgerId;
                            double proposedamount = Math.Abs(NumberSet.ToDouble(dr[AppSchema.Budget.PROPOSED_AMOUNTColumn.ColumnName].ToString()));
                            double approvedamount = Math.Abs(NumberSet.ToDouble(dr[AppSchema.Budget.APPROVED_AMOUNTColumn.ColumnName].ToString()));
                            if (proposedamount > 0 || approvedamount > 0)
                            {
                                resularg = budgetsystem.SaveMergedBudgetLedger(dataManages, tramsmode, proposedamount, approvedamount, string.Empty, string.Empty);
                            }
                            if (!resularg.Success)
                            {
                                break;
                            }
                        }
                    }
                }

                //4. Delete selected and mapped Ledgers budget mapping
                if (resularg.Success)
                {
                    resularg = DeleteMappedBudgetByLedger(dataManages, LedgerIdCollection);
                }
            }

            return resularg;
        }

        /// <summary>
        /// Get Selected and Merge Ledgers budget cummulity budget details based on mapped ledger nature
        /// </summary>
        /// <param name="MergeLedgerNatureId"></param>
        /// <returns></returns>
        private ResultArgs getMergeLedgerBudget(Int32 MergeLedgerNatureId, DataManager dataManages)
        {
            string ledgerids = LedgerIdCollection + "," + MapLedgerId.ToString();
            SQLCommand.Mapping sqlcmd = SQLCommand.Mapping.FetchIEMergeLedgersBudget;
            if (MergeLedgerNatureId == (Int32)Natures.Income || MergeLedgerNatureId == (Int32)Natures.Expenses)
            {
                sqlcmd = SQLCommand.Mapping.FetchIEMergeLedgersBudget;
            }
            else if (MergeLedgerNatureId == (Int32)Natures.Assert || MergeLedgerNatureId == (Int32)Natures.Libilities)
            {
                sqlcmd = SQLCommand.Mapping.FetchAIMergeLedgersBudget;
            }

            using (DataManager dataManager = new DataManager(sqlcmd))
            {
                dataManager.Database = dataManages.Database;
                dataManager.Parameters.Add(AppSchema.Ledger.ID_COLLECTIONSColumn, ledgerids);
                if (ProjectId > 0)
                {
                    dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs DeleteCategoryLedgers(DataManager dataManages)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.DeleteProjectCategoryByLedgerIdCollection))
            {
                //dataManager.Database = dataManages.Database;
                dataManager.Parameters.Add(AppSchema.Ledger.ID_COLLECTIONSColumn, LedgerIdCollection);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs UpdateMergedLedgerBalance(DataManager dataManagers)
        {
            resultArgs = FetchOpLedgers(dataManagers);
            if (resultArgs.Success)
            {
                DataTable dtOpBalance = resultArgs.DataSource.Table;
                resultArgs = DeleteMergeOpBalance(dataManagers);
                if (resultArgs.Success)
                {
                    if (dtOpBalance != null && dtOpBalance.Rows.Count > 0)
                    {
                        DataTable dtProjectId = dtOpBalance.AsEnumerable().GroupBy(r => r.Field<UInt32>("PROJECT_ID")).Select(g => g.First()).CopyToDataTable();
                        //To replace the old Ledger Id to new one for mapping 
                        DataView dvChangeLedgerId = dtOpBalance.DefaultView;
                        dvChangeLedgerId.RowFilter = String.Format(AppSchema.Ledger.LEDGER_IDColumn.ColumnName + "  IN ({0})", LedgerIdCollection + ',' + MapLedgerId);
                        DataTable dtFinalLedgers = dvChangeLedgerId.ToTable();
                        dtFinalLedgers.Select().ToList<DataRow>().ForEach(r => r[AppSchema.Ledger.LEDGER_IDColumn.ColumnName] = MapLedgerId);

                        //Mapping the remaining the ledgers
                        DataView dvOldLedgerId = dtOpBalance.DefaultView;
                        dvOldLedgerId.RowFilter = String.Format(AppSchema.Ledger.LEDGER_IDColumn.ColumnName + " NOT IN ({0})", LedgerIdCollection + ',' + MapLedgerId);
                        dtFinalLedgers.Merge(dvOldLedgerId.ToTable());
                        DataTable dtLedgerId = dtFinalLedgers.AsEnumerable().GroupBy(r => r.Field<UInt32>("LEDGER_ID")).Select(g => g.First()).CopyToDataTable();

                        foreach (DataRow dr in dtProjectId.Rows)
                        {
                            string Trans_Mode = "CR";
                            int ProjectId = NumberSet.ToInteger(dr[AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString());
                            OpeningBalanceDate = dr["BALANCE_DATE"].ToString();
                            DataView dvProjectBasedLedger = dtFinalLedgers.DefaultView;
                            dvProjectBasedLedger.RowFilter = String.Format("PROJECT_ID={0}", ProjectId);
                            double CreditAmount = NumberSet.ToDouble(dvProjectBasedLedger.ToTable().Compute("SUM(AMOUNT)", String.Format("TRANS_MODE='CR' AND PROJECT_ID='{0}'", ProjectId)).ToString());
                            double DebitAmount = NumberSet.ToDouble(dvProjectBasedLedger.ToTable().Compute("SUM(AMOUNT)", String.Format("TRANS_MODE='DR' AND PROJECT_ID='{0}'", ProjectId)).ToString());
                            CreditAmount = -CreditAmount;
                            double Totalamount = DebitAmount + CreditAmount;
                            if (Totalamount == 0)
                                Trans_Mode = LedgerNatureTransMode;
                            else if (Totalamount > 0)
                                Trans_Mode = "DR";
                            using (BalanceSystem balanceSystem = new BalanceSystem())
                            {
                                if (balanceSystem.HasBalance(ProjectId, LedgerId))
                                    balanceSystem.UpdateOpBalance(OpeningBalanceDate, ProjectId, MapLedgerId, Totalamount, Trans_Mode, TransactionAction.Cancel);
                                balanceSystem.UpdateOpBalance(OpeningBalanceDate, ProjectId, MapLedgerId, Totalamount, Trans_Mode, TransactionAction.New);
                            }
                        }
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs UpdateOpBalanceAfterMerging(DataManager dataManagers, DataTable dtOpBalance)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchOpLedgers))
            {
                dataManager.Database = dataManagers.Database;
                if (dtOpBalance != null)
                {
                    foreach (DataRow dr in dtOpBalance.Rows)
                    {
                        using (BalanceSystem balanceSystem = new BalanceSystem())
                        {
                            if (balanceSystem.HasBalance(NumberSet.ToInteger(dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString()), LedgerId))
                                balanceSystem.UpdateOpBalance(OpeningBalanceDate, NumberSet.ToInteger(dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString()), LedgerId, this.NumberSet.ToDouble(dr[AMOUNT].ToString()), dr[TRANS_MODE].ToString(), TransactionAction.Cancel);
                            balanceSystem.UpdateOpBalance(OpeningBalanceDate, NumberSet.ToInteger(dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString()), LedgerId, this.NumberSet.ToDouble(dr[AMOUNT].ToString()), dr[TRANS_MODE].ToString(), TransactionAction.New);
                        }
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs FetchOpLedgers(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchOpLedgers))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_ID_COLLECTIONColumn, LedgerIdCollection + ',' + MapLedgerId);

                //On 11/03/2024, To attach Project based merge
                if (ProjectId > 0)
                {
                    dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                }

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs DeleteMergeOpBalance(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.DeleteMergedOpBalance))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_ID_COLLECTIONColumn, LedgerIdCollection + ',' + MapLedgerId);
                //On 11/03/2024, To attach Project based merge
                if (ProjectId > 0)
                {
                    dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                }
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs FetchCommonProjectForMerge(DataManager dataManages)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchCommonProjectForMerge))
            {
                dataManager.Database = dataManages.Database;
                dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_ID_COLLECTIONColumn, LedgerIdCollection + ',' + MapLedgerId);

                //On 11/03/2024, To attach Project based merge
                if (ProjectId > 0)
                {
                    dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                }

                //  dataManager.Parameters.Add(this.AppSchema.Ledger.MAP_LEDGER_IDColumn, MapLedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        #endregion

        #region Cost Center System
        public ResultArgs FetchCostCentreDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.LoadAllCostCentre))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchCostCentreDetailsforCostCategory()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.LoadAllCostCentreforcostcategory))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchMappedCostCenter()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchMappedCostCenter))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(this.AppSchema.CostCentre.COSTCENTRE_MAPPINGColumn, this.CostCeterMapping);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchMappedCostCenterByProjectLedger(string ProjectIds, Int32 LedgerId = 0)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchMappedCostCenterByProjectLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDsColumn, ProjectIds);
                if (LedgerId > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName, LedgerId);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchMappedCostCenterByCostCenterid()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchMappedCostCenterByCostCenterId))
            {
                dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, CostCenterId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs AccountMappingCostCenter()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = DeleteMappedCostCenter(dataManager, ProjectId, LedgerId);
                if (resultArgs.Success)
                {
                    MapCostCentre(dtCostCenterIDCollection, ProjectId, LedgerId);
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public ResultArgs AccountMappingGeneralate()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = DeleteMappedGeneralate(dataManager, GeneralateId);
                if (resultArgs.Success)
                {
                    MapGeneralateLedgers(dtGeneralateIDCollection, GeneralateId);
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        /// <summary>
        /// Account Mapping Purpose
        /// </summary>
        /// <returns></returns>
        public ResultArgs AccountMappingPurpose()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = DeleteMappedPurpose(dataManager, ProjectId);
                if (resultArgs.Success)
                {
                    resultArgs = DeletePurposeCostCentreDistributionAmountByProject(dataManager, ProjectId);
                    if (resultArgs.Success)
                    {
                        resultArgs = MapPurpose(dtPurposeIDCollection, ProjectId, dataManager);
                    }
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }
        public ResultArgs AccountMappingCostCenterByCCId(DataManager dataManagers)
        {
            if (dtCostCenterIDCollection != null)
            {
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Database = dataManagers.Database;
                    resultArgs = UnMapMappedCostCentreByCCId(dataManager);
                    if (resultArgs.Success)
                        MapCostCentreByCCId(dataManager);
                }
            }
            else
            {
                resultArgs = new ResultArgs();
                resultArgs.Success = true;
            }

            return resultArgs;
        }

        private ResultArgs MapCostCentre(DataTable dtCostCenterId, int ProjectId, int CCMapLedgerId)
        {
            if (dtCostCenterId != null && dtCostCenterId.Rows.Count > 0)
            {
                foreach (DataRow dr in dtCostCenterId.Rows)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectCostCentreMappingAdd))
                    {
                        dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, dr[this.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName]);
                        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, CCMapLedgerId);
                        dataManager.Parameters.Add(this.AppSchema.FDRegisters.AMOUNTColumn, dr[this.AppSchema.FDRegisters.AMOUNTColumn.ColumnName]);
                        dataManager.Parameters.Add(this.AppSchema.FDRegisters.TRANS_MODEColumn, dr[this.AppSchema.FDRegisters.TRANS_MODEColumn.ColumnName]);
                        resultArgs = dataManager.UpdateData();
                    }
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// Generalate Ledgers details
        /// </summary>
        /// <param name="dtCostCenterId"></param>
        /// <param name="ProjectId"></param>
        /// <param name="CCMapLedgerId"></param>
        /// <returns></returns>
        private ResultArgs MapGeneralateLedgers(DataTable dtGeneralateLedger, int GenMapLedgerId)
        {
            if (dtGeneralateLedger != null && dtGeneralateLedger.Rows.Count > 0)
            {
                foreach (DataRow dr in dtGeneralateLedger.Rows)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectGeneralateMappingAdd))
                    {
                        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName]);
                        dataManager.Parameters.Add(this.AppSchema.GeneralateGroupLedger.CON_LEDGER_IDColumn, GenMapLedgerId);
                        resultArgs = dataManager.UpdateData();
                    }
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// Map Purpose
        /// </summary>
        /// <param name="dtCostCenterId"></param>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        private ResultArgs MapPurpose(DataTable dtPurposeId, int ProjectId, DataManager dm)
        {
            if (dtPurposeId != null && dtPurposeId.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPurposeId.Rows)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectPurposeMappingAdd))
                    {
                        dataManager.Database = dm.Database;
                        dataManager.Parameters.Add(this.AppSchema.Purposes.CONTRIBUTION_IDColumn, dr[this.AppSchema.Purposes.CONTRIBUTION_IDColumn.ColumnName]);
                        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                        dataManager.Parameters.Add(this.AppSchema.FDRegisters.AMOUNTColumn, dr[this.AppSchema.FDRegisters.AMOUNTColumn.ColumnName]);
                        dataManager.Parameters.Add(this.AppSchema.FDRegisters.TRANS_MODEColumn, dr[this.AppSchema.FDRegisters.TRANS_MODEColumn.ColumnName]);
                        resultArgs = dataManager.UpdateData();

                        //On 06/07/2022, To update Purpose CC distribution amount
                        if (dtPurposeCCDistribution != null && dtPurposeCCDistribution.Rows.Count > 0)
                        {
                            int purposeid = NumberSet.ToInteger(dr[this.AppSchema.Purposes.CONTRIBUTION_IDColumn.ColumnName].ToString());
                            UpdatePurposeCostCentreDistributionAmount(ProjectId, purposeid, dm);
                        }
                    }
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// To delete already distributed purpose opening balance
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="PurposeId"></param>
        /// <returns></returns>
        private ResultArgs DeletePurposeCostCentreDistributionAmountByProject(DataManager dm, int ProjectId)
        {
            if (dtPurposeCCDistribution != null && ProjectId > 0)
            {
                //Delete already distributed amount for project
                using (DataManager dataManager = new DataManager(SQLCommand.Mapping.DeletePurposeCCDistribution))
                {
                    dataManager.Database = dm.Database;
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    resultArgs = dataManager.UpdateData();
                }
            }

            return resultArgs;
        }

        /// <summary>
        /// Update distributed purpose opening balance to costcentre
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="PurposeId"></param>
        /// <returns></returns>
        private ResultArgs UpdatePurposeCostCentreDistributionAmount(int ProjectId, int PurposeId, DataManager dm)
        {
            try
            {
                if (dtPurposeCCDistribution != null && dtPurposeCCDistribution.Rows.Count > 0 && ProjectId > 0 && PurposeId > 0)
                {
                    dtPurposeCCDistribution.DefaultView.RowFilter = "CONTRIBUTION_ID = " + PurposeId + " AND COST_CENTRE_ID>0 AND AMOUNT>0";
                    DataTable dtCCDistribution = dtPurposeCCDistribution.DefaultView.ToTable();
                    dtPurposeCCDistribution.DefaultView.RowFilter = string.Empty;

                    foreach (DataRow dr in dtCCDistribution.Rows)
                    {
                        using (DataManager dataManager = new DataManager(SQLCommand.Mapping.InsertUpdateProjectPurposeCCDistribution))
                        {
                            dataManager.Database = dm.Database;
                            dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                            dataManager.Parameters.Add(this.AppSchema.Purposes.CONTRIBUTION_IDColumn, dr[this.AppSchema.Purposes.CONTRIBUTION_IDColumn.ColumnName]);
                            dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, dr[this.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName]);
                            dataManager.Parameters.Add(this.AppSchema.FDRegisters.AMOUNTColumn, dr[this.AppSchema.FDRegisters.AMOUNTColumn.ColumnName]);
                            dataManager.Parameters.Add(this.AppSchema.FDRegisters.TRANS_MODEColumn, dr[this.AppSchema.FDRegisters.TRANS_MODEColumn.ColumnName].ToString().ToUpper());
                            resultArgs = dataManager.UpdateData();
                            if (!resultArgs.Success)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                resultArgs.Message = err.Message;
                dtPurposeCCDistribution.DefaultView.RowFilter = string.Empty;
            }

            return resultArgs;
        }

        private ResultArgs DeleteMappedCostCenter(DataManager dataManagers, int ProjectId, int CCMapLedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.DeleteProjectCostCenterMapping))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, CCMapLedgerId);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteMappedGeneralate(DataManager dataManagers, int GeneralateId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.DeleteProjectGeneralateMapping))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.GeneralateGroupLedger.CON_LEDGER_IDColumn, GeneralateId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }


        /// <summary>
        /// Delete Mapped Purpose Details
        /// </summary>
        /// <param name="dataManagers"></param>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        private ResultArgs DeleteMappedPurpose(DataManager dataManagers, int ProjectId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.DeleteProjectPurposeMapping))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        private ResultArgs UnMapMappedCostCentreByCCId(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.UnMapCostCentreByCCId))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, CostCenterId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs MapCostCentreByCCId(DataManager dataManagers)
        {
            if (dtCostCenterIDCollection != null)
            {
                foreach (DataRow dr in dtCostCenterIDCollection.Rows)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectCostCentreMappingAdd))
                    {
                        dataManager.Database = dataManagers.Database;
                        dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, CostCenterId);
                        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName]);
                        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                        dataManager.Parameters.Add(this.AppSchema.FDRegisters.AMOUNTColumn, dr[this.AppSchema.FDRegisters.AMOUNTColumn.ColumnName]);
                        dataManager.Parameters.Add(this.AppSchema.FDRegisters.TRANS_MODEColumn, dr[this.AppSchema.FDRegisters.TRANS_MODEColumn.ColumnName]);
                        resultArgs = dataManager.UpdateData();
                    }
                }
            }
            return resultArgs;
        }
        #endregion

        #region Donor System
        /// <summary>
        /// To get all the Available Donors
        /// </summary>
        /// <returns>ResultArgs Type </returns>
        public ResultArgs LoadAllDonors()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.LoadAllDonor))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// To get all the Available Purpose
        /// </summary>
        /// <returns>ResultArgs Type </returns>
        public ResultArgs LoadAllPurpose()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.LoadAllPurpose))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// Generalate Ledger 19.07.2024
        /// </summary>
        /// <returns>ResultArgs Type </returns>
        public ResultArgs LoadGeneralateLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.LoadGeneralateLedger))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
            }
            return resultArgs;
        }

        public ResultArgs FetchMappedDonor()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchDonorMapped))
            {
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchMappedPurpose()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchPurposeMapped))
            {
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchPurposeCostCentreDistribution()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchPurposeCostCentreDistribution))
            {
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchInvestmentType()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchInvestmentType))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchProjectLedgerCostCentreDistribution(Int32 Pid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchProjectLedgerCostCentreDistribution))
            {
                if (Pid > 0)
                {
                    dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, Pid);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs CheckCostCentreMappingBySetting(bool IsProjectwise)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.CheckCostCentreMappingBySetting))
            {
                dataManager.Parameters.Add(AppSchema.CostCentre.COSTCENTRE_MAPPINGColumn, (IsProjectwise ? 0 : 1));
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        public ResultArgs FetchProjectLedgerApplicableByLedgerId(Int32 Lid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchProjectLedgerApplicable))
            {
                dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_IDColumn, Lid);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchProjectLedgerApplicableByProjectId(Int32 Pid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchProjectLedgerApplicable))
            {
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, Pid);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        /// <summary>
        /// On 01/12/2022, Change All cost center Mapping from PRoject-wise to Leger-wise and vice-versa
        /// </summary>
        /// <param name="IsProjectwise"></param>
        /// <returns></returns>
        public ResultArgs ChangeAllCostCentreMapping(bool ChangeToProjectwise)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.DeleteAllCostCentreMapping))
            {
                dataManager.BeginTransaction();
                resultArgs = dataManager.UpdateData();
                if (resultArgs.Success)
                {
                    using (DataManager dm = new DataManager(ChangeToProjectwise ? SQLCommand.Mapping.ChangeAllCostCentreMappingProjectBased :
                            SQLCommand.Mapping.ChangeAllCostCentreMappingLedgerBased))
                    {
                        dm.Database = dataManager.Database;
                        resultArgs = dm.UpdateData();
                    }
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public ResultArgs FetchMappedDonorById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchMappedDonorByDonorId))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.DONAUD_IDColumn, DonorId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs MappDonor()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                AccountMappingDonor(dataManager);
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public ResultArgs AccountMappingDonor(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Database = dataManagers.Database;
                resultArgs = UnMapDonor(dataManager);
                if (resultArgs.Success)
                    MapDonor(dataManager);
            }
            return resultArgs;
        }

        public ResultArgs AccountMappingDonorByDonorId(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Database = dataManagers.Database;
                resultArgs = UnMapDonorByDonorId(dataManager);
                if (resultArgs.Success)
                    MapDonorByDonorId(dataManager);
            }
            return resultArgs;
        }

        private ResultArgs MapDonor(DataManager dataManagers)
        {
            foreach (DataRow dr in dtDonorMapping.Rows)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Mapping.DonorMap))
                {
                    dataManager.Database = dataManagers.Database;
                    if (dtDonorMapping != null && dtDonorMapping.Rows.Count > 0)
                    {
                        dataManager.Parameters.Add(this.AppSchema.ProjectVoucher.PROJECT_IDColumn, ProjectId);
                        dataManager.Parameters.Add(this.AppSchema.DonorAuditor.DONAUD_IDColumn, this.NumberSet.ToInteger(dr[this.AppSchema.DonorAuditor.DONAUD_IDColumn.ColumnName].ToString()));
                        resultArgs = dataManager.UpdateData();
                    }
                    if (!resultArgs.Success)
                        break;
                }

            }
            return resultArgs;
        }

        private ResultArgs MapDonorByDonorId(DataManager dataManagers)
        {
            if (dtDonorMapping != null)
            {
                foreach (DataRow dr in dtDonorMapping.Rows)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Mapping.DonorMap))
                    {
                        if (dtDonorMapping != null && dtDonorMapping.Rows.Count > 0)
                        {
                            dataManager.Parameters.Add(this.AppSchema.ProjectVoucher.PROJECT_IDColumn, this.NumberSet.ToInteger(dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString()));
                            dataManager.Parameters.Add(this.AppSchema.DonorAuditor.DONAUD_IDColumn, DonorId);
                            resultArgs = dataManager.UpdateData();
                        }
                        if (!resultArgs.Success)
                            break;
                    }
                }
            }
            return resultArgs;
        }
        /// <summary>
        /// Map Donor from transaction
        /// </summary>
        /// <param name="dataManagers"></param>
        /// <returns></returns>
        public ResultArgs MapDonorTransaction()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.DonorMap))
            {
                dataManager.Parameters.Add(this.AppSchema.ProjectVoucher.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.DONAUD_IDColumn, DonorId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs CheckDonorMapped()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.CheckDonorMapped))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.DONAUD_IDColumn, DonorId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        private ResultArgs UnMapDonor(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.DonorUnMap))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs UnMapDonorByDonorId(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.DonorUnMapByDonorId))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.DONAUD_IDColumn, DonorId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        public ResultArgs FetchMappedActiveDonors()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchMappedActiveDonors))
            {
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                if (DonorId > 0)
                {
                    dataManager.Parameters.Add(AppSchema.DonorAuditor.DONAUD_IDColumn, DonorId);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public int CheckDonorStatus(int donorId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchDonorStatus))
            {
                dataManager.Parameters.Add(AppSchema.DonorAuditor.DONAUD_IDColumn, donorId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        #endregion

        #region CostCentreCategory System
        public ResultArgs MapCostCategoryDetails(DataTable dtCostCenterId, int CostCategoryId)
        {
            foreach (DataRow dr in dtCostCenterId.Rows)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Mapping.CostcentreCostCategoryMappingAdd))
                {
                    dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, dr[this.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName]);
                    dataManager.Parameters.Add(this.AppSchema.CostCentreCategory.COST_CENTRECATEGORY_IDColumn, CostCategoryId);
                    resultArgs = dataManager.UpdateData();
                }
            }
            return resultArgs;
        }

        private ResultArgs DeleteMappedCostCenterCategory(DataManager dataManagers, int CostCategoryId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.DeleteMapCostCategory))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.CostCentreCategory.COST_CENTRECATEGORY_IDColumn, CostCategoryId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs AccountMappingCostCategory()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = DeleteMappedCostCenterCategory(dataManager, CostCentreCategoryId);
                if (resultArgs.Success)
                {
                    MapCostCategoryDetails(dtCostCategoryIDCollection, CostCentreCategoryId);
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public ResultArgs FetchMappedCostCategory()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchMappedCostCategory))
            {
                dataManager.Parameters.Add(this.AppSchema.CostCentreCategory.COST_CENTRECATEGORY_IDColumn, CostCategoryId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchCostCentreTransaction()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.CostCentreCategoryUnmap))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.IDsColumn, CostCenterIDs);
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);

                if (LedgerId > 0)
                {
                    dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchCostCentreCategoryTransaction()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchCostCentreUnmapTransaction))
            {
                if (ProjectId > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                }

                dataManager.Parameters.Add(this.AppSchema.Ledger.IDsColumn, CostCenterIDs);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchFCPurposeTransaction()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchFCPurposeUnmapTransaction))
            {
                if (ProjectId > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                }
                dataManager.Parameters.Add(this.AppSchema.Ledger.IDsColumn, FCPurposeIDs);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion

        #region Common System

        public Int32 GetBankId(int LedgerId)
        {

            using (BankSystem banksystem = new BankSystem())
            {
                return banksystem.GetBankIdByLedgerId(LedgerId);
            }
        }

        public ResultArgs FetchBankId(int LedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.BankIdByLedgerId))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable); ;
            }
            return resultArgs;
        }
        public ResultArgs FixedDeposit()
        {
            using (DataManager dm = new DataManager(SQLCommand.Mapping.FixedDepositId))
            {
                resultArgs = dm.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchCategoryLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchcategoryLedgerByProject))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 05/02/2021
        /// To get given Ledger's Group Id
        /// </summary>
        /// <param name="LedgerId"></param>
        /// <returns></returns>
        public Int32 FetchLedgerGroup(int LedgerId)
        {
            Int32 groupid = 0;
            using (LedgerSystem ledgersystem = new LedgerSystem(LedgerId))
            {
                groupid = ledgersystem.GroupId;
            }

            return groupid;
        }

        /// <summary>
        /// This method is get Project and Its Ledger's Books begin opening balance
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="ledgerid"></param>
        /// <returns></returns>
        public ResultArgs FetchLedgerOpeningBalance(Int32 projectId, Int32 ledgerid)
        {
            ResultArgs result = new ResultArgs();
            using (BalanceSystem balanceSystem = new BalanceSystem())
            {
                //This method is used to get given Project and Its Ledger's Books begin opening balance
                result = balanceSystem.GetLedgerOpBalance(projectId, ledgerid);
            }
            return result;
        }

        /// <summary>
        /// On 24/02/2021
        ///// This method is used to update Project and Its Ledger's Books begin opening balance
        ///// </summary>
        ///// <returns></returns>
        public ResultArgs UpdateCashBankLedgerOpeningBalance(DataManager dmanager, Int32 sourceprojectId, Int32 sourcecashbankledgerid, Int32 toprojectId, Int32 tocashbankledgerid)
        {
            double sourceopamount = 0;
            double toopamount = 0;
            double finalamount = 0;

            //1. Get source project's opening balance
            resultArgs = FetchLedgerOpeningBalance(sourceprojectId, sourcecashbankledgerid);
            if (resultArgs.Success)
            {
                DataTable dtLedgerOpBal = resultArgs.DataSource.Table;
                if (dtLedgerOpBal != null && dtLedgerOpBal.Rows.Count > 0)
                {
                    //string ledgerOpBalDate = dtLedgerOpBal.Rows[0][this.AppSchema.LedgerBalance.BALANCE_DATEColumn.ColumnName].ToString();
                    sourceopamount = this.NumberSet.ToDouble(dtLedgerOpBal.Rows[0][this.AppSchema.LedgerBalance.AMOUNTColumn.ColumnName].ToString());

                    //1.a, If Source project has opening balance, it will try to get to project's op amout, calculate and update.
                    if (sourceopamount != 0)
                    {
                        string sourcetransmode = dtLedgerOpBal.Rows[0][this.AppSchema.LedgerBalance.TRANS_MODEColumn.ColumnName].ToString();
                        if (sourcetransmode.ToUpper() == TransSource.Cr.ToString().ToUpper())
                        {
                            sourceopamount = -sourceopamount;
                        }

                        //2. Get to project's opening balance
                        resultArgs = FetchLedgerOpeningBalance(toprojectId, tocashbankledgerid);
                        if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            dtLedgerOpBal = resultArgs.DataSource.Table;
                            //string ledgerOpBalDate = dtLedgerOpBal.Rows[0][this.AppSchema.LedgerBalance.BALANCE_DATEColumn.ColumnName].ToString();
                            toopamount = this.NumberSet.ToDouble(dtLedgerOpBal.Rows[0][this.AppSchema.LedgerBalance.AMOUNTColumn.ColumnName].ToString());
                            string totransmode = dtLedgerOpBal.Rows[0][this.AppSchema.LedgerBalance.TRANS_MODEColumn.ColumnName].ToString();
                            if (totransmode.ToUpper() == TransSource.Cr.ToString().ToUpper())
                            {
                                toopamount = -toopamount;
                            }
                        }
                    }

                    //Updae Cash and Bank Opening 

                    //Calculate final amount
                    string opdate = DateSet.ToDate(this.BookBeginFrom, false).AddDays(-1).ToShortDateString();
                    finalamount = sourceopamount + toopamount;
                    using (DataManager dataManager = new DataManager(SQLCommand.Mapping.MergeCashBankLedgersOPBalance))
                    {
                        dataManager.Database = dataManager.Database;

                        dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BALANCE_DATEColumn, opdate);
                        dataManager.Parameters.Add(this.AppSchema.LedgerBalance.PROJECT_IDColumn, toprojectId);
                        dataManager.Parameters.Add(this.AppSchema.LedgerBalance.LEDGER_IDColumn, tocashbankledgerid);
                        dataManager.Parameters.Add(AppSchema.Project.FROM_PROJECT_IDColumn, sourceprojectId);
                        dataManager.Parameters.Add(AppSchema.Ledger.FROM_LEDGER_IDColumn, sourcecashbankledgerid);
                        dataManager.Parameters.Add(this.AppSchema.LedgerBalance.LEDGER_IDColumn, tocashbankledgerid);
                        dataManager.Parameters.Add(this.AppSchema.LedgerBalance.TRANS_FLAGColumn, "OP");
                        dataManager.Parameters.Add(this.AppSchema.LedgerBalance.AMOUNTColumn, finalamount);
                        dataManager.Parameters.Add(this.AppSchema.LedgerBalance.TRANS_MODEColumn, (finalamount >= 0 ? TransSource.Dr : TransSource.Cr));
                        dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BRANCH_IDColumn, "0");
                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        resultArgs = dataManager.FetchData(DataSource.DataTable);
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs UpdateCurrentFYExchangeRate(DataTable dtFYExchangeRate)
        {
            ResultArgs result = new ResultArgs();
            if (dtFYExchangeRate != null)
            {
                DataTable dtFYCurrencyExchangeAmount = dtFYExchangeRate.DefaultView.ToTable();
                dtFYCurrencyExchangeAmount.DefaultView.RowFilter = this.AppSchema.Country.EXCHANGE_RATEColumn.ColumnName + ">0";
                dtFYCurrencyExchangeAmount = dtFYCurrencyExchangeAmount.DefaultView.ToTable();

                //DataTable dtFYExchangeAmount =  
                using (CountrySystem countrysystem = new CountrySystem())
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Country.UpdateCountryCurrencyExchangeRate))
                    {
                        dataManager.BeginTransaction();
                        countrysystem.CountryId = 0;
                        result = countrysystem.DeleteCountryCurrencyExchangeRate(dataManager);
                        if (result.Success)
                        {
                            foreach (DataRow dr in dtFYCurrencyExchangeAmount.Rows)
                            {
                                countrysystem.CountryId = NumberSet.ToInteger(dr[this.AppSchema.Country.COUNTRY_IDColumn.ColumnName].ToString());
                                countrysystem.ExchangeRate = NumberSet.ToDouble(dr[this.AppSchema.Country.EXCHANGE_RATEColumn.ColumnName].ToString());

                                result = countrysystem.UpdateCountryCurrencyExchangeRate(dataManager);
                                if (!result.Success)
                                {
                                    break;
                                }
                            }
                        }
                        dataManager.EndTransaction();

                        //On 09/09/2024, Refresh Opening Balances after updating Opening Balances
                        //For Temp pupose, we refresh entire year
                        if (result.Success && this.AllowMultiCurrency == 1 && DateSet.ToDate(this.YearFrom, false) == this.FirstFYDateFrom)
                        {
                            using (BalanceSystem balanceSystem = new BalanceSystem())
                            {
                                result = balanceSystem.UpdateCashBankOpBalanceByExchangeRate(this.BookBeginFrom);
                            }
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 12/09/2024, To Enforce to skip Default Ledgers for other than country 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public ResultArgs EnforceSkipDefaultLedgers(ResultArgs result, string fieldname)
        {
            try
            {
                if (AllowMultiCurrency == 1 || this.IsCountryOtherThanIndia)
                {
                    if (result.Success)
                    {
                        if (result.DataSource.Table != null)
                        {
                            if (result.DataSource.GetType() == typeof(DataTable))
                            {

                            }
                            DataTable dt = result.DataSource.Table;
                            dt.DefaultView.RowFilter = fieldname + " NOT IN (" + this.GetDefaultGeneralLedgersIds + ")";
                            dt = dt.DefaultView.ToTable();
                            result.DataSource.Data = dt;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                result.Message = err.Message;
            }
            return result;
        }

        /// <summary>
        /// On 23/11/2022, If we change 
        /// </summary>
        /// <returns></returns>
        public ResultArgs RestCCMappingAndOpening()
        {

            return resultArgs;
        }
        #endregion
    }
}
