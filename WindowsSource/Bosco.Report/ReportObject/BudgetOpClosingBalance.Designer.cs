namespace Bosco.Report.ReportObject
{
    partial class BudgetOpClosingBalance
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.grpBalanceLedger = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrtblLedger = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.tcLedgerCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.tcLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.tcAmountPeriod = new DevExpress.XtraReports.UI.XRTableCell();
            this.tcAmountProgress = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpBalanceGroup = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTableLedgerGroup = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.tcGroupCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.tcGroupName = new DevExpress.XtraReports.UI.XRTableCell();
            this.tcGroupAmountPeriod = new DevExpress.XtraReports.UI.XRTableCell();
            this.tcGroupAmountProgress = new DevExpress.XtraReports.UI.XRTableCell();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableLedgerGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 8F;
            this.TopMargin.Name = "TopMargin";
            // 
            // grpBalanceLedger
            // 
            this.grpBalanceLedger.BorderColor = System.Drawing.Color.Gainsboro;
            this.grpBalanceLedger.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.grpBalanceLedger.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblLedger});
            this.grpBalanceLedger.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("LEDGER_NAME", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.grpBalanceLedger.HeightF = 27.08333F;
            this.grpBalanceLedger.Name = "grpBalanceLedger";
            this.grpBalanceLedger.StylePriority.UseBorderColor = false;
            this.grpBalanceLedger.StylePriority.UseBorders = false;
            // 
            // xrtblLedger
            // 
            this.xrtblLedger.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrtblLedger.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblLedger.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrtblLedger.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrtblLedger.Name = "xrtblLedger";
            this.xrtblLedger.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100F);
            this.xrtblLedger.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
            this.xrtblLedger.SizeF = new System.Drawing.SizeF(721F, 25F);
            this.xrtblLedger.StylePriority.UseBorderColor = false;
            this.xrtblLedger.StylePriority.UseBorders = false;
            this.xrtblLedger.StylePriority.UsePadding = false;
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tcLedgerCode,
            this.tcLedgerName,
            this.tcAmountPeriod,
            this.tcAmountProgress});
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.Weight = 1D;
            // 
            // tcLedgerCode
            // 
            this.tcLedgerCode.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.tcLedgerCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AccountBalance.LEDGER_CODE")});
            this.tcLedgerCode.Name = "tcLedgerCode";
            this.tcLedgerCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.tcLedgerCode.StylePriority.UseBorders = false;
            this.tcLedgerCode.StylePriority.UsePadding = false;
            this.tcLedgerCode.StylePriority.UseTextAlignment = false;
            this.tcLedgerCode.Text = "tcLedgerCode";
            this.tcLedgerCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.tcLedgerCode.Weight = 0.57590000152588472D;
            // 
            // tcLedgerName
            // 
            this.tcLedgerName.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.tcLedgerName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AccountBalance.LEDGER_NAME")});
            this.tcLedgerName.Name = "tcLedgerName";
            this.tcLedgerName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.tcLedgerName.StylePriority.UseBorders = false;
            this.tcLedgerName.StylePriority.UsePadding = false;
            this.tcLedgerName.StylePriority.UseTextAlignment = false;
            this.tcLedgerName.Text = "tcLedgerName";
            this.tcLedgerName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.tcLedgerName.Weight = 3.1774335861206D;
            // 
            // tcAmountPeriod
            // 
            this.tcAmountPeriod.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.tcAmountPeriod.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AccountBalance.AMOUNT")});
            this.tcAmountPeriod.Name = "tcAmountPeriod";
            this.tcAmountPeriod.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.tcAmountPeriod.StylePriority.UseBorders = false;
            this.tcAmountPeriod.StylePriority.UsePadding = false;
            this.tcAmountPeriod.StylePriority.UseTextAlignment = false;
            xrSummary1.FormatString = "{0:n}";
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.tcAmountPeriod.Summary = xrSummary1;
            this.tcAmountPeriod.Text = "tcAmountPeriod";
            this.tcAmountPeriod.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.tcAmountPeriod.Weight = 1.8361998755741988D;
            // 
            // tcAmountProgress
            // 
            this.tcAmountProgress.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.tcAmountProgress.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AccountBalance.AMOUNT_BUDGET")});
            this.tcAmountProgress.Name = "tcAmountProgress";
            this.tcAmountProgress.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.tcAmountProgress.StylePriority.UseBorders = false;
            this.tcAmountProgress.StylePriority.UsePadding = false;
            this.tcAmountProgress.StylePriority.UseTextAlignment = false;
            xrSummary2.FormatString = "{0:n}";
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.tcAmountProgress.Summary = xrSummary2;
            this.tcAmountProgress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.tcAmountProgress.Weight = 1.6204665367814313D;
            // 
            // grpBalanceGroup
            // 
            this.grpBalanceGroup.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grpBalanceGroup.BorderColor = System.Drawing.Color.Silver;
            this.grpBalanceGroup.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.grpBalanceGroup.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTableLedgerGroup});
            this.grpBalanceGroup.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("LEDGER_GROUP", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.grpBalanceGroup.HeightF = 25F;
            this.grpBalanceGroup.Level = 1;
            this.grpBalanceGroup.Name = "grpBalanceGroup";
            this.grpBalanceGroup.StylePriority.UseBackColor = false;
            this.grpBalanceGroup.StylePriority.UseBorderColor = false;
            this.grpBalanceGroup.StylePriority.UseBorders = false;
            // 
            // xrTableLedgerGroup
            // 
            this.xrTableLedgerGroup.BackColor = System.Drawing.Color.WhiteSmoke;
            this.xrTableLedgerGroup.BorderColor = System.Drawing.Color.Silver;
            this.xrTableLedgerGroup.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTableLedgerGroup.Name = "xrTableLedgerGroup";
            this.xrTableLedgerGroup.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100F);
            this.xrTableLedgerGroup.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTableLedgerGroup.SizeF = new System.Drawing.SizeF(721F, 25F);
            this.xrTableLedgerGroup.StylePriority.UseBackColor = false;
            this.xrTableLedgerGroup.StylePriority.UseBorderColor = false;
            this.xrTableLedgerGroup.StylePriority.UsePadding = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tcGroupCode,
            this.tcGroupName,
            this.tcGroupAmountPeriod,
            this.tcGroupAmountProgress});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // tcGroupCode
            // 
            this.tcGroupCode.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.tcGroupCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AccountBalance.GROUP_CODE")});
            this.tcGroupCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tcGroupCode.Name = "tcGroupCode";
            this.tcGroupCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.tcGroupCode.StylePriority.UseBorders = false;
            this.tcGroupCode.StylePriority.UseFont = false;
            this.tcGroupCode.StylePriority.UsePadding = false;
            this.tcGroupCode.Text = "tcGroupCode";
            this.tcGroupCode.Weight = 0.57590000152588483D;
            // 
            // tcGroupName
            // 
            this.tcGroupName.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.tcGroupName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AccountBalance.LEDGER_GROUP")});
            this.tcGroupName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tcGroupName.Name = "tcGroupName";
            this.tcGroupName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.tcGroupName.StylePriority.UseBorders = false;
            this.tcGroupName.StylePriority.UseFont = false;
            this.tcGroupName.StylePriority.UsePadding = false;
            this.tcGroupName.Weight = 3.1774335861206002D;
            // 
            // tcGroupAmountPeriod
            // 
            this.tcGroupAmountPeriod.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.tcGroupAmountPeriod.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AccountBalance.AMOUNT")});
            this.tcGroupAmountPeriod.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tcGroupAmountPeriod.Name = "tcGroupAmountPeriod";
            this.tcGroupAmountPeriod.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.tcGroupAmountPeriod.StylePriority.UseBorders = false;
            this.tcGroupAmountPeriod.StylePriority.UseFont = false;
            this.tcGroupAmountPeriod.StylePriority.UsePadding = false;
            this.tcGroupAmountPeriod.StylePriority.UseTextAlignment = false;
            xrSummary3.FormatString = "{0:n}";
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.tcGroupAmountPeriod.Summary = xrSummary3;
            this.tcGroupAmountPeriod.Text = "tcGroupAmountPeriod";
            this.tcGroupAmountPeriod.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.tcGroupAmountPeriod.Weight = 1.8261665351200913D;
            // 
            // tcGroupAmountProgress
            // 
            this.tcGroupAmountProgress.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.tcGroupAmountProgress.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AccountBalance.AMOUNT_BUDGET")});
            this.tcGroupAmountProgress.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tcGroupAmountProgress.Name = "tcGroupAmountProgress";
            this.tcGroupAmountProgress.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.tcGroupAmountProgress.StylePriority.UseBorders = false;
            this.tcGroupAmountProgress.StylePriority.UseFont = false;
            this.tcGroupAmountProgress.StylePriority.UsePadding = false;
            this.tcGroupAmountProgress.StylePriority.UseTextAlignment = false;
            xrSummary4.FormatString = "{0:n}";
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.tcGroupAmountProgress.Summary = xrSummary4;
            this.tcGroupAmountProgress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.tcGroupAmountProgress.Weight = 1.6304998772355386D;
            // 
            // Detail
            // 
            this.Detail.HeightF = 0F;
            this.Detail.Name = "Detail";
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // BudgetOpClosingBalance
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.grpBalanceLedger,
            this.grpBalanceGroup,
            this.Detail});
            this.DataMember = "AccountBalance";
            this.DataSource = this.reportSetting1;
            this.Margins = new System.Drawing.Printing.Margins(48, 80, 8, 100);
            this.Version = "13.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrtblLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableLedgerGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpBalanceLedger;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpBalanceGroup;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRTable xrTableLedgerGroup;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell tcGroupCode;
        private DevExpress.XtraReports.UI.XRTableCell tcGroupName;
        private DevExpress.XtraReports.UI.XRTableCell tcGroupAmountPeriod;
        private DevExpress.XtraReports.UI.XRTableCell tcGroupAmountProgress;
        private DevExpress.XtraReports.UI.XRTable xrtblLedger;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow8;
        private DevExpress.XtraReports.UI.XRTableCell tcLedgerCode;
        private DevExpress.XtraReports.UI.XRTableCell tcLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell tcAmountPeriod;
        private DevExpress.XtraReports.UI.XRTableCell tcAmountProgress;
        private ReportSetting reportSetting1;
    }
}
