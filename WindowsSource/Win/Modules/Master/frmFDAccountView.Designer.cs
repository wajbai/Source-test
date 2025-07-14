namespace ACPP.Modules.Master
{
    partial class frmFDAccountView
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
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFDAccountView));
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            this.pnlFDAccountHeader = new DevExpress.XtraEditors.PanelControl();
            this.ucToolBarFDAccountView = new Bosco.Utility.Controls.ucToolBar();
            this.pnlFDFooter = new DevExpress.XtraEditors.PanelControl();
            this.lblFDScheme = new DevExpress.XtraEditors.LabelControl();
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.cboFDScheme = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblTextRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.chkFDAccount = new DevExpress.XtraEditors.CheckEdit();
            this.pnlFDAccount = new DevExpress.XtraEditors.PanelControl();
            this.gcFDAccount = new DevExpress.XtraGrid.GridControl();
            this.gvFDAccount = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAccountId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFDAccountNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTransType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFDScheme = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBank = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFDInvestmentType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedOn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColCurrencyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrincipalAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaturedOn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTransMode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFDVoucherId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFDStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.barAndDockingController1 = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.dockFDHPanel = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.ucFDHistory1 = new ACPP.Modules.UIControls.UcFDHistory();
            this.colfdschemeid = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFDAccountHeader)).BeginInit();
            this.pnlFDAccountHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFDFooter)).BeginInit();
            this.pnlFDFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboFDScheme.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFDAccount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFDAccount)).BeginInit();
            this.pnlFDAccount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcFDAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFDAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).BeginInit();
            this.dockFDHPanel.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFDAccountHeader
            // 
            this.pnlFDAccountHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlFDAccountHeader.Controls.Add(this.ucToolBarFDAccountView);
            resources.ApplyResources(this.pnlFDAccountHeader, "pnlFDAccountHeader");
            this.pnlFDAccountHeader.Name = "pnlFDAccountHeader";
            // 
            // ucToolBarFDAccountView
            // 
            this.ucToolBarFDAccountView.ChangeAddCaption = "&Add";
            this.ucToolBarFDAccountView.ChangeCaption = "&Edit";
            this.ucToolBarFDAccountView.ChangeDeleteCaption = "&Delete";
            this.ucToolBarFDAccountView.ChangeMoveVoucherCaption = "&Move Voucher";
            resources.ApplyResources(toolTipTitleItem1, "toolTipTitleItem1");
            toolTipItem1.LeftIndent = 6;
            resources.ApplyResources(toolTipItem1, "toolTipItem1");
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.ucToolBarFDAccountView.ChangeMoveVoucherTooltip = superToolTip1;
            this.ucToolBarFDAccountView.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucToolBarFDAccountView.ChangePostInterestCaption = "P&ost Interest";
            toolTipTitleItem2.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            toolTipTitleItem2.Appearance.Options.UseImage = true;
            resources.ApplyResources(toolTipTitleItem2, "toolTipTitleItem2");
            toolTipItem2.LeftIndent = 6;
            resources.ApplyResources(toolTipItem2, "toolTipItem2");
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.ucToolBarFDAccountView.ChangePostInterestSuperToolTip = superToolTip2;
            this.ucToolBarFDAccountView.ChangePrintCaption = "&Print";
            this.ucToolBarFDAccountView.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucToolBarFDAccountView.DisableAddButton = true;
            this.ucToolBarFDAccountView.DisableAMCRenew = true;
            this.ucToolBarFDAccountView.DisableCloseButton = true;
            this.ucToolBarFDAccountView.DisableDeleteButton = true;
            this.ucToolBarFDAccountView.DisableDownloadExcel = true;
            this.ucToolBarFDAccountView.DisableEditButton = true;
            this.ucToolBarFDAccountView.DisableInsertVoucher = true;
            this.ucToolBarFDAccountView.DisableMoveTransaction = true;
            this.ucToolBarFDAccountView.DisableNatureofPayments = true;
            this.ucToolBarFDAccountView.DisablePostInterest = true;
            this.ucToolBarFDAccountView.DisablePrintButton = true;
            this.ucToolBarFDAccountView.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucToolBarFDAccountView, "ucToolBarFDAccountView");
            this.ucToolBarFDAccountView.Name = "ucToolBarFDAccountView";
            this.ucToolBarFDAccountView.ShowHTML = true;
            this.ucToolBarFDAccountView.ShowMMT = true;
            this.ucToolBarFDAccountView.ShowPDF = true;
            this.ucToolBarFDAccountView.ShowRTF = true;
            this.ucToolBarFDAccountView.ShowText = true;
            this.ucToolBarFDAccountView.ShowXLS = true;
            this.ucToolBarFDAccountView.ShowXLSX = true;
            this.ucToolBarFDAccountView.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarFDAccountView.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarFDAccountView.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarFDAccountView.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarFDAccountView.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarFDAccountView.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarFDAccountView.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarFDAccountView.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarFDAccountView.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarFDAccountView.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarFDAccountView.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarFDAccountView.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarFDAccountView.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarFDAccountView.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarFDAccountView.AddClicked += new System.EventHandler(this.ucToolBarFDAccountView_AddClicked);
            this.ucToolBarFDAccountView.EditClicked += new System.EventHandler(this.ucToolBarFDAccountView_EditClicked);
            this.ucToolBarFDAccountView.DeleteClicked += new System.EventHandler(this.ucToolBarFDAccountView_DeleteClicked);
            this.ucToolBarFDAccountView.PrintClicked += new System.EventHandler(this.ucToolBarFDAccountView_PrintClicked);
            this.ucToolBarFDAccountView.CloseClicked += new System.EventHandler(this.ucToolBarFDAccountView_CloseClicked);
            this.ucToolBarFDAccountView.RefreshClicked += new System.EventHandler(this.ucToolBarFDAccountView_RefreshClicked);
            // 
            // pnlFDFooter
            // 
            this.pnlFDFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlFDFooter.Controls.Add(this.lblFDScheme);
            this.pnlFDFooter.Controls.Add(this.btnUpdate);
            this.pnlFDFooter.Controls.Add(this.cboFDScheme);
            this.pnlFDFooter.Controls.Add(this.lblTextRecordCount);
            this.pnlFDFooter.Controls.Add(this.lblRecordCount);
            this.pnlFDFooter.Controls.Add(this.chkFDAccount);
            resources.ApplyResources(this.pnlFDFooter, "pnlFDFooter");
            this.pnlFDFooter.Name = "pnlFDFooter";
            // 
            // lblFDScheme
            // 
            this.lblFDScheme.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblFDScheme.Appearance.Font")));
            resources.ApplyResources(this.lblFDScheme, "lblFDScheme");
            this.lblFDScheme.Name = "lblFDScheme";
            // 
            // btnUpdate
            // 
            resources.ApplyResources(this.btnUpdate, "btnUpdate");
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // cboFDScheme
            // 
            resources.ApplyResources(this.cboFDScheme, "cboFDScheme");
            this.cboFDScheme.Name = "cboFDScheme";
            this.cboFDScheme.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.cboFDScheme.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cboFDScheme.Properties.Buttons"))))});
            this.cboFDScheme.Properties.Items.AddRange(new object[] {
            resources.GetString("cboFDScheme.Properties.Items"),
            resources.GetString("cboFDScheme.Properties.Items1")});
            this.cboFDScheme.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // lblTextRecordCount
            // 
            resources.ApplyResources(this.lblTextRecordCount, "lblTextRecordCount");
            this.lblTextRecordCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblTextRecordCount.Appearance.Font")));
            this.lblTextRecordCount.Name = "lblTextRecordCount";
            // 
            // lblRecordCount
            // 
            resources.ApplyResources(this.lblRecordCount, "lblRecordCount");
            this.lblRecordCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblRecordCount.Appearance.Font")));
            this.lblRecordCount.Name = "lblRecordCount";
            // 
            // chkFDAccount
            // 
            resources.ApplyResources(this.chkFDAccount, "chkFDAccount");
            this.chkFDAccount.Name = "chkFDAccount";
            this.chkFDAccount.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkFDAccount.Properties.Caption = resources.GetString("chkFDAccount.Properties.Caption");
            this.chkFDAccount.CheckedChanged += new System.EventHandler(this.chkFDAccount_CheckedChanged);
            // 
            // pnlFDAccount
            // 
            this.pnlFDAccount.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlFDAccount.Controls.Add(this.gcFDAccount);
            resources.ApplyResources(this.pnlFDAccount, "pnlFDAccount");
            this.pnlFDAccount.Name = "pnlFDAccount";
            // 
            // gcFDAccount
            // 
            resources.ApplyResources(this.gcFDAccount, "gcFDAccount");
            this.gcFDAccount.MainView = this.gvFDAccount;
            this.gcFDAccount.Name = "gcFDAccount";
            this.gcFDAccount.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvFDAccount});
            // 
            // gvFDAccount
            // 
            this.gvFDAccount.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvFDAccount.Appearance.FocusedRow.Font")));
            this.gvFDAccount.Appearance.FocusedRow.Options.UseFont = true;
            this.gvFDAccount.Appearance.FooterPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvFDAccount.Appearance.FooterPanel.Font")));
            this.gvFDAccount.Appearance.FooterPanel.Options.UseFont = true;
            this.gvFDAccount.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvFDAccount.Appearance.HeaderPanel.Font")));
            this.gvFDAccount.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvFDAccount.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAccountId,
            this.colFDAccountNumber,
            this.colTransType,
            this.colVNo,
            this.colFDScheme,
            this.colBank,
            this.colLedgerName,
            this.colFDInvestmentType,
            this.colProjectName,
            this.colCreatedOn,
            this.ColCurrencyName,
            this.colPrincipalAmount,
            this.colAmount,
            this.colMaturedOn,
            this.colProjectId,
            this.colLedgerId,
            this.colBankId,
            this.colStatus,
            this.colTransMode,
            this.colFDVoucherId,
            this.colFDStatus,
            this.gridColumn1,
            this.colfdschemeid});
            this.gvFDAccount.GridControl = this.gcFDAccount;
            this.gvFDAccount.Name = "gvFDAccount";
            this.gvFDAccount.OptionsBehavior.Editable = false;
            this.gvFDAccount.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvFDAccount.OptionsView.ShowGroupPanel = false;
            this.gvFDAccount.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvFDAccount_FocusedRowChanged);
            this.gvFDAccount.DoubleClick += new System.EventHandler(this.gvFDAccount_DoubleClick);
            this.gvFDAccount.RowCountChanged += new System.EventHandler(this.gvFDAccount_RowCountChanged);
            // 
            // colAccountId
            // 
            resources.ApplyResources(this.colAccountId, "colAccountId");
            this.colAccountId.FieldName = "FD_ACCOUNT_ID";
            this.colAccountId.Name = "colAccountId";
            // 
            // colFDAccountNumber
            // 
            resources.ApplyResources(this.colFDAccountNumber, "colFDAccountNumber");
            this.colFDAccountNumber.FieldName = "FD_ACCOUNT_NUMBER";
            this.colFDAccountNumber.Name = "colFDAccountNumber";
            this.colFDAccountNumber.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colTransType
            // 
            resources.ApplyResources(this.colTransType, "colTransType");
            this.colTransType.FieldName = "TRANS_TYPE";
            this.colTransType.Name = "colTransType";
            this.colTransType.OptionsColumn.FixedWidth = true;
            this.colTransType.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colVNo
            // 
            resources.ApplyResources(this.colVNo, "colVNo");
            this.colVNo.FieldName = "VOUCHER_NO";
            this.colVNo.Name = "colVNo";
            this.colVNo.OptionsColumn.FixedWidth = true;
            this.colVNo.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colFDScheme
            // 
            resources.ApplyResources(this.colFDScheme, "colFDScheme");
            this.colFDScheme.FieldName = "FD_SCHEME";
            this.colFDScheme.Name = "colFDScheme";
            this.colFDScheme.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colBank
            // 
            resources.ApplyResources(this.colBank, "colBank");
            this.colBank.FieldName = "BANK";
            this.colBank.Name = "colBank";
            this.colBank.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colLedgerName
            // 
            resources.ApplyResources(this.colLedgerName, "colLedgerName");
            this.colLedgerName.FieldName = "LEDGER_NAME";
            this.colLedgerName.Name = "colLedgerName";
            this.colLedgerName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colFDInvestmentType
            // 
            resources.ApplyResources(this.colFDInvestmentType, "colFDInvestmentType");
            this.colFDInvestmentType.FieldName = "INVESTMENT_TYPE";
            this.colFDInvestmentType.Name = "colFDInvestmentType";
            this.colFDInvestmentType.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colProjectName
            // 
            resources.ApplyResources(this.colProjectName, "colProjectName");
            this.colProjectName.FieldName = "PROJECT";
            this.colProjectName.Name = "colProjectName";
            this.colProjectName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colCreatedOn
            // 
            resources.ApplyResources(this.colCreatedOn, "colCreatedOn");
            this.colCreatedOn.DisplayFormat.FormatString = "d";
            this.colCreatedOn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colCreatedOn.FieldName = "INVESTMENT_DATE";
            this.colCreatedOn.Name = "colCreatedOn";
            this.colCreatedOn.OptionsColumn.FixedWidth = true;
            this.colCreatedOn.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // ColCurrencyName
            // 
            resources.ApplyResources(this.ColCurrencyName, "ColCurrencyName");
            this.ColCurrencyName.FieldName = "CURRENCY_NAME";
            this.ColCurrencyName.Name = "ColCurrencyName";
            this.ColCurrencyName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colPrincipalAmount
            // 
            this.colPrincipalAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.colPrincipalAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colPrincipalAmount, "colPrincipalAmount");
            this.colPrincipalAmount.DisplayFormat.FormatString = "n";
            this.colPrincipalAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPrincipalAmount.FieldName = "PRINCIPAL_AMOUNT";
            this.colPrincipalAmount.Name = "colPrincipalAmount";
            this.colPrincipalAmount.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colPrincipalAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colPrincipalAmount.Summary"))))});
            // 
            // colAmount
            // 
            this.colAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.colAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colAmount, "colAmount");
            this.colAmount.DisplayFormat.FormatString = "n";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "AMOUNT";
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.FixedWidth = true;
            this.colAmount.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colMaturedOn
            // 
            resources.ApplyResources(this.colMaturedOn, "colMaturedOn");
            this.colMaturedOn.DisplayFormat.FormatString = "d";
            this.colMaturedOn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colMaturedOn.FieldName = "MATURITY_DATE";
            this.colMaturedOn.Name = "colMaturedOn";
            this.colMaturedOn.OptionsColumn.FixedWidth = true;
            this.colMaturedOn.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colProjectId
            // 
            resources.ApplyResources(this.colProjectId, "colProjectId");
            this.colProjectId.FieldName = "PROJECT_ID";
            this.colProjectId.Name = "colProjectId";
            this.colProjectId.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colLedgerId
            // 
            resources.ApplyResources(this.colLedgerId, "colLedgerId");
            this.colLedgerId.FieldName = "LEDGER_ID";
            this.colLedgerId.Name = "colLedgerId";
            this.colLedgerId.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colBankId
            // 
            resources.ApplyResources(this.colBankId, "colBankId");
            this.colBankId.FieldName = "BANK_ID";
            this.colBankId.Name = "colBankId";
            this.colBankId.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colStatus
            // 
            resources.ApplyResources(this.colStatus, "colStatus");
            this.colStatus.FieldName = "CLOSING_STATUS";
            this.colStatus.Name = "colStatus";
            this.colStatus.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colTransMode
            // 
            resources.ApplyResources(this.colTransMode, "colTransMode");
            this.colTransMode.FieldName = "TRANS_MODE";
            this.colTransMode.Name = "colTransMode";
            this.colTransMode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colFDVoucherId
            // 
            resources.ApplyResources(this.colFDVoucherId, "colFDVoucherId");
            this.colFDVoucherId.FieldName = "FD_VOUCHER_ID";
            this.colFDVoucherId.Name = "colFDVoucherId";
            this.colFDVoucherId.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colFDStatus
            // 
            resources.ApplyResources(this.colFDStatus, "colFDStatus");
            this.colFDStatus.FieldName = "STATUS";
            this.colFDStatus.Name = "colFDStatus";
            this.colFDStatus.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // gridColumn1
            // 
            resources.ApplyResources(this.gridColumn1, "gridColumn1");
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
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
            this.dockFDHPanel.ID = new System.Guid("212c36f3-ad5a-4ff7-b32f-ff09718ecdaa");
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
            // colfdschemeid
            // 
            resources.ApplyResources(this.colfdschemeid, "colfdschemeid");
            this.colfdschemeid.FieldName = "FD_SCHEME_ID";
            this.colfdschemeid.Name = "colfdschemeid";
            // 
            // frmFDAccountView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlFDAccount);
            this.Controls.Add(this.pnlFDFooter);
            this.Controls.Add(this.pnlFDAccountHeader);
            this.Controls.Add(this.dockFDHPanel);
            this.Name = "frmFDAccountView";
            this.ShowFilterClicked += new System.EventHandler(this.frmFDAccountView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmFDAccountView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmFDAccountView_Activated);
            this.Load += new System.EventHandler(this.frmFDAccountView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlFDAccountHeader)).EndInit();
            this.pnlFDAccountHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlFDFooter)).EndInit();
            this.pnlFDFooter.ResumeLayout(false);
            this.pnlFDFooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboFDScheme.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFDAccount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFDAccount)).EndInit();
            this.pnlFDAccount.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcFDAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFDAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).EndInit();
            this.dockFDHPanel.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlFDAccountHeader;
        private Bosco.Utility.Controls.ucToolBar ucToolBarFDAccountView;
        private DevExpress.XtraEditors.PanelControl pnlFDFooter;
        private DevExpress.XtraEditors.PanelControl pnlFDAccount;
        private DevExpress.XtraGrid.GridControl gcFDAccount;
        private DevExpress.XtraGrid.Views.Grid.GridView gvFDAccount;
        private DevExpress.XtraEditors.LabelControl lblTextRecordCount;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
        private DevExpress.XtraEditors.CheckEdit chkFDAccount;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountId;
        private DevExpress.XtraGrid.Columns.GridColumn colFDAccountNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerName;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedOn;
        private DevExpress.XtraGrid.Columns.GridColumn colMaturedOn;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectId;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colBankId;
        private DevExpress.XtraGrid.Columns.GridColumn colBank;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colTransType;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colTransMode;
        private DevExpress.XtraGrid.Columns.GridColumn colFDVoucherId;
        private DevExpress.XtraGrid.Columns.GridColumn colFDStatus;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn colVNo;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController1;
        private DevExpress.XtraBars.Docking.DockPanel dockFDHPanel;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private UIControls.UcFDHistory ucFDHistory1;
        private DevExpress.XtraGrid.Columns.GridColumn colPrincipalAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colFDInvestmentType;
        private DevExpress.XtraGrid.Columns.GridColumn ColCurrencyName;
        private DevExpress.XtraGrid.Columns.GridColumn colFDScheme;
        private DevExpress.XtraEditors.ComboBoxEdit cboFDScheme;
        private DevExpress.XtraEditors.LabelControl lblFDScheme;
        private DevExpress.XtraEditors.SimpleButton btnUpdate;
        private DevExpress.XtraGrid.Columns.GridColumn colfdschemeid;
    }
}