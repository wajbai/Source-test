using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.Model.Donor
{
   public class DonorRegistrationTypeSystem:SystemBase
   {
       #region Constructor
       public DonorRegistrationTypeSystem()
       {
       }
       public DonorRegistrationTypeSystem(int RegID)
           :this()
       {
           RegTypeID = RegID;
           AssignRegistrationTyeDetails();
       }
       #endregion

       #region VariabelDeclaration
       ResultArgs resultArgs = new ResultArgs();
       #endregion

       #region Properties
       public int RegTypeID { get; set; }
       public string RegistrationTypeName { get; set; }
	   #endregion

       #region Methods
       public ResultArgs SaveRegistrationType()
       {
           using (DataManager dataManager = new DataManager((RegTypeID == 0) ? SQLCommand.DonorRegistrationType.Add : SQLCommand.DonorRegistrationType.Update))
           {
               dataManager.Parameters.Add(this.AppSchema.DonorProspects.REGISTRATION_TYPE_IDColumn, RegTypeID,true);
               dataManager.Parameters.Add(this.AppSchema.DonorProspects.REGISTRATION_TYPEColumn, RegistrationTypeName);
               resultArgs = dataManager.UpdateData();
           }
           return resultArgs;
       }
       public ResultArgs AssignRegistrationTyeDetails()
       {
           resultArgs = FetchRegistrationTypeByID();
           if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
           {
               RegistrationTypeName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.REGISTRATION_TYPEColumn.ColumnName].ToString();
           }
           return resultArgs;
       }

       private ResultArgs FetchRegistrationTypeByID()
       {
           using (DataManager dataManager = new DataManager(SQLCommand.DonorRegistrationType.FetchRegistrationTypeByID))
           {
               dataManager.Parameters.Add(this.AppSchema.DonorProspects.REGISTRATION_TYPE_IDColumn, RegTypeID);
               dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
               resultArgs = dataManager.FetchData(DataSource.DataTable);
           }
           return resultArgs;
       }

       public ResultArgs FetchAllRegistrationTypeDetails()
       {
           using (DataManager dataManager = new DataManager(SQLCommand.DonorRegistrationType.FetchAll))
           {
               resultArgs = dataManager.FetchData(DataSource.DataTable);
           }
           return resultArgs;
       }

       public ResultArgs DeleteRegistrationTypeDetails()
       {
           using (DataManager dataManager = new DataManager(SQLCommand.DonorRegistrationType.Delete))
           {
               dataManager.Parameters.Add(this.AppSchema.DonorProspects.REGISTRATION_TYPE_IDColumn.ColumnName, RegTypeID);
               resultArgs = dataManager.UpdateData();
           }
           return resultArgs;
       }
       #endregion
   }
}
