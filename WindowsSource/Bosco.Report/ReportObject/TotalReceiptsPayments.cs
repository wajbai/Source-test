using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Report.Base;
using System.Data;
using Bosco.Utility.ConfigSetting;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;

namespace Bosco.Report.ReportObject
{
    public partial class TotalReceiptsPayments : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public TotalReceiptsPayments()
        {
            InitializeComponent();
            this.AttachDrillDownToRecord(xrTableSource, xrtblParticulars,
               new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName }, DrillDownType.LEDGER_VOUCHER, false, "VOUCHER_SUB_TYPE");
        }
        #endregion

        #region Variables
        ResultArgs resultArgs = null;
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            if (IsDrillDownMode)
            {
                Dictionary<string, object> dicDDProperties = this.ReportProperties.DrillDownProperties;
                DrillDownType ddtypeLinkType = DrillDownType.BASE_REPORT;
                ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), dicDDProperties["DrillDownLink"].ToString());

                if (dicDDProperties.ContainsKey(this.reportSetting1.CashBankFlow.DATEColumn.ColumnName))
                {
                    //LedgerDate = dicDDProperties[this.reportSetting1.CashBankFlow.DATEColumn.ColumnName].ToString();
                    //this.ReportProperties.BankAccount = "0";
                    //datefrom = dateto = LedgerDate;
                }
            }
            else
            {
                //datefrom = this.ReportProperties.DateFrom;
                //dateto = this.ReportProperties.DateTo;
            }
            BindTotalReceiptsPayments();
        }
        #endregion

        #region Methods

        private void BindTotalReceiptsPaymentsExtracted()
        {
            SplashScreenManager.ShowForm(typeof(frmReportWait));
            setHeaderTitleAlignment();
            SetReportTitle();
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                        
            resultArgs = GetReportSource();
            if (resultArgs.Success)
            {
                DataView dvTotalReceiptsPayments = resultArgs.DataSource.TableView;
                if (dvTotalReceiptsPayments != null && dvTotalReceiptsPayments.Count != 0)
                {
                    xrCellTotal.Text = this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(dvTotalReceiptsPayments.Table.Compute("SUM(AMOUNT)", string.Empty).ToString()));
                    dvTotalReceiptsPayments.Table.TableName = "TotalReceiptsPayments";
                    //On 05/06/2017, To add Amount filter condition
                    AttachAmountFilter(dvTotalReceiptsPayments);
                    this.DataSource = dvTotalReceiptsPayments;
                    this.DataMember = dvTotalReceiptsPayments.Table.TableName;
                }
                else
                {
                    this.Detail.Visible = false;
                    this.ReportFooter.Visible = false;
                    this.DataSource = null;
                }
            }
            else
            {
                MessageRender.ShowMessage("Could not generate Report " + resultArgs.Message, true);
            }
            SplashScreenManager.CloseForm();
        }
        
        private void BindTotalReceiptsPayments()
        {
            SetTitleWidth(xrtblHeaderCaption.WidthF);
            this.SetLandscapeHeader = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooter = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooterDateWidth = xrtblHeaderCaption.WidthF;
            this.setHeaderTitleAlignment();
            this.Detail.Visible = true;
            this.ReportFooter.Visible = true;
            if (!string.IsNullOrEmpty(this.ReportProperties.DateFrom) || !string.IsNullOrEmpty(this.ReportProperties.DateTo) || this.ReportProperties.Project != "0")
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        BindTotalReceiptsPaymentsExtracted();
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
                    BindTotalReceiptsPaymentsExtracted();
                    base.ShowReport();
                }
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrTableSource = AlignContentTable(xrTableSource);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);
            xrTblRowNarration.Visible = (this.ReportProperties.IncludeNarration == 1);
            this.SetCurrencyFormat(xrCapAmount.Text, xrCapAmount);
        }
        private ResultArgs GetReportSource()
        {
            try
            {
                string DayBook = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.TotalReceiptsPayments);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    if (this.ReportId == "RPT-157") //For Total Receitps
                        dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, VoucherSubTypes.RC.ToString());
                    else //For Total Payments
                        dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, VoucherSubTypes.PY.ToString());

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, DayBook);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return resultArgs;
        }
        #endregion

        #region Events

        #endregion
        
        /// <summary>
        /// This method is used to add filter condition
        /// </summary>
        private DataView AttachAmountFilter(DataView dv)
        {
            //On 05/06/2017, To add Amount filter condition
            string AmountFilter = this.GetAmountFilter();
            lblAmountFilter.Visible = false;
            if (AmountFilter != "")
            {
                dv.RowFilter = "(CREDIT > 0 AND CREDIT " + AmountFilter + ") OR (DEBIT > 0 AND DEBIT " + AmountFilter + ")";
                lblAmountFilter.Text = "Amount filtered by " + this.UtilityMember.NumberSet.ToNumber(this.ReportProperties.DonorFilterAmount);
                lblAmountFilter.Visible = true;
            }

            //On 30/08/2018, to filter based on Voucher Type
            string VoucherTypeFilter = string.Empty;
            if (this.ReportProperties.DayBookVoucherType != 0)
            {
                Utility.DefaultVoucherTypes vouchertype = (Utility.DefaultVoucherTypes)UtilityMember.EnumSet.GetEnumItemType(typeof(Utility.DefaultVoucherTypes), 
                                                            this.ReportProperties.DayBookVoucherType.ToString());
                VoucherTypeFilter = "(VOUCHER_TYPE =  '" + vouchertype.ToString() + "')"; 
            }
            dv.RowFilter = VoucherTypeFilter;
            return dv;
        }
    }
}
