using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using System.Data;

namespace AcMEDSync.Model
{
    public class ExportMasters : DsyncSystemBase
    {
        public DataSet dsMaster = new DataSet("dsMasters");
        public DataSet dsTDSMasters = new DataSet("dsTDSMaster");
        ResultArgs resultArgs = null;
        string BranchCode = string.Empty;

        //Budget Properties
        string BudgetName { get; set; }
        private DateTime BudgetDateFrom { get; set; }
        private DateTime BudgetDateTo { get; set; }
        private string BudgetProjectIds { get; set; }
        private int BudgetTypeId { get; set; }

        public ResultArgs GetMasters(string branchOfficeCode)
        {
            BranchCode = branchOfficeCode;
            AcMEDataSynLog.WriteLog("GetMasters Started..");
            resultArgs = new ResultArgs();

            if (!string.IsNullOrEmpty(BranchCode))
            {
                resultArgs = FetchLegalEntity();
                if (resultArgs.Success)
                {
                    resultArgs = FetchProjects();
                    if (resultArgs.Success)
                    {
                        resultArgs = FetchGoverningMember();
                        if (resultArgs.Success)
                        {
                            resultArgs = FetchPurpose();
                            if (resultArgs.Success)
                            {
                                resultArgs = FetchLedgerGroup();
                                if (resultArgs.Success)
                                {
                                    resultArgs = FetchLedgers();
                                    //On 22/12/2021, To get Budget Group and Budget Sub Group

                                    // To get the Generalate Ledgers (17.08.2022)
                                    resultArgs = FetchGeneralateLedgerGroup();
                                    if (resultArgs.Success)
                                    {
                                        resultArgs = FetchGeneralateLedgerGroupMap();
                                        if (resultArgs.Success)
                                        {
                                            resultArgs = FetchBudgetGroup();
                                            if (resultArgs.Success)
                                            {
                                                resultArgs = FetchBudgetSubGroup();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                resultArgs.Message = "Branch Office Code is Empty.";
            }

            if (resultArgs.Success)
            {
                resultArgs.DataSource.Data = dsMaster;
                AcMEDataSynLog.WriteLog("GetMasters Ended..");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in GetMasters" + resultArgs.Message);
            }
            return resultArgs;
        }

        public ResultArgs GetTDSMasters()
        {
            AcMEDataSynLog.WriteLog("GetTDSMasters Started..");
            resultArgs = new ResultArgs();

            resultArgs = FetchTDSSection();
            if (resultArgs.Success)
            {
                resultArgs = FetchNatureOfPayments();
                if (resultArgs.Success)
                {
                    resultArgs = FetchDeducteeTypes();
                    if (resultArgs.Success)
                    {
                        resultArgs = FetchDutyTax();
                        if (resultArgs.Success)
                        {
                            resultArgs = FetchTDSPolicyDeductees();
                            if (resultArgs.Success)
                            {
                                resultArgs = FetchTDSPolicy();
                                if (resultArgs.Success)
                                {
                                    resultArgs = FetchTaxRate();
                                }
                            }
                        }
                    }
                }
            }

            if (resultArgs.Success)
            {
                resultArgs.DataSource.Data = dsTDSMasters;
                AcMEDataSynLog.WriteLog("GetTDSMasters Ended..");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in GetTDSMasters" + resultArgs.Message);
            }
            return resultArgs;
        }

        /// <summary>
        /// this is common method in datasync for branch as well as for portal
        /// This method is used to get budget from branch office or Portal based on branch id
        /// if branchid =0, branch db or portal head office db
        /// if this method is called from branch office, branch_id will be 0, else real branch_id from portal
        /// 
        /// if its from portal, will return only approved budget alone otherwise empty
        /// 
        /// It will return budget detials which contains given budget's master, project and ledger details
        /// selecting budget bt branchid, datefrom, dateto, projects and its budget typeid
        /// 
        /// On 27/02/2020, For Sub Ledger Budget for Mysore, Add Sub Ledger Budgets tooo
        /// </summary>
        /// <returns></returns>
        public ResultArgs GetBudget(Int32 BranchId, DateTime datefrom, DateTime dateto, string Projectids, Int32 budgettypeid)
        {
            AcMEDataSynLog.WriteLog("GetBudget Started..");
            resultArgs = new ResultArgs();
            DataSet dsBudget = new DataSet("dsBudget");

            resultArgs = FetchBudgetMaster(BranchId, datefrom, dateto, Projectids, budgettypeid);
            if (resultArgs.Success && resultArgs.RowsAffected > 0)
            {
                dsBudget.Tables.Add(resultArgs.DataSource.Table); //Budget Master
                resultArgs = FetchBudgetProject(BranchId, datefrom, dateto, Projectids, budgettypeid);
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    dsBudget.Tables.Add(resultArgs.DataSource.Table); //Budget project
                    resultArgs = FetchBudgetLedger(BranchId, datefrom, dateto, Projectids, budgettypeid);
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        dsBudget.Tables.Add(resultArgs.DataSource.Table); //Budget Ledger
                    }

                    //On 27/02/2020, For Sub Ledger Budget details
                    resultArgs = FetchBudgetSubLedger(BranchId, datefrom, dateto, Projectids, budgettypeid);
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        dsBudget.Tables.Add(resultArgs.DataSource.Table); //Budget Ledger
                    }
                }
            }

            if (resultArgs.Success)
            {
                resultArgs.DataSource.Data = dsBudget;
                AcMEDataSynLog.WriteLog("GetBudget Ended..");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in GetBudget" + resultArgs.Message);
            }
            return resultArgs;
        }

        private ResultArgs FetchLegalEntity()
        {
            AcMEDataSynLog.WriteLog("FetchLegalEntity Started..");
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ExportMasters.LegalEntityFetchAll, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_CODEColumn, BranchCode);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        resultArgs.DataSource.Table.TableName = "LegalEntity";
                        dsMaster.Tables.Add(resultArgs.DataSource.Table);
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("FetchLegalEntity Ended..");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in FetchLegalEntity:" + resultArgs.Message);
            }
            return resultArgs;
        }

        public ResultArgs FetchProjects()
        {
            AcMEDataSynLog.WriteLog("FetchProjects Started..");
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ExportMasters.ProjectFetchAll, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_CODEColumn, BranchCode);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        resultArgs.DataSource.Table.TableName = "Project";
                        dsMaster.Tables.Add(resultArgs.DataSource.Table);
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("FetchProjects Ended..");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in FetchProjects:" + resultArgs.Message);
            }
            return resultArgs;
        }

        public ResultArgs FetchGoverningMember()
        {
            AcMEDataSynLog.WriteLog("FetchGoverningMember Started..");
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ExportMasters.GoverningMemberFetchAll, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        resultArgs.DataSource.Table.TableName = "GoverningMember";
                        dsMaster.Tables.Add(resultArgs.DataSource.Table);
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("FetchGoverningMember Ended..");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in FetchGoverningMember:" + resultArgs.Message);
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgerGroup()
        {
            AcMEDataSynLog.WriteLog("FetchLedgerGroup Started");
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ExportMasters.LedgerGroupFetchAll, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        resultArgs.DataSource.Table.TableName = "LedgerGroup";
                        dsMaster.Tables.Add(resultArgs.DataSource.Table);
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("FetchLedgerGroup Ended..");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in FetchLedgerGroup:" + resultArgs.Message);
            }
            return resultArgs;
        }


        public ResultArgs FetchLedgers()
        {
            AcMEDataSynLog.WriteLog("FetchLedgers Started");
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ExportMasters.LedgerFetchAll, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    if (!IsClientBranch)
                        dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_CODEColumn, BranchCode);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        resultArgs.DataSource.Table.TableName = "Ledger";
                        dsMaster.Tables.Add(resultArgs.DataSource.Table);
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("FetchLedgers Ended..");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in FetchLedgers:" + resultArgs.Message);
            }
            return resultArgs;
        }

