using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class BudgetOpClosingBalance : Report.Base.ReportBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public BudgetOpClosingBalance()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public float GroupNameWidth
        {
            set { tcGroupName.WidthF = value; }
        }

        public float NameColumnWidth
        {
            set { tcLedgerName.WidthF = value; }
        }

        public float AmountColumnWidth
        {
            set { tcAmountPeriod.WidthF = value; }
        }
        public float GroupAmountWidth
        {
            set { tcGroupAmountPeriod.WidthF = value; }
        }

        public float GroupCodeWith
        {
            set { tcGroupCode.WidthF = value; }
        }
        public float LedgerCodeWith
        {
            set { tcLedgerCode.WidthF = value; }
        }
        public float BudgetAmount
        {
            set { tcAmountProgress.WidthF = value; }
        }
        public float GroupBudgetAmount
        {
            set { tcGroupAmountProgress.WidthF = value; }
        }
        #endregion

        #region Methods
        public void BindBalance(bool isOpBalance, string budgetDate, string budgetCompare)
        {
            string dateFrom = ReportProperties.DateFrom;
            string dateTo = ReportProperties.DateTo;
            string balDate = "";
            string projectIds = ReportProperties.Project;
            string groupIds = this.GetLiquidityGroupIds();
            if (isOpBalance)
            {
                DateTime dateBalance = DateTime.Parse(budgetCompare).AddDays(-1);
                balDate = dateBalance.ToShortDateString();
            }
            else
            {
                balDate = budgetCompare;
            }
            resultArgs = GetBalance(balDate, budgetDate, projectIds, groupIds);
            //Include / Exclude Ledger group or Ledger
            grpBalanceGroup.Visible = true;
            grpBalanceLedger.Visible = (ReportProperties.ShowDetailedBalance == 1);
            xrTableLedgerGroup = AlignGroupTable(xrTableLedgerGroup);
            xrtblLedger = AlignContentTable(xrtblLedger);
            if (resultArgs != null && resultArgs.Success)
            {
                DataTable dtOpBalance = resultArgs.DataSource.Table;
                dtOpBalance.TableName = "AccountBalance";
                this.DataSource = dtOpBalance;
                this.DataMember = dtOpBalance.TableName;
            }
        }

        private ResultArgs GetBalance(string balDate, string budgetDate, string projectIds, string groupIds)
        {
            string BudgetSummary = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetOpBalance);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, projectIds);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, groupIds);
                dataManager.Parameters.Add(this.ReportParameters.BALANCE_DATEColumn, balDate);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, budgetDate);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, BudgetSummary);
            }
            return resultArgs;
        }

        // to align group tables
        public override XRTable AlignGroupTable(XRTable table)
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
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Top;
                            if (count == 1 && ReportProperties.ShowGroupCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Top;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowGroupCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                        else if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Top;
                        else if (count == trow.Cells.Count)
                            tcell.Borders = BorderSide.Right | BorderSide.Top;
                        else
                            tcell.Borders = BorderSide.Top;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Top;
                            if (count == 1 && ReportProperties.ShowGroupCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Top;
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

        // to align content tables
        public override XRTable AlignContentTable(XRTable table)
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
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Top;
                            if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Top;
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
                            tcell.Borders = BorderSide.Left | BorderSide.Top;
                            if (count == trow.Cells.Count)
                            {
                                tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Top;
                            }
                        }
                        else if (count == trow.Cells.Count)
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Top;
                        }

                        else
                        {
                            tcell.Borders = BorderSide.Top;
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
        #endregion

    }
}
