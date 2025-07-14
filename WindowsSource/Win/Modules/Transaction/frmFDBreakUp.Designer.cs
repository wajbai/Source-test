namespace ACPP.Modules.Transaction
{
    partial class frmFDBreakUp
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
            this.lblTotalAmount = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.gcFDBreakUp = new DevExpress.XtraGrid.GridControl();
            this.gvFDBreakUp = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gvColFDNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ritxtFDNo = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gvColInvestedOn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riDateInvested = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.gvColMaturityDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riMaturityDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.gvColAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ritxtAmount = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gvColInterestRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ritxtInterestRate = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gvColInterestAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ritxtInterstAmount = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcFDBreakUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFDBreakUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ritxtFDNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDateInvested)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDateInvested.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riMaturityDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riMaturityDate.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ritxtAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ritxtInterestRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ritxtInterstAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lblTotalAmount);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnOk);
            this.layoutControl1.Controls.Add(this.gcFDBreakUp);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(5, 5);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(129, 268, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(800, 387);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblTotalAmount.Location = new System.Drawing.Point(686, 2);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(112, 20);
            this.lblTotalAmount.StyleController = this.layoutControl1;
            this.lblTotalAmount.TabIndex = 7;
            this.lblTotalAmount.Text = "Amount :";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(723, 363);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(654, 363);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 22);
            this.btnOk.StyleController = this.layoutControl1;
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "&Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // gcFDBreakUp
            // 
            this.gcFDBreakUp.Location = new System.Drawing.Point(2, 26);
            this.gcFDBreakUp.MainView = this.gvFDBreakUp;
            this.gcFDBreakUp.Name = "gcFDBreakUp";
            this.gcFDBreakUp.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riDateInvested,
            this.riMaturityDate,
            this.ritxtAmount,
            this.ritxtInterestRate,
            this.ritxtInterstAmount,
            this.ritxtFDNo});
            this.gcFDBreakUp.Size = new System.Drawing.Size(796, 333);
            this.gcFDBreakUp.TabIndex = 4;
            this.gcFDBreakUp.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvFDBreakUp});
            // 
            // gvFDBreakUp
            // 
            this.gvFDBreakUp.Appearance.FocusedCell.BackColor = System.Drawing.Color.Lavender;
            this.gvFDBreakUp.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvFDBreakUp.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvFDBreakUp.Appearance.FocusedCell.Options.UseFont = true;
            this.gvFDBreakUp.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvFDBreakUp.Appearance.FocusedRow.Options.UseFont = true;
            this.gvFDBreakUp.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvFDBreakUp.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvFDBreakUp.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvFDBreakUp.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvFDBreakUp.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvFDBreakUp.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gvColFDNo,
            this.gvColInvestedOn,
            this.gvColMaturityDate,
            this.gvColAmount,
            this.gvColInterestRate,
            this.gvColInterestAmount});
            this.gvFDBreakUp.GridControl = this.gcFDBreakUp;
            this.gvFDBreakUp.Name = "gvFDBreakUp";
            this.gvFDBreakUp.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gvFDBreakUp.OptionsBehavior.FocusLeaveOnTab = true;
            this.gvFDBreakUp.OptionsBehavior.SummariesIgnoreNullValues = true;
            this.gvFDBreakUp.OptionsNavigation.AutoFocusNewRow = true;
            this.gvFDBreakUp.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvFDBreakUp.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gvFDBreakUp.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gvFDBreakUp.OptionsView.ShowGroupPanel = false;
            this.gvFDBreakUp.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvFDBreakUp_ValidateRow);
            this.gvFDBreakUp.RowCountChanged += new System.EventHandler(this.gvFDBreakUp_RowCountChanged);
            // 
            // gvColFDNo
            // 
            this.gvColFDNo.Caption = "FD No";
            this.gvColFDNo.ColumnEdit = this.ritxtFDNo;
            this.gvColFDNo.FieldName = "FD_NO";
            this.gvColFDNo.Name = "gvColFDNo";
            this.gvColFDNo.Visible = true;
            this.gvColFDNo.VisibleIndex = 0;
            // 
            // ritxtFDNo
            // 
            this.ritxtFDNo.AutoHeight = false;
            this.ritxtFDNo.Name = "ritxtFDNo";
            // 
            // gvColInvestedOn
            // 
            this.gvColInvestedOn.Caption = "Invested On";
            this.gvColInvestedOn.ColumnEdit = this.riDateInvested;
            this.gvColInvestedOn.FieldName = "INVESTED_ON";
            this.gvColInvestedOn.Name = "gvColInvestedOn";
            this.gvColInvestedOn.Visible = true;
            this.gvColInvestedOn.VisibleIndex = 1;
            // 
            // riDateInvested
            // 
            this.riDateInvested.AutoHeight = false;
            this.riDateInvested.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riDateInvested.Name = "riDateInvested";
            this.riDateInvested.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // gvColMaturityDate
            // 
            this.gvColMaturityDate.Caption = "Maturity Date";
            this.gvColMaturityDate.ColumnEdit = this.riMaturityDate;
            this.gvColMaturityDate.FieldName = "MATURITY_DATE";
            this.gvColMaturityDate.Name = "gvColMaturityDate";
            this.gvColMaturityDate.Visible = true;
            this.gvColMaturityDate.VisibleIndex = 2;
            // 
            // riMaturityDate
            // 
            this.riMaturityDate.AutoHeight = false;
            this.riMaturityDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riMaturityDate.Name = "riMaturityDate";
            this.riMaturityDate.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // gvColAmount
            // 
            this.gvColAmount.Caption = "Amount";
            this.gvColAmount.ColumnEdit = this.ritxtAmount;
            this.gvColAmount.FieldName = "AMOUNT";
            this.gvColAmount.Name = "gvColAmount";
            this.gvColAmount.Visible = true;
            this.gvColAmount.VisibleIndex = 3;
            // 
            // ritxtAmount
            // 
            this.ritxtAmount.AutoHeight = false;
            this.ritxtAmount.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.ritxtAmount.Name = "ritxtAmount";
            this.ritxtAmount.Leave += new System.EventHandler(this.ritxtAmount_Leave);
            // 
            // gvColInterestRate
            // 
            this.gvColInterestRate.Caption = "Rate(%)";
            this.gvColInterestRate.ColumnEdit = this.ritxtInterestRate;
            this.gvColInterestRate.FieldName = "INTEREST_RATE";
            this.gvColInterestRate.Name = "gvColInterestRate";
            this.gvColInterestRate.Visible = true;
            this.gvColInterestRate.VisibleIndex = 4;
            // 
            // ritxtInterestRate
            // 
            this.ritxtInterestRate.AutoHeight = false;
            this.ritxtInterestRate.Name = "ritxtInterestRate";
            this.ritxtInterestRate.Leave += new System.EventHandler(this.ritxtInterestRate_Leave);
            // 
            // gvColInterestAmount
            // 
            this.gvColInterestAmount.Caption = "Interest";
            this.gvColInterestAmount.ColumnEdit = this.ritxtInterstAmount;
            this.gvColInterestAmount.FieldName = "INTEREST_AMOUNT";
            this.gvColInterestAmount.Name = "gvColInterestAmount";
            this.gvColInterestAmount.OptionsColumn.AllowFocus = false;
            this.gvColInterestAmount.Visible = true;
            this.gvColInterestAmount.VisibleIndex = 5;
            // 
            // ritxtInterstAmount
            // 
            this.ritxtInterstAmount.AutoHeight = false;
            this.ritxtInterstAmount.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.ritxtInterstAmount.Name = "ritxtInterstAmount";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.emptySpaceItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(800, 387);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcFDBreakUp;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(800, 337);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnOk;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(652, 361);
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
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 361);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(652, 26);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(790, 361);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnCancel;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(721, 361);
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
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lblTotalAmount;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(684, 0);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(67, 17);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(116, 24);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(684, 24);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmFDBreakUp
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(810, 397);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmFDBreakUp";
            this.Text = "Fixed Deposit BreakUp";
            this.Load += new System.EventHandler(this.frmFDBreakUp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcFDBreakUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFDBreakUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ritxtFDNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDateInvested.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDateInvested)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riMaturityDate.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riMaturityDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ritxtAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ritxtInterestRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ritxtInterstAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gcFDBreakUp;
        private DevExpress.XtraGrid.Views.Grid.GridView gvFDBreakUp;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gvColFDNo;
        private DevExpress.XtraGrid.Columns.GridColumn gvColInvestedOn;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit riDateInvested;
        private DevExpress.XtraGrid.Columns.GridColumn gvColMaturityDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit riMaturityDate;
        private DevExpress.XtraGrid.Columns.GridColumn gvColAmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ritxtAmount;
        private DevExpress.XtraGrid.Columns.GridColumn gvColInterestRate;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ritxtInterestRate;
        private DevExpress.XtraGrid.Columns.GridColumn gvColInterestAmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ritxtInterstAmount;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ritxtFDNo;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.LabelControl lblTotalAmount;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
    }
}