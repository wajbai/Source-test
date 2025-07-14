using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.Report.Base;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using System.Data;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class CostCentreJournalTransaction : Bosco.Report.Base.ReportHeaderBase
    {

        #region Constructor
        public CostCentreJournalTransaction()
        {
            InitializeComponent();
            this.AttachDrillDownToRecord(xrtblCashBankTrans, xrLedger,
               new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName }, DrillDownType.LEDGER_JOURNAL_VOUCHER, false);
        }
        #endregion

        #region Declaration
        ResultArgs resultArgs = null;
        #endregion

        #region Show Report
        public override void ShowReport()
        {
            BindJournalTransactions();
        }
        #endregion

        #region Methods
        private void BindJournalTransactions()
        {
            if (!string.IsNullOrEmpty(this.ReportProperties.DateFrom) || !string.IsNullOrEmpty(this.ReportProperties.DateTo))
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        //  this.ReportTitle = ReportProperty.Current.ReportTitle;
                        //  this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
                        setHeaderTitleAlignment();
                        SetReportTitle();
                        // this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + this.ReportProperties.DateFrom + "-" + this.ReportProperties.DateTo;
                        this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                        this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                        
                        resultArgs = GetReportSource();
                        DataView dvCashBank = resultArgs.DataSource.TableView;
                        if (dvCashBank != null)
                        {
                            dvCashBank.Table.TableName = "CashBankTransactions";
                            this.DataSource = dvCashBank;
                            this.DataMember = dvCashBank.Table.TableName;
                        }
                        SplashScreenManager.CloseForm();
                        SetReportSetting(dvCashBank);
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
                    //  this.ReportTitle = ReportProperty.Current.ReportTitle;
                    //  this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
                    setHeaderTitleAlignment();
                    SetReportTitle();
                    // this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + this.ReportProperties.DateFrom + "-" + this.ReportProperties.DateTo;
                    this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                    this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                    

                    // To show by costcentre starts
                    if (this.ReportProperties.ShowByCostCentreCategory == 1 && this.ReportProperties.ShowByCostCentre == 1)
                    {
                        grpCostCentreName.Visible = grpCostCentreCategoryName.Visible = Detail.Visible = GroupFooter1.Visible = true;
                        grpCostCategoryName.Visible = false;
                    }
                    else if (this.ReportProperties.ShowByCostCentre == 1)
                    {
                        grpCostCentreCategoryName.Visible = grpCostCategoryName.Visible = false;
                        grpCostCentreName.Visible = Detail.Visible = GroupFooter1.Visible = true;
                    }
                    else if (this.ReportProperties.ShowByCostCentreCategory == 1)
                    {
                        grpCostCategoryName.Visible = true;
                        Detail.Visible = GroupFooter1.Visible = grpCostCentreCategoryName.Visible = grpCostCategoryName.Visible == true ? false : true;
                    }
                    else
                    {
                        grpCostCentreCategoryName.Visible = grpCostCategoryName.Visible = false;
                        Detail.Visible = GroupFooter1.Visible = true;
                    }
                    // To show by costcentre ends


                    resultArgs = GetReportSource();
                    DataView dvCashBank = resultArgs.DataSource.TableView;
                    if (dvCashBank != null)
                    {
                        dvCashBank.Table.TableName = "CashBankTransactions";
                        this.DataSource = dvCashBank;
                        this.DataMember = dvCashBank.Table.TableName;
                    }
                    SplashScreenManager.CloseForm();
                    SetReportSetting(dvCashBank);
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

        private void SetReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrtblCashBankTrans = AlignContentTable(xrtblCashBankTrans);
            xrtblCCCName = AlignCCCategoryTable(xrtblCCCName);
            xrtblCCName = AlignCostCentreTable(xrtblCCName);
            xrtblGrandTotal = AlignTotalTable(xrtblGrandTotal);

            this.SetCurrencyFormat(xrCapCredit.Text, xrCapCredit);
            this.SetCurrencyFormat(xrCapDebit.Text, xrCapDebit);
        }

        private XRTable AlignCCCategoryTable(XRTable table)
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
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
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
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
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
        private XRTable AlignCostCentreTable(XRTable table)
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
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
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
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
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

        private void SetReportSetting(DataView dvReceipt)
        {
            grpCostCentreName.Visible = (ReportProperties.ShowByCostCentre == 1);

            this.SetCurrencyFormat(xrCapCredit.Text, xrCapCredit);
            this.SetCurrencyFormat(xrCapDebit.Text, xrCapDebit);
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                string CashBankTransaction = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCentreJournalTransaction);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    string ccID = (!string.IsNullOrEmpty(this.ReportProperties.CostCentre) && this.ReportProperties.CostCentre != "0" ) ? this.ReportProperties.CostCentre : "0";
                    dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, ccID);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, CashBankTransaction);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return resultArgs;
        }

        #endregion
    }
}
