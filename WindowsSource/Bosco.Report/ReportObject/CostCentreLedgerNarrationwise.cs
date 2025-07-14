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
using Bosco.Report.View;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class CostCentreLedgerNarrationwise : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public CostCentreLedgerNarrationwise()
        {
            InitializeComponent();
            this.AttachDrillDownToRecord(xrtblDetails, xrCosTransMode,
                   new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName }, DrillDownType.LEDGER_VOUCHER, false);
        }
        #endregion

        #region Variables
        double CosLedgerDebit = 0;
        double CosLedgerCredit = 0;
        double CosLedgerDebitSum = 0;
        int CosMonthlyGroupNumber = 0;
        double CosMonthlyOpeningBalance = 0;
        double CosMonthlyClosingBalance = 0;
        private string CostCentreId = string.Empty;
        double CCDebit = 0.0;
        double CCCredit = 0.0;
        int count = 0;

        string VoucherType = "RC','PY";
        Int32 LedgerId = 0;
        string CostCenter = string.Empty;
        string RptAsOnDate = string.Empty;

        double CCOPBalance = 0;
        double CCCOPBalance = 0;
        double CosDebit = 0;
        double CosDebitTotal = 0;
        double CosCredit = 0;
        double CosCreditTotal = 0;
        double CosCreditGrandTotal = 0;
        double CosDebitGrandTotal = 0;
        int LedgerGroupNumber = 0;
        int CCGroupNumber = 0;

        DataTable dtCCOpeningBalance = new DataTable();

        //14/04/2020, to keep CC opening alone without adding closing balance to calculate cc category sum
        DataTable dtCCOpeningBalanceAlone = new DataTable();
        #endregion

        #region ShowReport

        public override void ShowReport()
        {
            //  MonthlyGroupNumber = 0;
            CCDebit = CCCredit = 0.0;

            //26/03/2020
            CosDebit = CosCredit = 0;
            CosDebitTotal = CosCreditTotal = 0;
            CosDebitGrandTotal = CosDebitGrandTotal = 0;


            xrtblCosBalance.Text = "";
            if (IsDrillDownMode)
            {
                //21/06/2017
                /// When drill-down , we use existing general ledger report for drill ledger report (for particular ledger).
                /// if user generate general ledger in another tab, it should not overlap drilled and general ledger
                if (this.ReportProperties.DrillDownProperties != null && this.ReportProperties.DrillDownProperties.Count > 0)
                {
                    Dictionary<string, object> dicDDProperties = this.ReportProperties.DrillDownProperties;
                    DrillDownType ddtypeLinkType = DrillDownType.BASE_REPORT;
                    ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), dicDDProperties["DrillDownLink"].ToString());

                    //10/03/2017, for drill down, we use this ledger report for all reports,
                    //If ledger reports is already showing in another report with its own properties like (monthly total, ledger summery etc)
                    this.ReportProperties.IncludeNarration = 1;
                    this.ReportProperties.IncludeLedgerGroup = 0;
                    this.ReportProperties.IncludeLedgerGroupTotal = 0;
                    this.ReportProperties.ShowMonthTotal = 0;
                    this.ReportProperties.ShowByLedgerSummary = 0;
                    this.ReportProperties.ShowByLedger = 0;
                    this.ReportProperties.ShowLedgerOpBalance = 0;
                    this.ReportProperties.BreakByLedger = 0;
                    this.ReportProperties.DonorConditionSymbol = string.Empty;
                    this.ReportProperties.DonorFilterAmount = 0;

                    switch (ddtypeLinkType)
                    {
                        case DrillDownType.LEDGER_SUMMARY_RECEIPTS:
                            VoucherType = "RC";
                            break;
                        case DrillDownType.LEDGER_SUMMARY_PAYMENTS:
                            VoucherType = "PY";
                            break;
                    }

                    if (dicDDProperties.ContainsKey("LEDGER_ID"))
                    {
                        LedgerId = UtilityMember.NumberSet.ToInteger(dicDDProperties["LEDGER_ID"].ToString());
                        this.ReportProperties.Ledger = dicDDProperties[this.ReportParameters.LEDGER_IDColumn.ColumnName].ToString();
                    }

                    //if (dicDDProperties.ContainsKey("PARTICULARS_ID"))
                    //    LedgerId = UtilityMember.NumberSet.ToInteger(dicDDProperties["PARTICULARS_ID"].ToString());

                    if (dicDDProperties.ContainsKey(this.ReportParameters.COST_CENTRE_IDColumn.ColumnName))
                        CostCentreId = dicDDProperties[this.ReportParameters.COST_CENTRE_IDColumn.ColumnName].ToString();

                    //if (dicDDProperties.ContainsKey(this.ReportParameters.DATE_AS_ONColumn.ColumnName))
                    //    RptAsOnDate = dicDDProperties[this.ReportParameters.DATE_AS_ONColumn.ColumnName].ToString();

                    SetReportTitle();
                }
            }
            BindReport();
            base.ShowReport();
        }

        #endregion

        #region Methods
        public void BindReport()
        {
            grpHeaderCC.Visible = grpFooterCC.Visible = Detail.Visible = ReportFooter.Visible = false;

            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo) ||
                this.ReportProperties.Project == "0" || this.ReportProperties.CostCentre == "0" ||
                (this.ReportProperties.Ledger == "0" && this.ReportProperties.ReportId != "RPT-238"))
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
                        BindReportProperty();
                    }
                    else
                    {
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    BindReportProperty();
                }
            }

            // This is to update the opening Balances
            if (this.ReportProperties.ReportId == "RPT-238")
            {
                this.ReportProperties.ShowByCostCentre = 1;

                xrtblOpDateFrom.Text = this.ReportProperties.DateFrom;
            }

            /// this is to show and hide the details
            if (this.ReportProperties.ShowByCostCentre == 1)
            {
                grpHeaderCC.GroupFields[0].FieldName = "COST_CENTRE_NAME";
                grpHeaderCC.Visible = true;
                grpFooterCC.Visible = true;
            }
            else
            {
                grpHeaderCC.GroupFields[0].FieldName = "";
                grpHeaderCC.Visible = false;
                grpFooterCC.Visible = false;
            }


        }

        private void BindReportProperty()
        {
            bool ShowLedgerGrp = true;
            SplashScreenManager.ShowForm(typeof(frmReportWait));
            this.SetLandscapeHeader = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooter = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooterDateWidth = xrtblHeaderCaption.WidthF;
            this.SetLandscapeCostCentreWidth = xrtblHeaderCaption.WidthF; ;
            setHeaderTitleAlignment();
            this.ReportTitle = this.ReportProperties.ReportTitle;
            grpHeaderCC.Visible = true;
            Detail.Visible = true;

            SetReportTitle();

            //On 26/11/2024 - Ledger report - by ledger / by cost centre 
            //this.ReportProperties.ShowByCostCentre = 0;
            if (this.ReportProperties.ReportId == "RPT-231") //Ledger- Cost Centre-wise
            {
                this.ReportProperties.ShowByCostCentre = 1;
            }
            else if (this.ReportProperties.ReportId == "RPT-238") //Ledger- Cost Centre-wise (Narration)
            {
                this.ReportProperties.ShowByCostCentre = 1;
                //  ShowLedgerGrp = false;

            }

            if (grpHeaderCC.Visible != true)
            {
                this.CosCenterName = ReportProperty.Current.CostCentreName;
            }
            else
            {
                this.CosCenterName = string.Empty;
            }



            // To show by costcentre starts
            /*if (this.ReportProperties.ShowByCostCentreCategory == 1)
            {
                grpHeaderCC.Visible = grpFooterCC.Visible = grpHeaderCostcentreCategoryName.Visible = Detail.Visible = true;
                grpHeadrLedgerName.Visible = true;
            }
            else
            {
                grpHeaderCostcentreCategoryName.Visible = false;
                grpHeaderCC.Visible = grpFooterCC.Visible = grpHeadrLedgerName.Visible = Detail.Visible = true;
            }*/

            //03/03/2025
            // grpHeaderCC.Visible = grpFooterCC.Visible = Detail.Visible = true;
            Detail.Visible = true;

            //On 22/12/2023 - Change Group order for CC-wise report


            //  this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + this.ReportProperties.DateFrom + "-" + this.ReportProperties.DateTo;
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.INCOME.ToString(), xrCosCapCredit);
            this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.EXPENDITURE.ToString(), xrCosCapDebit);
            this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.CLOSINGBALANCE.ToString(), xrCosCapBalance);

            CosLedgerDebit = CosLedgerCredit = CosLedgerDebitSum = CosMonthlyGroupNumber = LedgerGroupNumber = CCGroupNumber = 0;

            prOPBalance.Value = "0.00";

            //On 24/03/2020, to show/hide cc opening Balance based on setting 
            xrRowCCOpeningBalance.Visible = (AppSetting.ShowCCOpeningBalanceInReports == 1);
            if (ReportProperties.IncludeNarrationwithCurrencyDetails == 1)
            {
                ReportProperties.IncludeNarration = 1;
            }
            prOPBalance.Visible = false;
            ResultArgs resultArgs = BindLedgerSource();
            if (resultArgs.Success)
            {
                DataTable dtReport = resultArgs.DataSource.Table;
                if (dtReport != null)
                {
                    dtReport.TableName = "Ledger";
                    this.DataSource = dtReport;
                    this.DataMember = dtReport.TableName;

                    if (dtReport.Rows.Count == 0)
                    {
                        grpHeaderCC.Visible = grpFooterCC.Visible = Detail.Visible = ReportFooter.Visible = false;
                    }
                }
                SetReportBorder();
            }
            else
            {
                MessageRender.ShowMessage(resultArgs.Message);
            }
            SplashScreenManager.CloseForm();
        }

        private void SetReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrtblCCTotal = AlignCostCentreTable(xrtblCCTotal);
            xrtblGrandTotal = AlignCostCentreTable(xrtblGrandTotal);
            if (ReportProperties.ShowByCostCentre == 1)
            {
                xrCCName.BackColor = xrCosCapParticulars.BackColor;
                xrtblCCName = AlignCostCentreTable(xrtblCCName); //AlignGroupTable(xrtblCCName);
                xrtblCCCName = AlignCostCentreTable(xrtblCCCName);
            }
            else
            {
                xrtblCCName = AlignCostCentreTable(xrtblCCName);
                xrtblCCCName = AlignCostCentreTable(xrtblCCCName);
            }

        }

        public override XRTable AlignOpeningBalanceTable(XRTable table)
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
                            tcell.Borders = BorderSide.Left | BorderSide.Bottom | BorderSide.Right;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Bottom;
                        }
                        else if (count == trow.Cells.Count)
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Bottom | BorderSide.Right;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    //tcell.BorderColor = ((int)BorderStyleCell.Regular == 0) ? System.Drawing.Color.Black : System.Drawing.Color.Black;
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        public virtual XRTable AlignTotalTable(XRTable table)
        {
            int j = table.Rows.Count;
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Bottom | BorderSide.Left;
                        else if (count == trow.Cells.Count)
                            tcell.Borders = BorderSide.Bottom | BorderSide.Right;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom | BorderSide.Top;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom | BorderSide.Top;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    // tcell.BorderColor = ((int)BorderStyleCell.Regular==0)? System.Drawing.Color.Black :System.Drawing.Color.Black;
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        public override XRTable AlignGrandTotalTable(XRTable table)
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
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Bottom | BorderSide.Left;
                        else if (count == trow.Cells.Count)
                            tcell.Borders = BorderSide.Bottom | BorderSide.Right;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;
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
                            tcell.Borders = BorderSide.All;

                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Bottom | BorderSide.Top | BorderSide.Left;
                        else if (count == trow.Cells.Count)
                            tcell.Borders = BorderSide.Bottom | BorderSide.Top | BorderSide.Right;
                        else
                            tcell.Borders = BorderSide.Bottom | BorderSide.Top;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.All;
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Bottom | BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    //tcell.BorderColor = ((int)BorderStyleCell.Regular == 0) ? System.Drawing.Color.Black : System.Drawing.Color.Black;
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                    tcell.Font = (ReportProperty.Current.ColumnCaptionFontStyle == 0 ? FieldColumnHeaderFont : new Font(FieldColumnHeaderFont, FontStyle.Regular));
                }
            }
            return table;
        }

        public override XRTable AlignCashBankBookTable(XRTable table, string bankNarration, string cashNarration, int count)
        {
            int rowcount = 0;

            foreach (XRTableRow row in table.Rows)
            {
                int cellcount = 0;
                ++rowcount;
                if (rowcount == 2 && ReportProperties.IncludeNarration != 1)
                {
                    row.Visible = false;
                }
                else if (bankNarration == string.Empty && cashNarration == string.Empty && ReportProperties.IncludeNarration == 1 && rowcount == 2)
                {
                    row.Visible = false;
                }
                else
                {
                    row.Visible = true;
                }
                foreach (XRTableCell cell in row)
                {
                    ++cellcount;
                    if (ReportProperties.IncludeNarration != 1 && rowcount == 1)
                    {
                        if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                        {
                            if (cellcount == 1)
                                cell.Borders = BorderSide.Right | BorderSide.Left | BorderSide.Bottom;
                            else
                                cell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                        else if (ReportProperties.ShowHorizontalLine == 1)
                        {
                            if (cellcount == 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | BorderSide.Left;
                            else
                                if (cellcount == row.Cells.Count)
                                    cell.Borders = BorderSide.Bottom | BorderSide.Right;
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;

                        }
                        else if (ReportProperties.ShowVerticalLine == 1)
                        {
                            if (cellcount == 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                            else
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                        }
                        else
                        {
                            cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        }

                    }
                    else
                    {
                        if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                        {
                            if (rowcount == 1)
                            {
                                if (count == 1)
                                {
                                    if (cellcount == 1)
                                    {
                                        if (bankNarration != string.Empty || cashNarration != string.Empty)
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top;
                                        else
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                                    }
                                    else
                                    {
                                        if (bankNarration != string.Empty || cashNarration != string.Empty)
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                                        else
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                                    }
                                }
                                else
                                {
                                    if (cellcount == 1)
                                    {
                                        if (bankNarration != string.Empty || cashNarration != string.Empty)
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                                        else
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                                    }

                                    else
                                    {
                                        if (bankNarration != string.Empty || cashNarration != string.Empty)
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                                        else
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                                    }
                                }

                            }
                            else
                            {
                                if (cellcount == 1)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;

                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                            }

                        }
                        else if (ReportProperties.ShowHorizontalLine == 1)
                        {
                            if (rowcount == 1)
                            {
                                if (bankNarration != string.Empty || cashNarration != string.Empty)
                                    if (cellcount == 1)
                                        cell.Borders = BorderSide.Left;
                                    else if (cellcount == row.Cells.Count)
                                        cell.Borders = BorderSide.Right;
                                    else
                                        cell.Borders = BorderSide.None;
                                else if (cellcount == 1)
                                    cell.Borders = BorderSide.Left | BorderSide.Bottom;
                                else if (cellcount == row.Cells.Count)
                                    cell.Borders = BorderSide.Right | BorderSide.Bottom;
                                else
                                    cell.Borders = BorderSide.Bottom;
                            }
                            else
                            {
                                if (cellcount == 1)
                                    cell.Borders = BorderSide.Bottom | BorderSide.Left;
                                else if (cellcount == row.Cells.Count)
                                    cell.Borders = BorderSide.Bottom | BorderSide.Right;
                                else
                                    cell.Borders = BorderSide.Bottom;
                            }

                        }
                        else if (ReportProperties.ShowVerticalLine == 1)
                        {
                            if (rowcount == 1)
                            {
                                if (cellcount == 1)
                                {
                                    if (bankNarration != string.Empty || cashNarration != string.Empty)
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                                    else
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                                }
                                else
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                                }
                            }
                            else
                            {
                                if (cellcount == 1)
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                                }
                                else
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                                }
                            }

                        }
                        else
                        {
                            cell.Borders = BorderSide.None;
                        }
                    }
                    cell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
                cellcount = 0;
            }
            return table;
        }

        private XRTable AlignCCCategoryTable(XRTable table)
        {
            int j = table.Rows.Count;
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
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Bottom;
                            if (count == trow.Cells.Count)
                            {
                                tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            }
                        }
                        else if (count == trow.Cells.Count)
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }

                        else
                        {
                            tcell.Borders = BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;

                }
            }
            return table;
        }

        private XRTable AlignCostCentreTable(XRTable table)
        {
            int j = table.Rows.Count;
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
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;

                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Bottom;
                            if (count == trow.Cells.Count)
                            {
                                tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            }
                        }
                        else if (count == trow.Cells.Count)
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }

                        else
                        {
                            tcell.Borders = BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;

                }
            }
            return table;
        }


        private ResultArgs BindLedgerSource()
        {
            ResultArgs resultArgs = null;
            string Test = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCenterLedger);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.INCLUDE_NARRATION_REFNOColumn, this.ReportProperties.IncludeNarrationwithRefNo);

                if (this.ReportProperties.ReportId != "RPT-238")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.Ledger);
                }
                if (!string.IsNullOrEmpty(CostCentreId))
                {
                    dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, CostCentreId);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre != null ? this.ReportProperties.CostCentre : "0");
                }
                dataManager.Parameters.Add(this.ReportParameters.INCLUDE_NARRATION_CURRENCYColumn, this.ReportProperties.IncludeNarrationwithCurrencyDetails);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, Test);


                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    //On 24/03/2020, to show/hide cc opening Balance based on setting 
                    if (AppSetting.ShowCCOpeningBalanceInReports == 1)
                    {
                        BindCCOpeningBalance(resultArgs.DataSource.Table);
                    }
                }
            }
            return resultArgs;
        }


        /// <summary>
        /// On 21/08/2019, to get CC opening balance from the CC Trasaction for given datefrom
        /// </summary>
        private void BindCCOpeningBalance(DataTable dtRptSource)
        {
            ResultArgs resultArgs = new ResultArgs();
            string CCOPsql = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCenterOpeningBalance);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                //dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.Ledger);
                if (!string.IsNullOrEmpty(CostCentreId))
                {
                    dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, CostCentreId);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre != null ? this.ReportProperties.CostCentre : "0");
                }
                dataManager.Parameters.Add(this.ReportParameters.COSTCENTRE_MAPPINGColumn, settingProperty.CostCeterMapping);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, CCOPsql);
            }

            if (resultArgs.Success)
            {
                dtCCOpeningBalance = resultArgs.DataSource.Table;
                IncludeOpeningBalanceCostCenters(dtRptSource);
                //14/04/2020, to keep CC opening alone without adding closing balance to calculate cc category sum
                dtCCOpeningBalanceAlone = dtCCOpeningBalance.DefaultView.ToTable();
            }
            else
            {
                MessageRender.ShowMessage(resultArgs.Message);
            }
        }


        /// <summary>
        /// On 21/08/2019, include CC if there is no transaction (to show op balance alone)
        /// </summary>
        private void IncludeOpeningBalanceCostCenters(DataTable dtReportSource)
        {
            if (dtReportSource != null)
            {
                foreach (DataRow dr in dtCCOpeningBalance.Rows)
                {
                    Int32 CCid = this.UtilityMember.NumberSet.ToInteger(dr[this.ReportParameters.COST_CENTRE_IDColumn.ColumnName].ToString());
                    dtReportSource.DefaultView.RowFilter = this.ReportParameters.COST_CENTRE_IDColumn.ColumnName + " = " + CCid;
                    //# If there is no CC in Report source, add CC and its category
                    if (dtReportSource.DefaultView.Count == 0)
                    {
                        //31/03/2020, add if opening balane exists without transactions
                        double dCCopeningbalance = GetCCOpeningBalance(CCid);
                        if (dCCopeningbalance != 0)
                        {
                            DataRow drRptSource = dtReportSource.NewRow();
                            drRptSource[this.ReportParameters.VOUCHER_IDColumn.ColumnName] = 0;
                            drRptSource["SORT_ORDER"] = 0;
                            drRptSource[this.ReportParameters.LEDGER_IDColumn.ColumnName] = 0;
                            drRptSource["PARTICULAR_TYPE"] = "OP";
                            drRptSource["DATE"] = string.Empty;
                            drRptSource["VOUCHER_NO"] = string.Empty;
                            drRptSource[this.ReportParameters.COST_CENTRE_IDColumn.ColumnName] = CCid;
                            drRptSource["COST_CENTRE_NAME"] = dr["COST_CENTRE_NAME"].ToString();
                            drRptSource["COST_CENTRECATEGORY_ID"] = dr["COST_CENTRECATEGORY_ID"].ToString();
                            drRptSource["COST_CENTRE_CATEGORY_NAME"] = dr["COST_CENTRE_CATEGORY_NAME"].ToString();
                            drRptSource["GROUP"] = string.Empty;

                            drRptSource["LEDGER_ID"] = 0;
                            drRptSource["LEDGER_NAME"] = string.Empty;
                            if (settingProperty.CostCeterMapping == 1)
                            {
                                drRptSource["LEDGER_ID"] = dr["LEDGER_ID"].ToString(); ;
                                drRptSource["LEDGER_NAME"] = dr["LEDGER_NAME"].ToString(); ;
                            }

                            drRptSource["PARTICULARS"] = string.Empty;
                            drRptSource["CREDIT"] = 0;
                            drRptSource["DEBIT"] = 0;
                            drRptSource["LEDGER_OP_CR"] = UtilityMember.NumberSet.ToDouble(dr["LEDGER_OP_CR"].ToString());
                            drRptSource["LEDGER_OP_DR"] = UtilityMember.NumberSet.ToDouble(dr["LEDGER_OP_DR"].ToString());

                            drRptSource["NARRATION"] = string.Empty;
                            dtReportSource.Rows.Add(drRptSource);
                        }
                    }
                    dtReportSource.DefaultView.RowFilter = string.Empty;
                }
            }
        }
        #endregion

        #region Events



        private void xrtblLedgerDebitBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            string[] Led = this.ReportProperties.Ledger.Split(',');
            if (Led.Length != 1)
            {
                //CosLedgerDebit = CosLedgerCredit = 0.0;
            }
            e.Result = CosLedgerDebitSum;
            e.Handled = true;
        }

        private void xrtblLedgerDebitBalance_SummaryReset(object sender, EventArgs e)
        {
            string[] Led = this.ReportProperties.Ledger.Split(',');
            if (Led.Length != 1)
            {
                CosLedgerDebitSum = 0;
                CosLedgerDebit = CosLedgerCredit = 0;
            }
        }

        private void xttblOpBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {

            if (CosMonthlyGroupNumber == 0)
            {
                e.Result = "0.00";
                CosMonthlyClosingBalance = this.UtilityMember.NumberSet.ToDouble(xrtblCosBalance.Text);
                CosMonthlyGroupNumber++;
                e.Handled = true;
            }
            else
            {
                e.Result = CosMonthlyClosingBalance;
                CosMonthlyClosingBalance = this.UtilityMember.NumberSet.ToDouble(xrtblCosBalance.Text);
                e.Handled = true;

            }
        }

        private void xrCosDebit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double CashReceipts = this.ReportProperties.NumberSet.ToDouble(xrCosDebit.Text);
            CosDebit += CashReceipts;

            if (CashReceipts != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCosCredit.Text = "";
            }

        }

        private void xrCosCrdit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double CashReceipts = this.ReportProperties.NumberSet.ToDouble(xrCosCredit.Text);
            CosCredit += CashReceipts;

            if (CashReceipts != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCosCredit.Text = "";
            }
        }

        private void xrtblCosClosingBalance_SummaryReset(object sender, EventArgs e)
        {
            //CosLedgerDebit = CosLedgerCredit = 0;
        }

        private void xrTable2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            count++;
            string Narration = (GetCurrentColumnValue("NARRATION") == null) ? string.Empty : GetCurrentColumnValue("NARRATION").ToString();
            xrtblDetails = AlignCashBankBookTable(xrtblDetails, Narration, string.Empty, count);
        }

        private void xrOPBLDr_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //On 23/03/2020
            if (GetCurrentColumnValue(this.ReportParameters.COST_CENTRE_IDColumn.ColumnName) != null)
            {
                Int32 CCId = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(this.ReportParameters.COST_CENTRE_IDColumn.ColumnName).ToString());
                ShowCCOpeningBalance(CCId);
            }

            if (this.AppSetting.EnableCCModeReports == 1)
            {
                XRTableCell cell = (XRTableCell)sender;
                double Cellvalue = UtilityMember.NumberSet.ToDouble(cell.Text);
                if (Cellvalue < 0)
                {
                    cell.ForeColor = Color.Red;
                }
                else
                {
                    cell.ForeColor = Color.Black;
                }
            }
        }

        private void xrOPBLCr_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //On 23/03/2020
            if (GetCurrentColumnValue(this.ReportParameters.COST_CENTRE_IDColumn.ColumnName) != null)
            {
                Int32 CCId = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(this.ReportParameters.COST_CENTRE_IDColumn.ColumnName).ToString());
                ShowCCOpeningBalance(CCId);
            }

            double value = this.ReportProperties.NumberSet.ToDouble(xrOPBLCr.Text);
            if (value != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrOPBLCr.Text = "";
            }

        }

        private void xrtblCosBalance_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(this.ReportParameters.COST_CENTRE_IDColumn.ColumnName) != null)
            {
                Int32 CCId = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(this.ReportParameters.COST_CENTRE_IDColumn.ColumnName).ToString());
                double ccBalanceAmount = (CosDebit - CosCredit) + CCOPBalance;

                //On 26/03/2020, to show/hide cc opening Balance based on setting 
                if (AppSetting.ShowCCOpeningBalanceInReports == 1)
                {
                    UpdateCCOpeningBalance(CCId, ccBalanceAmount);
                }

                // This is to Show - and default + values for variuos accounts
                if (this.AppSetting.EnableCCModeReports == 1)
                {
                    if (ccBalanceAmount < 0)
                    {
                        xrtblCosBalance.Text = this.UtilityMember.NumberSet.ToNumber(Math.Abs(ccBalanceAmount)).ToString();
                    }
                    else
                    {
                        //xrtblCosBalance.Text = "-" + this.UtilityMember.NumberSet.ToNumber(Math.Abs(ccBalanceAmount)).ToString();

                        xrtblCosBalance.Text = ((UtilityMember.NumberSet.ToNumber(Math.Abs(ccBalanceAmount)).ToString()) != "0,00") ? "-" + this.UtilityMember.NumberSet.ToNumber(Math.Abs(ccBalanceAmount)).ToString() : this.UtilityMember.NumberSet.ToNumber(Math.Abs(ccBalanceAmount)).ToString();
                    }
                }
                else
                {
                    if (ccBalanceAmount < 0)
                    {
                        xrtblCosBalance.Text = this.UtilityMember.NumberSet.ToNumber(Math.Abs(ccBalanceAmount)).ToString() + " Cr";
                    }
                    else
                    {
                        xrtblCosBalance.Text = this.UtilityMember.NumberSet.ToNumber(Math.Abs(ccBalanceAmount)).ToString() + " Dr";
                    }
                }

                //if (ccBalanceAmount < 0)
                //{
                //    xrtblCosBalance.Text = this.UtilityMember.NumberSet.ToNumber(Math.Abs(ccBalanceAmount)).ToString() + " Cr";
                //}
                //else
                //{
                //    xrtblCosBalance.Text = this.UtilityMember.NumberSet.ToNumber(Math.Abs(ccBalanceAmount)).ToString() + " Dr";
                //}
            }
        }


        private void xrCLBLCr_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double ccClosingAmount = (CosDebit - CosCredit) + CCOPBalance;
            //if (ccClosingAmount < 0)
            //{
            if (GetCurrentColumnValue(this.ReportParameters.COST_CENTRE_IDColumn.ColumnName) != null)
            {
                Int32 CCId = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(this.ReportParameters.COST_CENTRE_IDColumn.ColumnName).ToString());
                //xrCLBLCr.Text = this.UtilityMember.NumberSet.ToNumber(Math.Abs(ccClosingAmount)).ToString();
                UpdateCCOpeningBalance(CCId, ccClosingAmount);
            }

        }





        private void xrSumBalance_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.DataSource != null)
            {
                DataView dv = this.DataSource as DataView;
                if (dv.Count > 0)
                {
                    double sumDebit = UtilityMember.NumberSet.ToDouble(dv.Table.Compute("SUM(" + reportSetting1.Ledger.DEBITColumn.ColumnName + ")", string.Empty).ToString());
                    double sumCredit = UtilityMember.NumberSet.ToDouble(dv.Table.Compute("SUM(" + reportSetting1.Ledger.CREDITColumn.ColumnName + ")", string.Empty).ToString());

                    double SumBalance = sumDebit - sumCredit;

                }
            }
        }

        #endregion

        private void ShowCCOpeningBalance(Int32 CostCenterId)
        {
            xrOPBLCr.Text = xrOPBLDr.Text = string.Empty;
            CCOPBalance = GetCCOpeningBalance(CostCenterId);
            if (CCOPBalance > 0)
            {
                // 11/02/2025, Chinna Opening Balances to add -
                if (this.AppSetting.EnableCCModeReports == 1)
                    xrOPBLDr.Text = "-" + this.UtilityMember.NumberSet.ToNumber(Math.Abs(CCOPBalance)).ToString();
                else
                    xrOPBLDr.Text = this.UtilityMember.NumberSet.ToNumber(Math.Abs(CCOPBalance)).ToString();
            }
            else
            {
                xrOPBLCr.Text = this.UtilityMember.NumberSet.ToNumber(Math.Abs(CCOPBalance)).ToString();
            }

            CosDebit = CosCredit = 0;
            CosDebitTotal = CosCreditTotal = 0;
            //  CosDebitGrandTotal = CosCreditGrandTotal = 0;
            //LedgerOPCR = LedgerOPDR = 0;
        }

        /// <summary>
        /// Category
        /// </summary>
        /// <param name="CategoryCCId"></param>
        private void ShowCategoryCCOpeningBalance(Int32 CategoryCCId)
        {
            //CCCOPBalance = GetCCCategoryOpeningBalance(CategoryCCId);
            //if (CCCOPBalance > 0)
            //{
            //    if (this.AppSetting.EnableCCModeReports == 1)
            //        xrCCCategoryOpeningExpense.Text = "-" + this.UtilityMember.NumberSet.ToNumber(Math.Abs(CCCOPBalance)).ToString();
            //    else
            //        xrCCCategoryOpeningExpense.Text = this.UtilityMember.NumberSet.ToNumber(Math.Abs(CCCOPBalance)).ToString();
            //}
            //else
            //{
            //    xrCCCategoryOpeningIncome.Text = this.UtilityMember.NumberSet.ToNumber(Math.Abs(CCCOPBalance)).ToString();
            //}

            //CosDebit = CosCredit = 0;
            //CosDebitTotal = CosCreditTotal = 0;
            //   CosDebitGrandTotal = CosCreditGrandTotal = 0;
            //LedgerOPCR = LedgerOPDR = 0;
        }

        private double GetCCOpeningBalance(Int32 CostCenterId)
        {
            double Rtn = 0;
            if (dtCCOpeningBalance != null && dtCCOpeningBalance.Rows.Count > 0)
            {
                dtCCOpeningBalance.DefaultView.RowFilter = this.ReportParameters.COST_CENTRE_IDColumn.ColumnName + "=" + CostCenterId;
                if (dtCCOpeningBalance.DefaultView.Count > 0)
                {
                    Rtn = UtilityMember.NumberSet.ToDouble(dtCCOpeningBalance.DefaultView[0]["OP_AMOUNT"].ToString());
                }
            }
            dtCCOpeningBalance.DefaultView.RowFilter = string.Empty;
            return Rtn;
        }

        //Opening Balance Cost Centre Category Balances
        private double GetCCCategoryOpeningBalance(Int32 CategoryCostCenterId)
        {
            double Rtn = 0;
            if (dtCCOpeningBalance != null && dtCCOpeningBalance.Rows.Count > 0)
            {
                dtCCOpeningBalance.DefaultView.RowFilter = this.ReportParameters.COST_CENTRECATEGORY_IDColumn.ColumnName + "=" + CategoryCostCenterId;
                //if (dtCCOpeningBalance.DefaultView.Count > 0)
                //{
                //    Rtn = UtilityMember.NumberSet.ToDouble(dtCCOpeningBalance.DefaultView[0]["OP_AMOUNT"].ToString());
                //}

                foreach (DataRowView drView in dtCCOpeningBalance.DefaultView)
                {
                    Rtn += UtilityMember.NumberSet.ToDouble(drView["OP_AMOUNT"].ToString());
                }
            }
            dtCCOpeningBalance.DefaultView.RowFilter = string.Empty;
            return Rtn;
        }

        private void UpdateCCOpeningBalance(Int32 CostCenterId, double CCClosingAmt)
        {
            if (dtCCOpeningBalance != null && dtCCOpeningBalance.Rows.Count > 0)
            {
                dtCCOpeningBalance.DefaultView.RowFilter = this.ReportParameters.COST_CENTRE_IDColumn.ColumnName + "=" + CostCenterId;
                if (dtCCOpeningBalance.DefaultView.Count > 0)
                {
                    dtCCOpeningBalance.DefaultView.BeginInit();
                    dtCCOpeningBalance.DefaultView[0]["OP_AMOUNT"] = CCClosingAmt;
                    dtCCOpeningBalance.DefaultView.EndInit();
                    dtCCOpeningBalance.DefaultView.Table.AcceptChanges();
                }
            }
            dtCCOpeningBalance.DefaultView.RowFilter = string.Empty;
        }


        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.GetCurrentColumnValue(this.reportSetting1.Receipts.LEDGER_IDColumn.ColumnName) != null)
            {
                Int32 ledgerid = UtilityMember.NumberSet.ToInteger(this.GetCurrentColumnValue(this.reportSetting1.Receipts.LEDGER_IDColumn.ColumnName).ToString());
                e.Cancel = (ledgerid == 0);
            }
        }

        private void xrCCTotalCr_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //On 23/03/2020, To Add negative balance CR Amount
            //double cctotal = CosCredit + (CCOPBalance<0? CCOPBalance:0);--------------------------------
            CosCreditTotal = 0;
            //On 22/12/2023, To add cc opening balnce 
            //CosCreditTotal = CosCredit; //+(CCOPBalance < 0 ? Math.Abs(CCOPBalance) : 0);
            //CosCreditTotal = CosCredit + this.ReportProperties.ShowByCostCentre == 0 ? (CCCOPBalance < 0 ? Math.Abs(CCCOPBalance) : 0) : (CCOPBalance < 0 ? Math.Abs(CCOPBalance) : 0);
            CosCreditTotal = CosCredit + (this.ReportProperties.ShowByCostCentre == 1 ? (CCOPBalance < 0 ? Math.Abs(CCOPBalance) : 0) : (CCCOPBalance < 0 ? Math.Abs(CCCOPBalance) : 0));
            CosLedgerCredit += CosCreditTotal;
            CosCreditGrandTotal += CosCreditTotal;
            //--------------------------------------------------------------------------------------------
            //xrCCTotalCr.Text = UtilityMember.NumberSet.ToNumber(Math.Abs(cctotal)).ToString();
            e.Result = UtilityMember.NumberSet.ToNumber(Math.Abs(CosCreditTotal)).ToString();
            e.Handled = true;
        }

        private void xrCCTotalDr_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            CosDebitTotal = 0;
            //On 22/12/2023, To add cc opening balnce 
            //CosDebitTotal = CosDebit; // +(CCOPBalance >= 0 ? CCOPBalance : 0); 
            // CosDebitTotal = CosDebit + this.ReportProperties.ShowByCostCentre == 0 ? (CCCOPBalance >= 0 ? CCCOPBalance : 0) : (CCOPBalance >= 0 ? CCOPBalance : 0);
            CosDebitTotal = CosDebit + (this.ReportProperties.ShowByCostCentre == 1 ? (CCOPBalance >= 0 ? CCOPBalance : 0) : (CCCOPBalance >= 0 ? CCCOPBalance : 0));
            CosLedgerDebit += CosDebitTotal;
            CosDebitGrandTotal += CosDebitTotal;
            //xrCCTotalDr.Text = UtilityMember.NumberSet.ToNumber(Math.Abs(cctotal)).ToString();

            //if (this.AppSetting.EnableCCModeReports == 1)
            //    e.Result = ((UtilityMember.NumberSet.ToNumber(Math.Abs(CosDebitTotal)).ToString()) != "0,00") ? "-" + UtilityMember.NumberSet.ToNumber(Math.Abs(CosDebitTotal)).ToString() : UtilityMember.NumberSet.ToNumber(Math.Abs(CosDebitTotal)).ToString();
            //else
            e.Result = UtilityMember.NumberSet.ToNumber(Math.Abs(CosDebitTotal)).ToString();

            e.Handled = true;


        }

        private void xrCCTotalBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //On 23/03/2020, to show proper balance
            //double crtotal = UtilityMember.NumberSet.ToDouble(xrCCTotalCr.Text);
            //double drtotal = UtilityMember.NumberSet.ToDouble(xrCCTotalDr.Text);

            //if ((drtotal - crtotal) >= 0)
            //{
            //    xrCCTotalBalance.Text = UtilityMember.NumberSet.ToNumber(drtotal - crtotal) + " Dr";
            //}
            //else
            //{
            //    xrCCTotalBalance.Text = UtilityMember.NumberSet.ToNumber(Math.Abs(drtotal - crtotal)) + " Cr";
            //}

            if ((CosDebitTotal - CosCreditTotal) >= 0)
            {
                if (this.AppSetting.EnableCCModeReports == 1)
                {
                    double calcuvalues = (CosDebitTotal - CosCreditTotal);
                    // e.Result = "-" + UtilityMember.NumberSet.ToNumber(CosDebitTotal - CosDebitTotal);

                    e.Result = ((UtilityMember.NumberSet.ToNumber(Math.Abs(calcuvalues)).ToString()) != "0,00") ? "-" + this.UtilityMember.NumberSet.ToNumber(Math.Abs(calcuvalues)).ToString() : this.UtilityMember.NumberSet.ToNumber(Math.Abs(calcuvalues)).ToString();
                }
                else
                {
                    e.Result = UtilityMember.NumberSet.ToNumber(CosDebitTotal - CosCreditTotal) + " Dr";
                }
            }
            else
            {
                if (this.AppSetting.EnableCCModeReports == 1)
                    e.Result = UtilityMember.NumberSet.ToNumber(Math.Abs(CosDebitTotal - CosCreditTotal));
                else
                    e.Result = UtilityMember.NumberSet.ToNumber(Math.Abs(CosDebitTotal - CosCreditTotal)) + " Cr";
            }
            e.Handled = true;
        }

        private void xrCCName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //On 01/04/2020, To reset debit and credit total amout
            CosDebit = CosCredit = 0;
            CosDebitTotal = CosCreditTotal = 0;
        }

        private void grpHeaderCC_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.ReportProperties.ShowByCostCentre == 1 &&
                this.GetCurrentColumnValue(this.reportSetting1.BudgetCostCentre.COST_CENTRE_IDColumn.ColumnName) != null)
            {
                Int32 ccid = UtilityMember.NumberSet.ToInteger(this.GetCurrentColumnValue(this.reportSetting1.BudgetCostCentre.COST_CENTRE_IDColumn.ColumnName).ToString());
                e.Cancel = (ccid == 0);

                if (ccid > 0)
                {
                    GroupHeaderBand grpccHeader = sender as GroupHeaderBand;
                    xrtblCCName.TopF = 5;
                    if (CCGroupNumber == 0)
                    {
                        xrtblCCName.TopF = 0;
                    }
                    grpccHeader.HeightF = 25;
                }
                CCGroupNumber++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xrCCTotalBalance_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (this.AppSetting.EnableCCModeReports == 1)
            {
                XRTableCell cell = (XRTableCell)sender;
                double Cellvalue = UtilityMember.NumberSet.ToDouble(cell.Text);
                if (Cellvalue < 0)
                {
                    cell.ForeColor = Color.Red;
                }
                else
                {
                    cell.ForeColor = Color.Black;
                }
            }
        }

        private void xrtblCosBalance_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (this.AppSetting.EnableCCModeReports == 1)
            {
                XRTableCell cell = (XRTableCell)sender;
                double Cellvalue = UtilityMember.NumberSet.ToDouble(cell.Text);
                if (Cellvalue < 0)
                {
                    cell.ForeColor = Color.Red;
                }
                else
                {
                    cell.ForeColor = Color.Black;
                }
            }
        }

        private void xrCosTransMode_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.ReportProperties.ReportId == "RPT-238" && this.DataSource != null) // 17/02/2025, Chinna
            {
                if (this.GetCurrentColumnValue(reportSetting1.Ledger.NARRATIONColumn.ColumnName) != null &&
                    !string.IsNullOrEmpty(this.GetCurrentColumnValue(reportSetting1.Ledger.NARRATIONColumn.ColumnName).ToString()))
                {
                    e.Value = this.GetCurrentColumnValue(reportSetting1.Ledger.NARRATIONColumn.ColumnName).ToString();
                }
                else
                {
                    e.Value = this.GetCurrentColumnValue(reportSetting1.Ledger.LEDGER_NAMEColumn.ColumnName).ToString();
                }
            }
        }

        private void xrOPBLDr_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (this.AppSetting.EnableCCModeReports == 1)
            {
                XRTableCell cell = (XRTableCell)sender;
                double Cellvalue = UtilityMember.NumberSet.ToDouble(cell.Text);
                if (Cellvalue < 0)
                {
                    cell.ForeColor = Color.Red;
                }
                else
                {
                    cell.ForeColor = Color.Black;
                }
            }
        }

        // Debit Side 03/03/2025
        private void xrCCCategoryOpeningIncome_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(this.ReportParameters.COST_CENTRECATEGORY_IDColumn.ColumnName) != null)
            {
                Int32 CCCId = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(this.ReportParameters.COST_CENTRECATEGORY_IDColumn.ColumnName).ToString());
                ShowCategoryCCOpeningBalance(CCCId);
            }

            //double value = this.ReportProperties.NumberSet.ToDouble(xrCCCategoryOpeningIncome.Text);
            //if (value != 0)
            //{
            //    e.Cancel = false;
            //}
            //else
            //{
            //    xrCCCategoryOpeningIncome.Text = "";
            //}
        }

        // Credit Side 03/03/2025
        private void xrCCCategoryOpeningExpense_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(this.ReportParameters.COST_CENTRECATEGORY_IDColumn.ColumnName) != null)
            {
                Int32 CCCId = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(this.ReportParameters.COST_CENTRECATEGORY_IDColumn.ColumnName).ToString());
                ShowCategoryCCOpeningBalance(CCCId);
            }

            if (this.AppSetting.EnableCCModeReports == 1)
            {
                XRTableCell cell = (XRTableCell)sender;
                double Cellvalue = UtilityMember.NumberSet.ToDouble(cell.Text);
                if (Cellvalue < 0)
                {
                    cell.ForeColor = Color.Red;
                }
                else
                {
                    cell.ForeColor = Color.Black;
                }
            }
        }

        private void xrCCGrandTotalCr_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = CosCreditGrandTotal;
            e.Handled = true;
        }

        private void xrCCGrandTotalDr_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = CosDebitGrandTotal;
            e.Handled = true;
        }

        private void xrCCGrandTotalBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double diff = CosCreditGrandTotal - CosDebitGrandTotal;
            e.Result = diff;
            e.Handled = true;
        }

        private double GetBalance(string fldname)
        {
            double rtn = 0;
            double income = 0;
            double expense = 0;
            try
            {
                if (this.DataSource != null && !string.IsNullOrEmpty(fldname) &&
                    this.GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.COST_CENTRECATEGORY_IDColumn.ColumnName) != null)
                {
                    DataTable dtRpt = this.DataSource as DataTable;
                    Int32 cccategoryid = UtilityMember.NumberSet.ToInteger(this.GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.COST_CENTRECATEGORY_IDColumn.ColumnName).ToString());
                    string filterCR = "CREDIT>0 AND " +
                                      reportSetting1.BUDGETVARIANCE.COST_CENTRECATEGORY_IDColumn.ColumnName + "=" + cccategoryid;
                    string filterDR = "DEBIT>0 AND " +
                                      reportSetting1.BUDGETVARIANCE.COST_CENTRECATEGORY_IDColumn.ColumnName + "=" + cccategoryid;

                    if (fldname == "CREDIT")
                    {
                        income = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(" + fldname + ")", filterCR).ToString());
                        rtn = income;
                    }
                    else
                    {
                        expense = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(" + fldname + ")", filterDR).ToString());
                        rtn = expense;
                    }
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
            return rtn;
        }

        private void xrCCCategoryOpeningExpense_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (this.AppSetting.EnableCCModeReports == 1)
            {
                XRTableCell cell = (XRTableCell)sender;
                double Cellvalue = UtilityMember.NumberSet.ToDouble(cell.Text);
                if (Cellvalue < 0)
                {
                    cell.ForeColor = Color.Red;
                }
                else
                {
                    cell.ForeColor = Color.Black;
                }
            }
        }

        private void grpHeaderCCCategory_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            CosCreditGrandTotal = 0;
            CosDebitGrandTotal = 0;
            CosLedgerCredit = 0;
            CosLedgerDebit = 0;
        }
    }
}

//if (IsDrillDownMode)
//{
//    if (this.ReportProperties.DrillDownProperties != null && this.ReportProperties.DrillDownProperties.Count > 0)
//    {
//        Dictionary<string, object> dicDDProperties = this.ReportProperties.DrillDownProperties;
//        DrillDownType ddtypeLinkType = DrillDownType.BASE_REPORT;
//        ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), dicDDProperties["DrillDownLink"].ToString());

//        if (dicDDProperties.ContainsKey(this.ReportParameters.COST_CENTRE_IDColumn.ColumnName))
//            CostCentreId = dicDDProperties[this.ReportParameters.COST_CENTRE_IDColumn.ColumnName].ToString();
//        if (dicDDProperties.ContainsKey(this.ReportParameters.LEDGER_IDColumn.ColumnName))
//            this.ReportProperties.Ledger = dicDDProperties[this.ReportParameters.LEDGER_IDColumn.ColumnName].ToString();
//       //this.ReportProperties.Ledger = this.ReportProperties.Ledger == "0" ? ReportProperty.Current.CostCentreLedgerId : this.ReportProperties.Ledger;
//    }
//}