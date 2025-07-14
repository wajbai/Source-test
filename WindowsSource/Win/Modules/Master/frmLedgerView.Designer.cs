namespace ACPP.Modules.Master
{
    partial class frmLedgerView 
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
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLedgerView));
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.pnlBankToolBar = new DevExpress.XtraEditors.PanelControl();
            this.ucToolBarBankView = new Bosco.Utility.Controls.ucToolBar();
            this.pnlBankView = new DevExpress.XtraEditors.PanelControl();
            this.pnlGridControl = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gcLedgerView = new DevExpress.XtraGrid.GridControl();
            this.gvLedgerView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colClosedBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFDInvestmentType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNature = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerSubType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankAccountId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectCategoryName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsCC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsBankInterest = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsGST = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGSTType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGSTClass = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBudgetGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBudgetSubGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCloseDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.applicationSchema1 = new Bosco.DAO.Schema.ApplicationSchema();
            this.pnlBankFooter = new DevExpress.XtraEditors.PanelControl();
            this.lblBankRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.pnlLedgerDetails = new DevExpress.XtraEditors.PanelControl();
            this.detLedgerDateClosed = new DevExpress.XtraEditors.DateEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.lblProjectClosedDate = new DevExpress.XtraEditors.LabelControl();
            this.lblLedgerNameValue = new DevExpress.XtraEditors.LabelControl();
            this.lblLedgerName = new DevExpress.XtraEditors.LabelControl();
            this.lblLedgerNameTitle = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.colCurrencyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOpExchangeRate = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBankToolBar)).BeginInit();
            this.pnlBankToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBankView)).BeginInit();
            this.pnlBankView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridControl)).BeginInit();
            this.pnlGridControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcLedgerView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLedgerView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationSchema1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBankFooter)).BeginInit();
            this.pnlBankFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLedgerDetails)).BeginInit();
            this.pnlLedgerDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.detLedgerDateClosed.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detLedgerDateClosed.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBankToolBar
            // 
            this.pnlBankToolBar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlBankToolBar.Controls.Add(this.ucToolBarBankView);
            resources.ApplyResources(this.pnlBankToolBar, "pnlBankToolBar");
            this.pnlBankToolBar.Name = "pnlBankToolBar";
            // 
            // ucToolBarBankView
            // 
            this.ucToolBarBankView.ChangeAddCaption = "&Add";
            this.ucToolBarBankView.ChangeCaption = "&Edit";
            this.ucToolBarBankView.ChangeDeleteCaption = "&Delete";
            this.ucToolBarBankView.ChangeMoveVoucherCaption = "&Move Voucher";
            resources.ApplyResources(toolTipTitleItem1, "toolTipTitleItem1");
            toolTipItem1.LeftIndent = 6;
            resources.ApplyResources(toolTipItem1, "toolTipItem1");
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.ucToolBarBankView.ChangeMoveVoucherTooltip = superToolTip1;
            this.ucToolBarBankView.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucToolBarBankView.ChangePostInterestCaption = "P&ost Interest";
            toolTipTitleItem2.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            toolTipTitleItem2.Appearance.Options.UseImage = true;
            resources.ApplyResources(toolTipTitleItem2, "toolTipTitleItem2");
            toolTipItem2.LeftIndent = 6;
            resources.ApplyResources(toolTipItem2, "toolTipItem2");
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.ucToolBarBankView.ChangePostInterestSuperToolTip = superToolTip2;
            this.ucToolBarBankView.ChangePrintCaption = "&Print";
            this.ucToolBarBankView.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucToolBarBankView.DisableAddButton = true;
            this.ucToolBarBankView.DisableAMCRenew = true;
            this.ucToolBarBankView.DisableCloseButton = true;
            this.ucToolBarBankView.DisableDeleteButton = true;
            this.ucToolBarBankView.DisableDownloadExcel = true;
            this.ucToolBarBankView.DisableEditButton = true;
            this.ucToolBarBankView.DisableInsertVoucher = true;
            this.ucToolBarBankView.DisableMoveTransaction = true;
            this.ucToolBarBankView.DisableNatureofPayments = true;
            this.ucToolBarBankView.DisablePostInterest = true;
            this.ucToolBarBankView.DisablePrintButton = true;
            this.ucToolBarBankView.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucToolBarBankView, "ucToolBarBankView");
            this.ucToolBarBankView.Name = "ucToolBarBankView";
            this.ucToolBarBankView.ShowHTML = true;
            this.ucToolBarBankView.ShowMMT = true;
            this.ucToolBarBankView.ShowPDF = true;
            this.ucToolBarBankView.ShowRTF = true;
            this.ucToolBarBankView.ShowText = true;
            this.ucToolBarBankView.ShowXLS = true;
            this.ucToolBarBankView.ShowXLSX = true;
            this.ucToolBarBankView.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarBankView.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarBankView.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.AddClicked += new System.EventHandler(this.ucToolBarBankView_AddClicked);
            this.ucToolBarBankView.EditClicked += new System.EventHandler(this.ucToolBarBankView_EditClicked);
            this.ucToolBarBankView.DeleteClicked += new System.EventHandler(this.ucToolBarBankView_DeleteClicked);
            this.ucToolBarBankView.PrintClicked += new System.EventHandler(this.ucToolBarBankView_PrintClicked);
            this.ucToolBarBankView.CloseClicked += new System.EventHandler(this.ucToolBarBankView_CloseClicked);
            this.ucToolBarBankView.RefreshClicked += new System.EventHandler(this.ucToolBarBankView_RefreshClicked);
            // 
            // pnlBankView
            // 
            this.pnlBankView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlBankView.Controls.Add(this.pnlGridControl);
            resources.ApplyResources(this.pnlBankView, "pnlBankView");
            this.pnlBankView.Name = "pnlBankView";
            // 
            // pnlGridControl
            // 
            this.pnlGridControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlGridControl.Controls.Add(this.panelControl2);
            this.pnlGridControl.Controls.Add(this.pnlBankToolBar);
            resources.ApplyResources(this.pnlGridControl, "pnlGridControl");
            this.pnlGridControl.Name = "pnlGridControl";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.gcLedgerView);
            resources.ApplyResources(this.panelControl2, "panelControl2");
            this.panelControl2.Name = "panelControl2";
            // 
            // gcLedgerView
            // 
            resources.ApplyResources(this.gcLedgerView, "gcLedgerView");
            this.gcLedgerView.MainView = this.gvLedgerView;
            this.gcLedgerView.Name = "gcLedgerView";
            this.gcLedgerView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLedgerView});
            // 
            // gvLedgerView
            // 
            this.gvLedgerView.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvLedgerView.Appearance.FocusedRow.Font")));
            this.gvLedgerView.Appearance.FocusedRow.Options.UseFont = true;
            this.gvLedgerView.AppearancePrint.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvLedgerView.AppearancePrint.HeaderPanel.Font")));
            this.gvLedgerView.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvLedgerView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvLedgerView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLedgerId,
            this.colClosedBy,
            this.colLedgerCode,
            this.colLedgerName,
            this.colFDInvestmentType,
            this.colGroup,
            this.colNature,
            this.colCurrencyName,
            this.colOpExchangeRate,
            this.colLedgerSubType,
            this.colBankAccountId,
            this.colProjectCategoryName,
            this.colIsCC,
            this.colIsBankInterest,
            this.colIsGST,
            this.colGSTType,
            this.colGSTClass,
            this.colBudgetGroup,
            this.colBudgetSubGroup,
            this.colCloseDate});
            this.gvLedgerView.CustomizationFormBounds = new System.Drawing.Rectangle(677, 363, 216, 178);
            this.gvLedgerView.GridControl = this.gcLedgerView;
            this.gvLedgerView.Name = "gvLedgerView";
            this.gvLedgerView.OptionsBehavior.Editable = false;
            this.gvLedgerView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvLedgerView.OptionsView.ShowGroupPanel = false;
            this.gvLedgerView.DoubleClick += new System.EventHandler(this.gvLedgerView_DoubleClick);
            this.gvLedgerView.RowCountChanged += new System.EventHandler(this.gvLedgerView_RowCountChanged);
            // 
            // colLedgerId
            // 
            this.colLedgerId.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colLedgerId.AppearanceHeader.Font")));
            this.colLedgerId.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colLedgerId, "colLedgerId");
            this.colLedgerId.FieldName = "LEDGER_ID";
            this.colLedgerId.Name = "colLedgerId";
            // 
            // colClosedBy
            // 
            resources.ApplyResources(this.colClosedBy, "colClosedBy");
            this.colClosedBy.FieldName = "CLOSED_BY";
            this.colClosedBy.Name = "colClosedBy";
            // 
            // colLedgerCode
            // 
            this.colLedgerCode.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colLedgerCode.AppearanceHeader.Font")));
            this.colLedgerCode.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colLedgerCode, "colLedgerCode");
            this.colLedgerCode.FieldName = "LEDGER_CODE";
            this.colLedgerCode.Name = "colLedgerCode";
            this.colLedgerCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colLedgerName
            // 
            this.colLedgerName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colLedgerName.AppearanceHeader.Font")));
            this.colLedgerName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colLedgerName, "colLedgerName");
            this.colLedgerName.FieldName = "LEDGER_NAME";
            this.colLedgerName.Name = "colLedgerName";
            this.colLedgerName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colFDInvestmentType
            // 
            this.colFDInvestmentType.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colFDInvestmentType.AppearanceHeader.Font")));
            this.colFDInvestmentType.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colFDInvestmentType, "colFDInvestmentType");
            this.colFDInvestmentType.FieldName = "FD_INVESTMENT_TYPE";
            this.colFDInvestmentType.Name = "colFDInvestmentType";
            this.colFDInvestmentType.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colGroup
            // 
            this.colGroup.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colGroup.AppearanceHeader.Font")));
            this.colGroup.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colGroup, "colGroup");
            this.colGroup.FieldName = "GROUP";
            this.colGroup.Name = "colGroup";
            this.colGroup.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colNature
            // 
            this.colNature.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colNature.AppearanceHeader.Font")));
            this.colNature.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colNature, "colNature");
            this.colNature.FieldName = "NATURE";
            this.colNature.Name = "colNature";
            this.colNature.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colLedgerSubType
            // 
            resources.ApplyResources(this.colLedgerSubType, "colLedgerSubType");
            this.colLedgerSubType.FieldName = "LEDGER_SUB_TYPE";
            this.colLedgerSubType.Name = "colLedgerSubType";
            this.colLedgerSubType.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colBankAccountId
            // 
            resources.ApplyResources(this.colBankAccountId, "colBankAccountId");
            this.colBankAccountId.FieldName = "BANK_ACCOUNT_ID";
            this.colBankAccountId.Name = "colBankAccountId";
            this.colBankAccountId.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colProjectCategoryName
            // 
            this.colProjectCategoryName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colProjectCategoryName.AppearanceHeader.Font")));
            this.colProjectCategoryName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colProjectCategoryName, "colProjectCategoryName");
            this.colProjectCategoryName.FieldName = "PROJECT_CATOGORY_NAME";
            this.colProjectCategoryName.Name = "colProjectCategoryName";
            this.colProjectCategoryName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colIsCC
            // 
            this.colIsCC.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colIsCC.AppearanceHeader.Font")));
            this.colIsCC.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colIsCC, "colIsCC");
            this.colIsCC.FieldName = "IS_COST_CENTER";
            this.colIsCC.Name = "colIsCC";
            this.colIsCC.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colIsBankInterest
            // 
            this.colIsBankInterest.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colIsBankInterest.AppearanceHeader.Font")));
            this.colIsBankInterest.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colIsBankInterest, "colIsBankInterest");
            this.colIsBankInterest.FieldName = "IS_BANK_INTEREST_LEDGER";
            this.colIsBankInterest.Name = "colIsBankInterest";
            this.colIsBankInterest.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colIsGST
            // 
            this.colIsGST.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colIsGST.AppearanceHeader.Font")));
            this.colIsGST.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colIsGST, "colIsGST");
            this.colIsGST.FieldName = "IS_GST_LEDGERS";
            this.colIsGST.Name = "colIsGST";
            this.colIsGST.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colGSTType
            // 
            this.colGSTType.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colGSTType.AppearanceHeader.Font")));
            this.colGSTType.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colGSTType, "colGSTType");
            this.colGSTType.FieldName = "GST_SERVICE_TYPE";
            this.colGSTType.Name = "colGSTType";
            this.colGSTType.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colGSTClass
            // 
            this.colGSTClass.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colGSTClass.AppearanceHeader.Font")));
            this.colGSTClass.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colGSTClass, "colGSTClass");
            this.colGSTClass.FieldName = "SLAB";
            this.colGSTClass.Name = "colGSTClass";
            // 
            // colBudgetGroup
            // 
            this.colBudgetGroup.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colBudgetGroup.AppearanceHeader.Font")));
            this.colBudgetGroup.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colBudgetGroup, "colBudgetGroup");
            this.colBudgetGroup.FieldName = "BUDGET_GROUP";
            this.colBudgetGroup.Name = "colBudgetGroup";
            this.colBudgetGroup.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colBudgetSubGroup
            // 
            this.colBudgetSubGroup.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colBudgetSubGroup.AppearanceHeader.Font")));
            this.colBudgetSubGroup.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colBudgetSubGroup, "colBudgetSubGroup");
            this.colBudgetSubGroup.FieldName = "BUDGET_SUB_GROUP";
            this.colBudgetSubGroup.Name = "colBudgetSubGroup";
            this.colBudgetSubGroup.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colCloseDate
            // 
            this.colCloseDate.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colCloseDate.AppearanceHeader.Font")));
            this.colCloseDate.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colCloseDate, "colCloseDate");
            this.colCloseDate.FieldName = "DATE_CLOSED";
            this.colCloseDate.Name = "colCloseDate";
            this.colCloseDate.OptionsColumn.AllowEdit = false;
            this.colCloseDate.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colCloseDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colCloseDate.OptionsColumn.AllowMove = false;
            this.colCloseDate.OptionsColumn.ReadOnly = true;
            this.colCloseDate.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // applicationSchema1
            // 
            this.applicationSchema1.DataSetName = "ApplicationSchema";
            this.applicationSchema1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // pnlBankFooter
            // 
            this.pnlBankFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlBankFooter.Controls.Add(this.lblBankRecordCount);
            this.pnlBankFooter.Controls.Add(this.lblRecordCount);
            this.pnlBankFooter.Controls.Add(this.chkShowFilter);
            resources.ApplyResources(this.pnlBankFooter, "pnlBankFooter");
            this.pnlBankFooter.Name = "pnlBankFooter";
            // 
            // lblBankRecordCount
            // 
            resources.ApplyResources(this.lblBankRecordCount, "lblBankRecordCount");
            this.lblBankRecordCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblBankRecordCount.Appearance.Font")));
            this.lblBankRecordCount.Name = "lblBankRecordCount";
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
            // pnlLedgerDetails
            // 
            this.pnlLedgerDetails.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.pnlLedgerDetails.Controls.Add(this.detLedgerDateClosed);
            this.pnlLedgerDetails.Controls.Add(this.btnSave);
            this.pnlLedgerDetails.Controls.Add(this.lblProjectClosedDate);
            this.pnlLedgerDetails.Controls.Add(this.lblLedgerNameValue);
            this.pnlLedgerDetails.Controls.Add(this.lblLedgerName);
            this.pnlLedgerDetails.Controls.Add(this.lblLedgerNameTitle);
            this.pnlLedgerDetails.Controls.Add(this.labelControl1);
            resources.ApplyResources(this.pnlLedgerDetails, "pnlLedgerDetails");
            this.pnlLedgerDetails.Name = "pnlLedgerDetails";
            // 
            // detLedgerDateClosed
            // 
            resources.ApplyResources(this.detLedgerDateClosed, "detLedgerDateClosed");
            this.detLedgerDateClosed.Name = "detLedgerDateClosed";
            this.detLedgerDateClosed.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.detLedgerDateClosed.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("detLedgerDateClosed.Properties.Buttons")))),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("detLedgerDateClosed.Properties.Buttons1"))), resources.GetString("detLedgerDateClosed.Properties.Buttons2"), ((int)(resources.GetObject("detLedgerDateClosed.Properties.Buttons3"))), ((bool)(resources.GetObject("detLedgerDateClosed.Properties.Buttons4"))), ((bool)(resources.GetObject("detLedgerDateClosed.Properties.Buttons5"))), ((bool)(resources.GetObject("detLedgerDateClosed.Properties.Buttons6"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("detLedgerDateClosed.Properties.Buttons7"))), ((System.Drawing.Image)(resources.GetObject("detLedgerDateClosed.Properties.Buttons8"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("detLedgerDateClosed.Properties.Buttons9"), ((object)(resources.GetObject("detLedgerDateClosed.Properties.Buttons10"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("detLedgerDateClosed.Properties.Buttons11"))), ((bool)(resources.GetObject("detLedgerDateClosed.Properties.Buttons12"))))});
            this.detLedgerDateClosed.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.detLedgerDateClosed.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("detLedgerDateClosed.Properties.Mask.MaskType")));
            this.detLedgerDateClosed.Properties.MaxLength = 50;
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
            this.lblProjectClosedDate.Click += new System.EventHandler(this.lblProjectClosedDate_Click);
            // 
            // lblLedgerNameValue
            // 
            resources.ApplyResources(this.lblLedgerNameValue, "lblLedgerNameValue");
            this.lblLedgerNameValue.Name = "lblLedgerNameValue";
            // 
            // lblLedgerName
            // 
            this.lblLedgerName.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblLedgerName.Appearance.Font")));
            resources.ApplyResources(this.lblLedgerName, "lblLedgerName");
            this.lblLedgerName.Name = "lblLedgerName";
            // 
            // lblLedgerNameTitle
            // 
            this.lblLedgerNameTitle.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblLedgerNameTitle.Appearance.Font")));
            resources.ApplyResources(this.lblLedgerNameTitle, "lblLedgerNameTitle");
            this.lblLedgerNameTitle.Name = "lblLedgerNameTitle";
            // 
            // labelControl1
            // 
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl1.Appearance.Font")));
            this.labelControl1.Name = "labelControl1";
            // 
            // colCurrencyName
            // 
            this.colCurrencyName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("gridColumn1.AppearanceHeader.Font")));
            this.colCurrencyName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colCurrencyName, "colCurrencyName");
            this.colCurrencyName.FieldName = "CURRENCY_NAME";
            this.colCurrencyName.Name = "colCurrencyName";
            this.colCurrencyName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colOpExchangeRate
            // 
            this.colOpExchangeRate.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("gridColumn1.AppearanceHeader.Font1")));
            this.colOpExchangeRate.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colOpExchangeRate, "colOpExchangeRate");
            this.colOpExchangeRate.FieldName = "OP_EXCHANGE_RATE";
            this.colOpExchangeRate.Name = "colOpExchangeRate";
            this.colOpExchangeRate.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // frmLedgerView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlLedgerDetails);
            this.Controls.Add(this.pnlBankFooter);
            this.Controls.Add(this.pnlBankView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmLedgerView";
            this.ShowFilterClicked += new System.EventHandler(this.frmLedgerView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmLedgerView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmLedgerView_Activated);
            this.Load += new System.EventHandler(this.frmLedgerView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlBankToolBar)).EndInit();
            this.pnlBankToolBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlBankView)).EndInit();
            this.pnlBankView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridControl)).EndInit();
            this.pnlGridControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcLedgerView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLedgerView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationSchema1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBankFooter)).EndInit();
            this.pnlBankFooter.ResumeLayout(false);
            this.pnlBankFooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLedgerDetails)).EndInit();
            this.pnlLedgerDetails.ResumeLayout(false);
            this.pnlLedgerDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.detLedgerDateClosed.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detLedgerDateClosed.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlBankToolBar;
        private Bosco.Utility.Controls.ucToolBar ucToolBarBankView;
        private DevExpress.XtraEditors.PanelControl pnlBankView;
        private DevExpress.XtraGrid.GridControl gcLedgerView;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLedgerView;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerCode;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerName;
        private DevExpress.XtraGrid.Columns.GridColumn colGroup;
        private Bosco.DAO.Schema.ApplicationSchema applicationSchema1;
        private DevExpress.XtraEditors.PanelControl pnlGridControl;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerSubType;
        private DevExpress.XtraGrid.Columns.GridColumn colBankAccountId;
        private DevExpress.XtraGrid.Columns.GridColumn colBudgetGroup;
        private DevExpress.XtraGrid.Columns.GridColumn colBudgetSubGroup;
        private DevExpress.XtraGrid.Columns.GridColumn colIsCC;
        private DevExpress.XtraGrid.Columns.GridColumn colIsBankInterest;
        private DevExpress.XtraGrid.Columns.GridColumn colIsGST;
        private DevExpress.XtraGrid.Columns.GridColumn colGSTType;
        private DevExpress.XtraGrid.Columns.GridColumn colGSTClass;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectCategoryName;
        private DevExpress.XtraGrid.Columns.GridColumn colCloseDate;
        private DevExpress.XtraEditors.PanelControl pnlBankFooter;
        private DevExpress.XtraEditors.LabelControl lblBankRecordCount;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraEditors.PanelControl pnlLedgerDetails;
        private DevExpress.XtraEditors.DateEdit detLedgerDateClosed;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.LabelControl lblProjectClosedDate;
        private DevExpress.XtraEditors.LabelControl lblLedgerNameValue;
        private DevExpress.XtraEditors.LabelControl lblLedgerName;
        private DevExpress.XtraEditors.LabelControl lblLedgerNameTitle;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn colNature;
        private DevExpress.XtraGrid.Columns.GridColumn colClosedBy;
        private DevExpress.XtraGrid.Columns.GridColumn colFDInvestmentType;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyName;
        private DevExpress.XtraGrid.Columns.GridColumn colOpExchangeRate;

    }
}