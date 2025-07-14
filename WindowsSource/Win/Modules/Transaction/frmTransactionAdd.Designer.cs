namespace ACPP.Modules.Transaction
{
    partial class frmTransactionAdd
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
            this.pnlFill = new DevExpress.XtraEditors.PanelControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtExchangeRate = new DevExpress.XtraEditors.TextEdit();
            this.txtAmount = new DevExpress.XtraEditors.TextEdit();
            this.rgTransactionType = new DevExpress.XtraEditors.RadioGroup();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.txtNarration = new DevExpress.XtraEditors.TextEdit();
            this.glkpPurpose = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colPurpose = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPurpos = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkpReceiptType = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colReceiptId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReceiptType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkpDonor = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDonorId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.gcTransaction = new DevExpress.XtraGrid.GridControl();
            this.gvTransaction = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSource = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rglkpSource = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSource1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSourceName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFlag = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rglkpFlag = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colFlag1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFlagC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedger = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rglkpLedger = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheque = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReconciled = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtDebit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.rcboSource = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.rcboFlag = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.glkpProject = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtVoucher = new DevExpress.XtraEditors.TextEdit();
            this.dteTransactionDate = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.gbMasterInfo = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblTransactionDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblVoucher = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblDonor = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblPurpose = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblReceiptType = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblAmount = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblExchangeRate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblActualAmount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblSymbol = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblActualAmt = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblCurrencySymbol = new DevExpress.XtraLayout.SimpleLabelItem();
            this.esiPurpose = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.esiDonor = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblProject = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblNarration = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblTransactionType = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFill)).BeginInit();
            this.pnlFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtExchangeRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgTransactionType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNarration.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpPurpose.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpReceiptType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpDonor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTransaction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTransaction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpFlag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtDebit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcboSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcboFlag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoucher.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTransactionDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTransactionDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbMasterInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTransactionDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVoucher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDonor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPurpose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReceiptType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblExchangeRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblActualAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSymbol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblActualAmt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCurrencySymbol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiPurpose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiDonor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNarration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTransactionType)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlFill
            // 
            this.pnlFill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlFill.Controls.Add(this.layoutControl1);
            this.pnlFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFill.Location = new System.Drawing.Point(5, 5);
            this.pnlFill.Name = "pnlFill";
            this.pnlFill.Size = new System.Drawing.Size(677, 381);
            this.pnlFill.TabIndex = 1;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtExchangeRate);
            this.layoutControl1.Controls.Add(this.txtAmount);
            this.layoutControl1.Controls.Add(this.rgTransactionType);
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.txtNarration);
            this.layoutControl1.Controls.Add(this.glkpPurpose);
            this.layoutControl1.Controls.Add(this.glkpReceiptType);
            this.layoutControl1.Controls.Add(this.glkpDonor);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.gcTransaction);
            this.layoutControl1.Controls.Add(this.glkpProject);
            this.layoutControl1.Controls.Add(this.txtVoucher);
            this.layoutControl1.Controls.Add(this.dteTransactionDate);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(120, 358, 263, 350);
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(677, 381);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtExchangeRate
            // 
            this.txtExchangeRate.EditValue = "1";
            this.txtExchangeRate.Location = new System.Drawing.Point(463, 147);
            this.txtExchangeRate.Name = "txtExchangeRate";
            this.txtExchangeRate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtExchangeRate.Size = new System.Drawing.Size(71, 20);
            this.txtExchangeRate.StyleController = this.layoutControl1;
            this.txtExchangeRate.TabIndex = 19;
            this.txtExchangeRate.TextChanged += new System.EventHandler(this.txtExchangeRate_TextChanged);
            // 
            // txtAmount
            // 
            this.txtAmount.EditValue = "0.00";
            this.txtAmount.Location = new System.Drawing.Point(93, 147);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtAmount.Size = new System.Drawing.Size(111, 20);
            this.txtAmount.StyleController = this.layoutControl1;
            this.txtAmount.TabIndex = 18;
            this.txtAmount.TextChanged += new System.EventHandler(this.txtAmount_TextChanged);
            // 
            // rgTransactionType
            // 
            this.rgTransactionType.EditValue = true;
            this.rgTransactionType.Location = new System.Drawing.Point(92, 23);
            this.rgTransactionType.Name = "rgTransactionType";
            this.rgTransactionType.Properties.Appearance.BackColor = System.Drawing.Color.GhostWhite;
            this.rgTransactionType.Properties.Appearance.Options.UseBackColor = true;
            this.rgTransactionType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.rgTransactionType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "Receipts"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "Payments"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "Contra")});
            this.rgTransactionType.Size = new System.Drawing.Size(380, 25);
            this.rgTransactionType.StyleController = this.layoutControl1;
            this.rgTransactionType.TabIndex = 17;
            this.rgTransactionType.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(618, 359);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(59, 22);
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.TabIndex = 16;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtNarration
            // 
            this.txtNarration.Location = new System.Drawing.Point(55, 361);
            this.txtNarration.Name = "txtNarration";
            this.txtNarration.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtNarration.Size = new System.Drawing.Size(479, 20);
            this.txtNarration.StyleController = this.layoutControl1;
            this.txtNarration.TabIndex = 15;
            // 
            // glkpPurpose
            // 
            this.glkpPurpose.EditValue = "";
            this.glkpPurpose.Location = new System.Drawing.Point(93, 124);
            this.glkpPurpose.Name = "glkpPurpose";
            this.glkpPurpose.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpPurpose.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkpPurpose.Properties.NullText = "";
            this.glkpPurpose.Properties.PopupFormSize = new System.Drawing.Size(257, 10);
            this.glkpPurpose.Properties.ShowFooter = false;
            this.glkpPurpose.Properties.View = this.gridLookUpEdit2View;
            this.glkpPurpose.Size = new System.Drawing.Size(257, 20);
            this.glkpPurpose.StyleController = this.layoutControl1;
            this.glkpPurpose.TabIndex = 14;
            // 
            // gridLookUpEdit2View
            // 
            this.gridLookUpEdit2View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPurpose,
            this.colPurpos});
            this.gridLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit2View.Name = "gridLookUpEdit2View";
            this.gridLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit2View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit2View.OptionsView.ShowIndicator = false;
            // 
            // colPurpose
            // 
            this.colPurpose.Caption = "PurposeId";
            this.colPurpose.FieldName = "CONTRIBUTION_HEAD_ID";
            this.colPurpose.Name = "colPurpose";
            // 
            // colPurpos
            // 
            this.colPurpos.Caption = "Purpose";
            this.colPurpos.FieldName = "HEAD";
            this.colPurpos.Name = "colPurpos";
            this.colPurpos.Visible = true;
            this.colPurpos.VisibleIndex = 0;
            // 
            // glkpReceiptType
            // 
            this.glkpReceiptType.Location = new System.Drawing.Point(462, 101);
            this.glkpReceiptType.Name = "glkpReceiptType";
            this.glkpReceiptType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpReceiptType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkpReceiptType.Properties.NullText = "";
            this.glkpReceiptType.Properties.PopupFormSize = new System.Drawing.Size(209, 20);
            this.glkpReceiptType.Properties.ShowFooter = false;
            this.glkpReceiptType.Properties.View = this.gridView2;
            this.glkpReceiptType.Size = new System.Drawing.Size(209, 20);
            this.glkpReceiptType.StyleController = this.layoutControl1;
            this.glkpReceiptType.TabIndex = 13;
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colReceiptId,
            this.colReceiptType});
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowColumnHeaders = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.OptionsView.ShowIndicator = false;
            // 
            // colReceiptId
            // 
            this.colReceiptId.Caption = "Id";
            this.colReceiptId.FieldName = "Id";
            this.colReceiptId.Name = "colReceiptId";
            // 
            // colReceiptType
            // 
            this.colReceiptType.Caption = "Receipt Type";
            this.colReceiptType.FieldName = "ReceiptType";
            this.colReceiptType.Name = "colReceiptType";
            this.colReceiptType.Visible = true;
            this.colReceiptType.VisibleIndex = 0;
            // 
            // glkpDonor
            // 
            this.glkpDonor.EditValue = "";
            this.glkpDonor.Location = new System.Drawing.Point(93, 101);
            this.glkpDonor.Name = "glkpDonor";
            this.glkpDonor.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpDonor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkpDonor.Properties.NullText = "";
            this.glkpDonor.Properties.PopupFormSize = new System.Drawing.Size(258, 20);
            this.glkpDonor.Properties.ShowFooter = false;
            this.glkpDonor.Properties.View = this.gridView1;
            this.glkpDonor.Size = new System.Drawing.Size(258, 20);
            this.glkpDonor.StyleController = this.layoutControl1;
            this.glkpDonor.TabIndex = 12;
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDonorId,
            this.colName});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowColumnHeaders = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            // 
            // colDonorId
            // 
            this.colDonorId.Caption = "DonorId";
            this.colDonorId.FieldName = "DONAUD_ID";
            this.colDonorId.Name = "colDonorId";
            // 
            // colName
            // 
            this.colName.Caption = "Donor";
            this.colName.FieldName = "NAME";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(557, 359);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(58, 22);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "&Save";
            // 
            // gcTransaction
            // 
            this.gcTransaction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.gcTransaction.Location = new System.Drawing.Point(6, 170);
            this.gcTransaction.MainView = this.gvTransaction;
            this.gcTransaction.Name = "gcTransaction";
            this.gcTransaction.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.gcTransaction.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcboSource,
            this.rcboFlag,
            this.rglkpLedger,
            this.rtxtDebit,
            this.rglkpFlag,
            this.rglkpSource});
            this.gcTransaction.Size = new System.Drawing.Size(665, 180);
            this.gcTransaction.TabIndex = 7;
            this.gcTransaction.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTransaction});
            // 
            // gvTransaction
            // 
            this.gvTransaction.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvTransaction.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvTransaction.BestFitMaxRowCount = 2;
            this.gvTransaction.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSource,
            this.colFlag,
            this.colLedger,
            this.colCheque,
            this.colReconciled,
            this.colAmount});
            this.gvTransaction.GridControl = this.gcTransaction;
            this.gvTransaction.Name = "gvTransaction";
            this.gvTransaction.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvTransaction.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gvTransaction.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvTransaction.OptionsView.ShowGroupPanel = false;
            this.gvTransaction.OptionsView.ShowIndicator = false;
            this.gvTransaction.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gvTransaction.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvTransaction_FocusedRowChanged);
            // 
            // colSource
            // 
            this.colSource.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colSource.AppearanceHeader.Options.UseFont = true;
            this.colSource.Caption = ".";
            this.colSource.ColumnEdit = this.rglkpSource;
            this.colSource.FieldName = "SOURCE";
            this.colSource.Name = "colSource";
            this.colSource.OptionsColumn.FixedWidth = true;
            this.colSource.Visible = true;
            this.colSource.VisibleIndex = 0;
            this.colSource.Width = 44;
            // 
            // rglkpSource
            // 
            this.rglkpSource.AutoHeight = false;
            this.rglkpSource.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rglkpSource.Name = "rglkpSource";
            this.rglkpSource.NullText = "";
            this.rglkpSource.PopupFormMinSize = new System.Drawing.Size(10, 0);
            this.rglkpSource.PopupFormSize = new System.Drawing.Size(44, 10);
            this.rglkpSource.View = this.gridView4;
            this.rglkpSource.EditValueChanged += new System.EventHandler(this.rglkpSource_EditValueChanged);
            // 
            // gridView4
            // 
            this.gridView4.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSource1,
            this.colSourceName});
            this.gridView4.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView4.Name = "gridView4";
            this.gridView4.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView4.OptionsView.ShowColumnHeaders = false;
            this.gridView4.OptionsView.ShowGroupPanel = false;
            this.gridView4.OptionsView.ShowIndicator = false;
            // 
            // colSource1
            // 
            this.colSource1.Caption = "SourceId";
            this.colSource1.FieldName = "Id";
            this.colSource1.Name = "colSource1";
            // 
            // colSourceName
            // 
            this.colSourceName.Caption = ".";
            this.colSourceName.FieldName = "Name";
            this.colSourceName.Name = "colSourceName";
            this.colSourceName.Visible = true;
            this.colSourceName.VisibleIndex = 0;
            // 
            // colFlag
            // 
            this.colFlag.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colFlag.AppearanceHeader.Options.UseFont = true;
            this.colFlag.Caption = "Flag";
            this.colFlag.ColumnEdit = this.rglkpFlag;
            this.colFlag.FieldName = "FLAG";
            this.colFlag.Name = "colFlag";
            this.colFlag.OptionsColumn.FixedWidth = true;
            this.colFlag.Width = 45;
            // 
            // rglkpFlag
            // 
            this.rglkpFlag.AutoHeight = false;
            this.rglkpFlag.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rglkpFlag.Name = "rglkpFlag";
            this.rglkpFlag.NullText = "";
            this.rglkpFlag.PopupFormMinSize = new System.Drawing.Size(10, 0);
            this.rglkpFlag.PopupFormSize = new System.Drawing.Size(45, 10);
            this.rglkpFlag.View = this.gridView3;
            this.rglkpFlag.EditValueChanged += new System.EventHandler(this.rglkpFlag_EditValueChanged);
            // 
            // gridView3
            // 
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colFlag1,
            this.colFlagC});
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            this.gridView3.OptionsView.ShowIndicator = false;
            // 
            // colFlag1
            // 
            this.colFlag1.Caption = "Flag";
            this.colFlag1.FieldName = "Id";
            this.colFlag1.Name = "colFlag1";
            // 
            // colFlagC
            // 
            this.colFlagC.Caption = "Flag";
            this.colFlagC.FieldName = "Name";
            this.colFlagC.Name = "colFlagC";
            this.colFlagC.Visible = true;
            this.colFlagC.VisibleIndex = 0;
            // 
            // colLedger
            // 
            this.colLedger.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colLedger.AppearanceHeader.Options.UseFont = true;
            this.colLedger.Caption = "Ledger";
            this.colLedger.ColumnEdit = this.rglkpLedger;
            this.colLedger.FieldName = "LEDGER_NAME";
            this.colLedger.Name = "colLedger";
            this.colLedger.Visible = true;
            this.colLedger.VisibleIndex = 1;
            this.colLedger.Width = 298;
            // 
            // rglkpLedger
            // 
            this.rglkpLedger.AutoHeight = false;
            this.rglkpLedger.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rglkpLedger.DisplayMember = "LEDGER_NAME";
            this.rglkpLedger.Name = "rglkpLedger";
            this.rglkpLedger.NullText = "";
            this.rglkpLedger.ShowFooter = false;
            this.rglkpLedger.ValueMember = "LEDGER_ID";
            this.rglkpLedger.View = this.repositoryItemGridLookUpEdit1View;
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLedgerId,
            this.colLedgerName});
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowAutoFilterRow = true;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colLedgerId
            // 
            this.colLedgerId.Caption = "LedgerId";
            this.colLedgerId.FieldName = "LEDGER_ID";
            this.colLedgerId.Name = "colLedgerId";
            // 
            // colLedgerName
            // 
            this.colLedgerName.Caption = "Ledger";
            this.colLedgerName.FieldName = "LEDGER_NAME";
            this.colLedgerName.Name = "colLedgerName";
            this.colLedgerName.Visible = true;
            this.colLedgerName.VisibleIndex = 0;
            // 
            // colCheque
            // 
            this.colCheque.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colCheque.AppearanceHeader.Options.UseFont = true;
            this.colCheque.Caption = "Cheque";
            this.colCheque.FieldName = "CHEQUE";
            this.colCheque.Name = "colCheque";
            this.colCheque.Visible = true;
            this.colCheque.VisibleIndex = 2;
            this.colCheque.Width = 112;
            // 
            // colReconciled
            // 
            this.colReconciled.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colReconciled.AppearanceHeader.Options.UseFont = true;
            this.colReconciled.Caption = "Reconciled On";
            this.colReconciled.FieldName = "RECONCILED_ON";
            this.colReconciled.Name = "colReconciled";
            this.colReconciled.Visible = true;
            this.colReconciled.VisibleIndex = 3;
            this.colReconciled.Width = 91;
            // 
            // colAmount
            // 
            this.colAmount.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colAmount.AppearanceHeader.Options.UseFont = true;
            this.colAmount.Caption = "Amount";
            this.colAmount.ColumnEdit = this.rtxtDebit;
            this.colAmount.FieldName = "AMOUNT";
            this.colAmount.Name = "colAmount";
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 4;
            this.colAmount.Width = 73;
            // 
            // rtxtDebit
            // 
            this.rtxtDebit.AutoHeight = false;
            this.rtxtDebit.Name = "rtxtDebit";
            // 
            // rcboSource
            // 
            this.rcboSource.AutoHeight = false;
            this.rcboSource.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcboSource.Items.AddRange(new object[] {
            "From",
            "To"});
            this.rcboSource.Name = "rcboSource";
            // 
            // rcboFlag
            // 
            this.rcboFlag.AutoHeight = false;
            this.rcboFlag.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcboFlag.Items.AddRange(new object[] {
            "Cash",
            "Bank"});
            this.rcboFlag.Name = "rcboFlag";
            this.rcboFlag.EditValueChanged += new System.EventHandler(this.rcboFlag_EditValueChanged);
            // 
            // glkpProject
            // 
            this.glkpProject.Location = new System.Drawing.Point(92, 0);
            this.glkpProject.Name = "glkpProject";
            this.glkpProject.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkpProject.Properties.NullText = "";
            this.glkpProject.Properties.PopupFormSize = new System.Drawing.Size(380, 20);
            this.glkpProject.Properties.View = this.gridLookUpEdit1View;
            this.glkpProject.Size = new System.Drawing.Size(380, 20);
            this.glkpProject.StyleController = this.layoutControl1;
            this.glkpProject.TabIndex = 6;
            this.glkpProject.EditValueChanged += new System.EventHandler(this.glkpProject_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
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
            this.colProjectId.Caption = "ProjectId";
            this.colProjectId.FieldName = "PROJECT_ID";
            this.colProjectId.Name = "colProjectId";
            // 
            // colProject
            // 
            this.colProject.Caption = "Project";
            this.colProject.FieldName = "PROJECT";
            this.colProject.Name = "colProject";
            this.colProject.Visible = true;
            this.colProject.VisibleIndex = 0;
            // 
            // txtVoucher
            // 
            this.txtVoucher.Location = new System.Drawing.Point(462, 78);
            this.txtVoucher.Name = "txtVoucher";
            this.txtVoucher.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtVoucher.Properties.Mask.EditMask = "c";
            this.txtVoucher.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtVoucher.Size = new System.Drawing.Size(209, 20);
            this.txtVoucher.StyleController = this.layoutControl1;
            this.txtVoucher.TabIndex = 5;
            // 
            // dteTransactionDate
            // 
            this.dteTransactionDate.EditValue = null;
            this.dteTransactionDate.Location = new System.Drawing.Point(93, 78);
            this.dteTransactionDate.Name = "dteTransactionDate";
            this.dteTransactionDate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.dteTransactionDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteTransactionDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteTransactionDate.Size = new System.Drawing.Size(81, 20);
            this.dteTransactionDate.StyleController = this.layoutControl1;
            this.dteTransactionDate.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.gbMasterInfo,
            this.layoutControlItem4,
            this.lblProject,
            this.emptySpaceItem2,
            this.lblNarration,
            this.emptySpaceItem4,
            this.layoutControlItem2,
            this.lblTransactionType});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(677, 381);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // gbMasterInfo
            // 
            this.gbMasterInfo.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gbMasterInfo.AppearanceGroup.Options.UseFont = true;
            this.gbMasterInfo.CustomizationFormText = "Master Details";
            this.gbMasterInfo.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblTransactionDate,
            this.lblVoucher,
            this.lblDonor,
            this.lblPurpose,
            this.lblReceiptType,
            this.layoutControlItem3,
            this.lblAmount,
            this.lblExchangeRate,
            this.lblActualAmount,
            this.lblSymbol,
            this.lblActualAmt,
            this.lblCurrencySymbol,
            this.esiPurpose,
            this.emptySpaceItem1,
            this.esiDonor});
            this.gbMasterInfo.Location = new System.Drawing.Point(0, 48);
            this.gbMasterInfo.Name = "gbMasterInfo";
            this.gbMasterInfo.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.gbMasterInfo.Size = new System.Drawing.Size(677, 308);
            this.gbMasterInfo.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 5, 0);
            this.gbMasterInfo.Text = "Transaction";
            // 
            // lblTransactionDate
            // 
            this.lblTransactionDate.Control = this.dteTransactionDate;
            this.lblTransactionDate.CustomizationFormText = "Transaction Date";
            this.lblTransactionDate.Location = new System.Drawing.Point(0, 0);
            this.lblTransactionDate.Name = "lblTransactionDate";
            this.lblTransactionDate.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblTransactionDate.Size = new System.Drawing.Size(168, 23);
            this.lblTransactionDate.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblTransactionDate.Text = "Transaction Date";
            this.lblTransactionDate.TextSize = new System.Drawing.Size(83, 13);
            // 
            // lblVoucher
            // 
            this.lblVoucher.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblVoucher.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblVoucher.Control = this.txtVoucher;
            this.lblVoucher.CustomizationFormText = "Voucher";
            this.lblVoucher.Location = new System.Drawing.Point(369, 0);
            this.lblVoucher.Name = "lblVoucher";
            this.lblVoucher.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblVoucher.Size = new System.Drawing.Size(296, 23);
            this.lblVoucher.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblVoucher.Text = "Voucher No";
            this.lblVoucher.TextSize = new System.Drawing.Size(83, 13);
            // 
            // lblDonor
            // 
            this.lblDonor.Control = this.glkpDonor;
            this.lblDonor.CustomizationFormText = "Donor";
            this.lblDonor.Location = new System.Drawing.Point(0, 23);
            this.lblDonor.Name = "lblDonor";
            this.lblDonor.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblDonor.Size = new System.Drawing.Size(345, 23);
            this.lblDonor.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblDonor.Text = "Donor";
            this.lblDonor.TextSize = new System.Drawing.Size(83, 13);
            // 
            // lblPurpose
            // 
            this.lblPurpose.Control = this.glkpPurpose;
            this.lblPurpose.CustomizationFormText = "Purpose";
            this.lblPurpose.Location = new System.Drawing.Point(0, 46);
            this.lblPurpose.Name = "lblPurpose";
            this.lblPurpose.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblPurpose.Size = new System.Drawing.Size(344, 23);
            this.lblPurpose.Text = "Purpose";
            this.lblPurpose.TextSize = new System.Drawing.Size(83, 13);
            // 
            // lblReceiptType
            // 
            this.lblReceiptType.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblReceiptType.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblReceiptType.Control = this.glkpReceiptType;
            this.lblReceiptType.CustomizationFormText = "Receipt Type";
            this.lblReceiptType.Location = new System.Drawing.Point(369, 23);
            this.lblReceiptType.Name = "lblReceiptType";
            this.lblReceiptType.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblReceiptType.Size = new System.Drawing.Size(296, 23);
            this.lblReceiptType.Text = "Receipt Type";
            this.lblReceiptType.TextSize = new System.Drawing.Size(83, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcTransaction;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 92);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem3.Size = new System.Drawing.Size(665, 180);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // lblAmount
            // 
            this.lblAmount.Control = this.txtAmount;
            this.lblAmount.CustomizationFormText = "Amount";
            this.lblAmount.Location = new System.Drawing.Point(0, 69);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblAmount.Size = new System.Drawing.Size(198, 23);
            this.lblAmount.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblAmount.Text = "Amount";
            this.lblAmount.TextSize = new System.Drawing.Size(83, 13);
            // 
            // lblExchangeRate
            // 
            this.lblExchangeRate.Control = this.txtExchangeRate;
            this.lblExchangeRate.CustomizationFormText = "Exchange Rate";
            this.lblExchangeRate.Location = new System.Drawing.Point(376, 69);
            this.lblExchangeRate.Name = "lblExchangeRate";
            this.lblExchangeRate.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 0, 0, 0);
            this.lblExchangeRate.Size = new System.Drawing.Size(152, 23);
            this.lblExchangeRate.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblExchangeRate.Text = "Exchange Rate";
            this.lblExchangeRate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblExchangeRate.TextSize = new System.Drawing.Size(73, 13);
            this.lblExchangeRate.TextToControlDistance = 5;
            // 
            // lblActualAmount
            // 
            this.lblActualAmount.AllowHotTrack = false;
            this.lblActualAmount.CustomizationFormText = "Actual Amount";
            this.lblActualAmount.Location = new System.Drawing.Point(198, 69);
            this.lblActualAmount.Name = "lblActualAmount";
            this.lblActualAmount.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.lblActualAmount.Size = new System.Drawing.Size(80, 23);
            this.lblActualAmount.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblActualAmount.Text = "Actual Amount";
            this.lblActualAmount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblActualAmount.TextSize = new System.Drawing.Size(70, 13);
            // 
            // lblSymbol
            // 
            this.lblSymbol.AllowHotTrack = false;
            this.lblSymbol.CustomizationFormText = "Currency Symbol";
            this.lblSymbol.Location = new System.Drawing.Point(528, 69);
            this.lblSymbol.Name = "lblSymbol";
            this.lblSymbol.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.lblSymbol.Size = new System.Drawing.Size(98, 23);
            this.lblSymbol.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblSymbol.Text = "Currency Symbol";
            this.lblSymbol.TextSize = new System.Drawing.Size(83, 13);
            // 
            // lblActualAmt
            // 
            this.lblActualAmt.AllowHotTrack = false;
            this.lblActualAmt.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblActualAmt.AppearanceItemCaption.Options.UseFont = true;
            this.lblActualAmt.CustomizationFormText = "0.00";
            this.lblActualAmt.Location = new System.Drawing.Point(278, 69);
            this.lblActualAmt.Name = "lblActualAmt";
            this.lblActualAmt.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.lblActualAmt.Size = new System.Drawing.Size(98, 23);
            this.lblActualAmt.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblActualAmt.Text = "0.00";
            this.lblActualAmt.TextSize = new System.Drawing.Size(83, 13);
            // 
            // lblCurrencySymbol
            // 
            this.lblCurrencySymbol.AllowHotTrack = false;
            this.lblCurrencySymbol.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCurrencySymbol.AppearanceItemCaption.Options.UseFont = true;
            this.lblCurrencySymbol.CustomizationFormText = "$";
            this.lblCurrencySymbol.Location = new System.Drawing.Point(626, 69);
            this.lblCurrencySymbol.Name = "lblCurrencySymbol";
            this.lblCurrencySymbol.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblCurrencySymbol.Size = new System.Drawing.Size(39, 23);
            this.lblCurrencySymbol.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblCurrencySymbol.Text = "$";
            this.lblCurrencySymbol.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblCurrencySymbol.TextSize = new System.Drawing.Size(7, 13);
            // 
            // esiPurpose
            // 
            this.esiPurpose.AllowHotTrack = false;
            this.esiPurpose.CustomizationFormText = "emptySpaceItem6";
            this.esiPurpose.Location = new System.Drawing.Point(344, 46);
            this.esiPurpose.Name = "esiPurpose";
            this.esiPurpose.Size = new System.Drawing.Size(321, 23);
            this.esiPurpose.Text = "esiPurpose";
            this.esiPurpose.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(168, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(201, 23);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // esiDonor
            // 
            this.esiDonor.AllowHotTrack = false;
            this.esiDonor.CustomizationFormText = "emptySpaceItem3";
            this.esiDonor.Location = new System.Drawing.Point(345, 23);
            this.esiDonor.Name = "esiDonor";
            this.esiDonor.Size = new System.Drawing.Size(24, 23);
            this.esiDonor.Text = "esiDonor";
            this.esiDonor.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnSave;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(557, 356);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 0);
            this.layoutControlItem4.Size = new System.Drawing.Size(58, 25);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // lblProject
            // 
            this.lblProject.Control = this.glkpProject;
            this.lblProject.CustomizationFormText = "Project";
            this.lblProject.Location = new System.Drawing.Point(0, 0);
            this.lblProject.MaxSize = new System.Drawing.Size(472, 23);
            this.lblProject.MinSize = new System.Drawing.Size(472, 23);
            this.lblProject.Name = "lblProject";
            this.lblProject.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.lblProject.Size = new System.Drawing.Size(472, 23);
            this.lblProject.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblProject.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblProject.Text = "Project";
            this.lblProject.TextSize = new System.Drawing.Size(83, 13);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(472, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(205, 48);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblNarration
            // 
            this.lblNarration.Control = this.txtNarration;
            this.lblNarration.CustomizationFormText = "Narration";
            this.lblNarration.Location = new System.Drawing.Point(0, 356);
            this.lblNarration.Name = "lblNarration";
            this.lblNarration.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 5, 0);
            this.lblNarration.Size = new System.Drawing.Size(534, 25);
            this.lblNarration.Text = "Narration";
            this.lblNarration.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblNarration.TextSize = new System.Drawing.Size(45, 13);
            this.lblNarration.TextToControlDistance = 5;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.CustomizationFormText = "emptySpaceItem4";
            this.emptySpaceItem4.Location = new System.Drawing.Point(534, 356);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem4.Size = new System.Drawing.Size(23, 25);
            this.emptySpaceItem4.Text = "emptySpaceItem4";
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnClose;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(615, 356);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 0, 3, 0);
            this.layoutControlItem2.Size = new System.Drawing.Size(62, 25);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // lblTransactionType
            // 
            this.lblTransactionType.Control = this.rgTransactionType;
            this.lblTransactionType.CustomizationFormText = "Transaction Type";
            this.lblTransactionType.Location = new System.Drawing.Point(0, 23);
            this.lblTransactionType.MaxSize = new System.Drawing.Size(472, 25);
            this.lblTransactionType.MinSize = new System.Drawing.Size(472, 25);
            this.lblTransactionType.Name = "lblTransactionType";
            this.lblTransactionType.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.lblTransactionType.Size = new System.Drawing.Size(472, 25);
            this.lblTransactionType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblTransactionType.Text = "Transaction Type";
            this.lblTransactionType.TextSize = new System.Drawing.Size(83, 13);
            // 
            // frmTransactionAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 391);
            this.Controls.Add(this.pnlFill);
            this.Name = "frmTransactionAdd";
            this.Text = "Transaction Entry";
            this.Load += new System.EventHandler(this.frmTransactionAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlFill)).EndInit();
            this.pnlFill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtExchangeRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgTransactionType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNarration.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpPurpose.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpReceiptType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpDonor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTransaction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTransaction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpFlag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtDebit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcboSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcboFlag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoucher.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTransactionDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTransactionDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbMasterInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTransactionDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVoucher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDonor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPurpose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReceiptType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblExchangeRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblActualAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSymbol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblActualAmt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCurrencySymbol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiPurpose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiDonor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNarration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTransactionType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlFill;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gcTransaction;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTransaction;
        private DevExpress.XtraEditors.GridLookUpEdit glkpProject;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraEditors.TextEdit txtVoucher;
        private DevExpress.XtraEditors.DateEdit dteTransactionDate;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.Columns.GridColumn colSource;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox rcboSource;
        private DevExpress.XtraGrid.Columns.GridColumn colFlag;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox rcboFlag;
        private DevExpress.XtraGrid.Columns.GridColumn colLedger;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkpLedger;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colCheque;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup gbMasterInfo;
        private DevExpress.XtraLayout.LayoutControlItem lblTransactionDate;
        private DevExpress.XtraLayout.LayoutControlItem lblProject;
        private DevExpress.XtraLayout.LayoutControlItem lblVoucher;
        private DevExpress.XtraEditors.GridLookUpEdit glkpPurpose;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit2View;
        private DevExpress.XtraEditors.GridLookUpEdit glkpReceiptType;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.GridLookUpEdit glkpDonor;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectId;
        private DevExpress.XtraGrid.Columns.GridColumn colProject;
        private DevExpress.XtraLayout.LayoutControlItem lblDonor;
        private DevExpress.XtraLayout.LayoutControlItem lblPurpose;
        private DevExpress.XtraLayout.LayoutControlItem lblReceiptType;
        private DevExpress.XtraEditors.TextEdit txtNarration;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem lblNarration;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraEditors.RadioGroup rgTransactionType;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem lblTransactionType;
        private DevExpress.XtraEditors.TextEdit txtExchangeRate;
        private DevExpress.XtraEditors.TextEdit txtAmount;
        private DevExpress.XtraLayout.LayoutControlItem lblAmount;
        private DevExpress.XtraLayout.LayoutControlItem lblExchangeRate;
        private DevExpress.XtraLayout.SimpleLabelItem lblActualAmount;
        private DevExpress.XtraLayout.SimpleLabelItem lblSymbol;
        private DevExpress.XtraLayout.SimpleLabelItem lblActualAmt;
        private DevExpress.XtraLayout.SimpleLabelItem lblCurrencySymbol;
        private DevExpress.XtraLayout.EmptySpaceItem esiPurpose;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem esiDonor;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtDebit;
        private DevExpress.XtraGrid.Columns.GridColumn colReconciled;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkpSource;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkpFlag;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Columns.GridColumn colSource1;
        private DevExpress.XtraGrid.Columns.GridColumn colSourceName;
        private DevExpress.XtraGrid.Columns.GridColumn colFlag1;
        private DevExpress.XtraGrid.Columns.GridColumn colFlagC;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerName;
        private DevExpress.XtraGrid.Columns.GridColumn colPurpose;
        private DevExpress.XtraGrid.Columns.GridColumn colPurpos;
        private DevExpress.XtraGrid.Columns.GridColumn colDonorId;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colReceiptId;
        private DevExpress.XtraGrid.Columns.GridColumn colReceiptType;
    }
}