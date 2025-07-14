namespace ACPP.Modules.Transaction
{
    partial class frmDenomination
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
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.gcDenomination = new DevExpress.XtraGrid.GridControl();
            this.gvDenomination = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colVoucherID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtCount = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colDenominationID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDenomination = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtDenomination = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colMulti = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtMultiple = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtTotalAmount = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblBankName = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblDenominationsFor = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblAmount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDenomination)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDenomination)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtDenomination)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtMultiple)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtTotalAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBankName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDenominationsFor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnOK);
            this.layoutControl1.Controls.Add(this.gcDenomination);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(5, 5);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(588, 54, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(388, 284);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(319, 260);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(67, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(248, 260);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(67, 22);
            this.btnOK.StyleController = this.layoutControl1;
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "&Ok";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // gcDenomination
            // 
            this.gcDenomination.Location = new System.Drawing.Point(2, 42);
            this.gcDenomination.MainView = this.gvDenomination;
            this.gcDenomination.Name = "gcDenomination";
            this.gcDenomination.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rtxtMultiple,
            this.rtxtCount,
            this.rtxtTotalAmount,
            this.rtxtDenomination});
            this.gcDenomination.Size = new System.Drawing.Size(384, 214);
            this.gcDenomination.TabIndex = 4;
            this.gcDenomination.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDenomination});
            this.gcDenomination.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.gcDenomination_ProcessGridKey);
            // 
            // gvDenomination
            // 
            this.gvDenomination.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvDenomination.Appearance.FooterPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gvDenomination.Appearance.FooterPanel.Options.UseFont = true;
            this.gvDenomination.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvDenomination.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colVoucherID,
            this.colDenominationID,
            this.colDenomination,
            this.colMulti,
            this.colCount,
            this.colAmount,
            this.colLedgerId});
            this.gvDenomination.GridControl = this.gcDenomination;
            this.gvDenomination.Name = "gvDenomination";
            this.gvDenomination.OptionsNavigation.EnterMoveNextColumn = true;
            this.gvDenomination.OptionsView.ShowFooter = true;
            this.gvDenomination.OptionsView.ShowGroupPanel = false;
            this.gvDenomination.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvDenomination.OptionsView.ShowIndicator = false;
            // 
            // colVoucherID
            // 
            this.colVoucherID.Caption = "Voucher ID";
            this.colVoucherID.ColumnEdit = this.rtxtCount;
            this.colVoucherID.FieldName = "VOUCHER_ID";
            this.colVoucherID.Name = "colVoucherID";
            // 
            // rtxtCount
            // 
            this.rtxtCount.AutoHeight = false;
            this.rtxtCount.Name = "rtxtCount";
            this.rtxtCount.Leave += new System.EventHandler(this.rtxtAmount_Leave);
            // 
            // colDenominationID
            // 
            this.colDenominationID.Caption = "Denominaiton ID";
            this.colDenominationID.FieldName = "DENOMINATION_ID";
            this.colDenominationID.Name = "colDenominationID";
            // 
            // colDenomination
            // 
            this.colDenomination.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colDenomination.AppearanceHeader.Options.UseFont = true;
            this.colDenomination.Caption = "Denominations";
            this.colDenomination.ColumnEdit = this.rtxtDenomination;
            this.colDenomination.FieldName = "DENOMINATION";
            this.colDenomination.Name = "colDenomination";
            this.colDenomination.OptionsColumn.AllowEdit = false;
            this.colDenomination.OptionsColumn.AllowFocus = false;
            this.colDenomination.OptionsColumn.FixedWidth = true;
            this.colDenomination.Visible = true;
            this.colDenomination.VisibleIndex = 0;
            this.colDenomination.Width = 102;
            // 
            // rtxtDenomination
            // 
            this.rtxtDenomination.AutoHeight = false;
            this.rtxtDenomination.Name = "rtxtDenomination";
            // 
            // colMulti
            // 
            this.colMulti.Caption = "Multiple";
            this.colMulti.ColumnEdit = this.rtxtMultiple;
            this.colMulti.FieldName = "MULTIPLE";
            this.colMulti.Name = "colMulti";
            this.colMulti.OptionsColumn.AllowEdit = false;
            this.colMulti.OptionsColumn.AllowFocus = false;
            this.colMulti.OptionsColumn.FixedWidth = true;
            this.colMulti.OptionsColumn.ShowCaption = false;
            this.colMulti.Visible = true;
            this.colMulti.VisibleIndex = 1;
            this.colMulti.Width = 20;
            // 
            // rtxtMultiple
            // 
            this.rtxtMultiple.AutoHeight = false;
            this.rtxtMultiple.Name = "rtxtMultiple";
            // 
            // colCount
            // 
            this.colCount.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.colCount.AppearanceCell.Options.UseBackColor = true;
            this.colCount.Caption = "Count";
            this.colCount.ColumnEdit = this.rtxtCount;
            this.colCount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colCount.FieldName = "COUNT";
            this.colCount.Name = "colCount";
            this.colCount.OptionsColumn.FixedWidth = true;
            this.colCount.OptionsColumn.ShowCaption = false;
            this.colCount.Visible = true;
            this.colCount.VisibleIndex = 2;
            this.colCount.Width = 102;
            // 
            // colAmount
            // 
            this.colAmount.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colAmount.AppearanceHeader.Options.UseFont = true;
            this.colAmount.Caption = "Amount";
            this.colAmount.ColumnEdit = this.rtxtTotalAmount;
            this.colAmount.DisplayFormat.FormatString = "n";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "AMOUNT";
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.AllowEdit = false;
            this.colAmount.OptionsColumn.AllowFocus = false;
            this.colAmount.OptionsColumn.FixedWidth = true;
            this.colAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "AMOUNT", "{0:n}")});
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 3;
            this.colAmount.Width = 137;
            // 
            // rtxtTotalAmount
            // 
            this.rtxtTotalAmount.AutoHeight = false;
            this.rtxtTotalAmount.Name = "rtxtTotalAmount";
            // 
            // colLedgerId
            // 
            this.colLedgerId.Caption = "Ledger ID";
            this.colLedgerId.FieldName = "LEDGER_ID";
            this.colLedgerId.Name = "colLedgerId";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.lblBankName,
            this.lblDenominationsFor,
            this.lblAmount,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.emptySpaceItem3,
            this.emptySpaceItem2,
            this.emptySpaceItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(388, 284);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcDenomination;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 40);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(388, 218);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(388, 218);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(388, 218);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // lblBankName
            // 
            this.lblBankName.AllowHotTrack = false;
            this.lblBankName.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblBankName.AppearanceItemCaption.Options.UseFont = true;
            this.lblBankName.CustomizationFormText = "LabellblBankName";
            this.lblBankName.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBankName.Location = new System.Drawing.Point(26, 0);
            this.lblBankName.MaxSize = new System.Drawing.Size(352, 20);
            this.lblBankName.MinSize = new System.Drawing.Size(352, 20);
            this.lblBankName.Name = "lblBankName";
            this.lblBankName.Size = new System.Drawing.Size(352, 20);
            this.lblBankName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblBankName.Text = "LabellblBankName";
            this.lblBankName.TextSize = new System.Drawing.Size(116, 16);
            // 
            // lblDenominationsFor
            // 
            this.lblDenominationsFor.AllowHotTrack = false;
            this.lblDenominationsFor.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblDenominationsFor.AppearanceItemCaption.Options.UseFont = true;
            this.lblDenominationsFor.CustomizationFormText = "lblDenominationsFor";
            this.lblDenominationsFor.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDenominationsFor.Location = new System.Drawing.Point(79, 20);
            this.lblDenominationsFor.MaxSize = new System.Drawing.Size(124, 20);
            this.lblDenominationsFor.MinSize = new System.Drawing.Size(124, 20);
            this.lblDenominationsFor.Name = "lblDenominationsFor";
            this.lblDenominationsFor.Size = new System.Drawing.Size(124, 20);
            this.lblDenominationsFor.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblDenominationsFor.Text = "Denominations For :";
            this.lblDenominationsFor.TextSize = new System.Drawing.Size(116, 16);
            // 
            // lblAmount
            // 
            this.lblAmount.AllowHotTrack = false;
            this.lblAmount.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblAmount.AppearanceItemCaption.Options.UseFont = true;
            this.lblAmount.CustomizationFormText = "Amount";
            this.lblAmount.Location = new System.Drawing.Point(203, 20);
            this.lblAmount.MaxSize = new System.Drawing.Size(185, 20);
            this.lblAmount.MinSize = new System.Drawing.Size(185, 20);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(185, 20);
            this.lblAmount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblAmount.Text = "Amount";
            this.lblAmount.TextSize = new System.Drawing.Size(116, 16);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnOK;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(246, 258);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(71, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(71, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(71, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnCancel;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(317, 258);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(71, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(71, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(71, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(26, 20);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(26, 20);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(26, 20);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 20);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(79, 20);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(79, 20);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(79, 20);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 258);
            this.emptySpaceItem2.MaxSize = new System.Drawing.Size(246, 26);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(246, 26);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(246, 26);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.CustomizationFormText = "emptySpaceItem4";
            this.emptySpaceItem4.Location = new System.Drawing.Point(378, 0);
            this.emptySpaceItem4.MaxSize = new System.Drawing.Size(10, 20);
            this.emptySpaceItem4.MinSize = new System.Drawing.Size(10, 20);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(10, 20);
            this.emptySpaceItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem4.Text = "emptySpaceItem4";
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmDenomination
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(398, 294);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmDenomination";
            this.Text = "Denomination";
            this.Load += new System.EventHandler(this.frmDenomination_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDenomination)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDenomination)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtDenomination)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtMultiple)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtTotalAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBankName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDenominationsFor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gcDenomination;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDenomination;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherID;
        private DevExpress.XtraGrid.Columns.GridColumn colDenominationID;
        private DevExpress.XtraGrid.Columns.GridColumn colDenomination;
        private DevExpress.XtraGrid.Columns.GridColumn colMulti;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtMultiple;
        private DevExpress.XtraGrid.Columns.GridColumn colCount;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtCount;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.SimpleLabelItem lblBankName;
        private DevExpress.XtraLayout.SimpleLabelItem lblDenominationsFor;
        private DevExpress.XtraLayout.SimpleLabelItem lblAmount;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtTotalAmount;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtDenomination;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerId;
    }
}