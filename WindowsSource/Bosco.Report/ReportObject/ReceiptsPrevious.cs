using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.Report.Base;
using System.Data;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class ReceiptsPrevious : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public ReceiptsPrevious()
        {
            InitializeComponent();
            
            //On 21/06/2017, For general R&P and IE reports
            ArrayList groupfilter = new ArrayList { reportSetting1.MonthlyAbstract.GROUP_IDColumn.ColumnName };
            ArrayList ledgerfilter = new ArrayList { reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName };
            DrillDownType groupdrilltype = DrillDownType.GROUP_SUMMARY_RECEIPTS;
            DrillDownType ledgerdrilltype = DrillDownType.LEDGER_SUMMARY;

            //On 21/06/2017, CC R&P and CC IE reports, we have to change drillproperties based on cc
            if (this.ReportProperties.ReportId == "RPT-041" || this.ReportProperties.ReportId == "RPT-049")
            {
                groupfilter = new ArrayList { reportSetting1.MonthlyAbstract.GROUP_IDColumn.ColumnName };
                ledgerfilter = new ArrayList { reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName, this.ReportParameters.COST_CENTRE_IDColumn.ColumnName };
                groupdrilltype = DrillDownType.GROUP_SUMMARY_RECEIPTS;
                ledgerdrilltype = DrillDownType.CC_LEDGER_SUMMARY;
            }
            
            //10/03/2017, To attach drill-down feature for main parent group too
            /*this.AttachDrillDownToRecord(xrtblReceiptGroup, xrParentGroupName,
                    groupfilter, groupdrilltype, false);

            this.AttachDrillDownToRecord(xrtblReceiptGroup, xrGroupName,
                    groupfilter, groupdrilltype, false);
            
            this.AttachDrillDownToRecord(xrTableReceipt, xrLedgerName,
                ledgerfilter, ledgerdrilltype, false, "", true);*/
        }
        #endregion

        #region Decelaration
        public double ReceiptAmount { get; set; }
        public double ReceiptAmountPrevious { get; set; }
        private int CountReceipt = 0;
        #endregion

        #region Show Reports
        public override void ShowReport()
        {

            BindReceiptSource(TransType.RC);
            base.ShowReport();
        }
        #endregion

        #region Properties
        public float CodeColumnWidth
        {
            set
            {
                xrLedgerCode.WidthF = value;

            }
        }
        public float GroupCodeColumnWidth
        {
            set
            { xrGroupCode.WidthF = value; }
        }

        public float NameColumnWidth
        {
            set
            {
                xrLedgerName.WidthF = value;
            }
        }
        public float GroupNameColumnWidth
        {
            set
            {
                xrGroupName.WidthF = value;
                xrParentGroupName.WidthF = value;
            }
        }

        public float AmountColumnWidth
        {
            set
            {
                xrLedgerAmt.WidthF = value;
                xrLedgerAmtPrevious.WidthF = value;
            }
        }
        public float GroupAmountColumnWidth
        {
            set { xrGroupAmt.WidthF = value;
            xrGroupAmtPrevious.WidthF = value;
            xrParentGroupAmt.WidthF = value;
            xrParentGroupAmtPrevious.WidthF = value;

            xrLedgerAmt.WidthF = value;
            xrLedgerAmtPrevious.WidthF = value;
                
            }
        }
        private bool hideCostcentre = false;
        public bool HideCostCentreReceipts
        {
            get { return hideCostcentre; }
            set { hideCostcentre = value; }
        }

        public float CostCentreCategoryNameWidth
        {
            set
            {
                xrCostCentreCategoryName.WidthF = value ;
            }
        }

        public bool PaymentCostCentreNameVisible
        {
            set { xrPaymentCostCentreName.Visible = value; }
        }

        public float CostCentreWidth { set { xrtblCellCostcentreName.WidthF = value; } }

        public float CostCategoryAmountWidth
        {
            get { return xrcellCCCAmount.WidthF; }
            set
            {
                xrcellCCCAmount.WidthF = value;
            }
        }

        public float CostCentreAmountWidth
        {
            get { return xrcellCCAmount.WidthF; }
            set
            {
                xrcellCCAmount.WidthF = value;
            }
        }

        public bool RecordsExists
        {
            set;
            get;
        }
        #endregion

        #region Methods
        public void HideReceiptReportHeader()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
        }

        private ResultArgs SetReportReceiptSource(TransType transType)
        {
            ResultArgs resultArgs = null;
            string sqlReceipts = string.Empty;

            if (transType == TransType.RC)
            {
                sqlReceipts = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.ReceiptsPreviousYear);

                if (ReportProperty.Current.ShowGroupCode != 0)
                {
                    xrGroupAmt.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                }
            }
            else if (transType == TransType.CRC)
            {
                sqlReceipts = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCentreReceipts);
                if (ReportProperty.Current.ShowGroupCode != 0)
                {
                    xrGroupAmt.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                }
            }

            else if (transType == TransType.CINC)
            {
                sqlReceipts = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCentreIncome);
                if (ReportProperty.Current.ShowGroupCode != 0)
                {
                    xrGroupAmt.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                }
            }

            else
            {
                sqlReceipts = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.Income);

            }
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);

                dataManager.Parameters.Add(this.ReportParameters.YEAR_FROM_PREVIOUS_YEARColumn, this.AppSetting.YearFromPrevious);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_TO_PREVIOUS_YEARColumn, this.AppSetting.YearToPrevious);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_TOColumn, this.AppSetting.YearTo);
                
                int LedgerPaddingRequired = (ReportProperties.ShowLedgerCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;
                int GroupPaddingRequired = (ReportProperties.ShowGroupCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;

                dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, LedgerPaddingRequired);
                dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, GroupPaddingRequired);

                dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre != null ? this.ReportProperties.CostCentre : "0");
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlReceipts);
            }
            return resultArgs;
        }

        public void BindReceiptSource(TransType transType)
        {
            ReceiptAmount = 0;
            ReceiptAmountPrevious = 0;
            ResultArgs resultArgs = SetReportReceiptSource(transType);
            DataTable dtReceipts = resultArgs.DataSource.Table;

            //On 01/11/2017, Since we take records previous  year from and current year to, so filter RECEIPTAMT or RECEIPTAMTPREVIOUS >0
            dtReceipts.DefaultView.RowFilter = "RECEIPTAMT >0 OR RECEIPTAMTPREVIOUS > 0";
            dtReceipts = dtReceipts.DefaultView.ToTable();

            /*if (transType.Equals(TransType.RC))
            {
                DataTable dtJournal = GetJournalSource();
                if (dtJournal != null && dtJournal.Rows.Count > 0)
                {                    
                    DataTable dtReceiptEmptyTable = ConstructReceiptTable();
                    //alex included sort order field. While merge the fields are mis matched.b
                    dtReceiptEmptyTable = dtJournal.DefaultView.ToTable(false, new string[] { "LEDGER_ID", "GROUP_ID", "GROUP_CODE", "SORT_ORDER", "PARENT_GROUP", "LEDGER_GROUP", "LEDGER_NAME", "LEDGER_CODE", "RECEIPTAMT", "RECEIPTAMTPREVIOUS" }); 
                    //  DataTable DT = dtReceiptEmptyTable.AsEnumerable().Select("LEDGER_ID, GROUP_ID, GROUP_CODE, LEDGER_GROUP, LEDGER_CODE, LEDGER_NAME, SUM(RECEIPTAMT) AS RECEIPTAMT  GROUP BY LEDGER_ID").CopyToDataTable();
                    double Recptamt = this.UtilityMember.NumberSet.ToDouble(dtReceiptEmptyTable.Compute("SUM(RECEIPTAMT)", "").ToString());
                    if (Recptamt > 0)
                    {
                        dtReceipts.Merge(dtReceiptEmptyTable);
                    }
                }

                dtJournal = GetJournalSourcePrviousYear();
                if (dtJournal != null && dtJournal.Rows.Count > 0)
                {
                    DataTable dtReceiptEmptyTable = ConstructReceiptTable();
                    //alex included sort order field. While merge the fields are mis matched.b
                    dtReceiptEmptyTable = dtJournal.DefaultView.ToTable(false, new string[] { "LEDGER_ID", "GROUP_ID", "GROUP_CODE", "SORT_ORDER", "PARENT_GROUP", "LEDGER_GROUP", "LEDGER_NAME", "LEDGER_CODE", "RECEIPTAMT", "RECEIPTAMTPREVIOUS" });
                    //  DataTable DT = dtReceiptEmptyTable.AsEnumerable().Select("LEDGER_ID, GROUP_ID, GROUP_CODE, LEDGER_GROUP, LEDGER_CODE, LEDGER_NAME, SUM(RECEIPTAMT) AS RECEIPTAMT  GROUP BY LEDGER_ID").CopyToDataTable();
                    double Recptamt = this.UtilityMember.NumberSet.ToDouble(dtReceiptEmptyTable.Compute("SUM(RECEIPTAMTPREVIOUS)", "").ToString());
                    if (Recptamt > 0)
                    {
                        dtReceipts.Merge(dtReceiptEmptyTable);
                    }
                }
            }*/
            if (transType == TransType.IC || transType == TransType.RC || transType == TransType.CRC || transType == TransType.CINC)
            {
                if (dtReceipts != null && dtReceipts.Rows.Count != 0)
                {
                    ReceiptAmount = this.UtilityMember.NumberSet.ToDouble(dtReceipts.Compute("SUM(RECEIPTAMT)", "").ToString());
                    ReceiptAmountPrevious = this.UtilityMember.NumberSet.ToDouble(dtReceipts.Compute("SUM(RECEIPTAMTPREVIOUS)", "").ToString());
                }
            }

            if (dtReceipts != null)
            {
                dtReceipts.TableName = "Receipts";
                
                //On 05/06/2017, To add Amount filter condition
                string AmountFilter = this.GetAmountFilter();
                if (AmountFilter != "")
                {
                    dtReceipts.DefaultView.RowFilter = "RECEIPTAMT " + AmountFilter;
                }

                this.DataSource = dtReceipts;
                this.DataMember = dtReceipts.TableName;
            }

            SetReportSetting();
            SetReportBorder();
            SortByLedgerorGroup();

            //On 03/07/2017, If there is no records hide, all group tables, otherwise it shows emtpy tables
            RecordsExists = true;
            if (dtReceipts.Rows.Count == 0)
            {
                grpParentGroup.Visible = false;
                grpReceiptGroup.Visible = false;
                grpReceiptLedger.Visible = false;
                RecordsExists = false;
            }
        }

        //private DataTable GetJournalSource()
        //{
        //    ResultArgs resultArgs = null;
        //    string sqlReceiptJournal = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.FinalReceiptJournal);
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
        //    return resultArgs.DataSource.Table;
        //}

        private DataTable GetJournalSourcePrviousYear()
        {
            ResultArgs resultArgs = null;
            string sqlReceiptJournal = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.FinalReceiptJournalPrevious);
            string dateProgress = this.GetProgressiveDate(this.ReportProperties.DateFrom);
            string liquidityGroupIds = this.GetLiquidityGroupIds();

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.AppSetting.YearFromPrevious);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.AppSetting.YearToPrevious);
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, TransType.JN.ToString());
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, liquidityGroupIds);
                dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, TransMode.CR.ToString());
                int LedgerPaddingRequired = (ReportProperties.ShowLedgerCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;
                int GroupPaddingRequired = (ReportProperties.ShowGroupCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;

                dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, LedgerPaddingRequired);
                dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, GroupPaddingRequired);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlReceiptJournal);
            }
            return resultArgs.DataSource.Table;
        }

        private void SetReportSetting()
        {
            float actualCodeWidth = xrGroupCode.WidthF;
            bool isCapCodeVisible = true;

            //Include / Exclude Code
            if (xrGroupCode.Tag != null && xrGroupCode.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrGroupCode.Tag.ToString());
            }
            else
            {
                xrGroupCode.Tag = xrGroupCode.WidthF;
            }

            isCapCodeVisible = (ReportProperties.ShowGroupCode == 1 || ReportProperties.ShowLedgerCode == 1);
            xrParentGroupCode.WidthF = ((ReportProperties.ShowGroupCode == 1) ? actualCodeWidth : 0);
            xrGroupCode.WidthF = ((ReportProperties.ShowGroupCode == 1) ? actualCodeWidth : 0);
            xrLedgerCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);

            grpParentGroup.Visible = (ReportProperties.ShowByLedgerGroup == 1);
            grpReceiptGroup.Visible = (ReportProperties.ShowByLedgerGroup == 1);
            grpReceiptLedger.Visible = ReportProperties.ShowByLedger == 1;
            grpCostCentreNameReceipts.Visible = (ReportProperties.ShowByCostCentre == 1);
            grpCCBreakup.Visible = (ReportProperties.BreakByCostCentre == 1);
            grpcostCenterCategory.Visible = ReportProperties.ShowByCostCentreCategory == 1;


            if (ReportProperties.ReportId == "RPT-041" || ReportProperties.ReportId == "RPT-049")
            {
                if (ReportProperties.ShowByCostCentreCategory == 1)
                    grpcostCenterCategory.Visible = true;
                else
                    grpcostCenterCategory.Visible = false;
            }

            if (grpCostCentreNameReceipts.Visible == true)
            {
                this.CosCenterName = ReportProperty.Current.CostCentreName;
                HideCostCentreReceipts = true;
            }
            else
            {
                //this.CosCenterName = string.Empty;
                HideCostCentreReceipts = false;
            }

            grpcostCenterCategory.GroupFields[0].FieldName = "";
            grpCostCentreNameReceipts.GroupFields[0].FieldName = "";
            grpParentGroup.GroupFields[0].FieldName = "";
            grpReceiptGroup.GroupFields[0].FieldName = "";
            grpReceiptLedger.GroupFields[0].FieldName = "";
            
            if (grpReceiptGroup.Visible == false && grpReceiptLedger.Visible == false)
            {
                // This code add by Amal
                if (ReportProperties.ReportId != "RPT-041" && ReportProperties.ReportId != "RPT-049")
                    grpReceiptLedger.Visible = true;
            }

            if (grpcostCenterCategory.Visible)
            {
                grpcostCenterCategory.GroupFields[0].FieldName = reportSetting1.Receipts.COST_CENTRE_CATEGORY_NAMEColumn.ColumnName;
            }

            if (grpCostCentreNameReceipts.Visible)
            {
                grpCostCentreNameReceipts.GroupFields[0].FieldName = reportSetting1.Receipts.COST_CENTRE_NAMEColumn.ColumnName;
            }

            if (grpParentGroup.Visible)
            {
                if (ReportProperties.ShowByLedgerGroup == 1)
                {
                    grpParentGroup.GroupFields[0].FieldName = reportSetting1.Receipts.PARENT_GROUPColumn.ColumnName;
                }
                else
                {
                    grpParentGroup.GroupFields[0].FieldName = reportSetting1.Receipts.PARENT_GROUPColumn.ColumnName;
                }
            }

            if (grpReceiptGroup.Visible)
            {
                if (ReportProperties.ShowByLedgerGroup == 1)
                {
                    grpReceiptGroup.GroupFields[0].FieldName = reportSetting1.Receipts.LEDGER_GROUPColumn.ColumnName;
                }
                else
                {
                    grpReceiptGroup.GroupFields[0].FieldName = reportSetting1.Receipts.LEDGER_GROUPColumn.ColumnName;
                }
            }

            if (grpReceiptLedger.Visible)
            {
                if (ReportProperties.ShowByLedger == 1)
                {
                    grpReceiptLedger.GroupFields[0].FieldName = reportSetting1.Receipts.LEDGER_NAMEColumn.ColumnName;
                }
                else
                {
                    grpReceiptLedger.GroupFields[0].FieldName = reportSetting1.Receipts.LEDGER_NAMEColumn.ColumnName;
                }
            }
        }

        private void SetReportBorder()
        {
            xrParentGroup = AlignGroupTable(xrParentGroup);
            xrtblReceiptGroup = AlignGroupTable(xrtblReceiptGroup);
            xrTableReceipt = AlignContentTable(xrTableReceipt);
            xrPaymentCostCentreName = AlignCostCentreTable(xrPaymentCostCentreName);
            xrTblCostCentreCategoryName = AlignCCCategoryTable(xrTblCostCentreCategoryName);
            if (ReportProperties.ReportId == "RPT-028" || ReportProperties.ReportId == "RPT-034")
                xrTblCostCentreCategoryName.Visible = false;
            xrCCBreakup = AlignContentTable(xrCCBreakup);
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
                            if (ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = BorderSide.Left;
                            }
                            else
                            {
                                tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
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
        
        public ResultArgs GetIncomeSource(TransType transType)
        {

            ResultArgs resultArgs = null;
            string sqlReceipts = string.Empty;

            if (transType == TransType.RC)
            {
                sqlReceipts = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.Receipts);

                if (ReportProperty.Current.ShowGroupCode != 0)
                {
                    xrGroupAmt.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                }
            }
            else if (transType == TransType.CRC)
            {
                sqlReceipts = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCentreReceipts);
                if (ReportProperty.Current.ShowGroupCode != 0)
                {
                    xrGroupAmt.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                }
            }

            else if (transType == TransType.CINC)
            {
                sqlReceipts = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCentreIncome);
                if (ReportProperty.Current.ShowGroupCode != 0)
                {
                    xrGroupAmt.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                }
            }

            else
            {
                sqlReceipts = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.Income);
            }
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);

                dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, ReportProperties.ShowByLedger);
                dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, ReportProperties.ShowByLedgerGroup);

                dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre != null ? this.ReportProperties.CostCentre : "0");
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlReceipts);
            }
            return resultArgs;

        }

        public void BindIncomeSource(ResultArgs resultArgs, TransType transType)
        {
            ReceiptAmount = 0;
            ReceiptAmountPrevious = 0; 
            //ResultArgs resultArgs = SetReportReceiptSource(transType);
            DataTable dtReceipts = resultArgs.DataSource.Table;

            if (transType == TransType.IC || transType == TransType.RC || transType == TransType.CRC || transType == TransType.CINC)
            {
                if (dtReceipts != null && dtReceipts.Rows.Count != 0)
                {
                    ReceiptAmount = this.UtilityMember.NumberSet.ToDouble(dtReceipts.Compute("SUM(RECEIPTAMT)", "").ToString());
                    ReceiptAmountPrevious = this.UtilityMember.NumberSet.ToDouble(dtReceipts.Compute("SUM(RECEIPTAMTPREVIOUS)", "").ToString());
                }
            }


            if (dtReceipts != null)
            {
                dtReceipts.TableName = "Receipts";
                this.DataSource = dtReceipts;
                this.DataMember = dtReceipts.TableName;
            }

            SetReportBorder();
            SetReportSetting();
            SortByLedgerorGroup();

            //On 03/07/2017, If there is no records hide, all group tables, otherwise it shows emtpy tables
            RecordsExists = true;
            if (dtReceipts.Rows.Count == 0)
            {
                grpParentGroup.Visible = false;
                grpReceiptGroup.Visible = false;
                grpReceiptLedger.Visible = false;
                RecordsExists = false;
            }
        }

        public void SetCostCategoryTableBorder()
        {
            xrTblCostCentreCategoryName = SetHeadingTableBorder(xrTblCostCentreCategoryName, ReportProperties.ShowHorizontalLine, ReportProperties.ShowVerticalLine);
        }

        private DataTable ConstructReceiptTable()
        {
            DataTable dtReceipts = new DataTable();
            dtReceipts.Columns.Add("LEDGER_ID", typeof(UInt32));
            dtReceipts.Columns.Add("GROUP_ID", typeof(UInt32));
            dtReceipts.Columns.Add("GROUP_CODE", typeof(string));
            dtReceipts.Columns.Add("SORT_ORDER", typeof(UInt32)); // alex included sort order field. While merge the fields are mismatched.
            dtReceipts.Columns.Add("PARENT_GROUP", typeof(string));
            dtReceipts.Columns.Add("LEDGER_GROUP", typeof(string));
            dtReceipts.Columns.Add("LEDGER_NAME", typeof(string));
            dtReceipts.Columns.Add("LEDGER_CODE", typeof(string));
            dtReceipts.Columns.Add("RECEIPTAMT", typeof(decimal));
            dtReceipts.Columns.Add("RECEIPTAMTPREVIOUS", typeof(decimal));
            return dtReceipts;
        }

        public void SortByLedgerorGroup()
        {
            if (grpParentGroup.Visible)
            {
                if (this.ReportProperties.SortByGroup == 0)
                {
                    grpParentGroup.SortingSummary.Enabled = true;
                    grpParentGroup.SortingSummary.FieldName = "SORT_ORDER";  // GROUP_CODE
                    grpParentGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpParentGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
                else
                {
                    grpParentGroup.SortingSummary.Enabled = true;
                    grpParentGroup.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_GROUP
                    grpParentGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpParentGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
            }

            if (grpReceiptGroup.Visible)
            {
                if (this.ReportProperties.SortByGroup == 0)
                {
                    grpReceiptGroup.SortingSummary.Enabled = true;
                    grpReceiptGroup.SortingSummary.FieldName = "SORT_ORDER";  // GROUP_CODE
                    grpReceiptGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpReceiptGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
                else
                {
                    grpReceiptGroup.SortingSummary.Enabled = true;
                    grpReceiptGroup.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_GROUP
                    grpReceiptGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpReceiptGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
            }
            if (grpReceiptLedger.Visible)
            {
                if (this.ReportProperties.SortByLedger == 0)
                {
                    grpReceiptLedger.SortingSummary.Enabled = true;
                    if (this.ReportProperties.ShowByLedgerGroup == 1)
                    {
                        grpReceiptLedger.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_CODE
                        grpReceiptLedger.SortingSummary.FieldName = "LEDGER_CODE";
                    }
                    else
                    {
                        grpReceiptLedger.SortingSummary.FieldName = "LEDGER_CODE";
                    }
                    grpReceiptLedger.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpReceiptLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
                else
                {
                    grpReceiptLedger.SortingSummary.Enabled = true;
                    if (this.ReportProperties.ShowByLedgerGroup == 1)
                    {
                        grpReceiptLedger.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_CODE
                        grpReceiptLedger.SortingSummary.FieldName = "LEDGER_NAME";
                    }
                    else
                    {
                        grpReceiptLedger.SortingSummary.FieldName = "LEDGER_NAME";
                    }
                    grpReceiptLedger.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpReceiptLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
            }
        }

        #endregion

        private void xrLedgerAmt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double ReceiptAmt = this.ReportProperties.NumberSet.ToDouble(xrLedgerAmt.Text);
            if (ReceiptAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrLedgerAmt.Text = "";
            }
        }

        private void grpReceiptGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.Receipts.LEDGER_GROUPColumn.ColumnName) != null)
            {
                string ParentGroup = GetCurrentColumnValue(reportSetting1.Receipts.PARENT_GROUPColumn.ColumnName) != null ?
                    GetCurrentColumnValue(reportSetting1.Receipts.PARENT_GROUPColumn.ColumnName).ToString() : string.Empty;
                string LedgerGroup = GetCurrentColumnValue(reportSetting1.Receipts.LEDGER_GROUPColumn.ColumnName) != null
                    ? GetCurrentColumnValue(reportSetting1.Receipts.LEDGER_GROUPColumn.ColumnName).ToString() : string.Empty;

                if (ParentGroup.Trim().Equals(LedgerGroup.Trim()))
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
