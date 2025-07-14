using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility.ConfigSetting;
using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.Report.View;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraPrinting;


namespace Bosco.Report.ReportObject
{
    public partial class ccIncomeandExpenditure : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public ccIncomeandExpenditure()
        {
            InitializeComponent();
        }
        #endregion

        #region Variable Declaration
        double ExpenditureAmount = 0;
        double IncomeAmount = 0;
        Receipts receiptsLedger;
        Payments PaymentsLedger;
        //double ExcessIncome = 0;
        //double ExcessExpenditure = 0;
        //private string EXCESSOFINCOME = "Excess Of Expenditure Over Income";
        //private string EXCESSOFEXPENDITURE = "Excess of Income Over Expenditure";
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindCCIncomeExpenditureSource();

        }
        #endregion

        #region Method
        private void BindCCIncomeExpenditureSource()
        {
            try
            {
                // this.ReportPeriod = String.Format(MessageCatalog.ReportCommonTitle.PERIOD + " {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                setReportBorder();
                this.CosCenterName = ReportProperty.Current.CostCentreName;
                setHeaderTitleAlignment();
                SetReportTitle();
                this.ReportTitle = this.ReportProperties.ReportTitle;
                PaymentsLedger = xrccSubpayments.ReportSource as Payments;
                PaymentsLedger.HidePaymentReportHeader();
                receiptsLedger = xrccSubReceipts.ReportSource as Receipts;
                receiptsLedger.HideReceiptReportHeader();
                if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo)
                                || this.ReportProperties.Project == "0" || string.IsNullOrEmpty(this.ReportProperties.CostCentre) || this.ReportProperties.CostCentre == "0")
                {
                    ShowReportFilterDialog();
                }
                else
                {
                    if (this.UIAppSetting.UICustomizationForm == "1")
                    {
                        if (ReportProperty.Current.ReportFlag == 0)
                        {
                            SplashScreenManager.ShowForm(typeof(frmReportWait));

                            SetReporProperties();

                            SetReportSetup();
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

                        SetReporProperties();

                        SetReportSetup();
                        SplashScreenManager.CloseForm();
                        base.ShowReport();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void SetReporProperties()
        {
            this.AttachDrillDownToSubReport(PaymentsLedger);
            PaymentsLedger.BindPaymentSource(TransType.CEXP);
            ExpenditureAmount = PaymentsLedger.PaymentAmout;

            this.AttachDrillDownToSubReport(receiptsLedger);
            receiptsLedger.BindReceiptSource(TransType.CINC);
            IncomeAmount = receiptsLedger.ReceiptAmount;

            float ReceiptCodeWidth = xrccCode.WidthF;
            if (xrccCode.Tag != null && xrccCode.Tag.ToString() != "")
            {
                ReceiptCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrccCode.Tag.ToString());
            }
            else
            {
                xrccCode.Tag = xrccCode.WidthF;
            }

            float ReceiptNameWidth = xtCapExpenditure.WidthF;
            if (xtCapExpenditure.Tag != null && xtCapExpenditure.Tag.ToString() != "")
            {
                ReceiptNameWidth = (float)this.UtilityMember.NumberSet.ToDouble(xtCapExpenditure.Tag.ToString());
            }
            else
            {
                xtCapExpenditure.Tag = xtCapExpenditure.WidthF;
            }

            float ReceiptAmountWidth = xrRecptAmount.WidthF;
            if (xrRecptAmount.Tag != null && xrRecptAmount.Tag.ToString() != "")
            {
                ReceiptAmountWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrRecptAmount.Tag.ToString());
            }
            else
            {
                xrRecptAmount.Tag = xrRecptAmount.WidthF;
            }

            if (ReportProperties.ShowLedgerCode == 1)
            {
                PaymentsLedger.CodeColumnWidth = ReceiptCodeWidth;
                PaymentsLedger.NameColumnWidth = ReceiptNameWidth;
                PaymentsLedger.AmountColumnWidth = ReceiptAmountWidth;

                if (ReportProperties.ShowGroupCode == 1)
                {
                    PaymentsLedger.NameColumnWidth = ReceiptNameWidth;
                }

                PaymentsLedger.CategoryNameWidth = ReceiptCodeWidth + ReceiptNameWidth;
                PaymentsLedger.CostCentreWidth = ReceiptCodeWidth + ReceiptNameWidth;
                PaymentsLedger.CostCategoryAmountWidth = ReceiptAmountWidth;
                PaymentsLedger.CostCentreAmountWidth = ReceiptAmountWidth;

                receiptsLedger.CodeColumnWidth = ReceiptCodeWidth - 2;
                receiptsLedger.NameColumnWidth = ReceiptNameWidth + 1;
                receiptsLedger.AmountColumnWidth = ReceiptAmountWidth;// +9;

                receiptsLedger.CostCentreWidth = ReceiptCodeWidth + ReceiptNameWidth - 1;
                receiptsLedger.CostCentreCategoryNameWidth = ReceiptCodeWidth + ReceiptNameWidth - 1;
                receiptsLedger.CostCategoryAmountWidth = ReceiptAmountWidth;// +9;
                receiptsLedger.CostCentreAmountWidth = ReceiptAmountWidth;// +9;
            }
            else
            {
                PaymentsLedger.CodeColumnWidth = 0;
                PaymentsLedger.NameColumnWidth = ReceiptCodeWidth + ReceiptNameWidth - 2;
                PaymentsLedger.AmountColumnWidth = ReceiptAmountWidth;
                PaymentsLedger.CategoryNameWidth = ReceiptCodeWidth + ReceiptNameWidth;
                PaymentsLedger.CostCentreWidth = ReceiptCodeWidth + ReceiptNameWidth;
                PaymentsLedger.CostCategoryAmountWidth = ReceiptAmountWidth;
                PaymentsLedger.CostCentreAmountWidth = ReceiptAmountWidth;

                receiptsLedger.CodeColumnWidth = 0;
                receiptsLedger.NameColumnWidth = ReceiptCodeWidth + ReceiptNameWidth - 3;
                receiptsLedger.AmountColumnWidth = ReceiptAmountWidth;// +9;
                receiptsLedger.CostCentreCategoryNameWidth = ReceiptCodeWidth + ReceiptNameWidth - 1;
                receiptsLedger.CostCentreWidth = ReceiptCodeWidth + ReceiptNameWidth - 1;
                receiptsLedger.CostCategoryAmountWidth = ReceiptAmountWidth;// +9;
                receiptsLedger.CostCentreAmountWidth = ReceiptAmountWidth;// +9;
            }
            if (ReportProperties.ShowGroupCode == 1)
            {
                PaymentsLedger.GroupCodeColumnWidth = ReceiptCodeWidth;
                PaymentsLedger.GroupNameColumnWidth = ReceiptNameWidth;
                PaymentsLedger.GroupAmountColumnWidth = ReceiptAmountWidth;

                receiptsLedger.GroupCodeColumnWidth = ReceiptCodeWidth - 2;
                receiptsLedger.GroupNameColumnWidth = ReceiptNameWidth + 1;
                receiptsLedger.GroupAmountColumnWidth = ReceiptAmountWidth;// +9;
            }
            else
            {
                PaymentsLedger.GroupCodeColumnWidth = 0;
                PaymentsLedger.GroupNameColumnWidth = ReceiptCodeWidth + ReceiptNameWidth - 2;
                PaymentsLedger.GroupAmountColumnWidth = ReceiptAmountWidth;

                receiptsLedger.GroupCodeColumnWidth = 0;
                receiptsLedger.GroupNameColumnWidth = ReceiptCodeWidth + ReceiptNameWidth - 3;
                receiptsLedger.GroupAmountColumnWidth = ReceiptAmountWidth;// +9;
            }
        }

        private void SetReportSetup()
        {
            float actualCodeWidth = xrccCode.WidthF;
            //Include / Exclude Code
            if (xrccCode.Tag != null && xrccCode.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrccCode.Tag.ToString());
            }
            else
            {
                xrccCode.Tag = xrccCode.WidthF;
            }
            if (receiptsLedger.HideCostCentreReceipts != true && PaymentsLedger.HideCostCentrePayments != true)
            {
                this.CosCenterName = ReportProperty.Current.CostCentreName;
            }
            else
            {
                this.CosCenterName = string.Empty;
            }
            if (ReportProperties.ShowLedgerCode == 1)
            {
                xrccCode.WidthF = actualCodeWidth;
                xrPaymtCode.WidthF = actualCodeWidth;
                xrccCode.Text = "Code";
                xrPaymtCode.Text = "Code";
            }
            else
            {
                xrccCode.WidthF = 0;
                xrPaymtCode.WidthF = 0;
                xrccCode.Text = string.Empty;
                xrPaymtCode.Text = string.Empty;
            }
            setReportBorder();
        }

        private void setReportBorder()
        {
            xrtblHeaderOption = AlignHeaderTable(xrtblHeaderOption);
            xrTblTotal = AlignHeaderTable(xrTblTotal);
            xrCrossBandLine1.ForeColor = xrCrossBandLine2.ForeColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
            this.SetCurrencyFormat(xrPaymtAmount.Text, xrPaymtAmount);
            this.SetCurrencyFormat(xrRecptAmount.Text, xrRecptAmount);
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
                        else if (count == 4)
                        {
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
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
                        if (count == 4 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else if (count == 4 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                    tcell.Font = (ReportProperty.Current.ColumnCaptionFontStyle == 0 ? FieldColumnHeaderFont : new Font(FieldColumnHeaderFont, FontStyle.Regular));
                }
            }
            return table;
        }

        private void setHeaderTableBorder()
        {
            foreach (XRTableRow trow in xrtblHeaderOption.Rows)
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
                        else if (count == 4)
                        {
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
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
                        if (count == 4 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else if (count == 4 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                }
            }
        }

        #endregion

        private void xrccPaymentAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = ExpenditureAmount;
            e.Handled = true;
        }

        private void xrccReceiptAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = IncomeAmount;
            e.Handled = true;
        }
    }
}
