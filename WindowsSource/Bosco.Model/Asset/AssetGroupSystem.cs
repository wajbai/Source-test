using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using System.Data;
using Bosco.Utility;

namespace Bosco.Model.Asset
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

        public int ImageId
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
                datamanager.Parameters.Add(this.AppSchema.ASSETGroupDetails.NAMEColumn, Name);
                datamanager.Parameters.Add(this.AppSchema.ASSETGroupDetails.PARENT_GROUP_IDColumn, ParentGroupId);
                datamanager.Parameters.Add(this.AppSchema.ASSETGroupDetails.DEP_IDColumn, Method);
                datamanager.Parameters.Add(this.AppSchema.ASSETGroupDetails.DEP_PERCENTAGEColumn, Depreciation);
                datamanager.Parameters.Add(this.AppSchema.ASSETGroupDetails.IMAGE_IDColumn, ImageId);
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
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
                Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETGroupDetails.NAMEColumn.ColumnName].ToString();
                ParentGroupId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETGroupDetails.PARENT_GROUP_IDColumn.ColumnName].ToString());
                Method = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETGroupDetails.DEP_IDColumn.ColumnName].ToString());
                Depreciation = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETGroupDetails.DEP_PERCENTAGEColumn.ColumnName].ToString());
                ImageId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETGroupDetails.IMAGE_IDColumn.ColumnName].ToString());
            }
        }

        private ResultArgs FetchGroupDetailsById()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetGroup.FetchbyID))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETGroupDetails.GROUP_IDColumn, GroupId);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion



    }
}
