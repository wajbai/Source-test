using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;
using System.Globalization;
using Bosco.Utility.ConfigSetting;
using Bosco.Report.View;
using DevExpress.XtraSplashScreen;
using System.Linq;
using DevExpress.XtraReports.UI.PivotGrid;
using DevExpress.Data.Filtering;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraCharts.Printing;
using DevExpress.XtraCharts;

namespace Bosco.Report.ReportObject
{
    public partial class MultiAbstractReceiptsYear : Bosco.Report.Base.ReportHeaderBase
    {
        private Graphics gr = Graphics.FromHwnd(IntPtr.Zero);
        bool IsMultiReceipts = true;
        bool rowemptyremoved = false;

        DateTime dtBudgetDateFrom = new DateTime();
        DateTime dtBudgetDateTo = new DateTime();
        string BudgetProjects = string.Empty;
        public DataTable RPTotalTable = new DataTable();
        public double TotalReceipts = 0;
        public double TotalPayments = 0;
        

        public MultiAbstractReceiptsYear()
        {
            InitializeComponent();

            this.SetTitleWidth(this.PageWidth - 25);
            this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = this.PageWidth - 25;
        }

        public void HideReportHeaderFooter()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
        }

        #region ShowReport
        public override void ShowReport()
        {
            rowemptyremoved = false;
            
            IsMultiReceipts = (this.ReportProperties.ReportId == "RPT-156" ? false : true);

            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                || String.IsNullOrEmpty(this.ReportProperties.Project)
                || this.ReportProperties.NoOfYears==0)
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
                        BindMultiAbstractReceiptSource(IsMultiReceipts);
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
                    BindMultiAbstractReceiptSource(IsMultiReceipts);
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
        }
        #endregion

        #region Methods
        public void BindMultiAbstractReceiptSource(bool isreceipts)
        {
            //On 29/05/2023, To set if ledger is not selected if CC or Donor selected
            if (ReportProperties.ShowCCDetails == 1 || ReportProperties.ShowDonorDetails == 1)
            {
                ReportProperties.ShowByLedger = 1;
                ReportProperties.ShowLedgerCode = 1;
            } 

            //On 27/01/2021, To set Year From/Year To Title ----------------------------------------------------------------------------------
            string YearFrom = UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).Year.ToString();
            string YearTo = UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).Year.ToString();
            if (this.ReportProperties.NoOfYears > 0)
            {
                YearFrom = (UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).Year - this.ReportProperties.NoOfYears).ToString();
            }
            //--------------------------------------------------------------------------------------------------------------------------------
            IsMultiReceipts = isreceipts;
            setHeaderTitleAlignment();
            SetReportTitle();
            this.ReportPeriod = "Year From " + YearFrom + " To " + YearTo;
            this.HideDateRange = false;            
            grpOpeningBalane.Visible = xrSubOpeningBalanceMulti.Visible = xrLabelCLBal.Visible = false;
            detailClosingBalance.Visible =  xrSubClosingBalanceMulti.Visible = xrLabelCLBal.Visible = false;
            
            if (IsMultiReceipts)
            {
                grpOpeningBalane.Visible = xrSubOpeningBalanceMulti.Visible = true;
                xrLabelCLBal.Visible = false;
            }
            else
            {
                this.Name = "MultiAbstractPaymentsYear";
                detailClosingBalance.Visible = xrSubClosingBalanceMulti.Visible = xrLabelCLBal.Visible = true;
            }

            ResultArgs resultArgs = GetReportSource(IsMultiReceipts);
            if (resultArgs.Success && resultArgs.DataSource.Table!=null)
            {
                DataTable dtReceiptPaymentsYear = resultArgs.DataSource.Table;
                if (dtReceiptPaymentsYear != null)
                {
                    dtReceiptPaymentsYear.TableName = "MultiAbstract";
                    xrPGMultiAbstractReceipt.DataSource = dtReceiptPaymentsYear.DefaultView;
                    xrPGMultiAbstractReceipt.DataMember = dtReceiptPaymentsYear.TableName;
                }

                if (IsMultiReceipts) //for Multi Year Receipts Opening Balance
                {
                    AccountBalanceMultiYear accountBalanceMultiYear = xrSubOpeningBalanceMulti.ReportSource as AccountBalanceMultiYear;
                    SetReportSetting(dtReceiptPaymentsYear.DefaultView, accountBalanceMultiYear);
                    accountBalanceMultiYear.BankClosedDate = this.ReportProperties.DateFrom; //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                    //On 28/04/2023, to have proper Date from 
                    if (this.ReportProperties.NoOfYears > 0 && !string.IsNullOrEmpty(this.ReportProperties.DateFrom))
                    {
                        accountBalanceMultiYear.BankClosedDate = UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).AddYears(-this.ReportProperties.NoOfYears).ToShortDateString();
                    }

                    //xrLabelCLBal.Text = (IsMultiReceipts ? "Opening Balance" : "Closing Balance");
                    accountBalanceMultiYear.ShowColumnHeader = true;
                    accountBalanceMultiYear.BindBalance(true);
                }
                else //for Multi Year Payments closing balance
                {
                    AccountBalanceMultiYear accountBalanceMultiYear = xrSubClosingBalanceMulti.ReportSource as AccountBalanceMultiYear;
                    SetReportSetting(dtReceiptPaymentsYear.DefaultView, accountBalanceMultiYear);
                    accountBalanceMultiYear.BankClosedDate = this.ReportProperties.DateFrom; //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                    //On 28/04/2023, to have proper Date from 
                    if (this.ReportProperties.NoOfYears > 0 && !string.IsNullOrEmpty(this.ReportProperties.DateFrom))
                    {
                        accountBalanceMultiYear.BankClosedDate = UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).AddYears(-this.ReportProperties.NoOfYears).ToShortDateString();
                    }
                    //xrLabelCLBal.Text = (IsMultiReceipts ? "Opening Balance" : "Closing Balance");
                    accountBalanceMultiYear.BindBalance(false);
                }
            }

            this.SetTitleWidth(this.PageWidth - 35);
            this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = this.PageWidth - 35;
        }

        private ResultArgs GetReportSource(bool ReceiptReport)
        {
            ResultArgs resultArgs = null;
            string sqlMultiAbstractReceipts = this.GetReportSQL(SQL.ReportSQLCommand.Report.MultiAbstractYear);            
            if (this.ReportProperties.ShowCCDetails == 1)
            {
                sqlMultiAbstractReceipts = this.GetReportSQL(SQL.ReportSQLCommand.Report.MultiAbstractYearWithCC);
            }
            string liquidityGroupIds = this.GetLiquidityGroupIds();

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, (ReceiptReport?  TransType.RC.ToString() :TransType.PY.ToString() ));
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, liquidityGroupIds);
                dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, (ReceiptReport?  TransMode.CR.ToString(): TransMode.DR.ToString()));
                dataManager.Parameters.Add(this.reportSetting1.MultiAbstract.NO_OF_YEARColumn, this.ReportProperties.NoOfYears);

                //On 06/12/2024 - To set currency details -----------------------------------------------------------------------------------
                if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, "0");
                }
                //---------------------------------------------------------------------------------------------------------------------------
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlMultiAbstractReceipts);
            }

            return resultArgs;
        }

        //private ResultArgs GetJournalSource(bool ReceiptReport)
        //{
        //    ResultArgs resultArgs = null;
        //    string sqlMonthlyJournalReceipt = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.FinalReceiptJournal);
        //    string dateProgress = this.GetProgressiveDate(this.ReportProperties.DateFrom);
        //    string liquidityGroupIds = this.GetLiquidityGroupIds();

        //    using (DataManager dataManager = new DataManager())
        //    {
        //        dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
        //        dataManager.Parameters.Add(this.ReportParameters.DATE_PROGRESS_FROMColumn, dateProgress);
        //        dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
        //        dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, TransType.JN.ToString());
        //        dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
        //        dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, liquidityGroupIds);
        //        dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, (ReceiptReport ? TransMode.CR.ToString() : TransMode.DR.ToString()));
        //        int LedgerPaddingRequired = (ReportProperties.ShowLedgerCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;
        //        int GroupPaddingRequired = (ReportProperties.ShowGroupCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;

        //        dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, LedgerPaddingRequired);
        //        dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, GroupPaddingRequired);
        //        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
        //        resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlMonthlyJournalReceipt);
        //    }

        //    return resultArgs;
        //}

        ///// <summary>
        ///// On 13/08/2018, to show TDS on FD interest for accumulate interest
        ///// We show FD renewal accumulated jounral entry interest amount in receipt side
        ///// After adding TDS entry along with FD interest, for Accumulated interest TDS amount should be added with Payment side
        ///// 
        ///// this method will retrn entries which are made on TDS on FD intererest ledger while renewing accumulated intrest
        ///// </summary>
        ///// <returns></returns>
        //private ResultArgs GetJournalTDSonFDInterestAmount()
        //{
        //    ResultArgs resultArgs = null;
        //    string sqlReceiptJournal = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.FetchTDSOnFDInterest);
        //    string dateProgress = this.GetProgressiveDate(this.ReportProperties.DateFrom);
        //    string liquidityGroupIds = this.GetLiquidityGroupIds();

        //    using (DataManager dataManager = new DataManager())
        //    {
        //        dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
        //        dataManager.Parameters.Add(this.ReportParameters.DATE_PROGRESS_FROMColumn, dateProgress);
        //        dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
        //        dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, TransType.JN.ToString());
        //        dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
        //        dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, liquidityGroupIds);
        //        dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, TransMode.CR.ToString());
        //        int LedgerPaddingRequired = (ReportProperties.ShowLedgerCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;
        //        int GroupPaddingRequired = (ReportProperties.ShowGroupCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;

        //        dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, LedgerPaddingRequired);
        //        dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, GroupPaddingRequired);

        //        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
        //        resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlReceiptJournal);
        //    }
        //    return resultArgs;
        //}

        private void SetReportSetting(DataView dvReceipt, AccountBalanceMultiYear accountBalanceMultiYear)
        {
           

            try { fieldGROUPCODE.Visible = true; }
            catch { }
            try { fieldLEDGERGROUP.Visible = true; }
            catch { }
            try { fieldLEDGERCODE.Visible = true; }
            catch { }
            try { fieldLEDGERNAME.Visible = true; }
            catch { }

            try
            {
                try { fieldGROUPCODE.AreaIndex = 0; }
                catch { }
                try { fieldLEDGERGROUP.AreaIndex = 1; }
                catch { }
                try { fieldLEDGERCODE.AreaIndex = 2; }
                catch { }
                try { fieldLEDGERNAME.AreaIndex = 3; }
                catch { }
                try { fieldCCName.AreaIndex = 4; }
                catch { }
                try { fieldDonorName.AreaIndex = 5; }
                catch { }
            }
            catch { }

            bool isGroupVisible = (ReportProperties.ShowByLedgerGroup == 1);
            bool isLedgerVisible = (ReportProperties.ShowByLedger == 1);
            if (isGroupVisible == false && isLedgerVisible == false) { isLedgerVisible = true; }
            bool isGroupCodeVisible = (isGroupVisible && (ReportProperties.ShowGroupCode == 1));
            bool isLedgerCodeVisible = (isLedgerVisible && (ReportProperties.ShowLedgerCode == 1));
            bool isHorizontalLine = (ReportProperties.ShowHorizontalLine == 1);
            bool isVerticalLine = (ReportProperties.ShowVerticalLine == 1);
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            //On 14/09/2022, to show Ledger Group Total-----------------------
            xrPGMultiAbstractReceipt.OptionsView.ShowRowTotals = false;
            if (ReportProperties.ShowByLedgerGroup == 1 && ReportProperties.ShowByLedger == 1)
            {
                //xrPGGrandTotal.KeepTogether = true;
                xrPGMultiAbstractReceipt.OptionsView.ShowRowTotals = true;
                xrPGMultiAbstractReceipt.Appearance.FieldValueTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
                fieldGROUPCODE.TotalsVisibility = PivotTotalsVisibility.None;
                fieldLEDGERCODE.TotalsVisibility = PivotTotalsVisibility.None;
                xrPGMultiAbstractReceipt.OptionsView.ShowTotalsForSingleValues = true;
                xrPGMultiAbstractReceipt.Styles.TotalCellStyle = xrPGMultiAbstractReceipt.Styles.GrandTotalCellStyle;
                xrPGMultiAbstractReceipt.Styles.TotalCellStyle.Padding = new DevExpress.XtraPrinting.PaddingInfo(5);
                //xrPGMultiAbstractReceipt.OptionsView.RowTotalsLocation = PivotRowTotalsLocation.Far;
            }
            //----------------------------------------------------------------

            //On 31/05/2023, To show CC/ Donor details --------------------------------------------------------------------------------------
            fieldCCName.Visible = (this.ReportProperties.ShowCCDetails == 1);
            fieldDonorName.Visible = (this.ReportProperties.ShowDonorDetails == 1);
            xrPGMultiAbstractReceipt.OptionsView.ShowRowGrandTotals = true;
            xrPGMultiAbstractReceipt.OptionsView.RowTotalsLocation = PivotRowTotalsLocation.Far;
            fieldLEDGERCODE.Caption = "Code";
            fieldDonorName.Caption = "Donor";
            fieldLEDGERNAME.Caption = "Particulars";
            if (this.ReportProperties.ShowCCDetails == 1 || this.ReportProperties.ShowDonorDetails == 1)
            {
                fieldLEDGERNAME.Visible = false;
                fieldLEDGERGROUP.Options.ShowTotals = false;
                fieldLEDGERNAME.Options.ShowTotals = false;
                fieldDonorName.Options.ShowTotals = false;
                fieldCCName.Options.ShowTotals = false;
                fieldLEDGERCODE.Options.ShowTotals = true;
                fieldGROUPCODE.TotalsVisibility = PivotTotalsVisibility.None;
                fieldLEDGERCODE.TotalsVisibility = PivotTotalsVisibility.AutomaticTotals;

                fieldCCName.Caption = fieldLEDGERNAME.Caption;
                fieldDonorName.Caption = "Donor";
                //fieldDonorName.Caption = (this.ReportProperties.ShowCCDetails == 1 && this.ReportProperties.ShowDonorDetails == 1) ? fieldDonorName.Caption : fieldLEDGERNAME.Caption;
                if ((this.ReportProperties.ShowCCDetails == 1 && this.ReportProperties.ShowDonorDetails == 1) ||
                   (this.ReportProperties.ShowCCDetails == 1 && this.ReportProperties.ShowDonorDetails == 0))
                    fieldCCName.Caption = fieldLEDGERNAME.Caption + " (Cost Centre)";
                else if (this.ReportProperties.ShowCCDetails == 0 && this.ReportProperties.ShowDonorDetails == 1)
                    fieldDonorName.Caption = fieldLEDGERNAME.Caption + " (Donor)";
                fieldLEDGERCODE.Caption = " ";

                if (this.ReportProperties.ShowByLedgerGroup == 1)
                {
                    fieldLEDGERGROUP.AreaIndex = 1;
                    fieldLEDGERCODE.AreaIndex = 2;
                    fieldLEDGERNAME.AreaIndex = 3;
                }
                else
                {
                    fieldLEDGERCODE.AreaIndex = 1;
                    fieldLEDGERNAME.AreaIndex = 2;
                }

                if (this.ReportProperties.ShowCCDetails == 1)
                    fieldCCName.AreaIndex = fieldLEDGERNAME.AreaIndex + 1;

                if (this.ReportProperties.ShowDonorDetails == 1)
                    fieldDonorName.AreaIndex = (this.ReportProperties.ShowCCDetails == 1) ? fieldCCName.AreaIndex + 1 : fieldLEDGERNAME.AreaIndex + 1;

                //xrPGMultiAbstractReceipt.KeepTogether = true;
                //this.VerticalContentSplitting = DevExpress.XtraPrinting.VerticalContentSplitting.Smart;

                xrPGMultiAbstractReceipt.OptionsView.ShowRowTotals = true;
                xrPGMultiAbstractReceipt.OptionsView.ShowRowGrandTotals = false;
                xrPGMultiAbstractReceipt.OptionsView.ShowTotalsForSingleValues = true;
                xrPGMultiAbstractReceipt.Appearance.FieldValueTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
                xrPGMultiAbstractReceipt.OptionsView.RowTotalsLocation = PivotRowTotalsLocation.Near;
                xrPGMultiAbstractReceipt.Styles.TotalCellStyle = xrPGMultiAbstractReceipt.Styles.GrandTotalCellStyle;
            }
            //------------------------------------------------------------------------------------------------------------------------------

            //On 14/03/2018, To set/reset amount column width based on Showcode
            fieldGROUPCODE.Width = 65;// 35;
            fieldLEDGERCODE.Width = (ReportProperties.ShowCCDetails == 1 || ReportProperties.ShowDonorDetails == 1 ? 0 : 60);

            //On 16/04/2021
            float fontsize = float.Parse("7.5");
            fieldLEDGERGROUP.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, fontsize, FontStyle.Bold);

            if (isLedgerCodeVisible || isGroupCodeVisible)
            {
                fieldLEDGERGROUP.Width = 135;//140 130; //90;
                fieldLEDGERNAME.Width = 225;//230 121 130;
                fieldACYEARNAME.Width = (isGroupVisible && isLedgerVisible) ? 95 : 105; //63 : 73; add 25
            }
            else
            {
                fieldLEDGERGROUP.Width = 120; //125 90;
                fieldLEDGERNAME.Width = 225; //220 118 130;
                fieldACYEARNAME.Width = (isGroupVisible && isLedgerVisible) ? 95 : 110; //66 : 76; add 25
            }

            if (ReportProperties.ShowCCDetails == 1 || ReportProperties.ShowDonorDetails == 1)
            {
                isLedgerVisible = false;
            }

            fieldCCName.Width = fieldLEDGERNAME.Width;
            fieldDonorName.Width =fieldLEDGERNAME.Width;
            if (ReportProperties.ShowCCDetails == 1 && ReportProperties.ShowDonorDetails == 1)
            {
                fieldACYEARNAME.Width = ReportProperties.NoOfYears >= 4 ? 90 : fieldACYEARNAME.Width;
                fieldLEDGERGROUP.Width = ReportProperties.NoOfYears >= 4 ? 120 : fieldLEDGERGROUP.Width;
                fieldCCName.Width = ReportProperties.NoOfYears >= 4 ? 150 : fieldLEDGERNAME.Width;
                fieldDonorName.Width = ReportProperties.NoOfYears >= 4 ? 150 : fieldLEDGERNAME.Width;
                
                if (ReportProperties.NoOfYears >= 4)
                {
                    fieldCCName.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, 7, FontStyle.Regular);
                    fieldDonorName.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, 7, FontStyle.Regular);
                    fieldLEDGERCODE.Appearance.TotalCell.Font = new Font(xrPGMultiAbstractReceipt.Styles.TotalCellStyle.Font.FontFamily, 7, FontStyle.Bold);
                    fieldAMOUNT.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.TotalCellStyle.Font.FontFamily, 7, FontStyle.Regular);
                    fieldAMOUNT.Appearance.TotalCell.Font = new Font(xrPGMultiAbstractReceipt.Styles.TotalCellStyle.Font.FontFamily, 7, FontStyle.Bold);

                    xrPGMultiAbstractReceipt.Styles.GrandTotalCellStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.GrandTotalCellStyle.Font.FontFamily, 7, FontStyle.Bold);
                    xrPGMultiAbstractReceipt.Styles.TotalCellStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.TotalCellStyle.Font.FontFamily, 7, FontStyle.Bold);
                }
            }

            try { fieldGROUPCODE.Visible = (isGroupCodeVisible); }
            catch { }
            try { fieldLEDGERGROUP.Visible = isGroupVisible; }
            catch { }
            try { fieldLEDGERCODE.Visible = (isLedgerCodeVisible); }
            catch { }
            try { fieldLEDGERNAME.Visible = isLedgerVisible; }
            catch { }

            //Grant Total Grid
            int rowWidth = 0;
            xrPGGrandTotal.OptionsView.ShowRowHeaders = false;
            xrPGGrandTotal.LeftF = xrPGMultiAbstractReceipt.LeftF;
            if (fieldGROUPCODE.Visible) { rowWidth = fieldGROUPCODE.Width; }
            if (fieldLEDGERGROUP.Visible) { rowWidth += fieldLEDGERGROUP.Width; }
            if (fieldLEDGERCODE.Visible) { rowWidth += fieldLEDGERCODE.Width; }
            if (fieldLEDGERNAME.Visible) { rowWidth += fieldLEDGERNAME.Width; }
            if (fieldCCName.Visible) { rowWidth += fieldCCName.Width; }
            if (fieldDonorName.Visible) { rowWidth += fieldDonorName.Width; }

            fieldGRANTTOTALPARTICULARS.Width = rowWidth;
            fieldGRANTTOTALMONTH.Width = fieldACYEARNAME.Width;
            fieldGRANTTOTALAMOUNT.Width = fieldAMOUNT.Width;

            //Grid Lines
            if (isHorizontalLine)
            {
                xrPGMultiAbstractReceipt.OptionsPrint.PrintHorzLines = DevExpress.Utils.DefaultBoolean.True;
            }
            else
            {
                xrPGMultiAbstractReceipt.OptionsPrint.PrintHorzLines = DevExpress.Utils.DefaultBoolean.False;
            }

            if (isVerticalLine)
            {
                xrPGMultiAbstractReceipt.OptionsPrint.PrintVertLines = DevExpress.Utils.DefaultBoolean.True;
            }
            else
            {
                xrPGMultiAbstractReceipt.OptionsPrint.PrintVertLines = DevExpress.Utils.DefaultBoolean.False;
            }

            //22/09/2020, To fix Border color based on settings
            xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
            xrPGMultiAbstractReceipt.Styles.CellStyle.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
            xrPGMultiAbstractReceipt.Styles.GrandTotalCellStyle.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;

            

            //Set Subreport Properties for opening
            xrSubOpeningBalanceMulti.LeftF = xrPGMultiAbstractReceipt.LeftF;

            //Set Subreport Properties for closing 
            xrSubClosingBalanceMulti.LeftF = xrPGMultiAbstractReceipt.LeftF;
            accountBalanceMultiYear.LeftPosition = (xrPGMultiAbstractReceipt.LeftF - 5);
            accountBalanceMultiYear.GroupCodeColumnWidth = fieldGROUPCODE.Width;
            accountBalanceMultiYear.GroupNameColumnWidth = fieldLEDGERGROUP.Width;
            accountBalanceMultiYear.LedgerCodeColumnWidth = fieldLEDGERCODE.Width;
            accountBalanceMultiYear.LedgerNameColumnWidth = fieldLEDGERNAME.Width;
            accountBalanceMultiYear.AmountColumnWidth = fieldACYEARNAME.Width;
            accountBalanceMultiYear.ApplyParentReportStyle = xrPGMultiAbstractReceipt.Styles;
            
            //Opening and Closing Balance with CC and Donor attached
            if (this.ReportProperties.ShowCCDetails == 1 || this.ReportProperties.ShowDonorDetails == 1)
            {
                if (this.ReportProperties.ShowCCDetails == 1 && this.ReportProperties.ShowDonorDetails == 1 )
                    accountBalanceMultiYear.LedgerNameColumnWidth = fieldCCName.Width + fieldDonorName.Width;
                else
                    accountBalanceMultiYear.LedgerNameColumnWidth = fieldCCName.Width;
            }  

            accountBalanceMultiYear.ShowColumnHeader = false;
        }

        private void BindGrandTotal(DataTable dtGrantTotal)
        {
            DataTable dtGrantTotalBalance = new DataTable();
            if (IsMultiReceipts)
            {
                AccountBalanceMultiYear accountBalanceMulti = xrSubOpeningBalanceMulti.ReportSource as AccountBalanceMultiYear;
                dtGrantTotalBalance = accountBalanceMulti.GrantTotalBalance;
            }
            else
            {
                AccountBalanceMultiYear accountBalanceMulti = xrSubClosingBalanceMulti.ReportSource as AccountBalanceMultiYear;
                dtGrantTotalBalance = accountBalanceMulti.GrantTotalBalance;
            }

            int rowIdx = 0;
            double amount = 0;

            foreach (DataRow drGrantTotalBal in dtGrantTotalBalance.Rows)
            {
                rowIdx = dtGrantTotalBalance.Rows.IndexOf(drGrantTotalBal);

                if (rowIdx < dtGrantTotal.Rows.Count)
                {
                    DataRow drGrantTotal = dtGrantTotal.Rows[rowIdx];
                    amount = this.UtilityMember.NumberSet.ToDouble(drGrantTotal[reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName].ToString());
                    amount += this.UtilityMember.NumberSet.ToDouble(drGrantTotalBal[reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName].ToString());

                    drGrantTotal.BeginEdit();
                    drGrantTotal[reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName] = amount;
                    drGrantTotal.EndEdit();
                }
            }
            dtGrantTotal.AcceptChanges();

            //Include Manual Total Receitps/Payments ----------------------------------------------------------------------
            if (fieldCCName.Visible || fieldDonorName.Visible)
            {
                double totVal = 0;
                double columnTotal = 0;
                int row = xrPGMultiAbstractReceipt.RowCount - 1;
                for (int col = 0; col < xrPGMultiAbstractReceipt.ColumnCount; col++)
                {
                    DataView dvBindData = xrPGMultiAbstractReceipt.DataSource as DataView;
                    DataTable t = dvBindData.Table;
                    totVal = GetTotal((col ));
                    columnTotal += totVal;
                    if ((col + 1) == xrPGMultiAbstractReceipt.ColumnCount)
                    {
                        totVal = columnTotal;
                    }
                    DataRow drGrantTotal = dtGrantTotal.NewRow();
                    drGrantTotal[reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName] = IsMultiReceipts ? "Total Receipts" : "Total Payments";
                    drGrantTotal[reportSetting1.MultiAbstract.AC_YEAR_NAMEColumn.ColumnName] = (col + 1);
                    drGrantTotal[reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName] = totVal;
                    dtGrantTotal.Rows.Add(drGrantTotal);
                }
            }
            //------------------------------------------------------------------------------------------------------------

            dtGrantTotal.TableName = "MultiAbstract";
            xrPGGrandTotal.DataSource = dtGrantTotal;
            xrPGGrandTotal.DataMember = dtGrantTotal.TableName;
            fieldGRANTTOTALPARTICULARS.SortOrder = PivotSortOrder.Descending;

            /*this.xrChart1.DataSource = dtGrantTotal;
            this.xrChart1.HeightF= 500;
            this.xrChart1.Series.Clear();

            this.xrChart1.Series.Add(new DevExpress.XtraCharts.Series("Total Receipts", DevExpress.XtraCharts.ViewType.Bar));
            this.xrChart1.Series[0].ArgumentDataMember = "AC_YEAR";
            this.xrChart1.Series[0].ValueDataMembers.AddRange(new string[] { "AMOUNT" });

            this.xrChart1.Series.Add(new DevExpress.XtraCharts.Series("Total Payments", DevExpress.XtraCharts.ViewType.Bar));
            this.xrChart1.Series[1].ArgumentDataMember = "AC_YEAR";
            this.xrChart1.Series[1].ValueDataMembers.AddRange(new string[] { "AMOUNT" });

            this.xrChart1.Series.Add(new DevExpress.XtraCharts.Series("Total Payments1", DevExpress.XtraCharts.ViewType.Bar));
            this.xrChart1.Series[2].ArgumentDataMember = "AC_YEAR";
            this.xrChart1.Series[2].ValueDataMembers.AddRange(new string[] { "AMOUNT" });

            ChartTitle tilte = new ChartTitle();
            tilte.Text = "Year Comparision";
            this.xrChart1.Titles.Add(tilte);
            
            tilte.Text = "Year Comparision 12345";
            this.xrChart1.Titles.Add(tilte);
            //this.xrChart1.Titles[0].Visible = false;*/
        }
        #endregion

        #region Events
        private void xrPGMultiAbstractReceipt_CustomFieldSort(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.Name == fieldACYEARNAME.Name)
            {
                if (e.Value1 != null && e.Value2 != null)
                {
                    //DateTime dt1 = DateTime.Parse(e.GetListSourceColumnValue(e.ListSourceRowIndex1, reportSetting1.MultiAbstract.MONTH_YEARColumn.ColumnName).ToString());
                    //DateTime dt2 = DateTime.Parse(e.GetListSourceColumnValue(e.ListSourceRowIndex2, reportSetting1.MultiAbstract.MONTH_YEARColumn.ColumnName).ToString());

                    DateTime dt1 = DateTime.Parse(e.GetListSourceColumnValue(e.ListSourceRowIndex1, this.reportSetting1.MultiAbstract.YEAR_FROMColumn.ColumnName).ToString());
                    DateTime dt2 = DateTime.Parse(e.GetListSourceColumnValue(e.ListSourceRowIndex2, this.reportSetting1.MultiAbstract.YEAR_FROMColumn.ColumnName).ToString());
                    
                    e.Result = Comparer.Default.Compare(dt1, dt2);
                    e.Handled = true;
                }
            }
        }

        private void xrPGMultiAbstractReceipt_FieldValueDisplayText(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
            {
                e.DisplayText = "Total " + (IsMultiReceipts ? "Receipts":"Payments");
            }
            else if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
            {
                if (e.Field == fieldLEDGERCODE)
                {
                    PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
                    DataTable dt = ConvertPivotDrillDownDataSourceToDataTable(ds);

                    if (dt.Rows.Count > 0)
                    {
                        string lcode = dt.Rows[0][reportSetting1.Ledger.LEDGER_CODEColumn.ColumnName].ToString().Trim();
                        string lname = dt.Rows[0][reportSetting1.Ledger.LEDGER_CODEColumn.ColumnName].ToString();
                        lname += (string.IsNullOrEmpty(lcode) ? "" : "  ") + dt.Rows[0][reportSetting1.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                        e.DisplayText = lname;
                    }
                }
                else
                {
                    e.DisplayText = "Total";
                }
            }
            else if (e.ValueType == PivotGridValueType.Value)
            {
                if (e.Field != null && e.Field == fieldLEDGERCODE && (ReportProperties.ShowCCDetails == 1 || ReportProperties.ShowDonorDetails == 1))
                {
                    e.DisplayText = string.Empty;
                }
            }
        }

        private void xrPGMultiAbstractReceipt_PrintFieldValue(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs e)
        {
            if (e.Field != null)
            {
                if (e.Field.Name == fieldLEDGERCODE.Name || e.Field.Name == fieldLEDGERNAME.Name || e.Field.Name == fieldCCName.Name || e.Field.Name == fieldDonorName.Name ||
                    e.Field.Name == fieldGROUPCODE.Name || e.Field.Name == fieldLEDGERGROUP.Name)
                {
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;

                    // On 06/06/2023, To allow split content to next page
                    /*//On 15/09/2002, If content is splitted into two pages (bottom text), let us fix to anyother page fully and keep space ---------
                    if (e.Brick.SeparableVert)
                    {
                        e.Brick.SeparableVert = false;
                    }
                    //-------------------------------------------------------------------------------------------------------------------------------*/

                }
                else if (e.Field.Name == fieldACYEARNAME.Name)
                {
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
                    e.Appearance.BorderColor = xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.BorderColor;
                    e.Appearance.BackColor = xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.BackColor;
                    e.Appearance.Font = xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font;
                }

                if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                {
                    //e.Appearance.ForeColor = xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.ForeColor;
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;

                    if (e.Field != null && e.Field == fieldLEDGERCODE)
                    {
                        e.Appearance.Font = fieldLEDGERCODE.Appearance.TotalCell.Font;
                        e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
                        e.Appearance.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
                        e.Appearance.WordWrap = true;
                    }
                    else
                    {
                        e.Appearance.Font = xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.Font;
                    }
                }

                if (e.Field.Name == fieldACYEARNAME.Name)
                {
                    if (xrPGMultiAbstractReceipt.OptionsView.ShowRowHeaders == false)
                    {
                        e.Brick.Text = "";
                        e.Brick.BorderWidth = 0;
                        e.Appearance.BackColor = Color.White;

                        DevExpress.XtraPrinting.TextBrick textBrick = e.Brick as DevExpress.XtraPrinting.TextBrick;
                        textBrick.Size = new SizeF(textBrick.Size.Width, 0);
                        e.Field.Options.ShowValues = false;
                    }
                }
            }

            if (e.ValueType == PivotGridValueType.GrandTotal)
            {
                if (e.IsColumn)
                {
                    e.Appearance.BackColor = xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.BackColor;
                }
            }
        }

        private void xrPGGrandTotal_PrintFieldValue(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs e)
        {
            if (e.Field != null)
            {
                if (e.Field.Name == fieldGRANTTOTALPARTICULARS.Name)
                {
                    e.Appearance.BackColor = xrPGGrandTotal.Styles.GrandTotalCellStyle.BackColor;
                }
                else if (e.Field.Name == fieldGRANTTOTALMONTH.Name)
                {
                    if (xrPGGrandTotal.OptionsView.ShowRowHeaders == false)
                    {
                        e.Brick.Text = "";
                        e.Brick.BorderWidth = 0;
                        e.Appearance.BackColor = Color.White;

                        DevExpress.XtraPrinting.TextBrick textBrick = e.Brick as DevExpress.XtraPrinting.TextBrick;
                        textBrick.Size = new SizeF(textBrick.Size.Width, 0);
                        //e.Field.Options.ShowValues = false;
                    }
                }
            }
        }

        private void xrPGMultiAbstractReceipt_PrintCell(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportCellEventArgs e)
        {
            if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.TotalCell)
            {
                //e.Appearance.ForeColor = xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.ForeColor;
                e.Appearance.Font = xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.Font;
                e.Appearance.Font = fieldAMOUNT.Appearance.TotalCell.Font;
            }

            if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.Cell)
            {
                if (e.Brick.TextValue == null || this.UtilityMember.NumberSet.ToDouble(e.Brick.TextValue.ToString()) == 0) { e.Brick.Text = ""; }
                e.Appearance.Font = fieldAMOUNT.Appearance.FieldValue.Font;
            }
        }

        private void xrPGMultiAbstractReceipt_AfterPrint(object sender, EventArgs e)
        {
            DataTable dtGrantTotal = new DataTable();
            dtGrantTotal.Columns.Add(reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName, typeof(string));
            dtGrantTotal.Columns.Add(reportSetting1.MultiAbstract.AC_YEAR_NAMEColumn.ColumnName, typeof(int));
            dtGrantTotal.Columns.Add(reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName, typeof(double));
            dtGrantTotal.Columns.Add("AC_YEAR", typeof(string));
            object oTotVal = null;
            double totVal = 0;
            double ColumnTotal = 0;
            int row = xrPGMultiAbstractReceipt.RowCount - 1;

            for (int col = 0; col < xrPGMultiAbstractReceipt.ColumnCount; col++)
            {
                oTotVal ="0";
                if (fieldCCName.Visible || fieldDonorName.Visible)
                {
                    totVal = GetTotal(col);
                    ColumnTotal += totVal;
                    if ((col + 1) == xrPGMultiAbstractReceipt.ColumnCount)
                    {
                        totVal = ColumnTotal;
                    }
                }
                else
                {
                    if (xrPGMultiAbstractReceipt.GetCellValue(col, row) != null)
                        oTotVal = xrPGMultiAbstractReceipt.GetCellValue(col, row);
                    totVal = this.UtilityMember.NumberSet.ToDouble(oTotVal.ToString());
                }

                DataRow drGrantTotal = dtGrantTotal.NewRow();
                drGrantTotal[reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName] = "Grand Total";
                drGrantTotal[reportSetting1.MultiAbstract.AC_YEAR_NAMEColumn.ColumnName] = (col + 1);
                drGrantTotal[reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName] = totVal;
                if (xrPGMultiAbstractReceipt.GetFieldValue(fieldACYEARNAME, col) != null)
                {
                    drGrantTotal["AC_YEAR"] = xrPGMultiAbstractReceipt.GetFieldValue(fieldACYEARNAME, col);
                }
                else
                {
                    //Total Receipts and Payments
                    if (IsMultiReceipts)
                    {
                        TotalReceipts = totVal;
                    }
                    else
                    {
                        TotalPayments = totVal;
                    }
                    drGrantTotal["AC_YEAR"] = "Total";
                }

                dtGrantTotal.Rows.Add(drGrantTotal);
            }
            dtGrantTotal.AcceptChanges();

            //On 23/01/2021, to keep total  receitps and payments without Cash/Bank/FD opening and closing balance
            RPTotalTable = dtGrantTotal.DefaultView.ToTable();

            BindGrandTotal(dtGrantTotal);
        }

        private void xrPGMultiAbstractReceipt_CustomCellValue(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotCellValueEventArgs e)
        {
            
        }
        #endregion

        private void xrPGMultiAbstractReceipt_CustomFieldValueCells(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotCustomFieldValueCellsEventArgs e)
        {
            //If particular year does not contain data, empty row will be displayed, it should be removed
            if (!rowemptyremoved)
            {
                bool isrowempty = false;
                for (int j = 0; j < e.ColumnCount; j++)
                {
                    DevExpress.XtraReports.UI.PivotGrid.FieldValueCell cell = e.GetCell(false, j);
                    if (cell != null && cell.Field != null && cell.Field.FieldName == "LEDGER_NAME")
                    {
                        isrowempty = string.IsNullOrEmpty(cell.Value.ToString());
                        break;
                    }
                }

                if (isrowempty)
                {
                    for (int j = 0; j < e.ColumnCount; j++)
                    {
                        DevExpress.XtraReports.UI.PivotGrid.FieldValueCell cell = e.GetCell(false, j);

                        if (cell == null) continue;
                        if (cell.EndLevel == e.GetLevelCount(false) - 1)
                        {
                            if (cell.Field != null && rowemptyremoved == false)
                            {
                                e.Remove(cell);
                                rowemptyremoved = true; ;
                            }
                        }
                    }
                }

                /*for (int i = e.GetCellCount(false) - 1; i >= 0; i--)
                {
                    DevExpress.XtraReports.UI.PivotGrid.FieldValueCell cell = e.GetCell(false, i);
                    if (cell != null)
                    {
                        if (object.Equals(cell.Value, string.Empty))
                        {
                            //e.Remove(cell);
                        }
                    }
                }*/
            }
        }

        private void xrPGMultiAbstractReceipt_CustomRowHeight(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotCustomRowHeightEventArgs e)
        {
            int defaultrowheight = e.RowHeight;//Default height
            try
            {
                if (e.Field != null)
                {
                    if (e.ValueType != PivotGridValueType.Total && e.ValueType != PivotGridValueType.GrandTotal)
                    {
                        if (e.Field.Name == fieldLEDGERNAME.Name || e.Field.Name == fieldLEDGERGROUP.Name || e.Field.Name == fieldCCName.Name || e.Field.Name == fieldDonorName.Name)
                        {
                            fieldLEDGERGROUP.Appearance.Cell.WordWrap = true;
                            fieldLEDGERGROUP.Appearance.FieldValue.WordWrap = true;
                            fieldLEDGERNAME.Appearance.Cell.WordWrap = true;
                            fieldLEDGERNAME.Appearance.FieldValue.WordWrap = true;
                            fieldCCName.Appearance.Cell.WordWrap = true;
                            fieldCCName.Appearance.FieldValue.WordWrap = true;
                            fieldDonorName.Appearance.Cell.WordWrap = true;
                            fieldDonorName.Appearance.FieldValue.WordWrap = true;

                            e.RowHeight = defaultrowheight;
                            string ledgergroup = string.Empty;
                            string ledgername = string.Empty;
                            string ccname = string.Empty;
                            string donorname = string.Empty;
                            Int32 RowHeightLedgerName, RowHeightccName = 0, RowHeightDonorName = 0, RowHeightLedgerGroup = 0;
                            if (fieldLEDGERNAME.Visible)
                            {
                                if (e.GetFieldValue(e.Field, e.RowIndex) != null)
                                {
                                    ledgername = e.GetFieldValue(e.Field, e.RowIndex).ToString().Trim();
                                }
                            }
                            SizeF size = gr.MeasureString(ledgername, xrPGMultiAbstractReceipt.Styles.CellStyle.Font, fieldLEDGERNAME.Width);
                            RowHeightLedgerName = Convert.ToInt32(size.Height + 0.5);

                            if (fieldCCName.Visible)
                            {
                                if (e.GetFieldValue(fieldCCName, e.RowIndex) != null)
                                {
                                    ccname = e.GetFieldValue(fieldCCName, e.RowIndex).ToString().Trim();
                                }
                                size = gr.MeasureString(ccname, xrPGMultiAbstractReceipt.Styles.CellStyle.Font, fieldCCName.Width);
                                RowHeightccName = Convert.ToInt32(size.Height + 0.5);
                                RowHeightLedgerName = Math.Max(RowHeightLedgerName, RowHeightccName);
                            }

                            if (fieldDonorName.Visible)
                            {
                                if (e.GetFieldValue(fieldDonorName, e.RowIndex) != null)
                                {
                                    donorname = e.GetFieldValue(fieldDonorName, e.RowIndex).ToString().Trim();
                                }
                                size = gr.MeasureString(donorname, xrPGMultiAbstractReceipt.Styles.CellStyle.Font, fieldDonorName.Width);
                                RowHeightDonorName = Convert.ToInt32(size.Height + 0.5);
                                RowHeightLedgerName = Math.Max(RowHeightLedgerName, RowHeightDonorName);
                            }

                            if (fieldLEDGERGROUP.Visible)
                            {
                                ledgergroup = e.GetFieldValue(fieldLEDGERGROUP, e.RowIndex).ToString();
                                size = gr.MeasureString(ledgergroup, fieldLEDGERGROUP.Appearance.FieldHeader.Font, fieldLEDGERGROUP.Width);
                                RowHeightLedgerGroup = Convert.ToInt32(size.Height + 5);// Convert.ToInt32(size.Height + 0.5);
                                if (string.IsNullOrEmpty (ledgergroup)) RowHeightLedgerGroup = 0;
                            }

                            e.RowHeight = Math.Max(RowHeightLedgerName, RowHeightLedgerGroup);
                        }
                    }
                    else if (e.ValueType == PivotGridValueType.Total)
                    {
                        //On 16/09/2022, To hide empty row group (Ledger Group), as unable remove empty ledger group ---------------
                        //e.RowIndex == 0 &&
                        if (e.Data.GetAvailableFieldValues(fieldLEDGERGROUP) != null &&
                                 ReportProperties.ShowByLedgerGroup == 1 && ReportProperties.ShowByLedger == 1)
                        {
                            //If Ledger Group value is empty,
                            string ledgergroup = (e.GetFieldValue(fieldLEDGERGROUP, e.RowIndex) != null ? e.GetFieldValue(fieldLEDGERGROUP, e.RowIndex).ToString() : string.Empty);
                            if (string.IsNullOrEmpty(ledgergroup))
                            {
                                e.RowHeight = 0;
                            }
                        }
                        //-----------------------------------------------------------------------------------------------------------
                    }

                    if (ReportProperties.ShowCCDetails == 1 || ReportProperties.ShowDonorDetails == 1)
                    {
                        PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
                        DataTable dt = ConvertPivotDrillDownDataSourceToDataTable(ds);
                        if (dt.Rows.Count > 0)
                        {
                            string lgroup = dt.Rows[0][fieldLEDGERGROUP.FieldName].ToString();
                            string lname = dt.Rows[0][fieldLEDGERNAME.FieldName].ToString();
                            string lcode = dt.Rows[0][fieldLEDGERCODE.FieldName].ToString();
                            lname = lcode + "  " + lname;
                            string ccname = dt.Columns.Contains(fieldCCName.FieldName) ? dt.Rows[0][fieldCCName.FieldName].ToString() : string.Empty;
                            string donorname = dt.Rows[0][fieldDonorName.FieldName].ToString();

                            if (string.IsNullOrEmpty(lname.Trim()) && string.IsNullOrEmpty(ccname) && string.IsNullOrEmpty(donorname)
                                && ReportProperties.ShowCCDetails == 1 && ReportProperties.ShowDonorDetails == 1)
                            {
                                e.RowHeight = 0;
                            }

                            if (e.ValueType == PivotGridValueType.Value)
                            {
                                Int32 TotalCounts = dt.Rows.Count;
                                string filter = "";
                                if (ReportProperties.ShowCCDetails == 1)
                                    filter = "ISNULL(" + fieldCCName.FieldName + ",'')=''";

                                if (ReportProperties.ShowDonorDetails == 1)
                                    filter += (string.IsNullOrEmpty(filter) ? "" : " AND ") + "ISNULL(" + fieldDonorName.FieldName + ",'')=''";

                                dt.DefaultView.RowFilter = filter;
                                if (dt.DefaultView.Count == TotalCounts)
                                {
                                    e.RowHeight = 0;
                                }
                            }
                            else if (e.ValueType==PivotGridValueType.Total)
                            {
                                if (string.IsNullOrEmpty(lgroup) && string.IsNullOrEmpty(lname.Trim()) && string.IsNullOrEmpty(donorname) && string.IsNullOrEmpty(ccname))
                                {
                                    e.RowHeight = 0;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                e.RowHeight = defaultrowheight;//Default height
                MessageRender.ShowMessage("Not able to set row right " + err.Message);
            }
        }

        private DataTable ConvertPivotDrillDownDataSourceToDataTable(PivotDrillDownDataSource source)
        {
            DataTable result = new DataTable();
            ITypedList dataProperties = source as ITypedList;
            if (dataProperties == null) return result;
            foreach (PropertyDescriptor prop in dataProperties.GetItemProperties(null))
                result.Columns.Add(prop.Name, prop.PropertyType);
            for (int row = 0; row < source.RowCount; row++)
            {
                List<object> values = new List<object>();
                foreach (DataColumn col in result.Columns)
                    values.Add(source.GetValue(row, col.ColumnName));
                result.Rows.Add(values.ToArray());
            }
            return result;
        }

        private double GetTotal(Int32 Qno)
        {
            double rtn = 0;
            DataView dvBindData = xrPGMultiAbstractReceipt.DataSource as DataView;
            DataTable dtACyears = dvBindData.ToTable(true, new string[] { reportSetting1.MultiAbstract.AC_YEAR_NAMEColumn.ColumnName });
            dtACyears.DefaultView.Sort = reportSetting1.MultiAbstract.AC_YEAR_NAMEColumn.ColumnName;
            if (dtACyears.DefaultView.Count > 0 && Qno<dtACyears.DefaultView.Count)
            {
                string filter = reportSetting1.MultiAbstract.AC_YEAR_NAMEColumn.ColumnName + "='" + dtACyears.DefaultView[Qno][reportSetting1.MultiAbstract.AC_YEAR_NAMEColumn.ColumnName].ToString() + "'"; 
                string fldname = reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName;
                object oTotVal = dvBindData.Table.Compute("SUM(" + fldname + ")", filter);

                if (oTotVal != null) rtn = UtilityMember.NumberSet.ToDouble(oTotVal.ToString());
            }
            return rtn;
        }
    }
}
