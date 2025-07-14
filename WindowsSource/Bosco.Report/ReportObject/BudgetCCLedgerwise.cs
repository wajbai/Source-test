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
using DevExpress.XtraPivotGrid;
using DevExpress.XtraReports.UI.PivotGrid;

namespace Bosco.Report.ReportObject
{
    public partial class BudgetCCLedgerwise : Bosco.Report.Base.ReportHeaderBase
    {
        private Int32 pagenumber = 1;
        public BudgetCCLedgerwise()
        {
            InitializeComponent();
            this.SetTitleWidth(xrPGMultiCCBudgetIncome.WidthF);
        }
        #region Show Reports
        public override void ShowReport()
        {
            FetchBudgetVariance();
        }
        #endregion

        public void FetchBudgetVariance()
        {
            if (string.IsNullOrEmpty(this.ReportProperties.Budget) || this.ReportProperties.Budget.Split(',').Length == 0)
            {
                ShowReportFilterDialog();
            }
            else
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
        }

        private void FetchBudgetDetails()
        {
            try
            {
                this.SetLandscapeHeader = xrTitleBudgetExpense.WidthF;
                this.SetLandscapeFooter = xrTitleBudgetExpense.WidthF;
                this.SetLandscapeFooterDateWidth = 970.00f;
                this.HideFooterLine = false;
                setHeaderTitleAlignment();
                this.ReportPeriod = "";
                SetHeaderplain();
                fldColumnINCLedgerGroup.Visible = fldColumnEXPLedgerGroup.Visible = false;

                pagenumber = 1;
                SetReportTitle();
                this.ReportPeriod = string.Empty;
                this.BudgetName = ReportProperty.Current.BudgetName;
                AssignBudgetDateRangeTitle();
                setHeaderTitleAlignment();
                AssignReportHeader();
                this.PrintingSystem.PageSettingsChanged += new EventHandler(PrintingSystem_PageSettingsChanged);
                xrcellLedgerGroup.WidthF = (fldRowINCCCCode.Width + fldRowINCCostCentre.Width + fldRowINCNewCount.Width + fldRowINCPresentCount.Width + fldRowINCTotal.Width + fldColmunINCLedger.Width) + 15;
                xrTotalCaption.WidthF = xrLedgerName.WidthF = (fldRowINCCCCode.Width + fldRowINCCostCentre.Width + fldRowINCNewCount.Width + fldRowINCPresentCount.Width + fldRowINCTotal.Width) + 10;
                xrGrandSumProposedAmount.WidthF = xrSumProposedAmount.WidthF = (fldColmunINCLedger.Width) + 5;
                                
                fldRowINCNewCount.RunningTotal = false;
                fldRowINCPresentCount.RunningTotal = false;
                fldRowINCTotal.RunningTotal = false;

                fldRowEXPNewCount.RunningTotal = false;
                fldRowEXPPresentCount.RunningTotal = false;
                fldRowEXPTotal.RunningTotal = false;
                fldRowEXPCCCode.RunningTotal = false;
                fldRowEXPCostCentre.RunningTotal = false;

                //On 22/01/2024, To hide strength details in Expenses Ledgers
                fldRowEXPNewCount.Visible = fldRowEXPPresentCount.Visible = fldRowEXPTotal.Visible = false;
                                
                string budgetvariance = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetCCLedgerwise);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn, this.ReportProperties.Budget);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    ResultArgs resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetvariance);

                    xrtblLedgerGroup.Visible = xrtblLedgerName.Visible = xrTblSumLGTotal.Visible = false;
                    xrPGMultiCCBudgetIncome.DataSource = null;
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        DataTable dtBudgetDetails = resultArgs.DataSource.Table;
                        //For Budget Income Ledgers ---------------------------------------------------------------------------------------------------
                        dtBudgetDetails.DefaultView.RowFilter = string.Empty;
                        dtBudgetDetails.DefaultView.RowFilter = reportSetting1.BudgetCostCentre.TRANS_MODEColumn.ColumnName + " = '"+ TransactionMode.CR  + "' AND " +
                                                "(" + reportSetting1.BUDGET_STRENGTH_DETAIL.NEW_COUNTColumn.ColumnName + " >0 OR " +
                                                reportSetting1.BUDGET_STRENGTH_DETAIL.PRESENT_COUNTColumn.ColumnName + " >0 OR " +
                                                reportSetting1.BudgetCostCentre.PROPOSED_AMOUNTColumn.ColumnName + " >0)";
                        DataTable dtBudgetIncomeLedgers = dtBudgetDetails.DefaultView.ToTable();
                        dtBudgetIncomeLedgers.Columns.Add("TOTAL_COUNT", typeof(System.Int32), reportSetting1.BUDGET_STRENGTH_DETAIL.NEW_COUNTColumn.ColumnName + "+" +
                                                                reportSetting1.BUDGET_STRENGTH_DETAIL.PRESENT_COUNTColumn.ColumnName);
                        grpBudgetIncomeCCDetails.Visible = xrPGMultiCCBudgetIncome.Visible = grpIncomeLedgerDetail.Visible = false;
                        
