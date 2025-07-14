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
    public partial class MutualFundRegister : ReportHeaderBase
    {
        #region Declaration
        List<Tuple<String, String, Int32>> lstcolumnwidth = new List<Tuple<String, String, Int32>>();
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public MutualFundRegister()
        {
            InitializeComponent();
            //On 27/08/2020
            this.AttachDrillDownToRecord(xrtblFdRegister, xrBankValue,
                        new ArrayList { this.ReportParameters.FD_ACCOUNT_IDColumn.ColumnName }, DrillDownType.LEDGER_VOUCHER, false, "VOUCHER_SUB_TYPE");

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
                        FetchMutualFundRegisterDetails();
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
                    FetchMutualFundRegisterDetails();
                    base.ShowReport();
                }
            }
        }

        public void FetchMutualFundRegisterDetails()
        {
            try
            {
                this.SetLandscapeHeader = xrtblHeaderTable.WidthF;
                this.SetLandscapeFooter = xrtblHeaderTable.WidthF;
                setHeaderTitleAlignment();
                
                SetReportTitle();
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;


                string MutualFundRegister = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.FetchMutualFundRegisterDetails);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, MutualFundRegister);
                    if (resultArgs.Success)
                    {
                        DataTable dtMutualFundRegister = resultArgs.DataSource.Table;
                        if (!ReportProperty.Current.FDRegisterStatus.Equals((int)YesNo.No))
                        {
                            if (ReportProperty.Current.FDRegisterStatus==1) //For Active
                                dtMutualFundRegister.DefaultView.RowFilter = reportSetting1.FDRegister.BALANCE_AMOUNTColumn.ColumnName + " >0 " ;
                            else if (ReportProperty.Current.FDRegisterStatus == 2) //For Closed
                                dtMutualFundRegister.DefaultView.RowFilter = reportSetting1.FDRegister.BALANCE_AMOUNTColumn.ColumnName + " = 0";
                            else
                                dtMutualFundRegister.DefaultView.RowFilter = string.Empty; ;

                            dtMutualFundRegister = dtMutualFundRegister.DefaultView.ToTable();
                        }
                        if (dtMutualFundRegister != null)
                        {
                            string status = "IIF(" + reportSetting1.FDRegister.BALANCE_AMOUNTColumn.ColumnName + " <=0, 'Closed', 'Active')";
                            dtMutualFundRegister.Columns.Add(reportSetting1.FDRegister.CLOSING_STATUSColumn.ColumnName, typeof(System.String), status);
                        }

                        this.DataSource = dtMutualFundRegister;
                        this.DataMember = dtMutualFundRegister.TableName;
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
            //31/07/2024, Other than India, let us lock TDS Amount
            if (this.AppSetting.IsCountryOtherThanIndia)
            {
                this.HideTableCell(xrtblHeaderTable, xrHeaderRow, xrCapTDSAmount);
                this.HideTableCell(xrtblFdRegister, xrDataRow, xrTDSAmount);
                this.HideTableCell(xrtblLedgerFooter, xrLedgerGrpFooterRow, xrSumTDSAmount);
            }

            //***To align header table dynamically---changed by sugan******************************************************************************************
           
            xrtblHeaderTable = AlignHeaderTable(xrtblHeaderTable, true);
            xrtblFdRegister = AlignContentTable(xrtblFdRegister);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);
           
            xrtblGrpLedger = AlignContentTable(xrtblGrpLedger);
            xrtblLedgerFooter = AlignGrandTotalTable(xrtblLedgerFooter);

            //this.SetCurrencyFormat(xrClosingBalance.Text, xrClosingBalance);
        }

        #endregion
        
        private void xrSnoValue_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void xrSnoValue_AfterPrint(object sender, EventArgs e)
        {

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

        private void xrSnoValue_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrSnoValue.Visible = false;
            if (this.GetCurrentRow() != null)
            {
                xrSnoValue.Visible = true;
            }
        }

       

    }
}
