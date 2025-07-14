using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.Report.Base;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using System.Data;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class ChartofStocks : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variable Declaration
        ResultArgs resultArgs = null;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public ChartofStocks()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindChartofStocks();
            base.ShowReport();
        }
        #endregion

        #region Methods
        public void BindChartofStocks()
        {
            SetReportTitle();

            this.SetLandscapeFooterDateWidth = 250.00f;
            this.SetLandscapeHeader = 723.00f;
            this.SetLandscapeFooter = 723.00f;

            this.ReportTitle = ReportProperty.Current.ReportTitle;
            if (!string.IsNullOrEmpty(ReportProperty.Current.ProjectTitle))
                this.ReportSubTitle = ReportProperty.Current.ProjectTitle;

            this.ReportPeriod = string.Empty;
            ResultArgs resultArgs = GetReportSource();
            DataView dvPayment = resultArgs.DataSource.TableView;

            if (dvPayment != null)
            {
                dvPayment.Table.TableName = "ChartofAssets";
                this.DataSource = dvPayment;
                this.DataMember = dvPayment.Table.TableName;
            }
            SetReportBorder();
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string ChartofStocks = this.GetReportTDS(SQL.ReportSQLCommand.Stock.ChartofStock);

            using (DataManager dataManager = new DataManager())
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, ChartofStocks);
            }
            return resultArgs;
        }

        private void SetReportBorder()
        {
            xrTable1 = AlignHeaderTable(xrTable1);
            xrtChartofAssetsDetail = AlignContentTable(xrtChartofAssetsDetail);
        }
        #endregion
    }
}
