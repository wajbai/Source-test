using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility.ConfigSetting;
using Bosco.Report.Base;
using Bosco.Utility;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using System.Data;

namespace Bosco.Report.ReportObject
{
    public partial class BalanceSheet : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        // SettingProperty settingProperty = new SettingProperty();
        // double CapDebit = 0;
        // double CapCredit = 0;
        // double OpCapDebit = 0;
        // double OpCapCredit = 0;
        //  double TotalAssetAmt = 0;
        //  double TotalLiabilitiesAmt = 0;
        //  double DifferenceAmt = 0;

        double assetTotal = 0;
        double LiabilityTotal = 0;

        double ExcessDebitAmount;
        double ExcessCreditAmount;
        double DiffOpeningAmount = 0;

        double ExcessOpCreditAmount = 0;
        double ExcessOpDebitAmount = 0;

        double ExcessPeriodCreditAmount = 0;
        double ExcessPeriodDebitAmount = 0;

        float LiabilityCodeWidth = 0;
        float LiabilityNameWidth = 0;
        float LiabilityAmountWidth = 0;

        float AssetCodeWidth = 0;
        float AssetNameWidth = 0;
        float AssetAmountWidth = 0;

        #endregion

        #region Constructor
        public BalanceSheet()
        {
            InitializeComponent();

            ArrayList ledgerfilter = new ArrayList { reportSetting1.ReportParameter.DATE_AS_ONColumn.ColumnName};
            DrillDownType ledgerdrilltype = DrillDownType.DRILL_TO_IE_REPORT;

            //Attach drill for diff.IE
            this.AttachDrillDownToRecord(xrtblDifference, xrExcessLiabilitiesAmount, ledgerfilter, ledgerdrilltype, false, "", false);
            this.AttachDrillDownToRecord(xrtblDifference, xrExcessAssetsAmt, ledgerfilter, ledgerdrilltype, false, "", false);

            //Attach drill for diff.opening
            ledgerdrilltype = DrillDownType.DRILL_TO_LEDGER_DEFINE_OPENING_BALANCE;
            this.AttachDrillDownToRecord(xrtblDifference, xrDifferenceOPLiability, ledgerfilter, ledgerdrilltype, false, "", false);
            this.AttachDrillDownToRecord(xrtblDifference, xrDifferenceOPAsset, ledgerfilter, ledgerdrilltype, false, "", false);

        }
        #endregion

        #region Property
        string yearFrom = string.Empty;
        public string YearFrom
        {
            get
            {
                yearFrom = settingProperty.YearFrom;
                return yearFrom;
            }
        }
        string yearto = string.Empty;
        public string YearTo
        {
            get
            {
                yearto = settingProperty.YearTo;
                return yearto;
            }
        }
        #endregion

        #region ShowReport

