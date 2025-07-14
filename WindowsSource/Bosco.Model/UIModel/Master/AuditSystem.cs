using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;

namespace Bosco.Model.UIModel.Master
{
    public class AuditSystem : SystemBase
   {
        #region Variable Decelaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public AuditSystem()
        {
        }
        public AuditSystem(int AuditId)
        {
            FillAuditProperties(AuditId);
        }
       
        #endregion

        #region Audit Properties
        public int AuditId { get; set; }
        public string AuditType { get; set; }
        #endregion

       #region Methods

        public ResultArgs FetchAuditTypeDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Audit.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteAuditDetails(int AuditId)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.Audit.Delete))
            {
                dataMember.Parameters.Add(this.AppSchema.Audit.AUDIT_TYPE_IDColumn,AuditId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveAuditDetails()
        {
            using (DataManager dataManager = new DataManager((AuditId == 0) ? SQLCommand.Audit.Add : SQLCommand.Audit.Update))
            {
                dataManager.BeginTransaction();
                dataManager.Parameters.Add(this.AppSchema.Audit.AUDIT_TYPEColumn, AuditType,true);
                dataManager.Parameters.Add(this.AppSchema.Audit.AUDIT_TYPE_IDColumn, AuditId);
                resultArgs = dataManager.UpdateData();
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public void FillAuditProperties(int AuditId)
        {
            resultArgs = FetchAuditDetailsById(AuditId);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                AuditType= resultArgs.DataSource.Table.Rows[0][this.AppSchema.Audit.AUDIT_TYPEColumn.ColumnName].ToString();
                
            }
        }

        private ResultArgs FetchAuditDetailsById(int AuditId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Audit.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.Audit.AUDIT_TYPE_IDColumn,AuditId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

       #endregion
   }
}
