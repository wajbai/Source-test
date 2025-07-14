using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using System.Data;
using Bosco.Report.Base;
using DevExpress.XtraReports.UI.PivotGrid;
using Bosco.Utility.ConfigSetting;

namespace Bosco.Report.ReportObject
{
    public partial class AccountBalanceMulti : Report.Base.ReportBase
    {
        public int NoOfMonths = 1;
        ResultArgs resultArgs = null;
        Graphics gr = Graphics.FromHwnd(IntPtr.Zero);
        UserProperty settinguserProperty = new UserProperty(); // 02.09.2022

        public double PeriodBalanceAmount { get; set; }

        public float LeftPosition
        {
            set
            {
                xrPGAccountBalanceMulti.LeftF = value;
            }
        }

        public int GroupCodeColumnWidth
        {
            set
            {
                fieldGROUPCODE.Width = value;
            }
        }

        public int GroupNameColumnWidth
        {
            set
            {
                fieldLEDGERGROUP.Width = value;
            }
        }

        public int LedgerCodeColumnWidth
        {
            set
            {
                fieldLEDGERCODE.Width = value;
            }
        }

        public int LedgerNameColumnWidth
        {
            set
            {
                fieldLEDGERNAME.Width = value;
            }
        }

        public int AmountColumnWidth
        {
            set
            {
                fieldMONTHNAME.Width = value;
                fieldAMOUNT.Width = value;
            }
        }

        public bool ShowColumnHeader
        {
            set
            {
                xrPGAccountBalanceMulti.OptionsView.ShowRowHeaders = value;
            }
        }

        DataTable dtBalGrantTotal = null;

        public DataTable GrantTotalBalance
        {
            get
            {
                return GetGrantTotalSource();
            }
        }

        public XRPivotGridStyles ApplyParentReportStyle
        {
            set
            {
                xrPGAccountBalanceMulti.Styles.CellStyle.Font = new Font(value.CellStyle.Font, value.CellStyle.Font.Style);
                xrPGAccountBalanceMulti.Styles.FieldHeaderStyle.Font = new Font(value.FieldHeaderStyle.Font, value.FieldHeaderStyle.Font.Style);
                //xrPGAccountBalanceMulti.Styles.CellStyle.Font = new Font(value.CellStyle.Font, value.CellStyle.Font.Style);
                xrPGAccountBalanceMulti.Styles.FieldHeaderStyle.Font = new Font(value.FieldHeaderStyle.Font, value.FieldHeaderStyle.Font.Style);
                xrPGAccountBalanceMulti.Styles.HeaderGroupLineStyle.Font = new Font(value.HeaderGroupLineStyle.Font, value.HeaderGroupLineStyle.Font.Style);

                //On 16/04/2021
                float fontsize = value.GrandTotalCellStyle.Font.Size;
                //For Temp
                if (fontsize == 6)
                {
                    fontsize = float.Parse("5.90");
                }

                xrPGAccountBalanceMulti.Styles.GrandTotalCellStyle.Padding = new DevExpress.XtraPrinting.PaddingInfo(1);
                xrPGAccountBalanceMulti.Styles.GrandTotalCellStyle.Font = new Font(value.GrandTotalCellStyle.Font.FontFamily, fontsize, value.GrandTotalCellStyle.Font.Style);

                //On 16/04/2021
                fieldLEDGERGROUP.Appearance.FieldValue.Font = new Font(value.FieldHeaderStyle.Font.FontFamily, value.GrandTotalCellStyle.Font.Size, value.FieldHeaderStyle.Font.Style);


                //22/09/2020, To fix Border color based on settings
                xrPGAccountBalanceMulti.Styles.FieldHeaderStyle.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                xrPGAccountBalanceMulti.Styles.CellStyle.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                xrPGAccountBalanceMulti.Styles.GrandTotalCellStyle.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
            }
        }

