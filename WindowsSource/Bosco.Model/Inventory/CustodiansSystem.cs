using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.Model.Inventory
{
    public class CustodiansSystem : SystemBase
    {
        #region Variable Declearation
        ResultArgs resultArgs = null;
        # endregion

        #region Constructor
        //public CustodiansSystem(int custodiansId)
        //{
        //    this.CustodiansId = custodiansId;
        //}
        #endregion

        #region Properties

        public int CustodiansId
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Role
        {
            get;
            set;
        }

        public int LocationID
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public ResultArgs SaveCustodiansDetails()
        {
            using (DataManager dataManager = new DataManager((CustodiansId == 0) ? SQLCommand.AssetCustodians.Add : SQLCommand.AssetCustodians.Update))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetCustodians.CUSTODIAN_IDColumn, CustodiansId, true);
                dataManager.Parameters.Add(this.AppSchema.AssetCustodians.CUSTODIANColumn, Name);
                dataManager.Parameters.Add(this.AppSchema.AssetCustodians.ROLEColumn, Role);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchAllCustodiansDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetCustodians.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchCustodiansDetailsById(int custodiansId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetCustodians.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetCustodians.CUSTODIAN_IDColumn, custodiansId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteCustodiansDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetCustodians.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetCustodians.CUSTODIAN_IDColumn, CustodiansId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public void AssignToProperties(int custodiandId)
        {
            resultArgs = FetchCustodiansDetailsById(custodiandId);
            if (resultArgs.Success && resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetCustodians.CUSTODIANColumn.ColumnName].ToString();
                Role = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetCustodians.ROLEColumn.ColumnName].ToString();
            }
        }

        public ResultArgs DeleteCutodianDetail()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetCustodians.DeleteCustudianDetails))
            {
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public int FetchCustodiansNameById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetCustodians.FetchCustodianNameByID))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetCustodians.CUSTODIANColumn, Name);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        public ResultArgs FetchCustodianNameByLocationID()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetCustodians.FetchCustodianNameyByLocationID))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATION_IDColumn, LocationID);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int FetchMappedCustodian()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetCustodians.FetchMappedCustodian))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetCustodians.CUSTODIAN_IDColumn, CustodiansId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        #endregion
    }
}
