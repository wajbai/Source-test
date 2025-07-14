using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using Bosco.Report.Base;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class FDInterestRegister: ReportHeaderBase
    {
        #region Declaration
        List<Tuple<String, String, Int32>> lstcolumnwidth = new List<Tuple<String, String, Int32>>();
        ResultArgs resultArgs = null;
        private double TotalGrandCollectedInterest = 0;
        #endregion

        #region Constructor
        public FDInterestRegister()
        {
            InitializeComponent();

            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                xrHeaderReceivedInterest.Tag =  xrHeaderReceivedInterest.Text;
                xrHeaderAccumulatedInterest.Tag =  xrHeaderAccumulatedInterest.Text;
                xrHeaderTDSAmount.Tag = xrHeaderTDSAmount.Text;
                xrHeaderInterestAmount.Tag =  xrHeaderInterestAmount.Text;
            }

            //On 27/08/2020
            this.AttachDrillDownToRecord(xrtblBindSource, xrRenewalDate,
              new ArrayList { "FD_VOUCHER_ID", "FD_RENEWAL_ID" }, DrillDownType.LEDGER_VOUCHER, false, "VOUCHER_SUB_TYPE");
        }

        #endregion

        #region Show Reports
        public override void ShowReport()
        {
            FetchFDInterestRegister();
        }
        #endregion

        #region Methods
        public void FetchFDInterestRegister()
        {
            
            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo))
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
                        FetchFDInterestRegisterDetails();
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
                    FetchFDInterestRegisterDetails();
                    base.ShowReport();
                }
            }
        }

        public void FetchFDInterestRegisterDetails()
        {
            try
            {
                //  this.ReportTitle = ReportProperty.Current.ReportTitle;
                //this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
                this.SetLandscapeHeader = xrtblHeaderTable.WidthF;
                this.SetLandscapeFooter = xrtblHeaderTable.WidthF;
                //this.SetLandscapeFooterDateWidth = 970.00f;
                setHeaderTitleAlignment();
                // this.ReportPeriod = String.Format("For the Period: {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
                SetReportTitle();
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                grpFDLedgerHeader.GroupFields[0].FieldName = this.reportSetting1.Ledger.LEDGER_NAMEColumn.ColumnName;

                string FDRegister = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.FetchFDInterestRegister);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);

                    //23/10/2024, Attach currency country id 
                    if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                    {
                        dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn.ColumnName, this.ReportProperties.CurrencyCountryId);
                    }

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, FDRegister);

                    DataView dvCashFlow = resultArgs.DataSource.TableView;
                    
                    Detail.SortFields.Clear();
                    Detail.SortFields.Add(new GroupField(this.reportSetting1.FDRegister.RENEWAL_DATEColumn.ColumnName));
                    Detail.SortFields.Add(new GroupField(this.reportSetting1.FDRegister.VOUCHER_NOColumn.ColumnName));
                    
                    if (dvCashFlow != null && dvCashFlow.Count != 0)
                    {
                        dvCashFlow.Table.TableName = "FDRegister";
                        this.DataSource = dvCashFlow.ToTable();
                        this.DataMember = dvCashFlow.Table.TableName;
                        dvCashFlow.RowFilter = "";
                    }
                    else
                    {
                        dvCashFlow.Table.TableName = "FDRegister";
                        this.DataSource = dvCashFlow;
                        this.DataMember = dvCashFlow.Table.TableName;
                        grpFDLedgerHeader.Visible = false;
                        grpFDLedgerFooter.Visible = false;
                    }
                }
                SetReportBorder();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, false);
            }
            finally { }
        }

        private void SetReportBorder()
        {
            //***To align header table dynamically---changed by sugan******************************************************************************************
            xrtblHeaderTable.SuspendLayout();
            xrtblBindSource.SuspendLayout();
            xrtblGrandTotal.SuspendLayout();
            xrtblLedgerFooter.SuspendLayout();

            xrRowNarration.Visible = (ReportProperties.IncludeNarration == 1);
            
            xrtblHeaderTable.PerformLayout();
            xrtblBindSource.PerformLayout();
            xrtblGrandTotal.PerformLayout();
            xrtblLedgerFooter.PerformLayout();

            //*********************************************************************************************

            //31/07/2024, Other than India, let us lock TDS Amount
            if (this.AppSetting.IsCountryOtherThanIndia)
            {
                this.HideTableCell(xrtblHeaderTable, xrHeaderRow, xrHeaderTDSAmount);
                this.HideTableCell(xrtblBindSource, xrRowData, xrTDSAmount);
                this.HideTableCell(xrtblBindSource, xrRowNarration, xrNarrationTDSAmount);

                this.HideTableCell(xrtblLedgerFooter, xrLedgerGrpFooterRow, xrSumTDSLedger);
                this.HideTableCell(xrtblGrandTotal, xrgrandRow, xrGrandTotalTDSAmount);
            }

            xrtblHeaderTable = AlignHeaderTable(xrtblHeaderTable);
            xrtblBindSource = AlignContentTable(xrtblBindSource);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);
            xrtblGrpLedger = AlignContentTable(xrtblGrpLedger);
            xrtblLedgerFooter = AlignGrandTotalTable(xrtblLedgerFooter);

            //On 23/10/2024, To set currency symbol based on currency selection
            string currencysymbol = (this.AppSetting.AllowMultiCurrency==1 ? ReportProperties.CurrencyCountrySymbol : this.AppSetting.Currency );
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                xrHeaderReceivedInterest.Text = (xrHeaderReceivedInterest.Tag != null ? xrHeaderReceivedInterest.Tag.ToString() : xrHeaderReceivedInterest.Text);
                xrHeaderAccumulatedInterest.Text = (xrHeaderAccumulatedInterest.Tag != null ? xrHeaderAccumulatedInterest.Tag.ToString() : xrHeaderAccumulatedInterest.Text);
                xrHeaderTDSAmount.Text = (xrHeaderTDSAmount.Tag != null ? xrHeaderTDSAmount.Tag.ToString() : xrHeaderTDSAmount.Text);
                xrHeaderInterestAmount.Text = (xrHeaderInterestAmount.Tag != null ? xrHeaderInterestAmount.Tag.ToString() : xrHeaderInterestAmount.Text);
            }

            this.SetCurrencyFormat(xrHeaderReceivedInterest.Text, xrHeaderReceivedInterest, currencysymbol);
            this.SetCurrencyFormat(xrHeaderAccumulatedInterest.Text, xrHeaderAccumulatedInterest, currencysymbol);
            this.SetCurrencyFormat(xrHeaderTDSAmount.Text, xrHeaderTDSAmount, currencysymbol);
            this.SetCurrencyFormat(xrHeaderInterestAmount.Text, xrHeaderInterestAmount, currencysymbol);
        }

        #endregion
        
        
        private void xrBankTotalCaption_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string banktitleCaption = (sender as XRTableCell).Text.Trim();

            if (ReportProperty.Current.ShowByLedger == 1 && (ReportProperty.Current.ShowByInvestment == 1 || ReportProperty.Current.ShowByLedger == 1))
            {
                if (!string.IsNullOrEmpty(banktitleCaption))
                {
                    (sender as XRTableCell).Text = "     " + banktitleCaption;
                }
            }
            else if (ReportProperty.Current.ShowByLedger == 0 && ReportProperty.Current.ShowByInvestment == 1)
            {
                if (!string.IsNullOrEmpty(banktitleCaption))
                {
                    (sender as XRTableCell).Text = "   " + banktitleCaption;
                }
            }
        }

      

        private void xrActualAmountValue_EvaluateBinding(object sender, BindingEventArgs e)
        {
            double actualinterestamunt = 0;
            if (GetCurrentColumnValue(reportSetting1.FDRegister.Q1_INTEREST_AMOUNTColumn.ColumnName)!=null)
                actualinterestamunt +=UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.FDRegister.Q1_INTEREST_AMOUNTColumn.ColumnName).ToString());
            if (GetCurrentColumnValue(reportSetting1.FDRegister.Q2_INTEREST_AMOUNTColumn.ColumnName) != null)
                actualinterestamunt += UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.FDRegister.Q2_INTEREST_AMOUNTColumn.ColumnName).ToString());
            if (GetCurrentColumnValue(reportSetting1.FDRegister.Q3_INTEREST_AMOUNTColumn.ColumnName) != null)
                actualinterestamunt += UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.FDRegister.Q3_INTEREST_AMOUNTColumn.ColumnName).ToString());
            if (GetCurrentColumnValue(reportSetting1.FDRegister.Q4_INTEREST_AMOUNTColumn.ColumnName) != null)
                actualinterestamunt += UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.FDRegister.Q4_INTEREST_AMOUNTColumn.ColumnName).ToString());
            //TotalGrandCollectedInterest += actualinterestamunt;

            e.Value = UtilityMember.NumberSet.ToNumber(actualinterestamunt);
        }

        private void xrGrandActualAmountValue_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //e.Result = UtilityMember.NumberSet.ToNumber(TotalGrandCollectedInterest);
            //e.Handled = true;
        }

        private void xrRowData_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string narration = (GetCurrentColumnValue(this.LedgerParameters.NARRATIONColumn.ColumnName) == null) ? " " : GetCurrentColumnValue(this.LedgerParameters.NARRATIONColumn.ColumnName).ToString();
            //Left,Top,Right
            xrRenewalDate.Borders = xrVoucherNo.Borders = xrVoucherType.Borders = xrParticulars.Borders = xrReceivedInterest.Borders = xrAccumulatedInterest.Borders = xrTDSAmount.Borders = xrInterestAmount.Borders = BorderSide.Left | BorderSide.Right;
            if (string.IsNullOrEmpty(narration.Trim()) || this.ReportProperties.IncludeNarration == 0)
            {
                xrRowData.Borders = BorderSide.All;
                xrRenewalDate.Borders = xrVoucherNo.Borders = xrVoucherType.Borders = xrParticulars.Borders = xrReceivedInterest.Borders = xrAccumulatedInterest.Borders = xrTDSAmount.Borders = xrInterestAmount.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
            }
        }

        private void xrRowNarration_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string narration = (GetCurrentColumnValue(this.LedgerParameters.NARRATIONColumn.ColumnName) == null) ? " " : GetCurrentColumnValue(this.LedgerParameters.NARRATIONColumn.ColumnName).ToString();
            if (this.ReportProperties.IncludeNarration==0 ||  string.IsNullOrEmpty(narration.Trim()))
            {
                xrRowNarration.Visible = false;
            }
            else
            {
                xrRowNarration.Visible = true;
            }
        }

    }
}
