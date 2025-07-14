namespace ACPP.Modules.Transaction
{
    partial class frmFDRegistersView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFDRegistersView));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            this.gcTransaction = new DevExpress.XtraGrid.GridControl();
            this.gvTransaction = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colFDAccountId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFDNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFDType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepositOn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaturityDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colClosedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInterestRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrincipalAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReinvestmentAmt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInterest = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccuredAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTdsAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPenaltyAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWithdrawalAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colClosingBalance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColProject_Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtCredit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.rbtnPrint = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.rbtnPrintVoucher = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.glkpCurrencyCountry = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gvCurrencyCountry = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCurrencyCountryId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencySymbol = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkpFDInvestmentType = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gvFDInvestmentType = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colInvestmentTypeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvestmentType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ucToolBar1 = new Bosco.Utility.Controls.ucToolBar();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.deDateFrom = new DevExpress.XtraEditors.DateEdit();
            this.glkpProject = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deTo = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem8 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcFDInvestmentType = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem7 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcCurrency = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem9 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.pnlOpeningBal = new DevExpress.XtraEditors.PanelControl();
            this.lblinterestrec = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblPrincipleAmount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblTotal = new DevExpress.XtraLayout.SimpleLabelItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblAccReceived = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblWithdraw = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblClosingBalance = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.simpleSeparator2 = new DevExpress.XtraLayout.SimpleSeparator();
            this.simpleSeparator3 = new DevExpress.XtraLayout.SimpleSeparator();
            this.simpleSeparator4 = new DevExpress.XtraLayout.SimpleSeparator();
            this.simpleSeparator5 = new DevExpress.XtraLayout.SimpleSeparator();
            this.simpleSeparator6 = new DevExpress.XtraLayout.SimpleSeparator();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.pnlClosingBal = new DevExpress.XtraEditors.PanelControl();
            this.pnlFooter = new DevExpress.XtraEditors.PanelControl();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.lblCount = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.pnlFill = new DevExpress.XtraEditors.PanelControl();
            this.chkContra = new DevExpress.XtraEditors.CheckEdit();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.deDateTo = new DevExpress.XtraEditors.DateEdit();
            this.simpleLabelItem2 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem3 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem4 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem5 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem6 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem7 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem8 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.barAndDockingController1 = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.dockFDHPanel = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.ucFDHistory1 = new ACPP.Modules.UIControls.UcFDHistory();
            this.colFDScheme = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gcTransaction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTransaction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtCredit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnPrintVoucher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkpCurrencyCountry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCurrencyCountry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpFDInvestmentType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFDInvestmentType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcFDInvestmentType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlOpeningBal)).BeginInit();
            this.pnlOpeningBal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblinterestrec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPrincipleAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAccReceived)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblWithdraw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblClosingBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlClosingBal)).BeginInit();
            this.pnlClosingBal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFooter)).BeginInit();
            this.pnlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFill)).BeginInit();
            this.pnlFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkContra.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).BeginInit();
            this.dockFDHPanel.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcTransaction
            // 
            resources.ApplyResources(this.gcTransaction, "gcTransaction");
            gridLevelNode1.RelationName = "Ledger";
            this.gcTransaction.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gcTransaction.MainView = this.gvTransaction;
            this.gcTransaction.Name = "gcTransaction";
            this.gcTransaction.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rtxtCredit,
            this.rbtnPrint,
            this.rbtnPrintVoucher});
            this.gcTransaction.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTransaction});
            // 
            // gvTransaction
            // 
            this.gvTransaction.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvTransaction.Appearance.FocusedRow.Font")));
            this.gvTransaction.Appearance.FocusedRow.Options.UseFont = true;
            this.gvTransaction.Appearance.FooterPanel.BackColor = ((System.Drawing.Color)(resources.GetObject("gvTransaction.Appearance.FooterPanel.BackColor")));
            this.gvTransaction.Appearance.FooterPanel.BackColor2 = ((System.Drawing.Color)(resources.GetObject("gvTransaction.Appearance.FooterPanel.BackColor2")));
            this.gvTransaction.Appearance.FooterPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvTransaction.Appearance.FooterPanel.Font")));
            this.gvTransaction.Appearance.FooterPanel.ForeColor = ((System.Drawing.Color)(resources.GetObject("gvTransaction.Appearance.FooterPanel.ForeColor")));
            this.gvTransaction.Appearance.FooterPanel.Options.UseBackColor = true;
            this.gvTransaction.Appearance.FooterPanel.Options.UseFont = true;
            this.gvTransaction.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvTransaction.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvTransaction.Appearance.HeaderPanel.Font")));
            this.gvTransaction.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvTransaction.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvTransaction.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colFDAccountId,
            this.colFDNo,
            this.colFDScheme,
            this.colFDType,
            this.colBankName,
            this.colProjectName,
            this.colDepositOn,
            this.colMaturityDate,
            this.colClosedDate,
            this.colInterestRate,
            this.colPrincipalAmount,
            this.colReinvestmentAmt,
            this.colInterest,
            this.colAccuredAmount,
            this.colTdsAmount,
            this.colPenaltyAmount,
            this.colTotalAmount,
            this.colWithdrawalAmount,
            this.colClosingBalance,
            this.colStatus,
            this.ColProject_Id});
            this.gvTransaction.GridControl = this.gcTransaction;
            this.gvTransaction.Name = "gvTransaction";
            this.gvTransaction.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvTransaction.OptionsView.ShowFooter = true;
            this.gvTransaction.OptionsView.ShowGroupPanel = false;
            this.gvTransaction.OptionsView.ShowIndicator = false;
            this.gvTransaction.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvTransaction_FocusedRowChanged);
            this.gvTransaction.RowCountChanged += new System.EventHandler(this.gvTransaction_RowCountChanged);
            // 
            // colFDAccountId
            // 
            resources.ApplyResources(this.colFDAccountId, "colFDAccountId");
            this.colFDAccountId.FieldName = "FD_ACCOUNT_ID";
            this.colFDAccountId.Name = "colFDAccountId";
            // 
            // colFDNo
            // 
            this.colFDNo.AppearanceCell.Options.UseTextOptions = true;
            this.colFDNo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colFDNo, "colFDNo");
            this.colFDNo.FieldName = "FD_ACCOUNT_NUMBER";
            this.colFDNo.Name = "colFDNo";
            this.colFDNo.OptionsColumn.AllowEdit = false;
            this.colFDNo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colFDNo.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colFDType
            // 
            resources.ApplyResources(this.colFDType, "colFDType");
            this.colFDType.FieldName = "TRANS_TYPE";
            this.colFDType.Name = "colFDType";
            this.colFDType.OptionsColumn.AllowEdit = false;
            this.colFDType.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colFDType.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colBankName
            // 
            this.colBankName.AppearanceCell.Options.UseTextOptions = true;
            this.colBankName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colBankName, "colBankName");
            this.colBankName.FieldName = "BANK";
            this.colBankName.Name = "colBankName";
            this.colBankName.OptionsColumn.AllowEdit = false;
            this.colBankName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colBankName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colProjectName
            // 
            resources.ApplyResources(this.colProjectName, "colProjectName");
            this.colProjectName.FieldName = "PROJECT";
            this.colProjectName.Name = "colProjectName";
            this.colProjectName.OptionsColumn.AllowEdit = false;
            this.colProjectName.OptionsColumn.AllowSize = false;
            this.colProjectName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colProjectName.OptionsColumn.FixedWidth = true;
            this.colProjectName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colDepositOn
            // 
            this.colDepositOn.AppearanceCell.Options.UseTextOptions = true;
            this.colDepositOn.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colDepositOn, "colDepositOn");
            this.colDepositOn.FieldName = "INVESTMENT_DATE";
            this.colDepositOn.GroupFormat.FormatString = "d";
            this.colDepositOn.GroupFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDepositOn.Name = "colDepositOn";
            this.colDepositOn.OptionsColumn.AllowEdit = false;
            this.colDepositOn.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDepositOn.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colMaturityDate
            // 
            this.colMaturityDate.AppearanceCell.Options.UseTextOptions = true;
            this.colMaturityDate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colMaturityDate, "colMaturityDate");
            this.colMaturityDate.FieldName = "MATURITY_DATE";
            this.colMaturityDate.GroupFormat.FormatString = "d";
            this.colMaturityDate.GroupFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colMaturityDate.Name = "colMaturityDate";
            this.colMaturityDate.OptionsColumn.AllowEdit = false;
            this.colMaturityDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colMaturityDate.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colClosedDate
            // 
            resources.ApplyResources(this.colClosedDate, "colClosedDate");
            this.colClosedDate.FieldName = "CLOSED_DATE";
            this.colClosedDate.Name = "colClosedDate";
            this.colClosedDate.OptionsColumn.AllowEdit = false;
            this.colClosedDate.OptionsColumn.AllowFocus = false;
            // 
            // colInterestRate
            // 
            this.colInterestRate.AppearanceCell.Options.UseTextOptions = true;
            this.colInterestRate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colInterestRate, "colInterestRate");
            this.colInterestRate.DisplayFormat.FormatString = "n";
            this.colInterestRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colInterestRate.FieldName = "INTEREST_RATE";
            this.colInterestRate.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colInterestRate.Name = "colInterestRate";
            this.colInterestRate.OptionsColumn.AllowEdit = false;
            this.colInterestRate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colInterestRate.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colPrincipalAmount
            // 
            this.colPrincipalAmount.AppearanceCell.Options.UseTextOptions = true;
            this.colPrincipalAmount.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colPrincipalAmount, "colPrincipalAmount");
            this.colPrincipalAmount.DisplayFormat.FormatString = "n";
            this.colPrincipalAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPrincipalAmount.FieldName = "PRINCIPLE_AMOUNT";
            this.colPrincipalAmount.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPrincipalAmount.Name = "colPrincipalAmount";
            this.colPrincipalAmount.OptionsColumn.AllowEdit = false;
            this.colPrincipalAmount.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colPrincipalAmount.OptionsFilter.AllowAutoFilter = false;
            this.colPrincipalAmount.OptionsFilter.AllowFilter = false;
            this.colPrincipalAmount.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colPrincipalAmount.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colPrincipalAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colPrincipalAmount.Summary"))), resources.GetString("colPrincipalAmount.Summary1"), resources.GetString("colPrincipalAmount.Summary2"))});
            // 
            // colReinvestmentAmt
            // 
            this.colReinvestmentAmt.AppearanceCell.Options.UseTextOptions = true;
            this.colReinvestmentAmt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            resources.ApplyResources(this.colReinvestmentAmt, "colReinvestmentAmt");
            this.colReinvestmentAmt.DisplayFormat.FormatString = "n";
            this.colReinvestmentAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colReinvestmentAmt.FieldName = "REINVESTED_AMOUNT";
            this.colReinvestmentAmt.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colReinvestmentAmt.Name = "colReinvestmentAmt";
            this.colReinvestmentAmt.OptionsColumn.AllowEdit = false;
            this.colReinvestmentAmt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colReinvestmentAmt.OptionsFilter.AllowAutoFilter = false;
            this.colReinvestmentAmt.OptionsFilter.AllowFilter = false;
            this.colReinvestmentAmt.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colReinvestmentAmt.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colReinvestmentAmt.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colReinvestmentAmt.Summary"))), resources.GetString("colReinvestmentAmt.Summary1"), resources.GetString("colReinvestmentAmt.Summary2"))});
            // 
            // colInterest
            // 
            this.colInterest.AppearanceCell.Options.UseTextOptions = true;
            this.colInterest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colInterest, "colInterest");
            this.colInterest.DisplayFormat.FormatString = "n";
            this.colInterest.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colInterest.FieldName = "INTEREST_AMOUNT";
            this.colInterest.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colInterest.Name = "colInterest";
            this.colInterest.OptionsColumn.AllowEdit = false;
            this.colInterest.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colInterest.OptionsFilter.AllowAutoFilter = false;
            this.colInterest.OptionsFilter.AllowFilter = false;
            this.colInterest.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colInterest.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colInterest.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colInterest.Summary"))), resources.GetString("colInterest.Summary1"), resources.GetString("colInterest.Summary2"))});
            // 
            // colAccuredAmount
            // 
            this.colAccuredAmount.AppearanceCell.Options.UseTextOptions = true;
            this.colAccuredAmount.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colAccuredAmount, "colAccuredAmount");
            this.colAccuredAmount.DisplayFormat.FormatString = "n";
            this.colAccuredAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAccuredAmount.FieldName = "ACCUMULATED_INTEREST_AMOUNT";
            this.colAccuredAmount.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAccuredAmount.Name = "colAccuredAmount";
            this.colAccuredAmount.OptionsColumn.AllowEdit = false;
            this.colAccuredAmount.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colAccuredAmount.OptionsFilter.AllowAutoFilter = false;
            this.colAccuredAmount.OptionsFilter.AllowFilter = false;
            this.colAccuredAmount.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colAccuredAmount.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colAccuredAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colAccuredAmount.Summary"))), resources.GetString("colAccuredAmount.Summary1"), resources.GetString("colAccuredAmount.Summary2"))});
            // 
            // colTdsAmount
            // 
            resources.ApplyResources(this.colTdsAmount, "colTdsAmount");
            this.colTdsAmount.DisplayFormat.FormatString = "n";
            this.colTdsAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTdsAmount.FieldName = "TDS_AMOUNT";
            this.colTdsAmount.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTdsAmount.Name = "colTdsAmount";
            this.colTdsAmount.OptionsColumn.AllowEdit = false;
            this.colTdsAmount.OptionsColumn.AllowFocus = false;
            this.colTdsAmount.OptionsFilter.AllowAutoFilter = false;
            this.colTdsAmount.OptionsFilter.AllowFilter = false;
            this.colTdsAmount.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colTdsAmount.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colTdsAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colTdsAmount.Summary"))), resources.GetString("colTdsAmount.Summary1"), resources.GetString("colTdsAmount.Summary2"))});
            // 
            // colPenaltyAmount
            // 
            resources.ApplyResources(this.colPenaltyAmount, "colPenaltyAmount");
            this.colPenaltyAmount.DisplayFormat.FormatString = "n";
            this.colPenaltyAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPenaltyAmount.FieldName = "CHARGE_AMOUNT";
            this.colPenaltyAmount.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPenaltyAmount.Name = "colPenaltyAmount";
            this.colPenaltyAmount.OptionsColumn.AllowEdit = false;
            this.colPenaltyAmount.OptionsColumn.AllowFocus = false;
            this.colPenaltyAmount.OptionsFilter.AllowAutoFilter = false;
            this.colPenaltyAmount.OptionsFilter.AllowFilter = false;
            this.colPenaltyAmount.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colPenaltyAmount.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colPenaltyAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colPenaltyAmount.Summary"))), resources.GetString("colPenaltyAmount.Summary1"), resources.GetString("colPenaltyAmount.Summary2"))});
            // 
            // colTotalAmount
            // 
            this.colTotalAmount.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalAmount.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colTotalAmount, "colTotalAmount");
            this.colTotalAmount.DisplayFormat.FormatString = "n";
            this.colTotalAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalAmount.FieldName = "TOTAL_AMOUNT";
            this.colTotalAmount.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalAmount.Name = "colTotalAmount";
            this.colTotalAmount.OptionsColumn.AllowEdit = false;
            this.colTotalAmount.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colTotalAmount.OptionsFilter.AllowAutoFilter = false;
            this.colTotalAmount.OptionsFilter.AllowFilter = false;
            this.colTotalAmount.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colTotalAmount.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colTotalAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colTotalAmount.Summary"))), resources.GetString("colTotalAmount.Summary1"), resources.GetString("colTotalAmount.Summary2"))});
            // 
            // colWithdrawalAmount
            // 
            this.colWithdrawalAmount.AppearanceCell.Options.UseTextOptions = true;
            this.colWithdrawalAmount.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colWithdrawalAmount, "colWithdrawalAmount");
            this.colWithdrawalAmount.DisplayFormat.FormatString = "n";
            this.colWithdrawalAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colWithdrawalAmount.FieldName = "WITHDRAWAL_AMOUNT";
            this.colWithdrawalAmount.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colWithdrawalAmount.Name = "colWithdrawalAmount";
            this.colWithdrawalAmount.OptionsColumn.AllowEdit = false;
            this.colWithdrawalAmount.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colWithdrawalAmount.OptionsFilter.AllowAutoFilter = false;
            this.colWithdrawalAmount.OptionsFilter.AllowFilter = false;
            this.colWithdrawalAmount.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colWithdrawalAmount.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colWithdrawalAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colWithdrawalAmount.Summary"))), resources.GetString("colWithdrawalAmount.Summary1"), resources.GetString("colWithdrawalAmount.Summary2"))});
            // 
            // colClosingBalance
            // 
            this.colClosingBalance.AppearanceCell.Options.UseTextOptions = true;
            this.colClosingBalance.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colClosingBalance, "colClosingBalance");
            this.colClosingBalance.DisplayFormat.FormatString = "n";
            this.colClosingBalance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colClosingBalance.FieldName = "BALANCE_AMOUNT";
            this.colClosingBalance.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colClosingBalance.Name = "colClosingBalance";
            this.colClosingBalance.OptionsColumn.AllowEdit = false;
            this.colClosingBalance.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colClosingBalance.OptionsFilter.AllowAutoFilter = false;
            this.colClosingBalance.OptionsFilter.AllowFilter = false;
            this.colClosingBalance.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colClosingBalance.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colClosingBalance.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colClosingBalance.Summary"))), resources.GetString("colClosingBalance.Summary1"), resources.GetString("colClosingBalance.Summary2"))});
            // 
            // colStatus
            // 
            this.colStatus.AppearanceCell.Options.UseTextOptions = true;
            this.colStatus.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colStatus, "colStatus");
            this.colStatus.FieldName = "CLOSING_STATUS";
            this.colStatus.Name = "colStatus";
            this.colStatus.OptionsColumn.AllowEdit = false;
            this.colStatus.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colStatus.OptionsColumn.FixedWidth = true;
            this.colStatus.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // ColProject_Id
            // 
            resources.ApplyResources(this.ColProject_Id, "ColProject_Id");
            this.ColProject_Id.FieldName = "PROJECT_ID";
            this.ColProject_Id.Name = "ColProject_Id";
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
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rbtnPrintVoucher.Buttons"))), resources.GetString("rbtnPrintVoucher.Buttons1"), ((int)(resources.GetObject("rbtnPrintVoucher.Buttons2"))), ((bool)(resources.GetObject("rbtnPrintVoucher.Buttons3"))), ((bool)(resources.GetObject("rbtnPrintVoucher.Buttons4"))), ((bool)(resources.GetObject("rbtnPrintVoucher.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rbtnPrintVoucher.Buttons6"))), global::ACPP.Properties.Resources.print1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, resources.GetString("rbtnPrintVoucher.Buttons7"), ((object)(resources.GetObject("rbtnPrintVoucher.Buttons8"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rbtnPrintVoucher.Buttons9"))), ((bool)(resources.GetObject("rbtnPrintVoucher.Buttons10"))))});
            this.rbtnPrintVoucher.Name = "rbtnPrintVoucher";
            // 
            // layoutControl1
            // 
            this.layoutControl1.AllowCustomizationMenu = false;
            this.layoutControl1.Controls.Add(this.glkpCurrencyCountry);
            this.layoutControl1.Controls.Add(this.glkpFDInvestmentType);
            this.layoutControl1.Controls.Add(this.ucToolBar1);
            this.layoutControl1.Controls.Add(this.btnApply);
            this.layoutControl1.Controls.Add(this.deDateFrom);
            this.layoutControl1.Controls.Add(this.glkpProject);
            this.layoutControl1.Controls.Add(this.deTo);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(338, 130, 371, 405);
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // glkpCurrencyCountry
            // 
            resources.ApplyResources(this.glkpCurrencyCountry, "glkpCurrencyCountry");
            this.glkpCurrencyCountry.Name = "glkpCurrencyCountry";
            this.glkpCurrencyCountry.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.glkpCurrencyCountry.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpCurrencyCountry.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpCurrencyCountry.Properties.Buttons"))))});
            this.glkpCurrencyCountry.Properties.NullText = resources.GetString("glkpCurrencyCountry.Properties.NullText");
            this.glkpCurrencyCountry.Properties.PopupFormMinSize = new System.Drawing.Size(89, 0);
            this.glkpCurrencyCountry.Properties.PopupFormSize = new System.Drawing.Size(0, 50);
            this.glkpCurrencyCountry.Properties.View = this.gvCurrencyCountry;
            this.glkpCurrencyCountry.StyleController = this.layoutControl1;
            // 
            // gvCurrencyCountry
            // 
            this.gvCurrencyCountry.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvCurrencyCountry.Appearance.FocusedRow.Font")));
            this.gvCurrencyCountry.Appearance.FocusedRow.Options.UseFont = true;
            this.gvCurrencyCountry.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCurrencyCountryId,
            this.colCurrency,
            this.colCurrencyName,
            this.colCurrencySymbol});
            this.gvCurrencyCountry.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gvCurrencyCountry.Name = "gvCurrencyCountry";
            this.gvCurrencyCountry.OptionsBehavior.Editable = false;
            this.gvCurrencyCountry.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvCurrencyCountry.OptionsView.RowAutoHeight = true;
            this.gvCurrencyCountry.OptionsView.ShowColumnHeaders = false;
            this.gvCurrencyCountry.OptionsView.ShowGroupPanel = false;
            this.gvCurrencyCountry.OptionsView.ShowIndicator = false;
            // 
            // colCurrencyCountryId
            // 
            resources.ApplyResources(this.colCurrencyCountryId, "colCurrencyCountryId");
            this.colCurrencyCountryId.FieldName = "COUNTRY_ID";
            this.colCurrencyCountryId.Name = "colCurrencyCountryId";
            // 
            // colCurrency
            // 
            resources.ApplyResources(this.colCurrency, "colCurrency");
            this.colCurrency.FieldName = "CURRENCY";
            this.colCurrency.Name = "colCurrency";
            // 
            // colCurrencyName
            // 
            resources.ApplyResources(this.colCurrencyName, "colCurrencyName");
            this.colCurrencyName.FieldName = "CURRENCY_NAME";
            this.colCurrencyName.Name = "colCurrencyName";
            // 
            // colCurrencySymbol
            // 
            resources.ApplyResources(this.colCurrencySymbol, "colCurrencySymbol");
            this.colCurrencySymbol.FieldName = "CURRENCY_SYMBOL";
            this.colCurrencySymbol.Name = "colCurrencySymbol";
            // 
            // glkpFDInvestmentType
            // 
            resources.ApplyResources(this.glkpFDInvestmentType, "glkpFDInvestmentType");
            this.glkpFDInvestmentType.Name = "glkpFDInvestmentType";
            this.glkpFDInvestmentType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpFDInvestmentType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpFDInvestmentType.Properties.Buttons"))))});
            this.glkpFDInvestmentType.Properties.ImmediatePopup = true;
            this.glkpFDInvestmentType.Properties.NullText = resources.GetString("glkpFDInvestmentType.Properties.NullText");
            this.glkpFDInvestmentType.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpFDInvestmentType.Properties.PopupFormMinSize = new System.Drawing.Size(269, 0);
            this.glkpFDInvestmentType.Properties.PopupFormSize = new System.Drawing.Size(269, 50);
            this.glkpFDInvestmentType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkpFDInvestmentType.Properties.View = this.gvFDInvestmentType;
            this.glkpFDInvestmentType.StyleController = this.layoutControl1;
            this.glkpFDInvestmentType.Tag = "PR";
            // 
            // gvFDInvestmentType
            // 
            this.gvFDInvestmentType.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvFDInvestmentType.Appearance.FocusedRow.Font")));
            this.gvFDInvestmentType.Appearance.FocusedRow.Options.UseFont = true;
            this.gvFDInvestmentType.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colInvestmentTypeId,
            this.colInvestmentType});
            this.gvFDInvestmentType.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gvFDInvestmentType.Name = "gvFDInvestmentType";
            this.gvFDInvestmentType.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvFDInvestmentType.OptionsView.ShowColumnHeaders = false;
            this.gvFDInvestmentType.OptionsView.ShowGroupPanel = false;
            this.gvFDInvestmentType.OptionsView.ShowIndicator = false;
            // 
            // colInvestmentTypeId
            // 
            resources.ApplyResources(this.colInvestmentTypeId, "colInvestmentTypeId");
            this.colInvestmentTypeId.FieldName = "INVESTMENT_TYPE_ID";
            this.colInvestmentTypeId.Name = "colInvestmentTypeId";
            // 
            // colInvestmentType
            // 
            resources.ApplyResources(this.colInvestmentType, "colInvestmentType");
            this.colInvestmentType.FieldName = "INVESTMENT_TYPE";
            this.colInvestmentType.Name = "colInvestmentType";
            // 
            // ucToolBar1
            // 
            this.ucToolBar1.ChangeAddCaption = "&Add";
            this.ucToolBar1.ChangeCaption = "&Change Project";
            this.ucToolBar1.ChangeDeleteCaption = "&Delete FD";
            this.ucToolBar1.ChangeMoveVoucherCaption = "&Move Voucher";
            resources.ApplyResources(toolTipTitleItem1, "toolTipTitleItem1");
            toolTipItem1.LeftIndent = 6;
            resources.ApplyResources(toolTipItem1, "toolTipItem1");
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.ucToolBar1.ChangeMoveVoucherTooltip = superToolTip1;
            this.ucToolBar1.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucToolBar1.ChangePostInterestCaption = "P&ost Interest";
            toolTipTitleItem2.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            toolTipTitleItem2.Appearance.Options.UseImage = true;
            resources.ApplyResources(toolTipTitleItem2, "toolTipTitleItem2");
            toolTipItem2.LeftIndent = 6;
            resources.ApplyResources(toolTipItem2, "toolTipItem2");
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.ucToolBar1.ChangePostInterestSuperToolTip = superToolTip2;
            this.ucToolBar1.ChangePrintCaption = "&Print";
            this.ucToolBar1.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucToolBar1.DisableAddButton = true;
            this.ucToolBar1.DisableAMCRenew = true;
            this.ucToolBar1.DisableCloseButton = true;
            this.ucToolBar1.DisableDeleteButton = true;
            this.ucToolBar1.DisableDownloadExcel = true;
            this.ucToolBar1.DisableEditButton = true;
            this.ucToolBar1.DisableInsertVoucher = true;
            this.ucToolBar1.DisableMoveTransaction = true;
            this.ucToolBar1.DisableNatureofPayments = true;
            this.ucToolBar1.DisablePostInterest = true;
            this.ucToolBar1.DisablePrintButton = true;
            this.ucToolBar1.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucToolBar1, "ucToolBar1");
            this.ucToolBar1.Name = "ucToolBar1";
            this.ucToolBar1.ShowHTML = true;
            this.ucToolBar1.ShowMMT = true;
            this.ucToolBar1.ShowPDF = true;
            this.ucToolBar1.ShowRTF = true;
            this.ucToolBar1.ShowText = true;
            this.ucToolBar1.ShowXLS = true;
            this.ucToolBar1.ShowXLSX = true;
            this.ucToolBar1.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.EditClicked += new System.EventHandler(this.ucToolBar1_EditClicked);
            this.ucToolBar1.DeleteClicked += new System.EventHandler(this.ucToolBar1_DeleteClicked);
            this.ucToolBar1.PrintClicked += new System.EventHandler(this.ucToolBar1_PrintClicked);
            this.ucToolBar1.CloseClicked += new System.EventHandler(this.ucToolBar1_CloseClicked);
            this.ucToolBar1.RefreshClicked += new System.EventHandler(this.ucToolBar1_RefreshClicked);
            // 
            // btnApply
            // 
            resources.ApplyResources(this.btnApply, "btnApply");
            this.btnApply.Name = "btnApply";
            this.btnApply.StyleController = this.layoutControl1;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // deDateFrom
            // 
            resources.ApplyResources(this.deDateFrom, "deDateFrom");
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
            this.glkpProject.Name = "glkpProject";
            this.glkpProject.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpProject.Properties.Buttons"))))});
            this.glkpProject.Properties.ImmediatePopup = true;
            this.glkpProject.Properties.NullText = resources.GetString("glkpProject.Properties.NullText");
            this.glkpProject.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpProject.Properties.PopupFormMinSize = new System.Drawing.Size(269, 0);
            this.glkpProject.Properties.PopupFormSize = new System.Drawing.Size(269, 50);
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
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.emptySpaceItem4,
            this.emptySpaceItem8,
            this.layoutControlItem6,
            this.emptySpaceItem6,
            this.layoutControlItem3,
            this.emptySpaceItem3,
            this.lcFDInvestmentType,
            this.emptySpaceItem7,
            this.lcCurrency,
            this.emptySpaceItem9});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1028, 72);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlItem1.AppearanceItemCaption.Font")));
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.Control = this.glkpProject;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 41);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(108, 27);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 5, 5, 2);
            this.layoutControlItem1.Size = new System.Drawing.Size(299, 31);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(41, 13);
            this.layoutControlItem1.TextToControlDistance = 10;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlItem2.AppearanceItemCaption.Font")));
            this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem2.Control = this.deDateFrom;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(309, 41);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(160, 31);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(160, 31);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 5, 2);
            this.layoutControlItem2.Size = new System.Drawing.Size(160, 31);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(62, 13);
            this.layoutControlItem2.TextToControlDistance = 5;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnApply;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(969, 41);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(49, 31);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(49, 31);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 5, 4);
            this.layoutControlItem4.Size = new System.Drawing.Size(49, 31);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem4, "emptySpaceItem4");
            this.emptySpaceItem4.Location = new System.Drawing.Point(1018, 41);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(10, 31);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem8
            // 
            this.emptySpaceItem8.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem8, "emptySpaceItem8");
            this.emptySpaceItem8.Location = new System.Drawing.Point(469, 41);
            this.emptySpaceItem8.Name = "emptySpaceItem8";
            this.emptySpaceItem8.Size = new System.Drawing.Size(10, 31);
            this.emptySpaceItem8.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.ucToolBar1;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(1028, 41);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem6
            // 
            this.emptySpaceItem6.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem6, "emptySpaceItem6");
            this.emptySpaceItem6.Location = new System.Drawing.Point(299, 41);
            this.emptySpaceItem6.Name = "emptySpaceItem6";
            this.emptySpaceItem6.Size = new System.Drawing.Size(10, 31);
            this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlItem3.AppearanceItemCaption.Font")));
            this.layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem3.Control = this.deTo;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(479, 41);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(160, 31);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(160, 31);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 5, 2);
            this.layoutControlItem3.Size = new System.Drawing.Size(160, 31);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(50, 13);
            this.layoutControlItem3.TextToControlDistance = 5;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem3, "emptySpaceItem3");
            this.emptySpaceItem3.Location = new System.Drawing.Point(955, 41);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(14, 31);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcFDInvestmentType
            // 
            this.lcFDInvestmentType.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lcFDInvestmentType.AppearanceItemCaption.Font")));
            this.lcFDInvestmentType.AppearanceItemCaption.Options.UseFont = true;
            this.lcFDInvestmentType.Control = this.glkpFDInvestmentType;
            this.lcFDInvestmentType.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.lcFDInvestmentType, "lcFDInvestmentType");
            this.lcFDInvestmentType.Location = new System.Drawing.Point(649, 41);
            this.lcFDInvestmentType.Name = "lcFDInvestmentType";
            this.lcFDInvestmentType.Size = new System.Drawing.Size(155, 31);
            this.lcFDInvestmentType.TextSize = new System.Drawing.Size(98, 13);
            this.lcFDInvestmentType.TrimClientAreaToControl = false;
            // 
            // emptySpaceItem7
            // 
            this.emptySpaceItem7.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem7, "emptySpaceItem7");
            this.emptySpaceItem7.Location = new System.Drawing.Point(639, 41);
            this.emptySpaceItem7.Name = "emptySpaceItem7";
            this.emptySpaceItem7.Size = new System.Drawing.Size(10, 31);
            this.emptySpaceItem7.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcCurrency
            // 
            this.lcCurrency.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lcCurrency.AppearanceItemCaption.Font")));
            this.lcCurrency.AppearanceItemCaption.Options.UseFont = true;
            this.lcCurrency.Control = this.glkpCurrencyCountry;
            this.lcCurrency.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.lcCurrency, "lcCurrency");
            this.lcCurrency.Location = new System.Drawing.Point(814, 41);
            this.lcCurrency.Name = "lcCurrency";
            this.lcCurrency.Size = new System.Drawing.Size(141, 31);
            this.lcCurrency.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lcCurrency.TextSize = new System.Drawing.Size(51, 13);
            this.lcCurrency.TextToControlDistance = 5;
            this.lcCurrency.TrimClientAreaToControl = false;
            // 
            // emptySpaceItem9
            // 
            this.emptySpaceItem9.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem9, "emptySpaceItem9");
            this.emptySpaceItem9.Location = new System.Drawing.Point(804, 41);
            this.emptySpaceItem9.Name = "emptySpaceItem9";
            this.emptySpaceItem9.Size = new System.Drawing.Size(10, 31);
            this.emptySpaceItem9.TextSize = new System.Drawing.Size(0, 0);
            // 
            // pnlOpeningBal
            // 
            this.pnlOpeningBal.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlOpeningBal.Controls.Add(this.layoutControl1);
            resources.ApplyResources(this.pnlOpeningBal, "pnlOpeningBal");
            this.pnlOpeningBal.Name = "pnlOpeningBal";
            // 
            // lblinterestrec
            // 
            this.lblinterestrec.AllowHide = false;
            this.lblinterestrec.AllowHotTrack = false;
            this.lblinterestrec.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblinterestrec.AppearanceItemCaption.Font")));
            this.lblinterestrec.AppearanceItemCaption.ForeColor = ((System.Drawing.Color)(resources.GetObject("lblinterestrec.AppearanceItemCaption.ForeColor")));
            this.lblinterestrec.AppearanceItemCaption.Options.UseFont = true;
            this.lblinterestrec.AppearanceItemCaption.Options.UseForeColor = true;
            resources.ApplyResources(this.lblinterestrec, "lblinterestrec");
            this.lblinterestrec.Location = new System.Drawing.Point(476, 0);
            this.lblinterestrec.MaxSize = new System.Drawing.Size(0, 24);
            this.lblinterestrec.MinSize = new System.Drawing.Size(29, 24);
            this.lblinterestrec.Name = "lblinterestrec";
            this.lblinterestrec.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.lblinterestrec.Size = new System.Drawing.Size(78, 24);
            this.lblinterestrec.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblinterestrec.TextSize = new System.Drawing.Size(17, 11);
            // 
            // lblPrincipleAmount
            // 
            this.lblPrincipleAmount.AllowHotTrack = false;
            this.lblPrincipleAmount.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblPrincipleAmount.AppearanceItemCaption.Font")));
            this.lblPrincipleAmount.AppearanceItemCaption.ForeColor = ((System.Drawing.Color)(resources.GetObject("lblPrincipleAmount.AppearanceItemCaption.ForeColor")));
            this.lblPrincipleAmount.AppearanceItemCaption.Options.UseFont = true;
            this.lblPrincipleAmount.AppearanceItemCaption.Options.UseForeColor = true;
            this.lblPrincipleAmount.ControlAlignment = System.Drawing.ContentAlignment.MiddleRight;
            resources.ApplyResources(this.lblPrincipleAmount, "lblPrincipleAmount");
            this.lblPrincipleAmount.Location = new System.Drawing.Point(381, 0);
            this.lblPrincipleAmount.MaxSize = new System.Drawing.Size(0, 24);
            this.lblPrincipleAmount.MinSize = new System.Drawing.Size(29, 24);
            this.lblPrincipleAmount.Name = "lblPrincipleAmount";
            this.lblPrincipleAmount.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.lblPrincipleAmount.Size = new System.Drawing.Size(93, 24);
            this.lblPrincipleAmount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblPrincipleAmount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblPrincipleAmount.TextSize = new System.Drawing.Size(24, 11);
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem1.AppearanceItemCaption.Font")));
            this.simpleLabelItem1.AppearanceItemCaption.ForeColor = ((System.Drawing.Color)(resources.GetObject("simpleLabelItem1.AppearanceItemCaption.ForeColor")));
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseForeColor = true;
            resources.ApplyResources(this.simpleLabelItem1, "simpleLabelItem1");
            this.simpleLabelItem1.Location = new System.Drawing.Point(122, 0);
            this.simpleLabelItem1.MaxSize = new System.Drawing.Size(0, 24);
            this.simpleLabelItem1.MinSize = new System.Drawing.Size(71, 24);
            this.simpleLabelItem1.Name = "lblOpeningBal";
            this.simpleLabelItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.simpleLabelItem1.Size = new System.Drawing.Size(257, 24);
            this.simpleLabelItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(71, 14);
            // 
            // layoutControlGroup3
            // 
            resources.ApplyResources(this.layoutControlGroup3, "layoutControlGroup3");
            this.layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup3.GroupBordersVisible = false;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.simpleLabelItem1,
            this.lblPrincipleAmount,
            this.lblinterestrec,
            this.lblTotal,
            this.emptySpaceItem5,
            this.lblAccReceived,
            this.lblWithdraw,
            this.lblClosingBalance,
            this.simpleSeparator1,
            this.simpleSeparator2,
            this.simpleSeparator3,
            this.simpleSeparator4,
            this.simpleSeparator5,
            this.simpleSeparator6,
            this.emptySpaceItem2});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "Root";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(1024, 24);
            this.layoutControlGroup3.TextVisible = false;
            // 
            // lblTotal
            // 
            this.lblTotal.AllowHotTrack = false;
            this.lblTotal.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblTotal.AppearanceItemCaption.Font")));
            this.lblTotal.AppearanceItemCaption.ForeColor = ((System.Drawing.Color)(resources.GetObject("lblTotal.AppearanceItemCaption.ForeColor")));
            this.lblTotal.AppearanceItemCaption.Options.UseFont = true;
            this.lblTotal.AppearanceItemCaption.Options.UseForeColor = true;
            resources.ApplyResources(this.lblTotal, "lblTotal");
            this.lblTotal.Location = new System.Drawing.Point(646, 0);
            this.lblTotal.MaxSize = new System.Drawing.Size(0, 24);
            this.lblTotal.MinSize = new System.Drawing.Size(29, 24);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.lblTotal.Size = new System.Drawing.Size(94, 24);
            this.lblTotal.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblTotal.TextSize = new System.Drawing.Size(17, 11);
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem5, "emptySpaceItem5");
            this.emptySpaceItem5.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem5.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(122, 24);
            this.emptySpaceItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblAccReceived
            // 
            this.lblAccReceived.AllowHotTrack = false;
            this.lblAccReceived.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblAccReceived.AppearanceItemCaption.Font")));
            this.lblAccReceived.AppearanceItemCaption.ForeColor = ((System.Drawing.Color)(resources.GetObject("lblAccReceived.AppearanceItemCaption.ForeColor")));
            this.lblAccReceived.AppearanceItemCaption.Options.UseFont = true;
            this.lblAccReceived.AppearanceItemCaption.Options.UseForeColor = true;
            resources.ApplyResources(this.lblAccReceived, "lblAccReceived");
            this.lblAccReceived.Location = new System.Drawing.Point(556, 0);
            this.lblAccReceived.MaxSize = new System.Drawing.Size(0, 24);
            this.lblAccReceived.MinSize = new System.Drawing.Size(28, 24);
            this.lblAccReceived.Name = "lblAccReceived";
            this.lblAccReceived.Size = new System.Drawing.Size(88, 24);
            this.lblAccReceived.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblAccReceived.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblAccReceived.TextSize = new System.Drawing.Size(17, 11);
            // 
            // lblWithdraw
            // 
            this.lblWithdraw.AllowHotTrack = false;
            this.lblWithdraw.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblWithdraw.AppearanceItemCaption.Font")));
            this.lblWithdraw.AppearanceItemCaption.ForeColor = ((System.Drawing.Color)(resources.GetObject("lblWithdraw.AppearanceItemCaption.ForeColor")));
            this.lblWithdraw.AppearanceItemCaption.Options.UseFont = true;
            this.lblWithdraw.AppearanceItemCaption.Options.UseForeColor = true;
            resources.ApplyResources(this.lblWithdraw, "lblWithdraw");
            this.lblWithdraw.Location = new System.Drawing.Point(742, 0);
            this.lblWithdraw.MaxSize = new System.Drawing.Size(0, 24);
            this.lblWithdraw.MinSize = new System.Drawing.Size(21, 24);
            this.lblWithdraw.Name = "lblWithdraw";
            this.lblWithdraw.Size = new System.Drawing.Size(106, 24);
            this.lblWithdraw.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblWithdraw.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblWithdraw.TextSize = new System.Drawing.Size(17, 13);
            // 
            // lblClosingBalance
            // 
            this.lblClosingBalance.AllowHotTrack = false;
            this.lblClosingBalance.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblClosingBalance.AppearanceItemCaption.Font")));
            this.lblClosingBalance.AppearanceItemCaption.ForeColor = ((System.Drawing.Color)(resources.GetObject("lblClosingBalance.AppearanceItemCaption.ForeColor")));
            this.lblClosingBalance.AppearanceItemCaption.Options.UseFont = true;
            this.lblClosingBalance.AppearanceItemCaption.Options.UseForeColor = true;
            resources.ApplyResources(this.lblClosingBalance, "lblClosingBalance");
            this.lblClosingBalance.ImageAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblClosingBalance.Location = new System.Drawing.Point(850, 0);
            this.lblClosingBalance.MaxSize = new System.Drawing.Size(0, 24);
            this.lblClosingBalance.MinSize = new System.Drawing.Size(21, 24);
            this.lblClosingBalance.Name = "lblClosingBalance";
            this.lblClosingBalance.Size = new System.Drawing.Size(95, 24);
            this.lblClosingBalance.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblClosingBalance.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblClosingBalance.TextSize = new System.Drawing.Size(17, 11);
            // 
            // simpleSeparator1
            // 
            this.simpleSeparator1.AllowHotTrack = false;
            resources.ApplyResources(this.simpleSeparator1, "simpleSeparator1");
            this.simpleSeparator1.Location = new System.Drawing.Point(474, 0);
            this.simpleSeparator1.Name = "simpleSeparator1";
            this.simpleSeparator1.Size = new System.Drawing.Size(2, 24);
            // 
            // simpleSeparator2
            // 
            this.simpleSeparator2.AllowHotTrack = false;
            resources.ApplyResources(this.simpleSeparator2, "simpleSeparator2");
            this.simpleSeparator2.Location = new System.Drawing.Point(379, 0);
            this.simpleSeparator2.Name = "simpleSeparator2";
            this.simpleSeparator2.Size = new System.Drawing.Size(2, 24);
            // 
            // simpleSeparator3
            // 
            this.simpleSeparator3.AllowHotTrack = false;
            resources.ApplyResources(this.simpleSeparator3, "simpleSeparator3");
            this.simpleSeparator3.Location = new System.Drawing.Point(554, 0);
            this.simpleSeparator3.Name = "simpleSeparator3";
            this.simpleSeparator3.Size = new System.Drawing.Size(2, 24);
            // 
            // simpleSeparator4
            // 
            this.simpleSeparator4.AllowHotTrack = false;
            resources.ApplyResources(this.simpleSeparator4, "simpleSeparator4");
            this.simpleSeparator4.Location = new System.Drawing.Point(644, 0);
            this.simpleSeparator4.Name = "simpleSeparator4";
            this.simpleSeparator4.Size = new System.Drawing.Size(2, 24);
            // 
            // simpleSeparator5
            // 
            this.simpleSeparator5.AllowHotTrack = false;
            resources.ApplyResources(this.simpleSeparator5, "simpleSeparator5");
            this.simpleSeparator5.Location = new System.Drawing.Point(740, 0);
            this.simpleSeparator5.Name = "simpleSeparator5";
            this.simpleSeparator5.Size = new System.Drawing.Size(2, 24);
            // 
            // simpleSeparator6
            // 
            this.simpleSeparator6.AllowHotTrack = false;
            resources.ApplyResources(this.simpleSeparator6, "simpleSeparator6");
            this.simpleSeparator6.Location = new System.Drawing.Point(848, 0);
            this.simpleSeparator6.Name = "simpleSeparator6";
            this.simpleSeparator6.Size = new System.Drawing.Size(2, 24);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(945, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(79, 24);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControl3
            // 
            this.layoutControl3.AllowCustomizationMenu = false;
            resources.ApplyResources(this.layoutControl3, "layoutControl3");
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(0, 146, 250, 350);
            this.layoutControl3.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl3.Root = this.layoutControlGroup3;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.gcTransaction);
            resources.ApplyResources(this.panelControl1, "panelControl1");
            this.panelControl1.Name = "panelControl1";
            // 
            // pnlClosingBal
            // 
            this.pnlClosingBal.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.pnlClosingBal.Controls.Add(this.layoutControl3);
            resources.ApplyResources(this.pnlClosingBal, "pnlClosingBal");
            this.pnlClosingBal.Name = "pnlClosingBal";
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
            // pnlFill
            // 
            this.pnlFill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlFill.Controls.Add(this.panelControl1);
            this.pnlFill.Controls.Add(this.pnlClosingBal);
            this.pnlFill.Controls.Add(this.pnlOpeningBal);
            this.pnlFill.Controls.Add(this.pnlFooter);
            resources.ApplyResources(this.pnlFill, "pnlFill");
            this.pnlFill.Name = "pnlFill";
            // 
            // chkContra
            // 
            resources.ApplyResources(this.chkContra, "chkContra");
            this.chkContra.Name = "chkContra";
            this.chkContra.Properties.Caption = resources.GetString("chkContra.Properties.Caption");
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(912, 30);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // deDateTo
            // 
            resources.ApplyResources(this.deDateTo, "deDateTo");
            this.deDateTo.Name = "deDateTo";
            this.deDateTo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deDateTo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.deDateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("deDateTo.Properties.Buttons"))))});
            this.deDateTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deDateTo.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("deDateTo.Properties.Mask.MaskType")));
            this.deDateTo.StyleController = this.layoutControl1;
            // 
            // simpleLabelItem2
            // 
            this.simpleLabelItem2.AllowHotTrack = false;
            this.simpleLabelItem2.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem2.AppearanceItemCaption.Font")));
            this.simpleLabelItem2.AppearanceItemCaption.ForeColor = ((System.Drawing.Color)(resources.GetObject("simpleLabelItem2.AppearanceItemCaption.ForeColor")));
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseForeColor = true;
            resources.ApplyResources(this.simpleLabelItem2, "simpleLabelItem2");
            this.simpleLabelItem2.Location = new System.Drawing.Point(464, 0);
            this.simpleLabelItem2.Name = "lblBankClosingAmt";
            this.simpleLabelItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.simpleLabelItem2.Size = new System.Drawing.Size(102, 24);
            this.simpleLabelItem2.TextSize = new System.Drawing.Size(24, 13);
            // 
            // simpleLabelItem3
            // 
            this.simpleLabelItem3.AllowHotTrack = false;
            this.simpleLabelItem3.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem3.AppearanceItemCaption.Font")));
            this.simpleLabelItem3.AppearanceItemCaption.ForeColor = ((System.Drawing.Color)(resources.GetObject("simpleLabelItem3.AppearanceItemCaption.ForeColor")));
            this.simpleLabelItem3.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem3.AppearanceItemCaption.Options.UseForeColor = true;
            resources.ApplyResources(this.simpleLabelItem3, "simpleLabelItem3");
            this.simpleLabelItem3.Location = new System.Drawing.Point(358, 0);
            this.simpleLabelItem3.Name = "lblCashClosingAmt";
            this.simpleLabelItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.simpleLabelItem3.Size = new System.Drawing.Size(105, 24);
            this.simpleLabelItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItem3.TextSize = new System.Drawing.Size(24, 13);
            // 
            // simpleLabelItem4
            // 
            this.simpleLabelItem4.AllowHotTrack = false;
            this.simpleLabelItem4.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem4.AppearanceItemCaption.Font")));
            this.simpleLabelItem4.AppearanceItemCaption.ForeColor = ((System.Drawing.Color)(resources.GetObject("simpleLabelItem4.AppearanceItemCaption.ForeColor")));
            this.simpleLabelItem4.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem4.AppearanceItemCaption.Options.UseForeColor = true;
            resources.ApplyResources(this.simpleLabelItem4, "simpleLabelItem4");
            this.simpleLabelItem4.Location = new System.Drawing.Point(358, 0);
            this.simpleLabelItem4.Name = "lblCashClosingAmt";
            this.simpleLabelItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.simpleLabelItem4.Size = new System.Drawing.Size(105, 24);
            this.simpleLabelItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItem4.TextSize = new System.Drawing.Size(24, 13);
            // 
            // simpleLabelItem5
            // 
            this.simpleLabelItem5.AllowHotTrack = false;
            this.simpleLabelItem5.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem5.AppearanceItemCaption.Font")));
            this.simpleLabelItem5.AppearanceItemCaption.ForeColor = ((System.Drawing.Color)(resources.GetObject("simpleLabelItem5.AppearanceItemCaption.ForeColor")));
            this.simpleLabelItem5.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem5.AppearanceItemCaption.Options.UseForeColor = true;
            resources.ApplyResources(this.simpleLabelItem5, "simpleLabelItem5");
            this.simpleLabelItem5.Location = new System.Drawing.Point(358, 0);
            this.simpleLabelItem5.Name = "lblCashClosingAmt";
            this.simpleLabelItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.simpleLabelItem5.Size = new System.Drawing.Size(105, 24);
            this.simpleLabelItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItem5.TextSize = new System.Drawing.Size(24, 13);
            // 
            // simpleLabelItem6
            // 
            this.simpleLabelItem6.AllowHotTrack = false;
            this.simpleLabelItem6.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem6.AppearanceItemCaption.Font")));
            this.simpleLabelItem6.AppearanceItemCaption.ForeColor = ((System.Drawing.Color)(resources.GetObject("simpleLabelItem6.AppearanceItemCaption.ForeColor")));
            this.simpleLabelItem6.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem6.AppearanceItemCaption.Options.UseForeColor = true;
            resources.ApplyResources(this.simpleLabelItem6, "simpleLabelItem6");
            this.simpleLabelItem6.Location = new System.Drawing.Point(358, 0);
            this.simpleLabelItem6.Name = "lblCashClosingAmt";
            this.simpleLabelItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.simpleLabelItem6.Size = new System.Drawing.Size(105, 24);
            this.simpleLabelItem6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItem6.TextSize = new System.Drawing.Size(24, 13);
            // 
            // simpleLabelItem7
            // 
            this.simpleLabelItem7.AllowHotTrack = false;
            this.simpleLabelItem7.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem7.AppearanceItemCaption.Font")));
            this.simpleLabelItem7.AppearanceItemCaption.ForeColor = ((System.Drawing.Color)(resources.GetObject("simpleLabelItem7.AppearanceItemCaption.ForeColor")));
            this.simpleLabelItem7.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem7.AppearanceItemCaption.Options.UseForeColor = true;
            resources.ApplyResources(this.simpleLabelItem7, "simpleLabelItem7");
            this.simpleLabelItem7.Location = new System.Drawing.Point(358, 0);
            this.simpleLabelItem7.Name = "lblCashClosingAmt";
            this.simpleLabelItem7.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.simpleLabelItem7.Size = new System.Drawing.Size(105, 24);
            this.simpleLabelItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItem7.TextSize = new System.Drawing.Size(24, 13);
            // 
            // simpleLabelItem8
            // 
            this.simpleLabelItem8.AllowHotTrack = false;
            this.simpleLabelItem8.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem8.AppearanceItemCaption.Font")));
            this.simpleLabelItem8.AppearanceItemCaption.ForeColor = ((System.Drawing.Color)(resources.GetObject("simpleLabelItem8.AppearanceItemCaption.ForeColor")));
            this.simpleLabelItem8.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem8.AppearanceItemCaption.Options.UseForeColor = true;
            resources.ApplyResources(this.simpleLabelItem8, "simpleLabelItem8");
            this.simpleLabelItem8.Location = new System.Drawing.Point(358, 0);
            this.simpleLabelItem8.Name = "lblCashClosingAmt";
            this.simpleLabelItem8.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.simpleLabelItem8.Size = new System.Drawing.Size(105, 24);
            this.simpleLabelItem8.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItem8.TextSize = new System.Drawing.Size(24, 13);
            // 
            // dockManager1
            // 
            this.dockManager1.Controller = this.barAndDockingController1;
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockFDHPanel});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            this.dockManager1.ActivePanelChanged += new DevExpress.XtraBars.Docking.ActivePanelChangedEventHandler(this.dockManager1_ActivePanelChanged);
            // 
            // barAndDockingController1
            // 
            this.barAndDockingController1.AppearancesDocking.FloatFormCaption.Font = ((System.Drawing.Font)(resources.GetObject("barAndDockingController1.AppearancesDocking.FloatFormCaption.Font")));
            this.barAndDockingController1.AppearancesDocking.FloatFormCaption.Options.UseFont = true;
            this.barAndDockingController1.AppearancesDocking.FloatFormCaptionActive.Font = ((System.Drawing.Font)(resources.GetObject("barAndDockingController1.AppearancesDocking.FloatFormCaptionActive.Font")));
            this.barAndDockingController1.AppearancesDocking.FloatFormCaptionActive.Options.UseFont = true;
            this.barAndDockingController1.AppearancesDocking.HideContainer.Font = ((System.Drawing.Font)(resources.GetObject("barAndDockingController1.AppearancesDocking.HideContainer.Font")));
            this.barAndDockingController1.AppearancesDocking.HideContainer.Options.UseFont = true;
            this.barAndDockingController1.AppearancesDocking.HidePanelButton.Font = ((System.Drawing.Font)(resources.GetObject("barAndDockingController1.AppearancesDocking.HidePanelButton.Font")));
            this.barAndDockingController1.AppearancesDocking.HidePanelButton.Options.UseFont = true;
            this.barAndDockingController1.AppearancesDocking.HidePanelButtonActive.Font = ((System.Drawing.Font)(resources.GetObject("barAndDockingController1.AppearancesDocking.HidePanelButtonActive.Font")));
            this.barAndDockingController1.AppearancesDocking.HidePanelButtonActive.Options.UseFont = true;
            this.barAndDockingController1.AppearancesDocking.PanelCaption.Font = ((System.Drawing.Font)(resources.GetObject("barAndDockingController1.AppearancesDocking.PanelCaption.Font")));
            this.barAndDockingController1.AppearancesDocking.PanelCaption.Options.UseFont = true;
            this.barAndDockingController1.AppearancesDocking.PanelCaptionActive.Font = ((System.Drawing.Font)(resources.GetObject("barAndDockingController1.AppearancesDocking.PanelCaptionActive.Font")));
            this.barAndDockingController1.AppearancesDocking.PanelCaptionActive.Options.UseFont = true;
            this.barAndDockingController1.AppearancesDocking.Tabs.Font = ((System.Drawing.Font)(resources.GetObject("barAndDockingController1.AppearancesDocking.Tabs.Font")));
            this.barAndDockingController1.AppearancesDocking.Tabs.Options.UseFont = true;
            this.barAndDockingController1.PropertiesBar.DefaultGlyphSize = new System.Drawing.Size(16, 16);
            this.barAndDockingController1.PropertiesBar.DefaultLargeGlyphSize = new System.Drawing.Size(32, 32);
            // 
            // dockFDHPanel
            // 
            this.dockFDHPanel.Controls.Add(this.dockPanel1_Container);
            this.dockFDHPanel.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dockFDHPanel.ID = new System.Guid("3ebead52-c432-4f74-9020-d86d6aa1a9fd");
            resources.ApplyResources(this.dockFDHPanel, "dockFDHPanel");
            this.dockFDHPanel.Name = "dockFDHPanel";
            this.dockFDHPanel.Options.AllowDockAsTabbedDocument = false;
            this.dockFDHPanel.Options.AllowDockLeft = false;
            this.dockFDHPanel.Options.AllowDockRight = false;
            this.dockFDHPanel.Options.AllowDockTop = false;
            this.dockFDHPanel.Options.AllowFloating = false;
            this.dockFDHPanel.Options.FloatOnDblClick = false;
            this.dockFDHPanel.Options.ShowCloseButton = false;
            this.dockFDHPanel.Options.ShowMaximizeButton = false;
            this.dockFDHPanel.OriginalSize = new System.Drawing.Size(200, 150);
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.ucFDHistory1);
            resources.ApplyResources(this.dockPanel1_Container, "dockPanel1_Container");
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            // 
            // ucFDHistory1
            // 
            resources.ApplyResources(this.ucFDHistory1, "ucFDHistory1");
            this.ucFDHistory1.FDAccountId = 0;
            this.ucFDHistory1.Name = "ucFDHistory1";
            this.ucFDHistory1.ShowPanelCaptionHeader = true;
            // 
            // colFDScheme
            // 
            resources.ApplyResources(this.colFDScheme, "colFDScheme");
            this.colFDScheme.FieldName = "FD_SCHEME";
            this.colFDScheme.Name = "colFDScheme";
            this.colFDScheme.OptionsColumn.AllowEdit = false;
            this.colFDScheme.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colFDScheme.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // frmFDRegistersView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlFill);
            this.Controls.Add(this.dockFDHPanel);
            this.Name = "frmFDRegistersView";
            this.ShowFilterClicked += new System.EventHandler(this.frmFDRegistersView_ShowFilterClicked);
            this.Load += new System.EventHandler(this.frmFDRegistersView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcTransaction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTransaction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtCredit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnPrintVoucher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.glkpCurrencyCountry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCurrencyCountry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpFDInvestmentType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFDInvestmentType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcFDInvestmentType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlOpeningBal)).EndInit();
            this.pnlOpeningBal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lblinterestrec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPrincipleAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAccReceived)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblWithdraw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblClosingBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlClosingBal)).EndInit();
            this.pnlClosingBal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlFooter)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFill)).EndInit();
            this.pnlFill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkContra.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).EndInit();
            this.dockFDHPanel.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcTransaction;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTransaction;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtCredit;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnPrintVoucher;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnPrint;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.PanelControl pnlOpeningBal;
        private DevExpress.XtraLayout.SimpleLabelItem lblinterestrec;
        private DevExpress.XtraLayout.SimpleLabelItem lblPrincipleAmount;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.SimpleLabelItem lblTotal;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl pnlClosingBal;
        private DevExpress.XtraEditors.PanelControl pnlFooter;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraEditors.PanelControl pnlFill;
        private DevExpress.XtraEditors.CheckEdit chkContra;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.DateEdit deDateTo;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraGrid.Columns.GridColumn colFDNo;
        private DevExpress.XtraGrid.Columns.GridColumn colBankName;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectName;
        private DevExpress.XtraGrid.Columns.GridColumn colDepositOn;
        private DevExpress.XtraGrid.Columns.GridColumn colMaturityDate;
        private DevExpress.XtraGrid.Columns.GridColumn colInterestRate;
        private DevExpress.XtraGrid.Columns.GridColumn colPrincipalAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colInterest;
        private DevExpress.XtraGrid.Columns.GridColumn colAccuredAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colWithdrawalAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colClosingBalance;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraEditors.GridLookUpEdit glkpProject;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectId;
        private DevExpress.XtraGrid.Columns.GridColumn colProject;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.DateEdit deDateFrom;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.DateEdit deTo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SimpleButton btnApply;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem8;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
        private DevExpress.XtraEditors.LabelControl lblCount;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem2;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem3;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem4;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem5;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem6;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem7;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem8;
        private DevExpress.XtraLayout.SimpleLabelItem lblAccReceived;
        private DevExpress.XtraLayout.SimpleLabelItem lblWithdraw;
        private DevExpress.XtraLayout.SimpleLabelItem lblClosingBalance;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator2;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator3;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator4;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator5;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private Bosco.Utility.Controls.ucToolBar ucToolBar1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private DevExpress.XtraGrid.Columns.GridColumn colClosedDate;
        private DevExpress.XtraGrid.Columns.GridColumn colTdsAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colFDAccountId;
        private DevExpress.XtraGrid.Columns.GridColumn ColProject_Id;
        private DevExpress.XtraGrid.Columns.GridColumn colReinvestmentAmt;
        private DevExpress.XtraGrid.Columns.GridColumn colPenaltyAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colFDType;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockFDHPanel;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController1;
        private UIControls.UcFDHistory ucFDHistory1;
        private DevExpress.XtraEditors.GridLookUpEdit glkpFDInvestmentType;
        private DevExpress.XtraGrid.Views.Grid.GridView gvFDInvestmentType;
        private DevExpress.XtraGrid.Columns.GridColumn colInvestmentTypeId;
        private DevExpress.XtraGrid.Columns.GridColumn colInvestmentType;
        private DevExpress.XtraLayout.LayoutControlItem lcFDInvestmentType;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem7;
        private DevExpress.XtraEditors.GridLookUpEdit glkpCurrencyCountry;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCurrencyCountry;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyCountryId;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyName;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencySymbol;
        private DevExpress.XtraLayout.LayoutControlItem lcCurrency;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem9;
        private DevExpress.XtraGrid.Columns.GridColumn colFDScheme;
    }
}