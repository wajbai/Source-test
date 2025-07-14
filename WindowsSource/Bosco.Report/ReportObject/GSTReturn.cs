using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.DAO.Data;
using DevExpress.XtraRichEdit;
using System.Data;

namespace Bosco.Report.ReportObject
{
    public partial class GSTReturn : Bosco.Report.Base.ReportHeaderBase
    {
        public GSTReturn()
        {
            InitializeComponent();
        }

        double TaxableGST = 0;

        #region Show Reports
        public override void ShowReport()
        {
            ShowGSTIncomeExpense();
        }
        #endregion

        public void ShowGSTIncomeExpense()
        {
            this.SetLandscapeBudgetNameWidth = xrtblHeaderCaption.WidthF;
            this.SetLandscapeHeader = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooter = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooterDateWidth = xrtblHeaderCaption.WidthF;
            setHeaderTitleAlignment();

            Detail.SortFields.Add(new GroupField(reportSetting1.GSTReturn.VENDORColumn.ColumnName, XRColumnSortOrder.Ascending));
            Detail.SortFields.Add(new GroupField(reportSetting1.GSTReturn.GST_VENDOR_INVOICE_NOColumn.ColumnName , XRColumnSortOrder.Ascending));

            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo) || string.IsNullOrEmpty(this.ReportProperties.Project))
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
                        BindGSTReturn();
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
                    BindGSTReturn();
                    base.ShowReport();
                }
            }
        }

        private void BindGSTReturn()
        {
            try
            {
                setHeaderTitleAlignment();
                SetReportTitle();
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                int DefaultGSTclassid = this.AppSetting.GSTZeroClassId;
                
                //For GST Inwards and OutWards
                string GSTdetailsSQL = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.GSTReturn);

                //for GST Exemption Invoices
                if (ReportProperties.ReportId == "RPT-213")
                {
                    GSTdetailsSQL = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.GSTExemptionInvoices);
                }

                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(reportSetting1.GSTReturn.LEDGER_GST_CLASS_IDColumn, DefaultGSTclassid);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    ResultArgs resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, GSTdetailsSQL);
                                        
                    SetReportBorder();
                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    //if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        DataTable dtGSTReturn = resultArgs.DataSource.Table;
                        
                        //GST OutWards
                        if (ReportProperties.ReportId == "RPT-213")
                        {
                            dtGSTReturn.DefaultView.RowFilter = "";
                        }
                        else if (ReportProperties.ReportId == "RPT-166")
                        {
                            dtGSTReturn.DefaultView.RowFilter = this.ReportParameters.VOUCHER_TYPEColumn.ColumnName + " IN ('RC', 'JN')";
                        }
                        else //For GST Inwards RPT-181
                        {
                            dtGSTReturn.DefaultView.RowFilter = this.ReportParameters.VOUCHER_TYPEColumn.ColumnName + " IN ('PY', 'JN')";
                        }


                        this.DataSource = dtGSTReturn;
                        this.DataMember = resultArgs.DataSource.Table.TableName;
                    }


                    xrLblVoucherType.Visible = (resultArgs.DataSource.Table.Rows.Count > 0);

                    Detail.SortFields.Add(new GroupField("GST_VENDOR_INVOICE_DATE", XRColumnSortOrder.Ascending));
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, false);
            }
            finally { }
        }

        private void SetReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrtblDetails = AlignContentTable(xrtblDetails);
            xrtblGrandTotal = AlignGroupTable(xrtblGrandTotal);
        }

        private void xrLblVoucherType_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel lblVoucherType = (sender as XRLabel);

            if (ReportProperties.ReportId != "RPT-213")
            {
                if (lblVoucherType.Text.ToUpper() == VoucherSubTypes.RC.ToString())
                {
                    lblVoucherType.Text = "GSTR-1 (Outwards)";
                }
                else
                {
                    lblVoucherType.Text = "GSTR-2 (Inwards)";
                }
            }
            else
            {
                lblVoucherType.Text = "List of GST Exemption Invoices";
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void xrVendorName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //var xrTableCell = (XRTableCell)sender as XRTableCell;
            //var xrTableCellText = xrTableCell.Text;
            //xrTableCell.Controls.Clear();
            //RichEditDocumentServer server = new RichEditDocumentServer();
            //server.HtmlText = xrTableCellText;
            //server.Document.DefaultCharacterProperties.FontName = xrTableCell.Font.Name;
            //server.Document.DefaultCharacterProperties.FontSize = xrTableCell.Font.Size;
            //XRRichText rich = new XRRichText
            //{
            //    Borders = DevExpress.XtraPrinting.BorderSide.None,
            //    WidthF = xrTableCell.WidthF - xrTableCell.Padding.Left - xrTableCell.Padding.Right,
            //    HeightF = xrTableCell.HeightF,
            //    CanGrow = true,
            //    Rtf = server.RtfText
            //};
            //xrTableCell.Controls.Add(rich);
            //xrTableCell.Text = null;
        }

        private void xrGSTRate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            if (!string.IsNullOrEmpty(cell.Text))
            {
                cell.Text = this.UtilityMember.NumberSet.ToDouble(cell.Text).ToString() + "%";
            }
        }

        private void xrTaxable_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
          
        }

        private void grpHeaderVoucherType_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TaxableGST = 0;
            GroupHeaderBand groupBand = sender as GroupHeaderBand;
            if (groupBand.Index == 0)
            {
                xrLblVoucherType.TopF = 0;
                xrtblHeaderCaption.TopF = (xrLblVoucherType.TopF + xrLblVoucherType.HeightF);
                groupBand.HeightF = xrtblHeaderCaption.TopF + xrtblHeaderCaption.HeightF;
            }
        }

        private void xrGrandTotalTaxAmt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }

        private void xrAmount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
                XRTableCell cell = sender as XRTableCell;
                cell.Text = string.Empty;
                
                if (GetCurrentColumnValue(reportSetting1.Transaction.AMOUNTColumn.ColumnName) != null)
                {
                    double invoiceamt = this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.Transaction.AMOUNTColumn.ColumnName).ToString());
                    if (invoiceamt > 0)
                    {
                        double gstamt = this.UtilityMember.NumberSet.ToDouble(xrCGST.Text) +
                                         this.UtilityMember.NumberSet.ToDouble(xrSGST.Text) +
                                         this.UtilityMember.NumberSet.ToDouble(xrIGST.Text);
                        cell.Text = this.UtilityMember.NumberSet.ToNumber(invoiceamt + gstamt);
                        TaxableGST += invoiceamt + gstamt;
                    }
                }
        }

        private void xrGrandTotalAmt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.Text = string.Empty;
            if (TaxableGST > 0)
            {
                cell.Text = this.UtilityMember.NumberSet.ToNumber(TaxableGST);
            }
        }
    }

}
