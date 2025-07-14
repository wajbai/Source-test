using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Utility.ConfigSetting;
using Bosco.Utility;
using System.Resources;
using System.Reflection;

namespace AcMEDSync.SQL
{
    public class AcMEDSyncSQL : CommonMember,IDisposable
    {
        string sqlQuery = string.Empty;
        private ImportMasterSQL importMasterSQL = null;
        private HeadOfficeLedgersSQL headOfficeLedgerSQL = null;
        private ExportVouchersSQL exportvoucherSQL = null;
        private ImportVoucherSQL importVoucherSQL = null;
        private AppSchemaSet.ApplicationSchemaSet appSchema = null;

        public AppSchemaSet.ApplicationSchemaSet AppSchema
        {
            get { if (appSchema == null) appSchema= new AppSchemaSet.ApplicationSchemaSet(); return appSchema; }
        }
        
        protected string GetMasterSQL(EnumDataSyncSQLCommand.ImportSQL queryId)
        {
            if (importMasterSQL == null) { importMasterSQL = new ImportMasterSQL(); }
            sqlQuery = importMasterSQL.GetQuery(queryId);
            return sqlQuery;
        }

        public string GetLedgerSQL(EnumDataSyncSQLCommand.ImportLedger queryId)
        {
            if (headOfficeLedgerSQL == null) { headOfficeLedgerSQL = new HeadOfficeLedgersSQL(); }
            sqlQuery = headOfficeLedgerSQL.GetQuery(queryId);
            return sqlQuery;
        }

        public string GetVoucherSQL(EnumDataSyncSQLCommand.ExportVouchers queryId)
        {
            if (exportvoucherSQL == null) { exportvoucherSQL = new ExportVouchersSQL(); }
            sqlQuery = exportvoucherSQL.GetQuery(queryId);
            return sqlQuery;
        }

        public string GetImportVoucherSQL(EnumDataSyncSQLCommand.ImportVoucher queryId)
        {
            if (exportvoucherSQL == null) { importVoucherSQL = new ImportVoucherSQL(); }
            sqlQuery = importVoucherSQL.GetQuery(queryId);
            return sqlQuery;
        }
        public string GetMessage(string keyCode)
        {
            ResourceManager resourceManger = new ResourceManager("AcMEDSync.Resources.Messages.Messages", Assembly.GetExecutingAssembly());
            string msg = "";
            try
            {
                msg = resourceManger.GetString(keyCode);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage("Resoure File is not available", false);
            }
            return msg;
        }


        public void Dispose()
        {
            GC.Collect();
        }
    }
}
