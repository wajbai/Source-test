using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using System.Data;
using Bosco.Utility;

namespace Bosco.Model.Stock
{
    public class StockGroupSystem : SystemBase, AssetStockProduct.IGroup
    {
        #region Variable Declearation
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public StockGroupSystem()
        {

        }
        #endregion

        #region Properties
        public int GroupId
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public int ParentGroupId
        {
            get;
            set;
        }

        public int Method
        {
            get;
            set;
        }

        public double Depreciation
        {
            get;
            set;
        }

        public string GroupIds
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
        public ResultArgs FetchGroupDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockGroup.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchSelectedGroupDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockGroup.FetchSelectedGroups))
            {
                dataManager.Parameters.Add(this.AppSchema.StockGroup.GROUP_IDColumn, GroupIds);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveGroupDetails()
        {
            using (DataManager datamanager = new DataManager((GroupId == 0) ? SQLCommand.StockGroup.Add : SQLCommand.StockGroup.Update))
            {
                datamanager.Parameters.Add(this.AppSchema.StockGroup.GROUP_IDColumn, GroupId, true);
                datamanager.Parameters.Add(this.AppSchema.StockGroup.NAMEColumn, Name);
                datamanager.Parameters.Add(this.AppSchema.StockGroup.PARENT_GROUP_IDColumn, ParentGroupId);
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteGroupDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockGroup.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.StockGroup.GROUP_IDColumn, GroupId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public void AssignGroupProperties()
        {
            resultArgs = FetchById();
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockGroup.NAMEColumn.ColumnName].ToString();
                ParentGroupId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockGroup.PARENT_GROUP_IDColumn.ColumnName].ToString());
                if (resultArgs.DataSource.Table.Columns.Contains(this.AppSchema.ASSETGroupDetails.DEP_IDColumn.ColumnName))
                {
                    Method = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETGroupDetails.DEP_IDColumn.ColumnName].ToString());
                }
                if (resultArgs.DataSource.Table.Columns.Contains(this.AppSchema.ASSETGroupDetails.DEP_PERCENTAGEColumn.ColumnName))
                {
                    Depreciation = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETGroupDetails.DEP_PERCENTAGEColumn.ColumnName].ToString());
                }
            }
        }

        private ResultArgs FetchById()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.StockGroup.FetchbyID))
            {
                datamanager.Parameters.Add(this.AppSchema.StockGroup.GROUP_IDColumn, GroupId);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        #endregion




    }
}
