namespace ACPP.Modules.Data_Utility
{
    partial class frmVouchersAuditLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVouchersAuditLog));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.gvVoucher = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colVoucherTransID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTransactionMode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoucherTransDebit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoucherTransCredit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerFlag = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCHEQUE_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMATERIALIZED_ON = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTransaction = new DevExpress.XtraGrid.GridControl();
            this.gvTransaction = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colVoucherID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoucherDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectDivision = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoucherNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoucherMode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoucherType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoucherDefinitionId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPurposeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContributionAmonut = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExchangeRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDebit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTopLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcashbank = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDebitAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtDebit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colCredit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtCredit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colNarration = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExchangeCountryID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChequeNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDonor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNameAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedOn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModifiedOn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModifiedBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAuditAction = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAuditTrackOn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTranLocked = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rbtnLockTrans = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colVoucherSubType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMasterLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFDVoucherId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReceiptType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPayRollClientId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rbtnPrint = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.rbtnPrintVoucher = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.rbtnDuplicateVoucher = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gvCostCentre = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCLedgerID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCostCentreId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCostcentreName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlToolBar = new DevExpress.XtraEditors.PanelControl();
            this.ucTrans = new Bosco.Utility.Controls.ucToolBar();
            this.pnlFooter = new DevExpress.XtraEditors.PanelControl();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.lblCount = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.pnlOpeningBal = new DevExpress.XtraEditors.PanelControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcTransactionGroup = new DevExpress.XtraEditors.GroupControl();
            this.chkJournal = new DevExpress.XtraEditors.CheckEdit();
            this.chkContra = new DevExpress.XtraEditors.CheckEdit();
            this.chkPayments = new DevExpress.XtraEditors.CheckEdit();
            this.chkReceipt = new DevExpress.XtraEditors.CheckEdit();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.deDateTo = new DevExpress.XtraEditors.DateEdit();
            this.deDateFrom = new DevExpress.XtraEditors.DateEdit();
            this.glkpProject = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblProject = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblDateFrom = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblDateTo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.gcLedgerDetails = new DevExpress.XtraGrid.GridControl();
            this.gvLedgerDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTrackVoucherId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTrackRevision = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTrackAudtion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTrackModifiedOn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTrackModifiedBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTrackVoucherDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTrackVoucherNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTrackProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTrackVoucherType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTrackVoucherSubType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTrackAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTrackPreviousAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTrackPreviousModifiedBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTrackByAuditor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtLedDebit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.rtxtLedCredit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgrpTrackOnAuditChanges = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.pnlFill = new DevExpress.XtraEditors.PanelControl();
            this.imgTransView = new DevExpress.Utils.ImageCollection(this.components);
            this.gcTransBalance = new DevExpress.XtraGrid.GridControl();
            this.gvTransBalance = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTransLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTransLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTransAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvVoucher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTransaction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTransaction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtDebit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtCredit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnLockTrans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnPrintVoucher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnDuplicateVoucher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCostCentre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolBar)).BeginInit();
            this.pnlToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFooter)).BeginInit();
            this.pnlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlOpeningBal)).BeginInit();
            this.pnlOpeningBal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcTransactionGroup)).BeginInit();
            this.gcTransactionGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkJournal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkContra.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPayments.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkReceipt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcLedgerDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLedgerDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtLedDebit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtLedCredit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgrpTrackOnAuditChanges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFill)).BeginInit();
            this.pnlFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgTransView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTransBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTransBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gvVoucher
            // 
            this.gvVoucher.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvVoucher.Appearance.FocusedRow.Font")));
            this.gvVoucher.Appearance.FocusedRow.Options.UseFont = true;
            this.gvVoucher.Appearance.FooterPanel.BackColor = ((System.Drawing.Color)(resources.GetObject("gvVoucher.Appearance.FooterPanel.BackColor")));
            this.gvVoucher.Appearance.FooterPanel.BorderColor = ((System.Drawing.Color)(resources.GetObject("gvVoucher.Appearance.FooterPanel.BorderColor")));
            this.gvVoucher.Appearance.FooterPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvVoucher.Appearance.FooterPanel.Font")));
            this.gvVoucher.Appearance.FooterPanel.ForeColor = ((System.Drawing.Color)(resources.GetObject("gvVoucher.Appearance.FooterPanel.ForeColor")));
            this.gvVoucher.Appearance.FooterPanel.Options.UseBackColor = true;
            this.gvVoucher.Appearance.FooterPanel.Options.UseBorderColor = true;
            this.gvVoucher.Appearance.FooterPanel.Options.UseFont = true;
            this.gvVoucher.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvVoucher.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.gvVoucher.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gvVoucher.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvVoucher.Appearance.HeaderPanel.Font")));
            this.gvVoucher.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvVoucher.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colVoucherTransID,
            this.colLedgerName,
            this.colTransactionMode,
            this.colVoucherTransDebit,
            this.colVoucherTransCredit,
            this.colAmount,
            this.colLedgerFlag,
            this.colAccountNumber,
            this.colCHEQUE_NO,
            this.colMATERIALIZED_ON,
            this.colLedgerId});
            this.gvVoucher.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gvVoucher.GridControl = this.gcTransaction;
            this.gvVoucher.Name = "gvVoucher";
            this.gvVoucher.OptionsBehavior.Editable = false;
            this.gvVoucher.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvVoucher.OptionsView.ShowFooter = true;
            this.gvVoucher.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gvVoucher.OptionsView.ShowGroupPanel = false;
            this.gvVoucher.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvVoucher_RowClick);
            this.gvVoucher.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gvVoucher_SelectionChanged);
            this.gvVoucher.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvVoucher_FocusedRowChanged);
            this.gvVoucher.DoubleClick += new System.EventHandler(this.gvVoucher_DoubleClick);
            // 
            // colVoucherTransID
            // 
            resources.ApplyResources(this.colVoucherTransID, "colVoucherTransID");
            this.colVoucherTransID.FieldName = "VOUCHER_ID";
            this.colVoucherTransID.Name = "colVoucherTransID";
            // 
            // colLedgerName
            // 
            resources.ApplyResources(this.colLedgerName, "colLedgerName");
            this.colLedgerName.FieldName = "LEDGER_NAME";
            this.colLedgerName.Name = "colLedgerName";
            this.colLedgerName.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colLedgerName.Summary"))), resources.GetString("colLedgerName.Summary1"), resources.GetString("colLedgerName.Summary2"))});
            // 
            // colTransactionMode
            // 
            resources.ApplyResources(this.colTransactionMode, "colTransactionMode");
            this.colTransactionMode.FieldName = "TRANSMODE";
            this.colTransactionMode.Name = "colTransactionMode";
            // 
            // colVoucherTransDebit
            // 
            resources.ApplyResources(this.colVoucherTransDebit, "colVoucherTransDebit");
            this.colVoucherTransDebit.DisplayFormat.FormatString = "n";
            this.colVoucherTransDebit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colVoucherTransDebit.FieldName = "DEBIT";
            this.colVoucherTransDebit.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colVoucherTransDebit.Name = "colVoucherTransDebit";
            this.colVoucherTransDebit.OptionsColumn.AllowEdit = false;
            this.colVoucherTransDebit.OptionsColumn.AllowSize = false;
            this.colVoucherTransDebit.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colVoucherTransDebit.OptionsFilter.AllowAutoFilter = false;
            this.colVoucherTransDebit.OptionsFilter.AllowFilter = false;
            // 
            // colVoucherTransCredit
            // 
            resources.ApplyResources(this.colVoucherTransCredit, "colVoucherTransCredit");
            this.colVoucherTransCredit.DisplayFormat.FormatString = "n";
            this.colVoucherTransCredit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colVoucherTransCredit.FieldName = "CREDIT";
            this.colVoucherTransCredit.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colVoucherTransCredit.Name = "colVoucherTransCredit";
            // 
            // colAmount
            // 
            resources.ApplyResources(this.colAmount, "colAmount");
            this.colAmount.DisplayFormat.FormatString = "n";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "AMOUNT";
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.FixedWidth = true;
            this.colAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colAmount.Summary"))), resources.GetString("colAmount.Summary1"), resources.GetString("colAmount.Summary2"), ((object)(resources.GetObject("colAmount.Summary3"))))});
            // 
            // colLedgerFlag
            // 
            resources.ApplyResources(this.colLedgerFlag, "colLedgerFlag");
            this.colLedgerFlag.FieldName = "LEDGER_FLAG";
            this.colLedgerFlag.Name = "colLedgerFlag";
            this.colLedgerFlag.OptionsColumn.FixedWidth = true;
            // 
            // colAccountNumber
            // 
            resources.ApplyResources(this.colAccountNumber, "colAccountNumber");
            this.colAccountNumber.FieldName = "ACCOUNT_NUMBER";
            this.colAccountNumber.Name = "colAccountNumber";
            // 
            // colCHEQUE_NO
            // 
            resources.ApplyResources(this.colCHEQUE_NO, "colCHEQUE_NO");
            this.colCHEQUE_NO.FieldName = "CHEQUE_NO";
            this.colCHEQUE_NO.Name = "colCHEQUE_NO";
            // 
            // colMATERIALIZED_ON
            // 
            resources.ApplyResources(this.colMATERIALIZED_ON, "colMATERIALIZED_ON");
            this.colMATERIALIZED_ON.FieldName = "MATERIALIZED_ON";
            this.colMATERIALIZED_ON.Name = "colMATERIALIZED_ON";
            // 
            // colLedgerId
            // 
            resources.ApplyResources(this.colLedgerId, "colLedgerId");
            this.colLedgerId.FieldName = "LEDGER_ID";
            this.colLedgerId.Name = "colLedgerId";
            // 
            // gcTransaction
            // 
            resources.ApplyResources(this.gcTransaction, "gcTransaction");
            this.gcTransaction.MainView = this.gvTransaction;
            this.gcTransaction.Name = "gcTransaction";
            this.gcTransaction.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rtxtCredit,
            this.rbtnPrint,
            this.rbtnPrintVoucher,
            this.rbtnLockTrans,
            this.rtxtDebit,
            this.rbtnDuplicateVoucher});
            this.gcTransaction.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTransaction,
            this.gvCostCentre,
            this.gvVoucher});
            this.gcTransaction.ViewRegistered += new DevExpress.XtraGrid.ViewOperationEventHandler(this.gcTransaction_ViewRegistered);
            this.gcTransaction.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.gcTransaction_ProcessGridKey);
            this.gcTransaction.DoubleClick += new System.EventHandler(this.gcTransaction_DoubleClick);
            // 
            // gvTransaction
            // 
            this.gvTransaction.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvTransaction.Appearance.FocusedRow.Font")));
            this.gvTransaction.Appearance.FocusedRow.Options.UseFont = true;
            this.gvTransaction.Appearance.FooterPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvTransaction.Appearance.FooterPanel.Font")));
            this.gvTransaction.Appearance.FooterPanel.Options.UseFont = true;
            this.gvTransaction.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvTransaction.Appearance.HeaderPanel.Font")));
            this.gvTransaction.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvTransaction.AppearancePrint.FooterPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvTransaction.AppearancePrint.FooterPanel.Font")));
            this.gvTransaction.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.gvTransaction.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gvTransaction.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colVoucherID,
            this.colVoucherDate,
            this.colProjectDivision,
            this.colVoucherNo,
            this.colVoucherMode,
            this.colVoucherType,
            this.colVoucherDefinitionId,
            this.colPurposeName,
            this.colContributionAmonut,
            this.colExchangeRate,
            this.colDebit,
            this.colTopLedgerName,
            this.colcashbank,
            this.colDebitAmount,
            this.colCredit,
            this.colNarration,
            this.colExchangeCountryID,
            this.colAccountNo,
            this.colChequeNo,
            this.colDonor,
            this.colNameAddress,
            this.colCreatedOn,
            this.colModifiedOn,
            this.colCreatedBy,
            this.colModifiedBy,
            this.colAuditAction,
            this.colAuditTrackOn,
            this.colTranLocked,
            this.colVoucherSubType,
            this.colMasterLedgerId,
            this.colFDVoucherId,
            this.colReceiptType,
            this.colPayRollClientId});
            this.gvTransaction.GridControl = this.gcTransaction;
            this.gvTransaction.Name = "gvTransaction";
            this.gvTransaction.OptionsPrint.PrintDetails = true;
            this.gvTransaction.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gvTransaction.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvTransaction.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            this.gvTransaction.OptionsView.ShowFooter = true;
            this.gvTransaction.OptionsView.ShowGroupedColumns = true;
            this.gvTransaction.OptionsView.ShowGroupPanel = false;
            this.gvTransaction.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvTransaction_CustomDrawCell);
            this.gvTransaction.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvTransaction_FocusedRowChanged);
            this.gvTransaction.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gvTransaction_CustomColumnDisplayText);
            this.gvTransaction.RowCountChanged += new System.EventHandler(this.gvTransaction_RowCountChanged);
            // 
            // colVoucherID
            // 
            resources.ApplyResources(this.colVoucherID, "colVoucherID");
            this.colVoucherID.FieldName = "VOUCHER_ID";
            this.colVoucherID.Name = "colVoucherID";
            this.colVoucherID.OptionsColumn.AllowEdit = false;
            // 
            // colVoucherDate
            // 
            this.colVoucherDate.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colVoucherDate.AppearanceHeader.Font")));
            this.colVoucherDate.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colVoucherDate, "colVoucherDate");
            this.colVoucherDate.FieldName = "VOUCHER_DATE";
            this.colVoucherDate.Name = "colVoucherDate";
            this.colVoucherDate.OptionsColumn.AllowEdit = false;
            this.colVoucherDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colVoucherDate.OptionsColumn.FixedWidth = true;
            this.colVoucherDate.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colProjectDivision
            // 
            this.colProjectDivision.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colProjectDivision.AppearanceHeader.Font")));
            this.colProjectDivision.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colProjectDivision, "colProjectDivision");
            this.colProjectDivision.FieldName = "PROJECT";
            this.colProjectDivision.Name = "colProjectDivision";
            this.colProjectDivision.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colVoucherNo
            // 
            resources.ApplyResources(this.colVoucherNo, "colVoucherNo");
            this.colVoucherNo.FieldName = "VOUCHER_NO";
            this.colVoucherNo.Name = "colVoucherNo";
            this.colVoucherNo.OptionsColumn.AllowEdit = false;
            this.colVoucherNo.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colVoucherNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colVoucherNo.OptionsColumn.FixedWidth = true;
            this.colVoucherNo.OptionsColumn.ReadOnly = true;
            this.colVoucherNo.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colVoucherMode
            // 
            resources.ApplyResources(this.colVoucherMode, "colVoucherMode");
            this.colVoucherMode.FieldName = "VOUCHERTYPE";
            this.colVoucherMode.Name = "colVoucherMode";
            this.colVoucherMode.OptionsColumn.AllowEdit = false;
            this.colVoucherMode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colVoucherType
            // 
            resources.ApplyResources(this.colVoucherType, "colVoucherType");
            this.colVoucherType.FieldName = "VOUCHER_TYPE";
            this.colVoucherType.Name = "colVoucherType";
            this.colVoucherType.OptionsColumn.AllowEdit = false;
            this.colVoucherType.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colVoucherType.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colVoucherType.OptionsColumn.FixedWidth = true;
            this.colVoucherType.OptionsColumn.ReadOnly = true;
            this.colVoucherType.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colVoucherDefinitionId
            // 
            resources.ApplyResources(this.colVoucherDefinitionId, "colVoucherDefinitionId");
            this.colVoucherDefinitionId.FieldName = "VOUCHER_DEFINITION_ID";
            this.colVoucherDefinitionId.Name = "colVoucherDefinitionId";
            this.colVoucherDefinitionId.OptionsColumn.AllowEdit = false;
            // 
            // colPurposeName
            // 
            resources.ApplyResources(this.colPurposeName, "colPurposeName");
            this.colPurposeName.FieldName = "FC_PURPOSE";
            this.colPurposeName.Name = "colPurposeName";
            this.colPurposeName.OptionsColumn.AllowEdit = false;
            this.colPurposeName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colContributionAmonut
            // 
            resources.ApplyResources(this.colContributionAmonut, "colContributionAmonut");
            this.colContributionAmonut.DisplayFormat.FormatString = "n";
            this.colContributionAmonut.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colContributionAmonut.FieldName = "ContributionAmonut";
            this.colContributionAmonut.Name = "colContributionAmonut";
            this.colContributionAmonut.OptionsColumn.AllowEdit = false;
            this.colContributionAmonut.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colExchangeRate
            // 
            resources.ApplyResources(this.colExchangeRate, "colExchangeRate");
            this.colExchangeRate.DisplayFormat.FormatString = "n";
            this.colExchangeRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colExchangeRate.FieldName = "ExchangeRate";
            this.colExchangeRate.Name = "colExchangeRate";
            this.colExchangeRate.OptionsColumn.AllowEdit = false;
            this.colExchangeRate.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colDebit
            // 
            resources.ApplyResources(this.colDebit, "colDebit");
            this.colDebit.DisplayFormat.FormatString = "n";
            this.colDebit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDebit.FieldName = "DEBIT";
            this.colDebit.Name = "colDebit";
            this.colDebit.OptionsColumn.AllowEdit = false;
            this.colDebit.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colTopLedgerName
            // 
            resources.ApplyResources(this.colTopLedgerName, "colTopLedgerName");
            this.colTopLedgerName.FieldName = "LEDGER_NAME";
            this.colTopLedgerName.Name = "colTopLedgerName";
            this.colTopLedgerName.OptionsColumn.AllowEdit = false;
            this.colTopLedgerName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colTopLedgerName.OptionsColumn.AllowMove = false;
            this.colTopLedgerName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colcashbank
            // 
            resources.ApplyResources(this.colcashbank, "colcashbank");
            this.colcashbank.FieldName = "CASHBANK";
            this.colcashbank.Name = "colcashbank";
            this.colcashbank.OptionsColumn.AllowEdit = false;
            this.colcashbank.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colcashbank.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colcashbank.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colDebitAmount
            // 
            this.colDebitAmount.AppearanceCell.Options.UseTextOptions = true;
            this.colDebitAmount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colDebitAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.colDebitAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colDebitAmount, "colDebitAmount");
            this.colDebitAmount.ColumnEdit = this.rtxtDebit;
            this.colDebitAmount.DisplayFormat.FormatString = "n";
            this.colDebitAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colDebitAmount.FieldName = "DEBIT_AMOUNT";
            this.colDebitAmount.Name = "colDebitAmount";
            this.colDebitAmount.OptionsColumn.AllowEdit = false;
            this.colDebitAmount.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colDebitAmount.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colDebitAmount.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colDebitAmount.OptionsColumn.FixedWidth = true;
            this.colDebitAmount.OptionsColumn.ReadOnly = true;
            this.colDebitAmount.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colDebitAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colDebitAmount.Summary"))), resources.GetString("colDebitAmount.Summary1"), resources.GetString("colDebitAmount.Summary2"))});
            // 
            // rtxtDebit
            // 
            resources.ApplyResources(this.rtxtDebit, "rtxtDebit");
            this.rtxtDebit.DisplayFormat.FormatString = "N";
            this.rtxtDebit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rtxtDebit.Mask.EditMask = resources.GetString("rtxtDebit.Mask.EditMask");
            this.rtxtDebit.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("rtxtDebit.Mask.MaskType")));
            this.rtxtDebit.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("rtxtDebit.Mask.UseMaskAsDisplayFormat")));
            this.rtxtDebit.Name = "rtxtDebit";
            // 
            // colCredit
            // 
            this.colCredit.AppearanceCell.Options.UseTextOptions = true;
            this.colCredit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colCredit.AppearanceHeader.Options.UseTextOptions = true;
            this.colCredit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colCredit, "colCredit");
            this.colCredit.ColumnEdit = this.rtxtCredit;
            this.colCredit.DisplayFormat.FormatString = "n";
            this.colCredit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCredit.FieldName = "CREDIT_AMOUNT";
            this.colCredit.Name = "colCredit";
            this.colCredit.OptionsColumn.AllowEdit = false;
            this.colCredit.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colCredit.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colCredit.OptionsColumn.FixedWidth = true;
            this.colCredit.OptionsColumn.ReadOnly = true;
            this.colCredit.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colCredit.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colCredit.Summary"))), resources.GetString("colCredit.Summary1"), resources.GetString("colCredit.Summary2"))});
            // 
            // rtxtCredit
            // 
            resources.ApplyResources(this.rtxtCredit, "rtxtCredit");
            this.rtxtCredit.DisplayFormat.FormatString = "n";
            this.rtxtCredit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rtxtCredit.Mask.EditMask = resources.GetString("rtxtCredit.Mask.EditMask");
            this.rtxtCredit.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("rtxtCredit.Mask.MaskType")));
            this.rtxtCredit.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("rtxtCredit.Mask.UseMaskAsDisplayFormat")));
            this.rtxtCredit.Name = "rtxtCredit";
            // 
            // colNarration
            // 
            resources.ApplyResources(this.colNarration, "colNarration");
            this.colNarration.FieldName = "NARRATION";
            this.colNarration.Name = "colNarration";
            this.colNarration.OptionsColumn.AllowEdit = false;
            this.colNarration.OptionsColumn.AllowFocus = false;
            this.colNarration.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colNarration.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colNarration.OptionsColumn.FixedWidth = true;
            this.colNarration.OptionsColumn.ReadOnly = true;
            this.colNarration.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colExchangeCountryID
            // 
            resources.ApplyResources(this.colExchangeCountryID, "colExchangeCountryID");
            this.colExchangeCountryID.FieldName = "ExchangeCountryID";
            this.colExchangeCountryID.Name = "colExchangeCountryID";
            this.colExchangeCountryID.OptionsColumn.AllowEdit = false;
            this.colExchangeCountryID.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colAccountNo
            // 
            resources.ApplyResources(this.colAccountNo, "colAccountNo");
            this.colAccountNo.FieldName = "Account No";
            this.colAccountNo.Name = "colAccountNo";
            this.colAccountNo.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colChequeNo
            // 
            resources.ApplyResources(this.colChequeNo, "colChequeNo");
            this.colChequeNo.FieldName = "CHEQUE_NO";
            this.colChequeNo.Name = "colChequeNo";
            this.colChequeNo.OptionsColumn.AllowEdit = false;
            this.colChequeNo.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colDonor
            // 
            resources.ApplyResources(this.colDonor, "colDonor");
            this.colDonor.FieldName = "DONOR_NAME";
            this.colDonor.Name = "colDonor";
            this.colDonor.OptionsColumn.AllowEdit = false;
            this.colDonor.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colNameAddress
            // 
            resources.ApplyResources(this.colNameAddress, "colNameAddress");
            this.colNameAddress.FieldName = "NAME_ADDRESS";
            this.colNameAddress.Name = "colNameAddress";
            this.colNameAddress.OptionsColumn.AllowEdit = false;
            this.colNameAddress.OptionsColumn.ReadOnly = true;
            this.colNameAddress.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colCreatedOn
            // 
            resources.ApplyResources(this.colCreatedOn, "colCreatedOn");
            this.colCreatedOn.FieldName = "CREATED_ON";
            this.colCreatedOn.Name = "colCreatedOn";
            this.colCreatedOn.OptionsColumn.AllowEdit = false;
            this.colCreatedOn.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colCreatedOn.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colCreatedOn.OptionsColumn.AllowMove = false;
            this.colCreatedOn.OptionsColumn.FixedWidth = true;
            this.colCreatedOn.OptionsColumn.ReadOnly = true;
            this.colCreatedOn.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colModifiedOn
            // 
            resources.ApplyResources(this.colModifiedOn, "colModifiedOn");
            this.colModifiedOn.FieldName = "MODIFIED_ON";
            this.colModifiedOn.Name = "colModifiedOn";
            this.colModifiedOn.OptionsColumn.AllowEdit = false;
            this.colModifiedOn.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colModifiedOn.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colModifiedOn.OptionsColumn.AllowMove = false;
            this.colModifiedOn.OptionsColumn.FixedWidth = true;
            this.colModifiedOn.OptionsColumn.ReadOnly = true;
            // 
            // colCreatedBy
            // 
            resources.ApplyResources(this.colCreatedBy, "colCreatedBy");
            this.colCreatedBy.FieldName = "CREATED_BY_NAME";
            this.colCreatedBy.Name = "colCreatedBy";
            this.colCreatedBy.OptionsColumn.AllowEdit = false;
            this.colCreatedBy.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colCreatedBy.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colCreatedBy.OptionsColumn.AllowMove = false;
            this.colCreatedBy.OptionsColumn.FixedWidth = true;
            this.colCreatedBy.OptionsColumn.ReadOnly = true;
            this.colCreatedBy.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colModifiedBy
            // 
            resources.ApplyResources(this.colModifiedBy, "colModifiedBy");
            this.colModifiedBy.FieldName = "MODIFIED_BY_NAME";
            this.colModifiedBy.Name = "colModifiedBy";
            this.colModifiedBy.OptionsColumn.AllowEdit = false;
            this.colModifiedBy.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colModifiedBy.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colModifiedBy.OptionsColumn.AllowMove = false;
            this.colModifiedBy.OptionsColumn.FixedWidth = true;
            this.colModifiedBy.OptionsColumn.ReadOnly = true;
            this.colModifiedBy.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colAuditAction
            // 
            resources.ApplyResources(this.colAuditAction, "colAuditAction");
            this.colAuditAction.FieldName = "AUDIT_ACTION";
            this.colAuditAction.Name = "colAuditAction";
            this.colAuditAction.OptionsColumn.AllowEdit = false;
            this.colAuditAction.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colAuditAction.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colAuditAction.OptionsColumn.AllowMove = false;
            this.colAuditAction.OptionsColumn.FixedWidth = true;
            this.colAuditAction.OptionsColumn.ReadOnly = true;
            this.colAuditAction.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colAuditTrackOn
            // 
            resources.ApplyResources(this.colAuditTrackOn, "colAuditTrackOn");
            this.colAuditTrackOn.FieldName = "AUDITOR_TRACK";
            this.colAuditTrackOn.Name = "colAuditTrackOn";
            this.colAuditTrackOn.OptionsColumn.AllowEdit = false;
            this.colAuditTrackOn.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colAuditTrackOn.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colAuditTrackOn.OptionsColumn.AllowMove = false;
            this.colAuditTrackOn.OptionsColumn.FixedWidth = true;
            this.colAuditTrackOn.OptionsColumn.ReadOnly = true;
            this.colAuditTrackOn.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colTranLocked
            // 
            resources.ApplyResources(this.colTranLocked, "colTranLocked");
            this.colTranLocked.ColumnEdit = this.rbtnLockTrans;
            this.colTranLocked.FieldName = "TRAS_LOCKED";
            this.colTranLocked.Name = "colTranLocked";
            this.colTranLocked.OptionsColumn.AllowEdit = false;
            this.colTranLocked.OptionsColumn.AllowFocus = false;
            this.colTranLocked.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colTranLocked.OptionsColumn.AllowMove = false;
            this.colTranLocked.OptionsColumn.AllowSize = false;
            this.colTranLocked.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colTranLocked.OptionsColumn.FixedWidth = true;
            this.colTranLocked.OptionsColumn.ShowCaption = false;
            this.colTranLocked.OptionsFilter.AllowAutoFilter = false;
            this.colTranLocked.OptionsFilter.AllowFilter = false;
            this.colTranLocked.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            // 
            // rbtnLockTrans
            // 
            resources.ApplyResources(this.rbtnLockTrans, "rbtnLockTrans");
            this.rbtnLockTrans.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rbtnLockTrans.Buttons"))))});
            this.rbtnLockTrans.Name = "rbtnLockTrans";
            this.rbtnLockTrans.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.rbtnLockTrans_ButtonClick);
            // 
            // colVoucherSubType
            // 
            resources.ApplyResources(this.colVoucherSubType, "colVoucherSubType");
            this.colVoucherSubType.FieldName = "VOUCHER_SUB_TYPE";
            this.colVoucherSubType.Name = "colVoucherSubType";
            this.colVoucherSubType.OptionsColumn.AllowEdit = false;
            this.colVoucherSubType.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colMasterLedgerId
            // 
            resources.ApplyResources(this.colMasterLedgerId, "colMasterLedgerId");
            this.colMasterLedgerId.FieldName = "LEDGER_ID";
            this.colMasterLedgerId.Name = "colMasterLedgerId";
            this.colMasterLedgerId.OptionsColumn.AllowEdit = false;
            this.colMasterLedgerId.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colFDVoucherId
            // 
            resources.ApplyResources(this.colFDVoucherId, "colFDVoucherId");
            this.colFDVoucherId.FieldName = "FD_ACCOUNT_ID";
            this.colFDVoucherId.Name = "colFDVoucherId";
            this.colFDVoucherId.OptionsColumn.AllowEdit = false;
            this.colFDVoucherId.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colReceiptType
            // 
            resources.ApplyResources(this.colReceiptType, "colReceiptType");
            this.colReceiptType.FieldName = "RECEIPT_TYPE";
            this.colReceiptType.Name = "colReceiptType";
            this.colReceiptType.OptionsColumn.AllowEdit = false;
            // 
            // colPayRollClientId
            // 
            resources.ApplyResources(this.colPayRollClientId, "colPayRollClientId");
            this.colPayRollClientId.FieldName = "CLIENT_REFERENCE_ID";
            this.colPayRollClientId.Name = "colPayRollClientId";
            // 
            // rbtnPrint
            // 
            resources.ApplyResources(this.rbtnPrint, "rbtnPrint");
            this.rbtnPrint.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rbtnPrint.Buttons"))), resources.GetString("rbtnPrint.Buttons1"), ((int)(resources.GetObject("rbtnPrint.Buttons2"))), ((bool)(resources.GetObject("rbtnPrint.Buttons3"))), ((bool)(resources.GetObject("rbtnPrint.Buttons4"))), ((bool)(resources.GetObject("rbtnPrint.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rbtnPrint.Buttons6"))), global::ACPP.Properties.Resources.bank1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("rbtnPrint.Buttons7"), ((object)(resources.GetObject("rbtnPrint.Buttons8"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rbtnPrint.Buttons9"))), ((bool)(resources.GetObject("rbtnPrint.Buttons10"))))});
            this.rbtnPrint.Name = "rbtnPrint";
            // 
            // rbtnPrintVoucher
            // 
            resources.ApplyResources(this.rbtnPrintVoucher, "rbtnPrintVoucher");
            this.rbtnPrintVoucher.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rbtnPrintVoucher.Buttons"))), resources.GetString("rbtnPrintVoucher.Buttons1"), ((int)(resources.GetObject("rbtnPrintVoucher.Buttons2"))), ((bool)(resources.GetObject("rbtnPrintVoucher.Buttons3"))), ((bool)(resources.GetObject("rbtnPrintVoucher.Buttons4"))), ((bool)(resources.GetObject("rbtnPrintVoucher.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rbtnPrintVoucher.Buttons6"))), global::ACPP.Properties.Resources.view, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, resources.GetString("rbtnPrintVoucher.Buttons7"), ((object)(resources.GetObject("rbtnPrintVoucher.Buttons8"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rbtnPrintVoucher.Buttons9"))), ((bool)(resources.GetObject("rbtnPrintVoucher.Buttons10"))))});
            this.rbtnPrintVoucher.Name = "rbtnPrintVoucher";
            this.rbtnPrintVoucher.Click += new System.EventHandler(this.rbtnPrintVoucher_Click);
            // 
            // rbtnDuplicateVoucher
            // 
            resources.ApplyResources(this.rbtnDuplicateVoucher, "rbtnDuplicateVoucher");
            this.rbtnDuplicateVoucher.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rbtnDuplicateVoucher.Buttons"))), resources.GetString("rbtnDuplicateVoucher.Buttons1"), ((int)(resources.GetObject("rbtnDuplicateVoucher.Buttons2"))), ((bool)(resources.GetObject("rbtnDuplicateVoucher.Buttons3"))), ((bool)(resources.GetObject("rbtnDuplicateVoucher.Buttons4"))), ((bool)(resources.GetObject("rbtnDuplicateVoucher.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rbtnDuplicateVoucher.Buttons6"))), global::ACPP.Properties.Resources.duplicate, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, resources.GetString("rbtnDuplicateVoucher.Buttons7"), ((object)(resources.GetObject("rbtnDuplicateVoucher.Buttons8"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rbtnDuplicateVoucher.Buttons9"))), ((bool)(resources.GetObject("rbtnDuplicateVoucher.Buttons10"))))});
            this.rbtnDuplicateVoucher.Name = "rbtnDuplicateVoucher";
            this.rbtnDuplicateVoucher.Click += new System.EventHandler(this.rbtnDuplicateVoucher_Click);
            // 
            // gvCostCentre
            // 
            this.gvCostCentre.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvCostCentre.Appearance.FocusedRow.Font")));
            this.gvCostCentre.Appearance.FocusedRow.Options.UseFont = true;
            this.gvCostCentre.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvCostCentre.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCLedgerID,
            this.colCostCentreId,
            this.colCLedgerName,
            this.colCostcentreName,
            this.colCAmount});
            this.gvCostCentre.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gvCostCentre.GridControl = this.gcTransaction;
            this.gvCostCentre.Name = "gvCostCentre";
            this.gvCostCentre.OptionsBehavior.Editable = false;
            this.gvCostCentre.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvCostCentre.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gvCostCentre.OptionsView.ShowGroupPanel = false;
            // 
            // colCLedgerID
            // 
            resources.ApplyResources(this.colCLedgerID, "colCLedgerID");
            this.colCLedgerID.FieldName = "LEDGER_ID";
            this.colCLedgerID.Name = "colCLedgerID";
            // 
            // colCostCentreId
            // 
            resources.ApplyResources(this.colCostCentreId, "colCostCentreId");
            this.colCostCentreId.FieldName = "COST_CENTRE_ID";
            this.colCostCentreId.Name = "colCostCentreId";
            // 
            // colCLedgerName
            // 
            this.colCLedgerName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colCLedgerName.AppearanceHeader.Font")));
            this.colCLedgerName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colCLedgerName, "colCLedgerName");
            this.colCLedgerName.FieldName = "LEDGER_NAME";
            this.colCLedgerName.Name = "colCLedgerName";
            // 
            // colCostcentreName
            // 
            this.colCostcentreName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colCostcentreName.AppearanceHeader.Font")));
            this.colCostcentreName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colCostcentreName, "colCostcentreName");
            this.colCostcentreName.FieldName = "COST_CENTRE_NAME";
            this.colCostcentreName.Name = "colCostcentreName";
            this.colCostcentreName.OptionsColumn.AllowEdit = false;
            this.colCostcentreName.OptionsColumn.ReadOnly = true;
            // 
            // colCAmount
            // 
            this.colCAmount.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colCAmount.AppearanceHeader.Font")));
            this.colCAmount.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colCAmount, "colCAmount");
            this.colCAmount.DisplayFormat.FormatString = "n";
            this.colCAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCAmount.FieldName = "AMOUNT";
            this.colCAmount.Name = "colCAmount";
            this.colCAmount.OptionsColumn.AllowEdit = false;
            this.colCAmount.OptionsColumn.ReadOnly = true;
            // 
            // pnlToolBar
            // 
            this.pnlToolBar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlToolBar.Controls.Add(this.ucTrans);
            resources.ApplyResources(this.pnlToolBar, "pnlToolBar");
            this.pnlToolBar.Name = "pnlToolBar";
            // 
            // ucTrans
            // 
            this.ucTrans.ChangeAddCaption = "&Add";
            this.ucTrans.ChangeCaption = "&Filter";
            this.ucTrans.ChangeDeleteCaption = "&Delete";
            this.ucTrans.ChangeMoveVoucherCaption = "&Move Voucher";
            resources.ApplyResources(toolTipTitleItem1, "toolTipTitleItem1");
            toolTipItem1.LeftIndent = 6;
            resources.ApplyResources(toolTipItem1, "toolTipItem1");
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.ucTrans.ChangeMoveVoucherTooltip = superToolTip1;
            this.ucTrans.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucTrans.ChangePostInterestCaption = "P&ost Interest";
            this.ucTrans.ChangePrintCaption = "&Print";
            this.ucTrans.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucTrans.DisableAddButton = true;
            this.ucTrans.DisableAMCRenew = true;
            this.ucTrans.DisableCloseButton = true;
            this.ucTrans.DisableDeleteButton = true;
            this.ucTrans.DisableDownloadExcel = true;
            this.ucTrans.DisableEditButton = true;
            this.ucTrans.DisableMoveTransaction = true;
            this.ucTrans.DisableNatureofPayments = true;
            this.ucTrans.DisablePostInterest = true;
            this.ucTrans.DisablePrintButton = true;
            this.ucTrans.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucTrans, "ucTrans");
            this.ucTrans.Name = "ucTrans";
            this.ucTrans.ShowHTML = true;
            this.ucTrans.ShowMMT = true;
            this.ucTrans.ShowPDF = true;
            this.ucTrans.ShowRTF = true;
            this.ucTrans.ShowText = true;
            this.ucTrans.ShowXLS = true;
            this.ucTrans.ShowXLSX = true;
            this.ucTrans.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucTrans.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucTrans.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucTrans.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucTrans.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucTrans.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucTrans.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucTrans.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucTrans.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucTrans.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucTrans.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucTrans.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucTrans.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucTrans.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucTrans.AddClicked += new System.EventHandler(this.ucTrans_AddClicked);
            this.ucTrans.EditClicked += new System.EventHandler(this.ucTrans_EditClicked);
            this.ucTrans.DeleteClicked += new System.EventHandler(this.ucTrans_DeleteClicked);
            this.ucTrans.PrintClicked += new System.EventHandler(this.ucTrans_PrintClicked);
            this.ucTrans.CloseClicked += new System.EventHandler(this.ucTrans_CloseClicked);
            this.ucTrans.RefreshClicked += new System.EventHandler(this.ucTrans_RefreshClicked);
            this.ucTrans.MoveTransaction += new System.EventHandler(this.ucTrans_MoveTransaction);
            this.ucTrans.InsertVoucher += new System.EventHandler(this.ucTrans_InsertVoucher);
            this.ucTrans.DownloadExcel += new System.EventHandler(this.ucTrans_DownloadExcel);
            this.ucTrans.NegativeBalanceClicked += new System.EventHandler(this.ucTrans_NegativeBalanceClicked);
            // 
            // pnlFooter
            // 
            this.pnlFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlFooter.Controls.Add(this.lblRecordCount);
            this.pnlFooter.Controls.Add(this.lblCount);
            this.pnlFooter.Controls.Add(this.chkShowFilter);
            resources.ApplyResources(this.pnlFooter, "pnlFooter");
            this.pnlFooter.Name = "pnlFooter";
            // 
            // lblRecordCount
            // 
            resources.ApplyResources(this.lblRecordCount, "lblRecordCount");
            this.lblRecordCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblRecordCount.Appearance.Font")));
            this.lblRecordCount.Name = "lblRecordCount";
            // 
            // lblCount
            // 
            resources.ApplyResources(this.lblCount, "lblCount");
            this.lblCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblCount.Appearance.Font")));
            this.lblCount.Name = "lblCount";
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // pnlOpeningBal
            // 
            this.pnlOpeningBal.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlOpeningBal.Controls.Add(this.layoutControl1);
            resources.ApplyResources(this.pnlOpeningBal, "pnlOpeningBal");
            this.pnlOpeningBal.Name = "pnlOpeningBal";
            // 
            // layoutControl1
            // 
            this.layoutControl1.AllowCustomizationMenu = false;
            this.layoutControl1.Controls.Add(this.gcTransactionGroup);
            this.layoutControl1.Controls.Add(this.btnApply);
            this.layoutControl1.Controls.Add(this.deDateTo);
            this.layoutControl1.Controls.Add(this.deDateFrom);
            this.layoutControl1.Controls.Add(this.glkpProject);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(147, 246, 250, 350);
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // gcTransactionGroup
            // 
            this.gcTransactionGroup.Controls.Add(this.chkJournal);
            this.gcTransactionGroup.Controls.Add(this.chkContra);
            this.gcTransactionGroup.Controls.Add(this.chkPayments);
            this.gcTransactionGroup.Controls.Add(this.chkReceipt);
            resources.ApplyResources(this.gcTransactionGroup, "gcTransactionGroup");
            this.gcTransactionGroup.Name = "gcTransactionGroup";
            this.gcTransactionGroup.ShowCaption = false;
            // 
            // chkJournal
            // 
            resources.ApplyResources(this.chkJournal, "chkJournal");
            this.chkJournal.EnterMoveNextControl = true;
            this.chkJournal.Name = "chkJournal";
            this.chkJournal.Properties.Caption = resources.GetString("chkJournal.Properties.Caption");
            this.chkJournal.CheckedChanged += new System.EventHandler(this.chkJournal_CheckedChanged);
            // 
            // chkContra
            // 
            resources.ApplyResources(this.chkContra, "chkContra");
            this.chkContra.EnterMoveNextControl = true;
            this.chkContra.Name = "chkContra";
            this.chkContra.Properties.Caption = resources.GetString("chkContra.Properties.Caption");
            this.chkContra.CheckedChanged += new System.EventHandler(this.chkContra_CheckedChanged);
            // 
            // chkPayments
            // 
            resources.ApplyResources(this.chkPayments, "chkPayments");
            this.chkPayments.EnterMoveNextControl = true;
            this.chkPayments.Name = "chkPayments";
            this.chkPayments.Properties.Caption = resources.GetString("chkPayments.Properties.Caption");
            this.chkPayments.CheckedChanged += new System.EventHandler(this.chkPayments_CheckedChanged);
            // 
            // chkReceipt
            // 
            resources.ApplyResources(this.chkReceipt, "chkReceipt");
            this.chkReceipt.EnterMoveNextControl = true;
            this.chkReceipt.Name = "chkReceipt";
            this.chkReceipt.Properties.Caption = resources.GetString("chkReceipt.Properties.Caption");
            this.chkReceipt.CheckedChanged += new System.EventHandler(this.chkReceipt_CheckedChanged);
            // 
            // btnApply
            // 
            resources.ApplyResources(this.btnApply, "btnApply");
            this.btnApply.Name = "btnApply";
            this.btnApply.StyleController = this.layoutControl1;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // deDateTo
            // 
            resources.ApplyResources(this.deDateTo, "deDateTo");
            this.deDateTo.EnterMoveNextControl = true;
            this.deDateTo.Name = "deDateTo";
            this.deDateTo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deDateTo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.deDateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("deDateTo.Properties.Buttons"))))});
            this.deDateTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deDateTo.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("deDateTo.Properties.Mask.MaskType")));
            this.deDateTo.StyleController = this.layoutControl1;
            this.deDateTo.Leave += new System.EventHandler(this.deDateTo_Leave);
            // 
            // deDateFrom
            // 
            resources.ApplyResources(this.deDateFrom, "deDateFrom");
            this.deDateFrom.EnterMoveNextControl = true;
            this.deDateFrom.Name = "deDateFrom";
            this.deDateFrom.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deDateFrom.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.deDateFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("deDateFrom.Properties.Buttons"))))});
            this.deDateFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deDateFrom.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("deDateFrom.Properties.Mask.MaskType")));
            this.deDateFrom.StyleController = this.layoutControl1;
            this.deDateFrom.EditValueChanged += new System.EventHandler(this.deDateFrom_EditValueChanged);
            this.deDateFrom.Leave += new System.EventHandler(this.deDateFrom_Leave);
            // 
            // glkpProject
            // 
            resources.ApplyResources(this.glkpProject, "glkpProject");
            this.glkpProject.EnterMoveNextControl = true;
            this.glkpProject.Name = "glkpProject";
            this.glkpProject.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpProject.Properties.Buttons"))))});
            this.glkpProject.Properties.ImmediatePopup = true;
            this.glkpProject.Properties.NullText = resources.GetString("glkpProject.Properties.NullText");
            this.glkpProject.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpProject.Properties.PopupFormSize = new System.Drawing.Size(302, 80);
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
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblProject,
            this.lblDateFrom,
            this.lblDateTo,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1219, 30);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lblProject
            // 
            this.lblProject.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblProject.AppearanceItemCaption.Font")));
            this.lblProject.AppearanceItemCaption.Options.UseFont = true;
            this.lblProject.Control = this.glkpProject;
            this.lblProject.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.lblProject, "lblProject");
            this.lblProject.Location = new System.Drawing.Point(0, 0);
            this.lblProject.Name = "lblProject";
            this.lblProject.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 3, 5);
            this.lblProject.Size = new System.Drawing.Size(556, 30);
            this.lblProject.TextSize = new System.Drawing.Size(41, 13);
            this.lblProject.TrimClientAreaToControl = false;
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblDateFrom.AppearanceItemCaption.Font")));
            this.lblDateFrom.AppearanceItemCaption.Options.UseFont = true;
            this.lblDateFrom.Control = this.deDateFrom;
            this.lblDateFrom.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.lblDateFrom, "lblDateFrom");
            this.lblDateFrom.Location = new System.Drawing.Point(566, 0);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 5);
            this.lblDateFrom.Size = new System.Drawing.Size(120, 30);
            this.lblDateFrom.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblDateFrom.TextSize = new System.Drawing.Size(29, 13);
            this.lblDateFrom.TextToControlDistance = 5;
            this.lblDateFrom.TrimClientAreaToControl = false;
            // 
            // lblDateTo
            // 
            this.lblDateTo.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblDateTo.AppearanceItemCaption.Font")));
            this.lblDateTo.AppearanceItemCaption.Options.UseFont = true;
            this.lblDateTo.Control = this.deDateTo;
            this.lblDateTo.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.lblDateTo, "lblDateTo");
            this.lblDateTo.Location = new System.Drawing.Point(686, 0);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 0, 3, 5);
            this.lblDateTo.Size = new System.Drawing.Size(120, 30);
            this.lblDateTo.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblDateTo.TextSize = new System.Drawing.Size(14, 13);
            this.lblDateTo.TextToControlDistance = 5;
            this.lblDateTo.TrimClientAreaToControl = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnApply;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(1146, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(73, 30);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            this.layoutControlItem2.TrimClientAreaToControl = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gcTransactionGroup;
            this.layoutControlItem4.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(806, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(340, 30);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            this.layoutControlItem4.TrimClientAreaToControl = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(556, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(10, 30);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.layoutControl2);
            resources.ApplyResources(this.panelControl1, "panelControl1");
            this.panelControl1.Name = "panelControl1";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.gcLedgerDetails);
            this.layoutControl2.Controls.Add(this.gcTransaction);
            resources.ApplyResources(this.layoutControl2, "layoutControl2");
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(147, 71, 250, 350);
            this.layoutControl2.Root = this.layoutControlGroup2;
            // 
            // gcLedgerDetails
            // 
            resources.ApplyResources(this.gcLedgerDetails, "gcLedgerDetails");
            this.gcLedgerDetails.MainView = this.gvLedgerDetails;
            this.gcLedgerDetails.Name = "gcLedgerDetails";
            this.gcLedgerDetails.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rtxtLedDebit,
            this.rtxtLedCredit});
            this.gcLedgerDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLedgerDetails});
            // 
            // gvLedgerDetails
            // 
            this.gvLedgerDetails.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvLedgerDetails.Appearance.FocusedRow.Font")));
            this.gvLedgerDetails.Appearance.FocusedRow.Options.UseFont = true;
            this.gvLedgerDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTrackVoucherId,
            this.colTrackRevision,
            this.colTrackAudtion,
            this.colTrackModifiedOn,
            this.colTrackModifiedBy,
            this.colTrackVoucherDate,
            this.colTrackVoucherNo,
            this.colTrackProject,
            this.colTrackVoucherType,
            this.colTrackVoucherSubType,
            this.colTrackAmount,
            this.colTrackPreviousAmount,
            this.colTrackPreviousModifiedBy,
            this.colTrackByAuditor});
            this.gvLedgerDetails.GridControl = this.gcLedgerDetails;
            this.gvLedgerDetails.Name = "gvLedgerDetails";
            this.gvLedgerDetails.OptionsView.ShowGroupPanel = false;
            this.gvLedgerDetails.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvLedgerDetails_FocusedRowChanged);
            this.gvLedgerDetails.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gvLedgerDetails_CustomColumnDisplayText);
            // 
            // colTrackVoucherId
            // 
            this.colTrackVoucherId.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colTrackVoucherId.AppearanceHeader.Font")));
            this.colTrackVoucherId.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colTrackVoucherId, "colTrackVoucherId");
            this.colTrackVoucherId.FieldName = "TRACK_VOUCHER_ID";
            this.colTrackVoucherId.Name = "colTrackVoucherId";
            this.colTrackVoucherId.OptionsColumn.AllowEdit = false;
            this.colTrackVoucherId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colTrackVoucherId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colTrackVoucherId.OptionsColumn.ReadOnly = true;
            this.colTrackVoucherId.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colTrackRevision
            // 
            this.colTrackRevision.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colTrackRevision.AppearanceHeader.Font")));
            this.colTrackRevision.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colTrackRevision, "colTrackRevision");
            this.colTrackRevision.FieldName = "NUMBER_OF_REVISIONS";
            this.colTrackRevision.Name = "colTrackRevision";
            this.colTrackRevision.OptionsColumn.AllowEdit = false;
            this.colTrackRevision.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colTrackRevision.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colTrackRevision.OptionsColumn.FixedWidth = true;
            this.colTrackRevision.OptionsColumn.ReadOnly = true;
            // 
            // colTrackAudtion
            // 
            this.colTrackAudtion.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colTrackAudtion.AppearanceHeader.Font")));
            this.colTrackAudtion.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colTrackAudtion, "colTrackAudtion");
            this.colTrackAudtion.FieldName = "AUDIT_ACTION";
            this.colTrackAudtion.Name = "colTrackAudtion";
            this.colTrackAudtion.OptionsColumn.AllowEdit = false;
            this.colTrackAudtion.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colTrackAudtion.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colTrackAudtion.OptionsColumn.FixedWidth = true;
            this.colTrackAudtion.OptionsColumn.ReadOnly = true;
            this.colTrackAudtion.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colTrackModifiedOn
            // 
            this.colTrackModifiedOn.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colTrackModifiedOn.AppearanceHeader.Font")));
            this.colTrackModifiedOn.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colTrackModifiedOn, "colTrackModifiedOn");
            this.colTrackModifiedOn.FieldName = "MODIFIED_ON";
            this.colTrackModifiedOn.Name = "colTrackModifiedOn";
            this.colTrackModifiedOn.OptionsColumn.AllowEdit = false;
            this.colTrackModifiedOn.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colTrackModifiedOn.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colTrackModifiedOn.OptionsColumn.FixedWidth = true;
            this.colTrackModifiedOn.OptionsColumn.ReadOnly = true;
            this.colTrackModifiedOn.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colTrackModifiedBy
            // 
            this.colTrackModifiedBy.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colTrackModifiedBy.AppearanceHeader.Font")));
            this.colTrackModifiedBy.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colTrackModifiedBy, "colTrackModifiedBy");
            this.colTrackModifiedBy.FieldName = "MODIFIED_BY_NAME";
            this.colTrackModifiedBy.Name = "colTrackModifiedBy";
            this.colTrackModifiedBy.OptionsColumn.AllowEdit = false;
            this.colTrackModifiedBy.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colTrackModifiedBy.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colTrackModifiedBy.OptionsColumn.FixedWidth = true;
            this.colTrackModifiedBy.OptionsColumn.ReadOnly = true;
            this.colTrackModifiedBy.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colTrackVoucherDate
            // 
            this.colTrackVoucherDate.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colTrackVoucherDate.AppearanceHeader.Font")));
            this.colTrackVoucherDate.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colTrackVoucherDate, "colTrackVoucherDate");
            this.colTrackVoucherDate.FieldName = "VOUCHER_DATE";
            this.colTrackVoucherDate.Name = "colTrackVoucherDate";
            this.colTrackVoucherDate.OptionsColumn.AllowEdit = false;
            this.colTrackVoucherDate.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colTrackVoucherDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colTrackVoucherDate.OptionsColumn.FixedWidth = true;
            this.colTrackVoucherDate.OptionsColumn.ReadOnly = true;
            this.colTrackVoucherDate.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colTrackVoucherNo
            // 
            this.colTrackVoucherNo.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colTrackVoucherNo.AppearanceHeader.Font")));
            this.colTrackVoucherNo.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colTrackVoucherNo, "colTrackVoucherNo");
            this.colTrackVoucherNo.FieldName = "VOUCHER_NO";
            this.colTrackVoucherNo.Name = "colTrackVoucherNo";
            this.colTrackVoucherNo.OptionsColumn.AllowEdit = false;
            this.colTrackVoucherNo.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colTrackVoucherNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colTrackVoucherNo.OptionsColumn.FixedWidth = true;
            this.colTrackVoucherNo.OptionsColumn.ReadOnly = true;
            this.colTrackVoucherNo.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colTrackProject
            // 
            this.colTrackProject.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colTrackProject.AppearanceHeader.Font")));
            this.colTrackProject.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colTrackProject, "colTrackProject");
            this.colTrackProject.FieldName = "PROJECT";
            this.colTrackProject.Name = "colTrackProject";
            this.colTrackProject.OptionsColumn.AllowEdit = false;
            this.colTrackProject.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colTrackProject.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colTrackProject.OptionsColumn.ReadOnly = true;
            this.colTrackProject.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colTrackVoucherType
            // 
            this.colTrackVoucherType.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colTrackVoucherType.AppearanceHeader.Font")));
            this.colTrackVoucherType.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colTrackVoucherType, "colTrackVoucherType");
            this.colTrackVoucherType.FieldName = "VOUCHER_TYPE";
            this.colTrackVoucherType.Name = "colTrackVoucherType";
            this.colTrackVoucherType.OptionsColumn.AllowEdit = false;
            this.colTrackVoucherType.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colTrackVoucherType.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colTrackVoucherType.OptionsColumn.FixedWidth = true;
            this.colTrackVoucherType.OptionsColumn.ReadOnly = true;
            this.colTrackVoucherType.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colTrackVoucherSubType
            // 
            this.colTrackVoucherSubType.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colTrackVoucherSubType.AppearanceHeader.Font")));
            this.colTrackVoucherSubType.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colTrackVoucherSubType, "colTrackVoucherSubType");
            this.colTrackVoucherSubType.FieldName = "VOUCHER_SUB_TYPE";
            this.colTrackVoucherSubType.Name = "colTrackVoucherSubType";
            this.colTrackVoucherSubType.OptionsColumn.AllowEdit = false;
            this.colTrackVoucherSubType.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colTrackVoucherSubType.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colTrackVoucherSubType.OptionsColumn.ReadOnly = true;
            this.colTrackVoucherSubType.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colTrackAmount
            // 
            this.colTrackAmount.AppearanceCell.Options.UseTextOptions = true;
            this.colTrackAmount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTrackAmount.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colTrackAmount.AppearanceHeader.Font")));
            this.colTrackAmount.AppearanceHeader.Options.UseFont = true;
            this.colTrackAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.colTrackAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colTrackAmount, "colTrackAmount");
            this.colTrackAmount.DisplayFormat.FormatString = "n";
            this.colTrackAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTrackAmount.FieldName = "AMOUNT";
            this.colTrackAmount.Name = "colTrackAmount";
            this.colTrackAmount.OptionsColumn.AllowEdit = false;
            this.colTrackAmount.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colTrackAmount.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colTrackAmount.OptionsColumn.FixedWidth = true;
            this.colTrackAmount.OptionsColumn.ReadOnly = true;
            this.colTrackAmount.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colTrackPreviousAmount
            // 
            this.colTrackPreviousAmount.AppearanceCell.Options.UseTextOptions = true;
            this.colTrackPreviousAmount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTrackPreviousAmount.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colTrackPreviousAmount.AppearanceHeader.Font")));
            this.colTrackPreviousAmount.AppearanceHeader.Options.UseFont = true;
            this.colTrackPreviousAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.colTrackPreviousAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colTrackPreviousAmount, "colTrackPreviousAmount");
            this.colTrackPreviousAmount.DisplayFormat.FormatString = "n";
            this.colTrackPreviousAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTrackPreviousAmount.FieldName = "PREVIOUS_AMOUNT";
            this.colTrackPreviousAmount.Name = "colTrackPreviousAmount";
            this.colTrackPreviousAmount.OptionsColumn.AllowEdit = false;
            this.colTrackPreviousAmount.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colTrackPreviousAmount.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colTrackPreviousAmount.OptionsColumn.FixedWidth = true;
            this.colTrackPreviousAmount.OptionsColumn.ReadOnly = true;
            this.colTrackPreviousAmount.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colTrackPreviousModifiedBy
            // 
            this.colTrackPreviousModifiedBy.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colTrackPreviousModifiedBy.AppearanceHeader.Font")));
            this.colTrackPreviousModifiedBy.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colTrackPreviousModifiedBy, "colTrackPreviousModifiedBy");
            this.colTrackPreviousModifiedBy.FieldName = "PREVIOUS_MODIFIED_BY_NAME";
            this.colTrackPreviousModifiedBy.Name = "colTrackPreviousModifiedBy";
            this.colTrackPreviousModifiedBy.OptionsColumn.AllowEdit = false;
            this.colTrackPreviousModifiedBy.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colTrackPreviousModifiedBy.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colTrackPreviousModifiedBy.OptionsColumn.FixedWidth = true;
            this.colTrackPreviousModifiedBy.OptionsColumn.ReadOnly = true;
            // 
            // colTrackByAuditor
            // 
            this.colTrackByAuditor.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colTrackByAuditor.AppearanceHeader.Font")));
            this.colTrackByAuditor.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colTrackByAuditor, "colTrackByAuditor");
            this.colTrackByAuditor.FieldName = "AUDITOR_TRACK";
            this.colTrackByAuditor.Name = "colTrackByAuditor";
            this.colTrackByAuditor.OptionsColumn.AllowEdit = false;
            this.colTrackByAuditor.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colTrackByAuditor.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colTrackByAuditor.OptionsColumn.FixedWidth = true;
            this.colTrackByAuditor.OptionsColumn.ReadOnly = true;
            // 
            // rtxtLedDebit
            // 
            resources.ApplyResources(this.rtxtLedDebit, "rtxtLedDebit");
            this.rtxtLedDebit.DisplayFormat.FormatString = "n";
            this.rtxtLedDebit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rtxtLedDebit.Mask.EditMask = resources.GetString("rtxtLedDebit.Mask.EditMask");
            this.rtxtLedDebit.Name = "rtxtLedDebit";
            // 
            // rtxtLedCredit
            // 
            resources.ApplyResources(this.rtxtLedCredit, "rtxtLedCredit");
            this.rtxtLedCredit.Name = "rtxtLedCredit";
            // 
            // layoutControlGroup2
            // 
            resources.ApplyResources(this.layoutControlGroup2, "layoutControlGroup2");
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgrpTrackOnAuditChanges,
            this.layoutControlGroup5});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "Root";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(1219, 345);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // lcgrpTrackOnAuditChanges
            // 
            this.lcgrpTrackOnAuditChanges.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcgrpTrackOnAuditChanges.AppearanceGroup.Font")));
            this.lcgrpTrackOnAuditChanges.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcgrpTrackOnAuditChanges, "lcgrpTrackOnAuditChanges");
            this.lcgrpTrackOnAuditChanges.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.lcgrpTrackOnAuditChanges.Location = new System.Drawing.Point(0, 247);
            this.lcgrpTrackOnAuditChanges.Name = "lcgrpTrackOnAuditChanges";
            this.lcgrpTrackOnAuditChanges.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgrpTrackOnAuditChanges.Size = new System.Drawing.Size(1219, 98);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcLedgerDetails;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 2, 0, 0);
            this.layoutControlItem3.Size = new System.Drawing.Size(1213, 73);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlGroup5
            // 
            resources.ApplyResources(this.layoutControlGroup5, "layoutControlGroup5");
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(1219, 247);
            this.layoutControlGroup5.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcTransaction;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1213, 241);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // pnlFill
            // 
            this.pnlFill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlFill.Controls.Add(this.panelControl1);
            this.pnlFill.Controls.Add(this.pnlOpeningBal);
            this.pnlFill.Controls.Add(this.pnlFooter);
            resources.ApplyResources(this.pnlFill, "pnlFill");
            this.pnlFill.Name = "pnlFill";
            // 
            // imgTransView
            // 
            this.imgTransView.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgTransView.ImageStream")));
            this.imgTransView.Images.SetKeyName(0, "TransLock.png");
            this.imgTransView.Images.SetKeyName(1, "TransLock1.png");
            this.imgTransView.Images.SetKeyName(2, "unlock.png");
            this.imgTransView.Images.SetKeyName(3, "Unlock-16.png");
            this.imgTransView.Images.SetKeyName(4, "unlock green.png");
            // 
            // gcTransBalance
            // 
            resources.ApplyResources(this.gcTransBalance, "gcTransBalance");
            this.gcTransBalance.MainView = this.gvTransBalance;
            this.gcTransBalance.Name = "gcTransBalance";
            this.gcTransBalance.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTransBalance,
            this.gridView1});
            // 
            // gvTransBalance
            // 
            this.gvTransBalance.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvTransBalance.Appearance.FocusedRow.Font")));
            this.gvTransBalance.Appearance.FocusedRow.Options.UseFont = true;
            this.gvTransBalance.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvTransBalance.Appearance.HeaderPanel.Font")));
            this.gvTransBalance.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvTransBalance.GridControl = this.gcTransBalance;
            this.gvTransBalance.Name = "gvTransBalance";
            this.gvTransBalance.OptionsBehavior.Editable = false;
            this.gvTransBalance.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvTransBalance.OptionsView.ShowGroupPanel = false;
            this.gvTransBalance.OptionsView.ShowIndicator = false;
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gcTransBalance;
            this.gridView1.Name = "gridView1";
            // 
            // colTransLedgerId
            // 
            resources.ApplyResources(this.colTransLedgerId, "colTransLedgerId");
            this.colTransLedgerId.FieldName = "ID";
            this.colTransLedgerId.Name = "colTransLedgerId";
            // 
            // colTransLedgerName
            // 
            resources.ApplyResources(this.colTransLedgerName, "colTransLedgerName");
            this.colTransLedgerName.FieldName = "LEDGER_NAME";
            this.colTransLedgerName.Name = "colTransLedgerName";
            // 
            // colTransAmount
            // 
            resources.ApplyResources(this.colTransAmount, "colTransAmount");
            this.colTransAmount.DisplayFormat.FormatString = "N";
            this.colTransAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTransAmount.FieldName = "AMOUNT";
            this.colTransAmount.Name = "colTransAmount";
            // 
            // colType
            // 
            this.colType.FieldName = "TRANSMODE";
            this.colType.Name = "colType";
            this.colType.OptionsColumn.ShowCaption = false;
            resources.ApplyResources(this.colType, "colType");
            // 
            // frmVouchersAuditLog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlFill);
            this.Controls.Add(this.pnlToolBar);
            this.Name = "frmVouchersAuditLog";
            this.ShowFilterClicked += new System.EventHandler(this.frmTransactionView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmTransactionView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmTransactionView_Activated);
            this.Load += new System.EventHandler(this.frmTransactionView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvVoucher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTransaction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTransaction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtDebit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtCredit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnLockTrans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnPrintVoucher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnDuplicateVoucher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCostCentre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolBar)).EndInit();
            this.pnlToolBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlFooter)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlOpeningBal)).EndInit();
            this.pnlOpeningBal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcTransactionGroup)).EndInit();
            this.gcTransactionGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkJournal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkContra.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPayments.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkReceipt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcLedgerDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLedgerDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtLedDebit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtLedCredit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgrpTrackOnAuditChanges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFill)).EndInit();
            this.pnlFill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgTransView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTransBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTransBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlToolBar;
        private DevExpress.XtraEditors.PanelControl pnlFooter;
        private DevExpress.XtraGrid.GridControl gcTransaction;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTransaction;
        private Bosco.Utility.Controls.ucToolBar ucTransaction;
        private DevExpress.XtraEditors.LabelControl lblCount;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
        private DevExpress.XtraEditors.PanelControl pnlOpeningBal;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.DateEdit deDateTo;
        private DevExpress.XtraEditors.GridLookUpEdit glkpProject;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem lblProject;
        private DevExpress.XtraLayout.LayoutControlItem lblDateFrom;
        private DevExpress.XtraLayout.LayoutControlItem lblDateTo;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl pnlFill;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectId;
        private DevExpress.XtraGrid.Columns.GridColumn colProject;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherID;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherDate;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectDivision;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherNo;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherMode;
        private DevExpress.XtraGrid.Columns.GridColumn colDonor;
        private DevExpress.XtraGrid.Columns.GridColumn colPurposeName;
        private DevExpress.XtraGrid.Columns.GridColumn colContributionAmonut;
        private DevExpress.XtraGrid.Columns.GridColumn colExchangeRate;
        private DevExpress.XtraGrid.Columns.GridColumn colExchangeCountryID;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCredit;
        private DevExpress.XtraGrid.Columns.GridColumn colDebit;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtCredit;
        private DevExpress.XtraGrid.Views.Grid.GridView gvVoucher;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherTransID;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerName;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerFlag;
        private DevExpress.XtraGrid.Columns.GridColumn colCHEQUE_NO;
        private DevExpress.XtraGrid.Columns.GridColumn colMATERIALIZED_ON;
        private DevExpress.XtraGrid.Columns.GridColumn colChequeNo;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colTransactionMode;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherTransDebit;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherTransCredit;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerId;
        private Bosco.Utility.Controls.ucToolBar ucTrans;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnPrint;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnPrintVoucher;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherSubType;
        private DevExpress.XtraGrid.Columns.GridColumn colMasterLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colFDVoucherId;
        private DevExpress.XtraGrid.Columns.GridColumn colTopLedgerName;
        private DevExpress.XtraGrid.Columns.GridColumn colNarration;
        private DevExpress.XtraGrid.Columns.GridColumn colcashbank;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCostCentre;
        private DevExpress.XtraGrid.Columns.GridColumn colCLedgerID;
        private DevExpress.XtraGrid.Columns.GridColumn colCostCentreId;
        private DevExpress.XtraGrid.Columns.GridColumn colCostcentreName;
        private DevExpress.XtraGrid.Columns.GridColumn colCAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colCLedgerName;
        private DevExpress.XtraGrid.Columns.GridColumn colTranLocked;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnLockTrans;
        private DevExpress.Utils.ImageCollection imgTransView;
        private DevExpress.XtraGrid.Columns.GridColumn colNameAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colDebitAmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtDebit;
        private DevExpress.XtraGrid.Columns.GridColumn colReceiptType;
        private DevExpress.XtraGrid.GridControl gcTransBalance;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTransBalance;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colTransLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colTransLedgerName;
        private DevExpress.XtraGrid.Columns.GridColumn colTransAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        public DevExpress.XtraEditors.DateEdit deDateFrom;
        private DevExpress.XtraGrid.Columns.GridColumn colPayRollClientId;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gcLedgerDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLedgerDetails;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn colTrackVoucherId;
        private DevExpress.XtraGrid.Columns.GridColumn colTrackAudtion;
        private DevExpress.XtraGrid.Columns.GridColumn colTrackModifiedOn;
        private DevExpress.XtraGrid.Columns.GridColumn colTrackAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colTrackPreviousAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colTrackPreviousModifiedBy;
        private DevExpress.XtraLayout.LayoutControlGroup lcgrpTrackOnAuditChanges;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtLedDebit;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtLedCredit;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnDuplicateVoucher;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherDefinitionId;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherType;
        private DevExpress.XtraGrid.Columns.GridColumn colModifiedBy;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedOn;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedBy;
        private DevExpress.XtraGrid.Columns.GridColumn colModifiedOn;
        private DevExpress.XtraGrid.Columns.GridColumn colAuditAction;
        private DevExpress.XtraEditors.GroupControl gcTransactionGroup;
        private DevExpress.XtraEditors.CheckEdit chkJournal;
        private DevExpress.XtraEditors.CheckEdit chkContra;
        private DevExpress.XtraEditors.CheckEdit chkPayments;
        private DevExpress.XtraEditors.CheckEdit chkReceipt;
        private DevExpress.XtraEditors.SimpleButton btnApply;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colAuditTrackOn;
        private DevExpress.XtraGrid.Columns.GridColumn colTrackModifiedBy;
        private DevExpress.XtraGrid.Columns.GridColumn colTrackRevision;
        private DevExpress.XtraGrid.Columns.GridColumn colTrackVoucherDate;
        private DevExpress.XtraGrid.Columns.GridColumn colTrackVoucherNo;
        private DevExpress.XtraGrid.Columns.GridColumn colTrackProject;
        private DevExpress.XtraGrid.Columns.GridColumn colTrackVoucherType;
        private DevExpress.XtraGrid.Columns.GridColumn colTrackVoucherSubType;
        private DevExpress.XtraGrid.Columns.GridColumn colTrackByAuditor;
    }
}