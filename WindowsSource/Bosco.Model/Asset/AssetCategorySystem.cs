using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.Model.Asset
{
    public class AssetCategorySystem : SystemBase, AssetStockProduct.ICategory
    {
        #region Variable Declearation
        ResultArgs resultArgs = null;
        # endregion

        #region Properties

        public int CategoryId
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public int CategoryParentId
        {
            get;
            set;
        }

        public int ImageId
        {
            get;
            set;
        }

        public string GroupIds
        {
            get;
            set;
        }

        #endregion

        #region Constructor
        public AssetCategorySystem()
        {

        }
        #endregion 

        #region Methods
        
        public ResultArgs FetchCategoryDetails()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetCategory.FetchAll))
            {
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveCategoryDetails()
        {
            using (DataManager dataManager = new DataManager((this.CategoryId == 0) ? SQLCommand.AssetCategory.Add : SQLCommand.AssetCategory.Update))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETCategory.CATEGORY_IDColumn, CategoryId, true);
                dataManager.Parameters.Add(this.AppSchema.ASSETCategory.PARENT_CATEGORY_IDColumn, CategoryParentId);
                dataManager.Parameters.Add(this.AppSchema.ASSETCategory.IMAGE_IDColumn, ImageId);
                dataManager.Parameters.Add(this.AppSchema.ASSETCategory.NAMEColumn, Name);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteCategoryDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetCategory.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETCategory.CATEGORY_IDColumn, CategoryId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public void FillCategoryProperties()
        {
            resultArgs = fetchCategoryById();
            if (resultArgs.DataSource.Table.Rows.Count > 0)
            {
                Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETCategory.NAMEColumn.ColumnName].ToString();
                CategoryParentId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETCategory.PARENT_CATEGORY_IDColumn.ColumnName].ToString());
            }
        }

        private ResultArgs fetchCategoryById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetCategory.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETCategory.CATEGORY_IDColumn, CategoryId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchSelectedCategoryDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetCategory.FetchSelectedGroups))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETCategory.CATEGORY_IDColumn, GroupIds);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        #endregion
    }
}
