using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using System.Linq;
using Bosco.Report.Base;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using Bosco.Utility.ConfigSetting;
using System.Text;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;

namespace Bosco.Report.ReportObject
{
    public partial class FCPurposeReceivedUtilisedDistribution : ReportHeaderBase
    {
        #region Variables
        ResultArgs resultArgs = new ResultArgs();
        private Int32 GrpNumber = 0 ;
        #endregion

        #region Constructor
        public FCPurposeReceivedUtilisedDistribution()
        {
            InitializeComponent();
        }
        #endregion

        #region Show Reports
        public override void ShowReport()
        {
            GrpNumber = 0;
            BindPurposeDistribution();
        }
        #endregion

        #region Methods
        public void BindPurposeDistribution()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
            //SetReportTitle();
            if (!string.IsNullOrEmpty(this.ReportProperties.DateFrom) || !string.IsNullOrEmpty(this.ReportProperties.DateTo)
                || this.ReportProperties.Project != "0")
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        BindProperty();   
                    }
                    else
                    {
                        SetReportTitle();
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    BindProperty();
                }
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
        }

        private void BindProperty()
        {
            setHeaderTitleAlignment();
            GrpNumber = 0;
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            AlignHeaderTable(xrtblHeader, true);
            AlignGroupTable(xrTblPurpose);
            AlignContentTable(xrTblData);
            AlignGrandTotalTable(xrtblGrandTotal);
            GetReportSource();
            Detail.SortFields.Add(new GroupField(reportSetting1.FC6PURPOSELIST.COST_CENTRE_NAMEColumn.ColumnName));
        }

        /// <summary>
        /// Fetch Report Source
        /// </summary>
        private void GetReportSource()
        {
            DateTime FirstFYFrom = this.AppSetting.FirstFYDateFrom;
            try
            {
                string Purpose = this.GetReportForeginContribution(SQL.ReportSQLCommand.ForeginContribution.FCPurposeDistribution);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_PROGRESS_FROMColumn, FirstFYFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, Purpose);

                    DataTable dtReportData = resultArgs.DataSource.Table;
                    
                    if (dtReportData != null)
                    {
                        string filter = "(" + reportSetting1.FC6PURPOSELIST.PREVIOUS_BALANCEColumn.ColumnName + "<> 0 OR " + //reportSetting1.FC6PURPOSELIST.OP_BALANCEColumn.ColumnName + "<> 0 OR " +
                                        reportSetting1.FC6PURPOSELIST.RECEIPTSColumn.ColumnName + "<> 0 OR " +
                                        reportSetting1.FC6PURPOSELIST.UTILISEDColumn.ColumnName + "<> 0)";

                        dtReportData.DefaultView.RowFilter = filter;
                        dtReportData = dtReportData.DefaultView.ToTable();

                        dtReportData.TableName = "FC6PURPOSELIST";
                        this.DataSource = dtReportData;
                        this.DataMember = dtReportData.TableName;
                    }
                    else
                    {
                        this.DataSource = null;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
        }

        #endregion

        private void xrcellOPBalance_EvaluateBinding(object sender, BindingEventArgs e)
        {
            //if (GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.PURPOSEColumn.ColumnName) != null)
            //{
            //    double dOpBalance = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.OP_BALANCEColumn.ColumnName).ToString());
            //    double dPreviousBalance = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.PREVIOUS_BALANCEColumn.ColumnName).ToString());
            //    e.Value = dOpBalance + dPreviousBalance;
            //}
        }

        private void xrcellTotal_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.PURPOSEColumn.ColumnName) != null)
            {
                //double dOpBalance = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.OP_BALANCEColumn.ColumnName).ToString());
                double dPreviousBalance = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.PREVIOUS_BALANCEColumn.ColumnName).ToString());
                double dReceived = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.RECEIPTSColumn.ColumnName).ToString());
                e.Value = dPreviousBalance + dReceived;
            }
        }

        private void xrcellBalance_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.PURPOSEColumn.ColumnName) != null)
            {
                //double dOpBalance = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.OP_BALANCEColumn.ColumnName).ToString());
                double dPreviousBalance = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.PREVIOUS_BALANCEColumn.ColumnName).ToString());
                double dReceived = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.RECEIPTSColumn.ColumnName).ToString());
                double dUtilized = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.UTILISEDColumn.ColumnName).ToString());
                e.Value = ( dPreviousBalance + dReceived) - dUtilized;
            }
        }

        private void xrcellSumOPBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //if (GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.PURPOSEColumn.ColumnName) != null)
            //{
            //    Int32 purposeid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("CONTRIBUTION_ID").ToString());
            //    DataTable dtReport = this.DataSource as DataTable;
            //    string op = "SUM(" + reportSetting1.FC6PURPOSELIST.OP_BALANCEColumn.ColumnName + ") + " +
            //                "SUM(" + reportSetting1.FC6PURPOSELIST.PREVIOUS_BALANCEColumn.ColumnName + ")";
            //    string filter = "CONTRIBUTION_ID = " + purposeid;

            //    double dOpBalance = UtilityMember.NumberSet.ToDouble(dtReport.Compute(op, filter).ToString());

            //    e.Result = dOpBalance;
            //    e.Handled = true;
            //}
        }

        private void xrcellSumTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.PURPOSEColumn.ColumnName) != null)
            {
                Int32 purposeid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("CONTRIBUTION_ID").ToString());
                DataTable dtReport = this.DataSource as DataTable;
                string total = "SUM(" + reportSetting1.FC6PURPOSELIST.PREVIOUS_BALANCEColumn.ColumnName + ") + " + //SUM(" + reportSetting1.FC6PURPOSELIST.OP_BALANCEColumn.ColumnName + ") + " +
                               "SUM(" + reportSetting1.FC6PURPOSELIST.RECEIPTSColumn.ColumnName +")";
                string filter = "CONTRIBUTION_ID = " + purposeid;
                e.Result = UtilityMember.NumberSet.ToDouble(dtReport.Compute(total, filter).ToString());
                e.Handled = true;
            }
        }

        private void xrcellSumBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.PURPOSEColumn.ColumnName) != null)
            {
                Int32 purposeid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("CONTRIBUTION_ID").ToString());
                DataTable dtReport = this.DataSource as DataTable;
                string TotalReceived = "SUM(" + reportSetting1.FC6PURPOSELIST.PREVIOUS_BALANCEColumn.ColumnName + ") + " + //"SUM(" + reportSetting1.FC6PURPOSELIST.OP_BALANCEColumn.ColumnName + ") + " +
                                        "SUM(" + reportSetting1.FC6PURPOSELIST.RECEIPTSColumn.ColumnName +")";
                string TotalUtlilized = "SUM(" + reportSetting1.FC6PURPOSELIST.UTILISEDColumn.ColumnName + ")";
                string filter = "CONTRIBUTION_ID = " + purposeid;

                double received = UtilityMember.NumberSet.ToDouble(dtReport.Compute(TotalReceived, filter).ToString()); ;
                double utilized = UtilityMember.NumberSet.ToDouble(dtReport.Compute(TotalUtlilized, filter).ToString()); ;

                e.Result = UtilityMember.NumberSet.ToNumber(received - utilized);
                e.Handled = true;
            }
        }

        private void grpHeaderPurpose_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            GrpNumber++;
        }

        private void xrTblPurpose_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.PURPOSEColumn.ColumnName) != null)
            {
                xrTblPurpose.TopF = 15;
                grpHeaderPurpose.HeightF = 40;
                if (GrpNumber==1)
                {
                    xrTblPurpose.TopF = 0;
                    grpHeaderPurpose.HeightF = 25;
                }

            }
        }

      

    }
}
