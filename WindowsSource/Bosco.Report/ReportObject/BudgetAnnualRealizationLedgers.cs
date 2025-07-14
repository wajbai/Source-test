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
    public partial class BudgetAnnualRealizationLedgers : Bosco.Report.Base.ReportHeaderBase
    {
        ResultArgs resultArgs = null;
        private int NoOfMonths = 1;
        public bool HeaderLoaded =false;
        
        private TransMode BudgetTransSource = TransMode.CR;
        
        public Double GrandIncomeApprovedTotal = 0;
        public Double GrandIncomeDueTotal = 0;
        public Double GrandIncomeDifferenceTotal = 0;
                        
        public Double GrandExpenseApprovedTotal = 0;
        public Double GrandExpenseDueTotal = 0;
        public Double GrandExpenseDifferenceTotal = 0;

        private DataTable dtCCDetails = new DataTable();
        private bool PrevLedgerCCFound = false;

        public BudgetAnnualRealizationLedgers()
        {
            InitializeComponent();
        }
        #region Show Reports
        public override void ShowReport()
        {
            BindBudgetAnnualMonthlyRealization();
        }
        #endregion

        private void BindBudgetAnnualMonthlyRealization()
        {

            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo) || string.IsNullOrEmpty(this.ReportProperties.Project) || this.ReportProperties.Project.Split(',').Length == 0)
            {
                ShowReportFilterDialog();
            }
            else
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        //BindBudgetDetails();
                        //base.ShowReport();
                    }
                    else
                    {
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    //BindBudgetDetails();
                    //base.ShowReport();
                }
            }
        }

        public void BindBudgetDetails(DataTable dtRptBudgetSource, TransMode BudgetSource)
        {
            try
            {
                float actualCodeWidth = xrcellCode.WidthF;
                bool isCapCodeVisible = true;

                this.BudgetName = ReportProperty.Current.BudgetName;
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                this.HideReportHeader = this.HidePageFooter = this.HidePageFooter = HidePageInfo = false;
                BudgetTransSource = BudgetSource;
                //SetReportTitle();
                //AssignBudgetDateRangeTitle();
                //SetTitleWidth(xrtblHeader.WidthF-50);
                
                //to get CC detail for Budget, it will be used to when reports generates
                xrSubreportCCDetails.Visible = false;
                if (this.ReportProperties.ShowCCDetails == 1)
                {
                    xrSubreportCCDetails.Visible = true;
                    AssignCCDetailReportSource();
                }

                //setHeaderTitleAlignment();
                MakeCaption();
                                
                //fix always sort by ledget name for cmf
                if (this.AppSetting.IS_CMF_CONGREGATION)
                {
                    this.FixReportPropertyForCMF();
                    xrcellGrandTotal.Text = xrcellGrandTotal.Text.ToUpper();
                }

                isCapCodeVisible = (ReportProperties.ShowGroupCode == 1 || ReportProperties.ShowLedgerCode == 1);                
                xrcellCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);
                xrcellHeaderCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);

                grpHeaderLedgerGroup.SortingSummary.Enabled = true;
                grpHeaderLedgerGroup.SortingSummary.FieldName = this.reportSetting1.BUDGETVARIANCE.NATURE_IDColumn.ColumnName;
                grpHeaderLedgerGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                grpHeaderLedgerGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;

                if (this.ReportProperties.SortByLedger == 1)
                    Detail.SortFields.Add(new GroupField(this.reportSetting1.BUDGETVARIANCE.LEDGER_NAMEColumn.ColumnName));
                else
                    Detail.SortFields.Add(new GroupField(this.reportSetting1.BUDGETVARIANCE.LEDGER_CODEColumn.ColumnName));

                if (dtRptBudgetSource != null)
                {
                    /*if (BudgetTransSource == TransMode.CR)
                    {
                        dtRptBudgetSource.Columns["AMOUNT_CR"].ColumnName = this.reportSetting1.BUDGETVARIANCE.ACTUAL_AMOUNTColumn.ColumnName;
                    }
                    else
                    {
                        dtRptBudgetSource.Columns["AMOUNT_DR"].ColumnName = this.reportSetting1.BUDGETVARIANCE.ACTUAL_AMOUNTColumn.ColumnName; ;
                    }*/
                    SplitReportDatasourceQuter(dtRptBudgetSource);

                    string filter = "(" + this.reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName + " > 0 "+
                                    " OR " + this.reportSetting1.BUDGETVARIANCE.ACTUAL_AMOUNTColumn.ColumnName + " <> 0 " + ")";
                    dtRptBudgetSource.DefaultView.RowFilter = filter;
                    dtRptBudgetSource = dtRptBudgetSource.DefaultView.ToTable();

                    //To show only Budgeted Ledgers alone (based on settings)
                    if (this.ReportProperties.IncludeAllLedger == 0)
                    {
                        filter = "(" + this.reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName + " > 0 OR " +
                                     this.reportSetting1.Receipts.GROUP_IDColumn.ColumnName + " = " +(int)FixedLedgerGroup.Cash  + " OR " +
                                     this.reportSetting1.Receipts.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.BankAccounts + " OR " +
                                     this.reportSetting1.Receipts.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.FixedDeposit + ")";

                        //On 19/03/2021, to filter only mapped Budget Ledger
                        //To show only Budgeted Ledgers alone (based on settings)
                        filter += " AND " + this.reportSetting1.BUDGETVARIANCE.TRANS_MODEColumn.ColumnName + "='" + BudgetSource + "'";

                        dtRptBudgetSource.DefaultView.RowFilter = filter;
                        dtRptBudgetSource = dtRptBudgetSource.DefaultView.ToTable();
                    }

                    if (BudgetTransSource == TransMode.CR)
                    {
                        GrandIncomeApprovedTotal = UtilityMember.NumberSet.ToDouble(dtRptBudgetSource.Compute("SUM(" + this.reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                        GrandIncomeDifferenceTotal= UtilityMember.NumberSet.ToDouble(dtRptBudgetSource.Compute("SUM(" + this.reportSetting1.BUDGETVARIANCE.DIFFERENCEColumn.ColumnName + ")", string.Empty).ToString());
                        GrandIncomeDueTotal = UtilityMember.NumberSet.ToDouble(dtRptBudgetSource.Compute("SUM(" + this.reportSetting1.BUDGETVARIANCE.DUE_MONTHS_APPROVED_AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                    }
                    else if (BudgetTransSource == TransMode.DR)
                    {
                        GrandExpenseApprovedTotal = UtilityMember.NumberSet.ToDouble(dtRptBudgetSource.Compute("SUM(" + this.reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                        GrandExpenseDifferenceTotal = UtilityMember.NumberSet.ToDouble(dtRptBudgetSource.Compute("SUM(" + this.reportSetting1.BUDGETVARIANCE.DIFFERENCEColumn.ColumnName + ")", string.Empty).ToString());
                        GrandExpenseDueTotal = UtilityMember.NumberSet.ToDouble(dtRptBudgetSource.Compute("SUM(" + this.reportSetting1.BUDGETVARIANCE.DUE_MONTHS_APPROVED_AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                    }
                    
                    this.DataSource = dtRptBudgetSource;
                    this.DataMember = dtRptBudgetSource.TableName;
                }
                else
                {
                    this.DataSource = null;
                }

                SetReportBorder();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, false);
            }
            finally { }
        }

        private void ShowCCDetails()
        {
            //On 25/02/2021, To show CC detail for given Ledger
            if (this.ReportProperties.ShowCCDetails == 1)
            {
                if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName) != null && dtCCDetails.Rows.Count > 0)
                {
                    Int32 ledgerid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName).ToString());
                    
                    BudgetAnnualRealizationCC ccDetail = xrSubreportCCDetails.ReportSource as BudgetAnnualRealizationCC;
                    dtCCDetails.DefaultView.RowFilter = string.Empty;
                    dtCCDetails.DefaultView.RowFilter = reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName + " = " + ledgerid + " AND " +
                                                    reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName + " = '" + BudgetTransSource.ToString() + "'";
                    dtCCDetails.DefaultView.Sort = reportSetting1.MonthlyAbstract.COST_CENTRE_NAMEColumn.ColumnName;
                    DataTable dtCC = dtCCDetails.DefaultView.ToTable();

                    ccDetail.BindCCDetails(dtCC);
                    ccDetail.CCTableWidth = xrtblBudget.WidthF;
                    ccDetail.CodeWidth = xrcellCode.WidthF;
                    ccDetail.CCNameWidth = xrcellParticular.WidthF;
                    ccDetail.CCApprovedAmount = xrcellApproved.WidthF;
                    ccDetail.CCRealizedAmount= xrcellRealized.WidthF;
                    ccDetail.CCDifferenceAmount= xrCellDifference.WidthF;
                    ccDetail.Note= xrCellNote.WidthF;
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
                xrcellParticular.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                xrcellApproved.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                xrcellRealized.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                xrCellDueMonths.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                xrCellDifference.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                xrCellNote.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
            }
            else
            {
                xrcellCode.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xrcellParticular.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xrcellApproved.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xrCellDueMonths.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xrcellRealized.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xrCellDifference.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xrCellNote.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
            }
        }

        private void AssignCCDetailReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlCCBudgetRealization = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetCCRealization);
            
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_FROMColumn, this.AppSetting.YearFrom);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_TOColumn, this.AppSetting.YearTo);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, BudgetTransSource.ToString());

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
              
        private void SetReportBorder()
        {
            xrtblHeader = AlignHeaderTable(xrtblHeader);
            xrtblBudget = AlignContentTable(xrtblBudget);
            xrTblBudgetNature = AlignContentTable(xrTblBudgetNature);

            xrTblLedgerGroup = AlignGrandTotalTable(xrTblLedgerGroup);
            xrTblFooterLedgerGroup = AlignGrandTotalTable(xrTblFooterLedgerGroup);
            xrTblBudgetNature = AlignContentTable(xrTblBudgetNature);
            xrTblFooterBudgetNature = AlignContentTable(xrTblFooterBudgetNature);
            xrTblGrandFooterTotal = AlignGrandTotalTable(xrTblGrandFooterTotal);
            float actualCodeWidth = xrcellHeaderCode.WidthF;
            bool isCapCodeVisible = true;
            isCapCodeVisible = (ReportProperties.ShowLedgerCode == 1);
            xrcellCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);

            if (this.DataSource != null)
            {
                DataTable dtBudgetDetails = this.DataSource as DataTable;
                bool records = dtBudgetDetails.Rows.Count > 0;
                grpHeaderBudgetNature.Visible = grpHeaderLedgerGroup.Visible = records;
                grpFooterBudgetNature.Visible = grpFooterLedgerGroup.Visible = records;
                Detail.Visible = records;
                ReportFooter.Visible = records;
            }
            else
            {
                Detail.Visible = false;
                grpHeaderBudgetNature.Visible = grpHeaderLedgerGroup.Visible = false;
                grpFooterBudgetNature.Visible = grpFooterLedgerGroup.Visible = false;
                Detail.Visible = false;
                ReportFooter.Visible = false;
            }
        }

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
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.All;
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
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;

                    tcell.Font = (ReportProperty.Current.ColumnCaptionFontStyle == 0 ?
                                new System.Drawing.Font(tcell.Font, System.Drawing.FontStyle.Bold) : new System.Drawing.Font(tcell.Font, System.Drawing.FontStyle.Regular));
                    
                }
            }
            return table;
        }

        /// <summary>
        /// Change Captions based on Budget
        /// </summary>
        private void MakeCaption()
        {
            NoOfMonths = this.UtilityMember.DateSet.GetDateDifferentInMonths(this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false), this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateTo, false));
            xrcellCurrentHeaderApproved.Multiline = true;
            xrcellCurrentHeaderApproved.Text = "Approved\r\nBudget - " + this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Year.ToString();
            
            xrcellHeaderDueMonths.Text = "Due -" + NoOfMonths + "\r\nMonths";
            //xrcellHeaderRealized.Text = "Realized as on\r\n" + this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateTo, true).ToShortDateString();
            xrcellHeaderRealized.Text = "Realized";
            int StartQuater = this.UtilityMember.DateSet.GetQuarter(this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false),  this.AppSetting.YearFrom);
            int EndQuater = this.UtilityMember.DateSet.GetQuarter(this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateTo, false), this.AppSetting.YearFrom);
        }

        /// <summary>
        /// Split Report datasource based on FYs
        /// </summary>
        /// <param name="dtReport"></param>
        private void SplitReportDatasourceQuter(DataTable dtReport)
        {
            NoOfMonths = this.UtilityMember.DateSet.GetDateDifferentInMonths(this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false), this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateTo, false));
            if (dtReport!=null)
            {
                string colgroupid = reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName;
                string colannualapprovedamount = reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName;
                string annaulbugetQutersplit = "(" + colannualapprovedamount + "/12)";
                string annaulbugetDuesplit = "(" + colannualapprovedamount + "/12)*" + NoOfMonths;
                
                if (!dtReport.Columns.Contains(reportSetting1.BUDGETVARIANCE.DUE_MONTHS_APPROVED_AMOUNTColumn.ColumnName))
                {
                    annaulbugetDuesplit = "IIF( (" + colgroupid + "=" + (Int32)FixedLedgerGroup.Cash + " OR " + colgroupid + "=" + (Int32)FixedLedgerGroup.BankAccounts + " OR "
                                            + colgroupid + "=" + (Int32)FixedLedgerGroup.FixedDeposit + ")," + colannualapprovedamount + ", (" + annaulbugetDuesplit + "))";
                    dtReport.Columns.Add(reportSetting1.BUDGETVARIANCE.DUE_MONTHS_APPROVED_AMOUNTColumn.ColumnName, typeof(double), annaulbugetDuesplit);

                    if (dtCCDetails != null && dtCCDetails.Rows.Count > 0 && !dtCCDetails.Columns.Contains(reportSetting1.BUDGETVARIANCE.DUE_MONTHS_APPROVED_AMOUNTColumn.ColumnName))
                    {
                        dtCCDetails.Columns.Add(reportSetting1.BUDGETVARIANCE.DUE_MONTHS_APPROVED_AMOUNTColumn.ColumnName, typeof(double), annaulbugetDuesplit);
                    }
                }
                                
                if (!dtReport.Columns.Contains(reportSetting1.BUDGETVARIANCE.DIFFERENCEColumn.ColumnName))
                {
                    string isFixedlegerGroups = "(" + reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.Cash + " OR " +
                                                 reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.BankAccounts + " OR " +
                                                 reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.FixedDeposit + ")";
                    //string difference = reportSetting1.BUDGETVARIANCE.DUE_MONTHS_APPROVED_AMOUNTColumn.ColumnName + " - " + reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName;
                    string difference = reportSetting1.BUDGETVARIANCE.ACTUAL_AMOUNTColumn.ColumnName + " - " + reportSetting1.BUDGETVARIANCE.DUE_MONTHS_APPROVED_AMOUNTColumn.ColumnName;
                    //difference = "IIF(" + isFixedlegerGroups + ", (" + reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName + "), " + difference + ")";

                    dtReport.Columns.Add(reportSetting1.BUDGETVARIANCE.DIFFERENCEColumn.ColumnName, typeof(double), difference);

                    if (dtCCDetails != null && dtCCDetails.Rows.Count>0 && !dtCCDetails.Columns.Contains(reportSetting1.BUDGETVARIANCE.DIFFERENCEColumn.ColumnName))
                    {
                        dtCCDetails.Columns.Add(reportSetting1.BUDGETVARIANCE.DIFFERENCEColumn.ColumnName, typeof(double), difference);
                    }
                }

                if (dtReport.Rows.Count > 0)
                {
                    AppendCashBankFDBalances(dtReport);
                }

                //do round off near 100 for temp purpose------------
                //if (!dtReport.Columns.Contains(reportSetting1.BUDGETVARIANCE.ROUND_ACTUAL_AMOUNTColumn.ColumnName))
                //{
                //    dtReport.Columns.Add(reportSetting1.BUDGETVARIANCE.ROUND_ACTUAL_AMOUNTColumn.ColumnName, typeof(Int32));
                //}
                
                //foreach (DataRow dr in dtReport.Rows)
                //{
                //    double amt = UtilityMember.NumberSet.ToDouble(dr[reportSetting1.BUDGETVARIANCE.ACTUAL_AMOUNTColumn.ColumnName].ToString());
                //    dr[reportSetting1.BUDGETVARIANCE.ROUND_ACTUAL_AMOUNTColumn.ColumnName] = Math.Round(amt / 100, MidpointRounding.AwayFromZero) * 100; // Math.Round(amt, 100);
                //}
                //--------------------------------------------------

                DataColumn dcBudgetNature = new DataColumn(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName, typeof(System.String));
                if (BudgetTransSource == TransMode.CR)
                {
                    string budgetnature = "IIF( (" + colgroupid + "=" + (Int32)FixedLedgerGroup.Cash + " OR " + colgroupid + "=" + (Int32)FixedLedgerGroup.BankAccounts + " OR "
                                            + colgroupid + "=" + (Int32)FixedLedgerGroup.FixedDeposit + "),'OPENING BALANCE', 'INCOME')";
                    dcBudgetNature.Expression = budgetnature;
                }
                else
                {
                    string budgetnature = "IIF( (" + colgroupid + "=" + (Int32)FixedLedgerGroup.Cash + " OR " + colgroupid + "=" + (Int32)FixedLedgerGroup.BankAccounts + " OR "
                                            + colgroupid + "=" + (Int32)FixedLedgerGroup.FixedDeposit + "),'CLOSING BALANCE', 'EXPENDITURE')";
                    dcBudgetNature.Expression = budgetnature;
                }
                dtReport.Columns.Add(dcBudgetNature);
            }
        }

        /// <summary>
        /// Attach Cash, Bank and FD ledgers opening and closing
        /// 
        /// If HO is CMF, take all Projects  Cash, Bank and FD ledgers opening and closing
        /// </summary>
        /// <param name="dtReport"></param>
        private void AppendCashBankFDBalances(DataTable dtReport)
        {
            //if (this.ReportProperties.ShowDetailedBalance == 1)
            //{
                if (dtReport != null)
                {
                    //For Opening and Closing balances (Current FY and Previous Year)
                    if (BudgetTransSource == TransMode.CR)
                    {
                        AttachBalancesForBudget(dtReport, true, false, false);
                    }
                    else if (AppSetting.IS_CMF_CONGREGATION && BudgetTransSource == TransMode.DR) //AppendCashBankFDBalances(dtBudgetLedgers);
                    {
                        AttachBalancesForBudget(dtReport, false, false, false);
                    }
                    //AttachQuaterCashBankFDBalances(dtReport);
                }
            //}
        }

       
        private void xrBudgetNature_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell label = (XRTableCell)sender;
            string budgetnature = label.Text;
            label.Text = budgetnature.ToString().ToUpper();

            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName) != null)
            {
                //string budgetnature = GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName).ToString();
                XRTableCell cell = sender as XRTableCell;
                cell.BackColor = Color.Silver;
                if (budgetnature.ToUpper() == "INCOME" || budgetnature.ToUpper() == "EXPENDITURE")
                {
                    cell.BackColor = Color.Transparent;
                }
            }
        }

        private void xrCellBNTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName) != null)
            {
                XRTableCell cell = sender as XRTableCell;
                if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit))
                        cell.Text = (BudgetTransSource == TransMode.CR ? "Total Opening Balance" : "Total Closing Balance");//Sub Group Total";
                    else
                        cell.Text = "Total";
                }
            }
        }
              

        private void xrcellLedgerGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.LEDGER_GROUPColumn.ColumnName)!=null)
            {
               string lgname =  GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.LEDGER_GROUPColumn.ColumnName).ToString().Trim();
               (sender as XRTableCell).Text = lgname;
            }
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //e.Cancel = (BudgetTransSource == TransMode.DR && !HeaderLoaded);
            //HeaderLoaded = true;
            e.Cancel = (BudgetTransSource == TransMode.DR);
        }

        private void grpHeaderLedgerGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            //{
            //    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
            //    if (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit))
            //        e.Cancel = true;
            //    else
            //        e.Cancel = false;
            //}
        }

        private void grpFooterLedgerGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            //{
            //    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
            //    if (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit))
            //        e.Cancel = true;
            //    else
            //        e.Cancel = false;
            //}
        }

        private void grpFooterBudgetNature_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                if (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit))
                    e.Cancel = false;
                else
                    e.Cancel = true;
            }
        }

        private void xrCellNote_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            //{
            //    XRTableCell cell = sender as XRTableCell;
            //    cell.Text = string.Empty;

            //    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
            //    if (BudgetTransSource == TransMode.CR && (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
            //    {
            //        cell.Text = "1";
            //    }
            //}

            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.NARRATIONColumn.ColumnName) != null)
            {
                if (AppSetting.IS_CMF_CONGREGATION)
                {
                    (sender as XRTableCell).TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                }
            }
        }

        private void xrcellGSumApprovedAmount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                if (BudgetTransSource == TransMode.DR)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (BudgetTransSource == TransMode.DR && (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
                    {
                        double closingbalance = GrandIncomeApprovedTotal - GrandExpenseApprovedTotal;
                        e.Result = GrandExpenseApprovedTotal + closingbalance;
                        e.Handled = true;
                    }
                }
            }
        }

        private void xrcellGSumDueMonths_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                if (BudgetTransSource == TransMode.DR)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (BudgetTransSource == TransMode.DR && (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
                    {
                        double closingbalance = GrandIncomeDueTotal - GrandExpenseDueTotal;
                        e.Result = GrandExpenseDueTotal + closingbalance;
                        e.Handled = true;
                    }
                }
            }
        }

        private void xrcellGSumDifference_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                if (BudgetTransSource == TransMode.DR)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (BudgetTransSource == TransMode.DR && (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
                    {
                        double closingbalance = GrandIncomeDifferenceTotal - GrandExpenseDifferenceTotal;
                        e.Result = GrandExpenseDifferenceTotal + closingbalance;
                        e.Handled = true;
                    }
                }
            }
        }

        private void xrcellBNSumApprovedAmount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                if (BudgetTransSource == TransMode.DR)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (BudgetTransSource == TransMode.DR && (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
                    {
                        e.Result = GrandIncomeApprovedTotal - GrandExpenseApprovedTotal;
                        e.Handled = true;
                    }
                }
            }
        }

        private void xrcellBNSumDueMonths_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                if (BudgetTransSource == TransMode.DR)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (BudgetTransSource == TransMode.DR && (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
                    {
                        e.Result = GrandIncomeDueTotal - GrandExpenseDueTotal;
                        e.Handled = true;
                    }
                }
            }
        }

        private void xrcellBNSumDifference_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                if (BudgetTransSource == TransMode.DR)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (BudgetTransSource == TransMode.DR && (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
                    {
                        e.Result = GrandIncomeDifferenceTotal - GrandExpenseDifferenceTotal;
                        e.Handled = true;
                    }
                }
            }
        }

        private void xrcellBNApproved_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName) != null)
            {
                string budgetnature = GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName).ToString();
                XRTableCell cell = sender as XRTableCell;
                cell.BackColor = Color.Silver;
                if (budgetnature.ToUpper() == "INCOME" || budgetnature.ToUpper() == "EXPENDITURE")
                {
                    cell.BackColor = Color.Transparent;
                }
            }
        }

        private void xrCellBNDueMonths_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName) != null)
            {
                string budgetnature = GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName).ToString();
                XRTableCell cell = sender as XRTableCell;
                cell.BackColor = Color.Silver;
                if (budgetnature.ToUpper() == "INCOME" || budgetnature.ToUpper() == "EXPENDITURE")
                {
                    cell.BackColor = Color.Transparent;
                }
            }
        }

        private void xrcellBNRealized_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName) != null)
            {
                string budgetnature = GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName).ToString();
                XRTableCell cell = sender as XRTableCell;
                cell.BackColor = Color.Silver;
                if (budgetnature.ToUpper() == "INCOME" || budgetnature.ToUpper() == "EXPENDITURE")
                {
                    cell.BackColor = Color.Transparent;
                }
            }
        }

        private void xrCellBNDifference_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName) != null)
            {
                string budgetnature = GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName).ToString();
                XRTableCell cell = sender as XRTableCell;
                cell.BackColor = Color.Silver;
                if (budgetnature.ToUpper() == "INCOME" || budgetnature.ToUpper() == "EXPENDITURE")
                {
                    cell.BackColor = Color.Transparent;
                }
            }
        }

      

        private void xrcellBNApproved_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                if (BudgetTransSource == TransMode.DR)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (BudgetTransSource == TransMode.DR && (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
                    {
                        e.Result = GrandIncomeApprovedTotal - GrandExpenseApprovedTotal;
                        e.Handled = true;
                    }
                }

                if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName) != null)
                {
                    string budgetnature = GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName).ToString();
                    if (budgetnature.ToUpper() == "INCOME" || budgetnature.ToUpper() == "EXPENDITURE")
                    {
                        e.Result = "";
                        e.Handled = true;
                    }
                }
            }
        }

        private void xrCellBNDueMonths_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                if (BudgetTransSource == TransMode.DR)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (BudgetTransSource == TransMode.DR && (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
                    {
                        e.Result = GrandIncomeDueTotal - GrandExpenseDueTotal;
                        e.Handled = true;
                    }
                }

                if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName) != null)
                {
                    string budgetnature = GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName).ToString();
                    if (budgetnature.ToUpper() == "INCOME" || budgetnature.ToUpper() == "EXPENDITURE")
                    {
                        e.Result = "";
                        e.Handled = true;
                    }
                }
            }
        }

        private void xrCellBNDifference_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                if (BudgetTransSource == TransMode.DR)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (BudgetTransSource == TransMode.DR && (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
                    {
                        e.Result = GrandIncomeDifferenceTotal - GrandExpenseDifferenceTotal;
                        e.Handled = true;
                    }
                }

                if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName) != null)
                {
                    string budgetnature = GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName).ToString();
                    if (budgetnature.ToUpper() == "INCOME" || budgetnature.ToUpper() == "EXPENDITURE")
                    {
                        e.Result = "";
                        e.Handled = true;
                    }
                }
            }
        }

        private void xrcellBNRealized_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName) != null)
            {
                string budgetnature = GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName).ToString();
                if (budgetnature.ToUpper() == "INCOME" || budgetnature.ToUpper() == "EXPENDITURE")
                {
                    e.Result = "";
                    e.Handled = true;
                }
            }
        }

        private void xrTblBudgetNature_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //xrBudgetNature.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName) != null)
            {
                string budgetnature = GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName).ToString();
                if (budgetnature.ToUpper() == "INCOME" || budgetnature.ToUpper() == "EXPENDITURE")
                {
                    //if (AppSetting.IS_CMF_CONGREGATION)
                    //{
                    //    xrBudgetNature.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    //}

                    if (xrBNHeaderRow.Cells.Contains(xrcellBNApproved))
                    {
                        xrBNHeaderRow.Cells.Remove(xrcellBNApproved);
                    }

                    if (xrBNHeaderRow.Cells.Contains(xrCellBNDueMonths))
                    {
                        xrBNHeaderRow.Cells.Remove(xrCellBNDueMonths);
                    }

                    if (xrBNHeaderRow.Cells.Contains(xrcellBNRealized))
                    {
                        xrBNHeaderRow.Cells.Remove(xrcellBNRealized);
                    }

                    if (xrBNHeaderRow.Cells.Contains(xrCellBNDifference))
                    {
                        xrBNHeaderRow.Cells.Remove(xrCellBNDifference);
                    }

                    if (xrBNHeaderRow.Cells.Contains(xrcellBNNote))
                    {
                        xrBNHeaderRow.Cells.Remove(xrcellBNNote);
                    }
                }
            }
        }

        private void xrcellBNNote_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName) != null)
            {
                string budgetnature = GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName).ToString();
                XRTableCell cell = sender as XRTableCell;
                cell.BackColor = Color.Silver;
                if (budgetnature.ToUpper() == "INCOME" || budgetnature.ToUpper() == "EXPENDITURE")
                {
                    cell.BackColor = Color.Transparent;
                }
            }
        }

        private void xrcellLGTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName) != null)
            {
                XRTableCell cell = sender as XRTableCell;
                if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (activegroupid == ((int)FixedLedgerGroup.Cash))
                        cell.Text = "Total Cash";
                    else if (activegroupid == (int)FixedLedgerGroup.BankAccounts)
                        cell.Text = "Total Bank";
                    else if (activegroupid == (int)FixedLedgerGroup.FixedDeposit)
                        cell.Text = "Total FD";
                    else
                        cell.Text = "Sub Group Total";
                }
            }
        }

        private void xrcellLGSumDueMonths_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //if (AppSetting.IS_CMF_CONGREGATION && BudgetTransSource == TransMode.DR)
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                if (AppSetting.IS_CMF_CONGREGATION && BudgetTransSource == TransMode.DR)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (BudgetTransSource == TransMode.DR && (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
                    {
                        e.Result = "";
                        e.Handled = true;
                    }
                }
            }
        }

        private void xrcellLGSumDifference_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //if (AppSetting.IS_CMF_CONGREGATION && BudgetTransSource == TransMode.DR)
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName)!=null)
            {
                if (AppSetting.IS_CMF_CONGREGATION && BudgetTransSource == TransMode.DR)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (BudgetTransSource == TransMode.DR && (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
                    {
                        e.Result = "";
                        e.Handled = true;
                    }
                }
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ShowCCDetails();
        }

        
    }
}
