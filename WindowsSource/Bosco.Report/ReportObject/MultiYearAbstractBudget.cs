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
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class MultiYearAbstractBudget : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variables
        double Y2OpBalance = 0;
        double Y1OpBalance = 0;
        double OpBalance = 0;
        double Y2TotalReceipts = 0;
        double Y1TotalReceipts = 0;
        double CYTotalReceipts = 0;
        double TotalReceiptsProposed = 0;

        double Y2CLBalance = 0;
        double Y1CLBalance = 0;
        double CLBalance = 0;
        double Y2TotalPayments = 0;
        double Y1TotalPayments = 0;
        double CYTotalPayments = 0;
        double TotalPaymentsProposed = 0;
        #endregion

        #region Constructor

        public MultiYearAbstractBudget()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReport

        public override void ShowReport()
        {
            this.ReportProperties.DateFrom = UtilityMember.DateSet.ToDate(settingProperty.YearFrom, false).ToShortDateString();
            this.ReportProperties.DateTo = UtilityMember.DateSet.ToDate(settingProperty.YearTo, false).ToShortDateString();
            
            if ( this.ReportProperties.Project == "0" || string.IsNullOrEmpty(this.ReportProperties.Project) ||
                 this.ReportProperties.Budget == "0" || string.IsNullOrEmpty(this.ReportProperties.Budget))
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
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        BindReportSource();
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
                    BindReportSource();
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }

        }

        #endregion

        #region Methods

        public void HideReportHeaderFooter()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
        }

        public void BindReportSource()
        {
            string filter = string.Empty;
            ReportProperty.Current.ProjectTitle = ReportProperties.BudgetName;
            this.SetLandscapeHeader = xrTableHeader.WidthF;
            this.SetLandscapeFooter = xrTableHeader.WidthF;
            this.SetLandscapeFooterDateWidth = xrTableHeader.WidthF;
            SetReportTitle();

            xrcellTransMode.Text = "Multi Year Budget";
            ResultArgs resultArgs = GetReportSource();
            if (resultArgs.Success && resultArgs.DataSource.Table!=null)
            {
                DataTable dtMultiAbstractBudget = resultArgs.DataSource.Table;
                if (dtMultiAbstractBudget != null)
                {
                    dtMultiAbstractBudget.DefaultView.RowFilter = "BUDGET_TRANS_MODE IN ('CR','DR')";
                    dtMultiAbstractBudget = dtMultiAbstractBudget.DefaultView.ToTable();
                    this.DataSource = dtMultiAbstractBudget;
                    this.DataMember = dtMultiAbstractBudget.TableName;
                    
                    //Receipts --- Caluculte Total Recepts and Opening Balance
                    //Payments --- Caluculte Total Payments and Opening Balance                    
                    BindCashBankBalances();
                    filter = reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName + " = '" + TransactionMode.CR.ToString() + "'";
                    Y2TotalReceipts= UtilityMember.NumberSet.ToDouble(dtMultiAbstractBudget.Compute("SUM(" + reportSetting1.MonthlyAbstract.PREVIOUS_YEAR2_AMOUNTColumn.ColumnName + ")", filter).ToString());
                    Y1TotalReceipts = UtilityMember.NumberSet.ToDouble(dtMultiAbstractBudget.Compute("SUM(" + reportSetting1.MonthlyAbstract.PREVIOUS_YEAR1_AMOUNTColumn.ColumnName + ")", filter).ToString());
                    TotalReceiptsProposed = UtilityMember.NumberSet.ToDouble(dtMultiAbstractBudget.Compute("SUM(" + reportSetting1.MonthlyAbstract.BUDGET_PROPOSED_AMOUNTColumn.ColumnName + ")", filter).ToString());
                    CYTotalReceipts = UtilityMember.NumberSet.ToDouble(dtMultiAbstractBudget.Compute("SUM(" + reportSetting1.MonthlyAbstract.AMOUNT_PERIODColumn.ColumnName + ")", filter).ToString());

                    filter = reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName + " = '" + TransactionMode.DR.ToString() + "'";
                    Y2TotalPayments= UtilityMember.NumberSet.ToDouble(dtMultiAbstractBudget.Compute("SUM(" + reportSetting1.MonthlyAbstract.PREVIOUS_YEAR2_AMOUNTColumn.ColumnName + ")", filter).ToString());
                    Y1TotalPayments = UtilityMember.NumberSet.ToDouble(dtMultiAbstractBudget.Compute("SUM(" + reportSetting1.MonthlyAbstract.PREVIOUS_YEAR1_AMOUNTColumn.ColumnName + ")", filter).ToString());
                    TotalPaymentsProposed = UtilityMember.NumberSet.ToDouble(dtMultiAbstractBudget.Compute("SUM(" + reportSetting1.MonthlyAbstract.BUDGET_PROPOSED_AMOUNTColumn.ColumnName + ")", filter).ToString());
                    CYTotalPayments = UtilityMember.NumberSet.ToDouble(dtMultiAbstractBudget.Compute("SUM(" + reportSetting1.MonthlyAbstract.AMOUNT_PERIODColumn.ColumnName + ")", filter).ToString());
                    
                    SetReportBorder();
                }
            }
            SortByLedgerorGroup();
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlMultiYearAbstractBudget = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.MultiAbstractBudget);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.BUDGET_IDColumn, this.ReportProperties.Budget);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlMultiYearAbstractBudget);
            }

            return resultArgs;
        }

        private void BindCashBankBalances()
        {
            string amtCaption = reportSetting1.ReportParameter.AMOUNTColumn.ColumnName;
            string filterCash = reportSetting1.ReportParameter.GROUP_IDColumn.ColumnName + " = " + (Int32)FixedLedgerGroup.Cash;
            string filterBank = reportSetting1.ReportParameter.GROUP_IDColumn.ColumnName + " = " + (Int32)FixedLedgerGroup.BankAccounts;
            string filterFd = reportSetting1.ReportParameter.GROUP_IDColumn.ColumnName + " = " + (Int32)FixedLedgerGroup.FixedDeposit;
            
            Y2OpBalance = Y1OpBalance = OpBalance = 0;
            Y2CLBalance = Y1CLBalance = CLBalance = 0;
            
            //For Opening
            ResultArgs result = this.GetCashBankFDGroupBalancePreviousYears(true, ReportProperties.DateFrom, ReportProperties.Project, ReportProperties.DateFrom);
            if (result.Success && result.DataSource.Table != null)
            {
                DataTable dtOPBal = result.DataSource.Table;
                OpBalance = UtilityMember.NumberSet.ToDouble(result.DataSource.Table.Compute("SUM(" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                Y1OpBalance = UtilityMember.NumberSet.ToDouble(result.DataSource.Table.Compute("SUM(Y1_" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                Y2OpBalance = UtilityMember.NumberSet.ToDouble(result.DataSource.Table.Compute("SUM(Y2_" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", string.Empty).ToString());

                tcCashOP.Text = tcCYCashOP.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(dtOPBal.Compute("SUM(" + amtCaption + ")", filterCash).ToString()));
                tcBankOP.Text = tcCYBankOP.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(dtOPBal.Compute("SUM(" + amtCaption + ")", filterBank).ToString()));
                tcFDOP.Text = tcCYFDOP.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(dtOPBal.Compute("SUM(" + amtCaption + ")", filterFd).ToString()));

                tcPY1CashOP.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(dtOPBal.Compute("SUM(Y1_" + amtCaption + ")", filterCash).ToString()));
                tcPY1BankOP.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(dtOPBal.Compute("SUM(Y1_" + amtCaption + ")", filterBank).ToString()));
                tcPY1FDOP.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(dtOPBal.Compute("SUM(Y1_" + amtCaption + ")", filterFd).ToString()));

                tcPY2CashOP.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(dtOPBal.Compute("SUM(Y2_" + amtCaption + ")", filterCash).ToString()));
                tcPY2BankOP.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(dtOPBal.Compute("SUM(Y2_" + amtCaption + ")", filterBank).ToString()));
                tcPY2FDOP.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(dtOPBal.Compute("SUM(Y2_" + amtCaption + ")", filterFd).ToString()));
                tcOPTotal.Text  = tcCYOPTotal.Text = UtilityMember.NumberSet.ToNumber(OpBalance);
                tcPY1OPTotal.Text = UtilityMember.NumberSet.ToNumber(Y1OpBalance);
                tcPY2OPTotal.Text = UtilityMember.NumberSet.ToNumber(Y2OpBalance);
            }

            //For Closing
            result = this.GetCashBankFDGroupBalancePreviousYears(false, ReportProperties.DateTo, ReportProperties.Project, ReportProperties.DateFrom);
            if (result.Success && result.DataSource.Table != null)
            {
                DataTable dtCLBal = result.DataSource.Table;
                CLBalance = UtilityMember.NumberSet.ToDouble(result.DataSource.Table.Compute("SUM(" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                Y1CLBalance = UtilityMember.NumberSet.ToDouble(result.DataSource.Table.Compute("SUM(Y1_" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                Y2CLBalance = UtilityMember.NumberSet.ToDouble(result.DataSource.Table.Compute("SUM(Y2_" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", string.Empty).ToString());

                tcCashCL.Text = tcCYCashCL.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(dtCLBal.Compute("SUM(" + amtCaption + ")", filterCash).ToString()));
                tcBankCL.Text = tcCYBankCL.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(dtCLBal.Compute("SUM(" + amtCaption + ")", filterBank).ToString()));
                tcFDCL.Text = tcCYFDCL.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(dtCLBal.Compute("SUM(" + amtCaption + ")", filterFd).ToString()));

                tcPY1CashCL.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(dtCLBal.Compute("SUM(Y1_" + amtCaption + ")", filterCash).ToString()));
                tcPY1BankCL.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(dtCLBal.Compute("SUM(Y1_" + amtCaption + ")", filterBank).ToString()));
                tcPY1FDCL.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(dtCLBal.Compute("SUM(Y1_" + amtCaption + ")", filterFd).ToString()));

                tcPY2CashCL.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(dtCLBal.Compute("SUM(Y2_" + amtCaption + ")", filterCash).ToString()));
                tcPY2BankCL.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(dtCLBal.Compute("SUM(Y2_" + amtCaption + ")", filterBank).ToString()));
                tcPY2FDCL.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(dtCLBal.Compute("SUM(Y2_" + amtCaption + ")", filterFd).ToString()));

                tcCLTotal.Text = tcCYCLTotal.Text = UtilityMember.NumberSet.ToNumber(CLBalance);
                tcPY1CLTotal.Text = UtilityMember.NumberSet.ToNumber(Y1CLBalance);
                tcPY2CLTotal.Text = UtilityMember.NumberSet.ToNumber(Y2CLBalance);
            }


            /*
            ResultArgs result =  this.GetCashBankFDGroupBalance(true, ReportProperties.DateFrom, ReportProperties.Project, ReportProperties.DateFrom);
            if (result.Success && result.DataSource.Table!=null)
            {
                result.DataSource.Table.Columns[reportSetting1.ReportParameter.AMOUNTColumn.ColumnName].ColumnName = "tmp";
                result.DataSource.Table.Columns.Add(reportSetting1.ReportParameter.AMOUNTColumn.ColumnName, typeof(System.Double), creditbalance);

                amt =  result.DataSource.Table.Compute("SUM(" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", filterCash).ToString();
                tcCashOP.Text = UtilityMember.NumberSet.ToNumber( UtilityMember.NumberSet.ToDouble(amt) );
                amt = result.DataSource.Table.Compute("SUM(" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", filterBank).ToString();
                tcBankOP.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(amt));
                amt = result.DataSource.Table.Compute("SUM(" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", filterFd).ToString();
                tcFDOP.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(amt));
                OpBalance = UtilityMember.NumberSet.ToDouble(result.DataSource.Table.Compute("SUM(" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                tcOPTotal.Text = UtilityMember.NumberSet.ToNumber(OpBalance);
            }
            result = this.GetCashBankFDGroupBalance(true, UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).AddYears(-1).ToShortDateString(), ReportProperties.Project, ReportProperties.DateFrom);
            if (result.Success && result.DataSource.Table != null)
            {
                result.DataSource.Table.Columns[reportSetting1.ReportParameter.AMOUNTColumn.ColumnName].ColumnName = "tmp";
                result.DataSource.Table.Columns.Add(reportSetting1.ReportParameter.AMOUNTColumn.ColumnName, typeof(System.Double), creditbalance);
                amt = result.DataSource.Table.Compute("SUM(" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", filterCash).ToString();
                tcPY1CashOP.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(amt));
                amt = result.DataSource.Table.Compute("SUM(" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", filterBank).ToString();
                tcPY1BankOP.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(amt));
                amt = result.DataSource.Table.Compute("SUM(" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", filterFd).ToString();
                tcPY1FDOP.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(amt));
                Y1OpBalance = UtilityMember.NumberSet.ToDouble(result.DataSource.Table.Compute("SUM(" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                tcPY1OPTotal.Text = UtilityMember.NumberSet.ToNumber(Y1OpBalance);
            }
            result = this.GetCashBankFDGroupBalance(true, UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).AddYears(-2).ToShortDateString(), ReportProperties.Project, ReportProperties.DateFrom);
            if (result.Success && result.DataSource.Table != null)
            {
                result.DataSource.Table.Columns[reportSetting1.ReportParameter.AMOUNTColumn.ColumnName].ColumnName = "tmp";
                result.DataSource.Table.Columns.Add(reportSetting1.ReportParameter.AMOUNTColumn.ColumnName, typeof(System.Double), creditbalance);
                amt = result.DataSource.Table.Compute("SUM(" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", filterCash).ToString();
                tcPY2CashOP.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(amt));
                amt = result.DataSource.Table.Compute("SUM(" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", filterBank).ToString();
                tcPY2BankOP.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(amt));
                amt = result.DataSource.Table.Compute("SUM(" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", filterFd).ToString();
                tcPY2FDOP.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(amt));
                Y2OpBalance = UtilityMember.NumberSet.ToDouble(result.DataSource.Table.Compute("SUM(" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                tcPY2OPTotal.Text = UtilityMember.NumberSet.ToNumber(Y2OpBalance);
            } */

            //For Closing
            /*result = this.GetCashBankFDGroupBalance(false, ReportProperties.DateTo, ReportProperties.Project, ReportProperties.DateFrom);
            if (result.Success && result.DataSource.Table != null)
            {
                result.DataSource.Table.Columns[reportSetting1.ReportParameter.AMOUNTColumn.ColumnName].ColumnName = "tmp";
                result.DataSource.Table.Columns.Add(reportSetting1.ReportParameter.AMOUNTColumn.ColumnName, typeof(System.Double), creditbalance);
                amt = result.DataSource.Table.Compute("SUM(" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", filterCash).ToString();
                tcCashCL.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(amt));
                amt = result.DataSource.Table.Compute("SUM(" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", filterBank).ToString();
                tcBankCL.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(amt));
                amt = result.DataSource.Table.Compute("SUM(" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", filterFd).ToString();
                tcFDCL.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(amt));
                CLBalance = UtilityMember.NumberSet.ToDouble(result.DataSource.Table.Compute("SUM(" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                tcCLTotal.Text = UtilityMember.NumberSet.ToNumber(CLBalance);
            }
            result = this.GetCashBankFDGroupBalance(false, UtilityMember.DateSet.ToDate(ReportProperties.DateTo, false).AddYears(-1).ToShortDateString(), ReportProperties.Project, ReportProperties.DateFrom);
            if (result.Success && result.DataSource.Table != null)
            {
                result.DataSource.Table.Columns[reportSetting1.ReportParameter.AMOUNTColumn.ColumnName].ColumnName = "tmp";
                result.DataSource.Table.Columns.Add(reportSetting1.ReportParameter.AMOUNTColumn.ColumnName, typeof(System.Double), creditbalance);
                amt = result.DataSource.Table.Compute("SUM(" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", filterCash).ToString();
                tcPY1CashCL.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(amt));
                amt = result.DataSource.Table.Compute("SUM(" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", filterBank).ToString();
                tcPY1BankCL.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(amt));
                amt = result.DataSource.Table.Compute("SUM(" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", filterFd).ToString();
                tcPY1FDCL.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(amt));
                Y1CLBalance = UtilityMember.NumberSet.ToDouble(result.DataSource.Table.Compute("SUM(" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                tcPY1CLTotal.Text = UtilityMember.NumberSet.ToNumber(Y1CLBalance);
            }
            result = this.GetCashBankFDGroupBalance(false, UtilityMember.DateSet.ToDate(ReportProperties.DateTo, false).AddYears(-2).ToShortDateString(), ReportProperties.Project, ReportProperties.DateFrom);
            if (result.Success && result.DataSource.Table != null)
            {
                result.DataSource.Table.Columns[reportSetting1.ReportParameter.AMOUNTColumn.ColumnName].ColumnName = "tmp";
                result.DataSource.Table.Columns.Add(reportSetting1.ReportParameter.AMOUNTColumn.ColumnName, typeof(System.Double), creditbalance);
                amt = result.DataSource.Table.Compute("SUM(" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", filterCash).ToString();
                tcPY2CashCL.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(amt));
                amt = result.DataSource.Table.Compute("SUM(" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", filterBank).ToString();
                tcPY2BankCL.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(amt));
                amt = result.DataSource.Table.Compute("SUM(" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", filterFd).ToString();
                tcPY2FDCL.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(amt));
                Y2CLBalance = UtilityMember.NumberSet.ToDouble(result.DataSource.Table.Compute("SUM(" + reportSetting1.ReportParameter.AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                tcPY2CLTotal.Text = UtilityMember.NumberSet.ToNumber(Y2CLBalance);
            }*/
        }

        private void SetReportSetting(DataView dvReceipt, AccountBalanceMultiYear accountBalance)
        {
            float actualCodeWidth = tcCapCode.WidthF;
            bool isCapCodeVisible = true;

            //Include / Exclude Code
            if (tcCapCode.Tag != null && tcCapCode.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(tcCapCode.Tag.ToString());
            }
            else
            {
                tcCapCode.Tag = tcCapCode.WidthF;
            }

            isCapCodeVisible = (ReportProperties.ShowGroupCode == 1 || ReportProperties.ShowLedgerCode == 1);
            tcCapCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            tcLedgerCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);

            grpHeaderBGGroup.GroupFields[0].FieldName = "";            
            if (grpHeaderBGGroup.Visible)
            {
                if (ReportProperties.SortByGroup == 1)
                {
                    grpHeaderBGGroup.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.BUDGET_GROUPColumn.ColumnName;
                }
                else
                {
                    grpHeaderBGGroup.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.BUDGET_GROUPColumn.ColumnName;
                }
            }

            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            this.setHeaderTitleAlignment();
        }

        private void SetReportBorder()
        {
            xrTableHeader = AlignHeaderTable(xrTableHeader);
            xrTblOpeningBalance = AlignContentTable(xrTblOpeningBalance);
            xrTblIncomeExpenseTitle = AlignContentTable(xrTblIncomeExpenseTitle);
            xrtblLedger = AlignContentTable(xrtblLedger);
            xrTableBGGroup = AlignContentTable(xrTableBGGroup);
            xrTableBGGroupTotal = AlignContentTable(xrTableBGGroupTotal);

            xrTblOPTitle = AlignContentTable(xrTblOPTitle);
            xrTblCLTitle = AlignContentTable(xrTblCLTitle);
            xrtblTotal = AlignTotalTable(xrtblTotal);
            xrTblClosingBalance = AlignContentTable(xrTblClosingBalance);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);

            this.SetCurrencyFormat(tcCapPY1Amount1.Text, tcCapPY1Amount1);
            this.SetCurrencyFormat(tcCapPY2Amount1.Text, tcCapPY2Amount1);
            this.SetCurrencyFormat(tcCapBudgetPropsed1.Text, tcCapBudgetPropsed1);
            this.SetCurrencyFormat(tcCapAmountPeriod1.Text, tcCapAmountPeriod1);

            tcCapPY2Amount.Text = UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).AddYears(-2).Year.ToString() + "-" +
                UtilityMember.DateSet.ToDate(ReportProperties.DateTo, false).AddYears(-2).Year.ToString();
            tcCapPY1Amount.Text = UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).AddYears(-1).Year.ToString() + "-" + 
                UtilityMember.DateSet.ToDate(ReportProperties.DateTo, false).AddYears(-1).Year.ToString();
            tcCapBudgetPropsed.Text = UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).Year.ToString() + "-" +
                UtilityMember.DateSet.ToDate(ReportProperties.DateTo, false).Year.ToString();
            tcCapAmountPeriod.Text = UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).Year.ToString() + "-" +
                UtilityMember.DateSet.ToDate(ReportProperties.DateTo, false).Year.ToString();
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
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Bottom | BorderSide.Top;
                        else if (count == trow.Cells.Count)
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom | BorderSide.Top;
                        }
                        else
                            tcell.Borders = BorderSide.Bottom | BorderSide.Top;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = BorderSide.Left;
                        else if (count == 1)
                            tcell.Borders = BorderSide.All;
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                    tcell.Font = (ReportProperty.Current.ColumnCaptionFontStyle == 0 ? this.styleColumnHeader.Font : new Font(this.styleColumnHeader.Font, FontStyle.Regular));
                }
            }
            return table;
        }

        private void SortByLedgerorGroup()
        {
            Detail.Visible = (this.ReportProperties.ShowByLedger==1);
            grpHeaderBGGroup.Visible = (this.ReportProperties.ShowByBudgetGroup== 1);
            grpFooterBGGroup.Visible = (this.ReportProperties.ShowByBudgetGroup == 1 && this.ReportProperties.ShowByLedger== 1);

            if (grpHeaderBGGroup.Visible)
            {
                grpHeaderBGGroup.SortingSummary.Enabled = true;
                grpHeaderBGGroup.SortingSummary.FieldName = reportSetting1.ProfitandLossbyHouse.BUDGET_GROUP_SORT_IDColumn.ColumnName;
                grpHeaderBGGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                grpHeaderBGGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            }

            Detail.SortFields.Clear();
            if (this.ReportProperties.SortByLedger == 0)
            {
                Detail.SortFields.Add(new GroupField("LEDGER_CODE", XRColumnSortOrder.Ascending));
                Detail.SortFields.Add(new GroupField("LEDGER_NAME", XRColumnSortOrder.Ascending));
            }
            else
            {
                Detail.SortFields.Add(new GroupField("LEDGER_NAME", XRColumnSortOrder.Ascending));
            }

            //if (grpHeaderBGGroup.Visible)
            //{
            //    grpHeaderBGGroup.SortingSummary.Enabled = true;
            //    grpHeaderBGGroup.SortingSummary.FieldName = reportSetting1.ProfitandLossbyHouse.BUDGET_GROUP_SORT_IDColumn.ColumnName;
            //    grpHeaderBGGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
            //    grpHeaderBGGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //}
        }

        #endregion

        #region Events
        
        private void xrcellTransMode_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrcellTransMode.Text = string.Empty;
            xrCellTotalCaption.Text = string.Empty;
            xrTblCLTitle.Visible = false;
            xrTblClosingBalance.Visible = false;
            if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName) != null)
            {
                if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName).ToString().ToUpper()
                     == TransactionMode.CR.ToString().ToUpper())
                {
                    xrcellTransMode.Text = "Multi Year Budget Income";
                    xrCellTotalCaption.Text = "Total Receipts";
                    xrtblGrandTotal.TopF = xrTblCLTitle.TopF;
                    //xrRowCLCash.Visible = xrRowCLBankAccounts.Visible = xrRowCLFD.Visible = xrRowCLTotal.Visible = false;
                    if (grpFooterBGTransMode.Controls[xrTblClosingBalance.Name] != null)
                    {
                        grpFooterBGTransMode.Controls.Remove(xrTblClosingBalance);
                    }
                    grpFooterBGTransMode.HeightF = 25;
                    
                }
                else if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName).ToString().ToUpper()
                     == TransactionMode.DR.ToString().ToUpper())
                {
                    xrcellTransMode.Text = "Multi Year Budget Expense";
                    xrCellTotalCaption.Text = "Total Payments";
                    xrTblCLTitle.Visible = true;
                    xrTblClosingBalance.Visible = true;
                    if (grpFooterBGTransMode.Controls[xrTblClosingBalance.Name] == null)
                    {
                        grpFooterBGTransMode.Controls.Add(xrTblClosingBalance);
                    }
                    xrTblCLTitle.TopF = xrtblTotal.TopF + xrtblTotal.HeightF;
                    xrTblClosingBalance.TopF = xrTblCLTitle.TopF + xrTblCLTitle.HeightF;
                    xrtblGrandTotal.TopF = xrTblClosingBalance.TopF + xrTblClosingBalance.HeightF;
                }
            }
        }

        private void tcGTotalPY2Amount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName) != null)
            {
                if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName).ToString().ToUpper() == TransactionMode.CR.ToString().ToUpper())
                {
                    e.Result = Y2TotalReceipts + Y2OpBalance;
                }
                else
                {
                    e.Result = Y2TotalPayments + Y2CLBalance;
                }
                e.Handled = true;
            }
        }

        private void tcGTotalPY1Amount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName) != null)
            {
                if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName).ToString().ToUpper() == TransactionMode.CR.ToString().ToUpper())
                {
                    e.Result = Y1TotalReceipts + Y1OpBalance;
                }
                else
                {
                    e.Result = Y1TotalPayments + Y1CLBalance;
                }
                e.Handled = true;
            }
        }

        private void tcGTotalBudgetPropsed_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName) != null)
            {
                if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName).ToString().ToUpper() == TransactionMode.CR.ToString().ToUpper())
                {
                    e.Result = TotalReceiptsProposed + OpBalance;
                }
                else
                {
                    e.Result = TotalPaymentsProposed + CLBalance;
                }
                e.Handled = true;
            }
        }

        private void tcBudgetGroupTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_GROUPColumn.ColumnName) != null)
            {
                XRTableCell cell = sender as XRTableCell;
                cell.Text = "Total - " + GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_GROUPColumn.ColumnName).ToString();
            }
        }

        private void tcGTotalCYAmount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName) != null)
            {
                if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName).ToString().ToUpper() == TransactionMode.CR.ToString().ToUpper())
                {
                    e.Result = CYTotalReceipts + OpBalance;
                }
                else
                {
                    e.Result = CYTotalPayments + CLBalance;
                }
                e.Handled = true;
            }
        }
        #endregion

        private void xrTableCell10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void grpHeaderBGTransMode_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrRowIEsplitter.Visible = false;
            if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName) != null)
            {
                if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName).ToString().ToUpper()
                     == TransactionMode.DR.ToString().ToUpper())
                {
                    xrRowIEsplitter.Visible = true;
                }
            }
        }

        

      
        

    }
}
