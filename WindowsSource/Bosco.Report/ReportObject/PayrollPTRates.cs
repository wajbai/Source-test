using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.DAO;
using Bosco.Utility;
using Bosco.Report.Base;
using Bosco.Utility.ConfigSetting;
using Bosco.DAO.Data;
using System.Data;
using Payroll.DAO.Schema;

namespace Bosco.Report.ReportObject
{
    public partial class PayrollPTRates : Bosco.Report.Base.ReportBase
    {
        #region VariableDeclaration
        ApplicationSchema.PRCOMPMONTHDataTable dtCompMonth = new ApplicationSchema.PRCOMPMONTHDataTable();

             
        public float CashBankLedgerTableWidth
        {
            set
            {
                xrTblData.WidthF = value;
            }
        }
        #endregion

        #region Constructor
        public PayrollPTRates()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        public override void ShowReport()
        {
            base.ShowReport();
        }

        public ResultArgs BindPTRateDetails()
        {
            //# Get list of components for given payroll and staff group
            ResultArgs resultArgs = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.PTRateDetails, "PTRateDetails"))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn.ColumnName, ReportProperty.Current.PayrollId);
                dataManager.Parameters.Add(dtCompMonth.GROUPIDColumn.ColumnName, ReportProperty.Current.PayrollGroupId);
                resultArgs = dataManager.FetchData(Bosco.DAO.Data.DataSource.DataTable);
            }

            if (resultArgs.Success && resultArgs.DataSource.Table != null)
            {
                DataTable dtPTRateDetails = resultArgs.DataSource.Table;
                dtPTRateDetails.TableName = this.DataMember;

                this.DataSource = dtPTRateDetails;
                this.DataMember = dtPTRateDetails.TableName;

                Detail.Visible = ReportFooter.Visible = (dtPTRateDetails.Rows.Count > 0);
            }
            else
            {
                MessageRender.ShowMessage(resultArgs.Message);
            }
            return resultArgs;
        }
        #endregion
    }
}
