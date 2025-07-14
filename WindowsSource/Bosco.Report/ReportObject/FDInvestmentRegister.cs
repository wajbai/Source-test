using System;
using System.Data;

using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Collections;
using DevExpress.XtraReports.UI;
namespace Bosco.Report.ReportObject
{
    public partial class FDInvestmentRegister : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        bool isRecordsExists = false;
        #endregion

        #region Constructor
        public FDInvestmentRegister()
        {
            InitializeComponent();

            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                xrcCapAmount.Tag = xrcCapAmount.Text;
                xrcCapExpMaturityAmount.Tag = xrcCapExpMaturityAmount.Text;
            }

            this.AttachDrillDownToRecord(xrtblBindData, xrcFDAccountNo,
                    new ArrayList { this.ReportParameters.FD_ACCOUNT_IDColumn.ColumnName }, DrillDownType.LEDGER_VOUCHER, false, "VOUCHER_SUB_TYPE");
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            this.SetLandscapeHeader = 1120.25f;
            this.SetLandscapeFooter = 1120.25f;
            this.SetLandscapeFooterDateWidth = 970.00f;

            BindFDInvestmentRegister();
        }
        #endregion

        #region Method
        private void BindFDInvestmentRegister()
        {
            isRecordsExists = false;
            if (!string.IsNullOrEmpty(this.ReportProperties.DateFrom) && !string.IsNullOrEmpty(this.ReportProperties.DateTo))
            {
                AttachShowBy();
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        BindProperty();
                    }
                    else
                    {
                        SetReportTitle();
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    BindProperty();
                }

                if (isRecordsExists)
                {
                    grpBankHeader.Visible = (ReportProperties.ShowByBank == 1);
                    grpBankFooter.Visible = (ReportProperties.ShowByBank == 1);
                    grpInvestmentHeader.Visible = (ReportProperties.ShowByInvestment == 1);
                    grpInvestmentFooter.Visible = (ReportProperties.ShowByInvestment == 1);
                    grpFDLedgerHeader.Visible = (ReportProperties.ShowByLedger == 1);
                    grpFDledgerFooter.Visible = (ReportProperties.ShowByLedger == 1);
                }
                else
                {
                    grpBankHeader.Visible = grpBankFooter.Visible = false;
                    grpInvestmentHeader.Visible = grpInvestmentFooter.Visible = false;
                    grpFDLedgerHeader.Visible = grpFDledgerFooter.Visible = false;
                }
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            
            SetReportBorder();
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                string FDInvestmentRegister = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.FDInvestmentRegisterbyDateRange);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    
                    //04/06/2024, Attach Investment Type
                    if (this.ReportProperties.FDInvestmentType > 0)
                    {
                        dataManager.Parameters.Add(reportSetting1.Ledger.FD_INVESTMENT_TYPE_IDColumn, this.ReportProperties.FDInvestmentType);
                    }

                    //23/10/2024, Attach currency country id 
                    if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                    {
                        dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn.ColumnName, this.ReportProperties.CurrencyCountryId);
                    }

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, FDInvestmentRegister);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return resultArgs;
        }

        private void BindProperty()
        {
            setHeaderTitleAlignment();
            SetReportTitle();
            if (ReportProperty.Current.FDInvestmentType > 0)
            {
                this.ReportTitle = this.ReportTitle + " - " + ReportProperty.Current.FDInvestmentTypeName;
                setHeaderTitleAlignment();
            }

            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            resultArgs = GetReportSource();
            DataView dvBankReconciliation = resultArgs.DataSource.TableView;
            if (dvBankReconciliation != null)
            {
                dvBankReconciliation.Table.TableName = "FDInvestmentRegister";
                this.DataSource = dvBankReconciliation;
                this.DataMember = dvBankReconciliation.Table.TableName;
                isRecordsExists = dvBankReconciliation.Count > 0;
            }
            base.ShowReport();
        }

        private void SetReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrtblBindData = AlignContentTable(xrtblBindData);
            xrTblBankHeader = AlignContentTable(xrTblBankHeader);
            xrTblBankTotal = AlignGrandTotalTable(xrTblBankTotal);
            xrTblInvestmentHeader = AlignContentTable(xrTblInvestmentHeader);
            xrTblInvestmentTotal = AlignGrandTotalTable(xrTblInvestmentTotal);
            xrTblFDLedgerHeader = AlignContentTable(xrTblFDLedgerHeader);
            xrTblFDLedgerTotal = AlignGrandTotalTable(xrTblFDLedgerTotal);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);

            //On 23/10/2024, To set currency symbol based on currency selection
            string currencysymbol = (this.AppSetting.AllowMultiCurrency == 1 ? ReportProperties.CurrencyCountrySymbol : settingProperty.Currency);
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                xrcCapAmount.Text = (xrcCapAmount.Tag != null ? xrcCapAmount.Tag.ToString() : xrcCapAmount.Text);
                xrcCapExpMaturityAmount.Text = (xrcCapExpMaturityAmount.Tag != null ? xrcCapExpMaturityAmount.Tag.ToString() : xrcCapExpMaturityAmount.Text);
            }

            this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.AMOUNT, xrcCapAmount, currencysymbol);
            this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.FDEXPMATAMOUNT, xrcCapExpMaturityAmount, currencysymbol);
        }
        #endregion

        private void xrcExpMatAmount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double ExpectedMatAmt = this.ReportProperties.NumberSet.ToDouble(xrcExpMatAmount.Text);
            if (ExpectedMatAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrcExpMatAmount.Text = "";
            }
        }

        private void xrBankGrpTotalExpectAmt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double ExpectedMatAmt = this.ReportProperties.NumberSet.ToDouble(xrBankGrpTotalExpectAmt.Text);
            if (ExpectedMatAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrBankGrpTotalExpectAmt.Text = "";
                ((XRTableCell)sender).Text = "";
            }
        }

        private void xrGrandTotalExpectAmt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void xrGrandTotalExpectAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double ExpectedMatAmt = this.ReportProperties.NumberSet.ToDouble(xrGrandTotalExpectAmt.Text);
            if (ExpectedMatAmt == 0)
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void xrBankGrpTotalExpectAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double ExpectedMatAmt = this.ReportProperties.NumberSet.ToDouble(xrBankGrpTotalExpectAmt.Text);
            if (ExpectedMatAmt == 0)
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void xrcellGrpInvestmentValue_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (ReportProperty.Current.ShowByLedger == 1)
            {
                string investmentname = GetCurrentColumnValue(reportSetting1.FDInvestmentRegister.ACCOUNT_HOLDERColumn.ColumnName).ToString();
                if (!string.IsNullOrEmpty(investmentname))
                {
                    xrcellGrpInvestmentValue.Text = "   " + investmentname;
                }
            }

        }

        private void xrcellGrpBankValue_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string bankname = GetCurrentColumnValue(reportSetting1.FDInvestmentRegister.BANKColumn.ColumnName).ToString();

            if (ReportProperty.Current.ShowByLedger == 1 && ReportProperty.Current.ShowByInvestment == 1)
            {
                if (!string.IsNullOrEmpty(bankname))
                {
                    xrcellGrpBankValue.Text = "     " + bankname;
                }
            }
            else if (ReportProperty.Current.ShowByLedger == 0 && ReportProperty.Current.ShowByInvestment == 1)
            {
                if (!string.IsNullOrEmpty(bankname))
                {
                    xrcellGrpBankValue.Text = "   " + bankname;
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

            if (ReportProperty.Current.ShowByLedger == 1 && ReportProperty.Current.ShowByInvestment == 1)
            {
                string banktotalTitle = (sender as XRTableCell).Text.Trim();
                if (!string.IsNullOrEmpty(banktotalTitle))
                {
                    (sender as XRTableCell).Text = "     " + banktotalTitle;
                }
            }
            else if (ReportProperty.Current.ShowByLedger == 0 && ReportProperty.Current.ShowByInvestment == 1)
            {
                string banktotalTitle = (sender as XRTableCell).Text.Trim();
                if (!string.IsNullOrEmpty(banktotalTitle))
                {
                    (sender as XRTableCell).Text = "   " + banktotalTitle;
                }
            }
        }

        private void xrInvestmentTotalCaption_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (ReportProperty.Current.ShowByLedger == 1)
            {
                string InvestmentTitle = (sender as XRTableCell).Text.Trim();
                if (!string.IsNullOrEmpty(InvestmentTitle))
                {
                    (sender as XRTableCell).Text = "   " + InvestmentTitle;
                }
            }
        }

        

    }
}
