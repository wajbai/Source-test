using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using Bosco.Report.Base;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class FDRegister : ReportHeaderBase
    {
        #region Declaration
        List<Tuple<String, String, Int32>> lstcolumnwidth = new List<Tuple<String, String, Int32>>();
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public FDRegister()
        {
            InitializeComponent();

            //To have real caption on the header amount
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                xrAccumulatedInterest.Tag = xrAccumulatedInterest.Text;
                xrIntReceived.Tag = xrIntReceived.Text;
                xrPrincipalAmount.Tag = xrPrincipalAmount.Text;
                xrReInvestAmount.Tag = xrReInvestAmount.Text;
                xrTDSAmount.Tag = xrTDSAmount.Text;
                xrMat.Tag = xrMat.Text;
                xrTotalAmount.Tag = xrTotalAmount.Text;
                xrWithdrawAmount.Tag = xrWithdrawAmount.Text;
                xrClosingBalance.Tag = xrClosingBalance.Text;
            }
            //On 27/08/2020
            ArrayList arraylst = new ArrayList { this.ReportParameters.FD_ACCOUNT_IDColumn.ColumnName };
            this.AttachDrillDownToRecord(xrtblFdRegister, xrFDAcNoValue,arraylst,
                        DrillDownType.LEDGER_VOUCHER, false, "VOUCHER_SUB_TYPE");



            //On 06/10/2017, Show "Investment Name" column only for SDB congiration alone
            //if (this.AppSetting.IS_SDB_CONGREGATION == false)
            //{
            //    xrtblHeaderTable.DeleteColumn(xrInvestName);
            //    xrtblFdRegister.DeleteColumn(xrInvestNameValue);
            //}

            //PrintingSystem.AfterMarginsChange += new MarginsChangeEventHandler(PrintingSystem_AfterMarginsChange);
            //PrintingSystem.PageSettingsChanged += new EventHandler(PrintingSystem_PageSettingsChanged);
        }

        #endregion

        #region Show Reports
        public override void ShowReport()
        {
            FetchFDRegister();
        }
        #endregion

        #region Methods
        public void FetchFDRegister()
        {
            
            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo))
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
                        FetchFDRegisterDetails();
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
                    FetchFDRegisterDetails();
                    base.ShowReport();
                }
            }
        }

        public void FetchFDRegisterDetails()
        {
            try
            {
                //  this.ReportTitle = ReportProperty.Current.ReportTitle;
                //this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
                this.SetLandscapeHeader = xrtblHeaderTable.WidthF;
                this.SetLandscapeFooter = xrtblHeaderTable.WidthF;
                this.SetLandscapeFooterDateWidth = 970.00f;
                //setHeaderTitleAlignment();
                // this.ReportPeriod = String.Format("For the Period: {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
                SetReportTitle();

                if (ReportProperty.Current.FDInvestmentType > 0)
                {
                    this.ReportTitle = this.ReportTitle + " - " + ReportProperty.Current.FDInvestmentTypeName;
                }

                if (this.AppSetting.EnableFlexiFD == "1" && ReportProperty.Current.FDScheme>= 0)
                {
                    this.ReportTitle = this.ReportTitle + " (" + ReportProperty.Current.FDSchemeName + ")";
                }
                setHeaderTitleAlignment();

                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                grpBankHeader.Visible = (ReportProperty.Current.ShowByBank == 1);
                grpBankFooter.Visible = (ReportProperty.Current.ShowByBank == 1);
                grpInvestmentHeader.Visible = (ReportProperty.Current.ShowByInvestment == 1);
                grpInvestmentFooter.Visible = (ReportProperty.Current.ShowByInvestment == 1);
                grpFDLedgerHeader.Visible = (ReportProperty.Current.ShowByLedger == 1);
                grpFDLedgerFooter.Visible = (ReportProperty.Current.ShowByLedger == 1);
                    

                string FDRegister = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.FetchFDRegisterDetails);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    //  dataManager.Parameters.Add(this.ReportParameters.FDREGISTERSTATUSColumn, this.ReportProperties.FDRegisterStatus);

                    //03/06/2024, Attach Investment Type
                    if (this.ReportProperties.FDInvestmentType > 0)
                    {
                        dataManager.Parameters.Add(reportSetting1.Ledger.FD_INVESTMENT_TYPE_IDColumn, this.ReportProperties.FDInvestmentType);
                    }

                    //23/10/2024, Attach currency country id 
                    if (this.AppSetting.AllowMultiCurrency ==1 && this.ReportProperties.CurrencyCountryId > 0 )
                    {
                        dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn.ColumnName, this.ReportProperties.CurrencyCountryId);
                    }
                    
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, FDRegister);

                    AttachShowBy();                   

                    DataView dvCashFlow = resultArgs.DataSource.TableView;
                    if (ReportProperty.Current.ShowByLedger == 1 || ReportProperty.Current.ShowByInvestment== 1 || ReportProperty.Current.ShowByBank == 1)
                    {
                        xrSnoValue.Summary.Running = SummaryRunning.Group;
                    }
                    else
                    {
                        xrSnoValue.Summary.Running = SummaryRunning.Report;
                    }

                    if (dvCashFlow != null && dvCashFlow.Count != 0)
                    {
                        string RegisterStatus = ReportProperty.Current.FDRegisterStatus.Equals((int)YesNo.No) ? "All" : ReportProperty.Current.FDRegisterStatus.Equals((int)YesNo.Yes) ? "Active" : "Closed";
                        if (!RegisterStatus.Equals("All"))
                            dvCashFlow.RowFilter = "CLOSING_STATUS='" + RegisterStatus + "'";

                        if (this.AppSetting.EnableFlexiFD == "1" && ReportProperty.Current.FDScheme>=0)
                        {
                            dvCashFlow.RowFilter = (string.IsNullOrEmpty(dvCashFlow.RowFilter) ? "" : " AND ") + " FD_SCHEME = " + ReportProperty.Current.FDScheme;
                        }
                        dvCashFlow.Table.TableName = "FDRegister";
                        this.DataSource = dvCashFlow.ToTable();
                        this.DataMember = dvCashFlow.Table.TableName;
                        dvCashFlow.RowFilter = "";
                    }
                    else
                    {
                        dvCashFlow.Table.TableName = "FDRegister";
                        this.DataSource = dvCashFlow;
                        this.DataMember = dvCashFlow.Table.TableName;

                        grpBankHeader.Visible = false;
                        grpBankFooter.Visible = false;
                        grpFDLedgerHeader.Visible = false;
                        grpFDLedgerFooter.Visible = false;
                        grpInvestmentHeader.Visible = false;
                        grpInvestmentFooter.Visible = false;
                    }
                }
                SetReportBorder();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, false);
            }
            finally { }
        }

        private void SetReportBorder()
        {
            //***To align header table dynamically---changed by sugan******************************************************************************************
            xrtblHeaderTable.SuspendLayout();
            xrtblFdRegister.SuspendLayout();
            xrtblGrandTotal.SuspendLayout();
            xrtblBankFooter.SuspendLayout();
            xrtblInvestmentFooter.SuspendLayout();
            xrtblLedgerFooter.SuspendLayout();

            //28/11/2018, to lock reinvestment feature based on setting ---------------------------------------------------
            if (this.UIAppSetting.EnableFlexiFD != "1")
            {
                if (xrHeaderRow.Cells.Contains(xrReInvestAmount))
                    xrHeaderRow.Cells.Remove(xrHeaderRow.Cells[xrReInvestAmount.Name]);

                if (xrDataRow.Cells.Contains(xrReInvestAmountValue))
                    xrDataRow.Cells.Remove(xrDataRow.Cells[xrReInvestAmountValue.Name]);

                if (xrBankGrpFooterRow.Cells.Contains(xrSumReIvestAmtBank))
                    xrBankGrpFooterRow.Cells.Remove(xrBankGrpFooterRow.Cells[xrSumReIvestAmtBank.Name]);

                if (xrInvestmentGrpFooterRow.Cells.Contains(xrSumReInvestAmtInvestment))
                    xrInvestmentGrpFooterRow.Cells.Remove(xrInvestmentGrpFooterRow.Cells[xrSumReInvestAmtInvestment.Name]);

                if (xrLedgerGrpFooterRow.Cells.Contains(xrSumReInvestAmtLedger))
                    xrLedgerGrpFooterRow.Cells.Remove(xrLedgerGrpFooterRow.Cells[xrSumReInvestAmtLedger.Name]);

                if (xrgrandRow.Cells.Contains(xrGrandReInvestAmt))
                    xrgrandRow.Cells.Remove(xrgrandRow.Cells[xrGrandReInvestAmt.Name]);
            }
            //-------------------------------------------------------------------------------------------------------------------

            //31/07/2024, Other than India, let us lock TDS Amount
            if (this.AppSetting.IsCountryOtherThanIndia)
            {
                this.HideTableCell(xrtblHeaderTable, xrHeaderRow, xrTDSAmount);
                this.HideTableCell(xrtblFdRegister, xrDataRow, xrTDSAmountValue);
                this.HideTableCell(xrtblBankFooter, xrBankGrpFooterRow, xrSumTDSAmt);
                this.HideTableCell(xrtblInvestmentFooter, xrInvestmentGrpFooterRow, xrSumTDSAmtInvestment);
                this.HideTableCell(xrtblLedgerFooter, xrLedgerGrpFooterRow, xrSumTDSAmtLedger);
                this.HideTableCell(xrtblGrandTotal, xrgrandRow, xrGrandTDSAmt);
            }

            xrtblHeaderTable.PerformLayout();
            xrtblFdRegister.PerformLayout();
            xrtblGrandTotal.PerformLayout();
            xrtblBankFooter.PerformLayout();
            xrtblInvestmentFooter.PerformLayout();
            xrtblLedgerFooter.PerformLayout();

            //*********************************************************************************************
            xrtblHeaderTable = AlignHeaderTable(xrtblHeaderTable);
            xrtblFdRegister = AlignContentTable(xrtblFdRegister);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);
            xrtblGrpBank = AlignContentTable(xrtblGrpBank);
            xrtblBankFooter = AlignGrandTotalTable(xrtblBankFooter);
            xrtblGrpInvestment = AlignGrandTotalTable(xrtblGrpInvestment);
            xrtblInvestmentFooter = AlignGrandTotalTable(xrtblInvestmentFooter);
            xrtblGrpLedger = AlignContentTable(xrtblGrpLedger);
            xrtblLedgerFooter = AlignGrandTotalTable(xrtblLedgerFooter);

            //On 23/10/2024, To set currency symbol based on currency selection
            string currencysymbol = (this.AppSetting.AllowMultiCurrency == 1 ? ReportProperties.CurrencyCountrySymbol : settingProperty.Currency);
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                xrAccumulatedInterest.Text = (xrAccumulatedInterest.Tag != null ? xrAccumulatedInterest.Tag.ToString() : xrAccumulatedInterest.Text);
                xrIntReceived.Text = (xrIntReceived.Tag != null ? xrIntReceived.Tag.ToString() : xrIntReceived.Text);
                xrPrincipalAmount.Text = (xrPrincipalAmount.Tag != null ? xrPrincipalAmount.Tag.ToString() : xrPrincipalAmount.Text);
                xrReInvestAmount.Text = (xrReInvestAmount.Tag != null ? xrReInvestAmount.Tag.ToString() : xrReInvestAmount.Text);
                xrTDSAmount.Text = (xrTDSAmount.Tag != null ? xrTDSAmount.Tag.ToString() : xrTDSAmount.Text);
                xrMat.Text = (xrMat.Tag != null ? xrMat.Tag.ToString() : xrMat.Text);
                xrTotalAmount.Text = (xrTotalAmount.Tag != null ? xrTotalAmount.Tag.ToString() : xrTotalAmount.Text);
                xrWithdrawAmount.Text = (xrWithdrawAmount.Tag != null ? xrWithdrawAmount.Tag.ToString() : xrWithdrawAmount.Text);
                xrClosingBalance.Text = (xrClosingBalance.Tag != null ? xrClosingBalance.Tag.ToString() : xrClosingBalance.Text);
            }

            this.SetCurrencyFormat(xrAccumulatedInterest.Text, xrAccumulatedInterest, currencysymbol);
            this.SetCurrencyFormat(xrIntReceived.Text, xrIntReceived, currencysymbol);
            this.SetCurrencyFormat(xrPrincipalAmount.Text, xrPrincipalAmount, currencysymbol);
            this.SetCurrencyFormat(xrReInvestAmount.Text, xrReInvestAmount, currencysymbol);
            this.SetCurrencyFormat(xrTDSAmount.Text, xrTDSAmount, currencysymbol);
            this.SetCurrencyFormat(xrMat.Text, xrMat, currencysymbol);
            this.SetCurrencyFormat(xrTotalAmount.Text, xrTotalAmount, currencysymbol);
            this.SetCurrencyFormat(xrWithdrawAmount.Text, xrWithdrawAmount, currencysymbol);
            this.SetCurrencyFormat(xrClosingBalance.Text, xrClosingBalance, currencysymbol);
        }

        #endregion
        private void PrintingSystem_AfterMarginsChange(object sender, MarginsChangeEventArgs e)
        {
            ChangeReportSettings(sender);
        }

        private void PrintingSystem_PageSettingsChanged(object sender, EventArgs e)
        {
            ChangeReportSettings(sender);
        }
        
        private void ChangeReportSettings(object sender)
        {
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrFDAcNo.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrBank.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrProject.Name, 10));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrInvestName.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrInvestOn.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrRenewOn.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrMaturityOn.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrClosedOn.Name, 5));
            //lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrInstMode.Name, 5));
            //lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrInterest.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrPrincipalAmount.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrIntReceived.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrAccumulatedInterest.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrMat.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrTDSAmount.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrTotalAmount.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrWithdrawAmount.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrClosingBalance.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrStatus.Name, 5));

            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrFDAcNoValue.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrBankValue.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrProjectValue.Name, 10));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrInvestNameValue.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrInvestOnValue.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrRenewOnValue.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrMaturityOnValue.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrClosedOnValue.Name, 5));
            //lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrInstModeValue.Name, 5));
            //lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrInterestValue.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrPrincipalAmountValue.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrIntReceivedValue.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrAccumulatedInterestValue.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrMatValue.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrTDSAmountValue.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrTotalAmountValue.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrWithdrawAmountValue.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrClosingBalanceValue.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrStatusValue.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(this.xrtblGrandTotal.Name, xrGrandPrincipleAmt.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(this.xrtblGrandTotal.Name, xrGrandReInvestAmt.Name, 5));
                        

            PrintingSystemBase ps = sender as PrintingSystemBase;
            bool isLocationChanged = false;
            int newPageWidth =
                ps.PageBounds.Width - ps.PageMargins.Left - ps.PageMargins.Right;
            int currentPageWidth =
                this.PageWidth - this.Margins.Left - this.Margins.Right;
            int shift = currentPageWidth - newPageWidth;
            ResizeControls(this, newPageWidth);
            isLocationChanged = true;
            if (isLocationChanged == true)
            {
                this.Margins.Top = ps.PageMargins.Top;
                this.Margins.Bottom = ps.PageMargins.Bottom;
                this.Margins.Left = ps.PageMargins.Left;
                this.Margins.Right = ps.PageMargins.Right;
                this.PaperKind = ps.PageSettings.PaperKind;
                this.PaperName = ps.PageSettings.PaperName;
                this.Landscape = ps.PageSettings.Landscape;

                //xrInvestOn.WidthF= xrInvestOnValue.WidthF = 58;
                //xrRenewOn.WidthF = xrRenewOnValue.WidthF = 58;
                //xrMaturityOn.WidthF = xrMaturityOnValue.WidthF = 58;
                //xrClosedOn.WidthF = xrClosedOnValue.WidthF = 58;

                this.CreateDocument();
            }
        }

        private void ResizeControls(ReportBase rpt, Int32 pagewidh)
        {
            foreach (Band _band in rpt.Bands)
            {
                foreach (XRControl _control in _band.Controls)
                {
                    //_control.Location = new Point((_control.Location.X - shift), _control.Location.Y);
                    if ( _control.GetType() == typeof(XRLabel))
                    {
                        _control.WidthF = pagewidh - 5;
                    }
                    else if (_control.GetType() == typeof(XRTable))
                    {
                        _control.WidthF = pagewidh - 5;
                        //XRTable xrtbl = _control as XRTable;
                        
                        //foreach(XRTableCell cell in xrtbl.Rows[0].Cells)
                        //{
                        //    int index = lstcolumnwidth.FindIndex(t => t.Item1 == xrtbl.Name && t.Item2 == cell.Name);
                        //    float columnwidth = (pagewidh * 2) / 100;
                        //    if (index >= 0)
                        //    {
                        //        Tuple<String, String, Int32> cellwidth = new Tuple<String, String, Int32>(string.Empty, string.Empty, 0);
                        //        lstcolumnwidth.TryGetValue(index, ref cellwidth);
                        //        Int32 percentage = cellwidth.Item3;
                        //        columnwidth = (pagewidh * percentage) / 100;
                        //        cell.WidthF = columnwidth;
                        //    }
                        //    else
                        //    {

                        //    }
                        //}
                    }
                    else if (_control.GetType() == typeof(XRSubreport))
                    {
                        //XRSubreport subreport = _control as XRSubreport;
                        //subreport.WidthF = 
                        //ResizeControls(subreport.ReportSource as ReportBase, pagewidh);
                    }
                }
            }
        }

        private void xrSnoValue_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataRowView drvcrrentrow = (DataRowView)this.GetCurrentRow();
            if (drvcrrentrow == null)
            {
                xrSnoValue.Visible = false;
            }
            else
            {
                xrSnoValue.Visible = true;
            }
        }

        private void xrSnoValue_AfterPrint(object sender, EventArgs e)
        {

        }

        private void xrcellInvestmentName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (ReportProperty.Current.ShowByLedger == 1)
            {
                string investmentname = GetCurrentColumnValue(reportSetting1.FDRegister.ACCOUNT_HOLDERColumn.ColumnName).ToString();
                if (!string.IsNullOrEmpty(investmentname))
                {
                    xrcellInvestmentName.Text =  "   " + investmentname; 
                }
            }
        }

        private void xrgrpBankValue_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string bankname = GetCurrentColumnValue(reportSetting1.FDRegister.BANKColumn.ColumnName).ToString();

            if (ReportProperty.Current.ShowByLedger == 1 && (ReportProperty.Current.ShowByInvestment == 1 || ReportProperty.Current.ShowByLedger == 1))
            {
                if (!string.IsNullOrEmpty(bankname))
                {
                    xrgrpBankValue.Text = "     " + bankname; 
                }
            }
            else if (ReportProperty.Current.ShowByLedger == 0 && ReportProperty.Current.ShowByInvestment == 1)
            {
                if (!string.IsNullOrEmpty(bankname))
                {
                    xrgrpBankValue.Text = "   " + bankname; 
                }
            }
        }

        /// <summary>
        /// This method attach group details settings
        /// </summary>
        private void AttachShowBy()
        {
            //Add Bank Group
            if (ReportProperty.Current.ShowByBank == 0)
            {
                grpBankHeader.GroupFields[0].FieldName = string.Empty;
            }
            else
            {
                grpBankHeader.GroupFields[0].FieldName = this.ReportParameters.BANKColumn.ColumnName;
                //grpBankHeader.GroupFields.Add( new GroupField(this.ReportParameters.BANKColumn.ColumnName,XRColumnSortOrder.Ascending));
            }

            //Add Investment Group
            if (ReportProperty.Current.ShowByInvestment == 0)
            {
                grpInvestmentHeader.GroupFields[0].FieldName = string.Empty;
            }
            else
            {
                grpInvestmentHeader.GroupFields[0].FieldName = this.reportSetting1.FixedDepositStatement.ACCOUNT_HOLDERColumn.ColumnName;
                //grpBankHeader.GroupFields.Add( new GroupField(this.ReportParameters.BANKColumn.ColumnName,XRColumnSortOrder.Ascending));
            }

            //Add Ledger Group
            if (ReportProperty.Current.ShowByLedger == 0)
            {
                grpFDLedgerHeader.GroupFields[0].FieldName = string.Empty;
            }
            else
            {
                grpFDLedgerHeader.GroupFields[0].FieldName = this.reportSetting1.Ledger.LEDGER_NAMEColumn.ColumnName;
            }
        }

        private void xrBankTotalCaption_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string banktitleCaption = (sender as XRTableCell).Text.Trim();

            if (ReportProperty.Current.ShowByLedger == 1 && (ReportProperty.Current.ShowByInvestment == 1 || ReportProperty.Current.ShowByLedger == 1))
            {
                if (!string.IsNullOrEmpty(banktitleCaption))
                {
                    (sender as XRTableCell).Text = "     " + banktitleCaption;
                }
            }
            else if (ReportProperty.Current.ShowByLedger == 0 && ReportProperty.Current.ShowByInvestment == 1)
            {
                if (!string.IsNullOrEmpty(banktitleCaption))
                {
                    (sender as XRTableCell).Text = "   " + banktitleCaption;
                }
            }
        }

        private void xrInvestmentCaption_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string investmentnameCaption = (sender as XRTableCell).Text.Trim();
            (sender as XRTableCell).Text = "   " + investmentnameCaption.Trim();

            if (ReportProperty.Current.ShowByLedger == 1)
            {
                if (!string.IsNullOrEmpty(investmentnameCaption))
                {
                    investmentnameCaption = "   " + investmentnameCaption;
                }
            }
            xrInvestmentCaption.Text = investmentnameCaption;
        }

    }
}
