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
    public partial class BudgetDetailsProjectwise : Bosco.Report.Base.ReportHeaderBase
    {
        int ProjectNumber = 1;
        string BudgetProjectNames = string.Empty;
        string ProjectBudgetIds = string.Empty;
        public BudgetDetailsProjectwise()
        {
            InitializeComponent();
            this.SetTitleWidth(xrPGMultiProjectBudgetIncome.WidthF);
        }
        #region Show Reports
        public override void ShowReport()
        {
            ProjectNumber = 1;
            FetchBudgetVariance();
        }
        #endregion

        public void FetchBudgetVariance()
        {
            if (this.UIAppSetting.UICustomizationForm == "1")
            {
                if (ReportProperty.Current.ReportFlag == 0)
                {
                    FetchBudgetDetails();
                    base.ShowReport();
                }
                else
                {
                    ShowReportFilterDialog();
                }
            }
            else
            {
                FetchBudgetDetails();
                base.ShowReport();
            }
        }

        private void FetchBudgetDetails()
        {
            try
            {
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                lblProjectName.Text = string.Empty;
                BudgetProjectNames = string.Empty;

                SetReportTitle();
                AssignBudgetDateRangeTitle();
                setHeaderTitleAlignment();
                this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom) 
                                                + " - " + this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo);
                this.HideBudgetName = this.HideReportSubTitle = false;

                string budgetvariance = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetDetailsByProject);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.AppSetting.YearFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.AppSetting.YearTo);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    ResultArgs resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetvariance);

                    //Detail.SortFields.Add(new GroupField("LEDGER_CODE"));
                    //Detail.SortFields.Add(new GroupField("BUDGET_SUB_GROUP"));

                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        DataTable dtBudgetDetails = resultArgs.DataSource.Table;

                        //# Get Budget Project Names
                        DataTable dtBudgets = dtBudgetDetails.DefaultView.ToTable(true, reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName);
                        string budgetproject = string.Empty;
                        int sno = 0;
                        foreach (DataRow dr in dtBudgets.Rows)
                        {
                            Int32 budgetid = UtilityMember.NumberSet.ToInteger(dr[reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName].ToString());
                            dtBudgetDetails.DefaultView.RowFilter = reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName  + "=" + budgetid;
                            if (dtBudgetDetails.DefaultView.Count > 0)
                            {
                                sno++;
                                budgetproject += System.Environment.NewLine + "P" + sno.ToString() + ": " + dtBudgetDetails.DefaultView[0][reportSetting1.BUDGETVARIANCE.PROJECTColumn.ColumnName].ToString();
                            }
                            ProjectBudgetIds += budgetid.ToString() + ",";
                        }
                        budgetproject = budgetproject.TrimEnd(',');
                        ProjectBudgetIds = ProjectBudgetIds.TrimEnd(',');
                        BudgetProjectNames = budgetproject;
                        dtBudgetDetails.DefaultView.RowFilter = string.Empty;

                        //# Budget Income 
                        dtBudgetDetails.DefaultView.RowFilter = reportSetting1.Ledger.TRANS_MODEColumn.ColumnName + "= '" + TransactionMode.CR.ToString() + "'";
                        DataTable dtBudgetIncome = dtBudgetDetails.DefaultView.ToTable();
                        AppendHOHelpAmountInIncome(dtBudgetIncome);

                        //# Budget Expense
                        dtBudgetDetails.DefaultView.RowFilter = reportSetting1.Ledger.TRANS_MODEColumn.ColumnName + "= '" + TransactionMode.DR.ToString() + "'";
                        DataTable dtBudgetExpense = dtBudgetDetails.DefaultView.ToTable();
                        //AppendHOHelpAmountInIncome(dtBudgetExpense);

                        xrPGMultiProjectBudgetIncome.DataSource = dtBudgetIncome;
                        xrPGMultiProjectBudgetIncome.DataMember = dtBudgetIncome.TableName;
                                                
                        xrPGMultiProjectBudgetExpense.DataSource = dtBudgetExpense;
                        xrPGMultiProjectBudgetExpense.DataMember = dtBudgetExpense.TableName;

                       
                    }
                    else
                    {
                        xrPGMultiProjectBudgetIncome.DataSource = null;
                        xrPGMultiProjectBudgetExpense.DataSource = null;
                    }
                }                
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
        private void AppendHOHelpAmountInIncome(DataTable dtBudgetReport)
        {
            decimal HOBudgetHelpPropsedAmt = 0;
            decimal HOBudgetHelpApprovedAmt = 0;
            ResultArgs resultArgs = new ResultArgs();
            if (dtBudgetReport!=null && dtBudgetReport.Rows.Count > 0 && AppSetting.ENABLE_BUDGET_HO_HELP_AMOUNT)
            {
                string budgetinfo = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetInfo);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn, ProjectBudgetIds);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetinfo);
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtBudgetProvinceHelp = resultArgs.DataSource.Table;
                        foreach (DataRow drProvinceHelp in dtBudgetProvinceHelp.Rows)
                        {
                            Int32 budgetid = UtilityMember.NumberSet.ToInteger(drProvinceHelp[reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName].ToString());
                            string strHOBudgetHelpPropsedAmt = dtBudgetProvinceHelp.Compute("SUM(" + reportSetting1.BUDGETVARIANCE.HO_HELP_PROPOSED_AMOUNTColumn.ColumnName + ")", "BUDGET_ID=" + budgetid).ToString();
                            string strHOBudgetHelpApprovedAmt = dtBudgetProvinceHelp.Compute("SUM(" + reportSetting1.BUDGETVARIANCE.HO_HELP_APPROVED_AMOUNTColumn.ColumnName + ")", "BUDGET_ID=" + budgetid).ToString();

                            HOBudgetHelpPropsedAmt = UtilityMember.NumberSet.ToDecimal(strHOBudgetHelpPropsedAmt);
                            HOBudgetHelpApprovedAmt = UtilityMember.NumberSet.ToDecimal(strHOBudgetHelpApprovedAmt);

                            //Get Budget report ProjectId and Project Name
                            dtBudgetReport.DefaultView.RowFilter = reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName + "=" + budgetid.ToString();
                            if (dtBudgetReport.DefaultView.Count > 0)
                            {
                                string BudgetProjectid = dtBudgetReport.DefaultView[0][reportSetting1.BUDGETVARIANCE.PROJECT_IDColumn.ColumnName].ToString();
                                string BudgetProjectName = dtBudgetReport.DefaultView[0][reportSetting1.BUDGETVARIANCE.PROJECTColumn.ColumnName].ToString();

                                dtBudgetReport.DefaultView.RowFilter = string.Empty;
                                DataRow drHOBudgetHelpAmount = dtBudgetReport.NewRow();
                                drHOBudgetHelpAmount[reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName] = budgetid;
                                drHOBudgetHelpAmount[reportSetting1.BUDGETVARIANCE.PROJECT_IDColumn.ColumnName] = BudgetProjectid;
                                drHOBudgetHelpAmount[reportSetting1.BUDGETVARIANCE.PROJECTColumn.ColumnName] = BudgetProjectName;

                                drHOBudgetHelpAmount[reportSetting1.BUDGET_LEDGER.LEDGER_GROUPColumn.ColumnName] = string.Empty;
                                drHOBudgetHelpAmount[reportSetting1.ReportParameter.LEDGER_IDColumn.ColumnName] = 0;
                                drHOBudgetHelpAmount[reportSetting1.Ledger.LEDGER_CODEColumn.ColumnName] = string.Empty;//To make to come first order
                                drHOBudgetHelpAmount[reportSetting1.Ledger.LEDGER_NAMEColumn.ColumnName] = " " +  AppSetting.BUDGET_HO_HELP_AMOUNT_CAPTION; //Make come fist order
                                drHOBudgetHelpAmount[reportSetting1.Ledger.NATURE_IDColumn.ColumnName] = (int)Natures.Income;
                                drHOBudgetHelpAmount[reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName] = Natures.Income.ToString();
                                drHOBudgetHelpAmount[reportSetting1.BUDGETVARIANCE.PROPOSED_AMOUNTColumn.ColumnName] = HOBudgetHelpPropsedAmt;
                                drHOBudgetHelpAmount[reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName] = HOBudgetHelpApprovedAmt;

                                drHOBudgetHelpAmount[reportSetting1.ReportParameter.TRANS_MODEColumn.ColumnName] = TransSource.Cr.ToString().ToUpper();
                                drHOBudgetHelpAmount[reportSetting1.BUDGETVARIANCE.BUDGET_GROUPColumn.ColumnName] = " ";
                                drHOBudgetHelpAmount[reportSetting1.BUDGETVARIANCE.BUDGET_GROUP_IDColumn.ColumnName] = 0;
                                drHOBudgetHelpAmount[reportSetting1.BUDGETVARIANCE.BUDGET_SUB_GROUPColumn.ColumnName] = string.Empty;
                                drHOBudgetHelpAmount[reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName] = Natures.Income.ToString();
                                dtBudgetReport.Rows.InsertAt(drHOBudgetHelpAmount, 0);
                            }
                        }
                    }
                }

                dtBudgetReport.DefaultView.RowFilter = string.Empty;
            }
        }
            
      private void xrPGMultiProjectBudgetIncome_PrintFieldValue(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs e)
        {
            if (e.Field != null)
            {
                if (e.Field.Name == fieldLEDGERCODE.Name || e.Field.Name == fieldLEDGERNAME.Name
                      || e.Field.Name == fieldGROUPCODE.Name || e.Field.Name == fieldLEDGERGROUP.Name)
                {
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
                    e.Appearance.Font = new Font(xrPGMultiProjectBudgetIncome.Styles.CellStyle.Font, FontStyle.Regular);
                }
                else if (e.Field.Name == fieldPROPOSEDAMOUNT.Name || e.Field.Name == fieldAPPROVEDAMOUNT.Name)
                {
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
                    e.Appearance.Font = new Font(xrPGMultiProjectBudgetIncome.Appearance.Cell.Font, FontStyle.Bold);
                    e.DataField.Appearance.Cell.Font = new Font(xrPGMultiProjectBudgetIncome.Appearance.Cell.Font, FontStyle.Regular);
                }
                else if (e.Field.Name == fieldBUDGETID.Name)
                {
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
                    e.Appearance.Font = new Font(xrPGMultiProjectBudgetIncome.Appearance.Cell.Font, FontStyle.Bold);
                    e.DataField.Appearance.FieldValueGrandTotal.Font = new Font(xrPGMultiProjectBudgetIncome.Appearance.Cell.Font, FontStyle.Bold); 

                    //e.Appearance.Font = new Font(xrPGMultiProjectBudgetIncome.Appearance.Cell.Font, FontStyle.Bold); 
                    Int32 projectid = UtilityMember.NumberSet.ToInteger(e.Brick.Text);
                    e.Brick.Text = "P" + ProjectNumber.ToString();
                    ProjectNumber++;
                }
            }

            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
            {
                e.Appearance.Font = new Font(xrPGMultiProjectBudgetIncome.Appearance.Cell.Font, FontStyle.Bold);
                e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            }

        }

      private void xrPGMultiProjectBudgetExpense_PrintFieldValue(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs e)
      {
          if (e.Field != null)
          {
              if (e.Field.Name == fieldLEDGERCODE1.Name || e.Field.Name == fieldLEDGERNAME1.Name
                    || e.Field.Name == fieldGROUPCODE1.Name || e.Field.Name == fieldLEDGERGROUP1.Name)
              {
                  e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
              }
              else if (e.Field.Name == fieldPROPOSEDAMOUNT1.Name || e.Field.Name == fieldAPPROVEDAMOUNT1.Name)
              {
                  e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
                  e.Appearance.Font = new Font(xrPGMultiProjectBudgetIncome.Appearance.Cell.Font, FontStyle.Bold);
                  e.DataField.Appearance.Cell.Font = new Font(xrPGMultiProjectBudgetIncome.Appearance.Cell.Font, FontStyle.Regular);
              }
              else if (e.Field.Name == fieldBUDGETID1.Name)
              {
                  e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
                  e.Appearance.Font = new Font(xrPGMultiProjectBudgetIncome.Appearance.Cell.Font, FontStyle.Bold);

                  e.Appearance.Font = new Font(xrPGMultiProjectBudgetIncome.Appearance.Cell.Font, FontStyle.Bold);
                  Int32 projectid = UtilityMember.NumberSet.ToInteger(e.Brick.Text);
                  e.Brick.Text = "P" + ProjectNumber.ToString();
                  ProjectNumber++;
              }
          }
      }
        
      private void xrPGMultiProjectBudgetExpense_PrintCell(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportCellEventArgs e)
      {
          if (e.ColumnField == null)
          {
              e.Appearance.Font = new Font(xrPGMultiProjectBudgetIncome.Appearance.Cell.Font, FontStyle.Bold);
          }
      }

      private void grpBudgetExpense_AfterPrint(object sender, EventArgs e)
      {
          grpBudgetExpense.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand;
      }

      private void xrPGMultiProjectBudgetIncome_PrintCell(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportCellEventArgs e)
      {
          if (e.ColumnField == null)
          {
              e.Appearance.Font = new Font(xrPGMultiProjectBudgetIncome.Appearance.Cell.Font, FontStyle.Bold);
          }
      }

      private void lblProjectName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
      {
          if (sender != null)
          {
              XRLabel lblProjectNames = sender as XRLabel;
              lblProjectNames.Text = BudgetProjectNames;
          }
      }

      private void grpBudgetExpense_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
      {
          ProjectNumber = 1;
      }

    }
}
