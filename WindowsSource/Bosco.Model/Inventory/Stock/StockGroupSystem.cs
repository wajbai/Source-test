using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using System.Data;
using Bosco.Utility;

namespace Bosco.Model
{
    public class StockGroupSystem : SystemBase
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
        public int StockGroupId
        {
            get;
            set;
        }

        public string StockGroup
        {
            get;
            set;
        }

        public int ParentGroupId
        {
            get;
            set;
        }

        public string GroupId
        {
            get;
            set;
        }

        //public int Method
        //{
        //    get;
        //    set;
        //}
        #endregion

        #region Methods

        public ResultArgs FetchStockGroupDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockGroup.FetchAll))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchSelectedGroupDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockGroup.FetchSelectedGroups))
            {
                dataManager.Parameters.Add(this.AppSchema.StockGroup.GROUP_IDColumn, GroupId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveGroupDetails()
        {
            using (DataManager datamanager = new DataManager((StockGroupId == 0) ? SQLCommand.StockGroup.Add : SQLCommand.StockGroup.Update))
            {
                datamanager.Parameters.Add(this.AppSchema.StockGroup.GROUP_IDColumn, StockGroupId, true);
                datamanager.Parameters.Add(this.AppSchema.StockGroup.GROUP_NAMEColumn, StockGroup);
                datamanager.Parameters.Add(this.AppSchema.StockGroup.PARENT_GROUP_IDColumn, ParentGroupId);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteGroupDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockGroup.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.StockGroup.GROUP_IDColumn, StockGroupId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public void AssignGroupProperties()
        {
            resultArgs = FetchGroupDetailsById();
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                StockGroup = resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockGroup.GROUP_NAMEColumn.ColumnName].ToString();
                ParentGroupId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockGroup.PARENT_GROUP_IDColumn.ColumnName].ToString());
            }
        }

        public ResultArgs FetchGroupDetailsById()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.StockGroup.FetchbyID))
            {
                datamanager.Parameters.Add(this.AppSchema.StockGroup.GROUP_IDColumn, StockGroupId);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int FetchAssetClassNameById()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.StockGroup.FetchGroupNameByParentID))
            {
                datamanager.Parameters.Add(this.AppSchema.StockGroup.GROUP_NAMEColumn, StockGroup);
                resultArgs = datamanager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs DeleteAll()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.StockGroup.DeleteStockGroupDetails))
            {
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public int FetchParentClassId()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetClass.FetchAssetParentClassNameByID))
            {
                datamanager.Parameters.Add(this.AppSchema.StockGroup.GROUP_NAMEColumn, this.StockGroup);
                resultArgs = datamanager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs FetchParentGroupName()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.StockGroup.FetchGroupNameByParentID))
            {
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        #endregion
        
    }
}
