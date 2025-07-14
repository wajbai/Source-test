using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using Bosco.Model.Setting;

namespace Bosco.Model.UIModel
{
    public class CostCentreSystem : SystemBase
    {
        #region Variable Decelaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public CostCentreSystem()
        {
        }

        public CostCentreSystem(int CostCentreId)
        {
            FillCostCentreProperties(CostCentreId);
        }
        #endregion

        #region Cost Centre Properties
        public int CostCentreId { get; set; }
        public string CostCentreAbbrevation { get; set; }
        public string CostCentreName { get; set; }
        public string CostCentreCategoryName { get; set; }
        public string Notes { get; set; }
        public int ProjectId { get; set; }
        public string ProjectIds { get; set; }
        public int LedgerId { get; set; }
        public int MapCostCentreId { get; set; }
        public int CostCategoryId { get; set; }
        public DataTable dtMapCostCentre { get; set; }

        public double CCAmount { get; set; }
        public string CCTransMode { get; set; }
        #endregion

        #region Methods
        public ResultArgs FetchCostCentreDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CostCentre.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchforLookUpDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CostCentre.FetchforLookupByProjectLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                //On 23/11/2022, Load Cost Centre based on Ledgers
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        public ResultArgs FetchforLookUpDetailsByProjectIds()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CostCentre.FetchforLookupByProjectLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectIds);
                //On 23/11/2022, Load Cost Centre based on Ledgers
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 17/05/2023, To load only Project mapped cost centre alone
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchMappedProjectCostCentre()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CostCentre.FetchforLookupByProjectLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectIds);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchforLookUpDetailsByProject()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CostCentre.FetchforLookupByProject))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteCostCentreDetails(int CostCentreId)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.CostCentre.Delete))
            {
                dataMember.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, CostCentreId);
                resultArgs = dataMember.UpdateData(dataMember, "", SQLType.SQLStatic);
            }
            return resultArgs;
        }

        public ResultArgs SaveCostCentre()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = SaveCostCentreDetails(dataManager);
                CostCentreId = CostCentreId.Equals(0) ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : CostCentreId;
                if (resultArgs.Success)
                {
                    resultArgs = MappCostCentre(dataManager);
                    if (resultArgs.Success)
                        SaveCostcentreCostCategory(CostCentreId);
                }

                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        /// <summary>
        /// Save Cost centre
        /// </summary>
        /// <param name="dm"></param>
        /// <returns></returns>
        public ResultArgs IndividualSaveCostCentre()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CostCentre.Add))
            {
                dataManager.Parameters.Add(this.AppSchema.CostCentre.ABBREVATIONColumn, CostCentreAbbrevation);
                dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_NAMEColumn, CostCentreName);
                dataManager.Parameters.Add(this.AppSchema.CostCentre.NOTESColumn, string.Empty);
                resultArgs = dataManager.UpdateData();
                if (resultArgs != null && resultArgs.Success)
                {
                    resultArgs = IsCostCentreExists();
                    if (resultArgs.DataSource.Sclar.ToInteger != 0)
                    {
                        CostCentreId = resultArgs.DataSource.Sclar.ToInteger;
                        resultArgs = CheckCostCentreMapped();
                        if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Sclar.ToInteger == 0)
                        {
                            resultArgs = MapProjectWithCostCentre();
                        }
                    }
                }
            }

            return resultArgs;
        }

        ///// <summary>
        ///// Delete cost centre based on the project id in project_costcentre Table
        ///// </summary>
        ///// <param name="dataManagerCostCentre"></param>
        ///// <returns></returns>
        //public ResultArgs DeleteCCMappingProject()
        //{
        //    using (DataManager dataManager = new DataManager(SQLCommand.Mapping.UnMapCostCentreByCCId))
        //    {
        //        dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, CostCentreId);
        //        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
        //        resultArgs = dataManager.UpdateData();
        //    }
        //    return resultArgs;
        //}

        public ResultArgs CheckCostCentreMapped()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.CheckCostCentreMapped))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, CostCentreId);

                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        /// <summary>
        /// Map Project with cost centres
        /// </summary>
        /// <param name="dataManagerCostCentreMapping"></param>
        /// <returns></returns>
        public ResultArgs MapProjectWithCostCentre()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectCostCentreMappingAdd))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, CostCentreId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.AMOUNTColumn, CCAmount);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.TRANS_MODEColumn, CCTransMode);
                resultArgs = dataManager.UpdateData();
                if (resultArgs.Success)
                {
                    resultArgs = SaveCostcentreCostCategory(CostCentreId);
                }
            }
            return resultArgs;
        }


        /// <summary>
        /// Map Project with cost centres
        /// </summary>
        /// <param name="dataManagerCostCentreMapping"></param>
        /// <returns></returns>
        public ResultArgs UpdateMapProjectWithCostCentre()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.UpdateProjectCostCentreOPBalance))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, CostCentreId);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.AMOUNTColumn, CCAmount);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.TRANS_MODEColumn, CCTransMode);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveCostCentreDetails(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager((CostCentreId == 0) ? SQLCommand.CostCentre.Add : SQLCommand.CostCentre.Update))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.CostCentre.ABBREVATIONColumn, CostCentreAbbrevation);
                dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_NAMEColumn, CostCentreName);
                dataManager.Parameters.Add(this.AppSchema.CostCentre.NOTESColumn, Notes);
                dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, CostCentreId);
                resultArgs = dataManager.UpdateData(dataManager, "", SQLType.SQLStatic);
            }
            return resultArgs;
        }

        public ResultArgs SaveCostcentreCostCategory(int costcenId)
        {
            resultArgs = Deletecostcategory(costcenId);
            if (resultArgs.Success)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Mapping.CostcentreCostCategoryMappingAdd))
                {
                    dataManager.Parameters.Add(this.AppSchema.CostCentreCategory.COST_CENTRECATEGORY_IDColumn, CostCategoryId);
                    dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, costcenId);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.UpdateData();
                }
            }
            return resultArgs;
        }
        public ResultArgs Deletecostcategory(int ccentreId)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.Mapping.DeleteCostCategory))
            {
                datamanager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, ccentreId);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }
        private ResultArgs MappCostCentre(DataManager dataManager)
        {
            using (MappingSystem mappingSystem = new MappingSystem())
            {
                mappingSystem.CostCenterId = MapCostCentreId.Equals(0) ? NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : MapCostCentreId;
                mappingSystem.LedgerId = 0;
                mappingSystem.dtCostCenterIDCollection = dtMapCostCentre;
                resultArgs = mappingSystem.AccountMappingCostCenterByCCId(dataManager);
            }
            return resultArgs;
        }

        public void FillCostCentreProperties(int CostCenterId)
        {
            resultArgs = FetchCostCategoryById(CostCenterId);
            if (resultArgs.Success)
            {
                // resultArgs = FetchCostCategoryById(CostCenterId);
                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    CostCentreAbbrevation = resultArgs.DataSource.Table.Rows[0][this.AppSchema.CostCentre.ABBREVATIONColumn.ColumnName].ToString();
                    CostCentreName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.CostCentre.COST_CENTRE_NAMEColumn.ColumnName].ToString();
                    Notes = resultArgs.DataSource.Table.Rows[0][this.AppSchema.CostCentre.NOTESColumn.ColumnName].ToString();
                    CostCentreCategoryName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.CostCentreCategory.COST_CENTRE_CATEGORY_NAMEColumn.ColumnName].ToString();
                    CostCategoryId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.CostCentreCategory.COST_CENTRECATEGORY_IDColumn.ColumnName].ToString());
                }
            }
        }

        private ResultArgs FetchCostCentreDetailsById(int CostCenterId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CostCentre.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, CostCenterId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        private ResultArgs FetchCostCategoryById(int CCategoryId)
        {
            using (DataManager datamanger = new DataManager(SQLCommand.CostCentre.FetchCostCentreCategorybyId))
            {
                datamanger.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, CCategoryId);
                resultArgs = datamanger.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchCostCentreCodes()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CostCentre.FetchCostCentreCodes))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchCostcentreBycostcentrecode(string CostcentreCode)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CostCentre.FetchCostcentreByExistingCode))
            {
                dataManager.Parameters.Add(this.AppSchema.CostCentre.ABBREVATIONColumn, CostcentreCode);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchCostCentreCategory()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CostCentre.FetchCostCentreCategory))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs IsCostCentreExists()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CostCentre.FetchCostCentreId))
            {
                dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_NAMEColumn, CostCentreName);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }



        #endregion
    }
}