                        if (dtBudgetIncomeLedgers.Rows.Count > 0)
                        {
                            grpBudgetIncomeCCDetails.Visible = xrPGMultiCCBudgetIncome.Visible = grpIncomeLedgerDetail.Visible = true;
                            xrPGMultiCCBudgetIncome.DataSource = dtBudgetIncomeLedgers;
                            xrPGMultiCCBudgetIncome.DataMember = dtBudgetIncomeLedgers.TableName;

                            //Show income Ledger Legent in Footer details
                            DataView dvLedgerDetails = new DataView(dtBudgetIncomeLedgers);
                            dvLedgerDetails.RowFilter = reportSetting1.BudgetCostCentre.LEDGER_NAMEColumn.ColumnName + " <> ''";
                            DataTable dtLedgerDetails = dvLedgerDetails.ToTable();
                            if (dtLedgerDetails.Rows.Count > 0)
                            {
                                MakeLedgerLegent(dtLedgerDetails, true);
                            }
                        }
                        //-----------------------------------------------------------------------------------------------------------------------------

                        //For Budget Expenses Ledgers -------------------------------------------------------------------------------------------------
                        dtBudgetDetails.DefaultView.RowFilter = string.Empty;
                        dtBudgetDetails.DefaultView.RowFilter = reportSetting1.BudgetCostCentre.TRANS_MODEColumn.ColumnName + " = '" + TransactionMode.DR + "' AND " +
                                                "(" + reportSetting1.BUDGET_STRENGTH_DETAIL.NEW_COUNTColumn.ColumnName + " >0 OR " +
                                                reportSetting1.BUDGET_STRENGTH_DETAIL.PRESENT_COUNTColumn.ColumnName + " >0 OR " +
                                                reportSetting1.BudgetCostCentre.PROPOSED_AMOUNTColumn.ColumnName + " >0)";
                        DataTable dtBudgetExpenseLedgers = dtBudgetDetails.DefaultView.ToTable();
                        dtBudgetExpenseLedgers.Columns.Add("TOTAL_COUNT", typeof(System.Int32), reportSetting1.BUDGET_STRENGTH_DETAIL.NEW_COUNTColumn.ColumnName + "+" +
                                                                reportSetting1.BUDGET_STRENGTH_DETAIL.PRESENT_COUNTColumn.ColumnName);
                        grpBudgetExpenseCCDetails.Visible = xrPGMultiCCBudgetExpense.Visible = grpExpenseLedgerDetail.Visible = false;
                        if (dtBudgetExpenseLedgers.Rows.Count > 0)
                        {
                            grpBudgetExpenseCCDetails.Visible = xrPGMultiCCBudgetExpense.Visible = grpExpenseLedgerDetail.Visible = true;
                            xrPGMultiCCBudgetExpense.DataSource = dtBudgetExpenseLedgers;
                            xrPGMultiCCBudgetExpense.DataMember = dtBudgetExpenseLedgers.TableName;

                            //Show expenses Ledger Legent in Footer details
                            DataView dvLedgerDetails = new DataView(dtBudgetExpenseLedgers);
                            dvLedgerDetails.RowFilter = reportSetting1.BudgetCostCentre.LEDGER_NAMEColumn.ColumnName + " <> ''";
                            DataTable dtLedgerDetails = dvLedgerDetails.ToTable();
                            if (dtLedgerDetails.Rows.Count > 0)
                            {
                                MakeLedgerLegent(dtLedgerDetails, false);
                            }
                        }
                        //-----------------------------------------------------------------------------------------------------------------------------

