using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using System.Data;

namespace Bosco.Model.ReportCustomization
{
    public class ReportCustomizationSystem : SystemBase
    {

        #region Variables
        ResultArgs resultArgs = null;
        #endregion

        #region Properties
        public string ReportName { get; set; }
        #endregion

        #region Constructor
        public ReportCustomizationSystem()
        {

        }
        #endregion

        #region Methods

        public ResultArgs SaveCustomReport(byte[] Value)
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = DeleteCustomReport();
                if (resultArgs != null && resultArgs.Success)
                    resultArgs = SaveCustomizedReport(Value);
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs SaveCustomizedReport(byte[] Value)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CustomReport.SaveCustomReport))
            {
                dataManager.Parameters.Add(AppSchema.ReportCustomization.SERIALIZED_REPORTColumn, Value);
                dataManager.Parameters.Add(AppSchema.ReportCustomization.REPORT_NAMEColumn, ReportName);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteCustomReport()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CustomReport.DeletePreviousReport))
            {
                dataManager.Parameters.Add(AppSchema.ReportCustomization.REPORT_NAMEColumn, ReportName);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchCustomizedReport()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CustomReport.FetchCustomReportByName))
            {
                dataManager.Parameters.Add(AppSchema.ReportCustomization.REPORT_NAMEColumn, ReportName);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion
    }
}
