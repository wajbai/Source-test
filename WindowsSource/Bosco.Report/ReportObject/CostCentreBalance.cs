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
    public partial class CostCentreBalance : ReportHeaderBase
    {
        #region Constructor
        public CostCentreBalance()
        {
            InitializeComponent();
            //this.AttachDrillDownToRecord(xrtblDetails, xrCosTransMode,
            //   new ArrayList { ReportParameters.COST_CENTRE_IDColumn.ColumnName }, DrillDownType.CC_LEDGER_SUMMARY, false, "", true);
        }
        #endregion

        #region Variables

        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindReport();
        }
        #endregion

        #region Methods
        public void BindReport()
        {
            SetTitleWidth(xrtblHeaderCaption.WidthF);
            this.SetLandscapeHeader = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooter = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooterDateWidth = xrtblHeaderCaption.WidthF;

            if ((string.IsNullOrEmpty(this.ReportProperties.DateFrom) ||
                string.IsNullOrEmpty(this.ReportProperties.DateTo) ||
                this.ReportProperties.Project == "0" || this.ReportProperties.CostCentre == "0"))
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
                        SetReportTitle();
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    BindReportProperty();
                }
            }
        }

        private void BindReportProperty()
        {
            SplashScreenManager.ShowForm(typeof(frmReportWait));
            setHeaderTitleAlignment();
            SetReportTitle();
            this.CosCenterName = ReportProperty.Current.CostCentreName;
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            ResultArgs resultArgs = BindCCBalanceSource();
            DataView dtview = resultArgs.DataSource.TableView;
            // grpCostCategoryName.Visible = false;
            Detail.Visible = false;
            grpFooterBalance.Visible = false;
            ReportFooter.Visible = false;

            if (dtview != null)
            {
                dtview.Table.TableName = "Ledger";
                this.DataSource = dtview;
                this.DataMember = dtview.Table.TableName;

                grpCostCategoryName.Visible = (dtview.Table.Rows.Count > 0);
                Detail.Visible = (dtview.Table.Rows.Count > 0);
                grpFooterBalance.Visible = (dtview.Table.Rows.Count > 0);
                ReportFooter.Visible = (dtview.Table.Rows.Count > 0);
            }
            grpCostCategoryName.Visible = grpFooterBalance.Visible = (this.ReportProperties.ShowByCostCentreCategory == 1);

            SetReportBorder();

            //On 26/03/2020, to show/hide cc opening Balance based on setting 
            if (AppSetting.ShowCCOpeningBalanceInReports == 0)
            {
                xrtblCCCName.SuspendLayout();
                if (xrRowHeader.Cells.Contains(xrCosCapOP))
                    xrRowHeader.Cells.Remove(xrRowHeader.Cells[xrCosCapOP.Name]);
                xrtblCCCName.PerformLayout();

                xrtblDetails.SuspendLayout();
                if (xrRowData.Cells.Contains(xrCellOPAmount))
                    xrRowData.Cells.Remove(xrRowData.Cells[xrCellOPAmount.Name]);
                xrtblDetails.PerformLayout();

                xrCellTotalCaption.WidthF = xrCellGrandTotalCaption.WidthF = xrCostCentreName.WidthF;
                xrCellTotalSumDr.WidthF = xrCellGrandSumDr.WidthF = xrCosCapDebit.WidthF;
                xrCellTotalSumCr.WidthF = xrCellGrandSumCr.WidthF = xrCosCapCredit.WidthF;
                xrCellGrpCLAmount.WidthF = xrCellGrandCLAmount.WidthF = xrCosCapclosingBalance.WidthF;
            }

            SplashScreenManager.CloseForm();
            base.ShowReport();
        }

        public ResultArgs BindCCBalanceSource()
        {
            ResultArgs resultArgs = null;

            ReportProperty.Current.CostCentreLedgerId = this.ReportProperties.Ledger;
            string Query = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCentreBalanceStatement);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.Ledger);
                dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre != null ? this.ReportProperties.CostCentre : "0");
                dataManager.Parameters.Add(this.ReportParameters.SHOW_OP_BALANCEColumn, AppSetting.ShowCCOpeningBalanceInReports);
                dataManager.Parameters.Add(this.ReportParameters.COSTCENTRE_MAPPINGColumn, settingProperty.CostCeterMapping);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, Query);
            }
            return resultArgs;
        }

        private void SetReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrtblCCCName = AlignCCCategoryTable(xrtblCCCName);
            xrtblDetails = AlignContentTable(xrtblDetails);
            xrtblTotal = AlignGrandTotalTable(xrtblTotal);
            xrtblGrandTotal = AlignTotalTable(xrtblGrandTotal);

            this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.DEBIT, xrCosCapDebit);
            this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.CREDIT, xrCosCapCredit);
            this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.CLOSINGBALANCE, xrCosCapclosingBalance);
            this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.OPBALANCE, xrCosCapOP);

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
        #endregion

        private void xrCellOPAmount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 10/02/2025,Chinna, to update the OP Balances
            if (GetCurrentColumnValue("OP_AMOUNT") != null)
            {
                double opamount = this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("OP_AMOUNT").ToString());
                if (opamount < 0)
                {
                    if (this.AppSetting.EnableCCModeReports == 1)
                        ((XRTableCell)sender).Text = this.UtilityMember.NumberSet.ToNumber(Math.Abs(opamount));
                    else
                        ((XRTableCell)sender).Text = this.UtilityMember.NumberSet.ToNumber(Math.Abs(opamount)) + " Cr";
                }
                else
                {
                    if (this.AppSetting.EnableCCModeReports == 1) // Need to check
                        ((XRTableCell)sender).Text = (((XRTableCell)sender).Text != "0,00") ? "-" + ((XRTableCell)sender).Text : ((XRTableCell)sender).Text;
                    else
                        ((XRTableCell)sender).Text += " Dr";
                }
            }
        }

        private void xrtblCosClosingBalance_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("CLOSING_BALANCE") != null)
            {
                double closingamount = this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("CLOSING_BALANCE").ToString());
                if (closingamount < 0)
                {
                    if (this.AppSetting.EnableCCModeReports == 1)
                        ((XRTableCell)sender).Text = this.UtilityMember.NumberSet.ToNumber(Math.Abs(closingamount));
                    else
                        ((XRTableCell)sender).Text = this.UtilityMember.NumberSet.ToNumber(Math.Abs(closingamount)) + " Cr";
                }
                else
                {
                    if (this.AppSetting.EnableCCModeReports == 1) // Need to check
                        ((XRTableCell)sender).Text = (((XRTableCell)sender).Text != "0,00") ? "-" + ((XRTableCell)sender).Text : ((XRTableCell)sender).Text;
                    else
                        ((XRTableCell)sender).Text += " Dr";
                }
            }
        }

        //private void xrCellGrpCLTransMode_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    double closingamountgrand = this.UtilityMember.NumberSet.ToDouble(xrCellGrpCLAmount.Text);
        //    if (closingamountgrand < 0)
        //    {
        //        ((XRTableCell)sender).Text = "Cr";
        //    }
        //    else
        //    {
        //        ((XRTableCell)sender).Text = "Dr";
        //    }
        //}

        private void xrTableCell4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double opamount = this.UtilityMember.NumberSet.ToDouble(((XRTableCell)sender).Text);
            if (opamount < 0)
            {
                ((XRTableCell)sender).Text = "Cr";
            }
            else
            {
                ((XRTableCell)sender).Text = "Dr";
            }
        }


        private void xrCellGrandCLAmount_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            double closinggrandamount = this.UtilityMember.NumberSet.ToDouble(((XRTableCell)sender).Text);
            if (closinggrandamount < 0)
            {
                if (this.AppSetting.EnableCCModeReports == 1)
                    ((XRTableCell)sender).Text = UtilityMember.NumberSet.ToNumber(Math.Abs(closinggrandamount));
                else
                    ((XRTableCell)sender).Text = UtilityMember.NumberSet.ToNumber(Math.Abs(closinggrandamount)) + " Cr";
            }
            else
            {
                if (this.AppSetting.EnableCCModeReports == 1)
                    ((XRTableCell)sender).Text = ((XRTableCell)sender).Text != "0,00" ? "-" + ((XRTableCell)sender).Text : ((XRTableCell)sender).Text;
                else
                    ((XRTableCell)sender).Text += " Dr";
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

        private void xrCellGrpCLAmount_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            double closinggrandamount = this.UtilityMember.NumberSet.ToDouble(((XRTableCell)sender).Text);
            if (closinggrandamount < 0)
            {
                if (this.AppSetting.EnableCCModeReports == 1)
                    ((XRTableCell)sender).Text = UtilityMember.NumberSet.ToNumber(Math.Abs(closinggrandamount));
                else
                    ((XRTableCell)sender).Text = UtilityMember.NumberSet.ToNumber(Math.Abs(closinggrandamount)) + " Cr";
            }
            else
            {
                if (this.AppSetting.EnableCCModeReports == 1)
                    ((XRTableCell)sender).Text = (((XRTableCell)sender).Text != "0,00") ? "-" + ((XRTableCell)sender).Text : ((XRTableCell)sender).Text;
                else
                    ((XRTableCell)sender).Text += " Dr";
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

        private void xrtblCosClosingBalance_PrintOnPage(object sender, PrintOnPageEventArgs e)
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

        private void xrCellGrandSumDr_PrintOnPage(object sender, PrintOnPageEventArgs e)
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

        private void xrCellTotalSumDr_PrintOnPage(object sender, PrintOnPageEventArgs e)
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

        private void xrCellOPAmount_PrintOnPage(object sender, PrintOnPageEventArgs e)
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