                        /*this.DataSource = dtLedgerDetails;
                        this.DataMember = dtLedgerDetails.TableName;
                        xrtblLedgerGroup = AlignGroupTable(xrtblLedgerGroup);
                        xrtblLedgerName = AlignContentTable(xrtblLedgerName);
                        xrTblSumLGTotal = AlignGroupTable(xrTblSumLGTotal);
                        xrtblLedgerGroup.Visible = xrtblLedgerName.Visible = xrTblSumLGTotal.Visible = (dtLedgerDetails.Rows.Count > 0);*/

                        
                    }
                }                
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, false);
            }
            finally { }
        }

        private void PrintingSystem_PageSettingsChanged(object sender, EventArgs e)
        {
            pagenumber = 1;
            PrintingSystemBase ps = sender as PrintingSystemBase;
            bool isLocationChanged = false;
            int newPageWidth =
                ps.PageBounds.Width - ps.PageMargins.Left - ps.PageMargins.Right;
            int currentPageWidth =
                this.PageWidth - this.Margins.Left - this.Margins.Right;
            int shift = currentPageWidth - newPageWidth;
            
            isLocationChanged = true;
            if (isLocationChanged == true)
            {
                this.Margins.Top = ps.PageMargins.Top;
                this.Margins.Bottom = ps.PageMargins.Bottom;
                this.Margins.Left = ps.PageMargins.Left;
                this.Margins.Right = ps.PageMargins.Right;
                this.PaperKind = ps.PageSettings.PaperKind;
                this.PaperName = ps.PageSettings.PaperName;
                this.Landscape = ps.PageSettings.Landscape;
                AssignReportHeader();
                this.CreateDocument();
            }
        }

      private void AssignReportHeader()
      {
          this.PageHeader.Visible = false;
          this.VerticalContentSplitting = DevExpress.XtraPrinting.VerticalContentSplitting.Smart;
          
          float titleLeft = xrpicReportLogoLeft1.LeftF;
          float titleWidth = xrTitleBudgetExpense.WidthF;// this.PageSize.Width - 35;
          xrlblBudgetname1.Visible = true;
          if (this.ReportProperties.VoucherPrintShowLogo == "1")
          {
              HideReportLogoLeft = true;
              titleLeft = xrpicReportLogoLeft1.LeftF + xrpicReportLogoLeft1.WidthF;
              titleWidth = (titleWidth - (xrpicReportLogoLeft1.LeftF + xrpicReportLogoLeft1.WidthF));
          }
          else
          {
              HideReportLogoLeft = false;
          }
          xrlblInstituteName.LeftF = xrlblInstituteAddress.LeftF = xrReportTitle.LeftF = xrHeaderProjectName.LeftF = titleLeft;
          xrlblInstituteName.WidthF = xrlblInstituteAddress.WidthF = xrHeaderProjectName.WidthF = xrReportTitle.WidthF = titleWidth;
          xrlblBudgetname1.WidthF = titleWidth;
          xrlblInstituteName.Text = this.GetInstituteName();
          xrlblInstituteAddress.Text = ReportProperty.Current.LegalAddress;
          xrlblBudgetname1.Text = ReportProperty.Current.BudgetName;
          

          if (this.ReportProperties.VoucherPrintProject == "1")
          {
              xrHeaderProjectName.Text = ReportProperty.Current.ProjectTitle;
          }
          else
          {
              xrHeaderProjectName.Text = "";
          }
          xrReportTitle.Text = ReportProperty.Current.ReportTitle;
          this.HideReportHeader = this.HidePageFooter = true;
      }

      private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
      {
          (sender as PageHeaderBand).Visible = (pagenumber==1);
          pagenumber++;
      }

      private void grpFooterLedgerGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
      {
          pagenumber = 1;
      }
            
      private void xrPGMultiAbstractIncome_CustomRowHeight(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotCustomRowHeightEventArgs e)
      {
          int defaultrowheight = e.RowHeight;//Default height
          Graphics gr = Graphics.FromHwnd(IntPtr.Zero);
          XRPivotGrid grd = (sender as XRPivotGrid);
          try
          {
              if (e.Field != null)
              {
                  if (e.ValueType != PivotGridValueType.Total && e.ValueType != PivotGridValueType.GrandTotal)
                  {
                      if (e.Field.Name == fldRowINCTotal.Name || e.Field.Name == fldRowEXPTotal.Name)
                      {
                          fldRowINCCostCentre.Appearance.Cell.WordWrap = true;
                          fldRowINCCostCentre.Appearance.FieldValue.WordWrap = true;
                          fldRowEXPCostCentre.Appearance.Cell.WordWrap = true;
                          fldRowEXPCostCentre.Appearance.FieldValue.WordWrap = true;
                          e.RowHeight = defaultrowheight;
                          string ccname = string.Empty;
                          Int32 RowHeightccName = 0;
                          if (grd == xrPGMultiCCBudgetIncome && fldRowINCCostCentre.Visible)
                          {
                              if (e.GetFieldValue(fldRowINCCostCentre, e.RowIndex) != null)
                              {
                                  ccname = e.GetFieldValue(fldRowINCCostCentre, e.RowIndex).ToString().Trim();
                              }
                              SizeF size = gr.MeasureString(ccname, xrPGMultiCCBudgetIncome.Styles.CellStyle.Font, fldRowINCCostCentre.Width);
                              RowHeightccName = Convert.ToInt32(size.Height + 0.5);
                          }
                          else if (grd == xrPGMultiCCBudgetExpense && fldRowEXPCostCentre.Visible)
                          {
                              if (e.GetFieldValue(fldRowEXPCostCentre, e.RowIndex) != null)
                              {
                                  ccname = e.GetFieldValue(fldRowEXPCostCentre, e.RowIndex).ToString().Trim();
                              }
                              SizeF size = gr.MeasureString(ccname, xrPGMultiCCBudgetExpense.Styles.CellStyle.Font, fldRowEXPCostCentre.Width);
                              RowHeightccName = Convert.ToInt32(size.Height + 0.5);
                          }
                          
                          e.RowHeight = Math.Max(e.RowHeight, RowHeightccName);
                      }
                  }
              }
          }
          catch (Exception err)
          {
              e.RowHeight = defaultrowheight;//Default height
              MessageRender.ShowMessage("Not able to set row right " + err.Message);
          }
      }

      private void xrPGMultiCCBudgetIncome_CustomFieldValueCells(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotCustomFieldValueCellsEventArgs e)
      {
          //Hide Empty column (ledger name is empty)
          if (fldColumnINCLedgerGroup.Area == PivotArea.ColumnArea)
          {
              for (int i = e.GetCellCount(true) - 1; i >= 0; i--)
              {
                  if (e.GetCell(true, i) != null)
                  {
                      DevExpress.XtraReports.UI.PivotGrid.FieldValueCell cell = e.GetCell(true, i);

                      /*if ((cell.Parent != null && cell.Parent.DisplayText.ToLower().Contains("to")) &&
                          (cell.DataField != null && cell.DataField.Name.ToLower().Contains("ams")))
                          e.Remove(cell);*/

                      if ((cell.Parent != null && cell.Parent.DisplayText == "") &&
                          (cell.DataField != null && cell.DataField.Name != ""))
                      {
                          e.Remove(cell);
                      }
                  }
              }
          }  
      }

      private void xrPGMultiCCBudgetIncome_FieldValueDisplayText(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs e)
      {
          XRPivotGrid grd = (sender as XRPivotGrid);
          decimal totalnewcount = 0;
          decimal totalpresentcount = 0;

          if (e.ValueType == PivotGridValueType.GrandTotal)
          {
              if (grd == xrPGMultiCCBudgetIncome)
              {
                  for (int i = 0; i < e.MaxIndex; i++)
                  {
                      object value = xrPGMultiCCBudgetIncome.GetFieldValue(fldRowINCNewCount, i);
                      if (value != null)
                          totalnewcount += UtilityMember.NumberSet.ToInteger(value.ToString());

                      value = xrPGMultiCCBudgetIncome.GetFieldValue(fldRowINCPresentCount, i);
                      if (value != null)
                          totalpresentcount += UtilityMember.NumberSet.ToInteger(value.ToString());
                  }
                  e.DisplayText = "New :" + totalnewcount + " |Present : " + totalpresentcount.ToString() + " |Total :" + (totalnewcount + totalpresentcount).ToString();
              }
              else if (grd == xrPGMultiCCBudgetExpense)
              {
                 /* for (int i = 0; i < e.MaxIndex; i++)
                  {
                      object value = xrPGMultiCCBudgetExpense.GetFieldValue(fldRowEXPNewCount, i);
                      if (value != null)
                          totalnewcount += UtilityMember.NumberSet.ToInteger(value.ToString());

                      value = xrPGMultiCCBudgetExpense.GetFieldValue(fldRowEXPPresentCount, i);
                      if (value != null)
                          totalpresentcount += UtilityMember.NumberSet.ToInteger(value.ToString());
                  }
                  e.DisplayText = "New :" + totalnewcount + " |Present : " + totalpresentcount.ToString() + " |Total :" + (totalnewcount + totalpresentcount).ToString();*/
                  e.DisplayText = "Total";
              }
          }
      }

      private void xrPGMultiCCBudgetIncome_PrintCell(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportCellEventArgs e)
      {
          if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.Cell)
          {
              if (e.Brick.TextValue == null || this.UtilityMember.NumberSet.ToDouble(e.Brick.TextValue.ToString()) == 0) { e.Brick.Text = ""; }
          }

          if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.TotalCell ||
                  e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.GrandTotalCell |
              e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.CustomTotalCell)
          {
              e.Appearance.Font = new Font(xrPGMultiCCBudgetIncome.Styles.FieldValueStyle.Font, FontStyle.Bold);
          }
          else if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.Cell)
          {
              e.Appearance.Font = xrPGMultiCCBudgetIncome.Styles.CellStyle.Font;
          }
      }

      private void xrPGMultiCCBudgetIncome_PrintFieldValue(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs e)
      {
          if (e.Field != null && e.Field.Area == DevExpress.XtraPivotGrid.PivotArea.ColumnArea)
          {
              if (e.Field != null && e.Field.Area == DevExpress.XtraPivotGrid.PivotArea.ColumnArea)
              {
                  e.Appearance.Font = new Font(xrPGMultiCCBudgetIncome.Styles.FieldValueStyle.Font, FontStyle.Bold);
                  e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
                  e.Appearance.BackColor = xrPGMultiCCBudgetIncome.Styles.FieldHeaderStyle.BackColor;
                  e.Appearance.BorderColor = xrPGMultiCCBudgetIncome.Styles.FieldHeaderStyle.BorderColor;

                  /*if (e.Field != null && e.Field.Area == DevExpress.XtraPivotGrid.PivotArea.ColumnArea && e.Field == fldColmunLedger)
                  {
                      string displaytext = e.Brick.Text;
                      Char[] columnheader = displaytext.ToCharArray();
                      foreach (char ch in columnheader)
                      {
                          displaytext += ch.ToString() + System.Environment.NewLine;
                      }
                      e.Brick.Text = displaytext;
                  } */
              }

              // On 06/06/2023, To allow split content to next page
              if (e.Field.FieldName == "LEDGER_GROUP")
              {
                  /*bool txt = e.Brick.Separable;
                  string n = e.Brick.Text;
                  if (n == "Tax Deducted at Source (TDS)")
                  {

                  }
                  //On 15/09/2002, If content is splitted into two pages (bottom text), let us fix to anyother page fully and keep space ---------
                  if (e.Brick.SeparableVert)
                  {
                      e.Brick.SeparableVert = false;
                  }*/

                  PanelBrick panelBrick = e.Brick as PanelBrick;
                  if (panelBrick != null)
                  {
                      ((TextBrick)panelBrick.Bricks[0]).Size = new SizeF(((TextBrick)panelBrick.Bricks[0]).Size.Width, 100);
                      ((TextBrick)panelBrick.Bricks[0]).VertAlignment = DevExpress.Utils.VertAlignment.Center;
                  }
              }
              //-------------------------------------------------------------------------------------------------------------------------------
          }
          else if (e.Field != null && e.Field.Area == DevExpress.XtraPivotGrid.PivotArea.RowArea)
          {
              e.Appearance.Font = xrPGMultiCCBudgetIncome.Styles.FieldValueStyle.Font;

              if (e.Field == fldRowINCCostCentre || e.Field == fldRowEXPCostCentre)
              {
                  e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
              }
              else if (e.Field == fldDataINCProposedAmt || e.Field == fldDataEXPProposedAmt)
              {
                  e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
              }

              if (e.Brick.TextValue == null || this.UtilityMember.NumberSet.ToDouble(e.Brick.TextValue.ToString()) == 0
                  && ( (e.Field == fldRowINCNewCount || e.Field == fldRowINCPresentCount || e.Field == fldRowINCTotal) ||
                       (e.Field == fldRowEXPNewCount || e.Field == fldRowEXPPresentCount || e.Field == fldRowEXPTotal)))
              {
                  e.Brick.Text = "";
              }
          }
          else if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
          {
              e.Appearance.Font = new Font(xrPGMultiCCBudgetIncome.Styles.FieldValueStyle.Font, FontStyle.Bold);
              e.Brick.SeparableHorz = true;
          }
      }

      private void xrPGMultiCCBudgetIncome_PrintHeader(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportHeaderEventArgs e)
      {
          if (e.Field != null)
          {
              DevExpress.XtraPrinting.TextBrick textBrick = e.Brick as DevExpress.XtraPrinting.TextBrick;
              textBrick.Location = new PointF(textBrick.Location.X, 0);
              textBrick.Size = new SizeF(textBrick.Size.Width, 110);
              //textBrick.Size = new SizeF(textBrick.Size.Width, 225);
          }
      }

      private void MakeLedgerLegent(DataTable dtBudgetLedger, bool IsIncomeLedger)
      {
          DataTable dtLedgerLegent1 = dtBudgetLedger.Clone();
          DataTable dtLedgerLegent2 = dtBudgetLedger.Clone();
          DataTable dtLedgerLegent3 = dtBudgetLedger.Clone();
          DataTable dt = dtBudgetLedger.DefaultView.ToTable(true, new string[] { reportSetting1.Receipts.LEDGER_GROUPColumn.ColumnName });
          dt.DefaultView.Sort = reportSetting1.Receipts.LEDGER_GROUPColumn.ColumnName;
          dt = dt.DefaultView.ToTable();

          int irow = 1;
          foreach (DataRow dr in dt.Rows)
          {
              DataTable dtResult = dtBudgetLedger.AsEnumerable()
                  .Where(row => row.Field<string>(reportSetting1.Receipts.LEDGER_GROUPColumn.ColumnName) == dr[reportSetting1.Receipts.LEDGER_GROUPColumn.ColumnName].ToString())
                  .Select(r => r).CopyToDataTable();
              int remainder, quotient = Math.DivRem(irow, 3, out remainder);
              if (remainder == 1) dtLedgerLegent1.Merge(dtResult);
              else if (remainder == 2) dtLedgerLegent2.Merge(dtResult);
              else dtLedgerLegent3.Merge(dtResult);
              irow++;
          }

          if (IsIncomeLedger)
          {
              UcLedgerDetail subledgerdetails1 = xrINCLedgerLegent1.ReportSource as UcLedgerDetail;
              subledgerdetails1.BindLedgerDetails(dtLedgerLegent1);

              UcLedgerDetail subledgerdetails2 = xrINCLedgerLegent2.ReportSource as UcLedgerDetail;
              subledgerdetails2.BindLedgerDetails(dtLedgerLegent2);

              UcLedgerDetail subledgerdetails3 = xrINCLedgerLegent3.ReportSource as UcLedgerDetail;
              subledgerdetails3.BindLedgerDetails(dtLedgerLegent3);
              xrINCLedgerLegent1.Visible = (dtLedgerLegent1.Rows.Count > 0);
              xrINCLedgerLegent2.Visible = (dtLedgerLegent2.Rows.Count > 0);
              xrINCLedgerLegent3.Visible = (dtLedgerLegent3.Rows.Count > 0);
          }
          else
          {
              UcLedgerDetail subledgerdetails1 = xrEXPLedgerLegent1.ReportSource as UcLedgerDetail;
              subledgerdetails1.BindLedgerDetails(dtLedgerLegent1);

              UcLedgerDetail subledgerdetails2 = xrEXPLedgerLegent2.ReportSource as UcLedgerDetail;
              subledgerdetails2.BindLedgerDetails(dtLedgerLegent2);

              UcLedgerDetail subledgerdetails3 = xrEXPLedgerLegent3.ReportSource as UcLedgerDetail;
              subledgerdetails3.BindLedgerDetails(dtLedgerLegent3);

              xrEXPLedgerLegent1.Visible = (dtLedgerLegent1.Rows.Count > 0);
              xrEXPLedgerLegent2.Visible = (dtLedgerLegent2.Rows.Count > 0);
              xrEXPLedgerLegent3.Visible = (dtLedgerLegent3.Rows.Count > 0);
          }
      }

      private void xrINCLedgerLegent3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
      {
          
      }

    }
}
