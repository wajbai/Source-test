namespace ACPP.Modules.UIControls
{
    partial class UcPendingTransaction
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
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gcPendingTransaction = new DevExpress.XtraGrid.GridControl();
            this.gvPendingTransaction = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoucherNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoucherDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTDSLedger = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNatureofPayments = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPartyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssessValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBalance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTransMode1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboFlag = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.colVoucherId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTDSLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTransMode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBookingId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBookingDetailId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkSelect = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPendingTransaction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPendingTransaction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFlag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridView1
            // 
            this.gridView1.Name = "gridView1";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(796, 248);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(796, 248);
            this.layoutControlGroup2.Text = "layoutControlGroup2";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcPendingTransaction;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(790, 219);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // gcPendingTransaction
            // 
            this.gcPendingTransaction.Location = new System.Drawing.Point(3, 3);
            this.gcPendingTransaction.MainView = this.gvPendingTransaction;
            this.gcPendingTransaction.Name = "gcPendingTransaction";
            this.gcPendingTransaction.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.chkSelect,
            this.cboFlag});
            this.gcPendingTransaction.Size = new System.Drawing.Size(790, 219);
            this.gcPendingTransaction.TabIndex = 2;
            this.gcPendingTransaction.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPendingTransaction});
            this.gcPendingTransaction.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.gcPendingTransaction_PreviewKeyDown);
            // 
            // gvPendingTransaction
            // 
            this.gvPendingTransaction.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvPendingTransaction.Appearance.FocusedRow.Options.UseFont = true;
            this.gvPendingTransaction.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvPendingTransaction.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvPendingTransaction.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvPendingTransaction.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSelect,
            this.colVoucherNo,
            this.colVoucherDate,
            this.colTDSLedger,
            this.colNatureofPayments,
            this.colPartyName,
            this.colAssessValue,
            this.colBalance,
            this.colTransMode1,
            this.colVoucherId,
            this.colLedgerId,
            this.colTDSLedgerId,
            this.colTransMode,
            this.colBookingId,
            this.colBookingDetailId});
            this.gvPendingTransaction.CustomizationFormBounds = new System.Drawing.Rectangle(693, 424, 210, 172);
            this.gvPendingTransaction.GridControl = this.gcPendingTransaction;
            this.gvPendingTransaction.Name = "gvPendingTransaction";
            this.gvPendingTransaction.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gvPendingTransaction.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvPendingTransaction.OptionsSelection.MultiSelect = true;
            this.gvPendingTransaction.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvPendingTransaction.OptionsView.ShowGroupPanel = false;
            this.gvPendingTransaction.OptionsView.ShowIndicator = false;
            // 
            // colSelect
            // 
            this.colSelect.Caption = "Select";
            this.colSelect.FieldName = "SELECT";
            this.colSelect.Name = "colSelect";
            this.colSelect.OptionsColumn.AllowSize = false;
            this.colSelect.OptionsColumn.FixedWidth = true;
            this.colSelect.OptionsColumn.ShowCaption = false;
            this.colSelect.Width = 26;
            // 
            // colVoucherNo
            // 
            this.colVoucherNo.Caption = "V.No";
            this.colVoucherNo.FieldName = "VOUCHER_NO";
            this.colVoucherNo.Name = "colVoucherNo";
            this.colVoucherNo.OptionsColumn.AllowEdit = false;
            this.colVoucherNo.OptionsColumn.AllowFocus = false;
            this.colVoucherNo.OptionsColumn.AllowSize = false;
            this.colVoucherNo.OptionsColumn.FixedWidth = true;
            this.colVoucherNo.Visible = true;
            this.colVoucherNo.VisibleIndex = 1;
            this.colVoucherNo.Width = 54;
            // 
            // colVoucherDate
            // 
            this.colVoucherDate.Caption = "Date";
            this.colVoucherDate.FieldName = "BOOKING_DATE";
            this.colVoucherDate.Name = "colVoucherDate";
            this.colVoucherDate.OptionsColumn.AllowEdit = false;
            this.colVoucherDate.OptionsColumn.AllowFocus = false;
            this.colVoucherDate.OptionsColumn.AllowSize = false;
            this.colVoucherDate.OptionsColumn.FixedWidth = true;
            this.colVoucherDate.Visible = true;
            this.colVoucherDate.VisibleIndex = 2;
            this.colVoucherDate.Width = 80;
            // 
            // colTDSLedger
            // 
            this.colTDSLedger.Caption = "TDS Ledger";
            this.colTDSLedger.FieldName = "TDS_LEDGER";
            this.colTDSLedger.Name = "colTDSLedger";
            this.colTDSLedger.OptionsColumn.AllowEdit = false;
            this.colTDSLedger.OptionsColumn.AllowFocus = false;
            this.colTDSLedger.Visible = true;
            this.colTDSLedger.VisibleIndex = 3;
            this.colTDSLedger.Width = 122;
            // 
            // colNatureofPayments
            // 
            this.colNatureofPayments.Caption = "Nature of Payments";
            this.colNatureofPayments.FieldName = "NATURE_OF_PAYMENT";
            this.colNatureofPayments.Name = "colNatureofPayments";
            this.colNatureofPayments.OptionsColumn.AllowEdit = false;
            this.colNatureofPayments.OptionsColumn.AllowFocus = false;
            this.colNatureofPayments.Visible = true;
            this.colNatureofPayments.VisibleIndex = 4;
            this.colNatureofPayments.Width = 155;
            // 
            // colPartyName
            // 
            this.colPartyName.Caption = "Party Name";
            this.colPartyName.FieldName = "PARTY_LEDGER";
            this.colPartyName.Name = "colPartyName";
            this.colPartyName.OptionsColumn.AllowEdit = false;
            this.colPartyName.OptionsColumn.AllowFocus = false;
            this.colPartyName.Visible = true;
            this.colPartyName.VisibleIndex = 5;
            this.colPartyName.Width = 109;
            // 
            // colAssessValue
            // 
            this.colAssessValue.Caption = "Assess Value";
            this.colAssessValue.DisplayFormat.FormatString = "N";
            this.colAssessValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAssessValue.FieldName = "ASSESS_AMOUNT";
            this.colAssessValue.Name = "colAssessValue";
            this.colAssessValue.OptionsColumn.AllowEdit = false;
            this.colAssessValue.OptionsColumn.AllowFocus = false;
            this.colAssessValue.Visible = true;
            this.colAssessValue.VisibleIndex = 6;
            this.colAssessValue.Width = 89;
            // 
            // colBalance
            // 
            this.colBalance.Caption = "TDS Balance";
            this.colBalance.DisplayFormat.FormatString = "N";
            this.colBalance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBalance.FieldName = "AMOUNT";
            this.colBalance.GroupFormat.FormatString = "N";
            this.colBalance.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBalance.Name = "colBalance";
            this.colBalance.OptionsColumn.AllowEdit = false;
            this.colBalance.OptionsColumn.AllowFocus = false;
            this.colBalance.Visible = true;
            this.colBalance.VisibleIndex = 7;
            this.colBalance.Width = 116;
            // 
            // colTransMode1
            // 
            this.colTransMode1.Caption = "Trans Mode";
            this.colTransMode1.ColumnEdit = this.cboFlag;
            this.colTransMode1.FieldName = "TRANS_MODE";
            this.colTransMode1.Name = "colTransMode1";
            this.colTransMode1.OptionsColumn.AllowSize = false;
            this.colTransMode1.OptionsColumn.FixedWidth = true;
            this.colTransMode1.OptionsColumn.ShowCaption = false;
            this.colTransMode1.Width = 37;
            // 
            // cboFlag
            // 
            this.cboFlag.AutoHeight = false;
            this.cboFlag.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboFlag.Items.AddRange(new object[] {
            "CR",
            "DR"});
            this.cboFlag.Name = "cboFlag";
            // 
            // colVoucherId
            // 
            this.colVoucherId.Caption = "VoucherId";
            this.colVoucherId.FieldName = "VOUCHER_ID";
            this.colVoucherId.Name = "colVoucherId";
            // 
            // colLedgerId
            // 
            this.colLedgerId.Caption = "Ledger Id";
            this.colLedgerId.FieldName = "LEDGER_ID";
            this.colLedgerId.Name = "colLedgerId";
            // 
            // colTDSLedgerId
            // 
            this.colTDSLedgerId.Caption = "TDS Ledger Id";
            this.colTDSLedgerId.FieldName = "TDS_LEDGER_ID";
            this.colTDSLedgerId.Name = "colTDSLedgerId";
            // 
            // colTransMode
            // 
            this.colTransMode.FieldName = "TRANS_MODE";
            this.colTransMode.Name = "colTransMode";
            this.colTransMode.OptionsColumn.AllowEdit = false;
            this.colTransMode.OptionsColumn.AllowFocus = false;
            this.colTransMode.OptionsColumn.ShowCaption = false;
            this.colTransMode.Visible = true;
            this.colTransMode.VisibleIndex = 8;
            this.colTransMode.Width = 39;
            // 
            // colBookingId
            // 
            this.colBookingId.FieldName = "BOOKING_ID";
            this.colBookingId.Name = "colBookingId";
            // 
            // colBookingDetailId
            // 
            this.colBookingDetailId.Caption = "Booking Detail Id";
            this.colBookingDetailId.FieldName = "BOOKING_DETAIL_ID";
            this.colBookingDetailId.Name = "colBookingDetailId";
            // 
            // chkSelect
            // 
            this.chkSelect.AutoHeight = false;
            this.chkSelect.Caption = "Check";
            this.chkSelect.Name = "chkSelect";
            this.chkSelect.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.chkSelect.ValueChecked = 1;
            this.chkSelect.ValueGrayed = 2;
            this.chkSelect.ValueUnchecked = 0;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.chkShowFilter;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 219);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(125, 23);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // chkShowFilter
            // 
            this.chkShowFilter.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Location = new System.Drawing.Point(5, 224);
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = "Sho<b>w</b> Filter";
            this.chkShowFilter.Size = new System.Drawing.Size(121, 19);
            this.chkShowFilter.StyleController = this.layoutControl1;
            this.chkShowFilter.TabIndex = 5;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.gcPendingTransaction);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1040, 127, 250, 342);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(796, 248);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(125, 219);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(665, 23);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // UcPendingTransaction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "UcPendingTransaction";
            this.Size = new System.Drawing.Size(796, 248);
            this.Load += new System.EventHandler(this.UcPendingTransaction_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPendingTransaction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPendingTransaction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFlag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkSelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cboFlag;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        public DevExpress.XtraGrid.GridControl gcPendingTransaction;
        public DevExpress.XtraGrid.Views.Grid.GridView gvPendingTransaction;
        private DevExpress.XtraGrid.Columns.GridColumn colSelect;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherNo;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherDate;
        private DevExpress.XtraGrid.Columns.GridColumn colTDSLedger;
        private DevExpress.XtraGrid.Columns.GridColumn colNatureofPayments;
        private DevExpress.XtraGrid.Columns.GridColumn colPartyName;
        private DevExpress.XtraGrid.Columns.GridColumn colAssessValue;
        private DevExpress.XtraGrid.Columns.GridColumn colBalance;
        private DevExpress.XtraGrid.Columns.GridColumn colTransMode1;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherId;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colTDSLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colTransMode;
        private DevExpress.XtraGrid.Columns.GridColumn colBookingId;
        private DevExpress.XtraGrid.Columns.GridColumn colBookingDetailId;

    }
}
