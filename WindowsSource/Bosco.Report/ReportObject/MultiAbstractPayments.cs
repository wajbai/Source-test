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
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using System.Linq;
using DevExpress.XtraPrinting;
using DevExpress.XtraPivotGrid;

namespace Bosco.Report.ReportObject
{
    public partial class MultiAbstractPayments : Bosco.Report.Base.ReportHeaderBase
    {
        public int NoOfMonths = 1;
        public bool ReceiptsAndPayments = false;
        Graphics gr = Graphics.FromHwnd(IntPtr.Zero);
        public MultiAbstractPayments()
        {
            InitializeComponent();
            this.SetTitleWidth(xrPGMultiAbstractPayment.WidthF);
        }


        public void HideReportHeaderFooter()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
        }

        public bool IsLandscapeReport
        {
            get
            {
                return this.Landscape;
            }
        }

        #region ShowReport

        public override void ShowReport()
        {
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                || String.IsNullOrEmpty(this.ReportProperties.Project))
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
                        BindMultiAbstractPaymentSource();
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
                    BindMultiAbstractPaymentSource();
                    SplashScreenManager.CloseForm();
                    base.ShowReport();

                }
            }
            // base.ShowReport();
        }

        #endregion

        public void BindMultiAbstractPaymentSource()
        {
            //  this.ReportTitle = ReportProperty.Current.ReportTitle;
            // this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
            setHeaderTitleAlignment();
            // this.ReportPeriod = "For the Period: " + this.ReportProperties.DateFrom + " - " + this.ReportProperties.DateTo;
            SetReportTitle();
            ResultArgs resultArgs = GetReportSource();
            DataView dvPayment = resultArgs.DataSource.TableView;

            //On 13/08/2018, to show TDS on FD interest for accumulate interest -------------------------------
            /*ResultArgs resultArgsTDSOnFD = GetJournalTDSonFDInterestAmount();
            if (resultArgsTDSOnFD != null && resultArgsTDSOnFD.Success)
            {
                //DataTable dtTDSonFDInterest = resultArgsTDSOnFD.DataSource.Table;
                //foreach (DataRow dr in dtTDSonFDInterest.Rows)
                //{
                //    DateTime dtVoucherDate = ReportProperty.Current.DateSet.ToDate(dr["VOUCHER_DATE"].ToString(), false);
                //    int LedgerId = ReportProperty.Current.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                //    decimal Amount = ReportProperty.Current.NumberSet.ToDecimal(dr["PAYMENTAMT"].ToString());
                //    foreach (DataRow drReceipt in dvPayment.Table.Rows)
                //    {
                //        if (LedgerId.Equals(this.UtilityMember.NumberSet.ToInteger(drReceipt["LEDGER_ID"].ToString())))
                //        {
                //            if (dtVoucherDate.Month.Equals(ReportProperty.Current.NumberSet.ToInteger(drReceipt["MONTH"].ToString())))
                //            {
                //                decimal AmountPeriod = ReportProperty.Current.NumberSet.ToDecimal(drReceipt["AMOUNT"].ToString());
                //                drReceipt.SetField("AMOUNT", Amount + AmountPeriod);
                //                break;
                //            }
                //        }
                //    }
                //}
            }*/
            //-------------------------------------------------------------------------------------------------------

            if (dvPayment != null)
            {
                dvPayment.Table.TableName = "MultiAbstract";
                xrPGMultiAbstractPayment.DataSource = dvPayment;
                xrPGMultiAbstractPayment.DataMember = dvPayment.Table.TableName;

            }

            AccountBalanceMulti accountBalanceMulti = xrSubBalanceMulti.ReportSource as AccountBalanceMulti;
            SetReportSetting(dvPayment, accountBalanceMulti);
            accountBalanceMulti.NoOfMonths = NoOfMonths;
            accountBalanceMulti.BankClosedDate = this.ReportProperties.DateFrom; //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
            accountBalanceMulti.BindBalance(false);
        }

        private void BindGrandTotal(DataTable dtGrantTotal)
        {
            AccountBalanceMulti accountBalanceMulti = xrSubBalanceMulti.ReportSource as AccountBalanceMulti;
            DataTable dtGrantTotalBalance = accountBalanceMulti.GrantTotalBalance;
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
            dtGrantTotal.TableName = "MultiAbstract";
            xrPGGrandTotal.DataSource = dtGrantTotal;
            xrPGGrandTotal.DataMember = dtGrantTotal.TableName;
        }

        //private DataTable RemoveEmptyRow(DataTable dt)
        //{
        //    string LedgerCode = string.Empty;
        //    DataTable dtPayment = dt;
        //    DataView dvReport = new DataView(dtPayment);
        //    dvReport.Sort = "LEDGER_NAME  DESC";
        //    DataTable dtSorted = dvReport.ToTable();

        //    DataTable dtLedgerAmount = dtSorted.AsEnumerable().GroupBy(r => r.Field<String>(reportSetting1.MultiAbstract.LEDGER_CODEColumn.ColumnName)).Select(g => g.First()).CopyToDataTable();
        //    DataTable dtFilter = new DataTable();
        //    foreach (DataRow dr in dtLedgerAmount.Rows)
        //    {
        //        dvReport.RowFilter = String.Format("{0}='{1}'", reportSetting1.MultiAbstract.LEDGER_CODEColumn.ColumnName, dr[reportSetting1.MultiAbstract.LEDGER_CODEColumn.ColumnName].ToString());
        //        dtFilter = dvReport.ToTable();

        //        var IsEmptyRow = (from report in dtFilter.AsEnumerable()
        //                          where ((report.Field<Decimal?>(reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName) > 0))
        //                          select report);

        //        if (IsEmptyRow.Count() <= 0)
        //        {
        //            LedgerCode += dr[reportSetting1.MultiAbstract.LEDGER_CODEColumn.ColumnName].ToString() + ',';
        //        }
        //    }
        //    dvReport.RowFilter = String.Format("{0} NOT IN({1})", reportSetting1.MultiAbstract.LEDGER_CODEColumn.ColumnName, LedgerCode.TrimEnd(','));
        //    if (dvReport != null)
        //        dtPayment = dvReport.ToTable();
        //    return dtPayment;
        //}

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlMultiAbstractPayments = this.GetReportSQL(SQL.ReportSQLCommand.Report.MultiAbstract);
            string liquidityGroupIds = this.GetLiquidityGroupIds();

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, TransType.PY.ToString());
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, liquidityGroupIds);
                dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, TransMode.DR.ToString());
                if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, "0");
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, sqlMultiAbstractPayments);
            }

            return resultArgs;
        }

        private void SortByLedgerorGroup()
        {

            //if (this.ReportProperties.SortByGroup == 0)
            //{
            //    DetailReport1.SortingSummary.Enabled = true;
            //    grpLedgerGroup.SortingSummary.FieldName = "GROUP_CODE";
            //    grpLedgerGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
            //    grpLedgerGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //}
            //else
            //{
            //    grpLedgerGroup.SortingSummary.Enabled = true;
            //    grpLedgerGroup.SortingSummary.FieldName = "LEDGER_GROUP";
            //    grpLedgerGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
            //    grpLedgerGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //}

            //if (this.ReportProperties.SortByLedger == 0)
            //{
            //    grpLedger.SortingSummary.Enabled = true;
            //    grpLedger.SortingSummary.FieldName = "LEDGER_CODE";
            //    grpLedger.SortingSummary.Function = SortingSummaryFunction.Avg;
            //    grpLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //}
            //else
            //{
            //    grpLedger.SortingSummary.Enabled = true;
            //    grpLedger.SortingSummary.FieldName = "LEDGER_NAME";
            //    grpLedger.SortingSummary.Function = SortingSummaryFunction.Avg;
            //    grpLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //}
        }

        private void xrPGMultiAbstractPayment_CustomFieldSort(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.Name == fieldMONTHNAME.Name)
            {
                if (e.Value1 != null && e.Value2 != null)
                {
                    DateTime dt1 = DateTime.Parse(e.GetListSourceColumnValue(e.ListSourceRowIndex1, reportSetting1.MultiAbstract.MONTH_YEARColumn.ColumnName).ToString());
                    DateTime dt2 = DateTime.Parse(e.GetListSourceColumnValue(e.ListSourceRowIndex2, reportSetting1.MultiAbstract.MONTH_YEARColumn.ColumnName).ToString());
                    e.Result = Comparer.Default.Compare(dt1, dt2);
                    e.Handled = true;
                }
            }
        }

        private void xrPGMultiAbstractPayment_FieldValueDisplayText(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
            {
                e.DisplayText = "Total Payments";
            }
            else if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
            {
                e.DisplayText = "Total";
            }
        }

        private void xrPGMultiAbstractPayment_PrintFieldValue(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs e)
        {
            if (e.Field != null)
            {
                if (e.Field.Name == fieldLEDGERCODE.Name || e.Field.Name == fieldLEDGERNAME.Name
                    || e.Field.Name == fieldGROUPCODE.Name || e.Field.Name == fieldLEDGERGROUP.Name)
                {
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;

                   
                    //e.Field.RowValueLineCount = 2;
                    //DevExpress.XtraPrinting.TextBrick textBrick = e.Brick as DevExpress.XtraPrinting.TextBrick;
                    //textBrick.Size = new SizeF(textBrick.Size.Width, 300);
                }
                else if (e.Field.Name == fieldMONTHNAME.Name)
                {
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
                    e.Appearance.BorderColor = xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.BorderColor;
                    e.Appearance.BackColor = xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.BackColor;
                    e.Appearance.Font = xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font;
                }

                if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                {
                   // e.Appearance.ForeColor = xrPGMultiAbstractPayment.Styles.HeaderGroupLineStyle.ForeColor;
                    e.Appearance.Font = xrPGMultiAbstractPayment.Styles.HeaderGroupLineStyle.Font;
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
                }
            }

            if (e.ValueType == PivotGridValueType.GrandTotal)
            {
                if (e.IsColumn)
                {
                    e.Appearance.BackColor = xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.BackColor;
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

        private void xrPGMultiAbstractPayment_PrintCell(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportCellEventArgs e)
        {
            if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.TotalCell)
            {
              //  e.Appearance.ForeColor = xrPGMultiAbstractPayment.Styles.HeaderGroupLineStyle.ForeColor;
                e.Appearance.Font = xrPGMultiAbstractPayment.Styles.HeaderGroupLineStyle.Font;
                e.Appearance.WordWrap = false;
            }
            else if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.GrandTotalCell)
            {
                e.Appearance.WordWrap = false;
            }

            if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.Cell)
            {
                if (e.Brick.TextValue == null || this.UtilityMember.NumberSet.ToDouble(e.Brick.TextValue.ToString()) == 0) { e.Brick.Text = ""; }
            }
        }

        private void xrPGMultiAbstractPayment_AfterPrint(object sender, EventArgs e)
        {
            DataTable dtGrantTotal = new DataTable();
            dtGrantTotal.Columns.Add(reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName, typeof(string));
            dtGrantTotal.Columns.Add(reportSetting1.MultiAbstract.MONTHColumn.ColumnName, typeof(int));
            dtGrantTotal.Columns.Add(reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName, typeof(double));
            object oTotVal = null;
            double totVal = 0;
            int row = xrPGMultiAbstractPayment.RowCount - 1;

            for (int col = 0; col < xrPGMultiAbstractPayment.ColumnCount; col++)
            {
                oTotVal = xrPGMultiAbstractPayment.GetCellValue(col, row);
                totVal = this.UtilityMember.NumberSet.ToDouble(oTotVal.ToString());
                DataRow drGrantTotal = dtGrantTotal.NewRow();
                drGrantTotal[reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName] = "Grand Total";
                drGrantTotal[reportSetting1.MultiAbstract.MONTHColumn.ColumnName] = (col + 1);
                drGrantTotal[reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName] = totVal;
                dtGrantTotal.Rows.Add(drGrantTotal);
            }

            dtGrantTotal.AcceptChanges();
            BindGrandTotal(dtGrantTotal);
        }

        private void SetReportSetting(DataView dvPayment, AccountBalanceMulti accountBalanceMulti)
        {
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
            bool isGroupCodeVisible = (isGroupVisible && (ReportProperties.ShowGroupCode == 1));
            bool isLedgerCodeVisible = (isLedgerVisible && (ReportProperties.ShowLedgerCode == 1));
            bool isHorizontalLine = (ReportProperties.ShowHorizontalLine == 1);
            bool isVerticalLine = (ReportProperties.ShowVerticalLine == 1);
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            string rowFilterItem = "";

            //On 22/09/2022, to show Ledger Group Total-----------------------
            xrPGMultiAbstractPayment.OptionsView.ShowRowTotals = false;
            if (ReportProperties.ShowByLedgerGroup == 1 && ReportProperties.ShowByLedger == 1)
            {
                xrPGMultiAbstractPayment.KeepTogether = true;
                this.VerticalContentSplitting = DevExpress.XtraPrinting.VerticalContentSplitting.Smart;

                xrPGMultiAbstractPayment.OptionsView.ShowRowTotals = true;
                xrPGMultiAbstractPayment.Appearance.FieldValueTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
                fieldGROUPCODE.TotalsVisibility = PivotTotalsVisibility.None;
                fieldLEDGERCODE.TotalsVisibility = PivotTotalsVisibility.None;
                xrPGMultiAbstractPayment.OptionsView.ShowTotalsForSingleValues = true;
                xrPGMultiAbstractPayment.Styles.TotalCellStyle = xrPGMultiAbstractPayment.Styles.GrandTotalCellStyle;
                //xrPGMultiAbstractReceipt.OptionsView.RowTotalsLocation = PivotRowTotalsLocation.Far;
            }
            //----------------------------------------------------------------

            //01/03/2019, fix paper oriantaion and font based on selected months
            SetFieldFontsPaperSize();

            //Attach / Detach all ledgers
            //dvPayment.RowFilter = "";

            //if (ReportProperties.IncludeAllLedger == 0)
            //{
            //    DataView dvFilter = dvPayment;

            //    //if (this.ReportProperties.SortByGroup == 0 && this.ReportProperties.SortByLedger == 0)
            //    //{
            //    //    dvFilter.Sort = reportSetting1.MultiAbstract.GROUP_CODEColumn.ColumnName + "," + reportSetting1.MultiAbstract.LEDGER_CODEColumn.ColumnName;
            //    //}
            //    //else if (this.ReportProperties.SortByGroup == 1 && this.ReportProperties.SortByLedger == 1)
            //    //{
            //    //    dvFilter.Sort = reportSetting1.MultiAbstract.LEDGER_GROUPColumn.ColumnName + "," + reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName;
            //    //}
            //    //else if (this.ReportProperties.SortByLedger == 1 && this.ReportProperties.SortByGroup == 0)
            //    //{
            //    //    dvFilter.Sort = reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName + "," + reportSetting1.MultiAbstract.GROUP_CODEColumn.ColumnName;
            //    //}
            //    //else if (this.ReportProperties.SortByLedger == 0 && this.ReportProperties.SortByGroup == 1)
            //    //{
            //    //    dvFilter.Sort = reportSetting1.MultiAbstract.LEDGER_CODEColumn.ColumnName + "," + reportSetting1.MultiAbstract.LEDGER_GROUPColumn.ColumnName;
            //    //}
            //    dvFilter.Sort = reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName;
            //    try
            //    {
            //        if (dvFilter.ToTable() != null && dvPayment.ToTable() != null)
            //        {
            //            dvFilter.Sort = reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName;
            //            //Applying Group by LEDGER_NAME
            //            DataTable dtMappedLedger = dvFilter.ToTable().AsEnumerable().
            //                GroupBy(r => r.Field<String>(reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName)).Select(g => g.First()).CopyToDataTable();

            //            DataTable dtSource = dvPayment.ToTable();

            //            //Joining Two table on LEDGER_NAME wiht Amount >0 and get all those records
            //            var LedgerName = (from s in dtMappedLedger.AsEnumerable()
            //                              join m in dtSource.AsEnumerable() on s.Field<string>(reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName)
            //                              equals m.Field<string>(reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName)
            //                              where m.Field<Decimal?>(reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName) > 0
            //                              select m);
            //            if (LedgerName.Count() > 0)
            //            {
            //                //Applying Group by LEDGER_NAME
            //                DataTable dtLedgerList = LedgerName.CopyToDataTable().AsEnumerable().GroupBy(r => r.Field<String>(reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName)).Select(g => g.First()).CopyToDataTable();
            //                rowFilterItem = this.GetCommaSeparatedValue(dtLedgerList, reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName);
            //                dvPayment.RowFilter = reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName + "  IN (" + rowFilterItem + ")";
            //            }
            //            else
            //            {
            //                //On 23/06/2017, when there is not entires, report takes first ledger name in the report
            //                //dvPayment.RowFilter = reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName + " IN ('" + dr[reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName] + "')";
            //                //DataRow dr = dtSource.AsEnumerable().FirstOrDefault();
            //                dvPayment.RowFilter = reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName + " IN ('')";
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageRender.ShowMessage(ex.Message);
            //    }
            //}


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
            xrPGGrandTotal.LeftF = xrPGMultiAbstractPayment.LeftF;
            if (fieldGROUPCODE.Visible) { rowWidth = fieldGROUPCODE.Width; }
            if (fieldLEDGERGROUP.Visible) { rowWidth += fieldLEDGERGROUP.Width; }
            if (fieldLEDGERCODE.Visible) { rowWidth += fieldLEDGERCODE.Width; }
            if (fieldLEDGERNAME.Visible) { rowWidth += fieldLEDGERNAME.Width; }
            fieldGRANTTOTALPARTICULARS.Width = rowWidth;
            fieldGRANTTOTALMONTH.Width = fieldMONTHNAME.Width;
            fieldGRANTTOTALAMOUNT.Width = fieldAMOUNT.Width;

            //Grid Lines
            if (isHorizontalLine)
            {
                xrPGMultiAbstractPayment.OptionsPrint.PrintHorzLines = DevExpress.Utils.DefaultBoolean.True;
            }
            else
            {
                xrPGMultiAbstractPayment.OptionsPrint.PrintHorzLines = DevExpress.Utils.DefaultBoolean.False;
            }

            if (isVerticalLine)
            {
                xrPGMultiAbstractPayment.OptionsPrint.PrintVertLines = DevExpress.Utils.DefaultBoolean.True;
            }
            else
            {
                xrPGMultiAbstractPayment.OptionsPrint.PrintVertLines = DevExpress.Utils.DefaultBoolean.False;
            }

            //22/09/2020, To fix Border color based on settings
            xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
            xrPGMultiAbstractPayment.Styles.CellStyle.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
            xrPGMultiAbstractPayment.Styles.GrandTotalCellStyle.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;

            //Set Subreport Properties
            xrSubBalanceMulti.LeftF = xrPGMultiAbstractPayment.LeftF;
            accountBalanceMulti.LeftPosition = (xrPGMultiAbstractPayment.LeftF - 5);
            accountBalanceMulti.GroupCodeColumnWidth = fieldGROUPCODE.Width;
            accountBalanceMulti.GroupNameColumnWidth = fieldLEDGERGROUP.Width;
            accountBalanceMulti.LedgerCodeColumnWidth = fieldLEDGERCODE.Width;
            accountBalanceMulti.LedgerNameColumnWidth = fieldLEDGERNAME.Width;
            accountBalanceMulti.AmountColumnWidth = fieldMONTHNAME.Width;
            accountBalanceMulti.ShowColumnHeader = false;
            accountBalanceMulti.ApplyParentReportStyle = xrPGMultiAbstractPayment.Styles;
        }


        //01/03/2019, fix paper oriantaion and font based on selected months
        private void SetFieldFontsPaperSize()
        {
            bool isGroupVisible = (ReportProperties.ShowByLedgerGroup == 1);
            bool isLedgerVisible = (ReportProperties.ShowByLedger == 1);
            if (isGroupVisible == false && isLedgerVisible == false) { isLedgerVisible = true; }
            bool isGroupCodeVisible = (isGroupVisible && (ReportProperties.ShowGroupCode == 1));
            bool isLedgerCodeVisible = (isLedgerVisible && (ReportProperties.ShowLedgerCode == 1));

            //01/03/2019, fix paper oriantaion and font based on selected months
            NoOfMonths = Math.Abs(12 * (UtilityMember.DateSet.ToDate(ReportProperties.DateTo, false).Year - UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).Year)
                                 + UtilityMember.DateSet.ToDate(ReportProperties.DateTo, false).Month - UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).Month);
            if (NoOfMonths <= 2)
            {
                this.Landscape = false;
                fieldGROUPCODE.Width = 70; //50;
                fieldLEDGERCODE.Width = 70; //50;
                fieldLEDGERGROUP.Width = isGroupVisible && isLedgerVisible ? 125 : 185; //121,90;
                fieldLEDGERNAME.Width = isGroupVisible && isLedgerVisible ? 125 : 185; //121,90;
                fieldMONTHNAME.Width = isGroupVisible && isLedgerVisible ? 108 : 110; //121,90;

                float realfont = (float)10;
                fieldGROUPCODE.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                fieldLEDGERGROUP.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                fieldLEDGERCODE.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                fieldLEDGERNAME.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                xrPGMultiAbstractPayment.Styles.CellStyle.Font = new Font(xrPGMultiAbstractPayment.Styles.CellStyle.Font.FontFamily, realfont);
                xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                xrPGMultiAbstractPayment.Styles.HeaderGroupLineStyle.Font = new Font(xrPGMultiAbstractPayment.Styles.HeaderGroupLineStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                xrPGMultiAbstractPayment.Styles.GrandTotalCellStyle.Font = new Font(xrPGMultiAbstractPayment.Styles.GrandTotalCellStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                
                //On 16/04/2021
                fieldLEDGERGROUP.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, realfont, FontStyle.Bold);
                
                this.Margins.Left = 35;
                this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = this.PageWidth - 15;
                this.SetTitleWidth(this.PageWidth - 15);
                this.PrintingSystem.PageSettingsChanged += new EventHandler(PrintingSystem_PageSettingsChanged);
            }
            else if (NoOfMonths <= 5)
            {
                this.Landscape = true;
                fieldGROUPCODE.Width = 70; //50;
                fieldLEDGERCODE.Width = 60;//70 //50;
                fieldLEDGERGROUP.Width = isGroupVisible && isLedgerVisible ? 120 : 185; //121,90;
                fieldLEDGERNAME.Width = isGroupVisible && isLedgerVisible ? 120 : 185; //121,90;
                fieldMONTHNAME.Width = isGroupVisible && isLedgerVisible ? 110 : 110; //121,90;
                this.Margins.Left = 35;
                float realfont = (float)10;
                fieldGROUPCODE.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                fieldLEDGERGROUP.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                fieldLEDGERCODE.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                fieldLEDGERNAME.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                xrPGMultiAbstractPayment.Styles.CellStyle.Font = new Font(xrPGMultiAbstractPayment.Styles.CellStyle.Font.FontFamily, realfont);
                xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                xrPGMultiAbstractPayment.Styles.HeaderGroupLineStyle.Font = new Font(xrPGMultiAbstractPayment.Styles.HeaderGroupLineStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                xrPGMultiAbstractPayment.Styles.GrandTotalCellStyle.Font = new Font(xrPGMultiAbstractPayment.Styles.GrandTotalCellStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                
                //On 16/04/2021
                fieldLEDGERGROUP.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, realfont, FontStyle.Bold);
                

                this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = this.PageWidth - 15;
                this.SetTitleWidth(this.PageWidth - 15);
            }
            else if (NoOfMonths <= 8)
            {
                this.Landscape = true;
                fieldGROUPCODE.Width = 65; //40;
                fieldLEDGERCODE.Width = 60; //65 //40;
                fieldLEDGERGROUP.Width = isGroupVisible && isLedgerVisible ? 90 : 135; //121,90;
                fieldLEDGERNAME.Width = isGroupVisible && isLedgerVisible ? 90 : 135; //121,90;
                fieldMONTHNAME.Width = isGroupVisible && isLedgerVisible ? 85 : 85; //121,90;

                float realfont = (float)8;
                fieldGROUPCODE.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                fieldLEDGERGROUP.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                fieldLEDGERCODE.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                fieldLEDGERNAME.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                xrPGMultiAbstractPayment.Styles.CellStyle.Font = new Font(xrPGMultiAbstractPayment.Styles.CellStyle.Font.FontFamily, realfont);
                xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                xrPGMultiAbstractPayment.Styles.HeaderGroupLineStyle.Font = new Font(xrPGMultiAbstractPayment.Styles.HeaderGroupLineStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                xrPGMultiAbstractPayment.Styles.GrandTotalCellStyle.Font = new Font(xrPGMultiAbstractPayment.Styles.GrandTotalCellStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);

                //On 16/04/2021
                fieldLEDGERGROUP.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, realfont , FontStyle.Bold);

                this.Margins.Left = 18;// 20;
                this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = this.PageWidth - 15;
                this.SetTitleWidth(this.PageWidth - 15);
            }
            else
            {
                //On 14/03/2018, To set/reset amount column width based on Showcode
                this.Landscape = true;
                fieldGROUPCODE.Width = 40; //30;
                fieldLEDGERCODE.Width = 35; //40 30;
                if (isLedgerCodeVisible || isGroupCodeVisible)
                {
                    fieldLEDGERGROUP.Width = (isGroupVisible && isLedgerVisible) ? 65 : 105; //70 : 110 //121,90;
                    fieldLEDGERNAME.Width = (isGroupVisible && isLedgerVisible) ? 65 : 105; //70 : 110 //121,130;
                    fieldMONTHNAME.Width = (isGroupVisible && isLedgerVisible) ? 71 : 73; //63 : 73
                }
                else
                {
                    fieldLEDGERGROUP.Width = (isGroupVisible && isLedgerVisible) ? 85 : 120; //118,90;
                    fieldLEDGERNAME.Width = (isGroupVisible && isLedgerVisible) ? 85 : 120; //118,130;
                    fieldMONTHNAME.Width = (isGroupVisible && isLedgerVisible) ? 71 : 75; //66 : 76;
                }

                float realfont = (float)6; //6.5
                fieldGROUPCODE.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, 6, FontStyle.Bold);
                fieldLEDGERGROUP.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, 6, FontStyle.Bold);
                fieldLEDGERCODE.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, 6, FontStyle.Bold);
                fieldLEDGERNAME.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, 6, FontStyle.Bold);
                xrPGMultiAbstractPayment.Styles.CellStyle.Font = new Font(xrPGMultiAbstractPayment.Styles.CellStyle.Font.FontFamily, realfont);
                xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, 6, FontStyle.Bold);
                xrPGMultiAbstractPayment.Styles.HeaderGroupLineStyle.Font = new Font(xrPGMultiAbstractPayment.Styles.HeaderGroupLineStyle.Font.FontFamily, 6, FontStyle.Bold);
                xrPGMultiAbstractPayment.Styles.GrandTotalCellStyle.Font = new Font(xrPGMultiAbstractPayment.Styles.GrandTotalCellStyle.Font.FontFamily, 6, FontStyle.Bold);
                
                //On 16/04/2021
                fieldLEDGERGROUP.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, 6 , FontStyle.Bold);

                this.Margins.Left = 14;//14;
                this.Margins.Right = 5; //0
                xrPGMultiAbstractPayment.LeftF = xrPGGrandTotal.LeftF = xrSubBalanceMulti.LeftF = 2;
                this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = (this.PageWidth - 35);
                this.SetTitleWidth(this.PageWidth-35);
            }
        }

        public void PrintingSystem_PageSettingsChanged(object sender, EventArgs e)
        {
            bool isGroupVisible = (ReportProperties.ShowByLedgerGroup == 1);
            bool isLedgerVisible = (ReportProperties.ShowByLedger == 1);
            if (isGroupVisible == false && isLedgerVisible == false) { isLedgerVisible = true; }
            bool isGroupCodeVisible = (isGroupVisible && (ReportProperties.ShowGroupCode == 1));
            bool isLedgerCodeVisible = (isLedgerVisible && (ReportProperties.ShowLedgerCode == 1));

            if (NoOfMonths <= 2)
            {
                if (xrPGMultiAbstractPayment.DataSource != null)
                {
                    PrintingSystemBase printingbase = sender as PrintingSystemBase;

                    this.Landscape = printingbase.PageSettings.Landscape;
                    int newPageWidth = printingbase.PageBounds.Width - printingbase.PageMargins.Left - printingbase.PageMargins.Right;
                    //SetReportSetting(dv, accountBalanceMulti);
                    if (this.Landscape)
                    {
                        fieldGROUPCODE.Width = 70;  //50;
                        fieldLEDGERCODE.Width = 70; //50;
                        fieldLEDGERGROUP.Width = isGroupVisible && isLedgerVisible ? 250 : 275; //121,90;
                        fieldLEDGERNAME.Width = isGroupVisible && isLedgerVisible ? 250 : 275; //121,90;
                        fieldMONTHNAME.Width = isGroupVisible && isLedgerVisible ? 110 : 125; //121,90;

                        float realfont = (float)10;
                        fieldGROUPCODE.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                        fieldLEDGERGROUP.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                        fieldLEDGERCODE.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                        fieldLEDGERNAME.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                        xrPGMultiAbstractPayment.Styles.CellStyle.Font = new Font(xrPGMultiAbstractPayment.Styles.CellStyle.Font.FontFamily, realfont);
                        xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                        xrPGMultiAbstractPayment.Styles.HeaderGroupLineStyle.Font = new Font(xrPGMultiAbstractPayment.Styles.HeaderGroupLineStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                        xrPGMultiAbstractPayment.Styles.GrandTotalCellStyle.Font = new Font(xrPGMultiAbstractPayment.Styles.GrandTotalCellStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);

                        //this.Margins.Left = 35;
                        this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = newPageWidth - 15;
                        this.SetTitleWidth(newPageWidth - 15);
                    }
                    else
                    {
                        fieldGROUPCODE.Width = 70;  //50;
                        fieldLEDGERCODE.Width = 70; // 50;
                        fieldLEDGERGROUP.Width = isGroupVisible && isLedgerVisible ? 125 : 185; //121,90;
                        fieldLEDGERNAME.Width = isGroupVisible && isLedgerVisible ? 125 : 185; //121,90;
                        fieldMONTHNAME.Width = isGroupVisible && isLedgerVisible ? 108 : 110; //121,90;

                        float realfont = (float)10;
                        fieldGROUPCODE.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                        fieldLEDGERGROUP.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                        fieldLEDGERCODE.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                        fieldLEDGERNAME.Appearance.FieldHeader.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                        xrPGMultiAbstractPayment.Styles.CellStyle.Font = new Font(xrPGMultiAbstractPayment.Styles.CellStyle.Font.FontFamily, realfont);
                        xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font = new Font(xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                        xrPGMultiAbstractPayment.Styles.HeaderGroupLineStyle.Font = new Font(xrPGMultiAbstractPayment.Styles.HeaderGroupLineStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);
                        xrPGMultiAbstractPayment.Styles.GrandTotalCellStyle.Font = new Font(xrPGMultiAbstractPayment.Styles.GrandTotalCellStyle.Font.FontFamily, realfont - 1, FontStyle.Bold);

                        //this.Margins.Left = 35;
                        this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = newPageWidth - 15;
                        this.SetTitleWidth(newPageWidth - 15);
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
                    xrPGGrandTotal.LeftF = xrPGMultiAbstractPayment.LeftF;
                    if (fieldGROUPCODE.Visible) { rowWidth = fieldGROUPCODE.Width; }
                    if (fieldLEDGERGROUP.Visible) { rowWidth += fieldLEDGERGROUP.Width; }
                    if (fieldLEDGERCODE.Visible) { rowWidth += fieldLEDGERCODE.Width; }
                    if (fieldLEDGERNAME.Visible) { rowWidth += fieldLEDGERNAME.Width; }
                    fieldGRANTTOTALPARTICULARS.Width = rowWidth;
                    fieldGRANTTOTALMONTH.Width = fieldMONTHNAME.Width;
                    fieldGRANTTOTALAMOUNT.Width = fieldAMOUNT.Width;

                    AccountBalanceMulti accountBalanceMulti = xrSubBalanceMulti.ReportSource as AccountBalanceMulti;
                    xrSubBalanceMulti.LeftF = xrPGMultiAbstractPayment.LeftF;
                    accountBalanceMulti.LeftPosition = (xrPGMultiAbstractPayment.LeftF - 5);
                    accountBalanceMulti.GroupCodeColumnWidth = fieldGROUPCODE.Width;
                    accountBalanceMulti.GroupNameColumnWidth = fieldLEDGERGROUP.Width;
                    accountBalanceMulti.LedgerCodeColumnWidth = fieldLEDGERCODE.Width;
                    accountBalanceMulti.LedgerNameColumnWidth = fieldLEDGERNAME.Width;
                    accountBalanceMulti.AmountColumnWidth = fieldMONTHNAME.Width;
                    accountBalanceMulti.ApplyParentReportStyle = xrPGMultiAbstractPayment.Styles;
                    accountBalanceMulti.SetReportSetting();
                    if (!ReceiptsAndPayments)
                    {
                        this.CreateDocument();
                    }
                }
            }
        }
        

        /// <summary>
        /// On 13/08/2018, to show TDS on FD interest for accumulate interest
        /// We show FD renewal accumulated jounral entry interest amount in receipt side
        /// After adding TDS entry along with FD interest, for Accumulated interest TDS amount should be added with Payment side
        /// 
        /// this method will retrn entries which are made on TDS on FD intererest ledger while renewing accumulated intrest
        /// </summary>
        /// <returns></returns>
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

        private void xrPGMultiAbstractPayment_CustomColumnWidth(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotCustomColumnWidthEventArgs e)
        {
            //On 26/09/2018, to increase row total column width alone
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
            {
                e.ColumnWidth = fieldMONTHNAME.Width + 1;  //15;
            }
        }

        private void xrPGGrandTotal_CustomColumnWidth(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotCustomColumnWidthEventArgs e)
        {
            //On 26/09/2018, to increase row total column width alone
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Value)
            {
                if (e.ColumnIndex == xrPGGrandTotal.ColumnCount - 1)
                {
                    e.ColumnWidth = fieldMONTHNAME.Width + 1; //15;
                }
            }
        }
                
        private void xrPGMultiAbstractPayment_CustomRowHeight(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotCustomRowHeightEventArgs e)
        {
            int defaultrowheight = e.RowHeight;//Default height
            try
            {
                if (e.Field != null)
                {
                    if (e.ValueType != PivotGridValueType.Total && e.ValueType != PivotGridValueType.GrandTotal)
                    {
                        if (e.Field.Name == fieldLEDGERNAME.Name || e.Field.Name == fieldLEDGERGROUP.Name)
                        {
                            //string ledgergroup = string.Empty;
                            //string ledgername = e.GetFieldValue(e.Field, e.RowIndex).ToString().Trim();
                            //Int32 RowHeightLedgerName = GetRowHeight(ledgername, fieldLEDGERNAME.Width, e.RowHeight);
                            //Int32 RowHeightLedgerGroup = 0;
                            //if (fieldLEDGERGROUP.Visible)
                            //{
                            //    ledgergroup = e.GetFieldValue(fieldLEDGERGROUP, e.RowIndex).ToString().Trim();
                            //    RowHeightLedgerGroup = GetRowHeight(ledgergroup, fieldLEDGERGROUP.Width, e.RowHeight);
                            //}
                            //if (ledgergroup == "Bank Charges of Bank 124")
                            //{

                            //}
                            //e.RowHeight = Math.Max(RowHeightLedgerName, RowHeightLedgerGroup);

                            e.RowHeight = defaultrowheight;
                            string ledgercode = string.Empty;
                            string ledgergroup = string.Empty;
                            string ledgername = e.GetFieldValue(e.Field, e.RowIndex).ToString().Trim();
                            //SizeF size = gr.MeasureString(ledgername, xrPGMultiAbstractPayment.Styles.CellStyle.Font, fieldLEDGERNAME.Width - 2);
                            SizeF size = gr.MeasureString(ledgername, xrPGMultiAbstractPayment.Styles.CellStyle.Font, fieldLEDGERNAME.Width);
                            Int32 RowHeightLedgerName = Convert.ToInt32(size.Height + 0.5);
                            //Int32 RowHeightLedgerName = GetRowHeight(ledgername, fieldLEDGERNAME.Width, e.RowHeight);
                            Int32 RowHeightLedgerGroup = 0;
                            Int32 RowHeightLedgerCode = 0;

                            if (fieldLEDGERGROUP.Visible)
                            {
                                ledgergroup = e.GetFieldValue(fieldLEDGERGROUP, e.RowIndex).ToString().Trim();
                                //size = gr.MeasureString(ledgergroup, xrPGMultiAbstractPayment.Styles.CellStyle.Font, fieldLEDGERGROUP.Width - 2);
                                size = gr.MeasureString(ledgergroup, fieldLEDGERGROUP.Appearance.FieldValue.Font, fieldLEDGERGROUP.Width);
                                //RowHeightLedgerGroup = GetRowHeight(ledgergroup, fieldLEDGERGROUP.Width, e.RowHeight);
                                RowHeightLedgerGroup = Convert.ToInt32(size.Height + 0.5);
                            }

                            if (fieldLEDGERCODE.Visible)
                            {
                                ledgercode = string.Empty;
                                if ((e.GetFieldValue(fieldLEDGERCODE, e.RowIndex)) != null)
                                {
                                    ledgercode = e.GetFieldValue(fieldLEDGERCODE, e.RowIndex).ToString().Trim();
                                }
                                //size = gr.MeasureString(ledgercode, xrPGMultiAbstractPayment.Styles.CellStyle.Font, fieldLEDGERCODE.Width - 2);
                                size = gr.MeasureString(ledgercode, fieldLEDGERCODE.Appearance.FieldValue.Font, fieldLEDGERCODE.Width - 2);
                                RowHeightLedgerCode = Convert.ToInt32(size.Height + 0.5);
                            }
                            e.RowHeight = Math.Max(RowHeightLedgerName, RowHeightLedgerGroup);
                            e.RowHeight = Math.Max(RowHeightLedgerCode, e.RowHeight);
                        }
                    }
                    else if (e.ValueType == PivotGridValueType.Total)
                    {
                        //On 22/09/2022, To hide empty row group (Ledger Group), as unable remove empty ledger group ---------------
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
                }
            }
            catch (Exception err)
            {
                e.RowHeight = defaultrowheight;//Default height
                MessageRender.ShowMessage("Not able to set row right "  +err.Message);
            }
        }

        private Int32 GetRowHeight(string name, Int32 fieldwith, int defaultHeight)
        {
            int RowHeight = defaultHeight;
            int noofrows = 1;
            SizeF size = this.GetPrintingSystem().Graph.MeasureString(name);
            if (size.Width > fieldwith)
            {
                float newVariable = (size.Width / fieldwith);
                double decPartnewVariable = newVariable - Math.Truncate(newVariable);
                if (decPartnewVariable > 0.5 && (NoOfMonths <= 8))
                {
                    newVariable++;
                }

                noofrows = (int)Math.Ceiling((decimal)newVariable);
                if (noofrows > 1)
                {
                    int newRowHeight = defaultHeight;
                    if (NoOfMonths > 8)
                    {
                        newRowHeight = ((defaultHeight - 2) * (noofrows));//(noofrows > 2 ? noofrows - 2 : noofrows));
                    }
                    else
                    {
                        newRowHeight = ((defaultHeight - 5) * (noofrows));
                    }

                    RowHeight = newRowHeight;
                }
            }

            return RowHeight;
        }
    }
}
