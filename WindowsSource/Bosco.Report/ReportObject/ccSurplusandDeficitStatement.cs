using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.Report.Base;
using System.Data;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class ccSurplusandDeficitStatement : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variable Declaration
        ResultArgs resultArgs = null;
        double TotalGains = 0;
        double TotalShortage = 0;
        int GroupCCID = 0;
        double GroupGains = 0;
        double GroupShortage = 0;
        DataTable dtSource = new DataTable();
        #endregion

        #region Property
        #endregion

        #region Constructor
        public ccSurplusandDeficitStatement()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindccSurplusandDeficitStatement();
            base.ShowReport();
        }
        #endregion

        #region Methods
        public void BindccSurplusandDeficitStatement()
        {
            if ((string.IsNullOrEmpty(this.ReportProperties.DateFrom) ||
                string.IsNullOrEmpty(this.ReportProperties.DateTo) ||
                this.ReportProperties.Project == "0"))
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                SetReportTitle();
                setHeaderTitleAlignment();
                this.SetLandscapeHeader = 1065.25f;
                this.SetLandscapeFooter = 1065.25f;
                this.SetLandscapeFooterDateWidth = 890.00f;
                this.HideReportDate = false;
                //this.ReportPeriod = string.Empty;
                this.ReportTitle = ReportProperty.Current.ReportTitle;
                this.ReportSubTitle = ReportProperty.Current.ProjectTitle;

                ResultArgs resultArgs = GetReportSource();
                DataView dvPayment = resultArgs.DataSource.TableView;

                if (dvPayment != null)
                {
                    DataTable dtSurples = dvPayment.ToTable();
                    if (dtSurples != null && dtSurples.Rows.Count > 0)
                    {
                        dtSource = dtSurples;
                        TotalGains = UtilityMember.NumberSet.ToDouble(dtSurples.Compute("SUM(AMOUNT)", "").ToString());
                        TotalShortage = UtilityMember.NumberSet.ToDouble(dtSurples.Compute("SUM(TRANS_MODE)", "").ToString());
                    }
                    else
                    {
                        dtSource = null;
                    }

                    dvPayment.Table.TableName = "CashBankReceipts";
                    this.DataSource = dvPayment;
                    this.DataMember = dvPayment.Table.TableName;

                }
            }
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string SurplusQuery = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.ccSurplusandDeficitStatement);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, SurplusQuery);
            }
            return resultArgs;
        }
        #endregion

        private void xrtcTotalGain_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = TotalGains >= Math.Abs(TotalShortage) ?
                 UtilityMember.NumberSet.ToNumber(TotalGains - Math.Abs(TotalShortage)) : string.Empty;
            e.Handled = true;
        }

        private void xrtcTotalLoss_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = Math.Abs(TotalShortage) >= TotalGains ?
               UtilityMember.NumberSet.ToNumber(Math.Abs(TotalShortage) - TotalGains) : string.Empty;
            e.Handled = true;
        }

        private void xrTableCell13_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            GroupCCID = 0;
            GroupGains = GroupShortage = 0.0;

            GroupCCID = GetCurrentColumnValue("CostCentre") != null ? this.UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("CostCentre").ToString()) : 0;
            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                DataView dvSource = dtSource.AsDataView();
                dvSource.RowFilter = "CostCentre = " + GroupCCID.ToString();

                if (dvSource != null && dvSource.ToTable().Rows.Count > 0)
                {
                    GroupGains = UtilityMember.NumberSet.ToDouble(dvSource.ToTable().Compute("SUM(AMOUNT)", "").ToString());
                    GroupShortage = UtilityMember.NumberSet.ToDouble(dvSource.ToTable().Compute("SUM(TRANS_MODE)", "").ToString());

                    if (GroupGains >= GroupShortage)
                    {
                        e.Result = UtilityMember.NumberSet.ToNumber(GroupGains - GroupShortage);
                        e.Handled = true;
                    }
                    else
                    {
                        e.Result = "0.00";
                        e.Handled = true;
                    }
                }
                else
                {
                    e.Result = "0.00";
                    e.Handled = true;
                }
            }
        }

        private void xrTableCell11_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            GroupCCID = 0;
            GroupGains = GroupShortage = 0.0;

            GroupCCID = GetCurrentColumnValue("CostCentre") != null ? this.UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("CostCentre").ToString()) : 0;
            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                DataView dvSource = dtSource.AsDataView();
                dvSource.RowFilter = "CostCentre = " + GroupCCID.ToString();

                if (dvSource != null && dvSource.ToTable().Rows.Count > 0)
                {
                    GroupGains = UtilityMember.NumberSet.ToDouble(dvSource.ToTable().Compute("SUM(AMOUNT)", "").ToString());
                    GroupShortage = UtilityMember.NumberSet.ToDouble(dvSource.ToTable().Compute("SUM(TRANS_MODE)", "").ToString());

                    if (GroupShortage >= GroupGains)
                    {
                        e.Result = UtilityMember.NumberSet.ToNumber(GroupShortage - GroupGains);
                        e.Handled = true;
                    }
                    else
                    {
                        e.Result = "0.00";
                        e.Handled = true;
                    }
                }
                else
                {
                    e.Result = "0.00";
                    e.Handled = true;
                }
            }
        }


    }
}
