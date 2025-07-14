namespace PAYROLL.Modules.Payroll_app
{
    partial class frmPostVoucherview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPostVoucherview));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcLedgerDetails = new DevExpress.XtraGrid.GridControl();
            this.gvLedgerDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ggcolVoucherTransID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ggcolLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtLedDebit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colComponentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ggcolTransactionMode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ggcolVoucherTransDebit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ggcolVoucherTransCredit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtLedCredit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ggcolAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ggcolLedgerFlag = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ggcolAccountNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ggcolCHEQUE_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ggcolMATERIALIZED_ON = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ggcolLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcPostVOucherview = new DevExpress.XtraGrid.GridControl();
            this.gvPostVoucherView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colVoucherId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPayrollMonth = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPayrollGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcrledgername = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCashBank = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ucPostPayment = new PAYROLL.UserControl.UcToolBar();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcToolbar = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcVoucherMaster = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblRowSymbol = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblRowCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lcGrpLedgerComponent = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcLedgerDetails = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcLedgerDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLedgerDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtLedDebit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtLedCredit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPostVOucherview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPostVoucherView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcToolbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcVoucherMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRowSymbol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRowCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGrpLedgerComponent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcLedgerDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcLedgerDetails);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.gcPostVOucherview);
            this.layoutControl1.Controls.Add(this.ucPostPayment);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(581, 305, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
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
            this.ggcolVoucherTransID,
            this.ggcolLedgerName,
            this.colComponentName,
            this.ggcolTransactionMode,
            this.ggcolVoucherTransDebit,
            this.ggcolVoucherTransCredit,
            this.ggcolAmount,
            this.ggcolLedgerFlag,
            this.ggcolAccountNumber,
            this.ggcolCHEQUE_NO,
            this.ggcolMATERIALIZED_ON,
            this.ggcolLedgerId});
            this.gvLedgerDetails.GridControl = this.gcLedgerDetails;
            this.gvLedgerDetails.Name = "gvLedgerDetails";
            this.gvLedgerDetails.OptionsView.ShowGroupPanel = false;
            // 
            // ggcolVoucherTransID
            // 
            this.ggcolVoucherTransID.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("ggcolVoucherTransID.AppearanceHeader.Font")));
            this.ggcolVoucherTransID.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.ggcolVoucherTransID, "ggcolVoucherTransID");
            this.ggcolVoucherTransID.FieldName = "VOUCHER_ID";
            this.ggcolVoucherTransID.Name = "ggcolVoucherTransID";
            // 
            // ggcolLedgerName
            // 
            this.ggcolLedgerName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("ggcolLedgerName.AppearanceHeader.Font")));
            this.ggcolLedgerName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.ggcolLedgerName, "ggcolLedgerName");
            this.ggcolLedgerName.ColumnEdit = this.rtxtLedDebit;
            this.ggcolLedgerName.FieldName = "LEDGER_NAME";
            this.ggcolLedgerName.Name = "ggcolLedgerName";
            this.ggcolLedgerName.OptionsColumn.AllowEdit = false;
            this.ggcolLedgerName.OptionsColumn.AllowFocus = false;
            // 
            // rtxtLedDebit
            // 
            resources.ApplyResources(this.rtxtLedDebit, "rtxtLedDebit");
            this.rtxtLedDebit.DisplayFormat.FormatString = "n";
            this.rtxtLedDebit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rtxtLedDebit.Mask.EditMask = resources.GetString("rtxtLedDebit.Mask.EditMask");
            this.rtxtLedDebit.Name = "rtxtLedDebit";
            // 
            // colComponentName
            // 
            this.colComponentName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colComponentName.AppearanceHeader.Font")));
            this.colComponentName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colComponentName, "colComponentName");
            this.colComponentName.FieldName = "COMPONENT";
            this.colComponentName.Name = "colComponentName";
            this.colComponentName.OptionsColumn.AllowEdit = false;
            // 
            // ggcolTransactionMode
            // 
            this.ggcolTransactionMode.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("ggcolTransactionMode.AppearanceHeader.Font")));
            this.ggcolTransactionMode.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.ggcolTransactionMode, "ggcolTransactionMode");
            this.ggcolTransactionMode.FieldName = "TRANSMODE";
            this.ggcolTransactionMode.Name = "ggcolTransactionMode";
            // 
            // ggcolVoucherTransDebit
            // 
            this.ggcolVoucherTransDebit.AppearanceCell.Options.UseTextOptions = true;
            this.ggcolVoucherTransDebit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.ggcolVoucherTransDebit.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("ggcolVoucherTransDebit.AppearanceHeader.Font")));
            this.ggcolVoucherTransDebit.AppearanceHeader.Options.UseFont = true;
            this.ggcolVoucherTransDebit.AppearanceHeader.Options.UseTextOptions = true;
            this.ggcolVoucherTransDebit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.ggcolVoucherTransDebit, "ggcolVoucherTransDebit");
            this.ggcolVoucherTransDebit.ColumnEdit = this.rtxtLedDebit;
            this.ggcolVoucherTransDebit.DisplayFormat.FormatString = "n";
            this.ggcolVoucherTransDebit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ggcolVoucherTransDebit.FieldName = "DEBIT";
            this.ggcolVoucherTransDebit.Name = "ggcolVoucherTransDebit";
            this.ggcolVoucherTransDebit.OptionsColumn.AllowEdit = false;
            this.ggcolVoucherTransDebit.OptionsColumn.AllowFocus = false;
            // 
            // ggcolVoucherTransCredit
            // 
            this.ggcolVoucherTransCredit.AppearanceCell.Options.UseTextOptions = true;
            this.ggcolVoucherTransCredit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.ggcolVoucherTransCredit.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("ggcolVoucherTransCredit.AppearanceHeader.Font")));
            this.ggcolVoucherTransCredit.AppearanceHeader.Options.UseFont = true;
            this.ggcolVoucherTransCredit.AppearanceHeader.Options.UseTextOptions = true;
            this.ggcolVoucherTransCredit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.ggcolVoucherTransCredit, "ggcolVoucherTransCredit");
            this.ggcolVoucherTransCredit.ColumnEdit = this.rtxtLedCredit;
            this.ggcolVoucherTransCredit.DisplayFormat.FormatString = "n";
            this.ggcolVoucherTransCredit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ggcolVoucherTransCredit.FieldName = "CREDIT";
            this.ggcolVoucherTransCredit.Name = "ggcolVoucherTransCredit";
            this.ggcolVoucherTransCredit.OptionsColumn.AllowEdit = false;
            this.ggcolVoucherTransCredit.OptionsColumn.AllowFocus = false;
            // 
            // rtxtLedCredit
            // 
            resources.ApplyResources(this.rtxtLedCredit, "rtxtLedCredit");
            this.rtxtLedCredit.Name = "rtxtLedCredit";
            // 
            // ggcolAmount
            // 
            this.ggcolAmount.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("ggcolAmount.AppearanceHeader.Font")));
            this.ggcolAmount.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.ggcolAmount, "ggcolAmount");
            this.ggcolAmount.FieldName = "AMOUNT";
            this.ggcolAmount.Name = "ggcolAmount";
            // 
            // ggcolLedgerFlag
            // 
            this.ggcolLedgerFlag.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("ggcolLedgerFlag.AppearanceHeader.Font")));
            this.ggcolLedgerFlag.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.ggcolLedgerFlag, "ggcolLedgerFlag");
            this.ggcolLedgerFlag.FieldName = "LEDGER_FLAG";
            this.ggcolLedgerFlag.Name = "ggcolLedgerFlag";
            // 
            // ggcolAccountNumber
            // 
            this.ggcolAccountNumber.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("ggcolAccountNumber.AppearanceHeader.Font")));
            this.ggcolAccountNumber.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.ggcolAccountNumber, "ggcolAccountNumber");
            this.ggcolAccountNumber.FieldName = "ACCOUNT_NUMBER";
            this.ggcolAccountNumber.Name = "ggcolAccountNumber";
            this.ggcolAccountNumber.OptionsColumn.AllowEdit = false;
            this.ggcolAccountNumber.OptionsColumn.AllowFocus = false;
            // 
            // ggcolCHEQUE_NO
            // 
            this.ggcolCHEQUE_NO.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("ggcolCHEQUE_NO.AppearanceHeader.Font")));
            this.ggcolCHEQUE_NO.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.ggcolCHEQUE_NO, "ggcolCHEQUE_NO");
            this.ggcolCHEQUE_NO.FieldName = "CHEQUE_NO";
            this.ggcolCHEQUE_NO.Name = "ggcolCHEQUE_NO";
            this.ggcolCHEQUE_NO.OptionsColumn.AllowEdit = false;
            this.ggcolCHEQUE_NO.OptionsColumn.AllowFocus = false;
            // 
            // ggcolMATERIALIZED_ON
            // 
            this.ggcolMATERIALIZED_ON.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("ggcolMATERIALIZED_ON.AppearanceHeader.Font")));
            this.ggcolMATERIALIZED_ON.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.ggcolMATERIALIZED_ON, "ggcolMATERIALIZED_ON");
            this.ggcolMATERIALIZED_ON.FieldName = "MATERIALIZED_ON";
            this.ggcolMATERIALIZED_ON.Name = "ggcolMATERIALIZED_ON";
            this.ggcolMATERIALIZED_ON.OptionsColumn.AllowFocus = false;
            // 
            // ggcolLedgerId
            // 
            this.ggcolLedgerId.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("ggcolLedgerId.AppearanceHeader.Font")));
            this.ggcolLedgerId.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.ggcolLedgerId, "ggcolLedgerId");
            this.ggcolLedgerId.FieldName = "LEDGER_ID";
            this.ggcolLedgerId.Name = "ggcolLedgerId";
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.StyleController = this.layoutControl1;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // gcPostVOucherview
            // 
            resources.ApplyResources(this.gcPostVOucherview, "gcPostVOucherview");
            this.gcPostVOucherview.MainView = this.gvPostVoucherView;
            this.gcPostVOucherview.Name = "gcPostVOucherview";
            this.gcPostVOucherview.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPostVoucherView});
            // 
            // gvPostVoucherView
            // 
            this.gvPostVoucherView.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvPostVoucherView.Appearance.FocusedRow.Font")));
            this.gvPostVoucherView.Appearance.FocusedRow.Options.UseFont = true;
            this.gvPostVoucherView.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvPostVoucherView.Appearance.HeaderPanel.Font")));
            this.gvPostVoucherView.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvPostVoucherView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colVoucherId,
            this.colDate,
            this.colPayrollMonth,
            this.colPayrollGroup,
            this.colProject,
            this.colcrledgername,
            this.colCashBank,
            this.colAmount});
            this.gvPostVoucherView.GridControl = this.gcPostVOucherview;
            this.gvPostVoucherView.Name = "gvPostVoucherView";
            this.gvPostVoucherView.OptionsBehavior.Editable = false;
            this.gvPostVoucherView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvPostVoucherView.OptionsView.ShowGroupPanel = false;
            this.gvPostVoucherView.OptionsView.ShowIndicator = false;
            this.gvPostVoucherView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvPostVoucherView_FocusedRowChanged);
            this.gvPostVoucherView.DoubleClick += new System.EventHandler(this.gvPostVoucherView_DoubleClick);
            this.gvPostVoucherView.RowCountChanged += new System.EventHandler(this.gvPostVoucherView_RowCountChanged);
            // 
            // colVoucherId
            // 
            this.colVoucherId.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colVoucherId.AppearanceHeader.Font")));
            this.colVoucherId.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colVoucherId, "colVoucherId");
            this.colVoucherId.FieldName = "VOUCHER_ID";
            this.colVoucherId.Name = "colVoucherId";
            // 
            // colDate
            // 
            this.colDate.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colDate.AppearanceHeader.Font")));
            this.colDate.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colDate, "colDate");
            this.colDate.FieldName = "VOUCHER_DATE";
            this.colDate.Name = "colDate";
            // 
            // colPayrollMonth
            // 
            this.colPayrollMonth.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colPayrollMonth.AppearanceHeader.Font")));
            this.colPayrollMonth.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colPayrollMonth, "colPayrollMonth");
            this.colPayrollMonth.FieldName = "PRNAME";
            this.colPayrollMonth.Name = "colPayrollMonth";
            // 
            // colPayrollGroup
            // 
            this.colPayrollGroup.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colPayrollGroup.AppearanceHeader.Font")));
            this.colPayrollGroup.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colPayrollGroup, "colPayrollGroup");
            this.colPayrollGroup.FieldName = "PAYROLL_GROUP";
            this.colPayrollGroup.Name = "colPayrollGroup";
            this.colPayrollGroup.OptionsColumn.AllowEdit = false;
            // 
            // colProject
            // 
            this.colProject.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colProject.AppearanceHeader.Font")));
            this.colProject.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colProject, "colProject");
            this.colProject.FieldName = "PROJECT";
            this.colProject.Name = "colProject";
            // 
            // colcrledgername
            // 
            this.colcrledgername.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colcrledgername.AppearanceHeader.Font")));
            this.colcrledgername.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colcrledgername, "colcrledgername");
            this.colcrledgername.FieldName = "LEDGER_NAME";
            this.colcrledgername.Name = "colcrledgername";
            // 
            // colCashBank
            // 
            this.colCashBank.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colCashBank.AppearanceHeader.Font")));
            this.colCashBank.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colCashBank, "colCashBank");
            this.colCashBank.FieldName = "CASHBANK";
            this.colCashBank.Name = "colCashBank";
            // 
            // colAmount
            // 
            this.colAmount.AppearanceCell.Options.UseTextOptions = true;
            this.colAmount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colAmount.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAmount.AppearanceHeader.Font")));
            this.colAmount.AppearanceHeader.Options.UseFont = true;
            this.colAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.colAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colAmount, "colAmount");
            this.colAmount.DisplayFormat.FormatString = "n";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "DEBIT_AMOUNT";
            this.colAmount.Name = "colAmount";
            // 
            // ucPostPayment
            // 
            this.ucPostPayment.ChangeAddCaption = "P<u>&o</u>st Voucher";
            this.ucPostPayment.DisableAddButton = true;
            this.ucPostPayment.DisableCloseButton = true;
            this.ucPostPayment.DisableDeleteButton = true;
            this.ucPostPayment.DisableEditButton = true;
            this.ucPostPayment.DisableImportButton = true;
            this.ucPostPayment.DisablePrintButton = true;
            this.ucPostPayment.DisableRefreshButton = true;
            resources.ApplyResources(this.ucPostPayment, "ucPostPayment");
            this.ucPostPayment.Name = "ucPostPayment";
            this.ucPostPayment.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucPostPayment.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucPostPayment.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucPostPayment.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucPostPayment.VisibleImport = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucPostPayment.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucPostPayment.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucPostPayment.AddClicked += new System.EventHandler(this.ucPostPayment_AddClicked);
            this.ucPostPayment.EditClicked += new System.EventHandler(this.ucPostPayment_EditClicked);
            this.ucPostPayment.DeleteClicked += new System.EventHandler(this.ucPostPayment_DeleteClicked);
            this.ucPostPayment.PrintClicked += new System.EventHandler(this.ucPostPayment_PrintClicked);
            this.ucPostPayment.CloseClicked += new System.EventHandler(this.ucPostPayment_CloseClicked);
            this.ucPostPayment.RefreshClicked += new System.EventHandler(this.ucPostPayment_RefreshClicked);
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcToolbar,
            this.lcVoucherMaster,
            this.layoutControlItem3,
            this.lblRowSymbol,
            this.lblRowCount,
            this.lcGrpLedgerComponent});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(785, 489);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcToolbar
            // 
            this.lcToolbar.Control = this.ucPostPayment;
            resources.ApplyResources(this.lcToolbar, "lcToolbar");
            this.lcToolbar.Location = new System.Drawing.Point(0, 0);
            this.lcToolbar.MaxSize = new System.Drawing.Size(785, 38);
            this.lcToolbar.MinSize = new System.Drawing.Size(785, 38);
            this.lcToolbar.Name = "lcToolbar";
            this.lcToolbar.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcToolbar.Size = new System.Drawing.Size(785, 38);
            this.lcToolbar.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcToolbar.TextSize = new System.Drawing.Size(0, 0);
            this.lcToolbar.TextToControlDistance = 0;
            this.lcToolbar.TextVisible = false;
            // 
            // lcVoucherMaster
            // 
            this.lcVoucherMaster.Control = this.gcPostVOucherview;
            resources.ApplyResources(this.lcVoucherMaster, "lcVoucherMaster");
            this.lcVoucherMaster.Location = new System.Drawing.Point(0, 38);
            this.lcVoucherMaster.MinSize = new System.Drawing.Size(110, 30);
            this.lcVoucherMaster.Name = "lcVoucherMaster";
            this.lcVoucherMaster.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcVoucherMaster.Size = new System.Drawing.Size(785, 275);
            this.lcVoucherMaster.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcVoucherMaster.TextSize = new System.Drawing.Size(0, 0);
            this.lcVoucherMaster.TextToControlDistance = 0;
            this.lcVoucherMaster.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 466);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(79, 23);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(689, 23);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // lblRowSymbol
            // 
            this.lblRowSymbol.AllowHotTrack = false;
            this.lblRowSymbol.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblRowSymbol.AppearanceItemCaption.Font")));
            this.lblRowSymbol.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblRowSymbol, "lblRowSymbol");
            this.lblRowSymbol.Location = new System.Drawing.Point(689, 466);
            this.lblRowSymbol.MaxSize = new System.Drawing.Size(22, 23);
            this.lblRowSymbol.MinSize = new System.Drawing.Size(22, 23);
            this.lblRowSymbol.Name = "lblRowSymbol";
            this.lblRowSymbol.Size = new System.Drawing.Size(22, 23);
            this.lblRowSymbol.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblRowSymbol.TextSize = new System.Drawing.Size(9, 13);
            // 
            // lblRowCount
            // 
            this.lblRowCount.AllowHotTrack = false;
            this.lblRowCount.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblRowCount.AppearanceItemCaption.Font")));
            this.lblRowCount.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblRowCount, "lblRowCount");
            this.lblRowCount.Location = new System.Drawing.Point(711, 466);
            this.lblRowCount.MaxSize = new System.Drawing.Size(74, 23);
            this.lblRowCount.MinSize = new System.Drawing.Size(74, 23);
            this.lblRowCount.Name = "lblRowCount";
            this.lblRowCount.Size = new System.Drawing.Size(74, 23);
            this.lblRowCount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblRowCount.TextSize = new System.Drawing.Size(9, 13);
            // 
            // lcGrpLedgerComponent
            // 
            this.lcGrpLedgerComponent.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcGrpLedgerComponent.AppearanceGroup.Font")));
            this.lcGrpLedgerComponent.AppearanceGroup.Options.UseFont = true;
            this.lcGrpLedgerComponent.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lcGrpLedgerComponent.AppearanceItemCaption.Font")));
            this.lcGrpLedgerComponent.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lcGrpLedgerComponent, "lcGrpLedgerComponent");
            this.lcGrpLedgerComponent.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcLedgerDetails});
            this.lcGrpLedgerComponent.Location = new System.Drawing.Point(0, 313);
            this.lcGrpLedgerComponent.Name = "lcGrpLedgerComponent";
            this.lcGrpLedgerComponent.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.lcGrpLedgerComponent.Size = new System.Drawing.Size(785, 153);
            // 
            // lcLedgerDetails
            // 
            this.lcLedgerDetails.Control = this.gcLedgerDetails;
            resources.ApplyResources(this.lcLedgerDetails, "lcLedgerDetails");
            this.lcLedgerDetails.Location = new System.Drawing.Point(0, 0);
            this.lcLedgerDetails.MaxSize = new System.Drawing.Size(0, 122);
            this.lcLedgerDetails.MinSize = new System.Drawing.Size(110, 122);
            this.lcLedgerDetails.Name = "lcLedgerDetails";
            this.lcLedgerDetails.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.lcLedgerDetails.Size = new System.Drawing.Size(773, 122);
            this.lcLedgerDetails.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcLedgerDetails.TextSize = new System.Drawing.Size(0, 0);
            this.lcLedgerDetails.TextToControlDistance = 0;
            this.lcLedgerDetails.TextVisible = false;
            // 
            // frmPostVoucherview
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPostVoucherview";
            this.ShowFilterClicked += new System.EventHandler(this.frmPostVoucherview_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmPostVoucherview_EnterClicked);
            this.Activated += new System.EventHandler(this.frmPostVoucherview_Activated);
            this.Load += new System.EventHandler(this.frmPostVoucherview_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcLedgerDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLedgerDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtLedDebit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtLedCredit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPostVOucherview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPostVoucherView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcToolbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcVoucherMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRowSymbol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRowCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGrpLedgerComponent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcLedgerDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcPostVOucherview;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPostVoucherView;
        private UserControl.UcToolBar ucPostPayment;
        private DevExpress.XtraLayout.LayoutControlItem lcToolbar;
        private DevExpress.XtraLayout.LayoutControlItem lcVoucherMaster;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherId;
        private DevExpress.XtraGrid.Columns.GridColumn colDate;
        private DevExpress.XtraGrid.Columns.GridColumn colPayrollMonth;
        private DevExpress.XtraGrid.Columns.GridColumn colProject;
        private DevExpress.XtraGrid.Columns.GridColumn colCashBank;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colcrledgername;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.SimpleLabelItem lblRowSymbol;
        private DevExpress.XtraLayout.SimpleLabelItem lblRowCount;
        private DevExpress.XtraGrid.Columns.GridColumn colPayrollGroup;
        private DevExpress.XtraGrid.GridControl gcLedgerDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLedgerDetails;
        private DevExpress.XtraGrid.Columns.GridColumn ggcolVoucherTransID;
        private DevExpress.XtraGrid.Columns.GridColumn ggcolLedgerName;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtLedDebit;
        private DevExpress.XtraGrid.Columns.GridColumn ggcolTransactionMode;
        private DevExpress.XtraGrid.Columns.GridColumn ggcolVoucherTransDebit;
        private DevExpress.XtraGrid.Columns.GridColumn ggcolVoucherTransCredit;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtLedCredit;
        private DevExpress.XtraGrid.Columns.GridColumn ggcolAmount;
        private DevExpress.XtraGrid.Columns.GridColumn ggcolLedgerFlag;
        private DevExpress.XtraGrid.Columns.GridColumn ggcolAccountNumber;
        private DevExpress.XtraGrid.Columns.GridColumn ggcolCHEQUE_NO;
        private DevExpress.XtraGrid.Columns.GridColumn ggcolMATERIALIZED_ON;
        private DevExpress.XtraGrid.Columns.GridColumn ggcolLedgerId;
        private DevExpress.XtraLayout.LayoutControlItem lcLedgerDetails;
        private DevExpress.XtraGrid.Columns.GridColumn colComponentName;
        private DevExpress.XtraLayout.LayoutControlGroup lcGrpLedgerComponent;
    }
}