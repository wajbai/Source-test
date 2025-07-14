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
   public class AssetDepreciationSystem : SystemBase
    {
       #region Variable Decelaration
       ResultArgs resultArgs = null;
       #endregion

       #region constructor
       public AssetDepreciationSystem()
       {
       }
       public AssetDepreciationSystem(int Depreciation_Id)
       {
           DepreciationId = Depreciation_Id;
           FillDepreciationProperties(Depreciation_Id);
       }
        #endregion

       #region Depreciation Properties
       public int DepreciationId { get; set; }
       public string DepreciationCode { get; set; }
       public string Name { get; set; }
       public string Description { get; set; }

      #endregion

       #region Methods

       public ResultArgs SaveDepreciationDetials()
       {
           using (DataManager datamanager = new DataManager((DepreciationId == 0) ? SQLCommand.AssetDepreciation.Add : SQLCommand.AssetDepreciation.Update))
           {
               datamanager.Parameters.Add(this.AppSchema.ASSETDepreciationDetails.DEP_IDColumn, DepreciationId);

               datamanager.Parameters.Add(this.AppSchema.ASSETDepreciationDetails.NAMEColumn, Name);
               datamanager.Parameters.Add(this.AppSchema.ASSETDepreciationDetails.DESCRIPTIONColumn, Description);
               resultArgs = datamanager.UpdateData();

           }
           return resultArgs;
       }

       public void FillDepreciationProperties(int DepreciationId)
       {
           resultArgs = FetchById(DepreciationId);
           if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
           {
               Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETDepreciationDetails.NAMEColumn.ColumnName].ToString();
               Description = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETDepreciationDetails.DESCRIPTIONColumn.ColumnName].ToString();

           }

       }

       public ResultArgs FetchAll()
       {
           using (DataManager datamanager = new DataManager(SQLCommand.AssetDepreciation.FetchAll))
           {
               resultArgs = datamanager.FetchData(DataSource.DataTable);
           }
           return resultArgs;
       }

       public ResultArgs DeleteDepreciation(int DepreciationId)
       {
           using (DataManager datamanager = new DataManager(SQLCommand.AssetDepreciation.Delete))
           {
               datamanager.Parameters.Add(this.AppSchema.ASSETDepreciationDetails.DEP_IDColumn, DepreciationId);
               resultArgs = datamanager.UpdateData();
           }
           return resultArgs;
       }

       private ResultArgs FetchById(int Depreciation_Id)
       {
           using (DataManager datamanager = new DataManager(SQLCommand.AssetDepreciation.Fetch))
           {
               datamanager.Parameters.Add(this.AppSchema.ASSETDepreciationDetails.DEP_IDColumn, Depreciation_Id);
               resultArgs = datamanager.FetchData(DataSource.DataTable);

           }
           return resultArgs;
       }

       #endregion

      

    }
}
