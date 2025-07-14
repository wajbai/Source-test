using System;
using System.Collections;
using Bosco.Utility;
using DevExpress.XtraSplashScreen;
using System.Data;
using Bosco.Report.View;
using Bosco.Report.Base;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class FCDonorContributionList : Bosco.Report.Base.ReportHeaderBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public FCDonorContributionList()
        {
            InitializeComponent();
            this.AttachDrillDownToRecord(xrFCPurposeDetails, xrDonorName,
                //new ArrayList { this.ReportParameters.DONAUD_IDColumn.ColumnName, this.ReportParameters.DONORColumn.ColumnName, "RECEIPT_DATE" }, DrillDownType.FC_REPORT, false);
             new ArrayList { this.ReportParameters.DONAUD_IDColumn.ColumnName, this.ReportParameters.DONORColumn.ColumnName, "RECEIPT_DATE" }, DrillDownType.FC_REPORT, false);

        }
        #endregion

        #region Methods
        public override void ShowReport()
        {
            SetFCPurposeDonorWiseReport();
        }

        private void SetFCPurposeDonorWiseReport()
        {
            this.SetLandscapeHeader = FCPurposeHeader.WidthF;
            this.SetLandscapeFooter = FCPurposeHeader.WidthF;
            this.SetLandscapeFooterDateWidth = 970.00f;

            if (this.ReportProperties.DateFrom != string.Empty || this.ReportProperties.DateTo != string.Empty)
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));

                        setHeaderTitleAlignment();
                        SetReportTitle();
                        // this.ReportSubTitle = "Foreign Projects";
                        this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                        this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                        ShowHideBreakUpByDonor();
                        this.SetCurrencyFormat(xrCapAmount.Text, xrCapAmount);
                        DataTable dtFCDonorWiseContribution = GetReportSource();
                        dtFCDonorWiseContribution.DefaultView.Sort = "";
                        if (dtFCDonorWiseContribution != null)
                        {
                            this.DataSource = dtFCDonorWiseContribution;
                            this.DataMember = dtFCDonorWiseContribution.TableName;
                        }
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
                    setHeaderTitleAlignment();
                    SetReportTitle();
                    // this.ReportSubTitle = "Foreign Projects";
                    this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                    this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                    ShowHideBreakUpByDonor();
                    this.SetCurrencyFormat(xrCapAmount.Text, xrCapAmount);
                    DataTable dtFCPurpose = GetReportSource();
                    if (dtFCPurpose != null)
                    {
                        this.DataSource = dtFCPurpose;
                        this.DataMember = dtFCPurpose.TableName;
                    }
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                    // xrtblDonor = AlignGroupTable(xrtblDonor);
                    //  xrFCPurposeDetails = AlignContentTable(xrFCPurposeDetails);
                }
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
        }

        private DataTable GetReportSource()
        {
            try
            {
                string FCDonorContributionQueryPath = this.GetReportForeginContribution(SQL.ReportSQLCommand.ForeginContribution.FCPurposeWiseContribution);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project ?? "0");
                    if (!string.IsNullOrEmpty(ReportProperty.Current.DonorConditionSymbol))
                    {
                        switch (ReportProperty.Current.DonorConditionSymbol)
                        {
                            case "=":
                                dataManager.Parameters.Add(this.ReportParameters.DONOR_EQUALSColumn, ReportProperty.Current.DonorFilterAmount);
                                break;
                            case "<>":
                                dataManager.Parameters.Add(this.ReportParameters.DONOR_NOT_EQUALSColumn, ReportProperty.Current.DonorFilterAmount);
                                break;
                            case ">":
                                dataManager.Parameters.Add(this.ReportParameters.DONOR_GREATER_THANColumn, ReportProperty.Current.DonorFilterAmount);
                                break;
                            case ">=":
                                dataManager.Parameters.Add(this.ReportParameters.DONOR_GREATER_EQUALSColumn, ReportProperty.Current.DonorFilterAmount);
                                break;
                            case "<":
                                dataManager.Parameters.Add(this.ReportParameters.DONOR_LEES_THANColumn, ReportProperty.Current.DonorFilterAmount);
                                break;
                            case "<=":
                                dataManager.Parameters.Add(this.ReportParameters.DONOR_LEES_EQUALSColumn, ReportProperty.Current.DonorFilterAmount);
                                break;
                        }
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, FCDonorContributionQueryPath);
                }
            }
            catch (Exception ee)
            {
                MessageRender.ShowMessage(ee.Message, true);
            }
            finally { }

            //On 01/06/2017, To change sort order as date and donor name
            DataView dv = new DataView();
            if (resultArgs.Success)
            {
                dv = resultArgs.DataSource.TableView;
                dv.Sort = "RECEIPT_DATE, DONOR";
            }

            return dv.ToTable();
        }
        #endregion

        private void grpBreakDonorFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            bool isPageBreakEnabled = ReportProperties.BreakbyDonor == 1;

            if (isPageBreakEnabled)
            {
                ReportFooter.Visible = false;
                xrTableCell1.WidthF = xrTableCell3.WidthF = 0F;
                grpBreakDonorFooter.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
            }
            else
            {
                ReportFooter.Visible = true;
                xrTableCell1.WidthF = xrTableCell3.WidthF = 219.59F;
                grpBreakDonorFooter.PageBreak = DevExpress.XtraReports.UI.PageBreak.None;
            }
            grpBreakDonorFooter.KeepTogether = true;
        }

        /// <summary>
        /// visibility
        /// </summary>
        private void ShowHideBreakUpByDonor()
        {
            if (ReportProperties.BreakbyDonor == 1)
            {
                
                ghBreakupDonor.Visible = grpBreakDonorFooter.Visible = true;
                ReportFooter.Visible = false;
                xrTableCell1.WidthF = xrTableCell3.WidthF = 0F;
            }
            else
            {
               
                ghBreakupDonor.Visible = grpBreakDonorFooter.Visible = false;
                ReportFooter.Visible = true;
                xrTableCell1.WidthF = xrTableCell3.WidthF = 219.59F;
            }

        }

    }
}
