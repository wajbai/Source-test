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
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;

namespace Bosco.Report.ReportObject
{
    public partial class CostCentreMontlyAbstractReceipts : Bosco.Report.Base.ReportHeaderBase
    {

        #region ShowReport

        public override void ShowReport()
        {
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom) || String.IsNullOrEmpty(this.ReportProperties.DateTo)
               || this.ReportProperties.Project == "0" || string.IsNullOrEmpty(this.ReportProperties.CostCentre) || this.ReportProperties.CostCentre == "0")
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                SplashScreenManager.ShowForm(typeof(frmReportWait));
                BindReceiptSource();
                SplashScreenManager.CloseForm();
                base.ShowReport();
            }

           // base.ShowReport();
        }

        #endregion

        #region Methods

        public void HideReportHeaderFooter()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
        }

        public void BindReceiptSource()
        {
            //  this.ReportTitle = ReportProperty.Current.ReportTitle;
            // this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
            setHeaderTitleAlignment();
            SetReportTitle();
            this.CosCenterName = ReportProperty.Current.CostCentreName;
            // this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + this.ReportProperties.DateFrom + " - " + this.ReportProperties.DateTo;
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            ResultArgs resultArgs = GetReportSource();
            DataView dvReceipt = resultArgs.DataSource.TableView;

            if (dvReceipt != null)
            {
                dvReceipt.Table.TableName = "MonthlyAbstract";
                this.DataSource = dvReceipt;
                this.DataMember = dvReceipt.Table.TableName;
            }
            SetReportBorder();
            
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlMonthlyAbstractReceipts = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.MonthlyAbstract);
            string dateProgress = this.GetProgressiveDate(this.ReportProperties.DateFrom);
            string liquidityGroupIds = this.GetLiquidityGroupIds();

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_PROGRESS_FROMColumn, dateProgress);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, TransType.RC.ToString());
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, liquidityGroupIds);
                dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre != null ? this.ReportProperties.CostCentre : "0");
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, sqlMonthlyAbstractReceipts);
            }

            return resultArgs;
        }

        private void SetReportSetting(DataView dvReceipt, AccountBalance accountBalance)
        {
            float actualCodeWidth = tcCapCosCode.WidthF;
            bool isCapCodeVisible = true;

            tcCapCosAmountPeriod.Text = this.SetCurrencyFormat(tcCapCosAmountPeriod.Text);
            tcCapAmountProgress.Text = this.SetCurrencyFormat(tcCapAmountProgress.Text);

            //Attach / Detach all ledgers
            dvReceipt.RowFilter = "";
            if (ReportProperties.IncludeAllLedger == 0)
            {
                dvReceipt.RowFilter = reportSetting1.MonthlyAbstract.HAS_TRANSColumn.ColumnName + " = 1";
            }

            //Include / Exclude Code
            if (tcCapCosCode.Tag != null && tcCapCosCode.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(tcCapCosCode.Tag.ToString());
            }
            else
            {
                tcCapCosCode.Tag = tcCapCosCode.WidthF;
            }

            isCapCodeVisible = (ReportProperties.ShowGroupCode == 1 || ReportProperties.ShowLedgerCode == 1);
            tcCapCosCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            tcCosGrpGroupCode.WidthF = ((ReportProperties.ShowGroupCode == 1) ? actualCodeWidth : 0); ;
            tcCosLedgerCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);

            //Include / Exclude Ledger group or Ledger
            grpLedgerGroup.Visible = (ReportProperties.ShowByLedgerGroup == 1);
            grpLedger.Visible = (ReportProperties.ShowByLedger == 1);
            grpLedgerGroup.GroupFields[0].FieldName = "";
            grpLedger.GroupFields[0].FieldName = "";

            if (grpLedgerGroup.Visible == false && grpLedger.Visible == false)
            {
                grpLedger.Visible = true;
            }

            if (grpLedgerGroup.Visible)
            {
                if (ReportProperties.SortByGroup == 1)
                {
                    grpLedgerGroup.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName;

                }
                else
                {
                    grpLedgerGroup.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.GROUP_CODEColumn.ColumnName;
                }
            }

            if (grpLedger.Visible)
            {
                if (ReportProperties.SortByLedger == 1)
                {
                    grpLedger.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.LEDGER_NAMEColumn.ColumnName;
                }
                else
                {
                    grpLedger.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.LEDGER_CODEColumn.ColumnName;
                }
            }

            //Include / Exclude Report Lines

            //Sub Group Row Style
            if (ReportProperties.ShowVerticalLine == 1 && ReportProperties.ShowHorizontalLine == 1)
            {
                styleRow.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
            }
            else if (ReportProperties.ShowVerticalLine == 1)
            {
                styleRow.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right;
            }
            else if (ReportProperties.ShowHorizontalLine == 1)
            {
                styleRow.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            }
            else
            {
                styleRow.Borders = DevExpress.XtraPrinting.BorderSide.None;
            }

            //Group Row Style
            if (grpLedger.Visible == false)
            {
                styleGroupRow.BackColor = Color.White;
                styleGroupRow.Borders = styleRow.Borders;
                xrTableLedgerGroup.StyleName = styleGroupRow.Name;
            }
            else
            {
                xrTableLedgerGroup.StyleName = styleGroupRowBase.Name;
            }

            //Set Subreport Properties
            accountBalance.LeftPosition = (xrTableHeader.LeftF - 5);
            accountBalance.CodeColumnWidth = tcCapCosCode.WidthF;
            accountBalance.NameColumnWidth = tcCapCosParticulars.WidthF;
            accountBalance.AmountColumnWidth = tcCapCosAmountPeriod.WidthF;
            accountBalance.AmountProgressiveColumnWidth = tcCapAmountProgress.WidthF;
        }

        private void SetReportBorder()
        {
            xrTableHeader = SetHeadingTableBorder(xrTableHeader, ReportProperties.ShowHorizontalLine, ReportProperties.ShowVerticalLine);
            xrTableLedgerGroup = SetBorders(xrTableLedgerGroup, ReportProperties.ShowHorizontalLine, ReportProperties.ShowVerticalLine);
            xrtblLedger = SetBorders(xrtblLedger, ReportProperties.ShowHorizontalLine, ReportProperties.ShowVerticalLine);
            xrTable3 = SetBorders(xrTable3, ReportProperties.ShowHorizontalLine, ReportProperties.ShowVerticalLine);
        }

        #endregion

        private void tcCosAmountPeriod_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double CashReceipts = this.ReportProperties.NumberSet.ToDouble(tcCosAmountPeriod.Text);
            if (CashReceipts != 0)
            {
                e.Cancel = false;
            }
            else
            {
                tcCosAmountPeriod.Text = "";
            }
        }

        private void tcCosAmountProgress_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double CashReceipts = this.ReportProperties.NumberSet.ToDouble(tcCosAmountProgress.Text);
            if (CashReceipts != 0)
            {
                e.Cancel = false;
            }
            else
            {
                tcCosAmountProgress.Text = "";
            }
        }
    }
}
