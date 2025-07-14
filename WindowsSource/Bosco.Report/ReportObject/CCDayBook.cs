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
    public partial class CCDayBook : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public CCDayBook()
        {
            InitializeComponent();
            this.AttachDrillDownToRecord(xrTableSource, xrtblLedger,
              new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName }, DrillDownType.LEDGER_VOUCHER, false, "VOUCHER_SUB_TYPE");
        }
        #endregion

        #region Variables
        ResultArgs resultArgs = null;
        int count = 0;
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindDayBook();
            base.ShowReport();
        }
        #endregion

        #region Methods
        private void BindDayBook()
        {
            //Set Report title
            if (this.ReportProperties.DayBookVoucherType != 0)
            {
                //Utility.DefaultVoucherTypes vouchertype = (Utility.DefaultVoucherTypes)UtilityMember.EnumSet.GetEnumItemType(typeof(Utility.DefaultVoucherTypes),
                //                                            this.ReportProperties.DayBookVoucherType.ToString());
                //if (ReportProperty.Current.ReportTitle.IndexOf(" (" + vouchertype.ToString() + ")") <= 0)
                //{
                //    ReportProperty.Current.ReportTitle += " (" + vouchertype.ToString() + ")";
                //}

                string selectedvtype = ReportProperty.Current.DayBookVoucherTypeName;
                if (ReportProperty.Current.ReportTitle.IndexOf(" (" + selectedvtype + ")") <= 0)
                {
                    ReportProperty.Current.ReportTitle += " (" + selectedvtype + ")";
                }
            }

            if (!string.IsNullOrEmpty(this.ReportProperties.DateFrom) || !string.IsNullOrEmpty(this.ReportProperties.DateTo) || this.ReportProperties.Project != "0")
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        setHeaderTitleAlignment();
                        SetReportTitle();

                        //    this.SetLandscapeHeader = 1065.25f;
                        //    this.SetLandscapeFooter = 1065.25f;
                        //   this.SetLandscapeFooterDateWidth = 910.25f;
                        //   this.SetLandscapeCostCentreWidth = 1065.25f;
                        this.HideCostCenter = (ReportProperties.ShowByCostCentre == 0);
                        this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                        this.grpCosName.Visible = (this.ReportProperties.ShowByCostCentre == 1);
                        this.grpLedgerGroup.Visible = (this.ReportProperties.ShowByLedgerGroup == 1);
                        this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                        grpCCBreakup.Visible = (ReportProperties.BreakByCostCentre == 1) ? true : false;
                        this.CosCenterName = ReportProperty.Current.CostCentreName;
                        
                        resultArgs = GetReportSource();
                        if (resultArgs.Success)
                        {
                            DataView dvDayBook = resultArgs.DataSource.TableView;
                            if (dvDayBook != null && dvDayBook.Count != 0)
                            {
                                dvDayBook.Table.TableName = "DAYBOOK";
                                dvDayBook = FilterByVoucherType(dvDayBook);
                                this.DataSource = dvDayBook;
                                this.DataMember = dvDayBook.Table.TableName;
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
                        SplashScreenManager.CloseForm();
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
                    setHeaderTitleAlignment();
                    SetReportTitle();

                    // this.SetLandscapeHeader = 1065.25f;
                    //  this.SetLandscapeFooter = 1065.25f;
                    //   this.SetLandscapeFooterDateWidth = 910.25f;
                    //   this.SetLandscapeCostCentreWidth = 1065.25f;
                    this.HideCostCenter = (ReportProperties.ShowByCostCentre == 0);
                    this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                    this.grpCosName.Visible = (this.ReportProperties.ShowByCostCentre == 1);

                    this.grpCostCategoryName.Visible = this.grpCostcentreCategorName.Visible = (this.ReportProperties.ShowByCostCentreCategory == 1);
                    this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                    grpCCBreakup.Visible = (this.ReportProperties.BreakByCostCentre == 1) ? true : false;

                    // To show by costcentre starts
                    if (this.ReportProperties.ShowByCostCentreCategory == 1 && this.ReportProperties.ShowByCostCentre == 1)
                    {
                        this.grpLedgerGroup.Visible = (this.ReportProperties.ShowByLedgerGroup == 1);
                        grpCosName.Visible = grpLedgerGroup.Visible = grpCostcentreCategorName.Visible = Detail.Visible = ReportFooter.Visible = true;
                        grpCostCategoryName.Visible = false;
                    }
                    else if (this.ReportProperties.ShowByCostCentre == 1)
                    {
                        this.grpLedgerGroup.Visible = (this.ReportProperties.ShowByLedgerGroup == 1);
                        grpCosName.Visible = grpLedgerGroup.Visible = Detail.Visible = ReportFooter.Visible = true;
                    }
                    else if (this.ReportProperties.ShowByCostCentreCategory == 1)
                    {
                        grpCostCategoryName.Visible = true;
                        Detail.Visible = ReportFooter.Visible = grpLedgerGroup.Visible = grpCostcentreCategorName.Visible = grpCostCategoryName.Visible == true ? false : true;
                    }
                    this.grpLedgerGroup.Visible = (this.ReportProperties.ShowByLedgerGroup == 1);
                    // To show by costcentre ends

                    this.CosCenterName = ReportProperty.Current.CostCentreName;

                    resultArgs = GetReportSource();
                    if (resultArgs.Success)
                    {
                        DataView dvDayBook = resultArgs.DataSource.TableView;
                        if (dvDayBook != null && dvDayBook.Count != 0)
                        {
                            dvDayBook.Table.TableName = "DAYBOOK";
                            dvDayBook = FilterByVoucherType(dvDayBook);
                            this.DataSource = dvDayBook;
                            this.DataMember = dvDayBook.Table.TableName;
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
                    SplashScreenManager.CloseForm();
                }
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }

            // To show by Date starts
            if (this.ReportProperties.ShowByLedgerGroup == 0)
                grpLedgerGroup.GroupFields[0].FieldName = "SORT_ID";
            else
                grpLedgerGroup.GroupFields[0].FieldName = "SORT_ORDER";

            if (this.ReportProperties.ShowByCostCentre == 0)
                grpCosName.GroupFields[0].FieldName = "SORT_ID";
            else
                grpCosName.GroupFields[0].FieldName = "COST_CENTRE_NAME";

            if (this.ReportProperties.ShowByCostCentreCategory == 0)
            {
                grpCostcentreCategorName.GroupFields[0].FieldName = "SORT_ID";
                grpCostCategoryName.GroupFields[0].FieldName = "SORT_ID";
            }
            else
            {
                grpCostcentreCategorName.GroupFields[0].FieldName = "COST_CENTRE_CATEGORY_NAME";
                grpCostCategoryName.GroupFields[0].FieldName = "COST_CENTRE_CATEGORY_NAME";
            }
            // To show by Date ends
            setReportBorder();
        }

        private void setReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrTableSource = AlignContentTable(xrTableSource);
            xrtblGrandTotal = AlignTotalTable(xrtblGrandTotal);
            xrtblTotal = AlignTotalTable(xrtblTotal);
            xrtblCCName = AlignCostCentreTable(xrtblCCName);
            xrtblCCCName = AlignCCCategoryTable(xrtblCCCName);
            xrtblLedgerGroup = AlignCostCentreTable(xrtblLedgerGroup);

            this.SetCurrencyFormat(xrCapDebit.Text, xrCapDebit);
            this.SetCurrencyFormat(xrCapCredit.Text, xrCapCredit);
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
        private ResultArgs GetReportSource()
        {
            try
            {
                string DayBook = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CCDayBook);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre != null ? this.ReportProperties.CostCentre : "0");
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, DayBook);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return resultArgs;
        }

        private DataView FilterByVoucherType(DataView dv )
        {
            //On 30/08/2018, to filter based on Voucher Type
            string VoucherTypeFilter = string.Empty;
            if (this.ReportProperties.DayBookVoucherType != 0)
            {
                //Utility.DefaultVoucherTypes vouchertype = (Utility.DefaultVoucherTypes)UtilityMember.EnumSet.GetEnumItemType(typeof(Utility.DefaultVoucherTypes),
                //                                            this.ReportProperties.DayBookVoucherType.ToString());
                //VoucherTypeFilter = "(VOUCHER_TYPE =  '" + vouchertype.ToString() + "')";
                //if (vouchertype == DefaultVoucherTypes.Receipt)
                //    VoucherTypeFilter += " OR (VOUCHER_TYPE ='" + Utility.DefaultVoucherTypes.Contra + "' AND CREDIT > 0)";
                //else if (vouchertype == DefaultVoucherTypes.Payment)
                //    VoucherTypeFilter += " OR (VOUCHER_TYPE = '" + Utility.DefaultVoucherTypes.Contra + "' AND DEBIT > 0)";

                Int32 vouchertype = this.ReportProperties.DayBookVoucherType;
                VoucherTypeFilter = "(VOUCHER_DEFINITION_ID =  " + this.ReportProperties.DayBookVoucherType.ToString() + ")";
                if (vouchertype == (Int32)DefaultVoucherTypes.Receipt)
                    VoucherTypeFilter += " OR (VOUCHER_TYPE ='" + Utility.DefaultVoucherTypes.Contra + "' AND CREDIT > 0)";
                else if (vouchertype == (Int32)DefaultVoucherTypes.Payment)
                    VoucherTypeFilter += " OR (VOUCHER_TYPE = '" + Utility.DefaultVoucherTypes.Contra + "' AND DEBIT > 0)";
            }
            dv.RowFilter = VoucherTypeFilter;
            return dv;
        }

        #endregion

        private void xrCCDebitAmt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (this.ReportProperties.ShowByCostCentreCategory == 1 && this.ReportProperties.ShowByCostCentre == 1)
            //{
            //    grpCostcentreCategorName.Visible = true;
            //    grpCostCategoryName.Visible = false;
            //}
            //else
            //{
            //    grpCostcentreCategorName.Visible = false;
            //}
        }

        private void xrCCCreditamt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }

        private void xrTableSource_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string Narration = (GetCurrentColumnValue("NARRATION") == null) ? string.Empty : GetCurrentColumnValue("NARRATION").ToString();
            xrTableSource = AlignDayBookTable(xrTableSource, Narration, count);
        }
        public virtual XRTable AlignDayBookTable(XRTable table, string Narration, int count)
        {
            int rowcount = 0;
            foreach (XRTableRow row in table.Rows)
            {
                ++rowcount;
                if (Narration == string.Empty && rowcount == 2)
                {
                    row.Visible = false;
                }
                else
                {
                    row.Visible = true;
                }
            }
            return table;
        }
        #region Events

        #endregion
    }
}
