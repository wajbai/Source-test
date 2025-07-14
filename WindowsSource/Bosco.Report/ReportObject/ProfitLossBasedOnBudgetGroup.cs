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
    public partial class ProfitLossBasedOnBudgetGroup: Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public ProfitLossBasedOnBudgetGroup()
        {
            InitializeComponent();
        }
        #endregion

        #region Variables
        
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            ShowPLReportBasedOnBudgetGroup();
        }
        #endregion

        #region Methods
        private void ShowPLReportBasedOnBudgetGroup()
        {
            if (!string.IsNullOrEmpty(this.ReportProperties.DateFrom) || !string.IsNullOrEmpty(this.ReportProperties.DateTo) 
                || this.ReportProperties.Project != "0" || string.IsNullOrEmpty(this.ReportProperties.Project))
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        SetReportProperty();
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
                    SetReportProperty();
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            SetReportBorder();
        }

        private void SetReportProperty()
        {
            this.SetLandscapeHeader = 1065.25f;
            this.SetLandscapeFooter = 1065.25f;
            this.SetLandscapeFooterDateWidth = 910.25f;

            setHeaderTitleAlignment();
            SetReportTitle();
                        
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            this.CosCenterName = null;
            SetTitleWidth(xrtblHeaderCaption.WidthF);
            this.SetLandscapeHeader = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooter = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooterDateWidth = xrtblHeaderCaption.WidthF;

            ResultArgs resultArgs = GetReportSource();
            if (resultArgs.Success)
            {
                DataTable dtPLBasedOnBudgetGroup = resultArgs.DataSource.Table;
                //Set show by Properties
                GroupFromCriteria();
                                
                if (dtPLBasedOnBudgetGroup != null)
                {
                    dtPLBasedOnBudgetGroup.TableName = this.DataMember;
                    //Take all Income Ledgers amount will be receitps and all Expenses ledgers would be payments with multiply by -1
                    dtPLBasedOnBudgetGroup.Columns.Add(reportSetting1.ProfitandLossbyHouse.RECEIPTColumn.ColumnName, typeof(double), "IIF(NATURE_ID IN (1), AMOUNT, 0)");
                    dtPLBasedOnBudgetGroup.Columns.Add(reportSetting1.ProfitandLossbyHouse.PAYMENTColumn.ColumnName, typeof(double), "IIF(NATURE_ID IN (2), AMOUNT *-1, 0)");
                    dtPLBasedOnBudgetGroup.Columns.Add(reportSetting1.ProfitandLossbyHouse.TOTAL_CRColumn.ColumnName, typeof(double), "RECEIPT + INTER_CR + CONTRIBUTION_FROM_CR");
                    dtPLBasedOnBudgetGroup.Columns.Add(reportSetting1.ProfitandLossbyHouse.TOTAL_DRColumn.ColumnName, typeof(double), "PAYMENT + INTER_DR + CONTRIBUTION_TO_DR");
                    dtPLBasedOnBudgetGroup.Columns.Add(reportSetting1.ProfitandLossbyHouse.FINALColumn.ColumnName, typeof(double), "TOTAL_CR - TOTAL_DR");
                    dtPLBasedOnBudgetGroup.DefaultView.RowFilter = "(AMOUNT <> 0 OR INTER_CR <> 0 OR INTER_DR <> 0 OR CONTRIBUTION_FROM_CR <> 0 OR CONTRIBUTION_TO_DR <> 0)";
                    dtPLBasedOnBudgetGroup = dtPLBasedOnBudgetGroup.DefaultView.ToTable();


                    //for SDBINM, just make to group all recuring and non recuring budget groups into single group
                    //if (settingProperty.IS_SDB_INM)
                    //{
                    //    string trasnfersectors = UtilityMember.EnumSet.GetDescriptionFromEnumValue(BudgetGroup.TransferotherSectors as Enum).ToString().ToUpper();
                    //    string contribution = UtilityMember.EnumSet.GetDescriptionFromEnumValue(BudgetGroup.ContributionProvince as Enum).ToString().ToUpper();

                    //    string recuring = string.Empty;
                    //    foreach (DataRow dr in dtPLBasedOnBudgetGroup.Rows)
                    //    {
                    //        dr.BeginEdit();
                    //        if (dr[reportSetting1.MonthlyAbstract.BUDGET_GROUPColumn.ColumnName].ToString().ToUpper() != trasnfersectors &&
                    //            dr[reportSetting1.MonthlyAbstract.BUDGET_GROUPColumn.ColumnName].ToString().ToUpper() != contribution)
                    //        {
                    //            dr[reportSetting1.MonthlyAbstract.BUDGET_GROUPColumn.ColumnName] = "Recurring";
                    //            dr[reportSetting1.ProfitandLossbyHouse.BUDGET_GROUP_SORT_IDColumn.ColumnName] = 1;
                    //        }
                    //        dr.EndEdit();
                    //    }
                    //    dtPLBasedOnBudgetGroup.AcceptChanges();
                    //}

                    this.DataSource = dtPLBasedOnBudgetGroup;
                    this.DataMember = dtPLBasedOnBudgetGroup.TableName;
                }
                else
                {
                    this.DataSource = null;
                }
            }
            else
            {
                MessageRender.ShowMessage("Could not generate Report " + resultArgs.Message, true);
            }
        }

        private void SetReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption, true);
            //xrTblSocietyName = AlignContentTable(xrTblSocietyName);
            //xrTblProjectName = AlignContentTable(xrTblProjectName);
            xrTblBudgetGroup = AlignContentTable(xrTblBudgetGroup);
            xrTblLedger = AlignContentTable(xrTblLedger);
            //xrTblProjectDetails = AlignContentTable(xrTblProjectDetails);
            //xrTblSocietyDetails = AlignContentTable(xrTblSocietyDetails);
            xrTblGrandTotal = AlignContentTable(xrTblGrandTotal);
        }
                

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                string Recurring  = "'" + UtilityMember.EnumSet.GetDescriptionFromEnumValue(BudgetGroup.RecurringIncome as Enum).ToString() + "'," +
                                        "'" + UtilityMember.EnumSet.GetDescriptionFromEnumValue(BudgetGroup.RecurringExpenses as Enum).ToString() + "'";
                
                string NonRecurring  = "'" + UtilityMember.EnumSet.GetDescriptionFromEnumValue(BudgetGroup.NonRecurringIncome as Enum).ToString() + "'," +
                                       "'" + UtilityMember.EnumSet.GetDescriptionFromEnumValue(BudgetGroup.NonRecurringExpenses as Enum).ToString() + "'";

                string reportsql = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.ProfitLossBasedOnBudgetGroup);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.CONTRIBUTION_FROM_LEDGERSColumn, this.settingProperty.ContributionFromLedgers);
                    dataManager.Parameters.Add(this.ReportParameters.CONTRIBUTION_TO_LEDGERSColumn, this.settingProperty.ContributionToLedgers);
                    dataManager.Parameters.Add(this.ReportParameters.INTER_ACCOUNT_FROM_LEDGERSColumn, this.settingProperty.InterAccountFromLedgers);
                    dataManager.Parameters.Add(this.ReportParameters.INTER_ACCOUNT_TO_LEDGERSColumn, this.settingProperty.InterAccountToLedgers);

                    dataManager.Parameters.Add("RECURRING", Recurring);
                    dataManager.Parameters.Add("NONRECURRING", NonRecurring);
                                        
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, reportsql);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// On 11/12/2021, to group based on the report criteria page
        /// </summary>
        public void GroupFromCriteria()
        {
            //If there is no group selection, fix by default Project with Budget Group
            if (this.ReportProperties.ShowBySociety == 0 && this.ReportProperties.ShowByProject == 0 && 
                 this.ReportProperties.ShowByBudgetGroup == 0 && this.ReportProperties.ShowByLedger == 0)
            {
                this.ReportProperties.ShowByProject = 1;
                this.ReportProperties.ShowByBudgetGroup = 1;
            }
            grpHeaderSociety.Visible = (this.ReportProperties.ShowBySociety == 1); //grpFooterSociety.Visible
            grpHeaderProject.Visible = (this.ReportProperties.ShowByProject == 1); //grpFooterProject.Visible = 
            grpHeaderBudgetGroup.Visible = (this.ReportProperties.ShowByBudgetGroup == 1);
            grpHeaderLedger.Visible = (this.ReportProperties.ShowByLedger == 1);

            grpHeaderSociety.GroupFields[0].FieldName = "";
            grpHeaderProject.GroupFields[0].FieldName = "";
            grpHeaderBudgetGroup.GroupFields[0].FieldName = "";
            grpHeaderLedger.GroupFields[0].FieldName = "";

            if (ReportProperties.ShowBySociety==1)
                grpHeaderSociety.GroupFields[0].FieldName = reportSetting1.ProfitandLossbyHouse.SOCIETYNAMEColumn.ColumnName;

            if (ReportProperties.ShowByProject == 1)
            {
                grpHeaderProject.GroupFields[0].FieldName = reportSetting1.ProfitandLossbyHouse.PROJECTColumn.ColumnName;
            }

            if (ReportProperties.ShowByBudgetGroup == 1)
            {
                grpHeaderBudgetGroup.GroupFields[0].FieldName = reportSetting1.ProfitandLossbyHouse.BUDGET_GROUPColumn.ColumnName;
                grpHeaderBudgetGroup.SortingSummary.Enabled = true;
                grpHeaderBudgetGroup.SortingSummary.FieldName = reportSetting1.ProfitandLossbyHouse.BUDGET_GROUP_SORT_IDColumn.ColumnName;
                grpHeaderBudgetGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                grpHeaderBudgetGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            }


            Font bgfont = new Font(xrBGDIncome.Font.Name, xrBGDIncome.Font.Size, FontStyle.Regular);
            if (ReportProperties.ShowByLedger == 1)
            {
                grpHeaderLedger.GroupFields[0].FieldName = reportSetting1.ProfitandLossbyHouse.LEDGER_NAMEColumn.ColumnName;
                bgfont = new Font(xrBGDIncome.Font.Name, xrBGDIncome.Font.Size, FontStyle.Bold);
            }
            xrBGDIncome.Font = xrBGProvinceFrom.Font = xrBGInterAcFrom.Font = xrBGIncomeTotal.Font = bgfont;
            xrBGDExpense.Font = xrBGProvinceTo.Font = xrBGInterAcTo.Font = xrBGExpenseTotal.Font = bgfont;
            xrBGName.Font = xrBGResult.Font = bgfont;

        }

        private void MakeEmptyCell(SummaryGetResultEventArgs ecell, string fldname)
        {
            if (GetCurrentColumnValue(fldname) != null)
            {
                double amt = 0;

                for (int i = 0; i <= ecell.CalculatedValues.Count - 1; i++)
                {
                    amt += UtilityMember.NumberSet.ToDouble(ecell.CalculatedValues[i].ToString());
                }
                
                if (amt == 0)
                {
                    ecell.Result = "";
                    ecell.Handled = true;
                }
            }
        }
        #endregion

        #region Events
        private void xrLGDIncome_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            MakeEmptyCell(e, reportSetting1.ProfitandLossbyHouse.RECEIPTColumn.ColumnName);
        }

        private void xrLGInterAcFrom_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            MakeEmptyCell(e, reportSetting1.ProfitandLossbyHouse.INTER_CRColumn.ColumnName);
        }

        private void xrLGProvinceFrom_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            MakeEmptyCell(e, reportSetting1.ProfitandLossbyHouse.CONTRIBUTION_FROM_CRColumn.ColumnName);
        }

        private void xrLGIncomeTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            MakeEmptyCell(e, reportSetting1.ProfitandLossbyHouse.TOTAL_CRColumn.ColumnName);
        }

        private void xrLGDExpense_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            MakeEmptyCell(e, reportSetting1.ProfitandLossbyHouse.PAYMENTColumn.ColumnName);
        }

        private void xrLGInterAcTo_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            MakeEmptyCell(e, reportSetting1.ProfitandLossbyHouse.INTER_DRColumn.ColumnName);
        }

        private void xrLGProvinceTo_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            MakeEmptyCell(e, reportSetting1.ProfitandLossbyHouse.CONTRIBUTION_TO_DRColumn.ColumnName);
        }

        private void xrLGExpenseTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            MakeEmptyCell(e, reportSetting1.ProfitandLossbyHouse.TOTAL_DRColumn.ColumnName);
        }

        private void xrLGResult_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            MakeEmptyCell(e, reportSetting1.ProfitandLossbyHouse.FINALColumn.ColumnName);
        }

        private void xrBGDIncome_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
           MakeEmptyCell(e, reportSetting1.ProfitandLossbyHouse.RECEIPTColumn.ColumnName);
        }

        private void xrBGInterAcFrom_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            MakeEmptyCell(e, reportSetting1.ProfitandLossbyHouse.INTER_CRColumn.ColumnName);
        }

        private void xrBGProvinceFrom_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            MakeEmptyCell(e, reportSetting1.ProfitandLossbyHouse.CONTRIBUTION_FROM_CRColumn.ColumnName);
        }

        private void xrBGIncomeTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            MakeEmptyCell(e, reportSetting1.ProfitandLossbyHouse.TOTAL_CRColumn.ColumnName);
        }

        private void xrBGDExpense_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            MakeEmptyCell(e, reportSetting1.ProfitandLossbyHouse.PAYMENTColumn.ColumnName);
        }

        private void xrBGInterAcTo_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            MakeEmptyCell(e, reportSetting1.ProfitandLossbyHouse.INTER_DRColumn.ColumnName);
        }

        private void xrBGProvinceTo_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            MakeEmptyCell(e, reportSetting1.ProfitandLossbyHouse.CONTRIBUTION_TO_DRColumn.ColumnName);
        }

        private void xrBGExpenseTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            MakeEmptyCell(e, reportSetting1.ProfitandLossbyHouse.TOTAL_DRColumn.ColumnName);
        }

        private void xrBGResult_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            MakeEmptyCell(e, reportSetting1.ProfitandLossbyHouse.FINALColumn.ColumnName);
        }
        #endregion

        private void xrtcSocietyTotalTitle_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.ProfitandLossbyHouse.PROJECTColumn.ColumnName)!=null)
            {
                XRTableCell cell = sender as XRTableCell;
                if (ReportProperties.ShowByBudgetGroup == 0 && ReportProperties.ShowByLedger == 0 && ReportProperties.ShowByProject== 0)
                {
                    cell.Text = string.Empty;
                }
                else
                {
                    cell.Text = "Society Total";
                }
            }
        }

        private void xrtcProjectTotalTitle_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.ProfitandLossbyHouse.PROJECTColumn.ColumnName) != null)
            {
                XRTableCell cell = sender as XRTableCell;
                if (ReportProperties.ShowByBudgetGroup == 0 && ReportProperties.ShowByLedger == 0)
                {
                    cell.Text = string.Empty;
                }
                else
                {
                    cell.Text = "Project Total";
                }
            }
        }
    }
}
