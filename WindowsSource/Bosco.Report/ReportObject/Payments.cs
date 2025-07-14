using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.Report.Base;
using System.Data;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class Payments : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public Payments()
        {
            InitializeComponent();

            //On 21/06/2017, For general R&P and IE reports
            ArrayList groupfilter = new ArrayList { reportSetting1.MonthlyAbstract.GROUP_IDColumn.ColumnName, ReportParameters.CURRENCY_COUNTRY_IDColumn.ColumnName };
            ArrayList ledgerfilter = new ArrayList { reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName, ReportParameters.CURRENCY_COUNTRY_IDColumn.ColumnName };
            DrillDownType groupdrilltype = DrillDownType.GROUP_SUMMARY_PAYMENTS;
            DrillDownType ledgerdrilltype = DrillDownType.LEDGER_SUMMARY;

            //On 21/06/2017, CC R&P and CC IE reports, we have to change drillproperties based on cc
            if (this.ReportProperties.ReportId == "RPT-041" || this.ReportProperties.ReportId == "RPT-049")
            {
                groupfilter = new ArrayList { reportSetting1.MonthlyAbstract.GROUP_IDColumn.ColumnName };
                ledgerfilter = new ArrayList { reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName, this.ReportParameters.COST_CENTRE_IDColumn.ColumnName };
                groupdrilltype = DrillDownType.GROUP_SUMMARY_PAYMENTS;
                ledgerdrilltype = DrillDownType.CC_LEDGER_SUMMARY;
            }
            else if (this.ReportProperties.ReportId == "RPT-027") //On 16/11/2018, show only pure payments alone
            {
                ledgerdrilltype = DrillDownType.LEDGER_SUMMARY_PAYMENTS;
            }

            //10/03/2017, To attach drill-down feature for main parent group too
            this.AttachDrillDownToRecord(xrtblReceiptGroup, xrParentGroupName,
                    groupfilter, groupdrilltype, false);

            this.AttachDrillDownToRecord(xrtblReceiptGroup, xrPaymentGroupName,
                    groupfilter, groupdrilltype, false);
            this.AttachDrillDownToRecord(xrtblPaymentLedger, xrPaymentLedgerName,
                ledgerfilter, ledgerdrilltype, false, "", true);
            //this.AttachDrillDownToRecord(xrtblPaymentLedger, xrPaymentLedgerName,
            //    new ArrayList { reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName }, DrillDownType.LEDGER_SUMMARY_PAYMENTS, false, "", true);
        }

        #endregion

        #region Decelaration
        public double PaymentAmout { get; set; }
        private int Rowcount = 0;
        private int GroupRowCount = 0;
        private DataTable dtCCDetails = new DataTable();
        private bool PrevLedgerCCFound = false;
        #endregion

        #region Show Reports
        public override void ShowReport()
        {
            Rowcount = 0;
            GroupRowCount = 0;
            BindPaymentSource(TransType.PY);
            base.ShowReport();
        }
        #endregion

        #region Properties
        public float CodeColumnWidth
        {
            set
            {
                xrPaymentLedgerCode.WidthF = value;
            }
            get
            {
                return xrPaymentGroupCode.WidthF;
            }
        }
        public float GroupCodeColumnWidth
        {
            set
            {
                xrPaymentGroupCode.WidthF = value;
                xrtblParentCode.WidthF = value;
            }
            get { return xrPaymentGroupCode.WidthF; }
        }
        public float NameColumnWidth
        {
            set
            {

                xrPaymentLedgerName.WidthF = value;
            }
            get
            {
                return xrPaymentGroupName.WidthF;
            }
        }
        public float GroupNameColumnWidth
        {
            set
            {
                xrPaymentGroupName.WidthF = value;
                xrParentGroupName.WidthF = value;
            }
            get
            {
                return xrPaymentGroupName.WidthF;
            }
        }

        public float ParentGroupNameColumnWidth
        {
            set
            {
                xrParentGroupName.WidthF = value;
            }
        }

        public float ParentGroupCodeColumnWidth
        {
            set
            {
                xrtblParentCode.WidthF = value;
            }
        }

        public float CategoryNameWidth
        {
            set
            {
                xrCostCentreCategoryName.WidthF = value;
            }
        }

        public float CostCentreWidth { set { xrCelCostCentreName.WidthF = value; } }

        public float AmountColumnWidth
        {
            set
            {
                xrPayAmt.WidthF = value;
            }
            get
            {
                return xrgrpPayAmt.WidthF;
            }
        }
        public float GroupAmountColumnWidth
        {
            set
            {
                xrgrpPayAmt.WidthF = value;
                xrParentGroupAmt.WidthF = value;
            }
        }

        public float ParentGroupAmountColumnWidth
        {
            set
            {
                xrParentGroupAmt.WidthF = value;
            }
        }

        private bool hideccpayments = false;
        public bool HideCostCentrePayments
        {
            get { return hideccpayments; }
            set { hideccpayments = value; }

        }
        public float CostCategoryAmountWidth
        {
            get { return xrCellCCCAmount.WidthF; }
            set { xrCellCCCAmount.WidthF = value; }
        }

        public float CostCentreAmountWidth
        {
            get { return xrCellCCAmount.WidthF; }
            set
            {
                xrCellCCAmount.WidthF = value;
            }
        }

        public bool CostCentreNameVisible
        {
            set { xrtblCostcenterName.Visible = value; }
        }

        public bool RecordsExists
        {
            set;
            get;
        }

        public DataTable ReportData
        {
            get
            {
                return this.DataSource as DataTable;
            }
        }
        #endregion

        #region Methods
        private ResultArgs SetReportReceiptSource(TransType transType)
        {
            ReportProperties.ShowGroupCode = 1;
            if (this.ReportProperties.ReportCodeType == (int)ReportCodeType.Standard)
                ReportProperties.ShowGroupCode = 0;

            ResultArgs resultArgs = null;
            string sqlPayments = string.Empty;
            if (transType == TransType.PY)
            {
                sqlPayments = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.Payments);
                xrgrpPayAmt.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                if (ReportProperty.Current.ShowGroupCode == 0)
                {
                    xrPaymentGroupCode.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                }

                xrSubreportCCDetails.Visible = false;
                if (ReportProperty.Current.ShowCCDetails == 1)
                {
                    xrSubreportCCDetails.Visible = true;
                    AssignCCDetailReportSource();
                }
            }
            else if (transType == TransType.CPY)
            {
                sqlPayments = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCentrePayments);
                xrgrpPayAmt.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                if (ReportProperty.Current.ShowGroupCode == 0)
                {
                    xrPaymentGroupCode.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                }

                xrSubreportCCDetails.Visible = false;
                if (ReportProperty.Current.ShowCCDetails == 1)
                {
                    xrSubreportCCDetails.Visible = true;
                    AssignCCDetailReportSource();
                }
            }
            else if (transType == TransType.CEXP)
            {
                sqlPayments = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCentreExpenditure);
                xrgrpPayAmt.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                if (ReportProperty.Current.ShowGroupCode == 0)
                {
                    xrPaymentGroupCode.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                }
            }
            else
            {
                sqlPayments = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.Expenditure);
                //if (ReportProperty.Current.ShowGroupCode == 0)
                //{
                //    xrgrpPayAmt.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top;
                //}
            }
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.BEGIN_FROMColumn, this.AppSetting.BookBeginFrom);
                dataManager.Parameters.Add(this.ReportParameters.SHOW_GENERALATEColumn, this.ReportProperties.ReportCodeType.Equals((int)ReportCodeType.Standard) || this.ReportProperties.ReportCodeType.Equals((int)ReportCodeType.Province) ? 0 : (int)ReportCodeType.Generalate);

                int LedgerPaddingRequired = (ReportProperties.ShowLedgerCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;
                int GroupPaddingRequired = (ReportProperties.ShowGroupCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;

                dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, LedgerPaddingRequired);
                dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, GroupPaddingRequired);

                dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre != null ? this.ReportProperties.CostCentre : "0");

                //On 19/01/2023, For Cost center reports, if show by cc, make it group by cc
                if (ReportProperties.ReportId == "RPT-041" || ReportProperties.ReportId == "RPT-049")
                {
                    dataManager.Parameters.Add(this.ReportParameters.SHOW_BY_COSTCENTREColumn, this.ReportProperties.ShowByCostCentre);

                    if (!string.IsNullOrEmpty(this.ReportProperties.Ledger) && this.ReportProperties.Ledger != "0" &&
                        ReportProperties.ReportId == "RPT-041")
                    {
                        dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.Ledger);
                    }
                }

                //On 04/12/2024, To set Currnecy
                if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, "0");
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlPayments);
            }
            return resultArgs;
        }
        private void SetReportBorder()
        {
            xrtblParentGroup = AlignGroupTable(xrtblParentGroup);
            xrtblReceiptGroup = AlignGroupTable(xrtblReceiptGroup);
            xrtblPaymentLedger = AlignContentTable(xrtblPaymentLedger);

            xrtblCostcenterName = AlignCostCentreTable(xrtblCostcenterName);
            tblCostCentreCategoryName = AlignCCCategoryTable(tblCostCentreCategoryName);
            if (ReportProperties.ReportId == "RPT-028" || ReportProperties.ReportId == "RPT-034")
                tblCostCentreCategoryName.Visible = false;
            xrCCBreakup = AlignContentTable(xrCCBreakup);
        }

        private XRTable AlignContentTable(XRTable table, int rCount)
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
                        if (rCount == 1)
                        {
                            if (count == 1)
                            {
                                if (ReportProperties.ReportId == "RPT-028")//Income and Expenditure
                                {
                                    tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom; ;
                                    if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                                    {
                                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                                    }
                                }
                                else if (ReportProperties.ReportId == "RPT-041" || ReportProperties.ReportId == "RPT-049")// Cost Centre Receipts and Payments
                                {
                                    tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom; ;
                                    if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                                    {
                                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                                    }
                                }

                                else
                                {
                                    tcell.Borders = BorderSide.All;
                                    if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                                    {
                                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                                    }
                                }
                            }
                            else
                            {
                                if (ReportProperties.ReportId == "RPT-028")//Income and Expenditure
                                {
                                    tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                                }
                                else if (ReportProperties.ReportId == "RPT-041" || ReportProperties.ReportId == "RPT-049")// Cost Centre Receipts and Payments
                                {
                                    tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                                }
                                else
                                {
                                    if (count == 2)
                                    {
                                        tcell.Borders = BorderSide.Right | BorderSide.Bottom | BorderSide.Top;
                                    }
                                    else
                                    {
                                        tcell.Borders = BorderSide.All;
                                    }
                                }
                            }
                        }
                        else
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
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                        {
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
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
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
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

        private XRTable AlignGroupTable(XRTable table, int rCount)
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
                        if (rCount == 1)
                        {
                            if (ReportProperties.ReportId == "RPT-028")
                            {
                                if (count == 1)
                                {
                                    tcell.Borders = BorderSide.Bottom | BorderSide.Left | BorderSide.Right;
                                    if (count == 1 && ReportProperties.ShowGroupCode != 1)
                                    {
                                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                                    }
                                }
                                else
                                {
                                    tcell.Borders = BorderSide.Bottom | BorderSide.Right;
                                }
                            }
                            else if (ReportProperties.ReportId == "RPT-041" || ReportProperties.ReportId == "RPT-049")
                            {
                                if (count == 1)
                                {
                                    tcell.Borders = BorderSide.Bottom | BorderSide.Left | BorderSide.Right;
                                    if (count == 1 && ReportProperties.ShowGroupCode != 1)
                                    {
                                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                                    }
                                }
                                else
                                {
                                    tcell.Borders = BorderSide.Bottom | BorderSide.Right;
                                }
                            }
                            else
                            {
                                if (count == 1)
                                {
                                    tcell.Borders = BorderSide.Top | BorderSide.Left | BorderSide.Right;
                                    if (count == 1 && ReportProperties.ShowGroupCode != 1)
                                    {
                                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                                    }
                                }
                                else
                                {
                                    if (count == 2)
                                    {
                                        if (ReportProperties.ShowByLedger != 1)
                                        {
                                            tcell.Borders = BorderSide.Right | BorderSide.Top | BorderSide.Bottom;
                                        }
                                        else
                                        {
                                            tcell.Borders = BorderSide.Right | BorderSide.Top;
                                        }
                                    }
                                    else
                                    {
                                        if (ReportProperties.ShowByLedger != 1)
                                        {
                                            tcell.Borders = BorderSide.Right | BorderSide.Top | BorderSide.Bottom;
                                        }
                                        else
                                        {
                                            tcell.Borders = BorderSide.Right | BorderSide.Top;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (count == 1)
                            {
                                tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                                if (count == 1 && ReportProperties.ShowGroupCode != 1)
                                {
                                    tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                                }
                            }
                            else
                            {
                                tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                            }
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowGroupCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                            if (count == 1 && ReportProperties.ShowGroupCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else
                            tcell.Borders = BorderSide.Right;
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
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1)
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
                        if (count == 1)
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

                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1)
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
                        if (count == 1)
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

        public void BindPaymentSource(TransType transType)
        {
            Rowcount = 0;
            GroupRowCount = 0;
            PaymentAmout = 0;
            ResultArgs resultArgs = SetReportReceiptSource(transType);
            DataTable dtPayments = resultArgs.DataSource.Table;

            //On 13/08/2018, to show TDS on FD interest for accumulate interest
            /*if (transType == TransType.PY)
            {
                ResultArgs resultarg =  GetJournalTDSonFDInterestAmount();
                if (resultarg != null && resultarg.Success)
                {
                    DataTable dtTDSonFDInterest = resultarg.DataSource.Table;
                    if (dtTDSonFDInterest!=null && dtTDSonFDInterest.Rows.Count>0)
                    {
                        //On 22/05/2019, to fix ledger grouping --------------------------------------------------------
                        //dtPayments.Merge(dtTDSonFDInterest);
                        foreach (DataRow drTDS in dtTDSonFDInterest.Rows)
                        {
                            int LedgerId = ReportProperty.Current.NumberSet.ToInteger(drTDS["LEDGER_ID"].ToString());
                            decimal TDSAmount = ReportProperty.Current.NumberSet.ToDecimal(drTDS["PAYMENTAMT"].ToString());
                            dtPayments.DefaultView.RowFilter = "LEDGER_ID=" + LedgerId;
                            if (dtPayments.DefaultView.Count > 0)
                            {
                                decimal PaymentAmt = ReportProperty.Current.NumberSet.ToDecimal(dtPayments.DefaultView[0]["PAYMENTAMT"].ToString());
                                dtPayments.DefaultView[0].BeginEdit();
                                dtPayments.DefaultView[0]["PAYMENTAMT"] = (PaymentAmt + TDSAmount);
                                dtPayments.DefaultView[0].EndEdit();
                            }
                            else
                            {
                                DataRow dr = dtPayments.NewRow();
                                dr["LEDGER_ID"] = drTDS["LEDGER_ID"];
                                dr["GROUP_ID"] = drTDS["GROUP_ID"];
                                dr["GROUP_CODE"] = drTDS["GROUP_CODE"];
                                dr["SORT_ORDER"] = drTDS["SORT_ORDER"];
                                dr["PARENT_GROUP"] = drTDS["PARENT_GROUP"];
                                dr["LEDGER_GROUP"] = drTDS["LEDGER_GROUP"];
                                dr["LEDGER_NAME"] = drTDS["LEDGER_NAME"];
                                dr["LEDGER_CODE"] = drTDS["LEDGER_CODE"];
                                dr["PAYMENTAMT"] = drTDS["PAYMENTAMT"];
                                dtPayments.Rows.Add(dr);
                            }
                            dtPayments.DefaultView.RowFilter = string.Empty;
                        }
                        //-----------------------------------------------------------------------------------------------

                    }
                }
            }*/

            if (transType == TransType.EP || transType == TransType.PY || transType == TransType.CPY || transType == TransType.CEXP)
            {
                if (dtPayments != null && dtPayments.Rows.Count != 0)
                {
                    PaymentAmout = this.UtilityMember.NumberSet.ToDouble(dtPayments.Compute("SUM(PAYMENTAMT)", "").ToString());
                }
            }


            if (dtPayments != null)
            {
                dtPayments.TableName = "Payments";

                //On 05/06/2017, To add Amount filter condition
                string AmountFilter = this.GetAmountFilter();
                if (AmountFilter != "")
                {
                    dtPayments.DefaultView.RowFilter = "PAYMENTAMT " + AmountFilter;
                }

                this.DataSource = dtPayments;
                this.DataMember = dtPayments.TableName;
            }

            SetReportSetting();
            SetReportBorder();
            SortByLedgerorGroup();

            //On 03/07/2017, If there is no records hide, all group tables, otherwise it shows emtpy tables
            RecordsExists = true;
            if (dtPayments.Rows.Count == 0)
            {
                grpParentGroup.Visible = false;
                grpPaymentGroup.Visible = false;
                //grpPaymentLedger.Visible = false;
                RecordsExists = false;
            }
        }

        public void HidePaymentReportHeader()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
        }

        private void SetReportSetting()
        {
            float actualCodeWidth = xrPaymentGroupCode.WidthF;
            bool isCapCodeVisible = true;
            //Include / Exclude Code
            if (xrPaymentGroupCode.Tag != null && xrPaymentGroupCode.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrPaymentGroupCode.Tag.ToString());
            }
            else
            {
                xrPaymentGroupCode.Tag = xrPaymentGroupCode.WidthF;
            }

            isCapCodeVisible = (ReportProperties.ShowGroupCode == 1 || ReportProperties.ShowLedgerCode == 1);
            if (this.ReportProperties.ReportCodeType == (int)ReportCodeType.Standard || this.ReportProperties.ReportCodeType == (int)ReportCodeType.Province)
            {
                // tcParentGroupCode.WidthF = ((ReportProperties.ShowGroupCode == 1) ? actualCodeWidth : 0);
                xrtblParentCode.WidthF = 0;
            }
            else
            {
                xrtblParentCode.WidthF = actualCodeWidth;
            }
            xrPaymentGroupCode.WidthF = ((ReportProperties.ShowGroupCode == 1) ? actualCodeWidth : 0);
            xrPaymentLedgerCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);

            grpParentGroup.Visible = (ReportProperties.ShowByLedgerGroup == 1);

            // 25/04/2025, *Chinna for Generalate Hide the Sub Group whether show or not
            //grpPaymentGroup.Visible = (ReportProperties.ShowByLedgerGroup == 1);
            ShouldShowReceiptGroup();

            if (ReportProperties.ShowByLedger == 0 && ReportProperties.ShowByLedgerGroup == 0)
            {
                ReportProperties.ShowByLedger = 1;   ////grpPaymentLedger.Visible = ReportProperties.ShowByLedger == 1 ;
            }
            Detail.Visible = (ReportProperties.ShowByLedger == 1);

            grpCostCentreNamePayments.Visible = (ReportProperties.ShowByCostCentre == 1);
            grpCCBreakup.Visible = (ReportProperties.BreakByCostCentre == 1);
            grpcostCentreCategory.Visible = ReportProperties.ShowByCostCentreCategory == 1;


            if (ReportProperties.ReportId == "RPT-041" || ReportProperties.ReportId == "RPT-049")
            {
                if (ReportProperties.ShowByCostCentreCategory == 1)
                    grpcostCentreCategory.Visible = true;
                else
                    grpcostCentreCategory.Visible = false;
            }

            if (grpCostCentreNamePayments.Visible == true)
            {
                this.CosCenterName = ReportProperty.Current.CostCentreName;
                HideCostCentrePayments = true;
            }
            else
            {
                //this.CosCenterName = string.Empty;
                HideCostCentrePayments = false;
            }
            //if (grpPaymentGroup.Visible == false && grpPaymentLedger.Visible == false)
            //{
            //    // This code add by Amal
            //    if (ReportProperties.ReportId != "RPT-041" && ReportProperties.ReportId != "RPT-049")
            //        grpPaymentLedger.Visible = true;
            //}

            //done by alwar on 18/12/2015 (for Temporary) for sorting issue
            grpcostCentreCategory.GroupFields[0].FieldName = "";
            grpCostCentreNamePayments.GroupFields[0].FieldName = "";
            grpParentGroup.GroupFields[0].FieldName = "";
            grpPaymentGroup.GroupFields[0].FieldName = "";
            //grpPaymentLedger.GroupFields[0].FieldName = "";

            if (grpcostCentreCategory.Visible)
            {
                grpcostCentreCategory.GroupFields[0].FieldName = reportSetting1.Receipts.COST_CENTRE_CATEGORY_NAMEColumn.ColumnName;
            }

            if (grpCostCentreNamePayments.Visible)
            {
                grpCostCentreNamePayments.GroupFields[0].FieldName = reportSetting1.Receipts.COST_CENTRE_NAMEColumn.ColumnName;
            }

            if (grpParentGroup.Visible)
            {
                if (ReportProperties.ShowByLedgerGroup == 1)
                {
                    grpParentGroup.GroupFields[0].FieldName = reportSetting1.Payments.PARENT_GROUPColumn.ColumnName;
                }
                else
                {
                    grpParentGroup.GroupFields[0].FieldName = reportSetting1.Payments.PARENT_GROUPColumn.ColumnName;
                }
            }

            if (this.ReportProperties.ReportCodeType == (int)ReportCodeType.Standard) // Group else Group Code
            {
                if (grpPaymentGroup.Visible)
                {
                    if (ReportProperties.SortByGroup == 1) // Group Code
                    {
                        grpPaymentGroup.GroupFields[0].FieldName = reportSetting1.Payments.LEDGER_GROUPColumn.ColumnName;
                        //  grpPaymentGroup.GroupFields[0].FieldName = reportSetting1.Payments.GROUP_CODEColumn.ColumnName;
                    }
                    else
                    {
                        // grpPaymentGroup.GroupFields[0].FieldName = reportSetting1.Payments.GROUP_CODEColumn.ColumnName;
                        grpPaymentGroup.GroupFields[0].FieldName = reportSetting1.Payments.LEDGER_GROUPColumn.ColumnName;
                    }
                }

            }
            else
            {
                if (grpPaymentGroup.Visible)
                {
                    if (ReportProperties.SortByGroup == 1) // Group Code
                    {
                        // grpPaymentGroup.GroupFields[0].FieldName = reportSetting1.Payments.LEDGER_GROUPColumn.ColumnName;
                        grpPaymentGroup.GroupFields[0].FieldName = reportSetting1.Payments.GROUP_CODEColumn.ColumnName;
                    }
                    else
                    {
                        grpPaymentGroup.GroupFields[0].FieldName = reportSetting1.Payments.GROUP_CODEColumn.ColumnName;
                        //grpPaymentGroup.GroupFields[0].FieldName = reportSetting1.Payments.LEDGER_GROUPColumn.ColumnName;
                    }
                }
            }

            //if (grpPaymentLedger.Visible)
            //{
            //    if (ReportProperties.SortByLedger == 1)
            //    {
            //        grpPaymentLedger.GroupFields[0].FieldName = reportSetting1.Payments.LEDGER_NAMEColumn.ColumnName;
            //    }
            //    else
            //    {
            //        grpPaymentLedger.GroupFields[0].FieldName = reportSetting1.Payments.LEDGER_NAMEColumn.ColumnName;
            //    }
            //}
        }

        private void ShouldShowReceiptGroup()
        {
            grpParentGroup.Visible = (ReportProperties.ShowByLedgerGroup == 1);

            // For Generalate, hide sub-group (Receipt Group) even if grouping is on
            if (this.ReportProperties.ReportCodeType == (int)ReportCodeType.Generalate)
            {
                grpPaymentGroup.Visible = false;
            }
            else
            {
                grpPaymentGroup.Visible = (ReportProperties.ShowByLedgerGroup == 1);
            }
        }

        public ResultArgs GetExpenseReportSource(TransType transType)
        {
            ResultArgs resultArgs = null;
            string sqlPayments = string.Empty;
            if (transType == TransType.PY)
            {
                sqlPayments = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.Payments);
                xrgrpPayAmt.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                if (ReportProperty.Current.ShowGroupCode == 0)
                {
                    xrPaymentGroupCode.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                }
            }
            else if (transType == TransType.CPY)
            {
                sqlPayments = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCentrePayments);
                xrgrpPayAmt.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                if (ReportProperty.Current.ShowGroupCode == 0)
                {
                    xrPaymentGroupCode.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                }
            }
            else if (transType == TransType.CEXP)
            {
                sqlPayments = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCentreExpenditure);
                xrgrpPayAmt.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                if (ReportProperty.Current.ShowGroupCode == 0)
                {
                    xrPaymentGroupCode.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                }
            }

            else
            {
                sqlPayments = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.Expenditure);
                if (ReportProperty.Current.ShowGroupCode == 0)
                {
                    xrgrpPayAmt.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top;
                }
            }
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.BEGIN_FROMColumn, this.AppSetting.BookBeginFrom);

                dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, ReportProperties.ShowByLedger);
                dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, ReportProperties.ShowByLedgerGroup);

                dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre != null ? this.ReportProperties.CostCentre : "0");
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlPayments);
            }
            return resultArgs;
        }

        public void BindExpenseSource(ResultArgs resultArgs, TransType transType)
        {
            PaymentAmout = 0;
            //ResultArgs resultArgs = SetReportReceiptSource(transType);
            DataTable dtPayments = resultArgs.DataSource.Table;

            if (transType == TransType.EP || transType == TransType.PY || transType == TransType.CPY || transType == TransType.CEXP)
            {
                if (dtPayments != null && dtPayments.Rows.Count != 0)
                {
                    PaymentAmout = this.UtilityMember.NumberSet.ToDouble(dtPayments.Compute("SUM(PAYMENTAMT)", "").ToString());
                }
            }

            if (dtPayments != null)
            {
                dtPayments.TableName = "Payments";
                this.DataSource = dtPayments;
                this.DataMember = dtPayments.TableName;
            }
            Rowcount = 0;
            GroupRowCount = 0;
            SetReportBorder();
            SetReportSetting();
            SortByLedgerorGroup();

            //On 03/07/2017, If there is no records hide, all group tables, otherwise it shows emtpy tables
            RecordsExists = true;
            if (dtPayments.Rows.Count == 0)
            {
                grpParentGroup.Visible = false;
                grpPaymentGroup.Visible = false;
                Detail.Visible = false; //grpPaymentLedger.Visible = false;
                RecordsExists = false;
            }
        }

        public void SortByLedgerorGroup()
        {
            if (grpParentGroup.Visible)
            {
                if (this.ReportProperties.SortByGroup == 0)
                {

                    grpParentGroup.SortingSummary.Enabled = true;
                    grpParentGroup.SortingSummary.FieldName = string.Empty;
                    //On 03/04/2020, to keep ledger group second leavel proper order 
                    //grpParentGroup.SortingSummary.FieldName = "SORT_ORDER"; // GROUP_CODE
                    if (this.ReportProperties.ReportCodeType == (int)ReportCodeType.Standard)
                    {
                        grpParentGroup.SortingSummary.FieldName = reportSetting1.Payments.PARENT_GROUPColumn.ColumnName; // GROUP_CODE
                    }
                    else
                    {
                        grpParentGroup.SortingSummary.FieldName = "PARENT_CODE1"; // reportSetting1.Payments.PARENT_CODEColumn.ColumnName; // GROUP_CODE
                    }
                    grpParentGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpParentGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
                else
                {
                    grpParentGroup.SortingSummary.Enabled = true;
                    grpParentGroup.SortingSummary.FieldName = string.Empty;
                    //On 03/04/2020, to keep ledger group second leavel proper order 
                    //grpParentGroup.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_GROUP
                    if (this.ReportProperties.ReportCodeType == (int)ReportCodeType.Standard)
                    {
                        grpParentGroup.SortingSummary.FieldName = reportSetting1.Payments.PARENT_GROUPColumn.ColumnName; // GROUP_CODE
                    }
                    else
                    {
                        grpParentGroup.SortingSummary.FieldName = "PARENT_CODE1"; // reportSetting1.Payments.PARENT_CODEColumn.ColumnName;
                    }
                    grpParentGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpParentGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
            }

            if (grpPaymentGroup.Visible)
            {
                if (this.ReportProperties.SortByGroup == 0)
                {
                    grpPaymentGroup.SortingSummary.Enabled = true;
                    grpPaymentGroup.SortingSummary.FieldName = string.Empty;
                    //On 03/04/2020, to keep ledger group second leavel proper order
                    //grpPaymentGroup.SortingSummary.FieldName = "SORT_ORDER"; // GROUP_CODE
                    if (this.ReportProperties.ReportCodeType == (int)ReportCodeType.Standard)
                    {
                        grpPaymentGroup.SortingSummary.FieldName = reportSetting1.Payments.LEDGER_GROUPColumn.ColumnName;
                    }
                    else
                    {
                        grpPaymentGroup.SortingSummary.FieldName = reportSetting1.Payments.GROUP_CODEColumn.ColumnName;
                    }
                    /// 
                    grpPaymentGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpPaymentGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
                else
                {
                    grpPaymentGroup.SortingSummary.Enabled = true;
                    grpPaymentGroup.SortingSummary.FieldName = string.Empty;
                    //On 03/04/2020, to keep ledger group second leavel proper order 
                    //grpPaymentGroup.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_GROUP
                    if (this.ReportProperties.ReportCodeType == (int)ReportCodeType.Standard)
                    {
                        grpPaymentGroup.SortingSummary.FieldName = reportSetting1.Payments.LEDGER_GROUPColumn.ColumnName;
                    }
                    else
                    {
                        grpPaymentGroup.SortingSummary.FieldName = reportSetting1.Payments.GROUP_CODEColumn.ColumnName;
                    }

                    grpPaymentGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpPaymentGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
            }

            //On 10/05/2019, To remove ledger group (it is already grouped in sql itself and to have proper ledger code sorting)
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
            //if (grpPaymentLedger.Visible)
            //{
            //    if (this.ReportProperties.SortByLedger == 0)
            //    {
            //        grpPaymentLedger.SortingSummary.Enabled = true;
            //        if (this.ReportProperties.ShowByLedgerGroup == 1)
            //        {
            //            grpPaymentLedger.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_CODE
            //            grpPaymentLedger.SortingSummary.FieldName = "LEDGER_CODE";  // LEDGER_CODE
            //        }
            //        else
            //        {
            //            grpPaymentLedger.SortingSummary.FieldName = "LEDGER_CODE";  // LEDGER_CODE
            //        }
            //        grpPaymentLedger.SortingSummary.Function = SortingSummaryFunction.Avg;
            //        grpPaymentLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //    }
            //    else
            //    {
            //        grpPaymentLedger.SortingSummary.Enabled = true;
            //        if (this.ReportProperties.ShowByLedgerGroup == 1)
            //        {
            //            grpPaymentLedger.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_NAME
            //            grpPaymentLedger.SortingSummary.FieldName = "LEDGER_NAME";
            //        }
            //        else
            //        {
            //            grpPaymentLedger.SortingSummary.FieldName = "LEDGER_NAME";
            //        }
            //        grpPaymentLedger.SortingSummary.Function = SortingSummaryFunction.Avg;
            //        grpPaymentLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //    }
            //}

            //if (grpcostCentreCategory.Visible)
            //{
            //    grpcostCentreCategory.SortingSummary.Enabled = true;
            //    grpcostCentreCategory.SortingSummary.FieldName = "COST_CENTRE_CATEGORY_NAME";
            //    grpcostCentreCategory.SortingSummary.Function = SortingSummaryFunction.Avg;
            //    grpcostCentreCategory.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //}

            //if (grpCostCentreNamePayments.Visible)
            //{
            //    grpCostCentreNamePayments.SortingSummary.Enabled = true;
            //    grpCostCentreNamePayments.SortingSummary.FieldName = "COST_CENTRE_NAME";
            //    grpCostCentreNamePayments.SortingSummary.Function = SortingSummaryFunction.Avg;
            //    grpCostCentreNamePayments.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //}
        }

        /// <summary>
        /// On 05/10/2021, to get CC deatils for given project and date range
        /// </summary>
        private void AssignCCDetailReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlccDetail = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CCDetailReceiptsPayments);
            string dateProgress = this.GetProgressiveDate(this.ReportProperties.DateFrom);

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, TransType.PY.ToString());
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, TransMode.DR.ToString());

                //For CC realted reports
                if (this.ReportProperties.ReportId == "RPT-041")
                {
                    dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre);
                }

                //On 04/12/2024, To to currency based reports --------------------------------------------------------------------------------------
                if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, "0");
                }
                //----------------------------------------------------------------------------------------------------------------------------------

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlccDetail);
            }

            if (resultArgs.Success && resultArgs.DataSource.Table != null)
            {
                dtCCDetails = resultArgs.DataSource.Table;

                dtCCDetails.Columns[reportSetting1.Ledger.AMOUNT_PERIODColumn.ColumnName].ColumnName = reportSetting1.Ledger.DEBITColumn.ColumnName;

            }
        }

        private void ShowCCDetails()
        {
            //On 05/10/2021, To show CC detail for given Ledger
            if (this.ReportProperties.ShowCCDetails == 1)
            {
                xrSubreportCCDetails.Visible = false;
                if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName) != null && dtCCDetails.Rows.Count > 0)
                {
                    Int32 ledgerid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName).ToString());
                    UcCCDetail ccDetail = xrSubreportCCDetails.ReportSource as UcCCDetail;
                    dtCCDetails.DefaultView.RowFilter = string.Empty;
                    dtCCDetails.DefaultView.RowFilter = reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName + " = " + ledgerid;
                    DataTable dtCC = dtCCDetails.DefaultView.ToTable();

                    ccDetail.BindCCDetails(dtCC, false, false, false, true); // ccDetail.BindCCDetails(dtCC, false);
                    ccDetail.DateWidth = 0;

                    xrSubreportCCDetails.LeftF = xrPaymentLedgerCode.WidthF;
                    ccDetail.CCCreditCaption = "";
                    ccDetail.CCDebitCaption = "";
                    ccDetail.CCNameWidth = xrPaymentLedgerName.WidthF;
                    ccDetail.CCDebitWidth = xrPayAmt.WidthF;
                    ccDetail.CCCreditWidth = xrPayAmt.WidthF;
                    ccDetail.PRojectNameWidth = (xrPaymentLedgerName.WidthF + xrPayAmt.WidthF);
                    ccDetail.HideReportHeaderFooter();
                    dtCCDetails.DefaultView.RowFilter = string.Empty;

                    ProperBorderForLedgerRow(PrevLedgerCCFound);
                    xrSubreportCCDetails.Visible = (dtCC.Rows.Count > 0);
                    PrevLedgerCCFound = (dtCC.Rows.Count > 0);
                }
                else
                {
                    xrSubreportCCDetails.Visible = false;
                    ProperBorderForLedgerRow(false);
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
                xrPaymentLedgerCode.Borders = BorderSide.Top | BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xrPaymentLedgerName.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                xrPayAmt.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
            }
            else
            {
                xrPaymentLedgerCode.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xrPaymentLedgerName.Borders = BorderSide.Right | BorderSide.Bottom;
                xrPayAmt.Borders = BorderSide.Right | BorderSide.Bottom;
            }
        }

        #endregion

        #region Events

        private void xrtblPaymentLedger_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Rowcount++;
            AlignContentTable(xrtblPaymentLedger, Rowcount);
        }

        private void xrtblReceiptGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            GroupRowCount++;
            AlignGroupTable(xrtblReceiptGroup, GroupRowCount);
        }

        private void grpPaymentGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.Payments.LEDGER_GROUPColumn.ColumnName) != null)
            {
                string ParentGroup = GetCurrentColumnValue(reportSetting1.Payments.PARENT_GROUPColumn.ColumnName) != null ?
                    GetCurrentColumnValue(reportSetting1.Payments.PARENT_GROUPColumn.ColumnName).ToString() : string.Empty;
                string LedgerGroup = GetCurrentColumnValue(reportSetting1.Payments.LEDGER_GROUPColumn.ColumnName) != null ?
                    GetCurrentColumnValue(reportSetting1.Payments.LEDGER_GROUPColumn.ColumnName).ToString() : string.Empty;

                if (ParentGroup.Trim().Equals(LedgerGroup.Trim()))
                {
                    e.Cancel = true;
                }
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ShowCCDetails();
        }

        #endregion

        private void grpParentGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (GetCurrentColumnValue(reportSetting1.Payments.PARENT_GROUPColumn.ColumnName) != null && (this.ReportProperties.ReportCodeType != (int)ReportCodeType.Standard))
            //{
            //    string ParentGroup = GetCurrentColumnValue(reportSetting1.Payments.PARENT_GROUPColumn.ColumnName) != null ?
            //        GetCurrentColumnValue(reportSetting1.Payments.PARENT_GROUPColumn.ColumnName).ToString() : string.Empty;
            //    string GroupCode = GetCurrentColumnValue(reportSetting1.Payments.PARENT_CODEColumn.ColumnName) != null ?
            //        GetCurrentColumnValue(reportSetting1.Payments.PARENT_CODEColumn.ColumnName).ToString() : string.Empty;
            //    string LedgerGroup = GetCurrentColumnValue(reportSetting1.Payments.LEDGER_GROUPColumn.ColumnName) != null ?
            //        GetCurrentColumnValue(reportSetting1.Payments.LEDGER_GROUPColumn.ColumnName).ToString() : string.Empty;

            //    if (ParentGroup.Trim().Equals("Current Liabilities") || ParentGroup.Trim().Equals("Fixed Assets"))
            //    {
            //        GetCurrentColumnValue(reportSetting1.Payments.PARENT_GROUPColumn.ColumnName = ParentGroup.Trim() + " - " + GroupCode.Trim());
            //    }
            //}
        }

        private void xrParentGroupName_EvaluateBinding(object sender, BindingEventArgs e)
        {
            {
                if (GetCurrentColumnValue(reportSetting1.Payments.PARENT_CODEColumn.ColumnName) != null)
                {
                    string GroupCode = GetCurrentColumnValue(reportSetting1.Payments.PARENT_CODEColumn.ColumnName) != null ?
                        GetCurrentColumnValue(reportSetting1.Payments.PARENT_CODEColumn.ColumnName).ToString() : string.Empty;

                    string ParentGroup = GetCurrentColumnValue(reportSetting1.Payments.PARENT_GROUPColumn.ColumnName) != null ?
                        GetCurrentColumnValue(reportSetting1.Payments.PARENT_GROUPColumn.ColumnName).ToString() : string.Empty;

                    if (ParentGroup.Trim() == "Current Liabilities" || ParentGroup.Trim() == "Fixed Assets")
                    {
                        e.Value = ParentGroup.Trim() + " -(" + GroupCode.Trim() + ")";
                    }
                    //    string GroupCode = GetCurrentColumnValue(reportSetting1.Payments.PARENT_CODEColumn.ColumnName) != null ?
                    //        GetCurrentColumnValue(reportSetting1.Payments.PARENT_CODEColumn.ColumnName).ToString() : string.Empty;
                    //    string LedgerGroup = GetCurrentColumnValue(reportSetting1.Payments.LEDGER_GROUPColumn.ColumnName) != null ?
                    //        GetCurrentColumnValue(reportSetting1.Payments.LEDGER_GROUPColumn.ColumnName).ToString() : string.Empty;

                    //    if (ParentGroup.Trim().Equals("Current Liabilities") || ParentGroup.Trim().Equals("Fixed Assets"))
                    //    {
                    //        GetCurrentColumnValue(reportSetting1.Payments.PARENT_GROUPColumn.ColumnName = ParentGroup.Trim() + " - " + GroupCode.Trim());
                    //    }
                }
            }
        }

        ///// <summary>
        ///// On 13/08/2018, to show TDS on FD interest for accumulate interest
        ///// We show FD renewal accumulated jounral entry interest amount in receipt side
        ///// After adding TDS entry along with FD interest, for Accumulated interest TDS amount should be added with Payment side
        ///// 
        ///// this method will retrn entries which are made on TDS on FD intererest ledger while renewing accumulated intrest
        ///// </summary>
        ///// <returns></returns>
        //private ResultArgs GetJournalTDSonFDInterestAmount()
        //{
        //    ResultArgs resultArgs = null;
        //    string sqlReceiptJournal = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.FetchTDSOnFDInterest);
        //    string dateProgress = this.GetProgressiveDate(this.ReportProperties.DateFrom);
        //    string liquidityGroupIds = this.GetLiquidityGroupIds();

        //    using (DataManager dataManager = new DataManager())
        //    {
        //        dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
        //        dataManager.Parameters.Add(this.ReportParameters.DATE_PROGRESS_FROMColumn, dateProgress);
        //        dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
        //        dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, TransType.JN.ToString());
        //        dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
        //        dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, liquidityGroupIds);
        //        dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, TransMode.CR.ToString());
        //        int LedgerPaddingRequired = (ReportProperties.ShowLedgerCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;
        //        int GroupPaddingRequired = (ReportProperties.ShowGroupCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;

        //        dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, LedgerPaddingRequired);
        //        dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, GroupPaddingRequired);

        //        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
        //        resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlReceiptJournal);
        //    }
        //    return resultArgs;
        //}

    }
}
