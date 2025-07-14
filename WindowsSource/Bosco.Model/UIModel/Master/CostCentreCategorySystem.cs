using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO.Schema;
using Bosco.DAO.Data;

namespace Bosco.Model.UIModel
{
    public class CostCentreCategorySystem : SystemBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public CostCentreCategorySystem()
        {
        }
        public CostCentreCategorySystem(int CostCentreCategoryId)
        {
            FillCostCentreCategoryDetails(CostCentreCategoryId);
        }
        #endregion

        #region CostCentreCatogoryProperties
        public int CostCentreCategoryId { get; set; }
        public string CostCentreCategoryName { get; set; }
        #endregion

        #region Methods
        public ResultArgs FetchCostCentreCatogoryDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CostCentreCategory.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteCostCentreCatogoryDetails(int CostCategoryid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CostCentreCategory.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.CostCentreCategory.COST_CENTRECATEGORY_IDColumn.ColumnName, CostCategoryid);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveCostCentreCatogoryDetails()
        {
            using (DataManager dataManager = new DataManager((CostCentreCategoryId == 0) ? SQLCommand.CostCentreCategory.Add : SQLCommand.CostCentreCategory.Update))
            {
                dataManager.Parameters.Add(this.AppSchema.CostCentreCategory.COST_CENTRECATEGORY_IDColumn, CostCentreCategoryId, true);
                dataManager.Parameters.Add(this.AppSchema.CostCentreCategory.COST_CENTRE_CATEGORY_NAMEColumn, CostCentreCategoryName);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FillCostCentreCategoryDetails(int CostCentreCategoryId)
        {
            resultArgs = CostCentreCatogoryDetailsById(CostCentreCategoryId);
            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                CostCentreCategoryName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.CostCentreCategory.COST_CENTRE_CATEGORY_NAMEColumn.ColumnName].ToString();
            }
            return resultArgs;
        }

        public ResultArgs CostCentreCatogoryDetailsById(int CostcentreCategoryId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CostCentreCategory.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.CostCentreCategory.COST_CENTRECATEGORY_IDColumn.ColumnName, CostcentreCategoryId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int CheckCostcentreCostcategoryExist(int CostcategoryId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CostCentreCategory.CheckCostcentreCostCategoryExists))
            {
                dataManager.Parameters.Add(this.AppSchema.CostCentreCategory.COST_CENTRECATEGORY_IDColumn, CostcategoryId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs IsCostCentreCategoryExists()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CostCentreCategory.FetchCostCentreCategoryId))
            {
                dataManager.Parameters.Add(this.AppSchema.CostCentreCategory.COST_CENTRE_CATEGORY_NAMEColumn, CostCentreCategoryName);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }
        #endregion

    }
}