        /// <summary>
        /// Generalate Ledgers
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchGeneralateLedgerGroup()
        {
            AcMEDataSynLog.WriteLog("FetchGeneralateLedgerGroup Started");
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ExportMasters.GeneralateLedgerFetchAll, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        resultArgs.DataSource.Table.TableName = "GeneralateLedger";
                        dsMaster.Tables.Add(resultArgs.DataSource.Table);
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("FetchLedgerGroup Ended..");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in FetchLedgerGroup:" + resultArgs.Message);
            }
            return resultArgs;
        }


        /// <summary>
        /// Generalate Map Ledgers 
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchGeneralateLedgerGroupMap()
        {
            AcMEDataSynLog.WriteLog("FetchGeneralateLedgerGroupMap Started");
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ExportMasters.GeneralateLedgerMapAll, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        resultArgs.DataSource.Table.TableName = "CongregationLedgerMap";
                        dsMaster.Tables.Add(resultArgs.DataSource.Table);
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("FetchLedgerGroupMap Ended..");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in FetchLedgerGroupMap:" + resultArgs.Message);
            }
            return resultArgs;
        }

        private ResultArgs FetchPurpose()
        {
            AcMEDataSynLog.WriteLog("FetchPurpose Started..");
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ExportMasters.PurposeFetchAll, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        resultArgs.DataSource.Table.TableName = "Purpose";
                        dsMaster.Tables.Add(resultArgs.DataSource.Table);
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("FetchPurpose Ended..");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in FetchPurpose:" + resultArgs.Message);
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetching the TDS Section Details from Portal
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchTDSSection()
        {
            AcMEDataSynLog.WriteLog("Fetch TDS Sections Started..");
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ExportMasters.TDSSectionFetchAll, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        resultArgs.DataSource.Table.TableName = "TDSSection";
                        dsTDSMasters.Tables.Add(resultArgs.DataSource.Table);
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("Fetch TDS Sections Ended..");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in Fetch TDS Sections:" + resultArgs.Message);
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetch the TDS Nature of Payments from Portal
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchNatureOfPayments()
        {
            AcMEDataSynLog.WriteLog("Fetch TDS Nature Of Payments Started..");
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ExportMasters.TDSNatureOfPaymentsFetchAll, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        resultArgs.DataSource.Table.TableName = "TDSNatureOfPayments";
                        dsTDSMasters.Tables.Add(resultArgs.DataSource.Table);
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("Fetch TDS Nature of Payments Ended..");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in Fetch TDS Nature of Payments:" + resultArgs.Message);
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetching Deductee Type Details from Portal.
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchDeducteeTypes()
        {
            AcMEDataSynLog.WriteLog("Fetch TDS Deductee Types Started..");
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ExportMasters.TDSDeducteeTypesFetchAll, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        resultArgs.DataSource.Table.TableName = "TDSDeducteeTypes";
                        dsTDSMasters.Tables.Add(resultArgs.DataSource.Table);
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("Fetch TDS Deductee Types Ended..");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in Fetch TDS Deductee Types:" + resultArgs.Message);
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetching Duty Tax Details from Portal.
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchDutyTax()
        {
            AcMEDataSynLog.WriteLog("Fetch TDS Duty Tax Started..");
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ExportMasters.TDSDutyTaxFetchAll, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        resultArgs.DataSource.Table.TableName = "TDSDutyTax";
                        dsTDSMasters.Tables.Add(resultArgs.DataSource.Table);
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("Fetch TDS Duty Tax Ended..");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in Fetch TDS Duty Tax:" + resultArgs.Message);
            }
            return resultArgs;
        }

        private ResultArgs FetchTDSPolicyDeductees()
        {
            AcMEDataSynLog.WriteLog("Fetch TDS Policy Deductees Started..");
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ExportMasters.TDSPolicyDeducteesFetchAll, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        resultArgs.DataSource.Table.TableName = "TDSPolicyDeductees";
                        dsTDSMasters.Tables.Add(resultArgs.DataSource.Table);
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("Fetch TDS Policy Deductees Ended..");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in Fetch TDS Policy Deductees :" + resultArgs.Message);
            }
            return resultArgs;
        }

        private ResultArgs FetchTDSPolicy()
        {
            AcMEDataSynLog.WriteLog("Fetch TDS Policy Started..");
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ExportMasters.TDSPolicyFetchAll, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        resultArgs.DataSource.Table.TableName = "TDSPolicy";
                        dsTDSMasters.Tables.Add(resultArgs.DataSource.Table);
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("Fetch TDS Policy Ended..");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in Fetch TDS Duty Tax:" + resultArgs.Message);
            }
            return resultArgs;
        }

        private ResultArgs FetchTaxRate()
        {
            AcMEDataSynLog.WriteLog("Fetch TDS Tax Rate Started..");
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ExportMasters.TDSTaxRateFetchAll, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        resultArgs.DataSource.Table.TableName = "TDSTaxRate";
                        dsTDSMasters.Tables.Add(resultArgs.DataSource.Table);
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("Fetch TDS Tax Rate Ended..");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in Fetch TDS Tax Rate:" + resultArgs.Message);
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetching Budget master from Portal/Branch.
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchBudgetMaster(Int32 BranchId, DateTime datefrom, DateTime dateto, string Projectids, Int32 budgettypeid)
        {
            AcMEDataSynLog.WriteLog("Fetch Budget Master Started..");
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ExportMasters.FetchBudgetMasterByDateRange, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BRANCH_IDColumn, BranchId);
                    dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, datefrom);
                    dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, dateto);
                    dataManager.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, Projectids);
                    dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_TYPE_IDColumn, budgettypeid);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success)
                    {
                        resultArgs.DataSource.Table.TableName = "BudgetMaster";
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("Fetch Budget Master Ended..");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in Fetch Budget Master :" + resultArgs.Message);
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetching Budget project from Portal/Branch.
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchBudgetProject(Int32 BranchId, DateTime datefrom, DateTime dateto, string Projectids, Int32 budgettypeid)
        {
            AcMEDataSynLog.WriteLog("Fetch Budget project Started..");
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ExportMasters.FetchBudgetProjectByDateRange, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BRANCH_IDColumn, BranchId);
                    dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, datefrom);
                    dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, dateto);
                    dataManager.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, Projectids);
                    dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_TYPE_IDColumn, budgettypeid);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success)
                    {
                        resultArgs.DataSource.Table.TableName = "BudgetProject";
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("Fetch Budget Project Ended..");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in Fetch Budget Project :" + resultArgs.Message);
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetching Budget Ledger from Portal/Branch.
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchBudgetLedger(Int32 BranchId, DateTime datefrom, DateTime dateto, string Projectids, Int32 budgettypeid)
        {
            AcMEDataSynLog.WriteLog("Fetch Budget Ledger Started..");
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ExportMasters.FetchBudgetLedgerByDateRange, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BRANCH_IDColumn, BranchId);
                    dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, datefrom);
                    dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, dateto);
                    dataManager.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, Projectids);
                    dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_TYPE_IDColumn, budgettypeid);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success)
                    {
                        resultArgs.DataSource.Table.TableName = "BudgetLedger";
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("Fetch Budget Ledger Ended..");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in Fetch Budget Ledger :" + resultArgs.Message);
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetching Budget Ledger from Portal/Branch.
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchBudgetSubLedger(Int32 BranchId, DateTime datefrom, DateTime dateto, string Projectids, Int32 budgettypeid)
        {
            AcMEDataSynLog.WriteLog("Fetch Budget Sub Ledger Started..");
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ExportMasters.FetchBudgetSubLedgerByDateRange, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BRANCH_IDColumn, BranchId);
                    dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, datefrom);
                    dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, dateto);
                    dataManager.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, Projectids);
                    dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_TYPE_IDColumn, budgettypeid);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success)
                    {
                        resultArgs.DataSource.Table.TableName = "BudgetSubLedger";
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("Fetch Budget Sub Ledger Ended..");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in Fetch Budget Sub Ledger :" + resultArgs.Message);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 22/12/2021, To get Budget Group
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchBudgetGroup()
        {
            AcMEDataSynLog.WriteLog("FetchBudgetGroup Started");
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ExportMasters.BudgetGroupFetchAll, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        resultArgs.DataSource.Table.TableName = "BudgetGroup";
                        dsMaster.Tables.Add(resultArgs.DataSource.Table);
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("FetchBudgetGroup Ended..");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in FetchBudgetGroup:" + resultArgs.Message);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 22/12/2021, To get Budget Sub Group
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchBudgetSubGroup()
        {
            AcMEDataSynLog.WriteLog("FetchBudgetSubGroup Started");
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ExportMasters.BudgetSubGroupFetchAll, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        resultArgs.DataSource.Table.TableName = "BudgetSubGroup";
                        dsMaster.Tables.Add(resultArgs.DataSource.Table);
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("FetchBudgetSubGroup Ended..");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in FetchBudgetSubGroup:" + resultArgs.Message);
            }
            return resultArgs;
        }

    }
}
