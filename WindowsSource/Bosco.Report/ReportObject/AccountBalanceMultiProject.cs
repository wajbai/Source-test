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
using Bosco.Utility.ConfigSetting;
using DevExpress.XtraReports.UI.PivotGrid;

namespace Bosco.Report.ReportObject
{
    public partial class AccountBalanceMultiProject : Report.Base.ReportBase
    {
        ResultArgs resultArgs = null;
        SettingProperty settingProperty = new SettingProperty();
        public double PeriodBalanceAmount { get; set; }
        int ProjectNumber = 1;

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
                fieldPROJECTID.Width = value;
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

        public XRPivotGridStyles ApplyParentReportStyle
        {
            set
            {
                xrPGAccountBalanceMulti.Styles.CellStyle.Font = new Font(value.CellStyle.Font, value.CellStyle.Font.Style);
                xrPGAccountBalanceMulti.Styles.FieldHeaderStyle.Font = new Font(value.FieldHeaderStyle.Font, value.FieldHeaderStyle.Font.Style);
                //xrPGAccountBalanceMulti.Styles.CellStyle.Font = new Font(value.CellStyle.Font, value.CellStyle.Font.Style);
                xrPGAccountBalanceMulti.Styles.FieldHeaderStyle.Font = new Font(value.FieldHeaderStyle.Font, value.FieldHeaderStyle.Font.Style);
                xrPGAccountBalanceMulti.Styles.HeaderGroupLineStyle.Font = new Font(value.HeaderGroupLineStyle.Font, value.HeaderGroupLineStyle.Font.Style);
                xrPGAccountBalanceMulti.Styles.GrandTotalCellStyle.Font = new Font(value.GrandTotalCellStyle.Font, value.GrandTotalCellStyle.Font.Style);


                //22/09/2020, To fix Border color based on settings
                xrPGAccountBalanceMulti.Styles.FieldHeaderStyle.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                xrPGAccountBalanceMulti.Styles.CellStyle.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                xrPGAccountBalanceMulti.Styles.GrandTotalCellStyle.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
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

        /// <summary>
        /// On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
        /// 
        /// This date is apart from balance date. If we take reports for given period (DATE_FROM and DATE_TO)
        /// When we show closing balance for DATE_TO, we have to check bank ledger closed date for DATE_FROM
        /// </summary>
        public string BankClosedDate { get; set; }

        public bool IsOpeningBalance { get; set; }

        public AccountBalanceMultiProject()
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
            DateTime dFromApplicable = string.IsNullOrEmpty(BankClosedDate) ? this.settingProperty.FirstFYDateFrom :
                UtilityMember.DateSet.ToDate(BankClosedDate, false);
            DateTime dToApplicable = string.IsNullOrEmpty(ReportProperties.DateTo) ? settingProperty.LastFYDateTo :
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

                //On 06/12/2024, To to currency based reports --------------------------------------------------------------------------------------
                if (settingProperty.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                }
                //----------------------------------------------------------------------------------------------------------------------------------

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
                ReportProperty.Current.EnforceSkipDefaultLedgers(resultArgs, this.ReportParameters.LEDGER_IDColumn.ColumnName);
            }
            return resultArgs;
        }