        public override void ShowReport()
        {
            LiabilityCodeWidth = xrtblLiaCode.WidthF;
            if (xrtblLiaCode.Tag != null && xrtblLiaCode.Tag.ToString() != "")
            {
                LiabilityCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrtblLiaCode.Tag.ToString());
            }
            else
            {
                xrtblLiaCode.Tag = xrtblLiaCode.WidthF;
            }
            LiabilityNameWidth = xrcolCap2.WidthF;
            if (xrcolCap2.Tag != null && xrcolCap2.Tag.ToString() != "")
            {
                LiabilityNameWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrcolCap2.Tag.ToString());
            }
            else
            {
                xrcolCap2.Tag = xrcolCap2.WidthF;
            }
            LiabilityAmountWidth = xrCellLiaCurrenctAmt.WidthF;
            if (xrCellLiaCurrenctAmt.Tag != null && xrCellLiaCurrenctAmt.Tag.ToString() != "")
            {
                LiabilityAmountWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrCellLiaCurrenctAmt.Tag.ToString());
            }
            else
            {
                xrCellLiaCurrenctAmt.Tag = xrCellLiaCurrenctAmt.WidthF;
            }

            AssetCodeWidth = xrtblAssCode.WidthF;
            if (xrtblAssCode.Tag != null && xrtblAssCode.Tag.ToString() != "")
            {
                AssetCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrtblAssCode.Tag.ToString());
            }
            else
            {
                xrtblAssCode.Tag = xrtblAssCode.WidthF;
            }

            AssetNameWidth = xrcolCap5.WidthF;
            if (xrcolCap5.Tag != null && xrcolCap5.Tag.ToString() != "")
            {
                AssetNameWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrcolCap5.Tag.ToString());
            }
            else
            {
                xrcolCap5.Tag = xrcolCap5.WidthF;
            }
            AssetAmountWidth = xrcellAssetCurrentAmt.WidthF;
            if (xrcellAssetCurrentAmt.Tag != null && xrcellAssetCurrentAmt.Tag.ToString() != "")
            {
                AssetAmountWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrcellAssetCurrentAmt.Tag.ToString());
            }
            else
            {
                xrcellAssetCurrentAmt.Tag = xrcellAssetCurrentAmt.WidthF;
            }

            ExcessDebitAmount = 0;
            ExcessCreditAmount = 0;
            DiffOpeningAmount = 0;
            // GetBalanceSheetExcessAmount();

            //  AssignDifferenceInOpeningBalance();
            BindBalanceSheet();

            xrDifferenceOPLiability.Text = string.Empty;
            xrDifferenceOPAsset.Text = string.Empty;

            if (DiffOpeningAmount >= 0)
                xrDifferenceOPLiability.Text = this.UtilityMember.NumberSet.ToNumber(DiffOpeningAmount).ToString();
            else
                xrDifferenceOPAsset.Text = this.UtilityMember.NumberSet.ToNumber(Math.Abs(DiffOpeningAmount)).ToString();

            base.ShowReport();
        }

        #endregion

        #region Events
        private void xrtblAssetTotalAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = Math.Abs(assetTotal) + (DiffOpeningAmount < 0 ? Math.Abs(DiffOpeningAmount) : 0) + ExcessDebitAmount;
            e.Handled = true;
        }

        private void xrtblLiaAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = Math.Abs(LiabilityTotal) + (DiffOpeningAmount >= 0 ? Math.Abs(DiffOpeningAmount) : 0) + ExcessCreditAmount;
            e.Handled = true;
        }

        private void xrDiffLiabilities_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (DiffOpeningAmount >= 0)
            {
                e.Result = "Difference in Opening Balance";
                e.Handled = true;
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void xrAssetDiff_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (DiffOpeningAmount < 0)
            {
                e.Result = "Difference in Opening Balance";
                e.Handled = true;
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }

        }

        private void xrExcessLiabilities_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessCreditAmount > 0)
            {
                e.Result = "Excess of Income Over Expenditure";
                e.Handled = true;
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void xrExcessAssets_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessDebitAmount > 0)
            {
                e.Result = "Excess of Expenditure Over Income";
                e.Handled = true;
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void xrLiaOpening_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessCreditAmount > 0)
            {
                e.Result = "Opening Balance ";
                e.Handled = true;
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void xrLiaPeriod_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessCreditAmount > 0)
            {
                e.Result = "For the Current Period ";
                e.Handled = true;
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void xrAssetOpening_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessDebitAmount > 0)
            {
                e.Result = "Opening Balance ";
                e.Handled = true;
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void xrAssetPeriod_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessDebitAmount > 0)
            {
                e.Result = "For the Current Period ";
                e.Handled = true;
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void xrLiaOpeningAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessCreditAmount > 0)
            {
                if (ExcessOpCreditAmount != 0 && ExcessOpCreditAmount > 0)
                {
                    e.Result = this.UtilityMember.NumberSet.ToNumber(ExcessOpCreditAmount);
                    e.Handled = true;
                }
                else
                {
                    e.Result = this.UtilityMember.NumberSet.ToNumber(ExcessOpDebitAmount);
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void xrLiaPeriodAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessCreditAmount > 0)
            {
                if (ExcessPeriodCreditAmount != 0 && ExcessPeriodCreditAmount > 0)
                {
                    e.Result = this.UtilityMember.NumberSet.ToNumber(ExcessPeriodCreditAmount);
                    e.Handled = true;
                }
                else
                {
                    e.Result = this.UtilityMember.NumberSet.ToNumber(ExcessPeriodDebitAmount);
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void xrAssetOpeningAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessDebitAmount > 0)
            {
                if (ExcessOpDebitAmount != 0 && ExcessOpDebitAmount > 0)
                {
                    e.Result = this.UtilityMember.NumberSet.ToNumber(ExcessOpCreditAmount);
                    e.Handled = true;
                }
                else
                {
                    e.Result = this.UtilityMember.NumberSet.ToNumber(ExcessOpDebitAmount);
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void xrAssetPeriodAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessDebitAmount > 0)
            {
                if (ExcessPeriodDebitAmount != 0 && ExcessPeriodDebitAmount > 0)
                {
                    e.Result = this.UtilityMember.NumberSet.ToNumber(ExcessPeriodCreditAmount);
                    e.Handled = true;
                }
                else
                {
                    e.Result = this.UtilityMember.NumberSet.ToNumber(ExcessPeriodDebitAmount);
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }
        private void xrExcessLiabilitiesAmount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessCreditAmount > 0)
            {
                e.Result = this.UtilityMember.NumberSet.ToNumber(ExcessCreditAmount);
                e.Handled = true;

                double ZeroValue = this.UtilityMember.NumberSet.ToDouble(e.Result.ToString());
                if (ZeroValue == 0.00)
                {
                    e.Result = "";
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void xrExcessAssetsAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessDebitAmount > 0)
            {
                e.Result = this.UtilityMember.NumberSet.ToNumber(-ExcessDebitAmount);
                e.Handled = true;

                double ZeroValue = this.UtilityMember.NumberSet.ToDouble(e.Result.ToString());
                if (ZeroValue == 0.00)
                {
                    e.Result = "";
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        #endregion

        #region Methods
        public void BindBalanceSheet()
        {
            try
            {

                this.ReportProperties.DateTo = this.ReportProperties.DateAsOn;
                string datetime = this.GetProgressiveDate(this.ReportProperties.DateAsOn);
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                this.ReportTitle = this.ReportProperties.ReportTitle;

                this.SetLandscapeHeader = 1030.25f;
                this.SetLandscapeFooter = 1030.25f;
                this.SetLandscapeFooterDateWidth = 860.00f;
                if (string.IsNullOrEmpty(this.ReportProperties.DateAsOn))
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
                            SetReportTitle();
                            this.ReportPeriod = String.Format("As on: {0}", this.ReportProperties.DateAsOn);
                            setHeaderTitleAlignment();

                            SplashScreenManager.ShowForm(typeof(frmReportWait));
                            BindSubReportSource();
                            GetBalanceSheetExcessAmount();  //01
                            AssignDifferenceInOpeningBalance(); //02

                            GetBalanceSheetOpening();
                            GetBalanceSheetPeriod();

                            SplashScreenManager.CloseForm();
                            base.ShowReport();
                            SetReportBorder();
                        }
                        else
                        {
                            SetReportTitle();
                            ShowReportFilterDialog();
                        }
                    }
                    else
                    {
                        SetReportTitle();
                        this.ReportPeriod = String.Format("As on: {0}", this.ReportProperties.DateAsOn);
                        setHeaderTitleAlignment();
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        BindSubReportSource();
                        GetBalanceSheetExcessAmount();  //01
                        AssignDifferenceInOpeningBalance(); //02

                        GetBalanceSheetOpening();
                        GetBalanceSheetPeriod();

                        SplashScreenManager.CloseForm();
                        base.ShowReport();
                        SetReportBorder();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
        }

        private void SetReportBorder()
        {
            xrtblDifference = AlignContentTable(xrtblDifference);
            xrtblGrandTotal = AlignTotalTable(xrtblGrandTotal);
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            this.SetCurrencyFormat(xrCellLiaCurrenctAmt.Text, xrCellLiaCurrenctAmt);
            this.SetCurrencyFormat(xrcellAssetCurrentAmt.Text, xrcellAssetCurrentAmt);
        }
        public ResultArgs GetBalanceSheet()
        {
            string BalanceSheet = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.BalanceSheet);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.ReportProperties.DateAsOn);

                int LedgerPaddingRequired = (ReportProperties.ShowLedgerCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;
                int GroupPaddingRequired = (ReportProperties.ShowGroupCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;

                dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, 1);
                dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, 1);

                //On 09/12/2024, To set Currnecy
                if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, "0");
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, BalanceSheet);
            }
            return resultArgs;
        }

        public void AssignDifferenceInOpeningBalance()
        {
            if (LiabilityTotal != 0 || assetTotal != 0)
            {
                string BalanceSheetOpeningAmt = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.BalanceSheetOpeningAmt);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);

                    //On 09/12/2024, To set Currnecy
                    if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                    {
                        dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                    }
                    else
                    {
                        dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, "0");
                    }

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.Scalar, BalanceSheetOpeningAmt);
                    DiffOpeningAmount = 0;
                    if (resultArgs != null && resultArgs.Success)
                    {
                        DiffOpeningAmount = this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.Data.ToString());
                    }
                }
            }
        }

        public void BindSubReportSource()
        {
            ResultArgs resultArgs = GetBalanceSheetSource();
            if (resultArgs.Success)
            {
                ReportProperties.ShowGroupCode = 1;
                if (this.ReportProperties.ReportCodeType == (int)ReportCodeType.Standard)
                    ReportProperties.ShowGroupCode = 0;

                xrSubLiabilities.Visible = xrsubAssets.Visible = true;
                DataTable dtBalanceRpt = resultArgs.DataSource.Table;
                dtBalanceRpt.DefaultView.RowFilter = "AMOUNT_ACTUAL < 0";
                DataTable dtLiabilities = dtBalanceRpt.DefaultView.ToTable();
                dtLiabilities.Columns.Add("AMOUNT", dtBalanceRpt.Columns["AMOUNT_ACTUAL"].DataType, "AMOUNT_ACTUAL * -1"); //Change negative value to possitive value
                BalanceSheetLiabilities liabilities = xrSubLiabilities.ReportSource as BalanceSheetLiabilities;
                liabilities.BindBalanceSheetLiability(dtLiabilities);
                LiabilityTotal = liabilities.TotalLiabilities;

                if (ReportProperties.ShowGroupCode == 1)
                {
                    liabilities.LiabilitiesLedgerCodeWidth = LiabilityCodeWidth;
                    liabilities.LiabilitiesLedgerNameWidth = LiabilityNameWidth;
                    liabilities.LiabilitiesAmountWidth = LiabilityAmountWidth;

                    liabilities.LiabilitiesGroupCodewidth = LiabilityCodeWidth;
                    liabilities.LiabilitiesGroupNamewidth = LiabilityNameWidth;
                    liabilities.LiabilitiesGroupAmount = LiabilityAmountWidth;

                    if (this.ReportProperties.ReportCodeType == (int)ReportCodeType.Generalate)
                    {
                        liabilities.LiabilitiesParentCode = LiabilityCodeWidth;
                        liabilities.LiabilitiesParentGroupName = LiabilityNameWidth;
                        liabilities.LiabilitiesParentGroupAmt = LiabilityAmountWidth;
                    }
                    else
                    {
                        liabilities.LiabilitiesParentCode = 0;
                        liabilities.LiabilitiesParentGroupName = LiabilityNameWidth + LiabilityCodeWidth - 3;
                        liabilities.LiabilitiesParentGroupAmt = LiabilityAmountWidth;
                    }
                }
                else
                {
                    liabilities.LiabilitiesLedgerCodeWidth = LiabilityCodeWidth;
                    liabilities.LiabilitiesLedgerNameWidth = LiabilityNameWidth;
                    liabilities.LiabilitiesAmountWidth = LiabilityAmountWidth;

                    liabilities.LiabilitiesGroupCodewidth = LiabilityCodeWidth - 2;
                    liabilities.LiabilitiesGroupNamewidth = LiabilityNameWidth;
                    liabilities.LiabilitiesGroupAmount = LiabilityAmountWidth;

                    if (this.ReportProperties.ReportCodeType == (int)ReportCodeType.Generalate)
                    {
                        liabilities.LiabilitiesParentCode = LiabilityCodeWidth;
                        liabilities.LiabilitiesParentGroupName = LiabilityNameWidth;
                        liabilities.LiabilitiesParentGroupAmt = LiabilityAmountWidth;
                    }
                    else
                    {
                        liabilities.LiabilitiesParentCode = 0;
                        liabilities.LiabilitiesParentGroupName = LiabilityNameWidth + LiabilityCodeWidth;
                        liabilities.LiabilitiesParentGroupAmt = LiabilityAmountWidth;
                    }
                }

                this.AttachDrillDownToSubReport(liabilities);
                liabilities.HideBalanceSheetLiabilityHeader();

                dtBalanceRpt.DefaultView.RowFilter = string.Empty;
                dtBalanceRpt.DefaultView.RowFilter = "AMOUNT_ACTUAL > 0";
                DataTable dtAsset = dtBalanceRpt.DefaultView.ToTable();
                dtAsset.Columns["AMOUNT_ACTUAL"].ColumnName = "AMOUNT";
                BalanceSheetAssets Asset = xrsubAssets.ReportSource as BalanceSheetAssets;
                Asset.BindBalanceSheetAsset(dtAsset);
                assetTotal = Asset.TotalAssets;
                if (ReportProperties.ShowGroupCode == 1)
                {
                    Asset.AssetLedgerCodeWidth = AssetCodeWidth;
                    Asset.AssetLedgerNameWidth = AssetNameWidth + 1;
                    Asset.AssetAmountWidth = AssetAmountWidth + 1;

                    Asset.AssetGroupCodewidth = AssetCodeWidth;
                    Asset.AssetGroupNamewidth = AssetNameWidth + 1;
                    Asset.AssetGroupAmount = AssetAmountWidth + 1;

                    if (this.ReportProperties.ReportCodeType == (int)ReportCodeType.Generalate)
                    {
                        Asset.AssetParentCode = AssetCodeWidth + 1;
                        Asset.AssetParentGroupName = AssetNameWidth;
                        Asset.AssetParentGroupAmt = AssetAmountWidth + 1;
                    }
                    else
                    {
                        Asset.AssetParentCode = 0;
                        Asset.AssetParentGroupName = AssetNameWidth + AssetCodeWidth;
                        Asset.AssetParentGroupAmt = AssetAmountWidth + 1;

                    }
                }
                else
                {
                    Asset.AssetLedgerCodeWidth = AssetCodeWidth;
                    Asset.AssetLedgerNameWidth = AssetNameWidth + 1;
                    Asset.AssetAmountWidth = AssetAmountWidth + 1;

                    Asset.AssetGroupCodewidth = AssetCodeWidth - 1;
                    Asset.AssetGroupNamewidth = AssetNameWidth;
                    Asset.AssetGroupAmount = AssetAmountWidth + 1;

                    if (this.ReportProperties.ReportCodeType == (int)ReportCodeType.Generalate)
                    {
                        Asset.AssetParentCode = AssetCodeWidth + 1;
                        Asset.AssetParentGroupName = AssetNameWidth;
                        Asset.AssetParentGroupAmt = AssetAmountWidth + 1;
                    }
                    else
                    {
                        Asset.AssetParentCode = 0;
                        Asset.AssetParentGroupName = AssetNameWidth + AssetCodeWidth;
                        Asset.AssetParentGroupAmt = AssetAmountWidth;
                    }

                }

                this.AttachDrillDownToSubReport(Asset);
                Asset.AttachDrillDownToAccountBalance(); //For closing balance
                Asset.HideBalanceSheetAssetCapHeader();

                //19/09/2024, To Show Forex split -----------------------------------------------------
                xrsubforex.Visible = false;
                if (this.settingProperty.AllowMultiCurrency == 1)
                {
                    xrsubforex.Visible = true;
                    UcForexSplit forexsplit = xrsubforex.ReportSource as UcForexSplit;
                    xrsubforex.WidthF = xrtblLiaCode.WidthF + xrcolCap2.WidthF;
                    forexsplit.CurrencyNameWidth = xrtblLiaCode.WidthF;
                    forexsplit.GainWidth = (xrcolCap2.WidthF / 2) ;
                    forexsplit.LossWidth = (xrcolCap2.WidthF / 2) ;
                    forexsplit.DateAsOn = ReportProperties.DateAsOn;
                    forexsplit.ShowForex();
                }
                //-------------------------------------------------------------------------------------

               
            }
            else
            {
                xrSubLiabilities.Visible = xrsubAssets.Visible = false;
            }

        }

        private ResultArgs GetBalanceSheetSource()
        {
            string BalanceSheet = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.BalanceSheet);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.ReportProperties.DateAsOn);
                dataManager.Parameters.Add(this.ReportParameters.SHOW_GENERALATEColumn, this.ReportProperties.ReportCodeType.Equals((int)ReportCodeType.Standard) || this.ReportProperties.ReportCodeType.Equals((int)ReportCodeType.Province) ? 0 : (int)ReportCodeType.Generalate);
                int Mode = this.ReportProperties.ReportCodeType.Equals((int)ReportCodeType.Standard) || this.ReportProperties.ReportCodeType.Equals((int)ReportCodeType.Province) ? 0 : (int)ReportCodeType.Generalate;
                int LedgerPaddingRequired = (ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;
                int GroupPaddingRequired = (ReportProperties.ShowByLedgerGroup == 1 && Mode == 1) ? 1 : 0;

                dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, LedgerPaddingRequired);
                dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, GroupPaddingRequired);

                //On 09/12/2024, To set Currnecy
                if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, "0");
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, BalanceSheet);
            }
            return resultArgs;
        }

        private void GetBalanceSheetExcessAmount()
        {
            if (LiabilityTotal != 0 || assetTotal != 0)
            {
                string BalanceSheet = this.GetFinalAccountsReportSQL((this.AppSetting.AllowMultiCurrency==1?  SQL.ReportSQLCommand.FinalAccounts.BalanceSheetExcessDifferenceForMultiCurrency
                            :SQL.ReportSQLCommand.FinalAccounts.BalanceSheetExcessDifference));
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateAsOn);

                    //On 09/12/2024, To set Currnecy
                    if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                    {
                        dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                    }
                    else
                    {
                        dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, "0");
                    }

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, BalanceSheet);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        DataTable dtResource = resultArgs.DataSource.Table;
                        double IESUM = this.UtilityMember.NumberSet.ToDouble(dtResource.Rows[0]["IESUM"].ToString());
                        ExcessCreditAmount = ExcessDebitAmount = 0;
                        if (IESUM < 0)
                        {
                            ExcessCreditAmount = Math.Abs(IESUM);
                        }
                        else
                        {
                            ExcessDebitAmount = IESUM;
                        }
                    }
                }
            }
        }

        private void GetBalanceSheetOpening()
        {
            if (LiabilityTotal != 0 || assetTotal != 0)
            {
                string BalanceOpening = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.BalanceSheetExcessOpeningPeriod);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.settingProperty.YearToPrevious);
                    dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, "OP");

                    //On 09/12/2024, To set Currnecy
                    if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                    {
                        dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                    }
                    else
                    {
                        dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, "0");
                    }

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, BalanceOpening);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        DataTable dtResource = resultArgs.DataSource.Table;
                        double CREDIT_SUM = this.UtilityMember.NumberSet.ToDouble(dtResource.Rows[0]["CREDIT"].ToString());
                        double DEBIT_SUM = this.UtilityMember.NumberSet.ToDouble(dtResource.Rows[0]["DEBIT"].ToString());
                        ExcessOpCreditAmount = ExcessOpDebitAmount = 0;
                        if (CREDIT_SUM > 0 && DEBIT_SUM == 0)
                        {
                            ExcessOpCreditAmount = CREDIT_SUM;
                        }
                        else
                        {
                            ExcessOpDebitAmount = -DEBIT_SUM;
                        }
                    }
                }
            }
        }

        private void GetBalanceSheetPeriod()
        {
            if (LiabilityTotal != 0 || assetTotal != 0)
            {
                string BalancePeriod = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.BalanceSheetExcessOpeningPeriod);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.settingProperty.YearFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateAsOn);
                    dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, "");

                    //On 09/12/2024, To set Currnecy
                    if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                    {
                        dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                    }
                    else
                    {
                        dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, "0");
                    }

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, BalancePeriod);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        DataTable dtResource = resultArgs.DataSource.Table;
                        double CREDIT_SUM = this.UtilityMember.NumberSet.ToDouble(dtResource.Rows[0]["CREDIT"].ToString());
                        double DEBIT_SUM = this.UtilityMember.NumberSet.ToDouble(dtResource.Rows[0]["DEBIT"].ToString());
                        ExcessPeriodCreditAmount = ExcessPeriodDebitAmount = 0;
                        if (CREDIT_SUM > 0 && DEBIT_SUM == 0)
                        {
                            ExcessPeriodCreditAmount = CREDIT_SUM;
                        }
                        else
                        {
                            ExcessPeriodDebitAmount = -DEBIT_SUM;
                        }
                    }
                }
            }
        }

        #endregion


    }
}
