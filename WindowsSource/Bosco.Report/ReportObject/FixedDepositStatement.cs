using System;
using System.Data;

using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Collections;
using DevExpress.XtraReports.UI;
namespace Bosco.Report.ReportObject
{
    public partial class FixedDepositStatement : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        bool RecordsExists = false;
        #endregion

        #region Constructor
        public FixedDepositStatement()
        {
            InitializeComponent();
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                xrCapFDAmount.Tag = xrCapFDAmount.Text;
            }
       //     this.AttachDrillDownToRecord(xrtblBindData, xrFDAccountNo,
       //new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName }, DrillDownType.LEDGER_VOUCHER, false, "VOUCHER_SUB_TYPE");
            this.AttachDrillDownToRecord(xrtblBindData, xrFDAccountNo,
     new ArrayList { this.ReportParameters.FD_ACCOUNT_IDColumn.ColumnName }, DrillDownType.LEDGER_VOUCHER, false, "VOUCHER_SUB_TYPE");
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindFixedDeposit();

        }
        #endregion

        #region Method
        private void BindFixedDeposit()
        {
            this.SetLandscapeHeader = 1120.25f;
            this.SetLandscapeFooter = 1120.25f;
            this.SetLandscapeFooterDateWidth = 970.00f;
            setHeaderTitleAlignment();

            RecordsExists = false;
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
                        if (ReportProperty.Current.FDInvestmentType > 0)
                        {
                            this.ReportTitle = this.ReportTitle + " - " + ReportProperty.Current.FDInvestmentTypeName;
                        }
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    BindProperty();
                }

                if (RecordsExists)
                {
                    grpBankHeader.Visible = (ReportProperties.ShowByBank == 1);
                    grpBankFooter.Visible = (ReportProperties.ShowByBank == 1);
                    grpInvestmentHeader.Visible = (ReportProperties.ShowByInvestment == 1);
                    grpInvestmentFooter.Visible = (ReportProperties.ShowByInvestment == 1);
                    grpFDLedgerHeader.Visible = (ReportProperties.ShowByLedger == 1);
                    grpFDLedgerFooter.Visible = (ReportProperties.ShowByLedger == 1);
                }
                else
                {
                    grpBankHeader.Visible = false;
                    grpBankFooter.Visible = false;
                    grpInvestmentHeader.Visible = false;
                    grpInvestmentFooter.Visible = false;
                    grpFDLedgerHeader.Visible = false;
                    grpFDLedgerFooter.Visible = false;
                }
            }
            else
            {
                SetReportTitle();
                if (ReportProperty.Current.FDInvestmentType > 0)
                {
                    this.ReportTitle = this.ReportTitle + " - " + ReportProperty.Current.FDInvestmentTypeName;
                }
                ShowReportFilterDialog();
            }
                        

            SetReportBorder();
        }

        private void BindProperty()
        {
            //this.ReportTitle = ReportProperty.Current.ReportTitle;
            //this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
            setHeaderTitleAlignment();
            //this.ReportPeriod = String.Format("For the Period: {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
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
                dvBankReconciliation.Table.TableName = "FixedDepositStatement";
                this.DataSource = dvBankReconciliation;
                this.DataMember = dvBankReconciliation.Table.TableName;
            }
            RecordsExists = (dvBankReconciliation.Count > 0);
            base.ShowReport();
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                string FixedDepositStatement = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.FixedDepositStatement);
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
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, FixedDepositStatement);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return resultArgs;
        }

        private void SetReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrtblBindData = AlignContentTable(xrtblBindData);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);
            xrTblBankHeader = AlignContentTable(xrTblBankHeader);
            xrTblBankTotal = AlignGrandTotalTable(xrTblBankTotal);
            xrTblInvestmentHeader = AlignContentTable(xrTblInvestmentHeader);
            xrTblInvestmentTotal = AlignGrandTotalTable(xrTblInvestmentTotal);
            xrTblFDLedgerHeader = AlignContentTable(xrTblFDLedgerHeader);
            xrTblFDLedgerTotal = AlignGrandTotalTable(xrTblFDLedgerTotal);

            string currencysymbol = (this.AppSetting.AllowMultiCurrency == 1 ? ReportProperties.CurrencyCountrySymbol : settingProperty.Currency);
             if (this.AppSetting.AllowMultiCurrency == 1)
             {
                 xrCapFDAmount.Text = (xrCapFDAmount.Tag != null ? xrCapFDAmount.Tag.ToString() : xrCapFDAmount.Text);
             }
             this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.AMOUNT, xrCapFDAmount, currencysymbol);
        }
        #endregion

        private void xrcellGrpInvestment_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (ReportProperty.Current.ShowByLedger == 1)
            {
                string investmentname = GetCurrentColumnValue(reportSetting1.FDRegister.ACCOUNT_HOLDERColumn.ColumnName).ToString();
                if (!string.IsNullOrEmpty(investmentname))
                {
                    xrcellGrpInvestment.Text = "   " + investmentname;
                }
            }
        }

        private void xrcellGrpBank_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string bankname = GetCurrentColumnValue(reportSetting1.FDRegister.BANKColumn.ColumnName).ToString();

            if (ReportProperty.Current.ShowByLedger == 1 && ReportProperty.Current.ShowByInvestment == 1)
            {
                if (!string.IsNullOrEmpty(bankname))
                {
                    xrcellGrpBank.Text = "     " + bankname;
                }
            }
            else if (ReportProperty.Current.ShowByLedger == 0 && ReportProperty.Current.ShowByInvestment == 1)
            {
                if (!string.IsNullOrEmpty(bankname))
                {
                    xrcellGrpBank.Text = "   " + bankname;
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

        private void xrInvestmentCaption_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (ReportProperty.Current.ShowByLedger == 1)
            {
                string investmentnameCaption = (sender as XRTableCell).Text.Trim();
                if (!string.IsNullOrEmpty(investmentnameCaption))
                {
                    (sender as XRTableCell).Text = "   " + investmentnameCaption;
                }
            }
        }

        private void xrBankTitleCaption_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string banknameTitleCaption = (sender as XRTableCell).Text.Trim();

            if (ReportProperty.Current.ShowByLedger == 1 && ReportProperty.Current.ShowByInvestment == 1)
            {
                if (!string.IsNullOrEmpty(banknameTitleCaption))
                {
                    (sender as XRTableCell).Text = "     " + banknameTitleCaption;
                }
            }
            else if (ReportProperty.Current.ShowByLedger == 0 && ReportProperty.Current.ShowByInvestment == 1)
            {
                if (!string.IsNullOrEmpty(banknameTitleCaption))
                {
                    (sender as XRTableCell).Text = "   " + banknameTitleCaption;
                }
            }
        }

       

        
    }
}
