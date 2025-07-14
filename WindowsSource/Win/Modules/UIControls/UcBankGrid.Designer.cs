namespace ACPP.Modules.UIControls
{
    partial class UcCashBankGrid
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcBank = new DevExpress.XtraGrid.GridControl();
            this.gvBank = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSource = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rglkpSource = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgers = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rglkpCashLedger = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.rglpvLedger = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.rcolLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcolLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtAmount = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colCheqNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtCheqNo = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colMaterializedOn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rdeMaterializedOn = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.colLedgerBalance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtLedgerBalance = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colTempAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColIdentification = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcBank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpCashLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglpvLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtCheqNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdeMaterializedOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdeMaterializedOn.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtLedgerBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcBank);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(753, 218);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcBank
            // 
            this.gcBank.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gcBank.Location = new System.Drawing.Point(7, 7);
            this.gcBank.MainView = this.gvBank;
            this.gcBank.Name = "gcBank";
            this.gcBank.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.txtAmount,
            this.rtxtLedgerBalance,
            this.rtxtCheqNo,
            this.rglkpCashLedger,
            this.rglkpSource,
            this.rdeMaterializedOn});
            this.gcBank.Size = new System.Drawing.Size(739, 204);
            this.gcBank.TabIndex = 3;
            this.gcBank.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBank});
            this.gcBank.Visible = false;
            this.gcBank.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.gcBank_ProcessGridKey);
            // 
            // gvBank
            // 
            this.gvBank.Appearance.FocusedCell.BackColor = System.Drawing.Color.LightBlue;
            this.gvBank.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvBank.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvBank.Appearance.FocusedCell.Options.UseFont = true;
            this.gvBank.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvBank.Appearance.FocusedRow.Options.UseFont = true;
            this.gvBank.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvBank.Appearance.FooterPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gvBank.Appearance.FooterPanel.Options.UseFont = true;
            this.gvBank.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvBank.Appearance.GroupFooter.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvBank.Appearance.GroupFooter.Options.UseFont = true;
            this.gvBank.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvBank.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvBank.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvBank.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSource,
            this.colLedgers,
            this.colAmount,
            this.colCheqNo,
            this.colMaterializedOn,
            this.colLedgerBalance,
            this.colTempAmount,
            this.ColIdentification});
            this.gvBank.DetailTabHeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom;
            this.gvBank.GridControl = this.gcBank;
            this.gvBank.Name = "gvBank";
            this.gvBank.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gvBank.OptionsBehavior.FocusLeaveOnTab = true;
            this.gvBank.OptionsBehavior.SummariesIgnoreNullValues = true;
            this.gvBank.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gvBank.OptionsFilter.AllowFilterEditor = false;
            this.gvBank.OptionsFilter.AllowFilterIncrementalSearch = false;
            this.gvBank.OptionsFilter.AllowMRUFilterList = false;
            this.gvBank.OptionsFilter.FilterEditorUseMenuForOperandsAndOperators = false;
            this.gvBank.OptionsFilter.ShowAllTableValuesInCheckedFilterPopup = false;
            this.gvBank.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.True;
            this.gvBank.OptionsNavigation.AutoFocusNewRow = true;
            this.gvBank.OptionsNavigation.EnterMoveNextColumn = true;
            this.gvBank.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gvBank.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvBank.OptionsView.BestFitMaxRowCount = 2;
            this.gvBank.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.gvBank.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gvBank.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gvBank.OptionsView.ShowFooter = true;
            this.gvBank.OptionsView.ShowGroupPanel = false;
            this.gvBank.OptionsView.ShowIndicator = false;
            this.gvBank.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvBank_RowCellStyle);
            this.gvBank.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gvBank_ShowingEditor);
            this.gvBank.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.gvBank_InvalidRowException);
            this.gvBank.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvBank_ValidateRow);
            this.gvBank.GotFocus += new System.EventHandler(this.gvBank_GotFocus);
            // 
            // colSource
            // 
            this.colSource.ColumnEdit = this.rglkpSource;
            this.colSource.FieldName = "SOURCE";
            this.colSource.Name = "colSource";
            this.colSource.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colSource.OptionsColumn.FixedWidth = true;
            this.colSource.OptionsColumn.ShowCaption = false;
            this.colSource.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedCell;
            this.colSource.Visible = true;
            this.colSource.VisibleIndex = 0;
            this.colSource.Width = 42;
            // 
            // rglkpSource
            // 
            this.rglkpSource.AutoHeight = false;
            this.rglkpSource.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.rglkpSource.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rglkpSource.DisplayFormat.FormatString = "C2";
            this.rglkpSource.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rglkpSource.Name = "rglkpSource";
            this.rglkpSource.NullText = "";
            this.rglkpSource.PopupFormMinSize = new System.Drawing.Size(10, 10);
            this.rglkpSource.PopupFormSize = new System.Drawing.Size(25, 40);
            this.rglkpSource.PopupSizeable = false;
            this.rglkpSource.ShowPopupShadow = false;
            this.rglkpSource.View = this.repositoryItemGridLookUpEdit1View;
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colName});
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colId
            // 
            this.colId.Caption = "Id";
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            // 
            // colName
            // 
            this.colName.Caption = "Name";
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            // 
            // colLedgers
            // 
            this.colLedgers.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.colLedgers.AppearanceCell.Options.UseBackColor = true;
            this.colLedgers.Caption = "Ledgers";
            this.colLedgers.ColumnEdit = this.rglkpCashLedger;
            this.colLedgers.FieldName = "LEDGER_ID";
            this.colLedgers.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText;
            this.colLedgers.Name = "colLedgers";
            this.colLedgers.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colLedgers.SortMode = DevExpress.XtraGrid.ColumnSortMode.DisplayText;
            this.colLedgers.Visible = true;
            this.colLedgers.VisibleIndex = 1;
            this.colLedgers.Width = 313;
            // 
            // rglkpCashLedger
            // 
            this.rglkpCashLedger.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.rglkpCashLedger.Appearance.Options.UseBackColor = true;
            this.rglkpCashLedger.AutoHeight = false;
            this.rglkpCashLedger.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.rglkpCashLedger.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.rglkpCashLedger.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rglkpCashLedger.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F12);
            this.rglkpCashLedger.ImmediatePopup = true;
            this.rglkpCashLedger.Name = "rglkpCashLedger";
            this.rglkpCashLedger.NullText = "";
            this.rglkpCashLedger.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.rglkpCashLedger.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.rglkpCashLedger.View = this.rglpvLedger;
            this.rglkpCashLedger.EditValueChanged += new System.EventHandler(this.rglkpCashLedger_EditValueChanged);
            this.rglkpCashLedger.Leave += new System.EventHandler(this.rglkpCashLedger_Leave);
            this.rglkpCashLedger.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rglkpCashLedger_MouseDown);
            this.rglkpCashLedger.Validating += new System.ComponentModel.CancelEventHandler(this.rglkpCashLedger_Validating);
            // 
            // rglpvLedger
            // 
            this.rglpvLedger.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.rcolLedgerId,
            this.rcolLedgerName});
            this.rglpvLedger.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.rglpvLedger.Name = "rglpvLedger";
            this.rglpvLedger.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.rglpvLedger.OptionsView.ShowColumnHeaders = false;
            this.rglpvLedger.OptionsView.ShowGroupPanel = false;
            this.rglpvLedger.OptionsView.ShowIndicator = false;
            // 
            // rcolLedgerId
            // 
            this.rcolLedgerId.Caption = "Ledger Id";
            this.rcolLedgerId.FieldName = "LEDGER_ID";
            this.rcolLedgerId.Name = "rcolLedgerId";
            // 
            // rcolLedgerName
            // 
            this.rcolLedgerName.Caption = "Ledger Name";
            this.rcolLedgerName.FieldName = "LEDGER_NAME";
            this.rcolLedgerName.Name = "rcolLedgerName";
            this.rcolLedgerName.Visible = true;
            this.rcolLedgerName.VisibleIndex = 0;
            // 
            // colAmount
            // 
            this.colAmount.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.colAmount.AppearanceCell.Options.UseBackColor = true;
            this.colAmount.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colAmount.AppearanceHeader.Options.UseFont = true;
            this.colAmount.Caption = "Amount";
            this.colAmount.ColumnEdit = this.txtAmount;
            this.colAmount.FieldName = "AMOUNT";
            this.colAmount.GroupFormat.FormatString = "C";
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.FixedWidth = true;
            this.colAmount.OptionsFilter.AllowAutoFilter = false;
            this.colAmount.OptionsFilter.AllowFilter = false;
            this.colAmount.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedCell;
            this.colAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "AMOUNT", "{0:C}")});
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 2;
            this.colAmount.Width = 96;
            // 
            // txtAmount
            // 
            this.txtAmount.AutoHeight = false;
            this.txtAmount.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtAmount.Mask.EditMask = "n";
            this.txtAmount.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtAmount.Mask.UseMaskAsDisplayFormat = true;
            this.txtAmount.MaxLength = 14;
            this.txtAmount.Name = "txtAmount";
            // 
            // colCheqNo
            // 
            this.colCheqNo.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.colCheqNo.AppearanceCell.Options.UseBackColor = true;
            this.colCheqNo.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colCheqNo.AppearanceHeader.Options.UseFont = true;
            this.colCheqNo.Caption = "Ref. No (DD/Cheque)";
            this.colCheqNo.ColumnEdit = this.rtxtCheqNo;
            this.colCheqNo.FieldName = "CHEQUE_NO";
            this.colCheqNo.GroupFormat.FormatString = "C";
            this.colCheqNo.Name = "colCheqNo";
            this.colCheqNo.OptionsColumn.FixedWidth = true;
            this.colCheqNo.OptionsFilter.AllowAutoFilter = false;
            this.colCheqNo.OptionsFilter.AllowFilter = false;
            this.colCheqNo.Width = 103;
            // 
            // rtxtCheqNo
            // 
            this.rtxtCheqNo.AutoHeight = false;
            this.rtxtCheqNo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.rtxtCheqNo.Mask.EditMask = "n";
            this.rtxtCheqNo.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rtxtCheqNo.Mask.UseMaskAsDisplayFormat = true;
            this.rtxtCheqNo.MaxLength = 14;
            this.rtxtCheqNo.Name = "rtxtCheqNo";
            // 
            // colMaterializedOn
            // 
            this.colMaterializedOn.Caption = "Materialized On";
            this.colMaterializedOn.ColumnEdit = this.rdeMaterializedOn;
            this.colMaterializedOn.FieldName = "MATERIALIZED_ON";
            this.colMaterializedOn.Name = "colMaterializedOn";
            // 
            // rdeMaterializedOn
            // 
            this.rdeMaterializedOn.AutoHeight = false;
            this.rdeMaterializedOn.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rdeMaterializedOn.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rdeMaterializedOn.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.rdeMaterializedOn.Mask.UseMaskAsDisplayFormat = true;
            this.rdeMaterializedOn.Name = "rdeMaterializedOn";
            // 
            // colLedgerBalance
            // 
            this.colLedgerBalance.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colLedgerBalance.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.colLedgerBalance.AppearanceCell.Options.UseFont = true;
            this.colLedgerBalance.AppearanceCell.Options.UseForeColor = true;
            this.colLedgerBalance.Caption = "Current Balance";
            this.colLedgerBalance.ColumnEdit = this.rtxtLedgerBalance;
            this.colLedgerBalance.FieldName = "LEDGER_BALANCE";
            this.colLedgerBalance.Name = "colLedgerBalance";
            this.colLedgerBalance.OptionsColumn.AllowEdit = false;
            this.colLedgerBalance.OptionsColumn.AllowFocus = false;
            this.colLedgerBalance.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colLedgerBalance.OptionsColumn.AllowMove = false;
            this.colLedgerBalance.OptionsColumn.AllowSize = false;
            this.colLedgerBalance.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colLedgerBalance.OptionsColumn.FixedWidth = true;
            this.colLedgerBalance.OptionsColumn.ReadOnly = true;
            this.colLedgerBalance.OptionsFilter.AllowAutoFilter = false;
            this.colLedgerBalance.OptionsFilter.AllowFilter = false;
            this.colLedgerBalance.Visible = true;
            this.colLedgerBalance.VisibleIndex = 3;
            this.colLedgerBalance.Width = 183;
            // 
            // rtxtLedgerBalance
            // 
            this.rtxtLedgerBalance.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.rtxtLedgerBalance.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.rtxtLedgerBalance.Appearance.Options.UseFont = true;
            this.rtxtLedgerBalance.Appearance.Options.UseForeColor = true;
            this.rtxtLedgerBalance.AutoHeight = false;
            this.rtxtLedgerBalance.Name = "rtxtLedgerBalance";
            // 
            // colTempAmount
            // 
            this.colTempAmount.Caption = "Debit";
            this.colTempAmount.FieldName = "TEMP_AMOUNT";
            this.colTempAmount.Name = "colTempAmount";
            // 
            // ColIdentification
            // 
            this.ColIdentification.FieldName = "VALUE";
            this.ColIdentification.Name = "ColIdentification";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.Size = new System.Drawing.Size(753, 218);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcBank;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(743, 208);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // UcCashBankGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "UcCashBankGrid";
            this.Size = new System.Drawing.Size(753, 218);
            this.Load += new System.EventHandler(this.UcCashBankGrid_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcBank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpCashLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglpvLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtCheqNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdeMaterializedOn.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdeMaterializedOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtLedgerBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcBank;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBank;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerBalance;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtLedgerBalance;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit txtAmount;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colCheqNo;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtCheqNo;
        private DevExpress.XtraGrid.Columns.GridColumn colMaterializedOn;
        private DevExpress.XtraGrid.Columns.GridColumn colTempAmount;
        private DevExpress.XtraGrid.Columns.GridColumn ColIdentification;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgers;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkpCashLedger;
        private DevExpress.XtraGrid.Views.Grid.GridView rglpvLedger;
        private DevExpress.XtraGrid.Columns.GridColumn rcolLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn rcolLedgerName;
        private DevExpress.XtraGrid.Columns.GridColumn colSource;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkpSource;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit rdeMaterializedOn;
    }
}
