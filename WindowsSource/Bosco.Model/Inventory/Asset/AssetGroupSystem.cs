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
    public class AssetGroupSystem : SystemBase, AssetStockProduct.IGroup
    {
        #region Variable Declearation
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public AssetGroupSystem()
        {

        }
        #endregion

        #region Properties
        public int GroupId
        {
            get;
            set;
        }

        public double Depreciation
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

        public string GroupIds
        {
            get;
            set;
        }

        public int Method
        {
            get;
            set;
        }   
        #endregion

        #region Methods

        public ResultArgs FetchGroupDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetGroup.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchSelectedGroupDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetGroup.FetchSelectedGroups))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETGroupDetails.GROUP_IDColumn, GroupIds);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveGroupDetails()
        {
            using (DataManager datamanager = new DataManager((GroupId == 0) ? SQLCommand.AssetGroup.Add : SQLCommand.AssetGroup.Update))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETGroupDetails.GROUP_IDColumn, GroupId, true);
                datamanager.Parameters.Add(this.AppSchema.ASSETGroupDetails.GROUP_NAMEColumn, Name);
                datamanager.Parameters.Add(this.AppSchema.ASSETGroupDetails.PARENT_GROUP_IDColumn, ParentGroupId);
                datamanager.Parameters.Add(this.AppSchema.ASSETGroupDetails.METHOD_IDColumn, Method);
                datamanager.Parameters.Add(this.AppSchema.ASSETGroupDetails.DEP_PERCENTAGEColumn, Depreciation);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteGroupDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetGroup.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETGroupDetails.GROUP_IDColumn, GroupId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public void AssignGroupProperties()
        {
            resultArgs = FetchGroupDetailsById();
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETGroupDetails.GROUP_NAMEColumn.ColumnName].ToString();
                ParentGroupId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETGroupDetails.PARENT_GROUP_IDColumn.ColumnName].ToString());
                Method = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETGroupDetails.METHOD_IDColumn.ColumnName].ToString());
                Depreciation = NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETGroupDetails.DEP_PERCENTAGEColumn.ColumnName].ToString());
            }
        }

        public ResultArgs FetchGroupDetailsById()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetGroup.FetchbyID))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETGroupDetails.GROUP_IDColumn, GroupId);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int FetchAssetGroupNameById()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetGroup.FetchAssetGroupNameByID))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETGroupDetails.GROUP_NAMEColumn,Name);
                resultArgs = datamanager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs DeleteAll()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetGroup.DeleteAssetDetails))
            {
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public int FetchGroupId()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetGroup.FetchAssetGroupNameByID))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETGroupDetails.GROUP_NAMEColumn,Name);
                resultArgs = datamanager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int FetchParentGroupId()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetGroup.FetchAssetParentGroupNameByID))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETGroupDetails.GROUP_NAMEColumn,this.Name);
                resultArgs = datamanager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        public ResultArgs FetchParentGroupName()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetGroup.FetchGroupNameByParentID))
            {
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion
    }
}
