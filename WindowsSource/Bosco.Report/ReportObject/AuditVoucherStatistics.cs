using System;
using Bosco.Utility;
using Bosco.Report.Base;
using System.Data;
using Bosco.DAO.Data;
using System.Collections;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using AcMEDSync.Model;
using DevExpress.XtraReports.UI;

namespace Bosco.Report.ReportObject
{
    public partial class AuditVoucherStatistics : ReportHeaderBase
    {
        #region Variables
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Constructor
        public AuditVoucherStatistics()
        {
            InitializeComponent();

            this.AttachDrillDownToRecord(xrtblHeaderCaption, xrCellVoucherType,
                new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName, "VOUCHER_NAME" }, DrillDownType.AUDITLOG, false);
        }
        #endregion

        #region Methods
        public override void ShowReport()
        {
            ShowAuditVoucherStatistics();
        }

        private void ShowAuditVoucherStatistics()
        {
            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo)
                || this.ReportProperties.Project == "0" || string.IsNullOrEmpty(this.ReportProperties.Project))
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        BindProperties();
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
                    BindProperties();
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
        }

        private void BindProperties()
        {
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            if (!string.IsNullOrEmpty(this.ReportProperties.UserName))
            {
                if (!ReportProperty.Current.ReportTitle.Contains(this.ReportProperties.UserName + " - "))
                {
                    ReportProperty.Current.ReportTitle = this.ReportProperties.UserName + " - " +  ReportProperty.Current.ReportTitle;
                }
            }

            setHeaderTitleAlignment();
            SetReportTitle();
            SetReportBorder();

            DataTable dtAuditVoucherStatistics = GetReportSource();
            if (dtAuditVoucherStatistics != null && dtAuditVoucherStatistics.Rows.Count > 0)
            {
                dtAuditVoucherStatistics.DefaultView.Sort = reportSetting1.AuditLog.VOUCHER_TYPEColumn.ColumnName;
                this.DataSource = dtAuditVoucherStatistics;
                this.DataMember = dtAuditVoucherStatistics.TableName;
            }
            else
            {
                this.DataSource = null;
            }
        }

        private void SetReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrTblData = AlignContentTable(xrTblData);
            xrTblGrandTotal = AlignContentTable(xrTblGrandTotal);
        }

        private DataTable GetReportSource()
        {
            try
            {
                string BankFlowQueryPath = this.GetAuditReportSQL(SQL.ReportSQLCommand.AuditReports.VoucherStatistics);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);

                    if (!string.IsNullOrEmpty(this.ReportProperties.UserName))
                    {
                        dataManager.Parameters.Add(this.reportSetting1.User.USER_NAMEColumn, this.ReportProperties.UserName);
                    }

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, BankFlowQueryPath);
                }
            }
            catch (Exception ee)
            {
                MessageRender.ShowMessage(ee.Message, true);
            }
            finally { }
                        
            DataTable dtAuditVoucherStatistics = new DataTable();
            if (resultArgs.Success)
            {
                dtAuditVoucherStatistics = resultArgs.DataSource.Table;
            }
            else
            {
                MessageRender.ShowMessage("Could not generate Audit Statistics", true);
            }

            return dtAuditVoucherStatistics;
        }
        #endregion

        
    }
}
