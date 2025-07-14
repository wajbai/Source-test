namespace ACPP.Modules.Transaction
{
    partial class frmTransactionJournalView
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTransactionJournalView));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            this.gvJournalLedger = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTransVoucherId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoucherLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDebit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtDebit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colVoucherTransCredit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtCredit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcJournal = new DevExpress.XtraGrid.GridControl();
            this.gvCostcentre = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCostcentreId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCostcentreName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtCCAmount = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gvJournal = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colVoucherId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVocherNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoucherType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExchangeRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLiveExchangeRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtAmount = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colNarration = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAuthorization = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVendorGSTInvoice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrintGSTInvoice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rbtnGSTInvoicePrint = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colPrint = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rbtnPrint = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colLockTrans = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rbtnTransLock = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colDuplicateVoucher = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ribDuplicateVoucher = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colFDAccountId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCredit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFDStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoucherSubType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBookingId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExpLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeducteeTypeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPartyLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoucherDefinitionId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.pnlToolBar = new DevExpress.XtraEditors.PanelControl();
            this.ucJournalToolbar = new Bosco.Utility.Controls.ucToolBar();
            this.pnlFill = new DevExpress.XtraEditors.PanelControl();
            this.pnlHeader = new DevExpress.XtraEditors.PanelControl();
            this.pnlFooter = new DevExpress.XtraEditors.PanelControl();
            this.lblRCount = new DevExpress.XtraEditors.LabelControl();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.pnlFilter = new DevExpress.XtraEditors.PanelControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.glkpProject = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deTo = new DevExpress.XtraEditors.DateEdit();
            this.deFrom = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.imgJournalView = new DevExpress.Utils.ImageCollection(this.components);
            this.colCashBankStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvJournalLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDebit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCredit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcJournal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCostcentre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtCCAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvJournal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnGSTInvoicePrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnTransLock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribDuplicateVoucher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolBar)).BeginInit();
            this.pnlToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFill)).BeginInit();
            this.pnlFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).BeginInit();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFooter)).BeginInit();
            this.pnlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFilter)).BeginInit();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgJournalView)).BeginInit();
            this.SuspendLayout();
            // 
            // gvJournalLedger
            // 
            this.gvJournalLedger.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvJournalLedger.Appearance.FocusedRow.Font")));
            this.gvJournalLedger.Appearance.FocusedRow.Options.UseFont = true;
            this.gvJournalLedger.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvJournalLedger.Appearance.HeaderPanel.Font")));
            this.gvJournalLedger.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvJournalLedger.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTransVoucherId,
            this.colVoucherLedgerName,
            this.colDebit,
            this.colVoucherTransCredit,
            this.colLedgerId});
            this.gvJournalLedger.GridControl = this.gcJournal;
            this.gvJournalLedger.Name = "gvJournalLedger";
            this.gvJournalLedger.OptionsBehavior.Editable = false;
            this.gvJournalLedger.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gvJournalLedger.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvJournalLedger.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gvJournalLedger.OptionsView.ShowGroupPanel = false;
            this.gvJournalLedger.DoubleClick += new System.EventHandler(this.gvJournalLedger_DoubleClick);
            // 
            // colTransVoucherId
            // 
            resources.ApplyResources(this.colTransVoucherId, "colTransVoucherId");
            this.colTransVoucherId.FieldName = "VOUCHER_ID";
            this.colTransVoucherId.Name = "colTransVoucherId";
            // 
            // colVoucherLedgerName
            // 
            resources.ApplyResources(this.colVoucherLedgerName, "colVoucherLedgerName");
            this.colVoucherLedgerName.FieldName = "LEDGER_NAME";
            this.colVoucherLedgerName.Name = "colVoucherLedgerName";
            // 
            // colDebit
            // 
            resources.ApplyResources(this.colDebit, "colDebit");
            this.colDebit.ColumnEdit = this.txtDebit;
            this.colDebit.DisplayFormat.FormatString = "{0:n}";
            this.colDebit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDebit.FieldName = "DEBIT";
            this.colDebit.Name = "colDebit";
            // 
            // txtDebit
            // 
            resources.ApplyResources(this.txtDebit, "txtDebit");
            this.txtDebit.DisplayFormat.FormatString = "n";
            this.txtDebit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtDebit.Mask.EditMask = resources.GetString("txtDebit.Mask.EditMask");
            this.txtDebit.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtDebit.Mask.MaskType")));
            this.txtDebit.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("txtDebit.Mask.UseMaskAsDisplayFormat")));
            this.txtDebit.Name = "txtDebit";
            // 
            // colVoucherTransCredit
            // 
            resources.ApplyResources(this.colVoucherTransCredit, "colVoucherTransCredit");
            this.colVoucherTransCredit.ColumnEdit = this.txtCredit;
            this.colVoucherTransCredit.DisplayFormat.FormatString = "{0:n}";
            this.colVoucherTransCredit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colVoucherTransCredit.FieldName = "CREDIT";
            this.colVoucherTransCredit.Name = "colVoucherTransCredit";
            // 
            // txtCredit
            // 
            resources.ApplyResources(this.txtCredit, "txtCredit");
            this.txtCredit.DisplayFormat.FormatString = "n";
            this.txtCredit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtCredit.Mask.EditMask = resources.GetString("txtCredit.Mask.EditMask");
            this.txtCredit.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtCredit.Mask.MaskType")));
            this.txtCredit.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("txtCredit.Mask.UseMaskAsDisplayFormat")));
            this.txtCredit.Name = "txtCredit";
            // 
            // colLedgerId
            // 
            resources.ApplyResources(this.colLedgerId, "colLedgerId");
            this.colLedgerId.FieldName = "LEDGER_ID";
            this.colLedgerId.Name = "colLedgerId";
            // 
            // gcJournal
            // 
            resources.ApplyResources(this.gcJournal, "gcJournal");
            gridLevelNode1.LevelTemplate = this.gvJournalLedger;
            gridLevelNode1.RelationName = "Ledger";
            gridLevelNode2.LevelTemplate = this.gvCostcentre;
            gridLevelNode2.RelationName = "CostCentre";
            this.gcJournal.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1,
            gridLevelNode2});
            this.gcJournal.MainView = this.gvJournal;
            this.gcJournal.Name = "gcJournal";
            this.gcJournal.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.txtDebit,
            this.txtCredit,
            this.rbtnPrint,
            this.rbtnTransLock,
            this.repositoryItemButtonEdit1,
            this.ribDuplicateVoucher,
            this.rbtnGSTInvoicePrint,
            this.rtxtAmount,
            this.rtxtCCAmount});
            this.gcJournal.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCostcentre,
            this.gvJournal,
            this.gvJournalLedger});
            this.gcJournal.Click += new System.EventHandler(this.gcJournal_Click);
            // 
            // gvCostcentre
            // 
            this.gvCostcentre.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvCostcentre.Appearance.FocusedRow.Font")));
            this.gvCostcentre.Appearance.FocusedRow.Options.UseFont = true;
            this.gvCostcentre.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCLedgerId,
            this.colCostcentreId,
            this.colLedgerName,
            this.colCostcentreName,
            this.colCAmount});
            this.gvCostcentre.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gvCostcentre.GridControl = this.gcJournal;
            this.gvCostcentre.Name = "gvCostcentre";
            this.gvCostcentre.OptionsBehavior.Editable = false;
            this.gvCostcentre.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvCostcentre.OptionsView.ShowGroupPanel = false;
            // 
            // colCLedgerId
            // 
            resources.ApplyResources(this.colCLedgerId, "colCLedgerId");
            this.colCLedgerId.FieldName = "LEDGER_ID";
            this.colCLedgerId.Name = "colCLedgerId";
            // 
            // colCostcentreId
            // 
            resources.ApplyResources(this.colCostcentreId, "colCostcentreId");
            this.colCostcentreId.FieldName = "COST_CENTRE_ID";
            this.colCostcentreId.Name = "colCostcentreId";
            // 
            // colLedgerName
            // 
            this.colLedgerName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colLedgerName.AppearanceHeader.Font")));
            this.colLedgerName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colLedgerName, "colLedgerName");
            this.colLedgerName.FieldName = "LEDGER_NAME";
            this.colLedgerName.Name = "colLedgerName";
            // 
            // colCostcentreName
            // 
            this.colCostcentreName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colCostcentreName.AppearanceHeader.Font")));
            this.colCostcentreName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colCostcentreName, "colCostcentreName");
            this.colCostcentreName.FieldName = "COST_CENTRE_NAME";
            this.colCostcentreName.Name = "colCostcentreName";
            // 
            // colCAmount
            // 
            this.colCAmount.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colCAmount.AppearanceHeader.Font")));
            this.colCAmount.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colCAmount, "colCAmount");
            this.colCAmount.ColumnEdit = this.rtxtCCAmount;
            this.colCAmount.DisplayFormat.FormatString = "n";
            this.colCAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCAmount.FieldName = "AMOUNT";
            this.colCAmount.Name = "colCAmount";
            // 
            // rtxtCCAmount
            // 
            resources.ApplyResources(this.rtxtCCAmount, "rtxtCCAmount");
            this.rtxtCCAmount.DisplayFormat.FormatString = "n";
            this.rtxtCCAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rtxtCCAmount.Mask.EditMask = resources.GetString("rtxtCCAmount.Mask.EditMask");
            this.rtxtCCAmount.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("rtxtCCAmount.Mask.MaskType")));
            this.rtxtCCAmount.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("rtxtCCAmount.Mask.UseMaskAsDisplayFormat")));
            this.rtxtCCAmount.Name = "rtxtCCAmount";
            // 
            // gvJournal
            // 
            this.gvJournal.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvJournal.Appearance.FocusedRow.Font")));
            this.gvJournal.Appearance.FocusedRow.Options.UseFont = true;
            this.gvJournal.Appearance.FooterPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvJournal.Appearance.FooterPanel.Font")));
            this.gvJournal.Appearance.FooterPanel.Options.UseFont = true;
            this.gvJournal.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvJournal.Appearance.HeaderPanel.Font")));
            this.gvJournal.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvJournal.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvJournal.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colVoucherId,
            this.colDate,
            this.colVocherNo,
            this.colVoucherType,
            this.colCurrencyName,
            this.colExchangeRate,
            this.colLiveExchangeRate,
            this.colAmount,
            this.colCashBankStatus,
            this.colNarration,
            this.colAuthorization,
            this.colVendorGSTInvoice,
            this.colPrintGSTInvoice,
            this.colPrint,
            this.colLockTrans,
            this.colDuplicateVoucher,
            this.colFDAccountId,
            this.colCredit,
            this.colFDStatus,
            this.colVoucherSubType,
            this.colBookingId,
            this.colExpLedgerId,
            this.colDeducteeTypeId,
            this.colPartyLedgerId,
            this.colVoucherDefinitionId});
            this.gvJournal.GridControl = this.gcJournal;
            this.gvJournal.Name = "gvJournal";
            this.gvJournal.OptionsPrint.PrintDetails = true;
            this.gvJournal.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvJournal.OptionsView.ShowFooter = true;
            this.gvJournal.OptionsView.ShowGroupPanel = false;
            this.gvJournal.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvJournal_CustomDrawCell);
            this.gvJournal.DoubleClick += new System.EventHandler(this.gvJournal_DoubleClick);
            this.gvJournal.RowCountChanged += new System.EventHandler(this.gvJournal_RowCountChanged);
            // 
            // colVoucherId
            // 
            resources.ApplyResources(this.colVoucherId, "colVoucherId");
            this.colVoucherId.FieldName = "VOUCHER_ID";
            this.colVoucherId.Name = "colVoucherId";
            this.colVoucherId.OptionsColumn.AllowEdit = false;
            // 
            // colDate
            // 
            resources.ApplyResources(this.colDate, "colDate");
            this.colDate.FieldName = "VOUCHER_DATE";
            this.colDate.Name = "colDate";
            this.colDate.OptionsColumn.AllowEdit = false;
            this.colDate.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colVocherNo
            // 
            resources.ApplyResources(this.colVocherNo, "colVocherNo");
            this.colVocherNo.FieldName = "VOUCHER_NO";
            this.colVocherNo.Name = "colVocherNo";
            this.colVocherNo.OptionsColumn.AllowEdit = false;
            this.colVocherNo.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colVoucherType
            // 
            resources.ApplyResources(this.colVoucherType, "colVoucherType");
            this.colVoucherType.FieldName = "VOUCHER_TYPE";
            this.colVoucherType.Name = "colVoucherType";
            this.colVoucherType.OptionsColumn.AllowEdit = false;
            this.colVoucherType.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colCurrencyName
            // 
            resources.ApplyResources(this.colCurrencyName, "colCurrencyName");
            this.colCurrencyName.FieldName = "CURRENCY_NAME";
            this.colCurrencyName.Name = "colCurrencyName";
            this.colCurrencyName.OptionsColumn.AllowEdit = false;
            this.colCurrencyName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colExchangeRate
            // 
            resources.ApplyResources(this.colExchangeRate, "colExchangeRate");
            this.colExchangeRate.DisplayFormat.FormatString = "n";
            this.colExchangeRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colExchangeRate.FieldName = "EXCHANGE_RATE";
            this.colExchangeRate.Name = "colExchangeRate";
            this.colExchangeRate.OptionsColumn.AllowEdit = false;
            this.colExchangeRate.OptionsColumn.FixedWidth = true;
            this.colExchangeRate.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Equals;
            // 
            // colLiveExchangeRate
            // 
            resources.ApplyResources(this.colLiveExchangeRate, "colLiveExchangeRate");
            this.colLiveExchangeRate.DisplayFormat.FormatString = "n";
            this.colLiveExchangeRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colLiveExchangeRate.FieldName = "LIVE_EXCHANGE_RATE";
            this.colLiveExchangeRate.Name = "colLiveExchangeRate";
            this.colLiveExchangeRate.OptionsColumn.AllowEdit = false;
            this.colLiveExchangeRate.OptionsColumn.FixedWidth = true;
            this.colLiveExchangeRate.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Equals;
            // 
            // colAmount
            // 
            resources.ApplyResources(this.colAmount, "colAmount");
            this.colAmount.ColumnEdit = this.rtxtAmount;
            this.colAmount.DisplayFormat.FormatString = "n";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colAmount.FieldName = "AMOUNT";
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.AllowEdit = false;
            this.colAmount.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Equals;
            this.colAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colAmount.Summary"))), resources.GetString("colAmount.Summary1"), resources.GetString("colAmount.Summary2"))});
            // 
            // rtxtAmount
            // 
            resources.ApplyResources(this.rtxtAmount, "rtxtAmount");
            this.rtxtAmount.Mask.EditMask = resources.GetString("rtxtAmount.Mask.EditMask");
            this.rtxtAmount.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("rtxtAmount.Mask.MaskType")));
            this.rtxtAmount.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("rtxtAmount.Mask.UseMaskAsDisplayFormat")));
            this.rtxtAmount.Name = "rtxtAmount";
            // 
            // colNarration
            // 
            resources.ApplyResources(this.colNarration, "colNarration");
            this.colNarration.FieldName = "NARRATION";
            this.colNarration.Name = "colNarration";
            this.colNarration.OptionsColumn.AllowEdit = false;
            this.colNarration.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colAuthorization
            // 
            resources.ApplyResources(this.colAuthorization, "colAuthorization");
            this.colAuthorization.FieldName = "AUTHORIZATION_STATUS";
            this.colAuthorization.Name = "colAuthorization";
            this.colAuthorization.OptionsColumn.AllowEdit = false;
            this.colAuthorization.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colVendorGSTInvoice
            // 
            resources.ApplyResources(this.colVendorGSTInvoice, "colVendorGSTInvoice");
            this.colVendorGSTInvoice.FieldName = "VENDOR_GST_INVOICE";
            this.colVendorGSTInvoice.Name = "colVendorGSTInvoice";
            this.colVendorGSTInvoice.OptionsColumn.AllowEdit = false;
            this.colVendorGSTInvoice.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colVendorGSTInvoice.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colVendorGSTInvoice.OptionsColumn.AllowMove = false;
            this.colVendorGSTInvoice.OptionsColumn.AllowSize = false;
            this.colVendorGSTInvoice.OptionsFilter.AllowFilter = false;
            this.colVendorGSTInvoice.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colPrintGSTInvoice
            // 
            resources.ApplyResources(this.colPrintGSTInvoice, "colPrintGSTInvoice");
            this.colPrintGSTInvoice.ColumnEdit = this.rbtnGSTInvoicePrint;
            this.colPrintGSTInvoice.Name = "colPrintGSTInvoice";
            this.colPrintGSTInvoice.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colPrintGSTInvoice.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colPrintGSTInvoice.OptionsColumn.AllowMove = false;
            this.colPrintGSTInvoice.OptionsColumn.AllowSize = false;
            this.colPrintGSTInvoice.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colPrintGSTInvoice.OptionsColumn.FixedWidth = true;
            this.colPrintGSTInvoice.OptionsColumn.ShowCaption = false;
            this.colPrintGSTInvoice.OptionsColumn.ShowInCustomizationForm = false;
            this.colPrintGSTInvoice.OptionsColumn.ShowInExpressionEditor = false;
            this.colPrintGSTInvoice.OptionsFilter.AllowAutoFilter = false;
            this.colPrintGSTInvoice.OptionsFilter.AllowFilter = false;
            this.colPrintGSTInvoice.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            // 
            // rbtnGSTInvoicePrint
            // 
            resources.ApplyResources(this.rbtnGSTInvoicePrint, "rbtnGSTInvoicePrint");
            this.rbtnGSTInvoicePrint.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rbtnGSTInvoicePrint.Buttons"))), resources.GetString("rbtnGSTInvoicePrint.Buttons1"), ((int)(resources.GetObject("rbtnGSTInvoicePrint.Buttons2"))), ((bool)(resources.GetObject("rbtnGSTInvoicePrint.Buttons3"))), ((bool)(resources.GetObject("rbtnGSTInvoicePrint.Buttons4"))), ((bool)(resources.GetObject("rbtnGSTInvoicePrint.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rbtnGSTInvoicePrint.Buttons6"))), global::ACPP.Properties.Resources.print1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("rbtnGSTInvoicePrint.Buttons7"), ((object)(resources.GetObject("rbtnGSTInvoicePrint.Buttons8"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rbtnGSTInvoicePrint.Buttons9"))), ((bool)(resources.GetObject("rbtnGSTInvoicePrint.Buttons10"))))});
            this.rbtnGSTInvoicePrint.Name = "rbtnGSTInvoicePrint";
            this.rbtnGSTInvoicePrint.Click += new System.EventHandler(this.rbtnGSTInvoicePrint_Click);
            // 
            // colPrint
            // 
            resources.ApplyResources(this.colPrint, "colPrint");
            this.colPrint.ColumnEdit = this.rbtnPrint;
            this.colPrint.FieldName = "PRINT";
            this.colPrint.Name = "colPrint";
            this.colPrint.OptionsColumn.AllowSize = false;
            this.colPrint.OptionsColumn.FixedWidth = true;
            this.colPrint.OptionsColumn.ShowCaption = false;
            this.colPrint.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colPrint.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            // 
            // rbtnPrint
            // 
            resources.ApplyResources(this.rbtnPrint, "rbtnPrint");
            this.rbtnPrint.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rbtnPrint.Buttons"))), resources.GetString("rbtnPrint.Buttons1"), ((int)(resources.GetObject("rbtnPrint.Buttons2"))), ((bool)(resources.GetObject("rbtnPrint.Buttons3"))), ((bool)(resources.GetObject("rbtnPrint.Buttons4"))), ((bool)(resources.GetObject("rbtnPrint.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rbtnPrint.Buttons6"))), global::ACPP.Properties.Resources.view, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, resources.GetString("rbtnPrint.Buttons7"), ((object)(resources.GetObject("rbtnPrint.Buttons8"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rbtnPrint.Buttons9"))), ((bool)(resources.GetObject("rbtnPrint.Buttons10"))))});
            this.rbtnPrint.Name = "rbtnPrint";
            this.rbtnPrint.Click += new System.EventHandler(this.rbtnPrint_Click);
            // 
            // colLockTrans
            // 
            resources.ApplyResources(this.colLockTrans, "colLockTrans");
            this.colLockTrans.ColumnEdit = this.rbtnTransLock;
            this.colLockTrans.Name = "colLockTrans";
            this.colLockTrans.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colLockTrans.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colLockTrans.OptionsColumn.AllowMove = false;
            this.colLockTrans.OptionsColumn.AllowSize = false;
            this.colLockTrans.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colLockTrans.OptionsColumn.FixedWidth = true;
            this.colLockTrans.OptionsColumn.ShowCaption = false;
            this.colLockTrans.OptionsFilter.AllowFilter = false;
            this.colLockTrans.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            // 
            // rbtnTransLock
            // 
            resources.ApplyResources(this.rbtnTransLock, "rbtnTransLock");
            this.rbtnTransLock.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rbtnTransLock.Buttons"))), resources.GetString("rbtnTransLock.Buttons1"), ((int)(resources.GetObject("rbtnTransLock.Buttons2"))), ((bool)(resources.GetObject("rbtnTransLock.Buttons3"))), ((bool)(resources.GetObject("rbtnTransLock.Buttons4"))), ((bool)(resources.GetObject("rbtnTransLock.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rbtnTransLock.Buttons6"))), global::ACPP.Properties.Resources.TransLock, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, resources.GetString("rbtnTransLock.Buttons7"), ((object)(resources.GetObject("rbtnTransLock.Buttons8"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rbtnTransLock.Buttons9"))), ((bool)(resources.GetObject("rbtnTransLock.Buttons10"))))});
            this.rbtnTransLock.Name = "rbtnTransLock";
            // 
            // colDuplicateVoucher
            // 
            this.colDuplicateVoucher.ColumnEdit = this.ribDuplicateVoucher;
            this.colDuplicateVoucher.FieldName = "Duplicate Voucher";
            this.colDuplicateVoucher.Name = "colDuplicateVoucher";
            this.colDuplicateVoucher.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colDuplicateVoucher.OptionsColumn.AllowMove = false;
            this.colDuplicateVoucher.OptionsColumn.AllowSize = false;
            this.colDuplicateVoucher.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDuplicateVoucher.OptionsColumn.FixedWidth = true;
            this.colDuplicateVoucher.OptionsColumn.ShowCaption = false;
            this.colDuplicateVoucher.OptionsFilter.AllowAutoFilter = false;
            this.colDuplicateVoucher.OptionsFilter.AllowFilter = false;
            this.colDuplicateVoucher.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            resources.ApplyResources(this.colDuplicateVoucher, "colDuplicateVoucher");
            // 
            // ribDuplicateVoucher
            // 
            resources.ApplyResources(this.ribDuplicateVoucher, "ribDuplicateVoucher");
            this.ribDuplicateVoucher.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("ribDuplicateVoucher.Buttons"))), resources.GetString("ribDuplicateVoucher.Buttons1"), ((int)(resources.GetObject("ribDuplicateVoucher.Buttons2"))), ((bool)(resources.GetObject("ribDuplicateVoucher.Buttons3"))), ((bool)(resources.GetObject("ribDuplicateVoucher.Buttons4"))), ((bool)(resources.GetObject("ribDuplicateVoucher.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("ribDuplicateVoucher.Buttons6"))), global::ACPP.Properties.Resources.duplicate, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject4, resources.GetString("ribDuplicateVoucher.Buttons7"), ((object)(resources.GetObject("ribDuplicateVoucher.Buttons8"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("ribDuplicateVoucher.Buttons9"))), ((bool)(resources.GetObject("ribDuplicateVoucher.Buttons10"))))});
            this.ribDuplicateVoucher.Name = "ribDuplicateVoucher";
            this.ribDuplicateVoucher.Click += new System.EventHandler(this.ribDuplicateVoucher_Click);
            // 
            // colFDAccountId
            // 
            resources.ApplyResources(this.colFDAccountId, "colFDAccountId");
            this.colFDAccountId.FieldName = "FD_ACCOUNT_ID";
            this.colFDAccountId.Name = "colFDAccountId";
            this.colFDAccountId.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colCredit
            // 
            resources.ApplyResources(this.colCredit, "colCredit");
            this.colCredit.FieldName = "CREDIT";
            this.colCredit.Name = "colCredit";
            this.colCredit.OptionsColumn.AllowEdit = false;
            this.colCredit.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colFDStatus
            // 
            resources.ApplyResources(this.colFDStatus, "colFDStatus");
            this.colFDStatus.FieldName = "FD_STATUS";
            this.colFDStatus.Name = "colFDStatus";
            this.colFDStatus.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colVoucherSubType
            // 
            resources.ApplyResources(this.colVoucherSubType, "colVoucherSubType");
            this.colVoucherSubType.FieldName = "VOUCHER_SUB_TYPE";
            this.colVoucherSubType.Name = "colVoucherSubType";
            this.colVoucherSubType.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colBookingId
            // 
            resources.ApplyResources(this.colBookingId, "colBookingId");
            this.colBookingId.FieldName = "BOOKING_ID";
            this.colBookingId.Name = "colBookingId";
            // 
            // colExpLedgerId
            // 
            resources.ApplyResources(this.colExpLedgerId, "colExpLedgerId");
            this.colExpLedgerId.FieldName = "EXPENSE_LEDGER_ID";
            this.colExpLedgerId.Name = "colExpLedgerId";
            // 
            // colDeducteeTypeId
            // 
            resources.ApplyResources(this.colDeducteeTypeId, "colDeducteeTypeId");
            this.colDeducteeTypeId.FieldName = "DEDUCTEE_TYPE_ID";
            this.colDeducteeTypeId.Name = "colDeducteeTypeId";
            // 
            // colPartyLedgerId
            // 
            resources.ApplyResources(this.colPartyLedgerId, "colPartyLedgerId");
            this.colPartyLedgerId.FieldName = "PARTY_LEDGER_ID";
            this.colPartyLedgerId.Name = "colPartyLedgerId";
            // 
            // colVoucherDefinitionId
            // 
            resources.ApplyResources(this.colVoucherDefinitionId, "colVoucherDefinitionId");
            this.colVoucherDefinitionId.FieldName = "VOUCHER_DEFINITION_ID";
            this.colVoucherDefinitionId.Name = "colVoucherDefinitionId";
            this.colVoucherDefinitionId.OptionsColumn.AllowEdit = false;
            this.colVoucherDefinitionId.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // repositoryItemButtonEdit1
            // 
            resources.ApplyResources(this.repositoryItemButtonEdit1, "repositoryItemButtonEdit1");
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // pnlToolBar
            // 
            this.pnlToolBar.Controls.Add(this.ucJournalToolbar);
            resources.ApplyResources(this.pnlToolBar, "pnlToolBar");
            this.pnlToolBar.Name = "pnlToolBar";
            // 
            // ucJournalToolbar
            // 
            this.ucJournalToolbar.ChangeAddCaption = "&Add";
            this.ucJournalToolbar.ChangeCaption = "&Edit";
            this.ucJournalToolbar.ChangeDeleteCaption = "&Delete";
            this.ucJournalToolbar.ChangeMoveVoucherCaption = "&Move Voucher";
            resources.ApplyResources(toolTipTitleItem1, "toolTipTitleItem1");
            toolTipItem1.LeftIndent = 6;
            resources.ApplyResources(toolTipItem1, "toolTipItem1");
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.ucJournalToolbar.ChangeMoveVoucherTooltip = superToolTip1;
            this.ucJournalToolbar.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucJournalToolbar.ChangePostInterestCaption = "P&ost Interest";
            toolTipTitleItem2.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            toolTipTitleItem2.Appearance.Options.UseImage = true;
            resources.ApplyResources(toolTipTitleItem2, "toolTipTitleItem2");
            toolTipItem2.LeftIndent = 6;
            resources.ApplyResources(toolTipItem2, "toolTipItem2");
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.ucJournalToolbar.ChangePostInterestSuperToolTip = superToolTip2;
            this.ucJournalToolbar.ChangePrintCaption = "&Print";
            this.ucJournalToolbar.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucJournalToolbar.DisableAddButton = true;
            this.ucJournalToolbar.DisableAMCRenew = true;
            this.ucJournalToolbar.DisableCloseButton = true;
            this.ucJournalToolbar.DisableDeleteButton = true;
            this.ucJournalToolbar.DisableDownloadExcel = true;
            this.ucJournalToolbar.DisableEditButton = true;
            this.ucJournalToolbar.DisableInsertVoucher = true;
            this.ucJournalToolbar.DisableMoveTransaction = true;
            this.ucJournalToolbar.DisableNatureofPayments = true;
            this.ucJournalToolbar.DisablePostInterest = true;
            this.ucJournalToolbar.DisablePrintButton = true;
            this.ucJournalToolbar.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucJournalToolbar, "ucJournalToolbar");
            this.ucJournalToolbar.Name = "ucJournalToolbar";
            this.ucJournalToolbar.ShowHTML = true;
            this.ucJournalToolbar.ShowMMT = true;
            this.ucJournalToolbar.ShowPDF = true;
            this.ucJournalToolbar.ShowRTF = true;
            this.ucJournalToolbar.ShowText = true;
            this.ucJournalToolbar.ShowXLS = true;
            this.ucJournalToolbar.ShowXLSX = true;
            this.ucJournalToolbar.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucJournalToolbar.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucJournalToolbar.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucJournalToolbar.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucJournalToolbar.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucJournalToolbar.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucJournalToolbar.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucJournalToolbar.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucJournalToolbar.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucJournalToolbar.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucJournalToolbar.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucJournalToolbar.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucJournalToolbar.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucJournalToolbar.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucJournalToolbar.AddClicked += new System.EventHandler(this.ucJournalToolbar_AddClicked);
            this.ucJournalToolbar.EditClicked += new System.EventHandler(this.ucJournalToolbar_EditClicked);
            this.ucJournalToolbar.DeleteClicked += new System.EventHandler(this.ucJournalToolbar_DeleteClicked);
            this.ucJournalToolbar.PrintClicked += new System.EventHandler(this.ucJournalToolbar_PrintClicked);
            this.ucJournalToolbar.CloseClicked += new System.EventHandler(this.ucJournalToolbar_CloseClicked);
            this.ucJournalToolbar.RefreshClicked += new System.EventHandler(this.ucJournalToolbar_RefreshClicked);
            this.ucJournalToolbar.MoveTransaction += new System.EventHandler(this.ucJournalToolbar_MoveTransaction);
            // 
            // pnlFill
            // 
            this.pnlFill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlFill.Controls.Add(this.pnlHeader);
            this.pnlFill.Controls.Add(this.pnlFooter);
            this.pnlFill.Controls.Add(this.pnlFilter);
            this.pnlFill.Controls.Add(this.pnlToolBar);
            resources.ApplyResources(this.pnlFill, "pnlFill");
            this.pnlFill.Name = "pnlFill";
            // 
            // pnlHeader
            // 
            this.pnlHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlHeader.Controls.Add(this.gcJournal);
            resources.ApplyResources(this.pnlHeader, "pnlHeader");
            this.pnlHeader.Name = "pnlHeader";
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.lblRCount);
            this.pnlFooter.Controls.Add(this.lblRecordCount);
            this.pnlFooter.Controls.Add(this.chkShowFilter);
            resources.ApplyResources(this.pnlFooter, "pnlFooter");
            this.pnlFooter.Name = "pnlFooter";
            // 
            // lblRCount
            // 
            resources.ApplyResources(this.lblRCount, "lblRCount");
            this.lblRCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblRCount.Appearance.Font")));
            this.lblRCount.Name = "lblRCount";
            // 
            // lblRecordCount
            // 
            resources.ApplyResources(this.lblRecordCount, "lblRecordCount");
            this.lblRecordCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblRecordCount.Appearance.Font")));
            this.lblRecordCount.Name = "lblRecordCount";
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // pnlFilter
            // 
            this.pnlFilter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlFilter.Controls.Add(this.layoutControl1);
            resources.ApplyResources(this.pnlFilter, "pnlFilter");
            this.pnlFilter.Name = "pnlFilter";
            // 
            // layoutControl1
            // 
            this.layoutControl1.AllowCustomizationMenu = false;
            this.layoutControl1.Controls.Add(this.btnApply);
            this.layoutControl1.Controls.Add(this.glkpProject);
            this.layoutControl1.Controls.Add(this.deTo);
            this.layoutControl1.Controls.Add(this.deFrom);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(657, 381, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // btnApply
            // 
            resources.ApplyResources(this.btnApply, "btnApply");
            this.btnApply.Name = "btnApply";
            this.btnApply.StyleController = this.layoutControl1;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // glkpProject
            // 
            resources.ApplyResources(this.glkpProject, "glkpProject");
            this.glkpProject.Name = "glkpProject";
            this.glkpProject.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpProject.Properties.Buttons"))))});
            this.glkpProject.Properties.ImmediatePopup = true;
            this.glkpProject.Properties.NullText = resources.GetString("glkpProject.Properties.NullText");
            this.glkpProject.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpProject.Properties.PopupFormSize = new System.Drawing.Size(380, 50);
            this.glkpProject.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkpProject.Properties.View = this.gridLookUpEdit1View;
            this.glkpProject.StyleController = this.layoutControl1;
            this.glkpProject.Tag = "PR";
            this.glkpProject.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.glkpProject_QueryPopUp);
            this.glkpProject.EditValueChanged += new System.EventHandler(this.glkpProject_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridLookUpEdit1View.Appearance.FocusedRow.Font")));
            this.gridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = true;
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProjectId,
            this.colProject});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colProjectId
            // 
            resources.ApplyResources(this.colProjectId, "colProjectId");
            this.colProjectId.FieldName = "PROJECT_ID";
            this.colProjectId.Name = "colProjectId";
            // 
            // colProject
            // 
            resources.ApplyResources(this.colProject, "colProject");
            this.colProject.FieldName = "PROJECT";
            this.colProject.Name = "colProject";
            // 
            // deTo
            // 
            resources.ApplyResources(this.deTo, "deTo");
            this.deTo.Name = "deTo";
            this.deTo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deTo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.deTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("deTo.Properties.Buttons"))))});
            this.deTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deTo.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("deTo.Properties.Mask.MaskType")));
            this.deTo.StyleController = this.layoutControl1;
            this.deTo.Leave += new System.EventHandler(this.deTo_Leave);
            // 
            // deFrom
            // 
            resources.ApplyResources(this.deFrom, "deFrom");
            this.deFrom.Name = "deFrom";
            this.deFrom.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deFrom.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.deFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("deFrom.Properties.Buttons"))))});
            this.deFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deFrom.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("deFrom.Properties.Mask.MaskType")));
            this.deFrom.StyleController = this.layoutControl1;
            this.deFrom.EditValueChanged += new System.EventHandler(this.deFrom_EditValueChanged);
            this.deFrom.Leave += new System.EventHandler(this.deFrom_Leave);
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem4,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(868, 30);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlItem1.AppearanceItemCaption.Font")));
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.Control = this.deFrom;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(482, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(167, 30);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(167, 30);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 5);
            this.layoutControlItem1.Size = new System.Drawing.Size(167, 30);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(59, 13);
            this.layoutControlItem1.TextToControlDistance = 5;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlItem4.AppearanceItemCaption.Font")));
            this.layoutControlItem4.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem4.Control = this.glkpProject;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 0, 3, 5);
            this.layoutControlItem4.Size = new System.Drawing.Size(378, 30);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(41, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlItem2.AppearanceItemCaption.Font")));
            this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem2.Control = this.deTo;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(649, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(159, 30);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(159, 30);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 3, 5);
            this.layoutControlItem2.Size = new System.Drawing.Size(159, 30);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(44, 13);
            this.layoutControlItem2.TextToControlDistance = 5;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnApply;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(808, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(60, 30);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(60, 30);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 12, 3, 5);
            this.layoutControlItem3.Size = new System.Drawing.Size(60, 30);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(378, 0);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(104, 30);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // imgJournalView
            // 
            this.imgJournalView.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgJournalView.ImageStream")));
            this.imgJournalView.Images.SetKeyName(0, "TransLock.png");
            this.imgJournalView.Images.SetKeyName(1, "TransLock1.png");
            this.imgJournalView.Images.SetKeyName(2, "unlock.png");
            this.imgJournalView.Images.SetKeyName(3, "Unlock-16.png");
            this.imgJournalView.Images.SetKeyName(4, "unlock green.png");
            // 
            // colCashBankStatus
            // 
            resources.ApplyResources(this.colCashBankStatus, "colCashBankStatus");
            this.colCashBankStatus.FieldName = "IS_CASH_BANK_STATUS";
            this.colCashBankStatus.Name = "colCashBankStatus";
            this.colCashBankStatus.OptionsColumn.AllowEdit = false;
            this.colCashBankStatus.OptionsColumn.FixedWidth = true;
            this.colCashBankStatus.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Equals;
            // 
            // frmTransactionJournalView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlFill);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmTransactionJournalView";
            this.ShowFilterClicked += new System.EventHandler(this.frmTransactionJournalView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmTransactionJournalView_EnterClicked);
            this.Load += new System.EventHandler(this.frmTransactionJournalView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvJournalLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDebit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCredit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcJournal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCostcentre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtCCAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvJournal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnGSTInvoicePrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnTransLock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribDuplicateVoucher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolBar)).EndInit();
            this.pnlToolBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlFill)).EndInit();
            this.pnlFill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlFooter)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFilter)).EndInit();
            this.pnlFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgJournalView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlToolBar;
        private DevExpress.XtraEditors.PanelControl pnlFill;
        private DevExpress.XtraEditors.PanelControl pnlFooter;
        private DevExpress.XtraEditors.PanelControl pnlFilter;
        private DevExpress.XtraEditors.PanelControl pnlHeader;
        private DevExpress.XtraGrid.GridControl gcJournal;
        private DevExpress.XtraGrid.Views.Grid.GridView gvJournal;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.DateEdit deTo;
        private DevExpress.XtraEditors.DateEdit deFrom;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit txtDebit;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit txtCredit;
        private DevExpress.XtraEditors.GridLookUpEdit glkpProject;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectId;
        private DevExpress.XtraGrid.Columns.GridColumn colProject;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SimpleButton btnApply;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Views.Grid.GridView gvJournalLedger;
        private DevExpress.XtraGrid.Columns.GridColumn colTransVoucherId;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherLedgerName;
        private DevExpress.XtraGrid.Columns.GridColumn colDebit;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherTransCredit;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherId;
        private DevExpress.XtraGrid.Columns.GridColumn colDate;
        private DevExpress.XtraGrid.Columns.GridColumn colVocherNo;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colCredit;
        private DevExpress.XtraGrid.Columns.GridColumn colNarration;
        private Bosco.Utility.Controls.ucToolBar ucJournalToolbar;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.LabelControl lblRCount;
        private DevExpress.XtraGrid.Columns.GridColumn colPrint;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnPrint;
        private DevExpress.XtraGrid.Columns.GridColumn colFDAccountId;
        private DevExpress.XtraGrid.Columns.GridColumn colFDStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherSubType;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colBookingId;
        private DevExpress.XtraGrid.Columns.GridColumn colExpLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colDeducteeTypeId;
        private DevExpress.XtraGrid.Columns.GridColumn colPartyLedgerId;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCostcentre;
        private DevExpress.XtraGrid.Columns.GridColumn colCLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colCostcentreId;
        private DevExpress.XtraGrid.Columns.GridColumn colCostcentreName;
        private DevExpress.XtraGrid.Columns.GridColumn colCAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerName;
        private DevExpress.XtraGrid.Columns.GridColumn colLockTrans;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnTransLock;
        private DevExpress.Utils.ImageCollection imgJournalView;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherType;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherDefinitionId;
        private DevExpress.XtraGrid.Columns.GridColumn colDuplicateVoucher;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit ribDuplicateVoucher;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colAuthorization;
        private DevExpress.XtraGrid.Columns.GridColumn colPrintGSTInvoice;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnGSTInvoicePrint;
        private DevExpress.XtraGrid.Columns.GridColumn colVendorGSTInvoice;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtAmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtCCAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyName;
        private DevExpress.XtraGrid.Columns.GridColumn colExchangeRate;
        private DevExpress.XtraGrid.Columns.GridColumn colLiveExchangeRate;
        private DevExpress.XtraGrid.Columns.GridColumn colCashBankStatus;
    }
}