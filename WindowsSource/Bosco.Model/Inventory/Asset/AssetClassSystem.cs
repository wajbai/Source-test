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
    public class AssetClassSystem : SystemBase
    {
        #region Variable Declearation
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public AssetClassSystem()
        {

        }
        #endregion

        #region Properties
        public int AssetClassId
        {
            get;
            set;
        }

        public double Depreciation
        {
            get;
            set;
        }

        public string AssetClass
        {
            get;
            set;
        }

        public int ParentClassId
        {
            get;
            set;
        }

        public string ClassId
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

        public ResultArgs FetchClassDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetClass.FetchAll))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchSelectedClassDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetClass.FetchSelectedClass))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETClassDetails.ASSET_CLASS_IDColumn, ClassId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveClassDetails()
        {
            using (DataManager datamanager = new DataManager((AssetClassId == 0) ? SQLCommand.AssetClass.Add : SQLCommand.AssetClass.Update))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETClassDetails.ASSET_CLASS_IDColumn, AssetClassId, true);
                datamanager.Parameters.Add(this.AppSchema.ASSETClassDetails.ASSET_CLASSColumn, AssetClass);
                datamanager.Parameters.Add(this.AppSchema.ASSETClassDetails.PARENT_CLASS_IDColumn, ParentClassId);
                datamanager.Parameters.Add(this.AppSchema.ASSETClassDetails.METHOD_IDColumn, Method);
                datamanager.Parameters.Add(this.AppSchema.ASSETClassDetails.DEP_PERCENTAGEColumn, Depreciation);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteClassDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetClass.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETClassDetails.ASSET_CLASS_IDColumn, ClassId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        public ResultArgs DeleteSubClassDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetClass.DeleteClass))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETClassDetails.ASSET_CLASS_IDColumn, ClassId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        public void AssignClassProperties()
        {
            resultArgs = FetchClassDetailsById();
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                AssetClass = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETClassDetails.ASSET_CLASSColumn.ColumnName].ToString();
                ParentClassId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETClassDetails.PARENT_CLASS_IDColumn.ColumnName].ToString());
                Method = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETClassDetails.METHOD_IDColumn.ColumnName].ToString());
                Depreciation = NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETClassDetails.DEP_PERCENTAGEColumn.ColumnName].ToString());
            }
        }

        public ResultArgs FetchClassDetailsById()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetClass.FetchbyID))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETClassDetails.ASSET_CLASS_IDColumn, AssetClassId);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int FetchAssetClassNameById()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetClass.FetchAssetClassNameByID))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETClassDetails.ASSET_CLASSColumn, AssetClass);
                resultArgs = datamanager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs DeleteAll()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetClass.DeleteAssetDetails))
            {
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public int FetchParentClassId()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetClass.FetchAssetParentClassNameByID))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETClassDetails.ASSET_CLASSColumn, this.AssetClass);
                resultArgs = datamanager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int FetchAccessClassId()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetClass.FetchAccessFlagClassId))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETClassDetails.ASSET_CLASS_IDColumn, AssetClassId);
                resultArgs = datamanager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs FetchParentClassName()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetClass.FetchClassNameByParentID))
            {
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        #endregion
    }
}
