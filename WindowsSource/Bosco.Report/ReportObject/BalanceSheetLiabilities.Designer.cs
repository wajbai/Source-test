namespace Bosco.Report.ReportObject
{
    partial class BalanceSheetLiabilities
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BalanceSheetLiabilities));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRGroupSortingSummary xrGroupSortingSummary1 = new DevExpress.XtraReports.UI.XRGroupSortingSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRGroupSortingSummary xrGroupSortingSummary2 = new DevExpress.XtraReports.UI.XRGroupSortingSummary();
            DevExpress.XtraReports.UI.XRGroupSortingSummary xrGroupSortingSummary3 = new DevExpress.XtraReports.UI.XRGroupSortingSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            this.xrTblLiabilityLedgerName = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrLiaLedgerCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.tcLiabilityLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLiabilityLedgerAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.calLiaTotalAmt = new DevExpress.XtraReports.UI.CalculatedField();
            this.calAssetAmt = new DevExpress.XtraReports.UI.CalculatedField();
            this.calOpLiability = new DevExpress.XtraReports.UI.CalculatedField();
            this.calOpAsset = new DevExpress.XtraReports.UI.CalculatedField();
            this.grpLedger = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTblLiabilityLedgerGroup = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrLiaGroupCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.tcLiabilityGrpGroupName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrGroupTransCredit = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpLedgerGroup = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.grpParentGroup = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTblLiabilityParentGroup = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.tcLiabilityParentCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.tcLiabilityParentGroupName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrParentGroupAmount = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblLiabilityLedgerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblLiabilityLedgerGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblLiabilityParentGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Expanded = false;
            resources.ApplyResources(this.Detail, "Detail");
            // 
            // xrTblLiabilityLedgerName
            // 
            resources.ApplyResources(this.xrTblLiabilityLedgerName, "xrTblLiabilityLedgerName");
            this.xrTblLiabilityLedgerName.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTblLiabilityLedgerName.Name = "xrTblLiabilityLedgerName";
            this.xrTblLiabilityLedgerName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTblLiabilityLedgerName.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTblLiabilityLedgerName.StyleName = "styleRow";
            this.xrTblLiabilityLedgerName.StylePriority.UseBorderColor = false;
            this.xrTblLiabilityLedgerName.StylePriority.UseBorders = false;
            this.xrTblLiabilityLedgerName.StylePriority.UsePadding = false;
            this.xrTblLiabilityLedgerName.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrLiaLedgerCode,
            this.tcLiabilityLedgerName,
            this.xrLiabilityLedgerAmt});
            this.xrTableRow3.Name = "xrTableRow3";
            resources.ApplyResources(this.xrTableRow3, "xrTableRow3");
            // 
            // xrLiaLedgerCode
            // 
            this.xrLiaLedgerCode.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLiaLedgerCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BalanceSheet.LEDGER_CODE")});
            this.xrLiaLedgerCode.Name = "xrLiaLedgerCode";
            this.xrLiaLedgerCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLiaLedgerCode.StylePriority.UseBorders = false;
            this.xrLiaLedgerCode.StylePriority.UsePadding = false;
            this.xrLiaLedgerCode.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrLiaLedgerCode, "xrLiaLedgerCode");
            // 
            // tcLiabilityLedgerName
            // 
            this.tcLiabilityLedgerName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BalanceSheet.LEDGER_NAME")});
            this.tcLiabilityLedgerName.Name = "tcLiabilityLedgerName";
            this.tcLiabilityLedgerName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.tcLiabilityLedgerName.StylePriority.UsePadding = false;
            this.tcLiabilityLedgerName.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.tcLiabilityLedgerName, "tcLiabilityLedgerName");
            // 
            // xrLiabilityLedgerAmt
            // 
            this.xrLiabilityLedgerAmt.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLiabilityLedgerAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BalanceSheet.AMOUNT", "{0:n}")});
            this.xrLiabilityLedgerAmt.Name = "xrLiabilityLedgerAmt";
            this.xrLiabilityLedgerAmt.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLiabilityLedgerAmt.StylePriority.UseBorders = false;
            this.xrLiabilityLedgerAmt.StylePriority.UsePadding = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            this.xrLiabilityLedgerAmt.Summary = xrSummary1;
            resources.ApplyResources(this.xrLiabilityLedgerAmt, "xrLiabilityLedgerAmt");
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // calLiaTotalAmt
            // 
            this.calLiaTotalAmt.DataMember = "BalanceSheet";
            this.calLiaTotalAmt.Name = "calLiaTotalAmt";
            // 
            // calAssetAmt
            // 
            this.calAssetAmt.DataMember = "BalanceSheet";
            this.calAssetAmt.Name = "calAssetAmt";
            // 
            // calOpLiability
            // 
            this.calOpLiability.DataMember = "BalanceSheet";
            this.calOpLiability.Name = "calOpLiability";
            // 
            // calOpAsset
            // 
            this.calOpAsset.DataMember = "BalanceSheet";
            this.calOpAsset.Name = "calOpAsset";
            // 
            // grpLedger
            // 
            this.grpLedger.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblLiabilityLedgerName});
            this.grpLedger.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("LEDGER_NAME", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpLedger, "grpLedger");
            this.grpLedger.Name = "grpLedger";
            xrGroupSortingSummary1.FieldName = "LEDGER_NAME";
            this.grpLedger.SortingSummary = xrGroupSortingSummary1;
            // 
            // xrTblLiabilityLedgerGroup
            // 
            this.xrTblLiabilityLedgerGroup.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrTblLiabilityLedgerGroup, "xrTblLiabilityLedgerGroup");
            this.xrTblLiabilityLedgerGroup.Name = "xrTblLiabilityLedgerGroup";
            this.xrTblLiabilityLedgerGroup.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow9});
            this.xrTblLiabilityLedgerGroup.StyleName = "styleGroupRow";
            this.xrTblLiabilityLedgerGroup.StylePriority.UseBorders = false;
            // 
            // xrTableRow9
            // 
            this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrLiaGroupCode,
            this.tcLiabilityGrpGroupName,
            this.xrGroupTransCredit});
            this.xrTableRow9.Name = "xrTableRow9";
            resources.ApplyResources(this.xrTableRow9, "xrTableRow9");
            // 
            // xrLiaGroupCode
            // 
            this.xrLiaGroupCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BalanceSheet.GROUP_CODE")});
            this.xrLiaGroupCode.Name = "xrLiaGroupCode";
            resources.ApplyResources(this.xrLiaGroupCode, "xrLiaGroupCode");
            // 
            // tcLiabilityGrpGroupName
            // 
            resources.ApplyResources(this.tcLiabilityGrpGroupName, "tcLiabilityGrpGroupName");
            this.tcLiabilityGrpGroupName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BalanceSheet.LEDGER_GROUP")});
            this.tcLiabilityGrpGroupName.Name = "tcLiabilityGrpGroupName";
            this.tcLiabilityGrpGroupName.StylePriority.UseBorderColor = false;
            // 
            // xrGroupTransCredit
            // 
            this.xrGroupTransCredit.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrGroupTransCredit.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BalanceSheet.AMOUNT")});
            this.xrGroupTransCredit.Name = "xrGroupTransCredit";
            this.xrGroupTransCredit.StylePriority.UseBorders = false;
            this.xrGroupTransCredit.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary2, "xrSummary2");
            xrSummary2.IgnoreNullValues = true;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrGroupTransCredit.Summary = xrSummary2;
            resources.ApplyResources(this.xrGroupTransCredit, "xrGroupTransCredit");
            // 
            // grpLedgerGroup
            // 
            this.grpLedgerGroup.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblLiabilityLedgerGroup});
            this.grpLedgerGroup.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("LEDGER_GROUP", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpLedgerGroup, "grpLedgerGroup");
            this.grpLedgerGroup.Level = 1;
            this.grpLedgerGroup.Name = "grpLedgerGroup";
            xrGroupSortingSummary2.FieldName = "LEDGER_GROUP";
            xrGroupSortingSummary2.Function = DevExpress.XtraReports.UI.SortingSummaryFunction.Custom;
            this.grpLedgerGroup.SortingSummary = xrGroupSortingSummary2;
            this.grpLedgerGroup.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.grpLedgerGroup_BeforePrint);
            // 
            // grpParentGroup
            // 
            this.grpParentGroup.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblLiabilityParentGroup});
            this.grpParentGroup.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("PARENT_GROUP", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpParentGroup, "grpParentGroup");
            this.grpParentGroup.Level = 2;
            this.grpParentGroup.Name = "grpParentGroup";
            xrGroupSortingSummary3.FieldName = "PARENT_GROUP";
            xrGroupSortingSummary3.Function = DevExpress.XtraReports.UI.SortingSummaryFunction.Custom;
            this.grpParentGroup.SortingSummary = xrGroupSortingSummary3;
            // 
            // xrTblLiabilityParentGroup
            // 
            this.xrTblLiabilityParentGroup.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrTblLiabilityParentGroup, "xrTblLiabilityParentGroup");
            this.xrTblLiabilityParentGroup.Name = "xrTblLiabilityParentGroup";
            this.xrTblLiabilityParentGroup.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTblLiabilityParentGroup.StyleName = "styleGroupRow";
            this.xrTblLiabilityParentGroup.StylePriority.UseBorders = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tcLiabilityParentCode,
            this.tcLiabilityParentGroupName,
            this.xrParentGroupAmount});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // tcLiabilityParentCode
            // 
            this.tcLiabilityParentCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BalanceSheet.PARENT_CODE")});
            this.tcLiabilityParentCode.Name = "tcLiabilityParentCode";
            resources.ApplyResources(this.tcLiabilityParentCode, "tcLiabilityParentCode");
            // 
            // tcLiabilityParentGroupName
            // 
            resources.ApplyResources(this.tcLiabilityParentGroupName, "tcLiabilityParentGroupName");
            this.tcLiabilityParentGroupName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BalanceSheet.PARENT_GROUP")});
            this.tcLiabilityParentGroupName.Name = "tcLiabilityParentGroupName";
            this.tcLiabilityParentGroupName.StylePriority.UseBorderColor = false;
            this.tcLiabilityParentGroupName.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.tcLiabilityParentGroupName_EvaluateBinding);
            // 
            // xrParentGroupAmount
            // 
            this.xrParentGroupAmount.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrParentGroupAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BalanceSheet.AMOUNT")});
            this.xrParentGroupAmount.Name = "xrParentGroupAmount";
            this.xrParentGroupAmount.StylePriority.UseBorders = false;
            this.xrParentGroupAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary3, "xrSummary3");
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrParentGroupAmount.Summary = xrSummary3;
            resources.ApplyResources(this.xrParentGroupAmount, "xrParentGroupAmount");
            // 
            // BalanceSheetLiabilities
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.grpLedger,
            this.grpLedgerGroup,
            this.grpParentGroup});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.calLiaTotalAmt,
            this.calAssetAmt,
            this.calOpLiability,
            this.calOpAsset});
            this.DataMember = "BalanceSheet";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.grpParentGroup, 0);
            this.Controls.SetChildIndex(this.grpLedgerGroup, 0);
            this.Controls.SetChildIndex(this.grpLedger, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblLiabilityLedgerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblLiabilityLedgerGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblLiabilityParentGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.XRTable xrTblLiabilityLedgerName;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell tcLiabilityLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell xrLiabilityLedgerAmt;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.CalculatedField calLiaTotalAmt;
        private DevExpress.XtraReports.UI.CalculatedField calAssetAmt;
        private DevExpress.XtraReports.UI.CalculatedField calOpLiability;
        private DevExpress.XtraReports.UI.CalculatedField calOpAsset;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpLedger;
        private DevExpress.XtraReports.UI.XRTable xrTblLiabilityLedgerGroup;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow9;
        private DevExpress.XtraReports.UI.XRTableCell tcLiabilityGrpGroupName;
        private DevExpress.XtraReports.UI.XRTableCell xrGroupTransCredit;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpLedgerGroup;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpParentGroup;
        private DevExpress.XtraReports.UI.XRTable xrTblLiabilityParentGroup;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell tcLiabilityParentGroupName;
        private DevExpress.XtraReports.UI.XRTableCell xrParentGroupAmount;
        private DevExpress.XtraReports.UI.XRTableCell xrLiaLedgerCode;
        private DevExpress.XtraReports.UI.XRTableCell xrLiaGroupCode;
        private DevExpress.XtraReports.UI.XRTableCell tcLiabilityParentCode;
    }
}
