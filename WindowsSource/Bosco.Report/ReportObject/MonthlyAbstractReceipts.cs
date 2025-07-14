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
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class MonthlyAbstractReceipts : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variables
        public int Flag = 0;
        private bool PrevLedgerCCFound = false;
        private bool PrevLedgerDonorFound = false;

        private DataTable dtCCDetails = new DataTable();
        private DataTable dtDonorDetails = new DataTable();

        string AmountCaption = string.Empty;
        string ProgressAmountCaption = string.Empty;
        string BudgetApprovedAmountCaption = string.Empty;
        decimal CurrencyAvgExchangeRate = 1;
        #endregion
        #region Constructor

        public MonthlyAbstractReceipts()
        {
            InitializeComponent();

            AmountCaption = tcCapAmountPeriod.Text;
            ProgressAmountCaption = tcCapAmountProgress.Text;
            BudgetApprovedAmountCaption = tcCapAmountBudget.Text;

            //  ReportProperties.IncludeNarration = 1;
            //10/03/2017, To attach drill-down feature for main parent group too
            this.AttachDrillDownToRecord(xrTableLedgerGroup, tcGrpParentGroupName, new ArrayList { reportSetting1.MonthlyAbstract.GROUP_IDColumn.ColumnName, 
                        ReportParameters.CURRENCY_COUNTRY_IDColumn.ColumnName  }, DrillDownType.GROUP_SUMMARY_RECEIPTS, false, "", true);

            this.AttachDrillDownToRecord(xrTableLedgerGroup, tcGrpGroupName, new ArrayList { reportSetting1.MonthlyAbstract.GROUP_IDColumn.ColumnName,
                        ReportParameters.CURRENCY_COUNTRY_IDColumn.ColumnName}, DrillDownType.GROUP_SUMMARY_RECEIPTS, false, "", true);

            this.AttachDrillDownToRecord(xrtblLedger, tcLedgerName, new ArrayList { reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName,
                        ReportParameters.CURRENCY_COUNTRY_IDColumn.ColumnName}, DrillDownType.LEDGER_SUMMARY_RECEIPTS, false, "", true);
        }
        #endregion

        #region ShowReport

        public override void ShowReport()
        {
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                || this.ReportProperties.Project == "0")
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
                        BindReceiptSource();
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
                    BindReceiptSource();
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }

        }

        #endregion

        #region Methods

        public void HideReportHeaderFooter()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
        }

        public void BindReceiptSource()
        {
            SettingProperty settingProperty = new SettingProperty();

            // this.HideReportHeader = true;
            SetReportTitle();
            
            ResultArgs resultArgs = GetReportSource();
            if (resultArgs.Success)
            {
                DataView dvReceipt = resultArgs.DataSource.TableView;
                if (dvReceipt != null)
                {
                    //On 02/07/2024, To skip unused default unused ledgers
                    /*if (ReportProperties.IncludeAllLedger == 1)
                    {
                        string skipdefaultledgers = "ACCESS_FLAG <> 2 OR " + reportSetting1.MonthlyAbstract.AMOUNT_PERIODColumn.ColumnName + " > 0 OR " +
                                              reportSetting1.MonthlyAbstract.AMOUNT_PROGRESSIVEColumn.ColumnName + " > 0";

                        dtReceipt.DefaultView.RowFilter = skipdefaultledgers;
                        dtReceipt = dtReceipt.DefaultView.ToTable();
                    }*/

                    dvReceipt.Table.TableName = "MonthlyAbstract";
                    
                    this.DataSource = dvReceipt;
                    this.DataMember = dvReceipt.Table.TableName;
                    AccountBalance accountBalance = xrSubBalance.ReportSource as AccountBalance;
                    SetReportSetting(dvReceipt, accountBalance);
                    //accountBalance.BankClosedDate = this.ReportProperties.DateFrom; //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                    accountBalance.BankClosedDate = this.AppSetting.YearFrom; //05/06/2020 to handle closed bank accounts for Progress Total from (this.AppSetting.YearFrom)
                    accountBalance.BindBalance(true, true);
                    this.AttachDrillDownToSubReport(accountBalance);
                    
                    prBalancePeriodAmount.Value = accountBalance.PeriodBalanceAmount;
                    prBalanceProgressiveAmount.Value = accountBalance.ProgressiveBalanceAmount;
                    SetReportBorder();
                    SetBalanceReportColumnWidth(accountBalance);
                }
            }
            SortByLedgerorGroup();
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlMonthlyAbstractReceipts = this.GetReportSQL(SQL.ReportSQLCommand.Report.MonthlyAbstract);
            string dateProgress = this.GetProgressiveDate(this.ReportProperties.DateFrom);
            string liquidityGroupIds = this.GetLiquidityGroupIds();

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_PROGRESS_FROMColumn, dateProgress);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, TransType.RC.ToString());
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, liquidityGroupIds);
                dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, TransMode.CR.ToString());

                int LedgerPaddingRequired = (ReportProperties.ShowLedgerCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;
                int GroupPaddingRequired = (ReportProperties.ShowGroupCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;

                dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, LedgerPaddingRequired);
                dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, GroupPaddingRequired);
                dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, TransMode.CR.ToString());

                dataManager.Parameters.Add(this.ReportParameters.INCLUDE_ALL_LEDGERColumn.ColumnName, this.ReportProperties.IncludeAllLedger);

                dataManager.Parameters.Add(this.ReportParameters.YEAR_FROMColumn, settingProperty.YearFrom);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_TOColumn, settingProperty.YearTo);

                //On 04/07/2024, If include all ledger(s) are enabled, let us load only for Income, Asset and Liability
                if (this.ReportProperties.IncludeAllLedger == 1)
                {
                    string receiptnature = (Int32)Natures.Income + "," + (Int32)Natures.Assert + "," + (Int32)Natures.Libilities;
                    dataManager.Parameters.Add(this.ReportParameters.NATURE_IDColumn, receiptnature);
                }

                //On 26/11/2024, To to currency based reports --------------------------------------------------------------------------------------
                CurrencyAvgExchangeRate = 1;
                if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                {
                    CurrencyAvgExchangeRate = GetAvgCurrencyExchangeRateForFY(this.ReportProperties.CurrencyCountryId);
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                }
                dataManager.Parameters.Add(reportSetting1.Country.EXCHANGE_RATEColumn, CurrencyAvgExchangeRate);
                //----------------------------------------------------------------------------------------------------------------------------------

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, sqlMonthlyAbstractReceipts);
            }

            //On 18/02/2021, to get CC detail for Monthly recipets, it will be used to when reports generates
            xrSubreportCCDetails.Visible = false;
            if (this.ReportProperties.ShowCCDetails == 1)
            {
                xrSubreportCCDetails.Visible = true;
                AssignCCDetailReportSource();
            }

            //On 18/02/2021, to get CC detail for Monthly recipets, it will be used to when reports generates
            xrSubreportDonorDetails.Visible = false;
            if (this.ReportProperties.ShowDonorDetails == 1)
            {
                xrSubreportDonorDetails.Visible = true;
                AssignDonorDetailReportSource();
            }

            return resultArgs;
        }

        private void AssignCCDetailReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlccDetail = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CCDetailMonthlyAbstract);
            string dateProgress = this.GetProgressiveDate(this.ReportProperties.DateFrom);

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_PROGRESS_FROMColumn, dateProgress);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, TransType.RC.ToString());
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, TransMode.CR.ToString());

                dataManager.Parameters.Add(this.ReportParameters.YEAR_FROMColumn, settingProperty.YearFrom);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_TOColumn, settingProperty.YearTo);

                //On 26/11/2024, To to currency based reports --------------------------------------------------------------------------------------
                if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, "0");
                }
                //----------------------------------------------------------------------------------------------------------------------------------

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlccDetail);
            }

            if (resultArgs.Success && resultArgs.DataSource.Table != null)
            {
                dtCCDetails = resultArgs.DataSource.Table;

                dtCCDetails.Columns[reportSetting1.Ledger.AMOUNT_PERIODColumn.ColumnName].ColumnName = reportSetting1.Ledger.DEBITColumn.ColumnName;
                dtCCDetails.Columns[reportSetting1.Ledger.AMOUNT_PROGRESSIVEColumn.ColumnName].ColumnName = reportSetting1.Ledger.CREDITColumn.ColumnName;
            }
        }

        private void AssignDonorDetailReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlccDetail = this.GetReportForeginContribution(SQL.ReportSQLCommand.ForeginContribution.DonorDetailMonthlyAbstract);
            string dateProgress = this.GetProgressiveDate(this.ReportProperties.DateFrom);

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_PROGRESS_FROMColumn, dateProgress);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, TransType.RC.ToString());
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, TransMode.CR.ToString());
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlccDetail);
            }

            if (resultArgs.Success && resultArgs.DataSource.Table != null)
            {
                dtDonorDetails = resultArgs.DataSource.Table;

                dtDonorDetails.Columns[reportSetting1.Ledger.AMOUNT_PERIODColumn.ColumnName].ColumnName = reportSetting1.Ledger.DEBITColumn.ColumnName;
                dtDonorDetails.Columns[reportSetting1.Ledger.AMOUNT_PROGRESSIVEColumn.ColumnName].ColumnName = reportSetting1.Ledger.CREDITColumn.ColumnName;
            }
        }

        private void SetReportSetting(DataView dvReceipt, AccountBalance accountBalance)
        {
            float actualCodeWidth = tcCapCode.WidthF;
            bool isCapCodeVisible = true;

            //On 04/03/2024, To show Budget Approved Amount -----------------------------------------------------------------
            xrlLnOpBorder.Visible = false;
            if (settingProperty.ShowBudgetApprovedAmountInMonthlyReport == 1)
            {
                xrlLnOpBorder.Visible = true;
            }
            if (settingProperty.ShowBudgetApprovedAmountInMonthlyReport != 1)
            {
                RemoveTableCell(xrTableHeader, xrRowHeader, tcCapAmountBudget);
                RemoveTableCell(xrtblParentGroup, xrRowParetnGroup, xrsumParentBudgetApproved);
                RemoveTableCell(xrTableLedgerGroup, xrRowGroup, xrsumGroupBudgetAmount);
                RemoveTableCell(xrtblLedger, xrRowLedger, tcAmountBudget);
                RemoveTableCell(xrtblGrandTotal, xrRowGrandTotal, tcGTotAmountBudget);
                RemoveTableCell(xrtblTotal, xrRowTotal, tcTotAmountBudget);
            }
            //---------------------------------------------------------------------------------------------------------------

            //Attach / Detach all ledgers--------------------------------------------------------------------
            //dvReceipt.RowFilter = "";
            //if (ReportProperties.IncludeAllLedger == 0)
            //{
            //    dvReceipt.RowFilter = reportSetting1.MonthlyAbstract.HAS_TRANSColumn.ColumnName + " = 1";
            //}

            //if (dvReceipt.Count == 0)
            //{
            //    DataRowView drvReceipt = dvReceipt.AddNew();
            //    drvReceipt.BeginEdit();
            //    drvReceipt[reportSetting1.MonthlyAbstract.AMOUNT_PERIODColumn.ColumnName] = 0;
            //    drvReceipt[reportSetting1.MonthlyAbstract.AMOUNT_PROGRESSIVEColumn.ColumnName] = 0;
            //    drvReceipt[reportSetting1.MonthlyAbstract.HAS_TRANSColumn.ColumnName] = 1;
            //    drvReceipt.EndEdit();
            //}
            //-----------------------------------------------------------------------------------------------------

            //Include / Exclude Code
            if (tcCapCode.Tag != null && tcCapCode.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(tcCapCode.Tag.ToString());
            }
            else
            {
                tcCapCode.Tag = tcCapCode.WidthF;
            }

            isCapCodeVisible = (ReportProperties.ShowGroupCode == 1 || ReportProperties.ShowLedgerCode == 1);
            tcCapCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            tcGrpParentCode.WidthF = ((ReportProperties.ShowGroupCode == 1) ? actualCodeWidth : 0);
            tcGrpGroupCode.WidthF = ((ReportProperties.ShowGroupCode == 1) ? actualCodeWidth : 0);
            tcLedgerCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);

            tcCapAmountBudget.Text = "Budget " + UtilityMember.DateSet.ToDate(settingProperty.YearFrom, false).Year + "-" 
                + UtilityMember.DateSet.ToDate(settingProperty.YearTo, false).ToString("yy") + "("+ 
                (this.AppSetting.AllowMultiCurrency == 1 && !string.IsNullOrEmpty(ReportProperties.CurrencyCountrySymbol) ? ReportProperties.CurrencyCountrySymbol : this.AppSetting.Currency) + ")";

            //Include / Exclude Ledger group or Ledger
            grpLedgerGroup.Visible = grpParentGroup.Visible = (ReportProperties.ShowByLedgerGroup == 1); // 

            if (ReportProperties.ShowByLedger == 0 && ReportProperties.ShowByLedgerGroup == 0)
            {
                ReportProperties.ShowByLedger = 1;   //grpLedger.Visible = (ReportProperties.ShowByLedger == 1);
            }
            Detail.Visible = (ReportProperties.ShowByLedger == 1);

            grpLedgerGroup.GroupFields[0].FieldName = "";
            grpParentGroup.GroupFields[0].FieldName = "";
            //grpLedger.GroupFields[0].FieldName = "";

            //if (grpLedgerGroup.Visible == false && grpLedger.Visible == false)
            //{
            //    grpLedger.Visible = true;
            //}

            if (grpParentGroup.Visible)
            {
                if (ReportProperties.SortByGroup == 1)
                {
                    grpParentGroup.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.PARENT_GROUPColumn.ColumnName;
                }
                else
                {
                    grpParentGroup.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.PARENT_GROUPColumn.ColumnName;
                }
            }

            if (grpLedgerGroup.Visible)
            {
                if (ReportProperties.SortByGroup == 1)
                {
                    grpLedgerGroup.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName;
                }
                else
                {
                    grpLedgerGroup.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName;
                }
            }

            //if (grpLedger.Visible)
            //{
            //    if (ReportProperties.SortByLedger == 1)
            //    {
            //        grpLedger.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.LEDGER_NAMEColumn.ColumnName;
            //    }
            //    else
            //    {
            //        grpLedger.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.LEDGER_NAMEColumn.ColumnName;
            //    }
            //}
            //Group Row Style
            //if (grpLedger.Visible == false)
            //{
            //    styleGroupRow.BackColor = Color.White;
            //    styleGroupRow.Borders = styleRow.Borders;
            //    xrTableLedgerGroup.StyleName = styleGroupRow.Name;
            //}
            //else
            //{
            //    xrTableLedgerGroup.StyleName = styleGroupRowBase.Name;
            //}

            grpEmptyRow.Visible = (ReportProperties.ShowByLedger == 1 && ReportProperties.ShowByLedgerGroup == 0);
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            this.SetLandscapeBudgetNameWidth = xrTableHeader.WidthF;
            this.SetLandscapeHeader = xrTableHeader.WidthF;
            this.SetLandscapeFooter = xrTableHeader.WidthF;
            this.SetLandscapeFooterDateWidth = xrTableHeader.WidthF;
            SetTitleWidth(xrTableHeader.WidthF);

            SetBalanceReportColumnWidth(accountBalance);
            this.setHeaderTitleAlignment();
        }

        private void SetBalanceReportColumnWidth(AccountBalance accountBalance)
        {
            accountBalance.CodeColumnWidth = tcCapCode.WidthF;
            accountBalance.NameColumnWidth = tcCapParticulars.WidthF;
            accountBalance.NameHeaderColumWidth = (tcCapCode.WidthF + tcCapParticulars.WidthF) - 2;
            accountBalance.AmountColumnWidth = tcCapAmountPeriod.WidthF;
            accountBalance.AmountHeaderColumWidth = tcCapAmountPeriod.WidthF;
            accountBalance.AmountProgressiveColumnWidth = accountBalance.AmountProgressiveHeaderColumnWidth = tcCapAmountProgress.WidthF;
        }

        private void RemoveTableCell(XRTable tbl, XRTableRow row, XRTableCell cell)
        {
            tbl.BeginInit();
            tbl.SuspendLayout();
            if (row.Cells.Contains(cell))
                row.Cells.Remove(row.Cells[cell.Name]);
            tbl.PerformLayout();
            tbl.EndInit();
        }

        private void SetReportBorder()
        {
            xrTableHeader = AlignHeaderTable(xrTableHeader);
            xrtblLedger = AlignContentTable(xrtblLedger);
            xrtblemptyRow = AlignContentTable(xrtblemptyRow);
            xrTableLedgerGroup = AlignGroupTable(xrTableLedgerGroup);
            xrtblParentGroup = AlignGroupTable(xrtblParentGroup);
            xrtblTotal = AlignTotalTable(xrtblTotal);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);
            xrTblOpeningBalance = AlignContentTable(xrTblOpeningBalance);

            if (this.settingProperty.AllowMultiCurrency == 1)
            {
                if (!string.IsNullOrEmpty(ReportProperties.CurrencyCountrySymbol))
                {
                    tcCapAmountPeriod.Text = AmountCaption + " (" + ReportProperties.CurrencyCountrySymbol + ")";
                    tcCapAmountProgress.Text = ProgressAmountCaption + " (" + ReportProperties.CurrencyCountrySymbol + ")";
                    //tcCapAmountBudget.Text = BudgetApprovedAmountCaption + " (" + ReportProperties.CurrencyCountrySymbol + ")";
                }
            }
            else
            {
                this.SetCurrencyFormat(tcCapAmountPeriod.Text, tcCapAmountPeriod);
                this.SetCurrencyFormat(tcCapAmountProgress.Text, tcCapAmountProgress);
                this.SetCurrencyFormat(tcCapAmountBudget.Text, tcCapAmountBudget);
            }
            xrlLnOpBorder.ForeColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
        }

        public override XRTable AlignHeaderTable(XRTable table, bool UseSameFont = false)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.All;
                            if (ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Bottom | BorderSide.Top;
                        else if (count == trow.Cells.Count)
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom | BorderSide.Top;
                        }
                        else
                            tcell.Borders = BorderSide.Bottom | BorderSide.Top;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = BorderSide.Left;
                        else if (count == 1)
                            tcell.Borders = BorderSide.All;
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                    tcell.Font = (ReportProperty.Current.ColumnCaptionFontStyle == 0 ? this.styleColumnHeader.Font : new Font(this.styleColumnHeader.Font, FontStyle.Regular));
                }
            }
            return table;
        }

        private void SortByLedgerorGroup()
        {
            if (grpParentGroup.Visible)
            {
                grpParentGroup.SortingSummary.Enabled = true;
                //On 03/04/2020, to keep ledger group second leavel proper order 
                //grpParentGroup.SortingSummary.FieldName = "SORT_ORDER";

                if (this.ReportProperties.ReportId == "RPT-227")
                {
                    grpParentGroup.SortingSummary.FieldName = "NATURE_ID";
                }
                else
                {
                    grpParentGroup.SortingSummary.FieldName = reportSetting1.MonthlyAbstract.PARENT_GROUPColumn.ColumnName; //"SORT_ORDER";
                }
                // + reportSetting1.MonthlyAbstract.PARENT_GROUPColumn.ColumnName; //"SORT_ORDER";
                grpParentGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                grpParentGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            }

            if (grpLedgerGroup.Visible)
            {
                if (this.ReportProperties.SortByGroup == 0)
                {
                    grpLedgerGroup.SortingSummary.Enabled = true;
                    //grpLedgerGroup.SortingSummary.FieldName = "SORT_ORDER"; //On 03/04/2020, To have proper Parent Group and Ledger Group
                    grpLedgerGroup.SortingSummary.FieldName = reportSetting1.MonthlyAbstract.PARENT_GROUPColumn.ColumnName; //"SORT_ORDER";
                    grpLedgerGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpLedgerGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
                else
                {
                    grpLedgerGroup.SortingSummary.Enabled = true;
                    //grpLedgerGroup.SortingSummary.FieldName = "SORT_ORDER"; //On 03/04/2020, To have proper Parent Group and Ledger Group
                    grpLedgerGroup.SortingSummary.FieldName = reportSetting1.MonthlyAbstract.PARENT_GROUPColumn.ColumnName; //"SORT_ORDER";
                    grpLedgerGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpLedgerGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
            }

            //On 10/05/2019, To remove ledger group (it is already grouped in sql itself and to have proper ledger code sorting)
            Detail.SortFields.Clear();

            if (this.ReportProperties.ReportId == "RPT-227")
            {
                Detail.SortFields.Add(new GroupField("NATURE_ID", XRColumnSortOrder.Ascending));
            }

            if (this.ReportProperties.SortByLedger == 0)
            {
                Detail.SortFields.Add(new GroupField("LEDGER_CODE", XRColumnSortOrder.Ascending));
                Detail.SortFields.Add(new GroupField("LEDGER_NAME", XRColumnSortOrder.Ascending));
            }
            else
            {
                Detail.SortFields.Add(new GroupField("LEDGER_NAME", XRColumnSortOrder.Ascending));
            }

            //if (grpLedger.Visible)
            //{
            //    if (this.ReportProperties.SortByLedger == 0)
            //    {
            //        grpLedger.SortingSummary.Enabled = true;
            //        if (this.ReportProperties.ShowByLedgerGroup == 1)
            //        {
            //            grpLedger.SortingSummary.FieldName = "SORT_ORDER";
            //            grpLedger.SortingSummary.FieldName = "LEDGER_CODE";
            //        }
            //        else
            //        {
            //            grpLedger.SortingSummary.FieldName = "LEDGER_CODE";
            //        }
            //        grpLedger.SortingSummary.Function = SortingSummaryFunction.Avg;
            //        grpLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //    }
            //    else
            //    {
            //        grpLedger.SortingSummary.Enabled = true;
            //        if (this.ReportProperties.ShowByLedgerGroup == 1)
            //        {
            //            grpLedger.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_CODE
            //            grpLedger.SortingSummary.FieldName = "LEDGER_NAME";
            //        }
            //        else
            //        {
            //            grpLedger.SortingSummary.FieldName = "LEDGER_NAME";
            //        }
            //        grpLedger.SortingSummary.Function = SortingSummaryFunction.Avg;
            //        grpLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //    }
            //}
        }
        #endregion

        #region Events

        private void tcAmountPeriod_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double PeriodAmt = this.ReportProperties.NumberSet.ToDouble(tcAmountPeriod.Text);
            if (PeriodAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                tcAmountPeriod.Text = "";
            }
        }

        private void tcAmountProgress_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double ProgressAmt = this.ReportProperties.NumberSet.ToDouble(tcAmountProgress.Text);
            if (ProgressAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                tcAmountProgress.Text = "";
            }
        }

        #endregion

        private void grpLedgerGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName) != null)
            {
                string ParentGroup = GetCurrentColumnValue(reportSetting1.MonthlyAbstract.PARENT_GROUPColumn.ColumnName) != null ?
                    GetCurrentColumnValue(reportSetting1.MonthlyAbstract.PARENT_GROUPColumn.ColumnName).ToString() : string.Empty;
                string LedgerGroup = GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName) != null ?
                    GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName).ToString() : string.Empty;

                if (ParentGroup.Trim().Equals(LedgerGroup.Trim()))
                {
                    e.Cancel = true;
                }
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //On 17/02/2021, To show CC detail for given Ledger
            ShowCCDetails();
            ShowDonorDetails();
            Detail.HeightF = 25;
        }

        private void ShowCCDetails()
        {
            //On 17/02/2021, To show CC detail for given Ledger
            xrSubreportCCDetails.Visible = false;
            if (this.ReportProperties.ShowCCDetails == 1)
            {
                if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName) != null && dtCCDetails.Rows.Count > 0)
                {
                    Int32 ledgerid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName).ToString());
                    UcCCDetail ccDetail = xrSubreportCCDetails.ReportSource as UcCCDetail;
                    dtCCDetails.DefaultView.RowFilter = string.Empty;
                    dtCCDetails.DefaultView.RowFilter = reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName + " = " + ledgerid;
                    DataTable dtCC = dtCCDetails.DefaultView.ToTable();

                    ccDetail.BindCCDetails(dtCC, true, false, false, true);
                    ccDetail.DateWidth = 0;

                    xrSubreportCCDetails.LeftF = tcLedgerCode.WidthF + 2;
                    ccDetail.CCCreditCaption = "";
                    ccDetail.CCDebitCaption = "";
                    ccDetail.CCTableWidth = tcCapParticulars.WidthF + tcCapAmountPeriod.WidthF + tcCapAmountProgress.WidthF + tcCapAmountBudget.WidthF;
                    ccDetail.CCNameWidth = tcCapParticulars.WidthF;
                    ccDetail.CCDebitWidth = tcCapAmountPeriod.WidthF;
                    ccDetail.CCCreditWidth = tcCapAmountProgress.WidthF;
                    ccDetail.PRojectNameWidth = (tcCapParticulars.WidthF + tcCapAmountPeriod.WidthF + tcCapAmountProgress.WidthF);
                    ccDetail.HideReportHeaderFooter();
                    dtCCDetails.DefaultView.RowFilter = string.Empty;

                    xrSubreportCCDetails.Visible = (dtCC.Rows.Count > 0);
                    PrevLedgerCCFound = (dtCC.Rows.Count > 0);
                }
                else
                {
                    xrSubreportCCDetails.Visible = false;
                    PrevLedgerCCFound = false;
                }
            }
            else
            {
                xrSubreportCCDetails.Visible = false;
                PrevLedgerCCFound = false;
            }


            if (!xrSubreportCCDetails.Visible)
            {
                if (Detail.Controls.Contains(xrSubreportCCDetails))
                {
                    Detail.Controls.Remove(xrSubreportCCDetails);
                }
            }
            else
            {
                Detail.Controls.Add(xrSubreportCCDetails);
            }
        }

        private void ShowDonorDetails()
        {
            //On 17/02/2021, To show CC detail for given Ledger
            xrSubreportDonorDetails.Visible = false;
            if (this.ReportProperties.ShowDonorDetails == 1)
            {
                if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName) != null && dtDonorDetails.Rows.Count > 0)
                {
                    Int32 ledgerid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName).ToString());
                    UcCCDonorDetail donorDetail = xrSubreportDonorDetails.ReportSource as UcCCDonorDetail;
                    dtDonorDetails.DefaultView.RowFilter = string.Empty;
                    dtDonorDetails.DefaultView.RowFilter = reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName + " = " + ledgerid;
                    DataTable dtDonor = dtDonorDetails.DefaultView.ToTable();

                    donorDetail.BindDonorDetails(dtDonor, true);
                    donorDetail.DateWidth = 0;

                    xrSubreportDonorDetails.LeftF = tcLedgerCode.WidthF;
                    donorDetail.DonorCreditCaption = "";
                    donorDetail.DonorDebitCaption = "";
                    donorDetail.DonorTableWidth = tcCapParticulars.WidthF + tcCapAmountPeriod.WidthF + tcCapAmountProgress.WidthF;
                    donorDetail.DonorNameWidth = tcCapParticulars.WidthF;
                    donorDetail.DonorDebitWidth = tcCapAmountPeriod.WidthF;
                    donorDetail.DonorCreditWidth = tcCapAmountProgress.WidthF;
                    donorDetail.ProjectNameWidth = (tcCapParticulars.WidthF + tcCapAmountPeriod.WidthF + tcCapAmountProgress.WidthF);
                    donorDetail.HideReportHeaderFooter();
                    dtDonorDetails.DefaultView.RowFilter = string.Empty;

                    xrSubreportDonorDetails.Visible = (dtDonor.Rows.Count > 0);
                    PrevLedgerDonorFound = (dtDonor.Rows.Count > 0);
                }
                else
                {
                    xrSubreportDonorDetails.Visible = false;
                    PrevLedgerDonorFound = false;
                }
            }
            else
            {
                xrSubreportDonorDetails.Visible = false;
                PrevLedgerDonorFound = false;
            }

            if (!xrSubreportDonorDetails.Visible)
            {
                if (Detail.Controls.Contains(xrSubreportDonorDetails))
                {
                    Detail.Controls.Remove(xrSubreportDonorDetails);
                }
            }
            else
            {
                xrSubreportDonorDetails.TopF = xrSubreportCCDetails.Visible ? (xrSubreportCCDetails.TopF + xrSubreportCCDetails.HeightF) : xrtblLedger.HeightF;
                Detail.Controls.Add(xrSubreportDonorDetails);
            }
        }

        private void ProperBorderForLedgerRow(bool ccFound, bool ccDonorFound)
        {
            if (ccFound || ccDonorFound)
            {
                tcLedgerCode.Borders = BorderSide.Top | BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                tcLedgerName.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                tcAmountPeriod.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                tcAmountProgress.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                tcAmountBudget.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
            }
            else
            {
                tcLedgerCode.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                tcLedgerName.Borders = BorderSide.Right | BorderSide.Bottom;
                tcAmountPeriod.Borders = BorderSide.Right | BorderSide.Bottom;
                tcAmountProgress.Borders = BorderSide.Right | BorderSide.Bottom;
                tcAmountBudget.Borders = BorderSide.Right | BorderSide.Bottom;
            }
        }

        private void xrtblLedger_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void Detail_AfterPrint(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_NAMEColumn.ColumnName) != null)
            {
                string ledgername = GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_NAMEColumn.ColumnName).ToString();
                ProperBorderForLedgerRow(PrevLedgerCCFound, PrevLedgerDonorFound);
            }
        }

        private void tcAmountBudget_EvaluateBinding(object sender, BindingEventArgs e)
        {
            /*try
            {
                if (dtBudgetApproved!=null && GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName) != null)
                {
                    Int32 lid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName).ToString());
                    string fitler = reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName + " = " + lid;
                    dtBudgetApproved.DefaultView.RowFilter = string.Empty;
                    dtBudgetApproved.DefaultView.RowFilter = fitler;
                    if (dtBudgetApproved.DefaultView.Count > 0)
                    {
                        e.Value = dtBudgetApproved.DefaultView[0][reportSetting1.MonthlyAbstract.APPROVED_AMOUNTColumn.ColumnName];
                    }
                    dtBudgetApproved.DefaultView.RowFilter = string.Empty;
                }
            }
            catch(Exception err)
            {
                MessageRender.ShowMessage(err.Message);
                dtBudgetApproved.DefaultView.RowFilter = string.Empty;
            }*/
        }

        private void xrsumParentBudgetApproved_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {

        }

        private void xrsumGroupBudgetAmount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {

        }

        private void tcTotAmountBudget_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            /*try
            {
                if (dtBudgetApproved != null )
                {
                    e.Result = dtBudgetApproved.Compute("SUM(" + reportSetting1.MonthlyAbstract.APPROVED_AMOUNTColumn + ")", string.Empty);
                    e.Handled = true;
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }*/
        }

        private void tcGTotAmountBudget_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            /*try
            {
                if (dtBudgetApproved != null)
                {
                    e.Result = dtBudgetApproved.Compute("SUM(" + reportSetting1.MonthlyAbstract.APPROVED_AMOUNTColumn + ")", string.Empty);
                    e.Handled = true;
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }*/
        }

        private void xrsumGroupBudgetAmount_EvaluateBinding(object sender, BindingEventArgs e)
        {
            /*try
            {
                if (dtBudgetApproved != null && GetCurrentColumnValue(reportSetting1.MonthlyAbstract.GROUP_IDColumn.ColumnName) != null)
                {
                    Int32 pgid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.MonthlyAbstract.PARENT_GROUP_IDColumn.ColumnName).ToString());
                    Int32 gid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.MonthlyAbstract.GROUP_IDColumn.ColumnName).ToString());
                    if (gid == 36)
                    {

                    }

                    string fitler = reportSetting1.MonthlyAbstract.GROUP_IDColumn.ColumnName + " = " + gid + " AND " +
                                    reportSetting1.MonthlyAbstract.APPROVED_AMOUNTColumn + ">0";
                    e.Value = dtBudgetApproved.Compute("SUM(" + reportSetting1.MonthlyAbstract.APPROVED_AMOUNTColumn + ")", fitler).ToString();
                    //e.Value = 45;
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }*/
        }

        private void xrsumParentBudgetApproved_EvaluateBinding(object sender, BindingEventArgs e)
        {
            /*try
            {
                if (dtBudgetApproved != null && GetCurrentColumnValue(reportSetting1.MonthlyAbstract.PARENT_GROUP_IDColumn.ColumnName) != null)
                {
                    Int32 pgid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.MonthlyAbstract.PARENT_GROUP_IDColumn.ColumnName).ToString());
                    Int32 gid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.MonthlyAbstract.GROUP_IDColumn.ColumnName).ToString());
                    
                    string fitler = reportSetting1.MonthlyAbstract.GROUP_IDColumn.ColumnName + " = " + gid + " AND " +
                                    reportSetting1.MonthlyAbstract.APPROVED_AMOUNTColumn + ">0";
                    e.Value = dtBudgetApproved.Compute("SUM(" + reportSetting1.MonthlyAbstract.APPROVED_AMOUNTColumn + ")", fitler);
                    //e.Value = 55;
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }*/
        }

        private void tcCapAmountBudget_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //On 05/03/2024, To hide, Budget column
            if (settingProperty.ShowBudgetApprovedAmountInMonthlyReport != 1) e.Cancel = true;
        }

        private void xrsumParentBudgetApproved_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //On 05/03/2024, To hide, Budget column
            if (settingProperty.ShowBudgetApprovedAmountInMonthlyReport != 1) e.Cancel = true;
        }

        private void xrsumGroupBudgetAmount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //On 05/03/2024, To hide, Budget column
            if (settingProperty.ShowBudgetApprovedAmountInMonthlyReport != 1) e.Cancel = true;
        }

        private void tcAmountBudget_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //On 05/03/2024, To hide, Budget column
            if (settingProperty.ShowBudgetApprovedAmountInMonthlyReport != 1) e.Cancel = true;
        }

        private void tcTotAmountBudget_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //On 05/03/2024, To hide, Budget column
            if (settingProperty.ShowBudgetApprovedAmountInMonthlyReport != 1) e.Cancel = true;
        }

        private void tcGTotAmountBudget_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //On 05/03/2024, To hide, Budget column
            if (settingProperty.ShowBudgetApprovedAmountInMonthlyReport != 1) e.Cancel = true;
        }

        private void xrTblOpeningBalance_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName) != null)
            {
                (sender as XRTable).WidthF = xrTableHeader.WidthF;
            }
        }

        private void xrtblemptyRow_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName) != null)
            {
                (sender as XRTable).WidthF = xrTableHeader.WidthF;
            }
        }

        private void xrTblOPBalanceDummy_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName) != null)
            //{
            //    XRTable tbl = sender as XRTable;
            //    tbl.LeftF = (xrTableHeader.WidthF - tcAmountBudget.WidthF); 
            //    tbl.TopF = xrtblemptyRow.TopF + xrtblemptyRow.HeightF;
            //    tbl.WidthF = tcAmountBudget.WidthF;
            //    tbl.HeightF = xrSubBalance.HeightF;
            //}
        }

        private void xrCellemptyRow_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName) != null)
            {
                (sender as XRTableCell).Borders = BorderSide.All;
            }
        }

        private void tcTotAmountPeriod_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //27/11/2024, For Tmep -not binding if there is no records
            ForTempBindValueIfEmptyDatasource(true, sender);
        }

        private void tcTotAmountProgress_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //27/11/2024, For Tmep -not binding if there is no records
            ForTempBindValueIfEmptyDatasource(false, sender);
        }

        /// <summary>
        /// For Tmep not binding if there is no records
        /// </summary>
        private void ForTempBindValueIfEmptyDatasource(bool isperiod, object sender)
        {
            //27/11/2024, For Tmep not binding if there is no records
            if (this.DataSource != null)
            {
                DataView dv = this.DataSource as DataView;
                if (dv.Count == 0)
                {
                    if (isperiod && prBalancePeriodAmount.Value != null)
                    {
                        double amt = UtilityMember.NumberSet.ToDouble(prBalancePeriodAmount.Value.ToString());
                        (sender as XRTableCell).Text = UtilityMember.NumberSet.ToNumber(amt);
                    }
                    else if (!isperiod && prBalanceProgressiveAmount.Value != null)
                    {
                        double amt = UtilityMember.NumberSet.ToDouble(prBalanceProgressiveAmount.Value.ToString());
                        (sender as XRTableCell).Text = UtilityMember.NumberSet.ToNumber(amt);
                    }
                }
            }
        }

        

        private void tcGTotAmountPeriod_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //27/11/2024, For Tmep -not binding if there is no records
            ForTempBindValueIfEmptyDatasource(true, sender);
        }

        private void tcGTotAmountProgressive_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //27/11/2024, For Tmep -not binding if there is no records
            ForTempBindValueIfEmptyDatasource(false, sender);
        }
    }
}
