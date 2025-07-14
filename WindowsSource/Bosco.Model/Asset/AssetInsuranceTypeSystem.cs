using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;

namespace Bosco.Model.Asset
{
   public  class AssetInsuranceTypeSystem : SystemBase
   {
       #region Variable Decelaration
       ResultArgs resultArgs = new ResultArgs();
       #endregion

       #region Constructors
       public AssetInsuranceTypeSystem()
       {

       }
       public AssetInsuranceTypeSystem(int Insurance_Id)
       {
           InsuranceId = Insurance_Id;
           FillInsuranceProperties(Insurance_Id);
       }
       #endregion
       
       #region Properties
       public int InsuranceId { get; set; }
       public string Name { get; set; }
       public string Description { get; set; }
       #endregion

       #region Methods

       public ResultArgs SaveInsuranceDetials()
       {
           using (DataManager datamanager = new DataManager((InsuranceId == 0) ? SQLCommand.AssetInsurance.Add : SQLCommand.AssetInsurance.Update))
           {
               datamanager.Parameters.Add(this.AppSchema.ASSETInsuranceDetails.INSURANCE_IDColumn, InsuranceId);
               datamanager.Parameters.Add(this.AppSchema.ASSETInsuranceDetails.NAMEColumn, Name);
               datamanager.Parameters.Add(this.AppSchema.ASSETInsuranceDetails.DESCRIPTIONColumn, Description);
               resultArgs = datamanager.UpdateData();
           }
           return resultArgs;
       }

       public void FillInsuranceProperties(int InsuranceId)
       {
           resultArgs = FetchInsuranceDetialsById(InsuranceId);
           if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
           {
               Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETInsuranceDetails.NAMEColumn.ColumnName].ToString();
               Description = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETInsuranceDetails.DESCRIPTIONColumn.ColumnName].ToString();
           }
       }

       private ResultArgs FetchInsuranceDetialsById(int Insurance_Id)
       {
           using (DataManager datamanager = new DataManager(SQLCommand.AssetInsurance.Fetch))
           {
               datamanager.Parameters.Add(this.AppSchema.ASSETInsuranceDetails.INSURANCE_IDColumn, Insurance_Id);
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

       public ResultArgs DeleteInsuranceDetails(int InsuranceId)
       {
           using (DataManager datamanager = new DataManager(SQLCommand.AssetInsurance.Delete))
           {
               datamanager.Parameters.Add(this.AppSchema.ASSETInsuranceDetails.INSURANCE_IDColumn, InsuranceId);
               resultArgs = datamanager.UpdateData();
           }
           return resultArgs;
       }

       #endregion
   }
}
