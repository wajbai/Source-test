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
    public partial class CCConsolidatedStatement : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public CCConsolidatedStatement()
        {
            InitializeComponent();
        }
        #endregion

        #region Variables
        ResultArgs resultArgs = null;
        double LedgerDebit = 0;
        double LedgerCredit = 0;
        double LedgerOPDebit = 0;
        double LedgerOPCredit = 0;
        int GrpNumber = 0;
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            LedgerDebit = 0;
            LedgerCredit = 0;
            xrtblClosingBalance.Text = "";
            BindConsolidatedStatement();
        }
        #endregion

        #region Methods
        private void BindConsolidatedStatement()
        {
            SetTitleWidth(xrtblHeaderCaption.WidthF);
            this.SetLandscapeHeader = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooter = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooterDateWidth = xrtblHeaderCaption.WidthF;
            GrpNumber = 0;
            if (!string.IsNullOrEmpty(this.ReportProperties.DateFrom) || !string.IsNullOrEmpty(this.ReportProperties.DateTo) || this.ReportProperties.Project != "0")
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        setHeaderTitleAlignment();
                        SetReportTitle();
                        this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                        this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                        this.CosCenterName = null;

                        //xrPageBreak1.Visible = (this.ReportProperties.BreakByLedger == 1) ? true : false;

                        if (this.ReportProperties.ShowByCostCentreCategory == 1)
                        {
                            GrpCostCentreCategoryName.Visible = true;
                        }
                        else
                        {
                            GrpCostCentreCategoryName.Visible = GroupHeader1.Visible = false;
                            grpHeaderLedgerName.Visible = Detail.Visible = grpFooterLedgerName.Visible = true;
                        }

                        if (GrpCostCentreCategoryName.GroupFields.Count > 0)
                        {
                            GrpCostCentreCategoryName.GroupFields[0].FieldName = string.Empty;
                            if (GrpCostCentreCategoryName.Visible)
                            {
                                GrpCostCentreCategoryName.GroupFields[0].FieldName = reportSetting1.Receipts.COST_CENTRE_CATEGORY_NAMEColumn.ColumnName;
                                GrpCostCentreCategoryName.GroupFields[0].SortOrder = XRColumnSortOrder.Ascending;
                            }
                        }

                        if (this.ReportProperties.ShowByCostCentre == 1)
                        {
                            grpHeaderCCName.Visible = grpFooterCCName.Visible = true;
                            grpHeaderCCName.GroupFields[0].FieldName = reportSetting1.Ledger.PARTICULARSColumn.ColumnName;
                        }
                        else
                        {
                            grpHeaderCCName.GroupFields[0].FieldName = string.Empty;
                            grpHeaderCCName.Visible = grpFooterCCName.Visible = false;
                        }

                        // To show by costcentre ends
                        resultArgs = GetReportSource();
                        DataView dvCashFlow = resultArgs.DataSource.TableView;
                        if (dvCashFlow != null && dvCashFlow.Count != 0)
                        {
                            dvCashFlow.Table.TableName = "Ledger";
                            this.DataSource = dvCashFlow;
                            this.DataMember = dvCashFlow.Table.TableName;
                        }
                        else
                        {
                            this.DataSource = null;
                        }
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
                    setHeaderTitleAlignment();
                    SetReportTitle();
                    this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                    this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                    this.CosCenterName = null;

                    //xrPageBreak1.Visible = (this.ReportProperties.BreakByLedger == 1) ? true : false;

                    // To show by costcentre starts
                    //if (this.ReportProperties.ShowByCostCentreCategory == 1 && this.ReportProperties.ShowByCostCentre == 1)
                    //{
                    //    GrpCostCentreCategoryName.Visible = Detail.Visible = GroupFooter1.Visible = true;
                    //    GroupHeader1.Visible = false;
                    //}
                    //else if (this.ReportProperties.ShowByCostCentre == 1)
                    //{
                    //    GrpCostCentreCategoryName.Visible = GroupHeader1.Visible = false;
                    //    grpLedgerName.Visible = Detail.Visible = GroupFooter1.Visible = true;
                    //}
                    //GroupHeader1.Visible = false;
                    if (this.ReportProperties.ShowByCostCentreCategory == 1)
                    {
                        GrpCostCentreCategoryName.Visible = true;
                        // Detail.Visible = GroupFooter1.Visible = grpLedgerName.Visible = GrpCostCentreCategoryName.Visible = GroupHeader1.Visible == true ? false : true;
                    }
                    else
                    {
                        GrpCostCentreCategoryName.Visible = GroupHeader1.Visible = false;
                        grpHeaderLedgerName.Visible = Detail.Visible = grpFooterLedgerName.Visible = true;
                    }

                    if (GrpCostCentreCategoryName.GroupFields.Count > 0)
                    {
                        GrpCostCentreCategoryName.GroupFields[0].FieldName = string.Empty;
                        if (GrpCostCentreCategoryName.Visible)
                        {
                            GrpCostCentreCategoryName.GroupFields[0].FieldName = reportSetting1.Receipts.COST_CENTRE_CATEGORY_NAMEColumn.ColumnName;
                            GrpCostCentreCategoryName.GroupFields[0].SortOrder = XRColumnSortOrder.Ascending;
                        }
                    }

                    if (this.ReportProperties.ShowByCostCentre == 1)
                    {
                        grpHeaderCCName.Visible = grpFooterCCName.Visible = true;
                        grpHeaderCCName.GroupFields[0].FieldName = reportSetting1.Ledger.PARTICULARSColumn.ColumnName;
                    }
                    else
                    {
                        grpHeaderCCName.GroupFields[0].FieldName = string.Empty;
                        grpHeaderCCName.Visible = grpFooterCCName.Visible = false;
                    }

                    resultArgs = GetReportSource();
                    DataView dvCashFlow = resultArgs.DataSource.TableView;
                    if (dvCashFlow != null && dvCashFlow.Count != 0)
                    {
                        dvCashFlow.Table.TableName = "Ledger";
                        this.DataSource = dvCashFlow;
                        this.DataMember = dvCashFlow.Table.TableName;
                    }
                    else
                    {
                        this.DataSource = null;
                    }
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            setReportBorder();
        }

        private void setReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrtblDetails = AlignContentTable(xrtblDetails);
            xrtblGrandTotal = AlignTotalTable(xrtblGrandTotal);
            xrtblLedger = AlignCCCategoryTable(xrtblLedger);
            xrtblCCCName = AlignCCCategoryTable(xrtblCCCName);
            xrTblCC = AlignTotalTable(xrTblCC);
            this.SetCurrencyFormat(xrCapDebit.Text, xrCapDebit);
            this.SetCurrencyFormat(xrCapCredit.Text, xrCapCredit);
            this.SetCurrencyFormat(xrCapclosingBalance.Text, xrCapclosingBalance);
            grpFooterLedgerName.PageBreak = DevExpress.XtraReports.UI.PageBreak.None;
            if ((this.ReportProperties.BreakByLedger == 1))
            {
                grpFooterLedgerName.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
            }
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
                            if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                        {
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                        }
                        else if (count == 1)
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
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                        {
                            tcell.Borders = BorderSide.Left;
                        }
                        else if (count == 1)
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
        private ResultArgs GetReportSource()
        {
            try
            {
                string CostCentreConsolidatedStat = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCentreConsolidatedStatement);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre != null ? this.ReportProperties.CostCentre : "0");
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, CostCentreConsolidatedStat);
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
        private void xrLedgerDebitBalance_SummaryRowChanged(object sender, EventArgs e)
        {
            LedgerDebit += (GetCurrentColumnValue(this.LedgerParameters.DEBITColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.LedgerParameters.DEBITColumn.ColumnName).ToString());
            LedgerCredit += (GetCurrentColumnValue(this.LedgerParameters.CREDITColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.LedgerParameters.CREDITColumn.ColumnName).ToString());
            LedgerOPDebit = LedgerOPCredit = 0;
            if (settingProperty.ShowCCOpeningBalanceInReports == 1)
            {
                LedgerOPDebit = (GetCurrentColumnValue(reportSetting1.Ledger.OP_DRColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.Ledger.OP_DRColumn.ColumnName).ToString());
                LedgerOPCredit = (GetCurrentColumnValue(reportSetting1.Ledger.OP_CRColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.Ledger.OP_CRColumn.ColumnName).ToString());
            }
            double clamt = ((LedgerDebit + LedgerOPDebit) - (LedgerCredit + LedgerOPCredit));

            // 10/02/2025, chinna, CR -, DR+
            if (clamt < 0)
            {
                string amt = this.UtilityMember.NumberSet.ToNumber(Math.Abs(clamt)).ToString();
                if (this.AppSetting.EnableCCModeReports == 1)
                    xrtblGrandClBal.Text = xrtblClosingBalance.Text = amt;
                else
                    xrtblGrandClBal.Text = xrtblClosingBalance.Text = amt + " Cr";
            }
            else
            {
                string amt = this.UtilityMember.NumberSet.ToNumber(Math.Abs(clamt)).ToString();

                if (this.AppSetting.EnableCCModeReports == 1)
                    xrtblGrandClBal.Text = xrtblClosingBalance.Text = (amt != "0,00") ? ("-" + amt) : amt;
                else
                    xrtblGrandClBal.Text = xrtblClosingBalance.Text = amt + " Dr";
            }

            //if (clamt < 0)
            //{
            //    string amt = this.UtilityMember.NumberSet.ToNumber(Math.Abs(clamt)).ToString();
            //    xrtblGrandClBal.Text = xrtblClosingBalance.Text = amt + " Cr";
            //}
            //else
            //{
            //    string amt = this.UtilityMember.NumberSet.ToNumber(Math.Abs(clamt)).ToString();
            //    xrtblGrandClBal.Text = xrtblClosingBalance.Text = amt + " Dr";
            //}

        }

        private void xrLedgerDebitBalance_SummaryReset(object sender, EventArgs e)
        {
            LedgerDebit = LedgerCredit = LedgerOPDebit = LedgerOPCredit = 0;

        }
        #endregion

        private void xrLedgerDebitBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = LedgerDebit + LedgerOPDebit;
            e.Handled = true;
        }

        private void xrDebit_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (e.Value != null && UtilityMember.NumberSet.ToDecimal(e.Value.ToString()) == 0)
            {
                e.Value = "";
            }
        }

        private void xrCrdit_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (e.Value != null && UtilityMember.NumberSet.ToDecimal(e.Value.ToString()) == 0)
            {
                e.Value = "";
            }
        }

        private void xrcellLedgerOPDr_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (e.Value != null && UtilityMember.NumberSet.ToDecimal(e.Value.ToString()) == 0)
            {
                e.Value = "";
            }
        }

        private void xrcellLedgerOPCr_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (e.Value != null && UtilityMember.NumberSet.ToDecimal(e.Value.ToString()) == 0)
            {
                e.Value = "";
            }
        }

        private void xrLedgerCreditBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = LedgerCredit + LedgerOPCredit;
            e.Handled = true;
        }

        private void grpHeaderLedgerName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GrpNumber == 0 || this.ReportProperties.BreakByLedger == 1)
            {
                GroupHeaderBand grpHeader = sender as GroupHeaderBand;
                xrtblLedger.TopF = 0;
                grpHeader.HeightF = 53;
            }
            else
            {
                xrtblLedger.TopF = 2;
            }
            GrpNumber++;
        }

        private void xrtblLedger_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {


        }

        private void xrRowLedgerOpBalance_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableRow rowLedgerOP = sender as XRTableRow;
            rowLedgerOP.Visible = (settingProperty.ShowCCOpeningBalanceInReports == 1);
        }

        private void xrtblGrandClBal_PrintOnPage(object sender, PrintOnPageEventArgs e)
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

        private void xrtblClosingBalance_PrintOnPage(object sender, PrintOnPageEventArgs e)
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

        private void xrcellLedgerOPDr_PrintOnPage(object sender, PrintOnPageEventArgs e)
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

        private void xrTableCell13_PrintOnPage(object sender, PrintOnPageEventArgs e)
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

    }
}
