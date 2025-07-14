using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using System.Data;

namespace Bosco.Model
{
    public class StockCategorySystem : SystemBase, AssetStockProduct.ICategory
    {
        #region Variable Declearation
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public StockCategorySystem()
        {

        }
        #endregion

        #region Properties
        public int CategoryId
        {
            get;
            set;
        }

        public string GroupIds
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
        #endregion

        #region Methods
        public ResultArgs FetchCategoryDetails()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.StockCategory.FetchAll))
            {
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveCategoryDetails()
        {
            using (DataManager dataManager = new DataManager((this.CategoryId == 0) ? SQLCommand.StockCategory.Add : SQLCommand.StockCategory.Update))
            {
                dataManager.Parameters.Add(this.AppSchema.StockCategory.CATEGORY_IDColumn, CategoryId, true);
                dataManager.Parameters.Add(this.AppSchema.StockCategory.PARENT_CATEGORY_IDColumn, CategoryParentId);
                dataManager.Parameters.Add(this.AppSchema.StockCategory.IMAGE_IDColumn, ImageId);
                dataManager.Parameters.Add(this.AppSchema.StockCategory.CATEGORY_NAMEColumn, Name);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteCategoryDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockCategory.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.StockCategory.CATEGORY_IDColumn, CategoryId);
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
                Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockCategory.CATEGORY_NAMEColumn.ColumnName].ToString();
                CategoryParentId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockCategory.PARENT_CATEGORY_IDColumn.ColumnName].ToString());
            }
        }

        public ResultArgs fetchCategoryById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockCategory.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.StockCategory.CATEGORY_IDColumn, CategoryId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchSelectedCategoryDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockCategory.FetchSelectedCategoryDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.StockCategory.CATEGORY_IDColumn, CategoryId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        //public DataSet FetchAssetItemMasterDetail()
        //{
        //    string VoucherID = string.Empty;
        //    DataSet dsItem = new DataSet();
        //    try
        //    {
        //        resultArgs = FetchSelectedCategoryDetails();
        //        if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
        //        {
        //            resultArgs.DataSource.Table.TableName = "Master";
        //            dsItem.Tables.Add(resultArgs.DataSource.Table);

        //            resultArgs = FetchAssetItemDetailAll();
        //            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
        //            {
        //                resultArgs.DataSource.Table.TableName = "AssetItemDetail";
        //                dsItem.Tables.Add(resultArgs.DataSource.Table);
        //                dsItem.Relations.Add(dsItem.Tables[1].TableName, dsItem.Tables[0].Columns[this.AppSchema.ASSETItem.ITEM_IDColumn.ColumnName], dsItem.Tables[1].Columns[this.AppSchema.ASSETItem.ITEM_IDColumn.ColumnName]);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.Message, false);
        //    }
        //    finally
        //    {
        //    }
        //    return dsItem;
        //}

        //public ResultArgs FetchAssetItemDetailAll()
        //{
        //    using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchAssetItemDetailAll))
        //    {
        //        resultArgs = dataManager.FetchData(DataSource.DataTable);
        //    }
        //    return resultArgs;
        //}

        #endregion
    }
}
