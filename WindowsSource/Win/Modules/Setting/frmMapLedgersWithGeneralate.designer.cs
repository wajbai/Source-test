namespace ACPP.Modules.Master
{
    partial class frmMapLedgersWithGeneralate
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
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem3 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnShowMapLedgers = new DevExpress.XtraEditors.SimpleButton();
            this.btnMapLedgers = new DevExpress.XtraEditors.SimpleButton();
            this.btnFAUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.btnLoadFromFinance = new DevExpress.XtraEditors.SimpleButton();
            this.gcCYFADepreciation = new DevExpress.XtraGrid.GridControl();
            this.gvCYFADepreciation = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colFALedgerid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFALedgerCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFALedger = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFAConOpAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFAConDebit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtDebit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colFAConCredit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtCredit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colFAConCLBalance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repLKPDepreciationLedger = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cboTransMode = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtOpeningIEBalance = new DevExpress.XtraEditors.TextEdit();
            this.gcLedger = new DevExpress.XtraGrid.GridControl();
            this.gvLedger = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkLedgerSelect = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colLedgerCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNature = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOPAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtOpBalance = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colOPAmountTransMode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcboType = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.glkpGeneralateLedgers = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gvColConLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColConLedgerCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColConLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcFACancel = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgFixedAssetDetails = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcFixedAssetLedgers = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblFixedAssetTotalCaption = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblDepreciationTotalCaption = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblDepreciationAccuTotalCaption = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblFixedAssetOP = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblDepreciationOP = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblDepreciationAccuOP = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblFixedAssetDR = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblDepreciationDR = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblDepreciationAccuDR = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblFixedAssetCR = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblDepreciationCR = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblDepreciationAccuCR = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblFixedAssetTotal = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblDepreciationTotal = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblDepreciationAccuTotal = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lcResetfromFinance = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcFAUpdate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcShowMapLedgers = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleLabelItem2 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lcgMapWithCongregation = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblRecordCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lcGeneralateLedgers = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblOpBalanceTitle = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblOpBalance = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lcMapLedgers = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcOpeningIEBalance = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcBalanceMode = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblNote = new DevExpress.XtraLayout.SimpleLabelItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblFANote = new DevExpress.XtraLayout.EmptySpaceItem();
            this.chkLedgerSelectAll = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCYFADepreciation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCYFADepreciation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtDebit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtCredit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repLKPDepreciationLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTransMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOpeningIEBalance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLedgerSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtOpBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcboType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpGeneralateLedgers.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcFACancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgFixedAssetDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcFixedAssetLedgers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFixedAssetTotalCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepreciationTotalCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepreciationAccuTotalCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFixedAssetOP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepreciationOP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepreciationAccuOP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFixedAssetDR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepreciationDR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepreciationAccuDR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFixedAssetCR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepreciationCR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepreciationAccuCR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFixedAssetTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepreciationTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepreciationAccuTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcResetfromFinance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcFAUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcShowMapLedgers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgMapWithCongregation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGeneralateLedgers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblOpBalanceTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblOpBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMapLedgers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOpeningIEBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcBalanceMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFANote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLedgerSelectAll.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnShowMapLedgers);
            this.layoutControl1.Controls.Add(this.btnMapLedgers);
            this.layoutControl1.Controls.Add(this.btnFAUpdate);
            this.layoutControl1.Controls.Add(this.btnLoadFromFinance);
            this.layoutControl1.Controls.Add(this.gcCYFADepreciation);
            this.layoutControl1.Controls.Add(this.cboTransMode);
            this.layoutControl1.Controls.Add(this.txtOpeningIEBalance);
            this.layoutControl1.Controls.Add(this.gcLedger);
            this.layoutControl1.Controls.Add(this.glkpGeneralateLedgers);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(5, 5);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(713, 296, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(653, 506);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnShowMapLedgers
            // 
            this.btnShowMapLedgers.Location = new System.Drawing.Point(7, 451);
            this.btnShowMapLedgers.Name = "btnShowMapLedgers";
            this.btnShowMapLedgers.Size = new System.Drawing.Size(101, 22);
            this.btnShowMapLedgers.StyleController = this.layoutControl1;
            toolTipTitleItem1.Text = "Map Ledgers";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "Show Map Ledgers";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.btnShowMapLedgers.SuperTip = superToolTip1;
            this.btnShowMapLedgers.TabIndex = 53;
            this.btnShowMapLedgers.Text = "&Show Map Ledgers";
            this.btnShowMapLedgers.Click += new System.EventHandler(this.btnShowMapLedgers_Click);
            // 
            // btnMapLedgers
            // 
            this.btnMapLedgers.Location = new System.Drawing.Point(529, 207);
            this.btnMapLedgers.Name = "btnMapLedgers";
            this.btnMapLedgers.Size = new System.Drawing.Size(117, 24);
            this.btnMapLedgers.StyleController = this.layoutControl1;
            toolTipTitleItem2.Text = "Save && Map Ledgers";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "Click here to Save Generalate Ledger Opening Balance && Map Ledgers";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.btnMapLedgers.SuperTip = superToolTip2;
            this.btnMapLedgers.TabIndex = 53;
            this.btnMapLedgers.Text = "Save && Map Ledgers";
            this.btnMapLedgers.Click += new System.EventHandler(this.btnMapLedgers_Click);
            // 
            // btnFAUpdate
            // 
            this.btnFAUpdate.Location = new System.Drawing.Point(495, 451);
            this.btnFAUpdate.Name = "btnFAUpdate";
            this.btnFAUpdate.Size = new System.Drawing.Size(151, 22);
            this.btnFAUpdate.StyleController = this.layoutControl1;
            this.btnFAUpdate.TabIndex = 35;
            this.btnFAUpdate.Text = "Save &Fixed Asset Details";
            this.btnFAUpdate.Click += new System.EventHandler(this.btnFAUpdate_Click);
            // 
            // btnLoadFromFinance
            // 
            this.btnLoadFromFinance.Location = new System.Drawing.Point(112, 451);
            this.btnLoadFromFinance.Name = "btnLoadFromFinance";
            this.btnLoadFromFinance.Size = new System.Drawing.Size(99, 22);
            this.btnLoadFromFinance.StyleController = this.layoutControl1;
            toolTipTitleItem3.Text = "Fixed Asset and Depreciation";
            toolTipItem3.LeftIndent = 6;
            toolTipItem3.Text = "Get current finance year Fixed Asset and Depreciation Voucher Amount";
            superToolTip3.Items.Add(toolTipTitleItem3);
            superToolTip3.Items.Add(toolTipItem3);
            this.btnLoadFromFinance.SuperTip = superToolTip3;
            this.btnLoadFromFinance.TabIndex = 35;
            this.btnLoadFromFinance.Text = "&Load from Finance";
            this.btnLoadFromFinance.Click += new System.EventHandler(this.btnLoadFromFinance_Click);
            // 
            // gcCYFADepreciation
            // 
            this.gcCYFADepreciation.Location = new System.Drawing.Point(7, 265);
            this.gcCYFADepreciation.MainView = this.gvCYFADepreciation;
            this.gcCYFADepreciation.Name = "gcCYFADepreciation";
            this.gcCYFADepreciation.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemTextEdit1,
            this.repositoryItemComboBox1,
            this.repLKPDepreciationLedger,
            this.rtxtDebit,
            this.rtxtCredit});
            this.gcCYFADepreciation.Size = new System.Drawing.Size(639, 105);
            this.gcCYFADepreciation.TabIndex = 35;
            this.gcCYFADepreciation.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCYFADepreciation});
            this.gcCYFADepreciation.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.gcCYFADepreciation_ProcessGridKey);
            this.gcCYFADepreciation.Validating += new System.ComponentModel.CancelEventHandler(this.gcCYFADepreciation_Validating);
            // 
            // gvCYFADepreciation
            // 
            this.gvCYFADepreciation.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvCYFADepreciation.Appearance.FocusedRow.Options.UseFont = true;
            this.gvCYFADepreciation.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvCYFADepreciation.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colFALedgerid,
            this.colFALedgerCode,
            this.colFALedger,
            this.colFAConOpAmount,
            this.colFAConDebit,
            this.colFAConCredit,
            this.colFAConCLBalance});
            this.gvCYFADepreciation.GridControl = this.gcCYFADepreciation;
            this.gvCYFADepreciation.Name = "gvCYFADepreciation";
            this.gvCYFADepreciation.OptionsView.ShowGroupPanel = false;
            this.gvCYFADepreciation.OptionsView.ShowIndicator = false;
            this.gvCYFADepreciation.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvCYFADepreciation_CellValueChanged);
            // 
            // colFALedgerid
            // 
            this.colFALedgerid.Caption = "Ledger Id";
            this.colFALedgerid.FieldName = "LEDGER";
            this.colFALedgerid.Name = "colFALedgerid";
            // 
            // colFALedgerCode
            // 
            this.colFALedgerCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colFALedgerCode.AppearanceHeader.Options.UseFont = true;
            this.colFALedgerCode.Caption = "Code";
            this.colFALedgerCode.FieldName = "LEDGER_CODE";
            this.colFALedgerCode.Name = "colFALedgerCode";
            this.colFALedgerCode.OptionsColumn.AllowEdit = false;
            this.colFALedgerCode.OptionsColumn.AllowFocus = false;
            this.colFALedgerCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colFALedgerCode.OptionsColumn.FixedWidth = true;
            this.colFALedgerCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colFALedgerCode.Visible = true;
            this.colFALedgerCode.VisibleIndex = 0;
            this.colFALedgerCode.Width = 52;
            // 
            // colFALedger
            // 
            this.colFALedger.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colFALedger.AppearanceHeader.Options.UseFont = true;
            this.colFALedger.Caption = "Ledger";
            this.colFALedger.FieldName = "LEDGER_NAME";
            this.colFALedger.Name = "colFALedger";
            this.colFALedger.OptionsColumn.AllowEdit = false;
            this.colFALedger.OptionsColumn.AllowFocus = false;
            this.colFALedger.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colFALedger.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colFALedger.OptionsColumn.FixedWidth = true;
            this.colFALedger.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colFALedger.Visible = true;
            this.colFALedger.VisibleIndex = 1;
            this.colFALedger.Width = 232;
            // 
            // colFAConOpAmount
            // 
            this.colFAConOpAmount.AppearanceCell.Options.UseTextOptions = true;
            this.colFAConOpAmount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colFAConOpAmount.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colFAConOpAmount.AppearanceHeader.Options.UseFont = true;
            this.colFAConOpAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.colFAConOpAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colFAConOpAmount.Caption = "OP. Balance";
            this.colFAConOpAmount.DisplayFormat.FormatString = "n";
            this.colFAConOpAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colFAConOpAmount.FieldName = "CON_OP_AMOUNT";
            this.colFAConOpAmount.Name = "colFAConOpAmount";
            this.colFAConOpAmount.OptionsColumn.AllowEdit = false;
            this.colFAConOpAmount.OptionsColumn.AllowFocus = false;
            this.colFAConOpAmount.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colFAConOpAmount.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colFAConOpAmount.OptionsColumn.AllowMove = false;
            this.colFAConOpAmount.OptionsColumn.AllowSize = false;
            this.colFAConOpAmount.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colFAConOpAmount.OptionsColumn.FixedWidth = true;
            this.colFAConOpAmount.OptionsFilter.AllowAutoFilter = false;
            this.colFAConOpAmount.OptionsFilter.AllowFilter = false;
            this.colFAConOpAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "CON_OP_AMOUNT", "{0:n}")});
            this.colFAConOpAmount.Visible = true;
            this.colFAConOpAmount.VisibleIndex = 2;
            this.colFAConOpAmount.Width = 80;
            // 
            // colFAConDebit
            // 
            this.colFAConDebit.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.colFAConDebit.AppearanceCell.Options.UseBackColor = true;
            this.colFAConDebit.AppearanceCell.Options.UseTextOptions = true;
            this.colFAConDebit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colFAConDebit.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colFAConDebit.AppearanceHeader.Options.UseFont = true;
            this.colFAConDebit.AppearanceHeader.Options.UseTextOptions = true;
            this.colFAConDebit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colFAConDebit.Caption = "Debit";
            this.colFAConDebit.ColumnEdit = this.rtxtDebit;
            this.colFAConDebit.DisplayFormat.FormatString = "n";
            this.colFAConDebit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colFAConDebit.FieldName = "DEBIT";
            this.colFAConDebit.Name = "colFAConDebit";
            this.colFAConDebit.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colFAConDebit.OptionsColumn.AllowMove = false;
            this.colFAConDebit.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colFAConDebit.OptionsColumn.FixedWidth = true;
            this.colFAConDebit.OptionsFilter.AllowAutoFilter = false;
            this.colFAConDebit.OptionsFilter.AllowFilter = false;
            this.colFAConDebit.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Debit", "{0:n}")});
            this.colFAConDebit.Visible = true;
            this.colFAConDebit.VisibleIndex = 3;
            this.colFAConDebit.Width = 80;
            // 
            // rtxtDebit
            // 
            this.rtxtDebit.AutoHeight = false;
            this.rtxtDebit.Mask.EditMask = "n";
            this.rtxtDebit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rtxtDebit.Name = "rtxtDebit";
            // 
            // colFAConCredit
            // 
            this.colFAConCredit.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.colFAConCredit.AppearanceCell.Options.UseBackColor = true;
            this.colFAConCredit.AppearanceCell.Options.UseTextOptions = true;
            this.colFAConCredit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colFAConCredit.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colFAConCredit.AppearanceHeader.Options.UseFont = true;
            this.colFAConCredit.AppearanceHeader.Options.UseTextOptions = true;
            this.colFAConCredit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colFAConCredit.Caption = "Credit";
            this.colFAConCredit.ColumnEdit = this.rtxtCredit;
            this.colFAConCredit.DisplayFormat.FormatString = "n";
            this.colFAConCredit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colFAConCredit.FieldName = "CREDIT";
            this.colFAConCredit.Name = "colFAConCredit";
            this.colFAConCredit.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colFAConCredit.OptionsColumn.AllowMove = false;
            this.colFAConCredit.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colFAConCredit.OptionsColumn.FixedWidth = true;
            this.colFAConCredit.OptionsFilter.AllowAutoFilter = false;
            this.colFAConCredit.OptionsFilter.AllowFilter = false;
            this.colFAConCredit.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Credit", "{0:n}")});
            this.colFAConCredit.Visible = true;
            this.colFAConCredit.VisibleIndex = 4;
            this.colFAConCredit.Width = 80;
            // 
            // rtxtCredit
            // 
            this.rtxtCredit.AutoHeight = false;
            this.rtxtCredit.Mask.EditMask = "n";
            this.rtxtCredit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rtxtCredit.Name = "rtxtCredit";
            // 
            // colFAConCLBalance
            // 
            this.colFAConCLBalance.AppearanceCell.Options.UseTextOptions = true;
            this.colFAConCLBalance.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colFAConCLBalance.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colFAConCLBalance.AppearanceHeader.Options.UseFont = true;
            this.colFAConCLBalance.AppearanceHeader.Options.UseTextOptions = true;
            this.colFAConCLBalance.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colFAConCLBalance.Caption = "CL. Balance";
            this.colFAConCLBalance.DisplayFormat.FormatString = "n";
            this.colFAConCLBalance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colFAConCLBalance.FieldName = "CON_CL_AMOUNT";
            this.colFAConCLBalance.Name = "colFAConCLBalance";
            this.colFAConCLBalance.OptionsColumn.AllowEdit = false;
            this.colFAConCLBalance.OptionsColumn.AllowFocus = false;
            this.colFAConCLBalance.OptionsFilter.AllowAutoFilter = false;
            this.colFAConCLBalance.OptionsFilter.AllowFilter = false;
            this.colFAConCLBalance.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "CON_CL_AMOUNT", "{0:n}")});
            this.colFAConCLBalance.Visible = true;
            this.colFAConCLBalance.VisibleIndex = 5;
            this.colFAConCLBalance.Width = 113;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.Appearance.Options.UseBackColor = true;
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Caption = "Check";
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repositoryItemCheckEdit1.ValueChecked = ((long)(1));
            this.repositoryItemCheckEdit1.ValueGrayed = ((long)(0));
            this.repositoryItemCheckEdit1.ValueUnchecked = ((long)(0));
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.DisplayFormat.FormatString = "n";
            this.repositoryItemTextEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Items.AddRange(new object[] {
            "CR",
            "DR"});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            this.repositoryItemComboBox1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // repLKPDepreciationLedger
            // 
            this.repLKPDepreciationLedger.AutoHeight = false;
            this.repLKPDepreciationLedger.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repLKPDepreciationLedger.Name = "repLKPDepreciationLedger";
            this.repLKPDepreciationLedger.View = this.repositoryItemGridLookUpEdit1View;
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // cboTransMode
            // 
            this.cboTransMode.EditValue = "Operating Profit";
            this.cboTransMode.Location = new System.Drawing.Point(312, 209);
            this.cboTransMode.Name = "cboTransMode";
            this.cboTransMode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.cboTransMode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTransMode.Properties.Items.AddRange(new object[] {
            "Operating Profit",
            "Operating losses"});
            this.cboTransMode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboTransMode.Size = new System.Drawing.Size(115, 20);
            this.cboTransMode.StyleController = this.layoutControl1;
            this.cboTransMode.TabIndex = 35;
            // 
            // txtOpeningIEBalance
            // 
            this.txtOpeningIEBalance.EditValue = "0";
            this.txtOpeningIEBalance.Location = new System.Drawing.Point(114, 209);
            this.txtOpeningIEBalance.Name = "txtOpeningIEBalance";
            this.txtOpeningIEBalance.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtOpeningIEBalance.Properties.DisplayFormat.FormatString = "n";
            this.txtOpeningIEBalance.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtOpeningIEBalance.Properties.Mask.EditMask = "n";
            this.txtOpeningIEBalance.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtOpeningIEBalance.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtOpeningIEBalance.Size = new System.Drawing.Size(119, 20);
            this.txtOpeningIEBalance.StyleController = this.layoutControl1;
            this.txtOpeningIEBalance.TabIndex = 47;
            // 
            // gcLedger
            // 
            this.gcLedger.Location = new System.Drawing.Point(7, 73);
            this.gcLedger.MainView = this.gvLedger;
            this.gcLedger.Name = "gcLedger";
            this.gcLedger.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.chkLedgerSelect,
            this.rtxtOpBalance,
            this.rcboType});
            this.gcLedger.Size = new System.Drawing.Size(639, 102);
            this.gcLedger.TabIndex = 34;
            this.gcLedger.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLedger});
            this.gcLedger.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.gcLedger_ProcessGridKey);
            this.gcLedger.Click += new System.EventHandler(this.gcLedger_Click);
            // 
            // gvLedger
            // 
            this.gvLedger.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvLedger.Appearance.FocusedRow.Options.UseFont = true;
            this.gvLedger.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvLedger.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLedgerId,
            this.colSelect,
            this.colLedgerCode,
            this.colLedgerName,
            this.colGroup,
            this.colNature,
            this.colOPAmount,
            this.colOPAmountTransMode});
            this.gvLedger.GridControl = this.gcLedger;
            this.gvLedger.Name = "gvLedger";
            this.gvLedger.OptionsView.ShowGroupPanel = false;
            this.gvLedger.OptionsView.ShowIndicator = false;
            this.gvLedger.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvLedger_RowCellStyle);
            this.gvLedger.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gvLedger_ShowingEditor);
            this.gvLedger.RowCountChanged += new System.EventHandler(this.gvLedgers_RowCountChanged);
            // 
            // colLedgerId
            // 
            this.colLedgerId.Caption = "Ledger Id";
            this.colLedgerId.FieldName = "LEDGER_ID";
            this.colLedgerId.Name = "colLedgerId";
            // 
            // colSelect
            // 
            this.colSelect.Caption = "Sel";
            this.colSelect.ColumnEdit = this.chkLedgerSelect;
            this.colSelect.FieldName = "SELECT";
            this.colSelect.Name = "colSelect";
            this.colSelect.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colSelect.OptionsColumn.ShowCaption = false;
            this.colSelect.OptionsFilter.AllowAutoFilter = false;
            this.colSelect.OptionsFilter.AllowFilter = false;
            this.colSelect.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colSelect.Visible = true;
            this.colSelect.VisibleIndex = 0;
            this.colSelect.Width = 22;
            // 
            // chkLedgerSelect
            // 
            this.chkLedgerSelect.Appearance.Options.UseBackColor = true;
            this.chkLedgerSelect.AutoHeight = false;
            this.chkLedgerSelect.Caption = "Check";
            this.chkLedgerSelect.Name = "chkLedgerSelect";
            this.chkLedgerSelect.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.chkLedgerSelect.ValueChecked = ((long)(1));
            this.chkLedgerSelect.ValueGrayed = ((long)(0));
            this.chkLedgerSelect.ValueUnchecked = ((long)(0));
            // 
            // colLedgerCode
            // 
            this.colLedgerCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colLedgerCode.AppearanceHeader.Options.UseFont = true;
            this.colLedgerCode.Caption = "Code";
            this.colLedgerCode.FieldName = "LEDGER_CODE";
            this.colLedgerCode.Name = "colLedgerCode";
            this.colLedgerCode.OptionsColumn.AllowEdit = false;
            this.colLedgerCode.OptionsColumn.AllowFocus = false;
            this.colLedgerCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colLedgerCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colLedgerCode.Visible = true;
            this.colLedgerCode.VisibleIndex = 1;
            this.colLedgerCode.Width = 37;
            // 
            // colLedgerName
            // 
            this.colLedgerName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colLedgerName.AppearanceHeader.Options.UseFont = true;
            this.colLedgerName.Caption = "Ledger";
            this.colLedgerName.FieldName = "LEDGER_NAME";
            this.colLedgerName.Name = "colLedgerName";
            this.colLedgerName.OptionsColumn.AllowEdit = false;
            this.colLedgerName.OptionsColumn.AllowFocus = false;
            this.colLedgerName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colLedgerName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colLedgerName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colLedgerName.Visible = true;
            this.colLedgerName.VisibleIndex = 2;
            this.colLedgerName.Width = 124;
            // 
            // colGroup
            // 
            this.colGroup.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colGroup.AppearanceHeader.Options.UseFont = true;
            this.colGroup.Caption = "Group";
            this.colGroup.FieldName = "LEDGER_GROUP";
            this.colGroup.Name = "colGroup";
            this.colGroup.OptionsColumn.AllowEdit = false;
            this.colGroup.OptionsColumn.AllowFocus = false;
            this.colGroup.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colGroup.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colGroup.Visible = true;
            this.colGroup.VisibleIndex = 3;
            this.colGroup.Width = 96;
            // 
            // colNature
            // 
            this.colNature.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colNature.AppearanceHeader.Options.UseFont = true;
            this.colNature.Caption = "Nature";
            this.colNature.FieldName = "NATURE";
            this.colNature.Name = "colNature";
            this.colNature.OptionsColumn.AllowEdit = false;
            this.colNature.OptionsColumn.AllowFocus = false;
            this.colNature.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colNature.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colNature.OptionsColumn.AllowMove = false;
            this.colNature.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colNature.Visible = true;
            this.colNature.VisibleIndex = 4;
            this.colNature.Width = 76;
            // 
            // colOPAmount
            // 
            this.colOPAmount.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.colOPAmount.AppearanceCell.Options.UseBackColor = true;
            this.colOPAmount.AppearanceCell.Options.UseTextOptions = true;
            this.colOPAmount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colOPAmount.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colOPAmount.AppearanceHeader.Options.UseFont = true;
            this.colOPAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.colOPAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colOPAmount.Caption = "O/P Balance";
            this.colOPAmount.ColumnEdit = this.rtxtOpBalance;
            this.colOPAmount.DisplayFormat.FormatString = "n";
            this.colOPAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colOPAmount.FieldName = "CON_OP_AMOUNT";
            this.colOPAmount.Name = "colOPAmount";
            this.colOPAmount.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colOPAmount.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colOPAmount.OptionsColumn.AllowMove = false;
            this.colOPAmount.OptionsColumn.AllowSize = false;
            this.colOPAmount.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colOPAmount.OptionsColumn.FixedWidth = true;
            this.colOPAmount.Visible = true;
            this.colOPAmount.VisibleIndex = 5;
            this.colOPAmount.Width = 79;
            // 
            // rtxtOpBalance
            // 
            this.rtxtOpBalance.AutoHeight = false;
            this.rtxtOpBalance.DisplayFormat.FormatString = "n";
            this.rtxtOpBalance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rtxtOpBalance.Mask.EditMask = "n";
            this.rtxtOpBalance.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rtxtOpBalance.Name = "rtxtOpBalance";
            // 
            // colOPAmountTransMode
            // 
            this.colOPAmountTransMode.ColumnEdit = this.rcboType;
            this.colOPAmountTransMode.FieldName = "CON_OP_TRANS_MODE";
            this.colOPAmountTransMode.Name = "colOPAmountTransMode";
            this.colOPAmountTransMode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colOPAmountTransMode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colOPAmountTransMode.OptionsColumn.AllowMove = false;
            this.colOPAmountTransMode.OptionsColumn.AllowSize = false;
            this.colOPAmountTransMode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colOPAmountTransMode.OptionsColumn.FixedWidth = true;
            this.colOPAmountTransMode.OptionsColumn.ShowCaption = false;
            this.colOPAmountTransMode.Visible = true;
            this.colOPAmountTransMode.VisibleIndex = 6;
            this.colOPAmountTransMode.Width = 40;
            // 
            // rcboType
            // 
            this.rcboType.AutoHeight = false;
            this.rcboType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcboType.Items.AddRange(new object[] {
            "CR",
            "DR"});
            this.rcboType.Name = "rcboType";
            this.rcboType.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // glkpGeneralateLedgers
            // 
            this.glkpGeneralateLedgers.Location = new System.Drawing.Point(106, 49);
            this.glkpGeneralateLedgers.Name = "glkpGeneralateLedgers";
            this.glkpGeneralateLedgers.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpGeneralateLedgers.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkpGeneralateLedgers.Properties.NullText = "";
            this.glkpGeneralateLedgers.Properties.PopupFormSize = new System.Drawing.Size(540, 0);
            this.glkpGeneralateLedgers.Properties.View = this.gridLookUpEdit1View;
            this.glkpGeneralateLedgers.Size = new System.Drawing.Size(540, 20);
            this.glkpGeneralateLedgers.StyleController = this.layoutControl1;
            this.glkpGeneralateLedgers.TabIndex = 2;
            this.glkpGeneralateLedgers.Tag = "PR";
            this.glkpGeneralateLedgers.EditValueChanged += new System.EventHandler(this.glkpGeneralateLedgers_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gvColConLedgerId,
            this.gvColConLedgerCode,
            this.gvColConLedgerName});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // gvColConLedgerId
            // 
            this.gvColConLedgerId.Caption = "Id";
            this.gvColConLedgerId.FieldName = "CON_LEDGER_ID";
            this.gvColConLedgerId.Name = "gvColConLedgerId";
            // 
            // gvColConLedgerCode
            // 
            this.gvColConLedgerCode.Caption = "Code";
            this.gvColConLedgerCode.FieldName = "CON_LEDGER_CODE";
            this.gvColConLedgerCode.Name = "gvColConLedgerCode";
            this.gvColConLedgerCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gvColConLedgerCode.Visible = true;
            this.gvColConLedgerCode.VisibleIndex = 0;
            this.gvColConLedgerCode.Width = 50;
            // 
            // gvColConLedgerName
            // 
            this.gvColConLedgerName.Caption = "Generalate Ledger";
            this.gvColConLedgerName.FieldName = "CON_LEDGER_NAME";
            this.gvColConLedgerName.Name = "gvColConLedgerName";
            this.gvColConLedgerName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gvColConLedgerName.Visible = true;
            this.gvColConLedgerName.VisibleIndex = 1;
            this.gvColConLedgerName.Width = 350;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(575, 482);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Close";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkShowFilter
            // 
            this.chkShowFilter.Location = new System.Drawing.Point(7, 179);
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = "Show <b> F</b>ilter";
            this.chkShowFilter.Size = new System.Drawing.Size(87, 19);
            this.chkShowFilter.StyleController = this.layoutControl1;
            this.chkShowFilter.TabIndex = 5;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcFACancel,
            this.lcgFixedAssetDetails,
            this.lcgMapWithCongregation,
            this.lblFANote});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(653, 506);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcFACancel
            // 
            this.lcFACancel.Control = this.btnCancel;
            this.lcFACancel.CustomizationFormText = "lcFACancel";
            this.lcFACancel.Location = new System.Drawing.Point(573, 480);
            this.lcFACancel.MaxSize = new System.Drawing.Size(80, 26);
            this.lcFACancel.MinSize = new System.Drawing.Size(80, 26);
            this.lcFACancel.Name = "lcFACancel";
            this.lcFACancel.Size = new System.Drawing.Size(80, 26);
            this.lcFACancel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcFACancel.Text = "lcFACancel";
            this.lcFACancel.TextSize = new System.Drawing.Size(0, 0);
            this.lcFACancel.TextToControlDistance = 0;
            this.lcFACancel.TextVisible = false;
            // 
            // lcgFixedAssetDetails
            // 
            this.lcgFixedAssetDetails.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lcgFixedAssetDetails.AppearanceGroup.Options.UseFont = true;
            this.lcgFixedAssetDetails.CustomizationFormText = "Fixed Asset && Depreciation - Current Year";
            this.lcgFixedAssetDetails.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcFixedAssetLedgers,
            this.lblFixedAssetTotalCaption,
            this.lblDepreciationTotalCaption,
            this.lblDepreciationAccuTotalCaption,
            this.lblFixedAssetOP,
            this.lblDepreciationOP,
            this.lblDepreciationAccuOP,
            this.lblFixedAssetDR,
            this.lblDepreciationDR,
            this.lblDepreciationAccuDR,
            this.lblFixedAssetCR,
            this.lblDepreciationCR,
            this.lblDepreciationAccuCR,
            this.lblFixedAssetTotal,
            this.lblDepreciationTotal,
            this.lblDepreciationAccuTotal,
            this.lcResetfromFinance,
            this.lcFAUpdate,
            this.lcShowMapLedgers,
            this.emptySpaceItem3,
            this.simpleLabelItem2});
            this.lcgFixedAssetDetails.Location = new System.Drawing.Point(0, 238);
            this.lcgFixedAssetDetails.Name = "lcgFixedAssetDetails";
            this.lcgFixedAssetDetails.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.lcgFixedAssetDetails.Size = new System.Drawing.Size(653, 242);
            this.lcgFixedAssetDetails.Text = "Fixed Asset && Depreciation - Current Year";
            // 
            // lcFixedAssetLedgers
            // 
            this.lcFixedAssetLedgers.Control = this.gcCYFADepreciation;
            this.lcFixedAssetLedgers.CustomizationFormText = "lcFixedAssetLedgers";
            this.lcFixedAssetLedgers.Location = new System.Drawing.Point(0, 0);
            this.lcFixedAssetLedgers.MinSize = new System.Drawing.Size(50, 25);
            this.lcFixedAssetLedgers.Name = "lcFixedAssetLedgers";
            this.lcFixedAssetLedgers.Size = new System.Drawing.Size(643, 109);
            this.lcFixedAssetLedgers.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcFixedAssetLedgers.Text = "lcFixedAssetLedgers";
            this.lcFixedAssetLedgers.TextSize = new System.Drawing.Size(0, 0);
            this.lcFixedAssetLedgers.TextToControlDistance = 0;
            this.lcFixedAssetLedgers.TextVisible = false;
            // 
            // lblFixedAssetTotalCaption
            // 
            this.lblFixedAssetTotalCaption.AllowHotTrack = false;
            this.lblFixedAssetTotalCaption.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblFixedAssetTotalCaption.AppearanceItemCaption.Options.UseFont = true;
            this.lblFixedAssetTotalCaption.CustomizationFormText = "Total Fixed Asset   (B) : ";
            this.lblFixedAssetTotalCaption.Location = new System.Drawing.Point(0, 109);
            this.lblFixedAssetTotalCaption.MaxSize = new System.Drawing.Size(287, 20);
            this.lblFixedAssetTotalCaption.MinSize = new System.Drawing.Size(287, 20);
            this.lblFixedAssetTotalCaption.Name = "lblFixedAssetTotalCaption";
            this.lblFixedAssetTotalCaption.Size = new System.Drawing.Size(287, 20);
            this.lblFixedAssetTotalCaption.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblFixedAssetTotalCaption.Text = "Fixed Asset Accumulated   (B) : ";
            this.lblFixedAssetTotalCaption.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblFixedAssetTotalCaption.TextSize = new System.Drawing.Size(126, 13);
            // 
            // lblDepreciationTotalCaption
            // 
            this.lblDepreciationTotalCaption.AllowHotTrack = false;
            this.lblDepreciationTotalCaption.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblDepreciationTotalCaption.AppearanceItemCaption.Options.UseFont = true;
            this.lblDepreciationTotalCaption.CustomizationFormText = "Total Depreciation (G) : ";
            this.lblDepreciationTotalCaption.Location = new System.Drawing.Point(0, 129);
            this.lblDepreciationTotalCaption.MaxSize = new System.Drawing.Size(287, 20);
            this.lblDepreciationTotalCaption.MinSize = new System.Drawing.Size(287, 20);
            this.lblDepreciationTotalCaption.Name = "lblDepreciationTotalCaption";
            this.lblDepreciationTotalCaption.Size = new System.Drawing.Size(287, 20);
            this.lblDepreciationTotalCaption.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblDepreciationTotalCaption.Text = "Depreciation Current Year (G) : ";
            this.lblDepreciationTotalCaption.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblDepreciationTotalCaption.TextSize = new System.Drawing.Size(134, 13);
            // 
            // lblDepreciationAccuTotalCaption
            // 
            this.lblDepreciationAccuTotalCaption.AllowHotTrack = false;
            this.lblDepreciationAccuTotalCaption.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblDepreciationAccuTotalCaption.AppearanceItemCaption.Options.UseFont = true;
            this.lblDepreciationAccuTotalCaption.CustomizationFormText = "Depreciation Accumulated (I) : ";
            this.lblDepreciationAccuTotalCaption.Location = new System.Drawing.Point(0, 149);
            this.lblDepreciationAccuTotalCaption.MaxSize = new System.Drawing.Size(287, 20);
            this.lblDepreciationAccuTotalCaption.MinSize = new System.Drawing.Size(287, 20);
            this.lblDepreciationAccuTotalCaption.Name = "lblDepreciationAccuTotalCaption";
            this.lblDepreciationAccuTotalCaption.Size = new System.Drawing.Size(287, 20);
            this.lblDepreciationAccuTotalCaption.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblDepreciationAccuTotalCaption.Text = "Depreciation Accumulated (I) : ";
            this.lblDepreciationAccuTotalCaption.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblDepreciationAccuTotalCaption.TextSize = new System.Drawing.Size(50, 20);
            // 
            // lblFixedAssetOP
            // 
            this.lblFixedAssetOP.AllowHotTrack = false;
            this.lblFixedAssetOP.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblFixedAssetOP.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblFixedAssetOP.CustomizationFormText = "0.00";
            this.lblFixedAssetOP.Location = new System.Drawing.Point(287, 109);
            this.lblFixedAssetOP.MaxSize = new System.Drawing.Size(80, 20);
            this.lblFixedAssetOP.MinSize = new System.Drawing.Size(80, 20);
            this.lblFixedAssetOP.Name = "lblFixedAssetOP";
            this.lblFixedAssetOP.Size = new System.Drawing.Size(80, 20);
            this.lblFixedAssetOP.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblFixedAssetOP.Text = "0.00";
            this.lblFixedAssetOP.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblFixedAssetOP.TextSize = new System.Drawing.Size(50, 20);
            // 
            // lblDepreciationOP
            // 
            this.lblDepreciationOP.AllowHotTrack = false;
            this.lblDepreciationOP.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblDepreciationOP.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblDepreciationOP.CustomizationFormText = "0.00";
            this.lblDepreciationOP.Location = new System.Drawing.Point(287, 129);
            this.lblDepreciationOP.MaxSize = new System.Drawing.Size(80, 20);
            this.lblDepreciationOP.MinSize = new System.Drawing.Size(80, 20);
            this.lblDepreciationOP.Name = "lblDepreciationOP";
            this.lblDepreciationOP.Size = new System.Drawing.Size(80, 20);
            this.lblDepreciationOP.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblDepreciationOP.Text = "0.00";
            this.lblDepreciationOP.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblDepreciationOP.TextSize = new System.Drawing.Size(50, 20);
            // 
            // lblDepreciationAccuOP
            // 
            this.lblDepreciationAccuOP.AllowHotTrack = false;
            this.lblDepreciationAccuOP.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblDepreciationAccuOP.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblDepreciationAccuOP.CustomizationFormText = "0.00";
            this.lblDepreciationAccuOP.Location = new System.Drawing.Point(287, 149);
            this.lblDepreciationAccuOP.MaxSize = new System.Drawing.Size(80, 20);
            this.lblDepreciationAccuOP.MinSize = new System.Drawing.Size(80, 20);
            this.lblDepreciationAccuOP.Name = "lblDepreciationAccuOP";
            this.lblDepreciationAccuOP.Size = new System.Drawing.Size(80, 20);
            this.lblDepreciationAccuOP.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblDepreciationAccuOP.Text = "0.00";
            this.lblDepreciationAccuOP.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblDepreciationAccuOP.TextSize = new System.Drawing.Size(50, 20);
            // 
            // lblFixedAssetDR
            // 
            this.lblFixedAssetDR.AllowHotTrack = false;
            this.lblFixedAssetDR.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblFixedAssetDR.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblFixedAssetDR.CustomizationFormText = "0.00";
            this.lblFixedAssetDR.Location = new System.Drawing.Point(367, 109);
            this.lblFixedAssetDR.MaxSize = new System.Drawing.Size(80, 20);
            this.lblFixedAssetDR.MinSize = new System.Drawing.Size(80, 20);
            this.lblFixedAssetDR.Name = "lblFixedAssetDR";
            this.lblFixedAssetDR.Size = new System.Drawing.Size(80, 20);
            this.lblFixedAssetDR.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblFixedAssetDR.Text = "0.00";
            this.lblFixedAssetDR.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblFixedAssetDR.TextSize = new System.Drawing.Size(50, 20);
            // 
            // lblDepreciationDR
            // 
            this.lblDepreciationDR.AllowHotTrack = false;
            this.lblDepreciationDR.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblDepreciationDR.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblDepreciationDR.CustomizationFormText = "0.00";
            this.lblDepreciationDR.Location = new System.Drawing.Point(367, 129);
            this.lblDepreciationDR.MaxSize = new System.Drawing.Size(80, 20);
            this.lblDepreciationDR.MinSize = new System.Drawing.Size(80, 20);
            this.lblDepreciationDR.Name = "lblDepreciationDR";
            this.lblDepreciationDR.Size = new System.Drawing.Size(80, 20);
            this.lblDepreciationDR.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblDepreciationDR.Text = "0.00";
            this.lblDepreciationDR.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblDepreciationDR.TextSize = new System.Drawing.Size(50, 20);
            // 
            // lblDepreciationAccuDR
            // 
            this.lblDepreciationAccuDR.AllowHotTrack = false;
            this.lblDepreciationAccuDR.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblDepreciationAccuDR.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblDepreciationAccuDR.CustomizationFormText = "0.00";
            this.lblDepreciationAccuDR.Location = new System.Drawing.Point(367, 149);
            this.lblDepreciationAccuDR.MaxSize = new System.Drawing.Size(80, 20);
            this.lblDepreciationAccuDR.MinSize = new System.Drawing.Size(80, 20);
            this.lblDepreciationAccuDR.Name = "lblDepreciationAccuDR";
            this.lblDepreciationAccuDR.Size = new System.Drawing.Size(80, 20);
            this.lblDepreciationAccuDR.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblDepreciationAccuDR.Text = "0.00";
            this.lblDepreciationAccuDR.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblDepreciationAccuDR.TextSize = new System.Drawing.Size(50, 20);
            // 
            // lblFixedAssetCR
            // 
            this.lblFixedAssetCR.AllowHotTrack = false;
            this.lblFixedAssetCR.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblFixedAssetCR.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblFixedAssetCR.CustomizationFormText = "0.00";
            this.lblFixedAssetCR.Location = new System.Drawing.Point(447, 109);
            this.lblFixedAssetCR.MaxSize = new System.Drawing.Size(80, 20);
            this.lblFixedAssetCR.MinSize = new System.Drawing.Size(80, 20);
            this.lblFixedAssetCR.Name = "lblFixedAssetCR";
            this.lblFixedAssetCR.Size = new System.Drawing.Size(80, 20);
            this.lblFixedAssetCR.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblFixedAssetCR.Text = "0.00";
            this.lblFixedAssetCR.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblFixedAssetCR.TextSize = new System.Drawing.Size(50, 20);
            // 
            // lblDepreciationCR
            // 
            this.lblDepreciationCR.AllowHotTrack = false;
            this.lblDepreciationCR.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblDepreciationCR.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblDepreciationCR.CustomizationFormText = "0.00";
            this.lblDepreciationCR.Location = new System.Drawing.Point(447, 129);
            this.lblDepreciationCR.MaxSize = new System.Drawing.Size(80, 20);
            this.lblDepreciationCR.MinSize = new System.Drawing.Size(80, 20);
            this.lblDepreciationCR.Name = "lblDepreciationCR";
            this.lblDepreciationCR.Size = new System.Drawing.Size(80, 20);
            this.lblDepreciationCR.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblDepreciationCR.Text = "0.00";
            this.lblDepreciationCR.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblDepreciationCR.TextSize = new System.Drawing.Size(50, 20);
            // 
            // lblDepreciationAccuCR
            // 
            this.lblDepreciationAccuCR.AllowHotTrack = false;
            this.lblDepreciationAccuCR.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblDepreciationAccuCR.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblDepreciationAccuCR.CustomizationFormText = "0.00";
            this.lblDepreciationAccuCR.Location = new System.Drawing.Point(447, 149);
            this.lblDepreciationAccuCR.MaxSize = new System.Drawing.Size(80, 20);
            this.lblDepreciationAccuCR.MinSize = new System.Drawing.Size(80, 20);
            this.lblDepreciationAccuCR.Name = "lblDepreciationAccuCR";
            this.lblDepreciationAccuCR.Size = new System.Drawing.Size(80, 20);
            this.lblDepreciationAccuCR.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblDepreciationAccuCR.Text = "0.00";
            this.lblDepreciationAccuCR.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblDepreciationAccuCR.TextSize = new System.Drawing.Size(50, 20);
            // 
            // lblFixedAssetTotal
            // 
            this.lblFixedAssetTotal.AllowHotTrack = false;
            this.lblFixedAssetTotal.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblFixedAssetTotal.AppearanceItemCaption.Options.UseFont = true;
            this.lblFixedAssetTotal.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblFixedAssetTotal.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblFixedAssetTotal.CustomizationFormText = "0.00";
            this.lblFixedAssetTotal.Location = new System.Drawing.Point(527, 109);
            this.lblFixedAssetTotal.MaxSize = new System.Drawing.Size(116, 20);
            this.lblFixedAssetTotal.MinSize = new System.Drawing.Size(116, 20);
            this.lblFixedAssetTotal.Name = "lblFixedAssetTotal";
            this.lblFixedAssetTotal.Size = new System.Drawing.Size(116, 20);
            this.lblFixedAssetTotal.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblFixedAssetTotal.Text = "0.00";
            this.lblFixedAssetTotal.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblFixedAssetTotal.TextSize = new System.Drawing.Size(50, 20);
            // 
            // lblDepreciationTotal
            // 
            this.lblDepreciationTotal.AllowHotTrack = false;
            this.lblDepreciationTotal.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblDepreciationTotal.AppearanceItemCaption.Options.UseFont = true;
            this.lblDepreciationTotal.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblDepreciationTotal.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblDepreciationTotal.CustomizationFormText = "0.00";
            this.lblDepreciationTotal.Location = new System.Drawing.Point(527, 129);
            this.lblDepreciationTotal.MaxSize = new System.Drawing.Size(116, 20);
            this.lblDepreciationTotal.MinSize = new System.Drawing.Size(116, 20);
            this.lblDepreciationTotal.Name = "lblDepreciationTotal";
            this.lblDepreciationTotal.Size = new System.Drawing.Size(116, 20);
            this.lblDepreciationTotal.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblDepreciationTotal.Text = "0.00";
            this.lblDepreciationTotal.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblDepreciationTotal.TextSize = new System.Drawing.Size(50, 20);
            // 
            // lblDepreciationAccuTotal
            // 
            this.lblDepreciationAccuTotal.AllowHotTrack = false;
            this.lblDepreciationAccuTotal.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblDepreciationAccuTotal.AppearanceItemCaption.Options.UseFont = true;
            this.lblDepreciationAccuTotal.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblDepreciationAccuTotal.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblDepreciationAccuTotal.CustomizationFormText = "0.00";
            this.lblDepreciationAccuTotal.Location = new System.Drawing.Point(527, 149);
            this.lblDepreciationAccuTotal.MaxSize = new System.Drawing.Size(116, 20);
            this.lblDepreciationAccuTotal.MinSize = new System.Drawing.Size(116, 20);
            this.lblDepreciationAccuTotal.Name = "lblDepreciationAccuTotal";
            this.lblDepreciationAccuTotal.Size = new System.Drawing.Size(116, 20);
            this.lblDepreciationAccuTotal.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblDepreciationAccuTotal.Text = "0.00";
            this.lblDepreciationAccuTotal.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblDepreciationAccuTotal.TextSize = new System.Drawing.Size(50, 20);
            // 
            // lcResetfromFinance
            // 
            this.lcResetfromFinance.Control = this.btnLoadFromFinance;
            this.lcResetfromFinance.CustomizationFormText = "lcResetfromFinance";
            this.lcResetfromFinance.Location = new System.Drawing.Point(105, 186);
            this.lcResetfromFinance.MaxSize = new System.Drawing.Size(103, 26);
            this.lcResetfromFinance.MinSize = new System.Drawing.Size(103, 26);
            this.lcResetfromFinance.Name = "lcResetfromFinance";
            this.lcResetfromFinance.Size = new System.Drawing.Size(103, 26);
            this.lcResetfromFinance.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcResetfromFinance.Text = "lcResetfromFinance";
            this.lcResetfromFinance.TextSize = new System.Drawing.Size(0, 0);
            this.lcResetfromFinance.TextToControlDistance = 0;
            this.lcResetfromFinance.TextVisible = false;
            // 
            // lcFAUpdate
            // 
            this.lcFAUpdate.Control = this.btnFAUpdate;
            this.lcFAUpdate.CustomizationFormText = "lcFAUpdate";
            this.lcFAUpdate.Location = new System.Drawing.Point(488, 186);
            this.lcFAUpdate.MaxSize = new System.Drawing.Size(155, 26);
            this.lcFAUpdate.MinSize = new System.Drawing.Size(155, 26);
            this.lcFAUpdate.Name = "lcFAUpdate";
            this.lcFAUpdate.Size = new System.Drawing.Size(155, 26);
            this.lcFAUpdate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcFAUpdate.Text = "lcFAUpdate";
            this.lcFAUpdate.TextSize = new System.Drawing.Size(0, 0);
            this.lcFAUpdate.TextToControlDistance = 0;
            this.lcFAUpdate.TextVisible = false;
            // 
            // lcShowMapLedgers
            // 
            this.lcShowMapLedgers.Control = this.btnShowMapLedgers;
            this.lcShowMapLedgers.CustomizationFormText = "lcShowMapLedgers";
            this.lcShowMapLedgers.Location = new System.Drawing.Point(0, 186);
            this.lcShowMapLedgers.MaxSize = new System.Drawing.Size(105, 26);
            this.lcShowMapLedgers.MinSize = new System.Drawing.Size(105, 26);
            this.lcShowMapLedgers.Name = "lcShowMapLedgers";
            this.lcShowMapLedgers.Size = new System.Drawing.Size(105, 26);
            this.lcShowMapLedgers.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcShowMapLedgers.Text = "lcShowMapLedgers";
            this.lcShowMapLedgers.TextSize = new System.Drawing.Size(0, 0);
            this.lcShowMapLedgers.TextToControlDistance = 0;
            this.lcShowMapLedgers.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(208, 186);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(280, 26);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleLabelItem2
            // 
            this.simpleLabelItem2.AllowHotTrack = false;
            this.simpleLabelItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem2.AppearanceItemCaption.ForeColor = System.Drawing.Color.Navy;
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseForeColor = true;
            this.simpleLabelItem2.CustomizationFormText = "For Depreciation Ledgers, Update values in Debit side";
            this.simpleLabelItem2.Location = new System.Drawing.Point(0, 169);
            this.simpleLabelItem2.Name = "simpleLabelItem2";
            this.simpleLabelItem2.Size = new System.Drawing.Size(643, 17);
            this.simpleLabelItem2.Text = "For Depreciation Ledgers, Update values in Debit side";
            this.simpleLabelItem2.TextSize = new System.Drawing.Size(300, 13);
            // 
            // lcgMapWithCongregation
            // 
            this.lcgMapWithCongregation.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lcgMapWithCongregation.AppearanceGroup.Options.UseFont = true;
            this.lcgMapWithCongregation.CustomizationFormText = "Map Ledgers to Generalate Ledger";
            this.lcgMapWithCongregation.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.simpleLabelItem1,
            this.lblRecordCount,
            this.lcGeneralateLedgers,
            this.layoutControlItem1,
            this.lblOpBalanceTitle,
            this.lblOpBalance,
            this.lcMapLedgers,
            this.lcOpeningIEBalance,
            this.lcBalanceMode,
            this.lblNote,
            this.emptySpaceItem1});
            this.lcgMapWithCongregation.Location = new System.Drawing.Point(0, 0);
            this.lcgMapWithCongregation.Name = "lcgMapWithCongregation";
            this.lcgMapWithCongregation.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.lcgMapWithCongregation.Size = new System.Drawing.Size(653, 238);
            this.lcgMapWithCongregation.Text = "Map Ledgers to Generalate Ledger";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.chkShowFilter;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 153);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(91, 23);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(91, 23);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(91, 28);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem1.CustomizationFormText = "#";
            this.simpleLabelItem1.Location = new System.Drawing.Point(602, 153);
            this.simpleLabelItem1.MaxSize = new System.Drawing.Size(15, 28);
            this.simpleLabelItem1.MinSize = new System.Drawing.Size(15, 28);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(15, 28);
            this.simpleLabelItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem1.Text = "#";
            this.simpleLabelItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(9, 13);
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.AllowHotTrack = false;
            this.lblRecordCount.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblRecordCount.AppearanceItemCaption.Options.UseFont = true;
            this.lblRecordCount.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblRecordCount.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.lblRecordCount.CustomizationFormText = "0";
            this.lblRecordCount.Location = new System.Drawing.Point(617, 153);
            this.lblRecordCount.MaxSize = new System.Drawing.Size(26, 28);
            this.lblRecordCount.MinSize = new System.Drawing.Size(26, 28);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(26, 28);
            this.lblRecordCount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblRecordCount.Text = "0";
            this.lblRecordCount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblRecordCount.TextSize = new System.Drawing.Size(7, 13);
            // 
            // lcGeneralateLedgers
            // 
            this.lcGeneralateLedgers.Control = this.glkpGeneralateLedgers;
            this.lcGeneralateLedgers.CustomizationFormText = "Generalate Ledgers";
            this.lcGeneralateLedgers.Location = new System.Drawing.Point(0, 23);
            this.lcGeneralateLedgers.Name = "lcGeneralateLedgers";
            this.lcGeneralateLedgers.Size = new System.Drawing.Size(643, 24);
            this.lcGeneralateLedgers.Text = "Generalate Ledgers";
            this.lcGeneralateLedgers.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lcGeneralateLedgers.TextSize = new System.Drawing.Size(94, 13);
            this.lcGeneralateLedgers.TextToControlDistance = 5;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcLedger;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 47);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(643, 106);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // lblOpBalanceTitle
            // 
            this.lblOpBalanceTitle.AllowHotTrack = false;
            this.lblOpBalanceTitle.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblOpBalanceTitle.AppearanceItemCaption.Options.UseFont = true;
            this.lblOpBalanceTitle.CustomizationFormText = "Opening Balance as on ";
            this.lblOpBalanceTitle.Location = new System.Drawing.Point(0, 0);
            this.lblOpBalanceTitle.MaxSize = new System.Drawing.Size(223, 23);
            this.lblOpBalanceTitle.MinSize = new System.Drawing.Size(223, 23);
            this.lblOpBalanceTitle.Name = "lblOpBalanceTitle";
            this.lblOpBalanceTitle.Size = new System.Drawing.Size(223, 23);
            this.lblOpBalanceTitle.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblOpBalanceTitle.Text = "Opening Balance as on 01/04/2022";
            this.lblOpBalanceTitle.TextSize = new System.Drawing.Size(300, 13);
            // 
            // lblOpBalance
            // 
            this.lblOpBalance.AllowHotTrack = false;
            this.lblOpBalance.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblOpBalance.AppearanceItemCaption.Options.UseFont = true;
            this.lblOpBalance.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblOpBalance.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblOpBalance.CustomizationFormText = "Opening Balance :  0.00";
            this.lblOpBalance.Location = new System.Drawing.Point(223, 0);
            this.lblOpBalance.MinSize = new System.Drawing.Size(201, 17);
            this.lblOpBalance.Name = "lblOpBalance";
            this.lblOpBalance.Size = new System.Drawing.Size(420, 23);
            this.lblOpBalance.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblOpBalance.Text = "Balance :  0.00";
            this.lblOpBalance.TextSize = new System.Drawing.Size(300, 13);
            // 
            // lcMapLedgers
            // 
            this.lcMapLedgers.Control = this.btnMapLedgers;
            this.lcMapLedgers.CustomizationFormText = "lcMapLedgers";
            this.lcMapLedgers.Location = new System.Drawing.Point(522, 181);
            this.lcMapLedgers.MaxSize = new System.Drawing.Size(121, 28);
            this.lcMapLedgers.MinSize = new System.Drawing.Size(121, 28);
            this.lcMapLedgers.Name = "lcMapLedgers";
            this.lcMapLedgers.Size = new System.Drawing.Size(121, 28);
            this.lcMapLedgers.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcMapLedgers.Text = "lcMapLedgers";
            this.lcMapLedgers.TextSize = new System.Drawing.Size(0, 0);
            this.lcMapLedgers.TextToControlDistance = 0;
            this.lcMapLedgers.TextVisible = false;
            // 
            // lcOpeningIEBalance
            // 
            this.lcOpeningIEBalance.Control = this.txtOpeningIEBalance;
            this.lcOpeningIEBalance.CustomizationFormText = "Opening IE Balance";
            this.lcOpeningIEBalance.Location = new System.Drawing.Point(0, 181);
            this.lcOpeningIEBalance.MaxSize = new System.Drawing.Size(232, 28);
            this.lcOpeningIEBalance.MinSize = new System.Drawing.Size(232, 28);
            this.lcOpeningIEBalance.Name = "lcOpeningIEBalance";
            this.lcOpeningIEBalance.Size = new System.Drawing.Size(232, 28);
            this.lcOpeningIEBalance.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcOpeningIEBalance.Spacing = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.lcOpeningIEBalance.Text = "IE Opening Balance";
            this.lcOpeningIEBalance.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcOpeningIEBalance.TextSize = new System.Drawing.Size(100, 13);
            this.lcOpeningIEBalance.TextToControlDistance = 5;
            // 
            // lcBalanceMode
            // 
            this.lcBalanceMode.Control = this.cboTransMode;
            this.lcBalanceMode.CustomizationFormText = "Balance Mode";
            this.lcBalanceMode.Location = new System.Drawing.Point(232, 181);
            this.lcBalanceMode.MaxSize = new System.Drawing.Size(194, 28);
            this.lcBalanceMode.MinSize = new System.Drawing.Size(194, 28);
            this.lcBalanceMode.Name = "lcBalanceMode";
            this.lcBalanceMode.Size = new System.Drawing.Size(194, 28);
            this.lcBalanceMode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcBalanceMode.Spacing = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.lcBalanceMode.Text = "Balance Mode";
            this.lcBalanceMode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lcBalanceMode.TextSize = new System.Drawing.Size(66, 13);
            this.lcBalanceMode.TextToControlDistance = 5;
            // 
            // lblNote
            // 
            this.lblNote.AllowHotTrack = false;
            this.lblNote.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblNote.AppearanceItemCaption.ForeColor = System.Drawing.Color.Navy;
            this.lblNote.AppearanceItemCaption.Options.UseFont = true;
            this.lblNote.AppearanceItemCaption.Options.UseForeColor = true;
            this.lblNote.CustomizationFormText = "Note : ";
            this.lblNote.Location = new System.Drawing.Point(91, 153);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(511, 28);
            this.lblNote.Text = "Note : ";
            this.lblNote.TextSize = new System.Drawing.Size(300, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(426, 181);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(96, 28);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblFANote
            // 
            this.lblFANote.AllowHotTrack = false;
            this.lblFANote.CustomizationFormText = "lblFANote";
            this.lblFANote.Location = new System.Drawing.Point(0, 480);
            this.lblFANote.Name = "lblFANote";
            this.lblFANote.Size = new System.Drawing.Size(573, 26);
            this.lblFANote.Text = "lblFANote";
            this.lblFANote.TextSize = new System.Drawing.Size(0, 0);
            // 
            // chkLedgerSelectAll
            // 
            this.chkLedgerSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkLedgerSelectAll.Location = new System.Drawing.Point(20, 79);
            this.chkLedgerSelectAll.Margin = new System.Windows.Forms.Padding(0);
            this.chkLedgerSelectAll.MinimumSize = new System.Drawing.Size(18, 18);
            this.chkLedgerSelectAll.Name = "chkLedgerSelectAll";
            this.chkLedgerSelectAll.Properties.Caption = "";
            this.chkLedgerSelectAll.Size = new System.Drawing.Size(19, 19);
            this.chkLedgerSelectAll.StyleController = this.layoutControl1;
            this.chkLedgerSelectAll.TabIndex = 52;
            // 
            // frmMapLedgersWithGeneralate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(663, 516);
            this.Controls.Add(this.chkLedgerSelectAll);
            this.Controls.Add(this.layoutControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMapLedgersWithGeneralate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Generalate Setting";
            this.Load += new System.EventHandler(this.frmMapLedgersWithGeneralate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcCYFADepreciation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCYFADepreciation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtDebit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtCredit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repLKPDepreciationLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTransMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOpeningIEBalance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLedgerSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtOpBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcboType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpGeneralateLedgers.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcFACancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgFixedAssetDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcFixedAssetLedgers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFixedAssetTotalCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepreciationTotalCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepreciationAccuTotalCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFixedAssetOP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepreciationOP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepreciationAccuOP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFixedAssetDR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepreciationDR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepreciationAccuDR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFixedAssetCR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepreciationCR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepreciationAccuCR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFixedAssetTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepreciationTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepreciationAccuTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcResetfromFinance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcFAUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcShowMapLedgers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgMapWithCongregation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGeneralateLedgers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblOpBalanceTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblOpBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMapLedgers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOpeningIEBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcBalanceMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFANote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLedgerSelectAll.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem lcFACancel;
        private DevExpress.XtraEditors.GridLookUpEdit glkpGeneralateLedgers;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gvColConLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn gvColConLedgerCode;
        private DevExpress.XtraGrid.Columns.GridColumn gvColConLedgerName;
        private DevExpress.XtraGrid.GridControl gcLedger;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLedger;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colSelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkLedgerSelect;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerCode;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerName;
        private DevExpress.XtraGrid.Columns.GridColumn colGroup;
        private DevExpress.XtraGrid.Columns.GridColumn colNature;
        private DevExpress.XtraGrid.Columns.GridColumn colOPAmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtOpBalance;
        private DevExpress.XtraGrid.Columns.GridColumn colOPAmountTransMode;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox rcboType;
        private DevExpress.XtraEditors.TextEdit txtOpeningIEBalance;
        private DevExpress.XtraEditors.ComboBoxEdit cboTransMode;
        private DevExpress.XtraLayout.LayoutControlGroup lcgFixedAssetDetails;
        private DevExpress.XtraGrid.GridControl gcCYFADepreciation;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCYFADepreciation;
        private DevExpress.XtraGrid.Columns.GridColumn colFALedgerid;
        private DevExpress.XtraGrid.Columns.GridColumn colFALedgerCode;
        private DevExpress.XtraGrid.Columns.GridColumn colFALedger;
        private DevExpress.XtraGrid.Columns.GridColumn colFAConOpAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colFAConDebit;
        private DevExpress.XtraGrid.Columns.GridColumn colFAConCredit;
        private DevExpress.XtraGrid.Columns.GridColumn colFAConCLBalance;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraLayout.LayoutControlItem lcFixedAssetLedgers;
        private DevExpress.XtraEditors.SimpleButton btnLoadFromFinance;
        private DevExpress.XtraLayout.LayoutControlItem lcResetfromFinance;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repLKPDepreciationLedger;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraLayout.SimpleLabelItem lblFixedAssetTotalCaption;
        private DevExpress.XtraLayout.SimpleLabelItem lblDepreciationTotalCaption;
        private DevExpress.XtraLayout.SimpleLabelItem lblDepreciationAccuTotalCaption;
        private DevExpress.XtraEditors.SimpleButton btnFAUpdate;
        private DevExpress.XtraLayout.LayoutControlItem lcFAUpdate;
        private DevExpress.XtraLayout.SimpleLabelItem lblNote;
        private DevExpress.XtraEditors.CheckEdit chkLedgerSelectAll;
        private DevExpress.XtraLayout.LayoutControlGroup lcgMapWithCongregation;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraLayout.SimpleLabelItem lblRecordCount;
        private DevExpress.XtraLayout.LayoutControlItem lcGeneralateLedgers;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.SimpleLabelItem lblOpBalanceTitle;
        private DevExpress.XtraLayout.SimpleLabelItem lblOpBalance;
        private DevExpress.XtraLayout.LayoutControlItem lcBalanceMode;
        private DevExpress.XtraLayout.LayoutControlItem lcOpeningIEBalance;
        private DevExpress.XtraLayout.SimpleLabelItem lblFixedAssetOP;
        private DevExpress.XtraLayout.SimpleLabelItem lblDepreciationOP;
        private DevExpress.XtraLayout.SimpleLabelItem lblDepreciationAccuOP;
        private DevExpress.XtraLayout.SimpleLabelItem lblFixedAssetDR;
        private DevExpress.XtraLayout.SimpleLabelItem lblDepreciationDR;
        private DevExpress.XtraLayout.SimpleLabelItem lblDepreciationAccuDR;
        private DevExpress.XtraLayout.SimpleLabelItem lblFixedAssetCR;
        private DevExpress.XtraLayout.SimpleLabelItem lblDepreciationCR;
        private DevExpress.XtraLayout.SimpleLabelItem lblDepreciationAccuCR;
        private DevExpress.XtraLayout.SimpleLabelItem lblFixedAssetTotal;
        private DevExpress.XtraLayout.SimpleLabelItem lblDepreciationTotal;
        private DevExpress.XtraLayout.SimpleLabelItem lblDepreciationAccuTotal;
        private DevExpress.XtraEditors.SimpleButton btnMapLedgers;
        private DevExpress.XtraLayout.LayoutControlItem lcMapLedgers;
        private DevExpress.XtraEditors.SimpleButton btnShowMapLedgers;
        private DevExpress.XtraLayout.LayoutControlItem lcShowMapLedgers;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem lblFANote;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtDebit;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtCredit;
    }
}