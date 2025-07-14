using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using System.Data;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class DrillDownFCReports : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        int PurposeId = 0;
        int CountryId = 0;
        int DonorId = 0;
        string PurposeName = string.Empty;
        string PurposeWithDonor = string.Empty;
        string ReceiptDate = string.Empty;
        string InsReceiptDate = string.Empty;
        #endregion
        public DrillDownFCReports()
        {
            InitializeComponent();
            this.AttachDrillDownToRecord(xrtblDetails, xrDonor,
                 new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName, }, DrillDownType.LEDGER_CASHBANK_VOUCHER, false, "VOUCHER_SUB_TYPE");
        }

        #region Methods
        public override void ShowReport()
        {
            if (IsDrillDownMode)
            {
                Dictionary<string, object> dicDDProperties = this.ReportProperties.DrillDownProperties;
                DrillDownType ddtypeLinkType = DrillDownType.BASE_REPORT;
                ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), dicDDProperties["DrillDownLink"].ToString());

                if (dicDDProperties.ContainsKey(this.ReportParameters.CONTRIBUTION_IDColumn.ColumnName))
                {
                    PurposeId = dicDDProperties[this.ReportParameters.CONTRIBUTION_IDColumn.ColumnName] != null ? this.ReportProperties.NumberSet.ToInteger(dicDDProperties[this.ReportParameters.CONTRIBUTION_IDColumn.ColumnName].ToString()) : 0; ;
                }
                if (dicDDProperties.ContainsKey(this.ReportParameters.DONAUD_IDColumn.ColumnName))
                {
                    DonorId = dicDDProperties[this.ReportParameters.DONAUD_IDColumn.ColumnName] != null ? this.ReportProperties.NumberSet.ToInteger(dicDDProperties[this.ReportParameters.DONAUD_IDColumn.ColumnName].ToString()) : 0; ;
                }
                if (dicDDProperties.ContainsKey(this.ReportParameters.COUNTRY_IDColumn.ColumnName))
                {
                    CountryId = dicDDProperties[this.ReportParameters.COUNTRY_IDColumn.ColumnName] != null ? this.ReportProperties.NumberSet.ToInteger(dicDDProperties[this.ReportParameters.COUNTRY_IDColumn.ColumnName].ToString()) : 0;
                }
                if (dicDDProperties.ContainsKey(this.ReportParameters.DONORColumn.ColumnName))
                {
                    PurposeWithDonor = dicDDProperties[this.ReportParameters.DONORColumn.ColumnName] != null ? dicDDProperties[this.ReportParameters.DONORColumn.ColumnName].ToString() : string.Empty;
                }
                if (dicDDProperties.ContainsKey("DATE_AND_MONTH_OF_RECEIPTS"))
                {
                    ReceiptDate = dicDDProperties["DATE_AND_MONTH_OF_RECEIPTS"] != null ? dicDDProperties["DATE_AND_MONTH_OF_RECEIPTS"].ToString() : string.Empty;
                }
                if (dicDDProperties.ContainsKey("RECEIPT_DATE"))
                {
                    ReceiptDate = dicDDProperties["RECEIPT_DATE"] != null ? dicDDProperties["RECEIPT_DATE"].ToString() : string.Empty;
                }
                if (dicDDProperties.ContainsKey(this.ReportParameters.PURPOSEColumn.ColumnName))
                {
                    PurposeWithDonor = dicDDProperties[this.ReportParameters.PURPOSEColumn.ColumnName] != null ? dicDDProperties[this.ReportParameters.PURPOSEColumn.ColumnName].ToString() : string.Empty;
                }
                if (dicDDProperties.ContainsKey(this.ReportParameters.PURPOSEColumn.ColumnName) && dicDDProperties.ContainsKey(this.ReportParameters.DONORColumn.ColumnName))
                {
                    PurposeName = dicDDProperties[this.ReportParameters.PURPOSEColumn.ColumnName] != null ? dicDDProperties[this.ReportParameters.PURPOSEColumn.ColumnName].ToString() : string.Empty;
                    string Donor = dicDDProperties[this.ReportParameters.DONORColumn.ColumnName] != null ? dicDDProperties[this.ReportParameters.DONORColumn.ColumnName].ToString() : string.Empty;
                    PurposeWithDonor = PurposeName + " by " + Donor;
                }
                if (dicDDProperties.ContainsKey("COUNTRY"))
                {
                    PurposeWithDonor = dicDDProperties["COUNTRY"] != null ? dicDDProperties["COUNTRY"].ToString() : string.Empty;
                }
            }
            ShowPurposeWise();
            base.ShowReport();
        }

        private void ShowPurposeWise()
        {
            try
            {
                this.SetLandscapeHeader = xrtblTotal.WidthF;
                this.SetLandscapeFooter = xrtblTotal.WidthF;
                this.SetLandscapeFooterDateWidth = 970.00f;

                SetReportTitle();
                if (CountryId > 0 || DonorId > 0)
                {
                    this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + this.ReportProperties.DateFrom + " - " + this.ReportProperties.DateTo;
                }
                else
                {
                    this.ReportPeriod = MessageCatalog.ReportCommonTitle.ASON + " : " + ReportProperties.DateSet.ToDate(ReceiptDate, false).ToShortDateString();
                }
                this.ReportTitle = PurposeWithDonor;
                this.SetCurrencyFormat(xrCapAmount.Text, xrCapAmount);
                resultArgs = GetReportSource();
                DataTable dtFCReprots = resultArgs.DataSource.Table;
                if (dtFCReprots != null && dtFCReprots.Rows.Count != 0)
                {
                    // dtFCReprots.TableName = "FCReports";
                    this.DataSource = dtFCReprots;
                    this.DataMember = dtFCReprots.TableName;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, false);
            }
            finally { }
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                string FCReportWise = this.GetReportForeginContribution(SQL.ReportSQLCommand.ForeginContribution.FCDrillDownReport);
                using (DataManager dataManager = new DataManager())
                {
                    if (!string.IsNullOrEmpty(ReceiptDate))
                    {
                        dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, ReceiptDate);
                    }
                    else
                    {
                        dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, ReportProperties.DateFrom);
                        dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, ReportProperties.DateTo);
                    }
                    if (PurposeId > 0)
                    {
                        dataManager.Parameters.Add(this.ReportParameters.CONTRIBUTION_IDColumn, PurposeId);
                    }
                    if (DonorId > 0)
                    {
                        dataManager.Parameters.Add(this.ReportParameters.DONAUD_IDColumn, DonorId);
                    }
                    if (CountryId > 0)
                    {
                        dataManager.Parameters.Add(this.ReportParameters.COUNTRY_IDColumn, CountryId);
                    }
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, (!string.IsNullOrEmpty(ReportProperties.Project)) ? ReportProperties.Project : "0");

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, FCReportWise);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, false);
            }
            finally { }
            return resultArgs;
        }

        #endregion

    }
}