        public void BindBalance(bool isOpBalance)
        {
            ProjectNumber = 1;
            string dateFrom = ReportProperties.DateFrom;
            string dateTo = ReportProperties.DateTo;
            string balDate = "";
            string projectIds = ReportProperties.Project;
            string groupIds = this.GetLiquidityGroupIds();
            IsOpeningBalance = isOpBalance;

            string transMode = "";
            double amount = 0;
            int LedgerId = 0;

            if (dateTo == "") { dateTo = ReportProperties.DateAsOn; }
            DateTime date_from = DateTime.Parse(dateFrom);
            DateTime date_to = DateTime.Parse(dateTo);

            if (isOpBalance)
            {
                //For Opening Balance, Finding Balance Date
                DateTime dateBalance = DateTime.Parse(dateFrom).AddDays(-1);
                balDate = dateBalance.ToShortDateString();
            }
            else //Closing Date
            {
                balDate = dateTo;
            }

            //Get Schema
            resultArgs = GetBalance(dateFrom, "0", "0");
            DataTable dtBalance = resultArgs.DataSource.Table;

            dtBalance.Columns.Add(reportSetting1.AccountBalance.PROJECT_IDColumn.ColumnName, typeof(int));
            dtBalance.Columns.Add(reportSetting1.AccountBalance.PROJECT_NAMEColumn.ColumnName, typeof(string));

            Int32 ProjectRow = 1;
            string[] ProjectsList = projectIds.Split(',');
            foreach (string projectid in ProjectsList)
            {
                //Fill Each Project Balance into 1 Table
                resultArgs = GetBalance(balDate, projectid, groupIds);
                DataTable dtBalProject = resultArgs.DataSource.Table;

                if (dtBalProject != null)
                {
                    if (dtBalProject.Rows.Count > 0)
                    {
                        foreach (DataRow drBalMonth in dtBalProject.Rows)
                        {
                            transMode = drBalMonth[reportSetting1.AccountBalance.TRANS_MODEColumn.ColumnName].ToString();
                            amount = UtilityMember.NumberSet.ToDouble(drBalMonth[reportSetting1.AccountBalance.AMOUNTColumn.ColumnName].ToString());
                            LedgerId = UtilityMember.NumberSet.ToInteger(drBalMonth[reportSetting1.AccountBalance.LEDGER_IDColumn.ColumnName].ToString());
                            if (transMode == TransactionMode.CR.ToString()) { amount = -amount; }

                            DataRow drBalance = dtBalance.NewRow();
                            drBalance[reportSetting1.AccountBalance.PROJECT_IDColumn.ColumnName] = projectid;
                            drBalance[reportSetting1.AccountBalance.PROJECT_NAMEColumn.ColumnName] = projectid;
                            drBalance[reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName] = drBalMonth[reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName];
                            drBalance[reportSetting1.AccountBalance.GROUP_CODEColumn.ColumnName] = drBalMonth[reportSetting1.AccountBalance.GROUP_CODEColumn.ColumnName];
                            drBalance[reportSetting1.AccountBalance.LEDGER_GROUPColumn.ColumnName] = drBalMonth[reportSetting1.AccountBalance.LEDGER_GROUPColumn.ColumnName];
                            drBalance[reportSetting1.AccountBalance.LEDGER_IDColumn.ColumnName] = drBalMonth[reportSetting1.AccountBalance.LEDGER_IDColumn.ColumnName];
                            drBalance[reportSetting1.AccountBalance.LEDGER_CODEColumn.ColumnName] = drBalMonth[reportSetting1.AccountBalance.LEDGER_CODEColumn.ColumnName];
                            drBalance[reportSetting1.AccountBalance.LEDGER_NAMEColumn.ColumnName] = drBalMonth[reportSetting1.AccountBalance.LEDGER_NAMEColumn.ColumnName];
                            drBalance[reportSetting1.AccountBalance.AMOUNTColumn.ColumnName] = amount;
                            drBalance[reportSetting1.AccountBalance.TRANS_MODEColumn.ColumnName] = transMode;

                            //On 01/03/2019, to hide last column (this is not grand coumun total)
                            //drBalance[reportSetting1.AccountBalance.AC_YEAR_NAMEColumn.ColumnName] = FinanceFromDate.Year.ToString() + "-" + FinanceToDate.Year.ToString();
                            if (xrPGAccountBalanceMulti.OptionsView.ShowRowHeaders && ProjectRow == ProjectsList.Length && IsOpeningBalance)
                            {
                                drBalance[reportSetting1.AccountBalance.PROJECT_NAMEColumn.ColumnName] = "Total";
                            }
                            else
                            {
                                drBalance[reportSetting1.AccountBalance.PROJECT_NAMEColumn.ColumnName] = projectid;
                            }

                            dtBalance.Rows.Add(drBalance);
                        }
                    }
                    else
                    {
                        DataRow drBalance = dtBalance.NewRow();
                        drBalance[reportSetting1.AccountBalance.PROJECT_IDColumn.ColumnName] = projectid;

                        //On 01/03/2019, to hide last column (this is not grand coumun total)
                        //drBalance[reportSetting1.AccountBalance.AC_YEAR_NAMEColumn.ColumnName] = FinanceFromDate.Year.ToString() + "-" + FinanceToDate.Year.ToString();
                        if (xrPGAccountBalanceMulti.OptionsView.ShowRowHeaders && ProjectRow == ProjectsList.Length && IsOpeningBalance)
                        {
                            drBalance[reportSetting1.AccountBalance.PROJECT_NAMEColumn.ColumnName] = "Total";
                        }
                        else
                        {
                            drBalance[reportSetting1.AccountBalance.PROJECT_NAMEColumn.ColumnName] = projectid;
                        }
                        dtBalance.Rows.Add(drBalance);
                    }
                }
                ProjectRow++;
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
            dtGrantTotal.Columns.Add(reportSetting1.AccountBalance.YEARColumn.ColumnName, typeof(int));
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
                drGrantTotal[reportSetting1.AccountBalance.YEARColumn.ColumnName] = (col + 1);
                drGrantTotal[reportSetting1.AccountBalance.AMOUNTColumn.ColumnName] = totVal;
                dtGrantTotal.Rows.Add(drGrantTotal);
            }

            dtGrantTotal.AcceptChanges();
            return dtGrantTotal;
        }