        /// <summary>
        /// On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
        /// 
        /// This date is apart from balance date. If we take reports for given period (DATE_FROM and DATE_TO)
        /// When we show closing balance for DATE_TO, we have to check bank ledger closed date for DATE_FROM
        /// </summary>
        public string BankClosedDate { get; set; }

        public bool IsOpeningBalance { get; set; }

        public AccountBalanceMulti()
        {
            InitializeComponent();
        }

        public override void ShowReport()
        {
            base.ShowReport();
        }

        private ResultArgs GetBalance(string balDate, string projectIds, string groupIds)
        {
            //On 28/09/2023, To attach Applicable date Range
            DateTime dFromApplicable = string.IsNullOrEmpty(BankClosedDate) ? this.settinguserProperty.FirstFYDateFrom :
                 UtilityMember.DateSet.ToDate(BankClosedDate, false);
            DateTime dToApplicable = string.IsNullOrEmpty(ReportProperties.DateTo) ? settinguserProperty.LastFYDateTo :
                UtilityMember.DateSet.ToDate(ReportProperties.DateTo, false);

            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchBalance))
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, projectIds);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, groupIds);
                dataManager.Parameters.Add(this.ReportParameters.BALANCE_DATEColumn, balDate);

                //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                if (!string.IsNullOrEmpty(BankClosedDate))
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_CLOSEDColumn, BankClosedDate);
                }

                //On 28/09/2023, To attach Applicable date Range
                dataManager.Parameters.Add(this.ReportParameters.APPLICABLE_FROMColumn, dFromApplicable);
                dataManager.Parameters.Add(this.ReportParameters.APPLICABLE_TOColumn, dToApplicable);

                //On 05/12/2024, To to currency based reports --------------------------------------------------------------------------------------
                if (settingProperty.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                }
                //----------------------------------------------------------------------------------------------------------------------------------

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
                //ReportProperty.Current.EnforceSkipDefaultLedgers(resultArgs, this.ReportParameters.LEDGER_IDColumn.ColumnName);
            }
            return resultArgs;
        }

        public void BindBalance(bool isOpBalance)
        {
            string dateFrom = ReportProperties.DateFrom;
            string dateTo = ReportProperties.DateTo;
            string balDate = "";
            string projectIds = ReportProperties.Project;
            string groupIds = this.GetLiquidityGroupIds();
            DateTime monthYear;

            int month = 0;
            int year = 0;
            string monthName = "";
            string transMode = "";
            double amount = 0;
            int LedgerId = 0;

            if (dateTo == "") { dateTo = ReportProperties.DateAsOn; }
            DateTime date_from = DateTime.Parse(dateFrom);
            DateTime date_to = DateTime.Parse(dateTo);
            DateTime openingMonthYear;

            Dictionary<DateTime, DateTime> dicMonthYear = new Dictionary<DateTime, DateTime>();
            IsOpeningBalance = isOpBalance;
            if (isOpBalance)
            {
                DateTime dateFr = DateTime.Parse(dateFrom);
                monthYear = new DateTime(dateFr.Year, dateFr.Month, 1);
                dicMonthYear[monthYear] = dateFr.AddDays(-1);
                openingMonthYear = monthYear;

                while (true)
                {
                    dateFr = new DateTime(date_from.Year, date_from.Month, 1).AddMonths(1);

                    if (dateFr <= date_to)
                    {
                        monthYear = new DateTime(dateFr.Year, dateFr.Month, 1);
                        dicMonthYear[monthYear] = dateFr.AddDays(-1);
                        date_from = dateFr;
                    }
                    else
                    {
                        //Opening Balance for Date of First Month for Balance Total Column
                        monthYear = new DateTime(dateFr.Year, dateFr.Month, 1);
                        dicMonthYear[monthYear] = dicMonthYear[openingMonthYear];
                        break;
                    }
                }
            }
            else
            {
                DateTime dateFr = DateTime.Parse(dateFrom);

                while (true)
                {
                    dateFr = new DateTime(date_from.Year, date_from.Month, 1).AddMonths(1).AddDays(-1);

                    if (dateFr < date_to)
                    {
                        monthYear = new DateTime(dateFr.Year, dateFr.Month, 1);
                        dicMonthYear[monthYear] = dateFr;
                        date_from = dateFr.AddDays(1);
                    }
                    else
                    {
                        monthYear = new DateTime(dateFr.Year, dateFr.Month, 1);
                        dicMonthYear[monthYear] = date_to;

                        //Closing Balance for Date of Last Month for Balance Total Column
                        date_from = dateFr.AddDays(1);
                        dateFr = new DateTime(date_from.Year, date_from.Month, 1).AddMonths(1).AddDays(-1);
                        monthYear = new DateTime(dateFr.Year, dateFr.Month, 1);
                        dicMonthYear[monthYear] = date_to;
                        break;
                    }
                }
            }

            //Get Schema
            resultArgs = GetBalance(dateFrom, "0", "0");
            DataTable dtBalance = resultArgs.DataSource.Table;

            dtBalance.Columns.Add(reportSetting1.AccountBalance.MONTH_YEARColumn.ColumnName, typeof(DateTime));
            dtBalance.Columns.Add(reportSetting1.AccountBalance.YEARColumn.ColumnName, typeof(int));
            dtBalance.Columns.Add(reportSetting1.AccountBalance.MONTHColumn.ColumnName, typeof(int));
            dtBalance.Columns.Add(reportSetting1.AccountBalance.MONTH_NAMEColumn.ColumnName, typeof(string));
            Int32 MonthRow = 1;
            foreach (KeyValuePair<DateTime, DateTime> dateKey in dicMonthYear)
            {
                monthYear = dateKey.Key;
                balDate = dateKey.Value.ToShortDateString();
                month = monthYear.Month;
                year = monthYear.Year;
                monthName = monthYear.ToString("MMM") + "-" + monthYear.Year;

                //Fill Each Month Balance into 1 Table
                resultArgs = GetBalance(balDate, projectIds, groupIds);
                DataTable dtBalMonth = resultArgs.DataSource.Table;
                //DataTable dtACIBalance = FetchACIBalance(balDate);

                if (dtBalMonth != null)
                {
                    if (dtBalMonth.Rows.Count > 0)
                    {
                        foreach (DataRow drBalMonth in dtBalMonth.Rows)
                        {
                            transMode = drBalMonth[reportSetting1.AccountBalance.TRANS_MODEColumn.ColumnName].ToString();
                            amount = UtilityMember.NumberSet.ToDouble(drBalMonth[reportSetting1.AccountBalance.AMOUNTColumn.ColumnName].ToString());
                            LedgerId = UtilityMember.NumberSet.ToInteger(drBalMonth[reportSetting1.AccountBalance.LEDGER_IDColumn.ColumnName].ToString());
                            if (transMode == TransactionMode.CR.ToString()) { amount = -amount; }

                            //foreach (DataRow dr in dtACIBalance.Rows)
                            //{
                            //    //if (LedgerId.Equals(this.UtilityMember.NumberSet.ToInteger(dr[reportSetting1.AccountBalance.LEDGER_IDColumn.ColumnName].ToString())))
                            //    //{
                            //    //    double ACIBalance = UtilityMember.NumberSet.ToDouble(dr["INTEREST_AMOUNT"].ToString());
                            //    //    amount = amount - ACIBalance;
                            //    //}
                            //}

                            DataRow drBalance = dtBalance.NewRow();
                            drBalance[reportSetting1.AccountBalance.MONTH_YEARColumn.ColumnName] = monthYear;
                            drBalance[reportSetting1.AccountBalance.YEARColumn.ColumnName] = year;
                            drBalance[reportSetting1.AccountBalance.MONTHColumn.ColumnName] = month;
                            //On 01/03/2019, to hide last column (this is not grand coumun total)
                            //drBalance[reportSetting1.AccountBalance.MONTH_NAMEColumn.ColumnName] = monthName;
                            if (xrPGAccountBalanceMulti.OptionsView.ShowRowHeaders && MonthRow == dicMonthYear.Count && IsOpeningBalance)
                            {
                                drBalance[reportSetting1.AccountBalance.MONTH_NAMEColumn.ColumnName] = "Opening (Total)";
                            }
                            else if (xrPGAccountBalanceMulti.OptionsView.ShowRowHeaders && MonthRow == dicMonthYear.Count && IsOpeningBalance == false)
                            {
                                drBalance[reportSetting1.AccountBalance.MONTH_NAMEColumn.ColumnName] = "Closing (Total)";
                            }
                            else
                            {
                                drBalance[reportSetting1.AccountBalance.MONTH_NAMEColumn.ColumnName] = monthName;
                            }
                            drBalance[reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName] = drBalMonth[reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName];
                            drBalance[reportSetting1.AccountBalance.GROUP_CODEColumn.ColumnName] = drBalMonth[reportSetting1.AccountBalance.GROUP_CODEColumn.ColumnName];
                            drBalance[reportSetting1.AccountBalance.LEDGER_GROUPColumn.ColumnName] = drBalMonth[reportSetting1.AccountBalance.LEDGER_GROUPColumn.ColumnName];
                            drBalance[reportSetting1.AccountBalance.LEDGER_IDColumn.ColumnName] = drBalMonth[reportSetting1.AccountBalance.LEDGER_IDColumn.ColumnName];
                            drBalance[reportSetting1.AccountBalance.LEDGER_CODEColumn.ColumnName] = drBalMonth[reportSetting1.AccountBalance.LEDGER_CODEColumn.ColumnName];
                            drBalance[reportSetting1.AccountBalance.LEDGER_NAMEColumn.ColumnName] = drBalMonth[reportSetting1.AccountBalance.LEDGER_NAMEColumn.ColumnName];
                            drBalance[reportSetting1.AccountBalance.AMOUNTColumn.ColumnName] = amount;
                            drBalance[reportSetting1.AccountBalance.TRANS_MODEColumn.ColumnName] = transMode;
                            dtBalance.Rows.Add(drBalance);
                        }
                    }
                    else
                    {
                        DataRow drBalance = dtBalance.NewRow();
                        drBalance[reportSetting1.AccountBalance.MONTH_YEARColumn.ColumnName] = monthYear;
                        drBalance[reportSetting1.AccountBalance.YEARColumn.ColumnName] = year;
                        drBalance[reportSetting1.AccountBalance.MONTHColumn.ColumnName] = month;
                        drBalance[reportSetting1.AccountBalance.MONTH_NAMEColumn.ColumnName] = monthName;
                        drBalance[reportSetting1.AccountBalance.AMOUNTColumn.ColumnName] = 0;
                        dtBalance.Rows.Add(drBalance);
                    }
                }
                MonthRow++;
            }

            dtBalance.AcceptChanges();
            DataView dvBalance = dtBalance.DefaultView;

            if (dvBalance != null)
            {
                dvBalance.Table.TableName = "AccountBalance";
                xrPGAccountBalanceMulti.DataSource = dvBalance;
                xrPGAccountBalanceMulti.DataMember = dvBalance.Table.TableName;
            }

            SetReportSetting();
        }

        private DataTable GetGrantTotalSource()
        {
            DataTable dtGrantTotal = new DataTable();
            dtGrantTotal.Columns.Add(reportSetting1.AccountBalance.LEDGER_NAMEColumn.ColumnName, typeof(string));
            dtGrantTotal.Columns.Add(reportSetting1.AccountBalance.MONTHColumn.ColumnName, typeof(int));
            dtGrantTotal.Columns.Add(reportSetting1.AccountBalance.AMOUNTColumn.ColumnName, typeof(double));

            object oTotVal = null;
            double totVal = 0;
            int row = xrPGAccountBalanceMulti.RowCount - 1;

            for (int col = 0; col < xrPGAccountBalanceMulti.ColumnCount; col++)
            {
                oTotVal = xrPGAccountBalanceMulti.GetCellValue(col, row);
                totVal = this.UtilityMember.NumberSet.ToDouble(oTotVal.ToString());
                DataRow drGrantTotal = dtGrantTotal.NewRow();
                drGrantTotal[reportSetting1.AccountBalance.LEDGER_NAMEColumn.ColumnName] = "Grand Total";
                drGrantTotal[reportSetting1.AccountBalance.MONTHColumn.ColumnName] = (col + 1);
                drGrantTotal[reportSetting1.AccountBalance.AMOUNTColumn.ColumnName] = totVal;
                dtGrantTotal.Rows.Add(drGrantTotal);
            }

            dtGrantTotal.AcceptChanges();
            return dtGrantTotal;
        }

        private void xrPGAccountBalanceMulti_CustomFieldSort(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs e)
        {
            try
            {
                if (e.Field.Name == fieldMONTHNAME.Name)
                {
                    if (e.Value1 != null && e.Value2 != null)
                    {
                        DateTime dt1 = DateTime.Parse(e.GetListSourceColumnValue(e.ListSourceRowIndex1, reportSetting1.AccountBalance.MONTH_YEARColumn.ColumnName).ToString());
                        DateTime dt2 = DateTime.Parse(e.GetListSourceColumnValue(e.ListSourceRowIndex2, reportSetting1.AccountBalance.MONTH_YEARColumn.ColumnName).ToString());
                        e.Result = Comparer.Default.Compare(dt1, dt2);
                        e.Handled = true;
                    }
                }
                else if (e.Field.Name == fieldGROUPCODE.Name || e.Field.Name == fieldLEDGERCODE.Name || e.Field.Name == fieldLEDGERNAME.Name)
                {   //On 10/01/2025 - To have proper ledger name sort order if show detials cash, bank and fd ledgers
                    //string ledgergroup1  = (e.GetListSourceColumnValue(e.ListSourceRowIndex1, reportSetting1.AccountBalance.LEDGER_GROUPColumn.ColumnName).ToString());
                    //string ledgergroup2 = (e.GetListSourceColumnValue(e.ListSourceRowIndex2, reportSetting1.AccountBalance.LEDGER_GROUPColumn.ColumnName).ToString());
                    //e.Result = Comparer.Default.Compare(ledgergroup1, ledgergroup2);
                    //e.Handled = true;


                    // Temp //
                    //On 10/01/2025 - To have proper ledger name sort order if show detials cash, bank and fd ledgers
                    string ledgergroup1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, reportSetting1.AccountBalance.LEDGER_GROUPColumn.ColumnName).ToString();
                    string ledgergroup2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, reportSetting1.AccountBalance.LEDGER_GROUPColumn.ColumnName).ToString();
                    int groupCompare = Comparer.Default.Compare(ledgergroup1, ledgergroup2);
                    if (this.ReportProperties.ShowDetailedBalance != 1)
                    {
                        e.Result = groupCompare;
                    }
                    else
                    {
                        if (groupCompare == 0)
                        {
                            string ledgername1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, reportSetting1.AccountBalance.LEDGER_NAMEColumn.ColumnName).ToString();
                            string ledgername2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, reportSetting1.AccountBalance.LEDGER_NAMEColumn.ColumnName).ToString();
                            e.Result = Comparer.Default.Compare(ledgername1, ledgername2);
                        }
                        else
                        {
                            e.Result = groupCompare;
                        }
                    }
                    e.Handled = true;

                }
            }
            catch (Exception err)
            {
                string s = err.Message;
            }
        }

        private void xrPGAccountBalanceMulti_FieldValueDisplayText(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
            {
                e.DisplayText = "Total";
            }

        }

        private void xrPGAccountBalanceMulti_PrintFieldValue(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs e)
        {
            if (e.Field != null)
            {
                if (e.Field.Name == fieldLEDGERCODE.Name || e.Field.Name == fieldLEDGERNAME.Name
                    || e.Field.Name == fieldGROUPCODE.Name || e.Field.Name == fieldLEDGERGROUP.Name)
                {
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
                    if (e.Field.Name == fieldLEDGERNAME.Name || e.Field.Name == fieldLEDGERGROUP.Name)
                    {
                        e.Brick.Text = e.Brick.Text.Trim();
                    }
                }
                else if (e.Field.Name == fieldMONTHNAME.Name)
                {
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
                    e.Appearance.BorderColor = xrPGAccountBalanceMulti.Styles.FieldHeaderStyle.BorderColor;
                    e.Appearance.BackColor = xrPGAccountBalanceMulti.Styles.FieldHeaderStyle.BackColor;
                    e.Appearance.Font = xrPGAccountBalanceMulti.Styles.FieldHeaderStyle.Font;
                }

                if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                {
                    // e.Appearance.ForeColor = xrPGAccountBalanceMulti.Styles.HeaderGroupLineStyle.ForeColor;
                    e.Appearance.Font = xrPGAccountBalanceMulti.Styles.HeaderGroupLineStyle.Font;
                }

                if (e.Field.Name == fieldMONTHNAME.Name)
                {
                    if (xrPGAccountBalanceMulti.OptionsView.ShowRowHeaders == false)
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
        }

        private void xrPGAccountBalanceMulti_PrintCell(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportCellEventArgs e)
        {
            if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.TotalCell)
            {
                //  e.Appearance.ForeColor = xrPGAccountBalanceMulti.Styles.HeaderGroupLineStyle.ForeColor;
                e.Appearance.Font = xrPGAccountBalanceMulti.Styles.HeaderGroupLineStyle.Font;
            }

            if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.Cell)
            {
                if (e.Brick.TextValue == null || this.UtilityMember.NumberSet.ToDouble(e.Brick.TextValue.ToString()) == 0) { e.Brick.Text = ""; }

                if (e.ColumnFieldIndex == xrPGAccountBalanceMulti.ColumnCount - 1)
                {
                    e.Brick.Text = "";
                }
            }
        }

        public void SetReportSetting()
        {
            int extendWidth = 0;

            try { fieldGROUPCODE.Visible = true; }
            catch { }
            try { fieldLEDGERGROUP.Visible = true; }
            catch { }
            try { fieldLEDGERCODE.Visible = true; }
            catch { }
            try { fieldLEDGERNAME.Visible = true; }
            catch { }

            fieldGROUPCODE.AreaIndex = 0;
            fieldLEDGERGROUP.AreaIndex = 1;
            fieldLEDGERCODE.AreaIndex = 2;
            fieldLEDGERNAME.AreaIndex = 3;

            bool isGroupVisible = (ReportProperties.ShowByLedgerGroup == 1);
            bool isLedgerVisible = (ReportProperties.ShowByLedger == 1);
            if (isGroupVisible == false && isLedgerVisible == false) { isLedgerVisible = true; }
            bool isShowBankDetail = (ReportProperties.ShowDetailedBalance == 1);

            bool isGroupCodeVisible = (isGroupVisible && (ReportProperties.ShowGroupCode == 1));
            bool isLedgerCodeVisible = (isLedgerVisible && (ReportProperties.ShowLedgerCode == 1));

            if (xrPGAccountBalanceMulti.OptionsView.ShowRowHeaders && IsOpeningBalance)
            {
                fieldLEDGERNAME.Caption = fieldLEDGERGROUP.Caption = "Opening Balance";
            }
            else if (xrPGAccountBalanceMulti.OptionsView.ShowRowHeaders && IsOpeningBalance)
            {
                fieldLEDGERNAME.Caption = fieldLEDGERGROUP.Caption = "Closing Balance";
            }

            if (isShowBankDetail)
            {
                isShowBankDetail = isLedgerVisible;
            }

            bool isHorizontalLine = (ReportProperties.ShowHorizontalLine == 1);
            bool isVerticalLine = (ReportProperties.ShowVerticalLine == 1);

            //Include / Exclude Code
            try { fieldGROUPCODE.Visible = (isGroupCodeVisible || (isLedgerCodeVisible && !isShowBankDetail)); }
            catch { }
            try { fieldLEDGERGROUP.Visible = (isGroupVisible || (isLedgerVisible && !isShowBankDetail)); }
            catch { }
            try { fieldLEDGERCODE.Visible = (isShowBankDetail && isLedgerCodeVisible); }
            catch { }
            try { fieldLEDGERNAME.Visible = isShowBankDetail; }
            catch { }

            if (isGroupVisible) { extendWidth = fieldLEDGERGROUP.Width; }
            if (isLedgerVisible) { extendWidth += fieldLEDGERNAME.Width; }
            if (isGroupCodeVisible) { extendWidth += fieldGROUPCODE.Width; }
            if (isLedgerCodeVisible) { extendWidth += fieldLEDGERCODE.Width; }

            if (fieldGROUPCODE.Visible) { extendWidth -= fieldGROUPCODE.Width; }
            if (fieldLEDGERGROUP.Visible) { extendWidth -= fieldLEDGERGROUP.Width; }
            if (fieldLEDGERCODE.Visible) { extendWidth -= fieldLEDGERCODE.Width; }
            if (fieldLEDGERNAME.Visible) { extendWidth -= fieldLEDGERNAME.Width; }

            if (fieldLEDGERGROUP.Visible)
            {
                fieldLEDGERGROUP.Width = fieldLEDGERGROUP.Width + extendWidth;
            }
            else
            {
                fieldLEDGERNAME.Width = fieldLEDGERNAME.Width + extendWidth;
            }

            //Hide Column Total
            xrPGAccountBalanceMulti.OptionsView.ShowColumnGrandTotalHeader = false;
            xrPGAccountBalanceMulti.OptionsView.ShowColumnGrandTotals = false;

            //Grid Lines
            if (isHorizontalLine)
            {
                xrPGAccountBalanceMulti.OptionsPrint.PrintHorzLines = DevExpress.Utils.DefaultBoolean.True;
            }
            else
            {
                xrPGAccountBalanceMulti.OptionsPrint.PrintHorzLines = DevExpress.Utils.DefaultBoolean.False;
            }

            if (isVerticalLine)
            {
                xrPGAccountBalanceMulti.OptionsPrint.PrintVertLines = DevExpress.Utils.DefaultBoolean.True;
            }
            else
            {
                xrPGAccountBalanceMulti.OptionsPrint.PrintVertLines = DevExpress.Utils.DefaultBoolean.False;
            }

            //On 10/01/2025 - To have proper ledger name sort order if show detials cash, bank and fd ledgers
            if ((fieldGROUPCODE.Visible || fieldLEDGERCODE.Visible || fieldLEDGERNAME.Visible))
            {
                fieldGROUPCODE.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom;
                fieldLEDGERCODE.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom;
                fieldLEDGERNAME.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom;
            }
        }

        private DataTable FetchACIBalance(string deDateFrom)
        {
            string FetchACIBalance = this.GetReportSQL(SQL.ReportSQLCommand.Report.FetchACIBalance);

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.BALANCE_DATEColumn, deDateFrom);
                //dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, deDateFrom);
                //dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, FetchACIBalance);
            }
            return resultArgs.DataSource.Table;
        }

        private void xrPGAccountBalanceMulti_CustomColumnWidth(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotCustomColumnWidthEventArgs e)
        {
            //On 26/09/2018, to increase row total column width alone
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Value)
            {
                if (e.ColumnIndex == xrPGAccountBalanceMulti.ColumnCount - 1)
                {
                    e.ColumnWidth = fieldMONTHNAME.Width + 1; //15;
                }
            }
        }

        private void xrPGAccountBalanceMulti_CustomRowHeight(object sender, PivotCustomRowHeightEventArgs e)
        {
            int defaultrowheight = e.RowHeight;//Default height
            try
            {
                if (e.Field != null)
                {
                    if (e.Field.Name == fieldLEDGERNAME.Name || e.Field.Name == fieldLEDGERGROUP.Name)
                    {
                        //string value = e.GetFieldValue(e.Field, e.RowIndex).ToString().Trim();
                        //SizeF size = this.GetPrintingSystem().Graph.MeasureString(value);
                        //if (size.Width > fieldLEDGERNAME.Width)
                        //{
                        //    float newVariable = (size.Width / fieldLEDGERNAME.Width);
                        //    double decPartnewVariable = newVariable - Math.Truncate(newVariable);
                        //    if (decPartnewVariable > 0.5 && (NoOfMonths <= 8))
                        //    {
                        //        newVariable++;
                        //    }

                        //    int noofrows = (int)Math.Ceiling((decimal)newVariable);
                        //    if (noofrows > 1)
                        //    {
                        //        if (NoOfMonths > 8)
                        //        {
                        //            e.RowHeight = ((defaultrowheight - 2) * (noofrows > 2 ? noofrows - 2 : noofrows));
                        //        }
                        //        else
                        //        {
                        //            e.RowHeight = ((defaultrowheight - 5) * (noofrows));
                        //        }
                        //    }

                        e.RowHeight = defaultrowheight;
                        string ledgergroup = string.Empty;
                        string ledgername = e.GetFieldValue(e.Field, e.RowIndex).ToString().Trim();
                        SizeF size = gr.MeasureString(ledgername, xrPGAccountBalanceMulti.Styles.CellStyle.Font, fieldLEDGERNAME.Width - 2);
                        Int32 RowHeightLedgerName = Convert.ToInt32(size.Height + 0.5);
                        //Int32 RowHeightLedgerName = GetRowHeight(ledgername, fieldLEDGERNAME.Width, e.RowHeight);
                        Int32 RowHeightLedgerGroup = 0;
                        if (fieldLEDGERGROUP.Visible)
                        {
                            ledgergroup = e.GetFieldValue(fieldLEDGERGROUP, e.RowIndex).ToString().Trim();
                            size = gr.MeasureString(ledgergroup, xrPGAccountBalanceMulti.Styles.CellStyle.Font, fieldLEDGERGROUP.Width - 2);
                            //RowHeightLedgerGroup = GetRowHeight(ledgergroup, fieldLEDGERGROUP.Width, e.RowHeight);
                            RowHeightLedgerGroup = Convert.ToInt32(size.Height + 0.5);
                        }
                        e.RowHeight = Math.Max(RowHeightLedgerName, RowHeightLedgerGroup);
                    }
                }
            }
            catch (Exception err)
            {
                e.RowHeight = defaultrowheight;//Default height
                MessageRender.ShowMessage("Not able to set row right " + err.Message);
            }
        }

        private void xrPGAccountBalanceMulti_CustomFieldValueCells(object sender, PivotCustomFieldValueCellsEventArgs e)
        {
            // On 16/12/2024 for temp purpose - remove default cash and fd ledgers
            try
            {
                if (this.settingProperty.AllowMultiCurrency == 1)
                {
                    for (int i = e.GetCellCount(false) - 1; i >= 0; i--)
                    {
                        DevExpress.XtraReports.UI.PivotGrid.FieldValueCell cell = e.GetCell(false, i);
                        if (cell == null) continue;

                        if (cell.Value != null)
                        {
                            if ((object.Equals(cell.Value, FixedLedgerGroup.Cash.ToString()) || object.Equals(cell.Value, "Fixed Deposit")) &&
                                cell.ValueType != DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                            {
                                e.Remove(cell);
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                //MessageRender.ShowMessage(err.Message);
            }
        }
    }
}
