namespace ACPP.Modules.TDS
{
    partial class frmPendingTDSTransaction
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.gcPendingTDS = new DevExpress.XtraGrid.GridControl();
            this.gvPendingTDS = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colVoucherNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNatureOfPayments = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssessValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTDSBalance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPartyPayments = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTransFlag = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcPendingTDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPendingTDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnOk);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.gcPendingTDS);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(5, 5);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(693, 300);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(557, 276);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 22);
            this.btnOk.StyleController = this.layoutControl1;
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "&Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(626, 276);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Close";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gcPendingTDS
            // 
            this.gcPendingTDS.Location = new System.Drawing.Point(2, 2);
            this.gcPendingTDS.MainView = this.gvPendingTDS;
            this.gcPendingTDS.Name = "gcPendingTDS";
            this.gcPendingTDS.Size = new System.Drawing.Size(689, 270);
            this.gcPendingTDS.TabIndex = 4;
            this.gcPendingTDS.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPendingTDS});
            this.gcPendingTDS.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.gcPendingTDS_ProcessGridKey);
            this.gcPendingTDS.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.gcPendingTDS_PreviewKeyDown);
            // 
            // gvPendingTDS
            // 
            this.gvPendingTDS.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvPendingTDS.Appearance.FocusedRow.Options.UseFont = true;
            this.gvPendingTDS.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvPendingTDS.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvPendingTDS.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colVoucherNo,
            this.colDate,
            this.colNatureOfPayments,
            this.colAssessValue,
            this.colTDSBalance,
            this.colPartyPayments,
            this.colTransFlag});
            this.gvPendingTDS.GridControl = this.gcPendingTDS;
            this.gvPendingTDS.Name = "gvPendingTDS";
            this.gvPendingTDS.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gvPendingTDS.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvPendingTDS.OptionsSelection.MultiSelect = true;
            this.gvPendingTDS.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvPendingTDS.OptionsView.ShowGroupPanel = false;
            this.gvPendingTDS.OptionsView.ShowIndicator = false;
            // 
            // colVoucherNo
            // 
            this.colVoucherNo.Caption = "V.No";
            this.colVoucherNo.FieldName = "VOUCHER_NO";
            this.colVoucherNo.Name = "colVoucherNo";
            this.colVoucherNo.OptionsColumn.AllowEdit = false;
            this.colVoucherNo.OptionsColumn.AllowFocus = false;
            this.colVoucherNo.OptionsColumn.FixedWidth = true;
            this.colVoucherNo.Visible = true;
            this.colVoucherNo.VisibleIndex = 1;
            this.colVoucherNo.Width = 53;
            // 
            // colDate
            // 
            this.colDate.Caption = "Date";
            this.colDate.FieldName = "VOUCHER_DATE";
            this.colDate.Name = "colDate";
            this.colDate.OptionsColumn.AllowEdit = false;
            this.colDate.OptionsColumn.AllowFocus = false;
            this.colDate.OptionsColumn.FixedWidth = true;
            this.colDate.Visible = true;
            this.colDate.VisibleIndex = 2;
            this.colDate.Width = 74;
            // 
            // colNatureOfPayments
            // 
            this.colNatureOfPayments.Caption = "Nature of Payments";
            this.colNatureOfPayments.FieldName = "NATURE_PAYMENTS";
            this.colNatureOfPayments.Name = "colNatureOfPayments";
            this.colNatureOfPayments.OptionsColumn.AllowEdit = false;
            this.colNatureOfPayments.OptionsColumn.AllowFocus = false;
            this.colNatureOfPayments.Visible = true;
            this.colNatureOfPayments.VisibleIndex = 3;
            this.colNatureOfPayments.Width = 218;
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
            this.colAssessValue.VisibleIndex = 4;
            this.colAssessValue.Width = 96;
            // 
            // colTDSBalance
            // 
            this.colTDSBalance.Caption = "TDS Balance";
            this.colTDSBalance.DisplayFormat.FormatString = "N";
            this.colTDSBalance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTDSBalance.FieldName = "BALANCE";
            this.colTDSBalance.GroupFormat.FormatString = "n";
            this.colTDSBalance.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTDSBalance.Name = "colTDSBalance";
            this.colTDSBalance.OptionsColumn.AllowEdit = false;
            this.colTDSBalance.Visible = true;
            this.colTDSBalance.VisibleIndex = 5;
            this.colTDSBalance.Width = 72;
            // 
            // colPartyPayments
            // 
            this.colPartyPayments.Caption = "Payment Amount";
            this.colPartyPayments.DisplayFormat.FormatString = "N";
            this.colPartyPayments.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPartyPayments.FieldName = "AMOUNT";
            this.colPartyPayments.Name = "colPartyPayments";
            this.colPartyPayments.OptionsColumn.AllowEdit = false;
            this.colPartyPayments.OptionsColumn.AllowFocus = false;
            this.colPartyPayments.Visible = true;
            this.colPartyPayments.VisibleIndex = 6;
            this.colPartyPayments.Width = 111;
            // 
            // colTransFlag
            // 
            this.colTransFlag.Caption = "Trans Flag";
            this.colTransFlag.FieldName = "TRANS_MODE";
            this.colTransFlag.Name = "colTransFlag";
            this.colTransFlag.OptionsColumn.AllowEdit = false;
            this.colTransFlag.OptionsColumn.AllowFocus = false;
            this.colTransFlag.OptionsColumn.FixedWidth = true;
            this.colTransFlag.OptionsColumn.ShowCaption = false;
            this.colTransFlag.Visible = true;
            this.colTransFlag.VisibleIndex = 7;
            this.colTransFlag.Width = 38;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "lcgPendingTrans";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(693, 300);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcPendingTDS;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(693, 274);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 274);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(555, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnCancel;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(624, 274);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(69, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(69, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnOk;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(555, 274);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(69, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(69, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // frmPendingTDSTransaction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(703, 310);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmPendingTDSTransaction";
            this.Text = "Pending TDS Transaction";
            this.Load += new System.EventHandler(this.frmPendingTDSTransaction_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcPendingTDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPendingTDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcPendingTDS;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPendingTDS;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherNo;
        private DevExpress.XtraGrid.Columns.GridColumn colDate;
        private DevExpress.XtraGrid.Columns.GridColumn colNatureOfPayments;
        private DevExpress.XtraGrid.Columns.GridColumn colAssessValue;
        private DevExpress.XtraGrid.Columns.GridColumn colTDSBalance;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn colPartyPayments;
        private DevExpress.XtraGrid.Columns.GridColumn colTransFlag;

    }
}