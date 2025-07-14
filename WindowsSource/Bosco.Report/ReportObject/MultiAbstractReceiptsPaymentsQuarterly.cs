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
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class MultiAbstractReceiptsPaymentsQuarterly : Bosco.Report.Base.ReportHeaderBase
    {
        public bool ReceiptsAndPayments = false;
        bool IsMultiReceipts = true;
        bool rowemptyremoved = false;
        private Graphics gr = Graphics.FromHwnd(IntPtr.Zero);
        public int NoOfQuters = 1;

        public bool IsLandscapeReport
        {
            get
            {
                return this.Landscape;
            }
        }

        public int ReportLeftMargin
        {
            get
            {
                return this.Margins.Left;
            }
        }

        public MultiAbstractReceiptsPaymentsQuarterly()
        {
            InitializeComponent();

            CheckDateRangeByQuater();
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
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                || String.IsNullOrEmpty(this.ReportProperties.Project) || this.ReportProperties.Project=="0")
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
                        BindMultiAbstractReceiptSource();
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
                    BindMultiAbstractReceiptSource();
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }

        }
        #endregion

        #region Methods
        public void BindMultiAbstractReceiptSource(bool IsReceipts=true)
        {
            //On 29/05/2023, To set if ledger is not selected if CC or Donor selected
            if (ReportProperties.ShowCCDetails == 1 || ReportProperties.ShowDonorDetails == 1)
            {
                ReportProperties.ShowByLedger = 1;
                ReportProperties.ShowLedgerCode = 1;
            } 
            
            if (this.ReportProperties.ReportId == "RPT-176")
            {
                IsMultiReceipts = IsReceipts;
                ReceiptsAndPayments = true;
            }
            else
            {
                IsMultiReceipts = (this.ReportProperties.ReportId == "RPT-175" ? false : true);
                ReceiptsAndPayments = false;
            }

            setHeaderTitleAlignment();
            SetReportTitle();
            //this.HideDateRange = false;            
            grpOpeningBalane.Visible = xrSubOpeningBalanceMulti.Visible = xrLabelCLBal.Visible = false;
            detailClosingBalance.Visible =  xrSubClosingBalanceMulti.Visible = xrLabelCLBal.Visible = false;
            xrPGMultiAbstractReceipt.KeepTogether = false;

            if (IsMultiReceipts)
            {
                grpOpeningBalane.Visible = xrSubOpeningBalanceMulti.Visible = true;
                xrLabelCLBal.Visible = false;
            }
            else
            {
                this.Name = "MultiAbstractPaymentsQuaterly";
                detailClosingBalance.Visible = xrSubClosingBalanceMulti.Visible = xrLabelCLBal.Visible = true;
            }

            ResultArgs resultArgs = GetReportSource(IsMultiReceipts);
            if (resultArgs.Success && resultArgs.DataSource.Table!=null)
            {
                //On 16/04/2020, to have proper quter number based on FY (April-March)
                if (this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Month==4)
                {
                    fieldQUATERNO.FieldName = this.reportSetting1.MultiAbstract.QUATER_NOColumn.ColumnName;
                }
                else if (this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Month==1) // (Jan-Feb)
                {
                    fieldQUATERNO.FieldName = this.reportSetting1.MultiAbstract.REAL_QUATER_NOColumn .ColumnName;
                }
                
                DataTable dtReceiptPaymentsQuaterly = resultArgs.DataSource.Table;
                if (dtReceiptPaymentsQuaterly != null)
                {
                    dtReceiptPaymentsQuaterly.TableName = "MultiAbstract";
                    xrPGMultiAbstractReceipt.DataSource = dtReceiptPaymentsQuaterly.DefaultView;
                    xrPGMultiAbstractReceipt.DataMember = dtReceiptPaymentsQuaterly.TableName;
                }

                if (IsMultiReceipts) //for Multi Quaterly Receipts Opening Balance
                {
                    AccountBalanceMultiYear accountBalanceMultiYear = xrSubOpeningBalanceMulti.ReportSource as AccountBalanceMultiYear;
                    SetReportSetting(dtReceiptPaymentsQuaterly.DefaultView, accountBalanceMultiYear);
                    accountBalanceMultiYear.BankClosedDate = this.ReportProperties.DateFrom; //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                    //xrLabelCLBal.Text = (IsMultiReceipts ? "Opening Balance" : "Closing Balance");
                    accountBalanceMultiYear.ShowColumnHeader = true;
                    accountBalanceMultiYear.IsQuaterly = true;
                    accountBalanceMultiYear.BindBalanceQuartely(true);
                }
                else //for Multi Year Payments closing balance
                {
                    AccountBalanceMultiYear accountBalanceMultiYear = xrSubClosingBalanceMulti.ReportSource as AccountBalanceMultiYear;
                    SetReportSetting(dtReceiptPaymentsQuaterly.DefaultView, accountBalanceMultiYear);
                    accountBalanceMultiYear.BankClosedDate = this.ReportProperties.DateFrom; //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                    //xrLabelCLBal.Text = (IsMultiReceipts ? "Opening Balance" : "Closing Balance");
                    accountBalanceMultiYear.IsQuaterly = true;
                    accountBalanceMultiYear.BindBalanceQuartely(false);
                }
            }

        }

        private ResultArgs GetReportSource(bool ReceiptReport)
        {
            ResultArgs resultArgs = null;
            string sqlMultiAbstractReceipts = this.GetReportSQL(SQL.ReportSQLCommand.Report.MultiAbstractQuaterly);
            string liquidityGroupIds = this.GetLiquidityGroupIds();

            if (this.ReportProperties.ShowCCDetails == 1)
            {
                sqlMultiAbstractReceipts = this.GetReportSQL(SQL.ReportSQLCommand.Report.MultiAbstractQuaterlyWithCC);
            }

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, (ReceiptReport?  TransType.RC.ToString() :TransType.PY.ToString() ));
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, liquidityGroupIds);
                dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, (ReceiptReport?  TransMode.CR.ToString(): TransMode.DR.ToString()));

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
               
                
        private void SetReportSetting(DataView dvReceipt, AccountBalanceMultiYear accountBalanceMultiYear)
        {
            bool isGroupVisible = (ReportProperties.ShowByLedgerGroup == 1);
            bool isLedgerVisible = (ReportProperties.ShowByLedger == 1);
            if (isGroupVisible == false && isLedgerVisible == false) { isLedgerVisible = true; }
            bool isGroupCodeVisible = (isGroupVisible && (ReportProperties.ShowGroupCode == 1));
            bool isLedgerCodeVisible = (isLedgerVisible && (ReportProperties.ShowLedgerCode == 1));
            bool isHorizontalLine = (ReportProperties.ShowHorizontalLine == 1);
            bool isVerticalLine = (ReportProperties.ShowVerticalLine == 1);
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            if (ReportProperties.ShowCCDetails == 1 || ReportProperties.ShowDonorDetails== 1)
            {
                isLedgerVisible = false;
            }

            //Include / Exclude Code
            try { fieldGROUPCODE.Visible = (isGroupCodeVisible); }
            catch { }
            try { fieldLEDGERGROUP.Visible = isGroupVisible; }
            catch { }
            try { fieldLEDGERCODE.Visible = (isLedgerCodeVisible); }
            catch { }
            try { fieldLEDGERNAME.Visible = isLedgerVisible; }
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
            
            //On 14/09/2022, to show Ledger Group Total-----------------------
            xrPGMultiAbstractReceipt.OptionsView.ShowRowTotals = false;
            if (ReportProperties.ShowByLedgerGroup == 1 && ReportProperties.ShowByLedger == 1)
            {
                //xrPGMultiAbstractReceipt.KeepTogether = true;
                //this.VerticalContentSplitting = DevExpress.XtraPrinting.VerticalContentSplitting.Smart;
                fieldLEDGERGROUP.Options.ShowTotals = true;    
                xrPGMultiAbstractReceipt.OptionsView.ShowRowTotals = true;
                xrPGMultiAbstractReceipt.Appearance.FieldValueTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
                xrPGMultiAbstractReceipt.OptionsView.RowTotalsLocation = PivotRowTotalsLocation.Far;
                fieldGROUPCODE.TotalsVisibility = PivotTotalsVisibility.None;
                fieldLEDGERCODE.TotalsVisibility = PivotTotalsVisibility.None;

                xrPGMultiAbstractReceipt.OptionsView.ShowTotalsForSingleValues = true;
                xrPGMultiAbstractReceipt.Styles.TotalCellStyle = xrPGMultiAbstractReceipt.Styles.GrandTotalCellStyle;
            }
            //----------------------------------------------------------------

            //On 29/05/2023, To show CC/ Donor details --------------------------------------------------------------------------------------
            fieldCCName.Visible = (this.ReportProperties.ShowCCDetails == 1);
            fieldDonorName.Visible = (this.ReportProperties.ShowDonorDetails == 1);
            xrPGMultiAbstractReceipt.OptionsView.ShowRowGrandTotals = true;
            xrPGMultiAbstractReceipt.OptionsView.RowTotalsLocation = PivotRowTotalsLocation.Far;
            fieldAMOUNT.FieldName = "AMOUNT";
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
                fieldLEDGERCODE.Caption =  " " ;

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
                
                //fieldAMOUNT.FieldName = this.ReportProperties.ShowCCDetails == 1 ? "CC_AMOUNT" : "AMOUNT";
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
            fieldGROUPCODE.Width = 35;
            fieldLEDGERCODE.Width = 35;
            if (isLedgerCodeVisible || isGroupCodeVisible)
            {
                fieldLEDGERGROUP.Width = 130; //90;
                fieldLEDGERNAME.Width = 220; //121 130;
                fieldQUATERNO.Width = (isGroupVisible && isLedgerVisible) ? 90 : 100; //63 : 73; add 25
            }
            else
            {
                fieldLEDGERGROUP.Width = 125; //90;
                fieldLEDGERNAME.Width = 215; //118 130;
                fieldQUATERNO.Width = (isGroupVisible && isLedgerVisible) ? 90 : 105; //66 : 76; add 25
            }
            fieldCCName.Width = fieldLEDGERNAME.Width;
            fieldDonorName.Width = fieldLEDGERNAME.Width;

            //01/03/2019, fix paper oriantaion and font based on selected months
            SetFieldFontsPaperSize();

            //Include / Exclude Code
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
            fieldGRANTTOTALMONTH.Width = fieldQUATERNO.Width;
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
            xrPGMultiAbstractReceipt.Styles.TotalCellStyle.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;

            //Set Subreport Properties for opening
            xrSubOpeningBalanceMulti.LeftF = xrPGMultiAbstractReceipt.LeftF;

            //Set Subreport Properties for closing 
            xrSubClosingBalanceMulti.LeftF = xrPGMultiAbstractReceipt.LeftF;
            accountBalanceMultiYear.LeftPosition = (xrPGMultiAbstractReceipt.LeftF - 5);
            accountBalanceMultiYear.GroupCodeColumnWidth = fieldGROUPCODE.Width;
            accountBalanceMultiYear.GroupNameColumnWidth = fieldLEDGERGROUP.Width  ;
            accountBalanceMultiYear.LedgerNameColumnWidth = fieldLEDGERNAME.Width;
            accountBalanceMultiYear.LedgerCodeColumnWidth = fieldLEDGERCODE.Width;
            accountBalanceMultiYear.AmountColumnWidth = fieldQUATERNO.Width;
            
            //Opening and Closing Balance with CC and Donor attached
            if (this.ReportProperties.ShowCCDetails == 1 || this.ReportProperties.ShowDonorDetails == 1)
            {
                if (this.ReportProperties.ShowCCDetails == 1 && this.ReportProperties.ShowDonorDetails == 1 &&  this.ReportProperties.ShowByLedgerGroup == 1)
                    accountBalanceMultiYear.LedgerNameColumnWidth = fieldCCName.Width +  fieldDonorName.Width;
                else if (this.ReportProperties.ShowCCDetails == 1 && this.ReportProperties.ShowDonorDetails == 1 &&  this.ReportProperties.ShowByLedgerGroup == 0)
                    accountBalanceMultiYear.LedgerNameColumnWidth = fieldCCName.Width + fieldDonorName.Width;
                else
                    accountBalanceMultiYear.LedgerNameColumnWidth = fieldCCName.Width;
            }
            
            accountBalanceMultiYear.ShowColumnHeader = false;
            accountBalanceMultiYear.ApplyParentReportStyle = xrPGMultiAbstractReceipt.Styles;

        }

        //01/03/2019, fix paper oriantaion and font based on selected months
        private void SetFieldFontsPaperSize()
        {
            bool isGroupVisible = (ReportProperties.ShowByLedgerGroup == 1);
            bool isLedgerVisible = (ReportProperties.ShowByLedger == 1);
            if (isGroupVisible == false && isLedgerVisible == false) { isLedgerVisible = true; }
            bool isGroupCodeVisible = (isGroupVisible && (ReportProperties.ShowGroupCode == 1));
            bool isLedgerCodeVisible = (isLedgerVisible && (ReportProperties.ShowLedgerCode == 1));

            List<Tuple<DateTime, DateTime>> quarterDates = UtilityMember.DateSet.GetQuarterDates(UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false), UtilityMember.DateSet.ToDate(ReportProperties.DateTo, false));
            NoOfQuters = quarterDates.Count;

            /*if (ReportProperties.ShowCCDetails == 1 || ReportProperties.ShowDonorDetails== 1)
            {
                isLedgerVisible = false;
            }*/

            //Int32 noofonths = UtilityMember.DateSet.ToDate(ReportProperties.DateTo, false).Month - UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).Month;
            this.Margins.Left = 20;
            this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = this.PageWidth - 25;
            this.SetTitleWidth(this.PageWidth - 25);
            float realfont = (float)10;
            if (NoOfQuters <= 2 && (ReportProperties.ShowCCDetails == 0 || ReportProperties.ShowDonorDetails == 0))
            {
                this.Landscape = false;
                fieldGROUPCODE.Width = 70;
                fieldLEDGERCODE.Width = (ReportProperties.ShowCCDetails == 1 || ReportProperties.ShowDonorDetails== 1 ? 0:60); //70 
                fieldLEDGERGROUP.Width = isGroupVisible && isLedgerVisible ? 180 : 235; //121,90;
                fieldLEDGERNAME.Width = isGroupVisible && isLedgerVisible ? 190 : 235; //121,90;
                fieldCCName.Width = isGroupVisible && isLedgerVisible ? 190 : 235; //121,90;
                fieldDonorName.Width = isGroupVisible && isLedgerVisible ? 190 : 235; //121,90;
                fieldQUATERNO.Width = isGroupVisible && isLedgerVisible ? 108 : 115; //121,90;
                realfont = (float)10;

                if (ReportProperties.ShowCCDetails == 1 || ReportProperties.ShowDonorDetails == 1)
                {
                    fieldLEDGERGROUP.Width = (NoOfQuters <= 2 || ReportProperties.ShowByLedgerGroup == 0) ? 180 : 180; //200 : 190;
                    fieldLEDGERNAME.Width = fieldCCName.Width = fieldDonorName.Width = (NoOfQuters <= 2 || ReportProperties.ShowByLedgerGroup == 0) ? 210 : 200;
                    fieldAMOUNT.Width = (NoOfQuters <= 2 || ReportProperties.ShowByLedgerGroup == 0) ? 90 : 90;
                    //realfont = (ReportProperties.ShowByLedgerGroup == 0) ? (float)9.5 : (float)9;
                }
                else
                {
                    fieldLEDGERGROUP.Width = (NoOfQuters <= 2 || ReportProperties.ShowByLedgerGroup == 1) ?  160 :fieldLEDGERGROUP.Width ;
                }

                this.Margins.Left = 35;
                this.SetTitleWidth(650);
                setHeaderTitleAlignment();
            }
            else if (NoOfQuters <= 3 && (ReportProperties.ShowCCDetails == 0 && ReportProperties.ShowDonorDetails == 0))
            {
                this.Landscape = false;
                fieldGROUPCODE.Width = 70;
                fieldLEDGERCODE.Width = (ReportProperties.ShowCCDetails == 1 || ReportProperties.ShowDonorDetails == 1 ? 0 : 55); //60 //70
                fieldLEDGERGROUP.Width = isGroupVisible && isLedgerVisible ? 125 : 220; //130 : 225 //121,90;
                fieldLEDGERNAME.Width = isGroupVisible && isLedgerVisible ? 125 : 220;  //130 : 225 //121,90;
                fieldCCName.Width = isGroupVisible && isLedgerVisible ? 125 : 220; //135 : 225 //121,90;
                fieldDonorName.Width = isGroupVisible && isLedgerVisible ? 125 : 220; //135 : 225 //121,90;
                fieldQUATERNO.Width = isGroupVisible && isLedgerVisible ? 108 : 115; //121,90;

                //fieldLEDGERGROUP.Width = (NoOfQuters <= 3 || ReportProperties.ShowByLedgerGroup == 1) ? 120 : fieldLEDGERGROUP.Width;
                realfont = (float)10;
                this.Margins.Left = 35;
                this.SetTitleWidth(650);
                setHeaderTitleAlignment();
            }
            else
            {
                this.Landscape = true;
                fieldGROUPCODE.Width = 70;//50;
                fieldLEDGERCODE.Width = (ReportProperties.ShowCCDetails == 1 || ReportProperties.ShowDonorDetails == 1 ? 0 : 55); //60 //70
                fieldLEDGERGROUP.Width = isGroupVisible && isLedgerVisible ? 190 : 250; //200 : 250 //121,90;
                fieldLEDGERNAME.Width = isGroupVisible && isLedgerVisible ? 190 : 250; // 200 : 250 //121,90;
                fieldCCName.Width = isGroupVisible && isLedgerVisible ? 190 : 250; //200 : 250 //121,90;
                fieldDonorName.Width = isGroupVisible && isLedgerVisible ? 190 : 250; //200 : 250 //121,90;
                fieldQUATERNO.Width = isGroupVisible && isLedgerVisible ? 120 : 115; //121,90;
                realfont = (float)10;
                if (ReportProperties.ShowCCDetails == 1 && ReportProperties.ShowDonorDetails == 1)
                {
                    fieldLEDGERGROUP.Width = (NoOfQuters <= 3 || ReportProperties.ShowByLedgerGroup== 0) ? 190: 160;
                    fieldLEDGERNAME.Width = fieldCCName.Width = fieldDonorName.Width = (NoOfQuters <= 3 || ReportProperties.ShowByLedgerGroup == 0) ? 170 : 155;
                    fieldAMOUNT.Width = (NoOfQuters <= 3 || ReportProperties.ShowByLedgerGroup == 0) ? 100 : 90;
                    fieldQUATERNO.Width = (NoOfQuters <= 3 || ReportProperties.ShowByLedgerGroup == 0) ? 120 : 110; ; 
                    //realfont = (NoOfQuters <= 3 || ReportProperties.ShowByLedgerGroup == 0) ? realfont : (float)9;
                }
                else if (ReportProperties.ShowCCDetails == 1 || ReportProperties.ShowDonorDetails == 1)
                {
                    fieldLEDGERGROUP.Width = (NoOfQuters <= 3 || ReportProperties.ShowByLedgerGroup == 0) ? 200 : 190;
                    fieldLEDGERNAME.Width = fieldCCName.Width = fieldDonorName.Width = (NoOfQuters <= 3 || ReportProperties.ShowByLedgerGroup == 0) ? 250 : 200;
                    fieldAMOUNT.Width = (NoOfQuters <= 3 || ReportProperties.ShowByLedgerGroup == 0) ? 100 : 90;
                    //realfont = (NoOfQuters <= 3 || ReportProperties.ShowByLedgerGroup == 0) ? realfont : (float)9;
                }

                this.Margins.Left = 30;
                this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = this.PageWidth - 65;
                this.SetTitleWidth(this.PageWidth - 65);
                setHeaderTitleAlignment();
            }

            //On 16/04/2021
            fieldGROUPCODE.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
            fieldLEDGERGROUP.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
            fieldLEDGERGROUP.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
            fieldLEDGERCODE.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
            fieldLEDGERCODE.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Regular);
            fieldLEDGERNAME.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Regular);
            fieldLEDGERNAME.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
            fieldAMOUNT.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Regular);
            fieldAMOUNT.Appearance.TotalCell.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
            fieldQUATERNO.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);

            xrPGMultiAbstractReceipt.Styles.CellStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.CellStyle.Font.FontFamily, realfont-1);
            xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
            xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
            xrPGMultiAbstractReceipt.Styles.GrandTotalCellStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.GrandTotalCellStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
            xrPGMultiAbstractReceipt.Styles.TotalCellStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.TotalCellStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
            xrPGMultiAbstractReceipt.Styles.GrandTotalCellStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.GrandTotalCellStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
            
            if (ReportProperties.ShowCCDetails == 1 || ReportProperties.ShowDonorDetails == 1)
            {
                fieldLEDGERCODE.Appearance.TotalCell.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                fieldLEDGERCODE.Appearance.TotalCell.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;

                fieldCCName.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                fieldCCName.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Regular);
                fieldDonorName.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                fieldDonorName.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Regular);
                fieldAMOUNT.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Regular);
                fieldAMOUNT.Appearance.TotalCell.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);

                xrPGMultiAbstractReceipt.Styles.GrandTotalCellStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.GrandTotalCellStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                xrPGMultiAbstractReceipt.Styles.TotalCellStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.TotalCellStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
            }

            //On 06/06/2023
            //this.Margins.Left = 20;
            //this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = this.PageWidth - 25;
            //this.SetTitleWidth(this.PageWidth - 25);
            this.PrintingSystem.PageSettingsChanged += new EventHandler(PrintingSystem_PageSettingsChanged);
        }

        public void PrintingSystem_PageSettingsChanged(object sender, EventArgs e)
        {
            bool isGroupVisible = (ReportProperties.ShowByLedgerGroup == 1);
            bool isLedgerVisible = (ReportProperties.ShowByLedger == 1 && ReportProperties.ShowCCDetails == 0);
            if (isGroupVisible == false && isLedgerVisible == false) { isLedgerVisible = true; }
            bool isGroupCodeVisible = (isGroupVisible && (ReportProperties.ShowGroupCode == 1));
            bool isLedgerCodeVisible = (isLedgerVisible && (ReportProperties.ShowLedgerCode == 1));

            if (NoOfQuters <= 2)
            {
                if (xrPGMultiAbstractReceipt.DataSource != null)
                {
                    PrintingSystemBase printingbase = sender as PrintingSystemBase;

                    this.Landscape = printingbase.PageSettings.Landscape;
                    int newPageWidth = printingbase.PageBounds.Width - printingbase.PageMargins.Left - printingbase.PageMargins.Right;
                    //SetReportSetting(dv, accountBalanceMulti);
                    if (this.Landscape)
                    {
                        fieldGROUPCODE.Width = 70;// 50;
                        fieldLEDGERCODE.Width = (ReportProperties.ShowCCDetails == 1 || ReportProperties.ShowDonorDetails == 1 ? 0 : 70); 
                        fieldLEDGERGROUP.Width = isGroupVisible && isLedgerVisible ? 250 : 275; //121,90;
                        fieldLEDGERNAME.Width = isGroupVisible && isLedgerVisible ? 250 : 275; //121,90;
                        fieldCCName.Width = isGroupVisible && isLedgerVisible ? 250 : 275; //121,90;
                        fieldDonorName.Width = isGroupVisible && isLedgerVisible ? 250 : 275; //121,90;
                        fieldQUATERNO.Width = isGroupVisible && isLedgerVisible ? 110 : 125; //121,90;

                        float realfont = (float)10;
                        fieldGROUPCODE.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                        fieldLEDGERGROUP.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                        fieldLEDGERCODE.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                        fieldLEDGERNAME.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                        xrPGMultiAbstractReceipt.Styles.CellStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.CellStyle.Font.FontFamily, realfont);
                        xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                        xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                        xrPGMultiAbstractReceipt.Styles.GrandTotalCellStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.GrandTotalCellStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                        xrPGMultiAbstractReceipt.Styles.TotalCellStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.TotalCellStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);

                        //this.Margins.Left = 35;
                        this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = newPageWidth - 15;
                        this.SetTitleWidth(newPageWidth - 15);
                    }
                    else
                    {
                        fieldGROUPCODE.Width = 70;//50;
                        fieldLEDGERCODE.Width = (ReportProperties.ShowCCDetails == 1 || ReportProperties.ShowDonorDetails == 1 ? 0 : 70); 
                        fieldLEDGERGROUP.Width = isGroupVisible && isLedgerVisible ? 125 : 185; //121,90;
                        fieldLEDGERNAME.Width = isGroupVisible && isLedgerVisible ? 125 : 185; //121,90;
                        fieldCCName.Width = isGroupVisible && isLedgerVisible ? 125 : 185; //121,90;
                        fieldDonorName.Width = isGroupVisible && isLedgerVisible ? 125 : 185; //121,90;
                        fieldQUATERNO.Width = isGroupVisible && isLedgerVisible ? 108 : 110; //121,90;

                        float realfont = (float)10;
                        fieldGROUPCODE.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                        fieldLEDGERGROUP.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                        fieldLEDGERCODE.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                        fieldLEDGERNAME.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                        xrPGMultiAbstractReceipt.Styles.CellStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.CellStyle.Font.FontFamily, realfont);
                        xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                        xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                        xrPGMultiAbstractReceipt.Styles.GrandTotalCellStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.GrandTotalCellStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                        xrPGMultiAbstractReceipt.Styles.TotalCellStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.TotalCellStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);

                        //this.Margins.Left = 35;
                        this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = newPageWidth - 25;
                        this.SetTitleWidth(newPageWidth - 25);
                    }

                    //Include / Exclude Code
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
                    fieldGRANTTOTALMONTH.Width = fieldQUATERNO.Width;
                    fieldGRANTTOTALAMOUNT.Width = fieldAMOUNT.Width;

                    AccountBalanceMulti accountBalanceMulti = xrSubClosingBalanceMulti.ReportSource as AccountBalanceMulti;
                    xrSubClosingBalanceMulti.LeftF = xrPGMultiAbstractReceipt.LeftF;
                    accountBalanceMulti.LeftPosition = (xrPGMultiAbstractReceipt.LeftF - 5);
                    accountBalanceMulti.GroupCodeColumnWidth = fieldGROUPCODE.Width;
                    accountBalanceMulti.GroupNameColumnWidth = fieldLEDGERGROUP.Width;
                    accountBalanceMulti.LedgerCodeColumnWidth = fieldLEDGERCODE.Width ;
                    accountBalanceMulti.LedgerNameColumnWidth = fieldLEDGERNAME.Width;
                    
                    //Opening and Closing Balance with CC and Donor attached
                    if (this.ReportProperties.ShowCCDetails == 1 || this.ReportProperties.ShowDonorDetails == 1)
                    {
                        if ((this.ReportProperties.ShowCCDetails == 1 && this.ReportProperties.ShowDonorDetails == 1))
                            accountBalanceMulti.LedgerNameColumnWidth = fieldCCName.Width + fieldDonorName.Width;
                        else
                            accountBalanceMulti.LedgerNameColumnWidth = fieldCCName.Width;
                    }
                    accountBalanceMulti.AmountColumnWidth = fieldQUATERNO.Width;
                    accountBalanceMulti.ApplyParentReportStyle = xrPGMultiAbstractReceipt.Styles;
                    accountBalanceMulti.SetReportSetting();
                    if (!ReceiptsAndPayments)
                    {
                        this.CreateDocument();
                    }
                }
            }
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
                    totVal = GetTotal((col + 1));
                    columnTotal += totVal;
                    if ((col + 1) == xrPGMultiAbstractReceipt.ColumnCount)
                    {
                        totVal = columnTotal;
                    }
                    DataRow drGrantTotal = dtGrantTotal.NewRow();
                    drGrantTotal[reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName] = IsMultiReceipts?"Total Receipts":"Total Payments";
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
           
        }

        private double GetTotal(Int32 Qno)
        {
            double rtn = 0;
            DataView dvBindData = xrPGMultiAbstractReceipt.DataSource as DataView;
            string filter = reportSetting1.MultiAbstract.QUATER_NOColumn.ColumnName + "=" + Qno;
            //string fldname = this.ReportProperties.ShowCCDetails == 1 ? "CC_AMOUNT" : reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName;
            string fldname = reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName;
            object oTotVal = dvBindData.Table.Compute("SUM(" + fldname + ")", filter);

            if (oTotVal != null) rtn = UtilityMember.NumberSet.ToDouble(oTotVal.ToString());
            return rtn;
        }

        private void CheckDateRangeByQuater()
        {
            DateTime rptdatefrom = this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false);
            DateTime rptdateto = this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateTo, false);
            DateTime dateQuaterFrom = ReportProperty.Current.DateSet.FirstDayOfQuater(rptdatefrom, this.AppSetting.YearFrom, this.AppSetting.YearTo);
            DateTime dateQuaterTo = ReportProperty.Current.DateSet.LastDayOfQuater(rptdateto, this.AppSetting.YearFrom, this.AppSetting.YearTo);

            if ((rptdatefrom != dateQuaterFrom) || (rptdateto != dateQuaterTo))
            {
                this.ReportProperties.DateFrom = dateQuaterFrom.ToShortDateString();
                this.ReportProperties.DateTo= dateQuaterTo.ToShortDateString();
            }
        }

        #endregion

        #region Events
        private void xrPGMultiAbstractReceipt_CustomFieldSort(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.Name == fieldQUATERNO.Name)
            {
                if (e.Value1 != null && e.Value2 != null)
                {
                    //DateTime dt1 = DateTime.Parse(e.GetListSourceColumnValue(e.ListSourceRowIndex1, reportSetting1.MultiAbstract.MONTH_YEARColumn.ColumnName).ToString());
                    //DateTime dt2 = DateTime.Parse(e.GetListSourceColumnValue(e.ListSourceRowIndex2, reportSetting1.MultiAbstract.MONTH_YEARColumn.ColumnName).ToString());
                    
                    //On 16/04/2020, to have proper quter number based on FY (April-March)
                    Int32 Quarter1 = UtilityMember.NumberSet.ToInteger(e.GetListSourceColumnValue(e.ListSourceRowIndex1, this.reportSetting1.MultiAbstract.QUATER_NOColumn.ColumnName).ToString());
                    Int32 Quarter2 = UtilityMember.NumberSet.ToInteger(e.GetListSourceColumnValue(e.ListSourceRowIndex2, this.reportSetting1.MultiAbstract.QUATER_NOColumn.ColumnName).ToString());
                    if (this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Month == 1) // (Jan-Feb)
                    {
                        Quarter1 = UtilityMember.NumberSet.ToInteger(e.GetListSourceColumnValue(e.ListSourceRowIndex1, this.reportSetting1.MultiAbstract.REAL_QUATER_NOColumn.ColumnName).ToString());
                        Quarter2 = UtilityMember.NumberSet.ToInteger(e.GetListSourceColumnValue(e.ListSourceRowIndex2, this.reportSetting1.MultiAbstract.REAL_QUATER_NOColumn.ColumnName).ToString());
                    }

                    e.Result = Comparer.Default.Compare(Quarter1, Quarter2);
                    e.Handled = true;
                }
            }
        }

        private void xrPGMultiAbstractReceipt_FieldValueDisplayText(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
            {
                e.DisplayText = "Total " + (IsMultiReceipts?"Receipts":"Payments");
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
                        lname += (string.IsNullOrEmpty(lcode) ? "" :  "  ") + dt.Rows[0][reportSetting1.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
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
                if (e.Field != null && e.Field == fieldLEDGERCODE && (ReportProperties.ShowCCDetails == 1 || ReportProperties.ShowDonorDetails== 1))
                {
                    e.DisplayText = string.Empty;
                }
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

        private void xrPGMultiAbstractReceipt_PrintFieldValue(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs e)
        {
            if (e.Field != null)
            {
                if (e.Field.Name == fieldLEDGERCODE.Name || e.Field.Name == fieldLEDGERNAME.Name || e.Field.Name == fieldCCName.Name || e.Field.Name == fieldDonorName.Name ||
                    e.Field.Name == fieldGROUPCODE.Name || e.Field.Name == fieldLEDGERGROUP.Name)
                {
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
                    
                    // On 06/06/2023, To allow split content to next page
                    /*bool txt = e.Brick.SeparableVert;
                    //On 15/09/2002, If content is splitted into two pages (bottom text), let us fix to anyother page fully and keep space ---------
                    if (e.Brick.SeparableVert)
                    {
                        e.Brick.SeparableVert = false; 
                    }
                    //-------------------------------------------------------------------------------------------------------------------------------*/
                }
                else if (e.Field.Name == fieldQUATERNO.Name)
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
                        e.Appearance.WordWrap = true;

                        PanelBrick panelBrick = e.Brick as PanelBrick;
                        if (panelBrick != null)
                        {
                            ((TextBrick)panelBrick.Bricks[0]).Size = new SizeF(((TextBrick)panelBrick.Bricks[0]).Size.Width, 100);
                            ((TextBrick)panelBrick.Bricks[0]).VertAlignment = DevExpress.Utils.VertAlignment.Center;
                                                        
                        }
                    }
                    else
                    {
                        e.Appearance.Font = xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.Font;
                    }
                }
                
                if (e.Field.Name == fieldQUATERNO.Name)
                {
                    if (xrPGMultiAbstractReceipt.OptionsView.ShowRowHeaders == false)
                    {
                        e.Brick.Text = "";
                        e.Brick.BorderWidth = 0;
                        //e.Appearance.BackColor = Color.White;

                        DevExpress.XtraPrinting.TextBrick textBrick = e.Brick as DevExpress.XtraPrinting.TextBrick;
                        textBrick.Size = new SizeF(textBrick.Size.Width, 0);
                        e.Field.Options.ShowValues = false;
                    }
                    else
                    {
                        //e.Brick.Text = "Q" + e.Brick.Text;
                        int QuterNo = UtilityMember.NumberSet.ToInteger(e.Brick.Text);
                        e.Brick.Text = UtilityMember.DateSet.GetQuaterMonthsFinancialYearByQuaterNo(QuterNo, AppSetting.YearFrom, AppSetting.YearTo);
                        e.Brick.TextValue = UtilityMember.DateSet.GetQuaterMonthsFinancialYearByQuaterNo(QuterNo, AppSetting.YearFrom, AppSetting.YearTo);
                    }
                }
            }
            else
            {
                if (e.IsColumn)
                {
                    e.Appearance.BackColor = xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.BackColor;
                    e.Appearance.Font = xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.Font;
                    e.Brick.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
                    e.Brick.BorderWidth = 1;
                    e.Brick.Padding = new PaddingInfo(0, 2, 2, 0);
                }
                else
                {
                    e.Appearance.Font = xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.Font;
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
                        //e.Appearance.BackColor = Color.White;

                        DevExpress.XtraPrinting.TextBrick textBrick = e.Brick as DevExpress.XtraPrinting.TextBrick;
                        textBrick.Size = new SizeF(textBrick.Size.Width, 0);
                        //e.Field.Options.ShowValues = false;
                    }
                }
            }
        }

        private void xrPGMultiAbstractReceipt_PrintCell(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportCellEventArgs e)
        {
            if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.TotalCell ||
                e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.GrandTotalCell)
            {
                //e.Appearance.ForeColor = xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.ForeColor;
                e.Appearance.Font = fieldAMOUNT.Appearance.TotalCell.Font;// xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.Font;
                e.Appearance.Font = fieldAMOUNT.Appearance.TotalCell.Font;
                e.Appearance.WordWrap = true;
                e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
                e.Appearance.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;

            }

            if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.Cell)
            {
                if (e.Brick.TextValue == null || this.UtilityMember.NumberSet.ToDouble(e.Brick.TextValue.ToString()) == 0) { e.Brick.Text = ""; }
                e.Appearance.Font = fieldAMOUNT.Appearance.FieldValue.Font; ;
            }


        }

        private void xrPGMultiAbstractReceipt_AfterPrint(object sender, EventArgs e)
        {
            DataTable dtGrantTotal = new DataTable();
            dtGrantTotal.Columns.Add(reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName, typeof(string));
            dtGrantTotal.Columns.Add(reportSetting1.MultiAbstract.AC_YEAR_NAMEColumn.ColumnName, typeof(int));
            dtGrantTotal.Columns.Add(reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName, typeof(double));
            object oTotVal = null;
            double totVal = 0;
            double ColumnTotal = 0;
            int row = xrPGMultiAbstractReceipt.RowCount - 1;
            
            for (int col = 0; col < xrPGMultiAbstractReceipt.ColumnCount; col++)
            {
                if (fieldCCName.Visible || fieldDonorName.Visible)
                {
                    totVal = GetTotal((col + 1));
                    ColumnTotal += totVal;
                    if ((col+1) == xrPGMultiAbstractReceipt.ColumnCount)
                    {
                        totVal = ColumnTotal;
                    }
                }
                else
                {
                    oTotVal = xrPGMultiAbstractReceipt.GetCellValue(col, row);
                    totVal = this.UtilityMember.NumberSet.ToDouble(oTotVal == null ? "0" : oTotVal.ToString());
                }

                DataRow drGrantTotal = dtGrantTotal.NewRow();
                drGrantTotal[reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName] = "Grand Total";
                drGrantTotal[reportSetting1.MultiAbstract.AC_YEAR_NAMEColumn.ColumnName] = (col + 1);
                drGrantTotal[reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName] = totVal;
                dtGrantTotal.Rows.Add(drGrantTotal);
            }

            dtGrantTotal.AcceptChanges();
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


            // Iterates through all row headers.
            //for (int i = e.GetCellCount(false) - 1; i >= 0; i--)
            //{
            //    //DevExpress.XtraReports.UI.PivotGrid.FieldValueCell cell = e.GetCell(false, i);
            //    //if (cell == null) continue;

            //    //// If the current header corresponds to the "Employee B"
            //    //// field value, and is not the Total Row header,
            //    //// it is removed with all corresponding rows.

            //    //if (cell.Field == fieldLEDGERNAME &&
            //    //    cell.ValueType != PivotGridValueType.Total)
            //    //{
            //    //    //e.Remove(cell);
            //    //}
            //}

            /*bool removeledger = false;
            for (int j = 0; j < e.ColumnCount; j++)
            {
                DevExpress.XtraReports.UI.PivotGrid.FieldValueCell cell = e.GetCell(false, j);
                if (cell != null && cell.Field != null && cell.Field.FieldName == "LEDGER_NAME")
                {
                    removeledger = !string.IsNullOrEmpty(cell.Value.ToString());
                    break;
                }
            }*/
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
                            fieldLEDGERNAME.Appearance.Cell.WordWrap = true;
                            fieldLEDGERNAME.Appearance.FieldValue.WordWrap = true;
                            fieldLEDGERGROUP.Appearance.Cell.WordWrap = true;
                            fieldLEDGERGROUP.Appearance.FieldValue.WordWrap = true;
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
                                size = gr.MeasureString(ccname, fieldCCName.Appearance.FieldValue.Font, fieldCCName.Width);
                                RowHeightccName = Convert.ToInt32(size.Height + 0.5);
                                RowHeightLedgerName = Math.Max(RowHeightLedgerName, RowHeightccName);
                            }

                            if (fieldDonorName.Visible)
                            {
                                if (e.GetFieldValue(fieldDonorName, e.RowIndex) != null)
                                {
                                    donorname = e.GetFieldValue(fieldDonorName, e.RowIndex).ToString().Trim();
                                }
                                size = gr.MeasureString(donorname, fieldDonorName.Appearance.FieldValue.Font, fieldDonorName.Width);
                                RowHeightDonorName = Convert.ToInt32(size.Height + 0.5);
                                RowHeightLedgerName = Math.Max(RowHeightLedgerName, RowHeightDonorName);
                            }

                            //Int32 RowHeightLedgerName = GetRowHeight(ledgername, fieldLEDGERNAME.Width, e.RowHeight);
                            if (fieldLEDGERGROUP.Visible)
                            {
                                ledgergroup = e.GetFieldValue(fieldLEDGERGROUP, e.RowIndex).ToString();
                                size = gr.MeasureString(ledgergroup, fieldLEDGERGROUP.Appearance.FieldValue.Font, fieldLEDGERGROUP.Width);
                                
                                //For Temp 11/04/2023, To handle skipping new page
                                //RowHeightLedgerGroup = Convert.ToInt32(size.Height + 0.5);
                                RowHeightLedgerGroup = Convert.ToInt32(size.Height + 5);// Convert.ToInt32(size.Height + 0.5);
                                if (string.IsNullOrEmpty(ledgergroup)) RowHeightLedgerGroup = 0;
                            }
                            e.RowHeight = Math.Max(RowHeightLedgerName, RowHeightLedgerGroup);
                        }
                    }
                    else if (e.ValueType == PivotGridValueType.Total)
                    {
                        fieldLEDGERCODE.Appearance.TotalCell.WordWrap = true;
                        fieldLEDGERCODE.Appearance.TotalCell.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Bottom;

                        //On 16/09/2022, To hide empty row group (Ledger Group), as unable remove empty ledger group ---------------
                        //e.RowIndex == 0 &&
                        if (e.Data.GetAvailableFieldValues(fieldLEDGERGROUP) != null &&
                                 ReportProperties.ShowByLedgerGroup == 1 && ReportProperties.ShowByLedger == 1)
                        {
                            //If Ledger Group value is empty,
                            string ledgergroup = (e.GetFieldValue(fieldLEDGERGROUP, e.RowIndex) != null ? e.GetFieldValue(fieldLEDGERGROUP, e.RowIndex).ToString() : string.Empty);
                            string ledgername = (e.GetFieldValue(fieldLEDGERNAME, e.RowIndex) != null ? e.GetFieldValue(fieldLEDGERNAME, e.RowIndex).ToString() : string.Empty);
                            if (string.IsNullOrEmpty(ledgergroup))
                            {
                                e.RowHeight = 0;
                            }    
                        }
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
                            lname = lcode + "  " + lname ;
                            string ccname = dt.Columns.Contains(fieldCCName.FieldName) ? dt.Rows[0][fieldCCName.FieldName].ToString() : string.Empty;
                            string donorname = dt.Rows[0][fieldDonorName.FieldName].ToString();

                            if (string.IsNullOrEmpty(lname.Trim()) && string.IsNullOrEmpty(ccname) && string.IsNullOrEmpty(donorname) 
                                && ReportProperties.ShowCCDetails == 1 && ReportProperties.ShowDonorDetails == 1)
                            {
                                e.RowHeight = 0;
                            }
                            else if (e.ValueType == PivotGridValueType.Value )
                            {
                                Int32 TotalCounts = dt.Rows.Count;
                                string filter = ""; 
                                if (ReportProperties.ShowCCDetails == 1)
                                    filter = "ISNULL(" + fieldCCName.FieldName + ",'')=''";
                                
                                if (ReportProperties.ShowDonorDetails == 1)
                                    filter += (string.IsNullOrEmpty(filter)? "" : " AND ") + "ISNULL(" + fieldDonorName.FieldName + ",'')=''";

                                dt.DefaultView.RowFilter = filter;
                                if (dt.DefaultView.Count == TotalCounts)
                                {
                                    e.RowHeight = 0;
                                }
                            }
                            else if (e.ValueType == PivotGridValueType.Total )
                            {
                                SizeF size = gr.MeasureString(lname, xrPGMultiAbstractReceipt.Styles.FieldValueTotalStyle.Font, fieldLEDGERNAME.Width);
                                Int32 RowHeightLedgerName = Convert.ToInt32(size.Height + 0.5);
                                size = gr.MeasureString(lgroup, xrPGMultiAbstractReceipt.Styles.FieldValueTotalStyle.Font, fieldLEDGERGROUP.Width);
                                Int32 RowHeightLedgerGroup = Convert.ToInt32(size.Height + 0.5);
                                e.RowHeight = Math.Max(RowHeightLedgerGroup, RowHeightLedgerName);
                                
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

        private void xrPGMultiAbstractReceipt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void xrPGMultiAbstractReceipt_CustomCellDisplayText(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotCellDisplayTextEventArgs e)
        {
            
        }
    }
}



 //private void SetFieldFontsPaperSize()
 //       {
 //           bool isGroupVisible = (ReportProperties.ShowByLedgerGroup == 1);
 //           bool isLedgerVisible = (ReportProperties.ShowByLedger == 1);
 //           if (isGroupVisible == false && isLedgerVisible == false) { isLedgerVisible = true; }
 //           bool isGroupCodeVisible = (isGroupVisible && (ReportProperties.ShowGroupCode == 1));
 //           bool isLedgerCodeVisible = (isLedgerVisible && (ReportProperties.ShowLedgerCode == 1));

 //           List<Tuple<DateTime, DateTime>> quarterDates = UtilityMember.DateSet.GetQuarterDates(UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false), UtilityMember.DateSet.ToDate(ReportProperties.DateTo, false));
 //           NoOfQuters = quarterDates.Count;

 //           /*if (ReportProperties.ShowCCDetails == 1 || ReportProperties.ShowDonorDetails== 1)
 //           {
 //               isLedgerVisible = false;
 //           }*/

 //           //Int32 noofonths = UtilityMember.DateSet.ToDate(ReportProperties.DateTo, false).Month - UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).Month;
 //           if (NoOfQuters <= 2 && (ReportProperties.ShowCCDetails == 0 || ReportProperties.ShowDonorDetails == 0))
 //           {
 //               this.Landscape = false;
 //               fieldGROUPCODE.Width = 70;
 //               fieldLEDGERCODE.Width = (ReportProperties.ShowCCDetails == 1 || ReportProperties.ShowDonorDetails== 1 ? 0:70); 
 //               fieldLEDGERGROUP.Width = isGroupVisible && isLedgerVisible ? 180 : 235; //121,90;
 //               fieldLEDGERNAME.Width = isGroupVisible && isLedgerVisible ? 190 : 235; //121,90;
 //               fieldCCName.Width = isGroupVisible && isLedgerVisible ? 190 : 235; //121,90;
 //               fieldDonorName.Width = isGroupVisible && isLedgerVisible ? 190 : 235; //121,90;
 //               fieldQUATERNO.Width = isGroupVisible && isLedgerVisible ? 108 : 115; //121,90;
 //               float realfont = (float)10;

 //               //On 16/04/2021
 //               fieldGROUPCODE.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
 //               fieldLEDGERGROUP.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
 //               fieldLEDGERNAME.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Regular);
 //               fieldCCName.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 2, FontStyle.Regular);
 //               fieldDonorName.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 2, FontStyle.Regular);
 //               fieldLEDGERCODE.Appearance.TotalCell.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 2, FontStyle.Bold);
 //               fieldAMOUNT.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Regular);
 //               if (ReportProperties.ShowCCDetails == 1 ||ReportProperties.ShowDonorDetails == 1)
 //                   fieldAMOUNT.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 2, FontStyle.Regular);
                
 //               xrPGMultiAbstractReceipt.Styles.CellStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.CellStyle.Font.FontFamily, realfont);
 //               xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
 //               xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
 //               xrPGMultiAbstractReceipt.Styles.GrandTotalCellStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.GrandTotalCellStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
 //               xrPGMultiAbstractReceipt.Styles.TotalCellStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.TotalCellStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);

 //               this.Margins.Left = 20;
 //               this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = this.PageWidth - 15;
 //               this.SetTitleWidth(this.PageWidth - 15);
 //               this.PrintingSystem.PageSettingsChanged += new EventHandler(PrintingSystem_PageSettingsChanged);
 //           }
 //           else if (NoOfQuters <= 3 && (ReportProperties.ShowCCDetails == 0 && ReportProperties.ShowDonorDetails == 0))
 //           {
 //               this.Landscape = false;
 //               fieldGROUPCODE.Width = 70;
 //               fieldLEDGERCODE.Width = (ReportProperties.ShowCCDetails == 1 || ReportProperties.ShowDonorDetails == 1 ? 0 : 70); 
 //               fieldLEDGERGROUP.Width = isGroupVisible && isLedgerVisible ? 130 : 225; //121,90;
 //               fieldLEDGERNAME.Width = isGroupVisible && isLedgerVisible ? 135 : 225; //121,90;
 //               fieldCCName.Width = isGroupVisible && isLedgerVisible ? 135 : 225; //121,90;
 //               fieldDonorName.Width = isGroupVisible && isLedgerVisible ? 135 : 225; //121,90;
 //               fieldQUATERNO.Width = isGroupVisible && isLedgerVisible ? 108 : 115; //121,90;
 //               float realfont = (float)10;
                
 //               //On 16/04/2021
 //               fieldGROUPCODE.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
 //               fieldLEDGERGROUP.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
 //               fieldLEDGERCODE.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
 //               fieldLEDGERNAME.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Regular);
 //               fieldCCName.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 2, FontStyle.Regular);
 //               fieldDonorName.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 2, FontStyle.Regular);
 //               fieldLEDGERCODE.Appearance.TotalCell.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 2, FontStyle.Bold);
 //               fieldAMOUNT.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Regular);
 //               if (ReportProperties.ShowCCDetails == 1 || ReportProperties.ShowDonorDetails == 1)
 //                   fieldAMOUNT.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 2, FontStyle.Regular);
                
 //               xrPGMultiAbstractReceipt.Styles.CellStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.CellStyle.Font.FontFamily, realfont);
 //               xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
 //               xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
 //               xrPGMultiAbstractReceipt.Styles.GrandTotalCellStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.GrandTotalCellStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
 //               xrPGMultiAbstractReceipt.Styles.TotalCellStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.TotalCellStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);

 //               this.Margins.Left = 20;
 //               this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = this.PageWidth - 15;
 //               this.SetTitleWidth(this.PageWidth - 15);
 //               this.PrintingSystem.PageSettingsChanged += new EventHandler(PrintingSystem_PageSettingsChanged);
 //           }
 //           else
 //           {
 //               this.Landscape = true;
 //               fieldGROUPCODE.Width = 70;//50;
 //               fieldLEDGERCODE.Width = (ReportProperties.ShowCCDetails == 1 || ReportProperties.ShowDonorDetails == 1 ? 0 : 70); 
 //               fieldLEDGERGROUP.Width = isGroupVisible && isLedgerVisible ? 200 : 250; //121,90;
 //               fieldLEDGERNAME.Width = isGroupVisible && isLedgerVisible ? 200 : 250; //121,90;
 //               fieldCCName.Width = isGroupVisible && isLedgerVisible ? 200 : 250; //121,90;
 //               fieldDonorName.Width = isGroupVisible && isLedgerVisible ? 200 : 250; //121,90;
 //               fieldQUATERNO.Width = isGroupVisible && isLedgerVisible ? 120 : 115; //121,90;
 //               float realfont = (float)10;
 //               if (ReportProperties.ShowCCDetails == 1 && ReportProperties.ShowDonorDetails == 1)
 //               {
 //                   fieldLEDGERGROUP.Width = 160;
 //                   fieldLEDGERNAME.Width = fieldCCName.Width = fieldDonorName.Width =   150;
 //                   fieldAMOUNT.Width = 90;
 //                   realfont = (float)9;
 //               }

 //               //On 16/04/2021
 //               fieldGROUPCODE.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
 //               fieldLEDGERGROUP.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
 //               fieldLEDGERNAME.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Regular);
 //               fieldCCName.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 2, FontStyle.Regular);
 //               fieldDonorName.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 2, FontStyle.Regular);
 //               fieldLEDGERCODE.Appearance.TotalCell.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 2, FontStyle.Bold);
 //               fieldAMOUNT.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Regular);
 //               if (ReportProperties.ShowCCDetails == 1 || ReportProperties.ShowDonorDetails == 1)
 //                   fieldAMOUNT.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 2, FontStyle.Regular);

 //               xrPGMultiAbstractReceipt.Styles.CellStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.CellStyle.Font.FontFamily, realfont);
 //               xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
 //               xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
 //               xrPGMultiAbstractReceipt.Styles.GrandTotalCellStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.GrandTotalCellStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
 //               xrPGMultiAbstractReceipt.Styles.TotalCellStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.TotalCellStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                
 //               this.Margins.Left = 20;
 //               this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = this.PageWidth - 15;
 //               this.SetTitleWidth(this.PageWidth - 15);
 //           }
 //       }