        private void xrPGAccountBalanceMulti_CustomFieldSort(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.Name == fieldPROJECTID.Name)
            {
                if (e.Value1 != null && e.Value2 != null)
                {
                    Int32 projectid1 = UtilityMember.NumberSet.ToInteger(e.GetListSourceColumnValue(e.ListSourceRowIndex1, reportSetting1.AccountBalance.PROJECT_IDColumn.ColumnName).ToString());
                    Int32 projectid2 = UtilityMember.NumberSet.ToInteger(e.GetListSourceColumnValue(e.ListSourceRowIndex2, reportSetting1.AccountBalance.PROJECT_IDColumn.ColumnName).ToString());

                    e.Result = Comparer.Default.Compare(projectid1, projectid2);
                    e.Handled = true;
                }
            }
            else if (e.Field.Name == fieldGROUPCODE.Name || e.Field.Name == fieldLEDGERCODE.Name || e.Field.Name == fieldLEDGERNAME.Name)
            {   //On 10/01/2025 - To have proper ledger name sort order if show detials cash, bank and fd ledgers
                //string ledgergroup1 = (e.GetListSourceColumnValue(e.ListSourceRowIndex1, reportSetting1.AccountBalance.LEDGER_GROUPColumn.ColumnName).ToString());
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
                else if (e.Field.Name == fieldPROJECTID.Name)
                {
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
                    e.Appearance.BorderColor = xrPGAccountBalanceMulti.Styles.FieldHeaderStyle.BorderColor;
                    e.Appearance.BackColor = xrPGAccountBalanceMulti.Styles.FieldHeaderStyle.BackColor;
                    e.Appearance.Font = xrPGAccountBalanceMulti.Styles.FieldHeaderStyle.Font;

                    e.Brick.Text = "P" + ProjectNumber.ToString();
                    Int32 projectid = UtilityMember.NumberSet.ToInteger(e.Brick.Text);
                    ProjectNumber++;
                }

                if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                {
                    // e.Appearance.ForeColor = xrPGAccountBalanceMulti.Styles.HeaderGroupLineStyle.ForeColor;
                    //e.Appearance.Font = xrPGAccountBalanceMulti.Styles.HeaderGroupLineStyle.Font;
                }

                if (e.Field.Name == fieldPROJECTID.Name)
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
                //e.Appearance.Font = xrPGAccountBalanceMulti.Styles.HeaderGroupLineStyle.Font;
            }

            if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.Cell)
            {
                if (e.Brick.TextValue == null || this.UtilityMember.NumberSet.ToDouble(e.Brick.TextValue.ToString()) == 0) { e.Brick.Text = ""; }

                //if (e.ColumnFieldIndex == xrPGAccountBalanceMulti.ColumnCount - 1)
                //{
                //    e.Brick.Text = "";
                //}
            }
        }

        private void SetReportSetting()
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
            xrPGAccountBalanceMulti.OptionsView.ShowColumnGrandTotalHeader = true;
            xrPGAccountBalanceMulti.OptionsView.ShowColumnGrandTotals = true;

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
    }
}
