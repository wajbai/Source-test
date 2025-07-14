namespace ACPP.Modules.Master
{
    partial class frmProjectView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProjectView));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.gvLedger = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLedgerProjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGroupCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGroupName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcProjectView = new DevExpress.XtraGrid.GridControl();
            this.gvVoucher = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colVoucherId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoucherProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoucherName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoucherType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoucherMethod = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrefixCharacter = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSuffixCharacter = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMonth = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvProjectView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectDivision = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStartedOn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colClosedOn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlProjectToolBar = new DevExpress.XtraEditors.PanelControl();
            this.ucToolBarProjectview = new Bosco.Utility.Controls.ucToolBar();
            this.pnlProjectView = new DevExpress.XtraEditors.PanelControl();
            this.pnlProjectDetails = new DevExpress.XtraEditors.PanelControl();
            this.detProjectDateClosed = new DevExpress.XtraEditors.DateEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.lblProjectClosedDate = new DevExpress.XtraEditors.LabelControl();
            this.lblProjectNameValue = new DevExpress.XtraEditors.LabelControl();
            this.lblProjectName = new DevExpress.XtraEditors.LabelControl();
            this.lblProjectTitle = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.pnlProjectFooter = new DevExpress.XtraEditors.PanelControl();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.lblRecordCountSymbol = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.colClosedBy = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcProjectView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvVoucher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProjectView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlProjectToolBar)).BeginInit();
            this.pnlProjectToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlProjectView)).BeginInit();
            this.pnlProjectView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlProjectDetails)).BeginInit();
            this.pnlProjectDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.detProjectDateClosed.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detProjectDateClosed.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlProjectFooter)).BeginInit();
            this.pnlProjectFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gvLedger
            // 
            this.gvLedger.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvLedger.Appearance.FocusedRow.Font")));
            this.gvLedger.Appearance.FocusedRow.Options.UseFont = true;
            this.gvLedger.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvLedger.Appearance.HeaderPanel.Font")));
            this.gvLedger.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvLedger.AppearancePrint.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvLedger.AppearancePrint.HeaderPanel.Font")));
            this.gvLedger.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvLedger.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLedgerProjectId,
            this.colLedgerId,
            this.colLedgerCode,
            this.colLedgerName,
            this.colGroupCode,
            this.colGroupName});
            this.gvLedger.GridControl = this.gcProjectView;
            this.gvLedger.Name = "gvLedger";
            this.gvLedger.OptionsBehavior.Editable = false;
            this.gvLedger.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvLedger.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gvLedger.OptionsView.ShowGroupPanel = false;
            this.gvLedger.OptionsView.ShowIndicator = false;
            this.gvLedger.SynchronizeClones = false;
            // 
            // colLedgerProjectId
            // 
            this.colLedgerProjectId.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colLedgerProjectId.AppearanceHeader.Font")));
            this.colLedgerProjectId.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colLedgerProjectId, "colLedgerProjectId");
            this.colLedgerProjectId.FieldName = "PROJECT_ID";
            this.colLedgerProjectId.Name = "colLedgerProjectId";
            // 
            // colLedgerId
            // 
            this.colLedgerId.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colLedgerId.AppearanceHeader.Font")));
            this.colLedgerId.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colLedgerId, "colLedgerId");
            this.colLedgerId.Name = "colLedgerId";
            // 
            // colLedgerCode
            // 
            this.colLedgerCode.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colLedgerCode.AppearanceHeader.Font")));
            this.colLedgerCode.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colLedgerCode, "colLedgerCode");
            this.colLedgerCode.FieldName = "LEDGER_CODE";
            this.colLedgerCode.Name = "colLedgerCode";
            // 
            // colLedgerName
            // 
            this.colLedgerName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colLedgerName.AppearanceHeader.Font")));
            this.colLedgerName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colLedgerName, "colLedgerName");
            this.colLedgerName.FieldName = "LEDGER_NAME";
            this.colLedgerName.Name = "colLedgerName";
            // 
            // colGroupCode
            // 
            this.colGroupCode.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colGroupCode.AppearanceHeader.Font")));
            this.colGroupCode.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colGroupCode, "colGroupCode");
            this.colGroupCode.FieldName = "GROUP_CODE";
            this.colGroupCode.Name = "colGroupCode";
            // 
            // colGroupName
            // 
            this.colGroupName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colGroupName.AppearanceHeader.Font")));
            this.colGroupName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colGroupName, "colGroupName");
            this.colGroupName.FieldName = "LEDGER_GROUP";
            this.colGroupName.Name = "colGroupName";
            // 
            // gcProjectView
            // 
            resources.ApplyResources(this.gcProjectView, "gcProjectView");
            gridLevelNode1.LevelTemplate = this.gvLedger;
            gridLevelNode1.RelationName = "Ledger";
            gridLevelNode2.LevelTemplate = this.gvVoucher;
            gridLevelNode2.RelationName = "Voucher";
            this.gcProjectView.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1,
            gridLevelNode2});
            this.gcProjectView.MainView = this.gvProjectView;
            this.gcProjectView.Name = "gcProjectView";
            this.gcProjectView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvVoucher,
            this.gvProjectView,
            this.gvLedger});
            this.gcProjectView.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.gcProjectView_PreviewKeyDown);
            // 
            // gvVoucher
            // 
            this.gvVoucher.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvVoucher.Appearance.FocusedRow.Font")));
            this.gvVoucher.Appearance.FocusedRow.Options.UseFont = true;
            this.gvVoucher.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvVoucher.Appearance.HeaderPanel.Font")));
            this.gvVoucher.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvVoucher.AppearancePrint.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvVoucher.AppearancePrint.HeaderPanel.Font")));
            this.gvVoucher.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvVoucher.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colVoucherId,
            this.colVoucherProject,
            this.colVoucherName,
            this.colVoucherType,
            this.colVoucherMethod,
            this.colPrefixCharacter,
            this.colSuffixCharacter,
            this.colMonth});
            this.gvVoucher.GridControl = this.gcProjectView;
            this.gvVoucher.Name = "gvVoucher";
            this.gvVoucher.OptionsBehavior.Editable = false;
            this.gvVoucher.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvVoucher.OptionsView.ShowGroupPanel = false;
            this.gvVoucher.OptionsView.ShowIndicator = false;
            // 
            // colVoucherId
            // 
            this.colVoucherId.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colVoucherId.AppearanceHeader.Font")));
            this.colVoucherId.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colVoucherId, "colVoucherId");
            this.colVoucherId.FieldName = "VOUCHER_ID";
            this.colVoucherId.Name = "colVoucherId";
            // 
            // colVoucherProject
            // 
            this.colVoucherProject.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colVoucherProject.AppearanceHeader.Font")));
            this.colVoucherProject.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colVoucherProject, "colVoucherProject");
            this.colVoucherProject.FieldName = "PROJECT_ID";
            this.colVoucherProject.Name = "colVoucherProject";
            // 
            // colVoucherName
            // 
            this.colVoucherName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colVoucherName.AppearanceHeader.Font")));
            this.colVoucherName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colVoucherName, "colVoucherName");
            this.colVoucherName.FieldName = "VOUCHER_NAME";
            this.colVoucherName.Name = "colVoucherName";
            // 
            // colVoucherType
            // 
            this.colVoucherType.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colVoucherType.AppearanceHeader.Font")));
            this.colVoucherType.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colVoucherType, "colVoucherType");
            this.colVoucherType.FieldName = "VOUCHER_TYPE";
            this.colVoucherType.Name = "colVoucherType";
            // 
            // colVoucherMethod
            // 
            this.colVoucherMethod.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colVoucherMethod.AppearanceHeader.Font")));
            this.colVoucherMethod.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colVoucherMethod, "colVoucherMethod");
            this.colVoucherMethod.FieldName = "VOUCHER_METHOD";
            this.colVoucherMethod.Name = "colVoucherMethod";
            // 
            // colPrefixCharacter
            // 
            this.colPrefixCharacter.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colPrefixCharacter.AppearanceHeader.Font")));
            this.colPrefixCharacter.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colPrefixCharacter, "colPrefixCharacter");
            this.colPrefixCharacter.FieldName = "PREFIX_CHAR";
            this.colPrefixCharacter.Name = "colPrefixCharacter";
            // 
            // colSuffixCharacter
            // 
            this.colSuffixCharacter.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colSuffixCharacter.AppearanceHeader.Font")));
            this.colSuffixCharacter.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colSuffixCharacter, "colSuffixCharacter");
            this.colSuffixCharacter.FieldName = "SUFFIX_CHAR";
            this.colSuffixCharacter.Name = "colSuffixCharacter";
            // 
            // colMonth
            // 
            this.colMonth.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colMonth.AppearanceHeader.Font")));
            this.colMonth.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colMonth, "colMonth");
            this.colMonth.FieldName = "MONTH";
            this.colMonth.Name = "colMonth";
            // 
            // gvProjectView
            // 
            this.gvProjectView.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvProjectView.Appearance.FocusedRow.Font")));
            this.gvProjectView.Appearance.FocusedRow.Options.UseFont = true;
            this.gvProjectView.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvProjectView.Appearance.HeaderPanel.Font")));
            this.gvProjectView.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvProjectView.AppearancePrint.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvProjectView.AppearancePrint.HeaderPanel.Font")));
            this.gvProjectView.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvProjectView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvProjectView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProjectId,
            this.colClosedBy,
            this.colProjectCode,
            this.colProjectName,
            this.colProjectDivision,
            this.colStartedOn,
            this.colClosedOn,
            this.colDescription});
            this.gvProjectView.GridControl = this.gcProjectView;
            this.gvProjectView.Name = "gvProjectView";
            this.gvProjectView.OptionsBehavior.Editable = false;
            this.gvProjectView.OptionsPrint.PrintDetails = true;
            this.gvProjectView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvProjectView.OptionsView.ShowGroupPanel = false;
            this.gvProjectView.DoubleClick += new System.EventHandler(this.gvProjectView_DoubleClick);
            this.gvProjectView.RowCountChanged += new System.EventHandler(this.gvProjectView_RowCountChanged);
            // 
            // colProjectId
            // 
            this.colProjectId.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colProjectId.AppearanceHeader.Font")));
            this.colProjectId.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colProjectId, "colProjectId");
            this.colProjectId.FieldName = "PROJECT_ID";
            this.colProjectId.Name = "colProjectId";
            // 
            // colProjectCode
            // 
            this.colProjectCode.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colProjectCode.AppearanceHeader.Font")));
            this.colProjectCode.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colProjectCode, "colProjectCode");
            this.colProjectCode.FieldName = "PROJECT_CODE";
            this.colProjectCode.Name = "colProjectCode";
            this.colProjectCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colProjectName
            // 
            this.colProjectName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colProjectName.AppearanceHeader.Font")));
            this.colProjectName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colProjectName, "colProjectName");
            this.colProjectName.FieldName = "PROJECT";
            this.colProjectName.Name = "colProjectName";
            this.colProjectName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colProjectDivision
            // 
            this.colProjectDivision.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colProjectDivision.AppearanceHeader.Font")));
            this.colProjectDivision.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colProjectDivision, "colProjectDivision");
            this.colProjectDivision.FieldName = "DIVISION";
            this.colProjectDivision.Name = "colProjectDivision";
            this.colProjectDivision.OptionsColumn.AllowSize = false;
            this.colProjectDivision.OptionsColumn.FixedWidth = true;
            this.colProjectDivision.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colStartedOn
            // 
            this.colStartedOn.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colStartedOn.AppearanceHeader.Font")));
            this.colStartedOn.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colStartedOn, "colStartedOn");
            this.colStartedOn.DisplayFormat.FormatString = "d";
            this.colStartedOn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colStartedOn.FieldName = "DATE_STARTED";
            this.colStartedOn.Name = "colStartedOn";
            this.colStartedOn.OptionsColumn.AllowSize = false;
            this.colStartedOn.OptionsColumn.FixedWidth = true;
            this.colStartedOn.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colClosedOn
            // 
            this.colClosedOn.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colClosedOn.AppearanceHeader.Font")));
            this.colClosedOn.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colClosedOn, "colClosedOn");
            this.colClosedOn.DisplayFormat.FormatString = "d";
            this.colClosedOn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colClosedOn.FieldName = "DATE_CLOSED";
            this.colClosedOn.Name = "colClosedOn";
            this.colClosedOn.OptionsColumn.AllowSize = false;
            this.colClosedOn.OptionsColumn.FixedWidth = true;
            this.colClosedOn.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colDescription
            // 
            this.colDescription.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colDescription.AppearanceHeader.Font")));
            this.colDescription.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colDescription, "colDescription");
            this.colDescription.FieldName = "PROJECT_CATOGORY_NAME";
            this.colDescription.Name = "colDescription";
            this.colDescription.OptionsColumn.AllowSize = false;
            this.colDescription.OptionsColumn.FixedWidth = true;
            this.colDescription.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // pnlProjectToolBar
            // 
            this.pnlProjectToolBar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlProjectToolBar.Controls.Add(this.ucToolBarProjectview);
            resources.ApplyResources(this.pnlProjectToolBar, "pnlProjectToolBar");
            this.pnlProjectToolBar.Name = "pnlProjectToolBar";
            // 
            // ucToolBarProjectview
            // 
            this.ucToolBarProjectview.ChangeAddCaption = "&Add";
            this.ucToolBarProjectview.ChangeCaption = "&Edit";
            this.ucToolBarProjectview.ChangeDeleteCaption = "&Delete";
            this.ucToolBarProjectview.ChangeMoveVoucherCaption = "&Move Voucher";
            resources.ApplyResources(toolTipTitleItem1, "toolTipTitleItem1");
            toolTipItem1.LeftIndent = 6;
            resources.ApplyResources(toolTipItem1, "toolTipItem1");
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.ucToolBarProjectview.ChangeMoveVoucherTooltip = superToolTip1;
            this.ucToolBarProjectview.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucToolBarProjectview.ChangePostInterestCaption = "P&ost Interest";
            this.ucToolBarProjectview.ChangePrintCaption = "&Print";
            this.ucToolBarProjectview.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucToolBarProjectview.DisableAddButton = true;
            this.ucToolBarProjectview.DisableAMCRenew = true;
            this.ucToolBarProjectview.DisableCloseButton = true;
            this.ucToolBarProjectview.DisableDeleteButton = true;
            this.ucToolBarProjectview.DisableDownloadExcel = true;
            this.ucToolBarProjectview.DisableEditButton = true;
            this.ucToolBarProjectview.DisableInsertVoucher = true;
            this.ucToolBarProjectview.DisableMoveTransaction = true;
            this.ucToolBarProjectview.DisableNatureofPayments = true;
            this.ucToolBarProjectview.DisablePostInterest = true;
            this.ucToolBarProjectview.DisablePrintButton = true;
            this.ucToolBarProjectview.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucToolBarProjectview, "ucToolBarProjectview");
            this.ucToolBarProjectview.Name = "ucToolBarProjectview";
            this.ucToolBarProjectview.ShowHTML = true;
            this.ucToolBarProjectview.ShowMMT = true;
            this.ucToolBarProjectview.ShowPDF = true;
            this.ucToolBarProjectview.ShowRTF = true;
            this.ucToolBarProjectview.ShowText = true;
            this.ucToolBarProjectview.ShowXLS = true;
            this.ucToolBarProjectview.ShowXLSX = true;
            this.ucToolBarProjectview.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarProjectview.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarProjectview.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarProjectview.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarProjectview.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarProjectview.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarProjectview.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarProjectview.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarProjectview.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarProjectview.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarProjectview.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarProjectview.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarProjectview.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarProjectview.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarProjectview.AddClicked += new System.EventHandler(this.ucToolBarProjectview_AddClicked);
            this.ucToolBarProjectview.EditClicked += new System.EventHandler(this.ucToolBarProjectview_EditClicked);
            this.ucToolBarProjectview.DeleteClicked += new System.EventHandler(this.ucToolBarProjectview_DeleteClicked);
            this.ucToolBarProjectview.PrintClicked += new System.EventHandler(this.ucToolBarProjectview_PrintClicked);
            this.ucToolBarProjectview.CloseClicked += new System.EventHandler(this.ucToolBarProjectview_CloseClicked);
            this.ucToolBarProjectview.RefreshClicked += new System.EventHandler(this.ucToolBarProjectview_RefreshClicked);
            // 
            // pnlProjectView
            // 
            this.pnlProjectView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlProjectView.Controls.Add(this.pnlProjectDetails);
            this.pnlProjectView.Controls.Add(this.gcProjectView);
            this.pnlProjectView.Controls.Add(this.pnlProjectFooter);
            resources.ApplyResources(this.pnlProjectView, "pnlProjectView");
            this.pnlProjectView.Name = "pnlProjectView";
            // 
            // pnlProjectDetails
            // 
            this.pnlProjectDetails.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.pnlProjectDetails.Controls.Add(this.detProjectDateClosed);
            this.pnlProjectDetails.Controls.Add(this.btnSave);
            this.pnlProjectDetails.Controls.Add(this.lblProjectClosedDate);
            this.pnlProjectDetails.Controls.Add(this.lblProjectNameValue);
            this.pnlProjectDetails.Controls.Add(this.lblProjectName);
            this.pnlProjectDetails.Controls.Add(this.lblProjectTitle);
            this.pnlProjectDetails.Controls.Add(this.labelControl1);
            resources.ApplyResources(this.pnlProjectDetails, "pnlProjectDetails");
            this.pnlProjectDetails.Name = "pnlProjectDetails";
            // 
            // detProjectDateClosed
            // 
            resources.ApplyResources(this.detProjectDateClosed, "detProjectDateClosed");
            this.detProjectDateClosed.Name = "detProjectDateClosed";
            this.detProjectDateClosed.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.detProjectDateClosed.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("detProjectDateClosed.Properties.Buttons")))),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("detProjectDateClosed.Properties.Buttons1"))), resources.GetString("detProjectDateClosed.Properties.Buttons2"), ((int)(resources.GetObject("detProjectDateClosed.Properties.Buttons3"))), ((bool)(resources.GetObject("detProjectDateClosed.Properties.Buttons4"))), ((bool)(resources.GetObject("detProjectDateClosed.Properties.Buttons5"))), ((bool)(resources.GetObject("detProjectDateClosed.Properties.Buttons6"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("detProjectDateClosed.Properties.Buttons7"))), ((System.Drawing.Image)(resources.GetObject("detProjectDateClosed.Properties.Buttons8"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("detProjectDateClosed.Properties.Buttons9"), ((object)(resources.GetObject("detProjectDateClosed.Properties.Buttons10"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("detProjectDateClosed.Properties.Buttons11"))), ((bool)(resources.GetObject("detProjectDateClosed.Properties.Buttons12"))))});
            this.detProjectDateClosed.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.detProjectDateClosed.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("detProjectDateClosed.Properties.Mask.MaskType")));
            this.detProjectDateClosed.Properties.MaxLength = 50;
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblProjectClosedDate
            // 
            this.lblProjectClosedDate.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblProjectClosedDate.Appearance.Font")));
            resources.ApplyResources(this.lblProjectClosedDate, "lblProjectClosedDate");
            this.lblProjectClosedDate.Name = "lblProjectClosedDate";
            // 
            // lblProjectNameValue
            // 
            resources.ApplyResources(this.lblProjectNameValue, "lblProjectNameValue");
            this.lblProjectNameValue.Name = "lblProjectNameValue";
            // 
            // lblProjectName
            // 
            this.lblProjectName.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblProjectName.Appearance.Font")));
            resources.ApplyResources(this.lblProjectName, "lblProjectName");
            this.lblProjectName.Name = "lblProjectName";
            // 
            // lblProjectTitle
            // 
            this.lblProjectTitle.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblProjectTitle.Appearance.Font")));
            resources.ApplyResources(this.lblProjectTitle, "lblProjectTitle");
            this.lblProjectTitle.Name = "lblProjectTitle";
            // 
            // labelControl1
            // 
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl1.Appearance.Font")));
            this.labelControl1.Name = "labelControl1";
            // 
            // pnlProjectFooter
            // 
            this.pnlProjectFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlProjectFooter.Controls.Add(this.lblRecordCount);
            this.pnlProjectFooter.Controls.Add(this.lblRecordCountSymbol);
            this.pnlProjectFooter.Controls.Add(this.chkShowFilter);
            resources.ApplyResources(this.pnlProjectFooter, "pnlProjectFooter");
            this.pnlProjectFooter.Name = "pnlProjectFooter";
            // 
            // lblRecordCount
            // 
            resources.ApplyResources(this.lblRecordCount, "lblRecordCount");
            this.lblRecordCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblRecordCount.Appearance.Font")));
            this.lblRecordCount.Name = "lblRecordCount";
            // 
            // lblRecordCountSymbol
            // 
            resources.ApplyResources(this.lblRecordCountSymbol, "lblRecordCountSymbol");
            this.lblRecordCountSymbol.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblRecordCountSymbol.Appearance.Font")));
            this.lblRecordCountSymbol.Name = "lblRecordCountSymbol";
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // colClosedBy
            // 
            resources.ApplyResources(this.colClosedBy, "colClosedBy");
            this.colClosedBy.FieldName = "CLOSED_BY";
            this.colClosedBy.Name = "colClosedBy";
            // 
            // frmProjectView
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.pnlProjectView);
            this.Controls.Add(this.pnlProjectToolBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmProjectView";
            this.ShowFilterClicked += new System.EventHandler(this.frmProjectView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmProjectView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmProjectView_Activated);
            this.Load += new System.EventHandler(this.frmProjectView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcProjectView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvVoucher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProjectView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlProjectToolBar)).EndInit();
            this.pnlProjectToolBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlProjectView)).EndInit();
            this.pnlProjectView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlProjectDetails)).EndInit();
            this.pnlProjectDetails.ResumeLayout(false);
            this.pnlProjectDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.detProjectDateClosed.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detProjectDateClosed.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlProjectFooter)).EndInit();
            this.pnlProjectFooter.ResumeLayout(false);
            this.pnlProjectFooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion


     //   private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private DevExpress.XtraEditors.PanelControl pnlProjectToolBar;
        private Bosco.Utility.Controls.ucToolBar ucToolBarProjectview;
        private DevExpress.XtraEditors.PanelControl pnlProjectView;
        private DevExpress.XtraEditors.PanelControl pnlProjectFooter;
        private DevExpress.XtraGrid.GridControl gcProjectView;
        private DevExpress.XtraGrid.Views.Grid.GridView gvProjectView;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectId;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectCode;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectName;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectDivision;
        private DevExpress.XtraGrid.Columns.GridColumn colStartedOn;
        private DevExpress.XtraGrid.Columns.GridColumn colClosedOn;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
        private DevExpress.XtraEditors.LabelControl lblRecordCountSymbol;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLedger;
        private DevExpress.XtraGrid.Views.Grid.GridView gvVoucher;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherId;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherProject;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherName;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherType;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherMethod;
        private DevExpress.XtraGrid.Columns.GridColumn colPrefixCharacter;
        private DevExpress.XtraGrid.Columns.GridColumn colSuffixCharacter;
        private DevExpress.XtraGrid.Columns.GridColumn colMonth;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerProjectId;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerCode;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerName;
        private DevExpress.XtraGrid.Columns.GridColumn colGroupCode;
        private DevExpress.XtraGrid.Columns.GridColumn colGroupName;
        private DevExpress.XtraEditors.PanelControl pnlProjectDetails;
        private DevExpress.XtraEditors.LabelControl lblProjectTitle;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblProjectName;
        private DevExpress.XtraEditors.LabelControl lblProjectClosedDate;
        private DevExpress.XtraEditors.LabelControl lblProjectNameValue;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.DateEdit detProjectDateClosed;
        private DevExpress.XtraGrid.Columns.GridColumn colClosedBy;
    }
}