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

    public partial class BudgetVariance : ReportHeaderBase
    {
        ResultArgs resultArgs = null;
        Double LedgerGrpPercentageAvg = 0;
        int GrpLedgerCount = 0;

        Double BudgetNaturePercentageAvg = 0;
        int BudgetNatureLedgerCount = 0;

        double BudgetAmountbyLG = 0;
        double ActualAmountbyLG = 0;

        double BudgetAmountbyBN = 0;
        double ActualAmountbyBN = 0;

        private DataTable dtCCDetails = new DataTable();
        private bool PrevLedgerCCFound = false;

        public BudgetVariance()
        {
            InitializeComponent();
        }
        #region Show Reports
        public override void ShowReport()
        {
            FetchBudgetVariance();

        }
        #endregion
        
        public void FetchBudgetVariance()
        {
            this.SetLandscapeBudgetNameWidth = xrtblHeader.WidthF;
            this.SetLandscapeHeader = xrtblHeader.WidthF;
            this.SetLandscapeFooter = xrtblHeader.WidthF;
            this.SetLandscapeFooterDateWidth = xrtblHeader.WidthF;
            setHeaderTitleAlignment();

            BudgetAmountbyLG = 0;
            ActualAmountbyLG = 0;
            BudgetAmountbyBN = 0;
            ActualAmountbyBN = 0;

            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo) || string.IsNullOrEmpty(this.ReportProperties.Budget) || this.ReportProperties.Budget.Split(',').Length == 0)
            {
                SetReportTitle();
                AssignBudgetDateRangeTitle();
                ShowReportFilterDialog();
            }
            else
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        FetchBudgetVarianceDetails();
                        base.ShowReport();
                    }
                    else
                    {
                        SetReportTitle();
                        AssignBudgetDateRangeTitle();
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    FetchBudgetVarianceDetails();
                    base.ShowReport();
                }
            }
        }

        private void FetchBudgetVarianceDetails()
        {
            try
            {
                this.SetLandscapeBudgetNameWidth = xrtblHeader.WidthF - 5;
                this.SetLandscapeHeader = xrtblHeader.WidthF - 5;
                this.SetLandscapeFooter = xrtblHeader.WidthF - 5;
                this.SetLandscapeFooterDateWidth = xrtblHeader.WidthF - 5;
                SetTitleWidth(xrtblHeader.WidthF - 5);
                setHeaderTitleAlignment();
                SetReportTitle();
                AssignBudgetDateRangeTitle();
                this.BudgetName = ReportProperty.Current.BudgetName;
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                //Hide budget group for ABE
                //if (AppSetting.IS_ABEBEN_DIOCESE || AppSetting.IS_DIOMYS_DIOCESE)
                if (AppSetting.IS_ABEBEN_DIOCESE || AppSetting.IS_DIOMYS_DIOCESE || AppSetting.IS_CMF_CONGREGATION)
                {
                    grpBudgetGroup.Visible = false;
                    xrtblHeader.SuspendLayout();
                    if (xrHeaderRow.Cells.Contains(xrcellHeaderBudgetSubGroup))
                        xrHeaderRow.Cells.Remove(xrHeaderRow.Cells[xrcellHeaderBudgetSubGroup.Name]);
                    xrtblHeader.PerformLayout();

                    xrtblDetails.SuspendLayout();
                    if (xrDetailRow.Cells.Contains(xrcellBudgetSubGroup))
                        xrDetailRow.Cells.Remove(xrDetailRow.Cells[xrcellBudgetSubGroup.Name]);
                    xrtblDetails.PerformLayout();

                    xrTblGrpLGHeader.SuspendLayout();
                    if (xrLGGrpRowHeader.Cells.Contains(xrcellGrpHeadEmpty))
                        xrLGGrpRowHeader.Cells.Remove(xrLGGrpRowHeader.Cells[xrcellGrpHeadEmpty.Name]);
                    xrTblGrpLGHeader.PerformLayout();
                    
                    xrCellTotal.WidthF = xrCellHeaderLedgerName.WidthF + xrCellHeaderLedgerCode.WidthF;
                    xrCellSumApproved.WidthF = xrCellHeaderApproved.WidthF;
                    xrCellSumActual.WidthF = xrCellHeaderActual.WidthF;
                    //xrlblVariance.WidthF = xrcellTotalVariance.WidthF;
                    xrlblVariance.WidthF = xrCellInAmt.WidthF;
                    xrcellTotalVariance.WidthF = xrCellInAmt.WidthF;
                    xrcellHeaderAmount.WidthF = xrCellInAmt.WidthF;
                    xrcellGrpLGFooterSubTotal.WidthF = xrcellCode.WidthF + xrCellLedgerName.WidthF;
                    xrcellHeaderInpercentage.WidthF = xrCellInPercentage.WidthF;

                    xrcellGrpFooterApprovedSum.WidthF = xrcellGrpHeadApprovedSum.WidthF;
                    xrcellGrpFooterActualSum.WidthF =  xrcellGrpHeadActualSum.WidthF;
                    xrcellGrpFooterVarianceSum.WidthF = xrcellGrpHeadVarianceSum.WidthF;
                    xrcellGrpFooterPercentageSum.WidthF = xrcellGrpHeadPercentageSum.WidthF;
                }

                //  resultArgs = GetBudgetDetails();
                //  if (resultArgs.Success)
                //{
                //DataTable dtBudgetInfo = resultArgs.DataSource.Table;
                //   if (dtBudgetInfo != null && dtBudgetInfo.Rows.Count > 0)
                // {
                // this.ReportProperties.DateFrom = dtBudgetInfo.Rows[0][this.ReportParameters.DATE_FROMColumn.ColumnName].ToString();
                // this.ReportProperties.DateTo = dtBudgetInfo.Rows[0][this.ReportParameters.DATE_TOColumn.ColumnName].ToString();
                //to get CC detail for Budget, it will be used to when reports generates
                
                xrSubreportCCDetails.Visible = false;
                if (this.ReportProperties.ShowCCDetails== 1)
                {
                    xrSubreportCCDetails.Visible = true;
                    AssignCCDetailReportSource();
                }

                string budgetvariance = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetVarianceReport);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.reportSetting5.BUDGETVARIANCE.BUDGET_IDColumn, this.ReportProperties.Budget);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);

                    //On 31/07/2020, to fix ledger balacen
                    /*dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.SHOWBUDGETLEDGERSEPARATERECEIPTPAYMENTACTUALBALANCEColumn, this.AppSetting.ShowBudgetLedgerSeparateReceiptPaymentActualBalance);
                    if (this.AppSetting.ShowBudgetLedgerActualBalance == "1")
                    {
                        dataManager.Parameters.Add(this.reportSetting1.BUDGET_LEDGER.VOUCHER_TYPEColumn, "JN");
                    }*/

                    //29/07/2021, to show ledger actual balance based on settings
                    if (this.AppSetting.ShowBudgetLedgerActualBalance == "1") //Ledger Closing Balance without Journal Voucher
                    {
                        //dataManager.Parameters.Add(reportSetting1.BUDGET_LEDGER.VOUCHER_TYPEColumn, VoucherSubTypes.JN.ToString());
                        dataManager.Parameters.Add(reportSetting1.BUDGETVARIANCE.SHOWBUDGETLEDGERSEPARATERECEIPTPAYMENTACTUALBALANCEColumn, "1");
                    }
                    else if (this.AppSetting.ShowBudgetLedgerActualBalance == "2") // Receipts Vocuehr & Payments Voucher separately
                    {
                        //dataManager.Parameters.Add(reportSetting1.BUDGET_LEDGER.VOUCHER_TYPEColumn, VoucherSubTypes.JN.ToString());
                        dataManager.Parameters.Add(reportSetting1.BUDGETVARIANCE.SHOWBUDGETLEDGERSEPARATERECEIPTPAYMENTACTUALBALANCEColumn, "2");
                    }
                    else //Ledger Closing Balance with Journal Voucher
                    {
                        dataManager.Parameters.Add(reportSetting1.BUDGETVARIANCE.SHOWBUDGETLEDGERSEPARATERECEIPTPAYMENTACTUALBALANCEColumn, "0");
                    }

                    //On 11/12/2024, To to currency based reports --------------------------------------------------------------------------------------
                    decimal CurrencyAvgExchangeRate = 1;
                    //decimal CurrencyPreviousYearAvgExchangeRate = 1;
                    if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                    {
                        CurrencyAvgExchangeRate = GetAvgCurrencyExchangeRateForFY(this.ReportProperties.CurrencyCountryId);
                        //CurrencyPreviousYearAvgExchangeRate = GetAvgCurrencyExchangeRateForPreviousFY(this.ReportProperties.CurrencyCountryId);
                        dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                    }
                    else
                    {
                        dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                    }
                    dataManager.Parameters.Add(reportSetting1.Country.EXCHANGE_RATEColumn, CurrencyAvgExchangeRate);
                    //dataManager.Parameters.Add(reportSetting1.Country.PREVIOUS_YEAR_EX_RATEColumn, CurrencyPreviousYearAvgExchangeRate);
                    //----------------------------------------------------------------------------------------------------------------------------------

                    //On 30/01/2025 filter cost centre if cc budget enabled and cc is selected --------------------------------------------------------
                    if (this.AppSetting.EnableCostCentreBudget == 1 && !string.IsNullOrEmpty(this.ReportProperties.CostCentre))
                    {
                        dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre);
                    }
                    //---------------------------------------------------------------------------------------------------------------------------------

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetvariance);

                    //Detail.SortFields.Add(new GroupField("LEDGER_CODE"));
                    Detail.SortFields.Clear();
                    if (this.ReportProperties.SortByLedger == 1)
                        Detail.SortFields.Add(new GroupField("LEDGER_NAME"));
                    else
                        Detail.SortFields.Add(new GroupField("LEDGER_CODE"));
                    Detail.SortFields.Add(new GroupField("BUDGET_SUB_GROUP"));

                    if (this.AppSetting.IS_CMF_CONGREGATION)
                    {
                        xrCellHeaderApproved.Multiline = true;
                        xrCellHeaderApproved.Text = "Approved\r\nBudget - " + UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Year.ToString();
                        xrCellHeaderActual.Text = "Realized";
                        this.BudgetName = string.Empty;
                        FixReportPropertyForCMF();
                        Detail.SortFields.Add(new GroupField("LEDGER_NAME"));

                        string NewRpTitle = "Budget Variance";
                        string annualbudgetsprojectsNames = GetBudgetProjectsNamesByDateProjects();
                        /*ReportProperty.Current.ReportTitle = annualbudgetsprojectsNames + " - " + NewRpTitle + " - " + UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Year.ToString();
                        this.ReportTitle = ReportProperty.Current.ReportTitle;
                        this.ReportSubTitle = annualbudgetsprojectsNames;*/

                        //On 23/09/2020
                        if (!string.IsNullOrEmpty(this.ReportProperties.Budget) && this.ReportProperties.Budget.Split(',').Length> 1)
                        {
                            NewRpTitle = "Consolidated " + NewRpTitle;
                        }
                        this.ReportTitle = NewRpTitle + " - " + UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Year.ToString();
                        this.ReportSubTitle = annualbudgetsprojectsNames;

                        //On 12/09/2020, to fix export file name
                        //if (ReportProperty.Current.SelectedProjectCount == ReportProperty.Current.AllProjectsCount)
                        if (!string.IsNullOrEmpty(this.ReportProperties.Budget) && this.ReportProperties.Budget.Split(',').Length > 1)
                        {
                            this.DisplayName = AppSetting.InstituteName + " - " + NewRpTitle + " - " + UtilityMember.DateSet.ToDate(ReportProperty.Current.DateFrom, false).Year.ToString();
                        }
                        else
                        {
                            this.DisplayName = annualbudgetsprojectsNames + " - " + NewRpTitle + " - " + UtilityMember.DateSet.ToDate(ReportProperty.Current.DateFrom, false).Year.ToString(); ;
                        }
                        //replace special characters which are not valid for file names
                        this.DisplayName = this.DisplayName.Replace("/", "").Replace("*", "");
                    }

                    
                    if (resultArgs.DataSource.Table != null )
                    {
                        DataTable dtBudgetVariance = resultArgs.DataSource.Table;
                        AppendHOHelpAmountInIncome(dtBudgetVariance);

                        //On 30/01/2025 filter cost centre if cc budget enabled and cc is selected --------------------------------------------------------
                        if (this.AppSetting.EnableCostCentreBudget == 1 && !string.IsNullOrEmpty(this.ReportProperties.CostCentre))
                        {
                            string filter = this.ReportParameters.COST_CENTRE_IDsColumn.ColumnName + "<>''";
                            dtBudgetVariance.DefaultView.RowFilter = filter;
                            dtBudgetVariance = dtBudgetVariance.DefaultView.ToTable();
                        }
                        //---------------------------------------------------------------------------------------------------------------------------------

                        this.DataSource = dtBudgetVariance;
                        this.DataMember = resultArgs.DataSource.Table.TableName;
                    }
                    SetReportBorder();
                }
                // }
                // }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, false);
            }
            finally { }
        }

        /// <summary>
        /// This method is used to attach HO/Province Help amount with Income side of Budget
        /// </summary>
        /// <param name="dtIncomeSource"></param>
        private void AppendHOHelpAmountInIncome(DataTable dtBudget)
        {
            decimal HOBudgetHelpPropsedAmt = 0;
            decimal HOBudgetHelpApprovedAmt = 0;
            ResultArgs resultArgs = new ResultArgs();
            if (dtBudget != null && dtBudget.Rows.Count > 0 && AppSetting.ENABLE_BUDGET_HO_HELP_AMOUNT)
            {
                string budgetinfo = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetInfo);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn, this.ReportProperties.Budget);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetinfo);
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtBudgetInfo = resultArgs.DataSource.Table;
                        string strHOBudgetHelpPropsedAmt = dtBudgetInfo.Compute("SUM(" + reportSetting1.BUDGETVARIANCE.HO_HELP_PROPOSED_AMOUNTColumn.ColumnName + ")", string.Empty).ToString();
                        string strHOBudgetHelpApprovedAmt = dtBudgetInfo.Compute("SUM(" + reportSetting1.BUDGETVARIANCE.HO_HELP_APPROVED_AMOUNTColumn.ColumnName + ")", string.Empty).ToString();

                        HOBudgetHelpPropsedAmt = UtilityMember.NumberSet.ToDecimal(strHOBudgetHelpPropsedAmt);
                        HOBudgetHelpApprovedAmt = UtilityMember.NumberSet.ToDecimal(strHOBudgetHelpApprovedAmt);

                        //On 12/02/2022, if amount is exists
                        if (HOBudgetHelpPropsedAmt > 0 || HOBudgetHelpApprovedAmt > 0)
                        {
                            DataRow drHOBudgetHelpAmount = dtBudget.NewRow();
                            drHOBudgetHelpAmount[reportSetting1.BUDGET_LEDGER.LEDGER_GROUPColumn.ColumnName] = string.Empty;
                            drHOBudgetHelpAmount[reportSetting1.ReportParameter.LEDGER_IDColumn.ColumnName] = 0;
                            drHOBudgetHelpAmount[reportSetting1.Ledger.LEDGER_CODEColumn.ColumnName] = string.Empty; //To come first order
                            drHOBudgetHelpAmount[reportSetting1.Ledger.LEDGER_NAMEColumn.ColumnName] = AppSetting.BUDGET_HO_HELP_AMOUNT_CAPTION; //To come first order
                            //On 12/02/2022
                            //drHOBudgetHelpAmount[reportSetting1.Ledger.NATURE_IDColumn.ColumnName] = (int)Natures.Income;
                            drHOBudgetHelpAmount[reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName] = Natures.Income.ToString();
                            drHOBudgetHelpAmount[reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName] = HOBudgetHelpApprovedAmt;
                            //drHOBudgetHelpAmount[reportSetting1.BUDGETVARIANCE.PROPOSED_AMOUNTColumn.ColumnName] = 0.0;
                            drHOBudgetHelpAmount[reportSetting1.ReportParameter.TRANS_MODEColumn.ColumnName] = TransSource.Cr.ToString().ToUpper();
                            drHOBudgetHelpAmount[reportSetting1.BUDGETVARIANCE.BUDGET_GROUPColumn.ColumnName] = string.Empty;
                            drHOBudgetHelpAmount[reportSetting1.BUDGETVARIANCE.BUDGET_SUB_GROUPColumn.ColumnName] = string.Empty;
                            drHOBudgetHelpAmount[reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName] = Natures.Income.ToString();
                            dtBudget.Rows.Add(drHOBudgetHelpAmount);
                        }
                    }
                }
            }
        }

        private void ShowCCDetails()
        {
            //On 25/02/2021, To show CC detail for given Ledger
            if (this.ReportProperties.ShowCCDetails == 1)
            {
                if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName) != null && dtCCDetails.Rows.Count > 0)
                {
                    Int32 ledgerid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName).ToString());
                    string budgetnature = GetCurrentColumnValue("BUDGET_NATURE").ToString();
                    string budgettransmode= (budgetnature.ToUpper() == "INCOME" ? TransMode.CR.ToString() : TransMode.DR.ToString()); 
                    BudgetVarianceCC ccDetail = xrSubreportCCDetails.ReportSource as BudgetVarianceCC;

                    dtCCDetails.DefaultView.RowFilter = string.Empty;
                    dtCCDetails.DefaultView.RowFilter = reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName + " = " + ledgerid + " AND " +
                        reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName + " = '" + budgettransmode + "'";
                    dtCCDetails.DefaultView.Sort = reportSetting1.MonthlyAbstract.COST_CENTRE_NAMEColumn.ColumnName;

                    DataTable dtCC = dtCCDetails.DefaultView.ToTable();

                    ccDetail.BindCCDetails(dtCC, budgettransmode);
                    ccDetail.CCTableWidth = xrtblDetails.WidthF;
                    ccDetail.CodeWidth = xrcellCode.WidthF;
                    if (AppSetting.IS_ABEBEN_DIOCESE || AppSetting.IS_DIOMYS_DIOCESE || AppSetting.IS_CMF_CONGREGATION)
                    {
                        ccDetail.CCNameWidth = xrCellLedgerName.WidthF;
                    }
                    else
                    {
                        ccDetail.CCNameWidth = xrCellLedgerName.WidthF + xrcellBudgetSubGroup.WidthF;
                    }
                    
                    ccDetail.CCApprovedAmount = xrCellApproved.WidthF;
                    ccDetail.CCActualAmount= xrCellActualAmt.WidthF;
                    ccDetail.CCVarianceAmount= xrCellInAmt.WidthF;
                    ccDetail.CCPercentage = xrCellInPercentage.WidthF;
                    dtCCDetails.DefaultView.RowFilter = string.Empty;

                    ProperBorderForLedgerRow(PrevLedgerCCFound);
                    xrSubreportCCDetails.Visible = (dtCC.Rows.Count > 0);
                    PrevLedgerCCFound = (dtCC.Rows.Count > 0);
                }
                else
                {
                    ProperBorderForLedgerRow(PrevLedgerCCFound);
                    xrSubreportCCDetails.Visible = false;
                    PrevLedgerCCFound = false;
                }
            }
            else
            {
                xrSubreportCCDetails.Visible = false;
                PrevLedgerCCFound = false;
            }
        }

        private void ProperBorderForLedgerRow(bool ccFound)
        {
            if (ccFound)
            {
                xrcellCode.Borders = BorderSide.Top | BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xrCellLedgerName.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                xrCellApproved.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                xrCellActualAmt.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                xrcellBudgetSubGroup.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                xrCellInAmt.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                xrCellInPercentage.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
            }
            else
            {
                xrcellCode.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xrcellCode.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xrCellLedgerName.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xrCellApproved.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xrCellActualAmt.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xrcellBudgetSubGroup.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xrCellInAmt.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xrCellInPercentage.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
            }
        }

        private void AssignCCDetailReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlCCBudgetRealization = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetCCVariation);

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.reportSetting5.BUDGETVARIANCE.BUDGET_IDColumn, this.ReportProperties.Budget);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);

                //29/07/2021, to show ledger actual balance based on settings
                if (this.AppSetting.ShowBudgetLedgerActualBalance == "1") //Ledger Closing Balance without Journal Voucher
                {
                    //dataManager.Parameters.Add(reportSetting1.BUDGET_LEDGER.VOUCHER_TYPEColumn, VoucherSubTypes.JN.ToString());
                    dataManager.Parameters.Add(reportSetting1.BUDGETVARIANCE.SHOWBUDGETLEDGERSEPARATERECEIPTPAYMENTACTUALBALANCEColumn, "1");
                }
                else if (this.AppSetting.ShowBudgetLedgerActualBalance == "2") // Receipts Vocuehr & Payments Voucher separately
                {
                    //dataManager.Parameters.Add(reportSetting1.BUDGET_LEDGER.VOUCHER_TYPEColumn, VoucherSubTypes.JN.ToString());
                    dataManager.Parameters.Add(reportSetting1.BUDGETVARIANCE.SHOWBUDGETLEDGERSEPARATERECEIPTPAYMENTACTUALBALANCEColumn, "2");
                }
                else //Ledger Closing Balance with Journal Voucher
                {
                    dataManager.Parameters.Add(reportSetting1.BUDGETVARIANCE.SHOWBUDGETLEDGERSEPARATERECEIPTPAYMENTACTUALBALANCEColumn, "0");
                }

                //On 30/01/2025 filter cost centre if cc budget enabled and cc is selected --------------------------------------------------------
                if (this.AppSetting.EnableCostCentreBudget == 1 && !string.IsNullOrEmpty(this.ReportProperties.CostCentre))
                {
                    dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre);
                }
                //---------------------------------------------------------------------------------------------------------------------------------

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlCCBudgetRealization);
            }
            if (resultArgs.Success && resultArgs.DataSource.Table != null)
            {
                dtCCDetails = resultArgs.DataSource.Table;
            }
        }

        // to align header tables
        public virtual XRTable AlignHeaderTable(XRTable table)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        //if (count == 1)
                        //{
                        //    tcell.Borders = BorderSide.All;
                        //    if (ReportProperties.ShowLedgerCode != 1)
                        //    {
                        //        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                        //    }
                        //}
                        //else if (count == 4)
                        //{
                        //    tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                        //    if (ReportProperties.ShowLedgerCode != 1)
                        //    {
                        //        tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        //    }
                        //}
                        //else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Bottom | BorderSide.Top | BorderSide.Left;
                        else if (count == 4 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else if (count == trow.Cells.Count)
                            tcell.Borders = BorderSide.Bottom | BorderSide.Top | BorderSide.Right;
                        else
                            tcell.Borders = BorderSide.Bottom | BorderSide.Top;

                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.All;
                        else if (count == 4 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;

                    //For FD register, FD history and GST return
                    if (ReportProperties.ReportId == "RPT-094" || ReportProperties.ReportId == "RPT-047" || ReportProperties.ReportId == "RPT-166" || ReportProperties.ReportId == "RPT-181")
                    {
                        tcell.Font = (ReportProperty.Current.ColumnCaptionFontStyle == 0 ?
                                new System.Drawing.Font(tcell.Font, System.Drawing.FontStyle.Bold) : new System.Drawing.Font(tcell.Font, System.Drawing.FontStyle.Regular));
                    }
                    else
                    {
                        tcell.Font = (ReportProperty.Current.ColumnCaptionFontStyle == 0 ?
                                new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))) :
                                new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))));
                    }

                }
            }
            return table;
        }

        // to align content tables
        public virtual XRTable AlignContentTable(XRTable table)
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
                        //if (count == 1)
                        //{
                        //    tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        //    if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                        //    {
                        //        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                        //    }
                        //}
                        //else
                        //{
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        //}
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
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right;
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

        private void SetReportBorder()
        {
            xrtblHeader = AlignHeaderTable(xrtblHeader);
            xrtblDetails = AlignContentTable(xrtblDetails);

            if (this.DataSource != null)
            {
                DataTable dtBudgetDetails = this.DataSource as DataTable;
                bool records = dtBudgetDetails.Rows.Count > 0;
                grpBudgetNature.Visible = grpLGHeader.Visible = grpBudgetGroup.Visible = records;
                grpBudgetNatureFooter.Visible = grpLGFooter.Visible = records;
                Detail.Visible = records;
                ReportFooter.Visible = records;
            }
            else
            {
                Detail.Visible = false;
                grpBudgetNature.Visible = grpLGHeader.Visible = grpBudgetGroup.Visible = false;
                grpBudgetNatureFooter.Visible = grpLGFooter.Visible = false;
                Detail.Visible = false;
                ReportFooter.Visible = false;
            }
        }

        private void xrBudgetGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell label = (XRTableCell)sender;
            string budgetgroup = label.Text;
            label.Text = budgetgroup.ToString().ToUpper();
        }

        private void xrBudgetNature_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell label = (XRTableCell)sender;
            string budgetnature = label.Text;
            label.Text = budgetnature.ToString().ToUpper();
        }

        private void grpBudgetGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataRowView drvcrrentrow = (DataRowView)this.GetCurrentRow();
            if (drvcrrentrow != null)
            {
                string budgetnature = drvcrrentrow["BUDGET_NATURE"].ToString().Trim();
                string budgetgroup = drvcrrentrow["BUDGET_GROUP"].ToString().Trim();
                if (budgetnature.ToUpper() == "INCOME" && string.IsNullOrEmpty(budgetgroup))
                {
                    e.Cancel = true;
                }
                else
                {
                    e.Cancel = false;
                }
            }
        }

        private void xrCellInPercentage_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataRowView drvcrrentrow = (DataRowView)this.GetCurrentRow();
            if (drvcrrentrow != null)
            {
                string budgetnature = drvcrrentrow["BUDGET_NATURE"].ToString().Trim();
                string budgetgroup = drvcrrentrow["BUDGET_GROUP"].ToString().Trim();
                Double approvedamount = UtilityMember.NumberSet.ToDouble(drvcrrentrow["APPROVED_AMOUNT"].ToString());
                Double actualamount = UtilityMember.NumberSet.ToDouble(drvcrrentrow["ACTUAL_AMOUNT"].ToString());
                Double varianceamountamount = UtilityMember.NumberSet.ToDouble(drvcrrentrow["BUDGET_VARIANCE"].ToString());
                Double Percentage_number = UtilityMember.NumberSet.ToDouble(drvcrrentrow["PERCENTAGE_NUMBER"].ToString());

                BudgetAmountbyLG += approvedamount;
                ActualAmountbyLG += actualamount;
                BudgetAmountbyBN += approvedamount;
                ActualAmountbyBN += actualamount;
                
                LedgerGrpPercentageAvg += Percentage_number;
                GrpLedgerCount += 1;

                BudgetNaturePercentageAvg += Percentage_number;
                BudgetNatureLedgerCount += 1;
                XRTableCell xrpercentagecell = ((XRTableCell)sender);
                xrpercentagecell.Font = new Font(xrpercentagecell.Font, FontStyle.Regular);
                xrpercentagecell.ForeColor = Color.Black;
                if (budgetnature.ToUpper() == "INCOME")
                {
                    if (actualamount > approvedamount)
                    {
                        xrpercentagecell.Font = new Font(xrpercentagecell.Font, FontStyle.Bold);
                        xrpercentagecell.ForeColor = Color.Green;
                    }
                }
                else
                {
                    if (actualamount > approvedamount)
                    {
                        xrpercentagecell.Font = new Font(xrpercentagecell.Font, FontStyle.Bold);
                        xrpercentagecell.ForeColor = Color.Red;
                    }
                }

                if (varianceamountamount == 0)
                {
                    //On 12/02/2002
                    xrpercentagecell.Text = "0%";
                }
            }
        }

        private void xrCellInAmt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataRowView drvcrrentrow = (DataRowView)this.GetCurrentRow();
            if (drvcrrentrow != null)
            {
                Double varianceamountamount = UtilityMember.NumberSet.ToDouble(drvcrrentrow["BUDGET_VARIANCE"].ToString());
                if (varianceamountamount == 0)
                {
                    XRTableCell xrvariaincecell = sender as XRTableCell;
                    //xrvariaincecell.Text = string.Empty;
                }
            }
        }

        private void grpLGHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            LedgerGrpPercentageAvg = 0;
            GrpLedgerCount = 0;
        }

        private void xrcellGrpHeadPercentageSum_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double dpercent = Math.Round(((ActualAmountbyLG - BudgetAmountbyLG) / BudgetAmountbyLG) * 100);
            if (double.IsNaN(dpercent))
            {
                e.Result = "0%";// Math.Round((LedgerGrpPercentageAvg / GrpLedgerCount)) + " %";
            }
            else
            {
                e.Result = Math.Round(((ActualAmountbyLG - BudgetAmountbyLG) / BudgetAmountbyLG) * 100) + "%";// Math.Round((LedgerGrpPercentageAvg / GrpLedgerCount)) + " %";
            }
            e.Handled = true;
        }

        private void xrcellPercentageAvg_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = Math.Round(((ActualAmountbyBN - BudgetAmountbyBN) / BudgetAmountbyBN) * 100) + "%"; //Math.Round((BudgetNaturePercentageAvg / BudgetNatureLedgerCount));
            e.Handled = true;
            ActualAmountbyBN = BudgetAmountbyBN = 0;//TO Reset 
        }

        private void grpBudgetNature_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BudgetNaturePercentageAvg= 0;
            BudgetNatureLedgerCount = 0;
        }

        private void xrcellGrpFooterPercentageSum_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double dprecentage = Math.Round(((ActualAmountbyLG - BudgetAmountbyLG) / BudgetAmountbyLG) * 100);
            if (double.IsNaN(dprecentage))
            {
                e.Result = "0%";
            }
            else
            {
                e.Result = Math.Round(((ActualAmountbyLG - BudgetAmountbyLG) / BudgetAmountbyLG) * 100) + "%"; //Math.Round((LedgerGrpPercentageAvg / GrpLedgerCount)) + " %";
            }
            e.Handled = true;
            ActualAmountbyLG = BudgetAmountbyLG = 0; //To Reset 
        }

        private void xrcellGrpHeadActualSum_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (e.CalculatedValues.Count == 0)
            {
                e.Result = 0;
                e.Handled = true;
            }
        }

        private void xrcellGrpFooterActualSum_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (e.CalculatedValues.Count == 0)
            {
                e.Result = 0;
                e.Handled = true;
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ShowCCDetails();
        }

    }
}
