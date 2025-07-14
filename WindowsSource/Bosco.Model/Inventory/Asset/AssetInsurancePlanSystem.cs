using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;

namespace Bosco.Model
{
    public class AssetInsurancePlanSystem : SystemBase
    {
        #region Variable Decelaration
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Constructors
        public AssetInsurancePlanSystem()
        {

        }
        public AssetInsurancePlanSystem(int insuranceTypeId)
        {
            InsuranceTypeId = insuranceTypeId;
            FillInsuranceProperties(InsuranceTypeId);
        }
        #endregion

        #region Properties
        public int InsuranceTypeId { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        #endregion

        #region Methods

        public ResultArgs SaveInsuranceDetials()
        {
            using (DataManager datamanager = new DataManager((InsuranceTypeId == 0) ? SQLCommand.AssetInsurance.Add : SQLCommand.AssetInsurance.Update))
            {
                datamanager.Parameters.Add(this.AppSchema.InsurancePlan.INSURANCE_PLAN_IDColumn, InsuranceTypeId,true);
                datamanager.Parameters.Add(this.AppSchema.InsurancePlan.INSURANCE_PLANColumn, Name);
                datamanager.Parameters.Add(this.AppSchema.InsurancePlan.COMPANYColumn, Company);
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public void FillInsuranceProperties(int InsuranceId)
        {
            resultArgs = FetchInsuranceDetialsById(InsuranceId);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.InsurancePlan.INSURANCE_PLANColumn.ColumnName].ToString();
                Company = resultArgs.DataSource.Table.Rows[0][this.AppSchema.InsurancePlan.COMPANYColumn.ColumnName].ToString();
            }
        }

        private ResultArgs FetchInsuranceDetialsById(int InsuranceTypeId)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetInsurance.Fetch))
            {
                datamanager.Parameters.Add(this.AppSchema.InsurancePlan.INSURANCE_PLAN_IDColumn, InsuranceTypeId);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAllInsuranceDetials()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInsurance.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchInsuranceDetailsForAutoFetch()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInsurance.FetchInsuranceDetails))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteInsuranceDetails(int InsuranceTypeId)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetInsurance.Delete))
            {
                datamanager.Parameters.Add(this.AppSchema.InsurancePlan.INSURANCE_PLAN_IDColumn, InsuranceTypeId);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs AutoFetchInsurancePlans()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInsurance.AutoFetchInsurance))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs LoadInsurance()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInsurance.GetInsuranceType))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        #endregion
    }
}
