using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using System.IO;
using System.Data;
using DevExpress.XtraSplashScreen;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Report.Base;
using Bosco.Report.View;
using Bosco.Utility;

namespace Bosco.Report.ReportObject
{
    public partial class InternalAuditTransactions : ReportHeaderBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public InternalAuditTransactions()
        {
            InitializeComponent();
        }

        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                || String.IsNullOrEmpty(this.ReportProperties.Project))
            {

                ShowReportFilterDialog();
            }
            else
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        BindInternalAuditSource();
                        SplashScreenManager.CloseForm();
                        base.ShowReport();
                    }
                    else
                    {
                        SetReportTitle();
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    SplashScreenManager.ShowForm(typeof(frmReportWait));
                    BindInternalAuditSource();
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
        }
        #endregion

        #region Methods

        public void BindInternalAuditSource()
        {
            setHeaderTitleAlignment();
            SetReportTitle();
            this.SetLandscapeBudgetNameWidth = xrtblHeader.WidthF;
            this.SetLandscapeHeader = xrtblHeader.WidthF;
            this.SetLandscapeFooter = xrtblHeader.WidthF;
            this.SetLandscapeFooterDateWidth = xrtblHeader.WidthF;

            DataTable dtSource = GetReportSource();

            if (dtSource != null)
            {
                this.DataSource = dtSource;
                this.DataMember = dtSource.TableName;
            }
        }

        private DataTable GetReportSource()
        {
            string InternalAudit = this.GetReportSQL(SQL.ReportSQLCommand.Report.MonitorInternalAudit);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, InternalAudit);
            }
            return resultArgs.DataSource.Table;
        }
        #endregion

        #region Events

        #endregion
    }
}
